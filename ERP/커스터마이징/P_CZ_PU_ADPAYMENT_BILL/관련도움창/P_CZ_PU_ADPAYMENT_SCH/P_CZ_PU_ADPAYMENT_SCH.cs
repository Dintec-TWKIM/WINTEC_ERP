using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.ERPU;

namespace cz
{
    public partial class P_CZ_PU_ADPAYMENT_SCH : Duzon.Common.Forms.CommonDialog
    {
        private string 지급형태;
        private string 거래구분;

        public DataTable resultDt;

        public P_CZ_PU_ADPAYMENT_SCH()
        {
            InitializeComponent();
        }

        public P_CZ_PU_ADPAYMENT_SCH(string 지급형태, string 거래구분, string 매입처코드, string 매입처, string 영업그룹코드, string 영업그룹)
        {
            InitializeComponent();

            this.지급형태 = 지급형태;
            this.거래구분 = 거래구분;

            this.ctx매입처.CodeValue = 매입처코드;
            this.ctx매입처.CodeName = 매입처;

            this.ctx구매그룹.CodeValue = 영업그룹코드;
            this.ctx구매그룹.CodeName = 영업그룹;
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);

            this._flex.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("NO_ADPAY", "지급번호", 100);
            this._flex.SetCol("DT_ADPAY", "지급일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("NM_EXCH", "통화명", 60);
            this._flex.SetCol("RT_EXCH", "환율", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flex.SetCol("CD_PJT", "프로젝트", 100);
            this._flex.SetCol("NO_DOCU", "전표번호", 100);
            this._flex.SetCol("NO_DOLINE", "전표라인", 100);
            this._flex.SetCol("AM_EX", "지급금액", 115, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex.SetCol("AM_ADPS_EX", "정리액", 115, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex.SetCol("AM_REMAIN_EX", "정리잔액", 115, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex.SetCol("AM", "지급금액(원화)", 115, false, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("AM_ADPS", "정리액(원화)", 115, false, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("AM_REMAIN", "정리잔액(원화)", 115, false, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("NM_FG_TRANS", "거래구분", 80);
            this._flex.SetCol("NM_SUPPLIER", "매입처", 100);
            this._flex.SetCol("NM_PURGRP", "구매그룹", 80);
            this._flex.SetCol("NM_EMP", "담당자", 60);

            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        private void InitEvent()
        {
            this.btn조회.Click += new EventHandler(this.btn조회_Click);
            this.btn확인.Click += new EventHandler(this.btn확인_Click);
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.dtp지급일자.StartDateToString = "20160101";
            this.dtp지급일자.EndDate = Global.MainFrame.GetDateTimeToday();

            this.cbo거래구분.DataSource = Global.MainFrame.GetComboDataCombine("N;PU_C000016");
            this.cbo거래구분.ValueMember = "CODE";
            this.cbo거래구분.DisplayMember = "NAME";
            this.cbo거래구분.SelectedValue = this.거래구분;

            this.cbo지급형태.DataSource = MA.GetCodeUser(new string[] { "001", "002" }, new string[] { Global.MainFrame.DD("국내매입"), Global.MainFrame.DD("해외매입") });
            this.cbo지급형태.ValueMember = "CODE";
            this.cbo지급형태.DisplayMember = "NAME";
            this.cbo지급형태.SelectedValue = this.지급형태;
        }

        private void btn조회_Click(object sender, EventArgs e)
        {
            DataTable dataTable;

            try
            {
                dataTable = DBHelper.GetDataTable("SP_CZ_PU_ADPAYMENT_SCH_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                             Global.MainFrame.LoginInfo.Language,
                                                                                             this.dtp지급일자.StartDateToString,
                                                                                             this.dtp지급일자.EndDateToString,
                                                                                             this.ctx매입처.CodeValue,
                                                                                             this.cbo거래구분.SelectedValue,
                                                                                             this.ctx담당자.CodeValue,
                                                                                             this.ctx구매그룹.CodeValue });

                this._flex.Binding = dataTable;

                if (!this._flex.HasNormalRow)
                {
                    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn확인_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;

            try
            {
                if (!this._flex.HasNormalRow) return;

                this.resultDt = this._flex.DataTable.Clone();
                dataRowArray = this._flex.DataTable.Select("S = 'Y'", string.Empty, DataViewRowState.CurrentRows);

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    Global.MainFrame.ShowMessage("반제대상액 또는 선택된 자료가 없습니다.");
                }
                else
                {
                    foreach (DataRow row in dataRowArray)
                    {
                        this.resultDt.ImportRow(row);
                    }
                    
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
    }
}

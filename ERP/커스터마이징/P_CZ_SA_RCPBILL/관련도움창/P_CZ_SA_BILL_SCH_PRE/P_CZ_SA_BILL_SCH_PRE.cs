using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Duzon.Common.BpControls;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.ERPU;

namespace cz
{
    public partial class P_CZ_SA_BILL_SCH_PRE : Duzon.Common.Forms.CommonDialog
    {
        private object[] m_re = null;

        public DataTable resultDt;

        public P_CZ_SA_BILL_SCH_PRE()
        {
            this.InitializeComponent();
        }

        public P_CZ_SA_BILL_SCH_PRE(object[] m_result)
        {
            this.InitializeComponent();
            this.m_re = m_result;
        }

        protected override void InitLoad()
        {
            try
            {
                base.InitLoad();

                this.InitGrid();
                this.InitEvent();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        protected override void InitPaint()
        {
            try
            {
                base.InitPaint();

                this.dtp기간.StartDateToString = "20160101";
                this.dtp기간.EndDate = Global.MainFrame.GetDateTimeToday();

                DataSet dsCombo = Global.MainFrame.GetComboData("S;MA_AISPOSTH;300", "N;PU_C000016");
                this.cbo수금형태.DataSource = dsCombo.Tables[0];
                this.cbo수금형태.DisplayMember = "NAME";
                this.cbo수금형태.ValueMember = "CODE";
                this.cbo거래구분.DataSource = dsCombo.Tables[1];
                this.cbo거래구분.DisplayMember = "NAME";
                this.cbo거래구분.ValueMember = "CODE";

                this.ctx매출처.CodeValue = this.m_re[0] as string;
                this.ctx수금처.CodeValue = this.m_re[1] as string;
                this.ctx영업그룹.CodeValue = this.m_re[2] as string;
                this.ctx담당자.CodeValue = this.m_re[3] as string;
                this.ctx매출처.CodeName = this.m_re[4] as string;
                this.ctx수금처.CodeName = this.m_re[5] as string;
                this.ctx영업그룹.CodeName = this.m_re[6] as string;
                this.ctx담당자.CodeName = this.m_re[7] as string;

                this.cbo거래구분.SelectedValue = (this.m_re[8] as string);
                this.cbo거래구분.Enabled = false;

                this.cbo수금형태.SelectedValue = (this.m_re[9] as string);
                this.cbo수금형태.Enabled = false;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void InitEvent()
        {

            this.btn조회.Click += new EventHandler(this.btn조회_Click);
            this.btn확인.Click += new EventHandler(this.btn확인_Click);
            this.btn닫기.Click += new EventHandler(this.btn닫기_Click);

            this.ctx매출처.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);

            this._flex.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("NO_RCP", "수금번호", 100);
            this._flex.SetCol("CD_PJT", "프로젝트", 100);
            this._flex.SetCol("NO_DOCU", "전표번호", 100);
            this._flex.SetCol("NO_DOLINE", "전표라인", 100);
            this._flex.SetCol("DT_RCP", "수금일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("NM_TP", "수금형태", 100);
            this._flex.SetCol("CD_EXCH_NAME", "통화명", 100, false);
            this._flex.SetCol("RT_EXCH", "환율", 100, false, typeof(Decimal), FormatTpType.EXCHANGE_RATE);
            this._flex.SetCol("AM_RCP_A_EX", "선수금", 115, false, typeof(Decimal), FormatTpType.FOREIGN_MONEY);
            this._flex.SetCol("AM_RCP_A", "선수금(원화)", 115, false, typeof(Decimal), FormatTpType.MONEY);
            this._flex.SetCol("AM_RCPS_EX", "정리액", 115, false, typeof(Decimal), FormatTpType.FOREIGN_MONEY);
            this._flex.SetCol("AM_RCPS", "정리액(원화)", 115, false, typeof(Decimal), FormatTpType.MONEY);
            this._flex.SetCol("AM_REMAIN_EX", "정리잔액", 115, false, typeof(Decimal), FormatTpType.FOREIGN_MONEY);
            this._flex.SetCol("AM_REMAIN", "정리잔액(원화)", 115, false, typeof(Decimal), FormatTpType.MONEY);
            this._flex.SetCol("LN_CD_PARTNER", "거래처", 100);
            this._flex.SetCol("LN_BILL_PARTNER", "수금처", 100);
            this._flex.SetCol("NM_SALEGRP", "영업그룹", 100);
            this._flex.SetCol("NM_KOR", "담당자", 100);

            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);   
        }

        private void btn조회_Click(object sender, EventArgs e)
        {
            DataTable dataTable;

            try
            {
                dataTable = DBHelper.GetDataTable("SP_CZ_SA_BILL_SCH_PRE_SELECT", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                 this.dtp기간.StartDateToString,
                                                                                                 this.dtp기간.EndDateToString,
                                                                                                 this.cbo수금형태.SelectedValue,
                                                                                                 this.cbo거래구분.SelectedValue,
                                                                                                 this.ctx매출처.CodeValue,
                                                                                                 this.ctx수금처.CodeValue,
                                                                                                 this.ctx영업그룹.CodeValue,
                                                                                                 this.ctx담당자.CodeValue });

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

        private void btn닫기_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Control_QueryAfter(object sender, BpQueryArgs e)
        {
            if (e.DialogResult == DialogResult.Cancel) return;

            if (e.ControlName == this.ctx매출처.Name)
            {
                this.ctx수금처.CodeValue = e.HelpReturn.Rows[0]["CD_PARTNER"].ToString();
                this.ctx수금처.CodeName = e.HelpReturn.Rows[0]["LN_PARTNER"].ToString();
            }
        }
    }
}

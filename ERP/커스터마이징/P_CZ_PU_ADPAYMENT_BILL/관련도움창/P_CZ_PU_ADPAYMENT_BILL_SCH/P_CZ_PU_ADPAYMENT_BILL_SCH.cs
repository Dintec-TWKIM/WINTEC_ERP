using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;

namespace cz
{
    public partial class P_CZ_PU_ADPAYMENT_BILL_SCH : Duzon.Common.Forms.CommonDialog
    {
        private string 거래구분;
        public string 정리번호
        {
            get
            {
                if (this._flex.HasNormalRow == true)
                    return D.GetString(this._flex["NO_BILLS"]);
                else
                    return string.Empty;
            }
        }

        public P_CZ_PU_ADPAYMENT_BILL_SCH()
        {
            InitializeComponent();
        }

        public P_CZ_PU_ADPAYMENT_BILL_SCH(string 거래구분, string 매입처코드, string 매입처, string 구매그룹코드, string 구매그룹)
        {
            InitializeComponent();

            this.거래구분 = 거래구분;
            
            this.ctx매입처.CodeValue = 매입처코드;
            this.ctx매입처.CodeName = 매입처;

            this.ctx구매그룹.CodeValue = 구매그룹코드;
            this.ctx구매그룹.CodeName = 구매그룹;
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

            this._flex.SetCol("NO_BILLS", "정리번호", 100);
            this._flex.SetCol("DT_BILLS", "정리일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("AM_BILLS", "정리액", 90, true, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("AM_IV", "반제액", 90, true, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("NM_PARTNER", "매입처", 100);
            this._flex.SetCol("NM_BILLTGRP", "구매그룹", 100);
            this._flex.SetCol("NM_KOR", "담당자", 70);

            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        private void InitEvent()
        {
            this.btn조회.Click += new EventHandler(this.btn조회_Click);
            this.btn확인.Click += new EventHandler(this.btn확인_Click);
            this._flex.DoubleClick += new EventHandler(this.btn확인_Click);
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.dtp정리일자.StartDate = Global.MainFrame.GetDateTimeToday().AddMonths(-3);
            this.dtp정리일자.EndDate = Global.MainFrame.GetDateTimeToday();

            DataSet comboData = Global.MainFrame.GetComboData("N;PU_C000016");

            this.cbo거래구분.DataSource = comboData.Tables[0];
            this.cbo거래구분.ValueMember = "CODE";
            this.cbo거래구분.DisplayMember = "NAME";
            this.cbo거래구분.SelectedValue = this.거래구분;
        }

        private void btn조회_Click(object sender, EventArgs e)
        {
            DataTable dataTable;

            try
            {
                dataTable = DBHelper.GetDataTable("SP_CZ_PU_ADPAYMENT_BILL_SCH_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                  this.dtp정리일자.StartDateToString,
                                                                                                  this.dtp정리일자.EndDateToString,
                                                                                                  this.cbo거래구분.SelectedValue,
                                                                                                  this.ctx매입처.CodeValue,
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
            try
            {
                if (!this._flex.HasNormalRow) return;
                if (this._flex.MouseRow < this._flex.Rows.Fixed) return;

                if (this.정리번호 != string.Empty)
                    this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
    }
}

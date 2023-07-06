using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.Common.Forms;
using Duzon.ERPU;

namespace cz
{
    public partial class P_CZ_MA_PARTNER_REQ_CHECK : Duzon.Common.Forms.CommonDialog
    {
        public P_CZ_MA_PARTNER_REQ_CHECK()
        {
            InitializeComponent();
        }

        public P_CZ_MA_PARTNER_REQ_CHECK(string 거래처명)
            : this()
        {
            this.txt거래처명.Text = 거래처명;
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.cur비교자리수.DecimalValue = 5;

            this.InitGrid();
            this.InitEvent();

            if (!string.IsNullOrEmpty(this.txt거래처명.Text))
                this.btn조회_Click(null, null);
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);

            this._flex.SetCol("NM_COMPANY", "회사명", 200);
            this._flex.SetCol("CD_PARTNER", "거래처코드", 80);
            this._flex.SetCol("LN_PARTNER", "거래처명", 150);

            this._flex.ExtendLastCol = true;

            this._flex.SettingVersion = "0.0.0.1";
            this._flex.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        private void InitEvent()
        {
            this.btn조회.Click += new EventHandler(this.btn조회_Click);
            this.btn닫기.Click += new EventHandler(this.btn닫기_Click);
        }

        private void btn조회_Click(object sender, EventArgs e)
        {
            string query;

            try
            {
                if (string.IsNullOrEmpty(this.txt거래처명.Text))
                {
                    Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl거래처명.Text);
                    return;
                }

                if (cur비교자리수.DecimalValue == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지._은_보다커야합니다, this.lbl비교자리수.Text, "0");
                    return;
                }

                query = @"SELECT MP.CD_COMPANY,
                                 MC.NM_COMPANY,
                                 MP.CD_PARTNER,
                                 MP.LN_PARTNER 
                          FROM MA_PARTNER MP WITH(NOLOCK) 
                          LEFT JOIN MA_COMPANY MC WITH(NOLOCK) ON MC.CD_COMPANY = MP.CD_COMPANY
                          WHERE MP.LN_PARTNER LIKE '%' + SUBSTRING('" + this.txt거래처명.Text.Replace("'", "''") + "', 1, " + D.GetInt(this.cur비교자리수.DecimalValue) + ") + '%'" +
                         "ORDER BY MP.LN_PARTNER";

                this._flex.Binding = DBHelper.GetDataTable(query);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn닫기_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        public void 재조회(string 거래처명)
        {
            try
            {
                this.txt거래처명.Text = 거래처명;
                this.btn조회_Click(null, null);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
    }
}

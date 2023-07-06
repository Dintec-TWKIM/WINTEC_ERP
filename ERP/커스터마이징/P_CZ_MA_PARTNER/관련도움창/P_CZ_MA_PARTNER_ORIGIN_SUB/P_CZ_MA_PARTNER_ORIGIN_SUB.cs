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
using Duzon.Common.Util;
using Dintec;
using Duzon.ERPU;

namespace cz
{
    public partial class P_CZ_MA_PARTNER_ORIGIN_SUB : Duzon.Common.Forms.CommonDialog
    {
        private string _거래처코드;

        public P_CZ_MA_PARTNER_ORIGIN_SUB()
        {
            InitializeComponent();
        }

        public P_CZ_MA_PARTNER_ORIGIN_SUB(string 거래처코드)
        {
            InitializeComponent();

            this._거래처코드 = 거래처코드;
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitEvent();
            this.InitGrid();

            this.btn검색_Click(null, null);
        }

        private void InitEvent()
        {
            this.btn검색.Click += new EventHandler(this.btn검색_Click);
            this.btn추가.Click += new EventHandler(this.btn추가_Click);
            this.btn삭제.Click += new EventHandler(this.btn삭제_Click);
            this.btn저장.Click += new EventHandler(this.btn저장_Click);
            this.btn종료.Click += new EventHandler(this.btn종료_Click);
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);

            this._flex.SetCol("CD_ORIGIN", "코드", 80);
            this._flex.SetCol("NM_ORIGIN", "원산지", 200, true);
            this._flex.SetCol("NM_ORIGIN_RPT", "원산지(출력물)", 200, true);
            
            this._flex.ExtendLastCol = true;

            this._flex.SettingVersion = "0.0.0.1";
            this._flex.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        private void btn검색_Click(object sender, EventArgs e)
        {
            string query;

            try
            {
                query = @"SELECT CD_COMPANY,
                                 CD_PARTNER,
                                 CD_ORIGIN,
                                 NM_ORIGIN,
                                 NM_ORIGIN_RPT
                          FROM CZ_MA_PARTNER_ORIGIN WITH(NOLOCK)
                          WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                         "AND CD_PARTNER = '" + this._거래처코드 + "'";

                this._flex.Binding = DBHelper.GetDataTable(query);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn추가_Click(object sender, EventArgs e)
        {
            try
            {
                this._flex.Row = this._flex.Rows.Count - 1;
                this._flex.Rows.Add();

                this._flex[this._flex.Row, "CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                this._flex[this._flex.Row, "CD_PARTNER"] = this._거래처코드;
                this._flex[this._flex.Row, "CD_ORIGIN"] = this.SeqMax().ToString();
                
                this._flex.AddFinished();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn삭제_Click(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                if (this._flex.Row < 0) return;

                dt = DBHelper.GetDataTable(@"SELECT 1 
                                             FROM CZ_MA_PARTNER WITH(NOLOCK)
                                             WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                            "AND CD_PARTNER = '" + this._거래처코드 + "'" + Environment.NewLine +
                                            "AND CD_ORIGIN = '" + this._flex["CD_ORIGIN"].ToString() + "'");

                if (dt != null && dt.Rows.Count > 0)
                {
                    Global.MainFrame.ShowMessage("사용중인 항목 입니다. 사용 해제 후 삭제 하시기 바랍니다.");
                    return;
                }

                this._flex.RemoveItem(this._flex.Row);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn저장_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Save())
                    this._flex.AcceptChanges();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn종료_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._flex.GetChanges() != null)
                    Global.MainFrame.ShowMessage("저장 후 종료하시기 바랍니다.");
                else
                    this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private bool Save()
        {
            try
            {
                DataTable dt = this._flex.GetChanges();

                if (dt != null && dt.Rows.Count != 0)
                {
                    SpInfo si = new SpInfo();

                    si.DataValue = Util.GetXmlTable(dt);
                    si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                    si.UserID = Global.MainFrame.LoginInfo.UserID;

                    si.SpNameInsert = "SP_CZ_MA_PARTNER_ORIGIN_XML";
                    si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                    DBHelper.Save(si);
                    Global.MainFrame.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
                }

                return true;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return false;
        }

        private Decimal SeqMax()
        {
            Decimal num = 1;
            DataTable dataTable = DBHelper.GetDataTable(@"SELECT MAX(CD_ORIGIN) AS CD_ORIGIN 
                                                          FROM CZ_MA_PARTNER_ORIGIN WITH(NOLOCK)  
                                                          WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
                                                        @"AND CD_PARTNER = '" + this._거래처코드 + "'");

            if (dataTable != null && dataTable.Rows.Count != 0)
                num = (D.GetDecimal(dataTable.Rows[0]["CD_ORIGIN"]) + 1);

            if (num <= this._flex.GetMaxValue("CD_ORIGIN"))
                num = (this._flex.GetMaxValue("CD_ORIGIN") + 1);

            return num;
        }
    }
}

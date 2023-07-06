using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_SA_GIR_CHARGE : Duzon.Common.Forms.CommonDialog
    {
        P_CZ_SA_GIR_CHARGE_BIZ _biz = new P_CZ_SA_GIR_CHARGE_BIZ();
        private string _회사코드, _협조전번호;

        public P_CZ_SA_GIR_CHARGE(string 회사코드, string 협조전번호, string 통화명)
        {
            InitializeComponent();

            this._회사코드 = 회사코드;
            this._협조전번호 = 협조전번호;
            this.txt통화명.Text = 통화명;

            DataTable dt = DBHelper.GetDataTable(string.Format(@"SELECT ISNULL(SUM(OL.QT_CLS), 0) AS QT_CLS
                                                                 FROM SA_GIRL GL WITH(NOLOCK)
                                                                 LEFT JOIN MM_QTIO OL WITH(NOLOCK) ON OL.CD_COMPANY = GL.CD_COMPANY AND OL.NO_ISURCV = GL.NO_GIR AND OL.NO_ISURCVLINE = GL.SEQ_GIR
                                                                 WHERE GL.CD_COMPANY = '{0}'
                                                                 AND GL.NO_GIR = '{1}' 
                                                                 GROUP BY GL.CD_COMPANY, GL.NO_GIR", this._회사코드, this._협조전번호));

            if (dt != null && 
                dt.Rows.Count > 0 && 
                D.GetDecimal(dt.Rows[0]["QT_CLS"]) > 0)
			{
                this._flex.Enabled = false;
                this.btn조회.Enabled = false;
                this.btn추가.Enabled = false;
                this.btn제거.Enabled = false;
                this.btn저장.Enabled = false;
            }
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();

            this.Btn조회_Click(null, null);
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);

            this._flex.SetCol("CD_ITEM", "품목코드", 300, true);
            this._flex.SetCol("AM_EX_CHARGE", "외화금액", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex.SetCol("DC_RMK", "비고", 100, true);

            this._flex.VerifyPrimaryKey = new string[] { "CD_ITEM" };

            this._flex.ExtendLastCol = true;

            this._flex.SettingVersion = "1.0.0.0";
            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            this._flex.SetDataMap("CD_ITEM", MA.GetCodeUser(new string[] { "SD0001", 
                                                                           "SD0002", 
                                                                           "SD0003" }, new string[] { "FREIGHT CHARGE",
                                                                                                      "HANDLING CHARGE",
                                                                                                      "PACKING CHARGE" }), "CODE", "NAME");
        }

        private void InitEvent()
        {
            this.btn조회.Click += Btn조회_Click;
            this.btn추가.Click += Btn추가_Click;
            this.btn제거.Click += Btn제거_Click;
            this.btn저장.Click += Btn저장_Click;
        }

        private void Btn조회_Click(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                dt = this._biz.Search(new object[] { this._회사코드, this._협조전번호 });

                this._flex.Binding = dt;

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

        private void Btn추가_Click(object sender, EventArgs e)
        {
            try
            {
                this._flex.Rows.Add();
                this._flex.Row = this._flex.Rows.Count - 1;

                this._flex["NO_GIR"] = this._협조전번호;

                this._flex.AddFinished();
                this._flex.Col = this._flex.Cols.Fixed;
                this._flex.Focus();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void Btn저장_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._flex.IsDataChanged == false) return;
                if (!Global.MainFrame.VerifyGrid(this._flex)) return;

                if (!this._biz.Save(this._flex.GetChanges())) return;

                this._flex.AcceptChanges();

                Global.MainFrame.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void Btn제거_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow) return;

                this._flex.GetDataRow(this._flex.Row).Delete();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        
    }
}

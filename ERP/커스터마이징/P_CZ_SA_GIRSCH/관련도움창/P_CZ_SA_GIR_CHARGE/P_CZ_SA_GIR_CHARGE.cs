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

        public P_CZ_SA_GIR_CHARGE(string 회사코드, string 협조전번호)
        {
            InitializeComponent();

            this._회사코드 = 회사코드;
            this._협조전번호 = 협조전번호;

            DataTable dt = DBHelper.GetDataTable(string.Format(@"SELECT WD.TP_CHARGE, 
                                                                 	    MC.NM_SYSDEF AS NM_EXCH,
                                                                        ISNULL(GL.QT_CLS, 0) AS QT_CLS
                                                                 FROM CZ_SA_GIRH_WORK_DETAIL WD WITH(NOLOCK)
                                                                 JOIN (SELECT GL.CD_COMPANY, GL.NO_GIR,
                                                                 	     	  MAX(GL.CD_EXCH) AS CD_EXCH,
                                                                              SUM(OL.QT_CLS) AS QT_CLS
                                                                 	   FROM SA_GIRL GL WITH(NOLOCK)
                                                                       LEFT JOIN MM_QTIO OL WITH(NOLOCK) ON OL.CD_COMPANY = GL.CD_COMPANY AND OL.NO_ISURCV = GL.NO_GIR AND OL.NO_ISURCVLINE = GL.SEQ_GIR
                                                                 	   GROUP BY GL.CD_COMPANY, GL.NO_GIR) GL
                                                                 ON GL.CD_COMPANY = WD.CD_COMPANY AND GL.NO_GIR = WD.NO_GIR
                                                                 LEFT JOIN MA_CODEDTL MC WITH(NOLOCK) ON MC.CD_COMPANY = GL.CD_COMPANY AND MC.CD_FIELD = 'MA_B000005' AND MC.CD_SYSDEF = GL.CD_EXCH
                                                                 WHERE WD.CD_COMPANY = '{0}'
                                                                 AND WD.NO_GIR = '{1}'", this._회사코드, this._협조전번호));

            this.txt통화명.Text = dt.Rows[0]["NM_EXCH"].ToString();

            this.cbo비용청구방법.DataSource = MA.GetCodeUser(new string[] { "001", "002" }, new string[] { "자동청구", "수동청구" }, true);
            this.cbo비용청구방법.DisplayMember = "NAME";
            this.cbo비용청구방법.ValueMember = "CODE";
            this.cbo비용청구방법.SelectedValue = (dt.Rows[0]["TP_CHARGE"].ToString() == "" ? "001" : dt.Rows[0]["TP_CHARGE"].ToString());

            if (D.GetDecimal(dt.Rows[0]["QT_CLS"]) > 0)
            {
                this.cbo비용청구방법.Enabled = false;
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

            this.cbo비용청구방법.MouseWheel += DropDownComboBox_MouseWheel;
        }

        private static void DropDownComboBox_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
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
                if (!this._biz.Save(this._flex.GetChanges())) return;

                this._flex.AcceptChanges();

                string query = @"SELECT NO_GIR, CD_ITEM 
FROM CZ_SA_GIRH_CHARGE GC WITH(NOLOCK)
WHERE GC.CD_COMPANY = '{0}'
AND GC.NO_GIR = '{1}'";

                DataTable dt = DBHelper.GetDataTable(string.Format(query, this._회사코드, this._협조전번호));

                if (dt != null && dt.Rows.Count > 0)
                    this.cbo비용청구방법.SelectedValue = "002";
                else
                    this.cbo비용청구방법.SelectedValue = "001";

                DBHelper.ExecuteScalar(string.Format(@"UPDATE CZ_SA_GIRH_WORK_DETAIL
                                                       SET TP_CHARGE = '{0}'
                                                       WHERE CD_COMPANY = '{1}'
                                                       AND NO_GIR = '{2}'", this.cbo비용청구방법.SelectedValue, this._회사코드, this._협조전번호));

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

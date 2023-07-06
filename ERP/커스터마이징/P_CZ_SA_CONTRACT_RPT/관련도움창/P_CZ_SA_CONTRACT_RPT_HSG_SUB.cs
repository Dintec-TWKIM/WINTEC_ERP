using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Duzon.Common.Forms;
using System.Text.RegularExpressions;
using Duzon.ERPU;

namespace cz
{
	public partial class P_CZ_SA_CONTRACT_RPT_HSG_SUB : Duzon.Common.Forms.CommonDialog
	{
		public P_CZ_SA_CONTRACT_RPT_HSG_SUB(string 발주번호, string 공사번호)
		{
			InitializeComponent();

			this.txt발주번호.Text = 발주번호;
			this.txt공사번호.Text = 공사번호;
		}

		protected override void InitLoad()
		{
			base.InitLoad();

			this.InitEvent();
		}

		private void InitEvent()
		{
			this.btn저장.Click += new EventHandler(this.btn저장_Click);
		}

		private void btn저장_Click(object sender, EventArgs e)
		{
			try
			{
                Regex regex = new Regex(@"^(K[A-Z]{2}\d{7})");

                if (!regex.IsMatch(this.txt공사번호.Text))
                {
                    Global.MainFrame.ShowMessage("현대공사번호 형식에 맞지 않습니다.");
                    return;
                }

                DBHelper.ExecuteScalar(@"UPDATE PU_POH
                                         SET NO_ORDER = '" + this.txt공사번호.Text + "'" + Environment.NewLine +
                                        "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                        "AND NO_PO = '" + this.txt발주번호.Text + "'");

                Global.MainFrame.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
                this.DialogResult = DialogResult.OK;
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}
	}
}

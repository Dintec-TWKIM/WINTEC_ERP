using Duzon.Common.Forms;
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
	public partial class P_CZ_SA_GIR_AUTO_CPR : Duzon.Common.Forms.CommonDialog
	{
		P_CZ_SA_GIR_AUTO_CPR_BIZ _biz = new P_CZ_SA_GIR_AUTO_CPR_BIZ();
		string _회사코드, _협조전번호;

		public P_CZ_SA_GIR_AUTO_CPR(string 회사코드, string 협조전번호)
		{
			InitializeComponent();

			this._회사코드 = 회사코드;
			this._협조전번호 = 협조전번호;

			this.btn조회.Click += Btn조회_Click;
			this.btn저장.Click += Btn저장_Click;

			this.Btn조회_Click(null, null);
		}

		private void Btn조회_Click(object sender, EventArgs e)
		{
			try
			{
				DataTable dt = this._biz.Search(this._회사코드, this._협조전번호);

				if (dt == null || dt.Rows.Count == 0)
				{
					Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
					return;
				}
				else
				{
					this.txt납품처메일.Text = dt.Rows[0]["NO_EMAIL"].ToString();
					this.txtCPR발송메일.Text = dt.Rows[0]["NO_DELIVERY_EMAIL"].ToString();
					this.chk자동발송제외.Checked = (dt.Rows[0]["YN_EXCLUDE_CPR"].ToString() == "Y" ? true : false);
					this.txt상업송장비고.Text = dt.Rows[0]["DC_RMK_CI"].ToString();
					this.txt전달사항.Text = dt.Rows[0]["DC_RMK_CPR"].ToString();
					this.chkCPR발송완료.Checked = (dt.Rows[0]["YN_CPR"].ToString() == "Y" ? true : false);
				}
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
				this._biz.Save(this._회사코드,
							   this._협조전번호,
							   this.txtCPR발송메일.Text,
							   (this.chk자동발송제외.Checked == true ? "Y" : "N"),
							   this.txt상업송장비고.Text,
							   this.txt전달사항.Text,
							   (this.chkCPR발송완료.Checked == true ? "Y" : "N"));
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}
	}
}

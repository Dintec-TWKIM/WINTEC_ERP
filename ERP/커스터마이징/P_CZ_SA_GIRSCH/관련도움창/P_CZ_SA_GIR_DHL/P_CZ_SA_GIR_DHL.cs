using Duzon.Common.BpControls;
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
	public partial class P_CZ_SA_GIR_DHL : Duzon.Common.Forms.CommonDialog
	{
		P_CZ_SA_GIR_DHL_BIZ _biz = new P_CZ_SA_GIR_DHL_BIZ();

		string _회사코드, _협조전번호, _송장번호;

		public P_CZ_SA_GIR_DHL(string 회사코드, string 협조전번호)
		{
			InitializeComponent();

			this._회사코드 = 회사코드;
			this._협조전번호 = 협조전번호;

			this.InitEvent();
		}

		private void InitEvent()
		{
			this.ctx국가.QueryBefore += Ctx국가_QueryBefore;
		}

		protected override void InitPaint()
		{
			base.InitPaint();

			this.cbo보험유무.DataSource = MA.GetCodeUser(new string[] { "Y", "N" }, new string[] { "예", "아니오" });
			this.cbo보험유무.ValueMember = "CODE";
			this.cbo보험유무.DisplayMember = "NAME";

			this.cbo관세납부여부.DataSource = MA.GetCodeUser(new string[] { "Y", "N" }, new string[] { "예", "아니오" });
			this.cbo관세납부여부.ValueMember = "CODE";
			this.cbo관세납부여부.DisplayMember = "NAME";

			this.btn조회.Click += Btn조회_Click;
			this.btn저장.Click += Btn저장_Click;

			this.Btn조회_Click(null, null);
		}

		private void Ctx국가_QueryBefore(object sender, BpQueryArgs e)
		{
			try
			{
				e.HelpParam.P61_CODE1 = "CD_SYSDEF AS CODE, NM_SYSDEF AS NAME";
				e.HelpParam.P62_CODE2 = "CZ_MA_CODEDTL WITH(NOLOCK)";
				e.HelpParam.P63_CODE3 = @"WHERE CD_COMPANY = 'K100' 
										  AND CD_FIELD = 'CZ_MA00004'";
				e.HelpParam.P64_CODE4 = "ORDER BY NM_SYSDEF";
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void Btn조회_Click(object sender, EventArgs e)
		{
			DataTable dt;

			try
			{
				dt = this._biz.Search(this._회사코드, this._협조전번호);

				if (dt == null || dt.Rows.Count == 0)
				{
					Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
					return;
				}
				else
				{
					this._송장번호 = dt.Rows[0]["NO_INV"].ToString();
					this.txtTEL.Text = dt.Rows[0]["REMARK1"].ToString();
					this.txtPIC.Text = dt.Rows[0]["REMARK3"].ToString();
					this.txtBillingAccount.Text = dt.Rows[0]["REMARK4"].ToString();
					this.txt우편번호.Text = dt.Rows[0]["REMARK5"].ToString();
					this.ctx국가.CodeValue = dt.Rows[0]["CD_COUNTRY"].ToString();
					this.ctx국가.CodeName = dt.Rows[0]["NM_COUNTRY"].ToString();
					this.cbo보험유무.SelectedValue = dt.Rows[0]["YN_INSURANCE"].ToString();
					this.cbo관세납부여부.SelectedValue = dt.Rows[0]["YN_DUTY"].ToString();
					this.txt도착지.Text = dt.Rows[0]["PORT_ARRIVER"].ToString();
					this.txt주소1.Text = dt.Rows[0]["ADDR1_CONSIGNEE"].ToString();
					this.txt주소2.Text = dt.Rows[0]["ADDR2_CONSIGNEE"].ToString();
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
							   this._송장번호,
							   this.txtTEL.Text,
							   this.txtPIC.Text,
							   this.txtBillingAccount.Text,
							   this.txt우편번호.Text,
							   this.ctx국가.CodeValue,
							   this.cbo보험유무.SelectedValue.ToString(),
							   this.cbo관세납부여부.SelectedValue.ToString(),
							   this.txt도착지.Text,
							   this.txt주소1.Text,
							   this.txt주소2.Text);

				Global.MainFrame.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}
	}
}

using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using DX;
using DzHelpFormLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;

namespace cz
{
	public partial class P_CZ_SA_GIR_AUTO_MAIL : Duzon.Common.Forms.CommonDialog
	{
		private string _회사코드, _협조전번호;

		public P_CZ_SA_GIR_AUTO_MAIL()
		{
			InitializeComponent();
		}

		public P_CZ_SA_GIR_AUTO_MAIL(string 회사코드, string 협조전번호)
		{
			InitializeComponent();

			this._회사코드 = 회사코드;
			this._협조전번호 = 협조전번호;
		}

		protected override void InitLoad()
		{
			base.InitLoad();

			this.InitGrid();
			this.InitEvent();
		}

		protected override void InitPaint()
		{
			base.InitPaint();

			this.txt협조전번호.Text = this._협조전번호;

			this.cbo운송구분.DataSource = MA.GetCodeUser(new string[] { string.Empty, "001", "002", "003", "004" }, new string[] { string.Empty, "항송", "해송", "COURIER", "항송&해송" });
			this.cbo운송구분.ValueMember = "CODE";
			this.cbo운송구분.DisplayMember = "NAME";

			this.btn저장.Enabled = false;
			this.btn포장비메일발송.Enabled = false;

			#region 초기화
			this.rdo매출처발송.Checked = true;
			this.rdo포워더발송.Checked = false;

			this.cbo운송구분.SelectedValue = string.Empty;

			this.chkDHL.Checked = false;
			this.chkFEDEX.Checked = false;
			this.chkTPO.Checked = false;
			this.chk쉥커.Checked = false;
			this.chkSR.Checked = false;
			this.chk기타.Checked = false;

			this.ctx목적지DHL.CodeValue = string.Empty;
			this.ctx목적지DHL.CodeName = string.Empty;
			this.ctx목적지FEDEX.CodeValue = string.Empty;
			this.ctx목적지FEDEX.CodeName = string.Empty;
			this.txt목적지.Text = string.Empty;

			this.txt수하인.Text = string.Empty;

			this.ctx기타업체.CodeValue = string.Empty;
			this.ctx기타업체.CodeName = string.Empty;

			this.txt기타업체메일.Text = string.Empty;
			#endregion

			#region 비활성화
			this.rdo매출처발송.Enabled = false;
			this.rdo포워더발송.Enabled = false;

			this.cbo운송구분.Enabled = false;

			this.chkDHL.Enabled = false;
			this.chkFEDEX.Enabled = false;
			this.chkTPO.Enabled = false;
			this.chk쉥커.Enabled = false;
			this.chkSR.Enabled = false;
			this.chk기타.Enabled = false;

			this.ctx목적지DHL.ReadOnly = ReadOnly.TotalReadOnly;
			this.ctx목적지FEDEX.ReadOnly = ReadOnly.TotalReadOnly;
			this.txt목적지.ReadOnly = true;

			this.txt수하인.ReadOnly = true;

			this.ctx기타업체.ReadOnly = ReadOnly.TotalReadOnly;

			this.txt기타업체메일.ReadOnly = true;
			#endregion
		}

		private void InitGrid()
		{
			this._flexGrid.BeginSetting(1, 1, false);

			this._flexGrid.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
			this._flexGrid.SetCol("YN_CHECK", "메일검증", 45, false, CheckTypeEnum.Y_N);
			this._flexGrid.SetCol("YN_PACK", "기본발송", 45, false, CheckTypeEnum.Y_N);
			this._flexGrid.SetCol("SEQ", "순번", 45);
			this._flexGrid.SetCol("NM_PTR", "담당자명", 100);
			this._flexGrid.SetCol("NM_EMAIL", "메일주소", 100, true);
			
			this._flexGrid.SettingVersion = "1.0.0.0";
			this._flexGrid.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

			this._flexGrid.SetDummyColumn(new string[] { "S" });
		}

		private void InitEvent()
		{
			this.btn조회.Click += Btn조회_Click;
			this.btn저장.Click += Btn저장_Click;
			this.btn포장비메일발송.Click += Btn포장비메일발송_Click;

			this.rdo매출처발송.CheckedChanged += Radio_CheckedChanged;
			this.rdo포워더발송.CheckedChanged += Radio_CheckedChanged;

			this.cbo운송구분.SelectedValueChanged += Cbo운송구분_SelectedValueChanged;

			this.chkDHL.CheckedChanged += ChkDHL_CheckedChanged;
			this.chkFEDEX.CheckedChanged += ChkFEDEX_CheckedChanged;
			this.chkTPO.CheckedChanged += ChkTPO_CheckedChanged;
			this.chk쉥커.CheckedChanged += Chk쉥커_CheckedChanged;
			this.chkSR.CheckedChanged += ChkSR_CheckedChanged;
			this.chk기타.CheckedChanged += Chk기타_CheckedChanged;

			this.ctx목적지DHL.QueryBefore += Ctx목적지DHL_QueryBefore;
			this.ctx목적지FEDEX.QueryBefore += Ctx목적지FEDEX_QueryBefore;

			this._flexGrid.ValidateEdit += _flexGrid_ValidateEdit;
			this._flexGrid.AfterEdit += _flexGrid_AfterEdit;
		}

		private void _flexGrid_ValidateEdit(object sender, ValidateEditEventArgs e)
		{
			try
			{
				string oldValue = D.GetString(((FlexGrid)sender).GetData(e.Row, e.Col));

				if (this._flexGrid.Cols[e.Col].Name == "S" && 
				    this._flexGrid["YN_CHECK"].ToString() == "N")
				{
					Global.MainFrame.ShowMessage("메일주소가 잘못 되었습니다.");
					this._flexGrid["S"] = oldValue;
					e.Cancel = true;
					return;
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void Ctx목적지FEDEX_QueryBefore(object sender, BpQueryArgs e)
		{
			try
			{
				e.HelpParam.P00_CHILD_MODE = "목적지(FEDEX)";
				e.HelpParam.P61_CODE1 = "MC.CD_SYSDEF AS CODE, MC.NM_SYSDEF AS NAME";
				e.HelpParam.P62_CODE2 = "CZ_MA_CODEDTL MC WITH(NOLOCK)";
				e.HelpParam.P63_CODE3 = "WHERE MC.CD_COMPANY = '" + this._회사코드 + "'" + Environment.NewLine +
										"AND MC.CD_FIELD = 'CZ_SA00055'";
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void Ctx목적지DHL_QueryBefore(object sender, BpQueryArgs e)
		{
			try
			{
				e.HelpParam.P00_CHILD_MODE = "목적지(DHL)";
				e.HelpParam.P61_CODE1 = "MC.CD_SYSDEF AS CODE, MC.NM_SYSDEF AS NAME";
				e.HelpParam.P62_CODE2 = "CZ_MA_CODEDTL MC WITH(NOLOCK)";
				e.HelpParam.P63_CODE3 = "WHERE MC.CD_COMPANY = '" + this._회사코드 + "'" + Environment.NewLine +
										"AND MC.CD_FIELD = 'CZ_SA00054'";
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void Chk기타_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.chk기타.Checked == true)
				{
					this.ctx기타업체.ReadOnly = ReadOnly.None;

					this.txt기타업체메일.ReadOnly = false;
				}
				else
				{
					this.ctx기타업체.CodeValue = string.Empty;
					this.ctx기타업체.CodeName = string.Empty;

					this.ctx기타업체.ReadOnly = ReadOnly.TotalReadOnly;

					this.txt기타업체메일.ReadOnly = true;
				}
				
				if (this.chkTPO.Checked || this.chk쉥커.Checked || this.chkSR.Checked || this.chk기타.Checked)
					this.txt목적지.ReadOnly = false;
				else
				{
					this.txt목적지.Text = string.Empty;
					this.txt목적지.ReadOnly = true;
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void ChkSR_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.chkTPO.Checked || this.chk쉥커.Checked || this.chkSR.Checked || this.chk기타.Checked)
					this.txt목적지.ReadOnly = false;
				else
				{
					this.txt목적지.Text = string.Empty;
					this.txt목적지.ReadOnly = true;
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void Chk쉥커_CheckedChanged(object sender, EventArgs e)
		{
			try
			{	
				if (this.chkTPO.Checked || this.chk쉥커.Checked || this.chkSR.Checked || this.chk기타.Checked)
					this.txt목적지.ReadOnly = false;
				else
				{
					this.txt목적지.Text = string.Empty;
					this.txt목적지.ReadOnly = true;
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void ChkTPO_CheckedChanged(object sender, EventArgs e)
		{
			try
			{	
				if (this.chkTPO.Checked || this.chk쉥커.Checked || this.chkSR.Checked || this.chk기타.Checked)
					this.txt목적지.ReadOnly = false;
				else
				{
					this.txt목적지.Text = string.Empty;
					this.txt목적지.ReadOnly = true;
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void ChkFEDEX_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.chkFEDEX.Checked == true)
				{
					this.ctx목적지FEDEX.ReadOnly = ReadOnly.None;
				}
				else
				{
					this.ctx목적지FEDEX.CodeValue = string.Empty;
					this.ctx목적지FEDEX.CodeName = string.Empty;

					this.ctx목적지FEDEX.ReadOnly = ReadOnly.TotalReadOnly;
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void ChkDHL_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.chkDHL.Checked == true)
					this.ctx목적지DHL.ReadOnly = ReadOnly.None;
				else
				{
					this.ctx목적지DHL.CodeValue = string.Empty;
					this.ctx목적지DHL.CodeName = string.Empty;

					this.ctx목적지DHL.ReadOnly = ReadOnly.TotalReadOnly;
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void Cbo운송구분_SelectedValueChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.cbo운송구분.SelectedValue.ToString() == "001")
				{
					#region 항송
					this.chkDHL.Checked = false;
					this.chkFEDEX.Checked = false;

					this.chkDHL.Enabled = false;
					this.chkFEDEX.Enabled = false;
					this.chkTPO.Enabled = true;
					this.chk쉥커.Enabled = true;
					this.chkSR.Enabled = true;
					this.chk기타.Enabled = true;
					#endregion
				}
				else if (this.cbo운송구분.SelectedValue.ToString() == "002")
				{
					#region 해송
					this.chkDHL.Checked = false;
					this.chkFEDEX.Checked = false;

					this.chkDHL.Enabled = false;
					this.chkFEDEX.Enabled = false;
					this.chkTPO.Enabled = true;
					this.chk쉥커.Enabled = true;
					this.chkSR.Enabled = true;
					this.chk기타.Enabled = true;
					#endregion
				}
				else if (this.cbo운송구분.SelectedValue.ToString() == "003")
				{
					#region COURIER
					this.chkTPO.Checked = false;
					this.chk쉥커.Checked = false;
					this.chkSR.Checked = false;
					this.chk기타.Checked = false;

					this.chkDHL.Enabled = true;
					this.chkFEDEX.Enabled = true;
					this.chkTPO.Enabled = false;
					this.chk쉥커.Enabled = false;
					this.chkSR.Enabled = false;
					this.chk기타.Enabled = false;
					#endregion
				}
				else if (this.cbo운송구분.SelectedValue.ToString() == "004")
				{
					#region 항송&해송
					this.chkDHL.Checked = false;
					this.chkFEDEX.Checked = false;

					this.chkDHL.Enabled = false;
					this.chkFEDEX.Enabled = false;
					this.chkTPO.Enabled = true;
					this.chk쉥커.Enabled = true;
					this.chkSR.Enabled = true;
					this.chk기타.Enabled = true;
					#endregion
				}
				else
				{
					this.chkDHL.Checked = false;
					this.chkFEDEX.Checked = false;
					this.chkTPO.Checked = false;
					this.chk쉥커.Checked = false;
					this.chkSR.Checked = false;
					this.chk기타.Checked = false;

					this.chkDHL.Enabled = false;
					this.chkFEDEX.Enabled = false;
					this.chkTPO.Enabled = false;
					this.chk쉥커.Enabled = false;
					this.chkSR.Enabled = false;
					this.chk기타.Enabled = false;
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void Radio_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.rdo매출처발송.Checked == true)
				{
					this.rdo매출처발송.Checked = true;
					this.rdo포워더발송.Checked = false;

					this.cbo운송구분.SelectedValue = string.Empty;

					this.chkDHL.Checked = false;
					this.chkFEDEX.Checked = false;
					this.chkTPO.Checked = false;
					this.chk쉥커.Checked = false;
					this.chkSR.Checked = false;
					this.chk기타.Checked = false;

					this.ctx목적지DHL.CodeValue = string.Empty;
					this.ctx목적지DHL.CodeName = string.Empty;
					this.ctx목적지FEDEX.CodeValue = string.Empty;
					this.ctx목적지FEDEX.CodeName = string.Empty;
					this.txt목적지.Text = string.Empty;

					this.txt수하인.Text = string.Empty;

					this.ctx기타업체.CodeValue = string.Empty;
					this.ctx기타업체.CodeName = string.Empty;

					this.txt기타업체메일.Text = string.Empty;

					this.cbo운송구분.Enabled = false;

					this.chkDHL.Enabled = false;
					this.chkFEDEX.Enabled = false;
					this.chkTPO.Enabled = false;
					this.chk쉥커.Enabled = false;
					this.chkSR.Enabled = false;
					this.chk기타.Enabled = false;

					this.ctx목적지DHL.ReadOnly = ReadOnly.TotalReadOnly;
					this.ctx목적지FEDEX.ReadOnly = ReadOnly.TotalReadOnly;
					this.txt목적지.ReadOnly = true;

					this.txt수하인.ReadOnly = true;

					this.ctx기타업체.ReadOnly = ReadOnly.TotalReadOnly;

					this.txt기타업체메일.ReadOnly = true;
				}
				else
				{
					this.cbo운송구분.Enabled = true;
					this.txt수하인.ReadOnly = false;
				}		
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
				dt = DBHelper.GetDataTable("SP_CZ_SA_GIR_AUTO_MAIL_SETTING_S", new object[] { this._회사코드, this._협조전번호 });

				if (dt != null && dt.Rows.Count > 0)
				{
					DataRow dr = dt.Rows[0];

					if (dr["TP_SEND"].ToString() == "002")
					{
						this.rdo매출처발송.Checked = false;
						this.rdo포워더발송.Checked = true;
					}
					else
					{
						this.rdo매출처발송.Checked = true;
						this.rdo포워더발송.Checked = false;
					}

					this.cbo운송구분.SelectedValue = dr["TP_TRANS"].ToString();

					this.chkDHL.Checked = (dr["YN_DHL"].ToString() == "Y" ? true : false);
					this.chkFEDEX.Checked = (dr["YN_FEDEX"].ToString() == "Y" ? true : false);
					this.chkTPO.Checked = (dr["YN_TPO"].ToString() == "Y" ? true : false);
					this.chk쉥커.Checked = (dr["YN_SK"].ToString() == "Y" ? true : false);
					this.chkSR.Checked = (dr["YN_SR"].ToString() == "Y" ? true : false);
					this.chk기타.Checked = (dr["YN_ETC"].ToString() == "Y" ? true : false);

					this.ctx목적지DHL.CodeValue = dr["CD_COUNTRY_DHL"].ToString();
					this.ctx목적지DHL.CodeName = dr["NM_COUNTRY_DHL"].ToString();
					this.ctx목적지FEDEX.CodeValue = dr["CD_COUNTRY_FEDEX"].ToString();
					this.ctx목적지FEDEX.CodeName = dr["NM_COUNTRY_FEDEX"].ToString();

					this.txt목적지.Text = dr["DC_COUNTRY_ETC"].ToString();

					this.txt수하인.Text = dr["DC_CONSIGNEE"].ToString();

					this.ctx기타업체.CodeValue = dr["CD_PARTNER_ETC"].ToString();
					this.ctx기타업체.CodeName = dr["NM_PARTNER_ETC"].ToString();

					this.txt기타업체메일.Text = dr["DC_EMAIL_ETC"].ToString();
				}
				else
				{
					#region 초기화
					this.rdo매출처발송.Checked = true;
					this.rdo포워더발송.Checked = false;

					this.cbo운송구분.SelectedValue = string.Empty;

					this.chkDHL.Checked = false;
					this.chkFEDEX.Checked = false;
					this.chkTPO.Checked = false;
					this.chk쉥커.Checked = false;
					this.chkSR.Checked = false;
					this.chk기타.Checked = false;

					this.ctx목적지DHL.CodeValue = string.Empty;
					this.ctx목적지DHL.CodeName = string.Empty;
					this.ctx목적지FEDEX.CodeValue = string.Empty;
					this.ctx목적지FEDEX.CodeName = string.Empty;
					this.txt목적지.Text = string.Empty;

					this.txt수하인.Text = string.Empty;

					this.ctx기타업체.CodeValue = string.Empty;
					this.ctx기타업체.CodeName = string.Empty;

					this.txt기타업체메일.Text = string.Empty;
					#endregion
				}

				dt = DBHelper.GetDataTable("SP_CZ_SA_GIR_AUTO_MAIL_PTR", new object[] { this._회사코드, 
																						this._협조전번호 });

				if (dt != null && dt.Rows.Count > 0)
				{
					this.ctx매출처.CodeValue = dt.Rows[0]["CD_PARTNER"].ToString();
					this.ctx매출처.CodeName = dt.Rows[0]["LN_PARTNER"].ToString();

					this._flexGrid.Binding = dt;
				}

				this.btn저장.Enabled = true;
				this.btn포장비메일발송.Enabled = true;

				this.rdo매출처발송.Enabled = true;
				this.rdo포워더발송.Enabled = true;
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void Btn저장_Click(object sender, EventArgs e)
		{
			DataTable dt;
			DataRow dr;

			try
			{
				dt = new DataTable();

				dt.Columns.Add("CD_COMPANY");
				dt.Columns.Add("NO_GIR");
				dt.Columns.Add("TP_SEND");
				dt.Columns.Add("TP_TRANS");
				dt.Columns.Add("YN_DHL");
				dt.Columns.Add("YN_FEDEX");
				dt.Columns.Add("YN_TPO");
				dt.Columns.Add("YN_SK");
				dt.Columns.Add("YN_SR");
				dt.Columns.Add("YN_ETC");
				dt.Columns.Add("CD_COUNTRY_DHL");
				dt.Columns.Add("CD_COUNTRY_FEDEX");
				dt.Columns.Add("DC_COUNTRY_ETC");
				dt.Columns.Add("DC_CONSIGNEE");
				dt.Columns.Add("AM_DHL", typeof(decimal));
				dt.Columns.Add("AM_FEDEX", typeof(decimal));
				dt.Columns.Add("AM_TPO", typeof(decimal));
				dt.Columns.Add("AM_SK", typeof(decimal));
				dt.Columns.Add("AM_SR", typeof(decimal));
				dt.Columns.Add("AM_ETC", typeof(decimal));
				dt.Columns.Add("CD_PARTNER_ETC");
				dt.Columns.Add("DC_EMAIL_ETC");

				dr = dt.NewRow();

				dr["CD_COMPANY"] = this._회사코드;
				dr["NO_GIR"] = this._협조전번호;

				if (this.rdo매출처발송.Checked == true)
					dr["TP_SEND"] = "001";
				else
					dr["TP_SEND"] = "002";

				dr["TP_TRANS"] = this.cbo운송구분.SelectedValue;

				dr["YN_DHL"] = (this.chkDHL.Checked == true ? "Y" : "N");
				dr["YN_FEDEX"] = (this.chkFEDEX.Checked == true ? "Y" : "N");
				dr["YN_TPO"] = (this.chkTPO.Checked == true ? "Y" : "N");
				dr["YN_SK"] = (this.chk쉥커.Checked == true ? "Y" : "N");
				dr["YN_SR"] = (this.chkSR.Checked == true ? "Y" : "N");
				dr["YN_ETC"] = (this.chk기타.Checked == true ? "Y" : "N");

				dr["CD_COUNTRY_DHL"] = this.ctx목적지DHL.CodeValue;
				dr["CD_COUNTRY_FEDEX"] = this.ctx목적지FEDEX.CodeValue;
				dr["DC_COUNTRY_ETC"] = this.txt목적지.Text;

				dr["DC_CONSIGNEE"] = this.txt수하인.Text;

				dr["CD_PARTNER_ETC"] = this.ctx기타업체.CodeValue.ToString();

				dr["DC_EMAIL_ETC"] = this.txt기타업체메일.Text;

				dt.Rows.Add(dr);

				DBHelper.ExecuteNonQuery("SP_CZ_SA_GIR_AUTO_MAIL_SETTING_JSON", new object[] { dt.Json(), 
																							   Global.MainFrame.LoginInfo.UserID });

				Global.MainFrame.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void _flexGrid_AfterEdit(object sender, RowColEventArgs e)
		{
			FlexGrid flexGrid;
			string query;

			try
			{
				flexGrid = ((FlexGrid)sender);

				if (flexGrid.HasNormalRow == false) return;

				if (flexGrid.Cols[e.Col].Name == "NM_EMAIL")
				{
					if (flexGrid["SEQ"].ToString() == "-1")
					{
						if (flexGrid["S"].ToString() != "Y")
						{
							flexGrid["NM_EMAIL"] = string.Empty;
							Global.MainFrame.ShowMessage("기타 메일주소는 선택 후에 수정 가능 합니다.");
							return;
						}
						
						query = @"UPDATE AM
SET AM.DC_ETC = '{4}',
	AM.ID_UPDATE = '{5}',
	AM.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
FROM CZ_SA_GIR_AUTO_MAIL_PTR AM
WHERE AM.CD_COMPANY = '{0}'
AND AM.CD_PARTNER = '{1}'
AND AM.NO_GIR = '{2}'
AND AM.SEQ = '{3}'";

						DBHelper.ExecuteScalar(string.Format(query, this._회사코드,
																	this.ctx매출처.CodeValue,
																	this.txt협조전번호.Text,
																	flexGrid["SEQ"].ToString(),
																	flexGrid["NM_EMAIL"].ToString(),
																	Global.MainFrame.LoginInfo.UserID));
					}
					else
					{
						query = @"UPDATE FP
SET FP.NM_EMAIL = '{3}',
	FP.ID_UPDATE = '{4}',
	FP.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
FROM FI_PARTNERPTR FP
WHERE FP.CD_COMPANY = '{0}'
AND FP.CD_PARTNER = '{1}'
AND FP.SEQ = {2}";

						DBHelper.ExecuteScalar(string.Format(query, this._회사코드,
																	this.ctx매출처.CodeValue,
																	flexGrid["SEQ"].ToString(),
																	flexGrid["NM_EMAIL"].ToString(),
																	Global.MainFrame.LoginInfo.UserID));
					}
				}
				else if (flexGrid.Cols[e.Col].Name == "S")
				{
					if (flexGrid["S"].ToString() == "Y")
					{
						query = @"INSERT INTO CZ_SA_GIR_AUTO_MAIL_PTR
(
	CD_COMPANY,
	NO_GIR,
	CD_PARTNER,
	SEQ,
	ID_INSERT,
	DTS_INSERT
)
VALUES
(
	'{0}',
	'{1}',
	'{2}',
	{3},
	'{4}',
	NEOE.SF_SYSDATE(GETDATE())
)";

						DBHelper.ExecuteScalar(string.Format(query, new object[] { this._회사코드, 
																				   this._협조전번호,
																				   this.ctx매출처.CodeValue,
																				   flexGrid["SEQ"].ToString(),
																				   Global.MainFrame.LoginInfo.UserID }));
					}
					else
					{
						query = @"DELETE AM 
FROM CZ_SA_GIR_AUTO_MAIL_PTR AM
WHERE AM.CD_COMPANY = '{0}'
AND AM.NO_GIR = '{1}'
AND AM.CD_PARTNER = '{2}'
AND AM.SEQ = {3}";

						DBHelper.ExecuteScalar(string.Format(query, new object[] { this._회사코드,
																				   this._협조전번호,
																				   this.ctx매출처.CodeValue,
																				   flexGrid["SEQ"].ToString() }));
					}
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void Btn포장비메일발송_Click(object sender, EventArgs e)
		{
			string 제목, 보내는사람, 받는사람, html, body, query;
			DataTable dt;

			try
			{
				제목 = "WOODEN PACKING CHARGE INCURRED";

				보내는사람 = Global.MainFrame.LoginInfo.EMail;

				if (string.IsNullOrEmpty(보내는사람))
				{
					Global.MainFrame.ShowMessage("보내는사람이 지정되어 있지 않습니다.");
					return;
				}

				query = @"DECLARE @P_CD_COMPANY NVARCHAR(7) = '{0}'
DECLARE @P_NO_GIR     NVARCHAR(20) = '{1}'

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

;WITH A AS
(
    SELECT FP.NM_EMAIL
    FROM CZ_SA_GIR_AUTO_MAIL_PTR AM
    JOIN FI_PARTNERPTR FP ON FP.CD_COMPANY = AM.CD_COMPANY AND FP.CD_PARTNER = AM.CD_PARTNER AND FP.SEQ = AM.SEQ
    WHERE AM.CD_COMPANY = @P_CD_COMPANY
    AND AM.NO_GIR = @P_NO_GIR
    UNION ALL
    SELECT AM.DC_ETC AS NM_EMAIL
    FROM CZ_SA_GIR_AUTO_MAIL_PTR AM
    WHERE AM.CD_COMPANY = @P_CD_COMPANY
    AND AM.NO_GIR = @P_NO_GIR
    AND AM.SEQ < 0
)
SELECT STRING_AGG(A.NM_EMAIL, ';')  
FROM A";

				object obj = DBHelper.ExecuteScalar(string.Format(query, new object[] { this._회사코드, this._협조전번호 }));

				if (obj != null)
					받는사람 = obj.ToString();
				else
					받는사람 = string.Empty;

				if (string.IsNullOrEmpty(받는사람))
				{
					Global.MainFrame.ShowMessage("받는사람이 지정되어 있지 않습니다.");
					return;
				}

				html = @"<div style='text-align:left; font-size: 10pt; font-family: 맑은 고딕;'>
Dear sir/madam<br> 
Good day to you.<br><br>
Thanks again for your orders – they have been successfully dispatched.<br> 
Kindly find below the packing details and <span style='background-color:#FFFF00;'>packing charges</span> to be included in your payment invoice for your reference.<br><br>
*following order(s) required wooden packing for the purpose of safe handling and delivery.<br><br>
</div>

<table style='width:2050px; border:2px solid black; font-size: 10pt; font-family: 맑은 고딕; border-collapse:collapse; border-spacing:0;'>
	<colgroup width='200px' align='center'></colgroup>
	<colgroup width='200px' align='center'></colgroup>
	<colgroup width='200px' align='center'></colgroup>
	<colgroup width='250px' align='center'></colgroup>
	<colgroup width='150px' align='center'></colgroup>
	<colgroup width='200px' align='center'></colgroup>
	<colgroup width='100px' align='center'></colgroup>

	<tbody>
		<tr style='height:30px'>
			<th style='border:solid 1px black; text-align:center; background-color:#c6efce; width:200px'>VESSEL</th>
			<th style='border:solid 1px black; text-align:center; background-color:#c6efce; width:200px'>CLIENT PO</th>
			<th style='border:solid 1px black; text-align:center; background-color:#c6efce; width:200px'>PACKING TERMS OFFERED</th>                                    
			<th style='border:solid 1px black; text-align:center; background-color:#c6efce; width:250px'>PACKING DETAILS</th>
			<th style='border:solid 1px black; text-align:center; background-color:#c6efce; width:150px'>PACKING CHARGE</th>                                    
			<th style='border:solid 1px black; text-align:center; background-color:#c6efce; width:100px'>PHOTO</th>
			<th style='border:solid 1px black; text-align:center; background-color:#c6efce; width:200px'>REMARKS</th>
		</tr>
	{0}
	</tbody>
</table>

<div style='text-align:left; font-size: 10pt; font-family: 맑은 고딕;'>
<br>Please do not hesitate to get in touch with us for any assistance.<br><br>
Thank you for your working with us.
</div>";
				dt = DBHelper.GetDataTable("SP_CZ_SA_GIR_AUTO_MAIL_PACK", new object[] { this._회사코드, this._협조전번호 });

				if (dt == null || dt.Rows.Count == 0)
				{
					Global.MainFrame.ShowMessage("청구 할 수 없거나, 우든 포장 내역이 없습니다.");
					return;
				}

				body = string.Empty;

				foreach (DataRow dr in dt.Rows)
				{
					string 포장사진 = string.Empty;

					int index1 = 1;

					foreach (string url in dr["DC_URL"].ToString().Split(','))
					{
						if (string.IsNullOrEmpty(url))
							continue;

						포장사진 += "<a href='" + url + "' style='color:#0000ff; text-decoration:underline;' target='_blank'>Photo" + index1.ToString() + "</a><br>";
						index1++;
					}

					body += string.Format(@"<tr style='height: 30px'>
												<th style='border:solid 1px black; text-align:center; font-weight:normal; width:200px'>{0}</th>
												<th style='border:solid 1px black; text-align:center; font-weight:normal; width:200px'>{1}</th>
												<th style='border:solid 1px black; text-align:center; font-weight:normal; width:200px'>{2}</th>
												<th style='border:solid 1px black; text-align:center; font-weight:normal; width:250px'>{3}</th>
												<th style='border:solid 1px black; text-align:center; font-weight:normal; width:150px'>{4}</th>
												<th style='border:solid 1px black; text-align:center; font-weight:normal; width:100px'>{5}</th>
												<th style='border:solid 1px black; text-align:center; font-weight:normal; width:200px'>{6}</th>
											</tr>", dr["NM_VESSEL"].ToString(),
													dr["NO_PO_PARTNER"].ToString(),
													dr["NM_PACKING"].ToString(),
													dr["DC_PACKING"].ToString(),
													dr["AM_PACKING"].ToString(),
													포장사진,
													dr["DC_RMK"].ToString());
				}

				html = string.Format(html, body);

				this.메일발송(보내는사람, 받는사람, 보내는사람, string.Empty, 제목, string.Empty, html);

				Global.MainFrame.ShowMessage(공통메세지._작업을완료하였습니다, this.btn포장비메일발송.Text);
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private bool 메일발송(string 보내는사람, string 받는사람, string 참조, string 숨은참조, string 제목, string 본문, string html)
		{
			MailMessage mailMessage;
			SmtpClient smtpClient;
			string[] tempText;
			string address, name, id, pw, domain, query;

			try
			{
				#region 기본설정
				mailMessage = new MailMessage();
				mailMessage.SubjectEncoding = Encoding.UTF8;
				mailMessage.BodyEncoding = Encoding.UTF8;
				mailMessage.IsBodyHtml = true;
				#endregion

				#region 보내는사람
				tempText = 보내는사람.Split('|');

				if (tempText.Length == 1)
				{
					address = tempText[0];
					name = tempText[0];
				}
				else if (tempText.Length == 2)
				{
					address = tempText[0];
					name = tempText[1];
				}
				else
					return false;

				tempText = address.Split('@');

				if (tempText.Length != 2) return false;

				id = tempText[0];
				domain = tempText[1];

				query = @"SELECT DM.DM_NAME,
								 DU.DU_USERID,
								 DU.DU_PWD
						  FROM MCDOMAINUSER DU WITH(NOLOCK)
						  LEFT JOIN MCDOMAIN DM WITH(NOLOCK) ON DM.DM_ID = DU.DM_ID
						  WHERE DM.DM_NAME = '" + domain + "'" + Environment.NewLine +
						 "AND DU.DU_USERID = '" + id + "'";

				DBMgr dbMgr = new DBMgr(DBConn.Mail);
				dbMgr.Query = query;

				pw = dbMgr.GetDataTable().Rows[0]["DU_PWD"].ToString();
				#endregion

				#region 메일정보
				mailMessage.From = new MailAddress(address, name, Encoding.UTF8);

				foreach (string 받는사람1 in 받는사람.Split(';'))
				{
					if (받는사람1.Trim() != "")
						mailMessage.To.Add(new MailAddress(받는사람1.Replace(";", "")));
				}

				foreach (string 참조1 in 참조.Split(';'))
				{
					if (참조1.Trim() != "")
						mailMessage.CC.Add(new MailAddress(참조1.Replace(";", "")));
				}

				foreach (string 숨은참조1 in 숨은참조.Split(';'))
				{
					if (숨은참조1.Trim() != "")
						mailMessage.Bcc.Add(new MailAddress(숨은참조1.Replace(";", "")));
				}

				mailMessage.Subject = 제목;

				// 본문 html로 변환할 시 <a>태그 앞뒤로 하고 <a>태그 내부는 건드리지 않음
				string body = "";
				string bodyA = "";
				string bodyB = "";
				string bodyC = "";

				int index = 본문.IndexOf("<a href=");

				if (index > 0)
				{
					bodyA = 본문.Substring(0, index);
					bodyB = 본문.Substring(index, 본문.IndexOf("</a>") + 4 - index);
					bodyC = 본문.Substring(본문.IndexOf("</a>") + 4);

					body = ""
						+ bodyA.Replace(" ", "&nbsp;").Replace(Environment.NewLine, "<br />")
						+ bodyB
						+ bodyC.Replace(" ", "&nbsp;").Replace(Environment.NewLine, "<br />");
				}
				else
				{
					body = 본문.Replace(" ", "&nbsp;").Replace(Environment.NewLine, "<br />"); ;
				}

				mailMessage.Body = "<div style='font-family:맑은 고딕; font-size:9pt'>" + body + "</div>" + html;
				#endregion

				#region 메일보내기
				smtpClient = new SmtpClient("113.130.254.131", 587);
				smtpClient.EnableSsl = false;
				smtpClient.UseDefaultCredentials = false;
				smtpClient.Credentials = new NetworkCredential(address, pw);
				smtpClient.Send(mailMessage);
				#endregion

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
}

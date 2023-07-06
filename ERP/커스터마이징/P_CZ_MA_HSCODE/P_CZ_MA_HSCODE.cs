using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Dintec;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.ERPU;
using Duzon.Common.Controls;

namespace cz
{
	public partial class P_CZ_MA_HSCODE : PageBase
	{
		public string CD_COMPANY { get; set; }
		public string CD_HSCODE { get; set; }

		public string CD_FILE { get; set; }

		public P_CZ_MA_HSCODE()
		{
			StartUp.Certify(this);
			InitializeComponent();
		}

		protected override void InitLoad()
		{
			base.InitLoad();

			InitControl();
			InitGrid();
			InitEvent();


		}

		private void InitControl()
		{
			tbxCODE.Enabled = false;

			CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;

			// 파일구분 동적 추가
			string query = @"
SELECT
	CD_SYSDEF	AS CODE
FROM V_CZ_MA_CODEDTL WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + CD_COMPANY + @"'
	AND CD_FIELD = 'CZ_SA00023'
	AND CD_FLAG1 = 'WF'
	AND ISNULL(CD_FLAG2, '') != 'CLAIM'
	AND CD_SYSDEF != 'PT'
	AND CD_SYSDEF != 'ZZ'
	AND CD_SYSDEF != 'TE'";

			DataTable dtPrefix = DBMgr.GetDataTable(query);

			for (int i = 0; i < dtPrefix.Rows.Count; i++)
			{
				CheckBoxExt chk = new CheckBoxExt();
				chk.Checked = true;
				chk.Text = dtPrefix.Rows[i]["CODE"].ToString();
				chk.Width = 41;
				chk.Left = 94 + (i * 51);
				chk.Top = 0;
				chk.Click += Chk_Click; ;

				pnlCheckBoxC.Controls.Add(chk);

				MainGrids = new FlexGrid[] { this.flexH, this.flexIMPA, this.flexKey, this.flexKeyEx };
			}

		}

		private void Chk_Click(object sender, EventArgs e)
		{
			foreach (Control con in pnlCheckBoxC.Controls)
			{
				if (con is CheckBoxExt chk)
				{
					if (chk.Checked)
					{
						CD_FILE = CD_FILE.Replace(chk.Text + ",", "").Trim();
						CD_FILE = CD_FILE + chk.Text + ",";
					}
					else
					{
						CD_FILE = CD_FILE.Replace(chk.Text + ",", "").Trim();
					}
				}
			}
			
			flexH["CD_FILE"] = CD_FILE;

		}

		private void InitGrid()
		{
			CD_FILE = string.Empty;

			#region flexH
			flexH.BeginSetting(1, 1, true);

			flexH.SetCol("CD_COMPANY", "회사", false);
			flexH.SetCol("CD_HSCODE", "CODE", 120);
			flexH.SetCol("NM_HSCODE", "코드명", 120);
			flexH.SetCol("CD_FILE", "파일형식", 120);
			flexH.SetCol("DC_RMK", "비고", 120);
			flexH.SetCol("ID_INSERT", "", false);
			flexH.SetCol("DTS_INSERT", "", false);
			flexH.SetCol("ID_UPDATE", "", false);
			flexH.SetCol("DTS_INSERT", "", false);

			flexH.Cols["CD_COMPANY"].TextAlign = TextAlignEnum.CenterCenter;
			flexH.Cols["CD_HSCODE"].TextAlign = TextAlignEnum.CenterCenter;

			flexH.SetOneGridBinding(new object[] { }, oneB);

			flexH.SettingVersion = "1.20.06.3";
			flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			#endregion flexH

			#region flexIMPA
			flexIMPA.BeginSetting(1, 1, true);

			flexIMPA.SetCol("CD_COMPANY", "회사", false);
			flexIMPA.SetCol("CD_HSCODE", "CODE", false);
			flexIMPA.SetCol("CD_TYPE", "코드명", false);
			flexIMPA.SetCol("SEQ", "SEQ", false);
			flexIMPA.SetCol("DXKEY1", "IMPA", 120);
			flexIMPA.SetCol("DC_RMK", "비고", false);
			flexIMPA.SetCol("ID_INSERT", "", false);
			flexIMPA.SetCol("DTS_INSERT", "", false);
			flexIMPA.SetCol("ID_UPDATE", "", false);
			flexIMPA.SetCol("DTS_INSERT", "", false);

			flexIMPA.Cols["DXKEY1"].TextAlign = TextAlignEnum.CenterCenter;

			flexIMPA.SetOneGridBinding(new object[] { }, oneIMPA);


			flexIMPA.SettingVersion = "1.20.06.4";
			flexIMPA.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			#endregion flexIMPA

			#region flexKey
			flexKey.BeginSetting(1, 1, true);

			flexKey.SetCol("CD_COMPANY", "회사", false);
			flexKey.SetCol("CD_HSCODE", "CODE", false);
			flexKey.SetCol("CD_TYPE", "코드명", false);
			flexKey.SetCol("SEQ", "seq", false);
			flexKey.SetCol("DXKEY1", "키워드1", 200);
			flexKey.SetCol("DXKEY2", "키워드2", 200);
			flexKey.SetCol("DXKEY3", "키워드3", 200);
			flexKey.SetCol("DXKEY4", "키워드4", 200);
			flexKey.SetCol("DC_RMK", "비고", false);
			flexKey.SetCol("ID_INSERT", "", false);
			flexKey.SetCol("DTS_INSERT", "", false);
			flexKey.SetCol("ID_UPDATE", "", false);
			flexKey.SetCol("DTS_INSERT", "", false);

			flexKey.Cols["DXKEY1"].TextAlign = TextAlignEnum.CenterCenter;
			flexKey.Cols["DXKEY2"].TextAlign = TextAlignEnum.CenterCenter;
			flexKey.Cols["DXKEY3"].TextAlign = TextAlignEnum.CenterCenter;
			flexKey.Cols["DXKEY4"].TextAlign = TextAlignEnum.CenterCenter;

			flexKey.SetOneGridBinding(new object[] { }, oneKey);

			flexKey.SettingVersion = "1.20.06.5";
			flexKey.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			#endregion flexKey

			#region flexKeyEx

			flexKeyEx.BeginSetting(1, 1, true);

			flexKeyEx.SetCol("CD_COMPANY", "회사", false);
			flexKeyEx.SetCol("CD_HSCODE", "CODE", false);
			flexKeyEx.SetCol("SEQ", "seq", false);
			flexKeyEx.SetCol("DXKEY_EX", "제외키워드", 200);
			flexKeyEx.SetCol("DC_RMK", "비고", false);
			flexKeyEx.SetCol("ID_INSERT", "", false);
			flexKeyEx.SetCol("DTS_INSERT", "", false);
			flexKeyEx.SetCol("ID_UPDATE", "", false);
			flexKeyEx.SetCol("DTS_INSERT", "", false);

			flexKeyEx.Cols["DXKEY_EX"].TextAlign = TextAlignEnum.CenterCenter;

			flexKeyEx.SetOneGridBinding(new object[] { }, oneKeyEx);

			flexKeyEx.SettingVersion = "1.20.06.6";
			flexKeyEx.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			#endregion flexKeyEx
		}


		private void InitEvent()
		{
			btnIMPA추가.Click += BtnIMPA추가_Click;
			btnIMPA삭제.Click += BtnIMPA삭제_Click;
			btnKEY삭제.Click += BtnKEY삭제_Click;
			btnKEY추가.Click += BtnKEY추가_Click;
			btn제외키워드제외.Click += Btn제외키워드제거_Click;
			btn제외키워드추가.Click += Btn제외키워드추가_Click;
			flexH.AfterRowChange += FlexH_AfterRowChange;
		}

		private void BtnKEY추가_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(CD_HSCODE))
			{
				ShowMessage("HS코드를 먼저 입력해주세요."); return;
			}

			if (!string.IsNullOrEmpty(tbxCODE.Text))
				CD_HSCODE = tbxCODE.Text;

			flexKey.Rows.Add();
			flexKey.Row = flexKey.Rows.Count - 1;
			flexKey["CD_TYPE"] = "K";
			flexKey["CD_COMPANY"] = CD_COMPANY;
			flexKey["CD_HSCODE"] = CD_HSCODE;

			flexKey.AddFinished();
			flexKey.AllowEditing = true;
		}

		private void BtnKEY삭제_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this.BeforeDelete() || !this.flexKey.HasNormalRow) return;
				this.flexKey.Rows.Remove(flexKey.Row);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void BtnIMPA추가_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(tbxCODE.Text))
				CD_HSCODE = tbxCODE.Text;

			if (string.IsNullOrEmpty(CD_HSCODE))
			{
				ShowMessage("HS코드를 먼저 입력해주세요."); return;
			}

			

			flexIMPA.Rows.Add();
			flexIMPA.Row = flexIMPA.Rows.Count - 1;
			flexIMPA["CD_TYPE"] = "I";
			flexIMPA["CD_COMPANY"] = CD_COMPANY;
			flexIMPA["CD_HSCODE"] = CD_HSCODE;

			flexIMPA.AddFinished();
			flexIMPA.AllowEditing = true;
		}

		private void BtnIMPA삭제_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this.BeforeDelete() || !this.flexIMPA.HasNormalRow) return;
				this.flexIMPA.Rows.Remove(flexIMPA.Row);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn제외키워드추가_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(CD_HSCODE))
			{
				ShowMessage("HS코드를 먼저 입력해주세요."); return;
			}

			if (!string.IsNullOrEmpty(tbxCODE.Text))
				CD_HSCODE = tbxCODE.Text;

			flexKeyEx.Rows.Add();
			flexKeyEx.Row = flexKeyEx.Rows.Count - 1;
			flexKeyEx["CD_COMPANY"] = CD_COMPANY;
			flexKeyEx["CD_HSCODE"] = CD_HSCODE;

			flexKeyEx.AddFinished();
			flexKeyEx.AllowEditing = true;
		}

		private void Btn제외키워드제거_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this.BeforeDelete() || !this.flexKeyEx.HasNormalRow) return;
				this.flexKeyEx.Rows.Remove(flexKeyEx.Row);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}


		#region 조회 
		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			base.OnToolBarSearchButtonClicked(sender, e);

			try
			{
				tbxCODE.Enabled = false;
				

				DataTable dt = null;

				dt = DBHelper.GetDataTable("P_CZ_MA_HSCODE_SELECT", new object[] { CD_COMPANY, tbx검색IMPA.Text, tbx검색NMCODE.Text });

				flexH.Redraw = false;
				flexH.Binding = dt;
				flexH.Redraw = true;

				if (!flexH.HasNormalRow)
				{
					ShowMessage(공통메세지.조건에해당하는내용이없습니다);
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}


		private void FlexH_AfterRowChange(object sender, RangeEventArgs e)
		{
			tbxIMPA.Text = "";
			tbxKey1.Text = "";
			tbxKey2.Text = "";
			tbxKey3.Text = "";
			tbxKey4.Text = "";
			tbxKeyEx.Text = "";

			if (!string.IsNullOrEmpty(flexH["CD_HSCODE"].ToString()))
				CD_HSCODE = flexH["CD_HSCODE"].ToString();
			else
				CD_HSCODE = string.Empty;

			if (!string.IsNullOrEmpty(flexH["CD_FILE"].ToString()))
				CD_FILE = flexH["CD_FILE"].ToString();
			else
				CD_FILE = string.Empty;

			DataTable dtL = null;
			DataTable dtL2 = null;
			DataTable dtL3 = null;

			dtL = DBMgr.GetDataTable("P_CZ_MA_HSCODE_SELECT_IMPA", new object[] { CD_COMPANY, CD_HSCODE });
			dtL2 = DBMgr.GetDataTable("P_CZ_MA_HSCODE_SELECT_KEY", new object[] { CD_COMPANY, CD_HSCODE });
			dtL3 = DBMgr.GetDataTable("P_CZ_MA_HSCODE_SELECT_KEYEX", new object[] { CD_COMPANY, CD_HSCODE });

			flexIMPA.Binding = dtL;
			flexKey.Binding = dtL2;
			flexKeyEx.Binding = dtL3;


			if(dtL.Rows.Count > 0)
			{
				string cdfilestr = flexH["CD_FILE"].ToString();

				foreach (Control con in pnlCheckBoxC.Controls)
				{
					if (con is CheckBoxExt chk)
					{
						if (cdfilestr.IndexOf(chk.Text) >= 0)
							chk.Checked = true;
						else
							chk.Checked = false;
					}
				}

			}

		}
		#endregion 조회

		#region 추가
		protected override bool BeforeAdd()
		{
			return base.BeforeAdd();
		}

		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
			base.OnToolBarAddButtonClicked(sender, e);

			try
			{
				tbxCODE.Enabled = true;

				flexH.Rows.Add();
				flexH.Row = flexH.Rows.Count - 1;
				flexH["CD_COMPANY"] = CD_COMPANY;

				flexH.AddFinished();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		#endregion 추가

		#region 저장

		protected override bool BeforeSave()
		{
			if (!base.BeforeSave()) return false;

			return true;
		}

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			try
			{
				if (!this.BeforeSave()) return;

				if (this.MsgAndSave(PageActionMode.Save))
					this.ShowMessage(PageResultMode.SaveGood);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		protected override bool SaveData()
		{

			if (!base.SaveData() || !this.Verify()) return false;

			if (flexH.HasNormalRow)
			{
				DataTable dt = this.flexH.GetChanges();
				string xml = Util.GetTO_Xml(dt);
				DBMgr.ExecuteNonQuery("SP_CZ_MA_HSCODE_H_XML", new object[] { xml });
			}

			if (flexIMPA.HasNormalRow)
			{
				DataTable dt = this.flexIMPA.GetChanges();
				string xml = Util.GetTO_Xml(dt);
				DBMgr.ExecuteNonQuery("SP_CZ_MA_HSCODE_KEY_XML", new object[] { xml });


			}

			if (flexKey.HasNormalRow)
			{
				DataTable dt = this.flexKey.GetChanges();
				string xml = Util.GetTO_Xml(dt);
				DBMgr.ExecuteNonQuery("SP_CZ_MA_HSCODE_KEY_XML", new object[] { xml });


			}

			if (flexKeyEx.HasNormalRow)
			{
				DataTable dt = this.flexKeyEx.GetChanges();
				string xml = Util.GetTO_Xml(dt);
				DBMgr.ExecuteNonQuery("SP_CZ_MA_HSCODE_KEY_EX_XML", new object[] { xml });


			}

			flexH.AcceptChanges();
			flexIMPA.AcceptChanges();
			flexKey.AcceptChanges();
			flexKeyEx.AcceptChanges();

			OnToolBarSearchButtonClicked(null, null);

			return true;
		}

		#endregion 저장

		#region  삭제

		public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{
			try
			{
				if (!this.BeforeDelete() || !this.flexH.HasNormalRow) return;
				this.flexH.Rows.Remove(flexH.Row);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		#endregion 삭제
	}
}

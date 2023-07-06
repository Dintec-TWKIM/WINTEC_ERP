using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.BpControls;
using DzHelpFormLib;
using Duzon.Windows.Print;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.ERPU;
using Dintec;
using Duzon.Common.Util;
using System.Net;
using System.IO;
using System.Reflection;
using Duzon.ERPU.Utils;
using System.Net.Mail;
using Aspose.Email.Outlook;

namespace cz
{
	public partial class P_CZ_MA_WORKFLOW_FILE_REG : PageBase
	{
		public string CompanyCode
		{
			get
			{
				return grd헤드["CD_COMPANY"].ToStr();
			}
		}

		public string FileNumber
		{
			get
			{
				return grd헤드["NO_FILE"].ToStr();
			}
		}


		#region ==================================================================================================== Constructor

		public P_CZ_MA_WORKFLOW_FILE_REG()
		{
			StartUp.Certify(this);
			InitializeComponent();			
		}

		#endregion

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{
			InitControl();
			InitGrid();
			InitEvent();
		}

		private void InitControl()
		{			
			// 콤보박스
			DataSet ds = GetDb.Code("CD_FILE", "CZ_MA00005");
			cbo파일번호.DataBind(ds.Tables[0], true);
			cbo파일유형.DataBind(ds.Tables[1], true);

			DataTable dt = new DataTable();
			dt.Columns.Add("CODE");
			dt.Columns.Add("NAME");

			dt.Rows.Add("NO_REF"		, DD("문의번호"));
			dt.Rows.Add("NO_PO_PARTNER"	, DD("주문번호"));
			dt.Rows.Add("NO_IV"			, DD("계산서번호"));

			cbo키워드.DataBind(dt, false);
			cbo키워드.SelectedIndex = 0;

			// 나머지
			tbx포커스.Left = -1000;

			dtp등록일자.StartDateToString = Util.GetToday(-30);
			dtp등록일자.EndDateToString = Util.GetToday();

			ctx회사코드.CodeValue = LoginInfo.CompanyCode;
			ctx회사코드.CodeName = LoginInfo.CompanyName;

			grd헤드.DetailGrids = new FlexGrid[] { grd라인 };
			MainGrids = new FlexGrid[] { grd라인 };

			splitContainer1.SplitterDistance = 1500;

			if (!LoginInfo.UserID.In("S-343", "S-391", "S-458"))
			{
				Grant.CanDelete = false;
				Grant.CanPrint = false;
			}
		}

		private void InitGrid()
		{
			// ---------------------------------------------------------------------------------------------------- Head
			grd헤드.BeginSetting(1, 1, false);

			grd헤드.SetCol("CD_COMPANY"		, "회사명"		, false);
			grd헤드.SetCol("NO_FILE"			, "파일번호"		, 80);
			grd헤드.SetCol("NO_FILE_OLD"		, "파일번호(OLD)", 80);
			grd헤드.SetCol("DT_INSERT"		, "등록일"		, 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			grd헤드.SetCol("LN_PARTNER"		, "매출처"		, 150);
			grd헤드.SetCol("NM_VESSEL"		, "선명"			, 150);
			grd헤드.SetCol("NO_REF"			, "문의번호"		, 120);
			grd헤드.SetCol("NO_PO_PARTNER"	, "주문번호"		, 120);
			grd헤드.SetCol("NM_EMP_SALE"		, "영업담당자"	, 70);
			grd헤드.SetCol("NM_EMP_TYPE"		, "입력담당자"	, 70);
			grd헤드.SetCol("NM_EMP_SLOG"		, "물류담당자"	, 70);

			grd헤드.Cols["CD_COMPANY"].TextAlign = TextAlignEnum.CenterCenter;
			grd헤드.Cols["NO_FILE"].TextAlign = TextAlignEnum.CenterCenter;
			grd헤드.Cols["NM_EMP_SALE"].TextAlign = TextAlignEnum.CenterCenter;
			grd헤드.Cols["NM_EMP_TYPE"].TextAlign = TextAlignEnum.CenterCenter;
			grd헤드.Cols["NM_EMP_SLOG"].TextAlign = TextAlignEnum.CenterCenter;

			grd헤드.SetDataMap("CD_COMPANY", GetDb.Company(), "CD_COMPANY", "NM_COMPANY");
			
			grd헤드.SetDefault("19.12.26.01", SumPositionEnum.None);
			grd헤드.Cols["NO_FILE_OLD"].Visible = false;

			// ---------------------------------------------------------------------------------------------------- Head
			grd라인.BeginSetting(1, 1, false);
			
			grd라인.SetCol("CD_COMPANY"	, "회사명"		, false);
			grd라인.SetCol("NO_FILE"		, "파일번호"		, false);
			grd라인.SetCol("TP_STEP"		, "유형"			, 85);
			grd라인.SetCol("DC_FILE"		, "파일명"		, 200);
			grd라인.SetCol("LN_VENDOR"	, "매입처"		, 150);
			grd라인.SetCol("NM_EMP"		, "등록자"		, 70);
			grd라인.SetCol("DTS_INSERT"	, "등록일"		, 140, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			grd라인.SetCol("DC_RMK"		, "비고"			, 200, true);

			grd라인.Cols["NM_EMP"].TextAlign = TextAlignEnum.CenterCenter;
			grd라인.Cols["DTS_INSERT"].Format = "####/##/## ##:##:##";

			grd라인.SetDataMap("TP_STEP", GetDb.Code("CZ_MA00005"), "CODE", "NAME");
			
			grd라인.SetDefault("20.09.02.01", SumPositionEnum.None);
		}

		protected override void InitPaint()
		{
			tbx파일번호.Focus();
		}

		#endregion

		#region ==================================================================================================== Event

		private void InitEvent()
		{
			btn맵스용저장.Click += Btn맵스용저장_Click;

			btn영업정보등록.Click += Btn영업정보등록_Click;
			btn첨부파일등록.Click += Btn첨부파일등록_Click;
			btn발송용저장.Click += Btn발송용저장_Click;
			btn파일변경.Click += Btn파일변경_Click;

			tbx파일번호.KeyDown += new KeyEventHandler(tbx검색_KeyDown);
			tbx키워드.KeyDown += new KeyEventHandler(tbx검색_KeyDown);
			cbm확장자.QueryBefore += Cbm확장자_QueryBefore;
		
			grd헤드.AfterRowChange += new RangeEventHandler(grd헤드_AfterRowChange);
			grd헤드.DoubleClick += new EventHandler(grd헤드_DoubleClick);
			grd라인.DoubleClick += new EventHandler(Grd라인_DoubleClick);
		}

		private void Btn맵스용저장_Click(object sender, EventArgs e)
		{			
			string query = @"
SELECT
	A.NM_FILE
,	A.DC_FILE
,	ISNULL(B.NM_FILE, '')	AS INQ_FILE
FROM	  V_CZ_MA_WORKFLOWL		AS A WITH(NOLOCK)
LEFT JOIN V_CZ_SA_QTN_PREREG	AS B WITH(NOLOCK) ON A.CD_COMPANY = B.CD_COMPANY AND A.NO_FILE = B.NO_FILE AND B.NO_LINE = 1 AND A.DC_FILE = B.NM_FILE
WHERE 1 = 1
	AND A.CD_COMPANY = '" + CompanyCode + @"'
	AND A.NO_FILE = '" + FileNumber + @"'
	AND A.TP_STEP = '01'";

			DataTable dt = SQL.GetDataTable(query);

			if (dt.Rows.Count > 0)
			{
				string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\" + grd헤드["NO_FILE"] + @"맵스용\";
				DIR.CreateDirectory(desktopPath);

				int i = 1;

				foreach (DataRow row in dt.Rows)
				{
					string fileName = FILE.DownloadWF(CompanyCode, FileNumber, row["NM_FILE"].ToStr(), false);
					string extension = FILE.GetExtension(fileName).ToLower();

					// MSG 파일의 경우는 PDF로 변환함
					if (extension == ".msg")
					{
						// 메세지 로드
						MapiMessage message = MapiMessage.FromFile(DIR.GetTempPath() + @"\" + fileName);

						// pdf로 변환
						fileName = FILE.GetFileNameWithoutExtension(fileName) + ".pdf";
						Html.ConvertPdf(DIR.GetTempPath() + @"\" + fileName, message.BodyHtml);
					}

					// ********** Inquiry 파일
					if (row["DC_FILE"].ToStr() == row["INQ_FILE"].ToStr())
					{
						File.Copy(DIR.GetTempPath() + @"\" + fileName, desktopPath + FileNumber + "-I" + FILE.GetExtension(fileName));
					}
					// ********** 기타 파일
					else
					{
						if (extension == ".msg")
						{
							File.Copy(DIR.GetTempPath() + @"\" + fileName, desktopPath + FileNumber + "-M" + FILE.GetExtension(fileName));
						}
						else
						{
							File.Copy(DIR.GetTempPath() + @"\" + fileName, desktopPath + FileNumber + "-R" + i + FILE.GetExtension(fileName));
							i++;
						}
					}
				}

				ShowMessage("바탕화면에 저장되었습니다.");
			}
			else
			{
				ShowMessage("저장할 파일이 없습니다.");
			}
		}

		private void Btn영업정보등록_Click(object sender, EventArgs e)
		{
			P_CZ_SA_USER_SUB subDlg = new P_CZ_SA_USER_SUB();
			subDlg.ShowDialog(this);
		}

		private void Btn첨부파일등록_Click(object sender, EventArgs e)
		{
			base.OnToolBarAddButtonClicked(sender, e);

			DataTable dt;
			string query, key;

			try
			{
				query = @"SELECT * 
                          FROM CZ_SA_USER WITH(NOLOCK)" + Environment.NewLine +
						 "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
						 "AND ID_USER = '" + Global.MainFrame.LoginInfo.UserID + "'";

				dt = Global.MainFrame.FillDataTable(query);

				if (dt.Rows.Count == 0)
				{
					this.ShowMessage("CZ_@ 이(가) 없습니다.", this.DD("영업정보"));
					return;
				}

				if (Global.MainFrame.LoginInfo.CompanyCode == D.GetString(this.grd헤드["CD_COMPANY"]))
					key = D.GetString(this.grd헤드["NO_FILE"]);
				else
					key = string.Empty;

				P_CZ_SA_INQ_REG subDlg = new P_CZ_SA_INQ_REG(key);
				subDlg.Show(this);

			}
			catch (Exception Ex)
			{
				MsgEnd(Ex);
			}
		}

		private void Btn발송용저장_Click(object sender, EventArgs e)
		{			
			DataTable dt = grd라인.DataTable.Select("CD_COMPANY = '" + grd헤드["CD_COMPANY"] + "' AND NO_FILE = '" + grd헤드["NO_FILE"] + "' AND TP_STEP = '58'").ToDataTable();

			if (dt != null)
			{
				string desktopPath = PATH.GetDesktopPath() + @"\" + FileNumber + @"견적발송용\";
				PATH.CreateDirectory(desktopPath);

				foreach (DataRow row in dt.Rows)
				{
					string fileDisp = row["DC_FILE"].ToString();
					string fileName = row["NM_FILE"].ToString();
					object fileData = row["FILE_DATA"];
					string downName = "";

					if (fileName != "")					// 워크플로우
					{ 
						if (fileName.IndexOf(@"\") > 0)
							downName = FILE.Download(fileName, false);
						else
							downName = FILE.Download(FileNumber, fileName, false);
					}
					else if (fileData.ToString() != "")	// SRM
					{
						downName = FILE.DownloadBinary(fileDisp, fileData, false);
					}

					string finName = downName.IndexOf("\\") > 0 ? downName : PATH.GetTempPath() + @"\" + downName;
					FILE.Copy(finName, desktopPath + fileDisp);
				}

				ShowMessage("바탕화면에 저장되었습니다.");
			}
			else
			{
				ShowMessage("저장할 파일이 없습니다.");
			}
		}

		private void Btn파일변경_Click(object sender, EventArgs e)
		{
			string companyCode = (string)grd헤드["CD_COMPANY"];
			string fileNumber = (string)grd헤드["NO_FILE"];
			string empName = (string)grd헤드["NM_EMP_SALE"];

			if (grd헤드["NO_FILE_OLD"].ToString() != "")
			{
				ShowMessage("이미 변경된 파일입니다.");
				return;
			}

			if (!LoginInfo.UserID.In("S-343") && (companyCode != LoginInfo.CompanyCode || empName != LoginInfo.EmployeeName))
			{
				ShowMessage("해당 영업담당자만 변경 가능합니다.");
				return;
			}

			H_CZ_FILE_CHANGE f = new H_CZ_FILE_CHANGE(fileNumber, empName);
			f.ShowDialog();

			if (f.IsChanged)
				OnToolBarSearchButtonClicked(null, null);
		}

		private void tbx검색_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
			{
				if (((TextBoxExt)sender).Text.Trim() == "")
				{
					ShowMessage("검색어를 입력하세요!");
				}
				else
				{
					//tbx포커스.Focus();
					OnToolBarSearchButtonClicked(null, null);
				}
			}
		}

		private void Cbm확장자_QueryBefore(object sender, BpQueryArgs e)
		{ 
			e.HelpParam.P00_CHILD_MODE = "확장자";

			e.HelpParam.P61_CODE1 = @"
	CD_SYSDEF	AS CODE
,	NM_SYSDEF	AS NAME";

			e.HelpParam.P62_CODE2 = @"
CZ_MA_CODEDTL WITH(NOLOCK)";

			e.HelpParam.P63_CODE3 = @"
WHERE 1 = 1
	AND CD_COMPANY = '" + "K100" + @"'
	AND CD_FIELD = 'CZ_MA00029'
	AND YN_USE = 'Y'
ORDER BY CD_SYSDEF";

			string a = e.HelpParam.ToString();
		}

		private void grd헤드_DoubleClick(object sender, EventArgs e)
		{
			if (!grd헤드.HasNormalRow || grd헤드.MouseCol <= 0)
				return;

			// 헤더클릭
			if (grd헤드.MouseRow < grd헤드.Rows.Fixed)
				return;

			// ***** 견적화면 열기
			// 로그인회사랑 다르면 오픈불가
			if (grd헤드["CD_COMPANY"].ToString() != LoginInfo.CompanyCode)
				return;

			// 선용이면 다른 ID 호출
			string query = @"
SELECT 1
FROM MA_CODEDTL WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + grd헤드["CD_COMPANY"] + @"'
	AND CD_FIELD = 'CZ_SA00023'
	AND CD_SYSDEF = '" + grd헤드["NO_FILE"].Left(2) + @"'
	AND CD_FLAG2 = 'GS'";

			string pageId;
			string pageName;

			if (DBMgr.GetDataTable(query).Rows.Count == 0)
			{
				pageId = "P_CZ_SA_QTN_REG";
				pageName = DD("견적등록");
			}
			else
			{
				pageId = "P_CZ_SA_QTN_REG_GS";
				pageName = DD("견적등록(선용)");
			}

			if (IsExistPage(pageId, false))
				UnLoadPage(pageId, false);

			LoadPageFrom(pageId, pageName, this.Grant, new object[] { grd헤드["NO_FILE"] });
		}

		private void Grd라인_DoubleClick(object sender, EventArgs e)
		{			
			// 헤더클릭
			if (grd라인.MouseRow < grd라인.Rows.Fixed)
			{
				SetGridStyle();
				return;
			}

			// 첨부파일 열기
			string colName = grd라인.Cols[grd라인.Col].Name;

			if (colName == "DC_FILE")
			{
				// 로그인 회사랑 다르면 오픈불가
				//if ()

				if (LoginInfo.CompanyCode != grd라인["CD_COMPANY"].ToString() && !(LoginInfo.CompanyCode == "S100" && grd라인["NO_FILE"].ToStr().Substring(0, 2) == "DS"))
					return;

				// ********** 파일 오픈
				string fileDisp = grd라인["DC_FILE"].ToString();
				string fileName = grd라인["NM_FILE"].ToString();
				object fileData = grd라인["FILE_DATA"];

				if (fileName != "")					// 워크플로우
				{
					if (fileName.IndexOf(@"\") > 0)
						FILE.Download(fileName, true);
					else
						FILE.Download(FileNumber, fileName, true);
				}
				else if (fileData.ToString() != "")	// SRM
				{
					FILE.DownloadBinary(fileDisp, fileData, true);
				}
			}
		}

		#endregion

		#region ==================================================================================================== Search

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			base.OnToolBarSearchButtonClicked(sender, e);

			if (ctx회사코드.CodeValue == "")
			{
				ShowMessage(공통메세지._은는필수입력항목입니다, DD("회사코드"));
				return;
			}

			DebugMode debug = Control.ModifierKeys == (Keys.Control | Keys.Alt) ? DebugMode.Popup : DebugMode.None;
			Util.ShowProgress(DD("조회중입니다."));
			tbx파일번호.Text = EngHanConverter.KorToEng(tbx파일번호.Text);
			tbx포커스.Focus();

			// 키워드 파라메타
			string keyPar = "@" + cbo키워드.GetValue();
			
			DBMgr dbm = new DBMgr();
			dbm.DebugMode = debug;
			dbm.Procedure = "PS_CZ_MA_WORKFLOW_FILE_REG_H_R2";
			dbm.AddParameter("@CD_COMPANY"	, ctx회사코드.CodeValue);
			dbm.AddParameter("@NO_FILE"		, tbx파일번호.Text);
			dbm.AddParameter("@CD_FILE"		, cbo파일번호.GetValue());
			dbm.AddParameter("@DT_F"		, dtp등록일자.StartDateToString);
			dbm.AddParameter("@DT_T"		, dtp등록일자.EndDateToString);

			dbm.AddParameter("@NO_EMP_SALE"	, ctx영업담당자.CodeValue);
			dbm.AddParameter("@NO_EMP_TYPE"	, ctx입력담당자.CodeValue);
			dbm.AddParameter("@CD_PARTNER"	, cbm매출처.QueryWhereIn_Pipe);
			dbm.AddParameter("@NO_IMO"		, cbm호선.QueryWhereIn_Pipe);			
			dbm.AddParameter("@CD_VENDOR"	, cbm매입처.QueryWhereIn_Pipe);

			dbm.AddParameter("@TP_STEP"		, cbo파일유형.GetValue());
			dbm.AddParameter(keyPar			, tbx키워드.Text);
			dbm.AddParameter("@CD_EXTENSION", cbm확장자.QueryWhereIn_Pipe);
			dbm.AddParameter("@YN_INCLUDED"	, chk첨부.Checked ? "Y" : "");
			dbm.AddParameter("@YN_LIMIT"	, chk제한.Checked ? "Y" : "");
			dbm.AddParameter("@YN_DXREG"	, chk자동생성.Checked ? "Y" : "");

			DataTable dt = dbm.GetDataTable();
			grd헤드.Binding = dt;

			Util.CloseProgress();
		}

		private void grd헤드_AfterRowChange(object sender, RangeEventArgs e)
		{
			base.OnToolBarSearchButtonClicked(sender, e);

			grd라인.Redraw = false;
			string companyCode = grd헤드["CD_COMPANY"].ToString();
			string fileNumber = grd헤드["NO_FILE"].ToString();
			DataTable dt = null;

			if (grd헤드.DetailQueryNeed)
			{
				DBMgr dbm = new DBMgr();
				dbm.DebugMode = DebugMode.None;
				dbm.Procedure = "PS_CZ_MA_WORKFLOW_FILE_REG_L_R3";
				dbm.AddParameter("@CD_COMPANY"	, companyCode);
				dbm.AddParameter("@NO_FILE"		, fileNumber);
				
				dbm.AddParameter("@CD_VENDOR"	, chk매입처.Checked ? cbm매입처.QueryWhereIn_Pipe : "");
				dbm.AddParameter("@TP_STEP"		, cbo파일유형.GetValue());
				dbm.AddParameter("@CD_EXTENSION", cbm확장자.QueryWhereIn_Pipe);
				dbm.AddParameter("@YN_INCLUDED"	, chk첨부.Checked ? "Y" : "");
				dbm.AddParameter("@YN_RECENT"	, chk최신.Checked ? "Y" : "");
				dt = dbm.GetDataTable();
			}
	
			grd라인.BindingAdd(dt, "CD_COMPANY = '" + companyCode + "' AND NO_FILE = '" + fileNumber + "'");
			SetGridStyle();
			grd라인.Redraw = true;
		}

		#endregion

		#region ==================================================================================================== Add

		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
			Btn첨부파일등록_Click(null, null);
		}

		#endregion

		#region ==================================================================================================== Delete

		public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{
			if (ShowMessage(공통메세지.자료를삭제하시겠습니까) == DialogResult.Yes)
			{
				string query = @"
DELETE FROM CZ_MA_WORKFLOWH	WHERE CD_COMPANY = @CD_COMPANY AND NO_KEY = @NO_FILE
DELETE FROM CZ_MA_WORKFLOWL	WHERE CD_COMPANY = @CD_COMPANY AND NO_KEY = @NO_FILE
DELETE FROM CZ_SA_QTNH		WHERE CD_COMPANY = @CD_COMPANY AND NO_FILE = @NO_FILE
DELETE FROM CZ_SA_QTNL		WHERE CD_COMPANY = @CD_COMPANY AND NO_FILE = @NO_FILE
DELETE FROM CZ_PU_QTNH		WHERE CD_COMPANY = @CD_COMPANY AND NO_FILE = @NO_FILE
DELETE FROM CZ_PU_QTNL		WHERE CD_COMPANY = @CD_COMPANY AND NO_FILE = @NO_FILE";

				DBMgr dbm = new DBMgr
				{
					Query = query
				};
				dbm.AddParameter("@CD_COMPANY"	, grd헤드["CD_COMPANY"]);
				dbm.AddParameter("@NO_FILE"		, grd헤드["NO_FILE"]);
				dbm.ExecuteNonQuery();

				ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
			}
		}

		#endregion

		#region ==================================================================================================== Save

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			// 그리드 검사
			if (!base.Verify())
				return;

			// 저장
			DBMgr.ExecuteNonQuery("PX_CZ_MA_WORKFLOW_FILE_REG", DebugMode.Print, GetTo.Xml(grd라인.GetChanges()));
			grd라인.AcceptChanges();
			ShowMessage(PageResultMode.SaveGood);
		}

		#endregion

		#region ==================================================================================================== 기타

		public void SetGridStyle()
		{
			// 첨부파일 아이콘 이미지 추가
			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
			{
				string fileName = grd라인[i, "DC_FILE"].ToString();
				Image icon = Icons.GetExtensionForCell(fileName.Substring(fileName.LastIndexOf(".") + 1));
				grd라인.SetCellImage(i, grd라인.Cols["DC_FILE"].Index, icon);

				// 병합
				grd라인.Merge("TP_STEP", "TP_STEP", "DC_RMK");
			}
		}

		#endregion
	}
}


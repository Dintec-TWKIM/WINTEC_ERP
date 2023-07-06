using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using System.Linq;

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
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;


namespace cz
{	
	public partial class P_CZ_MA_SEAWEB_REG : PageBase
	{
		string _serverPath;

		string NO_EMP = Global.MainFrame.LoginInfo.EmployeeNo.ToString();

		#region ==================================================================================================== Constructor
		public P_CZ_MA_SEAWEB_REG()
		{
			InitializeComponent();
			ControlExt.SetTextBoxDefault(this);
			StartUp.Certify(this);
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
			_serverPath = "Upload/" + PageID + "/";

			tbxIMO번호.Editable(false);
			tbx호출부호.Editable(false);
			tbx상태.Editable(false);

			tbx호선번호2.Editable(false);
			tbx인도일자2.Editable(false);
			tbx선명2.Editable(false);
			tbx유형2.Editable(false);
			tbx조선소2.Editable(false);
			tbx운항선사2.Editable(false);
			tbx선급2.Editable(false);


			MainGrids = new FlexGrid[] { fgd딘텍 };
		}

		protected override void InitPaint()
		{
			if (!LoginInfo.EmployeeNo.In("S-343", "S-495"))
			{
				//btn로그인.Visible = false;
				btn폴더파싱.Visible = false;
				btn자동등록.Visible = false;
			}

			btn로그인.Visible = true;
			spc딘텍.SplitterDistance = spc딘텍.Width - 550;   // 35 + 170 + 16
		}

		#endregion

		#region ==================================================================================================== Grid

		private void InitGrid()
		{
			// ********** 시웹
			fgd딘텍.BeginSetting(1, 1, false);
			   
			fgd딘텍.SetCol("NO_IMO"			, "IMO번호"			, 70);
			// 딘텍
			fgd딘텍.SetCol("NO_HULL"			, "호선번호"			, 120);
			fgd딘텍.SetCol("NM_VESSEL"		, "선명"				, 200);
			fgd딘텍.SetCol("NM_TP_SHIP"		, "유형"				, 190);
			fgd딘텍.SetCol("DC_SHIPBUILDER"	, "조선소"			, 300);
			fgd딘텍.SetCol("LN_PARTNER"		, "운항선사"			, 200);
			fgd딘텍.SetCol("NM_CERT"			, "선급"				, 150);
			fgd딘텍.SetCol("DT_SHIP_DLV"		, "인도일자"			, 70);
			// 시웹
			fgd딘텍.SetCol("HULL_NO"			, "호선번호(시웹)"	, false);
			fgd딘텍.SetCol("SHIP_NAME"		, "선명(시웹)"		, false);
			fgd딘텍.SetCol("SHIPTYPE"		, "유형(시웹)"		, false);
			fgd딘텍.SetCol("SHIPBUILDER"		, "조선소(시웹)"		, false);
			fgd딘텍.SetCol("TECH_MANAGER"	, "운항선사(시웹)"	, false);
			fgd딘텍.SetCol("CLASS"			, "선급(시웹)"		, false);
			fgd딘텍.SetCol("DELIVERY"		, "인도일자(시웹)"	, false);
			// 공통
			fgd딘텍.SetCol("CALL_SIGN"		, "호출부호"			, 70);
			fgd딘텍.SetCol("STATUS"			, "상태"				, 200);
			fgd딘텍.SetCol("FILE_ICON"		, "첨부"				, 40);
			fgd딘텍.SetCol("HTML_FILE_ORG"	, "첨부파일(O)"		, false);
			fgd딘텍.SetCol("HTML_FILE_LOC"	, "첨부파일(L)"		, false);

			fgd딘텍.Cols["NO_IMO"].TextAlign = TextAlignEnum.CenterCenter;
			fgd딘텍.Cols["FILE_ICON"].ImageAlign = ImageAlignEnum.CenterCenter;
			fgd딘텍.Cols["DT_SHIP_DLV"].Format = "####/##";

			fgd딘텍.VerifyNotNull = new string[] { "NO_HULL", "NM_VESSEL", "NM_TP_SHIP" };
			fgd딘텍.VerifyPrimaryKey = new string[] { "NO_IMO" };
			fgd딘텍.SetOneGridBinding(new object[] {}, one호선, one딘텍, one시웹);
			fgd딘텍.SetDefault("20.02.20.01", SumPositionEnum.None);

			// ********** 시웹
			fgd시웹.BeginSetting(1, 1, false);

			fgd시웹.SetCol("LRNO"			, "IMO/LR No."			, 70);
			fgd시웹.SetCol("HULL_NO"			, "Yard/hull No."		, 120);
			fgd시웹.SetCol("SHIP_NAME"		, "Ship Name"			, 200);
			fgd시웹.SetCol("SHIPTYPE"		, "Shiptype"			, 190);
			fgd시웹.SetCol("SHIPBUILDER"		, "Ship Builder"		, 300);
			fgd시웹.SetCol("TECH_MANAGER"	, "Technical Manager"	, 200);
			fgd시웹.SetCol("CLASS"			, "Class"				, 200);
			fgd시웹.SetCol("DELIVERY"		, "Delivery"			, 70);
			fgd시웹.SetCol("CALL_SIGN"		, "Call Sign"			, 70);			
			fgd시웹.SetCol("GROSS"			, "Gross"				, 70	, false	, typeof(decimal), FormatTpType.QUANTITY);
			fgd시웹.SetCol("DEADWEIGHT"		, "Deadweight"			, 70	, false	, typeof(decimal), FormatTpType.QUANTITY);
			fgd시웹.SetCol("STATUS"			, "Status"				, 200);
			fgd시웹.SetCol("FILE_ICON"		, "첨부"					, 40);
			fgd시웹.SetCol("HTML_FILE_ORG"	, "첨부파일(O)"			, false);
			fgd시웹.SetCol("HTML_FILE_LOC"	, "첨부파일(L)"			, false);
			
			fgd시웹.Cols["LRNO"].TextAlign = TextAlignEnum.CenterCenter;
			fgd시웹.Cols["FILE_ICON"].ImageAlign = ImageAlignEnum.CenterCenter;

			fgd시웹.SetDefault("20.02.19.01", SumPositionEnum.None);
		}

		#endregion

		#region ==================================================================================================== Event

		private void InitEvent()
		{
			btn로그인.Click += Btn로그인_Click;
			btn폴더파싱.Click += Btn폴더파싱_Click;
			btn자동등록.Click += Btn자동등록_Click;
			btn동기화.Click += Btn동기화_Click;			

			// 검색
			tbxIMO번호검색.KeyDown += TbxIMO번호검색_KeyDown;

			// 원그리드 관련
			tbxIMO번호.Leave += TbxIMO번호_Leave;
			tbx인도일자.Leave += Tbx인도일자_Leave;

			cbx유형.QueryBefore += Cbx유형_QueryBefore;

			tbx호선번호.Enter += Tbx호선번호_Enter;
			tbx호선번호.Leave += Tbx호선번호_Enter;

			tbx인도일자.Enter += Tbx호선번호_Enter;
			tbx인도일자.Leave += Tbx호선번호_Enter;

			tbx선명.Enter += Tbx호선번호_Enter;
			tbx선명.Leave += Tbx호선번호_Enter;

			tbx조선소.Enter += Tbx호선번호_Enter;
			tbx조선소.Leave += Tbx호선번호_Enter;

			tbx선급.Enter += Tbx호선번호_Enter;
			tbx선급.Leave += Tbx호선번호_Enter;

			tbx비고.Enter += Tbx호선번호_Enter;
			tbx비고.Leave += Tbx호선번호_Enter;

			cbx유형.Enter += Ctb유형_Enter;
			cbx유형.Leave += Ctb유형_Leave;						

			cbx운항선사.Enter += Ctb유형_Enter;
			cbx운항선사.Leave += Ctb유형_Leave;

			btn호선번호.Click += Btn호선번호_Click;
			btn인도일자.Click += Btn인도일자_Click;
			btn선명.Click += Btn선명_Click;
			btn유형.Click += Btn유형_Click;
			btn조선소.Click += Btn조선소_Click;
			btn운항선사.Click += Btn운항선사_Click;
			btn선급.Click += Btn선급_Click;
			btn콜사인.Click += Btn콜사인_Click;

			// 그리드
			fgd딘텍.AfterRowChange += Fgd딘텍_AfterRowChange;
			fgd딘텍.DoubleClick += Fgd딘텍_DoubleClick;
			fgd시웹.DoubleClick += Fgd딘텍_DoubleClick;
		}

	
		private void Btn로그인_Click(object sender, EventArgs e)
		{
			LogIn();
		}

		private void Btn폴더파싱_Click(object sender, EventArgs e)
		{			
			int cnt = 1;
			FolderBrowserDialog f = new FolderBrowserDialog();

			if (f.ShowDialog() == DialogResult.OK)
			{
				Util.ShowProgress("시작");

				DirectoryInfo dir = new DirectoryInfo(f.SelectedPath);

				foreach (FileInfo file in dir.GetFiles())
				{
					if (file.Extension.ToUpper() == ".HTM" || file.Extension.ToUpper() == ".HTML")
					{
						//textBox1.Text += "\r\n" + file.Name;
						string imoNumber = Path.GetFileNameWithoutExtension(file.Name);

						// 로컬화
						string htmlFileLoc = LocalizeShipHtml(file.FullName, @"d:\SeaWeb_Loc\" + imoNumber + "_loc.html");

						// 파싱
						DataSet ds = ParseShipHtml(file.FullName);

						// 담았으면 이제 테이블에 집에 넣기
						//DBMgr.GetDataTable("PX_CZ_MA_SEAWEB", DebugMode.None, imoNumber, GetTo.Xml(ds.Tables[0]), GetTo.Xml(ds.Tables[1]));
						cnt++;

						File.Move(file.FullName, f.SelectedPath + @"\완료\" + file.Name);
					}

					if (cnt == 1)
						break;
				}

				Util.CloseProgress();
				ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
			}
		}

		private void Btn자동등록_Click(object sender, EventArgs e)
		{		
			if (!LogIn())
				throw new Exception("로그인 실패!!");

			for (int i = fgd딘텍.Rows.Fixed; i < fgd딘텍.Rows.Count; i++)
			{
				string imoNumber = (string)fgd딘텍[i, "NO_IMO"];

				// 호선페이지로 이동
				Util.ShowProgress("작업중 : " + i + "/" + fgd딘텍.Rows.Count);
				string url = "https://maritime.ihs.com/Areas/Seaweb/authenticated/authenticated_handler.aspx?control=shipovw&LRNO=" + imoNumber;
				web시웹.GoTo(url);

				if (CheckShipExistence())
				{
					// 원본 파일 다운르도
					string htmlFile = DownloadShipHtml(imoNumber + ".html");
					FileMgr.Upload(htmlFile, _serverPath + "Original");

					// html 파일 로컬화
					string htmlFileLoc = LocalizeShipHtml(htmlFile, imoNumber + "_loc.html");
					htmlFileLoc = FileMgr.Upload(htmlFileLoc, _serverPath + "Localize");

					// 파싱
					DataSet ds = ParseShipHtml(htmlFile);
					ds.Tables[0].Columns.Add("LRNO");
					ds.Tables[0].Columns.Add("HTML_FILE_ORG");
					ds.Tables[0].Columns.Add("HTML_FILE_LOC");
					ds.Tables[0].Columns.Add("ID_USER");
					ds.Tables[0].Rows[0]["LRNO"] = imoNumber;
					ds.Tables[0].Rows[0]["HTML_FILE_ORG"] = htmlFile;
					ds.Tables[0].Rows[0]["HTML_FILE_LOC"] = htmlFileLoc;
					ds.Tables[0].Rows[0]["ID_USER"] = "SYSADMIN";

					ds.Tables[0].AcceptChanges();
					ds.Tables[0].Rows[0].SetModified();

					// 담았으면 이제 테이블에 집에 넣기
					SQL.GetDataTable("PX_CZ_MA_SEAWEB_REG_SEA", DebugMode.None, GetTo.Xml(ds.Tables[0]), GetTo.Xml(ds.Tables[1]));

					//break;
				}
			}

			LogOut();
			Util.CloseProgress();
			ShowMessage(공통메세지.자료가정상적으로저장되었습니다);			
		}

		private void Btn동기화_Click(object sender, EventArgs e)
		{
			string imoNumber = tbxIMO번호.Text.Trim();

			
			if (imoNumber == "")
			{
				ShowMessage("IMO번호를 입력하세요.");
				return;
			}

			//if(!string.IsNullOrEmpty(tbx호선번호.Text))
			//{
			//	ShowMessage("이미 등록된 호선 입니다.");
			//	return;
			//}

			string query = @"
INSERT INTO CZ_RPA_WORK_QUEUE
( 
	CD_COMPANY, 
	CD_RPA, 
	URGENT,
	NO_FILE, 
	CD_PARTNER, 
	DELAY_MIN,
	YN_READ, 
	YN_DONE, 
	NO_BOTS, 
	ID_INSERT, 
	DTS_INSERT
)
VALUES
(
	@CD_COMPANY, 
	@CD_RPA, 
	'5',
	@NO_FILE, 
	@CD_PARTNER, 
	'0',
	'N', 
	'N', 
	'1',
	'SYSADMIN',
	NEOE.SF_SYSDATE(GETDATE())
)";

			DBMgr dbm = new DBMgr();
			dbm.DebugMode = DebugMode.Print;
			dbm.Query = query;
			dbm.AddParameter("@CD_COMPANY", "K100");
			dbm.AddParameter("@CD_RPA", "SEAWEB_SYNC");
			dbm.AddParameter("@NO_FILE", imoNumber);
			dbm.AddParameter("@CD_PARTNER", NO_EMP);

			dbm.ExecuteNonQuery();


			ShowMessage("동기화 작업을 요청하였습니다.\r\n※동기화 작업 순서\r\n1.RPA 작업 완료 후 쪽지 발송\r\n2.ERP > SEA-WEB 조회\r\n3.데이터 확인 후 ↑ 버튼 사용하여 데이터 이동\r\n4. 저장");



			//pnlIMO번호.Editable(false);
			//string htmlFile = FileMgr.GetTempPath() + imoNumber + ".html";

			//// 로컬에 있는지 검사하고 없는 경우 다운로드
			//if (!File.Exists(htmlFile))
			//{
			//	htmlFile = "";

			//	try
			//	{
			//		Util.ShowProgress(DD("SEA-WEB 로그인 중입니다."));

			//		// 로그인
			//		if (!LogIn())
			//			throw new Exception("로그인 실패!!");

			//		// 호선페이지로 이동
			//		Util.ShowProgress(DD("해당 호선을 조회 중입니다."));
			//		string url = "https://maritime.ihs.com/Areas/Seaweb/authenticated/authenticated_handler.aspx?control=shipovw&LRNO=" + imoNumber;
			//		web시웹.GoTo(url);

			//		if (CheckShipExistence())
			//		{
			//			// 원본 파일 다운르도
			//			htmlFile = DownloadShipHtml(imoNumber + ".html");
			//			FileMgr.Upload(htmlFile, _serverPath + "Original");
			//		}
			//		else
			//			throw new Exception("호선이 없습니다.");
			//	}
			//	catch (Exception ex)
			//	{
			//		Util.CloseProgress();
			//		ShowMessage(ex.Message);
			//	}

			//	// 로그아웃
			//	Util.ShowProgress(DD("SEA-WEB 로그아웃 중입니다."));
			//	LogOut();
			//	Util.CloseProgress();
			//	if (htmlFile == "")
			//		return;
			//}

			//// html 파일 로컬화
			//string htmlFileLoc = LocalizeShipHtml(htmlFile, imoNumber + "_loc.html");
			//htmlFileLoc = FileMgr.Upload(htmlFileLoc, _serverPath + "Localize");

			//// html 파일 바인딩
			//fgd딘텍["HTML_FILE_ORG"] = Path.GetFileName(htmlFile);
			//fgd딘텍["HTML_FILE_LOC"] = Path.GetFileName(htmlFileLoc);
			//fgd딘텍.SetCellImage(fgd딘텍.Row, fgd딘텍.Cols["FILE_ICON"].Index, Icons.GetExtension(Path.GetExtension(htmlFileLoc).Replace(".", "")));

			//// 파싱
			//DataSet ds = ParseShipHtml(htmlFile);
			//DataRow rowShip = ds.Tables[0].Rows[0];

			//// 바인딩 - 공통	
			//tbx호출부호.Text = (string)rowShip["CALL_SIGN"];
			//tbx상태.Text = (string)rowShip["STATUS"];

			//// 바인딩 - 시웹
			//tbx호선번호2.Text = (string)rowShip["HULL_NO"];
			//tbx인도일자2.Text = string.Format("{0:####/##}", rowShip["DELIVERY"]);
			//tbx선명2.Text = (string)rowShip["SHIP_NAME"];
			//tbx유형2.Text = (string)rowShip["SHIPTYPE"];
			//tbx조선소2.Text = (string)rowShip["SHIPBUILDER"];
			//tbx운항선사2.Text = (string)rowShip["TECH_MANAGER"];
			//tbx선급2.Text = (string)rowShip["CLASS"];

			//// 불필요한 기타정보 - 혹시나 해서 저장해둠
			//fgd딘텍["GROSS"] = GetTo.Int(rowShip["GROSS"]);
			//fgd딘텍["DEADWEIGHT"] = GetTo.Int(rowShip["DEADWEIGHT"]);
			//fgd딘텍["YEAR_OF_BUILD"] = (string)rowShip["YEAR_OF_BUILD"];
			//fgd딘텍["SHIPBUILDER_CODE"] = (string)rowShip["SHIPBUILDER_CODE"];
			//fgd딘텍["MMSI_NO"] = (string)rowShip["MMSI_NO"];
			//fgd딘텍["FLAG"] = (string)rowShip["FLAG"];
			//fgd딘텍["OPERATOR"] = (string)rowShip["OPERATOR"];

			//			ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
		}

		private void TbxIMO번호검색_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
			{
				if (((TextBoxExt)sender).Text.Trim() == "")
					ShowMessage("검색어를 입력하세요!");
				else
					OnToolBarSearchButtonClicked(null, null);
			}
		}

		private void TbxIMO번호_Leave(object sender, EventArgs e)
		{
			tbxIMO번호.Text = tbxIMO번호.Text.Trim();

			if (tbxIMO번호.ReadOnly)
				return;

			if (tbxIMO번호.Text == "")
				return;

			//if (!CheckImoFormat(tbxIMO번호.Text))
			//{
			//	ShowMessage("@ 입력 형식이 올바르지 않습니다.", lblIMO번호.Text);
			//	tbxIMO번호.Text = "";
			//}

			//if (!CheckImoDuplication(tbxIMO번호.Text))
			//{
			//	ShowMessage("CZ_@ 이(가) 중복되었습니다.", tbxIMO번호.Text);
			//	tbxIMO번호.Text = "";
			//}
		}

		private void Tbx인도일자_Leave(object sender, EventArgs e)
		{
			tbx인도일자.Text = tbx인도일자.Text.Trim().Replace("-", "/");

			if (tbx인도일자.Text == "")
				return;

			if (tbx인도일자.Text.Length == 6)
				tbx인도일자.Text = tbx인도일자.Text.Left(4) + "/" + tbx인도일자.Text.Right(2);

			//if (!CheckYearMonthFormat(tbx인도일자.Text))
			//{
			//	ShowMessage("@ 입력 형식이 올바르지 않습니다.", lbl인도일자.Text);
			//	tbx인도일자.Text = "";
			//}
		}

		private void Cbx유형_QueryBefore(object sender, BpQueryArgs e)
		{
			e.HelpParam.P41_CD_FIELD1 = "CZ_MA00002";
		}

		private void Tbx호선번호_Enter(object sender, EventArgs e)
		{
			string name = ((TextBoxExt)sender).Name + "2";
			((TextBoxExt)Controls.Find(name, true)[0]).BorderColor = ((TextBoxExt)sender).BorderColor;
		}

		private void Ctb유형_Enter(object sender, EventArgs e)
		{
			string name = "tbx" + ((BpCodeTextBox)sender).Name.Substring(3) + "2";
			((TextBoxExt)Controls.Find(name, true)[0]).BorderColor = Constant.FocusedBorderColor;
		}

		private void Ctb유형_Leave(object sender, EventArgs e)
		{
			string name = "tbx" + ((BpCodeTextBox)sender).Name.Substring(3) + "2";
			((TextBoxExt)Controls.Find(name, true)[0]).BorderColor = Constant.UnfocusedBorderColor;
		}
		
		private void Btn호선번호_Click(object sender, EventArgs e)
		{
			tbx호선번호.Text = tbx호선번호2.Text;
		}

		private void Btn인도일자_Click(object sender, EventArgs e)
		{
			tbx인도일자.Text = tbx인도일자2.Text;
		}

		private void Btn선명_Click(object sender, EventArgs e)
		{
			tbx선명.Text = tbx선명2.Text;
		}



		private void Btn콜사인_Click(object sender, EventArgs e)
		{
			tbx콜사인.Text = tbx콜사인2.Text;
		}




		private void Btn유형_Click(object sender, EventArgs e)
		{
			string query = @"
SELECT TOP 1
	CD_SYSDEF	AS CODE
,	NM_SYSDEF	AS NAME
FROM V_CZ_MA_CODEDTL WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = 'K100'
	AND CD_FIELD = 'CZ_MA00002'
	AND (NM_SYSDEF LIKE '%' + @SEARCH + '%' OR @SEARCH LIKE '%' + NM_SYSDEF + '%')";

			DBMgr dbm = new DBMgr(query, QueryType.Text, DebugMode.Print);
			dbm.AddParameter("@SEARCH", tbx유형2.Text);
			DataTable dt = dbm.GetDataTable();

			if (dt.Rows.Count == 1)
			{
				cbx유형.CodeValue = (string)dt.Rows[0]["CODE"];
				cbx유형.CodeName = (string)dt.Rows[0]["NAME"];

				fgd딘텍["TP_SHIP"] = (string)dt.Rows[0]["CODE"];
				fgd딘텍["NM_TP_SHIP"] = (string)dt.Rows[0]["NAME"];
			}
			//else
			//	//ShowMessage("일치하는 항목이 없습니다.");
		}

		private void Btn조선소_Click(object sender, EventArgs e)
		{
			tbx조선소.Text = tbx조선소2.Text;
		}

		private void Btn운항선사_Click(object sender, EventArgs e)
		{
			string query = @"
SELECT TOP 1
	CD_PARTNER	AS CODE
,	LN_PARTNER	AS NAME
FROM MA_PARTNER WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + LoginInfo.CompanyCode +  @"'
	AND FG_PARTNER IN ('100', '110', '300')	-- 100:선주사, 110:대리점, 300:선박관리사
	AND (LN_PARTNER LIKE '%' + @SEARCH + '%' OR @SEARCH LIKE '%' + LN_PARTNER + '%')";

			DBMgr dbm = new DBMgr(query, QueryType.Text, DebugMode.Print);
			dbm.AddParameter("@SEARCH", tbx운항선사2.Text);
			DataTable dt = dbm.GetDataTable();

			if (dt.Rows.Count == 1)
			{
				cbx운항선사.CodeValue = (string)dt.Rows[0]["CODE"];
				cbx운항선사.CodeName = (string)dt.Rows[0]["NAME"];
			}
			//else
			//	ShowMessage("일치하는 항목이 없습니다.");
		}		

		private void Btn선급_Click(object sender, EventArgs e)
		{
			tbx선급.Text = tbx선급2.Text;
		}

		private void Fgd딘텍_AfterRowChange(object sender, RangeEventArgs e)
		{
			if (fgd딘텍.RowState() == DataRowState.Added)
				tbxIMO번호.Editable(true);
			else
				tbxIMO번호.Editable(false);
		}

		private void Fgd딘텍_DoubleClick(object sender, EventArgs e)
		{
			FlexGrid flexGrid = (FlexGrid)sender;

			// 헤더클릭
			if (flexGrid.MouseRow < flexGrid.Rows.Fixed)
			{
				SetGridStyle(flexGrid);
				return;
			}

			// 첨부파일 보기
			if (flexGrid.Cols[flexGrid.Col].Name == "FILE_ICON")
			{
				if (flexGrid["HTML_FILE_LOC"].ToString() == "")
					return;

				FileMgr.Download(_serverPath + "Localize/" + flexGrid["HTML_FILE_LOC"], true);
			}
		}

		#endregion

		#region ==================================================================================================== Search

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			base.OnToolBarSearchButtonClicked(sender, e);
			DebugMode debug = ModifierKeys == (Keys.Control | Keys.Alt) ? DebugMode.Popup : DebugMode.None;
			Util.ShowProgress(DD("조회중입니다."));

			tbx포커스.Focus();
			tbxIMO번호검색.Text = EngHanConverter.KorToEng(tbxIMO번호검색.Text);
			tbxIMO번호.Editable(false);

			if (tab메인.SelectedTab == tab딘텍)
			{
				one호선.ClearAndDefault(false);
				one딘텍.ClearAndDefault(false);
				one시웹.ClearAndDefault(false);

				DBMgr dbm = new DBMgr("PS_CZ_MA_SEAWEB_REG_DIN", QueryType.Procedure, debug);
				dbm.AddParameter("@CD_COMPANY", LoginInfo.CompanyCode);
				dbm.AddParameter("@NO_IMO", tbxIMO번호검색.Text);
				dbm.AddParameter("@NO_HULL", tbx호선번호검색.Text);
				dbm.AddParameter("@NM_VESSEL", tbx선명검색.Text);
				dbm.AddParameter("@DC_SHIPBUILDER", tbx조선소검색.Text);

				DataTable dt = dbm.GetDataTable();
				fgd딘텍.Binding = dt;
				SetGridStyle(fgd딘텍);
				Util.CloseProgress();

				if (!fgd딘텍.HasNormalRow)
				{
					ShowMessage("조회 데이터가 없습니다.\r\n동기화 요청 버튼을 이용하여 호선을 등록 하세요.");
					tbxIMO번호.Text = tbxIMO번호검색.Text;
				}
			}
			else
			{
				DBMgr dbm = new DBMgr("PS_CZ_MA_SEAWEB_REG_SEA", QueryType.Procedure, debug);
				dbm.AddParameter("@LRNO"			, tbxIMO번호검색.Text);
				dbm.AddParameter("@HULL_NO"			, tbx호선번호검색.Text);
				dbm.AddParameter("@SHIP_NAME"		, tbx선명검색.Text);
				dbm.AddParameter("@SHIPBUILDER"		, tbx조선소검색.Text);

				DataTable dt = dbm.GetDataTable();
				fgd시웹.Binding = dt;
				SetGridStyle(fgd시웹);
				Util.CloseProgress();

				if (!fgd시웹.HasNormalRow)								
					ShowMessage(공통메세지.조건에해당하는내용이없습니다);				
			}			
		}

		#endregion

		#region ==================================================================================================== Add

		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
			base.OnToolBarAddButtonClicked(sender, e);

			fgd딘텍.Rows.Add();
			fgd딘텍.Row = fgd딘텍.Rows.Count - 1;
			fgd딘텍.AddFinished();

			pnlIMO번호.Editable(true);
		}

		#endregion

		#region ==================================================================================================== Save

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			base.OnToolBarSaveButtonClicked(sender, e);
			DebugMode debugMode = ModifierKeys == (Keys.Control | Keys.Alt) ? DebugMode.Popup : DebugMode.None;

			// 그리드 검사
			if (!base.Verify())
				return;

			// 저장
			DataTable dtShip = fgd딘텍.GetChanges();	
			string shipXml = GetTo.Xml(dtShip);
			//SQL.GetDataTable("PX_CZ_MA_SEAWEB_REG", shipXml);
			SQL.GetDataTable("PX_CZ_MA_SEAWEB_REG2", shipXml);

			fgd딘텍.AcceptChanges();
			ShowMessage(PageResultMode.SaveGood);
		}

		#endregion

		#region ==================================================================================================== Parse

		private bool LogIn()
		{
			// 쿠키추가 (시웹 정보 선택 체크박스)
			string value = "ownerregovw~ship_ownership~ship_commercial_history~reg~vettingplaceholder~eventmatrix~class~surveysdue~crewlist~inspection~smc~certificates~consovw~constructionoverview~alterationsconversions~arrangement~consdetail~shipdisposal~serviceconstraints~shipbuilder~sisters~status~shipsupplementaryfeatures~dimensionsovw~dimensions~tonnages~cargo~cargooverview~capacities~gear~compartments~hatches~lanes_ramps_doors~specialist~tanks~towage~machine~prime_mover~auxengs~aux_generators~boilers~bunkers~primemovergrouped~thrusters~movementplaceholder~aismovementsummary~shippositionbingiframe~berthcalls~aismovements~aismovementscalendar~aismovementsheatmap~stspairs~timeline~photos~mynotes_ships~linkovw~equasis~inmarsat~fixtures~benchmark_ships~";
			DxWebBrowser.InternetSetCookie("https://maritime.ihs.com/Areas/Seaweb/authenticated", "shipovw", value);

			// 로그인 페이지로 이동
			string url = "https://maritime.ihs.com/Account2/Index";
			web시웹.GoTo(url);

			// 로그인 화면인지 체크 후 로그인
			string html = web시웹.GetHtml();
						
			if (html.IndexOf("Customer Login") > 0)
			{
				web시웹.SetValue("Username", "service@dintec.co.kr");
				web시웹.SetValue("Password", "dintec5771$$");
				//web시웹.SetValue("Username", "tsd@dintec.co.kr");
				//web시웹.SetValue("Password", "dintec2018@@");
				//web시웹.SetValue("Username", "smd@dintec.co.kr");
				//web시웹.SetValue("Password", "dintec2018@");
				web시웹.Click("btnLogInIdx");
			}

			// 로그인 되었는지 체크
			html = web시웹.GetHtml();

			if (html.IndexOf("Customer Login") > 0)
				return false;
			else
				return true;
		}

		private bool LogOut()
		{
			web시웹.GoTo("https://maritime.ihs.com/Account2/LogOff");
			return true;
		}

		private bool CheckShipExistence()
		{
			string html = web시웹.GetHtml();

			if (html.IndexOf("Ship Detail") > 0 && html.IndexOf("Ship Name") > 0)
				return true;
			else
				return false;
		}

		private string DownloadShipHtml(string fileName)
		{			
			return Html.MakeFile(fileName, web시웹.GetHtml());
		}

		private string LocalizeShipHtml(string sourceFile, string targetFile)
		{
			string sourceHtml = File.ReadAllText(sourceFile);
			string newHtml = @"
<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>

<html xmlns='http://www.w3.org/1999/xhtml'>
<head>
	<meta http-equiv='X-UA-Compatible' content='IE=edge' />
	<TITLE>Ship Detail - Sea-web</TITLE>
	<LINK href='https://maritime.ihs.com/Bundles/seawebCSS?v=0-KEPg9lqtPHDT17kV5rDc4lNn4GAyOZ2mnfBTI7vyQ1' rel='stylesheet'>
	<LINK href='https://maritime.ihs.com/Bundles/list_gridviewCSS?v=0Ga3ozDh3_umH6HZI6nwM2Gc6rLdFk4iVuqhxPvc6F41' rel='stylesheet'>
	<LINK href='https://maritime.ihs.com/Content/genericControlStyles?v=QKZBS3CYWoaF3F4JHVjnfp0c3dehtPHe1fNpxIXodr41' rel='stylesheet'>
	<LINK href='https://maritime.ihs.com/bundles/toastrCSS?v=xM2k-XzbntCIBvtujkSstfCtB_D9UPJK-crl3PRV_PA1' rel='stylesheet'>
</head>
<body>
<form>

{0}

</form>
</body>
</html>";

			HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
			doc.LoadHtml(sourceHtml);

			string divHtml = doc.GetElementbyId("rightcontentLayer").OuterHtml;
			divHtml = divHtml.Replace("view/--/ShowImage.ashx", "https://maritime.ihs.com/Areas/Seaweb/authenticated/view/--/ShowImage.ashx");

			return Html.MakeFile(targetFile, string.Format(newHtml, divHtml));
		}

		private DataSet ParseShipHtml(string fullPathFile)
		{
			HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
			string html = File.ReadAllText(fullPathFile);
			string subHtml;
			int startIndex, endIndex;			

			// ********** Ship Detail
			startIndex = html.IndexOf("<DIV class=\"pageheader\">Ship Detail</DIV>");
			startIndex = html.IndexOf("<TABLE", startIndex + 1);
			endIndex = html.IndexOf("</TABLE>", startIndex + 1) + 8;

			subHtml = html.Substring(startIndex, endIndex - startIndex);			
			doc.LoadHtml(subHtml);

			DataTable dtShipDetail = new DataTable();
			dtShipDetail.Columns.Add("DUMMY");
			dtShipDetail.Rows.Add();

			foreach (var row in doc.DocumentNode.SelectNodes("//tr[td]"))
			{
				string[] cell = row.SelectNodes("td").Select(td => td.InnerText).ToArray();

				for (int i = 0; i < cell.Length; i++)
					cell[i] = cell[i].Trim();

				for (int j = 0; j < cell.Length; j++)
				{
					if (cell[j].In("Ship Name", "Call Sign", "MMSI No.", "Flag", "Operator", "Shiptype", "Gross", "Deadweight", "Year of Build", "Status"))
					{
						string colName = cell[j].Replace(" ", "_").Replace(".", "").ToUpper();
						dtShipDetail.Columns.Add(colName);

						string value = cell[j + 1].Replace("\r", "").Replace("\n", "").Replace("&amp;", "&");
						value = Regex.Replace(value, @"\s+", " ");
						value = Html.Decode(value);

						if (cell[j].In("Gross", "Deadweight"))
							dtShipDetail.Rows[0][colName] = GetTo.Int(value.Replace(",", ""));
						else
							dtShipDetail.Rows[0][colName] = value;
						j++;
					}
				}
			}

			// 조선소코드만 임시 저장함
			string builderCodeTemp = "";

			if (subHtml.IndexOf(";code=") > 0)
				builderCodeTemp = subHtml.Substring(subHtml.IndexOf(";code=") + 6, 6);			

			// ********** Ownership
			startIndex = html.IndexOf("runat=\"server\">Ownership<");
			startIndex = html.IndexOf("<TABLE", startIndex + 1);
			endIndex = html.IndexOf("</TABLE>", startIndex + 1) + 8;

			subHtml = html.Substring(startIndex, endIndex - startIndex);
			doc.LoadHtml(subHtml);

			foreach (var row in doc.DocumentNode.SelectNodes("//tr[td]"))
			{
				string[] cell = row.SelectNodes("td").Select(td => td.InnerText).ToArray();

				// 테크니컬 매니저 값만 가져옴 (= 운항선사)
				if (cell[0] == "Technical Manager")
				{					
					string value = cell[1].Replace("\r", "").Replace("\n", "").Replace("&amp;", "&");
					value = Regex.Replace(value, @"\s+", " ");
					value = Html.Decode(value.Trim());

					dtShipDetail.Columns.Add("TECH_MANAGER");
					dtShipDetail.Rows[0]["TECH_MANAGER"] = value;

					break;
				}				
			}

			// ********** Class
			startIndex = html.IndexOf("<DIV class=\"resultheader\">Class</DIV>");
			startIndex = html.IndexOf("<TABLE", startIndex + 1);
			endIndex = html.IndexOf("</TABLE>", startIndex + 1) + 8;

			subHtml = html.Substring(startIndex, endIndex - startIndex);
			subHtml = subHtml.Replace("<BR>", "||").Replace(",", "||");     // 행이 바뀌거나 다음 콤마가 오기 전까지 부분이 Class 부분임
			doc.LoadHtml(subHtml);

			foreach (var row in doc.DocumentNode.SelectNodes("//tr[td]"))
			{
				string[] cell = row.SelectNodes("td").Select(td => td.InnerText).ToArray();
				string value = cell[0].Split(new string[] { "||" }, StringSplitOptions.None)[0];
				value = value.Replace("Class: ", "").Replace(Environment.NewLine, "").Trim();
				value = Regex.Replace(value, @"\s+", " ");

				dtShipDetail.Columns.Add("CLASS");
				dtShipDetail.Rows[0]["CLASS"] = value;
			}

			// ********** Ship Builder
			startIndex = html.IndexOf("<DIV class=\"resultheader\">Ship Builder</DIV>");
			startIndex = html.IndexOf("<TABLE", startIndex + 1);
			endIndex = html.IndexOf("</TABLE>", startIndex + 1) + 8;

			subHtml = html.Substring(startIndex, endIndex - startIndex);			
			subHtml = subHtml.Replace("<BR>", "||");    // 대문자 <BR>태그를 우선 || 로 변환한다. 가끔 조선소가 2개씩 인 것들이 있음
			doc.LoadHtml(subHtml);

			foreach (var row in doc.DocumentNode.SelectNodes("//tr[td]"))
			{
				string[] cell = row.SelectNodes("td").Select(td => td.InnerText).ToArray();
				string text = cell[0].Replace("\r", "").Replace("\n", "").Trim();
				text = Regex.Replace(text, @"\s+", " ");
				text = Html.Decode(text);

				// Ship Builder가 여러개 있을수 있으므로 그중에 하나 검색
				string builderInfo = "";
				string builderCode = "";
				string[] texts = text.Split(new string[] { "||" }, StringSplitOptions.None);

				for (int i = 0; i < texts.Length; i++)
				{
					if (texts[i].IndexOf("Yard/hull No") >= 0 && (texts[i].IndexOf("(Hull)") > 0 || texts[i].IndexOf("(Hull launched by)") > 0))
					{
						builderInfo = texts[i];
						builderCode = row.SelectNodes("//a")[i].Attributes["href"].Value;
						break;
					}
				}

				if (builderInfo == "")
				{
					builderInfo = texts[0];
					if (row.SelectNodes("//a") != null)
						builderCode = row.SelectNodes("//a")[0].Attributes["href"].Value;
				}

				if (builderCode != "")
					builderCode = builderCode.Substring(builderCode.IndexOf("CODE=") + 5);

				// 조선소코드가 없는 경우 Ship Detail 부분에서 임시 저장한 것을 가져옴
				if (builderCode == "")
					builderCode = builderCodeTemp;				

				// 인도일
				Regex regDelivery = new Regex("[12][0-9][0-9][0-9]-[01][0-9]");
				Match mchDelivery = builderInfo.Length >= 10 ? regDelivery.Match(builderInfo.Substring(0, 10)) : regDelivery.Match(builderInfo.Substring(0, builderInfo.Length));
				string delivery = mchDelivery.Value;

				if (delivery == "")
				{
					regDelivery = new Regex("[12][0-9][0-9][0-9]");
					mchDelivery = builderInfo.Length >= 10 ? regDelivery.Match(builderInfo.Substring(0, 10)) : regDelivery.Match(builderInfo.Substring(0, builderInfo.Length));
					delivery = mchDelivery.Value;					
				}

				// 호선번호
				string hullNo = "";
				if (builderInfo.IndexOf("Yard/hull No.: ") >= 0)
					hullNo = builderInfo.Substring(builderInfo.IndexOf("Yard/hull No.: ")).Replace("Yard/hull No.: ", "").Trim();

				// 조선소
				string shipBuilder = builderInfo;

				if (shipBuilder.IndexOf("Yard/hull No.: ") >= 0)
					shipBuilder = builderInfo.Remove(builderInfo.IndexOf("Yard/hull No.: "));

				if (delivery != "")
					shipBuilder = shipBuilder.Replace(delivery, "").Trim();

				if (shipBuilder != "" && shipBuilder.Left(1) == "(")
					shipBuilder = shipBuilder.Substring(shipBuilder.IndexOf(")") + 1).Trim();	// "(" 로 시작하는 경우는 괄호 부분은 없앰

				// 테이블에 집어 넣기
				dtShipDetail.Columns.Add("DELIVERY");
				dtShipDetail.Columns.Add("HULL_NO");
				dtShipDetail.Columns.Add("SHIPBUILDER");
				dtShipDetail.Columns.Add("SHIPBUILDER_CODE");

				dtShipDetail.Rows[0]["DELIVERY"] = delivery.Replace("-", "/");  // 여기서 "/" 표시로 바꾸고 DB에 넣을때 "/" 마저 빼자
				dtShipDetail.Rows[0]["HULL_NO"] = hullNo;
				dtShipDetail.Rows[0]["SHIPBUILDER"] = shipBuilder;
				dtShipDetail.Rows[0]["SHIPBUILDER_CODE"] = builderCode;
			}

			// ********** Sister Ships
			startIndex = html.IndexOf("<DIV class=\"resultheader\">Sister Ships</DIV>");
			startIndex = html.IndexOf("<TABLE", startIndex + 1);
			startIndex = html.IndexOf("<TABLE", startIndex + 1);
			endIndex = html.IndexOf("</TABLE>", startIndex + 1) + 8;

			subHtml = html.Substring(startIndex, endIndex - startIndex);
			doc.LoadHtml(subHtml);

			DataTable dtSisterShips = new DataTable();
			bool startRow = true;

			foreach (var row in doc.DocumentNode.SelectNodes("//tr[td]"))
			{
				string[] cell = row.SelectNodes("td").Select(td => td.InnerText).ToArray();

				for (int i = 0; i < cell.Length; i++)
					cell[i] = cell[i].Trim();

				if (startRow)
				{
					foreach (string c in cell)
						dtSisterShips.Columns.Add(c.Replace(" ", "_").ToUpper());

					startRow = false;
				}
				else
				{
					dtSisterShips.Rows.Add(cell);
				}
			}

			// 완료
			DataSet ds = new DataSet();
			ds.Tables.Add(dtShipDetail);
			ds.Tables.Add(dtSisterShips);

			return ds;
		}

		#endregion

		#region ==================================================================================================== Etc

		private bool CheckImoFormat(string imoNumber)
		{
			try
			{
				Regex regex = new Regex(@"^\d{7}$");
				return regex.IsMatch(imoNumber);
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}

			return false;
		}

		private bool CheckImoDuplication(string imoNumber)
		{
			if (SQL.GetDataTable("SELECT 1 FROM CZ_MA_HULL WITH(NOLOCK) WHERE NO_IMO = '" + imoNumber + "'").Rows.Count == 0)
				return true;
			else
				return false;
		}

		private bool CheckYearMonthFormat(string yearMonth)
		{
			Regex regex1 = new Regex(@"^\d{4}$");
			Regex regex2 = new Regex(@"^\d{4}/\d{2}$");

			return regex1.IsMatch(yearMonth) || regex2.IsMatch(yearMonth);
		}

		private void CompareShip()
		{
			if (tbx호선번호.Text == tbx호선번호2.Text)
				btn호선번호.Visible = false;
			else
				btn호선번호.Visible = true;

			if (tbx선명.Text == tbx선명2.Text)
				btn선명.Visible = false;
			else
				btn선명.Visible = true;
		}

		private void SetGridStyle(FlexGrid flexGrid)
		{
			flexGrid.Redraw = false;

			for (int i = flexGrid.Rows.Fixed; i < flexGrid.Rows.Count; i++)
			{
				string fileName = flexGrid[i, "HTML_FILE_LOC"].ToString();

				if (fileName == "")
					flexGrid.SetCellImage(i, flexGrid.Cols["FILE_ICON"].Index, null);
				else
					flexGrid.SetCellImage(i, flexGrid.Cols["FILE_ICON"].Index, Icons.GetExtension(Path.GetExtension(fileName)));
			}

			flexGrid.Redraw = true;
		}

		#endregion

	}


}

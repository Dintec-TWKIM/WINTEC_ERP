using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Duzon.Common.Forms;
using DzHelpFormLib;

using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.ERPU;
using Dintec;
using Duzon.Common.Util;
using System.Net;
using Duzon.Common.Controls;
using System.Text.RegularExpressions;
using Duzon.Common.BpControls;
using Parsing;
using DX;

// 20190917
namespace cz
{
	public partial class P_CZ_SA_INQ : PageBase
	{
		FreeBinding Header = new FreeBinding();
		string UnitDefault = "PCS";
		string ItemGroupDefault = "";
		
		#region ===================================================================================================== Property

		public P_CZ_SA_QTN_REG Quotation
		{
			get
			{
				return (P_CZ_SA_QTN_REG)this.Parent.GetContainerControl();
			}
		}
	
		public FlexGrid 그리드라인
		{
			get
			{
				return grd라인;
			}
		}

		#endregion

		#region ==================================================================================================== Constructor

		public P_CZ_SA_INQ()
		{			
			InitializeComponent();
			//StartUp.Certify(this);
		}

		#endregion

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{
			TitleText = "Loaded";
			InitControl();
			InitGrid();
			InitEvent();

			//URL.GetShortner("", "");
		}

		private void InitControl()
		{
			cbo엔진모델.ValueMember = "CODE";
			cbo엔진모델.DisplayMember = "NAME";

			// 콤보박스
			DataSet ds = GetDb.Code("GRP_ITEM", "CZ_SA00043", "MA_B000004");
			cbo유형.DataBind(ds.Tables[0], false);
			cbo주제.DataBind(ds.Tables[1], true);
			cbo단위.DataBind(ds.Tables[2], false);
			cbo유형적용.DataBind(ds.Tables[0], true);
			cbo주제적용.DataBind(ds.Tables[1], true);
			cbo단위적용.DataBind(ds.Tables[2], true);

			// 유형, 단위 기본값
			ItemGroupDefault = Quotation.부품영업 ? "EQ" : "GS";

			cbo유형.SetValue(ItemGroupDefault);
			cbo단위.SetValue(UnitDefault);

			cbo유형적용.SetValue(ItemGroupDefault);
			cbo단위적용.SetValue(UnitDefault);

			// 바인딩 초기화
			DataTable dtHead = DBMgr.GetDataTable("PS_CZ_SA_INQ_H_R3", "", "");
			DataTable dtLine = DBMgr.GetDataTable("PS_CZ_SA_INQ_L_R3", "", "");

			Header.SetBinding(dtHead, one헤드);
			Header.ClearAndNewRow();
			grd라인.Binding = dtLine;
			SearchSupplier("", "");
		}

		private void InitGrid()
		{
			DataTable dtYn = new DataTable();
			dtYn.Columns.Add("CODE");
			dtYn.Columns.Add("NAME");
			dtYn.Rows.Add("P", DD("선택"));
			dtYn.Rows.Add("C", DD("완료"));

			// ********** 라인 그리드
			grd라인.BeginSetting(1, 1, false);

			grd라인.SetCol("NO_FILE"			, "파일번호"		, false);
			grd라인.SetCol("NO_HST"			, "차수"			, false);
			grd라인.SetCol("NO_LINE"			, "항번"			, false);
			grd라인.SetCol("NO_LINE_PARENT"	, "부모항번"		, false);
			grd라인.SetCol("NO_DSP"			, "순번"			, 40	, true);
			grd라인.SetCol("GRP_ITEM"		, "유형코드"		, false);
			grd라인.SetCol("NM_SUBJECT"		, "주제"			, 130	, true);
			grd라인.SetCol("CD_UNIQ_PARTNER"	, "선사코드"		, 80	, true);
			grd라인.SetCol("CD_ITEM_PARTNER"	, "품목코드"		, 130	, true);
			grd라인.SetCol("NM_ITEM_PARTNER"	, "품목명"		, 340	, true);
			grd라인.SetCol("UNIT"			, "단위"			, 45	, true);
			grd라인.SetCol("QT"				, "수량"			, 50	, true, typeof(decimal), FormatTpType.QUANTITY);
			grd라인.SetCol("YN_QLINK"		, "퀵링크"		, 70);
			grd라인.SetCol("CD_ITEM"			, "재고코드"		, 80	, true);
			grd라인.SetCol("NM_ITEM"			, "재고명"		, false);
			grd라인.SetCol("UCODE"			, "U코드"		, 90	, true);
			grd라인.SetCol("KCODE"			, "K코드"		, false);
			grd라인.SetCol("TP_ENGINE"		, "사양"			, false);
			grd라인.SetCol("NO_ENGINE"		, "엔진번호"		, false);
			grd라인.SetCol("SORT"			, "SORT"		, false);
			grd라인.SetCol("TP_BOM"			, "BOM구분"		, false);

			grd라인.Cols["NO_DSP"].Format = "####.##";
			grd라인.Cols["NO_DSP"].TextAlign = TextAlignEnum.CenterCenter;
			grd라인.Cols["UNIT"].TextAlign = TextAlignEnum.CenterCenter;
			grd라인.Cols["CD_ITEM"].TextAlign = TextAlignEnum.CenterCenter;
			grd라인.Cols["YN_QLINK"].TextAlign = TextAlignEnum.CenterCenter;
			grd라인.SetDataMap("UNIT", GetDb.Code("MA_B000004"), "CODE", "NAME");
			grd라인.SetDataMap("YN_QLINK", dtYn, "CODE", "NAME");

			grd라인.SetOneGridBinding(new object[] { }, one라인);
			grd라인.SetDefault("19.04.10.01", SumPositionEnum.None);

			grd라인.Styles.Add("QLINK_Y");
			grd라인.Styles["QLINK_Y"].Font = new Font("맑은 고딕", 9, FontStyle.Bold);
			grd라인.Styles["QLINK_Y"].ForeColor = Color.Blue;

			grd라인.Styles.Add("QLINK_P");
			grd라인.Styles["QLINK_P"].Font = new Font("맑은 고딕", 9, FontStyle.Bold);
			grd라인.Styles["QLINK_P"].ForeColor = Color.Red;

			grd라인.LoadUserCache("P_CZ_SA_QTN_REG_SINQ");

			// ********** 매입처검색
			grd매입처검색.BeginSetting(1, 1, false);
			grd매입처검색.SetCol("CD_PARTNER"	, "코드"			, 60);
			grd매입처검색.SetCol("NM_PARTNER"	, "매입처명"		, 250);
			grd매입처검색.SetCol("NM_CEO"		, "대표자"		, 80);
			grd매입처검색.SetCol("NO_COMPANY"	, "사업자번호"	, 100);
			
			grd매입처검색.Cols["CD_PARTNER"].TextAlign = TextAlignEnum.CenterCenter;
			grd매입처검색.Cols["NM_CEO"].TextAlign = TextAlignEnum.CenterCenter;
			grd매입처검색.Cols["NO_COMPANY"].TextAlign = TextAlignEnum.CenterCenter;

			grd매입처검색.SetDefault("19.05.01.01", SumPositionEnum.None);

			// ********** 매입처선택
			grd매입처선택.BeginSetting(1, 1, false);
			grd매입처선택.SetCol("CD_PARTNER"	, "코드"			, 60);
			grd매입처선택.SetCol("NM_PARTNER"	, "매입처명"		, 250);
			grd매입처선택.SetCol("NM_CEO"		, "대표자"		, 80);
			grd매입처선택.SetCol("NO_COMPANY"	, "사업자번호"	, 100);
			
			grd매입처선택.Cols["CD_PARTNER"].TextAlign = TextAlignEnum.CenterCenter;
			grd매입처선택.Cols["NM_CEO"].TextAlign = TextAlignEnum.CenterCenter;
			grd매입처선택.Cols["NO_COMPANY"].TextAlign = TextAlignEnum.CenterCenter;
			
			grd매입처선택.SetDefault("19.05.01.01", SumPositionEnum.None);
		}

		protected override void InitPaint()
		{
			tbx포커스.Left = -500;
			spl메인.SplitterDistance = spl메인.Width - 690;

			if (!Certify.IsLive())
			{
				grd라인.Cols.Remove("UCODE");
				grd라인.Cols.Remove("KCODE");
			}
		}

		public void Clear()
		{
			if (TitleText != "Loaded")
				return;

			// 헤더
			Header.ClearAndNewRow();

			// 라인
			cbo엔진모델.ClearData();
			tbx순번.Text = "";
			cbo유형.SetValue(ItemGroupDefault);
			cbo주제.SetDefaultValue();
			tbx주제.Text = "";
			tbx선사코드.Text = "";
			tbx품목코드.Text = "";
			tbx품목명.Text = "";
			cur수량.DecimalValue = 0;
			cbo단위.SetValue(UnitDefault);

			// 일괄변경
			tbxFrom.Text = "";
			tbxTo.Text = "";
			cbo유형적용.SetValue(ItemGroupDefault);
			cbo주제적용.SetDefaultValue();
			tbx주제적용.Text = "";
			cbo단위적용.SetValue(UnitDefault);

			// 매입처
			tbx매입처검색.Text = "";

			// 그리드
			grd라인.ClearData();
			grd매입처검색.ClearData();
			grd매입처선택.ClearData();
		}

		public void 사용(bool editable)
		{
			pnl버튼.Editable(editable);

			one헤드.Editable(editable);
			one엔진.Editable(editable);
			one라인.Editable(editable);
			one변경.Editable(editable);

			one매입처.Editable(editable);
			pnl매입처추가.Editable(editable);
			pnl매입처삭제.Editable(editable);

			grd라인.AllowEditing = editable;
		}

		#endregion

		#region ==================================================================================================== Event

		private void InitEvent()
		{
			Header.ControlValueChanged += new FreeBindingEventHandler(Header_ControlValueChanged);
			ctx매출처담당자.QueryBefore += new BpQueryHandler(ctx매출처담당자_QueryBefore);

			tbx순번.Validated += new EventHandler(tbx순번_Validated);
			cur수량.KeyDown += new KeyEventHandler(cur수량_KeyDown);
			cbo단위.KeyPress += new KeyPressEventHandler(cbo단위_KeyPress);

			tbx주제.GotFocus += new EventHandler(textBox_GotFocus);
			tbx선사코드.GotFocus += new EventHandler(textBox_GotFocus);
			tbx품목코드.GotFocus += new EventHandler(textBox_GotFocus);
			tbx품목명.GotFocus += new EventHandler(textBox_GotFocus);

			btn퀵링크.Click += new EventHandler(btn퀵링크_Click);
			btn매입처지정.Click += new EventHandler(Btn매입처지정_Click);
			btn재정렬.Click += new EventHandler(btn재정렬_Click);

			btn파싱.Click += new EventHandler(btn파싱_Click);
			btn엑셀업로드.Click += new EventHandler(btn엑셀업로드_Click);

			btn기부속등록.Click += new EventHandler(btn기부속등록_Click);
			btnBOM등록.Click += new EventHandler(btnBOM등록_Click);

			btn추가.Click += new EventHandler(btn추가_Click);
			btn삽입.Click += new EventHandler(btn삽입_Click);
			btn삭제.Click += new EventHandler(btn삭제_Click);

			btn현대엑셀.Click += Btn현대엑셀_Click;
			btn두산엑셀.Click += new EventHandler(btn두산엑셀_Click);
			btn두산저장.Click += new EventHandler(btn두산저장_Click);

			btn엔터리셋.Click += new EventHandler(btn엔터리셋_Click);

			btn유형적용.Click += new EventHandler(btn유형적용_Click);
			btn주제적용.Click += new EventHandler(btn주제적용_Click);
			btn단위적용.Click += new EventHandler(btn단위적용_Click);

			grd라인.AfterRowChange += new RangeEventHandler(grd라인_AfterRowChange);
			grd라인.BeforeDoubleClick += new BeforeMouseDownEventHandler(grd라인_BeforeDoubleClick);
			grd라인.DoubleClick += new EventHandler(grd라인_DoubleClick);
			grd라인.KeyDown += new KeyEventHandler(grd라인_KeyDown);
			grd라인.ValidateEdit += new ValidateEditEventHandler(Fgd라인_ValidateEdit);

			tbx매입처검색.KeyDown += new KeyEventHandler(tbx매입처검색_KeyDown);
			btn매입처검색.Click += new EventHandler(btn매입처검색_Click);
			btn매입처추가.Click += new EventHandler(btn매입처추가_Click);
			btn매입처삭제.Click += new EventHandler(btn매입처삭제_Click);
			grd매입처검색.DoubleClick += new EventHandler(grd매입처검색_DoubleClick);
			grd매입처선택.DoubleClick += new EventHandler(grd매입처선택_DoubleClick);

			btn테스트.Click += Btn테스트_Click;
		}

		private void Btn테스트_Click(object sender, EventArgs e)
		{
			메시지.작업중("작업시작");

			//UPDATE CZ_SA_QTNL SET DTS_DX = NULL WHERE CD_COMPANY = 'K100' AND DTS_INSERT >= '20220101'
			
			for (int i = 0; i < 1; i++)
			{
				string query = @"
SELECT TOP 10000
	CD_COMPANY
,	NO_FILE
,	NO_LINE
,	NM_SUBJECT
,	CD_ITEM_PARTNER
,	NM_ITEM_PARTNER
FROM CZ_SA_QTNL
WHERE 1 = 1
	AND CD_COMPANY = 'K100'
	AND DTS_INSERT >= '20220101'
	AND DTS_DX IS NULL";

				DataTable dt = 디비.결과(query);
				키워드.견적저장(dt);
			}

			메시지.작업중();
		}

		private void Header_ControlValueChanged(object sender, FreeBindingArgs e)
		{
			(Parent.GetContainerControl() as PageBase).ToolBarSaveButtonEnabled = true;
			//Quotation.Header_ControlValueChanged(sender, e);
		}

		private void ctx매출처담당자_QueryBefore(object sender, BpQueryArgs e)
		{
			e.HelpParam.P00_CHILD_MODE = "담당자";

			// 첫번째 문자부터 첫빈칸까지를 컬럼이름으로 가져가므로 주석 가져가도록 처리해줌
			e.HelpParam.P61_CODE1 = @"-- 
	  CONVERT(NVARCHAR(3), SEQ)	AS CODE
	, NM_PTR					AS NAME";

			e.HelpParam.P62_CODE2 = @"
FI_PARTNERPTR WITH(NOLOCK)";

			e.HelpParam.P63_CODE3 = @"
WHERE 1 = 1
	AND CD_COMPANY = '" + Quotation.회사코드 + @"'
	AND CD_PARTNER = '" + Quotation.매출처코드 + "'";

			// 요기 뒤에 컬럼이름 + 문자열이 붙으므로 여기도 주석 가져가도록 처리해줌
			if (e.HelpParam.P92_DETAIL_SEARCH_CODE != "")
				e.HelpParam.P63_CODE3 += @"
	AND NM_PTR LIKE '%" + e.HelpParam.P92_DETAIL_SEARCH_CODE + "%'--";

			e.HelpParam.P64_CODE4 = @"
ORDER BY NM_PTR";

			e.HelpParam.P92_DETAIL_SEARCH_CODE = "";
		}
	
		private void tbx순번_Validated(object sender, EventArgs e)
		{
			grd라인["NO_DSP"] = GetTo.Decimal(tbx순번.Text);
		}

		private void cur수량_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
				btn추가_Click(null, null);
		}

		private void cbo단위_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)13)
				btn추가_Click(null, null);
		}

		private void textBox_GotFocus(object sender, EventArgs e)
		{
			if (sender is TextBoxExt)
			{
				TextBoxExt o = (TextBoxExt)sender;
				o.SelectionLength = 0;
				o.SelectionStart = o.Text.Length;
			}
			else if (sender is CurrencyTextBox)
			{
				CurrencyTextBox o = (CurrencyTextBox)sender;
				o.SelectionLength = 0;
				o.SelectionStart = o.Text.Length;
			}
		}

		private void tbx매입처검색_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
				btn매입처검색_Click(null, null);
		}

		private void btn매입처검색_Click(object sender, EventArgs e)
		{
			if (tbx매입처검색.Text.Trim() == "")
			{
				ShowMessage("검색어를 입력하세요!");
				return;
			}

			SearchSupplier(Quotation.회사코드, tbx매입처검색.Text);
		}

		private void btn매입처추가_Click(object sender, EventArgs e)
		{
			// 중복 체크
			if (grd매입처선택.DataTable.Select("CD_PARTNER = '" + grd매입처검색["CD_PARTNER"] + "'").Length > 0)
			{
				MessageBox.Show("중복");
				return;
			}

			// 추가
			grd매입처선택.Rows.Add();
			grd매입처선택.Row = grd매입처선택.Rows.Count - 1;

			for (int j = 0; j < grd매입처검색.Cols.Count; j++)
				grd매입처선택[grd매입처선택.Cols[j].Name] = grd매입처검색[grd매입처검색.Cols[j].Name];

			grd매입처선택.AddFinished();
		}

		private void btn매입처삭제_Click(object sender, EventArgs e)
		{
			grd매입처선택.Rows.Remove(grd매입처선택.Row);
		}

		private void grd매입처검색_DoubleClick(object sender, EventArgs e)
		{
			btn매입처추가_Click(null, null);
		}

		private void grd매입처선택_DoubleClick(object sender, EventArgs e)
		{
			btn매입처삭제_Click(null, null);
		}

		#endregion		

		#region ==================================================================================================== 버튼 이벤트

		private void btn퀵링크_Click(object sender, EventArgs e)
		{
			// 재고코드가 없는 애들을 대상으로 다시 수행
			grd라인.Redraw = false;

			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
			{
				if (grd라인[i, "CD_ITEM"].ToString() == "")
					grd라인[i, "YN_QLINK"] = "N";
			}

			// 저장 및 조회
			저장();
			Search();
		}

		private void Btn매입처지정_Click(object sender, EventArgs e)
		{
			DebugMode debugMode = Control.ModifierKeys == (Keys.Control | Keys.Alt) ? DebugMode.Popup : DebugMode.None;

			// 워크플로우에 파일이 없으면 로컬에서 수동으로 업로드
			OpenFileDialog f = new OpenFileDialog();
			f.Filter = DD("엑셀") + "|*.xls;*.xlsx";

			if (f.ShowDialog() != DialogResult.OK)
				return;

			ExcelReader excelReader = new ExcelReader();
			DataTable dtExcel = excelReader.Read(f.FileName, 1, 2);

			dtExcel.Columns.Add("CD_COMPANY", typeof(string), "'" + Quotation.회사코드 + "'").SetOrdinal(0);
			dtExcel.Columns.Add("NO_FILE", typeof(string), "'" + Quotation.파일번호 + "'").SetOrdinal(1);

			DBMgr.GetDataTable("PX_CZ_SA_INQ_VENDOR_EXCEL", debugMode, GetTo.Xml(dtExcel, "", "CD_COMPANY", "NO_FILE", "NO_DSP", "CD_PARTNER"));
			ShowMessage(PageResultMode.SaveGood);
		}
		
		private void btn재정렬_Click(object sender, EventArgs e)
		{			
			if (!grd라인.HasNormalRow)
				return;

			// 소수점 찾기
			string[] numbs = grd라인["NO_DSP"].ToString().Split('.');
			double factor = 0;

			if (numbs.Length > 1)			
				factor = 1 / Math.Pow(10, numbs[1].Length);			
			else
				factor = 1;

			// 재정렬
			CellRange range = grd라인.Selection;

			for (int i = range.r1 + 1; i <= range.r2; i++)
				grd라인[i, "NO_DSP"] = GetTo.Double(grd라인[i - 1, "NO_DSP"]) + factor;
		}

		private void btn파싱_Click(object sender, EventArgs e)
		{
			// 워크플로우에서 파일 자동 검색
			string query = @"
SELECT
	NM_FILE_REAL
FROM CZ_MA_WORKFLOWL WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + Quotation.회사코드 + @"'
	AND NO_KEY = '" + Quotation.파일번호 + @"'
	AND TP_STEP = '01'
	AND NM_FILE_REAL IS NOT NULL
	AND ISNULL(YN_INCLUDED, 'N') = 'N'
ORDER BY NO_LINE DESC";

            DataTable dtAttached = DBMgr.GetDataTable(query);
            string fileName = "";

			if (dtAttached.Rows.Count > 0)
			{
				fileName = Application.StartupPath + @"\temp\" + FileMgr.Download_WF(Quotation.회사코드, Quotation.파일번호, dtAttached.Rows[0][0].ToString(), false);
			}
			else
			{
				// 워크플로우에 파일이 없으면 로컬에서 수동으로 업로드
				OpenFileDialog f = new OpenFileDialog();
				f.Filter = "Inquiry|*.msg;*.pdf;*.xls;*.xlsx;*.xlsm";

				if (f.ShowDialog() != DialogResult.OK)
					return;

				fileName = f.FileName;
			}

            // 분석 시작
            MsgControl.ShowMsg(DD("파싱 중 입니다."));

			try
			{
				// Inquiry 파싱 시작
				InquiryParser parser = new InquiryParser(fileName);
				parser.Parse(true);

				// 호선 가져오기
				query = @"
SELECT
	  A.NO_IMO
	, A.NO_HULL
	, A.NM_VESSEL
	, B.CD_PARTNER
	, B.LN_PARTNER
	, B.CD_PARTNER_GRP
FROM	  CZ_MA_HULL	AS A WITH(NOLOCK)
LEFT JOIN MA_PARTNER	AS B WITH(NOLOCK) ON A.CD_PARTNER = B.CD_PARTNER AND B.CD_COMPANY = @CD_COMPANY
WHERE A.NO_IMO = @NO_IMO OR (@NO_IMO IS NULL AND A.NM_VESSEL LIKE '%' + @NM_VESSEL + '%')";

				DBMgr dbm = new DBMgr();
				dbm.DebugMode = DebugMode.Print;
				dbm.Query = query;
				dbm.AddParameter("@CD_COMPANY"	, Quotation.회사코드);
				dbm.AddParameter("@NO_IMO"		, parser.ImoNumber);
				dbm.AddParameter("@NM_VESSEL"	, parser.Vessel);
				DataTable dt = dbm.GetDataTable();

				if (dt.Rows.Count == 1)
				{
					Quotation.Imo번호 = dt.Rows[0]["NO_IMO"].문자();

					if (dt.Rows[0]["CD_PARTNER"].ToString() != "")
					{
						Quotation.매출처코드 = dt.Rows[0]["CD_PARTNER"].문자();
						SetPartnerPic(Quotation.매출처코드, parser.Contact);
					}
				}

				// 문의번호
				Quotation.문의번호 = parser.Reference;

				foreach (DataRow row in parser.Item.Rows)
				{
					grd라인.Rows.Add();
					grd라인.Row = grd라인.Rows.Count - 1;
					grd라인["NO_LINE"]			= (int)grd라인.Aggregate(AggregateEnum.Max, "NO_LINE") + 1;
					grd라인["NO_DSP"]			= (int)grd라인.Aggregate(AggregateEnum.Max, "NO_DSP") + 1;
					grd라인["GRP_ITEM"]			= ItemGroupDefault;
					grd라인["NM_SUBJECT"]		= row["SUBJ"].ToString().ToUpper();
					grd라인["UNIT"]				= row["UNIT"].ToString().ToUpper();
					grd라인["CD_ITEM_PARTNER"]	= row["ITEM"].ToString().ToUpper();
					grd라인["NM_ITEM_PARTNER"]	= row["DESC"].ToString().ToUpper();
					grd라인["CD_UNIQ_PARTNER"]	= row["UNIQ"].ToString().ToUpper();
					grd라인["QT"]				= row["QT"];
					grd라인["TP_BOM"]			= "S";
					grd라인.AddFinished();
				}
				
				SetGridStyle();
				MsgControl.CloseMsg();
			}
			catch (Exception ex)
			{
				MsgControl.CloseMsg();
				ShowMessage(ex.Message);
			}
		}
		
		private void btn엑셀업로드_Click(object sender, EventArgs e)
		{
			if (ModifierKeys == Keys.Control)
			{
				FolderBrowserDialog f = new FolderBrowserDialog();

				if (f.ShowDialog() == DialogResult.OK)
				{
					string fileName = "ExcelForm_Inquiry.xls";
					WebClient wc = new WebClient();
					wc.DownloadFile(Global.MainFrame.HostURL + "/shared/CZ/" + fileName, f.SelectedPath + @"\" + fileName);
				}
			}
			else
			{
				// 엑셀 업로드
				OpenFileDialog f = new OpenFileDialog();
				f.Filter = DD("엑셀 파일") + " |*.xls;*.xlsx";

				if (f.ShowDialog() != DialogResult.OK)
					return;

				// 워크플로우 파일 저장
				if (Quotation.담당자 != "S-343")
				{
					string fileName = FileMgr.Upload_WF(Quotation.회사코드, Quotation.파일번호, f.FileName, false);

					// 워크플로우 디비 저장
					WorkFlow workflow = new WorkFlow(Quotation.파일번호, "55", Quotation.차수);
					workflow.AddItem("", "", f.SafeFileName, fileName);
					workflow.Save();
				}

				// 엑셀 불러오기
				ExcelReader excel = new ExcelReader();
				DataTable dtExcel = excel.Read(f.FileName, 1, 4).Select("NM_ITEM_PARTNER <> ''").CopyToDataTable();

				// 그리드에 아이템 추가
				DataTable dtNew = grd라인.DataTable.Clone();
				int index = (int)grd라인.Aggregate(AggregateEnum.Max, "NO_LINE");

				for (int i = 0; i < dtExcel.Rows.Count; i++)
				{
					// 콤보박스 아이템 확인
					string itemGroup = dtExcel.Rows[i]["GRP_ITEM"].ToStr().ToUpper();
					string subjectCode = dtExcel.Rows[i]["CD_SUBJECT"].ToStr().ToUpper();
					string unit = dtExcel.Rows[i]["UNIT"].ToStr().ToUpper();

					if (cbo유형.GetData().Select("CODE = '" + itemGroup + "'").Length == 0)
					{
						ShowMessage("잘못된 유형이 있습니다 : " + (itemGroup == "" ? "빈칸" : itemGroup));
						return;
					}

					if (cbo주제.GetData().Select("ISNULL(CODE, '') = '" + subjectCode + "'").Length == 0)
					{
						ShowMessage("잘못된 유형(2)이 있습니다 : " + (subjectCode == "" ? "빈칸" : subjectCode));
						return;
					}

					if (cbo단위.GetData().Select("CODE = '" + unit + "'").Length == 0)
					{
						ShowMessage("잘못된 단위가 있습니다 : " + (unit == "" ? "빈칸" : unit));
						return;
					}

					// 추가
					DataRow newRow = dtNew.NewRow();
					newRow["NO_LINE"] = index + i + 1;
					newRow["NO_DSP"] = dtExcel.Rows[i]["NO_DSP"];
					newRow["GRP_ITEM"] = itemGroup;
					newRow["CD_SUBJECT"] = subjectCode;
					newRow["NM_SUBJECT"] = dtExcel.Rows[i]["NM_SUBJECT"].ToStr().ToUpper();
					newRow["CD_UNIQ_PARTNER"] = dtExcel.Rows[i]["CD_UNIQ_PARTNER"].ToStr().ToUpper();
					newRow["CD_ITEM_PARTNER"] = dtExcel.Rows[i]["CD_ITEM_PARTNER"].ToStr().ToUpper();
					newRow["NM_ITEM_PARTNER"] = dtExcel.Rows[i]["NM_ITEM_PARTNER"].ToStr().ToUpper();
					newRow["UNIT"] = unit;
					newRow["QT"] = dtExcel.Rows[i]["QT"];
					newRow["UCODE"] = dtExcel.Rows[i]["UCODE"].ToStr().ToUpper();
					newRow["TP_BOM"] = "S";
					dtNew.Rows.Add(newRow);
				}

				grd라인.BindingAdd(dtNew, "", false);
				SetGridStyle();
			}
		}

		private void btn기부속등록_Click(object sender, EventArgs e)
		{
			H_CZ_ADD_ITEM f = new H_CZ_ADD_ITEM();
			f.Caller = this;
			f.ShowDialog();
			SetGridStyle();
		}

		private void btnBOM등록_Click(object sender, EventArgs e)
		{
			// 싱글이면 부모로 상태값 변경
			if (grd라인["TP_BOM"].ToString() == "S")
			{
				grd라인["TP_BOM"] = "P";
				grd라인.SetCellImage(grd라인.Row, grd라인.Cols["CD_ITEM_PARTNER"].Index, Icons.FolderExpand);
			}

			// 바인딩 할 부모값 저장
			decimal NO_LINE_PARENT = grd라인["NO_LINE_PARENT"].ToString() == "" ? Util.GetTO_Decimal(grd라인["NO_LINE"]) : Util.GetTO_Decimal(grd라인["NO_LINE_PARENT"]);
			DataRow row = grd라인.DataTable.Select("NO_LINE = " + NO_LINE_PARENT)[0];
			string GRP_ITEM = row["GRP_ITEM"].ToString();
			string NM_SUBJECT = row["NM_SUBJECT"].ToString();
			string UNIT = row["UNIT"].ToString();

			// 삽입할 ROW 결정
			bool finded = false;

			for (int i = grd라인.Row + 1; i < grd라인.Rows.Count; i++)
			{
				if (grd라인[i, "NO_LINE_PARENT"].ToString() == "")
				{
					grd라인.DataTable.Rows.InsertAt(grd라인.DataTable.NewRow(), i - 1);		// 행 삽입
					grd라인.Row = i;
					finded = true;
					break;
				}
			}

			if (!finded)
			{
				grd라인.Rows.Add();
				grd라인.Row = grd라인.Rows.Count - 1;
			}

			// 기본값 설정
			grd라인["NO_LINE"] = (int)grd라인.Aggregate(AggregateEnum.Max, "NO_LINE") + 1;
			grd라인["NO_LINE_PARENT"] = NO_LINE_PARENT;
			grd라인["GRP_ITEM"] = GRP_ITEM;
			grd라인["NM_SUBJECT"] = NM_SUBJECT;
			grd라인["UNIT"] = UNIT;
			grd라인["QT"] = 0;
			grd라인["TP_BOM"] = "C";
			grd라인.AddFinished();
			grd라인.SetCellImage(grd라인.Row, grd라인.Cols["CD_ITEM_PARTNER"].Index, Icons.Empty_20x6);

			// 포커스 변경
			tbx품목코드.Focus();
		}

		private void btn추가_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < cur추가.DecimalValue; i++)
			{
				// 복사할 현재값 저장
				string itemGroup = cbo유형.GetValue();
				string subjectCode = cbo주제.GetValue();
				string subjectName = tbx주제.Text;
				string unit = (cbo단위.GetValue() == "") ? UnitDefault : cbo단위.GetValue();

				// 행 추가
				grd라인.Rows.Add();
				grd라인.Row = grd라인.Rows.Count - 1;
				grd라인["NO_LINE"] = (int)grd라인.Aggregate(AggregateEnum.Max, "NO_LINE") + 1;
				grd라인["NO_DSP"] = (int)grd라인.Aggregate(AggregateEnum.Max, "NO_DSP") + 1;
				grd라인["GRP_ITEM"] = itemGroup;
				grd라인["CD_SUBJECT"] = subjectCode;
				grd라인["NM_SUBJECT"] = subjectName;
				grd라인["UNIT"] = unit;
				grd라인["QT"] = 0;
				grd라인["TP_BOM"] = "S";
				grd라인.SetCellImage(grd라인.Row, grd라인.Cols["CD_ITEM_PARTNER"].Index, Icons.Empty_12x6);
				grd라인.AddFinished();
			}

			// 포커스 이동
			if (tbx주제.Text == "")
				tbx주제.Focus();
			else
				tbx품목코드.Focus();
		}

		private void btn삽입_Click(object sender, EventArgs e)
		{
			// 복사할 현재값 저장 (삽입하기 전에 미리 저장)
			string itemGroup = cbo유형.GetValue();
			string subjectCode = cbo주제.GetValue();
			string subjectName = tbx주제.Text;
			string unit = (cbo단위.GetValue() == "") ? UnitDefault : cbo단위.GetValue();

			// 행 삽입
			int row = grd라인.Row;

			grd라인.InsertRow(row);
			grd라인.Row = row;
			grd라인["NO_LINE"] = (int)grd라인.Aggregate(AggregateEnum.Max, "NO_LINE") + 1;
			grd라인["GRP_ITEM"] = itemGroup;
			grd라인["CD_SUBJECT"] = subjectCode;
			grd라인["NM_SUBJECT"] = subjectName;
			grd라인["UNIT"] = unit;
			grd라인["QT"] = 0;
			grd라인["TP_BOM"] = "S";
			grd라인.SetCellImage(grd라인.Row, grd라인.Cols["CD_ITEM_PARTNER"].Index, Icons.Empty_12x6);
			grd라인.AddFinished();
		}

		private void btn삭제_Click(object sender, EventArgs e)
		{
			CellRange range = grd라인.Selection;
			
			for (int i = range.r2; i >= range.r1; i--)
			{
				string lineNumber = grd라인[i, "NO_LINE"].ToString();
				string parentNumber = grd라인[i, "NO_LINE_PARENT"].ToString();
				string bomType = grd라인[i, "TP_BOM"].ToString();

				// 해당 행 삭제
				grd라인.DataTable.Select("NO_LINE = " + lineNumber)[0].Delete();

				// BOM 항목이 있는지 검색
				if (bomType == "P")
				{
					foreach (DataRow row in grd라인.DataTable.Select("NO_LINE_PARENT = " + lineNumber))
						row.Delete();
				}
				else if (bomType == "C")
				{
					if (grd라인.DataTable.Select("NO_LINE_PARENT = " + parentNumber).Length == 0)
					{
						grd라인.DataTable.Select("NO_LINE = " + parentNumber)[0]["TP_BOM"] = "S";
						grd라인.SetCellImage(i - 1, grd라인.Cols["CD_ITEM_PARTNER"].Index, Icons.Empty_12x6);
					}
				}
			}
		}

		private void Btn현대엑셀_Click(object sender, EventArgs e)
		{
			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;

			if (ModifierKeys == Keys.Control)
			{
				FolderBrowserDialog f = new FolderBrowserDialog();

				if (f.ShowDialog() == DialogResult.OK)
				{
					string fileName = "ExcelForm_Inquiry_H.xls";
					WebClient wc = new WebClient();
					wc.DownloadFile(Global.MainFrame.HostURL + "/shared/CZ/" + fileName, f.SelectedPath + @"\" + fileName);
				}
			}
			else
			{
				if (cbo엔진모델.GetValue() == "")
				{
					ShowMessage("엔진모델을 선택하세요");
					return;
				}

				// ********** 엑셀 업로드
				EXCEL excel = new EXCEL();
				if (excel.OpenDialog() != DialogResult.OK)
					return;

				// 워크플로우 파일 저장
				if (Quotation.담당자 != "S-343")
				{
					string fileName = Dintec.FILE.UploadWF(Quotation.파일번호, excel.FileName, false);

					// 워크플로우 디비 저장
					WorkFlow workflow = new WorkFlow(Quotation.파일번호, "55", Quotation.차수);
					workflow.AddItem("", "", excel.SafeFileName, fileName);
					workflow.Save();
				}

				// 엑셀 불러오기
				excel.ReadFirstColumn();

				if (excel.Sheet[0][0].ToString() == "Print No")
				{
					// MAPS 엑셀
					excel.HeaderRowIndex = 1;
					excel.StartDataIndex = 2;
					excel.Read();

					excel.Data.Columns["Print No"].ColumnName = "NO_DSP";
					excel.Data.Columns["Plate No"].ColumnName = "CD_ITEM_PARTNER";
					excel.Data.Columns["Description"].ColumnName = "NM_ITEM_PARTNER";
					excel.Data.Columns["U-CODE"].ColumnName = "UCODE";
					excel.Data.Columns["Qty"].ColumnName = "QT";
					excel.Data.Columns["Unit"].ColumnName = "UNIT";

					if (excel.Data.Columns.Contains("DWG_NO"))
						excel.Data.Columns["DWG_NO"].ColumnName = "KCODE";

					// 순번, 단위 조정
					foreach (DataRow row in excel.Data.Rows)
					{
						row["NO_DSP"] = row["NO_DSP"].ToInt() / 10;
						row["UNIT"] = "PCS";
					}
				}
				else
				{
					// 딘텍 엑셀
					excel.HeaderRowIndex = 1;
					excel.StartDataIndex = 4;
					excel.Read();
					excel.Data.Delete("ISNULL(CD_ITEM_PARTNER, '') = '' OR ISNULL(NM_ITEM_PARTNER, '') = ''");
				}

				// ********** 재고코드 가져오기
				DataRow engine = ((DataRowView)cbo엔진모델.SelectedItem).Row;
				DataTable dtItem = SQL.GetDataTable("PS_CZ_SA_INQ_UPLOAD_HGS_R2", sqlDebug, Quotation.Imo번호, engine["CODE"], excel.Data.ToXml());

				// 중복 아이템 체크 (U코드 등으로 중복 발생할 수 있음)
				if (excel.Data.Rows.Count != dtItem.Rows.Count)
				{
					DataTable dtPk = dtItem.DefaultView.ToTable(true, "NO_DSP", "CD_ITEM_PARTNER");
					string msg = "";

					foreach (DataRow r in dtPk.Rows)
					{
						DataRow[] rows = dtItem.Select("NO_DSP = " + r["NO_DSP"] + " AND CD_ITEM_PARTNER = '" + r["CD_ITEM_PARTNER"] + "'");

						if (rows.Length > 1)
						{
							msg += "\n"
								+ "No : " + rows[0]["NO_DSP"]
								+ " / Plate No : " + rows[0]["CD_ITEM_PARTNER"]
								+ " / Plate Name : " + rows[0]["NM_ITEM_PARTNER"]
								+ " / U Code : " + rows[0]["UCODE"]
								+ " / K Code : " + rows[0]["KCODE"];
						}
					}				

					ShowMessage("중복아이템 발생!! 기획실로 문의해주세요" + msg);
					return;
				}

				// ********** 그리드에 아이템 추가
				DataTable dtNew = grd라인.DataTable.Clone();
				int index = (int)grd라인.Aggregate(AggregateEnum.Max, "NO_LINE");

				for (int i = 0; i < dtItem.Rows.Count; i++)
				{
					// 콤보박스 아이템 확인
					string unit = dtItem.Rows[i]["UNIT"].ToString().Trim();

					if (cbo단위.GetData().Select("CODE = '" + unit + "'").Length == 0)
					{
						ShowMessage("잘못된 단위가 있습니다 : " + (unit == "" ? "빈칸" : unit));
						return;
					}

					// 추가
					DataRow newRow = dtNew.NewRow();
					newRow["NO_LINE"] = index + i + 1;
					newRow["NO_DSP"] = dtItem.Rows[i]["NO_DSP"];
					newRow["GRP_ITEM"] = engine["CLS_L"];
					newRow["NM_SUBJECT"] = engine["NM_MAKER"] + " " + engine["NM_MODEL"];
					newRow["CD_ITEM_PARTNER"] = dtItem.Rows[i]["CD_ITEM_PARTNER"];
					newRow["NM_ITEM_PARTNER"] = dtItem.Rows[i]["NM_ITEM_PARTNER"];
					newRow["UNIT"] = unit;
					newRow["QT"] = dtItem.Rows[i]["QT"];
					newRow["CD_ITEM"] = dtItem.Rows[i]["CD_ITEM"];
					newRow["UCODE"] = dtItem.Rows[i]["UCODE"];
					newRow["KCODE"] = dtItem.Rows[i]["KCODE"];
					newRow["NO_ENGINE"] = engine["CODE"];
					newRow["TP_BOM"] = "S";
					dtNew.Rows.Add(newRow);
				}

				grd라인.BindingAdd(dtNew, "", false);
				SetGridStyle();
			}
		}

		private void btn두산엑셀_Click(object sender, EventArgs e)
		{
			if (Control.ModifierKeys == Keys.Control)
			{
				FolderBrowserDialog f = new FolderBrowserDialog();

				if (f.ShowDialog() == DialogResult.OK)
				{
					string fileName = "ExcelForm_Inquiry_D.xls";
					WebClient wc = new WebClient();
					wc.DownloadFile(Global.MainFrame.HostURL + "/shared/CZ/" + fileName, f.SelectedPath + @"\" + fileName);
				}
			}
			else
			{
				if (cbo엔진모델.GetValue() == "")
				{
					ShowMessage("엔진모델을 선택하세요");
					return;
				}

				// 엑셀 업로드
				OpenFileDialog f = new OpenFileDialog();
				f.Filter = DD("엑셀 파일") + " (*.xls)|*.xls";

				if (f.ShowDialog() != DialogResult.OK)
					return;

				// 워크플로우 파일 저장
				string fileName = FileMgr.Upload_WF(Quotation.회사코드, Quotation.파일번호, f.FileName, false);

				// 워크플로우 디비 저장
				WorkFlow workflow = new WorkFlow(Quotation.파일번호, "55", Quotation.차수);
				workflow.AddItem("", "", f.SafeFileName, fileName);
				workflow.Save();

				// 엑셀 불러오기
				Excel excel = new Excel();
				DataTable dtExcel = excel.StartLoadExcel(f.FileName, 0, 3).Select("NO_PLATE <> '' AND NM_PLATE <> ''").CopyToDataTable();

				// 엔진 및 기부속 정보
				DataRow engine = ((DataRowView)cbo엔진모델.SelectedItem).Row;

				// 그리드에 아이템 추가
				DataTable dtNew = grd라인.DataTable.Clone();
				int index = (int)grd라인.Aggregate(AggregateEnum.Max, "NO_LINE");
				
				for (int i = 0; i < dtExcel.Rows.Count; i++)
				{
					// 콤보박스 아이템 확인
					string unit = dtExcel.Rows[i]["UNIT"].ToString().Trim();

					if (((DataTable)cbo단위.DataSource).Select("CODE = '" + unit + "'").Length == 0)
					{
						ShowMessage("잘못된 단위가 있습니다 : " + (unit == "" ? "빈칸" : unit));
						return;
					}

					// 추가
					DataRow newRow = dtNew.NewRow();
					newRow["NO_LINE"] = index + i + 1;
					newRow["NO_DSP"] = dtExcel.Rows[i]["NO_DSP"];
					newRow["GRP_ITEM"] = engine["CLS_L"];
					newRow["NM_SUBJECT"] = engine["NM_MAKER"] + " " + engine["NM_MODEL"];
					newRow["CD_ITEM_PARTNER"] = dtExcel.Rows[i]["NO_PLATE"];
					newRow["NM_ITEM_PARTNER"] = dtExcel.Rows[i]["NM_PLATE"];
					newRow["UNIT"] = unit;
					newRow["QT"] = dtExcel.Rows[i]["QT"];
					newRow["CD_ITEM"] = dtExcel.Rows[i]["CD_ITEM"];
					newRow["NO_ENGINE"] = engine["CODE"];
					newRow["TP_BOM"] = "S";
					dtNew.Rows.Add(newRow);
				}

				grd라인.BindingAdd(dtNew, "", false);
				SetGridStyle();
			}
		}

		private void btn두산저장_Click(object sender, EventArgs e)
		{
			// 엔진 및 기부속 정보
			DataRow engine = ((DataRowView)cbo엔진모델.SelectedItem).Row;

			foreach (DataRow row in grd라인.DataTable.Rows)
			{
				if (row.RowState == DataRowState.Added)
				{
					row["GRP_ITEM"] = engine["CLS_L"];
					row["NM_SUBJECT"] = engine["NM_MAKER"] + " " + engine["NM_MODEL"];
					row["NO_ENGINE"] = engine["CODE"];
					row["YN_QLINK"] = "N";
				}
			}
		}

		private void btn엔터리셋_Click(object sender, EventArgs e)
		{
			string query = @"
UPDATE CZ_SA_QTNL
	SET NM_SUBJECT = REPLACE(NM_SUBJECT, CHAR(10), CHAR(13) + CHAR(10))
WHERE 1 = 1
	AND CD_COMPANY = @CD_COMPANY
	AND NO_FILE = @NO_FILE
	AND NM_SUBJECT LIKE '%' + CHAR(10) + '%'
	AND NM_SUBJECT NOT LIKE '%' + CHAR(13) + CHAR(10) + '%'
	AND LEN(REPLACE(NM_SUBJECT, CHAR(10), CHAR(13) + CHAR(10))) <= 1000

UPDATE CZ_SA_QTNL
	SET NM_ITEM_PARTNER = REPLACE(NM_ITEM_PARTNER, CHAR(10), CHAR(13) + CHAR(10))
WHERE 1 = 1
	AND CD_COMPANY = @CD_COMPANY
	AND NO_FILE = @NO_FILE
	AND NM_ITEM_PARTNER LIKE '%' + CHAR(10) + '%'
	AND NM_ITEM_PARTNER NOT LIKE '%' + CHAR(13) + CHAR(10) + '%'
	AND LEN(REPLACE(NM_ITEM_PARTNER, CHAR(10), CHAR(13) + CHAR(10))) <= 1000";

			DBMgr dbm = new DBMgr();
			dbm.Query = query;
			dbm.AddParameter("@CD_COMPANY", Quotation.회사코드);
			dbm.AddParameter("@NO_FILE", Quotation.파일번호);
			dbm.ExecuteNonQuery();

			Search();
		}

		private void btn유형적용_Click(object sender, EventArgs e)
		{
			decimal from = GetTo.Decimal(tbxFrom.Text);
			decimal to = GetTo.Decimal(tbxTo.Text) == 0 ? 1000000 : GetTo.Decimal(tbxTo.Text);	// 종료가 없을 경우 무한대로 늘림

			foreach (DataRow row in grd라인.DataTable.Select("NO_DSP >= " + from + " AND NO_DSP <= " + to))
			{
				row["GRP_ITEM"] = cbo유형적용.SelectedValue;
				row["CD_SUBJECT"] = cbo주제적용.SelectedValue;
			}

			grd라인.AddFinished();
		}

		private void btn주제적용_Click(object sender, EventArgs e)
		{
			decimal from = GetTo.Decimal(tbxFrom.Text);
			decimal to = GetTo.Decimal(tbxTo.Text) == 0 ? 1000000 : GetTo.Decimal(tbxTo.Text);	// 종료가 없을 경우 무한대로 늘림

			foreach (DataRow row in grd라인.DataTable.Select("NO_DSP >= " + from + " AND NO_DSP <= " + to))
				row["NM_SUBJECT"] = tbx주제적용.Text;

			grd라인.AddFinished();
		}
	
		private void btn단위적용_Click(object sender, EventArgs e)
		{
			decimal from = GetTo.Decimal(tbxFrom.Text);
			decimal to = GetTo.Decimal(tbxTo.Text) == 0 ? 1000000 : GetTo.Decimal(tbxTo.Text);	// 종료가 없을 경우 무한대로 늘림

			foreach (DataRow row in grd라인.DataTable.Select("NO_DSP >= " + from + " AND NO_DSP <= " + to))
				row["UNIT"] = cbo단위적용.SelectedValue;

			grd라인.AddFinished();
		}

		#endregion

		#region ==================================================================================================== 그리드 이벤트

		private void grd라인_AfterRowChange(object sender, RangeEventArgs e)
		{
			tbx순번.Text = string.Format("{0:####.##}", grd라인["NO_DSP"]);
		}

		private void grd라인_BeforeDoubleClick(object sender, BeforeMouseDownEventArgs e)
		{
			if (!grd라인.HasNormalRow || grd라인.MouseCol <= 0)
				return;

			if (grd라인.MouseRow < grd라인.Rows.Fixed)
			{
				// BOM 항목이 있는지 확인, BOM이 있으면 정렬 불가
				if (grd라인.DataTable.Select("TP_BOM = 'C'").Length > 0)
				{
					e.Cancel = true;
					ShowMessage("BOM 항목이 있는 경우 정렬할 수 없습니다.");
					return;
				}
			}
		}

		private void grd라인_DoubleClick(object sender, EventArgs e)
		{
			// 헤더클릭
			if (grd라인.MouseRow < grd라인.Rows.Fixed)
			{
				SetGridStyle();
				return;
			}

			// 퀵링크
			if (grd라인.AllowEditing && grd라인.Cols[grd라인.Col].Name == "YN_QLINK")
			{
				DataTable dt = grd라인.DataTable.Select("NO_LINE = " + grd라인["NO_LINE"]).CopyToDataTable();
				P_CZ_SA_INQ_QLINK f = new P_CZ_SA_INQ_QLINK(dt);

				if (f.ShowDialog() == DialogResult.OK)
				{
					grd라인["CD_ITEM"] = f.List["CD_ITEM"];
					grd라인["UCODE"] = f.List["UCODE"];
					grd라인["YN_QLINK"] = "C";

					grd라인.SetCellStyle(grd라인.Row, grd라인.Cols["YN_QLINK"].Index, "QLINK_Y");
				}
			}
		}

		private void grd라인_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == (Keys.Control | Keys.V))
			{
				string[,] clipboard = Util.GetClipboardValues();
				int index = grd라인.Row;	// 시작인덱스 저장 (행이 클립보다 많은 경우는 .Row가 안바뀌지만 클립보드가 더 많은 경우에는 .Row가 바뀌므로 미리 저장함)

				for (int i = 0; i < clipboard.GetLength(0); i++)
				{
					int row = index + i;
					int j = 0;

					for (int col = grd라인.Col; col < grd라인.Cols.Count; col++)
					{
						// 클립보드 넘어가는 순간 제외
						if (j == clipboard.GetLength(1))
							break;

						// 비허용 컬럼
						if (!grd라인.Cols[col].Visible || !grd라인.Cols[col].AllowEditing)
							continue;

						grd라인[row, col] = clipboard[i, j];
						j++;
					}

					// 마지막 행이면 종료
					if (i == clipboard.GetLength(0) - 1)
						break;		

					// 클립보드는 아직 남았는데 그리드의 마지막 행인 경우 행 추가
					if (row == grd라인.Rows.Count - 1)
					{
						// 복사할 현재값 저장
						string itemGroup = cbo유형.GetValue();
						string subjectCode = cbo주제.GetValue();
						string subjectName = tbx주제.Text;
						string unit = (cbo단위.GetValue() == "") ? UnitDefault : cbo단위.GetValue();

						// 행 추가
						grd라인.Rows.Add();
						//flexL.Row = flexL.Rows.Count - 1;

						grd라인[row + 1, "NO_LINE"] = (int)grd라인.Aggregate(AggregateEnum.Max, "NO_LINE") + 1;
						grd라인[row + 1, "NO_DSP"] = (int)grd라인.Aggregate(AggregateEnum.Max, "NO_DSP") + 1;
						grd라인[row + 1, "GRP_ITEM"] = itemGroup;
						grd라인[row + 1, "CD_SUBJECT"] = subjectCode;
						grd라인[row + 1, "NM_SUBJECT"] = subjectName;
						grd라인[row + 1, "UNIT"] = unit;
						grd라인[row + 1, "QT"] = 0;
						grd라인[row + 1, "TP_BOM"] = "S";
						grd라인.SetCellImage(row + 1, grd라인.Cols["CD_ITEM_PARTNER"].Index, Icons.Empty_12x6);						
					}
				}

				grd라인.AddFinished();
			}
		}

		private void Fgd라인_ValidateEdit(object sender, ValidateEditEventArgs e)
		{
			if (grd라인.EditData == "")
				return;

			string colName = grd라인.Cols[e.Col].Name;

			if (colName == "CD_ITEM")
			{
				string itemCode = grd라인.EditData;
				string query = @"
SELECT
	CD_ITEM
FROM MA_PITEM WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + Quotation.회사코드 + @"'
	AND CD_ITEM = '" + itemCode + @"'
	AND YN_USE = 'Y'";

				DataTable dt = DBMgr.GetDataTable(query);

				if (dt.Rows.Count != 1)
				{
					H_CZ_MA_PITEM f = new H_CZ_MA_PITEM(itemCode);

					if (f.ShowDialog() == DialogResult.OK)
					{
						grd라인[colName] = f.ITEM[colName];
					}
					else
					{
						e.Cancel = true;
						grd라인["CD_ITEM"] = grd라인.GetData("CD_ITEM");
						tbx포커스.Focus();
						grd라인.Focus();
					}
				}
			}
			else if (colName == "UCODE")
			{
				string uCode = grd라인.EditData;
				uCode = uCode.Replace("‐", "-");

				string query = @"
SELECT TOP 1
	CD_ITEM
FROM MA_PITEM WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + Quotation.회사코드 + @"'
	AND CLS_ITEM = '009'
	AND (STND_DETAIL_ITEM = '" + uCode + @"' OR UCODE2 = '" + uCode + @"')
	AND YN_USE = 'Y'

UNION

SELECT TOP 1
	CD_ITEM
FROM CZ_MA_QLINK_ITEM
WHERE 1 = 1
	AND CD_COMPANY = '" + Quotation.회사코드 + @"'
	AND REPLACE(REPLACE(UCODE, '%', ''), '[^A-Z0-9]', '') = '" + uCode + @"'

UNION

SELECT TOP 1
	CD_ITEM
FROM CZ_MA_QLINK_ITEM
WHERE 1 = 1
	AND CD_COMPANY = '" + Quotation.회사코드 + @"'
	AND REPLACE(REPLACE(UCODE, '%', ''), '[^A-Z0-9]', '') = '" + uCode.Replace("-", "") + @"'";

				DataTable dt = DBMgr.GetDataTable(query);

				if (dt.Rows.Count > 0)
					grd라인["CD_ITEM"] = dt.Rows[0]["CD_ITEM"];
			}
		}

		public void SetGridStyle()
		{
			// 매입문의서가 1건이라도 있을 경우 된것 안된것 표시 (전부 빨갛게되면 보기 싫으니..)
			int pinqCnt = grd라인.DataTable.Select("YN_PINQ = 'Y'").Length;

			// 이미지 추가 및 빨간색 표시
			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
			{
				// BOM 이미지 추가
				if (grd라인[i, "TP_BOM"].ToString() == "P")
					grd라인.SetCellImage(i, grd라인.Cols["CD_ITEM_PARTNER"].Index, Icons.FolderExpand);
				else if (grd라인[i, "TP_BOM"].ToString() == "S")
					grd라인.SetCellImage(i, grd라인.Cols["CD_ITEM_PARTNER"].Index, Icons.Empty_12x6);
				else if (grd라인[i, "TP_BOM"].ToString() == "C")
					grd라인.SetCellImage(i, grd라인.Cols["CD_ITEM_PARTNER"].Index, Icons.Empty_20x6);

				// 매입견적서 적용 표시 여부
				if (pinqCnt > 0 && GetTo.String(grd라인[i, "YN_PINQ"]) == "N")
					SetGrid.CellRed(grd라인, i, grd라인.Cols["NM_ITEM_PARTNER"].Index);

				// 퀵링크
				if (GetTo.String(grd라인[i, "YN_QLINK"]) != "")
				{
					// P이거나 C이거나 거래처이거나 3가지 중 하나의 상태임
					if (GetTo.String(grd라인[i, "YN_QLINK"]).In("P"))
						grd라인.SetCellStyle(i, grd라인.Cols["YN_QLINK"].Index, "QLINK_P");
					else
						grd라인.SetCellStyle(i, grd라인.Cols["YN_QLINK"].Index, "QLINK_Y");
				}
			}
		}

		#endregion

		#region ==================================================================================================== Search

		public void Search()
		{
			((TabPage)this.Parent).ImageIndex = -1;
			DebugMode debugMode = Control.ModifierKeys == (Keys.Control | Keys.Alt) ? DebugMode.Popup : DebugMode.None;

			// 조회
			DataTable dtHead = DBMgr.GetDataTable("PS_CZ_SA_INQ_H_R3", Quotation.회사코드, Quotation.파일번호);
			DataTable dtLine = DBMgr.GetDataTable("PS_CZ_SA_INQ_L_R3", Quotation.회사코드, Quotation.파일번호);

			// 바인딩
			Header.SetDataTable(dtHead);
			grd라인.Binding = dtLine;			

			// 엔진유형 바인딩
			SetEngineType(Quotation.Imo번호);

			// 마무리
			SetGridStyle();
		}

		#endregion

		#region ==================================================================================================== Save

		public bool 저장()
		{
			DebugMode debug = ModifierKeys == (Keys.Control | Keys.Alt) ? DebugMode.Popup : DebugMode.None;

			// ********** 변경사항 가져오기
			DataTable dtHead = Header.GetChanges();
			DataTable dtLine = grd라인.GetChanges();
			DataTable dtVendor = grd매입처선택.GetChanges();

			#region ********** 유효성 검사
			
			// 라인 필수값 체크
			if (grd라인.DataTable.Select("GRP_ITEM IS NULL OR GRP_ITEM = ''").Length > 0)
			{
				if (Quotation.메시지여부)
					ShowMessage(공통메세지._은는필수입력항목입니다, DD("유형"));
				return false;
			}

			if (grd라인.DataTable.Select("NM_ITEM_PARTNER IS NULL OR NM_ITEM_PARTNER = ''").Length > 0)
			{
				if (Quotation.메시지여부)
					ShowMessage(공통메세지._은는필수입력항목입니다, DD("품목명"));
				return false;
			}

			// 이상 수량 체크
			if (dtLine != null && dtLine.Select("QT >= 1000").Length > 0)
			{
				if (ShowMessage("매우 큰 수량이 존재합니다. 저장 하시겠습니까?", "QY2") != DialogResult.Yes)
					return false;
			}

			#endregion

			// ********** 저장
			// 저장할건 없지만 부모를 위해서 true를 리턴함
			if (dtHead == null && dtLine == null)
				return true;	

			if (dtHead != null)
			{
				// 최초 추가시에는 (데이터 바인딩 전에는) 회사코드랑 파일번호 값이 없으므로 넣어줌
				dtHead.Rows[0]["CD_COMPANY"] = Quotation.회사코드;
				dtHead.Rows[0]["NO_FILE"] = Quotation.파일번호;

				// 담당자가 빈값일 때는 0을 넣음 (null을 넣을 방법도 없고 숫자형이라 공백문자가 안들어가짐)
				if (ctx매출처담당자.CodeValue == "")
					dtHead.Rows[0]["SEQ_ATTN"] = 0;
			}

			if (dtLine != null)
			{
				// 필수 컬럼
				dtLine.Columns["CD_COMPANY"].Expression = "'" + Quotation.회사코드 + "'";
				dtLine.Columns["NO_FILE"].Expression = "'" + Quotation.파일번호 + "'";

				// 라인의 유형(CD_ITEMGRP)을 헤드에 업데이트
				DataRow[] rowChange = dtLine.Select("", "", DataViewRowState.Added | DataViewRowState.ModifiedCurrent);

				if (rowChange.Length > 0)
				{
					if (dtHead == null)
					{
						// 헤드가 변경사항이 없을땐 임시로 만들어줌
						dtHead = new DataTable();
						dtHead.Columns.Add("CD_COMPANY", typeof(string), "'" + Quotation.회사코드 + "'");
						dtHead.Columns.Add("NO_FILE", typeof(string), "'" + Quotation.파일번호 + "'");
					}

					dtHead.Columns.Add("CD_ITEMGRP", typeof(string), "'" + rowChange[0]["GRP_ITEM"] + "'");
				}

				// EZLINK를 위해서 CORE 필드 만듬
				dtLine.Columns.Add("EZDESC_MODEL"); // 구필드
				dtLine.Columns.Add("EZDESC_UCODE");

				dtLine.Columns.Add("DXDESC");
				dtLine.Columns.Add("DXDESC_CORE_SUBJ");
				dtLine.Columns.Add("DXDESC_CORE_ITEM");

				foreach (DataRow row in dtLine.Rows)
				{
					if (row.RowState != DataRowState.Deleted)
					{
						row["EZDESC_MODEL"] = Util.GetDxCode(row["CD_ITEM_PARTNER"] + " ‡ " + row["NM_ITEM_PARTNER"] + " ‡ " + row["NM_SUBJECT"]);
						row["EZDESC_UCODE"] = Util.GetDxCode(row["CD_ITEM_PARTNER"] + " ‡ " + row["NM_ITEM_PARTNER"]);

						row["DXDESC"] = Util.GetDxDescriptionBySimple(row["NM_SUBJECT"] + " " + row["CD_ITEM_PARTNER"] + " " + row["NM_ITEM_PARTNER"]);
						row["DXDESC_CORE_SUBJ"] = Util.GetDxCode(row["NM_SUBJECT"].ToStr());
						row["DXDESC_CORE_ITEM"] = Util.GetDxCode(row["CD_ITEM_PARTNER"] + " ‡ " + row["NM_ITEM_PARTNER"]);
					}
				}
			}

			if (dtVendor != null)
			{
				dtVendor.Columns.Add("CD_COMPANY", typeof(string), "'" + Quotation.회사코드 + "'");
				dtVendor.Columns.Add("NO_FILE", typeof(string), "'" + Quotation.파일번호 + "'");
			}

			// ********** 저장		
			DataTable dtHeadCopy = null;

			if (dtHead != null)
			{
				dtHeadCopy = dtHead.Copy();
				dtHeadCopy.AcceptChanges();

				foreach (DataRow row in dtHeadCopy.Rows)
					row.SetModified();
			}

			string headXml = dtHeadCopy.ToXml();  // 신규는 부모폼에서 Insert 함, 무조건 Update 모드
			string lineXml = dtLine.ToXml();
			string vendorXml = dtVendor.ToXml();
			DBMgr.GetDataTable("PX_CZ_SA_INQ_R4", debug, headXml, lineXml, vendorXml, Quotation.선용영업 ? "Y" : "N");

			// DX테이블 저장
			키워드.견적저장(dtLine);

			Header.AcceptChanges();
			grd라인.AcceptChanges();
			//grd매입처검색.ClearData();
			//grd매입처선택.ClearData();

			#region 매입처키워드 검색하여 자동 매입INQURIY 생성

			//DataTable dtSupplier = DBMgr.GetDataTable("PX_CZ_SA_INQ_REG_EXT", Quotation.CompanyCode, Quotation.FileNumber);

			//if (dtSupplier.Rows.Count > 0)
			//{
			//    string msg = "";

			//    foreach (DataRow row in dtSupplier.Rows)
			//        msg += row["LN_PARTNER"] + "\n";

			//    ShowMessage(msg + "\n매입처가 추가되었습니다.");
			//}

			#endregion

			#region ********** 퀵링크

			if (dtLine != null)
			{
				// ********************* 기자재 (주로 댄포스)
				// 테이블 구조 복사
				DataTable dtQlink = grd라인.DataTable.Clone();

				// 1.신규 추가된 아이템 가져옴
				dtQlink.Merge(new DataView(dtLine, "NO_ENGINE IS NULL", null, DataViewRowState.Added).ToTable());

				// 2.YN_QLINK 값이 N 또는 C로 변경된 아이템 가져옴
				DataTable dtCompleted = GetTo.DataTable(dtLine.Select("NO_ENGINE IS NULL AND YN_QLINK IN ('N', 'C')"));

				if (dtCompleted != null)
					dtQlink.Merge(dtCompleted);

				// U코드 관련된 핵심 단어 추출
				// CORE 필드 추가
				dtQlink.Columns.Add("CORE_WORD_SUBJ");
				dtQlink.Columns.Add("CORE_WORD_ITEM");

				foreach (DataRow row in dtQlink.Rows)
				{
					row["CORE_WORD_SUBJ"] = Util.GetDxDescriptionByCoreCode((string)row["NM_SUBJECT"]);
					row["CORE_WORD_ITEM"] = Util.GetDxDescriptionByCoreCode(row["CD_ITEM_PARTNER"] + " ‡ " + row["NM_ITEM_PARTNER"]);
				}

				// 저장
				string qlinkXml = GetTo.Xml(GetTo.DataTable(dtQlink, "NO_FILE", "NO_LINE", "CD_ITEM", "YN_QLINK", "CORE_WORD_SUBJ", "CORE_WORD_ITEM"));
				DBMgr.GetDataTable("PX_CZ_SA_INQ_QLINK", DebugMode.Print, qlinkXml);

				// ********************* 두산
				if (Quotation.회사코드 == "K200")
				{
					dtQlink.Clear();

					// 신규 추가된 아이템 가져옴
					DataTable dtHsd = GetTo.DataTable(dtLine.Select("NO_ENGINE IS NOT NULL AND YN_QLINK IN ('D', 'N', 'C')"));

					if (dtHsd != null)
						dtQlink.Merge(dtHsd);
					
					dtQlink.Columns["NM_SUBJECT"].ColumnName = "SUBJ";
					dtQlink.Columns["CD_ITEM_PARTNER"].ColumnName = "CODE";
					dtQlink.Columns["NM_ITEM_PARTNER"].ColumnName = "ITEM";

					qlinkXml = GetTo.Xml(GetTo.DataTable(dtQlink, "NO_FILE", "NO_LINE", "CD_ITEM", "YN_QLINK", "SUBJ", "CODE", "ITEM"));
					DBMgr.GetDataTable("PX_CZ_SA_INQ_QLINK_HSD", true, true, qlinkXml);
				}
			}

			#endregion

			return true;
		}

		#endregion

		#region ==================================================================================================== 닫기

		public void Exit()
		{
			grd라인.SaveUserCache("P_CZ_SA_QTN_REG_SINQ");
		}

		#endregion

		#region ==================================================================================================== 기타

		public void SetPartnerPic(string partnerCode, string defaultPic)
		{
			// 바인딩
			DataTable dt = GetDb.PartnerPic(partnerCode, PicType.SalesQuotation);
			
			// 기본값 지정
			if (defaultPic == "")
			{
				// 거래처정보관리에서 대표여부(담당유형)이 Y인값 가져오기
				DataRow[] rows = dt.Select("YN_TYPE = 'Y'");

				if (rows.Length > 0)
				{
					ctx매출처담당자.CodeValue = rows[rows.Length - 1]["SEQ"].ToString();
					ctx매출처담당자.CodeName = rows[rows.Length - 1]["NM_PTR"].ToString();
					Header.CurrentRow["SEQ_ATTN"] = ctx매출처담당자.CodeValue;
				}
			}
			else
			{
				// 파싱에서 Contact 인 값이 넘어오면 이름 비교
				DataRow[] rows = dt.Select("NM_PTR LIKE '%" + defaultPic.Replace("'", "''") + "%'");

				if (rows.Length > 0)
				{
					ctx매출처담당자.CodeValue = rows[rows.Length - 1]["SEQ"].ToString();
					ctx매출처담당자.CodeName = rows[rows.Length - 1]["NM_PTR"].ToString();
					Header.CurrentRow["SEQ_ATTN"] = ctx매출처담당자.CodeValue;
				}
			}
		}

		public void SetEngineType(string imoNumber)
		{
			// 엔진유형 바인딩
			DataTable dt = DBMgr.GetDataTable("PS_CZ_MA_HULL_ENGINE", new object[] { Quotation.Imo번호 });
			dt.Columns["NO_ENGINE"].ColumnName = "CODE";
			dt.Columns["NM_DESC"].ColumnName = "NAME";

			cbo엔진모델.DataSource = dt;
		}

		public void SetSupplier(string remark)
		{
			remark = remark.Replace("(", " (");
			remark = Regex.Replace(remark, @"\s+", " ");
			remark = remark.Replace(" ", ",");
			remark = remark.Replace("-", ",");
			remark = remark.Replace("_", ",");

			string query = @"

SELECT
	  CD_PARTNER	AS CD_PARTNER
	, LN_PARTNER	AS NM_PARTNER
	, NM_CEO		AS NM_CEO
	, NO_COMPANY	AS NO_COMPANY
FROM MA_PARTNER	WITH(NOLOCK)
WHERE 1 != 1";

			foreach (string s in remark.Split(','))
			{
				string name = s;

				if (name.IndexOf("현대웹") >= 0)
					continue;

				name = name.Replace("(주)", "");					// (주) 없애기
				name = name.Replace("(유)", "");					// (유) 없애기
				name = Regex.Replace(name, "[^A-z가-힣]", "");	// 숫자, 특수문자 없애기

				if (name.Length >= 2)
					query += @"
SELECT
	  A.CD_PARTNER	AS CD_PARTNER
	, A.LN_PARTNER	AS NM_PARTNER
	, A.NM_CEO		AS NM_CEO
	, SUBSTRING(A.NO_COMPANY, 1, 3) + '-' + SUBSTRING(A.NO_COMPANY, 4, 2) + '-' + SUBSTRING(A.NO_COMPANY, 6, 5) AS NO_COMPANY
FROM MA_PARTNER	AS A WITH(NOLOCK)
JOIN
(
	SELECT
		  CD_COMPANY
		, CD_PARTNER
	FROM CZ_PU_QTNH WITH(NOLOCK)
	WHERE DT_INQ >= '20180503'
	GROUP BY CD_COMPANY, CD_PARTNER
	HAVING COUNT(1) > 5
)				AS B ON A.CD_COMPANY = B.CD_COMPANY AND A.CD_PARTNER = B.CD_PARTNER
WHERE 1 = 1
	AND A.CD_COMPANY = '" + Quotation.회사코드 + @"'
	AND LEFT(A.CD_PARTNER, 2) != 'AT'
	AND A.CD_PARTNER NOT IN ('00799', '11823')
	AND A.FG_PARTNER IN ('110', '200', '400')	-- 110:대리점, 200:메이커, 400:공급사
	AND A.USE_YN = 'Y'
	AND A.LN_PARTNER LIKE '%" + name + "%'";
			}

			// 바인딩 고고
			DataSet ds = DBMgr.GetDataSet(query);
			DataTable dtSelected = ds.Tables[0].Clone();
			DataTable dtSearched = ds.Tables[0].Clone();

			// 선용의 경우 DINTEC(10493)을 디폴트로 추가
			if (Quotation.회사코드 == "K100" && Quotation.파일번호.Left(2).In("SB", "NS"))
			{
				//dtSelected.Rows.Add("10493", "DINTEC CO., LTD.", "", "999-99-99999");
				dtSelected.Rows.Add("STOCKGS", "STOCK (선용)", "", "999-99-99999");
			}
			else
			{
				foreach (DataTable dt in ds.Tables)
				{
					if (dt.Rows.Count == 1)
						dtSelected.Merge(dt);
					else
						dtSearched.Merge(dt);
				}
			}
			
			grd매입처검색.BindingAdd(dtSearched.DefaultView.ToTable(true, "CD_PARTNER", "NM_PARTNER", "NM_CEO", "NO_COMPANY"), "");
			grd매입처선택.BindingAdd(dtSelected.DefaultView.ToTable(true, "CD_PARTNER", "NM_PARTNER", "NM_CEO", "NO_COMPANY"), "", false);

			if (dtSelected.Rows.Count + dtSearched.Rows.Count > 0 && Quotation.부품영업)
				tab메인.SelectedTab = tab매입처;
		}

		public void SearchSupplier(string companyCode, string supplierName)
		{
			string query = @"
SELECT
	  A.CD_PARTNER	AS CD_PARTNER
	, A.LN_PARTNER	AS NM_PARTNER
	, A.NM_CEO		AS NM_CEO
	, SUBSTRING(A.NO_COMPANY, 1, 3) + '-' + SUBSTRING(A.NO_COMPANY, 4, 2) + '-' + SUBSTRING(A.NO_COMPANY, 6, 5) AS NO_COMPANY
FROM MA_PARTNER	AS A WITH(NOLOCK)
WHERE 1 = 1
	AND A.CD_COMPANY = '" + companyCode + @"'
	AND LEFT(A.CD_PARTNER, 2) != 'AT'
	AND A.CD_PARTNER NOT IN ('00799', '11823')
	AND A.FG_PARTNER IN ('110', '200', '400')
	AND A.USE_YN = 'Y'
	AND A.LN_PARTNER LIKE '%" + supplierName + "%'";

			DataTable dt = DBMgr.GetDataTable(query);
			grd매입처검색.Binding = dt;

			// 바인딩 초기화인 경우
			if (grd매입처선택.DataTable == null)
				grd매입처선택.Binding = dt.Clone();
		}

		#endregion

		
	}
}




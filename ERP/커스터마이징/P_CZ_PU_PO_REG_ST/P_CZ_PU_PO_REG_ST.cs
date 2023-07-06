using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using Duzon.Common.Forms;
using Duzon.Common.BpControls;

using Dass.FlexGrid;
using C1.Win.C1FlexGrid;

using DX;

using System.IO;    // 삭제 가능할듯
using Dintec;       // 삭제 가능할듯
using System.Collections.Generic;

namespace cz
{
	public partial class P_CZ_PU_PO_REG_ST : PageBase
	{			
		헤더 헤더 = new 헤더();
		bool 입고여부 = false;
		string LinkedNumber = "";
		
		#region ===================================================================================================== Property

		public string 발주번호
		{
			get => tbx발주번호.Text.트림();
			set => tbx발주번호.Text = value.트림();
		}

		public string 주문번호
		{
			get => tbx주문번호.Text.트림();
			set => tbx주문번호.Text = value.트림();
		}

		public decimal 환율
		{
			get => cur환율.DecimalValue;
			set => cur환율.DecimalValue = value;
		}
			
		public decimal 부가세율 => GetTo.Int(GetDb.CodeFlag1(cbo과세구분));

		private int 표시형식단가용
		{
			get
			{
				int roundCode;

				if (rdo표시형식단가.Checked)
				{
					roundCode = GetTo.Int(GetDb.CodeFlag1(cbo표시형식));
				}
				else
				{
					if (cbo통화구분.GetValue() == "000")
						roundCode = 0;
					else
						roundCode = 2;
				}

				return roundCode;
			}
		}

		private int 표시형식금액용
		{
			get
			{
				// 금액 표시형식은 무조건 표기 표시형식을 따름 (단가, 금액 라디오 무의미, 단가 * 수량해서 나온거 이므로 단가 체크되있어도 단가 표시형식을 따름)
				return GetTo.Int(GetDb.CodeFlag1(cbo표시형식));
			}
		}

		private bool 선용여부 => ctx대분류.값() == "GS";
		
		#endregion

		#region ==================================================================================================== 생성자 == CONSTRUCTOR

		public P_CZ_PU_PO_REG_ST()
		{
			InitializeComponent();
			StartUp.Certify(this);
		}

		#endregion

		#region ==================================================================================================== 초기화 == INIT

		protected override void InitLoad()
		{
			this.페이지초기화();
			tbx발주번호.엔터검색();

			// 콤보
			DataSet ds = 코드.코드관리("MA_B000005", "MA_B000046", "PU_C000014", "TR_IM00003", "TR_IM00011", "CZ_PU00012", "CZ_SA00014");
			cbo통화구분.바인딩(ds.Tables[0], true);
			cbo과세구분.바인딩(ds.Tables[1], true);
			cbo지급조건.바인딩(ds.Tables[2], true);
			cbo선적조건.바인딩(ds.Tables[3], true);
			cbo포장형태.바인딩(ds.Tables[4], true);
			cbo납품장소.바인딩(ds.Tables[5], true);
			cbo표시형식.바인딩(ds.Tables[6], false);
			cbo발주유형.바인딩(코드.발주유형("재고"), true);

			InitGrid();
			InitEvent();

			헤더.컨테이너(lay헤드);
			헤더.기본키(tbx발주번호);
		}

		private void InitGrid()
		{
			MainGrids = new FlexGrid[] { grd라인 };

			grd라인.세팅시작(2);
		
			grd라인.컬럼세팅("NO_PO"						, "발주번호"		, false);
			grd라인.컬럼세팅("NO_LINE"					, "항번"			, false);
			grd라인.컬럼세팅("CD_ITEM"					, "재고코드"		, 100	, 정렬.가운데);
			grd라인.컬럼세팅("NM_ITEM"					, "재고명"		, 250);
			grd라인.컬럼세팅("NO_PART"					, "파트번호"		, 120);
			grd라인.컬럼세팅("CD_UNIT"					, "단위"			, 70	, 정렬.가운데);
			grd라인.컬럼세팅("QT_PO"						, "수량"			, 45	, 포맷.수량);
			grd라인.컬럼세팅("QT_RCV"						, "(입고)"		, 45	, 포맷.수량);
			
			grd라인.컬럼세팅("UM_EX_E"	, "매입견적단가"	, "외화단가"		, 80	, 포맷.외화단가);
			grd라인.컬럼세팅("AM_EX_E"	, "매입견적단가"	, "외화금액"		, 80	, 포맷.외화단가);
			grd라인.컬럼세팅("UM_KR_E"	, "매입견적단가"	, "원화단가"		, 80	, 포맷.원화단가);
			grd라인.컬럼세팅("AM_KR_E"	, "매입견적단가"	, "원화금액"		, 80	, 포맷.원화단가);
			
			grd라인.컬럼세팅("RT_DC"						, "DC\n(%)"		, 45	, 포맷.비율);
			grd라인.컬럼세팅("UM_EX"		, "매입단가"		, "외화단가"		, 80	, 포맷.외화단가);
			grd라인.컬럼세팅("AM_EX"		, "매입단가"		, "외화금액"		, 80	, 포맷.외화단가);
			grd라인.컬럼세팅("UM"			, "매입단가"		, "원화단가"		, 80	, 포맷.원화단가);
			grd라인.컬럼세팅("AM"			, "매입단가"		, "원화금액"		, 80	, 포맷.원화단가);
			grd라인.컬럼세팅("VAT"		, "매입단가"		, "부가세"		, 80	, 포맷.원화단가);
			
			grd라인.컬럼세팅("LT"							, "납기"			, 50	, 포맷.원화단가);
			grd라인.컬럼세팅("DC1"						, "비고1"		, 140);
			grd라인.컬럼세팅("DC2"						, "비고2"		, 230);
			grd라인.컬럼세팅("DC3"						, "비고3"		, 300);
			grd라인.컬럼세팅("NO_BL"						, "운송장번호"	, 150);
			grd라인.컬럼세팅("YN_GULL"					, "H"			, 30	, 포맷.체크);
			
			grd라인.기본키("NO_LINE");
			grd라인.필수값("CD_ITEM", "QT_PO", "LT");
			grd라인.데이터맵("CD_UNIT", 코드.단위());

			grd라인.에디트컬럼("CD_ITEM", "CD_UNIT", "QT_PO", "UM_EX_E", "RT_DC", "UM_EX", "LT", "DC1", "DC2", "DC3", "NO_BL");
			grd라인.합계컬럼스타일("AM_EX_E", "AM_KR_E", "AM_EX", "AM");
			grd라인.합계제외컬럼("UM_EX_E", "UM_KR_E", "RT_DC", "UM_EX", "UM", "LT");
			grd라인.세팅종료("22.09.16.01", true, true);

			grd라인.복사붙여넣기(Grd라인_AfterEdit);
		}

		protected override void InitPaint()
		{
			초기화();

			//if (LinkedNumber != "")
			//{
			//	발주번호 = LinkedNumber;
			//	OnToolBarSearchButtonClicked(null, null);
			//}

			Focus();
			tbx발주번호.Focus();

			// 테스트
			//발주번호 = "ST22000264";
		}

		protected override bool IsChanged() => base.IsChanged() || ToolBarSaveButtonEnabled;

		#endregion

		private void 초기화()
		{
			ToolBarSaveButtonEnabled = false;
			입고여부 = false;

			// ********** 패널 사용처리
			pnl발주번호.사용(true);
			pnl발주일자.사용(true);
			pnl담당자.사용(true);
			pnl매입처.사용(true);

			pnl환율.사용(true);
			pnl발주유형.사용(true);
			pnl과세구분.사용(true);

			pnl지급조건.사용(true);
			pnl할인.사용(true);
			pnl납기.사용(true);
			pnl표시형식.사용(true);			

			// ********** 헤더 초기화
			헤더.초기화();
			헤더.변경이벤트중지();

			// 기본값
			dtp발주일자.기본값();
			ctx담당자.기본값();
			ctx구매그룹.기본값();

			cbo통화구분.값("000");		// KRW
			Cbo통화구분_SelectionChangeCommitted(null, null); // 통화 이벤트 강제 실행
			cbo발주유형.값(상수.회사코드.발생("K") ? "1300" : "2300");	// 1300:국내,2300:해외
			tbx인도기간.Text = "AS BELOW";
			cbo선적조건.값("EXW");
			cbo포장형태.값("002");   // STANDARD 어쩌고

			헤더.변경이벤트시작();

			// ********** 그리드
			grd라인.초기화();

			// ********** 초기화 완료
			tbx발주번호.Focus();			
		}

		#region ==================================================================================================== 이벤트 == EVENT

		private void InitEvent()
		{
			btn메일발송.Click += Btn메일발송_Click;
			btn재고라벨.Click += Btn재고라벨_Click;

			ctx담당자.CodeChanged += Ctx담당자_CodeChanged;
			ctx매입처.CodeChanged += Ctx매입처_CodeChanged;
			ctx호선.QueryAfter += Ctx호선_QueryAfter;

			cbo통화구분.SelectionChangeCommitted += Cbo통화구분_SelectionChangeCommitted;
			cbo과세구분.SelectionChangeCommitted += Cbo과세구분_SelectionChangeCommitted;

			ctx대분류.QueryBefore += Ctx대분류_QueryBefore;
			ctx중분류.QueryBefore += Ctx중분류_QueryBefore;
			ctx소분류.QueryBefore += Ctx소분류_QueryBefore;
				
			ctx대분류.CodeChanged += Ctx대분류_CodeChanged;
			ctx중분류.CodeChanged += Ctx중분류_CodeChanged;

			btn할인.Click += Btn할인_Click;
			btn납기.Click += Btn납기_Click;
			btn표시형식.Click += Btn표시형식_Click;
			lbl첨부파일.DoubleClick += Lbl첨부파일_DoubleClick;

			btn현대엑셀.Click += btn현대엑셀_Click;
			btn추가.Click += Btn추가_Click;
			btn비용추가.Click +=Btn비용추가_Click;
			btn삭제.Click += Btn삭제_Click;
			
			grd라인.AfterEdit += Grd라인_AfterEdit;
		}

		private void Btn메일발송_Click(object sender, EventArgs e)
		{			
			DataTable dtEmp = 코드.사원(ctx담당자.값());
			DataTable dtVen = 코드.거래처(ctx매입처.값());
			DataTable dtPic = 코드.거래처담당자(ctx매입처.값(), 담당자구분.매입재고);

			// 메일 수발신 주소
			string 보내는사람 = dtEmp.첫행("NM_EMAIL").문자(); ;
			string 받는사람 = dtPic.존재() ? dtPic.첫행("NM_EMAIL").문자() : "";
			string 참조 = 보내는사람;

			// 제목
			string 제목 = "";
			string 날짜 = dtp발주일자.값().Substring(4, 2) + "/" + dtp발주일자.값().Substring(6, 2);

			if		(상수.회사코드 == "K100") 제목 = 날짜 + " DINTEC - ORDER SHEET(" + 발주번호 + ")";
			else if (상수.회사코드 == "K200") 제목 = 날짜 + " DUBHECO - ORDER SHEET(" + 발주번호 + ")";
			else if (상수.회사코드 == "S100") 제목 = "DINTEC - ORDER SHEET(" + 발주번호 + ")";

			// 첨부파일 (발주서, 입고라벨)		
			string 발주서 = 발주서다운로드(1, false);
			List<string> 첨부파일 = new List<string> { 발주서 + "|LOCAL" };

			// 입고라벨
			string 입고라벨 = 발주서다운로드(3, false);
			첨부파일.Add(입고라벨 + "|LOCAL");

			// 선용인 경우 블라인드 발주서 첨부
			if (선용여부)
			{
				발주서 = 발주서다운로드(2, false);
				첨부파일.Add(발주서 + "|LOCAL");
			}

			// 테크로스만 특별 첨부
			//if (dtHead.Rows[0]["CD_PARTNER"].ToString() == "17747")
			//{
			//	DataTable dtTcrs = SQL.GetDataTable("PS_CZ_PU_PO_REG_TCRS", dtHead.Rows[0]["CD_COMPANY"], dtHead.Rows[0]["NO_PO"]);

			//	string txt = PATH.GetTempPath() + @"\" + dtHead.Rows[0]["NO_PO"] + ".txt";
			//	System.IO.File.WriteAllText(txt, dtTcrs.Rows[0][0].ToString());
			//	files.Add(txt + "|LOCAL");			
			//}

			// 서명
			string 서명 = 메일서명.발주서(dtEmp, dtVen.첫행("LANG").문자());

			// 메일발송 팝업
			P_CZ_MA_EMAIL_SUB f = new P_CZ_MA_EMAIL_SUB(보내는사람, 받는사람, 참조, "", 제목, 첨부파일.ToArray(), null, 서명, "", "", false);

			if (f.ShowDialog() == DialogResult.OK)
			{
				try
				{
					// 발송에 성공한 경우 수신자, 발송일 업데이트
					string query = @"
DECLARE @DT_SEND	NVARCHAR(14) = NEOE.NOW()
UPDATE PU_POH SET DT_SEND = @DT_SEND, MAIL_SEND = '" + 받는사람 + "' WHERE CD_COMPANY = '" + 상수.회사코드 + "' AND NO_PO = '" + 발주번호 + @"'
SELECT FORMAT(NEOE.DATETIME(@DT_SEND), 'yyyy-MM-dd HH:mm')";

					DataTable dt = 디비.결과(query);
					lbl보낸일자.Text = dt.첫행(0).문자();
				}
				catch (Exception ex)
				{
					메시지.오류알람(ex);
				}
			}
		}

		private void Ctx담당자_CodeChanged(object sender, EventArgs e)
		{
			if (코드.영업구매그룹(ctx담당자.값()) is DataTable dt && dt.존재())
			{
				ctx구매그룹.값(dt.Rows[0]["CD_PURGRP"]);
				ctx구매그룹.글(dt.Rows[0]["NM_PURGRP"]);
			}
			else
			{
				ctx구매그룹.값("");
				ctx구매그룹.글("");
			}
		}

		private void Ctx매입처_CodeChanged(object sender, EventArgs e)
		{
			if (코드.거래처(ctx매입처.값()) is DataTable dt && dt.존재())
			{
				cbo통화구분.값(dt.첫행("CD_EXCH_P").문자() == "" ? "000" : dt.첫행("CD_EXCH_P"));
				cbo과세구분.값(dt.첫행("FG_TAX"));
				cbo지급조건.값(dt.첫행("FG_PAYMENT"));

				Cbo통화구분_SelectionChangeCommitted(null, null);
				Cbo과세구분_SelectionChangeCommitted(null, null);
				pnl지급조건.사용(false);
			}
			else
			{
				cbo통화구분.값("000");
				cbo과세구분.값("");
				cbo지급조건.값("");

				Cbo통화구분_SelectionChangeCommitted(null, null);
				pnl지급조건.사용(true);
			}
		}

		private void Ctx호선_QueryAfter(object sender, BpQueryArgs e) => ctx호선.CodeName = e.HelpReturn.Rows[0]["NM_VESSEL"] + " (" + e.HelpReturn.Rows[0]["NO_HULL"] + ")";

		private void Cbo통화구분_SelectionChangeCommitted(object sender, EventArgs e)
		{
			환율 = 코드.환율(dtp발주일자.Text, cbo통화구분.값(), "P");

			if (cbo통화구분.값() == "000")
			{
				cbo표시형식.값("5");
				rdo표시형식단가.체크();
			}
			else
			{
				cbo표시형식.값("3");
				rdo표시형식금액.체크();
			}
		}

		private void Cbo과세구분_SelectionChangeCommitted(object sender, EventArgs e)
		{
			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
				부가세계산(i);
		}


		private void Ctx대분류_QueryBefore(object sender, BpQueryArgs e) => e.HelpParam.P41_CD_FIELD1 = "MA_B000030";

		private void Ctx중분류_QueryBefore(object sender, BpQueryArgs e) { e.HelpParam.P41_CD_FIELD1 = "MA_B000031"; e.HelpParam.P42_CD_FIELD2 = ctx대분류.값(); }

		private void Ctx소분류_QueryBefore(object sender, BpQueryArgs e) { e.HelpParam.P41_CD_FIELD1 = "MA_B000032"; e.HelpParam.P42_CD_FIELD2 = ctx중분류.값(); }


		private void Ctx대분류_CodeChanged(object sender, EventArgs e)
		{
			ctx중분류.초기화();
			ctx소분류.초기화();

			// 엔진 관련일 경우 중분류 자동 선택
			if (ctx대분류.값().포함("ME", "HE", "AE"))
			{
				ctx중분류.값("H" + ctx대분류.값());
				ctx중분류.글(코드.코드관리("MA_B000031").선택("CODE = '" + ctx중분류.값() + "'")[0]["NAME"]);
			}
		}

		private void Ctx중분류_CodeChanged(object sender, EventArgs e)
		{
			ctx소분류.초기화();
		}

		private void Btn할인_Click(object sender, EventArgs e)
		{
			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
			{
				grd라인[i, "RT_DC"] = cur할인율.DecimalValue;
				행계산(i, "RT_DC");
			}
		}

		private void Btn납기_Click(object sender, EventArgs e)
		{
			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
			{
				if (grd라인[i, "NO_LINE"].정수() < 90000)
					grd라인[i, "LT"] = cur납기.DecimalValue;
			}
		}

		private void Btn표시형식_Click(object sender, EventArgs e)
		{
			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
			{
				행계산(i, "");
			}
		}

		private void Lbl첨부파일_DoubleClick(object sender, EventArgs e) => 발주서다운로드(1, true);


		private void Btn재고라벨_Click(object sender, EventArgs e)
		{			
			// ********** 라벨 출력 조건
			string fontName = "Agency FB";
			Font font = new Font(fontName, 12);

			if (font.Name != fontName)
			{
				ShowMessage("폰트가 설치되지 않았습니다.");
				return;
			}

			// ********** 저장
			DataTable dtHead = DBMgr.GetDataTable("PS_CZ_PU_PO_REG_ST_H_R2", "K100", tbx발주번호.Text);
			DataTable dtLine = DBMgr.GetDataTable("PS_CZ_PU_PO_REG_ST_L_R2", "K100", tbx발주번호.Text);
			string CompanyCode = "K100";
			string RefNumber = tbx발주번호.Text;

			// 저장할 로컬 임시 경로
			string path = Application.StartupPath + @"\temp\";

			// Html 스타일시트
			string headHtml = @"
  <style type='text/css'>
    td, div { font-family:Agency FB; }
  
    .page  { margin:25px auto; table-layout:fixed; }
    .cell  { width:480px; height:347px; }
    .space { width:30px; }
  
    .label       { margin:0 auto; }
    .label .col1 { width:120px; }
    .label .col2 { width:10px; }
    .label .col3 { width:210px; }	/* 넓이 조정할때 하위 div 넓이도 같이 조정해야 함 */
    .label .col4 { width:95px; }
  
    .label .logo     { height:67px; }
    .label .disp-num { height:28px; }
    .label .title    { height:21px; }
  
    .label .logo div   { width:110px; }
    .label .logo img   { width:100px; }
    .label .logo-dubheco { margin-bottom:10px; }
    .label .logo-dintec img { height:45px; }
    .label .logo-dubheco  img { height:30px; }

    .label .company     { font-size:20pt; font-weight:bold; }
    .label .disp-num    { font-size:20pt; font-weight:bold; }
    .label .qr-code     { text-align:right; }
    .label .qr-code img { width:90px; height:90px; }
    .label .title       { font-size:14pt; vertical-align:top; line-height:21px; }
    .label .conts       { font-size:14pt; height:19px; width:300px; word-break: break-all; overflow:hidden; }	/* col3 + col4 - 5 */
    .label .conts-code  { font-size:14pt; height:19px; width:190px; word-break: break-all; overflow:hidden; }	/* col3 - 20 */
    .label .conts-name  { font-size:14pt; height:55px; width:300px; word-break: break-all; overflow:hidden; }	/* col3 + col4 - 5 */
  
    /*
    .cell               { border:solid 1px red; }
    .label td           { border:solid 1px blue; }
    .label .conts       { border:solid 1px green; }
    .label .conts-code  { border:solid 1px green; }
    .label .conts-name  { border:solid 1px green; }
    */
  </style>";

			// Html 바디
			string bodyHtml = "";
			int pageNum = 1;    // 시작 페이지 넘버
			int pageSize = 8;   // 한페이지당 라벨 갯수

			while (true)
			{
				if ((pageNum - 1) * pageSize >= dtLine.Rows.Count)
					break;

				// 라벨 갯수만큼 신규 테이블 만들기
				DataTable dtPage = dtLine.Rows.Cast<DataRow>().Skip((pageNum - 1) * pageSize).Take(pageSize).CopyToDataTable();
				pageNum++;

				// Html GoGo!!
				bodyHtml += @"
  <div>
    <table class='page'>";
				for (int j = 0; j < dtPage.Rows.Count; j++)
				{
					// ********** QR코드 이미지 생성 (대신 pda에서 사용하기 위해 @대신 \n을 씀)
					string qrCode = ""
+ "QTY:" + string.Format("{0:###0.##}", dtPage.Rows[j]["QT_PO"]) + "\n"
+ "C/CODE:" + "" + "\n"
+ "D/CODE:" + "" + "/" + "" + "/S"; // S:Supplier

					//string qrCode = "Q:" + string.Format("{0:###0.##}", dtPage.Rows[j]["QT_PO"]) + "C:" + CompanyCode + "D:" + dtHead.Rows[0]["NO_PO"] + "/" + dtPage.Rows[j]["NO_LINE"] + "/S";	// S:Supplier

					string qrFile = dtHead.Rows[0]["NO_PO"] + "_" + dtPage.Rows[j]["NO_LINE"] + ".png";
					QRCode.Generate(qrCode.Trim(), path + qrFile);

					// ********** 라벨 Html
					string companyName = "";
					string logoHtml = "";

					if (CompanyCode == "K100")
					{
						companyName = "DINTEC CO., LTD";
						logoHtml = "<div class='logo-dintec'><img src='http://www.dintec.co.kr/common/img/common/logo.png' /></div>";
					}
					else if (CompanyCode == "K200")
					{
						companyName = "DUBHE CO., LTD";
						logoHtml = "<div class='logo-dubheco'><img src='http://www.dintec.co.kr/common/img/common/logo_dubheco3.png' /></div>";
					}

					// 기자재인 경우 Subject 추가, 선용은 Offer 추가
					string subjectHtml = "";
					string offerHtml = "";

					subjectHtml = @"
              <tr>
                <td class='title'>SUBJECT NAME</td>
                <td class='title'>:</td>
                <td colspan='2'><div class='conts'>" + Html.Encode(dtPage.Rows[j]["UCODE"]) + @"</div></td>
              </tr>";


					// 라벨 본문
					string labelHtml = @"
          <div>
            <table class='label'>
              <colgroup>
                <col class='col1' />
                <col class='col2' />
                <col class='col3' />
                <col class='col4' />
              </colgroup>
              <tr>
                <td class='logo'>" + logoHtml + @"</td>
                <td class='company' colspan='2'>" + companyName + @"</td>
                <td class='qr-code' rowspan='2'><img src='file:///" + path.Replace(@"\", "/") + qrFile + @"' /></td>
              </tr>
              <tr>
                <td class='disp-num'>No. " + string.Format("{0:###0.##}", dtPage.Rows[j]["NO_LINE"]) + @"</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
              </tr>
              <tr>
                <td class='title'>YOUR ORDER NO.</td>
                <td class='title'>:</td>
                <td colspan='2'><div class='conts'>" + "" + @"</div></td>
              </tr>
              <tr>
                <td class='title'>OUR REF NO.</td>
                <td class='title'>:</td>
                <td colspan='2'><div class='conts'>" + Html.Encode(RefNumber) + @"</div></td>
              </tr>
              <tr>
                <td class='title'>BUYER</td>
                <td class='title'>:</td>
                <td colspan='2'><div class='conts'>" + "" + @"</div></td>
              </tr>
              <tr>
                <td class='title'>VESSEL</td>
                <td class='title'>:</td>
                <td colspan='2'><div class='conts'>" + "" + @"</div></td>
              </tr>" + subjectHtml + @"
              <tr>
                <td class='title'>ITEM CODE</td>
                <td class='title'>:</td>
                <td><div class='conts-code'>" + Html.Encode(dtPage.Rows[j]["CD_ITEM"]) + @"</div></td>
                <td class='title'>QTY : " + string.Format("{0:#,##0}", dtPage.Rows[j]["QT_PO"]) + @"</td>
              </tr>
              <tr>
                <td class='title'>ITEM NAME</td>
                <td class='title'>:</td>
                <td colspan='2'><div class='conts-name'>" + Html.Encode(dtPage.Rows[j]["DC2"]) + @"</div></td>
              </tr>" + offerHtml + @"
            </table>
          </div>";

					// 짝수행(0,2,4) / 홀수행(1,3,5)에 따라 태그 생성
					if (j % 2 == 0)
					{
						bodyHtml += @"
      <tr>
        <td class='cell'>" + labelHtml + @"
        </td>
        <td class='space'>
          &nbsp;
        </td>";

						if (j == dtPage.Rows.Count - 1)
						{
							// 짝수행이 마지막으로 끝나면 빈값 홀수행 생성
							bodyHtml += @"
        <td class='cell'>
          &nbsp;
        </td>
      </tr>";
						}
					}
					else
					{
						bodyHtml += @"
        <td class='cell'>" + labelHtml + @"
        </td>
      </tr>";
					}
				}

				bodyHtml += @"
    </table>
  </div>
  <p style='page-break-before: always;'>
";
			}

			// ********** 파일 만들기
			MsgControl.ShowMsg("라벨 저장중입니다. \r\n잠시만 기다려주세요!");

			string purchasNumber = dtHead.Rows[0]["NO_PO"].ToString();
			string partnerCode = dtHead.Rows[0]["CD_PARTNER"].ToString();

			// Html, Pdf 파일 만들기
			string fileName = string.Format("라벨_{0}_{1}", purchasNumber, partnerCode);
			string html = Html.MakeHtml(headHtml, bodyHtml);
			Html.MakeFile(path + fileName + ".htm", html);
			Html.ConvertPdf(path + fileName + ".pdf", html);

			// 파일 저장
			string localFile = path + fileName + ".pdf";
			string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\" + tbx발주번호.Text + @"\";
			DIR.CreateDirectory(desktopPath);
			File.Copy(localFile, desktopPath + fileName + ".pdf");






			// ********** 발주서 다운로드			
			// ERP 서버에 업로드
			string 대상경로 = 상수.회사코드 + "/" + 발주번호;
			string 결과파일 = 파일.업로드(localFile, 대상경로, false);

			// DB 업데이트
			발주서저장(파일.파일이름(결과파일), 3);
			


			MsgControl.CloseMsg();
		}

		
		#endregion

		#region ==================================================================================================== 버튼 이벤트

		private void Btn추가_Click(object sender, EventArgs e)
		{
			// 행 추가
			grd라인.행추가();			
			grd라인["NO_LINE"] = grd라인.최대값("NO_LINE") + 1;
			grd라인.행추가완료();

			// 포커스 이동
			grd라인.Col = grd라인.컬럼인덱스("CD_ITEM");
			grd라인.Focus();
		}

		private void Btn비용추가_Click(object sender, EventArgs e)
		{
			H_CZ_EXTRA_COST f = new H_CZ_EXTRA_COST();

			if (f.ShowDialog() == DialogResult.OK)
			{
				int maxLineNumber = (int)grd라인.Aggregate(AggregateEnum.Max, "NO_LINE");

				if (maxLineNumber > 90000)
					maxLineNumber++;
				else
					maxLineNumber = 90001;

				grd라인.Rows.Add();
				grd라인.Row = grd라인.Rows.Count - 1;
				grd라인["NO_PO"] = 발주번호;
				grd라인["NO_LINE"] = maxLineNumber;
				grd라인["NM_ITEM_PARTNER"] = f.SelectedItem["NAME"];
				grd라인["CD_ITEM"] = f.SelectedItem["CODE"];
				grd라인["NM_ITEM"] = f.SelectedItem["NAME"];
				grd라인["QT_PO"] = 1;
				grd라인["RT_DC"] = 0;
				grd라인["LT"] = 0;
				grd라인.AddFinished();

				grd라인.Col = grd라인.Cols["UM_EX_E"].Index;
				grd라인.Focus();
			}
		}

		private void Btn삭제_Click(object sender, EventArgs e)
		{
			try
			{
				if (grd라인.Row < grd라인.Rows.Fixed)	메시지.경고발생("아이템이 선택되지 않았습니다.");				
				if (grd라인["QT_RCV"].정수() > 0)		메시지.경고발생("이미 입고되었습니다.");
				
				grd라인.Rows.Remove(grd라인.Row);
			}
			catch (Exception ex)
			{
				메시지.오류알람(ex);
			}
		}


		private void btn현대엑셀_Click(object sender, EventArgs e)
		{
			bool popupDebug = false;

			if (Control.ModifierKeys == Keys.Control)
				popupDebug = true;

			// 엑셀파일 선택
			OpenFileDialog f = new OpenFileDialog();
			f.Filter = Global.MainFrame.DD("엑셀 파일") + "|*.xls;*.xlsx";

			if (f.ShowDialog() != DialogResult.OK)
				return;

			// 엑셀업로드
			try
			{
				MsgControl.ShowMsg(DD("엑셀 업로드 중입니다."));

				// 엑셀 → DataTable 변환
				string fileName = f.FileName;
				ExcelReader excelReader = new ExcelReader();
				DataTable dtExcel = excelReader.Read(fileName, 1, 2);

				// 컬럼이름 변경
				dtExcel.Columns["출력순서"].ColumnName = "NO_DSP";
				dtExcel.Columns["Qty"].ColumnName = "QT";
				dtExcel.Columns["Plate No"].ColumnName = "CD_ITEM_PARTNER";
				dtExcel.Columns["Description"].ColumnName = "NM_ITEM_PARTNER";
				dtExcel.Columns["U-CODE NO"].ColumnName = "UCODE";
				dtExcel.Columns["Del."].ColumnName = "LT";
				dtExcel.Columns["Unit Price"].ColumnName = "UM_EX_E";							

				// 파일번호, 공사번호 가져오기
				fileName = Path.GetFileNameWithoutExtension(fileName);
				string fileNumber = fileName.Split('_')[0];
				string refNumber = fileName.Split('_')[1];

				// DB 조인하여 정보 가져옴
				dtExcel.Columns.Add("NO_FILE", typeof(string), "'" + fileNumber + "'");
				DataTable dtItem = DBMgr.GetDataTable("PS_CZ_PU_PO_REG_ST_L_HGS", false, popupDebug, GetTo.Xml(dtExcel));

				// 헤더 정보 세팅
				tbx발주번호.Text = fileNumber + "-ST";
				tbx주문번호.Text = refNumber;
				//Header.CurrentRow["NO_PO"] = tbx발주번호.Text;
				//Header.CurrentRow["NO_ORDER"] = tbx주문번호.Text;

				ctx매입처.CodeValue = "11823";
				ctx매입처.CodeName = "현대글로벌서비스 주식회사";
				//Ctx매입처_QueryAfter(null, null);
				//Header.CurrentRow["CD_PARTNER"] = ctx매입처.CodeValue;

				// 라인 정보 세팅
				for (int i = 0; i < dtItem.Rows.Count; i++)
				{
					grd라인.Rows.Add();
					grd라인.Row = grd라인.Rows.Count - 1;
					grd라인["NO_PO"] = tbx발주번호.Text;
					grd라인["NO_LINE"] = (int)grd라인.Aggregate(AggregateEnum.Max, "NO_LINE") + 1;
					grd라인["CD_ITEM"] = dtItem.Rows[i]["CD_ITEM"];
					grd라인["NM_ITEM"] = dtItem.Rows[i]["NM_ITEM"];
					grd라인["STND_ITEM"] = dtItem.Rows[i]["STND_ITEM"];
					grd라인["CD_UNIT"] = dtItem.Rows[i]["UNIT_IM"];
					grd라인["QT_PO"] = dtItem.Rows[i]["QT"];
					grd라인["UM_EX_E"] = dtItem.Rows[i]["UM_EX_E"];
					grd라인["AM_EX_E"] = dtItem.Rows[i]["AM_EX_E"];
					grd라인["RT_DC"] = 0;
					grd라인["UM_EX"] = dtItem.Rows[i]["UM_EX_E"];
					grd라인["LT"] = dtItem.Rows[i]["LT"];
					grd라인["DC1"] = dtItem.Rows[i]["CD_ITEM_PARTNER"];
					grd라인["DC2"] = dtItem.Rows[i]["NM_ITEM_PARTNER"];
					grd라인["DC3"] = "";
					grd라인.AddFinished();

					행계산(grd라인.Row, "");					
				}

				MsgControl.CloseMsg();
			}
			catch (Exception ex)
			{
				MsgControl.CloseMsg();
				Util.ShowMessage(Util.GetErrorMessage(ex.Message));
			}
		}

		#endregion

		#region ==================================================================================================== 그리드 이벤트

		private void Grd라인_ValidateEdit(object sender, ValidateEditEventArgs e)
		{
			string 컬럼이름 = grd라인.컬럼이름(e.Col);
			object 이전값 = grd라인[e.Row, e.Col];

			if (컬럼이름 == "CD_ITEM")
			{
				string 재고코드 = grd라인.EditData.한글을영어();

				if (재고코드 == "")
				{
					grd라인["NM_ITEM"] = "";
					grd라인["NO_PART"] = "";
					grd라인["CD_UNIT"] = "";
				}
				else
				{
					// 재고정보 조회
					string query = @"
SELECT
	CD_ITEM
,	NM_ITEM
,	NO_PART	= STND_ITEM
,	CD_UNIT	= UNIT_PO
FROM MA_PITEM WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + 상수.회사코드 + @"'
	AND CD_ITEM = '" + 재고코드 + @"'
	AND YN_USE = 'Y'";

					DataTable dt = 디비.결과(query);

					if (dt.Rows.Count == 1)
					{
						grd라인["CD_ITEM"] = dt.Rows[0]["CD_ITEM"];
						grd라인["NM_ITEM"] = dt.Rows[0]["NM_ITEM"];
						grd라인["NO_PART"] = dt.Rows[0]["NO_PART"];
						grd라인["CD_UNIT"] = dt.Rows[0]["CD_UNIT"];
						grd라인.Col = grd라인.Cols["QT_PO"].Index;
					}
					else
					{
						H_CZ_MA_PITEM f = new H_CZ_MA_PITEM(재고코드);

						if (f.ShowDialog() == DialogResult.OK)
						{
							grd라인["CD_ITEM"] = f.ITEM["CD_ITEM"];
							grd라인["NM_ITEM"] = f.ITEM["NM_ITEM"];
							grd라인["NO_PART"] = f.ITEM["STND_ITEM"];
							grd라인["CD_UNIT"] = f.ITEM["UNIT_PO"];
							grd라인.Col = grd라인.Cols["QT_PO"].Index;
						}
						else
						{
							grd라인["CD_ITEM"] = 이전값.문자();
							e.Cancel = true;
							grd라인.Focus();
						}
					}
				}
			}
			else if (컬럼이름 == "QT_PO")
			{
				행계산(e.Row, 컬럼이름);

				// 첫 입력이면 포커스 이동
				if (이전값.문자() == "")
					grd라인.Col = grd라인.Cols["UM_EX_E"].Index;
			}
			else if (컬럼이름 == "UM_EX_E")
			{
				행계산(e.Row, 컬럼이름);

				// 첫 입력이면 포커스 이동
				if (이전값.문자() == "")
					grd라인.Col = grd라인.Cols["LT"].Index;
			}
			else if (컬럼이름 == "RT_DC" || 컬럼이름 == "UM_EX")
			{
				행계산(e.Row, 컬럼이름);
			}
		}

		private void Grd라인_AfterEdit(object sender, RowColEventArgs e)
		{
			string 컬럼 = grd라인.컬럼이름(e.Col);
			string 새값 = grd라인[e.Row, e.Col].문자();

			if (컬럼 == "CD_ITEM")
			{
				string 재고코드 = 새값.한글을영어().대문자();
				grd라인[e.Row, 컬럼] = 재고코드;

				// 재고정보 조회
				string query = "SELECT CD_ITEM,	NM_ITEM, NO_PART = STND_ITEM, CD_UNIT = UNIT_PO FROM MA_PITEM WITH(NOLOCK) WHERE CD_COMPANY = '" + 상수.회사코드 + "'	AND CD_ITEM = '" + 재고코드 + "'	AND YN_USE = 'Y'";
				DataTable dt = 디비.결과(query);

				if (dt.존재())
				{
					grd라인["CD_ITEM"] = dt.첫행("CD_ITEM");
					grd라인["NM_ITEM"] = dt.첫행("NM_ITEM");
					grd라인["NO_PART"] = dt.첫행("NO_PART");
					grd라인["CD_UNIT"] = dt.첫행("CD_UNIT");
				}
				else
				{
					grd라인["CD_ITEM"] = "";
					grd라인["NM_ITEM"] = "";
					grd라인["NO_PART"] = "";
					grd라인["CD_UNIT"] = "";
				}				
			}
			//else if (컬럼.포함("QT_PO", "UM_EX_E", "RT_DC", "UM_EX"))
			//{
			//	행계산(e.Row, 컬럼);
			//}
			else if (컬럼 == "QT_PO")
			{
				행계산(e.Row, 컬럼);
				grd라인.Col = grd라인.컬럼인덱스("UM_EX_E");
			}
			else if (컬럼 == "UM_EX_E")
			{
				행계산(e.Row, 컬럼);
				grd라인.Col = grd라인.컬럼인덱스("LT");
			}
			else if (컬럼.포함("RT_DC", "UM_EX"))
			{
				행계산(e.Row, 컬럼);
			}
		}

		#endregion

		#region ==================================================================================================== 조회 == SEARCH

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{			
			// 발주번호 대신 주문번호 입력시 조회되도록 함
			if (발주번호 == "" && 주문번호 != "")
			{
				DataTable dtPo = 디비.결과("SELECT NO_PO FROM PU_POH WITH(NOLOCK) WHERE CD_COMPANY = '" + 상수.회사코드 + "' AND NO_ORDER = '" + 주문번호 + "' AND CD_TPPO IN ('1300', '1400', '2300', '2400')");
				if (dtPo.존재()) 발주번호 = dtPo.Rows[0][0].문자();
			}
			
			try
			{
				// ********** 조회
				발주번호 = 발주번호.한글을영어();

				DataTable dtHead = 디비.결과("PS_CZ_PU_PO_REG_ST_H", 상수.회사코드, 발주번호);
				DataTable dtLine = 디비.결과("PS_CZ_PU_PO_REG_ST_L", 상수.회사코드, 발주번호);

				if (!dtHead.존재())
				{
					초기화();
					메시지.경고발생("선택된 자료가 없습니다.");
				}

				// ********** 헤더 바인딩
				헤더.바인딩(dtHead);
				pnl발주번호.사용(false);				// 발주번호 박스는 수정 불가능
				ToolBarDeleteButtonEnabled = true;	// 삭제 버튼 강제 활성화 (L이 없는 경우 활성화가 안됨)

				// 지급조건 에디트 여부 설정 
				if (dtHead.첫행("YN_EDIT_PAYMENT").문자() == "Y")
					pnl지급조건.사용(true);
				else
					pnl지급조건.사용(false);

				// ********** 라인 바인딩
				grd라인.바인딩(dtLine);
				합계계산();

				// ********** 입고여부
				입고여부 = dtLine.Select("QT_RCV > 0").Length > 0;

				// 입고상태에 따라 컨트롤 변경
				pnl발주일자.사용(!입고여부);
				pnl담당자.사용(!입고여부);
				pnl매입처.사용(!입고여부);

				pnl환율.사용(!입고여부);
				pnl발주유형.사용(!입고여부);
				pnl과세구분.사용(!입고여부);

				pnl할인.사용(!입고여부);
				pnl납기.사용(!입고여부);
				pnl표시형식.사용(!입고여부);
			}
			catch (Exception ex)
			{
				메시지.오류알람(ex);
			}			
		}

		#endregion
		
		#region ==================================================================================================== 추가 == ADD

		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
			if (!MsgAndSave(PageActionMode.Search))
				return;

			초기화();
		}

		#endregion

		#region ==================================================================================================== 삭제 == DELETE

		public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{
			if (!메시지.일반선택(공통메세지.자료를삭제하시겠습니까)) return;
			if (!Util.CheckPW()) return;

			try
			{
				디비.실행("PX_CZ_PU_PO_REG_ST", 헤더.데이터테이블().Json(DataRowState.Deleted), DBNull.Value);

				초기화();
				메시지.일반알람(공통메세지.자료가정상적으로삭제되었습니다);
			}
			catch (Exception ex)
			{
				메시지.오류알람(ex);
			}
		}

		#endregion

		#region ==================================================================================================== 저장 == SAVE

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;

			try
			{
				#region ********** 유효성 검사

				// 헤드 정보
				if (tbx발주번호.값() == "")	메시지.경고발생(공통메세지._은는필수입력항목입니다, DD("발주번호"));
				if (dtp발주일자.값() == "")	메시지.경고발생(공통메세지._은는필수입력항목입니다, DD("발주일자"));
				if (ctx담당자.값() == "")		메시지.경고발생(공통메세지._은는필수입력항목입니다, DD("담당자"));
				if (ctx구매그룹.값() == "")	메시지.경고발생(공통메세지._은는필수입력항목입니다, DD("구매그룹"));
				if (ctx매입처.값() == "")		메시지.경고발생(공통메세지._은는필수입력항목입니다, DD("매입처"));
				if (tbx주문번호.값() == "")	메시지.경고발생(공통메세지._은는필수입력항목입니다, DD("주문번호"));
				if (cbo통화구분.값() == "")	메시지.경고발생(공통메세지._은는필수입력항목입니다, DD("환율"));
				if (cbo발주유형.값() == "")	메시지.경고발생(공통메세지._은는필수입력항목입니다, DD("발주유형"));
				if (cbo과세구분.값() == "")	메시지.경고발생(공통메세지._은는필수입력항목입니다, DD("과세구분"));
				//if (cbo지급조건.값() == "")	메시지.경고발생(공통메세지._은는필수입력항목입니다, DD("지급조건"));
				if (tbx인도기간.값() == "")	메시지.경고발생(공통메세지._은는필수입력항목입니다, DD("인도기간"));
				if (cbo선적조건.값() == "")	메시지.경고발생(공통메세지._은는필수입력항목입니다, DD("선적조건"));
				if (cbo포장형태.값() == "")	메시지.경고발생(공통메세지._은는필수입력항목입니다, DD("포장형태"));

				// 그리드 검사
				if (!base.Verify())
					return;

				#endregion
				
				// 저장하기 전 라인의 PO번호를 바꿔줌 (그전까지는 실제 PO번호와 다를수 도 있음)
				grd라인.컬럼값변경("NO_PO", 발주번호);

				// 데이터테이블 저장
				DataTable dtHead = 헤더.데이터테이블_수정();
				DataTable dtLine = grd라인.수정데이터테이블();

				#region ********** 경고마스터 확인

				WARNING warning = new WARNING(WARNING_TARGET.재고발주);
				warning.매입처코드 = ctx매입처.CodeValue;
				warning.아이템 = dtLine;
				//warning.SQLDebug = sqlDebug;
				warning.조회(true);

				// 경고할 꺼리가 있는 경우만.
				if (warning.경고여부)
				{
					DialogResult 경고결과 = warning.ShowDialog();

					if (warning.저장불가 || 경고결과 == DialogResult.Cancel)
					{
						UTIL.메세지("작업이 취소되었습니다.", "WK1");
						return;
					}
				}

				#endregion

				// ********** 저장
				if (입고여부)
				{
					// 첫번째 저장 - 입고항목
					dtLine.필터("ISNULL(QT_RCV, 0) > 0");
					디비.실행("PU_CZ_PU_PO_REG_ST", dtHead.Json(), dtLine.Json());

					// 두번째 저장 - 미입고항목
					dtLine.필터("ISNULL(QT_RCV, 0) = 0");
					디비.실행("PX_CZ_PU_PO_REG_ST", DBNull.Value, dtLine.Json());
				}
				else
				{
					디비.실행("PX_CZ_PU_PO_REG_ST", dtHead.Json(), dtLine.Json());
					pnl발주번호.사용(false);
				}

				헤더.커밋();
				grd라인.커밋();

				메시지.일반알람(공통메세지.자료가정상적으로저장되었습니다);
			}
			catch (Exception ex)
			{
				메시지.오류알람(ex);
			}
		}

		#endregion

		#region ==================================================================================================== 인쇄 == PRINT

		public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
		{			
			try
			{
				메시지.작업중("인쇄 중입니다.");

				// ********** 발주서 다운로드
				// 웹서비스에서 다운로드 및 늘 쓰던 이름으로 변경
				string 다운로드 = 문서.발주서(상수.회사코드, 발주번호);
				string 파일이름 = string.Format("{0}_{1}_PORD_{2}.pdf", 발주번호, ctx매입처.값(), dtp발주일자.값());
				파일.이름변경(다운로드, 파일이름);

				// ERP 서버에 업로드
				string 대상경로 = 상수.회사코드 + "/" + 발주번호;
				string 결과파일 = 파일.업로드(파일이름, 대상경로, false);
				
				// DB 업데이트
				발주서저장(파일.파일이름(결과파일), 1);
				lbl첨부파일.Text = 발주번호 + ".pdf";

				// ********** 선용일 경우 블라인드 발주서 인쇄
				if (선용여부)
				{
					다운로드 = 문서.발주서_블라인드(상수.회사코드, 발주번호);
					파일이름 = string.Format("{0}_{1}_PORD_{2}_B.pdf", 발주번호, ctx매입처.값(), dtp발주일자.값());
					파일.이름변경(다운로드, 파일이름);

					// ERP 서버에 업로드
					대상경로 = 상수.회사코드 + "/" + 발주번호;
					결과파일 = 파일.업로드(파일이름, 대상경로, false);
					발주서저장(파일.파일이름(결과파일), 2);
				}

				파일.실행(파일이름);
				메시지.작업중();
			}
			catch (Exception ex)
			{
				메시지.오류알람(ex);
			}
		}

		private string 발주서다운로드(int 순번, bool 자동실행)
		{
			if (lbl첨부파일.Text != "")
			{
				string query = @"SELECT CD_COMPANY + '/' + NO_PO + '/' + NM_FILE FROM CZ_PU_POL_FILE WITH(NOLOCK) WHERE CD_COMPANY = '" + 상수.회사코드 + "' AND NO_PO = '" + 발주번호 + "' AND NO_LINE = 0 AND SEQ = " + 순번;
				DataTable dt = 디비.결과(query);
				string 파일이름 = dt.첫행(0).문자();
				string 다운로드 = 파일.다운로드(파일이름, 자동실행);

				return 다운로드;
			}

			return "";
		}

		/// <summary>
		/// 데이터베이스에 첨부파일 INSERT OR UPDATE
		/// </summary>
		/// <param name="파일이름"></param>
		/// <param name="순번">순번 1:발주서,2:블라인드발주서</param>
		private void 발주서저장(string 파일이름, int 순번)
		{
			string query = @"
DECLARE		@CZ_PU_POL_FILE CZ_PU_POL_FILE
INSERT INTO @CZ_PU_POL_FILE
(
	CD_COMPANY
,	NO_PO
,	NO_LINE
,	SEQ
,	NM_FILE
,	DC_FILE
,	DC_RMK
,	ID_USER
)
VALUES
(
	@CD_COMPANY
,	@NO_PO
,	0				-- 헤더 관련 파일일 경우 0으로 고정
,	@SEQ
,	@NM_FILE
,	@DC_FILE
,	'발주서'
,	@ID_USER
)

EXEC PM_CZ_PU_POL_FILE @CZ_PU_POL_FILE";

			디비 db = new 디비(query);
			db.변수.추가("@CD_COMPANY"	, 상수.회사코드);
			db.변수.추가("@NO_PO"			, 발주번호);
			db.변수.추가("@SEQ"			, 순번);
			db.변수.추가("@NM_FILE"		, 파일이름);
			db.변수.추가("@DC_FILE"		, 발주번호 + ".pdf");
			db.변수.추가("@ID_USER"		, 상수.사원번호);
			db.실행();
		}

		#endregion

		#region ==================================================================================================== 계산식

		private void 행계산(int i, string colName)
		{
			// 표시형식(반올림) 때문에 단가까지 새로 계산해줌
			if (colName == "")
			{
				매입견적금액계산(i);
				매입단가계산(i);
				매입금액계산(i);
				부가세계산(i);
			}
			else if (colName == "QT_PO")
			{
				매입견적금액계산(i);
				매입단가계산(i);
				매입금액계산(i);
				부가세계산(i);
			}
			else if (colName == "UM_EX_E")
			{
				매입견적금액계산(i);
				매입단가계산(i);
				매입금액계산(i);
				부가세계산(i);
			}
			else if (colName == "RT_DC")
			{
				매입단가계산(i);
				매입금액계산(i);
				부가세계산(i);
			}
			else if (colName == "UM_EX")
			{
				할인율계산(i);
				매입금액계산(i);
				부가세계산(i);
			}

			합계계산();
		}

		// 매입견적금액 => 매입견적단가가 외부의 요소에 의해 바뀔일이 없으므로 금액만 계산
		private void 매입견적금액계산(int i)
		{
			decimal 수량				= GetTo.Decimal(grd라인[i, "QT_PO"]);
			decimal 매입견적단가외화	= GetTo.Decimal(grd라인[i, "UM_EX_E"]);
			decimal 매입견적금액외화	= 매입견적단가외화 * 수량;
			decimal 매입견적단가원화	= GetTo.Decimal(string.Format("{0:" + grd라인.Cols["UM_KR_E"].Format + "}", 매입견적단가외화 * 환율));
			decimal 매입견적금액원화	= GetTo.Decimal(string.Format("{0:" + grd라인.Cols["AM_KR_E"].Format + "}", 매입견적금액외화 * 환율));

			grd라인[i, "AM_EX_E"] = 매입견적금액외화;
			grd라인[i, "UM_KR_E"] = 매입견적단가원화;
			grd라인[i, "AM_KR_E"] = 매입견적금액원화;
		}

		// 할인율 계산 => 원화 베이스로 계산
		private void 할인율계산(int i)
		{
			decimal 매입견적단가외화 = GetTo.Decimal(grd라인[i, "UM_EX_E"]);
			decimal 매입단가외화	   = GetTo.Decimal(grd라인[i, "UM_EX"]);

			if (GetTo.Int(grd라인[i, "NO_LINE"]) < 90000)
				grd라인[i, "RT_DC"] = Calculator.할인율계산(매입견적단가외화, 매입단가외화);
			else
				grd라인[i, "RT_DC"] = 0;
		}

		// 매입단가 => 외부의 요소에 의해 변하는 경우(매입견적단가를 수정했다거나 DC를 수정했다거나)
		private void 매입단가계산(int i)
		{
			decimal 매입견적단가외화	= GetTo.Decimal(grd라인[i, "UM_EX_E"]);
			decimal 할인율			= GetTo.Decimal(grd라인[i, "RT_DC"]);
			decimal 매입단가외화;

			매입단가외화 = Calculator.할인율적용(매입견적단가외화, 할인율);
			매입단가외화 = Calculator.반올림(매입단가외화, 표시형식단가용);

			grd라인[i, "UM_EX"] = 매입단가외화;
		}

		// 매입금액 => 그냥 구하면 됨
		private void 매입금액계산(int i)
		{
			decimal 수량			= GetTo.Decimal(grd라인[i, "QT_PO"]);
			decimal 매입단가외화	= GetTo.Decimal(grd라인[i, "UM_EX"]);
			decimal 매입금액외화	= Calculator.반올림(매입단가외화 * 수량, 표시형식금액용);
			decimal 매입단가원화	= GetTo.Decimal(string.Format("{0:" + grd라인.Cols["UM"].Format + "}", 매입단가외화 * 환율));
			decimal 매입금액원화	= GetTo.Decimal(string.Format("{0:" + grd라인.Cols["AM"].Format + "}", 매입금액외화 * 환율));

			grd라인[i, "AM_EX"]	= 매입금액외화;
			grd라인[i, "UM"]		= 매입단가원화;
			grd라인[i, "AM"]		= 매입금액원화;
		}

		// 부가세 => 나누기 10
		private void 부가세계산(int i)
		{
			decimal 매입금액원화	= GetTo.Decimal(grd라인[i, "AM"]);
			decimal 부가세		= 매입금액원화 * (부가세율 / 100);

			grd라인[i, "VAT"] = 부가세;
		}

		// 합계
		private void 합계계산()
		{
			DataTable dt = grd라인.DataTable.Copy();

			// 매입금액
			decimal 매입견적금액원화 = GetTo.Decimal(dt.Compute("SUM(AM_KR_E)", "NO_LINE < 90000"));
			decimal 매입견적금액외화 = GetTo.Decimal(dt.Compute("SUM(AM_EX_E)", "NO_LINE < 90000"));
			
			decimal 매입금액원화	= GetTo.Decimal(dt.Compute("SUM(AM)", "NO_LINE < 90000"));
			decimal 매입금액외화	= GetTo.Decimal(dt.Compute("SUM(AM_EX)", "NO_LINE < 90000"));

			decimal 할인금액원화	= 매입견적금액원화 - 매입금액원화;
			decimal 할인금액외화	= 매입견적금액외화 - 매입금액외화;
			
			// 할인
			decimal 할인율일반	= Calculator.할인율계산(매입견적금액원화, 매입금액원화);

			// 합계
			decimal 부대비용원화	= GetTo.Decimal(dt.Compute("SUM(AM)", "NO_LINE > 90000"));
			decimal 최종합계원화	= 매입금액원화 + 부대비용원화;

			decimal 부대비용외화 = GetTo.Decimal(dt.Compute("SUM(AM_EX)", "NO_LINE > 90000"));
			decimal 최종합계외화 = 매입금액외화 + 부대비용외화;

			// Label 정보 변경

			lbl외화통화.Text = cbo통화구분.GetText();
			lbl외화통화2.Text = cbo통화구분.GetText();

			lbl매입견적금액원화.Text = string.Format("{0:#,##0.##}", 매입견적금액원화);
			lbl매입견적금액외화.Text = string.Format("{0:#,##0.##}", 매입견적금액외화);

			lbl할인금액원화.Text = string.Format("{0:#,##0.##}", 할인금액원화);
			lbl할인금액외화.Text = string.Format("{0:#,##0.##}", 할인금액외화);

			lbl매입금액원화.Text = string.Format("{0:#,##0.##}", 매입금액원화);
			lbl매입금액외화.Text = string.Format("{0:#,##0.##}", 매입금액외화);

			lbl할인금액일반.Text = string.Format("{0:#,##0.##}", 할인금액원화);
			lbl할인율일반.Text = string.Format("{0:#,##0.##}", 할인율일반);
		
			lbl부대비용원화.Text = string.Format("{0:#,##0.##}", 부대비용원화);
			lbl최종합계원화.Text = string.Format("{0:#,##0.##}", 최종합계원화);

			lbl부대비용외화.Text = string.Format("{0:#,##0.##}", 부대비용외화);
			lbl최종합계외화.Text = string.Format("{0:#,##0.##}", 최종합계외화);
		}

		#endregion
	}
}

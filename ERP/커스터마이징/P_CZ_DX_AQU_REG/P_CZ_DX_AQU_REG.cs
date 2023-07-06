using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Controls;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;

using DX;

namespace cz
{
	public partial class P_CZ_DX_AQU_REG : PageBase
	{
		string[] 재고컬럼;
		float 높이_헤더레이아웃;
		float 높이_라인레이아웃;

		string 회사코드 => grd헤드["CD_COMPANY"].문자() == "" ? 상수.회사코드 : grd헤드["CD_COMPANY"].문자();
		string 관리번호 => grd헤드["NO_AMS"].문자();
		int 아이템번호 => grd라인["NO_ITEM"].정수();

		public P_CZ_DX_AQU_REG()
		{
			InitializeComponent();
		}

		#region ==================================================================================================== 초기화 == INIT == ㅑㅜㅑㅅ

		protected override void InitLoad()
		{
			this.페이지초기화();
			spc메인.Visible = false;
			lbl작업시간.Text = "";

			// ********** 검색 관련
			tbx관리번호_검색.엔터검색();
			tbx제목_검색.엔터검색();

			// ********** 콤보 관련
			DataTable dt = new DataTable();
			dt.Columns.Add("TYPE");
			dt.Columns.Add("CODE");
			dt.Columns.Add("NAME");

			dt.Rows.Add("접수기간", "12", "12개월");
			dt.Rows.Add("접수기간", "9", "9개월");
			dt.Rows.Add("접수기간", "6", "6개월");
			dt.Rows.Add("접수기간", "3", "3개월");

			cbo접수일자.바인딩(dt.선택("TYPE = '접수기간'"), false);
			cbo접수일자.값("12");
			Cbo접수일자_SelectionChangeCommitted(null, null);

			cbo수주율.바인딩(dt.선택("TYPE = '접수기간'"), false);	// 똑같이 써도 될듯?

			// ********** 키워드 관련
			btn메인키추가.Text = "";
			btn제외키추가.Text = "";
			btn서브키1추가.Text = "";
			btn서브키2추가.Text = "";

			btn메인키삭제.Text = "";
			btn제외키삭제.Text = "";
			btn서브키1삭제.Text = "";
			btn서브키2삭제.Text = "";

			btn메인키추가.Image = new Bitmap(아이콘.더하기_18x18, new Size(14, 14));
			btn제외키추가.Image = new Bitmap(아이콘.더하기_18x18, new Size(14, 14));
			btn서브키1추가.Image = new Bitmap(아이콘.더하기_18x18, new Size(14, 14));
			btn서브키2추가.Image = new Bitmap(아이콘.더하기_18x18, new Size(14, 14));

			btn메인키삭제.Image = new Bitmap(아이콘.빼기_18x18, new Size(14, 14));
			btn제외키삭제.Image = new Bitmap(아이콘.빼기_18x18, new Size(14, 14));
			btn서브키1삭제.Image = new Bitmap(아이콘.빼기_18x18, new Size(14, 14));
			btn서브키2삭제.Image = new Bitmap(아이콘.빼기_18x18, new Size(14, 14));

			tbx매입단가F.숫자만_정수();
			tbx매입단가T.숫자만_정수();			
			tbx재고할인율F.숫자만_실수();
			tbx재고할인율T.숫자만_실수();
			tbx수주율F.숫자만_실수();
			tbx수주율T.숫자만_실수();
			tbx대표이윤율.숫자만_실수();
			tbx민감이윤율H.숫자만_실수();
			tbx민감이윤율M.숫자만_실수();
			tbx민감이윤율L.숫자만_실수();

			// 키워드 그리드 배경
			pnl메인키1.BackColor = Color.Blue;
			pnl메인키2.BackColor = Color.White;

			pnl제외키1.BackColor = Color.Red;
			pnl제외키2.BackColor = Color.White;

			pnl서브키11.BackColor = Color.Green;
			pnl서브키12.BackColor = Color.White;

			pnl서브키21.BackColor = Color.Green;
			pnl서브키22.BackColor = Color.White;

			grd메인키.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
			grd제외키.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
			grd서브키1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
			grd서브키2.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;

			// ********** 기타
			// 패널바인딩 하기전에 라디오 기본값 저장
			foreach (RadioButtonExt 라디오 in lay라인.컨트롤<RadioButtonExt>())
			{
				string 태그 = 라디오.라디오_디폴트().태그();
				string 컬럼 = 태그.분할(",")[0];
				string 값 = 태그.분할(",")[1];

				if (!라디오기본값s.ContainsKey(컬럼))
					라디오기본값s.Add(컬럼, 값);
			}






			for (int i = 2; i < lay금액_컬럼.ColumnCount - 1; i++)
			{
				lay금액_컬럼.ColumnStyles[i].Width = 0;
			}

			for (int i = 2; i < lay금액_라인.RowCount - 1; i++)
			{
				lay금액_라인.RowStyles[i].Height = 0;
			}

			



			// 초기화
			InitGrid();
			InitEvent();
		}

		Dictionary<string, string> 라디오기본값s = new Dictionary<string, string>();

		protected override void InitPaint()
		{
			spc메인.Visible = true;

			// 숨기기 할때를 위해서 저장
			높이_헤더레이아웃 = lay라인컨테이너.RowStyles[1].Height;
			높이_라인레이아웃 = lay라인컨테이너.RowStyles[2].Height;

			// 스플릿 기본 설정
			spc메인.SplitterDistance = 400;
			spc라인.SplitterDistance = 1000;
			
			spc키워드.SplitterDistance = spc키워드.Height / 2;
			spc메인키.SplitterDistance = spc메인키.Width - 260;
			spc서브키.SplitterDistance = 260;  // 키워드 한칸
		}

		#endregion

		#region ==================================================================================================== 그리드 == GRID == ㅎ걍

		private void InitGrid()
		{
			MainGrids = this.컨트롤<FlexGrid>();

			//DataTable dt = new DataTable();
			//dt.Columns.Add("CODE");
			//dt.Columns.Add("NAME");

			//dt.Rows.Add("STK", "재고");
			//dt.Rows.Add("STD", "정상");
			//dt.Rows.Add("NET", "할인");
			//dt.Rows.Add("MKT", "시장");

			// ********** 헤드
			grd헤드.세팅시작(1);
			grd헤드.컬럼세팅("CD_COMPANY"	, "회사코드"		, false);
			grd헤드.컬럼세팅("NO_AMS"		, "관리번호"		, 100	, 정렬.가운데);
			grd헤드.컬럼세팅("NM_AMS"		, "제목"			, 250);
			
			grd헤드.기본키("CD_COMPANY", "NO_AMS");
			grd헤드.에디트컬럼("NM_AMS");
			grd헤드.세팅종료("23.05.30.01", false);

			grd헤드.패널바인딩(lay헤드);
			grd헤드.상세그리드(grd라인);




			// ********** 헤드
			grd금액.세팅시작(2);
			grd금액.컬럼세팅("CD_COMPANY"	, "회사코드"		, false);
			grd금액.컬럼세팅("NO_AMS"		, "관리번호"		, false);
			grd금액.컬럼세팅("aaa"		, "이상미만"		, 80	, 포맷.비율);
			
			//grd금액.기본키("CD_COMPANY", "NO_AMS");
			//grd금액.에디트컬럼("NM_AMS");
			grd금액.세팅종료("23.05.01.01", false);

			//grd금액.패널바인딩(lay헤드);
			//grd금액.상세그리드(grd라인);





			// ********** 라인
			grd라인.세팅시작(2);
			grd라인.컬럼세팅("CD_COMPANY"	, "회사코드"				, false);
			grd라인.컬럼세팅("NO_AMS"		, "관리번호"				, false);
			grd라인.컬럼세팅("NO_ITEM"	, "아이템번호"			, false);
			
			grd라인.컬럼세팅("NM_GROUP"	, "아이템구분"			, 150);
			grd라인.컬럼세팅("CD_ITEM"	, "재고코드"				, 105	, 정렬.가운데);

			grd라인.컬럼세팅("RT_MARGIN"	, "이윤율\n(대표)"			, 상수.넓이_수량	, 포맷.비율);

			grd라인.컬럼세팅("NM_ITEM"	, "재고이름"				, 150);

			grd라인.컬럼세팅("UCODE"		, "U코드"				, 120);

						
			grd라인.컬럼세팅("NM_CLS_M"	, "품목분류"				, 80	, 정렬.가운데);
			grd라인.컬럼세팅("FG_UM"		, "단가유형"				, 80	, 정렬.가운데);
		
			grd라인.컬럼세팅("CNT_QTN"	, "건수"			, "견적"		, 상수.넓이_수량	, 포맷.수량);
			grd라인.컬럼세팅("CNT_RATE"	, "건수"			, "수주율"	, 상수.넓이_수량	, 포맷.비율);
			grd라인.컬럼세팅("QT_SO"		, "수량"			, "수주"		, 상수.넓이_수량	, 포맷.수량);
			grd라인.컬럼세팅("QT_RATE"	, "수량"			, "수주율"	, 상수.넓이_수량	, 포맷.비율);

			grd라인.컬럼세팅("QT_AVST"	, "가용재고"		, "재고"		, 상수.넓이_수량	, 포맷.수량);
			grd라인.컬럼세팅("QT_AVPO"	, "가용재고"		, "발주"		, 상수.넓이_수량	, 포맷.수량);

			grd라인.컬럼세팅("YN_EDIT"	, "수정여부"		, 80	, 정렬.가운데);
			grd라인.컬럼세팅("YN_AVST"	, "재고유무"		, 80	, 정렬.가운데);

			grd라인.컬럼세팅("UM_LAST"	, "단가"			, "최근단가"	, 상수.넓이_단가	, 포맷.수량);
			grd라인.컬럼세팅("UM_STK"		, "단가"			, "재고단가"	, 상수.넓이_단가	, 포맷.수량);
			grd라인.컬럼세팅("RT_DC"		, "단가"			, "할인율"	, 상수.넓이_수량	, 포맷.수량);
			grd라인.컬럼세팅("DT_QTN_LAST", "단가"			, "최근견적일", 상수.넓이_일자	, 포맷.날짜);

			재고컬럼 = new string[] {	  "CD_ITEM", "NM_ITEM"
									, "CNT_QTN", "CNT_RATE", "QT_SO", "QT_RATE"
									, "QT_AVST", "QT_AVPO"
									, "UM_LAST", "UM_STK", "RT_DC", "DT_QTN_LAST" };

			grd라인.컬럼세팅("UM_F"		, "매입단가"		, "부터"		, 상수.넓이_단가	, 포맷.원화단가);
			grd라인.컬럼세팅("UM_T"		, "매입단가"		, "까지"		, 상수.넓이_단가	, 포맷.원화단가);
			grd라인.컬럼세팅("RT_DC_F"	, "재고할인율"	, "부터"		, 상수.넓이_단가	, 포맷.비율);
			grd라인.컬럼세팅("RT_DC_T"	, "재고할인율"	, "까지"		, 상수.넓이_단가	, 포맷.비율);
			grd라인.컬럼세팅("RT_SO_F"	, "수주율"		, "부터"		, 상수.넓이_단가	, 포맷.비율);
			grd라인.컬럼세팅("RT_SO_T"	, "수주율"		, "까지"		, 상수.넓이_단가	, 포맷.비율);
			
			grd라인.컬럼세팅("RT_MARGIN_H", "이윤율(민감)"	, "높음"		, 상수.넓이_수량	, 포맷.비율);
			grd라인.컬럼세팅("RT_MARGIN_M", "이윤율(민감)"	, "중간"		, 상수.넓이_수량	, 포맷.비율);
			grd라인.컬럼세팅("RT_MARGIN_L", "이윤율(민감)"	, "낮음"		, 상수.넓이_수량	, 포맷.비율);

			grd라인.데이터맵("FG_UM", 코드.코드관리("SA_B000021"));
			grd라인.기본키("CD_COMPANY", "NO_AMS", "NO_ITEM");
			grd라인.에디트컬럼("CD_ITEM", "NM_GROUP", "UM_F", "UM_T", "RT_DC_F", "RT_DC_T", "RT_SO_F", "RT_SO_T", "RT_MARGIN", "RT_MARGIN_H", "RT_MARGIN_M", "RT_MARGIN_L");
			grd라인.세팅종료("23.06.19.01", false);

			grd라인.패널바인딩(lay라인);
			grd라인.상세그리드(grd메인키, grd제외키, grd서브키1, grd서브키2);

			grd라인.복사붙여넣기(Btn아이템추가_Click, Grd라인_AfterEdit);







			// ********** 트레이닝
			grd트레이닝.세팅시작(2);
			grd트레이닝.컬럼세팅("NO_FILE"			, "파일번호"		, 90	, 정렬.가운데);
			grd트레이닝.컬럼세팅("NO_LINE"			, "항번"			, false);
			grd트레이닝.컬럼세팅("NO_DSP"			, "순번"			, 40	, 포맷.순번);
			grd트레이닝.컬럼세팅("NM_SUBJECT"		, "주제"			, 300);
			grd트레이닝.컬럼세팅("CD_ITEM_PARTNER"	, "품목코드"		, 150);
			grd트레이닝.컬럼세팅("NM_ITEM_PARTNER"	, "품목명"		, 300);
			grd트레이닝.컬럼세팅("NM_VENDOR"		, "매입처"		, 100);
			grd트레이닝.컬럼세팅("CD_ITEM"			, "재고코드"		, 110	, 정렬.가운데);
			
			grd트레이닝.컬럼세팅("QT_ST"			, "재고수량"		, "재고"			, 55	, 포맷.수량);
			grd트레이닝.컬럼세팅("QT_PO"			, "재고수량"		, "발주"			, 55	, 포맷.수량);
			
			grd트레이닝.컬럼세팅("UM_KR_P"			, "단가"			, "매입단가"		, 85	, 포맷.원화단가);
			grd트레이닝.컬럼세팅("UM_STD"			, "단가"			, "표준단가"		, 85	, 포맷.원화단가);
			grd트레이닝.컬럼세팅("UM_STK"			, "단가"			, "재고단가"		, 85	, 포맷.원화단가);
			grd트레이닝.컬럼세팅("RT_DC_STK"		, "단가"			, "할인율"		, 55	, 포맷.비율);

			grd트레이닝.세팅종료("23.03.31.03", false);

			// ********** 키워드
			grd메인키.세팅시작(1);
			grd메인키.컬럼세팅("CD_COMPANY"	, "회사코드"		, false);
			grd메인키.컬럼세팅("NO_AMS"		, "관리코드"		, false);
			grd메인키.컬럼세팅("NO_ITEM"		, "아이템번호"	, false);
			grd메인키.컬럼세팅("CD_TYPE"		, "타입"			, false);
			grd메인키.컬럼세팅("SEQ"			, "순번"			, false);
			grd메인키.컬럼세팅("KEY1_DSP"		, "포함"			, 180);
			grd메인키.컬럼세팅("KEY2_DSP"		, "포함"			, 180);
			grd메인키.컬럼세팅("KEY3_DSP"		, "포함"			, 180);
			grd메인키.컬럼세팅("KEY4_DSP"		, "포함"			, 180);

			//grd메인키.필수값("KEY1_DSP");
			grd메인키.세팅종료("23.03.22.01", false);
			
			// 세팅복사
			grd제외키.세팅복사(grd메인키);
			grd서브키1.세팅복사(grd메인키);
			grd서브키2.세팅복사(grd메인키);

			grd메인키.에디트컬럼("KEY1_DSP", "KEY2_DSP", "KEY3_DSP", "KEY4_DSP");
			grd제외키.에디트컬럼("KEY1_DSP");
			grd서브키1.에디트컬럼("KEY1_DSP");
			grd서브키2.에디트컬럼("KEY1_DSP", "KEY2_DSP", "KEY3_DSP");

			grd메인키.복사붙여넣기(Btn키워드추가_Click, null);
			grd제외키.복사붙여넣기(Btn키워드추가_Click, null);
			grd서브키1.복사붙여넣기(Btn키워드추가_Click, null);
			grd서브키2.복사붙여넣기(Btn키워드추가_Click, null);

			// 컬럼별 처리
			grd제외키.Cols["KEY1_DSP"].Caption = "제외";

			grd제외키.Cols["KEY2_DSP"].Visible = false;
			grd제외키.Cols["KEY3_DSP"].Visible = false;
			grd제외키.Cols["KEY4_DSP"].Visible = false;

			grd서브키1.Cols["KEY2_DSP"].Visible = false;
			grd서브키1.Cols["KEY3_DSP"].Visible = false;
			grd서브키1.Cols["KEY4_DSP"].Visible = false;

			grd서브키2.Cols["KEY4_DSP"].Visible = false;
		}

		#endregion

		#region ==================================================================================================== 이벤트 == EVENT == ㄷㅍ두

		private void InitEvent()
		{
			// 메인
			btn아이템추가.Click += Btn아이템추가_Click;
			btn아이템삭제.Click += Btn아이템삭제_Click;
			btn트레이닝.Click += Btn트레이닝_Click;

			// 검색
			cbo접수일자.SelectionChangeCommitted += Cbo접수일자_SelectionChangeCommitted;

			// 아이템
			
			ctx재고코드.QueryBefore += Ctx재고코드_QueryBefore;
			ctx재고코드.CodeChanged += Ctx재고코드_CodeChanged;
			

			ctx품목분류.QueryBefore += Ctx품목분류_QueryBefore;
			
			tbx매입단가F.LostFocus += Tbx숫자형식_LostFocus;
			tbx매입단가T.LostFocus += Tbx숫자형식_LostFocus;
			tbx재고할인율F.LostFocus += Tbx숫자형식_LostFocus;
			tbx재고할인율T.LostFocus += Tbx숫자형식_LostFocus;
			tbx수주율F.LostFocus += Tbx숫자형식_LostFocus;
			tbx수주율T.LostFocus += Tbx숫자형식_LostFocus;
			tbx대표이윤율.LostFocus += Tbx숫자형식_LostFocus;
			tbx민감이윤율H.LostFocus += Tbx숫자형식_LostFocus;
			tbx민감이윤율M.LostFocus += Tbx숫자형식_LostFocus;
			tbx민감이윤율L.LostFocus += Tbx숫자형식_LostFocus;

			// 키워드
			btn메인키추가.Click += Btn키워드추가_Click;
			btn제외키추가.Click += Btn키워드추가_Click;
			btn서브키1추가.Click += Btn키워드추가_Click;
			btn서브키2추가.Click += Btn키워드추가_Click;

			btn메인키삭제.Click += Btn키워드삭제_Click;
			btn제외키삭제.Click += Btn키워드삭제_Click;
			btn서브키1삭제.Click += Btn키워드삭제_Click;
			btn서브키2삭제.Click += Btn키워드삭제_Click;

			// 그리드
			grd헤드.AfterRowChange += Grd헤드_AfterRowChange;
			grd라인.AfterRowChange += Grd라인_AfterRowChange;

			grd라인.AfterEdit += Grd라인_AfterEdit;
		

			//tab라인.Selected += Tab라인_Selected;



			btn금액_컬럼추가.Click += Btn금액_컬럼추가_Click;
			btn금액_컬럼삭제.Click += Btn금액_컬럼삭제_Click;

			btn금액_라인추가.Click += Btn금액_라인추가_Click;
			btn금액_라인삭제.Click += Btn금액_라인삭제_Click;

			btn금액_확정.Click += Btn금액_확정_Click;

			btn숨기기H.Click += Btn숨기기H_Click;
			btn숨기기L.Click += Btn숨기기L_Click;
		}

		private void Ctx재고코드_CodeChanged(object sender, EventArgs e)
		{			
			재고관련바인딩(grd라인.Row, ctx재고코드.CodeValue);
		}



		
		private void Grd라인_AfterEdit(object sender, RowColEventArgs e)
		{
			string 컬럼이름 = grd라인.컬럼이름(e);

			if (컬럼이름 == "CD_ITEM")
			{
				재고관련바인딩(e.Row, grd라인[e.Row, 컬럼이름].문자().대문자().한글을영어());
//				string 재고코드 = grd라인[e.Row, 컬럼이름].문자().대문자().한글을영어();
//				string query = @"
//SELECT
//	A.CD_ITEM
//,	A.NM_ITEM

//,	Y.CNT_QTN
//,	Y.CNT_RATE
//,	Y.QT_SO	
//,	Y.QT_RATE

//,	X.QT_AVST
//,	X.QT_AVPO

//,	Y.UM_LAST
//,	X.UM_STK
//,	RT_DC		= NEOE.CALC_DC_RATE(Y.UM_LAST, X.UM_STK)
//,	Y.DT_QTN_LAST
//FROM	  MA_PITEM			AS A WITH(NOLOCK)
//LEFT JOIN CZ_DX_STOCK_QT	AS X WITH(NOLOCK) ON A.CD_COMPANY = X.CD_COMPANY AND A.CD_ITEM = X.CD_ITEM
//LEFT JOIN CZ_SA_FREQH		AS Y WITH(NOLOCK) ON A.CD_COMPANY = Y.CD_COMPANY AND A.CD_ITEM = Y.CD_ITEM
//WHERE 1 = 1
//	AND A.CD_COMPANY = '" + 상수.회사코드 + @"'
//	AND A.CD_ITEM = '" + 재고코드 + @"'
//	AND A.YN_USE = 'Y'";

//				DataTable dt = 재고코드 == "" ? null : 디비.결과(query);

//				if (dt != null && dt.Rows.Count == 1)
//				{
//					foreach (string s in 재고컬럼) grd라인[e.Row, s] = dt.첫행(s);
//				}
//				else
//				{
//					DataRow 행 = grd라인.데이터테이블().선택(grd라인.상세그리드필터())[0];
//					foreach (string s in 재고컬럼) 행[s] = DBNull.Value;
//				}
				
			}
			else
			{
				//행계산(e.Row, colName);
			}
		}

		private void 재고관련바인딩(int i, string 재고코드)
		{
			string query = @"
SELECT
	A.CD_ITEM
,	A.NM_ITEM

,	Y.CNT_QTN
,	Y.CNT_RATE
,	Y.QT_SO	
,	Y.QT_RATE

,	X.QT_AVST
,	X.QT_AVPO

,	Y.UM_LAST
,	X.UM_STK
,	RT_DC		= NEOE.CALC_DC_RATE(Y.UM_LAST, X.UM_STK)
,	Y.DT_QTN_LAST
FROM	  MA_PITEM			AS A WITH(NOLOCK)
LEFT JOIN CZ_DX_STOCK_QT	AS X WITH(NOLOCK) ON A.CD_COMPANY = X.CD_COMPANY AND A.CD_ITEM = X.CD_ITEM
LEFT JOIN CZ_SA_FREQH		AS Y WITH(NOLOCK) ON A.CD_COMPANY = Y.CD_COMPANY AND A.CD_ITEM = Y.CD_ITEM
WHERE 1 = 1
	AND A.CD_COMPANY = '" + 상수.회사코드 + @"'
	AND A.CD_ITEM = '" + 재고코드.대문자().한글을영어() + @"'
	AND A.YN_USE = 'Y'";

			DataTable dt = 재고코드 == "" ? null : 디비.결과(query);

			if (dt != null && dt.Rows.Count == 1)
			{
				foreach (string s in 재고컬럼) grd라인[i, s] = dt.첫행(s);
			}
			else
			{
				DataRow 행 = grd라인.데이터테이블().선택(grd라인.상세그리드필터())[0];
				foreach (string s in 재고컬럼) 행[s] = DBNull.Value;
			}
		}

		private void Btn금액_확정_Click(object sender, EventArgs e)
		{
			//decimal 이전값;
			//decimal 신규값 = 0;

			//// ********** 컬럼
			//DataTable 컬럼 = new DataTable();
			//컬럼.컬럼추가("CD_COMPANY");
			//컬럼.컬럼추가("NO_AMS");
			//컬럼.컬럼추가("NO_COL"	, typeof(int));
			//컬럼.컬럼추가("VALUE_F"	, typeof(decimal));
			//컬럼.컬럼추가("VALUE_T"	, typeof(decimal));

			//for (int i = 1; i < lay금액_컬럼.ColumnCount - 1; i++)
			//{
			//	if (lay금액_컬럼.ColumnStyles[i].Width == 0)
			//		break;

			//	이전값 = i == 1 ? 0 : 신규값;
			//	신규값 = ((lay금액_컬럼.GetControlFromPosition(i, 1) as PanelExt).Controls[0] as TextBoxExt).Text.실수();
			//	컬럼.행추가(회사코드, 관리번호, i, 이전값, 신규값);
			//}

			//// ********** 라인
			//DataTable 라인 = new DataTable();
			//라인.컬럼추가("CD_COMPANY");
			//라인.컬럼추가("NO_AMS");
			//라인.컬럼추가("NO_COL"	, typeof(int));
			//라인.컬럼추가("NO_ROW"	, typeof(int));
			//라인.컬럼추가("VALUE_F"	, typeof(decimal));
			//라인.컬럼추가("VALUE_T"	, typeof(decimal));

			//for (int i = 1; i < lay금액_라인.RowCount - 1; i++)
			//{
			//	if (lay금액_라인.RowStyles[i].Height == 0)
			//		break;

			//	이전값 = i == 1 ? 0 : 신규값;
			//	신규값 = ((lay금액_라인.GetControlFromPosition(1, i) as PanelExt).Controls[0] as TextBoxExt).Text.실수();
			//	라인.행추가(회사코드, 관리번호, 1, i, 이전값, 신규값);
			//}

			//디비.실행("PX_CZ_DX_AMSL_AM", 컬럼.Json(), 라인.Json());

			//string a = "";

			//디비 db = new 디비("PX_CZ_DX_AMS");
			//db.변수.추가("@JSON_H", grd헤드.데이터테이블_수정().Json());
			//db.변수.추가("@JSON_L", grd라인.데이터테이블_수정().Json());
			//db.변수.추가("@JSON_MK", grd메인키.데이터테이블_수정().Json("CD_COMPANY", "NO_AMS", "NO_ITEM", "CD_TYPE", "SEQ", "KEY1_DSP", "KEY2_DSP", "KEY3_DSP", "KEY4_DSP"));
			//db.변수.추가("@JSON_EX", grd제외키.데이터테이블_수정().Json("CD_COMPANY", "NO_AMS", "NO_ITEM", "CD_TYPE", "SEQ", "KEY1_DSP", "KEY2_DSP", "KEY3_DSP", "KEY4_DSP"));
			//db.변수.추가("@JSON_SK1", grd서브키1.데이터테이블_수정().Json("CD_COMPANY", "NO_AMS", "NO_ITEM", "CD_TYPE", "SEQ", "KEY1_DSP", "KEY2_DSP", "KEY3_DSP", "KEY4_DSP"));
			//db.변수.추가("@JSON_SK2", grd서브키2.데이터테이블_수정().Json("CD_COMPANY", "NO_AMS", "NO_ITEM", "CD_TYPE", "SEQ", "KEY1_DSP", "KEY2_DSP", "KEY3_DSP", "KEY4_DSP"));
			//DataTable dt = db.결과();
		}

		private void 금액테이블바인딩()
		{
			try
			{
				DataTable 컬럼 = 디비.결과("SELECT * FROM CZ_DX_AMSL_AM_COL WHERE CD_COMPANY = '" + 회사코드 + "' AND NO_AMS = '" + 관리번호 + "' ORDER BY NO_COL");
				DataTable 라인 = 디비.결과("SELECT * FROM CZ_DX_AMSL_AM_ROW WHERE CD_COMPANY = '" + 회사코드 + "' AND NO_AMS = '" + 관리번호 + "' ORDER BY NO_COL, NO_ROW");


				//


				// ********** 아이템 그리드
				grd금액.세팅시작(2);

				// 매입처 컬럼 삭제
				for (int j = grd금액.Cols.Count - 1; j > 0; j--)
				{					
					if (grd금액.컬럼이름(j).정수여부())
						grd금액.Cols.Remove(j);
				}

				// 컬럼추가
				for (int i = 0; i < 컬럼.Rows.Count; i++)
				{
					string 컬럼이름;

					if (i == 0)
						컬럼이름 = string.Format("{0:#,##0.##} 미만", 컬럼.Rows[i]["VALUE_T"]);
					else
						컬럼이름 = string.Format("{0:#,##0.##} 이상\n{1:#,##0.##} 미만", 컬럼.Rows[i]["VALUE_F"], 컬럼.Rows[i]["VALUE_T"]);

					grd금액.컬럼세팅(컬럼.Rows[i]["NO_COL"].문자(), 컬럼이름, 80, 포맷.비율);
				}

				grd금액.세팅종료(grd금액.세팅버전(), false);

				//grd금액.세팅시작(2);
				//grd금액.컬럼세팅("CD_COMPANY", "회사코드", false);
				//grd금액.컬럼세팅("NO_AMS", "관리번호", false);
				//grd금액.컬럼세팅("aaa", "이상미만", 80, 포맷.비율);

				////grd금액.기본키("CD_COMPANY", "NO_AMS");
				////grd금액.에디트컬럼("NM_AMS");
				//grd금액.세팅종료("23.04.01.01", false);




				DataTable 금액 = 디비.결과("PS_CZ_DX_AMS_L_AM", 회사코드, 관리번호);
				grd금액.바인딩(금액);


			}
			catch (Exception ex)
			{
				//UT.ShowMsg(ex);
			}
		}

		private void Btn금액_컬럼추가_Click(object sender, EventArgs e)
		{
			for (int i = 2; i < lay금액_컬럼.ColumnCount - 1; i++)
			{
				if (lay금액_컬럼.ColumnStyles[i].Width == 0)
				{
					lay금액_컬럼.ColumnStyles[i].Width = lay금액_컬럼.ColumnStyles[1].Width;
					break;
				}
			}
		}

		private void Btn금액_컬럼삭제_Click(object sender, EventArgs e)
		{
			for (int i = lay금액_컬럼.ColumnCount - 2; i > 1; i--)
			{
				if (lay금액_컬럼.ColumnStyles[i].Width > 0)
				{
					lay금액_컬럼.ColumnStyles[i].Width = 0;
					break;
				}
			}
		}

		private void Btn금액_라인추가_Click(object sender, EventArgs e)
		{
			for (int i = 2; i < lay금액_라인.RowCount - 1; i++)
			{
				if (lay금액_라인.RowStyles[i].Height == 0)
				{
					lay금액_라인.RowStyles[i].Height = lay금액_라인.RowStyles[1].Height;
					break;
				}
			}
		}

		private void Btn금액_라인삭제_Click(object sender, EventArgs e)
		{
			for (int i = lay금액_라인.RowCount - 2; i > 1; i--)
			{
				if (lay금액_라인.RowStyles[i].Height > 0)
				{
					lay금액_라인.RowStyles[i].Height = 0;
					break;
				}
			}
		}

		//private void Tab라인_Selected(object sender, TabControlEventArgs e)
		//{
		//	if (e.TabPage == tab매출처그룹)
		//	{
		//		spc라인.Panel2Collapsed = true;
		//	}
		//	else
		//	{
		//		spc라인.Panel2Collapsed = false;
		//	}
		//}

		private void Cbo접수일자_SelectionChangeCommitted(object sender, EventArgs e)
		{
			dtp접수일자.StartDateToString = 상수.오늘날짜.AddMonths(-cbo접수일자.값().정수()).ToString("yyyyMMdd");
			dtp접수일자.EndDateToString = 유틸.오늘();			
		}




		private void Btn숨기기H_Click(object sender, EventArgs e)
		{
			if (spc메인.Panel1Collapsed)
			{
				spc메인.Panel1Collapsed = false;
				spc라인.Panel2Collapsed = false;
				btn숨기기H.Text = "숨기기";
			}
			else
			{
				spc메인.Panel1Collapsed = true;
				spc라인.Panel2Collapsed = true;
				btn숨기기H.Text = "보이기";
			}
		}


		private void Btn숨기기L_Click(object sender, EventArgs e)
		{
			if (lay헤드.Visible)
			{
				lay헤드.Visible = false;
				lay라인.Visible = false;
				lay라인컨테이너.RowStyles[1].Height = 0;
				lay라인컨테이너.RowStyles[2].Height = 0;
				btn숨기기L.Text = "보이기";
			}
			else
			{
				lay라인컨테이너.RowStyles[1].Height = 높이_헤더레이아웃;
				lay라인컨테이너.RowStyles[2].Height = 높이_라인레이아웃;
				lay헤드.Visible = true;
				lay라인.Visible = true;
				btn숨기기L.Text = "숨기기";
			}
		}

		private void Ctx재고코드_QueryBefore(object sender, BpQueryArgs e)
		{
			e.HelpParam.P42_CD_FIELD2 = "009";
			e.HelpParam.IsRequireSearchKey = true;
		}

		private void Ctx품목분류_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
		{
			e.HelpParam.P41_CD_FIELD1 = "MA_B000031";
		}

		private void Tbx숫자형식_LostFocus(object sender, EventArgs e)
		{
			if (!grd라인.HasNormalRow)
				return;

			TextBoxExt tbx = (TextBoxExt)sender;

			if (tbx.Text == "")
				grd라인.데이터행()[tbx.태그()] = DBNull.Value;
		}

		#endregion

		#region ==================================================================================================== 조회 == SEARCH == ㄴㄷㅁㄱ

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			base.OnToolBarSearchButtonClicked(sender, e);

			try
			{
				// 전체 클리어
				lay헤드.클리어();
				lay라인.클리어();

				grd메인키.클리어();
				grd제외키.클리어();
				grd서브키1.클리어();
				grd서브키2.클리어();

				// 조회
				DataTable dt = 디비.결과("PS_CZ_DX_AMS", 상수.회사코드, tbx관리번호_검색.Text, tbx제목_검색.Text);
				grd헤드.바인딩(dt);
			}
			catch (Exception ex)
			{
				메시지.오류알람(ex);
			}
		}

		private void Grd헤드_AfterRowChange(object sender, RangeEventArgs e)
		{
			try
			{
				DataTable dt = grd헤드.상세그리드쿼리() ? 디비.결과("PS_CZ_DX_AMS_UM", 회사코드, 관리번호) : null;
				grd라인.바인딩(dt, grd헤드.상세그리드필터());

				// 자식의 자식 패널, 그리드는 바로 위 부모가 없을때 클리어가 안됨 => 필터에서 안나오도록 조치
				if (!grd라인.데이터테이블_화면().존재())
				{
					lay라인.클리어();

					grd메인키.바인딩(null, "1 != 1");
					grd제외키.바인딩(null, "1 != 1");
					grd서브키1.바인딩(null, "1 != 1");
					grd서브키2.바인딩(null, "1 != 1");
				}

				//금액테이블바인딩();


				// 매입처별로 기능을 제한
				if (grd헤드["CD_VENDOR"].문자() == "11823")
				{
					//ctx매입처.사용(false);
					//ctx매출처.사용(false);

					panelExt10.사용(false);
				}
				else
				{
					//ctx매입처.사용(true);
					//ctx매출처.사용(true);

					panelExt10.사용(true);
				}


			}
			catch (Exception ex)
			{
				메시지.오류알람(ex);
			}
		}

		private void Grd라인_AfterRowChange(object sender, RangeEventArgs e)
		{
			try
			{
				// 하위 그리드 바인딩
				DataSet ds = grd라인.상세그리드쿼리() ? 디비.결과s("PS_CZ_DX_AMS_UM_KEY", 회사코드, 관리번호, 아이템번호) : null;

				grd메인키.바인딩(ds?.Tables[0], grd라인.상세그리드필터());
				grd제외키.바인딩(ds?.Tables[1], grd라인.상세그리드필터());
				grd서브키1.바인딩(ds?.Tables[2], grd라인.상세그리드필터());
				grd서브키2.바인딩(ds?.Tables[3], grd라인.상세그리드필터());
			}
			catch (Exception ex)
			{
				메시지.오류알람(ex);
			}
		}

		private void Btn트레이닝_Click(object sender, EventArgs e)
		{
			try
			{
				메시지.작업중("작업시작");
				DateTime 시작시간 = DateTime.Now;

				디비 db = new 디비("PS_CZ_DX_AMS_TRN");
				db.변수.추가("@CD_COMPANY"	, 회사코드);
				db.변수.추가("@NO_AMS"		, 관리번호);
				db.변수.추가("@NO_ITEM"		, 아이템번호);
				db.변수.추가("@DT_F"			, dtp접수일자.StartDateToString);
				db.변수.추가("@DT_T"			, dtp접수일자.EndDateToString);

				db.변수.추가("@YN_MK"			, chk메인키검색.값());
				db.변수.추가("@SEQ_MK"		, rdo메인키전체.선택라디오().태그() == "Y" ? 0 : grd메인키["SEQ"].정수());
				db.변수.추가("@YN_SK"			, chk서브키검색.값());
				db.변수.추가("@SEQ_SK1"		, rdo서브키전체.선택라디오().태그() == "Y" ? 0 : grd서브키1["SEQ"].정수());
				db.변수.추가("@SEQ_SK2"		, rdo서브키전체.선택라디오().태그() == "Y" ? 0 : grd서브키2["SEQ"].정수());

				DataTable dt = db.결과();
				grd트레이닝.바인딩(dt);
				tab라인.SelectedTab = tab트레이닝;

				메시지.작업중();
				DateTime 종료시간 = DateTime.Now;
				lbl작업시간.Text = (종료시간 - 시작시간).ToString(@"hh\:mm\:ss");
			}
			catch (Exception ex)
			{
				메시지.오류알람(ex);
			}
}

		#endregion

		#region ==================================================================================================== 추가 == ADD == ㅁㅇㅇ

		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
			base.OnToolBarAddButtonClicked(sender, e);

			if (grd헤드.데이터테이블("NO_AMS = '추가'").존재())
			{
				메시지.경고알람("이미 추가 중인 항목이 있습니다.");
				return;
			}

			// 추가
			grd헤드.행추가();
			grd헤드["CD_COMPANY"]	= 상수.회사코드;
			grd헤드["NO_AMS"]		= "추가";
			grd헤드["CD_SALES"]		= "EQ";
			grd헤드.행추가완료();
		}

		private void Btn아이템추가_Click(object sender, EventArgs e)
		{
			grd라인.행추가();
			grd라인["CD_COMPANY"]	= 회사코드;
			grd라인["NO_AMS"]		= 관리번호;
			grd라인["NO_ITEM"]		= grd라인.최대값("NO_ITEM") + 1;


			// 라디오버튼은 바인딩 한번 해줘야함
			foreach (KeyValuePair<string, string> 기본값 in 라디오기본값s)
				grd라인[기본값.Key] = 기본값.Value;


			// 매입처마다 기본값 세팅
			if (grd헤드["CD_VENDOR"].문자() == "11823")
			{
				grd라인["FG_UM"] = "004";
			}


			grd라인.행추가완료();
		}

		private void Btn키워드추가_Click(object sender, EventArgs e)
		{
			FlexGrid 그리드;

			if (sender is Button btn)
				그리드 = (FlexGrid)Controls.Find("grd" + btn.Name.제거("btn", "추가"), true)[0];
			else
				그리드 = (FlexGrid)sender;
			
			그리드.행추가();
			그리드["CD_COMPANY"] = 회사코드;
			그리드["NO_AMS"]		= 관리번호;
			그리드["NO_ITEM"]	= 아이템번호;
			그리드["CD_TYPE"]	= 그리드.태그();
			그리드["SEQ"]		= 그리드.최대값("SEQ") + 1;
			그리드.행추가완료();
		}

		#endregion

		#region ==================================================================================================== 삭제 == DELETE == ㅇ딛

		public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{
			if (grd헤드.HasNormalRow)
				grd헤드.행삭제();
		}

		private void Btn아이템삭제_Click(object sender, EventArgs e)
		{
			if (grd라인.HasNormalRow)
				grd라인.행삭제();
		}

		private void Btn키워드삭제_Click(object sender, EventArgs e)
		{
			FlexGrid 그리드 = (FlexGrid)Controls.Find("grd" + (sender as Button).Name.제거("btn", "삭제"), true)[0];

			if (그리드.HasNormalRow)
				그리드.행삭제();
		}

		#endregion

		#region ==================================================================================================== 저장 == SAVE == ㄴㅁㅍㄷ

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)	
		{
			base.OnToolBarSaveButtonClicked(sender, e);
			if (!base.Verify()) return;

			try
			{
				// 저장
				디비 db = new 디비("PX_CZ_DX_AMS");
				db.변수.추가("@JSON_H", grd헤드.데이터테이블_수정().Json());
				db.변수.추가("@JSON_L", grd라인.데이터테이블_수정().Json());
				db.변수.추가("@JSON_MK", grd메인키.데이터테이블_수정().Json("CD_COMPANY", "NO_AMS", "NO_ITEM", "CD_TYPE", "SEQ", "KEY1_DSP", "KEY2_DSP", "KEY3_DSP", "KEY4_DSP"));
				db.변수.추가("@JSON_EX", grd제외키.데이터테이블_수정().Json("CD_COMPANY", "NO_AMS", "NO_ITEM", "CD_TYPE", "SEQ", "KEY1_DSP", "KEY2_DSP", "KEY3_DSP", "KEY4_DSP"));
				db.변수.추가("@JSON_SK1", grd서브키1.데이터테이블_수정().Json("CD_COMPANY", "NO_AMS", "NO_ITEM", "CD_TYPE", "SEQ", "KEY1_DSP", "KEY2_DSP", "KEY3_DSP", "KEY4_DSP"));
				db.변수.추가("@JSON_SK2", grd서브키2.데이터테이블_수정().Json("CD_COMPANY", "NO_AMS", "NO_ITEM", "CD_TYPE", "SEQ", "KEY1_DSP", "KEY2_DSP", "KEY3_DSP", "KEY4_DSP"));
				DataTable dt = db.결과();

				// 추가행 PK 업데이트
				grd헤드.데이터테이블()?.선택("NO_AMS = '추가'").업데이트("NO_AMS", dt.첫행()?["NO_AMS"]);
				grd라인.데이터테이블()?.선택("NO_AMS = '추가'").업데이트("NO_AMS", dt.첫행()?["NO_AMS"]);
				grd메인키.데이터테이블()?.선택("NO_AMS = '추가'").업데이트("NO_AMS", dt.첫행()?["NO_AMS"]);
				grd제외키.데이터테이블()?.선택("NO_AMS = '추가'").업데이트("NO_AMS", dt.첫행()?["NO_AMS"]);
				grd서브키1.데이터테이블()?.선택("NO_AMS = '추가'").업데이트("NO_AMS", dt.첫행()?["NO_AMS"]);
				grd서브키2.데이터테이블()?.선택("NO_AMS = '추가'").업데이트("NO_AMS", dt.첫행()?["NO_AMS"]);

				grd헤드.수정완료();
				grd라인.수정완료();
				grd메인키.수정완료();
				grd제외키.수정완료();
				grd서브키1.수정완료();
				grd서브키2.수정완료();

				메시지.저장완료();
			}
			catch (Exception ex)
			{
				메시지.오류알람(ex);
			}
		}

		#endregion
	}
}



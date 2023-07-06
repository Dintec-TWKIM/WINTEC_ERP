using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Duzon.Common.Forms;
using Duzon.Common.BpControls;

using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using DX;
using System.Linq;
using NeoBizBoxS2;

namespace cz
{
	public partial class P_CZ_MM_SCRAP_MGT : PageBase
	{
		private string 회사코드 => grd헤드["CD_COMPANY"].문자();
		private string 폐기번호 => grd헤드["NO_SCRAP"].문자();

		#region ==================================================================================================== 생성자

		public P_CZ_MM_SCRAP_MGT()
		{
			InitializeComponent();
		}

		#endregion

		#region ==================================================================================================== 초기화


		protected override void InitLoad()
		{
			this.페이지초기화();
			tbx폐기번호검색.엔터검색();

			// ********** 편집불가 패널
			pnl폐기번호.사용(false);
			pnl등록일자.사용(false);
			pnl담당자.사용(false);
			pnl결재상태.사용(false);
			pnl수불번호.사용(false);
			pnl전표번호.사용(false);

			//// ********** 콤보박스
			//DataSet ds = CODE.코드관리("PU_C000021");

			cbo구분.바인딩(CODE.코드관리("PU_C000021").선택("CD_FLAG2 = 'SCRAP'", "NAME"), true);
			//cbo지급조건.바인딩(ds.Tables[1], true);
			//cbo선적조건.바인딩(ds.Tables[2], true);

			InitGrid();
			InitEvent();
		}

		protected override void InitPaint()
		{
			spc메인.SplitterDistance = spc메인.Width -  1300;
			spc라인.SplitterDistance = spc라인.Height - 210;
		}

		#endregion

		#region ================================================================================================== 그리드 = GRID

		private void InitGrid()
		{
			MainGrids = this.컨트롤<FlexGrid>();

			// ********** 목록
			grd헤드.세팅시작(1);
			grd헤드.컬럼세팅("CD_COMPANY"	, "회사코드"		, false);
			grd헤드.컬럼세팅("NO_SCRAP"	, "폐기번호"		, 100	, 정렬.가운데);
			grd헤드.컬럼세팅("NM_SCRAP"	, "제목"			, 250);
			grd헤드.컬럼세팅("NM_EMP"		, "담당자"		, 80	, 정렬.가운데);
			grd헤드.컬럼세팅("ST_STAT"	, "전자결재"		, 60	, 정렬.가운데);
			grd헤드.컬럼세팅("NO_IO"		, "수불번호"		, 120	, 정렬.가운데);
			grd헤드.컬럼세팅("NO_SLIP"	, "전표번호"		, 120	, 정렬.가운데);
			grd헤드.컬럼세팅("ST_SLIP"	, "전표상태"		, 60	, 정렬.가운데);
			grd헤드.컬럼세팅("AM_SLIP"	, "전표금액"		, 85	, 포맷.원화단가);
			grd헤드.컬럼세팅("AM_SCRAP"	, "폐기금액"		, 85	, 포맷.원화단가);
			grd헤드.컬럼세팅("DC_RMK"		, "비고"			, 200);

			grd헤드.데이터맵("ST_STAT", 코드.코드관리("FI_J000031"));
			grd헤드.데이터맵("ST_SLIP", 코드.코드관리("FI_J000003"));
			grd헤드.기본키("CD_COMPANY", "NO_SCRAP");
			grd헤드.세팅종료("22.12.28.06", false);

			grd헤드.패널바인딩(lay헤드);
			grd헤드.상세그리드(grd라인, grd파일);

			// ********** 라인
			grd라인.세팅시작(1);
			grd라인.컬럼세팅("CD_COMPANY"	, "회사코드"		, false);
			grd라인.컬럼세팅("NO_SCRAP"	, "폐기번호"		, false);
			grd라인.컬럼세팅("SEQ"		, "순번"			, 45	, "####.##", 정렬.가운데);
			grd라인.컬럼세팅("CD_ITEM"	, "재고코드"		, 130);
			grd라인.컬럼세팅("NM_ITEM"	, "재고명"		, 280);
			grd라인.컬럼세팅("CD_SL"		, "창고"			, 110);
			grd라인.컬럼세팅("DT_GR"		, "최근입고일자"	, 90	, 포맷.날짜);
			grd라인.컬럼세팅("QT_INV"		, "창고수량"		, 70	, 포맷.수량);
			grd라인.컬럼세팅("QT"			, "폐기수량"		, 70	, 포맷.수량);
			grd라인.컬럼세팅("QT_NOW"		, "현재수량"		, 70	, 포맷.수량);
			grd라인.컬럼세팅("UM"			, "단가"			, 100	, 포맷.원화단가);
			grd라인.컬럼세팅("AM"			, "금액"			, 100	, 포맷.원화단가);
			grd라인.컬럼세팅("DC_REASON"	, "폐기사유"		, 200);

			grd라인.데이터맵("CD_SL", 코드.창고());
			grd라인.기본키("CD_COMPANY", "NO_SCRAP", "SEQ");
			grd라인.세팅종료("22.12.28.01", true);

			grd라인.에디트컬럼("CD_ITEM", "QT", "UM", "DC_REASON");
			grd라인.합계제외컬럼("SEQ", "UM");
			grd라인.복사붙여넣기(Grd라인_AfterEdit);

			// ********** 라인
			grd파일.세팅시작(1);
			grd파일.컬럼세팅("CD_COMPANY"	, "회사코드"		, false);
			grd파일.컬럼세팅("NO_SCRAP"	, "폐기번호"		, false);
			grd파일.컬럼세팅("SEQ"		, "순번"			, 45	, "####.##", 정렬.가운데);
			grd파일.컬럼세팅("DC_FILE"	, "파일명"		, 400);
			grd파일.컬럼세팅("NM_FILE"	, "경로"			, false);
			grd파일.컬럼세팅("CD_PATH"	, "위치"			, false);
			grd파일.컬럼세팅("SHORTENER"	, "쇼트너"		, false);

			grd파일.기본키("CD_COMPANY", "NO_SCRAP", "SEQ");
			grd파일.세팅종료("21.12.29.02", false);
		}

		#endregion

		#region ==================================================================================================== 이벤트 == EVENT

		private void InitEvent()
		{			
			ctx창고.QueryBefore += Ctx창고_QueryBefore;
			
			btn전자결재.Click += Btn전자결재_Click;
			btn전표처리.Click += Btn전표처리_Click;
			btn계정대체.Click += Btn계정대체_Click;

			btn품목추가.Click += Btn품목추가_Click;
			btn품목삭제.Click += Btn품목삭제_Click;
			btn파일추가.Click += Btn파일추가_Click;
			btn파일삭제.Click += Btn파일삭제_Click;

			grd헤드.AfterRowChange += Grd헤드_AfterRowChange;
			grd헤드.DoubleClick += Grd헤드_DoubleClick;

			grd라인.AfterEdit += Grd라인_AfterEdit;			
			grd파일.DoubleClick += Grd파일_DoubleClick;

			btn테스트.Click += Btn테스트_Click;
		}

		private void Btn테스트_Click(object sender, EventArgs e)
		{

			NeoBizBoxS2.Model.TMSG_MSG a = new NeoBizBoxS2.Model.TMSG_MSG();
			NeoBizBoxS2.Model.TMSG_ATTACH b = new NeoBizBoxS2.Model.TMSG_ATTACH();
			NeoBizBoxS2.Web.Ws.UploadInput c = new NeoBizBoxS2.Web.Ws.UploadInput();
			NeoBizBoxS2.Web.Ws.asmxUploadAction d = new NeoBizBoxS2.Web.Ws.asmxUploadAction();
			


			//string a = NeoBizBoxS2.Cryptography.AES.EncryptString("test.txt");

			//NeoBizBoxS2.Web.Areas

		}

		private void Ctx창고_QueryBefore(object sender, BpQueryArgs e)
		{
			e.HelpParam.P09_CD_PLANT = LoginInfo.CdPlant;
		}

		private void Btn전자결재_Click(object sender, EventArgs e)
		{
			// 문서번호 업데이트
			string 문서번호 = 회사코드 + "-" + 폐기번호;
			string query = "UPDATE CZ_MM_SCRAPH SET NO_DOCU = '" + 문서번호 + "' WHERE CD_COMPANY = '" + 회사코드 + "' AND NO_SCRAP = '" + 폐기번호 + "'";
			TSQL.실행(query);

			// 그룹웨어 연동
			string 제목 = grd헤드["NM_SCRAP"].문자();			
			string 본문 = @"
<div class='erp-contents'>	
	<div class='summary'>
		<div class='title'>
			1. 기본정보
		</div>
		<table class='dx-viewbox'>
			<colgroup>
				<col style='width:13%; text-align:center;' />
				<col style='width:20%; text-align:center;' />
				<col style='width:13%; text-align:center;' />
				<col style='width:20%; text-align:center;' />
				<col style='width:13%; text-align:center;' />
				<col style='width:20%; text-align:center;' />
			</colgroup>
			<tr>
				<th>폐기번호</th>
				<td>" + 폐기번호 + @"</td>
				<th>구분</th>
				<td>" + cbo구분.글() + @"</td>
				<th>대상창고</th>
				<td>" + grd헤드["NM_SL"] + @"</td>
			</tr>
			<tr>
				<th>파일번호</th>
				<td>" + grd헤드["NO_FILE"] + @"</td>
				<th>그룹웨어</th>
				<td colspan='3'><a href='" + grd헤드["SHORTENER_GW"] + "' target='_blank'>" + grd헤드["NO_GW"] + @"</a></td>
			</tr>
			<tr>
				<th>종수</th>
				<td>" + grd라인.표시데이터테이블().계산("COUNT(CD_ITEM)", "").문자("#,##0") + @"</td>
				<th>수량</th>
				<td>" + grd라인.표시데이터테이블().계산("SUM(QT)", "").문자("#,##0") + @"</td>
				<th>금액</th>
				<td>" + grd라인.표시데이터테이블().계산("SUM(AM)", "").문자("#,##0") + @"원</td>
			</tr>
			<tr>
				<th>폐기사유</th>
				<td colspan='5'>" + grd헤드["DC_REASON"].문자().웹() + @"</td>
			</tr>
			<tr>
				<th>첨부파일</th>
				<td colspan='5'>
					<ul>";

			for (int i = grd파일.Rows.Fixed; i < grd파일.Rows.Count; i++)
			{
				본문 += @"
					<li><a href='" + grd파일[i, "SHORTENER"] + "' target='_blank'>" + grd파일[i, "DC_FILE"] + @"</a></li>";
			}

			본문 += @"
					</ul>
				</td>
			</tr>
		</table>
	</div>
	<div class='item-list'>
		<div class='title'>
			2. 품목리스트
		</div>
		<table class='dx-viewbox'>
			<tr>
				<th class='col1'>순번</th>
				<th class='col2'>재고코드</th>
				<th class='col3'>재고명</th>
				<th class='col4'>입고일자</th>
				<th class='col5'>수량</th>
				<th class='col6'>단가</th>
				<th class='col7'>금액</th>
			</tr>";

			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
			{
				본문 += @"
			<tr>
				<td class='col1'>" + grd라인[i, "SEQ"] + @"</td>
				<td class='col2'>" + grd라인[i, "CD_ITEM"] + @"</td>
				<td class='col3'>" + grd라인[i, "NM_ITEM"] + @"</td>
				<td class='col4'>" + grd라인[i, "DT_GR"].문자("yyyy-MM-dd") + @"</td>
				<td class='col5'>" + grd라인[i, "QT"].문자("#,##0") + @"</td>
				<td class='col6'>" + grd라인[i, "UM"].문자("#,##0") + @"</td>
				<td class='col7'>" + grd라인[i, "AM"].문자("#,##0") + @"</td>
			</tr>";
				
			}

			본문 += @"
		</table>
	</div>
</div>";

			그룹웨어.저장(문서번호, 제목, 본문, 1023);
		}

		private void Btn전표처리_Click(object sender, EventArgs e)
		{
			try
			{
				// 결재 상태 확인
				DataTable dt = TSQL.결과("PS_CZ_MM_SCRAP_H", 회사코드, 폐기번호, 상수.사원번호);

				//if (dt.첫행()["ST_STAT"].문자() != "1")	유틸.경고발생("결재상태를 확인하세요");
				if (dt.첫행()["NO_SLIP"].문자() != "")	유틸.경고발생("이미 등록된 전표가 있습니다");

				// 전표 작성
				if (유틸.메세지("전표 작성을 하시겠습니까?", 메세지구분.일반선택))
				{
					tbx전표번호.Text = TSQL.결과("SP_CZ_MM_SCRAP_DOCU", 회사코드, 폐기번호, "D08").첫행()[0].문자(); ;
					유틸.메세지(공통메세지.자료가정상적으로저장되었습니다);
				}
			}
			catch (Exception ex)
			{
				유틸.메세지(ex);
			}
		}

		private void Btn계정대체_Click(object sender, EventArgs e)
		{
			try
			{
				// 결재 상태 확인
				DataTable dt = TSQL.결과("PS_CZ_MM_SCRAP_H", 회사코드, 폐기번호, 상수.사원번호);

				//if (dt.첫행()["ST_STAT"].문자() != "1")	유틸.경고발생("결재상태를 확인하세요");
				if (dt.첫행()["NO_IO"].문자() != "")		유틸.경고발생("이미 계정대체 되었습니다");

				// 계정 대체
				if (유틸.메세지("계정 대체 출고를 하시겠습니까?", 메세지구분.일반선택))
				{
					DataTable dtLine = grd라인.표시데이터테이블();
					dtLine.컬럼추가("FG_TPIO", typeof(string), grd헤드["CD_SCRAP"]);
				
					TSQL sql = new TSQL("PX_CZ_MM_QTIO_TRANS_GI");
					sql.변수.추가("@CD_COMPANY"	, 회사코드);
					sql.변수.추가("@NO_SCRAP"		, 폐기번호);
					sql.변수.추가("@DT_IO"		, grd헤드["DT_GI"]);
					sql.변수.추가("@DC_RMK"		, grd헤드["NM_SCRAP"]);
					sql.변수.추가("@JSON_L"		, dtLine.Json("CD_SL", "CD_ITEM", "FG_TPIO", "QT", "UM", "AM"));
					
					tbx수불번호.Text = sql.결과().첫행()[0].문자();
					유틸.메세지(공통메세지.자료가정상적으로저장되었습니다);
				}
			}
			catch (Exception ex)
			{
				유틸.메세지(ex);
			}
		}

		private void Btn품목추가_Click(object sender, EventArgs e)
		{		
			try
			{
				if (ctx창고.값() == "")
					유틸.경고발생("대상창고를 선택하세요.");

				grd라인.행추가();
				grd라인["CD_COMPANY"]	= 상수.회사코드;
				grd라인["NO_SCRAP"]		= 폐기번호;
				grd라인["SEQ"]			= grd라인.최대값("SEQ") + 1;
				grd라인["CD_SL"]			= ctx창고.값();
				grd라인.행추가완료();
			}
			catch (Exception ex)
			{
				유틸.메세지(ex);
			}
		}

		private void Btn품목삭제_Click(object sender, EventArgs e)
		{
			grd라인.행삭제();
		}

		private void Btn파일추가_Click(object sender, EventArgs e)
		{
			//파일 파일 = new 파일 { 여러파일선택 = true };
			//파일.파일선택();
			파일선택 fs = new 파일선택();
			fs.다중선택 = true;
			fs.열기();

			try
			{
				foreach (string s in fs.파일이름s)
				{ 
					grd파일.행추가();
					grd파일["CD_COMPANY"]	= 상수.회사코드;
					grd파일["NO_SCRAP"]		= 폐기번호;
					grd파일["SEQ"]			= grd파일.최대값("SEQ") + 1;
					grd파일["DC_FILE"]		= 파일.파일이름(s);
					grd파일["NM_FILE"]		= s;
					grd파일["CD_PATH"]		= "로컬";
					grd파일.행추가완료();
				}
			}
			catch (Exception ex)
			{
				유틸.메세지(ex);
			}
		}

		private void Btn파일삭제_Click(object sender, EventArgs e)
		{
			grd파일.행삭제();
		}

		private void Grd헤드_DoubleClick(object sender, EventArgs e)
		{
			CallOtherPageMethod("P_FI_DOCU", "전표입력(" + PageName + ")", "P_FI_DOCU", Grant, new object[] { grd헤드["NO_SLIP"].문자(), "1", 상수.회계코드, 상수.회사코드 });
			
		}

		private void Grd라인_AfterEdit(object sender, RowColEventArgs e)
		{
			string 컬럼 = grd라인.컬럼이름(e.Col);
			string 새값 = grd라인[e.Row, e.Col].문자();

			if (컬럼 == "CD_ITEM")
			{
				새값 = 새값.한글을영어().대문자();
				grd라인[e.Row, 컬럼] = 새값;

				if (TSQL.실행<DataTable>("PS_CZ_MM_SCRAP_L_GET", 상수.회사코드, 새값, ctx창고.값()) is DataTable dt && dt.Rows.Count > 0)
				{
					grd라인[e.Row, "NM_ITEM"]	= dt.Rows[0]["NM_ITEM"];
					grd라인[e.Row, "CD_SL"]		= ctx창고.값();
					grd라인[e.Row, "DT_GR"]		= dt.Rows[0]["DT_GR"];
					grd라인[e.Row, "QT_INV"]		= dt.Rows[0]["QT_INV"];
					grd라인[e.Row, "UM"]			= dt.Rows[0]["UM_STK"];
				}
				else
				{
					for (int j = grd라인.Cols.Fixed; j < grd라인.Cols.Count; j++)
					{
						if (!grd라인.컬럼이름(j).포함(grd라인.기본키()))
							grd라인[e.Row, j] = DBNull.Value;
					}
				}
			}
			else if (컬럼.포함("QT", "UM"))
			{
				grd라인[e.Row, "AM"] = grd라인[e.Row, "QT"].정수() * grd라인[e.Row, "UM"].실수();
			}
		}

		private void Grd파일_DoubleClick(object sender, EventArgs e)
		{
			// 헤더클릭
			//if (grd라인.MouseRow < grd라인.Rows.Fixed)
			//{
			//	SetGridStyle();
			//	return;
			//}

			// 첨부파일 열기
			string 컬럼이름 = grd파일.컬럼이름();

			if (컬럼이름 == "DC_FILE")
			{
				// ********** 파일 오픈
				string 유형 = grd파일["CD_PATH"].문자();

				if (유형 == "로컬")
				{
					파일.실행(grd파일["NM_FILE"].문자());
				}
				else if (유형 == "웹")
				{
					파일.다운로드(grd파일["CD_COMPANY"] + "/" + grd파일["NO_SCRAP"] + "/" + grd파일["NM_FILE"].문자(), true);
				}

			}
		}

		#endregion

		#region ==================================================================================================== 조회 == SEARCH

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			base.OnToolBarSearchButtonClicked(sender, e);
			
			try
			{
				TSQL sql = new TSQL("PS_CZ_MM_SCRAP_H");
				sql.변수.추가("@CD_COMPANY"	, 상수.회사코드);
				sql.변수.추가("@NO_SCRAP"		, "");
				sql.변수.추가("@NO_EMP"		, 상수.사원번호);
				DataTable dt = sql.결과();
				grd헤드.바인딩(dt);
			}
			catch (Exception ex)
			{
				유틸.메세지(ex);
			}
		}

		private void Grd헤드_AfterRowChange(object sender, RangeEventArgs e)
		{			
			DataTable dtLine = grd헤드.상세그리드쿼리() ? TSQL.결과("PS_CZ_MM_SCRAP_L", 회사코드, 폐기번호) : null;
			DataTable dtFile = grd헤드.상세그리드쿼리() ? TSQL.결과("PS_CZ_MM_SCRAP_F", 회사코드, 폐기번호) : null;

			grd라인.바인딩(dtLine, grd헤드.상세그리드필터());
			grd파일.바인딩(dtFile, grd헤드.상세그리드필터());

			if (grd헤드["ST_STAT"].문자().포함("2", "3"))
				편집여부(true);
			else
				편집여부(false);

			그리드스타일();
		}

		private void 편집여부(bool 사용)
		{
			pnl구분.사용(사용);
			pnl대상창고.사용(사용);

			pnl출고일자.사용(사용);
			pnl회계일자.사용(사용);

			pnl파일번호.사용(사용);
			pnl그룹웨어.사용(사용);

			pnl제목.사용(사용);
			pnl폐기사유.사용(사용);

			btn품목추가.사용(사용);
			btn품목삭제.사용(사용);
			btn파일추가.사용(사용);
			btn파일삭제.사용(사용);

			grd라인.사용(사용);
		}

		#endregion

		#region ==================================================================================================== 추가 = ADD

		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
			base.OnToolBarAddButtonClicked(sender, e);

			try
			{
				if (grd헤드.데이터테이블("NO_SCRAP = '추가'").존재())
					유틸.경고발생("이미 추가 중인 항목이 있습니다.");

				// 추가
				grd헤드.행추가();
				grd헤드["CD_COMPANY"] = 상수.회사코드;
				grd헤드["NO_SCRAP"]	 = "추가";
				grd헤드["DT_REG"]	 = 유틸.오늘(0);
				grd헤드["DT_GI"]		 = 유틸.오늘(0);
				grd헤드["DT_SLIP"]	 = 유틸.오늘(0);
				grd헤드["NO_EMP"]	 = 상수.사원번호;
				grd헤드["NM_EMP"]	 = 상수.사원이름;
				grd헤드["ST_STAT"]	 = "2";
				//grd헤드["CD_SCRAP"]	 = "분실";
				grd헤드.행추가완료();
			}
			catch (Exception ex)
			{
				유틸.메세지(ex);
			}
		}

		#endregion

		#region ==================================================================================================== 저장 == SAVE

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{			
			base.OnToolBarSaveButtonClicked(sender, e);
			if (!base.Verify()) return;

			try
			{
				// ********** 파일
				// 업로드
				foreach (DataRow row in grd파일.데이터테이블().선택("CD_PATH = '로컬'"))
				{
					string 로컬파일 = row["NM_FILE"].문자();
					string 대상경로 = row["CD_COMPANY"] + "/" + row["NO_SCRAP"];
					string 결과파일 = 파일.업로드(로컬파일, 대상경로, true);
					row["NM_FILE"] = 파일.파일이름(결과파일);
					row["CD_PATH"] = "웹";
					row["SHORTENER"] = 쇼트너.다운로드(결과파일);
				}

				// 서버에서 삭제
				foreach (DataRow row in grd파일.삭제데이터테이블().선택("CD_PATH = '웹'"))
				{
					파일.서버파일삭제(row["CD_COMPANY"] + "/" + row["NO_SCRAP"] + "/" + row["DC_FILE"]);
				}

				// ********** 저장
				// 그룹웨어 쇼트너 생성				
				foreach (DataRow row in grd헤드.데이터테이블().Rows)
				{
					if (row.RowState.포함(DataRowState.Modified, DataRowState.Added))
					{
						if (row["NO_GW"].문자() != "" && (row["SHORTENER_GW"].문자() == "" || row["NO_GW", DataRowVersion.Current].문자() != row["NO_GW", DataRowVersion.Original].문자()))
							row["SHORTENER_GW"] = 쇼트너.그룹웨어(row["NO_GW"].문자());
					}
				}

				// 저장
				TSQL sql = new TSQL("PX_CZ_MM_SCRAP");
				sql.변수.추가("@JSON_H", grd헤드.수정데이터테이블().Json());
				sql.변수.추가("@JSON_L", grd라인.수정데이터테이블().Json());
				sql.변수.추가("@JSON_F", grd파일.수정데이터테이블().Json());
				DataTable dt = sql.결과();

				// 추가행 PK 업데이트
				grd헤드.데이터테이블().선택("NO_SCRAP = '추가'").업데이트("NO_SCRAP", dt.첫행()?["NO_SCRAP"]);
				grd라인.데이터테이블().선택("NO_SCRAP = '추가'").업데이트("NO_SCRAP", dt.첫행()?["NO_SCRAP"]);
				grd파일.데이터테이블().선택("NO_SCRAP = '추가'").업데이트("NO_SCRAP", dt.첫행()?["NO_SCRAP"]);

				grd헤드.수정완료();
				grd라인.수정완료();
				grd파일.수정완료();

				유틸.메세지(공통메세지.자료가정상적으로저장되었습니다);
			}
			catch (Exception ex)
			{
				유틸.메세지(ex);
			}
		}

		#endregion

		#region ==================================================================================================== 삭제 = DELETE

		public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{
			grd헤드.행삭제();
		}

		#endregion

		public void 그리드스타일()
		{
			grd라인.그리기중지();
			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++) 그리드스타일(i);
			grd라인.그리기시작();
		}

		public void 그리드스타일(int 행)
		{
			string 수불번호 = grd헤드["NO_IO"].문자();

			bool 정상 = true;
			정상 = 수불번호 != "" && grd라인[행, "QT_INV"].정수() - grd라인[행, "QT"].정수() == grd라인[행, "QT_INV"].정수() - grd라인[행, "QT_NOW"].정수();
			정상 = 수불번호 == "" && grd라인[행, "QT"].정수() <= grd라인[행, "QT_NOW"].정수();

			grd라인.셀글자색_파랑강조(행, "QT_INV", 정상);
			grd라인.셀글자색_파랑강조(행, "QT", 정상);
			grd라인.셀글자색_파랑강조(행, "QT_NOW", 정상);

			//grd라인.셀글자색_빨강강조(행, "QT_NOW", 수불번호 != "" && grd라인[행, "QT_INV"].정수() - grd라인[행, "QT_INV"].정수())

			//// 재고매칭 상태 컬러
			//Color 매칭컬러;
			//if (grd라인[행, "CD_MATCH"].문자() == "CNF") 매칭컬러 = SystemColors.WindowText; // 확정(모든게 끝남)
			//else if (grd라인[행, "CD_MATCH"].문자() == "MCH") 매칭컬러 = Color.Blue;             // 재고매칭 완료
			//else if (grd라인[행, "CD_MATCH"].문자() == "SLD") 매칭컬러 = Color.Blue;             // 선택완료되고 저장되기 직전 상태
			//else if (grd라인[행, "CD_MATCH"].문자() == "EXC") 매칭컬러 = Color.Green;                // 예외
			//else if (grd라인[행, "CD_MATCH"].문자() == "N/A") 매칭컬러 = Color.DarkGray;         // 재고매칭 안됨
			//else if (grd라인[행, "CD_MATCH"].문자() == "CHK") 매칭컬러 = Color.Red;
			//else if (grd라인[행, "CD_MATCH"].문자() == "SEL") 매칭컬러 = Color.Red;
			//else if (grd라인[행, "CD_MATCH"].문자() == "SBJ") 매칭컬러 = Color.Red;
			//else 매칭컬러 = Color.Yellow;

			//grd라인.셀글자색(행, "CD_MATCH", 매칭컬러);

			//// 상급품이 있는 재고는 밑줄 표시			
			//grd라인.셀밑줄(행, "CD_ITEM", grd라인[행, "CD_ITEM_SPE"].문자() != "");

			//// 재고단가가 더 비싸면 경고
			//grd라인.셀글자색_빨강(행, "UM_STK", grd라인[행, "UM_KR_MIN"].정수() > 0 && grd라인[행, "UM_STK"].정수() > grd라인[행, "UM_KR_MIN"].정수());

			//// 매입처 견적 경과일이 30일 넘었으면 경고
			//grd라인.셀글자색_빨강(행, "DAY_MIN", grd라인[행, "DAY_MIN"].정수() > 30);

			//// 매입처선택을 했을 경우 파란색 표시
			//grd라인.셀글자색_파랑강조(행, "NM_VENDOR_INQ", grd라인[행, "CD_VENDOR_INQ"].문자() != "");
		}
	}
}

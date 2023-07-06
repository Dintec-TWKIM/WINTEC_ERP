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
using DX;
using System.Linq;
using System.IO;

using Aspose.Email.Outlook;

namespace cz
{
	public partial class P_CZ_SA_INQ_2 : PageBase
	{
		헤더 헤더 = new 헤더();
		string[] 최저단가컬럼;
		string 유형기본값;
		string 단위기본값;

		DataTable 단위_통합;

		P_CZ_SA_QTN_REG 견적 => (P_CZ_SA_QTN_REG)Parent.GetContainerControl();

		string 회사코드 => ((P_CZ_SA_QTN_REG)Parent.GetContainerControl()).회사코드;

		string 파일번호 => ((P_CZ_SA_QTN_REG)Parent.GetContainerControl()).파일번호;

		bool 편집가능 => grd라인.AllowEditing;

		public FlexGrid 라인 => grd라인;

		public P_CZ_SA_INQ_2() => InitializeComponent();

		#region ==================================================================================================== 초기화 == INIT == ㅑㅜㅑㅅ

		protected override void InitLoad()
		{
			this.페이지초기화();

			단위_통합 = 코드.단위_통합_선용();

			// 필터 태그 설정
			rdo전체.Tag = "";
			rdo확정대기.Tag	= "CD_MATCH_OLD IN ('MCH', 'SLD')";
			rdo확인대기.Tag	= "CD_MATCH_OLD IN ('CHK', 'SEL', 'SBJ')";
			
			rdo단위확인.Tag	= "(CD_MATCH_OLD != 'EXC' AND CD_ITEM != ''  AND UNIT_INQ_NEW != UNIT_STK_NEW)"
						+ "OR ((CD_MATCH_OLD == 'EXC'  OR CD_ITEM == '') AND UNIT_INQ_NEW != UNIT_MIN_NEW AND CD_VENDOR_MIN != '')";

			rdo매입처선택.Tag = "YN_VENDOR_INQ = 'Y'";

			// DOCK 변경
			pnl비고.Dock = DockStyle.Fill;		// 중간라인 안보이게 하려면 dock fill
			pnl기타.Dock = DockStyle.Fill;		// 숨기기버튼 오른쪽으로 보내버리기!

			pnl주제.Dock = DockStyle.Fill;		// dock fill 해야 주제 텍스트박스가 끝까지 갈수있음
			pnl품목명.Dock = DockStyle.Fill;
			pnl주제변경.Dock = DockStyle.Fill;

			// 콤보
			cbo유형.바인딩(코드.품목그룹(), false);
			cbo주제.바인딩(코드.코드관리("CZ_SA00043"), true);
			cbo단위.바인딩(코드.단위(), false);
			cbo유형변경.바인딩(cbo유형, false);
			cbo주제변경.바인딩(cbo주제, true);
			cbo단위변경.바인딩(cbo단위, false);

			// 유형, 단위 기본값
			유형기본값 = 견적.부품영업 ? "EQ" : "GS";
			단위기본값 = "PCS";
			
			cbo유형.값(유형기본값);
			cbo단위.값(단위기본값);

			cbo유형변경.값(유형기본값);
			cbo단위변경.값(단위기본값);

			// 기타
			tbx순번.순번양식();
			헤더.컨테이너(lay헤드);

			InitGrid();
			InitEvent();

		}

		protected override void InitPaint()
		{
			lay라인.RowStyles[7].SizeType = SizeType.Absolute;
			lay라인.RowStyles[7].Height = 29;

			//lay변경.RowStyles[3].SizeType = SizeType.Absolute;
			//lay변경.RowStyles[3].Height = 29;

			spc메인.SplitterDistance = spc메인.Width - 690;
			spc라인.SplitterDistance = spc라인.Height - 302;


			// 설정값 불러오기
			if (설정.가져오기("P_CZ_SA_INQ", "실행_숨기기버튼").문자() == "Y")
			{
				Btn확장_Click(null, null);
			}
		}

		//protected override onclo
		//protected override void OnClosed(EventArgs e)
		//{
		//	설정.저장하기("TAB_SEQ", tab메인.SelectedIndex);
		//	base.OnClosed(e);
		//}



		public void 클리어()
		{
			헤더.초기화();
			grd라인.초기화();
		}

		public void 사용(bool 사용)
		{			
			grd라인.AllowEditing = 사용;
		}

		#endregion

		#region ==================================================================================================== 그리드 == RMFLEM == GRID == ㅎ걍

		private void InitGrid()
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("TYPE");
			dt.Columns.Add("CODE");
			dt.Columns.Add("NAME");

			// 비오엠
			dt.Rows.Add("TP_BOM", "S", "");
			dt.Rows.Add("TP_BOM", "P", "▶");
			dt.Rows.Add("TP_BOM", "C", "┖");

			// 매입문의
			dt.Rows.Add("YN_VENDOR_INQ", "Y", "문의");
			dt.Rows.Add("YN_VENDOR_INQ", "N", "");

			// ********** 라인 그리드
			grd라인.세팅시작(2);

			grd라인.컬럼세팅("CHK"			, "S"			, 30	, 포맷.체크);
			grd라인.컬럼세팅("CD_COMPANY"		, "회사코드"		, false);
			grd라인.컬럼세팅("NO_FILE"		, "파일번호"		, false);
			grd라인.컬럼세팅("NO_HST"			, "차수"			, false);
			grd라인.컬럼세팅("NO_LINE"		, "항번"			, false);
			grd라인.컬럼세팅("NO_LINE_PARENT"	, "부모항번"		, false);
			grd라인.컬럼세팅("TP_BOM"			, "BOM"			, 35	, 정렬.가운데);
			grd라인.컬럼세팅("NO_DSP"			, "순번"			, 40	, "####.##", 정렬.가운데);
			grd라인.컬럼세팅("NM_SUBJECT"		, "주제"			, 130);
			grd라인.컬럼세팅("CD_UNIQ_PARTNER", "선사코드"		, 90);
			grd라인.컬럼세팅("CD_ITEM_PARTNER", "품목코드"		, 110);
			grd라인.컬럼세팅("NM_ITEM_PARTNER", "품목명"		, 340);
			grd라인.컬럼세팅("UNIT"			, "단위"			, 70	, 정렬.가운데);
			grd라인.컬럼세팅("QT"				, "수량"			, 50	, 포맷.수량);

			// 재고매칭
			grd라인.컬럼세팅("SPLITTER1"		, "", 1);
			grd라인.컬럼세팅("CD_MATCH"		, "재고매칭"		, "상태"			, 60	, 정렬.가운데);
			grd라인.컬럼세팅("CD_ITEM"		, "재고매칭"		, "재고코드"		, 105	, 정렬.가운데);
			grd라인.컬럼세팅("NM_ITEM"		, "재고매칭"		, "재고명"		, 110	, false);
			grd라인.컬럼세팅("UCODE"			, "재고매칭"		, "U코드"		, 90);
			grd라인.컬럼세팅("KCODE"			, "재고매칭"		, "K코드"		, 90);
			grd라인.컬럼세팅("NO_PART"		, "재고매칭"		, "파트넘버"		, 130);
			grd라인.컬럼세팅("DC_OFFER_STK"	, "재고매칭"		, "오퍼"			, 150);
			grd라인.컬럼세팅("UNIT_STK"		, "재고매칭"		, "단위"			, 60	, 정렬.가운데);
			grd라인.컬럼세팅("UM_STK"			, "재고매칭"		, "단가"			, 70	, 포맷.원화단가);
			
			// 가용재고
			grd라인.컬럼세팅("QT_AVST"		, "가용재고"		, "재고"			, 47, 포맷.수량);
			grd라인.컬럼세팅("QT_AVPO"		, "가용재고"		, "발주"			, 47, 포맷.수량);

			// 최저단가
			grd라인.컬럼세팅("SPLITTER2"		, "", 1);			
			grd라인.컬럼세팅("CD_VENDOR_MIN"	, "최저단가"		, "매입처코드"	, 50	, false);
			grd라인.컬럼세팅("NM_VENDOR_MIN"	, "최저단가"		, "매입처명"		, 75	, 정렬.가운데);
			grd라인.컬럼세팅("DC_OFFER_MIN"	, "최저단가"		, "오퍼"			, 150);
			grd라인.컬럼세팅("UNIT_MIN"		, "최저단가"		, "단위"			, 60	, 정렬.가운데);
			grd라인.컬럼세팅("UM_MIN"			, "최저단가"		, "단가"			, 70	, 포맷.원화단가);
			grd라인.컬럼세팅("LT_MIN"			, "최저단가"		, "납기"			, 40	, false);
			grd라인.컬럼세팅("DAY_MIN"		, "최저단가"		, "경과"			, 40	, 포맷.정수);

			최저단가컬럼 = new string[] { "NM_ITEM", "UCODE", "KCODE", "NO_PART", "DC_OFFER_STK", "UNIT_STK", "UNIT_STK_NEW", "UM_STK"
				, "QT_AVST", "QT_AVPO"
				, "CD_VENDOR_MIN", "NM_VENDOR_MIN", "DC_OFFER_MIN", "UNIT_MIN", "UM_MIN", "LT_MIN", "DAY_MIN"
				, "YN_VENDOR_INQ" };

			// 매입처
			grd라인.컬럼세팅("SPLITTER3"		, "", 1);
			grd라인.컬럼세팅("YN_VENDOR_INQ"	, "견적문의"		, "상태"			, 60	, 정렬.가운데);
			grd라인.컬럼세팅("CD_VENDOR_INQ"	, "견적문의"		, "매입처코드"	, 50	, false);
			grd라인.컬럼세팅("NM_VENDOR_INQ"	, "견적문의"		, "매입처명"		, 250);

			grd라인.컬럼세팅("DC_RMK"			, "비고"			, 250);
			
			// 기타
			grd라인.컬럼세팅("CD_ITEM_SPE"	, "재고코드(상급)", false);
			grd라인.컬럼세팅("TP_ENGINE"		, "엔진분류"		, false);
			grd라인.컬럼세팅("NO_ENGINE"		, "엔진번호"		, false);
			grd라인.컬럼세팅("GRP_ITEM"		, "유형"			, false);
			grd라인.컬럼세팅("CD_SUBJECT"		, "유형2"		, false);
			grd라인.컬럼세팅("SORT"			, "SORT"		, false);

			grd라인.기본키("NO_LINE");
			grd라인.필수값("NO_LINE", "GRP_ITEM", "NM_ITEM_PARTNER", "UNIT");
			grd라인.더미값("CD_COMPANY", "NO_FILE");
			
			grd라인.데이터맵("UNIT", 코드.단위());
			grd라인.데이터맵("CD_MATCH", 코드.코드관리("CZ_DX00022"));
			grd라인.데이터맵("UNIT_STK", 코드.단위());
			grd라인.데이터맵("UNIT_MIN", 코드.단위());

			grd라인.데이터맵("TP_BOM", dt.선택("TYPE = 'TP_BOM'"));
			grd라인.데이터맵("YN_VENDOR_INQ", dt.선택("TYPE = 'YN_VENDOR_INQ'"));

			grd라인.패널바인딩(lay엔진);
			grd라인.패널바인딩(lay라인);

			grd라인.헤더더블클릭(그리드스타일);
			grd라인.복사붙여넣기(Btn추가_Click, null);
			grd라인.시프트체크();

			grd라인.세팅종료("23.05.15.01", false, true);
			grd라인.Cols["CD_MATCH"].굵게();

			// 모드에 따라 컬럼 숨기기
			if (견적.부품영업)
			{

			}
			else
			{
				grd라인.컬럼에디트가능("CHK", "NO_DSP", "NM_SUBJECT", "CD_UNIQ_PARTNER", "CD_ITEM_PARTNER", "NM_ITEM_PARTNER", "UNIT", "QT", "DC_RMK");
				grd라인.컬럼숨기기("UCODE", "KCODE");
			}

			//grd라인.LoadUserCache("P_CZ_SA_QTN_REG_SINQ");

			// ********** 매입처검색
			grd매입처검색.세팅시작(1);
			grd매입처검색.컬럼세팅("CD_PARTNER"	, "코드"			, 60	, 정렬.가운데);
			grd매입처검색.컬럼세팅("NM_PARTNER"	, "매입처명"		, 250);
			grd매입처검색.컬럼세팅("NM_CEO"		, "대표자"		, 80	, 정렬.가운데);
			grd매입처검색.컬럼세팅("NO_COMPANY"	, "사업자번호"	, 120	, 정렬.가운데);
			grd매입처검색.세팅종료("22.11.23.01", false, true);

			// ********** 매입처선택
			grd매입처선택.세팅시작(1);
			grd매입처선택.컬럼세팅("CD_PARTNER"	, "코드"			, 60	, 정렬.가운데);
			grd매입처선택.컬럼세팅("NM_PARTNER"	, "매입처명"		, 250);
			grd매입처선택.컬럼세팅("NM_CEO"		, "대표자"		, 80	, 정렬.가운데);
			grd매입처선택.컬럼세팅("NO_COMPANY"	, "사업자번호"	, 120	, 정렬.가운데);
			grd매입처선택.세팅종료("22.09.14.01", false, true);
		}

		#endregion

		#region ==================================================================================================== 이벤트 == EVENT == ㄷㅍ둣

		private void InitEvent()
		{
			btn테스트.Click += Btn테스트_Click;

			lay라인.Resize += Lay라인_Resize;
			lay변경.Resize += Lay변경_Resize;


			btn확장.Click += Btn확장_Click;

			ctx매출처담당자.QueryBefore += Ctx매출처담당자_QueryBefore;
			cur수량.KeyDown += Cur수량_KeyDown;
			cbo단위.KeyDown += Cbo단위_KeyDown;




			btn추가.Click += Btn추가_Click;
			btn삽입.Click += Btn삽입_Click;
			btn삭제.Click += Btn삭제_Click;


			btn재고매칭.Click += Btn재고매칭_Click;
			
			btn단가확정.Click += Btn단가확정_Click;
			btn확정취소.Click += Btn확정취소_Click;
			btn재고예외.Click += Btn재고예외_Click;


			btn매입처선택.Click += Btn매입처선택_Click;

			btnBOM등록.Click += BtnBOM등록_Click;





			btn재정렬.Click += Btn재정렬_Click;


			grd라인.DoubleClick += Grd라인_DoubleClick;
		


			btn엔터리셋.Click += Btn엔터리셋_Click;






			tbx매입처검색.KeyDown += Tbx매입처검색_KeyDown;
			btn매입처검색.Click += Btn매입처검색_Click;
			btn매입처추가.Click += Btn매입처추가_Click;
			btn매입처삭제.Click += Btn매입처삭제_Click;
			grd매입처검색.DoubleClick += Grd매입처검색_DoubleClick;
			grd매입처선택.DoubleClick += Grd매입처선택_DoubleClick;



			rdo전체.Click += Rdo전체_Click;
			rdo확정대기.Click += Rdo전체_Click;
			rdo확인대기.Click += Rdo전체_Click;
			rdo매입처선택.Click += Rdo전체_Click;
			rdo단위확인.Click += Rdo전체_Click;


			btn유형변경.Click += Btn유형변경_Click;
			btn주제변경.Click += Btn주제변경_Click;
			btn단위변경.Click += Btn단위변경_Click;
		}

		
		private void Btn유형변경_Click(object sender, EventArgs e)
		{
			decimal st = tbx범위시작.Text.실수();
			decimal ed = tbx범위종료.Text.실수() == 0 ? 10000000 : tbx범위종료.Text.실수();

			foreach (DataRow row in grd라인.데이터테이블().선택("NO_DSP >= " + st + " AND NO_DSP <= " + ed))
			{
				row["GRP_ITEM"] = cbo유형변경.값();
				row["CD_SUBJECT"] = cbo주제변경.값();
			}

			grd라인.행수정완료();
		}

		private void Btn주제변경_Click(object sender, EventArgs e)
		{
			decimal st = tbx범위시작.Text.실수();
			decimal ed = tbx범위종료.Text.실수() == 0 ? 10000000 : tbx범위종료.Text.실수();

			foreach (DataRow row in grd라인.데이터테이블().선택("NO_DSP >= " + st + " AND NO_DSP <= " + ed))
				row["NM_SUBJECT"] = tbx주제변경.Text;
				
			grd라인.행수정완료();
		}

		private void Btn단위변경_Click(object sender, EventArgs e)
		{
			string 단위_신 = 단위_통합.선택("UNIT = '" + cbo단위변경.값() + "'")[0]["UNIT_NEW"].문자();
			decimal st = tbx범위시작.Text.실수();
			decimal ed = tbx범위종료.Text.실수() == 0 ? 10000000 : tbx범위종료.Text.실수();

			foreach (DataRow row in grd라인.데이터테이블().선택("NO_DSP >= " + st + " AND NO_DSP <= " + ed))
			{
				row["UNIT"] = cbo단위변경.값();
				row["UNIT_INQ_NEW"] = 단위_신;
			}
			
			grd라인.행수정완료();
			그리드스타일();	// 얘는 단위 색깔 표시 때문에 스타일 한번 먹여줌
		}





		private void Rdo전체_Click(object sender, EventArgs e)
		{
			grd라인.그리기중지();
			grd라인.필터(rdo전체.선택().태그());			
			그리드스타일();			
		}
		
		private void Btn확장_Click(object sender, EventArgs e)
		{
			if (spc메인.Panel2Collapsed)
			{
				// 접혀 있으면 펼치기
				spc메인.Panel2Collapsed = false;
				btn확장.Text = "숨기기";
			}
			else
			{
				// 펼쳐져 있으면 접기
				spc메인.Panel2Collapsed = true;
				btn확장.Text = "보이기";
			}
		}

		private void Btn재고매칭_Click(object sender, EventArgs e)
		{
			try
			{
				// 최초 돌릴땐 체크 무시하고 전체 대상
				DataTable 대상 = grd라인.데이터테이블("ISNULL(CD_MATCH, '') != ''").존재() ? grd라인.데이터테이블("CHK = 'Y'") : grd라인.데이터테이블();

				if (!대상.존재())
					메시지.경고발생(메시지코드.선택된자료가없습니다);

				메시지.작업중("작업시작");
				grd라인.포커스저장();

				if (견적.저장())
				{					
					디비.결과("PI_CZ_SA_INQ_STMCH_GS", 대상.선택("ISNULL(CD_MATCH, '') NOT IN ('CNF')").Json("CD_COMPANY", "NO_FILE", "NO_LINE"));	// 컨컴 빼고 다다시
					조회_라인();
				}

				grd라인.포커스조회();
				메시지.작업중();
			}
			catch (Exception ex)
			{
				grd라인.그리기시작();
				메시지.오류알람(ex);
			}
		}
		
		private void Btn단가확정_Click(object sender, EventArgs e)
		{
			try
			{
				if (!grd라인.데이터테이블("CHK = 'Y'").존재())
					메시지.경고발생(메시지코드.선택된자료가없습니다);

				메시지.작업중("작업시작");
				grd라인.포커스저장();

				if (견적.저장())
				{
					디비.실행("PI_CZ_SA_INQ_UMCNF_GS", grd라인.데이터테이블("ISNULL(CD_MATCH, '') IN ('MCH', 'SLD') AND CHK = 'Y'").Json());    // 파랭이 중에 선택된 것만
					조회_라인();
				}

				grd라인.포커스조회();
				메시지.작업중();
			}
			catch (Exception ex)
			{
				grd라인.그리기시작();
				메시지.오류알람(ex);
			}
		}

		private void Btn확정취소_Click(object sender, EventArgs e)
		{
			try
			{
				if (!grd라인.데이터테이블("CHK = 'Y'").존재())
					메시지.경고발생(메시지코드.선택된자료가없습니다);

				메시지.작업중("작업시작");
				grd라인.포커스저장();

				if (견적.저장())
				{
					디비.실행("PI_CZ_SA_INQ_UMCNF_CANCEL", grd라인.데이터테이블_선택().Json("CD_COMPANY", "NO_FILE", "NO_LINE"));
					조회_라인();
				}

				grd라인.포커스조회();
				메시지.작업중();
			}
			catch (Exception ex)
			{
				grd라인.그리기시작();
				메시지.오류알람(ex);
			}
		}


		private void Btn재고예외_Click(object sender, EventArgs e)
		{
			try
			{
				if (!grd라인.데이터테이블("CHK = 'Y'").존재())
					메시지.경고발생(메시지코드.선택된자료가없습니다);

				// ********** 예외 처리
				메시지.작업중("작업시작");
				grd라인.그리기중지();

				for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
				{
					if (grd라인[i, "CHK"].문자() != "Y") continue;
					grd라인[i, "CHK"] = "N";

					// 재고코드 있는 놈만
					if (grd라인[i, "CD_ITEM"].문자() == "")
						continue;

					// 상태 변경
					grd라인[i, "CD_MATCH"] = "EXC";

					// XXXX 코드로 변경
					string 재고코드 = grd라인[i, "CD_ITEM"].문자();
					재고코드 = 재고코드.발생("-") ? 재고코드.분할("-")[0] : 재고코드;
					재고코드 += "-XXXX";

					string query = "SELECT CD_ITEM FROM MA_PITEM WITH(NOLOCK) WHERE CD_COMPANY = '" + 견적.회사코드 + "' AND CD_ITEM = '" + 재고코드 + "'";
					DataTable dt = 디비.결과(query);
					grd라인[i, "CD_ITEM"] = dt.존재() ? 재고코드 : "";

					// 관련 컬럼 내용 지우기
					DataRow 행 = grd라인.데이터테이블().선택("NO_LINE = " + grd라인[i, "NO_LINE"])[0];
					foreach (string s in 최저단가컬럼) 행[s] = DBNull.Value;

					그리드스타일(i);
				}

				grd라인.그리기시작();
				메시지.작업중();
			}
			catch (Exception ex)
			{
				grd라인.그리기시작();
				메시지.오류알람(ex);
			}
		}

		private void Btn매입처선택_Click(object sender, EventArgs e)
		{
			try
			{
				if (!grd라인.데이터테이블("CHK = 'Y'").존재())
					메시지.경고발생(메시지코드.선택된자료가없습니다);

				// ********** 매입처 선택
				H_CZ_VENDOR_SEL f = new H_CZ_VENDOR_SEL();

				if (f.ShowDialog() != DialogResult.OK)
					return;

				if (!f.결과.존재())
					메시지.경고발생(메시지코드.선택된자료가없습니다);

				// 바인딩
				grd라인.그리기중지();
				string 코드 = f.결과.결합(",", "CD_PARTNER"); // ,는 트림 불편하니 빈칸 없앰
				string 이름 = f.결과.결합(", ", "SN_PARTNER");

				for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
				{
					if (grd라인[i, "CHK"].문자() != "Y") continue;
					grd라인[i, "CHK"] = "N";
					
					grd라인[i, "CD_VENDOR_INQ"] = 코드;
					grd라인[i, "NM_VENDOR_INQ"] = 이름;
					그리드스타일(i);
				}

				grd라인.그리기시작();
			}
			catch (Exception ex)
			{
				grd라인.그리기시작();
				메시지.오류알람(ex);
			}
		}

		



		



		private void Btn재정렬_Click(object sender, EventArgs e)
		{
			if (!grd라인.HasNormalRow)
				return;

			// 소수점을 찾아서 그만큼 증가시키고 아니면 1 증가 (대부분 1증가 임)
			string 순번 = grd라인["NO_DSP"].문자(".##");
			double 증가값 = 순번.발생(".") ? 1 / Math.Pow(10, 순번.분할(".")[1].Length) : 1;

			// 재정렬
			for (int i = grd라인.Selection.r1 + 1; i <= grd라인.Selection.r2; i++)
				grd라인[i, "NO_DSP"] = grd라인[i - 1, "NO_DSP"].실수() + (decimal)증가값;


		}



		private void Lay라인_Resize(object sender, EventArgs e)
		{			
			// 가로
			int 가로;
			가로 = pnl주제.Width - tbx주제.Left - 55;
			가로 = 가로 > 500 ? 가로 : 500;			
			tbx주제.Width = 가로;
			tbx품목코드.Width = tbx주제.Width;
			tbx품목명.Width = tbx주제.Width;

			// 세로
			tbx주제.Height = pnl주제.Height - (tbx주제.Top * 2);
			tbx품목명.Height = pnl품목명.Height - (tbx품목명.Top * 2);
		}

		private void Lay변경_Resize(object sender, EventArgs e)
		{
			tbx주제변경.Width = tbx주제.Width;
			tbx주제변경.Height = pnl주제변경.Height - (tbx주제변경.Top * 2);
		}

		private void Ctx매출처담당자_QueryBefore(object sender, BpQueryArgs e)
		{
			e.HelpParam.P00_CHILD_MODE = "매출처 담당자";

			// 컬럼 (첫번째 문자부터 첫빈칸까지를 WHERE에 들어가는 컬럼이름, 그래서 컬럼 이름 다음에 ,는 반드시 빈칸 있어야함)
			e.HelpParam.P61_CODE1 = @"NAME , CODE";

			// FROM 이후
			e.HelpParam.P62_CODE2 = @"
(
	SELECT
		NAME = NM_PTR
	,	CODE = CONVERT(NVARCHAR(3), SEQ)
	FROM FI_PARTNERPTR WITH(NOLOCK)
	WHERE 1 = 1
		AND CD_COMPANY = '" + 견적.회사코드 + @"'
		AND CD_PARTNER = '" + 견적.매출처코드 + @"'
		AND TP_PTR IN ('000','001','002')
) AS A";
			// WHERE
			e.HelpParam.P63_CODE3 = "WHERE 1 = 1";

			// ORDER BY
			e.HelpParam.P64_CODE4 = "ORDER BY NAME";
		}

		private void Cur수량_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.엔터()) Btn추가_Click(null, null);
		}

		private void Cbo단위_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.엔터()) Btn추가_Click(null, null);
		}

		private void Btn추가_Click(object sender, EventArgs e)
		{
			// 행 추가할때 콤보 값이 바뀌므로 미리 저장
			string 유형 = grd라인.HasNormalRow ? grd라인[grd라인.Rows.Count - 1, "GRP_ITEM"].문자()		: cbo유형.값();
			string 위치 = grd라인.HasNormalRow ? grd라인[grd라인.Rows.Count - 1, "CD_SUBJECT"].문자()	: cbo주제.값();
			string 주제 = grd라인.HasNormalRow ? grd라인[grd라인.Rows.Count - 1, "NM_SUBJECT"].문자()	: "";
			string 단위 = grd라인.HasNormalRow ? grd라인[grd라인.Rows.Count - 1, "UNIT"].문자()			: cbo단위.값();

			grd라인.행추가();
			grd라인["NO_LINE"]		= grd라인.최대값("NO_LINE") + 1;
			grd라인["NO_DSP"]		= grd라인.최대값("NO_DSP") + 1;
			grd라인["GRP_ITEM"]		= 유형;
			grd라인["CD_SUBJECT"]	= 위치;
			grd라인["NM_SUBJECT"]	= 주제;
			grd라인["UNIT"]			= 단위;
			grd라인["QT"]			= 0;
			grd라인["TP_BOM"]		= "S";

			// 버튼 클릭했을 때 이벤트
			if (sender != null)
			{
				grd라인.행추가완료();

				// 포커스 이동
				if (tbx주제.Text == "")
					tbx주제.Focus();
				else
					tbx품목코드.Focus();
			}
		}

		private void Btn삽입_Click(object sender, EventArgs e)
		{
			// 현재행 체크
			int row = grd라인.Row;
			if (row < grd라인.Rows.Fixed) return;

			// 첫행일때랑 아닐때랑 구분
			string 유형 = row == grd라인.Rows.Fixed ? grd라인[row, "GRP_ITEM"].문자()		: grd라인[row - 1, "GRP_ITEM"].문자();
			string 위치 = row == grd라인.Rows.Fixed ? grd라인[row, "CD_SUBJECT"].문자()	: grd라인[row - 1, "CD_SUBJECT"].문자();
			string 주제 = row == grd라인.Rows.Fixed ? grd라인[row, "NM_SUBJECT"].문자()	: grd라인[row - 1, "NM_SUBJECT"].문자();
			string 단위 = row == grd라인.Rows.Fixed ? grd라인[row, "UNIT"].문자()			: grd라인[row - 1, "UNIT"].문자();

			grd라인.행삽입(row);
			grd라인["NO_LINE"]		= grd라인.최대값("NO_LINE") + 1;
			grd라인["GRP_ITEM"]		= 유형;
			grd라인["CD_SUBJECT"]	= 위치;
			grd라인["NM_SUBJECT"]	= 주제;
			grd라인["UNIT"]			= 단위;
			grd라인.행삽입완료();
			grd라인.SetCellImage(grd라인.Row, grd라인.Cols["CD_ITEM_PARTNER"].Index, Icons.Empty_12x6);
		}

		private void Btn삭제_Click(object sender, EventArgs e)
		{
			grd라인.그리기중지();

			// 체크된게 있는지 확인하고 없으면 마우스 선택된거 삭제
			DataRow[] 체크 = grd라인.데이터테이블().선택("CHK = 'Y'");

			if (체크.Length > 0)
			{
				체크.삭제();
			}
			else
			{
				CellRange 범위 = grd라인.Selection;

				for (int i = 범위.r2; i >= 범위.r1; i--)
				{
					string 항번 = grd라인[i, "NO_LINE"].문자();
					string 부모항번 = grd라인[i, "NO_LINE_PARENT"].문자();
					string 비오엠 = grd라인[i, "TP_BOM"].문자();

					// 해당 행 삭제 (대량으로 지울때는 데이터테이블에서 지우는게 훨 빠름)
					grd라인.데이터테이블().삭제("NO_LINE = " + 항번);

					// BOM 항목이 있는지 검색
					if (비오엠 == "P")
					{
						grd라인.데이터테이블().삭제("NO_LINE_PARENT = " + 항번);
					}
					else if (비오엠 == "C")
					{
						if (grd라인.데이터테이블().선택("NO_LINE_PARENT = " + 부모항번).Length == 0)
						{
							grd라인.데이터테이블().선택("NO_LINE = " + 부모항번)[0]["TP_BOM"] = "S";
							grd라인.SetCellImage(i - 1, grd라인.Cols["CD_ITEM_PARTNER"].Index, Icons.Empty_12x6);
						}
					}
				}
			}

			grd라인.그리기시작();
		}

		private void BtnBOM등록_Click(object sender, EventArgs e)
		{
			// 현재 행이 싱글이면 부모로 변경
			if (grd라인["TP_BOM"].문자() == "S") 	grd라인["TP_BOM"] = "P";

			// 바인딩 할 부모값 저장
			int 부모항번 = grd라인["TP_BOM"].문자() == "P" ? grd라인["NO_LINE"].정수() : grd라인["NO_LINE_PARENT"].정수();
			DataRow 부모행 = grd라인.데이터테이블().선택("NO_LINE = " + 부모항번)[0];
			string 유형 = 부모행["GRP_ITEM"].문자();
			string 위치 = 부모행["CD_SUBJECT"].문자();
			string 주제 = 부모행["NM_SUBJECT"].문자();
			string 단위 = 부모행["UNIT"].문자();

			// 다음 부모 찾아서 그 위치에 삽입
			bool 찾았음 = false;

			for (int i = grd라인.Row + 1; i < grd라인.Rows.Count; i++)
			{
				if (grd라인[i, "TP_BOM"].문자().포함("S", "P"))
				{
					grd라인.행삽입(i);
					찾았음 = true;
					break;
				}
			}
			
			// 못찾았으면 젤끝에 추가
			if (!찾았음) grd라인.행추가();

			// 기본값 설정
			grd라인["NO_LINE"]			= grd라인.최대값("NO_LINE") + 1;
			grd라인["NO_LINE_PARENT"]	= 부모항번;
			grd라인["GRP_ITEM"]			= 유형;
			grd라인["CD_SUBJECT"]		= 위치;
			grd라인["NM_SUBJECT"]		= 주제;
			grd라인["UNIT"]				= 단위;
			grd라인["QT"]				= 0;
			grd라인["TP_BOM"]			= "C";
			grd라인.행추가완료();

			// 포커스 변경
			tbx품목코드.Focus();
		}























		private void Btn엔터리셋_Click(object sender, EventArgs e)
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
			dbm.AddParameter("@CD_COMPANY", 견적.회사코드);
			dbm.AddParameter("@NO_FILE", 견적.파일번호);
			dbm.ExecuteNonQuery();

			조회();
		}

		private void Btn테스트_Click(object sender, EventArgs e)
		{
			btn재고매칭.사용(!btn재고매칭.Enabled);

			//string q = @"SELECT 메일주소 FROM [dbo].[Sheet1$] GROUP BY 메일주소";
			//DataTable a = 디비.결과(q);

			//foreach (DataRow row in a.Rows)
			//{
			//	foreach (string s in row["메일주소"].문자().분할(";"))
			//	{
			//		if (Regex.Match(s, 상수.메일패턴).Value == "")
			//		{

			//		}
			//	}				
			//}



			//foreach (Match m in Regex.Matches(mail.내용, 상수.메일패턴, RegexOptions.IgnoreCase | RegexOptions.Singleline))
			//	파싱메일.Add(m.Value);

			return;

			////파일 a = new 파일();
			//FolderBrowserDialog f = new FolderBrowserDialog();

			//if (f.ShowDialog() == DialogResult.OK)
			//{
			//	DirectoryInfo dir = new DirectoryInfo(f.SelectedPath);

			//	foreach (FileInfo s in dir.GetFiles())
			//	{
			//		string 원래이름 = s.FullName;;
			//		string 바꿀이름 = 파일.파일이름(원래이름);
			//		바꿀이름 = 바꿀이름.Replace("[헬로카봇 시즌12] ", "");
			//		바꿀이름 = 바꿀이름.Replace("화 - ", " ");
			//		//바꿀이름 = 바꿀이름.Replace("-2화 ", "-2 ");
			//		//바꿀이름 = 바꿀이름.Replace(" [BEYBLADE BURST CHO-Z ANIMATION]", "");
			//		바꿀이름 = 바꿀이름.트림();

			//		파일.이름변경(원래이름, 바꿀이름);
			//	}
			//}
			
			//return;


			메시지.작업중("시작");
			
			for (int i = 0; i < 10; i++)
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
	AND DTS_INSERT >= '20210101'
	AND DTS_DX IS NULL";

				DataTable dt = 디비.결과(query);
				키워드.견적저장(dt);
			}

			메시지.작업중();
		}

		private void Tbx매입처검색_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
				Btn매입처검색_Click(null, null);
		}

		private void Btn매입처검색_Click(object sender, EventArgs e)
		{
			if (tbx매입처검색.Text.Trim() == "")
			{
				유틸.메세지("검색어를 입력하세요", 메세지구분.경고알람);
				return;
			}

			매입처검색(tbx매입처검색.Text, false);
		}

		private void Btn매입처추가_Click(object sender, EventArgs e)
		{
			// 중복 체크
			if (grd매입처선택.DataTable.Select("CD_PARTNER = '" + grd매입처검색["CD_PARTNER"] + "'").Length > 0)
			{
				유틸.메세지("이미 추가된 업체입니다.", 메세지구분.경고알람);
				return;
			}

			// 추가
			grd매입처선택.Rows.Add();
			grd매입처선택.Row = grd매입처선택.Rows.Count - 1;

			for (int i = grd매입처선택.Row, j = 0; j < grd매입처검색.Cols.Count; j++)
				grd매입처선택[i, j] = grd매입처검색[i, j];

			grd매입처선택.AddFinished();
		}

		private void Btn매입처삭제_Click(object sender, EventArgs e)
		{
			grd매입처선택.Rows.Remove(grd매입처선택.Row);
		}

		private void Grd매입처검색_DoubleClick(object sender, EventArgs e)
		{
			Btn매입처추가_Click(null, null);
		}

		private void Grd매입처선택_DoubleClick(object sender, EventArgs e)
		{
			Btn매입처삭제_Click(null, null);
		}


















		private void Grd라인_DoubleClick(object sender, EventArgs e)
		{
			int 행 = grd라인.MouseRow;

			// ********** 재고매칭 팝업
			if (편집가능 && grd라인.컬럼이름().포함("CD_MATCH", "CD_ITEM") && !grd라인["CD_MATCH"].문자().포함("N/A", "EXC"))
			{
				H_CZ_MATCH_STK f = new H_CZ_MATCH_STK(견적.회사코드, 견적.파일번호, grd라인.데이터테이블("NO_LINE = " + grd라인["NO_LINE"]).첫행(), 견적.부품영업);
				f.Width = 1920;

				if (f.ShowDialog() == DialogResult.OK)
				{
					// 기본 컬럼
					grd라인["CD_MATCH"] = "SLD";
					grd라인["CD_ITEM"] = f.재고아이템["CD_ITEM"];
					
					// 최저단가 컬럼 채우기
					foreach (string s in 최저단가컬럼) grd라인[s] = f.재고아이템[s];					

					그리드스타일(행);
				}
			}
		}

		//private  void 매입문의판단(DataTable)

		#endregion

		#region ==================================================================================================== 조회 == SEARCH == ㄴㄷㅁㄱ초

		public void 조회()
		{
			// 전체 조회 일때만 필터 다시 원래대로 돌림
			rdo전체.Checked = true;

			// 조회
			DataTable 헤드 = 디비.결과("PS_CZ_SA_INQ_H_R3", 회사코드, 파일번호);
			헤더.바인딩(헤드);
			조회_라인();

			// 엔진유형 바인딩
			//SetEngineType(견적.ImoNumber);
		}

		public void 조회_라인()
		{
			DataTable 라인 = 디비.결과("PS_CZ_SA_INQ_L_2", 회사코드, 파일번호);

			// 리드로우 때문에 원시적으로 해줌
			grd라인.Redraw = false;
			grd라인.Binding = 라인;

			// 필터 적용
			Rdo전체_Click(null, null);
		}

		#endregion

		#region ==================================================================================================== 저장 == SAVE == ㄴㅁㅍㄷ

		public bool 저장()
		{
			// 나중에 헤더로 올리자
			if (!grd라인.Verify())
				return false;

			// 회사코드, 파일번호는 신규 생성시 없을수도 있으므로 저장하기 직전 바인딩 해줌
			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
			{
				grd라인[i, "CD_COMPANY"] = 회사코드;
				grd라인[i, "NO_FILE"] = 파일번호;
			}

			// ********** 변경사항 가져오기
			DataTable 헤드 = 헤더.데이터테이블_수정();
			DataTable 라인 = grd라인.데이터테이블_수정();

			// 저장할건 없지만 부모를 위해서 true를 리턴함
			if (!헤드.존재() && !라인.존재())
				return true;
			
			// 담당자가 빈값일 때는 0을 넣음 (숫자형이라 공백문자가 들어가면 json 파싱에서 오류남)
			if (헤드.존재() && ctx매출처담당자.값() == "")
				헤드.Rows[0]["SEQ_ATTN"] = 0;

			try
			{
				#region ********** 유효성 검사

				// 이상 수량 체크
				if (라인 != null && 라인.선택("QT >= 1000").존재())
				{
					if (!메시지.경고선택("매우 큰 수량이 존재합니다. 저장 하시겠습니까?"))
						return false;
				}

				#endregion

				// ********** 저장 (헤드는 무조건 수정모드, 부모폼에서 INSERT함)
				디비.실행("PX_CZ_SA_INQ_6", 헤드.Json(DataRowState.Modified), 라인.Json());

				// ********** 필터 적용시키기 위해 코드 변경
				grd라인.그리기중지();

				// 매칭코드 변경
				foreach (DataRow row in grd라인.데이터테이블().선택("CD_MATCH != CD_MATCH_OLD"))
				{
					row["CD_MATCH_OLD"] = row["CD_MATCH"];
				}

				// 단위 빨간색 안들어오게 조치
				foreach (DataRow row in grd라인.데이터테이블().선택("UNIT != UNIT_OLD"))
				{
					row["UNIT_INQ_NEW"] = 단위_통합.선택("UNIT = '" + row["UNIT"] + "'")[0]["UNIT_NEW"];
					row["UNIT_OLD"] = row["UNIT"];
				}

				// 견적문의 컬럼을 원래상태로 되돌림
				foreach (DataRow row in grd라인.데이터테이블().선택("CD_VENDOR_INQ != ''"))
				{
					row["YN_VENDOR_INQ"] = "N";
					row["CD_VENDOR_INQ"] = "";
				}

				// ********** 기타
				키워드.견적저장(라인);
				그리드스타일();
				
				// 커밋
				헤더.커밋();
				grd라인.커밋();
				grd매입처검색.클리어();
				grd매입처선택.클리어();
			}
			catch (Exception ex)
			{
				메시지.오류알람(ex);
				return false;
			}

			return true;
		}
		
		#endregion

		#region ==================================================================================================== 닫기 == CLOSE == 치ㅐㄴ

		public void Exit()
		{
			if (grd라인.Cols.Count > 1)
			{
				grd라인.SaveUserCache("P_CZ_SA_QTN_REG_SINQ");
				설정.내보내기("P_CZ_SA_INQ", "실행_숨기기버튼", spc메인.Panel2Collapsed ? "Y" : "N");
			}
		}

		#endregion


		public void 그리드스타일()
		{
			grd라인.그리기중지();
			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)	 그리드스타일(i);			
			grd라인.그리기시작();
		}

		public void 그리드스타일(int 행)
		{			
			// 상태랑 재고코드는 리셋하고 시작하자
			grd라인.셀글자색(행, "CD_MATCH", "");
			grd라인.셀글자색(행, "CD_ITEM", "");
			grd라인.셀밑줄(행, "CD_ITEM", false);

			// 재고매칭 상태 컬러
			if		(grd라인[행, "CD_MATCH"].문자() == "MCH") grd라인.셀글자색(행, "CD_MATCH", Color.Blue);
			else if (grd라인[행, "CD_MATCH"].문자() == "SLD") grd라인.셀글자색(행, "CD_MATCH", Color.Blue);
			else if (grd라인[행, "CD_MATCH"].문자() == "SEL") grd라인.셀글자색(행, "CD_MATCH", Color.Red);
			else if (grd라인[행, "CD_MATCH"].문자() == "CHK") grd라인.셀글자색(행, "CD_MATCH", Color.Red);
			else if (grd라인[행, "CD_MATCH"].문자() == "SBJ") grd라인.셀글자색(행, "CD_MATCH", Color.Red);
			else if (grd라인[행, "CD_MATCH"].문자() == "N/A") grd라인.셀글자색(행, "CD_MATCH", Color.DarkGray);
			else if (grd라인[행, "CD_MATCH"].문자() == "EXC")
			{
				grd라인.셀글자색(행, "CD_MATCH", Color.DarkGray);
				grd라인.셀글자색(행, "CD_ITEM", Color.DarkGray);
			}
			
			// 상급품이 있는 재고는 밑줄 표시
			if (grd라인[행, "CD_MATCH"].문자() != "EXC" && grd라인[행, "CD_ITEM_SPE"].문자() != "")
				grd라인.셀밑줄(행, "CD_ITEM", true);


			// ********** 단위!!
			bool 단위조건_재고 = (grd라인[행, "CD_MATCH"].문자() != "EXC" && grd라인[행, "CD_ITEM"].문자() != "") && grd라인[행, "UNIT_INQ_NEW"].문자() != grd라인[행, "UNIT_STK_NEW"].문자();
			bool 단위조건_최저 = (grd라인[행, "CD_MATCH"].문자() == "EXC" || grd라인[행, "CD_ITEM"].문자() == "") && grd라인[행, "UNIT_INQ_NEW"].문자() != grd라인[행, "UNIT_MIN_NEW"].문자() && grd라인[행, "CD_VENDOR_MIN"].문자() != "";

			grd라인.셀경고(행, "UNIT", 단위조건_재고 || 단위조건_최저);
			grd라인.셀글자색_빨강(행, "UNIT_STK", 단위조건_재고);
			grd라인.셀글자색_빨강(행, "UNIT_MIN", 단위조건_최저);



			// 재고단가가 더 비싸면 경고
			grd라인.셀글자색_빨강(행, "UM_STK", grd라인[행, "UM_MIN"].정수() > 0 && grd라인[행, "UM_STK"].정수() > grd라인[행, "UM_MIN"].정수());

			// 매입처 견적 경과일이 30일 넘었으면 경고
			grd라인.셀글자색_빨강(행, "DAY_MIN", grd라인[행, "DAY_MIN"].정수() > 30);

			// 매입처선택을 했을 경우 파란색 표시
			grd라인.셀글자색_파랑강조(행, "NM_VENDOR_INQ", grd라인[행, "CD_VENDOR_INQ"].문자() != "");
		}

		

		public void 매입처검색(string 검색어, bool 자동)
		{
			// 현대는 안함
			if (검색어.Contains("현대웹"))
				return;

			// 비고 정비
			검색어 = 검색어.괄호내용제거();
			검색어 = 검색어.단일공백();
			검색어 = 검색어.특수문자제거();
			검색어 = 검색어.Replace("(", " (");
			검색어 = 검색어.Replace(" ", ",");
			검색어 = 검색어.Replace("-", ",");
			검색어 = 검색어.Replace("_", ",");
			
			// ********** 문자열 리스트로 변환
			List<string> 리스트 = new List<string>();

			if (자동 && 견적.파일번호.왼쪽(2).포함("SB", "NS"))
				리스트.Add("DINTEC CO., LTD.");    // 선용은 DINTEC만 추가
			else
				리스트.AddRange(검색어.분할(",", 2));

			// 없으면 없는대로 쿼리 한번 날려야 해서 이상한거 추가함
			if (리스트.Count == 0)
				리스트.Add("‡‡‡‡‡");

			// ********** 쿼리
			string query = "";

			foreach (string s in 리스트)
			{
				query += @"
SELECT
	CD_PARTNER
,	NM_PARTNER	= LN_PARTNER
,	NM_CEO
,	NO_COMPANY	= SUBSTRING(NO_COMPANY, 1, 3) + '-' + SUBSTRING(NO_COMPANY, 4, 2) + '-' + SUBSTRING(NO_COMPANY, 6, 5)
FROM MA_PARTNER	AS A WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + 견적.회사코드 + @"'
	AND CD_PARTNER NOT IN ('00799', '11823')	-- 현대중공업, 현대글로벌서비스
	AND FG_PARTNER IN ('110', '200', '400')		-- 110:대리점, 200:메이커, 400:공급사
	AND USE_YN = 'Y'
	AND LN_PARTNER LIKE '%" + s + @"%'
";

				// 자동일때는 최근 1년간 10회 이상 견적건만 검색
				if (자동)
					query += @"
	AND EXISTS (SELECT 1 FROM CZ_PU_QTNH AS X WITH(NOLOCK) WHERE A.CD_COMPANY = X.CD_COMPANY AND A.CD_PARTNER = X.CD_PARTNER AND X.DT_INQ >= NEOE.TODAY(-365) GROUP BY X.CD_COMPANY, X.CD_PARTNER HAVING COUNT(1) >= 10)
";
			}

			// 바인딩 고고
			DataSet ds = SQL.GetDataSet(query);
			DataTable dt검색 = ds.Tables[0].Clone();
			DataTable dt선택 = ds.Tables[0].Clone();
			
			foreach (DataTable dt in ds.Tables)
			{
				if (자동 && dt.Rows.Count == 1)
					dt선택.Merge(dt);
				else				
					dt검색.Merge(dt);
			}
		
			grd매입처검색.바인딩(dt검색.중복제거());
			grd매입처선택.바인딩(dt선택.중복제거());

			//if (dtSelected.Rows.Count + dtSearched.Rows.Count > 0 && !견적.IsGS)
			//	tab메인.SelectedTab = tab매입처;
		}

	}
}
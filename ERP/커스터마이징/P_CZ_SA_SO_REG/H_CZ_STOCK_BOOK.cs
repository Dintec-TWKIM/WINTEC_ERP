using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using Dass.FlexGrid;
using C1.Win.C1FlexGrid;

using DX;

namespace cz
{
	public partial class H_CZ_STOCK_BOOK : Duzon.Common.Forms.CommonDialog
	{
		string 회사코드;
		string 파일번호;

		string 재고코드 => grd라인["CD_ITEM"].문자();

		public H_CZ_STOCK_BOOK(string 회사코드, string 파일번호)
		{
			InitializeComponent();
			
			this.페이지초기화();
			this.회사코드 = 회사코드;
			this.파일번호 = 파일번호;
		}
		
		#region ==================================================================================================== 초기화 == INIT == ㅑㅜㅑㅅ

		protected override void InitLoad()
		{
			InitGrid();
			InitEvent();

			// 팀장들은 담당 변경 권한 줌
			if (권한.팀장())
			{
				ctx담당자.Enabled = true;
			}

			// 옵션 한번 돌려주고..
			Rdo재고_발주_CheckedChanged(null, null);

			// 선용파일 옵션 디폴트 설정
			if (파일번호.왼쪽(2).포함("SB", "NS"))
			{
				rdo재고.Checked = true;
				chk가용재고.Checked = true;

				// 선용은 아예 선택 안되게함
				pnl예약.사용(false);
				pnl옵션.사용(false);
			}
		}

		protected override void InitPaint()
		{
			// 조회
			Btn새로고침_Click(null, null);
		}

		// ESC키 팝업 닫힘 방지
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Escape)
				return false;

			return base.ProcessCmdKey(ref msg, keyData);
		}

		#endregion

		#region ==================================================================================================== 그리드 == GRID == ㅎ걍

		private void InitGrid()
		{
			grd라인.DetailGrids = new FlexGrid[] { grd출고예약, grd발주예약, grd재고발주 };

			// ********** 라인
			grd라인.세팅시작(2);

			grd라인.컬럼세팅("NO_FILE"		, "파일번호"		, false);
			grd라인.컬럼세팅("NO_LINE"		, "항번"			, false);
			grd라인.컬럼세팅("NO_DSP"			, "순번"			, 상수.넓이_순번		, 포맷.순번);
			grd라인.컬럼세팅("CD_ITEM_PARTNER", "품목코드"		, 상수.넓이_품목코드);
			grd라인.컬럼세팅("NM_ITEM_PARTNER", "품목명"		, false);
			grd라인.컬럼세팅("CD_ITEM"		, "재고코드"		, 상수.넓이_재고코드	, 정렬.가운데);
			grd라인.컬럼세팅("NM_ITEM"		, "재고명"		, 221);
			grd라인.컬럼세팅("UCODE"			, "U코드"		, 상수.넓이_U코드);

			grd라인.컬럼세팅("QT_ST"			, "재고수량"		, "현재고"		, 상수.넓이_수량	, 포맷.수량);
			grd라인.컬럼세팅("QT_BOOK"		, "재고수량"		, "출고예약"		, 상수.넓이_수량	, 포맷.수량);
			grd라인.컬럼세팅("QT_AVST"		, "재고수량"		, "가용재고"		, 상수.넓이_수량	, 포맷.수량);

			grd라인.컬럼세팅("QT_STPO"		, "발주수량"		, "발주"			, 상수.넓이_수량	, 포맷.수량);
			grd라인.컬럼세팅("QT_HOLD"		, "발주수량"		, "발주예약"		, 상수.넓이_수량	, 포맷.수량);
			grd라인.컬럼세팅("QT_AVPO"		, "발주수량"		, "가용발주"		, 상수.넓이_수량	, 포맷.수량);
			
			grd라인.컬럼세팅("QT_SO"			, "진행"			, "수주"			, 상수.넓이_수량	, 포맷.수량);
			grd라인.컬럼세팅("QT_PO"			, "진행"			, "발주"			, 상수.넓이_수량	, 포맷.수량);
			grd라인.컬럼세팅("QT_GIR"			, "진행"			, "의뢰"			, 상수.넓이_수량	, 포맷.수량);
			
			grd라인.컬럼세팅("QT_BOOK_MY"		, "예약"			, "출고예약"		, 상수.넓이_수량	, 포맷.수량);
			grd라인.컬럼세팅("QT_HOLD_MY"		, "예약"			, "발주예약"		, 상수.넓이_수량	, 포맷.수량);
			grd라인.컬럼세팅("QT_MISS_MY"		, "예약"			, "잔량"			, 상수.넓이_수량	, 포맷.수량);
			
			grd라인.컬럼세팅("UM_KR"			, "재고단가"		, 상수.넓이_단가	, 포맷.원화단가);
			grd라인.컬럼세팅("AM_KR"			, "재고금액"		, 상수.넓이_단가	, 포맷.원화단가);
			grd라인.컬럼세팅("NM_EMP"			, "예약자"		, 상수.넓이_담당자	, 정렬.가운데);
			grd라인.컬럼세팅("NO_EMP"			, "예약자"		, false);
			
			grd라인.세팅종료("23.04.20.01", true);

			grd라인.에디트컬럼("QT_BOOK_MY", "QT_HOLD_MY");
			grd라인.합계제외컬럼("NO_DSP", "UM_KR");
			grd라인.합계컬럼스타일("QT_AVST", "QT_AVPO", "QT_BOOK_MY", "QT_HOLD_MY");

			grd라인.헤더더블클릭(그리드스타일);

			// 수주수량 컬럼은 초록색
			grd라인.Cols["QT_SO"].글자색(Color.Purple);
			grd라인.Cols["QT_SO"].굵게();

			grd라인.Cols["QT_BOOK_MY"].글자색(Color.Green);
			grd라인.Cols["QT_BOOK_MY"].굵게();

			grd라인.Cols["QT_HOLD_MY"].글자색(Color.Green);
			grd라인.Cols["QT_HOLD_MY"].굵게();

			// ********** 출고예약
			grd출고예약.세팅시작(1);

			grd출고예약.컬럼세팅("NO_SO"			, "파일번호"		, 상수.넓이_파일번호	, 정렬.가운데);
			grd출고예약.컬럼세팅("NO_DSP"			, "순번"			, 상수.넓이_순번		, 포맷.순번);
			grd출고예약.컬럼세팅("CD_ITEM_PARTNER"	, "품목코드"		, 상수.넓이_품목코드);
			grd출고예약.컬럼세팅("NM_ITEM_PARTNER"	, "품목명"		, 200);

			grd출고예약.컬럼세팅("CD_ITEM"			, "재고코드"		, false);
			grd출고예약.컬럼세팅("NM_PARTNER"		, "매출처"		, 180);
			grd출고예약.컬럼세팅("NM_VESSEL"		, "선명"			, 180);
			grd출고예약.컬럼세팅("NO_PO_PARTNER"	, "주문번호"		, 100);
			grd출고예약.컬럼세팅("NM_EMP"			, "예약자"		, 상수.넓이_담당자		, 정렬.가운데);
		
			grd출고예약.컬럼세팅("DT_SO"			, "수주일"		, 상수.넓이_일자		, 포맷.날짜);
			grd출고예약.컬럼세팅("DT_BOOK"			, "예약일"		, 상수.넓이_일자		, 포맷.날짜);
			grd출고예약.컬럼세팅("DT_DUEDATE"		, "납기예정일"	, 상수.넓이_일자		, 포맷.날짜);
			grd출고예약.컬럼세팅("QT_SO"			, "수주"			, 상수.넓이_수량		, 포맷.수량);
			grd출고예약.컬럼세팅("QT_BOOK"			, "예약"			, 상수.넓이_수량		, 포맷.수량);

			grd출고예약.세팅종료("23.04.20.01", true);
			grd출고예약.합계제외컬럼("NO_DSP");

			// ********** 발주예약
			grd발주예약.세팅시작(1);

			grd발주예약.컬럼복사(grd출고예약);
			grd발주예약.Cols["DT_BOOK"].Name = "DT_HOLD";
			grd발주예약.Cols["QT_BOOK"].Name = "QT_HOLD";

			grd발주예약.세팅종료(grd출고예약.세팅버전(), true);			
			grd발주예약.합계제외컬럼("NO_DSP");

			// ********** 재고발주
			grd재고발주.세팅시작(1);

			grd재고발주.컬럼세팅("NO_PO"			, "발주번호"		, 상수.넓이_파일번호	, 정렬.가운데);
			grd재고발주.컬럼세팅("NO_LINE"			, "순번"			, 상수.넓이_순번		, 포맷.순번);
			grd재고발주.컬럼세팅("CD_ITEM"			, "재고코드"		, false);
			grd재고발주.컬럼세팅("LN_PARTNER"		, "매입처"		, 180);
			grd재고발주.컬럼세팅("NO_ORDER"		, "공사번호"		, 100);
			grd재고발주.컬럼세팅("NM_EMP"			, "담당자"		, 상수.넓이_담당자		, 정렬.가운데);

			grd재고발주.컬럼세팅("DT_LIMIT"		, "납기예정일"	, 상수.넓이_일자		, 포맷.날짜);
			grd재고발주.컬럼세팅("DT_AVERAGE"		, "기대납기일"	, 상수.넓이_일자		, 포맷.날짜);
			grd재고발주.컬럼세팅("WEIGHTED_MEAN"	, "평균LT"		, 상수.넓이_수량);
			grd재고발주.컬럼세팅("STD_DEVIATION"	, "표준편차"		, 상수.넓이_수량);
			grd재고발주.컬럼세팅("DT_EXPECT"		, "확약일"		, 상수.넓이_일자		, 포맷.날짜);
			grd재고발주.컬럼세팅("DT_EXDATE"		, "반출일"		, 상수.넓이_일자		, 포맷.날짜);

			grd재고발주.컬럼세팅("QT_PO"			, "발주"			, 상수.넓이_수량		, 포맷.수량);
			grd재고발주.컬럼세팅("QT_NONGR"		, "미입고"		, 상수.넓이_수량		, 포맷.수량);
			grd재고발주.컬럼세팅("UM_EX"			, "매입단가"		, 상수.넓이_단가		, 포맷.원화단가);
			grd재고발주.컬럼세팅("DC2"				, "비고2"		, 244);

			grd재고발주.세팅종료(grd출고예약.세팅버전(), true);
			grd재고발주.합계제외컬럼("NO_LINE");
		}

		#endregion

		#region ==================================================================================================== 이벤트 == EVENT == ㄷㅍ둣

		private void InitEvent()
		{
			rdo재고_발주.CheckedChanged += Rdo재고_발주_CheckedChanged;
			rdo재고.CheckedChanged += Rdo재고_발주_CheckedChanged;

			btn새로고침.Click += Btn새로고침_Click;
			btn일괄예약.Click += Btn일괄예약_Click;
			btn초기화.Click += Btn초기화_Click;
			btn저장.Click += Btn저장_Click;
			btn취소.Click += Btn취소_Click;
			
			grd라인.AfterRowChange += Grd라인_AfterRowChange;
			grd라인.ValidateEdit += Grd라인_ValidateEdit;
		}

		private void Rdo재고_발주_CheckedChanged(object sender, EventArgs e)
		{
			pnl옵션.사용(!rdo재고_발주.Checked);
			chk가용재고.Checked = false;	// 일단 무조건 false
		}

		private void Btn취소_Click(object sender, EventArgs e) => Close();
		
		private void Btn초기화_Click(object sender, EventArgs e)
		{
			grd라인.그리기중지();

			try
			{
				for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
				{
					if (grd라인[i, "QT_BOOK_MY"].실수() > grd라인[i, "QT_GIR"].실수())
					{						
						grd라인[i, "QT_BOOK_MY"] = grd라인[i, "QT_GIR"];    // 의뢰수량보다 줄일수 없으므로 요고랑 동일시 해준다
						grd라인[i, "YN_EDIT_BOOK"] = "Y";
					}

					if (grd라인[i, "QT_HOLD_MY"].실수() > 0)
					{						
						grd라인[i, "QT_HOLD_MY"] = 0;
						grd라인[i, "YN_EDIT_HOLD"] = "Y";
					}

					재고금액및오토여부(i);
					그리드스타일(i);
				}
			}
			catch (Exception ex)
			{
				메시지.오류알람(ex);
			}

			grd라인.그리기시작();
		}

		private void Btn일괄예약_Click(object sender, EventArgs e)
		{
			메시지.작업중("작업중입니다.");
			grd라인.그리기중지();

			try
			{
				// 재고수량 가져오기 (중복 재고코드 때문에 복잡하게 계산해야함)
				DataTable dt = grd라인.데이터테이블().중복제거("CD_ITEM", "QT_AVST", "QT_AVPO");

				for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
				{
					DataRow 재고수량 = dt.선택("CD_ITEM = '" + grd라인[i, "CD_ITEM"] + "'")[0];

					// 현재 전산상 가용재고
					decimal 진짜재고 = 재고수량["QT_AVST"].실수();
					decimal 진짜발주 = 재고수량["QT_AVPO"].실수();

					// 내가 예약한 수량까지 합한 가용재고
					decimal 가용재고 = 진짜재고 + grd라인[i, "QT_BOOK_MY_OLD"].실수();
					decimal 가용발주 = 진짜발주 + grd라인[i, "QT_HOLD_MY_OLD"].실수();

					// 기준수량
					decimal 수주발주 = grd라인[i, "QT_SO"].실수() - grd라인[i, "QT_PO"].실수();

					// 현재 예약수량
					decimal 출고예약 = grd라인[i, "QT_BOOK_MY"].실수();
					decimal 발주예약 = grd라인[i, "QT_HOLD_MY"].실수();

					if (!chk가용재고.Checked)
					{
						// 출고예약 => 진짜가용재고가 1개라도 있어야 돌릴 가치고 있음
						if (진짜재고 > 0 && 수주발주 - 출고예약 > 0)
						{
							출고예약 = 수학.작은수(수주발주, 가용재고);
							발주예약 = 0;   // 발주예약 리셋
							재고수량["QT_AVST"] = 가용재고 - 출고예약;

							grd라인[i, "QT_BOOK_MY"] = 출고예약;
							grd라인[i, "QT_HOLD_MY"] = 발주예약;
							grd라인[i, "YN_EDIT_BOOK"] = "Y";
						}

						// 옵션 선택된 경우만 수행
						if (진짜발주 > 0 && 수주발주 - 출고예약 - 발주예약 > 0 && rdo재고_발주.Checked)
						{
							발주예약 = 수학.작은수(수주발주 - 출고예약, 가용발주);
							재고수량["QT_AVPO"] = 가용발주 - 발주예약;

							grd라인[i, "QT_HOLD_MY"] = 발주예약;
							grd라인[i, "YN_EDIT_HOLD"] = "Y";
						}
					}
					else
					{
						// 선용은 가용재고로 풀커버 될때만 예약해줌
						if (진짜재고 > 0 && 가용재고 >= 수주발주)
						{
							출고예약 = 수주발주;
							발주예약 = 0;   // 발주예약 리셋
							재고수량["QT_AVST"] = 가용재고 - 출고예약;

							grd라인[i, "QT_BOOK_MY"] = 출고예약;
							grd라인[i, "QT_HOLD_MY"] = 발주예약;
							grd라인[i, "YN_EDIT_BOOK"] = "Y";
						}
					}

					// 마무리
					재고금액및오토여부(i);
					그리드스타일(i);

					// 무결성검사
					if (재고수량["QT_AVST"].실수() < 0 || 재고수량["QT_AVPO"].실수() < 0 ||grd라인[i, "QT_MISS_MY"].실수() < 0)
						메시지.오류발생("\n심각한 오류! 기획실로 연락바랍니다.");
				}				
			}
			catch (Exception ex)
			{
				메시지.오류알람(ex);
			}

			grd라인.그리기시작();
			메시지.작업중();
		}

		private void Grd라인_ValidateEdit(object sender, ValidateEditEventArgs e)
		{
			grd라인.그리기중지();

			try
			{
				string 컬럼이름 = grd라인.컬럼이름(e);

				// 현재 전산상 가용재고
				decimal 진짜재고 = grd라인[e.Row, "QT_AVST"].실수();
				decimal 진짜발주 = grd라인[e.Row, "QT_AVPO"].실수();

				// 내가 예약한 수량까지 합한 가용재고
				decimal 가용재고 = 진짜재고 + grd라인[e.Row, "QT_BOOK_MY_OLD"].실수();
				decimal 가용발주 = 진짜발주 + grd라인[e.Row, "QT_HOLD_MY_OLD"].실수();

				// 기준수량
				decimal 수주발주 = grd라인[e.Row, "QT_SO"].실수() - grd라인[e.Row, "QT_PO"].실수();

				// 현재 예약수량
				decimal 출고예약 = grd라인[e.Row, "QT_BOOK_MY"].실수();
				decimal 발주예약 = grd라인[e.Row, "QT_HOLD_MY"].실수();

				// 기타
				decimal 출고의뢰 = grd라인[e.Row, "QT_GIR"].실수();
				decimal 예약수량 = 출고예약 + 발주예약;

				if (컬럼이름 == "QT_BOOK_MY")
				{
					if (!(출고예약 <= 수주발주))	메시지.경고발생(메시지코드._은는_보다작거나같아야합니다, "출고예약", "수주(수주-발주)");
					if (!(출고예약 <= 가용재고))	메시지.경고발생(메시지코드._은는_보다작거나같아야합니다, "출고예약", "가용재고");
					if (!(출고예약 >= 출고의뢰))	메시지.경고발생(메시지코드._은는_보다크거나같아야합니다, "출고예약", "출고의뢰");
					
					grd라인[e.Row, "YN_EDIT_BOOK"] = "Y";

					// 발주예약수량 초기화 및 재계산
					grd라인[e.Row, "QT_HOLD_MY"] = 0;
					grd라인[e.Row, "YN_EDIT_HOLD"] = "N";
					
					if (가용발주 > 0 & 수주발주 - 출고예약 > 0 && rdo재고_발주.Checked)
					{
						grd라인[e.Row, "QT_HOLD_MY"] = 수학.작은수(수주발주 - 출고예약, 가용발주);
						grd라인[e.Row, "YN_EDIT_HOLD"] = "Y";
					}					
				}
				else if (컬럼이름 == "QT_HOLD_MY")
				{
					if (!(예약수량 <= 수주발주))	메시지.경고발생(메시지코드._은는_보다작거나같아야합니다, "예약수량(출고+발주)", "수주(수주-발주)");
					if (!(발주예약 <= 가용발주))	메시지.경고발생(메시지코드._은는_보다작거나같아야합니다, "발주예약", "가용발주");

					grd라인[e.Row, "YN_EDIT_HOLD"] = "Y";
				}

				// 마무리
				재고금액및오토여부(e.Row);
				그리드스타일(e.Row);

				// 무결성검사
				if (grd라인[e.Row, "QT_MISS_MY"].정수() < 0)
					메시지.오류발생("\n심각한 오류! 기획실로 연락바랍니다.");
			}
			catch (Exception ex)
			{
				메시지.오류알람(ex);
				e.Cancel = true;
				SendKeys.Send("{ESC}");
			}

			grd라인.그리기시작();
		}

		private void 재고금액및오토여부(int i)
		{
			// 잔량 계산
			grd라인[i, "QT_MISS_MY"] = grd라인[i, "QT_SO"].실수() - grd라인[i, "QT_PO"].실수() - grd라인[i, "QT_BOOK_MY"].실수() - grd라인[i, "QT_HOLD_MY"].실수();

			// 출고예약일때만 재고금액 계산
			if (grd라인[i, "YN_EDIT_BOOK"].문자() == "Y")
				grd라인[i, "AM_KR"] = grd라인[i, "QT_BOOK_MY"].실수() * grd라인[i, "UM_KR"].실수();			
		}

		#endregion

		#region ==================================================================================================== 조회 == SEARCH == ㄴㄷㅁㄱ

		private void Btn새로고침_Click(object sender, EventArgs e)
		{
			// 양 많을때만 뛰움 (적을땐 쓸데없이 이거땜에 느려짐)
			if (grd라인.Rows.Count > 200)
				메시지.작업중("작업중입니다.");

			DataTable dt = 디비.결과("PS_CZ_SA_STOCK_BOOK", 회사코드, 파일번호);
			grd라인.바인딩(dt);

			그리드스타일();

			메시지.작업중();
		}

		private void Grd라인_AfterRowChange(object sender, RangeEventArgs e)
		{
			DataTable dt출고예약 = null;
			DataTable dt발주예약 = null;
			DataTable dt재고발주 = null;

			if (grd라인.상세그리드쿼리())
			{
				dt출고예약 = 디비.결과("PS_CZ_SA_STOCK_BOOK_BOOK", 회사코드, 재고코드);
				dt발주예약 = 디비.결과("SP_CZ_SA_STOCK_BOOK_SELECT_HOLD", 회사코드, 재고코드);
				dt재고발주 = 디비.결과("SP_CZ_SA_STOCK_BOOK_SELECT_NONGR", 회사코드, 재고코드);

				// 이벤트용 항번 추가 => 요고해야 동일 재고코드일때 더블데이터가 안생김 (expression으로 하면 절대 안됨)
				dt출고예약.컬럼추가("NO_LINE_EV", typeof(int), grd라인["NO_LINE"]);
				dt발주예약.컬럼추가("NO_LINE_EV", typeof(int), grd라인["NO_LINE"]);
				dt재고발주.컬럼추가("NO_LINE_EV", typeof(int), grd라인["NO_LINE"]);
			}

			grd출고예약.바인딩(dt출고예약, "NO_LINE_EV = " + grd라인["NO_LINE"]);
			grd발주예약.바인딩(dt발주예약, "NO_LINE_EV = " + grd라인["NO_LINE"]);
			grd재고발주.바인딩(dt재고발주, "NO_LINE_EV = " + grd라인["NO_LINE"]);
		}

		#endregion

		#region ==================================================================================================== 저장 == SAVE == ㄴㅁㅍㄷ

		private void Btn저장_Click(object sender, EventArgs e)
		{			
			try
			{
				if (ctx담당자.CodeValue == "")
				
					디비.실행("PX_CZ_SA_STOCK_BOOK_2", grd라인.데이터테이블_수정().Json("NO_FILE", "NO_LINE", "CD_ITEM", "QT_BOOK_MY", "QT_HOLD_MY", "UM_KR", "AM_KR"));
				else
					디비.실행("PX_CZ_SA_STOCK_BOOK_EMP", 회사코드, 파일번호, ctx담당자.값(), 상수.사원번호);				

				메시지.저장완료();
				DialogResult = DialogResult.OK;
			}
			catch (Exception ex)
			{
				메시지.오류알람(ex);
			}
		}

		#endregion

		public void 그리드스타일()
		{
			grd라인.그리기중지();
			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++) 그리드스타일(i);
			grd라인.그리기시작();
		}

		private void 그리드스타일(int i)
		{
			// ********** 가용재고, 가용발주
			grd라인.셀글자색(i, "QT_AVST", "", false);
			grd라인.셀글자색(i, "QT_AVPO", "", false);

			if (grd라인[i, "QT_AVST"].정수() > 0) grd라인.셀글자색(i, "QT_AVST", grd라인[i, "QT_AVST"].정수() >= grd라인[i, "QT_SO"].정수() ? Color.Blue : Color.Red, true);
			if (grd라인[i, "QT_AVPO"].정수() > 0) grd라인.셀글자색(i, "QT_AVPO", grd라인[i, "QT_AVPO"].정수() >= grd라인[i, "QT_SO"].정수() ? Color.Blue : Color.Red, true);

			// ********** 예약하고 남은 잔량 (= 발주수량이 되어야함)
			grd라인.셀글자색(i, "QT_MISS_MY", "");

			if (grd라인[i, "QT_MISS_MY"].정수()  > 0) grd라인.셀글자색(i, "QT_MISS_MY", Color.OrangeRed);

			// ********** 오토 선택에 따른 스타일
			string 오토스타일 = "CELL_AUTO";

			if (!grd라인.Styles.Contains(오토스타일))
			{
				grd라인.Styles.Add(오토스타일);
				grd라인.Styles[오토스타일].BackColor = Color.Yellow;
			}
			
			// 일단 초기화
			grd라인.셀이미지(i, "QT_BOOK_MY", null);
			grd라인.셀스타일(i, "QT_BOOK_MY", "");

			grd라인.셀이미지(i, "QT_HOLD_MY", null);
			grd라인.셀스타일(i, "QT_HOLD_MY", "");

			// 오토인 경우 칠하기
			if (grd라인[i, "YN_EDIT_BOOK"].문자() == "Y")
			{
				grd라인.셀이미지(i, "QT_BOOK_MY", 아이콘.동그라미_초록_15x9);
				grd라인.셀스타일(i, "QT_BOOK_MY", 오토스타일);				
			}

			if (grd라인[i, "YN_EDIT_HOLD"].문자() == "Y")
			{
				grd라인.셀이미지(i, "QT_HOLD_MY", 아이콘.동그라미_초록_15x9);
				grd라인.셀스타일(i, "QT_HOLD_MY", 오토스타일);
			}
		}
	}
}

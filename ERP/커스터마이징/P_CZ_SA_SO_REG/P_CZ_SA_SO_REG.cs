using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Duzon.Common.Forms;
using Duzon.Common.BpControls;
using DzHelpFormLib;
using Duzon.Windows.Print;

using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.ERPU;
using Dintec;
using Duzon.Common.Util;
using System.IO;
using Duzon.Common.Controls;
using DX;


namespace cz
{
	public partial class P_CZ_SA_SO_REG : PageBase
	{
		헤더 헤더_물류관리 = new 헤더();
		FreeBinding Header = new FreeBinding(); 
		bool IsHeaderChanged = false;
		
		string 파일번호링크 = "";
		bool 신규여부 = false;

		string 수주상태코드 = "";
		string 수주마감코드 = "";

		string[] 가변에디트컬럼;
		ToolTip RemarkTip = new ToolTip();

		string 프로시저H = "PS_CZ_SA_SO_REG_H_4";
		string 프로시저L = "PS_CZ_SA_SO_REG_L_4";

		#region ===================================================================================================== Property

		public string 파일번호
		{
			get => tbx파일번호.Text;
			set => tbx파일번호.Text = value;
		}

		public string 매출처코드 => ctx매출처.값();

		public string IMO번호 => ctx호선번호.값();

		public decimal 환율 => cur환율.DecimalValue;

		public decimal 부가세율 => GetTo.Int(GetDb.CodeFlag1(cbo과세구분));

		private int 표시형식단가용
		{
			get
			{
				int roundCode;

				if (chk표시형식단가.Checked)
				{
					roundCode = GetTo.Int(GetDb.CodeFlag1(cbo표시형식));
				}
				else
				{
					if (cbo통화.GetValue() == "000")
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


		public bool 기자재여부 => !선용여부;

		public bool 선용여부 => 파일번호.왼쪽(2).포함("SB", "NS");

		#endregion

		#region ==================================================================================================== Constructor

		public P_CZ_SA_SO_REG()
		{
			StartUp.Certify(this);
			InitializeComponent();			
		}

		public P_CZ_SA_SO_REG(string 파일번호, bool 신규여부)
		{
			StartUp.Certify(this);
			InitializeComponent();			

			this.파일번호 = 파일번호;
			this.신규여부 = 신규여부;
		}

		#endregion

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{
			this.페이지초기화();

			btn인보이스확인.Top = 2;
			btn인보이스확인.Left = 200;

			pnl수주일자.사용(false);
			pnl매출처.사용(false);

			DataSet ds = 코드.코드관리("MA_B000065", "MA_B000005", "MA_B000040", "CZ_SA00013", "TR_IM00003", "TR_IM00011", "CZ_SA00014", "CZ_SA00061");
			cbo거래처그룹.바인딩(ds.Tables[0], true);
			cbo통화.바인딩(ds.Tables[1], false);
			cbo과세구분.바인딩(ds.Tables[2], true);
			cbo지불조건.바인딩(ds.Tables[3], true);
			cbo선적조건.바인딩(ds.Tables[4], true);
			cbo포장형태.바인딩(ds.Tables[5], true);
			cbo표시형식.바인딩(ds.Tables[6], false);
			

			cbo인도기간.Items.Add("");
			cbo인도기간.Items.Add("기본값");
			cbo인도기간.Items.Add("기간(일)");
			cbo인도기간.Items.Add("기간(날짜)");
			cbo인도기간.Items.Add("납기(일)");
			cbo인도기간.Items.Add("납기(날짜)");


			// ********** 물류관리 콤보			
			DataSet ds물류 = 코드.코드관리("CZ_PU00013", "CZ_PU00012");

			cbo납품완료시간.바인딩(ds물류.Tables[0], true);
			cbo컷오프시간.바인딩(ds물류.Tables[0], true);
			cbo발주시간.바인딩(ds물류.Tables[0], true);
			cbo발주장소.바인딩(ds물류.Tables[1], true);

			cbo발주장소.바인딩(ds물류.Tables[1], true);


			MainGrids = new FlexGrid[] { grd라인 };

			InitGrid();
			InitEvent();

			// 바인딩 초기화
			Header.SetBinding(TSQL.결과(프로시저H, "", "", ""), lay헤드);
			Header.ClearAndNewRow();
			인보이스정보바인딩(null);

			// ********** 물류관리
			헤더_물류관리.컨테이너(lay물류관리);
			헤더_물류관리.기본값추가(chk자동협조전, true);
			chk캘린더.사용(false);   // 얘는 편집불가, 그냥 선용이냐 아니냐에 따라서 결정죔
			ctx송품방법.글("-");
			ctx송품상세.글("-");
			ctx납품처.글("-");




			

			// 필수값 컬러
			물류관리컨트롤상태(null, null);
		}


		private void InitGrid()
		{
			grd라인.세팅시작(2);
			
			grd라인.컬럼세팅("NO_SO"							, "파일번호"		, false);
			grd라인.컬럼세팅("SEQ_SO"							, "항번"			, false);
			grd라인.컬럼세팅("NO_DSP"							, "순번"			, 45	, "####.##", 정렬.가운데);
			grd라인.컬럼세팅("GRP_ITEM"						, "유형"			, false);
			grd라인.컬럼세팅("NM_SUBJECT"						, "주제"			, 200	, false);
			grd라인.컬럼세팅("CD_ITEM_PARTNER"				, "품목코드"		, 100);
			grd라인.컬럼세팅("NM_ITEM_PARTNER"				, "품목명"		, 200);
			grd라인.컬럼세팅("CD_ITEM"						, "재고코드"		, 80);
			grd라인.컬럼세팅("NM_ITEM"						, "재고명"		, false);
			grd라인.컬럼세팅("CD_VENDOR"						, "매입처코드"	, false);
			grd라인.컬럼세팅("UNIT_SO"						, "단위"			, 50	, 정렬.가운데);
			grd라인.컬럼세팅("QT_SO"							, "수량"			, 50	, 포맷.수량);
			grd라인.컬럼세팅("QT_PO"							, "(발주)"		, 50	, 포맷.수량);
			grd라인.컬럼세팅("QT_GIR"							, "(의뢰)"		, 50	, 포맷.수량	, false);
			grd라인.컬럼세팅("QT_GI"							, "(출고)"		, 50	, 포맷.수량	, false);
			grd라인.컬럼세팅("QT_IV"							, "(정산)"		, 50	, 포맷.수량	, false);
						
			grd라인.컬럼세팅("UM_KR_P"		, "매입단가"		, "원화단가"		, 75	, 포맷.원화단가);
			grd라인.컬럼세팅("AM_KR_P"		, "매입단가"		, "원화금액"		, 80	, 포맷.원화단가);
			
			grd라인.컬럼세팅("RT_PROFIT"						, "이윤\n(%)"	, 45	, 포맷.비율);
			grd라인.컬럼세팅("UM_EX_Q"		, "매출견적단가"	, "외화단가"		, 75	, 포맷.외화단가);
			grd라인.컬럼세팅("AM_EX_Q"		, "매출견적단가"	, "외화금액"		, 80	, 포맷.외화단가);
			grd라인.컬럼세팅("UM_KR_Q"		, "매출견적단가"	, "원화단가"		, 75	, 포맷.원화단가);
			grd라인.컬럼세팅("AM_KR_Q"		, "매출견적단가"	, "원화금액"		, 80	, 포맷.원화단가);

			grd라인.컬럼세팅("RT_DC"							, "DC\n(%)"		, 45	, 포맷.비율);
			grd라인.컬럼세팅("UM_EX_S"		, "매출단가"		, "외화단가"		, 75	, 포맷.외화단가);
			grd라인.컬럼세팅("AM_EX_S"		, "매출단가"		, "외화금액"		, 80	, 포맷.외화단가);
			grd라인.컬럼세팅("UM_KR_S"		, "매출단가"		, "원화단가"		, 75	, 포맷.원화단가);
			grd라인.컬럼세팅("AM_KR_S"		, "매출단가"		, "원화금액"		, 80	, 포맷.원화단가);

			grd라인.컬럼세팅("RT_MARGIN"						, "최종\n(%)"	, 45	, 포맷.비율);
			grd라인.컬럼세팅("AM_VAT"							, "부가세"		, 75	, 포맷.원화단가);
			grd라인.컬럼세팅("RT_VAT"							, "부가세율(%)"	, false);
			grd라인.컬럼세팅("LT"								, "납기"			, 50	, 포맷.원화단가);

			grd라인.컬럼세팅("QT_AVST"		, "재고수량"		, "가용재고"		, 50	, 포맷.수량);
			grd라인.컬럼세팅("QT_AVPO"		, "재고수량"		, "가용입고"		, 50	, 포맷.수량);

			grd라인.컬럼세팅("QT_BOOK_MY"		, "재고예약"		, "출고예약"		, 50	, 포맷.수량);
			grd라인.컬럼세팅("QT_HOLD_MY"		, "재고예약"		, "입고예약"		, 50	, 포맷.수량);
			grd라인.컬럼세팅("UM_STK"			, "재고예약"		, "재고단가"		, 70	, 포맷.수량);

			grd라인.컬럼세팅("DC_RMK"							, "비고"			, 100);

			//grd라인.컬럼세팅("NO_PO_PARTNER"	, "주문번호"		, false);
			grd라인.컬럼세팅("YN_DSP_RMK"						, "D"			, 30	, 포맷.체크);
			grd라인.컬럼세팅("YN_HOLD"						, "C"			, 30	, 포맷.체크);

			if (상수.회사코드.포함("K200"))
			{
				grd라인.컬럼세팅("YN_GULL", "H", 30, 포맷.체크);
				grd라인.에디트컬럼("YN_GULL");
			}

			grd라인.데이터맵("UNIT_SO", 코드.단위());
			grd라인.세팅종료("21.02.07.03", true);
			
			grd라인.합계제외컬럼("NO_DSP", "UM_EX_P", "UM_KR_P", "RT_PROFIT", "UM_EX_Q", "UM_KR_Q", "RT_DC", "UM_EX_S", "UM_KR_S", "RT_MARGIN", "LT", "UM_STK");
			grd라인.합계컬럼스타일("AM_KR_P", "AM_EX_Q", "AM_KR_Q", "AM_EX_S", "AM_KR_S");
			grd라인.에디트컬럼("NO_DSP", "NM_SUBJECT", "CD_ITEM_PARTNER", "NM_ITEM_PARTNER", "DC_RMK", "YN_DSP_RMK", "YN_HOLD");
			grd라인.에디트컬럼(가변에디트컬럼 = new string[] { "CD_ITEM", "UNIT_SO", "QT_SO", "RT_PROFIT", "UM_EX_Q", "RT_DC", "UM_EX_S", "LT" });

			//// 갈매기 발동이거나 테스트 서버일때 삭제해야할 컬럼
			//if (YnHidden == "Y" || !Certify.IsLive() || CompanyCode == "K100")
			//{
			//	grd라인.Cols.Remove("YN_HOLD");
			//	//grd라인.Cols.Remove("YN_GULL");
			//}
		}

		protected override void InitPaint()
		{			
			if (파일번호 != "")
				OnToolBarSearchButtonClicked(null, null);

			tbx파일번호.Focus();
			
			// 테스트
			//if (파일번호 == "") 파일번호 = "ZZ23000577";
			//if (파일번호 == "") 파일번호 = "DY23000006";


			//lay헤드컨테이너.ColumnStyles[1].Width = 600;
			//lay헤드컨테이너.ColumnStyles[2].Width = 460;  // 인보이스 주소 관련 컬럼
			spc헤드.SplitterDistance = spc헤드.Width - 1100;

		}

		protected override bool IsChanged()
		{
			// 헤더변경 or 그리드변경
			if (IsHeaderChanged || base.IsChanged())
				return true;
			else
				return false;
		}



		private void 초기화()
		{
			파일번호 = "";
			신규여부 = false;
			
			Header.ClearAndNewRow();
			Header.AcceptChanges();

			// 상태값
			IsHeaderChanged = false;
			수주상태코드 = "";
			수주마감코드 = "N";

			// 라벨 + 버튼
			lbl수발주통보서.강조(강조모드.사용불가);
			lbl수주마감.강조(강조모드.사용불가);
			lbl물류협조전2.강조(강조모드.사용불가);
			lbl포장협조전2.강조(강조모드.사용불가);
			btn삭제.사용(true);

			// 원그리드
			pnl파일번호.사용(true);
			pnl담당자.사용(true);
			pnl영업그룹.사용(true);

			pnl호선.사용(true);
			pnl매출처그룹.사용(true);

			pnl환율.사용(true);
			pnl수주형태.사용(true);
			pnl과세구분.사용(true);
			pnl지불조건.사용(true);

			pnl원산지.사용(true);
			pnl인도기간.사용(true);
			pnl선적조건.사용(true);
			pnl포장형태.사용(true);

			pnl이윤.사용(true);
			pnl할인.사용(true);
			pnl납기.사용(true);
			pnl표시형식.사용(true);

			// 콤보
			cbo인도기간.Clear(true);

			// 라인그리드
			grd라인.초기화();
			grd라인.에디트컬럼(true, 가변에디트컬럼);

			인보이스정보바인딩(null);

			tbx파일번호.Focus();


			// ********** 물류관리
			헤더_물류관리.클리어();
			납품처바인딩("", "", "", "");
			ctx송품방법.글("-");
			ctx송품상세.글("-");
			ctx납품처.글("-");
			lbl운임통화.Text = "";

			//cbo물류의뢰.사용(true);
			//cbo물류상세.사용(true);
			//cbo납품처.사용(true);
		}

		#endregion

		#region ==================================================================================================== 이벤트 == EVENT == ㄷㅍ두

		private void InitEvent()
		{
			Header.ControlValueChanged += new FreeBindingEventHandler(Header_ControlValueChanged);

			btn선사라벨.Click += Btn선사라벨_Click;

			btn메일발송.Click += new EventHandler(btn메일발송_Click);
			btn수주확인서.Click += new EventHandler(btn수주확인서_Click);	// 인쇄 3종세트
			btn견적송장.Click += new EventHandler(btn견적송장_Click);		// 인쇄 3종세트
			btn거래명세서.Click += new EventHandler(btn거래명세서_Click);	// 인쇄 3종세트

			btn수주현황표.Click += new EventHandler(btn수주현황표_Click);
			btn수주마감.Click += new EventHandler(btn수주마감_Click);

			tbx파일번호.KeyDown += new KeyEventHandler(tbx파일번호_KeyDown);
			ctx호선번호.QueryAfter += new BpQueryHandler(ctx호선번호_QueryAfter);
			ctx수주형태.QueryBefore += new BpQueryHandler(ctx수주형태_QueryBefore);

			cbo통화.SelectionChangeCommitted += new EventHandler(cbo통화_SelectionChangeCommitted);
			cbo과세구분.SelectionChangeCommitted += new EventHandler(cbo과세구분_SelectionChangeCommitted);
			cbo인도기간.SelectionChangeCommitted += new EventHandler(cbo인도기간_SelectionChangeCommitted);
			
			//btn환율.Click += new EventHandler(btn환율_Click);
			btn이윤.Click += new EventHandler(btn이윤_Click);
			btn할인.Click += new EventHandler(btn할인_Click);
			btn납기.Click += new EventHandler(btn납기_Click);
			btn표시형식.Click += new EventHandler(btn표시형식_Click);

			btn재고예약.Click += new EventHandler(btn재고예약_Click);
			btn순번정렬.Click += new EventHandler(btn순번정렬_Click);

			btn추가.Click += new EventHandler(btn추가_Click);
			btn비용추가.Click += new EventHandler(btn비용추가_Click);
			btn삭제.Click += new EventHandler(btn삭제_Click);

			tbx선사비고.MouseEnter += new EventHandler(textBox_MouseEnter);	// 비고 툴팁
			tbx커미션.MouseEnter += new EventHandler(textBox_MouseEnter);
			tbx담당비고.MouseEnter += new EventHandler(textBox_MouseEnter);
			tbx수통비고.MouseEnter += new EventHandler(textBox_MouseEnter);

			tbx담당비고.MouseLeave += new EventHandler(textBox_MouseLeave);
			tbx커미션.MouseLeave += new EventHandler(textBox_MouseLeave);
			tbx담당비고.MouseLeave += new EventHandler(textBox_MouseLeave);
			tbx수통비고.MouseLeave += new EventHandler(textBox_MouseLeave);

			grd라인.KeyDown += new KeyEventHandler(grd라인_KeyDown);
			grd라인.ValidateEdit += new ValidateEditEventHandler(grd라인_ValidateEdit);


			btn인보이스확인.Click += Btn인보이스확인_Click;


			// ********** 물류관리		
			chk자동협조전.CheckedChanged += 물류관리컨트롤상태;
			chk납기단축.CheckedChanged += 물류관리컨트롤상태;
			chk캘린더.CheckedChanged += 물류관리컨트롤상태;

			ctx송품방법.QueryBefore += Ctx송품방법_QueryBefore;
			//ctx송품상세.QueryBefore += Ctx송품방법_QueryBefore;
			ctx송품상세.QueryBefore += Ctx송품상세_QueryBefore;
			//ctx납품처.QueryBefore += Ctx송품방법_QueryBefore;
			ctx납품처.QueryBefore += Ctx납품처_QueryBefore;

			ctx송품방법.CodeChanged += Ctx송품방법_CodeChanged;
			ctx송품상세.CodeChanged += Ctx송품방법_CodeChanged;
			ctx납품처.CodeChanged += Ctx송품방법_CodeChanged;
			
			lbl납품지시서.DoubleClick += Lbl납품지시서_DoubleClick;
			btn납품지시서추가.Click += Btn납품지시서추가_Click;
			btn납품지시서복사.Click += Btn납품지시서복사_Click;
			btn협조전작성.Click += Btn협조전작성_Click;
			
			cbm납품담당자.QueryBefore += Cbm납품담당자_QueryBefore;

			rdo청구.CheckedChanged += 물류관리컨트롤상태;
			rdo자동.CheckedChanged += 물류관리컨트롤상태;
		}

		

		private void Btn선사라벨_Click(object sender, EventArgs e)
		{
			LABEL.선사라벨데이터생성(파일번호);
			유틸.메세지(공통메세지.자료가정상적으로저장되었습니다);
		}

		private void Btn인보이스확인_Click(object sender, EventArgs e)
		{			
			btn인보이스확인.Enabled = false;
		}

		private void Btn테스트_Click(object sender, EventArgs e)
		{
			EXCEL excel = new EXCEL();

			
			if (excel.OpenDialog() == DialogResult.OK)
			{
				excel.Read2();
			}
		}

		private void Header_ControlValueChanged(object sender, FreeBindingArgs e)
		{
			// 파일번호는 이벤트에서 제외
			if (sender != null && ((Control)sender).Name == "tbx파일번호")
				return;

			// 이전값과 현재값이 같을때는 이벤트를 발생시키지 않는다 (포커스만 변경되는 경우에도 발행하므로 골치 아픔)
			string oldValue = (e.Row.RowState == DataRowState.Added) ? "" : e.Row[sender.GetTag(), DataRowVersion.Original].ToString();
			string newValue = e.Row[sender.GetTag(), DataRowVersion.Current].ToString();

			if (oldValue == newValue)
				return;

			// Original 값 갱신을 위해 커밋했다가 다시 원래 상태로 되돌림
			e.Row.AcceptChanges();			
			e.Row.SetModified();	// 무조건 Modify 상태로 변경시키고 추가/수정 구분은 JobMode로 함

			// 저장버튼 활성화
			IsHeaderChanged = true;
			ToolBarSaveButtonEnabled = true;
		}

		private void tbx파일번호_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
				OnToolBarSearchButtonClicked(null, null);
		}

		private void ctx호선번호_QueryAfter(object sender, BpQueryArgs e)
		{
			ctx호선번호.CodeName = e.HelpReturn.Rows[0]["NM_VESSEL"] + " (" + e.HelpReturn.Rows[0]["NO_HULL"] + ")";
		}

		private void ctx수주형태_QueryBefore(object sender, BpQueryArgs e)
		{
			e.HelpParam.P61_CODE1 = "N";
			e.HelpParam.P62_CODE2 = "Y";
		}

		private void cbo통화_SelectionChangeCommitted(object sender, EventArgs e)
		{
			cur환율.DecimalValue = GetDb.ExchangeRate(dtp수주일자.Text, cbo통화.GetValue(), "S");

			if (cbo통화.GetValue() == "000")
			{
				cbo표시형식.SetValue("7");
				chk표시형식단가.Checked = true;
			}
			else
			{
				cbo표시형식.SetValue("3");
				chk표시형식금액.Checked = true;
			}

			Header.CurrentRow["RT_EXCH"] = cur환율.DecimalValue;
			Header.CurrentRow["TP_DIGIT"] = cbo표시형식.GetValue();
			Header.CurrentRow["TP_ROUND"] = chk표시형식단가.Checked ? "1" : "2";
			lbl운임통화.Text = cbo통화.글();
		}

		private void cbo과세구분_SelectionChangeCommitted(object sender, EventArgs e)
		{
			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
			{
				grd라인[i, "RT_VAT"] = 부가세율;
				부가세계산(i);
			}
		}

		private void cbo인도기간_SelectionChangeCommitted(object sender, EventArgs e)
		{
			if (!grd라인.HasNormalRow)
				return;

			int min = (int)grd라인.Aggregate(AggregateEnum.Min, "LT");
			int max = (int)grd라인.Aggregate(AggregateEnum.Max, "LT");

			string minDay = (min == 0) ? "STOCK" : string.Format("{0} DAYS", min);
			string maxDay = (max == 0) ? "STOCK" : string.Format("{0} DAYS", max);
			string minDate = (min == 0) ? "STOCK" : GetTo.DatePrint(dtp수주일자.ToDayDate.AddDays(min));
			string maxDate = (max == 0) ? "STOCK" : GetTo.DatePrint(dtp수주일자.ToDayDate.AddDays(max));

			if (cbo인도기간.SelectedIndex == 1)		tbx인도기간.Text = "AS BELOW";
			else if (cbo인도기간.SelectedIndex == 2)	tbx인도기간.Text = minDay + " ~ " + maxDay;
			else if (cbo인도기간.SelectedIndex == 3)	tbx인도기간.Text = minDate + " ~ " + maxDate;
			else if (cbo인도기간.SelectedIndex == 4) tbx인도기간.Text = maxDay;
			else if (cbo인도기간.SelectedIndex == 5) tbx인도기간.Text = maxDate;
		}

		private void 물류관리컨트롤상태(object sender, EventArgs e)
		{
			List<Control> 필수값 = new List<Control>();

			// 미입고 허용
			pnl미입고협조전.사용(chk자동협조전.Checked && chk납기단축.Checked);
			if (!chk미입고협조전.사용()) chk미입고협조전.Checked = false;

			// 송품방법 & 납품처
			if (chk자동협조전.Checked || chk캘린더.Checked)
			{
				pnl송품방법.사용(true);
				pnl납품처.사용(true);
				필수값.Add(ctx송품방법);
			}
			else
			{
				pnl송품방법.사용(false);
				pnl납품처.사용(false);
				ctx송품방법.클리어();
				ctx송품상세.클리어();
				ctx납품처.클리어();
				web납품처.클리어();
				ctx송품방법.CodeName = "-";
				ctx송품상세.CodeName = "-";
				ctx납품처.CodeName = "-";
			}

			// 서류담당 & 납품지시서 => 송품방법에 따라 필수값 되고 안되고 설정, 값은 항상 입력 받음
			lbl납품지시서.BackColor = chk납기단축.Checked ? 상수.필수값색상 : Color.White;
			if (lbl납품지시서.BackColor == 상수.필수값색상) 필수값.Add(lbl납품지시서);

			// 납품가능일 => 얘는 있는 경우에만 넣는 거라서 필수값 처리는 하면 안됨
			pnl납품가능일.사용(chk자동협조전.Checked && !chk납기단축.Checked);
			if (!dtp납품가능일.사용()) dtp납품가능일.클리어();

			// 납품완료일
			pnl납품완료일.사용(chk납기단축.Checked || chk캘린더.Checked);
			if (dtp납품완료일.사용()) 필수값.Add(dtp납품완료일); else dtp납품완료일.클리어();
			if (cbo납품완료시간.사용()) 필수값.Add(cbo납품완료시간); else cbo납품완료시간.값("");

			// 청구방법 & 청구금액
			if (rdo청구.Checked)
			{
				pnl청구방법.사용(true);
				pnl청구금액.사용(!rdo자동.Checked);
			}
			else
			{
				pnl청구방법.사용(false);
				pnl청구금액.사용(false);
			}

			// 납기단축(긴급) 관련
			pnl컷오프타임.사용(chk납기단축.Checked);
			if (dtp컷오프타임.사용()) 필수값.Add(dtp컷오프타임); else dtp컷오프타임.클리어();
			if (cbo컷오프시간.사용()) 필수값.Add(cbo컷오프시간); else cbo컷오프시간.값("");

			pnl발주장소.사용(chk납기단축.Checked);
			if (cbo발주장소.사용()) 필수값.Add(cbo발주장소); else cbo발주장소.값("");

			pnl발주일시.사용(chk납기단축.Checked);
			if (dtp발주일시.사용()) 필수값.Add(dtp발주일시); else dtp발주일시.클리어();
			if (cbo발주시간.사용()) 필수값.Add(cbo발주시간); else cbo발주시간.값("");

			pnl발주비고.사용(chk납기단축.Checked);
			if (!tbx발주비고.사용()) tbx발주비고.클리어();

			// 헤더 필수값
			헤더_물류관리.필수값(필수값.ToArray());
		}

		private void Ctx송품방법_QueryBefore(object sender, BpQueryArgs e)
		{
			e.QueryCancel = true;
			if (tbx파일번호.사용()) return;

			H_CZ_DLV_CODE_SEL f = new H_CZ_DLV_CODE_SEL(매출처코드, IMO번호);

			// 값이 들어올때만 적용
			if (f.ShowDialog() == DialogResult.OK)
			{
				ctx송품방법.값(f.물류의뢰_코드);
				ctx송품방법.글(f.물류의뢰_이름);
				ctx송품상세.값(f.물류상세_코드);
				ctx송품상세.글(f.물류상세_이름);
				ctx납품처.값(f.납품처_코드);
				ctx납품처.글(f.납품처_이름 == "" ? "-" : f.납품처_이름);

				납품처바인딩(f.납품처_구분코드, f.납품처_구분이름, f.납품처_주소, f.납품처_연락처);
				Ctx송품방법_CodeChanged(sender, null);
				헤더_물류관리.변경됨();  // 쿼리캔슬땜에 체인지드 이벤트 발생안해서 강제 변경
			}
		}

		private void Ctx송품상세_QueryBefore(object sender, BpQueryArgs e)
		{
			e.QueryCancel = true;
			if (tbx파일번호.사용()) return;

			if (ModifierKeys == Keys.Control && ctx송품방법.값() == "WM004")
			{
				// 특별한 기능
				H_CZ_DLV_PORT_SEL f = new H_CZ_DLV_PORT_SEL();

				if (f.ShowDialog() == DialogResult.OK)
				{
					ctx송품상세.값(f.물류상세_코드);
					ctx송품상세.글(f.물류상세_이름);
										
					//Ctx송품방법_CodeChanged(sender, null);
					헤더_물류관리.변경됨();  // 쿼리캔슬땜에 체인지드 이벤트 발생안해서 강제 변경
				}
			}
			else
			{
				Ctx송품방법_QueryBefore(sender, e);
			}
		}

		private void Ctx납품처_QueryBefore(object sender, BpQueryArgs e)
		{
			e.QueryCancel = true;
			if (tbx파일번호.사용()) return;

			if (ModifierKeys == Keys.Control)
			{
				// 특별한 기능
				H_CZ_DLV_PARTNER_SEL f = new H_CZ_DLV_PARTNER_SEL();

				if (f.ShowDialog() == DialogResult.OK)
				{					
					ctx납품처.값(f.납품처_코드);
					ctx납품처.글(f.납품처_이름 == "" ? "-" : f.납품처_이름);

					납품처바인딩(f.납품처_구분코드, f.납품처_구분이름, f.납품처_주소, f.납품처_연락처);
					Ctx송품방법_CodeChanged(sender, null);
					헤더_물류관리.변경됨();  // 쿼리캔슬땜에 체인지드 이벤트 발생안해서 강제 변경
				}
			}
			else
			{
				Ctx송품방법_QueryBefore(sender, e);
			}
		}


		private void Ctx송품방법_CodeChanged(object sender, EventArgs e)
		{
			BpCodeTextBox 코드박스 = (BpCodeTextBox)sender;

			// 값이 없어질때만 적용
			if (코드박스.CodeValue == "")
			{
				ctx송품방법.클리어();
				ctx송품상세.클리어();
				ctx납품처.클리어();

				ctx송품방법.CodeName = "-";
				ctx송품상세.CodeName = "-";
				ctx납품처.CodeName = "-";
				web납품처.클리어();
			}

			물류관리컨트롤상태(null, null);
		}

		private void Lbl납품지시서_DoubleClick(object sender, EventArgs e)
		{
			if (lbl납품지시서.Text != "")
				워크플로우.다운로드(파일번호, "51", true);
		}

		private void Btn납품지시서추가_Click(object sender, EventArgs e)
		{
			if (tbx파일번호.사용())
				return;

			파일선택 fs = new 파일선택(파일유형.메일);

			if (fs.열기())
			{
				워크플로우 wf = new 워크플로우(파일번호, "51");   // 51:납품지시서
				wf.파일추가("", "", fs.파일이름);
				wf.저장();

				lbl납품지시서.Text = fs.파일이름;
			}
		}

		private void Btn납품지시서복사_Click(object sender, EventArgs e)
		{
			if (tbx파일번호.사용())
				return;

			if (메시지.일반선택("발주서를 복사하시겠습니까?"))
			{
				// 기존 발주서 가져오기
				string query = "SELECT TOP 1 * FROM CZ_MA_WORKFLOWL WHERE CD_COMPANY = '" + 상수.회사코드 + "' AND NO_KEY = '" + 파일번호 + "' AND TP_STEP = '08' AND YN_INCLUDED = 'N' ORDER BY NO_LINE DESC";
				DataTable dt = 디비.결과(query);

				// 워크에 추가
				워크플로우 wf = new 워크플로우(파일번호, "51");   // 51:납품지시서
				wf.파일복사("", "", dt.첫행("NM_FILE").문자(), dt.첫행("NM_FILE_REAL").문자());
				wf.저장();

				lbl납품지시서.Text = dt.첫행("NM_FILE").문자();
			}
		}

		private void Btn협조전작성_Click(object sender, EventArgs e)
		{
			try
			{
				if (ToolBarSaveButtonEnabled)
				{
					메시지.경고알람("저장 후 진행바랍니다.");
					return;
				}

				// 실행
				DataTable dt = 디비.결과("PI_CZ_DX_AUTO_GIR", 상수.회사코드, 파일번호);

				// 오류발생!! dt에 값이 있을때만 오류임
				if (dt.존재())				
					throw new Exception(dt.첫행(0).문자());
								
				if		(ctx송품방법.값().발생("WM")) lbl물류협조전.ForeColor = Color.Red;
				else if (ctx송품방법.값().발생("PM")) lbl포장협조전.ForeColor = Color.Red;

				메시지.저장완료();
			}
			catch (Exception ex)
			{
				메시지.오류알람(ex);
			}
		}

		private void Cbm납품담당자_QueryBefore(object sender, BpQueryArgs e)
		{
			e.QueryCancel = true;

			if (tbx파일번호.사용())
				return;

			H_CZ_MAIL_SEL f = new H_CZ_MAIL_SEL(매출처코드, IMO번호, 파일번호);

			// 값이 들어올때만 적용
			if (f.ShowDialog() == DialogResult.OK)
			{
				cbm납품담당자.클리어();
				DataRow[] rows = f.결과.선택("SEQ < 0");

				// SEQ 중복뜨면 안되니까 음수로 다시 순번 해줌
				for (int i = 0; i < rows.Length; i++)
					rows[i]["SEQ"] = (i + 1) * -1;

				// 추가
				foreach (DataRow row in f.결과.Rows)
					cbm납품담당자.AddItem(row["SEQ"].문자(), row["NM_MAIL"].문자());

				헤더_물류관리.변경됨();   // 쿼리캔슬땜에 체인지드 이벤트 발생안해서 강제 변경
			}
		}

		#endregion

		#region ==================================================================================================== 버튼 이벤트

		private void btn메일발송_Click(object sender, EventArgs e)
		{
			H_CZ_MAIL_OPTION f = new H_CZ_MAIL_OPTION(상수.회사코드, MailType.Acknowledge, 파일번호, 상수.사원번호, "", 상수.회사코드);
			f.ShowDialog();
		}

		private void btn수주현황표_Click(object sender, EventArgs e)
		{
			H_CZ_STATUS_TABLE f = new H_CZ_STATUS_TABLE(파일번호);

			if (f.ShowDialog() == DialogResult.OK)
				OnToolBarSearchButtonClicked(null, null);
		}

		private void btn수주마감_Click(object sender, EventArgs e)
		{
			H_CZ_ORDER_CLOSE f = new H_CZ_ORDER_CLOSE(파일번호);

			if (f.ShowDialog() == DialogResult.OK)
				OnToolBarSearchButtonClicked(null, null);
		}
		
		//private void btn환율_Click(object sender, EventArgs e)
		//{
		//	for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
		//		행계산(i, "RT_EXCH");
		//}		

		private void btn이윤_Click(object sender, EventArgs e)
		{
			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
			{
				grd라인[i, "RT_PROFIT"] = cur이윤율.DecimalValue;
				행계산(i, "RT_PROFIT");
			}
		}

		private void btn할인_Click(object sender, EventArgs e)
		{
			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
			{
				grd라인[i, "RT_DC"] = cur할인율.DecimalValue;
				행계산(i, "RT_DC");
			}
		}
				
		private void btn납기_Click(object sender, EventArgs e)
		{
			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
			{
				if (GetTo.Int(grd라인[i, "SEQ_SO"]) < 90000)
					grd라인[i, "LT"] = cur납기.DecimalValue;
			}
		}

		private void btn표시형식_Click(object sender, EventArgs e)
		{
			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
				행계산(i, "");
		}

		private void btn재고예약_Click(object sender, EventArgs e)
		{
			if (ToolBarSaveButtonEnabled)
			{
				ShowMessage(DD("CZ_저장 후 진행바랍니다."));
				return;
			}

			H_CZ_STOCK_BOOK f = new H_CZ_STOCK_BOOK(상수.회사코드, 파일번호);

			if (f.ShowDialog() == DialogResult.OK)
			{
				
			}
		}

		private void btn순번정렬_Click(object sender, EventArgs e)
		{			
			for (int i = grd라인.Rows.Fixed, seq = 1; i < grd라인.Rows.Count; i++, seq++)
				grd라인[i, "NO_DSP"] = seq;
		}

		private void btn추가_Click(object sender, EventArgs e)
		{
			DataTable dtLine = GetTo.DataTable(grd라인.DataTable, "NO_SO", "SEQ_SO");
			string lineXml = GetTo.Xml(dtLine);

			// 아이템 선택 팝업
			H_CZ_ITEM_ADD f = new H_CZ_ITEM_ADD(파일번호, lineXml);

			if (f.ShowDialog() == DialogResult.OK)
			{
				DataTable dtItem = f.Items;
				if (dtItem == null) return;

				foreach (DataRow row in dtItem.Rows)
				{
					grd라인.Rows.Add();
					grd라인.Row = grd라인.Rows.Count - 1;

					grd라인["CD_COMPANY"] = 상수.회사코드;

					for (int j = grd라인.Cols.Fixed; j < grd라인.Cols.Count; j++)
					{
						string colName = grd라인.Cols[j].Name;
						if (dtItem.Columns.Contains(colName)) grd라인[colName] = row[colName];
					}

					grd라인.AddFinished();
					행계산(grd라인.Row, "");
				}

				합계계산();
			}
		}

		private void btn비용추가_Click(object sender, EventArgs e)
		{
			H_CZ_EXTRA_COST f = new H_CZ_EXTRA_COST();

			if (f.ShowDialog() == DialogResult.OK)
			{
				int maxLineNumber = (int)grd라인.Aggregate(AggregateEnum.Max, "SEQ_SO");

				if (maxLineNumber > 90000)
					maxLineNumber++;
				else
					maxLineNumber = 90001;

				grd라인.Rows.Add();
				grd라인.Row = grd라인.Rows.Count - 1;
				grd라인["CD_COMPANY"] = 상수.회사코드;
				grd라인["NO_SO"] = 파일번호;
				grd라인["SEQ_SO"] = maxLineNumber;
				grd라인["NM_ITEM_PARTNER"] = f.SelectedItem["NAME"];
				grd라인["CD_ITEM"] = f.SelectedItem["CODE"];
				grd라인["NM_ITEM"] = f.SelectedItem["NAME"];
				grd라인["QT_SO"] = 1;
				grd라인["RT_DC"] = 0;
				grd라인["LT"] = 0;
				grd라인.AddFinished();

				grd라인.Col = grd라인.Cols["UM_EX_Q"].Index;
				grd라인.Focus();
			}
		}

		private void btn삭제_Click(object sender, EventArgs e)
		{
			if (grd라인.Row < grd라인.Rows.Fixed)
			{
				ShowMessage("아이템이 선택되지 않았습니다.");
				return;
			}

			grd라인.Rows.Remove(grd라인.Row);
		}

		private void textBox_MouseEnter(object sender, EventArgs e)
		{
			TextBoxExt textBox = (TextBoxExt)sender;
			RemarkTip.Show(textBox.Text, textBox);
		}

		private void textBox_MouseLeave(object sender, EventArgs e)
		{
			RemarkTip.RemoveAll();
		}

		#endregion

		#region ==================================================================================================== 그리드 이벤트

		private void grd라인_KeyDown(object sender, KeyEventArgs e)
		{
			string colName = grd라인.Cols[grd라인.Col].Name;

			// 수량, 매출견적단가만 복붙 가능. 아니면 다 팅구기
			if (!colName.In("QT_SO", "UM_EX_Q"))
				return;

			if (e.KeyData == (Keys.Control | Keys.V))
			{
				string[,] clipboard = Util.GetClipboardValues();

				for (int i = 0; i < clipboard.GetLength(0); i++)
				{
					// 값
					int row = grd라인.Row + i;
					string val = clipboard[i, 0];

					// 복붙 및 계산
					grd라인[row, grd라인.Col] = val;
					행계산(row, colName);

					// 마지막 행이면 종료
					if (grd라인.Row + i == grd라인.Rows.Count - 1)
						break;
				}
			}
		}

		private void grd라인_ValidateEdit(object sender, ValidateEditEventArgs e)
		{
			string colName = grd라인.Cols[e.Col].Name;
			string oldValue = GetTo.String(grd라인[e.Row, e.Col]);

			if (colName == "CD_ITEM")
			{
				string itemCode = EngHanConverter.KorToEng(grd라인.EditData);

				if (itemCode == "")
				{
					grd라인["NM_ITEM"] = "";
				}
				else
				{
					string query = @"
SELECT
	  CD_ITEM
	, NM_ITEM
FROM MA_PITEM WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + 상수.회사코드 + @"'
	AND CD_ITEM = '" + itemCode + @"'
	AND YN_USE = 'Y'";

					DataTable dt = DBMgr.GetDataTable(query);

					if (dt.Rows.Count == 1)
					{
						grd라인["CD_ITEM"] = dt.Rows[0]["CD_ITEM"];
						grd라인["NM_ITEM"] = dt.Rows[0]["NM_ITEM"];	
					}
					else
					{
						H_CZ_MA_PITEM f = new H_CZ_MA_PITEM(itemCode);

						if (f.ShowDialog() == DialogResult.OK)
						{
							grd라인["CD_ITEM"] = f.ITEM["CD_ITEM"];
							grd라인["NM_ITEM"] = f.ITEM["NM_ITEM"];
						}
						else
						{
							grd라인["CD_ITEM"] = oldValue;
							e.Cancel = true;
							tbx포커스.Focus();	// 제자리로 돌아오기 위해 포커스 왔다갔다 한번 하기
							grd라인.Focus();
						}
					}
				}
			}
			else
			{
				행계산(e.Row, colName);
			}
		}

		#endregion

		#region ==================================================================================================== 조회 == SEARCH == ㄴㄷㅁㄱ

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			base.OnToolBarSearchButtonClicked(sender, e);

			// 갈매기 발동이거나 테스트 서버일때 삭제해야할 컬럼
			if (!권한.라이브서버())
			{
				grd라인.컬럼삭제("DC_RMK");
				grd라인.컬럼삭제("YN_GULL");
			}

			// ********** 조회
			tbx파일번호.한글을영어();

			try
			{
				DataTable dtHead = TSQL.결과(프로시저H, 상수.회사코드, 파일번호, 신규여부 ? "Q" : "S");
				DataTable dtLine = TSQL.결과(프로시저L, 상수.회사코드, 파일번호, 신규여부 ? "Q" : "S");

				if (!dtHead.존재())
				{
					초기화();
					유틸.경고발생("선택된 자료가 없습니다.");
				}

				UTIL.작업중("조회중입니다.");
				grd라인.그리기중지();

				// ********** 신규모드
				if (신규여부)
				{					
					// 인터컴퍼니 체크
					string query = @"
SELECT
	CD_PARTNER = B.CD_PARTNER
,	NM_PARTNER = B.LN_PARTNER
FROM CZ_SA_INTERCOMPANY AS A WITH(NOLOCK)
JOIN MA_PARTNER			AS B WITH(NOLOCK) ON A.CD_COMPANY = B.CD_COMPANY AND A.CD_PARTNER_SO = B.CD_PARTNER
WHERE 1 = 1
	AND A.CD_COMPANY = '" + 상수.회사코드 + @"'
	AND A.CD_PREFIX = '" + 파일번호.왼쪽(2) + "'";

					DataTable dtIC = TSQL.결과(query);

					if (dtIC.존재())
					{
						dtHead.첫행()["CD_PARTNER"] = dtIC.첫행("CD_PARTNER");
						dtHead.첫행()["NM_PARTNER"] = dtIC.첫행("NM_PARTNER");
					}

					// 수주일자와 환율은 오늘자로 세팅
					dtHead.첫행()["DT_SO"] = 유틸.오늘();
					dtHead.첫행()["RT_EXCH"] = 코드.환율(dtHead.첫행("CD_EXCH"), "S");
					
					// 라인그리드는 Add 상태로 변경
					dtLine.상태변환(DataRowState.Added);
				}


				//tbx담당비고.AutoSize = false;
				//tbx담당비고.TextAlign = HorizontalAlignment.Center;
				//dtp납품예정일.TextAlign = HorizontalAlignment.Center;

				// ********** 바인딩
				Header.바인딩(dtHead);
				grd라인.바인딩(dtLine);
				인보이스정보바인딩(dtHead);

				// ********** 물류관리 바인딩
				DataSet ds물류 = 디비.결과s("PS_CZ_SA_SOH_DLV_2", 상수.회사코드, 파일번호);
				헤더_물류관리.바인딩(ds물류.Tables[0]);
				납품처바인딩(ds물류.Tables[0].첫행("TP_ADDRESS").문자(), ds물류.Tables[0].첫행("NM_ADDRESS").문자(), ds물류.Tables[0].첫행("DC_ADDRESS").문자(), ds물류.Tables[0].첫행("NO_TEL").문자());

				// ***** 별도로 바인딩 해줘야 할 얘들
				lbl납품지시서.Text = ds물류.Tables[1].첫행("NM_FILE").문자();

				// 입고 상태
				string 입고상태 = ds물류.Tables[1].첫행("YN_GR").문자();
				if		(입고상태 == "")		lbl입고.ForeColor = Color.LightGray;
				else if (입고상태 == "C")		lbl입고.ForeColor = Color.Blue;
				else if (입고상태 == "P")		lbl입고.ForeColor = Color.Red;

				// 물류협조전 (C:종결,O,R 기타 나머지 전부 진행중)
				string 물류헙조전 = ds물류.Tables[1].첫행("STA_GIR").문자();
				if		(물류헙조전 == "")	lbl물류협조전.ForeColor = Color.LightGray;
				else if (물류헙조전 == "C")	lbl물류협조전.ForeColor = Color.Blue;
				else 						lbl물류협조전.ForeColor = Color.Red;

				// 포장협조전
				string 포장협조전 = ds물류.Tables[1].첫행("STA_PACK").문자();
				if		(포장협조전 == "")	lbl포장협조전.ForeColor = Color.LightGray;
				else if (포장협조전 == "C")	lbl포장협조전.ForeColor = Color.Blue;
				else 						lbl포장협조전.ForeColor = Color.Red;

				chk캘린더.Checked = 선용여부;		// 캘린더는 선용만 사용함
				lbl운임통화.Text = cbo통화.글();  // 쿼리에선 값이 없을수도 있으므로 헤더에 있는거 바인딩해줌

				// 데이터 없으면 자동협조전만 디폴트로 체크해줌
				if (!ds물류.Tables[0].존재())
				{
					chk자동협조전.Checked = true;
					ctx송품방법.CodeName = "-";
					ctx송품상세.CodeName = "-";
					ctx납품처.CodeName = "-";
				}

				// ********** 기타
				수주상태코드 = dtHead.첫행("STA_SO").문자();
				수주마감코드 = dtHead.첫행("YN_CLOSE").문자();
				lbl계약할인할인율.Text = dtHead.첫행("RT_COMMISSION").문자("#,##0.##");

				//수주상태코드 = "O";	// 테스트모드, 수통모드 강제 변경

				// 환율, 부가세 적용
				if (신규여부)
				{
					for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
					{
						grd라인[i, "RT_VAT"] = 부가세율;	// 부가세율
						행계산(i, "RT_EXCH");			// 재계산 (환율적용 및 부가세 계산)
					}
				}

				// 하단 합계 계산
				합계계산();

				// 상태에 따라 라벨 변경
				if (수주상태코드 != "O")	lbl수발주통보서.강조(강조모드.빨강);
				if (수주마감코드 == "Y")	lbl수주마감.강조(강조모드.빨강);
				if (수주마감코드 == "P")	lbl수주마감.강조(강조모드.검정);
				if (dtHead.첫행("YN_GIR").문자() == "Y")	lbl물류협조전2.강조(강조모드.빨강);
				if (dtHead.첫행("YN_PACK").문자() == "Y")	lbl포장협조전2.강조(강조모드.빨강);

				// 수주상태에 따라 컨트롤 변경 ==> 수정할 수 없는 상태
				if (수주상태코드 != "O" || 수주마감코드.포함("Y", "P"))
				{
					// 원그리드
					pnl담당자.사용(false);
					pnl영업그룹.사용(false);
					pnl호선.사용(false);
					pnl매출처그룹.사용(false);
					pnl환율.사용(false);
					pnl수주형태.사용(false);
					pnl과세구분.사용(false);
					pnl지불조건.사용(false);
					pnl원산지.사용(false);
					pnl인도기간.사용(false);
					pnl선적조건.사용(false);
					pnl포장형태.사용(false);
					pnl이윤.사용(false);
					pnl할인.사용(false);
					pnl납기.사용(false);
					pnl표시형식.사용(false);

					// 버튼
					btn추가.사용(false);
					btn비용추가.사용(false);
					btn삭제.사용(false);

					// 라인그리드
					grd라인.에디트컬럼(false, 가변에디트컬럼);

					// 테스트 서버는 수정 되도록 (갈매기)
					if (!Certify.IsLive())
					{
						pnl수주일자.사용(true);
						pnl이윤.사용(true);
						pnl할인.사용(true);
						grd라인.SetEditColumn(true, "RT_PROFIT", "UM_EX_Q", "RT_DC", "UM_EX_S", "LT");
					}
				}

				// 마무리
				ToolBarDeleteButtonEnabled = true;  // 삭제 버튼 강제 활성화 (L이 없는 경우 활성화가 안됨)
				pnl파일번호.사용(false);				// 파일번호 박스는 수정 불가능				
				grd라인.그리기시작();
				유틸.작업중();
				tbx포커스.Focus();
			}
			catch (Exception ex)
			{
				유틸.메세지(ex);
			}			
		}


		private void 납품처바인딩(string 구분코드, string 구분이름, string 주소, string 연락처)
		{
			// 태그에 코드값 넣어서 대분류랑 비교
			web납품처.Tag = 구분코드;
			string 색상 = 구분코드 == ctx송품방법.값() ? "blue" : "red";

			// 바인딩
			string 스타일 = "";
			string 바디 = @"
<div style='line-height:15px; padding:3px'>
" + (구분이름 == "" ? "" : "  <b><font color='" + 색상 + "'>[" + 구분이름 + "]</font></b> ") + 주소 + "<br />" + 연락처 + @"
</div>";

			web납품처.바인딩(스타일, 바디, false);
		}

		private void 인보이스정보바인딩(DataTable dt)
		{			
			if (dt == null)
			{
				dt = new DataTable();
				dt.Columns.Add("FG_PARTNER_NM");
				dt.Columns.Add("AUTO_INV");
				dt.Columns.Add("INVOICE_COMPANY");
				dt.Columns.Add("INVOICE_ADDRESS");
				dt.Columns.Add("INVOICE_EMAIL");
				dt.Columns.Add("INVOICE_TEL");
				dt.Columns.Add("INVOICE_RMK");
				dt.Columns.Add("FG_INV_NM");
				dt.Columns.Add("YN_COLOR");
				dt.Rows.Add();
			}
			else if (dt.Rows.Count == 0)
			{
				dt.Rows.Add();
			}
			else
			{

			}

			string autoInv = dt.Rows[0]["AUTO_INV"].ToString();
			autoInv = (autoInv == "자동" ? "<span style='color:blue;'>" : "<span style='color:red;'>") + autoInv + "</span>";

			int lenth = 22;
			string tel = dt.Rows[0]["INVOICE_TEL"].ToString();
			tel = tel.Length > lenth ? tel.Substring(0, lenth - 3) + "..." : tel;

			//lay헤드컨테이너.ColumnStyles[2].Width = 470;

			string 스타일 = @"
.dx-viewbox tr > * { line-height:15px; height:28px; }
.dx-viewbox div    { line-height:15px; text-overflow:ellipsis; white-space:nowrap; overflow:hidden; }";

			string 바디 = @"
<div>
	<table class='dx-viewbox'>
		<colgroup>
			<col style='width:15%;' />
			<col style='width:41%;' />
			<col style='width:15%;' />
			<col style='width:29%;' />
		</colgroup>
		<tr>
			<th>구분</th>
			<td>" + dt.Rows[0]["FG_PARTNER_NM"] + @"</td>
			<th>발송여부</th>
			<td><b>" + autoInv + @"</b></td>
		</tr>
		<tr>
			<th style='height:48px;'>회사명</th>
			<td colspan='3'>" + dt.Rows[0]["INVOICE_COMPANY"].문자().웹() + @"</td>
		</tr>
		<tr>
			<th style='height:66px;'>주소</th>
			<td colspan='3'>" + dt.Rows[0]["INVOICE_ADDRESS"].문자().웹() + @"</td>
		</tr>
		<tr>
			<th>이메일</th>
			<td title='" + dt.Rows[0]["INVOICE_EMAIL"] + "'>" + dt.Rows[0]["INVOICE_EMAIL"] + @"</td>
			<th>전화번호</th>
			<td title='" + dt.Rows[0]["INVOICE_TEL"] + "'>" + tel + @"</td>
		</tr>
		<tr>
			<th>발송방법</th>
			<td>" + dt.Rows[0]["FG_INV_NM"] + @"</td>
			<th>컬러여부</th>
			<td>" + dt.Rows[0]["YN_COLOR"] + @"</td>
		</tr>
		<tr>
			<th>비고</th>
			<td style='height:48px;' colspan='3'><div style='height:48px'>" + dt.Rows[0]["INVOICE_RMK"].문자().웹() + @"</div></td>
		</tr>
	</table>
</div>";

			web공지.바인딩(스타일, 바디, false);
		}
	
		#endregion

		#region ==================================================================================================== 추가 == ADD == ㅁㅇㅇ

		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
			if (!MsgAndSave(PageActionMode.Search))
				return;

			초기화();
		}

		#endregion

		#region ==================================================================================================== 저장 == SAVE == ㄴㅁㅍㄷ

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			



			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;
			DebugMode debug = Control.ModifierKeys == (Keys.Control | Keys.Alt) ? DebugMode.Popup : DebugMode.Print;
			tbx포커스.Focus();

			


			// ********** 저장
			try
			{
				if (상수.사원번호 == "S-343")
				{
					cur합계금액.DecimalValue = grd라인.데이터테이블().계산("SUM(AM_EX_S)").실수();
					btn인보이스확인.Enabled = false;
				}

				#region ********** 유효성 검사

				if (ctx담당자.값() == "")		유틸.경고발생("담당자를 입력하십시오.");
				if (ctx영업그룹.값() == "")	유틸.경고발생("영업그룹을 입력하십시오.");
				if (ctx호선번호.값() == "")	유틸.경고발생("호선을 입력하십시오.");
				if (cbo거래처그룹.값() == "")	유틸.경고발생("거래처그룹을 입력하십시오.");
				if (tbx주문번호.값() == "")	유틸.경고발생("주문번호를 입력하십시오.");
				if (ctx수주형태.값() == "")	유틸.경고발생("수주형태를 입력하십시오.");
				if (cbo과세구분.값() == "")	유틸.경고발생("과세구분을 입력하십시오.");
				if (cbo지불조건.값() == "")	유틸.경고발생("지불조건을 입력하십시오.");
				if (cbo선적조건.값() == "")	유틸.경고발생("선적조건을 입력하십시오.");
				if (tbx선적조건.값() == "")	유틸.경고발생("선적조건을 입력하십시오.");
				if (btn인보이스확인.Enabled)	유틸.경고발생("인보이스확인을 하십시오.");
				if (cur합계금액.값() != grd라인.데이터테이블().계산("SUM(AM_EX_S)").실수())	유틸.경고발생("합계금액이 일치하지 않습니다");

				// 제제 선박 체크
				string query = "SELECT 1 FROM V_CZ_MA_CODEDTL WHERE CD_COMPANY = '" + 상수.회사코드 + "' AND CD_FIELD = 'CZ_SA00062' AND CD_SYSDEF = '" + ctx호선번호.값() + "'";
				if (디비.결과(query).존재())
					메시지.경고발생("이란 제제 선박입니다.");

				// 물류관리
				헤더_물류관리.유효성검사();

				if (ctx납품처.값() != "" && ctx송품방법.값() != web납품처.태그())
				{
					if (!메시지.경고선택("송품방법과 납품처가 다릅니다. 계속 진행하시겠습니까?"))
						return;
				}

				// 동일호선, 동일 주문번호 저장 안되도록
				query = @"SELECT 1 FROM SA_SOH WHERE CD_COMPANY = '" + 상수.회사코드 + "' AND NO_SO != '" + 파일번호 + "' AND NO_IMO = '" + ctx호선번호.값() + "' AND NO_PO_PARTNER = '" + tbx주문번호.Text + "'";
				DataTable dtDup = TSQL.결과(query);

				if (!파일번호.시작("CL") && dtDup.존재())
				{
					if (!유틸.메세지("주문번호가 중복되었습니다. 계속 진행하시겠습니까?", 메세지구분.경고선택))
						return;
				}

				// 발주서 파싱된 경우 합계금액 검사


				// 그리드 검사
				if (!base.Verify())
					return;

				for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
				{
					// 부가세 소수점 존재하는지 체크
					decimal 부가세 = GetTo.Decimal(grd라인[i, "AM_VAT"]);

					if (부가세 - Calculator.버림(부가세) > 0)
					{
						grd라인.Row = i;
						grd라인.Col = grd라인.Cols["AM_VAT"].Index;
						ShowMessage("소수 이하의 부가세가 존재합니다.");
						return;
					}

					// 납기 검사
					if (GetTo.String(grd라인[i, "LT"]) == "")
					{
						grd라인.Row = i;
						grd라인.Col = grd라인.Cols["LT"].Index;
						ShowMessage("납기를 입력하십시오.");
						return;
					}
				}			

				// 조기경보시스템 → 미수금 체크
				//if (상수.사원번호 != "S-343")
				//{
					WarningLevel warningLevel = WarningLevel.정상;
					string message = "";
					string 제외여부 = "";
				string 지불조건제외여부 = "";

				EalryWarningSystem ews = new EalryWarningSystem();
					
				ews.미수금확인(ctx매출처.CodeValue, ref warningLevel, ref message, ref 제외여부, ref 지불조건제외여부);

					if (message != "")
						ShowMessage(message);

					if (warningLevel == WarningLevel.사용불가 && 제외여부 != "Y")
						return;
				//}

				#endregion
				
				DataTable dtHead = Header.GetChanges();
				DataTable dtLine = grd라인.GetChanges();

				if (dtHead != null)
					dtHead.Rows[0]["RT_EXCH"] = cur환율.DecimalValue;	// 환율 변경시 적용이 안됨

				if (수주상태코드 == "" || 수주상태코드 == "O" || !Certify.IsLive())
				{					
					// ********** 경고마스터 확인
					if (dtLine != null)
					{ 
						DataTable dtItem = dtLine.Copy();
						dtItem.Columns["NO_SO"].ColumnName = "NO_FILE";
						dtItem.Columns["SEQ_SO"].ColumnName = "NO_LINE";

						WARNING warning = new WARNING(WARNING_TARGET.수주)
						{
							파일구분		= 파일번호.Left(2)
						,	매출처코드	= ctx매출처.CodeValue
						,	IMO번호		= ctx호선번호.CodeValue
						,	아이템		= dtItem
						,	SQLDebug	= sqlDebug
						};

						// 조회
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
					}





					// ********** 재고 적용했는지 확인
					if (grd라인.DataTable.Select("(QT_AVST > 0 AND QT_BOOK_MY = 0) OR (QT_AVPO > 0 AND QT_HOLD_MY = 0)").Length > 0)
						ShowMessage("");

					// ********** 저장
					string headXml = 신규여부 ? GetTo.Xml(dtHead, RowState.Added) : GetTo.Xml(dtHead);
					string lineXml = 신규여부 ? GetTo.Xml(dtLine, RowState.Added) : GetTo.Xml(dtLine);
					DBMgr.ExecuteNonQuery("SP_CZ_SA_SO_REG_XML", debug, headXml, lineXml);

					// WORKFLOW DB 저장 
					WorkFlow workflow = new WorkFlow(파일번호, "08");
					workflow.Save();

					// 테스트 서버인 경우 수통 일자 변경
					if (!Certify.IsLive())
					{
						query = "UPDATE SA_SOH SET DT_CONTRACT = DT_SO WHERE CD_COMPANY = '" + 상수.회사코드 + "' AND NO_SO = '" + 파일번호 + "' AND DT_CONTRACT < DT_SO";
						DBMgr.ExecuteNonQuery(query);
					}
				}
				else
				{
					string headXml = GetTo.Xml(dtHead);
					string lineXml = GetTo.Xml(dtLine);

					// ********** 경고마스터 확인
					if (dtLine != null)
					{ 
						DataTable dtItem = dtLine.Copy();
						dtItem.Columns["NO_SO"].ColumnName = "NO_FILE";
						dtItem.Columns["SEQ_SO"].ColumnName = "NO_LINE";

						WARNING warning = new WARNING(WARNING_TARGET.수주)
						{
							파일구분		= 파일번호.Left(2)
						,	매출처코드	= ctx매출처.CodeValue
						,	IMO번호		= ctx호선번호.CodeValue
						,	아이템		= dtItem
						,	SQLDebug	= sqlDebug
						};

						// 조회
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
					}


					DBMgr.ExecuteNonQuery("SP_CZ_SA_SO_REG_XML_UPDATE", headXml, lineXml);

				}

				// ********** 물류관리 저장
				DataTable 물류관리 = 헤더_물류관리.데이터테이블_수정();
				DataTable 납품담당 = cbm납품담당자.데이터테이블();	// 코드만 저장해도 되는거면 이짓 안해도 되는뎅..

				if (물류관리.존재())	물류관리.컬럼추가("NO_SO", 파일번호, 0);
				if (납품담당.존재())	납품담당.컬럼추가("NO_SO", 파일번호, 0);
				
				디비.실행("PX_CZ_SA_SOH_DLV", 물류관리.Json(), 납품담당.Json());



				// ********** DX테이블 저장
				키워드.견적저장(dtLine);

				// 마무리
				IsHeaderChanged = false;
				신규여부 = false;
				Header.AcceptChanges();
				grd라인.AcceptChanges();

				// 라벨 데이터 생성
				if (CODE.코드관리("CZ_DX00019").Select("CODE = '" + ctx매출처.CodeValue + "'").Length > 0)
					LABEL.선사라벨데이터생성(파일번호);

				ShowMessage(PageResultMode.SaveGood);
			}
			catch (Exception ex)
			{
				메시지.오류알람(ex);
			}
		}

		#endregion

		#region ==================================================================================================== Delete

		public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{
			if (ShowMessage(공통메세지.자료를삭제하시겠습니까) != DialogResult.Yes)
				return;

			if (!Util.CheckPW())
				return;

			try
			{
				string headXml = GetTo.Xml(Header.CurrentRow.Table, RowState.Deleted);
				DBMgr.ExecuteNonQuery("SP_CZ_SA_SO_REG_XML", headXml, DBNull.Value);

				초기화();
				ShowMessage(공통메세지.자료가정상적으로삭제되었습니다); 			
			}
			catch (Exception ex)
			{
				Util.ShowMessage(Util.GetErrorMessage(ex.Message));
			}
		}

		#endregion

		#region ==================================================================================================== Print

		public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
		{
			btn수주확인서_Click(null, null);
		}

		private void btn수주확인서_Click(object sender, EventArgs e)
		{
			// 인쇄 옵션
			H_CZ_PRT_OPTION f = new H_CZ_PRT_OPTION();

			if (Control.ModifierKeys == Keys.Control)
			{
				if (f.ShowDialog() != DialogResult.OK)
					return;
			}

			string reportCode = !f.YNDate ? "R_CZ_SA_SO_REG_ACK" : "R_CZ_SA_SO_REG_ACK2";
			Print(reportCode, "{0}_ACK_{1}.pdf", "09");
		}

		private void btn견적송장_Click(object sender, EventArgs e)
		{
			Print("R_CZ_SA_SO_REG_PRF", "{0}_PRF_{1}.pdf", "56");
		}

		private void btn거래명세서_Click(object sender, EventArgs e)
		{
			Print("R_CZ_SA_SO_REG_SOT", "{0}_SOT_{1}.pdf", "56");
		}

		private void Print(string reportCode, string nameFormat, string workflowStep)
		{
			// 저장안되면 인쇄안되게 막기
			if (ToolBarSaveButtonEnabled)
			{				
				ShowMessage(DD("CZ_저장 후 진행바랍니다."));
				return;
			}

			// 자료 불러오기
			DataTable dtHead = DBMgr.GetDataTable("PS_CZ_SA_SO_PRT_H", 상수.회사코드, 파일번호);
			DataTable dtLine = grd라인.DataTable.Copy();
			
			if (dtHead.Rows.Count != 1)
			{
				ShowMessage(공통메세지.선택된자료가없습니다);
				return;
			}

			// ********** 리포트 기본 세팅
			ReportViewer rv = new ReportViewer(reportCode, dtHead, dtLine);

			// 조건 정보
			rv.SetData("NM_PAY"		, dtHead.Rows[0]["NM_PAY"]);		// 지불조건
			rv.SetData("ORIGIN"		, dtHead.Rows[0]["ORIGIN"]);		// 원산지
			rv.SetData("COND_TRANS" , dtHead.Rows[0]["COND_TRANS"]);	// 인도기간
			rv.SetData("NM_TRANS"	, dtHead.Rows[0]["NM_TRANS"] + "   " + dtHead.Rows[0]["PORT_LOADING"]);	// 선적조건
			rv.SetData("NM_PACKING" , dtHead.Rows[0]["NM_PACKING"]);	// 포장형태
			rv.SetData("DC_RMK"		, dtHead.Rows[0]["DC_RMK"]);		// 비고

			// 금액
			decimal 매출견적금액외화	= GetTo.Decimal(dtLine.Compute("SUM(AM_EX_Q)", ""));
			decimal 매출금액외화		= GetTo.Decimal(dtLine.Compute("SUM(AM_EX_S)", ""));
			decimal 할인금액외화		= 매출금액외화 - 매출견적금액외화;	// 음수값으로 표시

			// 거래명세서인 경우 부가세 포함 원화금액을 외화필드에 올려줌
			if (reportCode == "R_CZ_SA_SO_REG_SOT")
				매출금액외화 = GetTo.Decimal(dtLine.Compute("SUM(AM_KR_S)", "")) + GetTo.Decimal(dtLine.Compute("SUM(AM_VAT)", ""));

			rv.SetData("AM_EX_Q" , 매출견적금액외화);
			rv.SetData("AM_EX_S" , 매출금액외화);
			rv.SetData("AM_EX_DC", 할인금액외화);

            // 무게
            rv.SetData("WEIGHT"  , string.Format("{0:#,##0.0#}", dtHead.Rows[0]["WEIGHT"]));

            // 인쇄 팝업
            rv.Print();

			// ********** Pdf 변환
			MsgControl.ShowMsg("PDF 저장중입니다. \r\n잠시만 기다려주세요!");

			try
			{
				// 로컬에 Pdf 저장 및 서버에 업로드
				string orgPdfName = string.Format(nameFormat, 파일번호, dtp수주일자.Text);
				string locPdfName = rv.ConvertPdf(orgPdfName);
				string svrPdfName = FileMgr.Upload_WF(상수.회사코드, 파일번호, locPdfName, false);

				// 워크플로우 처리
				WorkFlow workflow = new WorkFlow(파일번호, workflowStep);
				workflow.AddItem("", "", orgPdfName, svrPdfName);
				workflow.Save();
			}
			catch (Exception ex)
			{
				ShowMessage(ex.Message);
			}

			MsgControl.CloseMsg();
		}

		#endregion

		#region ==================================================================================================== 계산식

		private void 행계산(int i, string colName)
		{
			switch (colName)
			{
				case "RT_EXCH":		// 환율
				{
					매출견적금액계산(i);
					매출금액계산(i);

					최종이윤율계산(i);
					부가세계산(i);
					break;
				}				
				case "QT_SO":		// 수량
				{
					매입금액계산(i);

					매출견적금액계산(i);
					매출금액계산(i);

					부가세계산(i);
					break;
				}
				case "RT_PROFIT":	// 이윤율 입력
				{
					매출견적단가계산(i);
					매출견적금액계산(i);

					매출단가계산(i);
					매출금액계산(i);

					최종이윤율계산(i);
					부가세계산(i);
					break;
				}				
				case "UM_EX_Q":		// 매출견적단가 입력
				{
					매출견적금액계산(i);
					이윤율계산(i);

					매출단가계산(i);
					매출금액계산(i);

					최종이윤율계산(i);
					부가세계산(i);
					break;
				}
				case "RT_DC":		// 할인율 입력
				{
					매출단가계산(i);
					매출금액계산(i);

					최종이윤율계산(i);
					부가세계산(i);
					break;
				}
				case "UM_EX_S":		// 매출단가 입력
				{
					매출금액계산(i);

					최종이윤율계산(i);
					부가세계산(i);

					// 매출견적단가 => 별도 로직 적용 (뒤에서 부터 와야함)
					decimal 할인율			= GetTo.Decimal(grd라인[i, "RT_DC"]);
					decimal 매출단가외화		= GetTo.Decimal(grd라인[i, "UM_EX_S"]);
					decimal 매출견적단가외화	= Calculator.이윤율적용(매출단가외화, 할인율);
					grd라인[i, "UM_EX_Q"] = 매출견적단가외화;

					매출견적금액계산(i);
					이윤율계산(i);
					break;
				}
			}

			합계계산();
		}

		// 매입금액 => 각 외화, 원화단가에 수량만 곱해짐
		private void 매입금액계산(int i)
		{
			decimal 수량			= GetTo.Decimal(grd라인[i, "QT_SO"]);
			decimal 매입단가외화	= GetTo.Decimal(grd라인[i, "UM_EX_P"]);
			decimal 매입단가원화	= GetTo.Decimal(grd라인[i, "UM_KR_P"]);
			decimal 매입금액외화	= 수량 * 매입단가외화;
			decimal 매입금액원화	= 수량 * 매입단가원화;

			grd라인[i, "AM_EX_P"] = 매입금액외화;
			grd라인[i, "AM_KR_P"] = 매입금액원화;
		}

		// 이윤율 => 매입, 매출견적 둘다 원화단가 베이스 계산
		private void 이윤율계산(int i)
		{	
			decimal 매입단가원화		= GetTo.Decimal(grd라인[i, "UM_KR_P"]);
			decimal 매출견적단가원화	= GetTo.Decimal(grd라인[i, "UM_KR_Q"]);
			decimal 이윤율			= Calculator.이윤율계산(매입단가원화, 매출견적단가원화);

			grd라인[i, "RT_PROFIT"] = 이윤율;
		}

		// 매출견적단가 => 이윤이 바뀌는 경우 원화단가 베이스로 해서 환율을 나누어 계산
		private void 매출견적단가계산(int i)
		{
			decimal 매입단가원화	= GetTo.Decimal(grd라인[i, "UM_KR_P"]);
			decimal 이윤율		= GetTo.Decimal(grd라인[i, "RT_PROFIT"]);
			decimal 매출견적단가외화;

			매출견적단가외화 = Calculator.이윤율적용(매입단가원화, 이윤율) / 환율;		// 원화를 외화로 변경
			매출견적단가외화 = Calculator.반올림(매출견적단가외화, 표시형식단가용);

			grd라인[i, "UM_EX_Q"] = 매출견적단가외화;
		}

		// 매출견적금액 => 그냥 구하면 됨
		private void 매출견적금액계산(int i)
		{
			decimal 수량				= GetTo.Decimal(grd라인[i, "QT_SO"]);
			decimal 매출견적단가외화	= GetTo.Decimal(grd라인[i, "UM_EX_Q"]);
			decimal 매출견적금액외화	= Calculator.반올림(매출견적단가외화 * 수량, 표시형식금액용);
			decimal 매출견적단가원화	= GetTo.Decimal(string.Format("{0:" + grd라인.Cols["UM_KR_Q"].Format + "}", 매출견적단가외화 * 환율));
			decimal 매출견적금액원화	= GetTo.Decimal(string.Format("{0:" + grd라인.Cols["AM_KR_Q"].Format + "}", 매출견적금액외화 * 환율));

			grd라인[i, "AM_EX_Q"] = 매출견적금액외화;
			grd라인[i, "UM_KR_Q"] = 매출견적단가원화;
			grd라인[i, "AM_KR_Q"] = 매출견적금액원화;
		}

		// 매출단가 => 할인이 바뀌는 경우 외화단가 베이스로 계산 (매출견적과 매출은 같은 통화이므로 외화 베이스임)
		private void 매출단가계산(int i)
		{
			decimal 매출견적단가외화	= GetTo.Decimal(grd라인[i, "UM_EX_Q"]);
			decimal 할인율			= GetTo.Decimal(grd라인[i, "RT_DC"]);
			decimal 표시형식			= (chk표시형식단가.Checked) ? 표시형식단가용 : 4;	// 매출은 소수 4번째 자리까지 가지고 있는다 (쉽섭 방식)
			decimal 매출단가외화;
			
			매출단가외화 = Calculator.할인율적용(매출견적단가외화, 할인율);
			매출단가외화 = Calculator.반올림(매출단가외화, 표시형식);

			grd라인[i, "UM_EX_S"] = 매출단가외화;
		}

		// 매출금액 => 그냥 구하면 됨
		private void 매출금액계산(int i)
		{
			decimal 수량			= GetTo.Decimal(grd라인[i, "QT_SO"]);
			decimal 매출단가외화	= GetTo.Decimal(grd라인[i, "UM_EX_S"]);
			decimal 매출금액외화	= Calculator.반올림(매출단가외화 * 수량	, 표시형식금액용);
			decimal 매출단가원화	= GetTo.Decimal(string.Format("{0:" + grd라인.Cols["UM_KR_S"].Format + "}", 매출단가외화 * 환율));
			decimal 매출금액원화	= GetTo.Decimal(string.Format("{0:" + grd라인.Cols["AM_KR_S"].Format + "}", 매출금액외화 * 환율));

			grd라인[i, "AM_EX_S"] = 매출금액외화;
			grd라인[i, "UM_KR_S"] = 매출단가원화;
			grd라인[i, "AM_KR_S"] = 매출금액원화;
		}

		// 최종이윤율 => 매입, 매출 둘다 원화금액 베이스 계산
		private void 최종이윤율계산(int i)
		{			
			decimal 매입금액원화	= GetTo.Decimal(grd라인[i, "AM_KR_P"]);
			decimal 매출금액원화	= GetTo.Decimal(grd라인[i, "AM_KR_S"]);
			decimal 최종이윤율	= Calculator.이윤율계산(매입금액원화, 매출금액원화);

			grd라인[i, "RT_MARGIN"] = 최종이윤율;
		}

		// 부가세 => 나누기 10
		private void 부가세계산(int i)
		{
			decimal 매출금액원화	= GetTo.Decimal(grd라인[i, "AM_KR_S"]);
			decimal 부가세		= Math.Floor(매출금액원화 * (부가세율 / 100));

			grd라인[i, "AM_VAT"] = 부가세;
		}

		private void 합계계산()
		{
			DataTable dt = grd라인.DataTable.Copy();

			// 매출금액
			decimal 매입금액원화 = GetTo.Decimal(dt.Compute("SUM(AM_KR_P)", "SEQ_SO < 90000"));

			decimal 매출견적금액원화 = GetTo.Decimal(dt.Compute("SUM(AM_KR_Q)", "SEQ_SO < 90000"));
			decimal 매출견적금액외화 = GetTo.Decimal(dt.Compute("SUM(AM_EX_Q)", "SEQ_SO < 90000"));

			decimal 매출금액원화 = GetTo.Decimal(dt.Compute("SUM(AM_KR_S)", "SEQ_SO < 90000"));
			decimal 매출금액외화 = GetTo.Decimal(dt.Compute("SUM(AM_EX_S)", "SEQ_SO < 90000"));
			
			decimal 할인금액원화 = 매출견적금액원화 - 매출금액원화;
			decimal 할인금액외화 = 매출견적금액외화 - 매출금액외화;

			// 이윤
			decimal 이윤금액일반 = 매출금액원화 - 매입금액원화;
			decimal 이윤율일반 = Calculator.이윤율계산(매입금액원화, 매출금액원화);
			
			dt.Columns.Add("AM_STOCK", typeof(decimal), "QT_SO * UM_STK");
			decimal 매입금액일반 = GetTo.Decimal(dt.Compute("SUM(AM_KR_P)", "ISNULL(QT_AVST, 0) + ISNULL(QT_AVPO, 0) = 0 AND SEQ_SO < 90000"));
			decimal 매입금액재고 = GetTo.Decimal(dt.Compute("SUM(AM_STOCK)", "ISNULL(QT_AVST, 0) + ISNULL(QT_AVPO, 0) > 0 AND SEQ_SO < 90000"));
			decimal 이윤금액재고 = 매출금액원화 - (매입금액일반 + 매입금액재고);	// 매입금액일반 : 재고를 뺀 일반매입
			decimal 이윤율재고 = Calculator.이윤율계산(매입금액일반 + 매입금액재고, 매출금액원화);

			// 합계
			decimal 부대비용원화 = GetTo.Decimal(dt.Compute("SUM(AM_KR_S)", "SEQ_SO > 90000"));
			decimal 최종합계원화 = 매출금액원화 + 부대비용원화;

			decimal 부대비용외화 = GetTo.Decimal(dt.Compute("SUM(AM_EX_S)", "SEQ_SO > 90000"));
			decimal 최종합계외화 = 매출금액외화 + 부대비용외화;

			// 계약할인
			decimal 계약할인할인율 = GetTo.Decimal(lbl계약할인할인율.Text);
			decimal 계약할인합계원화 = 매출금액원화 * (1 - 계약할인할인율 / 100);
			decimal 계약할인이윤원화 = 계약할인합계원화 - (매입금액일반 + 매입금액재고);
			decimal 계약할인이윤율 = Calculator.이윤율계산(매입금액일반 + 매입금액재고, 계약할인합계원화);

			// Label 정보 변경
			lbl외화통화.Text = cbo통화.GetText();
			lbl외화통화2.Text = cbo통화.GetText();

			lbl매출견적금액원화.Text = string.Format("{0:#,##0.##}", 매출견적금액원화);
			lbl매출견적금액외화.Text = string.Format("{0:#,##0.##}", 매출견적금액외화);

			lbl할인금액원화.Text = string.Format("{0:#,##0.##}", 할인금액원화);
			lbl할인금액외화.Text = string.Format("{0:#,##0.##}", 할인금액외화);

			lbl매출금액원화.Text = string.Format("{0:#,##0.##}", 매출금액원화);
			lbl매출금액외화.Text = string.Format("{0:#,##0.##}", 매출금액외화);

			lbl이윤금액일반.Text = string.Format("{0:#,##0.##}", 이윤금액일반);
			lbl이윤율일반.Text = string.Format("{0:#,##0.##}", 이윤율일반);

			lbl이윤금액재고.Text = string.Format("{0:#,##0.##}", 이윤금액재고);
			lbl이윤율재고.Text = string.Format("{0:#,##0.##}", 이윤율재고);

			lbl부대비용원화.Text = string.Format("{0:#,##0.##}", 부대비용원화);
			lbl최종합계원화.Text = string.Format("{0:#,##0.##}", 최종합계원화);

			lbl부대비용외화.Text = string.Format("{0:#,##0.##}", 부대비용외화);
			lbl최종합계외화.Text = string.Format("{0:#,##0.##}", 최종합계외화);

			lbl계약할인합계원화.Text = string.Format("{0:#,##0}", 계약할인합계원화);
			lbl계약할인이윤원화.Text = string.Format("{0:#,##0}", 계약할인이윤원화);
			lbl계약할인이윤율.Text = string.Format("{0:#,##0.##}", 계약할인이윤율);

		}

		#endregion
	}
}

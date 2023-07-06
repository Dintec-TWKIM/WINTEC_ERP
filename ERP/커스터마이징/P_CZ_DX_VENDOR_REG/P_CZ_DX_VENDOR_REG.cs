using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Duzon.Common.Forms;
using Duzon.Common.BpControls;

using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using DX;


namespace cz
{
	public partial class P_CZ_DX_VENDOR_REG : PageBase
	{
		private string 회사코드 => grd헤드["CD_COMPANY"].문자();


		#region ==================================================================================================== 생성자 = CONSTRUCTOR

		public P_CZ_DX_VENDOR_REG()
		{
			InitializeComponent();
		}

		#endregion

		#region ==================================================================================================== 초기화 = INITIALIZE

		protected override void InitLoad()
		{
			this.페이지초기화();
			//tbx폐기번호검색.엔터검색();

			//// ********** 편집불가 패널
			//pnl폐기번호.사용(false);
			//pnl등록일자.사용(false);
			//pnl담당자.사용(false);
			//pnl결재상태.사용(false);
			//pnl수불번호.사용(false);
			//pnl전표번호.사용(false);

			////// ********** 콤보박스
			////DataSet ds = CODE.코드관리("PU_C000021");

			//cbo구분.바인딩(CODE.코드관리("PU_C000021").선택("CD_FLAG1 = 'SCRAP'", "NAME"), true);
			////cbo지급조건.바인딩(ds.Tables[1], true);
			////cbo선적조건.바인딩(ds.Tables[2], true);
			///



			InitGrid();
			InitEvent();
		}

		protected override void InitPaint()
		{
			spc메인.SplitterDistance = spc메인.Width - 1300;

			spc키워드.SplitterDistance = spc키워드.Height / 2;

			//spc메인.SplitterDistance = spc메인.Width - 1050;
			//spc메인키.SplitterDistance = spc메인키.Width - 250;   // 35 + 170 + 16
			//spc서브키.SplitterDistance = 250;

			spc메인키.SplitterDistance = 180 * 4 + 100;
			spc서브키.SplitterDistance = 180 * 1 + 100;
		}

		#endregion

		#region ==================================================================================================== 그리드 = GRID

		private void InitGrid()
		{
			MainGrids = this.컨트롤<FlexGrid>();

			// ********** 헤드
			grd헤드.세팅시작(1);

			grd헤드.컬럼세팅("CD_COMPANY"	, "회사코드"		, false);
			grd헤드.컬럼세팅("SEQ_PARTNER", "항번"			, false);
			grd헤드.컬럼세팅("RANK"		, "순위"			, 45	, "####.##"	, 정렬.가운데);
			grd헤드.컬럼세팅("CD_PARTNER"	, "코드"			, 70	, 정렬.가운데);
			grd헤드.컬럼세팅("LN_PARTNER"	, "매입처명"		, 300);
			grd헤드.컬럼세팅("FG_PARTNER"	, "거래처구분"	, 120);
			grd헤드.컬럼세팅("TP_PARTNER"	, "정품구분"		, 120);
			grd헤드.컬럼세팅("YN_HOLD"	, "홀딩"			, 45	, 정렬.가운데);
			grd헤드.컬럼세팅("DC_RMK"		, "비고"			, 300);

			grd헤드.데이터맵("FG_PARTNER", 코드.코드관리("MA_B000001"));
			grd헤드.데이터맵("TP_PARTNER", 코드.코드관리("MA_B000002"));
			grd헤드.기본키("CD_COMPANY", "CD_PARTNER");
			grd헤드.세팅종료("22.04.22.02", false);

			grd헤드.에디트컬럼("RANK", "CD_PARTNER");

			//fgd헤드.SetCodeHelpCol("CD_PARTNER", HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, new string[] { "CD_PARTNER", "LN_PARTNER" }, new string[] { "CD_PARTNER", "LN_PARTNER" });
			//fgd헤드.SetOneGridBinding(new object[] { }, one매입처, one기타);
			//fgd헤드.SetBindningRadioButton(new RadioButtonExt[] { rdo사용, rdo미사용 }, new string[] { "Y", "N" });


			// ********** 메인 키워드
			grd메인키.세팅시작(1);
			
			grd메인키.컬럼세팅("CD_COMPANY"	, "회사코드"		, false);
			grd메인키.컬럼세팅("CD_PARTNER"	, "매입처"		, false);
			grd메인키.컬럼세팅("CD_TYPE"		, "구분"			, false);
			grd메인키.컬럼세팅("SEQ"			, "순번"			, false);
			grd메인키.컬럼세팅("KEY1"			, "포함1"		, 180);
			grd메인키.컬럼세팅("KEY2"			, "포함2"		, 180);
			grd메인키.컬럼세팅("KEY3"			, "포함3"		, 180);
			grd메인키.컬럼세팅("KEY4"			, "포함4"		, 180);

			grd메인키.세팅종료("22.04.22.01", false);
			grd메인키.에디트컬럼("KEY1", "KEY2", "KEY3", "KEY4");

			// ********** 제외 키워드
			grd제외키.세팅시작(1);

			grd제외키.컬럼세팅("CD_COMPANY"	, "회사코드"		, false);
			grd제외키.컬럼세팅("CD_PARTNER"	, "매입처"		, false);
			grd제외키.컬럼세팅("CD_TYPE"		, "구분"			, false);
			grd제외키.컬럼세팅("SEQ"			, "순번"			, false);
			grd제외키.컬럼세팅("KEY1"			, "제외"			, 180	, true);
			
			grd제외키.세팅종료("22.04.22.01", false);

			// ********** 서브1 키워드
			grd서브키1.세팅시작(1);

			grd서브키1.컬럼세팅("CD_COMPANY"	, "회사코드"		, false);
			grd서브키1.컬럼세팅("CD_PARTNER"	, "매입처"		, false);
			grd서브키1.컬럼세팅("CD_TYPE"		, "구분"			, false);
			grd서브키1.컬럼세팅("SEQ"			, "순번"			, false);
			grd서브키1.컬럼세팅("KEY1"			, "포함"			, 180	, true);

			grd서브키1.세팅종료("22.04.22.01", false);

			// ********** 서브2 키워드
			grd서브키2.세팅시작(1);

			grd서브키2.컬럼세팅("CD_COMPANY"	, "회사코드"		, false);
			grd서브키2.컬럼세팅("CD_PARTNER"	, "매입처"		, false);
			grd서브키2.컬럼세팅("CD_TYPE"		, "구분"			, false);
			grd서브키2.컬럼세팅("SEQ"			, "순번"			, false);
			grd서브키2.컬럼세팅("KEY1"			, "포함1"		, 180	, true);
			grd서브키2.컬럼세팅("KEY2"			, "포함2"		, 180	, true);
			grd서브키2.컬럼세팅("KEY3"			, "포함3"		, 180	, true);
			grd서브키2.컬럼세팅("DC_RMK"		, "비고"			, 180	, true);
			
			grd서브키2.세팅종료("22.04.22.01", false);

			// ********** 연동 매입처
			grd연동.세팅시작(1);

			grd연동.SetCol("CD_COMPANY"		, "회사코드"		, false);
			grd연동.SetCol("CD_PARTNER"		, "매입처"		, false);
			grd연동.컬럼세팅("CD_PARTNER_REL"	, "코드"			, 70	, 정렬.가운데);
			grd연동.컬럼세팅("NM_PARTNER_REL"	, "매입처명"		, 300);
			grd연동.컬럼세팅("FG_PARTNER"		, "거래처구분"	, 120);
			grd연동.컬럼세팅("TP_PARTNER"		, "정품구분"		, 120);
			grd연동.컬럼세팅("YN_HOLD"		, "홀딩"			, 45	, 정렬.가운데);
			grd연동.컬럼세팅("DC_RMK"			, "비고"			, 180	, true);
			
			grd연동.데이터맵("FG_PARTNER", 코드.코드관리("MA_B000001"));
			grd연동.데이터맵("TP_PARTNER", 코드.코드관리("MA_B000002"));
			
			grd연동.세팅종료("22.04.22.01", false);



			//// ********** 목록
			//grd헤드.세팅시작(1);
			//grd헤드.컬럼세팅("CD_COMPANY"	, "회사코드"		, false);
			//grd헤드.컬럼세팅("NO_SCRAP"	, "폐기번호"		, 100	, 정렬.가운데);
			//grd헤드.컬럼세팅("NM_SCRAP"	, "제목"			, 300);
			//grd헤드.컬럼세팅("ST_STAT"	, "전자결재"		, 60	, 정렬.가운데);
			//grd헤드.컬럼세팅("NO_SLIP"	, "전표번호"		, 120	, 정렬.가운데);
			//grd헤드.컬럼세팅("NO_IO"		, "수불번호"		, 120	, 정렬.가운데);

			//grd헤드.데이터맵("ST_STAT", 코드.코드관리("FI_J000031"));
			//grd헤드.기본키("CD_COMPANY", "NO_SCRAP");
			//grd헤드.세팅종료("22.01.06.02", false);

			//grd헤드.패널바인딩(lay헤드);
			//grd헤드.상세그리드(grd라인, grd파일);

			//// ********** 라인
			//grd라인.세팅시작(1);
			//grd라인.컬럼세팅("CD_COMPANY"	, "회사코드"		, false);
			//grd라인.컬럼세팅("NO_SCRAP"	, "폐기번호"		, false);
			//grd라인.컬럼세팅("SEQ"		, "순번"			, 45	, "####.##", 정렬.가운데);
			//grd라인.컬럼세팅("CD_ITEM"	, "재고코드"		, 130);
			//grd라인.컬럼세팅("NM_ITEM"	, "재고명"		, 280);
			//grd라인.컬럼세팅("CD_SL"		, "창고"			, 110);
			//grd라인.컬럼세팅("DT_GR"		, "최근입고일자"	, 90	, 포맷.날짜);
			//grd라인.컬럼세팅("QT_INV"		, "창고수량"		, 70	, 포맷.수량);
			//grd라인.컬럼세팅("QT"			, "폐기수량"		, 70	, 포맷.수량);
			//grd라인.컬럼세팅("UM"			, "단가"			, 100	, 포맷.원화단가);
			//grd라인.컬럼세팅("AM"			, "금액"			, 100	, 포맷.원화단가);
			//grd라인.컬럼세팅("DC_REASON"	, "폐기사유"		, 200);

			//grd라인.데이터맵("CD_SL", 코드.창고());
			//grd라인.기본키("CD_COMPANY", "NO_SCRAP", "SEQ");
			//grd라인.세팅종료("21.12.29.01", true);

			//grd라인.에디트컬럼("CD_ITEM", "QT", "DC_REASON");
			//grd라인.합계제외컬럼("SEQ", "UM");
			//grd라인.복사붙여넣기(Grd라인_AfterEdit);

			//// ********** 라인
			//grd파일.세팅시작(1);
			//grd파일.컬럼세팅("CD_COMPANY"	, "회사코드"		, false);
			//grd파일.컬럼세팅("NO_SCRAP"	, "폐기번호"		, false);
			//grd파일.컬럼세팅("SEQ"		, "순번"			, 45	, "####.##", 정렬.가운데);
			//grd파일.컬럼세팅("DC_FILE"	, "파일명"		, 400);
			//grd파일.컬럼세팅("NM_FILE"	, "경로"			, false);
			//grd파일.컬럼세팅("CD_PATH"	, "위치"			, false);
			//grd파일.컬럼세팅("SHORTENER"	, "쇼트너"		, false);

			//grd파일.기본키("CD_COMPANY", "NO_SCRAP", "SEQ");
			//grd파일.세팅종료("21.12.29.02", false);
		}

		#endregion

		#region ==================================================================================================== 이벤트 = EVENT

		private void InitEvent()
		{
			grd헤드.AfterRowChange += Grd헤드_AfterRowChange;

			btn메인키추가.Click += Btn키워드추가_Click;
			btn메인키삭제.Click += Btn키워드삭제_Click;

			btn제외키추가.Click += Btn키워드추가_Click;
			btn제외키삭제.Click += Btn키워드삭제_Click;

			btn서브키1추가.Click += Btn키워드추가_Click;
			btn서브키1삭제.Click += Btn키워드삭제_Click;

			btn서브키2추가.Click += Btn키워드추가_Click;
			btn서브키2삭제.Click += Btn키워드삭제_Click;

			//btn입찰추가.Click += Btn키워드추가_Click;
			//btn입찰삭제.Click += Btn키워드삭제_Click;
		}

		private void Btn키워드추가_Click(object sender, EventArgs e)
		{
			//if (fgd헤드["SEQ_PARTNER"] == DBNull.Value)
			//{
			//	ShowMessage("추가중인 매입처는 키워드 추가/삭제를 할 수 없습니다.\n저장 및 조회 후 시도 바랍니다");
			//	return;
			//}

			FlexGrid 그리드 = (FlexGrid)Controls.Find("grd" + ((Control)sender).태그(), true)[0];
			
			그리드.행추가();
			그리드["CD_COMPANY"] = 상수.회사코드;
			그리드["CD_PARTNER"] = grd헤드["CD_PARTNER"];
			그리드["SEQ"]		= (int)그리드.Aggregate(AggregateEnum.Max, "SEQ") + 1;
			그리드["CD_TYPE"]	= 그리드.태그();
			그리드.행추가완료();
		}

		private void Btn키워드삭제_Click(object sender, EventArgs e)
		{
			//string name = "fgd" + ((RoundedButton)sender).Name.Replace("btn", "").Replace("추가", "").Replace("삭제", "");
			//FlexGrid flexGrid = (FlexGrid)Controls.Find(name, true)[0];

			//if (flexGrid.HasNormalRow)
			//	flexGrid.Rows.Remove(flexGrid.Row);
		}

		#endregion

		#region ==================================================================================================== 조회 = SEARCH

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			base.OnToolBarSearchButtonClicked(sender, e);

			try
			{
				디비 db = new 디비("PS_CZ_DX_VENDOR_REG_H");
				db.변수.추가("@CD_COMPANY"	, 상수.회사코드);
				db.변수.추가("@PARTNER_TXT"	, tbx매입처.Text);
				//db.변수.추가("@" + cbo키워드.GetValue(), tbx키워드.Text);

				DataTable dt = db.결과();
				grd헤드.바인딩(dt);
			}
			catch (Exception ex)
			{
				유틸.메세지(ex);
			}
		}

		private void Grd헤드_AfterRowChange(object sender, RangeEventArgs e)
		{
			//if (fgd헤드["CD_PARTNER"].ToString() == "")
			//	return;

			//string partnerCode = grd헤드["CD_PARTNER"].ToString();
			//int partnerSeq = grd헤드["SEQ_PARTNER"].정수();

			//string filter = "CD_PARTNER = '" + partnerCode + "' AND SEQ_PARTNER = " + partnerSeq;
			//DataTable dt = null;

			//if (grd헤드.DetailQueryNeed)
			//{
			//	DataSet ds = 디비.결과s("PS_CZ_DX_VENDOR_REG_L", 상수.회사코드, partnerCode, partnerSeq);
			//	grd메인키.BindingAdd(ds.Tables[0], filter);
			//	grd제외키.BindingAdd(ds.Tables[1], filter);
			//	grd서브키1.BindingAdd(ds.Tables[2], filter);
			//	grd서브키2.BindingAdd(ds.Tables[3], filter);
			//	grd연동.BindingAdd(ds.Tables[4], filter);
			//}
			//else
			//{
			//	grd메인키.BindingAdd(null, filter);
			//	grd제외키.BindingAdd(null, filter);
			//	grd서브키1.BindingAdd(null, filter);
			//	grd서브키2.BindingAdd(null, filter);
			//	grd연동.BindingAdd(null, filter);
			//}

			//fgd메인키.BindingAdd(dt, "");


			string 필터 = grd헤드.상세그리드필터();

			if (grd헤드.상세그리드쿼리())
			{
				DataSet ds = 디비.결과s("PS_CZ_DX_VENDOR_REG_L", 상수.회사코드, grd헤드["CD_PARTNER"]);
				grd메인키.바인딩(ds.Tables[0], 필터);
				grd제외키.바인딩(ds.Tables[1], 필터);
				grd서브키1.바인딩(ds.Tables[2], 필터);
				grd서브키2.바인딩(ds.Tables[3], 필터);
				grd연동.바인딩(ds.Tables[4], 필터);
			}
			else
			{
				grd메인키.바인딩(null, 필터);
				grd제외키.바인딩(null, 필터);
				grd서브키1.바인딩(null, 필터);
				grd서브키2.바인딩(null, 필터);
				grd연동.바인딩(null, 필터);
			}

			//DataTable dtLine = grd헤드.상세그리드쿼리() ? TSQL.결과("PS_CZ_MM_SCRAP_L", 회사코드, 폐기번호) : null;
			//DataTable dtFile = grd헤드.상세그리드쿼리() ? TSQL.결과("PS_CZ_MM_SCRAP_F", 회사코드, 폐기번호) : null;

			//grd메인키.바인딩(dtLine, grd헤드.상세그리드필터());
			//grd제외키.바인딩(dtFile, grd헤드.상세그리드필터());
		}

		private void Btn트레이닝_Click(object sender, EventArgs e)
		{
			//if (cbm매입처.QueryWhereIn_Pipe == "")
			//{
			//	if (ShowMessage("매입처를 지정하지 않을 경우 많은 시간이 소요됩니다.\n진행하시겠습니까?", "QY2") != DialogResult.Yes)
			//		return;
			//}

			//SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;
			////MsgControl.ShowMsg(DD("조회중입니다."));
			//Util.ShowProgress(DD("조회중입니다."));

			//// 키워드에 의해 나온 결과
			//SQL sql = new SQL("PS_CZ_DXVENDOR_REG_TRAINING", SQLType.Procedure, sqlDebug);
			//sql.Parameter.Add2("@CD_COMPANY"	, _companyCode);
			//sql.Parameter.Add2("@CD_PARTNER"	, fgd헤드["CD_PARTNER"]);
			//sql.Parameter.Add2("@SEQ_PARTNER"	, fgd헤드["SEQ_PARTNER"]);
			//sql.Parameter.Add2("@DT_F"			, dtp접수일자.StartDateToString);
			//sql.Parameter.Add2("@DT_T"			, dtp접수일자.EndDateToString);
			//sql.Parameter.Add2("@DT_CHK"		, chk접수일자.Checked ? "Y" : "");
			//sql.Parameter.Add2("@CD_VENDOR"		, cbm매입처.QueryWhereIn_Pipe);
			//sql.Parameter.Add2("@CD_TRAINING"	, rdo메인키.Checked ? "MAIN" : "SUB");

			//DataTable dt = sql.GetDataTable();

			//// OK 여부
			//foreach (DataRow row in dt.Rows)
			//{
			//	if (row["LN_VENDOR_ALL"].ToString().IndexOf((string)fgd헤드["LN_PARTNER"]) < 0)
			//		row["YN_OK"] = "X";
			//}

			//fgd트레이닝1.Binding = dt;

			//// 해당 매입처이지만 키워드로 검색 안되는 리스트 (원인 분석용)
			//if (cbm매입처.QueryWhereIn_Pipe != "")
			//{
			//	SQL sqlChk = new SQL("PS_CZ_DXVENDOR_REG_TRAINING_CHK", SQLType.Procedure, sqlDebug);
			//	sqlChk.Parameter.Add2("@CD_COMPANY"	, _companyCode);
			//	sqlChk.Parameter.Add2("@XML"		, GetTo.Xml(dt, "", "CD_COMPANY", "NO_FILE", "NO_LINE"));
			//	sqlChk.Parameter.Add2("@DT_F"		, dtp접수일자.StartDateToString);
			//	sqlChk.Parameter.Add2("@DT_T"		, dtp접수일자.EndDateToString);
			//	sqlChk.Parameter.Add2("@DT_CHK"		, chk접수일자.Checked ? "Y" : "");
			//	sqlChk.Parameter.Add2("@CD_VENDOR"	, cbm매입처.QueryWhereIn_Pipe);

			//	DataTable dtChk = sqlChk.GetDataTable();
			//	fgd트레이닝2.Binding = dtChk;
			//}

			//Util.CloseProgress();
		}

		private void Btn테스트_Click(object sender, EventArgs e)
		{
			//SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;
			//Util.ShowProgress(DD("조회중입니다."));

			//if (tbx파일번호2.Text == "")
			//	return;

			//SQL sql = new SQL("PS_CZ_DXVENDOR_REG_TEST", SQLType.Procedure, sqlDebug);			
			//sql.Parameter.Add2("@CD_COMPANY", _companyCode);
			//sql.Parameter.Add2("@DT_F"		, dtp접수일자2.StartDateToString);
			//sql.Parameter.Add2("@DT_T"		, dtp접수일자2.EndDateToString);
			//sql.Parameter.Add2("@DT_CHK"	, chk접수일자2.Checked ? "Y" : "");
			//sql.Parameter.Add2("@CD_VENDOR"	, cbm매입처2.QueryWhereIn_Pipe);
			//sql.Parameter.Add2("@NO_FILE"	, tbx파일번호2.Text);

			//DataTable dt = sql.GetDataTable();
			//fgd테스트.Binding = dt;

			//Util.CloseProgress();
		}



		#endregion


		#region ==================================================================================================== 추가 = ADD

		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
			base.OnToolBarAddButtonClicked(sender, e);

			// 추가
			grd헤드.행추가();
			grd헤드["CD_COMPANY"] = 상수.회사코드;
			grd헤드.행추가완료();
		}

		#endregion
	}
}


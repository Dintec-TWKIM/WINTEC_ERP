using System;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using DX;

namespace cz
{
	public partial class H_CZ_VENDOR_SEL : Duzon.Common.Forms.CommonDialog
	{

		public DataTable 결과 => tab메인.SelectedTab.컨트롤<FlexGrid>()[1].데이터테이블_화면();

		public H_CZ_VENDOR_SEL()
		{
			InitializeComponent();
		}

		#region ==================================================================================================== 초기화 == INIT

		protected override void InitLoad()
		{
			this.페이지초기화();

			tbx검색.엔터검색(btn조회);
			tab메인.SelectedIndex = 설정.가져오기("TAB_SEQ").정수();

			spc개별.SplitterDistance = spc개별.Height / 2;
			spc그룹.SplitterDistance = spc개별.Height / 2;
			spc연관.SplitterDistance = spc개별.Height / 2;

			InitGrid();
			InitEvent();
		}

		protected override void InitPaint()
		{
			tbx검색.Focus();
		}

		protected override void OnClosed(EventArgs e)
		{
			설정.내보내기("TAB_SEQ", tab메인.SelectedIndex);
			base.OnClosed(e);
		}

		#endregion

		#region ==================================================================================================== 그리드 == RMFLEM == GRID == ㅎ걍

		private void InitGrid()
		{			
			// ********** 개별
			// 조회
			grd개별_조회.세팅시작(1);

			grd개별_조회.컬럼세팅("CD_PARTNER"		, "코드"			, 60	, 정렬.가운데);
			grd개별_조회.컬럼세팅("LN_PARTNER"		, "거래처명"		, 300);
			grd개별_조회.컬럼세팅("NM_CEO"			, "대표"			, 80	, 정렬.가운데);
			grd개별_조회.컬럼세팅("NO_COMPANY"		, "사업자번호"	, 100	, 정렬.가운데);
			grd개별_조회.컬럼세팅("CD_AREA"		, "지역구분"		, false);
			grd개별_조회.컬럼세팅("CD_PINQ"		, "INQ발신"		, false);
			grd개별_조회.컬럼세팅("CD_PRINT"		, "인쇄형태"		, false);
			grd개별_조회.컬럼세팅("SN_PARTNER"		, "거래처명(약어)", false);
			
			grd개별_조회.기본키("CD_PARTNER");
			grd개별_조회.세팅종료("22.12.06.02", false, false);

			// 선택
			grd개별_선택.세팅복사(grd개별_조회, true);

			// ********** 그룹
			// 조회
			grd그룹_조회.세팅시작(1);

			grd그룹_조회.컬럼세팅("CD_GROUP"		, "코드"		, 60	, 정렬.가운데);
			grd그룹_조회.컬럼세팅("NM_GROUP"		, "그룹"		, 200);

			grd그룹_조회.기본키("CD_GROUP");
			grd그룹_조회.상세그리드(grd그룹_선택);
			grd그룹_조회.세팅종료("22.12.06.03", false, false);

			// 선택
			grd그룹_선택.세팅복사(grd개별_조회);

			// ********** 관련
			// 조회
			grd관련_조회.세팅복사(grd개별_조회);
			grd관련_조회.상세그리드(grd관련_선택);

			// 선택
			grd관련_선택.세팅복사(grd개별_조회);
		}

		#endregion
		
		#region ==================================================================================================== 이벤트 == EVENT == ㄷㅍ둣

		private void InitEvent()
		{
			btn확인.Click += Btn확인_Click;
			btn취소.Click += Btn취소_Click;
			
			btn조회.Click += Btn조회_Click;

			// 개별 탭
			btn개별_추가.Click += Btn개별_추가_Click;
			btn개별_삭제.Click += Btn개별_삭제_Click;
			grd개별_조회.DoubleClick += Grd개별_조회_DoubleClick;
			grd개별_선택.DoubleClick += Grd개별_선택_DoubleClick;

			// 그룹, 관련 탭
			grd그룹_조회.AfterRowChange += Grd그룹_조회_AfterRowChange;
			grd관련_조회.AfterRowChange += Grd관련_조회_AfterRowChange;


			grd그룹_조회.DoubleClick += Grd그룹_조회_DoubleClick;
		}

		
		private void Btn확인_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}

		private void Btn취소_Click(object sender, EventArgs e)
		{
			Close();
		}



		private void Btn개별_추가_Click(object sender, EventArgs e)
		{
			if (!grd개별_조회.HasNormalRow)
				return;

			// 중복 체크
			if (grd개별_선택.데이터테이블("CD_PARTNER = '" + grd개별_조회["CD_PARTNER"] + "'").존재())
			{
				메시지.경고알람("중복");
				return;
			}

			// 추가
			grd개별_선택.행추가();

			for (int i = 0; i < grd개별_선택.Cols.Count; i++)
				grd개별_선택[grd개별_선택.컬럼이름(i)] = grd개별_조회[grd개별_선택.컬럼이름(i)];

			grd개별_선택.행추가완료();			
		}

		private void Btn개별_삭제_Click(object sender, EventArgs e)
		{
			grd개별_선택.Rows.Remove(grd개별_선택.Row);
		}

		private void Grd개별_조회_DoubleClick(object sender, EventArgs e)
		{
			FlexGrid 그리드 = (FlexGrid)sender;
			if (!그리드.HasNormalRow || 그리드.MouseCol <= 0 || 그리드.MouseRow <= 0)
				return;

			Btn개별_추가_Click(null, null);
		}

		private void Grd개별_선택_DoubleClick(object sender, EventArgs e)
		{
			FlexGrid 그리드 = (FlexGrid)sender;
			if (!그리드.HasNormalRow || 그리드.MouseCol <= 0 || 그리드.MouseRow <= 0)
				return;

			Btn개별_삭제_Click(null, null);
		}


		private void Grd그룹_조회_DoubleClick(object sender, EventArgs e) => Btn확인_Click(null, null);


		#endregion

		#region ==================================================================================================== 조회 == SEARCH == ㄴㄷㅁㄱ초

		private void Btn조회_Click(object sender, EventArgs e)
		{
			if (tab메인.SelectedTab == tab개별)
			{
				DataTable dt = 디비.결과("PS_CZ_MA_PARTNER", 상수.회사코드, tbx검색.Text);
				grd개별_조회.바인딩(dt);
			}
			else if (tab메인.SelectedTab == tab그룹)
			{
				DataTable dt = 디비.결과("PS_CZ_DX_VENDOR_GROUP", 상수.회사코드, tbx검색.Text);
				grd그룹_조회.바인딩(dt);
			}
			else if (tab메인.SelectedTab == tab관련)
			{
				DataTable dt = 디비.결과("PS_CZ_MA_PARTNER", 상수.회사코드, tbx검색.Text);
				grd관련_조회.바인딩(dt);
			}
		}

		private void Grd그룹_조회_AfterRowChange(object sender, RangeEventArgs e)
		{
			string 필터 = grd그룹_조회.상세그리드필터();

			if (grd그룹_조회.상세그리드쿼리())
			{
				디비 db = new 디비("PS_CZ_MA_PARTNER");
				db.변수.추가("@CD_COMPANY"	, 상수.회사코드);
				db.변수.추가("@CD_GROUP"		, grd그룹_조회["CD_GROUP"]);
				DataTable dt = db.결과();

				dt.컬럼추가("CD_GROUP", grd그룹_조회["CD_GROUP"].문자());	// 쿼리에 그룹코드가 없으므로 추가해줌
				grd그룹_선택.바인딩(dt, 필터);
			}
			else
			{
				grd그룹_선택.바인딩(null, 필터);
			}
		}

		private void Grd관련_조회_AfterRowChange(object sender, RangeEventArgs e)
		{
			string 필터 = "CD_PARTNER_GROUP = '" + grd관련_조회["CD_PARTNER"] + "'";

			if (grd관련_조회.상세그리드쿼리())
			{
				디비 db = new 디비("PS_CZ_MA_PARTNER");
				db.변수.추가("@CD_COMPANY"		, 상수.회사코드);
				db.변수.추가("@CD_PARTNER_GROUP"	, grd관련_조회["CD_PARTNER"]);
				DataTable dt = db.결과();
				
				dt.컬럼추가("CD_PARTNER_GROUP", grd관련_조회["CD_PARTNER"].문자()); // 거래처코드를 구분으로 해줌
				grd관련_선택.바인딩(dt, 필터);
			}
			else
			{
				grd관련_선택.바인딩(null, 필터);
			}
		}

		#endregion

		

	}
}

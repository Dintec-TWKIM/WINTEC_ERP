using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;

using Dintec;
using DX;

namespace cz
{
	public partial class P_CZ_HR_WORKTIME_RPT : PageBase
	{	
		private string 사원번호
		{
			get
			{
				if 		(tab메인.SelectedTab == tab사원별근태) return grd사원별근태헤드["NO_EMP"].문자();
				else if (tab메인.SelectedTab == tab일자별근태) return grd일자별근태헤드["NO_EMP"].문자();
				else if (tab메인.SelectedTab == tab시간관리)	return grd시간관리헤드["NO_EMP"].문자();

				return "";
			}
		}

		private string 부서
		{
			get
			{
				return grd일자별근태헤드["CD_DEPT"].문자();
			}
		}

		private string 팀
		{
			get
			{
				return grd일자별근태헤드["CD_TEAM"].문자();
			}
		}

		private string 근무년월
		{
			get
			{
				if (tab메인.SelectedTab == tab사원별근태)			return grd사원별근태헤드["DT_MONTH"].문자();
				else if (tab메인.SelectedTab == tab시간관리)		return grd시간관리헤드["DT_MONTH"].문자();
				//else if (tab메인.SelectedTab == tab추가연장수당)	return grd추가연장수당헤드["DT_MONTH"].문자();
				else return "";
			}
		}

		private string 근무일자
		{
			get
			{
				return grd일자별근태헤드["DT_WORK"].문자();
			}
		}


		#region ==================================================================================================== Constructor

		public P_CZ_HR_WORKTIME_RPT()
		{			
			InitializeComponent();
			this.SetConDefault();
		}

		#endregion

		#region ==================================================================================================== Initialize
		protected override void InitLoad()
		{
			this.페이지초기화();

			dtp근무년월.Text = 유틸.오늘(-1);

			// 콤보
			DataTable dtCode = new DataTable();
			dtCode.Columns.Add("TYPE");
			dtCode.Columns.Add("CODE");
			dtCode.Columns.Add("NAME");
			dtCode.Rows.Add("CD_CHECK", "ERP", DD("내근직"));
			dtCode.Rows.Add("CD_CHECK", "MOB", DD("외근직"));

			cbo구분.바인딩(dtCode.선택("TYPE = 'CD_CHECK'"), true);

			// 권한 설정
			string query = @"
SELECT
	A.CD_DUTY_RESP
,	A.NO_EMP
,	A.NM_KOR	AS NM_EMP
,	A.CD_DEPT
,	B.NM_DEPT
,	A.CD_CC		AS CD_TEAM
,	C.NM_CC		AS NM_TEAM
,	ISNULL(D.CD_FLAG1, 9)	AS DUTY_RANK
FROM	  MA_EMP			AS A WITH(NOLOCK)
LEFT JOIN MA_DEPT			AS B WITH(NOLOCK) ON A.CD_COMPANY = B.CD_COMPANY AND A.CD_DEPT = B.CD_DEPT
LEFT JOIN MA_CC				AS C WITH(NOLOCK) ON A.CD_COMPANY = C.CD_COMPANY AND A.CD_CC = C.CD_CC
LEFT JOIN V_CZ_MA_CODEDTL	AS D WITH(NOLOCK) ON D.CD_COMPANY = 'K100' AND D.CD_FIELD = 'HR_H000052' AND A.CD_DUTY_RESP = D.CD_SYSDEF
WHERE 1 = 1
	AND A.CD_COMPANY = '" + 상수.회사코드 + @"'
	AND A.NO_EMP = '" + 상수.사원번호 + "'";

			DataTable dt = TSQL.결과(query);
			int dutyRank = dt.Rows[0]["DUTY_RANK"].정수();

			// 임시 권한 부여 (김태준)
			if (LoginInfo.UserID.포함("S-421", "S-046", "D-004", "D-004A", "S-576", "S-458", "S-391"))
				dutyRank = 0;   // 관리자

			if (LoginInfo.GroupID != "ADMIN")
			{
				if (dutyRank >= 3)
				{
					ctx부서.CodeValue = dt.Rows[0]["CD_DEPT"].문자();
					ctx부서.CodeName = dt.Rows[0]["NM_DEPT"].문자();
					ctx부서.사용(false);
				}
				if (dutyRank >= 4)
				{
					ctx팀.CodeValue = dt.Rows[0]["CD_TEAM"].문자();
					ctx팀.CodeName = dt.Rows[0]["NM_TEAM"].문자();
					ctx팀.사용(false);
				}
				if (dutyRank >= 6)
				{
					ctx사원.CodeValue = dt.Rows[0]["NO_EMP"].문자();
					ctx사원.CodeName = dt.Rows[0]["NM_EMP"].문자();
					ctx사원.사용(false);
				}
			}

			InitGrid();
			InitEvent();

			tab메인.TabPages.Remove(tab추가연장수당);

			// 회장님
			if (상수.사원번호 == "S-001")
			{
				cbo구분.값("ERP");
				tab메인.TabPages.Remove(tab시간관리);
			}
		}

		protected override void InitPaint()
		{
			spc시간관리.SplitterDistance = spc시간관리.Width - 1350;
		}

		#endregion

		#region ==================================================================================================== 그리드 == GRID == ㅎ걍

		private void InitGrid()
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("TYPE");
			dt.Columns.Add("CODE");
			dt.Columns.Add("NAME");
			dt.Rows.Add("CD_WORK", "IN" , DD("출근"));
			dt.Rows.Add("CD_WORK", "FLX", DD("기준"));
			dt.Rows.Add("CD_WORK", "OUT", DD("퇴근"));
			dt.Rows.Add("CD_WORK", "OT" , DD("연장"));

			// ********** 사원별근태
			// 헤드
			grd사원별근태헤드.세팅시작(1);

			grd사원별근태헤드.컬럼세팅("CD_COMPANY"	, "회사코드"	, false);
			grd사원별근태헤드.컬럼세팅("NO_EMP"		, "사번"		, 60	, 정렬.가운데);
			grd사원별근태헤드.컬럼세팅("NM_EMP"		, "성명"		, 80	, 정렬.가운데);
			grd사원별근태헤드.컬럼세팅("NM_DEPT"	, "부서"		, 90	, 정렬.가운데);
			grd사원별근태헤드.컬럼세팅("NM_TEAM"	, "팀"		, 100	, 정렬.가운데);
			grd사원별근태헤드.컬럼세팅("DT_MONTH"	, "근무년월"	, false);

			grd사원별근태헤드.Cols["NM_EMP"].굵게();

			grd사원별근태헤드.기본키("CD_COMPANY", "NO_EMP");
			grd사원별근태헤드.세팅종료("22.02.25.01", false);
			grd사원별근태헤드.상세그리드(grd사원별근태라인);

			// 라인
			grd사원별근태라인.세팅시작(2);
					   
			grd사원별근태라인.컬럼세팅("NO_EMP"		, "사번"					, 60	, 정렬.가운데);
			grd사원별근태라인.컬럼세팅("NM_EMP"		, "성명"					, 80	, 정렬.가운데);
			grd사원별근태라인.컬럼세팅("NM_DEPT"	, "부서"					, 90	, 정렬.가운데);
			grd사원별근태라인.컬럼세팅("NM_TEAM"	, "팀"					, 100	, 정렬.가운데);

			grd사원별근태라인.컬럼세팅("DT_WORK2"	, "일자"					, 120	, 정렬.가운데);
			grd사원별근태라인.컬럼세팅("CD_WCODE"	, "근태"					, 100	, 정렬.가운데);
						
			grd사원별근태라인.컬럼세팅("TM_ST_ORG"	, "모바일"	, "출근"		, 100	, 정렬.가운데);
			grd사원별근태라인.컬럼세팅("TM_ED_ORG"	, "모바일"	, "퇴근"		, 100	, 정렬.가운데);
			grd사원별근태라인.컬럼세팅("TM_ST_FLX"	, "유연"		, "출근"		, 100	, 정렬.가운데);
			grd사원별근태라인.컬럼세팅("TM_ED_FLX"	, "유연"		, "퇴근"		, 100	, 정렬.가운데);
			grd사원별근태라인.컬럼세팅("TM_ED_EST"	, "근무"		, "예정"		, 100	, 정렬.가운데);
			grd사원별근태라인.컬럼세팅("TM_WORKING"	, "근무"		, "시간"		, 100	, 정렬.가운데);
			grd사원별근태라인.컬럼세팅("DC_RMK"		, "비고"					, 200	, 정렬.가운데);

			grd사원별근태라인.데이터맵("CD_WCODE", 코드.근태());
			grd사원별근태라인.세팅종료("22.02.25.01", false);


			// ********** 일자별근태
			// 헤드
			grd일자별근태헤드.세팅시작(1);

			grd일자별근태헤드.컬럼세팅("CD_COMPANY"	, "회사코드"	, false);
			grd일자별근태헤드.컬럼세팅("DT_WORK"	, "일자"		, false);
			grd일자별근태헤드.컬럼세팅("DT_WORK2"	, "일자"		, 120	, 정렬.가운데);			
			grd일자별근태헤드.컬럼세팅("DC_TEXT"	, "비고"		, 120);

			grd일자별근태헤드.기본키("CD_COMPANY", "DT_WORK");
			grd일자별근태헤드.세팅종료("22.02.14.05", false);
			grd일자별근태헤드.상세그리드(grd일자별근태라인);

			// 라인
			grd일자별근태라인.세팅복사(grd사원별근태라인);
			grd일자별근태라인.Cols["NM_EMP"].굵게();

			// ********** 52시간관리			
			// 헤드
			grd시간관리헤드.세팅시작(2);

			grd시간관리헤드.컬럼세팅("CD_COMPANY"	, "회사코드"	, false);
			grd시간관리헤드.컬럼세팅("NO_EMP"		, "사번"		, 60	, 정렬.가운데);
			grd시간관리헤드.컬럼세팅("NM_EMP"		, "성명"		, 80	, 정렬.가운데);
			grd시간관리헤드.컬럼세팅("NM_DEPT"		, "부서"		, 90	, 정렬.가운데);
			grd시간관리헤드.컬럼세팅("NM_TEAM"		, "팀"		, 100	, 정렬.가운데);
			
			grd시간관리헤드.컬럼세팅("TM_WORK_1_NW"	, "1주차", "정상"	, 65	, 정렬.가운데);
			grd시간관리헤드.컬럼세팅("TM_WORK_1_OT"	, "1주차", "연장"	, 65	, 정렬.가운데);
			grd시간관리헤드.컬럼세팅("TM_WORK_2_NW"	, "2주차", "정상"	, 65	, 정렬.가운데);
			grd시간관리헤드.컬럼세팅("TM_WORK_2_OT"	, "2주차", "연장"	, 65	, 정렬.가운데);
			grd시간관리헤드.컬럼세팅("TM_WORK_3_NW"	, "3주차", "정상"	, 65	, 정렬.가운데);
			grd시간관리헤드.컬럼세팅("TM_WORK_3_OT"	, "3주차", "연장"	, 65	, 정렬.가운데);
			grd시간관리헤드.컬럼세팅("TM_WORK_4_NW"	, "4주차", "정상"	, 65	, 정렬.가운데);
			grd시간관리헤드.컬럼세팅("TM_WORK_4_OT"	, "4주차", "연장"	, 65	, 정렬.가운데);
			grd시간관리헤드.컬럼세팅("TM_WORK_5_NW"	, "5주차", "정상"	, 65	, 정렬.가운데);
			grd시간관리헤드.컬럼세팅("TM_WORK_5_OT"	, "5주차", "연장"	, 65	, 정렬.가운데);

			grd시간관리헤드.Cols["NM_EMP"].굵게();

			grd시간관리헤드.기본키("CD_COMPANY", "NO_EMP");
			grd시간관리헤드.세팅종료("22.02.25.01", true);
			grd시간관리헤드.상세그리드(grd시간관리주간, grd시간관리일일, grd시간관리시간);

			// 주간
			grd시간관리주간.세팅시작(2);
			   
			grd시간관리주간.컬럼세팅("NM_WEEK"		, "근무주차"			, 80	, 정렬.가운데);
			grd시간관리주간.컬럼세팅("MIN_WORK_NW"	, "정상근무", "분"	, 65	, 정렬.가운데);
			grd시간관리주간.컬럼세팅("TM_WORK_NW"	, "정상근무", "시간"	, 65	, 정렬.가운데);
			grd시간관리주간.컬럼세팅("MIN_WORK_OT"	, "연장근무", "분"	, 65	, 정렬.가운데);
			grd시간관리주간.컬럼세팅("TM_WORK_OT"	, "연장근무", "시간"	, 65	, 정렬.가운데);

			grd시간관리주간.세팅종료("22.02.25.03", true);
			grd시간관리주간.상세그리드(grd시간관리일일);

			// 일일
			grd시간관리일일.세팅시작(2);

			grd시간관리일일.컬럼세팅("NM_WORK2"		, "근무일자"			, 80	, 정렬.가운데);
			grd시간관리일일.컬럼세팅("MIN_WORK_NW"	, "정상"		, "분"	, 65	, 정렬.가운데);
			grd시간관리일일.컬럼세팅("TM_WORK_NW"	, "정상"		, "시간"	, 65	, 정렬.가운데);
			grd시간관리일일.컬럼세팅("MIN_WORK_OT"	, "연장"		, "분"	, 65	, 정렬.가운데);
			grd시간관리일일.컬럼세팅("TM_WORK_OT"	, "연장"		, "시간"	, 65	, 정렬.가운데);
			grd시간관리일일.컬럼세팅("CD_WCODE"		, "근태"				, 70	, 정렬.가운데);

			grd시간관리일일.Cols["MIN_WORK_OT"].글자색(Color.Blue);
			grd시간관리일일.Cols["TM_WORK_OT"].글자색(Color.Blue);

			grd시간관리일일.데이터맵("CD_WCODE", 코드.근태());
			grd시간관리일일.세팅종료("22.02.25.05", true);
			grd시간관리일일.상세그리드(grd시간관리시간);

			// 시간 그리드
			grd시간관리시간.세팅시작(2);

			grd시간관리시간.컬럼세팅("TM_ST"		, "시작시간"			, 85	, "tt hh:mm", 정렬.가운데);
			grd시간관리시간.컬럼세팅("TM_ED"		, "종료시간"			, 85	, "tt hh:mm", 정렬.가운데);
			grd시간관리시간.컬럼세팅("MIN_WORK"		, "근무시간"	, "분"	, 65	, 정렬.가운데);
			grd시간관리시간.컬럼세팅("TM_WORK"		, "근무시간"	, "시간"	, 65	, 정렬.가운데);
			grd시간관리시간.컬럼세팅("CD_WORK"		, "구분"				, 60	, 정렬.가운데);

			grd시간관리시간.데이터맵("CD_WORK", dt.선택("TYPE = 'CD_WORK'"));

			grd시간관리시간.세팅종료("22.02.25.04", true);
		}

		#endregion

		#region ==================================================================================================== 이벤트 = EVENT

		private void InitEvent()
		{
			grd사원별근태헤드.AfterRowChange += Grd사원별근태헤드_AfterRowChange;
			grd일자별근태헤드.AfterRowChange += Grd일자별근태헤드_AfterRowChange;

			grd시간관리헤드.AfterRowChange += Grd시간관리헤드_AfterRowChange;
			grd시간관리주간.AfterRowChange += Grd시간관리주간_AfterRowChange;
			grd시간관리일일.AfterRowChange += Grd시간관리일일_AfterRowChange;

			grd사원별근태라인.DoubleClick += FlexGrid_DoubleClick;
			grd일자별근태라인.DoubleClick += FlexGrid_DoubleClick;

			grd시간관리헤드.DoubleClick += FlexGrid_DoubleClick;
			grd시간관리일일.DoubleClick += FlexGrid_DoubleClick;
			grd시간관리주간.DoubleClick += FlexGrid_DoubleClick;
		}

		
		private void FlexGrid_DoubleClick(object sender, EventArgs e)
		{
			FlexGrid flexGrid = (FlexGrid)sender;

			// 헤더클릭
			if (flexGrid.MouseRow < flexGrid.Rows.Fixed)
			{				
				그리드스타일(flexGrid);
				return;
			}
		}

		#endregion

		#region ==================================================================================================== 조회 = SEARCH

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			base.OnToolBarSearchButtonClicked(sender, e);

			유틸.작업중("조회중입니다.");

			try
			{
				if (tab메인.SelectedTab == tab사원별근태)
				{ 
					TSQL sql = new TSQL("PS_CZ_HR_WORKTIME_RPT_EMP_H");
					sql.변수.추가("@CD_COMPANY"	, 상수.회사코드);
					sql.변수.추가("@CD_DEPT"		, ctx부서.값());
					sql.변수.추가("@CD_TEAM"		, ctx팀.값());
					sql.변수.추가("@NO_EMP"		, ctx사원.값());
					sql.변수.추가("@CD_CHECK"		, cbo구분.값());

					DataTable dt = sql.결과();
					dt.컬럼추가("DT_MONTH", typeof(string), dtp근무년월.Text);

					grd사원별근태헤드.바인딩(dt);
					그리드스타일(grd사원별근태헤드);
				}
				else if (tab메인.SelectedTab == tab일자별근태)
				{
					TSQL sql = new TSQL("PS_CZ_HR_WORKTIME_RPT_DAY_H");
					sql.변수.추가("@CD_COMPANY"	, 상수.회사코드);
					sql.변수.추가("@DT_MONTH"		, dtp근무년월.Text);

					DataTable dt = sql.결과();
					dt.컬럼추가("CD_DEPT"	, typeof(string), ctx부서.값());
					dt.컬럼추가("CD_TEAM"	, typeof(string), ctx팀.값());
					dt.컬럼추가("NO_EMP"	, typeof(string), ctx사원.값());

					grd일자별근태헤드.바인딩(dt);
					그리드스타일(grd일자별근태헤드);
					grd일자별근태헤드.Row = grd일자별근태헤드.Rows.Count - 1;
				}
				else if (tab메인.SelectedTab == tab시간관리)
				{
					TSQL sql = new TSQL("PS_CZ_HR_WORKTIME_RPT_MGT_H");
					sql.변수.추가("@CD_COMPANY"	, 상수.회사코드);
					sql.변수.추가("@DT_MONTH"		, dtp근무년월.Text);
					sql.변수.추가("@CD_DEPT"		, ctx부서.값());
					sql.변수.추가("@CD_TEAM"		, ctx팀.값());
					sql.변수.추가("@NO_EMP"		, ctx사원.값());

					DataTable dt = sql.결과();
					dt.컬럼추가("DT_MONTH", typeof(string), dtp근무년월.Text);

					grd시간관리헤드.바인딩(dt);
					그리드스타일(grd시간관리헤드);
				}

				유틸.작업중();
			}
			catch (Exception ex)
			{
				유틸.메세지(ex);
			}
		}

		private void Grd사원별근태헤드_AfterRowChange(object sender, RangeEventArgs e)
		{
			TSQL sql = new TSQL("PS_CZ_HR_WBARCODE_FLX");
			sql.변수.추가("@CD_COMPANY"	, 상수.회사코드);
			sql.변수.추가("@DT_F"			, 근무년월);
			sql.변수.추가("@NO_EMP"		, 사원번호);

			DataTable dt = grd사원별근태헤드.상세그리드쿼리() ? sql.결과(): null;
			grd사원별근태라인.바인딩(dt, grd사원별근태헤드.상세그리드필터());
			그리드스타일(grd사원별근태라인);
		}

		private void Grd일자별근태헤드_AfterRowChange(object sender, RangeEventArgs e)
		{
			TSQL sql = new TSQL("PS_CZ_HR_WBARCODE_FLX");
			sql.변수.추가("@CD_COMPANY"	, 상수.회사코드);
			sql.변수.추가("@DT_F"			, 근무일자);
			sql.변수.추가("@DT_T"			, 근무일자);
			sql.변수.추가("@CD_DEPT"		, 부서);
			sql.변수.추가("@CD_TEAM"		, 팀);
			sql.변수.추가("@NO_EMP"		, 사원번호);
			sql.변수.추가("@CD_CHECK"		, cbo구분.값());

			DataTable dt = grd일자별근태헤드.상세그리드쿼리() ? sql.결과() : null;
			grd일자별근태라인.바인딩(dt, grd일자별근태헤드.상세그리드필터());
			그리드스타일(grd일자별근태라인);
		}

		private void Grd시간관리헤드_AfterRowChange(object sender, RangeEventArgs e)
		{			
			DataTable dt = null;
			
			if (grd시간관리헤드.상세그리드쿼리())
			{
				TSQL sql = new TSQL("PS_CZ_HR_WORKTIME_MGT_1WEEK");
				sql.변수.추가("@CD_COMPANY"	, 상수.회사코드);
				sql.변수.추가("@DT_MONTH"		, 근무년월);
				sql.변수.추가("@NO_EMP"		, 사원번호);
				dt = sql.결과();
			}

			grd시간관리주간.바인딩(dt, grd시간관리헤드.상세그리드필터());
			그리드스타일(grd시간관리주간);
		}

		private void Grd시간관리주간_AfterRowChange(object sender, RangeEventArgs e)
		{			
			string 사원번호	= grd시간관리주간["NO_EMP"].문자();
			string 시작일	= grd시간관리주간["DT_F"].문자();
			string 종료일	= grd시간관리주간["DT_T"].문자();
			string 필터		= "NO_EMP = '" + 사원번호 + "' AND DT_WORK >= '" + 시작일 + "' AND DT_WORK <= '" + 종료일 + "'";
			DataTable dt	= null;

			if (grd시간관리주간.상세그리드쿼리())
			{
				TSQL sql = new TSQL("PS_CZ_HR_WORKTIME_MGT_2DAY");
				sql.변수.추가("@CD_COMPANY"	, 상수.회사코드);				
				sql.변수.추가("@DT_F"			, 시작일);
				sql.변수.추가("@DT_T"			, 종료일);
				sql.변수.추가("@NO_EMP"		, 사원번호);
				dt = sql.결과();				
			}

			grd시간관리일일.그리기중지();
			grd시간관리일일.바인딩(dt, 필터);
			그리드스타일(grd시간관리일일);

			// 합계행
			//grd시간관리일일.표시데이터테이블()
		}
		private void Grd시간관리일일_AfterRowChange(object sender, RangeEventArgs e)
		{			
			string 사원번호	= grd시간관리일일["NO_EMP"].문자();
			string 시작일	= grd시간관리일일["DT_WORK"].문자();
			string 필터		= "NO_EMP = '" + 사원번호 + "' AND DT_WORK = '" + 시작일 + "'";
			DataTable dt = null;

			if (grd시간관리일일.상세그리드쿼리())
			{
				TSQL sql = new TSQL("PS_CZ_HR_WORKTIME_MGT_3TIME");
				sql.변수.추가("@CD_COMPANY"	, 상수.회사코드);				
				sql.변수.추가("@DT_F"			, 시작일);
				sql.변수.추가("@NO_EMP"		, 사원번호);
				dt = sql.결과();				
			}

			grd시간관리시간.그리기중지();
			grd시간관리시간.바인딩(dt, 필터);
			그리드스타일(grd시간관리시간);
		}

		private void 그리드스타일(FlexGrid 그리드)
		{
			int 주의 = 660;	// 11시간
			int 경고 = 720;	// 12시간

			그리드.그리기중지();

			for (int i = 그리드.Rows.Fixed; i < 그리드.Rows.Count; i++)
			{
				// ********** 날짜 관련
				if (그리드.포함(grd사원별근태라인, grd일자별근태헤드))
				{
					그리드.셀글자색(i, "DT_WORK2", 그리드[i, "CD_WORK"].문자() == "HOLI" ? "RED" : "");
				}

				// ********** 사원별근태라인, 일자별근태라인
				if (그리드.포함(grd사원별근태라인, grd일자별근태라인))
				{
					그리드.셀글자색(i, "TM_ED_EST", "");
					그리드.셀글자색(i, "TM_WORKING", "");

					if (그리드[i, "CD_WORK"].문자() == "WEEK")
					{
						if (그리드[i, "DT_WORK"].문자() != 유틸.오늘() || 그리드[i, "TM_ED_FLX"].문자() != "")
						{
							// 근무예정시간보다 빨리 퇴근
							if (그리드[i, "TM_ED_EST"].문자().삭제(":").정수() > 그리드[i, "TM_ED_FLX"].문자().삭제(":").정수())
								그리드.셀글자색(i, "TM_ED_EST", Color.Red);

							// 4시간, 8시간 이내 근무
							if (그리드[i, "CD_WCODE"].문자().포함("G18", "G19") && 그리드[i, "TM_WORKING"].문자().삭제(":").정수() < 400
								|| 그리드[i, "CD_WCODE"].문자() == "" && 그리드[i, "TM_WORKING"].문자().삭제(":").정수() + (그리드[i, "SHORT_TERM"] + "00").정수() < 800)
								그리드.셀글자색(i, "TM_WORKING", Color.Red);
						}
					}
				}

				// ********** 52시간관리
				if (그리드 == grd시간관리헤드)
				{
					if (그리드[i, "MIN_WORK_1_OT"].정수() <= 주의) 그리드.셀글자색(i, "TM_WORK_1_OT", Color.Blue);
					if (그리드[i, "MIN_WORK_2_OT"].정수() <= 주의) 그리드.셀글자색(i, "TM_WORK_2_OT", Color.Blue);
					if (그리드[i, "MIN_WORK_3_OT"].정수() <= 주의) 그리드.셀글자색(i, "TM_WORK_3_OT", Color.Blue);
					if (그리드[i, "MIN_WORK_4_OT"].정수() <= 주의) 그리드.셀글자색(i, "TM_WORK_4_OT", Color.Blue);
					if (그리드[i, "MIN_WORK_5_OT"].정수() <= 주의) 그리드.셀글자색(i, "TM_WORK_5_OT", Color.Blue);

					if (그리드[i, "MIN_WORK_1_OT"].정수() > 주의) 그리드.셀글자색(i, "TM_WORK_1_OT", Color.OrangeRed);
					if (그리드[i, "MIN_WORK_2_OT"].정수() > 주의) 그리드.셀글자색(i, "TM_WORK_2_OT", Color.OrangeRed);
					if (그리드[i, "MIN_WORK_3_OT"].정수() > 주의) 그리드.셀글자색(i, "TM_WORK_3_OT", Color.OrangeRed);
					if (그리드[i, "MIN_WORK_4_OT"].정수() > 주의) 그리드.셀글자색(i, "TM_WORK_4_OT", Color.OrangeRed);
					if (그리드[i, "MIN_WORK_5_OT"].정수() > 주의) 그리드.셀글자색(i, "TM_WORK_5_OT", Color.OrangeRed);

					if (그리드[i, "MIN_WORK_1_OT"].정수() > 경고) 그리드.셀글자색(i, "TM_WORK_1_OT", Color.Red, true);
					if (그리드[i, "MIN_WORK_2_OT"].정수() > 경고) 그리드.셀글자색(i, "TM_WORK_2_OT", Color.Red, true);
					if (그리드[i, "MIN_WORK_3_OT"].정수() > 경고) 그리드.셀글자색(i, "TM_WORK_3_OT", Color.Red, true);
					if (그리드[i, "MIN_WORK_4_OT"].정수() > 경고) 그리드.셀글자색(i, "TM_WORK_4_OT", Color.Red, true);
					if (그리드[i, "MIN_WORK_5_OT"].정수() > 경고) 그리드.셀글자색(i, "TM_WORK_5_OT", Color.Red, true);
				}

				if (그리드 == grd시간관리주간)
				{
					if (그리드[i, "MIN_WORK_OT"].정수() <= 주의) 그리드.셀글자색(i, "MIN_WORK_OT", Color.Blue);
					if (그리드[i, "MIN_WORK_OT"].정수() <= 주의) 그리드.셀글자색(i, "TM_WORK_OT", Color.Blue);

					if (그리드[i, "MIN_WORK_OT"].정수() > 주의) 그리드.셀글자색(i, "MIN_WORK_OT", Color.OrangeRed);
					if (그리드[i, "MIN_WORK_OT"].정수() > 주의) 그리드.셀글자색(i, "TM_WORK_OT", Color.OrangeRed);

					if (그리드[i, "MIN_WORK_OT"].정수() > 경고) 그리드.셀글자색(i, "MIN_WORK_OT", Color.Red, true);
					if (그리드[i, "MIN_WORK_OT"].정수() > 경고) 그리드.셀글자색(i, "TM_WORK_OT", Color.Red, true);
				}

				if (그리드 == grd시간관리일일)
				{
					if (그리드[i, "WEEK_DAY"].정수() >= 6)
						그리드.셀글자색(i, "NM_WORK2", Color.Red);
					else
						그리드.셀글자색(i, "NM_WORK2", "");
				}

				if (그리드 == grd시간관리시간)
				{
					if (그리드[i, "CD_WORK"].문자().포함("IN", "FLX", "OUT"))
						그리드.행글자색(i, "#c3c3c3");
					else if (그리드[i, "CD_WORK"].문자() == "OT")
						그리드.행글자색(i, "blue");
					else
						그리드.행글자색(i, "");
				}
			}

			그리드.그리기시작();
		}

		#endregion
	}
}

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
	public partial class P_CZ_HR_WORKTIME_PAY : PageBase
	{
		/*

1. 토요일, 일요일 + 공휴일 구분해서 추가수당 산정
   토요일이 공휴일인 경우?

2. 본사직원, 물류부 각 토요일, 일요일 수당은?

3. 주말근무신청은 토요일하고 일요일 출근하는 경우 시간합산 하는지? 급여 포함 하는지?




토요일:연장수당   ※설날은 연장수당으로
휴일수당


설날,추석 1.5배

		*/
		#region ==================================================================================================== Constructor

		public P_CZ_HR_WORKTIME_PAY()
		{
			InitializeComponent();
		}

		#endregion

		#region ==================================================================================================== Initialize
		protected override void InitLoad()
		{
			dtp근무년월.Text = UT.Today(-30);
//			dtp근무년월.Text = UT.Today(-1);
			grd라인.DetailGrids = new FlexGrid[] { grd주간 };
			grd주간.DetailGrids = new FlexGrid[] { grd일일 };


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
,	D.CD_FLAG1	AS DUTY_RANK
FROM	  MA_EMP			AS A WITH(NOLOCK)
LEFT JOIN MA_DEPT			AS B WITH(NOLOCK) ON A.CD_COMPANY = B.CD_COMPANY AND A.CD_DEPT = B.CD_DEPT
LEFT JOIN MA_CC				AS C WITH(NOLOCK) ON A.CD_COMPANY = C.CD_COMPANY AND A.CD_CC = C.CD_CC
LEFT JOIN V_CZ_MA_CODEDTL	AS D WITH(NOLOCK) ON A.CD_COMPANY = D.CD_COMPANY AND D.CD_FIELD = 'HR_H000052' AND A.CD_DUTY_RESP = D.CD_SYSDEF
WHERE 1 = 1
	AND A.CD_COMPANY = '" + 상수.회사코드 + @"'
	AND A.NO_EMP = '" + 상수.사원번호 + "'";

			DataTable dt = SQL.GetDataTable(query);
			int dutyRank = dt.Rows[0]["DUTY_RANK"].ToInt();

			// 임시 권한 부여 (정기원)
			if (LoginInfo.UserID == "S-209")
				dutyRank = 3;

			// 임시 권한 부여 (김태준)
			if (LoginInfo.UserID.포함("S-421", "S-046", "D-004", "D-004A"))
				dutyRank = 0;	// 관리자

			if (LoginInfo.GroupID != "ADMIN")
			{
				if (dutyRank >= 3)
				{
					ctx부서.CodeValue = dt.Rows[0]["CD_DEPT"].ToString();
					ctx부서.CodeName = dt.Rows[0]["NM_DEPT"].ToString();
					ctx부서.SetEdit(false);
				}
				if (dutyRank >= 4)
				{
					ctx팀.CodeValue = dt.Rows[0]["CD_TEAM"].ToString();
					ctx팀.CodeName = dt.Rows[0]["NM_TEAM"].ToString();
					ctx팀.SetEdit(false);
				}
				if (dutyRank >= 6)
				{
					ctx사원.CodeValue = dt.Rows[0]["NO_EMP"].ToString();
					ctx사원.CodeName = dt.Rows[0]["NM_EMP"].ToString();
					ctx사원.SetEdit(false);
				}
			}

			InitGrid();
			InitEvent();
		}

		protected override void InitPaint()
		{
			spc메인.SplitterDistance = spc메인.Width - 1600;
			//tbx테스트.Left = 1920;
		}

		#endregion

		#region ==================================================================================================== Grid

		private void InitGrid()
		{			
			// ********** 라인 그리드
			grd라인.BeginSetting(2, 1, false);

			grd라인.SetCol("NO_EMP"			, "사번"		, 60	, TextAlignEnum.CenterCenter);
			grd라인.SetCol("NM_EMP"			, "성명"		, 80	, TextAlignEnum.CenterCenter);
			grd라인.SetCol("NM_DEPT"			, "부서"		, 90	, TextAlignEnum.CenterCenter);
			grd라인.SetCol("NM_TEAM"			, "팀"		, 100	, TextAlignEnum.CenterCenter);

			grd라인.컬럼세팅("WORK_OT_1_5_WEEK"	, "연장근무"	, "근무"	, 70, 정렬.가운데);
			grd라인.컬럼세팅("APPR_OT_1_5_WEEK"	, "연장근무"	, "수당"	, 70, 정렬.가운데);
			grd라인.컬럼세팅("PAYF_OT_1_5_WEEK"	, "연장근무"	, "금액"	, 75, 포맷.원화단가);

			grd라인.컬럼세팅("WORK_OT_0_5_WEHO"	, "야간근무"	, "근무"	, 70, 정렬.가운데);
			grd라인.컬럼세팅("APPR_OT_0_5_WEHO"	, "야간근무"	, "수당"	, 70, 정렬.가운데);
			grd라인.컬럼세팅("PAYF_OT_0_5_WEHO"	, "야간근무"	, "금액"	, 75, 포맷.원화단가);

			grd라인.컬럼세팅("WORK_OT_1_5_HOLI"	, "휴일근무"	, "근무"	, 70, 정렬.가운데);
			grd라인.컬럼세팅("APPR_OT_1_5_HOLI"	, "휴일근무"	, "수당"	, 70, 정렬.가운데);
			grd라인.컬럼세팅("PAYF_OT_1_5_HOLI"	, "휴일근무"	, "금액"	, 75, 포맷.원화단가);
			
			grd라인.컬럼세팅("최종_합계금액"		, "합계\n금액"		, 75, 포맷.원화단가);

			grd라인.Cols["NM_EMP"].SetBold(true);
			
			grd라인.SetDefault("22.01.21.01", SumPositionEnum.Top);
			grd라인.SetAlternateRow();
			grd라인.SetMalgunGothic();
			
			// ********** 주간 그리드
			grd주간.BeginSetting(2, 1, false);
			   
			grd주간.SetCol("DT_MONTH"		, "근무일자"	, "월"		, 60	, TextAlignEnum.CenterCenter);
			grd주간.SetCol("NM_WEEK"			, "근무일자"	, "주차"		, 60	, TextAlignEnum.CenterCenter);
			grd주간.SetCol("DT_F"			, "근무일자"	, "시작일"	, 80	, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			grd주간.SetCol("DT_T"			, "근무일자"	, "종료일"	, 80	, typeof(string), FormatTpType.YEAR_MONTH_DAY);

			grd주간.컬럼세팅("WORK_OT_1_5_WEEK"	, "연장근무"	, "근무"	, 70, 정렬.가운데);
			grd주간.컬럼세팅("APPR_OT_1_5_WEEK"	, "연장근무"	, "수당"	, 70, 정렬.가운데);
			
			grd주간.컬럼세팅("WORK_OT_0_5_WEHO"	, "야간근무"	, "근무"	, 70, 정렬.가운데);
			grd주간.컬럼세팅("APPR_OT_0_5_WEHO"	, "야간근무"	, "수당"	, 70, 정렬.가운데);

			grd주간.컬럼세팅("WORK_OT_1_5_HOLI"	, "휴일근무"	, "근무"	, 70, 정렬.가운데);
			grd주간.컬럼세팅("APPR_OT_1_5_HOLI"	, "휴일근무"	, "수당"	, 70, 정렬.가운데);

			//grd주간.SetCol("WORK_TIME"		, "근무시간"	, "연장"		, 70, TextAlignEnum.CenterCenter);
			//grd주간.SetCol("WORK_TIME_PAY"	, "근무시간"	, "수당"		, 70, TextAlignEnum.CenterCenter);			

			grd주간.SetDefault("22.01.21.01", SumPositionEnum.None);
			grd주간.SetAlternateRow();
			grd주간.SetMalgunGothic();

			// ********** 일일 그리드
			grd일일.BeginSetting(2, 1, false);
			   
			grd일일.SetCol("NM_WORK"			, "근무일자"				, 110	, TextAlignEnum.CenterCenter);
			grd일일.SetCol("YN_GW"			, "결재"					, 50	, TextAlignEnum.CenterCenter);

			//grd일일.SetCol("WORK_TIME"		, "근무시간"	, "연장"		, 70	, TextAlignEnum.CenterCenter);
			//grd일일.SetCol("WORK_TIME_PAY"	, "근무시간"	, "수당"		, 70	, TextAlignEnum.CenterCenter);			

			grd일일.컬럼세팅("WORK_OT_1_5"	, "연장근무"	, "근무"	, 70, 정렬.가운데);
			grd일일.컬럼세팅("APPR_OT_1_5"	, "연장근무"	, "수당"	, 70, 정렬.가운데);

			grd일일.컬럼세팅("WORK_OT_0_5"	, "야간근무"	, "근무"	, 70, 정렬.가운데);
			grd일일.컬럼세팅("APPR_OT_0_5"	, "야간근무"	, "수당"	, 70, 정렬.가운데);
			
			grd일일.SetDefault("22.01.21.01", SumPositionEnum.None);
			grd일일.SetAlternateRow();
			grd일일.SetMalgunGothic();
		}

		#endregion

		#region ==================================================================================================== Event

		private void InitEvent()
		{
			btn급여반영.Click += Btn급여반영_Click;

			grd라인.AfterRowChange += Grd라인_AfterRowChange;
			grd주간.AfterRowChange += Grd주간_AfterRowChange;

			grd라인.DoubleClick += FlexGrid_DoubleClick;
			grd일일.DoubleClick += FlexGrid_DoubleClick;
			grd주간.DoubleClick += FlexGrid_DoubleClick;
		}

		private void Btn급여반영_Click(object sender, EventArgs e)
		{

			if (UT.ShowMsg("해당월 급여를 반영하시겠습니까?", "QY2") == DialogResult.Yes)
			{
				//SQL.ExecuteNonQuery("PX_CZ_HR_WORKTIME_PAY", SQLDebug.Print, grd라인.DataTable.ToXml("NO_EMP", "YM", "PAY_A", "PAY_B"));
				TSQL.실행("PX_CZ_HR_WORKTIME_PAY", grd라인.데이터테이블().Json("NO_EMP", "YM", "PAYF_OT_1_5_WEEK", "PAYF_OT_0_5_WEHO", "PAYF_OT_1_5_HOLI"));

				UT.ShowMsg(공통메세지.자료가정상적으로저장되었습니다);
				
			}
			
		}

		private void FlexGrid_DoubleClick(object sender, EventArgs e)
		{
			FlexGrid flexGrid = (FlexGrid)sender;

			// 헤더클릭
			if (flexGrid.MouseRow < flexGrid.Rows.Fixed)
			{
				SetGridStyle(flexGrid);
				return;
			}
		}

		#endregion

		#region ==================================================================================================== Search

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			base.OnToolBarSearchButtonClicked(sender, e);
			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;

			grd라인.Clear2();
			grd주간.Clear2();
			grd일일.Clear2();

			try
			{
				UT.ShowPgb("조회중입니다.");

				//SQL sql = new SQL("PS_CZ_HR_WORKTIME_PAY_SUM", SQLType.Procedure, sqlDebug);
				//sql.Parameter.Add2("@CD_COMPANY", 상수.회사코드);
				//sql.Parameter.Add2("@DT_F"		, dtp근무년월.Text + "00");
				////sql.Parameter.Add2("@DT_MONTH"	, dtp근무년월.Text);
				//sql.Parameter.Add2("@CD_DEPT"	, ctx부서.CodeValue);
				//sql.Parameter.Add2("@CD_TEAM"	, ctx팀.CodeValue);
				//sql.Parameter.Add2("@NO_EMP"	, ctx사원.CodeValue);
				//DataTable dt = sql.GetDataTable();

				TSQL sql = new TSQL("PS_CZ_HR_WORKTIME_PAY_1SUM");
				sql.변수.추가("@CD_COMPANY", 상수.회사코드);
				sql.변수.추가("@DT_MONTH", dtp근무년월.Text);
				sql.변수.추가("@CD_DEPT", ctx부서.값());
				sql.변수.추가("@CD_TEAM", ctx팀.값());
				sql.변수.추가("@NO_EMP", ctx사원.값());
				DataTable dt = sql.결과();

				grd라인.Binding = dt;
				SetGridStyle(grd라인);
				UT.ClosePgb();
			}
			catch (Exception ex)
			{
				UT.ShowMsg(ex);
			}
		}

		private void Grd라인_AfterRowChange(object sender, RangeEventArgs e)
		{
			//return;
			string empNumber = grd라인["NO_EMP"].ToString();
			string filter = "NO_EMP = '" + empNumber + "'";

			if (grd라인.DetailQueryNeed)
			{				
				//SQL sql = new SQL("PS_CZ_HR_WORKTIME_PAY_WEEK", SQLType.Procedure, SQLDebug.Print);
				//sql.Parameter.Add2("@CD_COMPANY", LoginInfo.CompanyCode);
				//sql.Parameter.Add2("@DT_MONTH"	, dtp근무년월.Text);
				//sql.Parameter.Add2("@NO_EMP"	, empNumber);
				//DataTable dt = sql.GetDataTable();

				TSQL sql = new TSQL("PS_CZ_HR_WORKTIME_PAY_2WEEK");
				sql.변수.추가("@CD_COMPANY"	, 상수.회사코드);
				sql.변수.추가("@DT_MONTH"		, dtp근무년월.Text);
				sql.변수.추가("@NO_EMP"		, empNumber);
				DataTable dt = sql.결과();


				grd주간.BindingAdd(dt, filter);
			}
			else
			{
				grd주간.BindingAdd(null, filter);
			}

			SetGridStyle(grd주간);
		}

		private void Grd주간_AfterRowChange(object sender, RangeEventArgs e)
		{
			//return;
			string empNumber = grd주간["NO_EMP"].ToString();
			string dateFrom = grd주간["DT_F"].ToString();
			string dateTo = grd주간["DT_T"].ToString();
			string filter = "NO_EMP = '" + empNumber + "' AND DT_WORK >= '" + dateFrom + "' AND DT_WORK <= '" + dateTo + "'";

			if (grd주간.DetailQueryNeed)
			{
			//	SQL sql = new SQL("PS_CZ_HR_WORKTIME_PAY_DAY", SQLType.Procedure, SQLDebug.Print);
			//	sql.Parameter.Add2("@CD_COMPANY", LoginInfo.CompanyCode);
			//	sql.Parameter.Add2("@DT_MONTH"	, dtp근무년월.Text);				
			//	sql.Parameter.Add2("@DT_F"		, dateFrom);
			//	sql.Parameter.Add2("@DT_T"		, dateTo);
			//	sql.Parameter.Add2("@NO_EMP"	, empNumber);
			//	DataTable dt = sql.GetDataTable();

				TSQL sql = new TSQL("PS_CZ_HR_WORKTIME_PAY_3DAY");
				sql.변수.추가("@CD_COMPANY"	, 상수.회사코드);
				sql.변수.추가("@DT_F"			, dateFrom);
				sql.변수.추가("@DT_T"			, dateTo);
				sql.변수.추가("@NO_EMP"		, empNumber);
				DataTable dt = sql.결과();


				grd일일.BindingAdd(dt, filter);
			}
			else
			{
				grd일일.BindingAdd(null, filter);
			}

			SetGridStyle(grd일일);
		}

		private void SetGridStyle(FlexGrid flexGrid)
		{
			flexGrid.Redraw = false;

			for (int i = flexGrid.Rows.Fixed; i < flexGrid.Rows.Count; i++)
			{
				if (flexGrid.포함(grd라인, grd주간))
				{
					if (flexGrid[i, "WORK_OT_1_5_WEEK"].문자() != flexGrid[i, "APPR_OT_1_5_WEEK"].문자())
						flexGrid.SetCellColor(i, "WORK_OT_1_5_WEEK", Color.Red);
					else
						flexGrid.SetCellColor(i, "WORK_OT_1_5_WEEK", "");

					if (flexGrid[i, "WORK_OT_0_5_WEHO"].문자() != flexGrid[i, "APPR_OT_0_5_WEHO"].문자())
						flexGrid.SetCellColor(i, "WORK_OT_0_5_WEHO", Color.Red);
					else
						flexGrid.SetCellColor(i, "WORK_OT_0_5_WEHO", "");

					if (flexGrid[i, "WORK_OT_1_5_HOLI"].문자() != flexGrid[i, "APPR_OT_1_5_HOLI"].문자())
						flexGrid.SetCellColor(i, "WORK_OT_1_5_HOLI", Color.Red);
					else
						flexGrid.SetCellColor(i, "WORK_OT_1_5_HOLI", "");
				}
				else if (flexGrid == grd일일)
				{
					if (flexGrid[i, "WORK_OT_1_5"].문자() != flexGrid[i, "APPR_OT_1_5"].문자())
						flexGrid.SetCellColor(i, "WORK_OT_1_5", Color.Red);
					else
						flexGrid.SetCellColor(i, "WORK_OT_1_5", "");

					if (flexGrid[i, "WORK_OT_0_5"].문자() != flexGrid[i, "APPR_OT_0_5"].문자())
						flexGrid.SetCellColor(i, "WORK_OT_0_5", Color.Red);
					else
						flexGrid.SetCellColor(i, "WORK_OT_0_5", "");
				}



				// 공통
				//if (flexGrid[i, "WORK_MIN"].ToInt() != flexGrid[i, "WORK_MIN_PAY"].ToInt())
				//	flexGrid.SetCellColor(i, "WORK_TIME", Color.Red);
				//else
				//	flexGrid.SetCellColor(i, "WORK_TIME", "");

					// 개별
					//if (flexGrid == grd일일)
					//{
					//	if (flexGrid[i, "WEEK_DAY"].ToInt() >= 6)
					//		flexGrid.SetCellColor(i, "NM_WORK", Color.Red);
					//	else
					//		flexGrid.SetCellColor(i, "NM_WORK", "");
					//}
			}

			flexGrid.Redraw = true;
		}

		#endregion
	}
}

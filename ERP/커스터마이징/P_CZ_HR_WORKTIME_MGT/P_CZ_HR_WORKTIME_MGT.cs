using System;

using System.Data;

using Duzon.Common.Forms;

using Dass.FlexGrid;
using C1.Win.C1FlexGrid;

using System.Windows.Threading;
using System.Diagnostics;
using DX;
using Dintec;

namespace cz
{
	public partial class P_CZ_HR_WORKTIME_MGT : PageBase
	{
		readonly DispatcherTimer timer1;
		readonly AllInputSources lastInput;

		int allowTime;
		bool idle = false;
		bool sleep = true;	// 자고있다고 생각하고 깨워야함 (시작부터)
		DateTime lastTime;
		DateTime autoTime;

		#region ==================================================================================================== Constructor

		public P_CZ_HR_WORKTIME_MGT()
		{
			InitializeComponent();
			this.SetConDefault();

			timer1 = new DispatcherTimer();
			lastInput = new AllInputSources();
		}

		#endregion

		#region ==================================================================================================== Initialize
		protected override void InitLoad()
		{
			label1.Text = "";
			label2.Text = "";
			label7.Text = "";
			label16.Text = "";

			// 레이블 초기화
			lbl입력상태.Text = "";
			lbl입력시간.Text = "";
			lbl유휴시간.Text = "";
			lbl테스트.Text = "";
			ClearStatus();

			// 근태관리하는 사원인지 쿼리
			string query = @"
SELECT * 
FROM MA_EMP
WHERE 1 = 1
	AND CD_COMPANY = '" + LoginInfo.CompanyCode + @"'
	AND NO_EMP = '" + LoginInfo.UserID + @"'
	AND NO_EMP != 'S-343'	-- 난 안뜨게 하자
	AND CD_DUTY_RANK >= '011'	-- 부장";

			DataTable dtEmp = SQL.GetDataTable(query);

			// 첫 ERP인지 체크
			int erpCnt = 0;

			foreach (Process p in Process.GetProcessesByName("NeoWeb"))
			{
				if (!(p.Threads[0].ThreadState == ThreadState.Wait && p.Threads[0].WaitReason == ThreadWaitReason.Suspended))
					erpCnt++;
			}

			if (erpCnt == 1 && dtEmp.Rows.Count == 1)
				GVAR.IsFirstErp = true;

			// ********** 로그찍기
			lbl프로세스.Text = "프로세스:" + erpCnt + "/사원체크:" + dtEmp.Rows.Count + "/실행:" + GVAR.IsFirstErp;

			// 테스트
			//if (LoginInfo.UserID == "S-343")
			//	GVAR.IsFirstErp = true;

			// 첫 ERP가 아니라면 Load 안함 (창닫기가 여기서 안됨)
			if (!GVAR.IsFirstErp || LoginInfo.UserID.In("SYSADMIN"))
			{
				Enabled = false;
				return;				
			}			

			allowTime = 15;	// 분단위 (실제 사용할때는 초단위로 씀, 밑에서 * 60함)

			

			DataTable dt = new DataTable();
			dt.Columns.Add("TM_WORK");
			dt.Columns.Add("FLAG", typeof(string));

			grd로그.Binding = dt;

			//this.ExitButtonClick()
			InitGrid();
			InitEvent();


		}

		protected override void InitPaint()
		{
			// 첫 ERP가 아니라면 그냥 창 닫기
			if (!Enabled)
			{
				UnLoadPage();
				return;

			}
			else
			{

				Search();
				//SearchStatus();
				OnToolBarSearchButtonClicked(null, null);
			}
		}

		private void ClearStatus()
		{
			lbl일일정상근무분.Text = "";
			lbl일일정상근무시간.Text = "";
			lbl일일연장근무분.Text = "";
			lbl일일연장근무시간.Text = "";

			lbl주간정상근무분.Text = "";
			lbl주간정상근무시간.Text = "";
			lbl주간연장근무분.Text = "";
			lbl주간연장근무시간.Text = "";

			lbl월간정상근무분.Text = "";
			lbl월간정상근무시간.Text = "";
			lbl월간연장근무분.Text = "";
			lbl월간연장근무시간.Text = "";
		}

		#endregion

		#region ==================================================================================================== Grid

		private void InitGrid()
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("TYPE");
			dt.Columns.Add("CODE");
			dt.Columns.Add("NAME");
			dt.Rows.Add("FLAG", "S", DD("시작"));
			dt.Rows.Add("FLAG", "E", DD("종료"));

			dt.Rows.Add("WORK_TYPE", "NW", DD("정상"));
			dt.Rows.Add("WORK_TYPE", "OT", DD("연장"));

			// ********** 로그 그리드
			grd로그.BeginSetting(1, 1, false);
			
			grd로그.SetCol("TM_WORK"		, "시간"		, 180	, typeof(DateTime), "tt hh:mm:ss", TextAlignEnum.CenterCenter);
			grd로그.SetCol("FLAG"		, "구분"		, 100	, TextAlignEnum.CenterCenter);
			
			grd로그.SetDataMap("FLAG", dt.Select("TYPE = 'FLAG'").ToDataTable(), "CODE", "NAME");

			grd로그.SetDefault("20.12.28.03", SumPositionEnum.None);
			grd로그.SetAlternateRow();
			grd로그.SetMalgunGothic();

			// ********** 일일 그리드
			grd일일.BeginSetting(2, 1, false);
			   
			grd일일.SetCol("TM_START"	, "시작시간"			, 110	, typeof(DateTime), "tt hh:mm:ss", TextAlignEnum.CenterCenter);
			grd일일.SetCol("TM_CLOSE"	, "종료시간"			, 110	, typeof(DateTime), "tt hh:mm:ss", TextAlignEnum.CenterCenter);
			grd일일.SetCol("WORK_MIN"	, "근무시간"	, "분"	, 100	, TextAlignEnum.CenterCenter);
			grd일일.SetCol("WORK_TIME"	, "근무시간"	, "시간"	, 100	, TextAlignEnum.CenterCenter);
			grd일일.SetCol("WORK_TYPE"	, "구분"				, 100	, TextAlignEnum.CenterCenter);

			grd일일.SetDataMap("WORK_TYPE", dt.Select("TYPE = 'WORK_TYPE'").ToDataTable(), "CODE", "NAME");

			grd일일.SetDefault("20.12.29.04", SumPositionEnum.None);
			grd일일.SetAlternateRow();
			grd일일.SetMalgunGothic();

			// ********** 주간 그리드
			grd주간.BeginSetting(2, 1, false);
			   
			grd주간.SetCol("DT_WORK"			, "근무일자"			, 100	, TextAlignEnum.CenterCenter);
			grd주간.SetCol("WORK_MIN_NW"		, "정상근무", "분"	, 100	, TextAlignEnum.CenterCenter);
			grd주간.SetCol("WORK_TIME_NW"	, "정상근무", "시간"	, 100	, TextAlignEnum.CenterCenter);
			grd주간.SetCol("WORK_MIN_OT"		, "연장근무", "분"	, 100	, TextAlignEnum.CenterCenter);
			grd주간.SetCol("WORK_TIME_OT"	, "연장근무", "시간"	, 100	, TextAlignEnum.CenterCenter);
							
			grd주간.SetDefault("20.12.30.01", SumPositionEnum.None);
			grd주간.SetAlternateRow();
			grd주간.SetMalgunGothic();

			// ********** 월간 그리드
			grd월간.BeginSetting(2, 1, false);
			
			grd월간.SetCol("NM_WEEK"			, "근무주차"			, 100	, TextAlignEnum.CenterCenter);
			grd월간.SetCol("WORK_MIN_NW"		, "정상근무", "분"	, 100	, TextAlignEnum.CenterCenter);
			grd월간.SetCol("WORK_TIME_NW"	, "정상근무", "시간"	, 100	, TextAlignEnum.CenterCenter);
			grd월간.SetCol("WORK_MIN_OT"		, "연장근무", "분"	, 100	, TextAlignEnum.CenterCenter);
			grd월간.SetCol("WORK_TIME_OT"	, "연장근무", "시간"	, 100	, TextAlignEnum.CenterCenter);
				
			grd월간.SetDefault("20.12.30.02", SumPositionEnum.None);
			grd월간.SetAlternateRow();
			grd월간.SetMalgunGothic();

			
		}

		#endregion

		#region ==================================================================================================== Event

		private void InitEvent()
		{
			lay메인.Resize += Lay메인_Resize;

			timer1.Interval = new TimeSpan(0, 0, 1);
			timer1.Tick += Timer1_Tick;
			timer1.Start();
		}

		private void Lay메인_Resize(object sender, EventArgs e)
		{
			lbl입력상태.Width = (pnl근무시간기록.Width - (lbl입력.Width * 2)) / 2 - 30;
			lbl입력시간.Width = lbl입력상태.Width;

			pnl일일정상근무.Width = ((pnl일일근무현황.Width - (lbl일일정상근무.Width * 2)) / 2) - 30;			
			pnl주간정상근무.Width = ((pnl주간근무현황.Width - (lbl주간정상근무.Width * 2)) / 2) - 30;			
			pnl월간정상근무.Width = ((pnl월간근무현황.Width - (lbl월간정상근무.Width * 2)) / 2) - 30;			
		}

		int testCnt = 1;

		DateTime idleTime;

		private void Timer1_Tick(object sender, EventArgs e)
		{
			// 페이지가 없어도 타이머가 한번 동작하면 stop할때까지 돌아가므로 혹시모를 만약을 대비해 현재 페이지가 존재하는지 체크함
			if (!IsExistPage(PageID, false))
				return;

			// Tick대로 동작하는지 테스트
			if (LoginInfo.UserID == "S-343")
			{
				//lbl테스트.Text = testCnt.ToString();
				//testCnt++;
			}

			// 최근 입력시간 가져오기 (근데 PC마다 시간이 다름, 왜그런지 모르겠음)
			DateTime lastTimeNew = lastInput.GetLastInputTime();


			// 비교 (스트링으로 변환 후 비교해야 비교가 됨, datetime으로 비교하면 무조건 다르다고 나옴)
			if (lastTimeNew.ToString("yyyy-MM-dd HH:mm:ss") != lastTime.ToString("yyyy-MM-dd HH:mm:ss"))
			{
				// ********** ACTIVE
				lbl입력상태.Text = "Active";
				lbl유휴시간.Text = "";
				idle = false;

				if (sleep)
				{
					grd로그.Rows.Add();
					grd로그.Row = grd로그.Rows.Count - 1;
					grd로그["TM_WORK"] = DateTime.Now;
					grd로그["FLAG"] = "S";

					sleep = false;
					Save("S", 0);
				}
			}
			else
			{
				// ********** IDLE
				lbl입력상태.Text = "Idle";
				
				// 최초 아이들 돌입이면 아이들시간 찍음
				if (!idle) idleTime = DateTime.Now;
				idle = true;

				// 유휴시간 기록
				TimeSpan duration = DateTime.Now - idleTime;
				lbl유휴시간.Text = duration.ToString(@"hh\:mm\:ss");

				// 계속 아이들 상태일 경우 최초 아이들 돌입 시간부터 현재 시간 차이 구함
				if (duration.TotalSeconds > (allowTime * 60) && !sleep)
				{
					grd로그.Rows.Add();
					grd로그.Row = grd로그.Rows.Count - 1;
					grd로그["TM_WORK"] = idleTime;
					grd로그["FLAG"] = "E";

					sleep = true;
					Save("E", duration.TotalSeconds.ToInt());
				}
			}

			// ********** 슬립이 아닌 경우 시간 관계없이 5분마다 로그 기록 (시간 비교는 분까지만 체크), 예기치 못하게 종료가 기록되지 않는 경우를 방지하기 위함
			if (!sleep)
			{
				if (DateTime.Now.Minute % 5 == 0 && DateTime.Now.ToString("yyyy-MM-dd HH:mm") != autoTime.ToString("yyyy-MM-dd HH:mm"))
				{
					autoTime = DateTime.Now;	// 순서 중요! (DB접속 시간때문에 1초 딜레이 생길수 도 있음)
					Save("W", 0);					
				}
			}

			lastTime = lastTimeNew;
			lbl입력시간.Text = (idle ? idleTime : DateTime.Now).ToString("tt hh:mm:ss");

			// 테스트
			//label1.Text = DateTime.Now.ToString() + " / " + DateTime.Now.ToString("tt hh: mm:ss");
			//label2.Text = lastTime.ToString() + " / " + lastTime.ToString("tt hh: mm:ss");
		}

		#endregion

		#region ==================================================================================================== Search

		public void Search()
		{
			SQL sql = new SQL("PS_CZ_MA_USER_LOG_KM", SQLType.Procedure, SQLDebug.None);
			sql.Parameter.Add2("@CD_COMPANY", LoginInfo.CompanyCode);
			sql.Parameter.Add2("@ID_USER"	, LoginInfo.UserID);
			sql.Parameter.Add2("@DT_WORK"	, DateTime.Now.ToString("yyyyMMdd"));

			DataTable dt = sql.GetDataTable();
			grd로그.Binding = dt;
		}

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			ClearStatus();

			// ********** 일일 근무현황
			SQL sqlDay = new SQL("PS_CZ_HR_WORKTIME_MGT_DAY", SQLType.Procedure, SQLDebug.None);
			sqlDay.Parameter.Add2("@CD_COMPANY"	, LoginInfo.CompanyCode);
			sqlDay.Parameter.Add2("@ID_USER"	, LoginInfo.UserID);
			sqlDay.Parameter.Add2("@DT_WORK"	, DateTime.Now.ToString("yyyyMMdd"));
			DataSet dsDay = sqlDay.GetDataSet();

			DataTable dtDayNW = dsDay.Tables[0].Select("WORK_TYPE = 'NW'").ToDataTable();
			DataTable dtDayOT = dsDay.Tables[0].Select("WORK_TYPE = 'OT'").ToDataTable();

			// 레이블
			if (dtDayNW != null)
			{
				lbl일일정상근무분.Text = dtDayNW.Rows[0]["WORK_MIN"].ToString();
				lbl일일정상근무시간.Text = dtDayNW.Rows[0]["WORK_TIME"].ToString();				
			}

			if (dtDayOT != null)
			{
				lbl일일연장근무분.Text = dtDayOT.Rows[0]["WORK_MIN"].ToString();
				lbl일일연장근무시간.Text = dtDayOT.Rows[0]["WORK_TIME"].ToString();
			}

			// 그리드
			grd일일.Binding = dsDay.Tables[1];

			// ********** 주간 근무현황
			SQL sqlWeek = new SQL("PS_CZ_HR_WORKTIME_MGT_WEEK", SQLType.Procedure, SQLDebug.None);
			sqlWeek.Parameter.Add2("@CD_COMPANY", LoginInfo.CompanyCode);
			sqlWeek.Parameter.Add2("@ID_USER"	, LoginInfo.UserID);
			sqlWeek.Parameter.Add2("@DT_WORK"	, DateTime.Now.ToString("yyyyMMdd"));
			DataSet dsWeek = sqlWeek.GetDataSet();

			DataTable dtWeekNW = dsWeek.Tables[0].Select("WORK_TYPE = 'NW'").ToDataTable();
			DataTable dtWeekOT = dsWeek.Tables[0].Select("WORK_TYPE = 'OT'").ToDataTable();

			// 레이블
			if (dtWeekNW != null)
			{
				lbl주간정상근무분.Text = dtWeekNW.Rows[0]["WORK_MIN"].ToString();
				lbl주간정상근무시간.Text = dtWeekNW.Rows[0]["WORK_TIME"].ToString();
			}

			// 레이블
			if (dtWeekOT != null)
			{
				lbl주간연장근무분.Text = dtWeekOT.Rows[0]["WORK_MIN"].ToString();
				lbl주간연장근무시간.Text = dtWeekOT.Rows[0]["WORK_TIME"].ToString();
			}

			// 그리드
			grd주간.Binding = dsWeek.Tables[1];

			// ********** 월간 근무현황
			SQL sqlMonth = new SQL("PS_CZ_HR_WORKTIME_MGT_MONTH", SQLType.Procedure, SQLDebug.None);
			sqlMonth.Parameter.Add2("@CD_COMPANY", LoginInfo.CompanyCode);
			sqlMonth.Parameter.Add2("@ID_USER"	 , LoginInfo.UserID);
			sqlMonth.Parameter.Add2("@DT_WORK"	 , DateTime.Now.ToString("yyyyMMdd"));
			DataSet dsMonth = sqlMonth.GetDataSet();

			DataTable dtMonthNW = dsMonth.Tables[0].Select("WORK_TYPE = 'NW'").ToDataTable();
			DataTable dtMonthOT = dsMonth.Tables[0].Select("WORK_TYPE = 'OT'").ToDataTable();

			// 레이블
			if (dtMonthNW != null)
			{
				lbl월간정상근무분.Text = dtMonthNW.Rows[0]["WORK_MIN"].ToString();
				lbl월간정상근무시간.Text = dtMonthNW.Rows[0]["WORK_TIME"].ToString();
			}

			// 레이블
			if (dtMonthOT != null)
			{
				lbl월간연장근무분.Text = dtMonthOT.Rows[0]["WORK_MIN"].ToString();
				lbl월간연장근무시간.Text = dtMonthOT.Rows[0]["WORK_TIME"].ToString();
			}

			// 그리드
			grd월간.Binding = dsMonth.Tables[1];
		}

		#endregion

		#region ==================================================================================================== Save		

		/// <summary>
		/// 정해진 "초" 만큼 이전 시간으로 저장
		/// </summary>
		/// <param name="flag"></param>
		/// <param name="secAgo"></param>
		private void Save(string flag, int secAgo)
		{
			if (!GVAR.IsFirstErp)
				return;

			SQL sql = new SQL("PX_CZ_MA_USER_LOG_KM_3", SQLType.Procedure, SQLDebug.None);
			sql.Parameter.Add2("@CD_COMPANY", LoginInfo.CompanyCode);
			sql.Parameter.Add2("@ID_USER"	, LoginInfo.UserID);
			sql.Parameter.Add2("@SEC_AGO"	, secAgo);
			sql.Parameter.Add2("@FLAG"		, flag);
			sql.Parameter.Add2("@NM_HOST"	, LOCAL.GetHostName());
			sql.Parameter.Add2("@ID_PROCESS", Process.GetCurrentProcess().Id);			
			sql.Parameter.Add2("@IP"		, LOCAL.GetIpAddress());
			sql.ExecuteNonQuery();
		}

		#endregion

		public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
		{
			timer1.Stop();
			Save("E", 0);
			return base.OnToolBarExitButtonClicked(sender, e);
		}




		
	}
}

using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.Erpiu.ComponentModel;
using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cz
{
	public partial class P_CZ_SA_WEEKLY_RPT : PageBase
	{
		P_CZ_SA_WEEKLY_RPT_BIZ _biz = new P_CZ_SA_WEEKLY_RPT_BIZ();
		P_CZ_SA_WEEKLY_RPT_GW _gw = new P_CZ_SA_WEEKLY_RPT_GW();
		private bool _isTeamLeader;

		public P_CZ_SA_WEEKLY_RPT()
		{
			InitializeComponent();
		}

		protected override void InitLoad()
		{
			base.InitLoad();

			this.InitGrid();
			this.InitEvent();
		}

		protected override void InitPaint()
		{
			base.InitPaint();

			this.dtp작성일자.StartDateToString = Global.MainFrame.GetDateTimeToday().AddMonths(-1).ToString("yyyyMMdd");
			this.dtp작성일자.EndDateToString = Global.MainFrame.GetStringToday;

			this.dtp견적일자.StartDateToString = Global.MainFrame.GetDateTimeToday().AddDays(-7).ToString("yyyyMMdd");
			this.dtp견적일자.EndDateToString = Global.MainFrame.GetStringToday;

			this.dtp수주일자.StartDateToString = Global.MainFrame.GetDateTimeToday().AddDays(-7).ToString("yyyyMMdd");
			this.dtp수주일자.EndDateToString = Global.MainFrame.GetStringToday;

			string query = @"SELECT ME.CD_DUTY_RESP
FROM MA_EMP ME WITH(NOLOCK)
WHERE ME.CD_COMPANY = '{0}'
AND ME.NO_EMP = '{1}'
AND ME.CD_DUTY_RESP = '400'";

			DataTable dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, Global.MainFrame.LoginInfo.EmployeeNo));

			if (dt != null && dt.Rows.Count > 0)
				this._isTeamLeader = true;
			else
				this._isTeamLeader = false;
			
			if (Global.MainFrame.LoginInfo.UserID == "S-391" ||
				Global.MainFrame.LoginInfo.UserID == "S-347" ||
				Global.MainFrame.LoginInfo.UserID == "S-343" ||
				Global.MainFrame.LoginInfo.UserID == "S-267")
			{

			}
			else if (this._isTeamLeader)
			{
				this.ctx팀.ReadOnly = ReadOnly.TotalReadOnly;
				this.ctx담당자.ReadOnly = ReadOnly.None;

				this.btn확정.Visible = false;
				this.btn확정취소.Visible = false;
				this.btn게시.Visible = true;
				this.btn게시취소.Visible = true;
				this.btn전자결재.Visible = true;
			}
			else
			{
				this.ctx팀.ReadOnly = ReadOnly.TotalReadOnly;
				this.ctx담당자.ReadOnly = ReadOnly.TotalReadOnly;

				this.btn확정.Visible = true;
				this.btn확정취소.Visible = true;
				this.btn게시.Visible = false;
				this.btn게시취소.Visible = false;
				this.btn전자결재.Visible = false;
			}
		}

		private void InitGrid()
		{
			this.MainGrids = new FlexGrid[] { this._flex담당자, this._flex견적L, this._flex수주L, this._flex프로젝트, this._flex클레임L, this._flex미수금L };
			this._flex담당자.DetailGrids = new FlexGrid[] { this._flex견적L, this._flex수주L, this._flex프로젝트, this._flex클레임L, this._flex미수금L };

			#region 담당자
			this._flex담당자.BeginSetting(1, 1, false);

			this._flex담당자.SetCol("S", "선택", 60, true, CheckTypeEnum.Y_N);
			this._flex담당자.SetCol("NM_ST_STAT", "결재상태", 100);
			this._flex담당자.SetCol("YN_POST", "게시여부", 60, false, CheckTypeEnum.Y_N);
			this._flex담당자.SetCol("YN_CONFIRM", "확정여부", 60, false, CheckTypeEnum.Y_N);
			this._flex담당자.SetCol("NO_EMP", "사번", 100);
			this._flex담당자.SetCol("NM_EMP", "이름", 100);
			this._flex담당자.SetCol("NM_CC", "팀", 100);
			this._flex담당자.SetCol("DT_YEAR", "작성년도", 100);
			this._flex담당자.SetCol("QT_WEEK", "작성주차", 100);
			this._flex담당자.SetCol("DT_FROM", "시작일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex담당자.SetCol("DT_TO", "종료일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

			this._flex담당자.AddDummyColumn("S");

			this._flex담당자.SettingVersion = "0.0.0.1";
			this._flex담당자.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion

			#region 견적

			#region Header

			this._flex견적H.BeginSetting(1, 1, false);

			this._flex견적H.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
			this._flex견적H.SetCol("NO_FILE", "파일번호", 100);
			this._flex견적H.SetCol("DT_QTN", "견적일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex견적H.SetCol("LN_PARTNER", "매출처", 100);
			this._flex견적H.SetCol("NM_VESSEL", "호선", 100);
			this._flex견적H.SetCol("NM_SUPPLIER", "매입처", 100);
			this._flex견적H.SetCol("NM_KOR", "담당자", 100);
			this._flex견적H.SetCol("NM_SALEGRP", "영업그룹", 100);
			this._flex견적H.SetCol("NM_PARTNER_GRP", "매출처그룹", 100);
			this._flex견적H.SetCol("QT_ITEM", "종수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex견적H.SetCol("NM_EXCH", "통화명", 100);
			this._flex견적H.SetCol("RT_EXCH", "환율", 100, false, typeof(decimal), FormatTpType.RATE);
			this._flex견적H.SetCol("AM_QTN_EX", "견적금액(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex견적H.SetCol("AM_QTN_S", "견적금액(원화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex견적H.SetCol("AM_PROFIT", "이윤", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex견적H.SetCol("RT_PROFIT", "이윤율(%)", 100, false, typeof(decimal), FormatTpType.RATE);

			this._flex견적H.SetDummyColumn("S");

			this._flex견적H.SettingVersion = "0.0.0.1";
			this._flex견적H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

			#endregion

			#region Line

			this._flex견적L.BeginSetting(1, 1, false);

			this._flex견적L.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
			this._flex견적L.SetCol("NO_KEY", "파일번호", 100);
			this._flex견적L.SetCol("DT_QTN", "견적일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex견적L.SetCol("LN_PARTNER", "매출처", 100);
			this._flex견적L.SetCol("NM_VESSEL", "호선", 100);
			this._flex견적L.SetCol("NM_SUPPLIER", "매입처", 100);
			this._flex견적L.SetCol("NM_KOR", "담당자", 100);
			this._flex견적L.SetCol("NM_SALEGRP", "영업그룹", 100);
			this._flex견적L.SetCol("NM_PARTNER_GRP", "매출처그룹", 100);
			this._flex견적L.SetCol("NM_ITEMGRP", "유형", 100);
			this._flex견적L.SetCol("QT_ITEM", "종수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex견적L.SetCol("NM_EXCH", "통화명", 100);
			this._flex견적L.SetCol("RT_EXCH", "환율", 100, false, typeof(decimal), FormatTpType.RATE);
			this._flex견적L.SetCol("AM_QTN_EX", "견적금액(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex견적L.SetCol("AM_QTN_S", "견적금액(원화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex견적L.SetCol("AM_PROFIT", "이윤", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex견적L.SetCol("RT_PROFIT", "이윤율(%)", 100, false, typeof(decimal), FormatTpType.RATE);
			this._flex견적L.SetCol("DC_RMK", "비고", 100, true);

			this._flex견적L.SetDummyColumn("S");
			this._flex견적L.ExtendLastCol = true;

			this._flex견적L.SettingVersion = "0.0.0.1";
			this._flex견적L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

			#endregion

			#endregion

			#region 수주

			#region Header
			this._flex수주H.BeginSetting(1, 1, false);

			this._flex수주H.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
			this._flex수주H.SetCol("NO_SO", "수주번호", 100);
			this._flex수주H.SetCol("DT_SO", "수주일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex수주H.SetCol("LN_PARTNER", "매출처", 100);
			this._flex수주H.SetCol("NM_VESSEL", "호선", 100);
			this._flex수주H.SetCol("NM_SUPPLIER", "매입처", 100);
			this._flex수주H.SetCol("NM_KOR", "담당자", 100);
			this._flex수주H.SetCol("NM_SALEGRP", "영업그룹", 100);
			this._flex수주H.SetCol("NM_PARTNER_GRP", "매출처그룹", 100);
			this._flex수주H.SetCol("QT_ITEM", "종수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex수주H.SetCol("NM_EXCH", "통화명", 100);
			this._flex수주H.SetCol("RT_EXCH", "환율", 100, false, typeof(decimal), FormatTpType.RATE);
			this._flex수주H.SetCol("AM_SO_EX", "수주금액(외화)", 100, 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex수주H.SetCol("AM_SO_S", "수주금액(원화)", 100, 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex수주H.SetCol("AM_PROFIT", "이윤", 100, 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex수주H.SetCol("RT_PROFIT", "이윤율(%)", 100, false, typeof(decimal), FormatTpType.RATE);

			this._flex수주H.SetDummyColumn("S");

			this._flex수주H.SettingVersion = "0.0.0.1";
			this._flex수주H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion

			#region Line
			this._flex수주L.BeginSetting(1, 1, false);

			this._flex수주L.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
			this._flex수주L.SetCol("NO_KEY", "수주번호", 100);
			this._flex수주L.SetCol("DT_SO", "수주일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex수주L.SetCol("LN_PARTNER", "매출처", 100);
			this._flex수주L.SetCol("NM_VESSEL", "호선", 100);
			this._flex수주L.SetCol("NM_SUPPLIER", "매입처", 100);
			this._flex수주L.SetCol("NM_KOR", "담당자", 100);
			this._flex수주L.SetCol("NM_SALEGRP", "영업그룹", 100);
			this._flex수주L.SetCol("NM_PARTNER_GRP", "매출처그룹", 100);
			this._flex수주L.SetCol("NM_ITEMGRP", "유형", 100);
			this._flex수주L.SetCol("QT_ITEM", "종수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex수주L.SetCol("NM_EXCH", "통화명", 100);
			this._flex수주L.SetCol("RT_EXCH", "환율", 100, false, typeof(decimal), FormatTpType.RATE);
			this._flex수주L.SetCol("AM_SO_EX", "수주금액(외화)", 100, 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex수주L.SetCol("AM_SO_S", "수주금액(원화)", 100, 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex수주L.SetCol("AM_PROFIT", "이윤", 100, 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex수주L.SetCol("RT_PROFIT", "이윤율(%)", 100, false, typeof(decimal), FormatTpType.RATE);
			this._flex수주L.SetCol("DC_RMK", "비고", 100, true);

			this._flex수주L.SetDummyColumn("S");
			this._flex수주L.ExtendLastCol = true;

			this._flex수주L.SettingVersion = "0.0.0.1";
			this._flex수주L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion

			#endregion

			#region 클레임

			#region Header
			this._flex클레임H.BeginSetting(1, 1, false);

			this._flex클레임H.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
			this._flex클레임H.SetCol("NO_CLAIM", "클레임번호", 100);
			this._flex클레임H.SetCol("NO_SO", "수주번호", 100);
			this._flex클레임H.SetCol("DT_INPUT", "작성일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex클레임H.SetCol("NM_PARTNER", "매출처", 100);
			this._flex클레임H.SetCol("NM_SUPPLIER", "매입처", 100);
			this._flex클레임H.SetCol("NM_VESSEL", "호선", 100);
			this._flex클레임H.SetCol("NM_KOR", "담당자", 100);
			this._flex클레임H.SetCol("NM_CLAIM", "클레임사유", 100);
			this._flex클레임H.SetCol("NM_CAUSE", "원인구분", 100);
			this._flex클레임H.SetCol("NM_ITEM", "항목분류", 100);
			this._flex클레임H.SetCol("NM_GW_STAT", "결재상태", 100);
			this._flex클레임H.SetCol("NM_STATUS", "상태", 100);
			this._flex클레임H.SetCol("NM_REASON", "클레임원인", 100);
			this._flex클레임H.SetCol("NM_RETURN", "실물반품여부", 100);
			this._flex클레임H.SetCol("NM_REQUEST", "고객사요청사항", 100);
			this._flex클레임H.SetCol("AM_TOTAL_RCV", "해결예상비용", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

			this._flex클레임H.SetDummyColumn("S");

			this._flex클레임H.SettingVersion = "0.0.0.1";
			this._flex클레임H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion

			#region Line
			this._flex클레임L.BeginSetting(1, 1, false);

			this._flex클레임L.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
			this._flex클레임L.SetCol("NO_KEY", "클레임번호", 100);
			this._flex클레임L.SetCol("NO_SO", "수주번호", 100);
			this._flex클레임L.SetCol("DT_INPUT", "작성일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex클레임L.SetCol("NM_PARTNER", "매출처", 100);
			this._flex클레임L.SetCol("NM_SUPPLIER", "매입처", 100);
			this._flex클레임L.SetCol("NM_VESSEL", "호선", 100);
			this._flex클레임L.SetCol("NM_KOR", "담당자", 100);
			this._flex클레임L.SetCol("NM_CLAIM", "클레임사유", 100);
			this._flex클레임L.SetCol("NM_CAUSE", "원인구분", 100);
			this._flex클레임L.SetCol("NM_ITEM", "항목분류", 100);
			this._flex클레임L.SetCol("NM_GW_STAT", "결재상태", 100);
			this._flex클레임L.SetCol("NM_STATUS", "상태", 100);
			this._flex클레임L.SetCol("NM_REASON", "클레임원인", 100);
			this._flex클레임L.SetCol("NM_RETURN", "실물반품여부", 100);
			this._flex클레임L.SetCol("NM_REQUEST", "고객사요청사항", 100);
			this._flex클레임L.SetCol("AM_TOTAL_RCV", "해결예상비용", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex클레임L.SetCol("DC_RMK", "비고", 100, true);

			this._flex클레임L.SetDummyColumn("S");
			this._flex클레임L.ExtendLastCol = true;

			this._flex클레임L.SettingVersion = "0.0.0.1";
			this._flex클레임L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion

			#endregion

			#region 미수금

			#region Header
			this._flex미수금H.BeginSetting(1, 1, false);

			this._flex미수금H.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
			this._flex미수금H.SetCol("NO_IV", "매출번호", 100);
			this._flex미수금H.SetCol("DT_IV", "매출일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex미수금H.SetCol("DT_RCP", "수금일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex미수금H.SetCol("DT_IV_DAY", "경과일수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex미수금H.SetCol("NM_PARTNER", "매출처", 100);
			this._flex미수금H.SetCol("NM_EMP", "담당자", 100);
			this._flex미수금H.SetCol("NM_EXCH", "통화명", 100);
			this._flex미수금H.SetCol("RT_EXCH", "환율", 100, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
			this._flex미수금H.SetCol("AM_EX_CLS", "매출금액(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex미수금H.SetCol("AM_CLS", "매출금액(원화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex미수금H.SetCol("AM_RCP_EX", "수금금액(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex미수금H.SetCol("AM_RCP", "수금금액(원화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex미수금H.SetCol("AM_EX_REMAIN", "미수금액(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex미수금H.SetCol("AM_REMAIN", "미수금액(원화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

			this._flex미수금H.SetDummyColumn("S");

			this._flex미수금H.SettingVersion = "0.0.0.1";
			this._flex미수금H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion

			#region Line
			this._flex미수금L.BeginSetting(1, 1, false);

			this._flex미수금L.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
			this._flex미수금L.SetCol("NO_KEY", "매출번호", 100);
			this._flex미수금L.SetCol("DT_IV", "매출일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex미수금L.SetCol("DT_RCP", "수금일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex미수금L.SetCol("DT_IV_DAY", "경과일수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex미수금L.SetCol("NM_PARTNER", "매출처", 100);
			this._flex미수금L.SetCol("NM_EMP", "담당자", 100);
			this._flex미수금L.SetCol("NM_EXCH", "통화명", 100);
			this._flex미수금L.SetCol("RT_EXCH", "환율", 100, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
			this._flex미수금L.SetCol("AM_EX_CLS", "매출금액(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex미수금L.SetCol("AM_CLS", "매출금액(원화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex미수금L.SetCol("AM_RCP_EX", "수금금액(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex미수금L.SetCol("AM_RCP", "수금금액(원화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex미수금L.SetCol("AM_EX_REMAIN", "미수금액(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex미수금L.SetCol("AM_REMAIN", "미수금액(원화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex미수금L.SetCol("DC_RMK", "비고", 100, true);

			this._flex미수금L.SetDummyColumn("S");
			this._flex미수금L.ExtendLastCol = true;

			this._flex미수금L.SettingVersion = "0.0.0.1";
			this._flex미수금L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion

			#endregion

			#region 프로젝트
			this._flex프로젝트.BeginSetting(1, 1, false);

			this._flex프로젝트.SetCol("S", "선택", 60, true, CheckTypeEnum.Y_N);
			this._flex프로젝트.SetCol("DC_CATEGORY", "제목", 100);
			this._flex프로젝트.SetCol("DT_FROM", "시작일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex프로젝트.SetCol("DT_TO", "종료일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex프로젝트.SetCol("DC_RMK", "내용", 100);
			this._flex프로젝트.SetCol("NO_KEY", "순번", 0);

			this._flex프로젝트.SetDummyColumn("S");

			this._flex프로젝트.SetOneGridBinding(null, new IUParentControl[] { this.pnl프로젝트, this.pnl내용 });

			this._flex프로젝트.SettingVersion = "0.0.0.2";
			this._flex프로젝트.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion
		}

		private void InitEvent()
		{
			this._flex담당자.AfterRowChange += _flex담당자_AfterRowChange;

			this._flex견적H.MouseDoubleClick += Grid_MouseDoubleClick;
			this._flex수주H.MouseDoubleClick += Grid_MouseDoubleClick;
			this._flex클레임H.MouseDoubleClick += Grid_MouseDoubleClick;
			this._flex미수금H.MouseDoubleClick += Grid_MouseDoubleClick;

			this.btn확정.Click += Btn확정_Click;
			this.btn확정취소.Click += Btn확정취소_Click;
			this.btn게시.Click += Btn게시_Click;
			this.btn게시취소.Click += Btn게시취소_Click;

			this.btn견적조회.Click += Btn견적조회_Click;
			this.btn견적추가.Click += Btn추가_Click;
			this.btn견적삭제.Click += Btn삭제_Click;

			this.btn수주조회.Click += Btn수주조회_Click;
			this.btn수주추가.Click += Btn추가_Click;
			this.btn수주삭제.Click += Btn삭제_Click;

			this.btn클레임조회.Click += Btn클레임조회_Click;
			this.btn클레임추가.Click += Btn추가_Click;
			this.btn클레임삭제.Click += Btn삭제_Click;

			this.btn미수금조회.Click += Btn미수금조회_Click;
			this.btn미수금추가.Click += Btn추가_Click;
			this.btn미수금삭제.Click += Btn삭제_Click;

			this.btn프로젝트추가.Click += Btn프로젝트추가_Click;
			this.btn프로젝트삭제.Click += Btn프로젝트삭제_Click;

			this.btn전자결재.Click += Btn전자결재_Click;
		}

		private void Btn전자결재_Click(object sender, EventArgs e)
		{
			try
			{
				this._gw.전자결재(this._flex담당자.GetDataRow(this._flex담당자.Row));
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void Btn미수금조회_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flex담당자.HasNormalRow) return;

				DataTable dt = this._biz.Search4(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																this._flex담당자["NO_EMP"].ToString(),
																this._flex담당자["DT_YEAR"].ToString(),
																this._flex담당자["QT_WEEK"].ToString() });

				this._flex미수금H.Binding = dt;

				if (!this._flex미수금H.HasNormalRow)
				{
					this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void Btn클레임조회_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flex담당자.HasNormalRow) return;

				DataTable dt = this._biz.Search3(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																this._flex담당자["NO_EMP"].ToString(),
																this._flex담당자["DT_YEAR"].ToString(),
																this._flex담당자["QT_WEEK"].ToString() });

				this._flex클레임H.Binding = dt;

				if (!this._flex클레임H.HasNormalRow)
				{
					this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void Btn확정취소_Click(object sender, EventArgs e)
		{
			try
			{
				if (this._flex담당자["YN_POST"].ToString() == "Y")
				{
					this.ShowMessage("게시된 주차는 확정 취소 할 수 없습니다.");
					return;
				}

				if (Global.MainFrame.ShowMessage("선택된 주차를 확정 취소 하시겠습니까?", "QY2") != DialogResult.Yes)
					return;

				this._flex담당자.Redraw = false;

				this._flex담당자["YN_CONFIRM"] = "N";
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
			finally
			{
				this._flex담당자.Redraw = true;
			}
		}

		private void Btn확정_Click(object sender, EventArgs e)
		{
			try
			{
				if (Global.MainFrame.ShowMessage("선택된 주차를 확정 처리 하시겠습니까?", "QY2") != DialogResult.Yes)
					return;

				this._flex담당자.Redraw = false;

				this._flex담당자["YN_CONFIRM"] = "Y";
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
			finally
			{
				this._flex담당자.Redraw = true;
			}
		}

		private void Btn프로젝트삭제_Click(object sender, EventArgs e)
		{
			DataRow[] dataRowArray;

			try
			{
				if (!this._flex프로젝트.HasNormalRow) return;

				dataRowArray = this._flex프로젝트.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					foreach (DataRow dr in dataRowArray)
					{
						dr.Delete();
					}

					this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn프로젝트삭제.Text);
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void Btn프로젝트추가_Click(object sender, EventArgs e)
		{
			string query;
			int index, index1;

			try
			{
				if (!this._flex담당자.HasNormalRow) return;

				query = @"SELECT ISNULL(MAX(RL.NO_KEY), 0) AS NO_KEY 
						  FROM CZ_SA_WEEKLY_RPT_L RL WITH(NOLOCK)
						  WHERE RL.CD_COMPANY = '{0}'
						  AND RL.NO_EMP = '{1}'
						  AND RL.DT_YEAR = '{2}'
					      AND RL.QT_WEEK = {3}
						  AND RL.TP_GUBUN = '003'";

				index = Convert.ToInt32(DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
																	                this._flex담당자["NO_EMP"].ToString(),
																	                this._flex담당자["DT_YEAR"].ToString(),
																	                this._flex담당자["QT_WEEK"].ToString())).ToString());

				index1 = Convert.ToInt32(this._flex프로젝트.GetMaxValue("NO_KEY"));

				this._flex프로젝트.Rows.Add();
				this._flex프로젝트.Row = this._flex프로젝트.Rows.Count - 1;

				this._flex프로젝트["NO_EMP"] = this._flex담당자["NO_EMP"].ToString();
				this._flex프로젝트["DT_YEAR"] = this._flex담당자["DT_YEAR"].ToString();
				this._flex프로젝트["QT_WEEK"] = this._flex담당자["QT_WEEK"].ToString();
				this._flex프로젝트["TP_GUBUN"] = "003";
				this._flex프로젝트["NO_KEY"] = (index >= index1 ? index + 1 : index1 + 1).ToString();

				this._flex프로젝트.AddFinished();
				this._flex프로젝트.Focus();
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void Grid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			FlexGrid grid = null, gridL = null;
			DataRow[] dataRowArray;
			DataRow dr;

			try
			{
				grid = ((FlexGrid)sender);

				if (grid == this._flex견적H)
					gridL = this._flex견적L;
				else if (grid == this._flex수주H)
					gridL = this._flex수주L;
				else if (grid == this._flex클레임H)
					gridL = this._flex클레임L;
				else if (grid == this._flex미수금H)
					gridL = this._flex미수금L;
				else
					return;

				if (!grid.HasNormalRow) return;
				if (grid.MouseRow < grid.Rows.Fixed) return;

				dr = ((DataRowView)grid.Rows[grid.RowSel].DataSource).Row;

				if (dr["NO_EMP"].ToString() != grid["NO_EMP"].ToString())
				{
					this.ShowMessage("담당자가 일치하지 않습니다.");
					return;
				}

				grid.Redraw = false;
				gridL.Redraw = false;

				if (grid == this._flex견적H)
				{
					#region 견적
					dataRowArray = gridL.DataTable.Select(string.Format("NO_EMP = '{0}' AND DT_YEAR = '{1}' AND QT_WEEK = '{2}' AND NO_KEY = '{3}'", dr["NO_EMP"].ToString(),
																																					 dr["DT_YEAR"].ToString(),
																																					 dr["QT_WEEK"].ToString(),
																																					 dr["NO_FILE"].ToString()));

					if (dataRowArray != null && dataRowArray.Length > 0)
					{
						this.ShowMessage("동일건이 이미 추가 되어 있습니다.");
						return;
					}

					gridL.Rows.Add();
					gridL.Row = gridL.Rows.Count - 1;

					gridL["S"] = "N";

					gridL["NO_EMP"] = dr["NO_EMP"];
					gridL["DT_YEAR"] = dr["DT_YEAR"];
					gridL["QT_WEEK"] = dr["QT_WEEK"];
					gridL["TP_GUBUN"] = "001";

					gridL["NO_KEY"] = dr["NO_FILE"];
					gridL["DT_QTN"] = dr["DT_QTN"];
					gridL["LN_PARTNER"] = dr["LN_PARTNER"];
					gridL["NM_KOR"] = dr["NM_KOR"];
					gridL["NM_SALEGRP"] = dr["NM_SALEGRP"];
					gridL["NM_PARTNER_GRP"] = dr["NM_PARTNER_GRP"];
					gridL["QT_ITEM"] = dr["QT_ITEM"];
					gridL["NM_EXCH"] = dr["NM_EXCH"];
					gridL["RT_EXCH"] = dr["RT_EXCH"];
					gridL["AM_QTN_EX"] = dr["AM_QTN_EX"];
					gridL["AM_QTN_S"] = dr["AM_QTN_S"];
					gridL["AM_PROFIT"] = dr["AM_PROFIT"];
					gridL["RT_PROFIT"] = dr["RT_PROFIT"];

					gridL.AddFinished();
					gridL.Focus();
					#endregion
				}
				else if (grid == this._flex수주H)
				{
					#region 수주
					dataRowArray = gridL.DataTable.Select(string.Format("NO_EMP = '{0}' AND DT_YEAR = '{1}' AND QT_WEEK = '{2}' AND NO_KEY = '{3}'", dr["NO_EMP"].ToString(),
																																					 dr["DT_YEAR"].ToString(),
																																					 dr["QT_WEEK"].ToString(),
																																					 dr["NO_SO"].ToString()));

					if (dataRowArray != null && dataRowArray.Length > 0)
					{
						this.ShowMessage("동일건이 이미 추가 되어 있습니다.");
						return;
					}

					gridL.Rows.Add();
					gridL.Row = gridL.Rows.Count - 1;

					gridL["S"] = "N";

					gridL["NO_EMP"] = dr["NO_EMP"];
					gridL["DT_YEAR"] = dr["DT_YEAR"];
					gridL["QT_WEEK"] = dr["QT_WEEK"];
					gridL["TP_GUBUN"] = "002";

					gridL["NO_KEY"] = dr["NO_SO"];
					gridL["DT_SO"] = dr["DT_SO"];
					gridL["LN_PARTNER"] = dr["LN_PARTNER"];
					gridL["NM_KOR"] = dr["NM_KOR"];
					gridL["NM_SALEGRP"] = dr["NM_SALEGRP"];
					gridL["NM_PARTNER_GRP"] = dr["NM_PARTNER_GRP"];
					gridL["QT_ITEM"] = dr["QT_ITEM"];
					gridL["NM_EXCH"] = dr["NM_EXCH"];
					gridL["RT_EXCH"] = dr["RT_EXCH"];
					gridL["AM_SO_EX"] = dr["AM_SO_EX"];
					gridL["AM_SO_S"] = dr["AM_SO_S"];
					gridL["AM_PROFIT"] = dr["AM_PROFIT"];
					gridL["RT_PROFIT"] = dr["RT_PROFIT"];

					gridL.AddFinished();
					gridL.Focus();
					#endregion
				}
				else if (grid == this._flex클레임H)
				{
					#region 클레임
					dataRowArray = gridL.DataTable.Select(string.Format("NO_EMP = '{0}' AND DT_YEAR = '{1}' AND QT_WEEK = '{2}' AND NO_KEY = '{3}'", dr["NO_EMP"].ToString(),
																																					 dr["DT_YEAR"].ToString(),
																																					 dr["QT_WEEK"].ToString(),
																																					 dr["NO_CLAIM"].ToString()));

					if (dataRowArray != null && dataRowArray.Length > 0)
					{
						this.ShowMessage("동일건이 이미 추가 되어 있습니다.");
						return;
					}

					gridL.Rows.Add();
					gridL.Row = gridL.Rows.Count - 1;

					gridL["S"] = "N";

					gridL["NO_EMP"] = dr["NO_EMP"];
					gridL["DT_YEAR"] = dr["DT_YEAR"];
					gridL["QT_WEEK"] = dr["QT_WEEK"];
					gridL["TP_GUBUN"] = "004";

					gridL["NO_KEY"] = dr["NO_CLAIM"];
					gridL["NO_SO"] = dr["NO_SO"];
					gridL["DT_INPUT"] = dr["DT_INPUT"];
					gridL["NM_PARTNER"] = dr["NM_PARTNER"];
					gridL["NM_SUPPLIER"] = dr["NM_SUPPLIER"];
					gridL["NM_VESSEL"] = dr["NM_VESSEL"];
					gridL["NM_KOR"] = dr["NM_KOR"];
					gridL["NM_CLAIM"] = dr["NM_CLAIM"];
					gridL["NM_CAUSE"] = dr["NM_CAUSE"];
					gridL["NM_ITEM"] = dr["NM_ITEM"];
					gridL["NM_GW_STAT"] = dr["NM_GW_STAT"];
					gridL["NM_STATUS"] = dr["NM_STATUS"];
					gridL["NM_REASON"] = dr["NM_REASON"];
					gridL["NM_RETURN"] = dr["NM_RETURN"];
					gridL["NM_REQUEST"] = dr["NM_REQUEST"];
					gridL["AM_TOTAL_RCV"] = dr["AM_TOTAL_RCV"];

					gridL.AddFinished();
					gridL.Focus();
					#endregion
				}
				else if (grid == this._flex미수금H)
				{
					#region 미수금
					dataRowArray = gridL.DataTable.Select(string.Format("NO_EMP = '{0}' AND DT_YEAR = '{1}' AND QT_WEEK = '{2}' AND NO_KEY = '{3}'", dr["NO_EMP"].ToString(),
																																					 dr["DT_YEAR"].ToString(),
																																					 dr["QT_WEEK"].ToString(),
																																					 dr["NO_IV"].ToString()));

					if (dataRowArray != null && dataRowArray.Length > 0)
					{
						this.ShowMessage("동일건이 이미 추가 되어 있습니다.");
						return;
					}

					gridL.Rows.Add();
					gridL.Row = gridL.Rows.Count - 1;

					gridL["S"] = "N";

					gridL["NO_EMP"] = dr["NO_EMP"];
					gridL["DT_YEAR"] = dr["DT_YEAR"];
					gridL["QT_WEEK"] = dr["QT_WEEK"];
					gridL["TP_GUBUN"] = "005";

					gridL["NO_KEY"] = dr["NO_IV"];
					gridL["DT_IV"] = dr["DT_IV"];
					gridL["DT_RCP"] = dr["DT_RCP"];
					gridL["DT_IV_DAY"] = dr["DT_IV_DAY"];
					gridL["NM_PARTNER"] = dr["NM_PARTNER"];
					gridL["NM_EMP"] = dr["NM_EMP"];
					gridL["NM_EXCH"] = dr["NM_EXCH"];
					gridL["RT_EXCH"] = dr["RT_EXCH"];
					gridL["AM_EX_CLS"] = dr["AM_EX_CLS"];
					gridL["AM_CLS"] = dr["AM_CLS"];
					gridL["AM_RCP_EX"] = dr["AM_RCP_EX"];
					gridL["AM_RCP"] = dr["AM_RCP"];
					gridL["AM_EX_REMAIN"] = dr["AM_EX_REMAIN"];
					gridL["AM_REMAIN"] = dr["AM_REMAIN"];

					gridL.AddFinished();
					gridL.Focus();
					#endregion
				}
				else
					return;

				dr.Delete();

				grid.SumRefresh();
				gridL.SumRefresh();
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
			finally
			{
				grid.Redraw = true;
				gridL.Redraw = true;
			}
		}

		private void Btn게시_Click(object sender, EventArgs e)
		{
			DataRow[] dataRowArray;
			string query, 종료일자;

			try
			{
				if (Global.MainFrame.ShowMessage("선택된 주차를 게시 하시겠습니까?", "QY2") != DialogResult.Yes)
					return;

				dataRowArray = this._flex담당자.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					query = @"SELECT TOP 1 DT_CAL 
FROM MA_CALENDAR WITH(NOLOCK)
WHERE CD_COMPANY = 'K100'
AND FG1_HOLIDAY = 'W'
AND DT_CAL >= CONVERT(CHAR(8), DATEADD(DAY, 4 - DATEPART(WEEKDAY, DATEADD(WK, 1, GETDATE())), DATEADD(WK, 1, GETDATE())), 112)
ORDER BY DT_CAL ASC";

					종료일자 = DBHelper.ExecuteScalar(query).ToString();

					this._flex담당자.Redraw = false;

					foreach (DataRow dr in dataRowArray)
					{
						dr["YN_POST"] = "Y";
						dr["DT_FROM"] = Global.MainFrame.GetStringToday;
						dr["DT_TO"] = 종료일자;
					}
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
			finally
			{
				this._flex담당자.Redraw = true;
			}
		}

		private void Btn게시취소_Click(object sender, EventArgs e)
		{
			try
			{
				if (Global.MainFrame.ShowMessage("선택된 주차를 게시취소 하시겠습니까?", "QY2") != DialogResult.Yes)
					return;

				this._flex담당자.Redraw = false;

				this._flex담당자["YN_POST"] = "N";
				this._flex담당자["DT_FROM"] = string.Empty;
				this._flex담당자["DT_TO"] = string.Empty;
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
			finally
			{
				this._flex담당자.Redraw = true;
			}
		}

		private void Btn삭제_Click(object sender, EventArgs e)
		{
			FlexGrid gridL = null;
			DataRow[] dataRowArray;

			try
			{
				base.OnToolBarDeleteButtonClicked(sender, e);

				if (this.tabControl1.SelectedTab == this.tpg견적)
					gridL = this._flex견적L;
				else if (this.tabControl1.SelectedTab == this.tpg수주)
					gridL = this._flex수주L;
				else if (this.tabControl1.SelectedTab == this.tpg클레임)
					gridL = this._flex클레임L;
				else if (this.tabControl1.SelectedTab == this.tpg미수금)
					gridL = this._flex미수금L;
				else
					return;

				dataRowArray = gridL.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					gridL.Redraw = false;

					foreach (DataRow dr in dataRowArray)
					{
						dr.Delete();
					}

					gridL.SumRefresh();

					this.ShowMessage(공통메세지._작업을완료하였습니다, "삭제");
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
			finally
			{
				gridL.Redraw = true;
			}
		}

		private void Btn추가_Click(object sender, EventArgs e)
		{
			FlexGrid grid = null, gridL = null;
			DataRow[] dataRowArray, dataRowArray1;
			DataRow dr;

			try
			{
				base.OnToolBarAddButtonClicked(sender, e);

				if (this.tabControl1.SelectedTab == this.tpg견적)
				{
					grid = this._flex견적H;
					gridL = this._flex견적L;
				}
				else if (this.tabControl1.SelectedTab == this.tpg수주)
				{
					grid = this._flex수주H;
					gridL = this._flex수주L;
				}
				else if (this.tabControl1.SelectedTab == this.tpg클레임)
				{
					grid = this._flex클레임H;
					gridL = this._flex클레임L;
				}
				else if (this.tabControl1.SelectedTab == this.tpg미수금)
				{
					grid = this._flex미수금H;
					gridL = this._flex미수금L;
				}
				else
					return;

				if (!grid.HasNormalRow)
					return;

				dataRowArray = grid.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					grid.Redraw = false;
					gridL.Redraw = false;

					for (int index = 0; index < dataRowArray.Length; ++index)
					{
						dr = dataRowArray[index];

						if (dr["NO_EMP"].ToString() != grid["NO_EMP"].ToString())
						{
							this.ShowMessage("담당자가 일치하지 않습니다.");
							return;
						}

						if (grid == this._flex견적H)
						{
							#region 견적
							dataRowArray1 = gridL.DataTable.Select(string.Format("NO_EMP = '{0}' AND DT_YEAR = '{1}' AND QT_WEEK = '{2}' AND NO_KEY = '{3}'", dr["NO_EMP"].ToString(),
																																							  dr["DT_YEAR"].ToString(),
																																							  dr["QT_WEEK"].ToString(),
																																							  dr["NO_FILE"].ToString()));

							if (dataRowArray1 != null && dataRowArray1.Length > 0)
								continue;

							gridL.Rows.Add();
							gridL.Row = gridL.Rows.Count - 1;

							gridL["S"] = "N";

							gridL["NO_EMP"] = dr["NO_EMP"];
							gridL["DT_YEAR"] = dr["DT_YEAR"];
							gridL["QT_WEEK"] = dr["QT_WEEK"];
							gridL["TP_GUBUN"] = "001";

							gridL["NO_KEY"] = dr["NO_FILE"];
							gridL["DT_QTN"] = dr["DT_QTN"];
							gridL["LN_PARTNER"] = dr["LN_PARTNER"];
							gridL["NM_VESSEL"] = dr["NM_VESSEL"];
							gridL["NM_SUPPLIER"] = dr["NM_SUPPLIER"];
							gridL["NM_KOR"] = dr["NM_KOR"];
							gridL["NM_SALEGRP"] = dr["NM_SALEGRP"];
							gridL["NM_PARTNER_GRP"] = dr["NM_PARTNER_GRP"];
							gridL["QT_ITEM"] = dr["QT_ITEM"];
							gridL["NM_ITEMGRP"] = dr["NM_ITEMGRP"];
							gridL["NM_EXCH"] = dr["NM_EXCH"];
							gridL["RT_EXCH"] = dr["RT_EXCH"];
							gridL["AM_QTN_EX"] = dr["AM_QTN_EX"];
							gridL["AM_QTN_S"] = dr["AM_QTN_S"];
							gridL["AM_PROFIT"] = dr["AM_PROFIT"];
							gridL["RT_PROFIT"] = dr["RT_PROFIT"];

							gridL.AddFinished();
							gridL.Focus();
							#endregion
						}
						else if (grid == this._flex수주H)
						{
							#region 수주
							dataRowArray1 = gridL.DataTable.Select(string.Format("NO_EMP = '{0}' AND DT_YEAR = '{1}' AND QT_WEEK = '{2}' AND NO_KEY = '{3}'", dr["NO_EMP"].ToString(),
																																							  dr["DT_YEAR"].ToString(),
																																							  dr["QT_WEEK"].ToString(),
																																							  dr["NO_SO"].ToString()));

							if (dataRowArray1 != null && dataRowArray1.Length > 0)
								continue;

							gridL.Rows.Add();
							gridL.Row = gridL.Rows.Count - 1;

							gridL["S"] = "N";

							gridL["NO_EMP"] = dr["NO_EMP"];
							gridL["DT_YEAR"] = dr["DT_YEAR"];
							gridL["QT_WEEK"] = dr["QT_WEEK"];
							gridL["TP_GUBUN"] = "002";

							gridL["NO_KEY"] = dr["NO_SO"];
							gridL["DT_SO"] = dr["DT_SO"];
							gridL["LN_PARTNER"] = dr["LN_PARTNER"];
							gridL["NM_VESSEL"] = dr["NM_VESSEL"];
							gridL["NM_SUPPLIER"] = dr["NM_SUPPLIER"];
							gridL["NM_KOR"] = dr["NM_KOR"];
							gridL["NM_SALEGRP"] = dr["NM_SALEGRP"];
							gridL["NM_PARTNER_GRP"] = dr["NM_PARTNER_GRP"];
							gridL["QT_ITEM"] = dr["QT_ITEM"];
							gridL["NM_ITEMGRP"] = dr["NM_ITEMGRP"];
							gridL["NM_EXCH"] = dr["NM_EXCH"];
							gridL["RT_EXCH"] = dr["RT_EXCH"];
							gridL["AM_SO_EX"] = dr["AM_SO_EX"];
							gridL["AM_SO_S"] = dr["AM_SO_S"];
							gridL["AM_PROFIT"] = dr["AM_PROFIT"];
							gridL["RT_PROFIT"] = dr["RT_PROFIT"];

							gridL.AddFinished();
							gridL.Focus();
							#endregion
						}
						else if (grid == this._flex클레임H)
						{
							#region 클레임
							dataRowArray1 = gridL.DataTable.Select(string.Format("NO_EMP = '{0}' AND DT_YEAR = '{1}' AND QT_WEEK = '{2}' AND NO_KEY = '{3}'", dr["NO_EMP"].ToString(),
																																							  dr["DT_YEAR"].ToString(),
																																							  dr["QT_WEEK"].ToString(),
																																							  dr["NO_CLAIM"].ToString()));

							if (dataRowArray1 != null && dataRowArray1.Length > 0)
								continue;

							gridL.Rows.Add();
							gridL.Row = gridL.Rows.Count - 1;

							gridL["S"] = "N";

							gridL["NO_EMP"] = dr["NO_EMP"];
							gridL["DT_YEAR"] = dr["DT_YEAR"];
							gridL["QT_WEEK"] = dr["QT_WEEK"];
							gridL["TP_GUBUN"] = "004";

							gridL["NO_KEY"] = dr["NO_CLAIM"];
							gridL["NO_SO"] = dr["NO_SO"];
							gridL["DT_INPUT"] = dr["DT_INPUT"];
							gridL["NM_PARTNER"] = dr["NM_PARTNER"];
							gridL["NM_SUPPLIER"] = dr["NM_SUPPLIER"];
							gridL["NM_VESSEL"] = dr["NM_VESSEL"];
							gridL["NM_KOR"] = dr["NM_KOR"];
							gridL["NM_CLAIM"] = dr["NM_CLAIM"];
							gridL["NM_CAUSE"] = dr["NM_CAUSE"];
							gridL["NM_ITEM"] = dr["NM_ITEM"];
							gridL["NM_GW_STAT"] = dr["NM_GW_STAT"];
							gridL["NM_STATUS"] = dr["NM_STATUS"];
							gridL["NM_REASON"] = dr["NM_REASON"];
							gridL["NM_RETURN"] = dr["NM_RETURN"];
							gridL["NM_REQUEST"] = dr["NM_REQUEST"];
							gridL["AM_TOTAL_RCV"] = dr["AM_TOTAL_RCV"];

							gridL.AddFinished();
							gridL.Focus();
							#endregion
						}
						else if (grid == this._flex미수금H)
						{
							#region 미수금
							dataRowArray1 = gridL.DataTable.Select(string.Format("NO_EMP = '{0}' AND DT_YEAR = '{1}' AND QT_WEEK = '{2}' AND NO_KEY = '{3}'", dr["NO_EMP"].ToString(),
																																							  dr["DT_YEAR"].ToString(),
																																							  dr["QT_WEEK"].ToString(),
																																							  dr["NO_IV"].ToString()));

							if (dataRowArray1 != null && dataRowArray1.Length > 0)
								continue;

							gridL.Rows.Add();
							gridL.Row = gridL.Rows.Count - 1;

							gridL["S"] = "N";

							gridL["NO_EMP"] = dr["NO_EMP"];
							gridL["DT_YEAR"] = dr["DT_YEAR"];
							gridL["QT_WEEK"] = dr["QT_WEEK"];
							gridL["TP_GUBUN"] = "005";

							gridL["NO_KEY"] = dr["NO_IV"];
							gridL["DT_IV"] = dr["DT_IV"];
							gridL["DT_RCP"] = dr["DT_RCP"];
							gridL["DT_IV_DAY"] = dr["DT_IV_DAY"];
							gridL["NM_PARTNER"] = dr["NM_PARTNER"];
							gridL["NM_EMP"] = dr["NM_EMP"];
							gridL["NM_EXCH"] = dr["NM_EXCH"];
							gridL["RT_EXCH"] = dr["RT_EXCH"];
							gridL["AM_EX_CLS"] = dr["AM_EX_CLS"];
							gridL["AM_CLS"] = dr["AM_CLS"];
							gridL["AM_RCP_EX"] = dr["AM_RCP_EX"];
							gridL["AM_RCP"] = dr["AM_RCP"];
							gridL["AM_EX_REMAIN"] = dr["AM_EX_REMAIN"];
							gridL["AM_REMAIN"] = dr["AM_REMAIN"];

							gridL.AddFinished();
							gridL.Focus();
							#endregion
						}
						else
							return;
					}

					grid.SumRefresh();
					gridL.SumRefresh();

					this.ShowMessage(공통메세지._작업을완료하였습니다, "추가");
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
			finally
			{
				grid.Redraw = true;
				gridL.Redraw = true;
			}
		}

		private void Btn수주조회_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flex담당자.HasNormalRow) return;

				DataTable dt = this._biz.Search2(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																this.dtp수주일자.StartDateToString,
																this.dtp수주일자.EndDateToString,
																this._flex담당자["NO_EMP"].ToString(),
																this._flex담당자["DT_YEAR"].ToString(),
																this._flex담당자["QT_WEEK"].ToString(),
															    this.cur수주금액.DecimalValue });

				this._flex수주H.Binding = dt;

				if (!this._flex수주H.HasNormalRow)
				{
					this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void Btn견적조회_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flex담당자.HasNormalRow) return;

				DataTable dt = this._biz.Search1(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
															    this.dtp견적일자.StartDateToString,
															    this.dtp견적일자.EndDateToString,
																this._flex담당자["NO_EMP"].ToString(),
																this._flex담당자["DT_YEAR"].ToString(),
																this._flex담당자["QT_WEEK"].ToString(),
																this.cur견적금액.DecimalValue });

				this._flex견적H.Binding = dt;

				if (!this._flex견적H.HasNormalRow)
				{
					this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void _flex담당자_AfterRowChange(object sender, RangeEventArgs e)
		{
			DataTable dt1, dt2, dt3, dt4, dt5;
			string filter;

			try
			{
				if (!this._flex담당자.HasNormalRow) return;

				if (this._flex담당자["YN_CONFIRM"].ToString() == "Y")
				{
					this.btn확정취소.Enabled = true;
					this.btn확정.Enabled = false;
				}
				else
				{
					this.btn확정.Enabled = true;
					this.btn확정취소.Enabled = false;
				}

				if (this._flex담당자["YN_POST"].ToString() == "Y")
				{
					this.btn게시취소.Enabled = true;
					this.btn게시.Enabled = false;
				}
				else
				{
					this.btn게시.Enabled = true;
					this.btn게시취소.Enabled = false;
				}

				if (this._flex담당자["YN_POST"].ToString() == "Y")
					this.ControlEnable(false);
				else if (this._flex담당자["YN_CONFIRM"].ToString() == "Y" && this._isTeamLeader == true)
				{
					this.ControlEnable(true);
				}
				else if (this._flex담당자["YN_CONFIRM"].ToString() == "Y" && this._isTeamLeader == false)
				{
					this.ControlEnable(false);
				}
				else
					this.ControlEnable(true);

				dt1 = null;
				dt2 = null;
				dt3 = null;
				dt4 = null;
				dt5 = null;

				filter = string.Format(@"NO_EMP = '{0}' AND DT_YEAR = '{1}' AND QT_WEEK = '{2}'", this._flex담당자["NO_EMP"].ToString(),
																							      this._flex담당자["DT_YEAR"].ToString(),
																							      this._flex담당자["QT_WEEK"].ToString());

				if (this._flex담당자.DetailQueryNeed == true)
				{
					dt1 = this._biz.SearchDetail1(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
														         this._flex담당자["NO_EMP"].ToString(),
																 this._flex담당자["DT_YEAR"].ToString(),
																 this._flex담당자["QT_WEEK"].ToString() });

					dt2 = this._biz.SearchDetail2(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
														         this._flex담당자["NO_EMP"].ToString(),
																 this._flex담당자["DT_YEAR"].ToString(),
																 this._flex담당자["QT_WEEK"].ToString() });

					dt3 = this._biz.SearchDetail3(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																 this._flex담당자["NO_EMP"].ToString(),
																 this._flex담당자["DT_YEAR"].ToString(),
																 this._flex담당자["QT_WEEK"].ToString() });

					dt4 = this._biz.SearchDetail4(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																 this._flex담당자["NO_EMP"].ToString(),
																 this._flex담당자["DT_YEAR"].ToString(),
																 this._flex담당자["QT_WEEK"].ToString() });

					dt5 = this._biz.SearchDetail5(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																 this._flex담당자["NO_EMP"].ToString(),
																 this._flex담당자["DT_YEAR"].ToString(),
																 this._flex담당자["QT_WEEK"].ToString() });
				}

				this._flex견적H.Binding = null;
				this._flex수주H.Binding = null;
				this._flex클레임H.Binding = null;
				this._flex미수금H.Binding = null;

				this._flex견적L.BindingAdd(dt1, filter);
				this._flex수주L.BindingAdd(dt2, filter);
				this._flex프로젝트.BindingAdd(dt3, filter);
				this._flex클레임L.BindingAdd(dt4, filter);
				this._flex미수금L.BindingAdd(dt5, filter);
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void ControlEnable(bool isEnable)
		{
			try
			{
				this.btn견적조회.Enabled = isEnable;
				this.btn견적추가.Enabled = isEnable;
				this.btn견적삭제.Enabled = isEnable;

				this.btn수주조회.Enabled = isEnable;
				this.btn수주추가.Enabled = isEnable;
				this.btn수주삭제.Enabled = isEnable;

				this.btn클레임조회.Enabled = isEnable;
				this.btn클레임추가.Enabled = isEnable;
				this.btn클레임삭제.Enabled = isEnable;

				this.btn미수금조회.Enabled = isEnable;
				this.btn미수금추가.Enabled = isEnable;
				this.btn미수금삭제.Enabled = isEnable;

				this.btn프로젝트추가.Enabled = isEnable;
				this.btn프로젝트삭제.Enabled = isEnable;
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarSearchButtonClicked(sender, e);

				this._flex담당자.Binding = this._biz.SearchHeader(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																				 this.dtp작성일자.StartDateToString,
																				 this.dtp작성일자.EndDateToString,
																				 this.ctx팀.CodeValue,
																			     this.ctx담당자.CodeValue,
																				 Global.MainFrame.LoginInfo.EmployeeNo,
																				 (this._isTeamLeader == true ? "Y" : "N") });

				if (!this._flex담당자.HasNormalRow)
				{
					Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
			DataTable dt, dt1;
			int weekOfYear;
			string query, year;

			try
			{
				base.OnToolBarAddButtonClicked(sender, e);

				if (!BeforeAdd()) return;

				Calendar cal = DateTimeFormatInfo.CurrentInfo.Calendar;

				if (DateTime.Today.DayOfWeek > DayOfWeek.Wednesday || 
					this.chk미리작성.Checked) //수요일 이후 OR 수요일 이전에 미리작성시 이번주 결재분 작성
				{
					weekOfYear = cal.GetWeekOfYear(DateTime.Today, DateTimeFormatInfo.CurrentInfo.CalendarWeekRule, DayOfWeek.Sunday);
					year = DateTime.Today.Year.ToString();
				}
				else // 수요일까지 지난주 결재분 작성
				{
					weekOfYear = cal.GetWeekOfYear(DateTime.Today.AddDays(-7), DateTimeFormatInfo.CurrentInfo.CalendarWeekRule, DayOfWeek.Sunday);
					year = DateTime.Today.AddDays(-7).Year.ToString();
				}

				query = @"SELECT * 
						  FROM CZ_SA_WEEKLY_RPT_H RH WITH(NOLOCK)
						  WHERE RH.CD_COMPANY = '{0}'
						  AND RH.NO_EMP = '{1}'
						  AND RH.DT_YEAR = '{2}'
					      AND RH.QT_WEEK = {3}";

				dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
																Global.MainFrame.LoginInfo.EmployeeNo,
																year,
																weekOfYear));

				if (dt != null && dt.Rows.Count > 0)
				{
					this.ShowMessage("해당 주차는 이미 작성되어 있습니다.");
					return;
				}

				this._flex담당자.Rows.Add();
				this._flex담당자.Row = this._flex담당자.Rows.Count - 1;

				this._flex담당자["NO_EMP"] = Global.MainFrame.LoginInfo.EmployeeNo;
				this._flex담당자["DT_YEAR"] = year;
				this._flex담당자["QT_WEEK"] = weekOfYear;

				this._flex담당자.AddFinished();
				this._flex담당자.Focus();

				#region 이전주차 복사
				query = @"SELECT TOP 1 RH.DT_YEAR,
RH.QT_WEEK,
FORMAT(CONVERT(DATETIME, RH.DT_FROM), 'yyyy-MM-dd') AS DT_FROM,
FORMAT(CONVERT(DATETIME, RH.DT_TO), 'yyyy-MM-dd') AS DT_TO
FROM CZ_SA_WEEKLY_RPT_H RH WITH(NOLOCK)
WHERE RH.CD_COMPANY = '{0}'
AND RH.NO_EMP = '{1}'
AND ISNULL(RH.YN_POST, 'N') = 'Y'
ORDER BY RH.DT_FROM DESC";

				dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
																Global.MainFrame.LoginInfo.EmployeeNo));

				if (dt == null || dt.Rows.Count == 0)
				{
					this.ShowMessage("이전주차의 게시된 정보가 없습니다.");
				}
				else
				{
					if (Global.MainFrame.ShowMessage(string.Format("{0} ~ {1} 까지 게시된 정보를 복사 하시겠습니까?", dt.Rows[0]["DT_FROM"], dt.Rows[0]["DT_TO"]), "QY2") != DialogResult.Yes)
						return;

					this._flex견적L.Redraw = false;
					this._flex수주L.Redraw = false;
					this._flex프로젝트.Redraw = false;
					this._flex클레임L.Redraw = false;
					this._flex미수금L.Redraw = false;

					#region 견적
					dt1 = DBHelper.GetDataTable("SP_CZ_SA_WEEKLY_RPT_L1_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																						   Global.MainFrame.LoginInfo.EmployeeNo,
																						   dt.Rows[0]["DT_YEAR"],
																						   dt.Rows[0]["QT_WEEK"] });

					foreach (DataRow dr in dt1.Rows)
					{
						this._flex견적L.Rows.Add();
						this._flex견적L.Row = this._flex견적L.Rows.Count - 1;

						this._flex견적L["S"] = "N";

						this._flex견적L["NO_EMP"] = Global.MainFrame.LoginInfo.EmployeeNo;
						this._flex견적L["DT_YEAR"] = year;
						this._flex견적L["QT_WEEK"] = weekOfYear;
						this._flex견적L["TP_GUBUN"] = "001";

						this._flex견적L["NO_KEY"] = dr["NO_KEY"];
						this._flex견적L["DT_QTN"] = dr["DT_QTN"];
						this._flex견적L["LN_PARTNER"] = dr["LN_PARTNER"];
						this._flex견적L["NM_VESSEL"] = dr["NM_VESSEL"];
						this._flex견적L["NM_SUPPLIER"] = dr["NM_SUPPLIER"];
						this._flex견적L["NM_KOR"] = dr["NM_KOR"];
						this._flex견적L["NM_SALEGRP"] = dr["NM_SALEGRP"];
						this._flex견적L["NM_PARTNER_GRP"] = dr["NM_PARTNER_GRP"];
						this._flex견적L["QT_ITEM"] = dr["QT_ITEM"];
						this._flex견적L["NM_ITEMGRP"] = dr["NM_ITEMGRP"];
						this._flex견적L["NM_EXCH"] = dr["NM_EXCH"];
						this._flex견적L["RT_EXCH"] = dr["RT_EXCH"];
						this._flex견적L["AM_QTN_EX"] = dr["AM_QTN_EX"];
						this._flex견적L["AM_QTN_S"] = dr["AM_QTN_S"];
						this._flex견적L["AM_PROFIT"] = dr["AM_PROFIT"];
						this._flex견적L["RT_PROFIT"] = dr["RT_PROFIT"];
						this._flex견적L["DC_RMK"] = dr["DC_RMK"];

						this._flex견적L.AddFinished();
						this._flex견적L.Focus();
					}
					#endregion

					#region 수주
					//dt1 = DBHelper.GetDataTable("SP_CZ_SA_WEEKLY_RPT_L2_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
					//																	   Global.MainFrame.LoginInfo.EmployeeNo,
					//																	   dt.Rows[0]["DT_YEAR"],
					//																	   dt.Rows[0]["QT_WEEK"] });

					//foreach (DataRow dr in dt1.Rows)
					//{
					//	this._flex수주L.Rows.Add();
					//	this._flex수주L.Row = this._flex수주L.Rows.Count - 1;

					//	this._flex수주L["S"] = "N";

					//	this._flex수주L["NO_EMP"] = Global.MainFrame.LoginInfo.EmployeeNo;
					//	this._flex수주L["DT_YEAR"] = year;
					//	this._flex수주L["QT_WEEK"] = weekOfYear;
					//	this._flex수주L["TP_GUBUN"] = "002";

					//	this._flex수주L["NO_KEY"] = dr["NO_KEY"];
					//	this._flex수주L["DT_SO"] = dr["DT_SO"];
					//	this._flex수주L["LN_PARTNER"] = dr["LN_PARTNER"];
					//	this._flex수주L["NM_KOR"] = dr["NM_KOR"];
					//	this._flex수주L["NM_SALEGRP"] = dr["NM_SALEGRP"];
					//	this._flex수주L["NM_PARTNER_GRP"] = dr["NM_PARTNER_GRP"];
					//	this._flex수주L["QT_ITEM"] = dr["QT_ITEM"];
					//	this._flex수주L["NM_EXCH"] = dr["NM_EXCH"];
					//	this._flex수주L["RT_EXCH"] = dr["RT_EXCH"];
					//	this._flex수주L["AM_SO_EX"] = dr["AM_SO_EX"];
					//	this._flex수주L["AM_SO_S"] = dr["AM_SO_S"];
					//	this._flex수주L["AM_PROFIT"] = dr["AM_PROFIT"];
					//	this._flex수주L["RT_PROFIT"] = dr["RT_PROFIT"];

					//	this._flex수주L.AddFinished();
					//	this._flex수주L.Focus();
					//}
					#endregion

					#region 프로젝트
					dt1 = DBHelper.GetDataTable("SP_CZ_SA_WEEKLY_RPT_L3_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																						   Global.MainFrame.LoginInfo.EmployeeNo,
																						   dt.Rows[0]["DT_YEAR"],
																						   dt.Rows[0]["QT_WEEK"] });

					foreach (DataRow dr in dt1.Rows)
					{
						this._flex프로젝트.Rows.Add();
						this._flex프로젝트.Row = this._flex프로젝트.Rows.Count - 1;

						this._flex프로젝트["S"] = "N";

						this._flex프로젝트["NO_EMP"] = Global.MainFrame.LoginInfo.EmployeeNo;
						this._flex프로젝트["DT_YEAR"] = year;
						this._flex프로젝트["QT_WEEK"] = weekOfYear;
						this._flex프로젝트["TP_GUBUN"] = "003";

						this._flex프로젝트["NO_KEY"] = dr["NO_KEY"];
						this._flex프로젝트["DC_CATEGORY"] = dr["DC_CATEGORY"];
						this._flex프로젝트["DT_FROM"] = dr["DT_FROM"];
						this._flex프로젝트["DT_TO"] = dr["DT_TO"];
						this._flex프로젝트["DC_RMK"] = dr["DC_RMK"];

						this._flex프로젝트.AddFinished();
						this._flex프로젝트.Focus();
					}
					#endregion

					#region 클레임
					dt1 = DBHelper.GetDataTable("SP_CZ_SA_WEEKLY_RPT_L4_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																						   Global.MainFrame.LoginInfo.EmployeeNo,
																						   dt.Rows[0]["DT_YEAR"],
																						   dt.Rows[0]["QT_WEEK"] });

					foreach (DataRow dr in dt1.Rows)
					{
						this._flex클레임L.Rows.Add();
						this._flex클레임L.Row = this._flex클레임L.Rows.Count - 1;

						this._flex클레임L["S"] = "N";

						this._flex클레임L["NO_EMP"] = Global.MainFrame.LoginInfo.EmployeeNo;
						this._flex클레임L["DT_YEAR"] = year;
						this._flex클레임L["QT_WEEK"] = weekOfYear;
						this._flex클레임L["TP_GUBUN"] = "004";

						this._flex클레임L["NO_KEY"] = dr["NO_KEY"];
						this._flex클레임L["NO_SO"] = dr["NO_SO"];
						this._flex클레임L["DT_INPUT"] = dr["DT_INPUT"];
						this._flex클레임L["NM_PARTNER"] = dr["NM_PARTNER"];
						this._flex클레임L["NM_SUPPLIER"] = dr["NM_SUPPLIER"];
						this._flex클레임L["NM_VESSEL"] = dr["NM_VESSEL"];
						this._flex클레임L["NM_KOR"] = dr["NM_KOR"];
						this._flex클레임L["NM_CLAIM"] = dr["NM_CLAIM"];
						this._flex클레임L["NM_CAUSE"] = dr["NM_CAUSE"];
						this._flex클레임L["NM_ITEM"] = dr["NM_ITEM"];
						this._flex클레임L["NM_GW_STAT"] = dr["NM_GW_STAT"];
						this._flex클레임L["NM_STATUS"] = dr["NM_STATUS"];
						this._flex클레임L["NM_REASON"] = dr["NM_REASON"];
						this._flex클레임L["NM_RETURN"] = dr["NM_RETURN"];
						this._flex클레임L["NM_REQUEST"] = dr["NM_REQUEST"];
						this._flex클레임L["AM_TOTAL_RCV"] = dr["AM_TOTAL_RCV"];
						this._flex클레임L["DC_RMK"] = dr["DC_RMK"];

						this._flex클레임L.AddFinished();
						this._flex클레임L.Focus();
					}
					#endregion

					#region 미수금
					dt1 = DBHelper.GetDataTable("SP_CZ_SA_WEEKLY_RPT_L5_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																						   Global.MainFrame.LoginInfo.EmployeeNo,
																						   dt.Rows[0]["DT_YEAR"],
																						   dt.Rows[0]["QT_WEEK"] });

					this._flex미수금L.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
					this._flex미수금L.SetCol("NO_KEY", "매출번호", 100);

					foreach (DataRow dr in dt1.Rows)
					{
						this._flex미수금L.Rows.Add();
						this._flex미수금L.Row = this._flex미수금L.Rows.Count - 1;

						this._flex미수금L["S"] = "N";

						this._flex미수금L["NO_EMP"] = Global.MainFrame.LoginInfo.EmployeeNo;
						this._flex미수금L["DT_YEAR"] = year;
						this._flex미수금L["QT_WEEK"] = weekOfYear;
						this._flex미수금L["TP_GUBUN"] = "005";

						this._flex미수금L["NO_KEY"] = dr["NO_KEY"];
						this._flex미수금L["DT_IV"] = dr["DT_IV"];
						this._flex미수금L["DT_RCP"] = dr["DT_RCP"];
						this._flex미수금L["DT_IV_DAY"] = dr["DT_IV_DAY"];
						this._flex미수금L["NM_PARTNER"] = dr["NM_PARTNER"];
						this._flex미수금L["NM_EMP"] = dr["NM_EMP"];
						this._flex미수금L["NM_EXCH"] = dr["NM_EXCH"];
						this._flex미수금L["RT_EXCH"] = dr["RT_EXCH"];
						this._flex미수금L["AM_EX_CLS"] = dr["AM_EX_CLS"];
						this._flex미수금L["AM_CLS"] = dr["AM_CLS"];
						this._flex미수금L["AM_RCP_EX"] = dr["AM_RCP_EX"];
						this._flex미수금L["AM_RCP"] = dr["AM_RCP"];
						this._flex미수금L["AM_EX_REMAIN"] = dr["AM_EX_REMAIN"];
						this._flex미수금L["AM_REMAIN"] = dr["AM_REMAIN"];
						this._flex미수금L["DC_RMK"] = dr["DC_RMK"];

						this._flex미수금L.AddFinished();
						this._flex미수금L.Focus();
					}
					#endregion
				}
				#endregion
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
			finally
			{
				this._flex견적L.Redraw = true;
				this._flex수주L.Redraw = true;
				this._flex프로젝트.Redraw = true;
				this._flex클레임L.Redraw = true;
				this._flex미수금L.Redraw = true;
			}
		}

		public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarDeleteButtonClicked(sender, e);

				if (!this.BeforeDelete() || !this._flex담당자.HasNormalRow) return;
				if (this._flex담당자["YN_CONFIRM"].ToString() == "Y")
				{
					this.ShowMessage("확정된 건은 삭제할 수 없습니다.");
					return;
				}

				if (this._flex담당자["YN_POST"].ToString() == "Y")
				{
					this.ShowMessage("게시된 건은 삭제할 수 없습니다.");
					return;
				}

				this._flex담당자.Rows.Remove(this._flex담당자.Row);
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarSaveButtonClicked(sender, e);

				if (!this.BeforeSave()) return;

				if (MsgAndSave(PageActionMode.Save))
					ShowMessage(PageResultMode.SaveGood);
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarPrintButtonClicked(sender, e);

				this._gw.미리보기(this._flex담당자.GetDataRow(this._flex담당자.Row));
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		protected override bool SaveData()
		{
			if (!base.SaveData() || !base.Verify()) return false;

			if (this._flex담당자.IsDataChanged == false && 
				this._flex견적L.IsDataChanged == false && 
				this._flex수주L.IsDataChanged == false &&
				this._flex프로젝트.IsDataChanged == false &&
				this._flex클레임L.IsDataChanged == false &&
				this._flex미수금L.IsDataChanged == false) return false;

			if (!this._biz.SaveJson(this._flex담당자.GetChanges(), 
								    this._flex견적L.GetChanges(), 
								    this._flex수주L.GetChanges(),
									this._flex프로젝트.GetChanges(),
									this._flex클레임L.GetChanges(),
									this._flex미수금L.GetChanges())) return false;

			this._flex담당자.AcceptChanges();
			this._flex견적L.AcceptChanges();
			this._flex수주L.AcceptChanges();
			this._flex프로젝트.AcceptChanges();
			this._flex클레임L.AcceptChanges();
			this._flex미수금L.AcceptChanges();

			return true;
		}
	}
}

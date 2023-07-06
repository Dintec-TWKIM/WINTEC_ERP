using System;
using System.Data;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.BizOn.Windows.PivotGrid;
using DevExpress.XtraPivotGrid;
using DevExpress.Utils;

namespace cz
{
	public partial class P_CZ_SA_AC_RECEIVABLE_RPT : PageBase
	{
		P_CZ_SA_AC_RECEIVABLE_RPT_BIZ _biz = new P_CZ_SA_AC_RECEIVABLE_RPT_BIZ();

		private enum 탭구분
		{
			계산서번호,
            수주번호,
            매출처,
            요약,
            피벗
		}

		public P_CZ_SA_AC_RECEIVABLE_RPT()
		{
            StartUp.Certify(this);
			InitializeComponent();
		}

		protected override void InitLoad()
		{
			base.InitLoad();

			this.InitGrid();
            this.InitPivot();
            this.InitEvent();
		}

		private void InitGrid()
		{
            this.MainGrids = new FlexGrid[] { this._flex계산서번호H, this._flex매출처별 };
            this._flex계산서번호H.DetailGrids = new FlexGrid[] { this._flex계산서번호L };

			#region 계산서번호

			#region Header
			this._flex계산서번호H.BeginSetting(1, 1, false);

            this._flex계산서번호H.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flex계산서번호H.SetCol("NO_IV", "계산서번호", 100);
            this._flex계산서번호H.SetCol("NO_SO", "수주번호", 100);
            this._flex계산서번호H.SetCol("CD_PJT", "프로젝트", 100);
            this._flex계산서번호H.SetCol("NO_PO_PARTNER", "매출처발주번호", 100);
            this._flex계산서번호H.SetCol("DT_IV", "매출일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex계산서번호H.SetCol("DT_RCP", "수금일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex계산서번호H.SetCol("DT_IV_MONTH", "경과월수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex계산서번호H.SetCol("DT_RCP_DAY", "수금일수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex계산서번호H.SetCol("NM_PARTNER", "매출처", 100);
            this._flex계산서번호H.SetCol("NO_IMO", "IMO번호", 100);
            this._flex계산서번호H.SetCol("NM_VESSEL", "호선명", 100);
            this._flex계산서번호H.SetCol("NM_EMP", "담당자", 60, true);
            this._flex계산서번호H.SetCol("NM_DEPT", "부서", 60);
            this._flex계산서번호H.SetCol("NM_SALEGRP", "영업그룹", 80);
            this._flex계산서번호H.SetCol("NM_PARTNER_GRP", "매출처그룹", 80);
            this._flex계산서번호H.SetCol("NM_EXCH", "통화명", 60);
            this._flex계산서번호H.SetCol("RT_EXCH", "환율", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flex계산서번호H.SetCol("AM_EX_CLS", "채권외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex계산서번호H.SetCol("AM_CLS", "채권원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex계산서번호H.SetCol("AM_RCP_EX", "수금외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex계산서번호H.SetCol("AM_RCP", "수금원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex계산서번호H.SetCol("AM_EX_REMAIN", "미수외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex계산서번호H.SetCol("AM_REMAIN", "미수원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex계산서번호H.SetCol("AM_EX_2MONTH", "2개월초과(외화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex계산서번호H.SetCol("AM_2MONTH", "2개월초과(원화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex계산서번호H.SetCol("AM_EX_3MONTH", "3개월초과(외화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex계산서번호H.SetCol("AM_3MONTH", "3개월초과(원화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex계산서번호H.SetCol("AM_EX_6MONTH", "6개월초과(외화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex계산서번호H.SetCol("AM_6MONTH", "6개월초과(원화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex계산서번호H.SetCol("AM_EX_12MONTH", "12개월초과(외화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex계산서번호H.SetCol("AM_12MONTH", "12개월초과(원화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex계산서번호H.SetCol("DC_RMK1", "수주비고(커미션)", 100);
            this._flex계산서번호H.SetCol("DC_REMARK", "매출비고", 100);
            this._flex계산서번호H.SetCol("DC_RMK", "비고", 100, true);

            this._flex계산서번호H.ExtendLastCol = true;

            this._flex계산서번호H.SetCodeHelpCol("NM_EMP", HelpID.P_MA_TABLE_SUB, ShowHelpEnum.Always, new string[] { "NO_EMP", "NM_EMP", "NM_DEPT", "NM_SALEGRP" }, new string[] { "CODE", "NAME", "NM_DEPT", "NM_SALEGRP" });

            this._flex계산서번호H.SetDummyColumn("S");

            this._flex계산서번호H.SettingVersion = "0.0.0.2";
            this._flex계산서번호H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            this._flex계산서번호H.SetExceptSumCol("DT_IV_MONTH", "DT_RCP_DAY", "RT_EXCH");
            #endregion

            #region Line
            this._flex계산서번호L.BeginSetting(1, 1, false);

            this._flex계산서번호L.SetCol("NO_SO", "수주번호", 100);
            this._flex계산서번호L.SetCol("NO_PO_PARTNER", "매출처발주번호", 100);
            this._flex계산서번호L.SetCol("DT_IV", "매출일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex계산서번호L.SetCol("DT_RCP", "수금일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex계산서번호L.SetCol("DT_IV_MONTH", "경과월수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex계산서번호L.SetCol("DT_RCP_DAY", "수금일수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex계산서번호L.SetCol("NM_PARTNER", "매출처", 100);
            this._flex계산서번호L.SetCol("NO_IMO", "IMO번호", 100);
            this._flex계산서번호L.SetCol("NM_VESSEL", "호선명", 100);
            this._flex계산서번호L.SetCol("NM_EMP", "담당자", 60);
            this._flex계산서번호L.SetCol("NM_DEPT", "부서", 60);
            this._flex계산서번호L.SetCol("NM_SALEGRP", "영업그룹", 80);
            this._flex계산서번호L.SetCol("NM_EXCH", "통화명", 60);
            this._flex계산서번호L.SetCol("AM_EX_CLS", "채권외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex계산서번호L.SetCol("AM_CLS", "채권원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex계산서번호L.SetCol("AM_RCP_EX", "수금외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex계산서번호L.SetCol("AM_RCP", "수금원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex계산서번호L.SetCol("AM_EX_REMAIN", "미수외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex계산서번호L.SetCol("AM_REMAIN", "미수원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex계산서번호L.SetCol("AM_EX_2MONTH", "2개월초과(외화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex계산서번호L.SetCol("AM_2MONTH", "2개월초과(원화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex계산서번호L.SetCol("AM_EX_3MONTH", "3개월초과(외화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex계산서번호L.SetCol("AM_3MONTH", "3개월초과(원화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex계산서번호L.SetCol("AM_EX_6MONTH", "6개월초과(외화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex계산서번호L.SetCol("AM_6MONTH", "6개월초과(원화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex계산서번호L.SetCol("AM_EX_12MONTH", "12개월초과(외화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex계산서번호L.SetCol("AM_12MONTH", "12개월초과(원화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex계산서번호L.SetCol("DC_RMK1", "수주비고(커미션)", 100);
            this._flex계산서번호L.SetCol("DC_RMK", "비고", 100, true);

            this._flex계산서번호L.ExtendLastCol = true;

            this._flex계산서번호L.SettingVersion = "0.0.0.1";
            this._flex계산서번호L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            this._flex계산서번호L.SetExceptSumCol("DT_IV_MONTH", "DT_RCP_DAY");
            #endregion

            #endregion

            #region 수주번호
            this._flex수주번호.BeginSetting(1, 1, false);

            this._flex수주번호.SetCol("NO_SO", "수주번호", 100);
            this._flex수주번호.SetCol("NO_PO_PARTNER", "매출처발주번호", 100);
            this._flex수주번호.SetCol("DT_IV", "매출일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex수주번호.SetCol("DT_RCP", "수금일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex수주번호.SetCol("DT_IV_MONTH", "경과월수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex수주번호.SetCol("DT_RCP_DAY", "수금일수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex수주번호.SetCol("NM_PARTNER", "매출처", 100);
            this._flex수주번호.SetCol("NO_IMO", "IMO번호", 100);
            this._flex수주번호.SetCol("NM_VESSEL", "호선명", 100);
            this._flex수주번호.SetCol("NM_EMP", "담당자", 60);
            this._flex수주번호.SetCol("NM_DEPT", "부서", 60);
            this._flex수주번호.SetCol("NM_SALEGRP", "영업그룹", 80);
            this._flex수주번호.SetCol("NM_PARTNER_GRP", "매출처그룹", 80);
            this._flex수주번호.SetCol("NM_EXCH", "통화명", 60);
            this._flex수주번호.SetCol("AM_EX_CLS", "채권외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수주번호.SetCol("AM_CLS", "채권원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수주번호.SetCol("AM_RCP_EX", "수금외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수주번호.SetCol("AM_RCP", "수금원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수주번호.SetCol("AM_EX_REMAIN", "미수외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수주번호.SetCol("AM_REMAIN", "미수원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수주번호.SetCol("AM_EX_2MONTH", "2개월초과(외화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수주번호.SetCol("AM_2MONTH", "2개월초과(원화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수주번호.SetCol("AM_EX_3MONTH", "3개월초과(외화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수주번호.SetCol("AM_3MONTH", "3개월초과(원화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수주번호.SetCol("AM_EX_6MONTH", "6개월초과(외화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수주번호.SetCol("AM_6MONTH", "6개월초과(원화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수주번호.SetCol("AM_EX_12MONTH", "12개월초과(외화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수주번호.SetCol("AM_12MONTH", "12개월초과(원화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수주번호.SetCol("DC_RMK1", "수주비고(커미션)", 100);
            this._flex수주번호.SetCol("DC_RMK", "비고", 100, true);

            this._flex수주번호.ExtendLastCol = true;

            this._flex수주번호.SettingVersion = "0.0.0.1";
            this._flex수주번호.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            this._flex수주번호.SetExceptSumCol("DT_IV_MONTH", "DT_RCP_DAY");
            #endregion

            #region 매출처별
            this._flex매출처별.BeginSetting(1, 1, false);

            this._flex매출처별.SetCol("NM_PARTNER", "매출처", 100);
            this._flex매출처별.SetCol("NM_PARTNER_GRP", "매출처그룹", 80);
            this._flex매출처별.SetCol("DT_RCP_PREARRANGED", "수금예정일", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex매출처별.SetCol("DT_IV_DAY", "평균경과일수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex매출처별.SetCol("DT_RCP_DAY", "평균수금일수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex매출처별.SetCol("NM_EMP", "담당자", 60);
            this._flex매출처별.SetCol("NM_EXCH", "통화명", 60);
            this._flex매출처별.SetCol("AM_EX_CLS", "채권외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매출처별.SetCol("AM_CLS", "채권원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매출처별.SetCol("AM_RCP_EX", "수금외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매출처별.SetCol("AM_RCP", "수금원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매출처별.SetCol("AM_EX_REMAIN", "미수외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매출처별.SetCol("AM_REMAIN", "미수원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매출처별.SetCol("AM_EX_2MONTH", "2개월초과(외화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매출처별.SetCol("AM_2MONTH", "2개월초과(원화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매출처별.SetCol("AM_EX_3MONTH", "3개월초과(외화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매출처별.SetCol("AM_3MONTH", "3개월초과(원화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매출처별.SetCol("AM_EX_6MONTH", "6개월초과(외화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매출처별.SetCol("AM_6MONTH", "6개월초과(원화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매출처별.SetCol("AM_EX_12MONTH", "12개월초과(외화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매출처별.SetCol("AM_12MONTH", "12개월초과(원화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매출처별.SetCol("DC_RMK", "비고", 100, true);

            this._flex매출처별.ExtendLastCol = true;

            this._flex매출처별.SettingVersion = "0.0.0.4";
            this._flex매출처별.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            this._flex매출처별.SetExceptSumCol("DT_RCP_PREARRANGED", "DT_IV_DAY", "DT_RCP_DAY");
            #endregion
            
            #region 부서별
			this._flex부서별.BeginSetting(2, 1, false);

			this._flex부서별.SetCol("NM_DEPT", "부서", 80);
			this._flex부서별.SetCol("AM_1MONTH", "2개월이하금액", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex부서별.SetCol("AM_2MONTH", "2개월초과금액", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex부서별.SetCol("AM_3MONTH", "3개월초과금액", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex부서별.SetCol("AM_6MONTH", "6개월초과금액", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex부서별.SetCol("AM_12MONTH", "12개월초과금액", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

			#region 미수금액(전월)
			this._flex부서별.SetCol("AM_LONG_BEFORE", "장기", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex부서별.SetCol("AM_BEFORE", "전체", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

			this._flex부서별[0, this._flex부서별.Cols["AM_LONG_BEFORE"].Index] = this.DD("미수금액(전월)");
			this._flex부서별[0, this._flex부서별.Cols["AM_BEFORE"].Index] = this.DD("미수금액(전월)");
			#endregion

			#region 미수금액(당월)
			this._flex부서별.SetCol("AM_LONG_TOTAL", "장기", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex부서별.SetCol("AM_TOTAL", "전체", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

			this._flex부서별[0, this._flex부서별.Cols["AM_LONG_TOTAL"].Index] = this.DD("미수금액(당월)");
			this._flex부서별[0, this._flex부서별.Cols["AM_TOTAL"].Index] = this.DD("미수금액(당월)");
			#endregion

			#region 수금액
			this._flex부서별.SetCol("AM_LONG_RCP", "장기", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex부서별.SetCol("AM_RCP", "전체", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

			this._flex부서별[0, this._flex부서별.Cols["AM_LONG_RCP"].Index] = this.DD("수금액(당월)");
			this._flex부서별[0, this._flex부서별.Cols["AM_RCP"].Index] = this.DD("수금액(당월)");
			#endregion

			#region 수금율
			this._flex부서별.SetCol("RT_LONG_RCP", "장기", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
			this._flex부서별.SetCol("RT_RCP", "전체", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);

			this._flex부서별[0, this._flex부서별.Cols["RT_LONG_RCP"].Index] = this.DD("수금율");
			this._flex부서별[0, this._flex부서별.Cols["RT_RCP"].Index] = this.DD("수금율");
			#endregion

			this._flex부서별.SettingVersion = "0.0.0.2";
			this._flex부서별.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			this._flex부서별.SetExceptSumCol(new string[] { "RT_LONG_RCP", "RT_RCP" });
			#endregion

			#region 수금현황
			this._flex수금현황.BeginSetting(2, 1, false);

			this._flex수금현황.SetCol("YYYYMM", "년월", 80);
			this._flex수금현황.SetCol("AM_CLS", "누적매출금액", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

			#region 누적수금액
			this._flex수금현황.SetCol("AM_RCP_OLD", "이전", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex수금현황.SetCol("AM_RCP", "현재", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

			this._flex수금현황[0, this._flex수금현황.Cols["AM_RCP_OLD"].Index] = this.DD("누적수금액");
			this._flex수금현황[0, this._flex수금현황.Cols["AM_RCP"].Index] = this.DD("누적수금액");
			#endregion

			#region 장기미수금액
			this._flex수금현황.SetCol("AM_REMAIN_LONG_OLD", "이전", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex수금현황.SetCol("AM_REMAIN_LONG", "현재", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex수금현황.SetCol("AM_RCP_LONG_MONTH", "수금금액\n(장기미수금)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex수금현황.SetCol("RT_RCP_LONG_MONTH", "수금율\n(장기미수금)", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);

			this._flex수금현황[0, this._flex수금현황.Cols["AM_REMAIN_LONG_OLD"].Index] = this.DD("장기미수금액");
			this._flex수금현황[0, this._flex수금현황.Cols["AM_REMAIN_LONG"].Index] = this.DD("장기미수금액");
			#endregion

			this._flex수금현황.SetCol("RT_REMAIN_LONG", "전월대비\n(장기미수금)", 100, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);

			#region 총미수금액
			this._flex수금현황.SetCol("AM_REMAIN_OLD", "이전", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex수금현황.SetCol("AM_REMAIN", "현재", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex수금현황.SetCol("AM_RCP_MONTH", "수금금액\n(총미수금)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex수금현황.SetCol("RT_RCP_MONTH", "수금율\n(총미수금)", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);

			this._flex수금현황[0, this._flex수금현황.Cols["AM_REMAIN_OLD"].Index] = this.DD("총미수금액");
			this._flex수금현황[0, this._flex수금현황.Cols["AM_REMAIN"].Index] = this.DD("총미수금액");
			#endregion

			this._flex수금현황.SetCol("RT_REMAIN", "전월대비\n(총미수금)", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);

			#region 2개월이하금액
			this._flex수금현황.SetCol("AM_REMAIN_OLD_1", "이전", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex수금현황.SetCol("AM_REMAIN_1", "현재", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

			this._flex수금현황[0, this._flex수금현황.Cols["AM_REMAIN_OLD_1"].Index] = this.DD("2개월이하금액");
			this._flex수금현황[0, this._flex수금현황.Cols["AM_REMAIN_1"].Index] = this.DD("2개월이하금액");
			#endregion

			#region 2개월초과금액
			this._flex수금현황.SetCol("AM_REMAIN_OLD_2", "이전", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex수금현황.SetCol("AM_REMAIN_2", "현재", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

			this._flex수금현황[0, this._flex수금현황.Cols["AM_REMAIN_OLD_2"].Index] = this.DD("2개월초과금액");
			this._flex수금현황[0, this._flex수금현황.Cols["AM_REMAIN_2"].Index] = this.DD("2개월초과금액");
			#endregion

			#region 3개월초과금액
			this._flex수금현황.SetCol("AM_REMAIN_OLD_3", "이전", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex수금현황.SetCol("AM_REMAIN_3", "현재", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

			this._flex수금현황[0, this._flex수금현황.Cols["AM_REMAIN_OLD_3"].Index] = this.DD("3개월초과금액");
			this._flex수금현황[0, this._flex수금현황.Cols["AM_REMAIN_3"].Index] = this.DD("3개월초과금액");
			#endregion

			#region 6개월초과금액
			this._flex수금현황.SetCol("AM_REMAIN_OLD_6", "이전", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex수금현황.SetCol("AM_REMAIN_6", "현재", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

			this._flex수금현황[0, this._flex수금현황.Cols["AM_REMAIN_OLD_6"].Index] = this.DD("6개월초과금액");
			this._flex수금현황[0, this._flex수금현황.Cols["AM_REMAIN_6"].Index] = this.DD("6개월초과금액");
			#endregion

			#region 12개월초과금액
			this._flex수금현황.SetCol("AM_REMAIN_OLD_12", "이전", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex수금현황.SetCol("AM_REMAIN_12", "현재", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

			this._flex수금현황[0, this._flex수금현황.Cols["AM_REMAIN_OLD_12"].Index] = this.DD("12개월초과금액");
			this._flex수금현황[0, this._flex수금현황.Cols["AM_REMAIN_12"].Index] = this.DD("12개월초과금액");
			#endregion

			this._flex수금현황.SettingVersion = "0.0.0.2";
			this._flex수금현황.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			#endregion
        }

        private void InitPivot()
        {
            this._pivot.SetStart();

            this._pivot.AddPivotField("NO_IV", "계산서번호", 100, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("NO_SO", "수주번호", 100, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("NO_PO_PARTNER", "매출처발주번호", 100, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("DT_IV", "매출일자", 100, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("DT_RCP", "수금일자", 100, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("DT_RCP_DAY", "수금일수", 100, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("NM_PARTNER", "매출처", 100, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("NO_IMO", "IMO번호", 100, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("NM_VESSEL", "호선명", 100, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("NM_EMP", "담당자", 100, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("NM_DEPT", "부서", 100, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("NM_SALEGRP", "영업그룹", 100, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("NM_EXCH", "통화명", 100, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("RT_EXCH", "환율", 100, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("DC_RMK1", "커미션비고", 100, true, PivotArea.FilterArea);

            this._pivot.AddPivotField("DT_IV_MONTH", "경과월수", 100, true, PivotArea.RowArea);

            this._pivot.AddPivotField("AM_EX_CLS", "채권외화금액", 100, true, PivotArea.DataArea);
            this._pivot.AddPivotField("AM_CLS", "채권원화금액", 100, true, PivotArea.DataArea);
            this._pivot.AddPivotField("AM_EX_CHARGE", "외화비용", 100, true, PivotArea.DataArea);
            this._pivot.AddPivotField("AM_CHARGE", "원화비용", 100, true, PivotArea.DataArea);
            this._pivot.AddPivotField("AM_EX_NET", "순외화금액", 100, true, PivotArea.DataArea);
            this._pivot.AddPivotField("AM_NET", "순원화금액", 100, true, PivotArea.DataArea);
            this._pivot.AddPivotField("AM_RCP_EX", "수금외화금액", 100, true, PivotArea.DataArea);
            this._pivot.AddPivotField("AM_RCP", "수금원화금액", 100, true, PivotArea.DataArea);
            this._pivot.AddPivotField("AM_EX_REMAIN", "미수외화금액", 100, true, PivotArea.DataArea);
            this._pivot.AddPivotField("AM_REMAIN", "미수원화금액", 100, true, PivotArea.DataArea);
            this._pivot.AddPivotField("AM_EX_2MONTH", "2개월초과(외화)", 100, true, PivotArea.DataArea);
            this._pivot.AddPivotField("AM_2MONTH", "2개월초과(원화)", 100, true, PivotArea.DataArea);
            this._pivot.AddPivotField("AM_EX_3MONTH", "3개월초과(외화)", 100, true, PivotArea.DataArea);
            this._pivot.AddPivotField("AM_3MONTH", "3개월초과(원화)", 100, true, PivotArea.DataArea);
            this._pivot.AddPivotField("AM_EX_6MONTH", "6개월초과(외화)", 100, true, PivotArea.DataArea);
            this._pivot.AddPivotField("AM_6MONTH", "6개월초과(원화)", 100, true, PivotArea.DataArea);
            this._pivot.AddPivotField("AM_EX_12MONTH", "12개월초과(외화)", 100, true, PivotArea.DataArea);
            this._pivot.AddPivotField("AM_12MONTH", "12개월초과(원화)", 100, true, PivotArea.DataArea);

            this._pivot.PivotGridControl.Fields["AM_EX_CLS"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_EX_CLS"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["AM_CLS"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_CLS"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["AM_EX_CHARGE"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_EX_CHARGE"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["AM_CHARGE"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_CHARGE"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["AM_EX_NET"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_EX_NET"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["AM_NET"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_NET"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["AM_RCP_EX"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_RCP_EX"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["AM_RCP"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_RCP"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["AM_EX_REMAIN"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_EX_REMAIN"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["AM_REMAIN"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_REMAIN"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["AM_EX_2MONTH"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_EX_2MONTH"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["AM_2MONTH"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_2MONTH"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["AM_EX_3MONTH"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_EX_3MONTH"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["AM_3MONTH"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_3MONTH"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["AM_EX_6MONTH"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_EX_6MONTH"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["AM_6MONTH"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_6MONTH"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["AM_EX_12MONTH"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_EX_12MONTH"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["AM_12MONTH"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_12MONTH"].CellFormat.FormatString = "#,###,###,###,##0.##";

            this._pivot.SetEnd();
        }

        private void InitEvent()
        {
            this._flex계산서번호H.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex계산서번호_BeforeCodeHelp);
			this._flex계산서번호H.AfterRowChange += _flex계산서번호H_AfterRowChange;

            this.ctx담당자변경.QueryBefore += new BpQueryHandler(this.ctx담당자변경_QueryBefore);
            this.btn담당자변경.Click += new EventHandler(this.btn담당자변경_Click);
        }

        private void _flex계산서번호H_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataSet dataSet = null;

            try
            {
                if (!this.chk계산서상세.Checked) return;

                object[] parameter = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                    this.dtp매출일자.StartDateToString,
                                                    this.dtp매출일자.EndDateToString,
                                                    this.dtp조회기준일.Text,
                                                    this.bpc매출처.QueryWhereIn_Pipe,
                                                    this.ctx부서.CodeValue,
                                                    this.ctx영업그룹.CodeValue,
                                                    this.ctx담당자.CodeValue,
                                                    this.txt프로젝트.Text,
                                                    this.txt수주번호.Text,
                                                    D.GetString(this._flex계산서번호H["NO_IV"]),
                                                    (this.chk미수금0표시.Checked == true ? "Y" : "N"),
                                                    this.nud경과월수.Value };

                dataSet = this._biz.Search5(parameter);

                this._flex계산서번호L.Binding = dataSet.Tables[0];
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override void InitPaint()
		{
			try
			{
				base.InitPaint();

				this.dtp매출일자.StartDate = this.MainFrameInterface.GetDateTimeToday().AddYears(-1);
				this.dtp매출일자.EndDate = this.MainFrameInterface.GetDateTimeToday();

				this.dtp조회기준일.Text = this.MainFrameInterface.GetStringToday;
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			DataSet dataSet;

			try
			{
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!BeforeSearch()) return;

                object[] parameter = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                    this.dtp매출일자.StartDateToString,
                                                    this.dtp매출일자.EndDateToString,
                                                    this.dtp조회기준일.Text,
                                                    this.bpc매출처.QueryWhereIn_Pipe,
                                                    this.ctx부서.CodeValue,
                                                    this.ctx영업그룹.CodeValue,
                                                    this.ctx담당자.CodeValue,
                                                    this.txt프로젝트.Text,
                                                    this.txt수주번호.Text,
                                                    this.txt계산서번호.Text,
                                                    (this.chk미수금0표시.Checked == true ? "Y" : "N"),
                                                    this.nud경과월수.Value };

                if (this.tabControl.SelectedIndex == (int)탭구분.계산서번호)
				{
                    dataSet = this._biz.Search(parameter);

                    this._flex계산서번호H.Binding = dataSet.Tables[0];

					if (!this._flex계산서번호H.HasNormalRow)
						this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
				}
                else if (this.tabControl.SelectedIndex == (int)탭구분.수주번호)
                {
                    dataSet = this._biz.Search1(parameter);

                    this._flex수주번호.Binding = dataSet.Tables[0];

                    if (!this._flex수주번호.HasNormalRow)
                        this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
                else if (this.tabControl.SelectedIndex == (int)탭구분.매출처)
                {
                    dataSet = this._biz.Search2(parameter);

                    this._flex매출처별.Binding = dataSet.Tables[0];

                    if (!this._flex매출처별.HasNormalRow)
                        this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
				else if (this.tabControl.SelectedIndex == (int)탭구분.요약)
				{
                    dataSet = this._biz.Search3(parameter);
                    this._flex부서별.Binding = dataSet.Tables[0];
					this._flex수금현황.Binding = dataSet.Tables[1];

					if (!this._flex부서별.HasNormalRow && !this._flex수금현황.HasNormalRow)
						this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
					else
					{
						decimal 전월장기미수금, 전월총미수금, 당월수금액장기, 당월수금액전체;

						전월장기미수금 = D.GetDecimal(this._flex부서별.DataTable.Compute("SUM(AM_LONG_BEFORE)", string.Empty));
						전월총미수금 = D.GetDecimal(this._flex부서별.DataTable.Compute("SUM(AM_BEFORE)", string.Empty));
						당월수금액장기 = D.GetDecimal(this._flex부서별.DataTable.Compute("SUM(AM_LONG_RCP)", string.Empty));
						당월수금액전체 = D.GetDecimal(this._flex부서별.DataTable.Compute("SUM(AM_RCP)", string.Empty));

						this._flex부서별[this._flex부서별.Rows.Fixed - 1, "RT_LONG_RCP"] = string.Format("{0:" + this._flex부서별.Cols["RT_LONG_RCP"].Format + "}", Decimal.Round(((당월수금액장기 / 전월장기미수금) * 100), 2, MidpointRounding.AwayFromZero));
						this._flex부서별[this._flex부서별.Rows.Fixed - 1, "RT_RCP"] = string.Format("{0:" + this._flex부서별.Cols["RT_RCP"].Format + "}", Decimal.Round(((당월수금액전체 / 전월총미수금) * 100), 2, MidpointRounding.AwayFromZero));
					}
				}
                else if (this.tabControl.SelectedIndex == (int)탭구분.피벗)
                {
                    dataSet = this._biz.Search4(parameter);

                    this._pivot.DataSource = dataSet.Tables[0];

                    if (dataSet.Tables[0] == null || dataSet.Tables[0].Rows.Count == 0)
                        this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
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

                if (!this.BeforeSave() || !this.MsgAndSave(PageActionMode.Save))
					return;

				this.ShowMessage(PageResultMode.SaveGood);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		protected override bool SaveData()
		{
			FlexGrid flex;

			try
			{
				if (!base.SaveData() || !base.Verify()) return false;

				flex = null;

				switch ((탭구분)this.tabControl.SelectedIndex)
				{
                    case 탭구분.계산서번호:
                        flex = this._flex계산서번호H;
                        break;
                    case 탭구분.수주번호:
                        flex = this._flex수주번호;
                        break;
					case 탭구분.매출처:
						flex = this._flex매출처별;
						break;
				}

				if (flex == null || flex.GetChanges() == null) return false;

				if (this._biz.SaveData(flex.GetChanges()))
				{
					flex.AcceptChanges();
				}

				return true;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}

			return false;
		}

        private void _flex계산서번호_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                e.Parameter.P00_CHILD_MODE = "사원도움창";
                e.Parameter.P61_CODE1 = @"ME.NO_EMP AS CODE,
	                                      ME.NM_KOR AS NAME,
	                                      MD.NM_DEPT,
	                                      SG.NM_SALEGRP";
                e.Parameter.P62_CODE2 = @"MA_EMP ME WITH(NOLOCK)
                                          JOIN MA_DEPT MD WITH(NOLOCK) ON MD.CD_COMPANY = ME.CD_COMPANY AND MD.CD_DEPT = ME.CD_DEPT
                                          JOIN (SELECT CD_COMPANY, NO_EMP,
                                          	  	       MAX(CD_SALEGRP) AS CD_SALEGRP 
                                          	    FROM MA_USER WITH(NOLOCK)
                                          	    GROUP BY CD_COMPANY, NO_EMP) MU
                                          ON MU.CD_COMPANY = ME.CD_COMPANY AND MU.NO_EMP = ME.NO_EMP
                                          JOIN MA_SALEGRP SG WITH(NOLOCK) ON SG.CD_COMPANY = MU.CD_COMPANY AND SG.CD_SALEGRP = MU.CD_SALEGRP";
                e.Parameter.P63_CODE3 = @"WHERE ME.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                        @"AND ME.CD_INCOM <> '099'
                                          AND ISNULL(ME.DT_RETIRE, '00000000') = '00000000'";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void ctx담당자변경_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                e.HelpParam.P00_CHILD_MODE = "사원도움창";
                e.HelpParam.P61_CODE1 = @"ME.NO_EMP AS CODE,
	                                      ME.NM_KOR AS NAME,
	                                      MD.NM_DEPT,
	                                      SG.NM_SALEGRP";
                e.HelpParam.P62_CODE2 = @"MA_EMP ME WITH(NOLOCK)
                                          JOIN MA_DEPT MD WITH(NOLOCK) ON MD.CD_COMPANY = ME.CD_COMPANY AND MD.CD_DEPT = ME.CD_DEPT
                                          JOIN (SELECT CD_COMPANY, NO_EMP,
                                          	  	       MAX(CD_SALEGRP) AS CD_SALEGRP 
                                          	    FROM MA_USER WITH(NOLOCK)
                                          	    GROUP BY CD_COMPANY, NO_EMP) MU
                                          ON MU.CD_COMPANY = ME.CD_COMPANY AND MU.NO_EMP = ME.NO_EMP
                                          JOIN MA_SALEGRP SG WITH(NOLOCK) ON SG.CD_COMPANY = MU.CD_COMPANY AND SG.CD_SALEGRP = MU.CD_SALEGRP";
                e.HelpParam.P63_CODE3 = @"WHERE ME.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                        @"AND ME.CD_INCOM <> '099'
                                          AND ISNULL(ME.DT_RETIRE, '00000000') = '00000000'";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn담당자변경_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;

            try
            {
                if (string.IsNullOrEmpty(this.ctx담당자변경.CodeValue.ToString()))
                {
                    this.ShowMessage("변경할 담당자가 지정되어 있지 않습니다.");
                    return;
                }

                dataRowArray = this._flex계산서번호H.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    this._flex계산서번호H.Redraw = false;
                    
                    foreach (DataRow dr in dataRowArray)
                    {
                        dr["NO_EMP"] = this.ctx담당자변경.HelpReturn.Rows[0]["CODE"].ToString();
                        dr["NM_EMP"] = this.ctx담당자변경.HelpReturn.Rows[0]["NAME"].ToString();
                        dr["NM_DEPT"] = this.ctx담당자변경.HelpReturn.Rows[0]["NM_DEPT"].ToString();
                        dr["NM_SALEGRP"] = this.ctx담당자변경.HelpReturn.Rows[0]["NM_SALEGRP"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this._flex계산서번호H.Redraw = true;
            }
        }
	}
}

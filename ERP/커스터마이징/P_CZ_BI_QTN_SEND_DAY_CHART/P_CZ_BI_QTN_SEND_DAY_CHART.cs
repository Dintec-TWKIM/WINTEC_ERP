using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Duzon.Common.Forms;
using ChartFX.WinForms;
using Duzon.ERPU;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using DevExpress.XtraPivotGrid;
using DevExpress.Data.PivotGrid;
using DevExpress.Utils;
using Dintec;

namespace cz
{
    public partial class P_CZ_BI_QTN_SEND_DAY_CHART : PageBase
    {
        P_CZ_BI_QTN_SEND_DAY_CHART_BIZ _biz = new P_CZ_BI_QTN_SEND_DAY_CHART_BIZ();

        public P_CZ_BI_QTN_SEND_DAY_CHART()
        {
			StartUp.Certify(this);
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

            this.dtp대상기간.StartDateToString = Global.MainFrame.GetDateTimeToday().AddMonths(-1).ToString("yyyyMMdd");
            this.dtp대상기간.EndDateToString = Global.MainFrame.GetStringToday;

            this.chart견적소요일.ChartFx.Gallery = Gallery.Lines;
            this.chart견적소요일.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Number;
            this.chart견적소요일.ChartFx.AxisY.LabelsFormat.Decimals = 2;
            this.chart견적소요일.ChartFx.AxisX.AutoScroll = true;
            this.chart견적소요일.ChartFx.LegendBox.Visible = true;

            this.chart고객문의추이.ChartFx.Gallery = Gallery.Lines;
            this.chart고객문의추이.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Number;
            this.chart고객문의추이.ChartFx.AxisX.AutoScroll = true;
            this.chart고객문의추이.ChartFx.LegendBox.Visible = true;

            this.chart고객문의추이.ChartFx.Panes.Add(new Pane());
            this.chart고객문의추이.ChartFx.Panes[1].AxisY.LabelsFormat.Format = AxisFormat.Number;

            this.cbo견적유형.DataSource = MA.GetCodeUser(new string[] { "000", "001", "002" },
                                                         new string[] { "전체",
                                                                        "재고매칭",
                                                                        "견적제출" });
            this.cbo견적유형.ValueMember = "CODE";
            this.cbo견적유형.DisplayMember = "NAME";
        }

        private void InitGrid()
        {
            #region 팀별견적현황
            this._flex팀별견적현황.BeginSetting(2, 1, false);

            this._flex팀별견적현황.SetCol("NM_SALEORG", "부서명", 100);
            this._flex팀별견적현황.SetCol("NM_CC", "팀명", 100);
            this._flex팀별견적현황.SetCol("QT_INQ", "문의건수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex팀별견적현황.SetCol("QT_DONE", "전체", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex팀별견적현황.SetCol("QT_DONE1", "전체1", false);
            this._flex팀별견적현황.SetCol("QT_DONE_DAY", "일평균", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex팀별견적현황.SetCol("QT_ITEM", "전체", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex팀별견적현황.SetCol("QT_ITEM_DAY", "일평균", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex팀별견적현황.SetCol("AM_QTN", "평균견적금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex팀별견적현황.SetCol("DT_QTN", "평균", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flex팀별견적현황.SetCol("QT_2DAY", "2일이내(건)", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex팀별견적현황.SetCol("RT_2DAY", "2일이내(%)", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flex팀별견적현황.SetCol("QT_SO", "전체건수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex팀별견적현황.SetCol("QT_SO_DAY", "일평균건수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex팀별견적현황.SetCol("RT_SO", "%", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flex팀별견적현황.SetCol("QT_NOT_DONE", "건수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex팀별견적현황.SetCol("RT_NOT_DONE", "%", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);

            this._flex팀별견적현황[0, this._flex팀별견적현황.Cols["QT_DONE"].Index] = "견적건수";
            this._flex팀별견적현황[0, this._flex팀별견적현황.Cols["QT_DONE1"].Index] = "견적건수";
            this._flex팀별견적현황[0, this._flex팀별견적현황.Cols["QT_DONE_DAY"].Index] = "견적건수";
            this._flex팀별견적현황[0, this._flex팀별견적현황.Cols["QT_ITEM"].Index] = "견적종수";
            this._flex팀별견적현황[0, this._flex팀별견적현황.Cols["QT_ITEM_DAY"].Index] = "견적종수";
            this._flex팀별견적현황[0, this._flex팀별견적현황.Cols["DT_QTN"].Index] = "견적소요일";
            this._flex팀별견적현황[0, this._flex팀별견적현황.Cols["QT_2DAY"].Index] = "견적소요일";
            this._flex팀별견적현황[0, this._flex팀별견적현황.Cols["RT_2DAY"].Index] = "견적소요일";
            this._flex팀별견적현황[0, this._flex팀별견적현황.Cols["QT_SO"].Index] = "수주";
            this._flex팀별견적현황[0, this._flex팀별견적현황.Cols["QT_SO_DAY"].Index] = "수주";
            this._flex팀별견적현황[0, this._flex팀별견적현황.Cols["RT_SO"].Index] = "수주";
            this._flex팀별견적현황[0, this._flex팀별견적현황.Cols["QT_NOT_DONE"].Index] = "미견적";
            this._flex팀별견적현황[0, this._flex팀별견적현황.Cols["RT_NOT_DONE"].Index] = "미견적";

            this._flex팀별견적현황.SettingVersion = "0.0.0.1";
            this._flex팀별견적현황.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region 담당자별견적현황
            this._flex담당자별견적현황.BeginSetting(2, 1, false);

            this._flex담당자별견적현황.SetCol("NM_SALEORG", "부서명", 100);
            this._flex담당자별견적현황.SetCol("NM_CC", "팀명", 100);
            this._flex담당자별견적현황.SetCol("NM_KOR", "담당자명", 100);
            this._flex담당자별견적현황.SetCol("QT_INQ", "문의건수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex담당자별견적현황.SetCol("QT_DONE", "전체", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex담당자별견적현황.SetCol("QT_DONE1", "전체1", false);
            this._flex담당자별견적현황.SetCol("QT_DONE_DAY", "일평균", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex담당자별견적현황.SetCol("QT_ITEM", "전체", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex담당자별견적현황.SetCol("QT_ITEM_DAY", "일평균", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex담당자별견적현황.SetCol("AM_QTN", "평균견적금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex담당자별견적현황.SetCol("DT_QTN", "평균", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flex담당자별견적현황.SetCol("QT_2DAY", "2일이내(건)", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex담당자별견적현황.SetCol("RT_2DAY", "2일이내(%)", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flex담당자별견적현황.SetCol("QT_SO", "전체건수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex담당자별견적현황.SetCol("QT_SO_DAY", "일평균건수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex담당자별견적현황.SetCol("RT_SO", "%", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flex담당자별견적현황.SetCol("QT_NOT_DONE", "건수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex담당자별견적현황.SetCol("RT_NOT_DONE", "%", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);

            this._flex담당자별견적현황[0, this._flex담당자별견적현황.Cols["QT_DONE"].Index] = "견적건수";
            this._flex담당자별견적현황[0, this._flex담당자별견적현황.Cols["QT_DONE1"].Index] = "견적건수";
            this._flex담당자별견적현황[0, this._flex담당자별견적현황.Cols["QT_DONE_DAY"].Index] = "견적건수";
            this._flex담당자별견적현황[0, this._flex담당자별견적현황.Cols["QT_ITEM"].Index] = "견적종수";
            this._flex담당자별견적현황[0, this._flex담당자별견적현황.Cols["QT_ITEM_DAY"].Index] = "견적종수";
            this._flex담당자별견적현황[0, this._flex담당자별견적현황.Cols["DT_QTN"].Index] = "견적소요일";
            this._flex담당자별견적현황[0, this._flex담당자별견적현황.Cols["QT_2DAY"].Index] = "견적소요일";
            this._flex담당자별견적현황[0, this._flex담당자별견적현황.Cols["RT_2DAY"].Index] = "견적소요일";
            this._flex담당자별견적현황[0, this._flex담당자별견적현황.Cols["QT_SO"].Index] = "수주";
            this._flex담당자별견적현황[0, this._flex담당자별견적현황.Cols["QT_SO_DAY"].Index] = "수주";
            this._flex담당자별견적현황[0, this._flex담당자별견적현황.Cols["RT_SO"].Index] = "수주";
            this._flex담당자별견적현황[0, this._flex담당자별견적현황.Cols["QT_NOT_DONE"].Index] = "미견적";
            this._flex담당자별견적현황[0, this._flex담당자별견적현황.Cols["RT_NOT_DONE"].Index] = "미견적";

            this._flex담당자별견적현황.SettingVersion = "0.0.0.1";
            this._flex담당자별견적현황.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region 매출처별견적현황
            this._flex매출처별견적현황.BeginSetting(2, 1, false);

            this._flex매출처별견적현황.SetCol("LN_PARTNER", "매출처", 100);
            this._flex매출처별견적현황.SetCol("QT_INQ", "문의건수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex매출처별견적현황.SetCol("QT_DONE", "전체", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex매출처별견적현황.SetCol("QT_DONE1", "전체1", false);
            this._flex매출처별견적현황.SetCol("QT_DONE_DAY", "일평균", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex매출처별견적현황.SetCol("QT_ITEM", "전체", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex매출처별견적현황.SetCol("QT_ITEM_DAY", "일평균", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex매출처별견적현황.SetCol("AM_QTN", "평균견적금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매출처별견적현황.SetCol("DT_QTN", "평균", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flex매출처별견적현황.SetCol("QT_2DAY", "2일이내(건)", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex매출처별견적현황.SetCol("RT_2DAY", "2일이내(%)", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flex매출처별견적현황.SetCol("QT_SO", "전체건수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex매출처별견적현황.SetCol("QT_SO_DAY", "일평균건수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex매출처별견적현황.SetCol("RT_SO", "%", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flex매출처별견적현황.SetCol("QT_NOT_DONE", "건수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex매출처별견적현황.SetCol("RT_NOT_DONE", "%", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);

            this._flex매출처별견적현황[0, this._flex매출처별견적현황.Cols["QT_DONE"].Index] = "견적건수";
            this._flex매출처별견적현황[0, this._flex매출처별견적현황.Cols["QT_DONE1"].Index] = "견적건수";
            this._flex매출처별견적현황[0, this._flex매출처별견적현황.Cols["QT_DONE_DAY"].Index] = "견적건수";
            this._flex매출처별견적현황[0, this._flex매출처별견적현황.Cols["QT_ITEM"].Index] = "견적종수";
            this._flex매출처별견적현황[0, this._flex매출처별견적현황.Cols["QT_ITEM_DAY"].Index] = "견적종수";
            this._flex매출처별견적현황[0, this._flex매출처별견적현황.Cols["DT_QTN"].Index] = "견적소요일";
            this._flex매출처별견적현황[0, this._flex매출처별견적현황.Cols["QT_2DAY"].Index] = "견적소요일";
            this._flex매출처별견적현황[0, this._flex매출처별견적현황.Cols["RT_2DAY"].Index] = "견적소요일";
            this._flex매출처별견적현황[0, this._flex매출처별견적현황.Cols["QT_SO"].Index] = "수주";
            this._flex매출처별견적현황[0, this._flex매출처별견적현황.Cols["QT_SO_DAY"].Index] = "수주";
            this._flex매출처별견적현황[0, this._flex매출처별견적현황.Cols["RT_SO"].Index] = "수주";
            this._flex매출처별견적현황[0, this._flex매출처별견적현황.Cols["QT_NOT_DONE"].Index] = "미견적";
            this._flex매출처별견적현황[0, this._flex매출처별견적현황.Cols["RT_NOT_DONE"].Index] = "미견적";

            this._flex매출처별견적현황.SettingVersion = "0.0.0.1";
            this._flex매출처별견적현황.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region Pivot
            this._pivot견적현황.SetStart();

            this._pivot견적현황.AddPivotField("NM_SALEORG", "부서명", 100, true, PivotArea.FilterArea);
            this._pivot견적현황.AddPivotField("NM_CC", "팀명", 100, true, PivotArea.FilterArea);
            this._pivot견적현황.AddPivotField("NM_EMP", "담당자명", 100, true, PivotArea.FilterArea);
            this._pivot견적현황.AddPivotField("NM_EMP_QTN", "견적담당", 100, true, PivotArea.FilterArea);
            this._pivot견적현황.AddPivotField("NM_EMP_STK", "재고담당", 100, true, PivotArea.FilterArea);
            this._pivot견적현황.AddPivotField("NM_SUPPLIER", "매입처", 100, true, PivotArea.FilterArea);
            this._pivot견적현황.AddPivotField("NO_IMO", "IMO 번호", 100, true, PivotArea.FilterArea);
            this._pivot견적현황.AddPivotField("NM_VESSEL", "호선명", 100, true, PivotArea.FilterArea);
            this._pivot견적현황.AddPivotField("NO_KEY", "파일번호", 100, true, PivotArea.FilterArea);
            this._pivot견적현황.AddPivotField("YN_ENGINE", "엔진여부", 100, true, PivotArea.FilterArea);

            this._pivot견적현황.AddPivotField("LN_PARTNER", "매출처", 120, true, PivotArea.RowArea);

            this._pivot견적현황.AddPivotField("QT_FILE_SUPPLIER", "견적건수(매입처)", 110, true, PivotSummaryType.Max, PivotArea.DataArea);
            this._pivot견적현황.AddPivotField("QT_ITEM_SUPPLIER", "견적종수(매입처)", 110, true, PivotSummaryType.Max, PivotArea.DataArea);
            this._pivot견적현황.AddPivotField("QT_INQ", "문의건수", 110, true, PivotArea.DataArea);
            this._pivot견적현황.AddPivotField("QT_DONE", "전체견적건수", 110, true, PivotArea.DataArea);
            this._pivot견적현황.AddPivotField("QT_DONE_DAY", "일평균견적건수", 110, true, PivotSummaryType.Custom, PivotArea.DataArea);
            this._pivot견적현황.AddPivotField("QT_ITEM", "전체견적종수", 110, true, PivotArea.DataArea);
            this._pivot견적현황.AddPivotField("QT_ITEM_DAY", "일평균견적종수", 110, true, PivotSummaryType.Custom, PivotArea.DataArea);
            this._pivot견적현황.AddPivotField("AM_QTN", "견적금액", 110, true, PivotArea.DataArea);
            this._pivot견적현황.AddPivotField("AM_QTN_AVG", "평균견적금액", 110, true, PivotSummaryType.Custom, PivotArea.DataArea);
            this._pivot견적현황.AddPivotField("DT_QTN", "평균견적소요일", 110, true, PivotSummaryType.Average, PivotArea.DataArea);
            this._pivot견적현황.AddPivotField("QT_2DAY", "2일이내(건)", 110, true, PivotArea.DataArea);
            this._pivot견적현황.AddPivotField("RT_2DAY", "2일이내(%)", 110, true, PivotSummaryType.Custom, PivotArea.DataArea);
            this._pivot견적현황.AddPivotField("QT_SO", "전체수주건수", 110, true, PivotArea.DataArea);
            this._pivot견적현황.AddPivotField("QT_SO_DAY", "일평균수주건수", 110, true, PivotSummaryType.Custom, PivotArea.DataArea);
            this._pivot견적현황.AddPivotField("RT_SO", "수주율(%)", 110, true, PivotSummaryType.Custom, PivotArea.DataArea);
            this._pivot견적현황.AddPivotField("QT_NOT_DONE", "미견적건수", 110, true, PivotSummaryType.Custom, PivotArea.DataArea);
            this._pivot견적현황.AddPivotField("RT_NOT_DONE", "미견적율(%)", 110, true, PivotSummaryType.Custom, PivotArea.DataArea);


            this._pivot견적현황.PivotGridControl.Fields["QT_FILE_SUPPLIER"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot견적현황.PivotGridControl.Fields["QT_FILE_SUPPLIER"].CellFormat.FormatString = "0";
            this._pivot견적현황.PivotGridControl.Fields["QT_ITEM_SUPPLIER"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot견적현황.PivotGridControl.Fields["QT_ITEM_SUPPLIER"].CellFormat.FormatString = "0";
            this._pivot견적현황.PivotGridControl.Fields["QT_INQ"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot견적현황.PivotGridControl.Fields["QT_INQ"].CellFormat.FormatString = "0";
            this._pivot견적현황.PivotGridControl.Fields["QT_DONE"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot견적현황.PivotGridControl.Fields["QT_DONE"].CellFormat.FormatString = "0";
            this._pivot견적현황.PivotGridControl.Fields["QT_DONE_DAY"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot견적현황.PivotGridControl.Fields["QT_DONE_DAY"].CellFormat.FormatString = "0";
            this._pivot견적현황.PivotGridControl.Fields["QT_ITEM"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot견적현황.PivotGridControl.Fields["QT_ITEM"].CellFormat.FormatString = "0";
            this._pivot견적현황.PivotGridControl.Fields["QT_ITEM_DAY"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot견적현황.PivotGridControl.Fields["QT_ITEM_DAY"].CellFormat.FormatString = "0";
            this._pivot견적현황.PivotGridControl.Fields["AM_QTN"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot견적현황.PivotGridControl.Fields["AM_QTN"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot견적현황.PivotGridControl.Fields["AM_QTN_AVG"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot견적현황.PivotGridControl.Fields["AM_QTN_AVG"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot견적현황.PivotGridControl.Fields["DT_QTN"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot견적현황.PivotGridControl.Fields["DT_QTN"].CellFormat.FormatString = "0.00";
            this._pivot견적현황.PivotGridControl.Fields["QT_2DAY"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot견적현황.PivotGridControl.Fields["QT_2DAY"].CellFormat.FormatString = "0";
            this._pivot견적현황.PivotGridControl.Fields["RT_2DAY"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot견적현황.PivotGridControl.Fields["RT_2DAY"].CellFormat.FormatString = "#,###,###,###,##0.00%";
            this._pivot견적현황.PivotGridControl.Fields["QT_SO"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot견적현황.PivotGridControl.Fields["QT_SO"].CellFormat.FormatString = "0";
            this._pivot견적현황.PivotGridControl.Fields["QT_SO_DAY"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot견적현황.PivotGridControl.Fields["QT_SO_DAY"].CellFormat.FormatString = "0";
            this._pivot견적현황.PivotGridControl.Fields["RT_SO"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot견적현황.PivotGridControl.Fields["RT_SO"].CellFormat.FormatString = "#,###,###,###,##0.00%";
            this._pivot견적현황.PivotGridControl.Fields["QT_NOT_DONE"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot견적현황.PivotGridControl.Fields["QT_NOT_DONE"].CellFormat.FormatString = "0";
            this._pivot견적현황.PivotGridControl.Fields["RT_NOT_DONE"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot견적현황.PivotGridControl.Fields["RT_NOT_DONE"].CellFormat.FormatString = "#,###,###,###,##0.00%";

            this._pivot견적현황.SetEnd();
            #endregion
        }

        private void InitEvent()
        {
            this._pivot견적현황.PivotGridControl.CustomSummary += new PivotGridCustomSummaryEventHandler(this.PivotGridControl_CustomSummary);
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            DataTable dataTable;

            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!BeforeSearch()) return;

                DataTable dt = DBHelper.GetDataTable(@"SELECT MIN(DT_CAL) AS DT_START,
                                                       	      MAX(DT_CAL) AS DT_END,
                                                       	      SUM(CASE WHEN FG1_HOLIDAY = 'W' THEN 1 ELSE 0 END) AS DAY_WORKING
                                                       FROM MA_CALENDAR WITH(NOLOCK)
                                                       WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
                                                      "AND DT_CAL <= CONVERT(CHAR(8), GETDATE(), 112)" +
                                                      "AND DT_CAL BETWEEN '" + this.dtp대상기간.StartDateToString + "' AND '" + this.dtp대상기간.EndDateToString + "'");

                this.cur근무일수.DecimalValue = D.GetDecimal(dt.Rows[0]["DAY_WORKING"]);

                if (this.tabControl1.SelectedIndex == 0)
                {
                    dataTable = this._biz.Search1(new object[] { Global.MainFrame.LoginInfo.CompanyCode, 
                                                                 this.dtp대상기간.StartDateToString,
                                                                 this.dtp대상기간.EndDateToString,
                                                                 this.cbo견적유형.SelectedValue.ToString() });

                    if (dataTable == null || dataTable.Rows.Count == 0)
                        this.chart견적소요일.ChartFx.Data.Clear();
                    else
                    {
                        this.chart견적소요일.DataSource = dataTable;

                        this.chart견적소요일.ChartFx.Series[0].Text = "전체";
                        this.chart견적소요일.ChartFx.Series[0].PointLabels.Visible = true;
                    }

                    this._flex팀별견적현황.UnBinding = this._biz.Search2(new object[] { Global.MainFrame.LoginInfo.CompanyCode, 
                                                                                        this.dtp대상기간.StartDateToString,
                                                                                        this.dtp대상기간.EndDateToString,
                                                                                        this.cbo견적유형.SelectedValue.ToString() });

                    this.팀별견적현황SubTotal();
                }
                else if (this.tabControl1.SelectedIndex == 1)
                {
                    this._flex담당자별견적현황.UnBinding = this._biz.Search3(new object[] { Global.MainFrame.LoginInfo.CompanyCode, 
                                                                                            this.dtp대상기간.StartDateToString,
                                                                                            this.dtp대상기간.EndDateToString,
                                                                                            this.cbo견적유형.SelectedValue.ToString() });

                    this.담당자별견적현황SubTotal();
                }
                else if (this.tabControl1.SelectedIndex == 2)
                {
                    this._flex매출처별견적현황.UnBinding = this._biz.Search5(new object[] { Global.MainFrame.LoginInfo.CompanyCode, 
                                                                                            this.dtp대상기간.StartDateToString,
                                                                                            this.dtp대상기간.EndDateToString,
                                                                                            this.cbo견적유형.SelectedValue.ToString() });

                    this.매출처별견적현황SubTotal();
                }
                else if (this.tabControl1.SelectedIndex == 3)
                {
                    dataTable = this._biz.Search4(new object[] { Global.MainFrame.LoginInfo.CompanyCode, 
                                                                 this.dtp대상기간.StartDateToString,
                                                                 this.dtp대상기간.EndDateToString,
                                                                 this.cbo견적유형.SelectedValue.ToString() });

                    if (dataTable == null || dataTable.Rows.Count == 0)
                        this.chart고객문의추이.ChartFx.Data.Clear();
                    else
                    {
                        this.chart고객문의추이.DataSource = dataTable;

                        this.chart고객문의추이.ChartFx.Series[0].Text = "전체 건수";
                        this.chart고객문의추이.ChartFx.Series[1].Text = "전체 종수";

                        this.chart고객문의추이.ChartFx.Series[0].PointLabels.Visible = true;
                        this.chart고객문의추이.ChartFx.Series[1].PointLabels.Visible = true;

                        this.chart고객문의추이.ChartFx.Series[1].Pane = this.chart고객문의추이.ChartFx.Panes[1];
                    }
                }
                else if (this.tabControl1.SelectedIndex == 4)
                {
                    dataTable = this._biz.Search6(new object[] { Global.MainFrame.LoginInfo.CompanyCode, 
                                                                 this.dtp대상기간.StartDateToString,
                                                                 this.dtp대상기간.EndDateToString,
                                                                 this.cbo견적유형.SelectedValue.ToString() });

                    dataTable.TableName = this.PageID;

                    this._pivot견적현황.DataSource = dataTable;
                }
            }
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
        }

        private void 팀별견적현황SubTotal()
        {
            SubTotals subTotals;
            SubTotal subTotal;
            CellRange cellRange;

            try
            {
                if (!this._flex팀별견적현황.HasNormalRow) return;

                this._flex팀별견적현황.Redraw = false;

                subTotals = new SubTotals();

                subTotals.FirstRow = this._flex팀별견적현황.Rows.Fixed;

                subTotal = subTotals.NewTotal();
                subTotal.Type = TotalEnum.SubTotal;
                subTotal.GroupCol = this._flex팀별견적현황.Cols["NM_SALEORG"].Index;
                subTotal.TotalColName = new string[] { "QT_INQ", "QT_DONE", "QT_DONE1", "QT_ITEM", "AM_QTN", "DT_QTN", "QT_2DAY", "QT_SO" };
                subTotals.Add(subTotal);

                subTotal = subTotals.NewTotal();
                subTotal.Type = TotalEnum.GrandTotal;
                subTotal.TotalColName = new string[] { "QT_INQ", "QT_DONE", "QT_DONE1", "QT_ITEM", "AM_QTN", "DT_QTN", "QT_2DAY", "QT_SO" };
                subTotals.Add(subTotal);

                this._flex팀별견적현황.DoSubTotal(subTotals);

                for (int i = this._flex팀별견적현황.Rows.Fixed; i < this._flex팀별견적현황.Rows.Count; i++)
                {
                    if (this._flex팀별견적현황.Rows[i].IsNode)
                    {
                        switch (D.GetString(this._flex팀별견적현황.Rows[i].UserData).Substring(0, 6))
                        {
                            case "XXSUB1":
                                this._flex팀별견적현황[i, "NM_SALEORG"] += "소계";
                                break;
                            case "XXGRA0":
                                this._flex팀별견적현황[i, "NM_SALEORG"] = "총계";
                                break;
                        }
                    }
                    else
                    {
                        cellRange = this._flex팀별견적현황.GetCellRange(i, "NM_SALEORG", i, "NM_SALEORG");
                        cellRange.UserData = this._flex팀별견적현황[i, "NM_SALEORG"].ToString();
                    }

                    if (this.cur근무일수.DecimalValue == 0)
                    {
                        this._flex팀별견적현황[i, "QT_DONE_DAY"] = 0;
                        this._flex팀별견적현황[i, "QT_ITEM_DAY"] = 0;
                        this._flex팀별견적현황[i, "QT_SO_DAY"] = 0;
                    }
                    else
                    {
                        this._flex팀별견적현황[i, "QT_DONE_DAY"] = Math.Round(D.GetDecimal(this._flex팀별견적현황[i, "QT_DONE"]) / this.cur근무일수.DecimalValue, MidpointRounding.AwayFromZero);
                        this._flex팀별견적현황[i, "QT_ITEM_DAY"] = Math.Round(D.GetDecimal(this._flex팀별견적현황[i, "QT_ITEM"]) / this.cur근무일수.DecimalValue, MidpointRounding.AwayFromZero);
                        this._flex팀별견적현황[i, "QT_SO_DAY"] = Math.Round(D.GetDecimal(this._flex팀별견적현황[i, "QT_SO"]) / this.cur근무일수.DecimalValue, MidpointRounding.AwayFromZero);
                    }

                    if (D.GetDecimal(this._flex팀별견적현황[i, "QT_DONE"]) == 0)
                    {
                        this._flex팀별견적현황[i, "AM_QTN"] = 0;
                        this._flex팀별견적현황[i, "RT_2DAY"] = 0;
                        this._flex팀별견적현황[i, "RT_SO"] = 0;
                    }
                    else
                    {
                        this._flex팀별견적현황[i, "AM_QTN"] = decimal.Round((D.GetDecimal(this._flex팀별견적현황[i, "AM_QTN"]) / D.GetDecimal(this._flex팀별견적현황[i, "QT_DONE"])), 0, MidpointRounding.AwayFromZero);
                        this._flex팀별견적현황[i, "RT_2DAY"] = decimal.Round((D.GetDecimal(this._flex팀별견적현황[i, "QT_2DAY"]) / D.GetDecimal(this._flex팀별견적현황[i, "QT_DONE"])) * 100, 2, MidpointRounding.AwayFromZero);
                        this._flex팀별견적현황[i, "RT_SO"] = decimal.Round((D.GetDecimal(this._flex팀별견적현황[i, "QT_SO"]) / D.GetDecimal(this._flex팀별견적현황[i, "QT_DONE"])) * 100, 2, MidpointRounding.AwayFromZero);
                    }

                    if (D.GetDecimal(this._flex팀별견적현황[i, "QT_DONE1"]) == 0)
                        this._flex팀별견적현황[i, "DT_QTN"] = 0;
                    else
                        this._flex팀별견적현황[i, "DT_QTN"] = decimal.Round((D.GetDecimal(this._flex팀별견적현황[i, "DT_QTN"]) / D.GetDecimal(this._flex팀별견적현황[i, "QT_DONE1"])), 2, MidpointRounding.AwayFromZero);

                    this._flex팀별견적현황[i, "QT_NOT_DONE"] = (D.GetDecimal(this._flex팀별견적현황[i, "QT_INQ"]) - D.GetDecimal(this._flex팀별견적현황[i, "QT_DONE"]));

                    if (D.GetDecimal(this._flex팀별견적현황[i, "QT_INQ"]) == 0)
                        this._flex팀별견적현황[i, "RT_NOT_DONE"] = 0;
                    else
                        this._flex팀별견적현황[i, "RT_NOT_DONE"] = decimal.Round((D.GetDecimal(this._flex팀별견적현황[i, "QT_NOT_DONE"]) / D.GetDecimal(this._flex팀별견적현황[i, "QT_INQ"])) * 100, 2, MidpointRounding.AwayFromZero);
                }

                this._flex팀별견적현황.DoMerge();

                this._flex팀별견적현황.Redraw = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void 담당자별견적현황SubTotal()
        {
            SubTotals subTotals;
            SubTotal subTotal;
            CellRange cellRange;

            try
            {
                if (!this._flex담당자별견적현황.HasNormalRow) return;

                this._flex담당자별견적현황.Redraw = false;

                subTotals = new SubTotals();

                subTotals.FirstRow = this._flex담당자별견적현황.Rows.Fixed;

                subTotal = subTotals.NewTotal();
                subTotal.Type = TotalEnum.SubTotal;
                subTotal.GroupCol = this._flex담당자별견적현황.Cols["NM_CC"].Index;
                subTotal.TotalColName = new string[] { "QT_INQ", "QT_DONE", "QT_DONE1", "QT_ITEM", "AM_QTN", "DT_QTN", "QT_2DAY", "QT_SO" };
                subTotals.Add(subTotal);

                subTotal = subTotals.NewTotal();
                subTotal.Type = TotalEnum.SubTotal;
                subTotal.GroupCol = this._flex담당자별견적현황.Cols["NM_SALEORG"].Index;
                subTotal.TotalColName = new string[] { "QT_INQ", "QT_DONE", "QT_DONE1", "QT_ITEM", "AM_QTN", "DT_QTN", "QT_2DAY", "QT_SO" };
                subTotals.Add(subTotal);

                subTotal = subTotals.NewTotal();
                subTotal.Type = TotalEnum.GrandTotal;
                subTotal.TotalColName = new string[] { "QT_INQ", "QT_DONE", "QT_DONE1", "QT_ITEM", "AM_QTN", "DT_QTN", "QT_2DAY", "QT_SO" };
                subTotals.Add(subTotal);

                this._flex담당자별견적현황.DoSubTotal(subTotals);

                for (int i = this._flex담당자별견적현황.Rows.Fixed; i < this._flex담당자별견적현황.Rows.Count; i++)
                {
                    if (this._flex담당자별견적현황.Rows[i].IsNode)
                    {
                        switch (D.GetString(this._flex담당자별견적현황.Rows[i].UserData).Substring(0, 6))
                        {
                            case "XXSUB1":
                                this._flex담당자별견적현황[i, "NM_SALEORG"] += "소계";
                                break;
                            case "XXSUB2":
                                this._flex담당자별견적현황[i, "NM_SALEORG"] += "소계";
                                break;
                            case "XXGRA0":
                                this._flex담당자별견적현황[i, "NM_SALEORG"] = "총계";
                                break;
                        }
                    }
                    else
                    {
                        cellRange = this._flex담당자별견적현황.GetCellRange(i, "NM_SALEORG", i, "NM_SALEORG");
                        cellRange.UserData = this._flex담당자별견적현황[i, "NM_SALEORG"].ToString();

                        cellRange = this._flex담당자별견적현황.GetCellRange(i, "NM_CC", i, "NM_CC");
                        cellRange.UserData = this._flex담당자별견적현황[i, "NM_CC"].ToString();
                    }

                    if (this.cur근무일수.DecimalValue == 0)
                    {
                        this._flex담당자별견적현황[i, "QT_DONE_DAY"] = 0;
                        this._flex담당자별견적현황[i, "QT_ITEM_DAY"] = 0;
                        this._flex담당자별견적현황[i, "QT_SO_DAY"] = 0;
                    }
                    else
                    {
                        this._flex담당자별견적현황[i, "QT_DONE_DAY"] = Math.Round(D.GetDecimal(this._flex담당자별견적현황[i, "QT_DONE"]) / this.cur근무일수.DecimalValue, MidpointRounding.AwayFromZero);
                        this._flex담당자별견적현황[i, "QT_ITEM_DAY"] = Math.Round(D.GetDecimal(this._flex담당자별견적현황[i, "QT_ITEM"]) / this.cur근무일수.DecimalValue, MidpointRounding.AwayFromZero);
                        this._flex담당자별견적현황[i, "QT_SO_DAY"] = Math.Round(D.GetDecimal(this._flex담당자별견적현황[i, "QT_SO"]) / this.cur근무일수.DecimalValue, MidpointRounding.AwayFromZero);
                    }

                    if (D.GetDecimal(this._flex담당자별견적현황[i, "QT_DONE"]) == 0)
                    {
                        this._flex담당자별견적현황[i, "AM_QTN"] = 0;
                        this._flex담당자별견적현황[i, "RT_2DAY"] = 0;
                        this._flex담당자별견적현황[i, "RT_SO"] = 0;
                    }
                    else
                    {
                        this._flex담당자별견적현황[i, "AM_QTN"] = decimal.Round((D.GetDecimal(this._flex담당자별견적현황[i, "AM_QTN"]) / D.GetDecimal(this._flex담당자별견적현황[i, "QT_DONE"])), 0, MidpointRounding.AwayFromZero);
                        this._flex담당자별견적현황[i, "RT_2DAY"] = decimal.Round((D.GetDecimal(this._flex담당자별견적현황[i, "QT_2DAY"]) / D.GetDecimal(this._flex담당자별견적현황[i, "QT_DONE"])) * 100, 2, MidpointRounding.AwayFromZero);
                        this._flex담당자별견적현황[i, "RT_SO"] = decimal.Round((D.GetDecimal(this._flex담당자별견적현황[i, "QT_SO"]) / D.GetDecimal(this._flex담당자별견적현황[i, "QT_DONE"])) * 100, 2, MidpointRounding.AwayFromZero);
                    }

                    if (D.GetDecimal(this._flex담당자별견적현황[i, "QT_DONE1"]) == 0)
                        this._flex담당자별견적현황[i, "DT_QTN"] = 0;
                    else
                        this._flex담당자별견적현황[i, "DT_QTN"] = decimal.Round((D.GetDecimal(this._flex담당자별견적현황[i, "DT_QTN"]) / D.GetDecimal(this._flex담당자별견적현황[i, "QT_DONE1"])), 2, MidpointRounding.AwayFromZero);

                    this._flex담당자별견적현황[i, "QT_NOT_DONE"] = (D.GetDecimal(this._flex담당자별견적현황[i, "QT_INQ"]) - D.GetDecimal(this._flex담당자별견적현황[i, "QT_DONE"]));

                    if (D.GetDecimal(this._flex담당자별견적현황[i, "QT_INQ"]) == 0)
                        this._flex담당자별견적현황[i, "RT_NOT_DONE"] = 0;
                    else
                        this._flex담당자별견적현황[i, "RT_NOT_DONE"] = decimal.Round((D.GetDecimal(this._flex담당자별견적현황[i, "QT_NOT_DONE"]) / D.GetDecimal(this._flex담당자별견적현황[i, "QT_INQ"])) * 100, 2, MidpointRounding.AwayFromZero);
                }

                this._flex담당자별견적현황.DoMerge();

                this._flex담당자별견적현황.Redraw = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void 매출처별견적현황SubTotal()
        {
            SubTotals subTotals;
            SubTotal subTotal;
            CellRange cellRange;

            try
            {
                if (!this._flex매출처별견적현황.HasNormalRow) return;

                this._flex매출처별견적현황.Redraw = false;

                subTotals = new SubTotals();

                subTotals.FirstRow = this._flex매출처별견적현황.Rows.Fixed;

                subTotal = subTotals.NewTotal();
                subTotal.Type = TotalEnum.GrandTotal;
                subTotal.TotalColName = new string[] { "QT_INQ", "QT_DONE", "QT_DONE1", "QT_ITEM", "AM_QTN", "DT_QTN", "QT_2DAY", "QT_SO" };
                subTotals.Add(subTotal);

                this._flex매출처별견적현황.DoSubTotal(subTotals);

                for (int i = this._flex매출처별견적현황.Rows.Fixed; i < this._flex매출처별견적현황.Rows.Count; i++)
                {
                    if (this._flex매출처별견적현황.Rows[i].IsNode)
                    {
                        switch (D.GetString(this._flex매출처별견적현황.Rows[i].UserData).Substring(0, 6))
                        {
                            case "XXGRA0":
                                this._flex매출처별견적현황[i, "LN_PARTNER"] = "총계";
                                break;
                        }
                    }
                    else
                    {
                        cellRange = this._flex매출처별견적현황.GetCellRange(i, "LN_PARTNER", i, "LN_PARTNER");
                        cellRange.UserData = this._flex매출처별견적현황[i, "LN_PARTNER"].ToString();
                    }

                    if (this.cur근무일수.DecimalValue == 0)
                    {
                        this._flex매출처별견적현황[i, "QT_DONE_DAY"] = 0;
                        this._flex매출처별견적현황[i, "QT_ITEM_DAY"] = 0;
                        this._flex매출처별견적현황[i, "QT_SO_DAY"] = 0;
                    }
                    else
                    {
                        this._flex매출처별견적현황[i, "QT_DONE_DAY"] = Math.Round(D.GetDecimal(this._flex매출처별견적현황[i, "QT_DONE"]) / this.cur근무일수.DecimalValue, MidpointRounding.AwayFromZero);
                        this._flex매출처별견적현황[i, "QT_ITEM_DAY"] = Math.Round(D.GetDecimal(this._flex매출처별견적현황[i, "QT_ITEM"]) / this.cur근무일수.DecimalValue, MidpointRounding.AwayFromZero);
                        this._flex매출처별견적현황[i, "QT_SO_DAY"] = Math.Round(D.GetDecimal(this._flex매출처별견적현황[i, "QT_SO"]) / this.cur근무일수.DecimalValue, MidpointRounding.AwayFromZero);
                    }

                    if (D.GetDecimal(this._flex매출처별견적현황[i, "QT_DONE"]) == 0)
                    {
                        this._flex매출처별견적현황[i, "AM_QTN"] = 0;
                        this._flex매출처별견적현황[i, "RT_2DAY"] = 0;
                        this._flex매출처별견적현황[i, "RT_SO"] = 0;
                    }
                    else
                    {
                        this._flex매출처별견적현황[i, "AM_QTN"] = decimal.Round((D.GetDecimal(this._flex매출처별견적현황[i, "AM_QTN"]) / D.GetDecimal(this._flex매출처별견적현황[i, "QT_DONE"])), 0, MidpointRounding.AwayFromZero);
                        this._flex매출처별견적현황[i, "RT_2DAY"] = decimal.Round((D.GetDecimal(this._flex매출처별견적현황[i, "QT_2DAY"]) / D.GetDecimal(this._flex매출처별견적현황[i, "QT_DONE"])) * 100, 2, MidpointRounding.AwayFromZero);
                        this._flex매출처별견적현황[i, "RT_SO"] = decimal.Round((D.GetDecimal(this._flex매출처별견적현황[i, "QT_SO"]) / D.GetDecimal(this._flex매출처별견적현황[i, "QT_DONE"])) * 100, 2, MidpointRounding.AwayFromZero);
                    }

                    if (D.GetDecimal(this._flex매출처별견적현황[i, "QT_DONE1"]) == 0)
                        this._flex매출처별견적현황[i, "DT_QTN"] = 0;
                    else
                        this._flex매출처별견적현황[i, "DT_QTN"] = decimal.Round((D.GetDecimal(this._flex매출처별견적현황[i, "DT_QTN"]) / D.GetDecimal(this._flex매출처별견적현황[i, "QT_DONE1"])), 2, MidpointRounding.AwayFromZero);

                    this._flex매출처별견적현황[i, "QT_NOT_DONE"] = (D.GetDecimal(this._flex매출처별견적현황[i, "QT_INQ"]) - D.GetDecimal(this._flex매출처별견적현황[i, "QT_DONE"]));

                    if (D.GetDecimal(this._flex매출처별견적현황[i, "QT_INQ"]) == 0)
                        this._flex매출처별견적현황[i, "RT_NOT_DONE"] = 0;
                    else
                        this._flex매출처별견적현황[i, "RT_NOT_DONE"] = decimal.Round((D.GetDecimal(this._flex매출처별견적현황[i, "QT_NOT_DONE"]) / D.GetDecimal(this._flex매출처별견적현황[i, "QT_INQ"])) * 100, 2, MidpointRounding.AwayFromZero);
                }

                this._flex매출처별견적현황.DoMerge();

                this._flex매출처별견적현황.Redraw = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void PivotGridControl_CustomSummary(object sender, PivotGridCustomSummaryEventArgs e)
        {
            PivotDrillDownDataSource drillDownDataSource;
            decimal num1, num2;

            try
            {
                drillDownDataSource = e.CreateDrillDownDataSource();
                num1 = 0;
                num2 = 0;

                for (int index = 0; index < drillDownDataSource.RowCount; ++index)
                {
                    switch (e.DataField.FieldName)
                    {
                        case "QT_DONE_DAY":
                            num1 += D.GetDecimal(drillDownDataSource[index]["QT_DONE"]);
                            break;
                        case "QT_ITEM_DAY":
                            num1 += D.GetDecimal(drillDownDataSource[index]["QT_ITEM"]);
                            break;
                        case "QT_SO_DAY":
                            num1 += D.GetDecimal(drillDownDataSource[index]["QT_SO"]);
                            break;
                        case "AM_QTN_AVG":
                            num1 += D.GetDecimal(drillDownDataSource[index]["AM_QTN"]);
                            num2 += D.GetDecimal(drillDownDataSource[index]["QT_DONE"]);
                            break;
                        case "RT_2DAY":
                            num1 += D.GetDecimal(drillDownDataSource[index]["QT_2DAY"]);
                            num2 += D.GetDecimal(drillDownDataSource[index]["QT_DONE"]);
                            break;
                        case "RT_SO":
                            num1 += D.GetDecimal(drillDownDataSource[index]["QT_SO"]);
                            num2 += D.GetDecimal(drillDownDataSource[index]["QT_DONE"]);
                            break;
                        case "DT_QTN":
                            num1 += D.GetDecimal(drillDownDataSource[index]["DT_QTN"]);
                            num2 += D.GetDecimal(drillDownDataSource[index]["QT_DONE1"]);
                            break;
                        case "QT_NOT_DONE":
                            num1 += D.GetDecimal(drillDownDataSource[index]["QT_INQ"]);
                            num2 += D.GetDecimal(drillDownDataSource[index]["QT_DONE"]);
                            break;
                        case "RT_NOT_DONE":
                            num1 += D.GetDecimal(drillDownDataSource[index]["QT_INQ"]);
                            num2 += D.GetDecimal(drillDownDataSource[index]["QT_DONE"]);
                            break;
                    }
                }

                switch (e.DataField.FieldName)
                {
                    case "QT_DONE_DAY":
                    case "QT_ITEM_DAY":
                    case "QT_SO_DAY":
                        if (this.cur근무일수.DecimalValue == 0)
                            e.CustomValue = 0;
                        else
                            e.CustomValue = Math.Round(num1 / this.cur근무일수.DecimalValue, MidpointRounding.AwayFromZero);
                        break;
                    case "AM_QTN_AVG":
                        if (num2 == 0)
                            e.CustomValue = 0;
                        else
                            e.CustomValue = decimal.Round(num1 / num2, 0, MidpointRounding.AwayFromZero);
                        break;
                    case "RT_2DAY":
                    case "RT_SO":
                    case "DT_QTN":
                        if (num2 == 0)
                            e.CustomValue = 0;
                        else
                            e.CustomValue = decimal.Round(num1 / num2, 2, MidpointRounding.AwayFromZero);
                        break;
                    case "QT_NOT_DONE":
                        e.CustomValue = (num1 - num2);
                        break;
                    case "RT_NOT_DONE":
                        if (num1 == 0)
                            e.CustomValue = 0;
                        else
                            e.CustomValue = decimal.Round((num1 - num2) / num1, 2, MidpointRounding.AwayFromZero);
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
    }
}

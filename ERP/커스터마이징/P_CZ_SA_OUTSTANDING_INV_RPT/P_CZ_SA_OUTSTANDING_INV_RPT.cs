using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Dintec;
using DevExpress.XtraPivotGrid;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.ERPU;
using DevExpress.Utils;
using ChartFX.WinForms;

namespace cz
{
    public partial class P_CZ_SA_OUTSTANDING_INV_RPT : PageBase
    {
        P_CZ_SA_OUTSTANDING_INV_RPT_BIZ _biz = new P_CZ_SA_OUTSTANDING_INV_RPT_BIZ();

        public P_CZ_SA_OUTSTANDING_INV_RPT()
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

            this.dtp기준년월.Text = this.MainFrameInterface.GetDateTimeToday().AddMonths(-1).ToString("yyyyMM");

            #region 차트

            #region 미수금
            this.chart미수금.ChartFx.Titles.Add(new TitleDockable("월별미수금") { Alignment = StringAlignment.Center, Font = new Font("굴림체", 12, FontStyle.Bold) });
            this.chart미수금.ChartFx.Gallery = Gallery.Lines;
            this.chart미수금.ChartFx.LegendBox.Visible = true;

            this.chart미수금.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Currency;
            this.chart미수금.ChartFx.AxisX.AutoScroll = true;
            #endregion

            #region 회수율
            this.chart회수율.ChartFx.Titles.Add(new TitleDockable("월별회수율") { Alignment = StringAlignment.Center, Font = new Font("굴림체", 12, FontStyle.Bold) });
            this.chart회수율.ChartFx.Gallery = Gallery.Lines;
            this.chart회수율.ChartFx.LegendBox.Visible = true;

            this.chart회수율.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Currency;
            this.chart회수율.ChartFx.AxisX.AutoScroll = true;

            this.chart회수율.ChartFx.Panes.Add(new Pane());
            this.chart회수율.ChartFx.Panes[1].AxisY.LabelsFormat.Decimals = 2;
            #endregion

            #endregion
        }

        private void InitEvent()
        {
            this._flex미수금현황.AfterRowChange += new RangeEventHandler(this._flex미수금현황_AfterRowChange);
            this._flex매출리스트.AfterRowChange += new RangeEventHandler(this._flex매출리스트_AfterRowChange);

            this._flex매출리스트.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
            this._flex수금리스트.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
        }

        private void InitGrid()
        {
            #region Pivot
            this._pivot미수금현황.SetStart();

            this._pivot미수금현황.AddPivotField("LN_PARTNER", "매출처명", 120, true, PivotArea.RowArea);

            this._pivot미수금현황.AddPivotField("AM_IV", "총매출금액", 100, true, PivotArea.DataArea);
            this._pivot미수금현황.AddPivotField("AM_RCP", "총수금금액", 100, true, PivotArea.DataArea);
            this._pivot미수금현황.AddPivotField("AM_REMAIN", "당월미수금액", 100, true, PivotArea.DataArea);
            this._pivot미수금현황.AddPivotField("AM_REMAIN_BEFORE", "전월미수금액", 100, true, PivotArea.DataArea);
            this._pivot미수금현황.AddPivotField("AM_REMAIN_DIFF", "미수금액증감", 100, true, PivotArea.DataArea);
            this._pivot미수금현황.AddPivotField("QT_REMAIN", "미수금건수", 100, true, PivotArea.DataArea);
            this._pivot미수금현황.AddPivotField("AM_IV_MONTH", "당월매출금액", 100, true, PivotArea.DataArea);
            this._pivot미수금현황.AddPivotField("QT_IV_MONTH", "당월매출건수", 100, true, PivotArea.DataArea);
            this._pivot미수금현황.AddPivotField("AM_RCP_MONTH", "당월수금금액", 100, true, PivotArea.DataArea);
            this._pivot미수금현황.AddPivotField("QT_RCP_MONTH", "당월수금건수", 100, true, PivotArea.DataArea);
            this._pivot미수금현황.AddPivotField("DT_RETURN", "평균회수일수", 100, true, PivotArea.DataArea);
            this._pivot미수금현황.AddPivotField("DT_REMAIN", "평균경과일수", 100, true, PivotArea.DataArea);
            this._pivot미수금현황.AddPivotField("RT_RETURN", "회수율", 100, true, PivotArea.DataArea);

            this._pivot미수금현황.PivotGridControl.Fields["AM_IV"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot미수금현황.PivotGridControl.Fields["AM_IV"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot미수금현황.PivotGridControl.Fields["AM_RCP"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot미수금현황.PivotGridControl.Fields["AM_RCP"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot미수금현황.PivotGridControl.Fields["AM_REMAIN"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot미수금현황.PivotGridControl.Fields["AM_REMAIN"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot미수금현황.PivotGridControl.Fields["AM_REMAIN_BEFORE"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot미수금현황.PivotGridControl.Fields["AM_REMAIN_BEFORE"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot미수금현황.PivotGridControl.Fields["AM_REMAIN_DIFF"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot미수금현황.PivotGridControl.Fields["AM_REMAIN_DIFF"].CellFormat.FormatString = "#,###,###,###,##0.##";

            this._pivot미수금현황.PivotGridControl.Fields["AM_IV_MONTH"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot미수금현황.PivotGridControl.Fields["AM_IV_MONTH"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot미수금현황.PivotGridControl.Fields["AM_RCP_MONTH"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot미수금현황.PivotGridControl.Fields["AM_RCP_MONTH"].CellFormat.FormatString = "#,###,###,###,##0.##";

            this._pivot미수금현황.PivotGridControl.Fields["DT_RETURN"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot미수금현황.PivotGridControl.Fields["DT_RETURN"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot미수금현황.PivotGridControl.Fields["DT_REMAIN"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot미수금현황.PivotGridControl.Fields["DT_REMAIN"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot미수금현황.PivotGridControl.Fields["RT_RETURN"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot미수금현황.PivotGridControl.Fields["RT_RETURN"].CellFormat.FormatString = "#,###,###,###,##0.##";

            this._pivot미수금현황.SetEnd();
            #endregion

            #region List
            this._flex미수금현황.DetailGrids = new FlexGrid[] { this._flex매출리스트 };
            this._flex매출리스트.DetailGrids = new FlexGrid[] { this._flex수금리스트 };

            #region 미수금현황
            this._flex미수금현황.BeginSetting(1, 1, false);

            this._flex미수금현황.SetCol("CD_PARTNER", "매출처코드", 100);
            this._flex미수금현황.SetCol("LN_PARTNER", "매출처명", 120);
            this._flex미수금현황.SetCol("AM_IV", "총매출금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex미수금현황.SetCol("AM_RCP", "총수금금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex미수금현황.SetCol("AM_REMAIN", "미수금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex미수금현황.SetCol("AM_REMAIN_BEFORE", "전월미수금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex미수금현황.SetCol("AM_REMAIN_DIFF", "미수금액증감", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex미수금현황.SetCol("QT_REMAIN", "미수금건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex미수금현황.SetCol("AM_IV_MONTH", "당월매출금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex미수금현황.SetCol("QT_IV_MONTH", "당월매출건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex미수금현황.SetCol("AM_RCP_MONTH", "당월수금금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex미수금현황.SetCol("QT_RCP_MONTH", "당월수금건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex미수금현황.SetCol("DT_RETURN", "평균회수일수", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex미수금현황.SetCol("DT_REMAIN", "평균경과일수", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex미수금현황.SetCol("RT_RETURN", "회수율", 100, false, typeof(decimal), FormatTpType.RATE);

            this._flex미수금현황.SettingVersion = "0.0.0.1";
            this._flex미수금현황.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex미수금현황.SetExceptSumCol("DT_RETURN", "DT_REMAIN", "RT_RETURN");
            #endregion

            #region 매출리스트
            this._flex매출리스트.BeginSetting(1, 1, false);

            this._flex매출리스트.SetCol("NO_IV", "계산서번호", 100);
            this._flex매출리스트.SetCol("DT_PROCESS", "매출일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex매출리스트.SetCol("DT_RCP", "수금일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex매출리스트.SetCol("AM_IV", "매출금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매출리스트.SetCol("AM_RCP", "수금금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매출리스트.SetCol("AM_REMAIN", "미수금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매출리스트.SetCol("DT_RETURN", "회수일수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex매출리스트.SetCol("DT_REMAIN", "경과일수", 100, false, typeof(decimal), FormatTpType.QUANTITY);

            this._flex매출리스트.SettingVersion = "0.0.0.1";
            this._flex매출리스트.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex매출리스트.SetExceptSumCol("DT_RETURN", "DT_REMAIN");
            #endregion

            #region 수금리스트
            this._flex수금리스트.BeginSetting(1, 1, false);

            this._flex수금리스트.SetCol("NO_RCP", "수금번호", 100);
            this._flex수금리스트.SetCol("DT_RCP", "수금일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex수금리스트.SetCol("AM_RCP_A", "선수금금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex수금리스트.SetCol("AM_RCP", "수금금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flex수금리스트.SettingVersion = "0.0.0.1";
            this._flex수금리스트.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion
            
            #endregion
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!BeforeSearch()) return;

                dt = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                     this.dtp기준년월.Text,
                                                     D.GetString(this.ctx매출처.CodeValue) });

                dt.TableName = this.PageID;
                this._pivot미수금현황.DataSource = dt;
                this._flex미수금현황.Binding = dt;

                if (dt == null || dt.Rows.Count == 0)
                {
                    ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                    return;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex미수금현황_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt, dt1;
            string key, filter;

            try
            {
                dt = null;
                key = D.GetString(this._flex미수금현황["CD_PARTNER"]);
                filter = "CD_PARTNER = '" + key + "'";

                if (this._flex미수금현황.DetailQueryNeed == true)
                {
                    dt = this._biz.Search매출리스트(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                   key,
                                                                   this.dtp기준년월.Text });
                }

                this._flex매출리스트.BindingAdd(dt, filter);

                dt1 = this._biz.Search차트(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                          key,
                                                          this.dtp기준년월.Text });

                if (dt1 == null || dt1.Rows.Count == 0)
                    this.chart미수금.ChartFx.Data.Clear();
                else
                {
                    this.chart미수금.DataSource = dt1.Copy();

                    this.chart미수금.ChartFx.Series[0].Text = "매출금액";
                    this.chart미수금.ChartFx.Series[1].Text = "수금금액";
                    this.chart미수금.ChartFx.Series[2].Text = "미수금액";
                    this.chart미수금.ChartFx.Series[3].Visible = false;
                    this.chart미수금.ChartFx.Series[4].Visible = false;
                    this.chart미수금.ChartFx.Series[5].Text = "전월미수금액";
                    this.chart미수금.ChartFx.Series[5].Gallery = Gallery.Bar;
                    this.chart미수금.ChartFx.Series[6].Visible = false;

                    this.chart회수율.DataSource = dt1.Copy();

                    this.chart회수율.ChartFx.Series[0].Visible = false;
                    this.chart회수율.ChartFx.Series[1].Visible = false;
                    this.chart회수율.ChartFx.Series[2].Visible = false;
                    this.chart회수율.ChartFx.Series[3].Text = "당월매출금액";
                    this.chart회수율.ChartFx.Series[4].Text = "당월수금금액";
                    this.chart회수율.ChartFx.Series[5].Visible = false;
                    this.chart회수율.ChartFx.Series[6].Text = "회수율";
                    this.chart회수율.ChartFx.Series[6].PointLabels.Visible = true;

                    this.chart회수율.ChartFx.Series[6].Pane = this.chart회수율.ChartFx.Panes[1];
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex매출리스트_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt;
            string key, filter;

            try
            {
                dt = null;
                key = D.GetString(this._flex매출리스트["NO_IV"]);
                filter = "NO_IV = '" + key + "'";

                if (this._flex매출리스트.DetailQueryNeed == true)
                {
                    dt = this._biz.Search수금리스트(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                   this.dtp기준년월.Text,
                                                                   key });
                }

                this._flex수금리스트.BindingAdd(dt, filter);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FlexGrid grid;
            string pageId, pageName;
            object[] obj;

            try
            {
                grid = (sender as FlexGrid);
                if (grid.HasNormalRow == false) return;
                if (grid.MouseRow < grid.Rows.Fixed) return;

                if (grid.Cols["NO_IV"] != null && grid.ColSel == grid.Cols["NO_IV"].Index)
                {
                    pageId = "P_CZ_SA_IVMNG";
                    pageName = Global.MainFrame.DD("매출관리(딘텍)");
                    obj = new object[] { D.GetString(grid["NO_IV"]) };

                    if (Global.MainFrame.IsExistPage(pageId, false))
                        Global.MainFrame.UnLoadPage(pageId, false);

                    Global.MainFrame.LoadPageFrom(pageId, pageName, this.Grant, obj);
                }
                else if (grid.Cols["NO_RCP"] != null && grid.ColSel == grid.Cols["NO_RCP"].Index)
                {
                    pageId = "P_CZ_SA_RCP";
                    pageName = Global.MainFrame.DD("수금등록(딘텍)");
                    obj = new object[] { D.GetString(grid["NO_RCP"]) };

                    if (Global.MainFrame.IsExistPage(pageId, false))
                        Global.MainFrame.UnLoadPage(pageId, false);

                    Global.MainFrame.LoadPageFrom(pageId, pageName, this.Grant, obj);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
    }
}

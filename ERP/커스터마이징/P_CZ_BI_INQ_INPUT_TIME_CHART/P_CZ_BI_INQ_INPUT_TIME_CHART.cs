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
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.ERPU;
using Dintec;

namespace cz
{
    public partial class P_CZ_BI_INQ_INPUT_TIME_CHART : PageBase
    {
        P_CZ_BI_INQ_INPUT_TIME_CHART_BIZ _biz = new P_CZ_BI_INQ_INPUT_TIME_CHART_BIZ();

        public P_CZ_BI_INQ_INPUT_TIME_CHART()
        {
			StartUp.Certify(this);
			InitializeComponent();
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
        }

        private void InitGrid()
        {
            #region 부서별
            this._flex부서별.BeginSetting(2, 1, false);

            this._flex부서별.SetCol("NM_SALEORG", "부서명", 100);
            this._flex부서별.SetCol("NM_CC", "팀명", 100);
            this._flex부서별.SetCol("TM_MINUTE", "총입력소요분", false);
            this._flex부서별.SetCol("TM_INPUT", "평균입력\n소요시간", 80);
            this._flex부서별.SetCol("QT_FILE", "건수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex부서별.SetCol("QT_ITEM", "종수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex부서별.SetCol("QT_TIME", "시간계산건수", false);
            this._flex부서별.SetCol("QT_FILE_DAY", "건수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex부서별.SetCol("QT_ITEM_DAY", "종수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex부서별.SetCol("QT_FILE_PARSING", "건수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex부서별.SetCol("QT_ITEM_PARSING", "종수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex부서별.SetCol("QT_FILE_SALES", "건수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex부서별.SetCol("QT_ITEM_SALES", "종수", 80, false, typeof(decimal), FormatTpType.QUANTITY);

            this._flex부서별[0, this._flex부서별.Cols["QT_FILE"].Index] = "Inquiry 입력";
            this._flex부서별[0, this._flex부서별.Cols["QT_ITEM"].Index] = "Inquiry 입력";
            this._flex부서별[0, this._flex부서별.Cols["QT_TIME"].Index] = "Inquiry 입력";
            this._flex부서별[0, this._flex부서별.Cols["QT_FILE_DAY"].Index] = "일평균 입력";
            this._flex부서별[0, this._flex부서별.Cols["QT_ITEM_DAY"].Index] = "일평균 입력";
            this._flex부서별[0, this._flex부서별.Cols["QT_FILE_PARSING"].Index] = "파싱";
            this._flex부서별[0, this._flex부서별.Cols["QT_ITEM_PARSING"].Index] = "파싱";
            this._flex부서별[0, this._flex부서별.Cols["QT_FILE_SALES"].Index] = "영업담당자 입력";
            this._flex부서별[0, this._flex부서별.Cols["QT_ITEM_SALES"].Index] = "영업담당자 입력";

            this._flex부서별.SettingVersion = "0.0.0.1";
            this._flex부서별.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region 담당자별
            this._flex담당자별.BeginSetting(2, 1, false);

            this._flex담당자별.SetCol("NM_SALEORG", "부서명", 100);
            this._flex담당자별.SetCol("NM_USER", "담당자명", 80);
            this._flex담당자별.SetCol("TM_MINUTE", "총입력소요분", false);
            this._flex담당자별.SetCol("TM_INPUT", "평균입력\n소요시간", 80);
            this._flex담당자별.SetCol("QT_FILE", "건수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex담당자별.SetCol("QT_ITEM", "종수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex담당자별.SetCol("QT_TIME", "시간계산건수", false);
            this._flex담당자별.SetCol("QT_FILE_DAY", "건수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex담당자별.SetCol("QT_ITEM_DAY", "종수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex담당자별.SetCol("QT_FILE_PARSING", "건수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex담당자별.SetCol("QT_ITEM_PARSING", "종수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex담당자별.SetCol("QT_FILE_SALES", "건수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex담당자별.SetCol("QT_ITEM_SALES", "종수", 80, false, typeof(decimal), FormatTpType.QUANTITY);

            this._flex담당자별[0, this._flex담당자별.Cols["QT_FILE"].Index] = "Inquiry 입력";
            this._flex담당자별[0, this._flex담당자별.Cols["QT_ITEM"].Index] = "Inquiry 입력";
            this._flex담당자별[0, this._flex담당자별.Cols["QT_TIME"].Index] = "Inquiry 입력";
            this._flex담당자별[0, this._flex담당자별.Cols["QT_FILE_DAY"].Index] = "일평균 입력";
            this._flex담당자별[0, this._flex담당자별.Cols["QT_ITEM_DAY"].Index] = "일평균 입력";
            this._flex담당자별[0, this._flex담당자별.Cols["QT_FILE_PARSING"].Index] = "파싱";
            this._flex담당자별[0, this._flex담당자별.Cols["QT_ITEM_PARSING"].Index] = "파싱";
            this._flex담당자별[0, this._flex담당자별.Cols["QT_FILE_SALES"].Index] = "영업담당자 입력";
            this._flex담당자별[0, this._flex담당자별.Cols["QT_ITEM_SALES"].Index] = "영업담당자 입력";

            this._flex담당자별.SettingVersion = "0.0.0.1";
            this._flex담당자별.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.dtp기준년월.Text = this.MainFrameInterface.GetStringFirstDayInMonth;
            this.dtp기준년월.ToDayDate = this.MainFrameInterface.GetDateTimeToday();

            this.chart월별입력시간.ChartFx.Gallery = Gallery.Bar;
            this.chart월별입력시간.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Number;
            this.chart월별입력시간.ChartFx.AxisX.AutoScroll = true;
            this.chart월별입력시간.ChartFx.LegendBox.Visible = true;
        }

        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch()) return false;

            if (string.IsNullOrEmpty(this.dtp기준년월.Text))
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl기준년월.Text);
                return false;
            }

            return true;
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            DataTable dataTable;
            TimeSpan timeSpan;
            decimal 건수, 시간;

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
                                                      "AND DT_CAL BETWEEN '" + this.dtp기준년월.Text + "' + '00' AND '" + this.dtp기준년월.Text + "' + '99'");

                this.dtp대상기간.StartDateToString = dt.Rows[0]["DT_START"].ToString();
                this.dtp대상기간.EndDateToString = dt.Rows[0]["DT_END"].ToString();

                this.cur근무일수.DecimalValue = D.GetDecimal(dt.Rows[0]["DAY_WORKING"]);

                dataTable = this._biz.Search1(new object[] { Global.MainFrame.LoginInfo.CompanyCode, 
                                                             this.dtp기준년월.Text });

                if (dataTable == null || dataTable.Rows.Count == 0)
                    this.chart월별입력시간.ChartFx.Data.Clear();
                else
                {
                    if (this.chart월별입력시간.ChartFx.Panes.Count > 1)
                        this.chart월별입력시간.ChartFx.Panes.Remove(this.chart월별입력시간.ChartFx.Panes[1]);

                    this.chart월별입력시간.ChartFx.Panes.Add(new Pane());
                    this.chart월별입력시간.ChartFx.Panes[1].AxisY.LabelsFormat.Format = AxisFormat.Time;
                    this.chart월별입력시간.ChartFx.Panes[1].AxisY.LabelsFormat.CustomFormat = "HH:mm";

                    this.chart월별입력시간.DataSource = dataTable;

                    this.chart월별입력시간.ChartFx.Series[0].Text = "영업부 건수";
                    this.chart월별입력시간.ChartFx.Series[1].Visible = false;
                    this.chart월별입력시간.ChartFx.Series[2].Text = "영업부 시간";

                    this.chart월별입력시간.ChartFx.Series[0].Gallery = Gallery.Lines;
                    this.chart월별입력시간.ChartFx.Series[0].PointLabels.Visible = true;

                    this.chart월별입력시간.ChartFx.Series[2].Pane = this.chart월별입력시간.ChartFx.Panes[1];
                    this.chart월별입력시간.ChartFx.Series[2].Gallery = Gallery.Lines;
                    this.chart월별입력시간.ChartFx.Series[2].PointLabels.Visible = true;

                    for (int i = 0; i < this.chart월별입력시간.ChartFx.Data.Points; i++)
                    {
                        건수 = Convert.ToDecimal(this.chart월별입력시간.ChartFx.Data.Y[1, i]);

                        if (건수 > 0)
                        {
                            시간 = Convert.ToDecimal(this.chart월별입력시간.ChartFx.Data.Y[2, i]);
                            timeSpan = TimeSpan.FromMinutes((double)Math.Round(시간 / 건수, MidpointRounding.AwayFromZero));
                            this.chart월별입력시간.ChartFx.Data.Y[2, i] = (new DateTime() + timeSpan).ToOADate();
                        }
                    }
                }

                this._flex부서별.UnBinding = this._biz.Search2(new object[] { Global.MainFrame.LoginInfo.CompanyCode, 
                                                                              this.dtp기준년월.Text });
                this._flex담당자별.UnBinding = this._biz.Search3(new object[] { Global.MainFrame.LoginInfo.CompanyCode, 
                                                                                this.dtp기준년월.Text });

                this.부서별SubTotal();
                this.담당자별SubTotal();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void 부서별SubTotal()
        {
            SubTotals subTotals;
            SubTotal subTotal;
            CellRange cellRange;

            try
            {
                if (!this._flex부서별.HasNormalRow) return;

                this._flex부서별.Redraw = false;

                subTotals = new SubTotals();

                subTotals.FirstRow = this._flex부서별.Rows.Fixed;

                subTotal = subTotals.NewTotal();
                subTotal.Type = TotalEnum.SubTotal;
                subTotal.GroupCol = this._flex부서별.Cols["NM_SALEORG"].Index;
                subTotal.TotalColName = new string[] { "TM_MINUTE", "QT_FILE", "QT_ITEM", "QT_FILE_PARSING", "QT_ITEM_PARSING", "QT_FILE_SALES", "QT_ITEM_SALES", "QT_TIME" };
                subTotals.Add(subTotal);

                subTotal = subTotals.NewTotal();
                subTotal.Type = TotalEnum.GrandTotal;
                subTotal.TotalColName = new string[] { "TM_MINUTE", "QT_FILE", "QT_ITEM", "QT_FILE_PARSING", "QT_ITEM_PARSING", "QT_FILE_SALES", "QT_ITEM_SALES", "QT_TIME" };
                subTotals.Add(subTotal);

                this._flex부서별.DoSubTotal(subTotals);

                for (int i = this._flex부서별.Rows.Fixed; i < this._flex부서별.Rows.Count; i++)
                {
                    if (this._flex부서별.Rows[i].IsNode)
                    {
                        switch (D.GetString(this._flex부서별.Rows[i].UserData).Substring(0, 6))
                        {
                            case "XXSUB1":
                                this._flex부서별[i, "NM_SALEORG"] += "소계";
                                break;
                            case "XXGRA0":
                                this._flex부서별[i, "NM_SALEORG"] = "총계";
                                break;
                        }
                    }
                    else
                    {
                        cellRange = this._flex부서별.GetCellRange(i, "NM_SALEORG", i, "NM_SALEORG");
                        cellRange.UserData = this._flex부서별[i, "NM_SALEORG"].ToString();
                    }
                    
                    TimeSpan timeSpan = TimeSpan.FromMinutes((double)Math.Round(D.GetDecimal(this._flex부서별[i, "TM_MINUTE"]) / D.GetDecimal(this._flex부서별[i, "QT_TIME"]), MidpointRounding.AwayFromZero));
                    this._flex부서별[i, "TM_INPUT"] = timeSpan.Hours.ToString("00") + ":" + timeSpan.Minutes.ToString("00");
                    this._flex부서별[i, "QT_FILE_DAY"] = Math.Round(D.GetDecimal(this._flex부서별[i, "QT_FILE"]) / this.cur근무일수.DecimalValue, MidpointRounding.AwayFromZero);
                    this._flex부서별[i, "QT_ITEM_DAY"] = Math.Round(D.GetDecimal(this._flex부서별[i, "QT_ITEM"]) / this.cur근무일수.DecimalValue, MidpointRounding.AwayFromZero);
                }

                this._flex부서별.DoMerge();

                this._flex부서별.Redraw = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void 담당자별SubTotal()
        {
            SubTotals subTotals;
            SubTotal subTotal;
            CellRange cellRange;

            try
            {
                if (!this._flex담당자별.HasNormalRow) return;

                this._flex담당자별.Redraw = false;

                subTotals = new SubTotals();

                subTotals.FirstRow = this._flex담당자별.Rows.Fixed;

                subTotal = subTotals.NewTotal();
                subTotal.Type = TotalEnum.SubTotal;
                subTotal.GroupCol = this._flex담당자별.Cols["NM_SALEORG"].Index;
                subTotal.TotalColName = new string[] { "TM_MINUTE", "QT_FILE", "QT_ITEM", "QT_FILE_PARSING", "QT_ITEM_PARSING", "QT_FILE_SALES", "QT_ITEM_SALES", "QT_TIME" };
                subTotals.Add(subTotal);

                subTotal = subTotals.NewTotal();
                subTotal.Type = TotalEnum.GrandTotal;
                subTotal.TotalColName = new string[] { "TM_MINUTE", "QT_FILE", "QT_ITEM", "QT_FILE_PARSING", "QT_ITEM_PARSING", "QT_FILE_SALES", "QT_ITEM_SALES", "QT_TIME" };
                subTotals.Add(subTotal);

                this._flex담당자별.DoSubTotal(subTotals);

                for (int i = this._flex담당자별.Rows.Fixed; i < this._flex담당자별.Rows.Count; i++)
                {
                    if (this._flex담당자별.Rows[i].IsNode)
                    {
                        switch (D.GetString(this._flex담당자별.Rows[i].UserData).Substring(0, 6))
                        {
                            case "XXSUB1":
                                this._flex담당자별[i, "NM_SALEORG"] += "소계";
                                break;
                            case "XXGRA0":
                                this._flex담당자별[i, "NM_SALEORG"] = "총계";
                                break;
                        }
                    }
                    else
                    {
                        cellRange = this._flex담당자별.GetCellRange(i, "NM_SALEORG", i, "NM_SALEORG");
                        cellRange.UserData = this._flex담당자별[i, "NM_SALEORG"].ToString();
                    }

                    if (D.GetDecimal(this._flex담당자별[i, "QT_TIME"]) != 0)
                    {
                        TimeSpan timeSpan = TimeSpan.FromMinutes((double)Math.Round(D.GetDecimal(this._flex담당자별[i, "TM_MINUTE"]) / D.GetDecimal(this._flex담당자별[i, "QT_TIME"]), MidpointRounding.AwayFromZero));
                        this._flex담당자별[i, "TM_INPUT"] = timeSpan.Hours.ToString("00") + ":" + timeSpan.Minutes.ToString("00");
                    }

                    this._flex담당자별[i, "QT_FILE_DAY"] = Math.Round(D.GetDecimal(this._flex담당자별[i, "QT_FILE"]) / this.cur근무일수.DecimalValue, MidpointRounding.AwayFromZero);
                    this._flex담당자별[i, "QT_ITEM_DAY"] = Math.Round(D.GetDecimal(this._flex담당자별[i, "QT_ITEM"]) / this.cur근무일수.DecimalValue, MidpointRounding.AwayFromZero);
                }

                this._flex담당자별.DoMerge();

                this._flex담당자별.Redraw = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
    }
}

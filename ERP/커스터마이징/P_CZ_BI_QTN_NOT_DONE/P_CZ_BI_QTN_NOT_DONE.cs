using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Duzon.ERPU;
using Duzon.Common.Forms;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using ChartFX.WinForms;


namespace cz
{
    public partial class P_CZ_BI_QTN_NOT_DONE : PageBase
    {
        P_CZ_BI_QTN_NOT_DONE_BIZ _biz = new P_CZ_BI_QTN_NOT_DONE_BIZ();

        string salesGroupNo = "010301"; // 영업

        public P_CZ_BI_QTN_NOT_DONE()
        {
            InitializeComponent();
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.chart미견적률추이.ChartFx.Gallery = Gallery.Lines;
            this.chart미견적률추이.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Number;
            this.chart미견적률추이.ChartFx.AxisY.LabelsFormat.Decimals = 1;
            this.chart미견적률추이.ChartFx.AxisX.AutoScroll = true;
            this.chart미견적률추이.ChartFx.LegendBox.Visible = true;
            this.chart미견적률추이.ChartFx.DataGrid.Visible = false;

            this.chart미견적률추이2.ChartFx.LegendBox.Visible = true;
            this.chart미견적률추이2.ChartFx.AxisX.AutoScroll = true;
            this.chart미견적률추이2.ChartFx.Gallery = Gallery.Lines;
            this.chart미견적률추이2.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Number;
            this.chart미견적률추이2.ChartFx.AxisY.LabelsFormat.Decimals = 1;
            this.chart미견적률추이2.ChartFx.DataGrid.Visible = true;


            this.dtp기준년월.Text = this.MainFrameInterface.GetStringFirstDayInMonth;
            this.dtp기준년월.ToDayDate = this.MainFrameInterface.GetDateTimeToday();

            salesGroupNo = "010301"; // 영업
        }

        private void InitGrid()
        {
            #region 담당자별미견적현황
            this._flex담당자별미견적현황.BeginSetting(2, 1, false);

            this._flex담당자별미견적현황.SetCol("NM_SALEORG", "부서명", 100);
            this._flex담당자별미견적현황.SetCol("NM_CC", "팀명", 100);
            this._flex담당자별미견적현황.SetCol("NM_USER", "성명", 100);
            this._flex담당자별미견적현황.SetCol("QT_INQ", "총 INQ 건 수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex담당자별미견적현황.SetCol("QT_NOT_DONE_2", "건수(CLOSE)", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex담당자별미견적현황.SetCol("QT_NOT_DONE", "건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex담당자별미견적현황.SetCol("FACT_CLOSE", "(%)", 100);
            //this._flex담당자별미견적현황.SetCol("QT_CLOSE", "Closing 소요일",100, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flex담당자별미견적현황.SetCol("QT_CLOSE", "Closing 소요일", 100);
            this._flex담당자별미견적현황.SetCol("DT_QTN_SUM", "미견적합계일", 0, false,typeof(decimal), FormatTpType.QUANTITY);

            this._flex담당자별미견적현황.Cols["FACT_CLOSE"].TextAlign = TextAlignEnum.RightCenter;
            this._flex담당자별미견적현황.Cols["QT_CLOSE"].TextAlign = TextAlignEnum.RightCenter;

            this._flex담당자별미견적현황[0, this._flex담당자별미견적현황.Cols["QT_NOT_DONE"].Index] = "미견적";
            this._flex담당자별미견적현황[0, this._flex담당자별미견적현황.Cols["FACT_CLOSE"].Index] = "미견적";

            this._flex담당자별미견적현황.SettingVersion = "0.0.0.9";
            this._flex담당자별미견적현황.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion 담당자별미견적현황

            #region 팀별미견적현황
            this._flex팀별미견적현황.BeginSetting(2, 1, false);

            this._flex팀별미견적현황.SetCol("NM_SALEORG", "부서명", 100);
            this._flex팀별미견적현황.SetCol("NM_CC", "팀명", 100);
            this._flex팀별미견적현황.SetCol("QT_INQ", "총 INQ 건 수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex팀별미견적현황.SetCol("QT_NOT_DONE_2", "건수(CLOSE)", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex팀별미견적현황.SetCol("QT_NOT_DONE", "건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex팀별미견적현황.SetCol("FACT_CLOSE", "(%)", 100);
            //this._flex팀별미견적현황.SetCol("QT_CLOSE", "Closing 소요일", 100, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flex팀별미견적현황.SetCol("QT_CLOSE", "Closing 소요일", 100);
            this._flex팀별미견적현황.SetCol("DT_QTN_SUM", "미견적합계일", 0, false, typeof(decimal), FormatTpType.QUANTITY);

            this._flex팀별미견적현황.Cols["FACT_CLOSE"].TextAlign = TextAlignEnum.RightCenter;
            this._flex팀별미견적현황.Cols["QT_CLOSE"].TextAlign = TextAlignEnum.RightCenter;



            this._flex팀별미견적현황[0, this._flex팀별미견적현황.Cols["QT_NOT_DONE"].Index] = "미견적";
            this._flex팀별미견적현황[0, this._flex팀별미견적현황.Cols["FACT_CLOSE"].Index] = "미견적";

            this._flex팀별미견적현황.SettingVersion = "0.0.0.8";
            this._flex팀별미견적현황.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion 팀별미견적현황

            #region 원인별미견적현황
            this._flex원인별미견적현황.BeginSetting(2, 1, false);

            this._flex원인별미견적현황.SetCol("NM_SALEORG", "부서명", 100);
            this._flex원인별미견적현황.SetCol("NM_CC", "팀명", 100);
            this._flex원인별미견적현황.SetCol("TOTAL_SUM", "총 합계", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex원인별미견적현황.SetCol("C1", "고객문의 취소", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex원인별미견적현황.SetCol("C2", "Inquiry 중복접수", 130, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex원인별미견적현황.SetCol("C3", "사양확인 중", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex원인별미견적현황.SetCol("C4", "사양확인 중", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex원인별미견적현황.SetCol("C5", "수배불가", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex원인별미견적현황.SetCol("C6", "견적 거절", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex원인별미견적현황.SetCol("C7", "견적 미회신", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex원인별미견적현황.SetCol("C8", "Maker 직거래 건", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex원인별미견적현황.SetCol("C9", "기타", 100, false, typeof(decimal), FormatTpType.QUANTITY);

            this._flex원인별미견적현황[0, this._flex원인별미견적현황.Cols["C3"].Index] = "매출처";
            this._flex원인별미견적현황[0, this._flex원인별미견적현황.Cols["C4"].Index] = "매입처";
            this._flex원인별미견적현황[0, this._flex원인별미견적현황.Cols["C5"].Index] = "매입처";
            this._flex원인별미견적현황[0, this._flex원인별미견적현황.Cols["C6"].Index] = "매입처";
            this._flex원인별미견적현황[0, this._flex원인별미견적현황.Cols["C7"].Index] = "매입처";

            this._flex원인별미견적현황.SettingVersion = "0.0.0.7";
            this._flex원인별미견적현황.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            #endregion 원인별미견적현황

            #region 선주사별미견적현황
            this._flex선주사별미견적현황.BeginSetting(2, 1, false);

            this._flex선주사별미견적현황.SetCol("LN_PARTNER", "Buyer", 250);
            this._flex선주사별미견적현황.SetCol("EXCLUDE_CLOSE2", "총 합계", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex선주사별미견적현황.SetCol("C1", "고객문의 취소", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex선주사별미견적현황.SetCol("C2", "Inquiry 중복접수", 130, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex선주사별미견적현황.SetCol("C3", "사양확인 중", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex선주사별미견적현황.SetCol("C4", "사양확인 중", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex선주사별미견적현황.SetCol("C5", "수배불가", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex선주사별미견적현황.SetCol("C6", "견적 거절", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex선주사별미견적현황.SetCol("C7", "견적 미회신", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex선주사별미견적현황.SetCol("C8", "Maker 직거래 건", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex선주사별미견적현황.SetCol("C9", "기타", 100, false, typeof(decimal), FormatTpType.QUANTITY);


            this._flex선주사별미견적현황[0, this._flex선주사별미견적현황.Cols["C3"].Index] = "매출처";
            this._flex선주사별미견적현황[0, this._flex선주사별미견적현황.Cols["C4"].Index] = "매입처";
            this._flex선주사별미견적현황[0, this._flex선주사별미견적현황.Cols["C5"].Index] = "매입처";
            this._flex선주사별미견적현황[0, this._flex선주사별미견적현황.Cols["C6"].Index] = "매입처";
            this._flex선주사별미견적현황[0, this._flex선주사별미견적현황.Cols["C7"].Index] = "매입처";


            this._flex선주사별미견적현황.SettingVersion = "0.0.0.5";
            this._flex선주사별미견적현황.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion 선주사별미견적현황



            #region 담당자원인별미견적현황
            this._flex담당자원인별미견적.BeginSetting(2, 1, false);

            this._flex담당자원인별미견적.SetCol("NM_SALEORG", "부서명", 100);
            this._flex담당자원인별미견적.SetCol("NM_CC", "팀명", 100);
            this._flex담당자원인별미견적.SetCol("NM_USER", "성명", 100);
            this._flex담당자원인별미견적.SetCol("TOTAL_SUM", "총 합계", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex담당자원인별미견적.SetCol("C1", "고객문의 취소", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex담당자원인별미견적.SetCol("C2", "Inquiry 중복접수", 130, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex담당자원인별미견적.SetCol("C3", "사양확인 중", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex담당자원인별미견적.SetCol("C4", "사양확인 중", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex담당자원인별미견적.SetCol("C5", "수배불가", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex담당자원인별미견적.SetCol("C6", "견적 거절", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex담당자원인별미견적.SetCol("C7", "견적 미회신", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex담당자원인별미견적.SetCol("C8", "Maker 직거래 건", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex담당자원인별미견적.SetCol("C9", "기타", 100, false, typeof(decimal), FormatTpType.QUANTITY);

            this._flex담당자원인별미견적[0, this._flex담당자원인별미견적.Cols["C3"].Index] = "매출처";
            this._flex담당자원인별미견적[0, this._flex담당자원인별미견적.Cols["C4"].Index] = "매입처";
            this._flex담당자원인별미견적[0, this._flex담당자원인별미견적.Cols["C5"].Index] = "매입처";
            this._flex담당자원인별미견적[0, this._flex담당자원인별미견적.Cols["C6"].Index] = "매입처";
            this._flex담당자원인별미견적[0, this._flex담당자원인별미견적.Cols["C7"].Index] = "매입처";

            this._flex담당자원인별미견적.SettingVersion = "0.0.0.7";
            this._flex담당자원인별미견적.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			#endregion 담당자원인별미견적현황
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

            try
            {
                if (!BeforeSearch()) return;

                DataTable dt = DBHelper.GetDataTable(@"SELECT MIN(DT_CAL) AS DT_START,
                                                       	      MAX(DT_CAL) AS DT_END,
                                                       	      SUM(CASE WHEN FG1_HOLIDAY = 'W' THEN 1 ELSE 0 END) AS DAY_WORKING
                                                       FROM MA_CALENDAR WITH(NOLOCK)
                                                       WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
                                                      "AND DT_CAL BETWEEN '" + this.dtp기준년월.Text + "' + '00' AND '" + this.dtp기준년월.Text + "' + '99'");

                this.dtp대상기간.StartDateToString = dt.Rows[0]["DT_START"].ToString();
                this.dtp대상기간.EndDateToString = dt.Rows[0]["DT_END"].ToString();

                this.cur근무일수.DecimalValue = D.GetDecimal(dt.Rows[0]["DAY_WORKING"]);


                // 미견적률 추이 1
                dataTable = this._biz.Search6(new object[] { Global.MainFrame.LoginInfo.CompanyCode, 
                                                             this.dtp기준년월.Text, salesGroupNo });

                if (dataTable == null || dataTable.Rows.Count == 0)
                    this.chart미견적률추이.ChartFx.Data.Clear();
                else
                {
                    this.chart미견적률추이.DataSource = dataTable;

                    this.chart미견적률추이.ChartFx.Series[0].Text = "전체";
                    //this.chart미견적률추이.ChartFx.Series[1].Text = "영업1부";
                    //this.chart미견적률추이.ChartFx.Series[2].Text = "영업2부";

                    this.chart미견적률추이.ChartFx.Series[0].PointLabels.Visible = true;
                    //this.chart미견적률추이.ChartFx.Series[1].Gallery = Gallery.Bar;
                    //this.chart미견적률추이.ChartFx.Series[2].Gallery = Gallery.Bar;
                }


                // 미견적률 추이2

                dataTable = this._biz.Search5(new object[] {Global.MainFrame.LoginInfo.CompanyCode,
                                                             this.dtp기준년월.Text, salesGroupNo});

                if (salesGroupNo == "010301")
                {

                    if (dataTable == null || dataTable.Rows.Count == 0)
                        this.chart미견적률추이2.ChartFx.Data.Clear();
                    else
                    {

                        this.chart미견적률추이2.DataSource = dataTable;

                        this.chart미견적률추이2.ChartFx.Series[0].Text = "영업1팀";
                        this.chart미견적률추이2.ChartFx.Series[1].Text = "영업2팀";
                        this.chart미견적률추이2.ChartFx.Series[2].Text = "영업3팀";
                        this.chart미견적률추이2.ChartFx.Series[3].Text = "영업5팀";
                        this.chart미견적률추이2.ChartFx.Series[4].Text = "영업6팀";
                        this.chart미견적률추이2.ChartFx.Series[5].Text = "영업7팀";
                        this.chart미견적률추이2.ChartFx.Series[6].Text = "영업8팀";

                        this.chart미견적률추이2.ChartFx.Series[0].Gallery = Gallery.Bar;
                        this.chart미견적률추이2.ChartFx.Series[1].Gallery = Gallery.Bar;
                        this.chart미견적률추이2.ChartFx.Series[2].Gallery = Gallery.Bar;
                        this.chart미견적률추이2.ChartFx.Series[3].Gallery = Gallery.Bar;
                        this.chart미견적률추이2.ChartFx.Series[4].Gallery = Gallery.Bar;
                        this.chart미견적률추이2.ChartFx.Series[6].Gallery = Gallery.Bar;
                    }
                }
                else
				{
                    if (dataTable == null || dataTable.Rows.Count == 0)
                        this.chart미견적률추이2.ChartFx.Data.Clear();
                    else
                    {

                        this.chart미견적률추이2.DataSource = dataTable;

                        this.chart미견적률추이2.ChartFx.Series[0].Text = "기획실";
                        this.chart미견적률추이2.ChartFx.Series[1].Text = "-";
                        this.chart미견적률추이2.ChartFx.Series[2].Text = "-";
                        this.chart미견적률추이2.ChartFx.Series[3].Text = "-";
                        this.chart미견적률추이2.ChartFx.Series[4].Text = "-";
                        this.chart미견적률추이2.ChartFx.Series[5].Text = "-";
                        this.chart미견적률추이2.ChartFx.Series[5].Text = "-";

                        this.chart미견적률추이2.ChartFx.Series[0].Gallery = Gallery.Bar;
                        this.chart미견적률추이2.ChartFx.Series[1].Gallery = Gallery.Bar;
                        this.chart미견적률추이2.ChartFx.Series[2].Gallery = Gallery.Bar;
                        this.chart미견적률추이2.ChartFx.Series[3].Gallery = Gallery.Bar;
                        this.chart미견적률추이2.ChartFx.Series[4].Gallery = Gallery.Bar;
                        this.chart미견적률추이2.ChartFx.Series[5].Gallery = Gallery.Bar;
                        this.chart미견적률추이2.ChartFx.Series[6].Gallery = Gallery.Bar;
                    }
                }





                this._flex담당자별미견적현황.UnBinding = this._biz.Search1(new object[] { Global.MainFrame.LoginInfo.CompanyCode, 
                                                                                        this.dtp기준년월.Text, salesGroupNo });
                this._flex팀별미견적현황.UnBinding = this._biz.Search2(new object[] { Global.MainFrame.LoginInfo.CompanyCode, 
                                                                                        this.dtp기준년월.Text, salesGroupNo });
                this._flex원인별미견적현황.UnBinding = this._biz.Search3(new object[] { Global.MainFrame.LoginInfo.CompanyCode, 
                                                                                        this.dtp기준년월.Text, salesGroupNo });
                this._flex선주사별미견적현황.UnBinding = this._biz.Search4(new object[] { Global.MainFrame.LoginInfo.CompanyCode, 
                                                                                        this.dtp기준년월.Text, salesGroupNo });
                this._flex담당자원인별미견적.UnBinding = this._biz.Search7(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                        this.dtp기준년월.Text, salesGroupNo });

                //this._flex미견적률추이.UnBinding = this._biz.Search2(new object[] { Global.MainFrame.LoginInfo.CompanyCode, 
                //                                                                    this.dtp기준년월.Text });


                //dataTable = this._biz.Search4(new object[] { Global.MainFrame.LoginInfo.CompanyCode, 
                //                                             this.dtp기준년월.Text });

                //if (dataTable == null || dataTable.Rows.Count == 0)
                //    this.chart고객문의추이.ChartFx.Data.Clear();
                //else
                //{
                //    this.chart고객문의추이.DataSource = dataTable;

                //    this.chart고객문의추이.ChartFx.Series[0].Text = "전체 건수";
                //    this.chart고객문의추이.ChartFx.Series[1].Text = "영업1부 건수";
                //    this.chart고객문의추이.ChartFx.Series[2].Text = "영업2부 건수";
                //    this.chart고객문의추이.ChartFx.Series[3].Text = "전체 종수";
                //    this.chart고객문의추이.ChartFx.Series[4].Text = "영업1부 종수";
                //    this.chart고객문의추이.ChartFx.Series[5].Text = "영업2부 종수";

                //    this.chart고객문의추이.ChartFx.Series[0].PointLabels.Visible = true;
                //    this.chart고객문의추이.ChartFx.Series[1].Gallery = Gallery.Bar;
                //    this.chart고객문의추이.ChartFx.Series[2].Gallery = Gallery.Bar;
                //    this.chart고객문의추이.ChartFx.Series[3].PointLabels.Visible = true;
                //    this.chart고객문의추이.ChartFx.Series[4].Gallery = Gallery.Bar;
                //    this.chart고객문의추이.ChartFx.Series[5].Gallery = Gallery.Bar;

                //    this.chart고객문의추이.ChartFx.Series[3].Pane = this.chart고객문의추이.ChartFx.Panes[1];
                //    this.chart고객문의추이.ChartFx.Series[4].Pane = this.chart고객문의추이.ChartFx.Panes[1];
                //    this.chart고객문의추이.ChartFx.Series[5].Pane = this.chart고객문의추이.ChartFx.Panes[1];
                //}

                //this.팀별견적현황SubTotal();
                //this.담당자별견적현황SubTotal();

                담당자별미견적현황SubTotal();
                팀별미견적현황SubTotal();
                원인별미견적현황SubTotal();
                담당자원인별미견적현황SubTotal();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void 담당자별미견적현황SubTotal()
        {
            SubTotals subTotals;
            SubTotal subTotal;
            CellRange cellRange;

            try
            {
                if (!this._flex담당자별미견적현황.HasNormalRow) return;

                this._flex담당자별미견적현황.Redraw = false;

                subTotals = new SubTotals();

                subTotals.FirstRow = this._flex담당자별미견적현황.Rows.Fixed;

                subTotal = subTotals.NewTotal();
                subTotal.Type = TotalEnum.SubTotal;
                subTotal.GroupCol = this._flex담당자별미견적현황.Cols["NM_CC"].Index;
                subTotal.TotalColName = new string[] { "QT_INQ", "QT_NOT_DONE", "DT_QTN_SUM", "QT_NOT_DONE_2" };
                subTotals.Add(subTotal);

                //subTotal = subTotals.NewTotal();
                //subTotal.Type = TotalEnum.SubTotal;
                //subTotal.GroupCol = this._flex담당자별미견적현황.Cols["NM_SALEORG"].Index;
                //subTotal.TotalColName = new string[] { "QT_INQ", "QT_NOT_DONE", "DT_QTN_SUM", "QT_NOT_DONE_2" };
                //subTotals.Add(subTotal);

                subTotal = subTotals.NewTotal();
                subTotal.Type = TotalEnum.GrandTotal;
                subTotal.TotalColName = new string[] { "QT_INQ", "QT_NOT_DONE", "DT_QTN_SUM", "QT_NOT_DONE_2" };
                subTotals.Add(subTotal);

                this._flex담당자별미견적현황.DoSubTotal(subTotals);

                for (int i = this._flex담당자별미견적현황.Rows.Fixed; i < this._flex담당자별미견적현황.Rows.Count; i++)
                {
                    if (this._flex담당자별미견적현황.Rows[i].IsNode)
                    {
                        switch (D.GetString(this._flex담당자별미견적현황.Rows[i].UserData).Substring(0, 6))
                        {
                            case "XXSUB1":
                                this._flex담당자별미견적현황[i, "NM_SALEORG"] += "소계";
                                break;
                            case "XXSUB2":
                                this._flex담당자별미견적현황[i, "NM_SALEORG"] += "소계";
                                break;
                            case "XXGRA0":
                                this._flex담당자별미견적현황[i, "NM_SALEORG"] = "총계";
                                break;
                        }
                    }
                    else
                    {
                        cellRange = this._flex담당자별미견적현황.GetCellRange(i, "NM_SALEORG", i, "NM_SALEORG");
                        cellRange.UserData = this._flex담당자별미견적현황[i, "NM_SALEORG"].ToString();

                        cellRange = this._flex담당자별미견적현황.GetCellRange(i, "NM_CC", i, "NM_CC");
                        cellRange.UserData = this._flex담당자별미견적현황[i, "NM_CC"].ToString();
                    }


                    if (D.GetDecimal(this._flex담당자별미견적현황[i, "QT_INQ"]) == 0)
                        this._flex담당자별미견적현황[i, "FACT_CLOSE"] = 0;
                    else
                        this._flex담당자별미견적현황[i, "FACT_CLOSE"] = decimal.Round((D.GetDecimal(this._flex담당자별미견적현황[i, "QT_NOT_DONE"]) / D.GetDecimal(this._flex담당자별미견적현황[i, "QT_INQ"])) * 100, 2, MidpointRounding.AwayFromZero);


                    decimal test3 = 0;

                    if (D.GetDecimal(this._flex담당자별미견적현황[i, "QT_NOT_DONE_2"]) == 0 || D.GetDecimal(this._flex담당자별미견적현황[i, "DT_QTN_SUM"]) == 0)
                        this._flex담당자별미견적현황[i, "QT_CLOSE"] = 0;
                    else
                    {
                        test3 = decimal.Round((D.GetDecimal(this._flex담당자별미견적현황[i, "DT_QTN_SUM"]) / D.GetDecimal(this._flex담당자별미견적현황[i, "QT_NOT_DONE_2"])), 2, MidpointRounding.AwayFromZero);
                        this._flex담당자별미견적현황[i, "QT_CLOSE"] = Convert.ToString(test3);//decimal.Round((D.GetDecimal(this._flex담당자별미견적현황[i, "DT_QTN_SUM"]) / D.GetDecimal(this._flex담당자별미견적현황[i, "QT_NOT_DONE"])), 2, MidpointRounding.AwayFromZero);
                    }
                }

                this._flex담당자별미견적현황.DoMerge();

                this._flex담당자별미견적현황.Redraw = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }


        private void 팀별미견적현황SubTotal()
        {
            SubTotals subTotals;
            SubTotal subTotal;
            CellRange cellRange;

            try
            {
                if (!this._flex팀별미견적현황.HasNormalRow) return;

                this._flex팀별미견적현황.Redraw = false;

                subTotals = new SubTotals();

                subTotals.FirstRow = this._flex팀별미견적현황.Rows.Fixed;

                //subTotal = subTotals.NewTotal();
                //subTotal.Type = TotalEnum.SubTotal;
                //subTotal.GroupCol = this._flex팀별미견적현황.Cols["NM_SALEORG"].Index;
                //subTotal.TotalColName = new string[] { "QT_INQ", "QT_NOT_DONE", "DT_QTN_SUM","QT_NOT_DONE_2"};
                //subTotals.Add(subTotal);

                subTotal = subTotals.NewTotal();
                subTotal.Type = TotalEnum.GrandTotal;
                subTotal.TotalColName = new string[] { "QT_INQ", "QT_NOT_DONE", "DT_QTN_SUM","QT_NOT_DONE_2" };
                subTotals.Add(subTotal);

                this._flex팀별미견적현황.DoSubTotal(subTotals);

                for (int i = this._flex팀별미견적현황.Rows.Fixed; i < this._flex팀별미견적현황.Rows.Count; i++)
                {
                    if (this._flex팀별미견적현황.Rows[i].IsNode)
                    {
                        switch (D.GetString(this._flex팀별미견적현황.Rows[i].UserData).Substring(0, 6))
                        {
                            case "XXSUB1":
                                this._flex팀별미견적현황[i, "NM_SALEORG"] += "소계";
                                break;
                            case "XXSUB2":
                                this._flex팀별미견적현황[i, "NM_SALEORG"] += "소계";
                                break;
                            case "XXGRA0":
                                this._flex팀별미견적현황[i, "NM_SALEORG"] = "총계";
                                break;
                        }
                    }
                    else
                    {
                        //cellRange = this._flex팀별미견적현황.GetCellRange(i, "NM_SALEORG", i, "NM_SALEORG");
                        //cellRange.UserData = this._flex팀별미견적현황[i, "NM_SALEORG"].ToString();

                        cellRange = this._flex팀별미견적현황.GetCellRange(i, "NM_CC", i, "NM_CC");
                        cellRange.UserData = this._flex팀별미견적현황[i, "NM_CC"].ToString();
                    }

                    if (D.GetDecimal(this._flex팀별미견적현황[i, "QT_INQ"]) == 0)
                        this._flex팀별미견적현황[i, "FACT_CLOSE"] = 0;
                    else
                        this._flex팀별미견적현황[i, "FACT_CLOSE"] = decimal.Round((D.GetDecimal(this._flex팀별미견적현황[i, "QT_NOT_DONE"]) / D.GetDecimal(this._flex팀별미견적현황[i, "QT_INQ"])) * 100, 2, MidpointRounding.AwayFromZero);


                    // QT_CLOSE = DT_QTN_SUM / QT_NOT_DONE
                    if (D.GetDecimal(this._flex팀별미견적현황[i, "QT_NOT_DONE_2"]) == 0 || D.GetDecimal(this._flex팀별미견적현황[i, "DT_QTN_SUM"]) == 0)
                        this._flex팀별미견적현황[i, "QT_CLOSE"] = 0;
                    else
                    {
                        this._flex팀별미견적현황[i, "QT_CLOSE"] = decimal.Round((D.GetDecimal(this._flex팀별미견적현황[i, "DT_QTN_SUM"]) / D.GetDecimal(this._flex팀별미견적현황[i, "QT_NOT_DONE_2"])), 2, MidpointRounding.AwayFromZero);

                        decimal test = decimal.Round((D.GetDecimal(this._flex팀별미견적현황[i, "DT_QTN_SUM"]) / D.GetDecimal(this._flex팀별미견적현황[i, "QT_NOT_DONE_2"])), 2, MidpointRounding.AwayFromZero);
                    }

                    

                }

                this._flex팀별미견적현황.DoMerge();

                this._flex팀별미견적현황.Redraw = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }



        private void 원인별미견적현황SubTotal()
        {
            SubTotals subTotals;
            SubTotal subTotal;
            CellRange cellRange;

            try
            {
                if (!this._flex원인별미견적현황.HasNormalRow) return;

                this._flex원인별미견적현황.Redraw = false;

                subTotals = new SubTotals();

                subTotals.FirstRow = this._flex원인별미견적현황.Rows.Fixed;

                //subTotal = subTotals.NewTotal();
                //subTotal.Type = TotalEnum.SubTotal;
                //subTotal.GroupCol = this._flex원인별미견적현황.Cols["NM_SALEORG"].Index;
                //subTotal.TotalColName = new string[] { "TOTAL_SUM", "C1", "C2", "C3", "C4", "C5", "C6", "C7", "C8", "C9" };
                //subTotals.Add(subTotal);

                subTotal = subTotals.NewTotal();
                subTotal.Type = TotalEnum.GrandTotal;
                subTotal.TotalColName = new string[] { "TOTAL_SUM", "C1", "C2", "C3", "C4", "C5", "C6", "C7", "C8", "C9" };
                subTotals.Add(subTotal);

                this._flex원인별미견적현황.DoSubTotal(subTotals);

                for (int i = this._flex원인별미견적현황.Rows.Fixed; i < this._flex원인별미견적현황.Rows.Count; i++)
                {
                    if (this._flex원인별미견적현황.Rows[i].IsNode)
                    {
                        switch (D.GetString(this._flex원인별미견적현황.Rows[i].UserData).Substring(0, 6))
                        {
                            case "XXSUB1":
                                this._flex원인별미견적현황[i, "NM_SALEORG"] += "소계";
                                break;
                            case "XXSUB2":
                                this._flex원인별미견적현황[i, "NM_SALEORG"] += "소계";
                                break;
                            case "XXGRA0":
                                this._flex원인별미견적현황[i, "NM_SALEORG"] = "총계";
                                break;
                        }
                    }
                    else
                    {
                        cellRange = this._flex원인별미견적현황.GetCellRange(i, "NM_SALEORG", i, "NM_SALEORG");
                        cellRange.UserData = this._flex원인별미견적현황[i, "NM_SALEORG"].ToString();

                        cellRange = this._flex원인별미견적현황.GetCellRange(i, "NM_CC", i, "NM_CC");
                        cellRange.UserData = this._flex원인별미견적현황[i, "NM_CC"].ToString();
                    }
                }

                this._flex원인별미견적현황.DoMerge();

                this._flex원인별미견적현황.Redraw = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }



        private void 선주사별미견적현황SubTotal()
        {
            SubTotals subTotals;
            SubTotal subTotal;
            CellRange cellRange;

            try
            {
                if (!this._flex선주사별미견적현황.HasNormalRow) return;

                this._flex선주사별미견적현황.Redraw = false;

                subTotals = new SubTotals();

                subTotals.FirstRow = this._flex선주사별미견적현황.Rows.Fixed;

                subTotal = subTotals.NewTotal();
                subTotal.Type = TotalEnum.SubTotal;
                subTotal.GroupCol = this._flex선주사별미견적현황.Cols["NM_SALEORG"].Index;
                subTotal.TotalColName = new string[] { "QT_INQ", "QT_DONE", "DT_QTN", "FACT_CLOSE" };
                subTotals.Add(subTotal);

                subTotal = subTotals.NewTotal();
                subTotal.Type = TotalEnum.GrandTotal;
                subTotal.TotalColName = new string[] { "QT_INQ", "QT_DONE", "DT_QTN", "FACT_CLOSE" };
                subTotals.Add(subTotal);

                this._flex선주사별미견적현황.DoSubTotal(subTotals);

                for (int i = this._flex선주사별미견적현황.Rows.Fixed; i < this._flex선주사별미견적현황.Rows.Count; i++)
                {
                    if (this._flex선주사별미견적현황.Rows[i].IsNode)
                    {
                        switch (D.GetString(this._flex선주사별미견적현황.Rows[i].UserData).Substring(0, 6))
                        {
                            case "XXSUB1":
                                this._flex선주사별미견적현황[i, "NM_SALEORG"] += "소계";
                                break;
                            case "XXSUB2":
                                this._flex선주사별미견적현황[i, "NM_SALEORG"] += "소계";
                                break;
                            case "XXGRA0":
                                this._flex선주사별미견적현황[i, "NM_SALEORG"] = "총계";
                                break;
                        }
                    }
                    else
                    {
                        cellRange = this._flex선주사별미견적현황.GetCellRange(i, "NM_SALEORG", i, "NM_SALEORG");
                        cellRange.UserData = this._flex선주사별미견적현황[i, "NM_SALEORG"].ToString();

                        cellRange = this._flex선주사별미견적현황.GetCellRange(i, "NM_CC", i, "NM_CC");
                        cellRange.UserData = this._flex선주사별미견적현황[i, "NM_CC"].ToString();
                    }
                }

                this._flex선주사별미견적현황.DoMerge();

                this._flex선주사별미견적현황.Redraw = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }


        private void 담당자원인별미견적현황SubTotal()
        {
            SubTotals subTotals;
            SubTotal subTotal;
            CellRange cellRange;

            try
            {
                if (!this._flex담당자원인별미견적.HasNormalRow) return;

                this._flex담당자원인별미견적.Redraw = false;

                subTotals = new SubTotals();

                subTotals.FirstRow = this._flex담당자원인별미견적.Rows.Fixed;

                //subTotal = subTotals.NewTotal();
                //subTotal.Type = TotalEnum.SubTotal;
                //subTotal.GroupCol = this._flex원인별미견적현황.Cols["NM_SALEORG"].Index;
                //subTotal.TotalColName = new string[] { "TOTAL_SUM", "C1", "C2", "C3", "C4", "C5", "C6", "C7", "C8", "C9" };
                //subTotals.Add(subTotal);

                subTotal = subTotals.NewTotal();
                subTotal.Type = TotalEnum.GrandTotal;
                subTotal.TotalColName = new string[] { "TOTAL_SUM", "C1", "C2", "C3", "C4", "C5", "C6", "C7", "C8", "C9" };
                subTotals.Add(subTotal);

                this._flex담당자원인별미견적.DoSubTotal(subTotals);

                for (int i = this._flex담당자원인별미견적.Rows.Fixed; i < this._flex담당자원인별미견적.Rows.Count; i++)
                {
                    if (this._flex담당자원인별미견적.Rows[i].IsNode)
                    {
                        switch (D.GetString(this._flex담당자원인별미견적.Rows[i].UserData).Substring(0, 6))
                        {
                            case "XXSUB1":
                                this._flex담당자원인별미견적[i, "NM_SALEORG"] += "소계";
                                break;
                            case "XXSUB2":
                                this._flex담당자원인별미견적[i, "NM_SALEORG"] += "소계";
                                break;
                            case "XXGRA0":
                                this._flex담당자원인별미견적[i, "NM_SALEORG"] = "총계";
                                break;
                        }
                    }
                    else
                    {
                        cellRange = this._flex담당자원인별미견적.GetCellRange(i, "NM_SALEORG", i, "NM_SALEORG");
                        cellRange.UserData = this._flex담당자원인별미견적[i, "NM_SALEORG"].ToString();

                        cellRange = this._flex담당자원인별미견적.GetCellRange(i, "NM_CC", i, "NM_CC");
                        cellRange.UserData = this._flex담당자원인별미견적[i, "NM_CC"].ToString();
                    }
                }

                this._flex담당자원인별미견적.DoMerge();

                this._flex담당자원인별미견적.Redraw = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

		private void cbx재고_CheckedChanged(object sender, EventArgs e)
		{
            if (cbx재고.Checked)
            {
                salesGroupNo = "010900";
            }
            else
            {
                salesGroupNo = "010301";
            }
        }
	}
}

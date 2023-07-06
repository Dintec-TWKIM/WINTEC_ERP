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
using account;
using Duzon.ERPU;
using Duzon.Common.BpControls;
using Duzon.DASS.Erpu.Windows.FX;
using Dintec;

namespace cz
{
    public partial class P_CZ_BI_BIZ_RESULT_CHART : PageBase
    {
        P_CZ_BI_BIZ_RESULT_CHART_BIZ _biz = new P_CZ_BI_BIZ_RESULT_CHART_BIZ();

        public P_CZ_BI_BIZ_RESULT_CHART()
        {
			StartUp.Certify(this);
			InitializeComponent();
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitEvent();
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.cbo회사.DataSource = MA.GetCodeUser(new string[] { "K100", "K200", "S100" }, new string[] { "(주)딘텍", "(주)두베코", "DINTEC SINGAPORE PTE.LTD." });
            this.cbo회사.DisplayMember = "NAME";
            this.cbo회사.ValueMember = "CODE";

            this.cbo회사.SelectedValue = Global.MainFrame.LoginInfo.CompanyCode;

            this.dtp기준일자.StartDateToString = Global.MainFrame.GetDateTimeToday().AddYears(-1).ToString("yyyyMMdd");
            this.dtp기준일자.EndDateToString = Global.MainFrame.GetStringToday;

            this.cbo화폐단위.DataSource = Global.MainFrame.GetComboDataCombine("N;FI_F000047");
            this.cbo화폐단위.ValueMember = "CODE";
            this.cbo화폐단위.DisplayMember = "NAME";
            this.cbo화폐단위.SelectedValue = "009";
            
            #region 월별수주실적
            this.chart월별수주실적.ChartFx.Titles.Add(new TitleDockable("▣ 월별 수주실적") { Alignment = StringAlignment.Near, Font = new Font("굴림체", 12, FontStyle.Bold) });
            this.chart월별수주실적.ChartFx.Gallery = Gallery.Lines;
            this.chart월별수주실적.ChartFx.AxisX.AutoScroll = true;
            this.chart월별수주실적.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Number;
            this.chart월별수주실적.ChartFx.LegendBox.Visible = true;
            this.chart월별수주실적.ChartFx.LegendBox.Dock = DockArea.Right;
            #endregion

            #region 분기별수주실적
            this.chart분기별수주실적.ChartFx.Titles.Add(new TitleDockable("▣ 분기별 수주실적") { Alignment = StringAlignment.Near, Font = new Font("굴림체", 12, FontStyle.Bold) });
            this.chart분기별수주실적.ChartFx.Gallery = Gallery.Lines;
            this.chart분기별수주실적.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Number;
            this.chart분기별수주실적.ChartFx.LegendBox.Visible = true;
            this.chart분기별수주실적.ChartFx.LegendBox.Dock = DockArea.Right;
            #endregion

            #region 분기별매출금액
            this.chart분기별매출금액.ChartFx.Titles.Add(new TitleDockable("▣ 분기별 매출금액") { Alignment = StringAlignment.Near, Font = new Font("굴림체", 12, FontStyle.Bold) });
            this.chart분기별매출금액.ChartFx.Gallery = Gallery.Lines;
            this.chart분기별매출금액.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Number;
            this.chart분기별매출금액.ChartFx.LegendBox.Visible = true;
            this.chart분기별매출금액.ChartFx.LegendBox.Dock = DockArea.Right;
            #endregion

            #region 분기별세전이익
            this.chart분기별세전이익.ChartFx.Titles.Add(new TitleDockable("▣ 분기별 세전이익") { Alignment = StringAlignment.Near, Font = new Font("굴림체", 12, FontStyle.Bold) });
            this.chart분기별세전이익.ChartFx.Gallery = Gallery.Lines;
            this.chart분기별세전이익.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Number;
            this.chart분기별세전이익.ChartFx.LegendBox.Visible = true;
            this.chart분기별세전이익.ChartFx.LegendBox.Dock = DockArea.Right;
            #endregion

            #region 통합
            this.chart통합.ChartFx.Titles.Add(new TitleDockable("▣ 분기별 수주/매출/세전이익") { Alignment = StringAlignment.Near, Font = new Font("굴림체", 12, FontStyle.Bold) });
            this.chart통합.ChartFx.Gallery = Gallery.Lines;
            this.chart통합.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Number;
            this.chart통합.ChartFx.LegendBox.Visible = true;
            this.chart통합.ChartFx.LegendBox.Dock = DockArea.Right;
            #endregion

            // 1분 : 60000
            this.timer.Interval = 60000; // 1분
        }

        private void InitEvent()
        {
            this.cbo회사.SelectedValueChanged += new EventHandler(this.cbo회사_SelectedValueChanged);
            this.btn손익계산서갱신.Click += new EventHandler(this.btn손익계산서갱신_Click);
            this.chk데이터그리드표시.CheckedChanged += new EventHandler(this.chk데이터그리드표시_CheckedChanged);
            this.chk타이머사용.CheckedChanged += new EventHandler(this.chk타이머사용여부_CheckedChanged);
            
            this.timer.Tick += new EventHandler(this.timer_Tick);
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            DataTable dt;
            
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!BeforeSearch()) return;

                if (this.tabControl1.SelectedIndex == 0)
                {
                    #region 월별수주실적
                    dt = this._biz.Search(new object[] { this.cbo회사.SelectedValue.ToString(),
                                                         this.tabControl1.SelectedIndex.ToString("D3"),
                                                         this.dtp기준일자.StartDateToString,
                                                         this.dtp기준일자.EndDateToString,
                                                         this.cbo화폐단위.SelectedValue.ToString() });

                    if (dt.Rows == null || dt.Rows.Count == 0)
                        this.chart월별수주실적.ChartFx.Data.Clear();
                    else
                    {
                        this.chart월별수주실적.DataSource = dt;
                        this.수주실적차트설정(this.chart월별수주실적);
                    }
                    #endregion
                }
                else if (this.tabControl1.SelectedIndex == 1)
                {
                    #region 분기별수주실적
                    dt = this._biz.Search(new object[] { this.cbo회사.SelectedValue.ToString(),
                                                         this.tabControl1.SelectedIndex.ToString("D3"),
                                                         this.dtp기준일자.StartDateToString,
                                                         this.dtp기준일자.EndDateToString,
                                                         this.cbo화폐단위.SelectedValue.ToString() });

                    if (dt.Rows == null || dt.Rows.Count == 0)
                        this.chart분기별수주실적.ChartFx.Data.Clear();
                    else
                    {
                        this.chart분기별수주실적.DataSource = dt;
                        this.수주실적차트설정(this.chart분기별수주실적);
                    }
                    #endregion
                }
                else if (this.tabControl1.SelectedIndex == 2)
                {
                    #region 분기별매출금액
                    dt = this._biz.Search(new object[] { this.cbo회사.SelectedValue.ToString(),
                                                         this.tabControl1.SelectedIndex.ToString("D3"),
                                                         this.dtp기준일자.StartDateToString,
                                                         this.dtp기준일자.EndDateToString,
                                                         this.cbo화폐단위.SelectedValue.ToString() });

                    if (dt.Rows == null || dt.Rows.Count == 0)
                        this.chart분기별매출금액.ChartFx.Data.Clear();
                    else
                    {
                        this.chart분기별매출금액.DataSource = dt;
                        this.chart분기별매출금액.ChartFx.Series[0].Text = "매출금액";
                    }
                    #endregion
                }
                else if (this.tabControl1.SelectedIndex == 3)
                {
                    #region 분기별세전이익
                    dt = this._biz.Search(new object[] { this.cbo회사.SelectedValue.ToString(),
                                                         this.tabControl1.SelectedIndex.ToString("D3"),
                                                         this.dtp기준일자.StartDateToString,
                                                         this.dtp기준일자.EndDateToString,
                                                         this.cbo화폐단위.SelectedValue.ToString() });

                    if (dt == null || dt.Rows.Count == 0)
                        this.chart분기별세전이익.ChartFx.Data.Clear();
                    else
                    {
                        this.chart분기별세전이익.DataSource = dt;
                        this.chart분기별세전이익.ChartFx.Series[0].Text = "세전이익";
                    }
                    #endregion
                }
                else
                {
                    #region 통합
                    dt = this._biz.Search(new object[] { this.cbo회사.SelectedValue.ToString(),
                                                         this.tabControl1.SelectedIndex.ToString("D3"),
                                                         this.dtp기준일자.StartDateToString,
                                                         this.dtp기준일자.EndDateToString,
                                                         this.cbo화폐단위.SelectedValue.ToString() });


                    if (dt == null || dt.Rows.Count == 0)
                        this.chart통합.ChartFx.Data.Clear();
                    else
                    {
                        this.chart통합.DataSource = dt;

                        this.chart통합.ChartFx.Series[0].Text = "수주금액";
                        this.chart통합.ChartFx.Series[1].Text = "매출금액";
                        this.chart통합.ChartFx.Series[2].Text = "세전이익";
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void cbo회사_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.cbo회사.SelectedValue.ToString() == "S100")
                    this.cbo화폐단위.SelectedValue = "005";
                else
                    this.cbo화폐단위.SelectedValue = "009";
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn손익계산서갱신_Click(object sender, EventArgs e)
        {   
            try
            {
                this.손익계산서갱신();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void chk데이터그리드표시_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.chk데이터그리드표시.Checked)
                {
                    this.chart월별수주실적.ChartFx.DataGrid.Visible = true;
                    this.chart분기별수주실적.ChartFx.DataGrid.Visible = true;
                    this.chart분기별매출금액.ChartFx.DataGrid.Visible = true;
                    this.chart분기별세전이익.ChartFx.DataGrid.Visible = true;
                    this.chart통합.ChartFx.DataGrid.Visible = true;
                }
                else
                {
                    this.chart월별수주실적.ChartFx.DataGrid.Visible = false;
                    this.chart분기별수주실적.ChartFx.DataGrid.Visible = false;
                    this.chart분기별매출금액.ChartFx.DataGrid.Visible = false;
                    this.chart분기별세전이익.ChartFx.DataGrid.Visible = false;
                    this.chart통합.ChartFx.DataGrid.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void chk타이머사용여부_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.chk타이머사용.Checked)
                {
                    this.전체조회();
                    this.timer.Start();
                }
                else
                    this.timer.Stop();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (this.tabControl1.SelectedIndex == 3)
                {
                    if (this.dtp기준일자.EndDateToString != Global.MainFrame.GetStringToday)
                    {
                        this.dtp기준일자.EndDateToString = Global.MainFrame.GetStringToday;
                        this.손익계산서갱신();
                        this.전체조회();
                    }

                    this.tabControl1.SelectedIndex = 0;
                }
                else
                    this.tabControl1.SelectedIndex += 1;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void 전체조회()
        {
            DataTable dt;

            try
            {
                #region 월별수주실적
                dt = this._biz.Search(new object[] { this.cbo회사.SelectedValue.ToString(),
                                                     "000",
                                                     this.dtp기준일자.StartDateToString,
                                                     this.dtp기준일자.EndDateToString,
                                                     this.cbo화폐단위.SelectedValue.ToString() });

                if (dt == null || dt.Rows.Count == 0)
                    this.chart월별수주실적.ChartFx.Data.Clear();
                else
                {
                    this.chart월별수주실적.DataSource = dt;
                    this.수주실적차트설정(this.chart월별수주실적);
                }
                #endregion

                #region 분기별수주실적
                dt = this._biz.Search(new object[] { this.cbo회사.SelectedValue.ToString(),
                                                     "001",
                                                     this.dtp기준일자.StartDateToString,
                                                     this.dtp기준일자.EndDateToString,
                                                     this.cbo화폐단위.SelectedValue.ToString() });

                if (dt.Rows == null || dt.Rows.Count == 0)
                    this.chart분기별수주실적.ChartFx.Data.Clear();
                else
                {
                    this.chart분기별수주실적.DataSource = dt;
                    this.수주실적차트설정(this.chart분기별수주실적);
                }
                #endregion

                #region 분기별매출금액
                dt = this._biz.Search(new object[] { this.cbo회사.SelectedValue.ToString(),
                                                     "002",
                                                     this.dtp기준일자.StartDateToString,
                                                     this.dtp기준일자.EndDateToString,
                                                     this.cbo화폐단위.SelectedValue.ToString() });

                if (dt == null || dt.Rows.Count == 0)
                    this.chart분기별매출금액.ChartFx.Data.Clear();
                else
                {
                    this.chart분기별매출금액.DataSource = dt;
                    this.chart분기별매출금액.ChartFx.Series[0].Text = "매출금액";
                }
                #endregion

                #region 분기별세전이익
                dt = this._biz.Search(new object[] { this.cbo회사.SelectedValue.ToString(),
                                                     "003",
                                                     this.dtp기준일자.StartDateToString,
                                                     this.dtp기준일자.EndDateToString,
                                                     this.cbo화폐단위.SelectedValue.ToString() });

                if (dt == null || dt.Rows.Count == 0)
                    this.chart분기별세전이익.ChartFx.Data.Clear();
                else
                {
                    this.chart분기별세전이익.DataSource = dt;
                    this.chart분기별세전이익.ChartFx.Series[0].Text = "세전이익";
                }
                #endregion

                #region 통합
                dt = this._biz.Search(new object[] { this.cbo회사.SelectedValue.ToString(),
                                                     "004",
                                                     this.dtp기준일자.StartDateToString,
                                                     this.dtp기준일자.EndDateToString,
                                                     this.cbo화폐단위.SelectedValue.ToString() });


                if (dt == null || dt.Rows.Count == 0)
                    this.chart통합.ChartFx.Data.Clear();
                else
                {
                    this.chart통합.DataSource = dt;

                    this.chart통합.ChartFx.Series[0].Text = "수주금액";
                    this.chart통합.ChartFx.Series[1].Text = "매출금액";
                    this.chart통합.ChartFx.Series[2].Text = "세전이익";
                }
                #endregion
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void 손익계산서갱신()
        {
            P_FI_PL_BASE pFiPlBase;
            DataTable dt, dt1;
            DataSet dataSet;
            DataRow dr, dr1;
            Dictionary<int, string[]> 기간데이터;
            List<string> stringList;
            Setting setting;
            DataSetting dataSetting;
            string 회계단위, 기준일자;

            try
            {
                if (this.cbo회사.SelectedValue.ToString() == "K100")
                    회계단위 = "010000|";
                else if (this.cbo회사.SelectedValue.ToString() == "K200")
                    회계단위 = "020000|";
                else if (this.cbo회사.SelectedValue.ToString() == "S100")
                    회계단위 = "030000|";
                else
                    회계단위 = string.Empty;

                pFiPlBase = new P_FI_PL_BASE(this.PageID, "D0032", "1", "0");

                if (this.dtp기준일자.StartDate.CompareTo(Convert.ToDateTime("2016-01-01")) <= 0)
                    기준일자 = "20160101";
                else
                    기준일자 = this.dtp기준일자.StartDateToString.Substring(0, 6);
                
                //월(1) 단위 조회
                기간데이터 = new 기간계산(1, 기준일자.Substring(0, 6), this.dtp기준일자.EndDateToString.Substring(0, 6), false).년월Dic;

                setting = new Setting(기간데이터, false, 기준일자.Substring(0, 6));
                setting.SetDtAcct = pFiPlBase.GetDtAcct;

                MsgControl.ShowMsg("데이터 조회 중... (1/3)");
                dataSet = this._biz.Search1(new object[] { "GAAP",
                                                           this.cbo회사.SelectedValue.ToString(),
                                                           회계단위,
                                                           "D0032",
                                                           기준일자,
                                                           this.dtp기준일자.EndDateToString,
                                                           Global.SystemLanguage.MultiLanguageLpoint });

                setting.SetDsData = dataSet;

                MsgControl.ShowMsg("손익계산서 생성중... (2/3)");
                setting.SetTable(this._biz.회계환경설정_결산평가손익환급(this.cbo회사.SelectedValue.ToString()));

                //원(001) 단위 조회
                dataSetting = new DataSetting(this.PageID, "0", "001");
                dataSetting.Set기준Col = "L_AM_CUR";

                stringList = new List<string>();
                stringList.Add("L_AM_CUR");
                foreach (int key in 기간데이터.Keys)
                    stringList.Add("AM_CUR" + D.GetInt(key));

                dataSetting.Set금액Cols = stringList.ToArray();
                dataSetting.Set주당손익Cols = new string[] { "STOCK_L_AM_CUR_PIPE" };

                dt = setting.GetDt;

                MsgControl.ShowMsg("데이터 집계 중... (3/3)");
                dataSetting.SetTable(dt);
                setting.SetModify(dt);
                dt.AcceptChanges();

                dr = dt.Select("CD_PACCT = '580'")[0];
                dt1 = new DataTable();

                dt1.Columns.Add("DT_ACCT", typeof(string));
                dt1.Columns.Add("AM_ACCT", typeof(decimal));

                foreach (int key in 기간데이터.Keys)
                {
                    dr1 = dt1.NewRow();

                    dr1["DT_ACCT"] = 기간데이터[key][0];
                    dr1["AM_ACCT"] = dr["AM_CUR" + D.GetInt(key)];

                    dt1.Rows.Add(dr1);
                }

                this._biz.Save(dt1, this.cbo회사.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
            }
        }

        private void 수주실적차트설정(UChart chart)
        {
            try
            {
                if (this.cbo회사.SelectedValue.ToString() == "K100")
                {
                    #region 수주금액
                    chart.ChartFx.Series[0].Text = "영업1부";
                    chart.ChartFx.Series[1].Text = "영업2부";
                    chart.ChartFx.Series[2].Text = "기술영업부";
                    chart.ChartFx.Series[3].Text = "전체";
                    chart.ChartFx.Series[4].Text = "영업1팀";
                    chart.ChartFx.Series[5].Text = "영업2팀";
                    chart.ChartFx.Series[6].Text = "영업3팀";
                    chart.ChartFx.Series[7].Text = "기술서비스팀";
                    chart.ChartFx.Series[8].Text = "해양1팀";
                    chart.ChartFx.Series[9].Text = "해양2팀";
                    chart.ChartFx.Series[10].Text = "국내영업팀";

                    if (this.chk상세보기.Checked)
                    {
                        chart.ChartFx.Series[0].Visible = false;
                        chart.ChartFx.Series[1].Visible = false;
                        chart.ChartFx.Series[2].Visible = true;
                        chart.ChartFx.Series[3].Visible = true;
                        chart.ChartFx.Series[4].Visible = true;
                        chart.ChartFx.Series[5].Visible = true;
                        chart.ChartFx.Series[6].Visible = true;
                        chart.ChartFx.Series[7].Visible = true;
                        chart.ChartFx.Series[8].Visible = true;
                        chart.ChartFx.Series[9].Visible = true;
                        chart.ChartFx.Series[10].Visible = true;
                    }
                    else
                    {
                        chart.ChartFx.Series[0].Visible = true;
                        chart.ChartFx.Series[1].Visible = true;
                        chart.ChartFx.Series[2].Visible = true;
                        chart.ChartFx.Series[3].Visible = true;
                        chart.ChartFx.Series[4].Visible = false;
                        chart.ChartFx.Series[5].Visible = false;
                        chart.ChartFx.Series[6].Visible = false;
                        chart.ChartFx.Series[7].Visible = false;
                        chart.ChartFx.Series[8].Visible = false;
                        chart.ChartFx.Series[9].Visible = false;
                        chart.ChartFx.Series[10].Visible = false;
                    }
                    #endregion

                    #region 수주목표
                    chart.ChartFx.Series[11].Text = "영업1부-목표";
                    chart.ChartFx.Series[12].Text = "영업2부-목표";
                    chart.ChartFx.Series[13].Text = "기술영업부-목표";
                    chart.ChartFx.Series[14].Text = "전체-목표";
                    chart.ChartFx.Series[15].Text = "영업1팀-목표";
                    chart.ChartFx.Series[16].Text = "영업2팀-목표";
                    chart.ChartFx.Series[17].Text = "영업3팀-목표";
                    chart.ChartFx.Series[18].Text = "기술서비스팀-목표";
                    chart.ChartFx.Series[19].Text = "해양1팀-목표";
                    chart.ChartFx.Series[20].Text = "해양2팀-목표";
                    chart.ChartFx.Series[21].Text = "국내영업팀-목표";

                    if (this.chk목표보기.Checked)
                    {
                        if (this.chk상세보기.Checked)
                        {
                            chart.ChartFx.Series[11].Visible = false;
                            chart.ChartFx.Series[12].Visible = false;
                            chart.ChartFx.Series[13].Visible = true;
                            chart.ChartFx.Series[14].Visible = true;
                            chart.ChartFx.Series[15].Visible = true;
                            chart.ChartFx.Series[16].Visible = true;
                            chart.ChartFx.Series[17].Visible = true;
                            chart.ChartFx.Series[18].Visible = true;
                            chart.ChartFx.Series[19].Visible = true;
                            chart.ChartFx.Series[20].Visible = true;
                            chart.ChartFx.Series[21].Visible = true;
                        }
                        else
                        {
                            chart.ChartFx.Series[11].Visible = true;
                            chart.ChartFx.Series[12].Visible = true;
                            chart.ChartFx.Series[13].Visible = true;
                            chart.ChartFx.Series[14].Visible = true;
                            chart.ChartFx.Series[15].Visible = false;
                            chart.ChartFx.Series[16].Visible = false;
                            chart.ChartFx.Series[17].Visible = false;
                            chart.ChartFx.Series[18].Visible = false;
                            chart.ChartFx.Series[19].Visible = false;
                            chart.ChartFx.Series[20].Visible = false;
                            chart.ChartFx.Series[21].Visible = false;
                        }
                    }
                    else
                    {
                        chart.ChartFx.Series[11].Visible = false;
                        chart.ChartFx.Series[12].Visible = false;
                        chart.ChartFx.Series[13].Visible = false;
                        chart.ChartFx.Series[14].Visible = false;
                        chart.ChartFx.Series[15].Visible = false;
                        chart.ChartFx.Series[16].Visible = false;
                        chart.ChartFx.Series[17].Visible = false;
                        chart.ChartFx.Series[18].Visible = false;
                        chart.ChartFx.Series[19].Visible = false;
                        chart.ChartFx.Series[20].Visible = false;
                        chart.ChartFx.Series[21].Visible = false;
                    }
                    #endregion

                    #region 전년동월
                    chart.ChartFx.Series[22].Text = "영업1부-전년동월";
                    chart.ChartFx.Series[23].Text = "영업2부-전년동월";
                    chart.ChartFx.Series[24].Text = "기술영업부-전년동월";
                    chart.ChartFx.Series[25].Text = "전체-전년동월";
                    chart.ChartFx.Series[26].Text = "영업1팀-전년동월";
                    chart.ChartFx.Series[27].Text = "영업2팀-전년동월";
                    chart.ChartFx.Series[28].Text = "영업3팀-전년동월";
                    chart.ChartFx.Series[29].Text = "기술서비스팀-전년동월";
                    chart.ChartFx.Series[30].Text = "해양1팀-전년동월";
                    chart.ChartFx.Series[31].Text = "해양2팀-전년동월";
                    chart.ChartFx.Series[32].Text = "국내영업팀-전년동월";

                    if (this.chk전년동월보기.Checked)
                    {
                        if (this.chk상세보기.Checked)
                        {
                            chart.ChartFx.Series[22].Visible = false;
                            chart.ChartFx.Series[23].Visible = false;
                            chart.ChartFx.Series[24].Visible = true;
                            chart.ChartFx.Series[25].Visible = true;
                            chart.ChartFx.Series[26].Visible = true;
                            chart.ChartFx.Series[27].Visible = true;
                            chart.ChartFx.Series[28].Visible = true;
                            chart.ChartFx.Series[29].Visible = true;
                            chart.ChartFx.Series[30].Visible = true;
                            chart.ChartFx.Series[31].Visible = true;
                            chart.ChartFx.Series[32].Visible = true;
                        }
                        else
                        {
                            chart.ChartFx.Series[22].Visible = true;
                            chart.ChartFx.Series[23].Visible = true;
                            chart.ChartFx.Series[24].Visible = true;
                            chart.ChartFx.Series[25].Visible = true;
                            chart.ChartFx.Series[26].Visible = false;
                            chart.ChartFx.Series[27].Visible = false;
                            chart.ChartFx.Series[28].Visible = false;
                            chart.ChartFx.Series[29].Visible = false;
                            chart.ChartFx.Series[30].Visible = false;
                            chart.ChartFx.Series[31].Visible = false;
                            chart.ChartFx.Series[32].Visible = false;
                        }
                    }
                    else
                    {
                        chart.ChartFx.Series[22].Visible = false;
                        chart.ChartFx.Series[23].Visible = false;
                        chart.ChartFx.Series[24].Visible = false;
                        chart.ChartFx.Series[25].Visible = false;
                        chart.ChartFx.Series[26].Visible = false;
                        chart.ChartFx.Series[27].Visible = false;
                        chart.ChartFx.Series[28].Visible = false;
                        chart.ChartFx.Series[29].Visible = false;
                        chart.ChartFx.Series[30].Visible = false;
                        chart.ChartFx.Series[31].Visible = false;
                        chart.ChartFx.Series[32].Visible = false;
                    }
                    #endregion
                }
                else
                {
                    chart.ChartFx.Series[0].Text = "수주금액";
                    chart.ChartFx.Series[1].Text = "수주목표";
                    chart.ChartFx.Series[2].Text = "전년동월";

                    if (this.chk목표보기.Checked)
                        chart.ChartFx.Series[1].Visible = true;
                    else
                        chart.ChartFx.Series[1].Visible = false;

                    if (this.chk전년동월보기.Checked)
                        chart.ChartFx.Series[2].Visible = true;
                    else
                        chart.ChartFx.Series[2].Visible = false;
                }   
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
    }
}

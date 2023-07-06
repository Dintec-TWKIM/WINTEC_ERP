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
using Duzon.ERPU.MF;
using Duzon.DASS.Erpu.Windows.FX;
using Duzon.Common.BpControls;

namespace cz
{
    public partial class P_CZ_SA_CRM_CHART : PageBase
    {
        P_CZ_SA_CRM_CHART_BIZ _biz;

        public P_CZ_SA_CRM_CHART()
        {
            StartUp.Certify(this);
            InitializeComponent();
        }

        public P_CZ_SA_CRM_CHART(string 거래처코드, string 거래처명, string 사원번호, string 사원명)
        {
            StartUp.Certify(this);
            InitializeComponent();

            this.bpc거래처.AddItem(거래처코드, 거래처명);

            this.ctx사원.CodeValue = 사원번호;
            this.ctx사원.CodeName = 사원명;
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this._biz = new P_CZ_SA_CRM_CHART_BIZ();

            this.InitEvent();
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.dtp조회기간.StartDateToString = Global.MainFrame.GetDateTimeToday().AddYears(-1).ToString("yyyyMMdd");
            this.dtp조회기간.EndDateToString = Global.MainFrame.GetStringToday;

            this.cbo차트유형.DataSource = Global.MainFrame.GetComboDataCombine("N;CZ_SA00038");
            this.cbo차트유형.ValueMember = "CODE";
            this.cbo차트유형.DisplayMember = "NAME";

            this.cbo품목군.DataSource = DBHelper.GetDataTable(@"SELECT '' AS CODE,
                                                                       '' AS NAME
                                                                UNION ALL
                                                                SELECT CD_ITEMGRP AS CODE,
                                                                	   NM_ITEMGRP AS NAME
                                                                FROM MA_ITEMGRP WITH(NOLOCK)
                                                                WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                               "AND USE_YN = 'Y'");
            this.cbo품목군.ValueMember = "CODE";
            this.cbo품목군.DisplayMember = "NAME";

            this.cbo조회단위.DataSource = MA.GetCodeUser(new string[] { "000", "001", "002" }, new string[] { "년단위", "월단위", "일단위" });
            this.cbo조회단위.ValueMember = "CODE";
            this.cbo조회단위.DisplayMember = "NAME";
            this.cbo조회단위.SelectedValue = "001";

            #region 전체

            #region 수주금액
            this.chart수주금액.ChartFx.Gallery = Gallery.Bar;
            this.chart수주금액.ChartFx.View3D.Enabled = true;
            this.chart수주금액.ChartFx.LegendBox.Visible = true;

            this.chart수주금액.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Currency;
            this.chart수주금액.ChartFx.AxisY.CustomGridLines.Add(new CustomGridLine(300000000, "\\300,000,000"));

            this.chart수주금액.ChartFx.AxisX.AutoScroll = true;
            this.chart수주금액.ChartFx.AxisX.MaxSizePercentage = 20;
            this.chart수주금액.ChartFx.AxisX.LabelTrimming = StringTrimming.EllipsisWord;

            this.chart수주금액.ChartFx.Panes.Add(new Pane());
            this.chart수주금액.ChartFx.Panes[1].AxisY.LabelsFormat.Format = AxisFormat.Number;
            #endregion

            #region 발주금액
            this.chart발주금액.ChartFx.Gallery = Gallery.Bar;
            this.chart발주금액.ChartFx.View3D.Enabled = true;

            this.chart발주금액.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Currency;

            this.chart발주금액.ChartFx.AxisX.AutoScroll = true;
            this.chart발주금액.ChartFx.AxisX.MaxSizePercentage = 20;
            this.chart발주금액.ChartFx.AxisX.LabelTrimming = StringTrimming.EllipsisWord;

            this.chart발주금액.ChartFx.DataGrid.Visible = true;
            this.chart발주금액.ChartFx.DataGrid.Dock = DockArea.Right;
            #endregion

            #region 이윤
            this.chart이윤.ChartFx.Gallery = Gallery.Bar;
            this.chart이윤.ChartFx.View3D.Enabled = true;

            this.chart이윤.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Currency;

            this.chart이윤.ChartFx.AxisX.AutoScroll = true;
            this.chart이윤.ChartFx.AxisX.MaxSizePercentage = 20;
            this.chart이윤.ChartFx.AxisX.LabelTrimming = StringTrimming.EllipsisWord;

            this.chart이윤.ChartFx.LegendBox.Visible = true;

            this.chart이윤.ChartFx.Panes.Add(new Pane());
            this.chart이윤.ChartFx.Panes.Add(new Pane());
            this.chart이윤.ChartFx.Panes[1].AxisY.LabelsFormat.Format = AxisFormat.Currency;
            this.chart이윤.ChartFx.Panes[2].AxisY.LabelsFormat.Decimals = 2;
            this.chart이윤.ChartFx.Panes[2].AxisY.CustomGridLines.Add(new CustomGridLine(15, "15%"));
            #endregion

            #region 수주율
            this.chart수주율.ChartFx.Gallery = Gallery.Bar;
            this.chart수주율.ChartFx.View3D.Enabled = true;

            this.chart수주율.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Number;

            this.chart수주율.ChartFx.AxisX.AutoScroll = true;
            this.chart수주율.ChartFx.AxisX.MaxSizePercentage = 20;
            this.chart수주율.ChartFx.AxisX.LabelTrimming = StringTrimming.EllipsisWord;

            this.chart수주율.ChartFx.LegendBox.Visible = true;

            this.chart수주율.ChartFx.Panes.Add(new Pane());
            this.chart수주율.ChartFx.Panes.Add(new Pane());

            this.chart수주율.ChartFx.Panes[2].AxisY.LabelsFormat.Decimals = 2;
            this.chart수주율.ChartFx.Panes[2].AxisY.CustomGridLines.Add(new CustomGridLine(50, "50%"));
            #endregion
            
            #endregion

            #region 매출

            #region 수주율

            #region 매출처기간별수주율
            this.chart매출처기간별수주율.ChartFx.Gallery = Gallery.Lines;

            this.chart매출처기간별수주율.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Number;

            this.chart매출처기간별수주율.ChartFx.AxisX.AutoScroll = true;

            this.chart매출처기간별수주율.ChartFx.LegendBox.Visible = true;

            this.chart매출처기간별수주율.ChartFx.Panes.Add(new Pane());
            this.chart매출처기간별수주율.ChartFx.Panes[1].AxisY.LabelsFormat.Decimals = 2;
            #endregion

            #region 매출처품목군별수주율
            this.chart매출처품목군별수주율.ChartFx.Gallery = Gallery.Bar;

            this.chart매출처품목군별수주율.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Number;

            this.chart매출처품목군별수주율.ChartFx.AxisX.AutoScroll = true;

            this.chart매출처품목군별수주율.ChartFx.LegendBox.Visible = true;

            this.chart매출처품목군별수주율.ChartFx.Panes.Add(new Pane());
            this.chart매출처품목군별수주율.ChartFx.Panes[1].AxisY.LabelsFormat.Decimals = 2;
            #endregion

            #region 매출처이윤별수주율
            this.chart매출처이윤별수주율.ChartFx.Gallery = Gallery.Bar;

            this.chart매출처이윤별수주율.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Number;
            this.chart매출처이윤별수주율.ChartFx.AxisY.LabelsFormat.Decimals = 2;

            this.chart매출처이윤별수주율.ChartFx.AxisX.AutoScroll = true;

            this.chart매출처이윤별수주율.ChartFx.LegendBox.Visible = true;

            this.chart매출처이윤별수주율.ChartFx.Panes.Add(new Pane());
            this.chart매출처이윤별수주율.ChartFx.Panes[1].AxisY.LabelsFormat.Format = AxisFormat.Number;
            this.chart매출처이윤별수주율.ChartFx.Panes[1].AxisY.LabelsFormat.Decimals = 2;
            this.chart매출처이윤별수주율.ChartFx.Panes[1].AxisY.CustomGridLines.Add(new CustomGridLine(0, "전체수주율") { Color = Color.Red });
            this.chart매출처이윤별수주율.ChartFx.Panes[1].AxisY.CustomGridLines[0].ShowText = false;
            #endregion

            #region 매출처납기별수주율
            this.chart매출처납기별수주율.ChartFx.Gallery = Gallery.Bar;

            this.chart매출처납기별수주율.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Number;
            this.chart매출처납기별수주율.ChartFx.AxisY.LabelsFormat.Decimals = 2;

            this.chart매출처납기별수주율.ChartFx.AxisX.AutoScroll = true;

            this.chart매출처납기별수주율.ChartFx.LegendBox.Visible = true;

            this.chart매출처납기별수주율.ChartFx.Panes.Add(new Pane());
            this.chart매출처납기별수주율.ChartFx.Panes[1].AxisY.LabelsFormat.Format = AxisFormat.Number;
            this.chart매출처납기별수주율.ChartFx.Panes[1].AxisY.LabelsFormat.Decimals = 2;
            this.chart매출처납기별수주율.ChartFx.Panes[1].AxisY.CustomGridLines.Add(new CustomGridLine(0, "전체수주율") { Color = Color.Red });
            this.chart매출처납기별수주율.ChartFx.Panes[1].AxisY.CustomGridLines[0].ShowText = false;
            #endregion

            #endregion

            #region 이윤율

            #region 매출처기간별이윤율견적
            this.chart매출처기간별이윤율견적.ChartFx.Gallery = Gallery.Lines;

            this.chart매출처기간별이윤율견적.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Currency;

            this.chart매출처기간별이윤율견적.ChartFx.AxisX.AutoScroll = true;
            this.chart매출처기간별이윤율견적.ChartFx.AxisX.MaxSizePercentage = 20;
            this.chart매출처기간별이윤율견적.ChartFx.AxisX.LabelTrimming = StringTrimming.EllipsisWord;

            this.chart매출처기간별이윤율견적.ChartFx.LegendBox.Visible = true;

            this.chart매출처기간별이윤율견적.ChartFx.Panes.Add(new Pane());
            this.chart매출처기간별이윤율견적.ChartFx.Panes.Add(new Pane());
            this.chart매출처기간별이윤율견적.ChartFx.Panes[1].AxisY.LabelsFormat.Format = AxisFormat.Currency;
            this.chart매출처기간별이윤율견적.ChartFx.Panes[2].AxisY.LabelsFormat.Decimals = 2;
            #endregion

            #region 매출처품목군별이윤율견적
            this.chart매출처품목군별이윤율견적.ChartFx.Gallery = Gallery.Bar;

            this.chart매출처품목군별이윤율견적.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Currency;

            this.chart매출처품목군별이윤율견적.ChartFx.AxisX.AutoScroll = true;
            this.chart매출처품목군별이윤율견적.ChartFx.AxisX.MaxSizePercentage = 20;
            this.chart매출처품목군별이윤율견적.ChartFx.AxisX.LabelTrimming = StringTrimming.EllipsisWord;

            this.chart매출처품목군별이윤율견적.ChartFx.LegendBox.Visible = true;

            this.chart매출처품목군별이윤율견적.ChartFx.Panes.Add(new Pane());
            this.chart매출처품목군별이윤율견적.ChartFx.Panes.Add(new Pane());
            this.chart매출처품목군별이윤율견적.ChartFx.Panes[1].AxisY.LabelsFormat.Format = AxisFormat.Currency;
            this.chart매출처품목군별이윤율견적.ChartFx.Panes[2].AxisY.LabelsFormat.Decimals = 2;
            #endregion

            #region 매출처기간별이윤율수주
            this.chart매출처기간별이윤율수주.ChartFx.Gallery = Gallery.Lines;

            this.chart매출처기간별이윤율수주.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Currency;

            this.chart매출처기간별이윤율수주.ChartFx.AxisX.AutoScroll = true;
            this.chart매출처기간별이윤율수주.ChartFx.AxisX.MaxSizePercentage = 20;
            this.chart매출처기간별이윤율수주.ChartFx.AxisX.LabelTrimming = StringTrimming.EllipsisWord;

            this.chart매출처기간별이윤율수주.ChartFx.LegendBox.Visible = true;

            this.chart매출처기간별이윤율수주.ChartFx.Panes.Add(new Pane());
            this.chart매출처기간별이윤율수주.ChartFx.Panes.Add(new Pane());
            this.chart매출처기간별이윤율수주.ChartFx.Panes[1].AxisY.LabelsFormat.Format = AxisFormat.Currency;
            this.chart매출처기간별이윤율수주.ChartFx.Panes[2].AxisY.LabelsFormat.Decimals = 2;
            #endregion

            #region 매출처품목군별이윤율수주
            this.chart매출처품목군별이윤율수주.ChartFx.Gallery = Gallery.Bar;

            this.chart매출처품목군별이윤율수주.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Currency;

            this.chart매출처품목군별이윤율수주.ChartFx.AxisX.AutoScroll = true;
            this.chart매출처품목군별이윤율수주.ChartFx.AxisX.MaxSizePercentage = 20;
            this.chart매출처품목군별이윤율수주.ChartFx.AxisX.LabelTrimming = StringTrimming.EllipsisWord;

            this.chart매출처품목군별이윤율수주.ChartFx.LegendBox.Visible = true;

            this.chart매출처품목군별이윤율수주.ChartFx.Panes.Add(new Pane());
            this.chart매출처품목군별이윤율수주.ChartFx.Panes.Add(new Pane());
            this.chart매출처품목군별이윤율수주.ChartFx.Panes[1].AxisY.LabelsFormat.Format = AxisFormat.Currency;
            this.chart매출처품목군별이윤율수주.ChartFx.Panes[2].AxisY.LabelsFormat.Decimals = 2;
            #endregion

            #endregion

            #region 기타

            #region 매입처정보
            this.chart매입처정보.ChartFx.Gallery = Gallery.Bar;

            this.chart매입처정보.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Number;
            this.chart매입처정보.ChartFx.AxisX.MaxSizePercentage = 20;
            this.chart매입처정보.ChartFx.AxisX.LabelTrimming = StringTrimming.EllipsisWord;

            this.chart매입처정보.ChartFx.AxisX.AutoScroll = true;

            this.chart매입처정보.ChartFx.LegendBox.Visible = true;

            this.chart매입처정보.ChartFx.Panes.Add(new Pane());
            this.chart매입처정보.ChartFx.Panes.Add(new Pane());
            this.chart매입처정보.ChartFx.Panes[1].AxisY.LabelsFormat.Format = AxisFormat.Currency;
            this.chart매입처정보.ChartFx.Panes[2].AxisY.LabelsFormat.Decimals = 2;
            #endregion

            #region 미수금
            this.chart미수금.ChartFx.Gallery = Gallery.Lines;
            this.chart미수금.ChartFx.LegendBox.Visible = true;

            this.chart미수금.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Currency;
            this.chart미수금.ChartFx.AxisX.AutoScroll = true;

            this.chart미수금.ChartFx.Panes.Add(new Pane());
            this.chart미수금.ChartFx.Panes.Add(new Pane());
            this.chart미수금.ChartFx.Panes[1].AxisY.LabelsFormat.Format = AxisFormat.Currency;
            this.chart미수금.ChartFx.Panes[2].AxisY.LabelsFormat.Decimals = 2;
            #endregion
            
            #endregion
            
            #endregion

            #region 매입

            #region 월별거래실적
            this.chart매입처거래실적.ChartFx.Gallery = Gallery.Lines;
            this.chart매입처거래실적.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Currency;
            this.chart매입처거래실적.ChartFx.AxisX.AutoScroll = true;
            this.chart매입처거래실적.ChartFx.LegendBox.Visible = true;
            #endregion

            #region 월별납기
            this.chart매입처납기.ChartFx.Gallery = Gallery.Lines;
            this.chart매입처납기.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Number;
            this.chart매입처납기.ChartFx.AxisX.AutoScroll = true;
            this.chart매입처납기.ChartFx.LegendBox.Visible = true;

            this.chart매입처납기.ChartFx.Panes.Add(new Pane());
            this.chart매입처납기.ChartFx.Panes[1].AxisY.LabelsFormat.Decimals = 2;
            #endregion

            #region 미지급금
            this.chart미지급금.ChartFx.Gallery = Gallery.Lines;
            this.chart미지급금.ChartFx.LegendBox.Visible = true;

            this.chart미지급금.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Currency;
            this.chart미지급금.ChartFx.AxisX.AutoScroll = true;

            this.chart미지급금.ChartFx.Panes.Add(new Pane());
            this.chart미지급금.ChartFx.Panes.Add(new Pane());
            this.chart미지급금.ChartFx.Panes[1].AxisY.LabelsFormat.Format = AxisFormat.Currency;
            this.chart미지급금.ChartFx.Panes[2].AxisY.LabelsFormat.Decimals = 2;
            #endregion

            #region 매출처정보

            #region 선택율
            this.chart매출처정보선택율.ChartFx.Gallery = Gallery.Lines;
            this.chart매출처정보선택율.ChartFx.LegendBox.Visible = true;

            this.chart매출처정보선택율.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Number;
            this.chart매출처정보선택율.ChartFx.AxisX.AutoScroll = true;

            this.chart매출처정보선택율.ChartFx.Panes.Add(new Pane());
            this.chart매출처정보선택율.ChartFx.Panes[1].AxisY.LabelsFormat.Decimals = 2;
            #endregion

            #region 수주율
            this.chart매출처정보수주율.ChartFx.Gallery = Gallery.Lines;
            this.chart매출처정보수주율.ChartFx.LegendBox.Visible = true;

            this.chart매출처정보수주율.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Number;
            this.chart매출처정보수주율.ChartFx.AxisX.AutoScroll = true;

            this.chart매출처정보수주율.ChartFx.Panes.Add(new Pane());
            this.chart매출처정보수주율.ChartFx.Panes[1].AxisY.LabelsFormat.Decimals = 2;
            #endregion

            #region 이윤율
            this.chart매출처정보이윤율.ChartFx.Gallery = Gallery.Lines;
            this.chart매출처정보이윤율.ChartFx.LegendBox.Visible = true;

            this.chart매출처정보이윤율.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Currency;
            this.chart매출처정보이윤율.ChartFx.AxisX.AutoScroll = true;

            this.chart매출처정보이윤율.ChartFx.Panes.Add(new Pane());
            this.chart매출처정보이윤율.ChartFx.Panes.Add(new Pane());
            this.chart매출처정보이윤율.ChartFx.Panes[1].AxisY.LabelsFormat.Format = AxisFormat.Currency;
            this.chart매출처정보이윤율.ChartFx.Panes[2].AxisY.LabelsFormat.Format = AxisFormat.Number;
            this.chart매출처정보이윤율.ChartFx.Panes[2].AxisY.LabelsFormat.Decimals = 2;
            #endregion

            #endregion

            #endregion

            #region 호선

            #region 매출처별호선
            this.chart매출처별호선.ChartFx.Gallery = Gallery.Bar;
            this.chart매출처별호선.ChartFx.View3D.Enabled = true;

            this.chart매출처별호선.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Number;

            this.chart매출처별호선.ChartFx.AxisX.AutoScroll = true;
            this.chart매출처별호선.ChartFx.AxisX.MaxSizePercentage = 20;
            this.chart매출처별호선.ChartFx.AxisX.LabelTrimming = StringTrimming.EllipsisWord;

            this.chart매출처별호선.ChartFx.DataGrid.Visible = true;
            this.chart매출처별호선.ChartFx.DataGrid.Dock = DockArea.Right;
            #endregion

            #region 호선별이윤
            this.chart호선별이윤율.ChartFx.Gallery = Gallery.Bar;
            this.chart호선별이윤율.ChartFx.View3D.Enabled = true;

            this.chart호선별이윤율.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Currency;

            this.chart호선별이윤율.ChartFx.AxisX.AutoScroll = true;
            this.chart호선별이윤율.ChartFx.AxisX.MaxSizePercentage = 20;
            this.chart호선별이윤율.ChartFx.AxisX.LabelTrimming = StringTrimming.EllipsisWord;

            this.chart호선별이윤율.ChartFx.LegendBox.Visible = true;

            this.chart호선별이윤율.ChartFx.Panes.Add(new Pane());
            this.chart호선별이윤율.ChartFx.Panes.Add(new Pane());
            this.chart호선별이윤율.ChartFx.Panes[1].AxisY.LabelsFormat.Format = AxisFormat.Currency;
            this.chart호선별이윤율.ChartFx.Panes[2].AxisY.LabelsFormat.Format = AxisFormat.Number;
            this.chart호선별이윤율.ChartFx.Panes[2].AxisY.LabelsFormat.Decimals = 2;
            #endregion

            #region 호선별수주율
            this.chart호선별수주율.ChartFx.Gallery = Gallery.Bar;
            this.chart호선별수주율.ChartFx.View3D.Enabled = true;

            this.chart호선별수주율.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Number;

            this.chart호선별수주율.ChartFx.AxisX.AutoScroll = true;
            this.chart호선별수주율.ChartFx.AxisX.MaxSizePercentage = 20;
            this.chart호선별수주율.ChartFx.AxisX.LabelTrimming = StringTrimming.EllipsisWord;

            this.chart호선별수주율.ChartFx.LegendBox.Visible = true;

            this.chart호선별수주율.ChartFx.Panes.Add(new Pane());
            this.chart호선별수주율.ChartFx.Panes.Add(new Pane());

            this.chart호선별수주율.ChartFx.Panes[1].AxisY.LabelsFormat.Format = AxisFormat.Currency;
            this.chart호선별수주율.ChartFx.Panes[2].AxisY.LabelsFormat.Decimals = 2;
            #endregion

            #endregion

            #region 클레임
            
            #region 클레임발생율
            this.chart클레임발생율.ChartFx.Gallery = Gallery.Bar;

            this.chart클레임발생율.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Number;

            this.chart클레임발생율.ChartFx.AxisX.AutoScroll = true;

            this.chart클레임발생율.ChartFx.LegendBox.Visible = true;

            this.chart클레임발생율.ChartFx.Panes.Add(new Pane());
            this.chart클레임발생율.ChartFx.Panes.Add(new Pane());
            this.chart클레임발생율.ChartFx.Panes[1].AxisY.LabelsFormat.Format = AxisFormat.Currency;
            this.chart클레임발생율.ChartFx.Panes[2].AxisY.LabelsFormat.Decimals = 2;
            #endregion

            #region 클레임사유
            this.chart클레임사유.ChartFx.Gallery = Gallery.Pie;
            this.chart클레임사유.ChartFx.View3D.Enabled = true;
            this.chart클레임사유.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Number;
            this.chart클레임사유.ChartFx.AxisX.AutoScroll = true;

            this.chart원인구분.ChartFx.Gallery = Gallery.Pie;
            this.chart원인구분.ChartFx.View3D.Enabled = true;
            this.chart원인구분.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Number;
            this.chart원인구분.ChartFx.AxisX.AutoScroll = true;

            this.chart항목분류.ChartFx.Gallery = Gallery.Pie;
            this.chart항목분류.ChartFx.View3D.Enabled = true;
            this.chart항목분류.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Number;
            this.chart항목분류.ChartFx.AxisX.AutoScroll = true;
            #endregion

            #endregion

            #region 사원

            #region 사원별이윤율
            this.chart사원별이윤율.ChartFx.Gallery = Gallery.Bar;
            this.chart사원별이윤율.ChartFx.LegendBox.Visible = true;

            this.chart사원별이윤율.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Currency;
            this.chart사원별이윤율.ChartFx.AxisX.AutoScroll = true;

            this.chart사원별이윤율.ChartFx.Panes.Add(new Pane());
            this.chart사원별이윤율.ChartFx.Panes.Add(new Pane());
            this.chart사원별이윤율.ChartFx.Panes[1].AxisY.LabelsFormat.Format = AxisFormat.Currency;
            this.chart사원별이윤율.ChartFx.Panes[2].AxisY.LabelsFormat.Format = AxisFormat.Number;
            this.chart사원별이윤율.ChartFx.Panes[2].AxisY.LabelsFormat.Decimals = 2;
            #endregion

            #region 사원별수주율
            this.chart사원별수주율.ChartFx.Gallery = Gallery.Bar;
            this.chart사원별수주율.ChartFx.LegendBox.Visible = true;

            this.chart사원별수주율.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Currency;
            this.chart사원별수주율.ChartFx.AxisX.AutoScroll = true;

            this.chart사원별수주율.ChartFx.Panes.Add(new Pane());
            this.chart사원별수주율.ChartFx.Panes.Add(new Pane());
            this.chart사원별수주율.ChartFx.Panes[1].AxisY.LabelsFormat.Format = AxisFormat.Number;
            this.chart사원별수주율.ChartFx.Panes[2].AxisY.LabelsFormat.Format = AxisFormat.Number;
            this.chart사원별수주율.ChartFx.Panes[2].AxisY.LabelsFormat.Decimals = 2;
            #endregion

            #endregion

            #region 키워드

            #region 주제
            this.chart주제.ChartFx.Gallery = Gallery.Lines;
            this.chart주제.ChartFx.LegendBox.Visible = true;

            this.chart주제.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Number;

            this.chart주제.ChartFx.AxisX.AutoScroll = true;
            #endregion

            #region 품목명
            this.chart품목명.ChartFx.Gallery = Gallery.Lines;
            this.chart품목명.ChartFx.LegendBox.Visible = true;

            this.chart품목명.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Number;

            this.chart품목명.ChartFx.AxisX.AutoScroll = true;
            #endregion

            #region 재고
            this.chart재고.ChartFx.Gallery = Gallery.Lines;
            this.chart재고.ChartFx.LegendBox.Visible = true;

            this.chart재고.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Number;

            this.chart재고.ChartFx.AxisX.AutoScroll = true;
            #endregion

            #region 발주
            this.chart발주.ChartFx.Gallery = Gallery.Lines;
            this.chart발주.ChartFx.LegendBox.Visible = true;

            this.chart발주.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Number;

            this.chart발주.ChartFx.AxisX.AutoScroll = true;
            #endregion
            
            #endregion

            if (!string.IsNullOrEmpty(this.ctx사원.CodeValue) || 
                !string.IsNullOrEmpty(this.bpc거래처.QueryWhereIn_Pipe))
                this.OnToolBarSearchButtonClicked(null, null);
        }

        private void InitEvent()
        {
            this.cbo차트유형.SelectedIndexChanged += new EventHandler(this.cbo차트유형_SelectedIndexChanged);
            this.ctx호선.QueryBefore += new BpQueryHandler(this.ctx호선_QueryBefore);

            #region 전체
            this.btn수주금액크게보기.Click += new EventHandler(this.btn크게보기_Click);
            this.btn발주금액크게보기.Click += new EventHandler(this.btn크게보기_Click);
            this.btn이윤크게보기.Click += new EventHandler(this.btn크게보기_Click);
            this.btn수주율크게보기.Click += new EventHandler(this.btn크게보기_Click);

            this.btn수주금액.Click += new EventHandler(this.btn수주금액_Click);
            this.btn발주금액.Click += new EventHandler(this.btn발주금액_Click);
            this.btn이윤.Click += new EventHandler(this.btn이윤_Click);
            this.btn수주율.Click += new EventHandler(this.btn수주율_Click);

            this._flex수주금액.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
            this._flex발주금액.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
            this._flex이윤.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
            this._flex수주율.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
            #endregion

            #region 매출
            
            #region 수주율
            this.btn매출처기간별수주율크게보기.Click += new EventHandler(this.btn크게보기_Click);
            this.btn매출처품목군별수주율크게보기.Click += new EventHandler(this.btn크게보기_Click);
            this.btn매출처이윤별수주율크게보기.Click += new EventHandler(this.btn크게보기_Click);
            this.btn매출처납기별수주율크게보기.Click += new EventHandler(this.btn크게보기_Click);

            this.btn매출처기간별수주율.Click += new EventHandler(this.btn매출처기간별수주율_Click);
            this.btn매출처품목군별수주율.Click += new EventHandler(this.btn매출처품목군별수주율_Click);
            this.btn매출처이윤별수주율.Click += new EventHandler(this.btn매출처이윤별수주율_Click);
            this.btn매출처납기별수주율.Click += new EventHandler(this.btn매출처납기별수주율_Click);
            #endregion

            #region 이윤율
            this.btn매출처기간별이윤율견적크게보기.Click += new EventHandler(this.btn크게보기_Click);
            this.btn매출처품목군별이윤율견적크게보기.Click += new EventHandler(this.btn크게보기_Click);
            this.btn매출처기간별이윤율수주크게보기.Click += new EventHandler(this.btn크게보기_Click);
            this.btn매출처품목군별이윤율수주크게보기.Click += new EventHandler(this.btn크게보기_Click);

            this.btn매출처기간별이윤율견적.Click += new EventHandler(this.btn매출처기간별이윤율견적_Click);
            this.btn매출처품목군별이윤율견적.Click += new EventHandler(this.btn매출처품목군별이윤율견적_Click);
            this.btn매출처기간별이윤율수주.Click += new EventHandler(this.btn매출처기간별이윤율수주_Click);
            this.btn매출처품목군별이윤율수주.Click += new EventHandler(this.btn매출처품목군별이윤율수주_Click);
            #endregion

            #region 기타
            this.btn매입처정보크게보기.Click += new EventHandler(this.btn크게보기_Click);
            this.btn미수금크게보기.Click += new EventHandler(this.btn크게보기_Click);

            this.btn매입처정보.Click += new EventHandler(this.btn매입처정보_Click);
            this.btn미수금.Click += new EventHandler(this.btn미수금_Click);

            this._flex매입처정보.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
            #endregion

            #endregion

            #region 매입
            this.btn매입처거래실적크게보기.Click += new EventHandler(this.btn크게보기_Click);
            this.btn매입처납기크게보기.Click += new EventHandler(this.btn크게보기_Click);
            this.btn매출처정보선택율크게보기.Click += new EventHandler(this.btn크게보기_Click);
            this.btn매출처정보수주율크게보기.Click += new EventHandler(this.btn크게보기_Click);
            this.btn매출처정보이윤율크게보기.Click += new EventHandler(this.btn크게보기_Click);
            this.btn미지급금크게보기.Click += new EventHandler(this.btn크게보기_Click);

            this.btn매입처거래실적.Click += new EventHandler(this.btn매입처거래실적_Click);
            this.btn매입처납기.Click += new EventHandler(this.btn매입처납기_Click);
            this.btn매출처정보.Click += new EventHandler(this.btn매출처정보_Click);
            this.btn미지급금.Click += new EventHandler(this.btn미지급금_Click);
            #endregion

            #region 호선
            this.btn매출처별호선크게보기.Click += new EventHandler(this.btn크게보기_Click);
            this.btn호선별이윤율크게보기.Click += new EventHandler(this.btn크게보기_Click);
            this.btn호선별수주율크게보기.Click += new EventHandler(this.btn크게보기_Click);

            this.btn매출처별호선.Click += new EventHandler(this.btn매출처별호선_Click);
            this.btn호선별이윤율.Click += new EventHandler(this.btn호선별이윤율_Click);
            this.btn호선별수주율.Click += new EventHandler(this.btn호선별수주율_Click);
            this.btn거래내역.Click += new EventHandler(this.btn거래내역_Click);

            this._flex매출처별호선.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
            this._flex호선별이윤율.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
            this._flex호선별수주율.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
            this._flex거래내역.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
            #endregion

            #region 클레임
            this.btn클레임사유크게보기.Click += new EventHandler(this.btn크게보기_Click);
            this.btn원인구분크게보기.Click += new EventHandler(this.btn크게보기_Click);
            this.btn항목분류크게보기.Click += new EventHandler(this.btn크게보기_Click);
            this.btn클레임발생율크게보기.Click += new EventHandler(this.btn크게보기_Click);

            this.btn클레임발생율.Click += new EventHandler(this.btn클레임발생율_Click);
            this.btn클레임유형.Click += new EventHandler(this.btn클레임유형_Click);

            this._flex클레임유형.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
            #endregion

            #region 사원
            this.btn사원별이윤율크게보기.Click += new EventHandler(this.btn크게보기_Click);
            this.btn사원별수주율크게보기.Click += new EventHandler(this.btn크게보기_Click);

            this.btn사원별이윤율.Click += new EventHandler(this.btn사원별이윤율_Click);
            this.btn사원별수주율.Click += new EventHandler(this.btn사원별수주율_Click);

            this._flex사원별이윤율.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
            this._flex사원별수주율.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
            #endregion

            #region 키워드
            this.btn주제크게보기.Click += new EventHandler(this.btn크게보기_Click);
            this.btn품목명크게보기.Click += new EventHandler(this.btn크게보기_Click);
            this.btn재고크게보기.Click += new EventHandler(this.btn크게보기_Click);
            this.btn발주크게보기.Click += new EventHandler(this.btn크게보기_Click);

            this.btn주제.Click += new EventHandler(this.btn주제_Click);
            this.btn품목명.Click += new EventHandler(this.btn품목명_Click);
            this.btn재고.Click += new EventHandler(this.btn재고_Click);
            this.btn발주.Click += new EventHandler(this.btn발주_Click);

            this._flex주제키워드.AfterRowChange += new RangeEventHandler(this._flex주제키워드_AfterRowChange);
            this._flex품목명키워드.AfterRowChange += new RangeEventHandler(this._flex품목명키워드_AfterRowChange);
            this._flex재고.AfterRowChange += new RangeEventHandler(this._flex재고_AfterRowChange);
            this._flex발주키워드.AfterRowChange += new RangeEventHandler(this._flex발주키워드_AfterRowChange);

            this._flex주제리스트.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
            this._flex품목명리스트.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
            this._flex발주리스트.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
            #endregion
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!this.BeforeSearch()) return;

                switch (this.cbo차트유형.SelectedIndex)
                {
                    case 0: // 전체
                        this.btn수주금액_Click(null, null);
                        this.btn발주금액_Click(null, null);
                        this.btn이윤_Click(null, null);
                        this.btn수주율_Click(null, null);
                        break;
                    case 1: // 매출
                        #region 이윤율
                        this.btn매출처기간별이윤율견적_Click(null, null);
                        this.btn매출처품목군별이윤율견적_Click(null, null);
                        this.btn매출처기간별이윤율수주_Click(null, null);
                        this.btn매출처품목군별이윤율수주_Click(null, null);
                        #endregion

                        #region 수주율
                        this.btn매출처기간별수주율_Click(null, null);
                        this.btn매출처품목군별수주율_Click(null, null);
                        this.btn매출처이윤별수주율_Click(null, null);
                        this.btn매출처납기별수주율_Click(null, null);
                        #endregion

                        #region 기타
                        this.btn매입처정보_Click(null, null);
                        this.btn미수금_Click(null, null);
                        #endregion
                        break;
                    case 2: // 매입
                        this.btn매입처거래실적_Click(null, null);
                        this.btn매입처납기_Click(null, null);
                        this.btn매출처정보_Click(null, null);
                        this.btn미지급금_Click(null, null);
                        break;
                    case 3: // 호선
                        this.btn매출처별호선_Click(null, null);
                        this.btn거래내역_Click(null, null);
                        this.btn호선별이윤율_Click(null, null);
                        this.btn호선별수주율_Click(null, null);
                        break;
                    case 4: // 클레임
                        this.btn클레임유형_Click(null, null);
                        this.btn클레임발생율_Click(null, null);
                        break;
                    case 5: // 사원
                        this.btn사원별이윤율_Click(null, null);
                        this.btn사원별수주율_Click(null, null);
                        break;
                    case 6: // 키워드
                        this.btn주제_Click(null, null);
                        this.btn품목명_Click(null, null);
                        this.btn재고_Click(null, null);
                        this.btn발주_Click(null, null);
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void cbo차트유형_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.pnl차트유형.TitleText = this.cbo차트유형.Text;
                this.tabControl1.SelectedIndex = this.cbo차트유형.SelectedIndex;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void ctx호선_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                e.HelpParam.UserParams = "호선;H_CZ_MA_HULL_SUB";
                e.HelpParam.P14_CD_PARTNER = this.bpc거래처.SelectedValue.ToString();
                e.HelpParam.P34_CD_MNG = this.bpc거래처.SelectedText;
                e.HelpParam.P35_CD_MNGD = "Y";
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #region 전체
        private void btn크게보기_Click(object sender, EventArgs e)
        {
            string name;
            UChart chart = null;
            Control control = null;

            try
            {
                name = ((Control)sender).Name;

                #region 전체
                if (this.btn수주금액크게보기.Name == name)
                {
                    chart = this.chart수주금액;
                    control = this.tpg수주금액차트;
                }
                else if (this.btn발주금액크게보기.Name == name)
                {
                    chart = this.chart발주금액;
                    control = this.tpg발주금액차트;
                }
                else if (this.btn이윤크게보기.Name == name)
                {
                    chart = this.chart이윤;
                    control = this.tpg이윤차트;
                }
                else if (this.btn수주율크게보기.Name == name)
                {
                    chart = this.chart수주율;
                    control = this.tpg수주율차트;
                }
                #endregion

                #region 매출
                else if (this.btn매출처기간별이윤율수주크게보기.Name == name)
                {
                    chart = this.chart매출처기간별이윤율수주;
                    control = this.tpg매출처기간별이윤율수주차트;
                }
                else if (this.btn매출처품목군별이윤율수주크게보기.Name == name)
                {
                    chart = this.chart매출처품목군별이윤율수주;
                    control = this.tpg매출처품목군별이윤율수주차트;
                }
                else if (this.btn매출처기간별수주율크게보기.Name == name)
                {
                    chart = this.chart매출처기간별수주율;
                    control = this.tpg매출처기간별수주율차트;
                }
                else if (this.btn매출처품목군별수주율크게보기.Name == name)
                {
                    chart = this.chart매출처품목군별수주율;
                    control = this.tpg매출처품목군별수주율차트;
                }
                else if (this.btn매입처정보크게보기.Name == name)
                {
                    chart = this.chart매입처정보;
                    control = this.tpg매입처정보차트;
                }
                else if (this.btn매출처이윤별수주율크게보기.Name == name)
                {
                    chart = this.chart매출처이윤별수주율;
                    control = this.tpg매출처이윤별수주율차트;
                }
                else if (this.btn매출처납기별수주율크게보기.Name == name)
                {
                    chart = this.chart매출처납기별수주율;
                    control = this.tpg매출처납기별수주율차트;
                }
                else if (this.btn미수금크게보기.Name == name)
                {
                    chart = this.chart미수금;
                    control = this.tpg미수금차트;
                }
                #endregion

                #region 매입
                else if (this.btn매입처거래실적크게보기.Name == name)
                {
                    chart = this.chart매입처거래실적;
                    control = this.tpg매입처거래실적차트;
                }
                else if (this.btn매입처납기크게보기.Name == name)
                {
                    chart = this.chart매입처납기;
                    control = this.tpg매입처납기차트;
                }
                else if (this.btn매출처정보선택율크게보기.Name == name)
                {
                    chart = this.chart매출처정보선택율;
                    control = this.tpg매출처정보선택율;
                }
                else if (this.btn매출처정보수주율크게보기.Name == name)
                {
                    chart = this.chart매출처정보수주율;
                    control = this.tpg매출처정보수주율;
                }
                else if (this.btn매출처정보이윤율크게보기.Name == name)
                {
                    chart = this.chart매출처정보이윤율;
                    control = this.tpg매출처정보이윤율;
                }
                else if (this.btn미지급금크게보기.Name == name)
                {
                    chart = this.chart미지급금;
                    control = this.tpg미지급금차트;
                }
                #endregion

                #region 호선
                else if (this.btn매출처별호선크게보기.Name == name)
                {
                    chart = this.chart매출처별호선;
                    control = this.tpg매출처별호선차트;
                }
                else if (this.btn호선별이윤율크게보기.Name == name)
                {
                    chart = this.chart호선별이윤율;
                    control = this.tpg호선별이윤율차트;
                }
                else if (this.btn호선별수주율크게보기.Name == name)
                {
                    chart = this.chart호선별수주율;
                    control = this.tpg호선별수주율차트;
                }
                #endregion

                #region 클레임
                else if (this.btn클레임사유크게보기.Name == name)
                {
                    chart = this.chart클레임사유;
                    control = this.tpg클레임사유;
                }
                else if (this.btn원인구분크게보기.Name == name)
                {
                    chart = this.chart원인구분;
                    control = this.tpg원인구분;
                }
                else if (this.btn항목분류크게보기.Name == name)
                {
                    chart = this.chart항목분류;
                    control = this.tpg항목분류;
                }
                else if (this.btn클레임발생율크게보기.Name == name)
                {
                    chart = this.chart클레임발생율;
                    control = this.tpg클레임발생율차트;
                }
                #endregion

                #region 사원
                else if (this.btn사원별이윤율크게보기.Name == name)
                {
                    chart = this.chart사원별이윤율;
                    control = this.tpg사원별이윤율차트;
                }
                else if (this.btn사원별수주율크게보기.Name == name)
                {
                    chart = this.chart사원별수주율;
                    control = this.tpg사원별수주율차트;
                }
                #endregion

                #region 키워드
                else if (this.btn주제크게보기.Name == name)
                {
                    chart = this.chart주제;
                    control = this.tpg주제차트;
                }
                else if (this.btn품목명크게보기.Name == name)
                {
                    chart = this.chart품목명;
                    control = this.tpg품목명차트;
                }
                else if (this.btn재고크게보기.Name == name)
                {
                    chart = this.chart재고;
                    control = this.split재고.Panel2;
                }
                else if (this.btn발주크게보기.Name == name)
                {
                    chart = this.chart발주;
                    control = this.tpg발주차트;
                }
                #endregion

                P_CZ_SA_CRM_CHART_SUB dialog = new P_CZ_SA_CRM_CHART_SUB(chart);

                dialog.ShowDialog();

                control.Controls.Add(dialog.Controls[0]);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn수주금액_Click(object sender, EventArgs e)
        {
            DataTable dt;
            
            try
            {
                dt = this._biz.Search(this.조회조건(), "A01").Tables[0];

                if (dt.Rows == null || dt.Rows.Count == 0)
                    this.chart수주금액.ChartFx.Data.Clear();
                else
                {
                    #region 리스트
                    if (this._flex수주금액.DataTable == null)
                    {
                        this._flex수주금액.BeginSetting(1, 1, false);

                        this._flex수주금액.SetCol("CD_PARTNER", "거래처코드", 100);
                        this._flex수주금액.SetCol("LN_PARTNER", "거래처명", 300);
                        this._flex수주금액.SetCol("AM_SO", "수주금액(원화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex수주금액.SetCol("AM_EX_SO", "수주금액(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

                        this._flex수주금액.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
                    }
                    #endregion

                    this.chart수주금액.DataSource = dt.Copy();

                    this.chart수주금액.ChartFx.Series[0].Color = Color.Red;
                    this.chart수주금액.ChartFx.Series[1].Color = Color.Tomato;

                    this.chart수주금액.ChartFx.Series[0].Text = "수주금액(원화)";
                    this.chart수주금액.ChartFx.Series[1].Text = "수주금액(외화)";

                    this.chart수주금액.ChartFx.Series[1].Pane = this.chart수주금액.ChartFx.Panes[1];
                }

                this._flex수주금액.Binding = dt;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn발주금액_Click(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                dt = this._biz.Search(this.조회조건(), "A02").Tables[0];

                if (dt.Rows == null || dt.Rows.Count == 0)
                    this.chart발주금액.ChartFx.Data.Clear();
                else
                {
                    #region 리스트
                    if (this._flex발주금액.DataTable == null)
                    {
                        this._flex발주금액.BeginSetting(1, 1, false);

                        this._flex발주금액.SetCol("CD_PARTNER", "거래처코드", 100);
                        this._flex발주금액.SetCol("LN_PARTNER", "거래처명", 300);
                        this._flex발주금액.SetCol("AM_PO", "발주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

                        this._flex발주금액.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
                    }
                    #endregion

                    this.chart발주금액.DataSource = dt.Copy();

                    this.chart발주금액.ChartFx.Series[0].Color = Color.Blue;
                    this.chart발주금액.ChartFx.Series[0].Text = "발주금액";
                }

                this._flex발주금액.Binding = dt;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn이윤_Click(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                dt = this._biz.Search(this.조회조건(), "A03").Tables[0];

                if (dt.Rows == null || dt.Rows.Count == 0)
                    this.chart이윤.ChartFx.Data.Clear();
                else
                {
                    #region 리스트
                    if (this._flex이윤.DataTable == null)
                    {
                        this._flex이윤.BeginSetting(1, 1, false);

                        this._flex이윤.SetCol("CD_PARTNER", "거래처코드", 100);
                        this._flex이윤.SetCol("LN_PARTNER", "거래처명", 300);
                        this._flex이윤.SetCol("AM_SO", "수주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex이윤.SetCol("AM_PO", "발주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex이윤.SetCol("AM_PROFIT", "이윤", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex이윤.SetCol("RT_PROFIT", "이윤율", 100, false, typeof(decimal), FormatTpType.RATE);

                        this._flex이윤.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

                        this._flex이윤.SetExceptSumCol("RT_PROFIT");
                    }
                    #endregion

                    this.chart이윤.DataSource = dt.Copy();

                    this.chart이윤.ChartFx.Series[0].Text = "수주금액";
                    this.chart이윤.ChartFx.Series[1].Text = "발주금액";
                    this.chart이윤.ChartFx.Series[2].Text = "이윤";
                    this.chart이윤.ChartFx.Series[3].Text = "이윤율";

                    this.chart이윤.ChartFx.Series[2].Pane = this.chart이윤.ChartFx.Panes[1];
                    this.chart이윤.ChartFx.Series[3].Pane = this.chart이윤.ChartFx.Panes[2];
                    this.chart이윤.ChartFx.Series[3].PointLabels.Visible = true;
                }

                this._flex이윤.Binding = dt;

                decimal 수주금액 = D.GetDecimal(this._flex이윤.DataTable.Compute("SUM(AM_SO)", string.Empty));
                decimal 이윤 = D.GetDecimal(this._flex이윤.DataTable.Compute("SUM(AM_PROFIT)", string.Empty));

                this._flex이윤[this._flex이윤.Rows.Fixed - 1, "RT_PROFIT"] = string.Format("{0:" + this._flex이윤.Cols["RT_PROFIT"].Format + "}", (수주금액 == 0 ? 0 : Decimal.Round(((이윤 / 수주금액) * 100), 2, MidpointRounding.AwayFromZero)));
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn수주율_Click(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                dt = this._biz.Search(this.조회조건(), "A04").Tables[0];

                if (dt.Rows == null || dt.Rows.Count == 0)
                    this.chart수주율.ChartFx.Data.Clear();
                else
                {
                    #region 리스트
                    if (this._flex수주율.DataTable == null)
                    {
                        this._flex수주율.BeginSetting(1, 1, false);

                        this._flex수주율.SetCol("CD_PARTNER", "거래처코드", 100);
                        this._flex수주율.SetCol("LN_PARTNER", "거래처명", 300);
                        this._flex수주율.SetCol("QT_QTN_FILE", "견적건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                        this._flex수주율.SetCol("QT_SO_FILE", "수주건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                        this._flex수주율.SetCol("QT_QTN_ITEM", "견적종수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                        this._flex수주율.SetCol("QT_SO_ITEM", "수주종수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                        this._flex수주율.SetCol("RT_SO_FILE", "수주율(건수)", 100, false, typeof(decimal), FormatTpType.RATE);
                        this._flex수주율.SetCol("RT_SO_ITEM", "수주율(종수)", 100, false, typeof(decimal), FormatTpType.RATE);

                        this._flex수주율.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

                        this._flex수주율.SetExceptSumCol("RT_SO_FILE", "RT_SO_ITEM");
                    }
                    #endregion

                    this.chart수주율.DataSource = dt.Copy();

                    this.chart수주율.ChartFx.Series[0].Text = "견적건수";
                    this.chart수주율.ChartFx.Series[1].Text = "수주건수";
                    this.chart수주율.ChartFx.Series[2].Text = "견적종수";
                    this.chart수주율.ChartFx.Series[3].Text = "수주종수";
                    this.chart수주율.ChartFx.Series[4].Text = "수주율(건수)";
                    this.chart수주율.ChartFx.Series[5].Text = "수주율(종수)";

                    this.chart수주율.ChartFx.Series[2].Pane = this.chart수주율.ChartFx.Panes[1];
                    this.chart수주율.ChartFx.Series[3].Pane = this.chart수주율.ChartFx.Panes[1];
                    this.chart수주율.ChartFx.Series[4].Pane = this.chart수주율.ChartFx.Panes[2];
                    this.chart수주율.ChartFx.Series[5].Pane = this.chart수주율.ChartFx.Panes[2];
                }

                this._flex수주율.Binding = dt;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region 매출

        #region 수주율
        private void btn매출처기간별수주율_Click(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                dt = this._biz.Search(this.조회조건(), "P01").Tables[0];

                if (dt.Rows == null || dt.Rows.Count == 0)
                    this.chart매출처기간별수주율.ChartFx.Data.Clear();
                else
                {
                    #region 리스트
                    if (this._flex매출처기간별수주율.DataTable == null)
                    {
                        this._flex매출처기간별수주율.BeginSetting(1, 1, false);

                        this._flex매출처기간별수주율.SetCol("DT_QTN", "견적일자", 80);
                        this._flex매출처기간별수주율.SetCol("QT_QTN_FILE", "견적건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                        this._flex매출처기간별수주율.SetCol("QT_SO_FILE", "수주건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                        this._flex매출처기간별수주율.SetCol("AM_QTN", "견적금액(원화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매출처기간별수주율.SetCol("AM_EX_QTN", "견적금액(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매출처기간별수주율.SetCol("AM_SO", "수주금액(원화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매출처기간별수주율.SetCol("AM_EX_SO", "수주금액(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매출처기간별수주율.SetCol("RT_SO_FILE", "수주율(건수)", 100, false, typeof(decimal), FormatTpType.RATE);
                        this._flex매출처기간별수주율.SetCol("RT_SO_AM", "수주율(원화)", 100, false, typeof(decimal), FormatTpType.RATE);
                        this._flex매출처기간별수주율.SetCol("RT_SO_EX_AM", "수주율(외화)", 100, false, typeof(decimal), FormatTpType.RATE);

                        this._flex매출처기간별수주율.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

                        this._flex매출처기간별수주율.SetExceptSumCol("RT_SO_FILE", "RT_SO_AM", "RT_SO_EX_AM");
                    }
                    #endregion

                    this.chart매출처기간별수주율.DataSource = dt.Copy();

                    this.chart매출처기간별수주율.ChartFx.Series[0].Text = "견적건수";
                    this.chart매출처기간별수주율.ChartFx.Series[1].Text = "수주건수";
                    this.chart매출처기간별수주율.ChartFx.Series[2].Visible = false;
                    this.chart매출처기간별수주율.ChartFx.Series[3].Visible = false;
                    this.chart매출처기간별수주율.ChartFx.Series[4].Visible = false;
                    this.chart매출처기간별수주율.ChartFx.Series[5].Visible = false;
                    this.chart매출처기간별수주율.ChartFx.Series[6].Text = "수주율";
                    this.chart매출처기간별수주율.ChartFx.Series[7].Visible = false;
                    this.chart매출처기간별수주율.ChartFx.Series[8].Visible = false;

                    this.chart매출처기간별수주율.ChartFx.Series[6].Pane = this.chart매출처기간별수주율.ChartFx.Panes[1];
                    this.chart매출처기간별수주율.ChartFx.Series[6].Gallery = Gallery.Lines;
                    this.chart매출처기간별수주율.ChartFx.Series[6].PointLabels.Visible = true;
                }

                this._flex매출처기간별수주율.Binding = dt;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn매출처품목군별수주율_Click(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                dt = this._biz.Search(this.조회조건(), "P02").Tables[0];

                if (dt.Rows == null || dt.Rows.Count == 0)
                    this.chart매출처품목군별수주율.ChartFx.Data.Clear();
                else
                {
                    #region 리스트
                    if (this._flex매출처품목군별수주율.DataTable == null)
                    {
                        this._flex매출처품목군별수주율.BeginSetting(1, 1, false);

                        this._flex매출처품목군별수주율.SetCol("NM_ITEMGRP", "품목군", 80);
                        this._flex매출처품목군별수주율.SetCol("QT_QTN_FILE", "견적건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                        this._flex매출처품목군별수주율.SetCol("QT_SO_FILE", "수주건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                        this._flex매출처품목군별수주율.SetCol("AM_QTN", "견적금액(원화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매출처품목군별수주율.SetCol("AM_EX_QTN", "견적금액(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매출처품목군별수주율.SetCol("AM_SO", "수주금액(원화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매출처품목군별수주율.SetCol("AM_EX_SO", "수주금액(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매출처품목군별수주율.SetCol("RT_SO_FILE", "수주율(건수)", 100, false, typeof(decimal), FormatTpType.RATE);
                        this._flex매출처품목군별수주율.SetCol("RT_SO_AM", "수주율(원화)", 100, false, typeof(decimal), FormatTpType.RATE);
                        this._flex매출처품목군별수주율.SetCol("RT_SO_EX_AM", "수주율(외화)", 100, false, typeof(decimal), FormatTpType.RATE);

                        this._flex매출처품목군별수주율.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

                        this._flex매출처품목군별수주율.SetExceptSumCol("RT_SO_FILE", "RT_SO_AM", "RT_SO_EX_AM");
                    }
                    #endregion

                    this.chart매출처품목군별수주율.DataSource = dt.Copy();

                    this.chart매출처품목군별수주율.ChartFx.Series[0].Text = "견적건수";
                    this.chart매출처품목군별수주율.ChartFx.Series[1].Text = "수주건수";
                    this.chart매출처품목군별수주율.ChartFx.Series[2].Visible = false;
                    this.chart매출처품목군별수주율.ChartFx.Series[3].Visible = false;
                    this.chart매출처품목군별수주율.ChartFx.Series[4].Visible = false;
                    this.chart매출처품목군별수주율.ChartFx.Series[5].Visible = false;
                    this.chart매출처품목군별수주율.ChartFx.Series[6].Text = "수주율";
                    this.chart매출처품목군별수주율.ChartFx.Series[7].Visible = false;
                    this.chart매출처품목군별수주율.ChartFx.Series[8].Visible = false;

                    this.chart매출처품목군별수주율.ChartFx.Series[6].Pane = this.chart매출처품목군별수주율.ChartFx.Panes[1];
                    this.chart매출처품목군별수주율.ChartFx.Series[6].Gallery = Gallery.Bar;
                    this.chart매출처품목군별수주율.ChartFx.Series[6].PointLabels.Visible = true;
                }

                this._flex매출처품목군별수주율.Binding = dt;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn매출처이윤별수주율_Click(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                dt = this._biz.Search(this.조회조건(), "P03").Tables[0];

                if (dt == null || dt.Rows.Count == 0)
                    this.chart매출처이윤별수주율.ChartFx.Data.Clear();
                else
                {
                    #region 리스트
                    if (this._flex매출처이윤별수주율.DataTable == null)
                    {
                        this._flex매출처이윤별수주율.BeginSetting(1, 1, false);

                        this._flex매출처이윤별수주율.SetCol("RT_PROFIT", "이윤율", 100);
                        this._flex매출처이윤별수주율.SetCol("RT_QT_QTN", "견적비율", 100, false, typeof(decimal), FormatTpType.RATE);
                        this._flex매출처이윤별수주율.SetCol("RT_QT_SO", "수주비율", 100, false, typeof(decimal), FormatTpType.RATE);
                        this._flex매출처이윤별수주율.SetCol("QT_QTN", "견적건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                        this._flex매출처이윤별수주율.SetCol("QT_SO", "수주건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                        this._flex매출처이윤별수주율.SetCol("RT_SO", "수주율", 100, false, typeof(decimal), FormatTpType.RATE);

                        this._flex매출처이윤별수주율.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

                        this._flex매출처이윤별수주율.SetExceptSumCol("RT_QT_QTN", "RT_QT_SO", "RT_SO");
                    }
                    #endregion

                    this.chart매출처이윤별수주율.DataSource = dt.Copy();

                    this.chart매출처이윤별수주율.ChartFx.Series[0].Text = "견적비율";
                    this.chart매출처이윤별수주율.ChartFx.Series[1].Text = "수주비율";
                    this.chart매출처이윤별수주율.ChartFx.Series[2].Visible = false;
                    this.chart매출처이윤별수주율.ChartFx.Series[3].Visible = false;
                    this.chart매출처이윤별수주율.ChartFx.Series[4].Text = "수주율";
                    this.chart매출처이윤별수주율.ChartFx.Series[5].Visible = false;

                    this.chart매출처이윤별수주율.ChartFx.Series[4].Pane = this.chart매출처이윤별수주율.ChartFx.Panes[1];
                    this.chart매출처이윤별수주율.ChartFx.Series[4].Gallery = Gallery.Bar;
                    this.chart매출처이윤별수주율.ChartFx.Series[4].PointLabels.Visible = true;

                    this.chart매출처이윤별수주율.ChartFx.Panes[1].AxisY.CustomGridLines[0].Value = Convert.ToDouble(dt.Rows[0]["RT_SO_ALL"]);
                    this.chart매출처이윤별수주율.ChartFx.Panes[1].AxisY.CustomGridLines[0].Text = dt.Rows[0]["RT_SO_ALL"].ToString();
                }

                this._flex매출처이윤별수주율.Binding = dt;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn매출처납기별수주율_Click(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                dt = this._biz.Search(this.조회조건(), "P04").Tables[0];

                if (dt.Rows == null || dt.Rows.Count == 0)
                    this.chart매출처납기별수주율.ChartFx.Data.Clear();
                else
                {
                    #region 리스트
                    if (this._flex매출처납기별수주율.DataTable == null)
                    {
                        this._flex매출처납기별수주율.BeginSetting(1, 1, false);

                        this._flex매출처납기별수주율.SetCol("LT", "납기", 100);
                        this._flex매출처납기별수주율.SetCol("RT_QT_QTN", "견적비율", 100, false, typeof(decimal), FormatTpType.RATE);
                        this._flex매출처납기별수주율.SetCol("RT_QT_SO", "수주비율", 100, false, typeof(decimal), FormatTpType.RATE);
                        this._flex매출처납기별수주율.SetCol("QT_QTN", "견적종수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                        this._flex매출처납기별수주율.SetCol("QT_SO", "수주종수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                        this._flex매출처납기별수주율.SetCol("RT_SO", "수주율", 100, false, typeof(decimal), FormatTpType.RATE);

                        this._flex매출처납기별수주율.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

                        this._flex매출처납기별수주율.SetExceptSumCol("RT_QT_QTN", "RT_QT_SO", "RT_SO");
                    }
                    #endregion

                    this.chart매출처납기별수주율.DataSource = dt.Copy();

                    this.chart매출처납기별수주율.ChartFx.Series[0].Text = "견적비율";
                    this.chart매출처납기별수주율.ChartFx.Series[1].Text = "수주비율";
                    this.chart매출처납기별수주율.ChartFx.Series[2].Visible = false;
                    this.chart매출처납기별수주율.ChartFx.Series[3].Visible = false;
                    this.chart매출처납기별수주율.ChartFx.Series[4].Text = "수주율";
                    this.chart매출처납기별수주율.ChartFx.Series[5].Visible = false;

                    this.chart매출처납기별수주율.ChartFx.Series[4].Pane = this.chart매출처납기별수주율.ChartFx.Panes[1];
                    this.chart매출처납기별수주율.ChartFx.Series[4].Gallery = Gallery.Bar;
                    this.chart매출처납기별수주율.ChartFx.Series[4].PointLabels.Visible = true;

                    this.chart매출처납기별수주율.ChartFx.Panes[1].AxisY.CustomGridLines[0].Value = Convert.ToDouble(dt.Rows[0]["RT_SO_ALL"]);
                    this.chart매출처납기별수주율.ChartFx.Panes[1].AxisY.CustomGridLines[0].Text = dt.Rows[0]["RT_SO_ALL"].ToString();
                }

                this._flex매출처납기별수주율.Binding = dt;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region 이윤율
        private void btn매출처기간별이윤율견적_Click(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                dt = this._biz.Search(this.조회조건(), "P05").Tables[0];

                if (dt.Rows == null || dt.Rows.Count == 0)
                    this.chart매출처기간별이윤율견적.ChartFx.Data.Clear();
                else
                {
                    #region 리스트
                    if (this._flex매출처기간별이윤율견적.DataTable == null)
                    {
                        this._flex매출처기간별이윤율견적.BeginSetting(1, 1, false);

                        this._flex매출처기간별이윤율견적.SetCol("DT_QTN", "견적일자", 80);
                        this._flex매출처기간별이윤율견적.SetCol("AM_QTN", "매출금액(원화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매출처기간별이윤율견적.SetCol("AM_EX_QTN", "매출금액(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매출처기간별이윤율견적.SetCol("AM_PO", "매입금액(원화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매출처기간별이윤율견적.SetCol("AM_EX_PO", "매입금액(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매출처기간별이윤율견적.SetCol("AM_PROFIT", "이윤", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매출처기간별이윤율견적.SetCol("RT_PROFIT", "이윤율", 100, false, typeof(decimal), FormatTpType.RATE);

                        this._flex매출처기간별이윤율견적.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

                        this._flex매출처기간별이윤율견적.SetExceptSumCol("RT_PROFIT");
                    }
                    #endregion

                    this.chart매출처기간별이윤율견적.DataSource = dt.Copy();

                    this.chart매출처기간별이윤율견적.ChartFx.Series[0].Text = "매출금액";
                    this.chart매출처기간별이윤율견적.ChartFx.Series[1].Visible = false;
                    this.chart매출처기간별이윤율견적.ChartFx.Series[2].Text = "매입금액";
                    this.chart매출처기간별이윤율견적.ChartFx.Series[3].Visible = false;
                    this.chart매출처기간별이윤율견적.ChartFx.Series[4].Text = "이윤";
                    this.chart매출처기간별이윤율견적.ChartFx.Series[5].Text = "이윤율";

                    this.chart매출처기간별이윤율견적.ChartFx.Series[4].Pane = this.chart매출처기간별이윤율견적.ChartFx.Panes[1];
                    this.chart매출처기간별이윤율견적.ChartFx.Series[4].Gallery = Gallery.Lines;
                    this.chart매출처기간별이윤율견적.ChartFx.Series[5].Pane = this.chart매출처기간별이윤율견적.ChartFx.Panes[2];
                    this.chart매출처기간별이윤율견적.ChartFx.Series[5].Gallery = Gallery.Lines;
                    this.chart매출처기간별이윤율견적.ChartFx.Series[5].PointLabels.Visible = true;
                }

                this._flex매출처기간별이윤율견적.Binding = dt;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn매출처품목군별이윤율견적_Click(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                dt = this._biz.Search(this.조회조건(), "P06").Tables[0];

                if (dt.Rows == null || dt.Rows.Count == 0)
                    this.chart매출처품목군별이윤율견적.ChartFx.Data.Clear();
                else
                {
                    #region 리스트
                    if (this._flex매출처품목군별이윤율견적.DataTable == null)
                    {
                        this._flex매출처품목군별이윤율견적.BeginSetting(1, 1, false);

                        this._flex매출처품목군별이윤율견적.SetCol("NM_ITEMGRP", "품목군", 80);
                        this._flex매출처품목군별이윤율견적.SetCol("AM_QTN", "매출금액(원화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매출처품목군별이윤율견적.SetCol("AM_EX_QTN", "매출금액(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매출처품목군별이윤율견적.SetCol("AM_PO", "매입금액(원화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매출처품목군별이윤율견적.SetCol("AM_EX_PO", "매입금액(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매출처품목군별이윤율견적.SetCol("AM_PROFIT", "이윤", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매출처품목군별이윤율견적.SetCol("RT_PROFIT", "이윤율", 100, false, typeof(decimal), FormatTpType.RATE);

                        this._flex매출처품목군별이윤율견적.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

                        this._flex매출처품목군별이윤율견적.SetExceptSumCol("RT_PROFIT");
                    }
                    #endregion

                    this.chart매출처품목군별이윤율견적.DataSource = dt.Copy();

                    this.chart매출처품목군별이윤율견적.ChartFx.Series[0].Text = "매출금액";
                    this.chart매출처품목군별이윤율견적.ChartFx.Series[1].Visible = false;
                    this.chart매출처품목군별이윤율견적.ChartFx.Series[2].Text = "매입금액";
                    this.chart매출처품목군별이윤율견적.ChartFx.Series[3].Visible = false;
                    this.chart매출처품목군별이윤율견적.ChartFx.Series[4].Text = "이윤";
                    this.chart매출처품목군별이윤율견적.ChartFx.Series[5].Text = "이윤율";

                    this.chart매출처품목군별이윤율견적.ChartFx.Series[4].Pane = this.chart매출처품목군별이윤율견적.ChartFx.Panes[1];
                    this.chart매출처품목군별이윤율견적.ChartFx.Series[4].Gallery = Gallery.Bar;
                    this.chart매출처품목군별이윤율견적.ChartFx.Series[5].Pane = this.chart매출처품목군별이윤율견적.ChartFx.Panes[2];
                    this.chart매출처품목군별이윤율견적.ChartFx.Series[5].Gallery = Gallery.Bar;
                    this.chart매출처품목군별이윤율견적.ChartFx.Series[5].PointLabels.Visible = true;
                }

                this._flex매출처품목군별이윤율견적.Binding = dt;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn매출처기간별이윤율수주_Click(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                dt = this._biz.Search(this.조회조건(), "P07").Tables[0];

                if (dt.Rows == null || dt.Rows.Count == 0)
                    this.chart매출처기간별이윤율수주.ChartFx.Data.Clear();
                else
                {
                    #region 리스트
                    if (this._flex매출처기간별이윤율수주.DataTable == null)
                    {
                        this._flex매출처기간별이윤율수주.BeginSetting(1, 1, false);

                        this._flex매출처기간별이윤율수주.SetCol("DT_SO", "수주일자", 80);
                        this._flex매출처기간별이윤율수주.SetCol("AM_SO", "수주금액(원화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매출처기간별이윤율수주.SetCol("AM_EX_SO", "수주금액(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매출처기간별이윤율수주.SetCol("AM_PO_ALL", "발주금액(재고포함)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매출처기간별이윤율수주.SetCol("AM_PO", "발주금액(원화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매출처기간별이윤율수주.SetCol("AM_EX_PO", "발주금액(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매출처기간별이윤율수주.SetCol("AM_STOCK", "재고금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매출처기간별이윤율수주.SetCol("AM_PROFIT", "이윤", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매출처기간별이윤율수주.SetCol("RT_PROFIT", "이윤율", 100, false, typeof(decimal), FormatTpType.RATE);

                        this._flex매출처기간별이윤율수주.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

                        this._flex매출처기간별이윤율수주.SetExceptSumCol("RT_PROFIT");
                    }
                    #endregion

                    this.chart매출처기간별이윤율수주.DataSource = dt.Copy();

                    this.chart매출처기간별이윤율수주.ChartFx.Series[0].Text = "수주금액";
                    this.chart매출처기간별이윤율수주.ChartFx.Series[1].Visible = false;
                    this.chart매출처기간별이윤율수주.ChartFx.Series[2].Text = "발주금액";
                    this.chart매출처기간별이윤율수주.ChartFx.Series[3].Visible = false;
                    this.chart매출처기간별이윤율수주.ChartFx.Series[4].Visible = false;
                    this.chart매출처기간별이윤율수주.ChartFx.Series[5].Visible = false;
                    this.chart매출처기간별이윤율수주.ChartFx.Series[6].Text = "이윤";
                    this.chart매출처기간별이윤율수주.ChartFx.Series[7].Text = "이윤율";

                    this.chart매출처기간별이윤율수주.ChartFx.Series[6].Pane = this.chart매출처기간별이윤율수주.ChartFx.Panes[1];
                    this.chart매출처기간별이윤율수주.ChartFx.Series[6].Gallery = Gallery.Lines;
                    this.chart매출처기간별이윤율수주.ChartFx.Series[7].Pane = this.chart매출처기간별이윤율수주.ChartFx.Panes[2];
                    this.chart매출처기간별이윤율수주.ChartFx.Series[7].Gallery = Gallery.Lines;
                    this.chart매출처기간별이윤율수주.ChartFx.Series[7].PointLabels.Visible = true;
                }

                this._flex매출처기간별이윤율수주.Binding = dt;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn매출처품목군별이윤율수주_Click(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                dt = this._biz.Search(this.조회조건(), "P08").Tables[0];

                if (dt.Rows == null || dt.Rows.Count == 0)
                    this.chart매출처품목군별이윤율수주.ChartFx.Data.Clear();
                else
                {
                    #region 리스트
                    if (this._flex매출처품목군별이윤율수주.DataTable == null)
                    {
                        this._flex매출처품목군별이윤율수주.BeginSetting(1, 1, false);

                        this._flex매출처품목군별이윤율수주.SetCol("NM_ITEMGRP", "품목군", 80);
                        this._flex매출처품목군별이윤율수주.SetCol("AM_SO", "수주금액(원화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매출처품목군별이윤율수주.SetCol("AM_EX_SO", "수주금액(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매출처품목군별이윤율수주.SetCol("AM_PO_ALL", "발주금액(재고포함)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매출처품목군별이윤율수주.SetCol("AM_PO", "발주금액(원화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매출처품목군별이윤율수주.SetCol("AM_EX_PO", "발주금액(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매출처품목군별이윤율수주.SetCol("AM_STOCK", "재고금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매출처품목군별이윤율수주.SetCol("AM_PROFIT", "이윤", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매출처품목군별이윤율수주.SetCol("RT_PROFIT", "이윤율", 100, false, typeof(decimal), FormatTpType.RATE);

                        this._flex매출처품목군별이윤율수주.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

                        this._flex매출처품목군별이윤율수주.SetExceptSumCol("RT_PROFIT");
                    }
                    #endregion

                    this.chart매출처품목군별이윤율수주.DataSource = dt.Copy();

                    this.chart매출처품목군별이윤율수주.ChartFx.Series[0].Text = "수주금액";
                    this.chart매출처품목군별이윤율수주.ChartFx.Series[1].Visible = false;
                    this.chart매출처품목군별이윤율수주.ChartFx.Series[2].Text = "발주금액";
                    this.chart매출처품목군별이윤율수주.ChartFx.Series[3].Visible = false;
                    this.chart매출처품목군별이윤율수주.ChartFx.Series[4].Visible = false;
                    this.chart매출처품목군별이윤율수주.ChartFx.Series[5].Visible = false;
                    this.chart매출처품목군별이윤율수주.ChartFx.Series[6].Text = "이윤";
                    this.chart매출처품목군별이윤율수주.ChartFx.Series[7].Text = "이윤율";

                    this.chart매출처품목군별이윤율수주.ChartFx.Series[6].Pane = this.chart매출처품목군별이윤율수주.ChartFx.Panes[1];
                    this.chart매출처품목군별이윤율수주.ChartFx.Series[6].Gallery = Gallery.Bar;
                    this.chart매출처품목군별이윤율수주.ChartFx.Series[7].Pane = this.chart매출처품목군별이윤율수주.ChartFx.Panes[2];
                    this.chart매출처품목군별이윤율수주.ChartFx.Series[7].Gallery = Gallery.Bar;
                    this.chart매출처품목군별이윤율수주.ChartFx.Series[7].PointLabels.Visible = true;
                }

                this._flex매출처품목군별이윤율수주.Binding = dt;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region 기타
        private void btn매입처정보_Click(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                dt = this._biz.Search(this.조회조건(), "P09").Tables[0];

                if (dt.Rows == null || dt.Rows.Count == 0)
                    this.chart매입처정보.ChartFx.Data.Clear();
                else
                {
                    #region 리스트
                    if (this._flex매입처정보.DataTable == null)
                    {
                        this._flex매입처정보.BeginSetting(1, 1, false);

                        this._flex매입처정보.SetCol("LN_PARTNER", "매입처", 100);
                        this._flex매입처정보.SetCol("QT_INQ", "문의건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                        this._flex매입처정보.SetCol("AM_INQ", "문의금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
						this._flex매입처정보.SetCol("QT_QTN", "견적건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
						this._flex매입처정보.SetCol("AM_QTN", "견적금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
						this._flex매입처정보.SetCol("QT_PO", "발주건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                        this._flex매입처정보.SetCol("AM_PO", "발주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매입처정보.SetCol("QT_STOCK", "재고건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                        this._flex매입처정보.SetCol("AM_STOCK", "재고금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
						this._flex매입처정보.SetCol("QT_SO", "수주건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
						this._flex매입처정보.SetCol("AM_SO", "수주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매입처정보.SetCol("AM_PROFIT", "이윤", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매입처정보.SetCol("RT_PO", "발주율", 100, false, typeof(decimal), FormatTpType.RATE);
                        this._flex매입처정보.SetCol("RT_PROFIT", "이윤율", 100, false, typeof(decimal), FormatTpType.RATE);
                        this._flex매입처정보.SetCol("LT_MAX", "최대납기", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                        this._flex매입처정보.SetCol("LT_MIN", "최소납기", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                        this._flex매입처정보.SetCol("LT_AVG", "평균납기", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                        this._flex매입처정보.SetCol("LT_STDEV", "표준편차", 100, false, typeof(decimal), FormatTpType.RATE);

                        this._flex매입처정보.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

                        this._flex매입처정보.SetExceptSumCol("RT_PO", "RT_PROFIT", "LT_MAX", "LT_MIN", "LT_AVG", "LT_STDEV");
                    }
                    #endregion

                    this.chart매입처정보.DataSource = dt.Copy();

                    this.chart매입처정보.ChartFx.Series[0].Text = "문의건수";
					this.chart매입처정보.ChartFx.Series[1].Visible = false;
					this.chart매입처정보.ChartFx.Series[2].Text = "발주건수";
                    this.chart매입처정보.ChartFx.Series[3].Text = "재고건수";
					this.chart매입처정보.ChartFx.Series[4].Visible = false;
					this.chart매입처정보.ChartFx.Series[5].Text = "문의금액";
					this.chart매입처정보.ChartFx.Series[6].Visible = false;
					this.chart매입처정보.ChartFx.Series[7].Text = "발주금액";
                    this.chart매입처정보.ChartFx.Series[8].Text = "재고금액";
                    this.chart매입처정보.ChartFx.Series[9].Visible = false;
                    this.chart매입처정보.ChartFx.Series[10].Visible = false;
                    this.chart매입처정보.ChartFx.Series[11].Text = "발주율";
                    this.chart매입처정보.ChartFx.Series[12].Visible = false;
                    this.chart매입처정보.ChartFx.Series[13].Visible = false;
                    this.chart매입처정보.ChartFx.Series[14].Visible = false;
                    this.chart매입처정보.ChartFx.Series[15].Visible = false;
                    this.chart매입처정보.ChartFx.Series[16].Visible = false;

                    this.chart매입처정보.ChartFx.Series[5].Pane = this.chart매입처정보.ChartFx.Panes[1];
                    this.chart매입처정보.ChartFx.Series[7].Pane = this.chart매입처정보.ChartFx.Panes[1];
                    this.chart매입처정보.ChartFx.Series[8].Pane = this.chart매입처정보.ChartFx.Panes[1];

                    this.chart매입처정보.ChartFx.Series[11].Pane = this.chart매입처정보.ChartFx.Panes[2];
                    this.chart매입처정보.ChartFx.Series[11].Gallery = Gallery.Lines;
                    this.chart매입처정보.ChartFx.Series[11].PointLabels.Visible = true;
                }

                this._flex매입처정보.Binding = dt;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn미수금_Click(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                dt = this._biz.Search(this.조회조건(), "P10").Tables[0];

                if (dt.Rows == null || dt.Rows.Count == 0)
                    this.chart미수금.ChartFx.Data.Clear();
                else
                {
                    #region 리스트
                    if (this._flex미수금.DataTable == null)
                    {
                        this._flex미수금.BeginSetting(1, 1, false);

                        this._flex미수금.SetCol("DT_IV", "기준일자", 80);
                        this._flex미수금.SetCol("AM_CLS", "매출금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex미수금.SetCol("AM_RCP", "수금금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex미수금.SetCol("AM_RCP_A", "선수금금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex미수금.SetCol("AM_REMAIN", "미수금", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex미수금.SetCol("RT_REMAIN", "미수금율", 80, false, typeof(decimal), FormatTpType.RATE);

                        this._flex미수금.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
                    }
                    #endregion

                    this.chart미수금.DataSource = dt.Copy();

                    this.chart미수금.ChartFx.Series[0].Text = "매출금액";
                    this.chart미수금.ChartFx.Series[1].Text = "수금금액";
                    this.chart미수금.ChartFx.Series[2].Text = "선수금금액";
                    this.chart미수금.ChartFx.Series[3].Text = "미수금";
                    this.chart미수금.ChartFx.Series[4].Text = "미수금율";

                    this.chart미수금.ChartFx.Series[3].Pane = this.chart미수금.ChartFx.Panes[1];
                    this.chart미수금.ChartFx.Series[4].Pane = this.chart미수금.ChartFx.Panes[2];
                    this.chart미수금.ChartFx.Series[4].PointLabels.Visible = true;
                }

                this._flex미수금.Binding = dt;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #endregion

        #region 매입
        private void btn매입처거래실적_Click(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                dt = this._biz.Search(this.조회조건(), "S01").Tables[0];

                if (dt.Rows == null || dt.Rows.Count == 0)
                    this.chart매입처거래실적.ChartFx.Data.Clear();
                else
                {
                    #region 리스트
                    if (this._flex매입처거래실적.DataTable == null)
                    {
                        this._flex매입처거래실적.BeginSetting(1, 1, false);

                        this._flex매입처거래실적.SetCol("DT_PO", "발주일자", 80);
                        this._flex매입처거래실적.SetCol("AM_AE", "발전기", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매입처거래실적.SetCol("AM_BD", "보세품", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매입처거래실적.SetCol("AM_EQ", "조선기자재", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매입처거래실적.SetCol("AM_ET", "기타", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매입처거래실적.SetCol("AM_GS", "선용품", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매입처거래실적.SetCol("AM_HE", "힘센엔진", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매입처거래실적.SetCol("AM_ME", "메인엔진", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매입처거래실적.SetCol("AM_PP", "펌프", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매입처거래실적.SetCol("AM_PV", "선식", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매입처거래실적.SetCol("AM_SG", "선박관리", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매입처거래실적.SetCol("AM_TC", "터보차저", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매입처거래실적.SetCol("AM_TS", "기술서비스", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

                        this._flex매입처거래실적.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
                    }
                    #endregion

                    this.chart매입처거래실적.DataSource = dt.Copy();

                    this.chart매입처거래실적.ChartFx.Series[0].Text = "발전기";
                    this.chart매입처거래실적.ChartFx.Series[1].Text = "보세품";
                    this.chart매입처거래실적.ChartFx.Series[2].Text = "조선기자재";
                    this.chart매입처거래실적.ChartFx.Series[3].Text = "기타";
                    this.chart매입처거래실적.ChartFx.Series[4].Text = "선용품";
                    this.chart매입처거래실적.ChartFx.Series[5].Text = "힘센엔진";
                    this.chart매입처거래실적.ChartFx.Series[6].Text = "메인엔진";
                    this.chart매입처거래실적.ChartFx.Series[7].Text = "펌프";
                    this.chart매입처거래실적.ChartFx.Series[8].Text = "선식";
                    this.chart매입처거래실적.ChartFx.Series[9].Text = "선박관리";
                    this.chart매입처거래실적.ChartFx.Series[10].Text = "터보차저";
                    this.chart매입처거래실적.ChartFx.Series[11].Text = "기술서비스";
                }

                this._flex매입처거래실적.Binding = dt;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn매입처납기_Click(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                dt = this._biz.Search(this.조회조건(), "S02").Tables[0];

                if (dt.Rows == null || dt.Rows.Count == 0)
                    this.chart매입처납기.ChartFx.Data.Clear();
                else
                {
                    #region 리스트
                    if (this._flex매입처납기.DataTable == null)
                    {
                        this._flex매입처납기.BeginSetting(1, 1, false);

                        this._flex매입처납기.SetCol("DT_PO", "발주일자", 80);
                        this._flex매입처납기.SetCol("LT_MAX", "최대납기", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                        this._flex매입처납기.SetCol("LT_MIN", "최소납기", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                        this._flex매입처납기.SetCol("LT_AVG", "평균납기", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                        this._flex매입처납기.SetCol("LT_STDEV", "표준편차", 100, false, typeof(decimal), FormatTpType.RATE);

                        this._flex매입처납기.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
                    }
                    #endregion

                    this.chart매입처납기.DataSource = dt.Copy();

                    this.chart매입처납기.ChartFx.Series[0].Text = "최대납기";
                    this.chart매입처납기.ChartFx.Series[1].Text = "최소납기";
                    this.chart매입처납기.ChartFx.Series[2].Text = "평균납기";
                    this.chart매입처납기.ChartFx.Series[3].Text = "표준편차";

                    this.chart매입처납기.ChartFx.Series[3].Pane = this.chart매입처납기.ChartFx.Panes[1];
                }

                this._flex매입처납기.Binding = dt;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn매출처정보_Click(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                dt = this._biz.Search(this.조회조건(), "S03").Tables[0];

                if (dt.Rows == null || dt.Rows.Count == 0)
                    this.chart매출처정보선택율.ChartFx.Data.Clear();
                else
                {
                    #region 리스트
                    if (this._flex매출처정보.DataTable == null)
                    {
                        this._flex매출처정보.BeginSetting(1, 1, false);

                        this._flex매출처정보.SetCol("DT_INQ", "문의일자", 80);
                        this._flex매출처정보.SetCol("QT_INQ", "문의종수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                        this._flex매출처정보.SetCol("QT_CHOICE", "견적(선택)종수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                        this._flex매출처정보.SetCol("RT_CHOICE", "선택율", 100, false, typeof(decimal), FormatTpType.RATE);
                        this._flex매출처정보.SetCol("QT_SO", "수주종수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                        this._flex매출처정보.SetCol("RT_SO", "수주율", 100, false, typeof(decimal), FormatTpType.RATE);
                        this._flex매출처정보.SetCol("AM_SO", "수주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매출처정보.SetCol("AM_PO", "발주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매출처정보.SetCol("AM_STOCK", "재고금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매출처정보.SetCol("AM_PROFIT", "이윤", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex매출처정보.SetCol("RT_PROFIT", "이윤율", 100, false, typeof(decimal), FormatTpType.RATE);

                        this._flex매출처정보.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

                        this._flex매출처정보.SetExceptSumCol("RT_CHOICE", "RT_SO", "RT_PROFIT");
                    }
                    #endregion

                    this.chart매출처정보선택율.DataSource = dt.Copy();
                    this.chart매출처정보수주율.DataSource = dt.Copy();
                    this.chart매출처정보이윤율.DataSource = dt.Copy();

                    this.chart매출처정보선택율.ChartFx.Series[0].Text = "문의종수";
                    this.chart매출처정보선택율.ChartFx.Series[1].Text = "견적(선택)종수";
                    this.chart매출처정보선택율.ChartFx.Series[2].Text = "선택율";
                    this.chart매출처정보선택율.ChartFx.Series[3].Visible = false;
                    this.chart매출처정보선택율.ChartFx.Series[4].Visible = false;
                    this.chart매출처정보선택율.ChartFx.Series[5].Visible = false;
                    this.chart매출처정보선택율.ChartFx.Series[6].Visible = false;
                    this.chart매출처정보선택율.ChartFx.Series[7].Visible = false;
                    this.chart매출처정보선택율.ChartFx.Series[8].Visible = false;
                    this.chart매출처정보선택율.ChartFx.Series[9].Visible = false;

                    this.chart매출처정보수주율.ChartFx.Series[0].Visible = false;
                    this.chart매출처정보수주율.ChartFx.Series[1].Text = "견적종수";
                    this.chart매출처정보수주율.ChartFx.Series[2].Visible = false;
                    this.chart매출처정보수주율.ChartFx.Series[3].Text = "수주종수";
                    this.chart매출처정보수주율.ChartFx.Series[4].Text = "수주율";
                    this.chart매출처정보수주율.ChartFx.Series[5].Visible = false;
                    this.chart매출처정보수주율.ChartFx.Series[6].Visible = false;
                    this.chart매출처정보수주율.ChartFx.Series[7].Visible = false;
                    this.chart매출처정보수주율.ChartFx.Series[8].Visible = false;
                    this.chart매출처정보수주율.ChartFx.Series[9].Visible = false;

                    this.chart매출처정보이윤율.ChartFx.Series[0].Visible = false;
                    this.chart매출처정보이윤율.ChartFx.Series[1].Visible = false;
                    this.chart매출처정보이윤율.ChartFx.Series[2].Visible = false;
                    this.chart매출처정보이윤율.ChartFx.Series[3].Visible = false;
                    this.chart매출처정보이윤율.ChartFx.Series[4].Visible = false;
                    this.chart매출처정보이윤율.ChartFx.Series[5].Text = "수주금액";
                    this.chart매출처정보이윤율.ChartFx.Series[6].Text = "발주금액";
                    this.chart매출처정보이윤율.ChartFx.Series[7].Text = "재고금액";
                    this.chart매출처정보이윤율.ChartFx.Series[8].Text = "이윤";
                    this.chart매출처정보이윤율.ChartFx.Series[9].Text = "이윤율";

                    this.chart매출처정보선택율.ChartFx.Series[2].Pane = this.chart매출처정보선택율.ChartFx.Panes[1];
                    this.chart매출처정보선택율.ChartFx.Series[2].PointLabels.Visible = true;

                    this.chart매출처정보수주율.ChartFx.Series[4].Pane = this.chart매출처정보수주율.ChartFx.Panes[1];
                    this.chart매출처정보수주율.ChartFx.Series[4].PointLabels.Visible = true;

                    this.chart매출처정보이윤율.ChartFx.Series[8].Pane = this.chart매출처정보이윤율.ChartFx.Panes[1];
                    this.chart매출처정보이윤율.ChartFx.Series[9].Pane = this.chart매출처정보이윤율.ChartFx.Panes[2];
                    this.chart매출처정보이윤율.ChartFx.Series[9].PointLabels.Visible = true;
                }

                this._flex매출처정보.Binding = dt;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn미지급금_Click(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                dt = this._biz.Search(this.조회조건(), "S04").Tables[0];

                if (dt.Rows == null || dt.Rows.Count == 0)
                    this.chart미지급금.ChartFx.Data.Clear();
                else
                {
                    #region 리스트
                    if (this._flex미지급금.DataTable == null)
                    {
                        this._flex미지급금.BeginSetting(1, 1, false);

                        this._flex미지급금.SetCol("DT_IV", "기준일자", 80);
                        this._flex미지급금.SetCol("AM_CLS", "매입금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex미지급금.SetCol("AM_BAN", "지급금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex미지급금.SetCol("AM_BAN_ADPAY", "선지급금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex미지급금.SetCol("AM_REMAIN", "미지급금", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex미지급금.SetCol("RT_REMAIN", "미지급율", 80, false, typeof(decimal), FormatTpType.RATE);

                        this._flex미지급금.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
                    }
                    #endregion

                    this.chart미지급금.DataSource = dt.Copy();

                    this.chart미지급금.ChartFx.Series[0].Text = "매입금액";
                    this.chart미지급금.ChartFx.Series[1].Text = "지급금액";
                    this.chart미지급금.ChartFx.Series[2].Text = "선지급금액";
                    this.chart미지급금.ChartFx.Series[3].Text = "미지급금";
                    this.chart미지급금.ChartFx.Series[4].Text = "미지급율";

                    this.chart미지급금.ChartFx.Series[3].Pane = this.chart미지급금.ChartFx.Panes[1];
                    this.chart미지급금.ChartFx.Series[4].Pane = this.chart미지급금.ChartFx.Panes[2];
                    this.chart미지급금.ChartFx.Series[4].PointLabels.Visible = true;
                }

                this._flex미지급금.Binding = dt;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region 호선
        private void btn매출처별호선_Click(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                dt = this._biz.Search(this.조회조건(), "H01").Tables[0];

                if (dt == null || dt.Rows.Count == 0)
                    this.chart매출처별호선.ChartFx.Data.Clear();
                else
                {
                    #region 리스트
                    if (this._flex매출처별호선.DataTable == null)
                    {
                        this._flex매출처별호선.BeginSetting(1, 1, false);

                        this._flex매출처별호선.SetCol("LN_PARTNER", "거래처명", 120);
                        this._flex매출처별호선.SetCol("QT_HULL", "호선수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                        
                        this._flex매출처별호선.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
                    }
                    #endregion

                    this.chart매출처별호선.DataSource = dt.Copy();

                    this.chart매출처별호선.ChartFx.Series[0].Text = "호선수";
                }

                this._flex매출처별호선.Binding = dt;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn호선별이윤율_Click(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                dt = this._biz.Search(this.조회조건(), "H02").Tables[0];

                if (dt.Rows == null || dt.Rows.Count == 0)
                    this.chart호선별이윤율.ChartFx.Data.Clear();
                else
                {
                    #region 리스트
                    if (this._flex호선별이윤율.DataTable == null)
                    {
                        this._flex호선별이윤율.BeginSetting(1, 1, false);

                        this._flex호선별이윤율.SetCol("NO_IMO", "IMO번호", 80);
                        this._flex호선별이윤율.SetCol("NM_VESSEL", "호선명", 120);
                        this._flex호선별이윤율.SetCol("NO_HULL", "HULL번호", 80);
                        this._flex호선별이윤율.SetCol("DT_SHIP_DLV", "선박납기일자", 60, false, typeof(string), FormatTpType.YEAR_MONTH);
                        this._flex호선별이윤율.SetCol("LN_PARTNER", "거래처명", 120);
                        this._flex호선별이윤율.SetCol("AM_SO", "수주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex호선별이윤율.SetCol("AM_PO", "발주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex호선별이윤율.SetCol("AM_STOCK", "재고금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex호선별이윤율.SetCol("AM_PROFIT", "이윤", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex호선별이윤율.SetCol("RT_PROFIT", "이윤율", 100, false, typeof(decimal), FormatTpType.RATE);

                        this._flex호선별이윤율.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

                        this._flex호선별이윤율.SetExceptSumCol("RT_PROFIT");
                    }
                    #endregion

                    this.chart호선별이윤율.DataSource = dt.Copy();

                    this.chart호선별이윤율.ChartFx.Series[0].Text = "수주금액";
                    this.chart호선별이윤율.ChartFx.Series[1].Text = "발주금액";
                    this.chart호선별이윤율.ChartFx.Series[2].Text = "재고금액";
                    this.chart호선별이윤율.ChartFx.Series[3].Text = "이윤";
                    this.chart호선별이윤율.ChartFx.Series[4].Text = "이윤율";

                    this.chart호선별이윤율.ChartFx.Series[3].Pane = this.chart호선별이윤율.ChartFx.Panes[1];
                    this.chart호선별이윤율.ChartFx.Series[4].Pane = this.chart호선별이윤율.ChartFx.Panes[2];
                }

                this._flex호선별이윤율.Binding = dt;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn호선별수주율_Click(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                dt = this._biz.Search(this.조회조건(), "H03").Tables[0];

                if (dt.Rows == null || dt.Rows.Count == 0)
                    this.chart호선별수주율.ChartFx.Data.Clear();
                else
                {
                    #region 리스트
                    if (this._flex호선별수주율.DataTable == null)
                    {
                        this._flex호선별수주율.BeginSetting(1, 1, false);

                        this._flex호선별수주율.SetCol("NO_IMO", "IMO번호", 80);
                        this._flex호선별수주율.SetCol("NM_VESSEL", "호선명", 120);
                        this._flex호선별수주율.SetCol("NO_HULL", "HULL번호", 80);
                        this._flex호선별수주율.SetCol("LN_PARTNER", "거래처명", 120);
                        this._flex호선별수주율.SetCol("QT_QTN", "견적건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                        this._flex호선별수주율.SetCol("QT_SO", "수주건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                        this._flex호선별수주율.SetCol("AM_QTN", "견적금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex호선별수주율.SetCol("AM_SO", "수주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex호선별수주율.SetCol("RT_QT_SO", "수주율(건수)", 100, false, typeof(decimal), FormatTpType.RATE);
                        this._flex호선별수주율.SetCol("RT_AM_SO", "수주율(금액)", 100, false, typeof(decimal), FormatTpType.RATE);

                        this._flex호선별수주율.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

                        this._flex호선별수주율.SetExceptSumCol("RT_QT_SO", "RT_AM_SO");
                    }
                    #endregion

                    this.chart호선별수주율.DataSource = dt.Copy();

                    this.chart호선별수주율.ChartFx.Series[0].Text = "견적건수";
                    this.chart호선별수주율.ChartFx.Series[1].Text = "수주건수";
                    this.chart호선별수주율.ChartFx.Series[2].Text = "견적금액";
                    this.chart호선별수주율.ChartFx.Series[3].Text = "수주금액";
                    this.chart호선별수주율.ChartFx.Series[4].Text = "수주율(건수)";
                    this.chart호선별수주율.ChartFx.Series[5].Text = "수주율(금액)";

                    this.chart호선별수주율.ChartFx.Series[2].Pane = this.chart호선별수주율.ChartFx.Panes[1];
                    this.chart호선별수주율.ChartFx.Series[3].Pane = this.chart호선별수주율.ChartFx.Panes[1];
                    this.chart호선별수주율.ChartFx.Series[4].Pane = this.chart호선별수주율.ChartFx.Panes[2];
                    this.chart호선별수주율.ChartFx.Series[5].Pane = this.chart호선별수주율.ChartFx.Panes[2];
                }

                this._flex호선별수주율.Binding = dt;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn거래내역_Click(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                dt = this._biz.Search(this.조회조건(), "H04").Tables[0];

                if (dt.Rows != null && dt.Rows.Count != 0)
                {
                    #region 리스트
                    if (this._flex거래내역.DataTable == null)
                    {
                        this._flex거래내역.BeginSetting(1, 1, false);

                        this._flex거래내역.SetCol("NO_FILE", "파일번호", 80);
                        this._flex거래내역.SetCol("DT_QTN", "견적일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                        this._flex거래내역.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                        this._flex거래내역.SetCol("LN_PARTNER", "거래처명", 120);
                        this._flex거래내역.SetCol("NO_IMO", "IMO번호", 80);
                        this._flex거래내역.SetCol("NM_VESSEL", "호선명", 120);
                        this._flex거래내역.SetCol("NO_HULL", "HULL번호", 80);
                        this._flex거래내역.SetCol("NM_EMP", "담당자", 80);
                        this._flex거래내역.SetCol("QT_ITEM_QTN", "견적종수", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex거래내역.SetCol("QT_ITEM_SO", "수주종수", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex거래내역.SetCol("AM_QTN", "견적금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex거래내역.SetCol("AM_SO", "수주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex거래내역.SetCol("AM_PO", "발주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex거래내역.SetCol("AM_STOCK", "재고금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex거래내역.SetCol("AM_PROFIT", "이윤", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex거래내역.SetCol("RT_PROFIT", "이윤율", 100, false, typeof(decimal), FormatTpType.RATE);

                        this._flex거래내역.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

                        this._flex거래내역.SetExceptSumCol("RT_PROFIT");
                    }
                    #endregion

                    this._flex거래내역.Binding = dt;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region 클레임
        private void btn클레임발생율_Click(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                dt = this._biz.Search(this.조회조건(), "C01").Tables[0];

                if (dt.Rows == null || dt.Rows.Count == 0)
                    this.chart클레임발생율.ChartFx.Data.Clear();
                else
                {
                    #region 리스트
                    if (this._flex클레임발생율.DataTable == null)
                    {
                        this._flex클레임발생율.BeginSetting(1, 1, false);

                        this._flex클레임발생율.SetCol("DT_SO", "수주일자", 80);
                        this._flex클레임발생율.SetCol("QT_SO", "수주건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                        this._flex클레임발생율.SetCol("QT_CLAIM", "클레임건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                        this._flex클레임발생율.SetCol("AM_SO", "수주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex클레임발생율.SetCol("AM_CLAIM", "클레임금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex클레임발생율.SetCol("RT_CLAIM_QT", "클레임율(건수)", 100, false, typeof(decimal), FormatTpType.RATE);
                        this._flex클레임발생율.SetCol("RT_CLAIM_AM", "클레임율(금액)", 100, false, typeof(decimal), FormatTpType.RATE);

                        this._flex클레임발생율.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

                        this._flex클레임발생율.SetExceptSumCol("RT_CLAIM_QT", "RT_CLAIM_AM");
                    }
                    #endregion

                    this.chart클레임발생율.DataSource = dt.Copy();

                    this.chart클레임발생율.ChartFx.Series[0].Visible = false;
                    this.chart클레임발생율.ChartFx.Series[1].Text = "클레임건수";
                    this.chart클레임발생율.ChartFx.Series[2].Visible = false;
                    this.chart클레임발생율.ChartFx.Series[3].Text = "클레임금액";
                    this.chart클레임발생율.ChartFx.Series[4].Text = "클레임율(건수)";
                    this.chart클레임발생율.ChartFx.Series[5].Text = "클레임율(금액)";

                    this.chart클레임발생율.ChartFx.Series[3].Pane = this.chart클레임발생율.ChartFx.Panes[1];
                    this.chart클레임발생율.ChartFx.Series[4].Pane = this.chart클레임발생율.ChartFx.Panes[2];
                    this.chart클레임발생율.ChartFx.Series[4].Gallery = Gallery.Lines;
                    this.chart클레임발생율.ChartFx.Series[5].Pane = this.chart클레임발생율.ChartFx.Panes[2];
                    this.chart클레임발생율.ChartFx.Series[5].Gallery = Gallery.Lines;
                }

                this._flex클레임발생율.Binding = dt;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn클레임유형_Click(object sender, EventArgs e)
        {
            DataSet ds;

            try
            {
                ds = this._biz.Search(this.조회조건(), "C02");

                if (ds.Tables == null || ds.Tables.Count == 0)
                {
                    this.chart클레임사유.ChartFx.Data.Clear();
                    this.chart원인구분.ChartFx.Data.Clear();
                    this.chart항목분류.ChartFx.Data.Clear();
                }
                else
                {
                    #region 리스트
                    if (this._flex클레임유형.DataTable == null)
                    {
                        this._flex클레임유형.BeginSetting(1, 1, false);

                        this._flex클레임유형.SetCol("NM_STATUS", "클레임상태", 80);
                        this._flex클레임유형.SetCol("NM_GW_STATUS", "결재상태", 80);
                        this._flex클레임유형.SetCol("NO_CLAIM", "클레임번호", 80);
                        this._flex클레임유형.SetCol("NO_SO", "수주번호", 80);
                        this._flex클레임유형.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                        this._flex클레임유형.SetCol("DT_INPUT", "발생일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                        this._flex클레임유형.SetCol("DT_CLOSING", "종결일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                        this._flex클레임유형.SetCol("NM_TP_CLAIM", "클레임사유", 100);
                        this._flex클레임유형.SetCol("NM_TP_CAUSE", "원인구분", 100);
                        this._flex클레임유형.SetCol("NM_TP_ITEM", "항목분류", 100);
                        this._flex클레임유형.SetCol("LN_PARTNER", "매출처", 100);
                        this._flex클레임유형.SetCol("NM_VESSEL", "호선", 100);
                        this._flex클레임유형.SetCol("NM_EMP", "담당자", 80);
                        this._flex클레임유형.SetCol("QT_CLAIM", "클레임건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                        this._flex클레임유형.SetCol("AM_CLAIM", "클레임금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex클레임유형.SetCol("DT_SO_DIFF", "발생일수", 80);
                        this._flex클레임유형.SetCol("DT_CLOSING_DIFF", "종결일수", 80);
                        this._flex클레임유형.SetCol("DC_CLAIM_CONTENS", "클레임내용", 100);

                        this._flex클레임유형.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
                    }
                    #endregion

                    this.chart클레임사유.DataSource = ds.Tables[1];
                    this.chart클레임사유.ChartFx.Series[0].Text = "클레임건수";
                    this.chart클레임사유.ChartFx.Series[0].PointLabels.Visible = true;

                    this.chart원인구분.DataSource = ds.Tables[2];
                    this.chart원인구분.ChartFx.Series[0].Text = "클레임건수";
                    this.chart원인구분.ChartFx.Series[0].PointLabels.Visible = true;

                    this.chart항목분류.DataSource = ds.Tables[3];
                    this.chart항목분류.ChartFx.Series[0].Text = "클레임건수";
                    this.chart항목분류.ChartFx.Series[0].PointLabels.Visible = true;
                }

                this._flex클레임유형.Binding = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region 사원
        private void btn사원별이윤율_Click(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                dt = this._biz.Search(this.조회조건(), "E01").Tables[0];

                if (dt.Rows == null || dt.Rows.Count == 0)
                    this.chart사원별이윤율.ChartFx.Data.Clear();
                else
                {
                    #region 리스트
                    if (this._flex사원별이윤율.DataTable == null)
                    {
                        this._flex사원별이윤율.BeginSetting(1, 1, false);

                        this._flex사원별이윤율.SetCol("NM_EMP", "담당자", 100);
                        this._flex사원별이윤율.SetCol("AM_SO", "수주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex사원별이윤율.SetCol("AM_PO", "발주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex사원별이윤율.SetCol("AM_STOCK", "재고금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex사원별이윤율.SetCol("AM_PROFIT", "이윤", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex사원별이윤율.SetCol("RT_PROFIT", "이윤율", 100, false, typeof(decimal), FormatTpType.RATE);

                        this._flex사원별이윤율.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
                    }
                    #endregion

                    this.chart사원별이윤율.DataSource = dt.Copy();

                    this.chart사원별이윤율.ChartFx.Series[0].Text = "수주금액";
                    this.chart사원별이윤율.ChartFx.Series[1].Text = "발주금액";
                    this.chart사원별이윤율.ChartFx.Series[2].Text = "재고금액";
                    this.chart사원별이윤율.ChartFx.Series[3].Text = "이윤";
                    this.chart사원별이윤율.ChartFx.Series[4].Text = "이윤율";

                    this.chart사원별이윤율.ChartFx.Series[4].PointLabels.Visible = true;
                    this.chart사원별이윤율.ChartFx.Series[3].Pane = this.chart사원별이윤율.ChartFx.Panes[1];
                    this.chart사원별이윤율.ChartFx.Series[4].Pane = this.chart사원별이윤율.ChartFx.Panes[2];
                }

                this._flex사원별이윤율.Binding = dt;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn사원별수주율_Click(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                dt = this._biz.Search(this.조회조건(), "E02").Tables[0];

                if (dt.Rows == null || dt.Rows.Count == 0)
                    this.chart사원별수주율.ChartFx.Data.Clear();
                else
                {
                    #region 리스트
                    if (this._flex사원별수주율.DataTable == null)
                    {
                        this._flex사원별수주율.BeginSetting(1, 1, false);

                        this._flex사원별수주율.SetCol("NM_EMP", "담당자", 100);
                        this._flex사원별수주율.SetCol("QT_QTN", "견적건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                        this._flex사원별수주율.SetCol("QT_SO", "수주건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                        this._flex사원별수주율.SetCol("AM_QTN", "견적금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex사원별수주율.SetCol("AM_SO", "수주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                        this._flex사원별수주율.SetCol("RT_QT_SO", "수주율(건수)", 100, false, typeof(decimal), FormatTpType.RATE);
                        this._flex사원별수주율.SetCol("RT_AM_SO", "수주율(금액)", 100, false, typeof(decimal), FormatTpType.RATE);

                        this._flex사원별수주율.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
                    }
                    #endregion

                    this.chart사원별수주율.DataSource = dt.Copy();

                    this.chart사원별수주율.ChartFx.Series[0].Text = "견적건수";
                    this.chart사원별수주율.ChartFx.Series[1].Text = "수주건수";
                    this.chart사원별수주율.ChartFx.Series[2].Text = "견적금액";
                    this.chart사원별수주율.ChartFx.Series[3].Text = "수주금액";
                    this.chart사원별수주율.ChartFx.Series[4].Text = "수주율(건수)";
                    this.chart사원별수주율.ChartFx.Series[5].Text = "수주율(금액)";

                    this.chart사원별수주율.ChartFx.Series[0].Pane = this.chart사원별수주율.ChartFx.Panes[1];
                    this.chart사원별수주율.ChartFx.Series[1].Pane = this.chart사원별수주율.ChartFx.Panes[1];
                    this.chart사원별수주율.ChartFx.Series[4].Pane = this.chart사원별수주율.ChartFx.Panes[2];
                    this.chart사원별수주율.ChartFx.Series[5].Pane = this.chart사원별수주율.ChartFx.Panes[2];
                }

                this._flex사원별수주율.Binding = dt;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region 키워드
        private void btn주제_Click(object sender, EventArgs e)
        {
            DataTable dt, dt1;
            DataRow dr;

            try
            {
                this.btn주제.Enabled = false;

                MsgControl.ShowMsg("자료를 조회 중 입니다.잠시만 기다려 주세요.");

                dt = this._biz.Search1(this.조회조건(), "K01").Tables[0];

                if (this._flex주제키워드.DataTable == null)
                {
                    this._flex주제키워드.BeginSetting(1, 1, false);

                    this._flex주제키워드.SetCol("KEYWORD", "키워드", 150);
                    this._flex주제키워드.SetCol("COUNT", "빈도수", 100, false, typeof(decimal), FormatTpType.QUANTITY);

                    this._flex주제키워드.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
                }

                if (this._flex주제리스트.DataTable == null)
                {
                    this._flex주제리스트.BeginSetting(1, 1, false);

                    this._flex주제리스트.SetCol("NO_FILE", "파일번호", 80);
                    this._flex주제리스트.SetCol("DT_INQ", "견적일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    this._flex주제리스트.SetCol("LN_PARTNER", "매출처", 100);
                    this._flex주제리스트.SetCol("NM_VESSEL", "호선명", 100);
                    this._flex주제리스트.SetCol("NM_SUBJECT", "주제", 150);
                    this._flex주제리스트.SetCol("QT_ITEM", "종수", 100, false, typeof(decimal), FormatTpType.QUANTITY);

                    this._flex주제리스트.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
                }

                var temp = dt.AsEnumerable().SelectMany(x => x["NM_SUBJECT"].ToString().Split(new string[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries))
                                            .GroupBy(x => x.Replace(" ", "")
                                                           .Replace("\t", ""), y => y, (x, y) => new { key = x, name = y.Max(), count = y.Count() })
                                            .OrderByDescending(x => x.count)
                                            .Take(100);

                dt1 = new DataTable();
                dt1.Columns.Add("KEY");
                dt1.Columns.Add("KEYWORD");
                dt1.Columns.Add("COUNT");

                foreach (var temp1 in temp)
                {
                    dr = dt1.NewRow();
                    dr["KEY"] = temp1.key;
                    dr["KEYWORD"] = temp1.name;
                    dr["COUNT"] = temp1.count;
                    dt1.Rows.Add(dr);
                }

                this._flex주제리스트.Binding = dt;
                this._flex주제키워드.Binding = dt1;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
                this.btn주제.Enabled = true;
            }
        }

        private void btn품목명_Click(object sender, EventArgs e)
        {
            DataTable dt, dt1;
            DataRow dr;

            try
            {
                this.btn품목명.Enabled = false;

                MsgControl.ShowMsg("자료를 조회 중 입니다.잠시만 기다려 주세요.");

                dt = this._biz.Search1(this.조회조건(), "K02").Tables[0];

                if (this._flex품목명키워드.DataTable == null)
                {
                    this._flex품목명키워드.BeginSetting(1, 1, false);

                    this._flex품목명키워드.SetCol("KEYWORD", "키워드", 150);
                    this._flex품목명키워드.SetCol("COUNT", "빈도수", 100, false, typeof(decimal), FormatTpType.QUANTITY);

                    this._flex품목명키워드.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
                }

                if (this._flex품목명리스트.DataTable == null)
                {
                    this._flex품목명리스트.BeginSetting(1, 1, false);

                    this._flex품목명리스트.SetCol("NO_FILE", "파일번호", 80);
                    this._flex품목명리스트.SetCol("DT_INQ", "견적일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    this._flex품목명리스트.SetCol("LN_PARTNER", "매출처", 100);
                    this._flex품목명리스트.SetCol("NM_VESSEL", "호선명", 100);
                    this._flex품목명리스트.SetCol("NM_SUPPLIER", "매입처", 100);
                    this._flex품목명리스트.SetCol("NM_ITEM_PARTNER", "매출처품명", 150);
                    this._flex품목명리스트.SetCol("CD_ITEM", "품목코드", 80);
                    this._flex품목명리스트.SetCol("NM_ITEM", "품목명", 100);
                    this._flex품목명리스트.SetCol("UM_QTN", "견적단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                    this._flex품목명리스트.SetCol("UM_SO", "수주단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                    this._flex품목명리스트.SetCol("UM_STOCK", "재고단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                    this._flex품목명리스트.SetCol("UM_PO", "발주단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);

                    this._flex품목명리스트.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
                }

                var temp = dt.AsEnumerable().SelectMany(x => x["NM_ITEM_PARTNER"].ToString().Split(new string[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries))
                                            .GroupBy(x => x.Replace(" ", "")
                                                           .Replace("\t", ""), y => y, (x, y) => new { key = x, name = y.Max(), count = y.Count() })
                                            .OrderByDescending(x => x.count)
                                            .Take(100);

                dt1 = new DataTable();
                dt1.Columns.Add("KEY");
                dt1.Columns.Add("KEYWORD");
                dt1.Columns.Add("COUNT");

                foreach (var temp1 in temp)
                {
                    dr = dt1.NewRow();
                    dr["KEY"] = temp1.key;
                    dr["KEYWORD"] = temp1.name;
                    dr["COUNT"] = temp1.count;
                    dt1.Rows.Add(dr);
                }

                this._flex품목명리스트.Binding = dt;
                this._flex품목명키워드.Binding = dt1;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
                this.btn품목명.Enabled = true;
            }
        }

        private void btn재고_Click(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                this.btn재고.Enabled = false;

                MsgControl.ShowMsg("자료를 조회 중 입니다.잠시만 기다려 주세요.");

                dt = this._biz.Search1(this.조회조건(), "K03").Tables[0];

                if (this._flex재고.DataTable == null)
                {
                    this._flex재고.BeginSetting(1, 1, false);

                    this._flex재고.SetCol("CD_ITEM", "품목코드", 150);
                    this._flex재고.SetCol("NM_ITEM", "품목명", 150);
                    this._flex재고.SetCol("STAND_PRC", "표준단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                    this._flex재고.SetCol("QT_QTN", "견적수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                    this._flex재고.SetCol("QT_SO", "수주수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                    this._flex재고.SetCol("QT_STOCK", "재고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                    this._flex재고.SetCol("QT_BOOK", "예약수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);

                    this._flex재고.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
                }

                this._flex재고.Binding = dt;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
                this.btn재고.Enabled = true;
            }
        }

        private void btn발주_Click(object sender, EventArgs e)
        {
            DataTable dt, dt1;
            DataRow dr;

            try
            {
                this.btn발주.Enabled = false;

                MsgControl.ShowMsg("자료를 조회 중 입니다.잠시만 기다려 주세요.");

                dt = this._biz.Search1(this.조회조건(), "K04").Tables[0];

                if (this._flex발주키워드.DataTable == null)
                {
                    this._flex발주키워드.BeginSetting(1, 1, false);

                    this._flex발주키워드.SetCol("KEYWORD", "키워드", 150);
                    this._flex발주키워드.SetCol("COUNT", "빈도수", 100, false, typeof(decimal), FormatTpType.QUANTITY);

                    this._flex발주키워드.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
                }

                if (this._flex발주리스트.DataTable == null)
                {
                    this._flex발주리스트.BeginSetting(1, 1, false);

                    this._flex발주리스트.SetCol("NO_PO", "발주번호", 80);
                    this._flex발주리스트.SetCol("DT_PO", "발주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    this._flex발주리스트.SetCol("LN_PARTNER", "매출처", 100);
                    this._flex발주리스트.SetCol("NM_VESSEL", "호선명", 100);
                    this._flex발주리스트.SetCol("NM_SUPPLIER", "매입처", 100);
                    this._flex발주리스트.SetCol("NM_ITEM_PARTNER", "매출처품명", 150);
                    this._flex발주리스트.SetCol("UM_QTN", "견적단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                    this._flex발주리스트.SetCol("UM_SO", "수주단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                    this._flex발주리스트.SetCol("UM_PO", "발주단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);

                    this._flex발주리스트.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
                }

                var temp = dt.AsEnumerable().SelectMany(x => x["NM_ITEM_PARTNER"].ToString().Split(new string[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries))
                                            .GroupBy(x => x.Replace(" ", "")
                                                           .Replace("\t", ""), y => y, (x, y) => new { key = x, name = y.Max(), count = y.Count() })
                                            .OrderByDescending(x => x.count)
                                            .Take(100);

                dt1 = new DataTable();
                dt1.Columns.Add("KEY");
                dt1.Columns.Add("KEYWORD");
                dt1.Columns.Add("COUNT");

                foreach (var temp1 in temp)
                {
                    dr = dt1.NewRow();
                    dr["KEY"] = temp1.key;
                    dr["KEYWORD"] = temp1.name;
                    dr["COUNT"] = temp1.count;
                    dt1.Rows.Add(dr);
                }

                this._flex발주리스트.Binding = dt;
                this._flex발주키워드.Binding = dt1;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
                this.btn발주.Enabled = true;
            }
        }

        private void _flex주제키워드_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt;
            DataRow dr;
            string filter;
            
            try
            {
                filter = "NM_KEY LIKE '%" + D.GetString(this._flex주제키워드["KEY"]).Replace("'", "''")
                                                                                    .Replace("[", "[[") + "%'";
                this._flex주제리스트.RowFilter = filter;

                var temp = this._flex주제리스트.DataTable.Select(filter)
                                                         .AsEnumerable()
                                                         .GroupBy(x => x["DT_INQ"].ToString().Substring(0, 6), y => y, (x, y) => new { key = x, count = y.Count() })
                                                         .OrderBy(x => x.key);

                dt = new DataTable();
                dt.Columns.Add("DT_INQ", typeof(string));
                dt.Columns.Add("COUNT", typeof(int));

                foreach (var temp1 in temp)
                {
                    dr = dt.NewRow();
                    dr["DT_INQ"] = temp1.key;
                    dr["COUNT"] = temp1.count;
                    dt.Rows.Add(dr);
                }

                this.chart주제.DataSource = dt;
                this.chart주제.ChartFx.Series[0].Text = "빈도수";
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex품목명키워드_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt;
            DataRow dr;
            string filter;
            
            try
            {
                filter = "NM_KEY LIKE '%" + D.GetString(this._flex품목명키워드["KEY"]).Replace("'", "''")
                                                                                      .Replace("[", "[[") + "%'";
                this._flex품목명리스트.RowFilter = filter;

                var temp = this._flex품목명리스트.DataTable.Select(filter)
                                                           .AsEnumerable()
                                                           .GroupBy(x => x["DT_INQ"].ToString().Substring(0, 6), y => y, (x, y) => new { key = x, count = y.Count() })
                                                           .OrderBy(x => x.key);

                dt = new DataTable();
                dt.Columns.Add("DT_INQ", typeof(string));
                dt.Columns.Add("COUNT", typeof(int));

                foreach (var temp1 in temp)
                {
                    dr = dt.NewRow();
                    dr["DT_INQ"] = temp1.key;
                    dr["COUNT"] = temp1.count;
                    dt.Rows.Add(dr);
                }

                this.chart품목명.DataSource = dt;
                this.chart품목명.ChartFx.Series[0].Text = "빈도수";
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex재고_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt;
            string query;

            try
            {
                query = @"SELECT LEFT(QH.DT_INQ, 6) AS DT_INQ,
                                 ISNULL(SUM(QL.QT_QTN), 0) AS QT_QTN,
                                 ISNULL(SUM(SB.QT_BOOK), 0) AS QT_BOOK
                          FROM CZ_SA_QTNH QH WITH(NOLOCK)
                          LEFT JOIN MA_SALEGRP SG WITH(NOLOCK) ON SG.CD_COMPANY = QH.CD_COMPANY AND SG.CD_SALEGRP = QH.CD_SALEGRP
                          LEFT JOIN CZ_SA_QTNL QL WITH(NOLOCK) ON QL.CD_COMPANY = QH.CD_COMPANY AND QL.NO_FILE = QH.NO_FILE
                          LEFT JOIN CZ_SA_STOCK_BOOK SB WITH(NOLOCK) ON SB.CD_COMPANY = QL.CD_COMPANY AND SB.NO_FILE = QL.NO_FILE AND SB.NO_LINE = QL.NO_LINE
                          WHERE QH.CD_COMPANY = '{0}'
                          AND QL.CD_ITEM = '{3}'
                          AND ('{4}' = '' OR QH.CD_PARTNER IN (SELECT CD_STR FROM GETTABLEFROMSPLIT('{4}')))
                          AND ('{5}' = '' OR QH.NO_IMO = '{5}')
                          AND ('{6}' = '' OR QH.NO_EMP = '{6}')
                          AND ('{7}' = '' OR SG.CD_SALEORG = '{7}') 
                          AND ('{8}' = '' OR QH.CD_SALEGRP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT('{8}')))
                          GROUP BY LEFT(QH.DT_INQ, 6)
                          ORDER BY LEFT(QH.DT_INQ, 6)";

                query = string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
                                             this.dtp조회기간.StartDateToString,
                                             this.dtp조회기간.EndDateToString,
                                             D.GetString(this._flex재고["CD_ITEM"]),
                                             this.bpc거래처.QueryWhereIn_Pipe,
                                             this.ctx호선.CodeValue,
                                             this.ctx사원.CodeValue,
                                             this.ctx영업조직.CodeValue,
                                             this.bpc영업그룹.QueryWhereIn_Pipe);

                dt = DBHelper.GetDataTable(query);

                if (dt.Rows == null || dt.Rows.Count == 0)
                    this.chart재고.ChartFx.Data.Clear();
                else
                {
                    this.chart재고.DataSource = dt;
                    this.chart재고.ChartFx.Series[0].Text = "견적수량";
                    this.chart재고.ChartFx.Series[1].Text = "예약수량";
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex발주키워드_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt;
            DataRow dr;
            string filter;

            try
            {
                filter = "NM_ITEM_PARTNER LIKE '%" + D.GetString(this._flex발주키워드["KEYWORD"]).Replace("'", "''")
                                                                                                 .Replace("[", "[[") + "%'";
                this._flex발주리스트.RowFilter = filter;

                var temp = this._flex발주리스트.DataTable.Select(filter)
                                                         .AsEnumerable()
                                                         .GroupBy(x => x["DT_PO"].ToString().Substring(0, 6), y => y, (x, y) => new { key = x, count = y.Count() })
                                                         .OrderBy(x => x.key);

                dt = new DataTable();
                dt.Columns.Add("DT_PO", typeof(string));
                dt.Columns.Add("COUNT", typeof(int));

                foreach (var temp1 in temp)
                {
                    dr = dt.NewRow();
                    dr["DT_PO"] = temp1.key;
                    dr["COUNT"] = temp1.count;
                    dt.Rows.Add(dr);
                }

                this.chart발주.DataSource = dt;
                this.chart발주.ChartFx.Series[0].Text = "빈도수";
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

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

                if (grid.Cols["NO_FILE"] != null && grid.ColSel == grid.Cols["NO_FILE"].Index && !string.IsNullOrEmpty(D.GetString(grid["NO_FILE"])))
                {
                    pageId = "P_CZ_SA_QTN_REG";
                    pageName = Global.MainFrame.DD("견적등록");
                    obj = new object[] { D.GetString(grid["NO_FILE"]) };

                    if (Global.MainFrame.IsExistPage(pageId, false))
                        Global.MainFrame.UnLoadPage(pageId, false);

                    Global.MainFrame.LoadPageFrom(pageId, pageName, this.Grant, obj);
                }
                else if (grid.Cols["NO_SO"] != null && grid.ColSel == grid.Cols["NO_SO"].Index && !string.IsNullOrEmpty(D.GetString(grid["NO_SO"])))
                {
                    pageId = "P_CZ_SA_SO_REG";
                    pageName = Global.MainFrame.DD("수주등록");
                    obj = new object[] { D.GetString(grid["NO_SO"]), false };

                    if (Global.MainFrame.IsExistPage(pageId, false))
                        Global.MainFrame.UnLoadPage(pageId, false);

                    Global.MainFrame.LoadPageFrom(pageId, pageName, this.Grant, obj);
                }
                else if (grid.Cols["NM_EMP"] != null && grid.ColSel == grid.Cols["NM_EMP"].Index && !string.IsNullOrEmpty(D.GetString(grid["NM_EMP"])))
                {
                    pageId = "P_CZ_SA_CRM_EMP";
                    pageName = Global.MainFrame.DD("사원(CRM)");
                    obj = new object[] { D.GetString(grid["NO_EMP"]), D.GetString(grid["NM_EMP"]) };

                    if (Global.MainFrame.IsExistPage(pageId, false))
                        Global.MainFrame.UnLoadPage(pageId, false);

                    Global.MainFrame.LoadPageFrom(pageId, pageName, this.Grant, obj);
                }
                else if (grid.Cols["LN_PARTNER"] != null && grid.ColSel == grid.Cols["LN_PARTNER"].Index && !string.IsNullOrEmpty(D.GetString(grid["LN_PARTNER"])))
                {
                    pageId = "P_CZ_SA_CRM_PARTNER";
                    pageName = Global.MainFrame.DD("거래처(CRM)");
                    obj = new object[] { D.GetString(grid["CD_PARTNER"]), D.GetString(grid["LN_PARTNER"]) };

                    if (Global.MainFrame.IsExistPage(pageId, false))
                        Global.MainFrame.UnLoadPage(pageId, false);

                    Global.MainFrame.LoadPageFrom(pageId, pageName, this.Grant, obj);
                }
                else if (grid.Cols["NM_SUPPLIER"] != null && grid.ColSel == grid.Cols["NM_SUPPLIER"].Index && !string.IsNullOrEmpty(D.GetString(grid["NM_SUPPLIER"])))
                {
                    pageId = "P_CZ_SA_CRM_PARTNER";
                    pageName = Global.MainFrame.DD("거래처(CRM)");
                    obj = new object[] { D.GetString(grid["CD_SUPPLIER"]), D.GetString(grid["NM_SUPPLIER"]) };

                    if (Global.MainFrame.IsExistPage(pageId, false))
                        Global.MainFrame.UnLoadPage(pageId, false);

                    Global.MainFrame.LoadPageFrom(pageId, pageName, this.Grant, obj);
                }
                else if (grid.Cols["NO_CLAIM"] != null && grid.ColSel == grid.Cols["NO_CLAIM"].Index && !string.IsNullOrEmpty(D.GetString(grid["NO_CLAIM"])))
                {
                    pageId = "P_CZ_SA_CLAIM";
                    pageName = Global.MainFrame.DD("클레임관리");
                    obj = new object[] { D.GetString(grid["NO_CLAIM"]) };

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

        private object[] 조회조건()
        {
            int 조회단위;

            if (this.cbo조회단위.SelectedValue.ToString() == "000")
                조회단위 = 4;
            else if (this.cbo조회단위.SelectedValue.ToString() == "001")
                조회단위 = 6;
            else
                조회단위 = 8;

            return new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                  this.dtp조회기간.StartDateToString,
                                  this.dtp조회기간.EndDateToString,
                                  this.bpc거래처.QueryWhereIn_Pipe,
                                  this.ctx호선.CodeValue,
                                  this.ctx사원.CodeValue,
                                  D.GetString(this.cbo품목군.SelectedValue),
                                  this.ctx영업조직.CodeValue,
                                  this.bpc영업그룹.QueryWhereIn_Pipe,
                                  조회단위 };
        }
    }
}

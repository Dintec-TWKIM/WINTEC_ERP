using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Web;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Util.Uploader;
using Duzon.Erpiu.ComponentModel;
using Duzon.ERPU;
using DzHelpFormLib;
using ChartFX.WinForms;
using DevExpress.XtraPivotGrid;
using DevExpress.Data.PivotGrid;
using DevExpress.Utils;
using Duzon.Common.Controls;

namespace cz
{
    public partial class P_CZ_SA_CRM_PARTNER : PageBase
    {
        #region 초기화 & 전역변수
        P_CZ_SA_CRM_PARTNER_BIZ _biz;
        FileUploader _fileUploader;
        FreeBinding _기본정보;
        ContextMenu _ctm거래처;
        private string _거래처코드, _거래처명;

        public P_CZ_SA_CRM_PARTNER()
        {
            StartUp.Certify(this);
            InitializeComponent();
        }

        public P_CZ_SA_CRM_PARTNER(string 거래처코드, string 거래처명)
        {
            StartUp.Certify(this);
            InitializeComponent();

            this.bpc거래처.AddItem(거래처코드, 거래처명);
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this._biz = new P_CZ_SA_CRM_PARTNER_BIZ();
            this._fileUploader = new FileUploader(this.MainFrameInterface);
            this._기본정보 = new FreeBinding();

            this.InitGrid();
            this.InitEvent();
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this._ctm거래처 = new ContextMenu();
            this._ctm거래처.MenuItems.Add(Global.MainFrame.DD("추가"), new EventHandler(this.btn거래처이미지추가_Click));
            this._ctm거래처.MenuItems.Add(Global.MainFrame.DD("삭제"), new EventHandler(this.btn거래처이미지삭제_Click));
            this.pic거래처이미지.ContextMenu = this._ctm거래처;

            this.split거래처명.SplitterDistance = 750;
            this.split좌측.SplitterDistance = 528;

            this.split영업활동.Panel2Collapsed = true;
            this.split미팅메모.Panel2Collapsed = true;
            this.split커미션.Panel2Collapsed = true;
            this.split메모.Panel2Collapsed = true;

            this.dtp조회기간.StartDateToString = Global.MainFrame.GetDateTimeToday().AddYears(-1).ToString("yyyyMMdd");
            this.dtp조회기간.EndDateToString = Global.MainFrame.GetStringToday;

            this.dtp거래내역매출.StartDateToString = Global.MainFrame.GetDateTimeToday().AddYears(-1).ToString("yyyyMMdd");
            this.dtp거래내역매출.EndDateToString = Global.MainFrame.GetStringToday;

            this.dtp거래내역매입.StartDateToString = Global.MainFrame.GetDateTimeToday().AddYears(-1).ToString("yyyyMMdd");
            this.dtp거래내역매입.EndDateToString = Global.MainFrame.GetStringToday;

            this.dtp호선.StartDateToString = Global.MainFrame.GetDateTimeToday().AddYears(-1).ToString("yyyyMMdd");
            this.dtp호선.EndDateToString = Global.MainFrame.GetStringToday;

            this.dtp미수금.StartDateToString = Global.MainFrame.GetDateTimeToday().AddYears(-1).ToString("yyyyMMdd");
            this.dtp미수금.EndDateToString = Global.MainFrame.GetStringToday;

            this.dtp클레임.StartDateToString = Global.MainFrame.GetDateTimeToday().AddYears(-1).ToString("yyyyMMdd");
            this.dtp클레임.EndDateToString = Global.MainFrame.GetStringToday;

            this.dtp재고판매.StartDateToString = Global.MainFrame.GetDateTimeToday().AddYears(-1).ToString("yyyyMMdd");
            this.dtp재고판매.EndDateToString = Global.MainFrame.GetStringToday;

            this.dtp판매품목.StartDateToString = Global.MainFrame.GetDateTimeToday().AddYears(-1).ToString("yyyyMMdd");
            this.dtp판매품목.EndDateToString = Global.MainFrame.GetStringToday;

            this.cbo거래처구분.DataSource = Global.MainFrame.GetComboDataCombine("S;MA_B000001");
            this.cbo거래처구분.ValueMember = "CODE";
            this.cbo거래처구분.DisplayMember = "NAME";

            this.cbo거래처분류.DataSource = Global.MainFrame.GetComboDataCombine("S;MA_B000003");
            this.cbo거래처분류.ValueMember = "CODE";
            this.cbo거래처분류.DisplayMember = "NAME";

            this.cbo지역구분.DataSource = Global.MainFrame.GetComboDataCombine("S;MA_B000062");
            this.cbo지역구분.ValueMember = "CODE";
            this.cbo지역구분.DisplayMember = "NAME";

            this.cbo국가명.DataSource = Global.MainFrame.GetComboDataCombine("S;MA_B000020");
            this.cbo국가명.ValueMember = "CODE";
            this.cbo국가명.DisplayMember = "NAME";

            this.cbo커미션통화명.DataSource = Global.MainFrame.GetComboDataCombine("S;MA_B000005");
            this.cbo커미션통화명.ValueMember = "CODE";
            this.cbo커미션통화명.DisplayMember = "NAME";

            this.cbo일자유형.DataSource = MA.GetCodeUser(new string[] { "001", "002" }, new string[] { "견적일자", "수주일자" });
            this.cbo일자유형.ValueMember = "CODE";
            this.cbo일자유형.DisplayMember = "NAME";

            SetControl setControl = new SetControl();

            setControl.SetCombobox(this.cboINQ수신방법, new DataView(MA.GetCode("CZ_SA00016", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cboQTN발신방법, new DataView(MA.GetCode("CZ_SA00017", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
            setControl.SetCombobox(this.cboPO수신방법, new DataView(MA.GetCode("CZ_PU00002", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());


            this.cboACK발신방법.DataSource = DBHelper.GetDataTable(@"SELECT '' AS CODE, 
																  		    '' AS NAME
																     UNION ALL
																     SELECT CD_SYSDEF AS CODE,
																  		    NM_SYSDEF AS NAME
																     FROM CZ_MA_CODEDTL WITH(NOLOCK)
																     WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                                   @"AND CD_FIELD = 'CZ_MA00048'
																     ORDER BY NAME ASC");
            this.cboACK발신방법.ValueMember = "CODE";
            this.cboACK발신방법.DisplayMember = "NAME";

            if (!string.IsNullOrEmpty(this.bpc거래처.SelectedText))
                this.OnToolBarSearchButtonClicked(null, null);
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flex영업활동,
                                              this._flex미팅메모,
                                              this._flex커미션,
                                              this._flex메모 };

            #region 거래내역

            #region 매출

            #region 요약
            this.pivot거래내역매출.SetStart();

            this.pivot거래내역매출.AddPivotField("NO_FILE", "파일번호", 100, true, PivotArea.FilterArea);
            this.pivot거래내역매출.AddPivotField("NM_VESSEL", "호선명", 100, true, PivotArea.FilterArea);
            this.pivot거래내역매출.AddPivotField("NM_EMP", "담당자", 100, true, PivotArea.FilterArea);
            this.pivot거래내역매출.AddPivotField("DT_INQ", "문의일자", 100, true, PivotArea.FilterArea);
            this.pivot거래내역매출.AddPivotField("DT_QTN", "견적일자", 100, true, PivotArea.FilterArea);
            this.pivot거래내역매출.AddPivotField("DT_SO", "수주일자", 100, true, PivotArea.FilterArea);

            this.pivot거래내역매출.AddPivotField("NM_ITEMGRP", "품목군", 100, true, PivotArea.RowArea);

            this.pivot거래내역매출.AddPivotField("QT_QTN", "견적건수", 100, true, PivotArea.DataArea);
            this.pivot거래내역매출.AddPivotField("QT_SO", "수주건수", 100, true, PivotArea.DataArea);
            this.pivot거래내역매출.AddPivotField("AM_SO_QTN", "견적금액(매출)", 120, true, PivotArea.DataArea);
            this.pivot거래내역매출.AddPivotField("AM_PO_QTN", "견적금액(매입)", 120, true, PivotArea.DataArea);
            this.pivot거래내역매출.AddPivotField("AM_SO", "수주금액", 100, true, PivotArea.DataArea);
            this.pivot거래내역매출.AddPivotField("AM_PO", "발주금액", 100, true, PivotArea.DataArea);
            this.pivot거래내역매출.AddPivotField("AM_STOCK", "재고금액", 100, true, PivotArea.DataArea);
            this.pivot거래내역매출.AddPivotField("AM_PROFIT_QTN", "이윤(견적)", 100, true, PivotArea.DataArea);
            this.pivot거래내역매출.AddPivotField("AM_PROFIT", "이윤(수주)", 100, true, PivotArea.DataArea);
            this.pivot거래내역매출.AddPivotField("RT_SO", "수주율", 100, true, PivotSummaryType.Custom, PivotArea.DataArea);
            this.pivot거래내역매출.AddPivotField("RT_PROFIT_QTN", "이윤율(견적)", 120, true, PivotSummaryType.Custom, PivotArea.DataArea);
            this.pivot거래내역매출.AddPivotField("RT_PROFIT", "이윤율(수주)", 120, true, PivotSummaryType.Custom, PivotArea.DataArea);

            this.pivot거래내역매출.PivotGridControl.Fields["QT_QTN"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot거래내역매출.PivotGridControl.Fields["QT_QTN"].CellFormat.FormatString = "0";
            this.pivot거래내역매출.PivotGridControl.Fields["QT_SO"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot거래내역매출.PivotGridControl.Fields["QT_SO"].CellFormat.FormatString = "0";
            this.pivot거래내역매출.PivotGridControl.Fields["AM_SO_QTN"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot거래내역매출.PivotGridControl.Fields["AM_SO_QTN"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this.pivot거래내역매출.PivotGridControl.Fields["AM_PO_QTN"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot거래내역매출.PivotGridControl.Fields["AM_PO_QTN"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this.pivot거래내역매출.PivotGridControl.Fields["AM_SO"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot거래내역매출.PivotGridControl.Fields["AM_SO"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this.pivot거래내역매출.PivotGridControl.Fields["AM_PO"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot거래내역매출.PivotGridControl.Fields["AM_PO"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this.pivot거래내역매출.PivotGridControl.Fields["AM_STOCK"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot거래내역매출.PivotGridControl.Fields["AM_STOCK"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this.pivot거래내역매출.PivotGridControl.Fields["AM_PROFIT_QTN"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot거래내역매출.PivotGridControl.Fields["AM_PROFIT_QTN"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this.pivot거래내역매출.PivotGridControl.Fields["AM_PROFIT"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot거래내역매출.PivotGridControl.Fields["AM_PROFIT"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this.pivot거래내역매출.PivotGridControl.Fields["RT_SO"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot거래내역매출.PivotGridControl.Fields["RT_SO"].CellFormat.FormatString = "#,###,###,###,##0.00%";
            this.pivot거래내역매출.PivotGridControl.Fields["RT_PROFIT_QTN"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot거래내역매출.PivotGridControl.Fields["RT_PROFIT_QTN"].CellFormat.FormatString = "#,###,###,###,##0.00%";
            this.pivot거래내역매출.PivotGridControl.Fields["RT_PROFIT"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot거래내역매출.PivotGridControl.Fields["RT_PROFIT"].CellFormat.FormatString = "#,###,###,###,##0.00%";

            this.pivot거래내역매출.SetEnd();
            #endregion

            #region 리스트
            this._flex거래내역매출.BeginSetting(1, 1, false);

            this._flex거래내역매출.SetCol("NO_FILE", "파일번호", 80);
            this._flex거래내역매출.SetCol("NM_VESSEL", "호선명", 100);
            this._flex거래내역매출.SetCol("NM_EMP", "담당자", 80);
            this._flex거래내역매출.SetCol("NM_ITEMGRP", "품목군", 80);
            this._flex거래내역매출.SetCol("DT_INQ", "문의일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex거래내역매출.SetCol("DT_QTN", "견적일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex거래내역매출.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex거래내역매출.SetCol("AM_SO_QTN", "견적금액(매출)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex거래내역매출.SetCol("AM_PO_QTN", "견적금액(매입)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex거래내역매출.SetCol("AM_SO", "수주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex거래내역매출.SetCol("AM_PO", "발주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex거래내역매출.SetCol("AM_STOCK", "재고금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex거래내역매출.SetCol("AM_PROFIT_QTN", "이윤(견적)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex거래내역매출.SetCol("AM_PROFIT", "이윤(수주)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex거래내역매출.SetCol("RT_PROFIT_QTN", "이윤율(견적)", 120, false, typeof(decimal), FormatTpType.RATE);
            this._flex거래내역매출.SetCol("RT_PROFIT", "이윤율(수주)", 120, false, typeof(decimal), FormatTpType.RATE);

            this._flex거래내역매출.SettingVersion = "0.0.0.1";
            this._flex거래내역매출.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex거래내역매출.SetExceptSumCol("RT_PROFIT_QTN", "RT_PROFIT");
            #endregion

            #endregion

            #region 매입

            #region 요약
            this.pivot거래내역매입.SetStart();

            this.pivot거래내역매입.AddPivotField("NO_FILE", "파일번호", 100, true, PivotArea.FilterArea);
            this.pivot거래내역매입.AddPivotField("NM_EMP", "담당자", 100, true, PivotArea.FilterArea);
            this.pivot거래내역매입.AddPivotField("DT_INQ", "문의일자", 100, true, PivotArea.FilterArea);
            this.pivot거래내역매입.AddPivotField("DT_QTN", "견적일자", 100, true, PivotArea.FilterArea);
            this.pivot거래내역매입.AddPivotField("DT_PO", "발주일자", 100, true, PivotArea.FilterArea);
            this.pivot거래내역매입.AddPivotField("DT_IN", "입고일자", 100, true, PivotArea.FilterArea);
            this.pivot거래내역매입.AddPivotField("LT", "납기", 100, true, PivotArea.FilterArea);
            this.pivot거래내역매입.AddPivotField("LT_IN", "납품소요일", 100, true, PivotArea.FilterArea);

            this.pivot거래내역매입.AddPivotField("NM_ITEMGRP", "품목군", 100, true, PivotArea.RowArea);

            this.pivot거래내역매입.AddPivotField("QT_INQ", "문의건수", 100, true, PivotArea.DataArea);
            this.pivot거래내역매입.AddPivotField("QT_QTN", "견적건수", 100, true, PivotArea.DataArea);
            this.pivot거래내역매입.AddPivotField("QT_SO", "수주건수", 100, true, PivotArea.DataArea);
            this.pivot거래내역매입.AddPivotField("QT_PO", "발주건수", 100, true, PivotArea.DataArea);
            this.pivot거래내역매입.AddPivotField("QT_STOCK", "재고건수", 100, true, PivotArea.DataArea);
            this.pivot거래내역매입.AddPivotField("AM_INQ", "문의금액", 100, true, PivotArea.DataArea);
            this.pivot거래내역매입.AddPivotField("AM_PO_QTN", "견적금액(매입)", 120, true, PivotArea.DataArea);
            this.pivot거래내역매입.AddPivotField("AM_SO_QTN", "견적금액(매출)", 120, true, PivotArea.DataArea);
            this.pivot거래내역매입.AddPivotField("AM_SO", "수주금액", 100, true, PivotArea.DataArea);
            this.pivot거래내역매입.AddPivotField("AM_SO_PO", "수주금액(발주만)", 120, true, PivotArea.DataArea);
            this.pivot거래내역매입.AddPivotField("AM_PO", "발주금액", 100, true, PivotArea.DataArea);
            this.pivot거래내역매입.AddPivotField("AM_STOCK", "재고금액", 100, true, PivotArea.DataArea);
            this.pivot거래내역매입.AddPivotField("AM_PROFIT_QTN", "이윤(견적)", 120, true, PivotArea.DataArea);
            this.pivot거래내역매입.AddPivotField("AM_PROFIT_PO", "이윤(발주만)", 120, true, PivotArea.DataArea);
            this.pivot거래내역매입.AddPivotField("AM_PROFIT", "이윤(재고포함)", 120, true, PivotArea.DataArea);
            this.pivot거래내역매입.AddPivotField("RT_CHOICE", "선택율", 60, true, PivotSummaryType.Custom, PivotArea.DataArea);
            this.pivot거래내역매입.AddPivotField("RT_SO", "수주율", 60, true, PivotSummaryType.Custom, PivotArea.DataArea);
            this.pivot거래내역매입.AddPivotField("RT_PO", "발주율", 60, true, PivotSummaryType.Custom, PivotArea.DataArea);
            this.pivot거래내역매입.AddPivotField("RT_PROFIT_QTN", "이윤율(견적)", 120, true, PivotSummaryType.Custom, PivotArea.DataArea);
            this.pivot거래내역매입.AddPivotField("RT_PROFIT_PO", "이윤율(발주만)", 120, true, PivotSummaryType.Custom, PivotArea.DataArea);
            this.pivot거래내역매입.AddPivotField("RT_PROFIT", "이윤율(재고포함)", 120, true, PivotSummaryType.Custom, PivotArea.DataArea);

            this.pivot거래내역매입.PivotGridControl.Fields["QT_INQ"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot거래내역매입.PivotGridControl.Fields["QT_INQ"].CellFormat.FormatString = "0";
            this.pivot거래내역매입.PivotGridControl.Fields["QT_QTN"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot거래내역매입.PivotGridControl.Fields["QT_QTN"].CellFormat.FormatString = "0";
            this.pivot거래내역매입.PivotGridControl.Fields["QT_SO"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot거래내역매입.PivotGridControl.Fields["QT_SO"].CellFormat.FormatString = "0";
            this.pivot거래내역매입.PivotGridControl.Fields["QT_PO"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot거래내역매입.PivotGridControl.Fields["QT_PO"].CellFormat.FormatString = "0";
            this.pivot거래내역매입.PivotGridControl.Fields["QT_STOCK"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot거래내역매입.PivotGridControl.Fields["QT_STOCK"].CellFormat.FormatString = "0";
            this.pivot거래내역매입.PivotGridControl.Fields["AM_INQ"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot거래내역매입.PivotGridControl.Fields["AM_INQ"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this.pivot거래내역매입.PivotGridControl.Fields["AM_PO_QTN"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot거래내역매입.PivotGridControl.Fields["AM_PO_QTN"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this.pivot거래내역매입.PivotGridControl.Fields["AM_SO_QTN"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot거래내역매입.PivotGridControl.Fields["AM_SO_QTN"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this.pivot거래내역매입.PivotGridControl.Fields["AM_SO"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot거래내역매입.PivotGridControl.Fields["AM_SO"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this.pivot거래내역매입.PivotGridControl.Fields["AM_SO_PO"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot거래내역매입.PivotGridControl.Fields["AM_SO_PO"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this.pivot거래내역매입.PivotGridControl.Fields["AM_PO"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot거래내역매입.PivotGridControl.Fields["AM_PO"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this.pivot거래내역매입.PivotGridControl.Fields["AM_STOCK"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot거래내역매입.PivotGridControl.Fields["AM_STOCK"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this.pivot거래내역매입.PivotGridControl.Fields["AM_PROFIT_QTN"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot거래내역매입.PivotGridControl.Fields["AM_PROFIT_QTN"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this.pivot거래내역매입.PivotGridControl.Fields["AM_PROFIT_PO"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot거래내역매입.PivotGridControl.Fields["AM_PROFIT_PO"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this.pivot거래내역매입.PivotGridControl.Fields["AM_PROFIT"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot거래내역매입.PivotGridControl.Fields["AM_PROFIT"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this.pivot거래내역매입.PivotGridControl.Fields["RT_CHOICE"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot거래내역매입.PivotGridControl.Fields["RT_CHOICE"].CellFormat.FormatString = "#,###,###,###,##0.00%";
            this.pivot거래내역매입.PivotGridControl.Fields["RT_SO"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot거래내역매입.PivotGridControl.Fields["RT_SO"].CellFormat.FormatString = "#,###,###,###,##0.00%";
            this.pivot거래내역매입.PivotGridControl.Fields["RT_PO"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot거래내역매입.PivotGridControl.Fields["RT_PO"].CellFormat.FormatString = "#,###,###,###,##0.00%";
            this.pivot거래내역매입.PivotGridControl.Fields["RT_PROFIT_QTN"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot거래내역매입.PivotGridControl.Fields["RT_PROFIT_QTN"].CellFormat.FormatString = "#,###,###,###,##0.00%";
            this.pivot거래내역매입.PivotGridControl.Fields["RT_PROFIT_PO"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot거래내역매입.PivotGridControl.Fields["RT_PROFIT_PO"].CellFormat.FormatString = "#,###,###,###,##0.00%";
            this.pivot거래내역매입.PivotGridControl.Fields["RT_PROFIT"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot거래내역매입.PivotGridControl.Fields["RT_PROFIT"].CellFormat.FormatString = "#,###,###,###,##0.00%";

            this.pivot거래내역매입.SetEnd();
            #endregion

            #region 리스트
            this._flex거래내역매입.BeginSetting(1, 1, false);

            this._flex거래내역매입.SetCol("NO_FILE", "파일번호", 80);
            this._flex거래내역매입.SetCol("NM_EMP", "담당자", 80);
            this._flex거래내역매입.SetCol("NM_ITEMGRP", "품목군", 80);
            this._flex거래내역매입.SetCol("DT_INQ", "문의일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex거래내역매입.SetCol("DT_QTN", "견적일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex거래내역매입.SetCol("DT_PO", "발주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex거래내역매입.SetCol("DT_IN", "입고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex거래내역매입.SetCol("LT", "납기", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex거래내역매입.SetCol("LT_IN", "납품소요일", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex거래내역매입.SetCol("AM_INQ", "문의금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex거래내역매입.SetCol("AM_PO_QTN", "견적금액(매입)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex거래내역매입.SetCol("AM_SO_QTN", "견적금액(매출)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex거래내역매입.SetCol("AM_SO", "수주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex거래내역매입.SetCol("AM_SO_PO", "수주금액(발주만)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex거래내역매입.SetCol("AM_PO", "발주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex거래내역매입.SetCol("AM_STOCK", "재고금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex거래내역매입.SetCol("AM_PROFIT_QTN", "이윤(견적)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex거래내역매입.SetCol("AM_PROFIT_PO", "이윤(발주만)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex거래내역매입.SetCol("AM_PROFIT", "이윤(재고포함)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex거래내역매입.SetCol("RT_PROFIT_QTN", "이윤율(견적)", 120, false, typeof(decimal), FormatTpType.RATE);
            this._flex거래내역매입.SetCol("RT_PROFIT_PO", "이윤율(발주만)", 120, false, typeof(decimal), FormatTpType.RATE);
            this._flex거래내역매입.SetCol("RT_PROFIT", "이윤율(재고포함)", 120, false, typeof(decimal), FormatTpType.RATE);

            this._flex거래내역매입.SettingVersion = "0.0.0.1";
            this._flex거래내역매입.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex거래내역매입.SetExceptSumCol("LT", "LT_IN", "RT_PROFIT_QTN", "RT_PROFIT_PO", "RT_PROFIT");
            #endregion

            #endregion
            
            #endregion

            #region 미수금

            #region 요약
            this.pivot미수금.SetStart();

            this.pivot미수금.AddPivotField("NO_IV", "계산서번호", 100, true, PivotArea.FilterArea);
            this.pivot미수금.AddPivotField("NO_SO", "수주번호", 100, true, PivotArea.FilterArea);
            this.pivot미수금.AddPivotField("NO_PO_PARTNER", "매출처발주번호", 100, true, PivotArea.FilterArea);
            this.pivot미수금.AddPivotField("DT_IV", "매출일자", 100, true, PivotArea.FilterArea);
            this.pivot미수금.AddPivotField("DT_RCP", "수금일자", 100, true, PivotArea.FilterArea);
            this.pivot미수금.AddPivotField("NM_PARTNER", "매출처", 100, true, PivotArea.FilterArea);
            this.pivot미수금.AddPivotField("NO_IMO", "IMO번호", 100, true, PivotArea.FilterArea);
            this.pivot미수금.AddPivotField("NM_VESSEL", "호선명", 100, true, PivotArea.FilterArea);
            this.pivot미수금.AddPivotField("NM_EMP", "담당자", 100, true, PivotArea.FilterArea);
            this.pivot미수금.AddPivotField("NM_DEPT", "부서", 100, true, PivotArea.FilterArea);
            this.pivot미수금.AddPivotField("NM_SALEGRP", "영업그룹", 100, true, PivotArea.FilterArea);
            this.pivot미수금.AddPivotField("NM_EXCH", "통화명", 100, true, PivotArea.FilterArea);
            this.pivot미수금.AddPivotField("RT_EXCH", "환율", 100, true, PivotArea.FilterArea);

            this.pivot미수금.AddPivotField("DT_PROGRESS", "경과월수", 100, true, PivotArea.RowArea);

            this.pivot미수금.AddPivotField("AM_EX_CLS", "채권외화금액", 100, true, PivotArea.DataArea);
            this.pivot미수금.AddPivotField("AM_CLS", "채권원화금액", 100, true, PivotArea.DataArea);
            this.pivot미수금.AddPivotField("AM_EX_CHARGE", "외화비용", 100, true, PivotArea.DataArea);
            this.pivot미수금.AddPivotField("AM_CHARGE", "원화비용", 100, true, PivotArea.DataArea);
            this.pivot미수금.AddPivotField("AM_EX_NET", "순외화금액", 100, true, PivotArea.DataArea);
            this.pivot미수금.AddPivotField("AM_NET", "순원화금액", 100, true, PivotArea.DataArea);
            this.pivot미수금.AddPivotField("AM_RCP_EX", "수금외화금액", 100, true, PivotArea.DataArea);
            this.pivot미수금.AddPivotField("AM_RCP", "수금원화금액", 100, true, PivotArea.DataArea);
            this.pivot미수금.AddPivotField("AM_EX_REMAIN", "미수외화금액", 100, true, PivotArea.DataArea);
            this.pivot미수금.AddPivotField("AM_REMAIN", "미수원화금액", 100, true, PivotArea.DataArea);

            this.pivot미수금.PivotGridControl.Fields["AM_EX_CLS"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot미수금.PivotGridControl.Fields["AM_EX_CLS"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this.pivot미수금.PivotGridControl.Fields["AM_CLS"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot미수금.PivotGridControl.Fields["AM_CLS"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this.pivot미수금.PivotGridControl.Fields["AM_EX_CHARGE"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot미수금.PivotGridControl.Fields["AM_EX_CHARGE"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this.pivot미수금.PivotGridControl.Fields["AM_CHARGE"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot미수금.PivotGridControl.Fields["AM_CHARGE"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this.pivot미수금.PivotGridControl.Fields["AM_EX_NET"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot미수금.PivotGridControl.Fields["AM_EX_NET"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this.pivot미수금.PivotGridControl.Fields["AM_NET"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot미수금.PivotGridControl.Fields["AM_NET"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this.pivot미수금.PivotGridControl.Fields["AM_RCP_EX"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot미수금.PivotGridControl.Fields["AM_RCP_EX"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this.pivot미수금.PivotGridControl.Fields["AM_RCP"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot미수금.PivotGridControl.Fields["AM_RCP"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this.pivot미수금.PivotGridControl.Fields["AM_EX_REMAIN"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot미수금.PivotGridControl.Fields["AM_EX_REMAIN"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this.pivot미수금.PivotGridControl.Fields["AM_REMAIN"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot미수금.PivotGridControl.Fields["AM_REMAIN"].CellFormat.FormatString = "#,###,###,###,##0.##";

            this.pivot미수금.SetEnd();
            #endregion

            #region 리스트
            this._flex미수금.BeginSetting(1, 1, false);

            this._flex미수금.SetCol("NO_IV", "계산서번호", 100);
            this._flex미수금.SetCol("NO_SO", "수주번호", 100);
            this._flex미수금.SetCol("NO_PO_PARTNER", "매출처발주번호", 100);
            this._flex미수금.SetCol("DT_IV", "매출일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex미수금.SetCol("DT_RCP", "수금일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex미수금.SetCol("DT_PROGRESS", "경과월수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex미수금.SetCol("NM_PARTNER", "매출처", 100);
            this._flex미수금.SetCol("NO_IMO", "IMO번호", 100);
            this._flex미수금.SetCol("NM_VESSEL", "호선명", 100);
            this._flex미수금.SetCol("NM_EMP", "담당자", 60);
            this._flex미수금.SetCol("NM_DEPT", "부서", 60);
            this._flex미수금.SetCol("NM_SALEGRP", "영업그룹", 80);
            this._flex미수금.SetCol("NM_EXCH", "통화명", 60);
            this._flex미수금.SetCol("RT_EXCH", "환율", 60, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flex미수금.SetCol("AM_EX_CLS", "채권외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex미수금.SetCol("AM_CLS", "채권원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex미수금.SetCol("AM_EX_CHARGE", "외화비용", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex미수금.SetCol("AM_CHARGE", "원화비용", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex미수금.SetCol("AM_EX_NET", "순외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex미수금.SetCol("AM_NET", "순원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex미수금.SetCol("AM_RCP_EX", "수금외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex미수금.SetCol("AM_RCP", "수금원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex미수금.SetCol("AM_EX_REMAIN", "미수외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex미수금.SetCol("AM_REMAIN", "미수원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex미수금.SetCol("DC_RMK1", "수주비고(커미션)", 100);
            this._flex미수금.SetCol("DC_RMK", "비고", 100);

            this._flex미수금.ExtendLastCol = true;

            this._flex미수금.SettingVersion = "0.0.0.1";
            this._flex미수금.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            this._flex미수금.SetExceptSumCol("DT_PROGRESS");
            #endregion

            #endregion

            #region 클레임

            #region 요약
            this.pivot클레임.SetStart();

            this.pivot클레임.AddPivotField("NO_CLAIM", "클레임번호", 100, true, PivotArea.FilterArea);
            this.pivot클레임.AddPivotField("NO_SO", "수주번호", 100, true, PivotArea.FilterArea);
            this.pivot클레임.AddPivotField("DT_SO", "수주일자", 100, true, PivotArea.FilterArea);
            this.pivot클레임.AddPivotField("DT_INPUT", "발생일자", 100, true, PivotArea.FilterArea);
            this.pivot클레임.AddPivotField("DT_CLOSING", "종결일자", 100, true, PivotArea.FilterArea);
            this.pivot클레임.AddPivotField("NM_TP_CLAIM", "클레임사유", 100, true, PivotArea.FilterArea);
            this.pivot클레임.AddPivotField("NM_TP_CAUSE", "원인구분", 100, true, PivotArea.FilterArea);
            this.pivot클레임.AddPivotField("NM_TP_ITEM", "항목분류", 100, true, PivotArea.FilterArea);
            this.pivot클레임.AddPivotField("NM_VESSEL", "호선", 100, true, PivotArea.FilterArea);
            this.pivot클레임.AddPivotField("NM_KOR", "담당자", 100, true, PivotArea.FilterArea);
            this.pivot클레임.AddPivotField("DT_SO_DIFF", "발생일수", 100, true, PivotArea.FilterArea);
            this.pivot클레임.AddPivotField("DT_CLOSING_DIFF", "종결일수", 100, true, PivotArea.FilterArea);
            this.pivot클레임.AddPivotField("DC_CLAIM_CONTENS", "클레임내용", 100, true, PivotArea.FilterArea);

            this.pivot클레임.AddPivotField("NM_STATUS", "클레임상태", 100, true, PivotArea.RowArea);
            this.pivot클레임.AddPivotField("NM_GW_STATUS", "결재상태", 100, true, PivotArea.RowArea);

            this.pivot클레임.AddPivotField("QT_CLAIM", "클레임건수", 100, true, PivotArea.DataArea);
            this.pivot클레임.AddPivotField("AM_CLAIM", "클레임금액", 100, true, PivotArea.DataArea);
            
            this.pivot클레임.SetEnd();
            #endregion

            #region 리스트
            this._flex클레임.BeginSetting(1, 1, false);

            this._flex클레임.SetCol("NM_STATUS", "클레임상태", 80);
            this._flex클레임.SetCol("NM_GW_STATUS", "결재상태", 80);
            this._flex클레임.SetCol("NO_CLAIM", "클레임번호", 80);
            this._flex클레임.SetCol("NO_SO", "수주번호", 80);
            this._flex클레임.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex클레임.SetCol("DT_INPUT", "발생일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex클레임.SetCol("DT_CLOSING", "종결일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex클레임.SetCol("NM_TP_CLAIM", "클레임사유", 100);
            this._flex클레임.SetCol("NM_TP_CAUSE", "원인구분", 100);
            this._flex클레임.SetCol("NM_TP_ITEM", "항목분류", 100);
            this._flex클레임.SetCol("NM_VESSEL", "호선", 100);
            this._flex클레임.SetCol("NM_KOR", "담당자", 80);
            this._flex클레임.SetCol("QT_CLAIM", "클레임건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex클레임.SetCol("AM_CLAIM", "클레임금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex클레임.SetCol("DT_SO_DIFF", "발생일수", 80);
            this._flex클레임.SetCol("DT_CLOSING_DIFF", "종결일수", 80);
            this._flex클레임.SetCol("DC_CLAIM_CONTENS", "클레임내용", 100);

            this._flex클레임.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #endregion

            #region 영업활동
            this._flex영업활동.BeginSetting(1, 1, false);

            this._flex영업활동.SetCol("YN_TODO", "연동여부", 60, false, CheckTypeEnum.Y_N);
            this._flex영업활동.SetCol("NM_TITLE", "제목", 100);
            this._flex영업활동.SetCol("DT_START", "시작일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex영업활동.SetCol("DT_END", "종료일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex영업활동.SetCol("DC_ACTIVITY", "내용", 100);

            this._flex영업활동.SetOneGridBinding(null, new IUParentControl[] { this.pnl영업활동, this.pnl영업활동내용 });

            this._flex영업활동.ExtendLastCol = true;

            this._flex영업활동.SettingVersion = "0.0.0.1";
            this._flex영업활동.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            this._flex영업활동.SetDummyColumn(new string[] { "YN_TODO" });
            #endregion

            #region 미팅메모
            this._flex미팅메모.BeginSetting(1, 1, false);

            this._flex미팅메모.SetCol("NM_GW_STAT", "결재상태(미팅메모)", 80);
            this._flex미팅메모.SetCol("NM_GW_STAT1", "결재상태(출장보고서)", 80);
            this._flex미팅메모.SetCol("NO_MEETING", "미팅번호", 80);
            this._flex미팅메모.SetCol("DT_MEETING", "미팅일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex미팅메모.SetCol("DC_LOCATION", "장소", 100);
            this._flex미팅메모.SetCol("DC_SUBJECT", "주제", 100);
            this._flex미팅메모.SetCol("DC_PURPOSE", "목적", 100);
            this._flex미팅메모.SetCol("DC_MEETING", "미팅내용", 100);

            this._flex미팅메모.SetOneGridBinding(null, new IUParentControl[] { this.pnl미팅기본정보, this.pnl미팅내용 });

            this._flex미팅메모.ExtendLastCol = true;

            this._flex미팅메모.SettingVersion = "0.0.0.1";
            this._flex미팅메모.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region 커미션
            this._flex커미션.BeginSetting(1, 1, false);

            this._flex커미션.SetCol("NM_STAT", "결재상태", 80);
            this._flex커미션.SetCol("NO_COMMISSION", "커미션번호", 80);
            this._flex커미션.SetCol("DT_COMMISSION", "등록일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex커미션.SetCol("NM_USER", "등록자", 80);
            this._flex커미션.SetCol("CD_EXCH", "통화명", 80);
            this._flex커미션.SetCol("AM_COMMISSION", "지급금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex커미션.SetCol("DC_COMMISSION", "내용", 100);

            this._flex커미션.SetOneGridBinding(null, new IUParentControl[] { this.pnl커미션, this.pnl커미션내역 });
            this._flex커미션.SetDataMap("CD_EXCH", Global.MainFrame.GetComboDataCombine("S;MA_B000005"), "CODE", "NAME");
            this._flex커미션.ExtendLastCol = true;

            this._flex커미션.SettingVersion = "0.0.0.1";
            this._flex커미션.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region 메모
            this._flex메모.BeginSetting(1, 1, false);

            this._flex메모.SetCol("DC_COMMENT", "내용", 500);

            this._flex메모.SetOneGridBinding(null, new IUParentControl[] { this.pnl메모, this.pnl메모내용 });
            this._flex메모.VerifyNotNull = new string[] { "DC_COMMENT" };

            this._flex메모.ExtendLastCol = true;

            this._flex메모.SettingVersion = "0.0.0.1";
            this._flex메모.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region 담당자

            #region 요약
            this.pivot담당자.SetStart();

            this.pivot담당자.AddPivotField("NM_PTR", "담당자", 100, true, PivotArea.FilterArea);
            this.pivot담당자.AddPivotField("NM_DEPT", "부서", 100, true, PivotArea.FilterArea);
            this.pivot담당자.AddPivotField("NM_DUTY_RESP", "직책", 100, true, PivotArea.FilterArea);
            this.pivot담당자.AddPivotField("NM_EMAIL", "메일", 100, true, PivotArea.FilterArea);
            this.pivot담당자.AddPivotField("NO_HP", "휴대전화", 100, true, PivotArea.FilterArea);
            this.pivot담당자.AddPivotField("NO_TEL", "전화번호", 100, true, PivotArea.FilterArea);
            this.pivot담당자.AddPivotField("NO_FAX", "팩스", 100, true, PivotArea.FilterArea);

            this.pivot담당자.AddPivotField("NM_TP_PTR", "담당유형", 100, true, PivotArea.RowArea);

            this.pivot담당자.AddPivotField("QT_PIC", "담당자수", 100, true, PivotArea.DataArea);

            this.pivot담당자.SetEnd();
            #endregion

            #region 리스트
            this._flex담당자.BeginSetting(1, 1, false);

            this._flex담당자.SetCol("NM_TP_PTR", "담당유형", 100);
            this._flex담당자.SetCol("NM_PTR", "담당자", 100);
            this._flex담당자.SetCol("NM_DEPT", "부서", 100);
            this._flex담당자.SetCol("NM_DUTY_RESP", "직책", 100);
            this._flex담당자.SetCol("NM_EMAIL", "메일", 100);
            this._flex담당자.SetCol("NO_HP", "휴대전화", 100);
            this._flex담당자.SetCol("NO_TEL", "전화번호", 100);
            this._flex담당자.SetCol("NO_FAX", "팩스", 100);
            this._flex담당자.SetCol("CLIENT_NOTE", "비고", 100);

            this._flex담당자.ExtendLastCol = true;

            this._flex담당자.SettingVersion = "0.0.0.1";
            this._flex담당자.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion
            
            #endregion

            #region 호선

            #region 요약
            this.pivot호선.SetStart();

            this.pivot호선.AddPivotField("NO_IMO", "IMO번호", 100, true, PivotArea.FilterArea);
            this.pivot호선.AddPivotField("NO_HULL", "호선번호", 100, true, PivotArea.FilterArea);
            this.pivot호선.AddPivotField("NM_VESSEL", "호선명", 200, true, PivotArea.FilterArea);
            this.pivot호선.AddPivotField("DT_SHIP_DLV", "선박납기일자", 100, true, PivotArea.FilterArea);
            this.pivot호선.AddPivotField("DC_SHIPBUILDER", "조선소", 200, true, PivotArea.FilterArea);

            this.pivot호선.AddPivotField("NM_TP_SHIP", "호선유형", 200, true, PivotArea.RowArea);

            this.pivot호선.AddPivotField("QT_VESSEL", "호선수", 100, true, PivotArea.DataArea);
            this.pivot호선.AddPivotField("QT_QTN", "견적건수", 100, true, PivotArea.DataArea);
            this.pivot호선.AddPivotField("QT_SO", "수주건수", 100, true, PivotArea.DataArea);
            this.pivot호선.AddPivotField("AM_SO_QTN", "견적금액(매출)", 100, true, PivotArea.DataArea);
            this.pivot호선.AddPivotField("AM_PO_QTN", "견적금액(매입)", 100, true, PivotArea.DataArea);
            this.pivot호선.AddPivotField("AM_SO", "수주금액", 100, true, PivotArea.DataArea);
            this.pivot호선.AddPivotField("AM_PO", "발주금액", 100, true, PivotArea.DataArea);
            this.pivot호선.AddPivotField("AM_STOCK", "재고금액", 100, true, PivotArea.DataArea);
            this.pivot호선.AddPivotField("RT_SO", "수주율", 100, true, PivotSummaryType.Custom, PivotArea.DataArea);
            this.pivot호선.AddPivotField("RT_PROFIT_QTN", "이윤율(견적)", 100, true, PivotSummaryType.Custom, PivotArea.DataArea);
            this.pivot호선.AddPivotField("RT_PROFIT", "이윤율(수주)", 100, true, PivotSummaryType.Custom, PivotArea.DataArea);

            this.pivot호선.PivotGridControl.Fields["QT_VESSEL"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot호선.PivotGridControl.Fields["QT_VESSEL"].CellFormat.FormatString = "0";
            this.pivot호선.PivotGridControl.Fields["QT_QTN"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot호선.PivotGridControl.Fields["QT_QTN"].CellFormat.FormatString = "0";
            this.pivot호선.PivotGridControl.Fields["QT_SO"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot호선.PivotGridControl.Fields["QT_SO"].CellFormat.FormatString = "0";
            this.pivot호선.PivotGridControl.Fields["AM_SO_QTN"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot호선.PivotGridControl.Fields["AM_SO_QTN"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this.pivot호선.PivotGridControl.Fields["AM_PO_QTN"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot호선.PivotGridControl.Fields["AM_PO_QTN"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this.pivot호선.PivotGridControl.Fields["AM_SO"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot호선.PivotGridControl.Fields["AM_SO"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this.pivot호선.PivotGridControl.Fields["AM_PO"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot호선.PivotGridControl.Fields["AM_PO"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this.pivot호선.PivotGridControl.Fields["AM_STOCK"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot호선.PivotGridControl.Fields["AM_STOCK"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this.pivot호선.PivotGridControl.Fields["RT_SO"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot호선.PivotGridControl.Fields["RT_SO"].CellFormat.FormatString = "#,###,###,###,##0.00%";
            this.pivot호선.PivotGridControl.Fields["RT_PROFIT_QTN"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot호선.PivotGridControl.Fields["RT_PROFIT_QTN"].CellFormat.FormatString = "#,###,###,###,##0.00%";
            this.pivot호선.PivotGridControl.Fields["RT_PROFIT"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot호선.PivotGridControl.Fields["RT_PROFIT"].CellFormat.FormatString = "#,###,###,###,##0.00%";

            this.pivot호선.SetEnd();
            #endregion

            #region 리스트
            this._flex호선.BeginSetting(1, 1, false);

            this._flex호선.SetCol("NO_IMO", "IMO번호", 100);
            this._flex호선.SetCol("NO_HULL", "호선번호", 100);
            this._flex호선.SetCol("NM_VESSEL", "호선명", 200);
            this._flex호선.SetCol("DT_SHIP_DLV", "선박납기일자", 60, false, typeof(string), FormatTpType.YEAR_MONTH);
            this._flex호선.SetCol("NM_TP_SHIP", "호선유형", 100);
            this._flex호선.SetCol("DC_SHIPBUILDER", "조선소", 100);
            this._flex호선.SetCol("QT_QTN", "견적건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex호선.SetCol("QT_SO", "수주건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex호선.SetCol("NM_EXCH", "통화명", 100);
            this._flex호선.SetCol("AM_SO_QTN_EX", "견적외화금액(매출)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex호선.SetCol("AM_PO_QTN_EX", "견적외화금액(매입)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex호선.SetCol("AM_SO_EX", "수주외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex호선.SetCol("AM_PO_EX", "발주외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex호선.SetCol("AM_SO_QTN", "견적금액(매출)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex호선.SetCol("AM_PO_QTN", "견적금액(매입)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex호선.SetCol("AM_SO", "수주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex호선.SetCol("AM_PO", "발주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex호선.SetCol("AM_STOCK", "재고금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex호선.SetCol("AM_PROFIT_QTN", "이윤(견적)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex호선.SetCol("AM_PROFIT", "이윤(수주)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex호선.SetCol("RT_SO", "수주율", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex호선.SetCol("RT_PROFIT_QTN", "이윤율(견적)", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex호선.SetCol("RT_PROFIT", "이윤율(수주)", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex호선.SetCol("DC_RMK", "비고", 200);

            this._flex호선.ExtendLastCol = true;

            this._flex호선.SettingVersion = "0.0.0.1";
            this._flex호선.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #endregion

            #region 엔진
            
            #region 요약
            this.pivot엔진.SetStart();

            this.pivot엔진.AddPivotField("NO_IMO", "IMO번호", 100, true, PivotArea.FilterArea);
            this.pivot엔진.AddPivotField("NO_HULL", "호선번호", 100, true, PivotArea.FilterArea);
            this.pivot엔진.AddPivotField("NM_VESSEL", "호선명", 200, true, PivotArea.FilterArea);
            this.pivot엔진.AddPivotField("NM_MODEL", "모델명", 100, true, PivotArea.FilterArea);
            this.pivot엔진.AddPivotField("NM_MAKER", "제조사", 100, true, PivotArea.FilterArea);
            this.pivot엔진.AddPivotField("NM_CLS_L", "대분류", 100, true, PivotArea.FilterArea);
            this.pivot엔진.AddPivotField("NM_CLS_M", "중분류", 100, true, PivotArea.FilterArea);
            this.pivot엔진.AddPivotField("NM_CLS_S", "소분류", 100, true, PivotArea.FilterArea);
            this.pivot엔진.AddPivotField("CAPACITY", "용량", 100, true, PivotArea.FilterArea);
            this.pivot엔진.AddPivotField("SERIAL", "일련번호", 100, true, PivotArea.FilterArea);

            this.pivot엔진.AddPivotField("NM_ENGINE", "유형", 200, true, PivotArea.RowArea);

            this.pivot엔진.AddPivotField("QT_ENGINE", "엔진수", 100, true, PivotArea.DataArea);
            this.pivot엔진.AddPivotField("QT_QTN", "견적건수", 100, true, PivotArea.DataArea);
            this.pivot엔진.AddPivotField("QT_SO", "수주건수", 100, true, PivotArea.DataArea);
            this.pivot엔진.AddPivotField("AM_SO_QTN", "견적금액(매출)", 100, true, PivotArea.DataArea);
            this.pivot엔진.AddPivotField("AM_PO_QTN", "견적금액(매입)", 100, true, PivotArea.DataArea);
            this.pivot엔진.AddPivotField("AM_SO", "수주금액", 100, true, PivotArea.DataArea);
            this.pivot엔진.AddPivotField("AM_PO", "발주금액", 100, true, PivotArea.DataArea);
            this.pivot엔진.AddPivotField("AM_STOCK", "재고금액", 100, true, PivotArea.DataArea);
            this.pivot엔진.AddPivotField("RT_SO", "수주율", 100, true, PivotSummaryType.Custom, PivotArea.DataArea);
            this.pivot엔진.AddPivotField("RT_PROFIT_QTN", "이윤율(견적)", 100, true, PivotSummaryType.Custom, PivotArea.DataArea);
            this.pivot엔진.AddPivotField("RT_PROFIT", "이윤율(수주)", 100, true, PivotSummaryType.Custom, PivotArea.DataArea);

            this.pivot엔진.PivotGridControl.Fields["QT_ENGINE"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot엔진.PivotGridControl.Fields["QT_ENGINE"].CellFormat.FormatString = "0";
            this.pivot엔진.PivotGridControl.Fields["QT_QTN"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot엔진.PivotGridControl.Fields["QT_QTN"].CellFormat.FormatString = "0";
            this.pivot엔진.PivotGridControl.Fields["QT_SO"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot엔진.PivotGridControl.Fields["QT_SO"].CellFormat.FormatString = "0";
            this.pivot엔진.PivotGridControl.Fields["AM_SO_QTN"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot엔진.PivotGridControl.Fields["AM_SO_QTN"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this.pivot엔진.PivotGridControl.Fields["AM_PO_QTN"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot엔진.PivotGridControl.Fields["AM_PO_QTN"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this.pivot엔진.PivotGridControl.Fields["AM_SO"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot엔진.PivotGridControl.Fields["AM_SO"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this.pivot엔진.PivotGridControl.Fields["AM_PO"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot엔진.PivotGridControl.Fields["AM_PO"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this.pivot엔진.PivotGridControl.Fields["AM_STOCK"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot엔진.PivotGridControl.Fields["AM_STOCK"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this.pivot엔진.PivotGridControl.Fields["RT_SO"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot엔진.PivotGridControl.Fields["RT_SO"].CellFormat.FormatString = "#,###,###,###,##0.00%";
            this.pivot엔진.PivotGridControl.Fields["RT_PROFIT_QTN"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot엔진.PivotGridControl.Fields["RT_PROFIT_QTN"].CellFormat.FormatString = "#,###,###,###,##0.00%";
            this.pivot엔진.PivotGridControl.Fields["RT_PROFIT"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot엔진.PivotGridControl.Fields["RT_PROFIT"].CellFormat.FormatString = "#,###,###,###,##0.00%";

            this.pivot엔진.SetEnd();
            #endregion

            #region 리스트
            this._flex엔진.BeginSetting(1, 1, false);

            this._flex엔진.SetCol("NO_IMO", "IMO번호", 100);
            this._flex엔진.SetCol("NO_HULL", "호선번호", 100);
            this._flex엔진.SetCol("NM_VESSEL", "호선명", 200);
            this._flex엔진.SetCol("NM_ENGINE", "유형", 100);
            this._flex엔진.SetCol("NM_MODEL", "모델명", 100);
            this._flex엔진.SetCol("NM_MAKER", "제조사", 100);
            this._flex엔진.SetCol("NM_CLS_L", "대분류", 100);
            this._flex엔진.SetCol("NM_CLS_M", "중분류", 100);
            this._flex엔진.SetCol("NM_CLS_S", "소분류", 100);
            this._flex엔진.SetCol("CAPACITY", "용량", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flex엔진.SetCol("SERIAL", "일련번호", 100);
            this._flex엔진.SetCol("QT_QTN", "견적건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex엔진.SetCol("QT_SO", "수주건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex엔진.SetCol("AM_SO_QTN", "견적금액(매출)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex엔진.SetCol("AM_PO_QTN", "견적금액(매입)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex엔진.SetCol("AM_SO", "수주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex엔진.SetCol("AM_PO", "발주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex엔진.SetCol("AM_STOCK", "재고금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex엔진.SetCol("AM_PROFIT_QTN", "이윤(견적)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex엔진.SetCol("AM_PROFIT", "이윤(수주)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex엔진.SetCol("RT_SO", "수주율", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex엔진.SetCol("RT_PROFIT_QTN", "이윤율(견적)", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex엔진.SetCol("RT_PROFIT", "이윤율(수주)", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex엔진.SetCol("DC_RMK", "비고", 200);

            this._flex엔진.ExtendLastCol = true;

            this._flex엔진.SettingVersion = "0.0.0.1";
            this._flex엔진.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #endregion

            #region 매입처

            #region 요약
            this.pivot매입처.SetStart();

            this.pivot매입처.AddPivotField("NO_FILE", "파일번호", 100, true, PivotArea.FilterArea);
            this.pivot매입처.AddPivotField("NO_PREFIX", "영업구분", 200, true, PivotArea.FilterArea);

            this.pivot매입처.AddPivotField("NM_VESSEL", "호선명", 200, true, PivotArea.RowArea);
            this.pivot매입처.AddPivotField("DC_SHIPBUILDER", "조선소", 200, true, PivotArea.RowArea);
            this.pivot매입처.AddPivotField("DT_SHIP_DLV", "선박납기일자", 200, true, PivotArea.RowArea);
            this.pivot매입처.AddPivotField("LN_PARTNER", "매입처", 100, true, PivotArea.RowArea);

            this.pivot매입처.AddPivotField("QT_FILE", "견적건수", 100, true, PivotArea.DataArea);
            this.pivot매입처.AddPivotField("QT_ITEM", "견적종수", 100, true, PivotArea.DataArea);
            this.pivot매입처.AddPivotField("QT_QTN", "견적수량", 100, true, PivotArea.DataArea);
            this.pivot매입처.AddPivotField("AM_QTN", "견적금액", 100, true, PivotArea.DataArea);
            this.pivot매입처.AddPivotField("QT_SO_FILE", "수주건수", 100, true, PivotArea.DataArea);
            this.pivot매입처.AddPivotField("QT_SO", "수주수량", 100, true, PivotArea.DataArea);
            this.pivot매입처.AddPivotField("AM_SO", "수주금액", 100, true, PivotArea.DataArea);

            this.pivot매입처.PivotGridControl.Fields["QT_FILE"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot매입처.PivotGridControl.Fields["QT_FILE"].CellFormat.FormatString = "0";
            this.pivot매입처.PivotGridControl.Fields["QT_ITEM"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot매입처.PivotGridControl.Fields["QT_ITEM"].CellFormat.FormatString = "0";
            this.pivot매입처.PivotGridControl.Fields["QT_QTN"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot매입처.PivotGridControl.Fields["QT_QTN"].CellFormat.FormatString = "0";
            this.pivot매입처.PivotGridControl.Fields["AM_QTN"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot매입처.PivotGridControl.Fields["AM_QTN"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this.pivot매입처.PivotGridControl.Fields["QT_SO_FILE"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot매입처.PivotGridControl.Fields["QT_SO_FILE"].CellFormat.FormatString = "0";
            this.pivot매입처.PivotGridControl.Fields["QT_SO"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot매입처.PivotGridControl.Fields["QT_SO"].CellFormat.FormatString = "0";
            this.pivot매입처.PivotGridControl.Fields["AM_SO"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot매입처.PivotGridControl.Fields["AM_SO"].CellFormat.FormatString = "#,###,###,###,##0.##";

            this.pivot매입처.SetEnd();
            #endregion

            #region 리스트
            this._flex매입처.BeginSetting(1, 1, false);

            this._flex매입처.SetCol("NO_FILE", "파일번호", 100);
            this._flex매입처.SetCol("NO_PREFIX", "영업구분", 100);
            this._flex매입처.SetCol("NM_VESSEL", "호선명", 200);
            this._flex매입처.SetCol("DC_SHIPBUILDER", "조선소", 100);
            this._flex매입처.SetCol("DT_SHIP_DLV", "선박납기일자", 60, false, typeof(string), FormatTpType.YEAR_MONTH);
            this._flex매입처.SetCol("LN_PARTNER", "매입처", 100);
            this._flex매입처.SetCol("QT_FILE", "견적건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex매입처.SetCol("QT_ITEM", "견적종수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex매입처.SetCol("QT_QTN", "견적수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex매입처.SetCol("AM_QTN", "견적금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex매입처.SetCol("QT_SO_FILE", "수주건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex매입처.SetCol("QT_SO", "수주수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex매입처.SetCol("AM_SO", "수주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flex매입처.SettingVersion = "0.0.0.1";
            this._flex매입처.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #endregion

            #region 재고판매

            this._flex재고판매H.DetailGrids = new FlexGrid[] { this._flex재고판매L, this._flex재고판매D };
            this._flex재고판매L.DetailGrids = new FlexGrid[] { this._flex재고판매D };

            #region Header
            this._flex재고판매H.BeginSetting(1, 1, false);

            this._flex재고판매H.SetCol("CD_ITEM", "재고코드", 80);
            this._flex재고판매H.SetCol("NM_ITEM", "재고명", 100);
            this._flex재고판매H.SetCol("DT_QTN", "견적일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex재고판매H.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex재고판매H.SetCol("QT_QTN", "견적수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex재고판매H.SetCol("QT_STOCK", "재고예약수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex재고판매H.SetCol("RT_SO", "수주율", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex재고판매H.SetCol("QT_INV", "가용재고", 100, false, typeof(decimal), FormatTpType.QUANTITY);

            this._flex재고판매H.SettingVersion = "0.0.0.1";
            this._flex재고판매H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex재고판매H.SetExceptSumCol("RT_SO");
            #endregion

            #region Line
            this._flex재고판매L.BeginSetting(1, 1, false);

            this._flex재고판매L.SetCol("NO_IMO", "IMO번호", 100);
            this._flex재고판매L.SetCol("NM_VESSEL", "호선명", 200);
            this._flex재고판매L.SetCol("DT_QTN", "견적일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex재고판매L.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex재고판매L.SetCol("QT_QTN_FILE", "견적건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex재고판매L.SetCol("QT_STOCK_FILE", "재고예약건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex재고판매L.SetCol("QT_QTN", "견적수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex재고판매L.SetCol("QT_STOCK", "재고예약수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex재고판매L.SetCol("RT_SO_FILE", "수주율(건수)", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex재고판매L.SetCol("RT_SO", "수주율(수량)", 100, false, typeof(decimal), FormatTpType.RATE);

            this._flex재고판매L.SettingVersion = "0.0.0.1";
            this._flex재고판매L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex재고판매L.SetExceptSumCol("RT_SO_FILE", "RT_SO");
            #endregion

            #region Detail
            this._flex재고판매D.BeginSetting(1, 1, false);

            this._flex재고판매D.SetCol("NO_FILE", "파일번호", 80);
            this._flex재고판매D.SetCol("NM_EMP", "담당자", 80);
            this._flex재고판매D.SetCol("DT_QTN", "견적일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex재고판매D.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex재고판매D.SetCol("QT_QTN", "견적수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex재고판매D.SetCol("QT_STOCK", "재고예약수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex재고판매D.SetCol("UM_SO_QTN", "견적단가(매출)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex재고판매D.SetCol("UM_PO_QTN", "견적단가(매입)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex재고판매D.SetCol("UM_SO", "수주단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex재고판매D.SetCol("UM_STOCK", "재고단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex재고판매D.SetCol("AM_SO_QTN", "견적금액(매출)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex재고판매D.SetCol("AM_PO_QTN", "견적금액(매입)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex재고판매D.SetCol("AM_SO", "수주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex재고판매D.SetCol("AM_STOCK", "재고금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex재고판매D.SetCol("RT_SO", "수주율", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex재고판매D.SetCol("RT_QTN_PROFIT", "이윤율(견적)", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex재고판매D.SetCol("RT_SO_PROFIT", "이윤율(수주)", 100, false, typeof(decimal), FormatTpType.RATE);

            this._flex재고판매D.SettingVersion = "0.0.0.1";
            this._flex재고판매D.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex재고판매D.SetExceptSumCol("RT_SO", "RT_QTN_PROFIT", "RT_SO_PROFIT");
            #endregion

            #endregion

            #region 판매품목
            this._flex판매품목H.DetailGrids = new FlexGrid[] { this._flex판매품목L, this._flex판매품목D };
            this._flex판매품목L.DetailGrids = new FlexGrid[] { this._flex판매품목D };

            #region Header
            this._flex판매품목H.BeginSetting(1, 1, false);

            this._flex판매품목H.SetCol("LN_PARTNER", "매입처", 100);
            this._flex판매품목H.SetCol("QT_FILE", "견적건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex판매품목H.SetCol("QT_ITEM", "견적종수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex판매품목H.SetCol("QT_ITEM_CHOICE", "선택종수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex판매품목H.SetCol("RT_ITEM_CHOICE", "선택율", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex판매품목H.SetCol("RT_CHOICE", "선택비중", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex판매품목H.SetCol("RT_QTN", "복수견적율", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex판매품목H.SetCol("AM_QTN", "견적금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex판매품목H.SetCol("AM_QTN1", "견적금액(상대)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex판매품목H.SetCol("AM_MINUS", "차이금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flex판매품목H.SettingVersion = "0.0.0.1";
            this._flex판매품목H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex판매품목H.SetExceptSumCol("RT_ITEM_CHOICE", "RT_CHOICE", "RT_QTN");
            #endregion

            #region Line
            this._flex판매품목L.BeginSetting(1, 1, false);

            this._flex판매품목L.SetCol("CD_ITEM", "재고코드", 100);
            this._flex판매품목L.SetCol("NM_ITEM", "재고명", 100);
            this._flex판매품목L.SetCol("QT_FILE", "견적건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex판매품목L.SetCol("QT_ITEM", "견적종수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex판매품목L.SetCol("QT_ITEM_CHOICE", "선택종수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex판매품목L.SetCol("UM", "견적단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex판매품목L.SetCol("UM1", "견적단가(상대)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flex판매품목L.SettingVersion = "0.0.0.1";
            this._flex판매품목L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #region Detail
            this._flex판매품목D.BeginSetting(2, 1, false);

            this._flex판매품목D.SetCol("NO_FILE", "파일번호", 80);
            this._flex판매품목D.SetCol("DT_QTN", "견적일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex판매품목D.SetCol("LN_PARTNER", "매출처", 100);
            this._flex판매품목D.SetCol("NM_VESSEL", "호선", 100);
            this._flex판매품목D.SetCol("NO_DSP", "순번", 80);
            this._flex판매품목D.SetCol("NM_SUBJECT", "주제", 100);
            this._flex판매품목D.SetCol("CD_ITEM_PARTNER", "매출처품번", 100);
            this._flex판매품목D.SetCol("NM_ITEM_PARTNER", "매출처품목", 100);
            this._flex판매품목D.SetCol("CD_ITEM", "재고코드", 80);
            this._flex판매품목D.SetCol("QT", "수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);

            this._flex판매품목D.SetCol("UM_MINUS", "단가차이", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex판매품목D.SetCol("LT_MINUS", "납기차이", 80, false, typeof(decimal), FormatTpType.QUANTITY);

            this._flex판매품목D.SetCol("YN_CHOICE", "선택", 45, false, CheckTypeEnum.Y_N);
            this._flex판매품목D.SetCol("NM_EXCH", "통화명", 80);
            this._flex판매품목D.SetCol("RT_EXCH", "환율", 80);
            this._flex판매품목D.SetCol("UM_EX", "외화단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex판매품목D.SetCol("UM_KR", "원화단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex판매품목D.SetCol("LT", "납기", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex판매품목D.SetCol("DC_RMK_QTN", "비고", 100);

            this._flex판매품목D.SetCol("YN_CHOICE1", "선택", 45, false, CheckTypeEnum.Y_N);
            this._flex판매품목D.SetCol("NM_EXCH1", "통화명", 80);
            this._flex판매품목D.SetCol("RT_EXCH1", "환율", 80);
            this._flex판매품목D.SetCol("UM_EX1", "외화단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex판매품목D.SetCol("UM_KR1", "원화단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex판매품목D.SetCol("LT1", "납기", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex판매품목D.SetCol("DC_RMK_QTN1", "비고", 100);

            this._flex판매품목D.SetCol("NM_SUPPLIER_CHOICE", "선택매입처", 100);
            this._flex판매품목D.SetCol("NM_EXCH2", "통화명", 80);
            this._flex판매품목D.SetCol("RT_EXCH2", "환율", 80);
            this._flex판매품목D.SetCol("UM_EX2", "외화단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex판매품목D.SetCol("UM_KR2", "원화단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex판매품목D.SetCol("LT2", "납기", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex판매품목D.SetCol("DC_RMK_QTN2", "비고", 100);

            this._flex판매품목D[0, this._flex판매품목D.Cols["YN_CHOICE"].Index] = "매입처";
            this._flex판매품목D[0, this._flex판매품목D.Cols["NM_EXCH"].Index] = "매입처";
            this._flex판매품목D[0, this._flex판매품목D.Cols["RT_EXCH"].Index] = "매입처";
            this._flex판매품목D[0, this._flex판매품목D.Cols["UM_EX"].Index] = "매입처";
            this._flex판매품목D[0, this._flex판매품목D.Cols["UM_KR"].Index] = "매입처";
            this._flex판매품목D[0, this._flex판매품목D.Cols["LT"].Index] = "매입처";
            this._flex판매품목D[0, this._flex판매품목D.Cols["DC_RMK_QTN"].Index] = "매입처";

            this._flex판매품목D[0, this._flex판매품목D.Cols["YN_CHOICE1"].Index] = "상대매입처";
            this._flex판매품목D[0, this._flex판매품목D.Cols["NM_EXCH1"].Index] = "상대매입처";
            this._flex판매품목D[0, this._flex판매품목D.Cols["RT_EXCH1"].Index] = "상대매입처";
            this._flex판매품목D[0, this._flex판매품목D.Cols["UM_EX1"].Index] = "상대매입처";
            this._flex판매품목D[0, this._flex판매품목D.Cols["UM_KR1"].Index] = "상대매입처";
            this._flex판매품목D[0, this._flex판매품목D.Cols["LT1"].Index] = "상대매입처";
            this._flex판매품목D[0, this._flex판매품목D.Cols["DC_RMK_QTN1"].Index] = "상대매입처";

            this._flex판매품목D[0, this._flex판매품목D.Cols["NM_EXCH2"].Index] = "선택매입처";
            this._flex판매품목D[0, this._flex판매품목D.Cols["RT_EXCH2"].Index] = "선택매입처";
            this._flex판매품목D[0, this._flex판매품목D.Cols["UM_EX2"].Index] = "선택매입처";
            this._flex판매품목D[0, this._flex판매품목D.Cols["UM_KR2"].Index] = "선택매입처";
            this._flex판매품목D[0, this._flex판매품목D.Cols["LT2"].Index] = "선택매입처";
            this._flex판매품목D[0, this._flex판매품목D.Cols["DC_RMK_QTN2"].Index] = "선택매입처";

            this._flex판매품목D.SettingVersion = "0.0.0.1";
            this._flex판매품목D.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #endregion

            #region 업무인계
             this._flex업무인계.BeginSetting(1, 1, false);

            this._flex업무인계.SetCol("SEQ", "순번", 60);
            this._flex업무인계.SetCol("DT_HIST", "등록일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            this._flex업무인계.SetOneGridBinding(null, new IUParentControl[] { this.one기본정보, this.one추가정보 });
            this._flex업무인계.SetBindningRadioButton(new RadioButtonExt[] { this.rdo마감후견적제출가능, this.rdo마감후견적제출불가 }, new string[] { "Y", "N" });

            this._flex업무인계.SettingVersion = "0.0.0.1";
            this._flex업무인계.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion
        }

        private void InitEvent()
        {
            this.btn차트보기.Click += new EventHandler(this.btn차트보기_Click);

            this.bpc거래처.SelectionChangeCommitted += new EventHandler(this.bpc거래처_SelectedIndexChanged);

            this.btn거래처상세정보.Click += new EventHandler(this.btn거래처상세정보_Click);
            this.btn지도보기.Click += new EventHandler(this.btn지도보기_Click);
            this.pic거래처이미지.DoubleClick += new EventHandler(this.btn거래처이미지추가_Click);

            #region 거래내역
            this.pivot거래내역매출.PivotGridControl.CustomSummary += new PivotGridCustomSummaryEventHandler(this.PivotGridControl_CustomSummary);
            this.pivot거래내역매입.PivotGridControl.CustomSummary += new PivotGridCustomSummaryEventHandler(this.PivotGridControl_CustomSummary);

            this._flex거래내역매출.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
            this._flex거래내역매입.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
            #endregion

            #region 영업활동
            this.btn영업활동상세보기.Click += new EventHandler(this.btn상세보기_Click);

            this.btn영업활동추가.Click += new EventHandler(this.btn영업활동추가_Click);
            this.btn영업활동제거.Click += new EventHandler(this.btn영업활동제거_Click);
            this.btn할일연동.Click += new EventHandler(this.btn할일연동_Click);
            this.btn할일연동해제.Click += new EventHandler(this.btn할일연동해제_Click);

            this.bpc거래처담당자.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.bpc거래처담당자.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
            this.bpc사내담당자.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.bpc사내담당자.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);

            this._flex영업활동.AfterRowChange += new RangeEventHandler(this._flex영업활동_AfterRowChange);
            #endregion

            #region 미팅메모
            this.btn미팅메모상세보기.Click += new EventHandler(this.btn상세보기_Click);
            this.btn미팅메모문서보기.Click += new EventHandler(this.btn미팅메모문서보기_Click);
            this.btn출장보고서문서보기.Click += new EventHandler(this.btn출장보고서문서보기_Click);
            this._flex미팅메모.DoubleClick += new EventHandler(this._flex미팅메모_DoubleClick);
            #endregion

            #region 커미션
            this.btn커미션상세보기.Click += new EventHandler(this.btn상세보기_Click);
            this._flex커미션.DoubleClick += new EventHandler(this._flex커미션_DoubleClick);
            #endregion

            #region 메모
            this.btn메모상세보기.Click += new EventHandler(this.btn상세보기_Click);

            this.btn메모추가.Click += new EventHandler(this.btn메모추가_Click);
            this.btn메모제거.Click += new EventHandler(this.btn메모제거_Click);

            this._flex메모.AfterRowChange += new RangeEventHandler(this._flex메모_AfterRowChange);
            #endregion

            #region 담당자
            this.btn담당자편집.Click += new EventHandler(this.btn담당자편집_Click);
            this._flex담당자.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
            #endregion

            #region 호선
            this.pivot호선.PivotGridControl.CustomSummary += new PivotGridCustomSummaryEventHandler(this.PivotGridControl_CustomSummary);
            this.pivot엔진.PivotGridControl.CustomSummary += new PivotGridCustomSummaryEventHandler(this.PivotGridControl_CustomSummary);

            this._flex호선.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
            this._flex엔진.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
            #endregion

            #region 미수금
            this._flex미수금.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
            #endregion

            #region 클레임
            this._flex클레임.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
            #endregion

            #region 재고판매
            this._flex재고판매H.AfterRowChange += new RangeEventHandler(this._flex재고판매H_AfterRowChange);
            this._flex재고판매L.AfterRowChange += new RangeEventHandler(this._flex재고판매L_AfterRowChange);
            this._flex재고판매H.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
            this._flex재고판매D.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
            #endregion

            #region 판매품목
            this._flex판매품목H.AfterRowChange += new RangeEventHandler(this._flex판매품목H_AfterRowChange);
            this._flex판매품목L.AfterRowChange += new RangeEventHandler(this._flex판매품목L_AfterRowChange);
            this._flex판매품목L.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
            #endregion
        }
        #endregion

        #region 메인버튼 이벤트
        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            DataTable dt, dt1, dt2;

            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!this.BeforeSearch()) return;
                if (this.bpc거래처.SelectedValue == null || string.IsNullOrEmpty(this.bpc거래처.SelectedValue.ToString()))
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl거래처.Text);
                    return;
                }

                this._거래처코드 = this.bpc거래처.SelectedValue.ToString();
                this._거래처명 = this.bpc거래처.SelectedText.ToString();

                #region 기본정보
                this.lbl거래처명.Text = this._거래처명;

                MsgControl.ShowMsg("[자료조회중 : 기본정보]\n잠시만 기다려 주세요.");

                dt = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                     this._거래처코드,
                                                     this.dtp조회기간.StartDateToString,
                                                     this.dtp조회기간.EndDateToString });

                dt1 = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                      this._거래처코드,
                                                      Global.MainFrame.GetDateTimeToday().AddYears(-1).ToString("yyyyMMdd"),
                                                      Global.MainFrame.GetStringToday });

                this._기본정보.SetBinding(dt, this.pnl기본정보);

                if (dt.Rows.Count > 0)
                {
                    this.cur매출금액.DecimalValue = D.GetDecimal(dt.Rows[0]["AM_SO"]);
                    this.cur이윤.DecimalValue = D.GetDecimal(dt.Rows[0]["AM_PROFIT"]);
                    this.cur이윤율.DecimalValue = D.GetDecimal(dt.Rows[0]["RT_PROFIT"]);
                    this.cur수주율.DecimalValue = D.GetDecimal(dt.Rows[0]["RT_SO"]);
                    this.cur평균회수기간.DecimalValue = D.GetDecimal(dt.Rows[0]["DT_RETURN"]);

                    //연간 3억이상, 이윤율 12% 이상, 미수채권 회수기간 평균 90일 이내
                    if (D.GetDecimal(dt1.Rows[0]["AM_SO"]) >= 300000000 &&
                        D.GetDecimal(dt1.Rows[0]["RT_PROFIT"]) >= 12 &&
                        D.GetDecimal(dt1.Rows[0]["DT_RETURN"]) <= 90)
                    {
                        this.lbl등급값.Text = "우량고객";
                        this.lbl등급값.ForeColor = Color.Blue;
                    }
                    //연간 수주율 50% 이상, 이윤율 15% 이상, 미수채권 회수기간 평균 90일 이내
                    else if (D.GetDecimal(dt1.Rows[0]["RT_SO"]) >= 50 &&
                             D.GetDecimal(dt1.Rows[0]["RT_PROFIT"]) >= 15 &&
                             D.GetDecimal(dt1.Rows[0]["DT_RETURN"]) <= 90)
                    {
                        this.lbl등급값.Text = "우수고객";
                        this.lbl등급값.ForeColor = Color.Green;
                    }
                    else
                    {
                        this.lbl등급값.Text = "일반고객";
                        this.lbl등급값.ForeColor = Color.Black;
                    }

                    this._기본정보.SetDataTable(dt);
                    this._기본정보.AcceptChanges();

                    FileInfoCs fileInfoCs = this._fileUploader[D.GetString(this._기본정보.CurrentRow["DC_PHOTO"]), "Upload/P_CZ_SA_CRM_PARTNER/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + this._거래처코드 + "/"];

                    if (fileInfoCs != null && fileInfoCs.Image != null)
                        this.pic거래처이미지.Image = fileInfoCs.Image;
                    else
                        this.pic거래처이미지.Image = Properties.Resources.사진;
                }
                else
                {
                    this.cur매출금액.DecimalValue = 0;
                    this.cur이윤율.DecimalValue = 0;
                    this.cur수주율.DecimalValue = 0;
                    this.cur평균회수기간.DecimalValue = 0;
                    this.lbl등급값.Text = string.Empty;
                    this.lbl등급값.ForeColor = Color.Black;

                    this._기본정보.ClearAndNewRow();
                    this._기본정보.AcceptChanges();
                }
                #endregion

                if (this.lbl거래처명.Text != this._거래처명)
                {
                    #region 초기화
                    this._flex영업활동.ClearData();
                    this._flex미팅메모.ClearData();
                    this._flex커미션.ClearData();
                    this._flex담당자.ClearData();
                    this._flex호선.ClearData();
                    this._flex엔진.ClearData();
                    this._flex거래내역매출.ClearData();
                    this._flex거래내역매입.ClearData();
                    this._flex재고판매H.ClearData();
                    this._flex판매품목H.ClearData();
                    this._flex미수금.ClearData();
                    this._flex클레임.ClearData();
                    this._flex메모.ClearData();

                    this.pivot담당자.DataSource = null;
                    this.pivot호선.DataSource = null;
                    this.pivot엔진.DataSource = null;
                    this.pivot거래내역매출.DataSource = null;
                    this.pivot거래내역매입.DataSource = null;
                    this.pivot미수금.DataSource = null;
                    this.pivot클레임.DataSource = null;
                    #endregion
                }
                
                if (this.tabControl1.SelectedTab == this.tpg이력)
                {
                    #region 이력
                    MsgControl.ShowMsg("[자료조회중 : 영업활동]\n잠시만 기다려 주세요.");

                    dt = this._biz.Search영업활동(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                 this._거래처코드 });

                    MsgControl.ShowMsg("[자료조회중 : 미팅메모]\n잠시만 기다려 주세요.");

                    dt1 = this._biz.Search미팅메모(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                  this._거래처코드 });

                    MsgControl.ShowMsg("[자료조회중 : 커미션]\n잠시만 기다려 주세요.");

                    dt2 = this._biz.Search커미션(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                this._거래처코드 });

                    this._flex영업활동.Binding = dt;
                    this._flex미팅메모.Binding = dt1;
                    this._flex커미션.Binding = dt2;
                    #endregion
                }

                if (this.tabControl1.SelectedTab == this.tpg담당자)
                {
                    #region 담당자
                    MsgControl.ShowMsg("[자료조회중 : 담당자]\n잠시만 기다려 주세요.");

                    dt = this._biz.Search담당자(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                               this._거래처코드 });

                    dt.TableName = this.PageID;
                    this.pivot담당자.DataSource = dt;
                    this._flex담당자.Binding = dt;
                    #endregion
                }
                
                if (this.tabControl1.SelectedTab == this.tpg호선)
                {
                    #region 호선
                    MsgControl.ShowMsg("[자료조회중 : 호선]\n잠시만 기다려 주세요.");

                    dt = this._biz.Search호선(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                             this._거래처코드,
                                                             this.cbo일자유형.SelectedValue.ToString(),
                                                             this.dtp호선.StartDateToString,
                                                             this.dtp호선.EndDateToString });

                    MsgControl.ShowMsg("[자료조회중 : 엔진]\n잠시만 기다려 주세요.");

                    dt1 = this._biz.Search엔진(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                              this._거래처코드,
                                                              this.dtp호선.StartDateToString,
                                                              this.dtp호선.EndDateToString });

                    MsgControl.ShowMsg("[자료조회중 : 호선-매입처]\n잠시만 기다려 주세요.");

                    dt2 = this._biz.Search호선_매입처(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                    this._거래처코드,
                                                                    this.dtp호선.StartDateToString,
                                                                    this.dtp호선.EndDateToString });

                    dt.TableName = this.PageID;
                    dt1.TableName = this.PageID;
                    dt2.TableName = this.PageID;
                    this.pivot호선.DataSource = dt;
                    this.pivot엔진.DataSource = dt1;
                    this.pivot매입처.DataSource = dt2;
                    this._flex호선.Binding = dt;
                    this._flex엔진.Binding = dt1;
                    this._flex매입처.Binding = dt2;
                    #endregion
                }
                
                if (this.tabControl1.SelectedTab == this.tpg거래내역매출)
                {
                    #region 거래내역매출

                    dt = this._biz.Search거래내역1(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                  this._거래처코드,
                                                                  this.dtp거래내역매출.StartDateToString,
                                                                  this.dtp거래내역매출.EndDateToString });

                    dt.TableName = this.PageID;
                    this.pivot거래내역매출.DataSource = dt;
                    this._flex거래내역매출.Binding = dt;
                    #endregion
                }

                if (this.tabControl1.SelectedTab == this.tpg거래내역매입)
                {
                    #region 거래내역매입
                    MsgControl.ShowMsg("[자료조회중 : 거래내역-매입]\n잠시만 기다려 주세요.");

                    dt = this._biz.Search거래내역2(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                  this._거래처코드,
                                                                  this.dtp거래내역매입.StartDateToString,
                                                                  this.dtp거래내역매입.EndDateToString });

                    dt.TableName = this.PageID;
                    this.pivot거래내역매입.DataSource = dt;
                    this._flex거래내역매입.Binding = dt;
                    #endregion
                }

                if (this.tabControl1.SelectedTab == this.tpg재고판매)
                {
                    #region 재고판매
                    MsgControl.ShowMsg("[자료조회중 : 재고판매]\n잠시만 기다려 주세요.");

                    dt = this._biz.Search재고판매H(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                  this._거래처코드,
                                                                  this.dtp재고판매.StartDateToString,
                                                                  this.dtp재고판매.EndDateToString });

                    this._flex재고판매H.Binding = dt;
                    #endregion
                }

                if (this.tabControl1.SelectedTab == this.tpg판매품목)
                {
                    #region 판매품목
                    MsgControl.ShowMsg("[자료조회중 : 판매품목]\n잠시만 기다려 주세요.");

                    dt = this._biz.Search판매품목H(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                  this._거래처코드,
                                                                  this.dtp판매품목.StartDateToString,
                                                                  this.dtp판매품목.EndDateToString });

                    this._flex판매품목H.Binding = dt;
                    #endregion
                }
                    
                if (this.tabControl1.SelectedTab == this.tpg미수금)
                {
                    #region 미수금
                    MsgControl.ShowMsg("[자료조회중 : 미수금]\n잠시만 기다려 주세요.");

                    dt = this._biz.Search미수금(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                               this._거래처코드,
                                                               this.dtp미수금.StartDateToString,
                                                               this.dtp미수금.EndDateToString,
                                                               (this.chk미수금0표시.Checked ? "Y" : "N") });

                    dt.TableName = this.PageID;
                    this.pivot미수금.DataSource = dt;
                    this._flex미수금.Binding = dt;
                    #endregion
                }

                if (this.tabControl1.SelectedTab == this.tpg클레임)
                {
                    #region 클레임
                    MsgControl.ShowMsg("[자료조회중 : 클레임]\n잠시만 기다려 주세요.");

                    dt = this._biz.Search클레임(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                               this._거래처코드,
                                                               this.dtp클레임.StartDateToString,
                                                               this.dtp클레임.EndDateToString });

                    dt.TableName = this.PageID;
                    this.pivot클레임.DataSource = dt;
                    this._flex클레임.Binding = dt;
                    #endregion
                }

                if (this.tabControl1.SelectedTab == this.tpg업무인계)
                {
                    #region 업무인계
                    MsgControl.ShowMsg("[자료조회중 : 업무인계]\n잠시만 기다려 주세요.");

                    dt = this._biz.Search업무인계(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                 this._거래처코드 });

                    this._flex업무인계.Binding = dt;
                    #endregion
                }

                if (this.tabControl1.SelectedTab == this.tpg메모)
                {
                    #region 메모
                    MsgControl.ShowMsg("[자료조회중 : 메모]\n잠시만 기다려 주세요.");

                    dt = this._biz.Search메모(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                             this._거래처코드 });

                    this._flex메모.Binding = dt;
                    #endregion
                }
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

        protected override bool SaveData()
        {
            if (!base.SaveData() || !base.Verify()) return false;

            if (this._flex영업활동.IsDataChanged == false &&
                this._flex메모.IsDataChanged == false) return false;

            if (!this._biz.Save(this._flex영업활동.GetChanges(),
                                this._flex메모.GetChanges())) return false;

            this._flex영업활동.AcceptChanges();
            this._flex메모.AcceptChanges();

            return true;
        }
        #endregion

        #region 컨트롤 이벤트
        private void bpc거래처_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.OnToolBarSearchButtonClicked(null, null);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn거래처상세정보_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this._거래처코드))
                    return;

                if (Global.MainFrame.IsExistPage("P_CZ_MA_PARTNER", false))
                    Global.MainFrame.UnLoadPage("P_CZ_MA_PARTNER", false);

                Global.MainFrame.LoadPageFrom("P_CZ_MA_PARTNER", "거래처정보관리(딘텍)", this.Grant, new object[] { this._거래처코드 });
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn지도보기_Click(object sender, EventArgs e)
        {
            string strURL;

            try
            {
                strURL = "http://maps.google.co.kr/maps?q=" + HttpUtility.UrlEncode(this.txt본사주소.Text.Trim(), Encoding.UTF8) + " " + HttpUtility.UrlEncode(this.txt본사주소상세.Text.Trim(), Encoding.UTF8);
                Process.Start("msedge.exe", strURL);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void Control_QueryBefore(object sender, BpQueryArgs e)
        {
            string name = ((Control)sender).Name;

            if ((name == this.bpc거래처담당자.Name || name == this.bpc사내담당자.Name) &&
                D.GetString(this._flex영업활동["YN_TODO"]) == "Y")
            {
                e.QueryCancel = true;
            }

            if (name == this.bpc거래처담당자.Name)
            {
                e.HelpParam.P14_CD_PARTNER = this._거래처코드;
                e.HelpParam.P34_CD_MNG = this._거래처명;
            }
        }

        private void Control_QueryAfter(object sender, BpQueryArgs e)
        {
            string name;

            try
            {
                name = ((Control)sender).Name;

                if (name == this.bpc거래처담당자.Name)
                {
                    this._flex영업활동["NO_PIC"] = this.bpc거래처담당자.QueryWhereIn_Pipe;
                    this._flex영업활동["NM_PIC"] = this.bpc거래처담당자.QueryWhereIn_PipeDisplayMember;
                }
                else if (name == this.bpc사내담당자.Name)
                {
                    this._flex영업활동["NO_EMP"] = this.bpc사내담당자.QueryWhereIn_Pipe;
                    this._flex영업활동["NM_EMP"] = this.bpc사내담당자.QueryWhereIn_PipeDisplayMember;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn거래처이미지추가_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this._거래처코드)) return;

                FileInfoCs fileInfoCs = this._fileUploader.Add(this._거래처코드, "Upload/P_CZ_SA_CRM_PARTNER/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + this._거래처코드 + "/");

                if (fileInfoCs != null)
                {
                    this._기본정보.CurrentRow["DC_PHOTO"] = fileInfoCs.FileName;
                    this.pic거래처이미지.Image = fileInfoCs.Image;

                    this.거래처이미지저장();
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn거래처이미지삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this._거래처코드)) return;

                if (!string.IsNullOrEmpty(D.GetString(this._기본정보.CurrentRow["DC_PHOTO"])))
                {
                    this._fileUploader.Remove(D.GetString(this._기본정보.CurrentRow["DC_PHOTO"]));
                    this.pic거래처이미지.Image = Properties.Resources.사진;
                    this._기본정보.CurrentRow["DC_PHOTO"] = string.Empty;

                    this.거래처이미지저장();
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn차트보기_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this._거래처코드))
                    return;

                if (Global.MainFrame.IsExistPage("P_CZ_SA_CRM_CHART", false))
                    Global.MainFrame.UnLoadPage("P_CZ_SA_CRM_CHART", false);

                Global.MainFrame.LoadPageFrom("P_CZ_SA_CRM_CHART", "차트(CRM)", new object[] { this.bpc거래처.SelectedValue, this.bpc거래처.SelectedText, null, null });
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn상세보기_Click(object sender, EventArgs e)
        {
            string name;

            try
            {
                name = ((Control)sender).Name;
                
                if (name == this.btn영업활동상세보기.Name)
                {
                    if (this.split영업활동.Panel2Collapsed)
                    {
                        this.split영업활동.SplitterDistance = 618;
                        this.split영업활동.Panel2Collapsed = false;
                    }
                    else
                        this.split영업활동.Panel2Collapsed = true;
                }
                else if (name == this.btn미팅메모상세보기.Name)
                {
                    if (this.split미팅메모.Panel2Collapsed)
                    {
                        this.split미팅메모.SplitterDistance = 618;
                        this.split미팅메모.Panel2Collapsed = false;
                    }
                    else
                        this.split미팅메모.Panel2Collapsed = true;
                }
                else if (name == this.btn커미션상세보기.Name)
                {
                    if (this.split커미션.Panel2Collapsed)
                    {
                        this.split커미션.SplitterDistance = 618;
                        this.split커미션.Panel2Collapsed = false;
                    }
                    else
                        this.split커미션.Panel2Collapsed = true;
                }
                else if (name == this.btn메모상세보기.Name)
                {
                    if (this.split메모.Panel2Collapsed)
                    {
                        this.split메모.SplitterDistance = 618;
                        this.split메모.Panel2Collapsed = false;
                    }
                    else
                        this.split메모.Panel2Collapsed = true;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #region 영업활동
        private void btn영업활동추가_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this._거래처코드)) return;

                this._flex영업활동.Rows.Add();
                this._flex영업활동.Row = this._flex영업활동.Rows.Count - 1;

                this._flex영업활동["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                this._flex영업활동["CD_PARTNER"] = this._거래처코드;
                this._flex영업활동["NO_INDEX"] = this.Get영업활동Seq(this._거래처코드);

                this._flex영업활동["YN_TODO"] = "N";

                this._flex영업활동["DT_START"] = Global.MainFrame.GetStringToday;
                this.dtp영업활동시작일자.Text = D.GetString(this._flex영업활동["DT_START"]);
                this._flex영업활동["DT_END"] = Global.MainFrame.GetStringToday;
                this.dtp영업활동종료일자.Text = D.GetString(this._flex영업활동["DT_END"]);

                this._flex영업활동["ID_INSERT"] = Global.MainFrame.LoginInfo.UserID;

                this._flex영업활동.AddFinished();
                this._flex영업활동.Col = this._flex영업활동.Cols.Fixed;
                this._flex영업활동.Focus();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn영업활동제거_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex영업활동.HasNormalRow) return;
                if (D.GetString(this._flex영업활동["ID_INSERT"]) != Global.MainFrame.LoginInfo.UserID)
                {
                    this.ShowMessage("등록자만 삭제 가능 합니다. (등록자 : " + D.GetString(this._flex영업활동["ID_INSERT"]) + ")");
                    return;
                }

                if (D.GetString(this._flex영업활동["YN_TODO"]) == "Y")
                {
                    this.ShowMessage("할일 연동해제 후 삭제 가능 합니다.");
                    return;
                }

                this._flex영업활동.GetDataRow(this._flex영업활동.Row).Delete();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn할일연동_Click(object sender, EventArgs e)
        {
            DBMgr dbMgr;
            string query, query1, query2, query3;
            string todoId, userId;

            try
            {
                if (this.ShowMessage("선택된 담당자의 할일 목록에 추가 하시겠습니까?", "QY2") != DialogResult.Yes)
                    return;

                this.btn할일연동.Enabled = false;

                dbMgr = new DBMgr(DBConn.GroupWare);

                query = @"SELECT (MAX(TODO_ID) + 1) AS TODO_ID
                          FROM BX.TSOG_TODOLIST";

                query1 = @"SELECT USER_ID
                           FROM BX.TCMG_USER WITH(NOLOCK)
                           WHERE LOGON_CD = '{0}'";

                query2 = @"INSERT INTO BX.TSOG_TODOLIST
                           (
                             TODO_ID,
                             CATEGORY_ID,
                             TITLE,
                             CONTENTS,
                             PROC_CONTENTS,
                             ALERT_TIME,
                             MAIL_YN,
                             PAPER_YN,
                             ALERT_YN,
                             SMS_YN,
                             PROC_RATE,
                             END_YN,
                             DEL_YN,
                             CREATED_BY,
                             CREATED_DT,
                             START_DT,
                             END_DT,
                             ORDERBY
                           )
                           VALUES
                           (
                           	 '{0}', -- TODO_ID
                           	 0, -- CATEGORY_ID
                           	 '{1}', -- TITLE
                           	 '{2}', -- CONTENTS
                           	 NULL, -- PROC_CONTENTS
                           	 NULL, -- ALERT_TIME
                           	 '0', -- MAIL_YN
                           	 '0', -- PAPER_YN
                           	 '0', -- ALERT_YN
                           	 '0', -- SMS_YN
                           	 '0', -- PROC_RATE
                           	 'N', -- END_YN,
                           	 '0', -- DEL_YN
                           	 '{3}', -- CREATED_BY
                           	 GETDATE(), -- CREATED_DT
                           	 '{4}', -- START_DT
                           	 '{5}', -- END_DT
                           	 '0' -- ORDERBY
                           )";

                query3 = @"UPDATE CZ_CRM_PARTNER_ACTIVITY
                           SET YN_TODO = 'Y'
                           WHERE CD_COMPANY = '{0}'
                           AND CD_PARTNER = '{1}'
                           AND NO_INDEX = '{2}'";

                foreach (string 사번 in this.bpc사내담당자.CodeValues)
                {
                    dbMgr.Query = query;
                    todoId = D.GetString(dbMgr.GetDataTable().Rows[0]["TODO_ID"]);

                    dbMgr.Query = string.Format(query1, 사번);
                    userId = D.GetString(dbMgr.GetDataTable().Rows[0]["USER_ID"]);

                    dbMgr.Query = string.Format(query2, todoId,
                                                        D.GetString(this._flex영업활동["NM_TITLE"]),
                                                        D.GetString(this._flex영업활동["DC_ACTIVITY"]),
                                                        userId,
                                                        D.GetString(this._flex영업활동["DT_START"]),
                                                        D.GetString(this._flex영업활동["DT_END"]));
                    dbMgr.ExecuteNonQuery();

                    DBHelper.ExecuteNonQuery("SP_CZ_SA_CRM_PARTNER_TODO_I", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                           D.GetString(this._flex영업활동["CD_PARTNER"]),
                                                                                           D.GetString(this._flex영업활동["NO_INDEX"]),
                                                                                           사번,
                                                                                           todoId,
                                                                                           Global.MainFrame.LoginInfo.UserID });
                }

                this._flex영업활동["YN_TODO"] = "Y";

                DBHelper.ExecuteScalar(string.Format(query3, Global.MainFrame.LoginInfo.CompanyCode,
                                                             D.GetString(this._flex영업활동["CD_PARTNER"]),
                                                             D.GetString(this._flex영업활동["NO_INDEX"])));

                this._flex영업활동_AfterRowChange(null, null);

                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn할일연동.Text);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn할일연동해제_Click(object sender, EventArgs e)
        {
            DBMgr dbMgr;
            DataTable dt;
            string query, query1, query2, userId;

            try
            {
                if (this.ShowMessage("선택된 담당자의 할일 목록에서 제거 하시겠습니까?", "QY2") != DialogResult.Yes)
                    return;

                this.btn할일연동해제.Enabled = false;

                dbMgr = new DBMgr(DBConn.GroupWare);

                query = @"SELECT NO_TODO 
                          FROM CZ_CRM_PARTNER_TODO WITH(NOLOCK)
                          WHERE CD_COMPANY = '{0}'
                          AND CD_PARTNER = '{1}'
                          AND NO_INDEX = '{2}'";

                dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
                                                                D.GetString(this._flex영업활동["CD_PARTNER"]),
                                                                D.GetString(this._flex영업활동["NO_INDEX"])));

                query = @"SELECT USER_ID
                           FROM BX.TCMG_USER WITH(NOLOCK)
                           WHERE LOGON_CD = '{0}'";
                dbMgr.Query = string.Format(query, Global.MainFrame.LoginInfo.EmployeeNo);
                userId = D.GetString(dbMgr.GetDataTable().Rows[0]["USER_ID"]);

                query = @"UPDATE BX.TSOG_TODOLIST
                          SET DEL_YN = '1',
                          	  MODIFY_BY = '{1}',
                          	  MODIFY_DT = GETDATE() 
                          WHERE TODO_ID = '{0}'";

                query1 = @"DELETE 
                           FROM CZ_CRM_PARTNER_TODO
                           WHERE CD_COMPANY = '{0}'
                           AND CD_PARTNER = '{1}'
                           AND NO_INDEX = '{2}'";

                query2 = @"UPDATE CZ_CRM_PARTNER_ACTIVITY
                           SET YN_TODO = 'N'
                           WHERE CD_COMPANY = '{0}'
                           AND CD_PARTNER = '{1}'
                           AND NO_INDEX = '{2}'";

                foreach (DataRow dr in dt.Rows)
                {
                    dbMgr.Query = string.Format(query, D.GetString(dr["NO_TODO"]),
                                                       userId);
                    dbMgr.ExecuteNonQuery();
                }

                DBHelper.ExecuteScalar(string.Format(query1, Global.MainFrame.LoginInfo.CompanyCode,
                                                             D.GetString(this._flex영업활동["CD_PARTNER"]),
                                                             D.GetString(this._flex영업활동["NO_INDEX"])));

                this._flex영업활동["YN_TODO"] = "N";

                DBHelper.ExecuteScalar(string.Format(query2, Global.MainFrame.LoginInfo.CompanyCode,
                                                             D.GetString(this._flex영업활동["CD_PARTNER"]),
                                                             D.GetString(this._flex영업활동["NO_INDEX"])));

                this._flex영업활동_AfterRowChange(null, null);

                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn할일연동해제.Text);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region 미팅메모
        private void btn미팅메모문서보기_Click(object sender, EventArgs e)
        {
            string strURL;

            try
            {
                if (!this._flex미팅메모.HasNormalRow) return;
                if (this._flex미팅메모["ID_WRITE"].ToString() != Global.MainFrame.LoginInfo.UserID && this._flex미팅메모["ST_STAT"].ToString() != "1")
                {
                    this.ShowMessage("문서작성자가 본인이거나 결재상태가 승인 상태인 문서만 확인 가능 합니다.");
                    return;
                }

                strURL = "http://gw.dintec.co.kr" + "/kor_webroot/src/cm/tims/index.aspx"
                                                  + "?cd_company=" + GroupWare.GetERP_CD_COMPANY()
                                                  + "&cd_pc=" + GroupWare.GetERP_CD_PC()
                                                  + "&no_docu=" + HttpUtility.UrlEncode(this._flex미팅메모["NO_DOCU"].ToString(), Encoding.UTF8)
                                                  + "&login_id=" + this._flex미팅메모["ID_WRITE"].ToString();

                P_CZ_MA_HTML_VIEWER dialog = new P_CZ_MA_HTML_VIEWER(strURL);
                dialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn출장보고서문서보기_Click(object sender, EventArgs e)
        {
            string strURL;

            try
            {
                if (!this._flex미팅메모.HasNormalRow) return;
                if (this._flex미팅메모["ID_WRITE1"].ToString() != Global.MainFrame.LoginInfo.UserID && this._flex미팅메모["ST_STAT1"].ToString() != "1")
                {
                    this.ShowMessage("문서작성자가 본인이거나 결재상태가 승인 상태인 문서만 확인 가능 합니다.");
                    return;
                }

                strURL = "http://gw.dintec.co.kr" + "/kor_webroot/src/cm/tims/index.aspx"
                                                  + "?cd_company=" + GroupWare.GetERP_CD_COMPANY()
                                                  + "&cd_pc=" + GroupWare.GetERP_CD_PC()
                                                  + "&no_docu=" + HttpUtility.UrlEncode(this._flex미팅메모["NO_DOCU1"].ToString(), Encoding.UTF8)
                                                  + "&login_id=" + this._flex미팅메모["ID_WRITE1"].ToString();

                P_CZ_MA_HTML_VIEWER dialog = new P_CZ_MA_HTML_VIEWER(strURL);
                dialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex미팅메모_DoubleClick(object sender, EventArgs e)
        {
            string pageId, pageName;

            try
            {
                if (this._flex미팅메모.HasNormalRow == false) return;
                if (this._flex미팅메모.MouseRow < this._flex미팅메모.Rows.Fixed) return;

                pageId = "P_CZ_SA_MEETING_MEMO_MNG";
                pageName = "미팅메모관리";

                if (this.IsExistPage(pageId, false))
                    this.UnLoadPage(pageId, false);

                this.LoadPageFrom(pageId, pageName, this.Grant, new object[] { D.GetString(this._flex미팅메모["NO_MEETING"]) });
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region 커미션
        private void _flex커미션_DoubleClick(object sender, EventArgs e)
        {
            string pageId, pageName;

            try
            {
                if (this._flex커미션.HasNormalRow == false) return;
                if (this._flex커미션.MouseRow < this._flex커미션.Rows.Fixed) return;

                pageId = "P_CZ_SA_COMMISSION_MNG";
                pageName = "커미션관리";

                if (this.IsExistPage(pageId, false))
                    this.UnLoadPage(pageId, false);

                this.LoadPageFrom(pageId, pageName, this.Grant, new object[] { D.GetString(this._flex커미션["NO_COMMISSION"]) });
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region 메모
        private void btn메모추가_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this._거래처코드)) return;

                this._flex메모.Rows.Add();
                this._flex메모.Row = this._flex메모.Rows.Count - 1;

                this._flex메모["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                this._flex메모["CD_PARTNER"] = this._거래처코드;
                this._flex메모["NO_INDEX"] = this.Get메모Seq(this._거래처코드);
                this._flex메모["ID_INSERT"] = Global.MainFrame.LoginInfo.UserID;

                this._flex메모.AddFinished();
                this._flex메모.Col = this._flex메모.Cols.Fixed;
                this._flex메모.Focus();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn메모제거_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex메모.HasNormalRow) return;
                if (D.GetString(this._flex메모["ID_INSERT"]) != Global.MainFrame.LoginInfo.UserID)
                {
                    this.ShowMessage("등록자만 삭제 가능 합니다. (등록자 : " + D.GetString(this._flex메모["ID_INSERT"]) + ")");
                    return;
                }

                this._flex메모.GetDataRow(this._flex메모.Row).Delete();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region 담당자
        private void btn담당자편집_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this._거래처코드))
                    return;

                P_CZ_FI_PARTNERPTR_SUB dialog = new P_CZ_FI_PARTNERPTR_SUB(this._거래처코드);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this._flex담당자.Binding = this._biz.Search담당자(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                     this._거래처코드 });
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #endregion

        #region 그리드 이벤트
        private void PivotGridControl_CustomSummary(object sender, PivotGridCustomSummaryEventArgs e)
        {
            PivotDrillDownDataSource drillDownDataSource;
            decimal num1, num2;

            try
            {
                drillDownDataSource = e.CreateDrillDownDataSource();
                num1 = 0;
                num2 = 0;

                switch (e.DataField.FieldName)
                {
                    case "RT_CHOICE":
                        for (int index = 0; index < drillDownDataSource.RowCount; ++index)
                        {
                            num1 += D.GetDecimal(drillDownDataSource[index]["QT_INQ"]);
                            num2 += D.GetDecimal(drillDownDataSource[index]["QT_QTN"]);
                        }
                        e.CustomValue = (num1 == 0 ? 0 : (num2 / num1));
                        break;
                    case "RT_PO":
                        for (int index = 0; index < drillDownDataSource.RowCount; ++index)
                        {
                            num1 += D.GetDecimal(drillDownDataSource[index]["QT_QTN"]);
                            num2 += D.GetDecimal(drillDownDataSource[index]["QT_PO"]);
                        }
                        e.CustomValue = (num1 == 0 ? 0 : (num2 / num1));
                        break;
                    case "RT_SO":
                        for (int index = 0; index < drillDownDataSource.RowCount; ++index)
                        {
                            num1 += D.GetDecimal(drillDownDataSource[index]["QT_QTN"]);
                            num2 += D.GetDecimal(drillDownDataSource[index]["QT_SO"]);
                        }
                        e.CustomValue = (num1 == 0 ? 0 : (num2 / num1));
                        break;
                    case "RT_PROFIT_QTN":
                        for (int index = 0; index < drillDownDataSource.RowCount; ++index)
                        {
                            num1 += D.GetDecimal(drillDownDataSource[index]["AM_SO_QTN"]);
                            num2 += D.GetDecimal(drillDownDataSource[index]["AM_PROFIT_QTN"]);
                        }
                        e.CustomValue = (num1 == 0 ? 0 : (num2 / num1));
                        break;
                    case "RT_PROFIT":
                        for (int index = 0; index < drillDownDataSource.RowCount; ++index)
                        {
                            num1 += D.GetDecimal(drillDownDataSource[index]["AM_SO"]);
                            num2 += D.GetDecimal(drillDownDataSource[index]["AM_PROFIT"]);
                        }
                        e.CustomValue = (num1 == 0 ? 0 : (num2 / num1));
                        break;
                    case "RT_PROFIT_PO":
                        for (int index = 0; index < drillDownDataSource.RowCount; ++index)
                        {
                            num1 += D.GetDecimal(drillDownDataSource[index]["AM_SO_PO"]);
                            num2 += D.GetDecimal(drillDownDataSource[index]["AM_PROFIT_PO"]);
                        }
                        e.CustomValue = (num1 == 0 ? 0 : (num2 / num1));
                        break;
                }
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

                if (grid.Cols["NO_FILE"] != null && grid.ColSel == grid.Cols["NO_FILE"].Index)
                {
                    pageId = "P_CZ_SA_QTN_REG";
                    pageName = Global.MainFrame.DD("견적등록");
                    obj = new object[] { D.GetString(grid["NO_FILE"]) };

                    if (Global.MainFrame.IsExistPage(pageId, false))
                        Global.MainFrame.UnLoadPage(pageId, false);

                    Global.MainFrame.LoadPageFrom(pageId, pageName, this.Grant, obj);
                }
                else if (grid.Cols["NO_IMO"] != null && grid.ColSel == grid.Cols["NO_IMO"].Index)
                {
                    pageId = "P_CZ_MA_HULL";
                    pageName = Global.MainFrame.DD("호선등록");
                    obj = new object[] { D.GetString(grid["NO_IMO"]) };

                    if (Global.MainFrame.IsExistPage(pageId, false))
                        Global.MainFrame.UnLoadPage(pageId, false);

                    Global.MainFrame.LoadPageFrom(pageId, pageName, this.Grant, obj);
                }
                else if (grid.Cols["NM_EMP"] != null && grid.ColSel == grid.Cols["NM_EMP"].Index)
                {
                    pageId = "P_CZ_SA_CRM_EMP";
                    pageName = Global.MainFrame.DD("사원(CRM)");
                    obj = new object[] { D.GetString(grid["NO_EMP"]), D.GetString(grid["NM_EMP"]) };

                    if (Global.MainFrame.IsExistPage(pageId, false))
                        Global.MainFrame.UnLoadPage(pageId, false);

                    Global.MainFrame.LoadPageFrom(pageId, pageName, this.Grant, obj);
                }
                else if (grid.Cols["NM_PTR"] != null && grid.ColSel == grid.Cols["NM_PTR"].Index)
                {
                    pageId = "P_CZ_SA_CRM_PARTNER_PIC";
                    pageName = Global.MainFrame.DD("거래처담당자(CRM)");
                    obj = new object[] { this._거래처코드, this._거래처명, D.GetString(grid["NM_PTR"]) };

                    if (Global.MainFrame.IsExistPage(pageId, false))
                        Global.MainFrame.UnLoadPage(pageId, false);

                    Global.MainFrame.LoadPageFrom(pageId, pageName, this.Grant, obj);
                }
                else if (grid.Cols["NO_IV"] != null && grid.ColSel == grid.Cols["NO_IV"].Index)
                {
                    pageId = "P_CZ_SA_IVMNG";
                    pageName = Global.MainFrame.DD("매출관리(딘텍)");
                    obj = new object[] { D.GetString(grid["NO_IV"]) };

                    if (Global.MainFrame.IsExistPage(pageId, false))
                        Global.MainFrame.UnLoadPage(pageId, false);

                    Global.MainFrame.LoadPageFrom(pageId, pageName, this.Grant, obj);
                }
                else if (grid.Cols["NO_CLAIM"] != null && grid.ColSel == grid.Cols["NO_CLAIM"].Index)
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

        private void _flex영업활동_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (D.GetString(this._flex영업활동["YN_TODO"]) == "Y")
                {
                    this.btn할일연동.Enabled = false;
                    this.btn할일연동해제.Enabled = true;

                    this.txt영업활동제목.ReadOnly = true;
                    this.dtp영업활동시작일자.Enabled = false;
                    this.dtp영업활동종료일자.Enabled = false;
                    this.txt영업활동내용.ReadOnly = true;
                }
                else
                {
                    this.btn할일연동.Enabled = true;
                    this.btn할일연동해제.Enabled = false;

                    this.txt영업활동제목.ReadOnly = false;
                    this.dtp영업활동시작일자.Enabled = true;
                    this.dtp영업활동종료일자.Enabled = true;
                    this.txt영업활동내용.ReadOnly = false;
                }

                this.bpc거래처담당자.Clear();
                this.bpc사내담당자.Clear();

                this.bpc거래처담당자.AddItem2(D.GetString(this._flex영업활동["NO_PIC"]), D.GetString(this._flex영업활동["NM_PIC"]));
                this.bpc사내담당자.AddItem2(D.GetString(this._flex영업활동["NO_EMP"]), D.GetString(this._flex영업활동["NM_EMP"]));
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex메모_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (D.GetString(this._flex메모["ID_INSERT"]) == Global.MainFrame.LoginInfo.UserID)
                    this.txt메모내용.ReadOnly = false;
                else
                    this.txt메모내용.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex재고판매H_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt;
            string key, filter;

            try
            {
                dt = null;
                key = D.GetString(this._flex재고판매H["CD_ITEM"]);
                filter = "CD_ITEM = '" + key + "'";

                if (this._flex재고판매H.DetailQueryNeed)
                {
                    dt = this._biz.Search재고판매L(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                  this._거래처코드,
                                                                  key,
                                                                  this.dtp재고판매.StartDateToString,
                                                                  this.dtp재고판매.EndDateToString });
                }

                this._flex재고판매L.Redraw = false;
                this._flex재고판매L.BindingAdd(dt, filter);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                this._flex재고판매L.Redraw = true;
            }
        }

        private void _flex재고판매L_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt;
            string key, key1, filter;

            try
            {
                dt = null;
                key = D.GetString(this._flex재고판매L["CD_ITEM"]);
                key1 = D.GetString(this._flex재고판매L["NO_IMO"]);
                filter = "CD_ITEM = '" + key + "' AND NO_IMO = '" + key1 + "'";

                if (this._flex재고판매L.DetailQueryNeed)
                {
                    dt = this._biz.Search재고판매D(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                  this._거래처코드,
                                                                  key,
                                                                  key1,
                                                                  this.dtp재고판매.StartDateToString,
                                                                  this.dtp재고판매.EndDateToString });
                }

                this._flex재고판매D.Redraw = false;
                this._flex재고판매D.BindingAdd(dt, filter);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                this._flex재고판매D.Redraw = true;
            }
        }

        private void _flex판매품목H_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt;
            string key, filter;

            try
            {
                dt = null;
                key = D.GetString(this._flex판매품목H["CD_SUPPLIER"]);
                filter = "CD_SUPPLIER = '" + key + "'";

                if (this._flex판매품목H.DetailQueryNeed)
                {
                    dt = this._biz.Search판매품목L(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                  this._거래처코드,
                                                                  key,
                                                                  this.dtp판매품목.StartDateToString,
                                                                  this.dtp판매품목.EndDateToString });
                }

                this._flex판매품목L.Redraw = false;
                this._flex판매품목L.BindingAdd(dt, filter);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                this._flex판매품목L.Redraw = true;
            }
        }

        private void _flex판매품목L_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt;
            string key, key1, filter;

            try
            {
                dt = null;
                key = D.GetString(this._flex판매품목L["CD_SUPPLIER"]);
                key1 = D.GetString(this._flex판매품목L["CD_ITEM"]);
                filter = "CD_SUPPLIER = '" + key + "' AND CD_ITEM = '" + key1 + "'";

                if (this._flex판매품목L.DetailQueryNeed)
                {
                    dt = this._biz.Search판매품목D(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                  this._거래처코드,
                                                                  key,
                                                                  key1,
                                                                  this.dtp재고판매.StartDateToString,
                                                                  this.dtp재고판매.EndDateToString });
                }

                this._flex판매품목D.Redraw = false;
                this._flex판매품목D.BindingAdd(dt, filter);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                this._flex판매품목D.Redraw = true;
            }
        }
        #endregion

        #region 기타 메소드
        private Decimal Get영업활동Seq(string 거래처코드)
        {
            Decimal num = 0, num1 = 0;
            DataTable dataTable = DBHelper.GetDataTable(@"SELECT MAX(NO_INDEX) AS NO_INDEX 
                                                          FROM CZ_CRM_PARTNER_ACTIVITY WITH(NOLOCK)  
                                                          WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                         "AND CD_PARTNER = '" + 거래처코드 + "'");

            if (dataTable != null && dataTable.Rows.Count != 0)
                num = D.GetDecimal(dataTable.Rows[0]["NO_INDEX"]);

            num1 = D.GetDecimal(this._flex영업활동.DataTable.Compute("MAX(NO_INDEX)", string.Empty));

            if (num >= num1)
                return (num + 1);
            else
                return (num1 + 1);
        }

        private Decimal Get메모Seq(string 거래처코드)
        {
            Decimal num = 0, num1 = 0;
            DataTable dataTable = DBHelper.GetDataTable(@"SELECT MAX(NO_INDEX) AS NO_INDEX 
                                                          FROM CZ_CRM_PARTNER_MEMO WITH(NOLOCK)  
                                                          WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                         "AND CD_PARTNER = '" + 거래처코드 + "'");

            if (dataTable != null && dataTable.Rows.Count != 0)
                num = D.GetDecimal(dataTable.Rows[0]["NO_INDEX"]);

            num1 = D.GetDecimal(this._flex메모.DataTable.Compute("MAX(NO_INDEX)", string.Empty));

            if (num >= num1)
                return (num + 1);
            else
                return (num1 + 1);
        }

        private void 거래처이미지저장()
        {
            try
            {
                if (this._fileUploader.Count > 0)
                {
                    if (!this._fileUploader.Start())
                    {
                        this.ShowMessage("파일 전송 중 오류가 발생했습니다.");
                        return;
                    }

                    Global.MainFrame.ExecuteScalar(@"UPDATE CZ_MA_PARTNER
                                                     SET DC_PHOTO = '" + this._기본정보.CurrentRow["DC_PHOTO"].ToString() + "'" + Environment.NewLine +
                                                    "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                    "AND CD_PARTNER = '" + this._거래처코드 + "'");

                    this._기본정보.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion
    }
}
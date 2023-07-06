using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Web;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using DevExpress.XtraPivotGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Util.Uploader;
using Duzon.Erpiu.ComponentModel;
using Duzon.ERPU;
using DzHelpFormLib;
using DevExpress.Data.PivotGrid;
using DevExpress.Utils;

namespace cz
{
    public partial class P_CZ_SA_CRM_EMP : PageBase
    {
        #region 초기화 & 전역변수
        private string _사원번호;
        P_CZ_SA_CRM_EMP_BIZ _biz;
        FileUploader _fileUploader;
        FreeBinding _기본정보;

        public P_CZ_SA_CRM_EMP()
        {
            StartUp.Certify(this);
            InitializeComponent();
        }

        public P_CZ_SA_CRM_EMP(string 사원번호, string 사원명)
        {
            StartUp.Certify(this);
            InitializeComponent();

            this.ctx사원.CodeValue = 사원번호;
            this.ctx사원.CodeName = 사원명;
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this._biz = new P_CZ_SA_CRM_EMP_BIZ();
            this._fileUploader = new FileUploader(this.MainFrameInterface);
            this._기본정보 = new FreeBinding();

            this.InitGrid();
            this.InitEvent();
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.dtp조회기간.StartDateToString = Global.MainFrame.GetDateTimeToday().AddYears(-1).ToString("yyyyMMdd");
            this.dtp조회기간.EndDateToString = Global.MainFrame.GetStringToday;

            this.dtp거래내역매출.StartDateToString = Global.MainFrame.GetDateTimeToday().AddYears(-1).ToString("yyyyMMdd");
            this.dtp거래내역매출.EndDateToString = Global.MainFrame.GetStringToday;

            this.dtp미수금.StartDateToString = Global.MainFrame.GetDateTimeToday().AddYears(-1).ToString("yyyyMMdd");
            this.dtp미수금.EndDateToString = Global.MainFrame.GetStringToday;

            this.dtp클레임.StartDateToString = Global.MainFrame.GetDateTimeToday().AddYears(-1).ToString("yyyyMMdd");
            this.dtp클레임.EndDateToString = Global.MainFrame.GetStringToday;

            this.split사원명.SplitterDistance = 900;
            this.split좌측.SplitterDistance = 467;

            this.split일정.Panel2Collapsed = true;
            this.split할일.Panel2Collapsed = true;
            this.split메모.Panel2Collapsed = true;

            DBMgr dbMgr = new DBMgr(DBConn.GroupWare);

            dbMgr.Query = @"SELECT CD AS CODE,
                            	   NM_KR AS NAME 
                            FROM BX.TCMG_CDD WITH(NOLOCK)
                            WHERE CM_CD = 'so0005'
                            AND CO_ID = 0
                            AND USE_YN = 1";

            this.cbo일정상태.DataSource = dbMgr.GetDataTable();
            this.cbo일정상태.DisplayMember = "NAME";
            this.cbo일정상태.ValueMember = "CODE";

            this._flex일정.SetDataMap("SCH_STS", ((DataTable)this.cbo일정상태.DataSource).Copy(), "CODE", "NAME");

            dbMgr.Query = @"SELECT CD AS CODE,
                            	   NM_KR AS NAME 
                            FROM BX.TCMG_CDD WITH(NOLOCK)
                            WHERE CM_CD = 'so0016'
                            AND CO_ID = 0
                            AND USE_YN = 1";

            this.cbo할일우선순위.DataSource = dbMgr.GetDataTable();
            this.cbo할일우선순위.DisplayMember = "NAME";
            this.cbo할일우선순위.ValueMember = "CODE";

            this._flex할일.SetDataMap("ORDERBY", ((DataTable)this.cbo할일우선순위.DataSource).Copy(), "CODE", "NAME");

            dbMgr.Query = @"SELECT NM_KR AS CODE,
                            	   NM_KR AS NAME 
                            FROM BX.TCMG_CDD WITH(NOLOCK)
                            WHERE CM_CD = 'so0007'
                            AND CO_ID = 0
                            AND USE_YN = 1";

            DataTable 시간 = dbMgr.GetDataTable();

            dbMgr.Query = @"SELECT NM_KR AS CODE,
                            	   NM_KR AS NAME 
                            FROM BX.TCMG_CDD WITH(NOLOCK)
                            WHERE CM_CD = 'so0008'
                            AND CO_ID = 0
                            AND USE_YN = 1";

            DataTable 분 = dbMgr.GetDataTable();

            this.cbo일정시작시간.DataSource = 시간.Copy();
            this.cbo일정시작시간.DisplayMember = "NAME";
            this.cbo일정시작시간.ValueMember = "CODE";

            this.cbo일정종료시간.DataSource = 시간.Copy();
            this.cbo일정종료시간.DisplayMember = "NAME";
            this.cbo일정종료시간.ValueMember = "CODE";

            this.cbo메모시작시간.DataSource = 시간.Copy();
            this.cbo메모시작시간.DisplayMember = "NAME";
            this.cbo메모시작시간.ValueMember = "CODE";

            this.cbo일정시작분.DataSource = 분.Copy();
            this.cbo일정시작분.DisplayMember = "NAME";
            this.cbo일정시작분.ValueMember = "CODE";

            this.cbo일정종료분.DataSource = 분.Copy();
            this.cbo일정종료분.DisplayMember = "NAME";
            this.cbo일정종료분.ValueMember = "CODE";

            this.cbo메모시작분.DataSource = 분.Copy();
            this.cbo메모시작분.DisplayMember = "NAME";
            this.cbo메모시작분.ValueMember = "CODE";

            if (!string.IsNullOrEmpty(this.ctx사원.CodeValue))
                this.OnToolBarSearchButtonClicked(null, null);
        }

        private void InitEvent()
        {
            this.btn차트보기.Click += new EventHandler(this.btn차트보기_Click);

            this.ctx사원.QueryAfter += new BpQueryHandler(this.ctx사원_QueryAfter);
            this.btn지도보기.Click += new EventHandler(this.btn지도보기_Click);
            this.btn상세정보.Click += new EventHandler(this.btn상세정보_Click);

            #region 거래내역
            this.btn거래내역매출갱신.Click += new EventHandler(this.btn거래내역매출갱신_Click);
            this.btn거래내역매입갱신.Click += new EventHandler(this.btn거래내역매입갱신_Click);

            this.pivot거래내역매출.PivotGridControl.CustomSummary += new PivotGridCustomSummaryEventHandler(this.PivotGridControl_CustomSummary);
            this.pivot거래내역매입.PivotGridControl.CustomSummary += new PivotGridCustomSummaryEventHandler(this.PivotGridControl_CustomSummary);

            this._flex거래내역매출.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
            this._flex거래내역매입.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
            #endregion

            #region 일정
            this.btn일정상세보기.Click += new EventHandler(this.btn상세보기_Click);

            this.btn일정추가.Click += new EventHandler(this.btn일정추가_Click);
            this.btn일정제거.Click += new EventHandler(this.btn일정제거_Click);

            this._flex일정.AfterRowChange += new RangeEventHandler(this._flex일정_AfterRowChange);
            #endregion

            #region 할일
            this.btn할일상세보기.Click += new EventHandler(this.btn상세보기_Click);

            this.btn할일추가.Click += new EventHandler(this.btn할일추가_Click);
            this.btn할일제거.Click += new EventHandler(this.btn할일제거_Click);
            this.btn할일완료.Click += new EventHandler(this.btn할일완료_Click);
            this.btn할일완료해제.Click += new EventHandler(this.btn할일완료_Click);

            this._flex할일.AfterRowChange += new RangeEventHandler(this._flex할일_AfterRowChange);
            #endregion

            #region 메모
            this.btn메모상세보기.Click += new EventHandler(this.btn상세보기_Click);

            this.btn메모추가.Click += new EventHandler(this.btn메모추가_Click);
            this.btn메모제거.Click += new EventHandler(this.btn메모제거_Click);

            this._flex메모.AfterRowChange += new RangeEventHandler(this._flex메모_AfterRowChange);
            #endregion

            #region 미수금
            this.btn미수금갱신.Click += new EventHandler(this.btn미수금갱신_Click);
            
            this._flex미수금.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
            #endregion

            #region 클레임
            this.btn클레임갱신.Click += new EventHandler(this.btn클레임갱신_Click); 

            this._flex클레임.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
            #endregion
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flex일정,
                                              this._flex할일,
                                              this._flex메모 };

            #region 거래내역

            #region 매출

            #region 요약
            this.pivot거래내역매출.SetStart();

            this.pivot거래내역매출.AddPivotField("NO_FILE", "파일번호", 100, true, PivotArea.FilterArea);
            this.pivot거래내역매출.AddPivotField("NM_VESSEL", "호선명", 100, true, PivotArea.FilterArea);
            this.pivot거래내역매출.AddPivotField("DT_INQ", "문의일자", 100, true, PivotArea.FilterArea);
            this.pivot거래내역매출.AddPivotField("DT_QTN", "견적일자", 100, true, PivotArea.FilterArea);
            this.pivot거래내역매출.AddPivotField("DT_SO", "수주일자", 100, true, PivotArea.FilterArea);
            this.pivot거래내역매출.AddPivotField("NM_ITEMGRP", "품목군", 100, true, PivotArea.FilterArea);

            this.pivot거래내역매출.AddPivotField("NM_PARTNER", "매출처", 100, true, PivotArea.RowArea);

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
            this._flex거래내역매출.SetCol("NM_PARTNER", "매출처", 100);
            this._flex거래내역매출.SetCol("NM_VESSEL", "호선명", 100);
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
            this.pivot거래내역매입.AddPivotField("DT_INQ", "문의일자", 100, true, PivotArea.FilterArea);
            this.pivot거래내역매입.AddPivotField("DT_QTN", "견적일자", 100, true, PivotArea.FilterArea);
            this.pivot거래내역매입.AddPivotField("DT_PO", "발주일자", 100, true, PivotArea.FilterArea);
            this.pivot거래내역매입.AddPivotField("DT_IN", "입고일자", 100, true, PivotArea.FilterArea);
            this.pivot거래내역매입.AddPivotField("LT", "납기", 100, true, PivotArea.FilterArea);
            this.pivot거래내역매입.AddPivotField("LT_IN", "납품소요일", 100, true, PivotArea.FilterArea);
            this.pivot거래내역매입.AddPivotField("NM_ITEMGRP", "품목군", 100, true, PivotArea.FilterArea);

            this.pivot거래내역매입.AddPivotField("NM_SUPPLIER", "매입처", 100, true, PivotArea.RowArea); 

            this.pivot거래내역매입.AddPivotField("QT_INQ", "문의건수", 100, true, PivotArea.DataArea);
            this.pivot거래내역매입.AddPivotField("QT_QTN", "견적건수", 100, true, PivotArea.DataArea);
            this.pivot거래내역매입.AddPivotField("QT_PO", "발주건수", 100, true, PivotArea.DataArea);
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
            this.pivot거래내역매입.AddPivotField("RT_PO", "발주율", 60, true, PivotSummaryType.Custom, PivotArea.DataArea);
            this.pivot거래내역매입.AddPivotField("RT_PROFIT_QTN", "이윤율(견적)", 120, true, PivotSummaryType.Custom, PivotArea.DataArea);
            this.pivot거래내역매입.AddPivotField("RT_PROFIT_PO", "이윤율(발주만)", 120, true, PivotSummaryType.Custom, PivotArea.DataArea);
            this.pivot거래내역매입.AddPivotField("RT_PROFIT", "이윤율(재고포함)", 120, true, PivotSummaryType.Custom, PivotArea.DataArea);

            this.pivot거래내역매입.PivotGridControl.Fields["QT_INQ"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot거래내역매입.PivotGridControl.Fields["QT_INQ"].CellFormat.FormatString = "0";
            this.pivot거래내역매입.PivotGridControl.Fields["QT_QTN"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot거래내역매입.PivotGridControl.Fields["QT_QTN"].CellFormat.FormatString = "0";
            this.pivot거래내역매입.PivotGridControl.Fields["QT_PO"].CellFormat.FormatType = FormatType.Numeric;
            this.pivot거래내역매입.PivotGridControl.Fields["QT_PO"].CellFormat.FormatString = "0";
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
            this._flex거래내역매입.SetCol("NM_SUPPLIER", "매입처", 80);
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

            #region 일정
            this._flex일정.BeginSetting(1, 1, false);

            this._flex일정.SetCol("DT_DDAY", "경과일수", 60);
            this._flex일정.SetCol("SCH_STS", "상태", 60);
            this._flex일정.SetCol("SCH_NM", "일정명", 100);
            this._flex일정.SetCol("SCH_PLACE", "장소", 100);
            this._flex일정.SetCol("ALL_DAY_YN", "종일여부", 60, false, CheckTypeEnum.ONE_ZERO);
            this._flex일정.SetCol("DT_START", "시작일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex일정.SetCol("DT_END", "종료일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex일정.SetCol("TP_REPEAT", "반복설정", false);
            this._flex일정.SetCol("WORK_CONTENTS", "완료내용", 100);
            this._flex일정.SetCol("CONTENTS", "내용", 150);
            
            this._flex일정.SetOneGridBinding(null, new IUParentControl[] { this.pnl일정, this.pnl일정내용, this.pnl완료내용 });
            this._flex일정.SetBindningCheckBox(this.chk종일여부, "1", "0");
            this._flex일정.VerifyNotNull = new string[] { "SCH_NM" };

            this._flex일정.ExtendLastCol = true;

            this._flex일정.SettingVersion = "0.0.0.1";
            this._flex일정.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region 할일
            this._flex할일.BeginSetting(1, 1, false);

            this._flex할일.SetCol("END_YN", "완료여부", 60, false, CheckTypeEnum.ONE_ZERO);
            this._flex할일.SetCol("CATEGORY_ID", "분류", 60);
            this._flex할일.SetCol("ORDERBY", "우선순위", 60);
            this._flex할일.SetCol("TITLE", "제목", 100);
            this._flex할일.SetCol("START_DT", "시작일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex할일.SetCol("END_DT", "종료일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex할일.SetCol("PROC_RATE", "진행율", 80);

            this._flex할일.SetOneGridBinding(null, new IUParentControl[] { this.pnl할일, this.pnl할일내용, this.pnl할일처리내역 });
            this._flex할일.VerifyNotNull = new string[] { "TITLE" };
            
            this._flex할일.ExtendLastCol = true;

            this._flex할일.SettingVersion = "0.0.0.1";
            this._flex할일.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region 메모
            this._flex메모.BeginSetting(1, 1, false);

            this._flex메모.SetCol("DT_START", "시작일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex메모.SetCol("CONTENTS", "내용", 150);

            this._flex메모.SetOneGridBinding(null, new IUParentControl[] { this.pnl메모, this.pnl메모내용 });
            this._flex메모.VerifyNotNull = new string[] { "CONTENTS" };

            this._flex메모.ExtendLastCol = true;

            this._flex메모.SettingVersion = "0.0.0.1";
            this._flex메모.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
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
            this.pivot클레임.AddPivotField("NM_PARTNER", "매출처", 100, true, PivotArea.FilterArea);
            this.pivot클레임.AddPivotField("NM_VESSEL", "호선", 100, true, PivotArea.FilterArea);
            this.pivot클레임.AddPivotField("DT_SO_DIFF", "발생일수", 100, true, PivotArea.FilterArea);
            this.pivot클레임.AddPivotField("DT_CLOSING_DIFF", "종결일수", 100, true, PivotArea.FilterArea);
            this.pivot클레임.AddPivotField("DC_RECEIVE", "클레임내용", 100, true, PivotArea.FilterArea);

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
            this._flex클레임.SetCol("NM_PARTNER", "매출처", 100);
            this._flex클레임.SetCol("NM_VESSEL", "호선", 100);
            this._flex클레임.SetCol("QT_CLAIM", "클레임건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex클레임.SetCol("AM_CLAIM", "클레임금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex클레임.SetCol("DT_SO_DIFF", "발생일수", 80);
            this._flex클레임.SetCol("DT_CLOSING_DIFF", "종결일수", 80);
            this._flex클레임.SetCol("DC_RECEIVE", "클레임내용", 100);

            this._flex클레임.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #endregion
        }
        #endregion

        #region 메인버튼 이벤트
        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            DataTable dt기본정보, dt거래내역매출, dt거래내역매입, dt일정 = null, dt할일 = null, dt메모 = null, dt미수금, dt클레임;

            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!this.BeforeSearch()) return;
                if (string.IsNullOrEmpty(this.ctx사원.CodeValue))
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl사원.Text);
                    return;
                }

                this._사원번호 = this.ctx사원.CodeValue;

                if (this._사원번호 == Global.MainFrame.LoginInfo.UserID)
                {
                    this.btn지도보기.Enabled = true;
                    this.btn상세정보.Enabled = true;

                    this.meb현주소.Visible = true;
                    this.txt현주소.Visible = true;
                    this.txt현주소상세.Visible = true;
                    this.txt휴대폰번호.Visible = true;

                    this.tcl개인일정.Visible = true;
                }
                else
                {
                    this.btn지도보기.Enabled = false;
                    this.btn상세정보.Enabled = false;

                    this.meb현주소.Visible = false;
                    this.txt현주소.Visible = false;
                    this.txt현주소상세.Visible = false;
                    this.txt휴대폰번호.Visible = false;

                    this.tcl개인일정.Visible = false;
                }

                DBMgr dbMgr = new DBMgr(DBConn.GroupWare);
                dbMgr.Query = @"SELECT 0 AS CODE,
                                       '' AS NAME
                                UNION ALL
                                SELECT CT.CATEGORY_ID AS CODE,
                                       CT.CATEGORY_NM AS NAME
                                FROM BX.TSOG_TODO_CATEGORY CT WITH(NOLOCK)
                                JOIN BX.TCMG_USER US WITH(NOLOCK) ON US.USER_ID = CT.USER_ID
                                WHERE US.LOGON_CD = '" + this._사원번호 + "'";

                this.cbo할일분류.DataSource = dbMgr.GetDataTable();
                this.cbo할일분류.DisplayMember = "NAME";
                this.cbo할일분류.ValueMember = "CODE";

                this._flex할일.SetDataMap("CATEGORY_ID", ((DataTable)this.cbo할일분류.DataSource).Copy(), "CODE", "NAME");

                MsgControl.ShowMsg("[자료조회중 : 기본정보]\n잠시만 기다려 주세요.");

                dt기본정보 = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                             this._사원번호,
                                                             this.dtp조회기간.StartDateToString,
                                                             this.dtp조회기간.EndDateToString });

                if (this._사원번호 == Global.MainFrame.LoginInfo.UserID)
                {
                    MsgControl.ShowMsg("[자료조회중 : 일정]\n잠시만 기다려 주세요.");
                    dt일정 = this._biz.Search일정(this._사원번호);
                    MsgControl.ShowMsg("[자료조회중 : 할일]\n잠시만 기다려 주세요.");
                    dt할일 = this._biz.Search할일(this._사원번호);
                    MsgControl.ShowMsg("[자료조회중 : 메모]\n잠시만 기다려 주세요.");
                    dt메모 = this._biz.Search메모(this._사원번호);
                }

                MsgControl.ShowMsg("[자료조회중 : 거래내역-매출]\n잠시만 기다려 주세요.");

                dt거래내역매출 = this._biz.Search거래내역(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                         this._사원번호,
                                                                         "001",
                                                                         this.dtp거래내역매출.StartDateToString,
                                                                         this.dtp거래내역매출.EndDateToString });

                MsgControl.ShowMsg("[자료조회중 : 거래내역-매입]\n잠시만 기다려 주세요.");

                dt거래내역매입 = this._biz.Search거래내역(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                         this._사원번호,
                                                                         "002",
                                                                         this.dtp거래내역매출.StartDateToString,
                                                                         this.dtp거래내역매출.EndDateToString });

                MsgControl.ShowMsg("[자료조회중 : 미수금]\n잠시만 기다려 주세요.");

                dt미수금 = this._biz.Search미수금(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                 this._사원번호,
                                                                 this.dtp미수금.StartDateToString,
                                                                 this.dtp미수금.EndDateToString,
                                                                 (this.chk미수금0표시.Checked ? "Y" : "N") });

                MsgControl.ShowMsg("[자료조회중 : 클레임]\n잠시만 기다려 주세요.");

                dt클레임 = this._biz.Search클레임(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                 this._사원번호,
                                                                 this.dtp클레임.StartDateToString,
                                                                 this.dtp클레임.EndDateToString });

                MsgControl.ShowMsg("[데이터세팅중]\n잠시만 기다려 주세요.");

                this._기본정보.SetBinding(dt기본정보, this.pnl기본정보);

                if (dt기본정보.Rows.Count > 0)
                {
                    this.lbl사원명.Text = D.GetString(dt기본정보.Rows[0]["NM_EMP"]);

                    this.txt근속기간.Text = D.GetString(dt기본정보.Rows[0]["DT_WORKING_PERIOD"]);
                    this.txt매출금액.Text = string.Format("{0:N0}", D.GetDecimal(dt기본정보.Rows[0]["AM_SO"])) + " 원";
                    this.txt이윤.Text = string.Format("{0:N0}", D.GetDecimal(dt기본정보.Rows[0]["AM_PROFIT"])) + " 원";
                    this.txt이윤율.Text = string.Format("{0:0.00}", D.GetDecimal(dt기본정보.Rows[0]["RT_PROFIT"])) + "%";
                    this.txt수주율.Text = string.Format("{0:0.00}", D.GetDecimal(dt기본정보.Rows[0]["RT_SO"])) + "%";

                    this._기본정보.SetDataTable(dt기본정보);
                    this._기본정보.AcceptChanges();

                    FileInfoCs fileInfoCs = this._fileUploader[D.GetString(this._기본정보.CurrentRow["DC_PHOTO"]), "shared/image/human/photo/" + Global.MainFrame.LoginInfo.CompanyCode + "/"];

                    if (fileInfoCs != null && fileInfoCs.Image != null)
                        this.pic사원이미지.Image = fileInfoCs.Image;
                    else
                        this.pic사원이미지.Image = Properties.Resources.사진;
                }
                else
                {
                    this.lbl사원명.Text = string.Empty;

                    this.txt근속기간.Text = string.Empty;

                    this._기본정보.ClearAndNewRow();
                    this._기본정보.AcceptChanges();
                }

                dt거래내역매출.TableName = this.PageID;
                dt거래내역매입.TableName = this.PageID;
                dt미수금.TableName = this.PageID;
                dt클레임.TableName = this.PageID;

                this.pivot거래내역매출.DataSource = dt거래내역매출;
                this.pivot거래내역매입.DataSource = dt거래내역매입;
                this.pivot미수금.DataSource = dt미수금;
                this.pivot클레임.DataSource = dt클레임;

                this._flex거래내역매출.Binding = dt거래내역매출;
                this._flex거래내역매입.Binding = dt거래내역매입;
                
                if (this._사원번호 == Global.MainFrame.LoginInfo.UserID)
                {
                    this._flex일정.Binding = dt일정;
                    this._flex할일.Binding = dt할일;
                    this._flex메모.Binding = dt메모;
                }
                
                this._flex미수금.Binding = dt미수금;
                this._flex클레임.Binding = dt클레임;
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

            if (this._flex일정.IsDataChanged == false &&
                this._flex할일.IsDataChanged == false &&
                this._flex메모.IsDataChanged == false) return false;

            if (!this._biz.Save(this._flex일정.GetChanges(),
                                this._flex할일.GetChanges(),
                                this._flex메모.GetChanges())) return false;

            this._flex일정.AcceptChanges();
            this._flex할일.AcceptChanges();
            this._flex메모.AcceptChanges();

            return true;
        }
        #endregion

        #region 컨트롤 이벤트
        private void ctx사원_QueryAfter(object sender, BpQueryArgs e)
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

        private void btn지도보기_Click(object sender, EventArgs e)
        {
            string strURL;

            try
            {
                strURL = "http://maps.google.co.kr/maps?q=" + HttpUtility.UrlEncode(this.txt현주소.Text.Trim(), Encoding.UTF8) + " " + HttpUtility.UrlEncode(this.txt현주소상세.Text.Trim(), Encoding.UTF8);
                Process.Start("msedge.exe", strURL);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn상세정보_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this._사원번호))
                    return;

                if (Global.MainFrame.IsExistPage("P_HR_EMP", false))
                    Global.MainFrame.UnLoadPage("P_HR_EMP", false);

                Global.MainFrame.LoadPageFrom("P_HR_EMP", "사원등록", new object[] { this._사원번호 });
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn차트보기_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this._사원번호))
                    return;

                if (Global.MainFrame.IsExistPage("P_CZ_SA_CRM_CHART", false))
                    Global.MainFrame.UnLoadPage("P_CZ_SA_CRM_CHART", false);

                Global.MainFrame.LoadPageFrom("P_CZ_SA_CRM_CHART", "차트(CRM)", new object[] { null, null, this.ctx사원.CodeValue, this.ctx사원.CodeName });
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

                if (name == this.btn일정상세보기.Name)
                {
                    if (this.split일정.Panel2Collapsed)
                    {
                        this.split일정.SplitterDistance = 699;
                        this.split일정내용.SplitterDistance = 216;
                        this.split일정.Panel2Collapsed = false;
                    }
                    else
                        this.split일정.Panel2Collapsed = true;
                }
                else if (name == this.btn할일상세보기.Name)
                {
                    if (this.split할일.Panel2Collapsed)
                    {
                        this.split할일.SplitterDistance = 699;
                        this.split할일내용.SplitterDistance = 212;
                        this.split할일.Panel2Collapsed = false;
                    }
                    else
                        this.split할일.Panel2Collapsed = true;
                }
                else if (name == this.btn메모상세보기.Name)
                {
                    if (this.split메모.Panel2Collapsed)
                    {
                        this.split메모.SplitterDistance = 699;
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

        #region 일정
        private void btn일정추가_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this._사원번호)) return;

                this._flex일정.Rows.Add();
                this._flex일정.Row = this._flex일정.Rows.Count - 1;

                this._flex일정["SCH_STS"] = "10";
                this._flex일정["DT_START"] = Global.MainFrame.GetStringToday;
                this._flex일정["DT_END"] = Global.MainFrame.GetStringToday;
                this._flex일정["DT_HOUR_START"] = "00";
                this._flex일정["DT_MINUTE_START"] = "00";
                this._flex일정["DT_HOUR_END"] = "00";
                this._flex일정["DT_MINUTE_END"] = "00";
                this._flex일정["ID_INSERT"] = Global.MainFrame.LoginInfo.UserID;

                this._flex일정.AddFinished();
                this._flex일정.Col = this._flex일정.Cols.Fixed;
                this._flex일정.Focus();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn일정제거_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex일정.HasNormalRow) return;
                if (D.GetString(this._flex일정["ID_INSERT"]) != Global.MainFrame.LoginInfo.UserID)
                {
                    this.ShowMessage("등록자만 삭제 가능 합니다. (등록자 : " + D.GetString(this._flex일정["ID_INSERT"]) + ")");
                    return;
                }

                this._flex일정.GetDataRow(this._flex일정.Row).Delete();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region 할일
        private void btn할일추가_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this._사원번호)) return;

                this._flex할일.Rows.Add();
                this._flex할일.Row = this._flex할일.Rows.Count - 1;

                this._flex할일["ORDERBY"] = "0";
                this._flex할일["END_YN"] = "0";
                this._flex할일["START_DT"] = Global.MainFrame.GetStringToday;
                this._flex할일["END_DT"] = Global.MainFrame.GetStringToday;
                this._flex할일["ID_INSERT"] = Global.MainFrame.LoginInfo.UserID;

                this._flex할일.AddFinished();
                this._flex할일.Col = this._flex할일.Cols.Fixed;
                this._flex할일.Focus();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn할일제거_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex할일.HasNormalRow) return;
                if (D.GetString(this._flex할일["ID_INSERT"]) != Global.MainFrame.LoginInfo.UserID)
                {
                    this.ShowMessage("등록자만 삭제 가능 합니다. (등록자 : " + D.GetString(this._flex할일["ID_INSERT"]) + ")");
                    return;
                }

                this._flex할일.GetDataRow(this._flex할일.Row).Delete();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn할일완료_Click(object sender, EventArgs e)
        {
            string name;

            try
            {
                if (!this._flex할일.HasNormalRow) return;
                if (D.GetString(this._flex할일["ID_INSERT"]) != Global.MainFrame.LoginInfo.UserID)
                {
                    this.ShowMessage("등록자만 완료 가능 합니다. (등록자 : " + D.GetString(this._flex할일["ID_INSERT"]) + ")");
                    return;
                }

                name = ((Control)sender).Name;

                if (name == this.btn할일완료.Name)
                    this._flex할일["END_YN"] = "1";
                else
                    this._flex할일["END_YN"] = "0";
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
                if (string.IsNullOrEmpty(this._사원번호)) return;

                this._flex메모.Rows.Add();
                this._flex메모.Row = this._flex메모.Rows.Count - 1;

                this._flex메모["DT_START"] = Global.MainFrame.GetStringToday;
                this._flex메모["DT_HOUR_START"] = "00";
                this._flex메모["DT_MINUTE_START"] = "00";
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

        private void btn거래내역매출갱신_Click(object sender, EventArgs e)
        {
            DataTable dt거래내역;

            try
            {
                if (string.IsNullOrEmpty(this._사원번호))
                {
                    this.ShowMessage("조회후 다시 시도하세요.");
                    return;
                }

                dt거래내역 = this._biz.Search거래내역(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                     this._사원번호,
                                                                     "001",
                                                                     this.dtp거래내역매출.StartDateToString,
                                                                     this.dtp거래내역매출.EndDateToString });

                dt거래내역.TableName = this.PageID;
                this.pivot거래내역매출.DataSource = dt거래내역;
                this._flex거래내역매출.Binding = dt거래내역;

                if (!this._flex거래내역매출.HasNormalRow)
                    this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn거래내역매입갱신_Click(object sender, EventArgs e)
        {
            DataTable dt거래내역;

            try
            {
                if (string.IsNullOrEmpty(this._사원번호))
                {
                    this.ShowMessage("조회후 다시 시도하세요.");
                    return;
                }

                dt거래내역 = this._biz.Search거래내역(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                     this._사원번호,
                                                                     "002",
                                                                     this.dtp거래내역매입.StartDateToString,
                                                                     this.dtp거래내역매입.EndDateToString });

                dt거래내역.TableName = this.PageID;
                this.pivot거래내역매입.DataSource = dt거래내역;
                this._flex거래내역매입.Binding = dt거래내역;

                if (!this._flex거래내역매입.HasNormalRow)
                    this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn미수금갱신_Click(object sender, EventArgs e)
        {
            DataTable dt미수금;

            try
            {
                if (string.IsNullOrEmpty(this._사원번호))
                {
                    this.ShowMessage("조회후 다시 시도하세요.");
                    return;
                }

                dt미수금 = this._biz.Search미수금(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                 this._사원번호,
                                                                 this.dtp미수금.StartDateToString,
                                                                 this.dtp미수금.EndDateToString,
                                                                 (this.chk미수금0표시.Checked ? "Y" : "N") });

                dt미수금.TableName = this.PageID;
                this.pivot미수금.DataSource = dt미수금;
                this._flex미수금.Binding = dt미수금;

                if (!this._flex미수금.HasNormalRow)
                    this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn클레임갱신_Click(object sender, EventArgs e)
        {
            DataTable dt클레임;

            try
            {
                if (string.IsNullOrEmpty(this._사원번호))
                {
                    this.ShowMessage("조회후 다시 시도하세요.");
                    return;
                }

                dt클레임 = this._biz.Search클레임(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                 this._사원번호,
                                                                 this.dtp클레임.StartDateToString,
                                                                 this.dtp클레임.EndDateToString });

                dt클레임.TableName = this.PageID;
                this.pivot클레임.DataSource = dt클레임;
                this._flex클레임.Binding = dt클레임;

                if (!this._flex클레임.HasNormalRow)
                    this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region 그리드 이벤트
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
                else if (grid.Cols["NM_PARTNER"] != null && grid.ColSel == grid.Cols["NM_PARTNER"].Index)
                {
                    pageId = "P_CZ_SA_CRM_PARTNER";
                    pageName = Global.MainFrame.DD("거래처(CRM)");
                    obj = new object[] { D.GetString(grid["CD_PARTNER"]), D.GetString(grid["NM_PARTNER"]) };

                    if (Global.MainFrame.IsExistPage(pageId, false))
                        Global.MainFrame.UnLoadPage(pageId, false);

                    Global.MainFrame.LoadPageFrom(pageId, pageName, this.Grant, obj);
                }
                else if (grid.Cols["NM_SUPPLIER"] != null && grid.ColSel == grid.Cols["NM_SUPPLIER"].Index)
                {
                    pageId = "P_CZ_SA_CRM_PARTNER";
                    pageName = Global.MainFrame.DD("거래처(CRM)");
                    obj = new object[] { D.GetString(grid["CD_SUPPLIER"]), D.GetString(grid["NM_SUPPLIER"]) };

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

        private void _flex일정_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (D.GetInt(this._flex일정["MENU_ID"]) == 0 && 
                    this._사원번호 == Global.MainFrame.LoginInfo.UserID)
                {
                    this.btn일정제거.Enabled = true;
                    this.일정컨트롤활성화(true);
                }
                else
                {
                    this.btn일정제거.Enabled = false;
                    this.일정컨트롤활성화(false);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex할일_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (this._사원번호 == Global.MainFrame.LoginInfo.UserID)
                {
                    if (D.GetString(this._flex할일["END_YN"]) == "1")
                    {
                        this.btn할일완료.Enabled = false;
                        this.btn할일완료해제.Enabled = true;
                    }
                    else
                    {
                        this.btn할일완료.Enabled = true;
                        this.btn할일완료해제.Enabled = false;
                    }

                    this.btn할일제거.Enabled = true;
                    this.할일컨트롤활성화(true);
                }
                else
                {
                    this.btn할일완료.Enabled = false;
                    this.btn할일완료해제.Enabled = false;

                    this.btn할일제거.Enabled = false;
                    this.할일컨트롤활성화(false);
                }
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
                if (this._사원번호 == Global.MainFrame.LoginInfo.UserID)
                {
                    this.btn메모제거.Enabled = true;
                    this.메모컨트롤활성화(true);
                }
                else
                {
                    this.btn메모제거.Enabled = false;
                    this.메모컨트롤활성화(false);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region 기타 메소드
        private void 일정컨트롤활성화(bool 활성여부)
        {
            try
            {
                if (활성여부)
                {
                    this.txt일정명.ReadOnly = false;
                    this.txt일정장소.ReadOnly = false;
                    this.dtp일정시작일자.Enabled = true;
                    this.cbo일정시작시간.Enabled = true;
                    this.cbo일정시작분.Enabled = true;
                    this.dtp일정종료일자.Enabled = true;
                    this.cbo일정종료시간.Enabled = true;
                    this.cbo일정종료분.Enabled = true;
                    this.chk종일여부.Enabled = true;
                    this.cbo일정상태.Enabled = true;

                    this.txt일정내용.ReadOnly = false;
                    this.txt완료내용.ReadOnly = false;
                }
                else
                {
                    this.txt일정명.ReadOnly = true;
                    this.txt일정장소.ReadOnly = true;
                    this.dtp일정시작일자.Enabled = false;
                    this.cbo일정시작시간.Enabled = false;
                    this.cbo일정시작분.Enabled = false;
                    this.dtp일정종료일자.Enabled = false;
                    this.cbo일정종료시간.Enabled = false;
                    this.cbo일정종료분.Enabled = false;
                    this.chk종일여부.Enabled = false;
                    this.cbo일정상태.Enabled = false;

                    this.txt일정내용.ReadOnly = true;
                    this.txt완료내용.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void 할일컨트롤활성화(bool 활성여부)
        {
            try
            {
                if (활성여부)
                {
                    this.txt할일제목.ReadOnly = false;
                    this.cbo할일분류.Enabled = true;
                    this.cbo할일우선순위.Enabled = true;
                    this.dtp할일시작일자.Enabled = true;
                    this.dtp할일종료일자.Enabled = true;
                    this.cur할일진행율.ReadOnly = false;

                    this.txt할일내용.ReadOnly = false;
                    this.txt할일처리내역.ReadOnly = false;
                }
                else
                {
                    this.txt할일제목.ReadOnly = true;
                    this.cbo할일분류.Enabled = false;
                    this.cbo할일우선순위.Enabled = false;
                    this.dtp할일시작일자.Enabled = false;
                    this.dtp할일종료일자.Enabled = false;
                    this.cur할일진행율.ReadOnly = true;

                    this.txt할일내용.ReadOnly = true;
                    this.txt할일처리내역.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void 메모컨트롤활성화(bool 활성여부)
        {
            try
            {
                if (활성여부)
                {
                    this.dtp메모시작일자.Enabled = true;
                    this.cbo메모시작시간.Enabled = true;
                    this.cbo메모시작분.Enabled = true;

                    this.txt메모내용.ReadOnly = false;
                }
                else
                {
                    this.dtp메모시작일자.Enabled = false;
                    this.cbo메모시작시간.Enabled = false;
                    this.cbo메모시작분.Enabled = false;

                    this.txt메모내용.ReadOnly = true;
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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using ChartFX.WinForms;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.Common.Util.Uploader;
using Duzon.Erpiu.ComponentModel;
using Duzon.ERPU;

namespace cz
{
    public partial class P_CZ_SA_CRM_PARTNER_PIC : PageBase
    {
        #region 초기화 & 전역변수
        P_CZ_SA_CRM_PARTNER_PIC_BIZ _biz = new P_CZ_SA_CRM_PARTNER_PIC_BIZ();
        private FileUploader _fileUploader;

        public P_CZ_SA_CRM_PARTNER_PIC()
        {
            StartUp.Certify(this);
            InitializeComponent();
        }

        public P_CZ_SA_CRM_PARTNER_PIC(string 거래처코드, string 거래처명, string 담당자명)
        {
            StartUp.Certify(this);
            InitializeComponent();

            this.ctx거래처S.CodeValue = 거래처코드;
            this.ctx거래처S.CodeName = 거래처명;

            this.txt담당자명.Text = 담당자명;
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this._fileUploader = new FileUploader(Global.MainFrame);

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flex담당자, this._flex부가정보, this._flex관련인물, this._flex근무이력, this._flex담당호선, this._flex영업활동, this._flex물류서비스 };
            this._flex담당자.DetailGrids = new FlexGrid[] { this._flex부가정보, this._flex관련인물, this._flex근무이력, this._flex담당호선, this._flex영업활동, this._flex미팅메모, this._flex커미션, this._flex물류서비스 };

            #region 담당자
            this._flex담당자.BeginSetting(1, 1, false);

            this._flex담당자.SetCol("CD_RANK", "등급", 80);
            this._flex담당자.SetCol("NM_CLS_PARTNER", "거래처분류", 80);
            this._flex담당자.SetCol("NM_FG_PARTNER", "거래처구분", 80);
            this._flex담당자.SetCol("LN_PARTNER", "거래처명", 120);
            this._flex담당자.SetCol("NM_PTR", "담당자명", 100);
            this._flex담당자.SetCol("FILE_PATH_MNG", "첨부파일", 80);
            this._flex담당자.SetCol("DT_INSERT", "등록일자", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex담당자.SetCol("DT_UPDATE", "갱신일자", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            this._flex담당자.SetDummyColumn(new string[] { "FILE_PATH_MNG" });

            this._flex담당자.SetOneGridBinding(null, new IUParentControl[] { this.pnl기본정보, this.one부가정보, this.pnl메모 });

            this._flex담당자.SettingVersion = "0.0.0.1";
            this._flex담당자.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex담당자.Styles.Add("매입처").ForeColor = Color.Blue;
            this._flex담당자.Styles.Add("매입처").BackColor = Color.White;
            this._flex담당자.Styles.Add("매출처").ForeColor = Color.Red;
            this._flex담당자.Styles.Add("매출처").BackColor = Color.White;
            this._flex담당자.Styles.Add("매입매출").ForeColor = Color.Green;
            this._flex담당자.Styles.Add("매입매출").BackColor = Color.White;
            this._flex담당자.Styles.Add("포워더").ForeColor = Color.Orange;
            this._flex담당자.Styles.Add("포워더").BackColor = Color.White;
            this._flex담당자.Styles.Add("관리").ForeColor = Color.Navy;
            this._flex담당자.Styles.Add("관리").BackColor = Color.White;
            this._flex담당자.Styles.Add("기타").ForeColor = Color.Purple;
            this._flex담당자.Styles.Add("기타").BackColor = Color.White;
            #endregion

            #region 부가정보
            this._flex부가정보.BeginSetting(1, 1, false);

            this._flex부가정보.SetCol("CD_ITEM", "항목코드", false);
            this._flex부가정보.SetCol("NM_ITEM", "항목명", 120, true);
            this._flex부가정보.SetCol("DC_ITEM", "내용", 120, true);

            this._flex부가정보.ExtendLastCol = true;

            this._flex부가정보.SettingVersion = "0.0.0.1";
            this._flex부가정보.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #region 관련인물
            this._flex관련인물.BeginSetting(1, 1, false);

            this._flex관련인물.SetCol("YN_AUTO", "자동등록", 80, false, CheckTypeEnum.Y_N);
            this._flex관련인물.SetCol("CD_RANK", "등급", 80);
            this._flex관련인물.SetCol("LN_PARTNER", "거래처명", 120);
            this._flex관련인물.SetCol("NM_PTR", "담당자명", 100);
            this._flex관련인물.SetCol("DC_RMK", "비고", 120, true);

            this._flex관련인물.SetOneGridBinding(null, new IUParentControl[] { this.pnl관련인물상세 });
            this._flex관련인물.ExtendLastCol = true;
            this._flex관련인물.SetCodeHelpCol("NM_PTR", "H_CZ_MA_PARTNERPTR_SUB", ShowHelpEnum.Always, new string[] { "CD_PARTNER_SUB", "SEQ_SUB", "NM_PTR", "LN_PARTNER" }, new string[] { "CD_PARTNER", "SEQ", "NM_PTR", "LN_PARTNER" });

            this._flex관련인물.SettingVersion = "0.0.0.1";
            this._flex관련인물.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #region 근무이력
            this._flex근무이력.BeginSetting(1, 1, false);

            this._flex근무이력.SetCol("DT_JOIN", "입사일자", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex근무이력.SetCol("DT_RETIRE", "퇴사일자", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex근무이력.SetCol("LN_EX_COMPANY", "회사명", 100, true);
            this._flex근무이력.SetCol("NM_EX_COMPANY", "회사명(입력)", 100, true);
            this._flex근무이력.SetCol("DC_DEPT", "부서", 100, true);
            this._flex근무이력.SetCol("DC_DUTY_RESP", "직급", 100, true);
            this._flex근무이력.SetCol("DC_RMK", "비고", 120, true);

            this._flex근무이력.ExtendLastCol = true;

            this._flex근무이력.SetCodeHelpCol("LN_EX_COMPANY", HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, "CD_EX_COMPANY", "LN_EX_COMPANY");

            this._flex근무이력.SettingVersion = "0.0.0.1";
            this._flex근무이력.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #region 담당호선
            this._flex담당호선.BeginSetting(1, 1, false);

            this._flex담당호선.SetCol("NO_IMO", "IMO번호", 100);
            this._flex담당호선.SetCol("NO_HULL", "호선번호", 100);
            this._flex담당호선.SetCol("NM_VESSEL", "호선명", 120);
            this._flex담당호선.SetCol("LN_PARTNER", "운항선사", 100);
            this._flex담당호선.SetCol("DC_RMK", "비고", 120, true);

            this._flex담당호선.ExtendLastCol = true;
            this._flex담당호선.SetCodeHelpCol("NO_IMO", "H_CZ_MA_HULL_SUB", ShowHelpEnum.Always, new string[] { "NO_IMO", "NO_HULL", "NM_VESSEL", "LN_PARTNER" }, new string[] { "NO_IMO", "NO_HULL", "NM_VESSEL", "LN_PARTNER" });

            this._flex담당호선.SettingVersion = "0.0.0.1";
            this._flex담당호선.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #region 담당호선리스트
            
            #region 품목그룹
            this._flex담당호선품목그룹.BeginSetting(1, 1, false);

            this._flex담당호선품목그룹.SetCol("NM_ITEMGRP", "품목그룹", 80);
            this._flex담당호선품목그룹.SetCol("QT_QTN", "견적건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex담당호선품목그룹.SetCol("QT_SO", "수주건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex담당호선품목그룹.SetCol("RT_SO", "수주율", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex담당호선품목그룹.SetCol("AM_QTN", "견적금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex담당호선품목그룹.SetCol("AM_SO", "수주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex담당호선품목그룹.SetCol("AM_PO", "발주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex담당호선품목그룹.SetCol("AM_STOCK", "재고금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex담당호선품목그룹.SetCol("AM_PROFIT", "이윤", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex담당호선품목그룹.SetCol("RT_PROFIT", "이윤율", 100, false, typeof(decimal), FormatTpType.RATE);

            this._flex담당호선품목그룹.SettingVersion = "0.0.0.1";
            this._flex담당호선품목그룹.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex담당호선품목그룹.SetExceptSumCol("RT_SO", "RT_PROFIT");
            #endregion

            #region 월별
            this._flex담당호선월별.BeginSetting(1, 1, false);

            this._flex담당호선월별.SetCol("DT_QTN", "견적일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH);
            this._flex담당호선월별.SetCol("QT_QTN", "견적건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex담당호선월별.SetCol("QT_SO", "수주건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex담당호선월별.SetCol("RT_SO", "수주율", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex담당호선월별.SetCol("AM_QTN", "견적금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex담당호선월별.SetCol("AM_SO", "수주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex담당호선월별.SetCol("AM_PO", "발주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex담당호선월별.SetCol("AM_STOCK", "재고금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex담당호선월별.SetCol("AM_PROFIT", "이윤", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex담당호선월별.SetCol("RT_PROFIT", "이윤율", 100, false, typeof(decimal), FormatTpType.RATE);

            this._flex담당호선월별.SettingVersion = "0.0.0.1";
            this._flex담당호선월별.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex담당호선월별.SetExceptSumCol("RT_SO", "RT_PROFIT");
            #endregion

            #region 매입처
            this._flex담당호선매입처.BeginSetting(1, 1, false);

            this._flex담당호선매입처.SetCol("LN_PARTNER", "매입처", 120);
            this._flex담당호선매입처.SetCol("AM_INQ", "문의금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex담당호선매입처.SetCol("AM_PO", "발주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex담당호선매입처.SetCol("AM_STOCK", "재고금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex담당호선매입처.SetCol("QT_INQ", "문의건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex담당호선매입처.SetCol("QT_PO", "발주건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex담당호선매입처.SetCol("QT_STOCK", "재고건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex담당호선매입처.SetCol("RT_PO", "발주율", 100, false, typeof(decimal), FormatTpType.RATE);

            this._flex담당호선매입처.SettingVersion = "0.0.0.1";
            this._flex담당호선매입처.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex담당호선매입처.SetExceptSumCol("RT_PO");
            #endregion
            
            #endregion

            #region 영업활동
            this._flex영업활동.BeginSetting(1, 1, false);

            this._flex영업활동.SetCol("NM_TITLE", "제목", 100);
            this._flex영업활동.SetCol("DT_START", "시작일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex영업활동.SetCol("DT_END", "종료일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex영업활동.SetCol("NM_PIC", "거래처담당자", 100);
            this._flex영업활동.SetCol("NM_EMP", "사내담당자", 100);
            this._flex영업활동.SetCol("DC_ACTIVITY", "내용", 100);

            this._flex영업활동.ExtendLastCol = true;

            this._flex영업활동.SettingVersion = "0.0.0.1";
            this._flex영업활동.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region 미팅메모
            this._flex미팅메모.BeginSetting(1, 1, false);

            this._flex미팅메모.SetCol("NO_MEETING", "미팅번호", 80);
            this._flex미팅메모.SetCol("DT_MEETING", "미팅일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex미팅메모.SetCol("DC_LOCATION", "장소", 100);
            this._flex미팅메모.SetCol("DC_SUBJECT", "주제", 100);
            this._flex미팅메모.SetCol("DC_PURPOSE", "목적", 100);
            this._flex미팅메모.SetCol("DC_MEETING", "미팅내용", 100);

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
            this._flex커미션.SetCol("NM_EXCH", "통화명", 80);
            this._flex커미션.SetCol("AM_COMMISSION", "지급금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex커미션.SetCol("DC_COMMISSION", "내용", 100);

            this._flex커미션.ExtendLastCol = true;

            this._flex커미션.SettingVersion = "0.0.0.1";
            this._flex커미션.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region 물류서비스
            this._flex물류서비스.BeginSetting(1, 1, false);

            this._flex물류서비스.SetCol("DT_LOG", "조사일자", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex물류서비스.SetCol("DC_CONTENTS", "내용", 200, true);
            this._flex물류서비스.SetCol("QT_RATE", "점수", 80, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flex물류서비스.SetCol("FILE_PATH_MNG", "첨부파일", 80);
            this._flex물류서비스.SetCol("DC_RMK", "비고", 200, true);

            this._flex물류서비스.SetDummyColumn(new string[] { "FILE_PATH_MNG" });

            this._flex물류서비스.ExtendLastCol = true;

            this._flex물류서비스.SettingVersion = "0.0.0.1";
            this._flex물류서비스.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion
        }

        private void InitEvent()
        {
            this._flex담당자.AfterRowChange += new RangeEventHandler(this._flex담당자_AfterRowChange);
            this._flex담당호선.AfterRowChange += new RangeEventHandler(this._flex담당호선_AfterRowChange);
            this._flex담당자.OwnerDrawCell += new OwnerDrawCellEventHandler(this._flex담당자_OwnerDrawCell);
            this._flex담당자.DoubleClick += new EventHandler(this._flex담당자_DoubleClick);
            this._flex관련인물.AfterRowChange += new RangeEventHandler(this._flex관련인물_AfterRowChange);
            this._flex관련인물.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this.Grid_BeforeCodeHelp);
            this._flex관련인물.AfterCodeHelp += new AfterCodeHelpEventHandler(this._flex관련인물_AfterCodeHelp);
            this._flex담당호선.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this.Grid_BeforeCodeHelp);
            this._flex미팅메모.DoubleClick += new EventHandler(this._flex미팅메모_DoubleClick);
            this._flex커미션.DoubleClick += new EventHandler(this._flex커미션_DoubleClick);
            this._flex물류서비스.DoubleClick += new EventHandler(this._flex물류서비스_DoubleClick);
            this._flex물류서비스.AfterDataRefresh += new ListChangedEventHandler(this._flex_AfterDataRefresh);

            this.ctx국가.QueryBefore += new BpQueryHandler(this.ctx국가_QueryBefore);
            this.dtp생년월일.TextChanged += new EventHandler(this.dtp생년월일_TextChanged);
            this.cbo중요도.DrawItem += new DrawItemEventHandler(this.cbo중요도_DrawItem);
            this.cbo관련인물중요도.DrawItem += new DrawItemEventHandler(this.cbo중요도_DrawItem);
            this.cbo지급통계.SelectionChangeCommitted += new EventHandler(this.cbo지급통계_SelectionChangeCommitted);

            this.btn거래처담당자수정.Click += new EventHandler(this.btn거래처담당자수정_Click);   
            this.btn부가정보추가.Click += new EventHandler(this.btn부가정보추가_Click);
            this.btn부가정보삭제.Click += new EventHandler(this.btn부가정보삭제_Click);
            this.btn관련인물추가.Click += new EventHandler(this.btn관련인물추가_Click);
            this.btn관련인물삭제.Click += new EventHandler(this.btn관련인물삭제_Click);
            this.btn근무이력추가.Click += new EventHandler(this.btn근무이력추가_Click);
            this.btn근무이력삭제.Click += new EventHandler(this.btn근무이력삭제_Click);
            this.btn담당호선추가.Click += new EventHandler(this.btn담당호선추가_Click);
            this.btn담당호선삭제.Click += new EventHandler(this.btn담당호선삭제_Click);
            this.btn물류서비스추가.Click += new EventHandler(this.btn물류서비스추가_Click);
            this.btn물류서비스삭제.Click += new EventHandler(this.btn물류서비스삭제_Click);
            this.btn담당호선리스트조회.Click += new EventHandler(this.btn담당호선리스트조회_Click);
            this.btn첨부파일.Click += new EventHandler(this.btn첨부파일_Click);
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.spl좌측.SplitterDistance = 525;
            this.spl부가정보.SplitterDistance = 364;
            this.spl담당호선.SplitterDistance = 225;
            this.spl인물관계도.SplitterDistance = 659;
            this.spl커미션.SplitterDistance = 747;

            this.dtp담당호선리스트.StartDateToString = Global.MainFrame.GetDateTimeToday().Year.ToString() + "0101";
            this.dtp담당호선리스트.EndDateToString = Global.MainFrame.GetStringToday;

            DataTable dt중요도 = MA.GetCodeUser(new string[] { "001", 
                                                               "002",
                                                               "003",
                                                               "004",
                                                               "005" }, new string[] { "★☆☆☆☆",
                                                                                       "★★☆☆☆",
                                                                                       "★★★☆☆",
                                                                                       "★★★★☆",
                                                                                       "★★★★★" }, true);

            this.cbo중요도.DataSource = dt중요도;
            this.cbo중요도.ValueMember = "CODE";
            this.cbo중요도.DisplayMember = "NAME";

            this.cbo관련인물중요도.DataSource = dt중요도.Copy();
            this.cbo관련인물중요도.ValueMember = "CODE";
            this.cbo관련인물중요도.DisplayMember = "NAME";

            this.cbo지급통계.DataSource = MA.GetCodeUser(new string[] { "000", "001", "002" }, new string[] { "년단위", "월단위", "일단위" });
            this.cbo지급통계.ValueMember = "CODE";
            this.cbo지급통계.DisplayMember = "NAME";
            this.cbo지급통계.SelectedValue = "000";

            this._flex담당자.SetDataMap("CD_RANK", dt중요도.Copy(), "CODE", "NAME");
            this._flex관련인물.SetDataMap("CD_RANK", dt중요도.Copy(), "CODE", "NAME");

            this.cbo거래처분류.DataSource = Global.MainFrame.GetComboDataCombine("S;MA_B000003");
            this.cbo거래처분류.ValueMember = "CODE";
            this.cbo거래처분류.DisplayMember = "NAME";

            this.cbo성별.DataSource = Global.MainFrame.GetComboDataCombine("S;HR_H000001");
            this.cbo성별.ValueMember = "CODE";
            this.cbo성별.DisplayMember = "NAME";

            this.cbo종교.DataSource = Global.MainFrame.GetComboDataCombine("S;HR_H000031");
            this.cbo종교.ValueMember = "CODE";
            this.cbo종교.DisplayMember = "NAME";

            #region 차트설정

            #region 담당호선비중
            this.chart담당호선비중.ChartFx.Gallery = Gallery.Pie;
            this.chart담당호선비중.ChartFx.View3D.Enabled = true;
            this.chart담당호선비중.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Number;
            this.chart담당호선비중.ChartFx.AxisX.AutoScroll = true;
            this.chart담당호선비중.ChartFx.LegendBox.Visible = true;
            #endregion

            #region 담당호선실적
            this.chart담당호선실적.ChartFx.Gallery = Gallery.Bar;
            this.chart담당호선실적.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Currency;
            this.chart담당호선실적.ChartFx.AxisX.AutoScroll = true;
            this.chart담당호선실적.ChartFx.LegendBox.Visible = true;

            this.chart담당호선실적.ChartFx.Panes.Add(new Pane());
            this.chart담당호선실적.ChartFx.Panes[1].AxisY.LabelsFormat.Format = AxisFormat.Number;
            #endregion

            #region 담당호선월별
            this.chart담당호선월별.ChartFx.Gallery = Gallery.Lines;
            this.chart담당호선월별.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Currency;
            this.chart담당호선월별.ChartFx.AxisX.AutoScroll = true;
            this.chart담당호선월별.ChartFx.LegendBox.Visible = true;

            this.chart담당호선월별.ChartFx.Panes.Add(new Pane());
            this.chart담당호선월별.ChartFx.Panes[1].AxisY.LabelsFormat.Format = AxisFormat.Number;
            #endregion

            #region 담당호선매입처
            this.chart담당호선매입처.ChartFx.Gallery = Gallery.Bar;
            this.chart담당호선매입처.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Currency;
            this.chart담당호선매입처.ChartFx.AxisX.MaxSizePercentage = 20;
            this.chart담당호선매입처.ChartFx.AxisX.AutoScroll = true;
            this.chart담당호선매입처.ChartFx.LegendBox.Visible = true;
            #endregion

            #region 커미션지급통계
            this.chart지급통계.ChartFx.Gallery = Gallery.Bar;
            this.chart지급통계.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Number;
            this.chart지급통계.ChartFx.AxisX.AutoScroll = true;
            this.chart지급통계.ChartFx.LegendBox.Visible = true;
            this.chart지급통계.ChartFx.DataGrid.Visible = true;
            #endregion

            #endregion

            if (!string.IsNullOrEmpty(this.ctx거래처S.CodeValue))
                this.OnToolBarSearchButtonClicked(null, null);
        }
        #endregion

        #region 메인버튼 이벤트
        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!BeforeSearch()) return;

                this._flex담당자.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                           this.cbo거래처분류.SelectedValue.ToString(),
                                                                           this.ctx거래처S.CodeValue,
                                                                           this.txt담당자명.Text });

                if (!this._flex담당자.HasNormalRow)
                    ShowMessage(공통메세지.조건에해당하는내용이없습니다);
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

                if (this.MsgAndSave(PageActionMode.Save))
                    this.ShowMessage(PageResultMode.SaveGood);
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
                this._flex부가정보.IsDataChanged == false &&
                this._flex관련인물.IsDataChanged == false &&
                this._flex근무이력.IsDataChanged == false &&
                this._flex담당호선.IsDataChanged == false &&
                this._flex물류서비스.IsDataChanged == false) return false;

            if (!this._biz.Save(this._flex담당자.GetChanges(), 
                                this._flex부가정보.GetChanges(),
                                this._flex관련인물.GetChanges(),
                                this._flex근무이력.GetChanges(),
                                this._flex담당호선.GetChanges(),
                                this._flex물류서비스.GetChanges())) return false;

            this._flex담당자.AcceptChanges();
            this._flex부가정보.AcceptChanges();
            this._flex관련인물.AcceptChanges();
            this._flex근무이력.AcceptChanges();
            this._flex담당호선.AcceptChanges();
            this._flex물류서비스.AcceptChanges();

            return true;
        }
        #endregion

        #region 그리드 이벤트
        private void _flex담당자_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt부가정보, dt관련인물, dt근무이력, dt담당호선, dt영업활동, dt미팅메모, dt커미션, dt물류서비스;
            string key, key1, filter;

            try
            {
                if (!this._flex담당자.HasNormalRow) return;

                #region 이미지설정
                FileInfoCs fileInfoCs = this._fileUploader[D.GetString(this._flex담당자["DC_PHOTO"]), "Upload/P_CZ_FI_PARTNERPTR_SUB/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + D.GetString(this._flex담당자["CD_PARTNER"]) + "/"];

                if (fileInfoCs != null && fileInfoCs.Image != null)
                    this.pic담당자이미지.Image = fileInfoCs.Image;
                else
                    this.pic담당자이미지.Image = global::cz.Properties.Resources.사진;
                #endregion

                #region 거래처분류설정
                this.lbl거래처분류.Text = this._flex담당자["NM_CLS_PARTNER"].ToString();

                switch (D.GetString(this._flex담당자["CLS_PARTNER"]))
                {
                    case "005":
                        this.lbl거래처분류.BackColor = Color.Blue;
                        this.lbl거래처분류.ForeColor = Color.White;
                        break;
                    case "006":
                        this.lbl거래처분류.BackColor = Color.Red;
                        this.lbl거래처분류.ForeColor = Color.White;
                        break;
                    case "007":
                        this.lbl거래처분류.BackColor = Color.Green;
                        this.lbl거래처분류.ForeColor = Color.White;
                        break;
                    case "008":
                        this.lbl거래처분류.BackColor = Color.Orange;
                        this.lbl거래처분류.ForeColor = Color.White;
                        break;
                    case "009":
                        this.lbl거래처분류.BackColor = Color.Navy;
                        this.lbl거래처분류.ForeColor = Color.White;
                        break;
                    case "010":
                        this.lbl거래처분류.BackColor = Color.Purple;
                        this.lbl거래처분류.ForeColor = Color.White;
                        break;
                    default:
                        break;
                }
                #endregion

                this.나이계산();

                dt관련인물 = null;
                dt부가정보 = null;
                dt근무이력 = null;
                dt담당호선 = null;
                dt영업활동 = null;
                dt미팅메모 = null;
                dt커미션 = null;
                dt물류서비스 = null;

                key = D.GetString(this._flex담당자["CD_PARTNER"]);
                key1 = D.GetString(this._flex담당자["SEQ"]);

                filter = "CD_PARTNER = '" + key + "' AND SEQ = '" + key1 + "'";

                if (this._flex담당자.DetailQueryNeed == true)
                {
                    dt부가정보 = this._biz.Search부가정보(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                         key,
                                                                         key1 });

                    dt관련인물 = this._biz.Search관련인물(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                         key,
                                                                         key1 });

                    dt근무이력 = this._biz.Search근무이력(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                         key,
                                                                         key1 });

                    dt담당호선 = this._biz.Search담당호선(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                         key,
                                                                         key1 });

                    dt영업활동 = this._biz.Search영업활동(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                         key,
                                                                         key1 });

                    dt미팅메모 = this._biz.Search미팅메모(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                         key,
                                                                         key1 });

                    dt커미션 = this._biz.Search커미션(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                     key,
                                                                     key1 });

                    dt물류서비스 = this._biz.Search물류서비스(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                             key,
                                                                             key1 });
                }

                this._flex부가정보.BindingAdd(dt부가정보, filter);
                this._flex관련인물.BindingAdd(dt관련인물, filter);
                this._flex근무이력.BindingAdd(dt근무이력, filter);
                this._flex담당호선.BindingAdd(dt담당호선, filter);
                this._flex영업활동.BindingAdd(dt영업활동, filter);
                this._flex미팅메모.BindingAdd(dt미팅메모, filter);
                this._flex커미션.BindingAdd(dt커미션, filter);
                this._flex물류서비스.BindingAdd(dt물류서비스, filter);

                this.cbo지급통계_SelectionChangeCommitted(null, null);

                this.관련인물관계도생성();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex담당호선_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                this.btn담당호선리스트조회_Click(null, null);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex관련인물_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (this._flex관련인물["YN_AUTO"].ToString() == "Y")
                    this._flex관련인물.Cols["DC_RMK"].AllowEditing = false;
                else
                    this._flex관련인물.Cols["DC_RMK"].AllowEditing = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex담당자_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            try
            {
                if (!this._flex담당자.HasNormalRow) return;

                CellStyle cellStyle = this._flex담당자.Rows[e.Row].Style;

                switch (D.GetString(this._flex담당자[e.Row, "CLS_PARTNER"]))
                {
                    case "005":
                        if (cellStyle == null || cellStyle.Name != "매입처")
                            this._flex담당자.Rows[e.Row].Style = this._flex담당자.Styles["매입처"];
                        break;
                    case "006":
                        if (cellStyle == null || cellStyle.Name != "매출처")
                            this._flex담당자.Rows[e.Row].Style = this._flex담당자.Styles["매출처"];
                        break;
                    case "007":
                        if (cellStyle == null || cellStyle.Name != "매입매출")
                            this._flex담당자.Rows[e.Row].Style = this._flex담당자.Styles["매입매출"];
                        break;
                    case "008":
                        if (cellStyle == null || cellStyle.Name != "포워더")
                            this._flex담당자.Rows[e.Row].Style = this._flex담당자.Styles["포워더"];
                        break;
                    case "009":
                        if (cellStyle == null || cellStyle.Name != "관리")
                            this._flex담당자.Rows[e.Row].Style = this._flex담당자.Styles["관리"];
                        break;
                    case "010":
                        if (cellStyle == null || cellStyle.Name != "기타")
                            this._flex담당자.Rows[e.Row].Style = this._flex담당자.Styles["기타"];
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
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

        public void _flex물류서비스_DoubleClick(object sender, EventArgs e)
        {
            string fileCode, fileName;

            try
            {
                if (!this._flex물류서비스.HasNormalRow) return;
                if (this._flex물류서비스.MouseRow < this._flex물류서비스.Rows.Fixed) return;
                if (this._flex물류서비스.RowState() != DataRowState.Unchanged) return;

                if (this._flex물류서비스.Cols[_flex물류서비스.Col].Name == "FILE_PATH_MNG")
                {
                    fileCode = D.GetString(this._flex물류서비스["CD_COMPANY"]) + "_" + D.GetString(this._flex물류서비스["CD_PARTNER"]) + "_" + D.GetString(this._flex물류서비스["SEQ"]) + "_" + D.GetString(this._flex물류서비스["NO_INDEX"]);
                    P_CZ_MA_FILE_SUB dlg = new P_CZ_MA_FILE_SUB("K100", "SA", "P_CZ_SA_CRM_PARTNER_PIC_LOG", fileCode, "P_CZ_SA_CRM_PARTNER_PIC_LOG" + "/" + D.GetString(this._flex물류서비스["CD_COMPANY"]) + "/" + D.GetString(this._flex물류서비스["CD_PARTNER"]) + "/" + D.GetString(this._flex물류서비스["SEQ"]) + "/" + D.GetString(this._flex물류서비스["NO_INDEX"]));
                    dlg.ShowDialog(this);

                    fileName = this._biz.SearchFileInfo("P_CZ_SA_CRM_PARTNER_PIC_LOG", fileCode);

                    if (!string.IsNullOrEmpty(fileName))
                    {
                        this._flex물류서비스["FILE_PATH_MNG"] = fileName;
                        this._flex물류서비스.SetCellImage(this._flex물류서비스.Row, this._flex물류서비스.Cols["FILE_PATH_MNG"].Index, global::cz.Properties.Resources.FILE_ICON);
                    }
                    else
                    {
                        this._flex물류서비스["FILE_PATH_MNG"] = string.Empty;
                        this._flex물류서비스.SetCellImage(this._flex물류서비스.Row, this._flex물류서비스.Cols["FILE_PATH_MNG"].Index, null);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public void _flex담당자_DoubleClick(object sender, EventArgs e)
        {
            string fileCode, fileName;

            try
            {
                if (!this._flex담당자.HasNormalRow) return;
                if (this._flex담당자.MouseRow < this._flex담당자.Rows.Fixed) return;
                if (this._flex담당자.RowState() != DataRowState.Unchanged) return;

                if (this._flex담당자.Cols[_flex담당자.Col].Name == "FILE_PATH_MNG")
                {
                    fileCode = D.GetString(this._flex담당자["CD_COMPANY"]) + "_" + D.GetString(this._flex담당자["CD_PARTNER"]) + "_" + D.GetString(this._flex담당자["SEQ"]);
                    P_CZ_MA_FILE_SUB dlg = new P_CZ_MA_FILE_SUB("K100", "SA", "P_CZ_SA_CRM_PARTNER_PIC", fileCode, "P_CZ_SA_CRM_PARTNER_PIC" + "/" + D.GetString(this._flex담당자["CD_COMPANY"]) + "/" + D.GetString(this._flex담당자["CD_PARTNER"]) + "/" + D.GetString(this._flex담당자["SEQ"]));
                    dlg.ShowDialog(this);

                    fileName = this._biz.SearchFileInfo("P_CZ_SA_CRM_PARTNER_PIC", fileCode);

                    if (!string.IsNullOrEmpty(fileName))
                    {
                        this._flex담당자["FILE_PATH_MNG"] = fileName;
                        this._flex담당자.SetCellImage(this._flex담당자.Row, this._flex담당자.Cols["FILE_PATH_MNG"].Index, global::cz.Properties.Resources.FILE_ICON);
                    }
                    else
                    {
                        this._flex담당자["FILE_PATH_MNG"] = string.Empty;
                        this._flex담당자.SetCellImage(this._flex담당자.Row, this._flex담당자.Cols["FILE_PATH_MNG"].Index, null);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void Grid_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            string name;

            try
            {
                name = ((Control)sender).Name;

                if (name == this._flex관련인물.Name)
                {
                    if (string.IsNullOrEmpty(D.GetString(this._flex담당자["CD_PARTNER"])))
                        return;

                    e.Parameter.UserParams = "관련인물;H_CZ_MA_PARTNERPTR_SUB";
                    e.Parameter.P14_CD_PARTNER = D.GetString(this._flex담당자["CD_PARTNER"]);
                    e.Parameter.P34_CD_MNG = D.GetString(this._flex담당자["LN_PARTNER"]);
                    e.Parameter.P35_CD_MNGD = "Y";
                }
                else if (name == this._flex담당호선.Name)
                {
                    e.Parameter.UserParams = "담당호선;H_CZ_MA_HULL_SUB";
                    e.Parameter.P14_CD_PARTNER = D.GetString(this._flex담당자["CD_PARTNER"]);
                    e.Parameter.P34_CD_MNG = D.GetString(this._flex담당자["LN_PARTNER"]);
                    e.Parameter.P35_CD_MNGD = "Y";
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex관련인물_AfterCodeHelp(object sender, AfterCodeHelpEventArgs e)
        {
            try
            {
                if (e.Result.Rows[0]["CD_PARTNER"].ToString() == this._flex담당자["CD_PARTNER"].ToString() &&
                    e.Result.Rows[0]["SEQ"].ToString() == this._flex담당자["SEQ"].ToString())
                {
                    this.ShowMessage("동일한 인물은 추가 할 수 없습니다.");
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_AfterDataRefresh(object sender, ListChangedEventArgs e)
        {
            FlexGrid grid;

            try
            {
                grid = ((FlexGrid)sender);
                if (!grid.HasNormalRow) return;

                if (grid.Name == this._flex담당자.Name)
                {
                    int rowIndex = 1;
                    int columnIndex = this._flex담당자.Cols["FILE_PATH_MNG"].Index;

                    for (rowIndex = this._flex담당자.Rows.Fixed; rowIndex < this._flex담당자.Rows.Count; rowIndex++)
                    {
                        if (!string.IsNullOrEmpty(D.GetString(this._flex담당자.Rows[rowIndex]["FILE_PATH_MNG"])))
                            this._flex담당자.SetCellImage(rowIndex, columnIndex, global::cz.Properties.Resources.FILE_ICON);
                        else
                            this._flex담당자.SetCellImage(rowIndex, columnIndex, null);
                    }
                }
                else if (grid.Name == this._flex물류서비스.Name)
                {
                    int rowIndex = 1;
                    int columnIndex = this._flex물류서비스.Cols["FILE_PATH_MNG"].Index;

                    for (rowIndex = this._flex물류서비스.Rows.Fixed; rowIndex < this._flex물류서비스.Rows.Count; rowIndex++)
                    {
                        if (!string.IsNullOrEmpty(D.GetString(this._flex물류서비스.Rows[rowIndex]["FILE_PATH_MNG"])))
                            this._flex물류서비스.SetCellImage(rowIndex, columnIndex, global::cz.Properties.Resources.FILE_ICON);
                        else
                            this._flex물류서비스.SetCellImage(rowIndex, columnIndex, null);
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region 컨트롤 이벤트
        private void cbo중요도_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox comboBox;
            string code, name;
            Brush brush;

            try
            {
                Graphics g = e.Graphics;
                Rectangle rect = e.Bounds;
                comboBox = ((ComboBox)sender);

                if (e.Index >= 0)
                {
                    code = ((DataRowView)(comboBox.Items[e.Index])).Row["CODE"].ToString();
                    name = ((DataRowView)(comboBox.Items[e.Index])).Row["NAME"].ToString();

                    switch(code)
                    {
                        case "001":
                            brush = Brushes.Blue;
                            break;
                        case "002":
                            brush = Brushes.Green;
                            break;
                        case "003":
                            brush = Brushes.Gold;
                            break;
                        case "004":
                            brush = Brushes.Orange;
                            break;
                        case "005":
                            brush = Brushes.Red;
                            break;
                        default:
                            brush = Brushes.Black;
                            break;
                    }

                    g.DrawString(name, new Font("Arial", 13, FontStyle.Regular), brush, rect.X, rect.Top);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void cbo지급통계_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DataTable dt;
            DataRow dr;
            Dictionary<string, decimal> tempDic;

            try
            {
                if (!this._flex커미션.HasNormalRow) return;

                dt = new DataTable();

                dt.Columns.Add("DT_COMMISSION", typeof(string));
                dt.Columns.Add("AM_COMMISSION", typeof(decimal));

                switch(this.cbo지급통계.SelectedValue.ToString())
                {
                    case "000":
                        tempDic = this._flex커미션.DataTable
                                                  .AsEnumerable()
                                                  .GroupBy(x => x["DT_COMMISSION"].ToString().Substring(0, 4), y => D.GetDecimal(y["AM_COMMISSION"]), (x, y) => new { key = x, value = y.Sum() })
                                                  .ToDictionary(x => x.key, y => y.value);
                        break;
                    case "001":
                        tempDic = this._flex커미션.DataTable
                                                  .AsEnumerable()
                                                  .GroupBy(x => x["DT_COMMISSION"].ToString().Substring(0, 6), y => D.GetDecimal(y["AM_COMMISSION"]), (x, y) => new { key = x, value = y.Sum() })
                                                  .ToDictionary(x => x.key, y => y.value);
                        break;
                    case "002":
                        tempDic = this._flex커미션.DataTable
                                                  .AsEnumerable()
                                                  .GroupBy(x => x["DT_COMMISSION"].ToString(), y => D.GetDecimal(y["AM_COMMISSION"]), (x, y) => new { key = x, value = y.Sum() })
                                                  .ToDictionary(x => x.key, y => y.value);
                        break;
                    default:
                        return;
                }

                foreach (KeyValuePair<string, decimal> kvp in tempDic)
                {
                    dr = dt.NewRow();
                    dr["DT_COMMISSION"] = kvp.Key;
                    dr["AM_COMMISSION"] = kvp.Value;
                    dt.Rows.Add(dr);
                }

                this.chart지급통계.DataSource = dt;

                this.chart지급통계.ChartFx.Series[0].Text = "금액";
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void dtp생년월일_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.dtp생년월일.IsValidated) return;

                this.나이계산();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void ctx국가_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                e.HelpParam.P41_CD_FIELD1 = "MA_B000020";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn거래처담당자수정_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this._flex담당자["CD_PARTNER"].ToString()))
                    return;

                P_CZ_FI_PARTNERPTR_SUB dialog = new P_CZ_FI_PARTNERPTR_SUB(this._flex담당자["CD_PARTNER"].ToString());
                if (dialog.ShowDialog() == DialogResult.OK)
                    this.OnToolBarSearchButtonClicked(null, null);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn부가정보추가_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex담당자.HasNormalRow) return;

                this._flex부가정보.Rows.Add();
                this._flex부가정보.Row = this._flex부가정보.Rows.Count - 1;

                this._flex부가정보["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                this._flex부가정보["CD_PARTNER"] = this._flex담당자["CD_PARTNER"];
                this._flex부가정보["SEQ"] = this._flex담당자["SEQ"];
                this._flex부가정보["CD_ITEM"] = (D.GetInt(this._flex부가정보.DataTable.Compute("MAX(CD_ITEM)", string.Empty)) + 1).ToString("D3");

                this._flex부가정보.AddFinished();
                this._flex부가정보.Col = this._flex부가정보.Cols.Fixed;
                this._flex부가정보.Focus();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn부가정보삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex부가정보.HasNormalRow) return;

                this._flex부가정보.GetDataRow(this._flex부가정보.Row).Delete();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn관련인물추가_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex담당자.HasNormalRow) return;

                this._flex관련인물.Rows.Add();
                this._flex관련인물.Row = this._flex관련인물.Rows.Count - 1;

                this._flex관련인물["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                this._flex관련인물["CD_PARTNER"] = this._flex담당자["CD_PARTNER"];
                this._flex관련인물["SEQ"] = this._flex담당자["SEQ"];

                this._flex관련인물.AddFinished();
                this._flex관련인물.Col = this._flex관련인물.Cols.Fixed;
                this._flex관련인물.Focus();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn관련인물삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex관련인물.HasNormalRow) return;
                if (this._flex관련인물["YN_AUTO"].ToString() == "Y")
                {
                    this.ShowMessage("자동등록된 건은 등록한 담당자에서만 삭제 가능 합니다.");
                    return;
                }

                this._flex관련인물.GetDataRow(this._flex관련인물.Row).Delete();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn근무이력추가_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex담당자.HasNormalRow) return;

                this._flex근무이력.Rows.Add();
                this._flex근무이력.Row = this._flex근무이력.Rows.Count - 1;

                this._flex근무이력["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                this._flex근무이력["CD_PARTNER"] = this._flex담당자["CD_PARTNER"];
                this._flex근무이력["SEQ"] = this._flex담당자["SEQ"];
                this._flex근무이력["NO_INDEX"] = (D.GetInt(this._flex근무이력.DataTable.Compute("MAX(NO_INDEX)", string.Empty)) + 1);

                this._flex근무이력.AddFinished();
                this._flex근무이력.Col = this._flex근무이력.Cols.Fixed;
                this._flex근무이력.Focus();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn근무이력삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex근무이력.HasNormalRow) return;

                this._flex근무이력.GetDataRow(this._flex근무이력.Row).Delete();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn담당호선추가_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex담당자.HasNormalRow) return;

                this._flex담당호선.Rows.Add();
                this._flex담당호선.Row = this._flex담당호선.Rows.Count - 1;

                this._flex담당호선["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                this._flex담당호선["CD_PARTNER"] = this._flex담당자["CD_PARTNER"];
                this._flex담당호선["SEQ"] = this._flex담당자["SEQ"];

                this._flex담당호선.AddFinished();
                this._flex담당호선.Col = this._flex담당호선.Cols.Fixed;
                this._flex담당호선.Focus();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn담당호선삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex담당호선.HasNormalRow) return;

                this._flex담당호선.GetDataRow(this._flex담당호선.Row).Delete();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn물류서비스추가_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex담당자.HasNormalRow) return;

                this._flex물류서비스.Rows.Add();
                this._flex물류서비스.Row = this._flex물류서비스.Rows.Count - 1;

                this._flex물류서비스["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                this._flex물류서비스["CD_PARTNER"] = this._flex담당자["CD_PARTNER"];
                this._flex물류서비스["SEQ"] = this._flex담당자["SEQ"];
                this._flex물류서비스["NO_INDEX"] = (D.GetInt(this._flex물류서비스.DataTable.Compute("MAX(NO_INDEX)", string.Empty)) + 1);

                this._flex물류서비스.AddFinished();
                this._flex물류서비스.Col = this._flex물류서비스.Cols.Fixed;
                this._flex물류서비스.Focus();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn물류서비스삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex물류서비스.HasNormalRow) return;

                this._flex물류서비스.GetDataRow(this._flex물류서비스.Row).Delete();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn담당호선리스트조회_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex담당호선.HasNormalRow) return;

                this._flex담당호선품목그룹.Binding = this._biz.Search담당호선리스트(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                   this._flex담당호선["NO_IMO"].ToString(),                                                                             
                                                                                                   this.dtp담당호선리스트.StartDateToString,
                                                                                                   this.dtp담당호선리스트.EndDateToString,
                                                                                                   "001" });

                this._flex담당호선월별.Binding = this._biz.Search담당호선리스트(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                               this._flex담당호선["NO_IMO"].ToString(),                                                                             
                                                                                               this.dtp담당호선리스트.StartDateToString,
                                                                                               this.dtp담당호선리스트.EndDateToString,
                                                                                               "003" });

                this._flex담당호선매입처.Binding = this._biz.Search담당호선리스트(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                 this._flex담당호선["NO_IMO"].ToString(),                                                                             
                                                                                                 this.dtp담당호선리스트.StartDateToString,
                                                                                                 this.dtp담당호선리스트.EndDateToString,
                                                                                                 "002" });

                this.담당호선리스트차트설정();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn첨부파일_Click(object sender, EventArgs e)
        {
            string fileCode, fileName;

            try
            {
                fileCode = D.GetString(this._flex담당자["CD_COMPANY"]) + "_" + D.GetString(this._flex담당자["CD_PARTNER"]) + "_" + D.GetString(this._flex담당자["SEQ"]);
                P_CZ_MA_FILE_SUB dlg = new P_CZ_MA_FILE_SUB("K100", "SA", "P_CZ_SA_CRM_PARTNER_PIC", fileCode, "P_CZ_SA_CRM_PARTNER_PIC" + "/" + D.GetString(this._flex담당자["CD_COMPANY"]) + "/" + D.GetString(this._flex담당자["CD_PARTNER"]) + "/" + D.GetString(this._flex담당자["SEQ"]));
                dlg.ShowDialog(this);

                fileName = this._biz.SearchFileInfo("P_CZ_SA_CRM_PARTNER_PIC", fileCode);

                if (!string.IsNullOrEmpty(fileName))
                {
                    this._flex담당자["FILE_PATH_MNG"] = fileName;
                    this._flex담당자.SetCellImage(this._flex담당자.Row, this._flex담당자.Cols["FILE_PATH_MNG"].Index, global::cz.Properties.Resources.FILE_ICON);
                }
                else
                {
                    this._flex담당자["FILE_PATH_MNG"] = string.Empty;
                    this._flex담당자.SetCellImage(this._flex담당자.Row, this._flex담당자.Cols["FILE_PATH_MNG"].Index, null);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region 기타 메소드
        private void 나이계산()
        {
            try
            {
                if (string.IsNullOrEmpty(this.dtp생년월일.Text))
                {
                    this.txt만나이.Text = string.Empty;
                    return;
                }

                DateTime now = DateTime.Today;
                DateTime birthday = this.dtp생년월일.Value;

                int age = now.Year - birthday.Year;

                if (now.Month < birthday.Month || (now.Month == birthday.Month && now.Day < birthday.Day))
                    age--;

                this.txt만나이.Text = ("만 " + age.ToString() + "세");
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void 담당호선리스트차트설정()
        {
            DataTable tmpDt;

            try
            {
                tmpDt = this._flex담당호선품목그룹.DataTable;

                if (tmpDt == null || tmpDt.Rows.Count == 0)
                {
                    this.chart담당호선비중.ChartFx.Data.Clear();
                    this.chart담당호선실적.ChartFx.Data.Clear();
                }
                else
                {
                    #region 담당호선비중
                    this.chart담당호선비중.DataSource = tmpDt.Copy();

                    this.chart담당호선비중.ChartFx.Series[0].Text = "견적건수";
                    this.chart담당호선비중.ChartFx.Series[1].Text = "수주건수";
                    this.chart담당호선비중.ChartFx.Series[2].Visible = false;
                    this.chart담당호선비중.ChartFx.Series[3].Text = "견적금액";
                    this.chart담당호선비중.ChartFx.Series[4].Text = "수주금액";
                    this.chart담당호선비중.ChartFx.Series[5].Text = "발주금액";
                    this.chart담당호선비중.ChartFx.Series[6].Text = "재고금액";
                    this.chart담당호선비중.ChartFx.Series[7].Text = "이윤";
                    this.chart담당호선비중.ChartFx.Series[8].Visible = false;
                    this.chart담당호선비중.ChartFx.Series[9].Visible = false;
                    #endregion

                    #region 담당호선실적
                    this.chart담당호선실적.DataSource = tmpDt.Copy();

                    this.chart담당호선실적.ChartFx.Series[0].Text = "견적건수";
                    this.chart담당호선실적.ChartFx.Series[1].Text = "수주건수";
                    this.chart담당호선실적.ChartFx.Series[2].Visible = false;
                    this.chart담당호선실적.ChartFx.Series[3].Text = "견적금액";
                    this.chart담당호선실적.ChartFx.Series[4].Text = "수주금액";
                    this.chart담당호선실적.ChartFx.Series[5].Visible = false;
                    this.chart담당호선실적.ChartFx.Series[6].Visible = false;
                    this.chart담당호선실적.ChartFx.Series[7].Visible = false;
                    this.chart담당호선실적.ChartFx.Series[8].Visible = false;
                    this.chart담당호선실적.ChartFx.Series[9].Visible = false;

                    this.chart담당호선실적.ChartFx.Series[0].Pane = this.chart담당호선실적.ChartFx.Panes[1];
                    this.chart담당호선실적.ChartFx.Series[1].Pane = this.chart담당호선실적.ChartFx.Panes[1];
                    #endregion
                }

                tmpDt = this._flex담당호선월별.DataTable;

                if (tmpDt == null || tmpDt.Rows.Count == 0)
                {
                    this.chart담당호선월별.ChartFx.Data.Clear();
                }
                else
                {
                    #region 담당호선월별
                    this.chart담당호선월별.DataSource = tmpDt.Copy();

                    this.chart담당호선월별.ChartFx.Series[0].Text = "견적건수";
                    this.chart담당호선월별.ChartFx.Series[1].Text = "수주건수";
                    this.chart담당호선월별.ChartFx.Series[2].Visible = false;
                    this.chart담당호선월별.ChartFx.Series[3].Text = "견적금액";
                    this.chart담당호선월별.ChartFx.Series[4].Text = "수주금액";
                    this.chart담당호선월별.ChartFx.Series[5].Visible = false;
                    this.chart담당호선월별.ChartFx.Series[6].Visible = false;
                    this.chart담당호선월별.ChartFx.Series[7].Visible = false;
                    this.chart담당호선월별.ChartFx.Series[8].Visible = false;
                    this.chart담당호선월별.ChartFx.Series[9].Visible = false;

                    this.chart담당호선월별.ChartFx.Series[0].Pane = this.chart담당호선월별.ChartFx.Panes[1];
                    this.chart담당호선월별.ChartFx.Series[1].Pane = this.chart담당호선월별.ChartFx.Panes[1];
                    #endregion
                }

                tmpDt = this._flex담당호선매입처.DataTable;

                if (tmpDt == null || tmpDt.Rows.Count == 0)
                {
                    this.chart담당호선매입처.ChartFx.Data.Clear();
                }
                else
                {
                    #region 담당호선매입처
                    this.chart담당호선매입처.DataSource = tmpDt.Copy();

                    this.chart담당호선매입처.ChartFx.Series[0].Visible = false;
                    this.chart담당호선매입처.ChartFx.Series[1].Visible = false;
                    this.chart담당호선매입처.ChartFx.Series[2].Visible = false;
                    this.chart담당호선매입처.ChartFx.Series[3].Text = "문의금액";
                    this.chart담당호선매입처.ChartFx.Series[4].Text = "발주금액";
                    this.chart담당호선매입처.ChartFx.Series[5].Text = "재고금액";
                    this.chart담당호선매입처.ChartFx.Series[6].Visible = false;
                    this.chart담당호선매입처.ChartFx.Series[7].Visible = false;
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void 관련인물관계도생성()
        {
            DataSet ds;
            TreeNode parent;

            try
            {
                ds = this._biz.Search인물관계도(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                               this._flex담당자["CD_PARTNER"].ToString(),
                                                               this._flex담당자["SEQ"].ToString() });

                #region
                this.treeView.Nodes.Clear();

                parent = new TreeNode();
                parent.Text = (this._flex담당자["LN_PARTNER"].ToString() + "-" + this._flex담당자["NM_PTR"].ToString());
                parent.Tag = (this._flex담당자["CD_PARTNER"].ToString() + "-" + this._flex담당자["SEQ"].ToString());

                this.ChkHasSubNode(parent, ds.Tables[0]);

                this.treeView.Nodes.Add(parent);

                this.treeView.ExpandAll();
                #endregion

                #region
                this.treeView1.Nodes.Clear();

                parent = new TreeNode();
                parent.Text = (this._flex담당자["LN_PARTNER"].ToString() + "-" + this._flex담당자["NM_PTR"].ToString());
                parent.Tag = (this._flex담당자["CD_PARTNER"].ToString() + "-" + this._flex담당자["SEQ"].ToString());

                this.ChkHasSubNode(parent, ds.Tables[1]);

                this.treeView1.Nodes.Add(parent);

                this.treeView1.ExpandAll();
                #endregion
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private TreeNode ChkHasSubNode(TreeNode parentNode, DataTable dt)
        {
            foreach(DataRow dr in dt.Select("ID_PARENT = '" + parentNode.Tag + "'"))
            {
                parentNode = this.AddTreeNode(parentNode, dt, dr["ID_CHILD"].ToString(), dr["NAME"].ToString());
            }

            return parentNode;
        }

        private TreeNode AddTreeNode(TreeNode parentNode, DataTable dt, string id, string name)
         {
             TreeNode child = new TreeNode();
             child.Text = name;
             child.Tag = id;
         
             parentNode.Nodes.Add(child);

             if (dt.Select("ID_PARENT = '" + parentNode.Tag + "'").Length > 0)
                 ChkHasSubNode(child, dt);
              
             return parentNode;
         }
        #endregion
    }
}

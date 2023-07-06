using System; 
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.Windows.Print;
using DzHelpFormLib;
using Duzon.ERPU;
using System.Text;
using Duzon.Common.Util;
using Duzon.ERPU.MF;
using Duzon.ERPU.PU.Common;
using Duzon.ERPU.MF.Common;
namespace pur
{
    // **************************************
    //   페이지 : S/L간이동처리
    //   작성자 : 최석제
    // 재작성일 : 2006-09-18
    // 수정내역:
    // 2009.12.28 - 안종호 - 컬럼끝에있을경우에 Enter key로 행 추가
    // 2010.04.30 - 안종호 - 창고 일괄적용 기능 추가
    // 2010.06.10 - 안종호 - 헤더 라인 초기화 데이터테이블 생성 로직 추가
    // 2010.06.21 - 신미란 - Serial 기능 넣기 (김한길/퓨쳐시스템)
    // 2010.08.25 - 안종호 - 엑셀업로드기능추가(김헌섭대리)
    // 2013.07.26 - 윤성우 - ONE GRID 수정(입력 전용)

    // **************************************

    public partial class P_PU_STS_REG : Duzon.Common.Forms.PageBase
    {
        private P_PU_STS_REG_BIZ _biz = null;
        private DataTable _dtH = null;
        private FreeBinding _header = null;
        public string MNG_LOT = string.Empty; //시스템통제등록
        public string MNG_SERIAL = string.Empty; //시스템통제등록
        internal string m_sys_set_SL = string.Empty; //창고통제
        private string fg_sub = string.Empty;
        public bool 입고적용 = false;
        //저장시 CC단에 들어갈 값들
        DataTable _dtReqDataREQ = new DataTable(); // 요청적용에서 받은 테이블
        private bool 프로젝트사용 = false;
        private bool PJT형여부 = false;
        public string _no_io = string.Empty;
        public string _cd_pjt = string.Empty;

        decimal d_SEQ_PROJECT = decimal.Zero;
        string s_CD_PJT_ITEM = string.Empty;
        string s_NM_PJT_ITEM = string.Empty;
        string s_PJT_ITEM_STND = string.Empty;

        #region ♣ 초기화 이벤트 / 메소드

        #region -> 생성자

        public P_PU_STS_REG()
        {
            try
            {
                InitializeComponent();

                this.MainGrids = new FlexGrid[] { _flex };
                _header = new FreeBinding();  // 반드시 InitPaint() 이벤트에서 SetBinding() 메소드로 초기화 해줘야 함

                _header.JobModeChanged += new FreeBindingEventHandler(_header_JobModeChanged);
                _header.ControlValueChanged += new FreeBindingEventHandler(_header_ControlValueChanged);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }


        public P_PU_STS_REG(string p_no_io, string p_cd_pjt)
        {
            try
            {
                InitializeComponent();

                this.MainGrids = new FlexGrid[] { _flex };
                _header = new FreeBinding();  // 반드시 InitPaint() 이벤트에서 SetBinding() 메소드로 초기화 해줘야 함

                _header.JobModeChanged += new FreeBindingEventHandler(_header_JobModeChanged);
                _header.ControlValueChanged += new FreeBindingEventHandler(_header_ControlValueChanged);

                _no_io = p_no_io;
                _cd_pjt = p_cd_pjt;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnCallExistingPageMethod(object sender, Duzon.Common.Forms.PageEventArgs e)
        {
            object[] obj = e.Args;
            _no_io = D.GetString(obj[0]);
            _cd_pjt = D.GetString(obj[1]);


            InitPaint();
        }

        #endregion

        #region -> _header_JobModeChanged

        void _header_JobModeChanged(object sender, FreeBindingArgs e)
        {
            try
            {
                if (e.JobMode == JobModeEnum.추가후수정)
                {
                    _header.SetControlEnabled(true);
                    tb_dc.Enabled = true;
                }
                else
                {
                    _header.SetControlEnabled(false);
                    tb_dc.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> _header_ControlValueChanged

        void _header_ControlValueChanged(object sender, FreeBindingArgs e)
        {
            try
            {
                Page_DataChanged(null, null);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        #region -> InitLoad

        protected override void InitLoad()
        {
            base.InitLoad();

            _biz = new P_PU_STS_REG_BIZ();
            
            DataTable dt = _biz.Search_LOT();
            MNG_LOT =  dt.Rows[0]["MNG_LOT"].ToString();
            MNG_SERIAL =_biz.Search_SERIAL();
            m_sys_set_SL = BASIC.GetMAEXC("S/L간이동처리-창고권한통제");
            프로젝트사용 = Config.MA_ENV.프로젝트사용;
            PJT형여부 = Config.MA_ENV.PJT형여부 == "Y" ? true : false;

            if (Global.MainFrame.ServerKeyCommon == "ASAN" || Global.MainFrame.ServerKeyCommon == "ASUNG" || Global.MainFrame.ServerKeyCommon == "DZSQL" || Global.MainFrame.ServerKeyCommon == "SQL_")
            {
                btnD2계획적용.Visible = true;
                btn수주적용.Visible = true;
            }
            
            InitGrid();
        }

        #endregion

        #region -> InitGrid

        void InitGrid()
        {
            _flex.BeginSetting(1, 1, true);
            _flex.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            _flex.SetCol("CD_ITEM", "품목코드", 120, true);
            _flex.SetCol("NM_ITEM", "품목명", 150, false);
            _flex.SetCol("STND_ITEM", "규격", 150, false);
            _flex.SetCol("UNIT_IM", "단위", 80, false);
            _flex.SetCol("NO_DESIGN", "도면번호", 100, true, typeof(string));
            // 2014/02/12 이동수량(발주단위) 컬럼 추가
            _flex.SetCol("UNIT_PO", "발주단위", 80, false);
            _flex.SetCol("NM_ITEMGRP", "품목군", 80, false);
            _flex.SetCol("FG_SERNO", "S/N,LOT관리", 80, false);
            _flex.SetCol("NO_LOT", "LOT여부", 100, false);
            _flex.SetCol("QT_GOOD_INV", "이동수량", 120, true, typeof(decimal), FormatTpType.QUANTITY);
            // 2014/02/12 이동수량(발주단위) 컬럼 추가
            _flex.SetCol("QT_UNIT_PO", "이동수량(발주단위)", 140, true, typeof(decimal), FormatTpType.QUANTITY);
            _flex.SetCol("출고창고", "출고창고", 100, true);
            _flex.SetCol("입고창고", "입고창고", 100, true);
            // 2011/01/31 현재고 컬럼 추가
            _flex.SetCol("QT_GOODS", "현재고", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex.SetCol("DC_RMK", "라인비고", 150, true);
            _flex.SetCol("CD_ZONE", "LOCATION", 120, false); // 2011.05.03 추가(최규원)
            _flex.SetCol("DC_RMK1", "라인비고1", 150, true);

            if (프로젝트사용)
            {
                _flex.SetCol("CD_PROJECT", "프로젝트", 120, true, typeof(string));
                _flex.SetCol("NM_PROJECT", "프로젝트명", 120, false, typeof(string));

                if (PJT형여부)
                {
                    _flex.SetCol("SEQ_PROJECT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 항번" : "프로젝트항번", 100, false, typeof(decimal));
                    _flex.SetCol("CD_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 코드" : "프로젝트품목", 120, true, typeof(string));
                    _flex.SetCol("NM_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 명" : "프로젝트품목명", 120, false, typeof(string));
                    _flex.SetCol("STND_UNIT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 규격" : "프로젝트품목규격", 100, false, typeof(string));
                    _flex.SetCol("NO_WBS", "WBS번호", 100, false, typeof(string));
                    _flex.SetCol("NO_CBS", "CBS번호", 100, false, typeof(string));
                }
            }

            _flex.SetCol("NO_ISURCV", "요청번호", 120, false, typeof(string));
            _flex.SetCol("NO_PSO_MGMT", "작업지시번호", 120, false, typeof(string));
            _flex.SetCol("BARCODE", "BARCODE", 120, false, typeof(string));
            _flex.SetCol("QT_GIREQ", "요청수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex.SetCol("FG_SLQC", "검사(이동)", 40, false, CheckTypeEnum.Y_N);
            _flex.SetCol("CLS_ITEM", "계정구분", 80, false);
            _flex.SetCol("NO_EMP", "요청자", 80, true);
            _flex.SetCol("요청자", "요청자명", 80, false);
            _flex.SetCol("요청부서", "요청부서", 80, false);
            _flex.SetCol("FG_GUBUN", "요청구분", 80, false);
            _flex.SetCol("LN_PARTNER", "외주거래처", false);
            _flex.SetCol("NM_MAKER", "MAKER", 100, false, typeof(string));
           
            _flex.SetCol("NM_CLS_L", "대분류", false);
            _flex.SetCol("NM_CLS_M", "중분류", false);
            _flex.SetCol("NM_CLS_S", "소분류", false);

            _flex.SetCol("GI_PARTNER", "납품처코드", 100, false);
            _flex.SetCol("LN_GI_PARTNER", "납품처명", 100, false);

            if (Global.MainFrame.ServerKeyCommon.Contains("HANSU"))
            {
                _flex.SetCol("CD_ITEM_PARTNER", "전용코드", 100, false);
                _flex.SetCol("NM_ITEM_PARTNER", "전용품명", 100, false);
                _flex.SetCol("CD_PACK", "포장단위", 100, false);
                _flex.SetCol("NM_PACK", "포장단위명", 100, false);
                _flex.SetCol("CD_PART", "납품부서코드", 100, false);
                _flex.SetCol("NM_PART", "납품부서명", 100, false);
                _flex.SetCol("YN_TEST_RPT", "성적서여부", 100, false); // cd_userdef1
                _flex.SetCol("DT_DELIVERY", "납기요구일", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                _flex.SetCol("CD_TRANSPORT", "운송방법", 100, false); // cd_userdef2
                _flex.SetCol("DC_DESTINATION", "목적지", 100, false); // cd_userdef3
                _flex.SetCol("DC_RMK_REQ", "요청비고", 200, 100, false, typeof(string));
                _flex.SetCol("CD_PARTNER", "매출처코드", 100, false);
                _flex.SetCol("PRIOR_GUBUN", "선입구분", 100, false);
                _flex.SetCol("NUM_USERDEF1", "선입수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                _flex.Cols["LN_PARTNER"].Caption = "매출처";



                //_flex.SetCodeHelpCol("CD_ITEM_PARTNER", "H_SA_Z_HANSU_ITEM_CD_SUB", ShowHelpEnum.Always,
                //                     new string[] { "CD_ITEM_PARTNER", "NM_ITEM_PARTNER" },
                //                     new string[] { "CD_ITEM_PARTNER", "NM_ITEM_PARTNER" },
                //                     new string[] { "CD_ITEM_PARTNER", "NM_ITEM_PARTNER" }, ResultMode.SlowMode);

                //_flex.SetCodeHelpCol("CD_PACKUNIT", HelpID.P_MA_PITEM_SUB, ShowHelpEnum.Always,
                //                     new string[] { "CD_PACK", "NM_PACK" },
                //                     new string[] { "CD_ITEM", "NM_ITEM" },
                //                     new string[] { "CD_PACK", "NM_PACK" }, ResultMode.SlowMode);
              
            }
            _flex.SetDummyColumn("S");

            if (Config.MA_ENV.YN_UNIT == "Y")
            {
                _flex.SetExceptEditCol("NM_ITEM", "STND_ITEM", "UNIT_IM", "UNIT_PO", "NO_LOT", "CD_ZONE", "NM_ITEMGRP", "FG_SERNO", "QT_GOODS", "FG_SLQC", "NM_PROJECT", "SEQ_PROJECT", "NM_PJT_ITEM", "STND_UNIT");
            }
            else
            {
                _flex.SetExceptEditCol("NM_ITEM", "STND_ITEM", "UNIT_IM", "UNIT_PO", "NO_LOT", "CD_ZONE", "NM_ITEMGRP", "FG_SERNO", "QT_GOODS", "FG_SLQC");
            }

            _flex.SettingVersion = "1.0.2.1";
            _flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            _flex.SetCodeHelpCol("CD_ITEM", HelpID.P_MA_PITEM_SUB1, ShowHelpEnum.Always, new string[] { "CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT_IM", "UNIT_PO", "CD_ZONE", "CLS_ITEM", "NO_DESIGN", "NM_CLS_L", "NM_CLS_M", "NM_CLS_S" },
                                                                                         new string[] { "CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT_IMNM", "UNIT_PONM", "CD_ZONE", "CLS_ITEM", "NO_DESIGN", "CLS_LN", "CLS_MN", "CLS_SN" }, ResultMode.SlowMode);
            _flex.SetCodeHelpCol("출고창고", HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CD_SL", "출고창고" }, new string[] { "CD_SL", "NM_SL" }, ResultMode.SlowMode);
            _flex.SetCodeHelpCol("입고창고", HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CD_SL_REF", "입고창고" }, new string[] { "CD_SL", "NM_SL" }, ResultMode.SlowMode);
            _flex.SetCodeHelpCol("NO_EMP", HelpID.P_MA_EMP_SUB, ShowHelpEnum.Always, new string[] { "NO_EMP", "요청자", "요청부서" }, new string[] { "NO_EMP", "NM_KOR","NM_DEPT" }, ResultMode.SlowMode);

            if (Config.MA_ENV.YN_UNIT == "Y")
            {
                _flex.SetCodeHelpCol("CD_PROJECT", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new String[] { "CD_PROJECT", "NM_PROJECT", "SEQ_PROJECT", "CD_PJT_ITEM", "NM_PJT_ITEM", "STND_UNIT" }, new String[] { "NO_PROJECT", "NM_PROJECT", "SEQ_PROJECT", "CD_PJT_ITEM", "NM_PJT_ITEM", "PJT_ITEM_STND" }, new String[] { "CD_PROJECT", "NM_PROJECT", "SEQ_PROJECT", "CD_PJT_ITEM", "NM_PJT_ITEM", "STND_UNIT" }, ResultMode.FastMode);
                _flex.SetCodeHelpCol("CD_PJT_ITEM", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new String[] { "CD_PROJECT", "NM_PROJECT", "SEQ_PROJECT", "CD_PJT_ITEM", "NM_PJT_ITEM", "STND_UNIT" }, new String[] { "NO_PROJECT", "NM_PROJECT", "SEQ_PROJECT", "CD_PJT_ITEM", "NM_PJT_ITEM", "PJT_ITEM_STND" }, new String[] { "CD_PROJECT", "NM_PROJECT", "SEQ_PROJECT", "CD_PJT_ITEM", "NM_PJT_ITEM", "STND_UNIT" }, ResultMode.FastMode);
            }
            else
            {
                _flex.SetCodeHelpCol("CD_PROJECT", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new string[] { "CD_PROJECT", "NM_PROJECT" }, new string[] { "NO_PROJECT", "NM_PROJECT" });
            }
            _flex.VerifyAutoDelete = new string[] { "CD_ITEM" };
            _flex.VerifyCompare(_flex.Cols["QT_GOOD_INV"], 0, OperatorEnum.Greater);
            if (프로젝트사용 && Global.MainFrame.ServerKeyCommon.ToUpper() != "YWD")
            {
                if (Config.MA_ENV.YN_UNIT == "Y")
                {
                    _flex.VerifyNotNull = new string[] { "CD_ITEM", "출고창고", "입고창고", "CD_PROJECT", "SEQ_PROJECT" };
                }
                else
                {
                    _flex.VerifyNotNull = new string[] { "CD_ITEM", "출고창고", "입고창고", "CD_PROJECT" };
                }

                _flex.SetExceptSumCol("SEQ_PROJECT");
            }
            else
                _flex.VerifyNotNull = new string[] { "CD_ITEM", "출고창고", "입고창고" };

            _flex.ValidateEdit += new ValidateEditEventHandler(Grid_ValidateEdit);
            _flex.BeforeCodeHelp += new BeforeCodeHelpEventHandler(_flex_BeforeCodeHelp);
            _flex.AfterCodeHelp += new AfterCodeHelpEventHandler(_flex_AfterCodeHelp);
            _flex.AddRow += new EventHandler(btn_insert_Click);
            _flex.StartEdit += new RowColEventHandler(_flex_StartEdit);
            
            _flex.DisableNumberColumnSort();

            의뢰번호Visible(fg_sub);
        }

        #endregion

        #region -> InitPaint

        protected override void InitPaint()
        {
            base.InitPaint();
            DataSet _dsCombo = GetComboData("NC;MA_PLANT", "N;MA_B000010", "S;PU_C000080", "S;SA_Z_HS001");

            cb_cd_plant.DataSource = _dsCombo.Tables[0];     // 공장
            cb_cd_plant.DisplayMember = "NAME";
            cb_cd_plant.ValueMember = "CODE";

            #region -> 예전 헤더,그리드 초기화
            //DataSet dsTemp = _biz.Search("", cb_cd_plant.SelectedValue == null ? string.Empty : cb_cd_plant.SelectedValue.ToString());

            //_header.SetBinding(dsTemp.Tables[0], m_pnlHeader);
            //_header.ClearAndNewRow();

            //_flex.Binding = dsTemp.Tables[1];
            #endregion

            Initial_Binding();// 헤더,그리드 초기화
            ToolBarPrintButtonEnabled = false;
            if (서버키_TEST포함("WONBONG"))
                btn지시소요량.Visible = true;
            //btn_BOM.Enabled = false;

            if (_no_io != string.Empty)  // 메뉴이동으로 호출됨
                OnToolBarSearchButtonClicked(null, null);

            _flex.SetDataMap("CLS_ITEM", _dsCombo.Tables[1], "CODE", "NAME");
            _flex.SetDataMap("FG_GUBUN", _dsCombo.Tables[2], "CODE", "NAME");
            if(Global.MainFrame.ServerKeyCommon.Contains("HANSU"))
                _flex.SetDataMap("PRIOR_GUBUN", _dsCombo.Tables[3], "CODE", "NAME");

            oneGrid1.UseCustomLayout = true;
            bpPanelControl1.IsNecessaryCondition = true;
            bpPanelControl2.IsNecessaryCondition = true;
            bpPanelControl3.IsNecessaryCondition = true;
            bpPanelControl4.IsNecessaryCondition = true;
            bpPanelControl7.IsNecessaryCondition = true;

            oneGrid1.IsSearchControl = false;   //2013.07.26 - 윤성우 - ONE GRID 수정(입력 전용)


            oneGrid1.InitCustomLayout();

            수불형태Default셋팅();
 
        }

        #region -> 헤더,그리드 초기화
        private void Initial_Binding()// 헤더,그리드 초기화
        {
            DataSet ds = _biz.Initial_DataSet();//디비가 아닌 biz단에서 데이터셋을 생성해서 가져옴
            _header.SetBinding(ds.Tables[0], oneGrid1);
            _header.ClearAndNewRow();

            _flex.Binding = ds.Tables[1];
        }
        #endregion

        #endregion

        #region -> Page_DataChanged

        void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsChanged())
                    this.ToolBarSaveButtonEnabled = true;


            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        #region -> InitControlEvent

        private void InitControlEvent(Control panel)
        {
            foreach (Control ctr in panel.Controls)
            {
                if (ctr is TextBoxExt)
                    ((TextBoxExt)ctr).Validated += new EventHandler(Control_Validated);
                else if (ctr is DatePicker)
                {
                    ((DatePicker)ctr).Validated += new EventHandler(Control_Validated);
                    ((DatePicker)ctr).CalendarClosed += new EventHandler(Control_Validated);
                }
                else if (ctr is BpCodeTextBox)
                {
                    ((BpCodeTextBox)ctr).QueryBefore += new BpQueryHandler(Control_QueryBefore);
                    ((BpCodeTextBox)ctr).QueryAfter += new BpQueryHandler(Control_QueryAfter);
                    ((BpCodeTextBox)ctr).CodeChanged += new EventHandler(Control_CodeChanged);
                }
                else if (ctr is DropDownComboBox)
                    ((DropDownComboBox)ctr).SelectionChangeCommitted += new EventHandler(Control_Validated);
                else if (ctr is CurrencyTextBox)
                    ((CurrencyTextBox)ctr).Validated += new EventHandler(Control_Validated);
                else if (ctr is PanelExt)
                    InitControlEvent(ctr);
            }
        }

        #endregion

        #endregion

        #region ♣ 메인버튼 이벤트 / 메소드

        #region -> BeforeSearch Override

        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch()) return false;

            if (cb_cd_plant.SelectedValue != null && cb_cd_plant.SelectedValue.ToString() != "")
                return true;

            this.ShowMessage("공장을 먼저 선택하십시오");
            cb_cd_plant.Focus();
            return false;
        }

        #endregion

        #region -> 조회버튼

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSearch()) return;

                pur.P_PU_STS_SUB dlg;

                if (프로젝트사용)
                {
                    object[] obj = { D.GetString(ctx프로젝트.CodeValue), D.GetString(ctx프로젝트.CodeName) };
                    dlg = new pur.P_PU_STS_SUB(D.GetString(bp_CD_SL.CodeValue), D.GetString(bp_CD_SL.CodeName), tb_no_emp.CodeValue, tb_no_emp.CodeName, cb_cd_plant.SelectedValue.ToString(), obj);
                }
                else
                {
                    dlg = new pur.P_PU_STS_SUB(D.GetString(bp_CD_SL.CodeValue), D.GetString(bp_CD_SL.CodeName), tb_no_emp.CodeValue, tb_no_emp.CodeName, cb_cd_plant.SelectedValue.ToString());
                }

                DialogResult Result = DialogResult.OK;
                string no_io = string.Empty ;
                string cd_pjt = string.Empty ;

                if (_no_io == string.Empty)
                {
                    Result = dlg.ShowDialog();
                    if (Result == DialogResult.OK)
                    {
                        no_io = dlg.m_NO_IO;
                        cd_pjt = dlg.m_CD_PJT == null ? string.Empty : dlg.m_CD_PJT;
                    }
                }
                else 
                {
                    no_io = _no_io;
                    cd_pjt = _cd_pjt;
                    _no_io = string.Empty;
                    _cd_pjt = string.Empty;
                }

                if (Result == DialogResult.OK)  //기본값을 Result == DialogResult.OK 줬기때문에 메뉴이동도 ok값이다.
                {
                  
                   // DataSet ds = _biz.Search(dlg.m_NO_IO, D.GetString(cb_cd_plant.SelectedValue),   dlg.m_CD_PJT == null ? string.Empty : dlg.m_CD_PJT);
                    DataSet ds = _biz.Search(no_io, D.GetString(cb_cd_plant.SelectedValue), cd_pjt);

                    _header.SetDataTable(ds.Tables[0]);

                    _flex.Binding = ds.Tables[1];

            

                    // 이부분이 왜 있을까?? 의문이네.. ㅜ.ㅜ
                    if (_flex["NO_EMP"].ToString() == string.Empty)  
                    {
                        _flex["NO_EMP"] = tb_no_emp.CodeValue;
                        if (Global.MainFrame.ServerKeyCommon != "THINK")//2011.11.14 팅크웨어 요청자가 없을시 요청자자리 비워붐
                       {
                            _flex["요청자"] = tb_no_emp.CodeName;
                       }
                        _flex.AcceptChanges();   
                    }
                    
                    
                    bp_CD_SL.CodeValue = ds.Tables[0].Rows[0]["CD_SL"].ToString();
                    bp_CD_SL.CodeName = ds.Tables[0].Rows[0]["OUT_SL"].ToString();
 
                    b_Append.Enabled = true;
                    b_Delete.Enabled = true;
                    btn_REQ.Enabled = false;
                    btn_SGIREQ.Enabled = false;
                    btn_WGIREQ.Enabled = false;

                    bp_CD_SL.Enabled = false;
                    bp입고창고.Enabled = false;

                    if (Global.MainFrame.ServerKeyCommon == "TOKIMEC") //이형준대리님요청으로 한국도키멕유공압(주) 요청적용시 조회시 추가버튼은 생기지 않도록 수정
                    {
                        b_Append.Enabled = false;
                        //b_Delete.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> BeforeAdd Override

        protected override bool BeforeAdd()
        {
            if (!base.BeforeAdd())
                return false;

            if (!MsgAndSave(PageActionMode.Search))
                return false;

            return true;
        }

        #endregion

        #region -> 추가버튼

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeAdd()) return;

                string grant = this.CurrentGrantMenu;

                Debug.Assert(_header.CurrentRow != null); // 혹시나 해서 한번 더 확인
                Debug.Assert(_flex.DataTable != null);    // 혹시나 해서 한번 더 확인

                _flex.DataTable.Rows.Clear();
                _flex.AcceptChanges();

                _header.ClearAndNewRow();

                b_Append.Enabled = true;
                b_Delete.Enabled = true;
                btn_REQ.Enabled = true;
                btn_SGIREQ.Enabled = true;
                btn_WGIREQ.Enabled = true;
                btn_ExcelUpload.Enabled = true;

                bp_CD_SL.Enabled = true;
                bp입고창고.Enabled = true;

                ToolBarPrintButtonEnabled = false;

                tb_DT_IO.Enabled = true;
                tb_cd_qtio.Enabled = true;
                cb_cd_plant.Enabled = true;

                if (!grant.Equals("E"))
                {
                    tb_no_emp.Enabled = true;
                }

                fg_sub = "";
                의뢰번호Visible(fg_sub);

                수불형태Default셋팅();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> BeforeDelete

        protected override bool BeforeDelete()
        {

            //string POP = BASIC.GetMAEXC("POP연동");
            //string MES = BASIC.GetMAEXC("MES_POP연동옵션");

            if (!base.BeforeDelete())
                return false;

            //DialogResult result = ShowMessage(공통메세지.자료를삭제하시겠습니까, PageName);
            //if (result != DialogResult.Yes) return false;

            //if (POP != "000" || MES != "000")
            //{
            //    DialogResult RESULT = ShowMessage("타 시스템으로부터 연동된 처리건입니다. 삭제하시겠습니까?", "QY2");
            //    if (RESULT != DialogResult.Yes) return false;
            //}

            if (D.GetString(_header.CurrentRow["NO_QC"]) != "") //(주)이노와이어리스 전용
            {
                ShowMessage("검사처리 항목은 삭제할 수 없습니다. 검사번호 : " + D.GetString(_header.CurrentRow["NO_QC"]));
                return false;
            }

            return true;
        }

        #endregion

        #region -> 삭제버튼

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (Global.MainFrame.ShowMessage("삭제하시겠습니까?", "QY2") == DialogResult.Yes)
                {
                    if (!BeforeDelete()) return;

                    _biz.Delete(tb_no_io.Text);

                    _header.AcceptChanges();
                    _flex.AcceptChanges();

                    OnToolBarAddButtonClicked(sender, e);
                    ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 저장버튼

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
               // if (!BeforeSave()) return;

                if (MsgAndSave(PageActionMode.Save))
                    ShowMessage(PageResultMode.SaveGood);
             
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> SaveData

        protected override bool SaveData()
        {
            if (!base.SaveData()) return false;
            
            if (!BeforeSave()) return false;

            decimal no_IOline = -1;//밑에서 더하기 2씩 해주기 때문에 -1로 잡았음 1,3,5.....if로 하는것보다 간단한것 같아서...바꿔도 상관없습니다...^^
            if (추가모드여부)
            {
                string no_seq = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PU", "12", tb_DT_IO.Text.Substring(0, 6));

                _header.CurrentRow["NO_IO"] = no_seq;

                //for (int i = _flex.Rows.Fixed; i < _flex.Rows.Count; i++)
                //    _flex[i, "NO_IO"] = no_seq;


                
                foreach (DataRow dr in _flex.DataTable.Rows)
                {
                    no_IOline = no_IOline + 2;
                    dr["NO_IO"] = no_seq;
                    dr["NO_IOLINE"] = no_IOline;
                    dr["FG_IO"] = "022";
                    if (dr["CD_SL"].ToString() == string.Empty)
                    {
                        dr["CD_SL"] = D.GetString(bp_CD_SL.CodeValue);
                        dr["출고창고"] = D.GetString(bp_CD_SL.CodeName);
                    }
                    if (dr["CD_SL_REF"].ToString() == string.Empty)
                    {
                        dr["CD_SL_REF"] = bp입고창고.CodeValue;
                        dr["입고창고"] = bp입고창고.CodeName;
                    }
                    if (dr["NO_EMP"].ToString() == string.Empty)
                    {
                        dr["NO_EMP"] = tb_no_emp.CodeValue;
                        if (Global.MainFrame.ServerKeyCommon != "THINK") //2011.11.14 팅크웨어 요청자가 없을시 요청자자리 비워붐
                           {
                              dr["요청자"] = tb_no_emp.CodeName;
                         }
                    }
                    //if (입고적용 != true) //입고적용받으면 관련수불정보 NO_IO_MGMT,NO_IOLINE_MGMT가 들어가고 아니면 기존처럼 관련수불정보에 NO_IO,NO_IO_LNE이들어간다.
                    //{

                    //    _flex["NO_IO_MGMT"] = no_seq;
                    //    _flex["NO_IOLINE_MGMT"] = no_IOline; 
                  
                }

 

                tb_no_io.Text = no_seq;

                _header.CurrentRow["YN_RETURN"] = "N";
                _header.CurrentRow["CD_PARTNER"] = "";
                _header.CurrentRow["GI_PARTNER"] = "";
            }

            if (!추가모드여부)
            {


                string no_seq = D.GetString(tb_no_io.Text);

                no_IOline = _flex.GetMaxValue("NO_IOLINE");
                foreach (DataRow dr in _flex.DataTable.Rows) //조회해온후 라인 추가 하여 저장할때 들어가게함
                {
                    if (dr.RowState == DataRowState.Added)//수정한 내용이나 삭제한 내용은 안타게 하였다
                    {
                        dr["NO_IO"] = no_seq;
                        dr["NO_IOLINE"] = no_IOline + 2;
                        no_IOline = no_IOline + 2;
                        dr["FG_IO"] = "022";
                    }
                }
            }

            DataTable dtH = _header.GetChanges();
            DataTable dtL = _flex.GetChanges();

            string YN_PALLET = string.Empty;

            if (dtH == null && dtL == null) return true;

            if (Global.MainFrame.ServerKeyCommon.Contains("ANJUN"))
            {
                string no_isurcv_multi = string.Empty;

                foreach (DataRow row in dtL.Rows) //프로젝트값을 라인에 일괄반영
                {
                    if (row.RowState == DataRowState.Deleted) continue;

                    no_isurcv_multi += D.GetString(row["NO_ISURCV"]) + "|";

                }
                if (no_isurcv_multi != string.Empty)
                {
                    DataTable dt = _biz.YN_Pallet(no_isurcv_multi);
                    if (dt != null)
                    { 
                        if (D.GetString(dt.Rows[0]["YN_PALLET"]) == "E")
                        {
                            ShowMessage("출고요청건중 적용구분이 다른데이터가 존재합니다.");
                            return false;
                        }
                        YN_PALLET = D.GetString(dt.Rows[0]["YN_PALLET"]);

                    }
                }
            }

            DataTable dtLOT = null;

            if (String.Compare(MNG_LOT, "Y") == 0 && dtL != null)
            {
                dtL.Columns.Add("NM_SL", typeof(string));

                DataRow[] DR = dtL.Select("NO_LOT = 'YES'", "", DataViewRowState.Added);
                DataTable _dtLOT = dtL.Clone();

                if (DR.Length > 0)
                {
                    foreach (DataRow drLOT in DR)
                    {

                        drLOT["NM_SL"] = drLOT["출고창고"];
                        _dtLOT.ImportRow(drLOT);
                    }

                    P_PU_LOT_SUB_I m_dlg = null;
                    if (Global.MainFrame.ServerKeyCommon.Contains("ANJUN"))
                        m_dlg = new P_PU_LOT_SUB_I(_dtLOT, new string[] { "N", "P_PU_STS_REG", YN_PALLET });
                    else
                        m_dlg = new P_PU_LOT_SUB_I(_dtLOT, new string[] { "N", "P_PU_STS_REG" });

                    if (m_dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        dtLOT = m_dlg.dtL;

                        dtLOT.Columns.Add("입고창고", typeof(string));

                        for (int i = 0; i < dtL.Rows.Count; i++)
                        {

                            DataRow[] drL = dtLOT.Select(" 출고항번 = " + dtL.Rows[i]["NO_IOLINE"].ToString().Trim() + "" , "", DataViewRowState.CurrentRows);

                            foreach (DataRow rowL in drL)
                            {
                                rowL["입고창고"] = dtL.Rows[i]["CD_SL_REF"].ToString().Trim();
                                rowL["수불구분"] = "022";

                            }
                        }
                    }
                    else
                    {
                        if (추가모드여부)
                        {
                            tb_no_io.Text = "";
                            _header.CurrentRow["NO_IO"] = "";
                        }
                        return false;
                    }
                }
            }

            if (dtLOT != null )
            {
                foreach (DataRow dr in dtLOT.Rows) //상태값 강제로 바꾸기
                {
                    dr.AcceptChanges();
                    dr.SetAdded();
                }
            }

            //시리얼추가 
            DataTable dtSERL = null;
            if (String.Compare(MNG_SERIAL, "Y") == 0 && dtL != null)
            {
                DataRow[] DR = dtL.Select("NO_SERL = 'YES'", "", DataViewRowState.Added);
                DataTable _dtSERL = dtL.Clone();

                if (DR.Length > 0)
                {
                    foreach (DataRow drSERL in DR)
                    {
                        _dtSERL.ImportRow(drSERL);
                    }

                    P_PU_SERL_SUB_I m_dlg3;

                    m_dlg3 = new P_PU_SERL_SUB_I(_dtSERL);
                    if (m_dlg3.ShowDialog(this) == DialogResult.OK)
                        dtSERL = m_dlg3.dtL;
                    else
                    {
                        if (추가모드여부)
                        {
                            tb_no_io.Text = "";
                            _header.CurrentRow["NO_IO"] = "";
                        }
                        return false;
                    }
                    
                }
            }

            #region -> LOCATION 등록
            DataTable dt_location = null;
            if (Config.MA_ENV.YN_LOCATION == "Y") //시스템환경설정에서 LOCATION사용인것만 창고별로 사용인지 아닌지는 도움창 호출후 판단한다. 붙여야하는화면이 많은 관계로 여기서 통합으로 처리해주는걸로 판단함
            {                                           //넘길때 공장,창고,품목은 필수항목

                bool b_lct = false;
                DataTable dt_lc = dtL.Clone().Copy();
                foreach (DataRow dr in dtL.Select())
                    dt_lc.LoadDataRow(dr.ItemArray, true);


                dt_location = P_OPEN_SUBWINDOWS.P_MA_LOCATION_I_R_SUB(dt_lc, out b_lct);

                if (b_lct == false)
                    return false;

            }
            #endregion

            DataTable dtQCl = null;

            DataRow[] drQC = _flex.DataTable.Select("FG_SLQC = 'Y'");
            dtQCl = _flex.DataTable.Clone();

            foreach (DataRow drL in drQC)
                dtQCl.ImportRow(drL);


            _biz.biz_cd_plant = D.GetString(cb_cd_plant.SelectedValue);
            bool result = _biz.Save(dtH, dtL, dtLOT, dtSERL, dt_location, dtQCl);  //_biz.Save(dtH, dtL, dtLOT);

            if (!result)
                return false;

            _header.AcceptChanges();
            _flex.AcceptChanges();

            b_Append.Enabled = true;
            bp_CD_SL.Enabled = false;
            bp입고창고.Enabled = false;
            btn_REQ.Enabled = false;

            if (Global.MainFrame.ServerKeyCommon == "TOKIMEC")
            {
                b_Append.Enabled = false;
                //b_Delete.Enabled = false;
            }

            return true;
        }

        #endregion

        #region -> BeforeSave

        protected override bool BeforeSave()
        {
            if (!base.BeforeSave()) return false;
            if (!Save_Check()) return false;

            if (!this.Verify())     // 그리드 체크
                return false;

            if (!_flex.HasNormalRow)
            {
                OnToolBarDeleteButtonClicked(null, null);
                return false;
            }

            return true;
        }

        #endregion

        #region -> 인쇄버튼

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (추가모드여부) return;

                DataTable dt요청 = _biz.Search_요청(tb_no_io.Text);
                string 요청번호 = dt요청.Rows.Count > 0 ? dt요청.Rows[0]["NO_GIREQ"].ToString() : "";
                string 요청일자 = dt요청.Rows.Count > 0 ? dt요청.Rows[0]["DT_GIREQ"].ToString() : "";
                string 요청부서 = dt요청.Rows.Count > 0 ? dt요청.Rows[0]["NM_DEPT"].ToString() : "";
                string 요청자 = dt요청.Rows.Count > 0 ? dt요청.Rows[0]["NM_KOR"].ToString() : "";

                ReportHelper rptHelper = new ReportHelper("R_PU_STS_REG2_0", "S/L 이동전표");

                rptHelper.SetData("수불번호", tb_no_io.Text);
                rptHelper.SetData("이동일자", tb_DT_IO.Text.Substring(0, 4) + "/" + tb_DT_IO.Text.Substring(4, 2) + "/" + tb_DT_IO.Text.Substring(6, 2));
                rptHelper.SetData("공장코드", cb_cd_plant.SelectedValue.ToString());
                rptHelper.SetData("공장명", cb_cd_plant.Text);
                rptHelper.SetData("출고창고코드", D.GetString(bp_CD_SL.CodeValue));
                rptHelper.SetData("출고창고명", D.GetString(bp_CD_SL.CodeName));
                rptHelper.SetData("입고창고코드", bp입고창고.CodeValue.ToString());
                rptHelper.SetData("입고창고명", bp입고창고.CodeName.ToString());
                rptHelper.SetData("사원코드", tb_no_emp.CodeValue);
                rptHelper.SetData("사원명", tb_no_emp.CodeName);
                rptHelper.SetData("수불형태", tb_cd_qtio.CodeName);
                rptHelper.SetData("부서코드", _header.CurrentRow["CD_DEPT"].ToString());
                rptHelper.SetData("부서명", _header.CurrentRow["NM_DEPT"].ToString());
                rptHelper.SetData("비고", tb_dc.Text);
                rptHelper.SetData("요청번호", 요청번호);
                rptHelper.SetData("요청일자", 요청일자);
                rptHelper.SetData("요청부서", 요청부서);
                rptHelper.SetData("요청자", 요청자);

                DataTable dt = new DataTable();

                if (Global.MainFrame.ServerKeyCommon == "ALOE" || Global.MainFrame.ServerKeyCommon =="WJIS")
                {
                    DataSet ds = _biz.dt_print(tb_no_io.Text, Global.MainFrame.ServerKeyCommon);
                    dt = ds.Tables[0];
                }
                else
                {
                    dt = _flex.DataTable;
                }
                
                rptHelper.SetDataTable(dt);
                
                rptHelper.Print();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 종료버튼
        public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
        {
            try
            {
                Settings1.Default.CD_QTIOTP = D.GetString(tb_cd_qtio.CodeValue);
                Settings1.Default.NM_QTIOTP = D.GetString(tb_cd_qtio.CodeName);
                Settings1.Default.Save();

                return base.OnToolBarExitButtonClicked(sender, e);
            }
            catch (Exception ex)
            {

                MsgEnd(ex);
                return false;
            }
        } 
        #endregion

        #endregion

        #region ♣ 바인딩

        #region -> 조회후 Head 테이블 바인딩

        void SetHeaderBinding()
        {
            SetBindingToControl(oneGrid1, _dtH.Rows[0]);
        }

        #endregion

        #region -> DataRow의 값을 컨트롤에 바인딩

        private void SetBindingToControl(Control panel, DataRow row)
        {
            foreach (Control ctr in panel.Controls)
            {
                if (ctr is TextBoxExt)
                {
                    ((TextBoxExt)ctr).Text = row == null ? string.Empty : row[ctr.Tag.ToString()].ToString();
                }
                else if (ctr is DatePicker)
                {
                    ((DatePicker)ctr).Text = row == null ? string.Empty : row[ctr.Tag.ToString()].ToString();
                }
                else if (ctr is DropDownComboBox)
                {
                    ((DropDownComboBox)ctr).SelectedValue = row == null ? string.Empty : row[ctr.Tag.ToString()].ToString();
                }
                else if (ctr is BpCodeTextBox)
                {
                    if (row == null)
                        ((BpCodeTextBox)ctr).IsEmpty();
                    else
                    {
                        string[] Args = ctr.Tag.ToString().Split(Convert.ToChar(";"));
                        ((BpCodeTextBox)ctr).SetCodeValue(row[Args[0]].ToString());
                        ((BpCodeTextBox)ctr).SetCodeName(row[Args[1]].ToString());
                    }
                }
                else if (ctr is CurrencyTextBox)
                {
                    if (row == null)
                        ((CurrencyTextBox)ctr).DecimalValue = 0;
                    else
                        ((CurrencyTextBox)ctr).DecimalValue = row[ctr.Tag.ToString()] == null ? 0 : Convert.ToDecimal(row[ctr.Tag.ToString()]);
                }
                else if (ctr is PanelExt)
                    SetBindingToControl(ctr, row);
            }
        }

        #endregion

        #region -> Control_Validated

        private void Control_Validated(object sender, EventArgs e)
        {
            try
            {
                if (((Control)sender) is TextBoxExt)
                {
                    if (!((TextBoxExt)sender).Modified) return;     // 사용자가 손으로 직접 입력한 경우가 아니면 무시
                }
                else if (((Control)sender) is DatePicker)
                {
                    if (!((DatePicker)sender).Modified) return;
                }
                else if (((Control)sender) is DropDownComboBox)
                {
                    if (!((DropDownComboBox)sender).Modified) return;
                }
                else if (((Control)sender) is CurrencyTextBox)
                {
                    if (!((CurrencyTextBox)sender).Modified) return;
                }
                this.SetBindingToDataRow(sender, _dtH.Rows[0]);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> SetBindingToDataRow

        private void SetBindingToDataRow(object sender, DataRow row)
        {
            if (row == null) return;

            if (((Control)sender) is TextBoxExt)
            {
                row[((Control)sender).Tag.ToString()] = ((TextBoxExt)sender).Text;
            }
            else if (((Control)sender) is DatePicker)
            {
                row[((Control)sender).Tag.ToString()] = ((DatePicker)sender).Text;
            }
            else if (((Control)sender) is DropDownComboBox)
            {
                row[((Control)sender).Tag.ToString()] = ((DropDownComboBox)sender).SelectedValue;
            }
            else if (((Control)sender) is CurrencyTextBox)
            {
                row[((Control)sender).Tag.ToString()] = ((CurrencyTextBox)sender).Text;
            }
            this.ToolBarSaveButtonEnabled = true;
        }

        #endregion

        #endregion

        #region ♣ 그리드 이벤트 / 메소드

        #region -> 그리드 도움창 호출전 세팅 이벤트(_flex_BeforeCodeHelp)

        private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                switch (_flex.Cols[e.Col].Name)
                {
                    case "CD_ITEM":
                        if (D.GetString(_flex["NO_IO"]) != string.Empty)
                        {
                            e.Cancel = true;
                            return;
                        }
                        e.Parameter.P01_CD_COMPANY = LoginInfo.CompanyCode;
                        e.Parameter.P09_CD_PLANT = cb_cd_plant.SelectedValue.ToString();
                        break;
                    case "출고창고":
                        // 2012.09.03 생산쪽에 문의해본결과 의뢰단에서는 창고를 알수 없고
                        // 창고이동당시에 출고창고를 아는경우가 많다.
                        // 적용시 등록된 출고창고가 기본적으로 등록되고
                        // 저장시 빈가 check 하므로 데이터를 변경하는데는 차이가 없다.
                        // 2012.09.03 SMR
                        //if (fg_sub == "생산" || fg_sub == "외주")
                        //{
                        //    ShowMessage(공통메세지.이미등록된자료가있습니다);
                        //    e.Cancel = true;
                            
                        //    return;
                        //}
                        //else
                        //{
                            e.Parameter.P01_CD_COMPANY = LoginInfo.CompanyCode;
                            e.Parameter.P09_CD_PLANT = cb_cd_plant.SelectedValue.ToString();
                            if (m_sys_set_SL == "000" || m_sys_set_SL == "001")
                            {
                                e.Parameter.P07_NO_EMP = Global.MainFrame.LoginInfo.EmployeeNo;
                            }
                            else
                            {
                                e.Parameter.P07_NO_EMP = null;
                            }
                        //}
                        break;
                    case "입고창고":
                        e.Parameter.P01_CD_COMPANY = LoginInfo.CompanyCode;
                        e.Parameter.P09_CD_PLANT = cb_cd_plant.SelectedValue.ToString();

                        if (m_sys_set_SL == "000" || m_sys_set_SL=="002")
                        {
                            e.Parameter.P07_NO_EMP = Global.MainFrame.LoginInfo.EmployeeNo;
                        }
                        else
                        {
                            e.Parameter.P07_NO_EMP = null;
                        }
                        break;

                    case "CD_PJT_ITEM":
                        if (D.GetString(_flex["CD_PROJECT"]) != string.Empty)
                        {
                            e.Parameter.P64_CODE4 = D.GetString(_flex["CD_PROJECT"]);
                        }
                        break;

                    //// 한수 전용로직
                    //case "CD_PACKUNIT":
                    //    e.Parameter.P09_CD_PLANT = D.GetString(cb_cd_plant.SelectedValue);
                    //    break;

                    //// 사용자정의 컬럼
                    //case "CD_USERDEF1":

                    //    // 한수 전용로직
                    //    if (Global.MainFrame.ServerKeyCommon.ToUpper() == "DZSQL"  || Global.MainFrame.ServerKeyCommon.ToUpper() == "HANSU")
                    //    {
                    //        e.Parameter.P09_CD_PLANT = D.GetString(cb_cd_plant.SelectedValue);
                    //        e.Parameter.P41_CD_FIELD1 = cb_cd_plant.Text.Substring(0, cb_cd_plant.Text.Replace(" ", "").IndexOf("("));
                    //        e.Parameter.UserParams = DD("전용코드;");

                            
                    //    }

                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 그리드 도움창 호출후 변경 이벤트(_flex_AfterCodeHelp)

        private void _flex_AfterCodeHelp(object sender, AfterCodeHelpEventArgs e)
        {
            try
            {
                FlexGrid flex = sender as FlexGrid;
                HelpReturn helpReturn = e.Result;
                DataTable dt = flex.DataTable;
                DataRow drD;
                bool 첫줄여부 = true;
                int apply_row = 0;
        
                switch (_flex.Cols[e.Col].Name)
                {
                    case "CD_ITEM":
                        
                        if (e.Result.DialogResult == DialogResult.Cancel)
                            return;

                        decimal MaxSeq = _flex.GetMaxValue("NO_IOLINE");

                        bool First = true;
                        _flex.Redraw = false;

                        foreach (DataRow dr in helpReturn.Rows)
                        {
                            if (First)
                            {
                                apply_row = e.Row;
                                _flex[e.Row, "CD_ITEM"] = dr["CD_ITEM"];
                                _flex[e.Row, "NM_ITEM"] = dr["NM_ITEM"];
                                _flex[e.Row, "STND_ITEM"] = dr["STND_ITEM"];
                                _flex[e.Row, "UNIT_IM"] = dr["UNIT_IM"];
                                _flex[e.Row, "UNIT_PO"] = dr["UNIT_PO"];
                                _flex[e.Row, "CD_ZONE"] = dr["CD_ZONE"];
                                _flex[e.Row, "NM_ITEMGRP"] = dr["GRP_ITEMNM"];
                                _flex[e.Row, "CLS_ITEM"] = dr["CLS_ITEM"];
                                _flex[e.Row, "NM_MAKER"] = dr["NM_MAKER"];
                                _flex[e.Row, "NO_DESIGN"] = dr["NO_DESIGN"];
                              
                                _flex[e.Row, "NM_CLS_L"] = dr["CLS_LN"];
                                _flex[e.Row, "NM_CLS_M"] = dr["CLS_MN"];
                                _flex[e.Row, "NM_CLS_S"] = dr["CLS_SN"];
                                

                                First = false;
                            }
                            else
                            {
                                _flex.Rows.Add();
                                _flex.Row = _flex.Rows.Count - 1;

                                _flex["CD_ITEM"] = dr["CD_ITEM"];
                                _flex["NM_ITEM"] = dr["NM_ITEM"];
                                _flex["STND_ITEM"] = dr["STND_ITEM"];
                                _flex["UNIT_IM"] = dr["UNIT_IM"];
                                _flex["UNIT_PO"] = dr["UNIT_PO"];
                                _flex["CD_ZONE"] = dr["CD_ZONE"];
                                _flex["NM_ITEMGRP"] = dr["GRP_ITEMNM"];
                                _flex["CLS_ITEM"] = dr["CLS_ITEM"];
                                _flex["NM_MAKER"] = dr["NM_MAKER"];
                                _flex["NO_DESIGN"] = dr["NO_DESIGN"];
                            
                                _flex[e.Row, "NM_CLS_L"] = dr["CLS_LN"];
                                _flex[e.Row, "NM_CLS_M"] = dr["CLS_MN"];
                                _flex[e.Row, "NM_CLS_S"] = dr["CLS_SN"];
                                apply_row = _flex.Rows.Count - 1;
                            }

                            if (D.GetString(dr["FG_SERNO"]) == "002") _flex["FG_SERNO"] = "LOT";
                            else if (D.GetString(dr["FG_SERNO"]) == "003") _flex["FG_SERNO"] = "S/N";
                            else _flex["FG_SERNO"] = DD("미관리");

                            _flex[apply_row, "CD_QTIOTP"] = _header.CurrentRow["CD_QTIOTP"];
                            _flex[apply_row, "CD_PLANT"] = _header.CurrentRow["CD_PLANT"];
                            _flex[apply_row, "DT_IO"] = _header.CurrentRow["DT_IO"];
                            _flex[apply_row, "YN_RETURN"] = "N";
                            _flex[apply_row, "YN_AM"] = _header.CurrentRow["YN_AM"];

                            _flex[apply_row, "S"] = "N";
                            //_flex[apply_row, "NO_IO"] = tb_no_io.Text; //savedata()에서 넣어줌
                            //_flex[apply_row, "NO_IOLINE"] = MaxSeq;

                           // if (/*Global.MainFrame.LoginInfo.MngLot == "Y" */MNG_LOT == "Y"  && D.GetString( e.Result.Rows[0][apply_row"FG_SERNO"]) == "002")

                            if (MNG_LOT == "Y" && D.GetString(dr["FG_SERNO"]) == "002")  
                              _flex[apply_row, "NO_LOT"] = "YES";
                            else
                            {
                                _flex[apply_row, "NO_LOT"] = "NO"; 
                              //  if (MNG_SERIAL == "Y" && D.GetString(e.Result.Rows[0]["FG_SERNO"])== "003")
                                if (MNG_SERIAL == "Y" && D.GetString(dr["FG_SERNO"]) == "003" ) 
                                    _flex[apply_row, "NO_SERL"] = "YES";
                                else
                                    _flex[apply_row, "NO_SERL"] = "NO";
                            }
                           
                            _flex[apply_row, "NM_QTIOTP"] = tb_cd_qtio.CodeName;


                            _flex[apply_row, "NO_ISURCV"] = "";
                            _flex[apply_row, "NO_ISURCVLINE"] = 0;

                            if (D.GetString(bp_CD_SL.CodeValue) != string.Empty)
                            {
                                _flex[apply_row, "CD_SL"] = D.GetString(bp_CD_SL.CodeValue);
                                _flex[apply_row, "출고창고"] = D.GetString(bp_CD_SL.CodeName);
                            }
                            else
                            {
                                _flex[apply_row, "CD_SL"] = dr["CD_GISL"];
                                _flex[apply_row, "출고창고"] = dr["NM_GISL"];
                            }

                            if (D.GetString(bp입고창고.CodeValue) != string.Empty)
                            {
                                _flex[apply_row, "CD_SL_REF"] = bp입고창고.CodeValue.ToString();
                                _flex[apply_row, "입고창고"] = bp입고창고.CodeName.ToString();
                            }
                            else
                            {
                                _flex[apply_row, "CD_SL_REF"] = dr["CD_SL"];
                                _flex[apply_row, "입고창고"] = dr["NM_SL"];
                            }


                            if (D.GetString(_flex["CD_SL_REF"]) != string.Empty)
                            {
                                if (_biz.FG_SLQC("CD_SL", "", D.GetString(cb_cd_plant.SelectedValue), D.GetString(_flex["CD_SL_REF"])) == "Y")
                                    _flex[apply_row, "FG_SLQC"] = _biz.FG_SLQC("ITEM", D.GetString(dr["CD_ITEM"]), D.GetString(cb_cd_plant.SelectedValue), "");
                                else
                                    _flex[apply_row, "FG_SLQC"] = "N";
                            }
                            else
                            {
                                _flex[apply_row, "FG_SLQC"] = _biz.FG_SLQC("ITEM", D.GetString(dr["CD_ITEM"]), D.GetString(cb_cd_plant.SelectedValue), "");

                            }

                            decimal qt_pinvn =0;
                            if (Config.MA_ENV.PJT형여부 == "N")
                            {
                                qt_pinvn = BASICPU.Item_PINVN(D.GetString(_header.CurrentRow["CD_PLANT"]), D.GetString(_flex[apply_row, "CD_SL"]), D.GetString(_flex[apply_row, "CD_ITEM"]));
                            }
                            else
                            {
                                qt_pinvn = BASICPU.Item_PINVN_PJT(D.GetString(_header.CurrentRow["CD_PLANT"]), D.GetString(_flex[apply_row, "CD_SL"]), D.GetString(_flex[apply_row, "CD_ITEM"]), D.GetString(_flex[apply_row, "CD_PROJECT"]), ((Config.MA_ENV.YN_UNIT == "Y") ? D.GetDecimal(_flex[apply_row, "SEQ_PROJECT"]) : 0));
                            }
                            _flex[apply_row, "QT_GOODS"] = D.GetDecimal(qt_pinvn);


                            _flex[apply_row, "BARCODE"] = D.GetString(dr["BARCODE"]);
                            _flex[apply_row, "UNIT_PO_FACT"] = D.GetString(dr["UNIT_PO_FACT"]); 

                            MaxSeq = MaxSeq + 2; //항번은 홀수로 입력됨에 유의 ( 짝수가 저장시 대응되는 입고수불로 처리됨)
                            첫줄여부 = false;
                        }
                        _flex.Select(e.Row, _flex.Cols.Fixed);

                        _flex.Redraw = true;
                        break;
                    case "출고창고":
                        if (_flex.RowState() != DataRowState.Added)
                        {
                            e.Cancel = true;
                            return;
                        }
                            _flex["CD_SL"] = e.Result.Rows[0]["CD_SL"];
                            _flex["출고창고"] = e.Result.Rows[0]["NM_SL"];
                           // _flex["QT_GOODS"] = BASICPU.Item_PINVN(D.GetString(_header.CurrentRow["CD_PLANT"]), D.GetString(_flex["CD_SL"]), D.GetString(_flex["CD_ITEM"]));

                            decimal qt_pinvn_sl = 0;
                            if (Config.MA_ENV.PJT형여부 == "N")
                            {
                                qt_pinvn_sl = BASICPU.Item_PINVN(D.GetString(_header.CurrentRow["CD_PLANT"]), D.GetString(_flex["CD_SL"]), D.GetString(_flex["CD_ITEM"]));
                            }
                            else
                            {
                                qt_pinvn_sl = BASICPU.Item_PINVN_PJT(D.GetString(_header.CurrentRow["CD_PLANT"]), D.GetString(_flex["CD_SL"]), D.GetString(_flex["CD_ITEM"]), D.GetString(_flex["CD_PROJECT"]), ((Config.MA_ENV.YN_UNIT == "Y") ? D.GetDecimal(_flex["SEQ_PROJECT"]) : 0));
                            }

                            _flex["QT_GOODS"] = D.GetDecimal(qt_pinvn_sl);

                        break;
                    case "입고창고":
                        if (_flex.RowState() != DataRowState.Added)
                        {
                            e.Cancel = true;
                            return;
                        }
                        _flex["CD_SL_REF"] = e.Result.Rows[0]["CD_SL"];
                        _flex["입고창고"] = e.Result.Rows[0]["NM_SL"];

                        if (_biz.FG_SLQC("CD_SL", "", D.GetString(cb_cd_plant.SelectedValue), D.GetString(_flex["CD_SL_REF"])) == "Y")
                            _flex["FG_SLQC"] = _biz.FG_SLQC("ITEM", D.GetString(_flex["CD_ITEM"]), D.GetString(cb_cd_plant.SelectedValue), "");
                        else
                            _flex["FG_SLQC"] = "N";

                        break;

                    case "CD_SL":
                        if (_flex.RowState() != DataRowState.Added)
                        {
                            e.Cancel = true;
                            return;
                        }

                        _flex["CD_PROJECT"] = e.Result.Rows[0]["NO_PROJECT"];
                        _flex["NM_PROJECT"] = e.Result.Rows[0]["NM_PROJECT"];
                        // _flex["QT_GOODS"] = BASICPU.Item_PINVN(D.GetString(_header.CurrentRow["CD_PLANT"]), D.GetString(_flex["CD_SL"]), D.GetString(_flex["CD_ITEM"]));

                        decimal qt_pinvn_pjt = 0;
                        if (Config.MA_ENV.PJT형여부 == "N")
                        {
                            qt_pinvn_pjt = BASICPU.Item_PINVN(D.GetString(_header.CurrentRow["CD_PLANT"]), D.GetString(_flex["CD_SL"]), D.GetString(_flex["CD_ITEM"]));
                        }
                        else
                        {
                            qt_pinvn_pjt = BASICPU.Item_PINVN_PJT(D.GetString(_header.CurrentRow["CD_PLANT"]), D.GetString(_flex["CD_SL"]), D.GetString(_flex["CD_ITEM"]), D.GetString(_flex["CD_PROJECT"]), ((Config.MA_ENV.YN_UNIT == "Y") ? D.GetDecimal(_flex["SEQ_PROJECT"]) : 0));
                        }

                        _flex["QT_GOODS"] = D.GetDecimal(qt_pinvn_pjt);

                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> Grid_HelpClick

        void Grid_HelpClick(object sender, EventArgs e)
        {
            try
            {
                FlexGrid _flex = sender as FlexGrid;
                if (_flex == null) return;

                switch (_flex.Cols[_flex.Col].Name)
                {
                    case "CD_SL":
                        master.P_MA_SL_AUTH_SUB dlg = new master.P_MA_SL_AUTH_SUB();
                        if (dlg.ShowDialog(this) == DialogResult.OK)
                        {
                            _flex["CD_SL"] = dlg.returnParams[0];
                            _flex["NM_SL"] = dlg.returnParams[1];
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 그리드 Validate Edit 이벤트(Grid_ValidateEdit)

        void _flex_StartEdit(object sender, RowColEventArgs e) 
        {
            if (_flex.Cols[e.Col].Name != "S" && D.GetString(_header.CurrentRow["NO_QC"]) != "")
            {
                ShowMessage("검사처리 항목은 수정 할 수 없습니다. 검사번호 : " + D.GetString(_header.CurrentRow["NO_QC"]));
                e.Cancel = true;
                return;
            }
                switch (_flex.Cols[e.Col].Name)
                {
                    case "QT_GIREQ":
                        e.Cancel = true;
                        break;
                    case "CD_ITEM":
                        if (D.GetString(_flex["NO_IO"]) != string.Empty)
                        {
                            e.Cancel = true;
                            return;
                        }
                        break;

                    case "입고창고":
                    case "출고창고":
                        if (_flex.RowState() != DataRowState.Added)
                        {
                            e.Cancel = true;
                            return;
                        }
                        break;
                }

            
        }

        private void Grid_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                FlexGrid _flex = sender as FlexGrid;
                if (_flex == null) return;

                string oldValue = _flex.GetData(e.Row, e.Col).ToString();
                string newValue = _flex.EditData;

                if (tb_no_io.Text != "")
                {
                    if (oldValue.ToUpper() == newValue.ToUpper())
                        return;
                }

                if (_flex.Cols[e.Col].Name == "QT_GOOD_INV")
                {
                    if ((String.Compare(MNG_LOT, "Y") == 0 && _flex["NO_LOT"].ToString() == "YES") && _flex.RowState() != DataRowState.Added )
                    {
                        ShowMessage(" LOT처리대상 - 저장된 이후로는 총수량 수정이 불가합니다. ");
                        if (_flex.Editor != null)
                            _flex.Editor.Text = oldValue;
                        _flex["QT_GOOD_INV"] = oldValue;
                        return;
                    }

                    _flex["QT_GOOD_INV"] = _flex.CDecimal(newValue);
                    _flex["QT_GOOD_OLD"] = _flex.CDecimal(oldValue);

                    _flex["QT_UNIT_PO"] = calc이동수량("QT_GOOD_INV", _flex.CDecimal(newValue), D.GetDecimal(_flex["UNIT_PO_FACT"]));

                }
                else if (_flex.Cols[e.Col].Name == "QT_UNIT_PO")
                {
                    _flex["QT_GOOD_INV"] = calc이동수량("QT_UNIT_PO", _flex.CDecimal(newValue), D.GetDecimal(_flex["UNIT_PO_FACT"]));
                }


            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region ♣ 도움창 이벤트 / 메소드

        #region -> 도움창 호출전 세팅 이벤트(Control_QueryBefore)

        private void Control_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                Control ctr = (Control)sender;
                switch (e.HelpID)
                {
                    case Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB:		// 창고 도움창
                        if (ctr.Name == "bp_CD_SL" || ctr.Name == "bp입고창고")
                        {
                            // 2012.09.03 생산쪽에 문의해본결과 의뢰단에서는 창고를 알수 없고
                            // 창고이동당시에 출고창고를 아는경우가 많다.
                            // 적용시 등록된 출고창고가 기본적으로 등록되고
                            // 저장시 빈가 check 하므로 데이터를 변경하는데는 차이가 없다.
                            // 2012.09.03 SMR
                            //if ( ctr.Name == "bp_CD_SL" && (fg_sub == "생산" || fg_sub == "외주"))
                            //{
                            //    ShowMessage(공통메세지.이미등록된자료가있습니다);
                            //    e.QueryCancel = true;
                            //    return;
                            //}
                            if (cb_cd_plant.SelectedValue != null && cb_cd_plant.SelectedValue.ToString() != "")
                            {
                                e.HelpParam.P09_CD_PLANT = cb_cd_plant.SelectedValue.ToString();

                                if (m_sys_set_SL == "000")
                                {
                                    e.HelpParam.P07_NO_EMP = Global.MainFrame.LoginInfo.EmployeeNo;                                 
                                }
                                else if(m_sys_set_SL == "001" && ctr.Name == "bp_CD_SL")
                                {
                                    e.HelpParam.P07_NO_EMP = Global.MainFrame.LoginInfo.EmployeeNo;
                                }
                                else if (m_sys_set_SL == "002" && ctr.Name == "bp입고창고")
                                {
                                    e.HelpParam.P07_NO_EMP = Global.MainFrame.LoginInfo.EmployeeNo;
                                }
                                else
                                {
                                    e.HelpParam.P07_NO_EMP = null;
                                }
                            }
                            else
                            {
                                ShowMessage("공장을 먼저 선택하십시오");
                                cb_cd_plant.Focus();
                                e.QueryCancel = true;
                                return;
                            }
                        }
                        break;
                    case Duzon.Common.Forms.Help.HelpID.P_PU_EJTP_SUB:		// 수불유형 도움창	
                        e.HelpParam.P61_CODE1 = "022|";
                        //btn_BOM.Enabled = true;
                        break;
                    

                }
                
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 도움창 호출후 세팅 이벤트(Control_QueryAfter)

        private void Control_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                if (e.DialogResult != DialogResult.OK) return;

                BpCodeTextBox bpControl = sender as BpCodeTextBox;
                if (bpControl == null) return;

                System.Data.DataRow[] rows = e.HelpReturn.Rows;

                switch (((BpCodeTextBox)sender).Name)
                {
                    case "tb_no_emp":
                        _header.CurrentRow["CD_DEPT"] = rows[0]["CD_DEPT"];
                        break;
                    case "tb_cd_qtio":
                        if (rows[0]["FG_IO"].ToString() == "022")   //창고이동 : '022'
                        {
                            //_header.CurrentRow["FG_TRANS"] = e.HelpReturn.Rows[0]["FG_TRANS"];		    //거래구분코드
                            //_header.CurrentRow["YN_PURCHASE"] = e.HelpReturn.Rows[0]["YN_PURCHASE"];		//매입유무					
                            //_header.CurrentRow["FG_TPPURCHASE"] = e.HelpReturn.Rows[0]["FG_TPPURCHASE"];	//매입형태
                            //_header.CurrentRow["TP_QTIO"] = e.HelpReturn.Rows[0]["TP_QTIO"];				//입출고구분
                            _header.CurrentRow["YN_AM"] = e.HelpReturn.Rows[0]["YN_AM"];
                            //_header.CurrentRow["CD_QTIOTP"] = e.HelpReturn.Rows[0]["CD_QTIOTP"];
                        }
                        else
                        {
                            ShowMessage("입력하신 입고형태(" + e.CodeName + "[" + e.CodeValue + "])가 화면에서 지정할 수 없는 수불형태입니다. 다시 입력해주십시오");
                            bpControl.CodeValue = "";
                            bpControl.CodeName = "";
                            //_header.CurrentRow["FG_TRANS"] = "";
                            //_header.CurrentRow["YN_PURCHASE"] = "";
                            //_header.CurrentRow["FG_TPPURCHASE"] = "";
                            //_header.CurrentRow["TP_QTIO"] = "";
                            //_header.CurrentRow["YN_AM"] = "";
                            //_header.CurrentRow["CD_QTIOTP"] = "";
                            bpControl.Focus();
                        }
                        break;

                    case "ctx프로젝트":
                        if (Config.MA_ENV.YN_UNIT == "Y")
                        {
                            d_SEQ_PROJECT = D.GetDecimal(e.HelpReturn.Rows[0]["SEQ_PROJECT"]);
                            s_CD_PJT_ITEM = D.GetString(e.HelpReturn.Rows[0]["CD_PJT_ITEM"]);
                            s_NM_PJT_ITEM = D.GetString(e.HelpReturn.Rows[0]["NM_PJT_ITEM"]);
                            s_PJT_ITEM_STND = D.GetString(e.HelpReturn.Rows[0]["PJT_ITEM_STND"]);
                        }
                        break;
                    //case "bp_CD_SL":
                    //    _flex.Redraw = false;
                    //    for (int i = 0; i < _flex.DataTable.Rows.Count; i++)
                    //    {

                    //        _flex.DataTable.Rows[i]["CD_SL"] = e.HelpReturn.Rows[0]["CD_SL"];
                    //        _flex.DataTable.Rows[i]["출고창고"] = e.HelpReturn.Rows[0]["NM_SL"];
                    //    }
                    //    _flex.Redraw = true;
                    //    break;
                    //case "bp입고창고":
                    //    _flex.Redraw = false;
                    //    for (int i = 0; i < _flex.DataTable.Rows.Count; i++)
                    //    {
                    //        _flex.DataTable.Rows[i]["CD_SL_REF"] = e.HelpReturn.Rows[0]["CD_SL"];
                    //        _flex.DataTable.Rows[i]["입고창고"] = e.HelpReturn.Rows[0]["NM_SL"];
                    //    }
                    //    _flex.Redraw = true;
                    //    break;
                }

                if (_dtH != null && _dtH.Rows.Count > 0)
                {
                    string[] Args = ((Control)sender).Tag.ToString().Split(';');
                    _dtH.Rows[0][Args[0].ToString()] = e.CodeValue;
                    _dtH.Rows[0][Args[1].ToString()] = e.CodeName;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 도움창 변경 이벤트(Control_CodeChanged)

        private void Control_CodeChanged(object sender, EventArgs e)
        {
            try
            {
                BpCodeTextBox bp = sender as BpCodeTextBox;
                if (bp == null) return;

                if (bp.CodeValue == "")
                {
                    if (_dtH != null && _dtH.Rows.Count > 0)
                    {
                        string[] Args = ((BpCodeTextBox)sender).Tag.ToString().Split(';');
                        _dtH.Rows[0][Args[0].ToString()] = string.Empty;
                        _dtH.Rows[0][Args[1].ToString()] = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region ♣ Control 이벤트 / 메소드

        #region -> 콤보박스 키 이벤트(Cbo_KeyDown)

        private void Cbo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            try
            {
                switch (((Control)sender).Name)
                {
                    case "tb_dc":
                        if (e.KeyData == Keys.Enter || e.KeyData == Keys.Tab)
                            btn_insert_Click(null, null);
                        break;
                    default:
                        if (e.KeyData == Keys.Enter)
                            SendKeys.SendWait("{TAB}");
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region ♣ 기타메소드

        #region -> 요청적용 버튼 클릭이벤트(btn_REQ_Click)

        private void btn_REQ_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (!Feild_Check("요청")) return;

                P_PU_GIREQ_SUB3 dlg = new P_PU_GIREQ_SUB3(this.cb_cd_plant.SelectedValue.ToString(), tb_cd_qtio.CodeValue, tb_cd_qtio.CodeName, _flex.DataTable);

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    _flex.DataTable.Clear();

                    DataTable dtL = dlg.dtL;
                    string[] gi_sub3_value = dlg.str_value;
                    bp_CD_SL.CodeValue = dtL.Rows[0]["CD_SL"].ToString();
                    bp_CD_SL.CodeName = dtL.Rows[0]["NM_SL"].ToString();

                    bp입고창고.CodeValue = dtL.Rows[0]["CD_GRSL"].ToString();
                    bp입고창고.CodeName = dtL.Rows[0]["NM_GRSL"].ToString();

                    

                    if (dtL.Rows.Count > 0)
                    {
                        _flex.Redraw = false;
                        for (int i = 0; i < dtL.Rows.Count; i++)
                        {
                            _flex.Rows.Add();
                            _flex.Row = _flex.Rows.Count - 1;

                            _flex["CD_ITEM"] = dtL.Rows[i]["CD_ITEM"].ToString();
                            _flex["NM_ITEM"] = dtL.Rows[i]["NM_ITEM"].ToString();
                            _flex["STND_ITEM"] = dtL.Rows[i]["STND_ITEM"].ToString();
                            _flex["UNIT_IM"] = dtL.Rows[i]["UNIT_IM"].ToString();
                            _flex["UNIT_PO"] = dtL.Rows[i]["UNIT_PO"].ToString();
                                                            
                            if (_flex.Row == _flex.Rows.Fixed)
                                _flex["NO_IOLINE"] = 1;
                            else
                                _flex["NO_IOLINE"] = _flex.GetMaxValue("NO_IOLINE") + 2;

                            _flex["NO_LOT"] = dtL.Rows[i]["NO_LOT"].ToString();
                            _flex["NO_SERL"] = dtL.Rows[i]["NO_SERL"].ToString();
                            _flex["QT_GOOD_INV"] = D.GetDecimal(dtL.Rows[i]["QT"]);
                            _flex["QT_UNIT_PO"] = calc이동수량("QT_GOOD_INV", D.GetDecimal(dtL.Rows[i]["QT"]), D.GetDecimal(dtL.Rows[i]["UNIT_PO_FACT"]));
                            _flex["NO_ISURCV"] = dtL.Rows[i]["NO_GIREQ"].ToString();
                            _flex["NO_ISURCVLINE"] = dtL.Rows[i]["NO_LINE"].ToString();
                            _flex["CD_SL"] = dtL.Rows[i]["CD_SL"].ToString();
                            _flex["CD_SL_REF"] = dtL.Rows[i]["CD_GRSL"].ToString();
                            _flex["출고창고"] = dtL.Rows[i]["NM_SL"].ToString();


                            _flex["입고창고"] = dtL.Rows[i]["NM_GRSL"].ToString();
                            _flex["CD_PARTNER"] = dtL.Rows[i]["CD_PARTNER"].ToString();
                            _flex["NO_EMP"] = dtL.Rows[i]["NO_EMP"].ToString();
                            _flex["요청자"] = dtL.Rows[i]["NM_KOR"].ToString();
                            _flex["요청부서"] = dtL.Rows[i]["NM_DEPT"].ToString();

                            //추가 20081230 -LOT재고창에서 조회안되는 문제처리 
                            _flex["CD_QTIOTP"] = _header.CurrentRow["CD_QTIOTP"];
                            _flex["CD_PLANT"]  = _header.CurrentRow["CD_PLANT"];
                            _flex["DT_IO"]     = _header.CurrentRow["DT_IO"];
                            _flex["YN_RETURN"] = "N";
                            _flex["YN_AM"] = _header.CurrentRow["YN_AM"];
                            _flex["NO_ISURCV"] = dtL.Rows[i]["NO_GIREQ"].ToString();
                            _flex["CD_ZONE"] = D.GetString(dtL.Rows[i]["CD_ZONE"]);
                            _flex["CLS_ITEM"] = D.GetString(dtL.Rows[i]["CLS_ITEM"]);
                            _flex["NM_MAKER"] = D.GetString(dtL.Rows[i]["NM_MAKER"]);
                            _flex["NO_DESIGN"] = D.GetString(dtL.Rows[i]["NO_DESIGN"]);
                            

                            if (bp입고창고.CodeName == "")
                            {
                                _flex["FG_SLQC"] = D.GetString(dtL.Rows[i]["FG_SLQC"]);
                            }
                            else if (_biz.FG_SLQC("CD_SL", "", D.GetString(cb_cd_plant.SelectedValue), bp입고창고.CodeValue) == "Y")
                            {
                                _flex["FG_SLQC"] = D.GetString(dtL.Rows[i]["FG_SLQC"]);
                            }
                            else
                            {
                                _flex["FG_SLQC"] = "N";
                            }
                           

                            if (gi_sub3_value[0] == "Y" && D.GetString(dtL.Rows[i]["DC50_PO"]) != "")
                            {
                                
                                    tb_dc.Text = D.GetString(dtL.Rows[i]["DC50_PO"]);
                                    _header.CurrentRow["DC_RMK"] = D.GetString(dtL.Rows[i]["DC50_PO"]);
                            }

                            //decimal qt_pinvn = BASICPU.Item_PINVN(D.GetString(_header.CurrentRow["CD_PLANT"]), D.GetString(_flex["CD_SL"]), D.GetString(_flex["CD_ITEM"]));
                            //_flex["QT_GOODS"] = D.GetDecimal(qt_pinvn);






                            _flex["NM_ITEMGRP"] = dtL.Rows[i]["NM_ITEMGRP"].ToString();
                            _flex["FG_SERNO"] = dtL.Rows[i]["FG_SERNO"].ToString();

                            _flex["NO_MREQ"] = D.GetString(dtL.Rows[i]["NO_MREQ"]);
                            _flex["NO_MREQLINE"] = D.GetString(dtL.Rows[i]["NO_MREQLINE"]);

                            _flex["BARCODE"] = D.GetString(dtL.Rows[i]["BARCODE"]);
                            _flex["QT_GIREQ"] = D.GetDecimal(dtL.Rows[i]["QT"]);
                            _flex["FG_GUBUN"] = D.GetString(dtL.Rows[i]["FG_GUBUN"]);

                            _flex["NO_TRACK"] = D.GetString(dtL.Rows[i]["NO_SO"]);
                            _flex["NO_TRACK_LINE"] = D.GetString(dtL.Rows[i]["SEQ_SO"]);


                            if (프로젝트사용)
                                Only_Pjt(dtL.Rows[i]);

                            decimal qt_pinvn = 0;
                            if (Config.MA_ENV.PJT형여부 == "N")
                            {
                                qt_pinvn = BASICPU.Item_PINVN(D.GetString(_header.CurrentRow["CD_PLANT"]), D.GetString(_flex["CD_SL"]), D.GetString(_flex["CD_ITEM"]));
                            }
                            else
                            {
                                qt_pinvn = BASICPU.Item_PINVN_PJT(D.GetString(_header.CurrentRow["CD_PLANT"]), D.GetString(_flex["CD_SL"]), D.GetString(_flex["CD_ITEM"]), D.GetString(_flex["CD_PROJECT"]), ((Config.MA_ENV.YN_UNIT == "Y") ? D.GetDecimal(_flex["SEQ_PROJECT"]) : 0));
                            }
                            _flex["QT_GOODS"] = D.GetDecimal(qt_pinvn);

                            _flex["GI_PARTNER"] = dtL.Rows[i]["GI_PARTNER"];
                            _flex["LN_GI_PARTNER"] = dtL.Rows[i]["LN_GI_PARTNER"];

                            if (Global.MainFrame.ServerKeyCommon.Contains("HANSU"))
                            {
                                _flex["CD_ITEM_PARTNER"] = dtL.Rows[i]["CD_ITEM_PARTNER"];
                                _flex["NM_ITEM_PARTNER"] = dtL.Rows[i]["NM_ITEM_PARTNER"];
                                _flex["CD_PACK"] = dtL.Rows[i]["CD_PACK"];
                                _flex["NM_PACK"] = dtL.Rows[i]["NM_PACK"];
                                _flex["CD_TRANSPORT"] = dtL.Rows[i]["CD_TRANSPORT"];
                                _flex["CD_PART"] = dtL.Rows[i]["CD_PART"];
                                _flex["NM_PART"] = dtL.Rows[i]["NM_PART"];
                                _flex["YN_TEST_RPT"] = dtL.Rows[i]["YN_TEST_RPT"];
                                _flex["DC_DESTINATION"] = dtL.Rows[i]["DC_DESTINATION"];
                                _flex["DC_RMK_REQ"] = dtL.Rows[i]["DC_RMK"];
                                _flex["DT_DELIVERY"] = dtL.Rows[i]["DT_DELIVERY"];
                                _flex["NUM_USERDEF1"] = dtL.Rows[i]["NUM_USERDEF1"];
                                _flex["CD_SALEGRP"] = dtL.Rows[i]["CD_SALEGRP"];
                                _flex["LN_PARTNER"] = dtL.Rows[i]["LN_PARTNER"];
                                _flex["PRIOR_GUBUN"] = dtL.Rows[i]["PRIOR_GUBUN"];
                            }

                            _flex.AddFinished(); 
                        }
                        fg_sub = "요청";
                        _flex.Redraw = true;
                    }

                    bp_CD_SL.Enabled = false;
                    bp입고창고.Enabled = false;
                    //b_Append.Enabled = false;

                    if (_flex.DataTable != null && _flex.DataTable.Rows.Count > 0)
                    {
                        tb_DT_IO.Enabled = false;
                        tb_cd_qtio.Enabled = false;
                        cb_cd_plant.Enabled = false;
                        tb_no_emp.Enabled = false;
                    }

                    if (Global.MainFrame.ServerKeyCommon == "TOKIMEC")
                    {
                        b_Append.Enabled = false;
                        //b_Delete.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 외주의뢰적용 버튼 클릭이벤트(btn_SGIREQ_Click)

        private void btn_SGIREQ_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Feild_Check("외주")) return;

                P_PU_SGIREQ_SUB3 dlg = new P_PU_SGIREQ_SUB3(D.GetString(cb_cd_plant.SelectedValue), D.GetString(bp_CD_SL.CodeValue), D.GetString(bp_CD_SL.CodeName), bp입고창고.CodeValue, bp입고창고.CodeName, _flex.DataTable);

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    _flex.DataTable.Clear();

                    DataTable dtL = dlg.dtL;

                    if (dtL.Rows.Count > 0)
                    { 
                        _flex.Redraw = false;
                        for (int i = 0; i < dtL.Rows.Count; i++)
                        {
                            _flex.Rows.Add();
                            _flex.Row = _flex.Rows.Count - 1;

                            _flex["CD_ITEM"] = dtL.Rows[i]["CD_ITEM"].ToString();
                            _flex["NM_ITEM"] = dtL.Rows[i]["NM_ITEM"].ToString();
                            _flex["STND_ITEM"] = dtL.Rows[i]["STND_ITEM"].ToString();
                            _flex["UNIT_IM"] = dtL.Rows[i]["UNIT_IM"].ToString();
                            _flex["UNIT_PO"] = dtL.Rows[i]["UNIT_PO"].ToString();
                            _flex["NO_LOT"] = dtL.Rows[i]["NO_LOT"].ToString();
                            _flex["NO_SERL"] = dtL.Rows[i]["NO_SERL"].ToString();

                            if (_flex.Row == _flex.Rows.Fixed)
                                _flex["NO_IOLINE"] = 1;
                            else
                                _flex["NO_IOLINE"] = _flex.GetMaxValue("NO_IOLINE") + 2;


                            _flex["QT_GOOD_INV"] = _flex.CDecimal(dtL.Rows[i]["QT_REQ"].ToString());
                            _flex["QT_UNIT_PO"] = calc이동수량("QT_GOOD_INV", _flex.CDecimal(dtL.Rows[i]["QT_REQ"].ToString()), _flex.CDecimal(dtL.Rows[i]["UNIT_PO_FACT"].ToString()));
                            _flex["NO_ISURCV"] = dtL.Rows[i]["NO_GIREQ"].ToString();
                            _flex["NO_ISURCVLINE"] = dtL.Rows[i]["NO_LINE"].ToString();
                            _flex["NO_PSO_MGMT"] = dtL.Rows[i]["NO_PSO_MGMT"].ToString();
                            _flex["NO_PSOLINE_MGMT"] = dtL.Rows[i]["NO_PSOLINE_MGMT"];
                            _flex["CD_SL"] = dtL.Rows[i]["CD_SL"].ToString();
                            _flex["CD_SL_REF"] = dtL.Rows[i]["CD_GRSL"].ToString();
                            _flex["출고창고"] = dtL.Rows[i]["출고창고"].ToString();
                            _flex["입고창고"] = dtL.Rows[i]["입고창고"].ToString();
                            _flex["NO_EMP"] = dtL.Rows[i]["NO_EMP"].ToString();
                            _flex["요청자"] = dtL.Rows[i]["NM_KOR"].ToString();
                            _flex["요청부서"] = dtL.Rows[i]["NM_DEPT"].ToString();

                            //추가 20081230 -LOT재고창에서 조회안되는 문제처리 
                            _flex["CD_QTIOTP"] = _header.CurrentRow["CD_QTIOTP"];
                            _flex["CD_PLANT"] = _header.CurrentRow["CD_PLANT"];
                            _flex["DT_IO"] = _header.CurrentRow["DT_IO"];
                            _flex["YN_RETURN"] = "N";
                            _flex["YN_AM"] = _header.CurrentRow["YN_AM"];

                            _flex["FG_SERNO"] = dtL.Rows[i]["FG_SERNO"].ToString();
                            _flex["NM_ITEMGRP"] = dtL.Rows[i]["NM_ITEMGRP"].ToString();
                                                        
                            //decimal qt_pinvn = BASICPU.Item_PINVN(D.GetString(_header.CurrentRow["CD_PLANT"]), D.GetString(_flex["CD_SL"]), D.GetString(_flex["CD_ITEM"]));
                            //_flex["QT_GOODS"] = D.GetDecimal(qt_pinvn);


                            _flex["QT_GIREQ"] = _flex.CDecimal(dtL.Rows[i]["QT_GIREQ"].ToString());


                            _flex["BARCODE"] = D.GetString(dtL.Rows[i]["BARCODE"]);
                            _flex["CD_ZONE"] = D.GetString(dtL.Rows[i]["CD_ZONE"]);
                            _flex["CLS_ITEM"] = D.GetString(dtL.Rows[i]["CLS_ITEM"]);
                            _flex["LN_PARTNER"] = D.GetString(dtL.Rows[i]["LN_PARTNER"]);
                            _flex["NM_MAKER"] = D.GetString(dtL.Rows[i]["NM_MAKER"]);

                            if (_biz.FG_SLQC("CD_SL", "", D.GetString(cb_cd_plant.SelectedValue), dtL.Rows[i]["입고창고"].ToString()) == "Y")
                                _flex["FG_SLQC"] = D.GetString(dtL.Rows[i]["FG_SLQC"]);
                            else
                                _flex["FG_SLQC"] = "N";

                            if (프로젝트사용)
                                Only_Pjt(dtL.Rows[i]);

                            decimal qt_pinvn = 0;
                            if (Config.MA_ENV.PJT형여부 == "N")
                            {
                                qt_pinvn = BASICPU.Item_PINVN(D.GetString(_header.CurrentRow["CD_PLANT"]), D.GetString(_flex["CD_SL"]), D.GetString(_flex["CD_ITEM"]));
                            }
                            else
                            {
                                qt_pinvn = BASICPU.Item_PINVN_PJT(D.GetString(_header.CurrentRow["CD_PLANT"]), D.GetString(_flex["CD_SL"]), D.GetString(_flex["CD_ITEM"]), D.GetString(_flex["CD_PROJECT"]), ((Config.MA_ENV.YN_UNIT == "Y") ? D.GetDecimal(_flex["SEQ_PROJECT"]) : 0));
                            }
                            _flex["QT_GOODS"] = D.GetDecimal(qt_pinvn);

                            _flex.AddFinished();
                        }
                         fg_sub = "외주";
                        _flex.Redraw = true;


                    }
                    bp_CD_SL.Enabled = true;
                    bp입고창고.Enabled = true;
                    //b_Append.Enabled = false;

                    if (_flex.DataTable != null && _flex.DataTable.Rows.Count > 0)
                    {
                        tb_DT_IO.Enabled = false;
                        tb_cd_qtio.Enabled = false;
                        cb_cd_plant.Enabled = false;
                        tb_no_emp.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 생산의뢰적용 버튼 클릭 이벤트(btn_WGIREQ_Click)

        private void btn_WGIREQ_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Feild_Check("생산")) return;

                P_PU_WGIREQ02_SUB dlg = new P_PU_WGIREQ02_SUB(new string[] { cb_cd_plant.SelectedValue.ToString(), cb_cd_plant.Text,
                                                                             bp_CD_SL.CodeValue, bp_CD_SL.CodeName,
                                                                             bp입고창고.CodeValue, bp입고창고.CodeName});

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    _flex.DataTable.Clear();

                    DataRow[] drs = dlg.ReturnDataRowArr;

                    _flex.Redraw = false;
                  
                    foreach (DataRow dr in drs)
                    {
                        _flex.Rows.Add();
                        _flex.Row = _flex.Rows.Count - 1;

                        _flex["CD_ITEM"] = dr["CD_ITEM"].ToString();
                        _flex["NM_ITEM"] = dr["NM_ITEM"].ToString();
                        _flex["STND_ITEM"] = dr["STND_ITEM"].ToString();
                        _flex["UNIT_IM"] = dr["UNIT_MO"].ToString();
                        _flex["UNIT_PO"] = dr["UNIT_PO"].ToString();
                        _flex["NO_LOT"] = D.GetString(dr["NO_LOT"]);
                        _flex["NO_SERL"] = D.GetString(dr["NO_SERL"]);

                        if (_flex.Row == _flex.Rows.Fixed)
                            _flex["NO_IOLINE"] = 1;
                        else
                            _flex["NO_IOLINE"] = _flex.GetMaxValue("NO_IOLINE") + 2;

                        _flex["QT_GOOD_INV"] = _flex.CDecimal(dr["QT_REQ"].ToString());
                        _flex["QT_UNIT_PO"] = calc이동수량("QT_GOOD_INV", _flex.CDecimal(dr["QT_REQ"].ToString()), _flex.CDecimal(dr["UNIT_PO_FACT"].ToString()));
                        _flex["NO_ISURCV"] = dr["NO_GIREQ"].ToString();   //요청번호
                        _flex["NO_ISURCVLINE"] = dr["NO_LINE"].ToString();
                        _flex["NO_PSO_MGMT"] = dr["NO_PSO_MGMT"].ToString(); //작업지시
                        _flex["NO_PSOLINE_MGMT"] = dr["NO_PSOLINE_MGMT"].ToString();
                        _flex["CD_SL"] = dr["CD_SL"].ToString();
                        _flex["CD_SL_REF"] = dr["CD_GRSL"].ToString();
                        _flex["출고창고"] = dr["출고창고"].ToString();
                        _flex["입고창고"] = dr["입고창고"].ToString();
                        _flex["NO_EMP"] = dr["NO_EMP"].ToString();
                        _flex["요청자"] = dr["NM_KOR"].ToString();
                        _flex["요청부서"] = dr["NM_DEPT"].ToString();

                        //추가 20081230 -LOT재고창에서 조회안되는 문제처리 
                        _flex["CD_QTIOTP"] = _header.CurrentRow["CD_QTIOTP"];
                        _flex["CD_PLANT"] = _header.CurrentRow["CD_PLANT"];
                        _flex["DT_IO"] = _header.CurrentRow["DT_IO"];
                        _flex["YN_RETURN"] = "N";
                        _flex["YN_AM"] = _header.CurrentRow["YN_AM"];

                        _flex["FG_SERNO"] = dr["FG_SERNO"].ToString();
                        _flex["NM_ITEMGRP"] = dr["NM_ITEMGRP"].ToString(); 

                        //decimal qt_pinvn = BASICPU.Item_PINVN(D.GetString(_header.CurrentRow["CD_PLANT"]), D.GetString(_flex["CD_SL"]), D.GetString(_flex["CD_ITEM"]));
                        //_flex["QT_GOODS"] = D.GetDecimal(qt_pinvn);


                        
                        _flex["BARCODE"] = D.GetString(dr["BARCODE"]);
                        _flex["CD_ZONE"] = D.GetString(dr["CD_ZONE"]);
                        _flex["CLS_ITEM"] = D.GetString(dr["CLS_ITEM"]);
                        _flex["NM_MAKER"] = D.GetString(dr["NM_MAKER"]);
                        _flex["QT_GIREQ"] = D.GetString(dr["QT_GIREQ"]);

                        if (_biz.FG_SLQC("CD_SL", "", D.GetString(cb_cd_plant.SelectedValue), dr["입고창고"].ToString()) == "Y")
                            _flex["FG_SLQC"] = D.GetString(dr["FG_SLQC"]);
                        else
                            _flex["FG_SLQC"] = "N";

                        if (프로젝트사용)
                            Only_Pjt(dr);

                        decimal qt_pinvn = 0;
                        if (Config.MA_ENV.PJT형여부 == "N")
                        {
                            qt_pinvn = BASICPU.Item_PINVN(D.GetString(_header.CurrentRow["CD_PLANT"]), D.GetString(_flex["CD_SL"]), D.GetString(_flex["CD_ITEM"]));
                        }
                        else
                        {
                            qt_pinvn = BASICPU.Item_PINVN_PJT(D.GetString(_header.CurrentRow["CD_PLANT"]), D.GetString(_flex["CD_SL"]), D.GetString(_flex["CD_ITEM"]), D.GetString(_flex["CD_PROJECT"]), ((Config.MA_ENV.YN_UNIT == "Y") ? D.GetDecimal(_flex["SEQ_PROJECT"]) : 0));
                        }
                        _flex["QT_GOODS"] = D.GetDecimal(qt_pinvn);
                        _flex["DC_RMK"] = D.GetString(dr["DC_RMK"]);

                        _flex["NM_CLS_L"] = D.GetString(dr["NM_CLS_L"]);
                        _flex["NM_CLS_M"] = D.GetString(dr["NM_CLS_M"]);
                        _flex["NM_CLS_S"] = D.GetString(dr["NM_CLS_S"]);

                        _flex.AddFinished();
                    }
                    fg_sub = "생산";
                    의뢰번호Visible(fg_sub);
                    bp_CD_SL.Enabled = true;
                    bp입고창고.Enabled = true;
                    //b_Append.Enabled = false;

                    if (_flex.DataTable != null && _flex.DataTable.Rows.Count > 0)
                    {
                        tb_DT_IO.Enabled = false;
                        tb_cd_qtio.Enabled = false;
                        cb_cd_plant.Enabled = false;
                        tb_no_emp.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                _flex.Redraw = true;
            }
        }

        #endregion

        #region ->  D2계획적용 버튼 클릭 이벤트(btnD2계획적용_Click)
        private void btnD2계획적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Feild_Check("요청")) return;

                P_PU_D2_PLAN_SUB dlg = new P_PU_D2_PLAN_SUB();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _flex.DataTable.Clear();
                    Grid_BOMAfter(dlg.dt_reutrn, "CD_ITEM");

                    if (_flex.DataTable != null && _flex.DataTable.Rows.Count > 0)
                    {
                        tb_DT_IO.Enabled = false;
                        tb_cd_qtio.Enabled = false;
                        cb_cd_plant.Enabled = false;
                        tb_no_emp.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region ->  수주적용 버튼 클릭 이벤트(btnD2btn수주적용_Click)
        private void btn수주적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Feild_Check("요청")) return;

                DataTable dt = _flex.DataTable;

                object[] obj = {
                                   D.GetString(cb_cd_plant.SelectedValue),
                                   D.GetString(cb_cd_plant.Text),
                                   "SL",
                                   dt
                               };


                P_PU_PO_SO_SUB dlg = new P_PU_PO_SO_SUB(obj);

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    _flex.DataTable.Clear();
                    Grid_BOMAfter(dlg.수주데이터, "CD_ITEM");
                    this.ToolBarDeleteButtonEnabled = false;

                    if (_flex.DataTable != null && _flex.DataTable.Rows.Count > 0)
                    {
                        tb_DT_IO.Enabled = false;
                        tb_cd_qtio.Enabled = false;
                        cb_cd_plant.Enabled = false;
                        tb_no_emp.Enabled = false;
                    }
                }

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region -> btn_insert_Click 추가 버턴

        private void btn_insert_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (!b_Append.Enabled) return;

                if (!Feild_Check("추가")) return;

                _flex.Rows.Add();
                _flex.Row = _flex.Rows.Count - 1;

                //_flex["NO_IO"] = tb_no_io.Text; //savedata()에서 넣어줌

                //decimal MaxSeq = _flex.GetMaxValue("NO_IOLINE"); //savedata()에서 넣어줌
                //if (MaxSeq == 0)
                //   MaxSeq = 1;
                //else
                //   MaxSeq = MaxSeq + 2;

               _flex["S"] = "N";

               //if (_flex.Row == _flex.Rows.Fixed)//savedata()에서 넣어줌
               //    _flex["NO_IOLINE"] = 1;
               //else
               //    _flex["NO_IOLINE"] = _flex.GetMaxValue("NO_IOLINE") + 2;

                _flex["CD_ITEM"] = "";
                _flex["QT_GOOD_INV"] = 0;
                _flex["QT_REJECT_INV"] = 0;

                _flex["NO_ISURCV"] = "";
                _flex["NO_ISURCVLINE"] = 0;

                if (D.GetString(bp_CD_SL.CodeValue) != string.Empty)
                {
                    _flex["CD_SL"] = D.GetString(bp_CD_SL.CodeValue);
                    _flex["출고창고"] = D.GetString(bp_CD_SL.CodeName);
                }

                if (bp입고창고.CodeValue.ToString() != string.Empty)
                {
                    _flex["CD_SL_REF"] = bp입고창고.CodeValue.ToString();
                    _flex["입고창고"] = bp입고창고.CodeName.ToString();
                }

                if (PJT형여부) _flex["SEQ_PROJECT"] = 0;

                _flex.AddFinished();
                _flex.Col = _flex.Cols.Fixed;

                _flex.Focus();

                ToolBarSaveButtonEnabled = true;
                ToolBarAddButtonEnabled = true;
                tb_DT_IO.Enabled = false;
                b_Delete.Enabled = true;
                btn_REQ.Enabled = false;
                btn_SGIREQ.Enabled = false;
                btn_WGIREQ.Enabled = false;
                tb_cd_qtio.Enabled = false;
                cb_cd_plant.Enabled = false;
                tb_no_emp.Enabled = false;
                
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        #region -> btn_delete_Click 삭제 버턴

        private void btn_delete_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (D.GetString(_header.CurrentRow["NO_QC"]) != "") //(주)이노와이어리스 전용
                {
                    ShowMessage("검사처리 항목은 삭제할 수 없습니다. 검사번호 : " + D.GetString(_header.CurrentRow["NO_QC"]));
                    return;
                }

                DataRow[] rows = _flex.DataTable.Select("S ='Y'");
                if (rows == null || rows.Length <= 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                _flex.Redraw = false;

                if (rows != null & rows.Length > 0)
                {
                    for (int i = 0; i < rows.Length; i++)
                    {
                        rows[i].Delete();
                    }
                }
                _flex.Redraw = true;

                if (_flex.DataView.Count <= 0)
                {
                    b_Append.Enabled = true;
                    btn_REQ.Enabled = true;
                    btn_SGIREQ.Enabled = true;
                    btn_WGIREQ.Enabled = true;
                    b_Delete.Enabled = false;
                    ToolBarSaveButtonEnabled = false;
                    tb_DT_IO.Enabled = true;
                    tb_cd_qtio.Enabled = true;
                    cb_cd_plant.Enabled = true;
                    tb_no_emp.Enabled = true;
                }
            }

            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                _flex.Redraw = true;
            }
        }

        #endregion

        #region -> 엑셀업로드

        private void btn_ExcelUpload_Click(object sender, EventArgs e)
        {
            try
            {

                if (!Feild_Check("요청")) return;


                OpenFileDialog m_FileDlg = new OpenFileDialog();
                Duzon.Common.Util.Excel excel = null;
                DataTable _dt_EXCEL = null;
                m_FileDlg.Filter = "엑셀 파일 (*.xls)|*.xls";


                if (m_FileDlg.ShowDialog() == DialogResult.OK)
                {
                    Application.DoEvents();

                    string FileName = m_FileDlg.FileName;
                    string NO_ITEM = string.Empty; 
                    string MULTI_ITEM = string.Empty;

                    excel = new Duzon.Common.Util.Excel();
                    _dt_EXCEL = excel.StartLoadExcel(FileName);
                    int j = _flex.Rows.Count - _flex.Rows.Fixed;
                    decimal Maxline = 0;

                    MsgControl.ShowMsg(" 엑셀 업로드 중입니다. \n잠시만 기다려주세요!");

                    _flex.Redraw = false;
                    //_flex.EmptyRowFilter();

                    DataTable dtLine = new DataTable();
                    DataTable MasterItemDt = new DataTable(); //품목테이블 
                    DataTable print_dt = _flex.DataTable;
                    DataTable temp_dt = null;

                    /* 엑셀의 품목이 많을 경우 Received Parameter가 OverFlow되는 문제를 사전에 방지하고자 일정 단위에서 쪼개서 Merge함으로 처리함 */
                    foreach (DataRow drSplit in _dt_EXCEL.Rows)
                    {
                        if (NO_ITEM != drSplit["CD_ITEM"].ToString())
                        {
                            NO_ITEM = drSplit["CD_ITEM"].ToString();
                            MULTI_ITEM += NO_ITEM + "|";
                        }
                    }


                    string[] No_PK_Multi_array = D.StringConvert.GetPipes(MULTI_ITEM, 200);


                    for (int i = 0; i < No_PK_Multi_array.Length; i++)
                    {
                        object[] obj = new object[] { Global.MainFrame.LoginInfo.CompanyCode, cb_cd_plant.SelectedValue, No_PK_Multi_array[i] };
                        temp_dt = _biz.Item_List_search(obj);

                        if (i == 0)//첫테이블은 복사하고 두번째 테이블부터는한줄씩 붙여넣는다
                        {
                            MasterItemDt = temp_dt.Copy();
                        }
                        else if (i > 0)
                        {
                            foreach (DataRow dr in temp_dt.Rows)
                                MasterItemDt.LoadDataRow(dr.ItemArray, true);
                        }

                    }

                    //창고가져오기
                    DataTable dt_SL = _biz.CD_SL_search(D.GetString(cb_cd_plant.SelectedValue));

                    StringBuilder checklist_item = new StringBuilder();

                    string msg = "품목코드  \t  출고창고 \t 입고창고";

                    checklist_item.AppendLine(msg);
                    msg = "-".PadRight(80, '-');

                    checklist_item.AppendLine(msg);

                    bool item_check = false;
                    bool cd_sl_check = false;
                    DataRow NewRowItem;

                    bool  bDc_Rmk = _dt_EXCEL.Columns.Contains("DC_RMK");

                    Maxline = _flex.GetMaxValue("NO_IOLINE") == 0 ? -1 : _flex.GetMaxValue("NO_IOLINE");
                    foreach (DataRow dr in _dt_EXCEL.Rows)
                    {
                        #region -> 엑셀 Data 검증 ( 품목 체크 )

                        DataRow[] drs_pitem = MasterItemDt.Select("CD_ITEM = '" + D.GetString(dr["CD_ITEM"]) + "'");
                        DataRow[] drs_GISL = dt_SL.Select("CD_SL = '" + D.GetString(dr["GI_SL"]) + "'");
                        DataRow[] drs_GRSL = dt_SL.Select("CD_SL = '" + D.GetString(dr["GR_SL"]) + "'");

                        if (drs_pitem.Length > 0 && drs_GISL.Length > 0 && drs_GRSL.Length >0)
                        {

                            Maxline = Maxline + 2;

                            NewRowItem = print_dt.NewRow();

                            NewRowItem["S"] = "N";
                            NewRowItem["NO_IO"] = D.GetString(tb_no_io.Text);
                            NewRowItem["NO_IOLINE"] = Maxline;

                            NewRowItem["CD_ITEM"] = dr["CD_ITEM"].ToString();
                            NewRowItem["NM_ITEM"] = D.GetString(drs_pitem[0]["NM_ITEM"]);
                            NewRowItem["STND_ITEM"] = drs_pitem[0]["STND_ITEM"];
                            NewRowItem["UNIT_IM"] = drs_pitem[0]["UNIT_IM"];
                            NewRowItem["UNIT_PO"] = drs_pitem[0]["UNIT_PO"];
                            NewRowItem["NM_MAKER"] = drs_pitem[0]["NM_MAKER"];

                            NewRowItem["QT_GOOD_INV"] = D.GetString(dr["QT_GOOD_INV"]);
                            NewRowItem["QT_UNIT_PO"] = calc이동수량("QT_GOOD_INV", D.GetDecimal(dr["QT_GOOD_INV"]), D.GetDecimal(drs_pitem[0]["UNIT_PO_FACT"]))
                                ;

                            NewRowItem["CD_SL"] = D.GetString(dr["GI_SL"]);
                            NewRowItem["출고창고"] = D.GetString(drs_GISL[0]["NM_SL"]);
                            bp_CD_SL.CodeValue = D.GetString(dr["GI_SL"]);
                            bp_CD_SL.CodeName = D.GetString(drs_GISL[0]["NM_SL"]);

                            NewRowItem["CD_SL_REF"] = D.GetString(dr["GR_SL"]);
                            NewRowItem["입고창고"] = D.GetString(drs_GRSL[0]["NM_SL"]);
//decimal A = BASICPU.Item_PINVN(D.GetString(cb_cd_plant.SelectedValue), D.GetString(drs_GISL[1]), D.GetString(dr["CD_ITEM"]));


                            if (_biz.FG_SLQC("CD_SL", "", D.GetString(cb_cd_plant.SelectedValue), D.GetString(dr["GR_SL"])) == "Y")
                                NewRowItem["FG_SLQC"] = D.GetString(drs_pitem[0]["FG_SLQC"]);
                            else
                                NewRowItem["FG_SLQC"] = "N";

                            NewRowItem["FG_IO"] = "022";
                            
                            NewRowItem["CD_QTIOTP"] = _header.CurrentRow["CD_QTIOTP"];
                            NewRowItem["CD_PLANT"] = _header.CurrentRow["CD_PLANT"];
                            NewRowItem["DT_IO"] = _header.CurrentRow["DT_IO"];
                            NewRowItem["YN_RETURN"] = "N";
                            NewRowItem["YN_AM"] = _header.CurrentRow["YN_AM"];

                            if (MNG_LOT == "Y" && drs_pitem[0]["FG_SERNO"].ToString() == "002")
                            {
                                NewRowItem["NO_LOT"] = "YES";
                                NewRowItem["FG_SERNO"] = "LOT";
                            }
                            else
                            {
                                NewRowItem["NO_LOT"] = "NO";
                                NewRowItem["FG_SERNO"] = "미관리";
                                if (MNG_SERIAL == "Y" && drs_pitem[0]["FG_SERNO"].ToString() == "003")
                                {
                                    NewRowItem["NO_SERL"] = "YES";
                                    NewRowItem["FG_SERNO"] = "S/N";
                                }
                                else
                                    NewRowItem["NO_SERL"] = "NO";
                            }

                            NewRowItem["NM_QTIOTP"] = tb_cd_qtio.CodeName;


                            NewRowItem["NO_ISURCV"] = "";
                            NewRowItem["NO_ISURCVLINE"] = 0;

                            if (bDc_Rmk == true)
                                NewRowItem["DC_RMK"] = dr["DC_RMK"];

                            print_dt.Rows.Add(NewRowItem);
                        }
                        else
                        {
                            string CD_ITEM = dr["CD_ITEM"].ToString().PadRight(10, ' ');
                            string GI_SL = D.GetString(dr["GI_SL"]);
                            string GR_SL = D.GetString(dr["GR_SL"]);
                           
                            string msg2 = CD_ITEM + " \t " + GI_SL +" \t" + GR_SL +" \n";

                            checklist_item.AppendLine(msg2);

                            if(drs_pitem.Length == 0) item_check = true;
                            if (drs_GISL.Length == 0 || drs_GRSL.Length == 0) cd_sl_check = true;


                            continue;
                        }



                        /* *********************************************************************************************** */

                        /* *********************************************************************************************** */
                    }   // 엑셀 데이타 for문 끝

                    if (item_check || cd_sl_check)
                    {
                        MsgControl.CloseMsg();

                        if(item_check == true) 
                            ShowDetailMessage("엑셀 업로드하는 중에 [공장품목]에 불일치 항목들이 존재합니다. \n " +
                        " \n ▼ 버튼을 눌러서 목록을 확인하세요!", D.GetString(checklist_item));
                        else if(cd_sl_check == true)
                            ShowDetailMessage("엑셀 업로드하는 중에 [창고]에 불일치 항목들이 존재합니다. \n " +
                   " \n ▼ 버튼을 눌러서 목록을 확인하세요!", D.GetString(checklist_item));
                    }

                        #endregion

                    /* *********************************************************************************************** */

                    MsgControl.CloseMsg();
                    _flex.AddFinished();
                    _flex.Col = _flex.Cols.Fixed;
                    _flex.Row = _flex.Rows.Fixed;

                    _flex.Focus();

                    ToolBarSaveButtonEnabled = true;
                    ToolBarAddButtonEnabled = true;
                    tb_DT_IO.Enabled = false;
                    b_Delete.Enabled = true;
                    btn_REQ.Enabled = false;
                    btn_SGIREQ.Enabled = false;
                    btn_WGIREQ.Enabled = false;
                    tb_cd_qtio.Enabled = false;
                    cb_cd_plant.Enabled = false;
                    tb_no_emp.Enabled = false;

                    ShowMessage("엑셀 작업을 완료하였습니다.");

                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
                _flex.Redraw = true;


            }
        }

        #endregion

        #region -> Feild_Check

        private bool Feild_Check(string pstr_gubun)
        {
            if (tb_DT_IO.Text == string.Empty)
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, lb_dt_trans.Text);
                return false;
            }

            if (tb_cd_qtio.CodeValue == string.Empty)
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, lb_cd_qtio.Text);
                return false;
            }
            if (cb_cd_plant.SelectedValue.ToString() == string.Empty)
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, lb_nm_plant.Text);
                return false;
            }
            if (tb_no_emp.CodeValue == string.Empty)
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, lb_no_emp.Text);
                return false;
            }
            if (pstr_gubun == "지시소요량" )
            {
                if (D.GetString(bp_CD_SL.CodeName) == string.Empty)
                {
                    ShowMessage("출고창고를 지정하십시요");
                    bp_CD_SL.Focus();
                    return false;
                }

                if (D.GetString(bp입고창고.CodeName) == string.Empty)
                {
                    ShowMessage("입고창고를 지정하십시요");
                    bp입고창고.Focus();
                    return false;
                }


            }



            return true;
        }

        #endregion

        #region -> Save_Check

        private bool Save_Check()
        {
            if (_header.CurrentRow["CD_SL"].ToString() != string.Empty && _header.CurrentRow["CD_SL_REF"].ToString() != string.Empty)
            {
                if (string.Compare(_header.CurrentRow["CD_SL"].ToString(), _header.CurrentRow["CD_SL_REF"].ToString()) == 0)
                {
                    ShowMessage("같은 창고끼리는 이동 할 수 없습니다.");
                    return false;
                }
            }
            for(int row = _flex.Rows.Fixed; row < _flex.Rows.Count; row++) 
            {
                if (string.Compare(_flex[row, "CD_SL"].ToString(), _flex[row, "CD_SL_REF"].ToString()) == 0)
                {
                    ShowMessage("같은 창고끼리는 이동 할 수 없습니다.");
                    return false;
                }
            }

            if (PJT형여부 && _flex.HasNormalRow)
            {
                for (int _row = _flex.Rows.Fixed; _row < _flex.Rows.Count; _row++)
                {
                    if (D.GetString(_flex[_row,"CD_PROJECT"]) == string.Empty)
                    {
                        ShowMessage(공통메세지._은는필수입력항목입니다,DD("프로젝트"));
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion


        #region -> IsChanged

        protected override bool IsChanged()
        {
            if (base.IsChanged())       // 그리드가 변경되었거나
                return true;

            return 헤더변경여부;        // 헤더가 변경되었거나
        }

        #endregion

        #region -> 적용버튼
        private void btn_Appet_Click(object sender, EventArgs e)
        {
            try
            {
                String ColName = string.Empty, ColName2 = string.Empty;
                String Data = string.Empty, Data2 = string.Empty;

                switch (((RoundedButton)sender).Name)
                {
                    case "btn_GI_Accept":
                        ColName = "CD_SL";//출고창고 
                        ColName2 = "출고창고";
                        Data = D.GetString(bp_CD_SL.CodeValue);
                        Data2 = bp_CD_SL.CodeName;

                        break;

                    case "btn_GR_Accept":

                        ColName = "CD_SL_REF";// 입고창고
                        ColName2 = "입고창고";
                        Data = bp입고창고.CodeValue;
                        Data2 = bp입고창고.CodeName;

                        break;

                    case "btn프로젝트적용":

                        ColName = "CD_PROJECT";// 입고창고
                        ColName2 = "NM_PROJECT";
                        Data = ctx프로젝트.CodeValue;
                        Data2 = ctx프로젝트.CodeName;

                        break;
                }
                //DataRow[] dr = _flex.DataTable.Select("S = 'Y'");
                //foreach(DataRow dr in _flexD.DataTable.Rows)
                if (_flex.RowState() != DataRowState.Added)
                {
                    ShowMessage(DD("수정할수없습니다."));
                    return;
                }

                //for (int row = 0; row < dr.Length; row++)
                //{

                //    dr[row][ColName] = Data;
                //    if (ColName2 != string.Empty)
                //        dr[row][ColName2] = Data2;

                //    if (((RoundedButton)sender).Name == "btn_GR_Accept")
                //    {
                //        //if (_biz.FG_SLQC("CD_SL", "", D.GetString(cb_cd_plant.SelectedValue), bp입고창고.CodeName) != "Y")
                //        //    dr[row]["FG_SLQC"] = "N";
                //        _flex[row + 2, "FG_SLQC"] = "N";
                //    }
                //}

                _flex.Redraw = false;

                for (int row = _flex.Rows.Fixed; row < _flex.Rows.Count; row++)
                {
                    if (D.GetString(_flex[row, "S"]) == "N" || D.GetString(_flex[row, "S"]) == "")
                        continue;

                    _flex[row, ColName] = Data;
                    if (ColName2 != string.Empty)
                        _flex[row, ColName2] = Data2;

                    if (((RoundedButton)sender).Name == "btn_GR_Accept")
                    {
                        if (_biz.FG_SLQC("CD_SL", "", D.GetString(cb_cd_plant.SelectedValue), bp입고창고.CodeValue) != "Y")
                            _flex[row, "FG_SLQC"] = "N";
                        else
                            _flex[row, "FG_SLQC"] = _biz.FG_SLQC("ITEM", D.GetString(_flex[row, "CD_ITEM"]), D.GetString(cb_cd_plant.SelectedValue), "");
                    }
                    else if (((RoundedButton)sender).Name == "btn_GI_Accept") // 현재고 넣는부분
                    {
                        if (D.GetString(_flex[row, "CD_ITEM"]) != "")
                        {
                            //decimal qt_pinvn = BASICPU.Item_PINVN(D.GetString(_header.CurrentRow["CD_PLANT"]), D.GetString(_flex[row, "CD_SL"]), D.GetString(_flex[row, "CD_ITEM"]));
                            //_flex[row, "QT_GOODS"] = D.GetDecimal(qt_pinvn);

                            decimal qt_pinvn_sl = 0;
                            if (Config.MA_ENV.PJT형여부 == "N")
                            {
                                qt_pinvn_sl = BASICPU.Item_PINVN(D.GetString(_header.CurrentRow["CD_PLANT"]), D.GetString(_flex[row, "CD_SL"]), D.GetString(_flex[row, "CD_ITEM"]));
                            }
                            else
                            {
                                qt_pinvn_sl = BASICPU.Item_PINVN_PJT(D.GetString(_header.CurrentRow["CD_PLANT"]), D.GetString(_flex[row, "CD_SL"]), D.GetString(_flex[row, "CD_ITEM"]), D.GetString(_flex[row, "CD_PROJECT"]), ((Config.MA_ENV.YN_UNIT == "Y") ? D.GetDecimal(_flex[row, "SEQ_PROJECT"]) : 0));
                            }

                            _flex[row, "QT_GOODS"] = D.GetDecimal(qt_pinvn_sl);
                        }
                        else
                            _flex[row, "QT_GOODS"] = 0;

                    }
                    else if (((RoundedButton)sender).Name == "btn프로젝트적용") // 현재고 넣는부분
                    {
                        if (D.GetString(_flex[row, "CD_ITEM"]) != "")
                        {
                            //decimal qt_pinvn = BASICPU.Item_PINVN(D.GetString(_header.CurrentRow["CD_PLANT"]), D.GetString(_flex[row, "CD_SL"]), D.GetString(_flex[row, "CD_ITEM"]));
                            //_flex[row, "QT_GOODS"] = D.GetDecimal(qt_pinvn);

                            decimal qt_pinvn_sl = 0;
                            if (Config.MA_ENV.PJT형여부 == "N")
                            {
                                qt_pinvn_sl = BASICPU.Item_PINVN(D.GetString(_header.CurrentRow["CD_PLANT"]), D.GetString(_flex[row, "CD_SL"]), D.GetString(_flex[row, "CD_ITEM"]));
                            }
                            else
                            {
                                qt_pinvn_sl = BASICPU.Item_PINVN_PJT(D.GetString(_header.CurrentRow["CD_PLANT"]), D.GetString(_flex[row, "CD_SL"]), D.GetString(_flex[row, "CD_ITEM"]), D.GetString(_flex[row, "CD_PROJECT"]), ((Config.MA_ENV.YN_UNIT == "Y") ? D.GetDecimal(_flex[row, "SEQ_PROJECT"]) : 0));
                            }

                            _flex[row, "QT_GOODS"] = D.GetDecimal(qt_pinvn_sl);
                        }
                        else
                            _flex[row, "QT_GOODS"] = 0;

                        if (Config.MA_ENV.YN_UNIT == "Y")
                        {
                            _flex[row, "SEQ_PROJECT"] = d_SEQ_PROJECT;
                            _flex[row, "CD_PJT_ITEM"] = s_CD_PJT_ITEM;
                            _flex[row, "NM_PJT_ITEM"] = s_NM_PJT_ITEM;
                            _flex[row, "STND_UNIT"] = s_PJT_ITEM_STND;
                        }

                    }
 
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                _flex.Redraw = true;
            }
        }
        #endregion

        #region -> 서버키
        private bool 서버키(string pstr_서버키)
        {
            if (Global.MainFrame.ServerKeyCommon == pstr_서버키)
                return true;



            return false;
        }

        private bool 서버키_TEST포함(string pstr_서버키)
        {
            if (Global.MainFrame.ServerKeyCommon == pstr_서버키 || Global.MainFrame.ServerKeyCommon == "DZSQL" || Global.MainFrame.ServerKeyCommon == "SQL_" || Global.MainFrame.ServerKeyCommon == "SQL_108")
                return true;



            return false;
        }
        #endregion

        #endregion

        #region ♣ 속성들

        private bool 추가모드여부
        {
            get
            {
                if (_header.JobMode == JobModeEnum.추가후수정)
                    return true;

                return false;
            }
        }

        private bool 헤더변경여부
        {
            get
            {
                bool bChange = false;

                bChange = _header.GetChanges() != null ? true : false;

                // 헤더가 변경됬지만 추가모드이고 디테일 그리드가 아무 내용이 없으면 변경안된걸로 본다.
                if (bChange && _header.JobMode == JobModeEnum.추가후수정 && !_flex.HasNormalRow)
                    bChange = false;
                return bChange;
            }
        }

        #endregion

        #region -> bp_CD_SL_Search

        private void bp_CD_SL_Search(object sender, SearchEventArgs e)
        {
            master.P_MA_SL_AUTH_SUB dlg = null;

            try
            {
                dlg = new master.P_MA_SL_AUTH_SUB(cb_cd_plant.SelectedValue.ToString(), Global.MainFrame.LoginInfo.EmployeeNo);

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    bp_CD_SL.CodeValue = dlg.returnParams[0];   //창고번호
                    bp_CD_SL.CodeName = dlg.returnParams[1];      //창고이름

                    foreach (DataRow dr in _flex.DataTable.Rows)
                    {
                        dr["CD_SL"] = D.GetString(bp_CD_SL.CodeValue);
                        dr["출고창고"] = D.GetString(bp_CD_SL.CodeName);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        private void btn_APP_Click(object sender, EventArgs e)
        {
            try
            {
                switch (((Control)sender).Name)
                {
                    case "btn_BOM":
                        APP_BOM();
                        break;
                    case "btn지시소요량":
                        APP_PRWO();
                        break;
                }


            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void APP_BOM()
        {
//             if (bp작업장.CodeValue.ToString() == string.Empty)
//             {
//                 Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, "작업장");
//                 return;
//             }
//             if (bp공정명.CodeValue.ToString() == string.Empty)
//             {
//                 Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, "공정명");
//                 return;
//             }

            if (tb_cd_qtio.CodeValue == "")
            {
                Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, "수불형태");
                       return;
            }
            // 그룹화 할때 헤더정보를 모두끌고 들어가서 헤더의 정보를 보고 도움창이 그것을 가져다가 쓸수있게끔...하는것이 낫지 않을까???? 일단 지금은 이런식으로 넣는데 그룹화는 좀더 생각을 해봐야 할것같음
            P_PU_GIREQ_BOM_SUB m_dlg = new P_PU_GIREQ_BOM_SUB(D.GetString(cb_cd_plant.SelectedValue), tb_DT_IO.Text); // 요청일자가 필요함, 화면구분(PR:구매요청, APP:구매품의, PO:구매발주)//입고공장을 바라보게 되어있었는데 김헌섭대리와 이야기후 요청공장으로 바라보게 바꾸었음

            if (m_dlg.ShowDialog(this) == DialogResult.OK)
            {
                Grid_BOMAfter(m_dlg.dt_return, "BOM"); //수주적용과 함께쓰기위해.. 구분값을 "CD_MATL"로 두었음.


                if (_flex.DataTable != null && _flex.DataTable.Rows.Count > 0)
                {
                    this.ToolBarDeleteButtonEnabled = false;
                    //btn_Delete.Enabled = false;
                    //btn_Insert.Enabled = false;

                    tb_DT_IO.Enabled = false;
                    tb_cd_qtio.Enabled = false;
                    cb_cd_plant.Enabled = false;
                    tb_no_emp.Enabled = false;
                }
                else
                {
                    this.ToolBarDeleteButtonEnabled = true;
                    //btn_Delete.Enabled = true;
                    //btn_Insert.Enabled = true;
                }
            }

        }

        #region ->  지시소요량 적용처리
        private void APP_PRWO()
        {
            try
            {
                if (!Feild_Check(btn지시소요량.Text)) return;

                DataTable dt = _flex.DataTable;

                object[] obj = {
                                   D.GetString(cb_cd_plant.SelectedValue),
                                   D.GetString(cb_cd_plant.Text),
                                   "SL"
                               };


                P_PU_PRWO_RELEASE_SUB dlg = new P_PU_PRWO_RELEASE_SUB(D.GetString(cb_cd_plant.SelectedValue), bp_CD_SL.CodeValue, bp입고창고.CodeValue);

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    _flex.DataTable.Clear();
                    Grid_PRWO(dlg.dt_reutrn);
                    this.ToolBarDeleteButtonEnabled = false;
                    if (_flex.DataTable != null && _flex.DataTable.Rows.Count > 0)
                    {
                        tb_DT_IO.Enabled = false;
                        tb_cd_qtio.Enabled = false;
                        cb_cd_plant.Enabled = false;
                        tb_no_emp.Enabled = false;
                    }
                }

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        private void Grid_BOMAfter(DataTable dt_return, string gubun)
        {
            try
            {
                DataTable dt = _flex.DataTable;
                DataRow drD;
                //bool 첫줄여부 = true;


                decimal MaxSeq = _flex.GetMaxValue("NO_ISURCVLINE");

                _flex.Redraw = false;

                CommonFunction objFunc = new CommonFunction();

                foreach (DataRow dr in dt_return.Rows)
                {
                    drD = dt.NewRow();

                    if (gubun == "CD_MATL" || gubun == "BOM")
                    {
                        drD["CD_ITEM"] = dr["CD_MATL"];
                        drD["NM_ITEM"] = dr["NM_ITEM_MATL"];

                        drD["STND_ITEM"] = dr["STND_ITEM_MATL"];
                        drD["UNIT_IM"] = dr["UNIT_IM_MATL"];
                        drD["QT_GOOD_INV"] = dr["QT_ITEM_NET"];
                    }
                    else
                    {
                        drD["CD_ITEM"] = dr["CD_ITEM"];
                        drD["NM_ITEM"] = dr["NM_ITEM"];

                        drD["STND_ITEM"] = dr["STND_ITEM"];
                        drD["UNIT_IM"] = dr["UNIT_IM"];
                        
                        drD["QT_GOOD_INV"] = dr["QT_POREQ_IM"];
                    }
                    drD["UNIT_PO"] = dr["UNIT_PO"];
                    drD["QT_UNIT_PO"] = calc이동수량("QT_GOOD_INV", D.GetDecimal(drD["QT_GOOD_INV"]), D.GetDecimal(dr["UNIT_PO_FACT"]));

                    drD["출고창고"] = bp_CD_SL.CodeName;
                    drD["입고창고"] = bp입고창고.CodeName;

                    //if (gubun != "BOM")
                    //{
                    //    drD["NO_LOT"] = (dr["FG_SERNO"].ToString() == "002") ? "YES" : "NO";
                    //    drD["NO_SERL"] = (dr["FG_SERNO"].ToString() == "003") ? "YES" : "NO";// SERL 여부 
                    //}
                    //else
                    //{

                    //BOM도 하단 GetTableSearch에서 동일한 로직을 태우기 위해서 CD_MATL로 변경 
                    //gubun = "CD_MATL";
                    //}


                    //BOM일경우 FG_SERNO1 = 자재 시리얼 FG_SERNO = 모품목시리얼입니다.
                    if (gubun == "BOM")
                    {
                        drD["NO_LOT"] = (dr["FG_SERNO1"].ToString() == "002") ? "YES" : "NO";
                        drD["NO_SERL"] = (dr["FG_SERNO1"].ToString() == "003") ? "YES" : "NO";// SERL 여부 
                    }
                    else
                    {
                        drD["NO_LOT"] = (dr["FG_SERNO"].ToString() == "002") ? "YES" : "NO";
                        drD["NO_SERL"] = (dr["FG_SERNO"].ToString() == "003") ? "YES" : "NO";// SERL 여부 

                    }

                    drD["CD_PLANT"] = _header.CurrentRow["CD_PLANT"];
                    drD["CD_QTIOTP"] = tb_cd_qtio.CodeValue;
                    drD["NM_QTIOTP"] = tb_cd_qtio.CodeName;
                    drD["DT_IO"] = tb_DT_IO.Text;
                    drD["YN_RETURN"] = "N";
                    drD["YN_AM"] = _header.CurrentRow["YN_AM"];

                    drD["FG_SERNO"] = dr["FG_SN_LOT"].ToString();
                    drD["NM_ITEMGRP"] = dr["GRP_ITEMNM"].ToString();
                    drD["BARCODE"] = D.GetString(dr["BARCODE"]);
                    drD["CD_ZONE"] = D.GetString(dr["CD_ZONE"]);
                    drD["CLS_ITEM"] = D.GetString(dr["CLS_ITEM"]);
                    drD["NM_MAKER"] = D.GetString(dr["NM_MAKER"]);

                    if (bp입고창고.CodeName == "")
                    {
                        drD["FG_SLQC"] = D.GetString(dr["FG_SLQC"]);
                    }
                    else if (_biz.FG_SLQC("CD_SL", "", D.GetString(cb_cd_plant.SelectedValue), bp입고창고.CodeValue) == "Y")
                    {
                        drD["FG_SLQC"] = D.GetString(dr["FG_SLQC"]);
                    }
                    else
                    {
                        drD["FG_SLQC"] = "N";
                    }

                    if (dr.Table.Columns.Contains("NO_WO"))
                    {
                         drD["NO_PSO_MGMT"] = dr["NO_WO"];
                         drD["NO_PSOLINE_MGMT"] = dr["NO_WOLINE"];
                    }

                    //drD["NO_GIREQ"] = tb_NO_GIREQ.Text;
                    //drD["NO_LINE"] = 최대차수 + 1;
                    //drD["QT_GIREQ"] = dr["QT_ITEM_NET"]; //실소요량 을 넣어줌, 김헌섭대리

//                     drD["CD_EXCH"] = "0000"; //환율 환종을 원화와 1로 지정해줌 //김헌섭대리 요청
//                     drD["RT_EXCH"] = 1;
// 
//                     /* 생산품목 */
//                     drD["CD_WORKITEM"] = dr["CD_ITEM"];
//                     drD["NM_WORKITEM"] = dr["NM_ITEM"];
//                     drD["STND_ITEM_WORKITEM"] = dr["STND_ITEM"];
//                     drD["UNIT_IM_WORKITEM"] = dr["UNIT_IM"];
//                     drD["QT_WORK"] = dr["QT_WORK"];  //생산품목수량

                    //object[] obj = { Global.MainFrame.LoginInfo.CompanyCode, //품목의 발주단위를 구해오기위해서
                    //                     _header.CurrentRow["CD_PLANT"].ToString(),
                    //                      dr[gubun].ToString()};                // BOM과 같이 쓰기위햐여~ 컬럼이름을 받아왔다.
                    //DataTable dt_PITEM = ComFunc.GetTableSearch("MA_PITEM", obj);
                    //if (dt_PITEM != null && dt_PITEM.Rows.Count != 0) drD["UNIT_PO"] = dt_PITEM.Rows[0]["UNIT_PO"].ToString();

//                     if (_header.CurrentRow["FG_IO"].ToString() == "011")
//                     {
//                         drD["CD_WC"] = _header.CurrentRow["CD_WC"];
//                         drD["CD_WCOP"] = _header.CurrentRow["CD_WCOP"];
//                         drD["NM_WC"] = _header.CurrentRow["NM_WC"];
//                         drD["NM_OP"] = _header.CurrentRow["NM_OP"];
// 
//                     }

                    //구매환산단위추가 20091228
//                     drD["UNIT_PO_FACT"] = dr["UNIT_PO_FACT"];
//                     if (_flex.CDecimal(dr["UNIT_PO_FACT"]) == 0)
//                         drD["UNIT_PO_FACT"] = 1;

                    //재고단위수량으로 발주단위를 계산
//                     if (_flex.CDecimal(dr["QT_ITEM_NET"]) != 0) drD["QT_PO_MM"] = _flex.CDecimal(dr["QT_ITEM_NET"]) / _flex.CDecimal(drD["UNIT_PO_FACT"]);//실소요량 을 넣어줌, 김헌섭대리
//                     else drD["QT_PO_MM"] = _flex.CDecimal(dr["QT_ITEM_NET"]);

                   

                    dt.Rows.Add(drD);

                    if (bp_CD_SL.CodeValue != "")
                    {
                        _flex[_flex.Rows.Count - 1, "CD_SL"] = bp_CD_SL.CodeValue;
                        _flex[_flex.Rows.Count - 1, "NM_SL"] = bp_CD_SL.CodeName;
                    }

                    //decimal qt_pinvn = BASICPU.Item_PINVN(D.GetString(_header.CurrentRow["CD_PLANT"]), D.GetString(_flex[_flex.Rows.Count - 1,"CD_SL"]), D.GetString(_flex[_flex.Rows.Count - 1,"CD_ITEM"]));
                    //_flex[_flex.Rows.Count - 1, "QT_GOODS"] = D.GetDecimal(qt_pinvn);

                    decimal qt_pinvn = 0;
                    if (Config.MA_ENV.PJT형여부 == "N")
                    {
                        qt_pinvn = BASICPU.Item_PINVN(D.GetString(_header.CurrentRow["CD_PLANT"]), D.GetString(_flex[_flex.Rows.Count - 1, "CD_SL"]), D.GetString(_flex[_flex.Rows.Count - 1, "CD_ITEM"]));
                    }
                    else
                    {
                        qt_pinvn = BASICPU.Item_PINVN_PJT(D.GetString(_header.CurrentRow["CD_PLANT"]), D.GetString(_flex[_flex.Rows.Count - 1, "CD_SL"]), D.GetString(_flex[_flex.Rows.Count - 1, "CD_ITEM"]), D.GetString(_flex[_flex.Rows.Count - 1, "CD_PROJECT"]), ((Config.MA_ENV.YN_UNIT == "Y") ? D.GetDecimal(_flex[_flex.Rows.Count - 1, "SEQ_PROJECT"]) : 0));
                    }
                    _flex[_flex.Rows.Count - 1, "QT_GOODS"] = D.GetDecimal(qt_pinvn);


//                     if (_header.CurrentRow["CD_GRSL"].ToString() != null || _header.CurrentRow["NM_GRSL"].ToString() != String.Empty)
//                     {
//                         _flex[_flex.Rows.Count - 1, "CD_GRSL"] = _header.CurrentRow["CD_GRSL"].ToString();
//                         _flex[_flex.Rows.Count - 1, "NM_GRSL"] = _header.CurrentRow["NM_GRSL"].ToString();
//                     }

                    MaxSeq++;
                    //첫줄여부 = false;
                }
                _flex.Select(_flex.Rows.Count - 1, _flex.Cols.Fixed);

                _flex.Redraw = true;


            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Grid_PRWO(DataTable dt_return)
        {
            try
            {
                DataTable dt = _flex.DataTable;
                DataRow drD;
                //bool 첫줄여부 = true;


                decimal MaxSeq = _flex.GetMaxValue("NO_ISURCVLINE");

                _flex.Redraw = false;

                CommonFunction objFunc = new CommonFunction();

                foreach (DataRow dr in dt_return.Rows)
                {
                    drD = dt.NewRow();

                    drD["CD_ITEM"] = dr["CD_MATL"];
                    drD["NM_ITEM"] = dr["NM_ITEM_MATL"];

                    drD["STND_ITEM"] = dr["STND_ITEM_MATL"];
                    drD["UNIT_IM"] = dr["UNIT_IM_MATL"];
                    drD["QT_GOOD_INV"] = dr["QT_ITEM_NET"];
               
                    drD["UNIT_PO"] = dr["UNIT_PO"];
                    drD["QT_UNIT_PO"] = calc이동수량("QT_GOOD_INV", D.GetDecimal(drD["QT_GOOD_INV"]), D.GetDecimal(dr["UNIT_PO_FACT"]));

                    drD["출고창고"] = bp_CD_SL.CodeName;
                    drD["입고창고"] = bp입고창고.CodeName;

                    drD["NO_LOT"] = (dr["FG_SERNO"].ToString() == "002") ? "YES" : "NO";
                    drD["NO_SERL"] = (dr["FG_SERNO"].ToString() == "003") ? "YES" : "NO";// SERL 여부 

                    drD["CD_PLANT"] = _header.CurrentRow["CD_PLANT"];
                    drD["CD_QTIOTP"] = tb_cd_qtio.CodeValue;
                    drD["NM_QTIOTP"] = tb_cd_qtio.CodeName;
                    drD["DT_IO"] = tb_DT_IO.Text;
                    drD["YN_RETURN"] = "N";
                    drD["YN_AM"] = _header.CurrentRow["YN_AM"];

                    drD["FG_SERNO"] = dr["FG_SN_LOT"].ToString();
                    drD["NM_ITEMGRP"] = dr["GRP_ITEMNM"].ToString();
                    drD["BARCODE"] = D.GetString(dr["BARCODE"]);
                    drD["CD_ZONE"] = D.GetString(dr["CD_ZONE"]);
                    drD["CLS_ITEM"] = D.GetString(dr["CLS_ITEM"]);
                    drD["NM_MAKER"] = D.GetString(dr["NM_MAKER"]);

                    if (bp입고창고.CodeName == "")
                    {
                        drD["FG_SLQC"] = D.GetString(dr["FG_SLQC"]);
                    }
                    else if (_biz.FG_SLQC("CD_SL", "", D.GetString(cb_cd_plant.SelectedValue), bp입고창고.CodeValue) == "Y")
                    {
                        drD["FG_SLQC"] = D.GetString(dr["FG_SLQC"]);
                    }
                    else
                    {
                        drD["FG_SLQC"] = "N";
                    }

                    if (dr.Table.Columns.Contains("NO_WO"))
                    {
                        drD["NO_PSO_MGMT"] = dr["NO_WO"];
                        drD["NO_PSOLINE_MGMT"] = dr["NO_WOLINE"];
                    }

                    dt.Rows.Add(drD);

                    if (bp_CD_SL.CodeValue != "")
                    {
                        _flex[_flex.Rows.Count - 1, "CD_SL"] = bp_CD_SL.CodeValue;
                        _flex[_flex.Rows.Count - 1, "NM_SL"] = bp_CD_SL.CodeName;
                    }

                    decimal qt_pinvn = 0;
                    if (Config.MA_ENV.PJT형여부 == "N")
                    {
                        qt_pinvn = BASICPU.Item_PINVN(D.GetString(_header.CurrentRow["CD_PLANT"]), D.GetString(_flex[_flex.Rows.Count - 1, "CD_SL"]), D.GetString(_flex[_flex.Rows.Count - 1, "CD_ITEM"]));
                    }
                    else
                    {
                        qt_pinvn = BASICPU.Item_PINVN_PJT(D.GetString(_header.CurrentRow["CD_PLANT"]), D.GetString(_flex[_flex.Rows.Count - 1, "CD_SL"]), D.GetString(_flex[_flex.Rows.Count - 1, "CD_ITEM"]), D.GetString(_flex[_flex.Rows.Count - 1, "CD_PROJECT"]), ((Config.MA_ENV.YN_UNIT == "Y") ? D.GetDecimal(_flex[_flex.Rows.Count - 1, "SEQ_PROJECT"]) : 0));
                    }
                    _flex[_flex.Rows.Count - 1, "QT_GOODS"] = D.GetDecimal(qt_pinvn);

                    MaxSeq++;
                 
                }
                _flex.Select(_flex.Rows.Count - 1, _flex.Cols.Fixed);

                _flex.Redraw = true;


            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Appstock_Click(object sender, EventArgs e)
        {

            if (!CheckFieldHead(sender))
                return;

            P_PU_GIREQ_SUB5 m_dlg = new P_PU_GIREQ_SUB5(cb_cd_plant.SelectedValue.ToString(), bp_CD_SL.CodeValue, bp_CD_SL.CodeName, tb_DT_IO.Text);

            if (m_dlg.ShowDialog(this) == DialogResult.OK)
            {
                Grid_StockAfter(m_dlg._dtL);

                if (_flex.DataTable != null && _flex.DataTable.Rows.Count > 0)
                {
                    btn_REQ.Enabled = false;
                    btn_BOM.Enabled = false;
                    btn_SGIREQ.Enabled = false;
                    btn_WGIREQ.Enabled = false;
                    btn_ExcelUpload.Enabled = false;
                    b_Append.Enabled = false;
                }
            }
        }

        private bool CheckFieldHead(object sender)
        {
            try
            {
                if (tb_cd_qtio.CodeValue.ToString() == "")
                {
                    tb_cd_qtio.Focus();
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, lb_cd_qtio.Text);
                    return false;
                }
                if (tb_no_emp.CodeValue.ToString() == "")
                {
                    tb_no_emp.Focus();
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, lb_no_emp.Text);
                    return false;
                }
                if (tb_DT_IO.Value.ToString() == "")
                {
                    tb_DT_IO.Focus();
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, lb_dt_trans.Text);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            return true;
        }

        private void Grid_StockAfter(DataTable _dtL)
        {
            try
            {
                DataTable dt = _flex.DataTable;
                DataRow drD;

                decimal MaxSeq = _flex.GetMaxValue("NO_ISURCVLINE");

                _flex.Redraw = false;

                CommonFunction objFunc = new CommonFunction();

                foreach (DataRow dr in _dtL.Rows)
                {
                    drD = dt.NewRow();

                    drD["CD_ITEM"] = dr["CD_ITEM"];
                    drD["NM_ITEM"] = dr["NM_ITEM"];

                    drD["STND_ITEM"] = dr["STND_ITEM"];
                    drD["UNIT_IM"] = dr["UNIT_IM"];
                    drD["UNIT_PO"] = dr["UNIT_PO"];
                    drD["NO_LOT"] = (dr["FG_SERNO"].ToString() == "002") ? "YES" : "NO";
                    drD["NO_SERL"] = (dr["FG_SERNO"].ToString() == "003") ? "YES" : "NO";// SERL 여부 
                    drD["QT_GOOD_INV"] = dr["QT_INV"];

                    drD["QT_UNIT_PO"] = calc이동수량("QT_GOOD_INV", D.GetDecimal(dr["QT_INV"]), D.GetDecimal(dr["UNIT_PO_FACT"]));

                    drD["QT_GOODS"] = dr["QT_INV"];

                    if (dr["CD_SL"] == null)
                        drD["CD_SL"] = bp_CD_SL.CodeValue;
                    else
                        drD["CD_SL"] = dr["CD_SL"];

                    if (dr["NM_SL"] == null)
                        drD["출고창고"] = bp_CD_SL.CodeName;
                    else
                        drD["출고창고"] = dr["NM_SL"];

                    if (D.GetString(bp입고창고.CodeValue) != string.Empty)
                    {
                        drD["CD_SL_REF"] = bp입고창고.CodeValue;
                        drD["입고창고"] = bp입고창고.CodeName;
                    }

                    drD["CD_PLANT"] = _header.CurrentRow["CD_PLANT"];

                    drD["DT_IO"] = tb_DT_IO.Text;
                    drD["CD_QTIOTP"] = tb_cd_qtio.CodeValue;
                    drD["NM_QTIOTP"] = tb_cd_qtio.CodeName;
                    drD["YN_RETURN"] = "N";
                    drD["YN_AM"] = _header.CurrentRow["YN_AM"];
                    drD["CD_PLANT"] = _header.CurrentRow["CD_PLANT"];
                    drD["FG_SLQC"] = D.GetString(dr["FG_SLQC"]);
                    drD["NM_MAKER"] = D.GetString(dr["NM_MAKER"]);

                    if (_dtL.Columns.Contains("CD_PJT"))
                    {
                        drD["CD_PROJECT"] = dr["CD_PJT"];
                        drD["NM_PROJECT"] = dr["NM_PJT"];

                        if (Config.MA_ENV.PJT형여부 == "Y")
                        {
                            drD["SEQ_PROJECT"] = dr["SEQ_PROJECT"];
                            drD["CD_PJT_ITEM"] = dr["CD_PJT_ITEM"];
                            drD["NM_PJT_ITEM"] = dr["NM_PJT_ITEM"];
                            drD["STND_UNIT"] = D.GetString(dr["PJT_STND_ITEM"]);
                        }
                    }


                    dt.Rows.Add(drD);

                    MaxSeq++;
                }
                _flex.Select(_flex.Rows.Count - 1, _flex.Cols.Fixed);
                _flex.AddFinished();
                _flex.Redraw = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Only_Pjt(DataRow dr)
        {
            _flex["CD_PROJECT"] = dr["CD_PJT"];
            _flex["NM_PROJECT"] = dr["NM_PJT"];

            if (PJT형여부)
            {
                _flex["SEQ_PROJECT"] = dr["SEQ_PROJECT"];
                _flex["CD_PJT_ITEM"] = dr["CD_PJT_ITEM"];
                _flex["NM_PJT_ITEM"] = dr["NM_PJT_ITEM"];
                _flex["NO_WBS"] = dr["NO_WBS"];
                _flex["NO_CBS"] = dr["NO_CBS"];
            }
        }

        private void btn입고적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckFieldHead(sender)) return;

                object[] obj = new object[] { D.GetString(cb_cd_plant.SelectedValue) };

                string str입고번호들 = "";
                string str입고항번들 = "";
                string STRdata = "SL간이동처리";


                P_PU_WGI_GR_SUB dlg = new P_PU_WGI_GR_SUB(MainFrameInterface, obj, str입고번호들, str입고항번들, STRdata);

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    if (dlg.dtL.Rows.Count > 0)
                    {

                        입고적용그리드라인추가(dlg.dtL);
                        b_Append.Enabled = false;
                        b_Delete.Enabled = true;
                        입고적용 = true;

                        tb_DT_IO.Enabled = false;
                        tb_cd_qtio.Enabled = false;
                        cb_cd_plant.Enabled = false;
                        tb_no_emp.Enabled = false;

                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void 입고적용그리드라인추가(DataTable pdt_Line)
        {
            try
            {
           

                _flex.Redraw = false;

                DataRow row;
                for (int i = 0; i < pdt_Line.Rows.Count; i++)
                {
                    row = pdt_Line.Rows[i];
        

                    _flex.Rows.Add();
                    _flex.Row = _flex.Rows.Count - 1;
                    
                          
                    _flex["S"] = "N";
                    _flex["CD_ITEM"] = row["CD_ITEM"].ToString();
                    _flex["NM_ITEM"] = row["NM_ITEM"].ToString();
                    _flex["STND_ITEM"] = row["STND_ITEM"].ToString();
                    _flex["UNIT_IM"] = row["UNIT_IM"].ToString();
                    _flex["UNIT_PO"] = row["UNIT_PO"].ToString();
                    _flex["CD_PLANT"] = _header.CurrentRow["CD_PLANT"];
                    _flex["NO_LOT"] = row["FG_LOT"].ToString();
                    _flex["NO_SERL"] = row["NO_SERL"].ToString();
                    _flex["QT_GOOD_INV"] = D.GetDecimal(row["QT_GOOD_INV"]);
                    _flex["QT_UNIT_PO"] = calc이동수량("QT_GOOD_INV", D.GetDecimal(row["QT_GOOD_INV"]), D.GetDecimal(row["UNIT_PO_FACT"]));

                    _flex["QT_REJECT_INV"] = 0;
                    _flex["DT_IO"] =   tb_DT_IO.Text;
                    _flex["요청자"] = row["NM_KOR"];
                    _flex["NO_EMP"] = row["NO_EMP"];
                    _flex["요청부서"] = row["NM_DEPT"];
                    //_flex["입고창고"] = row["GR_NM_SL"];
                    //_flex["CD_SL_REF"] = row["GR_CD_SL"];
                    _flex["출고창고"] = row["GR_NM_SL"];
                    _flex["CD_SL"] = row["GR_CD_SL"]; 
                    _flex["NM_QTIOTP"] = tb_cd_qtio.CodeName;
                    _flex["CD_QTIOTP"] = _header.CurrentRow["CD_QTIOTP"];
                    _flex["NO_PSO_MGMT"] = row["NO_WO"];
                    _flex["NO_PSOLINE_MGMT"] = 0;
                    _flex["YN_RETURN"] = "N";            
                    _flex["CD_WCOP"] = row["CD_WCOP"].ToString();
                    _flex["NM_WC"] = row["NM_WC"].ToString();
                    _flex["NM_OP"] = row["NM_OP"].ToString();
                    _flex["NM_WORKITEM"] = row["NM_WORKITEM"].ToString();
                    _flex["CD_WORKITEM"] = row["CD_WORKITEM"].ToString();
                    _flex["NO_IO_MGMT"] = row["NO_IO"].ToString();
                    _flex["NO_IOLINE"] = D.GetDecimal(row["NO_IOLINE"]);
                    _flex["NO_IOLINE_MGMT"] = D.GetDecimal(row["NO_IOLINE"]);
                    _flex["QT_INV"] = D.GetDecimal(row["QT_INV"]);
                    _flex["CD_PROJECT"] = row["CD_PJT"];
                    _flex["NM_PROJECT"] = row["NM_PJT"];
                    _flex["SEQ_PROJECT"] = row["SEQ_PROJECT"];
                    _flex["CD_PJT_ITEM"] = row["CD_PJT_ITEM"];
                    _flex["NM_PJT_ITEM"] = row["PJT_NM_ITEM"];
                    _flex["PJT_STND_ITEM"] = row["PJT_STND_ITEM"];
                    _flex["NO_WBS"] = row["NO_WBS"];
                    _flex["NO_CBS"] = row["NO_CBS"];
                    _flex["YN_AM"] = _header.CurrentRow["YN_AM"];

                    //decimal qt_pinvn = BASICPU.Item_PINVN(D.GetString(_header.CurrentRow["CD_PLANT"]), D.GetString(row["GR_CD_SL"]), D.GetString(row["CD_ITEM"].ToString()));

                    //_flex["QT_GOODS"] = D.GetDecimal(qt_pinvn);

                    decimal qt_pinvn = 0;
                    if (Config.MA_ENV.PJT형여부 == "N")
                    {
                        qt_pinvn = BASICPU.Item_PINVN(D.GetString(_header.CurrentRow["CD_PLANT"]), D.GetString(_flex[ "CD_SL"]), D.GetString(_flex["CD_ITEM"]));
                    }
                    else
                    {
                        qt_pinvn = BASICPU.Item_PINVN_PJT(D.GetString(_header.CurrentRow["CD_PLANT"]), D.GetString(_flex["CD_SL"]), D.GetString(_flex["CD_ITEM"]), D.GetString(_flex[ "CD_PROJECT"]), ((Config.MA_ENV.YN_UNIT == "Y") ? D.GetDecimal(_flex["SEQ_PROJECT"]) : 0));
                    }
                    _flex["QT_GOODS"] = D.GetDecimal(qt_pinvn);


                    _flex["BARCODE"] = D.GetString(row["BARCODE"]);
                    _flex["CD_ZONE"] = D.GetString(row["CD_ZONE"]);
                    _flex["FG_SLQC"] = D.GetString(row["FG_SLQC"]);
                    _flex["CLS_ITEM"] = D.GetString(row["CLS_ITEM"]);
                    _flex["NM_MAKER"] = D.GetString(row["NM_MAKER"]);
                    _flex.AddFinished();
                    _flex.Col = _flex.Cols.Fixed;
                }
                _flex.Focus();
            }
            catch (Exception ex)
            {
                _flex.Redraw = true;
                MsgEnd(ex);
            }
            finally
            {
                _flex.SumRefresh();
                _flex.Redraw = true;
            }
        }

        private decimal calc이동수량(string col, decimal val, decimal val2)
        {
            decimal ret = 0;

            if (val2 == 0)
                val2 = 1;

            if (col == "QT_GOOD_INV")
            {
                ret = val / val2;
            }
            else if (col == "QT_UNIT_PO")
            {
                ret = val * val2;
            }

            return ret;

        }

        #region -> tb_DT_IO_DateChanged

        private void tb_DT_IO_DateChanged(object sender, EventArgs e)
        {
            try
            {
                //if (!((DatePicker)sender).Modified)
                //    return;

                //if (((DatePicker)sender).Text == string.Empty)
                //    return;

                //// 유효성 검사
                //if (!((DatePicker)sender).IsValidated)
                //{
                //    this.ShowMessage(공통메세지.입력형식이올바르지않습니다);
                //    ((DatePicker)sender).Text = string.Empty;
                //    ((DatePicker)sender).Focus();
                //    return;
                //}

                _flex.Redraw = false;
                if (_flex.HasNormalRow)
                {
                    foreach (DataRow dr in _flex.DataTable.Select())
                    {
                        dr["DT_IO"] = tb_DT_IO.Text;
                    }
                }
                _flex.Redraw = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
                _flex.Redraw = true;
            }


        }
        #endregion

        #region ♣의뢰번호 visible설정 NO_GIREQ, 등등

        private void  의뢰번호Visible(string fg_sub)
        {
            if (fg_sub == "생산") 
                _flex.Cols["NO_ISURCV"].Visible = _flex.Cols["NO_PSO_MGMT"].Visible = true;
            else
                _flex.Cols["NO_ISURCV"].Visible = _flex.Cols["NO_PSO_MGMT"].Visible = false; 
             
        }
        #endregion

        #region -> 수불형태Default셋팅
        void 수불형태Default셋팅()
        {
            if (Settings1.Default.CD_QTIOTP.ToString() == string.Empty) return;

            tb_cd_qtio.CodeValue = Settings1.Default.CD_QTIOTP;
            tb_cd_qtio.CodeName = Settings1.Default.NM_QTIOTP;
            _header.CurrentRow["CD_QTIOTP"] = tb_cd_qtio.CodeValue;
            _header.CurrentRow["NM_QTIOTP"] = tb_cd_qtio.CodeName;

            DataRow row = Duzon.ERPU.MF.Common.CodeSearch.GetCodeInfo(MasterSearch.MM_EJTP, new string[] { MA.Login.회사코드, Settings1.Default.CD_QTIOTP });
            _header.CurrentRow["YN_AM"] = D.GetString(row["YN_AM"]);
        } 
        #endregion

    }
}

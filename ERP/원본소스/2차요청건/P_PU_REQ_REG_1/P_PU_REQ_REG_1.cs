using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.MF.EMail;
using Duzon.Windows.Print;
using DzHelpFormLib;
using master;


namespace pur
{
    // **************************************
    // 작   성   자 : 유지영
    // 재 작  성 일 : 2006-11-09
    // 모   듈   명 : 구매/자재
    // 시 스  템 명 : 구매자재관리
    // 서브시스템명 : 구매입고관리
    // 페 이 지  명 : 입고의뢰등록
    // 프로젝트  명 : P_PU_REQ_REG_1
    // 변 경  내 역 : 
    // 2010.01.06 적용버튼로직에 공장 적용추가
    // 2010.02.11 - 안종호- 총급액(공급가액+ 부가세) 컬럼추가(김헌섭대리,크라제)
    // 2010.02.16 - 안종호 - 창고도움창 적용버튼 추가
    // 2010.02.23 - 신미란 - 원화, 부가세 Edit 가능하게 (김광석, 어보브반도체)
    // 2010.03.24 - 심재희 - SERIAL 추가
    // 2010.04.12 - 신미란 - 발주적요 적용, 입고의뢰, 입고까지 모두 적용
    // 2010.04.22 - 신미란 - 수입검사적용 추가
    // 2010.06.10 - 안종호 - 헤더 라인 초기화 데이터테이블 생성 로직 추가
    // 2010.07.09 - 신미란 - 가입고적용추가 (검사구분이=N인것들 처리하기 위한)
    // 2010.07.22 - 신미란 - 단가,금액 권한통제 기능 추가 (김헌섭/한경희)
    // 2010.08.17 신미란 - 수입검사 여부에 따른 수입검사적용버튼 처리
    // 2010.09.01 신미란 - 통관적용시 환율은 BL 환율 (원화금액을 BL금액으로 하기때문....)(조선형,김형석검증)
    // 2010.09.28 신미란 - 가입고,수입검사 무역연동
    // 2013.07.26 윤성우 - ONE GRID 수정(입력 전용)
    // 2013.07.30 윤나라 - 발주라인의 DT_PLAN 컬럼 추가
    // **************************************
    public partial class P_PU_REQ_REG_1 : Duzon.Common.Forms.PageBase
    {
        private P_PU_REQ_REG_1_BIZ _biz = new P_PU_REQ_REG_1_BIZ();
        private FreeBinding _header = new FreeBinding();
        bool _isChagePossible;
        public string MNG_LOT = string.Empty; //시스템통제등록 LOT사용여부 
        public string MNG_SERIAL = string.Empty; //시스템통제등록 SERIAL사용여부 
        private string m_sEnv = "N";
        private string m_sEnv_IQC = "N";
        private string m_sPJT재고사용 = "000";
        private bool m_bPJT사용 = false; // 환경설정
        private string m_YN_special = "N";
        private string strNO_RCV;
        private decimal NO_RCV_LINE;
        private string strSOURCE;
        private string m_Elec_app = "000";
        private string m_Mail = "N";
        private string m_YN_SU = "000";
        private string FG_IO_SU;
        private string CD_QTIOTP_SU;
        bool b단가권한 = true;
        bool b금액권한 = true;
        string s_vat_fictitious = BASIC.GetMAEXC("발주등록(공장)-의제부가세적용");

        bool bStandard = false; //규격형


        #region ♣ 초기화

        #region -> 생성자
        public P_PU_REQ_REG_1() : this("", 0m, "") { }
        public P_PU_REQ_REG_1(string strNO_RCV, decimal NO_RCV_LINE, string strSOURCE)
        {
            try
            {
                InitializeComponent();

                this.strNO_RCV = strNO_RCV;
                this.NO_RCV_LINE = NO_RCV_LINE;
                this.strSOURCE = strSOURCE;   //구매입고현황 , 구매입고리스트 -> "화면이동"로 들어오도록 했습

                this.MainGrids = new FlexGrid[] { _flex };
                this.DataChanged += new EventHandler(Page_DataChanged);
                _header.ControlValueChanged += new FreeBindingEventHandler(_header_ControlValueChanged);
                _header.JobModeChanged += new FreeBindingEventHandler(_header_JobModeChanged);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        void ControlEnabledDisable(bool enable)
        {
            tb_DT_RCV.Enabled = enable;   //전체 panel부분 활성화
            cbo공장.Enabled = enable;
            cbo_TRANS.Enabled = enable;
            tb_NM_PARTNER.Enabled = enable;
            tb_NO_EMP.Enabled = enable;
            cb_Yn_AutoRcv.Enabled = enable;
            cbo_PROCESS.Enabled = false;
            cbo_TRANS.Enabled = enable;
        }

        #endregion

        #region -> InitLoad
        protected override void InitLoad()
        {
            base.InitLoad();

            DataTable dt = Duzon.ERPU.MF.Common.BASIC.MFG_AUTH("P_PU_REQ_REG_1");
            if (dt.Rows.Count > 0)
            {
                b단가권한 = (dt.Rows[0]["YN_UM"].ToString() == "Y") ? false : true;
                b금액권한 = (dt.Rows[0]["YN_AM"].ToString() == "Y") ? false : true;
            }
            

            MA_EXC_SETTING();//사용자통제설정
           // BTN_LOCATION_SETTING();

            InitGrid();
            InitEvent();
        }


        #endregion

        #region -> InitPaint
        protected override void InitPaint()
        {
            base.InitPaint();

     
            m_sEnv_IQC = Duzon.ERPU.MF.ComFunc.전용코드("수입검사-사용구분");
            if (m_sEnv_IQC == "Y") b_IQC_Apply.Visible = true;

            m_sPJT재고사용 = Duzon.ERPU.MF.ComFunc.전용코드("프로젝트재고사용");
            m_bPJT사용 = App.SystemEnv.PROJECT사용;

            m_YN_special = Duzon.ERPU.MF.ComFunc.전용코드("특채수량 사용여부");

            #region -> 헤더,그리드 초기화
            //DataSet dsTemp = _biz.Search("@#$G", "@%#%");               // 데이타테이블의 컬럼정보만 얻어오기 위해서

            //_header.SetBinding(dsTemp.Tables[0], panel_Head);
            //_header.ClearAndNewRow();                                   // 처음에 행을 하나 만든다. 초기에 추가모드로 하기 위해
            //_flex.Binding = dsTemp.Tables[1];
            #endregion

            Initial_Binding();// 헤더,그리드 초기화

            InitControl();

            DataTable dt = _biz.Search_LOT();
            MNG_LOT = dt.Rows[0]["MNG_LOT"].ToString();

            DataTable dt2 = _biz.Search_SERIAL();
            MNG_SERIAL = dt2.Rows[0]["YN_SERIAL"].ToString();


            m_btnLC.Enabled = false;
            m_btnTR_TO.Enabled = false;
            _isChagePossible = true;

            tb_NO_EMP.CodeValue = Global.MainFrame.LoginInfo.EmployeeNo;
            tb_NO_EMP.CodeName = Global.MainFrame.LoginInfo.EmployeeName;

            tb_DT_RCV.Text = Global.MainFrame.GetStringToday;

            if (Global.MainFrame.ServerKeyCommon == "CHOSUNHOTELBA" || MainFrameInterface.ServerKey == "DZSQL")
                m_btn_IqcStore.Visible = true;


            if (m_YN_SU == "100")
            {
                DataTable dtB =  _biz.Search_MATL("ASDFASDF");
                _flexD.Binding = dtB;
            }

            

            if (D.GetString(strNO_RCV) != string.Empty)
            {
                   DataSet ds = null;

                   if ( strSOURCE == "화면이동" )   //구매입고현황 , 구매입고리스트에서 화면이동된 경우
                      ds =_biz.Search(strNO_RCV, "N"); 
                   else
                      ds = _biz.Search(strNO_RCV, "Y");   //자동입고일 경우에만 조회해 보이기 위해 'Y' 표시


                    _header.SetDataTable(ds.Tables[0]);
                    if (strSOURCE != "화면이동")  
                         txt_NOIO.Text = string.Empty;

                    if (ds != null && ds.Tables.Count > 1)
                    {
                        DataTable ldt_head = ds.Tables[0];
                        DataTable ldt_line = ds.Tables[1];

                        Button_Enabled(ldt_head, ldt_line);
                        _flex.Binding = ds.Tables[1];

                        if (!_flex.HasNormalRow)
                            if (!_header.CurrentRow.IsNull(0))
                                this.ShowMessage(PageResultMode.SearchNoData);

                        _header.AcceptChanges();
                        _flex.AcceptChanges();
                        ControlEnabledDisable(false);
                    }
            }

            원그리드적용하기();

        }

        #endregion

        #region -> MA_EXC_SETTING 사용자통제설정 
        private void MA_EXC_SETTING()
        {
            try
            {
                m_Elec_app = BASIC.GetMAEXC("구매입고처리-업체별프로세스선택");

                if (m_Elec_app == "100")  //YTN전용
                {
                    //lbl관리번호.Visible = true;
                    //txt관리번호.Visible = true;
                    //btn적용_관리번호.Visible = true;
                    //pnl입고번호.Visible = true;
                    bpPanelControl6.Visible = true;

                    //lbl입고번호.Visible = false;
                    //txt_NOIO.Visible = false;
                    bpPanelControl13.Visible = false;

                }
                else
                {
                    bpPanelControl6.Visible = false;
                    bpPanelControl13.Visible = true;

                }

                m_Mail = BASIC.GetMAEXC("구매입고처리-메일전송설정");

                if (m_Mail == "Y")
                    btn메일전송.Visible = true;

                m_sEnv = _biz.EnvSearch();   // 재고단위Edit여부

                m_YN_SU = BASIC.GetMAEXC("구매발주-외주유무");

                if (m_YN_SU == "100")
                {
                    InitGridD();
                }
                else
                {
                    tab.TabPages.RemoveAt(1);
                }

                //규격형 사용 유무
                if (BASIC.GetMAEXC("공장품목등록-규격형") == "100")
                {
                    bStandard = true;

                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            
        }
        #endregion

        #region -> InitControl
        private void InitControl() 
        {
            DataSet ds = this.GetComboData("N;TR_IM00006", "NC;MA_PLANT", "N;PU_C000025", "N;PU_C000016", "N;PU_C000005","N;MA_B000010");

            // L/C 선택
            this.cbo_PROCESS.DataSource = ds.Tables[0].DefaultView;
            this.cbo_PROCESS.DisplayMember = "NAME";
            this.cbo_PROCESS.ValueMember = "CODE";

            // 공장 콤보
            cbo공장.DataSource = ds.Tables[1].DefaultView;
            cbo공장.DisplayMember = "NAME";
            cbo공장.ValueMember = "CODE";

            //if (Global.MainFrame.LoginInfo.CdPlant != null)
            //{
            //    cbo_CD_PLANT.SelectedValue = Global.MainFrame.LoginInfo.CdPlant;
            //}

            if (cbo공장.Items.Count > 0)
            {
                if (LoginInfo.CdPlant != "")
                    cbo공장.SelectedValue = LoginInfo.CdPlant;

                cbo공장.SelectedIndex = 0;

               _header.CurrentRow["CD_PLANT"] = D.GetString(cbo공장.SelectedValue);

            }

            // 거래구분
            this.cbo_TRANS.DataSource = ds.Tables[3].DefaultView;
            this.cbo_TRANS.DisplayMember = "NAME";
            this.cbo_TRANS.ValueMember = "CODE";

            // 자동입고여부
            this.cb_Yn_AutoRcv.DataSource = ds.Tables[2].DefaultView;
            this.cb_Yn_AutoRcv.DisplayMember = "NAME";
            this.cb_Yn_AutoRcv.ValueMember = "CODE";
           
            
            // 부가세여부
            _flex.SetDataMap("TP_UM_TAX", ds.Tables[4], "CODE", "NAME"); 

            //품목계정
            _flex.SetDataMap("CLS_ITEM", ds.Tables[5], "CODE", "NAME"); 

            cbo_PROCESS.Enabled = false;
            txt_NOIO.Text = string.Empty;

            if (Global.MainFrame.ServerKeyCommon == "CARGOTEC" || Global.MainFrame.ServerKeyCommon == "ANJUN" || Global.MainFrame.ServerKeyCommon == "TELCON") //카고텍,안전공업만 바코드기능사용
            {
                chk_barcode_use.Visible = true;
                if (Settings1.Default.chk_barcode_use == "Y")
                    chk_barcode_use.Checked = true;
                else
                    chk_barcode_use.Checked = false;

                //panelExt2.Visible = true;
                //txt바코드.Visible = true;
                bpPanelControl10.Visible = true;
                bpPanelControl15.Visible = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(Settings1.Default.cd_sl_apply))
                {
                    bp_CD_SL.CodeValue = Settings1.Default.cd_sl_apply;
                    bp_CD_SL.CodeName = Settings1.Default.nm_sl_apply;
                }

                chk_barcode_use.Visible = false;
                chk_barcode_use.Checked = false;
                //panelExt2.Visible = false;
                //txt바코드.Visible = false;
                bpPanelControl10.Visible = false;
                bpPanelControl15.Visible = false;
                txt바코드.Focus();
            }

        }
        #endregion

        #region -> InitGrid

        private void InitGrid()
        {
            _flex.BeginSetting(1, 1, false);

            _flex.SetDummyColumn("S");
            _flex.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            _flex.SetCol("CD_ITEM", "품목코드", 110, false);
            _flex.SetCol("NM_ITEM", "품목명", 130, false);
            _flex.SetCol("STND_ITEM", "규격", 70, false);
            _flex.SetCol("CD_UNIT_MM", "발주단위", 70, false);
            _flex.SetCol("CD_ZONE", "저장위치", 100, false);
            _flex.SetCol("QT_REQ_MM", "입고량", 80, true, typeof(decimal), FormatTpType.QUANTITY);

            if (bStandard)
            {
                _flex.SetCol("UM_WEIGHT", "중량단가", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                _flex.SetCol("TOT_WEIGHT", "총중량", 100, true, typeof(decimal));

                _flex.Cols["TOT_WEIGHT"].Format = "###,###,###.#";
            }

            if (b단가권한)
                _flex.SetCol("UM_EX_PO", "단가", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            if (b금액권한)
            {
                _flex.SetCol("AM_EXREQ", "금액", 100, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                _flex.SetCol("AM_REQ", "원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
                _flex.SetCol("VAT", "부가세", 90, false, typeof(decimal), FormatTpType.MONEY);
                _flex.SetCol("AM_TOTAL", "총금액", 80, true, typeof(decimal), FormatTpType.MONEY);
            }

            _flex.SetCol("PI_PARTNER", "주거래처", false);
            _flex.SetCol("PI_LN_PARTNER", "주거래처명", false); 
            _flex.SetCol("TP_UM_TAX", "부가세여부", 70, false); 
            _flex.SetCol("DT_LIMIT", "납기일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex.SetCol("NO_LOT", "LOT여부", 60);
            _flex.SetCol("YN_INSP", "검사", 50, true, CheckTypeEnum.Y_N);
            _flex.SetCol("CD_SL", "창고코드", 60, true);
            _flex.SetCol("NM_SL", "창고명", 120, true);
            _flex.SetCol("NO_PO", "발주번호", 100, false);
            _flex.SetCol("NM_KOR", "담당자", 80, false);
            _flex.SetCol("NM_FG_RCV", "입고형태", 100, false);
            _flex.SetCol("NM_FG_POST", "발주상태", 100, false);

            _flex.SetCol("CD_PJT", "PROJECT코드", 100, false);
            _flex.SetCol("NM_PROJECT", "PROJECT", 100, false);
            _flex.SetCol("NM_PURGRP", "구매그룹", 100, false);
            _flex.SetCol("UNIT_IM", "관리단위", 90, false);
            _flex.SetCol("NO_BL", "BL번호", 120, false);
            _flex.SetCol("QT_REQ", "관리수량", 120, (m_sEnv == "Y") ? true : false, typeof(decimal), FormatTpType.QUANTITY);
            _flex.SetCol("RT_EXCH", "환율", 120, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            _flex.SetCol("DC_RMK", "의뢰라인비고1", 150, 200, true);
            _flex.SetCol("DC_RMK2", "의뢰라인비고2", 150, 40, true);
            _flex.SetCol("DT_PLAN", "납기예정일", 80, false,typeof(string),FormatTpType.YEAR_MONTH_DAY);
            
            if (m_Elec_app == "100") //YTN전용
            {
                _flex.Rows[0]["DC_RMK"] = "문서번호";
                _flex.Rows[0]["DC_RMK2"] = "입고번호";
            }

            if (Config.MA_ENV.프로젝트사용)
            {
                _flex.SetCol("SEQ_PROJECT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 항번" : "프로젝트항번", 120, false, typeof(decimal));
                _flex.SetCol("CD_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 코드" : "프로젝트 품목코드", 140, false, typeof(string));
                _flex.SetCol("NM_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 명" : "프로젝트 품목명", 140, false, typeof(string));
                _flex.SetCol("PJT_ITEM_STND", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 규격" : "프로젝트 품목규격", 140, false, typeof(string));

            }

            if (Config.MA_ENV.PJT형여부 == "Y")
            {
                _flex.SetCol("NO_WBS", "WBS번호", 140, false, typeof(string));
                _flex.SetCol("NO_CBS", "CBS번호", 140, false, typeof(string));
            }
            // 단지 DISPLAY용
            _flex.SetCol("REV_QT_PASS", "검수수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex.SetCol("REV_QT_REV_MM", "납품계획수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);

            // 이것도 단지 DISPLAY 아카데미과학에서 요청 D20120229048
            _flex.SetCol("CD_ITEM_ORIGIN", "관련품목", 140, false, typeof(string));
            _flex.SetCol("NM_ITEM_ORIGIN", "관련품목명", 140, false, typeof(string));
            _flex.SetCol("STND_ITEM_ORIGIN", "관련품목규격", 140, false, typeof(string));

            _flex.SetCol("GI_PARTNER", "납품처코드", 140, false, typeof(string));
            _flex.SetCol("NM_GI_PARTER", "납품처명", 140, false, typeof(string));

            if (MainFrameInterface.ServerKeyCommon == "WOORIERP")
            {
                DataTable dtTEXT = MA.GetCode("PU_Z000007", false);
                if (dtTEXT != null && dtTEXT.Rows.Count != 0)
                {
                    foreach (DataRow row in dtTEXT.Rows)
                    {
                        string ColName = D.GetString(row["CD_FLAG1"]) == "" ? D.GetString(row["NAME"]) : D.GetString(row["CD_FLAG1"]);
                        _flex.SetCol(D.GetString(row["NAME"]), ColName, 100, false, typeof(string));
                    }
                }
            }

            if (MainFrameInterface.ServerKeyCommon == "ICDERPU" || MainFrameInterface.ServerKeyCommon == "DZSQL" || MainFrameInterface.ServerKeyCommon == "SQL_")
            {
                _flex.SetCol("DATE_USERDEF1", "입고예정일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                _flex.Cols["DC_RMK2"].Caption = "창고(적재장소)";
                //_flex.SetCol("CDSL_USERDEF1", "창고(적재장소)", 80, true);
                //_flex.SetCol("NMSL_USERDEF1", "창고명(적재장소)", 100, false);
                //_flex.SetCodeHelpCol("CDSL_USERDEF1", HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CDSL_USERDEF1", "NMSL_USERDEF1" }, new string[] { "CD_SL", "NM_SL" });
            }
            if (MainFrameInterface.ServerKeyCommon == "WINPLUS" || MainFrameInterface.ServerKeyCommon == "DZSQL" || MainFrameInterface.ServerKeyCommon == "SQL_")
            {
                _flex.SetCol("NUM_USERDEF1", "가로", 80, false, typeof(decimal),FormatTpType.QUANTITY);
                _flex.SetCol("NUM_USERDEF2", "세로", 80, false, typeof(decimal),FormatTpType.QUANTITY);
            }

            if (MainFrameInterface.ServerKeyCommon == "UNIPOINT")
            {
                _flex.SetCol("CD_PARTNER_PJT", "프로젝트 거래처코드", 100, false, typeof(string));
                _flex.SetCol("LN_PARTNER_PJT", "프로젝트 거래처", 100, false, typeof(string));
                _flex.SetCol("NO_EMP_PJT", "프로젝트 담당자코드", 100, false, typeof(string));
                _flex.SetCol("NM_KOR_PJT", "프로젝트 담당자", 100, false, typeof(string));
                _flex.SetCol("END_USER", "프로젝트 END USER", 100, false, typeof(string));
            } 

            _flex.SetCol("CLS_ITEM", "품목계정", 140, false, typeof(string));
            _flex.SetCol("MAT_ITEM", "재질", 140, false, typeof(string));
            
            _flex.SettingVersion = "1.0.0.4";
            _flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            if (Config.MA_ENV.YN_UNIT == "Y")
                _flex.SetExceptSumCol("UM_EX_PO", "SEQ_PROJECT");
            else
            {
                if (bStandard)
                    _flex.SetExceptSumCol("UM_EX_PO", "UM_WEIGHT");
                else
                    _flex.SetExceptSumCol("UM_EX_PO");
            }

            

            _flex.VerifyCompare(_flex.Cols["QT_REQ_MM"], 0, OperatorEnum.Greater);
            //_flex.VerifyCompare(_flex.Cols["QT_REQ_MM"], _flex.Cols["QT_REQ"], OperatorEnum.Equal); // 무조건 관리수량과 의뢰량이 같아야 한다는 로직....
            //_flex.SettingVersion = "1.0.0.2";

            if (App.SystemEnv.PROJECT사용 == true)
            {
                if (Config.MA_ENV.YN_UNIT == "Y")
                    _flex.VerifyNotNull = new string[] { "CD_ITEM", "DT_LIMIT", "NM_SL", "NM_PROJECT","SEQ_PROJECT" };
                else
                    _flex.VerifyNotNull = new string[] { "CD_ITEM", "DT_LIMIT", "NM_SL", "NM_PROJECT" };
            }
            else
                _flex.VerifyNotNull = new string[] { "CD_ITEM", "DT_LIMIT", "NM_SL" };

            _flex.SetExceptEditCol("CD_ITEM", "NM_ITEM", "STND_ITEM", "CD_UNIT_MM", "UNIT", "NM_SL", "NO_PO", "NM_KOR", "NM_FG_RCV", "NM_FG_POST", "NM_PROJECT", "UNIT_IM"/*, "QT_REQ"*/);
            _flex.SetCodeHelpCol("CD_SL", HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CD_SL", "NM_SL" }, new string[] { "CD_SL", "NM_SL" }, new string[] { "CD_SL", "NM_SL" });
            //달력보류
            //_flex.SetCodeHelpCol("NM_SL", Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CD_SL", "NM_SL" }, new string[] { "CD_SL", "NM_SL" });
            _flex.BeforeCodeHelp += new BeforeCodeHelpEventHandler(_Grid_BeforeCodeHelp);
            _flex.ValidateEdit += new ValidateEditEventHandler(_Grid_ValidateEdit);
            _flex.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);
            _flex.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);

         
        }

        #endregion

        #region -> InitGridD

        private void InitGridD()
        {
            _flexD.BeginSetting(1, 1, false);

            _flexD.SetCol("CD_ITEM", "의뢰품목", 100, 20, false);
            _flexD.SetCol("NM_ITEM_ITEM", "품목명", 140, 50, false);
            _flexD.SetCol("STND_ITEM_ITEM", "규격", 120, 50, false);
            _flexD.SetCol("UNIT_IM_ITEM", "단위", 40, 3, false);
            _flexD.SetCol("CD_MATL", "자재코드", 100, 20, false);
            _flexD.SetCol("NM_ITEM", "자재명", 140, 50, false);
            _flexD.SetCol("STND_ITEM", "규격", 120, 50, false);
            _flexD.SetCol("UNIT_IM", "단위", 40, 3, false);
            _flexD.SetCol("QT_NEED", "출고의뢰수량", 90, 17, true, typeof(decimal), FormatTpType.QUANTITY);
            _flexD.SetCol("NO_PO", "발주번호", 100, 20, false);
            _flexD.SetCol("NO_POLINE", "발주항번", 60, 5, false);
            _flexD.SetCol("NO_PO_MAL_LINE", "사급자재항번", 80, 5, false);
            _flexD.SetCol("CD_SL", "창고코드", 80, 7, true, typeof(string));
            _flexD.SetCol("NM_SL", "창고명", 120, false, typeof(string));

            if (Config.MA_ENV.프로젝트사용)
            {
                _flexD.SetCol("CD_PJT", "프로젝트", 140, 100, true, typeof(string));
                _flexD.SetCol("NM_PJT", "프로젝트명", 140, 100, false, typeof(string));
                _flexD.SetCol("SEQ_PROJECT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 항번" : "프로젝트항번", 120, false, typeof(decimal));
            }
            if (Config.MA_ENV.PJT형여부 == "Y")
            {
                _flexD.SetCol("CD_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 코드" : "프로젝트 품목코드", 140, false, typeof(string));
                _flexD.SetCol("PJT_NM_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 명" : "프로젝트 품목명", 140, false, typeof(string));
                _flexD.SetCol("PJT_STND_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 규격" : "프로젝트 품목규격", 140, false, typeof(string));
            }
            //_flexD.SetCol("QT_GOOD", "현재고", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY); //2011.05.02 추가(최규원)
            //_flexD.SetCol("DC_RMK", "비고", 100, 50, true);

            _flexD.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            _flexD.SetCodeHelpCol("CD_SL", HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CD_SL", "NM_SL" }, new string[] { "CD_SL", "NM_SL" }, new string[] { "CD_SL", "NM_SL" });
            if (Config.MA_ENV.YN_UNIT == "Y")
            {
                _flexD.SetCodeHelpCol("CD_PJT", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new String[] { "CD_PJT", "NM_PJT", "SEQ_PROJECT", "CD_PJT_ITEM", "PJT_NM_ITEM", "PJT_STND_ITEM" }, new String[] { "NO_PROJECT", "NM_PROJECT", "SEQ_PROJECT", "CD_PJT_ITEM", "NM_PJT_ITEM", "PJT_ITEM_STND" }, new String[] { "CD_PJT", "NM_PROJECT", "SEQ_PROJECT", "CD_PJT_ITEM", "PJT_NM_ITEM", "PJT_STND_ITEM" }, ResultMode.FastMode);
                _flexD.SetCodeHelpCol("CD_PJT_ITEM", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new String[] { "CD_PJT", "NM_PROJECT", "SEQ_PROJECT", "CD_PJT_ITEM", "PJT_NM_ITEM", "PJT_STND_ITEM" }, new String[] { "NO_PROJECT", "NM_PROJECT", "SEQ_PROJECT", "CD_PJT_ITEM", "NM_PJT_ITEM", "PJT_ITEM_STND" }, new String[] { "CD_PJT", "NM_PROJECT", "SEQ_PROJECT", "CD_PJT_ITEM", "PJT_NM_ITEM", "PJT_STND_ITEM" }, ResultMode.FastMode);
            }
            else
            {
                _flexD.SetCodeHelpCol("CD_PJT", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new String[] { "CD_PJT", "NM_PROJECT" }, new String[] { "NO_PROJECT", "NM_PROJECT" });
            }         

            _flexD.Cols["NO_POLINE"].TextAlign = TextAlignEnum.CenterCenter;
            _flexD.Cols["NO_PO_MAL_LINE"].TextAlign = TextAlignEnum.CenterCenter;

            _flexD.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);
            _flexD.BeforeCodeHelp += new BeforeCodeHelpEventHandler(_Grid_BeforeCodeHelp);
            _flexD.ValidateEdit += new ValidateEditEventHandler(_Grid_ValidateEdit);
            //_flexD.AfterCodeHelp += new AfterCodeHelpEventHandler(Grid_AfterCodeHelp);

        }

        #endregion

        #region -> InitEvent
        private void InitEvent()
        {
            btn메일전송.Click +=new EventHandler(btnMAIL알림_Click);
            tb_NO_EMP.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(tb_NO_EMP_QueryAfter);
            bp_CD_SL.QueryBefore += new BpQueryHandler(OnBpCodeTextBox_QueryBefore);
            btn_SL_Accept.Click += new EventHandler(btn_Appet_Click);
            txt바코드.KeyPress += new KeyPressEventHandler(txt바코드_KeyPress);
            btn첨부파일.Click += new EventHandler(btn첨부파일_Click);
            cbo공장.SelectionChangeCommitted+=new EventHandler(cbo공장_SelectionChangeCommitted);
        }
       
        #endregion


        #region -> 헤더,그리드 초기화
        private void Initial_Binding()// 헤더,그리드 초기화
        {
            DataSet ds = _biz.Initial_DataSet();//디비가 아닌   biz단에서 데이터셋을 생성해서 가져옴

            _header.SetBinding(ds.Tables[0], panel_Head);
            _header.SetBinding(ds.Tables[0], oneGrid1); 
            _header.ClearAndNewRow();                                   // 처음에 행을 하나 만든다. 초기에 추가모드로 하기 위해
            _flex.Binding = ds.Tables[1];
        }
        #endregion

        //#region -> 버튼 위치 셋팅
        //private void BTN_LOCATION_SETTING()
        //{

        //    RoundedButton[] r_button = { m_btnTR_TO, m_btnLC, b_OrderApp, m_btn_Rev, b_IQC_Apply, btn메일전송, m_btn_IqcStore };   
        //    SetButtonDisp(r_button, b_Inputlot.Location.X);

        //}
        //#endregion
        #endregion

        #region ♣ 저장관련 메소드

        #region -> SaveData

        protected override bool SaveData()
        {
            string no_seq = tb_NoIsuRcv.Text;
            string no_ioseq = "";

            if (추가모드여부)
            {
                no_seq = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PU", "04", tb_DT_RCV.Text.Substring(0, 6));
                no_ioseq = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PU", "06");

                if (no_seq == "" || no_seq == null)
                    return false;

                _header.CurrentRow["NO_RCV"] = no_seq;
                _header.CurrentRow["CD_PLANT"] = D.GetString(cbo공장.SelectedValue);
                if (_flex.HasNormalRow)
                {
                    // for (int i = _flex.Rows.Fixed; i < _flex.Rows.Count; i++)
                    foreach (DataRow dr in _flex.DataTable.Rows)
                    {
                        if (Convert.ToDecimal(dr["QT_REQ"]) /*+ Convert.ToDecimal(_flex[i, "QT_BAD1"])*/ == 0)
                        {
                            ShowMessage("수불 수량이 0이 있습니다.");
                            // _flex.Row = i;
                            return false;
                        }

                        dr["NO_RCV"] = no_seq;
                        dr["NO_IO"] = no_ioseq;
                        dr["DT_IO"] = _header.CurrentRow["DT_REQ"];
                        dr["FG_IO"] = "001";
                        dr["CD_QTIOTP"] = dr["FG_RCV"];
                        dr["NM_QTIOTP"] = dr["NM_FG_RCV"];
                        dr["CD_PLANT"] = _header.CurrentRow["CD_PLANT"];

                    }

                }
                tb_NoIsuRcv.Text = no_seq;
            }
            else 
            {
                // 추가 적용일경우...
                if (_flex.HasNormalRow)
                {
                    // for (int i = _flex.Rows.Fixed; i < _flex.Rows.Count; i++)
                    DataRow [] drs = _flex.DataTable.Select("ISNULL(NO_RCV,'') = '' OR ISNULL(NO_IO,'') = ''", "", DataViewRowState.Added);
                    DataRow [] drs_t = _flex.DataTable.Select("ISNULL(NO_IO,'') <> ''");
                    if (drs_t == null || drs_t.Length < 1)
                    {
                        // 기처리된 이후 추가로 적용을 받을 경우 수불번호가 없을 수 없다.
                        // 혹시나 체크하는 기능
                        ShowMessage("저장된 내용중 수불번호가 없습니다. 확인 바랍니다.");
                        return false;
                    }

                    foreach (DataRow dr in drs)
                    {
                        if (dr.RowState != DataRowState.Added) continue; // 한번 더 체크
                        dr["NO_RCV"] = _header.CurrentRow["NO_RCV"];
                        dr["NO_IO"] = drs_t[0]["NO_IO"];
                        dr["DT_IO"] = _header.CurrentRow["DT_REQ"];
                        dr["FG_IO"] = "001";
                        dr["CD_QTIOTP"] = dr["FG_RCV"];
                        dr["NM_QTIOTP"] = dr["NM_FG_RCV"];
                        dr["CD_PLANT"] = _header.CurrentRow["CD_PLANT"];

                    }
                }

            }

            if (_header.CurrentRow != null && _flex.HasNormalRow)
            {
                if (_header.CurrentRow["FG_RCV"].ToString() == "")
                    _header.CurrentRow["FG_RCV"] = _flex.DataTable.Rows[0]["FG_RCV"].ToString();  //헤더에 입고형태 컨트롤을 없앴으므로 그리드에 있는 값을 넣어준다.
            }

            DataTable dtH = _header.GetChanges();
            DataTable dtL = _flex.GetChanges();
            DataTable dtLOT = null;
            DataTable dtSERL = null;

            if (dtH == null && dtL == null)
            {
                ShowMessage(공통메세지.변경된내용이없습니다);
                return false;
            }

            if (dtL != null && dtL.Rows.Count > 0)
            {
                // 관리수량과 재고수량이 다른경우 
                DataRow[] drs = dtL.Select("(QT_REQ_MM * RATE_EXCHG) <> QT_REQ", "");

                if (m_sEnv == "Y" && drs != null && drs.Length > 0)
                {
                    P_PU_REQCHK_SUB m_dlg = new P_PU_REQCHK_SUB(MainFrameInterface, dtL);
                    if (m_dlg.ShowDialog(this) == DialogResult.Cancel)
                        return false;

                    // 선택되지 않는것은 관리수량과 
                    DataTable _rdt = m_dlg.gdt_return;

                    if (_rdt != null && _rdt.Rows.Count > 0)
                    {
                        //  foreach (DataRow row in _rdt)
                        for (int i = 0; i < _rdt.Rows.Count; i++)
                        {
                            //drs = dtL.Select("NO_RCV = '" + _rdt.Rows[i]["NO_RCV"].ToString() + "' AND NO_LINE = " + _rdt.Rows[i]["NO_LINE"].ToString(), "");
                            //if (_rdt != null && _rdt.Rows.Count > 0)
                            //{
                            //    drs[0]["QT_REQ"] = Convert.ToDecimal(drs[0]["QT_REQ_MM"]) * Convert.ToDecimal(drs[0]["RATE_EXCHG"]);
                            //}

                            for (int row = 0; row < _flex.DataTable.Rows.Count; row++)
                            {
                                if (_flex.DataTable.Rows[row]["NO_RCV"].ToString() == _rdt.Rows[i]["NO_RCV"].ToString() &&
                                    _flex.DataTable.Rows[row]["NO_LINE"].ToString() == _rdt.Rows[i]["NO_LINE"].ToString())
                                {
                                    _flex.DataTable.Rows[row]["QT_REQ"] = Unit.수량(DataDictionaryTypes.PU, Convert.ToDecimal(_flex.DataTable.Rows[row]["QT_REQ_MM"]) * Convert.ToDecimal(_flex.DataTable.Rows[row]["RATE_EXCHG"]));
                                    break;
                                }
                            }

                        }
                        dtL = _flex.GetChanges();
                    }

                }

                // m_sPJT재고사용 관련
                // PJT재고를 표현하기 어려 LOT재고로 대치 NO_LOT에 CD_PJT넣어 관리
                // 김광석, 김형석
                if (String.Compare(MNG_LOT, "Y") == 0 && dtL != null)
                {
                    DataTable _dtLOT = dtL.Clone();
                    _dtLOT = new DataView(dtL, "NO_LOT = 'YES'", "", DataViewRowState.CurrentRows).ToTable();
                    if (_dtLOT.Rows.Count > 0)
                    {
                        //foreach (DataRow drLOT in DR)
                        //{
                        //    _dtLOT.ImportRow(drLOT);
                        //}

                        if (m_sPJT재고사용 == "100" && m_bPJT사용)
                        {
                            dtLOT = _dtLOT;
                            dtLOT.Columns.Add("FG_PS", typeof(string), "1");
                            dtLOT.Columns.Add("QT_IO", typeof(decimal));
                            dtLOT.Columns.Add("NO_IOLINE2", typeof(decimal),"0");
                            dtLOT.Columns.Add("DC_LOTRMK", typeof(string), "");
                            dtLOT.Columns.Remove("YN_RETURN");
                            foreach (DataRow dr_lot in dtLOT.Rows)
                            {
                                dr_lot["NO_LOT"] = dr_lot["CD_PJT"];
                                dr_lot["QT_IO"] = dr_lot["QT_REQ"];
//                                dr_lot["DT_LIMIT"] = null;

                            }
                            
                        }
                        else
                        {


                            if (서버키("SRPACK") && // 삼륭물산
                            //if (서버키("SRPACK") && // 삼륭물산
                                (D.GetString(cbo_TRANS.SelectedValue) == "004" || D.GetString(cbo_TRANS.SelectedValue) == "005")
                            )  //   해외
                            {


                                P_PU_Z_SRPACK_LOT_SUB_R dlg = new P_PU_Z_SRPACK_LOT_SUB_R(_dtLOT);

                                if (dlg.ShowDialog(this) == DialogResult.OK)
                                {
                                    dtLOT = dlg.dtL;

                                    for (int i = 1; i <= 20; i++)
                                    {
                                        dtLOT.Columns.Add("CD_MNG" + i, typeof(string), "");
                                    }
                                }
                                else
                                    return false;
                            }
                            else
                            {

                                P_PU_LOT_SUB_R m_dlg;

                                if (서버키("SATREC"))
                                {
                                    string[] stra_value = new string[] { tb_DT_RCV.Text };
                                    m_dlg = new P_PU_LOT_SUB_R(_dtLOT, stra_value);
                                }
                                else
                                {
                                    m_dlg = new P_PU_LOT_SUB_R(_dtLOT);
                                }

                                if (m_dlg.ShowDialog(this) == DialogResult.OK)
                                    dtLOT = m_dlg.dtL;
                                else
                                    return false;
                            }
                            
                        }

                    }
                }

                //시리얼추가 
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

                        P_PU_SERL_SUB_R m_dlg3;

                        m_dlg3 = new P_PU_SERL_SUB_R(_dtSERL);
                        if (m_dlg3.ShowDialog(this) == DialogResult.OK)
                            dtSERL = m_dlg3.dtL;
                        else
                        {
                            //if (추가모드여부)
                            //{
                            //    tb_NO_IO.Text = "";
                            //    _header.CurrentRow["NO_IO"] = "";
                            //}
                            return false;
                        }
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

                if (dt_lc.Rows.Count > 0)
                {
                    dt_location = P_OPEN_SUBWINDOWS.P_MA_LOCATION_R_SUB(dt_lc, out b_lct);

                    if (b_lct == false)
                        return false;
                }

            }
            #endregion 

            //외주일경우 자재출고
            DataTable dtHH = null;
            DataTable dtLL = null;

            if (m_YN_SU == "100")
            {
                dtLL = _flexD.GetChanges();
                string NO_IO = string.Empty;
                string NO_IO_MGMT = string.Empty;

                if (dtLL != null && dtLL.Rows.Count != 0)
                {
                    if (추가모드여부)
                    {
                        NO_IO = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PU", "15", tb_DT_RCV.Text.Substring(0, 6));
                        NO_IO_MGMT = no_ioseq;

                    }
                    else
                    {
                        NO_IO_MGMT = _biz.getNO_IO_MGMT(tb_NoIsuRcv.Text);
                        NO_IO = _biz.getNO_IO(NO_IO_MGMT);

                        if (NO_IO == string.Empty)
                        {
                            NO_IO = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PU", "15", tb_DT_RCV.Text.Substring(0, 6));
                        }
                    }
                    foreach (DataRow dr in dtLL.Rows)
                    {
                        if (D.GetString(dr["CD_SL"]) == string.Empty)
                        {
                            ShowMessage(공통메세지._은는필수입력항목입니다, "창고");
                            return false;
                        }

                        dr["NO_IO"] = NO_IO;
                        dr["NO_IO_MGMT"] = NO_IO_MGMT;
                    }


                    DataTable dtSL = new DataView(dtLL, "YN_PARTNER_SL = 'Y'", "", DataViewRowState.CurrentRows).ToTable(true, new string[] { "CD_PLANT", "CD_PARTNER","CD_SL" });

                    foreach (DataRow drSL in dtSL.Rows)
                    {
                        string CD_SL = _biz.getCD_SL(D.GetString(drSL["CD_PARTNER"]), D.GetString(drSL["CD_PLANT"]));

                        if (D.GetString(drSL["CD_SL"]) != CD_SL)
                        {
                            ShowMessage(공통메세지._와_은같아야합니다,new string[]{DD("출고창고"),DD("외주거래처별창고")});
                            return false;
                        }
                    }

                    if (추가모드여부)
                    {
                        dtHH = dtLL.Clone();
                        dtHH.ImportRow(dtLL.Rows[0]);
                    }

                }
            }



            bool bSuccess = _biz.Save(dtH, dtL, dtLOT, no_ioseq, dtSERL, D.GetString(cbo공장.SelectedValue), D.GetString(tb_NoIsuRcv.Text),dt_location,m_YN_special,dtHH,dtLL);

            if (!bSuccess)
                return false;

            _header.AcceptChanges();
            _flex.AcceptChanges();

            if (m_YN_SU == "100")
            {
                _flexD.AcceptChanges();
            }

            //m_btnDel.Enabled = false;

            if(_flex.HasNormalRow) txt_NOIO.Text = D.GetString(_flex.DataTable.Rows[0]["NO_IO"]); //그리드 row가 존재할때 no_io를 컨트롤로 넣어준다
            b_OrderApp.Enabled = false;
            m_btnLC.Enabled = false;
            m_btnTR_TO.Enabled = false;

            ControlEnabledDisable(false);
            return true;
        }

        #endregion

        #endregion

        #region ♣ 메인버튼 이벤트

        #region -> 조회버튼클릭

        // 브라우저의 조회 버턴이 클릭될때 처리 부분
        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!base.BeforeSearch()) return;

                
                P_PU_REQ_SUB dlg = new P_PU_REQ_SUB(tb_NM_PARTNER.CodeValue.ToString(), D.GetString(cbo공장.SelectedValue), tb_NO_EMP.CodeValue.ToString(), "Y");

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    DataSet ds = _biz.Search(dlg.m_NO_RCV, "Y");   //자동입고일 경우에만 조회해 보이기 위해 'Y' 표시

                    _header.SetDataTable(ds.Tables[0]);
                    txt_NOIO.Text = string.Empty;

                    if (ds != null && ds.Tables.Count > 1)
                    {
                        DataTable ldt_head = ds.Tables[0];
                        DataTable ldt_line = ds.Tables[1];

                        Button_Enabled(ldt_head, ldt_line);
                        _flex.Binding = ds.Tables[1];

                        if (!_flex.HasNormalRow)
                            if (!_header.CurrentRow.IsNull(0))
                                this.ShowMessage(PageResultMode.SearchNoData);

                        if (m_YN_SU == "100")
                        {
                            string NO_IO = _biz.getNO_IO_MGMT(tb_NoIsuRcv.Text);
                            _flexD.Binding = _biz.Search_MATL(NO_IO);

                            string filter = "NO_PO = '" + D.GetString(_flex["NO_PO"]) + "' AND NO_POLINE = '" + D.GetString(_flex["NO_POLINE"]) + "' ";
                            _flexD.RowFilter = filter;
                        }

                        _header.AcceptChanges();
                        _flex.AcceptChanges();
                        ControlEnabledDisable(false);
                    }
                }

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }

        }
        #region -> 버튼 활성화

        private void Button_Enabled(DataTable ldt_head, DataTable ldt_line)
        {
            if (ldt_head != null && ldt_head.Rows.Count > 0)
            {
                DataRow row = ldt_head.Rows[0];
                DataRow[] ldr_row = ldt_line.Select("QT_GR_MM > 0");

                _isChagePossible = ((ldr_row != null) && (ldr_row.Length > 0)) == true ? false : true;

                if (!_isChagePossible)
                {
                    b_OrderApp.Enabled = false;
                    m_btnTR_TO.Enabled = false;
                    m_btnLC.Enabled = false;
                    //m_btnDel.Enabled = false;
                }
                //else
                //{
                //    m_btnDel.Enabled = true;
                //}

                if (ldt_head.Rows[0]["FG_TRANS"].ToString() == "001" ||
                    ldt_head.Rows[0]["FG_TRANS"].ToString() == "002")//국내
                {
                    cbo_PROCESS.Enabled = false;
                    b_OrderApp.Enabled = true;
                    m_btnTR_TO.Enabled = false;
                    m_btnLC.Enabled = false;

                }
                else if (ldt_head.Rows[0]["FG_TRANS"].ToString() == "003")
                {
                    cbo_PROCESS.Enabled = true;
                    if (ldt_head.Rows[0]["FG_PROCESS"].ToString() == "001")
                    {
                        b_OrderApp.Enabled = false;
                        m_btnTR_TO.Enabled = false;
                        m_btnLC.Enabled = true;
                    }
                    if (ldt_head.Rows[0]["FG_PROCESS"].ToString() == "002")
                    {
                        b_OrderApp.Enabled = true;
                        m_btnTR_TO.Enabled = false;
                        m_btnLC.Enabled = false;
                    }

                }
                else if (ldt_head.Rows[0]["FG_TRANS"].ToString() == "004" ||
                    ldt_head.Rows[0]["FG_TRANS"].ToString() == "005")
                {
                    cbo_PROCESS.Enabled = true;
                    if (ldt_head.Rows[0]["FG_PROCESS"].ToString() == "001")
                    {
                        b_OrderApp.Enabled = false;
                        m_btnLC.Enabled = false;
                        m_btnTR_TO.Enabled = true;
                    }
                }

                if (ldt_head.Rows[0]["FG_PROCESS"].ToString() == "001")
                {
                    if (cbo_TRANS.SelectedValue.ToString() == "002")
                    {
                        b_OrderApp.Enabled = true;
                        m_btnTR_TO.Enabled = false;
                    }
                    if (cbo_TRANS.SelectedValue.ToString() == "003")
                    {
                        b_OrderApp.Enabled = false;
                        m_btnTR_TO.Enabled = false;
                        m_btnLC.Enabled = true;
                    }
                    if (cbo_TRANS.SelectedValue.ToString() == "004" ||
                        cbo_TRANS.SelectedValue.ToString() == "005")
                    {
                        b_OrderApp.Enabled = false;
                        m_btnTR_TO.Enabled = true;
                        m_btnLC.Enabled = false;
                    }
                }
                if (ldt_head.Rows[0]["FG_PROCESS"].ToString() == "002")
                {
                    if (cbo_TRANS.SelectedValue.ToString() == "002")
                    {
                        b_OrderApp.Enabled = true;
                        m_btnTR_TO.Enabled = false;
                    }
                    if (cbo_TRANS.SelectedValue.ToString() == "003")
                    {
                        b_OrderApp.Enabled = true;
                        m_btnTR_TO.Enabled = false;
                        m_btnLC.Enabled = false;
                    }
                }
            }
        }

        #endregion

        #endregion

        #region -> 추가버튼클릭

        // 브라우저의 추가 버턴이 클릭될때 처리 부분
        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeAdd())
                    return;

                Debug.Assert(_header.CurrentRow != null);       // 혹시나 해서 한번 더 확인
                Debug.Assert(_flex.DataTable != null);          // 혹시나 해서 한번 더 확인

                _flex.DataTable.Rows.Clear();
                _flex.AcceptChanges();

                _header.ClearAndNewRow();
                InitControl();
                ControlEnabledDisable(true);
                cbo_TRANS_SelectionChangeCommitted(null, null);

                if (cbo_TRANS.SelectedValue.ToString() == "001")
                    cbo_PROCESS.Enabled = false;

                if (m_YN_SU == "100")
                {
                    _flexD.DataTable.Rows.Clear();
                    _flexD.AcceptChanges();
                }

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 삭제버튼클릭

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeDelete())
                    return;

                DialogResult result = MessageBoxEx.Show(this.GetMessageDictionaryItem("MA_M000103"), this.PageName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (_flex != null && _flex.HasNormalRow)
                    {
                        string NO_RCV = _header.CurrentRow["NO_RCV"].ToString();
                        string NO_IO = _flex[_flex.Rows.Fixed, "NO_IO"].ToString();
                        string NO_REQ = D.GetString(tb_NoIsuRcv.Text);
                        string NO_IO_MGMT = string.Empty;

                        if (m_YN_SU == "100")
                        {
                            NO_IO_MGMT = _biz.getNO_IO(NO_IO);
                        }
                        object[] lsa_args1 = new object[] { NO_RCV, NO_IO, this.LoginInfo.CompanyCode, NO_IO_MGMT};
                        object[] lsa_args2 = new object[] { this.LoginInfo.CompanyCode, NO_REQ };

                        _biz.Delete(lsa_args1, lsa_args2);
                        ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);

                        OnToolBarAddButtonClicked(sender, e);
                        //_header.AcceptChanges();
                        //_flex.AcceptChanges();
                        //SetControlEnabled(true);
                        ControlEnabledDisable(true);
                    }
                }


            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }

        }

        #endregion

        #region -> 저장버튼클릭

        // 브라우저의 저장 버턴이 클릭될때 처리 부분

        protected override bool BeforeSave()
        {
            if (!base.BeforeSave())
                return false;

            if (!Verify())
                return false;

            return true;
        }
        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSave())
                    return;

                if (!FieldCheck())
                    return;

                if (!IsChanged())
                    return;

                ToolBarSaveButtonEnabled = false;

                if (MsgAndSave(PageActionMode.Save))
                {
                    ShowMessage(PageResultMode.SaveGood);
                    if (!_flex.HasNormalRow)
                        OnToolBarAddButtonClicked(null, null);
                    else
                        Button_Enabled(_header.CurrentRow.Table, _flex.DataTable);
                }
                else
                    ToolBarSaveButtonEnabled = true;

                if (chk_barcode_use.Checked == true)
                    txt바코드.Focus();

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
                ToolBarSaveButtonEnabled = true;
            }
        }

        #endregion

        #region -> 출력

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {

            try
            {
                SetPrint(true);

            }
            catch (Exception Ex)
            {
                this.MsgEnd(Ex);
            }
         
        }

        //check true -> print 
        //      false -> mail
        private void SetPrint(bool check)
        {
            if (!_flex.HasNormalRow)
            {
                ShowMessage(공통메세지._이가존재하지않습니다, "데이터");
                return;
            }

            ReportHelper rptHelper = new ReportHelper("R_P_PU_REQ_REG_1", "구매입고처리");

             Dictionary<string, string> dic = new Dictionary<string, string>();

            dic["의뢰번호"] = D.GetString(_flex["NO_RCV"]);
            dic["입고일자"] = D.GetString(tb_DT_RCV.Text);
            dic["거래처코드"] =D.GetString(tb_NM_PARTNER.CodeValue);
            dic["거래처명"] =D.GetString(tb_NM_PARTNER.CodeName);
            dic["담당자코드"] =D.GetString(tb_NO_EMP.CodeValue);
            dic["담당자명"] =D.GetString(tb_NO_EMP.CodeName);
            dic["비고"]    = D.GetString(_header.CurrentRow["DC_RMK"].ToString());
            dic["거래구분"] =D.GetString(_header.CurrentRow["NM_EXCH"].ToString());
            dic["입고번호"] =D.GetString(_flex["NO_IO"]);
            //rptHelper.SetData("의뢰번호", D.GetString(_flex["NO_RCV"]));
            //rptHelper.SetData("입고일자", tb_DT_RCV.Text);
            //rptHelper.SetData("거래처코드", tb_NM_PARTNER.CodeValue);
            //rptHelper.SetData("거래처명", tb_NM_PARTNER.CodeName);
            //rptHelper.SetData("담당자코드", tb_NO_EMP.CodeValue);
            //rptHelper.SetData("담당자명", tb_NO_EMP.CodeName);
            //rptHelper.SetData("비고", D.GetString(_header.CurrentRow["DC_RMK"].ToString()));
            //rptHelper.SetData("거래구분", D.GetString(_header.CurrentRow["NM_EXCH"].ToString()));
            //rptHelper.SetData("입고번호", D.GetString(_flex["NO_IO"]));

            foreach (string key in dic.Keys)
            {
                rptHelper.SetData(key, dic[key]);
            }
            rptHelper.SetDataTable(_flex.DataTable);
            rptHelper.가로출력();


            if (check)
                rptHelper.Print();

            else
            {
                StringBuilder text = new StringBuilder();
                string title = D.GetString(tb_NO_EMP.CodeName) + "/" + D.GetString(tb_NoIsuRcv.Text) + "/" + D.GetString(_flex[_flex.Rows.Fixed, "CD_PJT"]) + "의 구매입고가 등록되었습니다.";
                string msg = string.Empty;
                DataTable dt = null;

                // 메일도움창에 보낼 파라미터셋팅 0번 제목, 1번 받을사람 이메일주소, 3번은 내용
                string[] str_histext = new string[3];


                if (Global.MainFrame.ServerKeyCommon == "ICDERPU")
                {
                    foreach (DataRow dr in _flex.DataTable.Rows)
                    {
                        msg = "품목코드: " + D.GetString(dr["CD_ITEM"]) + " / 품목명: " + D.GetString(dr["NM_ITEM"]) + " / 규격: " + D.GetString(dr["STND_ITEM"]) +
                                     " / 단위: " + D.GetString(dr["UNIT_IM"]) + " / 수량: " + D.GetDecimal(dr["QT_REQ_MM"]).ToString("#,###,##0.####") + 
                                     " / 프로젝트코드: " + D.GetString(dr["CD_PJT"]) + "/ 프로젝트명: " + D.GetString(dr["NM_PROJECT"]);
                        text.AppendLine(msg);
                        text.AppendLine("\n\n");
                    }

                    str_histext[0] = title;
                    str_histext[2] = text.ToString();

                    dt = _biz.getMail_Adress_ICD(_flex.DataTable);

                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            str_histext[1] += D.GetString(dr["NM_KOR"]) + "|" + D.GetString(dr["NO_EMAIL"]) + "|" + "N" + "?"; //담당자는 저장 하지 않기위해 N을 붙여줌.
                        }
                    }
                }
                else
                {
                    foreach (DataRow dr in _flex.DataTable.Rows)
                    {
                        msg = "품목코드: " + D.GetString(dr["CD_ITEM"]) + " / 품목명: " + D.GetString(dr["NM_ITEM"]) + " / 규격: " + D.GetString(dr["STND_ITEM"]) +
                                     " / 단위: " + D.GetString(dr["UNIT_IM"]) + " / 수량: " + D.GetDecimal(dr["QT_REQ_MM"]).ToString("#,###,##0.####") + " / 단가: " + D.GetDecimal(dr["UM_EX_PO"]).ToString("#,###,##0.####") +
                                     " / 금액: " + D.GetDecimal(dr["AM_EXREQ"]).ToString("#,###,##0.####") + " / 프로젝트코드: " + D.GetString(dr["CD_PJT"]) + "/ 프로젝트명: " + D.GetString(dr["NM_PROJECT"]);
                        text.AppendLine(msg);
                        text.AppendLine("\n\n");
                    }

                    str_histext[0] = title;
                    str_histext[1] = Settings1.Default.email_add;
                    str_histext[2] = text.ToString();

                    DataTable groupby_no_po = _flex.DataTable.DefaultView.ToTable(true, new string[] { "NO_PO" }); //여러 발주건이 있을 수 있기 때문에...
                    dt = _biz.getMail_Adress(groupby_no_po);


                }


                if (dt != null && dt.Rows.Count != 0)
                {
                    DataTable dt_emp = dt.DefaultView.ToTable(true, new string[] { "NO_EMP", "NM_KOR", "NO_EMAIL" }); //여러 발주건에 담당자가 같을 수도 있기때문에.. 
                    foreach (DataRow dr in dt_emp.Rows)
                    {
                        str_histext[1] += D.GetString(dr["NM_KOR"]) + "|" + D.GetString(dr["NO_EMAIL"]) + "|" + "N" + "?"; //담당자는 저장 하지 않기위해 N을 붙여줌.
                    }
                }

                P_MF_EMAIL mail = new P_MF_EMAIL(new string[] { D.GetString(tb_NM_PARTNER.CodeValue) }, "R_P_PU_REQ_REG_1", new ReportHelper[] { rptHelper }, dic, "구매입고처리", str_histext);
                mail.ShowDialog();
                Settings1.Default.email_add = mail._str_rt_data[0];
                Settings1.Default.Save();
            }
        }
        #endregion


        #endregion

        #region ♣ 주요 버튼 이벤트

        #region -> 발주적용

        private void 발주적용_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (!Cheak_For_btn())
                    return;
    
                string FG_TRANS = cbo_TRANS.SelectedValue.ToString();			// 거래구분

                string CD_PLANT = D.GetString(cbo공장.SelectedValue);
                string CD_PARTNER = tb_NM_PARTNER.CodeValue;
                string NM_PARTNER = tb_NM_PARTNER.CodeName;
                P_PU_REQPO_SUB dlg_PuRcvSub = null;


                if (Global.MainFrame.ServerKeyCommon == "KORAVL")
                    dlg_PuRcvSub = new P_PU_REQPO_SUB(FG_TRANS, CD_PLANT, CD_PARTNER, NM_PARTNER, _flex.DataTable, tb_NO_EMP.CodeValue, tb_NO_EMP.CodeName);
                else
                    //0:FG_TRANS,1:CD_PLANT,2:CD_PARTNER,3:NM_PARTNER,4:NO_EMP,5:NM_KOR,6:DT_FROM
                    dlg_PuRcvSub = new P_PU_REQPO_SUB( new object[] { (int)P_PU_REQPO_SUB.search_type.FG_TRANS ,
                                                                    (int)P_PU_REQPO_SUB.search_type.CD_PLANT ,
                                                                    (int)P_PU_REQPO_SUB.search_type.CD_PARTNER ,
                                                                    (int)P_PU_REQPO_SUB.search_type.NM_PARTNER ,
                                                                    (int)P_PU_REQPO_SUB.search_type.DT_FROM,
                                                                    (int)P_PU_REQPO_SUB.search_type.DATATABLE },
                                                        new object[] {  FG_TRANS, 
                                                                        CD_PLANT, 
                                                                        CD_PARTNER, 
                                                                        NM_PARTNER,
                                                                        Global.MainFrame.ServerKeyCommon == "ASUNG" ? D.GetString(tb_DT_RCV.Text) : "",
                                                                        _flex.DataTable});
                    //dlg_PuRcvSub = new P_PU_REQPO_SUB(FG_TRANS, CD_PLANT, CD_PARTNER, NM_PARTNER, _flex.DataTable);

                m_btnTR_TO.Enabled = false;

                if (dlg_PuRcvSub.ShowDialog(this) == DialogResult.OK)
                {
                    cbo_TRANS.Enabled = false;      // 거래구분을 수정할 수 없게

                    DataTable dt = _flex.DataTable.Clone();

                    foreach (DataColumn col in dt.Columns)
                    {
                        if (col.DataType == typeof(decimal))
                            col.DefaultValue = 0;
                    }
                    InserGridtAddREQ(dlg_PuRcvSub.gdt_return);
                    ControlEnabledDisable(false);
                    //if(_header.JobMode == JobModeEnum.추가후수정 && _flex.HasNormalRow)
                    //{
                    //    m_btnDel.Enabled = true;

                    //}
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #region -> 그리드 채워주는 메소드
        private void InserGridtAddREQ(DataTable pdt_Line)
        {
            try
            {
                if (pdt_Line == null || pdt_Line.Rows.Count <= 0)
                    return;

                DataRow row;
                decimal max_no_line = _flex.GetMaxValue("NO_LINE");
                _flex.Redraw = false;
                string NO_PO_MULTI = string.Empty;

                for (int i = 0; i < pdt_Line.Rows.Count; i++)
                {
                    row = pdt_Line.Rows[i];
                    max_no_line++;
                    _flex.Rows.Add();
                    _flex.Row = _flex.Rows.Count - 1;

                    _flex["NO_LINE"] = max_no_line;
                    _flex["NO_IOLINE"] = max_no_line;
                    //_flex["NO_IO"] = row["NO_IO"];
                    _flex["CD_ITEM"] = row["CD_ITEM"];						//품목코드
                    _flex["NM_ITEM"] = row["NM_ITEM"];						//품목명
                    _flex["STND_ITEM"] = row["STND_ITEM"];					//규격

                    if (row["CD_UNIT_MM"] == null)
                        row["CD_UNIT_MM"] = "";
                    _flex["CD_UNIT_MM"] = row["CD_UNIT_MM"];				//수배단위
                    _flex["UNIT_IM"] = row["UNIT_IM"];						//재고단위
                    _flex["RATE_EXCHG"] = row["RATE_EXCHG"];				//단위환산량으로 수배단위, 재고단위에 의해서 결정된다.	
                    _flex["RT_VAT"] = row["RT_VAT"];
                    _flex["DT_LIMIT"] = row["DT_LIMIT"];					//납기일

                    //_flex["QT_REQ_MM"] = row["CAL_QT_PO_MM"];				//수배수량
                    //_flex["QT_REAL"] = row["CAL_QT_PO_MM"];
                    //_flex["QT_REQ"] = row["CAL_QT_PO"];
                    //_flex["QT_GOOD_INV"] = row["CAL_QT_PO"];		        //의뢰량

                    if (row["CD_ZONE"] == null)
                        row["CD_ZONE"] = "";
                    _flex["CD_ZONE"] = row["CD_ZONE"];				//저장위치

                    _flex["QT_REQ_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ_MM"]));				    //수배수량
                    _flex["QT_REAL"] =   Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ_MM"]));
                    _flex["QT_REQ"] =  Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ"]));
                    _flex["QT_GOOD_INV"] =   Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ"]));					//의뢰량

                    _flex["QT_PASS"] =  Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ"]));                      //합격수량(재고단위)
                    _flex["QT_REJECTION"] = 0;                             //불량수량(재고단위)

                    // MA_PITEM.FG_FOQ =  'Y'이면 YN_INSP = 'Y'
                    _flex["YN_INSP"] = "N";

                    if (row["FG_IQC"].ToString() == "Y")
                    {
                        _flex["YN_INSP"] = "Y";
                    }

                    _flex["UM_EX_PO"]  = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX_PO"]));								//발주단가
                    _flex["UM_EX"]     = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX"]));									//단가<<--발주단가						
                    _flex["AM_EX"]     =  Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM_EX"]));
                    _flex["CD_PJT"] = row["CD_PJT"];						//프로젝트코드
                    _flex["CD_PURGRP"] = row["CD_PURGRP"];
                    _flex["NO_PO"] = row["NO_PO"];
                    _flex["NO_POLINE"] = row["NO_LINE"];
                    _flex["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM"]));
                    
                    //_flex["AM"] = System.Math.Floor(_flex.CDecimal(row["JAN_AM"]));
                    _flex["AM"] = Unit.원화금액(DataDictionaryTypes.PU,  _flex.CDecimal(row["JAN_AM"]));
                    
                    //_flex["VAT"] = System.Math.Floor(_flex.CDecimal(row["VAT"].ToString()));
                    _flex["VAT"] = Unit.원화금액(DataDictionaryTypes.PU,  _flex.CDecimal(row["VAT"].ToString()));

                    _flex["QT_GR_MM"] = 0;
                    _flex["RT_CUSTOMS"] = 0;
                    _flex["YN_RETURN"] = row["YN_RETURN"];
                    _header.CurrentRow["YN_RETURN"] = row["YN_RETURN"];              //헤더에 입력해줘야한다!
                    _flex["FG_TPPURCHASE"] = row["FG_PURCHASE"];
                    _flex["YN_PURCHASE"] = (row["FG_PURCHASE"].ToString() != "") ? "Y" : "N";

                    _flex["FG_POST"] = row["FG_POST"];			//발주상태 코드
                    _flex["NM_FG_POST"] = row["NM_FG_POST"];		//발주상태 명
                    _flex["FG_RCV"] = row["FG_RCV"];				//발주의 입고형태 코드
                    _flex["NM_FG_RCV"] = row["NM_QTIOTP"];		//발주의 입고형태 명
                    _flex["FG_TRANS"] = row["FG_TRANS"];			//거래구분
                    _flex["FG_TAX"] = row["FG_TAX"];				//과세구분
                    _flex["CD_EXCH"] = row["CD_EXCH"];			//환종
                    _header.CurrentRow["CD_EXCH"] = row["CD_EXCH"];

                    if (D.GetDecimal(row["RT_EXCH"]) == 0 && D.GetString(row["CD_EXCH"]) == "000")
                    {
                        _flex["RT_EXCH"] = 1;			//환율
                    }
                    else
                    {
                        _flex["RT_EXCH"] = D.GetDecimal(row["RT_EXCH"]);
                    }

                    _flex["NO_IO_MGMT"] = "";
                    _flex["NO_IOLINE_MGMT"] = 0;
                    _flex["NO_PO_MGMT"] = "";
                    _flex["NO_POLINE_MGMT"] = 0;

                    _flex["NO_TO"] = "";										//통관번호
                    _flex["NO_TO_LINE"] = 0;									//통관항번

                    _flex["CD_SL"] = row["CD_SL"];
                    _flex["NM_SL"] = row["NM_SL"];

                    _flex["NO_LC"] = "";
                    _flex["NO_LCLINE"] = 0;
                    _flex["FG_TAXP"] = row["FG_TAXP"];
                    //_flex["NO_EMP"] = row["NO_EMP"];
                    _flex["NO_EMP"] = D.GetString(tb_NO_EMP.CodeValue);
                    _flex["NM_PROJECT"] = row["NM_PROJECT"];
                    _flex["YN_AM"] = row["YN_AM"];
                    _header.CurrentRow["YN_AM"] = row["YN_AM"];              //헤더에 입력해줘야한다!
                    _flex["VAT_CLS"] = 0;
                    _flex["YN_AUTORCV"] = row["YN_AUTORCV"];
                    _flex["YN_REQ"] = row["YN_RCV"];

                    _flex["AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM_EX"]));

                    //_flex["AM_REQ"] = System.Math.Floor(_flex.CDecimal(row["JAN_AM"]));
                    _flex["AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, _flex.CDecimal(row["JAN_AM"]));

                    //_flex["AM_TOTAL"] = System.Math.Floor(_flex.CDecimal(row["JAN_AM"]) + _flex.CDecimal(row["VAT"])); //총금액
                    _flex["AM_TOTAL"] = Unit.원화금액(DataDictionaryTypes.PU,  _flex.CDecimal(row["JAN_AM"]) + _flex.CDecimal(row["VAT"])); //총금액

                    _flex["AM_EXRCV"] = 0;
                    _flex["AM_RCV"] = 0;

                    _flex["NM_SYSDEF"] = row["NM_SYSDEF"];
                    _flex["NM_KOR"] = row["NM_KOR"];
                    _flex["NO_LOT"] = row["NO_LOT"];

                    _flex["CD_PLANT"] = _header.CurrentRow["CD_PLANT"].ToString();

                    if (row["CD_EXCH"].ToString() != "")
                    {
                        tb_CD_EXCH.Text = row["NM_SYSDEF"].ToString();
                    }
                     
                    _flex["TP_UM_TAX"] = row["TP_UM_TAX"];
                    _flex["PO_PRICE"] = row["PO_PRICE"]; //추가 20090226 (구매그룹별 단가통제 chk용)
                    _flex["NM_PURGRP"] = row["NM_PURGRP"];

                    _flex["NO_SERL"] = row["NO_SERL"];
                    _flex["DC_RMK"] = row["DC1"];       //추가 2010.04.12

                    if (BASIC.GetMAEXC("구매입고처리-업체별프로세스선택") == "100")  //YTN전용 발주적용밖에 쓰지않음 이형준대리요청
                    {
                        _flex["DC_RMK2"] = D.GetString(txt관리번호.Text); //의뢰단까지만 저장하도록할것입니다. 수불테이블까지는 안들어가도됩니다. 이형준대리요청
                    }
                    else
                    {
                       if(Global.MainFrame.ServerKeyCommon != "SHINKI")
                           _flex["DC_RMK2"] = row["DC2"];
                    }
                    
                    // 2011-06-03, 최승애 , PIMS : M20110519153
                    _flex["REV_QT_REQ_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ_MM"]));             // 가입고 적용시 의뢰량 2011-06-03, 최승애 추가
                    _flex["REV_AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM_EX"]));          // 가입고 적용시 외화금액 2011-06-03, 최승애 추가
                    _flex["REV_AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, _flex.CDecimal(row["JAN_AM"]));             // 가입고 적용시 원화금액 2011-06-03, 최승애 추가


                    if (Config.MA_ENV.PJT형여부 == "Y")
                    {
                        _flex["CD_PJT_ITEM"] = row["CD_PJT_ITEM"];
                        _flex["NM_PJT_ITEM"] = row["NM_PJT_ITEM"];
                        _flex["PJT_ITEM_STND"] = row["PJT_ITEM_STND"];
                        _flex["NO_WBS"] = row["NO_WBS"];
                        _flex["NO_CBS"] = row["NO_CBS"];
                        _flex["SEQ_PROJECT"] = D.GetDecimal(row["SEQ_PROJECT"]);
                    }

                    if (pdt_Line.Columns.Contains("CD_ITEM_ORIGIN"))
                    {
                        _flex["CD_ITEM_ORIGIN"] = row["CD_ITEM_ORIGIN"];
                        _flex["NM_ITEM_ORIGIN"] = row["NM_ITEM_ORIGIN"];
                        _flex["STND_ITEM_ORIGIN"] = row["STND_ITEM_ORIGIN"];
                    }

                    _flex["GI_PARTNER"] = D.GetString(row["GI_PARTNER"]);
                    _flex["NM_GI_PARTER"] = row["NM_GI_PARTER"];

                    _flex["PI_PARTNER"] = D.GetString(row["PI_PARTNER"]);
                    _flex["PI_LN_PARTNER"] = row["PI_LN_PARTNER"];

                    if (MainFrameInterface.ServerKeyCommon == "WOORIERP" || MainFrameInterface.ServerKeyCommon == "DZSQL" || MainFrameInterface.ServerKeyCommon == "SQL_")
                    {
                        _flex["NM_USERDEF1"] = row["NM_USERDEF1"];
                        _flex["NM_USERDEF2"] = row["NM_USERDEF2"];
                    }
                    NO_PO_MULTI += D.GetString(row["NO_PO"]) + D.GetString(row["NO_LINE"]) + "|";

                    if (pdt_Line.Columns.Contains("DT_PLAN"))   //2013.07.30 납기예정일추가
                    {
                        _flex["DT_PLAN"] = D.GetString(row["DT_PLAN"]);
                    }
                    _flex["CLS_ITEM"] = row["CLS_ITEM"];
                    if (MainFrameInterface.ServerKeyCommon == "ICDERPU" || MainFrameInterface.ServerKeyCommon == "DZSQL" || MainFrameInterface.ServerKeyCommon == "SQL_")
                    {
                        _flex["DATE_USERDEF1"] = row["DATE_USERDEF1"];
                        _flex["DC_RMK2"] = row["NM_USERDEF2"];
                        //_flex["CDSL_USERDEF1"] = row["CDSL_USERDEF1"];
                        //_flex["NMSL_USERDEF1"] = row["NMSL_USERDEF1"];
                    }
                    if (MainFrameInterface.ServerKeyCommon == "WINPLUS" || MainFrameInterface.ServerKeyCommon == "DZSQL" || MainFrameInterface.ServerKeyCommon == "SQL_")
                    {
                        _flex["NUM_USERDEF1"] = row["NUM_USERDEF1"];
                        _flex["NUM_USERDEF2"] = row["NUM_USERDEF2"];
                        //_flex["CDSL_USERDEF1"] = row["CDSL_USERDEF1"];
                        //_flex["NMSL_USERDEF1"] = row["NMSL_USERDEF1"];
                    }

                    if (bStandard)
                    {
                        _flex["UM_WEIGHT"] = row["UM_WEIGHT"];
                        _flex["TOT_WEIGHT"] = row["TOT_WEIGHT"];
                        _flex["WEIGHT"] = row["WEIGHT"];
                    }

                    if (MainFrameInterface.ServerKeyCommon == "UNIPOINT")
                    {
                        _flex["CD_PARTNER_PJT"] = row["CD_PARTNER_PJT"];
                        _flex["LN_PARTNER_PJT"] = row["LN_PARTNER_PJT"];
                        _flex["NO_EMP_PJT"] = row["NO_EMP_PJT"];
                        _flex["NM_KOR_PJT"] = row["NM_KOR_PJT"];
                        _flex["END_USER"] = row["END_USER"];
                    }

                    _flex["MAT_ITEM"] = row["MAT_ITEM"];

                }
                // 2010.01.08 추가 거래처  
                _header.CurrentRow["CD_PARTNER"] = pdt_Line.Rows[0]["CD_PARTNER"];
                tb_NM_PARTNER.SetCode(pdt_Line.Rows[0]["CD_PARTNER"].ToString(), pdt_Line.Rows[0]["LN_PARTNER"].ToString());


                
                _flex.Redraw = true;


                if (m_YN_SU == "100")
                {
                    SET_FLEXD(NO_PO_MULTI);
                }

                //    ControlEnabledDisable(false);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #endregion

        #region -> 통관적용

        private void 통관적용_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (!Cheak_For_btn())
                    return;


                trade.P_TR_REQLC_SUB dlg_PuRcvSub = new trade.P_TR_REQLC_SUB(D.GetString(cbo공장.SelectedValue), cbo공장.Text,
                              tb_NM_PARTNER.CodeValue.ToString(), tb_NM_PARTNER.CodeName, cbo_TRANS.SelectedValue.ToString());

                if (dlg_PuRcvSub.ShowDialog(this) == DialogResult.OK)
                {
                    InserGridtAdd_TO(dlg_PuRcvSub.통관적용dt, dlg_PuRcvSub.check_app);
                    ToolBarDeleteButtonEnabled = false;
                }




                //object[] args = new Object[6]{MainFrameInterface, cbo_CD_PLANT.SelectedValue.ToString(), cbo_CD_PLANT.Text, 
                //                                 tb_NM_PARTNER.CodeValue.ToString(), tb_NM_PARTNER.CodeName, cbo_TRANS.SelectedValue.ToString()};

                //object obj = this.MainFrameInterface.LoadHelpWindow("P_TR_REQLC_SUB", args);
                //if (((Duzon.Common.Forms.Comm onDialog)obj).ShowDialog() == DialogResult.OK)
                //{
                //    if (obj is IHelpWindow)
                //    {
                //        object[] result = (object[])((IHelpWindow)obj).ReturnValues;
                //        DataTable pdt_Line = (DataTable)result[0];

                //        InserGridtAdd_TO(pdt_Line);
                //        //ToolBarDeleteButtonEnabled = true;
                //    }
                //}
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }


        /// <summary>
        /// Local TO의 경우 그리드값 채워주는것
        /// </summary>
        /// <param name="pdt_Line"></param>
        private void InserGridtAdd_TO(DataTable pdt_Line, bool check)
        {
            try
            {
                if (pdt_Line == null || pdt_Line.Rows.Count <= 0)
                    return;

                DataRow row;
                decimal max_no_line = _flex.GetMaxValue("NO_LINE");
                _flex.Redraw = false;
                string NO_PO_MULTI = string.Empty;


                for (int i = 0; i < pdt_Line.Rows.Count; i++)
                {
                    row = pdt_Line.Rows[i];
                    max_no_line++;
                    _flex.Rows.Add();
                    _flex.Row = _flex.Rows.Count - 1;

                    _flex[_flex.Row, "NO_RCV"] = tb_NoIsuRcv.Text;

                    _flex[_flex.Row, "NO_LINE"] = max_no_line;
                    _flex[_flex.Row, "NO_IOLINE"] = max_no_line;
                    _flex[_flex.Row, "NO_LOT"] = row["NO_LOT"].ToString();

                    _flex[_flex.Row, "CD_ITEM"] = row["CD_ITEM"].ToString();						//품목코드
                    _flex[_flex.Row, "NM_ITEM"] = row["NM_ITEM"].ToString();						//품목명
                    _flex[_flex.Row, "STND_ITEM"] = row["STND_ITEM"].ToString();
                    if (row["CD_UNIT_MM"].ToString() == null)
                        row["CD_UNIT_MM"] = "";				//규격
                    _flex[_flex.Row, "CD_UNIT_MM"] = row["CD_UNIT_MM"].ToString();					//수배단위
                    _flex[_flex.Row, "UNIT_IM"] = row["UNIT_IM"].ToString();						//재고단위
                    if (row["RATE_EXCHG"].ToString() == null || _flex[_flex.Row, "RATE_EXCHG"].ToString() == "0")
                        _flex[_flex.Row, "RATE_EXCHG"] = "1";
                    else
                        _flex[_flex.Row, "RATE_EXCHG"] = row["RATE_EXCHG"];

                    if (BASIC.GetMAEXC_Menu("P_PU_REQ_REG_1", "PU_A00000007") == "100")
                        _flex["DT_LIMIT"] = row["DT_LIMIT"];					//납기일
                    else
                        _flex["DT_LIMIT"] = row["DT_TO"];	

                    if (row["CD_ZONE"] == null)   row["CD_ZONE"] = "";
                    _flex["CD_ZONE"] = row["CD_ZONE"];				//저장위치

                    _flex[_flex.Row, "YN_INSP"] = "N";

                    if (row["FG_IQC"].ToString() != "")
                    {
                        if (row["FG_IQC"].ToString() == "Y")
                        {
                            _flex[_flex.Row, "YN_INSP"] = "Y";
                        }
                        else
                            _flex[_flex.Row, "YN_INSP"] = "N";
                    }

                    //_flex[_flex.Row, "QT_REQ"] = row["QT_TO"];
                    //_flex[_flex.Row, "QT_GOOD_INV"] = row["QT_TO"];								//통관수량(관리수량)
                    //_flex[_flex.Row, "QT_REQ_MM"] = row["QT_TO_MM"];								//통관수배수량


                    _flex[_flex.Row, "QT_REQ"] =  Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ"]));
                    _flex[_flex.Row, "QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ"]));								//통관수량(관리수량)
                    _flex[_flex.Row, "QT_REQ_MM"] = Unit.수량(DataDictionaryTypes.PU , D.GetDecimal(row["QT_REQ_MM"]));								//통관수배수량

                    _flex[_flex.Row, "QT_PASS"] = 0;
                    _flex[_flex.Row, "QT_REJECTION"] = 0;
                    _flex[_flex.Row, "QT_GR"] = 0;
                    _flex[_flex.Row, "QT_GR_MM"] = 0;

                    _flex[_flex.Row, "UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU , D.GetDecimal(row["UM_EX_PO"]));									//발주단가
                    _flex[_flex.Row, "UM_EX"] =  Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX"]));										//단가<<--발주단가						
                    _flex[_flex.Row, "AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["AM_EX"]));
                    _flex[_flex.Row, "CD_PJT"] = row["CD_PJT"].ToString();						//프로젝트코드
                    _flex[_flex.Row, "CD_PURGRP"] = row["CD_PURGRP"].ToString();
                    _flex[_flex.Row, "NO_PO"] = "";
                    _flex[_flex.Row, "NO_POLINE"] = 0;
                    //_flex[_flex.Row, "UM"] = row["UM"];

                    //_flex[_flex.Row, "AM"] = System.Math.Floor(_flex.CDecimal(row["AM_BL"]));                    
                    _flex[_flex.Row, "AM"] = Unit.원화금액(DataDictionaryTypes.PU,  D.GetDecimal(row["AM_TO"]));

                    _flex[_flex.Row, "UM"] = (_flex.CDecimal(row["QT_REQ"]) == 0) ? 0 : Unit.원화단가(DataDictionaryTypes.PU, _flex.CDecimal(row["AM_TO"]) / _flex.CDecimal(row["QT_REQ"])); 

                    _flex[_flex.Row, "VAT"] = 0;
                    _flex[_flex.Row, "RT_CUSTOMS"] = 0;
                    _flex[_flex.Row, "YN_RETURN"] = "N";
                    _header.CurrentRow["YN_RETURN"] = "N";  //헤더에 입력해줘야한다!

                    _flex[_flex.Row, "FG_TPPURCHASE"] = row["FG_TPPURCHASE"].ToString();

                    if (row["FG_TPPURCHASE"].ToString() != string.Empty)
                    {
                        _flex[_flex.Row, "YN_PURCHASE"] = "Y";
                    }
                    else
                    {
                        _flex[_flex.Row, "YN_PURCHASE"] = "N";
                    }

                    _flex[_flex.Row, "FG_POST"] = "";										//발주상태 코드
                    _flex[_flex.Row, "NM_FG_POST"] = "";									//발주상태 명
                    _flex[_flex.Row, "FG_RCV"] = row["CD_QTIOTP"].ToString();				//발주상태 명
                    _flex[_flex.Row, "NM_FG_RCV"] = row["NM_QTIOTP"].ToString();		//입고형태 명
                    _flex[_flex.Row, "FG_TRANS"] = row["FG_LC"].ToString();				//거래구분
                    _flex[_flex.Row, "FG_TAX"] = "23";//row["FG_TAX"].ToString();				//과세구분
                    _flex[_flex.Row, "CD_EXCH"] = row["CD_EXCH"].ToString();				//환종
                    _header.CurrentRow["CD_EXCH"] = row["CD_EXCH"].ToString();
                    //_flex[_flex.Row, "RT_EXCH"] = row["RT_EXCH"].ToString();				//환율
                    _flex[_flex.Row, "RT_EXCH"] = row["RT_EXCH_BL"].ToString();				//환율 BL 환율 적용한다.
                    _flex[_flex.Row, "NO_SERL"] = row["NO_SERL"];
                    _flex[_flex.Row, "NO_IO_MGMT"] = "";
                    _flex[_flex.Row, "NO_IOLINE_MGMT"] = 0;

                    _flex[_flex.Row, "NO_PO"] = row["NO_PO"].ToString();				//관련발주번호	
                    _flex[_flex.Row, "NO_POLINE"] = row["NO_POLINE"].ToString();		//관련발주항번

                    _flex[_flex.Row, "NO_TO"] = row["NO_TO"].ToString();					//통관번호
                    _flex[_flex.Row, "NO_TO_LINE"] = row["NO_LINE"].ToString();				//통관항번

                    _flex[_flex.Row, "NO_LC"] = row["NO_LC"].ToString();
                    _flex[_flex.Row, "NO_LCLINE"] = row["NO_LCLINE"].ToString();

                    _flex[_flex.Row, "CD_SL"] = row["CD_SL"].ToString();	//tb_NM_SL.Tag.ToString();		
                    //20031118
                    _flex[_flex.Row, "NM_SL"] = row["NM_SL"].ToString();

                    _flex[_flex.Row, "FG_TAXP"] = "001";//일괄
                    _flex[_flex.Row, "NO_EMP"] = "";//row["NO_EMP"];					
                    _flex[_flex.Row, "NM_PROJECT"] = row["NM_PROJECT"].ToString();
                    _flex[_flex.Row, "YN_AM"] = _flex[_flex.Row, "YN_PURCHASE"].ToString();
                    _header.CurrentRow["YN_AM"] = _flex[_flex.Row, "YN_PURCHASE"].ToString();              //헤더에 입력해줘야한다!

                    _flex[_flex.Row, "VAT_CLS"] = 0;
                    _flex[_flex.Row, "YN_AUTORCV"] = row["YN_AUTORCV"].ToString();
                    _flex[_flex.Row, "YN_REQ"] = "Y";

                    // 0314 : 발주금액, 원화금액 -->> 의뢰에 반영
                    _flex[_flex.Row, "AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["AM_EX_TO"].ToString()));


                    //_flex[_flex.Row, "AM_REQ"] = System.Ma^th.Floor(_flex.CDecimal(row["AM_BL"].ToString()));
                    _flex[_flex.Row, "AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(row["AM_TO"].ToString()));


                    _flex[_flex.Row, "AM_EXRCV"] = 0;
                    _flex[_flex.Row, "AM_RCV"] = 0;

                    _flex[_flex.Row, "NO_BL"] = row["NO_BL"];
                    _flex[_flex.Row, "NO_BLLINE"] = row["NO_BLLINE"];
                    

                    _flex[_flex.Row, "NM_SYSDEF"] = row["NM_SYSDEF"].ToString();

                    //_flex["AM_TOTAL"] = System.Math.Floor(_flex.CDecimal(_flex[_flex.Row, "AM_REQ"]) + _flex.CDecimal(_flex[_flex.Row, "VAT"])); //총금액
                    _flex["AM_TOTAL"] = Unit.원화금액(DataDictionaryTypes.PU,  _flex.CDecimal(_flex[_flex.Row, "AM_REQ"]) + _flex.CDecimal(_flex[_flex.Row, "VAT"])); //총금액


                    if (row["CD_EXCH"].ToString() != "")
                    {
                        tb_CD_EXCH.Text = row["NM_SYSDEF"].ToString();
                    }

                    if (_flex[_flex.Row, "RT_SPEC"] == System.DBNull.Value)
                    {
                        if (_flex.DataTable.Columns["RT_SPEC"].DataType.FullName == "System.String")
                            _flex[_flex.Row, "RT_SPEC"] = string.Empty;
                        else
                            _flex[_flex.Row, "RT_SPEC"] = 0;

                    }
                    if (_flex[_flex.Row, "DC_RMK"] == System.DBNull.Value)
                        _flex[_flex.Row, "DC_RMK"] = string.Empty;

                    _flex[_flex.Row, "CD_PLANT"] = _header.CurrentRow["CD_PLANT"].ToString();



                    // 2011-06-03, 최승애 , PIMS : M20110519153
                    _flex[_flex.Row, "REV_QT_REQ_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ"]));                // 가입고 적용시 의뢰량 2011-06-03, 최승애 추가
                    _flex[_flex.Row, "REV_AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["AM_EX_TO"].ToString()));   // 가입고 적용시 외화금액 2011-06-03, 최승애 추가
                    _flex[_flex.Row, "REV_AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(row["AM_TO"].ToString()));    // 가입고 적용시 원화금액 2011-06-03, 최승애 추가
                    _flex[_flex.Row,"TP_UM_TAX"] = row["TP_UM_TAX"];


                    _flex["GI_PARTNER"] = row["GI_PARTNER"];
                    _flex["NM_GI_PARTER"] = row["NM_GI_PARTER"];

                    if (Config.MA_ENV.PJT형여부 == "Y")
                    {
                        _flex[_flex.Row, "CD_PJT_ITEM"] = row["CD_PJT_ITEM"];
                        _flex[_flex.Row, "NM_PJT_ITEM"] = row["NM_PJT_ITEM"];
                        _flex[_flex.Row, "PJT_ITEM_STND"] = row["PJT_ITEM_STND"];
                        _flex[_flex.Row, "SEQ_PROJECT"] = D.GetDecimal(row["SEQ_PROJECT"]);
                    }

                    _flex["NO_SERL"] = row["NO_SERL"];

                    if (pdt_Line.Columns.Contains("DT_PLAN"))   //2013.07.30 납기예정일추가
                    {
                        _flex["DT_PLAN"] = row["DT_PLAN"];
                    }
                    _flex["CLS_ITEM"] = row["CLS_ITEM"];
                    NO_PO_MULTI += D.GetString(row["NO_PO"]) + D.GetString(row["NO_POLINE"]) + "|";

                    if (check)
                    {
                        _flex[_flex.Row, "DC_RMK"] = row["DC1"].ToString();
                        _flex[_flex.Row, "DC_RMK2"] = row["DC2"].ToString();
                    }

                    _flex["MAT_ITEM"] = row["MAT_ITEM"];

                }
                // 2010.01.08 추가 거래처  
                _header.CurrentRow["CD_PARTNER"] = pdt_Line.Rows[0]["CD_PARTNER"];
                tb_NM_PARTNER.SetCode(pdt_Line.Rows[0]["CD_PARTNER"].ToString(), pdt_Line.Rows[0]["LN_PARTNER"].ToString());

                _flex.Redraw = true;

                if (m_YN_SU == "100")
                {
                    SET_FLEXD(NO_PO_MULTI);
                }
                //        ControlEnabledDisable(false);

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }

        }

        #endregion

        #region -> Local LC

        private void Local_LC_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (!Cheak_For_btn())
                    return;

                DataTable gdt_ReqDataREQ = null;
                P_PU_REQLC_SUB dlg_PuLCSub = new P_PU_REQLC_SUB(MainFrameInterface, gdt_ReqDataREQ, D.GetString(cbo공장.SelectedValue));//tb_NM_PARTNER.Tag.ToString(), tb_NM_PARTNER.Text);
                dlg_PuLCSub.g_cdPartner = tb_NM_PARTNER.CodeValue.ToString();
                dlg_PuLCSub.g_nmPartner = tb_NM_PARTNER.CodeName;

                if (dlg_PuLCSub.ShowDialog(this) == DialogResult.OK)
                {
                    InserGridtAddLC(dlg_PuLCSub.gdt_return);
                    //ToolBarDeleteButtonEnabled = false;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }


        /// <summary>
        /// LOCAL L/C 적용을 받아왔을때 그리드값 채워주는것
        /// </summary>
        /// <param name="pdt_Line"></param>
        private void InserGridtAddLC(DataTable pdt_Line)
        {
            try
            {
                if (pdt_Line == null || pdt_Line.Rows.Count <= 0)
                    return;

                DataRow row;
                decimal max_no_line = _flex.GetMaxValue("NO_LINE");
                _flex.Redraw = false;
                string NO_PO_MULTI = string.Empty;


                for (int i = 0; i < pdt_Line.Rows.Count; i++)
                {
                    row = pdt_Line.Rows[i];
                    max_no_line++;
                    _flex.Rows.Add();
                    _flex.Row = _flex.Rows.Count - 1;

                    //_flex[_flex.Row, "NO_RCV"] = tb_NoIsuRcv.Text;
                    _flex[_flex.Row, "NO_LINE"] = max_no_line;
                    _flex[_flex.Row, "NO_IOLINE"] = max_no_line;
                    _flex[_flex.Row, "NO_LOT"] = row["NO_LOT"].ToString();
                    _flex[_flex.Row, "CD_ITEM"] = row["CD_ITEM"].ToString();						// 품목코드
                    _flex[_flex.Row, "NM_ITEM"] = row["NM_ITEM"].ToString();						// 품목명
                    _flex[_flex.Row, "STND_ITEM"] = row["STND_ITEM"].ToString();					// 규격
                    if (row["CD_UNIT_MM"].ToString() == null)
                        row["CD_UNIT_MM"] = "";
                    _flex[_flex.Row, "CD_UNIT_MM"] = row["CD_UNIT_MM"].ToString();					// 수배단위
                    _flex[_flex.Row, "UNIT_IM"] = row["UNIT_IM"].ToString();						// 재고단위
                    _flex[_flex.Row, "RATE_EXCHG"] = row["RATE_EXCHG"];	
                    // 단위환산량으로 수배단위, 재고단위에 의해서 결정된다.
                    if (BASIC.GetMAEXC_Menu("P_PU_REQ_REG_1", "PU_A00000007") == "100")
                        _flex["DT_LIMIT"] = row["DT_LIMIT"];					//납기일
                    else
                        _flex["DT_LIMIT"] = row["DT_DELIVERY"];	
                    //_flex[_flex.Row, "QT_REQ_MM"] = row["QT_LC_MM"];								// LC수배수량
                    _flex[_flex.Row, "QT_REQ_MM"] =  Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ_MM"]));								// LC수배수량
                    _flex[_flex.Row, "RT_VAT"] = "0";

                    // 0502(부가세를 위해서)
                    _flex[_flex.Row, "QT_REAL"] = 0;//row["QT_LC_MM"];	
                    // 0502

                    // MA_PITEM.FG_FOQ =  'Y'이면 YN_INSP = 'Y'
                    _flex[_flex.Row, "YN_INSP"] = "N";

                    if (row["FG_IQC"].ToString() != "")
                    {
                        if (row["FG_IQC"].ToString() == "Y")
                        {
                            _flex[_flex.Row, "YN_INSP"] = "Y";
                        }
                    }

                    //_flex[_flex.Row, "QT_REQ"] = row["QT_LC"];
                    //_flex[_flex.Row, "QT_GOOD_INV"] = row["QT_LC"];									// LC개설수량

                    _flex[_flex.Row, "QT_REQ"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ"]));
                    _flex[_flex.Row, "QT_GOOD_INV"] =  Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ"]));									// LC개설수량
                    _flex[_flex.Row, "QT_PASS"] = 0;
                    _flex[_flex.Row, "QT_REJECTION"] = 0;
                    _flex[_flex.Row, "QT_GR"] = 0;
                    _flex[_flex.Row, "UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX_PO"]));								// 발주단가
                    _flex[_flex.Row, "UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal (row["UM_EX"]));									// 단가<<--발주단가						
                    _flex[_flex.Row, "AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["AM_EX"]));
                    _flex[_flex.Row, "CD_PJT"] = row["CD_PJT"].ToString();						// 프로젝트코드
                    _flex[_flex.Row, "CD_PURGRP"] = row["CD_PURGRP"].ToString();
                    _flex[_flex.Row, "NO_PO"] = row["NO_PO"].ToString();
                    _flex[_flex.Row, "NO_POLINE"] = row["NO_LINE_PO"];
                    _flex[_flex.Row, "UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM"]));
                    _flex[_flex.Row, "AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(row["AM"]));
                    _flex[_flex.Row, "VAT"] = 0;
                    _flex[_flex.Row, "QT_GR_MM"] = 0;
                    _flex[_flex.Row, "RT_CUSTOMS"] = 0;
                    _flex[_flex.Row, "YN_RETURN"] = "N";
                    _header.CurrentRow["YN_RETURN"] = "N";  //헤더에 입력해줘야한다!



                    _flex[_flex.Row, "FG_TPPURCHASE"] = row["FG_TPPURCHASE"].ToString();

                    if (row["FG_TPPURCHASE"].ToString() != string.Empty)
                    {
                        _flex[_flex.Row, "YN_PURCHASE"] = "Y";
                    }
                    else
                    {
                        _flex[_flex.Row, "YN_PURCHASE"] = "N";
                    }

                    //	_flex[_flex.Row,"FG_POST"] = "";//row["FG_POST"].ToString();		// 발주상태 코드
                    //	_flex[_flex.Row,"NM_FG_POST"] = "";//row["NM_FG_POST"].ToString();	// 발주상태 명
                    _flex[_flex.Row, "FG_RCV"] = row["CD_QTIOTP"].ToString();				// 발주의 입고형태 코드
                    _flex[_flex.Row, "NM_FG_RCV"] = row["NM_QTIOTP"].ToString();			// 발주의 입고형태 명
                    _flex[_flex.Row, "FG_TRANS"] = row["FG_LC"].ToString();					// LC구분값
                    _flex[_flex.Row, "FG_TAX"] = "23";										// (POH 조인)과세구분
                    _flex[_flex.Row, "CD_EXCH"] = row["CD_EXCH"].ToString();				// 환종
                    _header.CurrentRow["CD_EXCH"] = row["CD_EXCH"].ToString();
                    if (D.GetDecimal(row["RT_EXCH"]) == 0 && D.GetString(row["CD_EXCH"]) == "000")
                    {
                        _flex[_flex.Row, "RT_EXCH"] = 1;			//환율
                    }
                    else
                    {
                        _flex[_flex.Row, "RT_EXCH"] = D.GetDecimal(row["RT_EXCH"]);
                    }
                    

                    _flex[_flex.Row, "NO_IO_MGMT"] = "";
                    _flex[_flex.Row, "NO_IOLINE_MGMT"] = 0;
                    _flex[_flex.Row, "NO_PO_MGMT"] = "";
                    _flex[_flex.Row, "NO_POLINE_MGMT"] = 0;

                    _flex[_flex.Row, "NO_TO"] = "";											// 통관번호
                    _flex[_flex.Row, "NO_TO_LINE"] = 0;										// 통관항번

                    _flex[_flex.Row, "CD_SL"] = row["CD_SL"].ToString();
                    // 20031118
                    _flex[_flex.Row, "NM_SL"] = row["NM_SL"].ToString();

                    _flex[_flex.Row, "NO_LC"] = row["NO_LC"].ToString();
                    _flex[_flex.Row, "NO_LCLINE"] = row["NO_LINE"];
                    _flex[_flex.Row, "FG_TAXP"] = "001";
                    //_flex[_flex.Row, "NO_EMP"] = row["NO_EMP"];
                    _flex[_flex.Row, "NO_EMP"] = D.GetString(tb_NO_EMP.CodeValue);
                    _flex[_flex.Row, "NM_PROJECT"] = row["NM_PJT"].ToString();
                    _flex[_flex.Row, "YN_AM"] = row["YN_AM"].ToString();                    //(POH 발주에서 읽어오기)
                    _header.CurrentRow["YN_AM"] = row["YN_AM"].ToString();              //헤더에 입력해줘야한다!
                    _flex[_flex.Row, "VAT_CLS"] = 0;
                    _flex[_flex.Row, "YN_AUTORCV"] = row["YN_AUTORCV"].ToString();          //(POL의YN_AUTORCV값 )
                    _flex[_flex.Row, "YN_REQ"] = "Y";                                       //row["YN_RCV"].ToString();

                    // 0314 : 발주금액, 원화금액 -->> 의뢰에 반영
                    _flex[_flex.Row, "AM_EXREQ"] =  Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["AM_EX"]));

                    //_flex[_flex.Row, "AM_REQ"] = System.Math.Floor(_flex.CDecimal(row["AM"]));
                    _flex[_flex.Row, "AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, _flex.CDecimal(row["AM"]));

                    _flex[_flex.Row, "AM_EXRCV"] = 0;
                    _flex[_flex.Row, "AM_RCV"] = 0;

                    // 유무환유무
                    //gyn_am = row["YN_AM"].ToString();
                    //gcd_exch = row["CD_EXCH"].ToString();

                    _flex[_flex.Row, "NM_SYSDEF"] = row["NM_SYSDEF"].ToString();
                    _flex[_flex.Row, "NM_KOR"] = row["NM_KOR"].ToString();

                    if (row["CD_EXCH"].ToString() != "")
                    {
                        tb_CD_EXCH.Text = row["NM_SYSDEF"].ToString();
                    }

                    _flex[_flex.Row, "CD_PLANT"] = _header.CurrentRow["CD_PLANT"].ToString();

                    //_flex["AM_TOTAL"] = System.Math.Floor(_flex.CDecimal(row["AM"]) + _flex.CDecimal(_flex[_flex.Row, "VAT"])); //총금액
                    _flex["AM_TOTAL"] = Unit.원화금액(DataDictionaryTypes.PU,  _flex.CDecimal(row["AM"]) + _flex.CDecimal(_flex[_flex.Row, "VAT"])); //총금액


                    _flex["GI_PARTNER"] = row["GI_PARTNER"];
                    _flex["NM_GI_PARTER"] = row["NM_GI_PARTER"];


                    if (Config.MA_ENV.PJT형여부 == "Y")
                    {
                        _flex["CD_PJT_ITEM"] = row["CD_PJT_ITEM"];
                        _flex["NM_PJT_ITEM"] = row["NM_PJT_ITEM"];
                        _flex["PJT_ITEM_STND"] = row["PJT_ITEM_STND"];
                        _flex["SEQ_PROJECT"] = D.GetDecimal(row["SEQ_PROJECT"]);
                    }

                    _flex[_flex.Row, "NO_SERL"] = row["NO_SERL"].ToString();

                    if (pdt_Line.Columns.Contains("DT_PLAN"))   //2013.07.30 납기예정일추가
                    {
                        _flex["DT_PLAN"] = row["DT_PLAN"];
                    }
                    _flex["CLS_ITEM"] = row["CLS_ITEM"];
                    _flex["MAT_ITEM"] = row["MAT_ITEM"];


                    NO_PO_MULTI += D.GetString(row["NO_PO"]) + D.GetString(row["NO_LINE_PO"]) + "|";
                }
                //if (pdt_Line.Rows.Count > 0)
                //    m_btnDel.Enabled = true;

                // 2010.01.08 추가 거래처  
                _header.CurrentRow["CD_PARTNER"] = pdt_Line.Rows[0]["CD_PARTNER"];
                tb_NM_PARTNER.SetCode(pdt_Line.Rows[0]["CD_PARTNER"].ToString(), pdt_Line.Rows[0]["LN_PARTNER"].ToString());

                if (m_YN_SU == "100")
                {
                    SET_FLEXD(NO_PO_MULTI);
                }


                _flex.Redraw = true;

                ControlEnabledDisable(false);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 가입고적용
        private void m_btn_Rev_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Cheak_For_btn())
                    return;


                string CD_PARTNER = tb_NM_PARTNER.CodeValue;
                string NM_PARTNER = tb_NM_PARTNER.CodeName;
                string FG_TRANS = cbo_TRANS.SelectedValue.ToString();
                P_PU_REV_SUB dlg_PuRevSub = new P_PU_REV_SUB(D.GetString(cbo공장.SelectedValue), CD_PARTNER, NM_PARTNER, _flex.DataTable, FG_TRANS, D.GetString(cbo_PROCESS.SelectedValue));

                m_btnTR_TO.Enabled = false;

                if (dlg_PuRevSub.ShowDialog(this) == DialogResult.OK)
                {
                    // 거래구분을 수정할 수 없게
                    cbo_TRANS.Enabled = false;

                    DataTable dt = _flex.DataTable.Clone();

                    foreach (DataColumn col in dt.Columns)
                    {
                        if (col.DataType == typeof(decimal))
                            col.DefaultValue = 0;
                    }
                    //_flex.Binding = dt;
    
                    InserGridtAddREV(dlg_PuRevSub.gdt_return);
                    ToolBarDeleteButtonEnabled = false;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region -> IQC 적용(가입고적용,수입검사적용,점포검수적용(조선호텔베이커리))
        private void InserGridtAddREV(DataTable pdt_Line)
        {
            try
            {
                if (pdt_Line == null || pdt_Line.Rows.Count <= 0)
                    return;
                DataRow row;
                decimal unit_po_fact = 1;
                decimal max_no_line = _flex.GetMaxValue("NO_LINE");
                _flex.Redraw = false;
                bool YN_EXIST = pdt_Line.Columns.Contains("JAN_QT_PASS");
                string NO_PO_MULTI = string.Empty;

                for (int i = 0; i < pdt_Line.Rows.Count; i++)
                {
                    row = pdt_Line.Rows[i];
                    max_no_line++;
                    _flex.Rows.Add();
                    _flex.Row = _flex.Rows.Count - 1;

                    _flex["NO_LINE"] = max_no_line;
                    _flex["NO_IOLINE"] = max_no_line;
                    _flex["CD_ITEM"] = row["CD_ITEM"];						//품목코드
                    _flex["NM_ITEM"] = row["NM_ITEM"];						//품목명
                    _flex["STND_ITEM"] = row["STND_ITEM"];					//규격
                    if (row["UNIT_IM"] == null)
                        row["UNIT_IM"] = "";
                    if (D.GetDecimal(row["UNIT_PO_FACT"]) != 0) unit_po_fact = D.GetDecimal(row["UNIT_PO_FACT"]);
                    _flex["NO_LOT"] = row["NO_LOT"];			

                    _flex["CD_UNIT_MM"] = row["UNIT_PO"];   				//수배단위
                    _flex["UNIT_IM"] = row["UNIT_IM"];						//재고단위
                    _flex["RATE_EXCHG"] = unit_po_fact; //row["QT_FACT"];				//단위환산량으로 수배단위, 재고단위에 의해서 결정된다.	
                    _flex["RT_VAT"] = row["RT_VAT"];

                    if (BASIC.GetMAEXC_Menu("P_PU_REQ_REG_1", "PU_A00000007") == "100")
                        _flex["DT_LIMIT"] = row["DT_LIMIT"];					//납기일
                    else
                        _flex["DT_LIMIT"] = row["DT_REV"];	

                    _flex["QT_REQ_MM"] = Unit.수량(DataDictionaryTypes.PU,   D.GetDecimal(row["QT_REV_VAL"]) / unit_po_fact);				//수배수량
                    _flex["QT_REAL"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REV_VAL"]));

                    _flex["QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.PU,  D.GetDecimal(row["QT_REV_VAL"])); //* unit_po_fact;	
                    _flex["QT_REQ"] =  Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REV_VAL"])); //* unit_po_fact;					//의뢰량

                    _flex["QT_PASS"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_PASS"]));                      //합격수량(재고단위)
                    _flex["QT_REJECTION"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_BAD"]));                  //불량수량(재고단위)

                    // MA_PITEM.FG_FOQ =  'Y'이면 YN_INSP = 'Y'
                    _flex["YN_INSP"] = "N";

                    //if (row["FG_PQC"].ToString() == "B")
                    //{
                    //    _flex["YN_INSP"] = "Y";
                    //}
                    _flex["YN_INSP"] = "N";
                    _flex["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX_PO"]));								//발주단가
                    _flex["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX"]));									//단가<<--발주단가						
                    _flex["AM_EX"] =  Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM_REV"]));
                    _flex["CD_PJT"] = row["CD_PJT"];						//프로젝트코드
                    _flex["CD_PURGRP"] = row["CD_PURGRP"];
                    _flex["NO_PO"] = row["NO_PO"];
                    _flex["NO_POLINE"] = row["NO_POLINE"];
                    _flex["UM"] =  Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM"]));
                    _flex["AM"] = Unit.원화금액(DataDictionaryTypes.PU,   _flex.CDecimal(_flex.CDecimal(row["JAN_AM"])));
                    _flex["VAT"] = Unit.원화금액(DataDictionaryTypes.PU,   _flex.CDecimal(_flex.CDecimal(row["VAT_REV"].ToString())));

                    _flex["AM_TOTAL"] = Unit.원화금액(DataDictionaryTypes.PU,  _flex.CDecimal(_flex["AM"]) + _flex.CDecimal(_flex["VAT"]));
                    _flex["QT_GR_MM"] = 0;
                    _flex["RT_CUSTOMS"] = 0;
                    _flex["YN_RETURN"] = "N";
                    _header.CurrentRow["YN_RETURN"] = row["YN_RETURN"];              //헤더에 입력해줘야한다!
                    _flex["FG_TPPURCHASE"] = row["FG_PURCHASE"];
                    _flex["YN_PURCHASE"] = row["YN_PURCHASE"].ToString(); //"";//(row["FG_PURCHASE"].ToString() != "") ? "Y" : "N";

                    _flex["FG_POST"] = row["FG_POST"];			//발주상태 코드
                    //_flex["NM_FG_POST"] = ""; //row["NM_FG_POST"];		//발주상태 명
                    _flex["FG_RCV"] = row["FG_RCV"];				//발주의 입고형태 코드
                    //_flex["NM_FG_RCV"] = "";//row["NM_QTIOTP"];		//발주의 입고형태 명
                    _flex["FG_TRANS"] = row["FG_TRANS"];			//거래구분
                    _flex["FG_TAX"] = row["FG_TAX"];				//과세구분
                    _flex["CD_EXCH"] = row["CD_EXCH"];			//환종
                    _header.CurrentRow["CD_EXCH"] = row["CD_EXCH"];
                    if(D.GetDecimal(row["RT_EXCH"]) == 0 && D.GetString(row["CD_EXCH"]) == "000") 
                    {
                        _flex["RT_EXCH"] = 1;			//환율
                    }
                    else
                    {
                        _flex["RT_EXCH"] = D.GetDecimal(row["RT_EXCH"]);
                    }

                    if (row["CD_EXCH"].ToString() != "")
                    {
                        tb_CD_EXCH.Text = row["NM_EXCH"].ToString();
                    }

                    _flex["NO_IO_MGMT"] = "";
                    _flex["NO_IOLINE_MGMT"] = 0;
                    _flex["NO_PO_MGMT"] = "";
                    _flex["NO_POLINE_MGMT"] = 0;

                    _flex["NO_TO"] = "";										//통관번호
                    _flex["NO_TO_LINE"] = 0;									//통관항번

                    _flex["CD_SL"] = row["CD_SL"];
                    _flex["NM_SL"] = row["NM_SL"];


                    _flex["FG_TAXP"] = row["FG_TAXP"];
                    //_flex["NO_EMP"] = row["NO_EMP"];
                    _flex["NO_EMP"] = D.GetString(tb_NO_EMP.CodeValue);
                    _flex["NM_PROJECT"] = row["NM_PROJECT"];
                    _flex["YN_AM"] = row["YN_AM"];
                    _header.CurrentRow["YN_AM"] = row["YN_AM"];              //헤더에 입력해줘야한다!
                    _flex["VAT_CLS"] = 0;
                    _flex["YN_AUTORCV"] = row["YN_AUTORCV"];
                    _flex["YN_REQ"] = row["YN_RCV"];

                    _flex["AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM_REV"]));
                    //_flex["AM_REQ"] = System.Math.Floor(_flex.CDecimal(row["JAN_AM"]));
                    _flex["AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM"]));

                    //_flex["AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX_PO"]) * Unit.수량(DataDictionaryTypes.PU,   D.GetDecimal(row["QT_REV_VAL"]) / unit_po_fact  ));
                    //_flex["AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU,  D.GetDecimal(row["UM"]) * Unit.수량(DataDictionaryTypes.PU,   D.GetDecimal(row["QT_REV_VAL"]) / unit_po_fact ));
                     

                    _flex["AM_EXRCV"] = 0;
                    _flex["AM_RCV"] = 0;

                    _flex["NM_SYSDEF"] = "";//row["NM_SYSDEF"];
                    //_flex["NM_KOR"] = "";   //row["NM_KOR"];

                    _flex["NO_REV"]     = row["NO_REV"]; //SetCol("NO_REV", "납품승인번호", false);
                    _flex["NO_REVLINE"] = row["NO_REVLINE"]; //.SetCol("NO_REVLINE", "승인LINE", false);

                    _flex["NM_KOR"]     = row["NM_KOR"];
                    _flex["NM_FG_RCV"]  = row["NM_FG_RCV"];
                    _flex["NM_FG_POST"] = row["NM_FG_POST"];
                    //2010.01.25추가
                    //_flex["CD_PJT"] = row["CD_PJT"];
                    _flex["NM_PROJECT"] = row["NM_PROJECT"];
                    _flex["CD_PLANT"]   = _header.CurrentRow["CD_PLANT"].ToString();

                    //2010.09.29 추가
                    _flex["NO_TO"]      = row["NO_TO"].ToString();					//통관번호
                    _flex["NO_TO_LINE"] = row["NO_TOLINE"];			//통관항번
                    //_flex["NO_LC"]      = row["NO_LC"].ToString();					//LC번호
                    //_flex["NO_LCLINE"]  = row["NO_LCLINE"];		    //LC항번
                    _flex["CD_ZONE"]    = row["CD_ZONE"];		    //저장위치

                    if (D.GetString(cbo_TRANS.SelectedValue) == "003")
                    {
                        _flex["NO_LC"] = row["NO_LC_LOCAL"].ToString();					//LC번호
                        _flex["NO_LCLINE"] = row["NO_LCLINE_LOCAL"];		    //LC항번
                    }
                    else
                    {
                        _flex["NO_LC"] = row["NO_LC"].ToString();					//LC번호
                        _flex["NO_LCLINE"] = row["NO_LCLINE"];		    //LC항번
                    }

                    // 2011-06-03, 최승애 , PIMS : M20110519153
                    _flex["REV_QT_REQ_MM"]  = Unit.수량(DataDictionaryTypes.PU,   UDecimal.Getdivision(D.GetDecimal(row["QT_REV_VAL"]), unit_po_fact));                // 가입고 적용시 의뢰량 2011-06-03, 최승애 추가
                    _flex["REV_AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM_REV"]));                  // 가입고 적용시 외화금액 2011-06-03, 최승애 추가 . AM_REV -> JAN_AM_REV 로 변경 => 수입검사에서 합격수량에서 잔량을 빼오는식때문에
                    _flex["REV_AM_REQ"]     = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flex.CDecimal(row["JAN_AM"])));    // 가입고 적용시 원화금액 2011-06-03, 최승애 추가
 
                    // 2012.03.07 신미란 추가 D20120307029
                   _flex["REV_QT_PASS"]   = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_PASS"]));
                   _flex["REV_QT_REV_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REV_VAL"]));

                    if (Config.MA_ENV.PJT형여부 == "Y")
                    {
                        _flex["CD_PJT_ITEM"] = row["CD_PJT_ITEM"];
                        _flex["NM_PJT_ITEM"] = row["NM_PJT_ITEM"];
                        _flex["PJT_ITEM_STND"] = row["PJT_ITEM_STND"];
                        _flex["NO_WBS"] = row["NO_WBS"];
                        _flex["NO_CBS"] = row["NO_CBS"];
                        _flex["SEQ_PROJECT"] = D.GetDecimal(row["SEQ_PROJECT"]);
                    }

                    //if (row["CD_EXCH"].ToString() != "")
                    //{
                    //    tb_CD_EXCH.Text = row["NM_SYSDEF"].ToString();
                    //}

                    if(Global.MainFrame.ServerKey.Contains("CNP"))
                    _flex["PO_PRICE"] = row["PO_PRICE"]; //추가 20090226 (구매그룹별 단가통제 chk용)
                    
                   if (pdt_Line.Columns.Contains("NM_PURGRP") )
                        _flex["NM_PURGRP"] = row["NM_PURGRP"];

                   if (_flex.DataTable.Columns.Contains("GI_PARTNER"))
                   {
                       _flex["GI_PARTNER"] = row["GI_PARTNER"];
                       _flex["NM_GI_PARTER"] = row["NM_GI_PARTER"];
                   }

                   if (m_YN_special == "Y" && YN_EXIST)
                   {
                       _flex["JAN_QT_PASS"] = row["JAN_QT_PASS"];
                       _flex["JAN_QT_SPECIAL"] = row["JAN_QT_SPECIAL"];
                       _flex["FG_SPECIAL"] = row["OB_PUT"];
                   }

                   if (MainFrameInterface.ServerKeyCommon == "WOORIERP" || MainFrameInterface.ServerKeyCommon == "DZSQL" || MainFrameInterface.ServerKeyCommon == "SQL_")
                   {
                       _flex["NM_USERDEF1"] = row["NM_USERDEF1"];
                       _flex["NM_USERDEF2"] = row["NM_USERDEF2"];
                   }

                   _flex["NO_SERL"] = row["NO_SERL"];

                   if (_flex.DataTable.Columns.Contains("DT_PLAN"))
                   {
                       _flex["DT_PLAN"] = row["DT_PLAN"];
                   }
                   _flex["CLS_ITEM"] = row["CLS_ITEM"];
                   _flex["TP_UM_TAX"] = row["TP_UM_TAX"];

                   NO_PO_MULTI += D.GetString(row["NO_PO"]) + D.GetString(row["NO_POLINE"]) + "|";


                   if (MainFrameInterface.ServerKeyCommon == "ICDERPU" || MainFrameInterface.ServerKeyCommon == "DZSQL" || MainFrameInterface.ServerKeyCommon == "SQL_")
                   {
                       _flex["DATE_USERDEF1"] = row["DATE_USERDEF1"];
                       _flex["DC_RMK2"] = row["NM_USERDEF2"];

                       //_flex["CDSL_USERDEF1"] = row["CDSL_USERDEF1"];
                       //_flex["NMSL_USERDEF1"] = row["NMSL_USERDEF1"];
                   }

                   _flex["MAT_ITEM"] = row["MAT_ITEM"];		
                }

                // 2010.01.08 추가 거래처  
                _header.CurrentRow["CD_PARTNER"] = pdt_Line.Rows[0]["CD_PARTNER"];
                
                tb_NM_PARTNER.SetCode(pdt_Line.Rows[0]["CD_PARTNER"].ToString(), pdt_Line.Rows[0]["LN_PARTNER"].ToString());
                _flex.Redraw = true;
                ///////////////////////////

                if (m_YN_SU == "100")
                {
                    SET_FLEXD(NO_PO_MULTI);
                }

                ControlEnabledDisable(false);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void InsertGrid_REV_BARCODE(DataTable pdt_Line)
        {
            try
            {
                if (pdt_Line == null || pdt_Line.Rows.Count <= 0)
                    return;
                DataRow row;
                decimal unit_po_fact = 1;
                decimal max_no_line = _flex.GetMaxValue("NO_LINE");
                _flex.Redraw = false;
                bool YN_EXIST = pdt_Line.Columns.Contains("JAN_QT_PASS");
                string NO_PO_MULTI = string.Empty;

                if (Global.MainFrame.ServerKeyCommon != "TELCON")
                {
                    tb_DT_RCV.Text = D.GetString(pdt_Line.Rows[0]["DT_REV"]);
                    _header.CurrentRow["DT_REQ"] = tb_DT_RCV.Text;
                }


                for (int i = 0; i < pdt_Line.Rows.Count; i++)
                {
                    row = pdt_Line.Rows[i];
                    max_no_line++;
                    _flex.Rows.Add();
                    _flex.Row = _flex.Rows.Count - 1;

                    _flex["NO_LINE"] = max_no_line;
                    _flex["NO_IOLINE"] = max_no_line;
                    _flex["CD_ITEM"] = row["CD_ITEM"];						//품목코드
                    _flex["NM_ITEM"] = row["NM_ITEM"];						//품목명
                    _flex["STND_ITEM"] = row["STND_ITEM"];					//규격
                    if (row["UNIT_IM"] == null)
                        row["UNIT_IM"] = "";
                    if (D.GetDecimal(row["UNIT_PO_FACT"]) != 0) unit_po_fact = D.GetDecimal(row["UNIT_PO_FACT"]);
                    _flex["NO_LOT"] = row["NO_LOT"];

                    _flex["CD_UNIT_MM"] = row["UNIT_PO"];   				//수배단위
                    _flex["UNIT_IM"] = row["UNIT_IM"];						//재고단위
                    _flex["RATE_EXCHG"] = unit_po_fact; //row["QT_FACT"];				//단위환산량으로 수배단위, 재고단위에 의해서 결정된다.	
                    _flex["RT_VAT"] = row["RT_VAT"];

                    if (BASIC.GetMAEXC_Menu("P_PU_REQ_REG_1", "PU_A00000007") == "100")
                        _flex["DT_LIMIT"] = row["DT_LIMIT"];					//납기일
                    else
                        _flex["DT_LIMIT"] = row["DT_REV"];

                    _flex["QT_REQ_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REV_VAL"]) / unit_po_fact);				//수배수량
                    _flex["QT_REAL"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REV_VAL"]));

                    _flex["QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REV_VAL"])); //* unit_po_fact;	
                    _flex["QT_REQ"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REV_VAL"])); //* unit_po_fact;					//의뢰량

                    _flex["QT_PASS"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_PASS"]));                      //합격수량(재고단위)
                    _flex["QT_REJECTION"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_BAD"]));                  //불량수량(재고단위)

                    // MA_PITEM.FG_FOQ =  'Y'이면 YN_INSP = 'Y'
                    _flex["YN_INSP"] = "N";

                    //if (row["FG_PQC"].ToString() == "B")
                    //{
                    //    _flex["YN_INSP"] = "Y";
                    //}
                    _flex["YN_INSP"] = "N";
                    _flex["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX_PO"]));								//발주단가
                    _flex["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX"]));									//단가<<--발주단가						
                    _flex["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM_REV"]));
                    _flex["CD_PJT"] = row["CD_PJT"];						//프로젝트코드
                    _flex["CD_PURGRP"] = row["CD_PURGRP"];
                    _flex["NO_PO"] = row["NO_PO"];
                    _flex["NO_POLINE"] = row["NO_POLINE"];
                    _flex["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM"]));
                    _flex["AM"] = Unit.원화금액(DataDictionaryTypes.PU, _flex.CDecimal(_flex.CDecimal(row["JAN_AM"])));
                    _flex["VAT"] = Unit.원화금액(DataDictionaryTypes.PU, _flex.CDecimal(_flex.CDecimal(row["VAT_REV"].ToString())));

                    _flex["AM_TOTAL"] = Unit.원화금액(DataDictionaryTypes.PU, _flex.CDecimal(_flex["AM"]) + _flex.CDecimal(_flex["VAT"]));
                    _flex["QT_GR_MM"] = 0;
                    _flex["RT_CUSTOMS"] = 0;
                    _flex["YN_RETURN"] = "N";
                    _header.CurrentRow["YN_RETURN"] = row["YN_RETURN"];              //헤더에 입력해줘야한다!
                    _flex["FG_TPPURCHASE"] = row["FG_PURCHASE"];
                    _flex["YN_PURCHASE"] = row["YN_PURCHASE"].ToString(); //"";//(row["FG_PURCHASE"].ToString() != "") ? "Y" : "N";

                    _flex["FG_POST"] = row["FG_POST"];			//발주상태 코드
                    //_flex["NM_FG_POST"] = ""; //row["NM_FG_POST"];		//발주상태 명
                    _flex["FG_RCV"] = row["FG_RCV"];				//발주의 입고형태 코드
                    //_flex["NM_FG_RCV"] = "";//row["NM_QTIOTP"];		//발주의 입고형태 명
                    _flex["FG_TRANS"] = row["FG_TRANS"];			//거래구분
                    _flex["FG_TAX"] = row["FG_TAX"];				//과세구분
                    _flex["CD_EXCH"] = row["CD_EXCH"];			//환종
                    _header.CurrentRow["CD_EXCH"] = row["CD_EXCH"];
                    if (D.GetDecimal(row["RT_EXCH"]) == 0 && D.GetString(row["CD_EXCH"]) == "000")
                    {
                        _flex["RT_EXCH"] = 1;			//환율
                    }
                    else
                    {
                        _flex["RT_EXCH"] = D.GetDecimal(row["RT_EXCH"]);
                    }
                    _flex["NO_IO_MGMT"] = "";
                    _flex["NO_IOLINE_MGMT"] = 0;
                    _flex["NO_PO_MGMT"] = "";
                    _flex["NO_POLINE_MGMT"] = 0;

                    _flex["NO_TO"] = "";										//통관번호
                    _flex["NO_TO_LINE"] = 0;									//통관항번

                    _flex["CD_SL"] = row["CD_SL"];
                    _flex["NM_SL"] = row["NM_SL"];


                    _flex["FG_TAXP"] = row["FG_TAXP"];
                    //_flex["NO_EMP"] = row["NO_EMP"];
                    _flex["NO_EMP"] = D.GetString(tb_NO_EMP.CodeValue);
                    _flex["NM_PROJECT"] = row["NM_PROJECT"];
                    _flex["YN_AM"] = row["YN_AM"];
                    _header.CurrentRow["YN_AM"] = row["YN_AM"];              //헤더에 입력해줘야한다!
                    _flex["VAT_CLS"] = 0;
                    _flex["YN_AUTORCV"] = row["YN_AUTORCV"];
                    _flex["YN_REQ"] = row["YN_RCV"];

                    _flex["AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM_REV"]));
                    //_flex["AM_REQ"] = System.Math.Floor(_flex.CDecimal(row["JAN_AM"]));
                    _flex["AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM"]));

                    //_flex["AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX_PO"]) * Unit.수량(DataDictionaryTypes.PU,   D.GetDecimal(row["QT_REV_VAL"]) / unit_po_fact  ));
                    //_flex["AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU,  D.GetDecimal(row["UM"]) * Unit.수량(DataDictionaryTypes.PU,   D.GetDecimal(row["QT_REV_VAL"]) / unit_po_fact ));


                    _flex["AM_EXRCV"] = 0;
                    _flex["AM_RCV"] = 0;

                    _flex["NM_SYSDEF"] = "";//row["NM_SYSDEF"];
                    //_flex["NM_KOR"] = "";   //row["NM_KOR"];

                    _flex["NO_REV"] = row["NO_REV"]; //SetCol("NO_REV", "납품승인번호", false);
                    _flex["NO_REVLINE"] = row["NO_REVLINE"]; //.SetCol("NO_REVLINE", "승인LINE", false);

                    _flex["NM_KOR"] = row["NM_KOR"];
                    _flex["NM_FG_RCV"] = row["NM_FG_RCV"];
                    _flex["NM_FG_POST"] = row["NM_FG_POST"];
                    //2010.01.25추가
                    //_flex["CD_PJT"] = row["CD_PJT"];
                    _flex["NM_PROJECT"] = row["NM_PROJECT"];
                    _flex["CD_PLANT"] = row["CD_PLANT"];

                    _header.CurrentRow["CD_PLANT"] = row["CD_PLANT"];
                    cbo공장.SelectedValue = row["CD_PLANT"];

                    //2010.09.29 추가
                    _flex["NO_TO"] = row["NO_TO"].ToString();					//통관번호
                    _flex["NO_TO_LINE"] = row["NO_TOLINE"];			//통관항번
                    //_flex["NO_LC"]      = row["NO_LC"].ToString();					//LC번호
                    //_flex["NO_LCLINE"]  = row["NO_LCLINE"];		    //LC항번
                    _flex["CD_ZONE"] = row["CD_ZONE"];		    //저장위치

                    if (D.GetString(cbo_TRANS.SelectedValue) == "003")
                    {
                        _flex["NO_LC"] = row["NO_LC_LOCAL"].ToString();					//LC번호
                        _flex["NO_LCLINE"] = row["NO_LCLINE_LOCAL"];		    //LC항번
                    }
                    else
                    {
                        _flex["NO_LC"] = row["NO_LC"].ToString();					//LC번호
                        _flex["NO_LCLINE"] = row["NO_LCLINE"];		    //LC항번
                    }

                    // 2011-06-03, 최승애 , PIMS : M20110519153
                    _flex["REV_QT_REQ_MM"] = Unit.수량(DataDictionaryTypes.PU, UDecimal.Getdivision(D.GetDecimal(row["QT_REV_VAL"]), unit_po_fact));                // 가입고 적용시 의뢰량 2011-06-03, 최승애 추가
                    _flex["REV_AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM_REV"]));                  // 가입고 적용시 외화금액 2011-06-03, 최승애 추가 . AM_REV -> JAN_AM_REV 로 변경 => 수입검사에서 합격수량에서 잔량을 빼오는식때문에
                    _flex["REV_AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flex.CDecimal(row["JAN_AM"])));    // 가입고 적용시 원화금액 2011-06-03, 최승애 추가

                    // 2012.03.07 신미란 추가 D20120307029
                    _flex["REV_QT_PASS"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_PASS"]));
                    _flex["REV_QT_REV_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REV_VAL"]));

                    if (Config.MA_ENV.PJT형여부 == "Y")
                    {
                        _flex["CD_PJT_ITEM"] = row["CD_PJT_ITEM"];
                        _flex["NM_PJT_ITEM"] = row["NM_PJT_ITEM"];
                        _flex["PJT_ITEM_STND"] = row["PJT_ITEM_STND"];
                        _flex["NO_WBS"] = row["NO_WBS"];
                        _flex["NO_CBS"] = row["NO_CBS"];
                        _flex["SEQ_PROJECT"] = D.GetDecimal(row["SEQ_PROJECT"]);
                    }

                    //if (row["CD_EXCH"].ToString() != "")
                    //{
                    //    tb_CD_EXCH.Text = row["NM_SYSDEF"].ToString();
                    //}

                    //_flex["PO_PRICE"] = row["PO_PRICE"]; //추가 20090226 (구매그룹별 단가통제 chk용)\

                    if (pdt_Line.Columns.Contains("NM_PURGRP"))
                        _flex["NM_PURGRP"] = row["NM_PURGRP"];

                    if (_flex.DataTable.Columns.Contains("GI_PARTNER"))
                    {
                        _flex["GI_PARTNER"] = row["GI_PARTNER"];
                        _flex["NM_GI_PARTER"] = row["NM_GI_PARTER"];
                    }

                    if (m_YN_special == "Y" && YN_EXIST)
                    {
                        _flex["JAN_QT_PASS"] = row["JAN_QT_PASS"];
                        _flex["JAN_QT_SPECIAL"] = row["JAN_QT_SPECIAL"];
                        _flex["FG_SPECIAL"] = row["OB_PUT"];
                    }

                    if (MainFrameInterface.ServerKeyCommon == "WOORIERP" || MainFrameInterface.ServerKeyCommon == "DZSQL" || MainFrameInterface.ServerKeyCommon == "SQL_")
                    {
                        _flex["NM_USERDEF1"] = row["NM_USERDEF1"];
                        _flex["NM_USERDEF2"] = row["NM_USERDEF2"];
                    }

                    _flex["NO_SERL"] = row["NO_SERL"];
                    _flex["DT_PLAN"] = row["DT_PLAN"];
                    _flex["CLS_ITEM"] = row["CLS_ITEM"];

                    NO_PO_MULTI += D.GetString(row["NO_PO"]) + D.GetString(row["NO_POLINE"]) + "|";
                }

                // 2010.01.08 추가 거래처  
                _header.CurrentRow["CD_PARTNER"] = pdt_Line.Rows[0]["CD_PARTNER"];

                tb_NM_PARTNER.SetCode(pdt_Line.Rows[0]["CD_PARTNER"].ToString(), pdt_Line.Rows[0]["LN_PARTNER"].ToString());
                _flex.Redraw = true;
                ///////////////////////////

                if (m_YN_SU == "100")
                {
                    SET_FLEXD(NO_PO_MULTI);
                }

                ControlEnabledDisable(false);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

       

        private void b_IQC_Apply_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Cheak_For_btn())
                    return;

                string CD_PLANT = D.GetString(cbo공장.SelectedValue);
                string CD_PARTNER = tb_NM_PARTNER.CodeValue;
                string NM_PARTNER = tb_NM_PARTNER.CodeName;
                string FG_TRANS = cbo_TRANS.SelectedValue.ToString();
                P_PU_REQ_IQC_SUB dlg_IQC = new P_PU_REQ_IQC_SUB(CD_PLANT, CD_PARTNER, NM_PARTNER, _flex.DataTable, FG_TRANS, D.GetString(cbo_PROCESS.SelectedValue));

                if (dlg_IQC.ShowDialog(this) == DialogResult.OK)
                {
                    cbo_TRANS.Enabled = false;      // 거래구분을 수정할 수 없게

                    DataTable dt = _flex.DataTable.Clone();

                    foreach (DataColumn col in dt.Columns)
                    {
                        if (col.DataType == typeof(decimal))
                            col.DefaultValue = 0;
                    }
                    InserGridtAddREV(dlg_IQC.gdt_return);
                    ControlEnabledDisable(false);

                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void InserGridtAddREV_CHOSUNHOTELBA(DataTable pdt_Line)
        {
            try
            {
                if (pdt_Line == null || pdt_Line.Rows.Count <= 0)
                    return;
                DataRow row;
                decimal unit_po_fact = 1;
                decimal max_no_line = _flex.GetMaxValue("NO_LINE");
                _flex.Redraw = false;
                bool YN_EXIST = pdt_Line.Columns.Contains("JAN_QT_PASS");

                for (int i = 0; i < pdt_Line.Rows.Count; i++)
                {
                    row = pdt_Line.Rows[i];
                    max_no_line++;
                    _flex.Rows.Add();
                    _flex.Row = _flex.Rows.Count - 1;

                    _flex["NO_LINE"] = max_no_line;
                    _flex["NO_IOLINE"] = max_no_line;
                    _flex["CD_ITEM"] = row["CD_ITEM"];						//품목코드
                    _flex["NM_ITEM"] = row["NM_ITEM"];						//품목명
                    _flex["STND_ITEM"] = row["STND_ITEM"];					//규격
                    if (row["UNIT_IM"] == null)
                        row["UNIT_IM"] = "";
                    if (D.GetDecimal(row["UNIT_PO_FACT"]) != 0) unit_po_fact = D.GetDecimal(row["UNIT_PO_FACT"]);
                    _flex["NO_LOT"] = row["NO_LOT"];

                    _flex["CD_UNIT_MM"] = row["UNIT_PO"];   				//수배단위
                    _flex["UNIT_IM"] = row["UNIT_IM"];						//재고단위
                    _flex["RATE_EXCHG"] = unit_po_fact; //row["QT_FACT"];				//단위환산량으로 수배단위, 재고단위에 의해서 결정된다.	
                    _flex["RT_VAT"] = row["RT_VAT"];
                    _flex["DT_LIMIT"] = row["DT_REV"];					//납기일
                    _flex["QT_REQ_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REV_VAL"]) / unit_po_fact);				//수배수량
                    _flex["QT_REAL"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REV_VAL"]));

                    _flex["QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REV_VAL"])); //* unit_po_fact;	
                    _flex["QT_REQ"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REV_VAL"])); //* unit_po_fact;					//의뢰량

                    _flex["QT_PASS"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_PASS"]));                      //합격수량(재고단위)
                    _flex["QT_REJECTION"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_BAD"]));                  //불량수량(재고단위)

                    // MA_PITEM.FG_FOQ =  'Y'이면 YN_INSP = 'Y'
                    _flex["YN_INSP"] = "N";

                    //if (row["FG_PQC"].ToString() == "B")
                    //{
                    //    _flex["YN_INSP"] = "Y";
                    //}
                    _flex["YN_INSP"] = "N";
                    _flex["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX_PO"]));								//발주단가
                    _flex["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX"]));									//단가<<--발주단가						
                    _flex["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM_REV"]));
                    _flex["CD_PJT"] = row["CD_PJT"];						//프로젝트코드
                    _flex["CD_PURGRP"] = row["CD_PURGRP"];
                    _flex["NO_PO"] = row["NO_PO"];
                    _flex["NO_POLINE"] = row["NO_POLINE"];
                    _flex["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM"]));
                    _flex["AM"] = Unit.원화금액(DataDictionaryTypes.PU, _flex.CDecimal(_flex.CDecimal(row["JAN_AM"])));
                    _flex["VAT"] = Unit.원화금액(DataDictionaryTypes.PU, _flex.CDecimal(_flex.CDecimal(row["VAT_REV"].ToString())));

                    _flex["AM_TOTAL"] = Unit.원화금액(DataDictionaryTypes.PU, _flex.CDecimal(_flex["AM"]) + _flex.CDecimal(_flex["VAT"]));
                    _flex["QT_GR_MM"] = 0;
                    _flex["RT_CUSTOMS"] = 0;
                    _flex["YN_RETURN"] = "N";
                    _header.CurrentRow["YN_RETURN"] = row["YN_RETURN"];              //헤더에 입력해줘야한다!
                    _flex["FG_TPPURCHASE"] = row["FG_PURCHASE"];
                    _flex["YN_PURCHASE"] = row["YN_PURCHASE"].ToString(); //"";//(row["FG_PURCHASE"].ToString() != "") ? "Y" : "N";

                    _flex["FG_POST"] = row["FG_POST"];			//발주상태 코드
                    //_flex["NM_FG_POST"] = ""; //row["NM_FG_POST"];		//발주상태 명
                    _flex["FG_RCV"] = row["FG_RCV"];				//발주의 입고형태 코드
                    //_flex["NM_FG_RCV"] = "";//row["NM_QTIOTP"];		//발주의 입고형태 명
                    _flex["FG_TRANS"] = row["FG_TRANS"];			//거래구분
                    _flex["FG_TAX"] = row["FG_TAX"];				//과세구분
                    _flex["CD_EXCH"] = row["CD_EXCH"];			//환종
                    _header.CurrentRow["CD_EXCH"] = row["CD_EXCH"];
                    if (D.GetDecimal(row["RT_EXCH"]) == 0 && D.GetString(row["CD_EXCH"]) == "000")
                    {
                        _flex["RT_EXCH"] = 1;			//환율
                    }
                    else
                    {
                        _flex["RT_EXCH"] = D.GetDecimal(row["RT_EXCH"]);
                    }
                    _flex["NO_IO_MGMT"] = "";
                    _flex["NO_IOLINE_MGMT"] = 0;
                    _flex["NO_PO_MGMT"] = "";
                    _flex["NO_POLINE_MGMT"] = 0;

                    _flex["NO_TO"] = "";										//통관번호
                    _flex["NO_TO_LINE"] = 0;									//통관항번

                    _flex["CD_SL"] = row["CD_SL"];
                    _flex["NM_SL"] = row["NM_SL"];


                    _flex["FG_TAXP"] = row["FG_TAXP"];
                    //_flex["NO_EMP"] = row["NO_EMP"];
                    _flex["NO_EMP"] = D.GetString(tb_NO_EMP.CodeValue);
                    _flex["NM_PROJECT"] = row["NM_PROJECT"];
                    _flex["YN_AM"] = row["YN_AM"];
                    _header.CurrentRow["YN_AM"] = row["YN_AM"];              //헤더에 입력해줘야한다!
                    _flex["VAT_CLS"] = 0;
                    _flex["YN_AUTORCV"] = row["YN_AUTORCV"];
                    _flex["YN_REQ"] = row["YN_RCV"];

                    _flex["AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM_REV"]));
                    //_flex["AM_REQ"] = System.Math.Floor(_flex.CDecimal(row["JAN_AM"]));
                    _flex["AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM"]));

                    //_flex["AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX_PO"]) * Unit.수량(DataDictionaryTypes.PU,   D.GetDecimal(row["QT_REV_VAL"]) / unit_po_fact  ));
                    //_flex["AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU,  D.GetDecimal(row["UM"]) * Unit.수량(DataDictionaryTypes.PU,   D.GetDecimal(row["QT_REV_VAL"]) / unit_po_fact ));


                    _flex["AM_EXRCV"] = 0;
                    _flex["AM_RCV"] = 0;

                    _flex["NM_SYSDEF"] = "";//row["NM_SYSDEF"];
                    //_flex["NM_KOR"] = "";   //row["NM_KOR"];

                    _flex["NO_REV"] = row["NO_REV"]; //SetCol("NO_REV", "납품승인번호", false);
                    _flex["NO_REVLINE"] = row["NO_REVLINE"]; //.SetCol("NO_REVLINE", "승인LINE", false);

                    _flex["NM_KOR"] = row["NM_KOR"];
                    _flex["NM_FG_RCV"] = row["NM_FG_RCV"];
                    _flex["NM_FG_POST"] = row["NM_FG_POST"];
                    //2010.01.25추가
                    //_flex["CD_PJT"] = row["CD_PJT"];
                    _flex["NM_PROJECT"] = row["NM_PROJECT"];
                    _flex["CD_PLANT"] = _header.CurrentRow["CD_PLANT"].ToString();

                    //2010.09.29 추가
                    _flex["NO_TO"] = row["NO_TO"].ToString();					//통관번호
                    _flex["NO_TO_LINE"] = row["NO_TOLINE"];			//통관항번
                    //_flex["NO_LC"]      = row["NO_LC"].ToString();					//LC번호
                    //_flex["NO_LCLINE"]  = row["NO_LCLINE"];		    //LC항번
                    _flex["CD_ZONE"] = row["CD_ZONE"];		    //저장위치

                    if (D.GetString(cbo_TRANS.SelectedValue) == "003")
                    {
                        _flex["NO_LC"] = row["NO_LC_LOCAL"].ToString();					//LC번호
                        _flex["NO_LCLINE"] = row["NO_LCLINE_LOCAL"];		    //LC항번
                    }
                    else
                    {
                        _flex["NO_LC"] = row["NO_LC"].ToString();					//LC번호
                        _flex["NO_LCLINE"] = row["NO_LCLINE"];		    //LC항번
                    }

                    // 2011-06-03, 최승애 , PIMS : M20110519153
                    _flex["REV_QT_REQ_MM"] = Unit.수량(DataDictionaryTypes.PU, UDecimal.Getdivision(D.GetDecimal(row["QT_REV_VAL"]), unit_po_fact));                // 가입고 적용시 의뢰량 2011-06-03, 최승애 추가
                    _flex["REV_AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM_REV"]));                  // 가입고 적용시 외화금액 2011-06-03, 최승애 추가 . AM_REV -> JAN_AM_REV 로 변경 => 수입검사에서 합격수량에서 잔량을 빼오는식때문에
                    _flex["REV_AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flex.CDecimal(row["JAN_AM"])));    // 가입고 적용시 원화금액 2011-06-03, 최승애 추가

                    // 2012.03.07 신미란 추가 D20120307029
                    _flex["REV_QT_PASS"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_PASS"]));
                    _flex["REV_QT_REV_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REV_VAL"]));

                    if (Config.MA_ENV.PJT형여부 == "Y")
                    {
                        _flex["CD_PJT_ITEM"] = row["CD_PJT_ITEM"];
                        _flex["NM_PJT_ITEM"] = row["NM_PJT_ITEM"];
                        _flex["PJT_ITEM_STND"] = row["PJT_ITEM_STND"];
                        _flex["NO_WBS"] = row["NO_WBS"];
                        _flex["NO_CBS"] = row["NO_CBS"];
                        _flex["SEQ_PROJECT"] = D.GetDecimal(row["SEQ_PROJECT"]);
                    }

                    //if (row["CD_EXCH"].ToString() != "")
                    //{
                    //    tb_CD_EXCH.Text = row["NM_SYSDEF"].ToString();
                    //}

                    //_flex["PO_PRICE"] = row["PO_PRICE"]; //추가 20090226 (구매그룹별 단가통제 chk용)\

                    if (pdt_Line.Columns.Contains("NM_PURGRP"))
                        _flex["NM_PURGRP"] = row["NM_PURGRP"];

                    if (_flex.DataTable.Columns.Contains("GI_PARTNER"))
                    {
                        _flex["GI_PARTNER"] = row["GI_PARTNER"];
                        _flex["NM_GI_PARTER"] = row["NM_GI_PARTNER"];
                    }

                    if (m_YN_special == "Y" && YN_EXIST)
                    {
                        _flex["JAN_QT_PASS"] = row["JAN_QT_PASS"];
                        _flex["JAN_QT_SPECIAL"] = row["JAN_QT_SPECIAL"];
                        _flex["FG_SPECIAL"] = row["OB_PUT"];
                    }
                }

                // 2010.01.08 추가 거래처  
                _header.CurrentRow["CD_PARTNER"] = pdt_Line.Rows[0]["CD_PARTNER"];

                tb_NM_PARTNER.SetCode(pdt_Line.Rows[0]["CD_PARTNER"].ToString(), pdt_Line.Rows[0]["LN_PARTNER"].ToString());
                _flex.Redraw = true;
                ///////////////////////////


                ControlEnabledDisable(false);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region -> MAIL알림
        private void btnMAIL알림_Click(object sender, EventArgs e)
        {
            try
            {
                if (D.GetString(tb_NoIsuRcv.Text) == string.Empty) return;

                SetPrint(false);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region -> 점포검수적용(조선호텔베이커리전용)
        private void m_btn_IqcStore_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Cheak_For_btn())
                    return;


                string CD_PARTNER = tb_NM_PARTNER.CodeValue;
                string NM_PARTNER = tb_NM_PARTNER.CodeName;

                P_PU_Z_CHBA_STOREIQC_SUB dlg_storeiqcSub = new P_PU_Z_CHBA_STOREIQC_SUB(D.GetString(cbo공장.SelectedValue), CD_PARTNER, NM_PARTNER, _flex.DataTable,   "0");

                m_btnTR_TO.Enabled = false;

                if (dlg_storeiqcSub.ShowDialog(this) == DialogResult.OK)
                {
                    // 거래구분을 수정할 수 없게
                    //cbo_TRANS.Enabled = false;

                    DataTable dt = _flex.DataTable.Clone();

                    foreach (DataColumn col in dt.Columns)
                    {
                        if (col.DataType == typeof(decimal))
                            col.DefaultValue = 0;
                    }
                    //_flex.Binding = dt;

                    //InserGridtAddREV() 와같은 메소드이지만..
                    //조선호텔 전용 프로시져 관리가 안되는 이유로 따로 메소드를 뺏습니다..
                    //D20121024079 - 이와같은 에러가 난적 있었음...
                    InserGridtAddREV_CHOSUNHOTELBA(dlg_storeiqcSub.dt_reutrn);
                    ToolBarDeleteButtonEnabled = false;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #endregion

        #region ♣ 그리드 이벤트

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

        #region -> _header_JobModeChanged
        void _header_JobModeChanged(object sender, FreeBindingArgs e)
        {

            if (서버키("CNP"))
            {
                b_OrderApp.Enabled = m_btnTR_TO.Enabled = false;

            }
        }
        #endregion

        #region -> Page_DataChanged

        void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsChanged())
                {
                    this.ToolBarSaveButtonEnabled = true;
                }

                //this.ToolBarDeleteButtonEnabled = false;

            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        #region -> _flex_StartEdit

        private void _flex_StartEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            try
            {
                FlexGrid FLEX = null;

                if (((FlexGrid)sender).Name == "_flex")
                    FLEX = _flex;
                else
                    FLEX = _flexD;

                if (_flex.Cols[e.Col].Name == "S") return;

                if (!추가모드여부)
                {
                    //if (D.GetString(_flex[e.Row,"NO_RCV"]) != string.Empty)
                    if (FLEX.RowState() != DataRowState.Added)
                    {
                        ShowMessage("이미입고되어수정불가합니다");
                        e.Cancel = true;
                    }
                }

                if (D.GetString(_flex["PO_PRICE"]) == "Y" && (_flex.Cols[e.Col].Name == "AM_EXREQ" || _flex.Cols[e.Col].Name == "AM_TOTAL" || _flex.Cols[e.Col].Name == "UM_EX_PO"))
                {
                    ShowMessage("구매단가통제된 구매그룹입니다.");
                    e.Cancel = true;
                }

                //부가세여부 포함 금액(AM_EX) 원화금액(AM), 부가세(VAT) EDIT 불가 
                if (D.GetString(_flex["TP_UM_TAX"]) == "001") 
                {
                    if (_flex.Cols[e.Col].Name == "AM_EXREQ" || _flex.Cols[e.Col].Name == "AM_REQ" || _flex.Cols[e.Col].Name == "VAT")
                        e.Cancel = true;
                }
                else
                {
                    if (_flex.Cols[e.Col].Name == "AM_TOTAL")
                        e.Cancel = true; 
                }
                

                switch (_flex.Cols[e.Col].Name)
                {
                    case "UM_EX_PO"://무역 단가 변경불가~!!!

                        if (_flex["FG_TRANS"].ToString() == "004" || _flex["FG_TRANS"].ToString() == "005")
                            e.Cancel = true;
                        break;
                }

                if (m_YN_special == "Y" && (_flex.Cols[e.Col].Name == "AM_TOTAL" || _flex.Cols[e.Col].Name == "AM_EXREQ" || _flex.Cols[e.Col].Name == "QT_REQ_MM" || _flex.Cols[e.Col].Name == "UM_EX_PO"))
                {
                    e.Cancel = true;
                    return;
                }

                if (bStandard)
                {
                    if ((D.GetDecimal(_flex["UM_WEIGHT"])) > 0)
                    {
                        if ((_flex.Cols[e.Col].Name == "UM_EX_PO") || (_flex.Cols[e.Col].Name == "AM_TOTAL") || (_flex.Cols[e.Col].Name == "UM_EX") || (_flex.Cols[e.Col].Name == "AM_EXREQ") || (_flex.Cols[e.Col].Name == "AM_REQ") )
                        {
                            e.Cancel = true;
                            return;
                        }
                    }
                }

                //if (_flex["NO_TO"].ToString() != string.Empty)
                //{
                //    ShowMessage("통관적용건 수정할 수 없습니다.");
                //    e.Cancel = true;
                //}

                return;

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region -> _Grid_ValidateEdit

        private void _Grid_ValidateEdit(object sender, C1.Win.C1FlexGrid.ValidateEditEventArgs e)
        {
            try
            {

                // 단위환산량(RATE_EXCHG)
                // 의뢰량, 금액, 원화금액 계산식
                // 의뢰량 = 수배수량*단위환산량
                // 금액 = 의뢰량*단가		
  
                string oldValue = ((FlexGrid)sender).GetData(e.Row, e.Col).ToString();
                string newValue = ((FlexGrid)sender).EditData;

                decimal RT_VAT = (D.GetDecimal(_flex["RT_VAT"]) == 0 ? 0 : D.GetDecimal(_flex["RT_VAT"]) * 0.01M);  //부가세율
                Decimal 부가세율 = D.GetDecimal(_flex["RT_VAT"]) == 0 ? 0 : D.GetDecimal(_flex["RT_VAT"]);  //과세율  
                decimal 환율 = D.GetDecimal(_flex[e.Row, "RT_EXCH"]);
                string  과세구분 = D.GetString(_flex[e.Row, "FG_TAX"]);
                bool    부가세포함 = D.GetString(_flex[e.Row, "TP_UM_TAX"]) == "001" ? true : false;
                decimal  ldb_AM_REQ = 0, ldb_AM_EXREQ = 0 ;
                decimal 단위환산 = D.GetDecimal(_flex[e.Row, "RATE_EXCHG"]) ;
                decimal 부가세 = 0;

                if (_flex.AllowEditing)
                {
                    if (_flex.GetData(e.Row, e.Col).ToString() != _flex.EditData)
                    {
                        ////단가통제여부CHK 20090226추가 -> _flex_StartEdit이동
                        //switch (_flex.Cols[e.Col].Name)
                        //{
                        //    //case "UM_EX_PO"://단가 변경시
                        //    //    if (_flex[_flex.Row, "PO_PRICE"].ToString() == "Y")
                        //    //    {
                        //    //        ShowMessage("구매단가통제된 구매그룹입니다.");
                        //    //        if (_flex.Editor != null)
                        //    //            _flex.Editor.Text = oldValue;
                        //    //        _flex["UM_EX_PO"] = oldValue;
                        //    //        return;
                        //    //    }
                        //    //    break;
                        //    case "AM_EXREQ"://금액변경시
                        //    case "AM_TOTAL":
                        //    case "UM_EX_PO"://단가 변경시


                        //        if (_flex[_flex.Row, "PO_PRICE"].ToString() == "Y")
                        //        {
                        //            ShowMessage("구매단가통제된 구매그룹입니다.");
                        //            if (_flex.Editor != null)
                        //                _flex.Editor.Text = oldValue;
                        //            _flex[_flex.Cols[e.Col].Name] = oldValue;
                        //            return;
                        //        }
                        //        break;

                        //}

                        //금액, 부가세, 원화금액 계산식
                        switch (_flex.Cols[e.Col].Name)
                        { 

                            case "QT_REQ_MM"://의뢰량 변경시-->>의뢰량 계산	 
                                //if (_flex.CDecimal(_flex["RATE_EXCHG"].ToString()) == 0)
                                //    _flex["RATE_EXCHG"] = 1;

                                _flex[e.Row, "QT_REQ"] = Unit.수량(DataDictionaryTypes.PU, _flex.CDecimal(newValue) *  단위환산 );	//관라수량(의뢰량)

                                //ldb_AM = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flex.EditData) * D.GetDecimal(_flex[_flex.Row, "UM_EX_PO"]) * D.GetDecimal(_flex[_flex.Row, "RT_EXCH"]));
                               // ldb_VatKr = Duzon.ERPU.MF.Common.Calc.GetVat(ldb_AM, 과세구분, 부가세율, 모듈.PUR, 부가세포함);

                                ldb_AM_REQ = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flex[_flex.Row, "UM_EX_PO"]) * D.GetDecimal(_flex.EditData) * 환율);
                                Calc.GetAmt(D.GetDecimal(_flex.EditData), D.GetDecimal(_flex[_flex.Row, "UM_EX_PO"]), 환율, 과세구분, 부가세율, 모듈.PUR, 부가세포함, out ldb_AM_EXREQ, out ldb_AM_REQ, out 부가세);
                               
                                if (!부가세포함)  //부가세별도
                                {
                                    // 2011-06-03, 최승애 PIMS번호 : M20110519153
                                    if (D.GetDecimal(_flex["REV_QT_REQ_MM"].ToString()) == D.GetDecimal(_flex["QT_REQ_MM"].ToString()))
                                    {
                                        ldb_AM_EXREQ = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(_flex[e.Row, "REV_AM_EXREQ"].ToString()));
                                        ldb_AM_REQ = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flex[e.Row, "REV_AM_REQ"].ToString()));
                                    }  
                                }  
                                _flex[e.Row, "AM_REQ"] = ldb_AM_REQ; //원화금액
                                _flex[e.Row, "AM"] = ldb_AM_REQ;//Unit.원화금액(DataDictionaryTypes.PU, _flex.CDecimal(_flex[_flex.Row, "AM_EXREQ"].ToString()) * _flex.CDecimal(_flex[_flex.Row, "RT_EXCH"].ToString()));
                             
                                _flex[e.Row, "AM_EXREQ"] = ldb_AM_EXREQ;
                                _flex[e.Row, "AM_EX"] = ldb_AM_EXREQ;   //Unit.외화금액(DataDictionaryTypes.PU, _flex.CDecimal(_flex[_flex.Row, "AM_EXREQ"].ToString()));
                              
                                _flex[e.Row, "VAT"] = 부가세; // _flex[e.Row, "VAT"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flex.CDecimal(_flex[_flex.Row, "AM_REQ"].ToString()) * RT_VAT));
                                _flex[e.Row, "AM_TOTAL"] = Calc.합계금액(ldb_AM_REQ, 부가세);  // _flex[e.Row, "AM_TOTAL"] = Unit.원화금액(DataDictionaryTypes.PU,  D.GetDecimal(_flex.CDecimal(_flex[_flex.Row, "AM_REQ"].ToString())) + Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flex.CDecimal(_flex[_flex.Row, "VAT"].ToString())))); //총합계
                                _flex[e.Row, "QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.PU , D.GetDecimal(_flex[e.Row, "QT_REQ"]));

                                calcAM(e.Row, 0, D.GetDecimal(newValue) );

                                // 0611
                                // 금액, 원화금액, 부가세 계산 함수
                                if (m_YN_SU == "100")
                                {
                                    DataRow[] drs = _flexD.DataTable.Select("NO_PO = '" + D.GetString(_flex["NO_PO"]) + "' AND NO_POLINE = '" + D.GetString(_flex["NO_POLINE"]) + "'", "", DataViewRowState.CurrentRows);

                                    if (drs == null || drs.Length == 0) return;

                                    foreach (DataRow dr in drs)
                                    {
                                        dr["QT_NEED"] = D.GetDecimal(dr["QT_NEED_UNIT"]) * D.GetDecimal(newValue);
                                    }
                                }


                                break;

                            case "UM_EX_PO"://단가 변경시  
                                //_flex[e.Row, "QT_REQ"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(_flex[_flex.Row, "QT_REQ_MM"]) *단위환산 );	//관라수량(의뢰량)
                                decimal 수량 = D.GetDecimal(_flex[_flex.Row, "QT_REQ_MM"]);
                              
                                if (수량 == 0)   return;

                                ldb_AM_REQ = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flex.EditData) * 수량 * 환율); 
                                Calc.GetAmt(수량, D.GetDecimal(_flex.EditData), 환율, 과세구분, 부가세율, 모듈.PUR, 부가세포함, out ldb_AM_EXREQ, out ldb_AM_REQ, out 부가세);
                                 
                                //if (Global.MainFrame.ServerKeyCommon == "ETANG") //에땅
                                //{
                                //    decimal d_um_ex_po = D.GetDecimal(newValue);
                                //    _flex[e.Row, "UM_EX_PO"] = UDecimal.Getdivision(d_um_ex_po, (1 + RT_VAT));
                                //    _flex[e.Row, "AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, UDecimal.Getdivision(D.GetDecimal(_flex["QT_REQ_MM"]) * d_um_ex_po, (1 + RT_VAT)));
                                //    _flex[e.Row, "AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, _flex.CDecimal(_flex["AM_EXREQ"].ToString()) * D.GetDecimal(_flex["RT_EXCH"]));
                                //    _flex[e.Row, "AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flex["AM_EXREQ"]) * D.GetDecimal(_flex["RT_EXCH"]));
                                //    _flex[e.Row, "AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(_flex["AM_EXREQ"]));
                                //    _flex[e.Row, "AM_TOTAL"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flex["QT_REQ_MM"]) * d_um_ex_po * D.GetDecimal(_flex["RT_EXCH"])); //총합계
                                //    _flex[e.Row, "VAT"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flex["AM_TOTAL"]) - D.GetDecimal(_flex["AM_REQ"]));
   
                                //    _flex[e.Row, "UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, UDecimal.Getdivision(D.GetDecimal(_flex["AM_EXREQ"]) , D.GetDecimal(_flex["QT_REQ"])));
                                //    _flex[e.Row, "UM"] = Unit.원화단가(DataDictionaryTypes.PU, UDecimal.Getdivision(D.GetDecimal(_flex["AM_REQ"]) , D.GetDecimal(_flex["QT_REQ"])));

                                //}
                                //else
                                //{ 
                                    _flex[e.Row, "AM_EXREQ"] = ldb_AM_EXREQ;
                                    _flex[e.Row, "AM_EX"]    = ldb_AM_EXREQ;     //외화금액

                                    _flex[e.Row, "AM_REQ"] = ldb_AM_REQ; //원화금액
                                    _flex[e.Row, "AM"]     = ldb_AM_REQ;   
                                  
                                    _flex[e.Row, "VAT"] = 부가세;
                                    _flex[e.Row, "AM_TOTAL"] = Calc.합계금액(ldb_AM_REQ, 부가세);
                                    _flex[e.Row, "UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(_flex.EditData) / 단위환산);
                                    _flex[e.Row, "UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(_flex.EditData) / 단위환산 * 환율); 
                                    
                                    // _flex[e.Row, "AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, _flex.CDecimal(newValue) * _flex.CDecimal(_flex[_flex.Row, "QT_REQ_MM"].ToString()));
                                    // _flex[e.Row, "AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, _flex.CDecimal(_flex[_flex.Row, "AM_EXREQ"].ToString()) * _flex.CDecimal(_flex[_flex.Row, "RT_EXCH"].ToString()));
                                    // _flex[e.Row, "AM"] = Unit.원화금액(DataDictionaryTypes.PU, _flex.CDecimal(_flex[_flex.Row, "AM_EXREQ"].ToString()) * _flex.CDecimal(_flex[_flex.Row, "RT_EXCH"].ToString()));
                                    //  _flex[e.Row, "AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, _flex.CDecimal(_flex[_flex.Row, "AM_EXREQ"].ToString()));
                                    //  _flex[e.Row, "VAT"] = Unit.원화금액(DataDictionaryTypes.PU, _flex.CDecimal(_flex[_flex.Row, "AM_REQ"].ToString()) * RT_VAT);
                                    //  _flex[e.Row, "AM_TOTAL"] = Unit.원화금액(DataDictionaryTypes.PU, _flex.CDecimal(_flex[_flex.Row, "AM_REQ"].ToString()) + Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flex.CDecimal(_flex[_flex.Row, "VAT"].ToString())))); //총합계

                                    //_flex[e.Row, "UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, _flex.CDecimal(_flex[_flex.Row, "AM_EXREQ"].ToString()) / _flex.CDecimal(_flex[_flex.Row, "QT_REQ"].ToString()));
                                    //_flex[e.Row, "UM"] = Unit.원화단가(DataDictionaryTypes.PU, _flex.CDecimal(_flex[_flex.Row, "AM_REQ"].ToString()) / _flex.CDecimal(_flex[_flex.Row, "QT_REQ"].ToString()));

                                //}
                                break; 


                            case "AM_EXREQ"://금액변경시 -->금액, 원화단가, 원화금액 계산

                                /*  단가일 경우 소수점 이하까지 보여줘야 함으로 절사 (Floor)는 주석처리함.. UMEX_PO, UM_EX, UM 2007.09.03*/
                                //_flex[e.Row, "AM_REQ"] = System.Math.Floor(_flex.CDecimal(newValue) * _flex.CDecimal(_flex[_flex.Row, "RT_EXCH"].ToString()));
                                _flex[e.Row, "AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU,  _flex.CDecimal(newValue) * _flex.CDecimal(_flex[_flex.Row, "RT_EXCH"].ToString()));
                                 //_flex[e.Row, "AM"] = System.Math.Floor(_flex.CDecimal(_flex[_flex.Row, "AM_EXREQ"].ToString()) * _flex.CDecimal(_flex[_flex.Row, "RT_EXCH"].ToString()));
                                _flex[e.Row, "AM"] = Unit.원화금액(DataDictionaryTypes.PU,  _flex.CDecimal(_flex[_flex.Row, "AM_EXREQ"].ToString()) * _flex.CDecimal(_flex[_flex.Row, "RT_EXCH"].ToString()));
                                
                                _flex[e.Row, "AM_EX"] =  Unit.외화금액(DataDictionaryTypes.PU,  _flex.CDecimal(_flex[_flex.Row, "AM_EXREQ"].ToString())); // 절대 절사 안됨..

                                //_flex[e.Row, "VAT"] = System.Math.Floor(_flex.CDecimal(_flex[_flex.Row, "AM_REQ"].ToString()) * RT_VAT);
                                _flex[e.Row, "VAT"] = Unit.원화금액(DataDictionaryTypes.PU,  _flex.CDecimal(_flex[_flex.Row, "AM_REQ"].ToString()) * RT_VAT);


                                //_flex[e.Row, "AM_TOTAL"] = System.Math.Floor(_flex.CDecimal(_flex[_flex.Row, "AM_REQ"].ToString()) + System.Math.Floor(_flex.CDecimal(_flex[_flex.Row, "VAT"].ToString()))); //총합계
                                _flex[e.Row, "AM_TOTAL"] = Unit.원화금액(DataDictionaryTypes.PU,  _flex.CDecimal(_flex[_flex.Row, "AM_REQ"].ToString()) + Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flex.CDecimal(_flex[_flex.Row, "VAT"].ToString())))); //총합계

                                _flex[e.Row, "UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU,  _flex.CDecimal(_flex[_flex.Row, "AM_EXREQ"].ToString()) / _flex.CDecimal(_flex[_flex.Row, "QT_REQ_MM"].ToString()));

                                _flex[e.Row, "UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU,  _flex.CDecimal(_flex[_flex.Row, "AM_EXREQ"].ToString()) / _flex.CDecimal(_flex[_flex.Row, "QT_REQ"].ToString()));

                                _flex[e.Row, "UM"] = Unit.원화단가(DataDictionaryTypes.PU,   _flex.CDecimal(_flex[_flex.Row, "AM_REQ"].ToString()) / _flex.CDecimal(_flex[_flex.Row, "QT_REQ"].ToString()));

                                break;

                            case "QT_REQ":
                                if (_flex.CDouble(newValue) != _flex.CDouble(_flex[e.Row, "QT_REQ_MM"]))
                                {
                                    if (Global.MainFrame.ShowMessage("의뢰량과 관리수량이 다릅니다. 계속 입력하시겠습니까?", "QY2") != DialogResult.Yes)
                                    {
                                        ((FlexGrid)sender)["QT_REQ"] =   ((FlexGrid)sender).GetData(e.Row, e.Col);
                                        return;
                                    }
                                }

                                calcAM(e.Row, 0, D.GetDecimal(newValue));

                                if (m_YN_SU == "100")
                                {
                                    DataRow[] drs = _flexD.DataTable.Select("NO_PO = '" + D.GetString(_flex["NO_PO"]) + "' AND NO_POLINE = '" + D.GetString(_flex["NO_POLINE"]) + "'", "", DataViewRowState.CurrentRows);

                                    if (drs == null || drs.Length == 0) return;

                                    foreach (DataRow dr in drs)
                                    {
                                        dr["QT_NEED"] = D.GetDecimal(dr["QT_NEED_UNIT"]) * D.GetDecimal(newValue);
                                    }
                                }

                                break;
                            case "AM_REQ":
                                //_flex[e.Row, "VAT"] = System.Math.Floor(_flex.CDecimal(newValue) * RT_VAT);
                                _flex[e.Row, "VAT"] = Unit.원화금액(DataDictionaryTypes.PU, _flex.CDecimal(newValue) * RT_VAT);
                                break;

                            case "AM_TOTAL":
                                if (D.GetDecimal(_flex[e.Row, "QT_REQ_MM"]) == 0) return;
                                decimal 단가 = D.GetDecimal(_flex[e.Row, "UM_EX_PO"]);   

                                if (부가세포함)  //부가세포함
                                {   
                                    ldb_AM_REQ = D.GetDecimal(_flex.EditData); //총금액 
                                    if (의제매입여부(과세구분) && s_vat_fictitious == "100")
                                    {
                                        부가세 = Unit.원화금액(DataDictionaryTypes.PU, (ldb_AM_REQ * RT_VAT));
                                    }
                                    else
                                    {
                                        if (Global.MainFrame.LoginInfo.CompanyLanguage != Language.KR)
                                            부가세 = Unit.원화금액(DataDictionaryTypes.PU, ldb_AM_REQ / (1 + RT_VAT) * RT_VAT); //부가세 
                                        else
                                            부가세 = Decimal.Round(ldb_AM_REQ / (1 + RT_VAT) * RT_VAT, MidpointRounding.AwayFromZero); //부가세 
                                    }

                                    _flex[e.Row, "AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, ldb_AM_REQ - 부가세);  //원화
                                    _flex[e.Row, "AM_EXREQ"] = Unit.원화금액(DataDictionaryTypes.PU, (ldb_AM_REQ - 부가세) / 환율); //외화
                                    _flex[e.Row, "VAT"] = 부가세;

                                    _flex[e.Row, "UM_EX_PO"] = 단가;
                                    _flex[e.Row, "UM_EX"] = 단가 / 단위환산;
                                    _flex[e.Row, "UM"] =  ( 단가 / 단위환산) * 환율;  
                                }
                             break;

                            case "UM_WEIGHT":
                             calcAM(e.Row, D.GetDecimal(_flex[e.Row, "TOT_WEIGHT"]), 0);
                             break;
                            case "TOT_WEIGHT":
                             calcAM(e.Row, D.GetDecimal(newValue), 0);
                             break;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> _Grid_BeforeCodeHelp

        private void _Grid_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                FlexGrid FLEX = null;

                if (((FlexGrid)sender).Name == "_flex")
                    FLEX = _flex;
                else
                    FLEX = _flexD;

                switch (FLEX.Cols[e.Col].Name)
                {
                    case "NM_SL":
                        e.Parameter.P09_CD_PLANT = D.GetString(cbo공장.SelectedValue);
                        break;
                    case "CD_SL":
                    //case "CDSL_USERDEF1":
                        e.Parameter.P09_CD_PLANT = D.GetString(cbo공장.SelectedValue);
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        #region -> _flex_AfterRowChange
        void _flex_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (m_YN_SU == "100")
                {
                    string filter = "NO_PO = '" + D.GetString(_flex["NO_PO"]) + "' AND NO_POLINE = '" + D.GetString(_flex["NO_POLINE"]) + "' ";
                    _flexD.RowFilter = filter;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #endregion

        #region ♣ 기타 이벤트

        #region -> txt바코드_KeyPress
        void txt바코드_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (_flex.HasNormalRow)
                    return;

                if (!chk_barcode_use.Checked)
                {
                    ShowMessage("바코드사용여부를 체크 해주시기 바랍니다.");
                    return;
                }

                if (D.GetString(txt바코드.Text) != string.Empty && e.KeyChar == (char)Keys.Enter)
                {

                    if (!Cheak_For_btn())
                    {
                        txt바코드.Text = string.Empty;
                        return;
                    }

                    //if (!Cheak_For_btn())
                    //    return;
                    if (Global.MainFrame.ServerKeyCommon == "TELCON")
                    {
                        string strFgqc = _biz.strTelcon(txt바코드.Text);
                        DataTable dt = null;

                        object[] obj = new object[]
                             {
                               Global.MainFrame.LoginInfo.CompanyCode,
                               cbo_TRANS.SelectedValue,
                               cbo_PROCESS.SelectedValue,
                               txt바코드.Text
                             };

                        if (strFgqc == "NOT")
                        {
                            ShowMessage("해당가입고번호가 없거나 거래구분, L/C기준이 잘못지정되었습니다." + txt바코드.Text);
                            txt바코드.Text = "";
                            return;
                        }
                        else if (strFgqc == "Y") //수입검사
                        {
                            dt = _biz.search_barcode_iqc(obj);
                        }
                        else
                        {
                            dt = _biz.search_barcode_rev(obj);
                        }

                        if (dt == null || dt.Rows.Count == 0)
                        {
                            ShowMessage("해당가입고번호가 없거나 거래구분, L/C기준이 잘못지정되었습니다." + txt바코드.Text);
                            txt바코드.Text = "";
                            return;
                        }

                        InsertGrid_REV_BARCODE(dt);
                    }
                    else
                    {
                        string[] str_barcode = txt바코드.Text.Split('+');

                        if (str_barcode.Length != 2) return;

                        object[] obj = new object[]
                         {
                           Global.MainFrame.LoginInfo.CompanyCode,
                           cbo_TRANS.SelectedValue,
                           cbo_PROCESS.SelectedValue,
                           D.GetString(str_barcode[1])
                         };
                        DataTable dt = _biz.search_barcode_rev(obj);

                        if (dt == null || dt.Rows.Count == 0)
                        {
                            ShowMessage("해당가입고번호가 없거나 거래구분, L/C기준이 잘못지정되었습니다.");
                            return;
                        }

                        InsertGrid_REV_BARCODE(dt);
                    }

                    m_btnTR_TO.Enabled = false;


                    // 거래구분을 수정할 수 없게
                    cbo_TRANS.Enabled = false;

                    ToolBarDeleteButtonEnabled = false;

                    if (chk_barcode_use.Checked == true)
                        Settings1.Default.chk_barcode_use = "Y";
                    else
                        Settings1.Default.chk_barcode_use = "N";
                    Settings1.Default.Save();

                    txt바코드.Text = ""; ///다시돌아가는것을 막기위해
                }
                else
                    return;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }



        #endregion

        #region -> 라인 삭제 버튼



        #endregion

        #region -> 거래구분


        #endregion

        #region -> 선입출


        #endregion

        #region -> 날짜 에 관련된 함수 및 이벤트

        #region -> OnControl_Validated


        #endregion

        #endregion

        #region -> LOT 입력 버튼 이벤트


        #endregion

        #region -> OnBpCodeTextBox_QueryBefore
        private void OnBpCodeTextBox_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            switch (e.HelpID)
            {
                case Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB:
                    if (D.GetString(this.cbo공장.SelectedValue) == "")
                    {
                        this.ShowMessage("PU_M000070");  //공장을 먼저 선택하세요!
                        cbo공장.Focus();
                        e.QueryCancel = true;
                        return;
                    }
                    e.HelpParam.P09_CD_PLANT = D.GetString(cbo공장.SelectedValue);
                    break;

            }


        }
        #endregion

        #region -> tb_NO_EMP_QueryAfter
        private void tb_NO_EMP_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                 switch (((BpCodeTextBox)sender).Name)
                {
                    case "tb_NO_EMP":
                        _header.CurrentRow["CD_DEPT"] = e.HelpReturn.Rows[0]["CD_DEPT"];
                        break;
                }
            
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region -> cbo공장_SelectionChangeCommitted
        void cbo공장_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                bp_CD_SL.CodeValue = "";
                bp_CD_SL.CodeName = "";
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
        {
            try
            {
                Settings1.Default.cd_sl_apply = bp_CD_SL.CodeValue;
                Settings1.Default.nm_sl_apply = bp_CD_SL.CodeName;
                Settings1.Default.Save();
            }
            catch (Exception Ex)
            {
                this.MsgEnd(Ex);
            }

            return true;
        }

        #endregion

        #region ♣ 기타 메소드

        #region -> 헤드값 체크

        private bool FieldCheck()
        {
            try
            {
                //공장명
                if (D.GetString(cbo공장.SelectedValue) == "")
                {
                    //필수입력사항이 누락되었습니다.
                    ShowMessage(공통메세지._은는필수입력항목입니다, lb_CdPlant.Text);
                    cbo공장.Focus();
                    return false;
                }

                //거래처명
                if (tb_NM_PARTNER.CodeValue.Trim() == "")
                {
                    //필수입력사항이 누락되었습니다.
                    ShowMessage(공통메세지._은는필수입력항목입니다, lbtn_NM_PARTNER.Text);
                    tb_NM_PARTNER.Focus();
                    return false;
                }

                //의뢰일자
                if (tb_DT_RCV.Text.Trim() == "")
                {
                    //필수입력사항이 누락되었습니다.
                    ShowMessage(공통메세지._은는필수입력항목입니다, lb_DtReq.Text);
                    tb_DT_RCV.Focus();
                    return false;
                }

                //담당자
                if (tb_NO_EMP.CodeValue.Trim() == "")
                {
                    //필수입력사항이 누락되었습니다.
                    ShowMessage(공통메세지._은는필수입력항목입니다, lbtn_NO_EMP.Text);
                    tb_NO_EMP.Focus();
                    return false;
                }

                //거래구분
                if (cbo_TRANS.SelectedValue.ToString() == "" || cbo_TRANS.SelectedIndex.ToString() == "")
                {
                    //필수입력사항이 누락되었습니다.
                    ShowMessage(공통메세지._은는필수입력항목입니다, lb_trans.Text);
                    cbo_TRANS.Focus();
                    return false;
                }

                //거래구분
                if (cb_Yn_AutoRcv.SelectedValue.ToString() == "Y")
                {
                    DataRow[] ldt_row = _flex.DataTable.Select("YN_INSP = 'Y'");
                    if (ldt_row.Length > 0)
                    {
                        ShowMessage("PU_M000126");  //품질검사 품목이 존재 하므로 자동 입고를 사용할수 없습니다.
                        return false;
                    }

                }

                this.ToolBarSaveButtonEnabled = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }


        #endregion

        #region -> 필수항목 체크(모든 적용버튼 클릭시)

        private bool Cheak_For_btn()
        {
            //공장명
            if (cbo공장.SelectedValue == null || D.GetString(cbo공장.SelectedValue) == "")
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, lb_CdPlant.Text);
                return false;
            }

            ////거래처명
            //if (tb_NM_PARTNER.CodeValue.ToString() == "")
            //{
            //    ShowMessage(공통메세지._은는필수입력항목입니다, lbtn_NM_PARTNER.Text);
            //    return false;
            //}
            //담당자
            if (tb_NO_EMP.CodeValue.ToString() == "")
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, lbtn_NO_EMP.Text);
                return false;
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

        #region -> 거래구분

        private void cbo_TRANS_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            if (cbo_TRANS.SelectedValue.ToString() == "001")//국내
            {
                cbo_PROCESS.Enabled = false;
                b_OrderApp.Enabled = true;
                m_btnTR_TO.Enabled = false;
                m_btnLC.Enabled = false;

            }
            if (cbo_TRANS.SelectedValue.ToString() == "002")//구매승인서
            {
                cbo_PROCESS.Enabled = false;
                b_OrderApp.Enabled = true;
                m_btnTR_TO.Enabled = false;
                m_btnLC.Enabled = false;

                cbo_PROCESS_SelectionChangeCommitted(sender, e);
            }//Local L/C, Master L/C, Master 기타

            if (cbo_TRANS.SelectedValue.ToString() == "003")
            {
                cbo_PROCESS.Enabled = true;
                if (cbo_PROCESS.SelectedValue.ToString() == "001")
                {
                    b_OrderApp.Enabled = false;
                    m_btnTR_TO.Enabled = false;
                    m_btnLC.Enabled = true;
                }
                if (cbo_PROCESS.SelectedValue.ToString() == "002")
                {
                    b_OrderApp.Enabled = true;
                    m_btnTR_TO.Enabled = false;
                    m_btnLC.Enabled = false;
                }
                cbo_PROCESS_SelectionChangeCommitted(sender, e);
            }
            if (cbo_TRANS.SelectedValue.ToString() == "004" ||
                cbo_TRANS.SelectedValue.ToString() == "005")
            {
                cbo_PROCESS.Enabled = true;
                if (cbo_PROCESS.SelectedValue.ToString() == "001")
                {
                    b_OrderApp.Enabled = false;
                    m_btnLC.Enabled = false;
                    m_btnTR_TO.Enabled = true;
                }
                cbo_PROCESS_SelectionChangeCommitted(sender, e);
            }

            if (서버키("CNP") && m_sEnv_IQC == "Y" )
            {
                b_OrderApp.Enabled = m_btnTR_TO.Enabled = false;

            }
        }

        #endregion

        #region -> 선입출

        private void cbo_PROCESS_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            if (cbo_PROCESS.SelectedValue.ToString() == "001")
            {
                if (cbo_TRANS.SelectedValue.ToString() == "002")
                {
                    b_OrderApp.Enabled = true;
                    m_btnTR_TO.Enabled = false;
                }
                if (cbo_TRANS.SelectedValue.ToString() == "003")
                {
                    b_OrderApp.Enabled = false;
                    m_btnTR_TO.Enabled = false;
                    m_btnLC.Enabled = true;
                }
                if (cbo_TRANS.SelectedValue.ToString() == "004" ||
                    cbo_TRANS.SelectedValue.ToString() == "005")
                {
                    b_OrderApp.Enabled = false;
                    m_btnTR_TO.Enabled = true;
                    m_btnLC.Enabled = false;
                }
            }
            if (cbo_PROCESS.SelectedValue.ToString() == "002")
            {
                if (cbo_TRANS.SelectedValue.ToString() == "002")
                {
                    b_OrderApp.Enabled = true;
                    m_btnTR_TO.Enabled = false;
                }
                if (cbo_TRANS.SelectedValue.ToString() == "003")
                {
                    b_OrderApp.Enabled = true;
                    m_btnTR_TO.Enabled = false;
                    m_btnLC.Enabled = false;
                }
            }
        }

        #endregion

        #region -> 서버키 
        private bool 서버키(string pstr_서버키 )
        {
            if (Global.MainFrame.ServerKeyCommon == pstr_서버키 )
                return true;

            

            return false;
        }

        private bool 서버키_TEST포함(string pstr_서버키)
        {
            if (Global.MainFrame.ServerKeyCommon == pstr_서버키|| Global.MainFrame.ServerKeyCommon == "DZSQL" || Global.MainFrame.ServerKeyCommon == "SQL_" || Global.MainFrame.ServerKeyCommon == "SQL_108")
                return true;



            return false;
        }
        #endregion

        #region -> 중량계산
        

        void calcAM(int row, decimal TOT_WEIGHT, decimal QT_REQ)
        {
            if (bStandard)
            {

                if (Global.MainFrame.ServerKey == "SINJINSM" || Global.MainFrame.ServerKey == "SQL_108" || Global.MainFrame.ServerKey == "DZSQL")
                {
                    
                    if (D.GetDecimal(_flex[row, "UM_WEIGHT"]) != 0)
                    {
                        if (TOT_WEIGHT == 0)
                        {
                            _flex[row, "TOT_WEIGHT"] = Math.Round(D.GetDecimal(_flex[row, "WEIGHT"]) * QT_REQ, 1);
                            TOT_WEIGHT = D.GetDecimal(_flex[row, "TOT_WEIGHT"]);
                        }

                        // 단위 환산량
                        decimal ldb_rateExchg = D.GetDecimal(_flex[row, "RATE_EXCHG"]);
                        
                        _flex[row, "AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, Math.Round(TOT_WEIGHT * D.GetDecimal(_flex[row, "UM_WEIGHT"])));
                        _flex[row, "AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, Math.Round(TOT_WEIGHT * D.GetDecimal(_flex[row, "UM_WEIGHT"])));


                        if (D.GetDecimal(_flex[row, "QT_REQ_MM"]) != 0)
                            _flex[row, "UM_EX"] = UDecimal.Getdivision(D.GetDecimal(_flex[row, "AM_EXREQ"]) / D.GetDecimal(_flex[row, "QT_REQ_MM"]), ldb_rateExchg);
                        else
                            _flex[row, "UM_EX"] = 0;

                        _flex[row, "AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flex[row, "AM_EXREQ"]) * D.GetDecimal(_flex[row,"RT_EXCH"]));
                        _flex[row, "AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flex[row, "AM_EXREQ"]) * D.GetDecimal(_flex[row,"RT_EXCH"]));

                        _flex[row, "UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, _flex.CDecimal(_flex[row, "UM_EX"]) * ldb_rateExchg);


                        decimal RT_VAT = (D.GetDecimal(_flex["RT_VAT"]) == 0 ? 0 : D.GetDecimal(_flex["RT_VAT"]));  //부가세율
                        bool 부가세포함 = D.GetString(_flex[row, "TP_UM_TAX"]) == "001" ? true : false;
                        decimal ldb_AM_REQ = 0, ldb_AM_EXREQ = 0, 부가세 =0;
                      
                        Calc.GetAmt(D.GetDecimal(_flex[row, "QT_REQ_MM"]), D.GetDecimal(_flex[row, "UM_EX_PO"]), D.GetDecimal(_flex[row, "RT_EXCH"]), D.GetString(_flex[row, "FG_TAX"]), RT_VAT, 모듈.PUR, 부가세포함, out ldb_AM_EXREQ, out ldb_AM_REQ, out 부가세);

                        _flex[row, "AM_REQ"] = ldb_AM_REQ;
                        _flex[row, "AM"] = ldb_AM_REQ;

                        _flex[row, "AM_EXREQ"] = ldb_AM_EXREQ;
                        _flex[row, "AM_EX"] = ldb_AM_EXREQ;

                        _flex[row, "VAT"] = 부가세;
                        _flex[row, "AM_TOTAL"] = D.GetDecimal(_flex[row, "VAT"]) + D.GetDecimal(_flex[row, "AM_REQ"]);


                    }
                }

            }

        }

        #endregion

        #region -> 부가세 계산
        private void 부가세계산(DataRow row)
        {
            Decimal ldb_VatKr = 0, ldb_AmKr = 0, ldb_amEx = 0, ldb_AM = 0;   // Decimal ldb_UMkr = 0, ldb_UMEX = 0;
            String ls_FG_TAX = string.Empty;             //과세구분 
            Decimal rate_vat = D.GetDecimal(row["RT_VAT"]) == 0 ? 0 : D.GetDecimal(row["RT_VAT"]) / 100;  //과세율  

            Decimal 수량 = D.GetDecimal(row["QT_REQ_MM"]);
            Decimal 단가 = D.GetDecimal(row["UM_EX_PO"]);
            Decimal 환율 = D.GetDecimal(_flex["RT_EXCH"]) == 0 ? 1 : D.GetDecimal(_flex["RT_EXCH"]);

            if (수량 == 0)
                return;

            if (D.GetString(row["TP_UM_TAX"]) == "001")  //부가세포함
            {
                /* 총금액     : 반올림( 수량 * 단가 * 환율)  
                 * 부가세     : 반올림( 총금액 / (1 + 과세율) * 과세율 )  
                 * 원화금액   : 총금액 - 부가세     
                 * 외화금액   : 원화금액  /  환율       
                */
                ldb_AM = Decimal.Round(수량 * 단가 * 환율, MidpointRounding.AwayFromZero); ; //총금액 
                if (s_vat_fictitious == "100")
                    ldb_VatKr = Unit.원화금액(DataDictionaryTypes.PU, ldb_AM * rate_vat);
                else
                    ldb_VatKr = Decimal.Round(ldb_AM / (1 + rate_vat) * rate_vat, MidpointRounding.AwayFromZero); //부가세   
                ldb_AmKr = Unit.원화금액(DataDictionaryTypes.PU, ldb_AM - ldb_VatKr);   //원화금액   
                ldb_amEx = Unit.외화금액(DataDictionaryTypes.PU, ldb_AmKr / 환율);  // 외화금액 
                //ldb_amEx = D.GetDecimal(row["AM_EX"]);
                //ldb_AmKr = Unit.원화금액(DataDictionaryTypes.PU, ldb_amEx * tb_NM_EXCH.DecimalValue);
                //if (cbo_TP_TAX.SelectedValue.ToString() == "001") //부가세포함
                //{
                //    ldb_VatKr = Unit.원화금액(DataDictionaryTypes.PU, System.Math.Round(D.GetDecimal(ldb_AmKr) / (1 + rate_vat), 9) * rate_vat);  //부가세구하기                              
                //    ldb_AmKr = ldb_AmKr - ldb_VatKr;
                //    ldb_amEx = ldb_AmKr;  //부가세 포함일경우 외화금액은 의미가 없슴

                //    row["UM_EX_PO"] = ldb_amEx / 수량; // _flexD.CDecimal(_flexD[row,"AM_EX"]) / 수량;
                //    row["UM_P"] = row["UM_EX_PO"];

                //    row["UM_EX"] = (D.GetDecimal(row["QT_PO"]) == 0) ? 0 : (ldb_amEx / D.GetDecimal(row["QT_PO"]));  // 외화 재고단위 단가                
                //    row["UM"] = row["UM_EX"];

                //}
                //else
                //{
                //    ldb_VatKr = Unit.원화금액(DataDictionaryTypes.PU, ldb_AmKr * rate_vat);
                //}
            }
            else
            {
                ldb_amEx = Unit.외화금액(DataDictionaryTypes.PU, 수량 * 단가);  // 외화금액
                ldb_AmKr = Unit.원화금액(DataDictionaryTypes.PU, 수량 * 단가 * 환율);   //원화금액  
                ldb_VatKr = Unit.원화금액(DataDictionaryTypes.PU, ldb_AmKr * rate_vat); //부가세   
                ldb_AM = Unit.원화금액(DataDictionaryTypes.PU, ldb_AmKr + ldb_VatKr);   //총금액  


                // ldb_UMEX  = ldb_amEx / 수량;  //외화단가   

                //    //ldb_VatKr = Unit.원화금액(DataDictionaryTypes.PU, ldb_AmKr * rate_vat);
                //    //ldb_amEx  = Unit.원화금액(DataDictionaryTypes.PU, System.Math.Round(수량 * 단가));  // 외화금액 
                //    //ldb_VatKr = Unit.원화금액(DataDictionaryTypes.PU, System.Math.Round(ldb_amEx * rate_vat * 환율));  //부가세구하기 
                //    //ldb_UMEX = 단가;  //외화단가 

                //    //ldb_AmKr = Unit.원화금액(DataDictionaryTypes.PU, System.Math.Round(ldb_amEx * 환율));   //원화금액   
                //    //ldb_UMkr = ldb_AmKr / 수량; 

                //row["UM_EX_PO"] = 단가;     //ldb_UMEX;   //외화단가(발주단위)
                //row["UM_EX"]    = (D.GetDecimal(row["QT_PO"]) == 0) ? 0 : (단가 / D.GetDecimal(row["QT_PO"]));  //  외화단가( 재고단위)     
                //row["UM_P"]     = 단가;   
            }
            row["UM_EX_PO"] = 단가;                                                              //외화단가(발주단위)   

            row["UM_EX"] = 단가 / ((D.GetDecimal(row["RATE_EXCHG"]) == 0) ? 1 : D.GetDecimal(row["RATE_EXCHG"])); ;   //(D.GetDecimal(row["QT_PO"]) == 0) ? 0 : (단가 / D.GetDecimal(row["QT_PO"]));  //  외화단가( 재고단위)     
            row["UM"] = 단가 / ((D.GetDecimal(row["RATE_EXCHG"]) == 0) ? 1 : D.GetDecimal(row["RATE_EXCHG"])) * 환율;    //원화단가                                               

            row["AM_EXREQ"] = ldb_amEx;
            row["AM_EX"] = ldb_amEx;

            row["AM_REQ"] = ldb_AmKr;
            row["AM"] = ldb_AmKr;
            
            row["VAT"] = ldb_VatKr;
            row["AM_TOTAL"] = ldb_AmKr + ldb_VatKr;//_flexD.CDecimal(_flexD["AM"]) + _flexD.CDecimal(_flexD["VAT"]);
            _flex.SumRefresh();
           
        }

        #endregion

        #endregion

        private void Control_KeyEvent(object sender, System.Windows.Forms.KeyEventArgs e)
        {

            if (e.KeyData == Keys.Enter)
                SendKeys.SendWait("{TAB}");

            if (((Control)sender).Name == "cbo_TRANS")
                cbo_TRANS_SelectionChangeCommitted(sender, e);

        }

        #region -> 속성

        #region -> 추가모드여부

        private bool 추가모드여부
        {
            get
            {
                if (_header.JobMode == JobModeEnum.추가후수정)
                    return true;

                return false;
            }
        }

        #endregion

        #region -> 헤더변경여부

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

        #endregion

        private void m_btnDel_Click(object sender, EventArgs e)
        {

            if (!_flex.HasNormalRow)
                return;

            if (Global.MainFrame.ServerKeyCommon == "GIGAVIS")
            {
                DataRow[] drChk = _flex.DataTable.Select(" S = 'Y' ", "", DataViewRowState.CurrentRows);

                if (drChk == null || drChk.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                foreach (DataRow row in drChk)
                {
                    if (_flex.CDecimal(row["QT_CLS"]) != 0)
                    {
                        ShowMessage("마감된 데이터입니다. 삭제할 수 없습니다.");
                        return;
                    }

                    row.Delete();
                }
            }
            else
            {
                if (_flex.CDecimal(_flex[_flex.Row, "QT_CLS"]) != 0)
                {
                    ShowMessage("마감된 데이터입니다. 삭제할 수 없습니다.");
                    return;
                }
                string NO_PO = D.GetString(_flex["NO_PO"]);
                string NO_POLINE = D.GetString(_flex["NO_POLINE"]);

                //for (int i = 0; i < _flexD.Rows.Count; i++)
                //{
                //    if (D.GetString(_flexD.DataTable.Rows[i]["NO_PO"]) == NO_PO && D.GetString(_flexD.DataTable.Rows[i]["NO_POLINE"]) == NO_POLINE)
                //    {
                //        _flex.Rows.Remove(i);
                //    }
                //}

                if (m_YN_SU == "100")
                {
                    DataRow[] dr = _flexD.DataTable.Select("NO_PO ='" + NO_PO + "' AND NO_POLINE='" + NO_POLINE + "'");
                    foreach (DataRow drD in dr)
                    {
                        drD.Delete();
                    }
                }

                _flex.Rows.Remove(_flex.Row);
            }


            if (!_flex.HasNormalRow)  //지운후 다 없어지면 추가모드
            {
                ControlEnabledDisable(true);
                // OnToolBarAddButtonClicked(null, null);
            }

            ToolBarSaveButtonEnabled = true;
        }

        #region -> 적용버튼
        private void btn_Appet_Click(object sender, EventArgs e)
        {
           
            String ColName = string.Empty, ColName2 = string.Empty;
            String Data = string.Empty, Data2 = string.Empty;

            if (!추가모드여부)
            {
                return;
            }

            switch (((RoundedButton)sender).Name)
            {

                case "btn_SL_Accept":
                    if (!추가모드여부)
                    {
                        ShowMessage("이미입고되어수정불가합니다");
                        return;
                    }
                    ColName = "CD_SL";
                    ColName2 = "NM_SL";
                    Data = bp_CD_SL.CodeValue;
                    Data2 = bp_CD_SL.CodeName;

                    break;

                case "btn적용_관리번호":
                    ColName = "DC_RMK2";
                    Data = D.GetString(txt관리번호.Text);
                    break;


            }
            DataRow[] dr = _flex.DataTable.Select("S = 'Y'");
            //foreach(DataRow dr in _flexD.DataTable.Rows)
            for (int row = 0; row < dr.Length; row++)
            {
                dr[row][ColName] = Data;
                if (ColName2 != string.Empty)
                    dr[row][ColName2] = Data2;

            }
      

        }
        #endregion

        #region -> 버튼 배열처리(visiable인것 가지런히 나열)
        private void SetButtonDisp(RoundedButton[] p_button, int p_X)
        {
            int iGAP = 1;
            int iLocation_X = p_X;
            int btnWidth = 0;
            //RoundedButton[] p_button = { btn전자결재, btn_PRJ_SUB, btn_예산chk내역, btn_H41_APP };

            for (int idx = 0; idx < p_button.Length; idx++)
            {
                if (p_button[idx].Visible == true)
                {
                    btnWidth = p_button[idx].Width;
                    iLocation_X = iLocation_X - iGAP - btnWidth;
                    p_button[idx].SetBounds(iLocation_X, p_button[idx].Location.Y, p_button[idx].Width, p_button[idx].Height);
                }
            }
        }
        #endregion

        private void SET_FLEXD(string NO_PO_MULTI)
        {
            try
            {
                if (NO_PO_MULTI == string.Empty) return;

                DataTable dt = _biz.Search_MATL(NO_PO_MULTI, new object[] { Global.MainFrame.LoginInfo.CompanyCode, "", D.GetString(tb_NM_PARTNER.CodeValue), D.GetString(cbo공장.SelectedValue) });

                string filter = "NO_PO = '" + D.GetString(_flex["NO_PO"]) + "' AND NO_POLINE = '" + D.GetString(_flex["NO_POLINE"]) + "' ";
                decimal NO_LINE = D.GetDecimal(_flexD.DataTable.Compute("MAX(NO_IOLINE)", filter));
             


                if (dt == null || dt.Rows.Count == 0) return;

                foreach (DataRow dr in dt.Rows)
                {
                    
                    _flexD.Rows.Add();
                    _flexD.Row = _flexD.Rows.Count - 1;

                    string filter2 = "NO_PO = '" + D.GetString(dr["NO_PO"]) + "' AND NO_POLINE = '" + D.GetString(dr["NO_POLINE"]) + "' ";
                    decimal QT_REQ_MM = D.GetDecimal(_flex.DataTable.Compute("MAX(QT_REQ_MM)", filter2));
 

                    _flexD["CD_ITEM"] = dr["CD_ITEM"];
                    _flexD["NM_ITEM_ITEM"] = dr["NM_ITEM_ITEM"];
                    _flexD["STND_ITEM"] = dr["STND_ITEM"];
                    _flexD["STND_ITEM_ITEM"] = dr["STND_ITEM_ITEM"];
                    _flexD["UNIT_IM_ITEM"] = dr["UNIT_IM_ITEM"];
                    _flexD["CD_MATL"] = dr["CD_MATL"];
                    _flexD["NM_ITEM"] = dr["NM_ITEM"];
                    _flexD["STND_ITEM"] = dr["STND_ITEM"];
                    _flexD["UNIT_IM"] = dr["UNIT_IM"];
                    _flexD["QT_NEED"] = D.GetDecimal(dr["QT_NEED_UNIT"]) * QT_REQ_MM;
                    _flexD["NO_PO"] = dr["NO_PO"];
                    _flexD["NO_POLINE"] = dr["NO_POLINE"];
                    _flexD["NO_PO_MAL_LINE"] = dr["NO_PO_MAL_LINE"];
                    _flexD["CD_SL"] = dr["CD_SL"];
                    _flexD["NM_SL"] = dr["NM_SL"];
                    _flexD["NO_IOLINE"] = ++NO_LINE;
                    _flexD["CD_PLANT"] = D.GetString(cbo공장.SelectedValue);
                    _flexD["DT_IO"] = D.GetString(tb_DT_RCV.Text);
                    _flexD["NO_PSO_MGMT"] = dr["NO_PO"];
                    _flexD["NO_PSOLINE_MGMT"] = dr["NO_POLINE"];
                    _flexD["CD_PARTNER"] = D.GetString(tb_NM_PARTNER.CodeValue);
                    _flexD["QT_IO"] = dr["QT_NEED"];
                    _flexD["NO_EMP"] = D.GetString(tb_NO_EMP.CodeValue);
                    _flexD["FG_IO"] = dr["FG_IO"];
                    _flexD["CD_QTIOTP"] = dr["CD_QTIOTP"];
                    _flexD["FG_TRANS"] = dr["FG_TRANS"];
                    _flexD["GI_PARTNER"] = D.GetString(tb_NM_PARTNER.CodeValue);
                    _flexD["YN_RETURN"] = dr["YN_RETURN"];
                    _flexD["CD_DEPT"] = dr["CD_DEPT"];
                    _flexD["NO_IOLINE_MGMT"] = D.GetDecimal(_flex.DataTable.Compute("MAX(NO_LINE)", filter2));
                    _flexD["QT_NEED_UNIT"] = dr["QT_NEED_UNIT"];
                    _flexD["YN_PARTNER_SL"] = dr["YN_PARTNER_SL"];

                }


                _flexD.AddFinished();
                _flexD.Col = _flexD.Cols.Fixed;

                _flexD.RowFilter = filter;
            }


            catch (Exception ex)
            {
                MsgEnd(ex);


            }

        }

        private void tab_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (_flexD.HasNormalRow)
                {
                    string filter = "NO_PO = '" + D.GetString(_flex["NO_PO"]) + "' AND NO_POLINE = '" + D.GetString(_flex["NO_POLINE"]) + "' ";
                    _flexD.RowFilter = filter;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void chk_barcode_use_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_barcode_use.Checked == true)
                txt바코드.Focus();
        }

        #region -> 첨부파일
        void btn첨부파일_Click(object sender, EventArgs e)
        {
            try
            {
                if (tb_NoIsuRcv.Text == string.Empty) return;

                string cd_file_code = D.GetString(tb_NoIsuRcv.Text + "_" + Global.MainFrame.LoginInfo.CompanyCode); //파일 PK설정 
                P_MA_FILE_SUB m_dlg = new master.P_MA_FILE_SUB("PU", Global.MainFrame.CurrentPageID, cd_file_code);

                if (m_dlg.ShowDialog(this) == DialogResult.Cancel) return;
            }
            catch (Exception ex)
            {

                MsgEnd(ex);
            }
        } 
        #endregion

        #region ▷ 원그리드 적용하기

        void 원그리드적용하기()
        {
            System.Drawing.Size s1 = oneGrid1.Size;

            oneGrid1.UseCustomLayout = true;
            bpPanelControl1.IsNecessaryCondition = true;
            bpPanelControl2.IsNecessaryCondition = true;
            bpPanelControl3.IsNecessaryCondition = true;
            bpPanelControl4.IsNecessaryCondition = true;
            bpPanelControl7.IsNecessaryCondition = true;
            bpPanelControl11.IsNecessaryCondition = true;
            bpPanelControl12.IsNecessaryCondition = true;

            oneGrid1.IsSearchControl = false;   //2013.07.26 - 윤성우 - ONE GRID 수정(입력 전용)

            oneGrid1.InitCustomLayout();

            //tabpage resizing 하기
            System.Drawing.Size s2 = oneGrid1.Size;
            System.Drawing.Size t = panel_Head.Size;
            t.Height = t.Height + (s2.Height - s1.Height);
            panel_Head.Size = t;

            return;
        }

        #endregion

        private bool 의제매입여부(string ps_taxp)
        {
            if (ps_taxp == "27" || ps_taxp == "28" || ps_taxp == "29" || ps_taxp == "30" || ps_taxp == "32" || ps_taxp == "33" ||
                ps_taxp == "34" || ps_taxp == "35" || ps_taxp == "36" || ps_taxp == "40" || ps_taxp == "41" || ps_taxp == "42" ||
                ps_taxp == "48" || ps_taxp == "49")

                return true;

            return false;
        }
    }
}
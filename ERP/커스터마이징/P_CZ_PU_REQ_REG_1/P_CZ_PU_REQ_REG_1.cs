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
using pur;
using Dintec;

namespace cz
{
    public partial class P_CZ_PU_REQ_REG_1 : PageBase
    {
        #region ♣ 전역 변수
        private P_CZ_PU_REQ_REG_1_BIZ _biz = new P_CZ_PU_REQ_REG_1_BIZ();
        private FreeBinding _header = new FreeBinding();
        
        private decimal NO_RCV_LINE;
        private string LOT사용여부 = string.Empty; //시스템통제등록 LOT사용여부 
        private string Serial사용여부 = string.Empty; //시스템통제등록 SERIAL사용여부 
        private string 재고단위수정여부 = "N";
        private string 수입검사사용여부 = "N";
        private string 프로젝트재고사용여부 = "000";
        private string 특채수량사용여부 = "N";
        private string 업체별프로세스 = "000";
        private string 메일전송설정 = "N";
        private string 외주유무 = "000";
        private string 의제부가세적용 = "000";
        private string 규격형사용유무 = "000";
        private string strNO_RCV;
        private string strSOURCE;
        private bool 프로젝트사용여부 = false; // 환경설정
        private bool 단가권한 = true;
        private bool 금액권한 = true;
        private bool _isChagePossible;

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
                if (bChange && _header.JobMode == JobModeEnum.추가후수정 && !this._flexA.HasNormalRow)
                    bChange = false;

                return bChange;
            }
        }
        #endregion
        
        #region ♣ 초기화

        public P_CZ_PU_REQ_REG_1() : this("", 0m, "") { }

        public P_CZ_PU_REQ_REG_1(string strNO_RCV, decimal NO_RCV_LINE, string strSOURCE)
        {
            try
            {
                StartUp.Certify(this);
                InitializeComponent();

                this.strNO_RCV = strNO_RCV;
                this.NO_RCV_LINE = NO_RCV_LINE;
                this.strSOURCE = strSOURCE;   //구매입고현황 , 구매입고리스트 -> "화면이동"로 들어오도록 했습

                this.MainGrids = new FlexGrid[] { this._flexA };
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void ControlEnabledDisable(bool enable)
        {
            this.dtp입고일자.Enabled = enable;   //전체 panel부분 활성화
            this.cbo공장.Enabled = enable;
            this.cbo거래구분.Enabled = enable;
            this.ctx매입처.Enabled = enable;
            this.ctx담당자.Enabled = enable;
            this.cbo자동입고.Enabled = enable;
            this.cboLC.Enabled = false;
            this.cbo거래구분.Enabled = enable;
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            DataTable dt = Duzon.ERPU.MF.Common.BASIC.MFG_AUTH("P_PU_REQ_REG_1");
            if (dt.Rows.Count > 0)
            {
                단가권한 = (dt.Rows[0]["YN_UM"].ToString() == "Y") ? false : true;
                금액권한 = (dt.Rows[0]["YN_AM"].ToString() == "Y") ? false : true;
            }

            MA_EXC_SETTING();//사용자통제설정

            InitGrid();
            InitEvent();
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            oneGrid1.UseCustomLayout = true;
            oneGrid1.IsSearchControl = false;   //2013.07.26 - 윤성우 - ONE GRID 수정(입력 전용)

            bpPanelControl1.IsNecessaryCondition = true;
            bpPanelControl2.IsNecessaryCondition = true;
            bpPanelControl3.IsNecessaryCondition = true;
            bpPanelControl4.IsNecessaryCondition = true;
            bpPanelControl7.IsNecessaryCondition = true;
            bpPanelControl11.IsNecessaryCondition = true;
            bpPanelControl12.IsNecessaryCondition = true;

            oneGrid1.InitCustomLayout();

            Initial_Binding();// 헤더,그리드 초기화

            InitControl();

            DataTable dt = _biz.SearchLOT();
            LOT사용여부 = dt.Rows[0]["MNG_LOT"].ToString();

            DataTable dt2 = _biz.SearchSerial();
            Serial사용여부 = dt2.Rows[0]["YN_SERIAL"].ToString();

            btnLocalLC.Enabled = false;
            btn통관적용.Enabled = false;
            _isChagePossible = true;

            ctx담당자.CodeValue = Global.MainFrame.LoginInfo.EmployeeNo;
            ctx담당자.CodeName = Global.MainFrame.LoginInfo.EmployeeName;

            dtp입고일자.Text = Global.MainFrame.GetStringToday;

            if (외주유무 == "100")
            {
                DataTable dtB = _biz.SearchMATL("ASDFASDF");
                this._flexB.Binding = dtB;
            }

            if (D.GetString(strNO_RCV) != string.Empty)
            {
                DataSet ds = null;

                if (strSOURCE == "화면이동")   //구매입고현황 , 구매입고리스트에서 화면이동된 경우
                    ds = _biz.Search(strNO_RCV, "N");
                else
                    ds = _biz.Search(strNO_RCV, "Y");   //자동입고일 경우에만 조회해 보이기 위해 'Y' 표시

                _header.SetDataTable(ds.Tables[0]);
                if (strSOURCE != "화면이동")
                    txt입고번호.Text = string.Empty;

                if (ds != null && ds.Tables.Count > 1)
                {
                    DataTable ldt_head = ds.Tables[0];
                    DataTable ldt_line = ds.Tables[1];

                    Button_Enabled(ldt_head, ldt_line);
                    this._flexA.Binding = ds.Tables[1];

                    if (!this._flexA.HasNormalRow)
                        if (!_header.CurrentRow.IsNull(0))
                            this.ShowMessage(PageResultMode.SearchNoData);

                    _header.AcceptChanges();
                    this._flexA.AcceptChanges();
                    ControlEnabledDisable(false);
                }
            }   
        }

        private void MA_EXC_SETTING()
        {
            try
            {
                #region 수입검사 사용여부
                수입검사사용여부 = ComFunc.전용코드("수입검사-사용구분");
                if (수입검사사용여부 == "Y")
                    this.btn수입검사적용.Visible = true;
                else
                    this.btn수입검사적용.Visible = false;
                #endregion

                #region 업체별 프로세스
                업체별프로세스 = BASIC.GetMAEXC("구매입고처리-업체별프로세스선택");
                if (업체별프로세스 == "100")  //YTN전용
                {
                    bpPanelControl6.Visible = true;
                    bpPanelControl13.Visible = false;
                }
                else
                {
                    bpPanelControl6.Visible = false;
                    bpPanelControl13.Visible = true;
                }
                #endregion

                #region 메일전송설정
                메일전송설정 = BASIC.GetMAEXC("구매입고처리-메일전송설정");
                if (메일전송설정 == "Y")
                    this.btn메일전송.Visible = true;
                else
                    this.btn메일전송.Visible = false;
                #endregion

                #region 외주유무
                외주유무 = BASIC.GetMAEXC("구매발주-외주유무");
                if (외주유무 == "100")
                    this.InitGridB();
                else
                    this.tabControlExt2.TabPages.RemoveAt(1);
                #endregion
                
                this.chk바코드사용여부.Visible = false;
                this.bpPanelControl10.Visible = false;
                this.bpPanelControl15.Visible = false;

                this.cboLC.Visible = false;
                this.btn가입고적용.Visible = false;
                this.btnLocalLC.Visible = false;
                this.btn통관적용.Visible = false;

                프로젝트재고사용여부 = ComFunc.전용코드("프로젝트재고사용");
                프로젝트사용여부 = App.SystemEnv.PROJECT사용;
                특채수량사용여부 = ComFunc.전용코드("특채수량 사용여부");
                재고단위수정여부 = _biz.EnvSearch();
                규격형사용유무 = BASIC.GetMAEXC("공장품목등록-규격형");
                의제부가세적용 = BASIC.GetMAEXC("발주등록(공장)-의제부가세적용");
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void InitControl()
        {
            DataSet ds = this.GetComboData("N;TR_IM00006", "NC;MA_PLANT", "N;PU_C000025", "N;PU_C000016", "N;PU_C000005", "N;MA_B000010");

            this.cboLC.DataSource = ds.Tables[0].DefaultView;
            this.cboLC.DisplayMember = "NAME";
            this.cboLC.ValueMember = "CODE";

            this.cbo공장.DataSource = ds.Tables[1].DefaultView;
            this.cbo공장.DisplayMember = "NAME";
            this.cbo공장.ValueMember = "CODE";

            if (this.cbo공장.Items.Count > 0)
            {
                if (LoginInfo.CdPlant != "") this.cbo공장.SelectedValue = LoginInfo.CdPlant;
                this.cbo공장.SelectedIndex = 0;
                this._header.CurrentRow["CD_PLANT"] = D.GetString(this.cbo공장.SelectedValue);
            }

            this.cbo거래구분.DataSource = ds.Tables[3].DefaultView;
            this.cbo거래구분.DisplayMember = "NAME";
            this.cbo거래구분.ValueMember = "CODE";

            this.cbo자동입고.DataSource = ds.Tables[2].DefaultView;
            this.cbo자동입고.DisplayMember = "NAME";
            this.cbo자동입고.ValueMember = "CODE";

            // 부가세여부
            this._flexA.SetDataMap("TP_UM_TAX", ds.Tables[4], "CODE", "NAME");

            //품목계정
            this._flexA.SetDataMap("CLS_ITEM", ds.Tables[5], "CODE", "NAME");

            this.cboLC.Enabled = false;
            this.txt입고번호.Text = string.Empty;

            if (!string.IsNullOrEmpty(Settings1.Default.cd_sl_apply))
            {
                this.ctx창고.CodeValue = Settings1.Default.cd_sl_apply;
                this.ctx창고.CodeName = Settings1.Default.nm_sl_apply;
            }
        }

        private void InitGrid()
        {
            this._flexA.BeginSetting(1, 1, false);

            this._flexA.SetDummyColumn("S");
            this._flexA.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flexA.SetCol("CD_ITEM", "품목코드", 110, false);
            this._flexA.SetCol("NM_ITEM", "품목명", 130, false);
            this._flexA.SetCol("STND_ITEM", "규격", 70, false);
            this._flexA.SetCol("CD_UNIT_MM", "발주단위", 70, false);
            this._flexA.SetCol("CD_ZONE", "저장위치", 100, false);
            this._flexA.SetCol("QT_REQ_MM", "입고량", 80, true, typeof(decimal), FormatTpType.QUANTITY);

            if (규격형사용유무 == "100")
            {
                this._flexA.SetCol("UM_WEIGHT", "중량단가", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                this._flexA.SetCol("TOT_WEIGHT", "총중량", 100, true, typeof(decimal));

                this._flexA.Cols["TOT_WEIGHT"].Format = "###,###,###.#";
            }

            if (단가권한)
                this._flexA.SetCol("UM_EX_PO", "단가", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);

            if (금액권한)
            {
                this._flexA.SetCol("AM_EXREQ", "금액", 100, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flexA.SetCol("AM_REQ", "원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
                this._flexA.SetCol("VAT", "부가세", 90, false, typeof(decimal), FormatTpType.MONEY);
                this._flexA.SetCol("AM_TOTAL", "총금액", 80, true, typeof(decimal), FormatTpType.MONEY);
            }

            this._flexA.SetCol("PI_PARTNER", "주거래처", false);
            this._flexA.SetCol("PI_LN_PARTNER", "주거래처명", false);
            this._flexA.SetCol("TP_UM_TAX", "부가세여부", 70, false);
            this._flexA.SetCol("DT_LIMIT", "납기일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexA.SetCol("NO_LOT", "LOT여부", 60);
            this._flexA.SetCol("YN_INSP", "검사", 50, true, CheckTypeEnum.Y_N);
            this._flexA.SetCol("CD_SL", "창고코드", 60, true);
            this._flexA.SetCol("NM_SL", "창고명", 120, true);
            this._flexA.SetCol("NO_PO", "발주번호", 100, false);
            this._flexA.SetCol("NM_KOR", "담당자", 80, false);
            this._flexA.SetCol("NM_FG_RCV", "입고형태", 100, false);
            this._flexA.SetCol("NM_FG_POST", "발주상태", 100, false);

            this._flexA.SetCol("CD_PJT", "PROJECT코드", 100, false);
            this._flexA.SetCol("NM_PROJECT", "PROJECT", 100, false);
            this._flexA.SetCol("NM_PURGRP", "구매그룹", 100, false);
            this._flexA.SetCol("UNIT_IM", "관리단위", 90, false);
            this._flexA.SetCol("NO_BL", "BL번호", 120, false);
            this._flexA.SetCol("QT_REQ", "관리수량", 120, (재고단위수정여부 == "Y") ? true : false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexA.SetCol("RT_EXCH", "환율", 120, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flexA.SetCol("DC_RMK", "의뢰라인비고1", 150, 200, true);
            this._flexA.SetCol("DC_RMK2", "의뢰라인비고2", 150, 40, true);
            this._flexA.SetCol("DT_PLAN", "납기예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            if (업체별프로세스 == "100") //YTN전용
            {
                this._flexA.Rows[0]["DC_RMK"] = "문서번호";
                this._flexA.Rows[0]["DC_RMK2"] = "입고번호";
            }

            if (Config.MA_ENV.프로젝트사용)
            {
                this._flexA.SetCol("SEQ_PROJECT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 항번" : "프로젝트항번", 120, false, typeof(decimal));
                this._flexA.SetCol("CD_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 코드" : "프로젝트 품목코드", 140, false, typeof(string));
                this._flexA.SetCol("NM_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 명" : "프로젝트 품목명", 140, false, typeof(string));
                this._flexA.SetCol("PJT_ITEM_STND", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 규격" : "프로젝트 품목규격", 140, false, typeof(string));

            }

            if (Config.MA_ENV.PJT형여부 == "Y")
            {
                this._flexA.SetCol("NO_WBS", "WBS번호", 140, false, typeof(string));
                this._flexA.SetCol("NO_CBS", "CBS번호", 140, false, typeof(string));
            }

            // 단지 DISPLAY용
            this._flexA.SetCol("REV_QT_PASS", "검수수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexA.SetCol("REV_QT_REV_MM", "납품계획수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);

            // 이것도 단지 DISPLAY 아카데미과학에서 요청 D20120229048
            this._flexA.SetCol("CD_ITEM_ORIGIN", "관련품목", 140, false, typeof(string));
            this._flexA.SetCol("NM_ITEM_ORIGIN", "관련품목명", 140, false, typeof(string));
            this._flexA.SetCol("STND_ITEM_ORIGIN", "관련품목규격", 140, false, typeof(string));

            this._flexA.SetCol("GI_PARTNER", "납품처코드", 140, false, typeof(string));
            this._flexA.SetCol("NM_GI_PARTER", "납품처명", 140, false, typeof(string));

            this._flexA.SetCol("CLS_ITEM", "품목계정", 140, false, typeof(string));
            this._flexA.SetCol("MAT_ITEM", "재질", 140, false, typeof(string));

            this._flexA.SettingVersion = "1.0.0.4";
            this._flexA.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            if (Config.MA_ENV.YN_UNIT == "Y")
                this._flexA.SetExceptSumCol("UM_EX_PO", "SEQ_PROJECT");
            else
            {
                if (규격형사용유무 == "100")
                    this._flexA.SetExceptSumCol("UM_EX_PO", "UM_WEIGHT");
                else
                    this._flexA.SetExceptSumCol("UM_EX_PO");
            }

            this._flexA.VerifyCompare(this._flexA.Cols["QT_REQ_MM"], 0, OperatorEnum.Greater);

            if (App.SystemEnv.PROJECT사용 == true)
            {
                if (Config.MA_ENV.YN_UNIT == "Y")
                    this._flexA.VerifyNotNull = new string[] { "CD_ITEM", "DT_LIMIT", "NM_SL", "NM_PROJECT", "SEQ_PROJECT" };
                else
                    this._flexA.VerifyNotNull = new string[] { "CD_ITEM", "DT_LIMIT", "NM_SL", "NM_PROJECT" };
            }
            else
                this._flexA.VerifyNotNull = new string[] { "CD_ITEM", "DT_LIMIT", "NM_SL" };

            this._flexA.SetExceptEditCol("CD_ITEM", "NM_ITEM", "STND_ITEM", "CD_UNIT_MM", "UNIT", "NM_SL", "NO_PO", "NM_KOR", "NM_FG_RCV", "NM_FG_POST", "NM_PROJECT", "UNIT_IM"/*, "QT_REQ"*/);
            this._flexA.SetCodeHelpCol("CD_SL", HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CD_SL", "NM_SL" }, new string[] { "CD_SL", "NM_SL" }, new string[] { "CD_SL", "NM_SL" });
        }

        private void InitGridB()
        {
            this._flexB.BeginSetting(1, 1, false);

            this._flexB.SetCol("CD_ITEM", "의뢰품목", 100, 20, false);
            this._flexB.SetCol("NM_ITEM_ITEM", "품목명", 140, 50, false);
            this._flexB.SetCol("STND_ITEM_ITEM", "규격", 120, 50, false);
            this._flexB.SetCol("UNIT_IM_ITEM", "단위", 40, 3, false);
            this._flexB.SetCol("CD_MATL", "자재코드", 100, 20, false);
            this._flexB.SetCol("NM_ITEM", "자재명", 140, 50, false);
            this._flexB.SetCol("STND_ITEM", "규격", 120, 50, false);
            this._flexB.SetCol("UNIT_IM", "단위", 40, 3, false);
            this._flexB.SetCol("QT_NEED", "출고의뢰수량", 90, 17, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flexB.SetCol("NO_PO", "발주번호", 100, 20, false);
            this._flexB.SetCol("NO_POLINE", "발주항번", 60, 5, false);
            this._flexB.SetCol("NO_PO_MAL_LINE", "사급자재항번", 80, 5, false);
            this._flexB.SetCol("CD_SL", "창고코드", 80, 7, true, typeof(string));
            this._flexB.SetCol("NM_SL", "창고명", 120, false, typeof(string));

            if (Config.MA_ENV.프로젝트사용)
            {
                this._flexB.SetCol("CD_PJT", "프로젝트", 140, 100, true, typeof(string));
                this._flexB.SetCol("NM_PJT", "프로젝트명", 140, 100, false, typeof(string));
                this._flexB.SetCol("SEQ_PROJECT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 항번" : "프로젝트항번", 120, false, typeof(decimal));
            }
            if (Config.MA_ENV.PJT형여부 == "Y")
            {
                this._flexB.SetCol("CD_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 코드" : "프로젝트 품목코드", 140, false, typeof(string));
                this._flexB.SetCol("PJT_NM_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 명" : "프로젝트 품목명", 140, false, typeof(string));
                this._flexB.SetCol("PJT_STND_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 규격" : "프로젝트 품목규격", 140, false, typeof(string));
            }

            this._flexB.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            this._flexB.SetCodeHelpCol("CD_SL", HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CD_SL", "NM_SL" }, new string[] { "CD_SL", "NM_SL" }, new string[] { "CD_SL", "NM_SL" });

            if (Config.MA_ENV.YN_UNIT == "Y")
            {
                this._flexB.SetCodeHelpCol("CD_PJT", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new String[] { "CD_PJT", "NM_PJT", "SEQ_PROJECT", "CD_PJT_ITEM", "PJT_NM_ITEM", "PJT_STND_ITEM" }, new String[] { "NO_PROJECT", "NM_PROJECT", "SEQ_PROJECT", "CD_PJT_ITEM", "NM_PJT_ITEM", "PJT_ITEM_STND" }, new String[] { "CD_PJT", "NM_PROJECT", "SEQ_PROJECT", "CD_PJT_ITEM", "PJT_NM_ITEM", "PJT_STND_ITEM" }, ResultMode.FastMode);
                this._flexB.SetCodeHelpCol("CD_PJT_ITEM", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new String[] { "CD_PJT", "NM_PROJECT", "SEQ_PROJECT", "CD_PJT_ITEM", "PJT_NM_ITEM", "PJT_STND_ITEM" }, new String[] { "NO_PROJECT", "NM_PROJECT", "SEQ_PROJECT", "CD_PJT_ITEM", "NM_PJT_ITEM", "PJT_ITEM_STND" }, new String[] { "CD_PJT", "NM_PROJECT", "SEQ_PROJECT", "CD_PJT_ITEM", "PJT_NM_ITEM", "PJT_STND_ITEM" }, ResultMode.FastMode);
            }
            else
            {
                this._flexB.SetCodeHelpCol("CD_PJT", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new String[] { "CD_PJT", "NM_PROJECT" }, new String[] { "NO_PROJECT", "NM_PROJECT" });
            }

            this._flexB.Cols["NO_POLINE"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexB.Cols["NO_PO_MAL_LINE"].TextAlign = TextAlignEnum.CenterCenter;
        }

        private void InitEvent()
        {
            this.DataChanged += new EventHandler(this.Page_DataChanged);
            this._header.ControlValueChanged += new FreeBindingEventHandler(this._header_ControlValueChanged);

            this.tabControlExt2.SelectedIndexChanged += new EventHandler(this.tabControlExt2_SelectedIndexChanged);

            this.btn첨부파일.Click += new EventHandler(this.btn첨부파일_Click);
            this.btn통관적용.Click += new EventHandler(this.btn통관적용_Click);
            this.btnLocalLC.Click += new EventHandler(this.btnLocalLC_Click);
            this.btn발주적용.Click += new EventHandler(this.btn발주적용_Click);
            this.btn가입고적용.Click += new EventHandler(this.btn가입고적용_Click);
            this.btn수입검사적용.Click += new EventHandler(this.btn수입검사적용_Click);
            this.btn메일전송.Click += new EventHandler(this.btn메일전송_Click);
            this.btn창고적용.Click += new EventHandler(this.btn적용_Click);
            this.btn관리번호적용.Click += new EventHandler(this.btn적용_Click);
            this.btn삭제.Click += new EventHandler(this.btn삭제_Click);

            this.cbo거래구분.SelectionChangeCommitted += new EventHandler(this.cbo거래구분_SelectionChangeCommitted);
            this.cboLC.SelectionChangeCommitted += new EventHandler(this.cboLC_SelectionChangeCommitted);
            this.cbo공장.SelectionChangeCommitted += new EventHandler(this.cbo공장_SelectionChangeCommitted);
            
            this.chk바코드사용여부.CheckedChanged += new EventHandler(this.chk바코드사용여부_CheckedChanged);
            
            this.ctx담당자.QueryAfter += new BpQueryHandler(this.ctx담당자_QueryAfter);
            this.ctx창고.QueryBefore += new BpQueryHandler(this.ctx창고_QueryBefore);
            
            this.txt바코드.KeyPress += new KeyPressEventHandler(this.txt바코드_KeyPress);
            
            this._flexA.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex_BeforeCodeHelp);
            this._flexA.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);
            this._flexA.StartEdit += new RowColEventHandler(this._flex_StartEdit);
            this._flexA.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);

            this._flexB.StartEdit += new RowColEventHandler(this._flex_StartEdit);
            this._flexB.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex_BeforeCodeHelp);
            this._flexB.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);
        }

        private void Initial_Binding()// 헤더,그리드 초기화
        {
            DataSet ds = _biz.InitialDataSet();//디비가 아닌 biz단에서 데이터셋을 생성해서 가져옴

            this._header.SetBinding(ds.Tables[0], this.tabControlExt1);
            this._header.ClearAndNewRow(); // 처음에 행을 하나 만든다. 초기에 추가모드로 하기 위해

            this._flexA.Binding = ds.Tables[1];
        }

        #endregion

        #region ♣ 메인버튼 이벤트

        protected override bool IsChanged()
        {
            if (base.IsChanged())       // 그리드가 변경되었거나
                return true;

            return 헤더변경여부;        // 헤더가 변경되었거나
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!base.BeforeSearch()) return;


                P_PU_REQ_SUB dlg = new P_PU_REQ_SUB(ctx매입처.CodeValue.ToString(), D.GetString(cbo공장.SelectedValue), ctx담당자.CodeValue.ToString(), "Y");

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    DataSet ds = _biz.Search(dlg.m_NO_RCV, "Y");   //자동입고일 경우에만 조회해 보이기 위해 'Y' 표시

                    _header.SetDataTable(ds.Tables[0]);
                    txt입고번호.Text = string.Empty;

                    if (ds != null && ds.Tables.Count > 1)
                    {
                        DataTable ldt_head = ds.Tables[0];
                        DataTable ldt_line = ds.Tables[1];

                        Button_Enabled(ldt_head, ldt_line);
                        this._flexA.Binding = ds.Tables[1];

                        if (!this._flexA.HasNormalRow)
                            if (!_header.CurrentRow.IsNull(0))
                                this.ShowMessage(PageResultMode.SearchNoData);

                        if (외주유무 == "100")
                        {
                            string NO_IO = _biz.GetNO_IO_MGMT(txt의뢰번호.Text);
                            this._flexB.Binding = _biz.SearchMATL(NO_IO);

                            string filter = "NO_PO = '" + D.GetString(this._flexA["NO_PO"]) + "' AND NO_POLINE = '" + D.GetString(this._flexA["NO_POLINE"]) + "' ";
                            this._flexB.RowFilter = filter;
                        }

                        _header.AcceptChanges();
                        this._flexA.AcceptChanges();
                        ControlEnabledDisable(false);
                    }
                }

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void Button_Enabled(DataTable ldt_head, DataTable ldt_line)
        {
            if (ldt_head != null && ldt_head.Rows.Count > 0)
            {
                DataRow row = ldt_head.Rows[0];
                DataRow[] ldr_row = ldt_line.Select("QT_GR_MM > 0");

                _isChagePossible = ((ldr_row != null) && (ldr_row.Length > 0)) == true ? false : true;

                if (!_isChagePossible)
                {
                    btn통관적용.Enabled = false;
                    btnLocalLC.Enabled = false;
                }

                if (ldt_head.Rows[0]["FG_TRANS"].ToString() == "001" ||
                    ldt_head.Rows[0]["FG_TRANS"].ToString() == "002")//국내
                {
                    cboLC.Enabled = false;
                    btn통관적용.Enabled = false;
                    btnLocalLC.Enabled = false;

                }
                else if (ldt_head.Rows[0]["FG_TRANS"].ToString() == "003")
                {
                    cboLC.Enabled = true;
                    if (ldt_head.Rows[0]["FG_PROCESS"].ToString() == "001")
                    {
                        btn통관적용.Enabled = false;
                        btnLocalLC.Enabled = true;
                    }
                    if (ldt_head.Rows[0]["FG_PROCESS"].ToString() == "002")
                    {
                        btn통관적용.Enabled = false;
                        btnLocalLC.Enabled = false;
                    }

                }
                else if (ldt_head.Rows[0]["FG_TRANS"].ToString() == "004" ||
                    ldt_head.Rows[0]["FG_TRANS"].ToString() == "005")
                {
                    cboLC.Enabled = true;
                    if (ldt_head.Rows[0]["FG_PROCESS"].ToString() == "001")
                    {
                        btnLocalLC.Enabled = false;
                        btn통관적용.Enabled = true;
                    }
                }

                if (ldt_head.Rows[0]["FG_PROCESS"].ToString() == "001")
                {
                    if (cbo거래구분.SelectedValue.ToString() == "002")
                    {
                        btn통관적용.Enabled = false;
                    }
                    if (cbo거래구분.SelectedValue.ToString() == "003")
                    {
                        btn통관적용.Enabled = false;
                        btnLocalLC.Enabled = true;
                    }
                    if (cbo거래구분.SelectedValue.ToString() == "004" ||
                        cbo거래구분.SelectedValue.ToString() == "005")
                    {
                        btn통관적용.Enabled = true;
                        btnLocalLC.Enabled = false;
                    }
                }
                if (ldt_head.Rows[0]["FG_PROCESS"].ToString() == "002")
                {
                    if (cbo거래구분.SelectedValue.ToString() == "002")
                    {
                        btn통관적용.Enabled = false;
                    }
                    if (cbo거래구분.SelectedValue.ToString() == "003")
                    {
                        btn통관적용.Enabled = false;
                        btnLocalLC.Enabled = false;
                    }
                }
            }
        }

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarAddButtonClicked(sender, e);

                if (!BeforeAdd())
                    return;

                Debug.Assert(_header.CurrentRow != null);       // 혹시나 해서 한번 더 확인
                Debug.Assert(this._flexA.DataTable != null);          // 혹시나 해서 한번 더 확인

                this._flexA.DataTable.Rows.Clear();
                this._flexA.AcceptChanges();

                _header.ClearAndNewRow();
                InitControl();
                ControlEnabledDisable(true);
                cbo거래구분_SelectionChangeCommitted(null, null);

                if (cbo거래구분.SelectedValue.ToString() == "001")
                    cboLC.Enabled = false;

                if (외주유무 == "100")
                {
                    this._flexB.DataTable.Rows.Clear();
                    this._flexB.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarDeleteButtonClicked(sender, e);

                if (!BeforeDelete())
                    return;

                DialogResult result = MessageBoxEx.Show(this.GetMessageDictionaryItem("MA_M000103"), this.PageName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (this._flexA != null && this._flexA.HasNormalRow)
                    {
                        string NO_RCV = _header.CurrentRow["NO_RCV"].ToString();
                        string NO_IO = this._flexA[this._flexA.Rows.Fixed, "NO_IO"].ToString();
                        string NO_REQ = D.GetString(txt의뢰번호.Text);
                        string NO_IO_MGMT = string.Empty;

                        if (외주유무 == "100")
                        {
                            NO_IO_MGMT = _biz.GetNO_IO(NO_IO);
                        }
                        object[] lsa_args1 = new object[] { NO_RCV, NO_IO, this.LoginInfo.CompanyCode, NO_IO_MGMT };
                        object[] lsa_args2 = new object[] { this.LoginInfo.CompanyCode, NO_REQ };

                        _biz.Delete(lsa_args1, lsa_args2);
                        ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);

                        this.OnToolBarAddButtonClicked(sender, e);
                        ControlEnabledDisable(true);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private bool 필수항목확인()
        {
            try
            {
                if (D.GetString(cbo공장.SelectedValue) == "")
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, lbl공장.Text);
                    cbo공장.Focus();
                    return false;
                }

                if (ctx매입처.CodeValue.Trim() == "")
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, lbl매입처.Text);
                    ctx매입처.Focus();
                    return false;
                }

                if (dtp입고일자.Text.Trim() == "")
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, lbl입고일자.Text);
                    dtp입고일자.Focus();
                    return false;
                }

                if (ctx담당자.CodeValue.Trim() == "")
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, lbl담당자.Text);
                    ctx담당자.Focus();
                    return false;
                }

                if (cbo거래구분.SelectedValue.ToString() == "" || cbo거래구분.SelectedIndex.ToString() == "")
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, lbl거래구분.Text);
                    cbo거래구분.Focus();
                    return false;
                }

                if (cbo자동입고.SelectedValue.ToString() == "Y")
                {
                    DataRow[] ldt_row = this._flexA.DataTable.Select("YN_INSP = 'Y'");
                    if (ldt_row.Length > 0)
                    {
                        ShowMessage("PU_M000126");  //품질검사 품목이 존재 하므로 자동 입고를 사용할수 없습니다.
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSaveButtonClicked(sender, e);

                if (!필수항목확인()) return;
                if (!IsChanged()) return;

                ToolBarSaveButtonEnabled = false;

                if (MsgAndSave(PageActionMode.Save))
                {
                    ShowMessage(PageResultMode.SaveGood);
                    if (!this._flexA.HasNormalRow)
                        this.OnToolBarAddButtonClicked(null, null);
                    else
                        Button_Enabled(_header.CurrentRow.Table, this._flexA.DataTable);
                }
                else
                    ToolBarSaveButtonEnabled = true;

                if (chk바코드사용여부.Checked == true)
                    txt바코드.Focus();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
                ToolBarSaveButtonEnabled = true;
            }
        }

        protected override bool SaveData()
        {
            string no_seq = txt의뢰번호.Text;
            string no_ioseq = "";

            if (!base.SaveData() || !base.Verify() || !this.BeforeSave()) return false;

            if (추가모드여부)
            {
                no_seq = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PU", "04", dtp입고일자.Text.Substring(0, 6));
                no_ioseq = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PU", "06");

                if (no_seq == "" || no_seq == null)
                    return false;

                _header.CurrentRow["NO_RCV"] = no_seq;
                _header.CurrentRow["CD_PLANT"] = D.GetString(cbo공장.SelectedValue);
                if (this._flexA.HasNormalRow)
                {
                    foreach (DataRow dr in this._flexA.DataTable.Rows)
                    {
                        if (Convert.ToDecimal(dr["QT_REQ"]) /*+ Convert.ToDecimal(this._flex[i, "QT_BAD1"])*/ == 0)
                        {
                            ShowMessage("수불 수량이 0이 있습니다.");
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
                txt의뢰번호.Text = no_seq;
            }
            else
            {
                // 추가 적용일경우...
                if (this._flexA.HasNormalRow)
                {
                    // for (int i = this._flex.Rows.Fixed; i < this._flex.Rows.Count; i++)
                    DataRow[] drs = this._flexA.DataTable.Select("ISNULL(NO_RCV,'') = '' OR ISNULL(NO_IO,'') = ''", "", DataViewRowState.Added);
                    DataRow[] drs_t = this._flexA.DataTable.Select("ISNULL(NO_IO,'') <> ''");
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

            if (_header.CurrentRow != null && this._flexA.HasNormalRow)
            {
                if (_header.CurrentRow["FG_RCV"].ToString() == "")
                    _header.CurrentRow["FG_RCV"] = this._flexA.DataTable.Rows[0]["FG_RCV"].ToString();  //헤더에 입고형태 컨트롤을 없앴으므로 그리드에 있는 값을 넣어준다.
            }

            DataTable dtH = _header.GetChanges();
            DataTable dtL = this._flexA.GetChanges();
            DataTable dtLOT = null;
            DataTable dtSERL = null;
            DataTable dtLocation = null;

            if (dtH == null && dtL == null)
            {
                ShowMessage(공통메세지.변경된내용이없습니다);
                return false;
            }

            if (dtL != null && dtL.Rows.Count > 0)
            {
                // 관리수량과 재고수량이 다른경우 
                DataRow[] drs = dtL.Select("(QT_REQ_MM * RATE_EXCHG) <> QT_REQ", "");

                if (재고단위수정여부 == "Y" && drs != null && drs.Length > 0)
                {
                    P_PU_REQCHK_SUB m_dlg = new P_PU_REQCHK_SUB(MainFrameInterface, dtL);
                    if (m_dlg.ShowDialog(this) == DialogResult.Cancel)
                        return false;

                    // 선택되지 않는것은 관리수량과 
                    DataTable _rdt = m_dlg.gdt_return;

                    if (_rdt != null && _rdt.Rows.Count > 0)
                    {
                        for (int i = 0; i < _rdt.Rows.Count; i++)
                        {
                            for (int row = 0; row < this._flexA.DataTable.Rows.Count; row++)
                            {
                                if (this._flexA.DataTable.Rows[row]["NO_RCV"].ToString() == _rdt.Rows[i]["NO_RCV"].ToString() &&
                                    this._flexA.DataTable.Rows[row]["NO_LINE"].ToString() == _rdt.Rows[i]["NO_LINE"].ToString())
                                {
                                    this._flexA.DataTable.Rows[row]["QT_REQ"] = Unit.수량(DataDictionaryTypes.PU, Convert.ToDecimal(this._flexA.DataTable.Rows[row]["QT_REQ_MM"]) * Convert.ToDecimal(this._flexA.DataTable.Rows[row]["RATE_EXCHG"]));
                                    break;
                                }
                            }

                        }
                        dtL = this._flexA.GetChanges();
                    }

                }

                // m_sPJT재고사용 관련
                // PJT재고를 표현하기 어려 LOT재고로 대치 NO_LOT에 CD_PJT넣어 관리
                // 김광석, 김형석
                if (String.Compare(LOT사용여부, "Y") == 0 && dtL != null)
                {
                    DataTable _dtLOT = dtL.Clone();
                    _dtLOT = new DataView(dtL, "NO_LOT = 'YES'", "", DataViewRowState.CurrentRows).ToTable();
                    if (_dtLOT.Rows.Count > 0)
                    {
                        if (프로젝트재고사용여부 == "100" && 프로젝트사용여부)
                        {
                            dtLOT = _dtLOT;
                            dtLOT.Columns.Add("FG_PS", typeof(string), "1");
                            dtLOT.Columns.Add("QT_IO", typeof(decimal));
                            dtLOT.Columns.Add("NO_IOLINE2", typeof(decimal), "0");
                            dtLOT.Columns.Add("DC_LOTRMK", typeof(string), "");
                            dtLOT.Columns.Remove("YN_RETURN");
                            foreach (DataRow dr_lot in dtLOT.Rows)
                            {
                                dr_lot["NO_LOT"] = dr_lot["CD_PJT"];
                                dr_lot["QT_IO"] = dr_lot["QT_REQ"];
                            }
                        }
                        else
                        {
                            P_PU_LOT_SUB_R m_dlg;
                            m_dlg = new P_PU_LOT_SUB_R(_dtLOT);

                            if (m_dlg.ShowDialog(this) == DialogResult.OK)
                                dtLOT = m_dlg.dtL;
                            else
                                return false;
                        }
                    }
                }

                #region 시리얼 추가
                if (String.Compare(Serial사용여부, "Y") == 0 && dtL != null)
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
                            return false;
                        }
                    }
                }
                #endregion
                
                #region LOCATION 등록
                if (Config.MA_ENV.YN_LOCATION == "Y") //시스템환경설정에서 LOCATION사용인것만 창고별로 사용인지 아닌지는 도움창 호출후 판단한다. 붙여야하는화면이 많은 관계로 여기서 통합으로 처리해주는걸로 판단함
                {
                    bool b_lct = false;
                    DataTable dt_lc = dtL.Clone().Copy();
                    foreach (DataRow dr in dtL.Select())
                        dt_lc.LoadDataRow(dr.ItemArray, true);

                    if (dt_lc.Rows.Count > 0)
                    {
                        dtLocation = P_OPEN_SUBWINDOWS.P_MA_LOCATION_R_SUB(dt_lc, out b_lct);

                        if (b_lct == false)
                            return false;
                    }
                }
                #endregion
            }
            
            //외주일경우 자재출고
            DataTable dtHH = null;
            DataTable dtLL = null;

            if (외주유무 == "100")
            {
                dtLL = this._flexB.GetChanges();
                string NO_IO = string.Empty;
                string NO_IO_MGMT = string.Empty;

                if (dtLL != null && dtLL.Rows.Count != 0)
                {
                    if (추가모드여부)
                    {
                        NO_IO = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PU", "15", dtp입고일자.Text.Substring(0, 6));
                        NO_IO_MGMT = no_ioseq;

                    }
                    else
                    {
                        NO_IO_MGMT = _biz.GetNO_IO_MGMT(txt의뢰번호.Text);
                        NO_IO = _biz.GetNO_IO(NO_IO_MGMT);

                        if (NO_IO == string.Empty)
                        {
                            NO_IO = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PU", "15", dtp입고일자.Text.Substring(0, 6));
                        }
                    }
                    foreach (DataRow dr in dtLL.Rows)
                    {
                        if (D.GetString(dr["CD_SL"]) == string.Empty)
                        {
                            ShowMessage(공통메세지._은는필수입력항목입니다, this.DD("창고"));
                            return false;
                        }

                        dr["NO_IO"] = NO_IO;
                        dr["NO_IO_MGMT"] = NO_IO_MGMT;
                    }


                    DataTable dtSL = new DataView(dtLL, "YN_PARTNER_SL = 'Y'", "", DataViewRowState.CurrentRows).ToTable(true, new string[] { "CD_PLANT", "CD_PARTNER", "CD_SL" });

                    foreach (DataRow drSL in dtSL.Rows)
                    {
                        string CD_SL = _biz.getCD_SL(D.GetString(drSL["CD_PARTNER"]), D.GetString(drSL["CD_PLANT"]));

                        if (D.GetString(drSL["CD_SL"]) != CD_SL)
                        {
                            ShowMessage(공통메세지._와_은같아야합니다, new string[] { this.DD("출고창고"), this.DD("외주매입처별창고") });
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

            bool bSuccess = _biz.Save(dtH, dtL, dtLOT, no_ioseq, dtSERL, D.GetString(cbo공장.SelectedValue), D.GetString(txt의뢰번호.Text), dtLocation, 특채수량사용여부, dtHH, dtLL);

            if (!bSuccess)
                return false;

            _header.AcceptChanges();
            this._flexA.AcceptChanges();

            if (외주유무 == "100")
            {
                this._flexB.AcceptChanges();
            }

            if (this._flexA.HasNormalRow)
                txt입고번호.Text = D.GetString(this._flexA.DataTable.Rows[0]["NO_IO"]); //그리드 row가 존재할때 no_io를 컨트롤로 넣어준다
            
            btnLocalLC.Enabled = false;
            btn통관적용.Enabled = false;

            ControlEnabledDisable(false);
            return true;
        }

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {

            try
            {
                base.OnToolBarPrintButtonClicked(sender, e);

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
            if (!this._flexA.HasNormalRow)
            {
                ShowMessage(공통메세지._이가존재하지않습니다, this.DD("데이터"));
                return;
            }

            ReportHelper rptHelper = new ReportHelper("R_P_PU_REQ_REG_1", "구매입고처리");

            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic["의뢰번호"] = D.GetString(this._flexA["NO_RCV"]);
            dic["입고일자"] = D.GetString(dtp입고일자.Text);
            dic["매입처코드"] = D.GetString(ctx매입처.CodeValue);
            dic["매입처명"] = D.GetString(ctx매입처.CodeName);
            dic["담당자코드"] = D.GetString(ctx담당자.CodeValue);
            dic["담당자명"] = D.GetString(ctx담당자.CodeName);
            dic["비고"] = D.GetString(_header.CurrentRow["DC_RMK"].ToString());
            dic["거래구분"] = D.GetString(_header.CurrentRow["NM_EXCH"].ToString());
            dic["입고번호"] = D.GetString(this._flexA["NO_IO"]);

            foreach (string key in dic.Keys)
            {
                rptHelper.SetData(key, dic[key]);
            }
            rptHelper.SetDataTable(this._flexA.DataTable);
            rptHelper.가로출력();


            if (check)
                rptHelper.Print();
            else
            {
                StringBuilder text = new StringBuilder();
                string title = D.GetString(ctx담당자.CodeName) + "/" + D.GetString(txt의뢰번호.Text) + "/" + D.GetString(this._flexA[this._flexA.Rows.Fixed, "CD_PJT"]) + "의 구매입고가 등록되었습니다.";
                string msg = string.Empty;
                DataTable dt = null;

                // 메일도움창에 보낼 파라미터셋팅 0번 제목, 1번 받을사람 이메일주소, 3번은 내용
                string[] str_histext = new string[3];

                foreach (DataRow dr in this._flexA.DataTable.Rows)
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

                DataTable groupby_no_po = this._flexA.DataTable.DefaultView.ToTable(true, new string[] { "NO_PO" }); //여러 발주건이 있을 수 있기 때문에...
                dt = _biz.GetMailAdress(groupby_no_po);

                if (dt != null && dt.Rows.Count != 0)
                {
                    DataTable dt_emp = dt.DefaultView.ToTable(true, new string[] { "NO_EMP", "NM_KOR", "NO_EMAIL" }); //여러 발주건에 담당자가 같을 수도 있기때문에.. 
                    foreach (DataRow dr in dt_emp.Rows)
                    {
                        str_histext[1] += D.GetString(dr["NM_KOR"]) + "|" + D.GetString(dr["NO_EMAIL"]) + "|" + "N" + "?"; //담당자는 저장 하지 않기위해 N을 붙여줌.
                    }
                }

                P_MF_EMAIL mail = new P_MF_EMAIL(new string[] { D.GetString(ctx매입처.CodeValue) }, "R_P_PU_REQ_REG_1", new ReportHelper[] { rptHelper }, dic, "구매입고처리", str_histext);
                mail.ShowDialog();
                Settings1.Default.email_add = mail._str_rt_data[0];
                Settings1.Default.Save();
            }
        }

        public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
        {
            try
            {
                Settings1.Default.cd_sl_apply = ctx창고.CodeValue;
                Settings1.Default.nm_sl_apply = ctx창고.CodeName;
                Settings1.Default.Save();
            }
            catch (Exception Ex)
            {
                this.MsgEnd(Ex);
            }

            return base.OnToolBarExitButtonClicked(sender, e);
        }

        #endregion

        #region ♣ 주요 버튼 이벤트

        private void btn발주적용_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (!적용버튼필수항목확인())
                    return;

                string FG_TRANS = cbo거래구분.SelectedValue.ToString();			// 거래구분

                string CD_PLANT = D.GetString(cbo공장.SelectedValue);
                string CD_PARTNER = ctx매입처.CodeValue;
                string NM_PARTNER = ctx매입처.CodeName;
                P_PU_REQPO_SUB dlg_PuRcvSub = null;

                //0:FG_TRANS,1:CD_PLANT,2:CD_PARTNER,3:NM_PARTNER,4:NO_EMP,5:NM_KOR,6:DT_FROM
                dlg_PuRcvSub = new P_PU_REQPO_SUB(new object[] { (int)P_PU_REQPO_SUB.search_type.FG_TRANS ,
                                                                    (int)P_PU_REQPO_SUB.search_type.CD_PLANT ,
                                                                    (int)P_PU_REQPO_SUB.search_type.CD_PARTNER ,
                                                                    (int)P_PU_REQPO_SUB.search_type.NM_PARTNER ,
                                                                    (int)P_PU_REQPO_SUB.search_type.DT_FROM,
                                                                    (int)P_PU_REQPO_SUB.search_type.DATATABLE },
                                                    new object[] {  FG_TRANS, 
                                                                        CD_PLANT, 
                                                                        CD_PARTNER, 
                                                                        NM_PARTNER,
                                                                        "",
                                                                        this._flexA.DataTable});

                btn통관적용.Enabled = false;

                if (dlg_PuRcvSub.ShowDialog(this) == DialogResult.OK)
                {
                    cbo거래구분.Enabled = false;      // 거래구분을 수정할 수 없게

                    DataTable dt = this._flexA.DataTable.Clone();

                    foreach (DataColumn col in dt.Columns)
                    {
                        if (col.DataType == typeof(decimal))
                            col.DefaultValue = 0;
                    }
                    발주데이터추가(dlg_PuRcvSub.gdt_return);
                    ControlEnabledDisable(false);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn통관적용_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (!적용버튼필수항목확인())
                    return;


                trade.P_TR_REQLC_SUB dlg_PuRcvSub = new trade.P_TR_REQLC_SUB(D.GetString(cbo공장.SelectedValue), cbo공장.Text,
                              ctx매입처.CodeValue.ToString(), ctx매입처.CodeName, cbo거래구분.SelectedValue.ToString());

                if (dlg_PuRcvSub.ShowDialog(this) == DialogResult.OK)
                {
                    통관데이터추가(dlg_PuRcvSub.통관적용dt, dlg_PuRcvSub.check_app);
                    ToolBarDeleteButtonEnabled = false;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btnLocalLC_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (!적용버튼필수항목확인())
                    return;

                DataTable gdt_ReqDataREQ = null;
                P_PU_REQLC_SUB dlg_PuLCSub = new P_PU_REQLC_SUB(MainFrameInterface, gdt_ReqDataREQ, D.GetString(cbo공장.SelectedValue));//tb_NM_PARTNER.Tag.ToString(), tb_NM_PARTNER.Text);
                dlg_PuLCSub.g_cdPartner = ctx매입처.CodeValue.ToString();
                dlg_PuLCSub.g_nmPartner = ctx매입처.CodeName;

                if (dlg_PuLCSub.ShowDialog(this) == DialogResult.OK)
                {
                    LC데이터추가(dlg_PuLCSub.gdt_return);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn가입고적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!적용버튼필수항목확인()) return;

                string CD_PARTNER = ctx매입처.CodeValue;
                string NM_PARTNER = ctx매입처.CodeName;
                string FG_TRANS = cbo거래구분.SelectedValue.ToString();
                P_PU_REV_SUB dlg_PuRevSub = new P_PU_REV_SUB(D.GetString(cbo공장.SelectedValue), CD_PARTNER, NM_PARTNER, this._flexA.DataTable, FG_TRANS, D.GetString(cboLC.SelectedValue));

                this.btn통관적용.Enabled = false;

                if (dlg_PuRevSub.ShowDialog(this) == DialogResult.OK)
                {
                    // 거래구분을 수정할 수 없게
                    this.cbo거래구분.Enabled = false;

                    DataTable dt = this._flexA.DataTable.Clone();

                    foreach (DataColumn col in dt.Columns)
                    {
                        if (col.DataType == typeof(decimal))
                            col.DefaultValue = 0;
                    }

                    가입고데이터추가(dlg_PuRevSub.gdt_return);
                    ToolBarDeleteButtonEnabled = false;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn수입검사적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!적용버튼필수항목확인()) return;

                string CD_PLANT = D.GetString(cbo공장.SelectedValue);
                string CD_PARTNER = ctx매입처.CodeValue;
                string NM_PARTNER = ctx매입처.CodeName;
                string FG_TRANS = cbo거래구분.SelectedValue.ToString();
                P_PU_REQ_IQC_SUB dlg_IQC = new P_PU_REQ_IQC_SUB(CD_PLANT, CD_PARTNER, NM_PARTNER, this._flexA.DataTable, FG_TRANS, D.GetString(cboLC.SelectedValue));

                if (dlg_IQC.ShowDialog(this) == DialogResult.OK)
                {
                    cbo거래구분.Enabled = false;      // 거래구분을 수정할 수 없게

                    DataTable dt = this._flexA.DataTable.Clone();

                    foreach (DataColumn col in dt.Columns)
                    {
                        if (col.DataType == typeof(decimal))
                            col.DefaultValue = 0;
                    }

                    가입고데이터추가(dlg_IQC.gdt_return);
                    ControlEnabledDisable(false);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void txt바코드_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (this._flexA.HasNormalRow)
                    return;

                if (!chk바코드사용여부.Checked)
                {
                    ShowMessage("바코드사용여부를 체크 해주시기 바랍니다.");
                    return;
                }

                if (D.GetString(txt바코드.Text) != string.Empty && e.KeyChar == (char)Keys.Enter)
                {

                    if (!적용버튼필수항목확인())
                    {
                        txt바코드.Text = string.Empty;
                        return;
                    }

                    string[] str_barcode = txt바코드.Text.Split('+');

                    if (str_barcode.Length != 2) return;

                    object[] obj = new object[]
                         {
                           Global.MainFrame.LoginInfo.CompanyCode,
                           cbo거래구분.SelectedValue,
                           cboLC.SelectedValue,
                           D.GetString(str_barcode[1])
                         };
                    DataTable dt = _biz.SearchBarcodeRev(obj);

                    if (dt == null || dt.Rows.Count == 0)
                    {
                        ShowMessage("해당가입고번호가 없거나 거래구분, L/C기준이 잘못지정되었습니다.");
                        return;
                    }

                    바코드데이터추가(dt);

                    btn통관적용.Enabled = false;


                    // 거래구분을 수정할 수 없게
                    cbo거래구분.Enabled = false;

                    ToolBarDeleteButtonEnabled = false;

                    if (chk바코드사용여부.Checked == true)
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

        private void btn메일전송_Click(object sender, EventArgs e)
        {
            try
            {
                if (D.GetString(txt의뢰번호.Text) == string.Empty) return;

                SetPrint(false);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn삭제_Click(object sender, EventArgs e)
        {

            if (!this._flexA.HasNormalRow) return;

            if (this._flexA.CDecimal(this._flexA["QT_CLS"]) != 0)
            {
                ShowMessage("마감된 데이터입니다. 삭제할 수 없습니다.");
                return;
            }
            string NO_PO = D.GetString(this._flexA["NO_PO"]);
            string NO_POLINE = D.GetString(this._flexA["NO_POLINE"]);

            if (외주유무 == "100")
            {
                DataRow[] dr = this._flexB.DataTable.Select("NO_PO ='" + NO_PO + "' AND NO_POLINE='" + NO_POLINE + "'");
                foreach (DataRow drD in dr)
                {
                    drD.Delete();
                }
            }

            this._flexA.Rows.Remove(this._flexA.Row);

            if (!this._flexA.HasNormalRow)  //지운후 다 없어지면 추가모드
            {
                ControlEnabledDisable(true);
            }

            ToolBarSaveButtonEnabled = true;
        }

        private bool 적용버튼필수항목확인()
        {
            //공장명
            if (cbo공장.SelectedValue == null || D.GetString(cbo공장.SelectedValue) == "")
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, lbl공장.Text);
                return false;
            }

            //담당자
            if (ctx담당자.CodeValue.ToString() == "")
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, lbl담당자.Text);
                return false;
            }
            return true;
        }

        private void 발주데이터추가(DataTable dt)
        {
            try
            {
                if (dt == null || dt.Rows.Count <= 0)
                    return;

                DataRow row;
                decimal max_no_line = this._flexA.GetMaxValue("NO_LINE");
                this._flexA.Redraw = false;
                string NO_PO_MULTI = string.Empty;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    row = dt.Rows[i];
                    max_no_line++;
                    this._flexA.Rows.Add();
                    this._flexA.Row = this._flexA.Rows.Count - 1;

                    this._flexA["NO_LINE"] = max_no_line;
                    this._flexA["NO_IOLINE"] = max_no_line;
                    this._flexA["CD_ITEM"] = row["CD_ITEM"];						//품목코드
                    this._flexA["NM_ITEM"] = row["NM_ITEM"];						//품목명
                    this._flexA["STND_ITEM"] = row["STND_ITEM"];					//규격

                    if (row["CD_UNIT_MM"] == null)
                        row["CD_UNIT_MM"] = "";
                    this._flexA["CD_UNIT_MM"] = row["CD_UNIT_MM"];				//수배단위
                    this._flexA["UNIT_IM"] = row["UNIT_IM"];						//재고단위
                    this._flexA["RATE_EXCHG"] = row["RATE_EXCHG"];				//단위환산량으로 수배단위, 재고단위에 의해서 결정된다.	
                    this._flexA["RT_VAT"] = row["RT_VAT"];
                    this._flexA["DT_LIMIT"] = row["DT_LIMIT"];					//납기일

                    if (row["CD_ZONE"] == null)
                        row["CD_ZONE"] = "";
                    this._flexA["CD_ZONE"] = row["CD_ZONE"];				//저장위치

                    this._flexA["QT_REQ_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ_MM"]));				    //수배수량
                    this._flexA["QT_REAL"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ_MM"]));
                    this._flexA["QT_REQ"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ"]));
                    this._flexA["QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ"]));					//의뢰량

                    this._flexA["QT_PASS"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ"]));                      //합격수량(재고단위)
                    this._flexA["QT_REJECTION"] = 0;                             //불량수량(재고단위)

                    this._flexA["YN_INSP"] = "N";

                    if (row["FG_IQC"].ToString() == "Y")
                    {
                        this._flexA["YN_INSP"] = "Y";
                    }

                    this._flexA["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX_PO"]));								//발주단가
                    this._flexA["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX"]));									//단가<<--발주단가						
                    this._flexA["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM_EX"]));
                    this._flexA["CD_PJT"] = row["CD_PJT"];						//프로젝트코드
                    this._flexA["CD_PURGRP"] = row["CD_PURGRP"];
                    this._flexA["NO_PO"] = row["NO_PO"];
                    this._flexA["NO_POLINE"] = row["NO_LINE"];
                    this._flexA["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM"]));

                    this._flexA["AM"] = Unit.원화금액(DataDictionaryTypes.PU, this._flexA.CDecimal(row["JAN_AM"]));

                    this._flexA["VAT"] = Unit.원화금액(DataDictionaryTypes.PU, this._flexA.CDecimal(row["VAT"].ToString()));

                    this._flexA["QT_GR_MM"] = 0;
                    this._flexA["RT_CUSTOMS"] = 0;
                    this._flexA["YN_RETURN"] = row["YN_RETURN"];
                    _header.CurrentRow["YN_RETURN"] = row["YN_RETURN"];              //헤더에 입력해줘야한다!
                    this._flexA["FG_TPPURCHASE"] = row["FG_PURCHASE"];
                    this._flexA["YN_PURCHASE"] = (row["FG_PURCHASE"].ToString() != "") ? "Y" : "N";

                    this._flexA["FG_POST"] = row["FG_POST"];			//발주상태 코드
                    this._flexA["NM_FG_POST"] = row["NM_FG_POST"];		//발주상태 명
                    this._flexA["FG_RCV"] = row["FG_RCV"];				//발주의 입고형태 코드
                    this._flexA["NM_FG_RCV"] = row["NM_QTIOTP"];		//발주의 입고형태 명
                    this._flexA["FG_TRANS"] = row["FG_TRANS"];			//거래구분
                    this._flexA["FG_TAX"] = row["FG_TAX"];				//과세구분
                    this._flexA["CD_EXCH"] = row["CD_EXCH"];			//환종
                    _header.CurrentRow["CD_EXCH"] = row["CD_EXCH"];

                    if (D.GetDecimal(row["RT_EXCH"]) == 0 && D.GetString(row["CD_EXCH"]) == "000")
                    {
                        this._flexA["RT_EXCH"] = 1;			//환율
                    }
                    else
                    {
                        this._flexA["RT_EXCH"] = D.GetDecimal(row["RT_EXCH"]);
                    }

                    this._flexA["NO_IO_MGMT"] = "";
                    this._flexA["NO_IOLINE_MGMT"] = 0;
                    this._flexA["NO_PO_MGMT"] = "";
                    this._flexA["NO_POLINE_MGMT"] = 0;

                    this._flexA["NO_TO"] = "";										//통관번호
                    this._flexA["NO_TO_LINE"] = 0;									//통관항번

                    this._flexA["CD_SL"] = row["CD_SL"];
                    this._flexA["NM_SL"] = row["NM_SL"];

                    this._flexA["NO_LC"] = "";
                    this._flexA["NO_LCLINE"] = 0;
                    this._flexA["FG_TAXP"] = row["FG_TAXP"];
                    this._flexA["NO_EMP"] = D.GetString(ctx담당자.CodeValue);
                    this._flexA["NM_PROJECT"] = row["NM_PROJECT"];
                    this._flexA["YN_AM"] = row["YN_AM"];
                    _header.CurrentRow["YN_AM"] = row["YN_AM"];              //헤더에 입력해줘야한다!
                    this._flexA["VAT_CLS"] = 0;
                    this._flexA["YN_AUTORCV"] = row["YN_AUTORCV"];
                    this._flexA["YN_REQ"] = row["YN_RCV"];

                    this._flexA["AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM_EX"]));

                    this._flexA["AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, this._flexA.CDecimal(row["JAN_AM"]));

                    this._flexA["AM_TOTAL"] = Unit.원화금액(DataDictionaryTypes.PU, this._flexA.CDecimal(row["JAN_AM"]) + this._flexA.CDecimal(row["VAT"])); //총금액

                    this._flexA["AM_EXRCV"] = 0;
                    this._flexA["AM_RCV"] = 0;

                    this._flexA["NM_SYSDEF"] = row["NM_SYSDEF"];
                    this._flexA["NM_KOR"] = row["NM_KOR"];
                    this._flexA["NO_LOT"] = row["NO_LOT"];

                    this._flexA["CD_PLANT"] = _header.CurrentRow["CD_PLANT"].ToString();

                    if (row["CD_EXCH"].ToString() != "")
                    {
                        txt통화명.Text = row["NM_SYSDEF"].ToString();
                    }

                    this._flexA["TP_UM_TAX"] = row["TP_UM_TAX"];
                    this._flexA["PO_PRICE"] = row["PO_PRICE"]; //추가 20090226 (구매그룹별 단가통제 chk용)
                    this._flexA["NM_PURGRP"] = row["NM_PURGRP"];

                    this._flexA["NO_SERL"] = row["NO_SERL"];
                    this._flexA["DC_RMK"] = row["DC1"];       //추가 2010.04.12

                    if (BASIC.GetMAEXC("구매입고처리-업체별프로세스선택") == "100")  //YTN전용 발주적용밖에 쓰지않음 이형준대리요청
                        this._flexA["DC_RMK2"] = D.GetString(txt관리번호.Text); //의뢰단까지만 저장하도록할것입니다. 수불테이블까지는 안들어가도됩니다. 이형준대리요청
                    else
                        this._flexA["DC_RMK2"] = row["DC2"];

                    // 2011-06-03, 최승애 , PIMS : M20110519153
                    this._flexA["REV_QT_REQ_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ_MM"]));             // 가입고 적용시 의뢰량 2011-06-03, 최승애 추가
                    this._flexA["REV_AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM_EX"]));          // 가입고 적용시 외화금액 2011-06-03, 최승애 추가
                    this._flexA["REV_AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, this._flexA.CDecimal(row["JAN_AM"]));             // 가입고 적용시 원화금액 2011-06-03, 최승애 추가


                    if (Config.MA_ENV.PJT형여부 == "Y")
                    {
                        this._flexA["CD_PJT_ITEM"] = row["CD_PJT_ITEM"];
                        this._flexA["NM_PJT_ITEM"] = row["NM_PJT_ITEM"];
                        this._flexA["PJT_ITEM_STND"] = row["PJT_ITEM_STND"];
                        this._flexA["NO_WBS"] = row["NO_WBS"];
                        this._flexA["NO_CBS"] = row["NO_CBS"];
                        this._flexA["SEQ_PROJECT"] = D.GetDecimal(row["SEQ_PROJECT"]);
                    }

                    if (dt.Columns.Contains("CD_ITEM_ORIGIN"))
                    {
                        this._flexA["CD_ITEM_ORIGIN"] = row["CD_ITEM_ORIGIN"];
                        this._flexA["NM_ITEM_ORIGIN"] = row["NM_ITEM_ORIGIN"];
                        this._flexA["STND_ITEM_ORIGIN"] = row["STND_ITEM_ORIGIN"];
                    }

                    this._flexA["GI_PARTNER"] = D.GetString(row["GI_PARTNER"]);
                    this._flexA["NM_GI_PARTER"] = row["NM_GI_PARTER"];

                    this._flexA["PI_PARTNER"] = D.GetString(row["PI_PARTNER"]);
                    this._flexA["PI_LN_PARTNER"] = row["PI_LN_PARTNER"];

                    NO_PO_MULTI += D.GetString(row["NO_PO"]) + D.GetString(row["NO_LINE"]) + "|";

                    if (dt.Columns.Contains("DT_PLAN"))   //2013.07.30 납기예정일추가
                    {
                        this._flexA["DT_PLAN"] = D.GetString(row["DT_PLAN"]);
                    }

                    this._flexA["CLS_ITEM"] = row["CLS_ITEM"];

                    if (규격형사용유무 == "100")
                    {
                        this._flexA["UM_WEIGHT"] = row["UM_WEIGHT"];
                        this._flexA["TOT_WEIGHT"] = row["TOT_WEIGHT"];
                        this._flexA["WEIGHT"] = row["WEIGHT"];
                    }

                    this._flexA["MAT_ITEM"] = row["MAT_ITEM"];

                }
  
                _header.CurrentRow["CD_PARTNER"] = dt.Rows[0]["CD_PARTNER"];
                ctx매입처.SetCode(dt.Rows[0]["CD_PARTNER"].ToString(), dt.Rows[0]["LN_PARTNER"].ToString());

                this._flexA.Redraw = true;

                if (외주유무 == "100")
                {
                    SetflexD(NO_PO_MULTI);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void 통관데이터추가(DataTable dt, bool check)
        {
            try
            {
                if (dt == null || dt.Rows.Count <= 0)
                    return;

                DataRow row;
                decimal max_no_line = this._flexA.GetMaxValue("NO_LINE");
                this._flexA.Redraw = false;
                string NO_PO_MULTI = string.Empty;


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    row = dt.Rows[i];
                    max_no_line++;
                    this._flexA.Rows.Add();
                    this._flexA.Row = this._flexA.Rows.Count - 1;

                    this._flexA["NO_RCV"] = txt의뢰번호.Text;

                    this._flexA["NO_LINE"] = max_no_line;
                    this._flexA["NO_IOLINE"] = max_no_line;
                    this._flexA["NO_LOT"] = row["NO_LOT"].ToString();

                    this._flexA["CD_ITEM"] = row["CD_ITEM"].ToString();						//품목코드
                    this._flexA["NM_ITEM"] = row["NM_ITEM"].ToString();						//품목명
                    this._flexA["STND_ITEM"] = row["STND_ITEM"].ToString();
                    if (row["CD_UNIT_MM"].ToString() == null)
                        row["CD_UNIT_MM"] = "";				//규격
                    this._flexA["CD_UNIT_MM"] = row["CD_UNIT_MM"].ToString();					//수배단위
                    this._flexA["UNIT_IM"] = row["UNIT_IM"].ToString();						//재고단위
                    if (row["RATE_EXCHG"].ToString() == null || this._flexA["RATE_EXCHG"].ToString() == "0")
                        this._flexA["RATE_EXCHG"] = "1";
                    else
                        this._flexA["RATE_EXCHG"] = row["RATE_EXCHG"];

                    if (BASIC.GetMAEXC_Menu("P_PU_REQ_REG_1", "PU_A00000007") == "100")
                        this._flexA["DT_LIMIT"] = row["DT_LIMIT"];					//납기일
                    else
                        this._flexA["DT_LIMIT"] = row["DT_TO"];

                    if (row["CD_ZONE"] == null) row["CD_ZONE"] = "";
                    this._flexA["CD_ZONE"] = row["CD_ZONE"];				//저장위치

                    this._flexA["YN_INSP"] = "N";

                    if (row["FG_IQC"].ToString() != "")
                    {
                        if (row["FG_IQC"].ToString() == "Y")
                        {
                            this._flexA["YN_INSP"] = "Y";
                        }
                        else
                            this._flexA["YN_INSP"] = "N";
                    }

                    this._flexA["QT_REQ"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ"]));
                    this._flexA["QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ"]));								//통관수량(관리수량)
                    this._flexA["QT_REQ_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ_MM"]));								//통관수배수량

                    this._flexA["QT_PASS"] = 0;
                    this._flexA["QT_REJECTION"] = 0;
                    this._flexA["QT_GR"] = 0;
                    this._flexA["QT_GR_MM"] = 0;

                    this._flexA["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX_PO"]));									//발주단가
                    this._flexA["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX"]));										//단가<<--발주단가						
                    this._flexA["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["AM_EX"]));
                    this._flexA["CD_PJT"] = row["CD_PJT"].ToString();						//프로젝트코드
                    this._flexA["CD_PURGRP"] = row["CD_PURGRP"].ToString();
                    this._flexA["NO_PO"] = "";
                    this._flexA["NO_POLINE"] = 0;

                    this._flexA["AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(row["AM_TO"]));

                    this._flexA["UM"] = (this._flexA.CDecimal(row["QT_REQ"]) == 0) ? 0 : Unit.원화단가(DataDictionaryTypes.PU, this._flexA.CDecimal(row["AM_TO"]) / this._flexA.CDecimal(row["QT_REQ"]));

                    this._flexA["VAT"] = 0;
                    this._flexA["RT_CUSTOMS"] = 0;
                    this._flexA["YN_RETURN"] = "N";
                    _header.CurrentRow["YN_RETURN"] = "N";  //헤더에 입력해줘야한다!

                    this._flexA["FG_TPPURCHASE"] = row["FG_TPPURCHASE"].ToString();

                    if (row["FG_TPPURCHASE"].ToString() != string.Empty)
                    {
                        this._flexA["YN_PURCHASE"] = "Y";
                    }
                    else
                    {
                        this._flexA["YN_PURCHASE"] = "N";
                    }

                    this._flexA["FG_POST"] = "";										//발주상태 코드
                    this._flexA["NM_FG_POST"] = "";									//발주상태 명
                    this._flexA["FG_RCV"] = row["CD_QTIOTP"].ToString();				//발주상태 명
                    this._flexA["NM_FG_RCV"] = row["NM_QTIOTP"].ToString();		//입고형태 명
                    this._flexA["FG_TRANS"] = row["FG_LC"].ToString();				//거래구분
                    this._flexA["FG_TAX"] = "23";//row["FG_TAX"].ToString();				//과세구분
                    this._flexA["CD_EXCH"] = row["CD_EXCH"].ToString();				//환종
                    _header.CurrentRow["CD_EXCH"] = row["CD_EXCH"].ToString();
                    this._flexA["RT_EXCH"] = row["RT_EXCH_BL"].ToString();				//환율 BL 환율 적용한다.
                    this._flexA["NO_SERL"] = row["NO_SERL"];
                    this._flexA["NO_IO_MGMT"] = "";
                    this._flexA["NO_IOLINE_MGMT"] = 0;

                    this._flexA["NO_PO"] = row["NO_PO"].ToString();				//관련발주번호	
                    this._flexA["NO_POLINE"] = row["NO_POLINE"].ToString();		//관련발주항번

                    this._flexA["NO_TO"] = row["NO_TO"].ToString();					//통관번호
                    this._flexA["NO_TO_LINE"] = row["NO_LINE"].ToString();				//통관항번

                    this._flexA["NO_LC"] = row["NO_LC"].ToString();
                    this._flexA["NO_LCLINE"] = row["NO_LCLINE"].ToString();

                    this._flexA["CD_SL"] = row["CD_SL"].ToString();	//tb_NM_SL.Tag.ToString();		
                    //20031118
                    this._flexA["NM_SL"] = row["NM_SL"].ToString();

                    this._flexA["FG_TAXP"] = "001";//일괄
                    this._flexA["NO_EMP"] = "";//row["NO_EMP"];					
                    this._flexA["NM_PROJECT"] = row["NM_PROJECT"].ToString();
                    this._flexA["YN_AM"] = this._flexA["YN_PURCHASE"].ToString();
                    _header.CurrentRow["YN_AM"] = this._flexA["YN_PURCHASE"].ToString();              //헤더에 입력해줘야한다!

                    this._flexA["VAT_CLS"] = 0;
                    this._flexA["YN_AUTORCV"] = row["YN_AUTORCV"].ToString();
                    this._flexA["YN_REQ"] = "Y";

                    // 0314 : 발주금액, 원화금액 -->> 의뢰에 반영
                    this._flexA["AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["AM_EX_TO"].ToString()));

                    this._flexA["AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(row["AM_TO"].ToString()));

                    this._flexA["AM_EXRCV"] = 0;
                    this._flexA["AM_RCV"] = 0;

                    this._flexA["NO_BL"] = row["NO_BL"];
                    this._flexA["NO_BLLINE"] = row["NO_BLLINE"];

                    this._flexA["NM_SYSDEF"] = row["NM_SYSDEF"].ToString();

                    this._flexA["AM_TOTAL"] = Unit.원화금액(DataDictionaryTypes.PU, this._flexA.CDecimal(this._flexA["AM_REQ"]) + this._flexA.CDecimal(this._flexA["VAT"])); //총금액

                    if (row["CD_EXCH"].ToString() != "")
                    {
                        txt통화명.Text = row["NM_SYSDEF"].ToString();
                    }

                    if (this._flexA["RT_SPEC"] == System.DBNull.Value)
                    {
                        if (this._flexA.DataTable.Columns["RT_SPEC"].DataType.FullName == "System.String")
                            this._flexA["RT_SPEC"] = string.Empty;
                        else
                            this._flexA["RT_SPEC"] = 0;

                    }

                    if (this._flexA["DC_RMK"] == System.DBNull.Value)
                        this._flexA["DC_RMK"] = string.Empty;

                    this._flexA["CD_PLANT"] = _header.CurrentRow["CD_PLANT"].ToString();

                    // 2011-06-03, 최승애 , PIMS : M20110519153
                    this._flexA["REV_QT_REQ_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ"]));                // 가입고 적용시 의뢰량 2011-06-03, 최승애 추가
                    this._flexA["REV_AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["AM_EX_TO"].ToString()));   // 가입고 적용시 외화금액 2011-06-03, 최승애 추가
                    this._flexA["REV_AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(row["AM_TO"].ToString()));    // 가입고 적용시 원화금액 2011-06-03, 최승애 추가
                    this._flexA["TP_UM_TAX"] = row["TP_UM_TAX"];

                    this._flexA["GI_PARTNER"] = row["GI_PARTNER"];
                    this._flexA["NM_GI_PARTER"] = row["NM_GI_PARTER"];

                    if (Config.MA_ENV.PJT형여부 == "Y")
                    {
                        this._flexA["CD_PJT_ITEM"] = row["CD_PJT_ITEM"];
                        this._flexA["NM_PJT_ITEM"] = row["NM_PJT_ITEM"];
                        this._flexA["PJT_ITEM_STND"] = row["PJT_ITEM_STND"];
                        this._flexA["SEQ_PROJECT"] = D.GetDecimal(row["SEQ_PROJECT"]);
                    }

                    this._flexA["NO_SERL"] = row["NO_SERL"];

                    if (dt.Columns.Contains("DT_PLAN"))   //2013.07.30 납기예정일추가
                    {
                        this._flexA["DT_PLAN"] = row["DT_PLAN"];
                    }
                    this._flexA["CLS_ITEM"] = row["CLS_ITEM"];
                    NO_PO_MULTI += D.GetString(row["NO_PO"]) + D.GetString(row["NO_POLINE"]) + "|";

                    if (check)
                    {
                        this._flexA["DC_RMK"] = row["DC1"].ToString();
                        this._flexA["DC_RMK2"] = row["DC2"].ToString();
                    }

                    this._flexA["MAT_ITEM"] = row["MAT_ITEM"];

                }
  
                _header.CurrentRow["CD_PARTNER"] = dt.Rows[0]["CD_PARTNER"];
                ctx매입처.SetCode(dt.Rows[0]["CD_PARTNER"].ToString(), dt.Rows[0]["LN_PARTNER"].ToString());

                this._flexA.Redraw = true;

                if (외주유무 == "100")
                {
                    SetflexD(NO_PO_MULTI);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void LC데이터추가(DataTable dt)
        {
            try
            {
                if (dt == null || dt.Rows.Count <= 0)
                    return;

                DataRow row;
                decimal max_no_line = this._flexA.GetMaxValue("NO_LINE");
                this._flexA.Redraw = false;
                string NO_PO_MULTI = string.Empty;


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    row = dt.Rows[i];
                    max_no_line++;
                    this._flexA.Rows.Add();
                    this._flexA.Row = this._flexA.Rows.Count - 1;

                    this._flexA["NO_LINE"] = max_no_line;
                    this._flexA["NO_IOLINE"] = max_no_line;
                    this._flexA["NO_LOT"] = row["NO_LOT"].ToString();
                    this._flexA["CD_ITEM"] = row["CD_ITEM"].ToString();						// 품목코드
                    this._flexA["NM_ITEM"] = row["NM_ITEM"].ToString();						// 품목명
                    this._flexA["STND_ITEM"] = row["STND_ITEM"].ToString();					// 규격
                    if (row["CD_UNIT_MM"].ToString() == null)
                        row["CD_UNIT_MM"] = "";
                    this._flexA["CD_UNIT_MM"] = row["CD_UNIT_MM"].ToString();					// 수배단위
                    this._flexA["UNIT_IM"] = row["UNIT_IM"].ToString();						// 재고단위
                    this._flexA["RATE_EXCHG"] = row["RATE_EXCHG"];
                    // 단위환산량으로 수배단위, 재고단위에 의해서 결정된다.
                    if (BASIC.GetMAEXC_Menu("P_PU_REQ_REG_1", "PU_A00000007") == "100")
                        this._flexA["DT_LIMIT"] = row["DT_LIMIT"];					//납기일
                    else
                        this._flexA["DT_LIMIT"] = row["DT_DELIVERY"];
                    this._flexA["QT_REQ_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ_MM"]));								// LC수배수량
                    this._flexA["RT_VAT"] = "0";

                    // 0502(부가세를 위해서)
                    this._flexA["QT_REAL"] = 0;//row["QT_LC_MM"];	
                    // 0502

                    // MA_PITEM.FG_FOQ =  'Y'이면 YN_INSP = 'Y'
                    this._flexA["YN_INSP"] = "N";

                    if (row["FG_IQC"].ToString() != "")
                    {
                        if (row["FG_IQC"].ToString() == "Y")
                        {
                            this._flexA["YN_INSP"] = "Y";
                        }
                    }

                    this._flexA["QT_REQ"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ"]));
                    this._flexA["QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ"]));									// LC개설수량
                    this._flexA["QT_PASS"] = 0;
                    this._flexA["QT_REJECTION"] = 0;
                    this._flexA["QT_GR"] = 0;
                    this._flexA["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX_PO"]));								// 발주단가
                    this._flexA["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX"]));									// 단가<<--발주단가						
                    this._flexA["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["AM_EX"]));
                    this._flexA["CD_PJT"] = row["CD_PJT"].ToString();						// 프로젝트코드
                    this._flexA["CD_PURGRP"] = row["CD_PURGRP"].ToString();
                    this._flexA["NO_PO"] = row["NO_PO"].ToString();
                    this._flexA["NO_POLINE"] = row["NO_LINE_PO"];
                    this._flexA["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM"]));
                    this._flexA["AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(row["AM"]));
                    this._flexA["VAT"] = 0;
                    this._flexA["QT_GR_MM"] = 0;
                    this._flexA["RT_CUSTOMS"] = 0;
                    this._flexA["YN_RETURN"] = "N";
                    _header.CurrentRow["YN_RETURN"] = "N";  //헤더에 입력해줘야한다!

                    this._flexA["FG_TPPURCHASE"] = row["FG_TPPURCHASE"].ToString();

                    if (row["FG_TPPURCHASE"].ToString() != string.Empty)
                    {
                        this._flexA["YN_PURCHASE"] = "Y";
                    }
                    else
                    {
                        this._flexA["YN_PURCHASE"] = "N";
                    }

                    this._flexA["FG_RCV"] = row["CD_QTIOTP"].ToString();				// 발주의 입고형태 코드
                    this._flexA["NM_FG_RCV"] = row["NM_QTIOTP"].ToString();			// 발주의 입고형태 명
                    this._flexA["FG_TRANS"] = row["FG_LC"].ToString();					// LC구분값
                    this._flexA["FG_TAX"] = "23";										// (POH 조인)과세구분
                    this._flexA["CD_EXCH"] = row["CD_EXCH"].ToString();				// 환종
                    _header.CurrentRow["CD_EXCH"] = row["CD_EXCH"].ToString();
                    if (D.GetDecimal(row["RT_EXCH"]) == 0 && D.GetString(row["CD_EXCH"]) == "000")
                    {
                        this._flexA["RT_EXCH"] = 1;			//환율
                    }
                    else
                    {
                        this._flexA["RT_EXCH"] = D.GetDecimal(row["RT_EXCH"]);
                    }

                    this._flexA["NO_IO_MGMT"] = "";
                    this._flexA["NO_IOLINE_MGMT"] = 0;
                    this._flexA["NO_PO_MGMT"] = "";
                    this._flexA["NO_POLINE_MGMT"] = 0;

                    this._flexA["NO_TO"] = "";											// 통관번호
                    this._flexA["NO_TO_LINE"] = 0;										// 통관항번

                    this._flexA["CD_SL"] = row["CD_SL"].ToString();
                    // 20031118
                    this._flexA["NM_SL"] = row["NM_SL"].ToString();

                    this._flexA["NO_LC"] = row["NO_LC"].ToString();
                    this._flexA["NO_LCLINE"] = row["NO_LINE"];
                    this._flexA["FG_TAXP"] = "001";
                    this._flexA["NO_EMP"] = D.GetString(ctx담당자.CodeValue);
                    this._flexA["NM_PROJECT"] = row["NM_PJT"].ToString();
                    this._flexA["YN_AM"] = row["YN_AM"].ToString();                    //(POH 발주에서 읽어오기)
                    _header.CurrentRow["YN_AM"] = row["YN_AM"].ToString();              //헤더에 입력해줘야한다!
                    this._flexA["VAT_CLS"] = 0;
                    this._flexA["YN_AUTORCV"] = row["YN_AUTORCV"].ToString();          //(POL의YN_AUTORCV값 )
                    this._flexA["YN_REQ"] = "Y";                                       //row["YN_RCV"].ToString();

                    // 0314 : 발주금액, 원화금액 -->> 의뢰에 반영
                    this._flexA["AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["AM_EX"]));

                    this._flexA["AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, this._flexA.CDecimal(row["AM"]));

                    this._flexA["AM_EXRCV"] = 0;
                    this._flexA["AM_RCV"] = 0;

                    this._flexA["NM_SYSDEF"] = row["NM_SYSDEF"].ToString();
                    this._flexA["NM_KOR"] = row["NM_KOR"].ToString();

                    if (row["CD_EXCH"].ToString() != "")
                    {
                        txt통화명.Text = row["NM_SYSDEF"].ToString();
                    }

                    this._flexA["CD_PLANT"] = _header.CurrentRow["CD_PLANT"].ToString();

                    this._flexA["AM_TOTAL"] = Unit.원화금액(DataDictionaryTypes.PU, this._flexA.CDecimal(row["AM"]) + this._flexA.CDecimal(this._flexA["VAT"])); //총금액

                    this._flexA["GI_PARTNER"] = row["GI_PARTNER"];
                    this._flexA["NM_GI_PARTER"] = row["NM_GI_PARTER"];

                    if (Config.MA_ENV.PJT형여부 == "Y")
                    {
                        this._flexA["CD_PJT_ITEM"] = row["CD_PJT_ITEM"];
                        this._flexA["NM_PJT_ITEM"] = row["NM_PJT_ITEM"];
                        this._flexA["PJT_ITEM_STND"] = row["PJT_ITEM_STND"];
                        this._flexA["SEQ_PROJECT"] = D.GetDecimal(row["SEQ_PROJECT"]);
                    }

                    this._flexA["NO_SERL"] = row["NO_SERL"].ToString();

                    if (dt.Columns.Contains("DT_PLAN"))   //2013.07.30 납기예정일추가
                    {
                        this._flexA["DT_PLAN"] = row["DT_PLAN"];
                    }
                    this._flexA["CLS_ITEM"] = row["CLS_ITEM"];
                    this._flexA["MAT_ITEM"] = row["MAT_ITEM"];


                    NO_PO_MULTI += D.GetString(row["NO_PO"]) + D.GetString(row["NO_LINE_PO"]) + "|";
                }
  
                _header.CurrentRow["CD_PARTNER"] = dt.Rows[0]["CD_PARTNER"];
                ctx매입처.SetCode(dt.Rows[0]["CD_PARTNER"].ToString(), dt.Rows[0]["LN_PARTNER"].ToString());

                if (외주유무 == "100")
                {
                    SetflexD(NO_PO_MULTI);
                }

                this._flexA.Redraw = true;

                ControlEnabledDisable(false);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void 가입고데이터추가(DataTable dt)
        {
            try
            {
                if (dt == null || dt.Rows.Count <= 0)
                    return;

                DataRow row;
                decimal unit_po_fact = 1;
                decimal max_no_line = this._flexA.GetMaxValue("NO_LINE");
                this._flexA.Redraw = false;
                bool YN_EXIST = dt.Columns.Contains("JAN_QT_PASS");
                string NO_PO_MULTI = string.Empty;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    row = dt.Rows[i];
                    max_no_line++;
                    this._flexA.Rows.Add();
                    this._flexA.Row = this._flexA.Rows.Count - 1;

                    this._flexA["NO_LINE"] = max_no_line;
                    this._flexA["NO_IOLINE"] = max_no_line;
                    this._flexA["CD_ITEM"] = row["CD_ITEM"];						//품목코드
                    this._flexA["NM_ITEM"] = row["NM_ITEM"];						//품목명
                    this._flexA["STND_ITEM"] = row["STND_ITEM"];					//규격
                    if (row["UNIT_IM"] == null)
                        row["UNIT_IM"] = "";
                    if (D.GetDecimal(row["UNIT_PO_FACT"]) != 0) unit_po_fact = D.GetDecimal(row["UNIT_PO_FACT"]);
                    this._flexA["NO_LOT"] = row["NO_LOT"];

                    this._flexA["CD_UNIT_MM"] = row["UNIT_PO"];   				//수배단위
                    this._flexA["UNIT_IM"] = row["UNIT_IM"];						//재고단위
                    this._flexA["RATE_EXCHG"] = unit_po_fact; //row["QT_FACT"];				//단위환산량으로 수배단위, 재고단위에 의해서 결정된다.	
                    this._flexA["RT_VAT"] = row["RT_VAT"];

                    if (BASIC.GetMAEXC_Menu("P_PU_REQ_REG_1", "PU_A00000007") == "100")
                        this._flexA["DT_LIMIT"] = row["DT_LIMIT"];					//납기일
                    else
                        this._flexA["DT_LIMIT"] = row["DT_REV"];

                    this._flexA["QT_REQ_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REV_VAL"]) / unit_po_fact);				//수배수량
                    this._flexA["QT_REAL"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REV_VAL"]));

                    this._flexA["QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REV_VAL"])); //* unit_po_fact;	
                    this._flexA["QT_REQ"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REV_VAL"])); //* unit_po_fact;					//의뢰량

                    this._flexA["QT_PASS"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_PASS"]));                      //합격수량(재고단위)
                    this._flexA["QT_REJECTION"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_BAD"]));                  //불량수량(재고단위)

                    // MA_PITEM.FG_FOQ =  'Y'이면 YN_INSP = 'Y'
                    this._flexA["YN_INSP"] = "N";
                    this._flexA["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX_PO"]));								//발주단가
                    this._flexA["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX"]));									//단가<<--발주단가						
                    this._flexA["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM_REV"]));
                    this._flexA["CD_PJT"] = row["CD_PJT"];						//프로젝트코드
                    this._flexA["CD_PURGRP"] = row["CD_PURGRP"];
                    this._flexA["NO_PO"] = row["NO_PO"];
                    this._flexA["NO_POLINE"] = row["NO_POLINE"];
                    this._flexA["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM"]));
                    this._flexA["AM"] = Unit.원화금액(DataDictionaryTypes.PU, this._flexA.CDecimal(this._flexA.CDecimal(row["JAN_AM"])));
                    this._flexA["VAT"] = Unit.원화금액(DataDictionaryTypes.PU, this._flexA.CDecimal(this._flexA.CDecimal(row["VAT_REV"].ToString())));

                    this._flexA["AM_TOTAL"] = Unit.원화금액(DataDictionaryTypes.PU, this._flexA.CDecimal(this._flexA["AM"]) + this._flexA.CDecimal(this._flexA["VAT"]));
                    this._flexA["QT_GR_MM"] = 0;
                    this._flexA["RT_CUSTOMS"] = 0;
                    this._flexA["YN_RETURN"] = "N";
                    _header.CurrentRow["YN_RETURN"] = row["YN_RETURN"];              //헤더에 입력해줘야한다!
                    this._flexA["FG_TPPURCHASE"] = row["FG_PURCHASE"];
                    this._flexA["YN_PURCHASE"] = row["YN_PURCHASE"].ToString(); //"";//(row["FG_PURCHASE"].ToString() != "") ? "Y" : "N";

                    this._flexA["FG_POST"] = row["FG_POST"];			//발주상태 코드
                    this._flexA["FG_RCV"] = row["FG_RCV"];				//발주의 입고형태 코드
                    this._flexA["FG_TRANS"] = row["FG_TRANS"];			//거래구분
                    this._flexA["FG_TAX"] = row["FG_TAX"];				//과세구분
                    this._flexA["CD_EXCH"] = row["CD_EXCH"];			//환종
                    _header.CurrentRow["CD_EXCH"] = row["CD_EXCH"];

                    if (D.GetDecimal(row["RT_EXCH"]) == 0 && D.GetString(row["CD_EXCH"]) == "000")
                    {
                        this._flexA["RT_EXCH"] = 1;			//환율
                    }
                    else
                    {
                        this._flexA["RT_EXCH"] = D.GetDecimal(row["RT_EXCH"]);
                    }

                    if (row["CD_EXCH"].ToString() != "")
                    {
                        txt통화명.Text = row["NM_EXCH"].ToString();
                    }

                    this._flexA["NO_IO_MGMT"] = "";
                    this._flexA["NO_IOLINE_MGMT"] = 0;
                    this._flexA["NO_PO_MGMT"] = "";
                    this._flexA["NO_POLINE_MGMT"] = 0;

                    this._flexA["NO_TO"] = "";										//통관번호
                    this._flexA["NO_TO_LINE"] = 0;									//통관항번

                    this._flexA["CD_SL"] = row["CD_SL"];
                    this._flexA["NM_SL"] = row["NM_SL"];

                    this._flexA["FG_TAXP"] = row["FG_TAXP"];
                    this._flexA["NO_EMP"] = D.GetString(ctx담당자.CodeValue);
                    this._flexA["NM_PROJECT"] = row["NM_PROJECT"];
                    this._flexA["YN_AM"] = row["YN_AM"];
                    _header.CurrentRow["YN_AM"] = row["YN_AM"];              //헤더에 입력해줘야한다!
                    this._flexA["VAT_CLS"] = 0;
                    this._flexA["YN_AUTORCV"] = row["YN_AUTORCV"];
                    this._flexA["YN_REQ"] = row["YN_RCV"];

                    this._flexA["AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM_REV"]));
                    this._flexA["AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM"]));

                    this._flexA["AM_EXRCV"] = 0;
                    this._flexA["AM_RCV"] = 0;

                    this._flexA["NM_SYSDEF"] = "";//row["NM_SYSDEF"];

                    this._flexA["NO_REV"] = row["NO_REV"]; //SetCol("NO_REV", "납품승인번호", false);
                    this._flexA["NO_REVLINE"] = row["NO_REVLINE"]; //.SetCol("NO_REVLINE", "승인LINE", false);

                    this._flexA["NM_KOR"] = row["NM_KOR"];
                    this._flexA["NM_FG_RCV"] = row["NM_FG_RCV"];
                    this._flexA["NM_FG_POST"] = row["NM_FG_POST"];
                    //2010.01.25추가
                    this._flexA["NM_PROJECT"] = row["NM_PROJECT"];
                    this._flexA["CD_PLANT"] = _header.CurrentRow["CD_PLANT"].ToString();

                    //2010.09.29 추가
                    this._flexA["NO_TO"] = row["NO_TO"].ToString();					//통관번호
                    this._flexA["NO_TO_LINE"] = row["NO_TOLINE"];			//통관항번
                    this._flexA["CD_ZONE"] = row["CD_ZONE"];		    //저장위치

                    if (D.GetString(cbo거래구분.SelectedValue) == "003")
                    {
                        this._flexA["NO_LC"] = row["NO_LC_LOCAL"].ToString();					//LC번호
                        this._flexA["NO_LCLINE"] = row["NO_LCLINE_LOCAL"];		    //LC항번
                    }
                    else
                    {
                        this._flexA["NO_LC"] = row["NO_LC"].ToString();					//LC번호
                        this._flexA["NO_LCLINE"] = row["NO_LCLINE"];		    //LC항번
                    }

                    // 2011-06-03, 최승애 , PIMS : M20110519153
                    this._flexA["REV_QT_REQ_MM"] = Unit.수량(DataDictionaryTypes.PU, UDecimal.Getdivision(D.GetDecimal(row["QT_REV_VAL"]), unit_po_fact));                // 가입고 적용시 의뢰량 2011-06-03, 최승애 추가
                    this._flexA["REV_AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM_REV"]));                  // 가입고 적용시 외화금액 2011-06-03, 최승애 추가 . AM_REV -> JAN_AM_REV 로 변경 => 수입검사에서 합격수량에서 잔량을 빼오는식때문에
                    this._flexA["REV_AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flexA.CDecimal(row["JAN_AM"])));    // 가입고 적용시 원화금액 2011-06-03, 최승애 추가

                    // 2012.03.07 신미란 추가 D20120307029
                    this._flexA["REV_QT_PASS"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_PASS"]));
                    this._flexA["REV_QT_REV_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REV_VAL"]));

                    if (Config.MA_ENV.PJT형여부 == "Y")
                    {
                        this._flexA["CD_PJT_ITEM"] = row["CD_PJT_ITEM"];
                        this._flexA["NM_PJT_ITEM"] = row["NM_PJT_ITEM"];
                        this._flexA["PJT_ITEM_STND"] = row["PJT_ITEM_STND"];
                        this._flexA["NO_WBS"] = row["NO_WBS"];
                        this._flexA["NO_CBS"] = row["NO_CBS"];
                        this._flexA["SEQ_PROJECT"] = D.GetDecimal(row["SEQ_PROJECT"]);
                    }

                    if (dt.Columns.Contains("NM_PURGRP"))
                        this._flexA["NM_PURGRP"] = row["NM_PURGRP"];

                    if (this._flexA.DataTable.Columns.Contains("GI_PARTNER"))
                    {
                        this._flexA["GI_PARTNER"] = row["GI_PARTNER"];
                        this._flexA["NM_GI_PARTER"] = row["NM_GI_PARTER"];
                    }

                    if (특채수량사용여부 == "Y" && YN_EXIST)
                    {
                        this._flexA["JAN_QT_PASS"] = row["JAN_QT_PASS"];
                        this._flexA["JAN_QT_SPECIAL"] = row["JAN_QT_SPECIAL"];
                        this._flexA["FG_SPECIAL"] = row["OB_PUT"];
                    }

                    this._flexA["NO_SERL"] = row["NO_SERL"];

                    if (this._flexA.DataTable.Columns.Contains("DT_PLAN"))
                    {
                        this._flexA["DT_PLAN"] = row["DT_PLAN"];
                    }
                    this._flexA["CLS_ITEM"] = row["CLS_ITEM"];
                    this._flexA["TP_UM_TAX"] = row["TP_UM_TAX"];

                    NO_PO_MULTI += D.GetString(row["NO_PO"]) + D.GetString(row["NO_POLINE"]) + "|";

                    this._flexA["MAT_ITEM"] = row["MAT_ITEM"];
                }
 
                _header.CurrentRow["CD_PARTNER"] = dt.Rows[0]["CD_PARTNER"];

                ctx매입처.SetCode(dt.Rows[0]["CD_PARTNER"].ToString(), dt.Rows[0]["LN_PARTNER"].ToString());
                this._flexA.Redraw = true;
                ///////////////////////////

                if (외주유무 == "100")
                {
                    SetflexD(NO_PO_MULTI);
                }

                ControlEnabledDisable(false);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void 바코드데이터추가(DataTable dt)
        {
            try
            {
                if (dt == null || dt.Rows.Count <= 0) return;

                DataRow row;
                decimal unit_po_fact = 1;
                decimal max_no_line = this._flexA.GetMaxValue("NO_LINE");
                this._flexA.Redraw = false;
                bool YN_EXIST = dt.Columns.Contains("JAN_QT_PASS");
                string NO_PO_MULTI = string.Empty;

                dtp입고일자.Text = D.GetString(dt.Rows[0]["DT_REV"]);
                _header.CurrentRow["DT_REQ"] = dtp입고일자.Text;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    row = dt.Rows[i];
                    max_no_line++;
                    this._flexA.Rows.Add();
                    this._flexA.Row = this._flexA.Rows.Count - 1;

                    this._flexA["NO_LINE"] = max_no_line;
                    this._flexA["NO_IOLINE"] = max_no_line;
                    this._flexA["CD_ITEM"] = row["CD_ITEM"];						//품목코드
                    this._flexA["NM_ITEM"] = row["NM_ITEM"];						//품목명
                    this._flexA["STND_ITEM"] = row["STND_ITEM"];					//규격
                    if (row["UNIT_IM"] == null)
                        row["UNIT_IM"] = "";
                    if (D.GetDecimal(row["UNIT_PO_FACT"]) != 0) unit_po_fact = D.GetDecimal(row["UNIT_PO_FACT"]);
                    this._flexA["NO_LOT"] = row["NO_LOT"];

                    this._flexA["CD_UNIT_MM"] = row["UNIT_PO"];   				//수배단위
                    this._flexA["UNIT_IM"] = row["UNIT_IM"];						//재고단위
                    this._flexA["RATE_EXCHG"] = unit_po_fact; //row["QT_FACT"];				//단위환산량으로 수배단위, 재고단위에 의해서 결정된다.	
                    this._flexA["RT_VAT"] = row["RT_VAT"];

                    if (BASIC.GetMAEXC_Menu("P_PU_REQ_REG_1", "PU_A00000007") == "100")
                        this._flexA["DT_LIMIT"] = row["DT_LIMIT"];					//납기일
                    else
                        this._flexA["DT_LIMIT"] = row["DT_REV"];

                    this._flexA["QT_REQ_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REV_VAL"]) / unit_po_fact);				//수배수량
                    this._flexA["QT_REAL"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REV_VAL"]));

                    this._flexA["QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REV_VAL"])); //* unit_po_fact;	
                    this._flexA["QT_REQ"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REV_VAL"])); //* unit_po_fact;					//의뢰량

                    this._flexA["QT_PASS"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_PASS"]));                      //합격수량(재고단위)
                    this._flexA["QT_REJECTION"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_BAD"]));                  //불량수량(재고단위)

                    // MA_PITEM.FG_FOQ =  'Y'이면 YN_INSP = 'Y'
                    this._flexA["YN_INSP"] = "N";
                    this._flexA["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX_PO"]));								//발주단가
                    this._flexA["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX"]));									//단가<<--발주단가						
                    this._flexA["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM_REV"]));
                    this._flexA["CD_PJT"] = row["CD_PJT"];						//프로젝트코드
                    this._flexA["CD_PURGRP"] = row["CD_PURGRP"];
                    this._flexA["NO_PO"] = row["NO_PO"];
                    this._flexA["NO_POLINE"] = row["NO_POLINE"];
                    this._flexA["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM"]));
                    this._flexA["AM"] = Unit.원화금액(DataDictionaryTypes.PU, this._flexA.CDecimal(this._flexA.CDecimal(row["JAN_AM"])));
                    this._flexA["VAT"] = Unit.원화금액(DataDictionaryTypes.PU, this._flexA.CDecimal(this._flexA.CDecimal(row["VAT_REV"].ToString())));

                    this._flexA["AM_TOTAL"] = Unit.원화금액(DataDictionaryTypes.PU, this._flexA.CDecimal(this._flexA["AM"]) + this._flexA.CDecimal(this._flexA["VAT"]));
                    this._flexA["QT_GR_MM"] = 0;
                    this._flexA["RT_CUSTOMS"] = 0;
                    this._flexA["YN_RETURN"] = "N";
                    _header.CurrentRow["YN_RETURN"] = row["YN_RETURN"];              //헤더에 입력해줘야한다!
                    this._flexA["FG_TPPURCHASE"] = row["FG_PURCHASE"];
                    this._flexA["YN_PURCHASE"] = row["YN_PURCHASE"].ToString(); //"";//(row["FG_PURCHASE"].ToString() != "") ? "Y" : "N";

                    this._flexA["FG_POST"] = row["FG_POST"];			//발주상태 코드
                    this._flexA["FG_RCV"] = row["FG_RCV"];				//발주의 입고형태 코드
                    this._flexA["FG_TRANS"] = row["FG_TRANS"];			//거래구분
                    this._flexA["FG_TAX"] = row["FG_TAX"];				//과세구분
                    this._flexA["CD_EXCH"] = row["CD_EXCH"];			//환종
                    _header.CurrentRow["CD_EXCH"] = row["CD_EXCH"];

                    if (D.GetDecimal(row["RT_EXCH"]) == 0 && D.GetString(row["CD_EXCH"]) == "000")
                    {
                        this._flexA["RT_EXCH"] = 1;			//환율
                    }
                    else
                    {
                        this._flexA["RT_EXCH"] = D.GetDecimal(row["RT_EXCH"]);
                    }

                    this._flexA["NO_IO_MGMT"] = "";
                    this._flexA["NO_IOLINE_MGMT"] = 0;
                    this._flexA["NO_PO_MGMT"] = "";
                    this._flexA["NO_POLINE_MGMT"] = 0;

                    this._flexA["NO_TO"] = "";										//통관번호
                    this._flexA["NO_TO_LINE"] = 0;									//통관항번

                    this._flexA["CD_SL"] = row["CD_SL"];
                    this._flexA["NM_SL"] = row["NM_SL"];

                    this._flexA["FG_TAXP"] = row["FG_TAXP"];
                    this._flexA["NO_EMP"] = D.GetString(ctx담당자.CodeValue);
                    this._flexA["NM_PROJECT"] = row["NM_PROJECT"];
                    this._flexA["YN_AM"] = row["YN_AM"];
                    _header.CurrentRow["YN_AM"] = row["YN_AM"];              //헤더에 입력해줘야한다!
                    this._flexA["VAT_CLS"] = 0;
                    this._flexA["YN_AUTORCV"] = row["YN_AUTORCV"];
                    this._flexA["YN_REQ"] = row["YN_RCV"];

                    this._flexA["AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM_REV"]));
                    this._flexA["AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM"]));

                    this._flexA["AM_EXRCV"] = 0;
                    this._flexA["AM_RCV"] = 0;

                    this._flexA["NM_SYSDEF"] = "";//row["NM_SYSDEF"];

                    this._flexA["NO_REV"] = row["NO_REV"]; //SetCol("NO_REV", "납품승인번호", false);
                    this._flexA["NO_REVLINE"] = row["NO_REVLINE"]; //.SetCol("NO_REVLINE", "승인LINE", false);

                    this._flexA["NM_KOR"] = row["NM_KOR"];
                    this._flexA["NM_FG_RCV"] = row["NM_FG_RCV"];
                    this._flexA["NM_FG_POST"] = row["NM_FG_POST"];
                    this._flexA["NM_PROJECT"] = row["NM_PROJECT"];
                    this._flexA["CD_PLANT"] = row["CD_PLANT"];

                    _header.CurrentRow["CD_PLANT"] = row["CD_PLANT"];
                    cbo공장.SelectedValue = row["CD_PLANT"];

                    //2010.09.29 추가
                    this._flexA["NO_TO"] = row["NO_TO"].ToString();					//통관번호
                    this._flexA["NO_TO_LINE"] = row["NO_TOLINE"];			//통관항번
                    this._flexA["CD_ZONE"] = row["CD_ZONE"];		    //저장위치

                    if (D.GetString(cbo거래구분.SelectedValue) == "003")
                    {
                        this._flexA["NO_LC"] = row["NO_LC_LOCAL"].ToString();					//LC번호
                        this._flexA["NO_LCLINE"] = row["NO_LCLINE_LOCAL"];		    //LC항번
                    }
                    else
                    {
                        this._flexA["NO_LC"] = row["NO_LC"].ToString();					//LC번호
                        this._flexA["NO_LCLINE"] = row["NO_LCLINE"];		    //LC항번
                    }

                    // 2011-06-03, 최승애 , PIMS : M20110519153
                    this._flexA["REV_QT_REQ_MM"] = Unit.수량(DataDictionaryTypes.PU, UDecimal.Getdivision(D.GetDecimal(row["QT_REV_VAL"]), unit_po_fact));                // 가입고 적용시 의뢰량 2011-06-03, 최승애 추가
                    this._flexA["REV_AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM_REV"]));                  // 가입고 적용시 외화금액 2011-06-03, 최승애 추가 . AM_REV -> JAN_AM_REV 로 변경 => 수입검사에서 합격수량에서 잔량을 빼오는식때문에
                    this._flexA["REV_AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flexA.CDecimal(row["JAN_AM"])));    // 가입고 적용시 원화금액 2011-06-03, 최승애 추가

                    // 2012.03.07 신미란 추가 D20120307029
                    this._flexA["REV_QT_PASS"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_PASS"]));
                    this._flexA["REV_QT_REV_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REV_VAL"]));

                    if (Config.MA_ENV.PJT형여부 == "Y")
                    {
                        this._flexA["CD_PJT_ITEM"] = row["CD_PJT_ITEM"];
                        this._flexA["NM_PJT_ITEM"] = row["NM_PJT_ITEM"];
                        this._flexA["PJT_ITEM_STND"] = row["PJT_ITEM_STND"];
                        this._flexA["NO_WBS"] = row["NO_WBS"];
                        this._flexA["NO_CBS"] = row["NO_CBS"];
                        this._flexA["SEQ_PROJECT"] = D.GetDecimal(row["SEQ_PROJECT"]);
                    }

                    if (dt.Columns.Contains("NM_PURGRP"))
                        this._flexA["NM_PURGRP"] = row["NM_PURGRP"];

                    if (this._flexA.DataTable.Columns.Contains("GI_PARTNER"))
                    {
                        this._flexA["GI_PARTNER"] = row["GI_PARTNER"];
                        this._flexA["NM_GI_PARTER"] = row["NM_GI_PARTER"];
                    }

                    if (특채수량사용여부 == "Y" && YN_EXIST)
                    {
                        this._flexA["JAN_QT_PASS"] = row["JAN_QT_PASS"];
                        this._flexA["JAN_QT_SPECIAL"] = row["JAN_QT_SPECIAL"];
                        this._flexA["FG_SPECIAL"] = row["OB_PUT"];
                    }

                    this._flexA["NO_SERL"] = row["NO_SERL"];
                    this._flexA["DT_PLAN"] = row["DT_PLAN"];
                    this._flexA["CLS_ITEM"] = row["CLS_ITEM"];

                    NO_PO_MULTI += D.GetString(row["NO_PO"]) + D.GetString(row["NO_POLINE"]) + "|";
                }

                _header.CurrentRow["CD_PARTNER"] = dt.Rows[0]["CD_PARTNER"];

                ctx매입처.SetCode(dt.Rows[0]["CD_PARTNER"].ToString(), dt.Rows[0]["LN_PARTNER"].ToString());
                this._flexA.Redraw = true;
                ///////////////////////////

                if (외주유무 == "100")
                {
                    SetflexD(NO_PO_MULTI);
                }

                ControlEnabledDisable(false);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        
        private void SetflexD(string NO_PO_MULTI)
        {
            try
            {
                if (NO_PO_MULTI == string.Empty) return;

                DataTable dt = _biz.SearchMATL(NO_PO_MULTI, new object[] { Global.MainFrame.LoginInfo.CompanyCode, "", D.GetString(ctx매입처.CodeValue), D.GetString(cbo공장.SelectedValue) });

                string filter = "NO_PO = '" + D.GetString(this._flexA["NO_PO"]) + "' AND NO_POLINE = '" + D.GetString(this._flexA["NO_POLINE"]) + "' ";
                decimal NO_LINE = D.GetDecimal(this._flexB.DataTable.Compute("MAX(NO_IOLINE)", filter));

                if (dt == null || dt.Rows.Count == 0) return;

                foreach (DataRow dr in dt.Rows)
                {

                    this._flexB.Rows.Add();
                    this._flexB.Row = this._flexB.Rows.Count - 1;

                    string filter2 = "NO_PO = '" + D.GetString(dr["NO_PO"]) + "' AND NO_POLINE = '" + D.GetString(dr["NO_POLINE"]) + "' ";
                    decimal QT_REQ_MM = D.GetDecimal(this._flexA.DataTable.Compute("MAX(QT_REQ_MM)", filter2));

                    this._flexB["CD_ITEM"] = dr["CD_ITEM"];
                    this._flexB["NM_ITEM_ITEM"] = dr["NM_ITEM_ITEM"];
                    this._flexB["STND_ITEM"] = dr["STND_ITEM"];
                    this._flexB["STND_ITEM_ITEM"] = dr["STND_ITEM_ITEM"];
                    this._flexB["UNIT_IM_ITEM"] = dr["UNIT_IM_ITEM"];
                    this._flexB["CD_MATL"] = dr["CD_MATL"];
                    this._flexB["NM_ITEM"] = dr["NM_ITEM"];
                    this._flexB["STND_ITEM"] = dr["STND_ITEM"];
                    this._flexB["UNIT_IM"] = dr["UNIT_IM"];
                    this._flexB["QT_NEED"] = D.GetDecimal(dr["QT_NEED_UNIT"]) * QT_REQ_MM;
                    this._flexB["NO_PO"] = dr["NO_PO"];
                    this._flexB["NO_POLINE"] = dr["NO_POLINE"];
                    this._flexB["NO_PO_MAL_LINE"] = dr["NO_PO_MAL_LINE"];
                    this._flexB["CD_SL"] = dr["CD_SL"];
                    this._flexB["NM_SL"] = dr["NM_SL"];
                    this._flexB["NO_IOLINE"] = ++NO_LINE;
                    this._flexB["CD_PLANT"] = D.GetString(cbo공장.SelectedValue);
                    this._flexB["DT_IO"] = D.GetString(dtp입고일자.Text);
                    this._flexB["NO_PSO_MGMT"] = dr["NO_PO"];
                    this._flexB["NO_PSOLINE_MGMT"] = dr["NO_POLINE"];
                    this._flexB["CD_PARTNER"] = D.GetString(ctx매입처.CodeValue);
                    this._flexB["QT_IO"] = dr["QT_NEED"];
                    this._flexB["NO_EMP"] = D.GetString(ctx담당자.CodeValue);
                    this._flexB["FG_IO"] = dr["FG_IO"];
                    this._flexB["CD_QTIOTP"] = dr["CD_QTIOTP"];
                    this._flexB["FG_TRANS"] = dr["FG_TRANS"];
                    this._flexB["GI_PARTNER"] = D.GetString(ctx매입처.CodeValue);
                    this._flexB["YN_RETURN"] = dr["YN_RETURN"];
                    this._flexB["CD_DEPT"] = dr["CD_DEPT"];
                    this._flexB["NO_IOLINE_MGMT"] = D.GetDecimal(this._flexA.DataTable.Compute("MAX(NO_LINE)", filter2));
                    this._flexB["QT_NEED_UNIT"] = dr["QT_NEED_UNIT"];
                    this._flexB["YN_PARTNER_SL"] = dr["YN_PARTNER_SL"];
                }

                this._flexB.AddFinished();
                this._flexB.Col = this._flexB.Cols.Fixed;

                this._flexB.RowFilter = filter;
            }

            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ♣ 그리드 이벤트

        private void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsChanged())
                {
                    this.ToolBarSaveButtonEnabled = true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _header_ControlValueChanged(object sender, FreeBindingArgs e)
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

        private void _flex_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                FlexGrid FLEX = null;

                if (((FlexGrid)sender).Name == "this._flex")
                    FLEX = this._flexA;
                else
                    FLEX = this._flexB;

                if (this._flexA.Cols[e.Col].Name == "S") return;

                if (!추가모드여부)
                {
                    if (FLEX.RowState() != DataRowState.Added)
                    {
                        ShowMessage("이미입고되어수정불가합니다");
                        e.Cancel = true;
                    }
                }

                if (D.GetString(this._flexA["PO_PRICE"]) == "Y" && (this._flexA.Cols[e.Col].Name == "AM_EXREQ" || this._flexA.Cols[e.Col].Name == "AM_TOTAL" || this._flexA.Cols[e.Col].Name == "UM_EX_PO"))
                {
                    ShowMessage("구매단가통제된 구매그룹입니다.");
                    e.Cancel = true;
                }

                //부가세여부 포함 금액(AM_EX) 원화금액(AM), 부가세(VAT) EDIT 불가 
                if (D.GetString(this._flexA["TP_UM_TAX"]) == "001")
                {
                    if (this._flexA.Cols[e.Col].Name == "AM_EXREQ" || this._flexA.Cols[e.Col].Name == "AM_REQ" || this._flexA.Cols[e.Col].Name == "VAT")
                        e.Cancel = true;
                }
                else
                {
                    if (this._flexA.Cols[e.Col].Name == "AM_TOTAL")
                        e.Cancel = true;
                }


                switch (this._flexA.Cols[e.Col].Name)
                {
                    case "UM_EX_PO"://무역 단가 변경불가~!!!

                        if (this._flexA["FG_TRANS"].ToString() == "004" || this._flexA["FG_TRANS"].ToString() == "005")
                            e.Cancel = true;
                        break;
                }

                if (특채수량사용여부 == "Y" && (this._flexA.Cols[e.Col].Name == "AM_TOTAL" || this._flexA.Cols[e.Col].Name == "AM_EXREQ" || this._flexA.Cols[e.Col].Name == "QT_REQ_MM" || this._flexA.Cols[e.Col].Name == "UM_EX_PO"))
                {
                    e.Cancel = true;
                    return;
                }

                if (규격형사용유무 == "100")
                {
                    if ((D.GetDecimal(this._flexA["UM_WEIGHT"])) > 0)
                    {
                        if ((this._flexA.Cols[e.Col].Name == "UM_EX_PO") || (this._flexA.Cols[e.Col].Name == "AM_TOTAL") || (this._flexA.Cols[e.Col].Name == "UM_EX") || (this._flexA.Cols[e.Col].Name == "AM_EXREQ") || (this._flexA.Cols[e.Col].Name == "AM_REQ"))
                        {
                            e.Cancel = true;
                            return;
                        }
                    }
                }
                return;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                // 단위환산량(RATE_EXCHG)
                // 의뢰량, 금액, 원화금액 계산식
                // 의뢰량 = 수배수량*단위환산량
                // 금액 = 의뢰량*단가		

                string oldValue = ((FlexGrid)sender).GetData(e.Row, e.Col).ToString();
                string newValue = ((FlexGrid)sender).EditData;

                decimal RT_VAT = (D.GetDecimal(this._flexA["RT_VAT"]) == 0 ? 0 : D.GetDecimal(this._flexA["RT_VAT"]) * 0.01M);  //부가세율
                Decimal 부가세율 = D.GetDecimal(this._flexA["RT_VAT"]) == 0 ? 0 : D.GetDecimal(this._flexA["RT_VAT"]);  //과세율  
                decimal 환율 = D.GetDecimal(this._flexA[e.Row, "RT_EXCH"]);
                string 과세구분 = D.GetString(this._flexA[e.Row, "FG_TAX"]);
                bool 부가세포함 = D.GetString(this._flexA[e.Row, "TP_UM_TAX"]) == "001" ? true : false;
                decimal ldb_AM_REQ = 0, ldb_AM_EXREQ = 0;
                decimal 단위환산 = D.GetDecimal(this._flexA[e.Row, "RATE_EXCHG"]);
                decimal 부가세 = 0;

                if (this._flexA.AllowEditing)
                {
                    if (this._flexA.GetData(e.Row, e.Col).ToString() != this._flexA.EditData)
                    {
                        //금액, 부가세, 원화금액 계산식
                        switch (this._flexA.Cols[e.Col].Name)
                        {
                            case "QT_REQ_MM"://의뢰량 변경시-->>의뢰량 계산	 
                                this._flexA[e.Row, "QT_REQ"] = Unit.수량(DataDictionaryTypes.PU, this._flexA.CDecimal(newValue) * 단위환산);	//관라수량(의뢰량)

                                ldb_AM_REQ = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flexA["UM_EX_PO"]) * D.GetDecimal(this._flexA.EditData) * 환율);
                                Calc.GetAmt(D.GetDecimal(this._flexA.EditData), D.GetDecimal(this._flexA["UM_EX_PO"]), 환율, 과세구분, 부가세율, 모듈.PUR, 부가세포함, out ldb_AM_EXREQ, out ldb_AM_REQ, out 부가세);

                                if (!부가세포함)  //부가세별도
                                {
                                    // 2011-06-03, 최승애 PIMS번호 : M20110519153
                                    if (D.GetDecimal(this._flexA["REV_QT_REQ_MM"].ToString()) == D.GetDecimal(this._flexA["QT_REQ_MM"].ToString()))
                                    {
                                        ldb_AM_EXREQ = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flexA[e.Row, "REV_AM_EXREQ"].ToString()));
                                        ldb_AM_REQ = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flexA[e.Row, "REV_AM_REQ"].ToString()));
                                    }
                                }

                                this._flexA[e.Row, "AM_REQ"] = ldb_AM_REQ; //원화금액
                                this._flexA[e.Row, "AM"] = ldb_AM_REQ;//Unit.원화금액(DataDictionaryTypes.PU, this._flex.CDecimal(this._flex["AM_EXREQ"].ToString()) * this._flex.CDecimal(this._flex["RT_EXCH"].ToString()));

                                this._flexA[e.Row, "AM_EXREQ"] = ldb_AM_EXREQ;
                                this._flexA[e.Row, "AM_EX"] = ldb_AM_EXREQ;   //Unit.외화금액(DataDictionaryTypes.PU, this._flex.CDecimal(this._flex["AM_EXREQ"].ToString()));

                                this._flexA[e.Row, "VAT"] = 부가세; // this._flex[e.Row, "VAT"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flex.CDecimal(this._flex["AM_REQ"].ToString()) * RT_VAT));
                                this._flexA[e.Row, "AM_TOTAL"] = Calc.합계금액(ldb_AM_REQ, 부가세);  // this._flex[e.Row, "AM_TOTAL"] = Unit.원화금액(DataDictionaryTypes.PU,  D.GetDecimal(this._flex.CDecimal(this._flex["AM_REQ"].ToString())) + Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flex.CDecimal(this._flex["VAT"].ToString())))); //총합계
                                this._flexA[e.Row, "QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(this._flexA[e.Row, "QT_REQ"]));

                                // 0611
                                // 금액, 원화금액, 부가세 계산 함수
                                if (외주유무 == "100")
                                {
                                    DataRow[] drs = this._flexB.DataTable.Select("NO_PO = '" + D.GetString(this._flexA["NO_PO"]) + "' AND NO_POLINE = '" + D.GetString(this._flexA["NO_POLINE"]) + "'", "", DataViewRowState.CurrentRows);

                                    if (drs == null || drs.Length == 0) return;

                                    foreach (DataRow dr in drs)
                                    {
                                        dr["QT_NEED"] = D.GetDecimal(dr["QT_NEED_UNIT"]) * D.GetDecimal(newValue);
                                    }
                                }
                                break;

                            case "UM_EX_PO"://단가 변경시  
                                decimal 수량 = D.GetDecimal(this._flexA["QT_REQ_MM"]);

                                if (수량 == 0) return;

                                ldb_AM_REQ = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flexA.EditData) * 수량 * 환율);
                                Calc.GetAmt(수량, D.GetDecimal(this._flexA.EditData), 환율, 과세구분, 부가세율, 모듈.PUR, 부가세포함, out ldb_AM_EXREQ, out ldb_AM_REQ, out 부가세);

                                this._flexA[e.Row, "AM_EXREQ"] = ldb_AM_EXREQ;
                                this._flexA[e.Row, "AM_EX"] = ldb_AM_EXREQ;     //외화금액

                                this._flexA[e.Row, "AM_REQ"] = ldb_AM_REQ; //원화금액
                                this._flexA[e.Row, "AM"] = ldb_AM_REQ;

                                this._flexA[e.Row, "VAT"] = 부가세;
                                this._flexA[e.Row, "AM_TOTAL"] = Calc.합계금액(ldb_AM_REQ, 부가세);
                                this._flexA[e.Row, "UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(this._flexA.EditData) / 단위환산);
                                this._flexA[e.Row, "UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(this._flexA.EditData) / 단위환산 * 환율);
                                break;


                            case "AM_EXREQ"://금액변경시 -->금액, 원화단가, 원화금액 계산

                                /*  단가일 경우 소수점 이하까지 보여줘야 함으로 절사 (Floor)는 주석처리함.. UMEX_PO, UM_EX, UM 2007.09.03*/
                                this._flexA[e.Row, "AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, this._flexA.CDecimal(newValue) * this._flexA.CDecimal(this._flexA["RT_EXCH"].ToString()));
                                this._flexA[e.Row, "AM"] = Unit.원화금액(DataDictionaryTypes.PU, this._flexA.CDecimal(this._flexA["AM_EXREQ"].ToString()) * this._flexA.CDecimal(this._flexA["RT_EXCH"].ToString()));
                                this._flexA[e.Row, "AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, this._flexA.CDecimal(this._flexA["AM_EXREQ"].ToString())); // 절대 절사 안됨..
                                this._flexA[e.Row, "VAT"] = Unit.원화금액(DataDictionaryTypes.PU, this._flexA.CDecimal(this._flexA["AM_REQ"].ToString()) * RT_VAT);
                                this._flexA[e.Row, "AM_TOTAL"] = Unit.원화금액(DataDictionaryTypes.PU, this._flexA.CDecimal(this._flexA["AM_REQ"].ToString()) + Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flexA.CDecimal(this._flexA["VAT"].ToString())))); //총합계
                                this._flexA[e.Row, "UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, this._flexA.CDecimal(this._flexA["AM_EXREQ"].ToString()) / this._flexA.CDecimal(this._flexA["QT_REQ_MM"].ToString()));
                                this._flexA[e.Row, "UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, this._flexA.CDecimal(this._flexA["AM_EXREQ"].ToString()) / this._flexA.CDecimal(this._flexA["QT_REQ"].ToString()));
                                this._flexA[e.Row, "UM"] = Unit.원화단가(DataDictionaryTypes.PU, this._flexA.CDecimal(this._flexA["AM_REQ"].ToString()) / this._flexA.CDecimal(this._flexA["QT_REQ"].ToString()));
                                break;

                            case "QT_REQ":
                                if (this._flexA.CDouble(newValue) != this._flexA.CDouble(this._flexA[e.Row, "QT_REQ_MM"]))
                                {
                                    if (Global.MainFrame.ShowMessage("의뢰량과 관리수량이 다릅니다. 계속 입력하시겠습니까?", "QY2") != DialogResult.Yes)
                                    {
                                        ((FlexGrid)sender)["QT_REQ"] = ((FlexGrid)sender).GetData(e.Row, e.Col);
                                        return;
                                    }
                                }

                                if (외주유무 == "100")
                                {
                                    DataRow[] drs = this._flexB.DataTable.Select("NO_PO = '" + D.GetString(this._flexA["NO_PO"]) + "' AND NO_POLINE = '" + D.GetString(this._flexA["NO_POLINE"]) + "'", "", DataViewRowState.CurrentRows);

                                    if (drs == null || drs.Length == 0) return;

                                    foreach (DataRow dr in drs)
                                    {
                                        dr["QT_NEED"] = D.GetDecimal(dr["QT_NEED_UNIT"]) * D.GetDecimal(newValue);
                                    }
                                }

                                break;
                            case "AM_REQ":
                                this._flexA[e.Row, "VAT"] = Unit.원화금액(DataDictionaryTypes.PU, this._flexA.CDecimal(newValue) * RT_VAT);
                                break;

                            case "AM_TOTAL":
                                if (D.GetDecimal(this._flexA[e.Row, "QT_REQ_MM"]) == 0) return;
                                decimal 단가 = D.GetDecimal(this._flexA[e.Row, "UM_EX_PO"]);

                                if (부가세포함)  //부가세포함
                                {
                                    ldb_AM_REQ = D.GetDecimal(this._flexA.EditData); //총금액 
                                    if (의제매입여부(과세구분) && 의제부가세적용 == "100")
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

                                    this._flexA[e.Row, "AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, ldb_AM_REQ - 부가세);  //원화
                                    this._flexA[e.Row, "AM_EXREQ"] = Unit.원화금액(DataDictionaryTypes.PU, (ldb_AM_REQ - 부가세) / 환율); //외화
                                    this._flexA[e.Row, "VAT"] = 부가세;

                                    this._flexA[e.Row, "UM_EX_PO"] = 단가;
                                    this._flexA[e.Row, "UM_EX"] = 단가 / 단위환산;
                                    this._flexA[e.Row, "UM"] = (단가 / 단위환산) * 환율;
                                }
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

        private bool 의제매입여부(string ps_taxp)
        {
            if (ps_taxp == "27" || ps_taxp == "28" || ps_taxp == "29" || ps_taxp == "30" || ps_taxp == "32" || ps_taxp == "33" ||
                ps_taxp == "34" || ps_taxp == "35" || ps_taxp == "36" || ps_taxp == "40" || ps_taxp == "41" || ps_taxp == "42" ||
                ps_taxp == "48" || ps_taxp == "49")

                return true;

            return false;
        }

        private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                FlexGrid FLEX = null;

                if (((FlexGrid)sender).Name == "this._flex")
                    FLEX = this._flexA;
                else
                    FLEX = this._flexB;

                switch (FLEX.Cols[e.Col].Name)
                {
                    case "NM_SL":
                        e.Parameter.P09_CD_PLANT = D.GetString(cbo공장.SelectedValue);
                        break;
                    case "CD_SL":
                        e.Parameter.P09_CD_PLANT = D.GetString(cbo공장.SelectedValue);
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (외주유무 == "100")
                {
                    string filter = "NO_PO = '" + D.GetString(this._flexA["NO_PO"]) + "' AND NO_POLINE = '" + D.GetString(this._flexA["NO_POLINE"]) + "' ";
                    this._flexB.RowFilter = filter;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ♣ 기타 이벤트

        private void tabControlExt2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this._flexB.HasNormalRow)
                {
                    string filter = "NO_PO = '" + D.GetString(this._flexA["NO_PO"]) + "' AND NO_POLINE = '" + D.GetString(this._flexA["NO_POLINE"]) + "' ";
                    this._flexB.RowFilter = filter;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void ctx창고_QueryBefore(object sender, BpQueryArgs e)
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

        private void ctx담당자_QueryAfter(object sender, BpQueryArgs e)
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

        private void cbo공장_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                ctx창고.CodeValue = "";
                ctx창고.CodeName = "";
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        
        private void cbo거래구분_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbo거래구분.SelectedValue.ToString() == "001")//국내
            {
                cboLC.Enabled = false;
                btn통관적용.Enabled = false;
                btnLocalLC.Enabled = false;

            }
            if (cbo거래구분.SelectedValue.ToString() == "002")//구매승인서
            {
                cboLC.Enabled = false;
                btn통관적용.Enabled = false;
                btnLocalLC.Enabled = false;

                cboLC_SelectionChangeCommitted(sender, e);
            }//Local L/C, Master L/C, Master 기타

            if (cbo거래구분.SelectedValue.ToString() == "003")
            {
                cboLC.Enabled = true;
                if (cboLC.SelectedValue.ToString() == "001")
                {
                    btn통관적용.Enabled = false;
                    btnLocalLC.Enabled = true;
                }
                if (cboLC.SelectedValue.ToString() == "002")
                {
                    btn통관적용.Enabled = false;
                    btnLocalLC.Enabled = false;
                }
                cboLC_SelectionChangeCommitted(sender, e);
            }
            if (cbo거래구분.SelectedValue.ToString() == "004" ||
                cbo거래구분.SelectedValue.ToString() == "005")
            {
                cboLC.Enabled = true;
                if (cboLC.SelectedValue.ToString() == "001")
                {
                    btnLocalLC.Enabled = false;
                    btn통관적용.Enabled = true;
                }
                cboLC_SelectionChangeCommitted(sender, e);
            }
        }

        private void cboLC_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            if (cboLC.SelectedValue.ToString() == "001")
            {
                if (cbo거래구분.SelectedValue.ToString() == "002")
                {
                    btn통관적용.Enabled = false;
                }
                if (cbo거래구분.SelectedValue.ToString() == "003")
                {
                    btn통관적용.Enabled = false;
                    btnLocalLC.Enabled = true;
                }
                if (cbo거래구분.SelectedValue.ToString() == "004" ||
                    cbo거래구분.SelectedValue.ToString() == "005")
                {
                    btn통관적용.Enabled = true;
                    btnLocalLC.Enabled = false;
                }
            }
            if (cboLC.SelectedValue.ToString() == "002")
            {
                if (cbo거래구분.SelectedValue.ToString() == "002")
                {
                    btn통관적용.Enabled = false;
                }
                if (cbo거래구분.SelectedValue.ToString() == "003")
                {
                    btn통관적용.Enabled = false;
                    btnLocalLC.Enabled = false;
                }
            }
        }

        private void btn적용_Click(object sender, EventArgs e)
        {
            String ColName = string.Empty, ColName2 = string.Empty;
            String Data = string.Empty, Data2 = string.Empty;
            RoundedButton roundedButton = (RoundedButton)sender;

            if (!추가모드여부)
            {
                return;
            }

            if (roundedButton.Name == this.btn창고적용.Name)
            {
                if (!추가모드여부)
                {
                    ShowMessage("이미입고되어수정불가합니다");
                    return;
                }
                ColName = "CD_SL";
                ColName2 = "NM_SL";
                Data = ctx창고.CodeValue;
                Data2 = ctx창고.CodeName;
            }
            else if (roundedButton.Name == this.btn관리번호적용.Name)
            {
                ColName = "DC_RMK2";
                Data = D.GetString(txt관리번호.Text);
            }

            DataRow[] dr = this._flexA.DataTable.Select("S = 'Y'");

            for (int row = 0; row < dr.Length; row++)
            {
                dr[row][ColName] = Data;
                if (ColName2 != string.Empty)
                    dr[row][ColName2] = Data2;
            }
        }

        private void chk바코드사용여부_CheckedChanged(object sender, EventArgs e)
        {
            if (chk바코드사용여부.Checked == true)
                txt바코드.Focus();
        }

        private void btn첨부파일_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt의뢰번호.Text == string.Empty) return;

                string cd_file_code = D.GetString(txt의뢰번호.Text + "_" + Global.MainFrame.LoginInfo.CompanyCode); //파일 PK설정 
                P_CZ_MA_FILE_SUB m_dlg = new P_CZ_MA_FILE_SUB(Global.MainFrame.LoginInfo.CompanyCode, "PU", "P_CZ_PU_REQ_REG_1", cd_file_code, "P_CZ_PU_REQ_REG_1");

                if (m_dlg.ShowDialog(this) == DialogResult.Cancel) return;
            }
            catch (Exception ex)
            {

                MsgEnd(ex);
            }
        }

        #endregion

        #region ♣ 사용 안함

        private void 부가세계산(DataRow row)
        {
            Decimal ldb_VatKr = 0, ldb_AmKr = 0, ldb_amEx = 0, ldb_AM = 0;   // Decimal ldb_UMkr = 0, ldb_UMEX = 0;
            String ls_FG_TAX = string.Empty;             //과세구분 
            Decimal rate_vat = D.GetDecimal(row["RT_VAT"]) == 0 ? 0 : D.GetDecimal(row["RT_VAT"]) / 100;  //과세율  

            Decimal 수량 = D.GetDecimal(row["QT_REQ_MM"]);
            Decimal 단가 = D.GetDecimal(row["UM_EX_PO"]);
            Decimal 환율 = D.GetDecimal(this._flexA["RT_EXCH"]) == 0 ? 1 : D.GetDecimal(this._flexA["RT_EXCH"]);

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
                if (의제부가세적용 == "100")
                    ldb_VatKr = Unit.원화금액(DataDictionaryTypes.PU, ldb_AM * rate_vat);
                else
                    ldb_VatKr = Decimal.Round(ldb_AM / (1 + rate_vat) * rate_vat, MidpointRounding.AwayFromZero); //부가세   
                ldb_AmKr = Unit.원화금액(DataDictionaryTypes.PU, ldb_AM - ldb_VatKr);   //원화금액   
                ldb_amEx = Unit.외화금액(DataDictionaryTypes.PU, ldb_AmKr / 환율);  // 외화금액 
            }
            else
            {
                ldb_amEx = Unit.외화금액(DataDictionaryTypes.PU, 수량 * 단가);  // 외화금액
                ldb_AmKr = Unit.원화금액(DataDictionaryTypes.PU, 수량 * 단가 * 환율);   //원화금액  
                ldb_VatKr = Unit.원화금액(DataDictionaryTypes.PU, ldb_AmKr * rate_vat); //부가세   
                ldb_AM = Unit.원화금액(DataDictionaryTypes.PU, ldb_AmKr + ldb_VatKr);   //총금액  
            }

            row["UM_EX_PO"] = 단가;                                                              //외화단가(발주단위)   

            row["UM_EX"] = 단가 / ((D.GetDecimal(row["RATE_EXCHG"]) == 0) ? 1 : D.GetDecimal(row["RATE_EXCHG"])); ;   //(D.GetDecimal(row["QT_PO"]) == 0) ? 0 : (단가 / D.GetDecimal(row["QT_PO"]));  //  외화단가( 재고단위)     
            row["UM"] = 단가 / ((D.GetDecimal(row["RATE_EXCHG"]) == 0) ? 1 : D.GetDecimal(row["RATE_EXCHG"])) * 환율;    //원화단가                                               

            row["AM_EXREQ"] = ldb_amEx;
            row["AM_EX"] = ldb_amEx;

            row["AM_REQ"] = ldb_AmKr;
            row["AM"] = ldb_AmKr;

            row["VAT"] = ldb_VatKr;
            row["AM_TOTAL"] = ldb_AmKr + ldb_VatKr;

            this._flexA.SumRefresh();
        }

        #endregion
    }
}
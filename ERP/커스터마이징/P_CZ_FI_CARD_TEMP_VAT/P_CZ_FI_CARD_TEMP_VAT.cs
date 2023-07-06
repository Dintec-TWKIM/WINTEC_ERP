using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.FI.전표;
using Duzon.ERPU.Grant;
using Duzon.ERPU.MF;
using Duzon.Windows.Print;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_FI_CARD_TEMP_VAT : PageBase
    {
        private P_CZ_FI_CARD_TEMP_VAT_BIZ _biz = new P_CZ_FI_CARD_TEMP_VAT_BIZ();
        private P_CZ_FI_CARD_TEMP_VAT_GW _gw = new P_CZ_FI_CARD_TEMP_VAT_GW();
        private Enum.상대계정전표처리설정 상대계정처리환경설정;

        private string _카드도움창설정 { get; set; }

        private string _부가세계정설정 { get; set; }

        private string _부가세계정 { get; set; }

        private string _전표회계일자 { get; set; }

        private string _사용자전표회계일자 { get; set; }

        private string _법인카드전표처리방식 { get; set; }

        private bool _그룹웨어뉴턴스사용여부 { get; set; }

        private string _카드가맹점거래처자동생성 { get; set; }

        public P_CZ_FI_CARD_TEMP_VAT()
        {
            this.InitializeComponent();

            this.상대계정처리환경설정 = this._biz.Get_CARDDOCU();
            
            int num;
            if (Enum.상대계정전표처리설정.계좌번호연결사용 == this.상대계정처리환경설정)
                num = !MA.ServerKey(false, "NICEIT", "MNS", "DKTE", "KORAIL", "MBIKOREA", "SSCARD", "MTCN", "ATECS") ? 1 : 0;
            else
                num = 0;
            
            if (num != 0) return;
            
            this.MainGrids = new Dass.FlexGrid.FlexGrid[1] { this._flex };
            this.DataChanged += new EventHandler(this.Page_DataChanged);
        }

        protected override void InitLoad()
        {
            base.InitLoad();
            this.InitEvent();
            this.InitOneGrid();
            this.InitEnv();
            this.InitGrid();
        }

        private void InitEvent()
        {
            this.btn부가세처리.Click += new EventHandler(this.부가세처리미처리_Click);
            this.btn부가세미처리.Click += new EventHandler(this.부가세처리미처리_Click);
            this.btn전표처리.Click += new EventHandler(this.전표처리_Click);
            this.btn전표취소.Click += new EventHandler(this.전표취소_Click);
            this.btn전표조회.Click += new EventHandler(this.전표조회_Click);
            this.btn전표처리적용.Click += new EventHandler(this.On_Click);
            this.btn전표처리미적용.Click += new EventHandler(this.On_Click);
            this.btn데이터확인.Click += new EventHandler(this.btn데이터확인_Click);
            this.btn거래처등록.Click += new EventHandler(this.btn거래처등록_Click);
            this.btn업종적용.Click += new EventHandler(this.btn_업종적용_Click);
            this.btn데이터삭제.Click += new EventHandler(this.btn_데이터삭제_Click);
            this.btn결의서.Click += new EventHandler(this.btn결의서_Click);
            this.btnVAT제외계정등록.Click += new EventHandler(this.btn_VAT제외계정_Click);
            this.btn카드전표마감.Click += new EventHandler(this.btn카드전표마감_Click);
            this.btn전표번호매핑.Click += new EventHandler(this.btn전표번호매핑_Click);
            this.btn전용4자.Click += new EventHandler(this.btnCust4_Click);
            this.btn전용버튼6자.Click += new EventHandler(this.btnCust1_Click);
            this.btn전용버튼8자.Click += new EventHandler(this.btnCust2_Click);
            this.btn전용버튼10자.Click += new EventHandler(this.btnCust3_Click);
            this.btn일괄복사.Click += new EventHandler(this.btn일괄복사_Click);
			this.btn전자결재.Click += Btn전자결재_Click;

            this.bpc작성부서.QueryBefore += new BpQueryHandler(this.BpControl_QueryBefore);
            this.bpc카드번호.QueryBefore += new BpQueryHandler(this.BpControl_QueryBefore);
            this.ctx증빙.QueryBefore += new BpQueryHandler(this.BpControl_QueryBefore);
        }

        private void Btn전자결재_Click(object sender, EventArgs e)
        {
            DataSet ds;
            DataTable dt1, dt2, dt3;
            DataRow header;

            try
            {
                header = this._flex.GetDataRow(this._flex.Row);

                if (string.IsNullOrEmpty(header["NO_DOCU"].ToString()))
                {
                    this.ShowMessage("CZ_전표 처리되지 않은 건이 포함되어 있습니다.");
                    return;
                }

                if (string.IsNullOrEmpty(header["NO_EMPMNG"].ToString()))
				{
                    this.ShowMessage("부서정보등록 -> 부서장 정보가 등록되어 있지 않습니다.");
                    return;
                }

                ds = this._biz.전표출력(Global.MainFrame.LoginInfo.CdPc, header["NO_DOCU"].ToString());

                dt1 = ds.Tables[0];
                dt2 = ds.Tables[1];

                dt3 = ComFunc.getGridGroupBy(dt2, new string[] { "NO_DOCU", "CD_ACCT", "NM_ACCT", "NM_NOTE" }, true);
                dt3.Columns.Add("AM_DR");
                dt3.Columns.Add("AM_CR");

                foreach (DataRow dr1 in dt3.Rows)
                {
                    string filter = "NO_DOCU = '" + D.GetString(dr1["NO_DOCU"]) + "' AND CD_ACCT = '" + D.GetString(dr1["CD_ACCT"]) + "'";
                    dr1["AM_DR"] = dt2.Compute("SUM(AM_DR)", filter);
                    dr1["AM_CR"] = dt2.Compute("SUM(AM_CR)", filter);
                }

                if (this._gw.전자결재(header, dt1, dt2, dt3))
                {
                    ShowMessage(공통메세지._작업을완료하였습니다, this.DD("전자결재"));
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            if (this._카드도움창설정 == "*")
            {
                this.ShowMessage("은행연동 환경설정이 필요합니다.");
                this.SetToolBarButtonState(false, false, false, false, false);
                this.CallOtherPageMethod("P_MA_ENV_FI", this.DD("회계관리환경설정") + "(" + this.PageName + ")", this.Grant, new object[] { this.PageID });
            }
            else
            {
                this.btn부가세미처리.Visible = this.btn부가세처리.Visible = this.Use부가세;
                this.dtp승인일자.Mask = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.FI, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
                this.dtp승인일자.StartDateToString = Global.MainFrame.GetStringFirstDayInMonth;
                this.dtp승인일자.EndDateToString = Global.MainFrame.GetStringToday;

                SetControl setControl = new SetControl();
                setControl.SetCombobox(this.cbo부가세구분, Duzon.ERPU.FI.FI.GetCode("FI_E000044", true));
                setControl.SetCombobox(this.cbo승인구분, Duzon.ERPU.FI.FI.GetCode("FI_E000016", true));
                setControl.SetCombobox(this.cbo전표처리, Duzon.ERPU.FI.FI.GetCode("FI_C000002", true));
                setControl.SetCombobox(this.cbo전표승인여부, Duzon.ERPU.FI.FI.GetCode("FI_J000003", true));
                setControl.SetCombobox(this.cbo카드사명, MA.GetCode("FI_T000016", true));
                setControl.SetCombobox(this.cbo그룹웨어처리, MA.GetCodeUser(new string[] { "Y", "N" }, new string[] { "처리", "미처리" }, true));

                this._flex.SetDataMap("ADMIN_GU", Duzon.ERPU.FI.FI.GetCode("FI_E000016"), "CODE", "NAME");
                this._flex.SetDataMap("DOCU_STAT", Duzon.ERPU.FI.FI.GetCode("FI_C000002"), "CODE", "NAME");
                this._flex.SetDataMap("VAT_TYPE", Duzon.ERPU.FI.FI.GetCode("FI_E000044"), "CODE", "NAME");
                this._flex.SetDataMap("ST_DOCU", Duzon.ERPU.FI.FI.GetCode("FI_J000003"), "CODE", "NAME");
                this._flex.SetDataMap("TP_DATE", MA.GetCodeUser(new string[] { "1", "2" }, new string[] { "평일", "휴일" }), "CODE", "NAME");
                
                if (this._flex.Cols.Contains("ST_GWARE"))
                {
                    if (MA.ServerKey(false, "KFI"))
                        this._flex.SetDataMap("ST_GWARE", MA.GetCode("FI_Z_KFI03"), "CODE", "NAME");
                    else
                        this._flex.SetDataMap("ST_GWARE", MA.GetCode("FI_J000031"), "CODE", "NAME");
                }
                
                if (Enum.상대계정전표처리설정.비용계정사용 == this.상대계정처리환경설정)
                    this.btn일괄복사.Visible = true;
                
                UGrant ugrant = new UGrant();
                ugrant.GrantButtonEnble("P_FI_CARD_TEMP_VAT", "JOURNALAPP", this.btn전표처리적용, true);
                ugrant.GrantButtonEnble("P_FI_CARD_TEMP_VAT", "JOURNALUNP", this.btn전표처리미적용, true);
                ugrant.GrantButtonEnble("P_FI_CARD_TEMP_VAT", "TAXTREAT", this.btn부가세처리, true);
                ugrant.GrantButtonEnble("P_FI_CARD_TEMP_VAT", "VATUNTREAT", this.btn부가세미처리, true);
                ugrant.GrantButtonEnble("P_FI_CARD_TEMP_VAT", "JOURNALPR", this.btn전표처리, true);
                ugrant.GrantButtonEnble("P_FI_CARD_TEMP_VAT", "JOURNALCC", this.btn전표취소, true);
                ugrant.GrantButtonEnble("P_FI_CARD_TEMP_VAT", "JOURNALVW", this.btn전표조회, true);
                ugrant.GrantButtonEnble("P_FI_CARD_TEMP_VAT", "DATA", this.btn데이터확인, true);
                ugrant.GrantButtonVisible("P_FI_CARD_TEMP_VAT", "COPY", this.btn일괄복사);
                ugrant.GrantButtonVisible("P_FI_CARD_TEMP_VAT", "PARTNER", this.btn거래처등록);

                if (this._카드도움창설정 == "1")
                {
                    this.bpc작성부서.Clear();
                    this.bpc작성부서.Enabled = false;
                }

                int num;
                if (!this._그룹웨어뉴턴스사용여부)
                {
                    if (!MA.ServerKey(false, "KAPES"))
                    {
                        num = 1;
                        goto label_18;
                    }
                }
                num = MA.ServerKey(false, "DBEXP") ? 1 : 0;
label_18:
                if (num == 0)
                {
                    this.bppnl그룹웨어처리.Visible = true;
                    this._flex.SetDataMap("GW_SEND_YN", MA.GetCodeUser(new string[] { "N", "Y" }, new string[] { "미처리", "처리" }, false), "CODE", "NAME");
                }

                if (MA.ServerKey(false, "MTCN", "POINTI"))
                    this.btn일괄복사.Visible = true;
                
                if (MA.ServerKey(false, "DOOSANFEED", "KFC"))
                {
                    this.btn업종적용.Visible = true;
                    ugrant.GrantButtonEnble("P_FI_CARD_TEMP_VAT", "JOB", this.btn업종적용, true);
                }
                
                if (MA.ServerKey(false, "SOLIDTECH", "SOLIDTECH1"))
                {
                    this.btn결의서.Visible = true;
                    setControl.SetCombobox(this.cboCust, MA.GetCodeUser(new string[] { "Y", "N" }, new string[] { "처리", "미처리" }, true));
                    this.bppnlCustCombo.Visible = true;
                    this.lblCustCombo.Text = "결의서처리여부";
                }

                if (MA.ServerKey(false, "NICEIT1", "NICEIT11"))
                    this.bppnl비용계정.Visible = true;
                
                if (MA.ServerKey(false, "CCENERGY"))
                    this.bppnl청구년월.Visible = false;
                
                if (MA.ServerKey(false, "HAATZ"))
                {
                    this.btn카드전표마감.Visible = true;
                    ugrant.GrantButtonEnble("P_FI_CARD_TEMP_VAT", "CARDCLOSE", this.btn카드전표마감, true);
                }
                
                if (MA.ServerKey(false, "SSCARD"))
                {
                    this.btn전표번호매핑.Visible = true;
                    this.btn전용4자.Visible = true;
                    this.btn전용4자.Text = "매핑취소";
                    this._flex.SetDataMap("CD_USERDEF3", MA.GetCodeUser(new string[] { "Y", "N" }, new string[] { "매핑", string.Empty }), "CODE", "NAME");
                }

                if (MA.ServerKey(false, "CBLGT"))
                {
                    DataRow userCard = this._biz.GetUserCard(new List<object>() { MA.Login.회사코드, string.Empty, MA.Login.사용자아이디 }.ToArray());
                    if (userCard != null)
                        this.bpc카드번호.AddItem(userCard["ACCT_NO"].ToString(), userCard["NM_CARD"].ToString());
                }

                if (MA.ServerKey(false, "KMB"))
                    this.btn데이터삭제.Visible = true;
                
                if (MA.ServerKey(false, "MNS"))
                    this.btnVAT제외계정등록.Visible = true;
                
                if (MA.ServerKey(false, "ISEC"))
                {
                    this.btn전용버튼6자.Visible = true;
                    this.btn전용버튼8자.Visible = true;
                    this.btn전용버튼6자.Text = "그룹웨어처리";
                    this.btn전용버튼8자.Text = "그룹웨어미처리";
                    this._flex.SetDataMap("GW_SEND_YN", MA.GetCode("FI_Z_ISEC1", false), "CODE", "NAME");
                }

                if (MA.ServerKey(false, "KAIMRO"))
                {
                    this.btn전용버튼8자.Visible = true;
                    this.btn전용버튼10자.Visible = true;
                    this.btn전용버튼8자.Text = "개인사용분적용";
                    this.btn전용버튼10자.Text = "개인사용분적용해제";
                    this._flex.SetDataMap("CD_USERDEF1", MA.GetCode("FI_ZKAI007", false), "CODE", "NAME");
                    setControl.SetCombobox(this.cboCust, MA.GetCode("FI_ZKAI007", true));
                    this.cboCust.SelectedValue = "2";
                    this.bppnlCustCombo.Visible = true;
                    this.lblCustCombo.Text = "카드사용구분";
                }

                if (MA.ServerKey(false, "DBEXP"))
                    this.bppnl그룹웨어처리.Visible = false;
                
                if (MA.ServerKey(false, "FJK"))
                    this.btn거래처등록.Visible = true;
                
                if (MA.ServerKey(false, "HANUL"))
                    this.ctx증빙.SetCode("1", "신용카드(법인)");
                
                if (MA.ServerKey(false, "AVLFNC"))
                {
                    this.btn전용4자.Visible = true;
                    this.btn전용4자.Text = "휴폐업";
                }

                if (!MA.ServerKey(false, "NOSAORKR"))
                    return;
                
                this.btn부가세처리.Visible = false;
                this.btn부가세미처리.Visible = false;
            }
        }

        private void InitOneGrid()
        {
            this.oneGrid.UseCustomLayout = true;
            this.oneGrid.CustomLayoutDefaultRowCount = !MA.ServerKey(false, "SOLIDTECH", "NICEIT1", "KAIMRO") ? 4 : 5;
            this.oneGrid.InitCustomLayout();
            this.bpPnl_DT.IsNecessaryCondition = true;
        }

        private void InitEnv()
        {
            시스템환경설정 시스템환경설정 = new 시스템환경설정();
            this._카드도움창설정 = 시스템환경설정.Get은행연동환경설정("TP_GR_CARD");
            this._부가세계정설정 = 시스템환경설정.Get은행연동환경설정("TP_DOCU");
            this._부가세계정 = 시스템환경설정.Get은행연동환경설정("TP_DOCU_CD_ACCT");
            this._전표회계일자 = 시스템환경설정.Get은행연동환경설정("TP_DTACCT_CARD");
            this._사용자전표회계일자 = 시스템환경설정.Get은행연동환경설정("TP_DOCU_DT_ACCT");
            this._법인카드전표처리방식 = 시스템환경설정.Get은행연동환경설정("SYS_CARD");
            this._그룹웨어뉴턴스사용여부 = 시스템환경설정.Get환경설정("TP_GWARED", false) == "7";
            this._카드가맹점거래처자동생성 = 시스템환경설정.Get은행연동환경설정("TP_PARTNER");
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);
            this._flex.SetCol("ADMIN_GU_Y", "ADMIN_GU_Y", false);
            this._flex.SetCol("ADMIN_GU_N", "ADMIN_GU_N", false);
            this._flex.SetCol("NO_CARD_G", "NO_CARD_G", false);
            this._flex.SetCol("NODE_CODE", "회계단위", false);
            this._flex.SetCol("S", "S", 30, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("NO_CARD", "법인카드", 150);
            this._flex.SetCol("NM_CARD", "카드명", false);
            this._flex.SetCol("NM_DEPT", "관리부서", 120);
            this._flex.SetCol("NM_OWNER", "소유자", 100);

            if (MA.ServerKey(false, "POSWITH"))
                this._flex.SetCol("ETC_CARD", "비고", 100);
            
            this._flex.SetCol("TRADE_DATE", "승인일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("TRADE_TIME", "승인시간", 100);
            this._flex.SetCol("CLIENT_NOTE", "사용내역", false);
            this._flex.SetCol("TRADE_PLACE", "가맹점", 150);
            this._flex.SetCol("MERC_ADDR", "가맹점주소", 200);
            this._flex.SetCol("TRADE_PLACE_TEL", "가맹점전화번호", 100);
            this._flex.SetCol("TRADE_PLACE_POST", "가맹점우편번호", 100);
            this._flex.SetCol("ADMIN_AMT", "승인금액", 120, false, typeof(decimal), FormatTpType.MONEY);
            
            if (MA.ServerKey(false, "NICEIT1", "NICEIT11"))
            {
                this._flex.SetCol("CD_COSTACCT", "계정코드", 80, true);
                this._flex.SetCol("NM_COSTACCT", "계정명", 80, false);
                this._flex.SetCol("NM_NOTE", "비용계정적요", 120, true);
            }
            
            this._flex.SetCol("VAT_TYPE", "부가세구분", 100);
            
            if (MA.ServerKey(false, "DOOSANFEED", "KFC"))
            {
                this._flex.SetCol("NM_ACCTCOST", "비용계정", 80, false);
                this._flex.SetCol("CD_ACCT", "회계계정코드", 80, false);
                this._flex.SetCol("NM_ACCT", "회계계정명", 80, false);
            }
            else
                this._flex.SetCol("YN_VAT", "부가세여부", 80, true, CheckTypeEnum.Y_N);
            
            if (MA.ServerKey(false, "DKTE"))
            {
                this._flex.SetCol("CD_USERDEF1", "경비유형코드", 100, true);
                this._flex.SetCol("CD_USERDEF2", "경비유형내역", 100, false);
                this._flex.SetCol("CD_COSTACCT", "상대계정코드", 100, false);
                this._flex.SetCol("NM_COSTACCT", "상대계정명", 100, false);
            }
            
            if (MA.ServerKey(false, "KPCI"))
                this._flex.SetCol("MCC_CODE_NAME", "업종", false);
            else
                this._flex.SetCol("MCC_CODE_NAME", "업종", 100);
            
            int num1;
            if (Enum.상대계정전표처리설정.상대계정처리유형사용 == this.상대계정처리환경설정)
                num1 = MA.ServerKey(false, "NICEIT", "DOOSANFEED", "KFC", "DKTE", "MNS", "KORAIL", "MTCN") ? 1 : 0;
            else
                num1 = 1;
            
            if (num1 == 0)
            {
                this._flex.SetCol("CD_TPACCT", "상대계정처리유형코드", false);
                this._flex.SetCol("NM_TPACCT", "상대계정처리유형명", 120, true);
                this._flex.SetCol("CD_COSTACCT", "상대계정코드", 100, false);
                this._flex.SetCol("NM_COSTACCT", "상대계정명", 100, false);
            }
            
            int num2;
            if (Enum.상대계정전표처리설정.비용계정사용 == this.상대계정처리환경설정)
                num2 = MA.ServerKey(false, "NICEIT", "DOOSANFEED", "KFC", "DKTE", "MNS", "KORAIL", "MTCN") ? 1 : 0;
            else
                num2 = 1;
            
            if (num2 == 0)
            {
                this._flex.SetCol("CD_COSTACCT", "상대계정코드", 100, true);
                this._flex.SetCol("NM_COSTACCT", "상대계정명", 100, false);
                this._flex.SetCol("CD_CC", "코스트센터", 100, true);
                this._flex.SetCol("NM_CC", "코스트센터명", 100, false);
                this._flex.SetCol("CD_PJT", "프로젝트", 100, true);
                this._flex.SetCol("NM_PJT", "프로젝트명", 100, false);
                this._flex.Cols["CD_CC"].Visible = false;
                this._flex.Cols["NM_CC"].Visible = false;
                this._flex.Cols["CD_PJT"].Visible = false;
                this._flex.Cols["NM_PJT"].Visible = false;
            }
            
            if (MA.ServerKey(false, "MTCN"))
            {
                this._flex.SetCol("CD_COSTACCT", "상대계정코드", 100, true);
                this._flex.SetCol("NM_COSTACCT", "상대계정명", 80, false);
                this._flex.SetCol("CD_CC", "코스트센터", false);
                this._flex.SetCol("NM_CC", "코스트센터명", 100, true);
            }
            
            if (MA.ServerKey(false, "SOLIDTECH", "SOLIDTECH1"))
                this._flex.SetCol("CD_USERDEF1", "결의서번호", 120, false);
            
            this._flex.SetCol("S_IDNO", "사업자등록번호", 120);
            this._flex.SetCol("SUPPLY_AMT", "공급가액", 120, false, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("VAT_AMT", "부가세", 120, false, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("SERVICE_CHARGE", "봉사료", 120, false, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("ADMIN_NO", "승인번호", 100);
            this._flex.SetCol("ADMIN_GU", "구분", 50);
            
            if (MA.ServerKey(false, "MNS"))
            {
                this._flex.SetCol("CD_COSTACCT", "상대계정코드", 100, true);
                this._flex.SetCol("NM_COSTACCT", "상대계정명", 80, false);
                this._flex.SetCol("CD_CC", "코스트센터", false);
                this._flex.SetCol("NM_CC", "코스트센터명", 100, true);
                this._flex.SetCol("NM_NOTE", "적요", 100, true);
            }
            
            if (MA.ServerKey(false, "KORAIL"))
            {
                this._flex.SetCol("CD_COSTACCT", "상대계정코드", 100, true);
                this._flex.SetCol("NM_COSTACCT", "상대계정명", 100, false);
                this._flex.SetCol("NM_NOTE", "적요", 100, true);
                this._flex.SetCol("CD_USERDEF1", "사용자코드", 80, true);
                this._flex.SetCol("NM_USERDEF1", "사용자", 80, false);
            }

            this._flex.SetCol("DOCU_STAT", "전표처리", 80);
            this._flex.SetCol("NO_DOCU_LINE_NO", "전표정보", 120);
            this._flex.SetCol("ASK_MON", "청구년월", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("ST_DOCU", "전표승인여부", 100);
            this._flex.SetCol("NM_GW_STATUS", "결재상태", 100);
            this._flex.SetCol("TRADE_PLACE_ADDR", "가맹점주소2", false);
            this._flex.SetCol("TP_DATE", "휴일정보", false);
            
            if (MA.ServerKey(false, "DBCAS"))
                this._flex.SetCol("CHECK", "CHECK", false);
            
            if (!MA.ServerKey(false, "DBEXP"))
                this._flex.SetCol("GW_SEND_YN", "그룹웨어처리", 90);
            
            this._flex.SetCol("MERC_REPR", "대표자", false);
            
            if (MA.ServerKey(false, "SKCHEMICAL"))
                this._flex.SetCol("TRAN_AMT", "국외사용원금", 120, false, typeof(decimal), FormatTpType.MONEY);
            
            if (MA.ServerKey(false, "KPCI"))
            {
                this._flex.SetCol("AQUI_RATE", "환율", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flex.SetCol("TRAN_AMT", "국외사용원금", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flex.Cols["AQUI_RATE"].Format = "0.####";
                this._flex.Cols["TRAN_AMT"].Format = "0.####";
            }
            
            if (MA.ServerKey(false, "ATECS"))
            {
                this._flex.SetCol("AQUI_RATE", "환율", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flex.SetCol("TRAN_AMT", "국외사용원금", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flex.SetCol("NM_NOTE", "적요", 100, true);
                this._flex.Cols["AQUI_RATE"].Format = "0.####";
                this._flex.Cols["TRAN_AMT"].Format = "0.####";
            }
            
            if (MA.ServerKey(false, "SOLIDTECH", "SOLIDTECH1"))
            {
                this._flex.SetCol("MCC_PARTTYPE", "업종유형", 80, true);
                this._flex.SetCol("VAT_TYPE1", "과세구분", 80, true);
                this._flex.SetCol("SINGO_TYPE", "신고대상분류", 100, true);
            }
            
            if (MA.ServerKey(false, "CFINC"))
                this._flex.SetCol("TRAN_AMT", "국외사용원금", 120, false, typeof(decimal), FormatTpType.MONEY);
            
            if (MA.ServerKey(false, "PUBLIC", "KFI", "EGITS", "SFZ", "DBEXP"))
                this._flex.SetCol("ST_GWARE", "결재상태", 65, false);
            
            if (MA.ServerKey(false, "HHIAS", "HHIMOS", "HHIGREEN"))
            {
                this._flex.SetCol("CURRENCY", "환종", 80, false, typeof(string), FormatTpType.FOREIGN_UNIT_COST);
                this._flex.SetCol("AQUI_RATE", "환율", 80, false, typeof(string), FormatTpType.EXCHANGE_RATE);
                this._flex.SetCol("AQUI_DOLL", "외화금액", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            }
            
            if (MA.ServerKey(false, "SSCARD"))
            {
                this._flex.SetCol("NM_NOTE", "적요", 100, true);
                this._flex.SetCol("CD_USERDEF3", "매핑여부", 80, false);
            }
            
            if (MA.ServerKey(false, "ISEC", "KINFRA", "IGISAM"))
            {
                this._flex.SetCol("EXP_DATE", "결재일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                this._flex.SetCol("SEQ", "순번", 80, false);
                this._flex.SetCol("AQUI_WON", "원", 120, false, typeof(decimal), FormatTpType.MONEY);
                this._flex.SetCol("TRAN_AMT", "국외사용원금", 120, false, typeof(decimal), FormatTpType.MONEY);
                this._flex.SetCol("FORE_USE", "국내/해외", 80, false);
            }
            
            if (MA.ServerKey(false, "NASM"))
            {
                this._flex.SetCol("EXP_DATE", "결재일자", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                this._flex.SetCol("SEQ", "순번", 80, false);
                this._flex.SetCol("AQUI_WON", "원", 120, false, typeof(decimal), FormatTpType.MONEY);
                this._flex.SetCol("TRAN_AMT", "국외사용원금", 120, false, typeof(decimal), FormatTpType.MONEY);
                this._flex.SetCol("FORE_USE", "국내/해외", 80, false);
            }
            
            if (MA.ServerKey(false, "KAIMRO"))
            {
                this._flex.SetCol("AQUI_RATE", "환율", 80, false, typeof(string), FormatTpType.EXCHANGE_RATE);
                this._flex.SetCol("AQUI_DOLL", "외화금액", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flex.SetCol("CD_USERDEF1", "카드사용구분 ", 100, false);
            }
            
            if (MA.ServerKey(false, "SEMICS", "POINTI", "CROEN", "SJMT"))
                this._flex.SetCol("NM_NOTE", "적요", 100, true);
            
            if (MA.ServerKey(false, "SEAHST"))
                this._flex.SetCol("AQUI_WON", "원", 120, false, typeof(decimal), FormatTpType.MONEY);
            
            if (MA.ServerKey(false, "HANUL"))
            {
                this._flex.SetCol("CD_CC", "코스트센터", 100, false);
                this._flex.SetCol("NM_CC", "코스트센터명", 100, false);
            }
            
            if (MA.ServerKey(true, "WITHU"))
                this._flex.SetCol("CD_USERDEF1", "결제대행여부", 100, true);
            
            if (!MA.ServerKey(false, "SKCHEMICAL", "KPCI", "ATECS", "CFINC", "ISEC", "IGISAM", "NASM"))
            {
                this._flex.SetCol("TRAN_AMT", "국외사용금액", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flex.Cols["TRAN_AMT"].Visible = false;
                this._flex.Cols["TRAN_AMT"].Format = "0.####";
            }
            
            if (!MA.ServerKey(false, "HHIAS", "HHIMOS", "HHIGREEN"))
            {
                this._flex.SetCol("CURRENCY", "환종", 80, false, typeof(string), FormatTpType.FOREIGN_UNIT_COST);
                this._flex.Cols["CURRENCY"].Visible = false;
            }
            
            if (!MA.ServerKey(false, "HHIAS", "HHIMOS", "HHIGREEN", "KAIMRO", "KPCI", "ATECS"))
            {
                this._flex.SetCol("AQUI_RATE", "환율", 80, false, typeof(string), FormatTpType.EXCHANGE_RATE);
                this._flex.Cols["AQUI_RATE"].Format = "0.####";
                this._flex.Cols["AQUI_RATE"].Visible = false;
            }
            
            if (!MA.ServerKey(false, "ISEC", "KINFRA", "IGISAM", "NASM"))
            {
                this._flex.SetCol("FORE_USE", "국내/해외", 80, false);
                this._flex.Cols["FORE_USE"].Visible = false;
            }

            this._flex.SetCol("NO_PO", "발주번호", false);
            this._flex.SetCol("NO_IO", "입고번호", false);
            this._flex.SetCol("NO_IV", "매입번호", false);
            this._flex.SetCol("NO_DOCU_PH", "전표번호", false);

            if (Global.MainFrame.LoginInfo.CompanyCode != "K200")
                this._flex.SetCol("DT_ACCT_BAN", "회계일자(반제)", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            this._flex.Cols["TRADE_TIME"].Format = "00:00:00";
            this._flex.Cols["TRADE_TIME"].TextAlign = TextAlignEnum.CenterCenter;
            this._flex.Cols["MERC_ADDR"].Visible = false;
            this._flex.Cols["S_IDNO"].Format = "000-00-00000";
            this._flex.Cols["S_IDNO"].TextAlign = TextAlignEnum.CenterCenter;
            this._flex.Cols["ASK_MON"].Visible = false;
            this._flex.Cols["TRADE_PLACE_TEL"].TextAlign = TextAlignEnum.CenterCenter;
            this._flex.SetDummyColumn("S", "VAT_TYPE", "YN_VAT");
            this._flex.SetStringFormatCol(new string[] { "TRADE_TIME", "S_IDNO" });
            this._flex.Cols.Frozen = this._flex.Cols["NO_CARD"].Index;
            this._flex.CellNoteInfo.EnabledCellNote = true;
            this._flex.CellNoteInfo.CategoryID = this.Name;
            this._flex.CellNoteInfo.DisplayColumnForDefaultNote = "NO_CARD";
            this._flex.CheckPenInfo.EnabledCheckPen = true;
            this._flex.SettingVersion = "1.0.4.4";

            this._flex.StartEdit += new RowColEventHandler(this.Flex_StartEdit);
            this._flex.HelpClick += new EventHandler(this.Flex_HelpClick);
            this._flex.CheckHeaderClick += new EventHandler(this.Flex_CheckHeaderClick);
            this._flex.CellContentChanged += new CellContentEventHandler(this._flex_CellContentChanged);
            this._flex.BeforeShowContextMenu += new EventHandler(this._flex_BeforeShowContextMenu);

            if (Enum.상대계정전표처리설정.상대계정처리유형사용 == this.상대계정처리환경설정)
            {
                this._flex.SetCodeHelpCol("NM_TPACCT", "H_FI_HELP02", ShowHelpEnum.Always, new string[] { "CD_TPACCT", "NM_TPACCT", "CD_COSTACCT", "NM_COSTACCT" }, new string[] { "CD_TPACCT", "NM_TPACCT", "CD_ACCT", "NM_ACCT" });
                this._flex.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex_BeforeCodeHelp);
            }

            if (Enum.상대계정전표처리설정.비용계정사용 == this.상대계정처리환경설정)
            {
                this._flex.SetCodeHelpCol("CD_COSTACCT", HelpID.P_FI_ACCTCODE_SUB, ShowHelpEnum.Always, new string[] { "CD_COSTACCT", "NM_COSTACCT" }, new string[] { "CD_ACCT", "NM_ACCT" }, ResultMode.FastMode);
                this._flex.SetCodeHelpCol("CD_CC", HelpID.P_MA_CC_SUB, ShowHelpEnum.Always, new string[] { "CD_CC", "NM_CC" }, new string[] { "CD_CC", "NM_CC" }, ResultMode.FastMode);
                this._flex.SetCodeHelpCol("CD_PJT", HelpID.P_SA_PROJECT_SUB, ShowHelpEnum.Always, new string[] { "CD_PJT", "NM_PJT" }, new string[] { "NO_PROJECT", "NM_PROJECT" }, ResultMode.FastMode);
                this._flex.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex_BeforeCodeHelp);
            }

            if (MA.ServerKey(false, "NICEIT1", "NICEIT11"))
                this._flex.SetCodeHelpCol("CD_COSTACCT", HelpID.P_FI_ACCTCODE_SUB, ShowHelpEnum.Always, new string[] { "CD_COSTACCT", "NM_COSTACCT" }, new string[] { "CD_ACCT", "NM_ACCT" }, ResultMode.FastMode);
            
            if (MA.ServerKey(false, "MNS", "MTCN"))
            {
                this._flex.SetCodeHelpCol("CD_COSTACCT", HelpID.P_FI_ACCTCODE_SUB, ShowHelpEnum.Always, new string[] { "CD_COSTACCT", "NM_COSTACCT" }, new string[] { "CD_ACCT", "NM_ACCT" }, ResultMode.FastMode);
                this._flex.SetCodeHelpCol("NM_CC", HelpID.P_MA_CC_SUB, ShowHelpEnum.Always, new string[] { "CD_CC", "NM_CC" }, new string[] { "CD_CC", "NM_CC" }, ResultMode.FastMode);
            }

            if (MA.ServerKey(false, "KORAIL"))
            {
                this._flex.SetCodeHelpCol("CD_COSTACCT", HelpID.P_FI_ACCTCODE_SUB, ShowHelpEnum.Always, new string[] { "CD_COSTACCT", "NM_COSTACCT" }, new string[] { "CD_ACCT", "NM_ACCT" }, ResultMode.FastMode);
                this._flex.SetCodeHelpCol("CD_USERDEF1", HelpID.P_MA_EMP_SUB, ShowHelpEnum.Always, new string[] { "CD_USERDEF1", "NM_USERDEF1" }, new string[] { "NO_EMP", "NM_KOR" }, ResultMode.FastMode);
                this._flex.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex_BeforeCodeHelp);
                this._flex.StartEdit += new RowColEventHandler(this._flex_StartEdit);
            }

            if (MA.ServerKey(false, "DKTE"))
            {
                this._flex.SetCodeHelpCol("CD_USERDEF1", "H_FI_HELP02", ShowHelpEnum.Always, new string[] { "CD_USERDEF1", "CD_USERDEF2", "CD_COSTACCT", "NM_COSTACCT" }, new string[] { "CD_COSTYH", "NM_COSTYH", "CD_ACCT", "NM_ACCT" });
                this._flex.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);
                this._flex.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex_BeforeCodeHelp);
            }

            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.None);
        }

        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch() || !this.승인일자체크 || this._카드도움창설정 == "1" && !this.GetCard)
                return false;

            return !MA.ServerKey(false, "TCHP") || this.GetCard;
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeSearch()) return;
                if (MA.ServerKey(false, "SEAHST") && new P_FI_Z_SEAHST_NOTICE_SUB().ShowDialog() != DialogResult.OK) return;
                
                this.Search();

                if (!this._flex.HasNormalRow)
                    this.ShowMessage(PageResultMode.SearchNoData);
                else
                {
                    this.GridColumnColorSetting();
                    this.ToolBarPrintButtonEnabled = true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool BeforeDelete()
        {
            return MA.ServerKey(false, "MBIKOREA");
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                DataRow[] dataRowArray = this._flex.DataTable.Select("S='Y' AND TP_SORT='1' AND DOCU_STAT = '0' AND GW_SEND_YN = 'N'", "", DataViewRowState.CurrentRows);

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage("삭제할 수 있는 자료가 없습니다.");
                }
                else
                {
                    this._flex.Redraw = false;
                    
                    foreach (DataRow dataRow in dataRowArray)
                        dataRow.Delete();
                    
                    this._flex.Redraw = true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool BeforeSave()
        {
            return base.BeforeSave();
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeSave() || !this.MsgAndSave(PageActionMode.Save))
                    return;
                
                this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            if (!base.SaveData() || !this.Verify())
                return false;

            DataTable changes = this._flex.GetChanges();
            
            if (changes == null || changes.Rows.Count == 0 || !this._biz.Save(changes))
                return false;
            
            this._flex.AcceptChanges();
            
            return true;
        }

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            DataSet ds;
            DataTable dt1, dt2, dt3;
            DataRow header;

            try
            {
                base.OnToolBarPrintButtonClicked(sender, e);

                header = this._flex.GetDataRow(this._flex.Row);

                if (string.IsNullOrEmpty(header["NO_DOCU"].ToString()))
                {
                    this.ShowMessage("CZ_전표 처리되지 않은 건이 포함되어 있습니다.");
                    return;
                }

                ds = this._biz.전표출력(Global.MainFrame.LoginInfo.CdPc, header["NO_DOCU"].ToString());

                dt1 = ds.Tables[0];
                dt2 = ds.Tables[1];

                dt3 = ComFunc.getGridGroupBy(dt2, new string[] { "NO_DOCU", "CD_ACCT", "NM_ACCT", "NM_NOTE" }, true);
                dt3.Columns.Add("AM_DR");
                dt3.Columns.Add("AM_CR");
                dt3.DefaultView.Sort = "CD_ACCT ASC";

                foreach (DataRow dr1 in dt3.Rows)
                {
                    string filter = "NO_DOCU = '" + D.GetString(dr1["NO_DOCU"]) + "' AND CD_ACCT = '" + D.GetString(dr1["CD_ACCT"]) + "' AND NM_NOTE = '" + D.GetString(dr1["NM_NOTE"]) + "'";
                    dr1["AM_DR"] = dt2.Compute("SUM(AM_DR)", filter);
                    dr1["AM_CR"] = dt2.Compute("SUM(AM_CR)", filter);
                }

                this._gw.미리보기(header, dt1, dt2, dt3);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void 부가세처리미처리_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow) return;

                DataTable table = new DataView(this._flex.DataTable, "S = 'Y' AND TP_SORT='1'", "", DataViewRowState.CurrentRows).ToTable();
                if (table == null || table.Rows.Count == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    DataRow[] dataRowArray1 = table.Select("DOCU_STAT = '1'");
                    if (dataRowArray1 != null && dataRowArray1.Length > 0)
                    {
                        this.ShowMessage("전표처리된 라인이 존재합니다.");
                    }
                    else
                    {
                        DataRow[] dataRowArray2 = table.Select("ADMIN_GU = 'N'");
                        if (dataRowArray2 != null && dataRowArray2.Length > 0)
                        {
                            this.ShowMessage("승인취소된 건은 부가세처리를 할 수 없습니다.");
                        }
                        else if (MA.ServerKey(false, "HDA", "DKTE") && table.Select("VAT_TYPE <> '1'").Length != 0)
                        {
                            if (MA.ServerKey(false, "HDA"))
                            {
                                this.ShowMessage("부가세구분이 일반이 아닌 경우에는 부가세처리를 할 수 없습니다. 재경팀 문의바랍니다.");
                            }
                            if (!MA.ServerKey(false, "DKTE"))
                                return;
                            this.ShowMessage("부가세구분이 일반이 아닌 경우에는 부가세처리를 할 수 없습니다.");
                        }
                        else if (MA.ServerKey(true, "NOSAORKR") && this.ChkNOSAORKR(table))
                        {
                            this.ShowMessage("결의서처리된 라인은 부가세처리를 할 수 없습니다.");
                        }
                        else if (MA.ServerKey(true, "EXAENC") && this._flex.DataTable.Select("S = 'Y' AND ST_GWARE IN ('0','4','F')").Length > 0)
                        {
                            Global.MainFrame.ShowMessage("현재 해당 전표가 전자결재 진행중입니다. 전표취소 할 수 없습니다.");
                        }
                        else
                        {
                            string 부가세처리옵션 = "Y";
                            Control control = sender as Control;
                            if (control.Name == this.btn부가세미처리.Name)
                                부가세처리옵션 = "N";

                            string 봉사료여부 = "N";
                            if (this.rdo여.Checked)
                                봉사료여부 = "Y";
                            
                            if (MA.ServerKey(false, "MNS"))
                            {
                                bool flag1 = false;
                                bool flag2 = false;
                                
                                foreach (DataRow row in table.Rows)
                                {
                                    bool flag3 = this._biz.Search_VAT_ACCT_SUB(row["CD_COSTACCT"].ToString());
                                    bool flag4 = this._biz.Search_MCC_CODE(row["MCC_CODE"].ToString());

                                    if ((flag3 || flag4 || row["VAT_TYPE"].ToString() != "1") && D.GetString(row["YN_VAT"]) == "N")
                                        flag1 = true;
                                    else
                                    {
                                        this._biz.부가세처리미처리(row["ACCT_NO"], row["BANK_CODE"], row["TRADE_DATE"], row["TRADE_TIME"], row["SEQ"], 부가세처리옵션, 봉사료여부);
                                        flag2 = true;
                                    }
                                }

                                if (flag1)
                                    this.ShowMessage("부가세처리 제외 조건에 해당하는 데이터는 부가세처리 할 수 없습니다!");

                                if (flag2)
                                    this.ShowMessage(공통메세지._작업을완료하였습니다, control.Text);
                            }
                            else
                            {
                                foreach (DataRow row in table.Rows)
                                    this._biz.부가세처리미처리(row["ACCT_NO"], row["BANK_CODE"], row["TRADE_DATE"], row["TRADE_TIME"], row["SEQ"], 부가세처리옵션, 봉사료여부);

                                this.ShowMessage(공통메세지._작업을완료하였습니다, control.Text);
                                if (table != null && table.Rows.Count > 0)
                                {
                                    List<object> objectList = new List<object>();
                                    objectList.Add(MA.Login.회사코드);
                                    objectList.Add(this.Get작성부서);
                                    objectList.Add(this.Get카드번호);
                                    objectList.Add(this.dtp승인일자.StartDateToString);
                                    objectList.Add(this.dtp승인일자.EndDateToString);
                                    objectList.Add(this.cbo부가세구분.SelectedValue);
                                    objectList.Add(this.cbo승인구분.SelectedValue);
                                    objectList.Add(this.cbo전표처리.SelectedValue);
                                    objectList.Add(Global.MainFrame.ServerKeyCommon.ToUpper());
                                    objectList.Add(this.cbo카드사명.SelectedValue);
                                    objectList.Add(this.dtp청구년월.StartDateToString);
                                    objectList.Add(this.dtp청구년월.EndDateToString);
                                    objectList.Add(this.cbo전표승인여부.SelectedValue);
                                    objectList.Add(this._카드도움창설정);
                                    objectList.Add(this.cbo그룹웨어처리.SelectedValue);
                                    objectList.Add(this.bpc비용계정.QueryWhereIn_Pipe);
                                    objectList.Add(Global.MainFrame.LoginInfo.UserID);

                                    if (!MA.ServerKey(false, "DOOSANFEED", "KFC"))
                                        objectList.Add(this.cboCust.SelectedValue);

                                    DataTable dataTable1 = this._biz.Search(objectList.ToArray());
                                    if (Global.MainFrame.ShowMessage("체크해제하시겠습니까?", "QY2") == DialogResult.No)
                                    {
                                        foreach (DataRow row in table.Rows)
                                        {
                                            DataTable dataTable2 = dataTable1;
                                            string filterExpression = "ACCT_NO = '" + D.GetString(row["ACCT_NO"]) + "'AND BANK_CODE ='" + D.GetString(row["BANK_CODE"]) + "'AND TRADE_DATE ='" + D.GetString(row["TRADE_DATE"]) + "'AND TRADE_TIME ='" + D.GetString(row["TRADE_TIME"]) + "'AND SEQ ='" + D.GetString(row["SEQ"]) + "'";
                                            
                                            foreach (DataRow dataRow in dataTable2.Select(filterExpression))
                                            {
                                                dataRow["S"] = "Y";
                                                dataRow.AcceptChanges();
                                            }
                                        }
                                    }

                                    this._flex.Binding = dataTable1;
                                    
                                    if (this._flex.HasNormalRow) this.GridColumnColorSetting();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void 전표처리_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow) return;

                DataTable table1 = new DataView(this._flex.DataTable, "S = 'Y' AND TP_SORT='1'", "", DataViewRowState.CurrentRows).ToTable();
                if (table1 == null || table1.Rows.Count == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    DataTable table2 = new DataView(this._flex.DataTable, "S = 'Y' AND TP_SORT='1'", "", DataViewRowState.ModifiedCurrent).ToTable();
                    if (table2 != null && table2.Rows.Count > 0)
                    {
                        if (this.ShowMessage(공통메세지.변경된사항이있습니다저장하시겠습니까, "QY2") != DialogResult.Yes)
                            return;

                        this.SaveData();
                    }

                    DataRow[] dataRowArray1 = table1.Select("DOCU_STAT = '1' OR GW_SEND_YN = 'Y'");
                    
                    if (dataRowArray1 != null && dataRowArray1.Length > 0)
                    {
                        this.ShowMessage("전표처리된 라인이 존재합니다.");
                    }
                    else
                    {
                        int num3;
                        
                        if (Enum.상대계정전표처리설정.비용계정사용 != this.상대계정처리환경설정)
                            num3 = !MA.ServerKey(false, "MTCN") ? 1 : 0;
                        else
                            num3 = 0;

                        if (num3 == 0)
                        {
                            DataRow[] dataRowArray2 = table1.Select("CD_COSTACCT IS NULL OR CD_COSTACCT = ''");
                            if (dataRowArray2 != null && dataRowArray2.Length > 0)
                            {
                                this.ShowMessage("상대계정이 누락되었습니다.");
                                return;
                            }

                            if (MA.ServerKey(false, "MTCN"))
                            {
                                DataRow[] dataRowArray3 = table1.Select("CD_CC IS NULL OR CD_CC = ''");
                                if (dataRowArray3 != null && dataRowArray3.Length > 0)
                                {
                                    this.ShowMessage("코스트센타가 누락되었습니다.");
                                    return;
                                }
                            }
                        }

                        if (MA.ServerKey(false, "KORAIL"))
                        {
                            DataRow[] dataRowArray2 = table1.Select("CD_COSTACCT IS NULL OR CD_COSTACCT = '' OR NM_NOTE IS NULL OR NM_NOTE = '' OR CD_USERDEF1 IS NULL OR CD_USERDEF1 = '' ");
                            if (dataRowArray2 != null && dataRowArray2.Length > 0)
                            {
                                this.ShowMessage("상대계정, 적요, 사원 중 누락된 값이 있습니다.\n확인 후 다시 처리하시기 바랍니다.");
                                return;
                            }
                        }

                        if (MA.ServerKey(false, "EDIYA"))
                        {
                            if (table1.DefaultView.ToTable(false, "CD_PC").Rows.Count > 1)
                            {
                                this.ShowMessage("회계단위가 다른 건은 동시에 전표처리가 안됩니다");
                                return;
                            }
                        }

                        if (MA.ServerKey(false, "ISEC"))
                        {
                            DataRow[] dataRowArray2 = table1.Select("GW_SEND_YN <> 'N'");
                            if (dataRowArray2 != null && dataRowArray2.Length > 0)
                            {
                                this.ShowMessage("그룹웨어 처리된 데이터는 전표처리 할 수 없습니다.");
                                return;
                            }
                        }

                        if (MA.ServerKey(false, "KAIMRO"))
                        {
                            DataRow[] dataRowArray2 = table1.Select("CD_USERDEF1 = '1'");
                            if (dataRowArray2 != null && dataRowArray2.Length > 0)
                            {
                                this.ShowMessage("선택하신 카드사용분은 개인사용분으로 처리된 내역 입니다. 카드사용구분에 개인사용분 체크를 해제 후 다시 작업하시면 됩니다.");
                                return;
                            }
                        }

                        if (MA.ServerKey(true, "NOSAORKR") && this.ChkNOSAORKR(table1))
                        {
                            this.ShowMessage("결의서처리된 라인이 존재합니다.");
                        }
                        else
                        {
                            DataRow[] dataRowArray2 = table1.Select("YN_VAT = 'Y' AND (S_IDNO IS NULL OR S_IDNO = '')");
                            if (dataRowArray2 != null && dataRowArray2.Length > 0)
                            {
                                this.ShowMessage("부가세 처리를 하기 위해서는 사업자등록번호는 필수항목 입니다.");
                            }
                            else
                            {
                                DataRow[] dr부가세처리 = !MA.ServerKey(false, "NICE", "NICEGR", "KPCI", "KTCS") ? table1.Select("YN_VAT = 'Y'") : table1.Select("");
                                if (this._biz.Get미등록거래처건수(dr부가세처리) > 0M)
                                {
                                    if (MA.ServerKey(false, "GALE", "YJRD", "PCPWR"))
                                    {
                                        this.ShowMessage("거래처등록이안된 데이터가 있습니다. 거래처등록해주시기 바랍니다.");
                                        return;
                                    }

                                    if (MA.ServerKey(false, "PCPWR"))
                                    {
                                        this.ShowMessage("미등록 거래처가 있습니다. 거래처 등록 후 전표처리 가능합니다.");
                                        return;
                                    }

                                    if (this.ShowMessage("등록되지 않은 거래처가 존재합니다.\n등록하시겠습니까?", "QY2") != DialogResult.Yes)
                                        return;
                                }

                                switch (this._법인카드전표처리방식)
                                {
                                    case "0":
                                        if (!Docu.HasDocuNew && Global.MainFrame.IsExistPage("P_FI_DOCU", false))
                                        {
                                            Global.MainFrame.UnLoadPage("P_FI_DOCU", false);
                                            break;
                                        }
                                        break;
                                    case "1":
                                        if (Global.MainFrame.IsExistPage("P_FI_DOCU_EPNOTESE", false))
                                        {
                                            Global.MainFrame.UnLoadPage("P_FI_DOCU_EPNOTESE", false);
                                            break;
                                        }
                                        break;
                                }

                                Hashtable hashtable1 = new Hashtable();
                                DocuData docuData = new DocuData(table1);
                                bool bDocu = true;

                                switch (this._법인카드전표처리방식)
                                {
                                    case "0":
                                        string str = string.Empty;
                                        if (this._전표회계일자 == "1")
                                            str = Global.MainFrame.GetStringToday;
                                        else if (this._전표회계일자 == "2")
                                            str = table1.Compute("MAX(TRADE_DATE)", "").ToString();
                                        else if (this._전표회계일자 == "3")
                                            str = this._사용자전표회계일자;
                                        
                                        if (MA.ServerKey(false, "HAATZ") && this._biz.Check_CARDCLOSE(str.Substring(0, 6)))
                                        {
                                            this.ShowMessage("카드승인전표 마감되었습니다");
                                            break;
                                        }

                                        Hashtable hashtable2;
                                        if (this._부가세계정설정 == "3")
                                        {
                                            P_CZ_FI_CARD_DOCU_SUB_VAT fiCardDocuSubVat = new P_CZ_FI_CARD_DOCU_SUB_VAT();
                                            if (fiCardDocuSubVat.ShowDialog() != DialogResult.OK)
                                                break;
                                            hashtable2 = docuData.Get전표옵션(table1, fiCardDocuSubVat.GetHashtable);
                                        }
                                        else
                                        {
                                            if (MA.ServerKey(false, "YIDO", "MTCN", "ETEC"))
                                            {
                                                hashtable2 = docuData.Get전표옵션(table1, new Hashtable() { { "건별일괄", true } });
                                            }
                                            else
                                            {
                                                P_CZ_FI_CARD_DOCU_SUB pFiCardDocuSub = new P_CZ_FI_CARD_DOCU_SUB();
                                                if (pFiCardDocuSub.ShowDialog() != DialogResult.OK)
                                                    break;
                                                hashtable2 = docuData.Get전표옵션(table1, pFiCardDocuSub.GetHashtable);
                                            }
                                            if (this._부가세계정설정 == "2")
                                            {
                                                if (this._부가세계정 == string.Empty)
                                                {
                                                    this.ShowMessage("회계환경설정 은행연동 탭에 법인카드 부가세계정을 셋팅해주세요");
                                                    break;
                                                }
                                                hashtable2.Add("부가세계정코드", this._부가세계정);
                                                hashtable2.Add("부가세계정명", this._biz.GetSettingNmacct(this._부가세계정));
                                            }
                                            else
                                            {
                                                hashtable2.Add("부가세계정코드", "");
                                                hashtable2.Add("부가세계정명", "");
                                            }
                                        }

                                        docuData.Set부가세처리시거래처셋팅(dr부가세처리);
                                        DataRow[] dr거래처 = table1.Select("YN_VAT = 'N' AND (S_IDNO IS NOT NULL OR S_IDNO <> '')");
                                        docuData.Set부가세미처리건거래처셋팅(dr거래처);
                                        this._flex.AcceptChanges();

                                        if (this.rdo부.Checked)
                                            hashtable2.Add("봉사료여부", false);
                                        else if (this.rdo여.Checked)
                                            hashtable2.Add("봉사료여부", true);
                                        
                                        hashtable2.Add("증빙", this.ctx증빙.CodeValue);
                                        
                                        if (MA.ServerKey(true, "ATPLUS", "CHF", "BLNH"))
                                        {
                                            hashtable2.Add("회계단위코드", this.ctx회계단위.CodeValue);
                                            hashtable2.Add("회계단위명", this.ctx회계단위.CodeName);
                                        }

                                        if (Docu.HasDocuNew)
                                        {
                                            hashtable2["회계일자"] = str;
                                            this.CallOtherPageMethod("P_FI_DOCU", "전표입력(" + this.PageName + ")", this.Grant, new object[] { this.PageID, hashtable2, table1, new VoidDelegate(this.DocuCloseEvent) });
                                            break;
                                        }

                                        this.CallOtherPageMethod("P_FI_DOCU", "전표입력(" + this.PageName + ")", this.Grant, new object[] { str, table1, hashtable2, new VoidDelegate(this.DocuCloseEvent) });
                                        break;
                                    case "1":
                                        P_CZ_FI_CARD_EPDOCU_SUB pFiCardEpdocuSub = new P_CZ_FI_CARD_EPDOCU_SUB(table1, bDocu);
                                        if (pFiCardEpdocuSub.ShowDialog() != DialogResult.OK)
                                        {
                                            if (pFiCardEpdocuSub.GetRbo)
                                            {
                                                break;
                                            }
                                            break;
                                        }

                                        docuData.Set부가세처리시거래처셋팅(dr부가세처리);
                                        this._flex.AcceptChanges();
                                        DataTable dataTable = this._biz.연동과목(D.GetString(pFiCardEpdocuSub.GetHashtable["상대예산계정"]));
                                        
                                        if (dataTable != null && dataTable.Rows.Count > 0 && (D.GetString(dataTable.Rows[0]["CD_RELATION"]) == "82" && D.GetString(pFiCardEpdocuSub.GetHashtable["일괄처리"]) == "True"))
                                        {
                                            if (table1.DefaultView.ToTable(true, "NO_CARD").Rows.Count > 1)
                                            {
                                                this.ShowMessage("분개라인처리방식이 일괄처리이고 상대예산계정이 접대비인 경우\n여러 개의 법인카드를 전표처리할 수 없습니다.");
                                                break;
                                            }
                                        }

                                        this.CallOtherPageMethod("P_FI_DOCU_EPNOTESE", "결의서(전표)입력(수동)(" + this.PageName + ")", this.Grant, new object[] { table1, pFiCardEpdocuSub.GetHashtable, new VoidDelegate(this.Search) });
                                        break;
                                    default:
                                        this.ShowMessage("은행연동환경설정페이지에서 사용여부체크되고, 법인카드전표처리방식이 설정된 데이터가 존재하여야합니다");
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void 전표취소_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow)
                    return;

                DataTable table = new DataView(this._flex.DataTable, "S = 'Y' AND TP_SORT='1'", "", DataViewRowState.CurrentRows).ToTable();
                DataRow[] dataRowArray1 = table.Select("DOCU_STAT = '0'");
                DataRow[] dataRowArray2 = table.Select("NO_DOCU ='임의처리'");
                DataRow[] dataRowArray3 = this._flex.DataTable.Select("TP_INPUT ='P_FI_CARD_TEMP_DOCU'");
                if (dataRowArray3 != null && dataRowArray3.Length > 0)
                {
                    this.ShowMessage("법인카드내역 전표처리 메뉴에서 처리한 데이터가 존재합니다. 하나의 메뉴만 사용해주세요.");
                }
                else if (dataRowArray1 != null && dataRowArray1.Length > 0 && dataRowArray2 != null && dataRowArray2.Length > 0)
                {
                    this.ShowMessage("전표미처리, 전표임의처리된 라인이 존재합니다.");
                }
                else if (dataRowArray1 != null && dataRowArray1.Length > 0)
                {
                    this.ShowMessage("전표미처리된 라인이 존재합니다.");
                }
                else if (dataRowArray2 != null && dataRowArray2.Length > 0)
                {
                    this.ShowMessage("전표임의처리된 라인이 존재합니다");
                }
                else
                {
                    if (MA.ServerKey(false, "HAATZ"))
                    {
                        string str = string.Empty;
                        if (this._전표회계일자 == "1")
                            str = Global.MainFrame.GetStringToday;
                        else if (this._전표회계일자 == "2")
                            str = table.Compute("MAX(TRADE_DATE)", "").ToString();
                        else if (this._전표회계일자 == "3")
                            str = this._사용자전표회계일자;

                        if (this._biz.Check_CARDCLOSE(str.Substring(0, 6)))
                        {
                            this.ShowMessage("카드승인전표 마감되었습니다");
                            return;
                        }
                    }
                    if (MA.ServerKey(true, "NOSAORKR") && this.ChkNOSAORKR(table))
                    {
                        this.ShowMessage("결의서처리된 라인이 존재합니다.");
                    }
                    else
                    {
                        if (MA.ServerKey(false, "KFI"))
                        {
                            if (this._flex.DataTable.Select("S = 'Y' AND ST_GWARE IN ('002','003','004','005')").Length > 0)
                            {
                                Global.MainFrame.ShowMessage("결재전,반려,삭제 자료만 삭제 가능합니다.");
                                return;
                            }
                        }
                        else if (MA.ServerKey(false, "DBEXP"))
                        {
                            if (this._flex.DataTable.Select("S = 'Y' AND (ST_GWARE = '0' OR ST_GWARE ='1')").Length > 0)
                            {
                                Global.MainFrame.ShowMessage("결제 상신/승인된 전표는 취소처리 할 수 없습니다.");
                                return;
                            }
                        }
                        else if (MA.ServerKey(false, "MGINFO"))
                        {
                            if (this._flex.DataTable.Select("S = 'Y' AND ST_GWARE = '0'").Length > 0)
                            {
                                Global.MainFrame.ShowMessage("현재 해당 전표가 전자결재 진행중입니다. 전표취소 할 수 없습니다.");
                                return;
                            }
                            foreach (DataRow row in table.Rows)
                            {
                                if (D.GetString(row["ST_GWARE"]) == "1" && !this._biz.GetMG신용정보삭제권한(D.GetString(row["ST_DOCU"])))
                                {
                                    Global.MainFrame.ShowMessage("현재 해당 전표가 전자결재 완료되었습니다. 전표취소 할 수 없습니다.");
                                    return;
                                }
                            }
                        }
                        else if (MA.ServerKey(false, "EXAENC"))
                        {
                            if (this._flex.DataTable.Select("S = 'Y' AND ST_GWARE IN ('0','4','F')").Length > 0)
                            {
                                Global.MainFrame.ShowMessage("현재 해당 전표가 전자결재 진행중입니다. 전표취소 할 수 없습니다.");
                                return;
                            }
                        }
                        else if (!(Global.MainFrame.LoginInfo.StDocuApp == "3"))
                        {
                            if (this._flex.DataTable.Select("S = 'Y' AND ST_GWARE = '0'").Length > 0)
                            {
                                Global.MainFrame.ShowMessage("현재 해당 전표가 전자결재 진행중입니다. 전표취소 할 수 없습니다.");
                                return;
                            }
                            if (this._flex.DataTable.Select("S = 'Y' AND ST_GWARE ='1'").Length > 0)
                            {
                                Global.MainFrame.ShowMessage("현재 해당 전표가 전자결재 완료되었습니다. 전표취소 할 수 없습니다.");
                                return;
                            }
                        }

                        if (this.ShowMessage("처리된 전표 및 관련자료들이 모두 삭제됩니다.\n삭제하시겠습니까?", "QY2") != DialogResult.Yes)
                            return;
                        
                        MsgControl.ShowMsg("전표취소 작업 중 입니다.잠시만 기다려 주세요.");
                        
                        foreach (DataRow row in table.DefaultView.ToTable(true, "NODE_CODE", "NO_DOCU").Rows)
                            this._biz.전표취소(D.GetString(row["NODE_CODE"]), D.GetString(row["NO_DOCU"]));
                        
                        MsgControl.CloseMsg();
                        this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn전표취소.Text);
                        this._flex.AcceptChanges();
                        this.OnToolBarSearchButtonClicked(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgControl.CloseMsg();
                this.MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
            }
        }

        private void 전표조회_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow || D.GetInt(this._flex["DOCU_STAT"]) == Enum.전표처리.처리.GetHashCode())
                    return;
                
                this.ShowMessage("전표처리 후 조회 하세요.");
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void On_Click(object sender, EventArgs e)
        {
            string name;

            try
            {
                if (!this._flex.HasNormalRow) return;

                if (this._flex.GetCheckedRows("S") == null)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else if (MA.ServerKey(true, "NOSAORKR") && this.ChkNOSAORKR(new DataView(this._flex.DataTable, "S = 'Y' AND TP_SORT='1'", "", DataViewRowState.CurrentRows).ToTable()))
                {
                    this.ShowMessage("결의서처리된 라인은 전표임의처리를 할 수 없습니다.");
                }
                else
                {
                    name = (sender as Control).Name;

                    if (name == this.btn전표처리적용.Name)
                        this.임의전표처리(P_CZ_FI_CARD_TEMP_VAT.임의처리.처리);
                    else if (name == this.btn전표처리미적용.Name)
                        this.임의전표처리(P_CZ_FI_CARD_TEMP_VAT.임의처리.미처리);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void 임의전표처리(P_CZ_FI_CARD_TEMP_VAT.임의처리 TYPE)
        {
            for (int row = this._flex.Rows.Fixed; row < this._flex.Rows.Count; ++row)
            {
                if (!(D.GetString(this._flex[row, "S"]) != "Y"))
                {
                    if (MA.ServerKey(false, "KAIMRO") && D.GetString(this._flex[row, "CD_USERDEF1"]) == "1")
                    {
                        this.ShowMessage("선택하신 카드사용분은 개인사용분으로 처리된 내역 입니다. 카드사용구분에 개인사용분 체크를 해제 후 다시 작업하시면 됩니다.");
                        break;
                    }

                    this._flex[row, "S"] = "N";
                    string DOCU_STAT = TYPE == P_CZ_FI_CARD_TEMP_VAT.임의처리.처리 ? "1" : "0";
                    
                    if (!(D.GetString(this._flex[row, "DOCU_STAT"]) == DOCU_STAT))
                    {
                        string str = TYPE == P_CZ_FI_CARD_TEMP_VAT.임의처리.처리 ? string.Empty : "임의처리";
                        if (!(D.GetString(this._flex[row, "NO_DOCU"]) != str))
                        {
                            this._biz.임의전표처리(this._flex[row, "ACCT_NO"].ToString(), this._flex[row, "BANK_CODE"].ToString(), this._flex[row, "TRADE_DATE"].ToString(), this._flex[row, "TRADE_TIME"].ToString(), this._flex[row, "SEQ"].ToString(), DOCU_STAT);
                            this._flex[row, "DOCU_STAT"] = DOCU_STAT;
                            this._flex[row, "NO_DOCU"] = TYPE == P_CZ_FI_CARD_TEMP_VAT.임의처리.처리 ? "임의처리" : string.Empty;
                            this._flex[row, "NO_DOCU_LINE_NO"] = D.GetString(this._flex[row, "NO_DOCU"]);
                            this._flex.AcceptChanges();
                        }
                    }
                }
            }
        }

        private void btn데이터확인_Click(object sender, EventArgs e)
        {
            try
            {
                new P_CZ_FI_CARD_DOCU_DATA_SUB().ShowDialog();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn거래처등록_Click(object sender, EventArgs e)
        {
            try
            {
                if (MA.ServerKey(false, "FJK"))
                {
                    DataTable checkedRows = this._flex.GetCheckedRows("S");
                    if (checkedRows == null || checkedRows.Rows.Count == 0)
                    {
                        this.ShowMessage(공통메세지.선택된자료가없습니다);
                    }
                    else
                    {
                        DataTable table = new DataView(this._flex.DataTable, "S = 'Y' AND TP_SORT='1'", "", DataViewRowState.CurrentRows).ToTable();
                        new DocuData(table).Set부가세처리시거래처셋팅(table.Select(""));
                        this.ShowMessage(공통메세지._작업을완료하였습니다, "거래처등록");
                    }
                }
                else if (this._카드가맹점거래처자동생성 == "N")
                {
                    this.ShowMessage("회계환경설정 - 카드가맹점 거래처 자동생성 미생성 입니다.");
                }
                else
                {
                    if (!this.승인일자체크) return;
                    if (this._biz.CreatePartner(new List<object>() { MA.Login.회사코드, this.dtp승인일자.StartDateToString, this.dtp승인일자.EndDateToString }.ToArray()))
                    {
                        this.ShowMessage(공통메세지._작업을완료하였습니다, "거래처등록");
                    }
                    else
                    {
                        this.ShowMessage(공통메세지.작업을정상적으로처리하지못했습니다, "거래처등록");
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn일괄복사_Click(object sender, EventArgs e)
        {
            P_FI_Z_MTCN_CC_SUB pFiZMtcnCcSub = new P_FI_Z_MTCN_CC_SUB();
            if (pFiZMtcnCcSub.ShowDialog() != DialogResult.Yes)
                return;

            Hashtable ht = pFiZMtcnCcSub.ht;
            foreach (DataRow dataRow in this._flex.DataTable.Select("S = 'Y' AND TP_SORT='1'", string.Empty))
            {
                dataRow["CD_COSTACCT"] = ht["계정"];
                dataRow["NM_COSTACCT"] = ht["계정명"];
                dataRow["CD_CC"] = ht["코스트센터"];
                dataRow["NM_CC"] = ht["코스트센터명"];
            }
        }

        private void btn_업종적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow) return;

                DataTable table1 = new DataView(this._flex.DataTable, "S = 'Y' AND TP_SORT='1'", "", DataViewRowState.CurrentRows).ToTable();
                if (table1 == null || table1.Rows.Count == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    DataTable table2 = new DataView(this._flex.DataTable, "S = 'Y' AND DOCU_STAT = '0' AND TP_SORT='1'", "", DataViewRowState.CurrentRows).ToTable();
                    DataTable dataTable1 = table2.Clone();
                    
                    foreach (DataRow row1 in this._biz.GetCodeData("FI_Z_DSF08").Rows)
                    {
                        DataTable dataTable2 = table2;
                        string filterExpression = "MCC_CODE_NAME LIKE '%" + row1["NM_SYSDEF"].ToString().Trim() + "%' OR S_IDNO = '" + row1["NM_SYSDEF"].ToString().Replace('-', ' ') + "'";
                        foreach (DataRow row2 in dataTable2.Select(filterExpression))
                            dataTable1.ImportRow(row2);
                    }

                    string 봉사료여부 = "N";
                    if (this.rdo여.Checked)
                        봉사료여부 = "Y";
                    
                    foreach (DataRow row in dataTable1.Rows)
                        this._biz.부가세처리미처리(row["ACCT_NO"], row["BANK_CODE"], row["TRADE_DATE"], row["TRADE_TIME"], row["SEQ"], "N", 봉사료여부);
                    
                    this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn업종적용.Text);
                    this.OnToolBarSearchButtonClicked(null, null);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn_데이터삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow) return;

                DataTable table = new DataView(this._flex.DataTable, "S = 'Y' AND TP_SORT='1'", "", DataViewRowState.CurrentRows).ToTable();
                if (table == null || table.Rows.Count == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    DataRow[] dataRowArray = table.Select("DOCU_STAT = '1'");
                    if (dataRowArray != null && dataRowArray.Length > 0)
                    {
                        this.ShowMessage("전표처리된 라인이 존재합니다.");
                    }
                    else
                    {
						if (new P_FI_CARD_DELETE_KMB_SUB(table).ShowDialog() != DialogResult.OK)
							return;

						this.OnToolBarSearchButtonClicked(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn결의서_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow) return;

                DataTable table1 = new DataView(this._flex.DataTable, "S = 'Y'  AND TP_SORT='1'", "", DataViewRowState.CurrentRows).ToTable();
                DocuData docuData = new DocuData(table1);
                if (table1 == null || table1.Rows.Count == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    table1.Columns.Add("YN_VAT_DB");
                    foreach (DataRow row in table1.Rows)
                    {
                        DataTable dataTable = this._biz.결의서처리여부(new List<object>() { row["ACCT_NO"], row["BANK_CODE"], row["TRADE_DATE"], row["TRADE_TIME"], row["SEQ"] }.ToArray());
                        if (dataTable.Rows[0]["CD_USERDEF1"].ToString() != string.Empty)
                        {
                            this.ShowMessage("이미 결의서 처리된 라인이 존재합니다.");
                            return;
                        }

                        row["YN_VAT_DB"] = dataTable.Rows[0]["SINGO_TYPE"].ToString() == "신고대상" ? "Y" : "N";
                        
                        if (D.GetString(row["YN_VAT_DB"]) == "N")
                        {
                            row["SUPPLY_AMT"] = D.GetDecimal(row["ADMIN_AMT"]);
                            row["VAT_AMT"] = 0;
                        }
                    }

                    table1.AcceptChanges();
                    DataRow[] dataRowArray = table1.Select("S_IDNO IS NULL OR S_IDNO = '' ");
                    DataTable table2 = table1.DefaultView.ToTable(false, "ACCT_NO", "BANK_CODE", "TRADE_DATE", "TRADE_TIME", "SEQ", "SUPPLY_AMT", "VAT_AMT", "ADMIN_AMT", "TRADE_PLACE", "S_IDNO", "MERC_REPR", "MCC_CODE_NAME", "MERC_ADDR", "CD_PARTNER", "NO_DOCU", "NO_CARD", "NM_CARD", "SINGO_TYPE", "CD_USERDEF1", "YN_VAT_DB", "VAT_TYPE", "LN_PARTNER");
                    foreach (DataRow row in table2.Rows)
                    {
                        string str = this._biz.Get거래처(row["S_IDNO"]);
                        if (dataRowArray.Length != 0 || str == string.Empty)
                        {
                            if (this.ShowMessage("등록되지 않은 거래처가 존재합니다.\n등록하시겠습니까?", "QK2") != DialogResult.OK)
                                return;
                            docuData.Set부가세처리시거래처셋팅(table2.Select(""));
                            break;
                        }
                        row["CD_PARTNER"] = str;
                    }

                    if (Global.MainFrame.IsExistPage("P_FI_EPDOCU_IN", false))
                        Global.MainFrame.UnLoadPage("P_FI_EPDOCU_IN", false);
                    
                    this.CallOtherPageMethod("P_FI_EPDOCU_IN", "(신)결의서입력(자동전표입력)(" + this.PageName + ")", this.Grant, new object[] { table2 });
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn_VAT제외계정_Click(object sender, EventArgs e)
        {
            try
            {
                new P_FI_Z_MNS_CARD_VAT_ACCT_SUB().ShowDialog();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn카드전표마감_Click(object sender, EventArgs e)
        {
            try
            {
                new P_FI_Z_HAATZ_CARDCLOSE_SUB().ShowDialog();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn전표번호매핑_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow) return;

                DataTable table = new DataView(this._flex.DataTable, "S = 'Y' AND TP_SORT='1'", "", DataViewRowState.CurrentRows).ToTable();
                if (table == null || table.Rows.Count == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else if (table.Select("DOCU_STAT = '1'", string.Empty).Length > 0)
                {
                    this.ShowMessage("이미 전표처리한 내역이 있습니다.");
                }
                else
                {
                    P_FI_Z_SSCARD_MAPPING_SUB sscardMappingSub = new P_FI_Z_SSCARD_MAPPING_SUB();
                    if (sscardMappingSub.ShowDialog() == DialogResult.OK)
                    {
                        object NO_DOCU = sscardMappingSub.GetHashtable["전표번호"];
                        object NO_DOLINE = sscardMappingSub.GetHashtable["라인번호"];
                        object CD_PC = sscardMappingSub.GetHashtable["회계단위"];
                        if (this._biz.SAVE_MAPPING_DOCU(table, NO_DOCU, NO_DOLINE, CD_PC))
                        {
                            this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
                            this.OnToolBarSearchButtonClicked(null, null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btnCust4_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow) return;

                if (MA.ServerKey(false, "SSCARD"))
                {
                    DataTable table = new DataView(this._flex.DataTable, "S = 'Y' AND TP_SORT='1' AND CD_USERDEF3 = 'Y'", "", DataViewRowState.CurrentRows).ToTable();
                    if (table == null || table.Rows.Count == 0)
                    {
                        this.ShowMessage(공통메세지.선택된자료가없습니다);
                        return;
                    }
                    if (this._biz.DELETE_MAPPING_DOCU(table))
                    {
                        this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
                        this.OnToolBarSearchButtonClicked(null, null);
                        return;
                    }
                }

                if (MA.ServerKey(false, "AVLFNC"))
                {
                    DataTable table = new DataView(this._flex.DataTable, "S = 'Y'", string.Empty, DataViewRowState.CurrentRows).ToTable();
                    if (table == null || table.Rows.Count == 0)
                    {
                        this.ShowMessage(공통메세지.선택된자료가없습니다);
                    }
                    else if (new P_FI_Z_AVLFNC_PARTNER_CLOSE(table).ShowDialog() != DialogResult.OK);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btnCust1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow) return;

                if (MA.ServerKey(false, "ISEC"))
                {
                    DataTable table = new DataView(this._flex.DataTable, "S = 'Y' AND TP_SORT='1'", "", DataViewRowState.CurrentRows).ToTable();
                    DataRow[] dataRowArray = table.Select("DOCU_STAT = '1'");

                    if (dataRowArray != null && dataRowArray.Length > 0)
                    {
                        this.ShowMessage("전표처리된 라인이 존재합니다.");
                    }
                    else if (new P_FI_Z_ISEC_GWSEND_SUB(table.Select()).ShowDialog() == DialogResult.Yes)
                        this.OnToolBarSearchButtonClicked(null, null);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btnCust2_Click(object sender, EventArgs e)
        {
            try
            {
                if (MA.ServerKey(false, "ISEC"))
                {
                    foreach (DataRow dataRow in this._flex.DataTable.Select("S='Y' AND TP_SORT='1'", string.Empty))
                    {
                        if (dataRow["DOCU_STAT"].ToString() == "1")
                        {
                            this.ShowMessage("전표처리된 데이터입니다.");
                            return;
                        }

                        if (dataRow["GW_SEND_YN"].ToString() == "N")
                        {
                            this.ShowMessage("이미 미처리된 데이터입니다.");
                            return;
                        }

                        if (!this._biz.Save_ISEC_CANCEL(new List<object>() { dataRow["ACCT_NO"], dataRow["BANK_CODE"], dataRow["TRADE_DATE"], dataRow["TRADE_TIME"], dataRow["SEQ"] }.ToArray()))
                        {
                            this.ShowMessage(공통메세지.자료저장중오류가발생하였습니다);
                        }
                    }

                    this.ShowMessage(공통메세지._작업을완료하였습니다, "그룹웨어미처리");
                    this.OnToolBarSearchButtonClicked(null, null);
                }

                if (MA.ServerKey(false, "KAIMRO") && this.SetKAIMRO("1"))
                {
                    this.ShowMessage(공통메세지._작업을완료하였습니다, "개인사용분적용");
                    this.OnToolBarSearchButtonClicked(null, null);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btnCust3_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MA.ServerKey(false, "KAIMRO") || !this.SetKAIMRO("2"))
                    return;

                this.ShowMessage(공통메세지._작업을완료하였습니다, "개인사용분적용해제");
                this.OnToolBarSearchButtonClicked(null, null);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Flex_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (D.GetString(this._flex[e.Row, "TP_SORT"]) == "2")
                    e.Cancel = true;

                switch (this._flex.Cols[e.Col].Name)
                {
                    case "YN_VAT":
                        if (MA.ServerKey(false, "MNS"))
                        {
                            e.Cancel = true;
                            break;
                        }

                        if (MA.ServerKey(false, "ETEC") && D.GetDecimal(this._flex["VAT_AMT"]) == 0M && D.GetString(this._flex["YN_VAT"]) == "Y")
                            this._flex["YN_VAT"] = "N";

                        DataSet buttonSet = this._biz.GetButtonSet();
                        if (buttonSet == null || buttonSet.Tables[0].Rows.Count == 0 || buttonSet.Tables[1].Rows.Count == 0 || buttonSet.Tables[2].Rows.Count == 0)
                            break;
                        
                        if (buttonSet.Tables[0].Rows[0]["YN_USERSET"].ToString() == "N")
                        {
                            if (buttonSet.Tables[1].Rows[0]["YN_TAX"].ToString() == "Y" && buttonSet.Tables[1].Rows[0]["YN_NTAX"].ToString() == "Y")
                            {
                                if (D.GetDecimal(this._flex["VAT_AMT"]) == 0M)
                                {
                                    e.Cancel = true;
                                    break;
                                }
                                break;
                            }

                            if (buttonSet.Tables[1].Rows[0]["YN_TAX"].ToString() == "N" && buttonSet.Tables[1].Rows[0]["YN_NTAX"].ToString() == "N")
                            {
                                e.Cancel = true;
                                break;
                            }

                            if (D.GetDecimal(this._flex["VAT_AMT"]) == 0M)
                            {
                                e.Cancel = true;
                                break;
                            }

                            if (buttonSet.Tables[1].Rows[0]["YN_TAX"].ToString() == "Y")
                            {
                                if (this._flex["YN_VAT"].ToString() == "Y")
                                    e.Cancel = true;
                            }
                            else if (this._flex["YN_VAT"].ToString() == "N")
                                e.Cancel = true;
                            break;
                        }

                        if (buttonSet.Tables[2].Rows[0]["YN_TAX"].ToString() == "Y" && buttonSet.Tables[2].Rows[0]["YN_NTAX"].ToString() == "Y")
                        {
                            if (D.GetDecimal(this._flex["VAT_AMT"]) == 0M)
                                e.Cancel = true;
                        }
                        else if (buttonSet.Tables[2].Rows[0]["YN_TAX"].ToString() == "N" && buttonSet.Tables[2].Rows[0]["YN_NTAX"].ToString() == "N")
                            e.Cancel = true;
                        else if (D.GetDecimal(this._flex["VAT_AMT"]) == 0M)
                            e.Cancel = true;
                        else if (buttonSet.Tables[2].Rows[0]["YN_TAX"].ToString() == "Y")
                        {
                            if (this._flex["YN_VAT"].ToString() == "Y")
                                e.Cancel = true;
                        }
                        else if (this._flex["YN_VAT"].ToString() == "N")
                            e.Cancel = true;
                        break;
                    case "CD_COSTACCT":
                    case "CD_CC":
                    case "CD_PJT":
                        if (Enum.상대계정전표처리설정.상대계정처리유형사용 == this.상대계정처리환경설정 || this._flex[e.Row, "DOCU_STAT"].ToString() == Enum.전표처리.처리.GetHashCode().ToString())
                        {
                            e.Cancel = true;
                            break;
                        }
                        break;
                    case "NM_COSTACCT":
                        e.Cancel = true;
                        break;
                    case "NM_TPACCT":
                        if (this._flex[e.Row, "DOCU_STAT"].ToString() == Enum.전표처리.처리.GetHashCode().ToString())
                        {
                            e.Cancel = true;
                            break;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Flex_HelpClick(object sender, EventArgs e)
        {
            try
            {
                if (D.GetInt(this._flex["DOCU_STAT"]) != Enum.전표처리.처리.GetHashCode() || D.GetString(this._flex["NO_DOCU"]) == "임의처리")
                    return;

                this.CallOtherPageMethod("P_FI_DOCU", "전표입력", this.Grant, new object[] { D.GetString(this._flex["NO_DOCU"]), "0", D.GetString(this._flex["NODE_CODE"]), this.LoginInfo.CompanyCode });
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Flex_CheckHeaderClick(object sender, EventArgs e)
        {
            try
            {
                this._flex.Redraw = false;
                CheckEnum cellCheck1 = this._flex.GetCellCheck(this._flex.Rows.Fixed, this._flex.Cols["YN_VAT"].Index);
                int index = this._flex.Cols["YN_VAT"].Index;
                this._biz.GetButtonSet();

                for (int row = this._flex.Rows.Fixed; row < this._flex.Rows.Count; ++row)
                {
                    if (row != this._flex.Rows.Fixed)
                    {
                        if (D.GetString(this._flex[row, "TP_SORT"]) == "2")
                        {
                            this._flex.SetCellCheck(row, this._flex.Cols["S"].Index, CheckEnum.Unchecked);
                            this._flex.SetCellCheck(row, index, CheckEnum.Unchecked);
                        }

                        CheckEnum cellCheck2 = this._flex.GetCellCheck(row, this._flex.Cols["YN_VAT"].Index);
                        if (D.GetInt(this._flex[row, "DOCU_STAT"]) == Enum.전표처리.처리.GetHashCode())
                        {
                            if (cellCheck2 == CheckEnum.Checked)
                                this._flex.SetCellCheck(row, index, CheckEnum.Unchecked);
                            else
                                this._flex.SetCellCheck(row, index, CheckEnum.Checked);
                        }
                        else if (cellCheck2 == CheckEnum.Checked && D.GetDecimal(this._flex[row, "VAT_AMT"]) == 0M)
                            this._flex.SetCellCheck(row, index, CheckEnum.Unchecked);
                    }
                }

                if (D.GetInt(this._flex[this._flex.Rows.Fixed, "DOCU_STAT"]) == Enum.전표처리.처리.GetHashCode())
                {
                    if (cellCheck1 == CheckEnum.Checked)
                        this._flex.SetCellCheck(this._flex.Rows.Fixed, index, CheckEnum.Unchecked);
                    else
                        this._flex.SetCellCheck(this._flex.Rows.Fixed, index, CheckEnum.Checked);
                }
                else if (cellCheck1 == CheckEnum.Checked && D.GetDecimal(this._flex[this._flex.Rows.Fixed, "VAT_AMT"]) == 0M)
                    this._flex.SetCellCheck(this._flex.Rows.Fixed, index, CheckEnum.Unchecked);

                this._flex.Redraw = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_CellContentChanged(object sender, CellContentEventArgs e)
        {
            try
            {
                string empty = string.Empty;
                if (e.ContentType == ContentType.Memo)
                {
                    if (e.CommandType == Dass.FlexGrid.CommandType.Add)
                    {
                        string query = string.Format("UPDATE CARD_TEMP SET MEMO_CD = '{0}' WHERE ACCT_NO = '{1}' AND BANK_CODE = '{2}' AND TRADE_DATE = '{3}' AND TRADE_TIME = '{4}' AND SEQ = '{5}' AND C_CODE = '{6}'", e.SettingValue, this._flex[e.Row, "ACCT_NO"].ToString(), this._flex[e.Row, "BANK_CODE"].ToString(), this._flex[e.Row, "TRADE_DATE"].ToString(), this._flex[e.Row, "TRADE_TIME"].ToString(), this._flex[e.Row, "SEQ"].ToString(), Global.MainFrame.LoginInfo.CompanyCode);
                        Global.MainFrame.ExecuteScalar(query);
                    }
                    else
                    {
                        if (e.CommandType != Dass.FlexGrid.CommandType.Delete)
                            return;

                        string query = string.Format("UPDATE CARD_TEMP SET MEMO_CD = NULL WHERE ACCT_NO = '{0}' AND BANK_CODE = '{1}' AND TRADE_DATE = '{2}' AND TRADE_TIME = '{3}' AND SEQ = '{4}' AND C_CODE = '{5}'", this._flex[e.Row, "ACCT_NO"].ToString(), this._flex[e.Row, "BANK_CODE"].ToString(), this._flex[e.Row, "TRADE_DATE"].ToString(), this._flex[e.Row, "TRADE_TIME"].ToString(), this._flex[e.Row, "SEQ"].ToString(), Global.MainFrame.LoginInfo.CompanyCode);
                        Global.MainFrame.ExecuteScalar(query);
                    }
                }
                else
                {
                    if (e.ContentType != ContentType.CheckPen)
                        return;

                    if (e.CommandType == Dass.FlexGrid.CommandType.Add)
                    {
                        string query = string.Format("UPDATE CARD_TEMP SET CHECK_PEN = '{0}' WHERE ACCT_NO = '{1}' AND BANK_CODE = '{2}' AND TRADE_DATE = '{3}' AND TRADE_TIME = '{4}' AND SEQ = '{5}' AND C_CODE = '{6}'", e.SettingValue, this._flex[e.Row, "ACCT_NO"].ToString(), this._flex[e.Row, "BANK_CODE"].ToString(), this._flex[e.Row, "TRADE_DATE"].ToString(), this._flex[e.Row, "TRADE_TIME"].ToString(), this._flex[e.Row, "SEQ"].ToString(), Global.MainFrame.LoginInfo.CompanyCode);
                        Global.MainFrame.ExecuteScalar(query);
                    }
                    else if (e.CommandType == Dass.FlexGrid.CommandType.Delete)
                    {
                        string query = string.Format("UPDATE CARD_TEMP SET CHECK_PEN = NULL WHERE ACCT_NO = '{0}' AND BANK_CODE = '{1}' AND TRADE_DATE = '{2}' AND TRADE_TIME = '{3}' AND SEQ = '{4}' AND C_CODE = '{5}'", this._flex[e.Row, "ACCT_NO"].ToString(), this._flex[e.Row, "BANK_CODE"].ToString(), this._flex[e.Row, "TRADE_DATE"].ToString(), this._flex[e.Row, "TRADE_TIME"].ToString(), this._flex[e.Row, "SEQ"].ToString(), Global.MainFrame.LoginInfo.CompanyCode);
                        Global.MainFrame.ExecuteScalar(query);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_BeforeShowContextMenu(object sender, EventArgs e)
        {
            try
            {
                this._flex.AddContextMenu = true;
                this._flex.AddMenuSeperator();
                this._flex.AddMenuItem("전표입력", new EventHandler(this.ContextMenuItem_Click));
                this._flex.AddMenuItem("전표승인", new EventHandler(this.ContextMenuItem_Click));
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void ContextMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow || !(sender is ToolStripMenuItem toolStripMenuItem) || D.GetInt(this._flex["DOCU_STAT"]) != Enum.전표처리.처리.GetHashCode())
                    return;

                switch (toolStripMenuItem.Name)
                {
                    case "전표입력":
                        this.CallOtherPageMethod("P_FI_DOCU", "전표입력", this.Grant, new object[] { D.GetString(this._flex["NO_DOCU"]), "0", D.GetString(this._flex["NODE_CODE"]), this.LoginInfo.CompanyCode });
                        break;
                    case "전표승인":
                        string codeValue = this.ctx회계단위.CodeValue;
                        string codeName = this.ctx회계단위.CodeName;
                        string str1 = D.GetString(this._flex["DT_ACCT"]);
                        string str2 = D.GetString(this._flex["ST_DOCU"]);
                        string str3 = D.GetString(this._flex["ID_WRITE"]);
                        string str4 = D.GetString(this._flex["NM_KOR"]);
                        string str5 = D.GetString(this._flex["CD_WDEPT"]);
                        string str6 = D.GetString(this._flex["NM_WDEPT"]);
                        string str7 = D.GetString(this._flex["NO_DOCU"]);
                        object[] args = new object[] { codeValue,
                                                       codeName,
                                                       str1,
                                                       str1,
                                                       str2,
                                                       str3,
                                                       str4,
                                                       str5,
                                                       str6,
                                                       str7 };
                        this.CallOtherPageMethod("P_FI_DOCUAPP", this.DD("전표승인") + "(" + this.PageName + ")", this.Grant, args);
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                if (MA.ServerKey(false, "KORAIL"))
                {
                    if (!(this._flex[e.Row, "DOCU_STAT"].ToString() == Enum.전표처리.처리.GetHashCode().ToString()) || !(Global.MainFrame.LoginInfo.StDocuApp == "1"))
                        return;
                    e.Cancel = true;
                }
                else
                {
                    switch (this._flex.Cols[e.Col].Name)
                    {
                        case "CD_USERDEF1":
                            e.Parameter.UserParams = "경비유형코드;H_FI_Z_DKTE_COSTNOTE;";
                            break;
                        case "NM_TPACCT":
                            if (this._flex[e.Row, "DOCU_STAT"].ToString() == Enum.전표처리.처리.GetHashCode().ToString())
                                e.Cancel = true;
                            e.Parameter.UserParams = "상대계정처리유형;H_FI_CARD_TEMP_ACCT;";
                            e.Parameter.P92_DETAIL_SEARCH_CODE = D.GetString(this._flex[e.Row, "NM_TPACCT"]);
                            break;
                        case "CD_COSTACCT":
                        case "CD_CC":
                        case "CD_PJT":
                            if (Enum.상대계정전표처리설정.상대계정처리유형사용 == this.상대계정처리환경설정 || this._flex[e.Row, "DOCU_STAT"].ToString() == Enum.전표처리.처리.GetHashCode().ToString())
                            {
                                e.Cancel = true;
                                break;
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_AfterRowChange(object sender, RangeEventArgs e)
        {
            if (this._flex["VAT_TYPE"].ToString() == "1")
            {
                this.btn부가세처리.Enabled = true;
                this.btn부가세미처리.Enabled = true;
            }
            else
            {
                this.btn부가세처리.Enabled = false;
                this.btn부가세미처리.Enabled = false;
            }
        }

        private void _flex_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (!MA.ServerKey(false, "KORAIL")) return;

                D.GetString(this._flex.GetData(e.Row, e.Col));
                string editData = this._flex.EditData;
                
                switch (this._flex.Cols[e.Col].Name)
                {
                    case "CD_COSTACCT":
                    case "NM_NOTE":
                    case "CD_USERDEF1":
                        if (this._flex[e.Row, "DOCU_STAT"].ToString() == Enum.전표처리.처리.GetHashCode().ToString() && Global.MainFrame.LoginInfo.StDocuApp == "1")
                        {
                            e.Cancel = true;
                            break;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Page_DataChanged(object sender, EventArgs e)
        {
            this.ToolBarAddButtonEnabled = this.ToolBarDeleteButtonEnabled = false;
            
            if (!MA.ServerKey(false, "MBIKOREA"))
                return;
            
            this.ToolBarDeleteButtonEnabled = true;
        }

        private void DocuCloseEvent(params string[] 더미)
        {
            this.Search();
            this.GridColumnColorSetting();
        }

        private void BpControl_QueryBefore(object sender, BpQueryArgs e)
        {
            string name;

            try
            {
                name = (sender as Control).Name;

                if (name == bpc카드번호.Name)
				{
                    if (MA.ServerKey(false, "TCHP") && D.GetString(this.bpc작성부서.QueryWhereIn_Pipe) == string.Empty)
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, "부서");
                        e.QueryCancel = true;
                    }

                    if (this._카드도움창설정 == "0")
                    {
                        this.bpc카드번호.UserParams = "카드도움창;H_FI_CARD_DEPT;" + this.bpc작성부서.QueryWhereIn_Pipe;
                        return;
                    }

                    if (MA.ServerKey(false, "SOLIDTECH"))
                    {
                        this.bpc카드번호.UserHelpID = "H_FI_Z_SOLIDTECH_CARD_USER_SUB";
                        this.bpc카드번호.UserParams = "카드도움창;H_FI_Z_SOLIDTECH_CARD_USER_SUB;" + this.bpc작성부서.QueryWhereIn_Pipe;
                    }
                    else
                        this.bpc카드번호.UserParams = "카드도움창;H_FI_CARD_USER;" + Global.MainFrame.LoginInfo.UserID;
                }
                else if (name == ctx증빙.Name)
				{
                    e.HelpParam.P41_CD_FIELD1 = "FI_F000105";
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_AfterCodeHelp(object sender, AfterCodeHelpEventArgs e)
        {
            try
            {
                if (!MA.ServerKey(false, "NICEIT1", "NICEIT11") || !(this._flex.Cols[e.Col].Name == "CD_COSTACCT"))
                    return;

                this._flex["CD_TACCT"] = e.Result.DataTable.Rows[0]["CD_ACCT"];
                this._flex["NM_TACCT"] = e.Result.DataTable.Rows[0]["NM_ACCT"];
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool ProcessKeyPreview(ref Message m)
        {
            int num = (int)((Keys)(int)m.WParam | Control.ModifierKeys);
            int modifierKeys = (int)Control.ModifierKeys;
            bool flag = (Control.ModifierKeys & Keys.Control) != Keys.None;
            
            if (m.Msg == 256 && modifierKeys == 131072 && ((num & (int)ushort.MaxValue) == 84 && flag))
                this.btn부가세처리.Visible = this.btn부가세미처리.Visible = this.btn전표처리적용.Visible = this.btn전표처리미적용.Visible = true;

            return base.ProcessKeyPreview(ref m);
        }

        private void Search()
        {
            List<object> objectList = new List<object>();

            objectList.Add(MA.Login.회사코드);
            objectList.Add(this.Get작성부서);
            objectList.Add(this.Get카드번호);
            objectList.Add(this.dtp승인일자.StartDateToString);
            objectList.Add(this.dtp승인일자.EndDateToString);
            objectList.Add(this.cbo부가세구분.SelectedValue);
            objectList.Add(this.cbo승인구분.SelectedValue);
            objectList.Add(this.cbo전표처리.SelectedValue);
            objectList.Add(Global.MainFrame.ServerKeyCommon.ToUpper());
            objectList.Add(this.cbo카드사명.SelectedValue);
            objectList.Add(this.dtp청구년월.StartDateToString);
            objectList.Add(this.dtp청구년월.EndDateToString);
            objectList.Add(this.cbo전표승인여부.SelectedValue);
            objectList.Add(this._카드도움창설정);
            objectList.Add(this.cbo그룹웨어처리.SelectedValue);
            objectList.Add(this.bpc비용계정.QueryWhereIn_Pipe);
            objectList.Add(Global.MainFrame.LoginInfo.UserID);

            if (!MA.ServerKey(false, "DOOSANFEED", "KFC"))
                objectList.Add(this.cboCust.SelectedValue);
            
            this._flex.Binding = this._biz.Search(objectList.ToArray());
        }

        private void Search(params string[] dummy)
        {
            this.Search();
        }

        private void GridColumnColorSetting()
        {
            for (int index = this._flex.Rows.Fixed; index < this._flex.Rows.Count; ++index)
            {
                CellRange cellRange;

                if (MA.ServerKey(false, "MNS", "TCHP") && D.GetString(this._flex[index, "DOCU_STAT"]) == "1")
                {
                    cellRange = this._flex.GetCellRange(index, 1, index, this._flex.Cols.Count - 1);
                    cellRange.Style = this._flex.Styles.Add("DOCU_STAT");
                    cellRange.Style.BackColor = Color.FromArgb(174, 218, 165);
                }

                if (MA.ServerKey(false, "DKTE") && D.GetString(this._flex[index, "ST_DOCU"]) == "2")
                {
                    cellRange = this._flex.GetCellRange(index, 1, index, this._flex.Cols.Count - 1);
                    cellRange.Style = this._flex.Styles.Add("ST_DOCU");
                    cellRange.Style.ForeColor = Color.Blue;
                }

                if (MA.ServerKey(false, "DBCAS") && D.GetString(this._flex[index, "CHECK"]) == "Y")
                {
                    cellRange = this._flex.GetCellRange(index, 1, index, this._flex.Cols.Count - 1);
                    cellRange.Style = this._flex.Styles.Add("ADMIN");
                    cellRange.Style.ForeColor = Color.Red;
                }

                if (D.GetString(this._flex[index, "ADMIN_GU"]) == "N")
                {
                    cellRange = this._flex.GetCellRange(index, 1, index, this._flex.Cols.Count - 1);
                    cellRange.Style = this._flex.Styles.Add("ADMIN");
                    cellRange.Style.ForeColor = Color.Red;
                }

                if (D.GetString(this._flex[index, "NO_CARD"]) == "승인합계" || D.GetString(this._flex[index, "NO_CARD"]) == "승인취소" || D.GetString(this._flex[index, "NO_CARD"]) == "계" || D.GetString(this._flex[index, "NO_CARD"]) == "합계")
                    this._flex.Rows[index].StyleNew.BackColor = Color.AntiqueWhite;
            }
        }

        private bool 승인일자체크
        {
            get
			{
                return Checker.IsValid(this.dtp승인일자, true, this.DD("승인일자"));
            }
        }

        private bool Use부가세
        {
            get
            {
                switch (Global.MainFrame.ServerKeyCommon.ToUpper())
                {
                    case "GSTLS":
                        return false;
                    default:
                        return true;
                }
            }
        }

        private object Get작성부서
        {
            get
            {
                if (this.bpc작성부서.QueryWhereIn_Pipe == string.Empty)
                    return DBNull.Value;
                else
                    return this.bpc작성부서.QueryWhereIn_Pipe;
            }
        }
        
        private object Get카드번호
        {
            get
            {
                if (this.bpc카드번호.QueryWhereIn_Pipe == string.Empty)
                    return DBNull.Value;

                return MA.ServerKey(false, "UBCARE") ? this.bpc카드번호.QueryWhereIn_Pipe : Duzon.ERPU.UEncryption.UEncryption.CreditEncryption(this.bpc카드번호.QueryWhereIn_Pipe);
            }
        }

        private bool GetCard
        {
            get
            {
                return Checker.IsValid(this.bpc카드번호, true, this.DD("카드번호"));
            }
        }
        
        private bool SetKAIMRO(string GUBUN)
        {
            foreach (DataRow dataRow in this._flex.DataTable.Select("S='Y' AND TP_SORT='1'", string.Empty))
            {
                if (dataRow["DOCU_STAT"].ToString() == "1")
                {
                    this.ShowMessage("전표처리된 데이터입니다.");
                    return false;
                }

                if (dataRow["CD_USERDEF1"].ToString() == GUBUN)
                {
                    this.ShowMessage(공통메세지.이미등록된자료가있습니다);
                    return false;
                }

                if (!this._biz.Save_KAIMRO_JUCARD(new List<object>() { dataRow["ACCT_NO"], dataRow["BANK_CODE"], dataRow["TRADE_DATE"], dataRow["TRADE_TIME"], dataRow["SEQ"], GUBUN }.ToArray()))
                {
                    this.ShowMessage(공통메세지.자료저장중오류가발생하였습니다);
                    return false;
                }
            }

            return true;
        }

        private bool ChkNOSAORKR(DataTable dt)
        {
            if (!MA.ServerKey(false, "NOSAORKR"))
                return false;

            DataRow[] dataRowArray = dt.Select("DOCU_STAT = '2' OR (CD_USERDEF1 <>' ' AND NO_DOCU <> ' ')");
            return dataRowArray != null && dataRowArray.Length > 0;
        }

        private enum 임의처리
        {
            NONE,
            처리,
            미처리,
        }
    }
}

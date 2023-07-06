using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.MF.EMail;
using Duzon.ERPU.OLD;
using Duzon.Windows.Print;
using System.Text;
using Duzon.Common.BpControls;
using System.Drawing;

namespace pur
{
    /// <summary>
    //********************************************************************
    // 작   성   자 : 김대영
    // 작   성   일 : 
    // 모   듈   명 : 구매/자재
    // 시 스  템 명 : 매입관리
    // 페 이 지  명 : 매입관리
    // 프로젝트  명 : P_PU_IV_MNG
    // 재 작  성 자 : 허성철
    //********************************************************************
    // CHANGE LIST
    //********************************************************************
    // 2010.03.11 _ 안종호 _ 부가세 사업장 컬럼 추가
    // 2010.04.20 - 안종호 - 사업장 콤보박스 컨트롤에서 헬프도움창으로 변경(크라제, 김헌섭대리)
    // 2010.04.20 - 안종호 - 라인부분 C/C코드 컬럼 추가(크라제, 김헌섭대리)
    // 2010.05.18 - 신미란 - 사업장 콤보박스 필수 color Login 사업장 Default
    // 2010.07.27 - 안종호 - 매입처 도움창 입력 추가, 전표번호컬럼 DISPLAY
    // 2010.08.16 - 신미란 - CC별GROUP BY 제외 옵션처리
    // 2013.11.08 : KBS미디어 전표처리할때 서버키추가
    //********************************************************************
    /// </summary>
    public partial class P_PU_IV_MNG : Duzon.Common.Forms.PageBase
    {

        #region ♣ 멤버필드

        DataTable ds_Ty1 = new DataTable();
        DataTable dataTable1 = new DataTable();
        // 매입등록 화면에서 받는 정보
        string _cdbizarea = "", _nmbizarea, _noemp = "", _nmemp = "", _fg_trans = "", _dtpo = "";
        bool _isCallIV = false;
        private P_PU_IV_MNG_BIZ _biz;
        string 지급관리통제설정 = "N";
        private string m_Elec_app = "000"; //전자결재
        string serverkey_common;
        DataTable dt_Plant = new DataTable();

        bool bStandard = false; //규격형
        //private IMainFrame _mf;

        #endregion

        #region ♣ 초기화

        #region -> 생성자
	
		public P_PU_IV_MNG(string ps_cdbizarea, string ps_no_emp, string ps_nm_emp,string ps_fgtrans,string ps_dtpo)
		{
			InitializeComponent();
            MainGrids = new FlexGrid[] { _flexM };
            _flexM.DetailGrids = new FlexGrid[] { _flexD };

            string[] arr_bizarea = ps_cdbizarea.Split('|');
            _cdbizarea = D.GetString(arr_bizarea[0]); //사업장코드
            _nmbizarea = D.GetString(arr_bizarea[1]); //사업장명

			_noemp = ps_no_emp;
			_nmemp = ps_nm_emp;
			_fg_trans = ps_fgtrans;
			_dtpo = ps_dtpo;

            _isCallIV = true;

            serverkey_common = Global.MainFrame.ServerKeyCommon;
            //PupUpMenu = new System.Windows.Forms.ContextMenu();
            //PupUpMenu.Popup += new System.EventHandler(PopupEventHandler);
        }
	
        public P_PU_IV_MNG()
        {
            InitializeComponent();
            MainGrids = new FlexGrid[] { _flexM };
            _flexM.DetailGrids = new FlexGrid[] { _flexD };
            serverkey_common = Global.MainFrame.ServerKeyCommon;
           // DataChanged += new EventHandler(Page_DataChanged);
        }

        #endregion

        #region -> InitLoad

        protected override void InitLoad()
        {
            _biz = new P_PU_IV_MNG_BIZ();

            //InitDD(_tlay_Main);

            MA_EXC_SET();

            InitControl();
            InitEvent();
            InitGrid();


        }

        #region -> 시스템통제설정
        private void MA_EXC_SET()
        {
            #region -> 시스템통제설정적용 메일전송설정
            if (Duzon.ERPU.MF.ComFunc.전용코드("매입관리-메일기능사용") == "100")
                btn_Mail.Visible = true;
            #endregion

            #region -> 시스템통제설정적용 지급관리사용여부
            지급관리통제설정 = BASIC.GetMAEXC("거래처정보등록-회계지급관리사용여부");
            
            #endregion

            if (BASIC.GetMAEXC("업체별프로세스") == "002")//세트렉아이
            {

                btn_ad_dist.Visible = true;
                btn_ad_nodist.Visible = true;
            }

            #region -> 전자결재버튼 visible true/false
            m_Elec_app = BASIC.GetMAEXC("전자결재-사용구분");
            if (m_Elec_app != "000" && BASIC.GetMAEXC("전자결재메뉴별사용여부-매입관리") == "100")
                btn전자결재.Visible = true;
            else
                btn전자결재.Visible = false;

            #endregion

            //규격형 사용 유무
            if (BASIC.GetMAEXC("공장품목등록-규격형") == "100")
            {
                bStandard = true;

            }

        }
        #endregion

        #endregion

        #region -> InitControl

        private void InitControl()
        {
            try
            {
                periodPicker1.StartDateToString = MainFrameInterface.GetStringFirstDayInMonth;

                periodPicker1.EndDateToString = MainFrameInterface.GetStringToday;

                btn_FI_CANCEL.Enabled = false;

                chk_line_notview.Checked = false;
                if (serverkey_common == "CHOSUNHOTELBA")//조선호텔 전용
                {
                    chk_line_notview.Visible = true; //속도를 향상시키기위해 라인데이터를 보여주지않음
                    chk_line_notview.Checked = true; //기본적으로 라인데이터를 안보이게 한다...
                }

                
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }

        }

        #endregion

        #region -> InitEvent
        private void InitEvent()
        {
            bp_CD_BIZAREA.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(OnBpControl_QueryAfter);
        }

        void OnBpControl_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                switch (((BpCodeTextBox)sender).Name)
                {
                    case "bp_CD_BIZAREA":
                        SetPlant();
                        break;
                }
            }
            catch(Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region -> InitPaint

        protected override void InitPaint()
        {
            DataSet g_dsCombo = this.GetComboData("S;PU_C000016", "S;YESNO", "S;MA_CODEDTL_003", "N;MA_B000005", "N;MA_B000095", "S;MA_PLANT");



            DataTable ldt_FG_TRANS = g_dsCombo.Tables[0].Clone();
            DataRow[] ldr_FgTrans = g_dsCombo.Tables[0].Select("CODE IN ('001', '002', '003')");

            ldt_FG_TRANS.Rows.Add("", "");
            if (ldr_FgTrans != null && ldr_FgTrans.Length > 0)
            {
                for (int i = 0; i < ldr_FgTrans.Length; i++)
                {
                    ldt_FG_TRANS.ImportRow(ldr_FgTrans[i]);
                }
            }
            

            // 거래구분		
            cbo_TRANS.DataSource = ldt_FG_TRANS;
            cbo_TRANS.DisplayMember = "NAME";
            cbo_TRANS.ValueMember = "CODE";

            // 전표처리	
            cbo_PT_PURCHASE.DataSource = g_dsCombo.Tables[1];
            cbo_PT_PURCHASE.DisplayMember = "NAME";
            cbo_PT_PURCHASE.ValueMember = "CODE";

            DataSet ds = GetComboData("N;FI_J000002", "N;PU_C000044");
            _flexM.SetDataMap("CD_DOCU", ds.Tables[0], "CODE", "NAME");
            

            if (지급관리통제설정 == "N")                
                _flexM.SetDataMap("FG_PAYBILL", ds.Tables[1], "CODE", "NAME");
            else
            {
                DataTable dt_pay = ComFunc.GetPayList();
                if (dt_pay != null)
                    _flexM.SetDataMap("FG_PAYBILL", dt_pay, "CODE", "NAME");
            }

            //_flexM.SetDataMap("CD_EXCH", g_dsCombo.Tables[3], "CODE", "NAME");

            if (_isCallIV)
            {
                bp_CD_BIZAREA.CodeValue = _cdbizarea; //cbo_CD_BIZAREA.SelectedValue = _cdbizarea;
                bp_CD_BIZAREA.CodeName = _nmbizarea;
                tb_NO_EMP.CodeValue = _noemp;
                tb_NO_EMP.CodeName = _nmemp;

                cbo_TRANS.SelectedValue = _fg_trans;
                periodPicker1.StartDateToString = _dtpo;
                periodPicker1.EndDateToString = _dtpo;

                OnToolBarSearchButtonClicked(null, null);
            }
            else //20091217 추가 by SJH
            {
                bp_CD_BIZAREA.CodeValue = LoginInfo.BizAreaCode; //cbo_CD_BIZAREA.SelectedValue = _cdbizarea;
                bp_CD_BIZAREA.CodeName = LoginInfo.BizAreaName;

                tb_NO_EMP.CodeValue = LoginInfo.EmployeeNo;
                tb_NO_EMP.CodeName = LoginInfo.EmployeeName;
            }

            _flexM.SetDataMap("YN_JEONJA", g_dsCombo.Tables[4], "CODE", "NAME"); //계산서발행형태

            dt_Plant = g_dsCombo.Tables[5];
            SetPlant();

            oneGrid1.UseCustomLayout = true;
            bpPanelControl1.IsNecessaryCondition = true;
            bpPanelControl2.IsNecessaryCondition = true;
            bpPanelControl6.IsNecessaryCondition = true;
            oneGrid1.InitCustomLayout();

            if (Global.MainFrame.ServerKeyCommon == "KIHA")
            {
                btnCompay.Visible = true;
                btnCompay.Click += new EventHandler(btnCompay_Click);
                btnCompay.Text = "고정자산처리";
            }

        }

        #endregion

        #region -> InitGrid

        private void InitGrid()
        {
            _flexM.BeginSetting(1, 1, true);
            _flexM.SetCol("CHK", "S", 30, true, CheckTypeEnum.T_F);
            _flexM.SetCol("NO_IV", "매입번호", 120);
            _flexM.SetCol("DT_PROCESS", "처리일", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexM.SetCol("CD_PARTNER", "거래처코드", 80);
            _flexM.SetCol("LN_PARTNER", "거래처명", 120);
            _flexM.SetCol("NO_BIZAREA", "사업자번호", 120, false, typeof(string));

            _flexM.Cols ["NO_BIZAREA"].Format = "###-##-#####";
            _flexM.SetStringFormatCol( "NO_BIZAREA" );

            _flexM.SetCol("NM_TRANS", "거래구분", 80);
            _flexM.SetCol("TXT_USERDEF1", "사용자정의1", 150, true);
            _flexM.SetCol("NM_TAX", "과세구분", 80);
            _flexM.SetCol("NM_EXCH", "환종", 80, false);
            _flexM.SetCol("RT_EXCH", "환율", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            _flexM.SetCol("QT_RCV_CLS", "수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexM.SetCol("AM_EX", "외화금액", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flexM.SetCol("AM_K", "공급가액", 120, false, typeof(decimal), FormatTpType.MONEY);
            _flexM.SetCol("VAT_TAX", "부가세", 100,  false, typeof(decimal), FormatTpType.MONEY);
            _flexM.SetCol("AM_SUP", "공급대가", 100, false, typeof(decimal), FormatTpType.MONEY);

           


            _flexM.SetCol("CD_DOCU", "전표유형", 80, true);
            _flexM.SetCol("TP_AIS", "처리상태", 80);
            _flexM.SetCol("FG_PAYBILL", "지급조건", 80, true, typeof(string));
            _flexM.SetCol("DT_PAY_PREARRANGED", "지급예정일", 80, true, typeof(string),FormatTpType.YEAR_MONTH_DAY);
            _flexM.SetCol("DT_DUE", "만기일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            _flexM.SetCol("NO_LC", "LC번호", 80);
            _flexM.SetCol("NM_FG_PAYMENT", "지급조건(발주)",false);
            _flexM.SetCol("DC_RMK", "비고", 200);
            _flexM.SetCol("NM_BIZAREA_TAX", "부가세사업장", 120);
            _flexM.SetCol("NO_DOCU", "전표번호", 120);
            _flexM.SetCol("FG_AD_YN", "관리구분저장여부", 120);
            _flexM.SetCol("YN_JEONJA", "계산서발행형태", 100, true);

            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "CNP")
            {
                _flexM.SetCol("NM_ST_STAT", "결재구분", 80, true);
            }

            if ((Global.MainFrame.ServerKeyCommon == "119") || (Global.MainFrame.ServerKeyCommon == "DZSQL") || (Global.MainFrame.ServerKeyCommon == "SQL_108") || (Global.MainFrame.ServerKeyCommon == "SEMICS"))
            {
                _flexM.Cols["TXT_USERDEF1"].Visible = true;

                _flexM.Cols["TXT_USERDEF1"].Caption = "현금영수증 승인번호";
                _flexM.Cols.Move(_flexM.Cols["TXT_USERDEF1"].Index, 9);
            }
            else
            {
                _flexM.Cols["TXT_USERDEF1"].Visible = false;
            }


            _flexM.SetCodeHelpCol("CD_PARTNER", HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, new string[] { "CD_PARTNER", "LN_PARTNER", "NO_BIZAREA" }, new string[] { "CD_PARTNER", "LN_PARTNER", "NO_COMPANY" }); //매입처 도움창
            _flexM.VerifyNotNull = new string[] { "NO_BIZAREA" };

            _flexM.SettingVersion = "1.0.0.7";
            
            _flexM.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);  // 반드시
            _flexM.AfterRowChange += new RangeEventHandler(_flexM_AfterRowChange);
            _flexM.StartEdit += new RowColEventHandler(Grid_StartEdit);
            _flexM.BeforeCodeHelp += new BeforeCodeHelpEventHandler(Grid_BeforeCodeHelp);
            _flexM.ValidateEdit += new C1.Win.C1FlexGrid.ValidateEditEventHandler(_flexM_ValidateEdit);
            _flexM.DoubleClick += new EventHandler(Grid_DoubleClick);
            _flexM.OwnerDrawCell += new OwnerDrawCellEventHandler(_flexM_OwnerDrawCell);

            _flexM.SetDummyColumn("CHK");

            _flexD.BeginSetting(1, 1, false);
            _flexD.SetCol("NO_LINE", "항번", 40);
            _flexD.SetCol("NO_IO", "입고번호", 120);
            _flexD.SetCol("DT_IO", "입고일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexD.SetCol("CD_ITEM", "품목코드", 120);
            _flexD.SetCol("NM_ITEM", "품목명", 150);
            _flexD.SetCol("STND_ITEM", "규격", 80, false, typeof(string));  // 2011.03.23 CBD 개발팀 김재훈 규격 추가
            _flexD.SetCol("CD_UNIT_MM", "단위", 80);
            _flexD.SetCol("PI_PARTNER", "주거래처", 80);
            _flexD.SetCol("PI_LN_PARTNER", "주거래처명", 120);
            _flexD.SetCol("GI_PARTNER", "납품처", 80);
            _flexD.SetCol("GI_LN_PARTNER", "납품처명", 120);
            _flexD.SetCol("QT_RCV_CLS", "수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexD.SetCol("UM_ITEM_CLS", "단가", 100, false, typeof(decimal), FormatTpType.UNIT_COST);
            
            if (bStandard)
            {
                _flexD.SetCol("UM_WEIGHT", "중량단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                _flexD.SetCol("TOT_WEIGHT", "총중량", 100, false, typeof(decimal));

                _flexD.Cols["TOT_WEIGHT"].Format = "###,###,###.#";
            }

            _flexD.SetCol("AM_CLS", "금액", 120, false, typeof(decimal), FormatTpType.MONEY);
            _flexD.SetCol("RT_EXCH", "환율", 100, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            _flexD.SetCol("UM_EX_CLS", "외화단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flexD.SetCol("AM_EX_CLS", "외화금액", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flexD.SetCol("VAT", "부가세", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flexD.SetCol("NO_PO", "발주번호", 120);
            
            _flexD.SetCol("NM_TPPURCHASE", "매입형태", 80);
            _flexD.SetCol("NM_PURGRP", "구매그룹", 120);
            _flexD.SetCol("NM_KOR", "담당자", 120);
            _flexD.SetCol("NM_PROJECT", "프로젝트", 120);
            _flexD.SetCol("NO_PROJECT", "프로젝트코드", 120);  // 2011.03.23 CBD 개발팀 김재훈 프로젝트코드 추가
            _flexD.SetCol("CD_CC", "C/C코드", 100);
            _flexD.SetCol("NM_CC", "C/C코드명", 100);

            if (Config.MA_ENV.PJT형여부 == "Y")
            {
                _flexD.SetCol("CD_PJT_ITEM", "프로젝트 품목코드", 140, false, typeof(string));
                _flexD.SetCol("NM_PJT_ITEM", "프로젝트 품목명", 140, false, typeof(string));
                _flexD.SetCol("PJT_ITEM_STND", "프로젝트 품목규격", 140, false, typeof(string));
                _flexD.SetCol("NO_WBS", "WBS번호", 100, false, typeof(string));
                _flexD.SetCol("NO_CBS", "CBS번호", 100, false, typeof(string));
                _flexD.SetCol("CD_ACTIVITY", "ACTIVITY 코드", 140, false, typeof(string));
                _flexD.SetCol("NM_ACTIVITY", "ACTIVITY", 140, false, typeof(string));
                _flexD.SetCol("CD_COST", "원가코드", 100, false, typeof(string));
                _flexD.SetCol("NM_COST", "원가명", 100, false, typeof(string));
            }

            if (Global.MainFrame.ServerKeyCommon == "KIHA")
            {
                _flexD.SetCol("CD_BUDGET", "예산단위코드", 150, true);
                _flexD.SetCol("NM_BUDGET", "예산단위명", 150, false);
                _flexD.SetCol("CD_BGACCT", "예산계정코드", 150, true);
                _flexD.SetCol("NM_BGACCT", "예산계정명", 150, false);

            }

            _flexD.SetCol("DC_RMK1", "비고1", 100);
            _flexD.SetCol("DC_RMK2", "비고2", 100);

            _flexD.SetCol("NM_CLS_ITEM", "품목계정", 100);
            _flexD.SetCol("NO_ORDER", "Order No", 120);
            _flexD.SetCol("AM_TOTAL", "합계", 120, false, typeof(decimal), FormatTpType.MONEY);

            _flexD.SettingVersion = "1.0.0.7";

            _flexD.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);  // 반드시
            _flexD.SetExceptSumCol("RT_EXCH", "MEMO_CD", "CHECK_PEN", "UM_ITEM_CLS", "UM_EX_CLS");


            _flexD.Cols["PI_PARTNER"].Visible = false;
            _flexD.Cols["PI_LN_PARTNER"].Visible = false;


            // 메모기능 
            _flexD.CellNoteInfo.EnabledCellNote = true;// 메모기능활성화 
            _flexD.CellNoteInfo.CategoryID = this.Name; // page 명입력 // 같은page명을입력했을경우여러화면에서볼수있습니다. 
            _flexD.CellNoteInfo.DisplayColumnForDefaultNote = "NM_ITEM";  // 마킹& 메모가보여질컬럼설정  

            _flexD.CheckPenInfo.EnabledCheckPen = true;// 체크펜기능활성화  
            _flexD.CellContentChanged += new CellContentEventHandler(_flexD_CellContentChanged);   // 메모& 체크추가, 삭제(수정제외) 되었을경우이벤트가발생.
             
        }

        #endregion

        #endregion

        #region ♣ 메인버튼 이벤트



        #region -> 조회버튼클릭

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!FieldCheck())
                {
                    return;
                }

                if (_flexM.DataTable != null)
                {
                    _flexM.DataTable.Rows.Clear();
                    _flexD.DataTable.Rows.Clear();
                }
                object[] obj = new object[]{D.GetString(bp_CD_BIZAREA.CodeValue),
                                            periodPicker1.StartDateToString, 
                                            periodPicker1.EndDateToString, 
                                            this.MainFrameInterface.LoginInfo.CompanyCode, 
                                            D.GetString(cbo_TRANS.SelectedValue), 
                                            D.GetString(cbo_PT_PURCHASE.SelectedValue), 
                                            tb_NM_PARTNER.CodeValue, 
                                            tb_NM_DEPT.CodeValue, 
                                            tb_NO_EMP.CodeValue,
                                            D.GetString(cbo_CD_PLANT.SelectedValue) };

                DataTable dt = _biz.Search(obj);
                DataTable dt2 = _biz.Search();

                _flexD.Binding = dt2;
                _flexM.Binding = dt; 	// 요기에서 곧바로 AfterRowColChange 이벤트 발생

                if (!_flexM.HasNormalRow)
                    this.ShowMessage(PageResultMode.SearchNoData);

                //_flexD[1, "UM_ITEM_CLS"] = "";
                //_flexD[1, "UM_EX_CLS"] = "";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }


        #endregion

        #region -> 저장버튼클릭

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSave())
                    return;

              //  SaveData();

                if (MsgAndSave(PageActionMode.Save))
                    this.ShowMessage(PageResultMode.SaveGood);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region -> SaveData

        protected override bool SaveData()
        {

            if (!this.Verify())
                return false;
            

            DataTable dt = _flexM.GetChanges();

            foreach (DataRow dr in dt.Rows) // 부가세값이 0 이 들어가는것을 영세일때도 체크하기때문에 따로 NULL인것만 체크해서 반환하게끔 하였습니다.
            {
                if (System.DBNull.Value == dr["VAT_TAX"])
                    return false;
            }

            if (dt == null)
                return false;   // 반드시

            bool result = _biz.Save(dt);

            if (result)
            {
                _flexM.AcceptChanges();
                _flexD.AcceptChanges();
                return true;
            }

            return false;
        }

        #endregion

        #region -> 삭제버튼클릭

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                DataRow [] rows = _flexM.DataTable.Select( "CHK ='T'" );
                if ( rows == null || rows.Length < 1 )
                {
                    this.ShowMessage( 공통메세지.선택된자료가없습니다 );
                    return;
                }

                // 전표처리가 완료된것이 선택되었는지 검사 
                DataRow [] ldr_Args1 = _flexM.DataTable.Select( "CHK ='T' AND TP_AIS ='Y'" );
                if ( ldr_Args1 != null && ldr_Args1.Length > 0 )
                {
                    this.ShowMessage( "PU_M000094" );
                    return;
                }

                if (BASIC.GetMAEXC("업체별프로세스") == "002")
                {
                    DataRow[] ldr_satrec = _flexM.DataTable.Select("CHK ='T' AND FG_AD_YN ='Y'");
                    if (ldr_satrec != null && ldr_satrec.Length > 0)
                    {
                        this.ShowMessage(DD("관리구분배부된 자료가존재합니다."));
                        return;
                    }
                }

                if (Global.MainFrame.ServerKeyCommon.ToUpper() == "CNP")
                {
                    DataRow[] dr_gw = _flexM.DataTable.Select("CHK ='T' AND ST_STAT IN ('0','1')");
                    if (dr_gw != null && dr_gw.Length > 0)
                    {
                        ShowMessage("결재 진행중이거나 승인된 건이 있으므로 삭제하실 수 없습니다.");
                        return;
                    }
                }

                string NO_IV;
                object [] lsa_args;

                DialogResult result = this.ShowMessage( 공통메세지.자료를삭제하시겠습니까 );
                if ( result == DialogResult.Yes )
                {
                    if ( rows != null && rows.Length > 0 )
                    {
                        for ( int r = _flexM.Rows.Count - 1 ; r >= _flexM.Rows.Fixed ; r-- )
                        {
                            if ( _flexM [r, "CHK"].ToString() == "T" )
                            {
                                NO_IV = _flexM [r, "NO_IV"].ToString();
                                lsa_args = new object [] { NO_IV, this.LoginInfo.CompanyCode };

                                _biz.Delete( lsa_args );
                            }
                        }
                    }
                    ShowMessage( 공통메세지.자료가정상적으로삭제되었습니다 );
                    OnToolBarSearchButtonClicked( sender, e );
                }


            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                _flexM.Redraw = true;
                OnToolBarSearchButtonClicked(null, null);
            }
        }

        #endregion        

        #region -> 인쇄버튼

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            try
            {
                string No_PK_Multi = "";

                DataRow[] ldt_Report = _flexM.DataTable.Select("CHK = 'T'");

                if (ldt_Report == null || ldt_Report.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    for (int i = _flexM.Rows.Fixed; i < _flexM.Rows.Count; i++)
                    {
                        if (_flexM[i, "CHK"].ToString() == "T")
                            No_PK_Multi += _flexM[i, "NO_IV"].ToString() + "|";
                    }
                }

                if (!_flexM.HasNormalRow) // && !_flexD.HasNormalRow)
                    return;


                //ReportHelper rptHelper = new ReportHelper("R_PU_IV_MNG_0", "매입관리");
                //rptHelper.가로출력();

                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic["사업장코드"] = bp_CD_BIZAREA.CodeValue;
                dic["사업장"] = bp_CD_BIZAREA.CodeName;
                dic["처리일자"] = periodPicker1.StartDateToString.Substring(0, 4) + "/" + periodPicker1.StartDateToString.Substring(4, 2) + "/" + periodPicker1.StartDateToString.Substring(6, 2) + " ~ " + periodPicker1.EndDateToString.Substring(0, 4) + "/" + periodPicker1.EndDateToString.Substring(4, 2) + "/" + periodPicker1.EndDateToString.Substring(6, 2);
                dic["담당부서코드"] = tb_NO_EMP.CodeName;
                dic["담당부서명"] = tb_NM_DEPT.CodeName;
                dic["거래처코드"] = tb_NM_PARTNER.CodeValue;
                dic["거래처명"] = tb_NM_PARTNER.CodeName;
                
                dic["거래구분"] = cbo_TRANS.Text;
                dic["전표처리"] = cbo_PT_PURCHASE.Text;
                dic["담당자코드"] = tb_NO_EMP.CodeValue;
                dic["담당자명"] = tb_NO_EMP.CodeName.ToString();

                //foreach (string key in dic.Keys)
                //{
                //    rptHelper.SetData(key, dic[key]);
                //}

                DataSet ds = _biz.Print(this.MainFrameInterface.LoginInfo.CompanyCode, No_PK_Multi);

                //rptHelper.SetDataTable(dt);

                //rptHelper.Print();

                P_PU_IV_MNG_PRINT _PRINT = new P_PU_IV_MNG_PRINT("R_PU_IV_MNG_0", "매입관리", true, ds, dic);
                _PRINT.ShowPrintDialog();
            
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }

        }

        #endregion

        #endregion

        #region ♣ 화면내 버튼

        #region -> 미결전표처리 버튼 클릭

        private void btn_IVPROCESS_Click(object sender, System.EventArgs e)
        {
            try
            {

                if (ToolBarSaveButtonEnabled)
                {
                    ShowMessage(DD("변경된 사항이 있습니다. 저장 후에 다시 처리 해야 합니다."));
                    return;
                }

                DataRow [] ldr_Args = _flexM.DataTable.Select( "CHK ='T'" );  // 선택된것 검사 , 선택이 안되면 전표처리가 불가 함으로 ㅋㅋㅋ 
                if (ldr_Args == null || ldr_Args.Length <= 0)
                {
                    this.ShowMessage("IK1_007");
                    return;
                }


                DataRow [] ldr_Args1 = _flexM.DataTable.Select( "CHK ='T' AND TP_AIS ='Y'" );                  // 이미 전표처리 된 것 검사
                if (ldr_Args1 != null && ldr_Args1.Length > 0)
                {
                    ShowMessage( "이미 전표처리된 건이 포함되어 있습니다" );
                    return;
                }

                //if (serverkey_common == "CNP")
                //{
                //    DataRow[] dr_gw = _flexM.DataTable.Select("CHK ='T' AND ST_STAT <> '1'");
                //    if (dr_gw != null && dr_gw.Length > 0)
                //    {
                //        ShowMessage("결재 승인된 건이 아니므로 전표처리 불가합니다.");
                //        return;
                //    }
                //}

                if (BASIC.GetMAEXC("업체별프로세스") == "002") //세트렉아이전용
                {
                    DataRow[] ldr_satrec = _flexM.DataTable.Select("CHK ='T' AND FG_AD_YN ='N'");
                    if (ldr_satrec != null && ldr_satrec.Length > 0)
                    {
                        this.ShowMessage(DD("관리구분배부 안된 자료가 존재합니다."));
                        return;
                    }
                }

                DataRow[] rows1 = _flexM.DataTable.Select("CHK ='T' AND TP_AIS = 'N' AND  AM_K = 0 AND VAT_TAX = 0 ");

                if (rows1 != null && rows1.Length > 0)
                {
                    DialogResult result;
                    // 변경된 내용이 있습니다.  저장하시겠습니까?
                    result = MessageBox.Show("공급금액/부가세가 0인 항목이 있습니다.\r\n" +
                                             "당 항목은 전표처리표시를 하되 회계전표를 만들지않습니다.\r\n" +
                                             "계속하시겠습니까?", "0값처리", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                    if (result == DialogResult.Cancel)
                        return;

                }

                DataTable ldt_Save = _flexM.DataTable.Clone();                  // 선택된것 담기..
                for (int i = 0; i < ldr_Args.Length; i++)
                {
                    ldt_Save.ImportRow(ldr_Args[i]);
                }

                
                //대한산업보건협회 전용
                if (Global.MainFrame.ServerKeyCommon == "KIHA")
                {
                    DialogResult Result =  KIHA(ldt_Save);

                    if (Result != DialogResult.OK)
                        return;
                }

                InDataInfoValue();              // 전표처리를 위한 기타 정보담기 

                bool bSuccess = false;

                foreach ( DataRow dr in ldt_Save.Rows )               // 전표 처리
                {
                    string P_CD_COMPANY = LoginInfo.CompanyCode;
                    string P_NO_IV = dr["NO_IV"].ToString();
                    string P_NO_MODULE = "210";  //회계전표유형 : 국내매입(210)
                    string Tx_내역표시구분 = Settings.Default.Tx_내역표시구분;      //내역표시구분            ( OPT_ITEM_RPT_GUBUN )
                    string Tx_내역표시_Text = Settings.Default.Tx_내역표시_Text;     //내역표시임의내용        ( OPT_ITEM_RPT_TEXT )
                    string Tx_품목표시구분 = Settings.Default.Tx_품목표시구분;      //품목표시구분            ( OPT_ITEM_NM_GUBUN )
                    bSuccess = _biz.미결전표처리( P_CD_COMPANY, P_NO_IV, P_NO_MODULE, Tx_내역표시구분, Tx_내역표시_Text, Tx_품목표시구분 );

                    if (Global.MainFrame.ServerKeyCommon == "KPCI")
                    { 
                        if ( bSuccess )
                        {
                            if (dr["CD_PARTNER"].ToString().Equals("1001349") || dr["CD_PARTNER"].ToString().Equals("1001351"))
                            {
                                DataTable Cal_dt = _biz.search_dt(dr["DT_PROCESS"].ToString());

                                string DT_LIMIT_D = D.GetString(Cal_dt.Rows[4]["DT_CAL"]);
                                string DT_LIMIT_C = D.GetString(Cal_dt.Rows[6]["DT_CAL"]);

                                _biz.KPCI_PAYMENT_INSERT(P_CD_COMPANY, P_NO_IV, DT_LIMIT_D, DT_LIMIT_C);        
                            }
                        }

                    }

                    for ( int i = 0 ; i < ldr_Args.Length ; i++ )               // 저장이 잘 되었으면 PU_IVH의 TP_AIS(전표처리여부)를 Y(처리)로 변경
                    {
                        if (ldr_Args[i]["NO_IV"].ToString() == P_NO_IV)
                        {
                            ldr_Args[i].BeginEdit();
                            ldr_Args[i]["TP_AIS"] = "Y";
                            ldr_Args[i].EndEdit();
                        }
                    }
                }

                if ( bSuccess )
                    ShowMessage( "전표가 처리되었습니다" );

                OnToolBarSearchButtonClicked( null, null );
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        // 전표 정보 담기
        private void InDataInfoValue()
        {
            DataRow newrow;
            ds_Ty1 = _biz.SettingDataTable();    
            ds_Ty1.Clear();
            newrow = ds_Ty1.NewRow();
            newrow["ID_INSERT"] = this.LoginInfo.UserID;							// 사용자
            newrow["CD_COMPANY"] = this.MainFrameInterface.LoginInfo.CompanyCode;	// 회사					
            newrow["MODULE"] = "PU";												// 모듈
            ds_Ty1.Rows.Add(newrow);
        }

        #endregion

        #region -> 전표처리취소 버튼 클릭

        private void btn_FI_CANCEL_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (Global.MainFrame.ServerKeyCommon == "FEELUX") //필룩스버튼권한없애달라는요청최면환팀장
                {
                    this.ShowMessage("전표처리취소버튼은사용하실수없습니다. 회계에서삭제가능합니다.");
                    return;
                }

                DataRow [] ldr_Args = _flexM.DataTable.Select( "CHK ='T'" );
                if ( ldr_Args == null || ldr_Args.Length <= 0 )
                {
                    this.ShowMessage( "IK1_007" );
                    return;
                }

                DataRow [] ldr_Args1 = _flexM.DataTable.Select( "CHK ='T' AND TP_AIS ='N'" );  // 이미 전표처리 된 것 검사
                if ( ldr_Args1 != null && ldr_Args1.Length > 0 )
                {
                    ShowMessage( "이미 전표취소된 건이 포함되어 있습니다" );
                    return;
                }

                DataTable ldt_Save = _flexM.DataTable.Clone();  // 선택된것 담기..
                for ( int i = 0 ; i < ldr_Args.Length ; i++ )
                {
                    ldt_Save.ImportRow( ldr_Args [i] );
                }

                InDataInfoValue();                  // 전표처리를 위한 기타 정보담기 

                bool bSuccess = false;

                // 전표 처리
                foreach ( DataRow dr in ldt_Save.Rows )
                {
                    string P_CD_COMPANY = LoginInfo.CompanyCode;
                    string P_NO_MODULE = "210";  //회계전표유형 : 국내매입(210)
                    string P_NO_IV = dr ["NO_IV"].ToString();

                    bSuccess = _biz.미결전표취소( P_NO_MODULE, P_NO_IV );

                    if (Global.MainFrame.ServerKeyCommon == "KPCI")
                    {
                        if (bSuccess)
                        {
                            if (dr["CD_PARTNER"].ToString().Equals("1001349") || dr["CD_PARTNER"].ToString().Equals("1001351"))
                            {
                                _biz.KPCI_PAYMENT_DELETE(P_CD_COMPANY, P_NO_IV);
                            }
                        }

                    }
              
                    for ( int i = 0 ; i < ldr_Args.Length ; i++ )
                    {
                        if ( ldr_Args [i] ["NO_IV"].ToString() == P_NO_IV )
                        {
                            ldr_Args [i].BeginEdit();
                            ldr_Args [i] ["TP_AIS"] = "N";           // 전표취소가 되었으면 PU_IVH의 TP_AIS(전표처리여부)를 N(처리)로 변경
                            ldr_Args [i].EndEdit();
                            btn_FI_CANCEL.Enabled = false;
                        }
                    }
                }

                if ( bSuccess )
                    ShowMessage( "전표가 취소되었습니다" );

                OnToolBarSearchButtonClicked( null, null );

            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        #region -> 메일발송 : 사용하실때 이부분만 복사해서 사용하시면 됩니다.
        private void btn_Mail_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flexM.HasNormalRow) return;

                string No_PK_Multi = "";

                DataRow[] ldt_Report = _flexM.DataTable.Select("CHK = 'T'");

                if (ldt_Report == null || ldt_Report.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    for (int i = _flexM.Rows.Fixed; i < _flexM.Rows.Count; i++)
                    {
                        if (_flexM[i, "CHK"].ToString() == "T")
                            No_PK_Multi += _flexM[i, "NO_IV"].ToString() + "|";
                    }
                }

                if (!_flexM.HasNormalRow)// && !_flexD.HasNormalRow)
                    return;

                DataSet ds = _biz.Print(this.MainFrameInterface.LoginInfo.CompanyCode, No_PK_Multi);// NO 와 CD_PARTNER 가 꼭 포함되어있어야합니다.
               // ReportHelper[] ARR_rptHelper = null;

                ReportHelper rptHelper = new ReportHelper("R_PU_IV_MNG_0", "매입관리");
                rptHelper.가로출력();

                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic["사업장코드"] = bp_CD_BIZAREA.CodeValue;
                dic["사업장"] = bp_CD_BIZAREA.CodeName;
                dic["처리일자"] = periodPicker1.StartDateToString.Substring(0, 4) + "/" + periodPicker1.StartDateToString.Substring(4, 2) + "/" + periodPicker1.StartDateToString.Substring(6, 2) + " ~ " + periodPicker1.EndDateToString.Substring(0, 4) + "/" + periodPicker1.EndDateToString.Substring(4, 2) + "/" + periodPicker1.EndDateToString.Substring(6, 2);
                dic["담당부서코드"] = tb_NO_EMP.CodeName;
                dic["담당부서명"] = tb_NM_DEPT.CodeName;
                dic["거래처코드"] = tb_NM_PARTNER.CodeValue;
                dic["거래처명"] = tb_NM_PARTNER.CodeName;
                
                dic["거래구분"] = cbo_TRANS.Text;
                dic["전표처리"] = cbo_PT_PURCHASE.Text;
                dic["담당자코드"] = tb_NO_EMP.CodeValue;
                dic["담당자명"] = tb_NO_EMP.CodeName.ToString();

                

                foreach (string key in dic.Keys)
                {
                    rptHelper.SetData(key, dic[key]);
                }


                
                //파트너 배열 생성, 담당자를 끌고오기위한
                string[] str_partner = null;
                string multi_partner = string.Empty;
                foreach (DataRow dr_H in ldt_Report) //거래처를 따로빼놓아서 작업 //데이터테이블에서 파트너 빼오는걸로 바꾸자
                {
                    Dictionary<string, string> dic_partner = new Dictionary<string, string>();// 파트너가 중복으로 들어가지않도록 이쪽에도 넣어주어서 중복으로 들어가는것을 체크하는 용도 실질적으로 이쪽데이터가 반영되는것은 없음

                    if (!dic_partner.ContainsKey(D.GetString(dr_H["CD_PARTNER"])))
                    {
                        dic_partner[D.GetString(dr_H["CD_PARTNER"])] = D.GetString(dr_H["CD_PARTNER"]);
                        multi_partner += D.GetString(dr_H["CD_PARTNER"]) + "|";
                    }

                    str_partner = multi_partner.Split('|'); //D.StringConvert.GetPipes(multi_partner); //.Split
                }

                string pkcol_name = "NO_IV";
                P_MF_EMAIL_NOMULTI sub = new P_MF_EMAIL_NOMULTI("R_PU_IV_MNG_0", new ReportHelper[] { rptHelper }, dic, "매입관리", pkcol_name, ds.Tables[0]); //(RDF오브젝트명 , 레포트헬퍼, 프린트헤더정보, RDF기본명, NO로 들어갈 컬럼명, 데이터)
                sub.ShowDialog();

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region -> 관리구분배부 쎄트렉아이
        private void btn_ad_dist_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flexM.HasNormalRow) return;
                DataTable dtH = _flexM.DataTable.Clone().Copy();
                DataTable dtL = _flexD.DataTable.Clone().Copy();

                foreach(DataRow drH in _flexM.DataTable.Select("NO_IV = '" + D.GetString(_flexM["NO_IV"]) + "'")) //멀티선택을 감안하여 이렇게 짜놓음
                {
                    foreach(DataRow drL in _flexD.DataTable.Select("NO_IV = '" + D.GetString(_flexM["NO_IV"]) + "'"))
                    {
                        dtL.LoadDataRow(drL.ItemArray, true);
                    }
                
                    dtH.LoadDataRow(drH.ItemArray, true);
                }

                P_PU_IV_AD_DIST_SUB _dlg = new P_PU_IV_AD_DIST_SUB(dtH, dtL, D.GetString(_flexM["FG_AD_YN"]));
                if(_dlg.ShowDialog() == DialogResult.OK)
                {
                    _flexM["FG_AD_YN"] = "Y";
                }

            }
            catch (Exception ex)
            {
               MsgEnd(ex);
            }
        }
        #endregion

        #region -> 관리구분배부 쎄트렉아이
        private void btn_ad_nodist_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flexM.HasNormalRow) return;

                if (D.GetString(_flexM["TP_AIS"]) == "Y")
                {
                    ShowMessage("이미 전표처리된 건이 포함되어 있습니다");
                    return;
                }

                if (D.GetString(_flexM["FG_AD_YN"]) == "Y")
                {
                    object[] obj = new object[]
                    {
                        Global.MainFrame.LoginInfo.CompanyCode,
                        D.GetString(_flexM["NO_IV"])
                    };

                    _biz.Delete_ad_dist(obj);
                    _flexM["FG_AD_YN"] = "N";
                }
                    


            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region -> 전자결재
        private void btn전자결재_Click(object sender, EventArgs e)
        {
            try
            {
                #region -> 결재체크
                if (!BeforeSave()) return;
                if (!_flexD.HasNormalRow) return;
                if (!_flexM.HasNormalRow) return;

                DataRow[] dr = _flexM.DataTable.Select("CHK = 'T'");

                if (dr.Length != 1 || dr == null)
                {
                    ShowMessage(DD("한개의 매입건을 선택해 주십시오."));
                    return;
                }

                string NO_IV = D.GetString(dr[0]["NO_IV"]);

                DataRow[] drd = _flexD.DataTable.Select("NO_IV = '" + NO_IV + "'");

                if (serverkey_common == "CNP")
                {
                    _CNP전자결재(NO_IV, dr, drd);
                    return;
                }

                bool bTrue = true;

                //2:미상신 0:진행중 1:종결 -1:반려 3:취소(삭제)
                string[] st_stat_msg = new string[5] { DD("진행"), DD("종결"), DD("미상신"), DD("취소"), DD("반려") };

                int i_stat = _biz.GetFI_GWDOCU(NO_IV);

                bool save_true = true;

                if (i_stat != 999)
                {
                    if (i_stat == -1) i_stat = 4;
                    ShowMessage("전자결제 @중 입니다.", st_stat_msg[i_stat]);
                    save_true = false;
                    if (i_stat == 4) i_stat = -1;
                }
                #endregion

                #region -> 실제 업체 결재상신 로직

                #region -> CIS

                P_PU_IV_MNG_GW _gw = new P_PU_IV_MNG_GW();

                save_true = _gw.save_TF(i_stat); 

                //object[] obj = new object[] { Global.MainFrame.LoginInfo.CompanyCode, NO_IV };
                //DataTable dt_GWdata = _biz.DataSearch_GW_RPT(obj);

                string[] strContents = _gw.getGwSearch(m_Elec_app, dr, drd);

                if (strContents == null || strContents.Length == 0) return;

                if (_flexD.Rows.Count > 0 && save_true)
                    bTrue = _biz.전자결재_실제사용(dr[0], strContents[0], strContents[1], strContents[3]);// App_Form_Kind를 추가한것은 회사마다 App_Form_Kind 번호가 중복되는 것들이 있기 때문에 따로 넘겨줘야 하므로 

                if (!bTrue) return;

                string strURL = "";
                string NO_DOCU = NO_IV;

                //한글처리를 위해서 Encode 사용!
                strURL = "cd_company=" + LoginInfo.CompanyCode + "&cd_pc=" + LoginInfo.CdPc + "&no_docu=" + System.Web.HttpUtility.UrlEncode(NO_DOCU, Encoding.UTF8) + "&login_id=" + LoginInfo.EmployeeNo;

                System.Diagnostics.Process.Start("IExplore.exe", strContents[2] + strURL);

                
                #endregion

                #endregion
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region -> _CNP전자결재
        private void _CNP전자결재(string no_iv, DataRow[] dr, DataRow[] drd)
        {
            try
            {
                int i_stat = _biz.GetFI_GWDOCU(no_iv);

                if (i_stat == 0 || i_stat == 1)
                {
                    ShowMessage((i_stat == 1) ? "결재 승인된 건입니다." : "결재 진행중인 건입니다.");
                    return;
                }
                
                if (i_stat == 2)
                {
                    ShowMessage("결재상태가 미상신 중 입니다.");
                    return;
                }

                P_PU_IV_MNG_GW gw = new P_PU_IV_MNG_GW();

                string[] sParams = gw.getGwSearch(m_Elec_app, dr, drd);
                if (sParams == null || sParams.Length == 0) return;

                string returnmsg = sParams[0];
                if (returnmsg == "00")
                {
                    if (!_biz.전자결재_CNP_실제사용(dr[0], sParams[0], sParams[1], sParams[3]))
                        return;
                }
                else
                {
                    ShowDetailMessage("전자결재시 오류가 발생하였습니다.", returnmsg);
                    return;
                }

                ShowMessage(공통메세지._작업을완료하였습니다, "전자결재");

                //string param = "cd_company=" + MA.Login.회사코드 + "&cd_pc=" + LoginInfo.CdPc + "&no_docu=" + System.Web.HttpUtility.UrlEncode(no_iv, Encoding.UTF8) + "&login_id=" + LoginInfo.EmployeeNo;
                //System.Diagnostics.Process.Start("IExplore.exe", sParams[2] + param);

                return;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region -> 업체전용
        void btnCompay_Click(object sender, EventArgs e)
        {
            try
            {
                Control ctrl = (Control)sender;

                if (ctrl.Text == "고정자산처리")
                {
                    DataRow[] ldr_Args = _flexM.DataTable.Select("CHK ='T'");  
                    if (ldr_Args == null || ldr_Args.Length <= 0)
                    {
                        this.ShowMessage("IK1_007");
                        return;
                    }

                    foreach (DataRow row in ldr_Args)
                    {
                        DataRow[] rowD = _flexD.DataTable.Select("NO_IV ='" + D.GetString(row["NO_IV"]) + "' AND CLS_ITEM NOT IN('006','007','008','010')");

                        if (rowD != null && rowD.Length > 0)
                        {
                            this.ShowMessage("자산계정이 아닌 품목이 존재합니다.  처리할 수 없습니다.");
                            return;
                        }
                    }

                    bool bSuccess = false;
                    foreach (DataRow row in ldr_Args)
                    {
                        bSuccess = _biz.고정자산처리(D.GetString(row["NO_IV"]));
                    }

                    if (bSuccess)
                        ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
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

        #region -> 그리드 변경 시작시 체크 이벤트(Grid_StartEdit)

        private void Grid_StartEdit(object sender, RowColEventArgs e)
            { 
                try
                {   // 미결전표처리가 되었으면 수정할 수 없다

                    if (_flexM.Cols[e.Col].Name == "CHK")
                        e.Cancel = false; 
             
                    else if (_flexM[e.Row,"TP_AIS"].ToString().Trim() == "Y")
                        e.Cancel = true;

                    else if (_flexM.Cols[e.Col].Name == "TXT_USERDEF1")
                    {
                        if (!_flexM["NM_TAX"].Equals("현금영수증"))
                        {
                            e.Cancel = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MsgEnd(ex);
                }
            }

            #endregion
               
        #region -> 그리드 도움창 호출전 세팅 이벤트(Grid_BeforeCodeHelp)

        private void Grid_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                switch (e.Parameter.HelpID)
                {
                    case HelpID.P_MA_PARTNER_SUB:
                        if (D.GetString(_flexM["TP_AIS"]) == "Y")
                        {
                            e.Cancel = true;
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

        #region -> _flexM_AfterRowColChange

        private void _flexM_AfterRowChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
            try
            {
                if (!_flexM.IsBindingEnd || !_flexM.HasNormalRow)
                {
                    btn_FI_CANCEL.Enabled = false;
                    _flexD.EmptyRowFilter();
                    return;
                }

                DataTable dt = _flexM.GetChanges();

                //if ( _flexM.Cols [e.NewRange.c1].Name == "CHK" )
                //    _flexM.AllowEditing = true;
                //else
                //    _flexM.AllowEditing = false;

                if (_flexM[_flexM.Row, "TP_AIS"].ToString().Trim() == "Y")
                {
                    btn_FI_CANCEL.Enabled = true;
                    btn_IVPROCESS.Enabled = false;
                }
                else
                {
                    btn_FI_CANCEL.Enabled = false;
                    btn_IVPROCESS.Enabled = true;
                }


                if (e.OldRange.r1 != e.NewRange.r1)
                {
                    _flexD.Redraw = false;

                    if (chk_line_notview.Checked == false) { ShowDetail(e.NewRange.r1); } //라인데이터를 안보여주는거면 조회해오지 못하게한다
                    
                    
                    dt = _flexM.GetChanges();
                    _flexD.Redraw = true;
                }
            }
            catch(Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        #region -> _flexM_ValidateEdit

        private void _flexM_ValidateEdit(object sender, C1.Win.C1FlexGrid.ValidateEditEventArgs e)
        {
            try
            {

                Decimal oldValue = D.GetDecimal(((FlexGrid)sender).GetData(e.Row, e.Col).ToString());
                Decimal newValue = D.GetDecimal(((FlexGrid)sender).EditData);
                Decimal vsoldValue = (oldValue + 99);
                Decimal vmoldValue = (oldValue - 99);
                Decimal roopValue = Math.Abs(oldValue - newValue);
                CommonFunction objFunc = new CommonFunction();

                string ColName = ((FlexGrid)sender).Cols[e.Col].Name;
                
                if (_flexM.AllowEditing)
                {			//	System.Diagnostics.Debugger.Break();
                    if (_flexM.GetData(e.Row, e.Col).ToString() != _flexM.EditData)
                    {
                        switch (_flexM.Cols[e.Col].Name)
                        {
 
                            case "FG_PAYBILL":
                                string dt_due = string.Empty;
                                string dt_pay = string.Empty;

                                if (지급관리통제설정 == "Y")
                                {
                                    decimal ldb_amk = D.GetDecimal(_flexM["AM_K"]);
                                    dt_pay = ComFunc.만기예정일자(periodPicker1.StartDateToString, ldb_amk, D.GetString(_flexM.EditData), "1");
                                    // 만기예정일
                                    if (dt_pay != string.Empty)
                                    {
                                        _flexM[_flexM.Row, "DT_PAY_PREARRANGED"] = dt_pay;
                                        _flexM[_flexM.Row, "DT_DUE"] = dt_pay;
                                    }

                                    //어음 만기일(구분이 어음이 아닐경우 Empty가 return됨)
                                    dt_due = ComFunc.만기예정일자(periodPicker1.StartDateToString, ldb_amk, D.GetString(_flexM.EditData), "2");
                                    if (dt_due != string.Empty)
                                        _flexM[_flexM.Row, "DT_DUE"] = dt_due;

                                }

                                if (지급관리통제설정 == "N" || dt_pay == string.Empty || dt_due == string.Empty)
                                {
                                    string str_temp = objFunc.DateAdd(periodPicker1.StartDateToString, "D", Convert.ToInt16(_flexM["DT_PAY_PREARRANGED"].ToString()));
                                    if (dt_pay == string.Empty)
                                        _flexM["DT_PAY_PREARRANGED"] = str_temp;

                                    if (dt_due == string.Empty)
                                        _flexM["DT_DUE"] = _flexM["DT_PAY_PREARRANGED"];
                                }
                                break;

                        }
                    }
                }
            }
            catch
            {
            }

        }

        #endregion

        #region -> Grid_DoubleClick
        private void Grid_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (_flexM.Cols[_flexM.Col].Name == "NO_DOCU") //ETAX_KEY
                {
                    if (D.GetString(_flexM["NO_DOCU"]) != "")
                    {
                        object[] args = {
                                       D.GetString(_flexM["NO_DOCU"]), //-- 전표번호
                                       "1", //D.GetString(_flexH["NO_ACCT"]), //-- 회계번호(모르면1)
                                       D.GetString(_flexM["CD_PC"]), //-- 회계단위
                                       Global.MainFrame.LoginInfo.CompanyCode //--회사코드
                                };

                        CallOtherPageMethod("P_FI_DOCU", "전표입력(" + PageName + ")", "P_FI_DOCU", Grant, args);
                    }
                }
            }
            catch
            {
            }
        }

        #endregion

        #region -> 그리드  메모기능 체크펜기능
        void _flexD_CellContentChanged(object sender, CellContentEventArgs e)
        {
            try
            {
                _biz.SaveContent(e.ContentType, e.CommandType, D.GetString(_flexD[e.Row, "NO_IV"]), D.GetDecimal(_flexD[e.Row, "NO_LINE"]), e.SettingValue);

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region -> _flexH_OwnerDrawCell
        // 그리드 컬럼 소트때문에 여기 이벤트 사용 - 속도 차이는 없음
        void _flexM_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            if (e.Row < _flexM.Rows.Fixed || e.Col < _flexM.Cols.Fixed)
                return;

            CellStyle s = _flexM.Rows[e.Row].Style;

            if (s == null)
            {
                if (D.GetString(_flexM[e.Row, "TP_AIS"]) == "Y")
                {
                    CellStyle C = _flexM.Styles.Add("YELLOW");
                    C.BackColor = Color.Yellow;

                    _flexM.Rows[e.Row].Style = C;
                }
            }
            else if (s.Name == "YELLOW")
            {
                CellStyle C = _flexM.Styles.Add("YELLOW");
                C.BackColor = Color.Yellow;

                _flexM.Rows[e.Row].Style = C;
            }

        }
        #endregion
        
        #endregion

        #region ♣ 기타메소드

        #region -> ShowDetail

        private void ShowDetail(int row)
        {
            string filter = "NO_IV = '" + _flexM[_flexM.Row, "NO_IV"].ToString() + "'";

            _flexD.RowFilter = filter;			// 이 문장이 반드시 처음으로 와야 한다.

            // 왜냐하면 디테일이 아무것도 없는경우 추가 버튼을 눌렀을 때 잘 되게 하기 위해
            if (!_flexM[row, "AFTER_OK"].Equals("Y"))
            {
                DataTable dt = _biz.Search(this.LoginInfo.CompanyCode, _flexM[_flexM.Row, "NO_IV"].ToString());

                if (dt != null && dt.Rows.Count > 0)
                {
                    _flexD.SetDummyColumnAll();	// 모든 컬럼을 더미컬럼으로 설정하고 -> 인서트시 빨리 처리되게 하기 위해

                    DataTable srcTable = _flexD.DataTable;

                    for (int r = 0; r < dt.Rows.Count; r++)
                    {
                        _flexD.DataTable.LoadDataRow(dt.Rows[r].ItemArray, true);
                    }

                    _flexD.RemoveDummyColumnAll();	// 다시 원상태로 돌린다.
                }

                _flexM[row, "AFTER_OK"] = "Y";
                _flexM.AcceptChanges();
            }

            this.ToolBarDeleteButtonEnabled = true;
        }


        protected override bool IsChanged()
        {
            if ( base.IsChanged() )
                return true;

            return false;          
        }


        #endregion

        #region -> 체크 필드

        private bool FieldCheck()
        {
            if (periodPicker1.StartDateToString == "")
            {
                this.ShowMessage("WK1_004", lb_DT_IV.Text);
                periodPicker1.Focus();
                return false;
            }

            if (periodPicker1.EndDateToString == "")
            {
                this.ShowMessage("WK1_004", lb_DT_IV.Text);
                periodPicker1.Focus();
                return false;
            }

            if (bp_CD_BIZAREA.GetCodeValue() == string.Empty)
            {
                this.ShowMessage("WK1_004", lb_NM_BIZAREA.Text);
                bp_CD_BIZAREA.Focus();
                return false;
            }

            return true;
        }

        #endregion

        #region -> 공장 setting

        private void SetPlant()
        {
            try
            {
                string cd_bizarea = D.GetString(bp_CD_BIZAREA.CodeValue);

                string Filter = "CD_BIZAREA ='" + cd_bizarea + "' OR CD_BIZAREA = '" +""+"'";
                DataView dv = dt_Plant.Copy().DefaultView;

                if (dt_Plant != null && dt_Plant.Rows.Count != 0)
                    dv = new DataView(dt_Plant, Filter, "", DataViewRowState.CurrentRows);

                cbo_CD_PLANT.DataSource = dv.ToTable();
                cbo_CD_PLANT.DisplayMember = "NAME";
                cbo_CD_PLANT.ValueMember = "CODE"; 
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 발행옵션
        private void btn_Tp_Ais_Option_Click(object sender, EventArgs e)
        {
            try
            {
                pur.P_PU_IVMNG_ETAX36524D_OPTION_SUB obj = new P_PU_IVMNG_ETAX36524D_OPTION_SUB();

                if (obj.ShowDialog() == DialogResult.OK)
                {

                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        DialogResult KIHA(DataTable dt)
        {
            P_PU_Z_KHIA_BUDGET_SUB _dlg = new P_PU_Z_KHIA_BUDGET_SUB(dt);

                return _dlg.ShowDialog();
        }

        #endregion

        #region ♣ 컨트롤 이벤트

        #region -> tb_COMBO_KeyDown

        private void tb_COMBO_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData.ToString() == "Enter")
            {
                SendKeys.SendWait("{TAB}");
            }
        }

        #endregion

        #region -> OnControl_Validated

        private void OnControl_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!((DatePicker)sender).Modified) return;

                if (!((DatePicker)sender).IsValidated)
                {
                    this.ShowMessage("WK1_003");
                    ((DatePicker)sender).Focus();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        #region -> OnControl_CalendarClosed

        private void OnControl_CalendarClosed(object sender, EventArgs e)
        {
            try
            {
                OnControl_Validated(sender, e);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion


        #endregion
    }
}

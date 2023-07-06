using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

using Duzon.Common.Controls;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;

using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms.Help;
using DzHelpFormLib;
using Duzon.Windows.Print;

using Duzon.ERPU.MF;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.MF.EMail;
using System.Collections.Generic;
using System.IO;
using Duzon.ERPU.PU.Common;
using Duzon.ERPU.PU;
using Duzon.Erpiu.Windows.OneControls;
using Duzon.ERPU.MF.SMS;


namespace pur
{


// **************************************
// 작   성   자 : 최기도
// 작   성   일 : 2006-09-28
// 페 이 지  명 : 구매발주등록
// 수   정   자 : 오성영
// 수   정   일 : 2007-11-21
// 수 정  내 역 : 엑셀업로그 기능, 비고 추가
//              : 2009.12.08 CC설정 수정 - SMR (백광, KTIS)
//              : 2009.12.08 의제매입여부 추가 수정
//              : 2010.01.12 납기일HEDER 추가 (적용및 LINE 추가시 납기일 DEFAULT 값 적용) - SMR(지엔텔/이형준)
//                           기존에 첫라인으로 DEFAULT 값하는것은 HEADER에 값 없을때만 사용
//              : 2010.01.15 단위중량추가 -SMR (제다/이형준)
//              : 2010.01.28 전자결재 그룹웨어연동 백광 산업 추가: 안종호, 이형준대리(백광산업)
//              : 2010.02.08 FG_POST(오더상태)명 오류 수정 추가시(적용포함) 모두 Planning로 Fixed 시켰음
//                           확인결과 적용되어지는 값에 따라서 달라질 수 있으므로 fg_post가무조건 '0'인것도 수정되어야하며
//                           그 명칭도 적용되는 시점에 따라서 바꿔야한다. -SMR 제다 수주적용작업중 오류발견(도움:김헌섭)
//              : 2010.02.11 전자결재 사용여부 환경설정에 따라 버튼 visible true/false 처리 - SMR (공통)
//              : 2010.02.11 - 안종호- 총급액(공급가액+ 부가세) 컬럼추가(김헌섭대리,크라제)
//              : 2010.02.22 - 안종호 - 납품처코드, 납품처명 컬럼추가(이형준대리, 싸이시스)
//              : 2010.03.18 - 신미란 - update 저장시 예산체크부분 _구매예산CHK설정 체크가 되질 않아 예산체크 메세지가 뜨는버그 수정(공통)
//                                    - 전용코드 호출하는 부분 Duzon.ERPU.MF.ComFunc.전용코드 함수 이용으로 수정 CC 설정부분만...
//                                    - 발주단위에 관계없이 환산량이 있으면 환산량을 곱해서 발주수량을 만든다.
//              : 2010.03.25 - 안종호 - 백광산업 : 전자결재 HTML수정
//                                    - SKDND : 발주서 수정
//              : 2010.03.29 - 신미란 - SKDND : EXCEL 업로드 품목체크로직 수정(첫라인 없는 품목이 아닐경우 
//                                              품목CHECK 로직 안타는 부분 수정 (문점용)
//              : 2010.04.27 - 신미란 -  빠르게 입력하면 AfterCodeHelp쪽에 다른 정보 가져오지 못하는 부분 수정
//              : 2010.04.28 - 신미란 - 니콘전용 H41 연동(김헌섭/니콘)
//              : 2010.06.08 - 신미란 - EXCEL 품목 CHECK 수정 , 금액 계산 수정
//              : 2010.06.09 - 신미란 - 그리드 프로젝트 도움창 수정
//              : 2010.06.22 - 안종호 - 수주적용통제설정추가
//              : 2010.07.13 - 안종호- 가격조건상세, 도착지, 선적지추가
//              : 2010.07.20 - 안종호 - 발주등록시 요청/품의 거래처 적용 기능 추가
//              : 2010.07.29 - 안종호 - LOT사이즈적용 기능 추가 (이정운대리) , 선택버튼추가
//              : 2010.08.25 - 신미란 - 원화금액 자동계산시 소수점 절사처리
//              : 2010.09.29 - 안종호 - 발주형태,공장품목 부가세율 적용추가
//              : 2010.10.06 - 안종호 - 넥스트아이 그룹웨어 추가
//              : 2011.03.22 - 김현철 - P_PU_STOCK_SUB 참조 제거
//              : 2011.04.12 - 최창종 - 발주의뢰서 수정(대림 html)
//              : 2013.07.24 - 윤성우 - ONEGRID 수정작업(등록단)
// **************************************

#region ♣ 메세지

public enum 메세지
{
    거래구분이국내일때만자동의뢰및입고행위가가능합니다,
    공장을먼저선택하십시오,
    삭제할수없습니다
}
#endregion
    
#region ♣ 발주등록

public partial class P_PU_PO_REG2 : Duzon.Common.Forms.PageBase
{
    #region ♣ 생성자 & 변수 선언

    #region -> 변수 선언
    private P_PU_PO_REG2_BIZ _biz = null;
    private FreeBinding _header = null;

    private string str발주번호;
    private string _ComfirmState;

    pur.CDT_PU_RCVH cPU_RCVH = new pur.CDT_PU_RCVH();
    pur.CDT_PU_RCV cPU_RCVL = new pur.CDT_PU_RCV();

    string str복사구분 = string.Empty;        //조회도움창에서 적용인지 복사인지
    bool 호출여부 = false; bool 비고수정여부 = false;

    string strSOURCE;       //MRP에서 넘기는 생성자인자
    decimal dNO_LINE;       //MRP에서 넘기는 생성자인자(항번)
    ReportHelper rptHelper;

    /// <summary>
    //영우에 전용설정위한 Flag 셋팅
    /// </summary>
    private string _전용설정 = "000";
    private string m_sEnv = "N";        // 재고단위 EDIT 여부
    private string m_sEnv_CC = "000", m_sEnv_CC_Line = "N";
    private string m_sEnv_FG_TAX = "000"; // 부가세 기본값 설정
    private string m_sEnv_Prt_Option = "000"; // 출력옵션사용 설정
    private string m_sEnv_App_Am = "000"; //품의적용시 금액기준 설정
    private string m_sEnv_Nego = BASIC.GetMAEXC("발주등록(공장)-업체별프로세스선택");
    private string m_sEnv_App_Sort = "000"; //품의적용시 금액기준 설정

    /// <summary>
    /// 전자결재 사용유무
    /// </summary>
    private string m_Elec_app = "000";//전자결재 사용코드 

    /// <summary>
    /// 요청_품의_거래처 적용사용유무 000 사용안함 , 100 사용함 
    /// </summary>
    string _m_partner_use = "000";
    string _m_partner_change = "000";

    /// <summary>
    /// 품의_발주유형 적용사용유무 000 사용안함 , 001사용함 
    /// </summary>
    string _m_tppo_use = "000";
    string _m_tppo_change = "000";

    /// <summary>
    /// LOTSIZE 적용사용유무 000 사용안함 , 100 사용함 
    /// </summary>
    string _m_lotsize_use = "000";

    String _구매예산CHK설정 = "N";

    /// <summary>
    /// Excel Upload에서 사용 PJT검사
    /// </summary>
    DataTable _dt_pjt = null;

    /// <summary>
    /// 납품일자 통제 000 사용안함 , 100 사용함 
    /// </summary>
    string _m_dt = "000";

    /// <summary>
    /// 업체전용 프로세스 000 사용안함(ERP-IU기본), 001 아사히카세히전용
    /// </summary>
    string _m_Company_only = "000";
    string _반품발주 = BASIC.GetMAEXC("반품발주사용여부");
    /// <summary>
    /// 코드관리에서 컬럼명 가져오기
    /// </summary>
    ///         
     DataTable dt공장 = null;

    /// <summary>
     /// 구매발주-외주유무 000 : 사용안함, 100 : 사용함 (구매발주에서 외주수입을 사용할지 여부)
    /// </summary>
     string sPUSU = BASIC.GetMAEXC("구매발주-외주유무");
     string s소요자재체크 = BASIC.GetMAEXC("발주등록-외주소요자재체크사용");


    

     /// <summary>
     /// 공장품목등록-규격형 000 : 사용안함, 100 : 사용함 
     /// </summary>
     bool bStandard = false; //규격형

    private string NUM_USERDEF4 = "NUM_USERDEF4";
    private string NUM_USERDEF5 = "NUM_USERDEF5";
    private string NUM_USERDEF6 = "NUM_USERDEF6";
    private string _APP_USERDEF = "N"; //사용자정의컬럼 적용여부
    string Tp_print = ""; //rdf, drf 사용구분
    private bool   _YN_REBATE = false;

    decimal d_SEQ_PROJECT = decimal.Zero;
    string s_CD_PJT_ITEM = string.Empty;
    string s_NM_PJT_ITEM = string.Empty;
    string s_PJT_ITEM_STND = string.Empty;
    string s_CD_PARTNER_PJT = string.Empty;
    string s_NM_PARTNER_PJT = string.Empty;
    string s_NO_EMP_PJT = string.Empty;
    string s_NM_EMP_PJT = string.Empty;
    string s_END_USER = string.Empty;

    string _지급관리통제설정 = "N";
    string s_vat_fictitious = BASIC.GetMAEXC("발주등록(공장)-의제부가세적용");

    P_PU_OPTION_INFO_SUB _infosub_dlg = new P_PU_OPTION_INFO_SUB("", "", true);



    #endregion

    #region -> 생성자

    public P_PU_PO_REG2() : this("") { }

    public P_PU_PO_REG2(string str발주번호) : this(str발주번호, 0m, "") { }

    public P_PU_PO_REG2(string str발주번호, decimal dNO_LINE, string strSOURCE)
    {
        try
        {

            InitializeComponent();

            MainGrids = new FlexGrid[] { _flexD,_flexDD, _flexH };
            _flexD.DetailGrids = new FlexGrid[] {_flexDD};

            this.str발주번호 = str발주번호;
            this.dNO_LINE = dNO_LINE;
            this.strSOURCE = strSOURCE;

            DataChanged += new EventHandler(Page_DataChanged);

            _header = new FreeBinding();        // 반드시 InitPaint() 이벤트에서 SetBinding() 메소드로 초기화 해줘야 함
            _header.JobModeChanged += new FreeBindingEventHandler(_header_JobModeChanged);
            _header.ControlValueChanged += new FreeBindingEventHandler(_header_ControlValueChanged);


        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
    }

    #endregion

    #endregion

    #region ♣ 초기화 이벤트 / 메소드

    #region -> ControlButtonEnabledDisable

    void ControlButtonEnabledDisable(Control ctr, bool lb_enable)
    {
        if (ctr == null || ctr.Name == "_flexD")
        {

            btn_RE_APP.Enabled = lb_enable;
            btn_RE_PR.Enabled = lb_enable;
            btn_ITEM_EXP.Enabled = lb_enable;
            btn_insert.Enabled = lb_enable;
            btn_delete.Enabled = lb_enable;
            ctx프로젝트.Enabled = lb_enable;
            btn적용.Enabled = lb_enable;
            btn_RE_SO.Enabled = lb_enable;
            btn_so_gir.Enabled = lb_enable;
            bp_CDSL.Enabled = lb_enable;
            btn_SL_apply.Enabled = lb_enable;


            if (MainFrameInterface.ServerKeyCommon == "INITECH" || MainFrameInterface.ServerKeyCommon == "DZSQL" 
               || MainFrameInterface.ServerKeyCommon == "ANJUN" || MainFrameInterface.ServerKeyCommon == "KPCI"
               || MainFrameInterface.ServerKeyCommon == "SQL_") 
            { 
                btn_INST.Enabled = lb_enable; btn_INST.Visible = true; 
            }
            else 
            { btn_INST.Enabled = false; btn_INST.Visible = false; 
            }

            //299번 소스세이프 히스토리
            //btn전자결재.Enabled = lb_enable;
            return;
        }
        else
        {
            btn_RE_APP.Enabled = false;
            btn_RE_PR.Enabled = false;
            btn_RE_SO.Enabled = false;
            btn_ITEM_EXP.Enabled = false;
            //btn_insert.Enabled = false;
            btn_insert.Enabled = true;
            btn_delete.Enabled = true;
            btn_so_gir.Enabled = false;
            btn_INST.Enabled = false;
        }

        //tb_DT_PO.Enabled = true; -->빼버림 20090234
        //tb_NM_PURGRP.Enabled = true;-->빼버림 20090234
        tb_NO_EMP.Enabled = true;
        tb_CD_DEPT.Enabled = true;
        cbo_PAYment.Enabled = true;
        //tb_TAX.Enabled = true;-->빼버림 20131014
        panel23.Enabled = true;
        //cbo_TP_TAX.Enabled = true;
        ctx프로젝트.Enabled = true;
        btn적용.Enabled = true;
        btn_Mail.Enabled = true;
        bp_CDSL.Enabled = true;
        btn_SL_apply.Enabled = true;

        if (ctr is RoundedButton)
        {
            switch (ctr.Name)
            {
                case "btn_RE_APP":
                    btn_RE_APP.Enabled = lb_enable;
                    if (_header.JobMode == JobModeEnum.추가후수정)
                        _header.CurrentRow["TP_PROCESS"] = "3";
                    break;
                case "btn_RE_PR":  //요청적용
                    btn_RE_PR.Enabled = lb_enable;
                    if (_header.JobMode == JobModeEnum.추가후수정)
                        _header.CurrentRow["TP_PROCESS"] = "1";
                    break;

                case "btn_RE_SO":  //수주적용

                    btn_RE_SO.Enabled = lb_enable;
                    if (_header.JobMode == JobModeEnum.추가후수정)
                        _header.CurrentRow["TP_PROCESS"] = "4";
                    break;


                case "btn_RE_PJT":
                    btn_RE_PJT.Enabled = lb_enable;
                    btn_ITEM_EXP.Enabled = lb_enable;
                    btn_insert.Enabled = lb_enable;
                    break;
                case "btn_ITEM_EXP":
                    btn_ITEM_EXP.Enabled = lb_enable;
                    btn_insert.Enabled = lb_enable;
                    if (_header.JobMode == JobModeEnum.추가후수정)
                        _header.CurrentRow["TP_PROCESS"] = "2";
                    break;
                case "btn_insert":
                    btn_ITEM_EXP.Enabled = lb_enable;
                    btn_insert.Enabled = lb_enable;
                    if (_header.JobMode == JobModeEnum.추가후수정)
                        _header.CurrentRow["TP_PROCESS"] = "2";
                    break;
                case "btn_delete":
                    break;
                case "btn_H41_APP":
                    btn_insert.Enabled = lb_enable;
                    break;

                case "btn_so_gir":  //수주적용/의뢰적용
                    btn_so_gir.Enabled = lb_enable;
                    if (_header.JobMode == JobModeEnum.추가후수정)
                        _header.CurrentRow["TP_PROCESS"] = "4";
                    break;

                case "btn_INST":  //손익매입적용
                    btn_INST.Enabled = lb_enable;
                    if (_header.JobMode == JobModeEnum.추가후수정)
                        _header.CurrentRow["TP_PROCESS"] = "4";
                    break;
            }
        }
    }

    #endregion

    #region -> SetHeadControlEnabled

    /// <summary>
    /// UI 상태 활성화 상태 관련 함수
    ///  
    /// </summary>
    /// <param name="isEnabled"></param>
    /// <param name="pi_type">1 : 확정이 되지 않은상태의 활성화 조절
    ///                       2 : 확정이 이루어진 상태의 활성화 조절
    ///                       3 : 구매발주 보조화면에서 복사클릭시 활성화 조절</param>
    ///                       4 : 프로젝트 발주적용 받았을때
    ///                       
    private void SetHeadControlEnabled(bool isEnabled, int pi_type)
    {
        //pi_type  모두 해당
        tb_DT_PO.Enabled = isEnabled;
        tb_NM_PARTNER.Enabled = isEnabled;
        tb_NM_PURGRP.Enabled = isEnabled;
        tb_FG_PO_TR.Enabled = isEnabled;
        cbo_FG_UM.Enabled = isEnabled;


        //tb_DT_LIMIT.Enabled = isEnabled; 2011.09.14 납기일 적용 버튼추가로인해 납기일 enabled 없애달라고요청
        tb_NO_EMP.Enabled = isEnabled;

        //cbo_PAYment.Enabled = isEnabled;
        cbo_FG_TAX.Enabled = isEnabled;
        cbo_TP_TAX.Enabled = isEnabled;
        cbo_CD_PLANT.Enabled = isEnabled;
        cbo_NM_EXCH.Enabled = isEnabled;



        if (pi_type == 2 || pi_type == 3)
        {
            //tb_DT_PO.Enabled = isEnabled;
            //tb_NM_PARTNER.Enabled = isEnabled;

            //tb_NM_PURGRP.Enabled = isEnabled;
            //tb_FG_PO_TR.Enabled = isEnabled;


            //cbo_FG_UM.Enabled = isEnabled;
            //cbo_PAYment.Enabled = isEnabled;
            //cbo_FG_TAX.Enabled = isEnabled;
            //cbo_TP_TAX.Enabled = isEnabled;


            //cbo_CD_PLANT.Enabled = isEnabled;

            //cbo_NM_EXCH.Enabled = isEnabled;

            ctx프로젝트.Enabled = isEnabled;
            rbtn_All.Enabled = isEnabled;
            rbtn_PRI.Enabled = isEnabled;

            btn_insert.Enabled = isEnabled;
            btn_delete.Enabled = isEnabled;
            btn_ITEM_EXP.Enabled = isEnabled;
            btn적용.Enabled = isEnabled;

            bp_CDSL.Enabled = isEnabled;
            btn_SL_apply.Enabled = isEnabled;

            //3만
            if (pi_type == 3) tb_TAX.Enabled = isEnabled;


        }

        if (pi_type == 4)
        {
            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "KORAVL")
            {
                cbo_NM_EXCH.Enabled = true;

                if (D.GetString(cbo_NM_EXCH.SelectedValue) != "000")
                    tb_NM_EXCH.Enabled = true;
            }

            btn적용.Enabled = isEnabled;
        }
        //else if (pi_type == 1)
        //{
        //tb_FG_PO_TR.Enabled = isEnabled;
        //cbo_FG_UM.Enabled = isEnabled;
        //cbo_PAYment.Enabled = isEnabled;
        //cbo_FG_TAX.Enabled = isEnabled;
        //cbo_TP_TAX.Enabled = isEnabled;
        //cbo_CD_PLANT.Enabled = isEnabled;
        //cbo_NM_EXCH.Enabled = isEnabled;
        //tb_NM_PARTNER.Enabled = isEnabled;

        //tb_NM_PURGRP.Enabled = isEnabled; //추가 (빼먹었나?? 20090324)
        //tb_DT_PO.Enabled = isEnabled;     //추가 (빼먹었나?? 20090324)


        //}
        //else if (pi_type == 3)
        //{
        //tb_DT_PO.Enabled = isEnabled;
        //tb_NM_PARTNER.Enabled = isEnabled;

        //tb_NM_PURGRP.Enabled = isEnabled;

        //tb_FG_PO_TR.Enabled = isEnabled;


        //cbo_FG_UM.Enabled = isEnabled;
        //    cbo_PAYment.Enabled = isEnabled;
        //    cbo_FG_TAX.Enabled = isEnabled;
        //    cbo_TP_TAX.Enabled = isEnabled;
        //    ctx프로젝트.Enabled = isEnabled;

        //    cbo_CD_PLANT.Enabled = isEnabled;

        //    cbo_NM_EXCH.Enabled = isEnabled;

        //    rbtn_All.Enabled = isEnabled;
        //    rbtn_PRI.Enabled = isEnabled;

        //    btn_insert.Enabled = isEnabled;
        //    btn_delete.Enabled = isEnabled;
        //    btn_ITEM_EXP.Enabled = isEnabled;

        //    //3만
        //    tb_TAX.Enabled = isEnabled;
        //}



    }

    #endregion

    #region -> _header_JobModeChanged

    void _header_JobModeChanged(object sender, FreeBindingArgs e)
    {
        try
        {
            tb_TAX.Enabled = false;
            if (e.JobMode == JobModeEnum.조회후수정)
            {
                _header.SetControlEnabled(false);
                tb_NO_EMP.Enabled = true;
                tb_DC.Enabled = true;
                tb_DC_RMK2.Enabled = true;
                cbo_TP_TRANSPORT.Enabled = true;
                cbo_COND_PAY.Enabled = true;
                txt_COND_PAY_DLV.Enabled = true;
                cbo_COND_PRICE.Enabled = true;
                txt_ARRIVER.Enabled = true;
                txt_LOADING.Enabled = true;
                bp_ARRIVER.Enabled = true;
                bp_LOADING.Enabled = true;
                txt_DC_RMK_TEXT.Enabled = true;
                            
                cbo_COND_SHIPMENT.Enabled = true;
                cbo_FREIGHT_CHARGE.Enabled = true;
                txt_DC_RMK_TEXT2.Enabled = true;
                cbo_stnd_pay.Enabled = true;
                txt_cond_days.Enabled = true;
                cbo_origin.Enabled = true;

                cbo포장형태.Enabled = true;
                txt인도조건.Enabled = true;
                txt인도기한.Enabled = true;
                txt유효기일.Enabled = true;
                ctx공급자.Enabled = true;
                ctx제조사.Enabled = true;
                txt검사정보.Enabled = true;
                txt필수서류.Enabled = true;
                cur운송비용.Enabled = true;
                txt오더번호.Enabled = true;
                txt_nm_packing.Enabled = true;
                btn_FILE_UPLOAD.Enabled = true;
                cbo_FG_TPPURCV.Enabled = false;
                btnFG_TPPURCV.Enabled = false;

                btnSMS.Enabled = true;

                if (_flexD.HasNormalRow && _flexD[_flexD.Rows.Fixed, "FG_POST"].ToString() == "R")
                {
                    btn배부.Enabled = false;
                    curDe.Enabled = false;
                    curNEGO금액.Enabled = false;
                }
                else
                {
                    curNEGO금액.Enabled = true;
                }

            }
            else
            {
                _header.SetControlEnabled(true);

                tb_CD_DEPT.Enabled = false;
                btn_FILE_UPLOAD.Enabled = false;
                cbo_FG_UM.SelectedIndex = 0;
                _header.CurrentRow["FG_UM"] = cbo_FG_UM.SelectedValue.ToString();
                cbo_FG_TAX.SelectedIndex = 0;
                cbo_TP_TAX.SelectedIndex = 1;
                _header.CurrentRow["TP_UM_TAX"] = cbo_TP_TAX.SelectedValue.ToString();

                cbo_FG_TPPURCV.Enabled = true;
                btnFG_TPPURCV.Enabled = true;
                btnSMS.Enabled = false;

                cbo_NM_EXCH.SelectedIndex = 0;
                // cbo_PAYment.SelectedIndex = 0;    //기초값에서 설정 이전값
                //_header.CurrentRow["FG_PAYMENT"] = cbo_PAYment.SelectedValue.ToString();
                tb_QT_ATP.DecimalValue = 0;
                tb_QT_INV.DecimalValue = 0;
                tb_QT_PO.DecimalValue = 0;
                tb_QT_REQ.DecimalValue = 0;
                if (cbo_CD_PLANT.SelectedValue != null)
                {
                    DataTable dt = ((DataTable)cbo_CD_PLANT.DataSource);
                    if (dt.Rows.Count > 0)
                    {
                        cbo_CD_PLANT.SelectedIndex = 0;
                        _header.CurrentRow["CD_PLANT"] = cbo_CD_PLANT.SelectedValue.ToString();
                    }
                }

                if (D.GetString(Global.MainFrame.LoginInfo.PurchaseGroupCode) == string.Empty)
                {
                    if (D.GetString(_header.CurrentRow["CD_PURGRP"]) == string.Empty)
                        _header.CurrentRow["CD_PURGRP"] = Settings1.Default.CD_PURGRP_SET;
                }
                else
                {
                    _header.CurrentRow["CD_PURGRP"] = Global.MainFrame.LoginInfo.PurchaseGroupCode;
                    //tb_NM_PURGRP.SetCodeValue(Global.MainFrame.LoginInfo.PurchaseGroupCode);
                    //tb_NM_PURGRP.SetCodeName(Global.MainFrame.LoginInfo.PurchaseGroupName);
                }

                if (_header.CurrentRow["CD_TPPO"].ToString() == string.Empty)
                    _header.CurrentRow["CD_TPPO"] = Settings1.Default.CD_TPPO_SET;

         


                if (_header.CurrentRow["FG_PAYMENT"].ToString() == string.Empty)
                {
                    _header.CurrentRow["FG_PAYMENT"] = Settings1.Default.FG_PAYMENT_SET;
                    if (_header.CurrentRow["FG_PAYMENT"].ToString() == string.Empty)
                        _header.CurrentRow["FG_PAYMENT"] = "000";
                    cbo_PAYment.SelectedValue = _header.CurrentRow["FG_PAYMENT"].ToString();
                }

                
                _header.CurrentRow["TP_UM_TAX"] = Settings1.Default.TP_UM_TAX;
                cbo_TP_TAX.SelectedValue = _header.CurrentRow["TP_UM_TAX"];

                기초값설정();
                ControlButtonEnabledDisable(null, true);

                btn배부.Enabled = true;
                curDe.Enabled = true;
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
            MsgEnd(ex);
        }
    }

    #endregion

    #region -> InitLoad

    protected override void InitLoad()
    {
        base.InitLoad();

        _biz = new P_PU_PO_REG2_BIZ();

        MA_EXC_SETTING();      // 통제설정 셋팅메소드
        if (_m_Company_only == "003")  //아카데미과학 전용
        {
            MA_Pjt_Setting();
        }
        InitGridD();
        InitEvent();

        BTN_LOCATION_SETTING();// 버튼 위치 셋팅
        if (sPUSU != "100") tabControlExt1.TabPages.RemoveAt(1);

        //토페스
        if (Global.MainFrame.ServerKeyCommon != "TOPES" && Global.MainFrame.ServerKeyCommon != "DZSQL" && Global.MainFrame.ServerKeyCommon != "SQL_")
        {
            m_tab_poh.TabPages.Remove(tabPage8);

        }
        else
        {
            DataTable dt = _biz.GetTopes(tb_DT_PO.Text);
            _flexH.Binding = dt;

        }


        if (Global.MainFrame.ServerKeyCommon == "MHIK" || Global.MainFrame.ServerKeyCommon == "DZSQL")
        {
            tb_NO_PO.Visible = false;
            tb_NO_PO_MH.Visible = true;
        }
        else
        {
            tb_NO_PO.Visible = true;
            tb_NO_PO_MH.Visible = false;
        }
    }


    #endregion

    #region -> InitPaint

    protected override void InitPaint()
    {
        base.InitPaint();

        InitControl();
        원그리드적용하기();

        DataSet dsTemp = null;

        switch (strSOURCE)
        {
            case "PU_POL":
                dsTemp = _biz.Search("@#$%");                      // 데이타테이블의 컬럼정보만 얻어오기 위해서
                _header.SetBinding(dsTemp.Tables[0], m_tab_poh);
                _header.ClearAndNewRow();                                  // 처음에 행을 하나 만든다. 초기에 추가모드로 하기 위해


                if (sPUSU == "100")
                    _flexDD.Binding = _biz.SearchDetail("sDFS", "SDFSD", 0);

                _flexD.Binding = dsTemp.Tables[1];

                조회(str발주번호, "OK");
                if (_flexD.HasNormalRow)
                    _flexD.Row = _flexD.FindRow(dNO_LINE, _flexD.Rows.Fixed, _flexD.Cols["NO_LINE"].Index, true);
                return;

    
          
        }


        if (!string.IsNullOrEmpty(str발주번호))
        {
            dsTemp = _biz.Search(str발주번호);
            //_header.SetBinding(dsTemp.Tables[0], m_pnlHeader);
            _header.SetBinding(dsTemp.Tables[0], m_tab_poh);
            if (dsTemp.Tables[0].Rows.Count == 0) // 구매요청등록으로 페이지이동시점 전 전표삭제된 경우에 추가모드로..
            {
                _header.ClearAndNewRow();
            }
            _header.SetDataTable(dsTemp.Tables[0]);
            _flexD.Binding = dsTemp.Tables[1];

            


            if (sPUSU == "100")
                _flexDD.Binding = _biz.SearchDetail(D.GetString(cbo_CD_PLANT.SelectedValue), str발주번호, D.GetDecimal(_flexD["NO_LINE"]));

        }
        else
        {
            dsTemp = _biz.Search("@#$%");                        // 데이타테이블의 컬럼정보만 얻어오기 위해서
            // _header.SetBinding(dsTemp.Tables[0], m_pnlHeader);
            _header.SetBinding(dsTemp.Tables[0], m_tab_poh);
            _header.ClearAndNewRow();                                  // 처음에 행을 하나 만든다. 초기에 추가모드로 하기 위해


            _flexD.Binding = dsTemp.Tables[1];
            tb_DT_PO.Focus();

            if (sPUSU == "100")
                _flexDD.Binding = _biz.SearchDetail("sDFS", "SDFSD", 0);

            cbo_NM_EXCH_SelectionChangeCommitted(null, null);

        }

        //if (cbo_FG_TAX.Enabled == true)
        //{
        //string str_fg_tax = string.Empty;
        //    if (cbo_FG_TAX.SelectedValue.ToString() != string.Empty) 
        //         str_fg_tax = cbo_FG_TAX.SelectedValue.ToString();

        //    부가세율(str_fg_tax);
        //}

        Setting_pu_poh_sub();

        if (D.GetString(dsTemp.Tables[0].Rows[0]["NO_PO"]) != string.Empty)
        {
            _header.AcceptChanges();
            _flexD.AcceptChanges();
        }

        if (Global.MainFrame.ServerKeyCommon == "SATREC" || Global.MainFrame.ServerKeyCommon == "JSERP" || Global.MainFrame.ServerKeyCommon == "SQL_")
        {
            txt_DC_RMK_TEXT.Text = D.GetString(Settings1.Default.DC_RMK_TEXT);
            _header.CurrentRow["DC_RMK_TEXT"] = D.GetString(Settings1.Default.DC_RMK_TEXT);

            txt_DC_RMK_TEXT2.Text = D.GetString(Settings1.Default.DC_RMK_TEXT2);
            _header.CurrentRow["DC_RMK_TEXT2"] = D.GetString(Settings1.Default.DC_RMK_TEXT2);
        }

        dtp_DT_PROCESS_IV.Text = Global.MainFrame.GetStringToday;
        dtp_DT_PAY_PRE_IV.Text = Global.MainFrame.GetStringToday;
        dtp_DT_DUE_IV.Text = Global.MainFrame.GetStringToday;


       

    }

    #endregion

    #region -> InitEvent

    void InitEvent()
    {
        ctx프로젝트.QueryBefore += new BpQueryHandler(Control_QueryBefore);
        ctx프로젝트.QueryAfter += new BpQueryHandler(OnBpControl_QueryAfter);
        btn배부.Click += new EventHandler(btn배부_Click);
        btn_INST.Click += new EventHandler(btn_INST_Click);
        btn전자결재L.Click += new EventHandler(btn전자결재L_Click);
        btnFG_TPPURCV.Click += new EventHandler(btnFG_TPPURCV_Click);
        cur_AM_K_IV.Validated +=new EventHandler(cur_AM_K_IV_Validated);
        cur_VAT_TAX_IV.Validated += new EventHandler(cur_VAT_TAX_IV_Validated);
        btn_SL_apply.Click += new EventHandler(btn_SL_apply_Click);
        bp_CDSL.QueryBefore += new BpQueryHandler(OnBpControl_QueryBefore);
        btnSMS.Click += new EventHandler(btnSMS_Click); 
    }

    #endregion

    #region -> InitGridD

    void InitGridD()
    {
        #region -> _flexD
        _flexD.BeginSetting(1, 1, false);

        if (_m_lotsize_use == "100") _flexD.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
        _flexD.SetCol("CD_ITEM", "품목코드", 120, 20, true);
        _flexD.SetCol("NM_ITEM", "품목명", 150, false);
        _flexD.SetCol("STND_ITEM", "규격", 120, false);
        _flexD.SetCol("FG_IQCL", "수입검사레벨", 80, false);
        _flexD.SetCol("CD_UNIT_MM", "발주단위", 70, false);
        _flexD.SetCol("GRP_ITEM", "품목군코드", false);
       
        if (bStandard)
        {
            _flexD.SetCol("NM_ITEMGRP", "품목군", 70, true, typeof(string));
            
        }
        else
        {
            _flexD.SetCol("NM_ITEMGRP", "품목군", 70, false);
        }

        _flexD.SetCol("PI_PARTNER", "주거래처코드", 120, false);
        _flexD.SetCol("PI_LN_PARTNER", "주거래처명", 200, false);

        _flexD.SetCol("DT_LIMIT", "납기일", 70, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
        _flexD.SetCol("DT_PLAN", "납품예정일", 75, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
        _flexD.SetCol("QT_PO_MM", "발주량", 80, 17, true, typeof(decimal), FormatTpType.QUANTITY);
        
        if (bStandard)
        {
            _flexD.SetCol("CLS_L", "대분류코드", false);
            _flexD.SetCol("CLS_M", "중분류코드", false);
            _flexD.SetCol("CLS_S", "소분류코드", false);
            _flexD.SetCol("NM_CLS_L", "대분류", 140, true, typeof(string));
            _flexD.SetCol("NM_CLS_M", "중분류", 140, true, typeof(string));
            _flexD.SetCol("NM_CLS_S", "소분류", 140, true, typeof(string));
            _flexD.SetCol("NUM_STND_ITEM_1", "NUM_STND_ITEM_1", false);
            _flexD.SetCol("NUM_STND_ITEM_2", "NUM_STND_ITEM_2", false);
            _flexD.SetCol("NUM_STND_ITEM_3", "NUM_STND_ITEM_3", false);
            _flexD.SetCol("NUM_STND_ITEM_4", "NUM_STND_ITEM_4", false);
            _flexD.SetCol("NUM_STND_ITEM_5", "NUM_STND_ITEM_5", false);
            _flexD.SetCol("SG_TYPE", "재질구분", false);
            _flexD.SetCol("QT_SG", "재질", false);
            _flexD.SetCol("WEIGHT", "단위중량", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            _flexD.SetCol("TOT_WEIGHT", "총중량", 80, true, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            
            _flexD.SetCol("UM_WEIGHT", "중량단가", 80, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
          

        }

        _flexD.SetCol("UM_EX_PO", "단가", 100, 17, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
        _flexD.SetCol("AM_EX", "금액", 150, 17, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
        _flexD.SetCol("AM", "원화금액", 150, 17, true, typeof(decimal), FormatTpType.MONEY);
        _flexD.SetCol("VAT", "부가세", 150, 17, false, typeof(decimal), FormatTpType.MONEY);
        // 20110322 아이큐브개발팀 김현철 수정
        // 설계서에는 없으나, 총금액은 "합계액"으로 원화금액이 맞지 않는가 싶어 수정.
        //_flexD.SetCol("AM_TOTAL", "총금액", 150, 17, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
        _flexD.SetCol("AM_TOTAL", "총금액", 150, 17, true, typeof(decimal), FormatTpType.MONEY);
        // 20110322 아이큐브개발팀 김현철 수정
        _flexD.SetCol("FG_TAX", "과세구분", 70, true);
        _flexD.SetCol("TP_UM_TAX", "부가세여부", 70, true); 
        //규격형이 아닐 경우만. 
        if (!bStandard)
        {
            _flexD.SetCol("WEIGHT", "단위중량", 80, 17, true, typeof(decimal), FormatTpType.MONEY);
        }
        
        //규격형이 아닐 경우만. 
        if (!bStandard)
        {
            _flexD.SetCol("QT_WEIGHT", "총중량", 80, 17, false, typeof(decimal), FormatTpType.MONEY);
        }
        _flexD.SetCol("CD_CC", "CC코드", 80, true, typeof(string));
        _flexD.SetCol("NM_CC", "CC명", 100, true, typeof(string));
        _flexD.SetCol("CD_SL", "창고코드", 80, 7, true, typeof(string));
        _flexD.SetCol("NM_SL", "창고명", 120, false, typeof(string));
        _flexD.SetCol("CD_PJT", "프로젝트", 120, true, typeof(string));
        _flexD.SetCol("NM_PJT", "프로젝트명", 120, false, typeof(string));
        _flexD.SetCol("SEQ_PROJECT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 항번" : "프로젝트항번", 120, false, typeof(decimal));
        _flexD.SetCol("NM_SYSDEF", "발주상태", 80, false, typeof(string));
        _flexD.SetCol("UNIT_IM", "재고단위", 80, false, typeof(string));
        _flexD.SetCol("QT_PO", "재고수량", 120, (m_sEnv == "Y") ? true : false, typeof(decimal), FormatTpType.QUANTITY);
        _flexD.SetCol("NO_PR", "요청번호", 120, false, typeof(string));
        _flexD.SetCol("NO_APP", "품의번호", 120, false, typeof(string));
        _flexD.SetCol("DC1", "발주라인비고1", 200, 200, true, typeof(string));
        _flexD.SetCol("DC2", "발주라인비고2", 200, 200, true, typeof(string));
        // 2010/12/30 조형우 비고3 컬럼 추가
        _flexD.SetCol("DC3", "발주라인비고3", 200, 30, true, typeof(string));
        _flexD.SetCol("DC4", "발주라인비고4", 200, 500, true, typeof(string));
        _flexD.SetCol("NO_LINE", "항번", 40, 17, false, typeof(decimal), FormatTpType.QUANTITY);
        _flexD.SetCol("NM_CUST_DLV", "수취인", 80, false, typeof(string));
        _flexD.SetCol("ADDR1_DLV", "주소", 80, false, typeof(string));
        _flexD.SetCol("NO_TEL_D2_DLV", "핸드폰번호", 80, false, typeof(string));
        _flexD.SetCol("QT_INVC", "재고량", 120, 17, false, typeof(decimal), FormatTpType.QUANTITY);
        //_flexD.SetCol("QT_ATPC", "가용재고량", 120, 17, false, typeof(decimal), FormatTpType.QUANTITY);
        _flexD.SetCol("GI_PARTNER", "납품처코드", 120, true);
        _flexD.SetCol("LN_PARTNER", "납품처명", 200, false);
        _flexD.SetCol("NM_CLS_ITEM", "계정", 120);
        _flexD.SetCol("FG_PACKING", "포장형태", 140, true, typeof(string));
        _flexD.SetCol("CD_REASON", "구매요청사유", 140, false, typeof(string));
        _flexD.SetCol("FG_SU", "외주유형", 140, false, typeof(string));
        _flexD.SetCol("GRP_MFG", "제품군코드", 80, false);
        _flexD.SetCol("NM_GRPMFG", "제품군명", 100, false);
        _flexD.SetCol("EN_ITEM", "품목명(영)", 100, false);

        if (_m_Company_only == "003")
        {
            _flexD.SetCol("NUM_USERDEF4", NUM_USERDEF4,150, false);
            _flexD.SetCol("NUM_USERDEF5", NUM_USERDEF5, 150, false);
            _flexD.SetCol("NUM_USERDEF6", NUM_USERDEF6, 150, false);
        }
        /*구매예산통제-추가 20091112*/
        if (_구매예산CHK설정 == "Y")
        {
            _flexD.SetCol("CD_BUDGET", "예산단위코드", 150, false);
            _flexD.SetCol("NM_BUDGET", "예산단위명", 150, false);
            _flexD.SetCol("CD_BGACCT", "예산계정코드", 150, false);
            _flexD.SetCol("NM_BGACCT", "예산계정명", 150, false);
        }
        /////////////////////////////////////////////////////

        // 아사히카세이 전용 //

        if (_m_Company_only == "001")
        {
            _flexD.SetCol("QT_WIDTH", "폭(mm)", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexD.SetCol("QT_LENGTH", "길이(m)", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexD.SetCol("QT_AREA", "면적(m²)", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexD.SetCol("TOTAL_AREA", "총면적(m²)", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexD.SetCol("UM_EX_AR", "외화단가(m²)", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);

            _flexD[0, "UM_EX_PO"] = "외화단가(EA)";
            _flexD[0, "QT_PO_MM"] = "발주량(EA)";
        }

        if (Config.MA_ENV.프로젝트사용)
        {
            _flexD.SetCol("CD_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 코드" : "프로젝트 품목코드", 140, false, typeof(string));
            _flexD.SetCol("NM_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 명" : "프로젝트 품목명", 140, false, typeof(string));
            _flexD.SetCol("NO_PJT_DESIGN", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 도면번호" : "프로젝트 도면번호", 140, false, typeof(string));
            _flexD.SetCol("PJT_ITEM_STND", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 규격" : "프로젝트 품목규격", 140, false, typeof(string));
        }
        if (Config.MA_ENV.PJT형여부 == "Y")
        {
            _flexD.SetCol("NO_WBS", "WBS번호", 140, false, typeof(string));
            _flexD.SetCol("NO_CBS", "CBS번호", 140, false, typeof(string));
            _flexD.SetCol("CD_ACTIVITY", "ACTIVITY 코드", 140, false, typeof(string));
            _flexD.SetCol("NM_ACTIVITY", "ACTIVITY", 140, false, typeof(string));
            _flexD.SetCol("CD_COST", "원가코드", 140, false, typeof(string));
            _flexD.SetCol("NM_COST", "원가명", 140, false, typeof(string));
            _flexD.SetCol("CD_ITEM_MO", "상위품목코드", 140, false, typeof(string));
            _flexD.SetCol("NM_ITEM_MO", "상위품목명", 140, false, typeof(string));
        }

        _flexD.SetCol("NO_MODEL", "모델코드", 140, false, typeof(string));

        _flexD.SetCol("STND_DETAIL_ITEM", "상세규격", 140, false, typeof(string));

        bool bEdit = true;

        if (Global.MainFrame.ServerKeyCommon == "ANJUN")
            bEdit = false;

        _flexD.SetCol("CD_USERDEF1", "사용자정의1", 100, bEdit, typeof(string));
        _flexD.SetCol("CD_USERDEF2", "사용자정의2", 100, true, typeof(string));
        //_flexD.SetCol("NM_USERDEF1", "사용자정의3", 100, true, typeof(string));
        //_flexD.SetCol("NM_USERDEF2", "사용자정의4", 100, true, typeof(string));

        if (Global.MainFrame.ServerKeyCommon.ToUpper() != "CHOSUNHOTELBA")
        {
            ////사용자정의컬럼 추가시 코드관리에 추가하여 사용여부가 사용일 경우 CD_FLAG1에 캡션명 저장한 후 업체별로 사용 하면됨... 
            DataTable dtTEXT = MA.GetCode("PU_Z000007", false);
            if (dtTEXT != null && dtTEXT.Rows.Count != 0)
            {
                foreach (DataRow row in dtTEXT.Rows)
                {
                    string ColCaption = D.GetString(row["CD_FLAG1"]) == "" ? D.GetString(row["NAME"]) : D.GetString(row["CD_FLAG1"]);
                    string ColName = D.GetString(row["NAME"]);
                    if (ColName.Contains("DATE"))
                        _flexD.SetCol(ColName, ColCaption, 70, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    else
                        _flexD.SetCol(ColName, ColCaption, 100, true, typeof(string));

                    if (ColName.Contains("CDSL_USERDEF1"))
                    {
                        _flexD.SetCodeHelpCol("CDSL_USERDEF1", HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CDSL_USERDEF1", "NMSL_USERDEF1" }, new string[] { "CD_SL", "NM_SL" });
                        _flexD.SetCol("NMSL_USERDEF1", D.GetString(row["CD_FLAG2"]), 100, false);
                    }
                }
            }
        }
        else
        {
            _flexD.SetCol("NM_USERDEF1", "사용자정의3", 100, true, typeof(string));
            _flexD.SetCol("NM_USERDEF2", "사용자정의4", 100, true, typeof(string));
        }

        _flexD.SetCol("DT_EXDATE", "공장출고일", 100, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
        _flexD.SetCol("AM_EX_TRANS", "운송비(외화)", 100, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
        _flexD.SetCol("AM_TRANS", "운송비(원화)", 100, true, typeof(decimal), FormatTpType.MONEY);
        
        _flexD.SetCol("CD_USERDEF14", "사용자정의14", 0, false, typeof(string));
        _flexD.SetCol("RATE_VAT", "부가세율", 0, false, typeof(decimal));
        _flexD.SetCol("NM_MAKER", "MAKER", 100, false, typeof(string));
        _flexD.SetCol("FG_PURCHASE", "매입형태", 100, true, typeof(string));
        _flexD.SetCol("MAT_ITEM", "재질", 100, true, typeof(string));
        _flexD.SetCol("NO_DESIGN", "도면번호", 100, true, typeof(string));

        _flexD.SetCol("UM", "원화단가", false);
        _flexD.SetCol("UM_P", "원화단가", false);

        _flexD.SetCol("NO_PRLINE", "요청번호항번", 80, false);

        if (Global.MainFrame.ServerKey == "ICDERPU")
        {
            _flexD.SetCol("UM_PRE", "할인전단가", 120, 17, false, typeof(decimal), FormatTpType.UNIT_COST);
            _flexD.SetCol("AM_PRE", "견적금액", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
        }

        if (_YN_REBATE)
        {
            _flexD.SetCol("UM_REBATE", "리베이트단가", 100, 17, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flexD.SetCol("AM_REBATE_EX", "리베이트금액", 100, 17, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flexD.SetCol("AM_REBATE", "리베이트원화금액", 100, 17, true, typeof(decimal), FormatTpType.MONEY);
        }

        DataTable dtNUM = MA.GetCode("PU_C000093", false);
        if (dtNUM != null && dtNUM.Rows.Count != 0)
        {
            foreach (DataRow row in dtNUM.Rows)
            {
                string ColName = D.GetString(row["CD_FLAG1"]) == "" ? D.GetString(row["NAME"]) : D.GetString(row["CD_FLAG1"]);
                _flexD.SetCol(D.GetString(row["NAME"]), ColName, 80, 17, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            }
        }

        if (MainFrameInterface.ServerKeyCommon == "UNIPOINT")
        {
            _flexD.SetCol("CD_PARTNER_PJT", "프로젝트 거래처코드", 100, false, typeof(string));
            _flexD.SetCol("LN_PARTNER_PJT", "프로젝트 거래처", 100, false, typeof(string));
            _flexD.SetCol("NO_EMP_PJT", "프로젝트 담당자코드", 100, false, typeof(string));
            _flexD.SetCol("NM_KOR_PJT", "프로젝트 담당자", 100, false, typeof(string));
            _flexD.SetCol("END_USER", "프로젝트 END USER", 100, false, typeof(string));
        } 

        _flexD.SetDummyColumn(new string[] { "S",  "QT_INVC", "FG_POCON", "NM_SYSDEF", "MEMO_CD", "CHECK_PEN" });

        if (Global.MainFrame.ServerKeyCommon.ToUpper() != "WJIS")
        {
            _flexD.SetCodeHelpCol("CD_ITEM", HelpID.P_MA_PITEM_SUB1, ShowHelpEnum.Always, new string[] { "CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT_IM", "CD_SL", "NM_SL", "CD_UNIT_MM", "PI_PARTNER", "PI_LN_PARTNER" },
                                   new string[] { "CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT_IM", "CD_SL", "NM_SL", "UNIT_PO", "PARTNER", "LN_PARTNER" }, ResultMode.SlowMode);
        }
        else
        {
            _flexD.SetCodeHelpCol("CD_ITEM", HelpID.P_MA_PITEM_SUB1, ShowHelpEnum.Always, new string[] { "CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT_IM", "CD_SL", "NM_SL", "CD_UNIT_MM", "PI_PARTNER", "PI_LN_PARTNER", "DC1","DC2" },
                                  new string[] { "CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT_IM", "CD_SL", "NM_SL", "UNIT_PO", "PARTNER", "LN_PARTNER", "CD_ITEM", "NM_ITEM" }, ResultMode.SlowMode);
        }
        _flexD.SetCodeHelpCol("CD_SL", HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CD_SL", "NM_SL" }, new string[] { "CD_SL", "NM_SL" });
        _flexD.SetCodeHelpCol("CD_CC", HelpID.P_MA_CC_SUB, ShowHelpEnum.Always, new string[] { "CD_CC", "NM_CC" }, new string[] { "CD_CC", "NM_CC" });
        //_flexD.SetCodeHelpCol("PI_PARTNER", HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, new string[] { "PI_PARTNER", "PI_LN_PARTNER" }, new string[] { "CD_PARTNER", "LN_PARTNER" });
        //_flexD.SetCodeHelpCol("CD_PJT", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new string[] { "CD_PJT", "NM_PJT" }, new string[] { "NO_PROJECT", "NM_PROJECT" });
        if (Config.MA_ENV.YN_UNIT == "Y")
        {
            _flexD.SetCodeHelpCol("CD_PJT", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new String[] { "CD_PJT", "NM_PJT", "SEQ_PROJECT", "CD_PJT_ITEM", "NM_PJT_ITEM", "PJT_ITEM_STND" }, new String[] { "NO_PROJECT", "NM_PROJECT", "SEQ_PROJECT", "CD_PJT_ITEM", "NM_PJT_ITEM", "PJT_ITEM_STND" }, new String[] { "NO_PROJECT", "NM_PROJECT", "SEQ_PROJECT", "CD_PJT_ITEM", "PJT_NM_ITEM", "PJT_STND_ITEM" }, ResultMode.FastMode);
            _flexD.SetCodeHelpCol("CD_PJT_ITEM", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new String[] { "CD_PJT", "NM_PJT", "SEQ_PROJECT", "CD_PJT_ITEM", "NM_PJT_ITEM", "PJT_ITEM_STND" }, new String[] { "NO_PROJECT", "NM_PROJECT", "SEQ_PROJECT", "CD_PJT_ITEM", "NM_PJT_ITEM", "PJT_ITEM_STND" }, new String[] { "NO_PROJECT", "NM_PROJECT", "SEQ_PROJECT", "CD_PJT_ITEM", "PJT_NM_ITEM", "PJT_STND_ITEM" }, ResultMode.FastMode);
        }
        else
        {
            _flexD.SetCodeHelpCol("CD_PJT", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new string[] { "CD_PJT", "NM_PJT" }, new string[] { "NO_PROJECT", "NM_PROJECT" });
        }


        _flexD.SetCodeHelpCol("GI_PARTNER", Duzon.Common.Forms.Help.HelpID.P_SA_TPPTR_SUB, ShowHelpEnum.Always, new string[] { "GI_PARTNER", "LN_PARTNER" }, new string[] { "CD_TPPTR", "NM_TPPTR" }); //납품처

        _flexD.SetCodeHelpCol("CD_BUDGET", HelpID.P_FI_BGCODE_SUB, ShowHelpEnum.Always, new string[] { "CD_BUDGET", "NM_BUDGET" }, new string[] { "CD_BUDGET", "NM_BUDGET" });
        _flexD.SetCodeHelpCol("CD_BGACCT", HelpID.P_FI_BGACCT_SUB, ShowHelpEnum.Always, new string[] { "CD_BGACCT", "NM_BGACCT" }, new string[] { "CD_BGACCT", "NM_BGACCT" });

        if (bStandard)
        {
            _flexD.SetCodeHelpCol("NM_CLS_L", HelpID.P_MA_CODE_SUB, ShowHelpEnum.Always, new string[] { "CLS_L", "NM_CLS_L" }, new string[] { "CD_SYSDEF", "NM_SYSDEF" }, ResultMode.SlowMode);
            _flexD.SetCodeHelpCol("NM_CLS_M", HelpID.P_MA_CODE_SUB, ShowHelpEnum.Always, new string[] { "CLS_M", "NM_CLS_M" }, new string[] { "CD_SYSDEF", "NM_SYSDEF" }, ResultMode.SlowMode);
            _flexD.SetCodeHelpCol("NM_CLS_S", HelpID.P_MA_CODE_SUB, ShowHelpEnum.Always, new string[] { "CLS_S", "NM_CLS_S" }, new string[] { "CD_SYSDEF", "NM_SYSDEF" }, ResultMode.SlowMode);
            _flexD.SetCodeHelpCol("NM_ITEMGRP", HelpID.P_MA_ITEMGP_SUB, ShowHelpEnum.Always, new string[] { "GRP_ITEM", "NM_ITEMGRP" }, new string[] { "CD_ITEMGRP", "NM_ITEMGRP" });

        }

        _flexD.SetExceptEditCol("NM_ITEM", "STND_ITEM", "UNIT_IM", "CD_UNIT_MM", "NM_SL", "NM_PJT");
        _flexD.SetExceptEditCol("NM_CC", "NM_BUDGET", "NM_BGACCT");

        _flexD.VerifyAutoDelete = new string[] { "CD_ITEM" };

        if (Global.MainFrame.ServerKeyCommon == "DKONT")
        {
            _flexD.VerifyNotNull = new string[] { "CD_ITEM", "DT_LIMIT","FG_PACKING" };
        }
        else if (App.SystemEnv.PROJECT사용 == true)
        {
            if (Config.MA_ENV.YN_UNIT == "Y")
                _flexD.VerifyNotNull = new string[] { "CD_ITEM", "DT_LIMIT", "CD_PJT","SEQ_PROJECT" };
            else
                _flexD.VerifyNotNull = new string[] { "CD_ITEM", "DT_LIMIT", "CD_PJT" };
        }
        else
            _flexD.VerifyNotNull = new string[] { "CD_ITEM", "DT_LIMIT" };

        _flexD.VerifyCompare(_flexD.Cols["QT_PO_MM"], 0, OperatorEnum.GreaterOrEqual);
        _flexD.VerifyCompare(_flexD.Cols["UM_EX_PO"], 0, OperatorEnum.GreaterOrEqual);
        _flexD.VerifyCompare(_flexD.Cols["AM_EX"], 0, OperatorEnum.GreaterOrEqual);
        _flexD.VerifyCompare(_flexD.Cols["AM"], 0, OperatorEnum.GreaterOrEqual);
        _flexD.VerifyCompare(_flexD.Cols["VAT"], 0, OperatorEnum.GreaterOrEqual);

        _flexD.Cols["PI_PARTNER"].Visible = false;
        _flexD.Cols["PI_LN_PARTNER"].Visible = false;

        _flexD.SettingVersion = "1.1.6";
        _flexD.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

        _flexD.DisableNumberColumnSort();    // Decimal sort 불가

       
        
        
        _flexD.Cols["NO_LINE"].Visible = false;
        if (_YN_REBATE)
        {
            if (Config.MA_ENV.YN_UNIT == "Y")
                _flexD.SetExceptSumCol("UM_EX_PO", "UM_REBATE", "SEQ_PROJECT");
            else
                _flexD.SetExceptSumCol("UM_EX_PO", "UM_REBATE");
        }
        else
        {
            if (Config.MA_ENV.YN_UNIT == "Y")
                _flexD.SetExceptSumCol("UM_EX_PO", "SEQ_PROJECT");
            else
                _flexD.SetExceptSumCol("UM_EX_PO");
        }
        

        _flexD.EnterKeyAddRow = true;
        _flexD.AddRow += new System.EventHandler(추가_Click);
        _flexD.StartEdit += new RowColEventHandler(Grid_StartEdit);
        _flexD.AfterRowChange += new RangeEventHandler(Grid_AfterRowChange);
        _flexD.ValidateEdit += new ValidateEditEventHandler(Grid_ValidateEdit);
        _flexD.BeforeCodeHelp += new BeforeCodeHelpEventHandler(Grid_BeforeCodeHelp);
        _flexD.AfterCodeHelp += new AfterCodeHelpEventHandler(Grid_AfterCodeHelp);
        _flexD.AfterEdit += new RowColEventHandler(_flexD_AfterEdit);
        _flexD.VerifyCompare(_flexD.Cols["QT_PO_MM"], 0, OperatorEnum.Greater);
        _flexD.VerifyCompare(_flexD.Cols["QT_PO"], 0, OperatorEnum.Greater);
        _flexD.VerifyCompare(_flexD.Cols["QT_PO_MM"], _flexD.Cols["QT_REQ_MM"], OperatorEnum.GreaterOrEqual);

        _flexD.DoubleClick += new EventHandler(_flex_DoubleClick);
        //_flexD.HelpClick +=new EventHandler(_flexD_HelpClick);

        // 메모기능 
        _flexD.CellNoteInfo.EnabledCellNote = true;// 메모기능활성화 
        _flexD.CellNoteInfo.CategoryID = this.Name; // page 명입력 // 같은page명을입력했을경우여러화면에서볼수있습니다. 
        _flexD.CellNoteInfo.DisplayColumnForDefaultNote = "NM_ITEM";  // 마킹& 메모가보여질컬럼설정 


        _flexD.CheckPenInfo.EnabledCheckPen = true;// 체크펜기능활성화  
        _flexD.CellContentChanged += new CellContentEventHandler(_flexD_CellContentChanged);   // 메모& 체크추가, 삭제(수정제외) 되었을경우이벤트가발생.

        _flexD.AddMyMenu = true;
        _flexD.AddMenuSeperator();
        ToolStripMenuItem parent = _flexD.AddPopup(DD("엑셀관리"));
        _flexD.AddMenuItem(parent, "파일생성", EXCEL_Popup);

        #endregion


        #region -> _flexDD
        _flexDD.BeginSetting(1, 1, false);

        _flexDD.SetCol("CHK", "S", 30, true, CheckTypeEnum.Y_N);
        _flexDD.SetCol("NO_LINE", "자재항번", 80, false);
        _flexDD.SetCol("CD_MATL", "자재코드", 100, 20, true);
        _flexDD.SetCol("NM_ITEM", "자재명", 140, false);
        _flexDD.SetCol("STND_ITEM", "규격", 140, false);
        _flexDD.SetCol("STND_DETAIL_ITEM", "세부규격", 140, false);
        _flexDD.SetCol("UNIT_MO", "단위", 40, false);
        _flexDD.SetCol("QT_NEED_UNIT", "실소요량", 90, true, typeof(decimal), FormatTpType.QUANTITY);
        _flexDD.SetCol("QT_NEED", "(사급)요청수량", 110, true, typeof(decimal), FormatTpType.QUANTITY);
        _flexDD.SetCol("QT_REQ", "출고의뢰수량", 110, false, typeof(decimal), FormatTpType.QUANTITY);
        _flexDD.SetCol("ECN_DT", "설계변경일", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
        _flexDD.SetCol("NO_HST", "차수", 40, 5, false, typeof(decimal));
        _flexDD.SetCol("NO_ECN", "ECN번호", 100, 20, false);
        _flexDD.SetCol("QT_LOSS", "(사급)요청가능수량", 130, false, typeof(decimal), FormatTpType.QUANTITY);
        _flexDD.SetCol("QT_PO", "발주수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
   
        //_flexD.SetCol("NO_PO", "NO_PO", false);
        //_flexD.SetCol("NO_POLINE", "NO_POLINE", false);
        //_flexD.SetCol("NO_LINE", "NO_LINE", false);

        _flexDD.VerifyPrimaryKey = new string[] {/* "NO_PO", */"NO_POLINE", "NO_LINE" };
        //_flexD.VerifyCompare(_flexD.Cols["QT_NEED"], _flexD.Cols["QT_LOSS"], OperatorEnum.LessOrEqual);

        //통제환경을 따라가기 위해서 주석처리(통제환경에서 소수점 4자리까지 포맷을 주기로 함)
        //_flexD.Cols["QT_NEED_UNIT"].Format = "###,###,###,###.####";
        //_flexD.Cols["QT_NEED"].Format = "###,###,###,###.####";

        _flexDD.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

        _flexDD.Cols["QT_LOSS"].Visible = false;
        _flexDD.Cols["QT_PO"].Visible = false;
        _flexDD.Cols["QT_NEED_UNIT"].Visible = false;
        _flexDD.Cols["ECN_DT"].Visible = false;
        _flexDD.Cols["NO_HST"].Visible = false;
        _flexDD.Cols["NO_ECN"].Visible = false;

        //20110329 최인성 김성호 김헌섭 출고수량이 있는 경우 EDIT 불가하도록 함.
        _flexDD.StartEdit += new RowColEventHandler(Grid_StartEdit);
        _flexDD.BeforeCodeHelp += new BeforeCodeHelpEventHandler(Grid_BeforeCodeHelp);
        _flexDD.AfterCodeHelp += new AfterCodeHelpEventHandler(Grid_AfterCodeHelp);
        _flexDD.ValidateEdit += new ValidateEditEventHandler(Grid_ValidateEdit);


        _flexDD.SetCodeHelpCol("CD_MATL", HelpID.P_MA_PITEM_SUB1, ShowHelpEnum.Always, new string[] { "CD_MATL", "NM_ITEM", "STND_ITEM", "STND_DETAIL_ITEM", "UNIT_MO" }, new string[] { "CD_ITEM", "NM_ITEM", "STND_ITEM", "STND_DETAIL_ITEM", "UNIT_MO" },
                                                                                      new string[] { "CD_MATL", "NM_ITEM", "STND_ITEM", "STND_DETAIL_ITEM", "UNIT_MO", "QT_NEED", "QT_LOSS", "ECN_DT", "NO_HST", "NO_ECN" }, ResultMode.FastMode);

        _flexDD.SetDummyColumn("S");

        _flexDD.VerifyAutoDelete = new string[] { "CD_MATL" };
        _flexDD.VerifyNotNull = new string[] { "CD_MATL", "QT_NEED" };
        #endregion
        
    }

   
    #endregion


    #region -> InitGridH

    void InitGridH()
    {

        _flexH.BeginSetting(1, 1, false);

        //_flexH.SetCol("CHK", "S", 30, true, CheckTypeEnum.Y_N);
        _flexH.SetCol("FG_IV", "조건구분", 100, true);
        _flexH.SetCol("DT_IV_PLAN", "마감예상일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
        _flexH.SetCol("RT_IV", "조건비율", 90, true, typeof(decimal), FormatTpType.EXCHANGE_RATE);
        _flexH.SetCol("AM", "조건금액", 110, true, typeof(decimal), FormatTpType.QUANTITY);
        _flexH.SetCol("VAT", "부가세", 110, false, typeof(decimal), FormatTpType.QUANTITY);
        _flexH.SetCol("AM_HAP", "조건합계금액", 110, false, typeof(decimal), FormatTpType.QUANTITY);
        _flexH.SetCol("DT_BAN_PLAN", "지금예상일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
        _flexH.SetCol("RT_BAN", "지급비율", 90, true, typeof(decimal), FormatTpType.EXCHANGE_RATE);
        _flexH.SetCol("AM_BAN", "지금예정액", 110, true, typeof(decimal), FormatTpType.QUANTITY);
        _flexH.SetCol("AM_BANK", "지급예정원화금액", 110, false, typeof(decimal), FormatTpType.QUANTITY);

        _flexH.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        _flexH.ValidateEdit += new ValidateEditEventHandler(_flexH_ValidateEdit);

    }


    #endregion

    #region -> InitControl

    private void InitControl()
    {
        DataSet ds = GetComboData("N;PU_C000001", "N;MA_CODEDTL_005;MA_B000046", "N;PU_C000005", "N;MA_B000005", "N;PU_C000009",
                                  "N;PU_C000014", "NC;MA_PLANT", "S;TR_IM00008", "S;TR_IM00004", "S;TR_IM00002",
                                  "S;TR_IM00003", "S;TR_IM00028", "N;PU_C000067", "S;MA_B000020","S;TR_IM00011",
                                  "N;PU_C000075", "N;PU_C000076", "N;PU_C000077", "S;FI_J000002","N;MA_AISPOSTH;200", "S;PU_C000044",
                                  "N;MA_B000141", "N;PU_Z000010", "N;MA_B000004");


        ds.Tables[1].PrimaryKey = new DataColumn[] { ds.Tables[1].Columns["CODE"] };

        // 단가유형						
        cbo_FG_UM.DataSource = ds.Tables[0];
        cbo_FG_UM.DisplayMember = "NAME";
        cbo_FG_UM.ValueMember = "CODE";

        // 과세구분			
        cbo_FG_TAX.DataSource = ds.Tables[1];
        cbo_FG_TAX.DisplayMember = "NAME";
        cbo_FG_TAX.ValueMember = "CODE";
        _flexD.SetDataMap("FG_TAX", ds.Tables[1], "CODE", "NAME");

        // 부가세여부		
        cbo_TP_TAX.DataSource = ds.Tables[2];
        cbo_TP_TAX.DisplayMember = "NAME";
        cbo_TP_TAX.ValueMember = "CODE";
        _flexD.SetDataMap("TP_UM_TAX", ds.Tables[2], "CODE", "NAME"); 

        // 환정보		
        cbo_NM_EXCH.DataSource = ds.Tables[3];
        cbo_NM_EXCH.DisplayMember = "NAME";
        cbo_NM_EXCH.ValueMember = "CODE";

        // 지급조건
        cbo_PAYment.DataSource = ds.Tables[5];
        cbo_PAYment.DisplayMember = "NAME";
        cbo_PAYment.ValueMember = "CODE";


        // 공장
        cbo_CD_PLANT.DataSource = ds.Tables[6];
        cbo_CD_PLANT.DisplayMember = "NAME";
        cbo_CD_PLANT.ValueMember = "CODE";

        cbo_CD_PLANT.SelectedValue = Global.MainFrame.LoginInfo.CdPlant;


        //운송방법
        cbo_TP_TRANSPORT.DataSource = ds.Tables[7];
        cbo_TP_TRANSPORT.DisplayMember = "NAME";
        cbo_TP_TRANSPORT.ValueMember = "CODE";

        //지불조건
        cbo_COND_PAY.DataSource = ds.Tables[8];
        cbo_COND_PAY.DisplayMember = "NAME";
        cbo_COND_PAY.ValueMember = "CODE";

        //가격조건
        cbo_COND_PRICE.DataSource = ds.Tables[9];
        cbo_COND_PRICE.DisplayMember = "NAME";
        cbo_COND_PRICE.ValueMember = "CODE";


        //선적조건
        cbo_COND_SHIPMENT.DataSource = ds.Tables[10];
        cbo_COND_SHIPMENT.DisplayMember = "NAME";
        cbo_COND_SHIPMENT.ValueMember = "CODE";


        //운임구분
        cbo_FREIGHT_CHARGE.DataSource = ds.Tables[11];
        cbo_FREIGHT_CHARGE.DisplayMember = "NAME";
        cbo_FREIGHT_CHARGE.ValueMember = "CODE";

        //지급기준
        cbo_stnd_pay.DataSource = ds.Tables[12];
        cbo_stnd_pay.DisplayMember = "NAME";
        cbo_stnd_pay.ValueMember = "CODE";

        //포장형태
        cbo포장형태.DataSource = ds.Tables[14];
        cbo포장형태.DisplayMember = "NAME";
        cbo포장형태.ValueMember = "CODE";

        //전표유형
        cbo_CD_DOCU_IV.DataSource = ds.Tables[18];
        cbo_CD_DOCU_IV.DisplayMember = "NAME";
        cbo_CD_DOCU_IV.ValueMember = "CODE";


        //매입형태
        cbo_FG_TPPURCV.DataSource = ds.Tables[19];
        cbo_FG_TPPURCV.DisplayMember = "NAME";
        cbo_FG_TPPURCV.ValueMember = "CODE";

        //오더상태
        DataTable dt = ds.Tables[4];
        DataRow[] ldrs_arg = dt.Select("CODE ='P'"); //'P'의 명칭Planning  

        if (ldrs_arg != null && ldrs_arg.Length > 0)
        {
            _ComfirmState = ldrs_arg[0]["NAME"].ToString();
        }

        tb_DT_PO.Mask = GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
        tb_DT_PO.ToDayDate = Global.MainFrame.GetDateTimeToday();
        tb_DT_PO.Text = Global.MainFrame.GetStringToday;



        tb_DT_LIMIT.Mask = GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
        tb_DT_LIMIT.ToDayDate = Global.MainFrame.GetDateTimeToday();
        tb_DT_LIMIT.Text = Global.MainFrame.GetStringToday;

        SetHeadControlEnabled(true, 2);


        //예산확인여부
        DataTable dt2 = new DataTable();
        dt2.Columns.Add("CODE", typeof(string));
        dt2.Columns.Add("NAME", typeof(string));
        DataRow row2 = null;
        row2 = dt2.NewRow(); row2["CODE"] = "Y"; row2["NAME"] = "함"; dt2.Rows.Add(row2);
        row2 = dt2.NewRow(); row2["CODE"] = "N"; row2["NAME"] = "안함"; dt2.Rows.Add(row2);

        cbo_YN_BUDGET.DataSource = dt2;
        cbo_YN_BUDGET.DisplayMember = "NAME";
        cbo_YN_BUDGET.ValueMember = "CODE";


        //원산지
        cbo_origin.DataSource = ds.Tables[13];
        cbo_origin.DisplayMember = "NAME";
        cbo_origin.ValueMember = "CODE";

        _flexD.SetDataMap("CD_REASON", ds.Tables[15].Copy(), "CODE", "NAME");
        _flexD.SetDataMap("FG_PACKING", ds.Tables[16].Copy(), "CODE", "NAME");
        _flexD.SetDataMap("FG_SU", ds.Tables[17].Copy(), "CODE", "NAME");
        _flexD.SetDataMap("FG_PURCHASE", ds.Tables[19].Copy(), "CODE", "NAME");
        _flexD.SetDataMap("CD_UNIT_MM", ds.Tables[23].Copy(), "CODE", "NAME");
        _flexD.SetDataMap("UNIT_IM", ds.Tables[23].Copy(), "CODE", "NAME");


        if (Global.MainFrame.ServerKeyCommon == "TOPES" || Global.MainFrame.ServerKeyCommon == "DZSQL" || Global.MainFrame.ServerKeyCommon == "SQL_")
        {
            _flexH.SetDataMap("FG_IV", ds.Tables[22].Copy(), "CODE", "NAME");
        }

        //지급구분(거래처)
        if (_지급관리통제설정 == "N")
        {

            cbo_FG_PAYBILL_IV.DataSource = ds.Tables[20];
            cbo_FG_PAYBILL_IV.DisplayMember = "NAME";
            cbo_FG_PAYBILL_IV.ValueMember = "CODE";
        }
        else
        {
            DataTable dt_pay = ComFunc.GetPayList();
            if (dt_pay != null)
            {
                //매입형태
                cbo_FG_PAYBILL_IV.DataSource = dt_pay;
                cbo_FG_PAYBILL_IV.DisplayMember = "NAME";
                cbo_FG_PAYBILL_IV.ValueMember = "CODE";
            }
        }
        cbo_FG_PAYBILL_IV.SelectedValue = "";

        if (Global.MainFrame.ServerKeyCommon == "GALAXIA")
        {
            btnSMS.Visible = true;
            btnSMS.Enabled = false;
        }



        //규격형
        if (bStandard)
        {
            DataTable dt규격_NUM = ds.Tables[21].Copy();

            DataRow[] DataNum1 = dt규격_NUM.Select("CODE = '001'");
            if (DataNum1.Length > 0)
            {
                _flexD.Cols["NUM_STND_ITEM_1"].Caption = D.GetString(DataNum1[0]["NAME"]);
                _flexD.Cols["NUM_STND_ITEM_1"].Visible = true;
                _flexD.Cols["NUM_STND_ITEM_1"].DataType = typeof(Decimal);
                _flexD.Cols["NUM_STND_ITEM_1"].Format = "#,###,##0.####";
                _flexD.Cols["NUM_STND_ITEM_1"].AllowEditing = true;

               

            }

            DataRow[] DataNum2 = dt규격_NUM.Select("CODE = '002'");
            if (DataNum2.Length > 0)
            {
                _flexD.Cols["NUM_STND_ITEM_2"].Caption = D.GetString(DataNum2[0]["NAME"]);
                _flexD.Cols["NUM_STND_ITEM_2"].Visible = true;
                _flexD.Cols["NUM_STND_ITEM_2"].DataType = typeof(Decimal);
                _flexD.Cols["NUM_STND_ITEM_2"].Format = "#,###,##0.####";
                _flexD.Cols["NUM_STND_ITEM_2"].AllowEditing = true;

            }
            DataRow[] DataNum3 = dt규격_NUM.Select("CODE = '003'");
            if (DataNum3.Length > 0)
            {
                _flexD.Cols["NUM_STND_ITEM_3"].Caption = D.GetString(DataNum3[0]["NAME"]);
                _flexD.Cols["NUM_STND_ITEM_3"].Visible = true;
                _flexD.Cols["NUM_STND_ITEM_3"].DataType = typeof(Decimal);
                _flexD.Cols["NUM_STND_ITEM_3"].Format = "#,###,##0.####";
                _flexD.Cols["NUM_STND_ITEM_3"].AllowEditing = true;

            }
            DataRow[] DataNum4 = dt규격_NUM.Select("CODE = '004'");
            if (DataNum4.Length > 0)
            {
                _flexD.Cols["NUM_STND_ITEM_4"].Caption = D.GetString(DataNum4[0]["NAME"]);
                _flexD.Cols["NUM_STND_ITEM_4"].Visible = true;
                _flexD.Cols["NUM_STND_ITEM_4"].DataType = typeof(Decimal);
                _flexD.Cols["NUM_STND_ITEM_4"].Format = "#,###,##0.####";
                _flexD.Cols["NUM_STND_ITEM_4"].AllowEditing = true;
            }
            DataRow[] DataNum5 = dt규격_NUM.Select("CODE = '005'");
            if (DataNum5.Length > 0)
            {
                _flexD.Cols["NUM_STND_ITEM_5"].Caption = D.GetString(DataNum5[0]["NAME"]);
                _flexD.Cols["NUM_STND_ITEM_5"].Visible = true;
                _flexD.Cols["NUM_STND_ITEM_5"].DataType = typeof(Decimal);
                _flexD.Cols["NUM_STND_ITEM_5"].Format = "#,###,##0.####";
                _flexD.Cols["NUM_STND_ITEM_5"].AllowEditing = true;
            }
        }
    }

    #endregion

    #region -> 전용코드 설정 및 설정에따른 컨트롤 셋팅
    private void MA_EXC_SETTING()
    {

        //다이렉트로 seelct해오기 전용코드 
        //DataTable dt_exc2 = Global.MainFrame.FillDataTable(" SELECT CD_EXC FROM MA_EXC " +
        //                                                  "  WHERE CD_COMPANY = '" + this.LoginInfo.CompanyCode + "'" +
        //                                                  "    AND EXC_TITLE = '구매예산CHK' ");

        //if (dt_exc2.Rows.Count > 0)
        //{   // N:기본 N:안함, Y:함 (null이거나 ''은 N으로 치환) 
        //    if (dt_exc2.Rows[0]["CD_EXC"] != System.DBNull.Value && dt_exc2.Rows[0]["CD_EXC"].ToString().Trim() != String.Empty)
        //    {
        //        _구매예산CHK설정 = dt_exc2.Rows[0]["CD_EXC"].ToString().Trim();
        //    }
        //}

        _구매예산CHK설정 = Duzon.ERPU.MF.ComFunc.전용코드("구매예산CHK");

        //if (_구매예산CHK설정 != "Y")
        //{ 일단 안씀 
        btn_예산chk.Visible = false;
        btn_예산chk내역.Visible = false;

        lb_YN_BUDGET.Visible = false;
        cbo_YN_BUDGET.Visible = false;
        lb_BUDGET_PASS.Visible = false;
        tb_BUDGET_PASS.Visible = false;
        //}



        m_sEnv = _biz.EnvSearch();
        m_sEnv_CC = Duzon.ERPU.MF.ComFunc.전용코드("발주등록-C/C설정");
        m_sEnv_CC_Line = Duzon.ERPU.MF.ComFunc.전용코드("발주라인-C/C설정수정유무");
        m_sEnv_App_Am = Duzon.ERPU.MF.ComFunc.전용코드("발주등록(공장)-품의적용시 금액설정");
        m_sEnv_App_Sort = Duzon.ERPU.MF.ComFunc.전용코드("발주등록(공장)-품의항번별정렬");


        if (App.SystemEnv.PROJECT사용 == true)
            btn_PRJ_SUB.Visible = true;
        else
            btn_PRJ_SUB.Visible = false;

        m_Elec_app = BASIC.GetMAEXC("전자결재-사용구분");
        #region -> 전자결재버튼 visible true/false
        if (m_Elec_app != "000" && BASIC.GetMAEXC("전자결재메뉴별사용여부-발주등록") == "100")
        {
            if (m_Elec_app == "026")
                btn전자결재L.Visible = true;
            else
                btn전자결재.Visible = true;
        }


        #endregion

        //영우만 엑셀업로드버튼과 추가버튼을 보여주지 않는다.
        //전용코드 조회 : 영우
        DataTable dt_exc = _biz.GetPartnerCodeSearch();

        if (dt_exc.Rows.Count > 0)
        {
            // 000:기본 100:평화 200:영우 (null이거나 ''은 000으로 치환) 
            if (dt_exc.Rows[0]["CD_EXC"] != System.DBNull.Value && dt_exc.Rows[0]["CD_EXC"].ToString().Trim() != String.Empty)
            {
                _전용설정 = dt_exc.Rows[0]["CD_EXC"].ToString().Trim();
            }
        }

        if (_전용설정 == "200") //영우일경우
        {
            엑셀업로드.Visible = false;
            btn_insert.Visible = false;
        }

        //if (MainFrameInterface.ServerKey != "GNTU" && MainFrameInterface.ServerKey != "DEM" && MainFrameInterface.ServerKey != "PKIC" && MainFrameInterface.ServerKey != "DZSQL" && MainFrameInterface.ServerKey != "DZORA" && MainFrameInterface.ServerKey != "108")
        //    btn전자결재.Visible = false;//대림,백광 테스트 서버만 보여주기 위해  상단에 전자결재 구분값이 존재함

        //
        if (Duzon.ERPU.MF.ComFunc.전용코드("H41적용여부") == "Y")
            btn_H41_APP.Visible = true;
        else
            btn_H41_APP.Visible = false;
        // 상단 버튼 visible된것 차례로 나열되게...   

        #region -> 수주적용 사용유무
        if (Duzon.ERPU.MF.ComFunc.전용코드("구매-수주적용설정") != "200")
            btn_RE_SO.Visible = false;
        #endregion

        #region -> 시스템통제설정적용 메일전송설정
        if (Duzon.ERPU.MF.ComFunc.전용코드("발주등록(공장)-메일전송설정") == "Y")
            btn_Mail.Visible = true;
        #endregion

        #region -> 시스템통제설정적용 구매요청_품의_거래처적용
        _m_partner_use = BASIC.GetMAEXC("구매요청_품의_거래처적용");
        #endregion

        #region -> 메뉴통제설정적용 구매품의_발주형태적용
        _m_tppo_use = BASIC.GetMAEXC_Menu("P_PU_APP_REG", "PU_A00000005");
        _m_tppo_change = BASIC.GetMAEXC_Menu("P_PU_PO_REG2", "PU_A00000006");
        #endregion

        #region -> 시스템통제설정적용 발주등록-헤더정보 자동적용
        _m_partner_change = BASIC.GetMAEXC("발주등록-헤더정보 자동적용");
        #endregion

        #region -> 시스템통제설정적용 발주등록 LOTSIZE 설정
        _m_lotsize_use = BASIC.GetMAEXC("발주등록_LOTSIZE적용");
        if (_m_lotsize_use == "100") btn_LOT_Accept.Visible = true;
        #endregion

        #region -> 과세구분설정
        m_sEnv_FG_TAX = Duzon.ERPU.MF.ComFunc.전용코드("과세구분설정");
        #endregion

        #region -> 메뉴통제설정

        BASICPU.CacheDataClear(BASICPU.CacheEnums.MA_MENU_CONTROL); //초기화
        //DataTable dt_MANU_CTR = BASICPU.MA_MENU_CONTROL_VALUES(Global.MainFrame.CurrentPageID, Global.MainFrame.CurrentModule);

        #endregion

        #region -> 시스템통제설정적용 우선순위단가적용
        if (BASIC.GetMAEXC("구매-우선순위단가적용_사용유무") != "000")
        {
            btn_um_pro.Visible = true;
        }
        #endregion

        // 발주서출력시 출력 옵션 사용자 정의 사용 설정
        m_sEnv_Prt_Option = Duzon.ERPU.MF.ComFunc.전용코드("발주등록-출력옵션사용");

        // 납품일자 통제 설정
        _m_dt = Duzon.ERPU.MF.ComFunc.전용코드("발주등록-납품일자 통제");

        #region -> Nego금액사용 포틱스전용

      
        if (m_sEnv_Nego == "100")
        {
            //pnlNego금액.Visible = true;
            lblNego금액.Visible = true;
            curNEGO금액.Visible = true;
            btn배부.Visible = true;

            curNEGO금액.Size = new System.Drawing.Size(131, 21);


        }

        if (Global.MainFrame.ServerKey.Contains("DEMAC") || Global.MainFrame.ServerKey.Contains("SQL_108"))
        {
            lblNego금액.Visible = true;
            curNEGO금액.Visible = true;
            btn배부.Visible = true;
            curDe.Visible = true;
            lblNego금액.Text = "할인율/자릿수";
        }
        #endregion

        if (Global.MainFrame.ServerKeyCommon.ToUpper() == "KORAVL")
            btn환정보적용.Visible = true;
        else
            tb_NM_EXCH.Size = new System.Drawing.Size(100,21);

        #region -> 업체별프로세스 사용유무 

        _m_Company_only = BASIC.GetMAEXC("업체별프로세스");
      
        #endregion

        //PJT형 사용여부
        if (Config.MA_ENV.PJT형여부 == "Y")
            btn_wbscbs.Visible = true;

        //평화발레오(브이피에이치) 수주/의뢰적용사용유무
        if (BASIC.GetMAEXC("발주등록(공장)-수주/의뢰적용") == "100")
            btn_so_gir.Visible = true;

        //부가정보사용여부
        if (BASIC.GetMAEXC("발주등록(공장)-부가정보적용") != "000")
        {
            btn_subinfo.Visible = true;
        }


        if (BASIC.GetMAEXC("요청,품의,발주-사용자정의컬럼적용") == "100")
            _APP_USERDEF = "Y";

        if (BASIC.GetMAEXC("리베이트사용여부") == "100")
            _YN_REBATE = true;

        _지급관리통제설정 = BASIC.GetMAEXC("거래처정보등록-회계지급관리사용여부");


        if (Global.MainFrame.ServerKeyCommon == "ANJUN") //안전공업
        {
            btn_INST.Text = "자재수급계획";
        }
        else if (Global.MainFrame.ServerKeyCommon == "KPCI" || Global.MainFrame.ServerKeyCommon == "SQL_") //안전공업
        {
            btn_INST.Text = "계약적용";
        }
        else //이니텍
        {
            btn_INST.Text = "손익매입적용";
        }

        //규격형 사용 유무
        if (BASIC.GetMAEXC("공장품목등록-규격형") == "100")
        {
            bStandard = true;

        }

        //토페스
        if (Global.MainFrame.ServerKeyCommon == "TOPES" || Global.MainFrame.ServerKeyCommon == "DZSQL" || Global.MainFrame.ServerKeyCommon == "SQL_")
        {
            btnHadd.Click += new EventHandler(btnH_Click);
            btnHDe.Click += new EventHandler(btnH_Click);

            InitGridH();
        }


       
    }


    #endregion

    #region -> 버튼 위치 셋팅
    private void BTN_LOCATION_SETTING()
    {

        //RoundedButton[] r_button = { btn전자결재, btn_PRJ_SUB, btn_예산chk내역, btn_예산chk, btn_H41_APP, btn_RE_SO, btn_Mail };
        //SetButtonDisp(r_button, btn_RE_APP.Location.X);

        RoundedButton[] r_button_1 = { btn_insert, btn_ITEM_EXP, 엑셀업로드, btn_RE_PJT, btn_LOT_Accept, btn_um_pro, btn단가정보, btnBOM };
        SetButtonDisp(r_button_1, btn_delete.Location.X);
    }
    #endregion

    #endregion

    #region ♣ 메인버튼 이벤트

    #region ♣ 필수입력 체크
    /// <summary>
    /// 필수입력 항목에 Null 체크해주는 함수
    /// 아래의 NUllCheck() 메소드가 리턴값을 Bool 형태로 반환합니다.
    /// </summary>
    /// <returns></returns>
    private bool FieldCheck()
    {
        Hashtable hList = new Hashtable();

        hList.Add(cbo_CD_PLANT, lb_NM_PLANT);


        return ComFunc.NullCheck(hList);
    }
    #endregion

    #region -> 조회

    public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
    {
        try
        {
            if (!base.BeforeSearch()) return;

            if (!FieldCheck()) return;

            string CD_PLANT = cbo_CD_PLANT.SelectedValue.ToString();
            string CD_PARTNER = tb_NM_PARTNER.CodeValue;
            string NM_PARTNER = tb_NM_PARTNER.CodeName;
            string CD_PURGRP = tb_NM_PURGRP.CodeValue;
            string NM_PURGRP = tb_NM_PURGRP.CodeName;

            pur.P_PU_PO_SUB2 m_dlg = new pur.P_PU_PO_SUB2(CD_PLANT, CD_PARTNER, NM_PARTNER, CD_PURGRP, NM_PURGRP);

            if (m_dlg.ShowDialog(this) == DialogResult.OK)
            {
                조회(m_dlg.m_NO_PO, m_dlg.m_btnType);
            }

            if (tb_NO_PO.Text.Trim() != "" && (MainFrameInterface.ServerKey == "PKIC" || MainFrameInterface.ServerKey == "DZSQL"))//전자결재 한번올려진것은 버튼 비활성화 : 백광
            { check_GW(tb_NO_PO.Text); }
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
    }

    private void 조회(string NO_PO, string BTN_TYPE)
    {
        DataSet ds = _biz.Search(NO_PO);
        if (ds.Tables[0].Rows.Count < 1)
        {
            ShowMessage(공통메세지._이가존재하지않습니다, NO_PO);
            OnToolBarAddButtonClicked(null, null);
        }
        else
        {
            str복사구분 = BTN_TYPE;

            호출여부 = false;
            _flexD.Binding = null;
            _flexDD.Binding = null;

            if (str복사구분 == "OK")
            {
                _header.SetDataTable(ds.Tables[0]);

                if (!요청적용여부)
                    ControlButtonEnabledDisable(btn_RE_PR, true);
                else if (!품의적용여부)
                    ControlButtonEnabledDisable(btn_RE_APP, true);
                else if (!수주적용여부)
                    ControlButtonEnabledDisable(btn_RE_SO, true);
                else
                    ControlButtonEnabledDisable(btn_insert, true);

                _flexD.Binding = ds.Tables[1];

                if (_m_Company_only == "001")
                {
                    int row = _flexD.RowSel;
                    DataTable _ds = ds.Tables[1].Clone();
                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {
                        _ds.ImportRow(dr);
                        AsahiKasei_Only_Item(row, _ds);
                        _ds.Clear();
                        row++;
                    }
                }

                _flexD.AcceptChanges();
            }

            else if (str복사구분 == "COPY")
            {
                _flexD.Redraw = false;

                _header.SetDataTable(ds.Tables[0]);
                _header.CurrentRow["NO_PO"] = "";
                tb_NO_PO.Text = "";
                SetHeadControlEnabled(true, 3);
                ControlButtonEnabledDisable(null, true);

                _header.JobMode = JobModeEnum.추가후수정;
                _flexD.IsDataChanged = true;
                ToolBarDeleteButtonEnabled = false;

                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    ds.Tables[1].Rows[i]["NO_APP"] = "";
                    ds.Tables[1].Rows[i]["NO_APPLINE"] = 0;
                    ds.Tables[1].Rows[i]["NO_PR"] = "";
                    ds.Tables[1].Rows[i]["NO_PRLINE"] = 0;
                    ds.Tables[1].Rows[i]["NO_RCV"] = "";
                    ds.Tables[1].Rows[i]["NO_RCVLINE"] = 0;
                    ds.Tables[1].Rows[i]["QT_REQ_MM"] = 0;
                    ds.Tables[1].Rows[i]["QT_REQ"] = 0;
                    ds.Tables[1].Rows[i]["QT_RCV"] = 0;
                    ds.Tables[1].Rows[i]["QT_TR"] = 0;
                    ds.Tables[1].Rows[i]["QT_TR_MM"] = 0;
                    ds.Tables[1].Rows[i]["FG_POST"] = "O";
                    ds.Tables[1].Rows[i]["FG_POCON"] = "001";
                }

                DataView dv = new DataView(ds.Tables[1], "", "NO_LINE ASC", DataViewRowState.CurrentRows);

                _flexD.Binding = dv;

                if (sPUSU == "100")
                {
                    for (int i = 1; i < _flexD.Rows.Count ; i++)
                    {
                        _flexD.Select(i, 1);
                        _flexDD.IsDataChanged = true;
                    }
                }

                _flexD.Redraw = true;
                SUMFunction();
            }

            Setting_pu_poh_sub();

            if (m_tab_poh.TabPages.Contains(tabPage8) && str복사구분 != "COPY")
            {
                _flexH.Binding = _biz.GetTopes(tb_NO_PO.Text);
            }


            if (str복사구분 == "COPY")
                tb_NO_PO.Enabled = true;
            else
                tb_NO_PO.Enabled = false;

            foreach (DataRow row in _flexD.DataTable.Rows)
            {
                //fg_post 오더상태..  이 컬럼은 확정여부인 fg_pocon과 동기화 되서 R(확정),C(close), O(발주) 
                if (row["FG_POST"].ToString().Trim() != "O" || !차수여부 ) //|| str복사구분 == "COPY")  // 확정이 됐다는 의미로 본다.
                {
                    ctx프로젝트.Enabled = false; btn적용.Enabled = false;
                    btn_RE_APP.Enabled = false; btn_RE_PR.Enabled = false;
                    btn_RE_SO.Enabled = false;
                    m_pnlHeader_Enabled();
                    //m_pnlHeader.Enabled = false; -- 비고 수정가능 때문에 컨트롤별로 Enabled설정 2011.04.08
                    oneGrid2.Enabled = false;
                    //m_pnlHeader3.Enabled = true;
                    txt_DC_RMK_TEXT.Enabled = true;
                    txt_DC_RMK_TEXT2.Enabled = true;
                    // _flexD.Enabled = false;
                    btn_so_gir.Enabled = false;

                    bp_CDSL.Enabled = false; btn_SL_apply.Enabled = false;

                    curNEGO금액.Enabled = false;
                    btn배부.Enabled = false;
                    curDe.Enabled = false;

                    //if (m_tab_poh.TabPages.Contains(tabPage8) )
                    //{
                    //    tableLayoutPanel4.Enabled = false;
                    //}
                }
                else
                {
                    ctx프로젝트.Enabled = true; btn적용.Enabled = true;
                    btn_RE_APP.Enabled = true; btn_RE_PR.Enabled = true;
                    btn_RE_SO.Enabled = true;
                    oneGrid1.Enabled = true;
                    oneGrid2.Enabled = true;
                    // _flexD.Enabled = true;
                    btn_so_gir.Enabled = true;
                    bp_CDSL.Enabled = true; btn_SL_apply.Enabled = true;

                    curNEGO금액.Enabled = true;
                    btn배부.Enabled = true;
                    curDe.Enabled = true;

                    //if (m_tab_poh.TabPages.Contains(tabPage8))
                    //{
                    //    tableLayoutPanel4.Enabled = true;
                    //}
                }
            }
        }
    }

    #endregion

    #region -> BeforeAdd

    protected override bool BeforeAdd()
    {
        if (!base.BeforeAdd())
            return false;

        if (!MsgAndSave(PageActionMode.Search))
            return false;

        return true;
    }

    #endregion

    #region -> 추가

    public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
    {
        try
        {
            if (!BeforeAdd()) return;

            _flexD.DataTable.Rows.Clear();
            _flexD.AcceptChanges();
            _header.ClearAndNewRow();
            if (sPUSU == "100")
            {
                _flexDD.DataTable.Rows.Clear();
                _flexDD.AcceptChanges();
            }

            ControlButtonEnabledDisable(null, true);

            SetHeadControlEnabled(true, 2);
            cbo_FG_TAX_SelectionChangeCommitted(null, null);
            ctx프로젝트.CodeValue = string.Empty;
            ctx프로젝트.CodeName = string.Empty;
            bp_CDSL.CodeValue = string.Empty;
            bp_CDSL.CodeName = string.Empty;

            oneGrid1.Enabled = true;
            oneGrid2.Enabled = true;

            if (D.GetString(Global.MainFrame.LoginInfo.PurchaseGroupCode) == string.Empty)
                _header.CurrentRow["CD_PURGRP"] = Settings1.Default.CD_PURGRP_SET;
            else
                _header.CurrentRow["CD_PURGRP"] = Global.MainFrame.LoginInfo.PurchaseGroupCode;


            if (_header.CurrentRow["CD_TPPO"].ToString() == string.Empty)
                _header.CurrentRow["CD_TPPO"] = Settings1.Default.CD_TPPO_SET;

            if (_header.CurrentRow["FG_PAYMENT"].ToString() == string.Empty)
            {
                _header.CurrentRow["FG_PAYMENT"] = Settings1.Default.FG_PAYMENT_SET;
                if (_header.CurrentRow["FG_PAYMENT"].ToString() == string.Empty)
                    _header.CurrentRow["FG_PAYMENT"] = "000";
                cbo_PAYment.SelectedValue = _header.CurrentRow["FG_PAYMENT"].ToString();
            }

            _header.CurrentRow["TP_UM_TAX"] = Settings1.Default.TP_UM_TAX;
            cbo_TP_TAX.SelectedValue = _header.CurrentRow["TP_UM_TAX"];
            
            기초값설정();

            //20110215 -- 최인성 추가
            //기존에 재상신 방지로 인하여 SetHeadControlEnabled 함수에서 모든 컨트롤을 관리하였으나
            //추가 버튼 클릭시 무조건 전자결재버튼 사용시 활성화로 함.
            btn전자결재.Enabled = true;

            if (Global.MainFrame.ServerKeyCommon == "SATREC" || Global.MainFrame.ServerKeyCommon == "JSERP" || Global.MainFrame.ServerKeyCommon == "SQL_")// 쎄트렉아이 임시로 해놓고 공통으로 갈지 협의해봐야함
            {
                txt_DC_RMK_TEXT.Text = D.GetString(Settings1.Default.DC_RMK_TEXT);
                txt_DC_RMK_TEXT2.Text = D.GetString(Settings1.Default.DC_RMK_TEXT2);
            }

            if (m_tab_poh.TabPages.Contains(tabPage7))
            {
                dtp_DT_DUE_IV.Text = Global.MainFrame.GetStringToday;
                dtp_DT_PAY_PRE_IV.Text = Global.MainFrame.GetStringToday;
                dtp_DT_PROCESS_IV.Text = Global.MainFrame.GetStringToday;
                cbo_FG_PAYBILL_IV.SelectedValue = "";
                cbo_CD_DOCU_IV.SelectedValue = "";
            }


            if (m_tab_poh.TabPages.Contains(tabPage8))
            {
                _flexH.DataTable.Rows.Clear();
                _flexH.AcceptChanges();
            }

            
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
    }

    #endregion

    #region -> 삭제
    public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
    {
        try
        {
            if (!BeforeDelete()) return;

            _biz.Delete(tb_NO_PO.Text);

            ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
            OnToolBarAddButtonClicked(sender, e);
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
    }

    #region -> BeforeDelete

    protected override bool BeforeDelete()
    {
        if (!base.BeforeDelete())
            return false;

        if (D.GetString(_header.CurrentRow["YN_ORDER"]) == "Y" && _flexD.DataTable.Select("FG_POST = 'R'", "", DataViewRowState.CurrentRows).Length > 0)
        {
            if (ShowMessage(DD("발주상태가 확정입니다. 삭제하시겠습니까?"), "QY2") != DialogResult.Yes)
                return false;
        }
        else
        {

            DialogResult result = ShowMessage(공통메세지.자료를삭제하시겠습니까, PageName);
            if (result != DialogResult.Yes) return false;
        }

        return true;
    }

    #endregion

    #endregion

    #region -> 저장

    public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
    {
        try
        {
            if (!BeforeSave()) return;

            ToolBarSaveButtonEnabled = false;
            if (MsgAndSave(PageActionMode.Save))
                ShowMessage(PageResultMode.SaveGood);
            else
                ToolBarSaveButtonEnabled = true;

            Settings1.Default.CD_PURGRP_SET = _header.CurrentRow["CD_PURGRP"].ToString();
            Settings1.Default.CD_TPPO_SET = _header.CurrentRow["CD_TPPO"].ToString();
            Settings1.Default.FG_PAYMENT_SET = _header.CurrentRow["FG_PAYMENT"].ToString();
            Settings1.Default.CD_EXCH = _header.CurrentRow["CD_EXCH"].ToString();
            Settings1.Default.RT_EXCH = D.GetDecimal(_header.CurrentRow["RT_EXCH"]);
            Settings1.Default.DC_RMK_TEXT = D.GetString(txt_DC_RMK_TEXT.Text);
            Settings1.Default.DC_RMK_TEXT2 = D.GetString(txt_DC_RMK_TEXT2.Text);
            Settings1.Default.TP_UM_TAX = _header.CurrentRow["TP_UM_TAX"].ToString();
            //꼭 저장을 해줘야 한다.
            Settings1.Default.Save();
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
            ToolBarSaveButtonEnabled = true;

            //발주번호 직접키인 했을경우, PK오류 발생후 발주번호 수정 가능 D20130816145
            if (추가모드여부)
            {
                tb_NO_PO.Enabled = true;
                btn_insert.Enabled = true;
                btn_delete.Enabled = true;
            }
        }
    }


    #region -> 저장관련메소드

    #region -> BeforeSave

    protected override bool BeforeSave()
    {
        if (m_tab_poh.TabPages.Contains(tabPage7) && ShowMessage("발주유형이 매입자동프로세스 입니다.\n매입정보TAP에 데이터를 입력하셨으면'확인'버튼을 눌러주세요.", "QK2") != DialogResult.OK)
            return false; 

        if (_m_dt == "100")
        {
            foreach (DataRow dr in _flexD.DataTable.Select())
            {
                if (D.GetDecimal(_header.CurrentRow["DT_PO"]) > D.GetDecimal(dr["DT_LIMIT"]) || D.GetDecimal(_header.CurrentRow["DT_PO"]) > D.GetDecimal(dr["DT_PLAN"]))
                {
                    ShowMessage("발주일자보다 납기일/납품예정일이 빠릅니다.");
                    return false;
                }

                if (D.GetDecimal(dr["DT_LIMIT"]) < D.GetDecimal(dr["DT_PLAN"]))
                {
                    ShowMessage("납기일보다 납품예정일이 느립니다");
                    return false;
                }
            }
        }

        if (!HeaderCheck(0)) return false;

        if (!Verify())     // 그리드 체크
            return false;

        if (!자동입고여부체크)
            return false;

        if (!_flexD.HasNormalRow)
        {
            OnToolBarDeleteButtonClicked(null, null);
            return false;
        }


        if (_header.CurrentRow["TP_GR"] == "103" || _header.CurrentRow["TP_GR"] == "104")
        {

            foreach (DataRow dr in _flexD.DataTable.Select())
            {
                if (D.GetString(dr["CD_SL"]) == string.Empty)
                {
                    ShowMessage("발주유형이 입고 후까지 처리되는 경우 창고데이터는 필수입니다.");
                    return false;
                }
               
            }
        }


        if (bStandard)
        {
            //신진SM일경우 대중소분류 및 품목군 CHECK
            if (Global.MainFrame.ServerKey == "SINJINSM" || Global.MainFrame.ServerKey == "SQL_108" || Global.MainFrame.ServerKey == "DZSQL")
            {
                int idx = 0;
                if ((idx = checkCLS()) != 0)
                {
                    _flexD.Select(idx, "CLS_L");
                    return false;
                }

                if ((idx = checkITEMGRP()) != 0)
                {
                    _flexD.Select(idx, "NM_ITEMGRP");

                    return false;
                }
            }
        }


        if (Global.MainFrame.ServerKeyCommon == "MHIK" )
        {
            if (!_biz.GetMHIK(tb_NO_PO_MH.Text))
            {
                ShowMessage("해당 발주번호는 이미 등록된 건입니다.");
                return false;
            }
        }

        return true;
    }

    #endregion

    #region -> 헤더데이타 필수등록검사구문(HeaderCheck)

    /// <summary>
    /// 헤더데이타 필수등록검사구문
    /// </summary>
    /// <returns></returns>

    private bool HeaderCheck(int p_chk)
    {
        if (p_chk != 1)
        {
            if (MainFrameInterface.ServerKeyCommon != "INITECH")
            {
                if (tb_NM_PARTNER.CodeValue == "")
                {
                    tb_NM_PARTNER.Focus();
                    ShowMessage(공통메세지._은는필수입력항목입니다, lb_NM_PARTNER.Text);
                    return false;
                }
            }
        }

        if (tb_DT_PO.Text == "")
        {
            tb_DT_PO.Focus();
            ShowMessage(공통메세지._은는필수입력항목입니다, lb_DT_PO.Text);
            return false;
        }

        if (!tb_DT_PO.IsValidated)
        {
            ShowMessage(공통메세지.입력형식이올바르지않습니다);
            tb_DT_PO.Focus();
            return false;
        }

        if (cbo_CD_PLANT.SelectedValue == null || cbo_CD_PLANT.SelectedValue.ToString() == "")
        {
            cbo_CD_PLANT.Focus();
            ShowMessage(공통메세지._은는필수입력항목입니다, lb_NM_PLANT.Text);
            return false;
        }

        if (tb_NM_PURGRP.CodeValue == "")
        {
            tb_NM_PURGRP.Focus();
            ShowMessage(공통메세지._은는필수입력항목입니다, lb_NM_PURGRP.Text);
            return false;
        }

        if (tb_NO_EMP.CodeValue == "")
        {
            tb_NO_EMP.Focus();
            ShowMessage(공통메세지._은는필수입력항목입니다, ib_NO_EMP.Text);
            return false;
        }
        if (_m_tppo_change != "001")
        {
            if (tb_FG_PO_TR.CodeValue == "")
            {
                tb_FG_PO_TR.Focus();
                ShowMessage(공통메세지._은는필수입력항목입니다, lb_FG_PO_TR.Text);
                return false;
            }
        }

        if (cbo_NM_EXCH.SelectedValue == null || cbo_NM_EXCH.SelectedValue.ToString() == "")
        {
            ShowMessage(공통메세지._은는필수입력항목입니다, lb_NM_EXCH.Text);
            cbo_NM_EXCH.Focus();
            return false;
        }

        if (tb_NM_EXCH.DecimalValue == 0)
        {
            ShowMessage(공통메세지._은는필수입력항목입니다, lb_NM_EXCH.Text);
            tb_NM_EXCH.Focus();
            return false;
        }

        if (cbo_TP_TAX.SelectedValue == null || cbo_TP_TAX.SelectedValue.ToString() == "")
        {
            ShowMessage(공통메세지._은는필수입력항목입니다, lb_TP_TAX.Text);
            cbo_TP_TAX.Focus();
            return false;
        }

        //if (App.SystemEnv.PROJECT사용 == true)
        //{
        //    if (bp_Project.Text == string.Empty || bp_Project.Text == "")
        //    {
        //        ShowMessage(공통메세지._은는필수입력항목입니다, lb_CD_PJT.Text);
        //        bp_Project.Focus();
        //        return false;
        //    }
        //}

        #region -> 현재일자보다 이전 인것은 통제한다
        DataTable dt_ma_menu = BASICPU.MA_MENU_CONTROL_VALUES(Global.MainFrame.CurrentPageID, Global.MainFrame.CurrentModule);
        string menu_dt_ctr = dt_ma_menu != null && dt_ma_menu.Rows.Count > 0 ? D.GetString(dt_ma_menu.Rows[0]["YN_DT_CONTROL"]) : "N";
        if (menu_dt_ctr == "Y")
        {
            if (D.GetDecimal(tb_DT_PO.Text) < D.GetDecimal(Global.MainFrame.GetStringToday))  //현재일자보다 이전 인것은 통제한다
            {

                ShowMessage(공통메세지._보다커야합니다, "현재일자");
                tb_DT_PO.Text = Global.MainFrame.GetStringToday;
                tb_DT_PO.Focus();
                return false;
            }
        }
        #endregion

        return true;
    }

    #endregion

    #region -> SaveData

    protected override bool SaveData()
    {

        bool lb_RcvSave = false;
        bool lb_RevSave = false;

        if (!base.SaveData())
            return false;

        SUMFunction();

        //if (호출여부 == true)
        //{

        string No_PO = tb_NO_PO.Text;

        //아사히 키세히 전용
        StringBuilder check_fg_tppurchase = new StringBuilder();
        string msg = "품목코드\t 매입형태코드\t";
        check_fg_tppurchase.AppendLine(msg);
        msg = "-".PadRight(75, '-');
        check_fg_tppurchase.AppendLine(msg);
        bool flag = true;

        if (추가모드여부)  // heade가 추가가 된상태 check
        {

            if (No_PO != "" && No_PO.Substring(0, 1) == " ")
            {
                ShowMessage("발주번호 첫자리에 공백은 올 수 없습니다");
                return false;
            }

            No_PO = No_PO == "" ? (string)GetSeq(LoginInfo.CompanyCode, "PU", "03", tb_DT_PO.Text.Substring(0, 6).Trim()) : tb_NO_PO.Text;
            _header.CurrentRow["NO_PO"] = No_PO;
            if ((Global.MainFrame.ServerKeyCommon == "MHIK" || Global.MainFrame.ServerKeyCommon == "DZSQL") && string.IsNullOrEmpty(D.GetString(tb_NO_PO_MH.Text))) //수기입력 때문에...
            {
                tb_NO_PO_MH.Text = No_PO;
            }

            if (_flexD.HasNormalRow)
            {
                int no_line = 0;
                foreach (DataRow dr in _flexD.DataTable.Rows)
                {
                    ++no_line;

                    if (dr.RowState == DataRowState.Deleted) continue;

                    if (sPUSU == "100")
                    {
                        DataRow[] drDD = _flexDD.DataTable.Select("NO_POLINE = '" + D.GetString(dr["NO_LINE"]) + "'");

                        if (drDD != null && drDD.Length != 0)
                        {
                            foreach (DataRow rowDD in drDD)
                            {
                                rowDD["NO_POLINE"] = no_line;
                            }
                        }
                    }

                    dr["NO_PO"] = No_PO;
                    dr["NO_LINE"] = no_line;

                }

               //토페스 
                if (m_tab_poh.TabPages.Contains(tabPage8))
                {
                    foreach (DataRow row in _flexH.DataTable.Rows)
                    {
                        row["NO_PO"] = No_PO;
                    }
                }
            }

            #region ->삼보컴퓨터 전용

            if (Global.MainFrame.ServerKeyCommon == "TRIGEM")
            {
                string FG_TRANS = D.GetString(_header.CurrentRow["FG_TRANS"]);
                string DOCU_YM = string.Empty;
                if (FG_TRANS == "004" || FG_TRANS == "005") //  MASTER L/C / T/T( MASTER 기타)
                {
                    string DACU_NO = string.Empty;
                    if (FG_TRANS == "004") //  MASTER L/C
                    {
                        DACU_NO = "2";
                        DOCU_YM = "201210";
                    }
                    else
                    {
                        DACU_NO = "8";
                        DOCU_YM = "201209";
                    }

                    DACU_NO = DACU_NO + ((string)GetSeq(LoginInfo.CompanyCode, "PU", "39", DOCU_YM)).Substring(7, 9);
                    _header.CurrentRow["DACU_NO"] = DACU_NO;
                    txtDocuNum.Text = DACU_NO;
                }
            }

            if (Global.MainFrame.ServerKeyCommon == "HANILTOYO" || Global.MainFrame.ServerKeyCommon == "DZSQL" || Global.MainFrame.ServerKeyCommon == "SQL_")
            {

                string DACU_NO = ((string)GetSeq(LoginInfo.CompanyCode, "PU", "44", tb_DT_PO.Text));
                _header.CurrentRow["DACU_NO"] = DACU_NO;
                txtDocuNum.Text = DACU_NO;


            }
            #endregion

            if (_flexD.HasNormalRow && _header.CurrentRow["YN_REQ"].ToString() == "N")
            {
                cPU_RCVH.DT_PURCVH.Clear();
                cPU_RCVL.DT_PURCV.Clear();
                GetPU_RCV_Save(_header.GetChanges(), _flexD.DataTable);
                lb_RcvSave = true;
            }

         
        }

        if (_flexD.HasNormalRow && _header.CurrentRow["TP_GR"].ToString() == "101")
        {
            lb_RevSave = true;
        }

        //ChagePoState(); 2010.02.08 주석처리함

        //#warning 이상함 나중에 꼭 확인할부분...

        // ChagePoState(); 내용과 겹치는 부분이다.  ----------------------------------------------
        // 추후에 깊게 분석해서 내용 정리할것 반드시....
        DataTable dtSU = _biz.GetYN_SU(D.GetString(_header.CurrentRow["CD_TPPO"]));
        if (_flexD.HasNormalRow)   //FixedRow 외의 행이 있으면 true 
        {
            // int i = _flexD.Rows.Fixed;                
            foreach (DataRow row in _flexD.DataTable.Rows) //프로젝트값을 라인에 일괄반영
            {
                //if (row.RowState == DataRowState.Deleted) continue;

                if (row.RowState == DataRowState.Added || str복사구분 == "COPY")   //.Deleted) continue;  // 추가모드
                {
                    if (row["YN_ORDER"].ToString() == "Y" || row["YN_REQ"].ToString() == "N" || lb_RevSave )    // 자동승인이고 의뢰된 것 lb_RevSave(자동프로세스 100:발주(DEFAULT) 101: 발주 - 가입고)
                    {
                        row["FG_POST"] = "R";
                        row["FG_POCON"] = "002";
                        m_pnlHeader_Enabled();
                        //m_pnlHeader.Enabled = false; -- 비고 수정가능 때문에 컨트롤별로 Enabled설정 2011.04.08
                        oneGrid2.Enabled = false;

                        SetHeadControlEnabled(false, 2);
                        btn_RE_PR.Enabled = false;
                        btn_RE_APP.Enabled = false;
                        btn_RE_SO.Enabled = false;
                        btn_so_gir.Enabled = false;


                        curNEGO금액.Enabled = false;
                        btn배부.Enabled = false;
                        curDe.Enabled = false;

                    }
                    else
                    {
                        row["FG_POST"] = "O";
                        row["FG_POCON"] = "001";
                        SetHeadControlEnabled(false, 1);


                        curNEGO금액.Enabled = true;
                        btn배부.Enabled = true;
                        curDe.Enabled = true;
                    }


                    // 2010.02.08 NO_PO(발주번호)가 ''로 입력되는 경우가 있다. 따라서 등록시 한번더 입력해준다.
                    if (row["NO_PO"].ToString().Trim().Length < 3)
                    {
                        if (_header.CurrentRow["NO_PO"].ToString() == string.Empty)
                        {
                            ShowMessage("발주번호는 공백이 될 수 없습니다.");
                            return false;
                        }
                        else
                            row["NO_PO"] = _header.CurrentRow["NO_PO"].ToString();

                    }


                }

                //if (row.RowState != DataRowState.Deleted)
                //    i++;

                //아사히키세히전용
                if (_m_Company_only == "001")
                {
                    if (D.GetString(_header.CurrentRow["FG_TPPURCHASE"]) != D.GetString(row["CD_TP"]))
                    {

                        msg = row["CD_ITEM"].ToString().PadRight(15, ' ') + " " + row["CD_TP"].ToString().PadRight(15, ' ');

                        check_fg_tppurchase.AppendLine(msg);
                        flag = false;
                    }

                }


                if (s소요자재체크 != "000" && D.GetString(dtSU.Rows[0]["YN_SU"]) == "Y")
                {
                    StringBuilder sbErrorList = new StringBuilder();

                    DataRow[] drs = _flexDD.DataTable.Select("NO_POLINE = '" + row["NO_LINE"] + "'");

                    if (drs == null || drs.Length == 0)
                        sbErrorList.AppendLine(D.GetString(row["CD_ITEM"]));
                    

                    if (sbErrorList.Length > 0 && s소요자재체크 == "100")
                    {
                        if (ShowDetailMessage(DD("아래 품목의 소요자재가 없습니다. 저장하시겠습니까?"), sbErrorList.ToString(), "QY2") != DialogResult.Yes)
                            return false;
                    }
                    else if (sbErrorList.Length > 0 && s소요자재체크 == "200")
                    {
                        ShowDetailMessage(DD("아래 품목의 소요자재가 없어 저장할 수 없습니다."), sbErrorList.ToString());
                        return false;
                    }
                }



            }

            if (!flag)
            {
                ShowDetailMessage
                (@"발주형태의 매입정보와 품목정보의 매입정보가 다릅니다." +
                   "\n▼ 버튼을 눌러서 목록을 확인하세요!", D.GetString(check_fg_tppurchase));
                return false;
            }
        }

         
        //---------------------------------------------------------------------------------------------------


        // 호출여부 = false; // 다시 초기화
        //   }

        _header.CurrentRow["DC50_PO"] = tb_DC.Text;
        _header.CurrentRow["FG_TRACK"] = "M";
        _header.CurrentRow["DC_RMK2"] = tb_DC_RMK2.Text;

        if (m_tab_poh.TabPages.Contains(tabPage7))
        {
            _header.CurrentRow["DT_PROCESS_IV"] = dtp_DT_PROCESS_IV.Text;
            _header.CurrentRow["DT_PAY_PRE_IV"] = dtp_DT_PAY_PRE_IV.Text;
            _header.CurrentRow["DT_DUE_IV"] = dtp_DT_DUE_IV.Text;
            _header.CurrentRow["FG_PAYBILL_IV"] = cbo_FG_PAYBILL_IV.SelectedValue;
            if (D.GetString(cbo_CD_DOCU_IV.SelectedValue) == string.Empty)
                _header.CurrentRow["CD_DOCU_IV"] = "45";
            else
                _header.CurrentRow["CD_DOCU_IV"] = cbo_CD_DOCU_IV.SelectedValue;
            _header.CurrentRow["AM_K_IV"] = cur_AM_K_IV.DecimalValue;
            _header.CurrentRow["VAT_TAX_IV"] = cur_VAT_TAX_IV.DecimalValue;
            _header.CurrentRow["DC_RMK_IV"] = txtDcRmkIv.Text;
        }


        //_header.CurrentRow["TP_TRANSPORT"] = cbo_TP_TRANSPORT.SelectedValue.ToString();
        //_header.CurrentRow["COND_PAY"]     = cbo_COND_PAY.SelectedValue.ToString();
        //_header.CurrentRow["COND_PAY_DLV"] = txt_COND_PAY_DLV.Text;
        //_header.CurrentRow["COND_PRICE"]   = cbo_COND_PRICE.SelectedValue.ToString();
        //_header.CurrentRow["ARRIVER"]      = txt_ARRIVER.Text;

        cbo_TP_TRANSPORT.Enabled = true;
        cbo_COND_PAY.Enabled = true;
        txt_COND_PAY_DLV.Enabled = true;
        cbo_TP_TRANSPORT.Enabled = true;
        txt_ARRIVER.Enabled = true;
        txt_LOADING.Enabled = true;
        bp_ARRIVER.Enabled = true;
        bp_LOADING.Enabled = true;
        cbo_COND_SHIPMENT.Enabled = true;
        cbo_FREIGHT_CHARGE.Enabled = true;
        cbo_stnd_pay.Enabled = true;
        txt_cond_days.Enabled = true;
        cbo_origin.Enabled = true;

        //cbo포장형태.Enabled = true;

        //txt_LOADING

        DataTable dtH = _header.GetChanges();
        DataTable dtL = null;

        if (str복사구분 == "COPY")
        {
            FillPol();
            //dtL = _flexD.DataTable;
            dtL = _flexD.DataTable.Clone();

            foreach (DataRow dr in _flexD.DataTable.Select("", "", DataViewRowState.CurrentRows))
            {
                dtL.ImportRow(dr);
            }

        }
        else
            dtL = _flexD.GetChanges();

        if (dtH == null && dtL == null) return true;

        cPU_RCVH.DT_PURCVH.GetChanges();
        cPU_RCVL.DT_PURCV.GetChanges();
    

        /* 예산chk yes이고 예산통과N이면 ...예산chk실행( 예산통과Y이면 이미 승인되었다고 인정) */

        // 성공: error_msg가 있거나 (예산통제구분값이 '4' 일경우 금액초과시 통제함)이면 경고후 저장여부 확인=>yes이면 이력에 저장
        // 실패: BUDGET_PASS를 Y로 수정하고 이력도 같이 저장 

        if (_구매예산CHK설정 == "Y" && dtL != null && cbo_YN_BUDGET.SelectedValue.ToString() == "Y") //&& tb_BUDGET_PASS.Text == "N")
        {
            //DataRow[] dr = _flexD.DataTable.Select("CD_TPPO = '' OR CD_TPPO IS NULL " +
            //    " OR CD_CC = '' OR CD_CC IS NULL " +
            //    " OR CD_BUDGET = '' OR CD_BUDGET IS NULL " +
            //    " OR CD_BGACCT = '' OR CD_BGACCT IS NULL " +
            //                                       "");

            DataRow[] dr = _flexD.DataTable.Select("CD_BUDGET = '' OR CD_BUDGET IS NULL " +
                                               " OR CD_BGACCT = '' OR CD_BGACCT IS NULL " +
                                                   "");
            if (dr != null && dr.Length > 0)
            {
                ShowMessage("예산확인 선택(Y)시 발주형태,CC코드,예산단위,예신계정은 필수입력입니다.");
                _flexD.Focus();
                return false;
            }
        }
        Boolean lb_chk_pass = false;

        DataTable dt_BUDGET_HST = _biz.PU_BUDGET_HST();

        if (D.GetString(cbo_YN_BUDGET.SelectedValue) == "Y" && tb_BUDGET_PASS.Text == "N")
        {

            DataTable dt_chk = 예산chk(_flexD.DataTable);

            if (dt_chk.Rows.Count > 0)
            {
                DataRow[] dr = dt_chk.Select("( AM_JAN < 0 AND TP_BUNIT = '4') AND ERROR_MSG IS NOT NULL");
                if (dr != null && dr.Length > 0)
                {
                    ShowMessage("예산통제대상계정이 예산잔액을 초과했거나 CHK시 오류가 발생했습니다");
                    lb_chk_pass = false;
                    tb_BUDGET_PASS.Text = "N";
                    //dtH.Rows[0]["BUDGET_PASS"] = "N";
                    _header.CurrentRow["BUDGET_PASS"] = "N";
                }
                else
                {
                    lb_chk_pass = true;
                    tb_BUDGET_PASS.Text = "Y";
                    //dtH.Rows[0]["BUDGET_PASS"] = "Y";
                    _header.CurrentRow["BUDGET_PASS"] = "Y";
                }

                // P_PU_BUDGET_SUB m_dlg = new P_PU_BUDGET_SUB(dt_chk);
                P_PU_BUDGET_SUB m_dlg = new P_PU_BUDGET_SUB(_flexD.DataTable, tb_DT_PO.Text, "NO_PO"); // 요청일자가 필요함, 화면구분(PR:구매요청, APP:구매품의, PO:구매발주)

                m_dlg.ShowDialog(this);

                //if (m_dlg.ShowDialog(this) == DialogResult.OK)
                //    return false;

                if (lb_chk_pass == false)
                {

                    DialogResult result;
                    // 변경된 내용이 있습니다.  저장하시겠습니까?
                    result = MessageBox.Show("예산통제가 통과 되지안았습니다. (N) " + Environment.NewLine +
                        "저장은 가능하며 예산통제내역은 예산chk내역에서 볼수있습니다. " + Environment.NewLine +
                        "저장하시겠습니까?", "예산통제대상", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                    if (result == DialogResult.Cancel)
                        return false;
                }

                DataRow NewRow;

                foreach (DataRow row in dt_chk.Rows)
                {

                    NewRow = dt_BUDGET_HST.NewRow();

                    NewRow["NO_PU"] = _header.CurrentRow["NO_PO"].ToString();
                    NewRow["NENU_TYPE"] = "PU_PO_REG";

                    NewRow["CD_BUDGET"] = row["CD_BUDGET"];
                    NewRow["NM_BUDGET"] = row["NM_BUDGET"];
                    NewRow["CD_BGACCT"] = row["CD_BGACCT"];
                    NewRow["NM_BGACCT"] = row["NM_BGACCT"];

                    NewRow["AM_ACTSUM"] = row["AM_ACTSUM"];
                    NewRow["AM_JSUM"] = row["AM"];
                    NewRow["RT_JSUM"] = row["RT_JSUM"];
                    NewRow["AM"] = row["AM"];
                    NewRow["AM_JAN"] = row["AM_JAN"];

                    NewRow["TP_BUNIT"] = row["TP_BUNIT"];
                    NewRow["ERROR_MSG"] = row["ERROR_MSG"];

                    NewRow["ID_INSERT"] = Global.MainFrame.LoginInfo.EmployeeNo;

                    dt_BUDGET_HST.Rows.Add(NewRow);

                }

            }

        }

        //시리얼번호 등록을 위한 string builder : 2011/03/29 아이큐브 개발팀 김현철
        //2011/04/14 : SOLITECH1 을 위해 StartsWith 로 비교 변경
        if (Global.MainFrame.ServerKeyCommon.StartsWith("SOLIDTECH", true, System.Globalization.CultureInfo.CurrentCulture))
        {
            StringBuilder sb = new StringBuilder();
            foreach (DataRow row in dtL.Rows)
            {
                if (tb_DT_LIMIT.Text == string.Empty)
                    sb.Append(row["DT_LIMIT"].ToString() + row["CD_ITEM"].ToString() + "|");
                else
                    sb.Append(tb_DT_LIMIT.Text + row["CD_ITEM"].ToString() + "|");
            }

            object[] objSerial = new object[4];
            objSerial[0] = LoginInfo.CompanyCode;
            objSerial[1] = tb_NM_PARTNER.CodeValue;
            objSerial[2] = cbo_CD_PLANT.SelectedValue;
            objSerial[3] = sb.ToString();

            DataTable dtSerial = _biz.GetItemSerial(objSerial);

            foreach (DataRow row in dtL.Rows)
            {
                if (dtSerial.Rows.Count > 0 && D.GetInt(row["QT_PO"]) > 0)
                {
                    DataRow[] rows = dtSerial.Select(
                        string.Format("DT_LIMIT = '{0}' AND CD_ITEM = '{1}'",
                        row["DT_LIMIT"], row["CD_ITEM"]));

                    if (rows != null && rows.Length > 0)
                    {
                        int nMaxQt = D.GetInt(row["QT_PO_MM"]) - 1;
                        row["DC1"] = string.Format("{0}{1:D5}", rows[0]["PREFIX"], D.GetInt(rows[0]["POSTFIX"]));
                        row["DC2"] = string.Format("{0}{1:D5}", rows[0]["PREFIX"], D.GetInt(rows[0]["POSTFIX"]) + nMaxQt);
                        rows[0].BeginEdit();
                        rows[0]["POSTFIX"] = D.GetInt(rows[0]["POSTFIX"]) + nMaxQt + 1;
                        rows[0].EndEdit();
                        rows[0].AcceptChanges();

                        for (int i = _flexD.Rows.Fixed; i < _flexD.Rows.Count; i++)
                        {
                            if (row["NO_LINE"].Equals(_flexD[i, "NO_LINE"]))
                            {
                                _flexD[i, "DC1"] = row["DC1"];
                                _flexD[i, "DC2"] = row["DC2"];
                                break;
                            }
                        }
                    }
                } 
            }
        }

        if (추가모드여부&& D.GetString(_header.CurrentRow["YN_ORDER"]) == "Y" && BASIC.GetMAEXC("발주등록(공장)-프로젝트예산통제설정") == "100") //프로젝트예산통제설정사용
        {
            P_PU_PJT_BUDGET_CTL_SUB _dlg = new P_PU_PJT_BUDGET_CTL_SUB(dtL, "NO_PO", "REG"); //dtL : 저장될 라인테이블
            //NO_PO : 이화면의 키가되는번호컬럼코드
            //REG : 등록본에서사용(하나라도 에러면 통과시키지않고 WARNING데이터도 부분적으로하지도못한다) 
            // or
            // "MNG" : WARNING데이터 라인하나하나 선별해서 따로등록할수있는 방식

            if (_dlg.ShowDialog() == DialogResult.OK)
            {
                if (_dlg.ret_data != null && _dlg.ret_data.Rows.Count != 0)
                {
                    dtL = _dlg.ret_data;
                }

            }
            else
            {
                return false;
            }


        }

        // 소요자재..
        DataTable dtDD = null;
        if (sPUSU == "100" && _flexDD.Enabled == true)
        {
            if (str복사구분 == "COPY")
            {
                dtDD = _flexDD.DataTable.Clone();

                foreach (DataRow dr in _flexDD.DataTable.Select("", "", DataViewRowState.CurrentRows))
                {
                    dtDD.ImportRow(dr);
                }
            }
            else
                dtDD = _flexDD.GetChanges();


            if (dtDD != null)
            {
                foreach (DataRow dr in dtDD.Rows)
                {
                    //DataRow의 BeginEdit와 EndEdit가 반드시 들어가야 함.
                    //BeginEdit와 EndEdit를 넣지 않을경우 채번한 번호의 인식이 되지않을 수도 있음.
                    //DataTable.GetChanges()와 DataTable의 내역이 틀려질 경우도 있음.
                    if (dr.RowState != DataRowState.Deleted)
                    {
                        dr.BeginEdit();

                        dr["NO_PO"] = _header.CurrentRow["NO_PO"];
                        dr.EndEdit();
                    }
                }
            }
        }

        DataTable dtTH = _flexH.GetChanges();

        bool bSuccess = _biz.Save(dtH, dtL, lb_RcvSave, cPU_RCVH.DT_PURCVH, cPU_RCVL.DT_PURCV, str복사구분
                        , dt_BUDGET_HST, _header.CurrentRow["NO_PO"].ToString(), _infosub_dlg.si_return, lb_RevSave, dtDD, dtTH);
        if (!bSuccess)
            return false;

        if (추가모드여부)
            tb_NO_PO.Text = No_PO;

        _flexD.Focus();

        _header.AcceptChanges();
        _flexD.AcceptChanges();
        _flexDD.AcceptChanges();

        //토페스 
        if (m_tab_poh.TabPages.Contains(tabPage8))
            _flexH.AcceptChanges();



        return true;
    }
    

    #region -> 자동입고의뢰 저장로직(GetPU_RCV_Save)

    /// <summary>
    /// 자동입고의뢰 저장로직
    /// </summary>
    /// <param name="pdt_Head">입고의뢰(PU_RCVH) 헤더 테이블</param>
    /// <param name="pdt_Line">입고의뢰(PU_RCVH) 라인 테이블</param>

    private void GetPU_RCV_Save(DataTable pdt_Head, DataTable pdt_Line)
    {
        cPU_RCVH = new pur.CDT_PU_RCVH();

        DataRow[] ldr_temp;

        for (int i = 0; i < pdt_Line.Rows.Count; i++)       // 의뢰 정보 수집
        {
            if (pdt_Line.Rows[i].RowState == DataRowState.Deleted)
                continue;

            if (pdt_Line.Rows[i]["NO_RCV"].ToString() == "")    // 이전에 입력된 것은 제외시키기 위해
            {
                string no_seq = (string)GetSeq(LoginInfo.CompanyCode, "PU", "04", tb_DT_PO.Text.Substring(0, 6));
                // 공장  ldt_result 분리
                ldr_temp = pdt_Line.Select("CD_PLANT ='" + pdt_Line.Rows[i]["CD_PLANT"].ToString() + "'");

                //의뢰 채번									
                DataRow newRow = cPU_RCVH.DT_PURCVH.NewRow();
                newRow["NO_RCV"] = no_seq;
                newRow["CD_PLANT"] = pdt_Line.Rows[i]["CD_PLANT"];
                newRow["CD_PARTNER"] = pdt_Head.Rows[0]["CD_PARTNER"];
                newRow["DT_REQ"] = pdt_Head.Rows[0]["DT_PO"];
                newRow["NO_EMP"] = pdt_Head.Rows[0]["NO_EMP"];
                newRow["FG_TRANS"] = pdt_Head.Rows[0]["FG_TRANS"];
                newRow["FG_PROCESS"] = pdt_Head.Rows[0]["FG_TAXP"];
                newRow["YN_AM"] = pdt_Head.Rows[0]["YN_AM"];
                newRow["CD_EXCH"] = pdt_Head.Rows[0]["CD_EXCH"];
                newRow["YN_RETURN"] = "N";
                newRow["CD_SL"] = pdt_Line.Rows[i]["CD_SL"];
                newRow["YN_AUTORCV"] = pdt_Line.Rows[0]["YN_AUTORCV"];
                newRow["DT_IO"] = pdt_Head.Rows[0]["DT_PO"];
                newRow["CD_DEPT"] = pdt_Head.Rows[0]["CD_DEPT"];
                newRow["FG_RCV"] = pdt_Head.Rows[0]["FG_TPRCV"];
                cPU_RCVH.DT_PURCVH.Rows.Add(newRow);

                for (int j = 0; j < ldr_temp.Length; j++)
                {
                    ldr_temp[j]["NO_RCV"] = no_seq;
                    ldr_temp[j]["NO_RCVLINE"] = j + 1;
                    ldr_temp[j]["FG_POCON"] = "002";
                }
            }
        }

        if (cPU_RCVH.DT_PURCVH == null || cPU_RCVH.DT_PURCVH.Rows.Count <= 0) return;

        for (int i = 0; i < cPU_RCVH.DT_PURCVH.Rows.Count; i++)
        {
            DataTable ldt_CopyCHG = pdt_Line.Clone();
            DataRow[] ldra_temp = pdt_Line.Select("NO_RCV ='" + cPU_RCVH.DT_PURCVH.Rows[i]["NO_RCV"].ToString() + "'");

            if (ldra_temp != null && ldra_temp.Length > 0)
            {
                for (int j = 0; j < ldra_temp.Length; j++)
                {
                    ldt_CopyCHG.ImportRow(ldra_temp[j]);
                }

                cPU_RCVL = new pur.CDT_PU_RCV();

                DataRow row;
                for (int k = 0; k < ldt_CopyCHG.Rows.Count; k++)
                {
                    row = ldt_CopyCHG.Rows[k];
                    DataRow newrow = cPU_RCVL.DT_PURCV.NewRow();

                    newrow["NO_RCV"] = cPU_RCVH.DT_PURCVH.Rows[i]["NO_RCV"];
                    newrow["NO_LINE"] = k + 1;
                    newrow["DT_IO"] = cPU_RCVH.DT_PURCVH.Rows[i]["DT_IO"];
                    newrow["NO_PO"] = row["NO_PO"];
                    newrow["NO_POLINE"] = row["NO_LINE"];
                    newrow["CD_PURGRP"] = pdt_Head.Rows[0]["CD_PURGRP"];
                    newrow["CD_ITEM"] = row["CD_ITEM"];
                    newrow["CD_UNIT_MM"] = row["CD_UNIT_MM"];
                    newrow["QT_REQ_MM"] = row["QT_PO_MM"];
                    newrow["QT_REQ"] = row["QT_PO"];
                    newrow["DT_LIMIT"] = row["DT_LIMIT"];
                    newrow["DT_PLAN"] = row["DT_PLAN"];
                    newrow["CD_EXCH"] = pdt_Head.Rows[0]["CD_EXCH"];
                    newrow["RT_EXCH"] = pdt_Head.Rows[0]["RT_EXCH"];
                    newrow["YN_INSP"] = "N";
                    newrow["UM_EX_PO"] = row["UM_EX_PO"];
                    newrow["UM_EX"] = row["UM_EX"];
                    newrow["AM_EX"] = row["AM_EX"];
                    newrow["AM"] = row["AM"];

                    newrow["AM_EXREQ"] = row["AM_EX"];
                    newrow["AM_REQ"] = row["AM"];

                    newrow["VAT"] = row["VAT"];
                    newrow["UM"] = row["UM"];
                    newrow["CD_PJT"] = row["CD_PJT"];
                    newrow["YN_RETURN"] = row["YN_RETURN"];
                    newrow["FG_TPPURCHASE"] = row["FG_TPPURCHASE"];
                    newrow["FG_TAXP"] = pdt_Head.Rows[0]["FG_TAXP"];
                    newrow["FG_TAX"] = row["FG_TAX"];
                    newrow["FG_RCV"] = row["FG_RCV"];
                    newrow["FG_TRANS"] = row["FG_TRANS"];
                    newrow["YN_AUTORCV"] = row["YN_AUTORCV"];
                    newrow["YN_REQ"] = row["YN_REQ"];
                    newrow["CD_SL"] = row["CD_SL"];
                    newrow["NO_EMP"] = pdt_Head.Rows[0]["NO_EMP"];
                    newrow["CD_PLANT"] = row["CD_PLANT"];
                    newrow["CD_PARTNER"] = pdt_Head.Rows[0]["CD_PARTNER"];
                    newrow["DT_REQ"] = pdt_Head.Rows[0]["DT_PO"];
                    cPU_RCVL.DT_PURCV.Rows.Add(newrow);
                }
            }
        }
    }

    #endregion

    #endregion

    #endregion

    #endregion

    #region -> 인쇄

    public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
    {
        try
        {
            if (추가모드여부) return;

            if (IsChanged())
            {
                if (ShowMessage(공통메세지.변경된사항이있습니다저장하시겠습니까, "QY2") == DialogResult.No)
                    return;

                if (!BeforeSave()) return;

                ToolBarSaveButtonEnabled = false;
                if (MsgAndSave(PageActionMode.Save))
                    ShowMessage(PageResultMode.SaveGood);
                else
                    ToolBarSaveButtonEnabled = true;

                Settings1.Default.CD_PURGRP_SET = _header.CurrentRow["CD_PURGRP"].ToString();
                Settings1.Default.CD_TPPO_SET = _header.CurrentRow["CD_TPPO"].ToString();
                Settings1.Default.FG_PAYMENT_SET = _header.CurrentRow["FG_PAYMENT"].ToString();
                Settings1.Default.CD_EXCH = _header.CurrentRow["CD_EXCH"].ToString();
                Settings1.Default.RT_EXCH = D.GetDecimal(_header.CurrentRow["RT_EXCH"]);
                Settings1.Default.DC_RMK_TEXT = D.GetString(txt_DC_RMK_TEXT.Text);
                Settings1.Default.DC_RMK_TEXT2 = D.GetString(txt_DC_RMK_TEXT2.Text);
                Settings1.Default.TP_UM_TAX = _header.CurrentRow["TP_UM_TAX"].ToString();
                Settings1.Default.Save();
            }

            rptHelper = new ReportHelper("R_PU_PO_REG2_0", "구매발주서");
            rptHelper.Printing += new ReportHelper.PrintEventHandler(rptHelper_Printing);
            rptHelper.Print();

        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
    }

    private void SetPrint(bool checkprint)
    {
        try
        {
            //checkprint : true -> 출력
            //checkprint : false -> 메일로 보내기 위해서 pdf로 저장

            if (추가모드여부) return;

            //if (_header.CurrentRow["NO_BIZAREA"].ToString() == string.Empty) //김성태대리의 요청으로인하여 인쇄버튼클릭후 저장버튼 및 종료시점에 저장하겠냐는 메세지가 나와서 그부분을 없애달라는 요청으로 인하여 이부분을 주석처리함
            //    조회(tb_NO_PO.Text, "OK");                                    //밑에서 데이터를 다가져오므로 현재 헤더와 그리드에 라인이 데이터가있는지만 조사하여 넘기는작업을해도될것같음 2011-11-11

            if (!_flexD.HasNormalRow) return;


            //string ls_nm_bizarea = _biz.GetBizAreaCodeSearch(tb_NO_EMP.CodeValue);

            DataTable dtBiz = _biz.GetBizAreaCodeSearch(tb_NO_EMP.CodeValue);
            string ls_nm_bizarea = "";        
            string txtARRIVER = "";
            string NO_BIZAREA_temp = "";
            string NO_COMPANY_temp = "";
            //if (D.GetString(_header.CurrentRow["NO_BIZAREA"]) != "" && D.GetString(_header.CurrentRow["NO_BIZAREA"]).Length == 10) { NO_BIZAREA_temp = D.GetString(_header.CurrentRow["NO_BIZAREA"]).Substring(0, 3) + "-" + D.GetString(_header.CurrentRow["NO_BIZAREA"]).Substring(3, 2) + "-" + D.GetString(_header.CurrentRow["NO_BIZAREA"]).Substring(5, 5); }
            //if (D.GetString(_header.CurrentRow["NO_COMPANY"]) != "" && D.GetString(_header.CurrentRow["NO_COMPANY"]).Length == 10) { NO_COMPANY_temp = D.GetString(_header.CurrentRow["NO_COMPANY"]).Substring(0, 3) + "-" + D.GetString(_header.CurrentRow["NO_COMPANY"]).Substring(3, 2) + "-" + D.GetString(_header.CurrentRow["NO_COMPANY"]).Substring(5, 5); }

            if (dtBiz != null && dtBiz.Rows.Count > 0)
            {
                ls_nm_bizarea = D.GetString(dtBiz.Rows[0]["NM_BIZAREA"]);
                if (D.GetString(dtBiz.Rows[0]["NO_BIZAREA"]) != "" && D.GetString(dtBiz.Rows[0]["NO_BIZAREA"]).Length == 10) { NO_BIZAREA_temp = D.GetString(dtBiz.Rows[0]["NO_BIZAREA"]).Substring(0, 3) + "-" + D.GetString(dtBiz.Rows[0]["NO_BIZAREA"]).Substring(3, 2) + "-" + D.GetString(dtBiz.Rows[0]["NO_BIZAREA"]).Substring(5, 5); }
                if (D.GetString(dtBiz.Rows[0]["NO_COMPANY"]) != "" && D.GetString(dtBiz.Rows[0]["NO_COMPANY"]).Length == 10) { NO_COMPANY_temp = D.GetString(dtBiz.Rows[0]["NO_COMPANY"]).Substring(0, 3) + "-" + D.GetString(dtBiz.Rows[0]["NO_COMPANY"]).Substring(3, 2) + "-" + D.GetString(dtBiz.Rows[0]["NO_COMPANY"]).Substring(5, 5); }
            }

            string txtDtPo = tb_DT_PO.Text;
            string NO_COUNT = D.GetString(_flexD.DataTable.Rows.Count);

            if(!checkprint) //메일발송일 경우만..
                rptHelper = new ReportHelper("R_PU_PO_REG2_0", "구매발주서");

            Dictionary<string, string> dic = new Dictionary<string, string>();

            DataSet ds = _biz.Print(tb_NO_PO.Text);

            dic["거래처명"] = D.GetString(ds.Tables[0].Rows[0]["LN_PARTNER"]);
            dic["대표자1"] = D.GetString(ds.Tables[0].Rows[0]["NM_CEO1"]);
            dic["우편번호1"] = D.GetString(ds.Tables[0].Rows[0]["NO_POST1"]);
            dic["주소1"] = D.GetString(ds.Tables[0].Rows[0]["DC_ADS1_H"]);
            dic["상세주소1"] = D.GetString(ds.Tables[0].Rows[0]["DC_ADS1_D"]);
            dic["전화1"] = D.GetString(ds.Tables[0].Rows[0]["NO_TEL1"]);
            dic["팩스1"] = D.GetString(ds.Tables[0].Rows[0]["NO_FAX1"]);
            dic["담당자1"] = D.GetString(ds.Tables[0].Rows[0]["NM_PTR"]);

            dic["대표자"] = D.GetString(ds.Tables[0].Rows[0]["NM_CEO"]);
            dic["우편번호"] = D.GetString(ds.Tables[0].Rows[0]["ADS_NO"]);
            dic["주소"] = D.GetString(ds.Tables[0].Rows[0]["ADS_H"]);
            dic["상세주소"] = D.GetString(ds.Tables[0].Rows[0]["ADS_D"]);
            dic["전화"] = D.GetString(ds.Tables[0].Rows[0]["NO_TEL"]);
            dic["팩스"] = D.GetString(ds.Tables[0].Rows[0]["NO_FAX"]);

            dic["구매그룹_전화"] = D.GetString(ds.Tables[0].Rows[0]["PURGRP_NO_TEL"]);
            dic["구매그룹_팩스"] = D.GetString(ds.Tables[0].Rows[0]["PURGRP_NO_FAX"]);
            dic["구매그룹_E_메일"] = D.GetString(ds.Tables[0].Rows[0]["PURGRP_E_MAIL"]);


            dic["담당자전화번호"] = D.GetString(ds.Tables[0].Rows[0]["EMP_NO_TEL"]);//2011.06.22 이장원 프린트출력물 변수 3개 추가
            dic["구매그룹전화번호"] = D.GetString(ds.Tables[0].Rows[0]["PURGRP_NO_TEL"]);
            dic["구매그룹팩스번호"] = D.GetString(ds.Tables[0].Rows[0]["PURGRP_NO_FAX"]);

            dic["USER_NO_TEL"] = D.GetString(ds.Tables[0].Rows[0]["EMP_NO_TEL1"]);

            dic["NM_PACKING"] = D.GetString(ds.Tables[0].Rows[0]["NM_PACKING"]);
            dic["SUPP_ADS1_H"] = D.GetString(ds.Tables[0].Rows[0]["SUPP_ADS1_H"]);
            dic["SUPP_ADS1_H"] = D.GetString(ds.Tables[0].Rows[0]["SUPP_ADS1_D"]);

            dic["구매그룹팩스번호"] = D.GetString(ds.Tables[0].Rows[0]["PURGRP_NO_FAX"]);
            dic["도착지명"] = D.GetString(ds.Tables[0].Rows[0]["NM_ARRIVER"]);
            dic["NO_PO_BAR"] = "*" + D.GetString(ds.Tables[0].Rows[0]["NO_PO"]) + "*";
            
            //}

            #region -> Old Source Code
            //rptHelper.SetData("발주번호", tb_NO_PO.Text);
            //rptHelper.SetData("발주일자", tb_DT_PO.Text);
            //rptHelper.SetData("공장", cbo_CD_PLANT.Text);
            //rptHelper.SetData("담당자", tb_NO_EMP.CodeName);
            //rptHelper.SetData("환율", tb_NM_EXCH.Text);
            //rptHelper.SetData("환정보", cbo_NM_EXCH.Text);
            //rptHelper.SetData("거래처", tb_CD_DEPT.Text);
            //rptHelper.SetData("지급조건", cbo_PAYment.Text);
            //rptHelper.SetData("과세구분", cbo_FG_TAX.Text);
            //rptHelper.SetData("단가유형", cbo_FG_UM.Text);
            //rptHelper.SetData("부가세율", tb_TAX.Text);
            //if (rbtn_All.Checked)
            //    rptHelper.SetData("계산서처리구분", rbtn_All.Text);
            //else
            //    rptHelper.SetData("계산서처리구분", rbtn_PRI.Text);

            //rptHelper.SetData("부가세여부", cbo_TP_TAX.Text);
            //rptHelper.SetData("비고", tb_DC.Text);
            //rptHelper.SetData("거래처명", _header.CurrentRow["LN_PARTNER"].ToString());
            //rptHelper.SetData("대표자1", _header.CurrentRow["NM_CEO1"].ToString());
            //rptHelper.SetData("우편번호1", _header.CurrentRow["NO_POST1"].ToString());
            //rptHelper.SetData("주소1", _header.CurrentRow["DC_ADS1_H"].ToString());
            //rptHelper.SetData("상세주소1", _header.CurrentRow["DC_ADS1_D"].ToString());
            //rptHelper.SetData("전화1", _header.CurrentRow["NO_TEL1"].ToString());
            //rptHelper.SetData("팩스1", _header.CurrentRow["NO_FAX1"].ToString());
            //rptHelper.SetData("담당자1", _header.CurrentRow["NM_PTR"].ToString());

            //rptHelper.SetData("사업자등록번호", NO_BIZAREA_temp);
            //rptHelper.SetData("사업자등록번호1", NO_COMPANY_temp);
            //rptHelper.SetData("대표자", _header.CurrentRow["NM_CEO"].ToString());
            //rptHelper.SetData("우편번호", _header.CurrentRow["ADS_NO"].ToString());
            //rptHelper.SetData("주소", _header.CurrentRow["ADS_H"].ToString());
            //rptHelper.SetData("상세주소", _header.CurrentRow["ADS_D"].ToString());
            //rptHelper.SetData("전화", _header.CurrentRow["NO_TEL"].ToString());
            //rptHelper.SetData("팩스", _header.CurrentRow["NO_FAX"].ToString());

            //rptHelper.SetData("구매그룹_전화", _header.CurrentRow["PURGRP_NO_TEL"].ToString());
            //rptHelper.SetData("구매그룹_팩스", _header.CurrentRow["PURGRP_NO_FAX"].ToString());
            //rptHelper.SetData("구매그룹_E_메일", _header.CurrentRow["PURGRP_E_MAIL"].ToString());
            //rptHelper.SetData("사업장명", ls_nm_bizarea);
            //rptHelper.SetData("비고2", tb_DC_RMK2.Text);

            //rptHelper.SetData("운송방법", cbo_TP_TRANSPORT.Text);
            //rptHelper.SetData("지불조건", cbo_COND_PAY.Text);
            //rptHelper.SetData("지불조건상세", txt_COND_PAY_DLV.Text);
            //rptHelper.SetData("가격조건", cbo_COND_PRICE.Text);


            //rptHelper.SetData("도착지", txtARRIVER);
            //rptHelper.SetData("선적지", txt_LOADING.Text.Trim());

            #endregion

            if (m_sEnv_Prt_Option != "000")
            {
                pur.P_PU_PRINT_OPTION dlg = new pur.P_PU_PRINT_OPTION(txtDtPo);
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    txtDtPo = dlg.gstr_return;
            }

            txtARRIVER = txt_ARRIVER.Text.Trim();
            //Dictionary<string, string> dic = new Dictionary<string, string>();

            dic["발주번호"] = tb_NO_PO.Text;
            dic["발주일자"] = txtDtPo;
            dic["공장"] = cbo_CD_PLANT.Text;
            dic["담당자"] = tb_NO_EMP.CodeName;
            dic["환율"] = tb_NM_EXCH.Text;
            dic["환정보"] = cbo_NM_EXCH.Text;
            dic["거래처"] = tb_CD_DEPT.Text;
            dic["지급조건"] = cbo_PAYment.Text;
            dic["과세구분"] = cbo_FG_TAX.Text;
            dic["단가유형"] = cbo_FG_UM.Text;
            dic["부가세율"] = tb_TAX.Text;
            if (rbtn_All.Checked)
                dic["계산서처리구분"] = rbtn_All.Text;
            else
                dic["계산서처리구분"] = rbtn_PRI.Text;

            dic["부가세여부"] = cbo_TP_TAX.Text;
            dic["비고"] = tb_DC.Text;


            dic["사업자등록번호"] = NO_BIZAREA_temp;
            dic["사업자등록번호1"] = NO_COMPANY_temp;

            dic["사업장명"] = ls_nm_bizarea;
            dic["비고2"] = tb_DC_RMK2.Text;

            dic["운송방법"] = cbo_TP_TRANSPORT.Text;
            dic["지불조건"] = cbo_COND_PAY.Text;
            dic["지불조건상세"] = txt_COND_PAY_DLV.Text;
            dic["가격조건"] = cbo_COND_PRICE.Text;

            dic["도착지"] = txtARRIVER;
            dic["선적지"] = txt_LOADING.Text.Trim();
            dic["DC_RMK_TEXT"] = txt_DC_RMK_TEXT.Text;
            dic["가격조건"] = D.GetString(cbo_COND_PRICE.Text);
            dic["선적조건"] = D.GetString(cbo_COND_SHIPMENT.Text);
            dic["운임구분"] = D.GetString(cbo_FREIGHT_CHARGE.Text);
            dic["DC_RMK_TEXT2"] = txt_DC_RMK_TEXT2.Text;
            dic["STND_PAY"] = cbo_stnd_pay.Text;


            dic["지불조건일자"] = txt_cond_days.Text;
            dic["원산지"] = cbo_origin.Text;
            dic["대행사"] = tb_NM_AGENCY.CodeValue;
            dic["대행사명"] = tb_NM_AGENCY.CodeName;

            //총액, 부가세 구하기
            decimal AM_H = (decimal)_flexD.DataTable.Compute("SUM(AM)", "NO_PO ='" + D.GetString(_header.CurrentRow["NO_PO"]) + "'");
            decimal H_VAT = (decimal)_flexD.DataTable.Compute("SUM(VAT)", "NO_PO ='" + D.GetString(_header.CurrentRow["NO_PO"]) + "'");
            decimal AM_EX_H = (decimal)_flexD.DataTable.Compute("SUM(AM_EX)", "NO_PO ='" + D.GetString(_header.CurrentRow["NO_PO"]) + "'");
            decimal AM_PRE_H = (decimal)_flexD.DataTable.Compute("SUM(AM_PRE)", "NO_PO ='" + D.GetString(_header.CurrentRow["NO_PO"]) + "'");
            decimal HAP_H = AM_H + H_VAT;

            dic["AM_H"] = D.GetString(AM_H);
            dic["AM_EX_H"] = D.GetString(AM_EX_H);
            dic["H_VAT"] = D.GetString(H_VAT);
            dic["HAP_H"] = D.GetString(HAP_H);
            dic["AM_PRE_H"] = D.GetString(AM_PRE_H);

            dic["운송비용"] = D.GetString(cur운송비용.DecimalValue);
            dic["인도조건"] = D.GetString(txt인도조건.Text);
            dic["인도기한"] = D.GetString(txt인도기한.Text);
            dic["유효기일"] = D.GetString(txt유효기일.Text);
            dic["포장형태"] = D.GetString(cbo포장형태.Text);
            dic["공급자"] = D.GetString(ctx공급자.CodeName);
            dic["제조사"] = D.GetString(ctx제조사.CodeName);
            dic["검사정보"] = D.GetString(txt검사정보.Text);
            dic["필수서류"] = D.GetString(txt필수서류.Text);

            dic["COND_PAY_LDV"] = D.GetString(txt_COND_PAY_DLV.Text);

            dic["발주연도"] = Date_convert_Text("YEAR", tb_DT_PO.Text);
            dic["발주월"] = Date_convert_Text("MON", tb_DT_PO.Text);
            dic["발주일"] = Date_convert_Text("DAY", tb_DT_PO.Text);
            dic["NO_COUNT"] = NO_COUNT;
            dic["NO_ORDER"] = txt오더번호.Text;

            if (Global.MainFrame.ServerKey.Contains("HDWIA2") || Global.MainFrame.ServerKey.Contains("HDWIA") || Global.MainFrame.ServerKey.Contains("DZSQL"))
            {
                dic["부서장"] = D.GetString(ds.Tables[0].Rows[0]["NM_EMPMNG"]);
                dic["부서장영문직위명"] = D.GetString(ds.Tables[0].Rows[0]["EN_DUTY_RANK"]);

                DataTable dtSP = _biz.search_partner(D.GetString(ds.Tables[0].Rows[0]["CD_AGENCY"]));

                if (dtSP != null && dtSP.Rows.Count > 0)
                {
                    dic["대행사주소"] = D.GetString(dtSP.Rows[0]["DC_ADS1_H"]);
                    dic["대행사상세주소"] = D.GetString(dtSP.Rows[0]["DC_ADS1_D"]);
                    dic["대행사담당자"] = D.GetString(dtSP.Rows[0]["CD_EMP_PARTNER"]);
                    dic["대행사전화번호"] = D.GetString(dtSP.Rows[0]["NO_TEL"]);
                    dic["대행사팩스"] = D.GetString(dtSP.Rows[0]["NO_FAX"]);
                    dic["대행사이메일"] = D.GetString(dtSP.Rows[0]["E_MAIL"]);
                }
            }


            if (Global.MainFrame.ServerKey.Contains("SINJINSM"))
            {
                DataTable dtSG = _biz.Check_EMP_SG(D.GetString(tb_NO_EMP.CodeValue));

                if (dtSG != null && dtSG.Rows.Count > 0)
                {
                    dic["DC_RMK_EMP"] = D.GetString(dtSG.Rows[0]["DC_RMK"]);
                }
            }

            foreach (string key in dic.Keys)
            {
                rptHelper.SetData(key, dic[key]);
            }


            if (Tp_print == "WONIK") //원익전용
            {
                DataTable dt = _biz.DataSearch_GW_RPT_ONLY(new object[] { Global.MainFrame.LoginInfo.CompanyCode, tb_NO_PO.Text, D.GetString(테이블구분.PU_POH.GetHashCode()), tb_DT_PO.Text, "WONIK" });
                rptHelper.SetDataTable(dt);
            }
            else if (Tp_print == "SAMTECH")
            {
                DataTable dt = _biz.DataSearch_GW_RPT_ONLY(new object[] { Global.MainFrame.LoginInfo.CompanyCode, tb_NO_PO.Text, D.GetString(테이블구분.PU_POH.GetHashCode()), tb_DT_PO.Text, Tp_print });
                rptHelper.SetDataTable(dt);

            }
            else if (Global.MainFrame.ServerKey.Contains("CNP"))          //20140409 추가
            {
                SetPrint차앤박();
            }
            //else if (Global.MainFrame.ServerKey.Contains("DEMAC"))          
            //{
            //    DataTable dt = _biz.DataSearch_GW_RPT_ONLY(new object[] { Global.MainFrame.LoginInfo.CompanyCode, tb_NO_PO.Text, D.GetString(테이블구분.PU_POH.GetHashCode()), tb_DT_PO.Text, "DEMAC" });
            //    rptHelper.SetDataTable(dt);
            //}
            else //RDF
            {
                rptHelper.SetDataTable(ds.Tables[0], 1);
                rptHelper.SetDataTable(ds.Tables[0], 2);
                rptHelper.SetDataTable(ds.Tables[0], 3);
                rptHelper.SetDataTable(ds.Tables[1], 4);

                if (sPUSU == "100")
                {
                    DataTable dtSU = _biz.Print_Detail(tb_NO_PO.Text);
                    rptHelper.SetDataTable(dtSU, 5);
                }
            }

            if (checkprint == true)
                return;

            StringBuilder text = new StringBuilder();
            string title = D.GetString(tb_NO_EMP.CodeName) + "/" + D.GetString(tb_NO_PO.Text) + "/" + D.GetString(ds.Tables[0].Rows[0]["CD_PJT"]) + "구매발주가 등록되었습니다.";

            foreach (DataRow dr in _flexD.DataTable.Rows)
            {//DD처리를 하여야할때는 "-" + DD(품목코드) + ":" 으로 바꾸여사용하기바랍니다.  '-', ':' 이것이 폰트를 바꿀 표식입니다.
                string msg = "품목코드: " + D.GetString(dr["CD_ITEM"]) + " / 품목명: " + D.GetString(dr["NM_ITEM"]) + " / 규격: " + D.GetString(dr["STND_ITEM"]) + " / 단위: " + D.GetString(dr["UNIT_IM"]) +
                             " / 수량: " + D.GetDecimal(dr["QT_PO_MM"]).ToString("#,###,##0.####") + " / 단가: " + D.GetDecimal(dr["UM_EX_PO"]).ToString("#,###,##0.####") + "/ 금액: " + D.GetDecimal(dr["AM_EX"]).ToString("#,###,##0.####") + " / 프로젝트코드: " + D.GetString(dr["CD_PJT"]) + " / 프로젝트명: " + D.GetString(dr["NM_PJT"]);
                text.AppendLine(msg);
                text.AppendLine("\n\n");

            }

            // 메일도움창에 보낼 파라미터셋팅 0번 제목, 1번 받을사람 이메일주소, 3번은 내용
            string[] str_histext = new string[3];
            str_histext[0] = title;
            str_histext[1] = Settings1.Default.MAIL_ADD;
            str_histext[2] = text.ToString();

            if (D.GetString(_flexD["NO_APP"]) != string.Empty)
            {
                DataTable groupby_no_app = _flexD.DataTable.DefaultView.ToTable(true, new string[] { "NO_APP" }); //여러 품의건이 있을 수 있기 때문에...
                DataTable dt = _biz.getMail_Adress(groupby_no_app);
                DataTable dt_emp = dt.DefaultView.ToTable(true, new string[] { "NO_EMP", "NM_KOR", "NO_EMAIL" }); //여러 품의건에 담당자가 같을 수도 있기때문에.. 
                foreach (DataRow dr in dt_emp.Rows)
                {
                    str_histext[1] += D.GetString(dr["NM_KOR"]) + "|" + D.GetString(dr["NO_EMAIL"]) + "|" + "N" + "?"; //담당자는 저장 하지 않기위해 N을 붙여줌.
                }

            }

            P_MF_EMAIL mail = new P_MF_EMAIL(new string[] { tb_NM_PARTNER.CodeValue }, "R_PU_PO_REG2_0", new ReportHelper[] { rptHelper }, dic, "구매발주서", str_histext);
            mail.ShowDialog();
            Settings1.Default.MAIL_ADD = mail._str_rt_data[0];
            Settings1.Default.Save();
 
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
        finally
        {
            ToolBarSaveButtonEnabled = false;
        }

    }

    #region SetPrint차앤박
    private void SetPrint차앤박()
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic["회사명"] = D.GetString(_header.CurrentRow["NM_COMPANY"]);
        dic["회사주소"] = D.GetString(_header.CurrentRow["COM_ADS"]);
        dic["회사전화"] = D.GetString(_header.CurrentRow["NO_TEL"]);
        dic["회사팩스"] = D.GetString(_header.CurrentRow["NO_FAX"]);
        dic["문서번호"] = D.GetString(_header.CurrentRow["NO_PO"]);
        dic["발주일자"] = D.GetString(_header.CurrentRow["DT_PO"]);
        dic["발신"] = D.GetString(_header.CurrentRow["NM_COMPANY"]);
        dic["발주담당"] = D.GetString(_header.CurrentRow["PU_NM_KOR"]);
        dic["발주담당자연락처"] = D.GetString(_header.CurrentRow["E_NO_TEL"]);
        dic["수신"] = D.GetString(_header.CurrentRow["LN_PARTNER"]);
        dic["연락처"] = D.GetString(_header.CurrentRow["P_NO_TEL"]);
        dic["팩스"] = D.GetString(_header.CurrentRow["P_NO_FAX"]);
        dic["담당"] = D.GetString(_header.CurrentRow["CD_EMP_PARTNER"]);
        dic["특이사항"] = D.GetString(_header.CurrentRow["DC50_PO"]);

        foreach (string key in dic.Keys)
        {
            rptHelper.SetData(key, dic[key]);
        }
        rptHelper.SetDataTable(_flexD.DataTable);
    }
    #endregion
    #region SetPrint필옵틱스

    private void SetPrint필옵틱스()
    {
        string FG_GW = "001";
        if (ShowMessage("ASS'Y 별로 인쇄 하시겠습니까?", "QY2") == DialogResult.Yes) FG_GW = "002";

        string NO_APP = D.GetString(_flexD["NO_APP"]);

        DataSet ds = DBHelper.GetDataSet("UP_PU_Z_PHIL_PO_PRINT", new object[] { MA.Login.회사코드, NO_APP, tb_NO_PO.Text });
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            DataRow row = ds.Tables[0].Rows[0];

            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic["품의번호"] = D.GetString(row["NO_APP"]);
            dic["작성일자"] = D.GetString(row["DT_WRITE"]);
            dic["담당부서"] = D.GetString(row["NM_DEPT"]);
            dic["담당자"] = D.GetString(row["APP_EMP"]);
            dic["지급조건"] = D.GetString(row["NM_FG_PAYMENT"]);
            dic["가격조건"] = D.GetString(row["NM_COND_PRICE"]);
            dic["발주처"] = D.GetString(row["LN_PARTNER"]);
            dic["납기일"] = D.GetString(row["DT_LIMIT"]);
            dic["프로젝트코드"] = D.GetString(row["CD_PJT"]);
            dic["프로젝트명"] = D.GetString(row["NM_PJT"]);
            dic["고객사명"] = D.GetString(row["LN_SP_PARTNER"]);
            dic["창고"] = D.GetString(row["NM_SL"]);
            dic["요청자"] = D.GetString(row["PR_NM_KOR"]);
            dic["비고1"] = D.GetString(row["DC_RMK"]);
            dic["비고2"] = D.GetString(row["DC_RMK2"]);
            dic["지출시행일자"] = D.GetString(row["DC_RMK3"]);
            dic["예금주"] = D.GetString(row["NM_DEPOSIT"]);
            dic["계좌번호"] = D.GetString(row["NO_DEPOSIT"]);
            dic["공급가액"] = D.GetString(row["AM"]);
            dic["부가세"] = D.GetString(row["VAT"]);
            dic["합계"] = D.GetString(row["TOT_AM"]);
            dic["환종"] = D.GetString(row["NM_EXCH"]);

            dic["발주일자"] = D.GetString(row["DT_PO"]);
            dic["발주번호"] = D.GetString(row["NO_PO"]);
            dic["발주담당자"] = D.GetString(row["PO_NO_EMP"]);
            dic["발주담당자명"] = D.GetString(row["PO_NM_KOR"]);
            dic["구매그룹전화번호"] = D.GetString(row["PU_NO_TEL"]);
            dic["구매그룹팩스번호"] = D.GetString(row["PU_NO_FAX"]);
            dic["발주형태코드"] = D.GetString(row["CD_TPPO"]);
            dic["발주형태명"] = D.GetString(row["NM_TPPO"]);
            dic["발주거래처코드"] = D.GetString(row["PO_CD_PARTNER"]);
            dic["발주거래처명"] = D.GetString(row["PO_LN_PARTNER"]);
            dic["발주납기일"] = D.GetString(row["PO_DT_LIMIT"]);
            dic["발주창고코드"] = D.GetString(row["PO_CD_SL"]);
            dic["발주창고명"] = D.GetString(row["PO_NM_SL"]);
            dic["발주지급조건코드"] = D.GetString(row["FG_TRANS"]);
            dic["발주지급조건명"] = D.GetString(row["NM_FG_TRANS"]);
            dic["발주거래처담당자"] = D.GetString(row["CD_EMP_PARTNER"]);
            dic["발주거래처핸드폰번호"] = D.GetString(row["NO_HPEMP_PARTNER"]);
            dic["발주거래처전화번호"] = D.GetString(row["PO_NO_TEL"]);
            dic["발주상단비고1"] = D.GetString(row["PO_DC_RMK_TEXT"]);


            decimal 총수량 = 0;
            decimal 공급합계 = 0;

            if (FG_GW == "001")
            {
                if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow row1 in ds.Tables[1].Rows)
                    {
                        총수량 = 총수량 + D.GetDecimal(row1["QT_APP"]);
                        공급합계 = 공급합계 + D.GetDecimal(row1["AM_EX"]);
                    }
                    rptHelper.SetDataTable(ds.Tables[1]);
                }
            }
            else if (FG_GW == "002")
            {
                if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow row2 in ds.Tables[2].Rows)
                    {
                        총수량 = 총수량 + 1;
                        공급합계 = 공급합계 + D.GetDecimal(row2["AM_EX"]);
                    }
                    rptHelper.SetDataTable(ds.Tables[2]);
                }
            }

            dic["총수량"] = D.GetString(총수량);
            dic["공급합계"] = D.GetString(공급합계);

            foreach (string key in dic.Keys)
            {
                rptHelper.SetData(key, dic[key]);
            }
        }
    }

    #endregion

    bool rptHelper_Printing(object sender, PrintArgs args)
    {
        try
        {
            if (args.Action == PrintActionEnum.ON_PREPARE_PRINT)
            {
                if (args.scriptFile.ToUpper() == "R_PU_PO_REG2_001_WONIK.RDF" || args.scriptFile.ToUpper() == "R_PU_PO_REG2_001_WONIK_0.RDF")
                    Tp_print = "WONIK";
                else if (args.scriptFile.ToUpper() == "R_PU_PO_REG2_002_SAMTECH.RDF")
                    Tp_print = "SAMTECH";
                else
                    Tp_print = "RDF";

                if (Global.MainFrame.ServerKey.Contains("PHILOPTICS") )
                {
                    SetPrint필옵틱스();
                }
                else if (Global.MainFrame.ServerKey.Contains("CNP"))
                {
                    SetPrint차앤박();
                }
                else SetPrint(true);

            }
            return true;
        }
        catch (Exception ex)
        {
            Global.MainFrame.MsgEnd(ex);
            return true;
        }
    }

    private string Date_convert_Text(string fg_date, string dt_po)
    {
        try
        {
            string rt_value = "";
            if (fg_date == "YEAR")
            {
                rt_value = dt_po.Substring(0, 4);
            }
            else if (fg_date == "MON")
            {
                switch(dt_po.Substring(4,2))
                {
                    case "01" : 
                          rt_value = "January";
                        break;
                    case "02" : 
                          rt_value = "February";
                        break;
                    case "03" : 
                          rt_value = "March";
                        break;
                    case "04" : 
                          rt_value = "April";
                        break;
                    case "05" : 
                          rt_value = "May";
                        break;
                    case "06" : 
                          rt_value = "June";
                        break;
                    case "07" : 
                          rt_value = "July";
                        break;
                    case "08" : 
                          rt_value = "August";
                        break;
                    case "09" : 
                          rt_value = "September";
                        break;
                    case "10" : 
                          rt_value = "October";
                        break;
                    case "11" : 
                          rt_value = "November";
                        break;
                    case "12" : 
                          rt_value = "December";
                        break;


                }
            }
            else if (fg_date == "DAY")
            {
                rt_value = dt_po.Substring(6, 2);
            }
            return rt_value;

        }
        catch(Exception ex)
        {
            MsgEnd(ex);
            return null;
        }
    }

    #endregion

    #region ♣ 종료(사용자 Default Settings)
    //원래는 화면이 닫힐때 사용자가 정의한 값을 읽어서 저장해야하지만 
    //화면이 닫히는 시점을 알기 어려움으로 해당 컨트롤에 변경이 일어 났을때 바로 저장 시킨다.
    public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
    {
        try
        {
            Settings1.Default.CD_PURGRP_SET = _header.CurrentRow["CD_PURGRP"].ToString();
            Settings1.Default.CD_TPPO_SET = _header.CurrentRow["CD_TPPO"].ToString();
            Settings1.Default.FG_PAYMENT_SET = _header.CurrentRow["FG_PAYMENT"].ToString();
            Settings1.Default.CD_EXCH = _header.CurrentRow["CD_EXCH"].ToString();
            Settings1.Default.RT_EXCH = D.GetDecimal(_header.CurrentRow["RT_EXCH"]);
            Settings1.Default.DC_RMK_TEXT = D.GetString(txt_DC_RMK_TEXT.Text);
            Settings1.Default.DC_RMK_TEXT2 = D.GetString(txt_DC_RMK_TEXT2.Text);
            Settings1.Default.TP_UM_TAX = _header.CurrentRow["TP_UM_TAX"].ToString();
            //꼭 저장을 해줘야 한다.
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

    #region ♣ 화면내버튼이벤트

    #region ★ 컨트롤 이벤트 2011.04.08 추가 프로젝트도움창

    #region -> Control_QueryBefore

    private void Control_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
    {
        BpCodeTextBox bp_Control = sender as BpCodeTextBox;

        switch (e.HelpID)
        {
            case HelpID.P_USER:
                if (bp_Control.UserHelpID == "H_SA_PRJ_SUB")
                {
                    e.HelpParam.P41_CD_FIELD1 = "프로젝트";             //도움창 이름 찍어줄 값
                }
                break;
        }
    }

    #endregion

    #endregion

    #region -> 도움창 호출후 변경 이벤트(OnBpControl_QueryAfter)

    private void OnBpControl_QueryAfter(object sender, BpQueryArgs e)
    {
        try
        {
            if (sender is BpCodeTextBox)
            {
                string str_data = string.Empty;
                switch (((BpCodeTextBox)sender).Name)
                {
                    case "tb_NO_EMP":
                        _header.CurrentRow["CD_DEPT"] = e.HelpReturn.Rows[0]["CD_DEPT"];
                        _header.CurrentRow["NM_DEPT"] = e.HelpReturn.Rows[0]["NM_DEPT"];
                        tb_CD_DEPT.Text = _header.CurrentRow["NM_DEPT"].ToString();
                        break;
                    case "tb_NM_PURGRP":
                        if (e.CodeValue != null)
                        {
                            _header.CurrentRow["PURGRP_NO_TEL"] = e.HelpReturn.Rows[0]["NO_TEL"];
                            _header.CurrentRow["PURGRP_NO_FAX"] = e.HelpReturn.Rows[0]["NO_FAX"];
                            _header.CurrentRow["PURGRP_E_MAIL"] = e.HelpReturn.Rows[0]["E_MAIL"];

                            _header.CurrentRow["PO_PRICE"] = "N";

                            //ShowMessage("tb_NM_PURGRP !");
                            str_data = e.HelpReturn.Rows[0]["CD_PURGRP"].ToString();
                            //다이렉트로 seelct해오기:구매그룹의 소속조직에대한 단가통제  
                            DataTable dt_exc = Global.MainFrame.FillDataTable(
                                    " SELECT O.PO_PRICE " +
                                    "   FROM MA_PURGRP G LEFT OUTER JOIN MA_PURORG O " +
                                    "     ON   G.CD_COMPANY = O.CD_COMPANY " +
                                    "    AND   G.CD_PURORG  = O.CD_PURORG " +
                                    " WHERE G.CD_COMPANY = '" + this.LoginInfo.CompanyCode + "'" +
                                    "  AND G.CD_PURGRP  = '" + str_data + "'");

                            if (dt_exc.Rows.Count > 0)
                            {   // N:안함 Y:함 (null이거나 ''은 N 으로 치환) 
                                if (dt_exc.Rows[0]["PO_PRICE"] != System.DBNull.Value && dt_exc.Rows[0]["PO_PRICE"].ToString().Trim() != String.Empty)
                                {
                                    _header.CurrentRow["PO_PRICE"] = dt_exc.Rows[0]["PO_PRICE"].ToString().Trim();
                                }
                            }

                            SetCC(0, str_data);
                        }
                        else
                        {
                            tb_NM_PURGRP.CodeValue = "";
                            tb_NM_PURGRP.CodeName = "";
                            _header.CurrentRow["PURGRP_NO_TEL"] = "";
                            _header.CurrentRow["PURGRP_NO_FAX"] = "";
                            _header.CurrentRow["PURGRP_E_MAIL"] = "";
                            _header.CurrentRow["PO_PRICE"] = "N";
                            _header.CurrentRow["CD_CC_PURGRP"] = "";
                            _header.CurrentRow["NM_CC_PURGRP"] = "";

                        }

                        //ShowMessage(_header.CurrentRow["CD_PURGRP"].ToString() + " " + _header.CurrentRow["PO_PRICE"].ToString());
                        break;
                    case "txt_NoProject":
                        DataRow[] drL = _flexD.DataTable.Select();
                        if (_flexD.HasNormalRow)
                        {
                            foreach (DataRow dr in drL)
                            {
                                dr["CD_PJT"] = ctx프로젝트.CodeValue;
                                dr["NM_PJT"] = ctx프로젝트.CodeName;
                            }
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

                        if (Global.MainFrame.ServerKeyCommon == "UNIPOINT")
                        {
                            DataTable dtPjt = _biz.Get_Project_Detail(D.GetString(e.HelpReturn.Rows[0]["NO_PROJECT"]));

                            if (dtPjt != null || dtPjt.Rows.Count > 0)
                            {
                                s_CD_PARTNER_PJT = D.GetString(dtPjt.Rows[0]["CD_PARTNER"]);
                                s_NM_PARTNER_PJT = D.GetString(dtPjt.Rows[0]["LN_PARTNER"]);
                                s_NO_EMP_PJT = D.GetString(dtPjt.Rows[0]["NO_EMP"]);
                                s_NM_EMP_PJT = D.GetString(dtPjt.Rows[0]["NM_KOR"]);
                                s_END_USER = D.GetString(dtPjt.Rows[0]["END_USER"]);
                            }
                        }

                        break ;
                    case "bp_ARRIVER":
                        txt_ARRIVER.Text = D.GetString(e.HelpReturn.Rows[0]["NM_SYSDEF"]);
                        break;
                    case "bp_LOADING":
                        txt_LOADING.Text = D.GetString(e.HelpReturn.Rows[0]["NM_SYSDEF"]);
                        break;

                    case "tb_NM_PARTNER":
                        if ((MainFrameInterface.ServerKeyCommon == "KPCI" || Global.MainFrame.ServerKeyCommon == "SQL_") && m_tab_poh.TabPages.Contains(tabPage7))
                        {
                            DataTable dt = _biz.search_partner(D.GetString(e.HelpReturn.Rows[0]["CD_PARTNER"]));

                            if (dt != null && dt.Rows.Count > 0)
                            {
                                if (D.GetString(dt.Rows[0]["FG_PAYBILL"]) != string.Empty)
                                    cbo_FG_PAYBILL_IV.SelectedValue = D.GetString(dt.Rows[0]["FG_PAYBILL"]);
                            }
                        }
                        break;
                }
            }
            if (sender is BpCodeNTextBox)
            {
                switch (((BpCodeNTextBox)sender).Name)
                {
                    case "tb_FG_PO_TR":
                        _header.CurrentRow["FG_TRANS"] = e.HelpReturn.Rows[0]["FG_TRANS"];
                        _header.CurrentRow["FG_TPRCV"] = e.HelpReturn.Rows[0]["FG_TPRCV"];
                        _header.CurrentRow["FG_TPPURCHASE"] = e.HelpReturn.Rows[0]["FG_TPPURCHASE"];
                        _header.CurrentRow["YN_AUTORCV"] = e.HelpReturn.Rows[0]["YN_AUTORCV"];
                        _header.CurrentRow["YN_RCV"] = e.HelpReturn.Rows[0]["YN_RCV"];
                        _header.CurrentRow["YN_RETURN"] = e.HelpReturn.Rows[0]["YN_RETURN"];
                        _header.CurrentRow["YN_SUBCON"] = e.HelpReturn.Rows[0]["YN_SUBCON"];
                        _header.CurrentRow["YN_IMPORT"] = e.HelpReturn.Rows[0]["YN_IMPORT"];
                        _header.CurrentRow["YN_ORDER"] = e.HelpReturn.Rows[0]["YN_ORDER"];
                        _header.CurrentRow["YN_REQ"] = e.HelpReturn.Rows[0]["YN_REQ"];
                        _header.CurrentRow["YN_AM"] = e.HelpReturn.Rows[0]["YN_AM"];
                        _header.CurrentRow["NM_TRANS"] = e.HelpReturn.Rows[0]["NM_TRANS"];
                        _header.CurrentRow["FG_TAX"] = e.HelpReturn.Rows[0]["FG_TAX"];
                        _header.CurrentRow["TP_GR"] = e.HelpReturn.Rows[0]["TP_GR"];

                        _header.CurrentRow["CD_CC_TPPO"] = e.HelpReturn.Rows[0]["CD_CC"];
                        _header.CurrentRow["NM_CC_TPPO"] = _biz.GetCCCodeSearch(e.HelpReturn.Rows[0]["CD_CC"].ToString());
                        거래구분(e.HelpReturn.Rows[0]["FG_TRANS"].ToString());

                        Setting_pu_poh_sub();
                        if (m_tab_poh.TabPages.Contains(tabPage7))
                        {
                            dtp_DT_DUE_IV.Text = Global.MainFrame.GetStringToday;
                            dtp_DT_PAY_PRE_IV.Text = Global.MainFrame.GetStringToday;
                            dtp_DT_PROCESS_IV.Text = Global.MainFrame.GetStringToday;
                            cbo_FG_PAYBILL_IV.SelectedValue = "";
                            cbo_CD_DOCU_IV.SelectedValue = "";
                        }
                        

                        DataTable dt = _biz.GetYN_SU(D.GetString(e.HelpReturn.Rows[0]["CD_TPPO"]));
                        if (D.GetString(dt.Rows[0]["YN_SU"]) == "Y")
                            _flexDD.Enabled = true;
                        else
                            _flexDD.Enabled = false;
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
    }

    #endregion

    #region -> 도움창 Clear 이벤트(Control_CodeChanged)

    private void Control_CodeChanged(object sender, EventArgs e)
    {
        try
        {
            switch (((BpCodeTextBox)sender).Name)
            {
                case "tb_NO_EMP":
                    if (tb_NO_EMP.CodeValue == string.Empty)
                    {
                        _header.CurrentRow["CD_DEPT"] = "";
                        _header.CurrentRow["NM_DEPT"] = "";
                        tb_CD_DEPT.Text = "";
                    }
                    break;
                case "tb_NM_PURGRP":
                    if (tb_NM_PURGRP.CodeValue == string.Empty)
                    {
                        _header.CurrentRow["PURGRP_NO_TEL"] = "";
                        _header.CurrentRow["PURGRP_NO_FAX"] = "";
                        _header.CurrentRow["PURGRP_E_MAIL"] = "";
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

    #region -> 도움창 호출전 세팅 이벤트(OnBpControl_QueryBefore)

    private void OnBpControl_QueryBefore(object sender, BpQueryArgs e)
    {
        try
        {
            switch (D.GetString(e.ControlName))
            {
                case "tb_FG_PO_TR":
           
                    e.HelpParam.P61_CODE1 = "N";
           
                    if (_반품발주 == "Y")
                        e.HelpParam.P41_CD_FIELD1 = "Y";    /*YN_RETURN 반품 포함해서 보이기*/
                    break; 

                case "bp_ARRIVER":
                    e.HelpParam.P41_CD_FIELD1 = "PU_C000046";
                    break;
                case "bp_LOADING":
                    e.HelpParam.P41_CD_FIELD1 = "PU_C000047";
                    break;
                case"bp_CDSL":
                    if (D.GetString(cbo_CD_PLANT.SelectedValue) == string.Empty)
                    {
                        ShowMessage(공통메세지._은는필수입력항목입니다, DD("공장"));
                        return;
                    }
                    e.HelpParam.P09_CD_PLANT = D.GetString(cbo_CD_PLANT.SelectedValue);
                    break;
            }
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
    }

    #endregion

    #region -> 추가 버튼 클릭 이벤트(추가_Click)

    private void 추가_Click(object sender, EventArgs e)
    {
        try
        {
            if (!btn_insert.Enabled)
                return;

            if (tabControlExt1.SelectedIndex == 0)
            {

                호출여부 = true;

                if (!HeaderCheck(0)) return; //필수항목 검사                

                if (_flexD.DataTable == null) return;

                ControlButtonEnabledDisable((Control)sender, true);
                cbo_CD_PLANT.Enabled = false;

                decimal MaxSeq = _flexD.GetMaxValue("NO_LINE");
                MaxSeq++;

                _flexD.Rows.Add();
                _flexD.Row = _flexD.Rows.Count - 1;
                if (tb_NO_PO.Text != string.Empty)
                    _flexD["NO_PO"] = tb_NO_PO.Text;
                _flexD["NO_LINE"] = MaxSeq;
                _flexD["CD_PLANT"] = cbo_CD_PLANT.SelectedValue.ToString();
                _flexD["CD_PJT"] = ctx프로젝트.CodeValue;
                _flexD["NM_PJT"] = ctx프로젝트.CodeName;
                _flexD["NO_PR"] = "";
                _flexD["CD_EXCH"] = cbo_NM_EXCH.SelectedValue.ToString();
                //_flexD["NM_SYSDEF"] = _ComfirmState;
                //_flexD["FG_POST"]는 _biz.search에서 기본값으로 'O'를 넣어준다.
                _flexD["NM_SYSDEF"] = _biz.GetGubunCodeSearch("PU_C000009", _flexD["FG_POST"].ToString());//_ComfirmState;

                if (bStandard)
                {
                    if (Global.MainFrame.ServerKey == "SINJINSM")
                    {
                        _flexD["RATE_VAT"] = tb_TAX.DecimalValue;
                    }
                }

                DataTable dt = (DataTable)cbo_NM_EXCH.DataSource;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["CODE"].ToString() == cbo_NM_EXCH.SelectedValue.ToString())
                    {
                        _flexD["NM_EXCH"] = dr["NAME"].ToString();
                        break;
                    }
                }

                // 2010.01.12 납기일 기본값
                if (tb_DT_LIMIT.Text == string.Empty)
                {
                    if (_flexD.Row != _flexD.Rows.Fixed)
                        //    _flexD["DT_LIMIT"] =  Global.MainFrame.GetStringToday;
                        //else
                        _flexD["DT_LIMIT"] = _flexD[_flexD.Row - 1, "DT_LIMIT"];
                }
                else
                    _flexD["DT_LIMIT"] = tb_DT_LIMIT.Text;

                //  _flexD["DT_PLAN"] = _flexD["DT_LIMIT"]; 


                FillPol(_flexD.Row);
                _flexD.AddFinished();
                _flexD.Col = _flexD.Cols.Fixed;
                _flexD.Focus();
                SetHeadControlEnabled(false, 1);
            }
            else if (tabControlExt1.SelectedIndex == 1)
            {
                if (!_flexD.HasNormalRow) return;
                if (_flexD["CD_ITEM"].ToString() == "")
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, DD("사급자재의 상위품목정보"));
                }


                if (D.GetString(_flexD["FG_POST"]) != "O" && _flexD.RowState() != DataRowState.Added)
                {
                    ShowMessage("이미 확정되었거나 종결처리된 품목에는 추가 할 수 없습니다.");
                    return;
                }

                decimal MaxValue = _flexDD.GetMaxValue("NO_LINE");

                _flexDD.Rows.Add();
                _flexDD.Row = _flexDD.Rows.Count - 1;
                _flexDD["CD_PLANT"] = _flexD["CD_PLANT"];
                _flexDD["NO_PO"] = _flexD["NO_PO"];
                _flexDD["NO_POLINE"] = _flexD["NO_LINE"];
                _flexDD["NO_LINE"] = ++MaxValue;
                _flexDD["QT_NEED_UNIT"] = 1d;
                _flexDD["QT_NEED"] = 1d;
                _flexDD["QT_LOSS"] = 1d;
                _flexDD.AddFinished();
                _flexDD.Col = _flexD.Cols.Fixed;
            }
         
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
    }

    #endregion

    #region -> 삭제 버튼 클릭 이벤트(삭제_Click)

    private void 삭제_Click(object sender, EventArgs e)
    {
        try
        {
            if (tabControlExt1.SelectedIndex == 0)
            {
                if (!_flexD.HasNormalRow)
                {
                    ctx프로젝트.Enabled = true; btn적용.Enabled = true;
                    bp_CDSL.Enabled = true; btn_SL_apply.Enabled = true;
                    return;
                }

                if (sPUSU == "100")
                {
                    DataRow[] dr = _flexDD.DataTable.Select("NO_PO ='" + D.GetString(_flexD["NO_PO"]) + "' AND NO_POLINE='" + D.GetString(_flexD["NO_LINE"]) + "'");
                    foreach (DataRow drD in dr)
                    {
                        drD.Delete();
                    }
                }

                if ((D.GetString(_flexD["YN_ORDER"]) == "Y" && D.GetString(_flexD["FG_POST"]) == "R" && Global.MainFrame.ServerKey != "HANSU"))
                {
                    if (ShowMessage(DD("발주상태가 확정입니다. 삭제하시겠습니까?"), "QY2") == DialogResult.Yes)
                        _flexD.Rows.Remove(_flexD.Row);
                    else
                        return;
                }
                else if (_flexD["FG_POST"].ToString() == "R")     
                    ShowMessage("발주상태가 미정일 경우에만 삭제가능합니다");
                else // 확정여부가 미정일 경우
                    _flexD.Rows.Remove(_flexD.Row);

                if (m_tab_poh.TabPages.Contains(tabPage7))
                    SUMFunction();

                //if (!_flexD.HasNormalRow)
                //{
                //    if (ShowMessage(공통메세지.자료를삭제하시겠습니까) == DialogResult.Yes)
                //    {
                //        bool Success = _biz.Delete(tb_NO_PO.Text);

                //        if (Success)
                //            ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
                //    }
                //}

            }
            else if (tabControlExt1.SelectedIndex == 1)
            {
                if (!_flexDD.HasNormalRow) return;

                if (D.GetString(_flexD["FG_POST"]) != "O" && _flexD.RowState() != DataRowState.Added)
                {
                    ShowMessage("발주상태가 미정일 경우에만 삭제가능합니다");
                    return;
                }

                DataRow[] dr = _flexDD.DataTable.Select("CHK = 'Y'", "", DataViewRowState.CurrentRows);
                if (dr == null || dr.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                _flexDD.Redraw = false;
                foreach (DataRow row in dr)
                    row.Delete();
                _flexDD.Redraw = true;
                _flexDD.IsDataChanged = true;
                Page_DataChanged(null, null);


            }
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
        finally
        {
            if (!_flexD.HasNormalRow)
            {
                cbo_CD_PLANT.Enabled = true;
                ControlButtonEnabledDisable(null, true);
                SetHeadControlEnabled(true, 1);
                btn_insert.Enabled = true;
                btn_RE_PR.Enabled = true;
            }
        }
    }

    #endregion

    #region -> 품목 전개 버턴 클릭 이벤트(품목전개_Click)

    /// <summary>
    /// 품목 전개 버턴 클릭 이벤트
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    private void 품목전개_Click(object sender, System.EventArgs e)
    {
        try
        {
            if (!HeaderCheck(0)) return;

            if (_flexD.DataTable == null) return;

            if (cbo_CD_PLANT.SelectedValue.ToString() == "")
            {
                ShowMessage(메세지.공장을먼저선택하십시오, "");
                return;
            }

            pur.P_PU_PO_ITEMEXPSUB m_dlg = new pur.P_PU_PO_ITEMEXPSUB(MainFrameInterface,
                cbo_CD_PLANT.SelectedValue.ToString(),
                tb_NM_PARTNER.CodeValue, tb_NM_PARTNER.CodeName, cbo_FG_UM.SelectedValue.ToString(),
                tb_DT_PO.Text, tb_DT_PO.Text, cbo_NM_EXCH.SelectedValue.ToString());

            if (m_dlg.ShowDialog(this) == DialogResult.OK)
            {
                _flexD.Redraw = false;

                if (m_dlg.gdt_return.Rows.Count > 0)
                {
                    SetITEMEXP(m_dlg.gdt_return);
                    SetHeadControlEnabled(false, 1);
                    ControlButtonEnabledDisable((Control)sender, true);

                }
            }
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
        finally
        {
            _flexD.Redraw = true;
        }
    }

    #endregion

    #region -> SetITEMEXP <-- 품목전개_Click 에서 호출

    /// <summary>
    /// 품목전개되면 그리드에 뿌려줌
    /// </summary>
    /// <param name="dt"></param>

    private void SetITEMEXP(DataTable dt)
    {
        if (dt != null && dt.Rows.Count > 0)
        {
            decimal MaxSeq = _flexD.GetMaxValue("NO_LINE");
            decimal ld_rate_exchg = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i].RowState == DataRowState.Deleted)
                    continue;

                _flexD.Rows.Add();
                _flexD.Row = _flexD.Rows.Count - 1;

                if (tb_NO_PO.Text != string.Empty)
                    _flexD["NO_PO"] = tb_NO_PO.Text;
                _flexD["DT_LIMIT"] = Global.MainFrame.GetStringToday;
                _flexD["DT_PLAN"] = _flexD["DT_LIMIT"];

                MaxSeq++;
                _flexD["NO_LINE"] = MaxSeq;
                _flexD["RT_PO"] = 1;
                _flexD["CD_SL"] = "";
                _flexD["NO_CONTRACT"] = "";
                _flexD["NO_RCV"] = ""; //UM_EXP
                _flexD["NO_PR"] = "";           // 구매요청
                _flexD["NO_APP"] = "";          // 품의
                _flexD["NO_CONTRACT"] = "";     // 계약
                _flexD["CD_PJT"] = ctx프로젝트.CodeValue;   // 프로젝트
                _flexD["NM_PJT"] = ctx프로젝트.CodeName;
                _flexD["CD_PLANT"] = dt.Rows[i]["CD_PLANT"];
                _flexD["CD_ITEM"] = dt.Rows[i]["CD_ITEM"];
                _flexD["NM_ITEM"] = dt.Rows[i]["NM_ITEM"];
                _flexD["CD_UNIT_MM"] = dt.Rows[i]["CD_UNIT_MM"];
                _flexD["STND_ITEM"] = dt.Rows[i]["STND_ITEM"];
                _flexD["UNIT_IM"] = dt.Rows[i]["UNIT_IM"];
                //_flexD["NM_SYSDEF"] = _ComfirmState;
                _flexD["NM_SYSDEF"] = _biz.GetGubunCodeSearch("PU_C000009", _flexD["FG_POST"].ToString());//_ComfirmState;
                _flexD["NM_CLS_ITEM"] = dt.Rows[i]["NM_CLS_ITEM"];
                FillPol(_flexD.Row);

                if (dt.Rows[i]["RATE_EXCHG"] == null)
                    ld_rate_exchg = 1;
                else
                {
                    ld_rate_exchg = _flexD.CDecimal(dt.Rows[i]["RATE_EXCHG"]);
                    if (ld_rate_exchg == 0) ld_rate_exchg = 1;
                }
                _flexD["RT_PO"] = ld_rate_exchg;
                _flexD["UM_EX_PO"] = _flexD.CDecimal(dt.Rows[i]["UM_ITEM"]);
                _flexD["UM_EX"] = _flexD.CDecimal(dt.Rows[i]["UM_ITEM"]) / ld_rate_exchg;
                _flexD["UM_P"] = _flexD.CDecimal(_flexD["UM_EX_PO"]) * tb_NM_EXCH.DecimalValue;
                _flexD["UM"] = _flexD.CDecimal(_flexD["UM_EX"]) * tb_NM_EXCH.DecimalValue;
                _flexD["CD_EXCH"] = cbo_NM_EXCH.SelectedValue.ToString();
                DataTable dtEXCH = (DataTable)cbo_NM_EXCH.DataSource;
                foreach (DataRow dr in dtEXCH.Rows)
                {
                    if (dr["CODE"].ToString() == cbo_NM_EXCH.SelectedValue.ToString())
                    {
                        _flexD["NM_EXCH"] = dr["NAME"].ToString();
                        break;
                    }
                }

                

                if (_flexD.CDecimal(_flexD["RT_PO"]) == 0)
                    _flexD["RT_PO"] = 1;
                _flexD["QT_PO_MM"] = (_flexD.CDecimal(_flexD["QT_PO"]) / _flexD.CDecimal(_flexD["RT_PO"]));
                _flexD["DT_LIMIT"] = tb_DT_LIMIT.Text;
                _flexD["DT_PLAN"] = tb_DT_LIMIT.Text;
                FillPol(_flexD.Row);
                object[] m_obj = new object[11];
                m_obj[0] = _flexD["CD_ITEM"].ToString();
                m_obj[1] = _flexD["CD_PLANT"].ToString();
                m_obj[2] = LoginInfo.CompanyCode;
                m_obj[3] = cbo_FG_UM.SelectedValue.ToString();
                m_obj[4] = cbo_NM_EXCH.SelectedValue.ToString();
                m_obj[5] = tb_DT_PO.Text;
                m_obj[6] = tb_NM_PARTNER.CodeValue;
                m_obj[7] = tb_NM_PURGRP.CodeValue;
                m_obj[8] = "N";
                m_obj[9] = _flexD["CD_PJT"].ToString();
                m_obj[10] = Global.MainFrame.ServerKeyCommon.ToUpper() ; 
                
                품목정보구하기(m_obj, "요청", 0);

                SetCC(_flexD.Row, string.Empty);

                _flexD["CD_SL"] = dt.Rows[i]["CD_SL"];
                _flexD["NM_SL"] = dt.Rows[i]["NM_SL"];


                _flexD.AddFinished();
                _flexD.Col = _flexD.Cols.Fixed;
            }
        }
    }

    #endregion

    #region -> 요청적용 버튼 클릭 이벤트(요청적용_Click)

    private void 요청적용_Click(object sender, EventArgs e)
    {
        try
        {
            if (_m_partner_use == "100") // 구매요청 품의에서 거래처를 적용받을때
            { if (!HeaderCheck(1)) return; }
            else
                if (!HeaderCheck(0)) return;

            호출여부 = true;

            pur.P_PU_POPR_SUB m_dlg = new pur.P_PU_POPR_SUB(_flexD.DataTable, cbo_CD_PLANT.SelectedValue.ToString(), tb_NM_PURGRP.CodeValue, tb_NM_PURGRP.CodeName);
            Cursor.Current = Cursors.Default;

            
            if (m_dlg.ShowDialog(this) == DialogResult.OK)
            {
                DataTable ldt_dlg = m_dlg.gdt_return;

                if (ldt_dlg == null || ldt_dlg.Rows.Count <= 0) return;

                #region ->요청 품의 거래처 적용
                //----------------요청 품의 거래처 적용-------------//
                if (_m_partner_use == "100")
                    if (!Partner_Accept(ldt_dlg)) return;
                //--------------------------------------------------//
                #endregion

                ControlButtonEnabledDisable((Control)sender, true);
                cbo_CD_PLANT.Enabled = false;

                _flexD.Redraw = false;
                decimal MaxSeq = _flexD.GetMaxValue("NO_LINE");

                tb_DC.Text = m_dlg._get_dc_rmk;
                for (int i = 0; i < ldt_dlg.Rows.Count; i++)
                {
                    if (ldt_dlg.Rows[i].RowState == DataRowState.Deleted)
                        continue;

                    MaxSeq++;
                    _flexD.Rows.Add();
                    _flexD.Row = _flexD.Rows.Count - 1;
                    _flexD["CD_ITEM"] = ldt_dlg.Rows[i]["CD_ITEM"];
                    _flexD["NM_ITEM"] = ldt_dlg.Rows[i]["NM_ITEM"];
                    _flexD["STND_ITEM"] = ldt_dlg.Rows[i]["STND_ITEM"];
                    _flexD["CD_UNIT_MM"] = ldt_dlg.Rows[i]["UNIT_PO"];
                    _flexD["UNIT_PO"] = ldt_dlg.Rows[i]["UNIT_PO"];
                    _flexD["STND_MA_ITEM"] = ldt_dlg.Rows[i]["STND_ITEM"];
                    _flexD["UNIT_IM"] = ldt_dlg.Rows[i]["UNIT_IM"];
                    _flexD["GRP_MFG"] = ldt_dlg.Rows[i]["GRP_MFG"];
                    _flexD["NM_GRPMFG"] = ldt_dlg.Rows[i]["NM_GRPMFG"];
                    //2010.01.12 납기일
                    if (tb_DT_LIMIT.Text == string.Empty || D.GetString(ldt_dlg.Rows[i]["DT_LIMIT"]) != "")
                        _flexD["DT_LIMIT"] = ldt_dlg.Rows[i]["DT_LIMIT"];
                    else
                        _flexD["DT_LIMIT"] = tb_DT_LIMIT.Text;

                    _flexD["DT_PLAN"] = ldt_dlg.Rows[i]["DT_PLAN"];

                    // 요청의 FG_POST를 읽어온다 (무조건'O'로 주는것이 아니다.)  2010.02.08
                    //_flexD["FG_POST"] =  "O";
                    //_flexD["NM_SYSDEF"] = _ComfirmState;

                    _flexD["FG_POST"] = "O";  // "O";

                   
                    _flexD["NM_SYSDEF"] = _biz.GetGubunCodeSearch("PU_C000009", _flexD["FG_POST"].ToString());//_ComfirmState;
                    _flexD["QT_PO"] = ldt_dlg.Rows[i]["QT_PR"];
                    _flexD["RT_PO"] = ldt_dlg.Rows[i]["RT_PO"];

                    if (_flexD.CDecimal(_flexD["RT_PO"]) == 0)
                        _flexD["RT_PO"] = 1;

                    _flexD["QT_PO_MM"] = (_flexD.CDecimal(ldt_dlg.Rows[i]["QT_PR"]) / _flexD.CDecimal(_flexD["RT_PO"]));

                    if (D.GetString(ldt_dlg.Rows[i]["YN_REQ_UM"]) == "Y")  // N이면 품목정보가져오기에서 단가 가져와 계산
                    {
                        _flexD["UM_EX_PO"] = _flexD.CDecimal(ldt_dlg.Rows[i]["UM_EX_PO"]);
                        _flexD["UM_EX"]    = _flexD.CDecimal(ldt_dlg.Rows[i]["UM_EX"]);
                        _flexD["UM_P"]     = _flexD.CDecimal(_flexD["UM_EX_PO"]) * tb_NM_EXCH.DecimalValue;
                        _flexD["UM"]       = _flexD.CDecimal(ldt_dlg.Rows[i]["UM_EX"]) * tb_NM_EXCH.DecimalValue;

                        _flexD["AM_EX"] = _flexD.CDecimal(_flexD["UM_EX"]) * _flexD.CDecimal(_flexD["QT_PO_MM"]);

                        #region ->부가세계산으로 들어감
                        // Decimal d_am_tatal = 0;
                        //_flexD["AM"] = Unit.원화금액(DataDictionaryTypes.PU, _flexD.CDecimal(_flexD["AM_EX"]) * tb_NM_EXCH.DecimalValue);
                        //_flexD["RATE_VAT"] = _header.CurrentRow["VAT_RATE"];

                        //_flexD["VAT"] = 부가세포함별도계산(D.GetDecimal(_flexD["AM"]), D.GetDecimal(_flexD["RATE_VAT"]), out d_am_tatal);   //Unit.원화금액(DataDictionaryTypes.PU, _flexD.CDecimal(_flexD["AM"]) * _flexD.CDecimal(_header.CurrentRow["VAT_RATE"]) / 100);
                        //총합계
                        //_flexD["AM_TOTAL"] = d_am_tatal;//_flexD.CDecimal(_flexD["AM"]) + _flexD.CDecimal(_flexD["VAT"]);
                        #endregion

                    }


                    _flexD["CD_PJT"] = ldt_dlg.Rows[i]["CD_PJT"];
                    _flexD["NM_PJT"] = ldt_dlg.Rows[i]["NM_PJT"];
                    _flexD["NO_PR"] = ldt_dlg.Rows[i]["NO_PR"];
                    _flexD["NO_PRLINE"] = ldt_dlg.Rows[i]["NO_PRLINE"];
                    _flexD["NO_CONTRACT"] = ldt_dlg.Rows[i]["NO_CONTRACT"];
                    _flexD["NO_CTLINE"] = ldt_dlg.Rows[i]["NO_CTLINE"];
                    _flexD["NO_APP"] = "";
                    _flexD["CD_PLANT"] = ldt_dlg.Rows[i]["CD_PLANT"];
                    if (tb_NO_PO.Text != string.Empty)
                        _flexD["NO_PO"] = tb_NO_PO.Text;
                    _flexD["NO_RCV"] = "";
                    _flexD["NO_LINE"] = MaxSeq;
                    _flexD["CD_SL"] = ldt_dlg.Rows[i]["CD_SL"];
                    _flexD["NM_SL"] = ldt_dlg.Rows[i]["NM_SL"];
                    _flexD["CD_EXCH"] = cbo_NM_EXCH.SelectedValue.ToString();
                    _flexD["DC1"] = ldt_dlg.Rows[i]["DC_RMK"];
                    _flexD["DC2"] = ldt_dlg.Rows[i]["DC_RMK2"];

                    //구매예산통제관련 추가 20091201
                    _flexD["CD_BUDGET"] = ldt_dlg.Rows[i]["CD_BUDGET"]; //예산단위코드
                    _flexD["NM_BUDGET"] = ldt_dlg.Rows[i]["NM_BUDGET"];
                    _flexD["CD_BGACCT"] = ldt_dlg.Rows[i]["CD_BGACCT"]; //예산계정코드
                    _flexD["NM_BGACCT"] = ldt_dlg.Rows[i]["NM_BGACCT"];


                    // 2009.12.08 다시 개발 cc설정 관련
                    // LINE 수정 권한이면 요청에서 적용받는다 
                    // 요청에 없으면 HEADER 설정에 따라 적용받는다.
                    // LINE 수정권한이 없으면 HEADER 설정에 따라 적용받는다.
                    if (m_sEnv_CC_Line == "Y" && ldt_dlg.Rows[i]["CD_CC"] != null && ldt_dlg.Rows[i]["CD_CC"].ToString().Trim() != "")
                    {
                        _flexD["CD_CC"] = ldt_dlg.Rows[i]["CD_CC"]; //cc코드
                        _flexD["NM_CC"] = ldt_dlg.Rows[i]["NM_CC"];

                    }
                    else if (m_sEnv_CC == "400") //요청담당자의 cc
                    {
                        _flexD["CD_CC"] = ldt_dlg.Rows[i]["CD_CC_EMP"]; //cc코드
                        _flexD["NM_CC"] = ldt_dlg.Rows[i]["NM_CC_EMP"];
                    }
                    else
                    {
                        //SetCC_Line(_flexD.Row, ldt_dlg.Rows[i]["CD_PURGRP"].ToString());
                        SetCC(_flexD.Row, string.Empty);
                    }

                    DataTable dt = (DataTable)cbo_NM_EXCH.DataSource;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["CODE"].ToString() == cbo_NM_EXCH.SelectedValue.ToString())
                        {
                            _flexD["NM_EXCH"] = dr["NAME"];
                            break;

                        }
                    }
                    // 발주단위에 관계없이 환산량이 있으면 환산량을 곱해서 발주수량을 만든다.
                    //if (ldt_dlg.Rows[i]["UNIT_PO"].ToString() != String.Empty)
                    //{

                    //}
                    //else
                    //{
                    //    _flexD["QT_PO_MM"] = _flexD.CDecimal(ldt_dlg.Rows[i]["QT_PR"]);
                    //}
                    //2010.03.05 추가
                    if (m_dlg.요청비고체크)
                        _flexD["DC1"] = ldt_dlg.Rows[i]["DC_LINE_RMK"];

                    //_flexD["CD_PJT"] = D.GetString(ldt_dlg.Rows[i]["CD_PJT_LINE"]);

                    _flexD["CD_PJT"] = D.GetString(ldt_dlg.Rows[i]["CD_PJT_LINE"]);
                    _flexD["NM_PJT"] = D.GetString(ldt_dlg.Rows[i]["NM_PJT_LINE"]);
                    _flexD["SEQ_PROJECT"] = D.GetDecimal(ldt_dlg.Rows[i]["SEQ_PJT_LINE"]);
                    _flexD["NM_CLS_ITEM"] = D.GetString(ldt_dlg.Rows[i]["NM_CLS_ITEM"]);
                    _flexD["CD_ITEM_ORIGIN"] = D.GetString(ldt_dlg.Rows[i]["CD_ITEM_ORIGIN"]);
                    _flexD["FG_PACKING"] = ldt_dlg.Rows[i]["FG_PACKING"];
                    _flexD["FG_SU"] = ldt_dlg.Rows[i]["FG_SU"];
                    _flexD["CD_REASON"] = ldt_dlg.Rows[i]["CD_REASON"];

                    _flexD["PI_PARTNER"] = ldt_dlg.Rows[i]["PARTNER"];
                    _flexD["PI_LN_PARTNER"] = ldt_dlg.Rows[i]["PARTNER_NM"];



                    if (Global.MainFrame.ServerKeyCommon.ToUpper() == "ICDERPU")
                     _flexD["CD_USERDEF1"] = ldt_dlg.Rows[i]["CD_USERDEF1"];

                    if (_APP_USERDEF == "Y")
                    {
                        _flexD["NM_USERDEF1"] = ldt_dlg.Rows[i]["CD_USERDEF1"];
                        _flexD["NM_USERDEF2"] = ldt_dlg.Rows[i]["CD_USERDEF2"];

                        if (Global.MainFrame.ServerKeyCommon.ToUpper() != "ICDERPU")
                        {
                            _flexD["DATE_USERDEF1"] = ldt_dlg.Rows[i]["DATE_USERDEF1_PR"];
                            _flexD["DATE_USERDEF2"] = ldt_dlg.Rows[i]["DATE_USERDEF2_PR"];
                        }
                        _flexD["TXT_USERDEF1"] = ldt_dlg.Rows[i]["TXT_USERDEF1_PR"];
                        _flexD["TXT_USERDEF2"] = ldt_dlg.Rows[i]["TXT_USERDEF2_PR"];
                        _flexD["CDSL_USERDEF1"] = ldt_dlg.Rows[i]["CDSL_USERDEF1_PR"];
                        _flexD["NMSL_USERDEF1"] = ldt_dlg.Rows[i]["NMSL_USERDEF1_PR"];


                        _flexD["NUM_USERDEF1"] = ldt_dlg.Rows[i]["NU_USERDEF1"];
                        _flexD["NUM_USERDEF2"] = ldt_dlg.Rows[i]["NU_USERDEF1"];
                    }
                    //규격형일 경우 
                    if (bStandard)
                    {
                        _flexD["NUM_STND_ITEM_1"] = ldt_dlg.Rows[i]["NUM_STND_ITEM_1"];
                        _flexD["NUM_STND_ITEM_2"] = ldt_dlg.Rows[i]["NUM_STND_ITEM_2"];
                        _flexD["NUM_STND_ITEM_3"] = ldt_dlg.Rows[i]["NUM_STND_ITEM_3"];
                        _flexD["NUM_STND_ITEM_4"] = ldt_dlg.Rows[i]["NUM_STND_ITEM_4"];
                        _flexD["NUM_STND_ITEM_5"] = ldt_dlg.Rows[i]["NUM_STND_ITEM_5"];
                        _flexD["UM_WEIGHT"] = ldt_dlg.Rows[i]["UM_WEIGHT"];
                        _flexD["TOT_WEIGHT"] = ldt_dlg.Rows[i]["TOT_WEIGHT"];
                        _flexD["CLS_L"] = ldt_dlg.Rows[i]["CLS_L"];
                        _flexD["CLS_M"] = ldt_dlg.Rows[i]["CLS_M"];
                        _flexD["CLS_S"] = ldt_dlg.Rows[i]["CLS_S"];
                        _flexD["NM_CLS_L"] = ldt_dlg.Rows[i]["NM_CLS_L"];
                        _flexD["NM_CLS_M"] = ldt_dlg.Rows[i]["NM_CLS_M"];
                        _flexD["NM_CLS_S"] = ldt_dlg.Rows[i]["NM_CLS_S"];
                        _flexD["SG_TYPE"] = ldt_dlg.Rows[i]["SG_TYPE"];
                        _flexD["QT_SG"] = ldt_dlg.Rows[i]["QT_SG"];
                    }

                    FillPol(_flexD.Row);
                    object[] m_obj = new object[11];

                    m_obj[0] = _flexD["CD_ITEM"].ToString();
                    m_obj[1] = _flexD["CD_PLANT"].ToString();
                    m_obj[2] = LoginInfo.CompanyCode;
                    m_obj[3] = cbo_FG_UM.SelectedValue.ToString();
                    m_obj[4] = cbo_NM_EXCH.SelectedValue.ToString();
                    m_obj[5] = tb_DT_PO.Text;
                    m_obj[6] = tb_NM_PARTNER.CodeValue;
                    m_obj[7] = tb_NM_PURGRP.CodeValue;
                    m_obj[8] = ldt_dlg.Rows[i]["YN_REQ_UM"];
                    m_obj[9] =  D.GetString(_flexD["CD_PJT"]);
                    m_obj[10] = Global.MainFrame.ServerKeyCommon.ToUpper();

                    품목정보구하기(m_obj, "요청", 0);

                    //중량단가가 존재하는 경우 품목정보 구하기의 금액계산에서 부가세를 계산하여 주기 때문에 따로 실행하지 않는다.
                    if (D.GetDecimal(_flexD["UM_WEIGHT"]) == 0)
                        부가세계산(_flexD.GetDataRow(_flexD.Row) );

                    if (_m_Company_only == "001")
                        AsahiKasei_Only_ValidateEdit(_flexD.Row, D.GetDecimal(_flexD["UM_EX_PO"]), "UM_EX_PO");

                    //PJT형 사용여부
                    if (Config.MA_ENV.PJT형여부 == "Y")
                    {
                        _flexD["CD_PJT_ITEM"] = ldt_dlg.Rows[i]["CD_PJT_ITEM"];
                        _flexD["NM_PJT_ITEM"] = ldt_dlg.Rows[i]["NM_PJT_ITEM"];
                        _flexD["PJT_ITEM_STND"] = ldt_dlg.Rows[i]["PJT_ITEM_STND"];
                        _flexD["NO_WBS"] = ldt_dlg.Rows[i]["NO_WBS"];
                        _flexD["NO_CBS"] = ldt_dlg.Rows[i]["NO_CBS"];
                        _flexD["CD_ACTIVITY"] = ldt_dlg.Rows[i]["CD_ACTIVITY"];
                        _flexD["NM_ACTIVITY"] = ldt_dlg.Rows[i]["NM_ACTIVITY"];
                        _flexD["CD_COST"] = ldt_dlg.Rows[i]["CD_COST"];
                        _flexD["NM_COST"] = ldt_dlg.Rows[i]["NM_COST"];
                        _flexD["NO_LINE_PJTBOM"] = ldt_dlg.Rows[i]["NO_LINE_PJTBOM"];
                        _flexD["CD_ITEM_MO"] = ldt_dlg.Rows[i]["CD_ITEM_MO"];
                        _flexD["NM_ITEM_MO"] = ldt_dlg.Rows[i]["NM_ITEM_MO"];
                    }

                    if (MainFrameInterface.ServerKeyCommon == "UNIPOINT")
                    {
                        _flexD["CD_PARTNER_PJT"] = ldt_dlg.Rows[i]["CD_PARTNER_PJT"];
                        _flexD["LN_PARTNER_PJT"] = ldt_dlg.Rows[i]["LN_PARTNER_PJT"];
                        _flexD["NO_EMP_PJT"] = ldt_dlg.Rows[i]["NO_EMP_PJT"];
                        _flexD["NM_KOR_PJT"] = ldt_dlg.Rows[i]["NM_KOR_PJT"];
                        _flexD["END_USER"] = ldt_dlg.Rows[i]["END_USER"];
                    } 

                    if (sPUSU == "100")
                        GET_SU_BOM();   //품목입력시 해당품목의 외주BOM(SU_BOM)을 가져오는 구문

                    _flexD.AddFinished();
                    _flexD.Col = _flexD.Cols.Fixed;
                }

                SUMFunction();
                SetHeadControlEnabled(false, 1);

 
            }
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
        finally
        {
            _flexD.Redraw = true;
        }
    }

    #endregion

    #region -> 품의적용 버튼 클릭 이벤트(품의적용_Click)

    /// <summary>
    /// 품의적용
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    private void 품의적용_Click(object sender, System.EventArgs e)
    {
        if (_m_partner_use == "100") // 구매요청 품의에서 거래처를 적용받을때
        { if (!HeaderCheck(1)) return; }
        else
            if (!HeaderCheck(0)) return;

        호출여부 = true;

        Cursor.Current = Cursors.WaitCursor;
        pur.P_PU_APP_SUB2 m_dlg;

        if (_m_partner_change != "001")
        {
                m_dlg = new pur.P_PU_APP_SUB2(MainFrameInterface, _flexD.DataTable,
                cbo_CD_PLANT.SelectedValue.ToString(), cbo_CD_PLANT.SelectedText.ToString(),
                tb_NM_PURGRP.CodeValue, tb_NM_PURGRP.CodeName, tb_NM_PARTNER.CodeValue, tb_NM_PARTNER.CodeName);
        }
        else
        {
                 m_dlg = new pur.P_PU_APP_SUB2(MainFrameInterface, _flexD.DataTable,
                cbo_CD_PLANT.SelectedValue.ToString(), cbo_CD_PLANT.SelectedText.ToString(),
                tb_NM_PURGRP.CodeValue, tb_NM_PURGRP.CodeName, tb_NM_PARTNER.CodeValue, tb_NM_PARTNER.CodeName, D.GetString(cbo_NM_EXCH.SelectedValue), tb_NM_EXCH.DecimalValue);
        }

        if (m_dlg.ShowDialog(this) == DialogResult.OK)
        {
            DataTable ldt_dlg = m_dlg.gdt_Return;

            try
            {
                if (ldt_dlg == null || ldt_dlg.Rows.Count < 0) return;
                if (m_sEnv_App_Sort == "100")
                {
                    ldt_dlg.DefaultView.Sort = "NO_APP, NO_APPLINE";
                    ldt_dlg = ldt_dlg.DefaultView.ToTable().Copy();
                }

                #region ->요청 품의 거래처 적용
                //----------------요청 품의 거래처 적용-------------//
                if (_m_partner_use == "100" || _m_partner_change =="001" )
                    if (!Partner_Accept(ldt_dlg)) return;
                //--------------------------------------------------//
                #endregion

                #region ->품의 발주형태 적용
                //------------------구매품의 발주형태적용시---체크로직----------------
                if (_m_tppo_use == "001")
                {
                    if (!Tppo_Accept(ldt_dlg,"")) return;
                }
                //-----------------------------------------------------------------------
                #endregion

                #region ->요청 품의 환종 적용
                //----------------요청 품의 환종 적용-------------//
                if (_m_partner_change == "001")
                    if (!Exch_Accept(ldt_dlg)) return;
                //--------------------------------------------------//
                #endregion

                _header.CurrentRow["DC_RMK_TEXT"] = D.GetString(ldt_dlg.Rows[0]["DC_RMK_TEXT"]);
                txt_DC_RMK_TEXT.Text = D.GetString(ldt_dlg.Rows[0]["DC_RMK_TEXT"]);

                ControlButtonEnabledDisable((Control)sender, true);
                cbo_CD_PLANT.Enabled = false;

                if (D.GetString(ldt_dlg.Rows[0]["COND_PRICE"]) != string.Empty)
                {
                    cbo_COND_PRICE.SelectedValue = D.GetString(ldt_dlg.Rows[0]["COND_PRICE"]);
                    _header.CurrentRow["COND_PRICE"] = D.GetString(ldt_dlg.Rows[0]["COND_PRICE"]);
                }

                if (D.GetString(ldt_dlg.Rows[0]["CD_STND_PAY"]) != string.Empty)
                {
                    cbo_stnd_pay.SelectedValue = D.GetString(ldt_dlg.Rows[0]["CD_STND_PAY"]);
                    _header.CurrentRow["STND_PAY"] = D.GetString(ldt_dlg.Rows[0]["CD_STND_PAY"]);
                }

                _flexD.Redraw = false;

                decimal MaxSeq = _flexD.GetMaxValue("NO_LINE");

                for (int i = 0; i < ldt_dlg.Rows.Count; i++)
                {
                    if (ldt_dlg.Rows[i].RowState == DataRowState.Deleted)
                        continue;

                    _flexD.Rows.Add();
                    _flexD.Row = _flexD.Rows.Count - 1;

                    if (tb_NO_PO.Text != string.Empty)
                        _flexD["NO_PO"] = tb_NO_PO.Text;
                    _flexD["NO_LINE"] = ++MaxSeq;
                    _flexD["RT_PO"] = ldt_dlg.Rows[i]["RT_PO"];

                    //if (ldt_dlg.Rows[i]["UNIT_PO"].ToString() != "")
                    //{

                    if (_flexD.CDecimal(_flexD["RT_PO"]) == 0) 
                        _flexD["RT_PO"] = 1;
                    // 구매품의에서 발주단위 수량을 추가한지 얼마 안되었기 때문에 QT_PO_MM 이 널이면 적용하기 이전값이라 생각하고 발주단위수량을 구해준다.
                    // 널이 아니면.. 적용 이후 발주단위수량이 저장된 것이므로 바로 적용 시켜준다.
                    if (D.GetString(ldt_dlg.Rows[i]["QT_APP_MM"]) == string.Empty) 
                        _flexD["QT_PO_MM"] = (_flexD.CDecimal(ldt_dlg.Rows[i]["QT_APP"]) / _flexD.CDecimal(_flexD["RT_PO"]));
                    else
                    {
                        _flexD["QT_PO_MM"] = D.GetDecimal(ldt_dlg.Rows[i]["QT_APP_MM"]);
                    }

                    //}
                    //else
                    //{
                    //    _flexD["QT_PO_MM"] = _flexD.CDecimal(ldt_dlg.Rows[i]["QT_APP"]);
                    //}

                    //_flexD["UM_EX_PO"] = _flexD.CDecimal(ldt_dlg.Rows[i]["UM"]) * _flexD.CDecimal(ldt_dlg.Rows[i]["RT_PO"]); ;
                    //_flexD["UM_EX"] = _flexD.CDecimal(ldt_dlg.Rows[i]["UM"]);       //품의재고단가
                    //_flexD["UM_P"] = _flexD.CDecimal(_flexD["UM_EX_PO"]) * tb_NM_EXCH.DecimalValue;
                    //_flexD["UM"] = _flexD.CDecimal(ldt_dlg.Rows[i]["UM"]) * tb_NM_EXCH.DecimalValue;

                    //구매품의 UM_EX, UM_EX_PO추가로... 

                    _flexD["UM_EX_PO"] = _flexD.CDecimal(ldt_dlg.Rows[i]["UM_EX_PO"]);
                    _flexD["UM_EX"] = _flexD.CDecimal(ldt_dlg.Rows[i]["UM_EX"]);       //품의재고단가
                    _flexD["UM_P"] = _flexD.CDecimal(_flexD["UM_EX_PO"]) * tb_NM_EXCH.DecimalValue;
                    _flexD["UM"] = _flexD.CDecimal(ldt_dlg.Rows[i]["UM_EX"]) * tb_NM_EXCH.DecimalValue;

                    #region -> 부가세계산으로 들어감
                    //Decimal d_am_tatal = 0;
                    //_flexD["AM"] = Unit.원화금액(DataDictionaryTypes.PU, _flexD.CDecimal(_flexD["AM_EX"]) * tb_NM_EXCH.DecimalValue);                        
                    //_flexD["RATE_VAT"] = _header.CurrentRow["VAT_RATE"];
                    //_flexD["VAT"] = 부가세포함별도계산(D.GetDecimal(_flexD["AM"]), D.GetDecimal(_flexD["RATE_VAT"]), out d_am_tatal);   //Unit.원화금액(DataDictionaryTypes.PU, _flexD.CDecimal(_flexD["AM"]) * _flexD.CDecimal(_header.CurrentRow["VAT_RATE"]) / 100);
                    //총합계
                    //_flexD["AM_TOTAL"] = d_am_tatal;//_flexD.CDecimal(_flexD["AM"])+_flexD.CDecimal(_flexD["VAT"]);
                    #endregion

                    _flexD["NO_CONTRACT"] = ldt_dlg.Rows[i]["NO_CONTRACT"];
                    _flexD["NO_CTLINE"] = ldt_dlg.Rows[i]["NO_CTLINE"];

                    // 의뢰
                    _flexD["NO_RCV"] = "";

                    // 요청적용에서 가지고 오는 것이므로..요청번호와 항번입력
                    _flexD["NO_PR"] = ldt_dlg.Rows[i]["NO_PR"].ToString();  //NULL 값이 들어가면 않되기 때문에 ToString 구문을 넣어준다.(ToString 구문을 쓰면 NULL인 경우 ""이 된다.)
                    _flexD["NO_PRLINE"] = D.GetDecimal(ldt_dlg.Rows[i]["NO_PRLINE"]);
                    //품의적용
                    _flexD["NO_APP"] = ldt_dlg.Rows[i]["NO_APP"];
                    _flexD["NO_APPLINE"] = D.GetDecimal(ldt_dlg.Rows[i]["NO_APPLINE"]);
                    //_flexD["NM_SYSDEF"] = _ComfirmState;
                    _flexD["FG_POST"] = "O";
                    _flexD["NM_SYSDEF"] = _biz.GetGubunCodeSearch("PU_C000009", _flexD["FG_POST"].ToString());//_ComfirmState;



                    _flexD["CD_PLANT"] = ldt_dlg.Rows[i]["CD_PLANT"];
                    _flexD["CD_ITEM"] = ldt_dlg.Rows[i]["CD_ITEM"];
                    _flexD["NM_ITEM"] = ldt_dlg.Rows[i]["NM_ITEM"];
                    _flexD["CD_UNIT_MM"] = ldt_dlg.Rows[i]["UNIT_PO"];
                    _flexD["UNIT_PO"] = ldt_dlg.Rows[i]["UNIT_PO"];
                    _flexD["STND_MA_ITEM"] = ldt_dlg.Rows[i]["STND_ITEM"];
                    _flexD["STND_ITEM"] = ldt_dlg.Rows[i]["STND_ITEM"];
                    _flexD["UNIT_IM"] = ldt_dlg.Rows[i]["UNIT_IM"];
                    _flexD["QT_PO"] = D.GetDecimal(ldt_dlg.Rows[i]["QT_APP"]);
                    //_flexD["DT_LIMIT"] = ldt_dlg.Rows[i]["DT_LIMIT"];
                    //2010.01.12 납기일
                    if (tb_DT_LIMIT.Text == string.Empty || D.GetString(ldt_dlg.Rows[i]["DT_LIMIT"]) != "")
                        _flexD["DT_LIMIT"] = ldt_dlg.Rows[i]["DT_LIMIT"];
                    else
                        _flexD["DT_LIMIT"] = tb_DT_LIMIT.Text;

                    _flexD["DT_PLAN"] = ldt_dlg.Rows[i]["DT_PLAN"];

                    _flexD["CD_SL"] = ldt_dlg.Rows[i]["CD_SL"];
                    _flexD["NM_SL"] = ldt_dlg.Rows[i]["NM_SL"];
                    _flexD["CD_EXCH"] = cbo_NM_EXCH.SelectedValue.ToString();
                    //                        SetCC(_flexD.Row,"");

                    //구매예산통제관련 추가 20091201
                    _flexD["CD_BUDGET"] = ldt_dlg.Rows[i]["CD_BUDGET"]; //예산단위코드
                    _flexD["NM_BUDGET"] = ldt_dlg.Rows[i]["NM_BUDGET"];
                    //_flexD["CD_CC"] = ldt_dlg.Rows[i]["CD_CC"]; //cc코드
                    //_flexD["NM_CC"] = ldt_dlg.Rows[i]["NM_CC"];
                    _flexD["CD_BGACCT"] = ldt_dlg.Rows[i]["CD_BGACCT"]; //예산계정코드
                    _flexD["NM_BGACCT"] = ldt_dlg.Rows[i]["NM_BGACCT"];

                    _flexD["CD_PJT"] = ldt_dlg.Rows[i]["CD_PJT"];
                    _flexD["NM_PJT"] = ldt_dlg.Rows[i]["NM_PROJECT"];
                    _flexD["SEQ_PROJECT"] = D.GetDecimal(ldt_dlg.Rows[i]["SEQ_PROJECT"]);

                    // 2009.12.08 다시 개발 cc설정 관련
                    // LINE 수정 권한이면 요청에서 적용받는다 
                    // 요청에 없으면 HEADER 설정에 따라 적용받는다.
                    // LINE 수정권한이 없으면 HEADER 설정에 따라 적용받는다.
                    if (m_sEnv_CC_Line == "Y" && ldt_dlg.Rows[i]["CD_CC"] != null && ldt_dlg.Rows[i]["CD_CC"].ToString().Trim() != "")
                    {
                        _flexD["CD_CC"] = ldt_dlg.Rows[i]["CD_CC"]; //cc코드
                        _flexD["NM_CC"] = ldt_dlg.Rows[i]["NM_CC"];

                    }
                    else
                    {
                        //SetCC_Line(_flexD.Row, ldt_dlg.Rows[i]["CD_PURGRP"].ToString());
                        SetCC(_flexD.Row, string.Empty);
                    }

                    ////적용된 cc가 설정되어있안으면 호출 20091201
                    //if (ldt_dlg.Rows[i]["CD_CC"] == null || ldt_dlg.Rows[i]["CD_CC"].ToString().Trim() != "")
                    //{
                    //    SetCC_Line(_flexD.Row, ldt_dlg.Rows[i]["CD_PURGRP"].ToString());
                    //}


                    DataTable dt = (DataTable)cbo_NM_EXCH.DataSource;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["CODE"].ToString() == cbo_NM_EXCH.SelectedValue.ToString())
                        {
                            _flexD["NM_EXCH"] = dr["NAME"];
                            break;
                        }
                    }

                    _flexD["DC1"] = ldt_dlg.Rows[i]["DC_RMK1"];
                    _flexD["DC2"] = ldt_dlg.Rows[i]["DC_RMK2"];
                    _flexD["NM_CLS_ITEM"] = ldt_dlg.Rows[i]["NM_CLS_ITEM"];
                    _flexD["CD_ITEM_ORIGIN"] = D.GetString(ldt_dlg.Rows[i]["CD_ITEM_ORIGIN"]);
                    _flexD["FG_PACKING"] = ldt_dlg.Rows[i]["FG_PACKING"];
                    _flexD["FG_SU"] = ldt_dlg.Rows[i]["FG_SU"];
                    _flexD["CD_REASON"] = ldt_dlg.Rows[i]["CD_REASON"];
                    _flexD["NM_GRPMFG"] = ldt_dlg.Rows[i]["NM_GRPMFG"];

                    _flexD["PI_PARTNER"] = ldt_dlg.Rows[i]["PI_PARTNER"];
                    _flexD["PI_LN_PARTNER"] = ldt_dlg.Rows[i]["PI_LN_PARTNER"];

                    if (Global.MainFrame.ServerKeyCommon.ToUpper() == "ICDERPU")
                    {
                        _flexD["CD_USERDEF1"] = ldt_dlg.Rows[i]["CD_USERDEF1"];
                        _flexD["UM_PRE"] = ldt_dlg.Rows[i]["UM_PRE"];
                        _flexD["AM_PRE"] = ldt_dlg.Rows[i]["AM_PRE"];
                    }
                    if (_APP_USERDEF == "Y")
                    {
                        _flexD["NM_USERDEF1"] = ldt_dlg.Rows[i]["CD_USERDEF1"];
                        _flexD["NM_USERDEF2"] = ldt_dlg.Rows[i]["CD_USERDEF2"];


                        _flexD["DATE_USERDEF1"] = ldt_dlg.Rows[i]["DATE_USERDEF1_APP"];
                        _flexD["DATE_USERDEF2"] = ldt_dlg.Rows[i]["DATE_USERDEF2_APP"];

                        _flexD["TXT_USERDEF1"] = ldt_dlg.Rows[i]["TXT_USERDEF1_APP"];
                        _flexD["TXT_USERDEF2"] = ldt_dlg.Rows[i]["TXT_USERDEF2_APP"];
                        _flexD["CDSL_USERDEF1"] = ldt_dlg.Rows[i]["CDSL_USERDEF1_APP"];
                        _flexD["NMSL_USERDEF1"] = ldt_dlg.Rows[i]["NMSL_USERDEF1_APP"];

                        _flexD["NUM_USERDEF1"] = ldt_dlg.Rows[i]["NU_USERDEF1"];
                        _flexD["NUM_USERDEF2"] = ldt_dlg.Rows[i]["NU_USERDEF1"];
                    }
                   

                    if (D.GetString(ldt_dlg.Rows[i]["FG_PAYMENT"]) != "" && Global.MainFrame.ServerKeyCommon != "DNF")
                    {
                        _header.CurrentRow["FG_PAYMENT"] = ldt_dlg.Rows[i]["FG_PAYMENT"];
                        cbo_PAYment.SelectedValue = D.GetString(_header.CurrentRow["FG_PAYMENT"]);
                    }

                    //규격형일 경우 
                    if (bStandard)
                    {
                        _flexD["NUM_STND_ITEM_1"] = ldt_dlg.Rows[i]["NUM_STND_ITEM_1"];
                        _flexD["NUM_STND_ITEM_2"] = ldt_dlg.Rows[i]["NUM_STND_ITEM_2"];
                        _flexD["NUM_STND_ITEM_3"] = ldt_dlg.Rows[i]["NUM_STND_ITEM_3"];
                        _flexD["NUM_STND_ITEM_4"] = ldt_dlg.Rows[i]["NUM_STND_ITEM_4"];
                        _flexD["NUM_STND_ITEM_5"] = ldt_dlg.Rows[i]["NUM_STND_ITEM_5"];
                        _flexD["UM_WEIGHT"] = ldt_dlg.Rows[i]["UM_WEIGHT"];
                        _flexD["TOT_WEIGHT"] = ldt_dlg.Rows[i]["TOT_WEIGHT"];
                        _flexD["CLS_L"] = ldt_dlg.Rows[i]["CLS_L"];
                        _flexD["CLS_M"] = ldt_dlg.Rows[i]["CLS_M"];
                        _flexD["CLS_S"] = ldt_dlg.Rows[i]["CLS_S"];
                        _flexD["NM_CLS_L"] = ldt_dlg.Rows[i]["NM_CLS_L"];
                        _flexD["NM_CLS_M"] = ldt_dlg.Rows[i]["NM_CLS_M"];
                        _flexD["NM_CLS_S"] = ldt_dlg.Rows[i]["NM_CLS_S"];
                        _flexD["SG_TYPE"] = ldt_dlg.Rows[i]["SG_TYPE"];
                        _flexD["QT_SG"] = ldt_dlg.Rows[i]["QT_SG"];
                    }


                    FillPol(_flexD.Row);
                    object[] m_obj = new object[11];
                    m_obj[0] = _flexD["CD_ITEM"].ToString();
                    m_obj[1] = _flexD["CD_PLANT"].ToString();
                    m_obj[2] = LoginInfo.CompanyCode;
                    m_obj[3] = cbo_FG_UM.SelectedValue.ToString();
                    m_obj[4] = cbo_NM_EXCH.SelectedValue.ToString();
                    m_obj[5] = tb_DT_PO.Text;
                    m_obj[6] = tb_NM_PARTNER.CodeValue;
                    m_obj[7] = tb_NM_PURGRP.CodeValue;
                    m_obj[8] = "N";
                    m_obj[9] = D.GetString(_flexD["CD_PJT"]);
                    m_obj[10] = Global.MainFrame.ServerKeyCommon.ToUpper();


                    품목정보구하기(m_obj, "품의", 0);

                    //중량단가가 존재하는 경우 품목정보 구하기의 금액계산에서 부가세를 계산하여 주기 때문에 따로 실행하지 않는다.
                    if (D.GetDecimal(_flexD["UM_WEIGHT"]) == 0)
                        부가세계산( _flexD.GetDataRow(_flexD.Row) );

                    if (_m_Company_only == "001")
                        AsahiKasei_Only_ValidateEdit(_flexD.Row, D.GetDecimal(_flexD["UM_EX_PO"]), "UM_EX_PO");

                    //PJT형 사용여부
                    if (Config.MA_ENV.PJT형여부 == "Y")
                    {
                        _flexD["CD_PJT_ITEM"] = ldt_dlg.Rows[i]["CD_PJT_ITEM"];
                        _flexD["NM_PJT_ITEM"] = ldt_dlg.Rows[i]["NM_PJT_ITEM"];
                        _flexD["PJT_ITEM_STND"] = ldt_dlg.Rows[i]["PJT_ITEM_STND"];
                        _flexD["NO_WBS"] = ldt_dlg.Rows[i]["NO_WBS"];
                        _flexD["NO_CBS"] = ldt_dlg.Rows[i]["NO_CBS"];
                        _flexD["CD_ACTIVITY"] = ldt_dlg.Rows[i]["CD_ACTIVITY"];
                        _flexD["NM_ACTIVITY"] = ldt_dlg.Rows[i]["NM_ACTIVITY"];
                        _flexD["CD_COST"] = ldt_dlg.Rows[i]["CD_COST"];
                        _flexD["NM_COST"] = ldt_dlg.Rows[i]["NM_COST"];
                        _flexD["NO_LINE_PJTBOM"] = ldt_dlg.Rows[i]["NO_LINE_PJTBOM"];
                        _flexD["CD_ITEM_MO"] = ldt_dlg.Rows[i]["CD_ITEM_MO"];
                        _flexD["NM_ITEM_MO"] = ldt_dlg.Rows[i]["NM_ITEM_MO"];
                    }

                    if (D.GetString(_flexD["TP_UM_TAX"]) != "001" && m_sEnv_App_Am == "001")  //부가세포함
                    {

                        _flexD["AM_EX"] = ldt_dlg.Rows[i]["AM_EX_JAN"];
                        _flexD["AM"] = ldt_dlg.Rows[i]["AM_JAN"];
                        부가세만계산();
                    }

                    _flexD.AddFinished();
                    _flexD.Col = _flexD.Cols.Fixed;

                    if (sPUSU == "100")
                        GET_SU_BOM();   //품목입력시 해당품목의 외주BOM(SU_BOM)을 가져오는 구문
                }
                SUMFunction();

                SetHeadControlEnabled(false, 1);

                if (_m_tppo_change == "001")
                {
                    btn_insert.Enabled = false;
                    btn_delete.Enabled = false;
                }

                _flexD.SumRefresh();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                _flexD.Redraw = true;
                tb_DC.Focus();
            }
        }
    }



    #endregion

    #region -> 수주적용 버튼 클릭 이벤트(수주적용_Click)

    /// <summary>
    /// 수주적용
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btn_RE_SO_Click(object sender, EventArgs e)
    {
        try
        {
            if (!HeaderCheck(0)) return;

            호출여부 = true;

            pur.P_PU_PO_SO_SUB m_dlg = new pur.P_PU_PO_SO_SUB(cbo_CD_PLANT.SelectedValue.ToString(), cbo_CD_PLANT.Text, _flexD.DataTable);
            Cursor.Current = Cursors.Default;

            if (m_dlg.ShowDialog(this) == DialogResult.OK)
            {
                DataTable ldt_dlg = m_dlg.수주데이터;
                string strSIN = string.Empty;

                if (ldt_dlg == null || ldt_dlg.Rows.Count <= 0) return;

                //if (!Partner_Accept(ldt_dlg)) return;

                ControlButtonEnabledDisable((Control)sender, true);
                cbo_CD_PLANT.Enabled = false;


                _flexD.Redraw = false;

                if (Global.MainFrame.ServerKeyCommon == "MHIK" || Global.MainFrame.ServerKeyCommon == "DZSQL") 
                {
                    if (!string.IsNullOrEmpty(D.GetString(_header.CurrentRow["TXT_USERDEF4"]))
                        && D.GetString(_header.CurrentRow["TXT_USERDEF4"]) != D.GetString(ldt_dlg.Rows[0]["TXT_USERDEF4"]))
                    {
                        //if (ShowMessage(DD("수주번호가 다릅니다. 선택된 데이터를 삭제하시겠습니까?"), "QY2") != DialogResult.Yes)
                        //    return;
                        DataRow[] drsD = _flexD.DataTable.Select("");//"TXT_USERDEF4 = '" + D.GetString(_header.CurrentRow["TXT_USERDEF4"]) + "'");
                        foreach (DataRow drD in drsD)
                            drD.Delete();

                        tb_NO_PO_MH.Text = D.GetString(ldt_dlg.Rows[0]["TXT_USERDEF4"]);
                        _header.CurrentRow["TXT_USERDEF4"] = D.GetString(ldt_dlg.Rows[0]["TXT_USERDEF4"]);
                    }
                    if(string.IsNullOrEmpty(D.GetString(_header.CurrentRow["TXT_USERDEF4"])))
                    {
                        tb_NO_PO_MH.Text = D.GetString(ldt_dlg.Rows[0]["TXT_USERDEF4"]);
                        _header.CurrentRow["TXT_USERDEF4"] = D.GetString(ldt_dlg.Rows[0]["TXT_USERDEF4"]);
                    }
                }

                decimal MaxSeq = _flexD.GetMaxValue("NO_LINE");

                for (int i = 0; i < ldt_dlg.Rows.Count; i++)
                {
                    if (ldt_dlg.Rows[i].RowState == DataRowState.Deleted)
                        continue;

                    MaxSeq++;
                    _flexD.Rows.Add();
                    _flexD.Row = _flexD.Rows.Count - 1;
                    _flexD["CD_ITEM"] = ldt_dlg.Rows[i]["CD_ITEM"];
                    _flexD["NM_ITEM"] = ldt_dlg.Rows[i]["NM_ITEM"];
                    _flexD["STND_ITEM"] = ldt_dlg.Rows[i]["STND_ITEM"];
                    _flexD["CD_UNIT_MM"] = ldt_dlg.Rows[i]["UNIT_PO"];
                    _flexD["UNIT_PO"] = ldt_dlg.Rows[i]["UNIT_PO"];
                    _flexD["STND_MA_ITEM"] = ldt_dlg.Rows[i]["STND_ITEM"];
                    _flexD["UNIT_IM"] = ldt_dlg.Rows[i]["UNIT_IM"];
                    //2010.01.12 납기일
                    if (D.GetString(ldt_dlg.Rows[i]["DT_DUEDATE"]) != "")
                        _flexD["DT_LIMIT"] = ldt_dlg.Rows[i]["DT_DUEDATE"];
                    else
                        _flexD["DT_LIMIT"] = tb_DT_LIMIT.Text;

                    _flexD["DT_PLAN"] = _flexD["DT_LIMIT"];

                    _flexD["FG_POST"] = "O"; //OPEN
                    //_flexD["NM_SYSDEF"] = _ComfirmState;                        
                    _flexD["NM_SYSDEF"] = _biz.GetGubunCodeSearch("PU_C000009", _flexD["FG_POST"].ToString());//_ComfirmState;


                    _flexD["RT_PO"] = ldt_dlg.Rows[i]["RT_PO"];
                    _flexD["CD_PJT"] = ldt_dlg.Rows[i]["CD_PJT"];
                    _flexD["NM_PJT"] = ldt_dlg.Rows[i]["NM_PJT"];
                    _flexD["NO_PR"] = "";
                    _flexD["NO_PRLINE"] = 0;
                    //_flexD["NO_CONTRACT"] = ldt_dlg.Rows[i]["NO_CONTRACT"];
                    //_flexD["NO_CTLINE"] = ldt_dlg.Rows[i]["NO_CTLINE"];

                    _flexD["NO_SO"] = ldt_dlg.Rows[i]["NO_SO"];   // 수주번호
                    _flexD["NO_SOLINE"] = ldt_dlg.Rows[i]["SEQ_SO"];  // 수주항번

                    _flexD["CD_PLANT"] = ldt_dlg.Rows[i]["CD_PLANT"];
                    if (tb_NO_PO.Text != string.Empty)
                        _flexD["NO_PO"] = tb_NO_PO.Text;
                    _flexD["NO_RCV"] = "";
                    _flexD["NO_LINE"] = MaxSeq;
                    _flexD["CD_SL"] = ldt_dlg.Rows[i]["CD_SL"];
                    _flexD["NM_SL"] = ldt_dlg.Rows[i]["NM_SL"];
                    _flexD["CD_EXCH"] = cbo_NM_EXCH.SelectedValue.ToString();
                    _flexD["DC1"] = ldt_dlg.Rows[i]["DC_RMK"];

                    ////구매예산통제관련 추가 20091201
                    //_flexD["CD_BUDGET"] = ldt_dlg.Rows[i]["CD_BUDGET"]; //예산단위코드
                    //_flexD["NM_BUDGET"] = ldt_dlg.Rows[i]["NM_BUDGET"];
                    //_flexD["CD_BGACCT"] = ldt_dlg.Rows[i]["CD_BGACCT"]; //예산계정코드
                    //_flexD["NM_BGACCT"] = ldt_dlg.Rows[i]["NM_BGACCT"];

                    // 2009.12.08 다시 개발 cc설정 관련
                    // LINE 수정 권한이면 요청에서 적용받는다 
                    // 요청에 없으면 HEADER 설정에 따라 적용받는다.
                    // LINE 수정권한이 없으면 HEADER 설정에 따라 적용받는다.

                    //SetCC_Line(_flexD.Row, ldt_dlg.Rows[i]["CD_PURGRP"].ToString());
                    SetCC(_flexD.Row, string.Empty);// C/C 적용 받아오는 부분 수주 C/C를 사용하지않으므로 C/C 재설정


                    DataTable dt = (DataTable)cbo_NM_EXCH.DataSource;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["CODE"].ToString() == cbo_NM_EXCH.SelectedValue.ToString())
                        {
                            _flexD["NM_EXCH"] = dr["NAME"];
                            break;
                        }
                    }
                    //if (ldt_dlg.Rows[i]["UNIT_PO"].ToString() != String.Empty)
                    //{

                    if (_flexD.CDecimal(_flexD["RT_PO"]) == 0)
                        _flexD["RT_PO"] = 1;

                    _flexD["QT_PO_MM"] = ldt_dlg.Rows[i]["QT_REM"];
                    _flexD["QT_PO"] = (D.GetDecimal(_flexD["QT_PO_MM"]) * D.GetDecimal(_flexD["RT_PO"]));
                    _flexD["NM_GRPMFG"] = ldt_dlg.Rows[i]["NM_GRPMFG"];
                    _flexD["GRP_MFG"] = ldt_dlg.Rows[i]["GRP_MFG"];
                    _flexD["NM_ITEMGRP"] = ldt_dlg.Rows[i]["GRP_ITEMNM"];
                    _flexD["GRP_ITEM"] = ldt_dlg.Rows[i]["GRP_ITEM"];
                    //}
                    //else
                    //{
                    //    _flexD["QT_PO_MM"] = _flexD.CDecimal(_flexD["QT_PO"]);
                    //}



                    FillPol(_flexD.Row);
                    object[] m_obj = new object[11];
                    m_obj[0] = _flexD["CD_ITEM"].ToString();
                    m_obj[1] = _flexD["CD_PLANT"].ToString();
                    m_obj[2] = LoginInfo.CompanyCode;
                    m_obj[3] = cbo_FG_UM.SelectedValue.ToString();
                    m_obj[4] = cbo_NM_EXCH.SelectedValue.ToString();
                    m_obj[5] = tb_DT_PO.Text;
                    m_obj[6] = tb_NM_PARTNER.CodeValue;
                    m_obj[7] = tb_NM_PURGRP.CodeValue;
                    m_obj[8] = "N";
                    m_obj[9] = D.GetString(_flexD["CD_PJT"]);
                    m_obj[10] = Global.MainFrame.ServerKeyCommon.ToUpper();

                    품목정보구하기(m_obj, "요청", 0);
                    부가세계산(_flexD.GetDataRow(_flexD.Row) );


                    if (Global.MainFrame.ServerKey == "SINJINSM" || Global.MainFrame.ServerKey == "SQL_108" || Global.MainFrame.ServerKey == "DZSQL")
                    {
                        DataTable dtSIN = _biz.Check_ITEMGRP_UM(D.GetString(tb_NM_PARTNER.CodeValue));

                        _flexD["CLS_L"] = ldt_dlg.Rows[i]["CLS_L"];
                        _flexD["CLS_M"] = ldt_dlg.Rows[i]["CLS_M"];
                        _flexD["CLS_S"] = ldt_dlg.Rows[i]["CLS_S"];
                        _flexD["NM_CLS_L"] = ldt_dlg.Rows[i]["NM_CLS_L"];
                        _flexD["NM_CLS_M"] = ldt_dlg.Rows[i]["NM_CLS_M"];
                        _flexD["NM_CLS_S"] = ldt_dlg.Rows[i]["NM_CLS_S"];

                        _flexD["QT_PO_MM"] = D.GetDecimal(ldt_dlg.Rows[i]["QT_REM"]);
                        _flexD["QT_PO"] = (D.GetDecimal(_flexD["QT_PO_MM"]) * D.GetDecimal(_flexD["RT_PO"]));
                        decimal dUM = D.GetDecimal(ldt_dlg.Rows[i]["UM"]);


                        if (dtSIN != null && dtSIN.Rows.Count > 0)
                        {
                            DataRow[] rowSIN = null;

                            if (D.GetString(_flexD["CLS_L"]) != "" && D.GetString(_flexD["GRP_ITEM"]) != "")
                            {
                                rowSIN = dtSIN.Select("CD_PARTNER = '" + D.GetString(tb_NM_PARTNER.CodeValue) + "' AND CLS_L ='" + D.GetString(_flexD["CLS_L"]) + "' AND GRP_ITEM ='" + D.GetString(_flexD["GRP_ITEM"]) + "'");
                            }
                            

                            if (rowSIN != null && rowSIN.Length > 0)
                            {
                                dUM = D.GetDecimal(ldt_dlg.Rows[i]["UM"]) * ( (D.GetDecimal(rowSIN[0]["UM_RT_ETC_1"]) * 0.01M));
                            }
                            else
                            {

                                rowSIN = dtSIN.Select("CD_PARTNER = '" + D.GetString(tb_NM_PARTNER.CodeValue) + "' AND CLS_L ='" + D.GetString(_flexD["CLS_L"]) + "'");

                                if (rowSIN != null && rowSIN.Length > 0)
                                    dUM = D.GetDecimal(ldt_dlg.Rows[i]["UM"]) * ((D.GetDecimal(rowSIN[0]["UM_RT_ETC_1"]) * 0.01M));
                                else
                                {

                                    rowSIN = dtSIN.Select("CD_PARTNER = '" + D.GetString(tb_NM_PARTNER.CodeValue) + "'");

                                    if (rowSIN != null && rowSIN.Length > 0)
                                        dUM = D.GetDecimal(ldt_dlg.Rows[i]["UM"]) * ((D.GetDecimal(rowSIN[0]["UM_RT_ETC_1"]) * 0.01M));
                                    else
                                        dUM = D.GetDecimal(ldt_dlg.Rows[i]["UM"]);
                                }
                            }

                            if (dUM != D.GetDecimal(ldt_dlg.Rows[i]["UM"]))
                                dUM = Math.Truncate(dUM / 10m) * 10m;

                        }

                        _flexD["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, dUM * (D.GetDecimal(ldt_dlg.Rows[i]["RT_PO"]) == 0 ? 1 : (D.GetDecimal(ldt_dlg.Rows[i]["RT_PO"]))));
                        _flexD["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, dUM);
                        _flexD["UM_P"] = Unit.원화단가(DataDictionaryTypes.PU, _flexD.CDecimal(_flexD["UM_EX_PO"]) * tb_NM_EXCH.DecimalValue);
                        _flexD["UM"] = Unit.원화단가(DataDictionaryTypes.PU, dUM * tb_NM_EXCH.DecimalValue);

                        _flexD["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(_flexD["UM_EX_PO"]) * _flexD.CDecimal(_flexD["QT_PO_MM"]));
                        _flexD["AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flexD["AM_EX"]) * tb_NM_EXCH.DecimalValue);

                        부가세만계산();
                    }

                    if (Global.MainFrame.ServerKeyCommon == "MHIK" || Global.MainFrame.ServerKeyCommon == "DZSQL")
                    {
                        _header.CurrentRow["DC50_PO"] = ldt_dlg.Rows[i]["DC_RMK"];
                        _header.CurrentRow["DC_RMK2"] = ldt_dlg.Rows[i]["DC_RMK1"];
                        tb_DC.Text = D.GetString(ldt_dlg.Rows[i]["DC_RMK"]);
                        tb_DC_RMK2.Text = D.GetString(ldt_dlg.Rows[i]["DC_RMK1"]);

                        _flexD["DC2"] = ldt_dlg.Rows[i]["DC1"];
                        _flexD["DC3"] = ldt_dlg.Rows[i]["DC2"];
                    }

                    _flexD.AddFinished();
                    _flexD.Col = _flexD.Cols.Fixed;
                }

                SUMFunction();
                SetHeadControlEnabled(false, 1);

               
            }
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
        finally
        {
            _flexD.Redraw = true;
        }
    }
    #endregion

    #region -> 전용 버튼 클릭 이벤트(btn_INST_Click)
    private void btn_INST_Click(object sender, EventArgs e)
    {
        try
        {
   

            if (((Control)sender).Text == "자재수급계획")
            {
                if (!HeaderCheck(0)) return;
                APP_ANJUN();  
            }
            else if (((Control)sender).Text == "계약적용")
            {
                APP_KPIC();
            }
            else
            {
                if (!HeaderCheck(0)) return;
                APP_INIT(sender, e);
            }

        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
        finally
        {
            _flexD.Redraw = true;
        }

    }



    #region -> 안전공업
    private void APP_ANJUN()
    {
        P_PU_Z_ANJUN_MS_SUB m_dlg = new P_PU_Z_ANJUN_MS_SUB(new object[] { D.GetString(cbo_CD_PLANT.SelectedValue) });

        if(m_dlg.ShowDialog() == DialogResult.OK)
        {
            DataTable dt_dlg = m_dlg.RtnTable;

            if(dt_dlg == null || dt_dlg.Rows.Count <= 0) return;

            _flexD.Redraw = false;
            //decimal MaxSeq = _flexD.GetMaxValue("NO_LINE");

            for (int i = 0; i < dt_dlg.Rows.Count; i++)
            {
                //_flexD.Rows.Add();
                //_flexD.Row = _flexD.Rows.Count - 1;

                //MaxSeq++;
                추가_Click(null, null);
                //_flexD["CD_PLANT"] = dt_dlg.Rows[i]["CD_PLANT"];
                _flexD["CD_ITEM"] = dt_dlg.Rows[i]["CD_ITEM"];
                _flexD["NM_ITEM"] = dt_dlg.Rows[i]["NM_ITEM"];
                _flexD["STND_ITEM"] = dt_dlg.Rows[i]["STND_ITEM"];
                _flexD["STND_DETAIL_ITEM"] = dt_dlg.Rows[i]["STND_DETAIL_ITEM"];
                _flexD["STND_MA_ITEM"] = dt_dlg.Rows[i]["STND_ITEM"];
                _flexD["CD_UNIT_MM"] = dt_dlg.Rows[i]["UNIT_PO"];
                _flexD["UNIT_PO"] = dt_dlg.Rows[i]["UNIT_PO"];
                _flexD["UNIT_IM"] = dt_dlg.Rows[i]["UNIT_IM"];
                _flexD["RT_PO"] = dt_dlg.Rows[i]["RT_PO"];
                _flexD["NM_ITEMGRP"] = dt_dlg.Rows[i]["NM_ITEMGRP"];
                _flexD["NM_GRPMFG"] = dt_dlg.Rows[i]["NM_GRPMFG"];
                _flexD["NM_CLS_ITEM"] = dt_dlg.Rows[i]["NM_CLS_ITEM"];
                _flexD["NO_MODEL"] = dt_dlg.Rows[i]["NO_MODEL"];
                _flexD["FG_IQCL"] = dt_dlg.Rows[i]["FG_IQCL"];
                _flexD["WEIGHT"] = dt_dlg.Rows[i]["WEIGHT"];
                _flexD["NM_MAKER"] = dt_dlg.Rows[i]["NM_MAKER"];
                _flexD["RATE_VAT"] = tb_TAX.DecimalValue;
                _flexD["CD_USERDEF1"] = dt_dlg.Rows[i]["STND_YYMM"];
                _flexD["CD_ITEMGRP"] = dt_dlg.Rows[i]["CD_ITEMGRP"];
                _flexD["MAT_ITEM"] = dt_dlg.Rows[i]["MAT_ITEM"];
                _flexD["NO_DESIGN"] = dt_dlg.Rows[i]["NO_DESIGN"];
                //_flexD["FG_POST"] = "O";
                //_flexD["NM_SYSDEF"] = _biz.GetGubunCodeSearch("PU_C000009", _flexD["FG_POST"].ToString());//_ComfirmState;
                _flexD["QT_PO"] = dt_dlg.Rows[i]["QT12"];

                if (D.GetDecimal(_flexD["RT_PO"]) == 0)
                    _flexD["RT_PO"] = 1;

                _flexD["QT_PO_MM"] = (D.GetDecimal(dt_dlg.Rows[i]["QT12"]) / D.GetDecimal(_flexD["RT_PO"]));

                if (tb_NO_PO.Text != string.Empty)
                    _flexD["NO_PO"] = tb_NO_PO.Text;
                //_flexD["NO_LINE"] = MaxSeq;
                //_flexD["CD_EXCH"] = cbo_NM_EXCH.SelectedValue.ToString();

                SetCC(_flexD.Row, string.Empty);

                //object[] m_obj = new object[11];
                //m_obj[0] = _flexD["CD_ITEM"].ToString();
                //m_obj[1] = _flexD["CD_PLANT"].ToString();
                //m_obj[2] = LoginInfo.CompanyCode;
                //m_obj[3] = cbo_FG_UM.SelectedValue.ToString();
                //m_obj[4] = cbo_NM_EXCH.SelectedValue.ToString();
                //m_obj[5] = tb_DT_PO.Text;
                //m_obj[6] = tb_NM_PARTNER.CodeValue;
                //m_obj[7] = tb_NM_PURGRP.CodeValue;
                //m_obj[8] = "N";
                //m_obj[9] = D.GetString(_flexD["CD_PJT"]);
                //m_obj[10] = Global.MainFrame.ServerKeyCommon.ToUpper();
                //품목정보구하기(m_obj, "수급", 0);

                //DataTable dt = (DataTable)cbo_NM_EXCH.DataSource;
                //foreach (DataRow dr in dt.Rows)
                //{
                //    if (dr["CODE"].ToString() == cbo_NM_EXCH.SelectedValue.ToString())
                //    {
                //        _flexD["NM_EXCH"] = dr["NAME"];
                //        break;

                //    }
                //}

                //FillPol(_flexD.Row);

                if (sPUSU == "100")
                    GET_SU_BOM();   //품목입력시 해당품목의 외주BOM(SU_BOM)을 가져오는 구문
            }


            //_flexD.AddFinished();
            //_flexD.Col = _flexD.Cols.Fixed;

            SetHeadControlEnabled(false, 1);
        }
    }
      #endregion

    #region -> 이니텍
    private void APP_INIT(object sender, EventArgs e)
    {
        

        호출여부 = true;

        pur.P_PU_PO_INST_INITECH_SUB m_dlg = new pur.P_PU_PO_INST_INITECH_SUB();
        Cursor.Current = Cursors.Default;

        if (m_dlg.ShowDialog(this) == DialogResult.OK)
        {
            DataTable dt_dlg = m_dlg.손익매입데이터;
            if (dt_dlg == null || dt_dlg.Rows.Count <= 0) return;

            if (!Partner_Accept(dt_dlg)) return;        // 거래처 매칭

            ControlButtonEnabledDisable((Control)sender, true);
            cbo_CD_PLANT.Enabled = false;

            _flexD.Redraw = false;
            decimal MaxSeq = _flexD.GetMaxValue("NO_LINE");

            for (int i = 0; i < dt_dlg.Rows.Count; i++)
            {
                if (dt_dlg.Rows[i].RowState == DataRowState.Deleted)
                    continue;

                MaxSeq++;
                _flexD.Rows.Add();
                _flexD.Row = _flexD.Rows.Count - 1;

                _flexD["CD_ITEM"] = dt_dlg.Rows[i]["CD_ITEM_BUY"];
                _flexD["NM_ITEM"] = dt_dlg.Rows[i]["NM_ITEM_BUY"];
                _flexD["STND_ITEM"] = dt_dlg.Rows[i]["STND_ITEM"];
                _flexD["CD_UNIT_MM"] = dt_dlg.Rows[i]["UNIT_PO"];
                _flexD["UNIT_PO"] = dt_dlg.Rows[i]["UNIT_PO"];
                _flexD["STND_MA_ITEM"] = dt_dlg.Rows[i]["STND_ITEM"];
                _flexD["UNIT_IM"] = dt_dlg.Rows[i]["UNIT_IM"];
                //2010.01.12 납기일
                if (tb_DT_LIMIT.Text == string.Empty)
                    _flexD["DT_LIMIT"] = dt_dlg.Rows[i]["DT_DUEDATE"];
                else
                    _flexD["DT_LIMIT"] = tb_DT_LIMIT.Text;

                _flexD["DT_PLAN"] = _flexD["DT_LIMIT"];

                _flexD["FG_POST"] = "O"; //OPEN
                //_flexD["NM_SYSDEF"] = _ComfirmState;                        
                _flexD["NM_SYSDEF"] = _biz.GetGubunCodeSearch("PU_C000009", _flexD["FG_POST"].ToString());//_ComfirmState;


                _flexD["RT_PO"] = D.GetDecimal(dt_dlg.Rows[i]["RT_PO"]);
                _flexD["CD_PJT"] = dt_dlg.Rows[i]["NO_PROJECT"];
                _flexD["NM_PJT"] = dt_dlg.Rows[i]["NM_PROJECT"];
                _flexD["SEQ_PROJECT"] = dt_dlg.Rows[i]["SEQ_PROJECT"];
                _flexD["NO_PR"] = "";
                _flexD["NO_PRLINE"] = 0;
                _flexD["NO_CONTRACT"] = dt_dlg.Rows[i]["NO_INST"];
                _flexD["NUM_USERDEF1"] = dt_dlg.Rows[i]["NO_INST_HST"];
                _flexD["NO_CTLINE"] = dt_dlg.Rows[i]["SEQ_INST"];

                _flexD["NO_SO"] = null;   // 수주번호
                _flexD["NO_SOLINE"] = null;  // 수주항번

                _flexD["CD_PLANT"] = dt_dlg.Rows[i]["CD_PLANT"];
                if (tb_NO_PO.Text != string.Empty)
                    _flexD["NO_PO"] = tb_NO_PO.Text;
                _flexD["NO_RCV"] = "";
                _flexD["NO_LINE"] = MaxSeq;
                _flexD["CD_SL"] = dt_dlg.Rows[i]["CD_SL"];
                _flexD["NM_SL"] = dt_dlg.Rows[i]["NM_SL"];
                _flexD["CD_EXCH"] = cbo_NM_EXCH.SelectedValue.ToString();
                _flexD["DC1"] = dt_dlg.Rows[i]["CD_PARTNER"];
                _flexD["DC2"] = dt_dlg.Rows[i]["LN_PARTNER"];
                _flexD["CD_USERDEF1"] = dt_dlg.Rows[i]["FG_GUBUN"];

                ////구매예산통제관련 추가 20091201
                //_flexD["CD_BUDGET"] = dt_dlg.Rows[i]["CD_BUDGET"]; //예산단위코드
                //_flexD["NM_BUDGET"] = dt_dlg.Rows[i]["NM_BUDGET"];
                //_flexD["CD_BGACCT"] = dt_dlg.Rows[i]["CD_BGACCT"]; //예산계정코드
                //_flexD["NM_BGACCT"] = dt_dlg.Rows[i]["NM_BGACCT"];

                // 2009.12.08 다시 개발 cc설정 관련
                // LINE 수정 권한이면 요청에서 적용받는다 
                // 요청에 없으면 HEADER 설정에 따라 적용받는다.
                // LINE 수정권한이 없으면 HEADER 설정에 따라 적용받는다.

                //SetCC_Line(_flexD.Row, dt_dlg.Rows[i]["CD_PURGRP"].ToString());
                SetCC(_flexD.Row, string.Empty);// C/C 적용 받아오는 부분 수주 C/C를 사용하지않으므로 C/C 재설정


                DataTable dt = (DataTable)cbo_NM_EXCH.DataSource;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["CODE"].ToString() == cbo_NM_EXCH.SelectedValue.ToString())
                    {
                        _flexD["NM_EXCH"] = dr["NAME"];
                        break;
                    }
                }
                //if (dt_dlg.Rows[i]["UNIT_PO"].ToString() != String.Empty)
                //{

                if (_flexD.CDecimal(_flexD["RT_PO"]) == 0)
                    _flexD["RT_PO"] = 1;

                _flexD["QT_PO_MM"] = D.GetDecimal(dt_dlg.Rows[i]["QT_REM"]);
                _flexD["QT_PO"] = (D.GetDecimal(_flexD["QT_PO_MM"]) * D.GetDecimal(_flexD["RT_PO"]));
                //}
                //else
                //{
                //    _flexD["QT_PO_MM"] = _flexD.CDecimal(_flexD["QT_PO"]);
                //}

                FillPol(_flexD.Row);
                object[] m_obj = new object[11];
                m_obj[0] = _flexD["CD_ITEM"].ToString();
                m_obj[1] = _flexD["CD_PLANT"].ToString();
                m_obj[2] = LoginInfo.CompanyCode;
                m_obj[3] = cbo_FG_UM.SelectedValue.ToString();
                m_obj[4] = cbo_NM_EXCH.SelectedValue.ToString();
                m_obj[5] = tb_DT_PO.Text;
                m_obj[6] = tb_NM_PARTNER.CodeValue;
                m_obj[7] = tb_NM_PURGRP.CodeValue;
                m_obj[8] = "N";
                m_obj[9] = D.GetString(_flexD["CD_PJT"]);
                m_obj[10] = Global.MainFrame.ServerKeyCommon.ToUpper();

                품목정보구하기(m_obj, "손익", 0);

                if (D.GetDecimal(_flexD["UM_EX_PO"]) == 0)
                {
                    _flexD["UM_EX_PO"] = dt_dlg.Rows[i]["UM_ITEM"];
                    if (_flexD.CDecimal(_flexD["RT_PO"]) == 0)
                    {
                        _flexD["UM_EX"] = dt_dlg.Rows[i]["UM_ITEM"];
                        _flexD["QT_PO"] = D.GetDecimal(_flexD["QT_PO_MM"]);
                    }
                    else
                    {
                        _flexD["UM_EX"] = D.GetDecimal(dt_dlg.Rows[i]["UM_ITEM"]) / D.GetDecimal(_flexD["RT_PO"]);
                        _flexD["QT_PO"] = D.GetDecimal(_flexD["QT_PO_MM"]) * D.GetDecimal(_flexD["RT_PO"]);
                    }

                    _flexD["UM_P"] = D.GetDecimal(_flexD["UM_EX_PO"]) * tb_NM_EXCH.DecimalValue;
                    _flexD["UM"] = D.GetDecimal(_flexD["UM_EX"]) * tb_NM_EXCH.DecimalValue;
                    _flexD["AM_EX"] = D.GetDecimal(_flexD["UM_EX_PO"]) * D.GetDecimal(_flexD["QT_PO_MM"]);
                }
                부가세계산(_flexD.GetDataRow(_flexD.Row));

                _flexD.AddFinished();
                _flexD.Col = _flexD.Cols.Fixed;
            }

            SUMFunction();
            SetHeadControlEnabled(false, 1);
        }
    }
    #endregion

    #region -> KPIC
    private void APP_KPIC()
    {
        try
        {

            pur.P_PU_Z_KPCI_CONTRACT_SUB m_dlg = new pur.P_PU_Z_KPCI_CONTRACT_SUB();

            if (m_dlg.ShowDialog() == DialogResult.OK)
            {
                DataTable dt_dlg = m_dlg.rtnDATA;

                if (dt_dlg == null || dt_dlg.Rows.Count <= 0) return;

                _flexD.Redraw = false;


                _header.CurrentRow["CD_PARTNER"] = dt_dlg.Rows[0]["CD_PARTNER"];
                _header.CurrentRow["LN_PARTNER"] = dt_dlg.Rows[0]["LN_PARTNER"];

                tb_NM_PARTNER.CodeValue = D.GetString(dt_dlg.Rows[0]["CD_PARTNER"]);
                tb_NM_PARTNER.CodeName = D.GetString(dt_dlg.Rows[0]["LN_PARTNER"]);

                _header.CurrentRow["CD_PURGRP"] = dt_dlg.Rows[0]["CD_PURGRP"];
                _header.CurrentRow["NM_PURGRP"] = dt_dlg.Rows[0]["NM_PURGRP"];

                tb_NM_PURGRP.CodeValue = D.GetString(dt_dlg.Rows[0]["CD_PURGRP"]);
                tb_NM_PURGRP.CodeName = D.GetString(dt_dlg.Rows[0]["NM_PURGRP"]);

                cbo_NM_EXCH.SelectedValue = D.GetString(dt_dlg.Rows[0]["CD_EXCH"]);
                _header.CurrentRow["CD_EXCH"] = dt_dlg.Rows[0]["CD_EXCH"];
                txtDcRmkIv.Text = D.GetString(dt_dlg.Rows[0]["NO_ORDER"]);
                tb_DC.Text = D.GetString(dt_dlg.Rows[0]["NO_ORDER"]);

                Tppo_Accept(dt_dlg,"전용");

                DataTable dt = _biz.search_partner(tb_NM_PARTNER.CodeValue);

                if (dt != null && dt.Rows.Count > 0)
                {
                    if (D.GetString(dt.Rows[0]["FG_PAYBILL"]) != string.Empty)
                        cbo_FG_PAYBILL_IV.SelectedValue = D.GetString(dt.Rows[0]["FG_PAYBILL"]);

                    _header.CurrentRow["FG_PAYBILL_IV"] = D.GetString(dt.Rows[0]["FG_PAYBILL"]);
                }

                if (!HeaderCheck(0)) return;


                for (int i = 0; i < dt_dlg.Rows.Count; i++)
                {
                    추가_Click(null, null);
                    _flexD["CD_ITEM"] = dt_dlg.Rows[i]["CD_ITEM"];
                    _flexD["NM_ITEM"] = dt_dlg.Rows[i]["NM_ITEM"];
                    _flexD["STND_ITEM"] = dt_dlg.Rows[i]["STND_ITEM"];
                    _flexD["STND_DETAIL_ITEM"] = dt_dlg.Rows[i]["STND_DETAIL_ITEM"];
                    _flexD["STND_MA_ITEM"] = dt_dlg.Rows[i]["STND_ITEM"];
                    _flexD["CD_UNIT_MM"] = dt_dlg.Rows[i]["UNIT_PO"];
                    _flexD["UNIT_PO"] = dt_dlg.Rows[i]["UNIT_PO"];
                    _flexD["UNIT_IM"] = dt_dlg.Rows[i]["UNIT_IM"];
                    _flexD["RT_PO"] = dt_dlg.Rows[i]["RT_PO"];
                    _flexD["NM_ITEMGRP"] = dt_dlg.Rows[i]["NM_ITEMGRP"];
                    _flexD["NM_GRPMFG"] = dt_dlg.Rows[i]["NM_GRPMFG"];
                    _flexD["NM_CLS_ITEM"] = dt_dlg.Rows[i]["NM_CLS_ITEM"];
                    _flexD["NO_MODEL"] = dt_dlg.Rows[i]["NO_MODEL"];
                    _flexD["FG_IQCL"] = dt_dlg.Rows[i]["FG_IQCL"];
                    _flexD["WEIGHT"] = dt_dlg.Rows[i]["WEIGHT"];
                    _flexD["NM_MAKER"] = dt_dlg.Rows[i]["NM_MAKER"];
                    _flexD["RATE_VAT"] = tb_TAX.DecimalValue;
                    _flexD["CD_ITEMGRP"] = dt_dlg.Rows[i]["CD_ITEMGRP"];
                    _flexD["MAT_ITEM"] = dt_dlg.Rows[i]["MAT_ITEM"];
                    _flexD["NO_DESIGN"] = dt_dlg.Rows[i]["NO_DESIGN"];
                    decimal dRtPo = (D.GetDecimal(dt_dlg.Rows[i]["RT_PO"]) == 0) ? 1 : D.GetDecimal(dt_dlg.Rows[i]["RT_PO"]);
                    _flexD["QT_PO"] = D.GetDecimal(dt_dlg.Rows[i]["QT_CON"]) * dRtPo;
                    _flexD["AM_EX"] = D.GetDecimal(dt_dlg.Rows[i]["AM_AMOUNT_BUY"]);
                    _flexD["DC1"] = dt_dlg.Rows[i]["NO_CONTRACT"];

                  
                    _flexD["AM"] =  D.GetDecimal(_flexD["AM_EX"]) * tb_NM_EXCH.DecimalValue;
                    
                    _flexD["RATE_VAT"] = (_flexD.CDecimal(tb_TAX.DecimalValue) == 0) ? 0 : _flexD.CDecimal(tb_TAX.DecimalValue) * 0.01M;
                    _flexD["NO_RELATION"] = dt_dlg.Rows[i]["NO_CONTRACT"];
                    _flexD["SEQ_RELATION"] = dt_dlg.Rows[i]["NO_LINE"];
                    _flexD["CD_SL"] = dt_dlg.Rows[i]["CD_SL"];
                    _flexD["NM_SL"] = dt_dlg.Rows[i]["NM_SL"];

                    부가세만계산();

                    if (D.GetDecimal(_flexD["RT_PO"]) == 0)
                        _flexD["RT_PO"] = 1;

                    _flexD["QT_PO_MM"] = (D.GetDecimal(dt_dlg.Rows[i]["QT_CON"]));

                    if (tb_NO_PO.Text != string.Empty)
                        _flexD["NO_PO"] = tb_NO_PO.Text;

                    _flexD["UM_EX_PO"] = dt_dlg.Rows[i]["UM_UNIT_BUY"];
                    _flexD["UM_EX"] = (D.GetDecimal(dt_dlg.Rows[i]["UM_UNIT_BUY"]) * _flexD.CDecimal(_flexD["QT_PO"]));

                    _flexD["UM"] = Unit.원화단가(DataDictionaryTypes.PU, (D.GetDecimal(dt_dlg.Rows[i]["UM_UNIT_BUY"]) * tb_NM_EXCH.DecimalValue));
                    _flexD["UM_P"] = Unit.원화단가(DataDictionaryTypes.PU, (D.GetDecimal(dt_dlg.Rows[i]["UM_UNIT_BUY"]) * _flexD.CDecimal(_flexD["QT_PO"]) * tb_NM_EXCH.DecimalValue));

                    //_flexD["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(_flexD["AM_EX"]) / D.GetDecimal(_flexD["QT_PO_MM"])); 
                    //_flexD["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU,  (D.GetDecimal(_flexD["AM_EX"]) / _flexD.CDecimal(_flexD["QT_PO"])));  // 외화 재고단위 단가                

                    //_flexD["UM"] = Unit.원화단가(DataDictionaryTypes.PU, (D.GetDecimal(_flexD["AM_EX"]) / (D.GetDecimal(_flexD["QT_PO_MM"]))) * tb_NM_EXCH.DecimalValue);
                    //_flexD["UM_P"] = Unit.원화단가(DataDictionaryTypes.PU, (D.GetDecimal(_flexD["AM_EX"]) / (D.GetDecimal(_flexD["QT_PO"])) * tb_NM_EXCH.DecimalValue));

                    SetCC(_flexD.Row, string.Empty);

                    if (sPUSU == "100")
                        GET_SU_BOM();

                }


                cbo_NM_EXCH.SelectedValue = D.GetString(dt_dlg.Rows[0]["CD_EXCH"]);
                _header.CurrentRow["CD_EXCH"] = dt_dlg.Rows[0]["CD_EXCH"];

                if (D.GetString(cbo_NM_EXCH.SelectedValue) == "000")  // 원화
                {
                    tb_NM_EXCH.DecimalValue = 1m;
                    _header.CurrentRow["RT_EXCH"] = 1;
                    tb_NM_EXCH.Enabled = false;
                }
                else
                {

                    decimal ld_rate_base = MA.기준환율적용(tb_DT_PO.Text, D.GetString(cbo_NM_EXCH.SelectedValue.ToString()));
                    if (ld_rate_base == 0)
                        ld_rate_base = 1;           

                    tb_NM_EXCH.Text = ld_rate_base.ToString();
                    _header.CurrentRow["RT_EXCH"] = ld_rate_base;
                    tb_NM_EXCH.Enabled = false;
                }

                if (D.GetString(dt_dlg.Rows[0]["EXPORT_KIND"]) == "100")
                {
                    string sDate = dtp_DT_DUE_IV.Text.Replace('/',' ');

                     dt = _biz.search_dt(sDate);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dtp_DT_DUE_IV.Text = D.GetString(dt.Rows[4]["DT_CAL"]);
                    }

                 
                }
                else if (D.GetString(dt_dlg.Rows[0]["EXPORT_KIND"]) == "200")
                {
                    string sDate = dtp_DT_DUE_IV.Text.Replace('/', ' ');

                     dt = _biz.search_dt(sDate);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dtp_DT_DUE_IV.Text = D.GetString(dt.Rows[6]["DT_CAL"]);
                    }
                }

                SetExchageMoney();
                SetHeadControlEnabled(false, 1);


                btn_INST.Enabled = false;
            }


        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
    }
    #endregion

    #endregion

    #region -> 재고확인 버튼 클릭 이벤트(btn_CM_INV_Click)

    private void btn_CM_INV_Click(object sender, System.EventArgs e)
    {
        try
        {
            if (!HeaderCheck(0)) return;

            if (_flexD.HasNormalRow)
            {
                object[] m_obj = new object[11];
                m_obj[0] = _flexD["CD_ITEM"].ToString();
                m_obj[1] = _flexD["CD_PLANT"].ToString();
                m_obj[2] = LoginInfo.CompanyCode;
                m_obj[3] = cbo_FG_UM.SelectedValue.ToString();
                m_obj[4] = cbo_NM_EXCH.SelectedValue.ToString();
                m_obj[5] = tb_DT_PO.Text;
                m_obj[6] = tb_NM_PARTNER.CodeValue;
                m_obj[7] = tb_NM_PURGRP.CodeValue;
                m_obj[8] = "N";
                m_obj[9]  = D.GetString(_flexD["CD_PJT"]);
                m_obj[10] = Global.MainFrame.ServerKeyCommon.ToUpper();

                품목정보구하기(m_obj, "재고", 0);
                SetQtValue(_flexD.Row);
            }
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
    }

    #endregion

    #region -> 전자결제 버튼 클릭 이벤트(btn전자결제_Click)
    private void btn전자결제_Click(object sender, EventArgs e)
    {
        try
        {
            if (!HeaderCheck(0)) return;
            if (m_Elec_app == "000") return;

            //2:미상신 0:진행중 1:종결 -1:반려 3:취소(삭제)
            string[] st_stat_msg = new string[5] { "진행", "종결", "미상신", "취소", "반려" };

            int i_stat = _biz.GetFI_GWDOCU(_header.CurrentRow["NO_PO"].ToString());

            bool save_true = true;
            if (i_stat != 999)
            {
                if (i_stat == -1) i_stat = 4;
                ShowMessage("전자결제 " + st_stat_msg[i_stat] + " 중 입니다.");
                save_true = false;
                //return;
            }

            #region -> 108 개발서버 결제상신 로직

            bool bTrue = true;

            if (MainFrameInterface.ServerKey == "108" || MainFrameInterface.ServerKey == "DZSQL" || MainFrameInterface.ServerKey == "DZORA")
            {
                string s_전자결재양식생성 = 전자결재양식생성_백광();

                bTrue = _biz.전자결재_108(_header.CurrentRow, /*_flex.DataView.ToTable(),*/ s_전자결재양식생성);

                if (bTrue)
                {
                    //_header.CurrentRow["YN_REPORT"] = "Y";
                    //btn_결제상신.Enabled = false;
                    //btn_Excel_Upload.Enabled = false;
                    ControlButtonEnabledDisable(null, false);
                    this.ShowMessage("전자결재가 완료되었습니다.");

                    if (tb_NO_PO.Text.Trim() != "" && (MainFrameInterface.ServerKey == "PKIC" || MainFrameInterface.ServerKey == "DZSQL"))//전자결재 한번올려진것은 버튼 비활성화 : 백광
                    { check_GW(tb_NO_PO.Text); }
                }
                return;
            }

            #endregion

            #region -> 실제 업체 결재상신 로직
            #region -> 대림
            if (MainFrameInterface.ServerKey == "DEM")
            {
                string strContents = 전자결재양식생성_대림();

                if (strContents == "false")
                    return;
                string App_Form_Kind = "3010";
                bTrue = _biz.전자결재_실제사용(_header.CurrentRow, strContents, App_Form_Kind,"");// App_Form_Kind를 추가한것은 회사마다 번호가 각각 틀리기때문에 따로 넘겨줘야 하므로 

                if (!bTrue) return;

                string strURL = "";
                string NO_DOCU = _header.CurrentRow["NO_PO"].ToString();
                //한글처리를 위해서 Encode 사용! 
                strURL = BASIC.GetMAENV("GROUPWARE_URL") + "/kor_webroot/src/cm/tims/index.aspx?cd_company=" + LoginInfo.CompanyCode + "&cd_pc=" + LoginInfo.CdPc + "&no_docu=" + System.Web.HttpUtility.UrlEncode(NO_DOCU, Encoding.UTF8) + "&login_id=" + LoginInfo.EmployeeNo;

                System.Diagnostics.Process.Start("IExplore.exe", strURL);

                //_header.CurrentRow["YN_REPORT"] = "Y";
                //_header.CurrentRow["ST_STAT"] = "2";    // 2 : 미상신
                //btn_Excel_Upload.Enabled = false;

                ControlButtonEnabledDisable(null, false);
                ShowMessage("전자결재 처리가 완료 되었습니다.");

                //결재상신 완료 후 재조회
                //Search_SaveAfter();

            }
            #endregion
            #region -> 백광
            else if (m_Elec_app == "002")
            {
                string strContents = 전자결재양식생성_백광();

                if (strContents == "false")
                    return;
                string App_Form_Kind = "3000";
                bTrue = _biz.전자결재_실제사용(_header.CurrentRow, strContents, App_Form_Kind,"");// App_Form_Kind를 추가한것은 회사마다 App_Form_Kind 번호가 중복되는 것들이 있기 때문에 따로 넘겨줘야 하므로 

                if (!bTrue) return;

                string strURL = "";
                string NO_DOCU = _header.CurrentRow["NO_PO"].ToString();
                //한글처리를 위해서 Encode 사용!
                strURL = BASIC.GetMAENV("GROUPWARE_URL");
                strURL += "/kor_webroot/src/cm/tims/index.aspx?cd_company=" + LoginInfo.CompanyCode + "&cd_pc=" + LoginInfo.CdPc + "&no_docu=" + System.Web.HttpUtility.UrlEncode(NO_DOCU, Encoding.UTF8) + "&login_id=" + LoginInfo.EmployeeNo;

                System.Diagnostics.Process.Start("IExplore.exe", strURL);

                //_header.CurrentRow["YN_REPORT"] = "Y";
                //_header.CurrentRow["ST_STAT"] = "2";    // 2 : 미상신
                //btn_Excel_Upload.Enabled = false;

                ControlButtonEnabledDisable(null, false);
                ShowMessage("전자결재 처리가 완료 되었습니다.");

                //check_GW(_header.CurrentRow["NO_PR"].ToString());
                //결재상신 완료 후 재조회
                //Search_SaveAfter();
                if (tb_NO_PO.Text.Trim() != "" && (MainFrameInterface.ServerKey == "PKIC" || MainFrameInterface.ServerKey == "DZSQL" || MainFrameInterface.ServerKey == "108"))//전자결재 한번올려진것은 버튼 비활성화 : 백광
                { check_GW(tb_NO_PO.Text); }

            }
            #endregion
            #region -> 지엔텔
            else if (m_Elec_app == "003")//지엔텔 전자결재사용
            {
                string strContents = 전자결재양식생성_지엔텔();

                if (strContents == "false")
                    return;

                string App_Form_Kind = "3000";//변경해야함
                bTrue = _biz.전자결재_실제사용(_header.CurrentRow, strContents, App_Form_Kind,"");// App_Form_Kind를 추가한것은 회사마다 App_Form_Kind 번호가 중복되는 것들이 있기 때문에 따로 넘겨줘야 하므로 

                if (!bTrue) return;

                string strURL = "";
                string NO_DOCU = _header.CurrentRow["NO_PO"].ToString();
                //한글처리를 위해서 Encode 사용!
                strURL = "http://gw.lgntel.com/kor_webroot/src/cm/tims/index.aspx?cd_company=" + LoginInfo.CompanyCode + "&cd_pc=" + LoginInfo.CdPc + "&no_docu=" + System.Web.HttpUtility.UrlEncode(NO_DOCU, Encoding.UTF8) + "&login_id=" + LoginInfo.EmployeeNo;

                System.Diagnostics.Process.Start("IExplore.exe", strURL);

                //_header.CurrentRow["YN_REPORT"] = "Y";
                //_header.CurrentRow["ST_STAT"] = "2";    // 2 : 미상신
                //btn_Excel_Upload.Enabled = false;

                ControlButtonEnabledDisable(null, false);
                ShowMessage("전자결재 처리가 완료 되었습니다.");

                //check_GW(_header.CurrentRow["NO_PR"].ToString());
                //결재상신 완료 후 재조회
                //Search_SaveAfter();
                if (tb_NO_PO.Text.Trim() != "" && (MainFrameInterface.ServerKey == "GNTU" || MainFrameInterface.ServerKey == "DZSQL" || MainFrameInterface.ServerKey == "108"))//전자결재 한번올려진것은 버튼 비활성화 : 백광
                { check_GW(tb_NO_PO.Text); }

            }
            #endregion
            #region -> 넥스트아이
            else if (m_Elec_app == "004")//넥스트아이 전자결재사용
            {
                string strContents = GW_Nexti_html();

                if (strContents == "false")
                    return;

                string App_Form_Kind = "2400";//변경해야함
                bTrue = _biz.전자결재_실제사용(_header.CurrentRow, strContents, App_Form_Kind,"");// App_Form_Kind를 추가한것은 회사마다 App_Form_Kind 번호가 중복되는 것들이 있기 때문에 따로 넘겨줘야 하므로 

                if (!bTrue) return;

                string strURL = "";
                string NO_DOCU = _header.CurrentRow["NO_PO"].ToString();
                //한글처리를 위해서 Encode 사용!
                strURL = BASIC.GetMAENV("GROUPWARE_URL") + "/kor_webroot/src/cm/tims/index.aspx?cd_company=" + LoginInfo.CompanyCode + "&cd_pc=" + LoginInfo.CdPc + "&no_docu=" + System.Web.HttpUtility.UrlEncode(NO_DOCU, Encoding.UTF8) + "&login_id=" + LoginInfo.EmployeeNo;

                System.Diagnostics.Process.Start("IExplore.exe", strURL);

                //_header.CurrentRow["YN_REPORT"] = "Y";
                //_header.CurrentRow["ST_STAT"] = "2";    // 2 : 미상신
                //btn_Excel_Upload.Enabled = false;

                ControlButtonEnabledDisable(null, false);
                ShowMessage("전자결재 처리가 완료 되었습니다.");

                //check_GW(_header.CurrentRow["NO_PR"].ToString());
                //결재상신 완료 후 재조회
                //Search_SaveAfter();
                if (tb_NO_PO.Text.Trim() != "" && (m_Elec_app == "004" || MainFrameInterface.ServerKey == "DZSQL" || MainFrameInterface.ServerKey == "108"))//전자결재 한번올려진것은 버튼 비활성화 : 백광
                { check_GW(tb_NO_PO.Text); }

            }
            #endregion
            #region -> 쏠리테크
            else if (m_Elec_app == "006")//쏠리테크 전자결재사용
            {
                string strContents = GW_Solid_html();

                if (strContents == "false")
                    return;

                string App_Form_Kind = "2200";//변경해야함

                if (save_true)
                    bTrue = _biz.전자결재_실제사용(_header.CurrentRow, strContents, App_Form_Kind,"");// App_Form_Kind를 추가한것은 회사마다 App_Form_Kind 번호가 중복되는 것들이 있기 때문에 따로 넘겨줘야 하므로 

                if (!bTrue) return;

                string strURL = "";
                string NO_DOCU = _header.CurrentRow["NO_PO"].ToString();
                //한글처리를 위해서 Encode 사용!
                strURL = "http://192.168.155.2/kor_webroot/src/cm/tims/index.aspx?cd_company=" + LoginInfo.CompanyCode + "&cd_pc=" + LoginInfo.CdPc + "&no_docu=" + System.Web.HttpUtility.UrlEncode(NO_DOCU, Encoding.UTF8) + "&login_id=" + LoginInfo.EmployeeNo;

                System.Diagnostics.Process.Start("IExplore.exe", strURL);

                //_header.CurrentRow["YN_REPORT"] = "Y";
                //_header.CurrentRow["ST_STAT"] = "2";    // 2 : 미상신
                //btn_Excel_Upload.Enabled = false;

                ControlButtonEnabledDisable(null, false);
                ShowMessage("전자결재 처리가 완료 되었습니다.");

                //check_GW(_header.CurrentRow["NO_PR"].ToString());
                //결재상신 완료 후 재조회
                //Search_SaveAfter();
                //if (tb_NO_PO.Text.Trim() != "" && (m_Elec_app == "006" || MainFrameInterface.ServerKey == "DZSQL" || MainFrameInterface.ServerKey == "108"))//전자결재 한번올려진것은 버튼 비활성화 : 백광
                //{ check_GW(tb_NO_PO.Text); }

            }
            #endregion
            #region -> 광진윈텍
            else if (m_Elec_app == "010")//광진윈텍 전자결재사용
            {
                string strContents = 전자결재양식생성_광진윈텍();

                if (strContents == "false")
                    return;

                string App_Form_Kind = "2103";//변경해야함
                bTrue = _biz.전자결재_실제사용(_header.CurrentRow, strContents, App_Form_Kind,"");// App_Form_Kind를 추가한것은 회사마다 App_Form_Kind 번호가 중복되는 것들이 있기 때문에 따로 넘겨줘야 하므로 

                if (!bTrue) return;

                string strURL = "";
                string NO_DOCU = _header.CurrentRow["NO_PO"].ToString();
                //한글처리를 위해서 Encode 사용!
                strURL = "http://smart.kwangjinwintec.com/kor_webroot/src/cm/tims/index.aspx?cd_company=" + LoginInfo.CompanyCode + "&cd_pc=" + LoginInfo.CdPc + "&no_docu=" + System.Web.HttpUtility.UrlEncode(NO_DOCU, Encoding.UTF8) + "&login_id=" + LoginInfo.EmployeeNo;

                System.Diagnostics.Process.Start("IExplore.exe", strURL);

                //_header.CurrentRow["YN_REPORT"] = "Y";
                //_header.CurrentRow["ST_STAT"] = "2";    // 2 : 미상신
                //btn_Excel_Upload.Enabled = false;

                ControlButtonEnabledDisable(null, false);
                ShowMessage("전자결재 처리가 완료 되었습니다.");

                //check_GW(_header.CurrentRow["NO_PR"].ToString());
                //결재상신 완료 후 재조회
                //Search_SaveAfter();
                if (tb_NO_PO.Text.Trim() != "" && (MainFrameInterface.ServerKey == "KJWT" || MainFrameInterface.ServerKey == "DZSQL" || MainFrameInterface.ServerKey == "108"))//전자결재 한번올려진것은 버튼 비활성화 : 백광
                { check_GW(tb_NO_PO.Text); }

            }
            #endregion
            #region -> 디엠티
            else if (m_Elec_app == "011" || m_Elec_app =="035")//디엠티, 본아이에프 전자결재사용
            {
                string strContents = 전자결재양식생성_디엠티();

                if (strContents == "false")
                    return;

                string App_Form_Kind = "2001";
                string Nm_Pumm = "";

                if (m_Elec_app == "035")
                {
                    App_Form_Kind = "2000";
                    Nm_Pumm = "구매발주서_" + D.GetString(_header.CurrentRow["NO_PO"]);
                }

                bTrue = _biz.전자결재_실제사용(_header.CurrentRow, strContents, App_Form_Kind, Nm_Pumm);// App_Form_Kind를 추가한것은 회사마다 App_Form_Kind 번호가 중복되는 것들이 있기 때문에 따로 넘겨줘야 하므로 

                if (!bTrue) return;

                string strURL = "";
                string NO_DOCU = _header.CurrentRow["NO_PO"].ToString();
                //한글처리를 위해서 Encode 사용!
                if (m_Elec_app == "011")
                    strURL = "http://biz.dmt.kr:8088/kor_webroot/src/cm/tims/index.aspx?cd_company=" + LoginInfo.CompanyCode + "&cd_pc=" + LoginInfo.CdPc + "&no_docu=" + System.Web.HttpUtility.UrlEncode(NO_DOCU, Encoding.UTF8) + "&login_id=" + LoginInfo.EmployeeNo;
                else
                {
                    strURL = BASIC.GetMAENV("GROUPWARE_URL");
                    strURL += "/kor_webroot/src/cm/tims/index.aspx?cd_company=" + LoginInfo.CompanyCode + "&cd_pc=" + LoginInfo.CdPc + "&no_docu=" + System.Web.HttpUtility.UrlEncode(NO_DOCU, Encoding.UTF8) + "&login_id=" + LoginInfo.EmployeeNo;
                }

                System.Diagnostics.Process.Start("IExplore.exe", strURL);

                //_header.CurrentRow["YN_REPORT"] = "Y";
                //_header.CurrentRow["ST_STAT"] = "2";    // 2 : 미상신
                //btn_Excel_Upload.Enabled = false;

                ControlButtonEnabledDisable(null, false);
                ShowMessage("전자결재 처리가 완료 되었습니다.");

                //if (tb_NO_PO.Text.Trim() != "" && (MainFrameInterface.ServerKey == "" || MainFrameInterface.ServerKey == "DZSQL" || MainFrameInterface.ServerKey == "108"))//전자결재 한번올려진것은 버튼 비활성화 : 백광
                //{ check_GW(tb_NO_PO.Text); }

            }
            #endregion
            #region -> 피앤이
            else if (m_Elec_app == "013")//피앤이솔류션 전자결재사용
            {
                string strContents = 전자결재양식생성_피앤이();

                if (strContents == "false")
                    return;

                string App_Form_Kind = "2800";
                bTrue = _biz.전자결재_실제사용(_header.CurrentRow, strContents, App_Form_Kind,"");// App_Form_Kind를 추가한것은 회사마다 App_Form_Kind 번호가 중복되는 것들이 있기 때문에 따로 넘겨줘야 하므로 

                if (!bTrue) return;

                string strURL = "";
                string NO_DOCU = _header.CurrentRow["NO_PO"].ToString();
                //한글처리를 위해서 Encode 사용!
                strURL = "http://gw.pnesolution.com/kor_webroot/src/cm/tims/index.aspx?cd_company=" + LoginInfo.CompanyCode + "&cd_pc=" + LoginInfo.CdPc + "&no_docu=" + System.Web.HttpUtility.UrlEncode(NO_DOCU, Encoding.UTF8) + "&login_id=" + LoginInfo.EmployeeNo;

                System.Diagnostics.Process.Start("IExplore.exe", strURL);

                //_header.CurrentRow["YN_REPORT"] = "Y";
                //_header.CurrentRow["ST_STAT"] = "2";    // 2 : 미상신
                //btn_Excel_Upload.Enabled = false;

                ControlButtonEnabledDisable(null, false);
                ShowMessage("전자결재 처리가 완료 되었습니다.");

                //if (tb_NO_PO.Text.Trim() != "" && (MainFrameInterface.ServerKey == "" || MainFrameInterface.ServerKey == "DZSQL" || MainFrameInterface.ServerKey == "108"))//전자결재 한번올려진것은 버튼 비활성화 : 백광
                //{ check_GW(tb_NO_PO.Text); }

            }
            #endregion
            #region -> 세미테크
            else if (m_Elec_app == "009")//세미테크 전자결재사용
            {
                string strContents = 전자결재양식생성_세미테크();

                if (strContents == "")
                    return;

                string App_Form_Kind = "8001";
                bTrue = _biz.전자결재_실제사용(_header.CurrentRow, strContents, App_Form_Kind,"");// App_Form_Kind를 추가한것은 회사마다 App_Form_Kind 번호가 중복되는 것들이 있기 때문에 따로 넘겨줘야 하므로 

                if (!bTrue) return;

                string strURL = "";
                string NO_DOCU = _header.CurrentRow["NO_PO"].ToString();
                //한글처리를 위해서 Encode 사용!
                strURL = "http://10.0.0.120:8088/kor_webroot/src/cm/tims/index.aspx?cd_company=" + LoginInfo.CompanyCode + "&cd_pc=" + LoginInfo.CdPc + "&no_docu=" + System.Web.HttpUtility.UrlEncode(NO_DOCU, Encoding.UTF8) + "&login_id=" + LoginInfo.EmployeeNo;

                System.Diagnostics.Process.Start("IExplore.exe", strURL);

                //_header.CurrentRow["YN_REPORT"] = "Y";
                //_header.CurrentRow["ST_STAT"] = "2";    // 2 : 미상신
                //btn_Excel_Upload.Enabled = false;

                ControlButtonEnabledDisable(null, false);
                ShowMessage("전자결재 처리가 완료 되었습니다.");

                //if (tb_NO_PO.Text.Trim() != "" && (MainFrameInterface.ServerKey == "" || MainFrameInterface.ServerKey == "DZSQL" || MainFrameInterface.ServerKey == "108"))//전자결재 한번올려진것은 버튼 비활성화 : 백광
                //{ check_GW(tb_NO_PO.Text); }
               

            }
            #endregion
            #region -> 정화선박의장
            //정화선박의장, 원익, 삼텍엔지니어링, 아바텍, 우리기술, 원봉, 쎄트렉아이, 기가비스,유니콘미싱공업 전자결재사용
            //else if (m_Elec_app == "016" || m_Elec_app == "020" || m_Elec_app == "025" || m_Elec_app == "027" || m_Elec_app == "028" || m_Elec_app == "029")
            else
            {
                P_PU_PO_REG2_GW _gw = new P_PU_PO_REG2_GW();

                string[] strContents = _gw.getGwSearch(_header.CurrentRow["NO_PO"].ToString(), m_Elec_app, tb_DT_PO.Text);

                if (strContents == null || strContents.Length == 0) return;

                bTrue = _biz.전자결재_실제사용(_header.CurrentRow, strContents[0], strContents[1],"");// App_Form_Kind를 추가한것은 회사마다 App_Form_Kind 번호가 중복되는 것들이 있기 때문에 따로 넘겨줘야 하므로 

                if (!bTrue) return;

                string strURL = string.Empty;
                string NO_DOCU = _header.CurrentRow["NO_PO"].ToString();

                if (D.GetString(strContents[2]) == string.Empty)
                    strContents[2] = BASIC.GetMAENV("GROUPWARE_URL") + "/kor_webroot/src/cm/tims/index.aspx?cd_company="; 

                //한글처리를 위해서 Encode 사용!
                strURL = strContents[2] + LoginInfo.CompanyCode + "&cd_pc=" + LoginInfo.CdPc + "&no_docu=" + System.Web.HttpUtility.UrlEncode(NO_DOCU, Encoding.UTF8) + "&login_id=" + LoginInfo.EmployeeNo;

                System.Diagnostics.Process.Start("IExplore.exe", strURL);

                ControlButtonEnabledDisable(null, false);
                ShowMessage("전자결재 처리가 완료 되었습니다.");

            }
            #endregion
            #endregion

        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
    }

    #endregion

    #region -> 전자결재양식생성
    private string 전자결재양식생성_대림()
    {
        try
        {
            string str_html = string.Empty, str_html_detail = string.Empty;
            string dt_po = _header.CurrentRow["DT_PO"].ToString();
            string no_emp = _header.CurrentRow["NM_KOR"].ToString();
            string dc50_po = _header.CurrentRow["DC50_PO"].ToString();
            string cd_partner = _header.CurrentRow["NM_PARTNER"].ToString();

            decimal QT_PO_MM = 0M;
            decimal UM_EX_PO = 0M;
            decimal AM_EX = 0M;
            decimal AM_SUM = 0M;
            string dt_limit = string.Empty;


            int i = 1;
            dt_po = dt_po.Substring(0, 4) + "." + dt_po.Substring(4, 2) + dt_po.Substring(6, 2);
            foreach (DataRow dr in _flexD.DataTable.Rows)
            {
                QT_PO_MM = Convert.ToDecimal(dr["QT_PO_MM"]);
                UM_EX_PO = Convert.ToDecimal(dr["UM_EX_PO"]);
                AM_EX = Convert.ToDecimal(dr["AM"]);
                dt_limit = dr["DT_LIMIT"].ToString().Substring(0, 4) + "." + dr["DT_LIMIT"].ToString().Substring(4, 2) + "." + dr["DT_LIMIT"].ToString().Substring(6, 2);
                AM_SUM += AM_EX;

                string Format = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY, FormatFgType.INSERT);

                str_html_detail += @"<tr>
                                   <td width='50' height='26' class='border_4'><div align='center'>" + i.ToString() + @"</div></td>
                                   <td width='240' height='26' class='border_4'><div align='left'>" + dr["NM_ITEM"].ToString() + @"</div></td>
                                   <td width='50' height='26' class='border_4'><div align='center'>" + QT_PO_MM.ToString(Format) + @"</div></td>
                                   <td width='90' height='26' class='border_4'><div align='right'>" + UM_EX_PO.ToString("###,###,###,###,##0") + @"</div></td>
                                   <td width='110' height='26' class='border_4'><div align='right'>" + AM_EX.ToString("###,###,###,###,##0") + @"</div></td>
                                   <td width='110' height='26' class='border_4'><div align='center'>" + dr["NM_PJT"].ToString() + @"</div></td>
                                   <td width='110' class='border_1'><div align='center'>" + dt_limit + @"</div></td>
                                 </tr>";
            }

            #region -> html source
            str_html = @" <html>      
                           <head>      
                           <title>발주서</title>      
                           <meta http-equiv='Content-Type' content='text/html; charset=euc-kr'>      
                            <style type='text/css'> 
                           BODY {font-family:굴림; font-size:12px; color:#000000;}      
                           P {font-family:굴림; font-size:12px; color:#000000;}      
                           td {font-family:굴림; font-size:12px; color:#000000;}      
                        .red {font-family:굴림; font-size:12px; color:#ff0000; text-decoration:none;} 
                        .redB {font-family:굴림; font-size:12px; font-weight: bold; color:#ff0000; text-decoration:none;} 
                        .redBB {font-family:굴림; font-size:16px; font-weight: bold; color:#ff0000; text-decoration:none;}
                        .blackB0 {font-size:12px; font-weight:bold; }
                        .blackB {font-size:14px; font-weight:bold; }
                        .blackBB {font-size:35px; font-weight:bold; text-decoration:underline; }
                        .blackBBB {font-size:35px; font-weight:bold; text-decoration:none; }
                         
                        .border_1 {
                                  border-left:0px;
	                              border-top:0px;
	                              border-right:0px;
                          }
                        .border_2 {
                                  border-left:0px;
	                              border-top:0px;
	                              border-bottom:0px;
                          }
                        .border_3 {
                                  border-left:0px;
	                              border-right:0px;
	                              border-top:0px;
	                              border-bottom:0px;
                          }
                        .border_4 {
	                              border-top:0px;
	                              border-left:0px;
                          }
                            .style1 {font-size: 12px; font-weight: bold; }
                            </style>   
                           </head>
                                    
                           <body bgcolor='#FFFFFF' text='#000000'>    
                           <!-- 계약체결보고서 -->  
                           <table width='650' border='0' cellspacing='0' cellpadding='0'>      
                             <tr>       
                               <td width='650' height='80'>       
                                 <div align='center' class='blackBBB'>발주서</div>      
                               </td>      
                             </tr>      
                             <tr>       
                               <td width='650' height='10'>&nbsp;</td>      
                             </tr>      
                             <tr>       
                               <td height='25' align='right'><table width='250' border='0' cellpadding='2' cellspacing='0'>
                                 <tr>
                                   <td width='90' height='25'><div align='right'>수주NO : </div></td>
                                   <td width='160' height='25'>" + dt_po + @"</td>
                                 </tr>
                                 <tr>
                                   <td height='25'><div align='right'>담당 : </div></td>
                                   <td width='160' height='25'>" + no_emp + @"</td>
                                 </tr>
                                 <tr>
                                   <td height='25' colspan='2'><span class='blackB'>아래 내역에 대해 발주합니다. </span></td>
                                 </tr>
                               </table></td>
                             </tr>      
                             <tr>       
                               <td></td>      
                             </tr>      
                             <tr>       
                               <td></td>      
                             </tr>      
                             <tr>       
                               <td height='26' class='blackB0'>1. 구매품 내역	   </td>      
                             </tr>
                             <tr>
                               <td height='10'><table width='650' border='1' cellpadding='2' cellspacing='0' bordercolor='#000000'>
                                 <tr bgcolor='#DBF2FD'>
                                   <td width='50' height='30' bgcolor='#DBF2FD' class='border_4'><div align='center'>NO</div></td>
                                   <td width='240' height='30' bgcolor='#DBF2FD' class='border_4'><div align='center'>품명</div></td>
                                   <td width='50' height='30' bgcolor='#DBF2FD' class='border_4'><div align='center'>수량</div></td>
                                   <td width='90' height='30' bgcolor='#DBF2FD' class='border_4'><div align='center'>단가</div></td>
                                   <td width='110' height='30' bgcolor='#DBF2FD' class='border_4'><div align='center'>금액</div></td>
                                   <td width='110' height='30' class='border_4'><div align='center'>수주번호</div></td>
                                   <td width='110' align='center' class='border_1'>납기일</td>
                                 </tr>" + str_html_detail +
                                @"<tr bgcolor='#FFFFCC'>
                                   <td height='36' colspan='7' class='border_3'><table width='600' border='0' align='center' cellpadding='0' cellspacing='0'>
                                     <tr>
                                       <td><div align='right' class='blackB'>
                                         <div align='right'>합계 : </div>
                                       </div></td>
                                       <td ><div align='right' class='redBB'>" + AM_SUM.ToString("###,###,###,###,##0") + @"</div></td>
                                     </tr>
                                   </table></td>
                                 </tr>
                               </table></td>
                             </tr>
                             <tr>
                               <td height='20'><table width='600' border='0' align='left' cellpadding='2' cellspacing='0'>
                                 <tr>
                                   <td width='110' height='25' class='blackB0'><div align='left'>2. 납&nbsp;품&nbsp;&nbsp;장&nbsp;소 : </div></td>
                                   <td width='490' height='25'>" + dc50_po + @" </td>
                                 </tr>
                                 <tr>
                                   <td width='110' height='25' class='blackB0'>3. 구입선 : </td>
                                   <td width='490' height='25'>" + cd_partner + @"</td>
                                 </tr>
                               </table></td>
                             </tr>
                             <tr>
                               <td height='40'>&nbsp;</td>
                             </tr>      
                           </table>
                           </body>      
                           </html>
            ";

            #endregion

            return str_html;


        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }

        return string.Empty;
    }

    private string 전자결재양식생성_백광()
    {
        try
        {
            string str_html = string.Empty, str_html_detail = string.Empty;
            string dt_po = _header.CurrentRow["DT_PO"].ToString();
            string no_emp = _header.CurrentRow["NM_KOR"].ToString();
            string dc50_po = _header.CurrentRow["DC50_PO"].ToString();
            string cd_partner = _header.CurrentRow["CD_PARTNER"].ToString();
            string nm_partner = _header.CurrentRow["NM_PARTNER"].ToString();
            string TEL_partner = "&nbsp;";
            string FAX_partner = "&nbsp;";
            string NM_ITEM = "&nbsp";
            decimal QT_PO_MM = 0M;
            decimal UM_EX_PO = 0M;
            decimal AM_EX = 0M;
            decimal UM_SUM = 0M;
            string dt_limit = string.Empty;
            string UNIT_IM = "&nbsp";
            string NM_CC = "&nbsp";
            string DC_LINE = "";
            string NO_PO = _header.CurrentRow["NO_PO"].ToString().Trim(); if (NO_PO == "") NO_PO = "&nbsp;";
            DataTable dt_PARTNER = _biz.search_partner(cd_partner);//거래처정보가져오기
            if (dt_PARTNER != null && dt_PARTNER.Rows.Count > 0)
            {
                if (dt_PARTNER.Rows[0]["NO_TEL"].ToString().Trim() != "") TEL_partner = dt_PARTNER.Rows[0]["NO_TEL"].ToString();
                if (dt_PARTNER.Rows[0]["NO_FAX"].ToString().Trim() != "") FAX_partner = dt_PARTNER.Rows[0]["NO_FAX"].ToString();

            }

            int i = 1;//순번
            //dt_po = dt_po.Substring(0, 4) + "." + dt_po.Substring(4, 2) + dt_po.Substring(6, 2);
            foreach (DataRow dr in _flexD.DataTable.Rows)
            {
                if (dr["NM_ITEM"].ToString().Trim() != "") NM_ITEM = dr["NM_ITEM"].ToString();
                if (dr["UNIT_IM"].ToString().Trim() != "") UNIT_IM = dr["UNIT_IM"].ToString();
                if (dr["NM_CC"].ToString().Trim() != "") NM_CC = dr["NM_CC"].ToString();

                DC_LINE = dr["DC1"].ToString();
                QT_PO_MM = Convert.ToDecimal(dr["QT_PO_MM"]);
                UM_EX_PO = Convert.ToDecimal(dr["UM_EX_PO"]);
                AM_EX = Convert.ToDecimal(dr["AM_EX"]);
                dt_limit = dr["DT_LIMIT"].ToString().Substring(0, 4) + "." + dr["DT_LIMIT"].ToString().Substring(4, 2) + "." + dr["DT_LIMIT"].ToString().Substring(6, 2);
                UM_SUM += AM_EX;


                str_html_detail += @"<tr align='center'>
                                     <td rowspan='2'  height='22' align='center' valign='middle' bgColor='f6f6f6'style='font-size:12px;'>" + i + @"</td>
                                     <td width='160' height='22' align='left' valign='middle' style='font-size:12px;'>" + NM_ITEM + @"</td>
                                     <td rowspan='2'   height='22' align='center' valign='middle' style='font-size:12px;'>" + QT_PO_MM.ToString("###,###,###,###,##0") + @"</td>
                                    <td rowspan='2'   height='22' align='center' valign='middle' style='font-size:12px;'>" + UNIT_IM + @"</td>
                                     <td  rowspan='2'  height='22' align='right' valign='middle' style='font-size:12px;'>" + UM_EX_PO.ToString("###,###,###,###,##0") + @"</td>
                                     <td  rowspan='2'  height='22' align='right' valign='middle' style='font-size:12px;'>" + AM_EX.ToString("###,###,###,###,##0") + @"</td>
                                     <td width='90' rowspan='2'   height='22' align='center' valign='middle' style='font-size:12px;'>" + NM_CC + @"</td>
                                   </tr>
                                    <tr><td width='160' height='22' >'" + DC_LINE + @"'</td></tr>";

                i++;//순번올려주는것
            }

            #region -> html source
            str_html = @"<html> <head> <title></title> 
  
                        </head>
                        <body style='FONT-FAMILY: gulim; FONT-SIZE: 9pt' bgColor=#ffffff text=#000000>
                        <table border=1 cellSpacing=0 borderColor=#7F8CA4 borderColorLight=#7F8CA4 borderColorDark=white cellPadding=1 width=617>
                        <tr align ='center'>
                        <td width='80' height='22' align='center' bgcolor='f6f6f6'style='font-size:12px;'>발주번호</td>
                        <td colspan =65> " + NO_PO + @"</td>
                        </tr>
                         <tr align='center'>
                            <td  width='70' height='22' align='center' valign='middle' bgcolor='f6f6f6' style='font-size:12px;'>업체명</td>
                             <td colspan ='2' width='250' height='22' align='center' valign='middle' style='font-size:12px;'>" + nm_partner + @"</td>
                             <td width='90' height='22' align='center' valign='middle' bgcolor='f6f6f6' style='font-size:12px;'>" + "전화" + @"</td>
                             <td width='80' height='22' align='center' valign='middle' style='font-size:12px;'>" + TEL_partner + @"</td>
                             <td width='80' height='22' align='center' valign='middle' bgcolor='f6f6f6'style='font-size:12px;'>" + "팩스" + @"</td>
                             <td width='90' height='22' align='center' valign='middle' style='font-size:12px;'>" + FAX_partner + @"</td>
                           </tr>
                           <tr align='center'>
                             <td width='617' height='22' colspan='7' align='left' valign='middle' style='font-size:12px;'>하기와 같이 발주하오니 요건에 맞추어 기일내에 완료하시기 바랍니다.</td>
                           </tr>

                            
                           <tr align='center'>
                             <td width='70' height='22' align='center' valign='middle' bgColor='DAEFFF' style='font-

                        size:12px;'>번호</td>
                             <td width='187' height='22' align='center' valign='middle' bgColor='DAEFFF' style='font-

                        size:12px;'>품명/규격</td>
                             <td width='90' height='22' align='center' valign='middle' bgColor='DAEFFF' style='font-

                        size:12px;'>수량(식)</td>
<td width='90' height='22' align='center' valign='middle' bgColor='DAEFFF' style='font-

                        size:12px;'>단위</td>
                             <td width='90' height='22' align='center' valign='middle' bgColor='DAEFFF' style='font-

                        size:12px;'>단가(원)</td>
                             <td width='90' height='22' align='center' valign='middle' bgcolor='DAEFFF' style='font-

                        size:12px;'>금액(원)</td>
                             <td width='90' height='22' align='center' valign='middle' bgcolor='DAEFFF' style='font-

                        size:12px;'>C/C명</td>
                           </tr>" + str_html_detail +
                                                            @" <tr align='center'>
                             <td  height='22' align='center' valign='middle' bgColor='FCEECD' style='font-

                        size:12px;font-weight: bold;'>계</td>
                             <td  height='22' align='left' valign='middle' bgcolor='FCEECD' style='font-

                        size:12px;font-weight: bold;padding-right:5px;'>&nbsp</td>
                             <td  height='22' align='center' valign='middle' bgcolor='FCEECD' style='font-

                        size:12px;font-weight: bold;'>&nbsp;</td>

                             <td  height='22' align='right' bgcolor='FCEECD' valign='middle' style='font-

                        size:12px;font-weight: bold;'>&nbsp;</td>
                    
                             <td  height='22' align='center' valign='middle' bgcolor='FCEECD' style='font-

                        size:12px;font-weight: bold;'>&nbsp;</td>
                            <td  height='22' align='right' valign='middle' bgcolor='FCEECD' style='font-

                        size:12px;font-weight: bold;padding-right:5px;'>" + UM_SUM.ToString("###,###,###,###,##0") + @"</td>
                             <td  height='22' align='center' valign='middle' bgcolor='FCEECD' style='font-

                        size:12px;font-weight: bold;'>&nbsp;</td>
                           </tr>
                           <tr valign='top' bgcolor='#FFFFFF'>
                             <td height='100' colspan='7' style='font-size:12px; padding:10px'>* 특기사항 : <br>" + dc50_po + @"</td>
                          </tr>
                        </table>
                        </BODY></HTML>";

            #endregion

            return str_html;

        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }

        return string.Empty;
    }

    private string 전자결재양식생성_지엔텔()
    {
        try
        {
            object[] obj = new object[] { Global.MainFrame.LoginInfo.CompanyCode, _header.CurrentRow["NO_PO"].ToString() };
            DataTable dt_Gntel = _biz.DataSearch_GW_RPT(obj);
            if (dt_Gntel == null || dt_Gntel.Rows.Count == 0)
                return "";

            string str_html = string.Empty, str_html_detail = string.Empty;
            //헤더부분
            string ln_partner = (D.GetString(dt_Gntel.Rows[0]["LN_PARTNER"]) != string.Empty) ? D.GetString(dt_Gntel.Rows[0]["LN_PARTNER"]) : "&nbsp";
            string nm_ceo1 = (D.GetString(dt_Gntel.Rows[0]["NM_CEO1"]) != string.Empty) ? D.GetString(dt_Gntel.Rows[0]["NM_CEO1"]) : "&nbsp";
            string ads1 = (D.GetString(dt_Gntel.Rows[0]["ADS1"]) + " " + D.GetString(dt_Gntel.Rows[0]["ADS_DETAIL1"]) != string.Empty) ? (D.GetString(dt_Gntel.Rows[0]["ADS1"]) + " " + D.GetString(dt_Gntel.Rows[0]["ADS_DETAIL1"])) : "&nbsp";
            string no_tel1 = (D.GetString(dt_Gntel.Rows[0]["NO_TEL1"]) != string.Empty) ? D.GetString(dt_Gntel.Rows[0]["NO_TEL1"]) : "&nbsp";
            string no_fax1 = (D.GetString(dt_Gntel.Rows[0]["NO_FAX1"]) != string.Empty) ? D.GetString(dt_Gntel.Rows[0]["NO_FAX1"]) : "&nbsp";
            string nm_ptr1 = (D.GetString(dt_Gntel.Rows[0]["NM_PTR1"]) != string.Empty) ? D.GetString(dt_Gntel.Rows[0]["NM_PTR1"]) : "&nbsp";
            string no_po = (D.GetString(dt_Gntel.Rows[0]["NO_PO"]) != string.Empty) ? D.GetString(dt_Gntel.Rows[0]["NO_PO"]) : "&nbsp";
            string dt_po = (D.GetString(dt_Gntel.Rows[0]["DT_PO"]) != string.Empty) ? dt_Gntel.Rows[0]["DT_PO"].ToString().Substring(0, 4) + "." + dt_Gntel.Rows[0]["DT_PO"].ToString().Substring(4, 2) + "." + dt_Gntel.Rows[0]["DT_PO"].ToString().Substring(6, 2) : "&nbsp";
            string dc50_po = (D.GetString(dt_Gntel.Rows[0]["DC50_PO"]) != string.Empty) ? D.GetString(dt_Gntel.Rows[0]["DC50_PO"]) : "&nbsp";

            string biz_num = (D.GetString(dt_Gntel.Rows[0]["BIZ_NUM1"]) != string.Empty) ? D.GetString(dt_Gntel.Rows[0]["BIZ_NUM1"]) : "&nbsp";
            string nm_ceo = (D.GetString(dt_Gntel.Rows[0]["NM_CEO"]) != string.Empty) ? D.GetString(dt_Gntel.Rows[0]["NM_CEO"]) : "&nbsp";
            string ads = (D.GetString(dt_Gntel.Rows[0]["ADS"]) + " " + D.GetString(dt_Gntel.Rows[0]["ADS_DETAIL"]) != string.Empty) ? D.GetString(dt_Gntel.Rows[0]["ADS"]) + " " + D.GetString(dt_Gntel.Rows[0]["ADS_DETAIL"]) : "&nbsp";
            string no_tel = (D.GetString(dt_Gntel.Rows[0]["NO_TEL"]) != string.Empty) ? D.GetString(dt_Gntel.Rows[0]["NO_TEL"]) : "&nbsp";
            string no_fax = (D.GetString(dt_Gntel.Rows[0]["NO_FAX"]) != string.Empty) ? D.GetString(dt_Gntel.Rows[0]["NO_FAX"]) : "&nbsp";
            string nm_ptr = (D.GetString(dt_Gntel.Rows[0]["NM_PTR"]) != string.Empty) ? D.GetString(dt_Gntel.Rows[0]["NM_PTR"]) : "&nbsp";//담당자

            //-----------------------------
            string cd_item = "&nbsp;";
            string nm_item = "&nbsp;";
            string cd_pjt = "&nbsp;";
            string nm_pjt = "&nbsp;";
            string stnd_item = "&nbsp;";
            decimal no_num = 0;
            decimal qt_po_mm = 0M;
            decimal um_ex_po = 0M;
            decimal am_ex = 0M;
            string dt_limit = string.Empty;
            string unit_im = "&nbsp;";
            string dc_line = "";
            decimal qt_po_mm_pjt = 0M;
            decimal um_ex_po_pjt = 0M;
            decimal am_ex_pjt = 0M;
            string dc_line_pjt = "";
            decimal sum_am_ex = 0M;

            //dt_po = dt_po.Substring(0, 4) + "." + dt_po.Substring(4, 2) + dt_po.Substring(6, 2);

            //라인부분-------------------------
            foreach (DataRow dr in dt_Gntel.Rows)
            {
                no_num++;
                cd_item = (D.GetString(dr["CD_ITEM"]) != string.Empty) ? D.GetString(dr["CD_ITEM"]) : "&nbsp;";
                nm_item = (D.GetString(dr["NM_ITEM"]) != string.Empty) ? D.GetString(dr["NM_ITEM"]) : "&nbsp;";
                stnd_item = (D.GetString(dr["STND_ITEM"]) != string.Empty) ? D.GetString(dr["STND_ITEM"]) : "&nbsp;";
                unit_im = (D.GetString(dr["UNIT_IM"]) != string.Empty) ? D.GetString(dr["UNIT_IM"]) : "&nbsp;";
                cd_pjt = (D.GetString(dr["CD_PJT"]) != string.Empty) ? D.GetString(dr["CD_PJT"]) : "&nbsp;";
                nm_pjt = (D.GetString(dr["NM_PJT"]) != string.Empty) ? D.GetString(dr["NM_PJT"]) : "&nbsp;";
                dt_limit = dr["DT_LIMIT"].ToString().Substring(0, 4) + "." + dr["DT_LIMIT"].ToString().Substring(4, 2) + "." + dr["DT_LIMIT"].ToString().Substring(6, 2);
                qt_po_mm = Convert.ToDecimal(dr["QT_PO_MM"]);
                um_ex_po = Convert.ToDecimal(dr["UM_EX_PO"]);
                am_ex = Convert.ToDecimal(dr["AM_EX"]);
                dc_line = (D.GetString(dr["DC1"]) != string.Empty) ? D.GetString(dr["DC1"]) : "&nbsp;";
                qt_po_mm_pjt = D.GetDecimal(dr["QT"]);
                um_ex_po_pjt = D.GetDecimal(dr["UM_PO"]);
                am_ex_pjt = D.GetDecimal(dr["AM_EXPO_CIS"]);
                dc_line_pjt = D.GetString(dr["DC_RMK_PJT"]);
                sum_am_ex += D.GetDecimal(dr["AM_EX"]);

                //라인부분
                str_html_detail += @"<tr height='25'>
		        <td style='border-width: 1 1 1 1; border-style: solid; border-color: #000000;' style='line-height: 140%' rowspan='2'>" + no_num + @"　</td>
		        <td style='border-width: 1 1 0 0; border-style: solid; border-color: #000000;' style='line-height: 140%'>" + cd_item + @"</td>
		        <td style='border-width: 1 1 0 0; border-style: solid; border-color: #000000;' style='line-height: 140%'>" + nm_item + @"　</td>
		        <td style='border-width: 1 1 0 0; border-style: solid; border-color: #000000;' style='line-height: 140%'>" + stnd_item + @"　</td>
		        <td style='border-width: 1 1 0 0; border-style: solid; border-color: #000000;' style='line-height: 140%'>" + unit_im + @"　</td>
		        <td style='border-width: 1 1 1 0; border-style: solid; border-color: #000000;' style='line-height: 140%' rowspan='2'>" + dt_limit + @"　</td>
		        <td style='border-width: 1 1 0 0; border-style: solid; border-color: #000000;' style='line-height: 140%'>" + qt_po_mm.ToString("###,###,###,###,##0") + @"　</td>
		        <td style='border-width: 1 1 0 0; border-style: solid; border-color: #000000;' style='line-height: 140%'>" + um_ex_po.ToString("###,###,###,###,##0") + @"　</td>
		        <td style='border-width: 1 1 0 0; border-style: solid; border-color: #000000;' style='line-height: 140%'>" + am_ex.ToString("###,###,###,###,##0") + @"　</td>
		        <td style='border-width: 1 1 0 0; border-style: solid; border-color: #000000;' style='line-height: 140%'>" + dc_line + @"　</td>
	        </tr>
	        <tr height='25'>
		        <td style='border-width: 1 1 1 0; border-style: solid; border-color: #000000;' style='line-height: 140%' colspan='2'>" + cd_pjt + @"　</td>
		        <td style='border-width: 1 1 1 0; border-style: solid; border-color: #000000;' style='line-height: 140%' colspan='2'>" + nm_pjt + @"　</td>
		        <td style='border-width: 1 1 1 0; border-style: solid; border-color: #000000;' style='line-height: 140%'>　" + qt_po_mm_pjt.ToString("###,###,###,###,##0") + @"</td>
		        <td style='border-width: 1 1 1 0; border-style: solid; border-color: #000000;' style='line-height: 140%'>" + um_ex_po_pjt.ToString("###,###,###,###,##0") + @"　</td>
		        <td style='border-width: 1 1 1 0; border-style: solid; border-color: #000000;' style='line-height: 140%'>　" + am_ex_pjt.ToString("###,###,###,###,##0") + @"</td>
		        <td style='border-width: 1 1 1 0; border-style: solid; border-color: #000000;' style='line-height: 140%'>　" + dc_line_pjt + @"</td>
	        </tr>";

            }

            str_html_detail += @"<tr height='25'>
		        <td style='border-width: 1 1 1 1; border-style: solid; border-color: #000000;' style='line-height: 140%' colspan='7'>&nbsp;</td>
		        <td style='border-width: 1 1 1 0; border-style: solid; border-color: #000000;' style='line-height: 140%'> 합계 </td>
		        <td style='border-width: 1 1 1 0; border-style: solid; border-color: #000000;' style='line-height: 140%'>" + sum_am_ex.ToString("###,###,###,###,##0") + @"&nbsp;　</td>
		        <td style='border-width: 1 1 1 0; border-style: solid; border-color: #000000;' style='line-height: 140%'> &nbsp; </td>
	        </tr>";

            #region -> html source 헤더부분
            str_html = @"<head> <meta http-equiv='Content-Language' content='ko'></head>
                        <center>
                        <body leftmargin='0' marginwidth='0' topmargin='0' marginheight='0'>
                            <table width='945' border='0' cellpadding='0' cellspacing='0' style='font-size: 10pt'>
	                            <tr height='50'>
		                        <td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'>
		                        <p align='center'><span style='font-size: 23pt; font-weight: 700'>구 매 발 주 서</span></td>
	                            </tr>
                            </table>
            <table width='945' border='0' cellpadding='0' cellspacing='0' style='font-size: 10pt'>
	                <colgroup width='90' align='center'></colgroup>
	                <colgroup width='156' align='center'></colgroup>
	                <colgroup width='80' align='center'></colgroup>
	                <colgroup width='146' align='center'></colgroup>
	                <colgroup width='95' align='center'></colgroup>
	                <colgroup width='151' align='center'></colgroup>
	                <colgroup width='80' align='center'></colgroup>
	                <colgroup width='147' align='center'></colgroup>
	            <tr height='30'> 
		        <td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' colspan='8'>
		        <p align='left'>" + ln_partner + @"</td>
	            </tr>
	            <tr height='25'> 
		<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'>
		대 표 자</td>
		<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' align='left'>
		: " + nm_ceo1 + @"</td>
		<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' align='left' colspan='2'>
		귀하</td>
		<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'>
		사업자등록번호</td>
		<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' colspan='3' align='left'>
		: " + biz_num + @" </td>
	</tr>
	<tr height='25'> 
		<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'>
		주&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 소</td>
		<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' align='left' colspan='3'>
		: " + ads1 + @"</td>
		<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' align='right'>
		대 표 자</td>
		<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' colspan='3' align='left'>
		&nbsp;:&nbsp;" + nm_ceo + @"</td>
	</tr>
	<tr height='25'> 
		<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'>
		전&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 화</td>
		<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' align='left'>
		: " + no_tel1 + @"</td>
		<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' align='left'>
		F A X</td>
		<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' align='left'>
		: " + no_fax1 + @"</td>
		<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' align='right'>
		주&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 소</td>
		<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' colspan='3' align='left'>
		&nbsp;: " + ads + @" </td>
	</tr>
	<tr height='25'> 
		<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'>
		담 당 자</td>
		<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' align='left' colspan='3'>
		: " + nm_ptr1 + @"</td>
		<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' align='right'>
		전&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 화</td>
		<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'>
		<p align='left'>&nbsp;: " + no_tel + @"</td>
		<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'>
		<p align='left'>F A X</td>
		<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'>
		<p align='left'>: " + no_fax + @"</td>
	</tr>
	<tr height='25'> 
		<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'>
		발주번호</td>
		<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' align='left'>
		: " + no_po + @"</td>
		<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' align='left'>
		발주일자</td>
		<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' align='left'>
		: " + dt_po + @"</td>
		<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' align='right'>
		담 당 자</td>
		<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' colspan='3'>
		<p align='left'>&nbsp;: " + nm_ptr + @"</td>
	</tr>
	<tr height='25'> 
		<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'>
		비&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 고</td>
		<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' align='left' colspan='7'>
		: " + dc50_po + @"</td>
	</tr>
	<tr height='45'> 
		<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' colspan='8'>
		( 아래와 같이 발주합니다. )</td>
	</tr>
</table>
	<table width='945' border='0' cellpadding='0' cellspacing='0' style='font-size: 9pt'>
	<colgroup width='5%' align='center'></colgroup>
	<colgroup width='10%' align='center'></colgroup>
	<colgroup width='10%' align='center'></colgroup>
	<colgroup width='10%' align='center'></colgroup>
	<colgroup width='10%' align='center'></colgroup>
	<colgroup width='10%' align='center'></colgroup>
	<colgroup width='9%' align='center'></colgroup>
	<colgroup width='9%' align='center'></colgroup>
	<colgroup width='9%' align='center'></colgroup>
	<colgroup width='18%' align='center'></colgroup>
	<tr height='25'>
		<td style='border-width: 1 1 0 1; border-style: solid; border-color: #000000;' style='line-height: 140%' rowspan='2'>
		NO</td>
		<td style='border-width: 1 1 0 0; border-style: solid; border-color: #000000;' style='line-height: 140%'>
		품 목</td>
		<td style='border-width: 1 1 0 0; border-style: solid; border-color: #000000;' style='line-height: 140%'>
		품 명</td>
		<td style='border-width: 1 1 0 0; border-style: solid; border-color: #000000;' style='line-height: 140%'>
		규 격</td>
		<td style='border-width: 1 1 0 0; border-style: solid; border-color: #000000;' style='line-height: 140%'>
		단 위</td>
		<td style='border-width: 1 1 0 0; border-style: solid; border-color: #000000;' style='line-height: 140%' rowspan='2'>
		납기일</td>
		<td style='border-width: 1 1 0 0; border-style: solid; border-color: #000000;' style='line-height: 140%' rowspan='2'>
		수 량</td>
		<td style='border-width: 1 1 0 0; border-style: solid; border-color: #000000;' style='line-height: 140%' rowspan='2'>
		단 가</td>
		<td style='border-width: 1 1 0 0; border-style: solid; border-color: #000000;' style='line-height: 140%' rowspan='2'>
		발주금액</td>
		<td style='border-width: 1 1 0 0; border-style: solid; border-color: #000000;' style='line-height: 140%' rowspan='2'>
		비&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 고</td>
	</tr>
	<tr height='25'>
		<td style='border-width: 1 1 0 0; border-style: solid; border-color: #000000;' style='line-height: 140%' colspan='2'>
		사전 PJT code</td>
		<td style='border-width: 1 1 0 0; border-style: solid; border-color: #000000;' style='line-height: 140%' colspan='2'>
		사전 PJT 명</td>
	</tr>" + str_html_detail + @"</table>
</body>
</center>";

            #endregion //헤더부분

            return str_html;

        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }

        return string.Empty;
    }

    private string GW_Nexti_html() //넥스트아이 html 생성
    {

        string html_source;
        string htmlSrcFile = Application.StartupPath + "\\download\\gw\\HT_PU_PO_REG2_NEXT_I.htm";

        using (StreamReader reader = new StreamReader(htmlSrcFile, Encoding.Default))
        {
            html_source = reader.ReadToEnd();
        }

        object[] obj = new object[] { Global.MainFrame.LoginInfo.CompanyCode, _header.CurrentRow["NO_PO"].ToString() };
        DataTable dt_GWdata = _biz.DataSearch_GW_RPT(obj);

        html_source = html_source.Replace("@@발주일자", D.GetString(dt_GWdata.Rows[0]["DT_PO"]).Substring(0,4) +"/" +D.GetString(dt_GWdata.Rows[0]["DT_PO"]).Substring(4,2) +"/"+D.GetString(dt_GWdata.Rows[0]["DT_PO"]).Substring(6,2) + "&nbsp;");
        html_source = html_source.Replace("@@납기일자", D.GetString(dt_GWdata.Rows[0]["DT_LIMIT"]).Substring(0,4) + "/"+D.GetString(dt_GWdata.Rows[0]["DT_LIMIT"]).Substring(4,2)+"/"+ D.GetString(dt_GWdata.Rows[0]["DT_LIMIT"]).Substring(6,2) + "&nbsp;");
        html_source = html_source.Replace("@@발주번호", D.GetString(dt_GWdata.Rows[0]["NO_PO"]) + "(" + D.GetString(dt_GWdata.Rows[0]["CD_PJT"]) +")"+ "&nbsp;");
        html_source = html_source.Replace("@@구매자", D.GetString(dt_GWdata.Rows[0]["NM_PTR"]) + "&nbsp;");
        html_source = html_source.Replace("@@전화", D.GetString(dt_GWdata.Rows[0]["NO_TEL"]) + "&nbsp;");
        html_source = html_source.Replace("@@팩스", D.GetString(dt_GWdata.Rows[0]["NO_FAX"]) + "&nbsp;");
        html_source = html_source.Replace("@@업체명", D.GetString(dt_GWdata.Rows[0]["거래처명"]) + "&nbsp;");
        html_source = html_source.Replace("@@담당자", D.GetString(dt_GWdata.Rows[0]["CD_EMP_PARTNER"]) + "&nbsp;");
        html_source = html_source.Replace("@@거래처전화", D.GetString(dt_GWdata.Rows[0]["NO_TEL1"]) + "&nbsp;");
        html_source = html_source.Replace("@@거래처팩스", D.GetString(dt_GWdata.Rows[0]["NO_FAX1"]) + "&nbsp;");
        html_source = html_source.Replace("@@사업자등록번호", D.GetString(dt_GWdata.Rows[0]["BIZ_NUM1"]) + "&nbsp;");
        html_source = html_source.Replace("@@본사", D.GetString(dt_GWdata.Rows[0]["ADS"]) + D.GetString(dt_GWdata.Rows[0]["ADS_DETAIL"]) + "&nbsp;");
        html_source = html_source.Replace("@@기반연구소", D.GetString(dt_GWdata.Rows[0]["PLANT_ADS_HD"]) + "&nbsp;");
        html_source = html_source.Replace("@@대표이사", D.GetString(dt_GWdata.Rows[0]["NM_CEO"]) + "&nbsp;");
        html_source = html_source.Replace("@@업태", D.GetString(dt_GWdata.Rows[0]["TP_JOB_BIZ"]) + "&nbsp;");
        html_source = html_source.Replace("@@종목", D.GetString(dt_GWdata.Rows[0]["CLS_JOB_BIZ"]) + "&nbsp;");
        html_source = html_source.Replace("@@프로젝트예산", D.GetString(dt_GWdata.Rows[0]["PROJECT_BUDGET"]) + "&nbsp;");
        html_source = html_source.Replace("@@발주누적액", D.GetDecimal(dt_GWdata.Rows[0]["POL_AM_SUM"]).ToString("###,###,###,###,##0.####") + "&nbsp;");


        int gw_no = 0;
        string gw_item_L, gw_cd_item, gw_nm_item, gw_qt_po, gw_um, gw_am, gw_dcrmk;
        decimal sum_am_ex = 0;
        foreach (DataRow dr in dt_GWdata.Rows)
        {
            gw_no = gw_no + 1;
            gw_item_L = D.GetString(dr["CD_CLS_L"]);//품목 대분류
            gw_cd_item = D.GetString(dr["CD_ITEM"]);
            gw_nm_item = D.GetString(dr["NM_ITEM"]);
            gw_qt_po = D.GetDecimal(dr["QT_PO_MM"]).ToString("###,###,###,###,##0");
            gw_um = D.GetDecimal(dr["UM_EX_PO"]).ToString("###,###,###,###,##0.####");
            gw_am = D.GetDecimal(dr["AM_EX"]).ToString("###,###,###,###,##0.####");
            gw_dcrmk = D.GetString(dr["DC1"]);
            sum_am_ex += D.GetDecimal(dr["AM_EX"]);
            html_source += @"
           		<tr height='20'>
		        <td style='border-width: 1 1 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:9pt; line-height:140%' align='center'>" + gw_no + @"&nbsp;</td>
		        <td style='border-width: 1 1 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:9pt; line-height:140%' align='center'>" + gw_item_L + @"&nbsp;</td>
		        <td style='border-width: 1 1 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:9pt; line-height:140%' align='center'>" + gw_cd_item + @"&nbsp;</td>
		        <td style='border-width: 1 1 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:9pt; line-height:140%' align='center'>" + gw_nm_item + @"&nbsp;</td>
		        <td style='border-width: 1 1 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:9pt; line-height:140%' align='center'>" + gw_qt_po + @"&nbsp;</td>
		        <td style='border-width: 1 1 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:9pt; line-height:140%' align='center'>" + gw_um + @"&nbsp;</td>
		        <td style='border-width: 1 1 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:9pt; line-height:140%' align='center'>" + gw_am + @"&nbsp;</td>
		        <td style='border-width: 1 0 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:9pt; line-height:140%' align='center'>" + gw_dcrmk + @"&nbsp;</td>
	        </tr>";

        }

        html_source += @"<tr height='20'>
	                    <td style='border-width: 1 1 1 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:9pt; line-height:140%' align='center' colspan='5'>
	                    총 발 주 금 액 (VAT 별도) </td>
	                    <td style='border-width: 1 0 1 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:9pt; line-height:140%' align='center' colspan='3'>
                        " + sum_am_ex.ToString("###,###,###,###,##0.####") + @" &nbsp;</td>
                                </tr>
                            </table>
                        </body>
                        </center> ";

        return html_source;
    }

    private string GW_Solid_html() //쏠리테크 html 생성
    {

        string html_source;
        string htmlSrcFile = Application.StartupPath + "\\download\\gw\\HT_PU_PO_REG2_SOLID.htm";

        using (StreamReader reader = new StreamReader(htmlSrcFile, Encoding.Default))
        {
            html_source = reader.ReadToEnd();
        }

        object[] obj = new object[] { Global.MainFrame.LoginInfo.CompanyCode, _header.CurrentRow["NO_PO"].ToString() };
        DataTable dt_GWdata = _biz.DataSearch_GW_RPT(obj);

        html_source = html_source.Replace("@@구매담당자", D.GetString(dt_GWdata.Rows[0]["NM_PTR"]) + "&nbsp;");
        html_source = html_source.Replace("@@전화번호", D.GetString(dt_GWdata.Rows[0]["NO_TEL"]) + "&nbsp;");
        html_source = html_source.Replace("@@팩스번호", D.GetString(dt_GWdata.Rows[0]["NO_FAX"]) + "&nbsp;");
        html_source = html_source.Replace("@@이메일", D.GetString(dt_GWdata.Rows[0]["NO_EMAIL"]) + "&nbsp;");

        html_source = html_source.Replace("@@거래처명", D.GetString(dt_GWdata.Rows[0]["거래처명"]) + "&nbsp;");
        html_source = html_source.Replace("@@거래처전화번호", D.GetString(dt_GWdata.Rows[0]["NO_TEL1"]) + "&nbsp;");
        html_source = html_source.Replace("@@거래처팩스번호", D.GetString(dt_GWdata.Rows[0]["NO_FAX1"]) + "&nbsp;");
        html_source = html_source.Replace("@@거래처담당자", D.GetString(dt_GWdata.Rows[0]["NM_PTR1"]) + "&nbsp;");
        html_source = html_source.Replace("@@핸드폰번호", D.GetString(dt_GWdata.Rows[0]["P_HP"]) + "&nbsp;");
        html_source = html_source.Replace("@@이메일", D.GetString(dt_GWdata.Rows[0]["P_EMAIL"]) + "&nbsp;");

        html_source = html_source.Replace("@@결제조건", D.GetString(dt_GWdata.Rows[0]["NM_FG_PAYMENT"]) + "&nbsp;");
        html_source = html_source.Replace("@@인도조건", D.GetString(dt_GWdata.Rows[0]["NM_COND_PRICE"]) + "&nbsp;");
        html_source = html_source.Replace("@@총공급가", D.GetDecimal(dt_GWdata.Rows[0]["H_AM"]).ToString("###,###,###,###,###") + "&nbsp;");
        html_source = html_source.Replace("@@부가세", D.GetDecimal(dt_GWdata.Rows[0]["H_VAT"]).ToString("###,###,###,###,###") + "&nbsp;");
        html_source = html_source.Replace("@@납품장소", D.GetString(dt_GWdata.Rows[0]["NM_ARRIVER"]) + "&nbsp;");


        int gw_no = 0;
        string gw_cd_item, gw_nm_item, gw_qt_po, gw_um, gw_am, gw_dt_limit, gw_dcrmk, gw_exch, gw_stnd_item;
        decimal sum_am_ex = 0, sum_vat = 0;
        foreach (DataRow dr in dt_GWdata.Rows)
        {
            gw_no = gw_no + 1;
            gw_cd_item = D.GetString(dr["CD_ITEM"]);
            gw_nm_item = D.GetString(dr["NM_ITEM"]);
            gw_stnd_item = D.GetString(dr["STND_ITEM"]);
            gw_qt_po = D.GetDecimal(dr["QT_PO_MM"]).ToString("###,###,###,###,##0");
            gw_exch = D.GetString(dr["NM_EXCH"]);
            gw_um = D.GetDecimal(dr["UM"]).ToString("###,###,###,###,##0.####");
            gw_am = D.GetDecimal(dr["AM"]).ToString("###,###,###,###,##0.####");
            gw_dt_limit = D.GetString(dr["DT_LIMIT"]);
            gw_dcrmk = D.GetString(dr["DC1"]);
            sum_am_ex += D.GetDecimal(dr["AM"]);
            sum_vat += D.GetDecimal(dr["VAT"]);

            html_source += @"
           		<tr height='25'>
		        <td style='border-width: 1 0 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center' rowspan='2' valign='top'>" + gw_no + @"&nbsp;</td>
		        <td style='border-width: 1 0 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='left' colspan='2' rowspan='2' valign='top'>" + gw_cd_item + @"&nbsp;</td>
		        <td style='border-width: 1 0 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='left' colspan='5' valign='top'>" + gw_nm_item + @"&nbsp;</td>
		        <td style='border-width: 1 0 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center' colspan='2' rowspan='2' valign='top'>" + gw_qt_po + @"&nbsp;</td>
		        <td style='border-width: 1 0 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center' rowspan='2' colspan='2' valign='top'>" + gw_exch + @"&nbsp;</td>
		        <td style='border-width: 1 0 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center' rowspan='2' valign='top'>" + gw_um + @"&nbsp;</td>
		        <td style='border-width: 1 0 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center' colspan='3' rowspan='2' valign='top'>" + gw_am + @"&nbsp;</td>
                <td style='border-width: 1 0 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center' colspan='2' rowspan='2' valign='top'>" + gw_dt_limit + @"&nbsp;</td>
		        <td style='border-width: 1 0 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='left' rowspan='2' valign='top'>" + gw_dcrmk + @"&nbsp;</td>
	        </tr>  
            <tr height='25'>
                <td style='border-width: 0 0 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='left' colspan='5' valign='top'>" + gw_stnd_item + @"&nbsp;</td>
            </tr>";

        }

        html_source += @"<tr height='25'>
                <td style='border-width: 1 1 1 1; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='left' colspan='19'>
                - 발 주 조 건 - <br>
                1. 대금결제조건: 당사 결제조건에 준함 
		                    <p style='margin-left:20; margin-top:2; margin-bottom:2'>(천만원 이상 : 익월 15일 전자어음 발행(60일), 백만원 이상 ~ 천만원 미만: 익월 15일 전자어음 
		                    발행(45일), 백만원 미만 : 익월 15일 현금지급) </p>2. 참고 사항 <br>
		                    <p style='margin-left:20; margin-top:2; margin-bottom:2'>1) 귀사는 본 발주서 접수 後 2일이내에 당사의 발주수용여부 및 납품가능일을 공식적으로 통보바랍니다. <br>
		                    2) 본 발주서에 따른 물품의 입고지연으로 발생되는 모든 손해 및 손실은 귀사에서 부담하여야 합니다. <br>
		                    &nbsp;&nbsp;
		                    [지체보상금 기준 : 총 발주금액 X 지체일 X 2.5/1,000] <br>
		                    3) 본 발주서에 명기된 견적가는 변동될 수 있습니다. <br>
		                    4) 최소 입고 3일전에 당사의 수입검사팀[TEL: 031-789-8519]으로 연락하여 관련된 사항을 협의바랍니다. 
		                    [수입검사 일정 및 수입검사 합.불합격에 따른 처리, 입고처 등] <br>
		                    5) 물품 입고간 사전동의 없이 부품변경을 하지 못하고, 만약 이를 위반할 경우 공급처에서 모든 책임을 진다 [계약서 
		                    18조 3항 개별 계약 및 특약사항 준수] <br>
		                    6) RoHS 관련사항 : 납품하는 자재가 RoHS 적용 품목일때는 RoHS관련 미사용 증명서를 제출한다. <br>
		                    7) 당사에 해당하는 관리품목(외주 주요품목 등)에 한하여 당사에서 지정한 규격의 POP 바코드를 부착하여 납품 하셔야 
		                    합니다. <br>
		                    &nbsp;&nbsp;
		                    -. 관리품목 대상확인은 당사 생산관리팀[TEL: 031-789-8505]에 문의하고 확인하시기 바랍니다 </p>
		                    3. 마감관련 <br>
		                    <p style='margin-left:20; margin-top:2; margin-bottom:2'>1) 당사의 구매마감은 매월 25일 입니다. <br>
		                    2) 세금계산서는 매월 말일 날짜로 작성 부탁드리며, 매월 25일까지 거래내역서와 세금계산서를 구매 담당자에게 전달해 
		                    주시기 바랍니다. </p>
		                    <br>
		                    4. 본 발주와 관련한 문의 사항 발생 시 (주)쏠리테크 구매팀으로 연락하시기 바랍니다. <br>
		                    <br>
		                    <p style='margin-left:20; margin-top:2; margin-bottom:2'>감사합니다.<br>
                    </td>
	            </tr>
        
                <tr height='25'>
		        <td style='border-width: 0 00 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center'>
		            &nbsp;</td>
		        <td style='border-width: 0 00 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center'>
		            &nbsp;</td>
		        <td style='border-width: 0 00 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center'>
		            &nbsp;</td>
		        <td style='border-width: 0 00 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center'>
		        &nbsp;</td>
		        <td style='border-width: 0 00 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center'>
		        &nbsp;</td>
		        <td style='border-width: 0 00 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center'>
		        &nbsp;</td>
		        <td style='border-width: 0 00 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center'>
		        &nbsp;</td>
		        <td style='border-width: 0 00 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center'>
		        &nbsp;</td>
		        <td style='border-width: 0 00 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center'>
		        &nbsp;</td>
		        <td style='border-width: 0 00 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center'>
		        &nbsp;</td>
		        <td style='border-width: 0 00 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center'>
		        &nbsp;</td>
		        <td style='border-width: 0 00 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center'>
		        &nbsp;</td>
		        <td style='border-width: 0 00 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center'>
		        &nbsp;</td>
		        <td style='border-width: 0 00 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center'>
		        &nbsp;</td>
		        <td style='border-width: 0 00 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center'>
		        &nbsp;</td>
		        <td style='border-width: 0 00 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center'>
		        &nbsp;</td>
		        <td style='border-width: 0 00 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center'>
		        &nbsp;</td>
		        <td style='border-width: 0 00 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center'>
		        &nbsp;</td>
		        <td style='border-width: 0 00 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center'>
		        &nbsp;</td>
	        </tr>
        </table>
    </body>
</center>
";
        html_source = html_source.Replace("@@총금액", D.GetDecimal(D.GetDecimal(sum_am_ex) + D.GetDecimal(sum_vat)).ToString("###,###,###,###,###") + "&nbsp;");
        return html_source;
    }

    private string 전자결재양식생성_광진윈텍() // 2011.06.10 추가(최규원)
    {
        try
        {
            string downPath_Html = Application.StartupPath + "\\download\\gw\\" + "HT_PU_PO_REG2_KJWT.htm";  //HTML 파일경로
            string html_source = "";//File.ReadAllText(downPath_Html, System.Text.UTF8Encoding.UTF8);            //HTML 파일읽기
            using (StreamReader reader = new StreamReader(downPath_Html, Encoding.Default))
            {
                html_source = reader.ReadToEnd();
            }

            object[] obj = new object[] { Global.MainFrame.LoginInfo.CompanyCode, _header.CurrentRow["NO_PO"].ToString() };
            DataTable dt_Gntel = _biz.DataSearch_GW_RPT(obj);
            if (dt_Gntel == null || dt_Gntel.Rows.Count == 0)
                return "";

            string str_html = string.Empty, str_html_detail = string.Empty;
            //헤더부분
            string ln_partner = (D.GetString(dt_Gntel.Rows[0]["LN_PARTNER"]) != string.Empty) ? D.GetString(dt_Gntel.Rows[0]["LN_PARTNER"]) : "&nbsp";
            string nm_ceo1 = (D.GetString(dt_Gntel.Rows[0]["NM_CEO1"]) != string.Empty) ? D.GetString(dt_Gntel.Rows[0]["NM_CEO1"]) : "&nbsp";
            string ads1 = (D.GetString(dt_Gntel.Rows[0]["ADS1"]) + " " + D.GetString(dt_Gntel.Rows[0]["ADS_DETAIL1"]) != string.Empty) ? (D.GetString(dt_Gntel.Rows[0]["ADS1"]) + " " + D.GetString(dt_Gntel.Rows[0]["ADS_DETAIL1"])) : "&nbsp";
            string no_tel1 = (D.GetString(dt_Gntel.Rows[0]["NO_TEL1"]) != string.Empty) ? D.GetString(dt_Gntel.Rows[0]["NO_TEL1"]) : "&nbsp";
            string no_fax1 = (D.GetString(dt_Gntel.Rows[0]["NO_FAX1"]) != string.Empty) ? D.GetString(dt_Gntel.Rows[0]["NO_FAX1"]) : "&nbsp";
            string nm_ptr1 = (D.GetString(dt_Gntel.Rows[0]["NM_PTR1"]) != string.Empty) ? D.GetString(dt_Gntel.Rows[0]["NM_PTR1"]) : "&nbsp";
            string no_po = (D.GetString(dt_Gntel.Rows[0]["NO_PO"]) != string.Empty) ? D.GetString(dt_Gntel.Rows[0]["NO_PO"]) : "&nbsp";
            string dt_po = (D.GetString(dt_Gntel.Rows[0]["DT_PO"]) != string.Empty) ? dt_Gntel.Rows[0]["DT_PO"].ToString().Substring(0, 4) + "." + dt_Gntel.Rows[0]["DT_PO"].ToString().Substring(4, 2) + "." + dt_Gntel.Rows[0]["DT_PO"].ToString().Substring(6, 2) : "&nbsp";
            string dc50_po = (D.GetString(dt_Gntel.Rows[0]["DC50_PO"]) != string.Empty) ? D.GetString(dt_Gntel.Rows[0]["DC50_PO"]) : "&nbsp";

            string biz_num = (D.GetString(dt_Gntel.Rows[0]["BIZ_NUM1"]) != string.Empty) ? D.GetString(dt_Gntel.Rows[0]["BIZ_NUM1"]) : "&nbsp";
            string nm_ceo = (D.GetString(dt_Gntel.Rows[0]["NM_CEO"]) != string.Empty) ? D.GetString(dt_Gntel.Rows[0]["NM_CEO"]) : "&nbsp";
            string ads = (D.GetString(dt_Gntel.Rows[0]["ADS"]) + " " + D.GetString(dt_Gntel.Rows[0]["ADS_DETAIL"]) != string.Empty) ? D.GetString(dt_Gntel.Rows[0]["ADS"]) + " " + D.GetString(dt_Gntel.Rows[0]["ADS_DETAIL"]) : "&nbsp";
            string no_tel = (D.GetString(dt_Gntel.Rows[0]["NO_TEL"]) != string.Empty) ? D.GetString(dt_Gntel.Rows[0]["NO_TEL"]) : "&nbsp";
            string no_fax = (D.GetString(dt_Gntel.Rows[0]["NO_FAX"]) != string.Empty) ? D.GetString(dt_Gntel.Rows[0]["NO_FAX"]) : "&nbsp";
            string nm_ptr = (D.GetString(dt_Gntel.Rows[0]["NM_PTR"]) != string.Empty) ? D.GetString(dt_Gntel.Rows[0]["NM_PTR"]) : "&nbsp";//담당자

            html_source = html_source.Replace("@@LN_PARTNER", ln_partner + "&nbsp;");
            html_source = html_source.Replace("@@NM_CEO1", nm_ceo1 + "&nbsp;");
            html_source = html_source.Replace("@@ADS1", ads1 + "&nbsp;");
            html_source = html_source.Replace("@@NO_TEL1", no_tel1 + "&nbsp;");
            html_source = html_source.Replace("@@NO_FAX1", no_fax1 + "&nbsp;");
            html_source = html_source.Replace("@@NM_PTR1", nm_ptr1 + "&nbsp;");
            html_source = html_source.Replace("@@NO_PO", no_po + "&nbsp;");
            html_source = html_source.Replace("@@DT_PO", dt_po + "&nbsp;");
            html_source = html_source.Replace("@@DC50_PO", dc50_po + "&nbsp;");

            html_source = html_source.Replace("@@BIZ_NUM", biz_num + "&nbsp;");
            html_source = html_source.Replace("@@NM_CEO", nm_ceo + "&nbsp;");
            html_source = html_source.Replace("@@ADS", ads + "&nbsp;");
            html_source = html_source.Replace("@@NO_TEL", no_tel + "&nbsp;");
            html_source = html_source.Replace("@@NO_FAX", no_fax + "&nbsp;");
            html_source = html_source.Replace("@@NM_PTR", nm_ptr + "&nbsp;");

            //-----------------------------
            string cd_item = "&nbsp;";
            string nm_item = "&nbsp;";
            string stnd_item = "&nbsp;";
            decimal no_num = 0;
            decimal qt_po_mm = 0M;
            decimal um_ex_po = 0M;
            decimal am_ex = 0M;
            decimal am = 0M;
            decimal vat = 0M;
            string dt_limit = string.Empty;
            string unit_im = "&nbsp;";
            string dc_line = "";
            decimal qt_po_mm_pjt = 0M;
            decimal um_ex_po_pjt = 0M;
            decimal am_ex_pjt = 0M;
            string dc_line_pjt = "";
            decimal sum_qt = 0M;
            decimal sum_am_ex = 0M;
            decimal sum_am = 0M;
            decimal sum_vat = 0M;

            //dt_po = dt_po.Substring(0, 4) + "." + dt_po.Substring(4, 2) + dt_po.Substring(6, 2);

            //라인부분-------------------------
            foreach (DataRow dr in dt_Gntel.Rows)
            {
                no_num++;
                cd_item = (D.GetString(dr["CD_ITEM"]) != string.Empty) ? D.GetString(dr["CD_ITEM"]) : "&nbsp;";
                nm_item = (D.GetString(dr["NM_ITEM"]) != string.Empty) ? D.GetString(dr["NM_ITEM"]) : "&nbsp;";
                stnd_item = (D.GetString(dr["STND_ITEM"]) != string.Empty) ? D.GetString(dr["STND_ITEM"]) : "&nbsp;";
                unit_im = (D.GetString(dr["UNIT_IM"]) != string.Empty) ? D.GetString(dr["UNIT_IM"]) : "&nbsp;";
                dt_limit = dr["DT_LIMIT"].ToString().Substring(0, 4) + "." + dr["DT_LIMIT"].ToString().Substring(4, 2) + "." + dr["DT_LIMIT"].ToString().Substring(6, 2);
                qt_po_mm = Convert.ToDecimal(dr["QT_PO_MM"]);
                um_ex_po = Convert.ToDecimal(dr["UM_EX_PO"]);
                am_ex = Convert.ToDecimal(dr["AM_EX"]);
                am = Convert.ToDecimal(dr["AM"]);
                vat = Convert.ToDecimal(dr["VAT"]);
                dc_line = (D.GetString(dr["DC1"]) != string.Empty) ? D.GetString(dr["DC1"]) : "&nbsp;";
                qt_po_mm_pjt = D.GetDecimal(dr["QT"]);
                um_ex_po_pjt = D.GetDecimal(dr["UM_PO"]);
                am_ex_pjt = D.GetDecimal(dr["AM_EXPO_CIS"]);
                dc_line_pjt = D.GetString(dr["DC_RMK_PJT"]);
                sum_am_ex += D.GetDecimal(dr["AM_EX"]);
                sum_qt += D.GetDecimal(dr["QT_PO_MM"]);
                sum_am += D.GetDecimal(dr["AM"]);
                sum_vat += D.GetDecimal(dr["VAT"]);

                //라인부분
                html_source += @"<tr height='25'>
                            <td style='border-left-width:1; border-bottom-width:1px' style='line-height: 140%' align=''>" + no_num + @" </td>
                            <td style='border-left-width:0; border-bottom-width:1px' style='line-height: 140%'>" + cd_item + @"</td>                                    
                            <td style='border-left-width:0; border-bottom-width:1px' style='line-height: 140%'>" + nm_item + @"</td>                                    
                            <td style='border-left-width:0; border-bottom-width:1px' style='line-height: 140%'>" + stnd_item + @"</td>                                    
                            <td style='border-left-width:0; border-bottom-width:1px' style='line-height: 140%'>" + unit_im + @"</td>                                    
                            <td style='border-left-width:0; border-bottom-width:1px' style='line-height: 140%'>" + dt_limit + @"</td>                                    
                            <td style='border-left-width:0; border-bottom-width:1px' style='line-height: 140%' align='right'>" + um_ex_po.ToString("###,###,###,###,##0") + @"</td>
                            <td style='border-left-width:0; border-bottom-width:1px' style='line-height: 140%' align='right'>" + qt_po_mm.ToString("###,###,###,###,##0") + @"</td>
                            <td style='border-left-width:0; border-bottom-width:1px' style='line-height: 140%' align='right'>" + am_ex.ToString("###,###,###,###,##0") + @"</td>    
                            <td style='border-left-width:0; border-bottom-width:1px' style='line-height: 140%' align='right'>" + am.ToString("###,###,###,###,##0") + @"</td>                                    
                            <td style='border-left-width:0; border-bottom-width:1px' style='line-height: 140%' align='right'>" + vat.ToString("###,###,###,###,##0") + @"</td>    
	                    </tr>";
            }

            html_source += @"<tr height='25'>
                            <td style='border-top:1 solid #000000; border-bottom:1 solid #000000;' style='line-height: 140%' width='44'>　</td>
                            <td style='border-top:1 solid #000000; border-bottom:1 solid #000000;' style='line-height: 140%' width='92'>　</td>
                            <td style='border-top:1 solid #000000; border-bottom:1 solid #000000;' style='line-height: 140%' width='92'>　</td>
                            <td style='border-top:1 solid #000000; border-bottom:1 solid #000000;' style='line-height: 140%' width='93'>　</td>
                            <td style='border-top:1 solid #000000; border-bottom:1 solid #000000;' style='line-height: 140%' width='93'>　</td>
                            <td style='border-top:1 solid #000000; border-bottom:1 solid #000000;' style='line-height: 140%' width='84'><b>합 계</b></td>
                            <td style='border-top:1 solid #000000; border-bottom:1 solid #000000;' style='line-height: 140%' width='93'>　</td>
                            <td style='border-top:1 solid #000000; border-bottom:1 solid #000000;' style='line-height: 140%' width='84' align='right'>" + sum_qt.ToString("###,###,###,###,##0") + @"&nbsp;</td>
		                    <td style='border-top:1 solid #000000; border-bottom:1 solid #000000;' style='line-height: 140%' width='84' align='right'>" + sum_am_ex.ToString("###,###,###,###,##0") + @"&nbsp;</td>
		                    <td style='border-top:1 solid #000000; border-bottom:1 solid #000000;' style='line-height: 140%' width='97' align='right'>" + sum_am.ToString("###,###,###,###,##0") + @"&nbsp;</td>
		                    <td style='border-top:1 solid #000000; border-bottom:1 solid #000000;' style='line-height: 140%' height='25' width='77' align='right'>" + sum_vat.ToString("###,###,###,###,##0") + @"&nbsp;</td>
	                    </tr>
                    </table>
                    <br>	
                    <br>
                    <table width='945' border='0' cellpadding='0' cellspacing='0' style='font-size: 9pt' height='60'>
	                    <colgroup width='25%' align='center'></colgroup>
	                    <colgroup width='25%' align='center'></colgroup>
	                    <colgroup width='25%' align='center'></colgroup>
	                    <colgroup width='25%' align='center'></colgroup>
	                    <tr>
		                    <td style='border-left-width:1; border-left-style:solid; border-top-style:solid; border-top-width:1px' style='line-height: 140%' align='left' bordercolor='#000000' valign='middle'>
		                    <p>&nbsp;&nbsp; 1. 귀사의 적극적인 업무협조에 감사드립니다.</p>
		                    </td>
		                    <td style='border-right-style:solid; border-right-width:1px; border-top-style:solid; border-top-width:1px' style='line-height: 140%' align='left' bordercolor='#000000' valign='middle'>
		                    <p>&nbsp;&nbsp; 3. 지정한 장소에 납품 바랍니다.</p>
		                    </td>
		                    </tr>
	                    <tr>
		                    <td style='border-bottom:1 solid #000000; border-left-style:solid; border-left-width:1px' style='line-height: 140%' align='left' bordercolor='#000000' valign='middle'>
		                    &nbsp;&nbsp;
			                    2. 상기와 같이 발주하오니, 납기 준수바라오며,</td>
		                    <td style='border-bottom:1 solid #000000; border-right-style:solid; border-right-width:1px' style='line-height: 140%' align='left' bordercolor='#000000' valign='middle'>
		                    <p>&nbsp;&nbsp; 4. 당사 생산계획 변경시 수량 및 입고 요청일이 변경될 수 있습니다.</p>
		                    </td>
		                    </tr>
                    </table> 
                </body>
                </center>
                ";


            return html_source;

        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }

        return string.Empty;
    }

    private string 전자결재양식생성_디엠티()
    {
        string downPath_Html = Application.StartupPath + "\\download\\gw\\" + "HT_P_PO_REG2_DMT.htm";  //HTML 파일경로
        string html_source = "";//File.ReadAllText(downPath_Html, System.Text.UTF8Encoding.UTF8);            //HTML 파일읽기
        using (StreamReader reader = new StreamReader(downPath_Html, Encoding.Default))
        {
            html_source = reader.ReadToEnd();
        }

        object[] obj = new object[] { Global.MainFrame.LoginInfo.CompanyCode, _header.CurrentRow["NO_PO"].ToString() };
        DataTable dt_GWdata = _biz.DataSearch_GW_RPT(obj);
        if (dt_GWdata == null || dt_GWdata.Rows.Count == 0)
            return "";

        html_source = html_source.Replace("@@거래처", D.GetString(dt_GWdata.Rows[0]["LN_PARTNER"]) + "&nbsp;");
        html_source = html_source.Replace("@@거래_대표자", D.GetString(dt_GWdata.Rows[0]["NM_CEO1"]) + "&nbsp;");
        html_source = html_source.Replace("@@거래_주소", D.GetString(dt_GWdata.Rows[0]["ADS1"]) + " " + D.GetString(dt_GWdata.Rows[0]["ADS_DETAIL1"]) + "&nbsp;");
        html_source = html_source.Replace("@@거래_전화", D.GetString(dt_GWdata.Rows[0]["NO_TEL1"]) + "&nbsp;");
        html_source = html_source.Replace("@@거래_팩스", D.GetString(dt_GWdata.Rows[0]["NO_FAX1"]) + "&nbsp;");
        html_source = html_source.Replace("@@거래_담당자", D.GetString(dt_GWdata.Rows[0]["NM_PTR1"]) + "&nbsp;");
        html_source = html_source.Replace("@@발주번호", D.GetString(dt_GWdata.Rows[0]["NO_PO"]) + "&nbsp;");
        html_source = html_source.Replace("@@발주일자", D.GetString(dt_GWdata.Rows[0]["DT_PO"]) + "&nbsp;");
        html_source = html_source.Replace("@@비고", D.GetString(dt_GWdata.Rows[0]["DC_RMK_TEXT"]).Replace("\n", "<br>") + "&nbsp;");
        html_source = html_source.Replace("@@환종", D.GetString(dt_GWdata.Rows[0]["NM_EXCH"]) + "&nbsp;");
        html_source = html_source.Replace("@@환율", D.GetString(dt_GWdata.Rows[0]["RT_EXCH"]) + "&nbsp;");

        html_source = html_source.Replace("@@사업자등록번호", D.GetString(dt_GWdata.Rows[0]["BIZ_NUM"]) + "&nbsp;");
        html_source = html_source.Replace("@@대표자", D.GetString(dt_GWdata.Rows[0]["NM_CEO"]) + "&nbsp;");
        html_source = html_source.Replace("@@주소", D.GetString(dt_GWdata.Rows[0]["ADS"]) + " " + D.GetString(dt_GWdata.Rows[0]["ADS_DETAIL"]) + "&nbsp;");
        html_source = html_source.Replace("@@전화", D.GetString(dt_GWdata.Rows[0]["NO_TEL"]) + "&nbsp;");
        html_source = html_source.Replace("@@팩스", D.GetString(dt_GWdata.Rows[0]["NO_FAX"]) + "&nbsp;");
        html_source = html_source.Replace("@@담당자", D.GetString(dt_GWdata.Rows[0]["NM_PTR"]) + "&nbsp;");
        html_source = html_source.Replace("@@지급조건", D.GetString(dt_GWdata.Rows[0]["NM_FG_PAYMENT"]) + "&nbsp;");

        string LINEDATA = string.Empty;
        int gw_no = 0;
        string gw_cd_item, gw_nm_item, gw_stnd_item, gw_unit_im, gw_dt_limit, gw_qt_po, gw_um, gw_am, gw_dcrmk;
        decimal sum_am_ex=decimal.Zero;

        foreach (DataRow dr in dt_GWdata.Rows)
        {
            gw_no = gw_no + 1;
            gw_cd_item = D.GetString(dr["CD_ITEM"]);
            gw_nm_item = D.GetString(dr["NM_ITEM"]);
            gw_stnd_item = D.GetString(dr["STND_ITEM"]);
            gw_unit_im = D.GetString(dr["UNIT_IM"]);
            gw_dt_limit = dr["DT_LIMIT"].ToString().Substring(0, 4) + "." + dr["DT_LIMIT"].ToString().Substring(4, 2) + "." + dr["DT_LIMIT"].ToString().Substring(6, 2);
            gw_qt_po = D.GetDecimal(dr["QT_PO_MM"]).ToString("###,###,###,###,##0");
            gw_um = D.GetDecimal(dr["UM_EX_PO"]).ToString("###,###,###,###,##0.####");
            gw_am = D.GetDecimal(dr["AM_EX"]).ToString("###,###,###,###,##0.####");
            gw_dcrmk = D.GetString(dr["DC1"]);
            sum_am_ex += D.GetDecimal(dr["AM_EX"]);

//                html_source += @"
//                        <tr height='30'>
//                        <td>" + gw_no + @"</td>
//                        <td>" + gw_cd_item + @"</td>
//                        <td>" + gw_nm_item + @"</td>              
//                        <td>" + gw_stnd_item + @"</td>
//                        <td align='center'>" + gw_unit_im + @"</td>
//                        <td align='center'>" + gw_dt_limit + @"</td>
//                        <td align='right'>" + gw_um + @"</td>
//                        <td align='right'>" + gw_qt_po + @"</td>
//                        <td align='right'>" + gw_am + @"</td>
//                        <td align='right'>" + gw_dcrmk + @"</td>";

            LINEDATA += @" 
                    <tr height='30'>
                        <td align='center' style='border-top: 1px solid #000000; border-left: 1px solid #000000'>
                        " + gw_no + @"&nbsp;</td>
                        <td align='center' style='border-top: 1px solid #000000; border-left: 1px solid #000000'>
                        " + gw_cd_item + @"&nbsp;</td>
                        <td  align='center' style='border-top: 1px solid #000000; border-left: 1px solid #000000'>
                        " + gw_nm_item + @"&nbsp;</td> 
                        <td  align='center' style='border-top: 1px solid #000000; border-left: 1px solid #000000'>
                        " + gw_stnd_item + @"&nbsp;</td>
                        <td align='center' style='border-top: 1px solid #000000;  border-left: 1px solid #000000'>
                        " + gw_unit_im + @"&nbsp;</td>
                        <td align='center' style='border-top: 1px solid #000000;  border-left: 1px solid #000000'>
                        " + gw_dt_limit + @"&nbsp;</td>
                        <td align='center' style='border-top: 1px solid #000000;  border-left: 1px solid #000000'>
                        " + gw_um + @"&nbsp;</td>
                        <td align='center' style='border-top: 1px solid #000000;  border-left: 1px solid #000000'>
                        " + gw_qt_po + @"&nbsp;</td>
                        <td align='center' style='border-top: 1px solid #000000;  border-left: 1px solid #000000'>
                        " + gw_am + @"&nbsp;</td>
                        <td align='center' style='border-top: 1px solid #000000;  border-right: 1px solid #000000; border-left: 1px solid #000000'>
                        " + gw_dcrmk + @"&nbsp;</td>
                    </tr>";

        }

        html_source = html_source.Replace("@@LINEDATA", LINEDATA);
        html_source = html_source.Replace("@@SUM_AM_EX", sum_am_ex.ToString("###,###,###,###,##0") + "&nbsp;");


        return html_source;
    }

    private string 전자결재양식생성_피앤이()
    {
        string downPath_Html = Application.StartupPath + "\\download\\gw\\" + "HT_P_PO_REG2_PNE.htm";  //HTML 파일경로
        string html_source = "";         
        using (StreamReader reader = new StreamReader(downPath_Html, Encoding.Default))
        {
            html_source = reader.ReadToEnd(); //HTML 파일읽기
        }

        object[] obj = new object[] { Global.MainFrame.LoginInfo.CompanyCode, _header.CurrentRow["NO_PO"].ToString() };
        DataTable dt_GWdata = _biz.DataSearch_GW_RPT(obj);
        if (dt_GWdata == null || dt_GWdata.Rows.Count == 0)
            return "";

        html_source = html_source.Replace("@@거래처명", D.GetString(dt_GWdata.Rows[0]["LN_PARTNER"]) + "&nbsp;");
        html_source = html_source.Replace("@@공장", D.GetString(dt_GWdata.Rows[0]["NM_PLANT"]) + "&nbsp;"); 
        html_source = html_source.Replace("@@거래_대표자", D.GetString(dt_GWdata.Rows[0]["NM_CEO1"]) + "&nbsp;");
        html_source = html_source.Replace("@@거래_주소", D.GetString(dt_GWdata.Rows[0]["ADS1"]) + " " + D.GetString(dt_GWdata.Rows[0]["ADS_DETAIL1"]) + "&nbsp;");
        html_source = html_source.Replace("@@거래_전화", D.GetString(dt_GWdata.Rows[0]["NO_TEL1"]) + "&nbsp;");
        html_source = html_source.Replace("@@거래_팩스", D.GetString(dt_GWdata.Rows[0]["NO_FAX1"]) + "&nbsp;");
        html_source = html_source.Replace("@@거래_담당자", D.GetString(dt_GWdata.Rows[0]["NM_PTR1"]) + "&nbsp;");
        html_source = html_source.Replace("@@발주번호", D.GetString(dt_GWdata.Rows[0]["NO_PO"]) + "&nbsp;");
        html_source = html_source.Replace("@@발주일자", D.GetString(dt_GWdata.Rows[0]["DT_PO"]) + "&nbsp;");
        html_source = html_source.Replace("@@비고", D.GetString(dt_GWdata.Rows[0]["DC50_PO"]) + "&nbsp;");
        html_source = html_source.Replace("@@환종", D.GetString(dt_GWdata.Rows[0]["NM_EXCH"]) + "&nbsp;");
        html_source = html_source.Replace("@@환율", D.GetString(dt_GWdata.Rows[0]["RT_EXCH"]) + "&nbsp;");

        html_source = html_source.Replace("@@사업자등록번호", D.GetString(dt_GWdata.Rows[0]["BIZ_NUM"]) + "&nbsp;");
        html_source = html_source.Replace("@@대표자", D.GetString(dt_GWdata.Rows[0]["NM_CEO"]) + "&nbsp;");
        html_source = html_source.Replace("@@주소", D.GetString(dt_GWdata.Rows[0]["ADS"]) + " " + D.GetString(dt_GWdata.Rows[0]["ADS_DETAIL"]) + "&nbsp;");
        html_source = html_source.Replace("@@전화", D.GetString(dt_GWdata.Rows[0]["NO_TEL"]) + "&nbsp;");
        html_source = html_source.Replace("@@팩스", D.GetString(dt_GWdata.Rows[0]["NO_FAX"]) + "&nbsp;");
        html_source = html_source.Replace("@@담당자", D.GetString(dt_GWdata.Rows[0]["NM_PTR"]) + "&nbsp;");

         int gw_no = 0;
        string gw_cd_item, gw_nm_item, gw_stnd_item, gw_unit_im, gw_dt_limit, gw_qt_po, gw_um, gw_am, gw_dcrmk;

        foreach (DataRow dr in dt_GWdata.Rows)
        {
            gw_no = gw_no + 1;
            gw_cd_item = D.GetString(dr["CD_ITEM"]);
            gw_nm_item = D.GetString(dr["NM_ITEM"]);
            gw_stnd_item = D.GetString(dr["STND_ITEM"]);
            gw_unit_im = D.GetString(dr["UNIT_IM"]);
            gw_dt_limit = dr["DT_LIMIT"].ToString().Substring(0, 4) + "." + dr["DT_LIMIT"].ToString().Substring(4, 2) + "." + dr["DT_LIMIT"].ToString().Substring(6, 2);
            gw_qt_po = D.GetDecimal(dr["QT_PO_MM"]).ToString("###,###,###,###,##0");
            gw_um = D.GetDecimal(dr["UM_EX_PO"]).ToString("###,###,###,###,##0.####");
            gw_am = D.GetDecimal(dr["AM_EX"]).ToString("###,###,###,###,##0.####");
            gw_dcrmk = D.GetString(dr["DC1"]);

            html_source += @"<tr height='25'>
	                         <td align='center'>
	                         "+ gw_no + @"&nbsp;</td>
	                         <td align='center' style='border-left-style: solid; border-left-width: 1px'>
	                         " + gw_cd_item + @"&nbsp;</td>
	                         <td align='center' style='border-left-style: solid; border-left-width: 1px'>
	                         " + gw_nm_item + @"&nbsp;</td>
	                         <td align='center' style='border-left-style: solid; border-left-width: 1px'>
	                         " + gw_stnd_item + @"&nbsp;</td>
	                         <td align='center' style='border-left-style: solid; border-left-width: 1px'>
	                         " + gw_unit_im + @"&nbsp;</td>
	                         <td align='center' style='border-left-style: solid; border-left-width: 1px'>
	                         " + gw_dt_limit + @"&nbsp;</td>
	                         <td align='center' style='border-left-style: solid; border-left-width: 1px'>
	                         " + gw_um + @"&nbsp;</td>
	                         <td align='center' style='border-left-style: solid; border-left-width: 1px'>
	                         " + gw_qt_po + @"&nbsp;</td>
	                         <td align='center' style='border-left-style: solid; border-left-width: 1px'>
	                         " + gw_am + @"&nbsp;</td>
	                         <td align='center' style='border-left-style: solid; border-left-width: 1px'>
	                         " + gw_dcrmk + @"&nbsp;</td>
                             </tr>";
        }

        return html_source;
    }

    private string 전자결재양식생성_세미테크()
    {
        string downPath_Html = Application.StartupPath + "\\download\\gw\\" + "HT_P_PO_REG2_SEMITEC.htm";  //HTML 파일경로
        string html_source = "";//File.ReadAllText(downPath_Html, System.Text.UTF8Encoding.UTF8);            //HTML 파일읽기
        using (StreamReader reader = new StreamReader(downPath_Html, Encoding.Default))
        {
            html_source = reader.ReadToEnd();
        }

        object[] obj = new object[] { Global.MainFrame.LoginInfo.CompanyCode, _header.CurrentRow["NO_PO"].ToString() };
        DataTable dt_GWdata = _biz.DataSearch_GW_RPT(obj);
        if (dt_GWdata == null || dt_GWdata.Rows.Count == 0)
            return "";

        string html_source_LINE = string.Empty;

        int gw_no = 0;
        string gw_cd_item, gw_nm_item, gw_stnd_item, gw_unit_im, gw_dt_limit, gw_qt_po_mm, gw_um, gw_am, gw_am_ex, gw_dc1;
        string gw_cd_pjt, gw_nm_pjt;
        string gw_no_so, gw_dt_duedate, gw_qt_so;
        string gw_nm_exch, gw_rt_exch;

        decimal sum_gw_qt_po_mm = 0; decimal sum_gw_am = 0;

        foreach (DataRow dr in dt_GWdata.Rows)
        {
            gw_no = gw_no + 1;
            gw_cd_item = D.GetString(dr["CD_ITEM"]);
            gw_nm_item = D.GetString(dr["NM_ITEM"]);
            gw_stnd_item = D.GetString(dr["STND_ITEM"]);
            if (D.GetString(dr["DT_LIMIT"]) != string.Empty)
                gw_dt_limit = D.GetString(dr["DT_LIMIT"]).Substring(0, 4) + "-" + D.GetString(dr["DT_LIMIT"]).Substring(4, 2) + "-" + D.GetString(dr["DT_LIMIT"]).Substring(6, 2);
            else
                gw_dt_limit = string.Empty;

	        gw_unit_im = D.GetString(dr["UNIT_IM"]);
            gw_qt_po_mm = D.GetDecimal(dr["QT_PO_MM"]).ToString("#,###,###,##0.####");
            gw_um = D.GetDecimal(dr["UM_EX_PO"]).ToString("#,###,###,##0.####");
            gw_am = D.GetDecimal(dr["AM"]).ToString("#,###,###,##0.####");
            gw_cd_pjt = D.GetString(dr["CD_PJT"]);
	        gw_nm_pjt = D.GetString(dr["NM_PROJECT"]);
            
            gw_no_so = D.GetString(dr["NO_SO"]);
            if (D.GetString(dr["DT_DUEDATE"]) != string.Empty)
                gw_dt_duedate = D.GetString(dr["DT_DUEDATE"]).Substring(0, 4) + "-" + D.GetString(dr["DT_DUEDATE"]).Substring(4, 2) + "-" + D.GetString(dr["DT_DUEDATE"]).Substring(6, 2);
            else
                gw_dt_duedate = string.Empty;

            gw_qt_so = D.GetDecimal(dr["QT_SO"]).ToString("#,###,###,##0.####");
	        gw_dc1 = D.GetString(dr["DC1"]);

            //gw_nm_partner = D.GetString(dr["APP_LN_PARTNER"]);
            gw_nm_exch = D.GetString(dr["NM_EXCH"]);
            
            gw_rt_exch = D.GetString(dr["RT_EXCH"]);

            sum_gw_qt_po_mm += D.GetDecimal(dr["QT_PO_MM"]);
            sum_gw_am += D.GetDecimal(dr["AM"]);

            if (gw_nm_exch != "KRW")
                gw_um += "[" + gw_nm_exch + "/" + gw_rt_exch + "]";


            html_source_LINE += @"<tr height='25'>
		                    <td style='border-width: 1 1 0 1; border-style:solid; border-color:#000000; font-family:굴림; font-size:9pt; line-height:140%' align='center'>
		                    " + gw_no + @"&nbsp;</td>
		                    <td style='border-width: 1 1 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:9pt; line-height:140%' align='center'>
		                    " + gw_cd_item + @"&nbsp;</td>
		                    <td style='border-width: 1 1 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:9pt; line-height:140%' align='center'>
		                    " + gw_nm_item + @"&nbsp;</td>
		                    <td style='border-width: 1 1 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:9pt; line-height:140%' align='center'>
		                    " + gw_stnd_item + @"&nbsp;</td>
		                    <td style='border-width: 1 1 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:9pt; line-height:140%' align='center'>
		                    " + gw_dt_limit + @"&nbsp;</td>
		                    <td style='border-width: 1 1 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:9pt; line-height:140%' align='center'>
		                    " + gw_unit_im + @"&nbsp;</td>
		                    <td style='border-width: 1 1 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:9pt; line-height:140%' align='center'>
		                    " + gw_qt_po_mm + @"&nbsp;</td>
		                    <td style='border-width: 1 1 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:9pt; line-height:140%' align='center'>
		                    " + gw_um + @"&nbsp;</td>
		                    <td style='border-width: 1 1 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:9pt; line-height:140%' align='center'>
		                    " + gw_am + @"&nbsp;</td>
                            <td style='border-width: 1 1 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:9pt; line-height:140%' align='center'>
		                    " + gw_cd_pjt+"/"+gw_nm_pjt + @"&nbsp;</td>
                            <td style='border-width: 1 1 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:9pt; line-height:140%' align='center'>
		                    " + gw_no_so + @"&nbsp;</td>
                            <td style='border-width: 1 1 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:9pt; line-height:140%' align='center'>
		                    " + gw_qt_so + @"&nbsp;</td>
                            <td style='border-width: 1 1 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:9pt; line-height:140%' align='center'>
		                    " + gw_dt_duedate + @"&nbsp;</td>
                            <td style='border-width: 1 1 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:9pt; line-height:140%' align='center'>
		                    " + gw_dc1 + @"&nbsp;</td>
	                        </tr>";
        }

        html_source = html_source.Replace("@@LINEDATA", html_source_LINE);

        html_source = html_source.Replace("@@NO_PO", D.GetString(dt_GWdata.Rows[0]["NO_PO"]) + "&nbsp;");

        if (D.GetString(dt_GWdata.Rows[0]["DT_PO"]) != string.Empty)
            gw_dt_limit = D.GetString(dt_GWdata.Rows[0]["DT_PO"]).Substring(0, 4) + "-" + D.GetString(dt_GWdata.Rows[0]["DT_PO"]).Substring(4, 2) + "-" + D.GetString(dt_GWdata.Rows[0]["DT_PO"]).Substring(6, 2);
        else
            gw_dt_limit = string.Empty;

        html_source = html_source.Replace("@@DT_PO", gw_dt_limit);
        html_source = html_source.Replace("@@NM_DEPT", D.GetString(dt_GWdata.Rows[0]["NM_DEPT"]) + "&nbsp;");
        html_source = html_source.Replace("@@NM_EMP", D.GetString(dt_GWdata.Rows[0]["NO_EMP"]) + "[" + D.GetString(dt_GWdata.Rows[0]["NM_PTR"]) + "]" + "&nbsp;");
        html_source = html_source.Replace("@@CD_PARTNER", D.GetString(dt_GWdata.Rows[0]["CD_PARTNER"]) + "[" + D.GetString(dt_GWdata.Rows[0]["LN_PARTNER"]) + "]" + "&nbsp;");
        html_source = html_source.Replace("@@NM_FG_PAYMENT", D.GetString(dt_GWdata.Rows[0]["FG_PAYMENT"]) + "[" + D.GetString(dt_GWdata.Rows[0]["NM_FG_PAYMENT"]) + "]" + "&nbsp;");


        html_source = html_source.Replace("@@SUM_GW_GW_QT_PO_MM", sum_gw_qt_po_mm.ToString("#,###,###,##0.####") + "&nbsp;");
        html_source = html_source.Replace("@@SUM_GW_AM", sum_gw_am.ToString("#,###,###,##0.####") + "&nbsp;");
        html_source = html_source.Replace("@@DC50_PO", D.GetString(dt_GWdata.Rows[0]["DC50_PO"]) + "&nbsp;");

        return html_source;
    }

    #endregion

    #region -> 전자결(라인단위)
    private void btn전자결재L_Click(object sender, EventArgs e)
    {
        try
        {
            if (!HeaderCheck(0)) return;
            string NO_PO = D.GetString(_header.CurrentRow["NO_PO"]);

            if (NO_PO == string.Empty)
            {
                ShowMessage(공통메세지.등록되지않은자료입니다);
                return;
            }

            bool bTrue = false;
            string strURL = "";


            P_PU_PO_REG2_GW_L _gwL = new P_PU_PO_REG2_GW_L();

            //O미상신, R요청, X반려, C완료, Q미상신상태였다가 아직 재상신 안한상태..(전자결재에서 데이터를 가지고갈때 NO_DOCU와 'O'인 값을 가지고 가기 때문..)
            Dictionary<string, string> DIC_STAT = new Dictionary<string, string>();

            DIC_STAT.Add("O", DD("미상신"));
            DIC_STAT.Add("Q", DD("미상신"));
            DIC_STAT.Add("R", DD("요청"));
            DIC_STAT.Add("C", DD("완료"));
            DIC_STAT.Add("X", DD("반려"));


            DataTable dt_stat = _biz.GetFI_GWDOCU_L(NO_PO);

            string str_stat = string.Empty;

            if (dt_stat != null && dt_stat.Rows.Count != 0)
            {
                str_stat = D.GetString(dt_stat.Rows[0]["ST_STAT"]);

                if (str_stat == "R" || str_stat == "C")
                {
                    ShowMessage("전자결제 @중 입니다.", DIC_STAT[str_stat]);
                    return;
                }

            }


            DataTable dt_GWdata = _biz.DataSearch_GW_RPT(new object[] { Global.MainFrame.LoginInfo.CompanyCode, NO_PO, Global.MainFrame.LoginInfo.UserID });

            if (dt_GWdata == null || dt_GWdata.Rows.Count == 0)
            {
                ShowMessage(공통메세지.작업을정상적으로처리하지못했습니다);
                return;
            }

            DataTable dtResult = _gwL.getGwSearch(m_Elec_app, dt_GWdata, out strURL);

            if (dtResult == null || dtResult.Rows.Count == 0)
            {
                ShowMessage(공통메세지.작업을정상적으로처리하지못했습니다);
                return;
            }

            bTrue = _biz.전자결재_실제사용_L(dtResult);

            if (!bTrue)
            {
                ShowMessage(공통메세지.작업을정상적으로처리하지못했습니다);
                return;
            }

            System.Diagnostics.Process.Start("IExplore.exe", strURL);


        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
    }

    #endregion

    //#region -> bp_Project_Click

    //private void bp_Project_Click(object sender, SearchEventArgs e)
    //{
    //    sale.P_SA_PRJ_SUB dlg = null;

    //    try
    //    {
    //        dlg = new sale.P_SA_PRJ_SUB();

    //        if (dlg.ShowDialog() == DialogResult.OK)
    //        {
    //            txt_NoProject.Text = dlg.returnParams[0];   //프로젝트번호
    //            bp_Project.Text = dlg.returnParams[4];      //프로젝트명
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        MsgEnd(ex);
    //    }
    //}

    //#endregion

    #region -> btn_PRJ_SUB_Click

    private void btn_PRJ_SUB_Click(object sender, EventArgs e)
    {
        if (!HeaderCheck(1)) return;     //필수항목 검사d

        P_PU_REG_PRJ_SUB dlg = null;

        try
        {
            string[] Params = new string[4];

            Params[0] = ctx프로젝트.CodeValue;
            Params[1] = ctx프로젝트.CodeName;
            Params[2] = tb_NM_PARTNER.CodeValue;
            Params[3] = tb_NM_PARTNER.CodeName;

            dlg = new P_PU_REG_PRJ_SUB(Params, _flexD.DataTable);

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                bool b평형비고체크 = dlg.평형비고체크;
                InserGridtAdd(dlg.gdt_return, b평형비고체크);
                ctx프로젝트.Enabled = false; btn적용.Enabled = false;
                //SetHeadControlEnabled(false, 3);
            }
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
    }

    #endregion

    #region -> btn적용_Click

    private void btn적용_Click(object sender, EventArgs e)
    {
        try
        {
            if (_flexD.DataTable == null || ctx프로젝트.CodeName.ToString() == "") return;
            if (!확정여부()) return;
            DataTable dtZOO = null;

              //조원관광진흥(주)전용.. 
              if (Global.MainFrame.ServerKeyCommon.ToUpper() == "ZOONE")
                  dtZOO = _biz.GetInfo_ZOO(ctx프로젝트.CodeValue);

            
            for(int i_row = _flexD.Rows.Fixed; i_row < _flexD.Rows.Count; i_row++)
            {


                _flexD.Rows[i_row]["CD_PJT"] = ctx프로젝트.CodeValue;
                _flexD.Rows[i_row]["NM_PJT"] = ctx프로젝트.CodeName;
                _flexD.Rows[i_row]["SEQ_PROJECT"] = 0;


                if (Config.MA_ENV.YN_UNIT == "Y")
                {
                    _flexD.Rows[i_row]["SEQ_PROJECT"] = d_SEQ_PROJECT;
                    _flexD.Rows[i_row]["CD_PJT_ITEM"] = s_CD_PJT_ITEM;
                    _flexD.Rows[i_row]["NM_PJT_ITEM"] = s_NM_PJT_ITEM;
                    _flexD.Rows[i_row]["PJT_ITEM_STND"] = s_PJT_ITEM_STND;
                }

                if (Global.MainFrame.ServerKeyCommon == "UNIPOINT")
                {
                    _flexD.Rows[i_row]["LN_PARTNER_PJT"] = s_NM_PARTNER_PJT;
                    _flexD.Rows[i_row]["CD_PARTNER_PJT"] = s_CD_PARTNER_PJT;
                    _flexD.Rows[i_row]["NO_EMP_PJT"] = s_NO_EMP_PJT;
                    _flexD.Rows[i_row]["NM_KOR_PJT"] = s_NM_EMP_PJT;
                    _flexD.Rows[i_row]["END_USER"] = s_END_USER;
                }

                if (Global.MainFrame.ServerKeyCommon.ToUpper() == "CHOSUNHOTELBA")
                {
                    FillPol(i_row);
                    object[] m_obj = new object[11];

                    m_obj[0] = D.GetString(_flexD.Rows[i_row]["CD_ITEM"]);
                    m_obj[1] = D.GetString(cbo_CD_PLANT.SelectedValue);
                    m_obj[2] = LoginInfo.CompanyCode;
                    m_obj[3] = cbo_FG_UM.SelectedValue.ToString();
                    m_obj[4] = cbo_NM_EXCH.SelectedValue.ToString();
                    m_obj[5] = tb_DT_PO.Text;
                    m_obj[6] = tb_NM_PARTNER.CodeValue;
                    m_obj[7] = tb_NM_PURGRP.CodeValue;
                    m_obj[8] = "N";
                    m_obj[9] = ctx프로젝트.CodeValue;
                    m_obj[10] = Global.MainFrame.ServerKeyCommon.ToUpper();

                    품목정보구하기(m_obj, "GRID", i_row);
                }

                if (m_sEnv_CC == "200")
                    SetCC(i_row, string.Empty);

                //조원관광진흥(주)전용.. 
                
                if (Global.MainFrame.ServerKeyCommon.ToUpper() == "ZOONE")
                {
                    if (dtZOO != null && dtZOO.Rows.Count != 0)
                    {

                        _flexD.Rows[i_row]["CD_SL"] = dtZOO.Rows[0]["CD_SL"];
                        _flexD.Rows[i_row]["NM_SL"] = dtZOO.Rows[0]["NM_SL"];
                    }
                }


                if (BASIC.GetMAEXC("발주등록(공장)-프로젝트별_의제매입세_구분") == "100" && D.GetString(_flexD.Rows[i_row]["CD_USERDEF14"]) == "001") //의제 매입 프로젝트별 적용
                {
                    string fg_tax_josun = _biz.pjt_item_josun(D.GetString(_flexD.Rows[i_row]["CD_PJT"]));
                    if (fg_tax_josun != "")
                    {
                        _flexD.Rows[i_row]["FG_TAX"] = fg_tax_josun;
                        _flexD.Rows[i_row]["RATE_VAT"] = 0;//부가세율은 0이 들어가면된다고함 (김광석사원요청) 2011-12-02

                        Decimal vat_rate = 0;
                        _flexD.Rows[i_row]["RATE_VAT"] = vat_rate;

                        vat_rate = (vat_rate == 0) ? 0 : vat_rate / 100;

                        if (vat_rate == 0 || Convert.ToDecimal(_flexD.Rows[i_row]["AM"]) == 0)
                            _flexD.Rows[i_row]["VAT"] = 0;
                        else
                            _flexD.Rows[i_row]["VAT"] = Unit.원화금액(DataDictionaryTypes.PU, Convert.ToDecimal(_flexD.Rows[i_row]["AM"]) * vat_rate); ;

                        _flexD.Rows[i_row]["AM_TOTAL"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flexD.Rows[i_row]["AM"]) + D.GetDecimal(_flexD.Rows[i_row]["VAT"]));
                    }
                    else
                    {
                        _flexD.Rows[i_row]["FG_TAX"] = _header.CurrentRow["FG_TAX"];
                        _flexD.Rows[i_row]["RATE_VAT"] = tb_TAX.DecimalValue;//부가세율은 0이 들어가면된다고함 (김광석사원요청) 2011-12-02

                        Decimal vat_rate = tb_TAX.DecimalValue;
                        _flexD.Rows[i_row]["RATE_VAT"] = vat_rate;

                        vat_rate = (vat_rate == 0) ? 0 : vat_rate / 100;

                        if (vat_rate == 0 || Convert.ToDecimal(_flexD.Rows[i_row]["AM"]) == 0)
                            _flexD.Rows[i_row]["VAT"] = 0;
                        else
                            _flexD.Rows[i_row]["VAT"] = Unit.원화금액(DataDictionaryTypes.PU, Convert.ToDecimal(_flexD.Rows[i_row]["AM"]) * vat_rate); ;

                        _flexD.Rows[i_row]["AM_TOTAL"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flexD.Rows[i_row]["AM"]) + D.GetDecimal(_flexD.Rows[i_row]["VAT"]));
                    }

                }
            }

            ShowMessage("적용작업을완료하였습니다");
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
    }

    #endregion

    #region ->btn_H41_Apply_Click
    private void btn_H41_Apply_Click(object sender, EventArgs e)
    {
        try
        {
            if (!HeaderCheck(0)) return;

            호출여부 = true;

            pur.P_PU_H41_SUB m_dlg = new pur.P_PU_H41_SUB(_flexD.DataTable, D.GetString(cbo_CD_PLANT.SelectedValue), D.GetString(cbo_NM_EXCH.SelectedValue));
            Cursor.Current = Cursors.Default;

            if (m_dlg.ShowDialog(this) == DialogResult.OK)
            {
                DataTable ldt_dlg = m_dlg.gdt_return;

                if (ldt_dlg == null || ldt_dlg.Rows.Count <= 0) return;

                ControlButtonEnabledDisable((Control)sender, true);
                cbo_CD_PLANT.Enabled = false;

                _flexD.Redraw = false;
                decimal MaxSeq = _flexD.GetMaxValue("NO_LINE");

                for (int i = 0; i < ldt_dlg.Rows.Count; i++)
                {
                    if (ldt_dlg.Rows[i].RowState == DataRowState.Deleted)
                        continue;

                    MaxSeq++;
                    _flexD.Rows.Add();
                    _flexD.Row = _flexD.Rows.Count - 1;
                    _flexD["CD_ITEM"] = ldt_dlg.Rows[i]["CD_ITEM"];
                    _flexD["NM_ITEM"] = ldt_dlg.Rows[i]["NM_ITEM"];
                    _flexD["STND_ITEM"] = ldt_dlg.Rows[i]["STND_ITEM"];
                    _flexD["CD_UNIT_MM"] = ldt_dlg.Rows[i]["UNIT_PO"];
                    _flexD["UNIT_PO"] = ldt_dlg.Rows[i]["UNIT_PO"];
                    _flexD["STND_MA_ITEM"] = ldt_dlg.Rows[i]["STND_ITEM"];
                    _flexD["UNIT_IM"] = ldt_dlg.Rows[i]["UNIT_IM"];
                    //2010.01.12 납기일
                    if (tb_DT_LIMIT.Text == string.Empty)
                        _flexD["DT_LIMIT"] = ldt_dlg.Rows[i]["DT_LIMIT"];
                    else
                        _flexD["DT_LIMIT"] = tb_DT_LIMIT.Text;

                    _flexD["DT_PLAN"] = _flexD["DT_LIMIT"];


                    _flexD["FG_POST"] = "O";
                    _flexD["NM_SYSDEF"] = _biz.GetGubunCodeSearch("PU_C000009", _flexD["FG_POST"].ToString());//_ComfirmState;

                    _flexD["QT_PO_MM"] = ldt_dlg.Rows[i]["QT_PO_MM"];
                    _flexD["RT_PO"] = ldt_dlg.Rows[i]["RT_PO"];
                    _flexD["CD_PLANT"] = cbo_CD_PLANT.SelectedValue.ToString();
                    if (tb_NO_PO.Text != string.Empty)
                        _flexD["NO_PO"] = tb_NO_PO.Text;

                    _flexD["NO_RCV"] = "";
                    _flexD["NO_LINE"] = MaxSeq;
                    _flexD["CD_SL"] = ldt_dlg.Rows[i]["CD_SL"];  //공장품목의 입고창고
                    _flexD["NM_SL"] = ldt_dlg.Rows[i]["NM_SL"];
                    _flexD["CD_EXCH"] = ldt_dlg.Rows[i]["CD_EXCH"];
                    _flexD["DC1"] = ldt_dlg.Rows[i]["DC1"];
                    _flexD["DC2"] = ldt_dlg.Rows[i]["DC2"];
                    _flexD["UM_EX"] = ldt_dlg.Rows[i]["UM_EX"];
                    _flexD["UM_EX_PO"] = ldt_dlg.Rows[i]["UM_EX"];

                    _flexD["NO_PR"] = "";

                    tb_DT_PO.Text = ldt_dlg.Rows[i]["DT_PO"].ToString();


                    // 2009.12.08 다시 개발 cc설정 관련
                    // LINE 수정 권한이면 요청에서 적용받는다 
                    // 요청에 없으면 HEADER 설정에 따라 적용받는다.
                    // LINE 수정권한이 없으면 HEADER 설정에 따라 적용받는다.
                    //if (m_sEnv_CC_Line == "Y" && ldt_dlg.Rows[i]["CD_CC"] != null && ldt_dlg.Rows[i]["CD_CC"].ToString().Trim() != "")
                    //{
                    //    _flexD["CD_CC"] = ldt_dlg.Rows[i]["CD_CC"]; //cc코드
                    //    _flexD["NM_CC"] = ldt_dlg.Rows[i]["NM_CC"];
                    //}
                    //else
                    //{
                    SetCC(_flexD.Row, string.Empty);
                    //}

                    DataTable dt = (DataTable)cbo_NM_EXCH.DataSource;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["CODE"].ToString() == cbo_NM_EXCH.SelectedValue.ToString())
                        {
                            _flexD["NM_EXCH"] = dr["NAME"];
                            break;
                        }
                    }
                    // 발주단위에 관계없이 환산량이 있으면 환산량을 곱해서 발주수량을 만든다.
                    if (_flexD.CDecimal(_flexD["RT_PO"]) == 0)
                        _flexD["RT_PO"] = 1;
                    _flexD["QT_PO"] = (_flexD.CDecimal(ldt_dlg.Rows[i]["QT_PO_MM"]) * _flexD.CDecimal(_flexD["RT_PO"]));

                    FillPol(_flexD.Row);
                    object[] m_obj = new object[11];
                    m_obj[0] = _flexD["CD_ITEM"].ToString();
                    m_obj[1] = _flexD["CD_PLANT"].ToString();
                    m_obj[2] = LoginInfo.CompanyCode;
                    m_obj[3] = cbo_FG_UM.SelectedValue.ToString();
                    m_obj[4] = cbo_NM_EXCH.SelectedValue.ToString();
                    m_obj[5] = tb_DT_PO.Text;
                    m_obj[6] = tb_NM_PARTNER.CodeValue;
                    m_obj[7] = tb_NM_PURGRP.CodeValue;
                    m_obj[8] = "N";
                    m_obj[9] = D.GetString(_flexD["CD_PJT"]);
                    m_obj[10] = Global.MainFrame.ServerKeyCommon.ToUpper();

                    품목정보구하기(m_obj, "H41", 0);

                    decimal rate_vat = (_flexD.CDecimal(tb_TAX.DecimalValue) == 0) ? 0 : _flexD.CDecimal(tb_TAX.DecimalValue) / 100;

                    _flexD["AM"] = Unit.원화금액(DataDictionaryTypes.PU, _flexD.CDecimal(_flexD["AM_EX"]) * tb_NM_EXCH.DecimalValue);
                    _flexD["VAT"] = Unit.원화금액(DataDictionaryTypes.PU, _flexD.CDecimal(_flexD["AM"]) * rate_vat);
                    _flexD["AM_TOTAL"] = _flexD.CDecimal((_flexD[ "AM"])) + _flexD.CDecimal((_flexD[ "VAT"])); //총합계


                    _flexD.AddFinished();
                    _flexD.Col = _flexD.Cols.Fixed;
                }
                _header.CurrentRow["DC50_PO"] = ldt_dlg.Rows[ldt_dlg.Rows.Count - 1]["INVOICE_NUMBER"];
                tb_DC.Text = _header.CurrentRow["DC50_PO"].ToString();
                SUMFunction();
                SetHeadControlEnabled(false, 1);
                btn_insert.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
        finally
        {
            _flexD.Redraw = true;
        }


    }
    #endregion

    #region ->btn_Mail_Click

    private void btn_Mail_Click(object sender, EventArgs e)
    {
        Tp_print = "MAIL";
        SetPrint(false);
    }

    #endregion

    #region ->btn_Lot_size적용

    private void btn_lotsize_accept_Click(object sender, EventArgs e)
    {
        try
        {
            if (!확정여부()) return;
            DataRow[] dRows = _flexD.DataTable.Select("S = 'Y'");

            string cd_plant = D.GetString(cbo_CD_PLANT.SelectedValue);
            string Multi_cditem = string.Empty;

            if (dRows == null || dRows.Length == 0) return;

            foreach (DataRow dr in dRows)
            {
                Multi_cditem += dr["CD_ITEM"] + "|";
            }

            string[] No_PK_Multi_array = D.StringConvert.GetPipes(Multi_cditem, 200);

            DataTable dt = null;
            DataTable temp_dt = null;
            DataTable item_dt = null;
            for (int i = 0; i < No_PK_Multi_array.Length; i++)
            {

                temp_dt = _biz.공장품목(Multi_cditem, cd_plant);

                if (i == 0)//첫테이블은 복사하고 두번째 테이블부터는한줄씩 붙여넣는다
                {
                    item_dt = temp_dt.Copy();
                }
                else if (i > 0)
                {
                    foreach (DataRow dr in temp_dt.Rows)
                        item_dt.LoadDataRow(dr.ItemArray, true);
                }
            }


            for (int i = _flexD.Rows.Fixed; i < _flexD.Rows.Count; i++) //로우넘버를 구하기위해 이러한 포문을 돌리게 되었다...추가적으로 계산 로직을 모두 만드는 것보다는 금액계산 메소드를 사용하는것이 나을것같아서
            {
                if ((D.GetString(_flexD.Rows[i]["S"])) == "N") continue;
                if ((D.GetDecimal(_flexD.Rows[i]["QT_PO_MM"])) == 0) continue;

                DataRow[] dritem = item_dt.Select("CD_ITEM ='" + D.GetString(_flexD.Rows[i]["CD_ITEM"]) + "'");
                decimal lotsize = 0;
                decimal qt_po_mm = 0;
                decimal val = 0; // 몫
                decimal lot_qt = 0; // 최종값
                if (dritem != null && dritem.Length > 0)
                {
                    lotsize = D.GetDecimal(dritem[0]["LOTSIZE"]);
                    qt_po_mm = D.GetDecimal(_flexD.Rows[i]["QT_PO_MM"]);

                    if (D.GetInt(Math.Floor(qt_po_mm)) != 0 && D.GetInt(Math.Floor(lotsize)) != 0)
                    {
                        if ((qt_po_mm % lotsize) == 0)  // 나머지가 0 인것은 +1을 해줄필요가 없으므로
                            val = (D.GetInt(Math.Floor(qt_po_mm)) / D.GetInt(Math.Floor(lotsize)));
                        else
                            val = (D.GetInt(Math.Floor(qt_po_mm)) / D.GetInt(Math.Floor(lotsize))) + 1;

                    }

                    lot_qt = lotsize * (val);
                    _flexD.Rows[i]["QT_PO_MM"] = lot_qt;
                    //qt_po = lot_qt * D.GetDecimal(dritem[0]["UNIT_PO_FACT"]);



                    금액계산(i, D.GetDecimal(_flexD.Rows[i]["UM_EX_PO"]), lot_qt, "QT_PO_MM", lot_qt);
                    _flexD[i, "QT_WEIGHT"] = lot_qt * _flexD.CDecimal(_flexD[i, "WEIGHT"]);
                }
            }
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }

    }

    #endregion

    #region -> 단가 우선순위 적용

    #region -> 단가 적용 클릭
    private void btn_UM_APP_Click(object sender, EventArgs e)
    {
        if (!_flexD.HasNormalRow) return;

        if (!확정여부()) return;

        DataTable dt_um_p = null;
        string returnstring_UM = string.Empty;


        string sSetting = Settings1.Default.PARAMETER_um;
        P_PU_UM_PRIORITIZE_PO_SUB dlg = new P_PU_UM_PRIORITIZE_PO_SUB(sSetting);

        if (dlg.ShowDialog() != DialogResult.OK) return;

        Settings1.Default.PARAMETER_um = dlg.Rtn_stting;
        Settings1.Default.Save();

        returnstring_UM = dlg.Rtn_stting;
        dt_um_p = dlg.Rtn_dt;

        Grid_um_apply(dt_um_p);
    }


    #endregion

    #region -> 단가가져오기
    private void Grid_um_apply(DataTable dt_um_p)
    {
        try
        {
            string str_cd_item_M = string.Empty;
            foreach (DataRow dr in _flexD.DataTable.Rows)
            {
                str_cd_item_M += dr["CD_ITEM"] + "|";
            }

            object[] obj = new object[]
            {
                Global.MainFrame.LoginInfo.CompanyCode,
                D.GetString(tb_DT_PO.Text),
                D.GetString(cbo_CD_PLANT.SelectedValue),
                
                str_cd_item_M                                    
            
            };

            DataSet ds_um = _biz.Search_um_prioritize_item(obj);

            if (ds_um.Tables[0].Rows.Count == 0 && ds_um.Tables[1].Rows.Count == 0 && ds_um.Tables[2].Rows.Count == 0 && ds_um.Tables[3].Rows.Count == 0)
                return;
            /*
             * 1%INV%최근재고평가%MIN&";
             * "2%IVL%최근매입단가%MIN&";
             * "3%APRT%구매거래처별단가%MIN&";
             * "4%POL%최근발주단가%MIN";         
             * 
             */
            int row_int = _flexD.Rows.Fixed;
            foreach (DataRow dr_flex in _flexD.DataTable.Rows)
            {
                if (D.GetString(dr_flex["CD_ITEM"]) == "") break;
                foreach (DataRow dr_um in dt_um_p.Rows)//단가 우선순위 테이블 foreach
                {
                    DataTable dt_item_um = null;
                    DataRow[] DR_sel_exch = null;
                    if (D.GetString(dr_um["CODE"]) == "INV")//최근재고평가
                    {

                        dt_item_um = ds_um.Tables[0];


                        DR_sel_exch = dt_item_um.Select("CD_ITEM ='" + D.GetString(dr_flex["CD_ITEM"]) + "'");
                        if (DR_sel_exch.Length > 0)
                        {
                            if (D.GetString(dr_um["MAXMIN"]) == "MAX")
                            {
                                dr_flex["UM"] = D.GetDecimal(DR_sel_exch[0]["UM_ITEM_MAX"]); //MAX단가
                                dr_flex["UM_EX"] = UDecimal.Getdivision(D.GetDecimal(DR_sel_exch[0]["UM_ITEM_MAX"]), D.GetDecimal(tb_NM_EXCH.Text)); //MAX단가
                                dr_flex["UM_EX_PO"] = D.GetDecimal(dr_flex["UM_EX"]) * D.GetDecimal(dr_flex["RT_PO"]); //MAX단가

                            }
                            else
                            {

                                dr_flex["UM"] = D.GetDecimal(DR_sel_exch[0]["UM_ITEM_MIN"]); //MAX단가
                                dr_flex["UM_EX"] = UDecimal.Getdivision(D.GetDecimal(DR_sel_exch[0]["UM_ITEM_MIN"]), D.GetDecimal(tb_NM_EXCH.Text));  //MAX단가
                                dr_flex["UM_EX_PO"] = D.GetDecimal(dr_flex["UM_EX"]) * D.GetDecimal(dr_flex["RT_PO"]); //MAX단가

                            }
                        }
                        if (D.GetDecimal(dr_flex["UM_EX"]) != 0)
                            break;

                    }
                    else if (D.GetString(dr_um["CODE"]) == "IVL")//최근매입단가
                    {

                        dt_item_um = ds_um.Tables[1];

                        DR_sel_exch = dt_item_um.Select("CD_ITEM ='" + D.GetString(dr_flex["CD_ITEM"]) + "'");
                        if (DR_sel_exch.Length > 0)
                        {
                            if (D.GetString(dr_um["MAXMIN"]) == "MAX")
                            {
                                dr_flex["UM"] = D.GetDecimal(DR_sel_exch[0]["UM_ITEM_MAX"]) * D.GetDecimal(tb_NM_EXCH.Text); //MAX단가
                                dr_flex["UM_EX"] = D.GetDecimal(DR_sel_exch[0]["UM_ITEM_MAX"]); //MAX단가
                                dr_flex["UM_EX_PO"] = D.GetDecimal(DR_sel_exch[0]["UM_ITEM_MAX"]) * D.GetDecimal(dr_flex["RT_PO"]); //MAX단가

                            }
                            else
                            {
                                dr_flex["UM"] = D.GetDecimal(DR_sel_exch[0]["UM_ITEM_MIN"]) * D.GetDecimal(tb_NM_EXCH.Text); //MAX단가
                                dr_flex["UM_EX"] = D.GetDecimal(DR_sel_exch[0]["UM_ITEM_MIN"]); //MAX단가
                                dr_flex["UM_EX_PO"] = D.GetDecimal(DR_sel_exch[0]["UM_ITEM_MIN"]) * D.GetDecimal(dr_flex["RT_PO"]); //MAX단가
                            }
                        }

                        if (D.GetDecimal(dr_flex["UM_EX"]) != 0)
                            break;
                    }
                    else if (D.GetString(dr_um["CODE"]) == "APRT")//구매거래처별단가
                    {

                        dt_item_um = ds_um.Tables[2];

                        DR_sel_exch = dt_item_um.Select("CD_ITEM ='" + D.GetString(dr_flex["CD_ITEM"]) + "'");
                        if (DR_sel_exch.Length > 0)
                        {
                            if (D.GetString(dr_um["MAXMIN"]) == "MAX")
                            {
                                dr_flex["UM"] = D.GetDecimal(DR_sel_exch[0]["UM_ITEM_MAX"]) * D.GetDecimal(tb_NM_EXCH.Text); //MAX단가
                                dr_flex["UM_EX"] = D.GetDecimal(DR_sel_exch[0]["UM_ITEM_MAX"]); //MAX단가
                                dr_flex["UM_EX_PO"] = D.GetDecimal(DR_sel_exch[0]["UM_ITEM_MAX"]) * D.GetDecimal(dr_flex["RT_PO"]); //MAX단가


                            }
                            else
                            {
                                dr_flex["UM"] = D.GetDecimal(DR_sel_exch[0]["UM_ITEM_MIN"]) * D.GetDecimal(tb_NM_EXCH.Text); //MAX단가
                                dr_flex["UM_EX"] = D.GetDecimal(DR_sel_exch[0]["UM_ITEM_MIN"]); //MAX단가
                                dr_flex["UM_EX_PO"] = D.GetDecimal(DR_sel_exch[0]["UM_ITEM_MIN"]) * D.GetDecimal(dr_flex["RT_PO"]); //MAX단가
                            }

                        }
                        if (D.GetDecimal(dr_flex["UM_EX"]) != 0)
                            break;
                    }
                    else if (D.GetString(dr_um["CODE"]) == "POL")//최근발주단가
                    {

                        dt_item_um = ds_um.Tables[3];

                        DR_sel_exch = dt_item_um.Select("CD_ITEM ='" + D.GetString(dr_flex["CD_ITEM"]) + "'");
                        if (DR_sel_exch.Length > 0)
                        {
                            if (D.GetString(dr_um["MAXMIN"]) == "MAX")
                            {
                                dr_flex["UM"] = D.GetDecimal(DR_sel_exch[0]["UM_ITEM_MAX"]) * D.GetDecimal(tb_NM_EXCH.Text); //MAX단가
                                dr_flex["UM_EX"] = D.GetDecimal(DR_sel_exch[0]["UM_ITEM_MAX"]); //MAX단가
                                dr_flex["UM_EX_PO"] = D.GetDecimal(DR_sel_exch[0]["UM_ITEM_MAX"]) * D.GetDecimal(dr_flex["RT_PO"]); //MAX단가

                            }
                            else
                            {
                                dr_flex["UM"] = D.GetDecimal(DR_sel_exch[0]["UM_ITEM_MIN"]) * D.GetDecimal(tb_NM_EXCH.Text); //MAX단가
                                dr_flex["UM_EX"] = D.GetDecimal(DR_sel_exch[0]["UM_ITEM_MIN"]); //MAX단가
                                dr_flex["UM_EX_PO"] = D.GetDecimal(DR_sel_exch[0]["UM_ITEM_MIN"]) * D.GetDecimal(dr_flex["RT_PO"]); //MAX단가
                            }
                        }

                        if (D.GetDecimal(dr_flex["UM_EX"]) != 0)
                            break;
                    }


                }

                금액계산(row_int, D.GetDecimal(dr_flex["UM_EX_PO"]), D.GetDecimal(dr_flex["QT_PO_MM"]), "UM_EX_PO", D.GetDecimal(dr_flex["UM_EX_PO"]));
                row_int++; //그리드를 제일 위부터 순서대로 거치면서 오기때문에 상관없을듯함...이상하면...다른방법을
            }


        }

        catch (Exception ex)
        {
            MsgEnd(ex);
        }



    }
    #endregion

    #endregion

    #region -> 프로젝트(WBS/CBS) 적용
    private void btn_wbscbs_Click(object sender, EventArgs e)
    {
        if (!HeaderCheck(0)) return;     //필수항목 검사

        DataTable dt = P_OPEN_SUBWINDOWS.P_PJT_WBS_CBS_SUB_LOAD(new object[] { "" ,"PU_POL"});

        if (dt == null)
            return;

        bool YN_Col = dt.Columns.Contains("NM_GRP_MFG");

        string CD_EXCH = string.Empty;
        decimal RT_EXCH = 1;

        CD_EXCH = D.GetString(dt.Rows[0]["CD_EXCH"]);
        RT_EXCH = D.GetDecimal(dt.Rows[0]["RT_EXCH"]);



        if (Global.MainFrame.ServerKeyCommon == "UNIPOINT")
            if (!Tppo_Accept(dt, "전용")) return;

        try
        {
            ControlButtonEnabledDisable((Control)sender, true);
            cbo_CD_PLANT.Enabled = false;

            decimal MaxSeq = _flexD.GetMaxValue("NO_LINE");

            foreach (DataRow dr in dt.Rows)
            {
                MaxSeq++;
                _flexD.Rows.Add();
                _flexD.Row = _flexD.Rows.Count - 1;

                _flexD["CD_PJT"] = dr["NO_PROJECT"];
                _flexD["NM_PJT"] = dr["NM_PROJECT"];
                _flexD["SEQ_PROJECT"] = dr["SEQ_PROJECT"];

                _flexD["CD_ITEM"] = dr["CD_MATL"];
                _flexD["NM_ITEM"] = dr["NM_MATL"];
                _flexD["STND_ITEM"] = dr["STND_ITEM"];
                _flexD["STND_DETAIL_ITEM"] = dr["STND_DETAIL_ITEM"];
                _flexD["UNIT_IM"] = dr["UNIT_IM"];

                if(YN_Col)
                    _flexD["NM_GRPMFG"] = dr["NM_GRP_MFG"];

                _flexD["CD_PJT_ITEM"] = dr["CD_ITEM"];
                _flexD["NM_PJT_ITEM"] = dr["NM_ITEM"];
                _flexD["PJT_ITEM_STND"] = dr["STND_ITEM_ITEM"];
                _flexD["NO_WBS"] = dr["NO_WBS"];
                _flexD["NO_CBS"] = dr["NO_CBS"];
                _flexD["CD_ACTIVITY"] = dr["CD_ACTIVITY"];
                _flexD["NM_ACTIVITY"] = dr["NM_ACTIVITY"];
                _flexD["CD_COST"] = dr["CD_COST"];
                _flexD["NM_COST"] = dr["NM_COST"];

                _flexD["DT_LIMIT"] = tb_DT_LIMIT.Text;

                _flexD["UNIT_PO"] = dr["UNIT_PO"];
                _flexD["CD_UNIT_MM"] = dr["UNIT_PO"];

                _flexD["STND_MA_ITEM"] = dr["STND_ITEM_ITEM"];                    
                _flexD["DT_PLAN"] = _flexD["DT_LIMIT"];

                _flexD["FG_POST"] = "O"; //OPEN
                //_flexD["NM_SYSDEF"] = _ComfirmState;                        
                _flexD["NM_SYSDEF"] = _biz.GetGubunCodeSearch("PU_C000009", _flexD["FG_POST"].ToString());//_ComfirmState;

                _flexD["CD_PJT"] = dr["NO_PROJECT"];
                _flexD["NM_PJT"] = dr["NM_PROJECT"];
                _flexD["SEQ_PROJECT"] = dr["SEQ_PROJECT"];

                _flexD["RT_PO"] = dr["UNIT_PO_FACT_ITEM"];

                _flexD["CD_PLANT"] = D.GetString(cbo_CD_PLANT.SelectedValue);
                if (tb_NO_PO.Text != string.Empty)
                    _flexD["NO_PO"] = tb_NO_PO.Text;
                _flexD["NO_LINE"] = MaxSeq;
                _flexD["CD_EXCH"] = D.GetString(cbo_NM_EXCH.SelectedValue);

                if (D.GetString(dr["CD_CC"]) == string.Empty)
                    SetCC(_flexD.Row, string.Empty);// C/C 적용 받아오는 부분 프로젝트 C/C를 사용하지않으므로 C/C 재설정, 프로젝트에 CD_CC가존재하면 프로젝트 CC를 사용한다고 가정한다.
                else
                {
                    _flexD["CD_CC"] = dr["CD_CC"];
                    _flexD["NM_CC"] = dr["NM_CC"];
                }

                _flexD["DC1"] = D.GetString(dr["DC_REMARK"]);
                _flexD["NO_LINE_PJTBOM"] = dr["NO_LINE_PJTBOM"];

                DataTable dt_exch = (DataTable)cbo_NM_EXCH.DataSource;
                foreach (DataRow dr_ex in dt_exch.Rows)
                {
                    if (dr_ex["CODE"].ToString() == D.GetString(cbo_NM_EXCH.SelectedValue))
                    {
                        _flexD["NM_EXCH"] = dr_ex["NAME"];
                        break;
                    }
                }

                if (_flexD.CDecimal(_flexD["RT_PO"]) == 0)
                    _flexD["RT_PO"] = 1;

                _flexD["QT_PO_MM"] = dr["QT_NEED_JAN"];
                _flexD["QT_PO"] = (D.GetDecimal(_flexD["QT_PO_MM"]) * D.GetDecimal(_flexD["RT_PO"]));

                _flexD["NO_PR"] = "";
                _flexD["NO_PRLINE"] = 0;

                _flexD["FG_TAX"] = _header.CurrentRow["FG_TAX"];
                _flexD["RATE_VAT"] = tb_TAX.DecimalValue;

                ctx공급자.CodeValue = D.GetString(dr["CD_PARTNER"]);
                ctx공급자.CodeName = D.GetString(dr["NM_PARTNER"]);

                if (Global.MainFrame.ServerKeyCommon.ToUpper() == "ICDERPU")
                    _flexD["CD_USERDEF1"] = dr["NO_ADN_SEQ"];

                string strYNmu = BASIC.GetMAEXC_Menu("P_PU_PO_REG2", "PU_A00000020");

                FillPol(_flexD.Row);
                if (Global.MainFrame.ServerKeyCommon.ToUpper() != "KORAVL" && Global.MainFrame.ServerKeyCommon.ToUpper() != "UCLICK" && strYNmu == "000")
                {
                    object[] m_obj = new object[11];
                    m_obj[0] = _flexD["CD_ITEM"].ToString();
                    m_obj[1] = _flexD["CD_PLANT"].ToString();
                    m_obj[2] = LoginInfo.CompanyCode;
                    m_obj[3] = cbo_FG_UM.SelectedValue.ToString();
                    m_obj[4] = cbo_NM_EXCH.SelectedValue.ToString();
                    m_obj[5] = tb_DT_PO.Text;
                    m_obj[6] = tb_NM_PARTNER.CodeValue;
                    m_obj[7] = tb_NM_PURGRP.CodeValue;
                    m_obj[8] = "N";
                    m_obj[9] = D.GetString(_flexD["CD_PJT"]);
                    m_obj[10] = Global.MainFrame.ServerKeyCommon.ToUpper();


                    품목정보구하기(m_obj, "요청", 0);
                }
                else //한국AVL 프로젝트에있는 단가를 환정보를 사용한다. 라인별로 환종, 환율이 다를경우 TOP1인것으로 사용 함.
                {
                    _flexD["UM_EX_PO"] = D.GetDecimal(dr["UM_EX"]) * D.GetDecimal(_flexD["RT_PO"]);
                    _flexD["CD_EXCH"] = D.GetString(CD_EXCH);
                    _flexD["QT_PO_MM"] = D.GetDecimal(dr["QT_NEED_JAN"]);
                    _flexD["QT_PO"] = (D.GetDecimal(_flexD["QT_PO_MM"]) * D.GetDecimal(_flexD["RT_PO"]));

                    if (D.GetString(CD_EXCH) != "" && D.GetDecimal(RT_EXCH) != 0)
                    {
                        cbo_NM_EXCH.SelectedValue = D.GetString(CD_EXCH);
                        tb_NM_EXCH.Text = D.GetString(RT_EXCH);
                        _header.CurrentRow["CD_EXCH"] = D.GetString(CD_EXCH);
                        _header.CurrentRow["RT_EXCH"] = D.GetDecimal(RT_EXCH);
                    }

                }
                부가세계산(_flexD.GetDataRow(_flexD.Row));


                _flexD.AddFinished();
                _flexD.Col = _flexD.Cols.Fixed;
            }

            SUMFunction();
            SetHeadControlEnabled(false, 4);

        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
    }
    #endregion

    #region -> BOM적용
    private void btnBOM_Click(object sender, EventArgs e)
    {
        try
        {
            if (!HeaderCheck(0)) return; //필수항목 검사 

            if (!btn_insert.Enabled)
                return;

            P_PU_GIREQ_BOM_SUB m_dlg = new P_PU_GIREQ_BOM_SUB(D.GetString(cbo_CD_PLANT.SelectedValue), tb_DT_PO.Text); // 요청일자가 필요함, 화면구분(PR:구매요청, APP:구매품의, PO:구매발주)//입고공장을 바라보게 되어있었는데 김헌섭대리와 이야기후 요청공장으로 바라보게 바꾸었음

            if (m_dlg.ShowDialog(this) == DialogResult.OK)
            {
                DataTable dt = m_dlg.dt_return;

                if (dt == null && dt.Rows.Count < 1)
                    return;
                //여러 생산품을 적용할 경우 공통자재에 대해서는 합산된 수량으로 적용이 될 수 있게 적용 D20121016027 
                DataTable dtGroupby = dt.DefaultView.ToTable(true, new string[] {"CD_MATL","NM_ITEM_MATL","STND_ITEM_MATL","STND_DETAIL_ITEM_MATL","UNIT_IM_MATL",
                                                                                 "UNIT_PO","UNIT_PO_FACT","NM_GRPMFG"});

                foreach (DataRow dr in dtGroupby.Rows)
                {

                    추가_Click(null, null);

                    _flexD["CD_ITEM"] = dr["CD_MATL"];
                    _flexD["NM_ITEM"] = dr["NM_ITEM_MATL"];
                    _flexD["STND_ITEM"] = dr["STND_ITEM_MATL"];
                    _flexD["STND_DETAIL_ITEM"] = dr["STND_DETAIL_ITEM_MATL"];
                    _flexD["UNIT_IM"] = dr["UNIT_IM_MATL"];

                    _flexD["UNIT_PO"] = dr["UNIT_PO"];
                    _flexD["CD_UNIT_MM"] = dr["UNIT_PO"];

                    _flexD["FG_POST"] = "O"; //OPEN                   
                    _flexD["NM_SYSDEF"] = _biz.GetGubunCodeSearch("PU_C000009", _flexD["FG_POST"].ToString());//_ComfirmState;

                    _flexD["RT_PO"] = dr["UNIT_PO_FACT"];

                    _flexD["CD_PLANT"] = D.GetString(cbo_CD_PLANT.SelectedValue);
                    _flexD["CD_EXCH"] = D.GetString(cbo_NM_EXCH.SelectedValue);

                    SetCC(_flexD.Row, string.Empty);// C/C 적용 받아오는 부분 수주 C/C를 사용하지않으므로 C/C 재설정


                    DataTable dt_exch = (DataTable)cbo_NM_EXCH.DataSource;
                    foreach (DataRow dr_ex in dt_exch.Rows)
                    {
                        if (dr_ex["CODE"].ToString() == D.GetString(cbo_NM_EXCH.SelectedValue))
                        {
                            _flexD["NM_EXCH"] = dr_ex["NAME"];
                            break;
                        }
                    }

                    if (_flexD.CDecimal(_flexD["RT_PO"]) == 0)
                        _flexD["RT_PO"] = 1;

                    //_flexD["QT_PO"] = dr["QT_ITEM_NET"];
                    _flexD["QT_PO"] = D.GetDecimal(dt.Compute("SUM(QT_ITEM_NET)", "CD_MATL ='" + D.GetString(dr["CD_MATL"]) + "'"));
                    _flexD["QT_PO_MM"] = D.GetDecimal(_flexD["QT_PO"]) / D.GetDecimal(_flexD["RT_PO"]);

                    FillPol(_flexD.Row);
                    object[] m_obj = new object[11];
                    m_obj[0] = _flexD["CD_ITEM"].ToString();
                    m_obj[1] = _flexD["CD_PLANT"].ToString();
                    m_obj[2] = LoginInfo.CompanyCode;
                    m_obj[3] = cbo_FG_UM.SelectedValue.ToString();
                    m_obj[4] = cbo_NM_EXCH.SelectedValue.ToString();
                    m_obj[5] = tb_DT_PO.Text;
                    m_obj[6] = tb_NM_PARTNER.CodeValue;
                    m_obj[7] = tb_NM_PURGRP.CodeValue;
                    m_obj[8] = "N";
                    m_obj[9] = D.GetString(_flexD["CD_PJT"]);
                    m_obj[10] = Global.MainFrame.ServerKeyCommon.ToUpper();


                    품목정보구하기(m_obj, "BOM", 0);
                    부가세계산(_flexD.GetDataRow(_flexD.Row));

                    _flexD["DT_PLAN"] = _flexD["DT_LIMIT"];
                    _flexD["NM_GRPMFG"] = dr["NM_GRPMFG"];
                }

                SetHeadControlEnabled(false, 1);

            }
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
    }
    #endregion

    #region -> 수주/의뢰적용 브이피에이치(평화발레오) 전용
    private void btn_so_gir_Click(object sender, EventArgs e)
    {
        try
        {
            if (!HeaderCheck(0)) return;

            호출여부 = true;


            object[] obj_so = new object[]
            {
                cbo_CD_PLANT.SelectedValue,
                cbo_CD_PLANT.Text,
                Global.MainFrame.CurrentPageID,
                _flexD.DataTable,
                _header.CurrentRow["FG_TRANS"],
                _header.CurrentRow["NM_TRANS"]
            };
            pur.P_PU_PO_SO_GIR_SUB m_dlg = new pur.P_PU_PO_SO_GIR_SUB(obj_so);
            Cursor.Current = Cursors.Default;

            if (m_dlg.ShowDialog(this) == DialogResult.OK)
            {
                DataTable ldt_dlg = m_dlg.수주데이터;

                if (ldt_dlg == null || ldt_dlg.Rows.Count <= 0) return;

                //if (!Partner_Accept(ldt_dlg)) return;

                ControlButtonEnabledDisable((Control)sender, true);
                cbo_CD_PLANT.Enabled = false;


                _flexD.Redraw = false;


                decimal MaxSeq = _flexD.GetMaxValue("NO_LINE");

                for (int i = 0; i < ldt_dlg.Rows.Count; i++)
                {
                    if (ldt_dlg.Rows[i].RowState == DataRowState.Deleted)
                        continue;

                    MaxSeq++;
                    _flexD.Rows.Add();
                    _flexD.Row = _flexD.Rows.Count - 1;
                    _flexD["CD_ITEM"] = ldt_dlg.Rows[i]["CD_ITEM"];
                    _flexD["NM_ITEM"] = ldt_dlg.Rows[i]["NM_ITEM"];
                    _flexD["STND_ITEM"] = ldt_dlg.Rows[i]["STND_ITEM"];
                    _flexD["CD_UNIT_MM"] = ldt_dlg.Rows[i]["UNIT_PO"];
                    _flexD["UNIT_PO"] = ldt_dlg.Rows[i]["UNIT_PO"];
                    _flexD["STND_MA_ITEM"] = ldt_dlg.Rows[i]["STND_ITEM"];
                    _flexD["UNIT_IM"] = ldt_dlg.Rows[i]["UNIT_IM"];
                    //2010.01.12 납기일
                    if (tb_DT_LIMIT.Text == string.Empty)
                        _flexD["DT_LIMIT"] = ldt_dlg.Rows[i]["DT_DUEDATE"];
                    else
                        _flexD["DT_LIMIT"] = tb_DT_LIMIT.Text;

                    _flexD["DT_PLAN"] = _flexD["DT_LIMIT"];

                    _flexD["FG_POST"] = "O"; //OPEN
                    //_flexD["NM_SYSDEF"] = _ComfirmState;                        
                    _flexD["NM_SYSDEF"] = _biz.GetGubunCodeSearch("PU_C000009", _flexD["FG_POST"].ToString());//_ComfirmState;


                    _flexD["RT_PO"] = D.GetDecimal(ldt_dlg.Rows[i]["RT_PO"]);
                    _flexD["CD_PJT"] = ldt_dlg.Rows[i]["CD_PJT"];
                    _flexD["NM_PJT"] = ldt_dlg.Rows[i]["NM_PJT"];
                    _flexD["NO_PR"] = "";
                    _flexD["NO_PRLINE"] = 0;
                    //_flexD["NO_CONTRACT"] = ldt_dlg.Rows[i]["NO_CONTRACT"];
                    //_flexD["NO_CTLINE"] = ldt_dlg.Rows[i]["NO_CTLINE"];
                    if (m_dlg.tab_no == "0")
                    {
                        _flexD["NO_SO"] = ldt_dlg.Rows[i]["NO_SO"];   // 수주번호
                        _flexD["NO_SOLINE"] = ldt_dlg.Rows[i]["SEQ_SO"];  // 수주항번
                    }

                    _flexD["CD_PLANT"] = ldt_dlg.Rows[i]["CD_PLANT"];
                    if (tb_NO_PO.Text != string.Empty)
                        _flexD["NO_PO"] = tb_NO_PO.Text;
                    _flexD["NO_RCV"] = "";
                    _flexD["NO_LINE"] = MaxSeq;
                    _flexD["CD_SL"] = ldt_dlg.Rows[i]["CD_SL"];
                    _flexD["NM_SL"] = ldt_dlg.Rows[i]["NM_SL"];
                    _flexD["CD_EXCH"] = cbo_NM_EXCH.SelectedValue.ToString();
                    
                    if (Global.MainFrame.ServerKeyCommon.ToUpper() == "WINPLUS")
                    {
                        _flexD["DC1"] = ldt_dlg.Rows[i]["DC1"];
                        _flexD["DC2"] = ldt_dlg.Rows[i]["DC2"];
                    }
                    else
                    {
                        _flexD["DC1"] = ldt_dlg.Rows[i]["CD_ITEM_PARTNER"];
                        _flexD["DC2"] = ldt_dlg.Rows[i]["NM_ITEM_PARTNER"];
                    }
                    _flexD["NM_GRPMFG"] = ldt_dlg.Rows[i]["NM_GRPMFG"];


                    _flexD["NUM_USERDEF1"] = ldt_dlg.Rows[i]["NUM_USERDEF1"];
                    _flexD["NUM_USERDEF2"] = ldt_dlg.Rows[i]["NUM_USERDEF2"];


                    ////구매예산통제관련 추가 20091201
                    //_flexD["CD_BUDGET"] = ldt_dlg.Rows[i]["CD_BUDGET"]; //예산단위코드
                    //_flexD["NM_BUDGET"] = ldt_dlg.Rows[i]["NM_BUDGET"];
                    //_flexD["CD_BGACCT"] = ldt_dlg.Rows[i]["CD_BGACCT"]; //예산계정코드
                    //_flexD["NM_BGACCT"] = ldt_dlg.Rows[i]["NM_BGACCT"];

                    // 2009.12.08 다시 개발 cc설정 관련
                    // LINE 수정 권한이면 요청에서 적용받는다 
                    // 요청에 없으면 HEADER 설정에 따라 적용받는다.
                    // LINE 수정권한이 없으면 HEADER 설정에 따라 적용받는다.

                    //SetCC_Line(_flexD.Row, ldt_dlg.Rows[i]["CD_PURGRP"].ToString());
                    SetCC(_flexD.Row, string.Empty);// C/C 적용 받아오는 부분 수주 C/C를 사용하지않으므로 C/C 재설정


                    DataTable dt = (DataTable)cbo_NM_EXCH.DataSource;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["CODE"].ToString() == cbo_NM_EXCH.SelectedValue.ToString())
                        {
                            _flexD["NM_EXCH"] = dr["NAME"];
                            break;
                        }
                    }
                    //if (ldt_dlg.Rows[i]["UNIT_PO"].ToString() != String.Empty)
                    //{

                    if (_flexD.CDecimal(_flexD["RT_PO"]) == 0)
                        _flexD["RT_PO"] = 1;

                    _flexD["QT_PO_MM"] = D.GetDecimal(ldt_dlg.Rows[i]["QT_REM"]);
                    _flexD["QT_PO"] = (D.GetDecimal(_flexD["QT_PO_MM"]) * D.GetDecimal(_flexD["RT_PO"]));
                    //}
                    //else
                    //{
                    //    _flexD["QT_PO_MM"] = _flexD.CDecimal(_flexD["QT_PO"]);
                    //}



                    FillPol(_flexD.Row);
                    object[] m_obj = new object[11];
                    m_obj[0] = _flexD["CD_ITEM"].ToString();
                    m_obj[1] = _flexD["CD_PLANT"].ToString();
                    m_obj[2] = LoginInfo.CompanyCode;
                    m_obj[3] = cbo_FG_UM.SelectedValue.ToString();
                    m_obj[4] = cbo_NM_EXCH.SelectedValue.ToString();
                    m_obj[5] = tb_DT_PO.Text;
                    m_obj[6] = tb_NM_PARTNER.CodeValue;
                    m_obj[7] = tb_NM_PURGRP.CodeValue;
                    m_obj[8] = "N";
                    m_obj[9] = D.GetString(_flexD["CD_PJT"]);
                    m_obj[10] = Global.MainFrame.ServerKeyCommon.ToUpper();


                    품목정보구하기(m_obj, "요청", 0);
                    부가세계산(_flexD.GetDataRow(_flexD.Row));


                    _flexD.AddFinished();
                    _flexD.Col = _flexD.Cols.Fixed;
                }

                SUMFunction();
                SetHeadControlEnabled(false, 1);
            }
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
        finally
        {
            _flexD.Redraw = true;
        }

    }
    #endregion

    #region -> 부가정보적용

    private void btn_subinfo_Click(object sender, EventArgs e)
    {
        try
        {

       

            _infosub_dlg = new P_PU_OPTION_INFO_SUB(D.GetString(_header.CurrentRow["NO_PO"]), D.GetString(테이블구분.PU_POH.GetHashCode()), false); //키값, 테이블 구분값, readonly값

            if (_infosub_dlg.ShowDialog() == DialogResult.OK)
            {
                ToolBarSaveButtonEnabled = true;
                _header.CurrentRow["NM_PACKING"] = txt_nm_packing.Text; //헤더가 변경된것처럼하여서 저장이되도록한다. 발주등록은 헤더나 라인이 바뀌지않으면 부가정보만 저장하도록하기가 너무 어렵다 (부가적인 로직이 너무많다) 발주등록에만 쓴 편법이므로 다른화면에서는이렇게 쓰면안됨

                //SetButtonState(false, 5);
            }
            return;
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
    }

    #endregion

    #region -> 첨부파일버튼 클릭
    private void btn_FILE_UPLOAD_Click(object sender, EventArgs e)
    {
        try
        {
            if(tb_NO_PO.Text != "")
            {
                string cd_file_code = D.GetString(_flexD["NO_PO"] + "_" + Global.MainFrame.LoginInfo.CompanyCode); //파일 PK설정   
                master.P_MA_FILE_SUB m_dlg = new master.P_MA_FILE_SUB("PU", Global.MainFrame.CurrentPageID, cd_file_code);

                m_dlg.ShowDialog();
            }
            else return;

        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
    }
    #endregion

    #region 창고적용

    void btn_SL_apply_Click(object sender, EventArgs e)
    {
        try
        {
            if (bp_CDSL.CodeValue == string.Empty) return;

            foreach (DataRow dr in _flexD.DataTable.Rows)
            {
                dr["CD_SL"] = bp_CDSL.CodeValue;
                dr["NM_SL"] = bp_CDSL.CodeName;

                object[] pi_obj = new object[] { Global.MainFrame.LoginInfo.CompanyCode, tb_DT_PO.Text.Substring(0, 4), D.GetString(cbo_CD_PLANT.SelectedValue), D.GetString(dr["CD_ITEM"]), D.GetString(dr["CD_SL"]) };
                DataTable dt_pinv = _biz.item_pinvn(pi_obj);

                if (dt_pinv != null && dt_pinv.Rows.Count > 0)
                {
                    dr["QT_INVC"] = dt_pinv.Rows[0]["QT_INVC"]; //현재고
                    dr["QT_ATPC"] = dt_pinv.Rows[0]["QT_ATPC"]; //가용재고
                }

            }

            SetQtValue(_flexD.Row);

            ShowMessage(DD("적용작업을완료하였습니다"));
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
    }

    #endregion

    #region -> SMS전송 클릭
    void btnSMS_Click(object sender, EventArgs e)
    {
        try
        {

            if (!_flexD.HasNormalRow) return;

            // dt_sms에는 기준이되는 키값(예: NO_PO) 이 존재해야하고 CD_PARTNER는 꼭 존재하여야 한다. 그리고 NO_PO는 키값이되는 컬럼의 코드이다.  그리고 담당 사원번호를 넣어주면된다
            P_MF_SMS SMS = new P_MF_SMS(_header.CurrentRow.Table, "NO_PO", LoginInfo.EmployeeNo);

            if (SMS.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                return;
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
    }
    #endregion

    #endregion

    #region ♣ 이벤트관련

    #region -> 그리드 이벤트

    #region -> 그리드 행변경 이벤트(Grid_AfterRowChange)

    private void Grid_AfterRowChange(object sender, RangeEventArgs e)
    {
        try
        {
            GetQtValue(_flexD.Row);

            if (sPUSU == "100")
            {
                DataTable dt = new DataTable();
                string filter = "NO_POLINE = " + _flexD["NO_LINE"].ToString() + "";
                if (_flexD.DetailQueryNeed)
                    dt = _biz.SearchDetail(_header.CurrentRow["CD_PLANT"].ToString(), _flexD["NO_PO"].ToString(), Convert.ToDecimal(_flexD["NO_LINE"]));

                _flexDD.BindingAdd(dt, filter);
            }
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
    }

    #endregion

    #region -> 그리드 변경 시작시 체크 이벤트(Grid_StartEdit)

    private void Grid_StartEdit(object sender, RowColEventArgs e)
  
{
        try
        {
            if (_flexD["FG_POST"].ToString().Trim() != "O" && _flexD.RowState() != DataRowState.Added)
                e.Cancel = true;

            if (!차수여부)
                e.Cancel = true;


            FlexGrid _flex = sender as FlexGrid;

            if (_flex.Name == _flexD.Name)
            {
                if (m_sEnv.Equals("N"))
                {
                    if (_flexD.Cols[e.Col].Name == "QT_PO")
                        e.Cancel = true;	// 셀 입력상태로 못 들어가게
                }


                //부가세여부 포함 금액(AM_EX) 원화금액(AM), 부가세(VAT) EDIT 불가 
                if (D.GetString(_flexD["TP_UM_TAX"]) == "001")
                {
                    if (_flexD.Cols[e.Col].Name == "AM" || _flexD.Cols[e.Col].Name == "AM_EX" || _flexD.Cols[e.Col].Name == "VAT")
                        e.Cancel = true;

                }
                else
                {
                    if (_flexD.Cols[e.Col].Name == "AM_TOTAL")
                        e.Cancel = true;

                }


                //구매단가통제여부: 구매그룹의 소속조직에 설정된 값에따라 단가/금액 입력통제
                if (_flexD.Cols[e.Col].Name == "UM_EX_PO" || _flexD.Cols[e.Col].Name == "AM_EX" || _flexD.Cols[e.Col].Name == "AM")
                {
                    if (_header.CurrentRow["PO_PRICE"].ToString() == "Y")
                    {
                        ShowMessage("구매 단가 통제된 구매그룹 입니다.");	// 셀 입력상태로 못 들어가게
                        e.Cancel = true;
                    }
                }

               

                switch (_flexD.Cols[e.Col].Name)
                {
                    case "AM":  // 환종이 원화수정 : 김헌섭요청 
                        if (D.GetString(_header.CurrentRow["CD_EXCH"]) == "000" || _m_tppo_change == "001")
                            e.Cancel = true;
                        break;

                    case "CD_USERDEF1":
                        if (Global.MainFrame.ServerKeyCommon.ToUpper() == "ICDERPU") e.Cancel = true;
                        break;
                    case "CD_ITEM":
                        if (_flexD.DataTable.Columns.Contains("APP_PJT") && D.GetString(_flexD["APP_PJT"]) == "Y") 
                            e.Cancel = true;
                        break;
                    case "QT_PO_MM":
                    case "UM_EX_PO":
                    case "AM_EX":
                    case "AM_TOTAL":
                    case "QT_PO":
                        if (_m_tppo_change == "001" && D.GetString(_flex["NO_APP"]) != string.Empty)
                            e.Cancel = true;
                        break;
                    case "FG_TAX":
                        if(m_tab_poh.TabPages.Contains(tabPage7))
                            e.Cancel = true;
                        break;
                    case "TP_UM_TAX":
                        if (의제매입여부(D.GetString(_flexD["FG_TAX"])) && s_vat_fictitious == "100")
                            e.Cancel = true;
                        break;
                    case "CD_PJT":
                        if (Global.MainFrame.ServerKeyCommon == "DEMAC" && D.GetString(_flexD["NO_PR"]) != "")
                            e.Cancel = true;
                        break;
                        
                }


            }
            //20110329 최인성 김성호 김헌섭 출고수량이 있는 경우 EDIT 불가하도록 함.
            else if (_flex.Name == _flexDD.Name)
            {
                if (D.GetDecimal(_flex["QT_REQ"]) > 0)
                {
                    e.Cancel = true;
                    return;
                }
            }

            if (bStandard)
            {
                if ((D.GetDecimal(_flex["UM_WEIGHT"])) > 0)
                {
                    if ((_flex.Cols[e.Col].Name == "UM_EX_PO") || (_flex.Cols[e.Col].Name == "UM_EX") || (_flex.Cols[e.Col].Name == "AM_EX") || (_flex.Cols[e.Col].Name == "AM"))
                    {
                        e.Cancel = true;
                        return;
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

    #region -> 그리드 도움창 호출전 세팅 이벤트(Grid_BeforeCodeHelp)

    private void Grid_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
    {
        try
        {
            FlexGrid _flex = sender as FlexGrid;
            if (_flex == null) return;

            if (_flex.Name == _flexD.Name)
            {
                switch (_flexD.Cols[e.Col].Name)
                {
                    case "CD_ITEM":
                        if (D.GetString(_flexD["FG_POST"]) != "O" || (_m_tppo_change == "001" && D.GetString(_flex["NO_APP"]) != string.Empty)) // 조회도움창에서 복사로 가져온 데이터는 품목을 고칠수있게 요청(김범영대리의 요청)
                            e.Cancel = true;
                        else if (_flexD.DataTable.Columns.Contains("APP_PJT") && D.GetString(_flexD["APP_PJT"]) == "Y")
                            e.Cancel = true;
                        else
                        {
                            e.Parameter.P01_CD_COMPANY = LoginInfo.CompanyCode;
                            e.Parameter.P09_CD_PLANT = _flexD["CD_PLANT"].ToString();
                        }
                        break;
                    case "CD_PURGRP":
                        e.Parameter.P92_DETAIL_SEARCH_CODE = e.EditValue;
                        break;
                    case "CD_SL":
                    case "CDSL_USERDEF1":
                        e.Parameter.P01_CD_COMPANY = LoginInfo.CompanyCode;
                        e.Parameter.P09_CD_PLANT = _flexD["CD_PLANT"].ToString();
                        break;
                    case "GI_PARTNER":
                        e.Parameter.P14_CD_PARTNER = tb_NM_PARTNER.CodeValue;
                        break;
                    //case "CD_BUDGET":
                    //    e.Parameter.P01_CD_COMPANY = LoginInfo.CompanyCode;
                    //    e.Parameter.P05_CD_DEPT = LoginInfo.DeptCode;
                    //    e.Parameter.P61_CODE1 = LoginInfo.DeptCode;
                    //    break;
                    //case "NO_PROJECT":
                    //    if (D.GetDecimal(_flexD["SEQ_PROJECT"]) != 0)
                    //    {
                    //        ShowMessage("프로젝트적용 받은 내용이므로 수정하실 수 없습니다.");
                    //        e.Cancel = true;
                    //        return;
                    //    }
                        //if (!Chk거래처 || !Chk영업그룹)
                        //{
                        //    e.Cancel = true;
                        //    return;
                        //}
                        //e.Parameter.P41_CD_FIELD1 = "프로젝트";             //도움창 이름 찍어줄 값            
                        //break;
                    case "NM_CLS_L":
                        e.Parameter.P41_CD_FIELD1 = "MA_B000030";
                        break;
                    case "NM_CLS_M":
                        e.Parameter.P41_CD_FIELD1 = "MA_B000031";
                        break;
                    case "NM_CLS_S":
                        e.Parameter.P41_CD_FIELD1 = "MA_B000032";
                        break;
                    case "CD_PJT":
                        if (Global.MainFrame.ServerKeyCommon == "DEMAC" && D.GetString(_flexD["NO_PR"]) != "")
                            e.Cancel = true;
                        break;
                }

                if (BASIC.GetMAEXC("공장품목-대중소분류 종속관계 설정").Equals("001"))
                {
                    switch (_flexD.Cols[e.Col].Name)
                    {


                        case "NM_CLS_M":

                            if (D.GetString(_flexD["CLS_L"]) == "")
                            {
                                ShowMessage(공통메세지._은는필수입력항목입니다, DD("대분류코드"));

                                e.Cancel = true;
                                //_flex_D.Cols["CD_BIZAREA_GI"].;
                            }
                            else
                            {
                                e.Parameter.P42_CD_FIELD2 = D.GetString(_flexD["CLS_L"]);
                            }

                            break;
                        case "NM_CLS_S":
                            if (D.GetString(_flexD["CLS_M"]) == "")
                            {
                                ShowMessage(공통메세지._은는필수입력항목입니다, DD("중분류코드"));
                                e.Cancel = true;
                            }
                            else
                            {
                                e.Parameter.P42_CD_FIELD2 = D.GetString(_flexD["CLS_M"]);
                            }

                            break;
                    }
                }
            }
            else if (_flex.Name == _flexDD.Name)
            {

                //20110329 최인성 김성호 김헌섭 출고수량이 있는 경우 EDIT 불가하도록 함.
                if (D.GetDecimal(_flex["QT_REQ"]) > 0)
                {
                    e.Cancel = true;
                    return;
                }

                e.Parameter.P09_CD_PLANT = cbo_CD_PLANT.SelectedValue.ToString();
            }
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
    }

    #endregion

    #region -> 그리드 도움창 호출후 변경 이벤트(Grid_AfterCodeHelp)

    private void Grid_AfterCodeHelp(object sender, AfterCodeHelpEventArgs e)
    {
        try
        {
            FlexGrid flex = sender as FlexGrid;
            HelpReturn helpReturn = e.Result;
            // DataTable dt = flex.DataTable;
            // DataRow drD;
            string DT_LIMIT = "";
            // bool 첫줄여부 = true;
            int apply_row = 0;

            if (flex.Name == _flexD.Name)
            {
                switch (_flexD.Cols[e.Col].Name)
                {
                    case "CD_ITEM":

                        
                        if (e.Result.DialogResult == DialogResult.Cancel)
                        {
                            _flexD[e.Row, "CD_ITEM"] = string.Empty;
                            return;
                        }

                        //decimal MaxSeq = _flexD.GetMaxValue("NO_LINE");

                        bool First = true;
                        _flexD.Redraw = false;
                        _flexD.SetDummyColumnAll();

                        if (_flexD[_flexD.Rows.Count - 1, "DT_LIMIT"].ToString() != "")
                            DT_LIMIT = _flexD[_flexD.Rows.Count - 1, "DT_LIMIT"].ToString();
                        else
                            DT_LIMIT = Global.MainFrame.GetStringToday;

                        foreach (DataRow dr in helpReturn.Rows)
                        {
                            // if (_flexD[_flexD.Rows.Count - 1, "CD_ITEM"].ToString().Equals("") && 첫줄여부)
                            if (First)
                            {
                                apply_row = e.Row;
                                _flexD[e.Row, "CD_ITEM"] = dr["CD_ITEM"];
                                _flexD[e.Row, "NM_ITEM"] = dr["NM_ITEM"];
                                _flexD[e.Row, "STND_ITEM"] = dr["STND_ITEM"];
                                _flexD[e.Row, "UNIT_IM"] = dr["UNIT_IM"];
                                _flexD[e.Row, "CD_UNIT_MM"] = dr["UNIT_PO"];
                                _flexD[e.Row, "NM_CLS_ITEM"] = dr["CLS_ITEMNM"];

                                if (tb_NO_PO.Text != string.Empty)
                                    _flexD[e.Row, "NO_PO"] = tb_NO_PO.Text;
                                //  _flexD[_flexD.Rows.Count - 1, "NO_LINE"] = 최대차후 + 1;
                                //_flexD[e.Row, "CD_PLANT"] = cbo_CD_PLANT.SelectedValue.ToString();
                                //_flexD[e.Row, "CD_PJT"] = txt_NoProject.Text;
                                //_flexD[e.Row, "NM_PJT"] = bp_Project.Text;                                
                                if (_flexD[e.Row, "DT_LIMIT"].ToString() == string.Empty)
                                    _flexD[e.Row, "DT_LIMIT"] = DT_LIMIT;

                                if (_flexD[e.Row, "DT_PLAN"].ToString() == string.Empty)
                                    _flexD["DT_PLAN"] = _flexD["DT_LIMIT"];
                                //_flexD[e.Row, "NM_SYSDEF"] = _ComfirmState; 

                                if (Global.MainFrame.ServerKeyCommon.ToUpper() == "WJIS")
                                {
                                    _flexD[e.Row, "DC1"] = dr["CD_ITEM"];
                                    _flexD[e.Row, "DC2"] = dr["NM_ITEM"];
                                }


                                
                                First = false;

                            }
                            else
                            {
                                //if (첫줄여부) _flexD.RemoveItem(e.Row);

                                //drD = dt.NewRow();

                                _flexD.Rows.Add();
                                _flexD.Row = _flexD.Rows.Count - 1;
                                _flexD["CD_ITEM"] = dr["CD_ITEM"];
                                _flexD["NM_ITEM"] = dr["NM_ITEM"];
                                _flexD["STND_ITEM"] = dr["STND_ITEM"];
                                _flexD["UNIT_IM"] = dr["UNIT_IM"];
                                _flexD["CD_UNIT_MM"] = dr["UNIT_PO"];
                                _flexD["NM_CLS_ITEM"] = dr["CLS_ITEMNM"];

                                if (tb_NO_PO.Text != string.Empty)
                                    _flexD["NO_PO"] = tb_NO_PO.Text;
                                _flexD["NO_LINE"] = 최대차수 + 1;
                                apply_row = _flexD.Row;
                                _flexD[apply_row, "CD_PLANT"] = cbo_CD_PLANT.SelectedValue.ToString();
                                _flexD[apply_row, "CD_PJT"] = ctx프로젝트.CodeValue;
                                _flexD[apply_row, "NM_PJT"] = ctx프로젝트.CodeName;
                                _flexD[apply_row, "DT_LIMIT"] = DT_LIMIT;
                                _flexD[apply_row, "DT_PLAN"] = DT_LIMIT;

                                _flexD["NM_SYSDEF"] = _biz.GetGubunCodeSearch("PU_C000009", _flexD["FG_POST"].ToString());//_ComfirmState;

                                if (Global.MainFrame.ServerKeyCommon.ToUpper() == "WJIS")
                                {
                                    _flexD[apply_row, "DC1"] = dr["CD_ITEM"];
                                    _flexD[apply_row, "DC2"] = dr["NM_ITEM"];
                                }

                                //_flexD[apply_row, "NM_SYSDEF"] = _ComfirmState;

                                // dt.Rows.Add(drD);
                                
                            }

                            _flexD[apply_row, "RT_PO"] = dr["UNIT_PO_FACT"];  // 2010.04.13 
                            _flexD[apply_row, "TP_UM_TAX"] = cbo_TP_TAX.SelectedValue.ToString();

                            //apply_row = _flexD.Row;
                            object[] m_obj = new object[11];
                            m_obj[0] = dr["CD_ITEM"];
                            m_obj[1] = cbo_CD_PLANT.SelectedValue.ToString();
                            m_obj[2] = Global.MainFrame.LoginInfo.CompanyCode;
                            m_obj[3] = cbo_FG_UM.SelectedValue.ToString();
                            m_obj[4] = cbo_NM_EXCH.SelectedValue.ToString();
                            m_obj[5] = tb_DT_PO.Text;
                            m_obj[6] = tb_NM_PARTNER.CodeValue;
                            m_obj[7] = tb_NM_PURGRP.CodeValue;
                            m_obj[8] = "N";
                            if (D.GetString(_flexD[e.Row, "CD_PJT"]) != string.Empty)
                                m_obj[9] = _flexD[e.Row, "CD_PJT"];
                            else
                                m_obj[9] = ctx프로젝트.CodeValue;
                            m_obj[10] = Global.MainFrame.ServerKeyCommon.ToUpper();



                            품목정보구하기(m_obj, "GRID", apply_row);

                            _flexD[apply_row, "FG_TRANS"] = _header.CurrentRow["FG_TRANS"];
                            _flexD[apply_row, "FG_TPPURCHASE"] = _header.CurrentRow["FG_TPPURCHASE"];
                            _flexD[apply_row, "YN_AUTORCV"] = _header.CurrentRow["YN_AUTORCV"];
                            _flexD[apply_row, "YN_RCV"] = _header.CurrentRow["YN_RCV"];
                            _flexD[apply_row, "YN_RETURN"] = _header.CurrentRow["YN_RETURN"];
                            _flexD[apply_row, "YN_IMPORT"] = _header.CurrentRow["YN_IMPORT"];
                            _flexD[apply_row, "YN_ORDER"] = _header.CurrentRow["YN_ORDER"];
                            _flexD[apply_row, "YN_REQ"] = _header.CurrentRow["YN_REQ"];
                            _flexD[apply_row, "FG_RCV"] = _header.CurrentRow["FG_TPRCV"];
                            _flexD[apply_row, "YN_SUBCON"] = _header.CurrentRow["YN_SUBCON"];
                            _flexD[apply_row, "FG_PURCHASE"] = _header.CurrentRow["FG_TPPURCHASE"];
                            _flexD[apply_row, "NO_PR"] = "";
                            _flexD[apply_row, "CD_SL"] = dr["CD_SL"];
                            _flexD[apply_row, "NM_SL"] = dr["NM_SL"];
                            _flexD[apply_row, "PI_PARTNER"] = dr["PARTNER"];
                            _flexD[apply_row, "PI_LN_PARTNER"] = dr["LN_PARTNER"];
                            _flexD[apply_row, "CD_EXCH"] = cbo_NM_EXCH.SelectedValue.ToString();

                         
                            

                            DataTable dtExch = (DataTable)cbo_NM_EXCH.DataSource;



                            foreach (DataRow drExch in dtExch.Rows)
                            {
                                if (drExch["CODE"].ToString() == cbo_NM_EXCH.SelectedValue.ToString())
                                {
                                    _flexD[apply_row, "NM_EXCH"] = drExch["NAME"];
                                    break;
                                }
                            }

                            SetCC(apply_row, "");

                            //MaxSeq++;
                            //첫줄여부 = false;

                                                    _flexD.Redraw = true;
                            _flexD[apply_row, "AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flexD[apply_row, "AM_EX"]) * tb_NM_EXCH.DecimalValue);
                            _flexD[apply_row, "VAT"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flexD[apply_row, "AM"]) * (UDecimal.Getdivision(D.GetDecimal(_flexD[apply_row, "RATE_VAT"]), 100)));
                            _flexD[apply_row, "AM_TOTAL"] = D.GetDecimal((_flexD[apply_row, "AM"])) + D.GetDecimal((_flexD[apply_row, "VAT"])); //총합계

                            if (bStandard)
                            {

                                _flexD[apply_row, "CLS_L"] = dr["CLS_L"];
                                _flexD[apply_row, "CLS_M"] = dr["CLS_M"];
                                _flexD[apply_row, "CLS_S"] = dr["CLS_S"];
                                _flexD[apply_row, "NM_CLS_L"] = dr["CLS_LN"];
                                _flexD[apply_row, "NM_CLS_M"] = dr["CLS_MN"];
                                _flexD[apply_row, "NM_CLS_S"] = dr["CLS_SN"];
                                _flexD[apply_row, "NUM_STND_ITEM_1"] = dr["NUM_STND_ITEM_1"];
                                _flexD[apply_row, "NUM_STND_ITEM_2"] = dr["NUM_STND_ITEM_2"];
                                _flexD[apply_row, "NUM_STND_ITEM_3"] = dr["NUM_STND_ITEM_3"];
                                _flexD[apply_row, "NUM_STND_ITEM_4"] = dr["NUM_STND_ITEM_4"];
                                _flexD[apply_row, "NUM_STND_ITEM_5"] = dr["NUM_STND_ITEM_5"];

                                //규격 중량단가 가져오기 
                                if (bStandard)
                                {
                                    if (Global.MainFrame.ServerKey == "SINJINSM" || Global.MainFrame.ServerKey == "SQL_108" || Global.MainFrame.ServerKey == "DZSQL")
                                    {
                                        DataTable dt_item = null;

                                        dt_item = _biz.Check_ITEMGRP_SG(D.GetString(_flexD[apply_row, "GRP_ITEM"]));

                                        if (dt_item.Rows.Count > 0)
                                        {
                                            _flexD[apply_row, "QT_SG"] = dt_item.Rows[0]["QT_SG"];
                                            _flexD[apply_row, "SG_TYPE"] = dt_item.Rows[0]["SG_TYPE"];
                                            _flexD[apply_row, "UM_WEIGHT"] = dt_item.Rows[0]["UM_WEIGHT"];
                                        }
                                        금액계산(apply_row, 0, _flexD.CDecimal(_flexD[e.Row, "QT_PO_MM"]), "CD_ITEM", 0);

                                    }
                                }


                            }


                            _flexD.Redraw = true;
                            _flexD.RemoveDummyColumnAll();
                            _flexD.AddFinished();
                            _flexD.Col = _flexD.Cols.Fixed;
                            _flexD.Select(apply_row, _flexD.Cols.Fixed);

                            if (sPUSU == "100")
                                GET_SU_BOM();   //품목입력시 해당품목의 외주BOM(SU_BOM)을 가져오는 구문

                        }

                      
                        break;

                    case "CD_SL":

                        object[] pi_obj = new object[] { Global.MainFrame.LoginInfo.CompanyCode, tb_DT_PO.Text.Substring(0, 4), D.GetString(cbo_CD_PLANT.SelectedValue), D.GetString(_flexD["CD_ITEM"]), D.GetString(helpReturn.Rows[0]["CD_SL"]) };
                        DataTable dt_pinv = _biz.item_pinvn(pi_obj);

                        if (dt_pinv != null && dt_pinv.Rows.Count > 0)
                        {
                            _flexD["QT_INVC"] = dt_pinv.Rows[0]["QT_INVC"]; //현재고
                            _flexD["QT_ATPC"] = dt_pinv.Rows[0]["QT_ATPC"]; //가용재고
                        }
                        // _flexD.AcceptChanges();
                        SetQtValue(_flexD.Row);
                        break;
                    case "CD_PJT":
                        _flexD["CD_PJT"] = D.GetString(helpReturn.Rows[0]["NO_PROJECT"]);
                        _flexD["NM_PJT"] = D.GetString(helpReturn.Rows[0]["NM_PROJECT"]);
                        if (Config.MA_ENV.YN_UNIT == "Y")
                            _flexD["SEQ_PROJECT"] = D.GetDecimal(helpReturn.Rows[0]["SEQ_PROJECT"]);
                        else
                            _flexD["SEQ_PROJECT"] = 0;


                        if (BASIC.GetMAEXC("발주등록(공장)-프로젝트별_의제매입세_구분") == "100" && D.GetString(_flexD["CD_USERDEF14"]) == "001") //의제 매입 프로젝트별 적용
                        {
                            string fg_tax_josun = _biz.pjt_item_josun(D.GetString(_flexD["CD_PJT"]));
                            Decimal vat_rate = 0;
                            if (fg_tax_josun != "")
                            {
                                _flexD[apply_row, "FG_TAX"] = fg_tax_josun;
                                vat_rate = 0;
                                _flexD[e.Row, "RATE_VAT"] = vat_rate;//부가세율은 0이 들어가면된다고함 (김광석사원요청) 2011-12-02
                            }
                            else
                            {
                                _flexD["FG_TAX"] = _header.CurrentRow["FG_TAX"];
                                _flexD["RATE_VAT"] = tb_TAX.DecimalValue;
                                vat_rate = tb_TAX.DecimalValue;
                            }



                            vat_rate = (vat_rate == 0) ? 0 : vat_rate / 100;

                            if (vat_rate == 0 || Convert.ToDecimal(_flexD["AM"]) == 0)
                                _flexD["VAT"] = 0;
                            else
                                _flexD["VAT"] = Unit.원화금액(DataDictionaryTypes.PU, Convert.ToDecimal(_flexD["AM"]) * vat_rate); ;

                            _flexD["AM_TOTAL"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flexD["AM"]) + D.GetDecimal(_flexD["VAT"]));


                        }
                        if (m_sEnv_CC == "200")
                            SetCC(e.Row, string.Empty);

                        //조원관광진흥(주)전용.. 
                        if (Global.MainFrame.ServerKeyCommon.ToUpper() == "ZOONE")
                        {
                            DataTable dtZOO = _biz.GetInfo_ZOO(helpReturn.CodeValue);

                            if (dtZOO != null && dtZOO.Rows.Count != 0)
                            {
                                _flexD["CD_SL"] = dtZOO.Rows[0]["CD_SL"];
                                _flexD["NM_SL"] = dtZOO.Rows[0]["NM_SL"];
                            }
                        }

                        if (Global.MainFrame.ServerKeyCommon == "UNIPOINT")
                        {
                            DataTable dtPjt = _biz.Get_Project_Detail(D.GetString(_flexD["CD_PJT"]));

                            if (dtPjt != null && dtPjt.Rows.Count != 0)
                            {
                                _flexD["CD_PARTNER_PJT"] = dtPjt.Rows[0]["CD_PARTNER"];
                                _flexD["LN_PARTNER_PJT"] = dtPjt.Rows[0]["LN_PARTNER"];
                                _flexD["NO_EMP_PJT"] = dtPjt.Rows[0]["NO_EMP"];
                                _flexD["NM_KOR_PJT"] = dtPjt.Rows[0]["NM_KOR"];
                                _flexD["END_USER"] = dtPjt.Rows[0]["END_USER"];
                            }
                        }

                        break;

                        //재질 코드(품목군)를 입력 하였을 때 비중 및 중량단가를 같이 입력해줌
                    case "NM_ITEMGRP":
                       
                        if (bStandard)
                        {

                            if (Global.MainFrame.ServerKey == "SINJINSM" || Global.MainFrame.ServerKey == "SQL_108" || Global.MainFrame.ServerKey == "DZSQL")
                            {
                                _flexD["GRP_ITEM"] = D.GetString(helpReturn.Rows[0]["CD_ITEMGRP"]);
                                _flexD["NM_ITEMGRP"] = D.GetString(helpReturn.Rows[0]["NM_ITEMGRP"]);
                                DataTable dt = _biz.Check_ITEMGRP_SG(D.GetString(_flexD["GRP_ITEM"]));

                                if (dt.Rows.Count > 0)
                                {
                                    //재질구분
                                    _flexD["SG_TYPE"] = D.GetString(dt.Rows[0]["SG_TYPE"]);

                                    //비중
                                    _flexD["QT_SG"] = D.GetDecimal(dt.Rows[0]["QT_SG"]);

                                    //중량단가
                                    _flexD["UM_WEIGHT"] = D.GetDecimal(dt.Rows[0]["UM_WEIGHT"]);
                                }
                                if (D.GetDecimal(_flexD[e.Row, "QT_PO_MM"]) != 0)
                                    금액계산(e.Row, 0, D.GetDecimal(_flexD[e.Row, "QT_PO_MM"]), "NM_ITEMGRP", 0);

                                _flexD["CD_ITEM"] = D.GetString(helpReturn.Rows[0]["CD_ITEMGRP"]).Substring(1, 2) + '-' + D.GetDecimal(_flexD["NUM_STND_ITEM_1"]).ToString("###############0.####") + '*' +
                                                          D.GetDecimal(_flexD["NUM_STND_ITEM_2"]).ToString("###############0.####") + '*' + D.GetDecimal(_flexD["NUM_STND_ITEM_3"]).ToString("###############0.####");
                                _flexD["CD_ITEM"] = D.GetString(_flexD["CD_ITEM"]) + getCLS_S_code(D.GetString(_flexD["CLS_S"]), D.GetString(_flexD["CD_ITEM"]), D.GetString(cbo_CD_PLANT.SelectedValue));

                                _flexD["NM_ITEM"] = D.GetString(_flexD["NM_CLS_L"]) + ' ' + D.GetString(_flexD.GetDataDisplay("NM_CLS_S")).Replace('"', ' ').Trim();
                                _flexD["STND_ITEM"] = D.GetString(helpReturn.Rows[0]["NM_ITEMGRP"]) + '-' + D.GetDecimal(_flexD["NUM_STND_ITEM_1"]).ToString("###############0.####") + '*' +
                                                       D.GetDecimal(_flexD["NUM_STND_ITEM_2"]).ToString("###############0.####") + '*' + D.GetDecimal(_flexD["NUM_STND_ITEM_3"]).ToString("###############0.####");

                                _flexD["STND_ITEM"] = _flexD["STND_ITEM"] + getCLS_S_code(D.GetString(_flexD["CLS_S"]), D.GetString(_flexD["CD_ITEM"]), D.GetString(cbo_CD_PLANT.SelectedValue));
                            }
                        }
                        
                        break;

                    case "NM_CLS_L" :
                                            
                        if (bStandard && BASIC.GetMAEXC("공장품목-대중소분류 종속관계 설정").Equals("001"))
                        {
                            if (Global.MainFrame.ServerKey == "SINJINSM" || Global.MainFrame.ServerKey == "SQL_108" || Global.MainFrame.ServerKey == "DZSQL")
                            {
                                if (D.GetString(_flexD["GRP_ITEM"]).Length < 3)
                                {
                                    ShowMessage(공통메세지._은는필수입력항목입니다, _flexD.Cols["NM_ITEMGRP"].Caption);
                                    e.Cancel = true;
                                    return;
                                }

                                _flexD["CLS_M"] = "";
                                _flexD["NM_CLS_M"] = "";

                                _flexD["CLS_S"] = "";
                                _flexD["NM_CLS_S"] = "";
                                _flexD["CD_ITEM"] = D.GetString(_flexD["GRP_ITEM"]).Substring(1, 2) + '-' + D.GetDecimal(_flexD["NUM_STND_ITEM_1"]).ToString("###############0.####") + '*' +
                                                             D.GetDecimal(_flexD["NUM_STND_ITEM_2"]).ToString("###############0.####") + '*' + D.GetDecimal(_flexD["NUM_STND_ITEM_3"]).ToString("###############0.####");
                                _flexD["CD_ITEM"] = D.GetString(_flexD["CD_ITEM"]) + getCLS_S_code(D.GetString(_flexD[e.Row, "CLS_S"]), D.GetString(_flexD["CD_ITEM"]), D.GetString(cbo_CD_PLANT.SelectedValue));

                                _flexD["NM_ITEM"] = D.GetString(helpReturn.Rows[0]["NM_SYSDEF"]) + ' ' + D.GetString(_flexD.GetDataDisplay("NM_CLS_S")).Replace('"', ' ').Trim();
                                _flexD["STND_ITEM"] = D.GetString(_flexD["NM_ITEMGRP"]) + '-' + D.GetDecimal(_flexD["NUM_STND_ITEM_1"]).ToString("###############0.####") + '*' +
                                                           D.GetDecimal(_flexD["NUM_STND_ITEM_2"]).ToString("###############0.####") + '*' + D.GetDecimal(_flexD["NUM_STND_ITEM_3"]).ToString("###############0.####");

                                _flexD["STND_ITEM"] = _flexD["STND_ITEM"] + getCLS_S_code(D.GetString(_flexD[e.Row, "CLS_S"]), D.GetString(_flexD["CD_ITEM"]), D.GetString(cbo_CD_PLANT.SelectedValue));
                            }
                        }

                        break;
                    case "NM_CLS_M" : 
                        
                        if (bStandard && BASIC.GetMAEXC("공장품목-대중소분류 종속관계 설정").Equals("001"))
                        {
                            if (Global.MainFrame.ServerKey == "SINJINSM" || Global.MainFrame.ServerKey == "SQL_108" || Global.MainFrame.ServerKey == "DZSQL")
                            {
                                if (D.GetString(_flexD["GRP_ITEM"]).Length < 3)
                                {
                                    ShowMessage(공통메세지._은는필수입력항목입니다, _flexD.Cols["NM_ITEMGRP"].Caption);
                                    e.Cancel = true;
                                    return;
                                }

                                _flexD["CLS_S"] = "";
                                _flexD["NM_CLS_S"] = "";
                                _flexD["CD_ITEM"] = D.GetString(_flexD["GRP_ITEM"]).Substring(1, 2) + '-' + D.GetDecimal(_flexD["NUM_STND_ITEM_1"]).ToString("###############0.####") + '*' +
                                                             D.GetDecimal(_flexD["NUM_STND_ITEM_2"]).ToString("###############0.####") + '*' + D.GetDecimal(_flexD["NUM_STND_ITEM_3"]).ToString("###############0.####");
                                _flexD["CD_ITEM"] = D.GetString(_flexD["CD_ITEM"]) + getCLS_S_code(D.GetString(_flexD["CLS_S"]), D.GetString(_flexD["CD_ITEM"]), D.GetString(cbo_CD_PLANT.SelectedValue));

                                _flexD["NM_ITEM"] = D.GetString(_flexD["NM_CLS_L"]) + ' ' + D.GetString(_flexD.GetDataDisplay("NM_CLS_S")).Replace('"', ' ').Trim();
                                _flexD["STND_ITEM"] = D.GetString(_flexD["NM_ITEMGRP"]) + '-' + D.GetDecimal(_flexD["NUM_STND_ITEM_1"]).ToString("###############0.####") + '*' +
                                                           D.GetDecimal(_flexD["NUM_STND_ITEM_2"]).ToString("###############0.####") + '*' + D.GetDecimal(_flexD["NUM_STND_ITEM_3"]).ToString("###############0.####");

                                _flexD["STND_ITEM"] = _flexD["STND_ITEM"] + getCLS_S_code(D.GetString(_flexD["CLS_S"]), D.GetString(_flexD["CD_ITEM"]), D.GetString(cbo_CD_PLANT.SelectedValue));
                            }
                        }
                        break;
                    case "NM_CLS_S" :
                        
                        if (bStandard)
                        {
                            if (Global.MainFrame.ServerKey == "SINJINSM" || Global.MainFrame.ServerKey == "SQL_108" || Global.MainFrame.ServerKey == "DZSQL")
                            {
                                if (D.GetString(_flexD["GRP_ITEM"]).Length < 3)
                                {
                                    ShowMessage(공통메세지._은는필수입력항목입니다, _flexD.Cols["NM_ITEMGRP"].Caption);
                                    e.Cancel = true;
                                    return;
                                }

                                _flexD["CD_ITEM"] = D.GetString(_flexD["GRP_ITEM"]).Substring(1, 2) + '-' + D.GetDecimal(_flexD["NUM_STND_ITEM_1"]).ToString("###############0.####") + '*' +
                                                         D.GetDecimal(_flexD["NUM_STND_ITEM_2"]).ToString("###############0.####") + '*' + D.GetDecimal(_flexD["NUM_STND_ITEM_3"]).ToString("###############0.####");
                                _flexD["CD_ITEM"] = D.GetString(_flexD["CD_ITEM"]) + getCLS_S_code(D.GetString(helpReturn.Rows[0]["CD_SYSDEF"]), D.GetString(_flexD["CD_ITEM"]), D.GetString(cbo_CD_PLANT.SelectedValue));

                                _flexD["NM_ITEM"] = D.GetString(_flexD["NM_CLS_L"]) + ' ' + D.GetString(helpReturn.Rows[0]["NM_SYSDEF"]).Replace('"', ' ').Trim();
                                _flexD["STND_ITEM"] = D.GetString(_flexD["NM_ITEMGRP"]) + '-' + D.GetDecimal(_flexD["NUM_STND_ITEM_1"]).ToString("###############0.####") + '*' +
                                                           D.GetDecimal(_flexD["NUM_STND_ITEM_2"]).ToString("###############0.####") + '*' + D.GetDecimal(_flexD["NUM_STND_ITEM_3"]).ToString("###############0.####");

                                _flexD["STND_ITEM"] = _flexD["STND_ITEM"] + getCLS_S_code(D.GetString(_flexD["CLS_S"]), D.GetString(_flexD["CD_ITEM"]), D.GetString(cbo_CD_PLANT.SelectedValue));
                            }
                        }
                        

                        break;
                }
            }
            else if (flex.Name == _flexDD.Name)
            {
                foreach (DataRow dr in e.Result.Rows)
                {
                    if (e.Result.Rows[0] != dr)     //첫번째 행이 아닐 경우만 추가를 해준다.
                        추가_Click(null, null);

                    flex["CD_MATL"] = dr["CD_ITEM"];
                    flex["NM_ITEM"] = dr["NM_ITEM"];
                    flex["STND_ITEM"] = dr["STND_ITEM"];
                    flex["STND_DETAIL_ITEM"] = dr["STND_DETAIL_ITEM"];
                    flex["UNIT_MO"] = dr["UNIT_MO"];

                    decimal dPo = _flexD.CDecimal(_flexD["QT_PO"]);

                    flex["QT_NEED"] = dPo != 0 ? System.Math.Round(dPo * flex.CDecimal(flex["QT_NEED_UNIT"]), 4, MidpointRounding.AwayFromZero) : flex.CDecimal(flex["QT_NEED_UNIT"]);
                    flex["QT_LOSS"] = flex.CDecimal(flex["QT_NEED"]) - flex.CDecimal(flex["QT_REQ"]);
                }
            }
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
        finally
        {
            _flexD.Redraw = true;
        }
    }

    #endregion

    #region -> 프로젝트 도움창 호출 이벤트(_flex_HelpClick)
    void _flexD_HelpClick(object sender, EventArgs e)
    {
        try
        {
            FlexGrid _flex = sender as FlexGrid;
            if (_flex == null) return;

            switch (_flex.Cols[_flex.Col].Name)
            {
                case "CD_PJT":

                    sale.P_SA_PRJ_SUB dlg = new sale.P_SA_PRJ_SUB();

                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        _flexD["CD_PJT"] = dlg.returnParams[0];   //프로젝트번호
                        _flexD["NM_PJT"] = dlg.returnParams[4];      //프로젝트명
                    }
                    break;
                case "UM_EX_PO":
                    if (!_flex.HasNormalRow || _flex["CD_ITEM"].ToString() == "") return;

                    P_PU_UM_HISTORY m_dlg = new P_PU_UM_HISTORY(tb_NM_PARTNER.CodeValue, tb_NM_PARTNER.CodeName, _flex["CD_ITEM"].ToString(), _flex["NM_ITEM"].ToString(), tb_FG_PO_TR.CodeValue, tb_FG_PO_TR.CodeName, cbo_NM_EXCH.SelectedValue.ToString(), _header.CurrentRow["FG_TRANS"].ToString());

                    if (m_dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        if (m_dlg.UM != "")
                            _flexD["UM_EX_PO"] = _flex.CDecimal(m_dlg.UM);
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

    #region -> 그리드 ValidateEdit Check 이벤트(Grid_ValidateEdit)

    private void 
        Grid_ValidateEdit(object sender, ValidateEditEventArgs e)
    {
        try
        {
                    FlexGrid _flex = sender as FlexGrid;

                    if (_flex.Name == _flexD.Name)
                    {


                        string oldValue = ((FlexGrid)sender).GetData(e.Row, e.Col).ToString();
                        string newValue = ((FlexGrid)sender).EditData;

                        if (oldValue.ToUpper() == newValue.ToUpper())
                            return;

                        string ColName = ((FlexGrid)sender).Cols[e.Col].Name;

                        if (_flexD["CD_ITEM"].ToString() == "") return;

                        decimal 부가세율 = 0.1M;
                        Decimal ldb_VatKr = 0, ldb_AmKr = 0, ldb_amEx = 0, ldb_AM = 0;
                        Decimal rate_vat = D.GetDecimal(_flexD[e.Row, "RATE_VAT"]) == 0 ? 0 : D.GetDecimal(_flexD[e.Row, "RATE_VAT"]) / 100;  //과세율   
                        Decimal 수량 = D.GetDecimal(_flexD[e.Row, "QT_PO_MM"]);
                        Decimal 단가 = D.GetDecimal(_flexD[e.Row, "UM_EX_PO"]);
                        Decimal 환율 = D.GetDecimal(_header.CurrentRow["RT_EXCH"]) == 0 ? 1 : D.GetDecimal(_header.CurrentRow["RT_EXCH"]);
                        string 과세구분 = D.GetString(_flexD[e.Row, "FG_TAX"]);
                        bool 부가세포함 = D.GetString(_flexD[e.Row, "TP_UM_TAX"]) == "001" ? true : false;

                        switch (_flexD.Cols[e.Col].Name)
                        {

                            case "QT_PO_MM":        // 발주량
                                if (_flexD.CDecimal(newValue) < _flexD.CDecimal(_flexD[e.Row, "QT_REQ_MM"]))
                                {
                                    ShowMessage(공통메세지._은_보다크거나같아야합니다, new String[] { DD("발주수량"), DD("입고수량") });
                                    ((FlexGrid)sender)["QT_PO_MM"] = ((FlexGrid)sender).GetData(e.Row, e.Col);
                                    return;
                                }

                                if (Global.MainFrame.ServerKey == "KPCI" && D.GetString(_flex["NO_RELATION"]) != string.Empty)
                                {
                                     DataRow dr = _biz.계약잔량체크(D.GetString(_flex["NO_RELATION"]), D.GetDecimal(_flex["SEQ_RELATION"]));

                                    if ( D.GetString(dr["CD_TYPE"]) == "001" && (D.GetDecimal(newValue) > D.GetDecimal(dr["QT_CON"])))
                                    {
                                        ShowMessage("잔량을 초과하였습니다.");
                                        _flex["QT_SO"] = oldValue;
                                        e.Cancel = true;
                                        return;
                                    }
                                }
                                /*
                                if (D.GetString(_flexD[e.Row, "NO_PR"]) != string.Empty)
                                {
                                    if (D.GetDecimal(_flexD[e.Row, "QT_PR"]) < _flexD.CDecimal(newValue))
                                    {
                                        ShowMessage(공통메세지._은_보다작거나같아야합니다, new string[] { DD("발주수량"), DD("요청수량") });
                                        return;
                                    }
                                }

                                if (D.GetString(_flexD[e.Row, "NO_APP"]) != string.Empty)
                                {
                                    if (D.GetDecimal(_flexD[e.Row, "QT_APP"]) < _flexD.CDecimal(newValue))
                                    {
                                        ShowMessage(공통메세지._은_보다작거나같아야합니다, new string[] { DD("발주수량"), DD("품의수량") });
                                        return;
                                    }
                                }
                                */

                                if (_m_Company_only == "001")
                                    AsahiKasei_Only_ValidateEdit(e.Row, D.GetDecimal(newValue), "QT_PO_MM");

                                금액계산(e.Row, _flexD.CDecimal(_flexD[e.Row, "UM_EX_PO"]), Convert.ToDecimal(newValue), "QT_PO_MM", Convert.ToDecimal(newValue));

                                if (!bStandard)
                                    _flexD[e.Row, "QT_WEIGHT"] = _flexD.CDecimal(newValue) * _flexD.CDecimal(_flexD[e.Row, "WEIGHT"]);


                                CalcRebate(D.GetDecimal(newValue), D.GetDecimal(_flexD["UM_REBATE"]));  // 리베이트 재계산
                                사급자재변경구문(D.GetDecimal(newValue));

                                if (Global.MainFrame.ServerKeyCommon.ToUpper() == "WINPLUS")
                                    _flexD[e.Row, "NUM_USERDEF1"] = _flexD[e.Row, "NUM_USERDEF2"] = 0;


                                break;

                            case "UM_EX_PO":  // 단가 
                                if (_m_Company_only == "001")
                                    AsahiKasei_Only_ValidateEdit(e.Row, D.GetDecimal(newValue), "UM_EX_PO");

                                금액계산(e.Row, Convert.ToDecimal(newValue), _flexD.CDecimal(_flexD[e.Row, "QT_PO_MM"]), "UM_EX_PO", Convert.ToDecimal(newValue));


                                break;

                            case "AM_EX":
                                금액계산(e.Row, _flexD.CDecimal(_flexD[e.Row, "UM_EX_PO"]), _flexD.CDecimal(_flexD[e.Row, "QT_PO_MM"]), "AM_EX", _flexD.CDecimal(newValue));


                                if (_m_Company_only == "001")
                                    AsahiKasei_Only_ValidateEdit(e.Row, D.GetDecimal(newValue), "AM_EX");
                                break;
                            case "AM":
                                if (System.Math.Abs((D.GetDecimal(_flexD["AM_EX"]) * D.GetDecimal(_header.CurrentRow["RT_EXCH"])) - D.GetDecimal(newValue)) > 500)
                                {
                                    ShowMessage("500원 범위에서 수정 가능합니다.(단수차 관리)");
                                    _flexD["AM"] = D.GetDecimal(oldValue);
                                    e.Cancel = true;
                                    return;

                                }
                                부가세만계산();

                                break;
                            case "VAT":

                                //if (tb_TAX.DecimalValue != 0)
                                //    SUMFunction();
                                //else
                                //    _flexD[e.Row, "VAT"] = 0;

                                부가세율 = _flexD.CDecimal(_flexD[e.Row, "RATE_VAT"]);
                                if (부가세율 == 0.0M && tb_TAX.DecimalValue == 0)
                                    _flexD[e.Row, "VAT"] = 0;
                                else
                                    SUMFunction();

                                break;
                            case "DT_LIMIT":
                            case "DT_PLAN":
                                if (newValue.Trim().Length != 8)
                                {
                                    ShowMessage(공통메세지.입력형식이올바르지않습니다);
                                    if (_flexD.Editor != null)
                                    {
                                        _flexD.Editor.Text = string.Empty;
                                    }
                                    e.Cancel = true;
                                    return;
                                }
                                else
                                {
                                    if (!_flexD.IsDate(ColName))
                                    {
                                        ShowMessage(공통메세지.입력형식이올바르지않습니다);
                                        if (_flexD.Editor != null)
                                        {
                                            _flexD.Editor.Text = string.Empty;
                                        }
                                        e.Cancel = true;
                                        return;
                                    }
                                }
                                break;
                            case "FG_TAX":

                                if (Global.MainFrame.LoginInfo.CompanyLanguage != Language.KR)
                                    ldb_AM = Unit.원화금액(DataDictionaryTypes.PU, 수량 * 단가 * 환율); //총금액 
                                else
                                    ldb_AM = Decimal.Round(System.Math.Round(수량 * 단가 * 환율), MidpointRounding.AwayFromZero); //총금액 

                                if (_header.CurrentRow["FG_TRANS"].ToString() != "001" || _flexD["FG_TAX"].ToString() == string.Empty)
                                    부가세율 = tb_TAX.DecimalValue;  // / 100;
                                else
                                {
                                    if (의제매입여부(newValue) && s_vat_fictitious == "100")
                                    {
                                        부가세포함 = true;
                                        _flexD[e.Row, "TP_UM_TAX"] = "001";
                                    }
                                    else
                                    {
                                        _flexD[e.Row, "TP_UM_TAX"] = D.GetString(cbo_TP_TAX.SelectedValue);
                                    }

                                    if (의제매입여부(newValue) && s_vat_fictitious == "000")
                                    {
                                        부가세율 = 0;
                                    }
                                    else
                                    {
                                        Object[] obj = new Object[3];
                                        obj[0] = LoginInfo.CompanyCode;
                                        obj[1] = "MA_B000046";
                                        obj[2] = newValue;
                                        DataTable dt = Duzon.ERPU.MF.ComFunc.GetTableSearch("MA_CODEDTL", obj);
                                        if (dt.Rows.Count > 0 && dt.Rows[0]["CD_FLAG1"].ToString() != string.Empty)
                                        {
                                            부가세율 = Convert.ToDecimal(dt.Rows[0]["CD_FLAG1"]);
                                        }
                                    }
                                }
                                _flexD[e.Row, "RATE_VAT"] = 부가세율;

                                if (수량 == 0) return;
                                Calc.GetAmt(수량, 단가, 환율, 과세구분, 부가세율, 모듈.PUR, 부가세포함, out ldb_amEx, out ldb_AmKr, out ldb_VatKr);

                                _flexD[e.Row, "VAT"] = ldb_VatKr;
                                _flexD[e.Row, "AM"] = ldb_AmKr;
                                _flexD[e.Row, "AM_EX"] = ldb_amEx;
                                _flexD[e.Row, "AM_TOTAL"] = Calc.합계금액(ldb_AmKr, ldb_VatKr);

                                SUMFunction();
                                break;

                            case "WEIGHT":

                                if (!bStandard)
                                    _flexD[e.Row, "QT_WEIGHT"] = _flexD.CDecimal(_flexD[e.Row, "QT_PO_MM"]) * _flexD.CDecimal(newValue);
                                break;

                            case "UM_EX_AR":
                                AsahiKasei_Only_ValidateEdit(e.Row, D.GetDecimal(newValue), "UM_EX_AR");
                                break;

                            case "TP_UM_TAX":
                                if (수량 == 0) return;
                                if (부가세포함)  //부가세포함
                                {
                                    if (Global.MainFrame.LoginInfo.CompanyLanguage != Language.KR)
                                        ldb_AM = Unit.원화금액(DataDictionaryTypes.PU, 수량 * 단가 * 환율); //총금액 
                                    else
                                        ldb_AM = Decimal.Round(수량 * 단가 * 환율, MidpointRounding.AwayFromZero); //총금액 
                                    if (Global.MainFrame.LoginInfo.CompanyLanguage != Language.KR)
                                        ldb_VatKr = Unit.원화금액(DataDictionaryTypes.PU, ldb_AM / (1 + rate_vat) * rate_vat); //부가세   
                                    else
                                        ldb_VatKr = Decimal.Round(ldb_AM / (1 + rate_vat) * rate_vat, MidpointRounding.AwayFromZero); //부가세   
                                    ldb_AmKr = Unit.원화금액(DataDictionaryTypes.PU, ldb_AM - ldb_VatKr);   //원화금액   
                                    ldb_amEx = Unit.외화금액(DataDictionaryTypes.PU, ldb_AmKr / 환율);  // 외화금액 
                                }
                                else
                                {
                                    ldb_AmKr = Unit.원화금액(DataDictionaryTypes.PU, 수량 * 단가 * 환율);     //원화금액  
                                    ldb_VatKr = Unit.원화금액(DataDictionaryTypes.PU, ldb_AmKr * rate_vat);   //부가세   
                                    ldb_AM = Unit.원화금액(DataDictionaryTypes.PU, ldb_AmKr + ldb_VatKr);   //총금액  
                                    ldb_amEx = Unit.외화금액(DataDictionaryTypes.PU, ldb_AmKr / 환율);  // 외화금액                     
                                }
                                //외화단가               
                                _flexD[e.Row, "UM_EX_PO"] = 단가;      //외화단가(발주단위) 
                                _flexD[e.Row, "UM_P"] = 단가 * 환율;
                                _flexD[e.Row, "UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, 단가 / (D.GetDecimal(_flexD["RT_PO"]) == 0 ? 1 : D.GetDecimal(_flexD["RT_PO"])));  //  외화단가( 재고단위)     
                                _flexD[e.Row, "UM"] = Unit.원화단가(DataDictionaryTypes.PU, (단가 / (D.GetDecimal(_flexD["RT_PO"]) == 0 ? 1 : D.GetDecimal(_flexD["RT_PO"]))) * 환율);

                                _flexD[e.Row, "AM_EX"] = ldb_amEx;
                                _flexD[e.Row, "AM"] = ldb_AmKr;
                                _flexD[e.Row, "VAT"] = ldb_VatKr;
                                _flexD[e.Row, "AM_TOTAL"] = ldb_AmKr + ldb_VatKr;    //_flexD.CDecimal(_flexD["AM"]) + _flexD.CDecimal(_flexD["VAT"]);

                                SUMFunction();

                                break;

                            case "AM_TOTAL":
                                if (수량 == 0) return;
                                if (부가세포함)  //부가세포함
                                {
                                    if (Global.MainFrame.LoginInfo.CompanyLanguage != Language.KR)
                                        ldb_AM = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flexD.EditData)); //총금액 
                                    else
                                        ldb_AM = Decimal.Round(D.GetDecimal(_flexD.EditData), MidpointRounding.AwayFromZero); //총금액 

                                    ldb_VatKr = 0.0m;
                                    if (의제매입여부(과세구분) && s_vat_fictitious == "100")
                                    {
                                        ldb_VatKr = Unit.원화금액(DataDictionaryTypes.PU, (ldb_AM * rate_vat));
                                    }
                                    else
                                    {
                                        if (Global.MainFrame.LoginInfo.CompanyLanguage != Language.KR)
                                            ldb_VatKr = Unit.원화금액(DataDictionaryTypes.PU, ldb_AM / (1 + rate_vat) * rate_vat); //부가세   
                                        else
                                            ldb_VatKr = Decimal.Round(ldb_AM / (1 + rate_vat) * rate_vat, MidpointRounding.AwayFromZero); //부가세   
                                    }
                                    ldb_AmKr = Unit.원화금액(DataDictionaryTypes.PU, ldb_AM - ldb_VatKr);   //원화금액   
                                    ldb_amEx = Unit.외화금액(DataDictionaryTypes.PU, ldb_AmKr / 환율);  // 외화금액

                                    _flexD[e.Row, "AM_EX"] = ldb_amEx;
                                    _flexD[e.Row, "AM"] = ldb_AmKr;
                                    _flexD[e.Row, "VAT"] = ldb_VatKr;

                                    단가 = (ldb_AM / 수량 / 환율);
                                    _flexD[e.Row, "UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, 단가);
                                    _flexD[e.Row, "UM_P"] = 단가 * 환율;
                                    _flexD[e.Row, "UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, UDecimal.Getdivision(단가, (D.GetDecimal(_flexD["RT_PO"]) == 0 ? 1 : D.GetDecimal(_flexD["RT_PO"]))));
                                    _flexD[e.Row, "UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(_flexD[e.Row, "UM_EX"]) * 환율);

                                    SUMFunction();
                                }

                                break;

                            case "AM_EX_TRANS":
                                _flexD["AM_TRANS"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(newValue) * D.GetDecimal(tb_NM_EXCH.Text));
                                break;

                            case "AM_TRANS":
                                _flexD["AM_EX_TRANS"] = Unit.외화금액(DataDictionaryTypes.PU, UDecimal.Getdivision(D.GetDecimal(newValue), D.GetDecimal(tb_NM_EXCH.Text)));
                                break;

                            case "UM_REBATE":
                                CalcRebate(D.GetDecimal(_flexD["QT_PO_MM"]), D.GetDecimal(newValue));
                                break;

                            case "AM_REBATE_EX":
                                _flexD["UM_REBATE"] = Unit.외화단가(DataDictionaryTypes.PU, UDecimal.Getdivision(D.GetDecimal(newValue), D.GetDecimal(_flexD["QT_PO_MM"])));
                                _flexD["AM_REBATE"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(newValue) * D.GetDecimal(tb_NM_EXCH.Text));
                                break;

                            case "AM_REBATE":
                                _flexD["AM_REBATE_EX"] = Unit.외화금액(DataDictionaryTypes.PU, UDecimal.Getdivision(D.GetDecimal(newValue), D.GetDecimal(tb_NM_EXCH.Text)));
                                _flexD["UM_REBATE"] = Unit.외화단가(DataDictionaryTypes.PU, UDecimal.Getdivision(D.GetDecimal(_flexD["AM_REBATE_EX"]), D.GetDecimal(_flexD["QT_PO_MM"])));
                                break;

                            case "NUM_USERDEF1":
                                if (Global.MainFrame.ServerKeyCommon.ToUpper() == "WINPLUS")
                                {
                                    _flexD[e.Row, "QT_PO_MM"] = Decimal.Round((D.GetDecimal(newValue) * D.GetDecimal(_flexD[e.Row, "NUM_USERDEF2"])) / 10000, 1, MidpointRounding.AwayFromZero);
                                    금액계산(e.Row, _flexD.CDecimal(_flexD[e.Row, "UM_EX_PO"]), Convert.ToDecimal(_flexD[e.Row, "QT_PO_MM"]), "QT_PO_MM", Convert.ToDecimal(_flexD[e.Row, "QT_PO_MM"]));
                                }
                                break;

                            case "NUM_USERDEF2":
                                if (Global.MainFrame.ServerKeyCommon.ToUpper() == "WINPLUS")
                                {
                                    _flexD[e.Row, "QT_PO_MM"] = Decimal.Round((D.GetDecimal(newValue) * D.GetDecimal(_flexD[e.Row, "NUM_USERDEF1"])) / 10000, 1, MidpointRounding.AwayFromZero);
                                    금액계산(e.Row, _flexD.CDecimal(_flexD[e.Row, "UM_EX_PO"]), Convert.ToDecimal(_flexD[e.Row, "QT_PO_MM"]), "QT_PO_MM", Convert.ToDecimal(_flexD[e.Row, "QT_PO_MM"]));
                                }
                                break;
                            case "CLS_L":
                            case "CLS_S":
                            case "NM_ITEMGRP":
                            case "NUM_STND_ITEM_1":
                            case "NUM_STND_ITEM_2":
                            case "NUM_STND_ITEM_3":

                                if (Global.MainFrame.ServerKey == "SINJINSM" || Global.MainFrame.ServerKey == "SQL_108" || Global.MainFrame.ServerKey == "DZSQL")
                                {

                                    _flexD[e.Row, "CD_ITEM"] = D.GetString(_flexD[e.Row, "GRP_ITEM"]).Substring(1, 2) + '-' + D.GetDecimal(_flexD[e.Row, "NUM_STND_ITEM_1"]).ToString("###############0.####") + '*' +
                                                               D.GetDecimal(_flexD[e.Row, "NUM_STND_ITEM_2"]).ToString("###############0.####") + '*' + D.GetDecimal(_flexD[e.Row, "NUM_STND_ITEM_3"]).ToString("###############0.####");
                                    _flexD[e.Row, "CD_ITEM"] = D.GetString(_flexD[e.Row, "CD_ITEM"]) + getCLS_S_code(D.GetString(_flexD[e.Row, "CLS_S"]), D.GetString(_flexD[e.Row, "CD_ITEM"]), D.GetString(cbo_CD_PLANT.SelectedValue));


                                    _flexD[e.Row, "NM_ITEM"] = D.GetString(_flexD.GetDataDisplay(e.Row, "NM_CLS_L")).Replace('"', ' ').Trim() + ' ' + D.GetString(_flexD.GetDataDisplay(e.Row, "NM_CLS_S")).Replace('"', ' ').Trim();
                                    _flexD[e.Row, "STND_ITEM"] = D.GetString(_flexD[e.Row, "NM_ITEMGRP"]) + '-' + D.GetDecimal(_flexD[e.Row, "NUM_STND_ITEM_1"]).ToString("###############0.####") + '*' +
                                                               D.GetDecimal(_flexD[e.Row, "NUM_STND_ITEM_2"]).ToString("###############0.####") + '*' + D.GetDecimal(_flexD[e.Row, "NUM_STND_ITEM_3"]).ToString("###############0.####");

                                    _flexD[e.Row, "STND_ITEM"] = _flexD[e.Row, "STND_ITEM"] + getCLS_S_code(D.GetString(_flexD[e.Row, "CLS_S"]), D.GetString(_flexD[e.Row, "CD_ITEM"]), D.GetString(cbo_CD_PLANT.SelectedValue));

                                }
                                calcAM(e.Row);
                                break;
                            case "UM_WEIGHT":
                                calcAM(e.Row);
                                break;
                            case "TOT_WEIGHT":
                                calcAM(e.Row, D.GetDecimal(newValue));
                                break;
                        }
                    }
                    else if (_flex.Name == _flexDD.Name)
                    {
                        decimal dQT_NEED_UNIT = 0m;

                        switch (_flex.Cols[_flex.Col].Name)
                        {
                            case "QT_NEED":
                                dQT_NEED_UNIT = _flexD.CDecimal(_flexD["QT_PO"]) == 0m ? 0m : _flexD.CDecimal(_flexDD["QT_NEED"]) / _flexD.CDecimal(_flexD["QT_PO"]);
                                _flexD["QT_NEED_UNIT"] = Math.Round(dQT_NEED_UNIT, 4, MidpointRounding.AwayFromZero);
                                break;
                        }
                    }
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }

    }

    #endregion

    #region -> 그리드 더블클릭 이벤트(Grid_DoubleClick)

    //private void Grid_DoubleClick(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        FlexGrid _flex = sender as FlexGrid;
    //        if (_flex == null) return;

    //        if (!_flex.HasNormalRow || _flex["CD_ITEM"].ToString() == "") return;

    //        if (_flex.Cols["UM_EX_PO"].Index != _flex.Col) return;

    //        //if (호출여부 == true)
    //        //{
    //        P_PU_UM_HISTORY m_dlg = new P_PU_UM_HISTORY(tb_NM_PARTNER.CodeValue, tb_NM_PARTNER.CodeName, _flex["CD_ITEM"].ToString(), _flex["NM_ITEM"].ToString(), tb_FG_PO_TR.CodeValue, tb_FG_PO_TR.CodeName, cbo_NM_EXCH.SelectedValue.ToString(), _header.CurrentRow["FG_TRANS"].ToString());

    //        if (m_dlg.ShowDialog(this) == DialogResult.OK)
    //        {
    //            if (m_dlg.UM != "")
    //                _flexD["UM_EX_PO"] = _flex.CDecimal(m_dlg.UM);
    //        }
    //        //  호출여부 = false;
    //        //}

    //    }
    //    catch (Exception ex)
    //    {
    //        MsgEnd(ex);
    //    }
    //}

    #endregion

    #region -> SetExchageMoney

    private void SetExchageMoney()
    {
        try
        {
            if (!_flexD.HasNormalRow) return;

            decimal ldb_exch = tb_NM_EXCH.DecimalValue; //환율
            decimal 부가세율 = tb_TAX.DecimalValue / 100;

            for (int i = _flexD.Rows.Fixed; i <= _flexD.Rows.Count - 1; i++)
            {
                decimal ldb_rateExchg = 1;       //발주량
                decimal ldb_umexp = 0;           // 단가
                부가세율 = 0.1m;

                ldb_rateExchg = _flexD.CDecimal(_flexD[i, "RT_PO"].ToString());      // 단위 환산량
                ldb_umexp = _flexD.CDecimal(_flexD[i, "UM_EX_PO"].ToString());       // 표현단가
                부가세율 = _flexD.CDecimal(tb_TAX.ClipText) / 100;                       // 부가세율

                if (ldb_rateExchg == 0) ldb_rateExchg = 1;

                if (tb_NM_EXCH.ClipText != "")      // 원화 단가 계산
                {
                    try
                    {
                        _flexD[i, "UM_P"] = ldb_umexp * ldb_exch;
                        _flexD[i, "UM"  ] = (ldb_umexp / ldb_rateExchg) * ldb_exch;
                        _flexD[i, "UM_EX"] = (ldb_umexp / ldb_rateExchg);
                    }
                    catch
                    {
                        _flexD[i, "UM_P"] = 0;
                        _flexD[i, "UM"] = 0;
                        _flexD[i, "UM_EX"] = 0;
                    }
                }
                else
                {
                    _flexD[i, "UM_P"] = 0;
                    _flexD[i, "UM"] = 0;
                    _flexD[i, "UM_EX"] = 0;
                }

                decimal ll_qtpo = 0;

                // 금액
                //				
                // 수배량에 의한 값 금액 계산
                if (ldb_rateExchg > 1 || ldb_rateExchg < 1)
                {
                    ll_qtpo = Unit.외화금액(DataDictionaryTypes.PU, _flexD.CDecimal(_flexD.CDecimal(_flexD[i, "QT_PO_MM"]) * ldb_umexp));
                }
                else // 발주량에 의한 값 결정
                {
                    ll_qtpo = Unit.외화금액(DataDictionaryTypes.PU, _flexD.CDecimal(_flexD.CDecimal(_flexD[i, "QT_PO"]) * _flexD.CDecimal(_flexD[i, "UM_EX"])));
                }

                _flexD[i, "AM_EX"] =   ll_qtpo;


                _flexD[i, "AM"] = Unit.원화금액(DataDictionaryTypes.PU, (ll_qtpo * ldb_exch));      // 원화금액

                decimal lb_amforvat = Unit.원화금액(DataDictionaryTypes.PU, ll_qtpo * ldb_exch);         //부가세 (원화금액기준)
                _flexD[i, "VAT"] = Unit.원화금액(DataDictionaryTypes.PU, lb_amforvat * 부가세율);
            }
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
        SUMFunction();
    }

    #endregion

    #region -> 환율 적용 함수
    private void SetExchageApply()
    {
        decimal ld_rate_base = 1;

        if (_header.CurrentRow == null || cbo_NM_EXCH.SelectedValue == null) return;

        if (_flexD.HasNormalRow) return;

        if (_header.CurrentRow.RowState != DataRowState.Unchanged)
        {
            if (tb_DT_PO.Text != string.Empty)
            {
                if (MA.기준환율.Option != MA.기준환율옵션.적용안함)
                    ld_rate_base = MA.기준환율적용(tb_DT_PO.Text, D.GetString(cbo_NM_EXCH.SelectedValue.ToString()));
                //ld_rate_base = _biz.ExchangeSearch(tb_DT_PO.Text, cbo_NM_EXCH.SelectedValue.ToString());

                if (D.GetString(cbo_NM_EXCH.SelectedValue.ToString()) == "000")
                    ld_rate_base = 1;
            }

            tb_NM_EXCH.Text = ld_rate_base.ToString();
            _header.CurrentRow["RT_EXCH"] = ld_rate_base;
        }


    }
    #endregion

    #region -> 라인 금액, 부가세 변경시 헤더의 금액과 부가세 세팅 구문(SUMFunction)

    /// <summary>
    /// 라인 금액, 부가세 변경시 헤더의 금액과 부가세 세팅 구문
    /// </summary>

    private void SUMFunction()
    {
        try
        {
            if (_flexD.HasNormalRow)
            {
                _header.CurrentRow["AM_EX"] = _flexD.DataTable.Compute("SUM(AM_EX)", "");
                _header.CurrentRow["AM"] = _flexD.DataTable.Compute("SUM(AM)", "");
                _header.CurrentRow["VAT"] = _flexD.DataTable.Compute("SUM(VAT)", "");

                if (m_tab_poh.TabPages.Contains(tabPage7))
                {
                    cur_AM_K_IV.DecimalValue = D.GetDecimal(_flexD.DataTable.Compute("SUM(AM)", ""));
                    cur_VAT_TAX_IV.DecimalValue = D.GetDecimal(_flexD.DataTable.Compute("SUM(VAT)", ""));
                    _header.CurrentRow["AM_EX_IV"] = D.GetDecimal(_flexD.DataTable.Compute("SUM(AM_EX)", ""));
                }
            }
            else
            {
                _header.CurrentRow["AM_EX"] = 0;
                _header.CurrentRow["AM"] = 0;
                _header.CurrentRow["VAT"] = 0;

                cur_AM_K_IV.DecimalValue = 0;
                cur_VAT_TAX_IV.DecimalValue = 0;
                _header.CurrentRow["AM_EX_IV"] = 0;
            }

        }
        catch
        {
            _header.CurrentRow["AM_EX"] = 0;
            _header.CurrentRow["AM"] = 0;
            _header.CurrentRow["VAT"] = 0;


            cur_AM_K_IV.DecimalValue = 0;
            cur_VAT_TAX_IV.DecimalValue = 0;
        }
    }

    #endregion

    #region -> 저장 후 오더 상태 변경(ChagePoState)

    /// <summary>
    /// 저장 후 오더 상태 변경
    /// 2010.02.08 savedata에서 다시 입력되는것 분석후 발견
    /// 우선 사용안되는것으로 막아놓음.... 
    /// 문제가 생기면 다시 살린다 -SMR
    /// </summary>

    private void ChagePoState()
    {
        try
        {
            string ps_R = "Release";
            string ps_S = "Start";
            string ps_C = "Close";
            bool isChaged = false;
            //FG_POCON 001 미확정 002 확정 003 마감

            for (int i = _flexD.Rows.Fixed; i <= _flexD.Rows.Count - 1; i++)
            {
                if (_flexD.RowState() == DataRowState.Added)
                {
                    if (_flexD[i, "YN_ORDER"].ToString() == "Y")        // 자동승인이면
                    {
                        _flexD[i, "NM_SYSDEF"] = ps_R;
                        _flexD[i, "FG_POCON"] = "001";
                    }
                    if (_flexD[i, "YN_REQ"].ToString() == "N")          // 의뢰가 존재하지 않으면 
                    {
                        _flexD[i, "NM_SYSDEF"] = ps_S;
                        _flexD[i, "FG_POCON"] = "002";
                        isChaged = true;

                        if (_flexD[i, "YN_AUTORCV"].ToString() == "Y")      // 자동 입고이면
                        {
                            _flexD[i, "NM_SYSDEF"] = ps_C;
                            _flexD[i, "FG_POCON"] = "003";
                        }
                    }
                }
            }

            if (isChaged)
            {
                SetHeadControlEnabled(false, 2);
                btn_RE_PR.Enabled = false;
                btn_RE_APP.Enabled = false;
                btn_RE_SO.Enabled = false;
                btn_so_gir.Enabled = false;
            }
            else
                SetHeadControlEnabled(false, 1);
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
    }

    #endregion

    #region -> _flex_DoubleClick

    void _flex_DoubleClick(object sender, EventArgs e)
    {
        try
        {
            if (!_flexD.HasNormalRow || _m_tppo_change =="001") return;
            if (_flexD["FG_POST"].ToString().Trim() != "O")
                return;

            if (_flexD.Cols[_flexD.Col].Name == "UM_EX_PO")
            {

                object[] obj = new object[]{
                                tb_NM_PARTNER.CodeValue,
                                tb_NM_PARTNER.CodeName,
                                tb_FG_PO_TR.CodeValue,
                                tb_FG_PO_TR.CodeName,
                                cbo_CD_PLANT.SelectedValue,
                                D.GetString(_flexD["CD_ITEM"]),
                                D.GetString(_flexD["NM_ITEM"]),
                                D.GetString(_flexD["CD_EXCH"])

                };


                P_PU_UM_HISTORY_SUB dlg = new P_PU_UM_HISTORY_SUB(obj);

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    decimal um_ex_po_sub = dlg.SUB_UM;

                    금액계산(_flexD.Row, um_ex_po_sub, D.GetDecimal(_flexD["QT_PO_MM"]), "UM_EX_PO", um_ex_po_sub);
                }
            }

        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
    }

    #endregion

    #region -> 그리드  메모기능 체크펜기능
    void _flexD_CellContentChanged(object sender, CellContentEventArgs e)
    {
        try
        {
            _biz.SaveContent(e.ContentType, e.CommandType, D.GetString(_flexD[e.Row, "NO_PO"]), D.GetDecimal(_flexD[e.Row, "NO_LINE"]), e.SettingValue);

        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
    }
    #endregion
   
    void _flexD_AfterEdit(object sender, RowColEventArgs e)
    {
        try
        {
            if (sPUSU == "100")
            {
                if (D.GetString(_flexD["CD_ITEM"]) == "")
                {
                    string filter = "NO_POLINE = " + _flexD.CDecimal(_flexD["NO_LINE"]) + "";

                    DataRow[] drs = _flexDD.DataTable.Select(filter);

                    foreach (DataRow row in drs)
                        row.Delete();
                }
            }

        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
    }

    #endregion

    #region -> 컨트롤이벤트관련

    #region -> 환율정보 선택완료시 발생 이벤트(cbo_NM_EXCH_SelectionChangeCommitted)

    private void cbo_NM_EXCH_SelectionChangeCommitted(object sender, System.EventArgs e)
    {
        try
        {
            if (D.GetString(cbo_NM_EXCH.SelectedValue) == "000")  // 원화
            {
                tb_NM_EXCH.DecimalValue = 1m;
                _header.CurrentRow["RT_EXCH"] = 1;
                tb_NM_EXCH.Enabled = false;
            }
            else
            {
                SetExchageApply();
                if (MA.기준환율.Option == MA.기준환율옵션.적용_수정불가)
                    tb_NM_EXCH.Enabled = false;
                else
                    tb_NM_EXCH.Enabled = true;


                if (_m_Company_only == "001")
                {
                    tb_NM_EXCH.DecimalValue = D.GetDecimal(Settings1.Default.RT_EXCH);
                    _header.CurrentRow["RT_EXCH"] = D.GetDecimal(Settings1.Default.RT_EXCH);
                }
            }

            if(Global.MainFrame.ServerKeyCommon.ToUpper() != "KORAVL")
                SetExchageMoney();
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
    }

    #endregion

    #region -> 환율정보 선택완료시 발생 이벤트(cbo_NM_EXCH_SelectionChangeCommitted)

    private void tb_NM_EXCH_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (Global.MainFrame.ServerKeyCommon.ToUpper() != "KORAVL")
                SetExchageMoney();
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
    }



    private void tb_NM_EXCH_Validated(object sender, EventArgs e)
    {
        try
        {
            if (Global.MainFrame.ServerKeyCommon.ToUpper() != "KORAVL")
                SetExchageMoney();
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }

    }

    #endregion





    #region -> 발주일변경시 발생 이벤트(tb_DT_PO_SelectionChangeCommitted)
    private void tb_DT_PO_DateChanged(object sender, System.EventArgs e)
    {
        //_header.CurrentRow["DT_PO"] = tb_DT_PO.Text;
        SetExchageApply();

    }
    #endregion

    #region -> 과세구분 선택완료시 발생 이벤트(cbo_FG_TAX_SelectionChangeCommitted)

    private void cbo_FG_TAX_SelectionChangeCommitted(object sender, System.EventArgs e)
    {
        try
        {
            decimal m_am = 1.0m;
            decimal m_vatrate = 0.1m;

            string strFG_TAX = "";
            if (cbo_FG_TAX.SelectedValue != null)
            {
                strFG_TAX = cbo_FG_TAX.SelectedValue.ToString();
            }

            부가세율(strFG_TAX);

            if (_flexD.HasNormalRow)
            {
                m_am = _flexD.CDecimal(tb_NM_EXCH.ClipText);
                m_vatrate = _flexD.CDecimal(tb_TAX.ClipText) / 100;

                for (int i = 0; i < _flexD.DataTable.Rows.Count; i++)
                {
                    try
                    {
                        decimal ll_qtpo = _flexD.CDecimal(_flexD.DataTable.Rows[i]["AM"]);
                        _flexD.DataTable.Rows[i]["VAT"] = Unit.원화금액(DataDictionaryTypes.PU, ll_qtpo * m_vatrate);
                    }
                    catch (Exception ex)
                    {
                        MsgEnd(ex);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }

        SUMFunction();
    }

    #endregion

    #region -> Control_KeyEvent

    private void Control_KeyEvent(object sender, System.Windows.Forms.KeyEventArgs e)
    {

        switch (((Control)sender).Name)
        {
            case "tb_DC":
            case "tb_DC_RMK2":
            case "cbo_TP_TRANSPORT":
            case "cbo_COND_PAY":
            case "txt_COND_PAY_DLV":
            case "cbo_COND_PRICE":
            case "txt_ARRIVER":
            case "txt_LOADING":
            case "txt_DC_RMK_TEXT":
            case "cbo_COND_SHIPMENT":
            case "cbo_FREIGHT_CHARGE":
            case "txt_DC_RMK_TEXT2":
            case "cbo_stnd_pay":
            case "txt_cond_days":
            case "cbo_origin":
                //  if (e.KeyData == Keys.Enter || e.KeyData == Keys.Tab)
                //추가_Click(null, null);
                비고수정여부 = true;
                ToolBarSaveButtonEnabled = true;
                //_header.JobMode =  JobModeEnum.추가후수정;

                if (((Control)sender).Name == "tb_DC" || ((Control)sender).Name == "tb_DC_RMK2")
                    SetHeadControlEnabled(false, 2);

                //tb_NO_PO.Enabled = false;
                //tb_NM_EXCH.Enabled = false;
                break;

            default:
                if (e.KeyData == Keys.Enter)
                    SendKeys.SendWait("{TAB}");
                break;
        }
    }

    #endregion

    #endregion

    public override void OnCallExistingPageMethod(object sender, Duzon.Common.Forms.PageEventArgs e)
    {
        object[] obj = e.Args;
        str발주번호 = D.GetString(obj[0]);


        InitPaint();
    }

    #region -> 품목입력시 해당품목의 외주BOM(SU_BOM)을 가져오는 구문

    private void GET_SU_BOM()
    {
        if (!_flexD.HasNormalRow) return;

        if (_flexDD.Enabled == false) return;

        DataTable dt = new DataTable();
        string filter = "NO_POLINE = " + _flexD.CDecimal(_flexD["NO_LINE"]) + "";

        dt = _biz.GET_SU_BOM(_header.CurrentRow["CD_PLANT"].ToString(), _header.CurrentRow["CD_PARTNER"].ToString(), _flexD["CD_ITEM"].ToString());



        //decimal dMaxLine = _flexD.GetMaxValue("NO_LINE");
        decimal dMaxLine = 0;
        foreach (DataRow dr in dt.Rows)
        {
            dr["QT_PO"] = _flexD.CDecimal(_flexD["QT_PO"]);
            dr["NO_PO"] = _flexD["NO_PO"].ToString();
            dr["NO_POLINE"] = _flexD.CDecimal(_flexD["NO_LINE"]);
            dr["NO_LINE"] = ++dMaxLine;
        }

        //품목을 변경했을 경우 해당 소요자재[사급자재]를 모두 지운다.
        for (int i = _flexDD.Rows.Count - _flexDD.Rows.Fixed; i >= _flexDD.Rows.Fixed; i--)
        {
            _flexDD.Rows.Remove(i);
        }

        _flexDD.BindingAdd(dt, filter, false);
        _flexD.DetailQueryNeed = false;
        사급자재변경구문(_flexD.CDecimal(_flexD["QT_PO"]));
    }

    #endregion

    #region -> 그리드 수량변경시 사급자재수량 변경해주는 구문

    private void 사급자재변경구문(decimal dPo)
    {
        //발주수량을 _flexM.CDecimal(_flexM["QT_PO"])에서 바로 읽어들이는 경우
        //Validate Check이벤트를 타고 왔을 때 1번째 행과 다음행부터 반영하는 값이 틀려짐.
        //첫번째 행은 변경하기전 값이 들어가고, 그 다음부터는 변경후의 값이 들어가게 됨.
        //따라서 인자로 발주수량값을 받아옴.
        for (int i = _flexDD.Rows.Fixed; i < _flexDD.Rows.Count; i++)
        {
            _flexDD[i, "QT_NEED"] = dPo != 0 ? System.Math.Round(dPo * _flexDD.CDecimal(_flexDD[i, "QT_NEED_UNIT"]), 4, MidpointRounding.AwayFromZero) : _flexDD.CDecimal(_flexDD[i, "QT_NEED_UNIT"]);
            _flexDD[i, "QT_LOSS"] = _flexDD.CDecimal(_flexDD[i, "QT_NEED"]) - _flexDD.CDecimal(_flexDD[i, "QT_REQ"]);
        }
    }

    #endregion

    #endregion

    #region ♣ 기타메소드

    #region -> Page_DataChanged

    void Page_DataChanged(object sender, EventArgs e)
    {
        try
        {
            ToolBarDeleteButtonEnabled = true;

            if (IsChanged())
                ToolBarSaveButtonEnabled = true;

        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
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

    #region ->기초값 설정
    /// <summary>
    /// 발주등록 시작단계에서 발주형태, 발주그룹 기초값 설정할때 발주형태에 따른 기본값 설정한다.
    /// 기본값 설정 2009.11.16 BY SMR (REQ:대림INS(컨설팅 허정민))
    /// </summary>
    /// <param name="str거래구분"></param>

    private void 기초값설정()
    {
        string strCD_PURGRP, strCD_TPPO;
        strCD_PURGRP = _header.CurrentRow["CD_PURGRP"].ToString();
        strCD_TPPO = _header.CurrentRow["CD_TPPO"].ToString();
        cbo_PAYment.SelectedValue = _header.CurrentRow["FG_PAYMENT"].ToString();

        Object[] obj = new Object[3];

        obj[0] = this.LoginInfo.CompanyCode;
        obj[1] = strCD_PURGRP;
        obj[2] = strCD_TPPO;



        DataSet ds = _biz.Get_TPPO_PURGRP(obj);
        DataRow dr = null;

        if (ds.Tables[0].Rows.Count > 0)
        {
            dr = ds.Tables[0].Rows[0];
            _header.CurrentRow["CD_PURGRP"] = strCD_PURGRP;
            tb_NM_PURGRP.SetCode(strCD_PURGRP, dr["NM_PURGRP"].ToString());
            // tb_FG_PO_TR.SetCode();
            //tb_NM_PURGRP.CodeValue = _header.CurrentRow["CD_PURGRP"].ToString();

            //구매그룹
            _header.CurrentRow["PURGRP_NO_TEL"] = dr["NO_TEL"].ToString();
            _header.CurrentRow["PURGRP_NO_FAX"] = dr["NO_FAX"].ToString();
            _header.CurrentRow["PURGRP_E_MAIL"] = dr["E_MAIL"].ToString();

            _header.CurrentRow["PO_PRICE"] = dr["PO_PRICE"].ToString();
            SetCC(0, strCD_PURGRP);  
        }

        if (ds.Tables[1].Rows.Count > 0)
        {
            dr = ds.Tables[1].Rows[0];

            _header.CurrentRow["CD_TPPO"] = strCD_TPPO;
            tb_FG_PO_TR.SetCode(strCD_TPPO, dr["NM_TPPO"].ToString());
            //발주유형
            _header.CurrentRow["FG_TRANS"] = dr["FG_TRANS"].ToString();
            _header.CurrentRow["FG_TPRCV"] = dr["FG_TPRCV"].ToString();
            _header.CurrentRow["FG_TPPURCHASE"] = dr["FG_TPPURCHASE"].ToString();
            _header.CurrentRow["YN_AUTORCV"] = dr["YN_AUTORCV"].ToString();
            _header.CurrentRow["YN_RCV"] = dr["YN_RCV"].ToString();
            _header.CurrentRow["YN_RETURN"] = dr["YN_RETURN"].ToString();
            _header.CurrentRow["YN_SUBCON"] = dr["YN_SUBCON"].ToString();
            _header.CurrentRow["YN_IMPORT"] = dr["YN_IMPORT"].ToString();
            _header.CurrentRow["YN_ORDER"] = dr["YN_ORDER"].ToString();
            _header.CurrentRow["YN_REQ"] = dr["YN_REQ"].ToString();
            _header.CurrentRow["YN_AM"] = dr["YN_AM"].ToString();
            _header.CurrentRow["NM_TRANS"] = dr["NM_TRANS"].ToString();
            _header.CurrentRow["FG_TAX"] = dr["FG_TAX"].ToString();
            _header.CurrentRow["TP_GR"] = dr["TP_GR"].ToString();

            _header.CurrentRow["CD_CC_TPPO"] = dr["CD_CC"].ToString();
            _header.CurrentRow["NM_CC_TPPO"] = _biz.GetCCCodeSearch(dr["CD_CC"].ToString());

            DataTable dt = _biz.GetYN_SU(D.GetString(_header.CurrentRow["CD_TPPO"]));
            if (D.GetString(dt.Rows[0]["YN_SU"]) == "Y")
                _flexDD.Enabled = true;
            else
                _flexDD.Enabled = false;

            거래구분(dr["FG_TRANS"].ToString());

            Setting_pu_poh_sub();

            curDe.DecimalValue = 0M;
            
        }
    }
    #endregion

    #region -> 발주 형태에 따른 값 설정

    #region -> 거래구분

    /// <summary>
    /// 발주형태 도움창에서 가져오는 발주형태의 거래구분값에 따른 컨트롤값 세팅 구문
    /// </summary>
    /// <param name="str거래구분"></param>

    private void 거래구분(string str거래구분)
    {
        if (str거래구분 == "001")
        {
            cbo_NM_EXCH.SelectedValue = "000";
            _header.CurrentRow["CD_EXCH"] = "000";
            cbo_NM_EXCH_SelectionChangeCommitted(null, null);

            //_header.CurrentRow["FG_TAX"] = "21";
            cbo_FG_TAX.Enabled = true;
            cbo_FG_TAX.SelectedValue = _header.CurrentRow["FG_TAX"];
            cbo_FG_TAX_SelectionChangeCommitted(null, null);
        }

        else if (str거래구분 == "002" || str거래구분 == "003")
        {
            cbo_NM_EXCH.SelectedValue = "000";
            _header.CurrentRow["CD_EXCH"] = "000";
            cbo_NM_EXCH_SelectionChangeCommitted(null, null);

            _header.CurrentRow["FG_TAX"] = "23";
            cbo_FG_TAX.Enabled = false;
            cbo_FG_TAX.SelectedValue = "23";
            cbo_FG_TAX_SelectionChangeCommitted(null, null);
        }
        else
        {
            //cbo_NM_EXCH.SelectedValue = "000";
            //_header.CurrentRow["CD_EXCH"] = "000";
            //cbo_NM_EXCH_SelectionChangeCommitted(null, null);

            cbo_NM_EXCH.SelectedValue = Settings1.Default.CD_EXCH;
            _header.CurrentRow["CD_EXCH"] = Settings1.Default.CD_EXCH;
            cbo_NM_EXCH_SelectionChangeCommitted(null, null);

            _header.CurrentRow["FG_TAX"] = "";
            cbo_FG_TAX.SelectedValue = "21";  //str거래구분 = '002','003' 후에 '004'로 변할때를 제어하기 위함..이유는 잘 모르겠음
            cbo_FG_TAX.SelectedValue = "";
            cbo_FG_TAX.Enabled = false;
            cbo_FG_TAX_SelectionChangeCommitted(null, null);
        }
    }

    #endregion

    #region -> 부가세율 (header의 과세구분이 변경되면 호출됨)

    private void 부가세율(string ps_taxp)
    {
        tb_TAX.Enabled = true;

        if (ps_taxp == "")
        {
            tb_TAX.Enabled = false;
            tb_TAX.DecimalValue = 0;
            _header.CurrentRow["VAT_RATE"] = 0;
        }
        else
        {
            /*2014.05.13 D20140509108 의제부가세 로직 변경
            s_vat_fictitious => 발주등록(공장)-의제부가세적용 000 : 부가세 0으로 처리, 100 : 부가세포함 로직으로 처리 */
            if (의제매입여부(ps_taxp) && s_vat_fictitious == "100")
            {
                _header.CurrentRow["TP_UM_TAX"] = "001";
                cbo_TP_TAX.SelectedValue = _header.CurrentRow["TP_UM_TAX"];
                cbo_TP_TAX.Enabled = false;
            }
            else
                cbo_TP_TAX.Enabled = true;
           
            if (의제매입여부(ps_taxp) && s_vat_fictitious == "000") 
            {
                tb_TAX.Enabled = false;
                tb_TAX.DecimalValue = 0;
                _header.CurrentRow["VAT_RATE"] = 0;
            }
            else
            {
                DataTable dt = (DataTable)cbo_FG_TAX.DataSource;
                DataRow[] row = dt.Select("CODE = '" + ps_taxp + "'");

                if (row != null && row.Length > 0)
                {
                    decimal dFG_VAT = _flexD.CDecimal(row[0]["CD_FLAG1"]);
                    tb_TAX.DecimalValue = dFG_VAT;
                    _header.CurrentRow["VAT_RATE"] = dFG_VAT;
                }
                else
                {
                    tb_TAX.Enabled = true;
                    tb_TAX.DecimalValue = 0;
                    _header.CurrentRow["VAT_RATE"] = 0;
                }
               
            }
        }
    }

    #endregion

    #endregion

    #region -> 품목정보구하기

    private void 품목정보구하기(object[] m_obj, string ls_app, int arg_row)
    {
        품목정보구하기(m_obj, ls_app, arg_row, null);
    }

    private void 품목정보구하기(object[] m_obj, string ls_app, int arg_row, object[] objSerial)
    {
        int apply_row = _flexD.Rows.Count - 1;
        decimal rate_vat = 0.0M;
        if (ls_app == "GRID") apply_row = arg_row;

        //_flexD[apply_row, "QT_POC"] = 0;
        //_flexD[apply_row, "QT_REQC"] = 0;
        _flexD[apply_row, "QT_INVC"] = 0;
        //_flexD[apply_row, "QT_ATPC"] = 0;

        DataSet ds = _biz.ItemInfo_Search(m_obj);

        object[] pi_obj = new object[] { Global.MainFrame.LoginInfo.CompanyCode, tb_DT_PO.Text.Substring(0, 4), D.GetString(cbo_CD_PLANT.SelectedValue), D.GetString(m_obj[0]), "" };
        DataTable dt_pinv = _biz.item_pinvn(pi_obj);

        _flexD[apply_row, "FG_TAX"] = _header.CurrentRow["FG_TAX"];
        _flexD[apply_row, "RATE_VAT"] = tb_TAX.DecimalValue;
        rate_vat = _flexD.CDecimal(tb_TAX.DecimalValue);

        if (ds != null && ds.Tables.Count > 3)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (m_sEnv_FG_TAX == "100")
                {
                    if (ds.Tables[0].Rows[0]["FG_TAX_PU"].ToString() != string.Empty)
                    {
                        _flexD[apply_row, "FG_TAX"] = ds.Tables[0].Rows[0]["FG_TAX_PU"];
                        _flexD[apply_row, "RATE_VAT"] = ds.Tables[0].Rows[0]["RATE_VAT"];
                        rate_vat = Convert.ToDecimal(ds.Tables[0].Rows[0]["RATE_VAT"]);
                    }
                }

                if (BASIC.GetMAEXC("발주등록(공장)-프로젝트별_의제매입세_구분") == "100" && D.GetString(ds.Tables[0].Rows[0]["CD_USERDEF14"]) == "001") //의제 매입 프로젝트별 적용
                {
                    string fg_tax_josun = _biz.pjt_item_josun(D.GetString(_flexD[apply_row, "CD_PJT"]));
                    if (fg_tax_josun != "")
                    {
                        _flexD[apply_row, "FG_TAX"] = fg_tax_josun; 
                        _flexD[apply_row, "RATE_VAT"] = 0;//부가세율은 0이 들어가면된다고함 (김광석사원요청) 2011-12-02
                        rate_vat = 0;
                    }
                }

               
               
                
                rate_vat = (rate_vat == 0) ? 0 : rate_vat / 100;

                if (ls_app == "GRID")
                {
                    apply_row = arg_row;
                    _flexD[apply_row, "RT_PO"] = _flexD.CDecimal(ds.Tables[0].Rows[0]["UNIT_PO_FACT"]);
                }

                if (_m_Company_only == "001") //아사히카세히 전용
                { 
                    AsahiKasei_Only_Item(apply_row, ds.Tables[0]); 
                }

                _flexD[apply_row, "GRP_ITEM"] = ds.Tables[0].Rows[0]["GRP_ITEM"];
                _flexD[apply_row, "NM_ITEMGRP"] = ds.Tables[0].Rows[0]["NM_ITEMGRP"];
                _flexD[apply_row, "STND_DETAIL_ITEM"] = ds.Tables[0].Rows[0]["STND_DETAIL_ITEM"];
                _flexD[apply_row, "NO_MODEL"] = ds.Tables[0].Rows[0]["NO_MODEL"];
                _flexD[apply_row, "CD_USERDEF14"] = ds.Tables[0].Rows[0]["CD_USERDEF14"]; //조선호텔 프로젝트별 의제매입세구분 사용품목 김광석사원요청(2011-12-02)
                _flexD[apply_row, "NM_MAKER"] = ds.Tables[0].Rows[0]["NM_MAKER"]; //조선호텔 프로젝트별 의제매입세구분 사용품목 김광석사원요청(2011-12-02)
                _flexD[apply_row, "MAT_ITEM"] = ds.Tables[0].Rows[0]["MAT_ITEM"];
                _flexD[apply_row, "NO_DESIGN"] = ds.Tables[0].Rows[0]["NO_DESIGN"];
                _flexD[apply_row, "EN_ITEM"] = ds.Tables[0].Rows[0]["EN_ITEM"];
                _flexD[apply_row, "GRP_MFG"] = ds.Tables[0].Rows[0]["GRP_MFG"];
                _flexD[apply_row, "NM_GRPMFG"] = ds.Tables[0].Rows[0]["NM_GRP_MFG"];

                if (_flexD.DataTable.Columns.Contains("FG_IQCL"))
                    _flexD[apply_row, "FG_IQCL"] = ds.Tables[0].Rows[0]["FG_IQCL"];
            }
            else
            {
                ShowMessage("품목정보를 확인하십시오(사용유무 확인 필수)");
                return;
            }

            if (((ls_app == "요청" && D.GetString(m_obj[8]) == "N") || ls_app == "GRID" || ls_app == "EXCEL" || ls_app == "H41"||ls_app=="BOM") && ds.Tables[1].Rows.Count > 0)
            {

                if (ls_app != "H41")//H41이 아닌경우에는 모두들어감
                {
                    _flexD[apply_row, "UM_EX_PO"] = _flexD.CDecimal(ds.Tables[1].Rows[0]["UM_ITEM"]);
                    if (_flexD.CDecimal(_flexD[apply_row, "RT_PO"]) == 0)
                    {
                        _flexD[apply_row, "UM_EX"] = _flexD.CDecimal(ds.Tables[1].Rows[0]["UM_ITEM"]);
                        _flexD[apply_row, "QT_PO"] = _flexD.CDouble(_flexD[apply_row, "QT_PO_MM"]);
                    }
                    else
                    {
                        _flexD[apply_row, "UM_EX"] = _flexD.CDecimal(ds.Tables[1].Rows[0]["UM_ITEM"]) / _flexD.CDecimal(_flexD[apply_row, "RT_PO"]);
                        _flexD[apply_row, "QT_PO"] = _flexD.CDouble(_flexD[apply_row, "QT_PO_MM"]) * _flexD.CDouble(_flexD[apply_row, "RT_PO"]);
                    }
                }

                _flexD[apply_row, "UM_P"] = _flexD.CDecimal(_flexD[apply_row, "UM_EX_PO"]) * tb_NM_EXCH.DecimalValue;
                _flexD[apply_row, "UM"] = _flexD.CDecimal(_flexD[apply_row, "UM_EX"]) * tb_NM_EXCH.DecimalValue; 
                _flexD[apply_row, "AM_EX"] = _flexD.CDecimal(_flexD[apply_row, "UM_EX_PO"]) * _flexD.CDecimal(_flexD[apply_row, "QT_PO_MM"]);
                 

                if (_m_Company_only == "001" && D.GetDecimal(_flexD[apply_row, "QT_AREA"]) != 0) //아사히카세이전용
                    _flexD[apply_row, "UM_EX_AR"] = D.GetDecimal(_flexD[apply_row, "UM_EX_PO"]) / D.GetDecimal(_flexD[apply_row, "QT_AREA"]);

                //주석걸려있던부분 신책임님께 물어보기
                //_flexD[apply_row, "AM"] = Unit.원화금액(DataDictionaryTypes.PU, _flexD.CDecimal(_flexD[apply_row, "AM_EX"]) * tb_NM_EXCH.DecimalValue);
                //_flexD[apply_row, "VAT"] = Unit.원화금액(DataDictionaryTypes.PU, _flexD.CDecimal(_flexD[apply_row, "AM"]) * rate_vat);
                //_flexD[apply_row, "AM_TOTAL"] = _flexD.CDecimal((_flexD[apply_row, "AM"])) + _flexD.CDecimal((_flexD[apply_row, "VAT"])); //총합계
            }

            _flexD[apply_row, "WEIGHT"] = ds.Tables[0].Rows[0]["WEIGHT"];
            if (!bStandard)
            {
                _flexD[apply_row, "QT_WEIGHT"] = _flexD.CDecimal(_flexD[apply_row, "QT_PO_MM"]) * _flexD.CDecimal(_flexD[apply_row, "WEIGHT"]);
            }
            else
            {
                if (MainFrameInterface.ServerKeyCommon == "SINJINSM" || MainFrameInterface.ServerKeyCommon == "DZSQL" || MainFrameInterface.ServerKeyCommon == "SQL_")
                {
                    if (D.GetDecimal(_flexD[apply_row, "UM_WEIGHT"]) > 0)
                        금액계산(apply_row, 0, D.GetDecimal(_flexD[apply_row, "QT_PO_MM"]), "", 0);
                }
            }
            

            //if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
            //{
            //    _flexD[apply_row, "QT_POC"] = ds.Tables[2].Rows[0]["QT_POC"]; //발주량
            //    _flexD[apply_row, "QT_REQC"] = ds.Tables[2].Rows[0]["QT_REQC"];//의뢰량

            //}
            if (dt_pinv != null && dt_pinv.Rows.Count > 0)
            {
                _flexD[apply_row, "QT_INVC"] = dt_pinv.Rows[0]["QT_INVC"]; //현재고
                _flexD[apply_row, "QT_ATPC"] = dt_pinv.Rows[0]["QT_ATPC"]; //가용재고
            }

            if (MainFrameInterface.ServerKeyCommon == "WOORIERP"  || MainFrameInterface.ServerKeyCommon == "SQL_")
            {
                if (ls_app != "요청" && ls_app != "품의")
                {
                    _flexD[arg_row, "NM_USERDEF1"] = ds.Tables[0].Rows[0]["NM_USERDEF1"];
                    _flexD[arg_row, "NM_USERDEF2"] = ds.Tables[0].Rows[0]["NM_USERDEF2"];
                }
            }
            else if (MainFrameInterface.ServerKeyCommon == "CARGOTEC")
            {
                _flexD[arg_row, "NM_USERDEF1"] = ds.Tables[0].Rows[0]["NM_USERDEF1"];

            }

        }

        SetQtValue(apply_row);
    }

    #endregion

    #region -> SetQtValue

    private void SetQtValue(int li_index)
    {
        if (li_index < 0) return;

        //tb_QT_PO.DecimalValue = _flexD.CDecimal(_flexD[li_index, "QT_POC"]);
        //tb_QT_REQ.DecimalValue = _flexD.CDecimal(_flexD[li_index, "QT_REQC"]);
        tb_QT_INV.DecimalValue = _flexD.CDecimal(_flexD[li_index, "QT_INVC"]);
        //tb_QT_ATP.DecimalValue = _flexD.CDecimal(_flexD[li_index, "QT_ATPC"]);
    }

    private void GetQtValue(int li_index)
    {
        if (li_index < 0) return;
        DataTable dt_pinv = null;

        if (D.GetDecimal(_flexD[li_index, "QT_INVC"]) == 0)
        {
            object[] pi_obj = new object[] { Global.MainFrame.LoginInfo.CompanyCode, tb_DT_PO.Text.Substring(0, 4), D.GetString(cbo_CD_PLANT.SelectedValue), D.GetString(_flexD[li_index, "CD_ITEM"]), D.GetString(_flexD[li_index, "CD_SL"]) };
            dt_pinv = _biz.item_pinvn(pi_obj);
        }

        //if (dt_pinv != null && dt_pinv.Rows.Count > 0)
        //{
        //    _flexD[li_index, "QT_INVC"] = dt_pinv.Rows[0]["QT_INVC"]; //현재고
        //}

        SetQtValue(li_index);

    }

    #endregion

    #region -> 메세지

    private DialogResult ShowMessage(메세지 msg, params object[] paras)
    {
        switch (msg)
        {
            case 메세지.거래구분이국내일때만자동의뢰및입고행위가가능합니다:
                return ShowMessage("PU_M000121");              // 이미 전표처리된 건입니다.
            case 메세지.공장을먼저선택하십시오:
                return ShowMessage("PU_M000070");
            case 메세지.삭제할수없습니다:
                return ShowMessage("MA_M000094");
        }

        return DialogResult.None;
    }

    #endregion

    #region -> CC적용
    //2009.09.29 라인에 CD_CC 추가 ( HEADER의 구매그룹의 CD_CC가 DEFAULT (tb_NM_PURGRP)
    public void SetCC(int row, String arg_cd_purgrp)
    {
        DataTable dt_cc = null;


        if (row == 0)   // header쪽에서 호출
        {
            if (arg_cd_purgrp == string.Empty)
            {
                _header.CurrentRow["CD_CC_PURGRP"] = string.Empty;
                _header.CurrentRow["NM_CC_PURGRP"] = string.Empty;
                return;
            }

            dt_cc = _biz.GetCD_CC_CodeSearch(arg_cd_purgrp);
            if (dt_cc != null && dt_cc.Rows.Count > 0)
            {
                _header.CurrentRow["CD_CC_PURGRP"] = dt_cc.Rows[0]["CD_CC"].ToString();
                _header.CurrentRow["NM_CC_PURGRP"] = dt_cc.Rows[0]["NM_CC"].ToString();
            }
            return;
        }

        if (m_sEnv_CC == "100")
        {
            _flexD[row, "CD_CC"] = _header.CurrentRow["CD_CC_TPPO"];
            _flexD[row, "NM_CC"] = _header.CurrentRow["NM_CC_TPPO"];
        }
        else if (m_sEnv_CC == "200" && D.GetString(_flexD[row,"CD_PJT"]) != string.Empty)
        {
            dt_cc = _biz.GetCD_CC_CodeSearch_pjt(D.GetString(_flexD[row, "CD_PJT"]));
            if (dt_cc != null && dt_cc.Rows.Count > 0)
            {
                _flexD[row, "CD_CC"] = D.GetString(dt_cc.Rows[0]["CD_CC"]);
                _flexD[row, "NM_CC"] = D.GetString(dt_cc.Rows[0]["NM_CC"]);
            }

        }
        else if (m_sEnv_CC == "300" && D.GetString(_flexD[row, "CD_ITEM"]) != string.Empty)
        {
            dt_cc = _biz.GetCD_CC_CodeSearch_cd_item(D.GetString(_flexD[row, "CD_ITEM"]), D.GetString(cbo_CD_PLANT.SelectedValue));
            if (dt_cc != null && dt_cc.Rows.Count > 0)
            {
                _flexD[row, "CD_CC"] = D.GetString(dt_cc.Rows[0]["CD_CC"]);
                _flexD[row, "NM_CC"] = D.GetString(dt_cc.Rows[0]["NM_CC"]);
            }

        }
        else
        {
            _flexD[row, "CD_CC"] = _header.CurrentRow["CD_CC_PURGRP"];
            _flexD[row, "NM_CC"] = _header.CurrentRow["NM_CC_PURGRP"];
        }
        

    }

    // 요청적용이나 품의적용일경우에만...
    // 2009.12.08 사용안하게 될듯.... 
    // 이후에 다시 요청할 수도 있으니 그냥 놔둔다...
    public void SetCC_Line(int row, String arg_cd_purgrp)
    {

        DataTable dt_cc = null;
        // 구매그룹환경설정에서 요청적용 cc 적용이면(요청적용에서만 arg_cd_purgrp값을 넣어준다 )
        if (m_sEnv_CC_Line == "Y" && arg_cd_purgrp != string.Empty)
        {
            dt_cc = _biz.GetCD_CC_CodeSearch(arg_cd_purgrp);
            if (dt_cc != null && dt_cc.Rows.Count > 0)
            {
                _flexD[row, "CD_CC"] = dt_cc.Rows[0]["CD_CC"].ToString();
                _flexD[row, "NM_CC"] = dt_cc.Rows[0]["NM_CC"].ToString();
            }
        }
        else
        {
            _flexD[row, "CD_CC"] = _header.CurrentRow["CD_CC_PURGRP"];
            _flexD[row, "NM_CC"] = _header.CurrentRow["NM_CC_PURGRP"];
        }


    }
    #endregion

    #region -> 금액계산 <-- 금액을 결정짓는 컬럼이 변경되었을때.. (같은 계산의 코딩이 많아서 수정함)

    /// <summary>
    /// 엑셀업로드 후 수량, 단가, 금액 계산 구문
    /// </summary>
    /// <param name="dUM_EX">단가</param>
    /// 2009.11.19 금액계산하는 여러군데 있어서 금액에 영향을 미치는 컬럼이 수정될때마다
    /// 여러곳을 수정해야해서 만든 함수다... 차츰 이것으로 바꿔간다.. - SMR
    void 금액계산(int row, Decimal 단가, Decimal 수량, string p_call, Decimal p_newValue) 
    {
        _flexD.Row = row;

        decimal 부가세율 = 0.1M;
        decimal 환율 = 1;  // 환율
        decimal ldb_VatKr = 0 ,ldb_AmKr = 0, ldb_amEx = 0 , ldb_Am =0;
        decimal ldb_um;


        if (D.GetDecimal(_flexD["QT_PO_MM"]) == 0 && p_call != "")
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, "발주수량");
                return;

            }
      

        // 단위 환산량
        decimal ldb_rateExchg = _flexD.CDecimal(_flexD["RT_PO"]);
        if (ldb_rateExchg == 0)
            ldb_rateExchg = 1;

        if (tb_NM_EXCH.DecimalValue != 0)                           // 환율
            환율 = tb_NM_EXCH.DecimalValue;


        ldb_um = 단가; 

       
        if (_header.CurrentRow["FG_TRANS"].ToString() != "001" || _flexD["FG_TAX"].ToString() == string.Empty)
            부가세율 = Unit.환율(DataDictionaryTypes.PU,tb_TAX.DecimalValue / 100);
        else
            부가세율 = Unit.환율(DataDictionaryTypes.PU,_flexD.CDecimal(_flexD["RATE_VAT"]) == 0 ? 0 : _flexD.CDecimal(_flexD["RATE_VAT"]) / 100);
           // 부가세율 = 부가세율 / 100;

        if (p_call == "AM_EX")
        {
            ldb_amEx  = Unit.외화금액(DataDictionaryTypes.PU,p_newValue);// / 수량;  
            ldb_AmKr  = Unit.원화금액(DataDictionaryTypes.PU,p_newValue * 환율) ;
            ldb_VatKr = Unit.환율(DataDictionaryTypes.PU,ldb_AmKr * 부가세율) ; 

            _flexD["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU,ldb_amEx / 수량); // _flexD.CDecimal(_flexD[row,"AM_EX"]) / 수량;
            _flexD["UM_EX"]  = Unit.외화단가(DataDictionaryTypes.PU,(_flexD.CDecimal(_flexD["QT_PO"]) == 0) ? 0 : (D.GetDecimal(_flexD["AM_EX"]) / (_flexD.CDecimal(_flexD["QT_PO"]))));  // 외화 재고단위 단가                
             
            _flexD["UM"]     = Unit.원화단가(DataDictionaryTypes.PU,(ldb_amEx / (수량 * ldb_rateExchg)) * 환율);
            _flexD["UM_P"]   = Unit.원화단가(DataDictionaryTypes.PU,(ldb_amEx / 수량) * 환율);
             
        }
        else
        {
            if (bStandard)
            {
                if (Global.MainFrame.ServerKey == "SINJINSM" || Global.MainFrame.ServerKey == "SQL_108" || Global.MainFrame.ServerKey == "DZSQL")
                {
                    if (D.GetDecimal(_flexD[row, "UM_WEIGHT"]) != 0)
                    {
                        calcAM(row);
                        ldb_um = D.GetDecimal(_flexD[row, "UM_EX_PO"]);
                        단가 = ldb_um;

                    }
                }
            }

            if ( D.GetString(_flexD["TP_UM_TAX"])  =="001") //부가세포함
            {                    
                //ldb_VatKr = Unit.원화금액(DataDictionaryTypes.PU, System.Math.Round((수량 * 환율 * ldb_um) / (1 + 부가세율), 9) * 부가세율);  //부가세구하기
                //ldb_AmKr = (수량 * 환율 * ldb_um) - ldb_VatKr;  // 원화금액   
                //ldb_amEx = ldb_AmKr;// 부가세 포함일경우 외화금액은 의미가 없슴     
                if (Global.MainFrame.LoginInfo.CompanyLanguage != Language.KR)
                    ldb_Am = Unit.원화금액(DataDictionaryTypes.PU, (수량 * 단가 * 환율)); //총금액: 무조건 반올림
                else
                    ldb_Am    = Decimal.Round((수량 * 단가 * 환율), MidpointRounding.AwayFromZero); //총금액: 무조건 반올림
                
                 if (Global.MainFrame.ServerKeyCommon.ToUpper() == "KFL")  //한국화스너 무조건 반올림 이정운대리 D20110711082
                 {
                     ldb_VatKr = decimal.Ceiling((ldb_Am / (1 + 부가세율) * 부가세율));
                     ldb_AmKr  = decimal.Ceiling((ldb_Am - ldb_VatKr));
                    
                 }
                 else
                 {
                     if (의제매입여부(D.GetString(_flexD["FG_TAX"])) && s_vat_fictitious == "100")
                     {
                         ldb_VatKr = Unit.원화금액(DataDictionaryTypes.PU, (ldb_Am * 부가세율));
                     }
                     else
                     {
                         if (Global.MainFrame.LoginInfo.CompanyLanguage != Language.KR)
                             ldb_VatKr = Unit.원화금액(DataDictionaryTypes.PU, (ldb_Am / (1 + 부가세율) * 부가세율));
                         else
                             ldb_VatKr = Decimal.Round((ldb_Am / (1 + 부가세율) * 부가세율), MidpointRounding.AwayFromZero);
                     }
                     ldb_AmKr  = Unit.원화금액(DataDictionaryTypes.PU, (ldb_Am - ldb_VatKr)); 
                 }  
                 ldb_amEx  =  Unit.외화금액(DataDictionaryTypes.PU, (ldb_AmKr / 환율)) ;  // 외화금액  
            }
            else  //부가세별도
            {
                ldb_amEx = Unit.외화금액(DataDictionaryTypes.PU, (수량 * ldb_um));//외화
                ldb_AmKr = Unit.원화금액(DataDictionaryTypes.PU, (ldb_amEx * 환율));   //원화
     

                if (Global.MainFrame.ServerKeyCommon.ToUpper() == "KFL")  //한국화스너 무조건 반올림 이정운대리 D20110711082
                {
                    ldb_VatKr = decimal.Ceiling(ldb_AmKr * 부가세율);
                    ldb_AmKr =  decimal.Ceiling(ldb_AmKr ) ; 
                }
                else
                {
                    ldb_VatKr = Unit.원화금액(DataDictionaryTypes.PU, (ldb_AmKr * 부가세율));
                    ldb_AmKr  = Unit.원화금액(DataDictionaryTypes.PU, (ldb_AmKr));  
                }  
            }
            _flexD["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU,단가); // _flexD.CDecimal(_flexD[row,"AM_EX"]) / 수량; 
            _flexD["UM_P"] = Unit.원화단가(DataDictionaryTypes.PU,단가 * 환율);
            _flexD["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU,(단가 / ldb_rateExchg));  // 외화 재고단위 단가    
            _flexD["UM"] = Unit.원화단가(DataDictionaryTypes.PU,(단가 / ldb_rateExchg) * 환율);


        } 
        _flexD["AM_EX"] =  Unit.외화금액(DataDictionaryTypes.PU,ldb_amEx);  //금액                
        _flexD["QT_PO"] =  Unit.수량(DataDictionaryTypes.PU,수량 * ldb_rateExchg); //_flexD.CDecimal(_flexD[row, "RT_PO"]);  // 수량(재고단위) 
        _flexD["AM"]    =  ldb_AmKr;
        _flexD["VAT"]   =  ldb_VatKr; 
        _flexD["AM_TOTAL"] = ldb_AmKr + ldb_VatKr;// _flexD[row, "AM_TOTAL"] = ldb_AmKr + ldb_VatKr; // 총합계 


        


        if (Global.MainFrame.ServerKeyCommon.ToUpper() == "ICDERPU")
        {
            _flexD["AM_PRE"] = D.GetDecimal(_flexD["UM_PRE"]) * D.GetDecimal(_flexD["QT_PO"]);
        }

        SUMFunction();

        //_flexD["UM_EX_PO"] = ldb_amEx / 수량; // _flexD.CDecimal(_flexD[row,"AM_EX"]) / 수량;
        //_flexD["UM_EX"] = (_flexD.CDecimal(_flexD["QT_PO"]) == 0) ? 0 : (D.GetDecimal(_flexD["AM_EX"]) / (_flexD.CDecimal(_flexD["QT_PO"])));  // 외화 재고단위 단가                
        
        //if (tb_NM_EXCH.DecimalValue != 0)   //  원화 단가 계산
        //{
        //    _flexD["UM"] =   (ldb_amEx / (수량 * ldb_rateExchg)) * 환율;
        //    _flexD["UM_P"] = (ldb_amEx / 수량) * 환율;
        //}
        //else
        //{
        //    _flexD["UM"] = 0;
        //    _flexD["UM_P"] = 0;
        //} 

        #region-> old data
        // decimal 부가세율 = 0.1M;
        // decimal 환율 = 1;  // 환율
        //// decimal 단가 = 0;
        // decimal ldb_VatKr = 0;
        // decimal ldb_AmKr = 0;
        // decimal ldb_amEx = 0;
        // decimal ldb_um;

        // // 단위 환산량
        // decimal ldb_rateExchg = _flexD.CDecimal(_flexD[row, "RT_PO"]);
        // if (ldb_rateExchg == 0)
        //     ldb_rateExchg = 1;

        // if (tb_NM_EXCH.DecimalValue != 0)                           // 환율
        //     환율 = tb_NM_EXCH.DecimalValue;

        // ldb_um = 단가;


        // 부가세율 = _flexD.CDecimal(_flexD[row, "RATE_VAT"]);
        // if (_header.CurrentRow["FG_TRANS"].ToString() != "001" || _flexD[row, "FG_TAX"].ToString() == string.Empty)
        //     부가세율 = tb_TAX.DecimalValue / 100;
        // else
        //     부가세율 = 부가세율 / 100;

        // if (p_call == "AM_EX" )
        // {
        //     ldb_AmKr = Math.Floor(p_newValue * 환율);
        //     ldb_VatKr = Math.Floor(ldb_AmKr * 부가세율);
        //     ldb_amEx = p_newValue;// / 수량;
        // }
        // else
        // {
        //     if (cbo_TP_TAX.SelectedValue.ToString() == "001") //부가세포함
        //     {
        //         ldb_VatKr = _flexD.CDecimal(Math.Floor(System.Math.Round((수량 * 환율 * ldb_um) / (1 + 부가세율), 9) * 부가세율));
        //         ldb_AmKr = _flexD.CDecimal(Math.Floor(수량 * 환율 * ldb_um - ldb_VatKr));
        //         ldb_amEx = ldb_AmKr;// 부가세 포함일경우 외화금액은 의미가 없슴

        //     }
        //     else
        //     {
        //         ldb_VatKr = _flexD.CDecimal(Math.Floor(수량 * 환율 * 부가세율 * ldb_um));
        //         ldb_AmKr = _flexD.CDecimal(Math.Floor(수량 * 환율 * ldb_um));
        //         ldb_amEx = Convert.ToDecimal(수량 * ldb_um);
        //     }


        //     _flexD[row,"AM_EX"] = ldb_amEx;  //금액
        //     _flexD[row, "UM_EX"] = ldb_amEx / (수량 * ldb_rateExchg);  // 외화 재고단위 단가                
        //     _flexD[row, "QT_PO"] = _flexD.CDecimal(수량) * ldb_rateExchg; //_flexD.CDecimal(_flexD[row, "RT_PO"]);  // 재고수량
        // }

        // _flexD[row, "AM"] = ldb_AmKr;
        // _flexD[row, "VAT"] = ldb_VatKr;
        // _flexD[row, "AM_TOTAL"] = ldb_AmKr + ldb_VatKr; // 총합계
        // _flexD[row, "UM_EX_PO"] = ldb_amEx / 수량; // _flexD.CDecimal(_flexD[row,"AM_EX"]) / 수량;



        // if (tb_NM_EXCH.DecimalValue != 0)   //  원화 단가 계산
        // {
        //     _flexD[row, "UM"] = (ldb_amEx / (수량 * ldb_rateExchg)) * 환율;
        //     _flexD[row, "UM_P"] = (ldb_amEx / 수량) * 환율;
        // }
        // else
        // {
        //     _flexD[row, "UM"] = 0;
        //     _flexD[row, "UM_P"] = 0;
        // }

        // SUMFunction();
        #endregion
    }
     
    #endregion


    #region -> 부가세 계산, 총합계 계산 (적용등에서 사용, 금액계산 메소드에서는 사용안함)
    private void 부가세계산( DataRow row )
    {
        Decimal ldb_VatKr = 0, ldb_AmKr = 0, ldb_amEx = 0 , ldb_AM =0;   // Decimal ldb_UMkr = 0, ldb_UMEX = 0;
        String  ls_FG_TAX = string.Empty;             //과세구분 
        Decimal rate_vat =  D.GetDecimal(row["RATE_VAT"]) == 0 ? 0 : D.GetDecimal(row["RATE_VAT"])  / 100;  //과세율  
         
        Decimal 수량 =  D.GetDecimal(row["QT_PO_MM"]); 
        Decimal 단가 =  D.GetDecimal(row["UM_EX_PO"]);
        Decimal 환율 =  D.GetDecimal(_header.CurrentRow["RT_EXCH"]) == 0 ? 1 : D.GetDecimal(_header.CurrentRow["RT_EXCH"]);

        if (수량 == 0)
            return; 
 
        if (D.GetString( row["TP_UM_TAX"]) == "001")  //부가세포함
        { 
            /* 총금액     : 반올림( 수량 * 단가 * 환율)  
             * 부가세     : 반올림( 총금액 / (1 + 과세율) * 과세율 )  
             * 원화금액   : 총금액 - 부가세     
             * 외화금액   : 원화금액  /  환율       
            */
            if (Global.MainFrame.LoginInfo.CompanyLanguage != Language.KR)
                ldb_AM    = Unit.원화금액(DataDictionaryTypes.PU, 수량 * 단가 * 환율 ); //총금액 
            else
                ldb_AM    = Decimal.Round( 수량 * 단가 * 환율, MidpointRounding.AwayFromZero );  ; //총금액 
            if (s_vat_fictitious == "100")
                ldb_VatKr = Unit.원화금액(DataDictionaryTypes.PU, ldb_AM * rate_vat);
            else
            {
                if (Global.MainFrame.LoginInfo.CompanyLanguage != Language.KR)
                    ldb_VatKr = Unit.원화금액(DataDictionaryTypes.PU, ldb_AM / (1 + rate_vat) * rate_vat); //부가세   
                else
                    ldb_VatKr = Decimal.Round(ldb_AM / (1 + rate_vat) * rate_vat, MidpointRounding.AwayFromZero); //부가세   
            }
            ldb_AmKr  = Unit.원화금액(DataDictionaryTypes.PU, ldb_AM - ldb_VatKr);   //원화금액   
            ldb_amEx  = Unit.외화금액(DataDictionaryTypes.PU, ldb_AmKr / 환율);  // 외화금액 
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
            ldb_amEx =  Unit.외화금액(DataDictionaryTypes.PU, 수량 * 단가);  // 외화금액
            ldb_AmKr  = Unit.원화금액(DataDictionaryTypes.PU, 수량 * 단가 * 환율);   //원화금액  
            ldb_VatKr = Unit.원화금액(DataDictionaryTypes.PU, ldb_AmKr * rate_vat); //부가세   
            ldb_AM    = Unit.원화금액(DataDictionaryTypes.PU, ldb_AmKr + ldb_VatKr);   //총금액  
          
             
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
        row["UM_EX_PO"] =  Unit.외화단가(DataDictionaryTypes.PU,단가);                                                              //외화단가(발주단위)   
        row["UM_P"]     = Unit.원화단가(DataDictionaryTypes.PU,단가 * 환율);
        row["UM_EX"]    = Unit.외화단가(DataDictionaryTypes.PU,단가 / ((D.GetDecimal(row["RT_PO"]) == 0) ? 1 : D.GetDecimal(row["RT_PO"])));    //(D.GetDecimal(row["QT_PO"]) == 0) ? 0 : (단가 / D.GetDecimal(row["QT_PO"]));  //  외화단가( 재고단위)     
        row["UM"]       = Unit.원화단가(DataDictionaryTypes.PU,단가 / ((D.GetDecimal(row["RT_PO"]) == 0) ? 1 : D.GetDecimal(row["RT_PO"]))  * 환율);    //원화단가                                               
   

        row["AM_EX"]  = ldb_amEx;
        row["AM"]     = ldb_AmKr;
        row["VAT"]    = ldb_VatKr;
        row["AM_TOTAL"] = ldb_AmKr + ldb_VatKr;//_flexD.CDecimal(_flexD["AM"]) + _flexD.CDecimal(_flexD["VAT"]);
        SUMFunction();
        //Decimal ldb_VatKr = 0, ldb_AmKr = 0, ldb_amEx = 0;

        //Decimal rate_vat = D.GetDecimal(_flexD["RATE_VAT"]) / 100; 
     
        //Decimal 수량 = D.GetDecimal(_flexD["QT_PO_MM"]);
        //Decimal 단가 = D.GetDecimal(_flexD["UM_EX_PO"]);

        //ldb_amEx = D.GetDecimal(_flexD["AM_EX"]);
        //ldb_AmKr = Unit.원화금액(DataDictionaryTypes.PU, ldb_amEx * tb_NM_EXCH.DecimalValue);
        //if (cbo_TP_TAX.SelectedValue.ToString() == "001") //부가세포함
        //{
        //    ldb_VatKr = Unit.원화금액(DataDictionaryTypes.PU, System.Math.Round(D.GetDecimal(ldb_AmKr) / (1 + rate_vat), 9) * rate_vat);  //부가세구하기                              
        //    ldb_AmKr = ldb_AmKr - ldb_VatKr;
        //    ldb_amEx = ldb_AmKr;  //부가세 포함일경우 외화금액은 의미가 없슴

        //    _flexD["UM_EX_PO"] = ldb_amEx / 수량; // _flexD.CDecimal(_flexD[row,"AM_EX"]) / 수량;
        //    _flexD["UM_P"] = _flexD["UM_EX_PO"];

        //    _flexD["UM_EX"] = (D.GetDecimal(_flexD["QT_PO"]) == 0) ? 0 : (ldb_amEx / D.GetDecimal(_flexD["QT_PO"]));  // 외화 재고단위 단가                
        //    _flexD["UM"] = _flexD["UM_EX"];

        //}
        //else
        //{
        //    ldb_VatKr = Unit.원화금액(DataDictionaryTypes.PU, ldb_AmKr * rate_vat);
        //}
        //_flexD["AM_EX"] = ldb_amEx;
        //_flexD["AM"] = ldb_AmKr;
        //_flexD["VAT"] = ldb_VatKr;
        ////총합계
        //_flexD["AM_TOTAL"] = ldb_AmKr + ldb_VatKr;//_flexD.CDecimal(_flexD["AM"]) + _flexD.CDecimal(_flexD["VAT"]);
    }

    #endregion

    #region -> 규격형 ROW일 경우 대중소분류 CHECK
    private int checkCLS()
    {

        for (int idx = 2; idx <= _flexD.Rows.Count; idx++)
        {
            if (D.GetDecimal(_flexD[idx, "UM_WEIGHT"]) > 0)
            {
                if (D.GetString(_flexD[idx, "CLS_L"]) == "" || D.GetString(_flexD[idx, "CLS_M"]) == "" || D.GetString(_flexD[idx, "CLS_S"]) == "")
                {
                    ShowMessage("입력되지 않은 대중소분류가 존재합니다.");
                    return idx;
                }

            }
        }
        return 0;

    }
    #endregion

    #region -> 규격형 ROW일 경우 품목군 CHECK
    private int checkITEMGRP()
    {

        for (int idx = 2; idx <= _flexD.Rows.Count; idx++)
        {
            if (D.GetDecimal(_flexD[idx, "UM_WEIGHT"]) > 0)
            {
                if (D.GetString(_flexD[idx, "GRP_ITEM"]) == "")
                {
                    ShowMessage("입력되지 않은 품목군이 존재합니다.");
                    return idx;
                }

            }
        }

        return 0;

    }
    #endregion

    private void 부가세만계산()
    {
        //Decimal rate_vat  = D.GetDecimal(_header.CurrentRow["VAT_RATE"]) / 100;
        //_flexD["VAT"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flexD["AM"]) * rate_vat);
        Decimal rate_vat = D.GetDecimal(_flexD["RATE_VAT"]) == 0 ? 0 : D.GetDecimal(_flexD["RATE_VAT"]) / 100;  //과세율   
        Decimal 환율     = D.GetDecimal(_header.CurrentRow["RT_EXCH"]) == 0 ? 1 : D.GetDecimal(_header.CurrentRow["RT_EXCH"]);

        _flexD["VAT"]      = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flexD["AM"]) * rate_vat * 환율);
        _flexD["AM_TOTAL"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flexD["AM"]) + D.GetDecimal(_flexD["VAT"]));

    }

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

    #region 컨트롤 Enabled(m_pnlHeader_Enabled = false)

    private void m_pnlHeader_Enabled()
    {
        tb_NO_PO.Enabled = false;
        tb_DT_PO.Enabled = false;
        tb_NM_PARTNER.Enabled = false;
        cbo_CD_PLANT.Enabled = false;
        tb_NM_PURGRP.Enabled = false;
        tb_NO_EMP.Enabled = false;
        tb_FG_PO_TR.Enabled = false;
        cbo_NM_EXCH.Enabled = false;
        tb_NM_EXCH.Enabled = false;
        cbo_FG_UM.Enabled = false;
        cbo_PAYment.Enabled = false;
        cbo_FG_TAX.Enabled = false;
        cbo_TP_TAX.Enabled = false;
        ctx프로젝트.Enabled = false;
        txt오더번호.Enabled = false;
        bp_CDSL.Enabled = false;
        btn_SL_apply.Enabled = false;

        tb_NO_PO_MH.Enabled = false;
       // tb_DT_LIMIT.Enabled = false;
    }

    #endregion

    #region -> 요청 품의 거래처 적용
    private bool Partner_Accept(DataTable dt)
    {
        int partner_count = 0;
        string cd_partner_prapp = string.Empty;
        string nm_partner_prapp = string.Empty;

        string[] filter_Grp = new string[] { "CD_PARTNER", "LN_PARTNER" };
        DataTable Groupby_dt = ComFunc.getGridGroupBy(dt, filter_Grp, true);


        foreach (DataRow dr in Groupby_dt.Rows)
        {
            if (D.GetString(dr["CD_PARTNER"]) != "")
            {
                partner_count++;
                cd_partner_prapp = D.GetString(dr["CD_PARTNER"]);
                nm_partner_prapp = D.GetString(dr["LN_PARTNER"]);
            }

        }

        if (partner_count != 1 && D.GetString(tb_NM_PARTNER.CodeValue) == "")
        {
            ShowMessage("거래처가 발주등록과 적용할 자료에서 모두 존재하지 않습니다.");
            return false;
        }
        else if (partner_count == 1)
        {
            _header.CurrentRow["CD_PARTNER"] = cd_partner_prapp;
            _header.CurrentRow["LN_PARTNER"] = nm_partner_prapp;


            tb_NM_PARTNER.CodeValue = cd_partner_prapp;
            tb_NM_PARTNER.CodeName = nm_partner_prapp;
        

        }
        return true;
    }
    #endregion

    #region -> 요청 품의 거래처 적용
    private bool Exch_Accept(DataTable dt)
    {
        int partner_count = 0;
        string cd_partner_prapp = string.Empty;
        string nm_partner_prapp = string.Empty;
        string cd_exch_prapp = string.Empty;
        string rt_exch_prapp = string.Empty;

        string[] filter_Grp = new string[] { "CD_PARTNER", "LN_PARTNER", "CD_EXCH", "RT_EXCH" };
        DataTable Groupby_dt = ComFunc.getGridGroupBy(dt, filter_Grp, true);


        foreach (DataRow dr in Groupby_dt.Rows)
        {
            if (D.GetString(dr["CD_PARTNER"]) != "" && D.GetString(dr["CD_EXCH"]) != "" && D.GetString(dr["RT_EXCH"]) != "")
            {
                partner_count++;
                cd_partner_prapp = D.GetString(dr["CD_PARTNER"]);
                nm_partner_prapp = D.GetString(dr["LN_PARTNER"]);
                cd_exch_prapp = D.GetString(dr["CD_EXCH"]);
                rt_exch_prapp = D.GetString(dr["RT_EXCH"]);
            }
            else
            {
                ShowMessage(DD("환정보or거래처가 입력되지 않았습니다."));
                return false;
            }
        }

        if (partner_count != 1 && D.GetString(tb_NM_PARTNER.CodeValue) == "")
        {
            ShowMessage("거래처가 발주등록과 적용할 자료에서 모두 존재하지 않습니다.");
            return false;
        }
        else if (partner_count == 1)
        {
            _header.CurrentRow["CD_PARTNER"] = cd_partner_prapp;
            _header.CurrentRow["LN_PARTNER"] = nm_partner_prapp;
            _header.CurrentRow["CD_EXCH"] = cd_exch_prapp;
            _header.CurrentRow["RT_EXCH"] = rt_exch_prapp;

            tb_NM_PARTNER.CodeValue = cd_partner_prapp;
            tb_NM_PARTNER.CodeName = nm_partner_prapp;
            cbo_NM_EXCH.SelectedValue = cd_exch_prapp;
            tb_NM_EXCH.Text = rt_exch_prapp;


        }
        return true;
    }
    #endregion

    #region ->품의 발주형태 적용
    private bool Tppo_Accept(DataTable dt, string strGubon)
    {
        int Tppo_count = 0;
        string cd_Tppo_prapp = string.Empty;
        string nm_Tppo_prapp = string.Empty;

        string[] filter_Grp = new string[] { "CD_TPPO", "NM_TPPO" };
        DataTable Groupby_dt = ComFunc.getGridGroupBy(dt, filter_Grp, true);


        foreach (DataRow dr in Groupby_dt.Rows)
        {
            if (D.GetString(dr["CD_TPPO"]) != "")
            {
                Tppo_count++;
                cd_Tppo_prapp = D.GetString(dr["CD_TPPO"]);
                nm_Tppo_prapp = D.GetString(dr["NM_TPPO"]);
            }

        }

        if (Tppo_count != 1 && D.GetString(tb_FG_PO_TR.CodeValue) == "")
        {
            ShowMessage(DD("발주형태가 중복 또는 없습니다."));
            return false;
        }
        else if (Tppo_count != 1 && strGubon == "전용")
        {
            ShowMessage(DD("발주형태가 중복 또는 없습니다."));
            return false;
        }
        else if (Tppo_count == 1)
        {
            _header.CurrentRow["CD_TPPO"] = cd_Tppo_prapp;
            _header.CurrentRow["NM_TPPO"] = nm_Tppo_prapp;


            tb_FG_PO_TR.CodeValue = cd_Tppo_prapp;
            tb_FG_PO_TR.CodeName = nm_Tppo_prapp;

            DataRow dr = BASIC.GetTPPO(cd_Tppo_prapp);

            _header.CurrentRow["FG_TRANS"] = dr["FG_TRANS"];
            _header.CurrentRow["FG_TPRCV"] = dr["FG_TPRCV"];
            _header.CurrentRow["FG_TPPURCHASE"] = dr["FG_TPPURCHASE"];
            _header.CurrentRow["YN_AUTORCV"] = dr["YN_AUTORCV"];
            _header.CurrentRow["YN_RCV"] = dr["YN_RCV"];
            _header.CurrentRow["YN_RETURN"] = dr["YN_RETURN"];
            _header.CurrentRow["YN_SUBCON"] = dr["YN_SUBCON"];
            _header.CurrentRow["YN_IMPORT"] = dr["YN_IMPORT"];
            _header.CurrentRow["YN_ORDER"] = dr["YN_ORDER"];
            _header.CurrentRow["YN_REQ"] = dr["YN_REQ"];
            _header.CurrentRow["YN_AM"] = dr["YN_AM"];
            _header.CurrentRow["NM_TRANS"] = dr["NM_TRANS"];
            _header.CurrentRow["FG_TAX"] = dr["FG_TAX"];
            _header.CurrentRow["TP_GR"] = dr["TP_GR"];

            _header.CurrentRow["CD_CC_TPPO"] = dr["CD_CC"];
            _header.CurrentRow["NM_CC_TPPO"] = dr["NM_CC"];
            거래구분(dr["FG_TRANS"].ToString());

            if (D.GetString(dr["YN_SU"]) == "Y")
                _flexDD.Enabled = true;
            else
                _flexDD.Enabled = false;

            Setting_pu_poh_sub();

            if (m_tab_poh.TabPages.Contains(tabPage7))
            {
                dtp_DT_DUE_IV.Text = Global.MainFrame.GetStringToday;
                dtp_DT_PAY_PRE_IV.Text = Global.MainFrame.GetStringToday;
                dtp_DT_PROCESS_IV.Text = Global.MainFrame.GetStringToday;
                cbo_FG_PAYBILL_IV.SelectedValue = "";
                cbo_CD_DOCU_IV.SelectedValue = "";
            }

        }
        return true;
    }
    #endregion

    public void CalcRebate(Decimal p_qt_mm, Decimal p_um_rebate)
    {
        if (!_YN_REBATE) return; 
        // 리베이트계산
        _flexD["AM_REBATE_EX"] = Unit.외화금액(DataDictionaryTypes.PU, p_um_rebate * p_qt_mm);
        _flexD["AM_REBATE"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flexD["AM_REBATE_EX"]) * D.GetDecimal(tb_NM_EXCH.Text));
 
    }

    #region -> 원그리드 필수값적용
    private void setNecessaryCondition(object[] obj, OneGrid _OneGrid, bool state)
    {
        try
        {

            //bool state = true;
            System.Collections.Generic.List<Control> list = _OneGrid.GetControlList();

            for (int i = 0; i < list.Count; i++)
            {

                if (list[i].GetType().Name == "BpPanelControl")
                {
                    BpPanelControl bp = (BpPanelControl)list[i];

                    if (!state)
                    {

                        for (int j = 0; j < obj.Length; j++)

                            if (bp.Name != D.GetString(obj[j]))
                            {
                                bp.IsNecessaryCondition = !state;
                            }
                            else
                            {
                                bp.IsNecessaryCondition = state;
                                break;
                            }
                    }
                    else
                    {
                        for (int j = 0; j < obj.Length; j++)

                            if (bp.Name != D.GetString(obj[j]))
                            {
                                bp.IsNecessaryCondition = state;
                            }
                            else
                            {
                                bp.IsNecessaryCondition = !state;
                                break;
                            }
                    }

                    if (obj.Length == 0)
                        bp.IsNecessaryCondition = true;
                }
            }
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
    }

    #endregion

    #region -> 토페스전용
    void btnH_Click(object sender, EventArgs e)
    {
        try
        {
            Control ctrl = (Control)sender;

            if (ctrl.Name == "btnHadd")
            {
                _flexH.Rows.Add();
                _flexH.Row = _flexH.Rows.Count - 1;

                decimal MaxSeq = _flexH.Rows.Count - _flexH.Rows.Fixed;

                _flexH["SQ_1"] = MaxSeq;
                _flexH["CD_PLANT"] = D.GetString(_header.CurrentRow["CD_PLANT"]);
                _flexH["NO_PO"] = D.GetString(_header.CurrentRow["NO_PO"]);

                MaxSeq++;
                _flexH.AddFinished();
                _flexH.Col = _flexH.Cols.Fixed;
                _flexH.Focus();
            }
            else
            {
                if (_flexH.HasNormalRow)
                    _flexH.Rows.Remove(_flexH.Row);
            }
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
    }

    void _flexH_ValidateEdit(object sender, ValidateEditEventArgs e)
    {
        try
        {
            FlexGrid _flex = sender as FlexGrid;
            decimal 발주총액 = D.GetDecimal(_header.CurrentRow["AM"]);

            if (발주총액 == 0)
                return;


            string oldValue = ((FlexGrid)sender).GetData(e.Row, e.Col).ToString();
            string newValue = ((FlexGrid)sender).EditData;

            if (oldValue.ToUpper() == newValue.ToUpper())
                return;

            bool 국내유무 = D.GetString(_header.CurrentRow["FG_TRANS"]) == "001" ? true : false;

            switch (_flexH.Cols[e.Col].Name)
            {
                case "RT_IV":
                    _flexH["AM"] = 발주총액 - (발주총액 * D.GetDecimal(newValue) / 100M);
                    if (국내유무)
                        _flexH["VAT"] = D.GetDecimal(_flexH["AM"]) * 0.1m;

                    _flexH["AM_HAP"] = D.GetDecimal(_flexH["AM"]) + D.GetDecimal(_flexH["VAT"]);
                    break;
                case "AM":
                    _flexH["RT_IV"] = (D.GetDecimal(newValue) / 발주총액) * 100M;
                    if (국내유무)
                        _flexH["VAT"] = D.GetDecimal(_flexH["AM"]) * 0.1m;

                    _flexH["AM_HAP"] = D.GetDecimal(_flexH["AM"]) + D.GetDecimal(_flexH["VAT"]);
                    break;

                case "RT_BAN":
                    _flexH["AM_BAN"] = 발주총액 - (발주총액 * D.GetDecimal(newValue) / 100M);

                    if (국내유무)
                        _flexH["AM_BANK"] = D.GetDecimal(_flexH["AM_BAN"]) * 1.1m;
                    else
                        _flexH["AM_BANK"] = D.GetDecimal(_flexH["AM_BAN"]);

                    break;
                case "AM_BAN":
                    _flexH["RT_BAN"] = (D.GetDecimal(newValue) / 발주총액) * 100M;

                    if (국내유무)
                        _flexH["AM_BANK"] = D.GetDecimal(newValue) * 1.1m;
                    else
                        _flexH["AM_BANK"] = D.GetDecimal(newValue);
                    break;

                default:
                    break;

            }


        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
    }
    #endregion

    #region -> 소분류코드 약칭
    string getCLS_S_code(string cls_s_code, string CD_ITEM, string CD_PLANT)
    {
        string s_code = cls_s_code;
        DataTable dt = null;

        if (s_code == string.Empty)
            return "";

        dt = _biz.Check_PITEM(CD_ITEM, CD_PLANT, s_code);

        if (dt.Rows.Count > 0)
            return "";


        switch (s_code)
        {
            //case "SL":
            //    s_code = "L";
            //    break;
            //case "SJ":
            //    s_code = "J";
            //    break;
            //case "SA":
            //    s_code = "A";
            //    break;
            case "SC":
                s_code = "M";
                break;
            //case "SW":
            //    s_code = "W";
            //    break;
            //case "SS":
            //    s_code = "S";
            //    break;
            //case "ST":
            //    s_code = "T";
            //    break;
            default:
                s_code = s_code.Substring(1, 1);
                break;



        }
        return s_code;

    }
    #endregion

    #endregion

    #region ♣ 엑셀기능

    #region -> 마우스 우클릭 엑셀양식

    private void EXCEL_Popup(object sender, EventArgs e)
    {
        try
        {
            ToolStripMenuItem tsMenuItem = sender as ToolStripMenuItem;
            if (tsMenuItem == null) return;

            if (tsMenuItem.Name == DD("파일생성"))
            {
                if (_header.JobMode != JobModeEnum.조회후수정) return;

                _flexD.ExportToExcel(true, true, true);
            }
           
        }
        catch (Exception ex)
        {
            _flexD.Redraw = true;
            MsgEnd(ex);
        }
    }

    #endregion

    #region -> 엑셀업로드_Click

    #region -> 엑셀업로드_Check
    //CD_PLANT	CD_PARTNER	CD_TPPO	FG_UM	CD_PJT	CD_ITEM	QT_PO	CD_EXCH	DC50_PO
    private bool Chk_ExcelData(DataTable dt_Excel)
    {
        string[] str_col = new string[3] { "CD_ITEM", "DT_LIMIT", "QT_PO_MM" };

        for (int i = 0; i < str_col.Length; i++)
        {
            if (!dt_Excel.Columns.Contains(str_col[i]))
            {
                ShowMessage("컬럼명 [" + str_col[i] + "] 이 엑셀에 존재하지 않습니다.");
                return false;
            }
        }

        return true;
    }
    #endregion

    #region -> 엑셀업로드_Check
    // 프로젝트가 포함되어 있으면 프로젝트(마스터) 유효성 검사
    private DataTable Get_ExcelData_PJT(DataTable dt_Excel)
    {
        string cd_pjt_pipe = string.Empty;
        if (dt_Excel.Columns.Contains("CD_PJT"))
        {
            DataTable dt_pjt_grp = dt_Excel.DefaultView.ToTable(true, "CD_PJT");

            foreach (DataRow drSplit in dt_pjt_grp.Rows)
            {
                cd_pjt_pipe += drSplit["CD_PJT"] + "|";
            }
        }


        string[] No_PK_Multi_array_PJT = D.StringConvert.GetPipes(cd_pjt_pipe, 200);

        DataTable dt_temp = null;
        DataTable dt_pjt = null;
        for (int j = 0; j < No_PK_Multi_array_PJT.Length; j++)
        {
            dt_temp = _biz.Get_PJTInfo(No_PK_Multi_array_PJT[j]);

            if (dt_temp != null && dt_temp.Rows.Count > 0)
            {
                if (dt_pjt == null)
                    dt_pjt = dt_temp.Clone();

                dt_pjt.Merge(dt_temp);
            }
        }

        return dt_pjt;
    }
    #endregion

    #region -> 엑셀업로드_pjt Check
    private Boolean ChkData_PJT(String p_cd_pjt, ref string p_nm_pjt)
    {
        string str_nm_pjt = string.Empty;
        if (p_cd_pjt.Trim() == string.Empty) return true;
        if (_dt_pjt == null || _dt_pjt.Rows.Count < 1) return false;

        DataRow[] drs = _dt_pjt.Select("NO_PROJECT = '" + p_cd_pjt + "'");

        if (drs == null || drs.Length < 1) return false;

        p_nm_pjt = D.GetString(drs[0]["NM_PROJECT"]);

        return true;
    }
    #endregion


    private void 엑셀업로드_Click(object sender, EventArgs e)
    {
        try
        {
            if (!HeaderCheck(0)) return;     //필수항목 검사

            Duzon.Common.Util.Excel excel = null;

            string 멀티품목코드 = string.Empty;
            string 품목코드 = string.Empty;
            //string MULTINOPR = string.Empty;
            //bool bExistNoPr = false;

            decimal 수량합계 = 0; decimal 재고수량합계 = 0; decimal 원화금액 = 0; decimal 금액합계 = 0; decimal 부가세 = 0;
            // 20110322 아이큐브개발팀 김현철 추가
            decimal 총금액 = 0;
            // 20110322 아이큐브개발팀 김현철 추가
            string DT_LIMIT = string.Empty; string ls_app = string.Empty;
            decimal rate_vat = 0.0M;

            //decimal MaxSeq = _flexD.GetMaxValue("NO_LINE");
            //MaxSeq++;

            DataTable _dt_EXCEL = null;
            OpenFileDialog m_FileDlg = new OpenFileDialog();

            m_FileDlg.Filter = "엑셀 파일 (*.xls)|*.xls";

            if (m_FileDlg.ShowDialog() == DialogResult.OK)
            {
                호출여부 = true;

                string FileName = m_FileDlg.FileName;
                excel = new Duzon.Common.Util.Excel();
                DataTable dt_EXCEL = excel.StartLoadExcel(FileName);

                if (!Chk_ExcelData(dt_EXCEL)) return;
                _dt_EXCEL = dt_EXCEL.Clone();

                _dt_EXCEL.Columns["CD_ITEM"].DataType = typeof(string);

                foreach (DataRow dr in dt_EXCEL.Rows)
                {
                    _dt_EXCEL.Rows.Add(dr.ItemArray);
                }

                foreach (DataRow row in _dt_EXCEL.Rows)
                {
                    if (row["CD_ITEM"].ToString() == string.Empty) continue;
                    if (품목코드 != D.GetString(row["CD_ITEM"]).ToUpper() )
                    {
                        멀티품목코드 += D.GetString(row["CD_ITEM"]).ToUpper() + "|";
                        품목코드      = D.GetString(row["CD_ITEM"]).ToUpper();
                    }
                    
                }

                /* DB에서 품목에 관련한 데이타 가지고 옴 */
                string 공장 = cbo_CD_PLANT.SelectedValue == null ? "" : cbo_CD_PLANT.SelectedValue.ToString();

                if (공장 == "")
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, lb_NM_PLANT.Text);
                    return;
                }

                bool 검증여부 = false;
                bool 품목적합 = false;
                string 적합품목 = string.Empty;
                string 적합거래처품목 = string.Empty;

                StringBuilder 검증리스트 = new StringBuilder();

                string msg = "품목코드\t 품목명\t";
                검증리스트.AppendLine(msg);
                msg = "-".PadRight(80, '-');
                검증리스트.AppendLine(msg);



                DataTable 엑셀dt = _biz.엑셀(_dt_EXCEL);
                DataTable dt공장품목 = _biz.공장품목(멀티품목코드, cbo_CD_PLANT.SelectedValue.ToString());

                DataRow NewRow;

                _flexD.Redraw = false;

                // pjt check
                _dt_pjt = Get_ExcelData_PJT(엑셀dt);
                Boolean bExist_Pjt = true;
              
                StringBuilder 검증리스트_PJT = new StringBuilder();

                #region -> 검증리스트_pjt 초기값
                string msgpjt = ".".PadRight(80, ' ');
                검증리스트_PJT.AppendLine(msgpjt);
                검증리스트_PJT.AppendLine(msgpjt);

                msgpjt = "프로젝트코드\t 프로젝트명\t";
                검증리스트_PJT.AppendLine(msgpjt);

                msgpjt = "-".PadRight(80, '-');
                검증리스트_PJT.AppendLine(msg);



                //seq_false_list
                StringBuilder 검증리스트_PJT_SEQ = new StringBuilder();
                string msgpjt_seq = ".".PadRight(80, ' ');
                검증리스트_PJT_SEQ.AppendLine(msgpjt_seq);
                검증리스트_PJT_SEQ.AppendLine(msgpjt_seq);

                msgpjt_seq = "프로젝트코드\t 프로젝트명\t UNIT항번";
                검증리스트_PJT_SEQ.AppendLine(msgpjt_seq);

                msgpjt_seq = "-".PadRight(80, '-');
                검증리스트_PJT_SEQ.AppendLine(msgpjt_seq);

                bool b_seq_project = false;
                bool seq_false_list = true;

                //////////////////////////////////////////////
                #endregion -> 검증리스트_pjt 초기값


                /* DB에 가져온 Data를 엑셀에 매칭하는 작업& 그리드에 매칭하는 작업 */
                foreach (DataRow row in 엑셀dt.Rows)
                {
                    if (row["CD_ITEM"] == null || row["CD_ITEM"].ToString().Trim() == string.Empty) { continue; }

                    품목적합 = false;
                    foreach (DataRow drItem in dt공장품목.Rows)
                    {
                        if (D.GetString(row["CD_ITEM"]).ToUpper() == drItem["CD_ITEM"].ToString().ToUpper().Trim())
                        {
                            품목적합 = true;
                            적합품목 = drItem["CD_ITEM"].ToString().Trim();
                            break;
                        }
                    }

                    if (품목적합 == true)
                    {
                        DataRow[] drs = dt공장품목.Select("CD_ITEM = '" + 적합품목 + "'");

                        NewRow = _flexD.DataTable.NewRow();
                        NewRow["CD_ITEM"] = D.GetString(row["CD_ITEM"]).ToUpper();
                        NewRow["NM_ITEM"] = drs[0]["NM_ITEM"];
                        NewRow["STND_ITEM"] = drs[0]["STND_ITEM"];
                        NewRow["UNIT_IM"] = drs[0]["UNIT_IM"];
                        NewRow["CD_UNIT_MM"] = drs[0]["UNIT_PO"];
                        NewRow["NO_PO"] = tb_NO_PO.Text;
                        NewRow["NO_LINE"] = 최대차수 + 1;
                        NewRow["CD_PLANT"] = cbo_CD_PLANT.SelectedValue.ToString();
                        NewRow["NM_SYSDEF"] = _ComfirmState;
                        NewRow["NM_GRPMFG"] = drs[0]["NM_GRPMFG"];
                        NewRow["GRP_MFG"] = drs[0]["GRP_MFG"];
                        NewRow["EN_ITEM"] = drs[0]["EN_ITEM"];

                        //품목CC
                        if (m_sEnv_CC == "300")
                        {
                            NewRow["CD_CC"] = drs[0]["CD_CC"];
                            NewRow["NM_CC"] = drs[0]["NM_CC"];
                        }

                        if (엑셀dt.Columns.Contains("CD_PJT") && D.GetString(row["CD_PJT"]) != "")
                        {
                            if (_dt_pjt != null && _dt_pjt.Rows.Count >0) //프로젝트 컬럼 존재시
                            {
                                DataRow[] dr_pjt = _dt_pjt.Select("NO_PROJECT = '" + D.GetString(row["CD_PJT"]) + "'");

                                if (dr_pjt == null || dr_pjt.Length == 0)
                                {
                                    NewRow["CD_PJT"] = string.Empty; // 2011.04.07 수정(엑셀데이터 우선적용) //txt_NoProject.Text;
                                    NewRow["NM_PJT"] = string.Empty;
                                    bExist_Pjt = false;
                                    msgpjt = row["CD_PJT"].ToString().PadRight(15, ' ');
                                    검증리스트_PJT.AppendLine(msgpjt);
                                }
                                else
                                {
                                    b_seq_project = 엑셀dt.Columns.Contains("SEQ_PROJECT");

                                    NewRow["CD_PJT"] = D.GetString(row["CD_PJT"]);
                                    NewRow["NM_PJT"] = dr_pjt[0]["NM_PJT"];

                                    DataRow[] dr_seq = null;

                                    if (Config.MA_ENV.YN_UNIT == "Y" && b_seq_project)
                                    {

                                        dr_seq = _dt_pjt.Select("SEQ_PROJECT = '" + D.GetString(row["SEQ_PROJECT"]) + "' AND NO_PROJECT ='" + D.GetString(row["CD_PJT"]) + "'");
                                        if (dr_seq == null || dr_seq.Length == 0)
                                        {
                                            msgpjt_seq = row["CD_PJT"].ToString().PadRight(15, ' ');
                                            msgpjt_seq += dr_pjt[0]["NM_PJT"].ToString().PadRight(15, ' ');
                                            msgpjt_seq += row["SEQ_PROJECT"].ToString().PadRight(15, ' ');

                                            검증리스트_PJT_SEQ.AppendLine(msgpjt_seq);
                                            seq_false_list = false;

                                        }
                                        else
                                        {
                                            NewRow["SEQ_PROJECT"] = row["SEQ_PROJECT"];
                                            NewRow["CD_PJT_ITEM"] = dr_pjt[0]["CD_PJT_ITEM"];
                                            NewRow["NM_PJT_ITEM"] = dr_pjt[0]["NM_PJT_ITEM"];
                                            NewRow["PJT_ITEM_STND"] = dr_pjt[0]["PJT_ITEM_STND"];
                                        }

                                    }
  
                                }
       
                            }

                            //if (ChkData_PJT(D.GetString(row["CD_PJT"]), ref str_nm_pjt))
                            //{
                            //    NewRow["CD_PJT"] = D.GetString(row["CD_PJT"]); // 2011.04.07 수정(엑셀데이터 우선적용) //txt_NoProject.Text;
                            //    NewRow["NM_PJT"] = str_nm_pjt;
                            //}
                            //else
                            //{
                            //    NewRow["CD_PJT"] = string.Empty; // 2011.04.07 수정(엑셀데이터 우선적용) //txt_NoProject.Text;
                            //    NewRow["NM_PJT"] = string.Empty;
                            //    bExist_Pjt = false;
                            //    msgpjt = row["CD_PJT"].ToString().PadRight(15, ' ');
                            //    검증리스트_PJT.AppendLine(msgpjt);
                            //}
                        }

                        if (_dt_EXCEL.Columns.Contains("NO_PR") && D.GetString(row["NO_PR"]) != "")
                        {
                            NewRow["NO_PR"] = row["NO_PR"];
                            NewRow["NO_PRLINE"] = row["NO_PRLINE"];
                        }
                        else
                        {
                            NewRow["NO_PR"] = "";
                        }

                        if (_dt_EXCEL.Columns.Contains("FG_PACKING") && D.GetString(row["FG_PACKING"]) != "")
                            NewRow["FG_PACKING"] = row["FG_PACKING"];
                        else
                            NewRow["FG_PACKING"] = "";

                        if (_dt_EXCEL.Columns.Contains("NM_USERDEF2") && D.GetString(row["NM_USERDEF2"]) != "")
                            NewRow["NM_USERDEF2"] = row["NM_USERDEF2"];
                        else
                            NewRow["NM_USERDEF2"] = "";


                        //NewRow["DT_LIMIT"] = Global.MainFrame.GetStringToday;

                        if (D.StringDate.IsValidDate(D.GetString(row["DT_LIMIT"]), true, "yyyymmdd") == false) return;
                        NewRow["DT_LIMIT"] = D.GetString(row["DT_LIMIT"]);
                        NewRow["DT_PLAN"] = D.GetString(row["DT_LIMIT"]);  //2011.07.26 추가 PIMS : M20110725166

                        //if (D.StringDate.IsValidDate(D.GetString(row["DT_PLAN"]), false, "yyyymmdd") == false) return; // 2011.04.07 수정

                        NewRow["AM_EX"] = row["AM_EX"];
                        NewRow["RT_PO"] = drs[0]["UNIT_PO_FACT"].ToString() == "" ? 1 : _flexD.CDecimal(drs[0]["UNIT_PO_FACT"]);
                        NewRow["QT_PO_MM"] = row["QT_PO_MM"].ToString() == "" ? 0 : _flexD.CDecimal(row["QT_PO_MM"]);
                        NewRow["UM_EX_PO"] = row["UM_EX_PO"].ToString() == "" ? 0 : _flexD.CDecimal(row["UM_EX_PO"]);

                        NewRow["QT_PO"] = _flexD.CDouble(NewRow["QT_PO_MM"]);

                        if (dt_EXCEL.Columns.Contains("DC1"))
                            NewRow["DC1"] = row["DC1"];
                        if (dt_EXCEL.Columns.Contains("DC2"))
                            NewRow["DC2"] = row["DC2"];
                        
                        NewRow["QT_PO"] = row["QT_PO"].ToString() == "" ? 0 : _flexD.CDecimal(row["QT_PO"]);
                        NewRow["CD_PLANT"]  = cbo_CD_PLANT.SelectedValue.ToString();
                        NewRow["FG_TAX"]    = _header.CurrentRow["FG_TAX"];
                        NewRow["TP_UM_TAX"] = _header.CurrentRow["TP_UM_TAX"];

                        NewRow["RATE_VAT"] = tb_TAX.DecimalValue;
                        rate_vat = _flexD.CDecimal(tb_TAX.DecimalValue);

                        NewRow["AM"] = 0;
                        NewRow["VAT"] = 0;
                        NewRow["UM_EX"] = 0;

                        NewRow["NM_CLS_ITEM"] = drs[0]["NM_CLS_ITEM"];

                        object[] m_obj = new object[8];
                        m_obj[0] = D.GetString(row["CD_ITEM"]).ToUpper();
                        m_obj[1] = cbo_CD_PLANT.SelectedValue.ToString();
                        m_obj[2] = Global.MainFrame.LoginInfo.CompanyCode;
                        m_obj[3] = cbo_FG_UM.SelectedValue.ToString();
                        m_obj[4] = cbo_NM_EXCH.SelectedValue.ToString();
                        m_obj[5] = tb_DT_PO.Text;
                        m_obj[6] = tb_NM_PARTNER.CodeValue;
                        m_obj[7] = tb_NM_PURGRP.CodeValue;

                        //NewRow["QT_POC"] = 0;
                        //NewRow["QT_REQC"] = 0;
                        NewRow["QT_INVC"] = 0;
                        //NewRow["QT_ATPC"] = 0;

                        #region 수량(QT_PO_MM) * 단가(UM_EX_PO)에 대한 계산로직
                        /// EXCEL에서 수량은 필수
                        /// 금액이 0 이면 수량 * 단가
                        /// 단가가 0 이면 금액 / 수량

                        if (NewRow["CD_ITEM"].ToString() == "") continue;

                        if (_flexD.CDecimal(NewRow["UM_EX_PO"]) == 0 && Math.Floor(_flexD.CDecimal(NewRow["AM_EX"])) != 0)
                        {
                            NewRow["UM_EX_PO"] = _flexD.CDecimal(NewRow["AM_EX"]) / _flexD.CDecimal(NewRow["QT_PO_MM"]);
                        }
                        else if (_flexD.CDecimal(NewRow["UM_EX_PO"]) != 0 && Math.Floor(_flexD.CDecimal(NewRow["AM_EX"])) == 0)
                        {
                            NewRow["AM_EX"] = _flexD.CDecimal(NewRow["UM_EX_PO"]) * _flexD.CDecimal(NewRow["QT_PO_MM"]);
                        }

                        if (_flexD.CDecimal(NewRow["RT_PO"]) == 0)
                        {
                            NewRow["UM_EX"] = _flexD.CDecimal(NewRow["UM_EX_PO"]);
                        }
                        else
                        {
                            NewRow["UM_EX"] = _flexD.CDecimal(NewRow["UM_EX_PO"]) / _flexD.CDecimal(NewRow["RT_PO"]);
                            NewRow["QT_PO"] = _flexD.CDouble(NewRow["QT_PO_MM"]) * _flexD.CDouble(NewRow["RT_PO"]);
                        }


                        NewRow["UM_P"] = _flexD.CDecimal(NewRow["UM_EX_PO"]) * tb_NM_EXCH.DecimalValue;
                        NewRow["UM"] = _flexD.CDecimal(NewRow["UM_EX"]) * tb_NM_EXCH.DecimalValue; 

                        //NewRow["AM"] = Unit.원화금액(DataDictionaryTypes.PU, _flexD.CDecimal(NewRow["AM_EX"]) * tb_NM_EXCH.DecimalValue);
                        //NewRow["VAT"] = Unit.원화금액(DataDictionaryTypes.PU, _flexD.CDecimal(NewRow["AM"]) * (_flexD.CDecimal(tb_TAX.DecimalValue) / 100));


                        #endregion

                        #region 수량(QT_PO_MM) * 단가(UM_EX_PO)에 대한 계산로직  <- 예전버젼(추후 지워질...)

                        //if (NewRow["CD_ITEM"].ToString() == "") return;

                        //decimal dQT_PO_MM = row["QT_PO_MM"].ToString() == "" ? 0 : _flexD.CDecimal(row["QT_PO_MM"]);

                        ////decimal ldb_qt = 0;
                        //decimal 환율 = 0.1M;
                        //decimal m_am = 1;  // 환율
                        //decimal 단가 = 0;
                        //decimal ldb_VatKr = 0;
                        //decimal ldb_AmKr = 0;
                        //decimal 외화금액 = 0;
                        //decimal ldb_amEx = 0;

                        //// 단위 환산량
                        //decimal ldb_rateExchg = _flexD.CDecimal(NewRow["RT_PO"]);
                        //if (ldb_rateExchg == 0)
                        //    ldb_rateExchg = 1;

                        //if (tb_NM_EXCH.DecimalValue != 0)                           // 환율
                        //    m_am = tb_NM_EXCH.DecimalValue;

                        //if (tb_TAX.DecimalValue != 0)
                        //    환율 = tb_TAX.DecimalValue / 100;
                        //else
                        //    환율 = 0;

                        //단가 = (_flexD.CDecimal(NewRow["UM_EX_PO"])) / ldb_rateExchg;  // 실제 단가

                        //decimal ldb_um;
                        //decimal 수량 = 0;

                        //수량 = _flexD.CDecimal(dQT_PO_MM);

                        //if (_flexD.CDecimal(row["AM_EX"]) != 0 && _flexD.CDecimal(NewRow["UM_EX_PO"]) == 0)
                        //{
                        //    ldb_um = _flexD.CDecimal(row["AM_EX"]) / 수량;
                        //    ldb_amEx = _flexD.CDecimal(row["AM_EX"]);

                        //}
                        //else
                        //{
                        //    ldb_um = _flexD.CDecimal(row["AM_EX"]) / 수량;
                        //    if (m_am == 0)
                        //    {
                        //        ldb_amEx = 외화금액;
                        //    }
                        //    else
                        //    {
                        //        ldb_amEx = 외화금액 / m_am;   //  외화금액 / 표기환율
                        //    }
                        //}



                        //if (cbo_TP_TAX.SelectedValue.ToString() == "001") //부가세포함
                        //{
                        //    ldb_VatKr = _flexD.CDecimal(Math.Floor(System.Math.Round((수량 * m_am * ldb_um) / (1 + 환율), 9) * 0.1M));
                        //    ldb_AmKr = _flexD.CDecimal(Math.Floor(수량 * m_am * ldb_um - ldb_VatKr));
                        //    외화금액 = Convert.ToDecimal(수량 * m_am * ldb_um - ldb_VatKr);

                        //    NewRow["AM"] = ldb_AmKr;
                        //    NewRow["VAT"] = ldb_VatKr;



                        //    // NewRow["AM_EX"] = ldb_amEx;  //금액
                        //    NewRow["UM_EX"] = ldb_amEx / (수량 * ldb_rateExchg);  // 외화 재고단위 단가
                        //    NewRow["UM_EX_PO"] = ldb_amEx / 수량;

                        //    //총합계
                        //    NewRow["AM_TOTAL"] = ldb_AmKr + ldb_VatKr;
                        //}
                        //else
                        //{
                        //    ldb_VatKr = _flexD.CDecimal(Math.Floor(수량 * m_am * 환율 * ldb_um));
                        //    ldb_AmKr = _flexD.CDecimal(Math.Floor(수량 * m_am * ldb_um));
                        //    외화금액 = Convert.ToDecimal(수량 * m_am * ldb_um);


                        //    NewRow["QT_PO"] = _flexD.CDecimal(dQT_PO_MM) * _flexD.CDecimal(NewRow["RT_PO"]);
                        //    //NewRow["AM_EX"] = 수량 * ldb_um;
                        //    NewRow["AM"] = ldb_AmKr; // 원화금액
                        //    NewRow["VAT"] = ldb_VatKr;                              //부가세 (원화금액기준)

                        //    //총합계
                        //    NewRow["AM_TOTAL"] = ldb_AmKr + ldb_VatKr;


                        //    if (수량 == 0)
                        //        return;

                        //    NewRow["UM_EX"] = ldb_amEx / (수량 * ldb_rateExchg);
                        //}
                        //ldb_amEx = _flexD.CDecimal(row["AM_EX"]);
                        //NewRow["UM_EX"] = ldb_amEx / (수량 * ldb_rateExchg);

                        //NewRow["UM_EX_PO"] = ldb_amEx / (수량);

                        //if (tb_NM_EXCH.DecimalValue != 0)                          //  원화 단가 계산
                        //{
                        //    NewRow["UM"] = ldb_amEx / (수량 * ldb_rateExchg) / m_am;
                        //    NewRow["UM_P"] = ldb_amEx / 수량 / m_am;
                        //}
                        //else
                        //{
                        //    NewRow["UM"] = 0;
                        //    NewRow["UM_P"] = 0;
                        //}

                        //SUMFunction();

                        #endregion


                        // 이부분은 속도 개선차원에서 DB를 가져오지 않고 (한번만 DB에서 가져올 수 있다던가.. 뭐..)
                        // 처리할 수 있도록 차후에 수정한다..
                        DataSet ds = _biz.ItemInfo_Search(m_obj);

                        object[] pi_obj = new object[] { Global.MainFrame.LoginInfo.CompanyCode, tb_DT_PO.Text.Substring(0, 4), D.GetString(cbo_CD_PLANT.SelectedValue), D.GetString(row["CD_ITEM"]), D.GetString(drs[0]["CD_SL"]) };
                        DataTable dt_pinv = _biz.item_pinvn(pi_obj);

                        if (ds != null && ds.Tables.Count > 3)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                NewRow["RT_PO"] = _flexD.CDecimal(ds.Tables[0].Rows[0]["UNIT_PO_FACT"]);

                                if (m_sEnv_FG_TAX != "000")
                                {
                                    if (ds.Tables[0].Rows[0]["FG_TAX_PU"].ToString() != string.Empty)
                                    {
                                        NewRow["FG_TAX"] = ds.Tables[0].Rows[0]["FG_TAX_PU"];
                                        NewRow["RATE_VAT"] = ds.Tables[0].Rows[0]["RATE_VAT"];
                                        rate_vat = Convert.ToDecimal(ds.Tables[0].Rows[0]["RATE_VAT"]);
                                    }
                                }
                                NewRow["WEIGHT"] = ds.Tables[0].Rows[0]["WEIGHT"];
                                NewRow["QT_WEIGHT"] = _flexD.CDecimal(NewRow["QT_PO_MM"]) * _flexD.CDecimal(ds.Tables[0].Rows[0]["WEIGHT"]);
                                NewRow["NM_ITEMGRP"] = ds.Tables[0].Rows[0]["NM_ITEMGRP"];
                                NewRow["NO_MODEL"] = ds.Tables[0].Rows[0]["NO_MODEL"];

                            }

                            if (ds.Tables[1].Rows.Count > 0)  // 유형별, 거래처별 구매조직에서 가져오기
                            {
                                if (!dt_EXCEL.Columns.Contains("UM_EX_PO"))
                                    NewRow["UM_EX_PO"] = _flexD.CDecimal(ds.Tables[1].Rows[0]["UM_ITEM"]);

                                if (_flexD.CDecimal(NewRow["RT_PO"]) == 0)
                                {
                                    NewRow["UM_EX"] = _flexD.CDecimal(NewRow["UM_EX_PO"]);
                                }
                                else
                                {
                                    NewRow["UM_EX"] = _flexD.CDecimal(NewRow["UM_EX_PO"]) / _flexD.CDecimal(NewRow["RT_PO"]);
                                    NewRow["QT_PO"] = _flexD.CDouble(NewRow["QT_PO_MM"]) * _flexD.CDouble(NewRow["RT_PO"]);
                                }

                                NewRow["UM_P"] = _flexD.CDecimal(NewRow["UM_EX_PO"]) * tb_NM_EXCH.DecimalValue;
                                NewRow["UM"] = _flexD.CDecimal(NewRow["UM_EX"]) * tb_NM_EXCH.DecimalValue;

                                NewRow["AM_EX"] = _flexD.CDecimal(NewRow["UM_EX_PO"]) * _flexD.CDecimal(NewRow["QT_PO_MM"]);

                                //NewRow["AM"] = Unit.원화금액(DataDictionaryTypes.PU, _flexD.CDecimal(NewRow["AM_EX"]) * tb_NM_EXCH.DecimalValue);
                                // NewRow["VAT"] = Unit.원화금액(DataDictionaryTypes.PU, _flexD.CDecimal(NewRow["AM"]) * (rate_vat / 100));
                            }

                            //if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                            //{
                            //    NewRow["QT_POC"] = ds.Tables[2].Rows[0]["QT_POC"]; //발주량
                            //    NewRow["QT_REQC"] = ds.Tables[2].Rows[0]["QT_REQC"];//의뢰량

                            //}
                            if (dt_pinv != null && dt_pinv.Rows.Count > 0)
                            {
                                NewRow["QT_INVC"] = dt_pinv.Rows[0]["QT_INVC"]; //현재고
                                NewRow["QT_ATPC"] = dt_pinv.Rows[0]["QT_ATPC"]; //가용재고
                            }


                        }

                        SUMFunction();

                       // tb_QT_PO.DecimalValue = _flexD.CDecimal(NewRow["QT_POC"]);
                        //tb_QT_REQ.DecimalValue = _flexD.CDecimal(NewRow["QT_REQC"]);
                        tb_QT_INV.DecimalValue = _flexD.CDecimal(NewRow["QT_INVC"]);
                        //tb_QT_ATP.DecimalValue = _flexD.CDecimal(NewRow["QT_ATPC"]);


                        NewRow["FG_TRANS"] = _header.CurrentRow["FG_TRANS"];
                        NewRow["FG_TPPURCHASE"] = _header.CurrentRow["FG_TPPURCHASE"];
                        NewRow["YN_AUTORCV"] = _header.CurrentRow["YN_AUTORCV"];
                        NewRow["YN_RCV"] = _header.CurrentRow["YN_RCV"];
                        NewRow["YN_RETURN"] = _header.CurrentRow["YN_RETURN"];
                        NewRow["YN_IMPORT"] = _header.CurrentRow["YN_IMPORT"];
                        NewRow["YN_ORDER"] = _header.CurrentRow["YN_ORDER"];
                        NewRow["YN_REQ"] = _header.CurrentRow["YN_REQ"];
                        NewRow["FG_RCV"] = _header.CurrentRow["FG_TPRCV"];
                        NewRow["YN_SUBCON"] = _header.CurrentRow["YN_SUBCON"];
                        NewRow["FG_PURCHASE"] = _header.CurrentRow["FG_TPPURCHASE"];

                        //NewRow["NO_PR"] = "";
                        NewRow["CD_SL"] = drs[0]["CD_SL"];
                        NewRow["NM_SL"] = drs[0]["NM_SL"];
                        NewRow["CD_EXCH"] = cbo_NM_EXCH.SelectedValue.ToString();

                        // cc 설정
                        if (m_sEnv_CC == "100")
                        {
                            NewRow["CD_CC"] = _header.CurrentRow["CD_CC_TPPO"];
                            NewRow["NM_CC"] = _header.CurrentRow["NM_CC_TPPO"];
                        }
                        else if(m_sEnv_CC == "000")
                        {
                            NewRow["CD_CC"] = _header.CurrentRow["CD_CC_PURGRP"];
                            NewRow["NM_CC"] = _header.CurrentRow["NM_CC_PURGRP"];
                        }


                        DataTable dtExch = (DataTable)cbo_NM_EXCH.DataSource;
                        foreach (DataRow drExch in dtExch.Rows)
                        {
                            if (drExch["CODE"].ToString() == cbo_NM_EXCH.SelectedValue.ToString())
                            {
                                NewRow["NM_EXCH"] = drExch["NAME"];
                                break;
                            }
                        }

         

                        // 20110322 아이큐브개발팀 김현철 추가
                        //NewRow["AM_TOTAL"] = _flexD.CDecimal(NewRow["AM"]) + _flexD.CDecimal(NewRow["VAT"]);
                        // 20110322 아이큐브개발팀 김현철 추가

                        부가세계산(NewRow);

                        //tb_QT_PO.DecimalValue = _flexD.CDecimal(NewRow["QT_POC"]);
                        //tb_QT_REQ.DecimalValue = _flexD.CDecimal(NewRow["QT_REQC"]);
                        tb_QT_INV.DecimalValue = _flexD.CDecimal(NewRow["QT_INVC"]);
                        //tb_QT_ATP.DecimalValue = _flexD.CDecimal(NewRow["QT_ATPC"]);
                        // MaxSeq++;
                        if (NewRow != null)
                        _flexD.DataTable.Rows.Add(NewRow);
                        //decimal dUM_EX = _flexD.CDecimal(_flexD.DataTable.Rows[_flexD.DataTable.Rows.Count - 1]["UM_EX"]);

                        //수량합계 += _flexD.CDecimal(NewRow["QT_PO_MM"]);
                        //재고수량합계 += _flexD.CDecimal(NewRow["QT_PO"]);
                        //금액합계 += _flexD.CDecimal(NewRow["AM_EX"]);
                        //원화금액 += _flexD.CDecimal(NewRow["AM"]);
                        //부가세 += _flexD.CDecimal(NewRow["VAT"]);

                        // 20110322 아이큐브개발팀 김현철 추가
                        //총금액 += _flexD.CDecimal(NewRow["AM_TOTAL"]);
                        // 20110322 아이큐브개발팀 김현철 추가

                        //_flexD[_flexD.Rows.Fixed, "QT_PO_MM"] = 수량합계.ToString();
                        //_flexD[_flexD.Rows.Fixed, "QT_PO"] = 재고수량합계.ToString();
                        //_flexD[_flexD.Rows.Fixed, "AM_EX"] = 금액합계.ToString();
                        //_flexD[_flexD.Rows.Fixed, "AM"] = 원화금액.ToString();
                        //_flexD[_flexD.Rows.Fixed, "VAT"] = 부가세.ToString();

                        //_flexD[_flexD.Rows.Fixed - 1, "QT_PO_MM"] = String.Format("{0:0,0}", 수량합계);
                        //_flexD[_flexD.Rows.Fixed - 1, "QT_PO"] = String.Format("{0:0,0}", 재고수량합계);
                        //_flexD[_flexD.Rows.Fixed - 1, "AM_EX"] = String.Format("{0:0,0.0000}", 금액합계);
                        //_flexD[_flexD.Rows.Fixed - 1, "AM"] = String.Format("{0:0,0}", 원화금액);
                        //_flexD[_flexD.Rows.Fixed - 1, "VAT"] = String.Format("{0:0,0}", 부가세);
                    }
                    else   // 공장품목에 등록 안되 있을 경우
                    {
                        string CD_ITEM = row["CD_ITEM"].ToString().PadRight(15, ' ');
                        string NM_ITEM = row["NM_ITEM"].ToString().PadRight(15, ' ');
                        string msg2 = CD_ITEM + " " + NM_ITEM;
                        검증리스트.AppendLine(msg2);
                        검증여부 = true;
                    }
                } // foreach문 (엑셀dt) End


                if (검증여부 || !bExist_Pjt || !seq_false_list)
                {
                    ShowDetailMessage
                        (@"엑셀 업로드하는 공장마스터품목,프로젝트 중 불일치하는 항목들이 있습니다. 
                         ▼ 버튼을 눌러서 목록을 확인하세요!", ((검증여부) ? 검증리스트.ToString() : "") + ((bExist_Pjt) ? "" : D.GetString(검증리스트_PJT)) + ((seq_false_list) ? "" : D.GetString(검증리스트_PJT_SEQ)));
                }

                if (_flexD.HasNormalRow) //포커스주기
                    _flexD.Select(_flexD.Rows.Fixed, _flexD.LeftCol);
                _flexD.IsDataChanged = true;
                Page_DataChanged(null, null);
            }
        }
        catch (Exception ex)
        {
            Global.MainFrame.MsgEnd(ex);
        }
        finally
        {
            _flexD.Redraw = true;
            _flexD.SumRefresh();
        }
    }

    #endregion

    #region -> 최대차수

    private decimal 최대차수
    {
        get
        {
            decimal MaxCntAmend = 0;

            for (int i = _flexD.Rows.Fixed; i < _flexD.Rows.Count; i++)
            {
                if (_flexD.CDecimal(_flexD[i, "NO_LINE"]) > MaxCntAmend)
                    MaxCntAmend = _flexD.CDecimal(_flexD[i, "NO_LINE"]);
            }

            return MaxCntAmend;
        }
    }

    #endregion

    #region -> 그리드 채워주는 메소드
    //프로젝트적용에서 사용
    private void InserGridtAdd(DataTable pdt_Line, bool p_b평형비고체크)
    {
        try
        {
            if (pdt_Line == null || pdt_Line.Rows.Count <= 0)
                return;

            호출여부 = true;

            DataRow row;
            decimal max_no_line = _flexD.GetMaxValue("NO_LINE");
            _flexD.Redraw = false;

            if (tb_NM_PARTNER.CodeValue == string.Empty || tb_NM_PARTNER.CodeValue != pdt_Line.Rows[0]["CD_PURCUST"].ToString())
            {
                tb_NM_PARTNER.SetCode(pdt_Line.Rows[0]["CD_PURCUST"].ToString(), pdt_Line.Rows[0]["LN_PARTNER"].ToString());
                _header.CurrentRow["CD_PARTNER"] = pdt_Line.Rows[0]["CD_PURCUST"].ToString();
            }

            Decimal rt_unit_po = 0;

            for (int i = 0; i < pdt_Line.Rows.Count; i++)
            {
                row = pdt_Line.Rows[i];
                max_no_line++;
                _flexD.Rows.Add();
                _flexD.Row = _flexD.Rows.Count - 1;

                _flexD["CD_ITEM"] = row["CD_ITEM"];						//품목코드			SA_PJTL_PRE.CD_ITEM
                _flexD["NM_ITEM"] = row["NM_ITEM"];						//품목명			품목마스터
                _flexD["STND_ITEM"] = row["STND_ITEM"];					//규격				품목마스터
                if (row["UNIT"] == null)
                    row["UNIT"] = "";									//단위				쓰지않음	
                _flexD["CD_UNIT_MM"] = row["UNIT"];						//수배단위			품목마스터

                //_flexD["DT_LIMIT"] = row["DT_IO"];					    //납기일			빈칸이나수주일자와동일
                //2010.01.12 납기일
                if (tb_DT_LIMIT.Text == string.Empty)
                    _flexD["DT_LIMIT"] = row["DT_IO"];
                else
                    _flexD["DT_LIMIT"] = tb_DT_LIMIT.Text;

                _flexD["DT_PLAN"] = _flexD["DT_LIMIT"];

                _flexD["QT_PO_MM"] = row["QT_MINUS"].ToString() == "" ? 0 : _flexD.CDecimal(row["QT_MINUS"]);    //발주단위수량		      SA_PJTL_PRE.CD_ITEM  

                //_flexD["UM_EX_PO"] = row["UM_EXPO_CIS"];                                       //발주단위외화단가                   SA_PJTL_PRE.UM_EXPO_CIS

                //거래구분이 국내 & 환종이 원화                    
                if ((_header.CurrentRow["FG_TRANS"]).ToString() == "001" && (_header.CurrentRow["CD_EXCH"]).ToString() == "000")   // 국내이면서 원화이면 단가 금액에 원화를 입력한다.
                {
                    // 이형준씨가 다시 정의해서 수정 요청했다 2009.03.06
                    _flexD["UM_EX_PO"] = row["UM_PO"];   // 단가
                    _flexD["AM_EX"] = Convert.ToDecimal(row["UM_PO"]) * Convert.ToDecimal(row["QT_MINUS"]); // 단가 * 잔여수량


                    // _flexD["UM_EX_PO"] = row["UM_EXPO_CIS"];
                    // _flexD["AM_EX"] = Convert.ToDecimal(row["UM_EXPO_CIS"]) * Convert.ToDecimal(row["QT_MINUS"]) ; 
                    //_flexD["AM_EX"] = row["AM_PO_MINUS"];                                //외화금액			                       
                    //_flexD["UM_EX_PO"] = (/*Convert.ToDecimal(row["QT"]) - */Convert.ToDecimal(row["QT_MINUS"]) != 0) ? Convert.ToDecimal(row["AM_PO_MINUS"]) / (/*Convert.ToDecimal(row["QT"]) - */Convert.ToDecimal(row["QT_MINUS"]) ): 0;      // ((i_qt != 0) ? ((double)row["AM_PO"]) / i_qt : 0);
                    //_flexD["AM"] = Convert.ToDecimal(row["QT"]) * Convert.ToDecimal(_flexD["UM_EX_PO"]) * tb_NM_EXCH.DecimalValue;
                }
                else
                {
                    _flexD["UM_EX_PO"] = row["UM_EXPO_CIS"];        //외화단가
                    _flexD["AM_EX"] = Convert.ToDecimal(row["UM_EXPO_CIS"]) * Convert.ToDecimal(row["QT_MINUS"]);      //외화단가 * 잔여수량 //row["AM_EXPO_CIS_MINUS"];     //외화금액	(프로)		                       
                    //_flexD["AM"] = row["AM_PO_MINUS"];
                }

                rt_unit_po = (System.DBNull.Value != row["UNIT_PO_FACT"] && Convert.ToDecimal(row["UNIT_PO_FACT"]) != 0) ? Convert.ToDecimal(row["UNIT_PO_FACT"]) : 1;


                //_flexD["UM"] = row["UM_PO"]; 
                _flexD["UM_EX"] = UDecimal.Getdivision(D.GetDecimal(_flexD["AM_EX"]), (((D.GetDecimal(_flexD["QT_PO_MM"]) != 0) ? D.GetDecimal(_flexD["QT_PO_MM"]) : 1) * rt_unit_po));
                _flexD["UM"] = D.GetDecimal( _flexD["UM_EX"]) * tb_NM_EXCH.DecimalValue;

                _flexD["RT_PO"] = rt_unit_po; //row["UNIT_PO_FACT"].ToString() == "" ? 1 : row["UNIT_PO_FACT"];
                _flexD["QT_PO"] = Convert.ToDecimal(row["QT_MINUS"]) * rt_unit_po;//   ((Convert.ToDecimal(_flexD["RT_PO"]) == 0) ? 1 : Convert.ToDecimal(_flexD["RT_PO"]));


                _flexD["CD_PJT"] = row["CD_PJT"];					//프로젝트코드		       SA_PJTL_PRE.CD_PJT
                _flexD["NM_PJT"] = row["NM_PJT"];					//프로젝트이름		       SA_PJTL_PRE.NM_PJT


                _flexD["SEQ_PROJECT"] = row["SEQ_PROJECT"];
                _flexD["UNIT_IM"] = row["UNIT_IM"];

                _flexD["FG_TAX"] = _header.CurrentRow["FG_TAX"];
                _flexD["TP_UM_TAX"] = _header.CurrentRow["TP_UM_TAX"];
                _flexD["RATE_VAT"] = tb_TAX.DecimalValue;
                if (m_sEnv_FG_TAX == "100")
                {

                    if (D.GetString(row["FG_TAX_PU"]) != string.Empty)
                    {
                        _flexD["FG_TAX"] = row["FG_TAX_PU"];
                        _flexD["RATE_VAT"] = D.GetDecimal(row["RATE_VAT"]);
                    }
                }

                if (BASIC.GetMAEXC("발주등록(공장)-프로젝트별_의제매입세_구분") == "100" && D.GetString(row["CD_USERDEF14"]) == "001") //의제 매입 프로젝트별 적용
                {
                    string fg_tax_josun = _biz.pjt_item_josun(D.GetString(_flexD["CD_PJT"]));
                    if (fg_tax_josun != "")
                    {
                        _flexD["FG_TAX"] = fg_tax_josun;
                        _flexD["RATE_VAT"] = 0;//부가세율은 0이 들어가면된다고함 (김광석사원요청) 2011-12-02

                    }
                }

                부가세계산(_flexD.GetDataRow(_flexD.Row));
                //_flexD["AM"] = _flexD.CDecimal(_flexD["AM_EX"]) * tb_NM_EXCH.DecimalValue;
                //_flexD["VAT"] = Unit.원화금액(DataDictionaryTypes.PU, Unit.원화금액(DataDictionaryTypes.PU, _flexD.CDecimal(_flexD["AM"]) * (UDecimal.Getdivision(tb_TAX.DecimalValue, 100))));
                //_flexD["AM_TOTAL"] = _flexD.CDecimal(_flexD["AM"]) + _flexD.CDecimal(_flexD["VAT"]);//총합계


                _flexD["NM_CUST_DLV"] = row["NM_CUST_DLV"];
                _flexD["ADDR1_DLV"] = row["ADDR1"];
                _flexD["NO_TEL_D2_DLV"] = row["NO_TEL_D2"];


                _flexD["NO_PR"] = "";
                _flexD["CD_PLANT"] = cbo_CD_PLANT.SelectedValue.ToString();


                _flexD["NO_PO"] = tb_NO_PO.Text;
                _flexD["NO_LINE"] = max_no_line;
                _flexD["NM_SYSDEF"] = _ComfirmState;
                _flexD["FG_TRANS"] = _header.CurrentRow["FG_TRANS"];
                _flexD["FG_TPPURCHASE"] = _header.CurrentRow["FG_TPPURCHASE"];
                _flexD["YN_AUTORCV"] = _header.CurrentRow["YN_AUTORCV"];
                _flexD["YN_RCV"] = _header.CurrentRow["YN_RCV"];
                _flexD["YN_RETURN"] = _header.CurrentRow["YN_RETURN"];
                _flexD["YN_IMPORT"] = _header.CurrentRow["YN_IMPORT"];
                _flexD["YN_ORDER"] = _header.CurrentRow["YN_ORDER"];
                _flexD["YN_REQ"] = _header.CurrentRow["YN_REQ"];
                _flexD["FG_RCV"] = _header.CurrentRow["FG_TPRCV"];
                _flexD["YN_SUBCON"] = _header.CurrentRow["YN_SUBCON"];
                _flexD["FG_PURCHASE"] = _header.CurrentRow["FG_TPPURCHASE"];

                _flexD["NO_PR"] = "";
                _flexD["CD_EXCH"] = cbo_NM_EXCH.SelectedValue.ToString();




                SetCC(_flexD.Row, "");

                _flexD["WEIGHT"] = row["WEIGHT"];						//단위중량
                _flexD["QT_WEIGHT"] = _flexD.CDecimal(_flexD["QT_PO_MM"]) * _flexD.CDecimal(_flexD["WEIGHT"]);

                //2010.03.09 추가
                if (p_b평형비고체크)
                    _flexD["DC1"] = row["PJT_GROUP"];

                _flexD["NM_ITEMGRP"] = D.GetString(row["NM_ITEMGRP"]);
                _flexD["NO_MODEL"] = D.GetString(row["NO_MODEL"]);
                _flexD["STND_DETAIL_ITEM"] = D.GetString(row["STND_DETAIL_ITEM"]);
                _flexD["CD_USERDEF14"] = D.GetString(row["CD_USERDEF14"]);
                _flexD["NM_GRPMFG"] = D.GetString(row["NM_GRPMFG"]);
                _flexD["NM_MAKER"] = D.GetString(row["NM_MAKER"]);

                if (!_flexD.DataTable.Columns.Contains("APP_PJT"))
                {
                    _flexD.DataTable.Columns.Add("APP_PJT", typeof(string));
                    _flexD.Cols["APP_PJT"].Visible = false;
                }

                _flexD["APP_PJT"] = "Y";


            }
            _flexD.Redraw = true;

            SetHeadControlEnabled(false, 1);


        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
    }

    #endregion

    #endregion

    #region♣ 예산관련
    private DataTable 예산chk(DataTable dt_tg)
    {

        //UP_HELP_FI_SELECT2 의 42번째(@P_P63_CODE3)에 계정에 대한 인자값을 넘겨주고 있습니다.
        //그래서 내용을 확인해보면 521번째 줄에 해당 계정과 연결된
        //예산계정만 뜨도록 하고 있습니다. 

        //실행예산 : 예산단위, 예산계정의 실행예산
        //집행실적 : 예산단위, 예산계정의 집행실적
        //집행신청 : 현재 구매요청등록하는 것의 예산단위, 예산계정 금액 (금액 현재에는 부가세가 없기 때문에 부가세는 제외함)
        //집행율 : 예산단위, 예산계정의 집행실적/실행예산 * 100
        //잔여예산 : 실행예산 - 집행실적 - 집행신청 으로 표기합니다.

        DataTable dt = new DataTable();
        dt.Columns.Add("CD_BUDGET", typeof(string));
        dt.Columns.Add("NM_BUDGET", typeof(string));
        dt.Columns.Add("CD_BGACCT", typeof(string));
        dt.Columns.Add("NM_BGACCT", typeof(string));

        dt.Columns.Add("AM_ACTSUM", typeof(decimal)); //실행예산
        dt.Columns.Add("AM_JSUM", typeof(decimal)); //집행실적
        dt.Columns.Add("RT_JSUM", typeof(decimal)); //집행율
        dt.Columns.Add("AM", typeof(decimal)); //집행신청 (청구금액)
        dt.Columns.Add("AM_JAN", typeof(decimal)); //잔여예산
        dt.Columns.Add("TP_BUNIT", typeof(string)); //예산통제구분

        dt.Columns.Add("ERROR_MSG", typeof(string)); //ERROR_MSG


        //1) 예산단위 예산계정별로 그룹핑한다.

        for (int i = 0; i < dt_tg.Rows.Count; i++)
        {
            if (dt_tg.Rows[i].RowState == DataRowState.Deleted)
                continue;

            //예산단위/예산계정기입된것만 대상 (시스템통제시 이들은 필수값임)
            if (dt_tg.Rows[i]["CD_BUDGET"] == null || dt_tg.Rows[i]["CD_BUDGET"].ToString().Trim() == ""
                || dt_tg.Rows[i]["CD_BGACCT"] == null || dt_tg.Rows[i]["CD_BGACCT"].ToString().Trim() == "")
                continue;

            DataRow[] DR;
            DataRow NewRow;

            DR = dt.Select(" CD_BUDGET = '" + dt_tg.Rows[i]["CD_BUDGET"].ToString().Trim() + "'" +
                        " AND CD_BGACCT = '" + dt_tg.Rows[i]["CD_BGACCT"].ToString().Trim() + "'");

            if (DR.Length == 0) //없다 
            {

                NewRow = dt.NewRow();

                NewRow["CD_BUDGET"] = dt_tg.Rows[i]["CD_BUDGET"].ToString().Trim();
                NewRow["NM_BUDGET"] = dt_tg.Rows[i]["NM_BUDGET"].ToString().Trim();
                NewRow["CD_BGACCT"] = dt_tg.Rows[i]["CD_BGACCT"].ToString().Trim();
                NewRow["NM_BGACCT"] = dt_tg.Rows[i]["NM_BGACCT"].ToString().Trim();
                NewRow["AM"] = _flexD.CDecimal(dt_tg.Rows[i]["AM"]);

                dt.Rows.Add(NewRow);
            }
            else
            {
                DR[0]["AM"] = _flexD.CDecimal(DR[0]["AM"]) + _flexD.CDecimal(_flexD.CDecimal(dt_tg.Rows[i]["AM"]));
            }
        }


        //foreach (DataRow drD in _flexD.DataTable.Rows)
        //{
        //    if (drD.RowState == DataRowState.Deleted)
        //        continue;

        //    //예산단위/예산계정기입된것만 대상 (시스템통제시 이들은 필수값임)
        //    if (drD["CD_BUDGET"] == null || drD["CD_BUDGET"].ToString().Trim() == ""
        //        || drD["CD_BGACCT"] == null || drD["CD_BGACCT"].ToString().Trim() == "")
        //        continue;

        //    DataRow[] DR_fd;
        //    DataRow NewRow;

        //    DR_fd = dt.Select(" CD_BUDGET = '" + drD["CD_BUDGET"].ToString().Trim() + "'" +
        //                " AND CD_BGACCT = '" + drD["CD_BGACCT"].ToString().Trim() + "'");

        //    if (DR_fd.Length == 0) //없다 
        //    {

        //        NewRow = dt.NewRow();

        //        NewRow["CD_BUDGET"] = drD["CD_BUDGET"].ToString().Trim();
        //        NewRow["NM_BUDGET"] = drD["NM_BUDGET"].ToString().Trim();
        //        NewRow["CD_BGACCT"] = drD["CD_BGACCT"].ToString().Trim();
        //        NewRow["NM_BGACCT"] = drD["NM_BGACCT"].ToString().Trim();
        //        NewRow["AM"] = _flexD.CDecimal(drD["AM"]);

        //        dt.Rows.Add(NewRow);
        //    }
        //    else
        //    {
        //        DR_fd[0]["AM"] = _flexD.CDecimal(DR_fd[0]["AM"]) + _flexD.CDecimal(_flexD.CDecimal(drD["AM"]));
        //    }
        //}

        //2) 그룹핑된 예산단위/예산계정별로 실행예산/집행실적을 반환하는 SP를 호출하여 잔액을 구한다.
        //리턴값은 실행예산금액, 집행예산금액, 통제여부..

        foreach (DataRow row in dt.Rows)
        {
            DataTable dt_rtn;
            dt_rtn = _biz.CheckBUDGET(row["CD_BUDGET"].ToString().Trim(), row["CD_BGACCT"].ToString().Trim(), tb_DT_PO.Text);

            if (dt_rtn.Rows.Count > 0)
            {
                row["AM_ACTSUM"] = _flexD.CDecimal(dt_rtn.Rows[0]["AM_ACTSUM"]);
                row["AM_JSUM"] = _flexD.CDecimal(dt_rtn.Rows[0]["AM_JSUM"]);
                row["TP_BUNIT"] = dt_rtn.Rows[0]["TP_BUNIT"].ToString().Trim();

                if (_flexD.CDecimal(row["AM_ACTSUM"]) != 0)
                    row["RT_JSUM"] = _flexD.CDecimal(row["AM_JSUM"]) / _flexD.CDecimal(row["AM_ACTSUM"]) * 100;

                row["AM_JAN"] = _flexD.CDecimal(row["AM_ACTSUM"]) - _flexD.CDecimal(row["AM_JSUM"]) - _flexD.CDecimal(row["AM"]);

                row["ERROR_MSG"] = dt_rtn.Rows[0]["ERROR_MSG"].ToString().Trim();
            }
        }

        return dt;
    }

    private void btn_예산chk_Click(object sender, EventArgs e)
    {
        //DataTable dt = 예산chk(_flexD.DataTable);

        //P_PU_BUDGET_SUB m_dlg = new P_PU_BUDGET_SUB(dt);

        P_PU_BUDGET_SUB m_dlg = new P_PU_BUDGET_SUB(_flexD.DataTable, tb_DT_PO.Text, "NO_PO"); // 요청일자가 필요함, 화면구분(PR:구매요청, APP:구매품의, PO:구매발주, 그외 내역)

        if (m_dlg.ShowDialog(this) == DialogResult.OK)
            return;
    }

    private void btn_예산chk내역_Click(object sender, EventArgs e)
    {
        if (_header.CurrentRow["NO_PO"] == null || _header.CurrentRow["NO_PO"].ToString() == "")
            return;

        DataSet ds = _biz.PU_BUDGET_HST_SELECT(_header.CurrentRow["NO_PO"].ToString());

        //P_PU_BUDGET_SUB m_dlg = new P_PU_BUDGET_SUB(ds.Tables[0]);

        P_PU_BUDGET_SUB m_dlg = new P_PU_BUDGET_SUB(ds.Tables[0], tb_DT_PO.Text, "HS_PO"); // 요청일자가 필요함 HS_PR은 내역체크를위해

        if (m_dlg.ShowDialog(this) == DialogResult.OK)
            return;
    }
    #endregion

    #region ♣ 업체전용메소드

    #region 백광에서만 필요한 작업 모아놓기위해

    #region 전자결재 한번올려진것은 버튼 비활성화 : 백광
    private void check_GW(string NO_PR)//백광에서만
    {
        if (_biz.GetFI_GWDOCU(NO_PR).ToString().Trim() == "999") btn전자결재.Enabled = true;//전자결재 한번올려진것은 버튼 비활성화 : 백광
        else { btn전자결재.Enabled = false; }
    }
    #endregion

    #endregion

    #region 포틱스전용

    void btn배부_Click(object sender, EventArgs e)
    {
        try
        {
            if(!Check()) return;
            decimal exch = tb_NM_EXCH.DecimalValue;

            if (m_sEnv_Nego == "100") //포티스
            {
                int i;
                decimal ORI_AM_EX = D.GetDecimal(_flexD[1, "AM_EX"]); // NEGO금액 배부받기 이전의 총 금액
                decimal ORI_AM = D.GetDecimal(_flexD[1, "AM"]); // NEGO금액 배부받기 이전의 총 원화금액
                decimal AM_NEGO = curNEGO금액.DecimalValue; //NEGO금액;


                decimal AM_EX;
                decimal SUM_AM_EX = decimal.Zero; //총금액을 맞춰주기위해서.....
                decimal SUM_AM = decimal.Zero;
                decimal SUM_VAT = decimal.Zero;

                decimal newValue;
                decimal tax = tb_TAX.DecimalValue;
              

                _header.CurrentRow["AM_NEGO"] = AM_NEGO;

                if (exch == 0) exch = 1;

                for (i = _flexD.Rows.Fixed; i < _flexD.Rows.Count - 1; i++)
                {

                    //AM_EX = Math.Round((ORI_AM_EX-AM_NEGO) * (D.GetDecimal(_flexD[i, "AM_EX"]) / ORI_AM_EX), 4); //NEGO금액 적용후 금액
                    AM_EX = Unit.외화금액(DataDictionaryTypes.PU, (ORI_AM_EX - AM_NEGO) * (D.GetDecimal(_flexD[i, "AM_EX"]) / ORI_AM_EX));
                    _flexD[i, "AM_EX"] = AM_EX;
                    //_flexD[i, "UM_EX_PO"] = Math.Round(AM_EX / D.GetDecimal(_flexD[i, "QT_PO_MM"]),4); //NEGO금액 적용후 단가
                    _flexD[i, "UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, AM_EX / D.GetDecimal(_flexD[i, "QT_PO_MM"])); //NEGO금액 적용후 단가
                    _flexD[i, "AM"] = Unit.원화금액(DataDictionaryTypes.PU, AM_EX * tb_NM_EXCH.DecimalValue); //NEGO금액 적용후 원화금액
                    _flexD[i, "VAT"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flexD[i, "AM"]) * tax * 0.01m);  //NEGO금액 적용후 부가세
                    _flexD[i, "AM_TOTAL"] = D.GetDecimal(_flexD[i, "AM"]) + D.GetDecimal(_flexD[i, "VAT"]);

                    SUM_AM_EX += AM_EX;
                    SUM_AM += D.GetDecimal(_flexD[i, "AM"]);
                    SUM_VAT += D.GetDecimal(_flexD[i, "VAT"]);

                }

                newValue = Unit.외화금액(DataDictionaryTypes.PU, (ORI_AM_EX - AM_NEGO) - SUM_AM_EX);
                _flexD[i, "AM_EX"] = newValue; //마지막행 금액에 남은금액을 적용
                //_flexD[i, "UM_EX_PO"] = Math.Round(newValue / D.GetDecimal(_flexD[i, "QT_PO_MM"]),4); //마지막행 단가
                _flexD[i, "UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, newValue / D.GetDecimal(_flexD[i, "QT_PO_MM"])); //마지막행 단가

                newValue = (ORI_AM - (AM_NEGO * tb_NM_EXCH.DecimalValue)) - SUM_AM; //마지막행 원화금액에 남은 원화금액을 적용
                _flexD[i, "AM"] = newValue;
                SUM_AM += newValue;

                newValue = Unit.원화금액(DataDictionaryTypes.PU, (SUM_AM * tax * 0.01m) - SUM_VAT); //마지막행 부가세에 남은 부가세를 적용
                _flexD[i, "VAT"] = newValue;
                _flexD[i, "AM_TOTAL"] = D.GetDecimal(_flexD[i, "AM"]) + D.GetDecimal(_flexD[i, "VAT"]);

            }
            else
            {
                decimal dUm = decimal.Zero;
                decimal dTemp = decimal.Zero;
                bool bCheck = false;

                StringBuilder 검증리스트 = new StringBuilder();
                string msg = "품목명\t\t 단가\t";
                검증리스트.AppendLine(msg);
                msg = "-".PadRight(80, '-');
                검증리스트.AppendLine(msg);

                for (int i = _flexD.Rows.Fixed; i < _flexD.Rows.Count; i++)
                {
                    dUm = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(_flexD[i, "UM_EX_PO"]) * (1 - (curNEGO금액.DecimalValue * 0.01M)));
                    decimal dPow = Convert.ToDecimal(Math.Pow(10d, Convert.ToDouble(curDe.DecimalValue)));

                    //입력한 자리수만큼 0처리해준다
                    if (curDe.DecimalValue > 0)
                    {
                        if (dUm / dPow < 1)
                        {
                            string msg2 = _flexD[i, "NM_ITEM"].ToString().PadRight(15, ' ') + " " + _flexD[i, "UM_EX_PO"].ToString().PadRight(15, ' ');
                            검증리스트.AppendLine(msg2);
                            bCheck = true;
                            continue;
                        }
                        
                        dTemp = Unit.외화단가(DataDictionaryTypes.PU,Math.Truncate(dUm / dPow) * dPow);
                    }
                    else
                        dTemp = dUm;

                    decimal ldb_rateExchg = D.GetDecimal(_flexD[i, "RT_PO"]) == 0 ? 1 : D.GetDecimal(_flexD[i, "RT_PO"]);
                    decimal rate_vat = D.GetDecimal(_flexD[i, "RATE_VAT"]) == 0 ? 0 : D.GetDecimal(_flexD[i, "RATE_VAT"]) / 100;  //과세율 

                    _flexD[i, "UM_EX_PO"] = dTemp;
                    _flexD[i, "UM_EX"] = dTemp / ldb_rateExchg;
                    _flexD[i, "UM_P"] = dTemp * exch;
                    _flexD[i, "UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(_flexD[i, "UM_EX"]) * exch);
                    _flexD[i, "AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flexD[i, "UM_EX_PO"]) * D.GetDecimal(_flexD[i, "QT_PO_MM"]) * exch);
                    _flexD[i, "AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(_flexD[i, "UM_EX_PO"]) * D.GetDecimal(_flexD[i, "QT_PO_MM"]));
                    _flexD[i, "VAT"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flexD[i, "AM"]) * rate_vat);
                    _flexD[i, "AM_TOTAL"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flexD[i, "AM"]) + D.GetDecimal(_flexD[i, "VAT"]));

                }

                if (bCheck)
                {
                    ShowDetailMessage(@"0처리 자리수를 초가하는 품목이 있습니다.▼ 버튼을 눌러서 목록을 확인하세요!", 검증리스트.ToString());
                }


            }

            SUMFunction();

        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }

    }

    private bool Check()
    {
        string[] strName = new string[3];

        if (m_sEnv_Nego == "100")
        {
            strName[0] = "Nego금액";
            strName[1] = "QT_PO_MM";
            strName[2] = "발주수량";
        }
        else
        {
            strName[0] = "할인율";
            strName[1] = "UM_EX_PO";
            strName[2] = "단가";
        }

        if (curNEGO금액.DecimalValue <= 0)
        {
            ShowMessage(공통메세지._은_보다커야합니다, new string[] { DD(strName[0]), "0" });
            return false;
        }


        for (int i = _flexD.Rows.Fixed; i < _flexD.Rows.Count; i++)
        {
            if (D.GetString(_flexD[i, "FG_POST"]) != "O")
            {
                ShowMessage("발주 확정/종결 건은 처리할 수 없습니다");
                return false;
            }

            if (D.GetDecimal(D.GetDecimal(_flexD[i, strName[1]])) == 0)
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, DD(strName[2]));
                return false;
            }

        }

        return true;
    }

    #endregion

    #region -> 아사히카세이 전용

    protected void AsahiKasei_Only_Item(int apply_row, DataTable dt)                
    {
        _flexD[apply_row, "QT_LENGTH"] = D.GetDecimal(dt.Rows[0]["QT_LENGTH"]);
        _flexD[apply_row, "QT_WIDTH"] = D.GetDecimal(dt.Rows[0]["QT_WIDTH"]) * 1000;
        _flexD[apply_row, "QT_AREA"] = D.GetDecimal(dt.Rows[0]["QT_LENGTH"]) * D.GetDecimal(dt.Rows[0]["QT_WIDTH"]);
        _flexD[apply_row, "CD_TP"] = D.GetString(dt.Rows[0]["CD_TP"]);

        if (D.GetDecimal(_flexD[apply_row, "QT_PO_MM"]) > 0)
            _flexD[apply_row, "TOTAL_AREA"] = D.GetDecimal(_flexD[apply_row, "QT_PO_MM"]) * D.GetDecimal(_flexD[apply_row, "QT_AREA"]);
    }

    protected void AsahiKasei_Only_ValidateEdit(int apply_row, decimal newvalue, string colname)
    {
        if (D.GetDecimal(_flexD[apply_row, "QT_AREA"]) <= 0)
            return;

        switch(colname)
        {  
            case "QT_PO_MM":
                _flexD[apply_row, "TOTAL_AREA"] = newvalue * D.GetDecimal(_flexD[apply_row, "QT_AREA"]);
            break;
            case "UM_EX_PO":
                _flexD[apply_row, "UM_EX_AR"] = newvalue / D.GetDecimal(_flexD[apply_row, "QT_AREA"]);
            break;
            case "AM_EX":
                _flexD[apply_row, "UM_EX_AR"] = D.GetDecimal(_flexD[apply_row, "UM_EX_PO"]) / D.GetDecimal(_flexD[apply_row, "QT_AREA"]);
            break;
            case "UM_EX_AR":
            _flexD[apply_row, "UM_EX_AR"] = newvalue;
            _flexD[apply_row, "UM_EX_PO"] = newvalue * D.GetDecimal(_flexD[apply_row, "QT_AREA"]);
            금액계산(apply_row, D.GetDecimal(_flexD[apply_row, "UM_EX_PO"]), D.GetDecimal(_flexD[apply_row, "QT_PO_MM"]), "UM_EX_PO", D.GetDecimal(_flexD[apply_row, "UM_EX_PO"]));
            break;
        }
    }

    #endregion

    #endregion

    #region ♣ 속성들

    #region -> FillPol <- 복사된 경우 호출

    /// <summary>
    /// 복사한값 저장할때불러옴  by J.Y
    /// </summary>

    private void FillPol()
    {
        if (!_flexD.HasNormalRow) return;

        foreach (DataRow row in _flexD.DataTable.Select("", "", DataViewRowState.CurrentRows))
        {
            //if (row.RowState == DataRowState.Deleted) continue;

            row["FG_TRANS"] = _header.CurrentRow["FG_TRANS"];
            row["FG_TPPURCHASE"] = _header.CurrentRow["FG_TPPURCHASE"];
            row["YN_AUTORCV"] = _header.CurrentRow["YN_AUTORCV"];
            row["YN_RCV"] = _header.CurrentRow["YN_RCV"];
            row["YN_RETURN"] = _header.CurrentRow["YN_RETURN"];
            row["YN_IMPORT"] = _header.CurrentRow["YN_IMPORT"];
            row["YN_ORDER"] = _header.CurrentRow["YN_ORDER"];
            row["YN_REQ"] = _header.CurrentRow["YN_REQ"];
            row["FG_RCV"] = _header.CurrentRow["FG_TPRCV"];
            row["YN_SUBCON"] = _header.CurrentRow["YN_SUBCON"];
            row["FG_PURCHASE"] = _header.CurrentRow["FG_TPPURCHASE"];
            row["FG_TAX"] = _header.CurrentRow["FG_TAX"];
            row["TP_UM_TAX"] = _header.CurrentRow["TP_UM_TAX"];  

        }
    }

    #endregion

    #region -> FillPol <- 추가된 경우 호출

    /// <summary>
    /// 그리드 라인이 추가된 경우 값을 채우는 구문
    /// </summary>
    /// <param name="i"></param>

    private void FillPol(int i)
    {
        if (_flexD.HasNormalRow)
        {
            _flexD[i, "FG_TRANS"] = _header.CurrentRow["FG_TRANS"];
            _flexD[i, "FG_TPPURCHASE"] = _header.CurrentRow["FG_TPPURCHASE"];
            _flexD[i, "YN_AUTORCV"] = _header.CurrentRow["YN_AUTORCV"];
            _flexD[i, "YN_RCV"] = _header.CurrentRow["YN_RCV"];
            _flexD[i, "YN_RETURN"] = _header.CurrentRow["YN_RETURN"];
            _flexD[i, "YN_IMPORT"] = _header.CurrentRow["YN_IMPORT"];
            _flexD[i, "YN_ORDER"] = _header.CurrentRow["YN_ORDER"];
            _flexD[i, "YN_REQ"] = _header.CurrentRow["YN_REQ"];
            _flexD[i, "FG_RCV"] = _header.CurrentRow["FG_TPRCV"];
            _flexD[i, "YN_SUBCON"] = _header.CurrentRow["YN_SUBCON"];
            _flexD[i, "FG_PURCHASE"] = _header.CurrentRow["FG_TPPURCHASE"];
            _flexD[i, "FG_TAX"] = _header.CurrentRow["FG_TAX"];
            _flexD[i, "TP_UM_TAX"] = _header.CurrentRow["TP_UM_TAX"];  

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


            // 헤더가 변경됐지만 추가모드이고 디테일 그리드가 아무 내용이 없으면 변경안된걸로 본다.
            if (bChange && _header.JobMode == JobModeEnum.추가후수정 && !_flexD.HasNormalRow)
                bChange = false;

            return bChange;
        }
    }

    #endregion

    #region -> 추가모드여부

    /// <summary>
    /// 추가이면  true
    /// </summary>

    private bool 추가모드여부
    {
        get
        {
            if (_header.JobMode == JobModeEnum.추가후수정)// && _header.CurrentRow["NO_PO"].ToString() == string.Empty)
                return true;
            return false;
        }
    }

    #endregion

    #region -> 차수여부

    private bool 차수여부
    {
        get
        {
            if (D.GetDecimal(_header.CurrentRow["NO_HST"]) != 0 && _flexD.HasNormalRow)
                return false;

            return true;
        }
    }
    #endregion

    #region -> 요청적용여부

    /// <summary>
    /// 조회시 요청적용여부를 체크하는 구문
    /// </summary>

    private bool 요청적용여부
    {
        get
        {
            if (_header.CurrentRow["TP_PROCESS"].ToString() == "1" && _flexD.HasNormalRow)
                return false;

            return true;
        }
    }

    #endregion

    #region -> 품의적용여부

    /// <summary>
    /// 조회시 품의적용여부를 체크하는 구문
    /// </summary>

    private bool 품의적용여부
    {
        get
        {
            if (_header.CurrentRow["TP_PROCESS"].ToString() == "3" && _flexD.HasNormalRow)
                return false;

            return true;
        }
    }

    #endregion

    #region -> 수주적용여부

    /// <summary>
    /// 조회시 요청적용여부를 체크하는 구문
    /// </summary>

    private bool 수주적용여부
    {
        get
        {
            if (_header.CurrentRow["TP_PROCESS"].ToString() == "4" && _flexD.HasNormalRow)
                return false;

            return true;
        }
    }

    #endregion

    #region -> 과세구분-의제매입여부

    /// <summary>
    /// 과세구분이 의제매입인경우 부가세금액을 0으로 처리한다...
    /// </summary>
    /// 2009.12.08
    private bool 의제매입여부(string ps_taxp)
    {
        if (ps_taxp == "27" || ps_taxp == "28" || ps_taxp == "29" || ps_taxp == "30" || ps_taxp == "32" || ps_taxp == "33" ||
            ps_taxp == "34" || ps_taxp == "35" || ps_taxp == "36" || ps_taxp == "40" || ps_taxp == "41" || ps_taxp == "42" ||
            ps_taxp == "48" || ps_taxp == "49" || ps_taxp == "51" || ps_taxp == "52" || ps_taxp == "53" || ps_taxp == "56" ||
            ps_taxp == "57" || ps_taxp == "58" || ps_taxp == "59")

            return true;

        return false;
    }

    #endregion

    #region -> 자동입고여부체크 <-- SaveData 에서 호출

    /// <summary>
    /// 저장시 자동입고여부 체크하는 구문
    /// </summary>

    private bool 자동입고여부체크
    {
        get
        {
            // 거래구분이 국내아니면 자동의뢰 및 입고 불가능
            if (_header.CurrentRow["YN_REQ"].ToString() == "N" && _header.CurrentRow["FG_TRANS"].ToString() != "001")
            {
                ShowMessage(메세지.거래구분이국내일때만자동의뢰및입고행위가가능합니다, "");
                //MessageBoxEx.Show("거래구분이 국내일 때만 자동의뢰및 입고행위가 가능합니다. \n 발주 형태를 변경해 주십시오.",PageName);
                return false;
            }

            // 자동입고일 경우 보관장소 필수 입력
            if (_header.CurrentRow["YN_AUTORCV"].ToString() == "Y" && _header.CurrentRow["YN_REQ"].ToString() == "N")
            {
                if (!_flexD.HasNormalRow) return false;

                if (_flexD.DataTable.Select("Len(CD_SL) = 0").Length > 0)
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, DD("창고"));
                    return false;
                }
            }
            return true;
        }
    }

    #endregion

    #region -> 단가정보
    private void btn단가정보_Click(object sender, EventArgs e)
    {
        try
        {
            if (!_flexD.HasNormalRow) return;

            if (!확정여부()) return;

            if (D.GetString(_flexD["CD_ITEM"]) == "") return;

            P_PU_ITEM_UM_INFO_SUB _dlg = new P_PU_ITEM_UM_INFO_SUB(D.GetString(_flexD["CD_PLANT"]), D.GetString(_flexD["CD_ITEM"]), D.GetString(_flexD["NM_ITEM"]), D.GetString(_flexD["STND_ITEM"]));

            if (_dlg.ShowDialog() == DialogResult.OK)
            {
                _flexD["UM_EX_PO"] = _dlg.um_info;

                금액계산(_flexD.Row, D.GetDecimal(_flexD["UM_EX_PO"]), _flexD.CDecimal(_flexD[_flexD.Row, "QT_PO_MM"]), "UM_EX_PO", D.GetDecimal(_flexD["UM_EX_PO"]));
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

    #region -> 확정여부
    private bool 확정여부()
    {
        try
        {
            if (!_flexD.HasNormalRow) return false;

            foreach (DataRow dr in _flexD.DataTable.Rows)
            {
                if (dr["FG_POST"].ToString().Trim() != "O" || !차수여부)
                {
                    ShowMessage(DD("발주 확정/종결 건은 처리할 수 없습니다"));
                    return false;
                }

            }

            return true;
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
            return false;
        }
    }
    #endregion

    private void btn적용1_Click(object sender, EventArgs e)
    {
        try
        {
            if (_flexD.DataTable.Rows == null || tb_DT_LIMIT.Text == "") return;
          
                foreach(DataRow row in _flexD.DataTable.Rows)
                {
                      row["DT_LIMIT"] = tb_DT_LIMIT.Text;
                      row["DT_PLAN"] = tb_DT_LIMIT.Text;
                   
                }
                    ShowMessage("적용작업을완료하였습니다");

        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }

    }
    #region 매입형태적용
    void btnFG_TPPURCV_Click(object sender, EventArgs e)
    {
        try
        {
            if (_flexD.HasNormalRow)
            {
                foreach (DataRow dr in _flexD.DataTable.Rows)
                    dr["FG_PURCHASE"] = cbo_FG_TPPURCV.SelectedValue;
            }

            return;
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
    }
    #endregion

    #region 공급가액 부가세
    private void cur_AM_K_IV_Validated(object sender, EventArgs e)
    {
        try
        {
            if (_flexD.HasNormalRow)
            {

                decimal SUM_AM = decimal.Zero;
                decimal SUM_VAT = decimal.Zero;
                int ROW_COUNT = _flexD.Rows.Count - _flexD.Rows.Fixed;

                SUM_AM = D.GetDecimal(_flexD.DataTable.Compute("SUM(AM)", ""));
                decimal vsoldValue = (SUM_AM + 99);
                decimal vmoldValue = (SUM_AM - 99);

                cur_VAT_TAX_IV.DecimalValue = Unit.원화금액(DataDictionaryTypes.PU,cur_AM_K_IV.DecimalValue * tb_TAX.DecimalValue * 0.01M);

                if (cur_AM_K_IV.DecimalValue > vsoldValue || cur_AM_K_IV.DecimalValue < vmoldValue)
                {
                    ShowMessage("수정가능금액은 100원 범위 입니다.");
                    cur_AM_K_IV.DecimalValue = SUM_AM;
                    return;
                }

                SUM_AM = decimal.Zero;

                for (int i = _flexD.Rows.Fixed; i < _flexD.Rows.Count; i++)
                {
   
                    SUM_AM += D.GetDecimal(_flexD.Rows[i]["AM"]);
                    SUM_VAT += D.GetDecimal(_flexD.Rows[i]["VAT"]);
                }

                _flexD.Rows[ROW_COUNT +1]["AM"] = D.GetDecimal(_flexD.Rows[ROW_COUNT +1]["AM"]) + (cur_AM_K_IV.DecimalValue - SUM_AM);
                _flexD.Rows[ROW_COUNT + 1]["VAT"] = D.GetDecimal(_flexD.Rows[ROW_COUNT + 1]["VAT"]) + (cur_VAT_TAX_IV.DecimalValue - SUM_VAT);
                _flexD.Rows[ROW_COUNT + 1]["AM_TOTAL"] = D.GetDecimal(_flexD.Rows[ROW_COUNT + 1]["VAT"]) + D.GetDecimal(_flexD.Rows[ROW_COUNT + 1]["AM"]);

                if (D.GetDecimal(_flexD.Rows[ROW_COUNT + 1]["QT_PO"]) != 0)
                {
                    _flexD.Rows[ROW_COUNT + 1]["UM"] = D.GetDecimal(_flexD.Rows[ROW_COUNT + 1]["AM"]) / D.GetDecimal(_flexD.Rows[ROW_COUNT + 1]["QT_PO"]);
                }

                if (D.GetDecimal(_flexD.Rows[ROW_COUNT + 1]["QT_PO_MM"]) != 0)
                {
                    _flexD.Rows[ROW_COUNT + 1]["UM_P"] = D.GetDecimal(_flexD.Rows[ROW_COUNT + 1]["AM"]) / D.GetDecimal(_flexD.Rows[ROW_COUNT + 1]["QT_PO_MM"]);
                }
            }

            return;
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }

    }
    void cur_VAT_TAX_IV_Validated(object sender, EventArgs e)
    {
        try
        {
            if (_flexD.HasNormalRow)
            {
                decimal SUM_VAT = decimal.Zero;
                int ROW_COUNT = _flexD.Rows.Count - _flexD.Rows.Fixed;

                for (int i = _flexD.Rows.Fixed; i < _flexD.Rows.Count; i++)
                {
                    SUM_VAT += D.GetDecimal(_flexD.Rows[i]["VAT"]);
                }

                _flexD.Rows[ROW_COUNT + 1]["VAT"] = D.GetDecimal(_flexD.Rows[ROW_COUNT + 1]["VAT"]) + (cur_VAT_TAX_IV.DecimalValue - SUM_VAT);
                _flexD.Rows[ROW_COUNT + 1]["AM_TOTAL"] = D.GetDecimal(_flexD.Rows[ROW_COUNT + 1]["VAT"]) + D.GetDecimal(_flexD.Rows[ROW_COUNT + 1]["AM"]);
            }

            return;
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }
    }
    #endregion

    #endregion

    private void MA_Pjt_Setting()
    {
        try
        {

            dt공장 = MA.GetCode("MA_B000093");

            DataRow[] DataNum1 = dt공장.Select("CODE = '04'");
            object[] ObjDataNum1 = DataNum1[0].ItemArray;
            NUM_USERDEF4 = ObjDataNum1[1].ToString();

            DataRow[] DataNum2 = dt공장.Select("CODE = '05'");
            object[] ObjDataNum2 = DataNum2[0].ItemArray;
            NUM_USERDEF5 = ObjDataNum2[1].ToString();


            DataRow[] DataNum3 = dt공장.Select("CODE = '06'");
            object[] ObjDataNum3 = DataNum3[0].ItemArray;
            NUM_USERDEF6 = ObjDataNum3[1].ToString();
        }
        catch
        {
        }
    }

    private void btn환정보적용_Click(object sender, EventArgs e)
    {
        try
        {

            foreach (DataRow dr in _flexD.DataTable.Rows)
            {
                부가세계산(dr);
            //    dr["UM_EX_PO"] = D.GetDecimal(dr["UM_EX"]) * D.GetDecimal(_flexD["RT_PO"]);
            //    dr["CD_EXCH"] = D.GetString(dr["CD_EXCH"]);
            //    dr["QT_PO_MM"] = D.GetDecimal(dr["QT_NEED_JAN"]);
            //    dr["QT_PO"] = (D.GetDecimal(_flexD["QT_PO_MM"]) * D.GetDecimal(_flexD["RT_PO"]));

            //    if (D.GetString(dr["CD_EXCH"]) != "" && D.GetString(dr["RT_EXCH"]) != "")
            //    {
            //        cbo_NM_EXCH.SelectedValue = D.GetString(dr["CD_EXCH"]);
            //        tb_NM_EXCH.Text = D.GetString(dr["RT_EXCH"]);
            //        _header.CurrentRow["CD_EXCH"] = D.GetString(dr["CD_EXCH"]);
            //        _header.CurrentRow["RT_EXCH"] = D.GetDecimal(dr["RT_EXCH"]);
            //    }
           }
        }
        catch (Exception ex)
        {
            MsgEnd(ex);
        }

    }

    private void Setting_pu_poh_sub()
    {
        /*PU_TPPO.TP_GR(자동프로세스) = 100(발주), 101(발주 - 가입고), 102(발주 - 의뢰), 
          103(발주 - 의뢰 - 입고), 104(발주 - 의뢰 - 입고 - 매입) */
        if (_header.CurrentRow["TP_GR"].ToString() == "104" && !m_tab_poh.TabPages.Contains(tabPage7))
            m_tab_poh.TabPages.Add(tabPage7);
  
        else if (_header.CurrentRow["TP_GR"].ToString() != "104" && m_tab_poh.TabPages.Contains(tabPage7))
            m_tab_poh.TabPages.Remove(tabPage7);
    }

    #region ▷ 원그리드 적용하기

    void 원그리드적용하기()
    {
        System.Drawing.Size s1 = oneGrid1.Size;

        oneGrid1.UseCustomLayout = true;
        oneGrid2.UseCustomLayout = true;
        oneGrid3.UseCustomLayout = true;
        setNecessaryCondition(new object[] { bpPanelControl7.Name, bpPanelControl8.Name, bpPanelControl14.Name,
                                             bpPanelControl15.Name, bpPanelControl16.Name, bpPanelControl21.Name,
                                             bpPanelControl21.Name, bpPanelControl22.Name, bpPanelControl23.Name},
                              oneGrid1, false);
        //setNecessaryCondition(new object[] { }, oneGrid2);
        setNecessaryCondition(new object[] { }, oneGrid3, true);

        oneGrid1.IsSearchControl = false;   //  2013.07.24 - 윤성우 - ONEGRID 수정작업(등록단)
        oneGrid2.IsSearchControl = false;   //  2013.07.24 - 윤성우 - ONEGRID 수정작업(등록단)
        oneGrid3.IsSearchControl = false;   //  2013.07.24 - 윤성우 - ONEGRID 수정작업(등록단)

        oneGrid1.InitCustomLayout();
        oneGrid2.InitCustomLayout();
        oneGrid3.InitCustomLayout();

        //tabpage resizing 하기
        System.Drawing.Size s2 = oneGrid1.Size;
        System.Drawing.Size t = m_tab_poh.Size;
        t.Height = t.Height + (s2.Height - s1.Height);
        m_tab_poh.Size = t;

        if (oneGrid2.Size.Height > oneGrid1.Size.Height)
        {
            t = oneGrid2.Size;
            t.Height = oneGrid2.Height + (s2.Height - s1.Height);
            oneGrid2.Size = t;
        }

        return;
    }

    #endregion

    #region -> 중량계산
    void calcAM(int row)
    {
        if (bStandard)
        {
            if (Global.MainFrame.ServerKey == "SINJINSM" || Global.MainFrame.ServerKey == "SQL_108" || Global.MainFrame.ServerKey == "DZSQL")
            {
                if (D.GetDecimal(_flexD[row, "UM_WEIGHT"]) != 0)
                {
                    switch (D.GetString(_flexD[row, "SG_TYPE"]))
                    {
                        case "100":
                        case "200":
                        case "400":
                            _flexD[row, "WEIGHT"] = D.GetDecimal(_flexD[row, "NUM_STND_ITEM_1"]) * D.GetDecimal(_flexD[row, "NUM_STND_ITEM_2"]) *
                                                 D.GetDecimal(_flexD[row, "NUM_STND_ITEM_3"]) * D.GetDecimal(_flexD[row, "QT_SG"]) / 1000000;

                            break;
                        case "300":
                            _flexD[row, "WEIGHT"] = (D.GetDecimal(_flexD[row, "NUM_STND_ITEM_1"]) + D.GetDecimal("1.5")) * D.GetDecimal(_flexD[row, "NUM_STND_ITEM_2"]) *
                                                 D.GetDecimal(_flexD[row, "NUM_STND_ITEM_3"]) * D.GetDecimal(_flexD[row, "QT_SG"]) / 1000000;
                            //_flexD[row, "QT_WEIGHT"] =  * D.GetDecimal(_flexD[row, "NUM_STND_ITEM_2"]) *
                            //                     D.GetDecimal(_flexD[row, "NUM_STND_ITEM_3"]) * D.GetDecimal(_flexD[row, "QT_SG"]) / 1000000 * D.GetDecimal(_flexD[row, "QT_PR"]),0) ;
                            break;

                    }
                    _flexD[row, "TOT_WEIGHT"] = Math.Round(D.GetDecimal(_flexD[row, "WEIGHT"]) * D.GetDecimal(_flexD[row, "QT_PO_MM"]), 1);

                    if (D.GetDecimal(_flexD[row, "TOT_WEIGHT"]) != 0)
                    {

                        // 단위 환산량
                        decimal ldb_rateExchg = _flexD.CDecimal(_flexD[row, "RT_PO"]) == 0 ? 1 : _flexD.CDecimal(_flexD[row, "RT_PO"]);

                        _flexD[row, "AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, Math.Round(D.GetDecimal(_flexD[row, "TOT_WEIGHT"]) * D.GetDecimal(_flexD[row, "UM_WEIGHT"])));
                        if (D.GetDecimal(_flexD[row, "QT_PO_MM"]) != 0)
                            _flexD[row, "UM_EX"] = UDecimal.Getdivision(D.GetDecimal(_flexD[row, "AM_EX"]) / D.GetDecimal(_flexD[row, "QT_PO_MM"]), ldb_rateExchg);
                        else
                            _flexD[row, "UM_EX"] = 0;
                        _flexD[row, "AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flexD[row, "AM_EX"]) * D.GetDecimal(_header.CurrentRow["RT_EXCH"]));


                        _flexD[row, "UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, _flexD.CDecimal(_flexD[row, "UM_EX"]) * ldb_rateExchg);

                        _flexD["UM_P"] = Unit.원화단가(DataDictionaryTypes.PU, _flexD.CDecimal(_flexD[row, "UM_EX"]) * D.GetDecimal(_header.CurrentRow["RT_EXCH"]));
                        _flexD["UM"] = Unit.원화단가(DataDictionaryTypes.PU, _flexD.CDecimal(_flexD[row, "UM_EX"]) / ldb_rateExchg * D.GetDecimal(_header.CurrentRow["RT_EXCH"]));
                    }
                    else
                    {
                        _flexD[row, "AM_EX"] = 0;
                        _flexD[row, "UM_EX"] = 0;
                        _flexD[row, "AM"] = 0;
                        _flexD[row, "UM_EX_PO"] = 0;
                        _flexD[row, "UM_P"] = 0;
                        _flexD[row, "UM_"] = 0;
                    }
                    부가세만계산();
                }

            }
        }



    }


    void calcAM(int row, decimal TOT_WEIGHT)
    {
        if (bStandard)
        {

            if (Global.MainFrame.ServerKey == "SINJINSM" || Global.MainFrame.ServerKey == "SQL_108" || Global.MainFrame.ServerKey == "DZSQL")
            {

                if (D.GetDecimal(_flexD[row, "UM_WEIGHT"]) != 0)
                {
                    // 단위 환산량
                    decimal ldb_rateExchg = _flexD.CDecimal(_flexD[row, "RT_PO"]) == 0 ? 1 : _flexD.CDecimal(_flexD[row, "RT_PO"]);

                    _flexD[row, "AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, Math.Round(TOT_WEIGHT * D.GetDecimal(_flexD[row, "UM_WEIGHT"])));
                    if (D.GetDecimal(_flexD[row, "QT_PO_MM"]) != 0)
                        _flexD[row, "UM_EX"] = UDecimal.Getdivision(D.GetDecimal(_flexD[row, "AM_EX"]) / D.GetDecimal(_flexD[row, "QT_PO_MM"]), ldb_rateExchg);
                    else
                        _flexD[row, "UM_EX"] = 0;
                    _flexD[row, "AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flexD[row, "AM_EX"]) * D.GetDecimal(_header.CurrentRow["RT_EXCH"]));


                    _flexD[row, "UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, _flexD.CDecimal(_flexD[row, "UM_EX"]) * ldb_rateExchg);

                    _flexD["UM_P"] = Unit.원화단가(DataDictionaryTypes.PU, _flexD.CDecimal(_flexD[row, "UM_EX"]) * D.GetDecimal(_header.CurrentRow["RT_EXCH"]));
                    _flexD["UM"] = Unit.원화단가(DataDictionaryTypes.PU, _flexD.CDecimal(_flexD[row, "UM_EX"]) / ldb_rateExchg * D.GetDecimal(_header.CurrentRow["RT_EXCH"]));
                }

                부가세만계산();
            }
        }

    }

    #endregion

}

#endregion


}






using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.BizOn.Erpu.Net.File;
using Duzon.Common.BpControls;
using Duzon.Common.ConstLib;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.Erpiu.Windows.OneControls;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.MF.EMail;
using Duzon.ERPU.MF.SMS;
using Duzon.ERPU.OLD;
using Duzon.ERPU.PU.Common;
using Duzon.Windows.Print;
using DzHelpFormLib;
using pm;
using pur;
using sale;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_PU_PO_WINTEC : PageBase
    {
        private P_CZ_PU_PO_WINTEC_BIZ _biz = new P_CZ_PU_PO_WINTEC_BIZ();
        private FreeBinding _header = null;
        private string str발주번호;
        private string _ComfirmState;
        private CDT_PU_RCVH cPU_RCVH = new CDT_PU_RCVH();
        private CDT_PU_RCV cPU_RCVL = new CDT_PU_RCV();
        private string str복사구분 = string.Empty;
        private bool 호출여부 = false;
        private bool 비고수정여부 = false;
        private string strSOURCE;
        private decimal dNO_LINE;
        private ReportHelper rptHelper;
        private string _전용설정 = "000";
        private string m_sEnv = "N";
        private string m_sEnv_CC = "000";
        private string m_sEnv_CC_Line = "N";
        private string m_sEnv_CC_Menu = "000";
        private string m_sEnv_FG_TAX = "000";
        private string m_sEnv_Prt_Option = "000";
        private string m_sEnv_App_Am = "000";
        private string m_sEnv_Nego = BASIC.GetMAEXC("발주등록(공장)-업체별프로세스선택");
        private string m_sEnv_App_Sort = "000";
        private string m_Elec_app = "000";
        private string _m_partner_use = "000";
        private string _m_partner_change = "000";
        private string _m_tppo_use = "000";
        private string _m_tppo_change = "000";
        private string _m_lotsize_use = "000";
        private string _구매예산CHK설정 = "N";
        private string _구매예산CHK설정FI = "000";
        private string _YN_CdBizplan = "0";
        private DataTable _dt_pjt = null;
        private string _m_dt = "000";
        private string _m_Company_only = "000";
        private string _반품발주 = BASIC.GetMAEXC("반품발주사용여부");
        private DataTable dt공장 = null;
        private DataTable _dt_app = null;
        private string sPUSU = BASIC.GetMAEXC("구매발주-외주유무");
        private string sPUIV = BASIC.GetMAEXC("발주등록-기성매입 사용유무");
        private string s소요자재체크 = BASIC.GetMAEXC("발주등록-외주소요자재체크사용");
        private string sFG_TAXcheck = BASIC.GetMAEXC_Menu(nameof(P_CZ_PU_PO_WINTEC), "PU_A00000001");
        private bool bStandard = false;
        private string NUM_USERDEF4 = nameof(NUM_USERDEF4);
        private string NUM_USERDEF5 = nameof(NUM_USERDEF5);
        private string NUM_USERDEF6 = nameof(NUM_USERDEF6);
        private string _APP_USERDEF = "N";
        private string Tp_print = "";
        private bool _YN_REBATE = false;
        private decimal d_SEQ_PROJECT = 0M;
        private string s_CD_PJT_ITEM = string.Empty;
        private string s_NM_PJT_ITEM = string.Empty;
        private string s_PJT_ITEM_STND = string.Empty;
        private string s_CD_PARTNER_PJT = string.Empty;
        private string s_NM_PARTNER_PJT = string.Empty;
        private string s_NO_EMP_PJT = string.Empty;
        private string s_NM_EMP_PJT = string.Empty;
        private string s_END_USER = string.Empty;
        private string _지급관리통제설정 = "N";
        private string _지급예정일통제설정 = "000";
        private string s_vat_fictitious = BASIC.GetMAEXC("발주등록(공장)-의제부가세적용");
        private string s_PTR_SUB = BASIC.GetMAEXC("거래처부가정보-발주정보매핑여부");
        private P_PU_OPTION_INFO_SUB _infosub_dlg = new P_PU_OPTION_INFO_SUB("", "", true);
        private string _m_pjtbom_rq_mng = "000";
        private bool b단가권한 = true;
        private bool b금액권한 = true;
        private string MNG_SERIAL = string.Empty;

        public P_CZ_PU_PO_WINTEC() : this("")
        {
        }

        public P_CZ_PU_PO_WINTEC(string str발주번호) : this(str발주번호, 0M, "")
        {
        }

        public P_CZ_PU_PO_WINTEC(string str발주번호, decimal dNO_LINE, string strSOURCE)
        {
            try
            {
                this.InitializeComponent();
                
                this.str발주번호 = str발주번호;
                this.dNO_LINE = dNO_LINE;
                this.strSOURCE = strSOURCE;
                this.DataChanged += new EventHandler(this.Page_DataChanged);
                this._header = new FreeBinding();
                this._header.JobModeChanged += new FreeBindingEventHandler(this._header_JobModeChanged);
                this._header.ControlValueChanged += new FreeBindingEventHandler(this._header_ControlValueChanged);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public P_CZ_PU_PO_WINTEC(PageBaseConst.CallType pageCallType, string idMemo)
          : this("")
        {
            this.str발주번호 = this._biz.GetNoPo(idMemo);
        }

        public override void OnCallExistingPageMethod(object sender, PageEventArgs e)
        {
            this.str발주번호 = D.GetString(e.Args[0]);
            base.InitPaint();
        }

        private void ControlButtonEnabledDisable(Control ctr, bool lb_enable)
        {
            if (ctr == null || ctr.Name == "_flexD")
            {
                this.btn품의적용.Enabled = lb_enable;
                this.btn요청적용.Enabled = lb_enable;
                this.btn품목전개.Enabled = lb_enable;
                this.btn품목추가.Enabled = lb_enable;
                this.btn품목삭제.Enabled = lb_enable;
                this.ctx프로젝트.Enabled = lb_enable;
                this.btnPJT적용.Enabled = lb_enable;
                this.btn수주적용.Enabled = lb_enable;
                this.btn수주_의뢰적용.Enabled = lb_enable;
                this.ctx창고.Enabled = lb_enable;
                this.btn창고적용.Enabled = lb_enable;
                if (this.MainFrameInterface.ServerKeyCommon == "INITECH" || this.MainFrameInterface.ServerKeyCommon == "ANJUN" || this.MainFrameInterface.ServerKeyCommon == "KPCI")
                {
                    this.btn_INST.Enabled = lb_enable;
                    this.btn_INST.Visible = true;
                }
                else
                {
                    this.btn_INST.Enabled = false;
                    this.btn_INST.Visible = false;
                }
            }
            else
            {
                this.btn품의적용.Enabled = false;
                this.btn요청적용.Enabled = false;
                this.btn수주적용.Enabled = false;
                this.btn품목전개.Enabled = false;
                this.btn품목추가.Enabled = true;
                this.btn품목삭제.Enabled = true;
                this.btn수주_의뢰적용.Enabled = false;
                this.btn_INST.Enabled = false;
                this.ctx담당자.Enabled = true;
                this.txt담당부서.Enabled = true;
                this.cbo지급조건.Enabled = true;
                this.panel23.Enabled = true;
                this.ctx프로젝트.Enabled = true;
                this.btnPJT적용.Enabled = true;
                this.btn메일전송.Enabled = true;
                this.ctx창고.Enabled = true;
                this.btn창고적용.Enabled = true;
                if (!(ctr is RoundedButton))
                    return;
                switch (ctr.Name)
                {
                    case "btn품의적용":
                        this.btn품의적용.Enabled = lb_enable;
                        if (this._header.JobMode == 0)
                        {
                            this._header.CurrentRow["TP_PROCESS"] = "3";
                            break;
                        }
                        break;
                    case "btn요청적용":
                        this.btn요청적용.Enabled = lb_enable;
                        if (this._header.JobMode == 0)
                        {
                            this._header.CurrentRow["TP_PROCESS"] = "1";
                            break;
                        }
                        break;
                    case "btn수주적용":
                        this.btn수주적용.Enabled = lb_enable;
                        if (this._header.JobMode == 0)
                        {
                            this._header.CurrentRow["TP_PROCESS"] = "4";
                            break;
                        }
                        break;
                    case "btnPJT관련":
                        this.btnPJT관련.Enabled = lb_enable;
                        this.btn품목전개.Enabled = lb_enable;
                        this.btn품목추가.Enabled = lb_enable;
                        break;
                    case "btn품목전개":
                        this.btn품목전개.Enabled = lb_enable;
                        this.btn품목추가.Enabled = lb_enable;
                        if (this._header.JobMode == 0)
                        {
                            this._header.CurrentRow["TP_PROCESS"] = "2";
                            break;
                        }
                        break;
                    case "btn품목추가":
                        this.btn품목전개.Enabled = lb_enable;
                        this.btn품목추가.Enabled = lb_enable;
                        if (this._header.JobMode == 0)
                        {
                            this._header.CurrentRow["TP_PROCESS"] = "2";
                            break;
                        }
                        break;
                    case "btnH41적용":
                        this.btn품목추가.Enabled = lb_enable;
                        break;
                    case "btn수주_의뢰적용":
                        this.btn수주_의뢰적용.Enabled = lb_enable;
                        if (this._header.JobMode == 0)
                        {
                            this._header.CurrentRow["TP_PROCESS"] = "4";
                            break;
                        }
                        break;
                    case "btn_INST":
                        this.btn_INST.Enabled = lb_enable;
                        if (this._header.JobMode == 0)
                        {
                            this._header.CurrentRow["TP_PROCESS"] = "4";
                            break;
                        }
                        break;
                }
            }
        }

        private void SetHeadControlEnabled(bool isEnabled, int pi_type)
        {
            this.dtp발주일자.Enabled = isEnabled;
            this.ctx거래처.Enabled = isEnabled;
            this.ctx구매그룹.Enabled = isEnabled;
            this.ctx발주형태.Enabled = isEnabled;
            this.cbo단가유형.Enabled = isEnabled;
            this.ctx담당자.Enabled = isEnabled;
            this.cbo부가세여부.Enabled = isEnabled;
            this.cbo공장.Enabled = isEnabled;
            this.cbo환정보.Enabled = isEnabled;
            if (pi_type == 2 || pi_type == 3)
            {
                this.ctx프로젝트.Enabled = isEnabled;
                this.rdo일괄.Enabled = isEnabled;
                this.rdo건별.Enabled = isEnabled;
                this.btn품목추가.Enabled = isEnabled;
                this.btn품목삭제.Enabled = isEnabled;
                this.btn품목전개.Enabled = isEnabled;
                this.btnPJT적용.Enabled = isEnabled;
                this.ctx창고.Enabled = isEnabled;
                this.btn창고적용.Enabled = isEnabled;
                if (pi_type == 3)
                {
                    this.cbo공장.Enabled = false;
                    this.cur부가세율.Enabled = isEnabled;
                }
            }
            switch (pi_type)
            {
                case 1:
                    this.cur부가세율.Enabled = isEnabled;
                    break;
                case 4:
                    if (Global.MainFrame.ServerKeyCommon.ToUpper() == "KORAVL")
                    {
                        this.cbo환정보.Enabled = true;
                        if (D.GetString(this.cbo환정보.SelectedValue) != "000")
                            this.cur환정보.Enabled = true;
                    }
                this.btnPJT적용.Enabled = isEnabled;
                    break;
            }
            if (pi_type == 5 && this._m_partner_use == "100")
                this.ctx거래처.Enabled = true;
            if (pi_type != 6)
                return;
            int num;
            if (!(Global.MainFrame.ServerKeyCommon == "DAEJOOKC"))
                num = !MA.ServerKey(false, new string[] { "ABLBIO" }) ? 1 : 0;
            else
                num = 0;
            if (num == 0)
            {
                this.ctx발주형태.Enabled = true;
                this.ctx구매그룹.Enabled = true;
                this.ctx담당자.Enabled = true;
                this.dtp발주일자.Enabled = true;
                this.cur부가세율.Enabled = false;
            }
            else
            {
                this.chk업체전용1.Enabled = true;
                this.chk업체전용2.Enabled = true;
                this.cbo업체전용1.Enabled = true;
                this.cbo업체전용2.Enabled = true;
            }
        }

        private void _header_JobModeChanged(object sender, FreeBindingArgs e)
        {
            try
            {
                this.cur부가세율.Enabled = false;
                if (e.JobMode == JobModeEnum.조회후수정)
                {
                    this._header.SetControlEnabled(false);
                    this.ctx담당자.Enabled = true;
                    this.txt비고.Enabled = true;
                    this.txt비고2.Enabled = true;
                    this.cbo운송방법.Enabled = true;
                    this.cbo지불조건.Enabled = true;
                    this.txt지불조건.Enabled = true;
                    this.cbo가격조건.Enabled = true;
                    this.txt도착지.Enabled = true;
                    this.txt선적지.Enabled = true;
                    this.ctx도착지.Enabled = true;
                    this.ctx선적지.Enabled = true;
                    this.txt발주텍스트비고1.Enabled = true;
                    this.cbo선적조건.Enabled = true;
                    this.cbo운임조건.Enabled = true;
                    this.txt발주텍스트비고2.Enabled = true;
                    this.cbo지급기준.Enabled = true;
                    this.cur지불조건.Enabled = true;
                    this.cbo원산지.Enabled = true;
                    this.cbo포장형태.Enabled = true;
                    this.txt인도조건.Enabled = true;
                    this.txt인도기한.Enabled = true;
                    this.txt유효기일.Enabled = true;
                    this.ctx공급자.Enabled = true;
                    this.ctx제조사.Enabled = true;
                    this.txt검사정보.Enabled = true;
                    this.txt필수서류.Enabled = true;
                    this.cur운송비용.Enabled = true;
                    this.txt오더번호.Enabled = true;
                    this.txt포장형태.Enabled = true;
                    this.btn_FILE_UPLOAD.Enabled = true;
                    this.cbo매입형태.Enabled = false;
                    this.btn매입형태적용.Enabled = false;
                    this.btnSMS.Enabled = true;
                    if (this._flexD.HasNormalRow && this._flexD[this._flexD.Rows.Fixed, "FG_POST"].ToString() == "R")
                    {
                        this.btnNEGO적용.Enabled = false;
                        this.curDe.Enabled = false;
                        this.curNEGO금액.Enabled = false;
                    }
                    else
                        this.curNEGO금액.Enabled = true;
                    this.cbo업체전용1.Enabled = true;
                    this.cbo업체전용2.Enabled = true;
                    this.chk업체전용1.Enabled = true;
                    this.chk업체전용2.Enabled = true;
                    this.ctx업체전용.Enabled = true;
                }
                else
                {
                    this._header.SetControlEnabled(true);
                    this.txt담당부서.Enabled = false;
                    this.btn_FILE_UPLOAD.Enabled = false;
                    this.cbo단가유형.SelectedIndex = 0;
                    this._header.CurrentRow["FG_UM"] = this.cbo단가유형.SelectedValue.ToString();
                    this.cbo과세구분.SelectedIndex = 0;
                    this.cbo부가세여부.SelectedIndex = 1;
                    this._header.CurrentRow["TP_UM_TAX"] = this.cbo부가세여부.SelectedValue.ToString();
                    this.cbo매입형태.Enabled = true;
                    this.btn매입형태적용.Enabled = true;
                    this.btnSMS.Enabled = false;
                    this.cbo환정보.SelectedIndex = 0;
                    this.cur가용재고량.DecimalValue = 0M;
                    this.cur현재고량.DecimalValue = 0M;
                    this.cur발주량.DecimalValue = 0M;
                    this.cur의뢰량.DecimalValue = 0M;
                    if (this.cbo공장.SelectedValue != null && this.str복사구분 != "COPY" && Global.MainFrame.LoginInfo.CdPlant == string.Empty && ((DataTable)this.cbo공장.DataSource).Rows.Count > 0)
                    {
                        this.cbo공장.SelectedIndex = 0;
                        this._header.CurrentRow["CD_PLANT"] = this.cbo공장.SelectedValue.ToString();
                    }
                    if (D.GetString(Global.MainFrame.LoginInfo.PurchaseGroupCode) == string.Empty)
                    {
                        if (D.GetString(this._header.CurrentRow["CD_PURGRP"]) == string.Empty)
                            this._header.CurrentRow["CD_PURGRP"] = Settings1.Default.CD_PURGRP_SET;
                    }
                    else
                        this._header.CurrentRow["CD_PURGRP"] = Global.MainFrame.LoginInfo.PurchaseGroupCode;
                    if (this._header.CurrentRow["CD_TPPO"].ToString() == string.Empty && Global.MainFrame.ServerKeyCommon != "WGSK")
                        this._header.CurrentRow["CD_TPPO"] = Settings1.Default.CD_TPPO_SET;
                    if (this._header.CurrentRow["FG_PAYMENT"].ToString() == string.Empty)
                    {
                        this._header.CurrentRow["FG_PAYMENT"] = Settings1.Default.FG_PAYMENT_SET;
                        if (this._header.CurrentRow["FG_PAYMENT"].ToString() == string.Empty)
                            this._header.CurrentRow["FG_PAYMENT"] = "000";
                        this.cbo지급조건.SelectedValue = this._header.CurrentRow["FG_PAYMENT"].ToString();
                    }
                    this._header.CurrentRow["TP_UM_TAX"] = Settings1.Default.TP_UM_TAX;
                    this.cbo부가세여부.SelectedValue = this._header.CurrentRow["TP_UM_TAX"];
                    this.기초값설정();
                    this.ControlButtonEnabledDisable(null, true);
                    this.btnNEGO적용.Enabled = true;
                    this.curDe.Enabled = true;
                }
                if (!Global.MainFrame.ServerKeyCommon.Contains("KAHP"))
                    return;
                if (D.GetString(this._header.CurrentRow["TXT_USERDEF4"]) != "")
                {
                    this.btnCompanyUse1.Enabled = false;
                    this.btnCompanyUse2.Enabled = true;
                    this.btn품의적용.Enabled = false;
                    this.btn요청적용.Enabled = false;
                    this.btn수주적용.Enabled = false;
                    this.flowLayoutPanel3.Enabled = false;
                }
                else
                {
                    this.btnCompanyUse1.Enabled = true;
                    this.btnCompanyUse2.Enabled = false;
                    this.btn품의적용.Enabled = true;
                    this.btn요청적용.Enabled = true;
                    this.btn수주적용.Enabled = true;
                    this.flowLayoutPanel3.Enabled = true;
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
                this.Page_DataChanged(null, null);
                if (sender == null)
                    ;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override void InitLoad()
        {
            base.InitLoad();
            this.MA_EXC_SETTING();
            if (this._m_Company_only == "003")
                this.MA_Pjt_Setting();
            this.InitGridD();
            this.InitEvent();
            this.BTN_LOCATION_SETTING();
            if (this.sPUSU != "100")
            {
                this.tabControlExt1.TabPages.RemoveAt(1);
                if (this.sPUIV != "100" && this.sPUIV != "200")
                {
                    this.tabControlExt1.TabPages.RemoveAt(1);
                    this.btn일괄발행.Visible = false;
                }
            }
            else if (this.sPUIV != "100" && this.sPUIV != "200")
            {
                this.tabControlExt1.TabPages.RemoveAt(2);
                this.btn일괄발행.Visible = false;
            }
            if (Global.MainFrame.ServerKeyCommon != "TOPES" && Global.MainFrame.ServerKeyCommon != "DZSQL" && Global.MainFrame.ServerKeyCommon != "SQL_")
                this.m_tab_poh.TabPages.Remove(this.tabPage8);
            else
                this._flexH.Binding = this._biz.GetTopes(this.dtp발주일자.Text);
            if (Global.MainFrame.ServerKeyCommon == "MHIK")
            {
                this.txt발주번호.Visible = false;
                this.tb_NO_PO_MH.Visible = true;
            }
            else
            {
                this.txt발주번호.Visible = true;
                this.tb_NO_PO_MH.Visible = false;
            }
            if (!(Global.MainFrame.ServerKeyCommon == "DEMAC"))
                return;
            this.btn품목전개.Text = "입고단가";
        }

        protected override void InitPaint()
        {
            base.InitPaint();
            this.InitControl();
            this.원그리드적용하기();
            string empty = string.Empty;
            switch (this.strSOURCE)
            {
                case "PU_POL":
                    DataSet dataSet1 = this._biz.Search("", "");
                    this._header.SetBinding(dataSet1.Tables[0], this.m_tab_poh);
                    this._header.ClearAndNewRow();
                    if (this.sPUSU == "100")
                        this._flexDD.Binding = this._biz.SearchDetail("sDFS", "SDFSD", 0M);
                    if (this.sPUIV == "100" || this.sPUIV == "200")
                        this._flexIV.Binding = dataSet1.Tables[2];
                    this._flexD.Binding = dataSet1.Tables[1];
                    this.조회(this.str발주번호, "OK", "");
                    if (!this._flexD.HasNormalRow)
                        break;
                    this._flexD.Row = this._flexD.FindRow(this.dNO_LINE, this._flexD.Rows.Fixed, this._flexD.Cols["NO_LINE"].Index, true);
                    break;
                default:
                    if (!string.IsNullOrEmpty(this.str발주번호))
                    {
                        this.Reload(this.str발주번호);
                    }
                    else
                    {
                        DataSet dataSet2 = this._biz.Search("", "");
                        this._header.SetBinding(dataSet2.Tables[0], this.m_tab_poh);
                        this._header.ClearAndNewRow();
                        this._flexD.Binding = dataSet2.Tables[1];
                        this.dtp발주일자.Focus();
                        if (this.sPUSU == "100")
                            this._flexDD.Binding = this._biz.SearchDetail("sDFS", "SDFSD", 0M);
                        if (this.sPUIV == "100" || this.sPUIV == "200")
                            this._flexIV.Binding = dataSet2.Tables[2];
                        this.cbo_NM_EXCH_SelectionChangeCommitted(null, null);
                        this.Setting_pu_poh_sub();
                        if (D.GetString(dataSet2.Tables[0].Rows[0]["NO_PO"]) != string.Empty)
                        {
                            this._header.AcceptChanges();
                            this._flexD.AcceptChanges();
                        }
                    }
                    if (Global.MainFrame.ServerKeyCommon == "SATREC" || Global.MainFrame.ServerKeyCommon == "JSERP" || Global.MainFrame.ServerKeyCommon == "SQL_")
                    {
                        this.txt발주텍스트비고1.Text = D.GetString(Settings1.Default.DC_RMK_TEXT);
                        this._header.CurrentRow["DC_RMK_TEXT"] = D.GetString(Settings1.Default.DC_RMK_TEXT);
                        this.txt발주텍스트비고2.Text = D.GetString(Settings1.Default.DC_RMK_TEXT2);
                        this._header.CurrentRow["DC_RMK_TEXT2"] = D.GetString(Settings1.Default.DC_RMK_TEXT2);
                    }
                    this.dtp매입일자.Text = Global.MainFrame.GetStringToday;
                    this.dtp지급예정일자.Text = Global.MainFrame.GetStringToday;
                    this.dtp만기일자.Text = Global.MainFrame.GetStringToday;
                    break;
            }
        }

        private void InitEvent()
        {
            this.ctx프로젝트.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
            this.ctx프로젝트.QueryAfter += new BpQueryHandler(this.OnBpControl_QueryAfter);
            this.btnNEGO적용.Click += new EventHandler(this.btn배부_Click);
            this.btn매입형태적용.Click += new EventHandler(this.btnFG_TPPURCV_Click);
            this.cur공급가액.Validated += new EventHandler(this.cur_AM_K_IV_Validated);
            this.cur부가세액.Validated += new EventHandler(this.cur_VAT_TAX_IV_Validated);
            this.btn창고적용.Click += new EventHandler(this.btn_SL_apply_Click);
            this.ctx창고.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
            this.btnSMS.Click += new EventHandler(this.btnSMS_Click);
            this.btn업무공유_WBS.Click += new EventHandler(this.btn_PMS_Click);
            this.btn견적적용.Click += new EventHandler(this.btn견적적용_Click);
            this.btn일괄발행.Click += new EventHandler(this.btn_일괄발행_Click);
            this.btn원샷전송.Click += new EventHandler(this.btn_OneShot_Click);
            this.cbo공장.SelectionChangeCommitted += new EventHandler(this.cbo_CD_PLANT_SelectionChangeCommitted);

            this.btn재고확인.Click += new EventHandler(this.btn_CM_INV_Click);
            this.ctx거래처.QueryAfter += new BpQueryHandler(this.OnBpControl_QueryAfter);
            this.dtp발주일자.DateChanged += new EventHandler(this.tb_DT_PO_DateChanged);
            this.ctx담당자.QueryAfter += new BpQueryHandler(this.OnBpControl_QueryAfter);
            this.ctx구매그룹.QueryAfter += new BpQueryHandler(this.OnBpControl_QueryAfter);
            this.btn환정보적용.Click += new EventHandler(this.btn환정보적용_Click);
            this.cbo환정보.SelectionChangeCommitted += new EventHandler(this.cbo_NM_EXCH_SelectionChangeCommitted);
            this.cur환정보.Validated += new EventHandler(this.tb_NM_EXCH_Validated);
            this.ctx발주형태.QueryAfter += new BpQueryHandler(this.OnBpControl_QueryAfter);
            this.ctx발주형태.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
            this.cbo과세구분.SelectionChangeCommitted += new EventHandler(this.cbo_FG_TAX_SelectionChangeCommitted);
            this.btnPJT적용.Click += new EventHandler(this.btn적용_Click);
            this.btn납기일적용.Click += new EventHandler(this.btn적용1_Click);
            this.cbo운송방법.KeyDown += new KeyEventHandler(this.Control_KeyEvent);
            this.cbo지불조건.KeyDown += new KeyEventHandler(this.Control_KeyEvent);
            this.txt지불조건.KeyDown += new KeyEventHandler(this.Control_KeyEvent);
            this.cbo가격조건.KeyDown += new KeyEventHandler(this.Control_KeyEvent);
            this.txt도착지.KeyDown += new KeyEventHandler(this.Control_KeyEvent);
            this.ctx도착지.QueryAfter += new BpQueryHandler(this.OnBpControl_QueryAfter);
            this.ctx도착지.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
            this.ctx선적지.QueryAfter += new BpQueryHandler(this.OnBpControl_QueryAfter);
            this.ctx선적지.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
            this.btn품목삭제.Click += new EventHandler(this.삭제_Click);
            this.btn품목추가.Click += new EventHandler(this.추가_Click);
            this.btn품목전개.Click += new EventHandler(this.품목전개_Click);
            this.btn엑셀업로드.Click += new EventHandler(this.엑셀업로드_Click);
            this.btnLOTSize적용.Click += new EventHandler(this.btn_lotsize_accept_Click);
            this.btn우선순위단가적용.Click += new EventHandler(this.btn_UM_APP_Click);
            this.btn단가정보.Click += new EventHandler(this.btn단가정보_Click);
            this.btnBOM적용.Click += new EventHandler(this.btnBOM_Click);
            this.btn요청적용.Click += new EventHandler(this.요청적용_Click);
            this.btn품의적용.Click += new EventHandler(this.품의적용_Click);
            this.btn수주적용.Click += new EventHandler(this.btn_RE_SO_Click);
            this.btn프로젝트적용.Click += new EventHandler(this.btn_PRJ_SUB_Click);
            this.btn예산chk내역.Click += new EventHandler(this.btn_예산chk내역_Click);
            this.btn예산chk실행.Click += new EventHandler(this.btn_예산chk_Click);
            this.btnH41적용.Click += new EventHandler(this.btn_H41_Apply_Click);
            this.btn메일전송.Click += new EventHandler(this.btn_Mail_Click);
            this.btn수주_의뢰적용.Click += new EventHandler(this.btn_so_gir_Click);
            this.btn_subinfo.Click += new EventHandler(this.btn_subinfo_Click);
            this.btn_FILE_UPLOAD.Click += new EventHandler(this.btn_FILE_UPLOAD_Click);
            this.btn_wbscbs.Click += new EventHandler(this.btn_wbscbs_Click);
        }

        private void InitGridD()
        {
            this.MainGrids = new FlexGrid[] { this._flexD, this._flexDD, this._flexH, this._flexIV };
            this._flexD.DetailGrids = new FlexGrid[] { this._flexDD };

            this._flexD.BeginSetting(1, 1, false);
            this._flexD.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flexD.SetCol("CD_ITEM", "품목코드", 120, 20, true);
            this._flexD.SetCol("NM_ITEM", "품목명", 150, false);
            this._flexD.SetCol("STND_ITEM", "규격", 120, false);
            this._flexD.SetCol("FG_IQCL", "수입검사레벨", 80, false);
            this._flexD.SetCol("CD_UNIT_MM", "발주단위", 70, false);
            this._flexD.SetCol("GRP_ITEM", "품목군코드", false);
            if (this.bStandard)
                this._flexD.SetCol("NM_ITEMGRP", "품목군", 70, true, typeof(string));
            else
                this._flexD.SetCol("NM_ITEMGRP", "품목군", 70, false);
            this._flexD.SetCol("PI_PARTNER", "주거래처코드", 120, false);
            this._flexD.SetCol("PI_LN_PARTNER", "주거래처명", 200, false);
            this._flexD.SetCol("DT_LIMIT", "납기일", 70, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexD.SetCol("DT_PLAN", "납품예정일", 75, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexD.SetCol("QT_PO_MM", "발주량", 80, 17, true, typeof(decimal));
            this._flexD.Cols["QT_PO_MM"].Format = "#,###,###.####";
            this._flexD.SetCol("UNIT_IM", "재고단위", 80, false, typeof(string));
            if (this.bStandard)
            {
                this._flexD.SetCol("CLS_L", "대분류코드", false);
                this._flexD.SetCol("CLS_M", "중분류코드", false);
                this._flexD.SetCol("CLS_S", "소분류코드", false);
                this._flexD.SetCol("NM_CLS_L", "대분류", 140, true, typeof(string));
                this._flexD.SetCol("NM_CLS_M", "중분류", 140, true, typeof(string));
                this._flexD.SetCol("NM_CLS_S", "소분류", 140, true, typeof(string));
                this._flexD.SetCol("NUM_STND_ITEM_1", "NUM_STND_ITEM_1", false);
                this._flexD.SetCol("NUM_STND_ITEM_2", "NUM_STND_ITEM_2", false);
                this._flexD.SetCol("NUM_STND_ITEM_3", "NUM_STND_ITEM_3", false);
                this._flexD.SetCol("NUM_STND_ITEM_4", "NUM_STND_ITEM_4", false);
                this._flexD.SetCol("NUM_STND_ITEM_5", "NUM_STND_ITEM_5", false);
                this._flexD.SetCol("SG_TYPE", "재질구분", false);
                this._flexD.SetCol("QT_SG", "재질", false);
                this._flexD.SetCol("WEIGHT", "단위중량", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
                this._flexD.SetCol("TOT_WEIGHT", "총중량", 80, true, typeof(decimal), FormatTpType.EXCHANGE_RATE);
                this._flexD.SetCol("UM_WEIGHT", "중량단가", 80, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            }
            else
            {
                this._flexD.SetCol("CLS_L", "대분류코드", false);
                this._flexD.SetCol("CLS_M", "중분류코드", false);
                this._flexD.SetCol("CLS_S", "소분류코드", false);
                this._flexD.SetCol("NM_CLS_L", "대분류", 140, false, typeof(string));
                this._flexD.SetCol("NM_CLS_M", "중분류", 140, false, typeof(string));
                this._flexD.SetCol("NM_CLS_S", "소분류", 140, false, typeof(string));
            }
            this._flexD.SetCol("RT_PO", "환산수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            if (Global.MainFrame.CurrentPageID == "P_PU_Z_JONGHAP_PO_REG2")
            {
                this._flexD.SetCol("CLS_L", "대분류코드", 80, true);
                this._flexD.SetCol("NM_CLS_L", "대분류", 140, false, typeof(string));
                this._flexD.SetCol("NUM_STND_ITEM_1", "NUM_STND_ITEM_1", false);
                this._flexD.SetCol("NUM_STND_ITEM_2", "NUM_STND_ITEM_2", false);
                this._flexD.SetCol("NUM_STND_ITEM_3", "NUM_STND_ITEM_3", false);
                this._flexD.SetCol("NUM_STND_ITEM_4", "NUM_STND_ITEM_4", false);
                this._flexD.SetCol("NUM_STND_ITEM_5", "NUM_STND_ITEM_5", false);
                this._flexD.Cols["RT_PO"].Caption = "단중";
                ((RowCol)this._flexD.Cols["RT_PO"]).AllowEditing = true;
                this._flexD.SetCol("UM_EX", "재고단가", 80, true, typeof(decimal), FormatTpType.UNIT_COST);
            }
            bool flag1 = true;
            if (Global.MainFrame.ServerKeyCommon == "LUKEN")
            {
                flag1 = false;
                this._flexD.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
                this._flexD.SetCol("NUM_USERDEF3_PO", "기준발주단가", 100, 17, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                this._flexD.SetCol("NUM_USERDEF4_PO", "기준발주금액", 150, 17, true, typeof(decimal), FormatTpType.MONEY);
                this._flexD.SetCol("AM_DISCONUNT", "개별할인금액", 150, 17, true, typeof(decimal), FormatTpType.MONEY);
            }
            if (this.b단가권한)
                this._flexD.SetCol("UM_EX_PO", "단가", 100, 17, flag1, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            if (this.b금액권한)
            {
                this._flexD.SetCol("AM_EX", "금액", 150, 17, flag1, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flexD.SetCol("AM", "원화금액", 150, 17, flag1, typeof(decimal), FormatTpType.MONEY);
                if (MA.ServerKey(false, new string[] { "DAEJOOKC", "SATREC", "MEERE", "DODRAM", "DHF5" }))
                    this._flexD.SetCol("VAT", "부가세", 150, 17, true, typeof(decimal), FormatTpType.MONEY);
                else
                    this._flexD.SetCol("VAT", "부가세", 150, 17, false, typeof(decimal), FormatTpType.MONEY);
                this._flexD.SetCol("AM_TOTAL", "총금액", 150, 17, flag1, typeof(decimal), FormatTpType.MONEY);
            }
            this._flexD.SetCol("FG_TAX", "과세구분", 70, true);
            this._flexD.SetCol("TP_UM_TAX", "부가세여부", 70, true);
            if (Global.MainFrame.CurrentPageID != "P_PU_Z_JONGHAP_PO_REG2")
            {
                if (!this.bStandard)
                {
                    if (Global.MainFrame.ServerKeyCommon == "DHPENG")
                        this._flexD.SetCol("WEIGHT", "단위중량", 80, 17, true, typeof(decimal), (FormatTpType)9);
                    else
                        this._flexD.SetCol("WEIGHT", "단위중량", 80, 17, true, typeof(decimal), FormatTpType.MONEY);
                }
                if (!this.bStandard)
                    this._flexD.SetCol("QT_WEIGHT", "총중량", 80, 17, false, typeof(decimal), FormatTpType.MONEY);
            }
            this._flexD.SetCol("CD_CC", "CC코드", 80, true, typeof(string));
            this._flexD.SetCol("NM_CC", "CC명", 100, true, typeof(string));
            this._flexD.SetCol("CD_SL", "창고코드", 80, 7, true, typeof(string));
            this._flexD.SetCol("NM_SL", "창고명", 120, false, typeof(string));
            this._flexD.SetCol("CD_PJT", "프로젝트", 120, true, typeof(string));
            this._flexD.SetCol("NM_PJT", "프로젝트명", 120, false, typeof(string));
            this._flexD.SetCol("SEQ_PROJECT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 항번" : "프로젝트항번", 120, false, typeof(decimal));
            this._flexD.SetCol("NM_SYSDEF", "발주상태", 80, false, typeof(string));
            this._flexD.SetCol("QT_PO", "재고수량", 120, this.m_sEnv == "Y", typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("NO_PR", "요청번호", 120, false, typeof(string));
            this._flexD.SetCol("NO_APP", "품의번호", 120, false, typeof(string));
            this._flexD.SetCol("DC1", "발주라인비고1", 200, 200, true, typeof(string));
            this._flexD.SetCol("DC2", "발주라인비고2", 200, 200, true, typeof(string));
            this._flexD.SetCol("DC3", "발주라인비고3", 200, 500, true, typeof(string));
            this._flexD.SetCol("DC4", "발주라인비고4", 200, 500, true, typeof(string));
            this._flexD.SetCol("NO_LINE", "항번", 40, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.Cols["NO_LINE"].Visible = false;
            this._flexD.SetCol("NM_CUST_DLV", "수취인", 80, false, typeof(string));
            this._flexD.SetCol("ADDR1_DLV", "주소", 80, false, typeof(string));
            this._flexD.SetCol("NO_TEL_D2_DLV", "핸드폰번호", 80, false, typeof(string));
            this._flexD.SetCol("QT_INVC", "재고량", 120, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("GI_PARTNER", "납품처코드", 120, true);
            this._flexD.SetCol("LN_PARTNER", "납품처명", 200, false);
            this._flexD.SetCol("NM_CLS_ITEM", "계정", 120);
            this._flexD.SetCol("FG_PACKING", "포장형태", 140, true, typeof(string));
            this._flexD.SetCol("CD_REASON", "구매요청사유", 140, false, typeof(string));
            this._flexD.SetCol("FG_SU", "외주유형", 140, false, typeof(string));
            this._flexD.SetCol("GRP_MFG", "제품군코드", 80, false);
            this._flexD.SetCol("NM_GRPMFG", "제품군명", 100, false);
            this._flexD.SetCol("EN_ITEM", "품목명(영)", 100, false);
            this._flexD.SetCol("NO_SO", "수주번호", false);
            this._flexD.SetCol("NO_SOLINE", "수주항번", false);
            this._flexD.SetCol("NO_RELATION", "연동번호", false);
            this._flexD.SetCol("SEQ_RELATION", "연동번호항번", false);
            if (this._m_Company_only == "003")
            {
                this._flexD.SetCol("NUM_USERDEF4", this.NUM_USERDEF4, 150, false);
                this._flexD.SetCol("NUM_USERDEF5", this.NUM_USERDEF5, 150, false);
                this._flexD.SetCol("NUM_USERDEF6", this.NUM_USERDEF6, 150, false);
            }
            if (this._구매예산CHK설정 == "Y")
            {
                this._flexD.SetCol("CD_BUDGET", "예산단위코드", 150, false);
                this._flexD.SetCol("NM_BUDGET", "예산단위명", 150, false);
                this._flexD.SetCol("CD_BGACCT", "예산계정코드", 150, false);
                this._flexD.SetCol("NM_BGACCT", "예산계정명", 150, false);
                this._flexD.SetCol("CD_ACCT", "회계계정코드", false);
                this._flexD.SetCol("NM_ACCT", "회계계정명", false);
                this._flexD.SetCodeHelpCol("CD_ACCT", HelpID.P_FI_ACCTCODE_NB_SUB, ShowHelpEnum.Always, new string[] { "CD_ACCT",
                                                                                                                       "NM_ACCT" },
                                                                                                        new string[] { "CD_ACCT",
                                                                                                                       "NM_ACCT" }, ResultMode.FastMode);
                this._flexD.Cols["CD_ACCT"].AllowEditing = true;
                this._flexD.Cols["NM_ACCT"].AllowEditing = false;
                if (this._YN_CdBizplan == "1")
                {
                    this._flexD.SetCol("CD_BIZPLAN", "사업계획", 150, true, typeof(string));
                    this._flexD.SetCol("NM_BIZPLAN", "사업계획명", 150, false, typeof(string));
                    this._flexD.SetCodeHelpCol("CD_BIZPLAN", HelpID.P_FI_BIZPLAN2_SUB, ShowHelpEnum.Always, new string[] { "CD_BIZPLAN",
                                                                                                                           "NM_BIZPLAN" }, 
                                                                                                            new string[] { "CD_BIZPLAN",
                                                                                                                           "NM_BIZPLAN" }, 
                                                                                                            new string[] { "CD_BGACCT",
                                                                                                                           "NM_BGACCT" }, ResultMode.FastMode);
                }
                if (this._구매예산CHK설정FI == "100" && BASIC.GetMAEXC_Menu(nameof(P_CZ_PU_PO_WINTEC), "PU_A00000007") == "000")
                {
                    this._flexD.SetCol("YN_BUDGET", "예산확인", 80, true);
                    this._flexD.SetCol("BUDGET_PASS", "예산통과", 80, false);
                }
            }
            if (this._m_Company_only == "001")
            {
                this._flexD.SetCol("QT_WIDTH", "폭(mm)", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                this._flexD.SetCol("QT_LENGTH", "길이(m)", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                this._flexD.SetCol("QT_AREA", "면적(m\u00B2)", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                this._flexD.SetCol("TOTAL_AREA", "총면적(m\u00B2)", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                this._flexD.SetCol("UM_EX_AR", "외화단가(m\u00B2)", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                this._flexD[0, "UM_EX_PO"] = "외화단가(EA)";
                this._flexD[0, "QT_PO_MM"] = "발주량(EA)";
            }
            if (Config.MA_ENV.프로젝트사용)
            {
                this._flexD.SetCol("CD_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 코드" : "프로젝트 품목코드", 140, false, typeof(string));
                this._flexD.SetCol("NM_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 명" : "프로젝트 품목명", 140, false, typeof(string));
                this._flexD.SetCol("NO_PJT_DESIGN", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 도면번호" : "프로젝트 도면번호", 140, false, typeof(string));
                this._flexD.SetCol("PJT_ITEM_STND", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 규격" : "프로젝트 품목규격", 140, false, typeof(string));
            }
            if (Config.MA_ENV.PJT형여부 == "Y")
            {
                if (!App.SystemEnv.PMS사용)
                {
                    this._flexD.SetCol("NO_WBS", "WBS번호", 140, false, typeof(string));
                    this._flexD.SetCol("CD_ACTIVITY", "ACTIVITY 코드", 140, false, typeof(string));
                    this._flexD.SetCol("NM_ACTIVITY", "ACTIVITY", 140, false, typeof(string));
                    this._flexD.SetCol("CD_COST", "원가코드", 140, false, typeof(string));
                    this._flexD.SetCol("NM_COST", "원가명", 140, false, typeof(string));
                }
                else
                {
                    this._flexD.SetCol("CD_CSTR", "CBS품목코드", 110, false, typeof(string));
                    this._flexD.SetCol("DL_CSTR", "CBS내역코드", 80, false, typeof(string));
                    this._flexD.SetCol("NM_CSTR", "CBS항목명", 140, false, typeof(string));
                    this._flexD.SetCol("SIZE_CSTR", "CBS규격", 140, false, typeof(string));
                    this._flexD.SetCol("UNIT_CSTR", "CBS단위", 110, false, typeof(string));
                    this._flexD.SetCol("QTY_ACT", "CBS예산수량", 120, false, typeof(decimal), FormatTpType.QUANTITY);
                    this._flexD.SetCol("UNT_ACT", "CBS예산단가", 100, false, typeof(decimal), FormatTpType.UNIT_COST);
                    this._flexD.SetCol("AMT_ACT", "CBS예산금액", 100, false, typeof(decimal), FormatTpType.MONEY);
                }
                this._flexD.SetCol("NO_CBS", "CBS번호", 140, true, typeof(string));
                this._flexD.SetCol("CD_ITEM_MO", "상위품목코드", 140, false, typeof(string));
                this._flexD.SetCol("NM_ITEM_MO", "상위품목명", 140, false, typeof(string));
            }
            this._flexD.SetCol("NO_MODEL", "모델코드", 140, false, typeof(string));
            this._flexD.SetCol("STND_DETAIL_ITEM", "상세규격", 140, false, typeof(string));
            bool flag2 = true;
            if (Global.MainFrame.ServerKeyCommon == "ANJUN")
                flag2 = false;
            this._flexD.SetCol("CD_USERDEF1", "사용자정의1", 100, flag2, typeof(string));
            this._flexD.SetCol("CD_USERDEF2", "사용자정의2", 100, true, typeof(string));
            DataTable code1 = MA.GetCode("PU_Z000007", false);
            if (code1 != null && code1.Rows.Count != 0)
            {
                foreach (DataRow row in code1.Rows)
                {
                    string str1 = D.GetString(row["CD_FLAG1"]) == "" ? D.GetString(row["NAME"]) : D.GetString(row["CD_FLAG1"]);
                    string str2 = D.GetString(row["NAME"]);
                    if (str2.Contains("DATE"))
                        this._flexD.SetCol(str2, str1, 70, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                    else
                        this._flexD.SetCol(str2, str1, 100, true, typeof(string));
                    if (str2.Contains("CDSL_USERDEF1"))
                    {
                        this._flexD.SetCodeHelpCol("CDSL_USERDEF1", HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CDSL_USERDEF1", "NMSL_USERDEF1" }, new string[] { "CD_SL", "NM_SL" });
                        this._flexD.SetCol("NMSL_USERDEF1", D.GetString(row["CD_FLAG2"]), 100, false);
                    }
                }
            }
            this._flexD.SetCol("DT_EXDATE", "공장출고일", 100, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            if (this.b금액권한)
            {
                this._flexD.SetCol("AM_EX_TRANS", "운송비(외화)", 100, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flexD.SetCol("AM_TRANS", "운송비(원화)", 100, true, typeof(decimal), FormatTpType.MONEY);
            }
            this._flexD.SetCol("CD_USERDEF14", "사용자정의14", 0, false, typeof(string));
            this._flexD.SetCol("RATE_VAT", "부가세율", 0, false, typeof(decimal));
            this._flexD.SetCol("NM_MAKER", "MAKER", 100, false, typeof(string));
            if (BASIC.GetMAEXC_Menu(Global.MainFrame.CurrentPageID, "PU_A00000002") == "000")
                this._flexD.SetCol("FG_PURCHASE", "매입형태", 100, false, typeof(string));
            else
                this._flexD.SetCol("FG_PURCHASE", "매입형태", 100, true, typeof(string));
            this._flexD.SetCol("MAT_ITEM", "재질", 100, true, typeof(string));
            this._flexD.SetCol("NO_DESIGN", "도면번호", 100, true, typeof(string));
            this._flexD.SetCol("TP_PART", "내외자구분", 100, false, typeof(string));
            if (this.b단가권한)
            {
                this._flexD.SetCol("UM", "원화단가", false);
                this._flexD.SetCol("UM_P", "원화단가", false);
            }
            this._flexD.SetCol("NO_PRLINE", "요청번호항번", 80, false);
            this._flexD.SetCol("CD_ITEM_PARTNER", "거래처품목코드", false);
            this._flexD.SetCol("NM_ITEM_PARTNER", "거래처품목명", false);
            this._flexD.SetCol("STND_ITEM_PARTNER", "거래처품목규격", false);
            this._flexD.SetCol("TP_ITEM", "품목타입", false);
            if (Global.MainFrame.ServerKey == "ICDERPU")
            {
                this._flexD.SetCol("UM_PRE", "할인전단가", 120, 17, false, typeof(decimal), FormatTpType.UNIT_COST);
                this._flexD.SetCol("AM_PRE", "견적금액", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            }
            if (this._YN_REBATE && this.b금액권한 && this.b단가권한)
            {
                this._flexD.SetCol("UM_REBATE", "리베이트단가", 100, 17, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                this._flexD.SetCol("AM_REBATE_EX", "리베이트금액", 100, 17, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flexD.SetCol("AM_REBATE", "리베이트원화금액", 100, 17, true, typeof(decimal), FormatTpType.MONEY);
            }
            DataTable code2 = MA.GetCode("PU_C000093", false);
            if (code2 != null && code2.Rows.Count != 0)
            {
                foreach (DataRow row in code2.Rows)
                {
                    string str = D.GetString(row["CD_FLAG1"]) == "" ? D.GetString(row["NAME"]) : D.GetString(row["CD_FLAG1"]);
                    this._flexD.SetCol(D.GetString(row["NAME"]), str, 80, 17, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                }
            }
            if (this.MainFrameInterface.ServerKeyCommon == "UNIPOINT")
            {
                this._flexD.SetCol("CD_PARTNER_PJT", "프로젝트 거래처코드", 100, false, typeof(string));
                this._flexD.SetCol("LN_PARTNER_PJT", "프로젝트 거래처", 100, false, typeof(string));
                this._flexD.SetCol("NO_EMP_PJT", "프로젝트 담당자코드", 100, false, typeof(string));
                this._flexD.SetCol("NM_KOR_PJT", "프로젝트 담당자", 100, false, typeof(string));
                this._flexD.SetCol("END_USER", "프로젝트 END USER", 100, false, typeof(string));
            }
            if (Global.MainFrame.ServerKeyCommon.Contains("KAHP"))
                this._flexD.SetCol("TXT_USERDEF4_H", "전표번호", 100, false, typeof(string));
            if (Global.MainFrame.ServerKeyCommon.Contains("WINFOOD"))
                this._flexD.SetCol("QT_WINFOOD_OUT", "출고수량", 80, 17, true, typeof(decimal), FormatTpType.QUANTITY);
            if (MA.ServerKey(false, new string[] { "TSUBAKI" }))
            {
                this._flexD.SetCol("CD_PARTNER_SO", "수주처코드", 100, false, typeof(string));
                this._flexD.SetCol("LN_PARTNER_SO", "수주처명", 100, false, typeof(string));
                this._flexD.SetCol("TXT_USERDEF1", "TC견적번호", 100, 14, true);
                this._flexD.SetCol("NM_USERDEF3_PO", "비고", 100, 100, true);
                this._flexD.SetCol("NM_USERDEF4_PO", "편성", 100, 80, true);
            }
            if (MA.ServerKey(false, new string[] { "KMI" }))
            {
                this._flexD.SetCol("QT_KMI", "발주대비미입고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                this._flexD.SetCodeHelpCol("CD_ITEM_ORIGIN", HelpID.P_MA_PITEM_SUB, ShowHelpEnum.Always, new string[] { "CD_ITEM_ORIGIN", "NM_ITEM_ORIGIN" },
                                                                                                         new string[] { "CD_ITEM", "NM_ITEM" }, 
                                                                                                         new string[] { "NM_USERDEF1" });
            }
            if (Global.MainFrame.CurrentPageID == "P_PU_Z_JONGHAP_PO_REG2")
            {
                this._flexD.Cols["CD_UNIT_MM"].AllowEditing = true;
                this._flexD.Cols["UNIT_IM"].AllowEditing = true;
                this._flexD.Cols["NM_ITEM"].AllowEditing = true;
                this._flexD.Cols["STND_DETAIL_ITEM"].AllowEditing = true;
                this._flexD.Cols["MAT_ITEM"].AllowEditing = true;
            }
            if (Global.MainFrame.ServerKeyCommon.Contains("HIOKI"))
                this.setCol_HIOKI();
            else if (Global.MainFrame.ServerKeyCommon == "VINA")
            {
                this._flexD.SetCol("NM_POST", "발주확정자", 100, false);
                this._flexD.SetCol("DTS_POST", "발주확정일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                this._flexD.Cols["DTS_POST"].Format = "####/##/## ##:##:##";
                this._flexD.SetStringFormatCol("DTS_POST");
            }
            this._flexD.SetDummyColumn(new string[] { "S",
                                                      "QT_INVC",
                                                      "FG_POCON",
                                                      "NM_SYSDEF",
                                                      "MEMO_CD",
                                                      "CHECK_PEN" });
            if (Global.MainFrame.ServerKeyCommon.ToUpper() != "WJIS")
            {
                if (Global.MainFrame.CurrentPageID == "P_PU_Z_JONGHAP_PO_REG2")
                    this._flexD.SetCodeHelpCol("CD_ITEM", HelpID.P_MA_PITEM_SUB1, ShowHelpEnum.Always, new string[] { "CD_ITEM",
                                                                                                                      "NM_ITEM",
                                                                                                                      "STND_ITEM",
                                                                                                                      "UNIT_IM",
                                                                                                                      "CD_SL",
                                                                                                                      "NM_SL",
                                                                                                                      "CD_UNIT_MM",
                                                                                                                      "PI_PARTNER",
                                                                                                                      "PI_LN_PARTNER",
                                                                                                                      "NUM_STND_ITEM_1",
                                                                                                                      "NUM_STND_ITEM_2",
                                                                                                                      "NUM_STND_ITEM_3",
                                                                                                                      "NUM_STND_ITEM_4",
                                                                                                                      "NUM_STND_ITEM_5",
                                                                                                                      "RT_PO" }
                                                                                                     , new string[] { "CD_ITEM",
                                                                                                                      "NM_ITEM",
                                                                                                                      "STND_ITEM",
                                                                                                                      "UNIT_IM",
                                                                                                                      "CD_SL",
                                                                                                                      "NM_SL",
                                                                                                                      "UNIT_PO",
                                                                                                                      "PARTNER",
                                                                                                                      "LN_PARTNER",
                                                                                                                      "NUM_STND_ITEM_1",
                                                                                                                      "NUM_STND_ITEM_2",
                                                                                                                      "NUM_STND_ITEM_3",
                                                                                                                      "NUM_STND_ITEM_4",
                                                                                                                      "NUM_STND_ITEM_5",
                                                                                                                      "UNIT_PO_FACT" }, ResultMode.SlowMode);
                else
                    this._flexD.SetCodeHelpCol("CD_ITEM", HelpID.P_MA_PITEM_SUB1, ShowHelpEnum.Always, new string[] { "CD_ITEM",
                                                                                                                      "NM_ITEM",
                                                                                                                      "STND_ITEM",
                                                                                                                      "UNIT_IM",
                                                                                                                      "CD_SL",
                                                                                                                      "NM_SL",
                                                                                                                      "CD_UNIT_MM",
                                                                                                                      "PI_PARTNER",
                                                                                                                      "PI_LN_PARTNER" },
                                                                                                       new string[] { "CD_ITEM",
                                                                                                                      "NM_ITEM",
                                                                                                                      "STND_ITEM",
                                                                                                                      "UNIT_IM",
                                                                                                                      "CD_SL",
                                                                                                                      "NM_SL",
                                                                                                                      "UNIT_PO",
                                                                                                                      "PARTNER",
                                                                                                                      "LN_PARTNER" }, ResultMode.SlowMode);
            }
            else
                this._flexD.SetCodeHelpCol("CD_ITEM", HelpID.P_MA_PITEM_SUB1, ShowHelpEnum.Always, new string[] { "CD_ITEM",
                                                                                                                  "NM_ITEM",
                                                                                                                  "STND_ITEM",
                                                                                                                  "UNIT_IM",
                                                                                                                  "CD_SL",
                                                                                                                  "NM_SL",
                                                                                                                  "CD_UNIT_MM",
                                                                                                                  "PI_PARTNER",
                                                                                                                  "PI_LN_PARTNER",
                                                                                                                  "DC1",
                                                                                                                  "DC2" },
                                                                                                   new string[] { "CD_ITEM",
                                                                                                                  "NM_ITEM",
                                                                                                                  "STND_ITEM",
                                                                                                                  "UNIT_IM",
                                                                                                                  "CD_SL",
                                                                                                                  "NM_SL",
                                                                                                                  "UNIT_PO",
                                                                                                                  "PARTNER",
                                                                                                                  "LN_PARTNER",
                                                                                                                  "CD_ITEM",
                                                                                                                  "NM_ITEM" }, ResultMode.SlowMode);
            this._flexD.SetCodeHelpCol("CD_SL", HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CD_SL",  "NM_SL" }, new string[] { "CD_SL", "NM_SL" });
            this._flexD.SetCodeHelpCol("CD_CC", HelpID.P_MA_CC_SUB, ShowHelpEnum.Always, new string[] { "CD_CC", "NM_CC" }, new string[] { "CD_CC", "NM_CC" });
            if (Config.MA_ENV.YN_UNIT == "Y" || BASIC.GetMAEXC("구매-프로젝트도움창 UNIT사용여부") == "100" && Config.MA_ENV.프로젝트사용)
            {
                this._flexD.SetCodeHelpCol("CD_PJT", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new string[] { "CD_PJT",
                                                                                                         "NM_PJT",
                                                                                                         "SEQ_PROJECT",
                                                                                                         "CD_PJT_ITEM",
                                                                                                         "NM_PJT_ITEM",
                                                                                                         "PJT_ITEM_STND" },
                                                                                          new string[] { "NO_PROJECT",
                                                                                                         "NM_PROJECT",
                                                                                                         "SEQ_PROJECT",
                                                                                                         "CD_PJT_ITEM",
                                                                                                         "NM_PJT_ITEM",
                                                                                                         "PJT_ITEM_STND" },
                                                                                          new string[] { "NO_PROJECT",
                                                                                                         "NM_PROJECT",
                                                                                                         "SEQ_PROJECT",
                                                                                                         "CD_PJT_ITEM",
                                                                                                         "PJT_NM_ITEM",
                                                                                                         "PJT_STND_ITEM" }, ResultMode.FastMode);
                this._flexD.SetCodeHelpCol("CD_PJT_ITEM", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new string[] { "CD_PJT",
                                                                                                              "NM_PJT",
                                                                                                              "SEQ_PROJECT",
                                                                                                              "CD_PJT_ITEM",
                                                                                                              "NM_PJT_ITEM",
                                                                                                              "PJT_ITEM_STND" },
                                                                                               new string[] { "NO_PROJECT",
                                                                                                              "NM_PROJECT",
                                                                                                              "SEQ_PROJECT",
                                                                                                              "CD_PJT_ITEM",
                                                                                                              "NM_PJT_ITEM",
                                                                                                              "PJT_ITEM_STND" }, 
                                                                                               new string[] { "NO_PROJECT",
                                                                                                              "NM_PROJECT",
                                                                                                              "SEQ_PROJECT",
                                                                                                              "CD_PJT_ITEM",
                                                                                                              "PJT_NM_ITEM",
                                                                                                              "PJT_STND_ITEM" }, ResultMode.FastMode);
            }
            else
                this._flexD.SetCodeHelpCol("CD_PJT", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new string[] { "CD_PJT", "NM_PJT" }, new string[] { "NO_PROJECT", "NM_PROJECT" });
            this._flexD.SetCodeHelpCol("NO_CBS", "H_PM_CBS_SUB", ShowHelpEnum.Always, new string[] { "NO_CBS",
                                                                                                     "CD_COST",
                                                                                                     "NM_COST" }, 
                                                                                      new string[] { "NO_CBS", 
                                                                                                     "CD_COST",
                                                                                                     "NM_COST" });
            this._flexD.SetCodeHelpCol("GI_PARTNER", HelpID.P_SA_TPPTR_SUB, ShowHelpEnum.Always, new string[] { "GI_PARTNER",
                                                                                                                "LN_PARTNER" },
                                                                                                 new string[] { "CD_TPPTR",
                                                                                                                "NM_TPPTR" });
            this._flexD.SetCodeHelpCol("CD_BUDGET", HelpID.P_FI_BGCODE_SUB, ShowHelpEnum.Always, new string[] { "CD_BUDGET",
                                                                                                                "NM_BUDGET" },
                                                                                                 new string[] { "CD_BUDGET",
                                                                                                                "NM_BUDGET" });
            if (this._YN_CdBizplan == "1")
                this._flexD.SetCodeHelpCol("CD_BGACCT", "H_FI_BUDGETACCTJO_SUB", ShowHelpEnum.Always, new string[] { "CD_BGACCT",
                                                                                                                     "NM_BGACCT" },
                                                                                                      new string[] { "CD_BGACCT",
                                                                                                                     "NM_BGACCT" });
            else
                this._flexD.SetCodeHelpCol("CD_BGACCT", HelpID.P_FI_BGACCT_SUB, ShowHelpEnum.Always, new string[] { "CD_BGACCT",
                                                                                                         "NM_BGACCT" },
                                                                                          new string[] { "CD_BGACCT",
                                                                                                         "NM_BGACCT" });
            if (this.bStandard)
            {
                this._flexD.SetCodeHelpCol("NM_CLS_L", HelpID.P_MA_CODE_SUB, ShowHelpEnum.Always, new string[] { "CLS_L",
                                                                                                                 "NM_CLS_L" }, 
                                                                                                  new string[] { "CD_SYSDEF",
                                                                                                                 "NM_SYSDEF" }, ResultMode.SlowMode);
                this._flexD.SetCodeHelpCol("NM_CLS_M", HelpID.P_MA_CODE_SUB, ShowHelpEnum.Always, new string[] { "CLS_M",
                                                                                                                 "NM_CLS_M" }, 
                                                                                                  new string[] { "CD_SYSDEF", 
                                                                                                                 "NM_SYSDEF" }, ResultMode.SlowMode);
                this._flexD.SetCodeHelpCol("NM_CLS_S", HelpID.P_MA_CODE_SUB, ShowHelpEnum.Always, new string[] { "CLS_S",
                                                                                                                 "NM_CLS_S" }, 
                                                                                                  new string[] { "CD_SYSDEF",
                                                                                                                 "NM_SYSDEF" }, ResultMode.SlowMode);
                this._flexD.SetCodeHelpCol("NM_ITEMGRP", (HelpID)21, ShowHelpEnum.Always, new string[] { "GRP_ITEM",
                                                                                                         "NM_ITEMGRP" }, 
                                                                                          new string[] { "CD_ITEMGRP", 
                                                                                                         "NM_ITEMGRP" });
            }
            if (Global.MainFrame.CurrentPageID == "P_PU_Z_JONGHAP_PO_REG2")
                this._flexD.SetCodeHelpCol("CLS_L", HelpID.P_MA_CODE_SUB, ShowHelpEnum.Always, new string[] { "CLS_L",
                                                                                                              "NM_CLS_L" },
                                                                                               new string[] { "CD_SYSDEF",
                                                                                                              "NM_SYSDEF" }, 
                                                                                               new string[] { "CLS_L",
                                                                                                              "NM_CLS_L",
                                                                                                              "NUM_STND_ITEM_1",
                                                                                                              "NUM_STND_ITEM_2",
                                                                                                              "NUM_STND_ITEM_3",
                                                                                                              "NUM_STND_ITEM_4",
                                                                                                              "NUM_STND_ITEM_5",
                                                                                                              "RT_PO",
                                                                                                              "UM",
                                                                                                              "UM_P",
                                                                                                              "UM_EX",
                                                                                                              "AM",
                                                                                                              "AM_EX",
                                                                                                              "UM_EX_PO",
                                                                                                              "VAT",
                                                                                                              "AM_TOTAL",
                                                                                                              "QT_PO_MM",
                                                                                                              "QT_PO" }, ResultMode.SlowMode);
            this._flexD.SetExceptEditCol(new string[] { "NM_ITEM",
                                                        "STND_ITEM",
                                                        "UNIT_IM",
                                                        "CD_UNIT_MM",
                                                        "NM_SL",
                                                        "NM_PJT" });
            this._flexD.SetExceptEditCol(new string[] { "NM_CC",
                                                        "NM_BUDGET",
                                                        "NM_BGACCT" });
            if (Global.MainFrame.CurrentPageID != "P_PU_Z_JONGHAP_PO_REG2")
                this._flexD.VerifyAutoDelete = new string[] { "CD_ITEM" };
            List<string> stringList = new List<string>();
            stringList.Add("CD_ITEM");
            stringList.Add("DT_LIMIT");
            if (App.SystemEnv.PROJECT사용)
            {
                stringList.Add("CD_PJT");
                if (Config.MA_ENV.YN_UNIT == "Y")
                    stringList.Add("SEQ_PROJECT");
            }
            if (Global.MainFrame.ServerKeyCommon == "DKONT")
                stringList.Add("FG_PACKING");
            else if (Global.MainFrame.CurrentPageID == "P_PU_Z_JONGHAP_PO_REG2")
            {
                stringList.Remove("CD_ITEM");
                stringList.Add("NM_ITEM");
                stringList.Add("RT_PO");
                stringList.Add("CD_UNIT_MM");
                stringList.Add("UNIT_IM");
                stringList.Add("AM_EX");
                stringList.Add("CLS_L");
            }
            else if (Global.MainFrame.ServerKey == "SWGD")
                stringList.Add("FG_TAX");
            else if (Global.MainFrame.ServerKeyCommon == "GIT")
            {
                stringList.Add("CD_SL");
                stringList.Add("FG_PACKING");
            }
            else if (Global.MainFrame.ServerKeyCommon.Contains("CNCINTER") || Global.MainFrame.ServerKeyCommon.Contains("PSMT"))
                stringList.Add("CD_SL");
            else if (Global.MainFrame.ServerKeyCommon.Contains("HOTEL"))
            {
                stringList.Add("FG_PURCHASE");
                stringList.Add("CD_CC");
            }
            if (BASIC.GetMAEXC_Menu(nameof(P_CZ_PU_PO_WINTEC), "PU_A00000036") == "100")
                stringList.Add("UM_EX_PO");
            this._flexD.VerifyNotNull = stringList.ToArray();
			this._flexD.VerifyCompare(this._flexD.Cols["QT_PO_MM"], 0, OperatorEnum.GreaterOrEqual);
			//if (this.b단가권한)
			//	this._flexD.VerifyCompare(this._flexD.Cols["UM_EX_PO"], 0, OperatorEnum.GreaterOrEqual);
			//if (this.b금액권한)
			//{
			//	this._flexD.VerifyCompare(this._flexD.Cols["AM_EX"], 0, OperatorEnum.GreaterOrEqual);
			//	this._flexD.VerifyCompare(this._flexD.Cols["AM"], 0, OperatorEnum.GreaterOrEqual);
			//	this._flexD.VerifyCompare(this._flexD.Cols["VAT"], 0, OperatorEnum.GreaterOrEqual);
			//}
			this._flexD.Cols["PI_PARTNER"].Visible = false;
            this._flexD.Cols["PI_LN_PARTNER"].Visible = false;
            this._flexD.SetCol("LOTSIZE", "LOTSIZE수량", 100);
            Config.UserColumnSetting.InitGrid_UserMenu(this._flexD, this.PageID, true);
            this._flexD.SettingVersion = "1.1.6";
            this._flexD.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            this._flexD.DisableNumberColumnSort();
            if (this._YN_REBATE)
            {
                if (Config.MA_ENV.YN_UNIT == "Y")
                    this._flexD.SetExceptSumCol(new string[] { "UM_EX_PO",
                                                               "UM_REBATE",
                                                               "SEQ_PROJECT" });
                else
                    this._flexD.SetExceptSumCol(new string[] { "UM_EX_PO",
                                                               "UM_REBATE" });
            }
            else if (Config.MA_ENV.YN_UNIT == "Y")
                this._flexD.SetExceptSumCol(new string[] { "UM_EX_PO",
                                                           "SEQ_PROJECT" });
            else
                this._flexD.SetExceptSumCol("UM_EX_PO");
            this._flexD.EnterKeyAddRow = true;
            this._flexD.AddRow += new EventHandler(this.추가_Click);
            this._flexD.StartEdit += new RowColEventHandler(this.Grid_StartEdit);
            this._flexD.AfterRowChange += new RangeEventHandler(this.Grid_AfterRowChange);
            this._flexD.ValidateEdit += new ValidateEditEventHandler(this.Grid_ValidateEdit);
            this._flexD.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this.Grid_BeforeCodeHelp);
            this._flexD.AfterCodeHelp += new AfterCodeHelpEventHandler(this.Grid_AfterCodeHelp);
            this._flexD.AfterEdit += new RowColEventHandler(this._flexD_AfterEdit);
            this._flexD.VerifyCompare(this._flexD.Cols["QT_PO_MM"], 0, OperatorEnum.Greater);
            this._flexD.VerifyCompare(this._flexD.Cols["QT_PO"], 0, OperatorEnum.Greater);
            this._flexD.VerifyCompare(this._flexD.Cols["QT_PO_MM"], this._flexD.Cols["QT_REQ_MM"], OperatorEnum.GreaterOrEqual);
            this._flexD.DoubleClick += new EventHandler(this._flex_DoubleClick);
            this._flexD.CellNoteInfo.EnabledCellNote = true;
            this._flexD.CellNoteInfo.CategoryID = (this).Name;
            this._flexD.CellNoteInfo.DisplayColumnForDefaultNote = "NM_ITEM";
            this._flexD.CheckPenInfo.EnabledCheckPen = true;
            this._flexD.CellContentChanged += new CellContentEventHandler(this._flexD_CellContentChanged);
            this._flexD.AddMyMenu = true;
            this._flexD.AddMenuSeperator();
            this._flexD.AddMenuItem(this._flexD.AddPopup(this.DD("엑셀관리")), "파일생성", new EventHandler(this.EXCEL_Popup));
            if (Global.MainFrame.ServerKeyCommon.Contains("HOSIGI"))
            {
                this._flexD.AddMenuItem(this._flexD.AddPopup(this.DD("일괄적용")), this.DD("납품처"), new EventHandler(this.MouseClick_App));
                this._flexD.BeforeShowContextMenu += new EventHandler(this.PopupEventHandler);
            }
            if (MA.ServerKey(false, new string[] { "SKTS",
                                                   "SKTSDEV",
                                                   "PTCE",
                                                   "PTCEDEV" }))
                this._flexD.AddMenuItem(this._flexD.AddPopup(this.DD("일괄적용")), this.DD("CC코드"), new EventHandler(this.MouseClick_App));
            this._flexDD.BeginSetting(1, 1, false);
            this._flexDD.SetCol("CHK", "S", 30, true, CheckTypeEnum.Y_N);
            this._flexDD.SetCol("NO_LINE", "자재항번", 80, false);
            this._flexDD.SetCol("CD_MATL", "자재코드", 100, 20, true);
            this._flexDD.SetCol("NM_ITEM", "자재명", 140, false);
            this._flexDD.SetCol("STND_ITEM", "규격", 140, false);
            this._flexDD.SetCol("STND_DETAIL_ITEM", "세부규격", 140, false);
            this._flexDD.SetCol("UNIT_MO", "단위", 40, false);
            this._flexDD.SetCol("QT_NEED_UNIT", "실소요량", 90, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flexDD.SetCol("QT_NEED", "(사급)요청수량", 110, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flexDD.SetCol("QT_REQ", "출고의뢰수량", 110, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexDD.SetCol("ECN_DT", "설계변경일", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexDD.SetCol("NO_HST", "차수", 40, 5, false, typeof(decimal));
            this._flexDD.SetCol("NO_ECN", "ECN번호", 100, 20, false);
            this._flexDD.SetCol("QT_LOSS", "(사급)요청가능수량", 130, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexDD.SetCol("QT_PO", "발주수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            if (Global.MainFrame.ServerKeyCommon.Contains("DONGWOON"))
            {
                this._flexDD.SetCol("TXT_USERDEF1", "LOT번호", 100, false);
                this._flexDD.SetCol("NUM_USERDEF2", "PCS수량", 100, true, typeof(decimal), FormatTpType.QUANTITY);
                this._flexDD.SetCol("TXT_USERDEF2", "WW", 100, true);
                this._flexDD.SetCol("TXT_USERDEF3", "SC", 100, true);
                this._flexDD.SetCol("TXT_USERDEF4", "ASSY LOT", 100, true);
                this._flexDD.SetCol("TXT_USERDEF5", "LOT비고", 100, true);
            }
            this._flexDD.VerifyPrimaryKey = new string[] { "NO_POLINE", "NO_LINE" };
            this._flexDD.EndSetting((GridStyleEnum)1, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            this._flexDD.Cols["QT_LOSS"].Visible = false;
            this._flexDD.Cols["QT_PO"].Visible = false;
            this._flexDD.Cols["QT_NEED_UNIT"].Visible = false;
            this._flexDD.Cols["ECN_DT"].Visible = false;
            this._flexDD.Cols["NO_HST"].Visible = false;
            this._flexDD.Cols["NO_ECN"].Visible = false;
            this._flexDD.StartEdit += new RowColEventHandler(this.Grid_StartEdit);
            this._flexDD.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this.Grid_BeforeCodeHelp);
            this._flexDD.AfterCodeHelp += new AfterCodeHelpEventHandler(this.Grid_AfterCodeHelp);
            this._flexDD.ValidateEdit += new ValidateEditEventHandler(this.Grid_ValidateEdit);
            this._flexDD.SetCodeHelpCol("CD_MATL", HelpID.P_MA_PITEM_SUB1, ShowHelpEnum.Always, new string[] { "CD_MATL",
                                                                                                               "NM_ITEM",
                                                                                                               "STND_ITEM",
                                                                                                               "STND_DETAIL_ITEM",
                                                                                                               "UNIT_MO" },
                                                                                                new string[] { "CD_ITEM",
                                                                                                               "NM_ITEM",
                                                                                                               "STND_ITEM",
                                                                                                               "STND_DETAIL_ITEM",
                                                                                                               "UNIT_MO" }, 
                                                                                                new string[] { "CD_MATL",
                                                                                                               "NM_ITEM",
                                                                                                               "STND_ITEM",
                                                                                                               "STND_DETAIL_ITEM",
                                                                                                               "UNIT_MO",
                                                                                                               "QT_NEED",
                                                                                                               "QT_LOSS",
                                                                                                               "ECN_DT",
                                                                                                               "NO_HST",
                                                                                                               "NO_ECN" }, ResultMode.FastMode);
            this._flexDD.SetDummyColumn(new string[] { "S" });
            this._flexDD.VerifyAutoDelete = new string[] { "CD_MATL" };
            this._flexDD.VerifyNotNull = new string[] { "CD_MATL",
                                                        "QT_NEED" };
            this._flexIV.BeginSetting(1, 1, false);
            this._flexIV.SetCol("FG_IV", "매입구분", 100, true);
            this._flexIV.SetCol("DT_PUR_PLAN", "매입예정일", 100, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexIV.SetCol("RT_IV", "발행비율", 100, true, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flexIV.SetCol("AM", "금액", 100, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexIV.SetCol("AM_K", "원화금액", 100, true, typeof(decimal), FormatTpType.MONEY);
            this._flexIV.SetCol("AM_VAT", "부가세", 100, true, typeof(decimal), FormatTpType.MONEY);
            this._flexIV.SetCol("AM_SUM", "합계금액", 100, true, typeof(decimal), FormatTpType.MONEY);
            this._flexIV.SetCol("AM_PUL", "확정매입금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexIV.SetCol("NM_TEXT", "비고1", 100, true);
            this._flexIV.SetCol("NM_TEXT2", "비고2", 100, true);
            this._flexIV.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.Top);
            this._flexIV.ValidateEdit += new ValidateEditEventHandler(this._flexIV_ValidateEdit);
            this._flexIV.StartEdit += new RowColEventHandler(this._flexIV_StartEdit);
        }

        private void InitGridH()
        {
            this._flexH.BeginSetting(1, 1, false);
            this._flexH.SetCol("FG_IV", "조건구분", 100, true);
            this._flexH.SetCol("DT_IV_PLAN", "마감예상일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("RT_IV", "조건비율", 90, true, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flexH.SetCol("AM", "조건금액", 110, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flexH.SetCol("VAT", "부가세", 110, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexH.SetCol("AM_HAP", "조건합계금액", 110, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexH.SetCol("DT_BAN_PLAN", "지금예상일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("RT_BAN", "지급비율", 90, true, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flexH.SetCol("AM_BAN", "지금예정액", 110, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flexH.SetCol("AM_BANK", "지급예정원화금액", 110, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexH.EndSetting((GridStyleEnum)1, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            this._flexH.ValidateEdit += new ValidateEditEventHandler(this._flexH_ValidateEdit);
        }

        private void InitControl()
        {
            DataSet comboData = this.GetComboData(new string[] { "N;PU_C000001",
                                                                 "N;MA_CODEDTL_005;MA_B000046",
                                                                 "N;PU_C000005",
                                                                 "N;MA_B000005",
                                                                 "N;PU_C000009",
                                                                 "N;PU_C000014",
                                                                 "NC;MA_PLANT",
                                                                 "S;TR_IM00008",
                                                                 "S;TR_IM00004",
                                                                 "S;TR_IM00002",
                                                                 "S;TR_IM00003",
                                                                 "S;TR_IM00028",
                                                                 "N;PU_C000067",
                                                                 "S;MA_B000020",
                                                                 "S;TR_IM00011",
                                                                 "N;PU_C000075",
                                                                 "N;PU_C000076",
                                                                 "N;PU_C000077",
                                                                 "S;FI_J000002",
                                                                 "N;MA_AISPOSTH;200",
                                                                 "S;PU_C000044",
                                                                 "N;MA_B000141",
                                                                 "N;PU_Z000010",
                                                                 "N;MA_B000004",
                                                                 "N;PJ_0000013" });
            comboData.Tables[1].PrimaryKey = new DataColumn[] { comboData.Tables[1].Columns["CODE"] };
            this.cbo단가유형.DataSource = comboData.Tables[0];
            this.cbo단가유형.DisplayMember = "NAME";
            this.cbo단가유형.ValueMember = "CODE";
            this.cbo과세구분.DataSource = comboData.Tables[1];
            this.cbo과세구분.DisplayMember = "NAME";
            this.cbo과세구분.ValueMember = "CODE";
            this._flexD.SetDataMap("FG_TAX", comboData.Tables[1], "CODE", "NAME");
            this.cbo부가세여부.DataSource = comboData.Tables[2];
            this.cbo부가세여부.DisplayMember = "NAME";
            this.cbo부가세여부.ValueMember = "CODE";
            this._flexD.SetDataMap("TP_UM_TAX", comboData.Tables[2], "CODE", "NAME");
            this.cbo환정보.DataSource = comboData.Tables[3];
            this.cbo환정보.DisplayMember = "NAME";
            this.cbo환정보.ValueMember = "CODE";
            this.cbo지급조건.DataSource = comboData.Tables[5];
            this.cbo지급조건.DisplayMember = "NAME";
            this.cbo지급조건.ValueMember = "CODE";
            this.cbo공장.DataSource = comboData.Tables[6];
            this.cbo공장.DisplayMember = "NAME";
            this.cbo공장.ValueMember = "CODE";
            this.cbo공장.SelectedValue = Global.MainFrame.LoginInfo.CdPlant;
            this.cbo운송방법.DataSource = comboData.Tables[7];
            this.cbo운송방법.DisplayMember = "NAME";
            this.cbo운송방법.ValueMember = "CODE";
            this.cbo지불조건.DataSource = comboData.Tables[8];
            this.cbo지불조건.DisplayMember = "NAME";
            this.cbo지불조건.ValueMember = "CODE";
            this.cbo가격조건.DataSource = comboData.Tables[9];
            this.cbo가격조건.DisplayMember = "NAME";
            this.cbo가격조건.ValueMember = "CODE";
            this.cbo선적조건.DataSource = comboData.Tables[10];
            this.cbo선적조건.DisplayMember = "NAME";
            this.cbo선적조건.ValueMember = "CODE";
            this.cbo운임조건.DataSource = comboData.Tables[11];
            this.cbo운임조건.DisplayMember = "NAME";
            this.cbo운임조건.ValueMember = "CODE";
            this.cbo지급기준.DataSource = comboData.Tables[12];
            this.cbo지급기준.DisplayMember = "NAME";
            this.cbo지급기준.ValueMember = "CODE";
            this.cbo포장형태.DataSource = comboData.Tables[14];
            this.cbo포장형태.DisplayMember = "NAME";
            this.cbo포장형태.ValueMember = "CODE";
            this.cbo전표유형.DataSource = comboData.Tables[18];
            this.cbo전표유형.DisplayMember = "NAME";
            this.cbo전표유형.ValueMember = "CODE";
            this.cbo매입형태.DataSource = comboData.Tables[19];
            this.cbo매입형태.DisplayMember = "NAME";
            this.cbo매입형태.ValueMember = "CODE";
            DataRow[] dataRowArray1 = comboData.Tables[4].Select("CODE ='P'");
            if (dataRowArray1 != null && dataRowArray1.Length > 0)
                this._ComfirmState = dataRowArray1[0]["NAME"].ToString();
            this.dtp발주일자.Mask = this.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            this.dtp발주일자.ToDayDate = Global.MainFrame.GetDateTimeToday();
            this.dtp발주일자.Text = Global.MainFrame.GetStringToday;
            this.dtp납기일.Mask = this.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            this.dtp납기일.ToDayDate = Global.MainFrame.GetDateTimeToday();
            this.dtp납기일.Text = Global.MainFrame.GetStringToday;
            this.SetHeadControlEnabled(true, 2);
            DataTable dataTable1 = new DataTable();
            dataTable1.Columns.Add("CODE", typeof(string));
            dataTable1.Columns.Add("NAME", typeof(string));
            DataRow row1 = dataTable1.NewRow();
            row1["CODE"] = "Y";
            row1["NAME"] = "함";
            dataTable1.Rows.Add(row1);
            DataRow row2 = dataTable1.NewRow();
            row2["CODE"] = "N";
            row2["NAME"] = "안함";
            dataTable1.Rows.Add(row2);
            this.cbo예산확인.DataSource = dataTable1;
            this.cbo예산확인.DisplayMember = "NAME";
            this.cbo예산확인.ValueMember = "CODE";
            if (this._구매예산CHK설정FI == "100" && BASIC.GetMAEXC_Menu(nameof(P_CZ_PU_PO_WINTEC), "PU_A00000007") == "000")
                this._flexD.SetDataMap("YN_BUDGET", dataTable1, "CODE", "NAME");
            this.cbo원산지.DataSource = comboData.Tables[13];
            this.cbo원산지.DisplayMember = "NAME";
            this.cbo원산지.ValueMember = "CODE";
            this._flexD.SetDataMap("CD_REASON", comboData.Tables[15].Copy(), "CODE", "NAME");
            this._flexD.SetDataMap("FG_PACKING", comboData.Tables[16].Copy(), "CODE", "NAME");
            this._flexD.SetDataMap("FG_SU", comboData.Tables[17].Copy(), "CODE", "NAME");
            this._flexD.SetDataMap("FG_PURCHASE", comboData.Tables[19].Copy(), "CODE", "NAME");
            this._flexD.SetDataMap("CD_UNIT_MM", comboData.Tables[23].Copy(), "CODE", "NAME");
            this._flexD.SetDataMap("UNIT_IM", comboData.Tables[23].Copy(), "CODE", "NAME");
            this._flexIV.SetDataMap("FG_IV", comboData.Tables[24].Copy(), "CODE", "NAME");
            this._flexD.SetDataMap("TP_PART", MA.GetCode("MA_B000029"), "CODE", "NAME");
            this._flexD.SetDataMap("TP_ITEM", MA.GetCode("MA_B000011"), "CODE", "NAME");
            if (Global.MainFrame.ServerKeyCommon == "TOPES")
                this._flexH.SetDataMap("FG_IV", comboData.Tables[22].Copy(), "CODE", "NAME");
            if (this._지급관리통제설정 == "N")
            {
                this.cbo지급구분.DataSource = comboData.Tables[20];
                this.cbo지급구분.DisplayMember = "NAME";
                this.cbo지급구분.ValueMember = "CODE";
            }
            else
            {
                DataTable payList = ComFunc.GetPayList();
                if (payList != null)
                {
                    this.cbo지급구분.DataSource = payList;
                    this.cbo지급구분.DisplayMember = "NAME";
                    this.cbo지급구분.ValueMember = "CODE";
                }
            }
          this.cbo지급구분.SelectedValue = "";
            if (Global.MainFrame.ServerKeyCommon == "GALAXIA")
            {
                this.btnSMS.Visible = true;
                this.btnSMS.Enabled = false;
            }
          this.cbo업체전용1.DataSource = MA.GetCode("PU_C000132", true);
            this.cbo업체전용1.DisplayMember = "NAME";
            this.cbo업체전용1.ValueMember = "CODE";
            this.cbo업체전용2.DataSource = MA.GetCode("PU_C000133", true);
            this.cbo업체전용2.DisplayMember = "NAME";
            this.cbo업체전용2.ValueMember = "CODE";
            if (!this.bStandard && !(Global.MainFrame.CurrentPageID == "P_PU_Z_JONGHAP_PO_REG2"))
                return;
            DataTable dataTable2 = comboData.Tables[21].Copy();
            DataRow[] dataRowArray2 = dataTable2.Select("CODE = '001'");
            if (dataRowArray2.Length > 0)
            {
                this._flexD.Cols["NUM_STND_ITEM_1"].Caption = D.GetString(dataRowArray2[0]["NAME"]);
                this._flexD.Cols["NUM_STND_ITEM_1"].Visible = true;
                this._flexD.Cols["NUM_STND_ITEM_1"].DataType = typeof(decimal);
                this._flexD.Cols["NUM_STND_ITEM_1"].Format = "#,###,##0.####";
                this._flexD.Cols["NUM_STND_ITEM_1"].AllowEditing = true;
            }
            DataRow[] dataRowArray3 = dataTable2.Select("CODE = '002'");
            if (dataRowArray3.Length > 0)
            {
                this._flexD.Cols["NUM_STND_ITEM_2"].Caption = D.GetString(dataRowArray3[0]["NAME"]);
                this._flexD.Cols["NUM_STND_ITEM_2"].Visible = true;
                this._flexD.Cols["NUM_STND_ITEM_2"].DataType = typeof(decimal);
                this._flexD.Cols["NUM_STND_ITEM_2"].Format = "#,###,##0.####";
                this._flexD.Cols["NUM_STND_ITEM_2"].AllowEditing = true;
            }
            DataRow[] dataRowArray4 = dataTable2.Select("CODE = '003'");
            if (dataRowArray4.Length > 0)
            {
                this._flexD.Cols["NUM_STND_ITEM_3"].Caption = D.GetString(dataRowArray4[0]["NAME"]);
                this._flexD.Cols["NUM_STND_ITEM_3"].Visible = true;
                this._flexD.Cols["NUM_STND_ITEM_3"].DataType = typeof(decimal);
                this._flexD.Cols["NUM_STND_ITEM_3"].Format = "#,###,##0.####";
                this._flexD.Cols["NUM_STND_ITEM_3"].AllowEditing = true;
            }
            DataRow[] dataRowArray5 = dataTable2.Select("CODE = '004'");
            if (dataRowArray5.Length > 0)
            {
                this._flexD.Cols["NUM_STND_ITEM_4"].Caption = D.GetString(dataRowArray5[0]["NAME"]);
                this._flexD.Cols["NUM_STND_ITEM_4"].Visible = true;
                this._flexD.Cols["NUM_STND_ITEM_4"].DataType = typeof(decimal);
                this._flexD.Cols["NUM_STND_ITEM_4"].Format = "#,###,##0.####";
                this._flexD.Cols["NUM_STND_ITEM_4"].AllowEditing = true;
            }
            DataRow[] dataRowArray6 = dataTable2.Select("CODE = '005'");
            if (dataRowArray6.Length > 0)
            {
                this._flexD.Cols["NUM_STND_ITEM_5"].Caption = D.GetString(dataRowArray6[0]["NAME"]);
                this._flexD.Cols["NUM_STND_ITEM_5"].Visible = true;
                this._flexD.Cols["NUM_STND_ITEM_5"].DataType = typeof(decimal);
                this._flexD.Cols["NUM_STND_ITEM_5"].Format = "#,###,##0.####";
                this._flexD.Cols["NUM_STND_ITEM_5"].AllowEditing = true;
            }
        }

        private void MA_EXC_SETTING()
        {
            this._구매예산CHK설정FI = ComFunc.전용코드("구매발주등록-예산체크사용유무(회계예산연동)");
            if (ComFunc.전용코드("구매예산CHK") == "Y" || this._구매예산CHK설정FI == "100" || ComFunc.전용코드("구매예산CHK") == "100" || ComFunc.전용코드("구매예산CHK") == "200")
                this._구매예산CHK설정 = "Y";
            this._YN_CdBizplan = new 시스템환경설정().Get환경설정("TP_BUDGETMNG", false);
            this.btn예산chk실행.Visible = false;
            this.btn예산chk내역.Visible = false;
            this.lbl예산확인.Visible = false;
            this.cbo예산확인.Visible = false;
            this.lbl예산통과.Visible = false;
            this.txt예산통과.Visible = false;
            if (this._구매예산CHK설정FI == "100")
                this.btn예산chk내역.Visible = true;
            this.m_sEnv = this._biz.EnvSearch();
            if (Global.MainFrame.CurrentPageID == "P_PU_Z_JONGHAP_PO_REG2")
                this.m_sEnv = "Y";
            this.m_sEnv_CC = ComFunc.전용코드("발주등록-C/C설정");
            this.m_sEnv_CC_Line = ComFunc.전용코드("발주라인-C/C설정수정유무");
            this.m_sEnv_App_Am = ComFunc.전용코드("발주등록(공장)-품의적용시 금액설정");
            this.m_sEnv_App_Sort = ComFunc.전용코드("발주등록(공장)-품의항번별정렬");
            if (App.SystemEnv.PROJECT사용)
                this.btn프로젝트적용.Visible = true;
            else
                this.btn프로젝트적용.Visible = false;
            this.m_Elec_app = BASIC.GetMAEXC("전자결재-사용구분");
            if ((this.m_Elec_app != "000" || BASIC.GetMAEXC("전자결재-버전정보") == "200") && BASIC.GetMAEXC("전자결재메뉴별사용여부-발주등록") == "100")
                this.btn전자결재.Visible = true;
            DataTable partnerCodeSearch = this._biz.GetPartnerCodeSearch();
            if (partnerCodeSearch.Rows.Count > 0 && partnerCodeSearch.Rows[0]["CD_EXC"] != DBNull.Value && partnerCodeSearch.Rows[0]["CD_EXC"].ToString().Trim() != string.Empty)
                this._전용설정 = partnerCodeSearch.Rows[0]["CD_EXC"].ToString().Trim();
            if (this._전용설정 == "200")
            {
                this.btn엑셀업로드.Visible = false;
                this.btn품목추가.Visible = false;
            }
            if (ComFunc.전용코드("H41적용여부") == "Y")
                (this.btnH41적용).Visible = true;
            else
                (this.btnH41적용).Visible = false;
            if (ComFunc.전용코드("구매-수주적용설정") != "200")
                this.btn수주적용.Visible = false;
            if (ComFunc.전용코드("발주등록(공장)-메일전송설정") == "Y")
                this.btn메일전송.Visible = true;
            this._m_partner_use = BASIC.GetMAEXC("구매요청_품의_거래처적용");
            this._m_tppo_use = BASIC.GetMAEXC_Menu("P_PU_APP_REG", "PU_A00000005");
            this._m_tppo_change = BASIC.GetMAEXC_Menu(nameof(P_CZ_PU_PO_WINTEC), "PU_A00000006");
            this._m_partner_change = BASIC.GetMAEXC("발주등록-헤더정보 자동적용");
            this._m_lotsize_use = BASIC.GetMAEXC("발주등록_LOTSIZE적용");
            if (this._m_lotsize_use == "100")
                (this.btnLOTSize적용).Visible = true;
            this.m_sEnv_FG_TAX = ComFunc.전용코드("과세구분설정");
            BASICPU.CacheDataClear(BASICPU.CacheEnums.MA_MENU_CONTROL);
            if (BASIC.GetMAEXC("구매-우선순위단가적용_사용유무") != "000")
                this.btn우선순위단가적용.Visible = true;
            this.m_sEnv_Prt_Option = ComFunc.전용코드("발주등록-출력옵션사용");
            this._m_dt = ComFunc.전용코드("발주등록-납품일자 통제");
            if (this.m_sEnv_Nego == "100" || Global.MainFrame.ServerKeyCommon == "LUKEN")
            {
                this.lblNego금액.Visible = true;
                this.curNEGO금액.Visible = true;
                this.btnNEGO적용.Visible = true;
                this.curNEGO금액.Size = new Size(131, 21);
                if (Global.MainFrame.ServerKeyCommon == "LUKEN")
                    this.lblNego금액.Text = "할인금액";
            }
            if (Global.MainFrame.ServerKey.Contains("DEMAC") || Global.MainFrame.ServerKey.Contains("SQL_108"))
            {
                this.lblNego금액.Visible = true;
                this.curNEGO금액.Visible = true;
                this.btnNEGO적용.Visible = true;
                this.curDe.Visible = true;
                this.lblNego금액.Text = "할인율/자릿수";
            }
            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "KORAVL")
                this.btn환정보적용.Visible = true;
            else
                this.cur환정보.Size = new Size(100, 21);
            this._m_Company_only = BASIC.GetMAEXC("업체별프로세스");
            if (Config.MA_ENV.PJT형여부 == "Y")
                this.btn_wbscbs.Visible = true;
            if (BASIC.GetMAEXC("발주등록(공장)-수주/의뢰적용") == "100")
                this.btn수주_의뢰적용.Visible = true;
            if (BASIC.GetMAEXC("발주등록(공장)-부가정보적용") != "000")
                (this.btn_subinfo).Visible = true;
            if (BASIC.GetMAEXC("요청,품의,발주-사용자정의컬럼적용") == "100")
                this._APP_USERDEF = "Y";
            if (BASIC.GetMAEXC("리베이트사용여부") == "100")
                this._YN_REBATE = true;
            this._지급관리통제설정 = BASIC.GetMAEXC("거래처정보등록-회계지급관리사용여부");
            this._지급예정일통제설정 = BASIC.GetMAEXC("매입등록-지급예정일통제설정");
            if (Global.MainFrame.ServerKeyCommon == "ANJUN")
                this.btn_INST.Text = "자재수급계획";
            else if (Global.MainFrame.ServerKeyCommon == "KPCI")
                this.btn_INST.Text = "계약적용";
            else
                this.btn_INST.Text = "손익매입적용";
            if (BASIC.GetMAEXC("공장품목등록-규격형") == "100")
                this.bStandard = true;
            if (Global.MainFrame.ServerKeyCommon == "TOPES" || Global.MainFrame.ServerKeyCommon == "DZSQL" || Global.MainFrame.ServerKeyCommon == "SQL_")
            {
                this.btnHadd.Click += new EventHandler(this.btnH_Click);
                this.btnHDe.Click += new EventHandler(this.btnH_Click);
                this.InitGridH();
            }
            if (App.SystemEnv.PMS사용)
                this.btn업무공유_WBS.Visible = true;
            if (BASIC.GetMAEXC("견적요청등록") == "100")
                this.btn견적적용.Visible = true;
            if (Global.MainFrame.ServerKeyCommon.Contains("KAHP"))
            {
                this.btnCompanyUse1.Visible = true;
                this.btnCompanyUse2.Visible = true;
                this.btnCompanyUse1.Text = "전표처리";
                this.btnCompanyUse2.Text = "전표취소";
            }
            if (Global.MainFrame.ServerKeyCommon.Contains("DONGWOON"))
            {
                this.btnCompanyUse1.Visible = true;
                this.btnCompanyUse1.Text = "입고적용";
            }
            if (Global.MainFrame.ServerKeyCommon == "INITECH")
                this.btn일괄발행.Visible = true;
            if (Global.MainFrame.CurrentPageID == "P_PU_Z_JONGHAP_PO_REG2")
            {
                this.btnCompanyUse1.Visible = true;
                this.btnCompanyUse1.Text = "견적적용";
            }
            else if (Global.MainFrame.ServerKeyCommon == "JONGHAP")
            {
                this.btnCompanyUse1.Visible = true;
                this.btnCompanyUse1.Text = "구매처발주서";
                this.btnCompanyUse1.Size = new Size(89, 19);
                this.btnCompanyUse2.Visible = true;
                this.btnCompanyUse2.Text = "견적서";
            }
            if (Global.MainFrame.ServerKeyCommon.Contains("DMENG"))
            {
                this.ctx공급자.HelpID = (HelpID)3;
                this.lbl공급자.Text = "사업장";
            }
            if (MA.ServerKey(false, new string[1] { "SGA" }))
            {
                this.btnCompanyUse1.Visible = true;
                this.btnCompanyUse1.Text = "영업포털적용";
            }
            if (MA.ServerKey(false, new string[1] { "CNCC" }))
                this.lbl비고.Text = "차수";
            if (Global.MainFrame.ServerKeyCommon.Contains("KSCC"))
            {
                this.oneGridItem25.Visible = true;
                this.chk업체전용1.Visible = true;
                this.chk업체전용2.Visible = true;
                this.btn품목추가.Visible = false;
                (this.btn단가정보).Visible = false;
                this.btn우선순위단가적용.Visible = false;
                this.btn품목전개.Visible = false;
                this.chk업체전용1.Text = "계약등록여부";
                this.chk업체전용2.Text = "고정자산여부";
                this.chk업체전용1.Tag = "CD_USERDEF3;Y;N";
                this.chk업체전용2.Tag = "CD_USERDEF4;Y;N";
                this.lbl업체전용1.Text = "구매방법";
                this.lbl업체전용2.Text = "계약방법";
                this.btn요청적용.Text = "배정조회";
            }
            if (Global.MainFrame.ServerKeyCommon.Contains("WINFOOD"))
            {
                this.lbl업체전용1.Text = "GATE NO";
                this.lbl업체전용2.Text = "입고시간";
                this.oneGridItem25.Visible = true;
                this.lbl업체전용3.Visible = true;
                this.lbl업체전용3.Text = "당월입고액";
                this.cur업체전용3.Visible = true;
                this.cur업체전용3.ReadOnly = true;
                this.cur업체전용3.ForMatTpType = FormatTpType.MONEY;
            }
            if (Global.MainFrame.ServerKeyCommon.Contains("YWD"))
            {
                this.btn발주라인비고1적용.Visible = true;
                this.btn발주라인비고1적용.Click += new EventHandler(this.btn발주라인비고1적용_Click);
            }
            if (Global.MainFrame.ServerKeyCommon.Contains("DITERP"))
            {
                this.lbl업체전용.Text = "부가담당자";
                this.lbl업체전용.Visible = true;
                this.ctx업체전용.Visible = true;
                this.ctx업체전용.HelpID = (HelpID)15;
                this.ctx업체전용.Tag = "CD_USERDEF3;TXT_USERDEF3";
            }
            if (Global.MainFrame.LoginInfo.CompanyLanguage != 0)
            {
                this.cur공급가액.CurrencyDecimalDigits = 4;
                this.cur부가세액.CurrencyDecimalDigits = 4;
            }
            if (BASIC.GetMAEXC_Menu(nameof(P_CZ_PU_PO_WINTEC), "PU_A00000034") == "100" || BASIC.GetMAEXC_Menu(nameof(P_CZ_PU_PO_WINTEC), "PU_A00000034") == "200")
            {
                this.btn품목추가.Visible = false;
                if (BASIC.GetMAEXC_Menu(nameof(P_CZ_PU_PO_WINTEC), "PU_A00000034") == "200")
                    this.btn품목삭제.Visible = false;
            }
            DataTable dataTable = BASIC.MFG_AUTH(nameof(P_CZ_PU_PO_WINTEC));
            if (dataTable.Rows.Count > 0)
            {
                this.b단가권한 = !(dataTable.Rows[0]["YN_UM"].ToString() == "Y");
                this.b금액권한 = !(dataTable.Rows[0]["YN_AM"].ToString() == "Y");
            }
            this.m_sEnv_CC_Menu = BASIC.GetMAEXC_Menu(nameof(P_CZ_PU_PO_WINTEC), "PU_A00000052");
            this._m_pjtbom_rq_mng = BASIC.GetMAEXC("프로젝트BOM적용_잔량관리_사용유무");
            if (Global.MainFrame.ServerKeyCommon == "KYINTR")
            {
                this.chk업체전용1.Visible = true;
                this.chk업체전용1.Text = "예약발주";
                this.chk업체전용1.Tag = "CD_USERDEF3;Y;N";
            }
            this.MNG_SERIAL = this._biz.Search_SERIAL();
        }

        private void BTN_LOCATION_SETTING()
        {
        }

        private bool FieldCheck() => ComFunc.NullCheck(new Hashtable() {
                                                                            { this.cbo공장,
                                                                              this.lbl공장명 }
                                                                        });

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeSearch() || !this.FieldCheck())
                    return;
                string CD_PLANT = this.cbo공장.SelectedValue.ToString();
                string codeValue1 = this.ctx거래처.CodeValue;
                string codeName1 = this.ctx거래처.CodeName;
                string codeValue2 = this.ctx구매그룹.CodeValue;
                string codeName2 = this.ctx구매그룹.CodeName;
                P_PU_PO_SUB2 pPuPoSuB2 = !(BASIC.GetMAEXC_Menu("P_PU_PO_REG_AUTO", "PU_A00000001") == "100") ? new P_PU_PO_SUB2(CD_PLANT, codeValue1, codeName1, codeValue2, codeName2) : new P_PU_PO_SUB2(CD_PLANT, codeValue1, codeName1, codeValue2, codeName2, "M|A|I|G");
                if (Global.MainFrame.ServerKeyCommon == "MEERE")
                    pPuPoSuB2.strParam = new string[] { this.ctx담당자.CodeValue,
                                                        this.ctx담당자.CodeName};
                if (((Form)pPuPoSuB2).ShowDialog(this) == DialogResult.OK)
                    this.조회(pPuPoSuB2.m_NO_PO, pPuPoSuB2.m_btnType, pPuPoSuB2.m_NO_POLINE);
                if (this.txt발주번호.Text.Trim() != "" && this.MainFrameInterface.ServerKey == "PKIC")
                    this.check_GW(this.txt발주번호.Text);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void 조회(string NO_PO, string BTN_TYPE, string NO_POLINE)
        {
            DataSet dataSet = this._biz.Search(NO_PO, NO_POLINE);
            if (dataSet.Tables[0].Rows.Count < 1)
            {
                this.ShowMessage(공통메세지._이가존재하지않습니다, new string[] { NO_PO });
                base.OnToolBarAddButtonClicked(null, null);
            }
            else
            {
                this.str복사구분 = BTN_TYPE;
                this.호출여부 = false;
                this._flexD.Binding = null;
                this._flexDD.Binding = null;
                if (this.str복사구분 == "OK")
                {
                    this._header.SetDataTable(dataSet.Tables[0]);
					if (!this.요청적용여부)
						this.ControlButtonEnabledDisable(this.btn요청적용, true);
					else if (!this.품의적용여부)
						this.ControlButtonEnabledDisable(this.btn품의적용, true);
					else if (!this.수주적용여부)
						this.ControlButtonEnabledDisable(this.btn수주적용, true);
					else
						this.ControlButtonEnabledDisable(this.btn품목추가, true);
					if (MA.ServerKey(false, new string[1] { "KMI" }))
                        this.SetQT_KMI(dataSet.Tables[1], 0);
                    this._flexD.Binding = dataSet.Tables[1];
                    if (this.sPUIV == "100" || this.sPUIV == "200")
                        this._flexIV.Binding = dataSet.Tables[2];
                    if (this._m_Company_only == "001")
                    {
                        int rowSel = this._flexD.RowSel;
                        DataTable dt = dataSet.Tables[1].Clone();
                        foreach (DataRow row in dataSet.Tables[1].Rows)
                        {
                            dt.ImportRow(row);
                            this.AsahiKasei_Only_Item(rowSel, dt);
                            dt.Clear();
                            ++rowSel;
                        }
                    }
                    this._flexD.AcceptChanges();
                }
                else if (this.str복사구분 == "COPY")
                {
                    this._flexD.Redraw = false;
                    this._header.SetDataTable(dataSet.Tables[0]);
                    this._header.CurrentRow[nameof(NO_PO)] = "";
                    this.txt발주번호.Text = "";
                    if (MA.ServerKey(false, new string[1] { "AXT" }))
                    {
                        this._header.CurrentRow["DT_PO"] = Global.MainFrame.GetStringToday;
                        this.dtp발주일자.Text = D.GetString(this._header.CurrentRow["DT_PO"]);
                    }
                    if (Global.MainFrame.ServerKeyCommon.Contains("KAHP"))
                        this._header.CurrentRow["TXT_USERDEF4"] = "";
                    if (Global.MainFrame.ServerKeyCommon.Contains("FOODKOR"))
                    {
                        DataTable gipartner = this._biz.Get_Gipartner(this.ctx거래처.CodeValue);
                        if (gipartner != null && gipartner.Rows.Count > 0 && D.GetString(gipartner.Rows[0]["CD_FLAG1"]) == "Y")
                        {
                            this.ShowMessage("발주등록이 불가능한 거래처입니다.");
                            this.ctx거래처.CodeValue = "";
                            this.ctx거래처.CodeName = "";
                            this._header.CurrentRow["CD_PARTNER"] = "";
                        }
                    }
                    this.SetHeadControlEnabled(true, 3);
                    this.ControlButtonEnabledDisable(null, true);
                    this._header.JobMode = JobModeEnum.추가후수정;
                    this._flexD.IsDataChanged = true;
                    this.ToolBarDeleteButtonEnabled = false;
                    this._flexD.Binding = new DataView(dataSet.Tables[1], "", "NO_LINE ASC", DataViewRowState.CurrentRows);
                    for (int index = 0; index < this._flexD.DataTable.Rows.Count; ++index)
                    {
                        this._flexD.DataTable.Rows[index].SetAdded();
                        this._flexD.Redraw = false;
                        this._flexD.DataTable.Rows[index]["NO_APP"] = "";
                        this._flexD.DataTable.Rows[index]["NO_APPLINE"] = 0;
                        this._flexD.DataTable.Rows[index]["NO_PR"] = "";
                        this._flexD.DataTable.Rows[index]["NO_PRLINE"] = 0;
                        this._flexD.DataTable.Rows[index]["NO_RCV"] = "";
                        this._flexD.DataTable.Rows[index]["NO_RCVLINE"] = 0;
                        this._flexD.DataTable.Rows[index]["QT_REQ_MM"] = 0;
                        this._flexD.DataTable.Rows[index]["QT_REQ"] = 0;
                        this._flexD.DataTable.Rows[index]["QT_RCV"] = 0;
                        this._flexD.DataTable.Rows[index]["QT_TR"] = 0;
                        this._flexD.DataTable.Rows[index]["QT_TR_MM"] = 0;
                        this._flexD.DataTable.Rows[index]["FG_POST"] = "O";
                        this._flexD.DataTable.Rows[index]["FG_POCON"] = "001";
                        this._flexD.DataTable.Rows[index]["NO_SO"] = "";
                        this._flexD.DataTable.Rows[index]["NO_SOLINE"] = 0;
                        if (MA.ServerKey(false, new string[] { "AXT" }))
                        {
                            this._flexD.DataTable.Rows[index]["DT_PLAN"] = Global.MainFrame.GetStringToday;
                            this._flexD.DataTable.Rows[index]["DT_LIMIT"] = Global.MainFrame.GetStringToday;
                        }
                    }
                    if (this.sPUIV == "100" || this.sPUIV == "200")
                        this._flexIV.Binding = dataSet.Tables[2];
                    if (this.sPUSU == "100")
                    {
                        for (int index = 1; index < this._flexD.Rows.Count; ++index)
                        {
                            this._flexD.Select(index, 1);
                            this._flexDD.IsDataChanged = true;
                        }
                    }
                  this._flexD.Redraw = true;
                    this.SUMFunction();
                }
                this.Setting_pu_poh_sub();
                if (this.m_tab_poh.TabPages.Contains(this.tabPage8) && this.str복사구분 != "COPY")
                    this._flexH.Binding = this._biz.GetTopes(this.txt발주번호.Text);
                if (this.str복사구분 == "COPY")
                {
                    this.txt발주번호.Enabled = true;
                    this.cbo공장.Enabled = false;
                }
                else
                    this.txt발주번호.Enabled = false;
                if (!this.차수여부 || !this.전자결재여부)
                {
                    this.ctx프로젝트.Enabled = false;
                    this.btnPJT적용.Enabled = false;
                    this.btn품의적용.Enabled = false;
                    this.btn요청적용.Enabled = false;
                    this.btn수주적용.Enabled = false;
                    this.m_pnlHeader_Enabled();
                    this.oneGrid2.Enabled = false;
                    this.txt발주텍스트비고1.Enabled = true;
                    this.txt발주텍스트비고2.Enabled = true;
                    this.btn수주_의뢰적용.Enabled = false;
                    this.ctx창고.Enabled = false;
                    this.btn창고적용.Enabled = false;
                    this.curNEGO금액.Enabled = false;
                    this.btnNEGO적용.Enabled = false;
                    this.curDe.Enabled = false;
                }
                else
                {
                    this.ctx프로젝트.Enabled = true;
                    this.btnPJT적용.Enabled = true;
                    this.btn품의적용.Enabled = true;
                    this.btn요청적용.Enabled = true;
                    this.btn수주적용.Enabled = true;
                    this.oneGrid1.Enabled = true;
                    this.oneGrid2.Enabled = true;
                    this.btn수주_의뢰적용.Enabled = true;
                    this.ctx창고.Enabled = true;
                    this.btn창고적용.Enabled = true;
                    this.curNEGO금액.Enabled = true;
                    this.btnNEGO적용.Enabled = true;
                    this.curDe.Enabled = true;
                }
                if (Global.MainFrame.ServerKeyCommon.Contains("WINFOOD"))
                {
                    this.cur업체전용3.DecimalValue = this._biz.AM_IN(D.GetString(this._header.CurrentRow["CD_PARTNER"]), D.GetString(this._header.CurrentRow["DT_PO"]));
                    this.SetWinFood("", 0);
                }
            }
        }

        protected override bool BeforeAdd() => base.BeforeAdd() && this.MsgAndSave(PageActionMode.Search);

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!base.BeforeAdd())
                    return;
                this._flexD.DataTable.Rows.Clear();
                this._flexD.AcceptChanges();
                this._header.ClearAndNewRow();
                if (this.sPUSU == "100")
                {
                    this._flexDD.DataTable.Rows.Clear();
                    this._flexDD.AcceptChanges();
                }
                if (this.sPUIV == "100" || this.sPUIV == "200")
                {
                    this._flexIV.DataTable.Rows.Clear();
                    this._flexIV.AcceptChanges();
                }
                this.ControlButtonEnabledDisable(null, true);
                this.SetHeadControlEnabled(true, 2);
                this.cbo_FG_TAX_SelectionChangeCommitted(null, null);
                this.ctx프로젝트.CodeValue = string.Empty;
                this.ctx프로젝트.CodeName = string.Empty;
                this.ctx창고.CodeValue = string.Empty;
                this.ctx창고.CodeName = string.Empty;
                this.oneGrid1.Enabled = true;
                this.oneGrid2.Enabled = true;
                this._header.CurrentRow["CD_PURGRP"] = !(D.GetString(Global.MainFrame.LoginInfo.PurchaseGroupCode) == string.Empty) ? Global.MainFrame.LoginInfo.PurchaseGroupCode : Settings1.Default.CD_PURGRP_SET;
                if (this._header.CurrentRow["CD_TPPO"].ToString() == string.Empty && Global.MainFrame.ServerKeyCommon != "WGSK")
                    this._header.CurrentRow["CD_TPPO"] = Settings1.Default.CD_TPPO_SET;
                if (this._header.CurrentRow["FG_PAYMENT"].ToString() == string.Empty)
                {
                    this._header.CurrentRow["FG_PAYMENT"] = Settings1.Default.FG_PAYMENT_SET;
                    if (this._header.CurrentRow["FG_PAYMENT"].ToString() == string.Empty)
                        this._header.CurrentRow["FG_PAYMENT"] = "000";
                    this.cbo지급조건.SelectedValue = this._header.CurrentRow["FG_PAYMENT"].ToString();
                }
                this._header.CurrentRow["TP_UM_TAX"] = Settings1.Default.TP_UM_TAX;
                this.cbo부가세여부.SelectedValue = this._header.CurrentRow["TP_UM_TAX"];
                this.기초값설정();
                this.btn전자결재.Enabled = true;
                if (Global.MainFrame.ServerKeyCommon == "SATREC" || Global.MainFrame.ServerKeyCommon == "JSERP" || Global.MainFrame.ServerKeyCommon == "SQL_")
                {
                    this.txt발주텍스트비고1.Text = D.GetString(Settings1.Default.DC_RMK_TEXT);
                    this.txt발주텍스트비고2.Text = D.GetString(Settings1.Default.DC_RMK_TEXT2);
                }
                if (this.m_tab_poh.TabPages.Contains(this.tabPage7))
                {
                    this.dtp만기일자.Text = Global.MainFrame.GetStringToday;
                    this.dtp지급예정일자.Text = Global.MainFrame.GetStringToday;
                    this.dtp매입일자.Text = Global.MainFrame.GetStringToday;
                    this.cbo지급구분.SelectedValue = "";
                    this.cbo전표유형.SelectedValue = "";
                }
                if (Global.MainFrame.ServerKeyCommon.Contains("WINFOOD"))
                    this.cur업체전용3.DecimalValue = 0M;
                if (this.m_tab_poh.TabPages.Contains(this.tabPage8))
                {
                    this._flexH.DataTable.Rows.Clear();
                    this._flexH.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!base.BeforeDelete())
                    return;
                string _text_sub = string.Empty;
                this._biz.Delete(this.txt발주번호.Text, _text_sub);
                this.ShowMessage(공통메세지.자료가정상적으로삭제되었습니다, new string[0]);
                base.OnToolBarAddButtonClicked(sender, e);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool BeforeDelete()
        {
            if (!base.BeforeDelete())
                return false;
            if (Global.MainFrame.ServerKeyCommon.Contains("KAHP") && D.GetString(this._header.CurrentRow["TXT_USERDEF4"]) != "")
            {
                this.ShowMessage("전표 처리여부를 확인 하십시오.");
                return false;
            }
            if (D.GetString(this._header.CurrentRow["YN_ORDER"]) == "Y" && this._flexD.DataTable.Select("FG_POST = 'R'", "", DataViewRowState.CurrentRows).Length > 0)
            {
                if (this.ShowMessage(this.DD("발주상태가 확정입니다. 삭제하시겠습니까?"), "QY2") != DialogResult.Yes)
                    return false;
            }
            else if (this.ShowMessage(공통메세지.자료를삭제하시겠습니까, new string[] { this.PageName }) != DialogResult.Yes)
                return false;
            return true;
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeSave())
                    return;
                this.ToolBarSaveButtonEnabled = false;
                if (this.MsgAndSave(PageActionMode.Save))
                {
                    this.ShowMessage(PageResultMode.SaveGood);
                    this.Reload(this.txt발주번호.Text);
                    this.str복사구분 = "OK";
                }
                else
                    this.ToolBarSaveButtonEnabled = true;
                Settings1.Default.CD_PURGRP_SET = this._header.CurrentRow["CD_PURGRP"].ToString();
                Settings1.Default.CD_TPPO_SET = this._header.CurrentRow["CD_TPPO"].ToString();
                Settings1.Default.FG_PAYMENT_SET = this._header.CurrentRow["FG_PAYMENT"].ToString();
                Settings1.Default.CD_EXCH = this._header.CurrentRow["CD_EXCH"].ToString();
                Settings1.Default.RT_EXCH = D.GetDecimal(this._header.CurrentRow["RT_EXCH"]);
                Settings1.Default.DC_RMK_TEXT = D.GetString(this.txt발주텍스트비고1.Text);
                Settings1.Default.DC_RMK_TEXT2 = D.GetString(this.txt발주텍스트비고2.Text);
                Settings1.Default.TP_UM_TAX = this._header.CurrentRow["TP_UM_TAX"].ToString();
                Settings1.Default.Save();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
                this.ToolBarSaveButtonEnabled = true;
                if (this.추가모드여부)
                {
                    this.txt발주번호.Enabled = true;
                    this.btn품목추가.Enabled = true;
                    this.btn품목삭제.Enabled = true;
                }
            }
        }

        protected override bool BeforeSave()
        {
            if (this.m_tab_poh.TabPages.Contains(this.tabPage7) && this.ShowMessage("발주유형이 매입자동프로세스 입니다.\n매입정보TAP에 데이터를 입력하셨으면'확인'버튼을 눌러주세요.", "QK2") != DialogResult.OK)
                return false;
            if (this._m_dt == "100")
            {
                foreach (DataRow dataRow in this._flexD.DataTable.Select())
                {
                    if (D.GetDecimal(this._header.CurrentRow["DT_PO"]) > D.GetDecimal(dataRow["DT_LIMIT"]) || D.GetDecimal(this._header.CurrentRow["DT_PO"]) > D.GetDecimal(dataRow["DT_PLAN"]))
                    {
                        this.ShowMessage("발주일자보다 납기일/납품예정일이 빠릅니다.");
                        return false;
                    }
                    if (D.GetDecimal(dataRow["DT_LIMIT"]) < D.GetDecimal(dataRow["DT_PLAN"]))
                    {
                        this.ShowMessage("납기일보다 납품예정일이 느립니다");
                        return false;
                    }
                }
            }
            if (!this.HeaderCheck(0))
                return false;
            if (Global.MainFrame.ServerKeyCommon == "DAEJOOKC")
            {
                if (this.ctx구매그룹.CodeValue == "")
                {
                    this.ctx구매그룹.Focus();
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl구매그룹명.Text });
                    return false;
                }
                if (this.ctx발주형태.CodeValue == "")
                {
                    this.ctx발주형태.Focus();
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl발주형태.Text });
                    return false;
                }
            }
            if (!this.자동입고여부체크)
                return false;
            if (D.GetString(this._header.CurrentRow["TP_GR"]) == "103" || D.GetString(this._header.CurrentRow["TP_GR"]) == "104")
            {
                foreach (DataRow dataRow in this._flexD.DataTable.Select())
                {
                    if (D.GetString(dataRow["CD_SL"]) == string.Empty)
                    {
                        this.ShowMessage("발주유형이 입고 후까지 처리되는 경우 창고데이터는 필수입니다.");
                        return false;
                    }
                }
            }
            if (this.m_sEnv_FG_TAX == "100" && this.sFG_TAXcheck == "100" && D.GetString(this._header.CurrentRow["YN_IMPORT"]) != "Y")
            {
                DataTable table = this._flexD.DataTable.DefaultView.ToTable(true, "FG_TAX");
                if (table.Rows.Count != 1 || D.GetString(table.Rows[0]["FG_TAX"]) != D.GetString(this._header.CurrentRow["FG_TAX"]))
                {
                    this.ShowMessage("과세구분이 일치하지 않는 품목이 있습니다.");
                    return false;
                }
            }
            if (this.bStandard && Global.MainFrame.ServerKey == "SINJINSM")
            {
                int num1;
                if ((num1 = this.checkCLS()) != 0)
                {
                    this._flexD.Select(num1, "CLS_L");
                    return false;
                }
                int num2;
                if ((num2 = this.checkITEMGRP()) != 0)
                {
                    this._flexD.Select(num2, "NM_ITEMGRP");
                    return false;
                }
            }
            if (Global.MainFrame.ServerKeyCommon == "MHIK" && !this._biz.GetMHIK(this.tb_NO_PO_MH.Text))
            {
                this.ShowMessage("해당 발주번호는 이미 등록된 건입니다.");
                return false;
            }
            if (BASIC.GetMAEXC_Menu(nameof(P_CZ_PU_PO_WINTEC), "PU_A00000035") == "100")
            {
                foreach (DataRow dataRow in this._flexD.DataTable.Select())
                {
                    if (D.GetString(dataRow["CD_SL"]) == string.Empty)
                    {
                        this.ShowMessage("저장시 창고데이터는 필수입니다. (메뉴별 통제)");
                        return false;
                    }
                }
            }
            if (MA.ServerKey(false, new string[] { "TSUBAKI" }) && !this.chk_AM_TSUBAKI())
            {
                this.ShowMessage("수량 단가 계산금액 또는 재고단위 수량 계산값과 일치하지 않는 금액, 재고단위수량 데이터가 존재합니다.");
                return false;
            }
            if (MA.ServerKey(false, new string[] { "SANSUNG" }))
            {
                foreach (DataRow dataRow in this._flexD.DataTable.Select())
                {
                    if (D.GetString(this._header.CurrentRow["CD_PLANT"]) != D.GetString(dataRow["CD_PLANT"]))
                    {
                        this.ShowMessage("헤더 공장코드와 라인 공장코드가 다른 항목이 있습니다.");
                        return false;
                    }
                }
            }
            if (!(D.GetString(this._header.CurrentRow["TP_GR"]) != "100") || !(D.GetString(this._header.CurrentRow["TP_GR"]) != "") || !(D.GetString(this._header.CurrentRow["FG_TRANS"]) == "004") && !(D.GetString(this._header.CurrentRow["FG_TRANS"]) == "005"))
                return true;
            this.ShowMessage("해당 발주유형(해외거래)은 자동프로세스를 사용할 수 없습니다.");
            return false;
        }

        private bool HeaderCheck(int p_chk)
        {
            if (p_chk != 1 && this.MainFrameInterface.ServerKeyCommon != "INITECH" && this.ctx거래처.CodeValue == "")
            {
                this.ctx거래처.Focus();
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl거래처명.Text });
                return false;
            }
            if (this.dtp발주일자.Text == "")
            {
                this.dtp발주일자.Focus();
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl발주일자.Text });
                return false;
            }
            if (!this.dtp발주일자.IsValidated)
            {
                this.ShowMessage(공통메세지.입력형식이올바르지않습니다, new string[0]);
                this.dtp발주일자.Focus();
                return false;
            }
            if (this.cbo공장.SelectedValue == null || this.cbo공장.SelectedValue.ToString() == "")
            {
                this.cbo공장.Focus();
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl공장명.Text });
                return false;
            }
            if (this.MainFrameInterface.ServerKeyCommon != "HCT" && this.MainFrameInterface.ServerKeyCommon != "DAEJOOKC" && this.ctx구매그룹.CodeValue == "")
            {
                this.ctx구매그룹.Focus();
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[1] { this.lbl구매그룹명.Text });
                return false;
            }
            if (this.ctx담당자.CodeValue == "")
            {
                this.ctx담당자.Focus();
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl담당자.Text });
                return false;
            }
            if (this._m_tppo_change != "001" && this.MainFrameInterface.ServerKeyCommon != "DAEJOOKC" && this.ctx발주형태.CodeValue == "")
            {
                this.ctx발주형태.Focus();
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl발주형태.Text });
                return false;
            }
            if (this.cbo환정보.SelectedValue == null || this.cbo환정보.SelectedValue.ToString() == "")
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl환정보.Text });
                this.cbo환정보.Focus();
                return false;
            }
            if (this.cur환정보.DecimalValue == 0M)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl환정보.Text });
                this.cur환정보.Focus();
                return false;
            }
            if (this.cbo부가세여부.SelectedValue == null || this.cbo부가세여부.SelectedValue.ToString() == "")
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl부가세여부.Text });
                this.cbo부가세여부.Focus();
                return false;
            }
            DataTable dataTable = BASICPU.MA_MENU_CONTROL_VALUES(Global.MainFrame.CurrentPageID, Global.MainFrame.CurrentModule);
            if (!((dataTable == null || dataTable.Rows.Count <= 0 ? "N" : D.GetString(dataTable.Rows[0]["YN_DT_CONTROL"])) == "Y") || !(D.GetDecimal(this.dtp발주일자.Text) < D.GetDecimal(Global.MainFrame.GetStringToday)))
                return true;
            this.ShowMessage(공통메세지._보다커야합니다, new string[] { "현재일자" });
            this.dtp발주일자.Text = Global.MainFrame.GetStringToday;
            this.dtp발주일자.Focus();
            return false;
        }

        protected override bool SaveData()
        {
            bool lb_RcvSave = false;
            bool lb_RevSave = false;
            if (!base.SaveData() || !this.Verify() || !this._flexD.HasNormalRow && this._header.JobMode == 0)
                return false;
            if (!this._flexD.HasNormalRow)
            {
                base.OnToolBarDeleteButtonClicked(null, null);
                return false;
            }
            this.SUMFunction();
            string str1 = this.txt발주번호.Text.Trim();
            StringBuilder stringBuilder1 = new StringBuilder();
            string str2 = "품목코드\t 매입형태코드\t";
            stringBuilder1.AppendLine(str2);
            string str3 = "-".PadRight(75, '-');
            stringBuilder1.AppendLine(str3);
            bool flag1 = true;
            if (this.추가모드여부)
            {
                if (str1 != "" && str1.Substring(0, 1) == " ")
                {
                    this.ShowMessage("발주번호 첫자리에 공백은 올 수 없습니다");
                    return false;
                }
                string str4 = this.dtp발주일자.Text.Substring(0, 6).Trim();
                if (Global.MainFrame.ServerKeyCommon == "DAEYONG")
                    str4 = this.dtp발주일자.Text.Trim();
                else if (Global.MainFrame.ServerKeyCommon == "ABLBIO")
                {
                    DataRow[] dataRowArray = this._flexD.DataTable.Select("NO_PR <> '' ");
                    string NO_PR = "";
                    string str5 = "";
                    if (dataRowArray != null && dataRowArray.Length > 0)
                        NO_PR = dataRowArray[0]["NO_PR"].ToString();
                    if (NO_PR != "")
                    {
                        string str6 = this._biz.Get_PREMP(NO_PR).Rows[0]["NO_EMP"].ToString();
                        str5 = str6.Substring(str6.Length - 3, 3);
                    }
                    if (str1 == string.Empty)
                    {
                        str4 = this.dtp발주일자.Text.Trim().Substring(2, 6) + str5;
                        str1 = this._biz.Get_CPGETNO(new object[] { this.LoginInfo.CompanyCode,
                                                                    "PU",
                                                                    "03",
                                                                    str4});
                        this.txt발주번호.Text = str1;
                    }
                }
                str1 = str1 == "" ? (string)this.GetSeq(this.LoginInfo.CompanyCode, "PU", "03", str4) : this.txt발주번호.Text.Trim();
                this._header.CurrentRow["NO_PO"] = str1;
                if (Global.MainFrame.ServerKeyCommon == "MHIK" && string.IsNullOrEmpty(D.GetString(this.tb_NO_PO_MH.Text)))
                    this.tb_NO_PO_MH.Text = str1;
                if (this._flexD.HasNormalRow)
                {
                    int num1 = 0;
                    foreach (DataRow dataRow1 in this._flexD.DataTable.Select())
                    {
                        ++num1;
                        if (dataRow1.RowState != DataRowState.Deleted)
                        {
                            if (this.sPUSU == "100")
                            {
                                DataRow[] dataRowArray = this._flexDD.DataTable.Select("NO_POLINE = '" + D.GetString(dataRow1["NO_LINE"]) + "'");
                                if (dataRowArray != null && dataRowArray.Length != 0)
                                {
                                    foreach (DataRow dataRow2 in dataRowArray)
                                        dataRow2["NO_POLINE"] = num1;
                                }
                            }
                            dataRow1["NO_PO"] = str1;
                            dataRow1["NO_LINE"] = num1;
                        }
                    }
                    if (this.m_tab_poh.TabPages.Contains(this.tabPage8))
                    {
                        foreach (DataRow row in this._flexH.DataTable.Rows)
                            row["NO_PO"] = str1;
                    }
                    if ((this.sPUIV == "100" || this.sPUIV == "200") && this._flexIV.DataTable.Rows.Count > 0)
                    {
                        if (D.GetString(this._biz.Get_TPPO_PURGRP(new object[] { Global.MainFrame.LoginInfo.CompanyCode.ToString(),
                                                                                 D.GetString( this.ctx구매그룹.CodeValue),
                                                                                 D.GetString( this.ctx발주형태.CodeValue),
                                                                                 Global.SystemLanguage.MultiLanguageLpoint.ToString() }).Tables[1].Rows[0]["YN_PBL"]) != "Y")
                        {
                            this.ShowMessage("기성매입을 사용하지 않는 발주형태 입니다.");
                            return false;
                        }
                        if (!this.Chk금액())
                            return false;
                        if (ComFunc.getGridGroupBy(this._flexD.DataTable, new string[] { "CD_PJT" }, true).Rows.Count > 1)
                        {
                            this.ShowMessage("발주라인의 프로젝트코드는 동일해야 합니다.");
                            return false;
                        }
                        foreach (DataRow row in this._flexIV.DataTable.Rows)
                        {
                            row["CD_PJT"] = D.GetString(this._flexD.DataTable.Rows[0]["CD_PJT"]);
                            row["SEQ_PROJECT"] = D.GetString(this._flexD.DataTable.Rows[0]["SEQ_PROJECT"]);
                            row["NO_PO"] = str1;
                        }
                    }
                }
                if (Global.MainFrame.ServerKeyCommon == "TRIGEM")
                {
                    string str7 = D.GetString(this._header.CurrentRow["FG_TRANS"]);
                    string empty1 = string.Empty;
                    if (str7 == "004" || str7 == "005")
                    {
                        string empty2 = string.Empty;
                        string str8;
                        string str9;
                        if (str7 == "004")
                        {
                            str8 = "2";
                            str9 = "201210";
                        }
                        else
                        {
                            str8 = "8";
                            str9 = "201209";
                        }
                        string str10 = str8 + ((string)this.GetSeq(this.LoginInfo.CompanyCode, "PU", "39", str9)).Substring(7, 9);
                        this._header.CurrentRow["DACU_NO"] = str10;
                        this.txt문서번호.Text = str10;
                    }
                }
                if (Global.MainFrame.ServerKeyCommon == "HANILTOYO")
                {
                    string seq = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PU", "44", this.dtp발주일자.Text);
                    this._header.CurrentRow["DACU_NO"] = seq;
                    this.txt문서번호.Text = seq;
                }
                if (this._flexD.HasNormalRow && this._header.CurrentRow["YN_REQ"].ToString() == "N")
                {
                    this.cPU_RCVH.DT_PURCVH.Clear();
                    this.cPU_RCVL.DT_PURCV.Clear();
                    this.GetPU_RCV_Save(this._header.GetChanges(), this._flexD.DataTable);
                    lb_RcvSave = true;
                }
                if (BASIC.GetMAEXC_Menu(nameof(P_CZ_PU_PO_WINTEC), "PU_Z00000002") == "200" && D.GetDecimal(this._flexD["AM_OLD"]) > 0M && D.GetDecimal(this._flexD["AM_OLD"]) < D.GetDecimal(this._flexD["AM"]))
                {
                    this.ShowMessage(공통메세지._은_보다작거나같아야합니다, new string[] { "변경금액", "적용금액" });
                    return false;
                }
            }
            if (this._flexD.HasNormalRow && this._header.CurrentRow["TP_GR"].ToString() == "101")
                lb_RevSave = true;
            if ((this.sPUIV == "100" || this.sPUIV == "200") && this._flexIV.DataTable.Rows.Count > 0 && !this.Chk금액())
                return false;
            DataTable ynSu = this._biz.GetYN_SU(D.GetString(this._header.CurrentRow["CD_TPPO"]));
            if (this._flexD.HasNormalRow)
            {
                foreach (DataRow dataRow in this._flexD.DataTable.Select())
                {
                    if (dataRow.RowState == DataRowState.Added || this.str복사구분 == "COPY")
                    {
                        if (dataRow["YN_ORDER"].ToString() == "Y" || dataRow["YN_REQ"].ToString() == "N" || lb_RevSave)
                        {
                            dataRow["FG_POST"] = "R";
                            dataRow["FG_POCON"] = "002";
                            this.m_pnlHeader_Enabled();
                            this.oneGrid2.Enabled = false;
                            this.SetHeadControlEnabled(false, 2);
                            this.btn요청적용.Enabled = false;
                            this.btn품의적용.Enabled = false;
                            this.btn수주적용.Enabled = false;
                            this.btn수주_의뢰적용.Enabled = false;
                            this.curNEGO금액.Enabled = false;
                            this.btnNEGO적용.Enabled = false;
                            this.curDe.Enabled = false;
                        }
                        else
                        {
                            dataRow["FG_POST"] = "O";
                            dataRow["FG_POCON"] = "001";
                            this.SetHeadControlEnabled(false, 1);
                            this.curNEGO금액.Enabled = true;
                            this.btnNEGO적용.Enabled = true;
                            this.curDe.Enabled = true;
                        }
                        if (dataRow["NO_PO"].ToString().Trim().Length < 3)
                        {
                            if (this._header.CurrentRow["NO_PO"].ToString() == string.Empty)
                            {
                                this.ShowMessage("발주번호는 공백이 될 수 없습니다.");
                                return false;
                            }
                            dataRow["NO_PO"] = this._header.CurrentRow["NO_PO"].ToString();
                        }
                        if (D.GetString(this._header.CurrentRow["FG_TRANS"]) == "001" && D.GetString(dataRow["FG_TAX"]) == "")
                        {
                            this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD("과세구분") });
                            return false;
                        }
                    }
                    if (this._m_Company_only == "001" && D.GetString(this._header.CurrentRow["FG_TPPURCHASE"]) != D.GetString(dataRow["CD_TP"]))
                    {
                        string str11 = dataRow["CD_ITEM"].ToString().PadRight(15, ' ') + " " + dataRow["CD_TP"].ToString().PadRight(15, ' ');
                        stringBuilder1.AppendLine(str11);
                        flag1 = false;
                    }
                    if (this.s소요자재체크 != "000" && D.GetString(ynSu.Rows[0]["YN_SU"]) == "Y")
                    {
                        StringBuilder stringBuilder2 = new StringBuilder();
                        DataRow[] dataRowArray = this._flexDD.DataTable.Select("NO_POLINE = '" + dataRow["NO_LINE"] + "'");
                        if (dataRowArray == null || dataRowArray.Length == 0)
                            stringBuilder2.AppendLine(D.GetString(dataRow["CD_ITEM"]));
                        if (stringBuilder2.Length > 0 && this.s소요자재체크 == "100")
                        {
                            if (this.ShowDetailMessage(this.DD("아래 품목의 소요자재가 없습니다. 저장하시겠습니까?"), stringBuilder2.ToString(), "QY2") != DialogResult.Yes)
                                return false;
                        }
                        else if (stringBuilder2.Length > 0 && this.s소요자재체크 == "200")
                        {
                            this.ShowDetailMessage(this.DD("아래 품목의 소요자재가 없어 저장할 수 없습니다."), stringBuilder2.ToString());
                            return false;
                        }
                    }
                }
                if (!flag1)
                {
                    this.ShowDetailMessage("발주형태의 매입정보와 품목정보의 매입정보가 다릅니다.\n▼ 버튼을 눌러서 목록을 확인하세요!", D.GetString(stringBuilder1));
                    return false;
                }
            }
            this._header.CurrentRow["DC50_PO"] = this.txt비고.Text;
            this._header.CurrentRow["FG_TRACK"] = "M";
            this._header.CurrentRow["DC_RMK2"] = this.txt비고2.Text;
            this._header.CurrentRow["DC_RMK_TEXT"] = this.txt발주텍스트비고1.Text;
            this._header.CurrentRow["DC_RMK_TEXT2"] = this.txt발주텍스트비고2.Text;
            if (this.m_tab_poh.TabPages.Contains(this.tabPage7))
            {
                this._header.CurrentRow["DT_PROCESS_IV"] = this.dtp매입일자.Text;
                this._header.CurrentRow["DT_PAY_PRE_IV"] = this.dtp지급예정일자.Text;
                this._header.CurrentRow["DT_DUE_IV"] = this.dtp만기일자.Text;
                this._header.CurrentRow["FG_PAYBILL_IV"] = this.cbo지급구분.SelectedValue;
                this._header.CurrentRow["CD_DOCU_IV"] = !(D.GetString(this.cbo전표유형.SelectedValue) == string.Empty) ? this.cbo전표유형.SelectedValue : "45";
                this._header.CurrentRow["AM_K_IV"] = this.cur공급가액.DecimalValue;
                this._header.CurrentRow["VAT_TAX_IV"] = this.cur부가세액.DecimalValue;
                this._header.CurrentRow["DC_RMK_IV"] = (this.txt매입비고).Text;
            }
          this.cbo운송방법.Enabled = true;
            this.cbo지불조건.Enabled = true;
            this.txt지불조건.Enabled = true;
            this.cbo운송방법.Enabled = true;
            this.txt도착지.Enabled = true;
            this.txt선적지.Enabled = true;
            this.ctx도착지.Enabled = true;
            this.ctx선적지.Enabled = true;
            this.cbo선적조건.Enabled = true;
            this.cbo운임조건.Enabled = true;
            this.cbo지급기준.Enabled = true;
            this.cur지불조건.Enabled = true;
            this.cbo원산지.Enabled = true;
            this.ctx업체전용.Enabled = true;
            DataTable changes1 = this._header.GetChanges();
            DataTable dataTable1 = this._flexD.GetChanges();
            if (changes1 == null && dataTable1 == null)
                return true;
            this.cPU_RCVH.DT_PURCVH.GetChanges();
            this.cPU_RCVL.DT_PURCV.GetChanges();
            string empty3 = string.Empty;
            DataTable dt_budget = this._biz.PU_BUDGET_HST();
            if (this._구매예산CHK설정FI == "100" && dataTable1 != null)
            {
                string str12 = "YN_BUDGET = 'Y' AND ( CD_BUDGET = '' OR CD_BUDGET IS NULL  OR CD_BGACCT = '' OR CD_BGACCT IS NULL ";
                if (this._YN_CdBizplan == "1")
                    str12 += " OR CD_BIZPLAN = '' OR CD_BIZPLAN IS NULL ";
                DataRow[] dataRowArray1 = this._flexD.DataTable.Select(str12 + " ) ");
                if (dataRowArray1 != null && dataRowArray1.Length > 0)
                {
                    this.ShowMessage("예산확인 선택(Y)시 발주형태,CC코드,예산단위,예산계정은 필수입력입니다.");
                    this._flexD.Focus();
                    return false;
                }
                string empty4 = string.Empty;
                DataTable dataTable2 = this.예산chk(this._flexD.DataTable);
                if (dataTable2.Rows.Count > 0)
                {
                    string filterExpression = !(this._구매예산CHK설정FI == "000") ? "( AM_JAN < 0 AND TP_BUNIT = 'Y') AND ERROR_MSG IS NOT NULL" : "( AM_JAN < 0 AND TP_BUNIT = '4') AND ERROR_MSG IS NOT NULL";
                    DataRow[] dataRowArray2 = dataTable2.Select(filterExpression);
                    bool flag2;
                    if (dataRowArray2 != null && dataRowArray2.Length > 0)
                    {
                        this.ShowMessage("예산통제대상계정이 예산잔액을 초과했거나 CHK시 오류가 발생했습니다");
                        flag2 = false;
                        this.txt예산통과.Text = "N";
                        this._header.CurrentRow["BUDGET_PASS"] = "N";
                        for (int index = 0; index < this._flexD.DataTable.Rows.Count; ++index)
                        {
                            if (this._flexD.DataTable.Rows[index].RowState != DataRowState.Deleted && D.GetString(this._flexD.DataTable.Rows[index]["YN_BUDGET"]) == "Y")
                                this._flexD.DataTable.Rows[index]["BUDGET_PASS"] = "N";
                        }
                    }
                    else
                    {
                        flag2 = true;
                        this.txt예산통과.Text = "Y";
                        this._header.CurrentRow["BUDGET_PASS"] = "Y";
                        for (int index = 0; index < this._flexD.DataTable.Rows.Count; ++index)
                        {
                            if (this._flexD.DataTable.Rows[index].RowState != DataRowState.Deleted && D.GetString(this._flexD.DataTable.Rows[index]["YN_BUDGET"]) == "Y")
                                this._flexD.DataTable.Rows[index]["BUDGET_PASS"] = "Y";
                        }
                    }
                    P_PU_BUDGET_SUB pPuBudgetSub = new P_PU_BUDGET_SUB(this._flexD.DataTable, this.dtp발주일자.Text, "NO_PO");
                    ((Form)pPuBudgetSub).ShowDialog(this);
                    if (((Form)pPuBudgetSub).ShowDialog(this) == DialogResult.OK || !flag2)
                        return false;
                    foreach (DataRow row1 in dataTable2.Rows)
                    {
                        DataRow row2 = dt_budget.NewRow();
                        row2["NO_PU"] = this._header.CurrentRow["NO_PO"].ToString();
                        row2["NENU_TYPE"] = "PU_PO_REG";
                        row2["CD_BUDGET"] = row1["CD_BUDGET"];
                        row2["NM_BUDGET"] = row1["NM_BUDGET"];
                        row2["CD_BGACCT"] = row1["CD_BGACCT"];
                        row2["NM_BGACCT"] = row1["NM_BGACCT"];
                        row2["CD_BIZPLAN"] = row1["CD_BIZPLAN"];
                        row2["NM_BIZPLAN"] = row1["NM_BIZPLAN"];
                        row2["AM_ACTSUM"] = row1["AM_ACTSUM"];
                        row2["AM_JSUM"] = row1["AM"];
                        row2["RT_JSUM"] = row1["RT_JSUM"];
                        row2["AM"] = row1["AM"];
                        row2["AM_JAN"] = row1["AM_JAN"];
                        row2["TP_BUNIT"] = row1["TP_BUNIT"];
                        row2["ERROR_MSG"] = row1["ERROR_MSG"];
                        row2["ID_INSERT"] = Global.MainFrame.LoginInfo.EmployeeNo;
                        dt_budget.Rows.Add(row2);
                    }
                }
            }
            if (Global.MainFrame.ServerKeyCommon.StartsWith("SOLIDTECH", true, CultureInfo.CurrentCulture))
            {
                StringBuilder stringBuilder3 = new StringBuilder();
                foreach (DataRow row in dataTable1.Rows)
                {
                    if (this.dtp납기일.Text == string.Empty)
                        stringBuilder3.Append(row["DT_LIMIT"].ToString() + row["CD_ITEM"].ToString() + "|");
                    else
                        stringBuilder3.Append(this.dtp납기일.Text + row["CD_ITEM"].ToString() + "|");
                }
                DataTable itemSerial = this._biz.GetItemSerial(new object[] { this.LoginInfo.CompanyCode,
                                                                              this.ctx거래처.CodeValue,
                                                                              this.cbo공장.SelectedValue,
                                                                              stringBuilder3.ToString() });
                foreach (DataRow row in dataTable1.Rows)
                {
                    if (itemSerial.Rows.Count > 0 && D.GetInt(row["QT_PO"]) > 0)
                    {
                        DataRow[] dataRowArray = itemSerial.Select(string.Format("DT_LIMIT = '{0}' AND CD_ITEM = '{1}'", row["DT_LIMIT"], row["CD_ITEM"]));
                        if (dataRowArray != null && dataRowArray.Length > 0)
                        {
                            int num = D.GetInt(row["QT_PO_MM"]) - 1;
                            row["DC1"] = string.Format("{0}{1:D5}", dataRowArray[0]["PREFIX"], D.GetInt(dataRowArray[0]["POSTFIX"]));
                            row["DC2"] = string.Format("{0}{1:D5}", dataRowArray[0]["PREFIX"], (D.GetInt(dataRowArray[0]["POSTFIX"]) + num));
                            dataRowArray[0].BeginEdit();
                            dataRowArray[0]["POSTFIX"] = (D.GetInt(dataRowArray[0]["POSTFIX"]) + num + 1);
                            dataRowArray[0].EndEdit();
                            dataRowArray[0].AcceptChanges();
                            for (int index = this._flexD.Rows.Fixed; index < this._flexD.Rows.Count; ++index)
                            {
                                if (row["NO_LINE"].Equals(this._flexD[index, "NO_LINE"]))
                                {
                                    this._flexD[index, "DC1"] = row["DC1"];
                                    this._flexD[index, "DC2"] = row["DC2"];
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            if (this.추가모드여부 && D.GetString(this._header.CurrentRow["YN_ORDER"]) == "Y" && BASIC.GetMAEXC("발주등록(공장)-프로젝트예산통제설정") == "100")
            {
                P_PU_PJT_BUDGET_CTL_SUB puPjtBudgetCtlSub = new P_PU_PJT_BUDGET_CTL_SUB(dataTable1, "NO_PO", "REG");
                if (((Form)puPjtBudgetCtlSub).ShowDialog() != DialogResult.OK)
                    return false;
                if (puPjtBudgetCtlSub.ret_data != null && puPjtBudgetCtlSub.ret_data.Rows.Count != 0)
                    dataTable1 = puPjtBudgetCtlSub.ret_data;
            }
            DataTable dtDD = null;
            if (this.sPUSU == "100" && this._flexDD.Enabled)
            {
                if (this.str복사구분 == "COPY")
                {
                    dtDD = this._flexDD.DataTable.Clone();
                    foreach (DataRow row in this._flexDD.DataTable.Select("", "", DataViewRowState.CurrentRows))
                        dtDD.ImportRow(row);
                }
                else
                    dtDD = this._flexDD.GetChanges();
                if (dtDD != null)
                {
                    foreach (DataRow row in dtDD.Rows)
                    {
                        if (row.RowState != DataRowState.Deleted)
                        {
                            row.BeginEdit();
                            row["NO_PO"] = this._header.CurrentRow["NO_PO"];
                            row.EndEdit();
                        }
                    }
                }
            }
            DataTable dtLOT = null;
            string strNOIO = "";
            if (D.GetString(this._header.CurrentRow["TP_GR"]) == "103" || D.GetString(this._header.CurrentRow["TP_GR"]) == "104" && Global.MainFrame.LoginInfo.MngLot == "Y")
            {
                DataRow[] dataRowArray = this._flexD.DataTable.Select("FG_SERNO = '002'");
                if (dataRowArray != null && dataRowArray.Length > 0)
                {
                    DataTable dt = this._biz.dtLot_Schema("LOT");
                    if (this.추가모드여부)
                    {
                        strNOIO = !(D.GetString(this._header.CurrentRow["YN_RETURN"]) == "Y") ? (string)this.GetSeq(this.LoginInfo.CompanyCode, "PU", "06", this.dtp발주일자.MaskEditBox.ClipText.Substring(0, 6)) : (string)this.GetSeq(this.LoginInfo.CompanyCode, "PU", "27", this.dtp발주일자.MaskEditBox.ClipText.Substring(0, 6));
                    }
                    else
                    {
                        string lotio = this._biz.getLOTIO(D.GetString(this._header.CurrentRow["NO_PO"]));
                        if (lotio == "")
                            return false;
                        strNOIO = lotio;
                    }
                    foreach (DataRow dataRow in dataRowArray)
                    {
                        if (dataRow.RowState == DataRowState.Added)
                        {
                            DataRow row = dt.NewRow();
                            row["NO_IO"] = strNOIO;
                            row["NO_IOLINE"] = dataRow["NO_LINE"];
                            row["CD_QTIOTP"] = D.GetString(this._header.CurrentRow["FG_TPRCV"]);
                            row["NM_QTIOTP"] = Duzon.ERPU.MF.Common.CodeSearch.GetCodeInfo(MasterSearch.MM_EJTP, new string[] { MA.Login.회사코드,
                                                                                                           D.GetString(this._header.CurrentRow["FG_TPRCV"]) })["NM_QTIOTP"];
                            row["FG_TRANS"] = D.GetString(this._header.CurrentRow["FG_TRANS"]);
                            row["DT_IO"] = D.GetString(this._header.CurrentRow["DT_PO"]);
                            row["CD_SL"] = dataRow["CD_SL"];
                            row["NM_SL"] = dataRow["NM_SL"];
                            row["CD_ITEM"] = dataRow["CD_ITEM"];
                            row["NM_ITEM"] = dataRow["NM_ITEM"];
                            row["UNIT_IM"] = dataRow["UNIT_IM"];
                            row["STND_ITEM"] = dataRow["STND_ITEM"];
                            row["CD_PJT"] = dataRow["CD_PJT"];
                            row["NM_PROJECT"] = dataRow["NM_PJT"];
                            row["AM"] = dataRow["AM"];
                            row["AM_EX"] = dataRow["AM_EX"];
                            row["UM_EX"] = dataRow["UM_EX"];
                            row["QT_GOOD_INV"] = dataRow["QT_PO"];
                            if (D.GetString(this._header.CurrentRow["YN_RETURN"]) == "Y")
                            {
                                row["FG_IO"] = "030";
                                row["YN_RETURN"] = "Y";
                            }
                            else
                                row["FG_IO"] = "001";
                            row["CD_PLANT"] = dataRow["CD_PLANT"];
                            row["YN_RETURN"] = "N";
                            dt.Rows.Add(row);
                        }
                    }
                    if (D.GetString(this._header.CurrentRow["YN_RETURN"]) == "Y")
                    {
                        P_PU_LOT_SUB_I pPuLotSubI = new P_PU_LOT_SUB_I(dt, "Y");
                        if (((Form)pPuLotSubI).ShowDialog(this) != DialogResult.OK)
                            return false;
                        dtLOT = pPuLotSubI.dtL;
                    }
                    else
                    {
                        P_PU_LOT_SUB_R pPuLotSubR = new P_PU_LOT_SUB_R(dt);
                        if (((Form)pPuLotSubR).ShowDialog(this) != DialogResult.OK)
                            return false;
                        dtLOT = pPuLotSubR.dtL;
                    }
                }
            }
            DataTable dtSERL = null;
            if (D.GetString(this._header.CurrentRow["TP_GR"]) == "103" || D.GetString(this._header.CurrentRow["TP_GR"]) == "104" && string.Compare(this.MNG_SERIAL, "Y") == 0)
            {
                DataRow[] dataRowArray = this._flexD.DataTable.Select("FG_SERNO = '003'");
                if (dataRowArray != null && dataRowArray.Length > 0)
                {
                    DataTable dt = this._biz.dtLot_Schema("SERIAL");
                    if (strNOIO == string.Empty)
                    {
                        if (this.추가모드여부)
                        {
                            strNOIO = !(D.GetString(this._header.CurrentRow["YN_RETURN"]) == "Y") ? (string)this.GetSeq(this.LoginInfo.CompanyCode, "PU", "06", this.dtp발주일자.MaskEditBox.ClipText.Substring(0, 6)) : (string)this.GetSeq(this.LoginInfo.CompanyCode, "PU", "27", this.dtp발주일자.MaskEditBox.ClipText.Substring(0, 6));
                        }
                        else
                        {
                            string lotio = this._biz.getLOTIO(D.GetString(this._header.CurrentRow["NO_PO"]));
                            if (lotio == "")
                                return false;
                            strNOIO = lotio;
                        }
                    }
                    foreach (DataRow dataRow in dataRowArray)
                    {
                        if (dataRow.RowState == DataRowState.Added)
                        {
                            DataRow row = dt.NewRow();
                            row["NO_IO"] = strNOIO;
                            row["NO_IOLINE"] = dataRow["NO_LINE"];
                            row["CD_QTIOTP"] = D.GetString(this._header.CurrentRow["FG_TPRCV"]);
                            row["NM_QTIOTP"] = Duzon.ERPU.MF.Common.CodeSearch.GetCodeInfo(MasterSearch.MM_EJTP, new string[] { MA.Login.회사코드,
                                                                                                                                D.GetString(this._header.CurrentRow["FG_TPRCV"]) })["NM_QTIOTP"];
                            row["DT_IO"] = D.GetString(this._header.CurrentRow["DT_PO"]);
                            row["CD_SL"] = dataRow["CD_SL"];
                            row["NM_SL"] = dataRow["NM_SL"];
                            row["CD_ITEM"] = dataRow["CD_ITEM"];
                            row["NM_ITEM"] = dataRow["NM_ITEM"];
                            row["UNIT_IM"] = dataRow["UNIT_IM"];
                            row["STND_ITEM"] = dataRow["STND_ITEM"];
                            row["QT_GOOD_INV"] = dataRow["QT_PO"];
                            if (D.GetString(this._header.CurrentRow["YN_RETURN"]) == "Y")
                            {
                                row["FG_IO"] = "030";
                                row["YN_RETURN"] = "Y";
                            }
                            else
                                row["FG_IO"] = "001";
                            row["CD_PLANT"] = dataRow["CD_PLANT"];
                            row["YN_RETURN"] = "N";
                            dt.Rows.Add(row);
                        }
                    }
                    if (D.GetString(this._header.CurrentRow["YN_RETURN"]) == "Y")
                    {
                        P_PU_SERL_SUB_I pPuSerlSubI = new P_PU_SERL_SUB_I(dt);
                        if (((Form)pPuSerlSubI).ShowDialog(this) != DialogResult.OK)
                            return false;
                        dtSERL = pPuSerlSubI.dtL;
                    }
                    else
                    {
                        P_PU_SERL_SUB_R pPuSerlSubR = new P_PU_SERL_SUB_R(dt);
                        if (((Form)pPuSerlSubR).ShowDialog(this) != DialogResult.OK)
                            return false;
                        dtSERL = pPuSerlSubR.dtL;
                    }
                }
            }
            DataTable changes2 = this._flexH.GetChanges();
            DataTable changes3 = this._flexIV.GetChanges();
            if (!this._biz.Save(changes1, dataTable1, lb_RcvSave, this.cPU_RCVH.DT_PURCVH, this.cPU_RCVL.DT_PURCV, this.str복사구분, dt_budget, this._header.CurrentRow["NO_PO"].ToString(), this._infosub_dlg.si_return, lb_RevSave, dtDD, changes2, changes3, dtLOT, strNOIO, dtSERL))
                return false;
            if (this.추가모드여부)
                this.txt발주번호.Text = str1;
            this._flexD.Focus();
            this._header.AcceptChanges();
            this._flexD.AcceptChanges();
            this._flexDD.AcceptChanges();
            this._flexIV.AcceptChanges();
            if (this.m_tab_poh.TabPages.Contains(this.tabPage8))
                this._flexH.AcceptChanges();
            ((DataTable)this._infosub_dlg.si_return.DataValue)?.AcceptChanges();
            return true;
        }

        private void GetPU_RCV_Save(DataTable pdt_Head, DataTable pdt_Line)
        {
            this.cPU_RCVH = new CDT_PU_RCVH();
            for (int index1 = 0; index1 < pdt_Line.Rows.Count; ++index1)
            {
                if (pdt_Line.Rows[index1].RowState != DataRowState.Deleted && pdt_Line.Rows[index1]["NO_RCV"].ToString() == "")
                {
                    string seq = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PU", "04", this.dtp발주일자.Text.Substring(0, 6));
                    DataRow[] dataRowArray = pdt_Line.Select("CD_PLANT ='" + pdt_Line.Rows[index1]["CD_PLANT"].ToString() + "'");
                    DataRow row = this.cPU_RCVH.DT_PURCVH.NewRow();
                    row["NO_RCV"] = seq;
                    row["CD_PLANT"] = pdt_Line.Rows[index1]["CD_PLANT"];
                    row["CD_PARTNER"] = pdt_Head.Rows[0]["CD_PARTNER"];
                    row["DT_REQ"] = pdt_Head.Rows[0]["DT_PO"];
                    row["NO_EMP"] = pdt_Head.Rows[0]["NO_EMP"];
                    row["FG_TRANS"] = pdt_Head.Rows[0]["FG_TRANS"];
                    row["FG_PROCESS"] = pdt_Head.Rows[0]["FG_TAXP"];
                    row["YN_AM"] = pdt_Head.Rows[0]["YN_AM"];
                    row["CD_EXCH"] = pdt_Head.Rows[0]["CD_EXCH"];
                    row["YN_RETURN"] = "N";
                    row["CD_SL"] = pdt_Line.Rows[index1]["CD_SL"];
                    row["YN_AUTORCV"] = pdt_Line.Rows[0]["YN_AUTORCV"];
                    row["DT_IO"] = pdt_Head.Rows[0]["DT_PO"];
                    row["CD_DEPT"] = pdt_Head.Rows[0]["CD_DEPT"];
                    row["FG_RCV"] = pdt_Head.Rows[0]["FG_TPRCV"];
                    this.cPU_RCVH.DT_PURCVH.Rows.Add(row);
                    for (int index2 = 0; index2 < dataRowArray.Length; ++index2)
                    {
                        dataRowArray[index2]["NO_RCV"] = seq;
                        dataRowArray[index2]["NO_RCVLINE"] = (index2 + 1);
                        dataRowArray[index2]["FG_POCON"] = "002";
                    }
                }
            }
            if (this.cPU_RCVH.DT_PURCVH == null || this.cPU_RCVH.DT_PURCVH.Rows.Count <= 0)
                return;
            for (int index3 = 0; index3 < this.cPU_RCVH.DT_PURCVH.Rows.Count; ++index3)
            {
                DataTable dataTable = pdt_Line.Clone();
                DataRow[] dataRowArray = pdt_Line.Select("NO_RCV ='" + this.cPU_RCVH.DT_PURCVH.Rows[index3]["NO_RCV"].ToString() + "'");
                if (dataRowArray != null && dataRowArray.Length > 0)
                {
                    for (int index4 = 0; index4 < dataRowArray.Length; ++index4)
                        dataTable.ImportRow(dataRowArray[index4]);
                    this.cPU_RCVL = new CDT_PU_RCV();
                    for (int index5 = 0; index5 < dataTable.Rows.Count; ++index5)
                    {
                        DataRow row1 = dataTable.Rows[index5];
                        DataRow row2 = this.cPU_RCVL.DT_PURCV.NewRow();
                        row2["NO_RCV"] = this.cPU_RCVH.DT_PURCVH.Rows[index3]["NO_RCV"];
                        row2["NO_LINE"] = (index5 + 1);
                        row2["DT_IO"] = this.cPU_RCVH.DT_PURCVH.Rows[index3]["DT_IO"];
                        row2["NO_PO"] = row1["NO_PO"];
                        row2["NO_POLINE"] = row1["NO_LINE"];
                        row2["CD_PURGRP"] = pdt_Head.Rows[0]["CD_PURGRP"];
                        row2["CD_ITEM"] = row1["CD_ITEM"];
                        row2["CD_UNIT_MM"] = row1["CD_UNIT_MM"];
                        row2["QT_REQ_MM"] = row1["QT_PO_MM"];
                        row2["QT_REQ"] = row1["QT_PO"];
                        row2["DT_LIMIT"] = row1["DT_LIMIT"];
                        row2["DT_PLAN"] = row1["DT_PLAN"];
                        row2["CD_EXCH"] = pdt_Head.Rows[0]["CD_EXCH"];
                        row2["RT_EXCH"] = pdt_Head.Rows[0]["RT_EXCH"];
                        row2["YN_INSP"] = "N";
                        row2["UM_EX_PO"] = row1["UM_EX_PO"];
                        row2["UM_EX"] = row1["UM_EX"];
                        row2["AM_EX"] = row1["AM_EX"];
                        row2["AM"] = row1["AM"];
                        row2["AM_EXREQ"] = row1["AM_EX"];
                        row2["AM_REQ"] = row1["AM"];
                        row2["VAT"] = row1["VAT"];
                        row2["UM"] = row1["UM"];
                        row2["CD_PJT"] = row1["CD_PJT"];
                        row2["YN_RETURN"] = row1["YN_RETURN"];
                        row2["FG_TPPURCHASE"] = row1["FG_TPPURCHASE"];
                        row2["FG_TAXP"] = pdt_Head.Rows[0]["FG_TAXP"];
                        row2["FG_TAX"] = row1["FG_TAX"];
                        row2["FG_RCV"] = row1["FG_RCV"];
                        row2["FG_TRANS"] = row1["FG_TRANS"];
                        row2["YN_AUTORCV"] = row1["YN_AUTORCV"];
                        row2["YN_REQ"] = row1["YN_REQ"];
                        row2["CD_SL"] = row1["CD_SL"];
                        row2["NO_EMP"] = pdt_Head.Rows[0]["NO_EMP"];
                        row2["CD_PLANT"] = row1["CD_PLANT"];
                        row2["CD_PARTNER"] = pdt_Head.Rows[0]["CD_PARTNER"];
                        row2["DT_REQ"] = pdt_Head.Rows[0]["DT_PO"];
                        this.cPU_RCVL.DT_PURCV.Rows.Add(row2);
                    }
                }
            }
        }

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (this.추가모드여부)
                    return;
                if (base.IsChanged())
                {
                    if (this.ShowMessage(공통메세지.변경된사항이있습니다저장하시겠습니까, new string[] { "QY2" }) == DialogResult.No || !base.BeforeSave())
                        return;
                    this.ToolBarSaveButtonEnabled = false;
                    if (this.MsgAndSave(PageActionMode.Save))
                    {
                        this.ShowMessage(PageResultMode.SaveGood);
                    }
                    else
                        this.ToolBarSaveButtonEnabled = true;
                    Settings1.Default.CD_PURGRP_SET = this._header.CurrentRow["CD_PURGRP"].ToString();
                    Settings1.Default.CD_TPPO_SET = this._header.CurrentRow["CD_TPPO"].ToString();
                    Settings1.Default.FG_PAYMENT_SET = this._header.CurrentRow["FG_PAYMENT"].ToString();
                    Settings1.Default.CD_EXCH = this._header.CurrentRow["CD_EXCH"].ToString();
                    Settings1.Default.RT_EXCH = D.GetDecimal(this._header.CurrentRow["RT_EXCH"]);
                    Settings1.Default.DC_RMK_TEXT = D.GetString(this.txt발주텍스트비고1.Text);
                    Settings1.Default.DC_RMK_TEXT2 = D.GetString(this.txt발주텍스트비고2.Text);
                    Settings1.Default.TP_UM_TAX = this._header.CurrentRow["TP_UM_TAX"].ToString();
                    Settings1.Default.Save();
                }
                this.rptHelper = new ReportHelper("R_PU_PO_REG2_0", "구매발주서");
                this.rptHelper.Printing += new ReportHelper.PrintEventHandler(this.rptHelper_Printing);
                this.rptHelper.Print();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void SetPrint(bool checkprint)
        {
            try
            {
                if (this.추가모드여부 || !this._flexD.HasNormalRow)
                    return;
                DataTable bizAreaCodeSearch = this._biz.GetBizAreaCodeSearch(this.ctx담당자.CodeValue);
                string str1 = "";
                string str2 = "";
                string str3 = "";
                if (bizAreaCodeSearch != null && bizAreaCodeSearch.Rows.Count > 0)
                {
                    str1 = D.GetString(bizAreaCodeSearch.Rows[0]["NM_BIZAREA"]);
                    if (D.GetString(bizAreaCodeSearch.Rows[0]["NO_BIZAREA"]) != "" && D.GetString(bizAreaCodeSearch.Rows[0]["NO_BIZAREA"]).Length == 10)
                        str2 = D.GetString(bizAreaCodeSearch.Rows[0]["NO_BIZAREA"]).Substring(0, 3) + "-" + D.GetString(bizAreaCodeSearch.Rows[0]["NO_BIZAREA"]).Substring(3, 2) + "-" + D.GetString(bizAreaCodeSearch.Rows[0]["NO_BIZAREA"]).Substring(5, 5);
                    if (D.GetString(bizAreaCodeSearch.Rows[0]["NO_COMPANY"]) != "" && D.GetString(bizAreaCodeSearch.Rows[0]["NO_COMPANY"]).Length == 10)
                        str3 = D.GetString(bizAreaCodeSearch.Rows[0]["NO_COMPANY"]).Substring(0, 3) + "-" + D.GetString(bizAreaCodeSearch.Rows[0]["NO_COMPANY"]).Substring(3, 2) + "-" + D.GetString(bizAreaCodeSearch.Rows[0]["NO_COMPANY"]).Substring(5, 5);
                }
                string ls_dt_po = this.dtp발주일자.Text;
                string str4 = D.GetString(this._flexD.DataTable.Rows.Count);
                if (!checkprint)
                    this.rptHelper = new ReportHelper("R_PU_PO_REG2_0", "구매발주서");
                Dictionary<string, string> dic = new Dictionary<string, string>();
                DataSet ds = this._biz.Print(this.txt발주번호.Text, this.cbo공장.SelectedValue.ToString(), this.dtp발주일자.Text);
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
                dic["담당자전화번호"] = D.GetString(ds.Tables[0].Rows[0]["EMP_NO_TEL"]);
                dic["구매그룹전화번호"] = D.GetString(ds.Tables[0].Rows[0]["PURGRP_NO_TEL"]);
                dic["구매그룹팩스번호"] = D.GetString(ds.Tables[0].Rows[0]["PURGRP_NO_FAX"]);
                dic["USER_NO_TEL"] = D.GetString(ds.Tables[0].Rows[0]["EMP_NO_TEL1"]);
                dic["NM_PACKING"] = D.GetString(ds.Tables[0].Rows[0]["NM_PACKING"]);
                dic["SUPP_ADS1_H"] = D.GetString(ds.Tables[0].Rows[0]["SUPP_ADS1_H"]);
                dic["SUPP_ADS1_H"] = D.GetString(ds.Tables[0].Rows[0]["SUPP_ADS1_D"]);
                dic["구매그룹팩스번호"] = D.GetString(ds.Tables[0].Rows[0]["PURGRP_NO_FAX"]);
                dic["도착지명"] = D.GetString(ds.Tables[0].Rows[0]["NM_ARRIVER"]);
                dic["NO_PO_BAR"] = "*" + D.GetString(ds.Tables[0].Rows[0]["NO_PO"]) + "*";
                if (this.m_sEnv_Prt_Option != "000")
                {
                    P_PU_PRINT_OPTION pPuPrintOption = new P_PU_PRINT_OPTION(ls_dt_po);
                    if (((Form)pPuPrintOption).ShowDialog(this) == DialogResult.OK)
                        ls_dt_po = pPuPrintOption.gstr_return;
                }
                string str5 = this.txt도착지.Text.Trim();
                dic["발주번호"] = this.txt발주번호.Text;
                dic["발주일자"] = ls_dt_po;
                dic["공장"] = this.cbo공장.Text;
                dic["담당자"] = this.ctx담당자.CodeName;
                dic["환율"] = this.cur환정보.Text;
                dic["환정보"] = this.cbo환정보.Text;
                dic["거래처"] = this.txt담당부서.Text;
                dic["지급조건"] = this.cbo지급조건.Text;
                dic["과세구분"] = this.cbo과세구분.Text;
                dic["단가유형"] = this.cbo단가유형.Text;
                dic["부가세율"] = this.cur부가세율.Text;
                dic["계산서처리구분"] = !this.rdo일괄.Checked ? this.rdo건별.Text : this.rdo일괄.Text;
                dic["부가세여부"] = this.cbo부가세여부.Text;
                dic["비고"] = this.txt비고.Text;
                dic["사업자등록번호"] = str2;
                dic["사업자등록번호1"] = str3;
                dic["사업장명"] = str1;
                dic["비고2"] = this.txt비고2.Text;
                dic["운송방법"] = this.cbo운송방법.Text;
                dic["지불조건"] = this.cbo지불조건.Text;
                dic["지불조건상세"] = this.txt지불조건.Text;
                dic["가격조건"] = this.cbo가격조건.Text;
                dic["도착지"] = str5;
                dic["선적지"] = this.txt선적지.Text.Trim();
                dic["DC_RMK_TEXT"] = this.txt발주텍스트비고1.Text;
                dic["가격조건"] = D.GetString(this.cbo가격조건.Text);
                dic["선적조건"] = D.GetString(this.cbo선적조건.Text);
                dic["운임구분"] = D.GetString(this.cbo운임조건.Text);
                dic["DC_RMK_TEXT2"] = this.txt발주텍스트비고2.Text;
                dic["STND_PAY"] = this.cbo지급기준.Text;
                dic["지불조건일자"] = this.cur지불조건.Text;
                dic["원산지"] = this.cbo원산지.Text;
                dic["대행사"] = this.ctx대행사.CodeValue;
                dic["대행사명"] = this.ctx대행사.CodeName;
                decimal num1 = (decimal)this._flexD.DataTable.Compute("SUM(AM)", "NO_PO ='" + D.GetString(this._header.CurrentRow["NO_PO"]) + "'");
                decimal num2 = (decimal)this._flexD.DataTable.Compute("SUM(VAT)", "NO_PO ='" + D.GetString(this._header.CurrentRow["NO_PO"]) + "'");
                decimal num3 = (decimal)this._flexD.DataTable.Compute("SUM(AM_EX)", "NO_PO ='" + D.GetString(this._header.CurrentRow["NO_PO"]) + "'");
                decimal num4 = (decimal)this._flexD.DataTable.Compute("SUM(AM_PRE)", "NO_PO ='" + D.GetString(this._header.CurrentRow["NO_PO"]) + "'");
                decimal num5 = num1 + num2;
                dic["AM_H"] = D.GetString(num1);
                dic["AM_EX_H"] = D.GetString(num3);
                dic["H_VAT"] = D.GetString(num2);
                dic["HAP_H"] = D.GetString(num5);
                dic["AM_PRE_H"] = D.GetString(num4);
                dic["운송비용"] = D.GetString(this.cur운송비용.DecimalValue);
                dic["인도조건"] = D.GetString(this.txt인도조건.Text);
                dic["인도기한"] = D.GetString(this.txt인도기한.Text);
                dic["유효기일"] = D.GetString(this.txt유효기일.Text);
                dic["포장형태"] = D.GetString(this.cbo포장형태.Text);
                dic["공급자"] = D.GetString(this.ctx공급자.CodeName);
                dic["제조사"] = D.GetString(this.ctx제조사.CodeName);
                dic["검사정보"] = D.GetString(this.txt검사정보.Text);
                dic["필수서류"] = D.GetString(this.txt필수서류.Text);
                dic["COND_PAY_LDV"] = D.GetString(this.txt지불조건.Text);
                dic["발주연도"] = this.Date_convert_Text("YEAR", this.dtp발주일자.Text);
                dic["발주월"] = this.Date_convert_Text("MON", this.dtp발주일자.Text);
                dic["발주일"] = this.Date_convert_Text("DAY", this.dtp발주일자.Text);
                dic["NO_COUNT"] = str4;
                dic["NO_ORDER"] = this.txt오더번호.Text;
                dic["CD_TPPO"] = this.ctx발주형태.CodeValue;
                dic["공장명"] = D.GetString(ds.Tables[0].Rows[0]["NM_PLANT"]);
                if (Global.MainFrame.ServerKey.Contains("HDWIA2") || Global.MainFrame.ServerKey.Contains("HDWIA") || Global.MainFrame.ServerKey.Contains("DZSQL"))
                {
                    dic["부서장"] = D.GetString(ds.Tables[0].Rows[0]["NM_EMPMNG"]);
                    dic["부서장영문직위명"] = D.GetString(ds.Tables[0].Rows[0]["EN_DUTY_RANK"]);
                    DataTable dataTable = this._biz.search_partner(D.GetString(ds.Tables[0].Rows[0]["CD_AGENCY"]));
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        dic["대행사주소"] = D.GetString(dataTable.Rows[0]["DC_ADS1_H"]);
                        dic["대행사상세주소"] = D.GetString(dataTable.Rows[0]["DC_ADS1_D"]);
                        dic["대행사담당자"] = D.GetString(dataTable.Rows[0]["CD_EMP_PARTNER"]);
                        dic["대행사전화번호"] = D.GetString(dataTable.Rows[0]["NO_TEL"]);
                        dic["대행사팩스"] = D.GetString(dataTable.Rows[0]["NO_FAX"]);
                        dic["대행사이메일"] = D.GetString(dataTable.Rows[0]["E_MAIL"]);
                    }
                }
                if (Global.MainFrame.ServerKey.Contains("SINJINSM"))
                {
                    DataTable dataTable = this._biz.Check_EMP_SG(D.GetString(this.ctx담당자.CodeValue));
                    if (dataTable != null && dataTable.Rows.Count > 0)
                        dic["DC_RMK_EMP"] = D.GetString(dataTable.Rows[0]["DC_RMK"]);
                }
                if (Global.MainFrame.ServerKey.Contains("DEMAC"))
                {
                    DataTable docu = this._biz.GetDOCU(D.GetString(this.txt발주번호.Text));
                    if (docu != null && docu.Rows.Count > 0)
                    {
                        string[] strArray = new string[] { "진행",
                                                           "종결",
                                                           "미상신",
                                                           "취소" };
                        dic["APP_DT_END"] = D.GetString(docu.Rows[0]["APP_DT_END"]);
                        dic["ST_STAT"] = !(D.GetDecimal(docu.Rows[0]["ST_STAT"]) == -1M) ? strArray[D.GetInt(docu.Rows[0]["ST_STAT"])] : "반려";
                    }
                    else
                    {
                        dic["ST_STAT"] = "미상신";
                        dic["APP_DT_END"] = "";
                    }
                }
                foreach (string key in dic.Keys)
                    this.rptHelper.SetData(key, dic[key]);
                if (this.Tp_print == "WONIK")
                    this.rptHelper.SetDataTable(this._biz.DataSearch_GW_RPT_ONLY(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                this.txt발주번호.Text,
                                                                                                D.GetString(테이블구분.PU_POH.GetHashCode()),
                                                                                                this.dtp발주일자.Text,
                                                                                                "WONIK" }));
                else if (this.Tp_print == "SAMTECH")
                    this.rptHelper.SetDataTable(this._biz.DataSearch_GW_RPT_ONLY(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                this.txt발주번호.Text,
                                                                                                D.GetString(테이블구분.PU_POH.GetHashCode()),
                                                                                                this.dtp발주일자.Text,
                                                                                                this.Tp_print }));
                else if (Global.MainFrame.ServerKey.Contains("CNP"))
                    this.SetPrint차앤박();
                else if (Global.MainFrame.ServerKeyCommon == "DYPNF")
                {
                    DataSet dataSet = this._biz.Gw_DYPNF(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                        this.txt발주번호.Text });
                    this.rptHelper.SetDataTable(dataSet.Tables[0], 1);
                    this.rptHelper.SetDataTable(dataSet.Tables[1], 2);
                }
                else
                {
                    this.rptHelper.SetDataTable(ds.Tables[0], 1);
                    this.rptHelper.SetDataTable(ds.Tables[0], 2);
                    this.rptHelper.SetDataTable(ds.Tables[0], 3);
                    this.rptHelper.SetDataTable(ds.Tables[1], 4);
                    if (this.sPUSU == "100")
                        this.rptHelper.SetDataTable(this._biz.Print_Detail(this.txt발주번호.Text), 5);
                }
                if (Global.MainFrame.ServerKeyCommon == "YDGLS")
                    this.rptHelper.SetDataTable(this._biz.DataSearch_GW_RPT_ONLY(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                this.txt발주번호.Text,
                                                                                                D.GetString(테이블구분.PU_POH.GetHashCode()),
                                                                                                this.dtp발주일자.Text,
                                                                                                "YDGLS" }), 5);
                if (checkprint)
                    return;
                if (!checkprint && this.Tp_print == "ONESHOT")
                {
                    return;
                }
                else
                {
                    StringBuilder text = new StringBuilder();
                    string empty = string.Empty;
                    this.setMail(ref empty, ref text, ds);
                    string[] strArray1 = new string[] { empty,
                                                        Settings1.Default.MAIL_ADD,
                                                        text.ToString() };
                    if (D.GetString(this._flexD["NO_APP"]) != string.Empty)
                    {
                        foreach (DataRow row in this._biz.getMail_Adress(this._flexD.DataTable.DefaultView.ToTable(true, "NO_APP")).DefaultView.ToTable(true, "NO_EMP", "NM_KOR", "NO_EMAIL").Rows)
                        {
                            string[] strArray2;
                            string str6 = (strArray2 = strArray1)[1] + D.GetString(row["NM_KOR"]) + "|" + D.GetString(row["NO_EMAIL"]) + "|N?";
                            strArray2[1] = str6;
                        }
                    }
                    int num6;
                    if (D.GetString(this._flexD["NO_PR"]) != string.Empty)
                        num6 = !MA.ServerKey(false, new string[] { "ABLBIO" }) ? 1 : 0;
                    else
                        num6 = 1;
                    if (num6 == 0)
                    {
                        foreach (DataRow row in this._biz.getMail_Adress_ABLBIO(this._flexD.DataTable.DefaultView.ToTable(true, "NO_PR"), this.txt발주번호.Text).DefaultView.ToTable(true, "NO_EMP", "NM_KOR", "NO_EMAIL").Rows)
                        {
                            string[] strArray3;
                            string str7 = (strArray3 = strArray1)[1] + D.GetString(row["NM_KOR"]) + "|" + D.GetString(row["NO_EMAIL"]) + "|N?";
                            strArray3[1] = str7;
                        }
                    }
                    P_MF_EMAIL pMfEmail = new P_MF_EMAIL(new string[] { this.ctx거래처.CodeValue }, "R_PU_PO_REG2_0", new ReportHelper[] { this.rptHelper }, dic, "구매발주서", strArray1, this.txt발주번호.Text);
                    if (Global.MainFrame.ServerKeyCommon.Contains("JICO") || Global.MainFrame.ServerKeyCommon == "KINTEC")
                        pMfEmail.SetSendUser = "EMP_USER";
                    else if (Global.MainFrame.ServerKeyCommon.Contains("ABLBIO"))
                        pMfEmail.SetSendUser = "LOGIN_EMAIL";
                    else if (Global.MainFrame.ServerKeyCommon.Contains("ILSC") || Global.MainFrame.ServerKeyCommon == "DANSUK")
                    {
                        pMfEmail.SetSendUser = "LOGIN_EMAIL";
                        pMfEmail.SetSendEmpCode = this.ctx담당자.CodeValue;
                    }
                    ((Form)pMfEmail).ShowDialog();
                    Settings1.Default.MAIL_ADD = pMfEmail._str_rt_data[0];
                    Settings1.Default.MAIL_TEXT = pMfEmail.GetMailText;
                    Settings1.Default.Save();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this.ToolBarSaveButtonEnabled = false;
            }
        }

        private void setMail(ref string title, ref StringBuilder text, DataSet ds)
        {
            try
            {
                string empty = string.Empty;
                if (MA.ServerKey(false, new string[1] { "AXT" }))
                    title = "[아진엑스텍]발주서/" + D.GetString(this.txt발주번호.Text) + "/" + D.GetString(this.ctx거래처.CodeName);
                else if (Global.MainFrame.ServerKeyCommon == "ECIS")
                    title = "(주)이시스코스메틱/" + D.GetString(this.txt발주번호.Text) + "/구매발주가 등록되었습니다.";
                else if (Global.MainFrame.ServerKeyCommon == "ABLBIO")
                    title = Global.MainFrame.LoginInfo.CompanyName + "/" + D.GetString(this.txt발주번호.Text) + "/구매발주가 도착하였습니다.";
                else if (Global.MainFrame.ServerKeyCommon == "DANSUK")
                    title = "[" + Global.MainFrame.LoginInfo.CompanyName + "] " + D.GetString(this.txt발주번호.Text) + "/발주서 송부 건";
                else if (Global.MainFrame.ServerKeyCommon == "GIT")
                    title = D.GetString(this.ctx거래처.CodeName) + "/" + D.GetString(this.ctx담당자.CodeName) + "/" + D.GetString(this.txt발주번호.Text) + "/구매발주가 등록되었습니다.";
                else if (MA.ServerKey(false, new string[] { "AUTON" }))
                    title = "오토앤발주서(" + D.GetString(this.txt발주번호.Text) + ")_" + D.GetString(this.ctx거래처.CodeName);
                else
                    title = D.GetString(this.ctx담당자.CodeName) + "/" + D.GetString(this.txt발주번호.Text) + "/" + D.GetString(ds.Tables[0].Rows[0]["CD_PJT"]) + "구매발주가 등록되었습니다.";
                if (MA.ServerKey(false, new string[1] { "FRIXA" }))
                {
                    title = "";
                    string str = "첨부파일 참조";
                    text.AppendLine(str);
                }
                else if (Global.MainFrame.ServerKeyCommon == "ECIS" || Global.MainFrame.ServerKeyCommon == "KINTEC" || Global.MainFrame.ServerKeyCommon == "DANSUK")
                {
                    string str = "";
                    text.AppendLine(str);
                }
                else if (Global.MainFrame.ServerKeyCommon.Contains("SEMICS") || Global.MainFrame.ServerKeyCommon.Contains("MEERE"))
                    text.Append(D.GetString(Settings1.Default.MAIL_TEXT));
                else if (Global.MainFrame.ServerKeyCommon == "YHPLA")
                {
                    if (this._header.CurrentRow["YN_IMPORT"].ToString() == "Y")
                    {
                        title = "Yuhan P/O no." + this._header.CurrentRow["NO_PO"].ToString();
                        text.AppendLine("Dear Sir,");
                        text.AppendLine(Environment.NewLine);
                        text.AppendLine("Please find the new P/O sheet as attached herewith and inform us of the shipping schedule when it available.");
                        text.AppendLine("(P/O no.:" + this._header.CurrentRow["NO_PO"].ToString() + ")");
                        text.AppendLine("Please reply me for confirmation of P/O sheet receiving.");
                        text.AppendLine(Environment.NewLine);
                        text.AppendLine("Best regards");
                    }
                    else
                    {
                        title = "유한건강생활신규 발주 안내메일";
                        text.AppendLine("안녕하십니까.");
                        text.AppendLine("유한건강생활 구매 담당자 입니다.");
                        text.AppendLine(Environment.NewLine);
                        text.AppendLine("신규 발주서 첨부하였으니 확인하시고 회신 부탁드립니다.");
                        text.AppendLine("발주 승인 및 납품 입력 진행을 위해 아래 주소를 클릭하시기 바랍니다.");
                        text.AppendLine("감사합니다.");
                        text.AppendLine(Environment.NewLine);
                        text.AppendLine(BASIC.GetMAENV("MYBOXS_URL"));
                    }
                }
                else if (Global.MainFrame.ServerKeyCommon == "VCCD")
                {
                    title = "(주)브이씨 – 발주서 / " + D.GetString(this.txt발주번호.Text);
                    text.AppendLine("보낸회사 : (주)브이씨");
                    text.AppendLine("Email : jslim@vcinc.co.kr.");
                    text.AppendLine("연락처 : 010-2624-1831");
                    text.AppendLine(Environment.NewLine);
                    foreach (DataRow row in this._flexD.DataTable.Rows)
                    {
                        string str = "품목코드: " + D.GetString(row["CD_ITEM"]) + " / 품목명: " + D.GetString(row["NM_ITEM"]) + " / 규격: " + D.GetString(row["STND_ITEM"]) + " / 단위: " + D.GetString(row["UNIT_IM"]) + " / 수량: " + D.GetDecimal(row["QT_PO_MM"]).ToString("#,###,##0.####") + " / 단가: " + D.GetDecimal(row["UM_EX_PO"]).ToString("#,###,##0.####") + "/ 금액: " + D.GetDecimal(row["AM_EX"]).ToString("#,###,##0.####") + " / 환종: " + D.GetString(row["NM_EXCH"]);
                        text.AppendLine(str);
                        text.AppendLine(Environment.NewLine);
                    }
                }
                else if (Global.MainFrame.ServerKeyCommon == "ABLBIO")
                {
                    foreach (DataRow row in this._flexD.DataTable.Rows)
                    {
                        string str = "품목코드: " + D.GetString(row["CD_ITEM"]) + " / 품목명: " + D.GetString(row["NM_ITEM"]) + " / 규격: " + D.GetString(row["STND_ITEM"]) + " / 세부규격: " + D.GetString(row["STND_DETAIL_ITEM"]) + " / 재고단위: " + D.GetString(row["UNIT_IM"]) + " / 발주수량: " + D.GetDecimal(row["QT_PO_MM"]).ToString("#,###,##0.####") + " / 발주단가: " + D.GetDecimal(row["UM_P"]).ToString("#,###,##0.####") + "/ 발주금액(원화): " + D.GetDecimal(row["AM"]).ToString("#,###,##0.####");
                        text.AppendLine(str);
                        text.AppendLine(Environment.NewLine);
                    }
                }
                else if (Global.MainFrame.ServerKeyCommon == "DAEKHON")
                {
                    title = "대곤코퍼레이션 발주서 송부합니다(" + D.GetString(this.ctx거래처.CodeName) + ") - 납기회신 바랍니다.";
                    text.AppendLine("안녕하세요~ 대곤코퍼레이션 " + this.ctx담당자.CodeName + "입니다.");
                    text.AppendLine("당사 서울본사 발주서를 보내드리오니, 확인하시고 납품 부탁드립니다.");
                    text.AppendLine("발주서 수신 시 확정납기를 전체대상으로 e-mail 회신요청 드립니다.");
                    text.AppendLine("문의 사항 있으시면 하기 연락처로 연락주시기 바랍니다.");
                }
                else
                {
                    foreach (DataRow row in this._flexD.DataTable.Rows)
                    {
                        string str = "품목코드: " + D.GetString(row["CD_ITEM"]) + " / 품목명: " + D.GetString(row["NM_ITEM"]) + " / 규격: " + D.GetString(row["STND_ITEM"]) + " / 단위: " + D.GetString(row["UNIT_IM"]) + " / 수량: " + D.GetDecimal(row["QT_PO_MM"]).ToString("#,###,##0.####") + " / 단가: " + D.GetDecimal(row["UM_EX_PO"]).ToString("#,###,##0.####") + "/ 금액: " + D.GetDecimal(row["AM_EX"]).ToString("#,###,##0.####") + " / 프로젝트코드: " + D.GetString(row["CD_PJT"]) + " / 프로젝트명: " + D.GetString(row["NM_PJT"]);
                        text.AppendLine(str);
                        text.AppendLine(Environment.NewLine);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void SetPrint차앤박()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["회사명"] = D.GetString(this._header.CurrentRow["NM_COMPANY"]);
            dictionary["회사주소"] = D.GetString(this._header.CurrentRow["COM_ADS"]);
            dictionary["회사전화"] = D.GetString(this._header.CurrentRow["NO_TEL"]);
            dictionary["회사팩스"] = D.GetString(this._header.CurrentRow["NO_FAX"]);
            dictionary["문서번호"] = D.GetString(this._header.CurrentRow["NO_PO"]);
            dictionary["발주일자"] = D.GetString(this._header.CurrentRow["DT_PO"]);
            dictionary["발신"] = D.GetString(this._header.CurrentRow["NM_COMPANY"]);
            dictionary["발주담당"] = D.GetString(this._header.CurrentRow["PU_NM_KOR"]);
            dictionary["발주담당자연락처"] = D.GetString(this._header.CurrentRow["E_NO_TEL"]);
            dictionary["수신"] = D.GetString(this._header.CurrentRow["LN_PARTNER"]);
            dictionary["연락처"] = D.GetString(this._header.CurrentRow["P_NO_TEL"]);
            dictionary["팩스"] = D.GetString(this._header.CurrentRow["P_NO_FAX"]);
            dictionary["담당"] = D.GetString(this._header.CurrentRow["CD_EMP_PARTNER"]);
            dictionary["특이사항"] = D.GetString(this._header.CurrentRow["DC50_PO"]);
            foreach (string key in dictionary.Keys)
                this.rptHelper.SetData(key, dictionary[key]);
            this.rptHelper.SetDataTable(this._flexD.DataTable);
        }

        private void SetPrint필옵틱스()
        {
            string str = "001";
            if (this.ShowMessage("ASS'Y 별로 인쇄 하시겠습니까?", "QY2") == DialogResult.Yes)
                str = "002";
            DataSet dataSet = DBHelper.GetDataSet("UP_PU_Z_PHIL_PO_PRINT", new object[] { MA.Login.회사코드,
                                                                                          D.GetString(this._flexD["NO_APP"]),
                                                                                          this.txt발주번호.Text });
            if (dataSet.Tables[0] == null || dataSet.Tables[0].Rows.Count <= 0)
                return;
            DataRow row1 = dataSet.Tables[0].Rows[0];
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["품의번호"] = D.GetString(row1["NO_APP"]);
            dictionary["작성일자"] = D.GetString(row1["DT_WRITE"]);
            dictionary["담당부서"] = D.GetString(row1["NM_DEPT"]);
            dictionary["담당자"] = D.GetString(row1["APP_EMP"]);
            dictionary["지급조건"] = D.GetString(row1["NM_FG_PAYMENT"]);
            dictionary["가격조건"] = D.GetString(row1["NM_COND_PRICE"]);
            dictionary["발주처"] = D.GetString(row1["LN_PARTNER"]);
            dictionary["납기일"] = D.GetString(row1["DT_LIMIT"]);
            dictionary["프로젝트코드"] = D.GetString(row1["CD_PJT"]);
            dictionary["프로젝트명"] = D.GetString(row1["NM_PJT"]);
            dictionary["고객사명"] = D.GetString(row1["LN_SP_PARTNER"]);
            dictionary["창고"] = D.GetString(row1["NM_SL"]);
            dictionary["요청자"] = D.GetString(row1["PR_NM_KOR"]);
            dictionary["비고1"] = D.GetString(row1["DC_RMK"]);
            dictionary["비고2"] = D.GetString(row1["DC_RMK2"]);
            dictionary["지출시행일자"] = D.GetString(row1["DC_RMK3"]);
            dictionary["예금주"] = D.GetString(row1["NM_DEPOSIT"]);
            dictionary["계좌번호"] = D.GetString(row1["NO_DEPOSIT"]);
            dictionary["공급가액"] = D.GetString(row1["AM"]);
            dictionary["부가세"] = D.GetString(row1["VAT"]);
            dictionary["합계"] = D.GetString(row1["TOT_AM"]);
            dictionary["환종"] = D.GetString(row1["NM_EXCH"]);
            dictionary["발주일자"] = D.GetString(row1["DT_PO"]);
            dictionary["발주번호"] = D.GetString(row1["NO_PO"]);
            dictionary["발주담당자"] = D.GetString(row1["PO_NO_EMP"]);
            dictionary["발주담당자명"] = D.GetString(row1["PO_NM_KOR"]);
            dictionary["구매그룹전화번호"] = D.GetString(row1["PU_NO_TEL"]);
            dictionary["구매그룹팩스번호"] = D.GetString(row1["PU_NO_FAX"]);
            dictionary["발주형태코드"] = D.GetString(row1["CD_TPPO"]);
            dictionary["발주형태명"] = D.GetString(row1["NM_TPPO"]);
            dictionary["발주거래처코드"] = D.GetString(row1["PO_CD_PARTNER"]);
            dictionary["발주거래처명"] = D.GetString(row1["PO_LN_PARTNER"]);
            dictionary["발주납기일"] = D.GetString(row1["PO_DT_LIMIT"]);
            dictionary["발주창고코드"] = D.GetString(row1["PO_CD_SL"]);
            dictionary["발주창고명"] = D.GetString(row1["PO_NM_SL"]);
            dictionary["발주지급조건코드"] = D.GetString(row1["FG_TRANS"]);
            dictionary["발주지급조건명"] = D.GetString(row1["NM_FG_TRANS"]);
            dictionary["발주거래처담당자"] = D.GetString(row1["CD_EMP_PARTNER"]);
            dictionary["발주거래처핸드폰번호"] = D.GetString(row1["NO_HPEMP_PARTNER"]);
            dictionary["발주거래처전화번호"] = D.GetString(row1["PO_NO_TEL"]);
            dictionary["발주상단비고1"] = D.GetString(row1["PO_DC_RMK_TEXT"]);
            decimal num1 = 0M;
            decimal num2 = 0M;
            if (str == "001")
            {
                if (dataSet.Tables[1] != null && dataSet.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow row2 in dataSet.Tables[1].Rows)
                    {
                        num1 += D.GetDecimal(row2["QT_APP"]);
                        num2 += D.GetDecimal(row2["AM_EX"]);
                    }
                    this.rptHelper.SetDataTable(dataSet.Tables[1]);
                }
            }
            else if (str == "002" && dataSet.Tables[2] != null && dataSet.Tables[2].Rows.Count > 0)
            {
                foreach (DataRow row3 in dataSet.Tables[2].Rows)
                {
                    ++num1;
                    num2 += D.GetDecimal(row3["AM_EX"]);
                }
                this.rptHelper.SetDataTable(dataSet.Tables[2]);
            }
            dictionary["총수량"] = D.GetString(num1);
            dictionary["공급합계"] = D.GetString(num2);
            foreach (string key in dictionary.Keys)
                this.rptHelper.SetData(key, dictionary[key]);
        }

        private bool rptHelper_Printing(object sender, PrintArgs args)
        {
            try
            {
                if (args.Action == PrintActionEnum.ON_PREPARE_PRINT)
                {
                    this.Tp_print = !(args.scriptFile.ToUpper() == "R_PU_PO_REG2_001_WONIK.RDF") && !(args.scriptFile.ToUpper() == "R_PU_PO_REG2_001_WONIK_0.RDF") ? (!(args.scriptFile.ToUpper() == "R_PU_PO_REG2_002_SAMTECH.RDF") ? "RDF" : "SAMTECH") : "WONIK";
                    if (Global.MainFrame.ServerKey.Contains("PHILOPTICS"))
                        this.SetPrint필옵틱스();
                    else if (Global.MainFrame.ServerKey.Contains("CNP"))
                        this.SetPrint차앤박();
                    else
                        this.SetPrint(true);
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
                string str = "";
                if (fg_date == "YEAR")
                    str = dt_po.Substring(0, 4);
                else if (fg_date == "MON")
                {
                    switch (dt_po.Substring(4, 2))
                    {
                        case "01":
                            str = "January";
                            break;
                        case "02":
                            str = "February";
                            break;
                        case "03":
                            str = "March";
                            break;
                        case "04":
                            str = "April";
                            break;
                        case "05":
                            str = "May";
                            break;
                        case "06":
                            str = "June";
                            break;
                        case "07":
                            str = "July";
                            break;
                        case "08":
                            str = "August";
                            break;
                        case "09":
                            str = "September";
                            break;
                        case "10":
                            str = "October";
                            break;
                        case "11":
                            str = "November";
                            break;
                        case "12":
                            str = "December";
                            break;
                    }
                }
                else if (fg_date == "DAY")
                    str = dt_po.Substring(6, 2);
                return str;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
                return null;
            }
        }

        public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
        {
            try
            {
                Settings1.Default.CD_PURGRP_SET = this._header.CurrentRow["CD_PURGRP"].ToString();
                Settings1.Default.CD_TPPO_SET = this._header.CurrentRow["CD_TPPO"].ToString();
                Settings1.Default.FG_PAYMENT_SET = this._header.CurrentRow["FG_PAYMENT"].ToString();
                Settings1.Default.CD_EXCH = this._header.CurrentRow["CD_EXCH"].ToString();
                Settings1.Default.RT_EXCH = D.GetDecimal(this._header.CurrentRow["RT_EXCH"]);
                Settings1.Default.DC_RMK_TEXT = D.GetString(this.txt발주텍스트비고1.Text);
                Settings1.Default.DC_RMK_TEXT2 = D.GetString(this.txt발주텍스트비고2.Text);
                Settings1.Default.TP_UM_TAX = this._header.CurrentRow["TP_UM_TAX"].ToString();
                Settings1.Default.Save();
                return base.OnToolBarExitButtonClicked(sender, e);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
                return false;
            }
        }

        private void OnBpControl_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                if (sender is BpCodeTextBox)
                {
                    string empty = string.Empty;
                    switch (((Control)sender).Name)
                    {
                        case "ctx담당자":
                            this._header.CurrentRow["CD_DEPT"] = e.HelpReturn.Rows[0]["CD_DEPT"];
                            this._header.CurrentRow["NM_DEPT"] = e.HelpReturn.Rows[0]["NM_DEPT"];
                            this.txt담당부서.Text = this._header.CurrentRow["NM_DEPT"].ToString();
                            break;
                        case "ctx구매그룹":
                            if (e.CodeValue != null)
                            {
                                this._header.CurrentRow["PURGRP_NO_TEL"] = e.HelpReturn.Rows[0]["NO_TEL"];
                                this._header.CurrentRow["PURGRP_NO_FAX"] = e.HelpReturn.Rows[0]["NO_FAX"];
                                this._header.CurrentRow["PURGRP_E_MAIL"] = e.HelpReturn.Rows[0]["E_MAIL"];
                                this._header.CurrentRow["PO_PRICE"] = "N";
                                string arg_cd_purgrp = e.HelpReturn.Rows[0]["CD_PURGRP"].ToString();
                                DataTable dataTable = Global.MainFrame.FillDataTable(" SELECT O.PO_PRICE    FROM MA_PURGRP G LEFT OUTER JOIN MA_PURORG O      ON   G.CD_COMPANY = O.CD_COMPANY     AND   G.CD_PURORG  = O.CD_PURORG  WHERE G.CD_COMPANY = '" + this.LoginInfo.CompanyCode + "'  AND G.CD_PURGRP  = '" + arg_cd_purgrp + "'");
                                if (dataTable.Rows.Count > 0 && dataTable.Rows[0]["PO_PRICE"] != DBNull.Value && dataTable.Rows[0]["PO_PRICE"].ToString().Trim() != string.Empty)
                                    this._header.CurrentRow["PO_PRICE"] = dataTable.Rows[0]["PO_PRICE"].ToString().Trim();
                                this.SetCC(0, arg_cd_purgrp);
                                break;
                            }
                            this.ctx구매그룹.CodeValue = "";
                            this.ctx구매그룹.CodeName = "";
                            this._header.CurrentRow["PURGRP_NO_TEL"] = "";
                            this._header.CurrentRow["PURGRP_NO_FAX"] = "";
                            this._header.CurrentRow["PURGRP_E_MAIL"] = "";
                            this._header.CurrentRow["PO_PRICE"] = "N";
                            this._header.CurrentRow["CD_CC_PURGRP"] = "";
                            this._header.CurrentRow["NM_CC_PURGRP"] = "";
                            break;
                        case "txt_NoProject":
                            DataRow[] dataRowArray = this._flexD.DataTable.Select();
                            if (this._flexD.HasNormalRow)
                            {
                                foreach (DataRow dataRow in dataRowArray)
                                {
                                    dataRow["CD_PJT"] = this.ctx프로젝트.CodeValue;
                                    dataRow["NM_PJT"] = this.ctx프로젝트.CodeName;
                                }
                                break;
                            }
                            break;
                        case "ctx프로젝트":
                            if (Config.MA_ENV.YN_UNIT == "Y" || BASIC.GetMAEXC("구매-프로젝트도움창 UNIT사용여부") == "100" && Config.MA_ENV.프로젝트사용)
                            {
                                this.d_SEQ_PROJECT = D.GetDecimal(e.HelpReturn.Rows[0]["SEQ_PROJECT"]);
                                this.s_CD_PJT_ITEM = D.GetString(e.HelpReturn.Rows[0]["CD_PJT_ITEM"]);
                                this.s_NM_PJT_ITEM = D.GetString(e.HelpReturn.Rows[0]["NM_PJT_ITEM"]);
                                this.s_PJT_ITEM_STND = D.GetString(e.HelpReturn.Rows[0]["PJT_ITEM_STND"]);
                            }
                            if (Global.MainFrame.ServerKeyCommon == "UNIPOINT")
                            {
                                DataTable projectDetail = this._biz.Get_Project_Detail(D.GetString(e.HelpReturn.Rows[0]["NO_PROJECT"]));
                                if (projectDetail != null || projectDetail.Rows.Count > 0)
                                {
                                    this.s_CD_PARTNER_PJT = D.GetString(projectDetail.Rows[0]["CD_PARTNER"]);
                                    this.s_NM_PARTNER_PJT = D.GetString(projectDetail.Rows[0]["LN_PARTNER"]);
                                    this.s_NO_EMP_PJT = D.GetString(projectDetail.Rows[0]["NO_EMP"]);
                                    this.s_NM_EMP_PJT = D.GetString(projectDetail.Rows[0]["NM_KOR"]);
                                    this.s_END_USER = D.GetString(projectDetail.Rows[0]["END_USER"]);
                                }
                                break;
                            }
                            break;
                        case "ctx도착지":
                            this.txt도착지.Text = D.GetString(e.HelpReturn.Rows[0]["NM_SYSDEF"]);
                            break;
                        case "ctx선적지":
                            this.txt선적지.Text = D.GetString(e.HelpReturn.Rows[0]["NM_SYSDEF"]);
                            break;
                        case "ctx거래처":
                            if ((this.MainFrameInterface.ServerKeyCommon == "KPCI" || Global.MainFrame.ServerKeyCommon == "SQL_") && this.m_tab_poh.TabPages.Contains(this.tabPage7))
                            {
                                DataTable dataTable = this._biz.search_partner(D.GetString(e.HelpReturn.Rows[0]["CD_PARTNER"]));
                                if (dataTable != null && dataTable.Rows.Count > 0 && D.GetString(dataTable.Rows[0]["FG_PAYBILL"]) != string.Empty)
                                    this.cbo지급구분.SelectedValue = D.GetString(dataTable.Rows[0]["FG_PAYBILL"]);
                            }
                            if (Global.MainFrame.ServerKeyCommon.Contains("WINFOOD"))
                                this.cur업체전용3.DecimalValue = this._biz.AM_IN(D.GetString(e.HelpReturn.Rows[0]["CD_PARTNER"]), this.dtp발주일자.Text);
                            if (this.s_PTR_SUB == "001")
                            {
                                DataTable maPartnerSub = this._biz.get_MA_PARTNER_SUB(this.ctx거래처.CodeValue);
                                if (maPartnerSub != null && maPartnerSub.Rows.Count > 0)
                                {
                                    this.SetTppoAfter(D.GetString(maPartnerSub.Rows[0]["CD_TPPO"]), D.GetString(maPartnerSub.Rows[0]["NM_TPPO"]));
                                    this.SetPurgrpAfter(D.GetString(maPartnerSub.Rows[0]["CD_PURGRP"]), D.GetString(maPartnerSub.Rows[0]["NM_PURGRP"]));
                                }
                            }
                            if (Global.MainFrame.ServerKeyCommon.Contains("FOODKOR"))
                            {
                                DataTable gipartner = this._biz.Get_Gipartner(this.ctx거래처.CodeValue);
                                if (gipartner != null && gipartner.Rows.Count > 0 && D.GetString(gipartner.Rows[0]["CD_FLAG1"]) == "Y")
                                {
                                    this.ShowMessage("발주등록이 불가능한 거래처입니다.");
                                    this.ctx거래처.CodeValue = "";
                                    this.ctx거래처.CodeName = "";
                                    this._header.CurrentRow["CD_PARTNER"] = "";
                                }
                            }
                            int num1;
                            if (this._m_partner_use == "100")
                                num1 = !MA.ServerKey(false, new string[] { "KCF" }) ? 1 : 0;
                            else
                                num1 = 1;
                            if (num1 == 0)
                                this.set_UM_ITEM_KCF(D.GetString(e.HelpReturn.Rows[0]["CD_PARTNER"]));
                            int num2;
                            if (this.m_tab_poh.TabPages.Contains(this.tabPage7))
                                num2 = !MA.ServerKey(false, new string[] { "HOTEL" }) ? 1 : 0;
                            else
                                num2 = 1;
                            if (num2 == 0)
                            {
                                this.set_IV_DT_HOTEL(D.GetString(e.HelpReturn.Rows[0]["CD_PARTNER"]));
                                break;
                            }
                            break;
                    }
                }
                if (sender is BpCodeNTextBox)
                {
                    switch (((Control)sender).Name)
                    {
                        case "ctx발주형태":
                            DataTable ynSu = this._biz.GetYN_SU(D.GetString(e.HelpReturn.Rows[0]["CD_TPPO"]));
                            if (D.GetString(ynSu.Rows[0]["YN_SU"]) == "Y")
                            {
                                if (Global.MainFrame.ServerKeyCommon.Contains("DONGWOON"))
                                    this.btnCompanyUse1.Enabled = true;
                                this._flexDD.Enabled = true;
                            }
                            else
                            {
                                this._flexDD.Enabled = false;
                                if (Global.MainFrame.ServerKeyCommon.Contains("DONGWOON"))
                                    this.btnCompanyUse1.Enabled = false;
                            }
                            if (this.sPUSU == "100" && D.GetString(ynSu.Rows[0]["YN_SU"]) == "Y" && D.GetString(e.HelpReturn.Rows[0]["FG_TRANS"]) != "004" && D.GetString(e.HelpReturn.Rows[0]["FG_TRANS"]) != "005")
                            {
                                this.ShowMessage(this.DD("국내인 외주발주가 있습니다. 발주유형을 확인하세요."));
                                this.ctx발주형태.CodeValue = "";
                                this.ctx발주형태.CodeName = "";
                                this._header.CurrentRow["CD_TPPO"] = "";
                                break;
                            }
                            this._header.CurrentRow["FG_TRANS"] = e.HelpReturn.Rows[0]["FG_TRANS"];
                            this._header.CurrentRow["FG_TPRCV"] = e.HelpReturn.Rows[0]["FG_TPRCV"];
                            this._header.CurrentRow["FG_TPPURCHASE"] = e.HelpReturn.Rows[0]["FG_TPPURCHASE"];
                            this._header.CurrentRow["YN_AUTORCV"] = e.HelpReturn.Rows[0]["YN_AUTORCV"];
                            this._header.CurrentRow["YN_RCV"] = e.HelpReturn.Rows[0]["YN_RCV"];
                            this._header.CurrentRow["YN_RETURN"] = e.HelpReturn.Rows[0]["YN_RETURN"];
                            this._header.CurrentRow["YN_SUBCON"] = e.HelpReturn.Rows[0]["YN_SUBCON"];
                            this._header.CurrentRow["YN_IMPORT"] = e.HelpReturn.Rows[0]["YN_IMPORT"];
                            this._header.CurrentRow["YN_ORDER"] = e.HelpReturn.Rows[0]["YN_ORDER"];
                            this._header.CurrentRow["YN_REQ"] = e.HelpReturn.Rows[0]["YN_REQ"];
                            this._header.CurrentRow["YN_AM"] = e.HelpReturn.Rows[0]["YN_AM"];
                            this._header.CurrentRow["NM_TRANS"] = e.HelpReturn.Rows[0]["NM_TRANS"];
                            this._header.CurrentRow["FG_TAX"] = e.HelpReturn.Rows[0]["FG_TAX"];
                            this._header.CurrentRow["TP_GR"] = e.HelpReturn.Rows[0]["TP_GR"];
                            this._header.CurrentRow["CD_CC_TPPO"] = e.HelpReturn.Rows[0]["CD_CC"];
                            this._header.CurrentRow["NM_CC_TPPO"] = this._biz.GetCCCodeSearch(e.HelpReturn.Rows[0]["CD_CC"].ToString());
                            this.거래구분(e.HelpReturn.Rows[0]["FG_TRANS"].ToString(), D.GetString(e.HelpReturn.Rows[0]["FG_TAX"]));
                            this.Setting_pu_poh_sub();
                            if (this.m_tab_poh.TabPages.Contains(this.tabPage7))
                            {
                                this.dtp만기일자.Text = Global.MainFrame.GetStringToday;
                                this.dtp지급예정일자.Text = Global.MainFrame.GetStringToday;
                                this.dtp매입일자.Text = Global.MainFrame.GetStringToday;
                                this.cbo지급구분.SelectedValue = "";
                                this.cbo전표유형.SelectedValue = "";
                                if (MA.ServerKey(false, new string[] { "HOTEL" }))
                                    this.set_IV_DT_HOTEL(D.GetString(this._header.CurrentRow["CD_PARTNER"]));
                            }
                            if (MA.ServerKey(false, new string[] { "HISF" }))
                                this.setCD_EXCH_HISF(D.GetString(e.HelpReturn.Rows[0]["CD_TPPO"]));
                            this.FillPol();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Control_CodeChanged(object sender, EventArgs e)
        {
            try
            {
                switch (((Control)sender).Name)
                {
                    case "ctx담당자":
                        if (!(this.ctx담당자.CodeValue == string.Empty))
                            break;
                        this._header.CurrentRow["CD_DEPT"] = "";
                        this._header.CurrentRow["NM_DEPT"] = "";
                        this.txt담당부서.Text = "";
                        break;
                    case "ctx구매그룹":
                        if (!(this.ctx구매그룹.CodeValue == string.Empty))
                            break;
                        this._header.CurrentRow["PURGRP_NO_TEL"] = "";
                        this._header.CurrentRow["PURGRP_NO_FAX"] = "";
                        this._header.CurrentRow["PURGRP_E_MAIL"] = "";
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void OnBpControl_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                switch (D.GetString(e.ControlName))
                {
                    case "ctx발주형태":
                        e.HelpParam.P61_CODE1 = "N";
                        if (this._반품발주 == "Y")
                        {
                            e.HelpParam.P41_CD_FIELD1 = "Y";
                            break;
                        }
                        break;
                    case "ctx도착지":
                        e.HelpParam.P41_CD_FIELD1 = "PU_C000046";
                        break;
                    case "ctx선적지":
                        e.HelpParam.P41_CD_FIELD1 = "PU_C000047";
                        break;
                    case "ctx창고":
                        if (D.GetString(this.cbo공장.SelectedValue) == string.Empty)
                        {
                            this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD("공장") });
                            break;
                        }
                        e.HelpParam.P09_CD_PLANT = D.GetString(this.cbo공장.SelectedValue);
                        break;
                    case "ctx프로젝트":
                        e.HelpParam.P41_CD_FIELD1 = "프로젝트";
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void 추가_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.btn품목추가.Enabled)
                    return;
                decimal num1;
                if (this.tabControlExt1.SelectedTab.Name == "tabPage5")
                {
                    if (!this.전자결재여부)
                    {
                        this.ShowMessage("전자결제 진행된 발주건은 추가가 불가능 합니다.");
                    }
                    else if (!this.차수여부)
                    {
                        this.ShowMessage("변경된 차수이므로 추가가 불가능 합니다.");
                    }
                    else
                    {
                        this.호출여부 = true;
                        if (!this.HeaderCheck(0))
                            return;
                        if (MA.ServerKey(true, new string[] { "ETNERS" }) && D.GetString(this.ctx프로젝트.CodeValue) == string.Empty)
                        {
                            this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl프로젝트.Text });
                            this.ctx프로젝트.Focus();
                        }
                        else
                        {
                            if (this._flexD.DataTable == null)
                                return;
                            this.ControlButtonEnabledDisable((Control)sender, true);
                            this.btn품목추가.Enabled = false;
                            this.cbo공장.Enabled = false;
                            decimal num5 = this._flexD.GetMaxValue("NO_LINE") + 1;
                            this._flexD.Rows.Add();
                            this._flexD.Row = this._flexD.Rows.Count - 1;
                            if (this.txt발주번호.Text != string.Empty)
                                this._flexD["NO_PO"] = this.txt발주번호.Text;
                            this._flexD["NO_LINE"] = num5;
                            this._flexD["CD_PLANT"] = this.cbo공장.SelectedValue.ToString();
                            this._flexD["CD_PJT"] = this.ctx프로젝트.CodeValue;
                            this._flexD["NM_PJT"] = this.ctx프로젝트.CodeName;
                            this._flexD["NO_PR"] = "";
                            this._flexD["CD_EXCH"] = this.cbo환정보.SelectedValue.ToString();
                            this._flexD["NM_SYSDEF"] = this._biz.GetGubunCodeSearch("PU_C000009", this._flexD["FG_POST"].ToString());
                            if (this.bStandard && Global.MainFrame.ServerKey == "SINJINSM")
                                this._flexD["RATE_VAT"] = this.cur부가세율.DecimalValue;
                            if (Global.MainFrame.CurrentPageID == "P_PU_Z_JONGHAP_PO_REG2")
                            {
                                this._flexD["RATE_VAT"] = this.cur부가세율.DecimalValue;
                                this._flexD["CD_UNIT_MM"] = "EA";
                                this._flexD["UNIT_IM"] = "KG";
                            }
                            foreach (DataRow row in ((DataTable)this.cbo환정보.DataSource).Rows)
                            {
                                if (row["CODE"].ToString() == this.cbo환정보.SelectedValue.ToString())
                                {
                                    this._flexD["NM_EXCH"] = row["NAME"].ToString();
                                    break;
                                }
                            }
                            if (this.dtp납기일.Text == string.Empty)
                            {
                                if (this._flexD.Row != this._flexD.Rows.Fixed)
                                    this._flexD["DT_LIMIT"] = this._flexD[this._flexD.Row - 1, "DT_LIMIT"];
                            }
                            else
                                this._flexD["DT_LIMIT"] = this.dtp납기일.Text;
                            if (MA.ServerKey(false, new string[] { "KCF",
                                                                   "VINA" }))
                            {
                                this._flexD["GI_PARTNER"] = D.GetString(this.ctx거래처.CodeValue);
                                this._flexD["LN_PARTNER"] = D.GetString(this.ctx거래처.CodeName);
                            }
                            if (MA.ServerKey(false, new string[] { "HOTEL" }))
                                this.CalcItemLT(this._flexD.Row);
                            else if (Global.MainFrame.ServerKeyCommon == "CUREXO")
                            {
                                this._flexD["CD_SL"] = this.ctx창고.CodeValue;
                                this._flexD["NM_SL"] = this.ctx창고.CodeName;
                            }
                            this.FillPol(this._flexD.Row);
                            this._flexD.AddFinished();
                            this._flexD.Col = this._flexD.Cols.Fixed;
                            this._flexD.Focus();
                            this.SetHeadControlEnabled(false, 1);
                        }
                    }
                }
                else if (this.tabControlExt1.SelectedTab.Name == "tabPage6")
                {
                    if (!this._flexD.HasNormalRow)
                        return;
                    if (this._flexD["CD_ITEM"].ToString() == "")
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD("사급자재의 상위품목정보") });
                    }
                    if (D.GetString(this._flexD["FG_POST"]) != "O" && this._flexD.RowState() != DataRowState.Added)
                    {
                        this.ShowMessage("이미 확정되었거나 종결처리된 품목에는 추가 할 수 없습니다.");
                    }
                    else
                    {
                        decimal maxValue = this._flexDD.GetMaxValue("NO_LINE");
                        this._flexDD.Rows.Add();
                        this._flexDD.Row = (this._flexDD.Rows).Count - 1;
                        this._flexDD["CD_PLANT"] = this._flexD["CD_PLANT"];
                        this._flexDD["NO_PO"] = this._flexD["NO_PO"];
                        this._flexDD["NO_POLINE"] = this._flexD["NO_LINE"];
                        this._flexDD["NO_LINE"] = num1 = ++maxValue;
                        this._flexDD["QT_NEED_UNIT"] = 1.0;
                        this._flexDD["QT_NEED"] = 1.0;
                        this._flexDD["QT_LOSS"] = 1.0;
                        this._flexDD.AddFinished();
                        this._flexDD.Col = this._flexD.Cols.Fixed;
                    }
                }
                else if (this.tabControlExt1.SelectedTab.Name == "tabPage9" && this.HeaderCheck(0) && this._flexIV.DataTable != null)
                {
                    if (this._flexD.DataTable.Rows.Count == 0)
                    {
                        this.ShowMessage(공통메세지._이가존재하지않습니다, new string[] { "발주품목" });
                    }
                    else if (!this.차수여부)
                    {
                        this.ShowMessage("변경된 차수이므로 추가가 불가능 합니다.");
                    }
                    else
                    {
                        decimal maxValue = this._flexIV.GetMaxValue("NO_SEQ");
                        this._flexIV.Rows.Add();
                        this._flexIV.Row = this._flexIV.Rows.Count - 1;
                        this._flexIV["NO_PO"] = this._flexD["NO_PO"];
                        if (this.sPUIV == "100")
                            this._flexIV["NO_POLINE"] = 0;
                        else if (this.sPUIV == "200")
                            this._flexIV["NO_POLINE"] = this._flexD["NO_LINE"];
                        this._flexIV["NO_SEQ"] = num1 = ++maxValue;
                        this._flexIV["DT_PUR_PLAN"] = Global.MainFrame.GetStringToday;
                        this._flexIV["CD_PJT"] = D.GetString(this._flexD["CD_PJT"]);
                        this._flexIV["SEQ_PROJECT"] = D.GetDecimal(this._flexD["SEQ_PROJECT"]);
                        this._flexIV.AddFinished();
                        this._flexIV.Col = this._flexIV.Cols.Fixed;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this.btn품목추가.Enabled = true;
            }
        }

        private void 삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.tabControlExt1.SelectedTab.Name == "tabPage5")
                {
                    DataRow[] dataRowArray = this._flexD.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);
                    if (dataRowArray == null || dataRowArray.Length <= 0)
                    {
                        this.ShowMessage(공통메세지.선택된자료가없습니다, new string[0]);
                    }
                    else if (!this._flexD.HasNormalRow)
                    {
                        this.ctx프로젝트.Enabled = true;
                        this.btnPJT적용.Enabled = true;
                        this.ctx창고.Enabled = true;
                        this.btn창고적용.Enabled = true;
                    }
                    else if (!this.전자결재여부)
                    {
                        this.ShowMessage("전자결제 진행된 발주건은 삭제가 불가능 합니다.");
                    }
                    else if (!this.차수여부)
                    {
                        this.ShowMessage("변경된 차수이므로 삭제가 불가능 합니다.");
                    }
                    else if (Global.MainFrame.ServerKeyCommon.Contains("KAHP") && D.GetString(this._header.CurrentRow["TXT_USERDEF4"]) != "")
                    {
                        this.ShowMessage("전표 처리여부를 확인 하십시오.");
                    }
                    else
                    {
                        this._flexD.Redraw = false;
                        for (int index = this._flexD.Rows.Count - 1; index >= this._flexD.Rows.Fixed; --index)
                        {
                            if (this._flexD[index, "S"].ToString() == "Y")
                            {
                                if (D.GetString(this._flexD[index, "YN_ORDER"]) == "Y" && D.GetString(this._flexD[index, "FG_POST"]) == "R" && Global.MainFrame.ServerKey != "HANSU")
                                {
                                    if (this.ShowMessage("발주상태가 확정입니다. 삭제하시겠습니까?", "QY2") != DialogResult.Yes)
                                        return;
                                    if (this.sPUSU == "100")
                                    {
                                        DataTable dataTable = this._flexDD.DataTable;
                                        string filterExpression = "NO_PO ='" + D.GetString(this._flexD[index, "NO_PO"]) + "' AND NO_POLINE='" + D.GetDecimal(this._flexD[index, "NO_LINE"]) + "'";
                                        foreach (DataRow dataRow in dataTable.Select(filterExpression))
                                            dataRow.Delete();
                                    }
                                  this._flexD.Rows.Remove(index);
                                }
                                else if (this._flexD[index, "FG_POST"].ToString() == "R")
                                {
                                    this.ShowMessage("발주상태가 미정일 경우에만 삭제가능합니다");
                                }
                                else
                                {
                                    if (this.sPUSU == "100")
                                    {
                                        DataTable dataTable = this._flexDD.DataTable;
                                        string filterExpression = "ISNULL(NO_PO,'') ='" + D.GetString(this._flexD[index, "NO_PO"]) + "' AND NO_POLINE='" + D.GetDecimal(this._flexD[index, "NO_LINE"]) + "'";
                                        foreach (DataRow dataRow in dataTable.Select(filterExpression))
                                            dataRow.Delete();
                                    }
                                  this._flexD.Rows.Remove(index);
                                }
                            }
                        }
                      this._flexD.Redraw = true;
                        if (this.m_tab_poh.TabPages.Contains(this.tabPage7))
                            this.SUMFunction();
                    }
                }
                else if (this.tabControlExt1.SelectedTab.Name == "tabPage6")
                {
                    if (!this._flexDD.HasNormalRow)
                        return;
                    if (D.GetString(this._flexD["FG_POST"]) != "O" && this._flexD.RowState() != DataRowState.Added)
                    {
                        this.ShowMessage("발주상태가 미정일 경우에만 삭제가능합니다");
                    }
                    else
                    {
                        DataRow[] dataRowArray = this._flexDD.DataTable.Select("CHK = 'Y'", "", DataViewRowState.CurrentRows);
                        if (dataRowArray == null || dataRowArray.Length == 0)
                        {
                            this.ShowMessage(공통메세지.선택된자료가없습니다, new string[0]);
                        }
                        else
                        {
                            this._flexDD.Redraw = false;
                            foreach (DataRow dataRow in dataRowArray)
                                dataRow.Delete();
                            this._flexDD.Redraw = true;
                            this._flexDD.IsDataChanged = true;
                            this.Page_DataChanged(null, null);
                        }
                    }
                }
                else if (this.tabControlExt1.SelectedTab.Name == "tabPage9")
                {
                    if (!this.차수여부)
                    {
                        this.ShowMessage("변경된 차수이므로 삭제가 불가능 합니다.");
                    }
                    else if (D.GetDecimal(this._flexIV["AM_PUL"]) > 0M)
                    {
                        this.ShowMessage("확정매입건이므로 삭제 할 수 없습니다.");
                    }
                    else
                        this._flexIV.RemoveItem(this._flexIV.Row);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
                this._flexD.Redraw = true;
            }
            finally
            {
                if (!this._flexD.HasNormalRow)
                {
                    this.cbo공장.Enabled = true;
                    this.ControlButtonEnabledDisable(null, true);
                    this.SetHeadControlEnabled(true, 1);
                    this.btn품목추가.Enabled = true;
                    this.btn요청적용.Enabled = true;
                }
                if (this.txt발주번호.Text != "")
                {
                    this.cbo공장.Enabled = false;
                    this.ctx거래처.Enabled = false;
                    this.ctx발주형태.Enabled = false;
                    this.cbo부가세여부.Enabled = false;
                    this.cbo환정보.Enabled = false;
                }
              this._flexD.Redraw = true;
            }
        }

        private void 품목전개_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.HeaderCheck(0) || this._flexD.DataTable == null)
                    return;
                if (this.cbo공장.SelectedValue.ToString() == "")
                {
                    this.ShowMessage("공장을먼저선택하십시오");
                    this.cbo공장.Focus();
                }
                else if (Global.MainFrame.ServerKeyCommon == "DEMAC")
                {
                    P_PU_UM_HISTORY_SUB pPuUmHistorySub = new P_PU_UM_HISTORY_SUB(new object[] { this.ctx거래처.CodeValue,
                                                                                                 this.ctx거래처.CodeName,
                                                                                                 this.ctx발주형태.CodeValue,
                                                                                                 this.ctx발주형태.CodeName,
                                                                                                 this.cbo공장.SelectedValue,
                                                                                                 "",
                                                                                                 "",
                                                                                                 "000",
                                                                                                 "",
                                                                                                 "",
                                                                                                 "",
                                                                                                 "",
                                                                                                 "",
                                                                                                 "" });
                    if (((Form)pPuUmHistorySub).ShowDialog() == DialogResult.OK)
                    {
                        this.SETDEMAC(pPuUmHistorySub.gdt_return);
                        this.SetHeadControlEnabled(false, 1);
                        this.ControlButtonEnabledDisable((Control)sender, true);
                    }
                }
                else
                {
                    string fgUm = Global.MainFrame.ServerKeyCommon.Contains("OGELEC") ? "" : D.GetString(this.cbo단가유형.SelectedValue);
                    P_PU_PO_ITEMEXPSUB pPuPoItemexpsub = new P_PU_PO_ITEMEXPSUB(this.MainFrameInterface, this.cbo공장.SelectedValue.ToString(), this.ctx거래처.CodeValue, this.ctx거래처.CodeName, fgUm, this.dtp발주일자.Text, this.dtp발주일자.Text, this.cbo환정보.SelectedValue.ToString());
                    if (((Form)pPuPoItemexpsub).ShowDialog(this) == DialogResult.OK)
                    {
                        this._flexD.Redraw = false;
                        if (pPuPoItemexpsub.gdt_return.Rows.Count > 0)
                        {
                            this.SetITEMEXP(pPuPoItemexpsub.gdt_return);
                            this.SetHeadControlEnabled(false, 1);
                            this.ControlButtonEnabledDisable((Control)sender, true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this._flexD.Redraw = true;
            }
        }

        private void SETDEMAC(DataTable dt)
        {
            try
            {
                if (dt == null || dt.Rows.Count <= 0)
                    return;
                decimal maxValue = this._flexD.GetMaxValue("NO_LINE");
                for (int index = 0; index < dt.Rows.Count; ++index)
                {
                    if (dt.Rows[index].RowState != DataRowState.Deleted)
                    {
                        this._flexD.Rows.Add();
                        this._flexD.Row = this._flexD.Rows.Count - 1;
                        if (this.txt발주번호.Text != string.Empty)
                            this._flexD["NO_PO"] = this.txt발주번호.Text;
                        this._flexD["DT_LIMIT"] = Global.MainFrame.GetStringToday;
                        this._flexD["DT_PLAN"] = this._flexD["DT_LIMIT"];
                        ++maxValue;
                        this._flexD["NO_LINE"] = maxValue;
                        this._flexD["CD_SL"] = "";
                        this._flexD["NO_CONTRACT"] = "";
                        this._flexD["NO_RCV"] = "";
                        this._flexD["NO_PR"] = "";
                        this._flexD["NO_APP"] = "";
                        this._flexD["NO_CONTRACT"] = "";
                        this._flexD["CD_PJT"] = this.ctx프로젝트.CodeValue;
                        this._flexD["NM_PJT"] = this.ctx프로젝트.CodeName;
                        this._flexD["CD_PLANT"] = D.GetString(this.cbo공장.SelectedValue);
                        this._flexD["CD_ITEM"] = dt.Rows[index]["CD_ITEM"];
                        this._flexD["NM_ITEM"] = dt.Rows[index]["NM_ITEM"];
                        this._flexD["CD_UNIT_MM"] = dt.Rows[index]["CD_UNIT_MM"];
                        this._flexD["STND_ITEM"] = dt.Rows[index]["STND_ITEM"];
                        this._flexD["UNIT_IM"] = dt.Rows[index]["UNIT_IM"];
                        this._flexD["NM_SYSDEF"] = this._biz.GetGubunCodeSearch("PU_C000009", this._flexD["FG_POST"].ToString());
                        this._flexD["NM_CLS_ITEM"] = dt.Rows[index]["NM_CLS_ITEM"];
                        this.FillPol(this._flexD.Row);
                        decimal num;
                        if (dt.Rows[index]["RATE_EXCHG"] == null)
                        {
                            num = 1M;
                        }
                        else
                        {
                            num = this._flexD.CDecimal(dt.Rows[index]["RATE_EXCHG"]);
                            if (num == 0M)
                                num = 1M;
                        }
                        this._flexD["RT_PO"] = num;
                        this._flexD["UM_EX_PO"] = this._flexD.CDecimal(dt.Rows[index]["UM_EX_PSO"]);
                        this._flexD["UM_EX"] = this._flexD.CDecimal(dt.Rows[index]["UM_EX_PSO"]) / num;
                        this._flexD["UM_P"] = this._flexD.CDecimal(this._flexD["UM_EX_PO"]) * this.cur환정보.DecimalValue;
                        this._flexD["UM"] = this._flexD.CDecimal(this._flexD["UM_EX"]) * this.cur환정보.DecimalValue;
                        this._flexD["CD_EXCH"] = this.cbo환정보.SelectedValue.ToString();
                        foreach (DataRow row in ((DataTable)this.cbo환정보.DataSource).Rows)
                        {
                            if (row["CODE"].ToString() == this.cbo환정보.SelectedValue.ToString())
                            {
                                this._flexD["NM_EXCH"] = row["NAME"].ToString();
                                break;
                            }
                        }
                        if (this._flexD.CDecimal(this._flexD["RT_PO"]) == 0M)
                            this._flexD["RT_PO"] = 1;
                        this._flexD["DT_LIMIT"] = this.dtp납기일.Text;
                        this._flexD["DT_PLAN"] = this.dtp납기일.Text;
                        this.FillPol(this._flexD.Row);
                        this.품목정보구하기(new object[] { this._flexD["CD_ITEM"].ToString(),
                                                           this._flexD["CD_PLANT"].ToString(),
                                                           this.LoginInfo.CompanyCode,
                                                           this.cbo단가유형.SelectedValue.ToString(),
                                                           this.cbo환정보.SelectedValue.ToString(),
                                                           this.dtp발주일자.Text,
                                                           this.ctx거래처.CodeValue,
                                                           this.ctx구매그룹.CodeValue,
                                                           "N",
                                                           this._flexD["CD_PJT"].ToString(),
                                                           Global.MainFrame.ServerKeyCommon.ToUpper() }, "품목전개", 0);
                        this.SetCC(this._flexD.Row, string.Empty);
                        this._flexD.AddFinished();
                        this._flexD.Col = this._flexD.Cols.Fixed;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void SetITEMEXP(DataTable dt)
        {
            if (dt == null || dt.Rows.Count <= 0)
                return;
            decimal maxValue = this._flexD.GetMaxValue("NO_LINE");
            for (int index = 0; index < dt.Rows.Count; ++index)
            {
                if (dt.Rows[index].RowState != DataRowState.Deleted)
                {
                    this._flexD.Rows.Add();
                    this._flexD.Row = this._flexD.Rows.Count - 1;
                    if (this.txt발주번호.Text != string.Empty)
                        this._flexD["NO_PO"] = this.txt발주번호.Text;
                    this._flexD["DT_LIMIT"] = Global.MainFrame.GetStringToday;
                    this._flexD["DT_PLAN"] = this._flexD["DT_LIMIT"];
                    ++maxValue;
                    this._flexD["NO_LINE"] = maxValue;
                    this._flexD["RT_PO"] = 1;
                    this._flexD["CD_SL"] = "";
                    this._flexD["NO_CONTRACT"] = "";
                    this._flexD["NO_RCV"] = "";
                    this._flexD["NO_PR"] = "";
                    this._flexD["NO_APP"] = "";
                    this._flexD["NO_CONTRACT"] = "";
                    this._flexD["CD_PJT"] = this.ctx프로젝트.CodeValue;
                    this._flexD["NM_PJT"] = this.ctx프로젝트.CodeName;
                    this._flexD["CD_PLANT"] = dt.Rows[index]["CD_PLANT"];
                    this._flexD["CD_ITEM"] = dt.Rows[index]["CD_ITEM"];
                    this._flexD["NM_ITEM"] = dt.Rows[index]["NM_ITEM"];
                    this._flexD["CD_UNIT_MM"] = dt.Rows[index]["CD_UNIT_MM"];
                    this._flexD["STND_ITEM"] = dt.Rows[index]["STND_ITEM"];
                    this._flexD["UNIT_IM"] = dt.Rows[index]["UNIT_IM"];
                    this._flexD["NM_SYSDEF"] = this._biz.GetGubunCodeSearch("PU_C000009", this._flexD["FG_POST"].ToString());
                    this._flexD["NM_CLS_ITEM"] = dt.Rows[index]["NM_CLS_ITEM"];
                    this.FillPol(this._flexD.Row);
                    decimal num;
                    if (dt.Rows[index]["RATE_EXCHG"] == null)
                    {
                        num = 1M;
                    }
                    else
                    {
                        num = this._flexD.CDecimal(dt.Rows[index]["RATE_EXCHG"]);
                        if (num == 0M)
                            num = 1M;
                    }
                    this._flexD["RT_PO"] = num;
                    this._flexD["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(dt.Rows[index]["UM_ITEM"]));
                    this._flexD["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(dt.Rows[index]["UM_ITEM"]) / num);
                    this._flexD["UM_P"] = Unit.원화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(this._flexD["UM_EX_PO"]) * this.cur환정보.DecimalValue);
                    this._flexD["UM"] = Unit.원화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(this._flexD["UM_EX"]) * this.cur환정보.DecimalValue);
                    this._flexD["CD_EXCH"] = this.cbo환정보.SelectedValue.ToString();
                    foreach (DataRow row in ((DataTable)this.cbo환정보.DataSource).Rows)
                    {
                        if (row["CODE"].ToString() == this.cbo환정보.SelectedValue.ToString())
                        {
                            this._flexD["NM_EXCH"] = row["NAME"].ToString();
                            break;
                        }
                    }
                    if (this._flexD.CDecimal(this._flexD["RT_PO"]) == 0M)
                        this._flexD["RT_PO"] = 1;
                    this._flexD["QT_PO_MM"] = Unit.수량(DataDictionaryTypes.PU, this._flexD.CDecimal(this._flexD["QT_PO"]) / this._flexD.CDecimal(this._flexD["RT_PO"]));
                    this._flexD["DT_LIMIT"] = this.dtp납기일.Text;
                    this._flexD["DT_PLAN"] = this.dtp납기일.Text;
                    this.FillPol(this._flexD.Row);
                    this.품목정보구하기(new object[] { this._flexD["CD_ITEM"].ToString(),
                                                       this._flexD["CD_PLANT"].ToString(),
                                                       this.LoginInfo.CompanyCode,
                                                       this.cbo단가유형.SelectedValue.ToString(),
                                                       this.cbo환정보.SelectedValue.ToString(),
                                                       this.dtp발주일자.Text,
                                                       this.ctx거래처.CodeValue,
                                                       this.ctx구매그룹.CodeValue,
                                                       "N",
                                                       this._flexD["CD_PJT"].ToString(),
                                                       Global.MainFrame.ServerKeyCommon.ToUpper() }, "품목전개", 0);
                    this.SetCC(this._flexD.Row, string.Empty);
                    this._flexD["CD_SL"] = dt.Rows[index]["CD_SL"];
                    this._flexD["NM_SL"] = dt.Rows[index]["NM_SL"];
                    this._flexD.AddFinished();
                    this._flexD.Col = this._flexD.Cols.Fixed;
                }
            }
            if (Global.MainFrame.ServerKeyCommon.Contains("WINFOOD"))
                this.SetWinFood("", 0);
        }

        private void 요청적용_Click(object sender, EventArgs e)
        {
            try
            {
                int num;
                if (!(this._m_partner_use == "100"))
                    num = !MA.ServerKey(false, new string[] { "HCT" }) ? 1 : 0;
                else
                    num = 0;
                if (num == 0)
                {
                    if (!this.HeaderCheck(1))
                        return;
                }
                else if (!this.HeaderCheck(0))
                    return;
                this.호출여부 = true;
                string[] strParam = new string[] { this.ctx거래처.CodeValue,
                                                   this.ctx거래처.CodeName };
                P_PU_POPR_SUB pPuPoprSub = new P_PU_POPR_SUB(this._flexD.DataTable, this.cbo공장.SelectedValue.ToString(), this.ctx구매그룹.CodeValue, this.ctx구매그룹.CodeName, strParam);
                Cursor.Current = Cursors.Default;
                if (MA.ServerKey(false, new string[] { "USHIO" }))
                    pPuPoprSub.SetCdEXCH = D.GetString(this.cbo환정보.SelectedValue);
                if (((Form)pPuPoprSub).ShowDialog(this) == DialogResult.OK)
                {
                    DataTable gdtReturn = pPuPoprSub.gdt_return;
                    if (gdtReturn == null || gdtReturn.Rows.Count <= 0)
                        return;
                    DataTable userColumnInfo = Config.UserColumnSetting.GetUSerColumnInfo(this.PageID, "P_PU_POPR_SUB");
                    if (this._m_partner_use == "100" && !this.Partner_Accept(gdtReturn) || Global.MainFrame.ServerKeyCommon == "HCT" && !this.Exch_Accept(gdtReturn))
                        return;
                    this.ControlButtonEnabledDisable((Control)sender, true);
                    this.cbo공장.Enabled = false;
                    this._flexD.Redraw = false;
                    this.mDataArea.Enabled = false;
                    this.flowLayoutPanel1.Enabled = false;
                    decimal maxValue = this._flexD.GetMaxValue("NO_LINE");
                    this.txt비고.Text = pPuPoprSub._get_dc_rmk;
                    this.txt비고2.Text = pPuPoprSub._get_dc_rmk2;
                    if (MA.ServerKey(true, new string[1] { "MDIK" }))
                    {
                        this.txt오더번호.Text = pPuPoprSub._get_dc_rmk3;
                        this._header.CurrentRow["NO_ORDER"] = pPuPoprSub._get_dc_rmk3;
                    }
                    for (int index = 0; index < gdtReturn.Rows.Count; ++index)
                    {
                        if (gdtReturn.Rows[index].RowState != DataRowState.Deleted)
                        {
                            ++maxValue;
                            this._flexD.Rows.Add();
                            this._flexD.Row = this._flexD.Rows.Count - 1;
                            this._flexD["CD_ITEM"] = gdtReturn.Rows[index]["CD_ITEM"];
                            this._flexD["NM_ITEM"] = gdtReturn.Rows[index]["NM_ITEM"];
                            this._flexD["STND_ITEM"] = gdtReturn.Rows[index]["STND_ITEM"];
                            this._flexD["CD_UNIT_MM"] = gdtReturn.Rows[index]["UNIT_PO"];
                            this._flexD["UNIT_PO"] = gdtReturn.Rows[index]["UNIT_PO"];
                            this._flexD["STND_MA_ITEM"] = gdtReturn.Rows[index]["STND_ITEM"];
                            this._flexD["UNIT_IM"] = gdtReturn.Rows[index]["UNIT_IM"];
                            this._flexD["GRP_MFG"] = gdtReturn.Rows[index]["GRP_MFG"];
                            this._flexD["NM_GRPMFG"] = gdtReturn.Rows[index]["NM_GRPMFG"];
                            this._flexD["DT_LIMIT"] = !(this.dtp납기일.Text == string.Empty) && !(D.GetString(gdtReturn.Rows[index]["DT_LIMIT"]) != "") ? this.dtp납기일.Text : gdtReturn.Rows[index]["DT_LIMIT"];
                            if (Global.MainFrame.ServerKeyCommon == "AMANO")
                            {
                                string DT = DateTime.ParseExact(Global.MainFrame.GetStringToday, "yyyyMMdd", null).AddDays((double)D.GetDecimal(gdtReturn.Rows[index]["LT_ITEM"])).ToString("yyyyMMdd");
                                this._flexD["DT_LIMIT"] = this._biz.GET_CALENDAR(this.cbo공장.SelectedValue.ToString(), DT);
                            }
                            this._flexD["DT_PLAN"] = gdtReturn.Rows[index]["DT_PLAN"];
                            this._flexD["FG_POST"] = "O";
                            this._flexD["NM_SYSDEF"] = this._biz.GetGubunCodeSearch("PU_C000009", this._flexD["FG_POST"].ToString());
                            this._flexD["QT_PO"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(gdtReturn.Rows[index]["QT_PR"]));
                            this._flexD["RT_PO"] = gdtReturn.Rows[index]["RT_PO"];
                            if (this._flexD.CDecimal(this._flexD["RT_PO"]) == 0M)
                                this._flexD["RT_PO"] = 1;
                            this._flexD["QT_PO_MM"] = Unit.수량(DataDictionaryTypes.PU, this._flexD.CDecimal(gdtReturn.Rows[index]["QT_PR"]) / this._flexD.CDecimal(this._flexD["RT_PO"]));
                            if (D.GetString(gdtReturn.Rows[index]["YN_REQ_UM"]) == "Y")
                            {
                                this._flexD["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(gdtReturn.Rows[index]["UM_EX_PO"]));
                                this._flexD["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(gdtReturn.Rows[index]["UM_EX"]));
                                this._flexD["UM_P"] = Unit.원화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(this._flexD["UM_EX_PO"]) * this.cur환정보.DecimalValue);
                                this._flexD["UM"] = Unit.원화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(gdtReturn.Rows[index]["UM_EX"]) * this.cur환정보.DecimalValue);
                                this._flexD["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, this._flexD.CDecimal(this._flexD["UM_EX_PO"]) * this._flexD.CDecimal(this._flexD["QT_PO_MM"]));
                            }
                            this._flexD["CD_PJT"] = gdtReturn.Rows[index]["CD_PJT"];
                            this._flexD["NM_PJT"] = gdtReturn.Rows[index]["NM_PJT"];
                            this._flexD["NO_PR"] = gdtReturn.Rows[index]["NO_PR"];
                            this._flexD["NO_PRLINE"] = gdtReturn.Rows[index]["NO_PRLINE"];
                            this._flexD["NO_CONTRACT"] = gdtReturn.Rows[index]["NO_CONTRACT"];
                            this._flexD["NO_CTLINE"] = gdtReturn.Rows[index]["NO_CTLINE"];
                            this._flexD["NO_APP"] = "";
                            this._flexD["CD_PLANT"] = gdtReturn.Rows[index]["CD_PLANT"];
                            if (this.txt발주번호.Text != string.Empty)
                                this._flexD["NO_PO"] = this.txt발주번호.Text;
                            this._flexD["NO_RCV"] = "";
                            this._flexD["NO_LINE"] = maxValue;
                            this._flexD["CD_SL"] = gdtReturn.Rows[index]["CD_SL"];
                            this._flexD["NM_SL"] = gdtReturn.Rows[index]["NM_SL"];
                            this._flexD["CD_EXCH"] = this.cbo환정보.SelectedValue.ToString();
                            this._flexD["DC1"] = gdtReturn.Rows[index]["DC_RMK"];
                            this._flexD["DC2"] = gdtReturn.Rows[index]["DC_RMK2"];
                            this._flexD["CD_BUDGET"] = gdtReturn.Rows[index]["CD_BUDGET"];
                            this._flexD["NM_BUDGET"] = gdtReturn.Rows[index]["NM_BUDGET"];
                            this._flexD["CD_BGACCT"] = gdtReturn.Rows[index]["CD_BGACCT"];
                            this._flexD["NM_BGACCT"] = gdtReturn.Rows[index]["NM_BGACCT"];
                            this._flexD["CD_BIZPLAN"] = gdtReturn.Rows[index]["CD_BIZPLAN"];
                            this._flexD["NM_BIZPLAN"] = gdtReturn.Rows[index]["NM_BIZPLAN"];
                            this._flexD["CD_ACCT"] = gdtReturn.Rows[index]["CD_ACCT"];
                            this._flexD["NM_ACCT"] = gdtReturn.Rows[index]["NM_ACCT"];
                            this._flexD["YN_BUDGET"] = gdtReturn.Rows[index]["YN_BUDGET"];
                            this._flexD["BUDGET_PASS"] = "N";
                            this._flexD["YN_BUDGET_PR"] = gdtReturn.Rows[index]["YN_BUDGET"];
                            if (this.m_sEnv_CC_Line == "Y" && gdtReturn.Rows[index]["CD_CC"] != null && gdtReturn.Rows[index]["CD_CC"].ToString().Trim() != "")
                            {
                                this._flexD["CD_CC"] = gdtReturn.Rows[index]["CD_CC"];
                                this._flexD["NM_CC"] = gdtReturn.Rows[index]["NM_CC"];
                            }
                            else if (this.m_sEnv_CC == "400" && this.m_sEnv_CC_Menu != "100")
                            {
                                this._flexD["CD_CC"] = gdtReturn.Rows[index]["CD_CC_EMP"];
                                this._flexD["NM_CC"] = gdtReturn.Rows[index]["NM_CC_EMP"];
                            }
                            else
                                this.SetCC(this._flexD.Row, string.Empty, "", "", D.GetString(gdtReturn.Rows[index]["CD_CC_EMP"]), D.GetString(gdtReturn.Rows[index]["NM_CC_EMP"]));
                            foreach (DataRow row in ((DataTable)this.cbo환정보.DataSource).Rows)
                            {
                                if (row["CODE"].ToString() == this.cbo환정보.SelectedValue.ToString())
                                {
                                    this._flexD["NM_EXCH"] = row["NAME"];
                                    break;
                                }
                            }
                            if (pPuPoprSub.요청비고체크)
                                this._flexD["DC1"] = gdtReturn.Rows[index]["DC_LINE_RMK"];
                            this._flexD["CD_PJT"] = D.GetString(gdtReturn.Rows[index]["CD_PJT_LINE"]);
                            this._flexD["NM_PJT"] = D.GetString(gdtReturn.Rows[index]["NM_PJT_LINE"]);
                            this._flexD["SEQ_PROJECT"] = D.GetDecimal(gdtReturn.Rows[index]["SEQ_PJT_LINE"]);
                            this._flexD["CD_PJT_ITEM"] = gdtReturn.Rows[index]["CD_PJT_ITEM"];
                            this._flexD["NM_PJT_ITEM"] = gdtReturn.Rows[index]["NM_PJT_ITEM"];
                            this._flexD["PJT_ITEM_STND"] = gdtReturn.Rows[index]["PJT_ITEM_STND"];
                            this._flexD["NM_CLS_ITEM"] = D.GetString(gdtReturn.Rows[index]["NM_CLS_ITEM"]);
                            this._flexD["CD_ITEM_ORIGIN"] = D.GetString(gdtReturn.Rows[index]["CD_ITEM_ORIGIN"]);
                            this._flexD["FG_PACKING"] = gdtReturn.Rows[index]["FG_PACKING"];
                            this._flexD["FG_SU"] = gdtReturn.Rows[index]["FG_SU"];
                            this._flexD["CD_REASON"] = gdtReturn.Rows[index]["CD_REASON"];
                            this._flexD["PI_PARTNER"] = gdtReturn.Rows[index]["PARTNER"];
                            this._flexD["PI_LN_PARTNER"] = gdtReturn.Rows[index]["PARTNER_NM"];
                            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "ICDERPU")
                                this._flexD["CD_USERDEF1"] = gdtReturn.Rows[index]["CD_USERDEF1"];
                            if (this._APP_USERDEF == "Y")
                            {
                                this._flexD["NM_USERDEF1"] = gdtReturn.Rows[index]["CD_USERDEF1"];
                                this._flexD["NM_USERDEF2"] = gdtReturn.Rows[index]["CD_USERDEF2"];
                                if (Global.MainFrame.ServerKeyCommon.ToUpper() != "ICDERPU")
                                {
                                    this._flexD["DATE_USERDEF1"] = gdtReturn.Rows[index]["DATE_USERDEF1_PR"];
                                    this._flexD["DATE_USERDEF2"] = gdtReturn.Rows[index]["DATE_USERDEF2_PR"];
                                }
                                this._flexD["TXT_USERDEF1"] = gdtReturn.Rows[index]["TXT_USERDEF1_PR"];
                                this._flexD["TXT_USERDEF2"] = gdtReturn.Rows[index]["TXT_USERDEF2_PR"];
                                this._flexD["CDSL_USERDEF1"] = gdtReturn.Rows[index]["CDSL_USERDEF1_PR"];
                                this._flexD["NMSL_USERDEF1"] = gdtReturn.Rows[index]["NMSL_USERDEF1_PR"];
                                this._flexD["NUM_USERDEF1"] = gdtReturn.Rows[index]["NU_USERDEF1"];
                                this._flexD["NUM_USERDEF2"] = gdtReturn.Rows[index]["NU_USERDEF1"];
                            }
                            if (this.bStandard)
                            {
                                this._flexD["NUM_STND_ITEM_1"] = gdtReturn.Rows[index]["NUM_STND_ITEM_1"];
                                this._flexD["NUM_STND_ITEM_2"] = gdtReturn.Rows[index]["NUM_STND_ITEM_2"];
                                this._flexD["NUM_STND_ITEM_3"] = gdtReturn.Rows[index]["NUM_STND_ITEM_3"];
                                this._flexD["NUM_STND_ITEM_4"] = gdtReturn.Rows[index]["NUM_STND_ITEM_4"];
                                this._flexD["NUM_STND_ITEM_5"] = gdtReturn.Rows[index]["NUM_STND_ITEM_5"];
                                this._flexD["UM_WEIGHT"] = gdtReturn.Rows[index]["UM_WEIGHT"];
                                this._flexD["TOT_WEIGHT"] = gdtReturn.Rows[index]["TOT_WEIGHT"];
                                this._flexD["CLS_L"] = gdtReturn.Rows[index]["CLS_L"];
                                this._flexD["CLS_M"] = gdtReturn.Rows[index]["CLS_M"];
                                this._flexD["CLS_S"] = gdtReturn.Rows[index]["CLS_S"];
                                this._flexD["NM_CLS_L"] = gdtReturn.Rows[index]["NM_CLS_L"];
                                this._flexD["NM_CLS_M"] = gdtReturn.Rows[index]["NM_CLS_M"];
                                this._flexD["NM_CLS_S"] = gdtReturn.Rows[index]["NM_CLS_S"];
                                this._flexD["SG_TYPE"] = gdtReturn.Rows[index]["SG_TYPE"];
                                this._flexD["QT_SG"] = gdtReturn.Rows[index]["QT_SG"];
                            }
                            this.FillPol(this._flexD.Row);
                            this.품목정보구하기(new object[] { this._flexD["CD_ITEM"].ToString(),
                                                               this._flexD["CD_PLANT"].ToString(),
                                                               this.LoginInfo.CompanyCode,
                                                               this.cbo단가유형.SelectedValue.ToString(),
                                                               this.cbo환정보.SelectedValue.ToString(),
                                                               this.dtp발주일자.Text,
                                                               this.ctx거래처.CodeValue,
                                                               this.ctx구매그룹.CodeValue,
                                                               gdtReturn.Rows[index]["YN_REQ_UM"],
                                                               D.GetString(this._flexD["CD_PJT"]),
                                                               Global.MainFrame.ServerKeyCommon.ToUpper() }, "요청", 0);
                            if (D.GetDecimal(this._flexD["UM_WEIGHT"]) == 0M)
                                this.부가세계산(this._flexD.GetDataRow(this._flexD.Row));
                            if (this._m_Company_only == "001")
                                this.AsahiKasei_Only_ValidateEdit(this._flexD.Row, D.GetDecimal(this._flexD["UM_EX_PO"]), "UM_EX_PO");
                            if (Config.MA_ENV.PJT형여부 == "Y")
                            {
                                this._flexD["NO_WBS"] = gdtReturn.Rows[index]["NO_WBS"];
                                this._flexD["NO_CBS"] = gdtReturn.Rows[index]["NO_CBS"];
                                this._flexD["CD_ACTIVITY"] = gdtReturn.Rows[index]["CD_ACTIVITY"];
                                this._flexD["NM_ACTIVITY"] = gdtReturn.Rows[index]["NM_ACTIVITY"];
                                this._flexD["CD_COST"] = gdtReturn.Rows[index]["CD_COST"];
                                this._flexD["NM_COST"] = gdtReturn.Rows[index]["NM_COST"];
                                this._flexD["NO_LINE_PJTBOM"] = gdtReturn.Rows[index]["NO_LINE_PJTBOM"];
                                this._flexD["CD_ITEM_MO"] = gdtReturn.Rows[index]["CD_ITEM_MO"];
                                this._flexD["NM_ITEM_MO"] = gdtReturn.Rows[index]["NM_ITEM_MO"];
                            }
                            if (this.MainFrameInterface.ServerKeyCommon == "UNIPOINT")
                            {
                                this._flexD["CD_PARTNER_PJT"] = gdtReturn.Rows[index]["CD_PARTNER_PJT"];
                                this._flexD["LN_PARTNER_PJT"] = gdtReturn.Rows[index]["LN_PARTNER_PJT"];
                                this._flexD["NO_EMP_PJT"] = gdtReturn.Rows[index]["NO_EMP_PJT"];
                                this._flexD["NM_KOR_PJT"] = gdtReturn.Rows[index]["NM_KOR_PJT"];
                                this._flexD["END_USER"] = gdtReturn.Rows[index]["END_USER"];
                            }
                            if (App.SystemEnv.PMS사용)
                            {
                                this._flexD["CD_CSTR"] = gdtReturn.Rows[index]["CD_CSTR"];
                                this._flexD["DL_CSTR"] = gdtReturn.Rows[index]["DL_CSTR"];
                                this._flexD["NM_CSTR"] = gdtReturn.Rows[index]["NM_CSTR"];
                                this._flexD["SIZE_CSTR"] = gdtReturn.Rows[index]["SIZE_CSTR"];
                                this._flexD["UNIT_CSTR"] = gdtReturn.Rows[index]["UNIT_CSTR"];
                                this._flexD["QTY_ACT"] = gdtReturn.Rows[index]["QTY_ACT"];
                                this._flexD["UNT_ACT"] = gdtReturn.Rows[index]["UNT_ACT"];
                                this._flexD["AMT_ACT"] = gdtReturn.Rows[index]["AMT_ACT"];
                            }
                            if (BASIC.GetMAEXC_Menu(nameof(P_CZ_PU_PO_WINTEC), "PU_Z00000002") != "000")
                                this._flexD["AM_OLD"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(gdtReturn.Rows[index]["AM"]));
                            Config.UserColumnSetting.ApplyUserColumn(userColumnInfo, gdtReturn.Rows[index], this._flexD, this._flexD.Row);
                            if (this.sPUSU == "100")
                                this.GET_SU_BOM();
                            this._flexD.AddFinished();
                            this._flexD.Col = this._flexD.Cols.Fixed;
                        }
                    }
                    if (Global.MainFrame.ServerKeyCommon.Contains("WINFOOD"))
                        this.SetWinFood("", 0);
                    this.SUMFunction();
                    this.SetHeadControlEnabled(false, 5);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this._flexD.Redraw = true;
                this.mDataArea.Enabled = true;
                this.flowLayoutPanel1.Enabled = true;
            }
        }

        private void 품의적용_Click(object sender, EventArgs e)
        {
            if (this._m_partner_use == "100" || this._m_partner_change == "001")
            {
                if (!this.HeaderCheck(1))
                    return;
            }
            else if (!this.HeaderCheck(0))
                return;
            this.호출여부 = true;
            Cursor.Current = Cursors.WaitCursor;
            P_PU_APP_SUB2 pPuAppSuB2 = !(this._m_partner_change != "001") ? new P_PU_APP_SUB2(this.MainFrameInterface, this._flexD.DataTable, this.cbo공장.SelectedValue.ToString(), this.cbo공장.SelectedText.ToString(), this.ctx구매그룹.CodeValue, this.ctx구매그룹.CodeName, this.ctx거래처.CodeValue, this.ctx거래처.CodeName, D.GetString(this.cbo환정보.SelectedValue), this.cur환정보.DecimalValue) : new P_PU_APP_SUB2(this.MainFrameInterface, this._flexD.DataTable, this.cbo공장.SelectedValue.ToString(), this.cbo공장.SelectedText.ToString(), this.ctx구매그룹.CodeValue, this.ctx구매그룹.CodeName, this.ctx거래처.CodeValue, this.ctx거래처.CodeName);
            pPuAppSuB2.품의헤더사용유무 = true;
            if (((Form)pPuAppSuB2).ShowDialog(this) != DialogResult.OK)
                return;
            DataTable dt = pPuAppSuB2.gdt_Return;
            try
            {
                if (dt == null || dt.Rows.Count < 0)
                    return;
                DataTable userColumnInfo = Config.UserColumnSetting.GetUSerColumnInfo(this.PageID, "P_PU_APP_SUB2");
                if (this.m_sEnv_App_Sort == "100")
                {
                    dt.DefaultView.Sort = "NO_APP, NO_APPLINE";
                    dt = dt.DefaultView.ToTable().Copy();
                }
                if ((this._m_partner_use == "100" || this._m_partner_change == "001") && !this.Partner_Accept(dt) || this._m_tppo_use == "001" && !this.Tppo_Accept(dt, "") || this._m_partner_change == "001" && !this.Exch_Accept(dt))
                    return;
                this._header.CurrentRow["DC_RMK_TEXT"] = D.GetString(dt.Rows[0]["DC_RMK_TEXT"]);
                this.txt발주텍스트비고1.Text = D.GetString(dt.Rows[0]["DC_RMK_TEXT"]);
                this.ControlButtonEnabledDisable((Control)sender, true);
                this.cbo공장.Enabled = false;
                if (D.GetString(dt.Rows[0]["COND_PRICE"]) != string.Empty)
                {
                    this.cbo가격조건.SelectedValue = D.GetString(dt.Rows[0]["COND_PRICE"]);
                    this._header.CurrentRow["COND_PRICE"] = D.GetString(dt.Rows[0]["COND_PRICE"]);
                }
                if (D.GetString(dt.Rows[0]["CD_STND_PAY"]) != string.Empty)
                {
                    this.cbo지급기준.SelectedValue = D.GetString(dt.Rows[0]["CD_STND_PAY"]);
                    this._header.CurrentRow["STND_PAY"] = D.GetString(dt.Rows[0]["CD_STND_PAY"]);
                }
              this._flexD.Redraw = false;
                this.mDataArea.Enabled = false;
                this.flowLayoutPanel1.Enabled = false;
                decimal maxValue = this._flexD.GetMaxValue("NO_LINE");
                for (int index = 0; index < dt.Rows.Count; ++index)
                {
                    if (dt.Rows[index].RowState != DataRowState.Deleted)
                    {
                        this._flexD.Rows.Add();
                        this._flexD.Row = this._flexD.Rows.Count - 1;
                        if (this.txt발주번호.Text != string.Empty)
                            this._flexD["NO_PO"] = this.txt발주번호.Text;
                        this._flexD["NO_LINE"] = ++maxValue;
                        this._flexD["RT_PO"] = dt.Rows[index]["RT_PO"];
                        if (this._flexD.CDecimal(this._flexD["RT_PO"]) == 0M)
                            this._flexD["RT_PO"] = 1;
                        this._flexD["QT_PO_MM"] = !(D.GetString(dt.Rows[index]["QT_APP_MM"]) == string.Empty) ? Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(dt.Rows[index]["QT_APP_MM"])) : Unit.수량(DataDictionaryTypes.PU, this._flexD.CDecimal(dt.Rows[index]["QT_APP"]) / this._flexD.CDecimal(this._flexD["RT_PO"]));
                        this._flexD["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(dt.Rows[index]["UM_EX_PO"]));
                        this._flexD["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(dt.Rows[index]["UM_EX"]));
                        this._flexD["UM_P"] = Unit.원화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(this._flexD["UM_EX_PO"]) * this.cur환정보.DecimalValue);
                        this._flexD["UM"] = Unit.원화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(dt.Rows[index]["UM_EX"]) * this.cur환정보.DecimalValue);
                        this._flexD["NO_CONTRACT"] = dt.Rows[index]["NO_CONTRACT"];
                        this._flexD["NO_CTLINE"] = dt.Rows[index]["NO_CTLINE"];
                        this._flexD["NO_RCV"] = "";
                        this._flexD["NO_PR"] = dt.Rows[index]["NO_PR"].ToString();
                        this._flexD["NO_PRLINE"] = D.GetDecimal(dt.Rows[index]["NO_PRLINE"]);
                        this._flexD["NO_APP"] = dt.Rows[index]["NO_APP"];
                        this._flexD["NO_APPLINE"] = D.GetDecimal(dt.Rows[index]["NO_APPLINE"]);
                        this._flexD["FG_POST"] = "O";
                        this._flexD["NM_SYSDEF"] = this._biz.GetGubunCodeSearch("PU_C000009", this._flexD["FG_POST"].ToString());
                        this._flexD["CD_PLANT"] = dt.Rows[index]["CD_PLANT"];
                        this._flexD["CD_ITEM"] = dt.Rows[index]["CD_ITEM"];
                        this._flexD["NM_ITEM"] = dt.Rows[index]["NM_ITEM"];
                        this._flexD["CD_UNIT_MM"] = dt.Rows[index]["UNIT_PO"];
                        this._flexD["UNIT_PO"] = dt.Rows[index]["UNIT_PO"];
                        this._flexD["STND_MA_ITEM"] = dt.Rows[index]["STND_ITEM"];
                        this._flexD["STND_ITEM"] = dt.Rows[index]["STND_ITEM"];
                        this._flexD["UNIT_IM"] = dt.Rows[index]["UNIT_IM"];
                        this._flexD["QT_PO"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(dt.Rows[index]["QT_APP"]));
                        this._flexD["DT_LIMIT"] = !(this.dtp납기일.Text == string.Empty) && !(D.GetString(dt.Rows[index]["DT_LIMIT"]) != "") ? this.dtp납기일.Text : dt.Rows[index]["DT_LIMIT"];
                        this._flexD["DT_PLAN"] = dt.Rows[index]["DT_PLAN"];
                        this._flexD["CD_SL"] = dt.Rows[index]["CD_SL"];
                        this._flexD["NM_SL"] = dt.Rows[index]["NM_SL"];
                        this._flexD["CD_EXCH"] = this.cbo환정보.SelectedValue.ToString();
                        this._flexD["CD_BUDGET"] = dt.Rows[index]["CD_BUDGET"];
                        this._flexD["NM_BUDGET"] = dt.Rows[index]["NM_BUDGET"];
                        this._flexD["CD_BGACCT"] = dt.Rows[index]["CD_BGACCT"];
                        this._flexD["NM_BGACCT"] = dt.Rows[index]["NM_BGACCT"];
                        this._flexD["CD_PJT"] = dt.Rows[index]["CD_PJT"];
                        this._flexD["NM_PJT"] = dt.Rows[index]["NM_PROJECT"];
                        this._flexD["SEQ_PROJECT"] = D.GetDecimal(dt.Rows[index]["SEQ_PROJECT"]);
                        this._flexD["CD_PJT_ITEM"] = dt.Rows[index]["CD_PJT_ITEM"];
                        this._flexD["NM_PJT_ITEM"] = dt.Rows[index]["NM_PJT_ITEM"];
                        this._flexD["PJT_ITEM_STND"] = dt.Rows[index]["PJT_ITEM_STND"];
                        if (this.m_sEnv_CC_Line == "Y" && dt.Rows[index]["CD_CC"] != null && dt.Rows[index]["CD_CC"].ToString().Trim() != "")
                        {
                            this._flexD["CD_CC"] = dt.Rows[index]["CD_CC"];
                            this._flexD["NM_CC"] = dt.Rows[index]["NM_CC"];
                        }
                        else
                            this.SetCC(this._flexD.Row, string.Empty, D.GetString(dt.Rows[index]["CD_CC"]), D.GetString(dt.Rows[index]["NM_CC"]), D.GetString(dt.Rows[index]["CD_PR_EMP_CC"]), D.GetString(dt.Rows[index]["NM_PR_EMP_CC"]));
                        foreach (DataRow row in ((DataTable)this.cbo환정보.DataSource).Rows)
                        {
                            if (row["CODE"].ToString() == this.cbo환정보.SelectedValue.ToString())
                            {
                                this._flexD["NM_EXCH"] = row["NAME"];
                                break;
                            }
                        }
                        this._flexD["DC1"] = dt.Rows[index]["DC_RMK1"];
                        this._flexD["DC2"] = dt.Rows[index]["DC_RMK2"];
                        this._flexD["NM_CLS_ITEM"] = dt.Rows[index]["NM_CLS_ITEM"];
                        this._flexD["CD_ITEM_ORIGIN"] = D.GetString(dt.Rows[index]["CD_ITEM_ORIGIN"]);
                        this._flexD["FG_PACKING"] = dt.Rows[index]["FG_PACKING"];
                        this._flexD["FG_SU"] = dt.Rows[index]["FG_SU"];
                        this._flexD["CD_REASON"] = dt.Rows[index]["CD_REASON"];
                        this._flexD["NM_GRPMFG"] = dt.Rows[index]["NM_GRPMFG"];
                        this._flexD["PI_PARTNER"] = dt.Rows[index]["PI_PARTNER"];
                        this._flexD["PI_LN_PARTNER"] = dt.Rows[index]["PI_LN_PARTNER"];
                        if (Global.MainFrame.ServerKeyCommon.ToUpper() == "ICDERPU")
                        {
                            this._flexD["CD_USERDEF1"] = dt.Rows[index]["CD_USERDEF1"];
                            this._flexD["UM_PRE"] = dt.Rows[index]["UM_PRE"];
                            this._flexD["AM_PRE"] = dt.Rows[index]["AM_PRE"];
                        }
                        if (this._APP_USERDEF == "Y")
                        {
                            this._flexD["NM_USERDEF1"] = dt.Rows[index]["CD_USERDEF1"];
                            this._flexD["NM_USERDEF2"] = dt.Rows[index]["CD_USERDEF2"];
                            this._flexD["DATE_USERDEF1"] = dt.Rows[index]["DATE_USERDEF1_APP"];
                            this._flexD["DATE_USERDEF2"] = dt.Rows[index]["DATE_USERDEF2_APP"];
                            this._flexD["TXT_USERDEF1"] = dt.Rows[index]["TXT_USERDEF1_APP"];
                            this._flexD["TXT_USERDEF2"] = dt.Rows[index]["TXT_USERDEF2_APP"];
                            this._flexD["CDSL_USERDEF1"] = dt.Rows[index]["CDSL_USERDEF1_APP"];
                            this._flexD["NMSL_USERDEF1"] = dt.Rows[index]["NMSL_USERDEF1_APP"];
                            this._flexD["NUM_USERDEF1"] = dt.Rows[index]["NU_USERDEF1"];
                            this._flexD["NUM_USERDEF2"] = dt.Rows[index]["NU_USERDEF1"];
                        }
                        if (D.GetString(dt.Rows[index]["FG_PAYMENT"]) != "" && Global.MainFrame.ServerKeyCommon != "DNF")
                        {
                            this._header.CurrentRow["FG_PAYMENT"] = dt.Rows[index]["FG_PAYMENT"];
                            this.cbo지급조건.SelectedValue = D.GetString(this._header.CurrentRow["FG_PAYMENT"]);
                        }
                        if (this.bStandard)
                        {
                            this._flexD["NUM_STND_ITEM_1"] = dt.Rows[index]["NUM_STND_ITEM_1"];
                            this._flexD["NUM_STND_ITEM_2"] = dt.Rows[index]["NUM_STND_ITEM_2"];
                            this._flexD["NUM_STND_ITEM_3"] = dt.Rows[index]["NUM_STND_ITEM_3"];
                            this._flexD["NUM_STND_ITEM_4"] = dt.Rows[index]["NUM_STND_ITEM_4"];
                            this._flexD["NUM_STND_ITEM_5"] = dt.Rows[index]["NUM_STND_ITEM_5"];
                            this._flexD["UM_WEIGHT"] = dt.Rows[index]["UM_WEIGHT"];
                            this._flexD["TOT_WEIGHT"] = dt.Rows[index]["TOT_WEIGHT"];
                            this._flexD["CLS_L"] = dt.Rows[index]["CLS_L"];
                            this._flexD["CLS_M"] = dt.Rows[index]["CLS_M"];
                            this._flexD["CLS_S"] = dt.Rows[index]["CLS_S"];
                            this._flexD["NM_CLS_L"] = dt.Rows[index]["NM_CLS_L"];
                            this._flexD["NM_CLS_M"] = dt.Rows[index]["NM_CLS_M"];
                            this._flexD["NM_CLS_S"] = dt.Rows[index]["NM_CLS_S"];
                            this._flexD["SG_TYPE"] = dt.Rows[index]["SG_TYPE"];
                            this._flexD["QT_SG"] = dt.Rows[index]["QT_SG"];
                        }
                        this.FillPol(this._flexD.Row);
                        object[] m_obj = new object[] { this._flexD["CD_ITEM"].ToString(),
                                                        this._flexD["CD_PLANT"].ToString(),
                                                        this.LoginInfo.CompanyCode,
                                                        this.cbo단가유형.SelectedValue.ToString(),
                                                        this.cbo환정보.SelectedValue.ToString(),
                                                        this.dtp발주일자.Text,
                                                        this.ctx거래처.CodeValue,
                                                        this.ctx구매그룹.CodeValue,
                                                        "N",
                                                        D.GetString(this._flexD["CD_PJT"]),
                                                        Global.MainFrame.ServerKeyCommon.ToUpper(),
                                                        Global.SystemLanguage.MultiLanguageLpoint.ToString() };
                        if (Global.MainFrame.ServerKeyCommon.Contains("WINFOOD"))
                        {
                            DataSet dataSet = this._biz.ItemInfo_Search(m_obj);
                            if (dataSet.Tables[1].Rows.Count == 1)
                            {
                                this._flexD["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(dataSet.Tables[1].Rows[0]["UM_ITEM"]));
                                this._flexD["UM_EX"] = (Unit.외화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(dataSet.Tables[1].Rows[0]["UM_ITEM"])) / D.GetDecimal(this._flexD["RT_PO"]));
                                this._flexD["UM_P"] = Unit.원화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(this._flexD["UM_EX_PO"]) * this.cur환정보.DecimalValue);
                                this._flexD["UM"] = Unit.원화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(this._flexD["UM_EX"]) * this.cur환정보.DecimalValue);
                                this._flexD["DC1"] = dataSet.Tables[1].Rows[0]["NM_FG_UM"];
                            }
                        }
                        this.품목정보구하기(m_obj, "품의", 0);
                        if (D.GetDecimal(this._flexD["UM_WEIGHT"]) == 0M)
                            this.부가세계산(this._flexD.GetDataRow(this._flexD.Row));
                        if (this._m_Company_only == "001")
                            this.AsahiKasei_Only_ValidateEdit(this._flexD.Row, D.GetDecimal(this._flexD["UM_EX_PO"]), "UM_EX_PO");
                        if (Config.MA_ENV.PJT형여부 == "Y")
                        {
                            this._flexD["NO_WBS"] = dt.Rows[index]["NO_WBS"];
                            this._flexD["NO_CBS"] = dt.Rows[index]["NO_CBS"];
                            this._flexD["CD_ACTIVITY"] = dt.Rows[index]["CD_ACTIVITY"];
                            this._flexD["NM_ACTIVITY"] = dt.Rows[index]["NM_ACTIVITY"];
                            this._flexD["CD_COST"] = dt.Rows[index]["CD_COST"];
                            this._flexD["NM_COST"] = dt.Rows[index]["NM_COST"];
                            this._flexD["NO_LINE_PJTBOM"] = dt.Rows[index]["NO_LINE_PJTBOM"];
                            this._flexD["CD_ITEM_MO"] = dt.Rows[index]["CD_ITEM_MO"];
                            this._flexD["NM_ITEM_MO"] = dt.Rows[index]["NM_ITEM_MO"];
                        }
                        if (D.GetString(this._flexD["TP_UM_TAX"]) != "001" && this.m_sEnv_App_Am == "001")
                        {
                            this._flexD["AM_EX"] = dt.Rows[index]["AM_EX_JAN"];
                            this._flexD["AM"] = dt.Rows[index]["AM_JAN"];
                            this.부가세만계산();
                        }
                        if (BASIC.GetMAEXC_Menu("P_PU_APP_MNG", "PU_A00000003") == "100")
                        {
                            if (D.GetDecimal(this._flexD["VAT"]) > 0M)
                            {
                                this._flexD["AM_EX"] = dt.Rows[index]["AM_EX_JAN"];
                                this._flexD["AM"] = dt.Rows[index]["AM_JAN"];
                                this._flexD["FG_TAX"] = dt.Rows[index]["FG_TAX"];
                                this._flexD["VAT"] = dt.Rows[index]["VAT"];
                                this._flexD["TP_UM_TAX"] = dt.Rows[index]["TP_UM_TAX"];
                            }
                            else
                            {
                                this._flexD["AM_EX"] = dt.Rows[index]["AM_EX_JAN"];
                                this._flexD["AM"] = dt.Rows[index]["AM_JAN"];
                                this.부가세만계산();
                            }
                        }
                        if (App.SystemEnv.PMS사용)
                        {
                            this._flexD["CD_CSTR"] = dt.Rows[index]["CD_CSTR"];
                            this._flexD["DL_CSTR"] = dt.Rows[index]["DL_CSTR"];
                            this._flexD["NM_CSTR"] = dt.Rows[index]["NM_CSTR"];
                            this._flexD["SIZE_CSTR"] = dt.Rows[index]["SIZE_CSTR"];
                            this._flexD["UNIT_CSTR"] = dt.Rows[index]["UNIT_CSTR"];
                            this._flexD["QTY_ACT"] = dt.Rows[index]["QTY_ACT"];
                            this._flexD["UNT_ACT"] = dt.Rows[index]["UNT_ACT"];
                            this._flexD["AMT_ACT"] = dt.Rows[index]["AMT_ACT"];
                        }
                        if (BASIC.GetMAEXC_Menu(nameof(P_CZ_PU_PO_WINTEC), "PU_Z00000002") != "000")
                            this._flexD["AM_OLD"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(dt.Rows[index]["AM_JAN"]));
                        this._flexD["CD_BIZPLAN"] = dt.Rows[index]["CD_BIZPLAN"];
                        this._flexD["NM_BIZPLAN"] = dt.Rows[index]["NM_BIZPLAN"];
                        Config.UserColumnSetting.ApplyUserColumn(userColumnInfo, dt.Rows[index], this._flexD, this._flexD.Row);
                        this._flexD.AddFinished();
                        this._flexD.Col = this._flexD.Cols.Fixed;
                        if (this.sPUSU == "100")
                            this.GET_SU_BOM();
                    }
                }
                this.SUMFunction();
                int num;
                if (!(Global.MainFrame.ServerKeyCommon == "DAEJOOKC"))
                    num = !MA.ServerKey(false, new string[] { "ABLBIO" }) ? 1 : 0;
                else
                    num = 0;
                if (num == 0)
                    this.SetHeadControlEnabled(false, 6);
                else
                    this.SetHeadControlEnabled(false, 5);
                if (this._m_tppo_change == "001")
                {
                    this.btn품목추가.Enabled = false;
                    this.btn품목삭제.Enabled = false;
                }
                if (Global.MainFrame.ServerKeyCommon == "TYPHC" || pPuAppSuB2.품의헤더비고체크)
                {
                    this.txt비고.Text = D.GetString(dt.Rows[0]["DC_RMK_H"]);
                    this.txt비고2.Text = D.GetString(dt.Rows[0]["DC_RMK_H2"]);
                }
                if (Global.MainFrame.ServerKeyCommon.Contains("WINFOOD"))
                    this.SetWinFood("", 0);
                this._flexD.SumRefresh();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this._flexD.Redraw = true;
                this.mDataArea.Enabled = true;
                this.flowLayoutPanel1.Enabled = true;
                this.txt비고.Focus();
            }
        }

        private void btn_RE_SO_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.HeaderCheck(0))
                    return;
                this.호출여부 = true;
                P_PU_PO_SO_SUB pPuPoSoSub = new P_PU_PO_SO_SUB(this.cbo공장.SelectedValue.ToString(), this.cbo공장.Text, this._flexD.DataTable);
                Cursor.Current = Cursors.Default;
                if (((Form)pPuPoSoSub).ShowDialog(this) == DialogResult.OK)
                {
                    DataTable 수주데이터 = pPuPoSoSub.수주데이터;
                    string empty = string.Empty;
                    if (수주데이터 == null || 수주데이터.Rows.Count <= 0)
                        return;
                    this.ControlButtonEnabledDisable((Control)sender, true);
                    this.cbo공장.Enabled = false;
                    this._flexD.Redraw = false;
                    this.mDataArea.Enabled = false;
                    this.flowLayoutPanel1.Enabled = false;
                    if (Global.MainFrame.ServerKeyCommon == "MHIK")
                    {
                        if (!string.IsNullOrEmpty(D.GetString(this._header.CurrentRow["TXT_USERDEF4"])) && D.GetString(this._header.CurrentRow["TXT_USERDEF4"]) != D.GetString(수주데이터.Rows[0]["TXT_USERDEF4_H"]))
                        {
                            foreach (DataRow dataRow in this._flexD.DataTable.Select(""))
                                dataRow.Delete();
                            this.tb_NO_PO_MH.Text = D.GetString(수주데이터.Rows[0]["TXT_USERDEF4_H"]);
                            this._header.CurrentRow["TXT_USERDEF4"] = D.GetString(수주데이터.Rows[0]["TXT_USERDEF4_H"]);
                        }
                        if (string.IsNullOrEmpty(D.GetString(this._header.CurrentRow["TXT_USERDEF4"])))
                        {
                            this.tb_NO_PO_MH.Text = D.GetString(수주데이터.Rows[0]["TXT_USERDEF4_H"]);
                            this._header.CurrentRow["TXT_USERDEF4"] = D.GetString(수주데이터.Rows[0]["TXT_USERDEF4_H"]);
                        }
                    }
                    decimal maxValue = this._flexD.GetMaxValue("NO_LINE");
                    for (int index = 0; index < 수주데이터.Rows.Count; ++index)
                    {
                        if (수주데이터.Rows[index].RowState != DataRowState.Deleted)
                        {
                            ++maxValue;
                            this._flexD.Rows.Add();
                            this._flexD.Row = this._flexD.Rows.Count - 1;
                            this._flexD["CD_ITEM"] = 수주데이터.Rows[index]["CD_ITEM"];
                            this._flexD["NM_ITEM"] = 수주데이터.Rows[index]["NM_ITEM"];
                            this._flexD["STND_ITEM"] = 수주데이터.Rows[index]["STND_ITEM"];
                            this._flexD["CD_UNIT_MM"] = 수주데이터.Rows[index]["UNIT_PO"];
                            this._flexD["UNIT_PO"] = 수주데이터.Rows[index]["UNIT_PO"];
                            this._flexD["STND_MA_ITEM"] = 수주데이터.Rows[index]["STND_ITEM"];
                            this._flexD["UNIT_IM"] = 수주데이터.Rows[index]["UNIT_IM"];
                            this._flexD["DT_LIMIT"] = !(D.GetString(수주데이터.Rows[index]["DT_DUEDATE"]) != "") ? this.dtp납기일.Text : 수주데이터.Rows[index]["DT_DUEDATE"];
                            this._flexD["DT_PLAN"] = this._flexD["DT_LIMIT"];
                            this._flexD["FG_POST"] = "O";
                            this._flexD["NM_SYSDEF"] = this._biz.GetGubunCodeSearch("PU_C000009", this._flexD["FG_POST"].ToString());
                            this._flexD["RT_PO"] = 수주데이터.Rows[index]["RT_PO"];
                            this._flexD["CD_PJT"] = 수주데이터.Rows[index]["CD_PJT"];
                            this._flexD["NM_PJT"] = 수주데이터.Rows[index]["NM_PJT"];
                            this._flexD["NO_PR"] = "";
                            this._flexD["NO_PRLINE"] = 0;
                            this._flexD["NO_SO"] = 수주데이터.Rows[index]["NO_SO"];
                            this._flexD["NO_SOLINE"] = 수주데이터.Rows[index]["SEQ_SO"];
                            this._flexD["CD_PLANT"] = 수주데이터.Rows[index]["CD_PLANT"];
                            if (this.txt발주번호.Text != string.Empty)
                                this._flexD["NO_PO"] = this.txt발주번호.Text;
                            this._flexD["NO_RCV"] = "";
                            this._flexD["NO_LINE"] = maxValue;
                            this._flexD["CD_SL"] = 수주데이터.Rows[index]["CD_SL"];
                            this._flexD["NM_SL"] = 수주데이터.Rows[index]["NM_SL"];
                            this._flexD["CD_EXCH"] = this.cbo환정보.SelectedValue.ToString();
                            this._flexD["DC1"] = 수주데이터.Rows[index]["DC_RMK"];
                            if (Global.MainFrame.ServerKeyCommon.Contains("TEK"))
                                this._flexD["NUM_USERDEF1"] = (D.GetDecimal(수주데이터.Rows[index]["SOL_NUM_USERDEF3"]) * D.GetDecimal(수주데이터.Rows[index]["QT_REM"]));
                            this.SetCC(this._flexD.Row, string.Empty);
                            foreach (DataRow row in ((DataTable)this.cbo환정보.DataSource).Rows)
                            {
                                if (row["CODE"].ToString() == this.cbo환정보.SelectedValue.ToString())
                                {
                                    this._flexD["NM_EXCH"] = row["NAME"];
                                    break;
                                }
                            }
                            if (this._flexD.CDecimal(this._flexD["RT_PO"]) == 0M)
                                this._flexD["RT_PO"] = 1;
                            this._flexD["QT_PO_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(수주데이터.Rows[index]["QT_REM"]));
                            this._flexD["QT_PO"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(this._flexD["QT_PO_MM"]) * D.GetDecimal(this._flexD["RT_PO"]));
                            this._flexD["NM_GRPMFG"] = 수주데이터.Rows[index]["NM_GRPMFG"];
                            this._flexD["GRP_MFG"] = 수주데이터.Rows[index]["GRP_MFG"];
                            this._flexD["NM_ITEMGRP"] = 수주데이터.Rows[index]["GRP_ITEMNM"];
                            this._flexD["GRP_ITEM"] = 수주데이터.Rows[index]["GRP_ITEM"];
                            this._flexD["NO_RELATION"] = 수주데이터.Rows[index]["NO_RELATION"];
                            this._flexD["SEQ_RELATION"] = 수주데이터.Rows[index]["SEQ_RELATION"];
                            if (pPuPoSoSub.요청비고체크)
                            {
                                this._flexD["DC1"] = 수주데이터.Rows[index]["DC1"];
                                this._flexD["DC2"] = 수주데이터.Rows[index]["DC2"];
                            }
                            if (MA.ServerKey(false, new string[] { "TSUBAKI" }))
                            {
                                this._flexD["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(수주데이터.Rows[index]["UM_EX_PO"]));
                                this._flexD["AM_EX"] = !(D.GetString(수주데이터.Rows[index]["QT_SO"]) == D.GetString(수주데이터.Rows[index]["QT_REM"])) ? Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flexD["QT_PO_MM"]) * D.GetDecimal(this._flexD["UM_EX_PO"])) : Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(수주데이터.Rows[index]["AM_EX_PO"]));
                            }
                            this.FillPol(this._flexD.Row);
                            if (!Global.MainFrame.ServerKeyCommon.Contains("DAIKIN"))
                            {
                                object[] m_obj = new object[] { this._flexD["CD_ITEM"].ToString(),
                                                                this._flexD["CD_PLANT"].ToString(),
                                                                this.LoginInfo.CompanyCode,
                                                                this.cbo단가유형.SelectedValue.ToString(),
                                                                this.cbo환정보.SelectedValue.ToString(),
                                                                this.dtp발주일자.Text,
                                                                this.ctx거래처.CodeValue,
                                                                this.ctx구매그룹.CodeValue,
                                                                "N",
                                                                D.GetString(this._flexD["CD_PJT"]),
                                                                Global.MainFrame.ServerKeyCommon.ToUpper() };
                                if (MA.ServerKey(false, new string[] { "TSUBAKI" }))
                                {
                                    this.품목정보구하기(m_obj, "SO", 0);
                                    this.부가세계산_쯔바키(this._flexD.GetDataRow(this._flexD.Row));
                                }
                                else
                                {
                                    this.품목정보구하기(m_obj, "요청", 0);
                                    this.부가세계산(this._flexD.GetDataRow(this._flexD.Row));
                                }
                            }
                            else
                            {
                                string[] str = new string[] { D.GetString(수주데이터.Rows[index]["CD_PARTNER"]),
                                                              this.ctx거래처.CodeValue,
                                                              D.GetString(수주데이터.Rows[index]["GI_PARTNER"]),
                                                              D.GetString(this.cbo단가유형.SelectedValue),
                                                              D.GetString(수주데이터.Rows[index]["CD_EXCH"]),
                                                              D.GetString(this.cbo환정보.SelectedValue),
                                                              D.GetString(수주데이터.Rows[index]["CD_ITEM"]),
                                                              this.dtp발주일자.Text };
                                this._flexD["GI_PARTNER"] = 수주데이터.Rows[index]["GI_PARTNER"];
                                this._flexD["LN_PARTNER"] = 수주데이터.Rows[index]["LN_PARTNER_GI"];
                                this._flexD["STND_DETAIL_ITEM"] = 수주데이터.Rows[index]["STND_DETAIL_ITEM"];
                                this._flexD["NO_MODEL"] = 수주데이터.Rows[index]["NO_MODEL"];
                                this._flexD["NM_MAKER"] = 수주데이터.Rows[index]["NM_MAKER"];
                                this._flexD["MAT_ITEM"] = 수주데이터.Rows[index]["MAT_ITEM"];
                                this._flexD["NO_DESIGN"] = 수주데이터.Rows[index]["NO_DESIGN"];
                                this._flexD["EN_ITEM"] = 수주데이터.Rows[index]["EN_ITEM"];
                                this._flexD["FG_SERNO"] = 수주데이터.Rows[index]["FG_SERNO"];
                                this._flexD["RATE_VAT"] = this.cur부가세율.DecimalValue;
                                this._flexD["UM_EX_PO"] = this._biz.GET_DKC_UM(str);
                                this.부가세계산(this._flexD.GetDataRow(this._flexD.Row));
                            }
                            if (Global.MainFrame.ServerKey == "SINJINSM")
                            {
                                DataTable dataTable = this._biz.Check_ITEMGRP_UM(D.GetString(this.ctx거래처.CodeValue));
                                this._flexD["CLS_L"] = 수주데이터.Rows[index]["CLS_L"];
                                this._flexD["CLS_M"] = 수주데이터.Rows[index]["CLS_M"];
                                this._flexD["CLS_S"] = 수주데이터.Rows[index]["CLS_S"];
                                this._flexD["NM_CLS_L"] = 수주데이터.Rows[index]["NM_CLS_L"];
                                this._flexD["NM_CLS_M"] = 수주데이터.Rows[index]["NM_CLS_M"];
                                this._flexD["NM_CLS_S"] = 수주데이터.Rows[index]["NM_CLS_S"];
                                this._flexD["QT_PO_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(수주데이터.Rows[index]["QT_REM"]));
                                this._flexD["QT_PO"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(this._flexD["QT_PO_MM"]) * D.GetDecimal(this._flexD["RT_PO"]));
                                decimal num = D.GetDecimal(수주데이터.Rows[index]["UM"]);
                                if (dataTable != null && dataTable.Rows.Count > 0)
                                {
                                    DataRow[] dataRowArray1 = null;
                                    if (D.GetString(this._flexD["CLS_L"]) != "" && D.GetString(this._flexD["GRP_ITEM"]) != "")
                                        dataRowArray1 = dataTable.Select("CD_PARTNER = '" + D.GetString(this.ctx거래처.CodeValue) + "' AND CLS_L ='" + D.GetString(this._flexD["CLS_L"]) + "' AND GRP_ITEM ='" + D.GetString(this._flexD["GRP_ITEM"]) + "'");
                                    if (dataRowArray1 != null && dataRowArray1.Length > 0)
                                    {
                                        num = D.GetDecimal(수주데이터.Rows[index]["UM"]) * (D.GetDecimal(dataRowArray1[0]["UM_RT_ETC_1"]) * 0.01M);
                                    }
                                    else
                                    {
                                        DataRow[] dataRowArray2 = dataTable.Select("CD_PARTNER = '" + D.GetString(this.ctx거래처.CodeValue) + "' AND CLS_L ='" + D.GetString(this._flexD["CLS_L"]) + "'");
                                        if (dataRowArray2 != null && dataRowArray2.Length > 0)
                                        {
                                            num = D.GetDecimal(수주데이터.Rows[index]["UM"]) * D.GetDecimal(dataRowArray2[0]["UM_RT_ETC_1"]) * 0.01M;
                                        }
                                        else
                                        {
                                            DataRow[] dataRowArray3 = dataTable.Select("CD_PARTNER = '" + D.GetString(this.ctx거래처.CodeValue) + "'");
                                            num = dataRowArray3 == null || dataRowArray3.Length <= 0 ? D.GetDecimal(수주데이터.Rows[index]["UM"]) : D.GetDecimal(수주데이터.Rows[index]["UM"]) * (D.GetDecimal(dataRowArray3[0]["UM_RT_ETC_1"]) * 0.01M);
                                        }
                                    }
                                    if (num != D.GetDecimal(수주데이터.Rows[index]["UM"]))
                                        num = Math.Truncate(num / 10M) * 10M;
                                }
                                if (D.GetString(수주데이터.Rows[index]["NO_RELATION"]) != string.Empty && D.GetString(수주데이터.Rows[index]["NO_RELATION"]).Substring(0, 3) == "PEO")
                                {
                                    DataTable sinjinsmUmBonsaEm = this._biz.get_SINJINSM_UM_BONSA_EM(D.GetString(수주데이터.Rows[index]["NO_RELATION"]), D.GetString(수주데이터.Rows[index]["SEQ_RELATION"]));
                                    if (sinjinsmUmBonsaEm != null && sinjinsmUmBonsaEm.Rows.Count > 0)
                                        num = D.GetDecimal(sinjinsmUmBonsaEm.Rows[0]["UM_BONSA_EM"]);
                                }
                                this._flexD["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, num * (D.GetDecimal(수주데이터.Rows[index]["RT_PO"]) == 0M ? 1M : D.GetDecimal(수주데이터.Rows[index]["RT_PO"])));
                                this._flexD["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, num);
                                this._flexD["UM_P"] = Unit.원화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(this._flexD["UM_EX_PO"]) * this.cur환정보.DecimalValue);
                                this._flexD["UM"] = Unit.원화단가(DataDictionaryTypes.PU, num * this.cur환정보.DecimalValue);
                                this._flexD["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flexD["UM_EX_PO"]) * this._flexD.CDecimal(this._flexD["QT_PO_MM"]));
                                this._flexD["AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flexD["AM_EX"]) * this.cur환정보.DecimalValue);
                                this.부가세만계산();
                            }
                            if (Global.MainFrame.ServerKeyCommon == "MHIK")
                            {
                                this._header.CurrentRow["DC50_PO"] = 수주데이터.Rows[index]["DC_RMK"];
                                this._header.CurrentRow["DC_RMK2"] = 수주데이터.Rows[index]["DC_RMK1"];
                                this.txt비고.Text = D.GetString(수주데이터.Rows[index]["DC_RMK"]);
                                this.txt비고2.Text = D.GetString(수주데이터.Rows[index]["DC_RMK1"]);
                                this._flexD["DC2"] = 수주데이터.Rows[index]["DC1"];
                                this._flexD["DC3"] = 수주데이터.Rows[index]["DC2"];
                            }
                            if (Global.MainFrame.ServerKeyCommon == "HIOKI")
                            {
                                this._flexD["CD_USERDEF1"] = 수주데이터.Rows[index]["TXT_USERDEF1"];
                                this._flexD["CD_USERDEF2"] = 수주데이터.Rows[index]["TXT_USERDEF2"];
                                this._flexD["NM_USERDEF3_PO"] = 수주데이터.Rows[index]["TXT_USERDEF3"];
                                this._flexD["DC50_PO"] = 수주데이터.Rows[index]["TXT_USERDEF4"];
                                this._flexD["NM_USERDEF1"] = 수주데이터.Rows[index]["TXT_USERDEF5"];
                                this._flexD["NM_USERDEF2"] = 수주데이터.Rows[index]["TXT_USERDEF6"];
                                this._flexD["TXT_USERDEF1"] = 수주데이터.Rows[index]["TXT_USERDEF7"];
                                this._flexD["TXT_USERDEF2"] = 수주데이터.Rows[index]["TXT_USERDEF8"];
                                this._flexD["DC4"] = 수주데이터.Rows[index]["TXT_USERDEF9"];
                                this._flexD["NM_USERDEF4_PO"] = 수주데이터.Rows[index]["TXT_USERDEF10"];
                                this._flexD["NM_USERDEF5"] = 수주데이터.Rows[index]["TXT_USERDEF11"];
                                this._flexD["GI_PARTNER"] = 수주데이터.Rows[index]["GI_PARTNER"];
                                this._flexD["LN_PARTNER"] = 수주데이터.Rows[index]["LN_PARTNER_GI"];
                                this._flexD["CD_PARTNER_SO"] = 수주데이터.Rows[index]["CD_PARTNER"];
                                this._flexD["LN_PARTNER_SO"] = 수주데이터.Rows[index]["LN_PARTNER"];
                            }
                            if (MA.ServerKey(false, new string[] { "DAOU" }))
                            {
                                this._flexD["CD_USERDEF1"] = 수주데이터.Rows[index]["TXT_USERDEF6"];
                                this._flexD["CD_USERDEF2"] = 수주데이터.Rows[index]["CD_PARTNER"];
                                this._flexD["NM_USERDEF1"] = 수주데이터.Rows[index]["NM_ENDUSER"];
                                this._flexD["NM_USERDEF2"] = 수주데이터.Rows[index]["LN_PARTNER"];
                                this._flexD["CD_CC"] = 수주데이터.Rows[index]["CD_CC"];
                                this._flexD["NM_CC"] = 수주데이터.Rows[index]["NM_CC"];
                            }
                            this._flexD.AddFinished();
                            this._flexD.Col = this._flexD.Cols.Fixed;
                        }
                    }
                    this.SUMFunction();
                    this.SetHeadControlEnabled(false, 1);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this._flexD.Redraw = true;
                this.mDataArea.Enabled = true;
                this.flowLayoutPanel1.Enabled = true;
            }
        }


        private void APP_ANJUN()
        {
            P_PU_Z_ANJUN_MS_SUB pPuZAnjunMsSub = new P_PU_Z_ANJUN_MS_SUB(new object[] { D.GetString(this.cbo공장.SelectedValue) });
            if (((Form)pPuZAnjunMsSub).ShowDialog() != DialogResult.OK)
                return;
            DataTable rtnTable = pPuZAnjunMsSub.RtnTable;
            if (rtnTable == null || rtnTable.Rows.Count <= 0)
                return;
            this._flexD.Redraw = false;
            for (int index = 0; index < rtnTable.Rows.Count; ++index)
            {
                this.추가_Click(null, null);
                this._flexD["CD_ITEM"] = rtnTable.Rows[index]["CD_ITEM"];
                this._flexD["NM_ITEM"] = rtnTable.Rows[index]["NM_ITEM"];
                this._flexD["STND_ITEM"] = rtnTable.Rows[index]["STND_ITEM"];
                this._flexD["STND_DETAIL_ITEM"] = rtnTable.Rows[index]["STND_DETAIL_ITEM"];
                this._flexD["STND_MA_ITEM"] = rtnTable.Rows[index]["STND_ITEM"];
                this._flexD["CD_UNIT_MM"] = rtnTable.Rows[index]["UNIT_PO"];
                this._flexD["UNIT_PO"] = rtnTable.Rows[index]["UNIT_PO"];
                this._flexD["UNIT_IM"] = rtnTable.Rows[index]["UNIT_IM"];
                this._flexD["RT_PO"] = rtnTable.Rows[index]["RT_PO"];
                this._flexD["NM_ITEMGRP"] = rtnTable.Rows[index]["NM_ITEMGRP"];
                this._flexD["NM_GRPMFG"] = rtnTable.Rows[index]["NM_GRPMFG"];
                this._flexD["NM_CLS_ITEM"] = rtnTable.Rows[index]["NM_CLS_ITEM"];
                this._flexD["NO_MODEL"] = rtnTable.Rows[index]["NO_MODEL"];
                this._flexD["FG_IQCL"] = rtnTable.Rows[index]["FG_IQCL"];
                this._flexD["WEIGHT"] = rtnTable.Rows[index]["WEIGHT"];
                this._flexD["NM_MAKER"] = rtnTable.Rows[index]["NM_MAKER"];
                this._flexD["RATE_VAT"] = this.cur부가세율.DecimalValue;
                this._flexD["CD_USERDEF1"] = rtnTable.Rows[index]["STND_YYMM"];
                this._flexD["CD_ITEMGRP"] = rtnTable.Rows[index]["CD_ITEMGRP"];
                this._flexD["MAT_ITEM"] = rtnTable.Rows[index]["MAT_ITEM"];
                this._flexD["NO_DESIGN"] = rtnTable.Rows[index]["NO_DESIGN"];
                this._flexD["QT_PO"] = rtnTable.Rows[index]["QT12"];
                if (D.GetDecimal(this._flexD["RT_PO"]) == 0M)
                    this._flexD["RT_PO"] = 1;
                this._flexD["QT_PO_MM"] = (D.GetDecimal(rtnTable.Rows[index]["QT12"]) / D.GetDecimal(this._flexD["RT_PO"]));
                if (this.txt발주번호.Text != string.Empty)
                    this._flexD["NO_PO"] = this.txt발주번호.Text;
                this.SetCC(this._flexD.Row, string.Empty);
                if (this.sPUSU == "100")
                    this.GET_SU_BOM();
            }
            this.SetHeadControlEnabled(false, 1);
        }

        private void APP_KPIC()
        {
            try
            {
                P_PU_Z_KPCI_CONTRACT_SUB zKpciContractSub = new P_PU_Z_KPCI_CONTRACT_SUB();
                if (((Form)zKpciContractSub).ShowDialog() == DialogResult.OK)
                {
                    DataTable rtnData = zKpciContractSub.rtnDATA;
                    if (rtnData == null || rtnData.Rows.Count <= 0 || !this.Tppo_Accept(rtnData, "전용"))
                        return;
                    this._flexD.Redraw = false;
                    this._header.CurrentRow["CD_PARTNER"] = rtnData.Rows[0]["CD_PARTNER"];
                    this._header.CurrentRow["LN_PARTNER"] = rtnData.Rows[0]["LN_PARTNER"];
                    this.ctx거래처.CodeValue = D.GetString(rtnData.Rows[0]["CD_PARTNER"]);
                    this.ctx거래처.CodeName = D.GetString(rtnData.Rows[0]["LN_PARTNER"]);
                    this._header.CurrentRow["CD_PURGRP"] = rtnData.Rows[0]["CD_PURGRP"];
                    this._header.CurrentRow["NM_PURGRP"] = rtnData.Rows[0]["NM_PURGRP"];
                    this.ctx구매그룹.CodeValue = D.GetString(rtnData.Rows[0]["CD_PURGRP"]);
                    this.ctx구매그룹.CodeName = D.GetString(rtnData.Rows[0]["NM_PURGRP"]);
                    this.cbo환정보.SelectedValue = D.GetString(rtnData.Rows[0]["CD_EXCH"]);
                    this._header.CurrentRow["CD_EXCH"] = rtnData.Rows[0]["CD_EXCH"];
                    (this.txt매입비고).Text = D.GetString(rtnData.Rows[0]["NO_ORDER"]);
                    this.txt발주번호.Text = D.GetString(rtnData.Rows[0]["NO_ORDER"]);
                    this._header.CurrentRow["NO_PO"] = D.GetString(rtnData.Rows[0]["NO_ORDER"]);
                    this.txt오더번호.Text = D.GetString(rtnData.Rows[0]["NO_ORDER"]);
                    this._header.CurrentRow["NO_ORDER"] = D.GetString(rtnData.Rows[0]["NO_ORDER"]);
                    DataTable dataTable1 = this._biz.search_partner(this.ctx거래처.CodeValue);
                    if (dataTable1 != null && dataTable1.Rows.Count > 0)
                    {
                        if (D.GetString(dataTable1.Rows[0]["FG_PAYBILL"]) != string.Empty)
                            this.cbo지급구분.SelectedValue = D.GetString(dataTable1.Rows[0]["FG_PAYBILL"]);
                        this._header.CurrentRow["FG_PAYBILL_IV"] = D.GetString(dataTable1.Rows[0]["FG_PAYBILL"]);
                    }
                    if (!this.HeaderCheck(0))
                        return;
                    for (int index = 0; index < rtnData.Rows.Count; ++index)
                    {
                        this.추가_Click(null, null);
                        this._flexD["CD_ITEM"] = rtnData.Rows[index]["CD_ITEM"];
                        this._flexD["NM_ITEM"] = rtnData.Rows[index]["NM_ITEM"];
                        this._flexD["STND_ITEM"] = rtnData.Rows[index]["STND_ITEM"];
                        this._flexD["STND_DETAIL_ITEM"] = rtnData.Rows[index]["STND_DETAIL_ITEM"];
                        this._flexD["STND_MA_ITEM"] = rtnData.Rows[index]["STND_ITEM"];
                        this._flexD["CD_UNIT_MM"] = rtnData.Rows[index]["UNIT_PO"];
                        this._flexD["UNIT_PO"] = rtnData.Rows[index]["UNIT_PO"];
                        this._flexD["UNIT_IM"] = rtnData.Rows[index]["UNIT_IM"];
                        this._flexD["RT_PO"] = rtnData.Rows[index]["RT_PO"];
                        this._flexD["NM_ITEMGRP"] = rtnData.Rows[index]["NM_ITEMGRP"];
                        this._flexD["NM_GRPMFG"] = rtnData.Rows[index]["NM_GRPMFG"];
                        this._flexD["NM_CLS_ITEM"] = rtnData.Rows[index]["NM_CLS_ITEM"];
                        this._flexD["NO_MODEL"] = rtnData.Rows[index]["NO_MODEL"];
                        this._flexD["FG_IQCL"] = rtnData.Rows[index]["FG_IQCL"];
                        this._flexD["WEIGHT"] = rtnData.Rows[index]["WEIGHT"];
                        this._flexD["NM_MAKER"] = rtnData.Rows[index]["NM_MAKER"];
                        this._flexD["RATE_VAT"] = this.cur부가세율.DecimalValue;
                        this._flexD["CD_ITEMGRP"] = rtnData.Rows[index]["CD_ITEMGRP"];
                        this._flexD["MAT_ITEM"] = rtnData.Rows[index]["MAT_ITEM"];
                        this._flexD["NO_DESIGN"] = rtnData.Rows[index]["NO_DESIGN"];
                        decimal num = D.GetDecimal(rtnData.Rows[index]["RT_PO"]) == 0M ? 1M : D.GetDecimal(rtnData.Rows[index]["RT_PO"]);
                        this._flexD["QT_PO"] = (D.GetDecimal(rtnData.Rows[index]["QT_CON"]) * num);
                        this._flexD["AM_EX"] = D.GetDecimal(rtnData.Rows[index]["AM_AMOUNT_BUY"]);
                        this._flexD["DC1"] = rtnData.Rows[index]["NO_CONTRACT"];
                        this._flexD["DC2"] = rtnData.Rows[index]["NO_ORDER"];
                        this._flexD["NUM_USERDEF1"] = rtnData.Rows[index]["NO_HST"];
                        this._flexD["AM"] = (D.GetDecimal(this._flexD["AM_EX"]) * this.cur환정보.DecimalValue);
                        this._flexD["NO_RELATION"] = rtnData.Rows[index]["NO_CONTRACT"];
                        this._flexD["SEQ_RELATION"] = rtnData.Rows[index]["NO_LINE"];
                        this._flexD["CD_SL"] = rtnData.Rows[index]["CD_SL"];
                        this._flexD["NM_SL"] = rtnData.Rows[index]["NM_SL"];
                        this._flexD["FG_SERNO"] = rtnData.Rows[index]["FG_SERNO"];
                        this.부가세만계산();
                        if (D.GetDecimal(this._flexD["RT_PO"]) == 0M)
                            this._flexD["RT_PO"] = 1;
                        this._flexD["QT_PO_MM"] = D.GetDecimal(rtnData.Rows[index]["QT_CON"]);
                        if (this.txt발주번호.Text != string.Empty)
                            this._flexD["NO_PO"] = this.txt발주번호.Text;
                        this._flexD["UM_EX_PO"] = rtnData.Rows[index]["UM_UNIT_BUY"];
                        this._flexD["UM_EX"] = (D.GetDecimal(rtnData.Rows[index]["UM_UNIT_BUY"]) * this._flexD.CDecimal(this._flexD["QT_PO"]));
                        this._flexD["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(rtnData.Rows[index]["UM_UNIT_BUY"]) * this.cur환정보.DecimalValue);
                        this._flexD["UM_P"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(rtnData.Rows[index]["UM_UNIT_BUY"]) * this._flexD.CDecimal(this._flexD["QT_PO"]) * this.cur환정보.DecimalValue);
                        this.SetCC(this._flexD.Row, string.Empty);
                        if (this.sPUSU == "100")
                            this.GET_SU_BOM();
                    }
                  this.cbo환정보.SelectedValue = D.GetString(rtnData.Rows[0]["CD_EXCH"]);
                    this._header.CurrentRow["CD_EXCH"] = rtnData.Rows[0]["CD_EXCH"];
                    if (D.GetString(this.cbo환정보.SelectedValue) == "000")
                    {
                        this.cur환정보.DecimalValue = 1M;
                        this._header.CurrentRow["RT_EXCH"] = 1;
                        this.cur환정보.Enabled = false;
                    }
                    else
                    {
                        decimal num = MA.기준환율적용(this.dtp발주일자.Text, D.GetString(this.cbo환정보.SelectedValue.ToString()));
                        if (num == 0M)
                            num = 1M;
                        this.cur환정보.Text = num.ToString();
                        this._header.CurrentRow["RT_EXCH"] = num;
                        this.cur환정보.Enabled = false;
                    }
                    DataTable dataTable2 = this._biz.search_dt(this.dtp발주일자.Text.Replace('/', ' '));
                    if (dataTable2 != null && dataTable2.Rows.Count > 0)
                    {
                        this.dtp만기일자.Text = D.GetString(dataTable2.Rows[6]["DT_CAL"]);
                        this.dtp지급예정일자.Text = D.GetString(dataTable2.Rows[6]["DT_CAL"]);
                    }
                    this.SetExchageMoney();
                    this.SetHeadControlEnabled(false, 1);
                    this.btn_INST.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void APP_KAHPDEV(string strYN)
        {
            try
            {
                if (this.txt발주번호.Text != "")
                {
                    if (strYN == "Y")
                    {
                        if (D.GetString(this._header.CurrentRow["TXT_USERDEF4"]) != "")
                        {
                            this.ShowMessage("이미 전표처리된 건이 포함되어 있습니다");
                        }
                        else
                        {
                            if (this.ShowMessage("전표처리를 하시겠습니까?", "QY2") != DialogResult.Yes || !this._biz.미결전표처리(this.txt발주번호.Text))
                                return;
                            this.ShowMessage("전표가 처리되었습니다");
                            this.Reload(this.txt발주번호.Text);
                        }
                    }
                    else if (D.GetString(this._header.CurrentRow["TXT_USERDEF4"]) == "")
                    {
                        this.ShowMessage("이미 전표취소된 건이 포함되어 있습니다");
                    }
                    else
                    {
                        if (this.ShowMessage("전표를 취소 하시겠습니까?", "QY2") != DialogResult.Yes || !this._biz.미결전표취소처리(this.txt발주번호.Text, D.GetString(this._header.CurrentRow["TXT_USERDEF4"])))
                            return;
                        this.ShowMessage("전표가 취소되었습니다");
                        this.Reload(this.txt발주번호.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void APP_DONGWOON()
        {
            try
            {
                if (!this.HeaderCheck(0))
                    return;
                P_PU_Z_DONGWOON_PUSU_GR_SUB dongwoonPusuGrSub = new P_PU_Z_DONGWOON_PUSU_GR_SUB(new object[] { this.ctx거래처.CodeValue,
                                                                                                               this.ctx거래처.CodeName });
                if (((Form)dongwoonPusuGrSub).ShowDialog() == DialogResult.OK)
                {
                    this.ControlButtonEnabledDisable(this.btnCompanyUse1, true);
                    this.cbo공장.Enabled = false;
                    this._flexD.Redraw = false;
                    decimal maxValue = this._flexD.GetMaxValue("NO_LINE");
                    DataTable dtReutrn = dongwoonPusuGrSub.dt_reutrn;
                    DataTable dtReutrnLot = dongwoonPusuGrSub.dt_reutrnLOT;
                    for (int index = 0; index < dtReutrn.Rows.Count; ++index)
                    {
                        if (dtReutrn.Rows[index].RowState != DataRowState.Deleted)
                        {
                            ++maxValue;
                            this._flexD.Rows.Add();
                            this._flexD.Row = this._flexD.Rows.Count - 1;
                            this._flexD["CD_ITEM"] = dtReutrn.Rows[index]["CD_ITEM"];
                            this._flexD["NM_ITEM"] = dtReutrn.Rows[index]["NM_ITEM"];
                            this._flexD["STND_ITEM"] = dtReutrn.Rows[index]["STND_ITEM"];
                            this._flexD["CD_UNIT_MM"] = dtReutrn.Rows[index]["UNIT_PO"];
                            this._flexD["UNIT_PO"] = dtReutrn.Rows[index]["UNIT_PO"];
                            this._flexD["STND_MA_ITEM"] = dtReutrn.Rows[index]["STND_ITEM"];
                            this._flexD["UNIT_IM"] = dtReutrn.Rows[index]["UNIT_IM"];
                            this._flexD["GRP_MFG"] = dtReutrn.Rows[index]["GRP_MFG"];
                            this._flexD["NM_GRPMFG"] = dtReutrn.Rows[index]["NM_GRPMFG"];
                            this._flexD["NO_PR"] = "";
                            this._flexD["NO_PRLINE"] = 0;
                            this._flexD["DT_LIMIT"] = this.dtp납기일.Text;
                            this._flexD["DT_PLAN"] = this.dtp납기일.Text;
                            this._flexD["FG_POST"] = "O";
                            this._flexD["NM_SYSDEF"] = this._biz.GetGubunCodeSearch("PU_C000009", this._flexD["FG_POST"].ToString());
                            this._flexD["QT_PO"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(dtReutrn.Rows[index]["QT_PO"]));
                            this._flexD["NUM_USERDEF1"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(dtReutrn.Rows[index]["QT_BOM"]));
                            this._flexD["RT_PO"] = dtReutrn.Rows[index]["RT_PO"];
                            if (this._flexD.CDecimal(this._flexD["RT_PO"]) == 0M)
                                this._flexD["RT_PO"] = 1;
                            this._flexD["QT_PO_MM"] = Unit.수량(DataDictionaryTypes.PU, this._flexD.CDecimal(dtReutrn.Rows[index]["QT_PO"]) / this._flexD.CDecimal(this._flexD["RT_PO"]));
                            this._flexD["NO_RELATION"] = dtReutrn.Rows[index]["NO_IO"];
                            this._flexD["SEQ_RELATION"] = dtReutrn.Rows[index]["NO_IOLINE"];
                            this._flexD["CD_PLANT"] = dtReutrn.Rows[index]["CD_PLANT"];
                            if (this.txt발주번호.Text != string.Empty)
                                this._flexD["NO_PO"] = this.txt발주번호.Text;
                            this._flexD["NO_LINE"] = maxValue;
                            this._flexD["CD_EXCH"] = this.cbo환정보.SelectedValue.ToString();
                            this.SetCC(this._flexD.Row, string.Empty);
                            foreach (DataRow row in ((DataTable)this.cbo환정보.DataSource).Rows)
                            {
                                if (row["CODE"].ToString() == this.cbo환정보.SelectedValue.ToString())
                                {
                                    this._flexD["NM_EXCH"] = row["NAME"];
                                    break;
                                }
                            }
                            this._flexD["NM_CLS_ITEM"] = D.GetString(dtReutrn.Rows[index]["NM_CLS_ITEM"]);
                            this._flexD["PI_PARTNER"] = dtReutrn.Rows[index]["PARTNER"];
                            this._flexD["PI_LN_PARTNER"] = dtReutrn.Rows[index]["PARTNER_NM"];
                            this.FillPol(this._flexD.Row);
                            this.품목정보구하기(new object[] { this._flexD["CD_ITEM"].ToString(),
                                                               this._flexD["CD_PLANT"].ToString(),
                                                               this.LoginInfo.CompanyCode,
                                                               this.cbo단가유형.SelectedValue.ToString(),
                                                               this.cbo환정보.SelectedValue.ToString(),
                                                               this.dtp발주일자.Text,
                                                               this.ctx거래처.CodeValue,
                                                               this.ctx구매그룹.CodeValue,
                                                               "N",
                                                               D.GetString(this._flexD["CD_PJT"]),
                                                               Global.MainFrame.ServerKeyCommon.ToUpper() }, "USE", 0);
                            this.부가세계산(this._flexD.GetDataRow(this._flexD.Row));
                            DataRow[] dataRowArray = dtReutrnLot.Select("NO_IO = '" + D.GetString(dtReutrn.Rows[index]["NO_IO"]) + "' AND NO_IOLINE = '" + D.GetString(dtReutrn.Rows[index]["NO_IOLINE"]) + "'");
                            DataTable ynSu = this._biz.GetYN_SU(D.GetString(this._header.CurrentRow["CD_TPPO"]));
                            if (dataRowArray != null && dataRowArray.Length > 0 && D.GetString(ynSu.Rows[0]["YN_SU"]) == "Y")
                            {
                                int num = 0;
                                foreach (DataRow dataRow in dataRowArray)
                                {
                                    this._flexDD.Rows.Add();
                                    this._flexDD.Row = (this._flexDD.Rows).Count - 1;
                                    this._flexDD["CD_MATL"] = dataRow["CD_ITEM"];
                                    this._flexDD["NM_ITEM"] = dataRow["NM_ITEM"];
                                    this._flexDD["STND_ITEM"] = dataRow["STND_ITEM"];
                                    this._flexDD["STND_DETAIL_ITEM"] = dataRow["STND_DETAIL_ITEM"];
                                    this._flexDD["UNIT_MO"] = dataRow["UNIT_IM"];
                                    this._flexDD["QT_NEED_UNIT"] = dataRow["QT_APP"];
                                    this._flexDD["QT_NEED"] = dataRow["QT_APP"];
                                    this._flexDD["CD_PLANT"] = dataRow["CD_PLANT"];
                                    this._flexDD["NO_RELATION"] = dataRow["NO_IO"];
                                    this._flexDD["SEQ_RELATION"] = dataRow["NO_IOLINE"];
                                    this._flexDD["TXT_USERDEF1"] = dataRow["NO_LOT"];
                                    this._flexDD["NUM_USERDEF1"] = dataRow["NO_IOLINE2"];
                                    this._flexDD["TXT_USERDEF2"] = dataRow["CD_MNG1_WW"];
                                    this._flexDD["TXT_USERDEF3"] = dataRow["CD_MNG2_SC"];
                                    this._flexDD["TXT_USERDEF4"] = dataRow["CD_MNG4_ASSY"];
                                    this._flexDD["TXT_USERDEF5"] = dataRow["CD_MNG5_LOTRMK"];
                                    this._flexDD["NUM_USERDEF2"] = dataRow["CD_MNG3_PCS"];
                                    this._flexDD["QT_PO"] = this._flexD["QT_PO"];
                                    this._flexDD["NO_PO"] = this._flexD["NO_PO"];
                                    this._flexDD["NO_POLINE"] = maxValue;
                                    this._flexDD["NO_LINE"] = ++num;
                                    this._flexDD.AddFinished();
                                    this._flexDD.Col = (this._flexDD.Cols).Fixed;
                                }
                            }
                            this._flexD.AddFinished();
                            this._flexD.Col = this._flexD.Cols.Fixed;
                        }
                    }
                    this.SUMFunction();
                    this.SetHeadControlEnabled(false, 1);
                }
              this._flexD.Redraw = true;
            }
            catch (Exception ex)
            {
                this._flexD.Redraw = true;
                this.MsgEnd(ex);
            }
        }

        private void APP_ESTMT()
        {
            try
            {
                if (!this.HeaderCheck(0))
                    return;
                P_PU_Z_JONGHAP_ESTMT_SUB zJonghapEstmtSub = new P_PU_Z_JONGHAP_ESTMT_SUB();
                if (((Form)zJonghapEstmtSub).ShowDialog() != DialogResult.OK)
                    return;
                this.ControlButtonEnabledDisable(this.btnCompanyUse1, true);
                this.cbo공장.Enabled = false;
                this._flexD.Redraw = false;
                decimal maxValue = this._flexD.GetMaxValue("NO_LINE");
                DataTable rtnData = zJonghapEstmtSub.rtnDATA;
                DataTable userColumnInfo = Config.UserColumnSetting.GetUSerColumnInfo(this.PageID, "P_PU_Z_JONGHAP_ESTMT_SUB");
                for (int index = 0; index < rtnData.Rows.Count; ++index)
                {
                    ++maxValue;
                    this._flexD.Rows.Add();
                    this._flexD.Row = this._flexD.Rows.Count - 1;
                    this._flexD["NM_ITEM"] = rtnData.Rows[index]["NM_CLS_L"];
                    this._flexD["NO_PR"] = "";
                    this._flexD["NO_PRLINE"] = 0;
                    this._flexD["DT_LIMIT"] = this.dtp납기일.Text;
                    this._flexD["DT_PLAN"] = this.dtp납기일.Text;
                    this._flexD["FG_POST"] = "O";
                    this._flexD["NM_SYSDEF"] = this._biz.GetGubunCodeSearch("PU_C000009", this._flexD["FG_POST"].ToString());
                    this._flexD["NUM_USERDEF1"] = rtnData.Rows[index]["NO_HST"];
                    this._flexD["RT_PO"] = rtnData.Rows[index]["RT_SO"];
                    this._flexD["NO_RELATION"] = rtnData.Rows[index]["NO_EST"];
                    this._flexD["SEQ_RELATION"] = rtnData.Rows[index]["SEQ_EST"];
                    this._flexD["CD_PLANT"] = rtnData.Rows[index]["CD_PLANT"];
                    this._flexD["CD_UNIT_MM"] = rtnData.Rows[index]["UNIT_SO"];
                    this._flexD["UNIT_IM"] = rtnData.Rows[index]["UNIT_IM"];
                    if (this.txt발주번호.Text != string.Empty)
                        this._flexD["NO_PO"] = this.txt발주번호.Text;
                    this._flexD["NO_LINE"] = maxValue;
                    this._flexD["CD_EXCH"] = this.cbo환정보.SelectedValue.ToString();
                    this.SetCC(this._flexD.Row, string.Empty);
                    foreach (DataRow row in ((DataTable)this.cbo환정보.DataSource).Rows)
                    {
                        if (row["CODE"].ToString() == this.cbo환정보.SelectedValue.ToString())
                        {
                            this._flexD["NM_EXCH"] = row["NAME"];
                            break;
                        }
                    }
                    this._flexD["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(rtnData.Rows[index]["UM_EST"]));
                    this._flexD["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(rtnData.Rows[index]["UM_EST_IM"]));
                    this._flexD["UM_P"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(this._flexD["UM_EX_PO"]) * this.cur환정보.DecimalValue);
                    this._flexD["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(this._flexD["UM_EX"]) * this.cur환정보.DecimalValue);
                    if (D.GetDecimal(rtnData.Rows[index]["QT_EST_R"]) == D.GetDecimal(rtnData.Rows[index]["QT_EST"]))
                    {
                        this._flexD["QT_PO"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(rtnData.Rows[index]["QT_EST_IM"]));
                        this._flexD["QT_PO_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(rtnData.Rows[index]["QT_EST"]));
                        this._flexD["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(rtnData.Rows[index]["AM_EST"]));
                        this._flexD["AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(rtnData.Rows[index]["AM_K_EST"]));
                    }
                    else
                    {
                        this._flexD["QT_PO_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(rtnData.Rows[index]["QT_EST_R"]));
                        this._flexD["QT_PO"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(rtnData.Rows[index]["QT_EST_IM_R"]));
                        this._flexD["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flexD["UM_EX_PO"]) * D.GetDecimal(this._flexD["QT_PO_MM"]));
                        this._flexD["AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flexD["UM_EX_PO"]) * D.GetDecimal(this._flexD["QT_PO_MM"]) * this.cur환정보.DecimalValue);
                    }
                    this._flexD["NUM_STND_ITEM_1"] = rtnData.Rows[index]["NUM_STND_ITEM_1"];
                    this._flexD["NUM_STND_ITEM_2"] = rtnData.Rows[index]["NUM_STND_ITEM_2"];
                    this._flexD["NUM_STND_ITEM_3"] = rtnData.Rows[index]["NUM_STND_ITEM_3"];
                    this._flexD["NUM_STND_ITEM_4"] = rtnData.Rows[index]["NUM_STND_ITEM_4"];
                    this._flexD["NUM_STND_ITEM_5"] = rtnData.Rows[index]["NUM_STND_ITEM_5"];
                    this._flexD["CLS_L"] = rtnData.Rows[index]["CLS_L"];
                    this._flexD["NM_CLS_L"] = rtnData.Rows[index]["NM_CLS_L"];
                    this._flexD["RATE_VAT"] = this.cur부가세율.DecimalValue;
                    this._flexD["STND_DETAIL_ITEM"] = rtnData.Rows[index]["NUM_DETAIL_ITEM_STEEL"];
                    this._flexD["MAT_ITEM"] = rtnData.Rows[index]["NUM_MAT_ITEM_STEEL"];
                    this._flexD["CD_PJT"] = D.GetString(rtnData.Rows[index]["NO_PROJECT"]);
                    this._flexD["NM_PJT"] = D.GetString(rtnData.Rows[index]["NM_PROJECT"]);
                    this.FillPol(this._flexD.Row);
                    this.부가세만계산();
                    this._flexD.AddFinished();
                    this._flexD.Col = this._flexD.Cols.Fixed;
                    Config.UserColumnSetting.ApplyUserColumn(userColumnInfo, rtnData.Rows[index], this._flexD, this._flexD.Row);
                }
                this.SUMFunction();
                this.SetHeadControlEnabled(false, 1);
                this._flexD.Redraw = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void APP_SGA()
        {
            try
            {
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn_CM_INV_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.HeaderCheck(0) || !this._flexD.HasNormalRow)
                    return;
                this.품목정보구하기(new object[] { this._flexD["CD_ITEM"].ToString(),
                                                   this._flexD["CD_PLANT"].ToString(),
                                                   this.LoginInfo.CompanyCode,
                                                   this.cbo단가유형.SelectedValue.ToString(),
                                                   this.cbo환정보.SelectedValue.ToString(),
                                                   this.dtp발주일자.Text,
                                                   this.ctx거래처.CodeValue,
                                                   this.ctx구매그룹.CodeValue,
                                                   "N",
                                                   D.GetString(this._flexD["CD_PJT"]),
                                                   Global.MainFrame.ServerKeyCommon.ToUpper() }, "재고", 0);
                this.SetQtValue(this._flexD.Row);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        private string 전자결재양식생성_대림()
        {
            try
            {
                string empty1 = string.Empty;
                string str1 = string.Empty;
                string str2 = this._header.CurrentRow["DT_PO"].ToString();
                string str3 = this._header.CurrentRow["NM_KOR"].ToString();
                string str4 = this._header.CurrentRow["DC50_PO"].ToString();
                string str5 = this._header.CurrentRow["NM_PARTNER"].ToString();
                decimal num1 = 0M;
                string empty2 = string.Empty;
                int num2 = 1;
                string str6 = str2.Substring(0, 4) + "." + str2.Substring(4, 2) + str2.Substring(6, 2);
                foreach (DataRow row in this._flexD.DataTable.Rows)
                {
                    decimal num3 = Convert.ToDecimal(row["QT_PO_MM"]);
                    decimal num4 = Convert.ToDecimal(row["UM_EX_PO"]);
                    decimal num5 = Convert.ToDecimal(row["AM"]);
                    string str7 = row["DT_LIMIT"].ToString().Substring(0, 4) + "." + row["DT_LIMIT"].ToString().Substring(4, 2) + "." + row["DT_LIMIT"].ToString().Substring(6, 2);
                    num1 += num5;
                    string formatDescription = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY, FormatFgType.INSERT);
                    str1 = str1 + "<tr>\r\n                                   <td width='50' height='26' class='border_4'><div align='center'>" + num2.ToString() + "</div></td>\r\n                                   <td width='240' height='26' class='border_4'><div align='left'>" + row["NM_ITEM"].ToString() + "</div></td>\r\n                                   <td width='50' height='26' class='border_4'><div align='center'>" + num3.ToString(formatDescription) + "</div></td>\r\n                                   <td width='90' height='26' class='border_4'><div align='right'>" + num4.ToString("###,###,###,###,##0") + "</div></td>\r\n                                   <td width='110' height='26' class='border_4'><div align='right'>" + num5.ToString("###,###,###,###,##0") + "</div></td>\r\n                                   <td width='110' height='26' class='border_4'><div align='center'>" + row["NM_PJT"].ToString() + "</div></td>\r\n                                   <td width='110' class='border_1'><div align='center'>" + str7 + "</div></td>\r\n                                 </tr>";
                }
                return " <html>      \r\n                           <head>      \r\n                           <title>발주서</title>      \r\n                           <meta http-equiv='Content-Type' content='text/html; charset=euc-kr'>      \r\n                            <style type='text/css'> \r\n                           BODY {font-family:굴림; font-size:12px; color:#000000;}      \r\n                           P {font-family:굴림; font-size:12px; color:#000000;}      \r\n                           td {font-family:굴림; font-size:12px; color:#000000;}      \r\n                        .red {font-family:굴림; font-size:12px; color:#ff0000; text-decoration:none;} \r\n                        .redB {font-family:굴림; font-size:12px; font-weight: bold; color:#ff0000; text-decoration:none;} \r\n                        .redBB {font-family:굴림; font-size:16px; font-weight: bold; color:#ff0000; text-decoration:none;}\r\n                        .blackB0 {font-size:12px; font-weight:bold; }\r\n                        .blackB {font-size:14px; font-weight:bold; }\r\n                        .blackBB {font-size:35px; font-weight:bold; text-decoration:underline; }\r\n                        .blackBBB {font-size:35px; font-weight:bold; text-decoration:none; }\r\n                         \r\n                        .border_1 {\r\n                                  border-left:0px;\r\n\t                              border-top:0px;\r\n\t                              border-right:0px;\r\n                          }\r\n                        .border_2 {\r\n                                  border-left:0px;\r\n\t                              border-top:0px;\r\n\t                              border-bottom:0px;\r\n                          }\r\n                        .border_3 {\r\n                                  border-left:0px;\r\n\t                              border-right:0px;\r\n\t                              border-top:0px;\r\n\t                              border-bottom:0px;\r\n                          }\r\n                        .border_4 {\r\n\t                              border-top:0px;\r\n\t                              border-left:0px;\r\n                          }\r\n                            .style1 {font-size: 12px; font-weight: bold; }\r\n                            </style>   \r\n                           </head>\r\n                                    \r\n                           <body bgcolor='#FFFFFF' text='#000000'>    \r\n                           <!-- 계약체결보고서 -->  \r\n                           <table width='650' border='0' cellspacing='0' cellpadding='0'>      \r\n                             <tr>       \r\n                               <td width='650' height='80'>       \r\n                                 <div align='center' class='blackBBB'>발주서</div>      \r\n                               </td>      \r\n                             </tr>      \r\n                             <tr>       \r\n                               <td width='650' height='10'>&nbsp;</td>      \r\n                             </tr>      \r\n                             <tr>       \r\n                               <td height='25' align='right'><table width='250' border='0' cellpadding='2' cellspacing='0'>\r\n                                 <tr>\r\n                                   <td width='90' height='25'><div align='right'>수주NO : </div></td>\r\n                                   <td width='160' height='25'>" + str6 + "</td>\r\n                                 </tr>\r\n                                 <tr>\r\n                                   <td height='25'><div align='right'>담당 : </div></td>\r\n                                   <td width='160' height='25'>" + str3 + "</td>\r\n                                 </tr>\r\n                                 <tr>\r\n                                   <td height='25' colspan='2'><span class='blackB'>아래 내역에 대해 발주합니다. </span></td>\r\n                                 </tr>\r\n                               </table></td>\r\n                             </tr>      \r\n                             <tr>       \r\n                               <td></td>      \r\n                             </tr>      \r\n                             <tr>       \r\n                               <td></td>      \r\n                             </tr>      \r\n                             <tr>       \r\n                               <td height='26' class='blackB0'>1. 구매품 내역\t   </td>      \r\n                             </tr>\r\n                             <tr>\r\n                               <td height='10'><table width='650' border='1' cellpadding='2' cellspacing='0' bordercolor='#000000'>\r\n                                 <tr bgcolor='#DBF2FD'>\r\n                                   <td width='50' height='30' bgcolor='#DBF2FD' class='border_4'><div align='center'>NO</div></td>\r\n                                   <td width='240' height='30' bgcolor='#DBF2FD' class='border_4'><div align='center'>품명</div></td>\r\n                                   <td width='50' height='30' bgcolor='#DBF2FD' class='border_4'><div align='center'>수량</div></td>\r\n                                   <td width='90' height='30' bgcolor='#DBF2FD' class='border_4'><div align='center'>단가</div></td>\r\n                                   <td width='110' height='30' bgcolor='#DBF2FD' class='border_4'><div align='center'>금액</div></td>\r\n                                   <td width='110' height='30' class='border_4'><div align='center'>수주번호</div></td>\r\n                                   <td width='110' align='center' class='border_1'>납기일</td>\r\n                                 </tr>" + str1 + "<tr bgcolor='#FFFFCC'>\r\n                                   <td height='36' colspan='7' class='border_3'><table width='600' border='0' align='center' cellpadding='0' cellspacing='0'>\r\n                                     <tr>\r\n                                       <td><div align='right' class='blackB'>\r\n                                         <div align='right'>합계 : </div>\r\n                                       </div></td>\r\n                                       <td ><div align='right' class='redBB'>" + num1.ToString("###,###,###,###,##0") + "</div></td>\r\n                                     </tr>\r\n                                   </table></td>\r\n                                 </tr>\r\n                               </table></td>\r\n                             </tr>\r\n                             <tr>\r\n                               <td height='20'><table width='600' border='0' align='left' cellpadding='2' cellspacing='0'>\r\n                                 <tr>\r\n                                   <td width='110' height='25' class='blackB0'><div align='left'>2. 납&nbsp;품&nbsp;&nbsp;장&nbsp;소 : </div></td>\r\n                                   <td width='490' height='25'>" + str4 + " </td>\r\n                                 </tr>\r\n                                 <tr>\r\n                                   <td width='110' height='25' class='blackB0'>3. 구입선 : </td>\r\n                                   <td width='490' height='25'>" + str5 + "</td>\r\n                                 </tr>\r\n                               </table></td>\r\n                             </tr>\r\n                             <tr>\r\n                               <td height='40'>&nbsp;</td>\r\n                             </tr>      \r\n                           </table>\r\n                           </body>      \r\n                           </html>\r\n            ";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            return string.Empty;
        }

        private string 전자결재양식생성_백광()
        {
            try
            {
                string empty = string.Empty;
                string str1 = string.Empty;
                this._header.CurrentRow["DT_PO"].ToString();
                this._header.CurrentRow["NM_KOR"].ToString();
                string str2 = this._header.CurrentRow["DC50_PO"].ToString();
                string cd_partner = this._header.CurrentRow["CD_PARTNER"].ToString();
                string str3 = this._header.CurrentRow["NM_PARTNER"].ToString();
                string str4 = "&nbsp;";
                string str5 = "&nbsp;";
                string str6 = "&nbsp";
                decimal num1 = 0M;
                string str7 = string.Empty;
                string str8 = "&nbsp";
                string str9 = "&nbsp";
                string str10 = this._header.CurrentRow["NO_PO"].ToString().Trim();
                if (str10 == "")
                    str10 = "&nbsp;";
                DataTable dataTable = this._biz.search_partner(cd_partner);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    if (dataTable.Rows[0]["NO_TEL"].ToString().Trim() != "")
                        str4 = dataTable.Rows[0]["NO_TEL"].ToString();
                    if (dataTable.Rows[0]["NO_FAX"].ToString().Trim() != "")
                        str5 = dataTable.Rows[0]["NO_FAX"].ToString();
                }
                int num2 = 1;
                foreach (DataRow row in this._flexD.DataTable.Rows)
                {
                    if (row["NM_ITEM"].ToString().Trim() != "")
                        str6 = row["NM_ITEM"].ToString();
                    if (row["UNIT_IM"].ToString().Trim() != "")
                        str8 = row["UNIT_IM"].ToString();
                    if (row["NM_CC"].ToString().Trim() != "")
                        str9 = row["NM_CC"].ToString();
                    string str11 = row["DC1"].ToString();
                    decimal num3 = Convert.ToDecimal(row["QT_PO_MM"]);
                    decimal num4 = Convert.ToDecimal(row["UM_EX_PO"]);
                    decimal num5 = Convert.ToDecimal(row["AM_EX"]);
                    str7 = row["DT_LIMIT"].ToString().Substring(0, 4) + "." + row["DT_LIMIT"].ToString().Substring(4, 2) + "." + row["DT_LIMIT"].ToString().Substring(6, 2);
                    num1 += num5;
                    str1 = str1 + "<tr height='25'>\r\n\t\t\t                            <td rowspan='2' bgcolor='#F6F6F6'>" + num2 + "</td>\r\n\t\t\t                            <td class='al'>" + str6 + "</td>\r\n\t\t\t                            <td rowspan='2'>" + num3.ToString("###,###,###,###,##0") + "</td>\r\n\t\t\t                            <td rowspan='2'>" + str8 + "</td>\r\n\t\t\t                            <td rowspan='2'>" + num4.ToString("###,###,###,###,##0") + "</td>\r\n\t\t\t                            <td rowspan='2'>" + num5.ToString("###,###,###,###,##0") + "</td>\r\n\t\t\t                            <td rowspan='2'>" + str9 + "</td>\r\n\t\t                            </tr>\r\n\t\t\r\n\t\t                            <tr height='25'>\r\n\t\t\t                            <td class='al'>'" + str11 + "'</td>\r\n\t\t                            </tr>";
                    ++num2;
                }
                return "<html>\r\n\t                        <head>\r\n\t\t                        <meta http-equiv='Content-Language' content='ko'>\r\n\t\t                        <meta http-equiv='Content-Type' content='text/html; charset=utf-8'>\r\n\t                        </head>\r\n\r\n                        <center>\r\n                        <body leftmargin='0' marginwidth='0' topmargin='0' marginheight='0'>\r\n\t                        <table width='100%' cellpadding='0' cellspacing='0' style='font-size: 10pt; border-collapse: separate; border:0;' class='statement sepa_ta'>\r\n\t\t                        <colgroup>\r\n\t\t                            <col width='10%' />\r\n\t\t                            <col width='25%' />\r\n\t\t                            <col width='13%' />\r\n\t\t                            <col width='13%' />\r\n\t\t                            <col width='13%' />\r\n\t\t                            <col width='13%' />\r\n\t\t                            <col width='13%' />\r\n\t\t                        </colgroup>\r\n\t\t\r\n\t\t                        <tr height='25'>\r\n\t\t\t                        <td bgcolor='#F6F6F6'>발주번호</td>\r\n\t\t\t                        <td colspan='6'>" + str10 + "</td>\r\n\t\t                        </tr>\r\n\t\t\r\n\t\t                        <tr height='25'>\r\n\t\t\t                        <td bgcolor='#F6F6F6'>업체명</td>\r\n\t\t\t                        <td colspan='2'>" + str3 + "</td>\r\n\t\t\t                        <td bgcolor='#F6F6F6'>전화</td>\r\n\t\t\t                        <td>" + str4 + "</td>\r\n\t\t\t                        <td bgcolor='#F6F6F6'>팩스</td>\r\n\t\t\t                        <td>" + str5 + "</td>\r\n\t\t                        </tr>\r\n\t\t\r\n\t\t                        <tr height='25'>\r\n\t\t\t                        <td class='al' colspan='7'>하기와 같이 발주하오니 요건에 맞추어 기일내에 완료하기시 바랍니다.</td>\r\n\t\t                        </tr>\r\n\t\t\r\n\t\t                        <tr height='25'>\r\n\t\t\t                        <td bgcolor='#DAEFFF'>번호</td>\r\n\t\t\t                        <td bgcolor='#DAEFFF'>품명/규격</td>\r\n\t\t\t                        <td bgcolor='#DAEFFF'>수량(식)</td>\r\n\t\t\t                        <td bgcolor='#DAEFFF'>단위</td>\r\n\t\t\t                        <td bgcolor='#DAEFFF'>단가(원)</td>\r\n\t\t\t                        <td bgcolor='#DAEFFF'>금액(원)</td>\r\n\t\t\t                        <td bgcolor='#DAEFFF'>C/C명</td>\r\n\t\t                        </tr>" + str1 + " <tr height='25'>\r\n\t\t\t                        <td bgcolor='#FCEECD'>계</td>\r\n\t\t\t                        <td bgcolor='#FCEECD'></td>\r\n\t\t\t                        <td bgcolor='#FCEECD'></td>\r\n\t\t\t                        <td bgcolor='#FCEECD'></td>\r\n\t\t\t                        <td bgcolor='#FCEECD'></td>\r\n\t\t\t                        <td bgcolor='#FCEECD'>" + num1.ToString("###,###,###,###,##0") + "</td>\r\n\t\t\t                        <td bgcolor='#FCEECD'></td>\r\n\t\t                        </tr>\r\n\t\t\r\n\t\t                        <tr height='100'>\r\n\t\t\t                        <td class='al' colspan='7' valign='top'>* 특기사항 : <br>" + str2 + "</td>\r\n\t\t                        </tr>\r\n\t\t\r\n\t\t                        </table>\r\n\t                        </body>\r\n                        </center>\r\n                        </html>";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            return string.Empty;
        }

        private string 전자결재양식생성_지엔텔()
        {
            try
            {
                DataTable dataTable = this._biz.DataSearch_GW_RPT(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                 this._header.CurrentRow["NO_PO"].ToString() });
                if (dataTable == null || dataTable.Rows.Count == 0)
                    return "";
                string empty1 = string.Empty;
                string str1 = string.Empty;
                string str2 = D.GetString(dataTable.Rows[0]["LN_PARTNER"]) != string.Empty ? D.GetString(dataTable.Rows[0]["LN_PARTNER"]) : "&nbsp";
                string str3 = D.GetString(dataTable.Rows[0]["NM_CEO1"]) != string.Empty ? D.GetString(dataTable.Rows[0]["NM_CEO1"]) : "&nbsp";
                string str4 = D.GetString(dataTable.Rows[0]["ADS1"]) + " " + D.GetString(dataTable.Rows[0]["ADS_DETAIL1"]) != string.Empty ? D.GetString(dataTable.Rows[0]["ADS1"]) + " " + D.GetString(dataTable.Rows[0]["ADS_DETAIL1"]) : "&nbsp";
                string str5 = D.GetString(dataTable.Rows[0]["NO_TEL1"]) != string.Empty ? D.GetString(dataTable.Rows[0]["NO_TEL1"]) : "&nbsp";
                string str6 = D.GetString(dataTable.Rows[0]["NO_FAX1"]) != string.Empty ? D.GetString(dataTable.Rows[0]["NO_FAX1"]) : "&nbsp";
                string str7 = D.GetString(dataTable.Rows[0]["NM_PTR1"]) != string.Empty ? D.GetString(dataTable.Rows[0]["NM_PTR1"]) : "&nbsp";
                string str8 = D.GetString(dataTable.Rows[0]["NO_PO"]) != string.Empty ? D.GetString(dataTable.Rows[0]["NO_PO"]) : "&nbsp";
                string str9;
                if (!(D.GetString(dataTable.Rows[0]["DT_PO"]) != string.Empty))
                    str9 = "&nbsp";
                else
                    str9 = dataTable.Rows[0]["DT_PO"].ToString().Substring(0, 4) + "." + dataTable.Rows[0]["DT_PO"].ToString().Substring(4, 2) + "." + dataTable.Rows[0]["DT_PO"].ToString().Substring(6, 2);
                string str10 = str9;
                string str11 = D.GetString(dataTable.Rows[0]["DC50_PO"]) != string.Empty ? D.GetString(dataTable.Rows[0]["DC50_PO"]) : "&nbsp";
                string str12 = D.GetString(dataTable.Rows[0]["BIZ_NUM1"]) != string.Empty ? D.GetString(dataTable.Rows[0]["BIZ_NUM1"]) : "&nbsp";
                string str13 = D.GetString(dataTable.Rows[0]["NM_CEO"]) != string.Empty ? D.GetString(dataTable.Rows[0]["NM_CEO"]) : "&nbsp";
                string str14 = D.GetString(dataTable.Rows[0]["ADS"]) + " " + D.GetString(dataTable.Rows[0]["ADS_DETAIL"]) != string.Empty ? D.GetString(dataTable.Rows[0]["ADS"]) + " " + D.GetString(dataTable.Rows[0]["ADS_DETAIL"]) : "&nbsp";
                string str15 = D.GetString(dataTable.Rows[0]["NO_TEL"]) != string.Empty ? D.GetString(dataTable.Rows[0]["NO_TEL"]) : "&nbsp";
                string str16 = D.GetString(dataTable.Rows[0]["NO_FAX"]) != string.Empty ? D.GetString(dataTable.Rows[0]["NO_FAX"]) : "&nbsp";
                string str17 = D.GetString(dataTable.Rows[0]["NM_PTR"]) != string.Empty ? D.GetString(dataTable.Rows[0]["NM_PTR"]) : "&nbsp";
                decimal num1 = 0M;
                string empty2 = string.Empty;
                decimal num2 = 0M;
                foreach (DataRow row in dataTable.Rows)
                {
                    ++num1;
                    string str18 = D.GetString(row["CD_ITEM"]) != string.Empty ? D.GetString(row["CD_ITEM"]) : "&nbsp;";
                    string str19 = D.GetString(row["NM_ITEM"]) != string.Empty ? D.GetString(row["NM_ITEM"]) : "&nbsp;";
                    string str20 = D.GetString(row["STND_ITEM"]) != string.Empty ? D.GetString(row["STND_ITEM"]) : "&nbsp;";
                    string str21 = D.GetString(row["UNIT_IM"]) != string.Empty ? D.GetString(row["UNIT_IM"]) : "&nbsp;";
                    string str22 = D.GetString(row["CD_PJT"]) != string.Empty ? D.GetString(row["CD_PJT"]) : "&nbsp;";
                    string str23 = D.GetString(row["NM_PJT"]) != string.Empty ? D.GetString(row["NM_PJT"]) : "&nbsp;";
                    string str24 = row["DT_LIMIT"].ToString().Substring(0, 4) + "." + row["DT_LIMIT"].ToString().Substring(4, 2) + "." + row["DT_LIMIT"].ToString().Substring(6, 2);
                    decimal num3 = Convert.ToDecimal(row["QT_PO_MM"]);
                    decimal num4 = Convert.ToDecimal(row["UM_EX_PO"]);
                    decimal num5 = Convert.ToDecimal(row["AM_EX"]);
                    string str25 = D.GetString(row["DC1"]) != string.Empty ? D.GetString(row["DC1"]) : "&nbsp;";
                    decimal num6 = D.GetDecimal(row["QT"]);
                    decimal num7 = D.GetDecimal(row["UM_PO"]);
                    decimal num8 = D.GetDecimal(row["AM_EXPO_CIS"]);
                    string str26 = D.GetString(row["DC_RMK_PJT"]);
                    num2 += D.GetDecimal(row["AM_EX"]);
                    str1 = str1 + "<tr height='25'>\r\n\t\t        <td style='border-width: 1 1 1 1; border-style: solid; border-color: #000000;' style='line-height: 140%' rowspan='2'>" + num1 + "　</td>\r\n\t\t        <td style='border-width: 1 1 0 0; border-style: solid; border-color: #000000;' style='line-height: 140%'>" + str18 + "</td>\r\n\t\t        <td style='border-width: 1 1 0 0; border-style: solid; border-color: #000000;' style='line-height: 140%'>" + str19 + "　</td>\r\n\t\t        <td style='border-width: 1 1 0 0; border-style: solid; border-color: #000000;' style='line-height: 140%'>" + str20 + "　</td>\r\n\t\t        <td style='border-width: 1 1 0 0; border-style: solid; border-color: #000000;' style='line-height: 140%'>" + str21 + "　</td>\r\n\t\t        <td style='border-width: 1 1 1 0; border-style: solid; border-color: #000000;' style='line-height: 140%' rowspan='2'>" + str24 + "　</td>\r\n\t\t        <td style='border-width: 1 1 0 0; border-style: solid; border-color: #000000;' style='line-height: 140%'>" + num3.ToString("###,###,###,###,##0") + "　</td>\r\n\t\t        <td style='border-width: 1 1 0 0; border-style: solid; border-color: #000000;' style='line-height: 140%'>" + num4.ToString("###,###,###,###,##0") + "　</td>\r\n\t\t        <td style='border-width: 1 1 0 0; border-style: solid; border-color: #000000;' style='line-height: 140%'>" + num5.ToString("###,###,###,###,##0") + "　</td>\r\n\t\t        <td style='border-width: 1 1 0 0; border-style: solid; border-color: #000000;' style='line-height: 140%'>" + str25 + "　</td>\r\n\t        </tr>\r\n\t        <tr height='25'>\r\n\t\t        <td style='border-width: 1 1 1 0; border-style: solid; border-color: #000000;' style='line-height: 140%' colspan='2'>" + str22 + "　</td>\r\n\t\t        <td style='border-width: 1 1 1 0; border-style: solid; border-color: #000000;' style='line-height: 140%' colspan='2'>" + str23 + "　</td>\r\n\t\t        <td style='border-width: 1 1 1 0; border-style: solid; border-color: #000000;' style='line-height: 140%'>　" + num6.ToString("###,###,###,###,##0") + "</td>\r\n\t\t        <td style='border-width: 1 1 1 0; border-style: solid; border-color: #000000;' style='line-height: 140%'>" + num7.ToString("###,###,###,###,##0") + "　</td>\r\n\t\t        <td style='border-width: 1 1 1 0; border-style: solid; border-color: #000000;' style='line-height: 140%'>　" + num8.ToString("###,###,###,###,##0") + "</td>\r\n\t\t        <td style='border-width: 1 1 1 0; border-style: solid; border-color: #000000;' style='line-height: 140%'>　" + str26 + "</td>\r\n\t        </tr>";
                }
                string str27 = str1 + "<tr height='25'>\r\n\t\t        <td style='border-width: 1 1 1 1; border-style: solid; border-color: #000000;' style='line-height: 140%' colspan='7'>&nbsp;</td>\r\n\t\t        <td style='border-width: 1 1 1 0; border-style: solid; border-color: #000000;' style='line-height: 140%'> 합계 </td>\r\n\t\t        <td style='border-width: 1 1 1 0; border-style: solid; border-color: #000000;' style='line-height: 140%'>" + num2.ToString("###,###,###,###,##0") + "&nbsp;　</td>\r\n\t\t        <td style='border-width: 1 1 1 0; border-style: solid; border-color: #000000;' style='line-height: 140%'> &nbsp; </td>\r\n\t        </tr>";
                return "<head> <meta http-equiv='Content-Language' content='ko'></head>\r\n                        <center>\r\n                        <body leftmargin='0' marginwidth='0' topmargin='0' marginheight='0'>\r\n                            <table width='945' border='0' cellpadding='0' cellspacing='0' style='font-size: 10pt'>\r\n\t                            <tr height='50'>\r\n\t\t                        <td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'>\r\n\t\t                        <p align='center'><span style='font-size: 23pt; font-weight: 700'>구 매 발 주 서</span></td>\r\n\t                            </tr>\r\n                            </table>\r\n            <table width='945' border='0' cellpadding='0' cellspacing='0' style='font-size: 10pt'>\r\n\t                <colgroup width='90' align='center'></colgroup>\r\n\t                <colgroup width='156' align='center'></colgroup>\r\n\t                <colgroup width='80' align='center'></colgroup>\r\n\t                <colgroup width='146' align='center'></colgroup>\r\n\t                <colgroup width='95' align='center'></colgroup>\r\n\t                <colgroup width='151' align='center'></colgroup>\r\n\t                <colgroup width='80' align='center'></colgroup>\r\n\t                <colgroup width='147' align='center'></colgroup>\r\n\t            <tr height='30'> \r\n\t\t        <td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' colspan='8'>\r\n\t\t        <p align='left'>" + str2 + "</td>\r\n\t            </tr>\r\n\t            <tr height='25'> \r\n\t\t<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'>\r\n\t\t대 표 자</td>\r\n\t\t<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' align='left'>\r\n\t\t: " + str3 + "</td>\r\n\t\t<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' align='left' colspan='2'>\r\n\t\t귀하</td>\r\n\t\t<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'>\r\n\t\t사업자등록번호</td>\r\n\t\t<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' colspan='3' align='left'>\r\n\t\t: " + str12 + " </td>\r\n\t</tr>\r\n\t<tr height='25'> \r\n\t\t<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'>\r\n\t\t주&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 소</td>\r\n\t\t<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' align='left' colspan='3'>\r\n\t\t: " + str4 + "</td>\r\n\t\t<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' align='right'>\r\n\t\t대 표 자</td>\r\n\t\t<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' colspan='3' align='left'>\r\n\t\t&nbsp;:&nbsp;" + str13 + "</td>\r\n\t</tr>\r\n\t<tr height='25'> \r\n\t\t<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'>\r\n\t\t전&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 화</td>\r\n\t\t<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' align='left'>\r\n\t\t: " + str5 + "</td>\r\n\t\t<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' align='left'>\r\n\t\tF A X</td>\r\n\t\t<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' align='left'>\r\n\t\t: " + str6 + "</td>\r\n\t\t<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' align='right'>\r\n\t\t주&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 소</td>\r\n\t\t<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' colspan='3' align='left'>\r\n\t\t&nbsp;: " + str14 + " </td>\r\n\t</tr>\r\n\t<tr height='25'> \r\n\t\t<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'>\r\n\t\t담 당 자</td>\r\n\t\t<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' align='left' colspan='3'>\r\n\t\t: " + str7 + "</td>\r\n\t\t<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' align='right'>\r\n\t\t전&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 화</td>\r\n\t\t<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'>\r\n\t\t<p align='left'>&nbsp;: " + str15 + "</td>\r\n\t\t<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'>\r\n\t\t<p align='left'>F A X</td>\r\n\t\t<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'>\r\n\t\t<p align='left'>: " + str16 + "</td>\r\n\t</tr>\r\n\t<tr height='25'> \r\n\t\t<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'>\r\n\t\t발주번호</td>\r\n\t\t<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' align='left'>\r\n\t\t: " + str8 + "</td>\r\n\t\t<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' align='left'>\r\n\t\t발주일자</td>\r\n\t\t<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' align='left'>\r\n\t\t: " + str10 + "</td>\r\n\t\t<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' align='right'>\r\n\t\t담 당 자</td>\r\n\t\t<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' colspan='3'>\r\n\t\t<p align='left'>&nbsp;: " + str17 + "</td>\r\n\t</tr>\r\n\t<tr height='25'> \r\n\t\t<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'>\r\n\t\t비&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 고</td>\r\n\t\t<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' align='left' colspan='7'>\r\n\t\t: " + str11 + "</td>\r\n\t</tr>\r\n\t<tr height='45'> \r\n\t\t<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' colspan='8'>\r\n\t\t( 아래와 같이 발주합니다. )</td>\r\n\t</tr>\r\n</table>\r\n\t<table width='945' border='0' cellpadding='0' cellspacing='0' style='font-size: 9pt'>\r\n\t<colgroup width='5%' align='center'></colgroup>\r\n\t<colgroup width='10%' align='center'></colgroup>\r\n\t<colgroup width='10%' align='center'></colgroup>\r\n\t<colgroup width='10%' align='center'></colgroup>\r\n\t<colgroup width='10%' align='center'></colgroup>\r\n\t<colgroup width='10%' align='center'></colgroup>\r\n\t<colgroup width='9%' align='center'></colgroup>\r\n\t<colgroup width='9%' align='center'></colgroup>\r\n\t<colgroup width='9%' align='center'></colgroup>\r\n\t<colgroup width='18%' align='center'></colgroup>\r\n\t<tr height='25'>\r\n\t\t<td style='border-width: 1 1 0 1; border-style: solid; border-color: #000000;' style='line-height: 140%' rowspan='2'>\r\n\t\tNO</td>\r\n\t\t<td style='border-width: 1 1 0 0; border-style: solid; border-color: #000000;' style='line-height: 140%'>\r\n\t\t품 목</td>\r\n\t\t<td style='border-width: 1 1 0 0; border-style: solid; border-color: #000000;' style='line-height: 140%'>\r\n\t\t품 명</td>\r\n\t\t<td style='border-width: 1 1 0 0; border-style: solid; border-color: #000000;' style='line-height: 140%'>\r\n\t\t규 격</td>\r\n\t\t<td style='border-width: 1 1 0 0; border-style: solid; border-color: #000000;' style='line-height: 140%'>\r\n\t\t단 위</td>\r\n\t\t<td style='border-width: 1 1 0 0; border-style: solid; border-color: #000000;' style='line-height: 140%' rowspan='2'>\r\n\t\t납기일</td>\r\n\t\t<td style='border-width: 1 1 0 0; border-style: solid; border-color: #000000;' style='line-height: 140%' rowspan='2'>\r\n\t\t수 량</td>\r\n\t\t<td style='border-width: 1 1 0 0; border-style: solid; border-color: #000000;' style='line-height: 140%' rowspan='2'>\r\n\t\t단 가</td>\r\n\t\t<td style='border-width: 1 1 0 0; border-style: solid; border-color: #000000;' style='line-height: 140%' rowspan='2'>\r\n\t\t발주금액</td>\r\n\t\t<td style='border-width: 1 1 0 0; border-style: solid; border-color: #000000;' style='line-height: 140%' rowspan='2'>\r\n\t\t비&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 고</td>\r\n\t</tr>\r\n\t<tr height='25'>\r\n\t\t<td style='border-width: 1 1 0 0; border-style: solid; border-color: #000000;' style='line-height: 140%' colspan='2'>\r\n\t\t사전 PJT code</td>\r\n\t\t<td style='border-width: 1 1 0 0; border-style: solid; border-color: #000000;' style='line-height: 140%' colspan='2'>\r\n\t\t사전 PJT 명</td>\r\n\t</tr>" + str27 + "</table>\r\n</body>\r\n</center>";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            return string.Empty;
        }

        private string GW_Nexti_html()
        {
            string end;
            using (StreamReader streamReader = new StreamReader(Application.StartupPath + "\\download\\gw\\HT_PU_PO_REG2_NEXT_I.htm", Encoding.Default))
                end = streamReader.ReadToEnd();
            DataTable dataTable = this._biz.DataSearch_GW_RPT(new object[2]
            {
         Global.MainFrame.LoginInfo.CompanyCode,
         this._header.CurrentRow["NO_PO"].ToString()
            });
            string str1 = end.Replace("@@발주일자", D.GetString(dataTable.Rows[0]["DT_PO"]).Substring(0, 4) + "/" + D.GetString(dataTable.Rows[0]["DT_PO"]).Substring(4, 2) + "/" + D.GetString(dataTable.Rows[0]["DT_PO"]).Substring(6, 2) + "&nbsp;").Replace("@@납기일자", D.GetString(dataTable.Rows[0]["DT_LIMIT"]).Substring(0, 4) + "/" + D.GetString(dataTable.Rows[0]["DT_LIMIT"]).Substring(4, 2) + "/" + D.GetString(dataTable.Rows[0]["DT_LIMIT"]).Substring(6, 2) + "&nbsp;").Replace("@@발주번호", D.GetString(dataTable.Rows[0]["NO_PO"]) + "(" + D.GetString(dataTable.Rows[0]["CD_PJT"]) + ")&nbsp;").Replace("@@구매자", D.GetString(dataTable.Rows[0]["NM_PTR"]) + "&nbsp;").Replace("@@전화", D.GetString(dataTable.Rows[0]["NO_TEL"]) + "&nbsp;").Replace("@@팩스", D.GetString(dataTable.Rows[0]["NO_FAX"]) + "&nbsp;").Replace("@@업체명", D.GetString(dataTable.Rows[0]["거래처명"]) + "&nbsp;").Replace("@@담당자", D.GetString(dataTable.Rows[0]["CD_EMP_PARTNER"]) + "&nbsp;").Replace("@@거래처전화", D.GetString(dataTable.Rows[0]["NO_TEL1"]) + "&nbsp;").Replace("@@거래처팩스", D.GetString(dataTable.Rows[0]["NO_FAX1"]) + "&nbsp;").Replace("@@사업자등록번호", D.GetString(dataTable.Rows[0]["BIZ_NUM"]) + "&nbsp;").Replace("@@본사", D.GetString(dataTable.Rows[0]["ADS"]) + D.GetString(dataTable.Rows[0]["ADS_DETAIL"]) + "&nbsp;").Replace("@@기반연구소", D.GetString(dataTable.Rows[0]["PLANT_ADS_HD"]) + "&nbsp;").Replace("@@대표이사", D.GetString(dataTable.Rows[0]["NM_CEO"]) + "&nbsp;").Replace("@@업태", D.GetString(dataTable.Rows[0]["TP_JOB_BIZ"]) + "&nbsp;").Replace("@@종목", D.GetString(dataTable.Rows[0]["CLS_JOB_BIZ"]) + "&nbsp;").Replace("@@프로젝트예산", D.GetString(dataTable.Rows[0]["PROJECT_BUDGET"]) + "&nbsp;").Replace("@@발주누적액", D.GetDecimal(dataTable.Rows[0]["POL_AM_SUM"]).ToString("###,###,###,###,##0.####") + "&nbsp;");
            int num1 = 0;
            decimal num2 = 0M;
            foreach (DataRow row in dataTable.Rows)
            {
                ++num1;
                string str2 = D.GetString(row["CD_CLS_L"]);
                string str3 = D.GetString(row["CD_ITEM"]);
                string str4 = D.GetString(row["NM_ITEM"]);
                string str5 = D.GetDecimal(row["QT_PO_MM"]).ToString("###,###,###,###,##0");
                string str6 = D.GetDecimal(row["UM_EX_PO"]).ToString("###,###,###,###,##0.####");
                string str7 = D.GetDecimal(row["AM_EX"]).ToString("###,###,###,###,##0.####");
                string str8 = D.GetString(row["DC1"]);
                num2 += D.GetDecimal(row["AM_EX"]);
                str1 = str1 + "\r\n           \t\t<tr height='20'>\r\n\t\t        <td style= 'font-family:굴림; font-size:9pt;'  align='center'>" + num1 + "&nbsp;</td>\r\n\t\t        <td style=' font-family:굴림; font-size:9pt;'  align='center'>" + str2 + "&nbsp;</td>\r\n\t\t        <td style=' font-family:굴림; font-size:9pt;'  align='center'>" + str3 + "&nbsp;</td>\r\n\t\t        <td style=' font-family:굴림; font-size:9pt;'  align='center'>" + str4 + "&nbsp;</td>\r\n\t\t        <td style=' font-family:굴림; font-size:9pt;'  align='center'>" + str5 + "&nbsp;</td>\r\n\t\t        <td style=' font-family:굴림; font-size:9pt;'  align='center'>" + str6 + "&nbsp;</td>\r\n\t\t        <td style=' font-family:굴림; font-size:9pt;'  align='center'>" + str7 + "&nbsp;</td>\r\n\t\t        <td style=' font-family:굴림; font-size:9pt;'  align='center'>" + str8 + "&nbsp;</td>\r\n\t        </tr>";
            }
            return str1 + "<tr height='20'>\r\n\t                    <td style=' font-family:굴림; font-size:9pt; ' align='center' colspan='5'>\r\n\t                    총 발 주 금 액 (VAT 별도) </td>\r\n\t                    <td style=' font-family:굴림; font-size:9pt; ' align='center' colspan='3'>\r\n                        " + num2.ToString("###,###,###,###,##0.####") + " &nbsp;</td>\r\n                                </tr>\r\n                            </table>\r\n                        </body>\r\n                        </center> ";
        }

        private string GW_Solid_html()
        {
            string end;
            using (StreamReader streamReader = new StreamReader(Application.StartupPath + "\\download\\gw\\HT_PU_PO_REG2_SOLID.htm", Encoding.Default))
                end = streamReader.ReadToEnd();
            DataTable dataTable = this._biz.DataSearch_GW_RPT(new object[2]
            {
         Global.MainFrame.LoginInfo.CompanyCode,
         this._header.CurrentRow["NO_PO"].ToString()
            });
            string str1 = end.Replace("@@구매담당자", D.GetString(dataTable.Rows[0]["NM_PTR"]) + "&nbsp;").Replace("@@전화번호", D.GetString(dataTable.Rows[0]["NO_TEL"]) + "&nbsp;").Replace("@@팩스번호", D.GetString(dataTable.Rows[0]["NO_FAX"]) + "&nbsp;").Replace("@@이메일", D.GetString(dataTable.Rows[0]["NO_EMAIL"]) + "&nbsp;").Replace("@@거래처명", D.GetString(dataTable.Rows[0]["거래처명"]) + "&nbsp;").Replace("@@거래처전화번호", D.GetString(dataTable.Rows[0]["NO_TEL1"]) + "&nbsp;").Replace("@@거래처팩스번호", D.GetString(dataTable.Rows[0]["NO_FAX1"]) + "&nbsp;").Replace("@@거래처담당자", D.GetString(dataTable.Rows[0]["NM_PTR1"]) + "&nbsp;").Replace("@@핸드폰번호", D.GetString(dataTable.Rows[0]["P_HP"]) + "&nbsp;").Replace("@@이메일", D.GetString(dataTable.Rows[0]["P_EMAIL"]) + "&nbsp;").Replace("@@결제조건", D.GetString(dataTable.Rows[0]["NM_FG_PAYMENT"]) + "&nbsp;").Replace("@@인도조건", D.GetString(dataTable.Rows[0]["NM_COND_PRICE"]) + "&nbsp;").Replace("@@총공급가", D.GetDecimal(dataTable.Rows[0]["H_AM"]).ToString("###,###,###,###,###") + "&nbsp;").Replace("@@부가세", D.GetDecimal(dataTable.Rows[0]["H_VAT"]).ToString("###,###,###,###,###") + "&nbsp;").Replace("@@납품장소", D.GetString(dataTable.Rows[0]["NM_ARRIVER"]) + "&nbsp;");
            int num1 = 0;
            decimal num2 = 0M;
            decimal num3 = 0M;
            foreach (DataRow row in dataTable.Rows)
            {
                ++num1;
                string str2 = D.GetString(row["CD_ITEM"]);
                string str3 = D.GetString(row["NM_ITEM"]);
                string str4 = D.GetString(row["STND_ITEM"]);
                decimal num4 = D.GetDecimal(row["QT_PO_MM"]);
                string str5 = num4.ToString("###,###,###,###,##0");
                string str6 = D.GetString(row["NM_EXCH"]);
                num4 = D.GetDecimal(row["UM"]);
                string str7 = num4.ToString("###,###,###,###,##0.####");
                num4 = D.GetDecimal(row["AM"]);
                string str8 = num4.ToString("###,###,###,###,##0.####");
                string str9 = D.GetString(row["DT_LIMIT"]);
                string str10 = D.GetString(row["DC1"]);
                num2 += D.GetDecimal(row["AM"]);
                num3 += D.GetDecimal(row["VAT"]);
                str1 = str1 + "\r\n           \t\t<tr height='25'>\r\n\t\t        <td style='border-width: 1 0 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center' rowspan='2' valign='top'>" + num1 + "&nbsp;</td>\r\n\t\t        <td style='border-width: 1 0 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='left' colspan='2' rowspan='2' valign='top'>" + str2 + "&nbsp;</td>\r\n\t\t        <td style='border-width: 1 0 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='left' colspan='5' valign='top'>" + str3 + "&nbsp;</td>\r\n\t\t        <td style='border-width: 1 0 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center' colspan='2' rowspan='2' valign='top'>" + str5 + "&nbsp;</td>\r\n\t\t        <td style='border-width: 1 0 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center' rowspan='2' colspan='2' valign='top'>" + str6 + "&nbsp;</td>\r\n\t\t        <td style='border-width: 1 0 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center' rowspan='2' valign='top'>" + str7 + "&nbsp;</td>\r\n\t\t        <td style='border-width: 1 0 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center' colspan='3' rowspan='2' valign='top'>" + str8 + "&nbsp;</td>\r\n                <td style='border-width: 1 0 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center' colspan='2' rowspan='2' valign='top'>" + str9 + "&nbsp;</td>\r\n\t\t        <td style='border-width: 1 0 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='left' rowspan='2' valign='top'>" + str10 + "&nbsp;</td>\r\n\t        </tr>  \r\n            <tr height='25'>\r\n                <td style='border-width: 0 0 0 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='left' colspan='5' valign='top'>" + str4 + "&nbsp;</td>\r\n            </tr>";
            }
            return (str1 + "<tr height='25'>\r\n                <td style='border-width: 1 1 1 1; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='left' colspan='19'>\r\n                - 발 주 조 건 - <br>\r\n                1. 대금결제조건: 당사 결제조건에 준함 \r\n\t\t                    <p style='margin-left:20; margin-top:2; margin-bottom:2'>(천만원 이상 : 익월 15일 전자어음 발행(60일), 백만원 이상 ~ 천만원 미만: 익월 15일 전자어음 \r\n\t\t                    발행(45일), 백만원 미만 : 익월 15일 현금지급) </p>2. 참고 사항 <br>\r\n\t\t                    <p style='margin-left:20; margin-top:2; margin-bottom:2'>1) 귀사는 본 발주서 접수 後 2일이내에 당사의 발주수용여부 및 납품가능일을 공식적으로 통보바랍니다. <br>\r\n\t\t                    2) 본 발주서에 따른 물품의 입고지연으로 발생되는 모든 손해 및 손실은 귀사에서 부담하여야 합니다. <br>\r\n\t\t                    &nbsp;&nbsp;\r\n\t\t                    [지체보상금 기준 : 총 발주금액 X 지체일 X 2.5/1,000] <br>\r\n\t\t                    3) 본 발주서에 명기된 견적가는 변동될 수 있습니다. <br>\r\n\t\t                    4) 최소 입고 3일전에 당사의 수입검사팀[TEL: 031-789-8519]으로 연락하여 관련된 사항을 협의바랍니다. \r\n\t\t                    [수입검사 일정 및 수입검사 합.불합격에 따른 처리, 입고처 등] <br>\r\n\t\t                    5) 물품 입고간 사전동의 없이 부품변경을 하지 못하고, 만약 이를 위반할 경우 공급처에서 모든 책임을 진다 [계약서 \r\n\t\t                    18조 3항 개별 계약 및 특약사항 준수] <br>\r\n\t\t                    6) RoHS 관련사항 : 납품하는 자재가 RoHS 적용 품목일때는 RoHS관련 미사용 증명서를 제출한다. <br>\r\n\t\t                    7) 당사에 해당하는 관리품목(외주 주요품목 등)에 한하여 당사에서 지정한 규격의 POP 바코드를 부착하여 납품 하셔야 \r\n\t\t                    합니다. <br>\r\n\t\t                    &nbsp;&nbsp;\r\n\t\t                    -. 관리품목 대상확인은 당사 생산관리팀[TEL: 031-789-8505]에 문의하고 확인하시기 바랍니다 </p>\r\n\t\t                    3. 마감관련 <br>\r\n\t\t                    <p style='margin-left:20; margin-top:2; margin-bottom:2'>1) 당사의 구매마감은 매월 25일 입니다. <br>\r\n\t\t                    2) 세금계산서는 매월 말일 날짜로 작성 부탁드리며, 매월 25일까지 거래내역서와 세금계산서를 구매 담당자에게 전달해 \r\n\t\t                    주시기 바랍니다. </p>\r\n\t\t                    <br>\r\n\t\t                    4. 본 발주와 관련한 문의 사항 발생 시 (주)쏠리테크 구매팀으로 연락하시기 바랍니다. <br>\r\n\t\t                    <br>\r\n\t\t                    <p style='margin-left:20; margin-top:2; margin-bottom:2'>감사합니다.<br>\r\n                    </td>\r\n\t            </tr>\r\n        \r\n                <tr height='25'>\r\n\t\t        <td style='border-width: 0 00 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center'>\r\n\t\t            &nbsp;</td>\r\n\t\t        <td style='border-width: 0 00 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center'>\r\n\t\t            &nbsp;</td>\r\n\t\t        <td style='border-width: 0 00 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center'>\r\n\t\t            &nbsp;</td>\r\n\t\t        <td style='border-width: 0 00 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center'>\r\n\t\t        &nbsp;</td>\r\n\t\t        <td style='border-width: 0 00 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center'>\r\n\t\t        &nbsp;</td>\r\n\t\t        <td style='border-width: 0 00 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center'>\r\n\t\t        &nbsp;</td>\r\n\t\t        <td style='border-width: 0 00 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center'>\r\n\t\t        &nbsp;</td>\r\n\t\t        <td style='border-width: 0 00 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center'>\r\n\t\t        &nbsp;</td>\r\n\t\t        <td style='border-width: 0 00 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center'>\r\n\t\t        &nbsp;</td>\r\n\t\t        <td style='border-width: 0 00 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center'>\r\n\t\t        &nbsp;</td>\r\n\t\t        <td style='border-width: 0 00 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center'>\r\n\t\t        &nbsp;</td>\r\n\t\t        <td style='border-width: 0 00 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center'>\r\n\t\t        &nbsp;</td>\r\n\t\t        <td style='border-width: 0 00 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center'>\r\n\t\t        &nbsp;</td>\r\n\t\t        <td style='border-width: 0 00 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center'>\r\n\t\t        &nbsp;</td>\r\n\t\t        <td style='border-width: 0 00 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center'>\r\n\t\t        &nbsp;</td>\r\n\t\t        <td style='border-width: 0 00 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center'>\r\n\t\t        &nbsp;</td>\r\n\t\t        <td style='border-width: 0 00 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center'>\r\n\t\t        &nbsp;</td>\r\n\t\t        <td style='border-width: 0 00 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center'>\r\n\t\t        &nbsp;</td>\r\n\t\t        <td style='border-width: 0 00 0; border-style:solid; border-color:#000000; font-family:굴림; font-size:8pt; line-height:140%' align='center'>\r\n\t\t        &nbsp;</td>\r\n\t        </tr>\r\n        </table>\r\n    </body>\r\n</center>\r\n").Replace("@@총금액", D.GetDecimal((D.GetDecimal(num2) + D.GetDecimal(num3))).ToString("###,###,###,###,###") + "&nbsp;");
        }

        private string 전자결재양식생성_광진윈텍()
        {
            try
            {
                string path = Application.StartupPath + "\\download\\gw\\HT_PU_PO_REG2_KJWT.htm";
                string str1 = "";
                using (StreamReader streamReader = new StreamReader(path, Encoding.Default))
                    str1 = streamReader.ReadToEnd();
                DataTable dataTable = this._biz.DataSearch_GW_RPT(new object[2]
                {
           Global.MainFrame.LoginInfo.CompanyCode,
           this._header.CurrentRow["NO_PO"].ToString()
                });
                if (dataTable == null || dataTable.Rows.Count == 0)
                    return "";
                string empty1 = string.Empty;
                string empty2 = string.Empty;
                string str2 = D.GetString(dataTable.Rows[0]["LN_PARTNER"]) != string.Empty ? D.GetString(dataTable.Rows[0]["LN_PARTNER"]) : "&nbsp";
                string str3 = D.GetString(dataTable.Rows[0]["NM_CEO1"]) != string.Empty ? D.GetString(dataTable.Rows[0]["NM_CEO1"]) : "&nbsp";
                string str4 = D.GetString(dataTable.Rows[0]["ADS1"]) + " " + D.GetString(dataTable.Rows[0]["ADS_DETAIL1"]) != string.Empty ? D.GetString(dataTable.Rows[0]["ADS1"]) + " " + D.GetString(dataTable.Rows[0]["ADS_DETAIL1"]) : "&nbsp";
                string str5 = D.GetString(dataTable.Rows[0]["NO_TEL1"]) != string.Empty ? D.GetString(dataTable.Rows[0]["NO_TEL1"]) : "&nbsp";
                string str6 = D.GetString(dataTable.Rows[0]["NO_FAX1"]) != string.Empty ? D.GetString(dataTable.Rows[0]["NO_FAX1"]) : "&nbsp";
                string str7 = D.GetString(dataTable.Rows[0]["NM_PTR1"]) != string.Empty ? D.GetString(dataTable.Rows[0]["NM_PTR1"]) : "&nbsp";
                string str8 = D.GetString(dataTable.Rows[0]["NO_PO"]) != string.Empty ? D.GetString(dataTable.Rows[0]["NO_PO"]) : "&nbsp";
                string str9;
                if (!(D.GetString(dataTable.Rows[0]["DT_PO"]) != string.Empty))
                    str9 = "&nbsp";
                else
                    str9 = dataTable.Rows[0]["DT_PO"].ToString().Substring(0, 4) + "." + dataTable.Rows[0]["DT_PO"].ToString().Substring(4, 2) + "." + dataTable.Rows[0]["DT_PO"].ToString().Substring(6, 2);
                string str10 = str9;
                string str11 = D.GetString(dataTable.Rows[0]["DC50_PO"]) != string.Empty ? D.GetString(dataTable.Rows[0]["DC50_PO"]) : "&nbsp";
                string str12 = D.GetString(dataTable.Rows[0]["BIZ_NUM1"]) != string.Empty ? D.GetString(dataTable.Rows[0]["BIZ_NUM1"]) : "&nbsp";
                string str13 = D.GetString(dataTable.Rows[0]["NM_CEO"]) != string.Empty ? D.GetString(dataTable.Rows[0]["NM_CEO"]) : "&nbsp";
                string str14 = D.GetString(dataTable.Rows[0]["ADS"]) + " " + D.GetString(dataTable.Rows[0]["ADS_DETAIL"]) != string.Empty ? D.GetString(dataTable.Rows[0]["ADS"]) + " " + D.GetString(dataTable.Rows[0]["ADS_DETAIL"]) : "&nbsp";
                string str15 = D.GetString(dataTable.Rows[0]["NO_TEL"]) != string.Empty ? D.GetString(dataTable.Rows[0]["NO_TEL"]) : "&nbsp";
                string str16 = D.GetString(dataTable.Rows[0]["NO_FAX"]) != string.Empty ? D.GetString(dataTable.Rows[0]["NO_FAX"]) : "&nbsp";
                string str17 = D.GetString(dataTable.Rows[0]["NM_PTR"]) != string.Empty ? D.GetString(dataTable.Rows[0]["NM_PTR"]) : "&nbsp";
                string str18 = str1.Replace("@@LN_PARTNER", str2 + "&nbsp;").Replace("@@NM_CEO1", str3 + "&nbsp;").Replace("@@ADS1", str4 + "&nbsp;").Replace("@@NO_TEL1", str5 + "&nbsp;").Replace("@@NO_FAX1", str6 + "&nbsp;").Replace("@@NM_PTR1", str7 + "&nbsp;").Replace("@@NO_PO", str8 + "&nbsp;").Replace("@@DT_PO", str10 + "&nbsp;").Replace("@@DC50_PO", str11 + "&nbsp;").Replace("@@BIZ_NUM", str12 + "&nbsp;").Replace("@@NM_CEO", str13 + "&nbsp;").Replace("@@ADS", str14 + "&nbsp;").Replace("@@NO_TEL", str15 + "&nbsp;").Replace("@@NO_FAX", str16 + "&nbsp;").Replace("@@NM_PTR", str17 + "&nbsp;");
                decimal num1 = 0M;
                string empty3 = string.Empty;
                string str19 = "";
                decimal num2 = 0M;
                decimal num3 = 0M;
                decimal num4 = 0M;
                string str20 = "";
                decimal num5 = 0M;
                decimal num6 = 0M;
                decimal num7 = 0M;
                decimal num8 = 0M;
                foreach (DataRow row in dataTable.Rows)
                {
                    ++num1;
                    string str21 = D.GetString(row["CD_ITEM"]) != string.Empty ? D.GetString(row["CD_ITEM"]) : "&nbsp;";
                    string str22 = D.GetString(row["NM_ITEM"]) != string.Empty ? D.GetString(row["NM_ITEM"]) : "&nbsp;";
                    string str23 = D.GetString(row["STND_ITEM"]) != string.Empty ? D.GetString(row["STND_ITEM"]) : "&nbsp;";
                    string str24 = D.GetString(row["UNIT_IM"]) != string.Empty ? D.GetString(row["UNIT_IM"]) : "&nbsp;";
                    string str25 = row["DT_LIMIT"].ToString().Substring(0, 4) + "." + row["DT_LIMIT"].ToString().Substring(4, 2) + "." + row["DT_LIMIT"].ToString().Substring(6, 2);
                    decimal num9 = Convert.ToDecimal(row["QT_PO_MM"]);
                    decimal num10 = Convert.ToDecimal(row["UM_EX_PO"]);
                    decimal num11 = Convert.ToDecimal(row["AM_EX"]);
                    decimal num12 = Convert.ToDecimal(row["AM"]);
                    decimal num13 = Convert.ToDecimal(row["VAT"]);
                    str19 = D.GetString(row["DC1"]) != string.Empty ? D.GetString(row["DC1"]) : "&nbsp;";
                    num2 = D.GetDecimal(row["QT"]);
                    num3 = D.GetDecimal(row["UM_PO"]);
                    num4 = D.GetDecimal(row["AM_EXPO_CIS"]);
                    str20 = D.GetString(row["DC_RMK_PJT"]);
                    num6 += D.GetDecimal(row["AM_EX"]);
                    num5 += D.GetDecimal(row["QT_PO_MM"]);
                    num7 += D.GetDecimal(row["AM"]);
                    num8 += D.GetDecimal(row["VAT"]);
                    str18 = str18 + "<tr height='25'>\r\n                            <td style='border-left-width:1; border-bottom-width:1px' style='line-height: 140%' align=''>" + num1 + " </td>\r\n                            <td style='border-left-width:0; border-bottom-width:1px' style='line-height: 140%'>" + str21 + "</td>                                    \r\n                            <td style='border-left-width:0; border-bottom-width:1px' style='line-height: 140%'>" + str22 + "</td>                                    \r\n                            <td style='border-left-width:0; border-bottom-width:1px' style='line-height: 140%'>" + str23 + "</td>                                    \r\n                            <td style='border-left-width:0; border-bottom-width:1px' style='line-height: 140%'>" + str24 + "</td>                                    \r\n                            <td style='border-left-width:0; border-bottom-width:1px' style='line-height: 140%'>" + str25 + "</td>                                    \r\n                            <td style='border-left-width:0; border-bottom-width:1px' style='line-height: 140%' align='right'>" + num10.ToString("###,###,###,###,##0") + "</td>\r\n                            <td style='border-left-width:0; border-bottom-width:1px' style='line-height: 140%' align='right'>" + num9.ToString("###,###,###,###,##0") + "</td>\r\n                            <td style='border-left-width:0; border-bottom-width:1px' style='line-height: 140%' align='right'>" + num11.ToString("###,###,###,###,##0") + "</td>    \r\n                            <td style='border-left-width:0; border-bottom-width:1px' style='line-height: 140%' align='right'>" + num12.ToString("###,###,###,###,##0") + "</td>                                    \r\n                            <td style='border-left-width:0; border-bottom-width:1px' style='line-height: 140%' align='right'>" + num13.ToString("###,###,###,###,##0") + "</td>    \r\n\t                    </tr>";
                }
                return str18 + "<tr height='25'>\r\n                            <td style='border-top:1 solid #000000; border-bottom:1 solid #000000;' style='line-height: 140%' width='44'>　</td>\r\n                            <td style='border-top:1 solid #000000; border-bottom:1 solid #000000;' style='line-height: 140%' width='92'>　</td>\r\n                            <td style='border-top:1 solid #000000; border-bottom:1 solid #000000;' style='line-height: 140%' width='92'>　</td>\r\n                            <td style='border-top:1 solid #000000; border-bottom:1 solid #000000;' style='line-height: 140%' width='93'>　</td>\r\n                            <td style='border-top:1 solid #000000; border-bottom:1 solid #000000;' style='line-height: 140%' width='93'>　</td>\r\n                            <td style='border-top:1 solid #000000; border-bottom:1 solid #000000;' style='line-height: 140%' width='84'><b>합 계</b></td>\r\n                            <td style='border-top:1 solid #000000; border-bottom:1 solid #000000;' style='line-height: 140%' width='93'>　</td>\r\n                            <td style='border-top:1 solid #000000; border-bottom:1 solid #000000;' style='line-height: 140%' width='84' align='right'>" + num5.ToString("###,###,###,###,##0") + "&nbsp;</td>\r\n\t\t                    <td style='border-top:1 solid #000000; border-bottom:1 solid #000000;' style='line-height: 140%' width='84' align='right'>" + num6.ToString("###,###,###,###,##0") + "&nbsp;</td>\r\n\t\t                    <td style='border-top:1 solid #000000; border-bottom:1 solid #000000;' style='line-height: 140%' width='97' align='right'>" + num7.ToString("###,###,###,###,##0") + "&nbsp;</td>\r\n\t\t                    <td style='border-top:1 solid #000000; border-bottom:1 solid #000000;' style='line-height: 140%' height='25' width='77' align='right'>" + num8.ToString("###,###,###,###,##0") + "&nbsp;</td>\r\n\t                    </tr>\r\n                    </table>\r\n                    <br>\t\r\n                    <br>\r\n                    <table width='945' border='0' cellpadding='0' cellspacing='0' style='font-size: 9pt' height='60'>\r\n\t                    <colgroup width='25%' align='center'></colgroup>\r\n\t                    <colgroup width='25%' align='center'></colgroup>\r\n\t                    <colgroup width='25%' align='center'></colgroup>\r\n\t                    <colgroup width='25%' align='center'></colgroup>\r\n\t                    <tr>\r\n\t\t                    <td style='border-left-width:1; border-left-style:solid; border-top-style:solid; border-top-width:1px' style='line-height: 140%' align='left' bordercolor='#000000' valign='middle'>\r\n\t\t                    <p>&nbsp;&nbsp; 1. 귀사의 적극적인 업무협조에 감사드립니다.</p>\r\n\t\t                    </td>\r\n\t\t                    <td style='border-right-style:solid; border-right-width:1px; border-top-style:solid; border-top-width:1px' style='line-height: 140%' align='left' bordercolor='#000000' valign='middle'>\r\n\t\t                    <p>&nbsp;&nbsp; 3. 지정한 장소에 납품 바랍니다.</p>\r\n\t\t                    </td>\r\n\t\t                    </tr>\r\n\t                    <tr>\r\n\t\t                    <td style='border-bottom:1 solid #000000; border-left-style:solid; border-left-width:1px' style='line-height: 140%' align='left' bordercolor='#000000' valign='middle'>\r\n\t\t                    &nbsp;&nbsp;\r\n\t\t\t                    2. 상기와 같이 발주하오니, 납기 준수바라오며,</td>\r\n\t\t                    <td style='border-bottom:1 solid #000000; border-right-style:solid; border-right-width:1px' style='line-height: 140%' align='left' bordercolor='#000000' valign='middle'>\r\n\t\t                    <p>&nbsp;&nbsp; 4. 당사 생산계획 변경시 수량 및 입고 요청일이 변경될 수 있습니다.</p>\r\n\t\t                    </td>\r\n\t\t                    </tr>\r\n                    </table> \r\n                </body>\r\n                </center>\r\n                ";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            return string.Empty;
        }

        private string 전자결재양식생성_디엠티()
        {
            string path = Application.StartupPath + "\\download\\gw\\HT_P_PO_REG2_DMT.htm";
            string str1 = "";
            using (StreamReader streamReader = new StreamReader(path, Encoding.Default))
                str1 = streamReader.ReadToEnd();
            DataTable dataTable = this._biz.DataSearch_GW_RPT(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                             this._header.CurrentRow["NO_PO"].ToString() });
            if (dataTable == null || dataTable.Rows.Count == 0)
                return "";
            string str2 = str1.Replace("@@거래처", D.GetString(dataTable.Rows[0]["LN_PARTNER"]) + "&nbsp;").Replace("@@거래_대표자", D.GetString(dataTable.Rows[0]["NM_CEO1"]) + "&nbsp;").Replace("@@거래_주소", D.GetString(dataTable.Rows[0]["ADS1"]) + " " + D.GetString(dataTable.Rows[0]["ADS_DETAIL1"]) + "&nbsp;").Replace("@@거래_전화", D.GetString(dataTable.Rows[0]["NO_TEL1"]) + "&nbsp;").Replace("@@거래_팩스", D.GetString(dataTable.Rows[0]["NO_FAX1"]) + "&nbsp;").Replace("@@거래_담당자", D.GetString(dataTable.Rows[0]["NM_PTR1"]) + "&nbsp;").Replace("@@발주번호", D.GetString(dataTable.Rows[0]["NO_PO"]) + "&nbsp;").Replace("@@발주일자", D.GetString(dataTable.Rows[0]["DT_PO"]) + "&nbsp;").Replace("@@비고", D.GetString(dataTable.Rows[0]["DC_RMK_TEXT"]).Replace("\n", "<br>") + "&nbsp;").Replace("@@환종", D.GetString(dataTable.Rows[0]["NM_EXCH"]) + "&nbsp;").Replace("@@환율", D.GetString(dataTable.Rows[0]["RT_EXCH"]) + "&nbsp;").Replace("@@사업자등록번호", D.GetString(dataTable.Rows[0]["BIZ_NUM"]) + "&nbsp;").Replace("@@대표자", D.GetString(dataTable.Rows[0]["NM_CEO"]) + "&nbsp;").Replace("@@주소", D.GetString(dataTable.Rows[0]["ADS"]) + " " + D.GetString(dataTable.Rows[0]["ADS_DETAIL"]) + "&nbsp;").Replace("@@전화", D.GetString(dataTable.Rows[0]["NO_TEL"]) + "&nbsp;").Replace("@@팩스", D.GetString(dataTable.Rows[0]["NO_FAX"]) + "&nbsp;").Replace("@@담당자", D.GetString(dataTable.Rows[0]["NM_PTR"]) + "&nbsp;").Replace("@@지급조건", D.GetString(dataTable.Rows[0]["NM_FG_PAYMENT"]) + "&nbsp;");
            string newValue = string.Empty;
            int num1 = 0;
            decimal num2 = 0M;
            foreach (DataRow row in dataTable.Rows)
            {
                ++num1;
                string str3 = D.GetString(row["CD_ITEM"]);
                string str4 = D.GetString(row["NM_ITEM"]);
                string str5 = D.GetString(row["STND_ITEM"]);
                string str6 = D.GetString(row["UNIT_IM"]);
                string str7 = row["DT_LIMIT"].ToString().Substring(0, 4) + "." + row["DT_LIMIT"].ToString().Substring(4, 2) + "." + row["DT_LIMIT"].ToString().Substring(6, 2);
                string str8 = D.GetDecimal(row["QT_PO_MM"]).ToString("###,###,###,###,##0");
                string str9 = D.GetDecimal(row["UM_EX_PO"]).ToString("###,###,###,###,##0.####");
                string str10 = D.GetDecimal(row["AM_EX"]).ToString("###,###,###,###,##0.####");
                string str11 = D.GetString(row["DC1"]);
                num2 += D.GetDecimal(row["AM_EX"]);
                newValue = newValue + " \r\n                    <tr height='30'>\r\n                        <td align='center' style='border-top: 1px solid #000000; border-left: 1px solid #000000'>\r\n                        " + num1 + "&nbsp;</td>\r\n                        <td align='center' style='border-top: 1px solid #000000; border-left: 1px solid #000000'>\r\n                        " + str3 + "&nbsp;</td>\r\n                        <td  align='center' style='border-top: 1px solid #000000; border-left: 1px solid #000000'>\r\n                        " + str4 + "&nbsp;</td> \r\n                        <td  align='center' style='border-top: 1px solid #000000; border-left: 1px solid #000000'>\r\n                        " + str5 + "&nbsp;</td>\r\n                        <td align='center' style='border-top: 1px solid #000000;  border-left: 1px solid #000000'>\r\n                        " + str6 + "&nbsp;</td>\r\n                        <td align='center' style='border-top: 1px solid #000000;  border-left: 1px solid #000000'>\r\n                        " + str7 + "&nbsp;</td>\r\n                        <td align='center' style='border-top: 1px solid #000000;  border-left: 1px solid #000000'>\r\n                        " + str9 + "&nbsp;</td>\r\n                        <td align='center' style='border-top: 1px solid #000000;  border-left: 1px solid #000000'>\r\n                        " + str8 + "&nbsp;</td>\r\n                        <td align='center' style='border-top: 1px solid #000000;  border-left: 1px solid #000000'>\r\n                        " + str10 + "&nbsp;</td>\r\n                        <td align='center' style='border-top: 1px solid #000000;  border-right: 1px solid #000000; border-left: 1px solid #000000'>\r\n                        " + str11 + "&nbsp;</td>\r\n                    </tr>";
            }
            return str2.Replace("@@LINEDATA", newValue).Replace("@@SUM_AM_EX", num2.ToString("###,###,###,###,##0") + "&nbsp;");
        }

        private string 전자결재양식생성_피앤이()
        {
            string path = Application.StartupPath + "\\download\\gw\\HT_P_PO_REG2_PNE.htm";
            string str1 = "";
            using (StreamReader streamReader = new StreamReader(path, Encoding.Default))
                str1 = streamReader.ReadToEnd();
            DataTable dataTable = this._biz.DataSearch_GW_RPT(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                             this._header.CurrentRow["NO_PO"].ToString() });
            if (dataTable == null || dataTable.Rows.Count == 0)
                return "";
            string str2 = str1.Replace("@@거래처명", D.GetString(dataTable.Rows[0]["LN_PARTNER"]) + "&nbsp;").Replace("@@공장", D.GetString(dataTable.Rows[0]["NM_PLANT"]) + "&nbsp;").Replace("@@거래_대표자", D.GetString(dataTable.Rows[0]["NM_CEO1"]) + "&nbsp;").Replace("@@거래_주소", D.GetString(dataTable.Rows[0]["ADS1"]) + " " + D.GetString(dataTable.Rows[0]["ADS_DETAIL1"]) + "&nbsp;").Replace("@@거래_전화", D.GetString(dataTable.Rows[0]["NO_TEL1"]) + "&nbsp;").Replace("@@거래_팩스", D.GetString(dataTable.Rows[0]["NO_FAX1"]) + "&nbsp;").Replace("@@거래_담당자", D.GetString(dataTable.Rows[0]["NM_PTR1"]) + "&nbsp;").Replace("@@발주번호", D.GetString(dataTable.Rows[0]["NO_PO"]) + "&nbsp;").Replace("@@발주일자", D.GetString(dataTable.Rows[0]["DT_PO"]) + "&nbsp;").Replace("@@비고", D.GetString(dataTable.Rows[0]["DC50_PO"]) + "&nbsp;").Replace("@@환종", D.GetString(dataTable.Rows[0]["NM_EXCH"]) + "&nbsp;").Replace("@@환율", D.GetString(dataTable.Rows[0]["RT_EXCH"]) + "&nbsp;").Replace("@@사업자등록번호", D.GetString(dataTable.Rows[0]["BIZ_NUM"]) + "&nbsp;").Replace("@@대표자", D.GetString(dataTable.Rows[0]["NM_CEO"]) + "&nbsp;").Replace("@@주소", D.GetString(dataTable.Rows[0]["ADS"]) + " " + D.GetString(dataTable.Rows[0]["ADS_DETAIL"]) + "&nbsp;").Replace("@@전화", D.GetString(dataTable.Rows[0]["NO_TEL"]) + "&nbsp;").Replace("@@팩스", D.GetString(dataTable.Rows[0]["NO_FAX"]) + "&nbsp;").Replace("@@담당자", D.GetString(dataTable.Rows[0]["NM_PTR"]) + "&nbsp;");
            int num = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                ++num;
                string str3 = D.GetString(row["CD_ITEM"]);
                string str4 = D.GetString(row["NM_ITEM"]);
                string str5 = D.GetString(row["STND_ITEM"]);
                string str6 = D.GetString(row["UNIT_IM"]);
                string str7 = row["DT_LIMIT"].ToString().Substring(0, 4) + "." + row["DT_LIMIT"].ToString().Substring(4, 2) + "." + row["DT_LIMIT"].ToString().Substring(6, 2);
                string str8 = D.GetDecimal(row["QT_PO_MM"]).ToString("###,###,###,###,##0");
                string str9 = D.GetDecimal(row["UM_EX_PO"]).ToString("###,###,###,###,##0.####");
                string str10 = D.GetDecimal(row["AM_EX"]).ToString("###,###,###,###,##0.####");
                string str11 = D.GetString(row["DC1"]);
                str2 = str2 + "<tr height='25'>\r\n\t                         <td align='center'>\r\n\t                         " + num + "&nbsp;</td>\r\n\t                         <td align='center' style='border-left-style: solid; border-left-width: 1px'>\r\n\t                         " + str3 + "&nbsp;</td>\r\n\t                         <td align='center' style='border-left-style: solid; border-left-width: 1px'>\r\n\t                         " + str4 + "&nbsp;</td>\r\n\t                         <td align='center' style='border-left-style: solid; border-left-width: 1px'>\r\n\t                         " + str5 + "&nbsp;</td>\r\n\t                         <td align='center' style='border-left-style: solid; border-left-width: 1px'>\r\n\t                         " + str6 + "&nbsp;</td>\r\n\t                         <td align='center' style='border-left-style: solid; border-left-width: 1px'>\r\n\t                         " + str7 + "&nbsp;</td>\r\n\t                         <td align='center' style='border-left-style: solid; border-left-width: 1px'>\r\n\t                         " + str9 + "&nbsp;</td>\r\n\t                         <td align='center' style='border-left-style: solid; border-left-width: 1px'>\r\n\t                         " + str8 + "&nbsp;</td>\r\n\t                         <td align='center' style='border-left-style: solid; border-left-width: 1px'>\r\n\t                         " + str10 + "&nbsp;</td>\r\n\t                         <td align='center' style='border-left-style: solid; border-left-width: 1px'>\r\n\t                         " + str11 + "&nbsp;</td>\r\n                             </tr>";
            }
            return str2;
        }

        private string[] 전자결재양식생성_세미테크()
        {
            string[] strArray = new string[2];
            string path = Application.StartupPath + "\\download\\gw\\HT_P_PO_REG2_SEMITEC.htm";
            string str1 = "";
            using (StreamReader streamReader = new StreamReader(path, Encoding.Default))
                str1 = streamReader.ReadToEnd();
            DataTable dataTable = this._biz.DataSearch_GW_RPT_ONLY(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                  this._header.CurrentRow["NO_PO"].ToString(),
                                                                                  "",
                                                                                  "",
                                                                                  "SEMI" });
            if (dataTable == null || dataTable.Rows.Count == 0)
                return strArray;
            string newValue1 = string.Empty;
            int num1 = 0;
            decimal num2 = 0M;
            decimal num3 = 0M;
            foreach (DataRow row in dataTable.Rows)
            {
                ++num1;
                string str2 = D.GetString(row["CD_ITEM"]);
                string str3 = D.GetString(row["NM_ITEM"]);
                string str4 = D.GetString(row["STND_ITEM"]);
                string str5;
                if (D.GetString(row["DT_LIMIT"]) != string.Empty)
                    str5 = D.GetString(row["DT_LIMIT"]).Substring(2, 2) + "/" + D.GetString(row["DT_LIMIT"]).Substring(4, 2) + "/" + D.GetString(row["DT_LIMIT"]).Substring(6, 2);
                else
                    str5 = string.Empty;
                string str6 = D.GetString(row["UNIT_IM"]);
                decimal num4 = D.GetDecimal(row["QT_PO_MM"]);
                string str7 = num4.ToString("#,###,###,##0.####");
                num4 = D.GetDecimal(row["UM_EX_PO"]);
                string str8 = num4.ToString("#,###,###,##0.####");
                num4 = D.GetDecimal(row["AM"]);
                string str9 = num4.ToString("#,###,###,##0.####");
                string str10 = D.GetString(row["CD_PJT"]);
                string str11 = D.GetString(row["NM_PROJECT"]);
                string str12 = D.GetString(row["NO_SO"]);
                string str13;
                if (D.GetString(row["DT_DUEDATE"]) != string.Empty)
                    str13 = D.GetString(row["DT_DUEDATE"]).Substring(2, 2) + "/" + D.GetString(row["DT_DUEDATE"]).Substring(4, 2) + "/" + D.GetString(row["DT_DUEDATE"]).Substring(6, 2);
                else
                    str13 = string.Empty;
                num4 = D.GetDecimal(row["QT_SO"]);
                string str14 = num4.ToString("#,###,###,##0.####");
                D.GetString(row["DC1"]);
                string str15 = D.GetString(row["NM_EXCH"]);
                string str16 = D.GetString(row["RT_EXCH"]);
                num2 += D.GetDecimal(row["QT_PO_MM"]);
                num3 += D.GetDecimal(row["AM"]);
                if (str15 != "KRW")
                    str8 = str8 + "[" + str15 + "/" + str16 + "]";
                newValue1 = newValue1 + "<tr height='25'>\r\n\t\t\t                            <td style='font-size:9pt'>" + num1 + "</td>\r\n\t\t\t                            <td style='font-size:9pt'>" + str2 + "</td>\r\n\t\t\t                            <td style='font-size:9pt'>" + str3 + "</td>\r\n\t\t\t                            <td style='font-size:9pt'>" + str4 + "</td>\r\n\t\t\t                            <td style='font-size:9pt'>" + str5 + "</td>\r\n\t\t\t                            <td style='font-size:9pt'>" + str7 + "</td>\r\n\t\t\t                            <td style='font-size:9pt'>" + str6 + "</td>\r\n\t\t\t                            <td style='font-size:9pt'>" + str8 + "</td>\r\n\t\t\t                            <td style='font-size:9pt'>" + str9 + "</td>\r\n\t\t\t                            <td style='font-size:9pt'>" + str10 + "/" + str11 + "</td>\r\n\t\t\t                            <td style='font-size:9pt'>" + str12 + "</td>\r\n\t\t\t                            <td style='font-size:9pt'>" + str14 + "</td>\r\n\t\t\t                            <td style='font-size:9pt'>" + str13 + "</td>\r\n\t\t                            </tr>";
            }
            string str17 = str1.Replace("@@LINEDATA", newValue1).Replace("@@NO_PO", D.GetString(dataTable.Rows[0]["NO_PO"]) + "&nbsp;");
            string newValue2;
            if (D.GetString(dataTable.Rows[0]["DT_PO"]) != string.Empty)
                newValue2 = D.GetString(dataTable.Rows[0]["DT_PO"]).Substring(0, 4) + "/" + D.GetString(dataTable.Rows[0]["DT_PO"]).Substring(4, 2) + "/" + D.GetString(dataTable.Rows[0]["DT_PO"]).Substring(6, 2);
            else
                newValue2 = string.Empty;
            string str18 = str17.Replace("@@DT_PO", newValue2).Replace("@@NM_DEPT", D.GetString(dataTable.Rows[0]["NM_DEPT"]) + "&nbsp;").Replace("@@NM_EMP", D.GetString(dataTable.Rows[0]["NO_EMP"]) + "[" + D.GetString(dataTable.Rows[0]["NM_PTR"]) + "]&nbsp;").Replace("@@CD_PARTNER", D.GetString(dataTable.Rows[0]["CD_PARTNER"]) + "[" + D.GetString(dataTable.Rows[0]["LN_PARTNER"]) + "]&nbsp;").Replace("@@NM_FG_PAYMENT", D.GetString(dataTable.Rows[0]["FG_PAYMENT"]) + "[" + D.GetString(dataTable.Rows[0]["NM_FG_PAYMENT"]) + "]&nbsp;").Replace("@@SUM_GW_GW_QT_PO_MM", num2.ToString("#,###,###,##0.####") + "&nbsp;").Replace("@@SUM_GW_AM", num3.ToString("#,###,###,##0.####") + "&nbsp;").Replace("@@DC50_PO", D.GetString(dataTable.Rows[0]["DC50_PO"]) + "&nbsp;");
            strArray[0] = str18;
            strArray[1] = dataTable.Rows[0]["CD_PJT"].ToString() + "_구매발주결재";
            return strArray;
        }

        private void btn_PRJ_SUB_Click(object sender, EventArgs e)
        {
            if (!this.HeaderCheck(1))
                return;
            try
            {
                P_PU_REG_PRJ_SUB pPuRegPrjSub = new P_PU_REG_PRJ_SUB(new string[] { this.ctx프로젝트.CodeValue,
                                                                                    this.ctx프로젝트.CodeName,
                                                                                    this.ctx거래처.CodeValue,
                                                                                    this.ctx거래처.CodeName }, this._flexD.DataTable);
                if (((Form)pPuRegPrjSub).ShowDialog() == DialogResult.OK)
                {
                    bool 평형비고체크 = pPuRegPrjSub.평형비고체크;
                    this.InserGridtAdd(pPuRegPrjSub.gdt_return, 평형비고체크);
                    this.ctx프로젝트.Enabled = false;
                    this.btnPJT적용.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._flexD.DataTable == null || this.ctx프로젝트.CodeName.ToString() == "" || !this.확정여부())
                    return;
                DataTable dataTable = null;
                if (Global.MainFrame.ServerKeyCommon.ToUpper() == "ZOONE")
                    dataTable = this._biz.GetInfo_ZOO(this.ctx프로젝트.CodeValue);
                for (int index = this._flexD.Rows.Fixed; index < this._flexD.Rows.Count; ++index)
                {
                    if (this._flexD.RowState(index) != DataRowState.Deleted)
                    {
                        this._flexD.Rows[index]["CD_PJT"] = this.ctx프로젝트.CodeValue;
                        this._flexD.Rows[index]["NM_PJT"] = this.ctx프로젝트.CodeName;
                        this._flexD.Rows[index]["SEQ_PROJECT"] = 0;
                        if (Config.MA_ENV.YN_UNIT == "Y" || BASIC.GetMAEXC("구매-프로젝트도움창 UNIT사용여부") == "100" && Config.MA_ENV.프로젝트사용)
                        {
                            this._flexD.Rows[index]["SEQ_PROJECT"] = this.d_SEQ_PROJECT;
                            this._flexD.Rows[index]["CD_PJT_ITEM"] = this.s_CD_PJT_ITEM;
                            this._flexD.Rows[index]["NM_PJT_ITEM"] = this.s_NM_PJT_ITEM;
                            this._flexD.Rows[index]["PJT_ITEM_STND"] = this.s_PJT_ITEM_STND;
                        }
                        if (Global.MainFrame.ServerKeyCommon == "UNIPOINT")
                        {
                            this._flexD.Rows[index]["LN_PARTNER_PJT"] = this.s_NM_PARTNER_PJT;
                            this._flexD.Rows[index]["CD_PARTNER_PJT"] = this.s_CD_PARTNER_PJT;
                            this._flexD.Rows[index]["NO_EMP_PJT"] = this.s_NO_EMP_PJT;
                            this._flexD.Rows[index]["NM_KOR_PJT"] = this.s_NM_EMP_PJT;
                            this._flexD.Rows[index]["END_USER"] = this.s_END_USER;
                        }
                        if (Global.MainFrame.ServerKeyCommon.ToUpper() == "CHOSUNHOTELBA")
                        {
                            this.FillPol(index);
                            this.품목정보구하기(new object[] { D.GetString(this._flexD.Rows[index]["CD_ITEM"]),
                                                               D.GetString(this.cbo공장.SelectedValue),
                                                               this.LoginInfo.CompanyCode,
                                                               this.cbo단가유형.SelectedValue.ToString(),
                                                               this.cbo환정보.SelectedValue.ToString(),
                                                               this.dtp발주일자.Text,
                                                               this.ctx거래처.CodeValue,
                                                               this.ctx구매그룹.CodeValue,
                                                               "N",
                                                               this.ctx프로젝트.CodeValue,
                                                               Global.MainFrame.ServerKeyCommon.ToUpper() }, "GRID", index);
                        }
                        if (this.m_sEnv_CC == "200")
                            this.SetCC(index, string.Empty);
                        if (Global.MainFrame.ServerKeyCommon.ToUpper() == "ZOONE" && dataTable != null && dataTable.Rows.Count != 0)
                        {
                            this._flexD.Rows[index]["CD_SL"] = dataTable.Rows[0]["CD_SL"];
                            this._flexD.Rows[index]["NM_SL"] = dataTable.Rows[0]["NM_SL"];
                        }
                        if (BASIC.GetMAEXC("발주등록(공장)-프로젝트별_의제매입세_구분") == "100" && D.GetString(this._flexD.Rows[index]["CD_USERDEF14"]) == "001")
                        {
                            string str = this._biz.pjt_item_josun(D.GetString(this._flexD.Rows[index]["CD_PJT"]));
                            if (str != "")
                            {
                                this._flexD.Rows[index]["FG_TAX"] = str;
                                this._flexD.Rows[index]["RATE_VAT"] = 0;
                                decimal num1 = 0M;
                                this._flexD.Rows[index]["RATE_VAT"] = num1;
                                decimal num2 = num1 == 0M ? 0M : num1 / 100M;
                                if (num2 == 0M || Convert.ToDecimal(this._flexD.Rows[index]["AM"]) == 0M)
                                    this._flexD.Rows[index]["VAT"] = 0;
                                else
                                    this._flexD.Rows[index]["VAT"] = Unit.원화금액(DataDictionaryTypes.PU, Convert.ToDecimal(this._flexD.Rows[index]["AM"]) * num2);
                                this._flexD.Rows[index]["AM_TOTAL"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flexD.Rows[index]["AM"]) + D.GetDecimal(this._flexD.Rows[index]["VAT"]));
                            }
                            else
                            {
                                this._flexD.Rows[index]["FG_TAX"] = this._header.CurrentRow["FG_TAX"];
                                this._flexD.Rows[index]["RATE_VAT"] = this.cur부가세율.DecimalValue;
                                decimal DecimalValue = this.cur부가세율.DecimalValue;
                                this._flexD.Rows[index]["RATE_VAT"] = DecimalValue;
                                decimal num = DecimalValue == 0M ? 0M : DecimalValue / 100M;
                                if (num == 0M || Convert.ToDecimal(this._flexD.Rows[index]["AM"]) == 0M)
                                    this._flexD.Rows[index]["VAT"] = 0;
                                else
                                    this._flexD.Rows[index]["VAT"] = Unit.원화금액(DataDictionaryTypes.PU, Convert.ToDecimal(this._flexD.Rows[index]["AM"]) * num);
                                this._flexD.Rows[index]["AM_TOTAL"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flexD.Rows[index]["AM"]) + D.GetDecimal(this._flexD.Rows[index]["VAT"]));
                            }
                        }
                    }
                }
                this.ShowMessage("적용작업을완료하였습니다");
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn_H41_Apply_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.HeaderCheck(0))
                    return;
                this.호출여부 = true;
                P_PU_H41_SUB pPuH41Sub = new P_PU_H41_SUB(this._flexD.DataTable, D.GetString(this.cbo공장.SelectedValue), D.GetString(this.cbo환정보.SelectedValue));
                Cursor.Current = Cursors.Default;
                if (((Form)pPuH41Sub).ShowDialog(this) == DialogResult.OK)
                {
                    DataTable gdtReturn = pPuH41Sub.gdt_return;
                    if (gdtReturn == null || gdtReturn.Rows.Count <= 0)
                        return;
                    this.ControlButtonEnabledDisable((Control)sender, true);
                    this.cbo공장.Enabled = false;
                    this._flexD.Redraw = false;
                    decimal maxValue = this._flexD.GetMaxValue("NO_LINE");
                    for (int index = 0; index < gdtReturn.Rows.Count; ++index)
                    {
                        if (gdtReturn.Rows[index].RowState != DataRowState.Deleted)
                        {
                            ++maxValue;
                            this._flexD.Rows.Add();
                            this._flexD.Row = this._flexD.Rows.Count - 1;
                            this._flexD["CD_ITEM"] = gdtReturn.Rows[index]["CD_ITEM"];
                            this._flexD["NM_ITEM"] = gdtReturn.Rows[index]["NM_ITEM"];
                            this._flexD["STND_ITEM"] = gdtReturn.Rows[index]["STND_ITEM"];
                            this._flexD["CD_UNIT_MM"] = gdtReturn.Rows[index]["UNIT_PO"];
                            this._flexD["UNIT_PO"] = gdtReturn.Rows[index]["UNIT_PO"];
                            this._flexD["STND_MA_ITEM"] = gdtReturn.Rows[index]["STND_ITEM"];
                            this._flexD["UNIT_IM"] = gdtReturn.Rows[index]["UNIT_IM"];
                            this._flexD["DT_LIMIT"] = !(this.dtp납기일.Text == string.Empty) ? this.dtp납기일.Text : gdtReturn.Rows[index]["DT_LIMIT"];
                            this._flexD["DT_PLAN"] = this._flexD["DT_LIMIT"];
                            this._flexD["FG_POST"] = "O";
                            this._flexD["NM_SYSDEF"] = this._biz.GetGubunCodeSearch("PU_C000009", this._flexD["FG_POST"].ToString());
                            this._flexD["QT_PO_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(gdtReturn.Rows[index]["QT_PO_MM"]));
                            this._flexD["RT_PO"] = gdtReturn.Rows[index]["RT_PO"];
                            this._flexD["CD_PLANT"] = this.cbo공장.SelectedValue.ToString();
                            if (this.txt발주번호.Text != string.Empty)
                                this._flexD["NO_PO"] = this.txt발주번호.Text;
                            this._flexD["NO_RCV"] = "";
                            this._flexD["NO_LINE"] = maxValue;
                            this._flexD["CD_SL"] = gdtReturn.Rows[index]["CD_SL"];
                            this._flexD["NM_SL"] = gdtReturn.Rows[index]["NM_SL"];
                            this._flexD["CD_EXCH"] = gdtReturn.Rows[index]["CD_EXCH"];
                            this._flexD["DC1"] = gdtReturn.Rows[index]["DC1"];
                            this._flexD["DC2"] = gdtReturn.Rows[index]["DC2"];
                            this._flexD["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(gdtReturn.Rows[index]["UM_EX"]));
                            this._flexD["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(gdtReturn.Rows[index]["UM_EX"]));
                            this._flexD["NO_PR"] = "";
                            this.dtp발주일자.Text = gdtReturn.Rows[index]["DT_PO"].ToString();
                            this.SetCC(this._flexD.Row, string.Empty);
                            foreach (DataRow row in ((DataTable)this.cbo환정보.DataSource).Rows)
                            {
                                if (row["CODE"].ToString() == this.cbo환정보.SelectedValue.ToString())
                                {
                                    this._flexD["NM_EXCH"] = row["NAME"];
                                    break;
                                }
                            }
                            if (this._flexD.CDecimal(this._flexD["RT_PO"]) == 0M)
                                this._flexD["RT_PO"] = 1;
                            this._flexD["QT_PO"] = Unit.수량(DataDictionaryTypes.PU, this._flexD.CDecimal(gdtReturn.Rows[index]["QT_PO_MM"]) * this._flexD.CDecimal(this._flexD["RT_PO"]));
                            this.FillPol(this._flexD.Row);
                            this.품목정보구하기(new object[] { this._flexD["CD_ITEM"].ToString(),
                                                               this._flexD["CD_PLANT"].ToString(),
                                                               this.LoginInfo.CompanyCode,
                                                               this.cbo단가유형.SelectedValue.ToString(),
                                                               this.cbo환정보.SelectedValue.ToString(),
                                                               this.dtp발주일자.Text,
                                                               this.ctx거래처.CodeValue,
                                                               this.ctx구매그룹.CodeValue,
                                                               "N",
                                                               D.GetString(this._flexD["CD_PJT"]),
                                                               Global.MainFrame.ServerKeyCommon.ToUpper() }, "H41", 0);
                            decimal num = this._flexD.CDecimal(this.cur부가세율.DecimalValue) == 0M ? 0M : this._flexD.CDecimal(this.cur부가세율.DecimalValue) / 100M;
                            this._flexD["AM"] = Unit.원화금액(DataDictionaryTypes.PU, this._flexD.CDecimal(this._flexD["AM_EX"]) * this.cur환정보.DecimalValue);
                            this._flexD["VAT"] = Unit.원화금액(DataDictionaryTypes.PU, this._flexD.CDecimal(this._flexD["AM"]) * num);
                            this._flexD["AM_TOTAL"] = (this._flexD.CDecimal(this._flexD["AM"]) + this._flexD.CDecimal(this._flexD["VAT"]));
                            this._flexD.AddFinished();
                            this._flexD.Col = this._flexD.Cols.Fixed;
                        }
                    }
                    this._header.CurrentRow["DC50_PO"] = gdtReturn.Rows[gdtReturn.Rows.Count - 1]["INVOICE_NUMBER"];
                    this.txt비고.Text = this._header.CurrentRow["DC50_PO"].ToString();
                    this.SUMFunction();
                    this.SetHeadControlEnabled(false, 1);
                    this.btn품목추가.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this._flexD.Redraw = true;
            }
        }

        private void btn_Mail_Click(object sender, EventArgs e)
        {
            this.Tp_print = "MAIL";
            this.SetPrint(false);
        }

        private void btn_lotsize_accept_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.확정여부())
                    return;
                DataRow[] dataRowArray1 = this._flexD.DataTable.Select("S = 'Y'");
                string 공장 = D.GetString(this.cbo공장.SelectedValue);
                string 멀티품목코드 = string.Empty;
                if (dataRowArray1 == null || dataRowArray1.Length == 0)
                    return;
                foreach (DataRow dataRow in dataRowArray1)
                    멀티품목코드 = 멀티품목코드 + dataRow["CD_ITEM"] + "|";
                string[] pipes = D.StringConvert.GetPipes(멀티품목코드, 200);
                DataTable dataTable1 = null;
                for (int index = 0; index < pipes.Length; ++index)
                {
                    DataTable dataTable2 = this._biz.공장품목(멀티품목코드, 공장);
                    if (index == 0)
                        dataTable1 = dataTable2.Copy();
                    else if (index > 0)
                    {
                        foreach (DataRow row in dataTable2.Rows)
                            dataTable1.LoadDataRow(row.ItemArray, true);
                    }
                }
                for (int row = this._flexD.Rows.Fixed; row < this._flexD.Rows.Count; ++row)
                {
                    if (!(D.GetString(this._flexD.Rows[row]["S"]) == "N") && !(D.GetDecimal(this._flexD.Rows[row]["QT_PO_MM"]) == 0M))
                    {
                        DataRow[] dataRowArray2 = dataTable1.Select("CD_ITEM ='" + D.GetString(this._flexD.Rows[row]["CD_ITEM"]) + "'");
                        decimal num1 = 0M;
                        if (dataRowArray2 != null && dataRowArray2.Length > 0)
                        {
                            decimal d1 = D.GetDecimal(dataRowArray2[0]["LOTSIZE"]);
                            decimal d2 = D.GetDecimal(this._flexD.Rows[row]["QT_PO_MM"]);
                            if (D.GetInt(Math.Floor(d2)) != 0 && D.GetInt(Math.Floor(d1)) != 0)
                                num1 = !(d2 % d1 == 0M) ? (decimal)(D.GetInt(Math.Floor(d2)) / D.GetInt(Math.Floor(d1)) + 1) : (decimal)(D.GetInt(Math.Floor(d2)) / D.GetInt(Math.Floor(d1)));
                            decimal num2 = d1 * num1;
                            this._flexD.Rows[row]["QT_PO_MM"] = num2;
                            this.금액계산(row, D.GetDecimal(this._flexD.Rows[row]["UM_EX_PO"]), num2, "QT_PO_MM", num2);
                            this._flexD[row, "QT_WEIGHT"] = (num2 * this._flexD.CDecimal(this._flexD[row, "WEIGHT"]));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn_UM_APP_Click(object sender, EventArgs e)
        {
            if (!this._flexD.HasNormalRow || !this.확정여부())
                return;
            string str = string.Empty;
            P_PU_UM_PRIORITIZE_PO_SUB umPrioritizePoSub = new P_PU_UM_PRIORITIZE_PO_SUB(Settings1.Default.PARAMETER_um);
            if (((Form)umPrioritizePoSub).ShowDialog() != DialogResult.OK)
                return;
            Settings1.Default.PARAMETER_um = umPrioritizePoSub.Rtn_stting;
            Settings1.Default.Save();
            str = umPrioritizePoSub.Rtn_stting;
            this.Grid_um_apply(umPrioritizePoSub.Rtn_dt);
        }

        private void Grid_um_apply(DataTable dt_um_p)
        {
            try
            {
                string str = string.Empty;
                foreach (DataRow row in this._flexD.DataTable.Rows)
                    str = str + row["CD_ITEM"] + "|";
                DataSet dataSet = this._biz.Search_um_prioritize_item(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                     D.GetString( this.dtp발주일자.Text),
                                                                                     D.GetString(this.cbo공장.SelectedValue),
                                                                                     str });
                if (dataSet.Tables[0].Rows.Count == 0 && dataSet.Tables[1].Rows.Count == 0 && dataSet.Tables[2].Rows.Count == 0 && dataSet.Tables[3].Rows.Count == 0)
                    return;
                int row1 = this._flexD.Rows.Fixed;
                foreach (DataRow row2 in this._flexD.DataTable.Rows)
                {
                    if (!(D.GetString(row2["CD_ITEM"]) == ""))
                    {
                        foreach (DataRow row3 in dt_um_p.Rows)
                        {
                            if (D.GetString(row3["CODE"]) == "INV")
                            {
                                DataRow[] dataRowArray = dataSet.Tables[0].Select("CD_ITEM ='" + D.GetString(row2["CD_ITEM"]) + "'");
                                if (dataRowArray.Length > 0)
                                {
                                    if (D.GetString(row3["MAXMIN"]) == "MAX")
                                    {
                                        row2["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MAX"]));
                                        row2["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, UDecimal.Getdivision(D.GetDecimal(dataRowArray[0]["UM_ITEM_MAX"]), D.GetDecimal(this.cur환정보.Text)));
                                        row2["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row2["UM_EX"]) * D.GetDecimal(row2["RT_PO"]));
                                    }
                                    else
                                    {
                                        row2["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MIN"]));
                                        row2["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, UDecimal.Getdivision(D.GetDecimal(dataRowArray[0]["UM_ITEM_MIN"]), D.GetDecimal(this.cur환정보.Text)));
                                        row2["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row2["UM_EX"]) * D.GetDecimal(row2["RT_PO"]));
                                    }
                                }
                                if (D.GetDecimal(row2["UM_EX"]) != 0M)
                                    break;
                            }
                            else if (D.GetString(row3["CODE"]) == "IVL")
                            {
                                DataRow[] dataRowArray = dataSet.Tables[1].Select("CD_ITEM ='" + D.GetString(row2["CD_ITEM"]) + "'");
                                if (dataRowArray.Length > 0)
                                {
                                    if (D.GetString(row3["MAXMIN"]) == "MAX")
                                    {
                                        row2["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MAX"]) * D.GetDecimal(this.cur환정보.Text));
                                        row2["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MAX"]));
                                        row2["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MAX"]) * D.GetDecimal(row2["RT_PO"]));
                                    }
                                    else
                                    {
                                        row2["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MIN"]) * D.GetDecimal(this.cur환정보.Text));
                                        row2["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MIN"]));
                                        row2["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MIN"]) * D.GetDecimal(row2["RT_PO"]));
                                    }
                                }
                                if (D.GetDecimal(row2["UM_EX"]) != 0M)
                                    break;
                            }
                            else if (D.GetString(row3["CODE"]) == "APRT")
                            {
                                DataRow[] dataRowArray = dataSet.Tables[2].Select("CD_ITEM ='" + D.GetString(row2["CD_ITEM"]) + "'");
                                if (dataRowArray.Length > 0)
                                {
                                    if (D.GetString(row3["MAXMIN"]) == "MAX")
                                    {
                                        row2["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MAX"]) * D.GetDecimal(this.cur환정보.Text));
                                        row2["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MAX"]));
                                        row2["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MAX"]) * D.GetDecimal(row2["RT_PO"]));
                                    }
                                    else
                                    {
                                        row2["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MIN"]) * D.GetDecimal(this.cur환정보.Text));
                                        row2["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MIN"]));
                                        row2["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MIN"]) * D.GetDecimal(row2["RT_PO"]));
                                    }
                                }
                                if (D.GetDecimal(row2["UM_EX"]) != 0M)
                                    break;
                            }
                            else if (D.GetString(row3["CODE"]) == "POL")
                            {
                                DataRow[] dataRowArray = dataSet.Tables[3].Select("CD_ITEM ='" + D.GetString(row2["CD_ITEM"]) + "'");
                                if (dataRowArray.Length > 0)
                                {
                                    if (D.GetString(row3["MAXMIN"]) == "MAX")
                                    {
                                        row2["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MAX"]) * D.GetDecimal(this.cur환정보.Text));
                                        row2["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MAX"]));
                                        row2["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MAX"]) * D.GetDecimal(row2["RT_PO"]));
                                    }
                                    else
                                    {
                                        row2["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MIN"]) * D.GetDecimal(this.cur환정보.Text));
                                        row2["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MIN"]));
                                        row2["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["UM_ITEM_MIN"]) * D.GetDecimal(row2["RT_PO"]));
                                    }
                                }
                                if (D.GetDecimal(row2["UM_EX"]) != 0M)
                                    break;
                            }
                        }
                        this.금액계산(row1, D.GetDecimal(row2["UM_EX_PO"]), D.GetDecimal(row2["QT_PO_MM"]), "UM_EX_PO", D.GetDecimal(row2["UM_EX_PO"]));
                        ++row1;
                    }
                    else
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn_wbscbs_Click(object sender, EventArgs e)
        {
            if (!this.HeaderCheck(0))
                return;
            DataTable dt = P_OPEN_SUBWINDOWS.P_PJT_WBS_CBS_SUB_LOAD(new object[] { "",
                                                                                   "PU_POL" });
            if (dt == null)
                return;
            bool flag = dt.Columns.Contains("NM_GRP_MFG");
            string empty = string.Empty;
            string str = D.GetString(dt.Rows[0]["CD_EXCH"]);
            decimal num = D.GetDecimal(dt.Rows[0]["RT_EXCH"]);
            if (Global.MainFrame.ServerKeyCommon == "UNIPOINT")
            {
                if (!this.Tppo_Accept(dt, "전용"))
                    return;
            }
            try
            {
                this.ControlButtonEnabledDisable((Control)sender, true);
                this.cbo공장.Enabled = false;
                decimal maxValue = this._flexD.GetMaxValue("NO_LINE");
                foreach (DataRow row1 in dt.Rows)
                {
                    ++maxValue;
                    this._flexD.Rows.Add();
                    this._flexD.Row = this._flexD.Rows.Count - 1;
                    this._flexD["CD_PJT"] = row1["NO_PROJECT"];
                    this._flexD["NM_PJT"] = row1["NM_PROJECT"];
                    this._flexD["SEQ_PROJECT"] = row1["SEQ_PROJECT"];
                    this._flexD["CD_ITEM"] = row1["CD_MATL"];
                    this._flexD["NM_ITEM"] = row1["NM_MATL"];
                    this._flexD["STND_ITEM"] = row1["STND_ITEM"];
                    this._flexD["STND_DETAIL_ITEM"] = row1["STND_DETAIL_ITEM"];
                    this._flexD["UNIT_IM"] = row1["UNIT_IM"];
                    if (flag)
                        this._flexD["NM_GRPMFG"] = row1["NM_GRP_MFG"];
                    this._flexD["CD_PJT_ITEM"] = row1["CD_ITEM"];
                    this._flexD["NM_PJT_ITEM"] = row1["NM_ITEM"];
                    this._flexD["PJT_ITEM_STND"] = row1["STND_ITEM_ITEM"];
                    this._flexD["NO_WBS"] = row1["NO_WBS"];
                    this._flexD["NO_CBS"] = row1["NO_CBS"];
                    this._flexD["CD_ACTIVITY"] = row1["CD_ACTIVITY"];
                    this._flexD["NM_ACTIVITY"] = row1["NM_ACTIVITY"];
                    this._flexD["CD_COST"] = row1["CD_COST"];
                    this._flexD["NM_COST"] = row1["NM_COST"];
                    this._flexD["DT_LIMIT"] = this.dtp납기일.Text;
                    this._flexD["UNIT_PO"] = row1["UNIT_PO"];
                    this._flexD["CD_UNIT_MM"] = row1["UNIT_PO"];
                    this._flexD["STND_MA_ITEM"] = row1["STND_ITEM_ITEM"];
                    this._flexD["DT_PLAN"] = this._flexD["DT_LIMIT"];
                    this._flexD["FG_POST"] = "O";
                    this._flexD["NM_SYSDEF"] = this._biz.GetGubunCodeSearch("PU_C000009", this._flexD["FG_POST"].ToString());
                    this._flexD["CD_PJT"] = row1["NO_PROJECT"];
                    this._flexD["NM_PJT"] = row1["NM_PROJECT"];
                    this._flexD["SEQ_PROJECT"] = row1["SEQ_PROJECT"];
                    this._flexD["RT_PO"] = row1["UNIT_PO_FACT_ITEM"];
                    this._flexD["CD_PLANT"] = D.GetString(this.cbo공장.SelectedValue);
                    if (this.txt발주번호.Text != string.Empty)
                        this._flexD["NO_PO"] = this.txt발주번호.Text;
                    this._flexD["NO_LINE"] = maxValue;
                    this._flexD["CD_EXCH"] = D.GetString(this.cbo환정보.SelectedValue);
                    if (D.GetString(row1["CD_CC"]) == string.Empty)
                    {
                        this.SetCC(this._flexD.Row, string.Empty);
                    }
                    else
                    {
                        this._flexD["CD_CC"] = row1["CD_CC"];
                        this._flexD["NM_CC"] = row1["NM_CC"];
                    }
                    this._flexD["DC1"] = D.GetString(row1["DC_REMARK"]);
                    this._flexD["NO_LINE_PJTBOM"] = row1["NO_LINE_PJTBOM"];
                    foreach (DataRow row2 in ((DataTable)this.cbo환정보.DataSource).Rows)
                    {
                        if (row2["CODE"].ToString() == D.GetString(this.cbo환정보.SelectedValue))
                        {
                            this._flexD["NM_EXCH"] = row2["NAME"];
                            break;
                        }
                    }
                    if (this._flexD.CDecimal(this._flexD["RT_PO"]) == 0M)
                        this._flexD["RT_PO"] = 1;
                    this._flexD["QT_PO_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row1["QT_NEED_JAN"]));
                    this._flexD["QT_PO"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(this._flexD["QT_PO_MM"]) * D.GetDecimal(this._flexD["RT_PO"]));
                    this._flexD["NO_PR"] = "";
                    this._flexD["NO_PRLINE"] = 0;
                    this._flexD["FG_TAX"] = this._header.CurrentRow["FG_TAX"];
                    this._flexD["RATE_VAT"] = this.cur부가세율.DecimalValue;
                    this.ctx공급자.CodeValue = D.GetString(row1["CD_PARTNER"]);
                    this.ctx공급자.CodeName = D.GetString(row1["NM_PARTNER"]);
                    if (App.SystemEnv.PMS사용)
                    {
                        this._flexD["CD_CSTR"] = row1["CD_CSTR"];
                        this._flexD["DL_CSTR"] = row1["DL_CSTR"];
                        this._flexD["NM_CSTR"] = row1["NM_CSTR"];
                        this._flexD["SIZE_CSTR"] = row1["SIZE_CSTR"];
                        this._flexD["UNIT_CSTR"] = row1["UNIT_CSTR"];
                        this._flexD["QTY_ACT"] = row1["QTY_ACT"];
                        this._flexD["UNT_ACT"] = row1["UNT_ACT"];
                        this._flexD["AMT_ACT"] = row1["AMT_ACT"];
                    }
                    if (Global.MainFrame.ServerKeyCommon.ToUpper() == "ICDERPU")
                        this._flexD["CD_USERDEF1"] = row1["NO_ADN_SEQ"];
                    string maexcMenu = BASIC.GetMAEXC_Menu(nameof(P_CZ_PU_PO_WINTEC), "PU_A00000020");
                    this.FillPol(this._flexD.Row);
                    if (Global.MainFrame.ServerKeyCommon.ToUpper() != "KORAVL" && Global.MainFrame.ServerKeyCommon.ToUpper() != "UCLICK" && maexcMenu == "000")
                    {
                        this.품목정보구하기(new object[] { this._flexD["CD_ITEM"].ToString(),
                                                           this._flexD["CD_PLANT"].ToString(),
                                                           this.LoginInfo.CompanyCode,
                                                           this.cbo단가유형.SelectedValue.ToString(),
                                                           this.cbo환정보.SelectedValue.ToString(),
                                                           this.dtp발주일자.Text,
                                                           this.ctx거래처.CodeValue,
                                                           this.ctx구매그룹.CodeValue,
                                                           "N",
                                                           D.GetString(this._flexD["CD_PJT"]),
                                                           Global.MainFrame.ServerKeyCommon.ToUpper() }, "요청", 0);
                    }
                    else
                    {
                        this._flexD["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row1["UM_EX"]) * D.GetDecimal(this._flexD["RT_PO"]));
                        this._flexD["CD_EXCH"] = D.GetString(str);
                        this._flexD["QT_PO_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row1["QT_NEED_JAN"]));
                        this._flexD["QT_PO"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(this._flexD["QT_PO_MM"]) * D.GetDecimal(this._flexD["RT_PO"]));
                        if (D.GetString(str) != "" && D.GetDecimal(num) != 0M)
                        {
                            this.cbo환정보.SelectedValue = D.GetString(str);
                            this.cur환정보.Text = D.GetString(num);
                            this._header.CurrentRow["CD_EXCH"] = D.GetString(str);
                            this._header.CurrentRow["RT_EXCH"] = D.GetDecimal(num);
                        }
                    }
                    this.부가세계산(this._flexD.GetDataRow(this._flexD.Row));
                    this._flexD.AddFinished();
                    this._flexD.Col = this._flexD.Cols.Fixed;
                }
                this.SUMFunction();
                this.SetHeadControlEnabled(false, 4);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btnBOM_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.btn품목추가.Enabled)
                    return;
                if (Config.MA_ENV.PJT형여부 == "Y")
                {
                    if (this._m_pjtbom_rq_mng == "100")
                    {
                        P_PM_PROJECT_BOM_SUB2 pmProjectBomSuB2 = new P_PM_PROJECT_BOM_SUB2(D.GetString(this.cbo공장.SelectedValue), D.GetString(this.cbo공장.SelectedText));
                        if (MA.ServerKey(false, new string[] { "CVNET" }))
                        {
                            if (!this.HeaderCheck(1))
                                return;
                            pmProjectBomSuB2._yn_partner = "Y";
                        }
                        else if (!this.HeaderCheck(0))
                            return;
                        if (((Form)pmProjectBomSuB2).ShowDialog() == DialogResult.OK)
                        {
                            DataTable returnDt = pmProjectBomSuB2.returnDt;
                            if (returnDt == null && returnDt.Rows.Count < 1)
                                return;
                            if (MA.ServerKey(false, new string[] { "CVNET" }) && !this.Partner_Accept(returnDt))
                                return;
                            this._flexD.Redraw = false;
                            this.SettingDiviedData(returnDt);
                            this.SetHeadControlEnabled(false, 1);
                        }
                    }
                    else
                    {
                        P_PU_PJT_BOM_SUB pPuPjtBomSub = new P_PU_PJT_BOM_SUB(new object[] { D.GetString(this.cbo공장.SelectedValue) });
                        if (((Form)pPuPjtBomSub).ShowDialog() == DialogResult.OK)
                        {
                            DataTable dtReutrn = pPuPjtBomSub.dt_reutrn;
                            if (dtReutrn == null && dtReutrn.Rows.Count < 1)
                                return;
                            this._flexD.Redraw = false;
                            this.SettingDiviedData(dtReutrn);
                            this.SetHeadControlEnabled(false, 1);
                        }
                    }
                }
                else
                {
                    if (!this.HeaderCheck(0))
                        return;
                    P_PU_GIREQ_BOM_SUB pPuGireqBomSub = new P_PU_GIREQ_BOM_SUB(D.GetString(this.cbo공장.SelectedValue), this.dtp발주일자.Text);
                    if (((Form)pPuGireqBomSub).ShowDialog(this) == DialogResult.OK)
                    {
                        DataTable dtReturn = pPuGireqBomSub.dt_return;
                        if (dtReturn == null && dtReturn.Rows.Count < 1)
                            return;
                        DataTable table;
                        if (Global.MainFrame.ServerKeyCommon.Contains("CHCN"))
                            table = dtReturn.DefaultView.ToTable(true, "CD_MATL", "NM_ITEM_MATL", "STND_ITEM_MATL", "STND_DETAIL_ITEM_MATL", "UNIT_IM_MATL", "UNIT_PO", "UNIT_PO_FACT", "NM_GRPMFG", "QT_ITEM");
                        else
                            table = dtReturn.DefaultView.ToTable(true, "CD_MATL", "NM_ITEM_MATL", "STND_ITEM_MATL", "STND_DETAIL_ITEM_MATL", "UNIT_IM_MATL", "UNIT_PO", "UNIT_PO_FACT", "NM_GRPMFG");
                        foreach (DataRow row1 in table.Rows)
                        {
                            this.추가_Click(null, null);
                            this._flexD["CD_ITEM"] = row1["CD_MATL"];
                            this._flexD["NM_ITEM"] = row1["NM_ITEM_MATL"];
                            this._flexD["STND_ITEM"] = row1["STND_ITEM_MATL"];
                            this._flexD["STND_DETAIL_ITEM"] = row1["STND_DETAIL_ITEM_MATL"];
                            this._flexD["UNIT_IM"] = row1["UNIT_IM_MATL"];
                            this._flexD["UNIT_PO"] = row1["UNIT_PO"];
                            this._flexD["CD_UNIT_MM"] = row1["UNIT_PO"];
                            this._flexD["FG_POST"] = "O";
                            this._flexD["NM_SYSDEF"] = this._biz.GetGubunCodeSearch("PU_C000009", this._flexD["FG_POST"].ToString());
                            this._flexD["RT_PO"] = row1["UNIT_PO_FACT"];
                            this._flexD["CD_PLANT"] = D.GetString(this.cbo공장.SelectedValue);
                            this._flexD["CD_EXCH"] = D.GetString(this.cbo환정보.SelectedValue);
                            this.SetCC(this._flexD.Row, string.Empty);
                            foreach (DataRow row2 in ((DataTable)this.cbo환정보.DataSource).Rows)
                            {
                                if (row2["CODE"].ToString() == D.GetString(this.cbo환정보.SelectedValue))
                                {
                                    this._flexD["NM_EXCH"] = row2["NAME"];
                                    break;
                                }
                            }
                            if (this._flexD.CDecimal(this._flexD["RT_PO"]) == 0M)
                                this._flexD["RT_PO"] = 1;
                            if (Global.MainFrame.ServerKeyCommon.Contains("CHCN"))
                            {
                                this._flexD["QT_PO"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(dtReturn.Compute("SUM(QT_ITEM_NET)", "CD_MATL ='" + D.GetString(row1["CD_MATL"]) + "' AND QT_ITEM = " + D.GetDecimal(row1["QT_ITEM"]))));
                                this._flexD["NUM_USERDEF1"] = D.GetDecimal(row1["QT_ITEM"]);
                            }
                            else
                                this._flexD["QT_PO"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(dtReturn.Compute("SUM(QT_ITEM_NET)", "CD_MATL ='" + D.GetString(row1["CD_MATL"]) + "'")));
                            this._flexD["QT_PO_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(this._flexD["QT_PO"]) / D.GetDecimal(this._flexD["RT_PO"]));
                            this.FillPol(this._flexD.Row);
                            this.품목정보구하기(new object[] { this._flexD["CD_ITEM"].ToString(),
                                                               this._flexD["CD_PLANT"].ToString(),
                                                               this.LoginInfo.CompanyCode,
                                                               this.cbo단가유형.SelectedValue.ToString(),
                                                               this.cbo환정보.SelectedValue.ToString(),
                                                               this.dtp발주일자.Text,
                                                               this.ctx거래처.CodeValue,
                                                               this.ctx구매그룹.CodeValue,
                                                               "N",
                                                               D.GetString(this._flexD["CD_PJT"]),
                                                               Global.MainFrame.ServerKeyCommon.ToUpper() }, "BOM", 0);
                            this.부가세계산(this._flexD.GetDataRow(this._flexD.Row));
                            this._flexD["DT_PLAN"] = this._flexD["DT_LIMIT"];
                            this._flexD["NM_GRPMFG"] = row1["NM_GRPMFG"];
                        }
                        if (Global.MainFrame.ServerKeyCommon.Contains("WINFOOD"))
                            this.SetWinFood("", 0);
                        this.SetHeadControlEnabled(false, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this._flexD.Redraw = true;
            }
        }

        private void btn_so_gir_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.HeaderCheck(0))
                    return;
                this.호출여부 = true;
                P_PU_PO_SO_GIR_SUB pPuPoSoGirSub = new P_PU_PO_SO_GIR_SUB(new object[] { this.cbo공장.SelectedValue,
                                                                                         this.cbo공장.Text,
                                                                                         Global.MainFrame.CurrentPageID,
                                                                                         this._flexD.DataTable,
                                                                                         this._header.CurrentRow["FG_TRANS"],
                                                                                         this._header.CurrentRow["NM_TRANS"] });
                Cursor.Current = Cursors.Default;
                if (((Form)pPuPoSoGirSub).ShowDialog(this) == DialogResult.OK)
                {
                    DataTable 수주데이터 = pPuPoSoGirSub.수주데이터;
                    if (수주데이터 == null || 수주데이터.Rows.Count <= 0)
                        return;
                    this.ControlButtonEnabledDisable((Control)sender, true);
                    this.cbo공장.Enabled = false;
                    this._flexD.Redraw = false;
                    decimal maxValue = this._flexD.GetMaxValue("NO_LINE");
                    for (int index = 0; index < 수주데이터.Rows.Count; ++index)
                    {
                        if (수주데이터.Rows[index].RowState != DataRowState.Deleted)
                        {
                            ++maxValue;
                            this._flexD.Rows.Add();
                            this._flexD.Row = this._flexD.Rows.Count - 1;
                            this._flexD["CD_ITEM"] = 수주데이터.Rows[index]["CD_ITEM"];
                            this._flexD["NM_ITEM"] = 수주데이터.Rows[index]["NM_ITEM"];
                            this._flexD["STND_ITEM"] = 수주데이터.Rows[index]["STND_ITEM"];
                            this._flexD["CD_UNIT_MM"] = 수주데이터.Rows[index]["UNIT_PO"];
                            this._flexD["UNIT_PO"] = 수주데이터.Rows[index]["UNIT_PO"];
                            this._flexD["STND_MA_ITEM"] = 수주데이터.Rows[index]["STND_ITEM"];
                            this._flexD["UNIT_IM"] = 수주데이터.Rows[index]["UNIT_IM"];
                            this._flexD["DT_LIMIT"] = !(this.dtp납기일.Text == string.Empty) ? this.dtp납기일.Text : 수주데이터.Rows[index]["DT_DUEDATE"];
                            this._flexD["DT_PLAN"] = this._flexD["DT_LIMIT"];
                            this._flexD["FG_POST"] = "O";
                            this._flexD["NM_SYSDEF"] = this._biz.GetGubunCodeSearch("PU_C000009", this._flexD["FG_POST"].ToString());
                            this._flexD["RT_PO"] = D.GetDecimal(수주데이터.Rows[index]["RT_PO"]);
                            this._flexD["CD_PJT"] = 수주데이터.Rows[index]["CD_PJT"];
                            this._flexD["NM_PJT"] = 수주데이터.Rows[index]["NM_PJT"];
                            this._flexD["NO_PR"] = "";
                            this._flexD["NO_PRLINE"] = 0;
                            if (pPuPoSoGirSub.tab_no == "0")
                            {
                                this._flexD["NO_SO"] = 수주데이터.Rows[index]["NO_SO"];
                                this._flexD["NO_SOLINE"] = 수주데이터.Rows[index]["SEQ_SO"];
                            }
                            this._flexD["CD_PLANT"] = 수주데이터.Rows[index]["CD_PLANT"];
                            if (this.txt발주번호.Text != string.Empty)
                                this._flexD["NO_PO"] = this.txt발주번호.Text;
                            this._flexD["NO_RCV"] = "";
                            this._flexD["NO_LINE"] = maxValue;
                            this._flexD["CD_SL"] = 수주데이터.Rows[index]["CD_SL"];
                            this._flexD["NM_SL"] = 수주데이터.Rows[index]["NM_SL"];
                            this._flexD["CD_EXCH"] = this.cbo환정보.SelectedValue.ToString();
                            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "WINPLUS")
                            {
                                this._flexD["DC1"] = 수주데이터.Rows[index]["DC1"];
                                this._flexD["DC2"] = 수주데이터.Rows[index]["DC2"];
                            }
                            else
                            {
                                this._flexD["DC1"] = 수주데이터.Rows[index]["CD_ITEM_PARTNER"];
                                this._flexD["DC2"] = 수주데이터.Rows[index]["NM_ITEM_PARTNER"];
                            }
                            this._flexD["NM_GRPMFG"] = 수주데이터.Rows[index]["NM_GRPMFG"];
                            this._flexD["NUM_USERDEF1"] = 수주데이터.Rows[index]["NUM_USERDEF1"];
                            this._flexD["NUM_USERDEF2"] = 수주데이터.Rows[index]["NUM_USERDEF2"];
                            this.SetCC(this._flexD.Row, string.Empty);
                            foreach (DataRow row in ((DataTable)this.cbo환정보.DataSource).Rows)
                            {
                                if (row["CODE"].ToString() == this.cbo환정보.SelectedValue.ToString())
                                {
                                    this._flexD["NM_EXCH"] = row["NAME"];
                                    break;
                                }
                            }
                            if (this._flexD.CDecimal(this._flexD["RT_PO"]) == 0M)
                                this._flexD["RT_PO"] = 1;
                            this._flexD["QT_PO_MM"] = D.GetDecimal(수주데이터.Rows[index]["QT_REM"]);
                            this._flexD["QT_PO"] = (D.GetDecimal(this._flexD["QT_PO_MM"]) * D.GetDecimal(this._flexD["RT_PO"]));
                            if (MA.ServerKey(false, new string[] { "TSUBAKI" }))
                            {
                                this._flexD["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(수주데이터.Rows[index]["UM_EX_PO"]));
                                this._flexD["AM_EX"] = !(D.GetString(수주데이터.Rows[index]["QT_SO"]) == D.GetString(수주데이터.Rows[index]["QT_REM"])) ? Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flexD["QT_PO_MM"]) * D.GetDecimal(this._flexD["UM_EX_PO"])) : Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(수주데이터.Rows[index]["AM_EX_PO"]));
                            }
                            this.FillPol(this._flexD.Row);
                            object[] m_obj = new object[] { this._flexD["CD_ITEM"].ToString(),
                                                            this._flexD["CD_PLANT"].ToString(),
                                                            this.LoginInfo.CompanyCode,
                                                            this.cbo단가유형.SelectedValue.ToString(),
                                                            this.cbo환정보.SelectedValue.ToString(),
                                                            this.dtp발주일자.Text,
                                                            this.ctx거래처.CodeValue,
                                                            this.ctx구매그룹.CodeValue,
                                                            "N",
                                                            D.GetString(this._flexD["CD_PJT"]),
                                                            Global.MainFrame.ServerKeyCommon.ToUpper() };
                            if (MA.ServerKey(false, new string[] { "TSUBAKI" }))
                            {
                                this.품목정보구하기(m_obj, "SO", 0);
                                this.부가세계산_쯔바키(this._flexD.GetDataRow(this._flexD.Row));
                                this._flexD["GI_PARTNER"] = 수주데이터.Rows[index]["GI_PARTNER"];
                                this._flexD["LN_PARTNER"] = 수주데이터.Rows[index]["GI_LN_PARTNER"];
                                this._flexD["CD_PARTNER_SO"] = 수주데이터.Rows[index]["CD_PARTNER"];
                                this._flexD["LN_PARTNER_SO"] = 수주데이터.Rows[index]["LN_PARTNER"];
                                this._flexD["TXT_USERDEF1"] = 수주데이터.Rows[index]["TXT_USERDEF4_SO"];
                            }
                            else
                            {
                                this.품목정보구하기(m_obj, "요청", 0);
                                this.부가세계산(this._flexD.GetDataRow(this._flexD.Row));
                            }
                            if (pPuPoSoGirSub.헤더비고체크)
                            {
                                this.txt비고.Text = D.GetString(수주데이터.Rows[index]["DC_RMK"]);
                                this.txt발주텍스트비고1.Text = D.GetString(수주데이터.Rows[index]["DC_RMK_TEXT"]);
                                this._header.CurrentRow["DC50_PO"] = D.GetString(수주데이터.Rows[index]["DC_RMK"]);
                                this._header.CurrentRow["DC_RMK_TEXT"] = D.GetString(수주데이터.Rows[index]["DC_RMK_TEXT"]);
                            }
                            int num;
                            if (pPuPoSoGirSub.라인비고체크)
                                num = !MA.ServerKey(false, new string[] { "TSUBAKI" }) ? 1 : 0;
                            else
                                num = 1;
                            if (num == 0)
                            {
                                this._flexD["NM_USERDEF3_PO"] = 수주데이터.Rows[index]["TXT_USERDEF1_SO"].ToString().Length <= 100 ? 수주데이터.Rows[index]["TXT_USERDEF1_SO"] : 수주데이터.Rows[index]["TXT_USERDEF1_SO"].ToString().Substring(0, 100);
                                this._flexD["NM_USERDEF4_PO"] = 수주데이터.Rows[index]["TXT_USERDEF2_SO"].ToString().Length <= 80 ? 수주데이터.Rows[index]["TXT_USERDEF2_SO"] : 수주데이터.Rows[index]["TXT_USERDEF2_SO"].ToString().Substring(0, 80);
                            }
                            this._flexD.AddFinished();
                            this._flexD.Col = this._flexD.Cols.Fixed;
                        }
                    }
                    this.SUMFunction();
                    this.SetHeadControlEnabled(false, 1);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this._flexD.Redraw = true;
            }
        }

        private void btn_subinfo_Click(object sender, EventArgs e)
        {
            try
            {
                this._infosub_dlg = new P_PU_OPTION_INFO_SUB(D.GetString(this._header.CurrentRow["NO_PO"]), D.GetString(테이블구분.PU_POH.GetHashCode()), false);
                if (((Form)this._infosub_dlg).ShowDialog() != DialogResult.OK)
                    return;
                this.ToolBarSaveButtonEnabled = true;
                this._header.CurrentRow["NM_PACKING"] = this.txt포장형태.Text;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn_FILE_UPLOAD_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(this.txt발주번호.Text != ""))
                    return;
                string str = D.GetString(this._header.CurrentRow["NO_PO"]) + "_" + Global.MainFrame.LoginInfo.CompanyCode;
                ((Form)new AttachmentManager("PU", Global.MainFrame.CurrentPageID, str)).ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn_SL_apply_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ctx창고.CodeValue == string.Empty)
                    return;
                foreach (DataRow row in this._flexD.DataTable.Select())
                {
                    row["CD_SL"] = this.ctx창고.CodeValue;
                    row["NM_SL"] = this.ctx창고.CodeName;
                    DataTable dataTable = this._biz.item_pinvn(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                              this.dtp발주일자.Text.Substring(0, 4),
                                                                              D.GetString(this.cbo공장.SelectedValue),
                                                                              D.GetString(row["CD_ITEM"]),
                                                                              D.GetString(row["CD_SL"]) });
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        row["QT_INVC"] = dataTable.Rows[0]["QT_INVC"];
                        row["QT_ATPC"] = dataTable.Rows[0]["QT_ATPC"];
                    }
                    if (this.m_sEnv_CC_Menu == "100")
                        this.SetCC_Priority(row, null, null, null, null);
                }
                this.SetQtValue(this._flexD.Row);
                this.ShowMessage(this.DD("적용작업을완료하였습니다"));
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btnSMS_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexD.HasNormalRow || ((Form)new P_MF_SMS(this._header.CurrentRow.Table, "NO_PO", this.LoginInfo.EmployeeNo)).ShowDialog() != DialogResult.OK)
                    ;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn_PMS_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.추가모드여부 || D.GetString(this._flexD["ID_MEMO"]) == string.Empty)
                    return;
                object[] objArray = new object[] { "A03",
                                                   "",
                                                   Global.MainFrame.LoginInfo.CompanyCode,
                                                   D.GetString(this._flexD["CD_PJT"]),
                                                   D.GetString(this._flexD["CD_WBS"]),
                                                   D.GetString(this._flexD["NO_SHARE"]),
                                                   D.GetString(this._flexD["NO_ISSUE"]),
                                                   "04" };
                ((Form)new P_WS_PM_S_JOBSHARE_SUB1(this, D.GetString(this._flexD["ID_MEMO"]), objArray)).ShowDialog();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn견적적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._m_partner_use == "100")
                {
                    if (!this.HeaderCheck(1))
                        return;
                }
                else if (!this.HeaderCheck(0))
                    return;
                P_PU_POQUO_SUB pPuPoquoSub = new P_PU_POQUO_SUB(this._flexD.DataTable, D.GetString(this.cbo공장.SelectedValue), Global.MainFrame.LoginInfo.PurchaseGroupCode, Global.MainFrame.LoginInfo.PurchaseGroupName);
                if (((Form)pPuPoquoSub).ShowDialog(this) == DialogResult.OK)
                {
                    DataTable gdtReturn = pPuPoquoSub.gdt_return;
                    if (gdtReturn == null || gdtReturn.Rows.Count <= 0 || this._m_partner_use == "100" && !this.Partner_Accept_quo(gdtReturn))
                        return;
                    this.ControlButtonEnabledDisable((Control)sender, true);
                    this.cbo공장.Enabled = false;
                    this._flexD.Redraw = false;
                    decimal maxValue = this._flexD.GetMaxValue("NO_LINE");
                    for (int index = 0; index < gdtReturn.Rows.Count; ++index)
                    {
                        if (gdtReturn.Rows[index].RowState != DataRowState.Deleted)
                        {
                            ++maxValue;
                            this._flexD.Rows.Add();
                            this._flexD.Row = this._flexD.Rows.Count - 1;
                            this._flexD["CD_ITEM"] = gdtReturn.Rows[index]["CD_ITEM"];
                            this._flexD["NM_ITEM"] = gdtReturn.Rows[index]["NM_ITEM"];
                            this._flexD["STND_ITEM"] = gdtReturn.Rows[index]["STND_ITEM"];
                            this._flexD["CD_UNIT_MM"] = gdtReturn.Rows[index]["UNIT_PO"];
                            this._flexD["UNIT_PO"] = gdtReturn.Rows[index]["UNIT_PO"];
                            this._flexD["STND_MA_ITEM"] = gdtReturn.Rows[index]["STND_ITEM"];
                            this._flexD["UNIT_IM"] = gdtReturn.Rows[index]["UNIT_IM"];
                            this._flexD["GRP_MFG"] = gdtReturn.Rows[index]["GRP_MFG"];
                            this._flexD["NM_GRPMFG"] = gdtReturn.Rows[index]["NM_GRPMFG"];
                            this._flexD["DT_LIMIT"] = !(this.dtp납기일.Text == string.Empty) && !(D.GetString(gdtReturn.Rows[index]["DT_LIMIT"]) != "") ? this.dtp납기일.Text : gdtReturn.Rows[index]["DT_LIMIT"];
                            this._flexD["DT_PLAN"] = gdtReturn.Rows[index]["DT_PLAN"];
                            this._flexD["FG_POST"] = "O";
                            this._flexD["NM_SYSDEF"] = this._biz.GetGubunCodeSearch("PU_C000009", this._flexD["FG_POST"].ToString());
                            this._flexD["QT_PO"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(gdtReturn.Rows[index]["QT_QUO_RE"]));
                            this._flexD["RT_PO"] = gdtReturn.Rows[index]["RT_PO"];
                            if (this._flexD.CDecimal(this._flexD["RT_PO"]) == 0M)
                                this._flexD["RT_PO"] = 1;
                            this._flexD["QT_PO_MM"] = Unit.수량(DataDictionaryTypes.PU, this._flexD.CDecimal(gdtReturn.Rows[index]["QT_QUO_RE"]) / this._flexD.CDecimal(this._flexD["RT_PO"]));
                            this._flexD["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(gdtReturn.Rows[index]["UM_EX_PO"]));
                            this._flexD["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(gdtReturn.Rows[index]["UM_EX"]));
                            this._flexD["UM_P"] = Unit.원화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(this._flexD["UM_EX_PO"]) * this.cur환정보.DecimalValue);
                            this._flexD["UM"] = Unit.원화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(gdtReturn.Rows[index]["UM_EX"]) * this.cur환정보.DecimalValue);
                            this._flexD["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, this._flexD.CDecimal(this._flexD["UM_EX"]) * this._flexD.CDecimal(this._flexD["QT_PO_MM"]));
                            this._flexD["FG_TAX"] = this._header.CurrentRow["FG_TAX"];
                            this._flexD["RATE_VAT"] = this.cur부가세율.DecimalValue;
                            this.부가세계산(this._flexD.GetDataRow(this._flexD.Row));
                            this._flexD["NO_PR"] = gdtReturn.Rows[index]["NO_PR"];
                            this._flexD["NO_PRLINE"] = gdtReturn.Rows[index]["NO_PRLINE"];
                            this._flexD["NO_QUO"] = gdtReturn.Rows[index]["NO_QUO"];
                            this._flexD["NO_QUOLINE"] = gdtReturn.Rows[index]["NO_LINE"];
                            this._flexD["CD_PLANT"] = gdtReturn.Rows[index]["CD_PLANT"];
                            if (this.txt발주번호.Text != string.Empty)
                                this._flexD["NO_PO"] = this.txt발주번호.Text;
                            this._flexD["NO_LINE"] = maxValue;
                            this._flexD["CD_SL"] = gdtReturn.Rows[index]["CD_SL"];
                            this._flexD["NM_SL"] = gdtReturn.Rows[index]["NM_SL"];
                            this._flexD["CD_EXCH"] = this.cbo환정보.SelectedValue.ToString();
                            this._flexD["DC1"] = gdtReturn.Rows[index]["DC_RMK1"];
                            this._flexD["DC2"] = gdtReturn.Rows[index]["DC_RMK2"];
                            this._flexD["CD_BUDGET"] = gdtReturn.Rows[index]["CD_BUDGET"];
                            this._flexD["NM_BUDGET"] = gdtReturn.Rows[index]["NM_BUDGET"];
                            this._flexD["CD_BGACCT"] = gdtReturn.Rows[index]["CD_BGACCT"];
                            this._flexD["NM_BGACCT"] = gdtReturn.Rows[index]["NM_BGACCT"];
                            if (this.m_sEnv_CC_Line == "Y" && gdtReturn.Rows[index]["CD_CC"] != null && gdtReturn.Rows[index]["CD_CC"].ToString().Trim() != "")
                            {
                                this._flexD["CD_CC"] = gdtReturn.Rows[index]["CD_CC"];
                                this._flexD["NM_CC"] = gdtReturn.Rows[index]["NM_CC"];
                            }
                            else
                                this.SetCC(this._flexD.Row, string.Empty);
                            foreach (DataRow row in ((DataTable)this.cbo환정보.DataSource).Rows)
                            {
                                if (row["CODE"].ToString() == this.cbo환정보.SelectedValue.ToString())
                                {
                                    this._flexD["NM_EXCH"] = row["NAME"];
                                    break;
                                }
                            }
                            this._flexD["CD_PJT"] = D.GetString(gdtReturn.Rows[index]["CD_PJT"]);
                            this._flexD["NM_PJT"] = D.GetString(gdtReturn.Rows[index]["NM_PROJECT"]);
                            this._flexD["SEQ_PROJECT"] = D.GetDecimal(gdtReturn.Rows[index]["SEQ_PROJECT"]);
                            this._flexD["CD_PJT_ITEM"] = gdtReturn.Rows[index]["CD_PJT_ITEM"];
                            this._flexD["NM_PJT_ITEM"] = gdtReturn.Rows[index]["NM_PJT_ITEM"];
                            this._flexD["PJT_ITEM_STND"] = gdtReturn.Rows[index]["PJT_ITEM_STND"];
                            this._flexD["NM_CLS_ITEM"] = D.GetString(gdtReturn.Rows[index]["NM_CLS_ITEM"]);
                            this._flexD["PI_PARTNER"] = gdtReturn.Rows[index]["PARTNER"];
                            this._flexD["PI_LN_PARTNER"] = gdtReturn.Rows[index]["PARTNER_NM"];
                            this.FillPol(this._flexD.Row);
                            if (Config.MA_ENV.PJT형여부 == "Y")
                            {
                                this._flexD["NO_WBS"] = gdtReturn.Rows[index]["NO_WBS"];
                                this._flexD["NO_CBS"] = gdtReturn.Rows[index]["NO_CBS"];
                                this._flexD["CD_ACTIVITY"] = gdtReturn.Rows[index]["CD_ACTIVITY"];
                                this._flexD["NM_ACTIVITY"] = gdtReturn.Rows[index]["NM_ACTIVITY"];
                                this._flexD["CD_COST"] = gdtReturn.Rows[index]["CD_COST"];
                                this._flexD["NM_COST"] = gdtReturn.Rows[index]["NM_COST"];
                                this._flexD["NO_LINE_PJTBOM"] = gdtReturn.Rows[index]["NO_LINE_PJTBOM"];
                            }
                            if (App.SystemEnv.PMS사용)
                            {
                                this._flexD["CD_CSTR"] = gdtReturn.Rows[index]["CD_CSTR"];
                                this._flexD["DL_CSTR"] = gdtReturn.Rows[index]["DL_CSTR"];
                                this._flexD["NM_CSTR"] = gdtReturn.Rows[index]["NM_CSTR"];
                                this._flexD["SIZE_CSTR"] = gdtReturn.Rows[index]["SIZE_CSTR"];
                                this._flexD["UNIT_CSTR"] = gdtReturn.Rows[index]["UNIT_CSTR"];
                                this._flexD["QTY_ACT"] = gdtReturn.Rows[index]["QTY_ACT"];
                                this._flexD["UNT_ACT"] = gdtReturn.Rows[index]["UNT_ACT"];
                                this._flexD["AMT_ACT"] = gdtReturn.Rows[index]["AMT_ACT"];
                            }
                            if (this.sPUSU == "100")
                                this.GET_SU_BOM();
                        }
                    }
                    this._flexD.AddFinished();
                    this._flexD.Col = this._flexD.Cols.Fixed;
                }
                this.SUMFunction();
                this.SetHeadControlEnabled(false, 5);
                this._flexD.Redraw = true;
            }
            catch (Exception ex)
            {
                this._flexD.Redraw = true;
                this.MsgEnd(ex);
            }
        }

        private void btn_일괄발행_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexD.HasNormalRow)
                    return;
                string 프로젝트 = D.GetString(this._flexD["CD_PJT"]);
                string 프로젝트항번 = D.GetString(this._flexD["SEQ_PROJECT"]);
                string str = D.GetString(this._flexD["NO_PO"]);
                decimal num1 = D.GetDecimal(this._flexD["NO_LINE"]);
                string 발주형태 = D.GetString(this.ctx발주형태.CodeValue);
                bool 부가세포함 = D.GetString(this._flexD["TP_UM_TAX"]) == "001";
                string filter = "NO_PO = '" + str + "' AND NO_POLINE = " + num1;
                decimal 부가세율 = !(this._header.CurrentRow["FG_TRANS"].ToString() != "001") && !(this._flexD["FG_TAX"].ToString() == string.Empty) ? (this._flexD.CDecimal(this._flexD["RATE_VAT"]) == 0M ? 0M : this._flexD.CDecimal(this._flexD["RATE_VAT"]) / 100M) : this.cur부가세율.DecimalValue / 100M;
                decimal num2 = D.GetDecimal(this._flexD["AM"]);
                decimal num3 = 1M;
                if (프로젝트 == "")
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { "프로젝트" });
                }
                else if (num2 == 0M)
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { "금액" });
                }
                else
                {
                    string 매입예정일 = D.GetString(D.GetInt(this._flexIV.DataTable.Compute("MAX(DT_PUR_PLAN)", filter)));
                    P_PU_Z_INITECH_PO_PBL_REG_SUB initechPoPblRegSub = new P_PU_Z_INITECH_PO_PBL_REG_SUB(프로젝트, 프로젝트항번, 매입예정일, 발주형태, 부가세포함, 부가세율);
                    if (((Form)initechPoPblRegSub).ShowDialog() == DialogResult.OK)
                    {
                        DataTable getRows = initechPoPblRegSub.getRows;
                        if (D.GetDecimal(this._flexD["AM"]) < D.GetDecimal(getRows.Compute("SUM(AM)", "")))
                        {
                            this.ShowMessage("매입 합계금액이 한도를 초과하였습니다.");
                        }
                        else
                        {
                            if (this.cur환정보.DecimalValue != 0M)
                                num3 = this.cur환정보.DecimalValue;
                            foreach (DataRow row in getRows.Rows)
                            {
                                decimal num7 = this._flexIV.GetMaxValue("NO_SEQ") + 1;
                                this._flexIV.Rows.Add();
                                this._flexIV.Row = this._flexIV.Rows.Count - 1;
                                this._flexIV["NO_PO"] = this._flexD["NO_PO"];
                                this._flexIV["NO_POLINE"] = this._flexD["NO_LINE"];
                                this._flexIV["NO_SEQ"] = num7;
                                this._flexIV["CD_PJT"] = this._flexD["CD_PJT"];
                                this._flexIV["SEQ_PROJECT"] = this._flexD["SEQ_PROJECT"];
                                this._flexIV["FG_IV"] = row["FG_IV"];
                                this._flexIV["DT_PUR_PLAN"] = row["DT_PUR_PLAN"];
                                this._flexIV["RT_IV"] = Math.Floor(D.GetDecimal(row["AM"]) * 100M / num2);
                                this._flexIV["AM_K"] = row["AM"];
                                this._flexIV["AM"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["AM"]) / num3);
                                this._flexIV["AM_VAT"] = row["AM_VAT"];
                                this._flexIV["AM_SUM"] = row["AM_SUM"];
                                this._flexIV["NM_TEXT"] = row["DC_RMK"];
                            }
                            this._flexIV.AddFinished();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn환정보적용_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow row in this._flexD.DataTable.Select())
                    this.부가세계산(row);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn_OneShot_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexD.HasNormalRow)
                    return;
                this.Tp_print = "ONESHOT";
                this.SetPrint(false);
            }
            catch (Exception ex)
            {
                MsgControl.CloseMsg();
                this.MsgEnd(ex);
            }
        }

        private void btn발주라인비고1적용_Click(object sender, EventArgs e)
        {
            if (!this._flexD.HasNormalRow || this._flexD.DataTable.Select("TRIM(FG_POST) <> 'O'", "", DataViewRowState.CurrentRows).Length > 0)
                return;
            if (this.txt발주번호.Text == "")
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl발주번호.Text });
                this.txt발주번호.Focus();
            }
            else if (this._flexD.DataTable.Select("ISNULL(DC2, '') = ''", "", DataViewRowState.CurrentRows).Length > 0)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD("발주라인비고2") });
            }
            else
            {
                string 프로젝트코드 = string.Empty;
                Hashtable hashtable = new Hashtable();
                try
                {
                    MsgControl.ShowMsg("발주라인비고1 생성중입니다.\n잠시만 기다려 주세요.");
                    for (int index = this._flexD.Rows.Fixed; index < this._flexD.Rows.Count; ++index)
                    {
                        if (this._flexD.RowState(index) != DataRowState.Deleted && !hashtable.Contains(D.GetString(this._flexD[index, "CD_PJT"])) && !(D.GetString(this._flexD[index, "FG_POST"]) != "O"))
                        {
                            프로젝트코드 = 프로젝트코드 + D.GetString(this._flexD[index, "CD_PJT"]) + "|";
                            hashtable.Add(D.GetString(this._flexD[index, "CD_PJT"]), null);
                        }
                    }
                    DataTable dataTable = this._biz.Get발주라인비고1생성(프로젝트코드);
                    for (int index = this._flexD.Rows.Fixed; index < this._flexD.Rows.Count; ++index)
                    {
                        if (this._flexD.RowState(index) != DataRowState.Deleted)
                        {
                            DataRow[] dataRowArray = dataTable.Select("CD_PJT = '" + D.GetString(this._flexD[index, "CD_PJT"]) + "'");
                            if (dataRowArray.Length > 0)
                                this._flexD[index, "DC1"] = (D.GetString(dataRowArray[0]["NM_KOR"]) + "-" + this.txt발주번호.Text + "-" + D.GetString(dataRowArray[0]["NM_SELCUST"]) + "-" + (D.GetString(dataRowArray[0]["PARTNER_ENDUSER"]) == "" ? "NULL" : D.GetString(dataRowArray[0]["PARTNER_ENDUSER"])) + "-" + D.GetString(this._flexD[index, "DC2"]));
                        }
                    }
                    MsgControl.CloseMsg();
                    this.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { this.btn발주라인비고1적용.Text });
                }
                catch (Exception ex)
                {
                    MsgControl.CloseMsg();
                    this.MsgEnd(ex);
                }
            }
        }

        private void Grid_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                this.GetQtValue(this._flexD.Row);
                DataTable dataTable = new DataTable();
                string str = "NO_POLINE = " + this._flexD["NO_LINE"].ToString();
                if (this.sPUSU == "100")
                {
                    if (this._flexD.DetailQueryNeed)
                        dataTable = this._biz.SearchDetail(this._header.CurrentRow["CD_PLANT"].ToString(), this._flexD["NO_PO"].ToString(), Convert.ToDecimal(this._flexD["NO_LINE"]));
                    this._flexDD.BindingAdd(dataTable, str);
                }
                if (!(this.sPUIV == "200"))
                    return;
                this._flexIV.RowFilter = str;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Grid_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (this._flexD["FG_POST"].ToString().Trim() != "O" && this._flexD.RowState() != DataRowState.Added)
                    e.Cancel = true;
                if (!this.차수여부 || !this.전자결재여부)
                    e.Cancel = true;
                FlexGrid flexGrid = sender as FlexGrid;
                if ((flexGrid).Name == this._flexD.Name)
                {
                    if (this.m_sEnv.Equals("N") && this._flexD.Cols[e.Col].Name == "QT_PO")
                        e.Cancel = true;
                    if (D.GetString(this._flexD["TP_UM_TAX"]) == "001")
                    {
                        if (this._flexD.Cols[e.Col].Name == "AM" || this._flexD.Cols[e.Col].Name == "AM_EX" || this._flexD.Cols[e.Col].Name == "VAT")
                            e.Cancel = true;
                    }
                    else if (this._flexD.Cols[e.Col].Name == "AM_TOTAL")
                        e.Cancel = true;
                    if ((this._flexD.Cols[e.Col].Name == "UM_EX_PO" || this._flexD.Cols[e.Col].Name == "AM_EX" || this._flexD.Cols[e.Col].Name == "AM") && this._header.CurrentRow["PO_PRICE"].ToString() == "Y")
                    {
                        this.ShowMessage("구매 단가 통제된 구매그룹 입니다.");
                        e.Cancel = true;
                    }
                    if (MA.ServerKey(false, new string[1] { "CNCC" }))
                    {
                        if (D.GetString(flexGrid["CD_ITEM"]) != string.Empty && D.GetString(flexGrid["TP_ITEM"]) == string.Empty)
                        {
                            DataTable dataTable = DBHelper.GetDataTable("SELECT TP_ITEM FROM MA_PITEM WHERE CD_COMPANY = '" + this.LoginInfo.CompanyCode + "' AND CD_PLANT = '" + D.GetString(this.cbo공장.SelectedValue) + "' AND CD_ITEM = '" + D.GetString(flexGrid["CD_ITEM"]) + "'");
                            flexGrid["TP_ITEM"] = dataTable.Rows.Count > 0 ? D.GetString(dataTable.Rows[0]["TP_ITEM"]) : string.Empty;
                        }
                        switch (this._flexD.Cols[e.Col].Name)
                        {
                            case "UM_EX_PO":
                            case "UM_EX":
                            case "AM_EX":
                            case "AM":
                            case "VAT":
                            case "AM_TOTAL":
                                if (D.GetString(flexGrid["TP_ITEM"]) == "MAS")
                                {
                                    e.Cancel = true;
                                    break;
                                }
                                break;
                        }
                    }
                    switch (this._flexD.Cols[e.Col].Name)
                    {
                        case "AM":
                            if (D.GetString(this._header.CurrentRow["CD_EXCH"]) == "000" || this._m_tppo_change == "001")
                            {
                                e.Cancel = true;
                                break;
                            }
                            break;
                        case "CD_USERDEF1":
                            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "ICDERPU")
                            {
                                e.Cancel = true;
                                break;
                            }
                            break;
                        case "CD_ITEM":
                            if (this._flexD.DataTable.Columns.Contains("APP_PJT") && D.GetString(this._flexD["APP_PJT"]) == "Y")
                                e.Cancel = true;
                            if (this._flexD["NO_APP"].ToString() != string.Empty || this._flexD["NO_PR"].ToString() != string.Empty)
                            {
                                e.Cancel = true;
                                break;
                            }
                            break;
                        case "QT_PO_MM":
                        case "UM_EX_PO":
                        case "AM_EX":
                        case "AM_TOTAL":
                        case "QT_PO":
                            if (this._m_tppo_change == "001" && D.GetString(flexGrid["NO_APP"]) != string.Empty)
                            {
                                e.Cancel = true;
                                break;
                            }
                            break;
                        case "FG_TAX":
                            if (this.m_tab_poh.TabPages.Contains(this.tabPage7))
                            {
                                e.Cancel = true;
                                break;
                            }
                            break;
                        case "TP_UM_TAX":
                            if (this.의제매입여부(D.GetString(this._flexD["FG_TAX"])) && this.s_vat_fictitious == "100")
                            {
                                e.Cancel = true;
                                break;
                            }
                            break;
                        case "CD_PJT":
                            if (Global.MainFrame.ServerKeyCommon == "DEMAC" && D.GetString(this._flexD["NO_PR"]) != "")
                            {
                                e.Cancel = true;
                                break;
                            }
                            break;
                        case "YN_BUDGET":
                        case "CD_BUDGET":
                        case "CD_BIZPLAN":
                        case "CD_BGACCT":
                        case "CD_ACCT":
                            if (this._flexD.RowState() != DataRowState.Added || D.GetString(this._flexD["YN_BUDGET_PR"]) == "Y" || D.GetString(this._flexD["YN_BUDGET_APP"]) == "Y")
                            {
                                e.Cancel = true;
                                break;
                            }
                            break;
                        case "NM_USERDEF4_PO":
                            if ((Global.MainFrame.ServerKeyCommon.Contains("JSREMK") || Global.MainFrame.ServerKeyCommon.Contains("DZSQL")) && D.GetString(this._flexD["CD_ITEM"]) == "")
                            {
                                e.Cancel = true;
                                break;
                            }
                            break;
                        case "NUM_STND_ITEM_1":
                        case "NUM_STND_ITEM_2":
                        case "NUM_STND_ITEM_3":
                        case "NUM_STND_ITEM_4":
                        case "CLS_L":
                            if (Global.MainFrame.CurrentPageID == "P_PU_Z_JONGHAP_PO_REG2" && D.GetString(this._flexD["CD_ITEM"]) != "")
                            {
                                e.Cancel = true;
                                break;
                            }
                            break;
                        case "NO_CBS":
                            if (D.GetDecimal(this._flexD["NO_LINE_PJTBOM"]) > 0M)
                            {
                                e.Cancel = true;
                                return;
                            }
                            if (Global.MainFrame.ServerKeyCommon == "MEERE" && (D.GetString(this._flexD["NO_APP"]) != string.Empty || D.GetString(this._flexD["NO_PR"]) != string.Empty))
                            {
                                e.Cancel = true;
                                return;
                            }
                            break;
                        case "CD_SL":
                            if (MA.ServerKey(false, new string[] { "HOTEL" }) && D.GetString(this._flexD["NO_PR"]) != "")
                            {
                                e.Cancel = true;
                                return;
                            }
                            break;
                    }
                }
                else if (flexGrid.Name == this._flexDD.Name && D.GetDecimal(flexGrid["QT_REQ"]) > 0M)
                {
                    e.Cancel = true;
                    return;
                }
                if (this.bStandard && D.GetDecimal(flexGrid["UM_WEIGHT"]) > 0M && (flexGrid.Cols[e.Col].Name == "UM_EX_PO" || flexGrid.Cols[e.Col].Name == "UM_EX" || flexGrid.Cols[e.Col].Name == "AM_EX" || flexGrid.Cols[e.Col].Name == "AM"))
                {
                    e.Cancel = true;
                }
                else
                {
                    if (BASIC.GetMAEXC_Menu(nameof(P_CZ_PU_PO_WINTEC), "PU_Z00000002") == "100" && (D.GetString(flexGrid["NO_PR"]) != string.Empty || D.GetString(flexGrid["NO_APP"]) != string.Empty))
                    {
                        if (flexGrid.Cols[e.Col].Name == "UM_EX_PO" || flexGrid.Cols[e.Col].Name == "AM_EX" || flexGrid.Cols[e.Col].Name == "AM" || flexGrid.Cols[e.Col].Name == "AM_TOTAL")
                        {
                            e.Cancel = true;
                            return;
                        }
                        if ((MA.ServerKey(true, new string[] { "METRO" }) || Global.MainFrame.ServerKeyCommon.Contains("KSCC")) && (flexGrid.Cols[e.Col].Name == "QT_PO_MM" || flexGrid.Cols[e.Col].Name == "QT_PO"))
                        {
                            e.Cancel = true;
                            return;
                        }
                    }
                    if (Global.MainFrame.ServerKeyCommon.Contains("DONGWOON") && D.GetString(flexGrid["NO_RELATION"]) != "")
                    {
                        if (!(flexGrid.Cols[e.Col].Name == "QT_PO_MM") && !(flexGrid.Cols[e.Col].Name == "QT_PO") && !(flexGrid.Cols[e.Col].Name == "QT_NEED_UNIT") && !(flexGrid.Cols[e.Col].Name == "QT_NEED"))
                            return;
                        e.Cancel = true;
                    }
                    else if (Global.MainFrame.CurrentPageID == "P_PU_Z_JONGHAP_PO_REG2" && D.GetString(this._flexD["CD_UNIT_MM"]) != "")
                    {
                        string str = "발주단위";
                        DataRow codeInfo = Duzon.ERPU.MF.Common.CodeSearch.GetCodeInfo(MasterSearch.MA_CODEDTL, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                                               "MA_B000004",
                                                                                                                               D.GetString(this._flexD["CD_UNIT_MM"]) });
                        if (codeInfo != null && D.GetString(codeInfo["CD_FLAG1"]) == "2")
                            str = "재고단위";
                        if (str == "발주단위")
                        {
                            if (flexGrid.Cols[e.Col].Name == "UM" || flexGrid.Cols[e.Col].Name == "UM_EX")
                                e.Cancel = true;
                        }
                        else if (flexGrid.Cols[e.Col].Name == "UM_EX_PO")
                            e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Grid_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                if (!(sender is FlexGrid flexGrid))
                    return;
                if (e.StartEditCancel)
                    e.Cancel = true;
                else if (flexGrid.Name == this._flexD.Name)
                {
                    switch (this._flexD.Cols[e.Col].Name)
                    {
                        case "CD_ITEM":
                            if (D.GetString(this._flexD["FG_POST"]) != "O" || this._m_tppo_change == "001" && D.GetString(flexGrid["NO_APP"]) != string.Empty)
                            {
                                e.Cancel = true;
                                break;
                            }
                            if (this._flexD.DataTable.Columns.Contains("APP_PJT") && D.GetString(this._flexD["APP_PJT"]) == "Y")
                            {
                                e.Cancel = true;
                                break;
                            }
                            e.Parameter.P01_CD_COMPANY = this.LoginInfo.CompanyCode;
                            e.Parameter.P09_CD_PLANT = this._flexD["CD_PLANT"].ToString();
                            break;
                        case "CD_PURGRP":
                            e.Parameter.P92_DETAIL_SEARCH_CODE = e.EditValue;
                            break;
                        case "CD_SL":
                        case "CDSL_USERDEF1":
                            e.Parameter.P01_CD_COMPANY = this.LoginInfo.CompanyCode;
                            e.Parameter.P09_CD_PLANT = this._flexD["CD_PLANT"].ToString();
                            break;
                        case "GI_PARTNER":
                            e.Parameter.P14_CD_PARTNER = this.ctx거래처.CodeValue;
                            e.Parameter.P61_CODE1 = "003";
                            break;
                        case "NM_CLS_L":
                        case "CLS_L":
                            e.Parameter.P41_CD_FIELD1 = "MA_B000030";
                            break;
                        case "NM_CLS_M":
                            e.Parameter.P41_CD_FIELD1 = "MA_B000031";
                            break;
                        case "NM_CLS_S":
                            e.Parameter.P41_CD_FIELD1 = "MA_B000032";
                            break;
                        case "CD_PJT":
                            if (Global.MainFrame.ServerKeyCommon == "DEMAC" && D.GetString(this._flexD["NO_PR"]) != "")
                            {
                                e.Cancel = true;
                                break;
                            }
                            break;
                        case "CD_BIZPLAN":
                            if (D.GetString(this._flexD["CD_BUDGET"]) == string.Empty)
                            {
                                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD("예산단위") });
                                e.Cancel = true;
                                return;
                            }
                            e.Parameter.P61_CODE1 = D.GetString(this._flexD["CD_BUDGET"]);
                            break;
                        case "CD_BGACCT":
                            if (Global.MainFrame.ServerKeyCommon.Contains("KAHP"))
                                e.Parameter.UserParams = "예산계정;H_FI_BUDGETACCTJO_SUB6;" + string.Empty + ";" + D.GetString(this._flexD["CD_BUDGET"]) + "|;" + D.GetString(this._flexD["NM_USERDEF1_PR"]) + "|;1;" + string.Empty + ";";
                            if (this._YN_CdBizplan == "1")
                            {
                                e.Parameter.UserParams = "예산계정;H_FI_BUDGETACCTJO_SUB6;" + string.Empty + ";" + D.GetString(this._flexD["CD_BUDGET"]) + "|;" + D.GetString(this._flexD["CD_BIZPLAN"]) + "|;1;" + string.Empty + ";";
                                break;
                            }
                            break;
                        case "CD_ACCT":
                            e.Parameter.P62_CODE2 = "Y";
                            if (D.GetString(this._flexD[e.Row, "CD_BGACCT"]) == string.Empty)
                            {
                                Global.MainFrame.ShowMessage("예산계정을 먼저 입력하세요.");
                                e.Cancel = true;
                                return;
                            }
                            break;
                        case "CD_PJT_ITEM":
                            if (D.GetString(this._flexD["CD_PJT"]) != string.Empty)
                            {
                                e.Parameter.P64_CODE4 = D.GetString(this._flexD["CD_PJT"]);
                                break;
                            }
                            break;
                        case "NO_CBS":
                            if (D.GetString(this._flexD["CD_PJT"]) == string.Empty)
                            {
                                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD("프로젝트")  });
                                e.Cancel = true;
                            }
                            e.Parameter.P61_CODE1 = "|";
                            e.Parameter.UserParams = D.GetString(this._flexD["CD_PJT"]) + "|";
                            break;
                        case "CD_ITEM_ORIGIN":
                            e.Parameter.P01_CD_COMPANY = this.LoginInfo.CompanyCode;
                            e.Parameter.P09_CD_PLANT = this._flexD["CD_PLANT"].ToString();
                            break;
                    }
                    if (BASIC.GetMAEXC("공장품목-대중소분류 종속관계 설정").Equals("001"))
                    {
                        switch (this._flexD.Cols[e.Col].Name)
                        {
                            case "NM_CLS_M":
                                if (D.GetString(this._flexD["CLS_L"]) == "")
                                {
                                    this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD("대분류코드") });
                                    e.Cancel = true;
                                    break;
                                }
                                e.Parameter.P42_CD_FIELD2 = D.GetString(this._flexD["CLS_L"]);
                                break;
                            case "NM_CLS_S":
                                if (D.GetString(this._flexD["CLS_M"]) == "")
                                {
                                    this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD("중분류코드") });
                                    e.Cancel = true;
                                    break;
                                }
                                e.Parameter.P42_CD_FIELD2 = D.GetString(this._flexD["CLS_M"]);
                                break;
                        }
                    }
                }
                else if ((flexGrid).Name == this._flexDD.Name)
                {
                    if (D.GetDecimal(flexGrid["QT_REQ"]) > 0M)
                        e.Cancel = true;
                    else
                        e.Parameter.P09_CD_PLANT = this.cbo공장.SelectedValue.ToString();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Grid_AfterCodeHelp(object sender, AfterCodeHelpEventArgs e)
        {
            try
            {
                FlexGrid flexGrid = sender as FlexGrid;
                HelpReturn result = e.Result;
                int num1 = 0;
                if ((flexGrid).Name == this._flexD.Name)
                {
                    switch (this._flexD.Cols[e.Col].Name)
                    {
                        case "CD_ITEM":
                            if (e.Result.DialogResult == DialogResult.Cancel)
                            {
                                this._flexD[e.Row, "CD_ITEM"] = string.Empty;
                                break;
                            }
                            if (Global.MainFrame.ServerKeyCommon == "OGELEC")
                            {
                                foreach (DataRow row in e.Result.Rows)
                                {
                                    if (D.GetString(row["TP_PROC"]) != "P")
                                    {
                                        e.Cancel = true;
                                        this._flexD[e.Row, "CD_ITEM"] = string.Empty;
                                        int num2 = (int)this.ShowMessage("조달구분이 구매품이 아닌 건이 존재합니다. 품목코드 : " + row["CD_ITEM"]);
                                        return;
                                    }
                                }
                            }
                            bool flag1 = true;
                            bool flag2 = false;
                            StringBuilder stringBuilder = new StringBuilder();
                            string str1 = "품목코드\t 품목명\t";
                            stringBuilder.AppendLine(str1);
                            string str2 = "-".PadRight(80, '-');
                            stringBuilder.AppendLine(str2);
                            this._flexD.Redraw = false;
                            this.mDataArea.Enabled = false;
                            this.flowLayoutPanel1.Enabled = false;
                            this._flexD.SetDummyColumnAll();
                            string str3 = !(this._flexD[this._flexD.Rows.Count - 1, "DT_LIMIT"].ToString() != "") ? Global.MainFrame.GetStringToday : this._flexD[this._flexD.Rows.Count - 1, "DT_LIMIT"].ToString();
                            foreach (DataRow row1 in result.Rows)
                            {
                                int row2;
                                if (flag1)
                                {
                                    if (Global.MainFrame.ServerKeyCommon.Contains("DAIKIN") && D.GetString(row1["CD_USERDEF2"]) == "Y")
                                    {
                                        this.ShowMessage("수입불가 품목입니다. 해당 품목(" + D.GetString(row1["NM_ITEM"]) + ")은 발주 할 수 없습니다.");
                                        this._flexD[e.Row, "CD_ITEM"] = "";
                                        return;
                                    }
                                    if (this.sFG_TAXcheck == "100" && D.GetString(this._header.CurrentRow["YN_IMPORT"]) != "Y" && D.GetString(row1["FG_TAX_PU"]) != D.GetString(this.cbo과세구분.SelectedValue))
                                    {
                                        this._flexD[e.Row, "CD_ITEM"] = "";
                                        string str4 = row1["CD_ITEM"].ToString().PadRight(15, ' ') + " " + row1["NM_ITEM"].ToString().PadRight(15, ' ');
                                        stringBuilder.AppendLine(str4);
                                        flag2 = true;
                                        continue;
                                    }
                                    row2 = e.Row;
                                    this._flexD[e.Row, "CD_ITEM"] = row1["CD_ITEM"];
                                    this._flexD[e.Row, "NM_ITEM"] = row1["NM_ITEM"];
                                    this._flexD[e.Row, "STND_ITEM"] = row1["STND_ITEM"];
                                    this._flexD[e.Row, "UNIT_IM"] = row1["UNIT_IM"];
                                    this._flexD[e.Row, "CD_UNIT_MM"] = row1["UNIT_PO"];
                                    this._flexD[e.Row, "NM_CLS_ITEM"] = row1["CLS_ITEMNM"];
                                    this._flexD[e.Row, "FG_SERNO"] = row1["FG_SERNO"];
                                    this._flexD[e.Row, "TP_PART"] = row1["TP_PART"];
                                    if (this.txt발주번호.Text != string.Empty)
                                        this._flexD[e.Row, "NO_PO"] = this.txt발주번호.Text;
                                    if (this._flexD[e.Row, "DT_LIMIT"].ToString() == string.Empty)
                                        this._flexD[e.Row, "DT_LIMIT"] = str3;
                                    if (MA.ServerKey(false, new string[] { "KOINO", "WINIX" }))
                                    {
                                        DataTable dtLimit = this._biz.getDT_LIMIT(new object[] { MA.Login.회사코드,
                                                                                                 D.GetString(this.cbo공장.SelectedValue),
                                                                                                 D.GetString(row1["CD_ITEM"]),
                                                                                                 this.dtp발주일자.Text });
                                        if (dtLimit != null && dtLimit.Rows.Count > 0)
                                            flexGrid[e.Row, "DT_LIMIT"] = D.GetString(dtLimit.Rows[0]["DT_LIMIT"]);
                                    }
                                    else if (Global.MainFrame.ServerKeyCommon == "AMANO")
                                    {
                                        string DT = DateTime.ParseExact(Global.MainFrame.GetStringToday, "yyyyMMdd", null).AddDays((double)D.GetDecimal(row1["LT_ITEM"])).ToString("yyyyMMdd");
                                        string calendar = this._biz.GET_CALENDAR(this.cbo공장.SelectedValue.ToString(), DT);
                                        flexGrid[e.Row, "DT_LIMIT"] = calendar;
                                    }
                                    if (this._flexD[e.Row, "DT_PLAN"].ToString() == string.Empty && !Global.MainFrame.ServerKeyCommon.Contains("SOLIDTECH"))
                                        this._flexD["DT_PLAN"] = this._flexD["DT_LIMIT"];
                                    if (Global.MainFrame.ServerKeyCommon.ToUpper() == "WJIS")
                                    {
                                        this._flexD[e.Row, "DC1"] = row1["CD_ITEM"];
                                        this._flexD[e.Row, "DC2"] = row1["NM_ITEM"];
                                    }
                                    flag1 = false;
                                }
                                else
                                {
                                    if (Global.MainFrame.ServerKeyCommon.Contains("DAIKIN") && D.GetString(row1["CD_USERDEF2"]) == "Y")
                                    {
                                        this.ShowMessage("수입불가 품목입니다. 해당 품목(" + D.GetString(row1["NM_ITEM"]) + ")은 발주 할 수 없습니다.");
                                        return;
                                    }
                                    if (this.sFG_TAXcheck == "100" && D.GetString(this._header.CurrentRow["YN_IMPORT"]) != "Y" && D.GetString(row1["FG_TAX_PU"]) != D.GetString(this.cbo과세구분.SelectedValue))
                                    {
                                        string str5 = row1["CD_ITEM"].ToString().PadRight(15, ' ') + " " + row1["NM_ITEM"].ToString().PadRight(15, ' ');
                                        stringBuilder.AppendLine(str5);
                                        flag2 = true;
                                        continue;
                                    }
                                  this._flexD.Rows.Add();
                                    this._flexD.Row = this._flexD.Rows.Count - 1;
                                    this._flexD["CD_ITEM"] = row1["CD_ITEM"];
                                    this._flexD["NM_ITEM"] = row1["NM_ITEM"];
                                    this._flexD["STND_ITEM"] = row1["STND_ITEM"];
                                    this._flexD["UNIT_IM"] = row1["UNIT_IM"];
                                    this._flexD["CD_UNIT_MM"] = row1["UNIT_PO"];
                                    this._flexD["NM_CLS_ITEM"] = row1["CLS_ITEMNM"];
                                    if (this.txt발주번호.Text != string.Empty)
                                        this._flexD["NO_PO"] = this.txt발주번호.Text;
                                    this._flexD["NO_LINE"] = this.최대차수 + 1;
                                    row2 = this._flexD.Row;
                                    this._flexD[row2, "CD_PLANT"] = this.cbo공장.SelectedValue.ToString();
                                    this._flexD[row2, "CD_PJT"] = this.ctx프로젝트.CodeValue;
                                    this._flexD[row2, "NM_PJT"] = this.ctx프로젝트.CodeName;
                                    this._flexD[row2, "DT_LIMIT"] = str3;
                                    if (MA.ServerKey(false, new string[] { "KOINO", "WINIX" }))
                                    {
                                        DataTable dtLimit = this._biz.getDT_LIMIT(new object[] { MA.Login.회사코드,
                                                                                                 D.GetString(this.cbo공장.SelectedValue),
                                                                                                 D.GetString(row1["CD_ITEM"]),
                                                                                                 this.dtp발주일자.Text });
                                        if (dtLimit != null && dtLimit.Rows.Count > 0)
                                            this._flexD[row2, "DT_LIMIT"] = D.GetString(dtLimit.Rows[0]["DT_LIMIT"]);
                                    }
                                    if (this._flexD[row2, "DT_PLAN"].ToString() == string.Empty && !Global.MainFrame.ServerKeyCommon.Contains("SOLIDTECH"))
                                        this._flexD[row2, "DT_PLAN"] = this._flexD[row2, "DT_LIMIT"];
                                    this._flexD["NM_SYSDEF"] = this._biz.GetGubunCodeSearch("PU_C000009", this._flexD["FG_POST"].ToString());
                                    if (Global.MainFrame.ServerKeyCommon.ToUpper() == "WJIS")
                                    {
                                        this._flexD[row2, "DC1"] = row1["CD_ITEM"];
                                        this._flexD[row2, "DC2"] = row1["NM_ITEM"];
                                    }
                                    this._flexD["TP_PART"] = row1["TP_PART"];
                                }
                                this._flexD[row2, "RT_PO"] = row1["UNIT_PO_FACT"];
                                this._flexD[row2, "TP_UM_TAX"] = this.cbo부가세여부.SelectedValue.ToString();
                                object[] m_obj = new object[] { row1["CD_ITEM"],
                                                                this.cbo공장.SelectedValue.ToString(),
                                                                Global.MainFrame.LoginInfo.CompanyCode,
                                                                this.cbo단가유형.SelectedValue.ToString(),
                                                                this.cbo환정보.SelectedValue.ToString(),
                                                                this.dtp발주일자.Text,
                                                                this.ctx거래처.CodeValue,
                                                                this.ctx구매그룹.CodeValue,
                                                                "N",
                                                               !(D.GetString(this._flexD[e.Row, "CD_PJT"]) != string.Empty) ?  this.ctx프로젝트.CodeValue : this._flexD[e.Row, "CD_PJT"],
                                                                Global.MainFrame.ServerKeyCommon.ToUpper() };
                                if (MA.ServerKey(false, new string[] { "TSUBAKI" }))
                                    this.품목정보구하기(m_obj, "TSUBAKI", row2);
                                else
                                    this.품목정보구하기(m_obj, "GRID", row2);
                                this._flexD[row2, "FG_TRANS"] = this._header.CurrentRow["FG_TRANS"];
                                this._flexD[row2, "FG_TPPURCHASE"] = this._header.CurrentRow["FG_TPPURCHASE"];
                                this._flexD[row2, "YN_AUTORCV"] = this._header.CurrentRow["YN_AUTORCV"];
                                this._flexD[row2, "YN_RCV"] = this._header.CurrentRow["YN_RCV"];
                                this._flexD[row2, "YN_RETURN"] = this._header.CurrentRow["YN_RETURN"];
                                this._flexD[row2, "YN_IMPORT"] = this._header.CurrentRow["YN_IMPORT"];
                                this._flexD[row2, "YN_ORDER"] = this._header.CurrentRow["YN_ORDER"];
                                this._flexD[row2, "YN_REQ"] = this._header.CurrentRow["YN_REQ"];
                                this._flexD[row2, "FG_RCV"] = this._header.CurrentRow["FG_TPRCV"];
                                this._flexD[row2, "YN_SUBCON"] = this._header.CurrentRow["YN_SUBCON"];
                                this._flexD[row2, "FG_PURCHASE"] = this._header.CurrentRow["FG_TPPURCHASE"];
                                this._flexD[row2, "NO_PR"] = "";
                                if (Global.MainFrame.ServerKeyCommon != "CUREXO")
                                {
                                    this._flexD[row2, "CD_SL"] = row1["CD_SL"];
                                    this._flexD[row2, "NM_SL"] = row1["NM_SL"];
                                }
                                this._flexD[row2, "PI_PARTNER"] = row1["PARTNER"];
                                this._flexD[row2, "PI_LN_PARTNER"] = row1["LN_PARTNER"];
                                this._flexD[row2, "CD_EXCH"] = this.cbo환정보.SelectedValue.ToString();
                                if (Global.MainFrame.ServerKeyCommon == "HOTEL" && row1["CD_USERDEF2"].ToString() != "")
                                    this._flexD[row2, "FG_PURCHASE"] = row1["CD_USERDEF2"];
                                foreach (DataRow row3 in ((DataTable)this.cbo환정보.DataSource).Rows)
                                {
                                    if (row3["CODE"].ToString() == this.cbo환정보.SelectedValue.ToString())
                                    {
                                        this._flexD[row2, "NM_EXCH"] = row3["NAME"];
                                        break;
                                    }
                                }
                                if (!MA.ServerKey(false, new string[] { "TSUBAKI" }))
                                {
                                    this.SetCC(row2, "");
                                    this._flexD.Redraw = true;
                                    this.금액계산(row2, D.GetDecimal(this._flexD[row2, "UM_EX_PO"]), D.GetDecimal(this._flexD[row2, "QT_PO_MM"]), "", 0M);
                                }
                                else if (Global.MainFrame.ServerKeyCommon == "FDWL")
                                {
                                    DataTable dataTable = this._biz.GETACCT_FDWL(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                this._header.CurrentRow["CD_PLANT"].ToString(),
                                                                                                this._flexD[row2, "CD_ITEM"].ToString(),
                                                                                                this._header.CurrentRow["FG_TPPURCHASE"].ToString(),
                                                                                                this._header.CurrentRow["CD_DEPT"].ToString() });
                                    if (dataTable != null && dataTable.Rows.Count > 0)
                                    {
                                        this._flexD[row2, "CD_BGACCT"] = dataTable.Rows[0]["CD_ACCT"];
                                        this._flexD[row2, "NM_BGACCT"] = dataTable.Rows[0]["NM_ACCT"];
                                        this._flexD[row2, "CD_BUDGET"] = dataTable.Rows[0]["CD_BGACCT"];
                                        this._flexD[row2, "NM_BUDGET"] = dataTable.Rows[0]["NM_BGACCT"];
                                    }
                                }
                                if (this.bStandard || Global.MainFrame.CurrentPageID == "P_PU_Z_JONGHAP_PO_REG2")
                                {
                                    this._flexD[row2, "CLS_L"] = row1["CLS_L"];
                                    this._flexD[row2, "CLS_M"] = row1["CLS_M"];
                                    this._flexD[row2, "CLS_S"] = row1["CLS_S"];
                                    this._flexD[row2, "NM_CLS_L"] = row1["CLS_LN"];
                                    this._flexD[row2, "NM_CLS_M"] = row1["CLS_MN"];
                                    this._flexD[row2, "NM_CLS_S"] = row1["CLS_SN"];
                                    this._flexD[row2, "NUM_STND_ITEM_1"] = row1["NUM_STND_ITEM_1"];
                                    this._flexD[row2, "NUM_STND_ITEM_2"] = row1["NUM_STND_ITEM_2"];
                                    this._flexD[row2, "NUM_STND_ITEM_3"] = row1["NUM_STND_ITEM_3"];
                                    this._flexD[row2, "NUM_STND_ITEM_4"] = row1["NUM_STND_ITEM_4"];
                                    this._flexD[row2, "NUM_STND_ITEM_5"] = row1["NUM_STND_ITEM_5"];
                                    if (this.bStandard && Global.MainFrame.ServerKey == "SINJINSM")
                                    {
                                        DataTable dataTable = this._biz.Check_ITEMGRP_SG(D.GetString(this._flexD[row2, "GRP_ITEM"]));
                                        if (dataTable.Rows.Count > 0)
                                        {
                                            this._flexD[row2, "QT_SG"] = dataTable.Rows[0]["QT_SG"];
                                            this._flexD[row2, "SG_TYPE"] = dataTable.Rows[0]["SG_TYPE"];
                                            this._flexD[row2, "UM_WEIGHT"] = dataTable.Rows[0]["UM_WEIGHT"];
                                        }
                                        this.금액계산(row2, 0M, this._flexD.CDecimal(this._flexD[e.Row, "QT_PO_MM"]), "CD_ITEM", 0M);
                                    }
                                }
                              this._flexD.Redraw = true;
                                this._flexD.AddFinished();
                                this._flexD.Col = this._flexD.Cols.Fixed;
                                this._flexD.Select(row2, this._flexD.Cols.Fixed);
                                if (this.sPUSU == "100")
                                    this.GET_SU_BOM();
                                if (Global.MainFrame.ServerKeyCommon.Contains("WINFOOD"))
                                    this.SetWinFood("CD_ITEM", row2);
                            }
                            if (flag2)
                            {
                                this.ShowDetailMessage("과세구분이 일치하지 않는 품목이 있습니다. \n  \n ▼ 버튼을 눌러서 목록을 확인하세요!", stringBuilder.ToString());
                                break;
                            }
                            break;
                        case "CD_SL":
                            DataTable dataTable1 = this._biz.item_pinvn(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                       this.dtp발주일자.Text.Substring(0, 4),
                                                                                       D.GetString(this.cbo공장.SelectedValue),
                                                                                       D.GetString(this._flexD["CD_ITEM"]),
                                                                                       D.GetString(result.Rows[0]["CD_SL"]) });
                            if (dataTable1 != null && dataTable1.Rows.Count > 0)
                            {
                                this._flexD["QT_INVC"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(dataTable1.Rows[0]["QT_INVC"]));
                                this._flexD["QT_ATPC"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(dataTable1.Rows[0]["QT_ATPC"]));
                            }
                            this.SetQtValue(this._flexD.Row);
                            if (MA.ServerKey(false, new string[] { "KMI" }))
                                this.SetQT_KMI(new DataTable(), this._flexD.Row);
                            if (this.m_sEnv_CC_Menu == "100")
                            {
                                this._flexD["CD_SL"] = D.GetString(result.Rows[0]["CD_SL"]);
                                this._flexD["NM_SL"] = D.GetString(result.Rows[0]["NM_SL"]);
                                this.SetCC_Priority(this._flexD.Row, null, null, null, null);
                                break;
                            }
                            break;
                        case "CD_PJT":
                            this._flexD["CD_PJT"] = D.GetString(result.Rows[0]["NO_PROJECT"]);
                            this._flexD["NM_PJT"] = D.GetString(result.Rows[0]["NM_PROJECT"]);
                            this._flexD["SEQ_PROJECT"] = !(Config.MA_ENV.YN_UNIT == "Y") && (!(BASIC.GetMAEXC("구매-프로젝트도움창 UNIT사용여부") == "100") || !Config.MA_ENV.프로젝트사용) ? 0 : D.GetDecimal(result.Rows[0]["SEQ_PROJECT"]);
                            if (BASIC.GetMAEXC("발주등록(공장)-프로젝트별_의제매입세_구분") == "100" && D.GetString(this._flexD["CD_USERDEF14"]) == "001")
                            {
                                string str6 = this._biz.pjt_item_josun(D.GetString(this._flexD["CD_PJT"]));
                                decimal num6;
                                if (str6 != "")
                                {
                                    this._flexD[num1, "FG_TAX"] = str6;
                                    num6 = 0M;
                                    this._flexD[e.Row, "RATE_VAT"] = num6;
                                }
                                else
                                {
                                    this._flexD["FG_TAX"] = this._header.CurrentRow["FG_TAX"];
                                    this._flexD["RATE_VAT"] = this.cur부가세율.DecimalValue;
                                    num6 = this.cur부가세율.DecimalValue;
                                }
                                decimal num7 = num6 == 0M ? 0M : num6 / 100M;
                                this._flexD["VAT"] = !(num7 == 0M) && !(Convert.ToDecimal(this._flexD["AM"]) == 0M) ? Unit.원화금액(DataDictionaryTypes.PU, Convert.ToDecimal(this._flexD["AM"]) * num7) : 0;
                                this._flexD["AM_TOTAL"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flexD["AM"]) + D.GetDecimal(this._flexD["VAT"]));
                            }
                            if (this.m_sEnv_CC == "200")
                                this.SetCC(e.Row, string.Empty);
                            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "ZOONE")
                            {
                                DataTable infoZoo = this._biz.GetInfo_ZOO(result.CodeValue);
                                if (infoZoo != null && infoZoo.Rows.Count != 0)
                                {
                                    this._flexD["CD_SL"] = infoZoo.Rows[0]["CD_SL"];
                                    this._flexD["NM_SL"] = infoZoo.Rows[0]["NM_SL"];
                                }
                            }
                            if (Global.MainFrame.ServerKeyCommon == "UNIPOINT")
                            {
                                DataTable projectDetail = this._biz.Get_Project_Detail(D.GetString(this._flexD["CD_PJT"]));
                                if (projectDetail != null && projectDetail.Rows.Count != 0)
                                {
                                    this._flexD["CD_PARTNER_PJT"] = projectDetail.Rows[0]["CD_PARTNER"];
                                    this._flexD["LN_PARTNER_PJT"] = projectDetail.Rows[0]["LN_PARTNER"];
                                    this._flexD["NO_EMP_PJT"] = projectDetail.Rows[0]["NO_EMP"];
                                    this._flexD["NM_KOR_PJT"] = projectDetail.Rows[0]["NM_KOR"];
                                    this._flexD["END_USER"] = projectDetail.Rows[0]["END_USER"];
                                }
                            }
                            if (this.sPUIV == "200")
                            {
                                foreach (DataRow dataRow in this._flexIV.DataTable.Select("NO_POLINE = '" + D.GetString(this._flexD["NO_LINE"]) + "'"))
                                {
                                    dataRow["CD_PJT"] = D.GetString(result.Rows[0]["NO_PROJECT"]);
                                    int num8 = Config.MA_ENV.YN_UNIT == "Y" ? 0 : (!(BASIC.GetMAEXC("구매-프로젝트도움창 UNIT사용여부") == "100") ? 1 : (!Config.MA_ENV.프로젝트사용 ? 1 : 0));
                                    dataRow["SEQ_PROJECT"] = num8 != 0 ? 0 : D.GetDecimal(result.Rows[0]["SEQ_PROJECT"]);
                                }
                                break;
                            }
                            break;
                        case "NM_ITEMGRP":
                            if (this.bStandard && Global.MainFrame.ServerKey == "SINJINSM")
                            {
                                this._flexD["GRP_ITEM"] = D.GetString(result.Rows[0]["CD_ITEMGRP"]);
                                this._flexD["NM_ITEMGRP"] = D.GetString(result.Rows[0]["NM_ITEMGRP"]);
                                DataTable dataTable2 = this._biz.Check_ITEMGRP_SG(D.GetString(this._flexD["GRP_ITEM"]));
                                if (dataTable2.Rows.Count > 0)
                                {
                                    this._flexD["SG_TYPE"] = D.GetString(dataTable2.Rows[0]["SG_TYPE"]);
                                    this._flexD["QT_SG"] = D.GetDecimal(dataTable2.Rows[0]["QT_SG"]);
                                    this._flexD["UM_WEIGHT"] = D.GetDecimal(dataTable2.Rows[0]["UM_WEIGHT"]);
                                }
                                if (D.GetDecimal(this._flexD[e.Row, "QT_PO_MM"]) != 0M)
                                    this.금액계산(e.Row, 0M, D.GetDecimal(this._flexD[e.Row, "QT_PO_MM"]), "NM_ITEMGRP", 0M);
                                this._flexD["CD_ITEM"] = (D.GetString(result.Rows[0]["CD_ITEMGRP"]).Substring(1, 2) + '-' + D.GetDecimal(this._flexD["NUM_STND_ITEM_1"]).ToString("###############0.####") + '*' + D.GetDecimal(this._flexD["NUM_STND_ITEM_2"]).ToString("###############0.####") + '*' + D.GetDecimal(this._flexD["NUM_STND_ITEM_3"]).ToString("###############0.####"));
                                this._flexD["CD_ITEM"] = (D.GetString(this._flexD["CD_ITEM"]) + this.getCLS_S_code(D.GetString(this._flexD["CLS_S"]), D.GetString(this._flexD["CD_ITEM"]), D.GetString(this.cbo공장.SelectedValue)));
                                this._flexD["NM_ITEM"] = (D.GetString(this._flexD["NM_CLS_L"]) + ' ' + D.GetString(this._flexD.GetDataDisplay("NM_CLS_S")).Replace('"', ' ').Trim());
                                FlexGrid flexD = this._flexD;
                                object[] objArray1 = new object[] { D.GetString(result.Rows[0]["NM_ITEMGRP"]),
                                                                    '-',
                                                                    D.GetDecimal(this._flexD["NUM_STND_ITEM_1"]).ToString("###############0.####"),
                                                                    '*',
                                                                   null,
                                                                   null,
                                                                   null };
                                object[] objArray2 = objArray1;
                                decimal num9 = D.GetDecimal(this._flexD["NUM_STND_ITEM_2"]);
                                string str7 = num9.ToString("###############0.####");
                                objArray2[4] = str7;
                                objArray1[5] = '*';
                                object[] objArray3 = objArray1;
                                num9 = D.GetDecimal(this._flexD["NUM_STND_ITEM_3"]);
                                string str8 = num9.ToString("###############0.####");
                                objArray3[6] = str8;
                                string str9 = string.Concat(objArray1);
                                flexD["STND_ITEM"] = str9;
                                this._flexD["STND_ITEM"] = (this._flexD["STND_ITEM"].ToString() + this.getCLS_S_code(D.GetString(this._flexD["CLS_S"]), D.GetString(this._flexD["CD_ITEM"]), D.GetString(this.cbo공장.SelectedValue)));
                                break;
                            }
                            break;
                        case "NM_CLS_L":
                        case "CLS_L":
                            if (this.bStandard && BASIC.GetMAEXC("공장품목-대중소분류 종속관계 설정").Equals("001") && Global.MainFrame.ServerKey == "SINJINSM")
                            {
                                if (D.GetString(this._flexD["GRP_ITEM"]).Length < 3)
                                {
                                    this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this._flexD.Cols["NM_ITEMGRP"].Caption });
                                    e.Cancel = true;
                                    break;
                                }
                                this._flexD["CLS_M"] = "";
                                this._flexD["NM_CLS_M"] = "";
                                this._flexD["CLS_S"] = "";
                                this._flexD["NM_CLS_S"] = "";
                                FlexGrid flexD = this._flexD;
                                object[] objArray4 = new object[7];
                                objArray4[0] = D.GetString(this._flexD["GRP_ITEM"]).Substring(1, 2);
                                objArray4[1] = '-';
                                object[] objArray5 = objArray4;
                                decimal num11 = D.GetDecimal(this._flexD["NUM_STND_ITEM_1"]);
                                string str10 = num11.ToString("###############0.####");
                                objArray5[2] = str10;
                                objArray4[3] = '*';
                                object[] objArray6 = objArray4;
                                num11 = D.GetDecimal(this._flexD["NUM_STND_ITEM_2"]);
                                string str11 = num11.ToString("###############0.####");
                                objArray6[4] = str11;
                                objArray4[5] = '*';
                                objArray4[6] = D.GetDecimal(this._flexD["NUM_STND_ITEM_3"]).ToString("###############0.####");
                                string str12 = string.Concat(objArray4);
                                flexD["CD_ITEM"] = str12;
                                this._flexD["CD_ITEM"] = (D.GetString(this._flexD["CD_ITEM"]) + this.getCLS_S_code(D.GetString(this._flexD[e.Row, "CLS_S"]), D.GetString(this._flexD["CD_ITEM"]), D.GetString(this.cbo공장.SelectedValue)));
                                this._flexD["NM_ITEM"] = (D.GetString(result.Rows[0]["NM_SYSDEF"]) + ' ' + D.GetString(this._flexD.GetDataDisplay("NM_CLS_S")).Replace('"', ' ').Trim());
                                this._flexD["STND_ITEM"] = (D.GetString(this._flexD["NM_ITEMGRP"]) + '-' + D.GetDecimal(this._flexD["NUM_STND_ITEM_1"]).ToString("###############0.####") + '*' + D.GetDecimal(this._flexD["NUM_STND_ITEM_2"]).ToString("###############0.####") + '*' + D.GetDecimal(this._flexD["NUM_STND_ITEM_3"]).ToString("###############0.####"));
                                this._flexD["STND_ITEM"] = (this._flexD["STND_ITEM"].ToString() + this.getCLS_S_code(D.GetString(this._flexD[e.Row, "CLS_S"]), D.GetString(this._flexD["CD_ITEM"]), D.GetString(this.cbo공장.SelectedValue)));
                            }
                            if (Global.MainFrame.CurrentPageID == "P_PU_Z_JONGHAP_PO_REG2")
                            {
                                this._flexD["CLS_L"] = e.Result.CodeValue;
                                this._flexD["NM_ITEM"] = e.Result.CodeName;
                                this.calcRTPO_JONGHAP(e.Row, "NM_CLS_L");
                                break;
                            }
                            break;
                        case "NM_CLS_M":
                            if (this.bStandard && BASIC.GetMAEXC("공장품목-대중소분류 종속관계 설정").Equals("001") && Global.MainFrame.ServerKey == "SINJINSM")
                            {
                                if (D.GetString(this._flexD["GRP_ITEM"]).Length < 3)
                                {
                                    this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this._flexD.Cols["NM_ITEMGRP"].Caption });
                                    e.Cancel = true;
                                    break;
                                }
                                this._flexD["CLS_S"] = "";
                                this._flexD["NM_CLS_S"] = "";
                                FlexGrid flexD = this._flexD;
                                object[] objArray7 = new object[7];
                                objArray7[0] = D.GetString(this._flexD["GRP_ITEM"]).Substring(1, 2);
                                objArray7[1] = '-';
                                object[] objArray8 = objArray7;
                                decimal num13 = D.GetDecimal(this._flexD["NUM_STND_ITEM_1"]);
                                string str13 = num13.ToString("###############0.####");
                                objArray8[2] = str13;
                                objArray7[3] = '*';
                                object[] objArray9 = objArray7;
                                num13 = D.GetDecimal(this._flexD["NUM_STND_ITEM_2"]);
                                string str14 = num13.ToString("###############0.####");
                                objArray9[4] = str14;
                                objArray7[5] = '*';
                                object[] objArray10 = objArray7;
                                num13 = D.GetDecimal(this._flexD["NUM_STND_ITEM_3"]);
                                string str15 = num13.ToString("###############0.####");
                                objArray10[6] = str15;
                                string str16 = string.Concat(objArray7);
                                flexD["CD_ITEM"] = str16;
                                this._flexD["CD_ITEM"] = D.GetString(this._flexD["CD_ITEM"]) + this.getCLS_S_code(D.GetString(this._flexD["CLS_S"]), D.GetString(this._flexD["CD_ITEM"]), D.GetString(this.cbo공장.SelectedValue));
                                this._flexD["NM_ITEM"] = D.GetString(this._flexD["NM_CLS_L"]) + ' ' + D.GetString(this._flexD.GetDataDisplay("NM_CLS_S")).Replace('"', ' ').Trim();
                                this._flexD["STND_ITEM"] = D.GetString(this._flexD["NM_ITEMGRP"]) + '-' + D.GetDecimal(this._flexD["NUM_STND_ITEM_1"]).ToString("###############0.####") + '*' + D.GetDecimal(this._flexD["NUM_STND_ITEM_2"]).ToString("###############0.####") + '*' + D.GetDecimal(this._flexD["NUM_STND_ITEM_3"]).ToString("###############0.####");
                                this._flexD["STND_ITEM"] = this._flexD["STND_ITEM"].ToString() + this.getCLS_S_code(D.GetString(this._flexD["CLS_S"]), D.GetString(this._flexD["CD_ITEM"]), D.GetString(this.cbo공장.SelectedValue));
                                break;
                            }
                            break;
                        case "NM_CLS_S":
                            if (this.bStandard && Global.MainFrame.ServerKey == "SINJINSM")
                            {
                                if (D.GetString(this._flexD["GRP_ITEM"]).Length < 3)
                                {
                                    this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this._flexD.Cols["NM_ITEMGRP"].Caption });
                                    e.Cancel = true;
                                    break;
                                }
                                this._flexD["CD_ITEM"] = D.GetString(this._flexD["GRP_ITEM"]).Substring(1, 2) + '-' + D.GetDecimal(this._flexD["NUM_STND_ITEM_1"]).ToString("###############0.####") + '*' + D.GetDecimal(this._flexD["NUM_STND_ITEM_2"]).ToString("###############0.####") + '*' + D.GetDecimal(this._flexD["NUM_STND_ITEM_3"]).ToString("###############0.####");
                                this._flexD["CD_ITEM"] = D.GetString(this._flexD["CD_ITEM"]) + this.getCLS_S_code(D.GetString(result.Rows[0]["CD_SYSDEF"]), D.GetString(this._flexD["CD_ITEM"]), D.GetString(this.cbo공장.SelectedValue));
                                this._flexD["NM_ITEM"] = D.GetString(this._flexD["NM_CLS_L"]) + ' ' + D.GetString(result.Rows[0]["NM_SYSDEF"]).Replace('"', ' ').Trim();
                                FlexGrid flexD = this._flexD;
                                object[] objArray11 = new object[] { D.GetString(this._flexD["NM_ITEMGRP"]),
                                                                     '-',
                                                                     D.GetDecimal(this._flexD["NUM_STND_ITEM_1"]).ToString("###############0.####"),
                                                                     '*',
                                                                    null,
                                                                    null,
                                                                    null };
                                object[] objArray12 = objArray11;
                                decimal num15 = D.GetDecimal(this._flexD["NUM_STND_ITEM_2"]);
                                string str17 = num15.ToString("###############0.####");
                                objArray12[4] = str17;
                                objArray11[5] = '*';
                                object[] objArray13 = objArray11;
                                num15 = D.GetDecimal(this._flexD["NUM_STND_ITEM_3"]);
                                string str18 = num15.ToString("###############0.####");
                                objArray13[6] = str18;
                                string str19 = string.Concat(objArray11);
                                flexD["STND_ITEM"] = str19;
                                this._flexD["STND_ITEM"] = (this._flexD["STND_ITEM"].ToString() + this.getCLS_S_code(D.GetString(this._flexD["CLS_S"]), D.GetString(this._flexD["CD_ITEM"]), D.GetString(this.cbo공장.SelectedValue)));
                                break;
                            }
                            break;
                        case "CD_BUDGET":
                            this._flexD["CD_BIZPLAN"] = string.Empty;
                            this._flexD["NM_BIZPLAN"] = string.Empty;
                            this._flexD["CD_BGACCT"] = string.Empty;
                            this._flexD["NM_BGACCT"] = string.Empty;
                            this._flexD["CD_ACCT"] = string.Empty;
                            this._flexD["NM_ACCT"] = string.Empty;
                            break;
                        case "CD_BIZPLAN":
                            this._flexD["CD_BGACCT"] = string.Empty;
                            this._flexD["NM_BGACCT"] = string.Empty;
                            break;
                        case "CD_ITEM_ORIGIN":
                            if (MA.ServerKey(false, new string[] { "KMI" }))
                            {
                                this._flexD["NM_USERDEF1"] = string.Empty;
                                break;
                            }
                            break;
                    }
                }
                else if ((flexGrid).Name == this._flexDD.Name)
                {
                    foreach (DataRow row in e.Result.Rows)
                    {
                        if (e.Result.Rows[0] != row)
                            this.추가_Click(null, null);
                        flexGrid["CD_MATL"] = row["CD_ITEM"];
                        flexGrid["NM_ITEM"] = row["NM_ITEM"];
                        flexGrid["STND_ITEM"] = row["STND_ITEM"];
                        flexGrid["STND_DETAIL_ITEM"] = row["STND_DETAIL_ITEM"];
                        flexGrid["UNIT_MO"] = row["UNIT_MO"];
                        decimal num16 = this._flexD.CDecimal(this._flexD["QT_PO"]);
                        flexGrid["QT_NEED"] = (num16 != 0M ? Math.Round(num16 * flexGrid.CDecimal(flexGrid["QT_NEED_UNIT"]), 4, MidpointRounding.AwayFromZero) : flexGrid.CDecimal(flexGrid["QT_NEED_UNIT"]));
                        flexGrid["QT_LOSS"] = (flexGrid.CDecimal(flexGrid["QT_NEED"]) - flexGrid.CDecimal(flexGrid["QT_REQ"]));
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this._flexD.Redraw = true;
                this.mDataArea.Enabled = true;
                this.flowLayoutPanel1.Enabled = true;
            }
        }

        private void _flexD_HelpClick(object sender, EventArgs e)
        {
            try
            {
                if (!(sender is FlexGrid flexGrid))
                    return;
                switch (flexGrid.Cols[(flexGrid).Col].Name)
                {
                    case "CD_PJT":
                        P_SA_PRJ_SUB pSaPrjSub = new P_SA_PRJ_SUB();
                        if (((Form)pSaPrjSub).ShowDialog(this) == DialogResult.OK)
                        {
                            this._flexD["CD_PJT"] = pSaPrjSub.returnParams[0];
                            this._flexD["NM_PJT"] = pSaPrjSub.returnParams[4];
                            break;
                        }
                        break;
                    case "UM_EX_PO":
                        if (!flexGrid.HasNormalRow || flexGrid["CD_ITEM"].ToString() == "")
                            break;
                        if (MA.ServerKey(false, new string[] { "CNCC" }))
                        {
                            if (D.GetString(flexGrid["CD_ITEM"]) != string.Empty && D.GetString(flexGrid["TP_ITEM"]) == string.Empty)
                            {
                                DataTable dataTable = DBHelper.GetDataTable("SELECT TP_ITEM FROM MA_PITEM WHERE CD_COMPANY = '" + this.LoginInfo.CompanyCode + "' AND CD_PLANT = '" + D.GetString(this.cbo공장.SelectedValue) + "' AND CD_ITEM = '" + D.GetString(flexGrid["CD_ITEM"]) + "'");
                                flexGrid["TP_ITEM"] = dataTable.Rows.Count > 0 ? D.GetString(dataTable.Rows[0]["TP_ITEM"]) : string.Empty;
                            }
                            if (D.GetString(flexGrid["TP_ITEM"]) == "MAS")
                                break;
                        }
                        P_PU_UM_HISTORY pPuUmHistory = new P_PU_UM_HISTORY(this.ctx거래처.CodeValue, this.ctx거래처.CodeName, flexGrid["CD_ITEM"].ToString(), flexGrid["NM_ITEM"].ToString(), this.ctx발주형태.CodeValue, this.ctx발주형태.CodeName, this.cbo환정보.SelectedValue.ToString(), this._header.CurrentRow["FG_TRANS"].ToString());
                        if (((Form)pPuUmHistory).ShowDialog(this) == DialogResult.OK && pPuUmHistory.UM != "")
                        {
                            this._flexD["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, flexGrid.CDecimal(pPuUmHistory.UM));
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

        private void Grid_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                FlexGrid flexGrid = sender as FlexGrid;
                if ((flexGrid).Name == this._flexD.Name)
                {
                    string str1 = ((C1FlexGridBase)sender).GetData(e.Row, e.Col).ToString();
                    string editData = ((FlexGrid)sender).EditData;
                    if (str1.ToUpper() == editData.ToUpper())
                        return;
                    string name = ((C1FlexGridBase)sender).Cols[e.Col].Name;
                    if (Global.MainFrame.CurrentPageID != "P_PU_Z_JONGHAP_PO_REG2" && this._flexD["CD_ITEM"].ToString() == "")
                        return;
                    decimal num1 = 0.1M;
                    decimal num2 = 0M;
                    decimal num3 = 0M;
                    decimal ldb_amEx1 = 0M;
                    decimal num4 = 0M;
                    decimal d = D.GetDecimal(this._flexD[e.Row, "RATE_VAT"]) == 0M ? 0M : D.GetDecimal(this._flexD[e.Row, "RATE_VAT"]) / 100M;
                    decimal num5 = D.GetDecimal(this._flexD[e.Row, "QT_PO_MM"]);
                    decimal num6 = D.GetDecimal(this._flexD[e.Row, "UM_EX_PO"]);
                    decimal num7 = D.GetDecimal(this._header.CurrentRow["RT_EXCH"]) == 0M ? 1M : D.GetDecimal(this._header.CurrentRow["RT_EXCH"]);
                    string ps_taxp = D.GetString(this._flexD[e.Row, "FG_TAX"]);
                    bool flag = D.GetString(this._flexD[e.Row, "TP_UM_TAX"]) == "001";
                    switch (this._flexD.Cols[e.Col].Name)
                    {
                        case "QT_PO_MM":
                            if (this._flexD.CDecimal(editData) < this._flexD.CDecimal(this._flexD[e.Row, "QT_REQ_MM"]))
                            {
                                this.ShowMessage(공통메세지._은_보다크거나같아야합니다, new string[] { this.DD("발주수량"),
                                                                                                    this.DD("입고수량") });
                                ((FlexGrid)sender)["QT_PO_MM"] = ((C1FlexGridBase)sender).GetData(e.Row, e.Col);
                                break;
                            }
                            if (Global.MainFrame.ServerKey == "KPCI" && D.GetString(flexGrid["NO_RELATION"]) != string.Empty)
                            {
                                DataRow dataRow = this._biz.계약잔량체크(D.GetString(flexGrid["NO_RELATION"]), D.GetDecimal(flexGrid["SEQ_RELATION"]));
                                if (D.GetString(dataRow["CD_TYPE"]) == "001" && D.GetDecimal(editData) > D.GetDecimal(dataRow["QT_CON"]))
                                {
                                    this.ShowMessage("잔량을 초과하였습니다.");
                                    flexGrid["QT_SO"] = str1;
                                    e.Cancel = true;
                                    break;
                                }
                            }
                            if (this._m_Company_only == "001")
                                this.AsahiKasei_Only_ValidateEdit(e.Row, D.GetDecimal(editData), "QT_PO_MM");
                            this.금액계산(e.Row, this._flexD.CDecimal(this._flexD[e.Row, "UM_EX_PO"]), Convert.ToDecimal(editData), "QT_PO_MM", Convert.ToDecimal(editData));
                            if (!this.bStandard)
                                this._flexD[e.Row, "QT_WEIGHT"] = (this._flexD.CDecimal(editData) * this._flexD.CDecimal(this._flexD[e.Row, "WEIGHT"]));
                            this.CalcRebate(D.GetDecimal(editData), D.GetDecimal(this._flexD["UM_REBATE"]));
                            this.사급자재변경구문(D.GetDecimal(editData));
                            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "WINPLUS")
                            {
                                this._flexD[e.Row, "NUM_USERDEF1"] = this._flexD[e.Row, "NUM_USERDEF2"] = 0;
                                break;
                            }
                            if (MA.ServerKey(false, new string[] { "FDAMK" }))
                            {
                                this.setCalBoxEa_FDAMK("QT_PO_MM", e.Row);
                                break;
                            }
                            if (Global.MainFrame.ServerKeyCommon == "DHPENG" && D.GetDecimal(flexGrid["WEIGHT"]) > 0M)
                            {
                                this.calWeight_DHPENG("QT_PO_MM", e.Row, D.GetDecimal(editData));
                                break;
                            }
                            break;
                        case "UM_EX_PO":
                            if (BASIC.GetMAEXC_Menu(nameof(P_CZ_PU_PO_WINTEC), "PU_Z00000002") == "200" && (D.GetString(flexGrid["NO_PR"]) != string.Empty || D.GetString(flexGrid["NO_APP"]) != string.Empty) && D.GetDecimal(flexGrid["AM_OLD"]) < Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(editData) * this._flexD.CDecimal(this._flexD[e.Row, "QT_PO_MM"]) * this.cur환정보.DecimalValue))
                            {
                                this.ShowMessage(공통메세지._은_보다작거나같아야합니다, new string[] { "변경금액", "적용금액" });
                                flexGrid["UM_EX_PO"] = str1;
                                e.Cancel = true;
                                break;
                            }
                            if (this._m_Company_only == "001")
                                this.AsahiKasei_Only_ValidateEdit(e.Row, D.GetDecimal(editData), "UM_EX_PO");
                            this.금액계산(e.Row, Convert.ToDecimal(editData), this._flexD.CDecimal(this._flexD[e.Row, "QT_PO_MM"]), "UM_EX_PO", Convert.ToDecimal(editData));
                            if (Global.MainFrame.ServerKeyCommon == "DHPENG")
                            {
                                flexGrid["NUM_USERDEF1"] = 0M;
                                break;
                            }
                            break;
                        case "UM_EX":
                            this.금액계산(e.Row, Convert.ToDecimal(editData), this._flexD.CDecimal(this._flexD[e.Row, "QT_PO"]), "UM_EX", Convert.ToDecimal(editData));
                            if (Global.MainFrame.ServerKeyCommon == "DHPENG")
                            {
                                flexGrid["NUM_USERDEF1"] = 0M;
                                break;
                            }
                            break;
                        case "NUM_USERDEF3_PO":
                            if (Global.MainFrame.ServerKeyCommon == "LUKEN")
                            {
                                this.금액계산(e.Row, Convert.ToDecimal(editData), this._flexD.CDecimal(this._flexD[e.Row, "QT_PO_MM"]), "UM_EX_PO", Convert.ToDecimal(editData));
                                break;
                            }
                            break;
                        case "AM_EX":
                            if (BASIC.GetMAEXC_Menu(nameof(P_CZ_PU_PO_WINTEC), "PU_Z00000002") == "200" && (D.GetString(flexGrid["NO_PR"]) != string.Empty || D.GetString(flexGrid["NO_APP"]) != string.Empty) && D.GetDecimal(flexGrid["AM_OLD"]) < Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(editData) * this.cur환정보.DecimalValue))
                            {
                                this.ShowMessage(공통메세지._은_보다작거나같아야합니다, new string[] { "변경금액", "적용금액" });
                                flexGrid["AM_EX"] = str1;
                                e.Cancel = true;
                                break;
                            }
                            this.금액계산(e.Row, this._flexD.CDecimal(this._flexD[e.Row, "UM_EX_PO"]), this._flexD.CDecimal(this._flexD[e.Row, "QT_PO_MM"]), "AM_EX", this._flexD.CDecimal(editData));
                            if (this._m_Company_only == "001")
                            {
                                this.AsahiKasei_Only_ValidateEdit(e.Row, D.GetDecimal(editData), "AM_EX");
                                break;
                            }
                            if (Global.MainFrame.ServerKeyCommon.Contains("SHTP"))
                            {
                                this._flexD["AM_EX_TRANS"] = this._flexD["AM_TRANS"] = 0;
                                break;
                            }
                            break;
                        case "AM":
                            if (BASIC.GetMAEXC_Menu(nameof(P_CZ_PU_PO_WINTEC), "PU_Z00000002") == "200" && (D.GetString(flexGrid["NO_PR"]) != string.Empty || D.GetString(flexGrid["NO_APP"]) != string.Empty) && D.GetDecimal(flexGrid["AM_OLD"]) < D.GetDecimal(editData))
                            {
                                this.ShowMessage(공통메세지._은_보다작거나같아야합니다, new string[] { "변경금액", "적용금액" });
                                flexGrid["AM"] = str1;
                                e.Cancel = true;
                                break;
                            }
                            if (Math.Abs(D.GetDecimal(this._flexD["AM_EX"]) * D.GetDecimal(this._header.CurrentRow["RT_EXCH"]) - D.GetDecimal(editData)) > 500M)
                            {
                                this.ShowMessage("500원 범위에서 수정 가능합니다.(단수차 관리)");
                                this._flexD["AM"] = D.GetDecimal(str1);
                                e.Cancel = true;
                                break;
                            }
                            this.부가세만계산();
                            break;
                        case "VAT":
                            if (MA.ServerKey(false, new string[] { "DAEJOOKC", "SATREC", "MEERE", "DODRAM", "DHF5" }))
                            {
                                if (Math.Abs(Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flexD["AM"]) * d) - D.GetDecimal(editData)) > 100M)
                                {
                                    this.ShowMessage("과세율에 해당하는 부가세 금액 표준오차보다 초과합니다.");
                                    this._flexD["VAT"] = D.GetDecimal(str1);
                                    e.Cancel = true;
                                    break;
                                }
                                this._flexD["AM_TOTAL"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flexD["AM"]) + D.GetDecimal(editData));
                                this.SUMFunction();
                                break;
                            }
                            if (this._flexD.CDecimal(this._flexD[e.Row, "RATE_VAT"]) == 0.0M && this.cur부가세율.DecimalValue == 0M)
                                this._flexD[e.Row, "VAT"] = 0;
                            else
                                this.SUMFunction();
                            break;
                        case "DT_LIMIT":
                        case "DT_PLAN":
                            if (editData.Trim().Length != 8)
                            {
                                this.ShowMessage(공통메세지.입력형식이올바르지않습니다, new string[0]);
                                if (this._flexD.Editor != null)
                                    this._flexD.Editor.Text = string.Empty;
                                e.Cancel = true;
                                break;
                            }
                            if (!this._flexD.IsDate(name))
                            {
                                this.ShowMessage(공통메세지.입력형식이올바르지않습니다, new string[0]);
                                if (this._flexD.Editor != null)
                                    this._flexD.Editor.Text = string.Empty;
                                e.Cancel = true;
                                break;
                            }
                            break;
                        case "FG_TAX":
                            num4 = Global.MainFrame.LoginInfo.CompanyLanguage == 0 ? decimal.Round(Math.Round(num5 * num6 * num7), MidpointRounding.AwayFromZero) : Unit.원화금액(DataDictionaryTypes.PU, num5 * num6 * num7);
                            if (this._header.CurrentRow["FG_TRANS"].ToString() != "001" || this._flexD["FG_TAX"].ToString() == string.Empty)
                            {
                                num1 = this.cur부가세율.DecimalValue;
                            }
                            else
                            {
                                if (this.의제매입여부(editData) && this.s_vat_fictitious == "100" || Global.MainFrame.ServerKeyCommon.Contains("INITECH"))
                                {
                                    flag = true;
                                    this._flexD[e.Row, "TP_UM_TAX"] = "001";
                                }
                                else
                                    this._flexD[e.Row, "TP_UM_TAX"] = D.GetString(this.cbo부가세여부.SelectedValue);
                                if (this.의제매입여부(editData) && this.s_vat_fictitious == "000")
                                {
                                    num1 = 0M;
                                }
                                else
                                {
                                    DataTable tableSearch = ComFunc.GetTableSearch("MA_CODEDTL", new object[] { this.LoginInfo.CompanyCode,
                                                                                                                "MA_B000046",
                                                                                                                editData });
                                    if (tableSearch.Rows.Count > 0 && tableSearch.Rows[0]["CD_FLAG1"].ToString() != string.Empty)
                                        num1 = Convert.ToDecimal(tableSearch.Rows[0]["CD_FLAG1"]);
                                }
                            }
                            this._flexD[e.Row, "RATE_VAT"] = num1;
                            if (num5 == 0M)
                                break;
                            Calc.GetAmt(num5, num6, num7, ps_taxp, num1, 모듈.PUR, flag, out ldb_amEx1, out num3, out num2);
                            this._flexD[e.Row, "VAT"] = num2;
                            this._flexD[e.Row, "AM"] = num3;
                            this._flexD[e.Row, "AM_EX"] = ldb_amEx1;
                            this._flexD[e.Row, "AM_TOTAL"] = Calc.합계금액(num3, num2);
                            this.SUMFunction();
                            this.기성매입재계산(ldb_amEx1);
                            break;
                        case "WEIGHT":
                            if (!this.bStandard)
                            {
                                this._flexD[e.Row, "QT_WEIGHT"] = this._flexD.CDecimal(this._flexD[e.Row, "QT_PO_MM"]) * this._flexD.CDecimal(editData);
                                break;
                            }
                            break;
                        case "UM_EX_AR":
                            this.AsahiKasei_Only_ValidateEdit(e.Row, D.GetDecimal(editData), "UM_EX_AR");
                            break;
                        case "TP_UM_TAX":
                            if (num5 == 0M)
                                break;
                            decimal num17;
                            decimal num18;
                            decimal ldb_amEx2;
                            if (flag)
                            {
                                decimal num19 = Global.MainFrame.LoginInfo.CompanyLanguage == 0 ? decimal.Round(num5 * num6 * num7, MidpointRounding.AwayFromZero) : Unit.원화금액(DataDictionaryTypes.PU, num5 * num6 * num7);
                                num17 = (!this.의제매입여부(D.GetString(this._flexD["FG_TAX"])) || !(this.s_vat_fictitious == "100")) && !Global.MainFrame.ServerKeyCommon.Contains("INITECH") ? (Global.MainFrame.LoginInfo.CompanyLanguage == 0 ? decimal.Round(num19 / ++d * d, MidpointRounding.AwayFromZero) : Unit.원화금액(DataDictionaryTypes.PU, num19 / ++d * d)) : Unit.원화금액(DataDictionaryTypes.PU, num19 * d);
                                num18 = Unit.원화금액(DataDictionaryTypes.PU, num19 - num17);
                                ldb_amEx2 = Unit.외화금액(DataDictionaryTypes.PU, num18 / num7);
                            }
                            else
                            {
                                num18 = Unit.원화금액(DataDictionaryTypes.PU, num5 * num6 * num7);
                                num17 = Unit.원화금액(DataDictionaryTypes.PU, num18 * d);
                                num4 = Unit.원화금액(DataDictionaryTypes.PU, num18 + num17);
                                ldb_amEx2 = Unit.외화금액(DataDictionaryTypes.PU, num18 / num7);
                            }
                            this._flexD[e.Row, "UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, num6);
                            this._flexD[e.Row, "UM_P"] = Unit.원화단가(DataDictionaryTypes.PU, num6 * num7);
                            this._flexD[e.Row, "UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, num6 / (D.GetDecimal(this._flexD["RT_PO"]) == 0M ? 1M : D.GetDecimal(this._flexD["RT_PO"])));
                            this._flexD[e.Row, "UM"] = Unit.원화단가(DataDictionaryTypes.PU, num6 / (D.GetDecimal(this._flexD["RT_PO"]) == 0M ? 1M : D.GetDecimal(this._flexD["RT_PO"])) * num7);
                            this._flexD[e.Row, "AM_EX"] = ldb_amEx2;
                            this._flexD[e.Row, "AM"] = num18;
                            this._flexD[e.Row, "VAT"] = num17;
                            this._flexD[e.Row, "AM_TOTAL"] = (num18 + num17);
                            this.SUMFunction();
                            this.기성매입재계산(ldb_amEx2);
                            break;
                        case "AM_TOTAL":
                            if (num5 == 0M || !flag)
                                break;
                            decimal num20 = Global.MainFrame.LoginInfo.CompanyLanguage == 0 ? decimal.Round(D.GetDecimal(this._flexD.EditData), MidpointRounding.AwayFromZero) : Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flexD.EditData));
                            decimal num21 = (!this.의제매입여부(ps_taxp) || !(this.s_vat_fictitious == "100")) && !Global.MainFrame.ServerKeyCommon.Contains("INITECH") ? (Global.MainFrame.LoginInfo.CompanyLanguage == 0 ? decimal.Round(num20 / ++d * d, MidpointRounding.AwayFromZero) : Unit.원화금액(DataDictionaryTypes.PU, num20 / ++d * d)) : Unit.원화금액(DataDictionaryTypes.PU, num20 * d);
                            decimal num22 = Unit.원화금액(DataDictionaryTypes.PU, num20 - num21);
                            decimal ldb_amEx3 = Unit.외화금액(DataDictionaryTypes.PU, num22 / num7);
                            this._flexD[e.Row, "AM_EX"] = ldb_amEx3;
                            this._flexD[e.Row, "AM"] = num22;
                            this._flexD[e.Row, "VAT"] = num21;
                            decimal num23 = num20 / num5 / num7;
                            this._flexD[e.Row, "UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, num23);
                            this._flexD[e.Row, "UM_P"] = Unit.원화단가(DataDictionaryTypes.PU, num23 * num7);
                            this._flexD[e.Row, "UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, UDecimal.Getdivision(num23, D.GetDecimal(this._flexD["RT_PO"]) == 0M ? 1M : D.GetDecimal(this._flexD["RT_PO"])));
                            this._flexD[e.Row, "UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(this._flexD[e.Row, "UM_EX"]) * num7);
                            this.SUMFunction();
                            this.기성매입재계산(ldb_amEx3);
                            break;
                        case "AM_EX_TRANS":
                            this._flexD["AM_TRANS"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(editData) * D.GetDecimal(this.cur환정보.Text));
                            if (Global.MainFrame.ServerKeyCommon.Contains("SHTP"))
                            {
                                this._flexD["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, num5 * D.GetDecimal(this._flexD["NUM_USERDEF1"]) + num5 * D.GetDecimal(editData));
                                this.금액계산(e.Row, this._flexD.CDecimal(this._flexD[e.Row, "UM_EX_PO"]), this._flexD.CDecimal(this._flexD[e.Row, "QT_PO_MM"]), "AM_EX", D.GetDecimal(this._flexD["AM_EX"]));
                                break;
                            }
                            break;
                        case "AM_TRANS":
                            this._flexD["AM_EX_TRANS"] = Unit.외화금액(DataDictionaryTypes.PU, UDecimal.Getdivision(D.GetDecimal(editData), D.GetDecimal(this.cur환정보.Text)));
                            if (Global.MainFrame.ServerKeyCommon.Contains("SHTP"))
                            {
                                this._flexD["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, num5 * D.GetDecimal(this._flexD["NUM_USERDEF1"]) + num5 * D.GetDecimal(this._flexD["AM_EX_TRANS"]));
                                this.금액계산(e.Row, this._flexD.CDecimal(this._flexD[e.Row, "UM_EX_PO"]), this._flexD.CDecimal(this._flexD[e.Row, "QT_PO_MM"]), "AM_EX", D.GetDecimal(this._flexD["AM_EX"]));
                                break;
                            }
                            break;
                        case "UM_REBATE":
                            this.CalcRebate(D.GetDecimal(this._flexD["QT_PO_MM"]), D.GetDecimal(editData));
                            break;
                        case "AM_REBATE_EX":
                            this._flexD["UM_REBATE"] = Unit.외화단가(DataDictionaryTypes.PU, UDecimal.Getdivision(D.GetDecimal(editData), D.GetDecimal(this._flexD["QT_PO_MM"])));
                            this._flexD["AM_REBATE"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(editData) * D.GetDecimal(this.cur환정보.Text));
                            break;
                        case "AM_REBATE":
                            this._flexD["AM_REBATE_EX"] = Unit.외화금액(DataDictionaryTypes.PU, UDecimal.Getdivision(D.GetDecimal(editData), D.GetDecimal(this.cur환정보.Text)));
                            this._flexD["UM_REBATE"] = Unit.외화단가(DataDictionaryTypes.PU, UDecimal.Getdivision(D.GetDecimal(this._flexD["AM_REBATE_EX"]), D.GetDecimal(this._flexD["QT_PO_MM"])));
                            break;
                        case "NUM_USERDEF1":
                            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "WINPLUS")
                            {
                                decimal maItemgrp = this._biz.GET_MA_ITEMGRP(D.GetString(flexGrid[e.Row, "CD_ITEMGRP"]));
                                this._flexD[e.Row, "QT_PO_MM"] = decimal.Round(D.GetDecimal(editData) * D.GetDecimal(this._flexD[e.Row, "NUM_USERDEF2"]) / 10000M, 1, MidpointRounding.AwayFromZero);
                                if (!(maItemgrp == 0M) || !(D.GetDecimal(this._flexD[e.Row, "QT_PO_MM"]) == 0M))
                                {
                                    if (maItemgrp > D.GetDecimal(this._flexD[e.Row, "QT_PO_MM"]))
                                        this._flexD[e.Row, "QT_PO_MM"] = maItemgrp;
                                    this.금액계산(e.Row, this._flexD.CDecimal(this._flexD[e.Row, "UM_EX_PO"]), Convert.ToDecimal(this._flexD[e.Row, "QT_PO_MM"]), "QT_PO_MM", Convert.ToDecimal(this._flexD[e.Row, "QT_PO_MM"]));
                                    break;
                                }
                                break;
                            }
                            if (Global.MainFrame.ServerKeyCommon.Contains("SHTP"))
                            {
                                this._flexD["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, num5 * D.GetDecimal(editData) + num5 * D.GetDecimal(this._flexD["AM_EX_TRANS"]));
                                this.금액계산(e.Row, this._flexD.CDecimal(this._flexD[e.Row, "UM_EX_PO"]), this._flexD.CDecimal(this._flexD[e.Row, "QT_PO_MM"]), "AM_EX", D.GetDecimal(this._flexD["AM_EX"]));
                                break;
                            }
                            if (MA.ServerKey(false, new string[] { "FDAMK" }))
                            {
                                this.setCalBoxEa_FDAMK("NUM_USERDEF1", e.Row);
                                break;
                            }
                            if (Global.MainFrame.ServerKeyCommon == "DHPENG")
                            {
                                this.calWeight_DHPENG("NUM_USERDEF1", e.Row, D.GetDecimal(editData));
                                break;
                            }
                            break;
                        case "NUM_USERDEF2":
                            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "WINPLUS")
                            {
                                decimal maItemgrp = this._biz.GET_MA_ITEMGRP(D.GetString(flexGrid[e.Row, "CD_ITEMGRP"]));
                                this._flexD[e.Row, "QT_PO_MM"] = decimal.Round(D.GetDecimal(editData) * D.GetDecimal(this._flexD[e.Row, "NUM_USERDEF1"]) / 10000M, 1, MidpointRounding.AwayFromZero);
                                if (!(maItemgrp == 0M) || !(D.GetDecimal(this._flexD[e.Row, "QT_PO_MM"]) == 0M))
                                {
                                    if (maItemgrp > D.GetDecimal(this._flexD[e.Row, "QT_PO_MM"]))
                                        this._flexD[e.Row, "QT_PO_MM"] = maItemgrp;
                                    this.금액계산(e.Row, this._flexD.CDecimal(this._flexD[e.Row, "UM_EX_PO"]), Convert.ToDecimal(this._flexD[e.Row, "QT_PO_MM"]), "QT_PO_MM", Convert.ToDecimal(this._flexD[e.Row, "QT_PO_MM"]));
                                    break;
                                }
                                break;
                            }
                            if (MA.ServerKey(false, new string[] { "FDAMK" }))
                            {
                                this.setCalBoxEa_FDAMK("NUM_USERDEF2", e.Row);
                                break;
                            }
                            break;
                        case "CLS_L":
                        case "CLS_S":
                        case "NM_ITEMGRP":
                        case "NUM_STND_ITEM_1":
                        case "NUM_STND_ITEM_2":
                        case "NUM_STND_ITEM_3":
                            if (Global.MainFrame.ServerKey == "SINJINSM")
                            {
                                FlexGrid flexD1 = this._flexD;
                                int row1 = e.Row;
                                object[] objArray1 = new object[7];
                                objArray1[0] = D.GetString(this._flexD[e.Row, "GRP_ITEM"]).Substring(1, 2);
                                objArray1[1] = '-';
                                object[] objArray2 = objArray1;
                                decimal num24 = D.GetDecimal(this._flexD[e.Row, "NUM_STND_ITEM_1"]);
                                string str2 = num24.ToString("###############0.####");
                                objArray2[2] = str2;
                                objArray1[3] = '*';
                                object[] objArray3 = objArray1;
                                num24 = D.GetDecimal(this._flexD[e.Row, "NUM_STND_ITEM_2"]);
                                string str3 = num24.ToString("###############0.####");
                                objArray3[4] = str3;
                                objArray1[5] = '*';
                                object[] objArray4 = objArray1;
                                num24 = D.GetDecimal(this._flexD[e.Row, "NUM_STND_ITEM_3"]);
                                string str4 = num24.ToString("###############0.####");
                                objArray4[6] = str4;
                                string str5 = string.Concat(objArray1);
                                flexD1[row1, "CD_ITEM"] = str5;
                                this._flexD[e.Row, "CD_ITEM"] = (D.GetString(this._flexD[e.Row, "CD_ITEM"]) + this.getCLS_S_code(D.GetString(this._flexD[e.Row, "CLS_S"]), D.GetString(this._flexD[e.Row, "CD_ITEM"]), D.GetString(this.cbo공장.SelectedValue)));
                                this._flexD[e.Row, "NM_ITEM"] = (D.GetString(this._flexD.GetDataDisplay(e.Row, "NM_CLS_L")).Replace('"', ' ').Trim() + ' ' + D.GetString(this._flexD.GetDataDisplay(e.Row, "NM_CLS_S")).Replace('"', ' ').Trim());
                                FlexGrid flexD2 = this._flexD;
                                int row2 = e.Row;
                                object[] objArray5 = new object[7];
                                objArray5[0] = D.GetString(this._flexD[e.Row, "NM_ITEMGRP"]);
                                objArray5[1] = '-';
                                object[] objArray6 = objArray5;
                                num24 = D.GetDecimal(this._flexD[e.Row, "NUM_STND_ITEM_1"]);
                                string str6 = num24.ToString("###############0.####");
                                objArray6[2] = str6;
                                objArray5[3] = '*';
                                object[] objArray7 = objArray5;
                                num24 = D.GetDecimal(this._flexD[e.Row, "NUM_STND_ITEM_2"]);
                                string str7 = num24.ToString("###############0.####");
                                objArray7[4] = str7;
                                objArray5[5] = '*';
                                object[] objArray8 = objArray5;
                                num24 = D.GetDecimal(this._flexD[e.Row, "NUM_STND_ITEM_3"]);
                                string str8 = num24.ToString("###############0.####");
                                objArray8[6] = str8;
                                string str9 = string.Concat(objArray5);
                                flexD2[row2, "STND_ITEM"] = str9;
                                this._flexD[e.Row, "STND_ITEM"] = (this._flexD[e.Row, "STND_ITEM"].ToString() + this.getCLS_S_code(D.GetString(this._flexD[e.Row, "CLS_S"]), D.GetString(this._flexD[e.Row, "CD_ITEM"]), D.GetString(this.cbo공장.SelectedValue)));
                            }
                            this.calcAM(e.Row);
                            if (Global.MainFrame.CurrentPageID == "P_PU_Z_JONGHAP_PO_REG2")
                            {
                                this._flexD[e.Row, this._flexD.Cols[e.Col].Name] = editData;
                                this._flexD.DataTable.Rows[e.Row - this._flexD.Rows.Fixed][this._flexD.Cols[e.Col].Name] = editData;
                                this.calcRTPO_JONGHAP(e.Row, this._flexD.Cols[e.Col].Name);
                                break;
                            }
                            break;
                        case "NUM_STND_ITEM_4":
                        case "NUM_STND_ITEM_5":
                            if (Global.MainFrame.CurrentPageID == "P_PU_Z_JONGHAP_PO_REG2")
                            {
                                this._flexD[e.Row, this._flexD.Cols[e.Col].Name] = editData;
                                this._flexD.DataTable.Rows[e.Row - this._flexD.Rows.Fixed][this._flexD.Cols[e.Col].Name] = editData;
                                this.calcRTPO_JONGHAP(e.Row, this._flexD.Cols[e.Col].Name);
                                break;
                            }
                            break;
                        case "UM_WEIGHT":
                            this.calcAM(e.Row);
                            break;
                        case "TOT_WEIGHT":
                            this.calcAM(e.Row, D.GetDecimal(editData));
                            break;
                        case "NUM_USERDEF4_PO":
                            this.금액계산(e.Row, this._flexD.CDecimal(this._flexD[e.Row, "UM_EX_PO"]), this._flexD.CDecimal(this._flexD[e.Row, "QT_PO_MM"]), "NUM_USERDEF4_PO", this._flexD.CDecimal(editData));
                            break;
                        case "NM_USERDEF4_PO":
                            if (Global.MainFrame.ServerKeyCommon.Contains("JSREMK"))
                            {
                                DataTable jsremkDt = this._biz.GET_JSREMK_DT(new string[] { D.GetString(this._flexD[e.Row, "CD_PLANT"]),
                                                                                            D.GetString(this._flexD[e.Row, "CD_ITEM"]) });
                                if (jsremkDt != null && jsremkDt.Rows.Count > 0)
                                {
                                    DateTime exact = DateTime.ParseExact(editData, "yyyyMMdd", null);
                                    FlexGrid flexD3 = this._flexD;
                                    int row3 = e.Row;
                                    string str10;
                                    if (D.GetInt(jsremkDt.Rows[0]["NUM_USERDEF1"]) != 0)
                                    {
                                        DateTime dateTime = exact.AddMonths(D.GetInt(jsremkDt.Rows[0]["NUM_USERDEF1"]));
                                        dateTime = dateTime.AddDays(-1.0);
                                        str10 = dateTime.ToString("yyyyMMdd");
                                    }
                                    else
                                        str10 = D.GetString(editData);
                                    flexD3[row3, "DATE_USERDEF1"] = str10;
                                    FlexGrid flexD4 = this._flexD;
                                    int row4 = e.Row;
                                    string str11;
                                    if (D.GetInt(jsremkDt.Rows[0]["NUM_USERDEF3"]) != 0)
                                    {
                                        DateTime dateTime = exact.AddMonths(D.GetInt(jsremkDt.Rows[0]["NUM_USERDEF3"]));
                                        dateTime = dateTime.AddDays(-1.0);
                                        str11 = dateTime.ToString("yyyyMMdd");
                                    }
                                    else
                                        str11 = D.GetString(editData);
                                    flexD4[row4, "DATE_USERDEF2"] = str11;
                                }
                                break;
                            }
                            break;
                        case "RT_PO":
                            if (Global.MainFrame.CurrentPageID == "P_PU_Z_JONGHAP_PO_REG2")
                            {
                                this.calcRTPO_JONGHAP(e.Row, this._flexD.Cols[e.Col].Name);
                                break;
                            }
                            break;
                        case "QT_PO":
                            if (Global.MainFrame.CurrentPageID == "P_PU_Z_JONGHAP_PO_REG2")
                            {
                                this.calcRTPO_JONGHAP(e.Row, this._flexD.Cols[e.Col].Name);
                                break;
                            }
                            break;
                    }
                }
                else if ((flexGrid).Name == this._flexDD.Name)
                {
                    switch (flexGrid.Cols[(flexGrid).Col].Name)
                    {
                        case "QT_NEED":
                            this._flexD["QT_NEED_UNIT"] = Math.Round(this._flexD.CDecimal(this._flexD["QT_PO"]) == 0M ? 0M : this._flexD.CDecimal(this._flexDD["QT_NEED"]) / this._flexD.CDecimal(this._flexD["QT_PO"]), 4, MidpointRounding.AwayFromZero);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void SetExchageMoney()
        {
            try
            {
                if (!this._flexD.HasNormalRow)
                    return;
                foreach (DataRow row in this._flexD.DataTable.Rows)
                {
                    decimal num1 = 0M;
                    string empty = string.Empty;
                    decimal d = D.GetDecimal(row["RATE_VAT"]) == 0M ? 0M : D.GetDecimal(row["RATE_VAT"]) / 100M;
                    decimal num2 = D.GetDecimal(row["QT_PO_MM"]);
                    decimal num3 = D.GetDecimal(row["UM_EX_PO"]);
                    decimal num4 = D.GetDecimal(this.cur환정보.DecimalValue) == 0M ? 1M : D.GetDecimal(this.cur환정보.DecimalValue);
                    if (num2 == 0M)
                        return;
                    decimal num5;
                    decimal num6;
                    decimal num7;
                    if (D.GetString(row["TP_UM_TAX"]) == "001")
                    {
                        decimal num8 = Global.MainFrame.LoginInfo.CompanyLanguage == 0 ? decimal.Round(num2 * num3 * num4, MidpointRounding.AwayFromZero) : Unit.원화금액(DataDictionaryTypes.PU, num2 * num3 * num4);
                        num5 = !(this.s_vat_fictitious == "100") && !Global.MainFrame.ServerKeyCommon.Contains("INITECH") ? (Global.MainFrame.LoginInfo.CompanyLanguage == 0 ? decimal.Round(num8 / (d + 1) * d, MidpointRounding.AwayFromZero) : Unit.원화금액(DataDictionaryTypes.PU, num8 / (d + 1) * d)) : Unit.원화금액(DataDictionaryTypes.PU, num8 * d);
                        num6 = Unit.원화금액(DataDictionaryTypes.PU, num8 - num5);
                        num7 = Unit.외화금액(DataDictionaryTypes.PU, num6 / num4);
                    }
                    else
                    {
                        num7 = Unit.외화금액(DataDictionaryTypes.PU, num2 * num3);
                        num6 = Unit.원화금액(DataDictionaryTypes.PU, num2 * num3 * num4);
                        num5 = Unit.원화금액(DataDictionaryTypes.PU, num6 * d);
                        num1 = Unit.원화금액(DataDictionaryTypes.PU, num6 + num5);
                    }
                    row["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, num3);
                    row["UM_P"] = Unit.원화단가(DataDictionaryTypes.PU, num3 * num4);
                    row["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, num3 / (D.GetDecimal(row["RT_PO"]) == 0M ? 1M : D.GetDecimal(row["RT_PO"])));
                    row["UM"] = Unit.원화단가(DataDictionaryTypes.PU, num3 / (D.GetDecimal(row["RT_PO"]) == 0M ? 1M : D.GetDecimal(row["RT_PO"])) * num4);
                    row["AM_EX"] = num7;
                    row["AM"] = num6;
                    row["VAT"] = num5;
                    row["AM_TOTAL"] = (num6 + num5);
                    this.SUMFunction();
                }
                this._flexD.SumRefresh();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            this.SUMFunction();
        }

        private void SetExchageApply()
        {
            decimal num = 1M;
            if (this._header.CurrentRow == null || this.cbo환정보.SelectedValue == null || this._flexD.HasNormalRow || this._header.CurrentRow.RowState == DataRowState.Unchanged)
                return;
            if (this.dtp발주일자.Text != string.Empty)
            {
                if (MA.기준환율.Option != 0)
                    num = MA.기준환율적용(this.dtp발주일자.Text, D.GetString(this.cbo환정보.SelectedValue.ToString()));
                if (D.GetString(this.cbo환정보.SelectedValue.ToString()) == "000")
                    num = 1M;
                else if (Global.MainFrame.ServerKeyCommon == "VINA" && num == 0M)
                    num = this._biz.GetMaxRtExch(D.GetString(this.cbo환정보.SelectedValue.ToString()));
            }
          this.cur환정보.Text = num.ToString();
            this._header.CurrentRow["RT_EXCH"] = num;
        }

        private void SUMFunction()
        {
            try
            {
                if (this._flexD.HasNormalRow)
                {
                    this._header.CurrentRow["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flexD.DataTable.Compute("SUM(AM_EX)", "")));
                    this._header.CurrentRow["AM"] = this._flexD.DataTable.Compute("SUM(AM)", "");
                    this._header.CurrentRow["VAT"] = this._flexD.DataTable.Compute("SUM(VAT)", "");
                    if (!this.m_tab_poh.TabPages.Contains(this.tabPage7))
                        return;
                    this.cur공급가액.DecimalValue = D.GetDecimal(this._flexD.DataTable.Compute("SUM(AM)", ""));
                    this.cur부가세액.DecimalValue = D.GetDecimal(this._flexD.DataTable.Compute("SUM(VAT)", ""));
                    this._header.CurrentRow["AM_EX_IV"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flexD.DataTable.Compute("SUM(AM_EX)", "")));
                }
                else
                {
                    this._header.CurrentRow["AM_EX"] = 0;
                    this._header.CurrentRow["AM"] = 0;
                    this._header.CurrentRow["VAT"] = 0;
                    this.cur공급가액.DecimalValue = 0M;
                    this.cur부가세액.DecimalValue = 0M;
                    this._header.CurrentRow["AM_EX_IV"] = 0;
                }
            }
            catch
            {
                this._header.CurrentRow["AM_EX"] = 0;
                this._header.CurrentRow["AM"] = 0;
                this._header.CurrentRow["VAT"] = 0;
                this.cur공급가액.DecimalValue = 0M;
                this.cur부가세액.DecimalValue = 0M;
            }
        }

        private void ChagePoState()
        {
            try
            {
                string str1 = "Release";
                string str2 = "Start";
                string str3 = "Close";
                bool flag = false;
                for (int index = this._flexD.Rows.Fixed; index <= this._flexD.Rows.Count - 1; ++index)
                {
                    if (this._flexD.RowState() == DataRowState.Added)
                    {
                        if (this._flexD[index, "YN_ORDER"].ToString() == "Y")
                        {
                            this._flexD[index, "NM_SYSDEF"] = str1;
                            this._flexD[index, "FG_POCON"] = "001";
                        }
                        if (this._flexD[index, "YN_REQ"].ToString() == "N")
                        {
                            this._flexD[index, "NM_SYSDEF"] = str2;
                            this._flexD[index, "FG_POCON"] = "002";
                            flag = true;
                            if (this._flexD[index, "YN_AUTORCV"].ToString() == "Y")
                            {
                                this._flexD[index, "NM_SYSDEF"] = str3;
                                this._flexD[index, "FG_POCON"] = "003";
                            }
                        }
                    }
                }
                if (flag)
                {
                    this.SetHeadControlEnabled(false, 2);
                    this.btn요청적용.Enabled = false;
                    this.btn품의적용.Enabled = false;
                    this.btn수주적용.Enabled = false;
                    this.btn수주_의뢰적용.Enabled = false;
                }
                else
                    this.SetHeadControlEnabled(false, 1);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexD.HasNormalRow || this._m_tppo_change == "001" || this._flexD["FG_POST"].ToString().Trim() != "O" || BASIC.GetMAEXC_Menu(nameof(P_CZ_PU_PO_WINTEC), "PU_Z00000002") != "000" && (D.GetString(this._flexD["NO_PR"]) != string.Empty || D.GetString(this._flexD["NO_APP"]) != string.Empty) || Global.MainFrame.CurrentPageID == "P_PU_Z_JONGHAP_PO_REG2")
                    return;
                if (this._flexD.Cols[this._flexD.Col].Name == "UM_EX_PO" && Global.MainFrame.ServerKeyCommon != "LUKEN" || this._flexD.Cols[this._flexD.Col].Name == "NUM_USERDEF3_PO" && Global.MainFrame.ServerKeyCommon == "LUKEN")
                {
                    object[] objArray = new object[] { this.ctx거래처.CodeValue,
                                                       this.ctx거래처.CodeName,
                                                       this.ctx발주형태.CodeValue,
                                                       this.ctx발주형태.CodeName,
                                                      this.cbo공장.SelectedValue,
                                                       D.GetString(this._flexD["CD_ITEM"]),
                                                       D.GetString(this._flexD["NM_ITEM"]),
                                                       D.GetString(this._flexD["CD_EXCH"]),
                                                       "",
                                                       "",
                                                       "",
                                                       "",
                                                       "",
                                                       D.GetString(this._flexD["STND_ITEM"]) };
                    if (MA.ServerKey(false, new string[1] { "CNCC" }))
                    {
                        if (D.GetString(this._flexD["CD_ITEM"]) != string.Empty && D.GetString(this._flexD["TP_ITEM"]) == string.Empty)
                        {
                            DataTable dataTable = DBHelper.GetDataTable("SELECT TP_ITEM FROM MA_PITEM WHERE CD_COMPANY = '" + this.LoginInfo.CompanyCode + "' AND CD_PLANT = '" + D.GetString(this.cbo공장.SelectedValue) + "' AND CD_ITEM = '" + D.GetString(this._flexD["CD_ITEM"]) + "'");
                            this._flexD["TP_ITEM"] = dataTable.Rows.Count > 0 ? D.GetString(dataTable.Rows[0]["TP_ITEM"]) : string.Empty;
                        }
                        if (D.GetString(this._flexD["TP_ITEM"]) == "MAS")
                            return;
                    }
                    P_PU_UM_HISTORY_SUB pPuUmHistorySub = new P_PU_UM_HISTORY_SUB(objArray);
                    if (((Form)pPuUmHistorySub).ShowDialog() == DialogResult.OK)
                    {
                        decimal subUm = pPuUmHistorySub.SUB_UM;
                        this.금액계산(this._flexD.Row, subUm, D.GetDecimal(this._flexD["QT_PO_MM"]), "UM_EX_PO", subUm);
                    }
                }
                if (this._flexD.Cols[this._flexD.Col].Name == "NM_USERDEF1")
                {
                    if (MA.ServerKey(false, new string[1] { "KMI" }))
                    {
                        if (D.GetString(this._flexD["CD_ITEM_ORIGIN"]) == "")
                        {
                            this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD(this._flexD.Cols["CD_ITEM_ORIGIN"].Caption) });
                        }
                        else
                        {
                            P_PU_GIREQ_LOT_SUB pPuGireqLotSub = new P_PU_GIREQ_LOT_SUB(new object[] { D.GetString(this._flexD["CD_PLANT"]),
                                                                                                      "",
                                                                                                      D.GetString(this._flexD["CD_ITEM_ORIGIN"]),
                                                                                                      D.GetString(this._flexD["NM_ITEM_ORIGIN"]) });
                            pPuGireqLotSub.EnablePlant = false;
                            pPuGireqLotSub.EnableItem = false;
                            pPuGireqLotSub.CheckItemCount = true;
                            if (((Form)pPuGireqLotSub).ShowDialog() == DialogResult.OK)
                            {
                                DataTable dtReturn = pPuGireqLotSub._dt_return;
                                if (dtReturn != null && dtReturn.Rows.Count > 0)
                                    this._flexD["NM_USERDEF1"] = dtReturn.Rows[0]["NO_LOT"];
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

        private void _flexD_CellContentChanged(object sender, CellContentEventArgs e)
        {
            try
            {
                this._biz.SaveContent(e.ContentType, e.CommandType, D.GetString(this._flexD[e.Row, "NO_PO"]), D.GetDecimal(this._flexD[e.Row, "NO_LINE"]), e.SettingValue);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexIV_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                if (!this._flexD.HasNormalRow)
                    return;
                FlexGrid flexGrid = (FlexGrid)sender;
                string str1 = D.GetString(this._flexIV.GetData(e.Row, e.Col));
                string editData = this._flexIV.EditData;
                if (str1.ToUpper() == editData.ToUpper())
                    return;
                string str2 = D.GetString(this._header.CurrentRow["NO_PO"]);
                decimal num1 = D.GetDecimal(this._flexIV[e.Row, "NO_SEQ"]);
                D.GetString(this._header.CurrentRow["FG_TAX"]);
                decimal num2 = 0.1M;
                decimal num3 = 0M;
                decimal num4 = 0M;
                decimal 총금액 = 0M;
                if (this.sPUIV == "100")
                {
                    num3 = 0M;
                    num4 = D.GetDecimal(this._header.CurrentRow["AM_EX"]);
                    총금액 = D.GetDecimal(this._header.CurrentRow["AM"]) + D.GetDecimal(this._header.CurrentRow["VAT"]);
                }
                else if (this.sPUIV == "200")
                {
                    num3 = D.GetDecimal(this._flexD["NO_LINE"]);
                    num4 = D.GetDecimal(this._flexD["AM_EX"]);
                    총금액 = D.GetDecimal(this._flexD["AM_TOTAL"]);
                }
                bool flag = D.GetString(this._flexD["TP_UM_TAX"]) == "001";
                decimal num5 = D.GetDecimal(this._flexIV[e.Row, "AM_K"]);
                decimal num6 = D.GetDecimal(this._flexIV[e.Row, "AM_VAT"]);
                D.GetDecimal(this._flexIV[e.Row, "AM_SUM"]);
                D.GetDecimal(this._flexIV[e.Row, "AM_EX_PUL"]);
                decimal key = D.GetDecimal(this._flexIV[e.Row, "RT_IV"]);
                D.GetDecimal(this._header.CurrentRow["RT_EXCH"]);
                num2 = !(this._header.CurrentRow["FG_TRANS"].ToString() != "001") && !(this._flexD["FG_TAX"].ToString() == string.Empty) ? (this._flexD.CDecimal(this._flexD["RATE_VAT"]) == 0M ? 0M : this._flexD.CDecimal(this._flexD["RATE_VAT"]) / 100M) : this.cur부가세율.DecimalValue / 100M;
                switch (flexGrid.Cols[e.Col].Name)
                {
                    case "RT_IV":
                        if (100M < D.GetDecimal(this._flexIV.DataTable.Compute("SUM(RT_IV)", "ISNULL(NO_PO,'') = '" + str2 + "' AND NO_POLINE = " + num3 + " AND NO_SEQ <> " + num1)) + D.GetDecimal(this._flexIV[e.Row, "RT_IV"]))
                        {
                            this.ShowMessage(공통메세지._은_보다작거나같아야합니다, new string[] { "발행비율합 ", "100 " });
                            this._flexIV[e.Row, "RT_IV"] = str1;
                            e.Cancel = true;
                            break;
                        }
                        this.기성매입금액계산(e.Row, num4, 총금액, num4, "RT_IV", D.GetDecimal(editData));
                        break;
                    case "AM":
                        decimal num8 = D.GetDecimal(this._flexIV.DataTable.Compute("SUM(AM)", "ISNULL(NO_PO,'') = '" + str2 + "' AND NO_POLINE = " + num3 + " AND NO_SEQ <> " + num1)) + D.GetDecimal(this._flexIV[e.Row, "AM"]);
                        if (num4 < num8)
                        {
                            this.ShowMessage(공통메세지._은_보다작거나같아야합니다, new string[] { "금액합 ", "발주금액 " });
                            this._flexIV[e.Row, "AM"] = str1;
                            e.Cancel = true;
                            break;
                        }
                        if (!flag)
                        {
                            this.기성매입금액계산(e.Row, num4, 총금액, num4, "AM", D.GetDecimal(editData));
                            break;
                        }
                        break;
                    case "AM_SUM":
                        decimal num10 = D.GetDecimal(this._flexIV.DataTable.Compute("SUM(AM_SUM)", "ISNULL(NO_PO,'') = '" + str2 + "' AND NO_POLINE = " + num3 + " AND NO_SEQ <> " + num1)) + D.GetDecimal(this._flexIV[e.Row, "AM_SUM"]);
                        if (총금액 < num10)
                        {
                            this.ShowMessage(공통메세지._은_보다작거나같아야합니다, new string[] { "금액합 ", "발주금액 " });
                            this._flexIV[e.Row, "AM"] = str1;
                            e.Cancel = true;
                            break;
                        }
                        this.기성매입금액계산(e.Row, key, 총금액, num4, "AM_SUM", D.GetDecimal(editData));
                        break;
                    case "AM_K":
                        this._flexIV["AM_SUM"] = (D.GetDecimal(editData) + num6);
                        break;
                    case "AM_VAT":
                        decimal num12 = D.GetDecimal(editData);
                        this._flexIV["AM_SUM"] = (num5 + num12);
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexIV_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (!this.차수여부 || !this.전자결재여부)
                    e.Cancel = true;
                if (D.GetString(this._flexD["TP_UM_TAX"]) == "001" || !(this._flexIV.Cols[e.Col].Name == "AM_SUM"))
                    return;
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexD_AfterEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (!(this.sPUSU == "100") || !(D.GetString(this._flexD["CD_ITEM"]) == ""))
                    return;
                foreach (DataRow dataRow in this._flexDD.DataTable.Select("NO_POLINE = " + this._flexD.CDecimal(this._flexD["NO_LINE"])))
                    dataRow.Delete();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void MouseClick_App(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexD.HasNormalRow || !this.차수여부 || !this.전자결재여부)
                    return;
                ToolStripItem toolStripItem = (ToolStripItem)sender;
                string columnName1 = string.Empty;
                string columnName2 = string.Empty;
                if (toolStripItem.Text == this.DD("납품처"))
                {
                    if (D.GetString(this._flexD["GI_PARTNER"]) == "" || D.GetString(this._flexD["LN_PARTNER"]) == "")
                        return;
                    columnName1 = "GI_PARTNER";
                    columnName2 = "LN_PARTNER";
                }
                if (toolStripItem.Text == this.DD("CC코드"))
                {
                    if (D.GetString(this._flexD["CD_CC"]) == "" || D.GetString(this._flexD["NM_CC"]) == "")
                        return;
                    columnName1 = "CD_CC";
                    columnName2 = "NM_CC";
                }
                if (columnName1 == string.Empty && columnName2 == string.Empty)
                    return;
                DataRow[] dataRowArray = this._flexD.DataTable.Select("", "", DataViewRowState.CurrentRows);
                if (dataRowArray != null && dataRowArray.Length > 0)
                {
                    foreach (DataRow dataRow in dataRowArray)
                    {
                        if (!(D.GetString(dataRow["FG_POST"]) != "O") || dataRow.RowState == DataRowState.Added)
                        {
                            dataRow[columnName1] = this._flexD[columnName1];
                            if (columnName2 != string.Empty)
                                dataRow[columnName2] = this._flexD[columnName2];
                        }
                    }
                    this.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { this.DD("일괄적용") });
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void PopupEventHandler(object sender, EventArgs e)
        {
            try
            {
                FlexGrid flexGrid = sender as FlexGrid;
                if (!flexGrid.HasNormalRow || flexGrid.Rows[flexGrid.Row].IsNode || flexGrid.Row < (flexGrid.Rows).Fixed)
                    return;
                flexGrid.GridContextMenuStrip.Items.Find(this.DD("납품처"), true)[0].Enabled = !(flexGrid["GI_PARTNER"].ToString() == "");
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void cbo_NM_EXCH_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (D.GetString(this.cbo환정보.SelectedValue) == "000")
                {
                    this.cur환정보.DecimalValue = 1M;
                    this._header.CurrentRow["RT_EXCH"] = 1;
                    this.cur환정보.Enabled = false;
                }
                else
                {
                    this.SetExchageApply();
                    if (MA.기준환율.Option == MA.기준환율옵션.적용_수정불가)
                        this.cur환정보.Enabled = false;
                    else
                        this.cur환정보.Enabled = true;
                    if (this._m_Company_only == "001")
                    {
                        this.cur환정보.DecimalValue = D.GetDecimal(Settings1.Default.RT_EXCH);
                        this._header.CurrentRow["RT_EXCH"] = D.GetDecimal(Settings1.Default.RT_EXCH);
                    }
                }
                if (!(Global.MainFrame.ServerKeyCommon.ToUpper() != "KORAVL"))
                    return;
                this.SetExchageMoney();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void tb_NM_EXCH_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!(Global.MainFrame.ServerKeyCommon.ToUpper() != "KORAVL"))
                    return;
                this.SetExchageMoney();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void tb_NM_EXCH_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!(Global.MainFrame.ServerKeyCommon.ToUpper() != "KORAVL"))
                    return;
                this.SetExchageMoney();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void tb_DT_PO_DateChanged(object sender, EventArgs e)
        {
            if (this._header.CurrentRow == null)
                return;
            this.SetExchageApply();
            if (this.m_tab_poh.TabPages.Contains(this.tabPage7))
            {
                this.dtp만기일자.Text = this.dtp발주일자.Text;
                this.dtp지급예정일자.Text = this.dtp발주일자.Text;
                this.dtp매입일자.Text = this.dtp발주일자.Text;
                this._header.CurrentRow["DT_PROCESS_IV"] = this.dtp발주일자.Text;
                this._header.CurrentRow["DT_PAY_PRE_IV"] = this.dtp발주일자.Text;
                this._header.CurrentRow["DT_DUE_IV"] = this.dtp발주일자.Text;
            }
        }

        private void cbo_FG_TAX_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                decimal num1 = 1.0M;
                string ps_taxp = "";
                if (this.cbo과세구분.SelectedValue != null)
                    ps_taxp = this.cbo과세구분.SelectedValue.ToString();
                if (this.s_vat_fictitious == "100")
                {
                    if (this.의제매입여부(ps_taxp))
                    {
                        this.cbo부가세여부.SelectedValue = "001";
                        this._header.CurrentRow["TP_UM_TAX"] = "001";
                    }
                    else
                    {
                        this.cbo부가세여부.SelectedValue = "002";
                        this._header.CurrentRow["TP_UM_TAX"] = "002";
                    }
                }
                this.부가세율(ps_taxp);
                if (this._flexD.HasNormalRow)
                {
                    num1 = this._flexD.CDecimal(this.cur환정보.ClipText);
                    decimal num2 = this._flexD.CDecimal(this.cur부가세율.ClipText) / 100M;
                    for (int index = 0; index < this._flexD.DataTable.Rows.Count; ++index)
                    {
                        try
                        {
                            this._flexD.DataTable.Rows[index]["FG_TAX"] = ps_taxp;
                            this._flexD.DataTable.Rows[index]["RATE_VAT"] = this._flexD.CDecimal(this.cur부가세율.ClipText);
                            this._flexD.DataTable.Rows[index]["TP_UM_TAX"] = this._header.CurrentRow["TP_UM_TAX"];
                            if (this.의제매입여부(ps_taxp) && this.s_vat_fictitious == "100")
                            {
                                decimal num3 = D.GetDecimal(this._flexD.DataTable.Rows[index]["QT_PO_MM"]);
                                decimal num4 = D.GetDecimal(this._flexD.DataTable.Rows[index]["UM_EX_PO"]);
                                decimal num5 = D.GetDecimal(this._header.CurrentRow["RT_EXCH"]) == 0M ? 1M : D.GetDecimal(this._header.CurrentRow["RT_EXCH"]);
                                decimal DecimalValue = this.cur부가세율.DecimalValue;
                                bool flag = !(D.GetString(this._flexD.DataTable.Rows[index]["TP_UM_TAX"]) == "002");
                                decimal num6 = 0M;
                                decimal num7 = 0M;
                                decimal num8 = 0M;
                                Calc.GetAmt(num3, num4, num5, ps_taxp, DecimalValue, 모듈.PUR, flag, out num6, out num7, out num8);
                                this._flexD.DataTable.Rows[index]["VAT"] = num8;
                                this._flexD.DataTable.Rows[index]["AM_EX"] = num6;
                                this._flexD.DataTable.Rows[index]["AM"] = num7;
                                this._flexD.DataTable.Rows[index]["AM_TOTAL"] = Calc.합계금액(num7, num8);
                            }
                            else
                            {
                                decimal num9 = this._flexD.CDecimal(this._flexD.DataTable.Rows[index]["AM"]);
                                this._flexD.DataTable.Rows[index]["VAT"] = Unit.원화금액(DataDictionaryTypes.PU, num9 * num2);
                            }
                        }
                        catch (Exception ex)
                        {
                            this.MsgEnd(ex);
                        }
                    }
                    this._flexD.SumRefresh();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            this.SUMFunction();
        }

        private void Control_KeyEvent(object sender, KeyEventArgs e)
        {
            switch (((Control)sender).Name)
            {
                case "txt비고":
                case "txt비고2":
                case "cbo운송방법":
                case "cbo지불조건":
                case "txt지불조건":
                case "cbo가격조건":
                case "txt도착지":
                case "txt선적지":
                case "txt발주텍스트비고":
                case "cbo선적조건":
                case "cbo운임조건":
                case "txt발주텍스트비고2":
                case "cbo지급기준":
                case "cur지불조건":
                case "cbo원산지":
                    this.비고수정여부 = true;
                    this.ToolBarSaveButtonEnabled = true;
                    if (!(((Control)sender).Name == "txt비고") && !(((Control)sender).Name == "txt비고2"))
                        break;
                    this.SetHeadControlEnabled(false, 2);
                    break;
                default:
                    if (e.KeyData != Keys.Return)
                        break;
                    SendKeys.SendWait("{TAB}");
                    break;
            }
        }

        private void cbo_CD_PLANT_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                switch (((Control)sender).Name)
                {
                    case "cbo공장":
                        this.ctx창고.Clear();
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void GET_SU_BOM()
        {
            if (!this._flexD.HasNormalRow || !this._flexDD.Enabled)
                return;
            DataTable dataTable = new DataTable();
            string str = "NO_POLINE = " + this._flexD.CDecimal(this._flexD["NO_LINE"]);
            DataTable suBom = this._biz.GET_SU_BOM(this._header.CurrentRow["CD_PLANT"].ToString(), this._header.CurrentRow["CD_PARTNER"].ToString(), this._flexD["CD_ITEM"].ToString());
            decimal num = 0M;
            foreach (DataRow row in suBom.Rows)
            {
                row["QT_PO"] = this._flexD.CDecimal(this._flexD["QT_PO"]);
                row["NO_PO"] = this._flexD["NO_PO"].ToString();
                row["NO_POLINE"] = this._flexD.CDecimal(this._flexD["NO_LINE"]);
                row["NO_LINE"] = ++num;
            }
            for (int index = (this._flexDD.Rows).Count - (this._flexDD.Rows).Fixed; index >= (this._flexDD.Rows).Fixed; --index)
                this._flexDD.Rows.Remove(index);
            this._flexDD.BindingAdd(suBom, str, false);
            this._flexD.DetailQueryNeed = false;
            this.사급자재변경구문(this._flexD.CDecimal(this._flexD["QT_PO"]));
        }

        private void 사급자재변경구문(decimal dPo)
        {
            for (int index = (this._flexDD.Rows).Fixed; index < (this._flexDD.Rows).Count; ++index)
            {
                this._flexDD[index, "QT_NEED"] = (dPo != 0M ? Math.Round(dPo * this._flexDD.CDecimal(this._flexDD[index, "QT_NEED_UNIT"]), 4, MidpointRounding.AwayFromZero) : this._flexDD.CDecimal(this._flexDD[index, "QT_NEED_UNIT"]));
                this._flexDD[index, "QT_LOSS"] = (this._flexDD.CDecimal(this._flexDD[index, "QT_NEED"]) - this._flexDD.CDecimal(this._flexDD[index, "QT_REQ"]));
            }
        }

        private void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToolBarDeleteButtonEnabled = true;
                if (!base.IsChanged())
                    return;
                this.ToolBarSaveButtonEnabled = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool IsChanged() => base.IsChanged() || this.헤더변경여부;

        private void 기초값설정()
        {
            string arg_cd_purgrp = this._header.CurrentRow["CD_PURGRP"].ToString();
            string CD_TPPO = this._header.CurrentRow["CD_TPPO"].ToString();
            this.cbo지급조건.SelectedValue = this._header.CurrentRow["FG_PAYMENT"].ToString();
            DataSet tppoPurgrp = this._biz.Get_TPPO_PURGRP(new object[] { this.LoginInfo.CompanyCode,
                                                                          arg_cd_purgrp,
                                                                          CD_TPPO,
                                                                          Global.SystemLanguage.MultiLanguageLpoint.ToString() });
            if (tppoPurgrp.Tables[0].Rows.Count > 0)
            {
                DataRow row = tppoPurgrp.Tables[0].Rows[0];
                this._header.CurrentRow["CD_PURGRP"] = arg_cd_purgrp;
                this.ctx구매그룹.SetCode(arg_cd_purgrp, row["NM_PURGRP"].ToString());
                this._header.CurrentRow["PURGRP_NO_TEL"] = row["NO_TEL"].ToString();
                this._header.CurrentRow["PURGRP_NO_FAX"] = row["NO_FAX"].ToString();
                this._header.CurrentRow["PURGRP_E_MAIL"] = row["E_MAIL"].ToString();
                this._header.CurrentRow["PO_PRICE"] = row["PO_PRICE"].ToString();
                this.SetCC(0, arg_cd_purgrp);
            }
            if (tppoPurgrp.Tables[1].Rows.Count <= 0)
                return;
            DataTable ynSu = this._biz.GetYN_SU(D.GetString(this._header.CurrentRow["CD_TPPO"]));
            if (D.GetString(ynSu.Rows[0]["YN_SU"]) == "Y")
            {
                if (Global.MainFrame.ServerKeyCommon.Contains("DONGWOON"))
                    this.btnCompanyUse1.Enabled = true;
                this._flexDD.Enabled = true;
            }
            else
            {
                if (Global.MainFrame.ServerKeyCommon.Contains("DONGWOON"))
                    this.btnCompanyUse1.Enabled = false;
                this._flexDD.Enabled = false;
            }
            DataRow row1 = tppoPurgrp.Tables[1].Rows[0];
            if (this.sPUSU == "100" && D.GetString(ynSu.Rows[0]["YN_SU"]) == "Y" && D.GetString(row1["FG_TRANS"]) != "004" && D.GetString(row1["FG_TRANS"]) != "005")
            {
                this.ShowMessage(this.DD("국내인 외주발주가 있습니다. 발주유형을 확인하세요."));
                this._header.CurrentRow["CD_TPPO"] = "";
            }
            else
            {
                this._header.CurrentRow["CD_TPPO"] = CD_TPPO;
                this.ctx발주형태.SetCode(CD_TPPO, row1["NM_TPPO"].ToString());
                this._header.CurrentRow["FG_TRANS"] = row1["FG_TRANS"].ToString();
                this._header.CurrentRow["FG_TPRCV"] = row1["FG_TPRCV"].ToString();
                this._header.CurrentRow["FG_TPPURCHASE"] = row1["FG_TPPURCHASE"].ToString();
                this._header.CurrentRow["YN_AUTORCV"] = row1["YN_AUTORCV"].ToString();
                this._header.CurrentRow["YN_RCV"] = row1["YN_RCV"].ToString();
                this._header.CurrentRow["YN_RETURN"] = row1["YN_RETURN"].ToString();
                this._header.CurrentRow["YN_SUBCON"] = row1["YN_SUBCON"].ToString();
                this._header.CurrentRow["YN_IMPORT"] = row1["YN_IMPORT"].ToString();
                this._header.CurrentRow["YN_ORDER"] = row1["YN_ORDER"].ToString();
                this._header.CurrentRow["YN_REQ"] = row1["YN_REQ"].ToString();
                this._header.CurrentRow["YN_AM"] = row1["YN_AM"].ToString();
                this._header.CurrentRow["NM_TRANS"] = row1["NM_TRANS"].ToString();
                this._header.CurrentRow["FG_TAX"] = row1["FG_TAX"].ToString();
                this._header.CurrentRow["TP_GR"] = row1["TP_GR"].ToString();
                this._header.CurrentRow["CD_CC_TPPO"] = row1["CD_CC"].ToString();
                this._header.CurrentRow["NM_CC_TPPO"] = this._biz.GetCCCodeSearch(row1["CD_CC"].ToString());
                this.거래구분(row1["FG_TRANS"].ToString(), D.GetString(row1["FG_TAX"]));
                this.Setting_pu_poh_sub();
                if (MA.ServerKey(false, new string[1] { "HISF" }))
                    this.setCD_EXCH_HISF(CD_TPPO);
                this.curDe.DecimalValue = 0M;
            }
        }

        private void 거래구분(string str거래구분, string strTax)
        {
            if (str거래구분 == "001")
            {
                this.cbo환정보.SelectedValue = "000";
                this._header.CurrentRow["CD_EXCH"] = "000";
                this.cbo_NM_EXCH_SelectionChangeCommitted(null, null);
                this.cbo과세구분.Enabled = true;
                this.cbo과세구분.SelectedValue = this._header.CurrentRow["FG_TAX"];
                this.cbo_FG_TAX_SelectionChangeCommitted(null, null);
            }
            else if (str거래구분 == "002" || str거래구분 == "003")
            {
                this.cbo환정보.SelectedValue = "000";
                this._header.CurrentRow["CD_EXCH"] = "000";
                this.cbo_NM_EXCH_SelectionChangeCommitted(null, null);
                if (Global.MainFrame.ServerKeyCommon == "DYPNF" || Global.MainFrame.LoginInfo.CompanyLanguage != null || Global.MainFrame.ServerKeyCommon == "NSM")
                {
                    this._header.CurrentRow["FG_TAX"] = strTax;
                    this.cbo과세구분.Enabled = false;
                    this.cbo과세구분.SelectedValue = strTax;
                }
                else
                {
                    this._header.CurrentRow["FG_TAX"] = "23";
                    this.cbo과세구분.Enabled = false;
                    this.cbo과세구분.SelectedValue = "23";
                }
                this.cbo_FG_TAX_SelectionChangeCommitted(null, null);
            }
            else
            {
                this.cbo환정보.SelectedValue = Settings1.Default.CD_EXCH;
                this._header.CurrentRow["CD_EXCH"] = Settings1.Default.CD_EXCH;
                this.cbo_NM_EXCH_SelectionChangeCommitted(null, null);
                this._header.CurrentRow["FG_TAX"] = "";
                this.cbo과세구분.SelectedValue = "21";
                this.cbo과세구분.SelectedValue = "";
                this.cbo과세구분.Enabled = false;
                this.cbo_FG_TAX_SelectionChangeCommitted(null, null);
            }
        }

        private void 부가세율(string ps_taxp)
        {
            if (this._flexD.HasNormalRow)
                this.cur부가세율.Enabled = false;
            else
                this.cur부가세율.Enabled = true;
            if (ps_taxp == "")
            {
                this.cur부가세율.Enabled = false;
                this.cur부가세율.DecimalValue = 0M;
                this._header.CurrentRow["VAT_RATE"] = 0;
            }
            else
            {
                if (this.의제매입여부(ps_taxp) && this.s_vat_fictitious == "100")
                {
                    this._header.CurrentRow["TP_UM_TAX"] = "001";
                    this.cbo부가세여부.SelectedValue = this._header.CurrentRow["TP_UM_TAX"];
                    this.cbo부가세여부.Enabled = false;
                }
                else if (!this._flexD.HasNormalRow)
                    this.cbo부가세여부.Enabled = true;
                if (this.의제매입여부(ps_taxp) && this.s_vat_fictitious == "000")
                {
                    this.cur부가세율.Enabled = false;
                    this.cur부가세율.DecimalValue = 0M;
                    this._header.CurrentRow["VAT_RATE"] = 0;
                }
                else
                {
                    DataRow[] dataRowArray = ((DataTable)this.cbo과세구분.DataSource).Select("CODE = '" + ps_taxp + "'");
                    if (dataRowArray != null && dataRowArray.Length > 0)
                    {
                        decimal num = this._flexD.CDecimal(dataRowArray[0]["CD_FLAG1"]);
                        this.cur부가세율.DecimalValue = num;
                        this._header.CurrentRow["VAT_RATE"] = num;
                    }
                    else
                    {
                        this.cur부가세율.Enabled = true;
                        this.cur부가세율.DecimalValue = 0M;
                        this._header.CurrentRow["VAT_RATE"] = 0;
                    }
                }
            }
        }

        private void 품목정보구하기(object[] m_obj, string ls_app, int arg_row) => this.품목정보구하기(m_obj, ls_app, arg_row, null);

        private void 품목정보구하기(object[] m_obj, string ls_app, int arg_row, object[] objSerial)
        {
            int num1 = this._flexD.Rows.Count - 1;
            decimal num2 = 0.0M;
            if (ls_app == "GRID")
                num1 = arg_row;
            if (ls_app == "TSUBAKI")
            {
                num1 = arg_row;
                ls_app = "SO";
            }
            this._flexD[num1, "QT_INVC"] = 0;
            if (Global.MainFrame.ServerKeyCommon.Contains("OGELEC"))
                m_obj[3] = "";
            ArrayList arrayList = new ArrayList();
            arrayList.AddRange(m_obj);
            arrayList.Add(Global.SystemLanguage.MultiLanguageLpoint.ToString());
            DataSet dataSet = this._biz.ItemInfo_Search((object[])arrayList.ToArray(typeof(string)));
            DataTable dataTable = this._biz.item_pinvn(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                      this.dtp발주일자.Text.Substring(0, 4),
                                                                      D.GetString(this.cbo공장.SelectedValue),
                                                                      D.GetString(m_obj[0]),
                                                                      "" });
            this._flexD[num1, "FG_TAX"] = this._header.CurrentRow["FG_TAX"];
            this._flexD[num1, "RATE_VAT"] = this.cur부가세율.DecimalValue;
            decimal num3 = this._flexD.CDecimal(this.cur부가세율.DecimalValue);
            if (dataSet != null && dataSet.Tables.Count > 3)
            {
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    if (this.m_sEnv_FG_TAX == "100" && dataSet.Tables[0].Rows[0]["FG_TAX_PU"].ToString() != string.Empty)
                    {
                        this._flexD[num1, "FG_TAX"] = dataSet.Tables[0].Rows[0]["FG_TAX_PU"];
                        this._flexD[num1, "RATE_VAT"] = dataSet.Tables[0].Rows[0]["RATE_VAT"];
                        num3 = Convert.ToDecimal(dataSet.Tables[0].Rows[0]["RATE_VAT"]);
                        if (this.의제매입여부(D.GetString(this._flexD[num1, "FG_TAX"])) && this.s_vat_fictitious == "100")
                            this._flexD[num1, "TP_UM_TAX"] = "001";
                    }
                    if (BASIC.GetMAEXC("발주등록(공장)-프로젝트별_의제매입세_구분") == "100" && D.GetString(dataSet.Tables[0].Rows[0]["CD_USERDEF14"]) == "001")
                    {
                        string str = this._biz.pjt_item_josun(D.GetString(this._flexD[num1, "CD_PJT"]));
                        if (str != "")
                        {
                            this._flexD[num1, "FG_TAX"] = str;
                            this._flexD[num1, "RATE_VAT"] = 0;
                            num3 = 0M;
                        }
                    }
                    num2 = num3 == 0M ? 0M : num3 / 100M;
                    if (ls_app == "GRID")
                    {
                        num1 = arg_row;
                        this._flexD[num1, "RT_PO"] = this._flexD.CDecimal(dataSet.Tables[0].Rows[0]["UNIT_PO_FACT"]);
                        if (Global.MainFrame.ServerKeyCommon == "AMICOGEN" || Global.MainFrame.ServerKeyCommon == "SQL_")
                        {
                            this._flexD["CD_PJT"] = dataSet.Tables[0].Rows[0]["CD_USERDEF15"];
                            this._flexD["NM_PJT"] = dataSet.Tables[0].Rows[0]["NM_PROJECT"];
                        }
                    }
                    if (this._m_Company_only == "001")
                        this.AsahiKasei_Only_Item(num1, dataSet.Tables[0]);
                    this._flexD[num1, "GRP_ITEM"] = dataSet.Tables[0].Rows[0]["GRP_ITEM"];
                    this._flexD[num1, "NM_ITEMGRP"] = dataSet.Tables[0].Rows[0]["NM_ITEMGRP"];
                    this._flexD[num1, "CD_ITEMGRP"] = dataSet.Tables[0].Rows[0]["CD_ITEMGRP"];
                    this._flexD[num1, "STND_DETAIL_ITEM"] = dataSet.Tables[0].Rows[0]["STND_DETAIL_ITEM"];
                    this._flexD[num1, "NO_MODEL"] = dataSet.Tables[0].Rows[0]["NO_MODEL"];
                    this._flexD[num1, "CD_USERDEF14"] = dataSet.Tables[0].Rows[0]["CD_USERDEF14"];
                    this._flexD[num1, "NM_MAKER"] = dataSet.Tables[0].Rows[0]["NM_MAKER"];
                    this._flexD[num1, "MAT_ITEM"] = dataSet.Tables[0].Rows[0]["MAT_ITEM"];
                    this._flexD[num1, "NO_DESIGN"] = dataSet.Tables[0].Rows[0]["NO_DESIGN"];
                    this._flexD[num1, "EN_ITEM"] = dataSet.Tables[0].Rows[0]["EN_ITEM"];
                    this._flexD[num1, "GRP_MFG"] = dataSet.Tables[0].Rows[0]["GRP_MFG"];
                    this._flexD[num1, "NM_GRPMFG"] = dataSet.Tables[0].Rows[0]["NM_GRP_MFG"];
                    this._flexD[num1, "FG_SERNO"] = dataSet.Tables[0].Rows[0]["FG_SERNO"];
                    this._flexD[num1, "TP_PART"] = dataSet.Tables[0].Rows[0]["TP_PART"];
                    this._flexD[num1, "CLS_L"] = dataSet.Tables[0].Rows[0]["CLS_L"];
                    this._flexD[num1, "CLS_M"] = dataSet.Tables[0].Rows[0]["CLS_M"];
                    this._flexD[num1, "CLS_S"] = dataSet.Tables[0].Rows[0]["CLS_S"];
                    this._flexD[num1, "NM_CLS_L"] = dataSet.Tables[0].Rows[0]["NM_CLS_L"];
                    this._flexD[num1, "NM_CLS_M"] = dataSet.Tables[0].Rows[0]["NM_CLS_M"];
                    this._flexD[num1, "NM_CLS_S"] = dataSet.Tables[0].Rows[0]["NM_CLS_S"];
                    if (this._flexD.DataTable.Columns.Contains("FG_IQCL"))
                        this._flexD[num1, "FG_IQCL"] = dataSet.Tables[0].Rows[0]["FG_IQCL"];
                    if (this._flexD.DataTable.Columns.Contains("TP_ITEM"))
                        this._flexD[num1, "TP_ITEM"] = dataSet.Tables[0].Rows[0]["TP_ITEM"];
                    if (Global.MainFrame.ServerKeyCommon == "GIT")
                        this._flexD[num1, "FG_PACKING"] = !(D.GetString(this._flexD[num1, "FG_SERNO"]) == "003") ? "N" : "I";
                    if ((ls_app == "요청" && D.GetString(m_obj[8]) == "N" || ls_app == "GRID" || ls_app == "EXCEL" || ls_app == "H41" || ls_app == "BOM" || ls_app == "USE") && dataSet.Tables[1].Rows.Count > 0)
                    {
                        if (ls_app != "H41")
                        {
                            this._flexD[num1, "UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(dataSet.Tables[1].Rows[0]["UM_ITEM"]));
                            if (this._flexD.CDecimal(this._flexD[num1, "RT_PO"]) == 0M)
                            {
                                this._flexD[num1, "UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(dataSet.Tables[1].Rows[0]["UM_ITEM"]));
                                this._flexD[num1, "QT_PO"] = Unit.수량(DataDictionaryTypes.PU, this._flexD.CDecimal(this._flexD[num1, "QT_PO_MM"]));
                            }
                            else
                            {
                                this._flexD[num1, "UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(dataSet.Tables[1].Rows[0]["UM_ITEM"]) / this._flexD.CDecimal(this._flexD[num1, "RT_PO"]));
                                //this._flexD[num1, "QT_PO"] = Unit.수량(DataDictionaryTypes.PU, this._flexD.CDecimal(this._flexD[num1, "QT_PO_MM"]) * this._flexD.CDecimal(this._flexD[num1, "RT_PO"]));
                            }
                        }
                        this._flexD[num1, "UM_P"] = Unit.원화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(this._flexD[num1, "UM_EX_PO"]) * this.cur환정보.DecimalValue);
                        this._flexD[num1, "UM"] = Unit.원화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(this._flexD[num1, "UM_EX"]) * this.cur환정보.DecimalValue);
                        this._flexD[num1, "AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, this._flexD.CDecimal(this._flexD[num1, "UM_EX_PO"]) * this._flexD.CDecimal(this._flexD[num1, "QT_PO_MM"]));
                        if (Global.MainFrame.ServerKeyCommon == "LUKEN")
                            this._flexD["NUM_USERDEF3_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(this._flexD[num1, "UM_EX_PO"]));
                        if (this._m_Company_only == "001" && D.GetDecimal(this._flexD[num1, "QT_AREA"]) != 0M)
                            this._flexD[num1, "UM_EX_AR"] = (D.GetDecimal(this._flexD[num1, "UM_EX_PO"]) / D.GetDecimal(this._flexD[num1, "QT_AREA"]));
                        if (Global.MainFrame.ServerKeyCommon.Contains("WINFOOD"))
                            this._flexD[num1, "DC1"] = dataSet.Tables[1].Rows[0]["NM_FG_UM"];
                        else if (Global.MainFrame.ServerKeyCommon.Contains("SHTP"))
                            this._flexD[num1, "NUM_USERDEF1"] = Unit.외화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(dataSet.Tables[1].Rows[0]["UM_ITEM"]));
                        else if (MA.ServerKey(false, new string[] { "WINIX" }))
                        {
                            if (dataSet.Tables[1].Columns.Contains("CD_USERDEF1"))
                                this._flexD[num1, "CD_USERDEF1"] = dataSet.Tables[1].Rows[0]["CD_USERDEF1"];
                            DataTable dtWinix = this._biz.getDT_WINIX(new object[] { MA.Login.회사코드,
                                                                                     D.GetString(this.cbo공장.SelectedValue),
                                                                                     D.GetString(this._flexD[num1, "CD_ITEM"]),
                                                                                     this.dtp발주일자.Text });
                            if (dtWinix != null && dtWinix.Rows.Count > 0)
                                this._flexD[num1, "DATE_USERDEF1"] = D.GetString(dtWinix.Rows[0]["DT_LIMIT"]);
                        }
                    }
                    else if (ls_app == "SO")
                    {
                        if (this._flexD.CDecimal(this._flexD[num1, "RT_PO"]) == 0M)
                        {
                            this._flexD[num1, "UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(this._flexD[num1, "UM_EX_PO"]));
                            this._flexD[num1, "QT_PO"] = Unit.수량(DataDictionaryTypes.PU, this._flexD.CDecimal(this._flexD[num1, "QT_PO_MM"]));
                        }
                        else
                        {
                            this._flexD[num1, "UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(this._flexD[num1, "UM_EX_PO"]) / this._flexD.CDecimal(this._flexD[num1, "RT_PO"]));
                            this._flexD[num1, "QT_PO"] = Unit.수량(DataDictionaryTypes.PU, this._flexD.CDecimal(this._flexD[num1, "QT_PO_MM"]) * this._flexD.CDecimal(this._flexD[num1, "RT_PO"]));
                        }
                        this._flexD[num1, "UM_P"] = Unit.원화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(this._flexD[num1, "UM_EX_PO"]) * this.cur환정보.DecimalValue);
                        this._flexD[num1, "UM"] = Unit.원화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(this._flexD[num1, "UM_EX"]) * this.cur환정보.DecimalValue);
                    }
                    this._flexD[num1, "WEIGHT"] = dataSet.Tables[0].Rows[0]["WEIGHT"];
                    if (!this.bStandard)
                        this._flexD[num1, "QT_WEIGHT"] = (this._flexD.CDecimal(this._flexD[num1, "QT_PO_MM"]) * this._flexD.CDecimal(this._flexD[num1, "WEIGHT"]));
                    else if (this.MainFrameInterface.ServerKeyCommon == "SINJINSM" && D.GetDecimal(this._flexD[num1, "UM_WEIGHT"]) > 0M)
                        this.금액계산(num1, 0M, D.GetDecimal(this._flexD[num1, "QT_PO_MM"]), "", 0M);
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        this._flexD[num1, "QT_INVC"] = dataTable.Rows[0]["QT_INVC"];
                        this._flexD[num1, "QT_ATPC"] = dataTable.Rows[0]["QT_ATPC"];
                    }
                    if (this.MainFrameInterface.ServerKeyCommon == "WOORIERP")
                    {
                        if (ls_app != "요청" && ls_app != "품의")
                        {
                            this._flexD[num1, "NM_USERDEF1"] = dataSet.Tables[0].Rows[0]["NM_USERDEF1"];
                            this._flexD[num1, "NM_USERDEF2"] = dataSet.Tables[0].Rows[0]["NM_USERDEF2"];
                        }
                    }
                    else if (this.MainFrameInterface.ServerKeyCommon == "CARGOTEC")
                        this._flexD[num1, "NM_USERDEF1"] = dataSet.Tables[0].Rows[0]["NM_USERDEF1"];
                    else if (Global.MainFrame.ServerKeyCommon.Contains("JSREMK"))
                        this._flexD[num1, "TXT_USERDEF1"] = dataSet.Tables[0].Rows[0]["NM_USERDEF1"];
                    else if (MA.ServerKey(false, new string[] { "NANDA" }) && (ls_app == "GRID" || ls_app == "요청"))
                        this.CalcItemLT(num1, D.GetDecimal(dataSet.Tables[0].Rows[0]["LT_ITEM"]));
                    else if (MA.ServerKey(false, new string[] { "KCF", "VINA" }))
                    {
                        this._flexD[num1, "GI_PARTNER"] = D.GetString(this.ctx거래처.CodeValue);
                        this._flexD[num1, "LN_PARTNER"] = D.GetString(this.ctx거래처.CodeName);
                        if (ls_app == "GRID" && Global.MainFrame.ServerKeyCommon == "KCF")
                            this.CalcItemLT(num1, D.GetDecimal(dataSet.Tables[0].Rows[0]["LT_ITEM"]));
                    }
                    else if (MA.ServerKey(false, new string[] { "ONEGENE" }) && ls_app == "GRID")
                        this._flexD[num1, "NUM_USERDEF1"] = dataSet.Tables[0].Rows[0]["NUM_USERDEF1"];
                    else if (this.MainFrameInterface.ServerKeyCommon == "HOTEL")
                    {
                        if (dataSet.Tables[0].Rows[0]["CD_USERDEF2"].ToString() != "")
                            this._flexD[num1, "FG_PURCHASE"] = dataSet.Tables[0].Rows[0]["CD_USERDEF2"];
                    }
                    else if (MA.ServerKey(false, new string[] { "KMI" }))
                    {
                        this.SetQT_KMI(new DataTable(), num1);
                        this._flexD[num1, "NM_USERDEF2"] = dataSet.Tables[0].Rows[0]["NM_USERDEF2"];
                    }
                    else if (MA.ServerKey(false, new string[]{ "FDAMK" }))
                    {
                        this._flexD[num1, "NUM_USERDEF3_PO"] = dataSet.Tables[0].Rows[0]["NUM_USERDEF3"];
                        this.setCalBoxEa_FDAMK("QT_PO_MM", num1);
                    }
                }
                else
                {
                    this.ShowMessage("품목정보를 확인하십시오(사용유무 확인 필수)");
                    return;
                }
            }
            this.SetQtValue(num1);
        }

        private void SetQtValue(int li_index)
        {
            if (li_index < 0)
                return;
            this.cur현재고량.DecimalValue = this._flexD.CDecimal(this._flexD[li_index, "QT_INVC"]);
        }

        private void GetQtValue(int li_index)
        {
            if (li_index < 0)
                return;
            DataTable dataTable = null;
            if (D.GetDecimal(this._flexD[li_index, "QT_INVC"]) == 0M)
                dataTable = this._biz.item_pinvn(new object[] { Global.MainFrame.LoginInfo.CompanyCode, 
                                                                this.dtp발주일자.Text.Substring(0, 4),
                                                                D.GetString(this.cbo공장.SelectedValue),
                                                                D.GetString(this._flexD[li_index, "CD_ITEM"]),
                                                                D.GetString(this._flexD[li_index, "CD_SL"]) });
            this.SetQtValue(li_index);
        }

        private DialogResult ShowMessage(메세지 msg, params object[] paras)
        {
            switch (msg)
            {
                case 메세지.거래구분이국내일때만자동의뢰및입고행위가가능합니다:
                    return this.ShowMessage("PU_M000121");
                case 메세지.공장을먼저선택하십시오:
                    return this.ShowMessage("PU_M000070");
                case 메세지.삭제할수없습니다:
                    return this.ShowMessage("MA_M000094");
                default:
                    return DialogResult.None;
            }
        }

        public void SetCC(int row, string arg_cd_purgrp)
        {
            if (row == 0)
            {
                if (arg_cd_purgrp == string.Empty)
                {
                    this._header.CurrentRow["CD_CC_PURGRP"] = string.Empty;
                    this._header.CurrentRow["NM_CC_PURGRP"] = string.Empty;
                }
                else
                {
                    DataTable cdCcCodeSearch = this._biz.GetCD_CC_CodeSearch(arg_cd_purgrp);
                    if (cdCcCodeSearch == null || cdCcCodeSearch.Rows.Count <= 0)
                        return;
                    this._header.CurrentRow["CD_CC_PURGRP"] = cdCcCodeSearch.Rows[0]["CD_CC"].ToString();
                    this._header.CurrentRow["NM_CC_PURGRP"] = cdCcCodeSearch.Rows[0]["NM_CC"].ToString();
                }
            }
            else if (this.m_sEnv_CC_Menu == "100")
                this.SetCC_Priority(row, null, null, null, null);
            else if (this.m_sEnv_CC == "100")
            {
                this._flexD[row, "CD_CC"] = this._header.CurrentRow["CD_CC_TPPO"];
                this._flexD[row, "NM_CC"] = this._header.CurrentRow["NM_CC_TPPO"];
            }
            else if (this.m_sEnv_CC == "200" && D.GetString(this._flexD[row, "CD_PJT"]) != string.Empty)
            {
                DataTable cdCcCodeSearchPjt = this._biz.GetCD_CC_CodeSearch_pjt(D.GetString(this._flexD[row, "CD_PJT"]));
                if (cdCcCodeSearchPjt != null && cdCcCodeSearchPjt.Rows.Count > 0)
                {
                    this._flexD[row, "CD_CC"] = D.GetString(cdCcCodeSearchPjt.Rows[0]["CD_CC"]);
                    this._flexD[row, "NM_CC"] = D.GetString(cdCcCodeSearchPjt.Rows[0]["NM_CC"]);
                }
            }
            else if (this.m_sEnv_CC == "300" && D.GetString(this._flexD[row, "CD_ITEM"]) != string.Empty)
            {
                DataTable codeSearchCdItem = this._biz.GetCD_CC_CodeSearch_cd_item(D.GetString(this._flexD[row, "CD_ITEM"]), D.GetString(this.cbo공장.SelectedValue));
                if (codeSearchCdItem != null && codeSearchCdItem.Rows.Count > 0)
                {
                    this._flexD[row, "CD_CC"] = D.GetString(codeSearchCdItem.Rows[0]["CD_CC"]);
                    this._flexD[row, "NM_CC"] = D.GetString(codeSearchCdItem.Rows[0]["NM_CC"]);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(this._flexD[row, "CD_ITEM"].ToString()))
				{
                    this._flexD[row, "CD_CC"] = this._header.CurrentRow["CD_CC_PURGRP"];
                    this._flexD[row, "NM_CC"] = this._header.CurrentRow["NM_CC_PURGRP"];
                }
				else
				{
                    string query = @"SELECT A.CD_CC, B.NM_CC
FROM MA_PITEM A
LEFT JOIN DZSN_MA_CC B ON B.CD_COMPANY = A.CD_COMPANY AND B.CD_CC = A.CD_CC
WHERE A.CD_COMPANY = '{0}'
AND A.CD_ITEM = '{1}'";
                    DataTable dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, this._flexD[row, "CD_ITEM"].ToString()));
                    this._flexD[row, "CD_CC"] = dt.Rows[0]["CD_CC"].ToString();
                    this._flexD[row, "NM_CC"] = dt.Rows[0]["NM_CC"].ToString();
                }
            }
        }

        public void SetCC(
          int row,
          string arg_cd_purgrp,
          string cd_cc,
          string nm_cc,
          string cd_pr_emp_cc,
          string nm_pr_emp_cc)
        {
            if (row == 0)
            {
                if (arg_cd_purgrp == string.Empty)
                {
                    this._header.CurrentRow["CD_CC_PURGRP"] = string.Empty;
                    this._header.CurrentRow["NM_CC_PURGRP"] = string.Empty;
                }
                else
                {
                    DataTable cdCcCodeSearch = this._biz.GetCD_CC_CodeSearch(arg_cd_purgrp);
                    if (cdCcCodeSearch == null || cdCcCodeSearch.Rows.Count <= 0)
                        return;
                    this._header.CurrentRow["CD_CC_PURGRP"] = cdCcCodeSearch.Rows[0]["CD_CC"].ToString();
                    this._header.CurrentRow["NM_CC_PURGRP"] = cdCcCodeSearch.Rows[0]["NM_CC"].ToString();
                }
            }
            else if (this.m_sEnv_CC_Menu == "100")
                this.SetCC_Priority(row, cd_cc, nm_cc, cd_pr_emp_cc, nm_pr_emp_cc);
            else if (this.m_sEnv_CC == "100")
            {
                this._flexD[row, "CD_CC"] = this._header.CurrentRow["CD_CC_TPPO"];
                this._flexD[row, "NM_CC"] = this._header.CurrentRow["NM_CC_TPPO"];
            }
            else if (this.m_sEnv_CC == "200" && D.GetString(this._flexD[row, "CD_PJT"]) != string.Empty)
            {
                DataTable cdCcCodeSearchPjt = this._biz.GetCD_CC_CodeSearch_pjt(D.GetString(this._flexD[row, "CD_PJT"]));
                if (cdCcCodeSearchPjt != null && cdCcCodeSearchPjt.Rows.Count > 0)
                {
                    this._flexD[row, "CD_CC"] = D.GetString(cdCcCodeSearchPjt.Rows[0]["CD_CC"]);
                    this._flexD[row, "NM_CC"] = D.GetString(cdCcCodeSearchPjt.Rows[0]["NM_CC"]);
                }
            }
            else if (this.m_sEnv_CC == "300" && D.GetString(this._flexD[row, "CD_ITEM"]) != string.Empty)
            {
                DataTable codeSearchCdItem = this._biz.GetCD_CC_CodeSearch_cd_item(D.GetString(this._flexD[row, "CD_ITEM"]), D.GetString(this.cbo공장.SelectedValue));
                if (codeSearchCdItem != null && codeSearchCdItem.Rows.Count > 0)
                {
                    this._flexD[row, "CD_CC"] = D.GetString(codeSearchCdItem.Rows[0]["CD_CC"]);
                    this._flexD[row, "NM_CC"] = D.GetString(codeSearchCdItem.Rows[0]["NM_CC"]);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(this._flexD[row, "CD_ITEM"].ToString()))
                {
                    this._flexD[row, "CD_CC"] = this._header.CurrentRow["CD_CC_PURGRP"];
                    this._flexD[row, "NM_CC"] = this._header.CurrentRow["NM_CC_PURGRP"];
                }
                else
                {
                    string query = @"SELECT A.CD_CC, B.NM_CC
FROM MA_PITEM A
LEFT JOIN DZSN_MA_CC B ON B.CD_COMPANY = A.CD_COMPANY AND B.CD_CC = A.CD_CC
WHERE A.CD_COMPANY = '{0}'
AND A.CD_ITEM = '{1}'";
                    DataTable dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, this._flexD[row, "CD_ITEM"].ToString()));
                    this._flexD[row, "CD_CC"] = dt.Rows[0]["CD_CC"].ToString();
                    this._flexD[row, "NM_CC"] = dt.Rows[0]["NM_CC"].ToString();
                }
            }
        }

        public void SetCC_Line(int row, string arg_cd_purgrp)
        {
            if (this.m_sEnv_CC_Line == "Y" && arg_cd_purgrp != string.Empty)
            {
                DataTable cdCcCodeSearch = this._biz.GetCD_CC_CodeSearch(arg_cd_purgrp);
                if (cdCcCodeSearch == null || cdCcCodeSearch.Rows.Count <= 0)
                    return;
                this._flexD[row, "CD_CC"] = cdCcCodeSearch.Rows[0]["CD_CC"].ToString();
                this._flexD[row, "NM_CC"] = cdCcCodeSearch.Rows[0]["NM_CC"].ToString();
            }
            else
            {
                if (string.IsNullOrEmpty(this._flexD[row, "CD_ITEM"].ToString()))
                {
                    this._flexD[row, "CD_CC"] = this._header.CurrentRow["CD_CC_PURGRP"];
                    this._flexD[row, "NM_CC"] = this._header.CurrentRow["NM_CC_PURGRP"];
                }
                else
                {
                    string query = @"SELECT A.CD_CC, B.NM_CC
FROM MA_PITEM A
LEFT JOIN DZSN_MA_CC B ON B.CD_COMPANY = A.CD_COMPANY AND B.CD_CC = A.CD_CC
WHERE A.CD_COMPANY = '{0}'
AND A.CD_ITEM = '{1}'";
                    DataTable dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, this._flexD[row, "CD_ITEM"].ToString()));
                    this._flexD[row, "CD_CC"] = dt.Rows[0]["CD_CC"].ToString();
                    this._flexD[row, "NM_CC"] = dt.Rows[0]["NM_CC"].ToString();
                }
            }
        }
        
        private void 금액계산(int row, decimal 단가, decimal 수량, string p_call, decimal p_newValue)
        {
            this._flexD.Row = row;
            decimal num1 = 1M;
            decimal num2 = 0M;
            decimal num3 = 0M;
            decimal ldb_amEx = 0M;
            decimal num8 = Math.Round(수량, 0);
            if (D.GetDecimal(this._flexD[row, "QT_PO_MM"]) == 0M && p_call != "")
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { "발주수량" });
            }
            else
            {
                decimal num5 = this._flexD.CDecimal(this._flexD[row, "RT_PO"]);
                if (num5 == 0M)
                    num5 = 1M;
                if (this.cur환정보.DecimalValue != 0M)
                    num1 = this.cur환정보.DecimalValue;
                decimal num6 = 단가;
                decimal d1 = !(this._header.CurrentRow["FG_TRANS"].ToString() != "001") && !(this._flexD[row, "FG_TAX"].ToString() == string.Empty) ? (this._flexD.CDecimal(this._flexD[row, "RATE_VAT"]) == 0M ? 0M : this._flexD.CDecimal(this._flexD[row, "RATE_VAT"]) / 100M) : this.cur부가세율.DecimalValue / 100M;
                if (Global.MainFrame.ServerKeyCommon.Contains("DHF") && this._header.CurrentRow["FG_TRANS"].ToString() != "001" && D.GetDecimal(this._flexD[row, "RATE_VAT"]) != 0M)
                    d1 = this._flexD.CDecimal(this._flexD[row, "RATE_VAT"]) == 0M ? 0M : this._flexD.CDecimal(this._flexD[row, "RATE_VAT"]) / 100M;
                if (p_call == "AM_EX")
                {
                    ldb_amEx = Unit.외화금액(DataDictionaryTypes.PU, p_newValue);
                    num3 = Unit.원화금액(DataDictionaryTypes.PU, p_newValue * num1);
                    num2 = Unit.원화금액(DataDictionaryTypes.PU, num3 * d1);
                    this._flexD[row, "UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, ldb_amEx / 수량);
                    this._flexD[row, "UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(this._flexD[row, "QT_PO"]) == 0M ? 0M : D.GetDecimal(this._flexD[row, "AM_EX"]) / this._flexD.CDecimal(this._flexD[row, "QT_PO"]));
                    this._flexD[row, "UM"] = Unit.원화단가(DataDictionaryTypes.PU, ldb_amEx / (수량 * num5) * num1);
                    this._flexD[row, "UM_P"] = Unit.원화단가(DataDictionaryTypes.PU, ldb_amEx / 수량 * num1);
                }
                else if (p_call == "NUM_USERDEF4_PO")
                {
                    if (Global.MainFrame.ServerKeyCommon == "LUKEN")
                    {
                        ldb_amEx = Unit.외화금액(DataDictionaryTypes.PU, p_newValue / num1);
                        num3 = Unit.원화금액(DataDictionaryTypes.PU, p_newValue);
                        num2 = Unit.원화금액(DataDictionaryTypes.PU, num3 * d1);
                        this._flexD[row, "UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, ldb_amEx / 수량);
                        this._flexD[row, "UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(this._flexD[row, "QT_PO"]) == 0M ? 0M : ldb_amEx / this._flexD.CDecimal(this._flexD[row, "QT_PO"]));
                        this._flexD[row, "UM"] = Unit.원화단가(DataDictionaryTypes.PU, ldb_amEx / (수량 * num5) * num1);
                        this._flexD[row, "UM_P"] = Unit.원화단가(DataDictionaryTypes.PU, ldb_amEx / 수량 * num1);
                        if (수량 > 0M)
                            this._flexD[row, "NUM_USERDEF3_PO"] = (p_newValue / 수량);
                    }
                }
                else
                {
                    if (this.bStandard && Global.MainFrame.ServerKey == "SINJINSM" && D.GetDecimal(this._flexD[row, "UM_WEIGHT"]) != 0M)
                    {
                        this.calcAM(row);
                        num6 = D.GetDecimal(this._flexD[row, "UM_EX_PO"]);
                        단가 = num6;
                    }
                    if (D.GetString(this._flexD[row, "TP_UM_TAX"]) == "001")
                    {
                        decimal num7 = Global.MainFrame.LoginInfo.CompanyLanguage == 0 ? decimal.Round(수량 * 단가 * num1, MidpointRounding.AwayFromZero) : Unit.원화금액(DataDictionaryTypes.PU, 수량 * 단가 * num1);
                        if (Global.MainFrame.ServerKeyCommon.ToUpper() == "KFL")
                        {
                            num2 = decimal.Ceiling(num7 / (d1 + 1) * d1);
                            num3 = decimal.Ceiling(num7 - num2);
                        }
                        else
                        {
                            num2 = (!this.의제매입여부(D.GetString(this._flexD[row, "FG_TAX"])) || !(this.s_vat_fictitious == "100")) && !Global.MainFrame.ServerKeyCommon.Contains("INITECH") ? (Global.MainFrame.LoginInfo.CompanyLanguage == 0 ? decimal.Round(num7 / (d1 + 1) * d1, MidpointRounding.AwayFromZero) : Unit.원화금액(DataDictionaryTypes.PU, num7 / (d1 + 1) * d1)) : Unit.원화금액(DataDictionaryTypes.PU, num7 * d1);
                            num3 = Unit.원화금액(DataDictionaryTypes.PU, num7 - num2);
                        }
                        ldb_amEx = Unit.외화금액(DataDictionaryTypes.PU, num3 / num1);
                    }
                    else
                    {
                        ldb_amEx = Unit.외화금액(DataDictionaryTypes.PU, num8 * num6);
                        decimal d2 = Unit.원화금액(DataDictionaryTypes.PU, ldb_amEx * num1);
                        if (Global.MainFrame.ServerKeyCommon.ToUpper() == "KFL")
                        {
                            num2 = decimal.Ceiling(d2 * d1);
                            num3 = decimal.Ceiling(d2);
                        }
                        else
                        {
                            num2 = Unit.원화금액(DataDictionaryTypes.PU, d2 * d1);
                            num3 = Unit.원화금액(DataDictionaryTypes.PU, d2);
                        }
                    }
                    if (p_call == "UM_EX")
                    {
                        this._flexD[row, "UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, 단가);
                        this._flexD[row, "UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, 단가 * num5);
                        this._flexD[row, "UM"] = Unit.원화단가(DataDictionaryTypes.PU, 단가 * num1);
                        this._flexD[row, "UM_P"] = Unit.원화단가(DataDictionaryTypes.PU, 단가 * num5 * num1);
                    }
                    else
                    {
                        this._flexD[row, "UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, 단가);
                        this._flexD[row, "UM_P"] = Unit.원화단가(DataDictionaryTypes.PU, 단가 * num1);
                        this._flexD[row, "UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, 단가 / num5);
                        this._flexD[row, "UM"] = Unit.원화단가(DataDictionaryTypes.PU, 단가 / num5 * num1);
                    }
                    if (Global.MainFrame.ServerKeyCommon == "LUKEN")
                        this._flexD[row, "NUM_USERDEF3_PO"] = Unit.외화단가(DataDictionaryTypes.PU, 단가);
                    else if (Global.MainFrame.ServerKeyCommon.Contains("SHTP") && p_call == "UM_EX_PO")
                    {
                        if (D.GetDecimal(this._flexD[row, "AM_EX_TRANS"]) == 0M)
                        {
                            this._flexD[row, "NUM_USERDEF1"] = Unit.외화단가(DataDictionaryTypes.PU, 단가);
                        }
                        else
                        {
                            this._flexD[row, "AM_EX_TRANS"] = 0;
                            this._flexD[row, "AM_TRANS"] = 0;
                        }
                    }
                }
                this._flexD[row, "AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, ldb_amEx);
				if (p_call != "UM_EX")
					this._flexD[row, "QT_PO"] = Unit.수량(DataDictionaryTypes.PU, 수량 * num5);
				this._flexD[row, "AM"] = num3;
                this._flexD[row, "VAT"] = num2;
                this._flexD[row, "AM_TOTAL"] = (num3 + num2);
                if (Global.MainFrame.ServerKeyCommon == "LUKEN")
                    this._flexD[row, "NUM_USERDEF4_PO"] = num3;
                if (Global.MainFrame.ServerKeyCommon.ToUpper() == "ICDERPU")
                    this._flexD[row, "AM_PRE"] = (D.GetDecimal(this._flexD[row, "UM_PRE"]) * D.GetDecimal(this._flexD[row, "QT_PO"]));
                this.SUMFunction();
                this.기성매입재계산(ldb_amEx);
            }
        }

        private void 기성매입재계산(decimal ldb_amEx)
        {
            if (D.GetString(this.sPUIV) == "000")
                return;
            decimal num1 = 0M;
            decimal num2 = 1M;
            DataRow[] dataRowArray = null;
            if (this.cur환정보.DecimalValue != 0M)
                num2 = this.cur환정보.DecimalValue;
            decimal d1 = !(this._header.CurrentRow["FG_TRANS"].ToString() != "001") && !(this._flexD["FG_TAX"].ToString() == string.Empty) ? (this._flexD.CDecimal(this._flexD["RATE_VAT"]) == 0M ? 0M : this._flexD.CDecimal(this._flexD["RATE_VAT"]) / 100M) : this.cur부가세율.DecimalValue / 100M;
            if (this.sPUIV == "100")
            {
                ldb_amEx = D.GetDecimal(this._header.CurrentRow["AM_EX"]);
                dataRowArray = this._flexIV.DataTable.Select("NO_POLINE = '" + num1 + "' AND ISNULL(AM_EX_PUL, 0) = 0");
            }
            if (this.sPUIV == "200")
                dataRowArray = this._flexIV.DataTable.Select("NO_POLINE = '" + D.GetDecimal(this._flexD["NO_LINE"]) + "'");
            foreach (DataRow dataRow in dataRowArray)
            {
                decimal num3 = D.GetDecimal(dataRow["RT_IV"]);
                decimal num4 = Unit.외화금액(DataDictionaryTypes.PU, ldb_amEx * num3 / 100M);
                decimal d2 = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flexD["AM_TOTAL"]) * num3 / 100M);
                decimal num5;
                decimal num6;
                decimal num7;
                if (D.GetString(this._flexD["TP_UM_TAX"]) == "001")
                {
                    decimal num8 = Global.MainFrame.LoginInfo.CompanyLanguage == 0 ? decimal.Round(d2, MidpointRounding.AwayFromZero) : Unit.원화금액(DataDictionaryTypes.PU, d2);
                    num5 = (!this.의제매입여부(D.GetString(this._flexD["FG_TAX"])) || !(this.s_vat_fictitious == "100")) && !Global.MainFrame.ServerKeyCommon.Contains("INITECH") ? (Global.MainFrame.LoginInfo.CompanyLanguage == 0 ? decimal.Round(num8 / ++(d1) * d1, MidpointRounding.AwayFromZero) : Unit.원화금액(DataDictionaryTypes.PU, num8 / (d1 + 1) * d1)) : Unit.원화금액(DataDictionaryTypes.PU, num8 * d1);
                    num6 = Unit.원화금액(DataDictionaryTypes.PU, num8 - num5);
                    num7 = Unit.외화금액(DataDictionaryTypes.PU, num6 / num2);
                }
                else
                {
                    num7 = Unit.외화금액(DataDictionaryTypes.PU, num4);
                    decimal num9 = Unit.원화금액(DataDictionaryTypes.PU, num7 * num2);
                    num5 = Unit.원화금액(DataDictionaryTypes.PU, num9 * d1);
                    num6 = Unit.원화금액(DataDictionaryTypes.PU, num9);
                }
                dataRow["AM"] = num7;
                dataRow["AM_k"] = num6;
                dataRow["AM_VAT"] = num5;
                dataRow["AM_SUM"] = (num6 + num5);
            }
            this._flexIV.SumRefresh();
        }

        private void 기성매입금액계산(
          int row,
          decimal key,
          decimal 총금액,
          decimal 외화금액,
          string p_call,
          decimal p_newValue)
        {
            if (this.sPUIV == "000")
                return;
            this._flexIV.Row = row;
            decimal num1 = 1M;
            decimal num2 = 0M;
            decimal d1 = 0M;
            if (this.cur환정보.DecimalValue != 0M)
                num1 = this.cur환정보.DecimalValue;
            decimal d2 = !(this._header.CurrentRow["FG_TRANS"].ToString() != "001") && !(this._flexD["FG_TAX"].ToString() == string.Empty) ? (this._flexD.CDecimal(this._flexD["RATE_VAT"]) == 0M ? 0M : this._flexD.CDecimal(this._flexD["RATE_VAT"]) / 100M) : this.cur부가세율.DecimalValue / 100M;
            decimal num3;
            if (p_call == "RT_IV")
            {
                num2 = Unit.외화금액(DataDictionaryTypes.PU, 외화금액 * p_newValue / 100M);
                d1 = 총금액 * p_newValue / 100M;
                num3 = p_newValue;
            }
            else if (p_call == "AM")
            {
                num2 = p_newValue;
                num3 = Math.Floor(num2 / 외화금액 * 100M);
            }
            else
            {
                d1 = p_newValue;
                num3 = Math.Floor(d1 / 총금액 * 100M);
            }
            decimal num4;
            decimal num5;
            decimal num6;
            if (D.GetString(this._flexD["TP_UM_TAX"]) == "001")
            {
                decimal num7 = Global.MainFrame.LoginInfo.CompanyLanguage == 0 ? decimal.Round(d1, MidpointRounding.AwayFromZero) : Unit.원화금액(DataDictionaryTypes.PU, d1);
                num4 = (!this.의제매입여부(D.GetString(this._flexD["FG_TAX"])) || !(this.s_vat_fictitious == "100")) && !Global.MainFrame.ServerKeyCommon.Contains("INITECH") ? (Global.MainFrame.LoginInfo.CompanyLanguage == 0 ? decimal.Round(num7 / ++(d2) * d2, MidpointRounding.AwayFromZero) : Unit.원화금액(DataDictionaryTypes.PU, num7 / (d2 + 1) * d2)) : Unit.원화금액(DataDictionaryTypes.PU, num7 * d2);
                num5 = Unit.원화금액(DataDictionaryTypes.PU, num7 - num4);
                num6 = Unit.외화금액(DataDictionaryTypes.PU, num5 / num1);
            }
            else
            {
                num6 = Unit.외화금액(DataDictionaryTypes.PU, num2);
                decimal num8 = Unit.원화금액(DataDictionaryTypes.PU, num6 * num1);
                num4 = Unit.원화금액(DataDictionaryTypes.PU, num8 * d2);
                num5 = Unit.원화금액(DataDictionaryTypes.PU, num8);
            }
            if (D.GetDecimal(this._flexIV["AM_EX_PUL"]) > 0M && num6 > D.GetDecimal(this._flexIV["AM_EX_PUL"]))
            {
                this.ShowMessage(공통메세지._은_보다작거나같아야합니다, new string[] { "금액은 ", "확정매입액 " });
            }
            else
            {
                if (this.sPUIV == "200")
                    this._flexIV["RT_IV"] = num3;
                this._flexIV["AM"] = Unit.외화금액(DataDictionaryTypes.PU, num6);
                this._flexIV["AM_K"] = num5;
                this._flexIV["AM_VAT"] = num4;
                this._flexIV["AM_SUM"] = (num5 + num4);
            }
        }

        private void 부가세계산(DataRow row)
        {
            decimal num1 = 0M;
            string empty = string.Empty;
            decimal d = D.GetDecimal(row["RATE_VAT"]) == 0M ? 0M : D.GetDecimal(row["RATE_VAT"]) / 100M;
            decimal num2 = D.GetDecimal(row["QT_PO_MM"]);
            decimal num3 = D.GetDecimal(row["UM_EX_PO"]);
            decimal num4 = D.GetDecimal(this._header.CurrentRow["RT_EXCH"]) == 0M ? 1M : D.GetDecimal(this._header.CurrentRow["RT_EXCH"]);
            if (num2 == 0M)
                return;
            decimal num5;
            decimal num6;
            decimal num7;
            if (D.GetString(row["TP_UM_TAX"]) == "001")
            {
                decimal num8 = Global.MainFrame.LoginInfo.CompanyLanguage == 0 ? decimal.Round(num2 * num3 * num4, MidpointRounding.AwayFromZero) : Unit.원화금액(DataDictionaryTypes.PU, num2 * num3 * num4);
                num5 = !(this.s_vat_fictitious == "100") && !Global.MainFrame.ServerKeyCommon.Contains("INITECH") ? (Global.MainFrame.LoginInfo.CompanyLanguage == 0 ? decimal.Round(num8 / (d + 1) * d, MidpointRounding.AwayFromZero) : Unit.원화금액(DataDictionaryTypes.PU, num8 / (d + 1) * d)) : Unit.원화금액(DataDictionaryTypes.PU, num8 * d);
                num6 = Unit.원화금액(DataDictionaryTypes.PU, num8 - num5);
                num7 = Unit.외화금액(DataDictionaryTypes.PU, num6 / num4);
            }
            else
            {
                num7 = Unit.외화금액(DataDictionaryTypes.PU, num2 * num3);
                num6 = Unit.원화금액(DataDictionaryTypes.PU, num2 * num3 * num4);
                num5 = Unit.원화금액(DataDictionaryTypes.PU, num6 * d);
                num1 = Unit.원화금액(DataDictionaryTypes.PU, num6 + num5);
            }
            row["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, num3);
            row["UM_P"] = Unit.원화단가(DataDictionaryTypes.PU, num3 * num4);
            row["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, num3 / (D.GetDecimal(row["RT_PO"]) == 0M ? 1M : D.GetDecimal(row["RT_PO"])));
            row["UM"] = Unit.원화단가(DataDictionaryTypes.PU, num3 / (D.GetDecimal(row["RT_PO"]) == 0M ? 1M : D.GetDecimal(row["RT_PO"])) * num4);
            row["AM_EX"] = num7;
            row["AM"] = num6;
            row["VAT"] = num5;
            row["AM_TOTAL"] = (num6 + num5);
            this.SUMFunction();
        }

        private int checkCLS()
        {
            for (int index = 2; index <= this._flexD.Rows.Count; ++index)
            {
                if (D.GetDecimal(this._flexD[index, "UM_WEIGHT"]) > 0M && (D.GetString(this._flexD[index, "CLS_L"]) == "" || D.GetString(this._flexD[index, "CLS_M"]) == "" || D.GetString(this._flexD[index, "CLS_S"]) == ""))
                {
                    this.ShowMessage("입력되지 않은 대중소분류가 존재합니다.");
                    return index;
                }
            }
            return 0;
        }

        private int checkITEMGRP()
        {
            for (int index = 2; index <= this._flexD.Rows.Count; ++index)
            {
                if (D.GetDecimal(this._flexD[index, "UM_WEIGHT"]) > 0M && D.GetString(this._flexD[index, "GRP_ITEM"]) == "")
                {
                    this.ShowMessage("입력되지 않은 품목군이 존재합니다.");
                    return index;
                }
            }
            return 0;
        }

        private void 부가세만계산()
        {
            decimal num1 = D.GetDecimal(this._flexD["RATE_VAT"]) == 0M ? 0M : D.GetDecimal(this._flexD["RATE_VAT"]) / 100M;
            decimal num2 = D.GetDecimal(this._header.CurrentRow["RT_EXCH"]) == 0M ? 1M : D.GetDecimal(this._header.CurrentRow["RT_EXCH"]);
            this._flexD["VAT"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flexD["AM"]) * num1);
            this._flexD["AM_TOTAL"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flexD["AM"]) + D.GetDecimal(this._flexD["VAT"]));
        }

        private void SetButtonDisp(RoundedButton[] p_button, int p_X)
        {
            int num = 1;
            int x = p_X;
            for (int index = 0; index < p_button.Length; ++index)
            {
                if ((p_button[index]).Visible)
                {
                    int width = (p_button[index]).Width;
                    x = x - num - width;
                    (p_button[index]).SetBounds(x, (p_button[index]).Location.Y, (p_button[index]).Width, (p_button[index]).Height);
                }
            }
        }

        private void m_pnlHeader_Enabled()
        {
            this.txt발주번호.Enabled = false;
            this.dtp발주일자.Enabled = false;
            this.ctx거래처.Enabled = false;
            this.cbo공장.Enabled = false;
            this.ctx구매그룹.Enabled = false;
            this.ctx담당자.Enabled = false;
            this.ctx발주형태.Enabled = false;
            this.cbo환정보.Enabled = false;
            this.cur환정보.Enabled = false;
            this.cbo단가유형.Enabled = false;
            this.cbo지급조건.Enabled = false;
            this.cbo부가세여부.Enabled = false;
            this.ctx프로젝트.Enabled = false;
            if (MA.ServerKey(false, new string[1] { "TSUBAKI" }))
                this.txt오더번호.Enabled = true;
            else
                this.txt오더번호.Enabled = false;
            this.ctx창고.Enabled = false;
            this.btn창고적용.Enabled = false;
            this.tb_NO_PO_MH.Enabled = false;
        }

        private bool Partner_Accept(DataTable dt)
        {
            int num1 = 0;
            string empty1 = string.Empty;
            string empty2 = string.Empty;
            string[] strArray = new string[] { "CD_PARTNER",
                                               "LN_PARTNER" };
            foreach (DataRow row in ComFunc.getGridGroupBy(dt, strArray, true).Rows)
            {
                if (D.GetString(row["CD_PARTNER"]) != "")
                {
                    ++num1;
                    empty1 = D.GetString(row["CD_PARTNER"]);
                    empty2 = D.GetString(row["LN_PARTNER"]);
                }
            }
            if (empty1 != string.Empty)
            {
                this._header.CurrentRow["CD_PARTNER"] = empty1;
                this._header.CurrentRow["LN_PARTNER"] = empty2;
                this.ctx거래처.CodeValue = empty1;
                this.ctx거래처.CodeName = empty2;
            }
            int num2;
            if (this.m_tab_poh.TabPages.Contains(this.tabPage7))
                num2 = !MA.ServerKey(false, new string[] { "HOTEL" }) ? 1 : 0;
            else
                num2 = 1;
            if (num2 == 0)
                this.set_IV_DT_HOTEL(D.GetString(this._header.CurrentRow["CD_PARTNER"]));
            return true;
        }

        private bool Exch_Accept(DataTable dt)
        {
            int num1 = 0;
            string empty1 = string.Empty;
            string empty2 = string.Empty;
            string empty3 = string.Empty;
            string empty4 = string.Empty;
            string[] strArray = new string[] { "CD_PARTNER",
                                               "LN_PARTNER",
                                               "CD_EXCH",
                                               "RT_EXCH" };
            foreach (DataRow row in ComFunc.getGridGroupBy(dt, strArray, true).Rows)
            {
                if (D.GetString(row["CD_PARTNER"]) != "" && D.GetString(row["CD_EXCH"]) != "" && D.GetString(row["RT_EXCH"]) != "")
                {
                    ++num1;
                    empty1 = D.GetString(row["CD_PARTNER"]);
                    empty2 = D.GetString(row["LN_PARTNER"]);
                    empty3 = D.GetString(row["CD_EXCH"]);
                    empty4 = D.GetString(row["RT_EXCH"]);
                }
                else
                {
                    this.ShowMessage(this.DD("환정보or거래처가 입력되지 않았습니다."));
                    return false;
                }
            }
            if (num1 != 1 && D.GetString(this.ctx거래처.CodeValue) == "")
            {
                this.ShowMessage("거래처가 발주등록과 적용할 자료에서 모두 존재하지 않습니다.");
                return false;
            }
            if (num1 == 1)
            {
                this._header.CurrentRow["CD_PARTNER"] = empty1;
                this._header.CurrentRow["LN_PARTNER"] = empty2;
                this._header.CurrentRow["CD_EXCH"] = empty3;
                this._header.CurrentRow["RT_EXCH"] = empty4;
                this.ctx거래처.CodeValue = empty1;
                this.ctx거래처.CodeName = empty2;
                this.cbo환정보.SelectedValue = empty3;
                this.cur환정보.Text = empty4;
                if (Global.MainFrame.ServerKeyCommon == "HCT")
                {
                    this.cbo_NM_EXCH_SelectionChangeCommitted(null, null);
                    this.cur환정보.Enabled = false;
                    this._header.CurrentRow["CD_PARTNER"] = empty1;
                    this._header.CurrentRow["LN_PARTNER"] = empty2;
                    this.ctx구매그룹.CodeValue = D.GetString(dt.Rows[0]["CD_PURGRP"]);
                    this.ctx구매그룹.CodeName = D.GetString(dt.Rows[0]["NM_PURGRP"]);
                    this._header.CurrentRow["CD_PURGRP"] = D.GetString(dt.Rows[0]["CD_PURGRP"]);
                    this._header.CurrentRow["NM_PURGRP"] = D.GetString(dt.Rows[0]["NM_PURGRP"]);
                    this.SetCC(0, this.ctx구매그룹.CodeValue);
                }
            }
            int num4;
            if (this.m_tab_poh.TabPages.Contains(this.tabPage7))
                num4 = !MA.ServerKey(false, new string[] { "HOTEL" }) ? 1 : 0;
            else
                num4 = 1;
            if (num4 == 0)
                this.set_IV_DT_HOTEL(D.GetString(this._header.CurrentRow["CD_PARTNER"]));
            return true;
        }

        private bool Tppo_Accept(DataTable dt, string strGubon)
        {
            int num1 = 0;
            string empty1 = string.Empty;
            string empty2 = string.Empty;
            string[] strArray = new string[] { "CD_TPPO",
                                               "NM_TPPO" };
            foreach (DataRow row in ComFunc.getGridGroupBy(dt, strArray, true).Rows)
            {
                if (D.GetString(row["CD_TPPO"]) != "")
                {
                    ++num1;
                    empty1 = D.GetString(row["CD_TPPO"]);
                    empty2 = D.GetString(row["NM_TPPO"]);
                }
            }
            if (num1 != 1 && D.GetString(this.ctx발주형태.CodeValue) == "")
            {
                this.ShowMessage(this.DD("발주형태가 중복 또는 없습니다."));
                return false;
            }
            if (num1 != 1 && strGubon == "전용")
            {
                this.ShowMessage(this.DD("발주형태가 중복 또는 없습니다."));
                return false;
            }
            if (num1 == 1)
            {
                this._header.CurrentRow["CD_TPPO"] = empty1;
                this._header.CurrentRow["NM_TPPO"] = empty2;
                this.ctx발주형태.CodeValue = empty1;
                this.ctx발주형태.CodeName = empty2;
                DataRow tppo = BASIC.GetTPPO(empty1);
                this._header.CurrentRow["FG_TRANS"] = tppo["FG_TRANS"];
                this._header.CurrentRow["FG_TPRCV"] = tppo["FG_TPRCV"];
                this._header.CurrentRow["FG_TPPURCHASE"] = tppo["FG_TPPURCHASE"];
                this._header.CurrentRow["YN_AUTORCV"] = tppo["YN_AUTORCV"];
                this._header.CurrentRow["YN_RCV"] = tppo["YN_RCV"];
                this._header.CurrentRow["YN_RETURN"] = tppo["YN_RETURN"];
                this._header.CurrentRow["YN_SUBCON"] = tppo["YN_SUBCON"];
                this._header.CurrentRow["YN_IMPORT"] = tppo["YN_IMPORT"];
                this._header.CurrentRow["YN_ORDER"] = tppo["YN_ORDER"];
                this._header.CurrentRow["YN_REQ"] = tppo["YN_REQ"];
                this._header.CurrentRow["YN_AM"] = tppo["YN_AM"];
                this._header.CurrentRow["NM_TRANS"] = tppo["NM_TRANS"];
                this._header.CurrentRow["FG_TAX"] = tppo["FG_TAX"];
                this._header.CurrentRow["TP_GR"] = tppo["TP_GR"];
                this._header.CurrentRow["CD_CC_TPPO"] = tppo["CD_CC"];
                this._header.CurrentRow["NM_CC_TPPO"] = tppo["NM_CC"];
                if (tppo["FG_TRANS"].ToString() == "001")
                {
                    this.cbo환정보.SelectedValue = "000";
                    this._header.CurrentRow["CD_EXCH"] = "000";
                    this.cbo_NM_EXCH_SelectionChangeCommitted(null, null);
                    this._header.CurrentRow["FG_TAX"] = D.GetString(this._header.CurrentRow["FG_TAX"]) == "" ? "21" : tppo["FG_TAX"];
                    this.cbo과세구분.Enabled = true;
                    this.cbo과세구분.SelectedValue = this._header.CurrentRow["FG_TAX"];
                    this.cbo_FG_TAX_SelectionChangeCommitted(null, null);
                }
                else if (tppo["FG_TRANS"].ToString() == "002" || tppo["FG_TRANS"].ToString() == "003")
                {
                    this.cbo환정보.SelectedValue = "000";
                    this._header.CurrentRow["CD_EXCH"] = "000";
                    this.cbo_NM_EXCH_SelectionChangeCommitted(null, null);
                    this._header.CurrentRow["FG_TAX"] = D.GetString(this._header.CurrentRow["FG_TAX"]) == "" ? "23" : tppo["FG_TAX"];
                    this.cbo과세구분.Enabled = false;
                    this.cbo과세구분.SelectedValue = this._header.CurrentRow["FG_TAX"];
                    this.cbo_FG_TAX_SelectionChangeCommitted(null, null);
                }
                else
                {
                    this._header.CurrentRow["FG_TAX"] = "";
                    this.cbo과세구분.SelectedValue = "21";
                    this.cbo과세구분.SelectedValue = "";
                    this.cbo과세구분.Enabled = false;
                    this.cbo_FG_TAX_SelectionChangeCommitted(null, null);
                }
                if (D.GetString(tppo["YN_SU"]) == "Y")
                {
                    if (Global.MainFrame.ServerKeyCommon.Contains("DONGWOON"))
                        this.btnCompanyUse1.Enabled = true;
                    this._flexDD.Enabled = true;
                }
                else
                {
                    if (Global.MainFrame.ServerKeyCommon.Contains("DONGWOON"))
                        this.btnCompanyUse1.Enabled = true;
                    this._flexDD.Enabled = false;
                }
                this.Setting_pu_poh_sub();
                if (this.m_tab_poh.TabPages.Contains(this.tabPage7))
                {
                    this.dtp만기일자.Text = Global.MainFrame.GetStringToday;
                    this.dtp지급예정일자.Text = Global.MainFrame.GetStringToday;
                    this.dtp매입일자.Text = Global.MainFrame.GetStringToday;
                    this.cbo지급구분.SelectedValue = "";
                    this.cbo전표유형.SelectedValue = "";
                    if (MA.ServerKey(false, new string[] { "HOTEL" }))
                        this.set_IV_DT_HOTEL(D.GetString(this._header.CurrentRow["CD_PARTNER"]));
                }
            }
            return true;
        }

        private bool Partner_Accept_quo(DataTable dt)
        {
            if (this.ctx거래처.CodeValue != "")
                return true;
            int num1 = 0;
            string empty1 = string.Empty;
            string empty2 = string.Empty;
            string[] strArray = new string[] { "CD_PARTNER",
                                               "LN_PARTNER" };
            foreach (DataRow row in ComFunc.getGridGroupBy(dt, strArray, true).Rows)
            {
                if (D.GetString(row["CD_PARTNER"]) != "")
                {
                    ++num1;
                    empty1 = D.GetString(row["CD_PARTNER"]);
                    empty2 = D.GetString(row["LN_PARTNER"]);
                }
            }
            if (num1 != 1)
            {
                this.ShowMessage("2개 이상의 거래처가 존재합니다.");
                return false;
            }
            if (empty1 != string.Empty)
            {
                this._header.CurrentRow["CD_PARTNER"] = empty1;
                this._header.CurrentRow["LN_PARTNER"] = empty2;
                this.ctx거래처.CodeValue = empty1;
                this.ctx거래처.CodeName = empty2;
            }
            return true;
        }

        public void CalcRebate(decimal p_qt_mm, decimal p_um_rebate)
        {
            if (!this._YN_REBATE)
                return;
            this._flexD["AM_REBATE_EX"] = Unit.외화금액(DataDictionaryTypes.PU, p_um_rebate * p_qt_mm);
            this._flexD["AM_REBATE"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flexD["AM_REBATE_EX"]) * D.GetDecimal(this.cur환정보.Text));
        }

        private void setNecessaryCondition(object[] obj, OneGrid _OneGrid, bool state)
        {
            try
            {
                List<Control> controlList = _OneGrid.GetControlList();
                for (int index1 = 0; index1 < controlList.Count; ++index1)
                {
                    if (controlList[index1].GetType().Name == "BpPanelControl")
                    {
                        BpPanelControl bpPanelControl = (BpPanelControl)controlList[index1];
                        if (!state)
                        {
                            for (int index2 = 0; index2 < obj.Length; ++index2)
                            {
                                if ((bpPanelControl).Name != D.GetString(obj[index2]))
                                {
                                    bpPanelControl.IsNecessaryCondition = !state;
                                }
                                else
                                {
                                    bpPanelControl.IsNecessaryCondition = state;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            for (int index3 = 0; index3 < obj.Length; ++index3)
                            {
                                if ((bpPanelControl).Name != D.GetString(obj[index3]))
                                {
                                    bpPanelControl.IsNecessaryCondition = state;
                                }
                                else
                                {
                                    bpPanelControl.IsNecessaryCondition = !state;
                                    break;
                                }
                            }
                        }
                        if (obj.Length == 0)
                            bpPanelControl.IsNecessaryCondition = true;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btnH_Click(object sender, EventArgs e)
        {
            try
            {
                if (((Control)sender).Name == "btnHadd")
                {
                    this._flexH.Rows.Add();
                    this._flexH.Row = this._flexH.Rows.Count - 1;
                    decimal d = this._flexH.Rows.Count - this._flexH.Rows.Fixed;
                    this._flexH["SQ_1"] = d;
                    this._flexH["CD_PLANT"] = D.GetString(this._header.CurrentRow["CD_PLANT"]);
                    this._flexH["NO_PO"] = D.GetString(this._header.CurrentRow["NO_PO"]);
                    d = d + 1;
                    this._flexH.AddFinished();
                    this._flexH.Col = this._flexH.Cols.Fixed;
                    this._flexH.Focus();
                }
                else if (this._flexH.HasNormalRow)
                    this._flexH.Rows.Remove(this._flexH.Row);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexH_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                FlexGrid flexGrid = sender as FlexGrid;
                decimal num = D.GetDecimal(this._header.CurrentRow["AM"]);
                if (num == 0M)
                    return;
                string str = ((C1FlexGridBase)sender).GetData(e.Row, e.Col).ToString();
                string editData = ((FlexGrid)sender).EditData;
                if (str.ToUpper() == editData.ToUpper())
                    return;
                bool flag = D.GetString(this._header.CurrentRow["FG_TRANS"]) == "001";
                switch (this._flexH.Cols[e.Col].Name)
                {
                    case "RT_IV":
                        this._flexH["AM"] = num - num * D.GetDecimal(editData) / 100M;
                        if (flag)
                            this._flexH["VAT"] = D.GetDecimal(this._flexH["AM"]) * 0.1M;
                        this._flexH["AM_HAP"] = D.GetDecimal(this._flexH["AM"]) + D.GetDecimal(this._flexH["VAT"]);
                        break;
                    case "AM":
                        this._flexH["RT_IV"] = D.GetDecimal(editData) / num * 100M;
                        if (flag)
                            this._flexH["VAT"] = D.GetDecimal(this._flexH["AM"]) * 0.1M;
                        this._flexH["AM_HAP"] = D.GetDecimal(this._flexH["AM"]) + D.GetDecimal(this._flexH["VAT"]);
                        break;
                    case "RT_BAN":
                        this._flexH["AM_BAN"] = num - num * D.GetDecimal(editData) / 100M;
                        this._flexH["AM_BANK"] = !flag ? D.GetDecimal(this._flexH["AM_BAN"]) : (D.GetDecimal(this._flexH["AM_BAN"]) * 1.1M);
                        break;
                    case "AM_BAN":
                        this._flexH["RT_BAN"] = (D.GetDecimal(editData) / num * 100M);
                        this._flexH["AM_BANK"] = !flag ? D.GetDecimal(editData) : (D.GetDecimal(editData) * 1.1M);
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private string getCLS_S_code(string cls_s_code, string CD_ITEM, string CD_PLANT)
        {
            string CLS_S = cls_s_code;
            if (CLS_S == string.Empty || this._biz.Check_PITEM(CD_ITEM, CD_PLANT, CLS_S).Rows.Count > 0)
                return "";
            string clsSCode;
            switch (CLS_S)
            {
                case "SC":
                    clsSCode = "M";
                    break;
                default:
                    clsSCode = CLS_S.Substring(1, 1);
                    break;
            }
            return clsSCode;
        }

        public void SetCC_Priority(
          int row,
          string cd_cc,
          string nm_cc,
          string cd_pr_emp_cc,
          string nm_pr_emp_cc)
        {
            try
            {
                foreach (DataRow row1 in this._biz.GetCD_CC_Priority().Rows)
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add("CD_CC", typeof(string));
                    dataTable.Columns.Add("NM_CC", typeof(string));
                    DataRow dataRow = dataTable.NewRow();
                    if (D.GetString(row1["CD_SYSDEF"]) == "001")
                    {
                        dataRow["CD_CC"] = D.GetString(this._header.CurrentRow["CD_CC_PURGRP"]);
                        dataRow["NM_CC"] = D.GetString(this._header.CurrentRow["NM_CC_PURGRP"]);
                    }
                    else if (D.GetString(row1["CD_SYSDEF"]) == "002")
                    {
                        dataRow["CD_CC"] = this._header.CurrentRow["CD_CC_TPPO"];
                        dataRow["NM_CC"] = this._header.CurrentRow["NM_CC_TPPO"];
                    }
                    else if (D.GetString(row1["CD_SYSDEF"]) == "003")
                        dataTable = this._biz.GetCD_CC(D.GetString(this.cbo공장.SelectedValue), D.GetString(this._flexD[row, "CD_PJT"]), "CD_PJT");
                    else if (D.GetString(row1["CD_SYSDEF"]) == "004")
                        dataTable = this._biz.GetCD_CC(D.GetString(this.cbo공장.SelectedValue), D.GetString(this._flexD[row, "CD_ITEM"]), "CD_ITEM");
                    else if (D.GetString(row1["CD_SYSDEF"]) == "005")
                    {
                        dataRow["CD_CC"] = cd_pr_emp_cc;
                        dataRow["NM_CC"] = nm_pr_emp_cc;
                    }
                    else if (D.GetString(row1["CD_SYSDEF"]) == "006")
                    {
                        dataRow["CD_CC"] = cd_cc;
                        dataRow["NM_CC"] = nm_cc;
                    }
                    else if (D.GetString(row1["CD_SYSDEF"]) == "007")
                        dataTable = this._biz.GetCD_CC(D.GetString(this._flexD[row, "CD_PLANT"]), D.GetString(this._flexD[row, "GRP_ITEM"]), "GRP_ITEM");
                    else if (D.GetString(row1["CD_SYSDEF"]) == "008")
                        dataTable = this._biz.GetCD_CC(D.GetString(this._flexD[row, "CD_PLANT"]), D.GetString(this._flexD[row, "CD_SL"]), "CD_SL");
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        if (D.GetString(dataTable.Rows[0]["CD_CC"]) != "")
                        {
                            this._flexD[row, "CD_CC"] = D.GetString(dataTable.Rows[0]["CD_CC"]);
                            this._flexD[row, "NM_CC"] = D.GetString(dataTable.Rows[0]["NM_CC"]);
                            break;
                        }
                    }
                    else if (D.GetString(dataRow["CD_CC"]) != "")
                    {
                        this._flexD[row, "CD_CC"] = D.GetString(dataRow["CD_CC"]);
                        this._flexD[row, "NM_CC"] = D.GetString(dataRow["NM_CC"]);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public void SetCC_Priority(
          DataRow row,
          string cd_cc,
          string nm_cc,
          string cd_pr_emp_cc,
          string nm_pr_emp_cc)
        {
            try
            {
                foreach (DataRow row1 in this._biz.GetCD_CC_Priority().Rows)
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add("CD_CC", typeof(string));
                    dataTable.Columns.Add("NM_CC", typeof(string));
                    DataRow dataRow = dataTable.NewRow();
                    if (D.GetString(row1["CD_SYSDEF"]) == "001")
                    {
                        dataRow["CD_CC"] = this._header.CurrentRow["CD_CC_PURGRP"];
                        dataRow["NM_CC"] = this._header.CurrentRow["NM_CC_PURGRP"];
                    }
                    else if (D.GetString(row1["CD_SYSDEF"]) == "002")
                    {
                        dataRow["CD_CC"] = this._header.CurrentRow["CD_CC_TPPO"];
                        dataRow["NM_CC"] = this._header.CurrentRow["NM_CC_TPPO"];
                    }
                    else if (D.GetString(row1["CD_SYSDEF"]) == "003")
                        dataTable = this._biz.GetCD_CC(D.GetString(this.cbo공장.SelectedValue), D.GetString(row["CD_PJT"]), "CD_PJT");
                    else if (D.GetString(row1["CD_SYSDEF"]) == "004")
                        dataTable = this._biz.GetCD_CC(D.GetString(this.cbo공장.SelectedValue), D.GetString(row["CD_ITEM"]), "CD_ITEM");
                    else if (D.GetString(row1["CD_SYSDEF"]) == "005")
                    {
                        dataRow["CD_CC"] = cd_pr_emp_cc;
                        dataRow["NM_CC"] = nm_pr_emp_cc;
                    }
                    else if (D.GetString(row1["CD_SYSDEF"]) == "006")
                    {
                        dataRow["CD_CC"] = cd_cc;
                        dataRow["NM_CC"] = nm_cc;
                    }
                    else if (D.GetString(row1["CD_SYSDEF"]) == "007")
                        dataTable = this._biz.GetCD_CC(D.GetString(row["CD_PLANT"]), D.GetString(row["GRP_ITEM"]), "GRP_ITEM");
                    else if (D.GetString(row1["CD_SYSDEF"]) == "008")
                        dataTable = this._biz.GetCD_CC(D.GetString(row["CD_PLANT"]), D.GetString(row["CD_SL"]), "CD_SL");
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        if (D.GetString(dataTable.Rows[0]["CD_CC"]) != "")
                        {
                            row["CD_CC"] = D.GetString(dataTable.Rows[0]["CD_CC"]);
                            row["NM_CC"] = D.GetString(dataTable.Rows[0]["NM_CC"]);
                            break;
                        }
                    }
                    else if (D.GetString(dataRow["CD_CC"]) != "")
                    {
                        row["CD_CC"] = D.GetString(dataRow["CD_CC"]);
                        row["NM_CC"] = D.GetString(dataRow["NM_CC"]);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public void CalcItemLT(int apply_row, decimal lt_item)
        {
            try
            {
                this._flexD[apply_row, "LT_ITEM"] = lt_item;
                DateTime exact = DateTime.ParseExact(D.GetString(this.dtp발주일자.Text), "yyyyMMdd", null);
                FlexGrid flexD1 = this._flexD;
                int num1 = apply_row;
                FlexGrid flexD2 = this._flexD;
                int num2 = apply_row;
                DateTime dateTime = exact.AddDays((double)D.GetDecimal(this._flexD[apply_row, "LT_ITEM"]));
                string str;
                object obj1 = (str = dateTime.ToString("yyyyMMdd"));
                flexD2[num2, "DT_PLAN"] = str;
                object obj2 = obj1;
                flexD1[num1, "DT_LIMIT"] = obj2;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public void CalcItemLT(int apply_row)
        {
            try
            {
                DateTime exact = DateTime.ParseExact(D.GetString(this.dtp발주일자.Text), "yyyyMMdd", null);
                FlexGrid flexD1 = this._flexD;
                int num1 = apply_row;
                FlexGrid flexD2 = this._flexD;
                int num2 = apply_row;
                DateTime dateTime = exact.AddDays(1.0);
                string str;
                object obj1 = (str = dateTime.ToString("yyyyMMdd"));
                flexD2[num2, "DT_PLAN"] = str;
                object obj2 = obj1;
                flexD1[num1, "DT_LIMIT"] = obj2;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public void SettingDiviedData(DataTable dt)
        {
            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    this._flexD.Rows.Add();
                    this._flexD.Row = this._flexD.Rows.Count - 1;
                    this._flexD["CD_PLANT"] = D.GetString(this.cbo공장.SelectedValue);
                    this._flexD["CD_EXCH"] = D.GetString(this.cbo환정보.SelectedValue);
                    this._flexD["CD_ITEM"] = row["CD_MATL"];
                    this._flexD["NM_ITEM"] = row["NM_MATL"];
                    this._flexD["STND_ITEM"] = row["STND_ITEM_MATL"];
                    this._flexD["UNIT_IM"] = row["UNIT_IM"];
                    this._flexD["UNIT_PO"] = row["UNIT_PO"];
                    this._flexD["NM_CLS_ITEM"] = row["NM_CLS_ITEM"];
                    this._flexD["DT_LIMIT"] = row["DT_DUE"];
                    this._flexD["DT_PLAN"] = row["DT_PLAN"];
                    this._flexD["RT_PO"] = !(D.GetDecimal(row["UNIT_PO_FACT"]) == 0M) ? row["UNIT_PO_FACT"] : 1;
                    this._flexD["QT_PO"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(this._m_pjtbom_rq_mng == "100" ? row["QT_REMAIN"] : row["QT_NEED"]));
                    this._flexD["QT_PO_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(this._flexD["QT_PO"]) / D.GetDecimal(this._flexD["RT_PO"]));
                    this._flexD["CD_PLANT"] = D.GetString(this.cbo공장.SelectedValue);
                    this._flexD["LT_ITEM"] = row["LT_ITEM"];
                    this._flexD["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX"]));
                    this._flexD["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX"]) * D.GetDecimal(this._flexD["RT_PO"]));
                    this._flexD["FG_SERNO"] = row["FG_SERNO"];
                    this._flexD["CD_PJT"] = row["CD_PJT"];
                    this._flexD["NM_PJT"] = row["NM_PJT"];
                    this._flexD["CD_PJT_ITEM"] = row["CD_PJT_ITEM"];
                    this._flexD["NM_PJT_ITEM"] = row["NM_PJT_ITEM"];
                    this._flexD["PJT_ITEM_STND"] = row["PJT_ITEM_STND"];
                    this._flexD["SEQ_PROJECT"] = row["SEQ_PROJECT"];
                    if (this._m_pjtbom_rq_mng == "100")
                        this._flexD["NO_LINE_PJTBOM"] = row["NO_LINE_PJTBOM"];
                    this._flexD["NO_PR"] = "";
                    this._flexD["NO_PRLINE"] = 0;
                    this.FillPol(this._flexD.Row);
                    this.품목정보구하기(new object[] { this._flexD["CD_ITEM"].ToString(),
                                                       this._flexD["CD_PLANT"].ToString(),
                                                       this.LoginInfo.CompanyCode,
                                                       this.cbo단가유형.SelectedValue.ToString(),
                                                       this.cbo환정보.SelectedValue.ToString(),
                                                       this.dtp발주일자.Text,
                                                       this.ctx거래처.CodeValue,
                                                       this.ctx구매그룹.CodeValue,
                                                       "N",
                                                       D.GetString(this._flexD["CD_PJT"]),
                                                       Global.MainFrame.ServerKeyCommon.ToUpper() }, "PJTBOM", 0);
                    this.부가세계산(this._flexD.GetDataRow(this._flexD.Row));
                    this._flexD.AddFinished();
                    this._flexD.Col = this._flexD.Cols.Fixed;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void EXCEL_Popup(object sender, EventArgs e)
        {
            try
            {
                if (!(sender is ToolStripMenuItem toolStripMenuItem) || !(toolStripMenuItem.Name == this.DD("파일생성")) || this._header.JobMode != JobModeEnum.조회후수정)
                    return;
                this._flexD.ExportToExcel(true, true, true);
            }
            catch (Exception ex)
            {
                this._flexD.Redraw = true;
                this.MsgEnd(ex);
            }
        }

        private bool Chk_ExcelData(DataTable dt_Excel)
        {
            string[] strArray = new string[] { "CD_ITEM",
                                               "DT_LIMIT",
                                               "QT_PO_MM" };
            for (int index = 0; index < strArray.Length; ++index)
            {
                if (!dt_Excel.Columns.Contains(strArray[index]))
                {
                    this.ShowMessage("컬럼명 [" + strArray[index] + "] 이 엑셀에 존재하지 않습니다.");
                    return false;
                }
            }
            return true;
        }

        private DataTable Get_ExcelData_PJT(DataTable dt_Excel)
        {
            string str = string.Empty;
            if (dt_Excel.Columns.Contains("CD_PJT"))
            {
                foreach (DataRow row in dt_Excel.DefaultView.ToTable(true, "CD_PJT").Rows)
                    str = str + row["CD_PJT"] + "|";
            }
            string[] pipes = D.StringConvert.GetPipes(str, 200);
            DataTable excelDataPjt = null;
            for (int index = 0; index < pipes.Length; ++index)
            {
                DataTable pjtInfo = this._biz.Get_PJTInfo(pipes[index]);
                if (pjtInfo != null && pjtInfo.Rows.Count > 0)
                {
                    if (excelDataPjt == null)
                        excelDataPjt = pjtInfo.Clone();
                    excelDataPjt.Merge(pjtInfo);
                }
            }
            return excelDataPjt;
        }

        private bool ChkData_PJT(string p_cd_pjt, ref string p_nm_pjt)
        {
            string empty = string.Empty;
            if (p_cd_pjt.Trim() == string.Empty)
                return true;
            if (this._dt_pjt == null || this._dt_pjt.Rows.Count < 1)
                return false;
            DataRow[] dataRowArray = this._dt_pjt.Select("NO_PROJECT = '" + p_cd_pjt + "'");
            if (dataRowArray == null || dataRowArray.Length < 1)
                return false;
            p_nm_pjt = D.GetString(dataRowArray[0]["NM_PROJECT"]);
            return true;
        }

        private void 엑셀업로드_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.HeaderCheck(0))
                    return;
                if (MA.ServerKey(true, new string[1] { "ETNERS" }) && D.GetString(this.ctx프로젝트.CodeValue) == string.Empty)
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl프로젝트.Text });
                    this.ctx프로젝트.Focus();
                }
                else
                {
                    string 멀티품목코드 = string.Empty;
                    string str1 = string.Empty;
                    string empty1 = string.Empty;
                    string empty2 = string.Empty;
                    decimal num1 = 0.0M;
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "엑셀 파일 (*.xls)|*.xls";
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        this.호출여부 = true;
                        DataTable dt_Excel1 = new Excel().StartLoadExcel(openFileDialog.FileName);
                        if (!this.Chk_ExcelData(dt_Excel1))
                            return;
                        DataTable dt_엑셀 = dt_Excel1.Clone();
                        dt_엑셀.Columns["CD_ITEM"].DataType = typeof(string);
                        foreach (DataRow row in dt_Excel1.Rows)
                            dt_엑셀.Rows.Add(row.ItemArray);
                        if ((this.cbo공장.SelectedValue == null ? "" : this.cbo공장.SelectedValue.ToString()) == "")
                        {
                            this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl공장명.Text });
                        }
                        else
                        {
                            StringBuilder stringBuilder1 = new StringBuilder();
                            bool flag1 = false;
                            string str2 = "품목코드\t 품목명\t";
                            stringBuilder1.AppendLine(str2);
                            string str3 = "-".PadRight(80, '-');
                            stringBuilder1.AppendLine(str3);
                            foreach (DataRow row in dt_엑셀.Rows)
                            {
                                if (Global.MainFrame.ServerKeyCommon == "TANHAY")
                                {
                                    if (!(row["CD_ITEM"].ToString() == string.Empty) || !(row["STND_ITEM"].ToString() == string.Empty))
                                    {
                                        if (D.GetString(row["CD_ITEM"]) == "")
                                        {
                                            DataTable dataTable = this._biz.STND_PITEM(D.GetString(row["STND_ITEM"]), D.GetString(this.cbo공장.SelectedValue));
                                            if (dataTable.Rows.Count > 1 || dataTable.Rows.Count == 0)
                                            {
                                                row["CD_ITEM"].ToString().PadRight(15, ' ');
                                                string str4 = row["STND_ITEM"].ToString().PadRight(15, ' ') + "(중복품목오류)";
                                                stringBuilder1.AppendLine(str4);
                                                flag1 = true;
                                                continue;
                                            }
                                            row["CD_ITEM"] = D.GetString(dataTable.Rows[0]["CD_ITEM"]);
                                        }
                                        if (str1 != D.GetString(row["CD_ITEM"]).ToUpper())
                                        {
                                            멀티품목코드 = 멀티품목코드 + D.GetString(row["CD_ITEM"]).ToUpper() + "|";
                                            str1 = D.GetString(row["CD_ITEM"]).ToUpper();
                                        }
                                    }
                                }
                                else if (!(row["CD_ITEM"].ToString() == string.Empty) && str1 != D.GetString(row["CD_ITEM"]).ToUpper())
                                {
                                    멀티품목코드 = 멀티품목코드 + D.GetString(row["CD_ITEM"]).ToUpper() + "|";
                                    str1 = D.GetString(row["CD_ITEM"]).ToUpper();
                                }
                            }
                            string str5 = string.Empty;
                            string empty3 = string.Empty;
                            DataTable dt_Excel2 = this._biz.엑셀(dt_엑셀);
                            DataTable dataTable1 = this._biz.공장품목(멀티품목코드, this.cbo공장.SelectedValue.ToString());
                            this._flexD.Redraw = false;
                            this._dt_pjt = this.Get_ExcelData_PJT(dt_Excel2);
                            bool flag2 = true;
                            StringBuilder stringBuilder2 = new StringBuilder();
                            string str6 = ".".PadRight(80, ' ');
                            stringBuilder2.AppendLine(str6);
                            stringBuilder2.AppendLine(str6);
                            string str7 = "프로젝트코드\t 프로젝트명\t";
                            stringBuilder2.AppendLine(str7);
                            "-".PadRight(80, '-');
                            stringBuilder2.AppendLine(str3);
                            StringBuilder stringBuilder3 = new StringBuilder();
                            string str8 = ".".PadRight(80, ' ');
                            stringBuilder3.AppendLine(str8);
                            stringBuilder3.AppendLine(str8);
                            string str9 = "프로젝트코드\t 프로젝트명\t UNIT항번";
                            stringBuilder3.AppendLine(str9);
                            string str10 = "-".PadRight(80, '-');
                            stringBuilder3.AppendLine(str10);
                            bool flag3 = true;
                            StringBuilder stringBuilder4 = new StringBuilder();
                            bool flag4 = false;
                            string str11 = "품목코드\t 품목명\t";
                            stringBuilder4.AppendLine(str11);
                            string str12 = "-".PadRight(80, '-');
                            stringBuilder4.AppendLine(str12);
                            bool flag5 = true;
                            if (Global.MainFrame.ServerKeyCommon.Contains("GLOZEN"))
                                this._dt_app = this.get_EXCEL_APP_NO(dt_Excel2);
                            StringBuilder stringBuilder5 = new StringBuilder();
                            string str13 = ".".PadRight(80, ' ');
                            stringBuilder5.AppendLine(str13);
                            stringBuilder5.AppendLine(str13);
                            string str14 = "품의번호\t 원천품목코드\t";
                            stringBuilder5.AppendLine(str14);
                            string str15 = "-".PadRight(80, '-');
                            stringBuilder5.AppendLine(str15);
                            foreach (DataRow row1 in dt_Excel2.Rows)
                            {
                                if (row1["CD_ITEM"] != null && !(row1["CD_ITEM"].ToString().Trim() == string.Empty))
                                {
                                    bool flag6 = false;
                                    foreach (DataRow row2 in dataTable1.Rows)
                                    {
                                        if (D.GetString(row1["CD_ITEM"]).ToUpper() == row2["CD_ITEM"].ToString().ToUpper().Trim())
                                        {
                                            flag6 = true;
                                            str5 = row2["CD_ITEM"].ToString().Trim();
                                            break;
                                        }
                                    }
                                    if (flag6)
                                    {
                                        DataRow[] dataRowArray1 = dataTable1.Select("CD_ITEM = '" + str5 + "'");
                                        DataRow row3 = this._flexD.DataTable.NewRow();
                                        row3["CD_ITEM"] = D.GetString(row1["CD_ITEM"]).ToUpper();
                                        row3["NM_ITEM"] = dataRowArray1[0]["NM_ITEM"];
                                        row3["STND_ITEM"] = dataRowArray1[0]["STND_ITEM"];
                                        row3["UNIT_IM"] = dataRowArray1[0]["UNIT_IM"];
                                        row3["CD_UNIT_MM"] = dataRowArray1[0]["UNIT_PO"];
                                        row3["NO_PO"] = this.txt발주번호.Text;
                                        row3["NO_LINE"] = this.최대차수 + 1;
                                        row3["CD_PLANT"] = this.cbo공장.SelectedValue.ToString();
                                        row3["NM_SYSDEF"] = this._ComfirmState;
                                        row3["NM_GRPMFG"] = dataRowArray1[0]["NM_GRPMFG"];
                                        row3["GRP_MFG"] = dataRowArray1[0]["GRP_MFG"];
                                        row3["EN_ITEM"] = dataRowArray1[0]["EN_ITEM"];
                                        row3["FG_SERNO"] = dataRowArray1[0]["FG_SERNO"];
                                        row3["LT_ITEM"] = dataRowArray1[0]["LT_ITEM"];
                                        row3["NM_MAKER"] = dataRowArray1[0]["NM_MAKER"];
                                        if (this.m_sEnv_CC == "300")
                                        {
                                            row3["CD_CC"] = dataRowArray1[0]["CD_CC"];
                                            row3["NM_CC"] = dataRowArray1[0]["NM_CC"];
                                        }
                                        if (dt_Excel2.Columns.Contains("CD_PJT") && D.GetString(row1["CD_PJT"]) != "" && this._dt_pjt != null && this._dt_pjt.Rows.Count > 0)
                                        {
                                            DataRow[] dataRowArray2 = this._dt_pjt.Select("NO_PROJECT = '" + D.GetString(row1["CD_PJT"]) + "'");
                                            if (dataRowArray2 == null || dataRowArray2.Length == 0)
                                            {
                                                row3["CD_PJT"] = string.Empty;
                                                row3["NM_PJT"] = string.Empty;
                                                flag2 = false;
                                                string str16 = row1["CD_PJT"].ToString().PadRight(15, ' ');
                                                stringBuilder2.AppendLine(str16);
                                            }
                                            else
                                            {
                                                bool flag7 = dt_Excel2.Columns.Contains("SEQ_PROJECT");
                                                row3["CD_PJT"] = D.GetString(row1["CD_PJT"]);
                                                row3["NM_PJT"] = dataRowArray2[0]["NM_PJT"];
                                                if (Config.MA_ENV.YN_UNIT == "Y" && flag7)
                                                {
                                                    DataRow[] dataRowArray3 = this._dt_pjt.Select("SEQ_PROJECT = '" + D.GetString(row1["SEQ_PROJECT"]) + "' AND NO_PROJECT ='" + D.GetString(row1["CD_PJT"]) + "'");
                                                    if (dataRowArray3 == null || dataRowArray3.Length == 0)
                                                    {
                                                        string str17 = row1["CD_PJT"].ToString().PadRight(15, ' ') + dataRowArray2[0]["NM_PJT"].ToString().PadRight(15, ' ') + row1["SEQ_PROJECT"].ToString().PadRight(15, ' ');
                                                        stringBuilder3.AppendLine(str17);
                                                        flag3 = false;
                                                    }
                                                    else
                                                    {
                                                        row3["SEQ_PROJECT"] = row1["SEQ_PROJECT"];
                                                        row3["CD_PJT_ITEM"] = dataRowArray3[0]["CD_PJT_ITEM"];
                                                        row3["NM_PJT_ITEM"] = dataRowArray3[0]["NM_PJT_ITEM"];
                                                        row3["PJT_ITEM_STND"] = dataRowArray3[0]["PJT_ITEM_STND"];
                                                    }
                                                }
                                            }
                                        }
                                        if (dt_엑셀.Columns.Contains("NO_PR") && D.GetString(row1["NO_PR"]) != "")
                                        {
                                            row3["NO_PR"] = row1["NO_PR"];
                                            row3["NO_PRLINE"] = row1["NO_PRLINE"];
                                        }
                                        else
                                            row3["NO_PR"] = "";
                                        int num3 = !dt_엑셀.Columns.Contains("FG_PACKING") ? 1 : (!(D.GetString(row1["FG_PACKING"]) != "") ? 1 : 0);
                                        row3["FG_PACKING"] = num3 != 0 ? "" : row1["FG_PACKING"];
                                        int num4 = !dt_엑셀.Columns.Contains("NM_USERDEF2") ? 1 : (!(D.GetString(row1["NM_USERDEF2"]) != "") ? 1 : 0);
                                        row3["NM_USERDEF2"] = num4 != 0 ? "" : row1["NM_USERDEF2"];
                                        if (!D.StringDate.IsValidDate(D.GetString(row1["DT_LIMIT"]), true, "yyyymmdd"))
                                            return;
                                        row3["DT_LIMIT"] = D.GetString(row1["DT_LIMIT"]);
                                        if (!Global.MainFrame.ServerKeyCommon.Contains("SOLIDTECH"))
                                        {
                                            int num5 = !dt_Excel1.Columns.Contains("DT_PLAN") ? 1 : (!D.StringDate.IsValidDate(D.GetString(row1["DT_PLAN"]), true, "yyyymmdd") ? 1 : 0);
                                            row3["DT_PLAN"] = num5 != 0 ? D.GetString(row1["DT_LIMIT"]) : D.GetString(row1["DT_PLAN"]);
                                        }
                                        row3["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, this._flexD.CDecimal(row1["AM_EX"]));
                                        row3["RT_PO"] = (D.GetDecimal(dataRowArray1[0]["UNIT_PO_FACT"]) == 0M ? 1M : this._flexD.CDecimal(dataRowArray1[0]["UNIT_PO_FACT"]));
                                        row3["QT_PO_MM"] = Unit.수량(DataDictionaryTypes.PU, row1["QT_PO_MM"].ToString() == "" ? 0M : this._flexD.CDecimal(row1["QT_PO_MM"]));
                                        row3["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, row1["UM_EX_PO"].ToString() == "" ? 0M : this._flexD.CDecimal(row1["UM_EX_PO"]));
                                        row3["QT_PO"] = Unit.수량(DataDictionaryTypes.PU, this._flexD.CDecimal(row3["QT_PO_MM"]));
                                        if (dt_Excel1.Columns.Contains("DC1"))
                                            row3["DC1"] = row1["DC1"];
                                        if (dt_Excel1.Columns.Contains("DC2"))
                                            row3["DC2"] = row1["DC2"];
                                        if (dt_Excel1.Columns.Contains("DC3"))
                                            row3["DC3"] = row1["DC3"];
                                        if (dt_Excel1.Columns.Contains("DC4"))
                                            row3["DC4"] = row1["DC4"];
                                        row3["QT_PO"] = Unit.수량(DataDictionaryTypes.PU, row1["QT_PO"].ToString() == "" ? 0M : this._flexD.CDecimal(row1["QT_PO"]));
                                        row3["CD_PLANT"] = this.cbo공장.SelectedValue.ToString();
                                        row3["FG_TAX"] = this._header.CurrentRow["FG_TAX"];
                                        row3["TP_UM_TAX"] = this._header.CurrentRow["TP_UM_TAX"];
                                        row3["RATE_VAT"] = this.cur부가세율.DecimalValue;
                                        num1 = this._flexD.CDecimal(this.cur부가세율.DecimalValue);
                                        row3["AM"] = 0;
                                        row3["VAT"] = 0;
                                        row3["UM_EX"] = 0;
                                        row3["NM_CLS_ITEM"] = dataRowArray1[0]["NM_CLS_ITEM"];
                                        object[] m_obj = new object[] { D.GetString(row1["CD_ITEM"]).ToUpper(),
                                                                        this.cbo공장.SelectedValue.ToString(),
                                                                        Global.MainFrame.LoginInfo.CompanyCode,
                                                                        this.cbo단가유형.SelectedValue.ToString(),
                                                                        this.cbo환정보.SelectedValue.ToString(),
                                                                        this.dtp발주일자.Text,
                                                                        this.ctx거래처.CodeValue,
                                                                        this.ctx구매그룹.CodeValue,
                                                                        Global.SystemLanguage.MultiLanguageLpoint.ToString() };
                                        row3["QT_INVC"] = 0;
                                        if (!(row3["CD_ITEM"].ToString() == ""))
                                        {
                                            if (this._flexD.CDecimal(row3["UM_EX_PO"]) == 0M && Math.Floor(this._flexD.CDecimal(row3["AM_EX"])) != 0M)
                                                row3["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(row3["AM_EX"]) / this._flexD.CDecimal(row3["QT_PO_MM"]));
                                            else if (this._flexD.CDecimal(row3["UM_EX_PO"]) != 0M && Math.Floor(this._flexD.CDecimal(row3["AM_EX"])) == 0M)
                                                row3["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, this._flexD.CDecimal(row3["UM_EX_PO"]) * this._flexD.CDecimal(row3["QT_PO_MM"]));
                                            if (this._flexD.CDecimal(row3["RT_PO"]) == 0M)
                                            {
                                                row3["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(row3["UM_EX_PO"]));
                                            }
                                            else
                                            {
                                                row3["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(row3["UM_EX_PO"]) / this._flexD.CDecimal(row3["RT_PO"]));
                                                row3["QT_PO"] = Unit.수량(DataDictionaryTypes.PU, this._flexD.CDecimal(row3["QT_PO_MM"]) * this._flexD.CDecimal(row3["RT_PO"]));
                                            }
                                            row3["UM_P"] = Unit.원화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(row3["UM_EX_PO"]) * this.cur환정보.DecimalValue);
                                            row3["UM"] = Unit.원화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(row3["UM_EX"]) * this.cur환정보.DecimalValue);
                                            DataSet dataSet = this._biz.ItemInfo_Search(m_obj);
                                            DataTable dataTable2 = this._biz.item_pinvn(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                       this.dtp발주일자.Text.Substring(0, 4),
                                                                                                       D.GetString(this.cbo공장.SelectedValue),
                                                                                                       D.GetString(row1["CD_ITEM"]),
                                                                                                       D.GetString(dataRowArray1[0]["CD_SL"]) });
                                            if (dataSet != null && dataSet.Tables.Count > 3)
                                            {
                                                if (dataSet.Tables[0].Rows.Count > 0)
                                                {
                                                    if (this.sFG_TAXcheck == "100" && D.GetString(this._header.CurrentRow["YN_IMPORT"]) != "Y" && D.GetString(dataSet.Tables[0].Rows[0]["FG_TAX_PU"]) != D.GetString(this.cbo과세구분.SelectedValue))
                                                    {
                                                        string str18 = row1["CD_ITEM"].ToString().PadRight(15, ' ') + " " + dataRowArray1[0]["NM_ITEM"].ToString().PadRight(15, ' ');
                                                        stringBuilder4.AppendLine(str18);
                                                        flag4 = true;
                                                        continue;
                                                    }
                                                    row3["RT_PO"] = this._flexD.CDecimal(dataSet.Tables[0].Rows[0]["UNIT_PO_FACT"]);
                                                    if (this.m_sEnv_FG_TAX != "000" && dataSet.Tables[0].Rows[0]["FG_TAX_PU"].ToString() != string.Empty)
                                                    {
                                                        row3["FG_TAX"] = dataSet.Tables[0].Rows[0]["FG_TAX_PU"];
                                                        row3["RATE_VAT"] = dataSet.Tables[0].Rows[0]["RATE_VAT"];
                                                        num1 = Convert.ToDecimal(dataSet.Tables[0].Rows[0]["RATE_VAT"]);
                                                    }
                                                    row3["WEIGHT"] = dataSet.Tables[0].Rows[0]["WEIGHT"];
                                                    row3["QT_WEIGHT"] = (this._flexD.CDecimal(row3["QT_PO_MM"]) * this._flexD.CDecimal(dataSet.Tables[0].Rows[0]["WEIGHT"]));
                                                    row3["NM_ITEMGRP"] = dataSet.Tables[0].Rows[0]["NM_ITEMGRP"];
                                                    row3["NO_MODEL"] = dataSet.Tables[0].Rows[0]["NO_MODEL"];
                                                }
                                                if (dataSet.Tables[1].Rows.Count > 0)
                                                {
                                                    if (!dt_Excel1.Columns.Contains("UM_EX_PO"))
                                                        row3["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(dataSet.Tables[1].Rows[0]["UM_ITEM"]));
                                                    if (this._flexD.CDecimal(row3["RT_PO"]) == 0M)
                                                    {
                                                        row3["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(row3["UM_EX_PO"]));
                                                    }
                                                    else
                                                    {
                                                        row3["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(row3["UM_EX_PO"]) / this._flexD.CDecimal(row3["RT_PO"]));
                                                        row3["QT_PO"] = Unit.수량(DataDictionaryTypes.PU, this._flexD.CDecimal(row3["QT_PO_MM"]) * this._flexD.CDecimal(row3["RT_PO"]));
                                                    }
                                                    row3["UM_P"] = Unit.원화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(row3["UM_EX_PO"]) * this.cur환정보.DecimalValue);
                                                    row3["UM"] = Unit.원화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(row3["UM_EX"]) * this.cur환정보.DecimalValue);
                                                    row3["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, this._flexD.CDecimal(row3["UM_EX_PO"]) * this._flexD.CDecimal(row3["QT_PO_MM"]));
                                                    if (MA.ServerKey(false, new string[] { "WINIX" }) && dataSet.Tables[1].Columns.Contains("CD_USERDEF1"))
                                                        row3["CD_USERDEF1"] = dataSet.Tables[1].Rows[0]["CD_USERDEF1"];
                                                }
                                                if (dataTable2 != null && dataTable2.Rows.Count > 0)
                                                {
                                                    row3["QT_INVC"] = dataTable2.Rows[0]["QT_INVC"];
                                                    row3["QT_ATPC"] = dataTable2.Rows[0]["QT_ATPC"];
                                                }
                                            }
                                            this.SUMFunction();
                                            this.cur현재고량.DecimalValue = this._flexD.CDecimal(row3["QT_INVC"]);
                                            row3["FG_TRANS"] = this._header.CurrentRow["FG_TRANS"];
                                            row3["FG_TPPURCHASE"] = this._header.CurrentRow["FG_TPPURCHASE"];
                                            row3["YN_AUTORCV"] = this._header.CurrentRow["YN_AUTORCV"];
                                            row3["YN_RCV"] = this._header.CurrentRow["YN_RCV"];
                                            row3["YN_RETURN"] = this._header.CurrentRow["YN_RETURN"];
                                            row3["YN_IMPORT"] = this._header.CurrentRow["YN_IMPORT"];
                                            row3["YN_ORDER"] = this._header.CurrentRow["YN_ORDER"];
                                            row3["YN_REQ"] = this._header.CurrentRow["YN_REQ"];
                                            row3["FG_RCV"] = this._header.CurrentRow["FG_TPRCV"];
                                            row3["YN_SUBCON"] = this._header.CurrentRow["YN_SUBCON"];
                                            row3["FG_PURCHASE"] = this._header.CurrentRow["FG_TPPURCHASE"];
                                            row3["CD_SL"] = dataRowArray1[0]["CD_SL"];
                                            row3["NM_SL"] = dataRowArray1[0]["NM_SL"];
                                            row3["CD_EXCH"] = this.cbo환정보.SelectedValue.ToString();
                                            if (this.m_sEnv_CC == "100")
                                            {
                                                row3["CD_CC"] = this._header.CurrentRow["CD_CC_TPPO"];
                                                row3["NM_CC"] = this._header.CurrentRow["NM_CC_TPPO"];
                                            }
                                            else if (this.m_sEnv_CC == "000")
                                            {
                                                row3["CD_CC"] = this._header.CurrentRow["CD_CC_PURGRP"];
                                                row3["NM_CC"] = this._header.CurrentRow["NM_CC_PURGRP"];
                                            }
                                            foreach (DataRow row4 in ((DataTable)this.cbo환정보.DataSource).Rows)
                                            {
                                                if (row4["CODE"].ToString() == this.cbo환정보.SelectedValue.ToString())
                                                {
                                                    row3["NM_EXCH"] = row4["NAME"];
                                                    break;
                                                }
                                            }
                                            if (dt_Excel1.Columns.Contains("CD_USERDEF1"))
                                                row3["CD_USERDEF1"] = row1["CD_USERDEF1"];
                                            if (dt_Excel1.Columns.Contains("CD_USERDEF2"))
                                                row3["CD_USERDEF2"] = row1["CD_USERDEF2"];
                                            if (dt_Excel1.Columns.Contains("DATE_USERDEF1"))
                                                row3["DATE_USERDEF1"] = row1["DATE_USERDEF1"];
                                            if (Global.MainFrame.ServerKeyCommon.Contains("DAEKHON"))
                                            {
                                                row3["NM_USERDEF1"] = ComFunc.MasterSearch("MA_PARTNER_NM", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                                           D.GetString(row1["CD_USERDEF1"]) });
                                                row3["NM_USERDEF2"] = ComFunc.MasterSearch("MA_EMP", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                                    D.GetString(row1["CD_USERDEF2"]) });
                                            }
                                            if (dt_Excel1.Columns.Contains("LN_PARTNER_GI") && D.GetString(row1["LN_PARTNER_GI"]) != "")
                                            {
                                                DataTable gipartner = this._biz.Get_Gipartner(D.GetString(row1["LN_PARTNER_GI"]));
                                                if (gipartner != null && gipartner.Rows.Count > 0)
                                                {
                                                    row3["GI_PARTNER"] = D.GetString(gipartner.Rows[0]["CD_PARTNER"]);
                                                    row3["LN_PARTNER"] = D.GetString(gipartner.Rows[0]["LN_PARTNER"]);
                                                }
                                            }
                                            if (dt_Excel2.Columns.Contains("NO_APP") && dt_Excel2.Columns.Contains("CD_ITEM_ORIGIN") && D.GetString(row1["NO_APP"]) != "" && Global.MainFrame.ServerKeyCommon.Contains("GLOZEN"))
                                            {
                                                if (this._dt_app != null && this._dt_app.Rows.Count > 0)
                                                {
                                                    string str19 = D.GetString(row1["CD_ITEM_ORIGIN"]) == "" ? D.GetString(row1["CD_ITEM"]) : D.GetString(row1["CD_ITEM_ORIGIN"]);
                                                    DataRow[] dataRowArray4 = this._dt_app.Select("NO_APP = '" + D.GetString(row1["NO_APP"]) + "' AND CD_ITEM = '" + str19 + "'");
                                                    if (dataRowArray4 == null || dataRowArray4.Length == 0)
                                                    {
                                                        string str20 = row1["NO_APP"].ToString().PadRight(15, ' ') + row1["CD_ITEM"].ToString().PadRight(15, ' ');
                                                        stringBuilder5.AppendLine(str20);
                                                        flag5 = false;
                                                    }
                                                    else
                                                    {
                                                        row3["NO_APP"] = D.GetString(row1["NO_APP"]);
                                                        row3["NO_APPLINE"] = dataRowArray4[0]["NO_APPLINE"];
                                                        row3["CD_ITEM_ORIGIN"] = dataRowArray4[0]["CD_ITEM"];
                                                    }
                                                }
                                                else
                                                {
                                                    string str21 = row1["NO_APP"].ToString().PadRight(15, ' ') + row1["CD_ITEM"].ToString().PadRight(15, ' ');
                                                    stringBuilder5.AppendLine(str21);
                                                    flag5 = false;
                                                }
                                            }
                                            if (MA.ServerKey(false, new string[] { "NANDA" }))
                                            {
                                                DateTime exact = DateTime.ParseExact(D.GetString(this.dtp발주일자.Text), "yyyyMMdd", null);
                                                DataRow dataRow1 = row3;
                                                DataRow dataRow2 = row3;
                                                DateTime dateTime = exact.AddDays((double)D.GetDecimal(row3["LT_ITEM"]));
                                                string str22;
                                                object obj1 = (str22 = dateTime.ToString("yyyyMMdd"));
                                                dataRow2["DT_PLAN"] = str22;
                                                object obj2 = obj1;
                                                dataRow1["DT_LIMIT"] = obj2;
                                            }
                                            this.부가세계산(row3);
                                            this.cur현재고량.DecimalValue = this._flexD.CDecimal(row3["QT_INVC"]);
                                            if (row3 != null)
                                                this._flexD.DataTable.Rows.Add(row3);
                                        }
                                    }
                                    else
                                    {
                                        string str23 = row1["CD_ITEM"].ToString().PadRight(15, ' ') + " " + row1["NM_ITEM"].ToString().PadRight(15, ' ');
                                        stringBuilder1.AppendLine(str23);
                                        flag1 = true;
                                    }
                                }
                            }
                            if (flag1 || !flag2 || !flag3)
                            {
                                this.ShowDetailMessage("엑셀 업로드하는 공장마스터품목,프로젝트 중 불일치하는 항목들이 있습니다. \r\n                         ▼ 버튼을 눌러서 목록을 확인하세요!", (flag1 ? stringBuilder1.ToString() : "") + (flag2 ? "" : D.GetString(stringBuilder2)) + (flag3 ? "" : D.GetString(stringBuilder3)));
                            }
                            if (flag4)
                            {
                                this.ShowDetailMessage("과세구분이 일치하지 않는 품목이 있습니다. \n  \n ▼ 버튼을 눌러서 목록을 확인하세요!", stringBuilder4.ToString());
                            }
                            if (!flag5)
                            {
                                this.ShowDetailMessage("원천품의항목이 일치하지 않는 품목이 있습니다. \n  \n ▼ 버튼을 눌러서 목록을 확인하세요!", stringBuilder5.ToString());
                            }
                            if (this._flexD.HasNormalRow)
                            {
                                this._flexD.Select(this._flexD.Rows.Fixed, this._flexD.LeftCol);
                                this.ControlButtonEnabledDisable((Control)sender, true);
                                this.cbo공장.Enabled = false;
                                this.SetHeadControlEnabled(false, 1);
                            }
                            if (Global.MainFrame.ServerKeyCommon.Contains("WINFOOD"))
                                this.SetWinFood("", 0);
                            this._flexD.IsDataChanged = true;
                            this.Page_DataChanged(null, null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                this._flexD.Redraw = true;
                this._flexD.SumRefresh();
            }
        }

        private decimal 최대차수
        {
            get
            {
                decimal 최대차수 = 0M;
                for (int index = this._flexD.Rows.Fixed; index < this._flexD.Rows.Count; ++index)
                {
                    if (this._flexD.CDecimal(this._flexD[index, "NO_LINE"]) > 최대차수)
                        최대차수 = this._flexD.CDecimal(this._flexD[index, "NO_LINE"]);
                }
                return 최대차수;
            }
        }

        private void InserGridtAdd(DataTable pdt_Line, bool p_b평형비고체크)
        {
            try
            {
                if (pdt_Line == null || pdt_Line.Rows.Count <= 0)
                    return;
                this.호출여부 = true;
                decimal maxValue = this._flexD.GetMaxValue("NO_LINE");
                this._flexD.Redraw = false;
                this.mDataArea.Enabled = false;
                this.flowLayoutPanel1.Enabled = false;
                if (this.ctx거래처.CodeValue == string.Empty || this.ctx거래처.CodeValue != pdt_Line.Rows[0]["CD_PURCUST"].ToString())
                {
                    this.ctx거래처.SetCode(pdt_Line.Rows[0]["CD_PURCUST"].ToString(), pdt_Line.Rows[0]["LN_PARTNER"].ToString());
                    this._header.CurrentRow["CD_PARTNER"] = pdt_Line.Rows[0]["CD_PURCUST"].ToString();
                }
                for (int index = 0; index < pdt_Line.Rows.Count; ++index)
                {
                    DataRow row = pdt_Line.Rows[index];
                    ++maxValue;
                    this._flexD.Rows.Add();
                    this._flexD.Row = this._flexD.Rows.Count - 1;
                    this._flexD["CD_ITEM"] = row["CD_ITEM"];
                    this._flexD["NM_ITEM"] = row["NM_ITEM"];
                    this._flexD["STND_ITEM"] = row["STND_ITEM"];
                    if (row["UNIT"] == null)
                        row["UNIT"] = "";
                    this._flexD["CD_UNIT_MM"] = row["UNIT"];
                    this._flexD["DT_LIMIT"] = !(this.dtp납기일.Text == string.Empty) ? this.dtp납기일.Text : row["DT_IO"];
                    this._flexD["DT_PLAN"] = this._flexD["DT_LIMIT"];
                    this._flexD["QT_PO_MM"] = Unit.수량(DataDictionaryTypes.PU, row["QT_MINUS"].ToString() == "" ? 0M : this._flexD.CDecimal(row["QT_MINUS"]));
                    if (this._header.CurrentRow["FG_TRANS"].ToString() == "001" && this._header.CurrentRow["CD_EXCH"].ToString() == "000")
                    {
                        this._flexD["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_PO"]));
                        this._flexD["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, Convert.ToDecimal(row["UM_PO"]) * Convert.ToDecimal(row["QT_MINUS"]));
                    }
                    else
                    {
                        this._flexD["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EXPO_CIS"]));
                        this._flexD["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, Convert.ToDecimal(row["UM_EXPO_CIS"]) * Convert.ToDecimal(row["QT_MINUS"]));
                    }
                    decimal num = DBNull.Value == row["UNIT_PO_FACT"] || !(Convert.ToDecimal(row["UNIT_PO_FACT"]) != 0M) ? 1M : Convert.ToDecimal(row["UNIT_PO_FACT"]);
                    this._flexD["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, UDecimal.Getdivision(D.GetDecimal(this._flexD["AM_EX"]), (D.GetDecimal(this._flexD["QT_PO_MM"]) != 0M ? D.GetDecimal(this._flexD["QT_PO_MM"]) : 1M) * num));
                    this._flexD["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(this._flexD["UM_EX"]) * this.cur환정보.DecimalValue);
                    this._flexD["RT_PO"] = num;
                    this._flexD["QT_PO"] = Unit.수량(DataDictionaryTypes.PU, Convert.ToDecimal(row["QT_MINUS"]) * num);
                    this._flexD["CD_PJT"] = row["CD_PJT"];
                    this._flexD["NM_PJT"] = row["NM_PJT"];
                    this._flexD["SEQ_PROJECT"] = row["SEQ_PROJECT"];
                    this._flexD["UNIT_IM"] = row["UNIT_IM"];
                    this._flexD["FG_TAX"] = this._header.CurrentRow["FG_TAX"];
                    this._flexD["TP_UM_TAX"] = this._header.CurrentRow["TP_UM_TAX"];
                    this._flexD["RATE_VAT"] = this.cur부가세율.DecimalValue;
                    if (this.m_sEnv_FG_TAX == "100" && D.GetString(row["FG_TAX_PU"]) != string.Empty)
                    {
                        this._flexD["FG_TAX"] = row["FG_TAX_PU"];
                        this._flexD["RATE_VAT"] = D.GetDecimal(row["RATE_VAT"]);
                    }
                    if (BASIC.GetMAEXC("발주등록(공장)-프로젝트별_의제매입세_구분") == "100" && D.GetString(row["CD_USERDEF14"]) == "001")
                    {
                        string str = this._biz.pjt_item_josun(D.GetString(this._flexD["CD_PJT"]));
                        if (str != "")
                        {
                            this._flexD["FG_TAX"] = str;
                            this._flexD["RATE_VAT"] = 0;
                        }
                    }
                    this.부가세계산(this._flexD.GetDataRow(this._flexD.Row));
                    this._flexD["NM_CUST_DLV"] = row["NM_CUST_DLV"];
                    this._flexD["ADDR1_DLV"] = row["ADDR1"];
                    this._flexD["NO_TEL_D2_DLV"] = row["NO_TEL_D2"];
                    this._flexD["NO_PR"] = "";
                    this._flexD["CD_PLANT"] = this.cbo공장.SelectedValue.ToString();
                    this._flexD["NO_PO"] = this.txt발주번호.Text;
                    this._flexD["NO_LINE"] = maxValue;
                    this._flexD["NM_SYSDEF"] = this._ComfirmState;
                    this._flexD["FG_TRANS"] = this._header.CurrentRow["FG_TRANS"];
                    this._flexD["FG_TPPURCHASE"] = this._header.CurrentRow["FG_TPPURCHASE"];
                    this._flexD["YN_AUTORCV"] = this._header.CurrentRow["YN_AUTORCV"];
                    this._flexD["YN_RCV"] = this._header.CurrentRow["YN_RCV"];
                    this._flexD["YN_RETURN"] = this._header.CurrentRow["YN_RETURN"];
                    this._flexD["YN_IMPORT"] = this._header.CurrentRow["YN_IMPORT"];
                    this._flexD["YN_ORDER"] = this._header.CurrentRow["YN_ORDER"];
                    this._flexD["YN_REQ"] = this._header.CurrentRow["YN_REQ"];
                    this._flexD["FG_RCV"] = this._header.CurrentRow["FG_TPRCV"];
                    this._flexD["YN_SUBCON"] = this._header.CurrentRow["YN_SUBCON"];
                    this._flexD["FG_PURCHASE"] = this._header.CurrentRow["FG_TPPURCHASE"];
                    this._flexD["NO_PR"] = "";
                    this._flexD["CD_EXCH"] = this.cbo환정보.SelectedValue.ToString();
                    this.SetCC(this._flexD.Row, "");
                    this._flexD["WEIGHT"] = row["WEIGHT"];
                    this._flexD["QT_WEIGHT"] = (this._flexD.CDecimal(this._flexD["QT_PO_MM"]) * this._flexD.CDecimal(this._flexD["WEIGHT"]));
                    if (p_b평형비고체크)
                        this._flexD["DC1"] = row["PJT_GROUP"];
                    this._flexD["NM_ITEMGRP"] = D.GetString(row["NM_ITEMGRP"]);
                    this._flexD["NO_MODEL"] = D.GetString(row["NO_MODEL"]);
                    this._flexD["STND_DETAIL_ITEM"] = D.GetString(row["STND_DETAIL_ITEM"]);
                    this._flexD["CD_USERDEF14"] = D.GetString(row["CD_USERDEF14"]);
                    this._flexD["NM_GRPMFG"] = D.GetString(row["NM_GRPMFG"]);
                    this._flexD["NM_MAKER"] = D.GetString(row["NM_MAKER"]);
                    if (!this._flexD.DataTable.Columns.Contains("APP_PJT"))
                    {
                        this._flexD.DataTable.Columns.Add("APP_PJT", typeof(string));
                        this._flexD.Cols["APP_PJT"].Visible = false;
                    }
                    this._flexD["APP_PJT"] = "Y";
                    this._flexD["FG_SERNO"] = row["FG_SERNO"];
                    this._flexD["CLS_L"] = D.GetString(row["CLS_L"]);
                    this._flexD["NM_CLS_L"] = D.GetString(row["NM_CLS_L"]);
                    this._flexD["CLS_M"] = D.GetString(row["CLS_M"]);
                    this._flexD["NM_CLS_M"] = D.GetString(row["NM_CLS_M"]);
                    this._flexD["CLS_S"] = D.GetString(row["CLS_S"]);
                    this._flexD["NM_CLS_S"] = D.GetString(row["NM_CLS_S"]);
                }
                this._flexD.Redraw = true;
                this.mDataArea.Enabled = true;
                this.flowLayoutPanel1.Enabled = true;
                this.SetHeadControlEnabled(false, 1);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private DataTable 예산chk(DataTable dt_tg)
        {
            DataTable dataTable1 = new DataTable();
            dataTable1.Columns.Add("CD_BUDGET", typeof(string));
            dataTable1.Columns.Add("NM_BUDGET", typeof(string));
            dataTable1.Columns.Add("CD_BGACCT", typeof(string));
            dataTable1.Columns.Add("NM_BGACCT", typeof(string));
            dataTable1.Columns.Add("CD_BIZPLAN", typeof(string));
            dataTable1.Columns.Add("NM_BIZPLAN", typeof(string));
            dataTable1.Columns.Add("AM_ACTSUM", typeof(decimal));
            dataTable1.Columns.Add("AM_JSUM", typeof(decimal));
            dataTable1.Columns.Add("RT_JSUM", typeof(decimal));
            dataTable1.Columns.Add("AM", typeof(decimal));
            dataTable1.Columns.Add("AM_JAN", typeof(decimal));
            dataTable1.Columns.Add("AM_ORG", typeof(decimal));
            dataTable1.Columns.Add("TP_BUNIT", typeof(string));
            dataTable1.Columns.Add("ERROR_MSG", typeof(string));
            for (int index = 0; index < dt_tg.Rows.Count; ++index)
            {
                if (dt_tg.Rows[index].RowState != DataRowState.Deleted && (!(D.GetString(dt_tg.Rows[index]["YN_BUDGET"]) != "Y") && !(D.GetString(dt_tg.Rows[index]["YN_BUDGET_PR"]) == "Y") && !(D.GetString(dt_tg.Rows[index]["YN_BUDGET_APP"]) == "Y") || !(this._구매예산CHK설정FI == "100")) && dt_tg.Rows[index]["CD_BUDGET"] != null && !(dt_tg.Rows[index]["CD_BUDGET"].ToString().Trim() == "") && dt_tg.Rows[index]["CD_BGACCT"] != null && !(dt_tg.Rows[index]["CD_BGACCT"].ToString().Trim() == "") && (!(this._YN_CdBizplan == "1") || !(D.GetString(dt_tg.Rows[index]["CD_BIZPLAN"]) == string.Empty)))
                {
                    string empty = string.Empty;
                    string filterExpression = " CD_BUDGET = '" + dt_tg.Rows[index]["CD_BUDGET"].ToString().Trim() + "' AND CD_BGACCT = '" + dt_tg.Rows[index]["CD_BGACCT"].ToString().Trim() + "'";
                    if (this._YN_CdBizplan == "1")
                        filterExpression = filterExpression + " AND CD_BIZPLAN = '" + D.GetString(dt_tg.Rows[index]["CD_BIZPLAN"]) + "'";
                    DataRow[] dataRowArray = dataTable1.Select(filterExpression);
                    if (dataRowArray.Length == 0)
                    {
                        DataRow row = dataTable1.NewRow();
                        row["CD_BUDGET"] = dt_tg.Rows[index]["CD_BUDGET"].ToString().Trim();
                        row["NM_BUDGET"] = dt_tg.Rows[index]["NM_BUDGET"].ToString().Trim();
                        row["CD_BGACCT"] = dt_tg.Rows[index]["CD_BGACCT"].ToString().Trim();
                        row["NM_BGACCT"] = dt_tg.Rows[index]["NM_BGACCT"].ToString().Trim();
                        row["CD_BIZPLAN"] = D.GetString(dt_tg.Rows[index]["CD_BIZPLAN"]);
                        row["NM_BIZPLAN"] = D.GetString(dt_tg.Rows[index]["NM_BIZPLAN"]);
                        row["AM"] = this._flexD.CDecimal(dt_tg.Rows[index]["AM"]);
                        row["AM_ORG"] = D.GetDecimal(dt_tg.Rows[index]["AM_ORG"]);
                        dataTable1.Rows.Add(row);
                    }
                    else
                    {
                        dataRowArray[0]["AM"] = this._flexD.CDecimal(dataRowArray[0]["AM"]) + this._flexD.CDecimal(this._flexD.CDecimal(dt_tg.Rows[index]["AM"]));
                        dataRowArray[0]["AM_ORG"] = D.GetDecimal(dataRowArray[0]["AM_ORG"]) + D.GetDecimal(dt_tg.Rows[index]["AM_ORG"]);
                    }
                }
            }
            foreach (DataRow row in dataTable1.Rows)
            {
                DataTable dataTable2 = !(this._구매예산CHK설정FI == "100") ? this._biz.CheckBUDGET(row["CD_BUDGET"].ToString().Trim(), row["CD_BGACCT"].ToString().Trim(), this.dtp발주일자.Text, string.Empty, "000") : this._biz.CheckBUDGET(row["CD_BUDGET"].ToString().Trim(), row["CD_BGACCT"].ToString().Trim(), this.dtp발주일자.Text, D.GetString(row["CD_BIZPLAN"]), "100");
                if (dataTable2.Rows.Count > 0)
                {
                    row["AM_ACTSUM"] = this._flexD.CDecimal(dataTable2.Rows[0]["AM_ACTSUM"]);
                    row["AM_JSUM"] = this._flexD.CDecimal(dataTable2.Rows[0]["AM_JSUM"]);
                    row["TP_BUNIT"] = dataTable2.Rows[0]["TP_BUNIT"].ToString().Trim();
                    if (this._flexD.CDecimal(row["AM_ACTSUM"]) != 0M)
                        row["RT_JSUM"] = (this._flexD.CDecimal(row["AM_JSUM"]) / this._flexD.CDecimal(row["AM_ACTSUM"]) * 100M);
                    row["AM_JAN"] = (this._flexD.CDecimal(row["AM_ACTSUM"]) - this._flexD.CDecimal(row["AM_JSUM"]) - this._flexD.CDecimal(row["AM"]) + D.GetDecimal(row["AM_ORG"]));
                    row["ERROR_MSG"] = dataTable2.Rows[0]["ERROR_MSG"].ToString().Trim();
                }
            }
            return dataTable1;
        }

        private void btn_예산chk_Click(object sender, EventArgs e)
        {
            if (((Form)new P_PU_BUDGET_SUB(this._flexD.DataTable, this.dtp발주일자.Text, "NO_PO")).ShowDialog(this) != DialogResult.OK)
                ;
        }

        private void btn_예산chk내역_Click(object sender, EventArgs e)
        {
            if (this._header.CurrentRow["NO_PO"] == null || this._header.CurrentRow["NO_PO"].ToString() == "" || ((Form)new P_PU_BUDGET_SUB(this._biz.PU_BUDGET_HST_SELECT(this._header.CurrentRow["NO_PO"].ToString()).Tables[0], this.dtp발주일자.Text, "HS_PO")).ShowDialog(this) != DialogResult.OK)
                ;
        }

        private void check_GW(string NO_PR)
        {
            if (this._biz.GetFI_GWDOCU(NO_PR).ToString().Trim() == "999")
                this.btn전자결재.Enabled = true;
            else
                this.btn전자결재.Enabled = false;
        }

        private void btn배부_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.Check() || !this._flexD.HasNormalRow)
                    return;
                decimal DecimalValue1 = this.cur환정보.DecimalValue;
                if (this.m_sEnv_Nego == "100")
                {
                    decimal num1 = D.GetDecimal(this._flexD[1, "AM_EX"]);
                    decimal num2 = D.GetDecimal(this._flexD[1, "AM"]);
                    decimal DecimalValue2 = this.curNEGO금액.DecimalValue;
                    decimal num3 = 0M;
                    decimal num4 = 0M;
                    decimal num5 = 0M;
                    decimal DecimalValue3 = this.cur부가세율.DecimalValue;
                    this._header.CurrentRow["AM_NEGO"] = DecimalValue2;
                    if (DecimalValue1 == 0M)
                        ;
                    int num6;
                    for (num6 = this._flexD.Rows.Fixed; num6 < this._flexD.Rows.Count - 1; ++num6)
                    {
                        decimal num7 = Unit.외화금액(DataDictionaryTypes.PU, (num1 - DecimalValue2) * (D.GetDecimal(this._flexD[num6, "AM_EX"]) / num1));
                        this._flexD[num6, "AM_EX"] = num7;
                        this._flexD[num6, "UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, num7 / D.GetDecimal(this._flexD[num6, "QT_PO_MM"]));
                        this._flexD[num6, "AM"] = Unit.원화금액(DataDictionaryTypes.PU, num7 * this.cur환정보.DecimalValue);
                        this._flexD[num6, "VAT"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flexD[num6, "AM"]) * DecimalValue3 * 0.01M);
                        this._flexD[num6, "AM_TOTAL"] = (D.GetDecimal(this._flexD[num6, "AM"]) + D.GetDecimal(this._flexD[num6, "VAT"]));
                        num3 += num7;
                        num4 += D.GetDecimal(this._flexD[num6, "AM"]);
                        num5 += D.GetDecimal(this._flexD[num6, "VAT"]);
                    }
                    decimal num8 = Unit.외화금액(DataDictionaryTypes.PU, num1 - DecimalValue2 - num3);
                    this._flexD[num6, "AM_EX"] = num8;
                    this._flexD[num6, "UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, num8 / D.GetDecimal(this._flexD[num6, "QT_PO_MM"]));
                    decimal num9 = num2 - DecimalValue2 * this.cur환정보.DecimalValue - num4;
                    this._flexD[num6, "AM"] = num9;
                    decimal num10 = Unit.원화금액(DataDictionaryTypes.PU, (num4 + num9) * DecimalValue3 * 0.01M - num5);
                    this._flexD[num6, "VAT"] = num10;
                    this._flexD[num6, "AM_TOTAL"] = (D.GetDecimal(this._flexD[num6, "AM"]) + D.GetDecimal(this._flexD[num6, "VAT"]));
                }
                else if (Global.MainFrame.ServerKeyCommon == "LUKEN")
                {
                    DataRow[] dataRowArray1 = this._flexD.DataTable.Select("S = 'Y'");
                    if (dataRowArray1 == null || dataRowArray1.Length == 0)
                    {
                        this.ShowMessage(공통메세지.선택된자료가없습니다, new string[0]);
                        return;
                    }
                    decimal DecimalValue4 = this.curNEGO금액.DecimalValue;
                    decimal num11 = 0M;
                    decimal num12 = 0M;
                    decimal num13 = D.GetDecimal(this._flexD.DataTable.Compute("SUM(NUM_USERDEF4_PO)", "S = 'Y'"));
                    this._header.CurrentRow["AM_NEGO"] = DecimalValue4;
                    for (int index = 0; index < dataRowArray1.Length; ++index)
                    {
                        decimal num14 = D.GetDecimal(dataRowArray1[index]["NUM_USERDEF4_PO"]) / num13;
                        dataRowArray1[index]["AM_DISCONUNT"] = index != dataRowArray1.Length - 1 ? Math.Round(DecimalValue4 * num14, MidpointRounding.AwayFromZero) : (DecimalValue4 - num11);
                        num11 += D.GetDecimal(dataRowArray1[index]["AM_DISCONUNT"]);
                        dataRowArray1[index]["AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray1[index]["NUM_USERDEF4_PO"]) - D.GetDecimal(dataRowArray1[index]["AM_DISCONUNT"]));
                        dataRowArray1[index]["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray1[index]["AM"]) / this.cur환정보.DecimalValue);
                        if (index == dataRowArray1.Length - 1)
                        {
                            dataRowArray1[index]["VAT"] = (D.GetDecimal(this._flexD.DataTable.Compute("SUM(AM)", "S = 'Y'")) * this.cur부가세율.DecimalValue * 0.01M - num12);
                        }
                        else
                        {
                            dataRowArray1[index]["VAT"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray1[index]["AM"]) * this.cur부가세율.DecimalValue * 0.01M);
                            num12 += D.GetDecimal(dataRowArray1[index]["VAT"]);
                        }
                        dataRowArray1[index]["AM_TOTAL"] = (D.GetDecimal(dataRowArray1[index]["AM"]) + D.GetDecimal(dataRowArray1[index]["VAT"]));
                        dataRowArray1[index]["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray1[index]["AM"]) / D.GetDecimal(dataRowArray1[index]["QT_PO"]));
                        dataRowArray1[index]["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray1[index]["AM_EX"]) / D.GetDecimal(dataRowArray1[index]["QT_PO"]));
                        dataRowArray1[index]["UM_P"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray1[index]["AM"]) / D.GetDecimal(dataRowArray1[index]["QT_PO_MM"]));
                        dataRowArray1[index]["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray1[index]["AM_EX"]) / D.GetDecimal(dataRowArray1[index]["QT_PO_MM"]));
                    }
                    DataRow[] dataRowArray2 = this._flexD.DataTable.Select("isnull(S,'N') = 'N'");
                    for (int index = 0; index < dataRowArray2.Length; ++index)
                    {
                        decimal num15 = D.GetDecimal(dataRowArray2[index]["RT_PO"]);
                        if (num15 == 0M)
                            num15 = 1M;
                        dataRowArray2[index]["UM_P"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray2[index]["NUM_USERDEF3_PO"]) * this.cur환정보.DecimalValue);
                        dataRowArray2[index]["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray2[index]["NUM_USERDEF3_PO"]) / num15);
                        dataRowArray2[index]["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray2[index]["NUM_USERDEF3_PO"]) / num15);
                        dataRowArray2[index]["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray2[index]["NUM_USERDEF3_PO"]));
                        dataRowArray2[index]["AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray2[index]["NUM_USERDEF4_PO"]));
                        dataRowArray2[index]["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray2[index]["QT_PO_MM"]) * D.GetDecimal(dataRowArray2[index]["NUM_USERDEF3_PO"]));
                        dataRowArray2[index]["VAT"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray2[index]["AM"]) * this.cur부가세율.DecimalValue * 0.01M);
                        dataRowArray2[index]["AM_TOTAL"] = (D.GetDecimal(dataRowArray2[index]["AM"]) + D.GetDecimal(dataRowArray2[index]["VAT"]));
                        dataRowArray2[index]["AM_DISCONUNT"] = 0;
                    }
                    this._flexD.SumRefresh();
                }
                else
                {
                    bool flag = false;
                    StringBuilder stringBuilder = new StringBuilder();
                    string str1 = "품목명\t\t 단가\t";
                    stringBuilder.AppendLine(str1);
                    string str2 = "-".PadRight(80, '-');
                    stringBuilder.AppendLine(str2);
                    for (int index = this._flexD.Rows.Fixed; index < this._flexD.Rows.Count; ++index)
                    {
                        decimal num16 = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(this._flexD[index, "UM_EX_PO"]) * (1M - this.curNEGO금액.DecimalValue * 0.01M));
                        decimal num17 = Convert.ToDecimal(Math.Pow(10.0, Convert.ToDouble(this.curDe.DecimalValue)));
                        decimal num18;
                        if (this.curDe.DecimalValue > 0M)
                        {
                            if (num16 / num17 < 1M)
                            {
                                string str3 = this._flexD[index, "NM_ITEM"].ToString().PadRight(15, ' ') + " " + this._flexD[index, "UM_EX_PO"].ToString().PadRight(15, ' ');
                                stringBuilder.AppendLine(str3);
                                flag = true;
                                continue;
                            }
                            num18 = Unit.외화단가(DataDictionaryTypes.PU, Math.Truncate(num16 / num17) * num17);
                        }
                        else
                            num18 = num16;
                        decimal num19 = D.GetDecimal(this._flexD[index, "RT_PO"]) == 0M ? 1M : D.GetDecimal(this._flexD[index, "RT_PO"]);
                        decimal num20 = D.GetDecimal(this._flexD[index, "RATE_VAT"]) == 0M ? 0M : D.GetDecimal(this._flexD[index, "RATE_VAT"]) / 100M;
                        this._flexD[index, "UM_EX_PO"] = num18;
                        this._flexD[index, "UM_EX"] = (num18 / num19);
                        this._flexD[index, "UM_P"] = (num18 * DecimalValue1);
                        this._flexD[index, "UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(this._flexD[index, "UM_EX"]) * DecimalValue1);
                        this._flexD[index, "AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flexD[index, "UM_EX_PO"]) * D.GetDecimal(this._flexD[index, "QT_PO_MM"]) * DecimalValue1);
                        this._flexD[index, "AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flexD[index, "UM_EX_PO"]) * D.GetDecimal(this._flexD[index, "QT_PO_MM"]));
                        this._flexD[index, "VAT"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flexD[index, "AM"]) * num20);
                        this._flexD[index, "AM_TOTAL"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flexD[index, "AM"]) + D.GetDecimal(this._flexD[index, "VAT"]));
                    }
                    if (flag)
                    {
                        this.ShowDetailMessage("0처리 자리수를 초가하는 품목이 있습니다.▼ 버튼을 눌러서 목록을 확인하세요!", stringBuilder.ToString());
                    }
                }
                this.SUMFunction();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private bool Check()
        {
            string[] strArray = new string[3];
            if (this.m_sEnv_Nego == "100")
            {
                strArray[0] = "Nego금액";
                strArray[1] = "QT_PO_MM";
                strArray[2] = "발주수량";
            }
            else if (Global.MainFrame.ServerKeyCommon == "LUKEN")
            {
                strArray[0] = "할인금액";
                strArray[1] = "NUM_USERDEF4_PO";
                strArray[2] = "기준발주금액";
            }
            else
            {
                strArray[0] = "할인율";
                strArray[1] = "UM_EX_PO";
                strArray[2] = "단가";
            }
            if (this.curNEGO금액.DecimalValue <= 0M)
            {
                this.ShowMessage(공통메세지._은_보다커야합니다, new string[] { this.DD(strArray[0]), "0" });
                return false;
            }
            if (Global.MainFrame.ServerKeyCommon == "LUKEN")
            {
                foreach (DataRow dataRow in this._flexD.DataTable.Select("S = 'Y'"))
                {
                    if (D.GetString(dataRow["FG_POST"]) != "O")
                    {
                        this.ShowMessage("발주 확정/종결 건은 처리할 수 없습니다");
                        return false;
                    }
                    if (D.GetDecimal(D.GetDecimal(dataRow[strArray[1]])) == 0M)
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD(strArray[2]) });
                        return false;
                    }
                }
            }
            else
            {
                for (int index = this._flexD.Rows.Fixed; index < this._flexD.Rows.Count; ++index)
                {
                    if (D.GetString(this._flexD[index, "FG_POST"]) != "O")
                    {
                        this.ShowMessage("발주 확정/종결 건은 처리할 수 없습니다");
                        return false;
                    }
                    if (D.GetDecimal(D.GetDecimal(this._flexD[index, strArray[1]])) == 0M)
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD(strArray[2]) });
                        return false;
                    }
                }
            }
            return true;
        }

        protected void AsahiKasei_Only_Item(int apply_row, DataTable dt)
        {
            this._flexD[apply_row, "QT_LENGTH"] = D.GetDecimal(dt.Rows[0]["QT_LENGTH"]);
            this._flexD[apply_row, "QT_WIDTH"] = (D.GetDecimal(dt.Rows[0]["QT_WIDTH"]) * 1000M);
            this._flexD[apply_row, "QT_AREA"] = (D.GetDecimal(dt.Rows[0]["QT_LENGTH"]) * D.GetDecimal(dt.Rows[0]["QT_WIDTH"]));
            this._flexD[apply_row, "CD_TP"] = D.GetString(dt.Rows[0]["CD_TP"]);
            if (!(D.GetDecimal(this._flexD[apply_row, "QT_PO_MM"]) > 0M))
                return;
            this._flexD[apply_row, "TOTAL_AREA"] = (D.GetDecimal(this._flexD[apply_row, "QT_PO_MM"]) * D.GetDecimal(this._flexD[apply_row, "QT_AREA"]));
        }

        protected void AsahiKasei_Only_ValidateEdit(int apply_row, decimal newvalue, string colname)
        {
            if (D.GetDecimal(this._flexD[apply_row, "QT_AREA"]) <= 0M)
                return;
            switch (colname)
            {
                case "QT_PO_MM":
                    this._flexD[apply_row, "TOTAL_AREA"] = (newvalue * D.GetDecimal(this._flexD[apply_row, "QT_AREA"]));
                    break;
                case "UM_EX_PO":
                    this._flexD[apply_row, "UM_EX_AR"] = (newvalue / D.GetDecimal(this._flexD[apply_row, "QT_AREA"]));
                    break;
                case "AM_EX":
                    this._flexD[apply_row, "UM_EX_AR"] = (D.GetDecimal(this._flexD[apply_row, "UM_EX_PO"]) / D.GetDecimal(this._flexD[apply_row, "QT_AREA"]));
                    break;
                case "UM_EX_AR":
                    this._flexD[apply_row, "UM_EX_AR"] = newvalue;
                    this._flexD[apply_row, "UM_EX_PO"] = (newvalue * D.GetDecimal(this._flexD[apply_row, "QT_AREA"]));
                    this.금액계산(apply_row, D.GetDecimal(this._flexD[apply_row, "UM_EX_PO"]), D.GetDecimal(this._flexD[apply_row, "QT_PO_MM"]), "UM_EX_PO", D.GetDecimal(this._flexD[apply_row, "UM_EX_PO"]));
                    break;
            }
        }

        private void SetWinFood(string strFlag, int row)
        {
            try
            {
                if (!this._flexD.HasNormalRow)
                    return;
                DataRow[] dataRowArray = null;
                if (strFlag == "CD_ITEM")
                {
                    DataTable dataTable = this._biz.AM_OUT(D.GetString(this._flexD[row, "CD_ITEM"]), D.GetString(this._flexD[row, "CD_PLANT"]), this.dtp발주일자.Text);
                    if (dataTable != null && dataTable.Rows.Count > 0)
                        this._flexD[row, "QT_WINFOOD_OUT"] = dataTable.Rows[0]["QT"];
                }
                else
                {
                    DataTable dataTable = this._biz.AM_OUT("", D.GetString(this._header.CurrentRow["CD_PLANT"]), this.dtp발주일자.Text);
                    foreach (DataRow row1 in this._flexD.DataTable.Rows)
                    {
                        if (dataTable != null && dataTable.Rows.Count > 0)
                            dataRowArray = dataTable.Select("CD_ITEM = '" + D.GetString(row1["CD_ITEM"]) + "'");
                        if (dataRowArray != null && dataRowArray.Length > 0)
                            row1["QT_WINFOOD_OUT"] = dataRowArray[0]["QT"];
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void setCol_HIOKI()
        {
            try
            {
                this._flexD.SetCol("CD_PARTNER_SO", "수주처코드", 100, false);
                this._flexD.SetCol("LN_PARTNER_SO", "수주처명", 100, false);
                string[] strArray = new string[] { "CD_USERDEF1",
                                                   "CD_USERDEF2",
                                                   "NM_USERDEF3_PO",
                                                   "DC50_PO",
                                                   "NM_USERDEF1",
                                                   "NM_USERDEF2",
                                                   "TXT_USERDEF1",
                                                   "TXT_USERDEF2",
                                                   "DC4",
                                                   "NM_USERDEF4_PO",
                                                   "NM_USERDEF5" };
                for (int index = 1; index <= strArray.Length; ++index)
                {
                    if (this._flexD.Cols.Contains(strArray[index - 1]))
                    {
                        this._flexD.Cols[strArray[index - 1]].Caption = this.DD("TEXT" + index.ToString());
                        this._flexD.Cols.Move(((RowCol)this._flexD.Cols[strArray[index - 1]]).Index, this._flexD.Cols.Count - 1);
                    }
                    else
                        this._flexD.SetCol(strArray[index - 1], this.DD("TEXT" + index.ToString()), 100, true);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void 부가세계산_쯔바키(DataRow row)
        {
            try
            {
                decimal num1 = 0M;
                decimal num2 = 0M;
                decimal num3 = D.GetDecimal(row["RATE_VAT"]) == 0M ? 0M : D.GetDecimal(row["RATE_VAT"]) / 100M;
                D.GetDecimal(row["QT_PO_MM"]);
                D.GetDecimal(row["UM_EX_PO"]);
                decimal num4 = D.GetDecimal(row["AM_EX"]);
                decimal num5 = D.GetDecimal(this._header.CurrentRow["RT_EXCH"]) == 0M ? 1M : D.GetDecimal(this._header.CurrentRow["RT_EXCH"]);
                bool flag = D.GetString(row["TP_UM_TAX"]) == "001";
                string str = D.GetString(row["FG_TAX"]);
                Calc.GetAmt(num4, num5, str, num3, 모듈.PUR, flag, out num2, out num1);
                row["AM"] = num2;
                row["VAT"] = num1;
                row["AM_TOTAL"] = (num2 + num1);
                this.SUMFunction();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private DataTable get_EXCEL_APP_NO(DataTable dt_Excel)
        {
            string str = string.Empty;
            if (dt_Excel.Columns.Contains("NO_APP") && dt_Excel.Columns.Contains("CD_ITEM_ORIGIN"))
            {
                foreach (DataRow row in dt_Excel.DefaultView.ToTable(true, "CD_ITEM", "NO_APP", "CD_ITEM_ORIGIN").Rows)
                    str = !(D.GetString(row["CD_ITEM_ORIGIN"]) == "") ? str + row["CD_ITEM_ORIGIN"].ToString().Trim() + row["NO_APP"].ToString().Trim() + "|" : str + row["CD_ITEM"].ToString().Trim() + row["NO_APP"].ToString().Trim() + "|";
            }
            string[] pipes = D.StringConvert.GetPipes(str, 100);
            DataTable excelAppNo = null;
            for (int index = 0; index < pipes.Length; ++index)
            {
                DataTable appInfo = this._biz.Get_AppInfo(pipes[index]);
                if (appInfo != null && appInfo.Rows.Count > 0)
                {
                    if (excelAppNo == null)
                        excelAppNo = appInfo.Clone();
                    excelAppNo.Merge(appInfo);
                }
            }
            return excelAppNo;
        }

        private void set_UM_ITEM_KCF(string CD_PARTNER)
        {
            try
            {
                if (!this._flexD.HasNormalRow || D.GetString(this.ctx거래처.CodeValue) == "")
                    return;
                for (int index = this._flexD.Rows.Fixed; index < this._flexD.Rows.Count; ++index)
                {
                    DataSet dataSet = this._biz.ItemInfo_Search(new object[] { D.GetString(this._flexD[index, "CD_ITEM"]),
                                                                               D.GetString(this._flexD[index, "CD_PLANT"]),
                                                                               this.LoginInfo.CompanyCode,
                                                                               this.cbo단가유형.SelectedValue.ToString(),
                                                                               this.cbo환정보.SelectedValue.ToString(),
                                                                               this.dtp발주일자.Text,
                                                                               this.ctx거래처.CodeValue,
                                                                               this.ctx구매그룹.CodeValue,
                                                                               "N",
                                                                               D.GetString(this._flexD[index, "CD_PJT"]),
                                                                               Global.MainFrame.ServerKeyCommon.ToUpper() });
                    if (dataSet != null && dataSet.Tables.Count > 3)
                    {
                        if (dataSet.Tables[1].Rows.Count > 0)
                        {
                            this._flexD[index, "UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(dataSet.Tables[1].Rows[0]["UM_ITEM"]));
                            this.금액계산(index, D.GetDecimal(this._flexD[index, "UM_EX_PO"]), this._flexD.CDecimal(this._flexD[index, "QT_PO_MM"]), "UM_EX_PO", D.GetDecimal(this._flexD[index, "UM_EX_PO"]));
                        }
                        this._flexD[index, "GI_PARTNER"] = D.GetString(this.ctx거래처.CodeValue);
                        this._flexD[index, "LN_PARTNER"] = D.GetString(this.ctx거래처.CodeName);
                        this.SetQtValue(index);
                    }
                }
                this.SUMFunction();
                this._flexD.SumRefresh();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public void SetQT_KMI(DataTable dt, int apply_row)
        {
            try
            {
                string empty1 = string.Empty;
                string empty2 = string.Empty;
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (!dt.Columns.Contains("QT_KMI"))
                        dt.Columns.Add("QT_KMI", typeof(decimal));
                    string str1 = D.GetString(dt.Rows[0]["NO_PO"]);
                    string str2 = D.GetString(dt.Rows[0]["CD_PLANT"]);
                    DataTable qtKmi = this._biz.getQT_KMI(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                         str2,
                                                                         "PO",
                                                                         str1 });
                    if (qtKmi != null && qtKmi.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (D.GetString(row["CD_SL"]) != "" && D.GetString(row["CD_ITEM"]) != "")
                            {
                                DataRow[] dataRowArray = qtKmi.Select("CD_SL = '" + D.GetString(row["CD_SL"]) + "' AND CD_ITEM = '" + D.GetString(row["CD_ITEM"]) + "'");
                                if (dataRowArray != null && dataRowArray.Length > 0)
                                    row["QT_KMI"] = dataRowArray[0]["QT_REMAIN"];
                            }
                        }
                        dt.AcceptChanges();
                    }
                }
                if (dt != null && dt.Rows.Count >= 1 || apply_row == 0 || !(D.GetString(this._flexD[apply_row, "CD_SL"]) != "") || !(D.GetString(this._flexD[apply_row, "CD_ITEM"]) != ""))
                    return;
                DataTable qtKmi1 = this._biz.getQT_KMI(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                      D.GetString(this._flexD[apply_row, "CD_PLANT"]),
                                                                      "APP",
                                                                      "",
                                                                      D.GetString(this._flexD[apply_row, "CD_ITEM"]),
                                                                      D.GetString(this._flexD[apply_row, "CD_SL"]) });
                if (qtKmi1 != null && qtKmi1.Rows.Count > 0)
                    this._flexD[apply_row, "QT_KMI"] = qtKmi1.Rows[0]["QT_REMAIN"];
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public void SetQT_KMI(DataRow[] dr, int row)
        {
            try
            {
                string empty1 = string.Empty;
                string empty2 = string.Empty;
                if (dr == null || dr.Length <= 0 || !(D.GetString(dr[row]["CD_SL"]) != "") || !(D.GetString(dr[row]["CD_ITEM"]) != ""))
                    return;
                DataTable qtKmi = this._biz.getQT_KMI(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                     D.GetString(dr[row]["CD_PLANT"]),
                                                                     "PO",
                                                                     "",
                                                                     D.GetString(dr[row]["CD_ITEM"]),
                                                                     D.GetString(dr[row]["CD_SL"]) });
                if (qtKmi != null && qtKmi.Rows.Count > 0)
                    dr[row]["QT_KMI"] = qtKmi.Rows[0]["QT_REMAIN"];
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public void SetQT_KMI(DataRow dr)
        {
            try
            {
                string empty1 = string.Empty;
                string empty2 = string.Empty;
                if (dr == null || !(D.GetString(dr["CD_SL"]) != "") || !(D.GetString(dr["CD_ITEM"]) != ""))
                    return;
                DataTable qtKmi = this._biz.getQT_KMI(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                     D.GetString(dr["CD_PLANT"]),
                                                                     "PO",
                                                                     "",
                                                                     D.GetString(dr["CD_ITEM"]),
                                                                     D.GetString(dr["CD_SL"]) });
                if (qtKmi != null && qtKmi.Rows.Count > 0)
                    dr["QT_KMI"] = qtKmi.Rows[0]["QT_REMAIN"];
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public void setCD_EXCH_HISF(string CD_TPPO)
        {
            try
            {
                if (!(CD_TPPO == "5000") && !(CD_TPPO == "1120") && !(CD_TPPO == "1130") && !(CD_TPPO == "1140"))
                    return;
                this.cbo환정보.SelectedValue = "001";
                this._header.CurrentRow["CD_EXCH"] = "001";
                this.cbo_NM_EXCH_SelectionChangeCommitted(null, null);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void setCalBoxEa_FDAMK(string strFlag, int row)
        {
            if (D.GetDecimal(this._flexD[row, "NUM_USERDEF3_PO"]) <= 0M)
                return;
            if (strFlag == "NUM_USERDEF1")
                this._flexD[row, "QT_PO_MM"] = (D.GetDecimal(this._flexD[row, "NUM_USERDEF3_PO"]) * D.GetDecimal(this._flexD[row, "NUM_USERDEF1"]) + D.GetDecimal(this._flexD[row, "NUM_USERDEF2"]));
            else if (strFlag == "NUM_USERDEF2")
                this._flexD[row, "QT_PO_MM"] = !(D.GetDecimal(this._flexD[row, "NUM_USERDEF2"]) > D.GetDecimal(this._flexD[row, "NUM_USERDEF3_PO"])) ? (D.GetDecimal(this._flexD[row, "NUM_USERDEF3_PO"]) * D.GetDecimal(this._flexD[row, "NUM_USERDEF1"]) + D.GetDecimal(this._flexD[row, "NUM_USERDEF2"])) : D.GetDecimal(this._flexD[row, "NUM_USERDEF2"]);
            decimal num = Math.Truncate(D.GetDecimal(this._flexD[row, "QT_PO_MM"]) / D.GetDecimal(this._flexD[row, "NUM_USERDEF3_PO"]));
            if (strFlag != "NUM_USERDEF1")
                this._flexD[row, "NUM_USERDEF1"] = num;
            this._flexD[row, "NUM_USERDEF2"] = (D.GetDecimal(this._flexD[row, "QT_PO_MM"]) - num * D.GetDecimal(this._flexD[row, "NUM_USERDEF3_PO"]));
            if (!(strFlag != "QT_PO_MM"))
                return;
            this.금액계산(row, this._flexD.CDecimal(this._flexD[row, "UM_EX_PO"]), this._flexD.CDecimal(this._flexD[row, "QT_PO_MM"]), "QT_PO_MM", this._flexD.CDecimal(this._flexD[row, "QT_PO_MM"]));
        }

        private void calWeight_DHPENG(string strFlag, int row, decimal newValue)
        {
            if (strFlag == "NUM_USERDEF1")
                this._flexD[row, "AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flexD[row, "QT_PO_MM"]) * newValue * D.GetDecimal(this._flexD[row, "WEIGHT"]));
            else if (strFlag == "QT_PO_MM")
                this._flexD[row, "AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flexD[row, "NUM_USERDEF1"]) * newValue * D.GetDecimal(this._flexD[row, "WEIGHT"]));
            this._flexD[row, "AM_EX"] = !(D.GetDecimal(this.cur환정보.Text) != 0M) ? 0 : Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flexD[row, "AM"]) / D.GetDecimal(this.cur환정보.Text));
            this.금액계산(row, this._flexD.CDecimal(this._flexD[row, "UM_EX_PO"]), this._flexD.CDecimal(this._flexD[row, "QT_PO_MM"]), "AM_EX", D.GetDecimal(this._flexD[row, "AM_EX"]));
        }

        protected void set_IV_DT_HOTEL(string cd_partner)
        {
            try
            {
                if (cd_partner == "")
                    return;
                string empty = string.Empty;
                CommonFunction commonFunction = new CommonFunction();
                DataTable dataTable = this._biz.search_partner_HOTEL(cd_partner);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    string str1;
                    if (this._지급예정일통제설정 != "100")
                    {
                        string str2;
                        if (D.GetString(dataTable.Rows[0]["TP_DT_PAY_PREARRANGED"]) == "1")
                        {
                            string str3 = commonFunction.DateAdd(this.dtp발주일자.Text.Substring(0, 6) + "01", "M", 1);
                            str2 = commonFunction.DateAdd(str3, "D", -1);
                        }
                        else
                            str2 = this.dtp발주일자.Text;
                        str1 = commonFunction.DateAdd(str2, "D", (int)Convert.ToInt16(dataTable.Rows[0]["DT_PAY_PREARRANGED"]));
                    }
                    else
                    {
                        str1 = commonFunction.DateAdd(this.dtp발주일자.Text, "M", (int)Convert.ToInt16(D.GetDecimal(dataTable.Rows[0]["TP_PAY_DD"]))).Substring(0, 6) + string.Format("{0:00}", D.GetDecimal(dataTable.Rows[0]["DT_PAY_DD"]));
                        if (D.GetDecimal(str1) <= D.GetDecimal(this.dtp발주일자.Text))
                        {
                            str1 = this.dtp발주일자.Text;
                        }
                        else
                        {
                            string lastDayDateText = commonFunction.GetLastDayDateText(str1);
                            if (D.GetDecimal(str1) > D.GetDecimal(lastDayDateText))
                                str1 = lastDayDateText;
                        }
                    }
                  this.dtp만기일자.Text = str1;
                    this.dtp지급예정일자.Text = str1;
                    this.cbo지급구분.SelectedValue = D.GetString(dataTable.Rows[0]["FG_PAYBILL"]);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void FillPol()
        {
            if (!this._flexD.HasNormalRow)
                return;
            for (int index = 0; index < this._flexD.DataTable.Rows.Count; ++index)
            {
                this._flexD.DataTable.Rows[index]["YN_SUBCON"] = this._header.CurrentRow["YN_SUBCON"];
                this._flexD.DataTable.Rows[index]["FG_TRANS"] = this._header.CurrentRow["FG_TRANS"];
                this._flexD.DataTable.Rows[index]["FG_TPPURCHASE"] = this._header.CurrentRow["FG_TPPURCHASE"];
                this._flexD.DataTable.Rows[index]["YN_AUTORCV"] = this._header.CurrentRow["YN_AUTORCV"];
                this._flexD.DataTable.Rows[index]["YN_RCV"] = this._header.CurrentRow["YN_RCV"];
                this._flexD.DataTable.Rows[index]["YN_RETURN"] = this._header.CurrentRow["YN_RETURN"];
                this._flexD.DataTable.Rows[index]["YN_IMPORT"] = this._header.CurrentRow["YN_IMPORT"];
                this._flexD.DataTable.Rows[index]["YN_ORDER"] = this._header.CurrentRow["YN_ORDER"];
                this._flexD.DataTable.Rows[index]["YN_REQ"] = this._header.CurrentRow["YN_REQ"];
                this._flexD.DataTable.Rows[index]["FG_RCV"] = this._header.CurrentRow["FG_TPRCV"];
                this._flexD.DataTable.Rows[index]["YN_SUBCON"] = this._header.CurrentRow["YN_SUBCON"];
                this._flexD.DataTable.Rows[index]["FG_PURCHASE"] = this._header.CurrentRow["FG_TPPURCHASE"];
            }
        }

        private void FillPol(int i)
        {
            if (!this._flexD.HasNormalRow)
                return;
            this._flexD[i, "FG_TRANS"] = this._header.CurrentRow["FG_TRANS"];
            this._flexD[i, "FG_TPPURCHASE"] = this._header.CurrentRow["FG_TPPURCHASE"];
            this._flexD[i, "YN_AUTORCV"] = this._header.CurrentRow["YN_AUTORCV"];
            this._flexD[i, "YN_RCV"] = this._header.CurrentRow["YN_RCV"];
            this._flexD[i, "YN_RETURN"] = this._header.CurrentRow["YN_RETURN"];
            this._flexD[i, "YN_IMPORT"] = this._header.CurrentRow["YN_IMPORT"];
            this._flexD[i, "YN_ORDER"] = this._header.CurrentRow["YN_ORDER"];
            this._flexD[i, "YN_REQ"] = this._header.CurrentRow["YN_REQ"];
            this._flexD[i, "FG_RCV"] = this._header.CurrentRow["FG_TPRCV"];
            this._flexD[i, "YN_SUBCON"] = this._header.CurrentRow["YN_SUBCON"];
            this._flexD[i, "FG_PURCHASE"] = this._header.CurrentRow["FG_TPPURCHASE"];
            this._flexD[i, "FG_TAX"] = this._header.CurrentRow["FG_TAX"];
            this._flexD[i, "TP_UM_TAX"] = this._header.CurrentRow["TP_UM_TAX"];
        }

        private bool 헤더변경여부
        {
            get
            {
                bool 헤더변경여부 = this._header.GetChanges() != null;
                if (헤더변경여부 && !this._flexD.HasNormalRow)
                    헤더변경여부 = false;
                return 헤더변경여부;
            }
        }

        private bool 추가모드여부 => this._header.JobMode == 0;

        private bool 차수여부
        {
            get
            {
                if (D.GetDecimal(this._header.CurrentRow["NO_HST"]) != 0M && this._flexD.HasNormalRow)
                    return false;
                if (BASIC.GetMAEXC("전자결재-ERP수정여부") == "100")
                {
                    int fiGwdocu = this._biz.GetFI_GWDOCU(this.txt발주번호.Text);
                    if (fiGwdocu != 2 && fiGwdocu != 999)
                        return false;
                }
                return true;
            }
        }

        private bool 전자결재여부
        {
            get
            {
                if (BASIC.GetMAEXC("전자결재-ERP수정여부") == "100")
                {
                    int fiGwdocu = this._biz.GetFI_GWDOCU(this.txt발주번호.Text);
                    if (fiGwdocu != 2 && fiGwdocu != 999)
                        return false;
                }
                return true;
            }
        }

        private bool 요청적용여부 => !(this._header.CurrentRow["TP_PROCESS"].ToString() == "1") || !this._flexD.HasNormalRow;

        private bool 품의적용여부 => !(this._header.CurrentRow["TP_PROCESS"].ToString() == "3") || !this._flexD.HasNormalRow;

        private bool 수주적용여부 => !(this._header.CurrentRow["TP_PROCESS"].ToString() == "4") || !this._flexD.HasNormalRow;

        private bool 의제매입여부(string ps_taxp) => ps_taxp == "27" || ps_taxp == "28" || ps_taxp == "29" || ps_taxp == "30" || ps_taxp == "32" || ps_taxp == "33" || ps_taxp == "34" || ps_taxp == "35" || ps_taxp == "36" || ps_taxp == "40" || ps_taxp == "41" || ps_taxp == "42" || ps_taxp == "48" || ps_taxp == "49" || ps_taxp == "51" || ps_taxp == "52" || ps_taxp == "53" || ps_taxp == "56" || ps_taxp == "57" || ps_taxp == "58" || ps_taxp == "59";

        private bool 자동입고여부체크
        {
            get
            {
                if (this._header.CurrentRow["YN_REQ"].ToString() == "N" && this._header.CurrentRow["FG_TRANS"].ToString() != "001")
                {
                    this.ShowMessage(메세지.거래구분이국내일때만자동의뢰및입고행위가가능합니다, "");
                    return false;
                }
                if (this._header.CurrentRow["YN_AUTORCV"].ToString() == "Y" && this._header.CurrentRow["YN_REQ"].ToString() == "N")
                {
                    if (!this._flexD.HasNormalRow)
                        return false;
                    if (this._flexD.DataTable.Select("Len(CD_SL) = 0").Length > 0)
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD("창고") });
                        return false;
                    }
                }
                return true;
            }
        }

        private void btn단가정보_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexD.HasNormalRow || !this.확정여부() || D.GetString(this._flexD["CD_ITEM"]) == "")
                    return;
                P_PU_ITEM_UM_INFO_SUB pPuItemUmInfoSub = new P_PU_ITEM_UM_INFO_SUB(D.GetString(this._flexD["CD_PLANT"]), D.GetString(this._flexD["CD_ITEM"]), D.GetString(this._flexD["NM_ITEM"]), D.GetString(this._flexD["STND_ITEM"]));
                if (((Form)pPuItemUmInfoSub).ShowDialog() != DialogResult.OK)
                    return;
                (this.btn단가정보).Focus();
                this._flexD["UM_EX_PO"] = pPuItemUmInfoSub.um_info;
                this.금액계산(this._flexD.Row, D.GetDecimal(this._flexD["UM_EX_PO"]), this._flexD.CDecimal(this._flexD["QT_PO_MM"]), "UM_EX_PO", D.GetDecimal(this._flexD["UM_EX_PO"]));
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
            }
        }

        private bool 확정여부()
        {
            try
            {
                if (!this._flexD.HasNormalRow)
                    return false;
                foreach (DataRow dataRow in this._flexD.DataTable.Select())
                {
                    if (dataRow["FG_POST"].ToString().Trim() != "O" || !this.차수여부)
                    {
                        this.ShowMessage(this.DD("발주 확정/종결 건은 처리할 수 없습니다"));
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
                return false;
            }
        }

        private bool Chk금액()
        {
            string str = D.GetString(this.txt발주번호.Text);
            decimal num1 = D.GetDecimal(this._header.CurrentRow["RT_EXCH"]);
            decimal num2 = !(this._header.CurrentRow["FG_TRANS"].ToString() != "001") && !(this._flexD["FG_TAX"].ToString() == string.Empty) ? (this._flexD.CDecimal(this._flexD["RATE_VAT"]) == 0M ? 0M : this._flexD.CDecimal(this._flexD["RATE_VAT"]) / 100M) : this.cur부가세율.DecimalValue / 100M;
            decimal num3 = D.GetDecimal(this._flexIV.DataTable.Compute("MAX(NO_SEQ)", "NO_PO = '" + str + "' AND ISNULL(AM_EX_PUL, 0) = 0"));
            if (this.sPUIV == "100")
            {
                decimal num4 = D.GetDecimal(this._flexIV.DataTable.Compute("SUM(RT_IV)", ""));
                decimal num5 = D.GetDecimal(this._flexD.DataTable.Compute("SUM(AM_EX)", ""));
                decimal num6 = D.GetDecimal(this._flexIV.DataTable.Compute("SUM(AM)", ""));
                if (num4 != 100M)
                {
                    this.ShowMessage("발행비율의 합은 100%여야 합니다.");
                    return false;
                }
                if (num5 != num6)
                {
                    if (this.ShowMessage("발주금액과 기성매입액이 같지않습니다. 자동보정하시겠습니까?", "QY2") == DialogResult.No)
                        return false;
                    DataRow[] dataRowArray = this._flexIV.DataTable.Select("NO_PO = '" + str + "' AND NO_SEQ = " + num3);
                    if (dataRowArray == null || dataRowArray.Length < 1)
                    {
                        this.ShowMessage("자동 보정 가능한 행이 없습니다.");
                        return false;
                    }
                    decimal num9 = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRowArray[0]["AM"]) + (num5 - num6));
                    decimal num10 = Unit.원화금액(DataDictionaryTypes.PU, num9 * num1);
                    decimal num11 = Unit.원화금액(DataDictionaryTypes.PU, num10 * num2);
                    dataRowArray[0]["AM"] = num9;
                    dataRowArray[0]["AM_K"] = num10;
                    dataRowArray[0]["AM_VAT"] = num11;
                    dataRowArray[0]["AM_SUM"] = (num10 + num11);
                    this._flexIV.SumRefresh();
                }
            }
            else if (this.sPUIV == "200")
            {
                for (int index = this._flexD.Rows.Fixed; index < this._flexD.Rows.Count; ++index)
                {
                    decimal num12 = D.GetDecimal(this._flexD[index, "NO_LINE"]);
                    if (D.GetDecimal(this._flexD[index, "AM_EX"]) < D.GetDecimal(this._flexIV.DataTable.Compute("SUM(AM)", "NO_PO = '" + str + "' AND NO_POLINE = " + num12)))
                    {
                        this.ShowMessage(공통메세지._은_보다크거나같아야합니다, new string[] { "발주금액", "매입금액합" });
                        return false;
                    }
                }
            }
            return true;
        }

        public void Reload(string NO_PO)
        {
            try
            {
                DataSet dataSet = this._biz.Search("", "");
                this._header.SetBinding(dataSet.Tables[0], this.m_tab_poh);
                this._header.ClearAndNewRow();
                if (this.sPUSU == "100")
                    this._flexDD.Binding = this._biz.SearchDetail("sDFS", "SDFSD", 0M);
                if (this.sPUIV == "100" || this.sPUIV == "200")
                    this._flexIV.Binding = dataSet.Tables[2];
                this._flexD.Binding = dataSet.Tables[1];
                this.조회(NO_PO, "OK", "");
                if (!this._flexD.HasNormalRow)
                    return;
                this._flexD.Row = this._flexD.FindRow(this.dNO_LINE, this._flexD.Rows.Fixed, ((RowCol)this._flexD.Cols["NO_LINE"]).Index, true);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn적용1_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._flexD.DataTable.Rows == null || this.dtp납기일.Text == "")
                    return;
                foreach (DataRow dataRow in this._flexD.DataTable.Select())
                {
                    dataRow["DT_LIMIT"] = this.dtp납기일.Text;
                    dataRow["DT_PLAN"] = this.dtp납기일.Text;
                }
                this.ShowMessage("적용작업을완료하였습니다");
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btnFG_TPPURCV_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexD.HasNormalRow)
                    return;
                foreach (DataRow row in this._flexD.DataTable.Rows)
                    row["FG_PURCHASE"] = this.cbo매입형태.SelectedValue;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void cur_AM_K_IV_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexD.HasNormalRow)
                    return;
                decimal num1 = 0M;
                int num2 = this._flexD.Rows.Count - this._flexD.Rows.Fixed;
                decimal num3 = D.GetDecimal(this._flexD.DataTable.Compute("SUM(AM)", ""));
                decimal num4 = num3 + 99M;
                decimal num5 = num3 - 99M;
                this.cur부가세액.DecimalValue = Unit.원화금액(DataDictionaryTypes.PU, this.cur공급가액.DecimalValue * this.cur부가세율.DecimalValue * 0.01M);
                if (this.cur공급가액.DecimalValue > num4 || this.cur공급가액.DecimalValue < num5)
                {
                    this.ShowMessage("수정가능금액은 100원 범위 입니다.");
                    this.cur공급가액.DecimalValue = num3;
                }
                else
                {
                    decimal num7 = 0M;
                    for (int index = this._flexD.Rows.Fixed; index < this._flexD.Rows.Count; ++index)
                    {
                        num7 += D.GetDecimal(this._flexD.Rows[index]["AM"]);
                        num1 += D.GetDecimal(this._flexD.Rows[index]["VAT"]);
                    }
                    this._flexD.Rows[num2 + 1]["AM"] = (D.GetDecimal(this._flexD.Rows[num2 + 1]["AM"]) + (this.cur공급가액.DecimalValue - num7));
                    this._flexD.Rows[num2 + 1]["VAT"] = (D.GetDecimal(this._flexD.Rows[num2 + 1]["VAT"]) + (this.cur부가세액.DecimalValue - num1));
                    this._flexD.Rows[num2 + 1]["AM_TOTAL"] = (D.GetDecimal(this._flexD.Rows[num2 + 1]["VAT"]) + D.GetDecimal(this._flexD.Rows[num2 + 1]["AM"]));
                    if (D.GetDecimal(this._flexD.Rows[num2 + 1]["QT_PO"]) != 0M)
                        this._flexD.Rows[num2 + 1]["UM"] = (D.GetDecimal(this._flexD.Rows[num2 + 1]["AM"]) / D.GetDecimal(this._flexD.Rows[num2 + 1]["QT_PO"]));
                    if (D.GetDecimal(this._flexD.Rows[num2 + 1]["QT_PO_MM"]) != 0M)
                        this._flexD.Rows[num2 + 1]["UM_P"] = (D.GetDecimal(this._flexD.Rows[num2 + 1]["AM"]) / D.GetDecimal(this._flexD.Rows[num2 + 1]["QT_PO_MM"]));
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void cur_VAT_TAX_IV_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexD.HasNormalRow)
                    return;
                decimal num1 = 0M;
                int num2 = this._flexD.Rows.Count - this._flexD.Rows.Fixed;
                for (int index = this._flexD.Rows.Fixed; index < this._flexD.Rows.Count; ++index)
                    num1 += D.GetDecimal(this._flexD.Rows[index]["VAT"]);
                this._flexD.Rows[num2 + 1]["VAT"] = (D.GetDecimal(this._flexD.Rows[num2 + 1]["VAT"]) + (this.cur부가세액.DecimalValue - num1));
                this._flexD.Rows[num2 + 1]["AM_TOTAL"] = (D.GetDecimal(this._flexD.Rows[num2 + 1]["VAT"]) + D.GetDecimal(this._flexD.Rows[num2 + 1]["AM"]));
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void MA_Pjt_Setting()
        {
            try
            {
                this.dt공장 = MA.GetCode("MA_B000093");
                this.NUM_USERDEF4 = this.dt공장.Select("CODE = '04'")[0].ItemArray[1].ToString();
                this.NUM_USERDEF5 = this.dt공장.Select("CODE = '05'")[0].ItemArray[1].ToString();
                this.NUM_USERDEF6 = this.dt공장.Select("CODE = '06'")[0].ItemArray[1].ToString();
            }
            catch
            {
            }
        }

        private void Setting_pu_poh_sub()
        {
            if (this._header.CurrentRow["TP_GR"].ToString() == "104" && !this.m_tab_poh.TabPages.Contains(this.tabPage7))
            {
                this.m_tab_poh.TabPages.Add(this.tabPage7);
            }
            else
            {
                if (!(this._header.CurrentRow["TP_GR"].ToString() != "104") || !this.m_tab_poh.TabPages.Contains(this.tabPage7))
                    return;
                this.m_tab_poh.TabPages.Remove(this.tabPage7);
            }
        }

        private bool chk_AM_TSUBAKI()
        {
            if (!this._flexD.HasNormalRow)
                return false;
            for (int index = this._flexD.Rows.Fixed; index < this._flexD.Rows.Count; ++index)
            {
                decimal num1 = D.GetDecimal(this._flexD[index, "QT_PO_MM"]);
                decimal num2 = D.GetDecimal(this._flexD[index, "UM_EX_PO"]);
                decimal num3 = D.GetDecimal(this._flexD[index, "RT_PO"]) == 0M ? 1M : D.GetDecimal(this._flexD[index, "RT_PO"]);
                decimal num4 = Unit.수량(DataDictionaryTypes.PU, num1 * num3);
                decimal num5 = Unit.외화금액(DataDictionaryTypes.PU, num1 * num2);
                if (D.GetDecimal(this._flexD[index, "AM_EX"]) != num5 || D.GetDecimal(this._flexD[index, "QT_PO"]) != num4)
                    return false;
            }
            return true;
        }

        private void 원그리드적용하기()
        {
            this.oneGrid1.UseCustomLayout = true;
            this.oneGrid2.UseCustomLayout = true;
            this.oneGrid3.UseCustomLayout = true;
            this.setNecessaryCondition(new object[] { this.bpPanelControl7.Name,
                                                      this.bpPanelControl8.Name,
                                                      this.bpPanelControl14.Name,
                                                      this.bpPanelControl15.Name,
                                                      this.bpPanelControl16.Name,
                                                      this.bpPanelControl21.Name,
                                                      this.bpPanelControl21.Name,
                                                      this.bpPanelControl22.Name,
                                                      this.bpPanelControl23.Name }, this.oneGrid1, false);
            this.setNecessaryCondition(new object[0], this.oneGrid3, true);
            this.oneGrid1.IsSearchControl = false;
            this.oneGrid2.IsSearchControl = false;
            this.oneGrid3.IsSearchControl = false;
            this.oneGrid1.InitCustomLayout();
            this.oneGrid2.InitCustomLayout();
            this.oneGrid3.InitCustomLayout();

        }

        private void calcAM(int row)
        {
            if (!this.bStandard || !(Global.MainFrame.ServerKey == "SINJINSM") || !(D.GetDecimal(this._flexD[row, "UM_WEIGHT"]) != 0M))
                return;
            switch (D.GetString(this._flexD[row, "SG_TYPE"]))
            {
                case "100":
                case "200":
                case "400":
                    this._flexD[row, "WEIGHT"] = (D.GetDecimal(this._flexD[row, "NUM_STND_ITEM_1"]) * D.GetDecimal(this._flexD[row, "NUM_STND_ITEM_2"]) * D.GetDecimal(this._flexD[row, "NUM_STND_ITEM_3"]) * D.GetDecimal(this._flexD[row, "QT_SG"]) / 1000000M);
                    break;
                case "300":
                    this._flexD[row, "WEIGHT"] = ((D.GetDecimal(this._flexD[row, "NUM_STND_ITEM_1"]) + D.GetDecimal("1.5")) * D.GetDecimal(this._flexD[row, "NUM_STND_ITEM_2"]) * D.GetDecimal(this._flexD[row, "NUM_STND_ITEM_3"]) * D.GetDecimal(this._flexD[row, "QT_SG"]) / 1000000M);
                    break;
            }
            this._flexD[row, "TOT_WEIGHT"] = Math.Round(D.GetDecimal(this._flexD[row, "WEIGHT"]) * D.GetDecimal(this._flexD[row, "QT_PO_MM"]), 1);
            if (D.GetDecimal(this._flexD[row, "TOT_WEIGHT"]) != 0M)
            {
                decimal num = this._flexD.CDecimal(this._flexD[row, "RT_PO"]) == 0M ? 1M : this._flexD.CDecimal(this._flexD[row, "RT_PO"]);
                this._flexD[row, "AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, Math.Round(D.GetDecimal(this._flexD[row, "TOT_WEIGHT"]) * D.GetDecimal(this._flexD[row, "UM_WEIGHT"])));
                this._flexD[row, "UM_EX"] = !(D.GetDecimal(this._flexD[row, "QT_PO_MM"]) != 0M) ? 0 : Unit.외화단가(DataDictionaryTypes.PU, UDecimal.Getdivision(D.GetDecimal(this._flexD[row, "AM_EX"]) / D.GetDecimal(this._flexD[row, "QT_PO_MM"]), num));
                this._flexD[row, "AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flexD[row, "AM_EX"]) * D.GetDecimal(this._header.CurrentRow["RT_EXCH"]));
                this._flexD[row, "UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(this._flexD[row, "UM_EX"]) * num);
                this._flexD["UM_P"] = Unit.원화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(this._flexD[row, "UM_EX"]) * D.GetDecimal(this._header.CurrentRow["RT_EXCH"]));
                this._flexD["UM"] = Unit.원화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(this._flexD[row, "UM_EX"]) / num * D.GetDecimal(this._header.CurrentRow["RT_EXCH"]));
            }
            else
            {
                this._flexD[row, "AM_EX"] = 0;
                this._flexD[row, "UM_EX"] = 0;
                this._flexD[row, "AM"] = 0;
                this._flexD[row, "UM_EX_PO"] = 0;
                this._flexD[row, "UM_P"] = 0;
                this._flexD[row, "UM_"] = 0;
            }
            this.부가세만계산();
        }

        private void calcAM(int row, decimal TOT_WEIGHT)
        {
            if (!this.bStandard || !(Global.MainFrame.ServerKey == "SINJINSM"))
                return;
            if (D.GetDecimal(this._flexD[row, "UM_WEIGHT"]) != 0M)
            {
                decimal num = this._flexD.CDecimal(this._flexD[row, "RT_PO"]) == 0M ? 1M : this._flexD.CDecimal(this._flexD[row, "RT_PO"]);
                this._flexD[row, "AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, Math.Round(TOT_WEIGHT * D.GetDecimal(this._flexD[row, "UM_WEIGHT"])));
                this._flexD[row, "UM_EX"] = !(D.GetDecimal(this._flexD[row, "QT_PO_MM"]) != 0M) ? 0 : Unit.외화단가(DataDictionaryTypes.PU, UDecimal.Getdivision(D.GetDecimal(this._flexD[row, "AM_EX"]) / D.GetDecimal(this._flexD[row, "QT_PO_MM"]), num));
                this._flexD[row, "AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flexD[row, "AM_EX"]) * D.GetDecimal(this._header.CurrentRow["RT_EXCH"]));
                this._flexD[row, "UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(this._flexD[row, "UM_EX"]) * num);
                this._flexD["UM_P"] = Unit.원화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(this._flexD[row, "UM_EX"]) * D.GetDecimal(this._header.CurrentRow["RT_EXCH"]));
                this._flexD["UM"] = Unit.원화단가(DataDictionaryTypes.PU, this._flexD.CDecimal(this._flexD[row, "UM_EX"]) / num * D.GetDecimal(this._header.CurrentRow["RT_EXCH"]));
            }
            this.부가세만계산();
        }

        private void calcRTPO_JONGHAP(int row, string strColName)
        {
            try
            {
                if (strColName == "QT_PO" && D.GetDecimal(this._flexD[row, "RT_PO"]) > 0M && D.GetDecimal(this._flexD[row, "QT_PO_MM"]) == 0M)
                    this._flexD[row, "QT_PO_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(this._flexD[row, "QT_PO"]) / D.GetDecimal(this._flexD[row, "RT_PO"]));
                string str1 = "발주단위";
                if (D.GetString(this._flexD[row, "CD_UNIT_MM"]) != "")
                {
                    DataRow codeInfo = Duzon.ERPU.MF.Common.CodeSearch.GetCodeInfo(MasterSearch.MA_CODEDTL, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                                           "MA_B000004",
                                                                                                                           D.GetString(this._flexD[row, "CD_UNIT_MM"]) });
                    if (codeInfo != null && D.GetString(codeInfo["CD_FLAG1"]) == "2")
                        str1 = "재고단위";
                }
                if (strColName != "RT_PO" && strColName != "QT_PO")
                {
                    SCF scf = new SCF();
                    decimal num = 0M;
                    DataTable dcCalculation = this._biz.GET_DC_CALCULATION(new string[] { D.GetString(this._flexD[row, "CD_PLANT"]),
                                                                                          D.GetString(this._flexD[row, "CLS_L"]),
                                                                                          D.GetString(this._flexD[row, "NUM_STND_ITEM_1"]),
                                                                                          D.GetString(this._flexD[row, "NUM_STND_ITEM_2"]),
                                                                                          D.GetString(this._flexD[row, "NUM_STND_ITEM_3"]),
                                                                                          D.GetString(this._flexD[row, "NUM_STND_ITEM_4"]) });
                    if (dcCalculation != null && dcCalculation.Rows.Count > 0)
                    {
                        string str2 = D.GetString(dcCalculation.Rows[0]["DC_CALCULATION"]);
                        if (strColName != "NUM_STND_ITEM_5")
                        {
                            this._flexD[row, "NUM_STND_ITEM_5"] = D.GetDecimal(dcCalculation.Rows[0]["NUM_STND_ITEM_5"]);
                            num = !(D.GetString(dcCalculation.Rows[0]["NUM_WEIGHT_STEEL"]) != "") ? scf.GetValue(str2, this._flexD.DataTable.Rows[this._flexD.RowSel - this._flexD.Rows.Fixed]) : D.GetDecimal(dcCalculation.Rows[0]["NUM_WEIGHT_STEEL"]);
                        }
                        else
                            num = scf.GetValue(str2, this._flexD.DataTable.Rows[this._flexD.RowSel - this._flexD.Rows.Fixed]);
                    }
                    this._flexD[row, "RT_PO"] = Unit.수량(DataDictionaryTypes.PU, num);
                }
                if (D.GetDecimal(this._flexD[row, "RT_PO"]) == 0M)
                {
                    this._flexD[row, "UM_EX_PO"] = 0;
                    this._flexD[row, "UM_P"] = 0;
                    this._flexD[row, "UM_EX"] = 0;
                    this._flexD[row, "UM"] = 0;
                    this._flexD[row, "AM_EX"] = 0;
                    this._flexD[row, "AM"] = 0;
                    this._flexD[row, "VAT"] = 0;
                    this._flexD[row, "AM_TOTAL"] = 0;
                    this._flexD[row, "QT_PO_MM"] = 0;
                    this._flexD[row, "QT_PO"] = 0;
                }
                if (!(D.GetDecimal(this._flexD[row, "QT_PO_MM"]) > 0M))
                    return;
                if (strColName == "QT_PO")
                    this._flexD[row, "RT_PO"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(this._flexD[row, "QT_PO"]) / D.GetDecimal(this._flexD[row, "QT_PO_MM"]));
                else
                    this._flexD[row, "QT_PO"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(this._flexD[row, "QT_PO_MM"]) * D.GetDecimal(this._flexD[row, "RT_PO"]));
                if (str1 == "재고단위")
                {
                    this._flexD[row, "AM_EX"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(this._flexD[row, "QT_PO"]) * D.GetDecimal(this._flexD[row, "UM_EX"]));
                    this.금액계산(row, D.GetDecimal(this._flexD[row, "UM_EX_PO"]), D.GetDecimal(this._flexD[row, "QT_PO_MM"]), "AM_EX", D.GetDecimal(this._flexD[row, "AM_EX"]));
                }
                else
                    this.금액계산(row, D.GetDecimal(this._flexD[row, "UM_EX_PO"]), D.GetDecimal(this._flexD[row, "QT_PO_MM"]), "UM_EX_PO", D.GetDecimal(this._flexD[row, "UM_EX_PO"]));
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void SetTppoAfter(string strCDTPPO, string strNMTPPO)
        {
            try
            {
                this.ctx발주형태.CodeValue = strCDTPPO;
                this.ctx발주형태.CodeName = strNMTPPO;
                this._header.CurrentRow["CD_TPPO"] = strCDTPPO;
                if (!(strCDTPPO != ""))
                    return;
                DataRow tppo = BASIC.GetTPPO(strCDTPPO);
                this._header.CurrentRow["FG_TRANS"] = tppo["FG_TRANS"];
                this._header.CurrentRow["FG_TPRCV"] = tppo["FG_TPRCV"];
                this._header.CurrentRow["FG_TPPURCHASE"] = tppo["FG_TPPURCHASE"];
                this._header.CurrentRow["YN_AUTORCV"] = tppo["YN_AUTORCV"];
                this._header.CurrentRow["YN_RCV"] = tppo["YN_RCV"];
                this._header.CurrentRow["YN_RETURN"] = tppo["YN_RETURN"];
                this._header.CurrentRow["YN_SUBCON"] = tppo["YN_SUBCON"];
                this._header.CurrentRow["YN_IMPORT"] = tppo["YN_IMPORT"];
                this._header.CurrentRow["YN_ORDER"] = tppo["YN_ORDER"];
                this._header.CurrentRow["YN_REQ"] = tppo["YN_REQ"];
                this._header.CurrentRow["YN_AM"] = tppo["YN_AM"];
                this._header.CurrentRow["NM_TRANS"] = tppo["NM_TRANS"];
                this._header.CurrentRow["FG_TAX"] = tppo["FG_TAX"];
                this._header.CurrentRow["TP_GR"] = tppo["TP_GR"];
                this._header.CurrentRow["CD_CC_TPPO"] = tppo["CD_CC"];
                this._header.CurrentRow["NM_CC_TPPO"] = tppo["NM_CC"];
                this.거래구분(D.GetString(this._header.CurrentRow["FG_TRANS"]), D.GetString(this._header.CurrentRow["FG_TAX"]));
                this.Setting_pu_poh_sub();
                if (this.m_tab_poh.TabPages.Contains(this.tabPage7))
                {
                    this.dtp만기일자.Text = Global.MainFrame.GetStringToday;
                    this.dtp지급예정일자.Text = Global.MainFrame.GetStringToday;
                    this.dtp매입일자.Text = Global.MainFrame.GetStringToday;
                    this.cbo지급구분.SelectedValue = "";
                    this.cbo전표유형.SelectedValue = "";
                }
                if (MA.ServerKey(false, new string[1] { "HISF" }))
                    this.setCD_EXCH_HISF(strCDTPPO);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void SetPurgrpAfter(string strCDPURGRP, string strNMPURGRP)
        {
            try
            {
                this.ctx구매그룹.CodeValue = strCDPURGRP;
                this.ctx구매그룹.CodeName = strNMPURGRP;
                this._header.CurrentRow["CD_PURGRP"] = strCDPURGRP;
                if (strCDPURGRP != "")
                {
                    DataTable cdCcCodeSearch = this._biz.GetCD_CC_CodeSearch(strCDPURGRP);
                    if (cdCcCodeSearch != null && cdCcCodeSearch.Rows.Count > 0)
                    {
                        this._header.CurrentRow["PURGRP_NO_TEL"] = D.GetString(cdCcCodeSearch.Rows[0]["NO_TEL"]);
                        this._header.CurrentRow["PURGRP_NO_FAX"] = D.GetString(cdCcCodeSearch.Rows[0]["NO_FAX"]);
                        this._header.CurrentRow["PURGRP_E_MAIL"] = D.GetString(cdCcCodeSearch.Rows[0]["E_MAIL"]);
                    }
                    this._header.CurrentRow["PO_PRICE"] = "N";
                    DataTable dataTable = Global.MainFrame.FillDataTable(" SELECT O.PO_PRICE    FROM MA_PURGRP G LEFT OUTER JOIN MA_PURORG O      ON   G.CD_COMPANY = O.CD_COMPANY     AND   G.CD_PURORG  = O.CD_PURORG  WHERE G.CD_COMPANY = '" + this.LoginInfo.CompanyCode + "'  AND G.CD_PURGRP  = '" + strCDPURGRP + "'");
                    if (dataTable.Rows.Count > 0 && dataTable.Rows[0]["PO_PRICE"] != DBNull.Value && dataTable.Rows[0]["PO_PRICE"].ToString().Trim() != string.Empty)
                        this._header.CurrentRow["PO_PRICE"] = dataTable.Rows[0]["PO_PRICE"].ToString().Trim();
                    this.SetCC(0, strCDPURGRP);
                }
                else
                {
                    this._header.CurrentRow["PURGRP_NO_TEL"] = "";
                    this._header.CurrentRow["PURGRP_NO_FAX"] = "";
                    this._header.CurrentRow["PURGRP_E_MAIL"] = "";
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
    }
}

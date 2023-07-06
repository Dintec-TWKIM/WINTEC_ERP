using System;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;

using Duzon.Common.Util;
using Duzon.Common.Forms;
using Duzon.Common.Controls;
using Duzon.Common.Forms.Help;

using C1.Win.C1FlexGrid;
using Dass.FlexGrid;

using System.Diagnostics;


namespace pur
{
    /// <summary>
    /// 작성자 : 김대영, 김대영
    /// 작성일 : 
    /// 수정일 : 2004-02-18 
    /// 모듈 : 구매자재
    /// 서브모듈 : 발주관리
    /// 페이지명 : 발주등록(공장)
    /// 수정자 : 유지영 (2006-08-28)
    /// </summary>
    public class P_PU_PO_REG2_OLD : Duzon.Common.Forms.PageBase
    {

        #region ♣ 멤버필드

        #region -> 멤버필드(일반)

        private Duzon.Common.Controls.PanelExt panel9;
        private Duzon.Common.Controls.LabelExt lb_NM_PARTNER;
        private Duzon.Common.Controls.TextBoxExt tb_NO_PO;
        private Duzon.Common.Controls.LabelExt lb_NM_PURGRP;
        private Duzon.Common.Controls.LabelExt ib_NO_EMP;
        private Duzon.Common.Controls.LabelExt lb_NO_PO;
        private Duzon.Common.Controls.LabelExt lb_DT_PO;
        private Duzon.Common.Controls.LabelExt lb_FG_PAYMENT;
        private Duzon.Common.Controls.LabelExt lb_FG_TAX;
        private Duzon.Common.Controls.LabelExt lb_FG_UM;
        private Duzon.Common.Controls.LabelExt lb_FG_PO_TR;
        private Duzon.Common.Controls.LabelExt lb_TP_TAX;
        private Duzon.Common.Controls.LabelExt lb_NM_EXCH;
        private Duzon.Common.Controls.CurrencyTextBox tb_NM_EXCH;
        private Duzon.Common.Controls.LabelExt lb_CD_PJT;
        private Duzon.Common.Controls.CurrencyTextBox tb_TAX;
        private Duzon.Common.Controls.TextBoxExt tb_DC;
        private Duzon.Common.Controls.LabelExt lb_DC;
        private Duzon.Common.Controls.RoundedButton btn_ITEM_EXP;
        private Duzon.Common.Controls.RoundedButton btn_RE_PJT;
        private Duzon.Common.Controls.RoundedButton btn_RE_PR;
        private System.Windows.Forms.ImageList imageList1;
        private Duzon.Common.Controls.PanelExt panel1;
        private Duzon.Common.Controls.PanelExt panel2;
        private Duzon.Common.Controls.PanelExt panel3;
        private Duzon.Common.Controls.PanelExt panel11;
        private Duzon.Common.Controls.PanelExt panel12;
        private Duzon.Common.Controls.PanelExt panel13;
        private Duzon.Common.Controls.PanelExt panel14;
        private Duzon.Common.Controls.PanelExt panel18;
        private System.ComponentModel.IContainer components;


        private Duzon.Common.Controls.RadioButtonExt rbtn_PRI;
        private Duzon.Common.Controls.RadioButtonExt rbtn_All;
        private Duzon.Common.Controls.LabelExt lb_Tax_Type;
        private System.Data.DataSet ds_Ty1;
        private System.Data.DataTable dataTable1;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn3;
        private System.Data.DataColumn dataColumn4;
        private System.Data.DataColumn dataColumn5;
        private System.Data.DataColumn dataColumn6;
        private System.Data.DataColumn dataColumn7;
        private System.Data.DataColumn dataColumn8;
        private System.Data.DataColumn dataColumn9;
        private System.Data.DataColumn dataColumn10;
        private System.Data.DataColumn dataColumn11;
        private System.Data.DataColumn dataColumn12;
        private System.Data.DataColumn dataColumn13;
        private System.Data.DataColumn dataColumn14;
        private System.Data.DataColumn dataColumn15;
        private System.Data.DataColumn dataColumn16;
        private System.Data.DataColumn dataColumn17;
        private System.Data.DataColumn dataColumn18;
        private System.Data.DataColumn dataColumn19;
        private System.Data.DataColumn dataColumn20;
        private System.Data.DataColumn dataColumn21;
        private System.Data.DataColumn dataColumn22;
        private System.Data.DataColumn dataColumn23;
        private System.Data.DataColumn dataColumn24;
        private System.Data.DataColumn dataColumn25;
        private System.Data.DataColumn dataColumn26;
        private System.Data.DataColumn dataColumn27;
        private System.Data.DataColumn dataColumn28;
        private System.Data.DataColumn dataColumn29;
        private System.Data.DataColumn dataColumn30;
        private System.Data.DataColumn dataColumn31;
        private System.Data.DataColumn dataColumn32;
        private System.Data.DataColumn dataColumn33;
        private System.Data.DataColumn dataColumn34;
        private System.Data.DataColumn dataColumn35;
        private System.Data.DataColumn dataColumn36;
        private Duzon.Common.Controls.LabelExt lb_TAX_RATE;
        private System.Data.DataColumn dataColumn37;
        private System.Data.DataColumn dataColumn38;
        private System.Data.DataColumn dataColumn39;
        private System.Data.DataColumn dataColumn40;
        private System.Data.DataColumn dataColumn41;
        private System.Data.DataColumn dataColumn42;
        private System.Data.DataColumn dataColumn43;
        private System.Data.DataColumn dataColumn44;
        private System.Data.DataColumn dataColumn45;
        private System.Data.DataColumn dataColumn46;
        private System.Data.DataColumn dataColumn47;
        private System.Data.DataColumn dataColumn48;
        private System.Data.DataColumn dataColumn49;
        private System.Data.DataColumn dataColumn50;
        private System.Data.DataColumn dataColumn51;
        private System.Data.DataColumn dataColumn52;
        private Duzon.Common.Controls.PanelExt m_pnlGrid;
        private Duzon.Common.Controls.RoundedButton btn_delete;
        private Duzon.Common.Controls.RoundedButton btn_insert;
        private System.Data.DataTable dataTable2;
        private System.Data.DataColumn dataColumn53;
        private System.Data.DataColumn dataColumn54;

        private Duzon.Common.Controls.PanelExt panel23;
        private Duzon.Common.Controls.TextBoxExt tb_NM_TRANS;
        private Duzon.Common.Controls.RoundedButton btn_CM_INV;
        private Duzon.Common.Controls.PanelExt panel5;
        private Duzon.Common.Controls.CurrencyTextBox tb_QT_PO;
        private Duzon.Common.Controls.LabelExt lb_qt_po;
        private Duzon.Common.Controls.CurrencyTextBox tb_QT_INV;
        private Duzon.Common.Controls.LabelExt lb_qt_inv;
        private Duzon.Common.Controls.CurrencyTextBox tb_QT_REQ;
        private Duzon.Common.Controls.LabelExt lb_qt_req;
        private Duzon.Common.Controls.CurrencyTextBox tb_QT_ATP;
        private Duzon.Common.Controls.LabelExt lb_qt_atp;
        private System.Data.DataTable dataTable3;
        private System.Data.DataColumn dataColumn55;
        private System.Data.DataColumn dataColumn56;
        private System.Data.DataColumn dataColumn57;
        private System.Data.DataColumn dataColumn58;
        private System.Data.DataColumn dataColumn59;
        private System.Data.DataColumn dataColumn60;
        private System.Data.DataColumn dataColumn61;
        private System.Data.DataColumn dataColumn62;
        private System.Data.DataColumn dataColumn63;
        private System.Data.DataColumn dataColumn64;
        private System.Data.DataColumn dataColumn65;
        private System.Data.DataColumn dataColumn66;
        private System.Data.DataColumn dataColumn67;
        private System.Data.DataColumn dataColumn68;
        private System.Data.DataColumn dataColumn69;
        private System.Data.DataColumn dataColumn70;
        private System.Data.DataTable dataTable4;
        private System.Data.DataColumn dataColumn71;
        private System.Data.DataColumn dataColumn72;
        private System.Data.DataColumn dataColumn73;
        private System.Data.DataColumn dataColumn74;
        private System.Data.DataColumn dataColumn75;
        private System.Data.DataColumn dataColumn76;
        private System.Data.DataColumn dataColumn77;
        private System.Data.DataColumn dataColumn78;
        private System.Data.DataColumn dataColumn79;
        private System.Data.DataColumn dataColumn80;
        private System.Data.DataColumn dataColumn81;
        private System.Data.DataColumn dataColumn82;
        private System.Data.DataColumn dataColumn83;
        private System.Data.DataColumn dataColumn84;
        private System.Data.DataColumn dataColumn85;
        private System.Data.DataColumn dataColumn86;
        private System.Data.DataColumn dataColumn87;
        private System.Data.DataColumn dataColumn88;
        private System.Data.DataColumn dataColumn89;
        private System.Data.DataColumn dataColumn90;
        private System.Data.DataColumn dataColumn91;
        private System.Data.DataColumn dataColumn92;
        private System.Data.DataColumn dataColumn93;
        private System.Data.DataTable dataTable5;
        private System.Data.DataColumn dataColumn94;
        private System.Data.DataColumn dataColumn95;
        private System.Data.DataColumn dataColumn96;
        private System.Data.DataColumn dataColumn97;
        private System.Data.DataColumn dataColumn98;
        private System.Data.DataColumn dataColumn99;
        private System.Data.DataColumn dataColumn100;
        private System.Data.DataColumn dataColumn101;
        private System.Data.DataColumn dataColumn102;
        private Duzon.Common.Controls.LabelExt lb_NM_PLANT;
        private System.Data.DataColumn dataColumn103;
        private System.Data.DataColumn dataColumn104;
        private Duzon.Common.Controls.RoundedButton btn_RE_APP;
        private System.Data.DataTable dataTable6;
        private System.Data.DataColumn dataColumn105;
        private System.Data.DataColumn dataColumn106;
        private System.Data.DataColumn dataColumn107;
        private Duzon.Common.Controls.DropDownComboBox cbo_NM_EXCH;
        private Duzon.Common.Controls.DropDownComboBox cbo_FG_UM;
        private Duzon.Common.Controls.DropDownComboBox cbo_PAYment;
        private Duzon.Common.Controls.DropDownComboBox cbo_FG_TAX;
        private Duzon.Common.Controls.DropDownComboBox cbo_TP_TAX;
        private Duzon.Common.Controls.DropDownComboBox cbo_CD_PLANT;
        private Duzon.Common.Controls.DatePicker tb_DT_PO;

        #endregion

        #region -> 멤버필드(주요)

        //그리드
        private Dass.FlexGrid.FlexGrid _flex;

        private double _db_am = 0;
        private double _db_amk = 0;
        private double _db_vat = 0;

        //페인팅 관련
        private bool _isPainted = false;


        //프로세스구분(요청적용에서 받으면 1 , 단지 추가이면 2)
        string _tp_process = "2";

        private string _dtsUpdate = "";

        private bool _isChagePossible = true;

        DataSet _dsCombo = new DataSet();


        //페이지 상태 
        private string _page_state;

        // 확정여부에서의 001(미정) 값
        private string _ComfirmState;

        //// 담당부서(담당자의 부서)
        //private string _cddept="";


        // PopUp 바로가기 변수들
        private string _nopoPop = "";

        // 현황의 (팝업)에서 오면 TRUE
        private bool is_PopUp = false;

        private string strFG_RCV = string.Empty;

        pur.CDT_PU_RCV gc_Rcvl = new pur.CDT_PU_RCV();
        private Duzon.Common.BpControls.BpCodeTextBox tb_NM_PARTNER;
        private Duzon.Common.BpControls.BpCodeTextBox tb_NM_PURGRP;
        private Duzon.Common.BpControls.BpCodeTextBox tb_NO_EMP;
        private Duzon.Common.BpControls.BpCodeTextBox tb_FG_PO_TR;
        private Duzon.Common.BpControls.BpCodeTextBox tb_CD_PJT;
        private System.Data.DataColumn dataColumn108;
        private PanelExt m_gridTmp;
        private TableLayoutPanel tableLayoutPanel1;
        private PanelExt panelExt1;
        private TextBoxExt tb_CD_DEPT;
        private LabelExt lb_CD_DEPT;
        pur.CDT_PU_RCVH gc_Rcvh = new pur.CDT_PU_RCVH();

        #endregion

        #endregion

        #region ♣ 생성자/소멸자

        #region -> 생성자
        /// <summary>
        /// 생성자
        /// </summary>
        public P_PU_PO_REG2_OLD()
        {
            // 이 호출은 Windows.Forms Form 디자이너에 필요합니다.  // RT_PO
            InitializeComponent();

            // TODO: InitForm을 호출한 다음 초기화 작업을 추가합니다.
            this.Load += new System.EventHandler(Page_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(Page_Paint);


        }
        /// <summary>
        /// 생성자( 바로오기로 호출 ) 
        /// </summary>
        /// <param name="ps_nopo"></param>
        public P_PU_PO_REG2_OLD(string ps_nopo)
        {
            // 이 호출은 Windows.Forms Form 디자이너에 필요합니다.  // RT_PO
            InitializeComponent();

            _nopoPop = ps_nopo;
            is_PopUp = true;
            // TODO: InitForm을 호출한 다음 초기화 작업을 추가합니다.
            this.Load += new System.EventHandler(Page_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(Page_Paint);
            base.AddAutoAnchorControl(this, m_pnlGrid, ControlPositionType.Single);


        }


        #endregion

        #region -> 소멸자
        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Component Designer generated code
        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_PU_PO_REG2_OLD));
            this.panel9 = new Duzon.Common.Controls.PanelExt();
            this.tb_CD_PJT = new Duzon.Common.BpControls.BpCodeTextBox();
            this.tb_FG_PO_TR = new Duzon.Common.BpControls.BpCodeTextBox();
            this.tb_NO_EMP = new Duzon.Common.BpControls.BpCodeTextBox();
            this.tb_NM_PURGRP = new Duzon.Common.BpControls.BpCodeTextBox();
            this.tb_NM_PARTNER = new Duzon.Common.BpControls.BpCodeTextBox();
            this.tb_NM_TRANS = new Duzon.Common.Controls.TextBoxExt();
            this.panel18 = new Duzon.Common.Controls.PanelExt();
            this.panel13 = new Duzon.Common.Controls.PanelExt();
            this.panel14 = new Duzon.Common.Controls.PanelExt();
            this.panel12 = new Duzon.Common.Controls.PanelExt();
            this.panel11 = new Duzon.Common.Controls.PanelExt();
            this.panel3 = new Duzon.Common.Controls.PanelExt();
            this.lb_NM_PARTNER = new Duzon.Common.Controls.LabelExt();
            this.ib_NO_EMP = new Duzon.Common.Controls.LabelExt();
            this.lb_FG_UM = new Duzon.Common.Controls.LabelExt();
            this.lb_TP_TAX = new Duzon.Common.Controls.LabelExt();
            this.lb_CD_PJT = new Duzon.Common.Controls.LabelExt();
            this.tb_NO_PO = new Duzon.Common.Controls.TextBoxExt();
            this.tb_NM_EXCH = new Duzon.Common.Controls.CurrencyTextBox();
            this.tb_TAX = new Duzon.Common.Controls.CurrencyTextBox();
            this.tb_DC = new Duzon.Common.Controls.TextBoxExt();
            this.panel2 = new Duzon.Common.Controls.PanelExt();
            this.lb_DT_PO = new Duzon.Common.Controls.LabelExt();
            this.lb_NM_PURGRP = new Duzon.Common.Controls.LabelExt();
            this.lb_NM_EXCH = new Duzon.Common.Controls.LabelExt();
            this.lb_FG_TAX = new Duzon.Common.Controls.LabelExt();
            this.lb_Tax_Type = new Duzon.Common.Controls.LabelExt();
            this.panel1 = new Duzon.Common.Controls.PanelExt();
            this.lb_NO_PO = new Duzon.Common.Controls.LabelExt();
            this.lb_DC = new Duzon.Common.Controls.LabelExt();
            this.lb_NM_PLANT = new Duzon.Common.Controls.LabelExt();
            this.lb_FG_PO_TR = new Duzon.Common.Controls.LabelExt();
            this.lb_FG_PAYMENT = new Duzon.Common.Controls.LabelExt();
            this.lb_TAX_RATE = new Duzon.Common.Controls.LabelExt();
            this.panel23 = new Duzon.Common.Controls.PanelExt();
            this.rbtn_PRI = new Duzon.Common.Controls.RadioButtonExt();
            this.rbtn_All = new Duzon.Common.Controls.RadioButtonExt();
            this.cbo_NM_EXCH = new Duzon.Common.Controls.DropDownComboBox();
            this.cbo_FG_UM = new Duzon.Common.Controls.DropDownComboBox();
            this.cbo_PAYment = new Duzon.Common.Controls.DropDownComboBox();
            this.cbo_FG_TAX = new Duzon.Common.Controls.DropDownComboBox();
            this.cbo_TP_TAX = new Duzon.Common.Controls.DropDownComboBox();
            this.cbo_CD_PLANT = new Duzon.Common.Controls.DropDownComboBox();
            this.tb_DT_PO = new Duzon.Common.Controls.DatePicker();
            this.m_pnlGrid = new Duzon.Common.Controls.PanelExt();
            this.btn_RE_PJT = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn_RE_PR = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn_ITEM_EXP = new Duzon.Common.Controls.RoundedButton(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ds_Ty1 = new System.Data.DataSet();
            this.dataTable1 = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.dataColumn5 = new System.Data.DataColumn();
            this.dataColumn6 = new System.Data.DataColumn();
            this.dataColumn7 = new System.Data.DataColumn();
            this.dataColumn8 = new System.Data.DataColumn();
            this.dataColumn9 = new System.Data.DataColumn();
            this.dataColumn10 = new System.Data.DataColumn();
            this.dataColumn11 = new System.Data.DataColumn();
            this.dataColumn12 = new System.Data.DataColumn();
            this.dataColumn13 = new System.Data.DataColumn();
            this.dataColumn14 = new System.Data.DataColumn();
            this.dataColumn15 = new System.Data.DataColumn();
            this.dataColumn16 = new System.Data.DataColumn();
            this.dataColumn17 = new System.Data.DataColumn();
            this.dataColumn18 = new System.Data.DataColumn();
            this.dataColumn19 = new System.Data.DataColumn();
            this.dataColumn20 = new System.Data.DataColumn();
            this.dataColumn21 = new System.Data.DataColumn();
            this.dataColumn22 = new System.Data.DataColumn();
            this.dataColumn23 = new System.Data.DataColumn();
            this.dataColumn24 = new System.Data.DataColumn();
            this.dataColumn25 = new System.Data.DataColumn();
            this.dataColumn26 = new System.Data.DataColumn();
            this.dataColumn27 = new System.Data.DataColumn();
            this.dataColumn28 = new System.Data.DataColumn();
            this.dataColumn29 = new System.Data.DataColumn();
            this.dataColumn30 = new System.Data.DataColumn();
            this.dataColumn31 = new System.Data.DataColumn();
            this.dataColumn32 = new System.Data.DataColumn();
            this.dataColumn33 = new System.Data.DataColumn();
            this.dataColumn34 = new System.Data.DataColumn();
            this.dataColumn35 = new System.Data.DataColumn();
            this.dataColumn36 = new System.Data.DataColumn();
            this.dataColumn37 = new System.Data.DataColumn();
            this.dataColumn38 = new System.Data.DataColumn();
            this.dataColumn39 = new System.Data.DataColumn();
            this.dataColumn40 = new System.Data.DataColumn();
            this.dataColumn41 = new System.Data.DataColumn();
            this.dataColumn42 = new System.Data.DataColumn();
            this.dataColumn43 = new System.Data.DataColumn();
            this.dataColumn44 = new System.Data.DataColumn();
            this.dataColumn45 = new System.Data.DataColumn();
            this.dataColumn46 = new System.Data.DataColumn();
            this.dataColumn47 = new System.Data.DataColumn();
            this.dataColumn48 = new System.Data.DataColumn();
            this.dataColumn49 = new System.Data.DataColumn();
            this.dataColumn50 = new System.Data.DataColumn();
            this.dataColumn51 = new System.Data.DataColumn();
            this.dataColumn52 = new System.Data.DataColumn();
            this.dataColumn66 = new System.Data.DataColumn();
            this.dataColumn67 = new System.Data.DataColumn();
            this.dataColumn68 = new System.Data.DataColumn();
            this.dataColumn69 = new System.Data.DataColumn();
            this.dataColumn103 = new System.Data.DataColumn();
            this.dataColumn104 = new System.Data.DataColumn();
            this.dataColumn108 = new System.Data.DataColumn();
            this.dataTable2 = new System.Data.DataTable();
            this.dataColumn53 = new System.Data.DataColumn();
            this.dataColumn54 = new System.Data.DataColumn();
            this.dataTable3 = new System.Data.DataTable();
            this.dataColumn55 = new System.Data.DataColumn();
            this.dataColumn56 = new System.Data.DataColumn();
            this.dataColumn57 = new System.Data.DataColumn();
            this.dataColumn58 = new System.Data.DataColumn();
            this.dataColumn59 = new System.Data.DataColumn();
            this.dataColumn60 = new System.Data.DataColumn();
            this.dataColumn61 = new System.Data.DataColumn();
            this.dataColumn62 = new System.Data.DataColumn();
            this.dataColumn63 = new System.Data.DataColumn();
            this.dataColumn64 = new System.Data.DataColumn();
            this.dataColumn65 = new System.Data.DataColumn();
            this.dataColumn70 = new System.Data.DataColumn();
            this.dataTable4 = new System.Data.DataTable();
            this.dataColumn71 = new System.Data.DataColumn();
            this.dataColumn72 = new System.Data.DataColumn();
            this.dataColumn73 = new System.Data.DataColumn();
            this.dataColumn74 = new System.Data.DataColumn();
            this.dataColumn75 = new System.Data.DataColumn();
            this.dataColumn76 = new System.Data.DataColumn();
            this.dataColumn77 = new System.Data.DataColumn();
            this.dataColumn78 = new System.Data.DataColumn();
            this.dataColumn79 = new System.Data.DataColumn();
            this.dataColumn80 = new System.Data.DataColumn();
            this.dataColumn81 = new System.Data.DataColumn();
            this.dataColumn82 = new System.Data.DataColumn();
            this.dataColumn83 = new System.Data.DataColumn();
            this.dataColumn84 = new System.Data.DataColumn();
            this.dataColumn85 = new System.Data.DataColumn();
            this.dataColumn86 = new System.Data.DataColumn();
            this.dataColumn87 = new System.Data.DataColumn();
            this.dataColumn88 = new System.Data.DataColumn();
            this.dataColumn89 = new System.Data.DataColumn();
            this.dataColumn90 = new System.Data.DataColumn();
            this.dataColumn91 = new System.Data.DataColumn();
            this.dataColumn92 = new System.Data.DataColumn();
            this.dataColumn93 = new System.Data.DataColumn();
            this.dataColumn102 = new System.Data.DataColumn();
            this.dataColumn107 = new System.Data.DataColumn();
            this.dataTable5 = new System.Data.DataTable();
            this.dataColumn94 = new System.Data.DataColumn();
            this.dataColumn95 = new System.Data.DataColumn();
            this.dataColumn96 = new System.Data.DataColumn();
            this.dataColumn97 = new System.Data.DataColumn();
            this.dataColumn98 = new System.Data.DataColumn();
            this.dataColumn99 = new System.Data.DataColumn();
            this.dataColumn100 = new System.Data.DataColumn();
            this.dataColumn101 = new System.Data.DataColumn();
            this.dataTable6 = new System.Data.DataTable();
            this.dataColumn105 = new System.Data.DataColumn();
            this.dataColumn106 = new System.Data.DataColumn();
            this.btn_delete = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn_insert = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn_CM_INV = new Duzon.Common.Controls.RoundedButton(this.components);
            this.panel5 = new Duzon.Common.Controls.PanelExt();
            this.tb_QT_PO = new Duzon.Common.Controls.CurrencyTextBox();
            this.lb_qt_atp = new Duzon.Common.Controls.LabelExt();
            this.lb_qt_po = new Duzon.Common.Controls.LabelExt();
            this.tb_QT_INV = new Duzon.Common.Controls.CurrencyTextBox();
            this.lb_qt_inv = new Duzon.Common.Controls.LabelExt();
            this.tb_QT_REQ = new Duzon.Common.Controls.CurrencyTextBox();
            this.lb_qt_req = new Duzon.Common.Controls.LabelExt();
            this.tb_QT_ATP = new Duzon.Common.Controls.CurrencyTextBox();
            this.btn_RE_APP = new Duzon.Common.Controls.RoundedButton(this.components);
            this.m_gridTmp = new Duzon.Common.Controls.PanelExt();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelExt1 = new Duzon.Common.Controls.PanelExt();
            this.lb_CD_DEPT = new Duzon.Common.Controls.LabelExt();
            this.tb_CD_DEPT = new Duzon.Common.Controls.TextBoxExt();
            this.panel9.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb_NM_EXCH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_TAX)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel23.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rbtn_PRI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtn_All)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_DT_PO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Ty1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable6)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb_QT_PO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_QT_INV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_QT_REQ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_QT_ATP)).BeginInit();
            this.m_gridTmp.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelExt1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.White;
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel9.Controls.Add(this.tb_CD_DEPT);
            this.panel9.Controls.Add(this.tb_CD_PJT);
            this.panel9.Controls.Add(this.tb_FG_PO_TR);
            this.panel9.Controls.Add(this.tb_NO_EMP);
            this.panel9.Controls.Add(this.tb_NM_PURGRP);
            this.panel9.Controls.Add(this.tb_NM_PARTNER);
            this.panel9.Controls.Add(this.tb_NM_TRANS);
            this.panel9.Controls.Add(this.panel18);
            this.panel9.Controls.Add(this.panel13);
            this.panel9.Controls.Add(this.panel14);
            this.panel9.Controls.Add(this.panel12);
            this.panel9.Controls.Add(this.panel11);
            this.panel9.Controls.Add(this.panel3);
            this.panel9.Controls.Add(this.tb_NO_PO);
            this.panel9.Controls.Add(this.tb_NM_EXCH);
            this.panel9.Controls.Add(this.tb_TAX);
            this.panel9.Controls.Add(this.tb_DC);
            this.panel9.Controls.Add(this.panel2);
            this.panel9.Controls.Add(this.panel1);
            this.panel9.Controls.Add(this.panel23);
            this.panel9.Controls.Add(this.cbo_NM_EXCH);
            this.panel9.Controls.Add(this.cbo_FG_UM);
            this.panel9.Controls.Add(this.cbo_PAYment);
            this.panel9.Controls.Add(this.cbo_FG_TAX);
            this.panel9.Controls.Add(this.cbo_TP_TAX);
            this.panel9.Controls.Add(this.cbo_CD_PLANT);
            this.panel9.Controls.Add(this.tb_DT_PO);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(3, 34);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(787, 149);
            this.panel9.TabIndex = 0;
            // 
            // tb_CD_PJT
            // 
            this.tb_CD_PJT.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.tb_CD_PJT.ButtonImage = ((System.Drawing.Image)(resources.GetObject("tb_CD_PJT.ButtonImage")));
            this.tb_CD_PJT.ChildMode = "";
            this.tb_CD_PJT.CodeName = "";
            this.tb_CD_PJT.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.tb_CD_PJT.CodeValue = "";
            this.tb_CD_PJT.ComboCheck = true;
            this.tb_CD_PJT.HelpID = Duzon.Common.Forms.Help.HelpID.P_SA_PROJECT_SUB;
            this.tb_CD_PJT.ItemBackColor = System.Drawing.Color.Empty;
            this.tb_CD_PJT.Location = new System.Drawing.Point(616, 127);
            this.tb_CD_PJT.Name = "tb_CD_PJT";
            this.tb_CD_PJT.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.tb_CD_PJT.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.tb_CD_PJT.SearchCode = true;
            this.tb_CD_PJT.SelectCount = 0;
            this.tb_CD_PJT.SetDefaultValue = false;
            this.tb_CD_PJT.SetNoneTypeMsg = "Please! Set Help Type!";
            this.tb_CD_PJT.Size = new System.Drawing.Size(152, 21);
            this.tb_CD_PJT.TabIndex = 14;
            this.tb_CD_PJT.TabStop = false;
            // 
            // tb_FG_PO_TR
            // 
            this.tb_FG_PO_TR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.tb_FG_PO_TR.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.tb_FG_PO_TR.ButtonImage = ((System.Drawing.Image)(resources.GetObject("tb_FG_PO_TR.ButtonImage")));
            this.tb_FG_PO_TR.ChildMode = "";
            this.tb_FG_PO_TR.CodeName = "";
            this.tb_FG_PO_TR.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.tb_FG_PO_TR.CodeValue = "";
            this.tb_FG_PO_TR.ComboCheck = true;
            this.tb_FG_PO_TR.HelpID = Duzon.Common.Forms.Help.HelpID.P_PU_TPPO_SUB;
            this.tb_FG_PO_TR.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.tb_FG_PO_TR.Location = new System.Drawing.Point(84, 52);
            this.tb_FG_PO_TR.Name = "tb_FG_PO_TR";
            this.tb_FG_PO_TR.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.tb_FG_PO_TR.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.tb_FG_PO_TR.SearchCode = true;
            this.tb_FG_PO_TR.SelectCount = 0;
            this.tb_FG_PO_TR.SetDefaultValue = false;
            this.tb_FG_PO_TR.SetNoneTypeMsg = "Please! Set Help Type!";
            this.tb_FG_PO_TR.Size = new System.Drawing.Size(100, 21);
            this.tb_FG_PO_TR.TabIndex = 6;
            this.tb_FG_PO_TR.TabStop = false;
            this.tb_FG_PO_TR.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpCodeTextBox_QueryBefore);
            this.tb_FG_PO_TR.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpCodeTextBox_QueryAfter);
            // 
            // tb_NO_EMP
            // 
            this.tb_NO_EMP.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.tb_NO_EMP.ButtonImage = ((System.Drawing.Image)(resources.GetObject("tb_NO_EMP.ButtonImage")));
            this.tb_NO_EMP.ChildMode = "";
            this.tb_NO_EMP.CodeName = "";
            this.tb_NO_EMP.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.tb_NO_EMP.CodeValue = "";
            this.tb_NO_EMP.ComboCheck = true;
            this.tb_NO_EMP.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
            this.tb_NO_EMP.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.tb_NO_EMP.Location = new System.Drawing.Point(616, 26);
            this.tb_NO_EMP.Name = "tb_NO_EMP";
            this.tb_NO_EMP.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.tb_NO_EMP.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.tb_NO_EMP.SearchCode = true;
            this.tb_NO_EMP.SelectCount = 0;
            this.tb_NO_EMP.SetDefaultValue = false;
            this.tb_NO_EMP.SetNoneTypeMsg = "Please! Set Help Type!";
            this.tb_NO_EMP.Size = new System.Drawing.Size(156, 21);
            this.tb_NO_EMP.TabIndex = 5;
            this.tb_NO_EMP.TabStop = false;
            this.tb_NO_EMP.CodeChanged += new System.EventHandler(this.OnBpCodeTextBox_CodeChanged);
            this.tb_NO_EMP.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpCodeTextBox_QueryAfter);
            // 
            // tb_NM_PURGRP
            // 
            this.tb_NM_PURGRP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.tb_NM_PURGRP.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.tb_NM_PURGRP.ButtonImage = ((System.Drawing.Image)(resources.GetObject("tb_NM_PURGRP.ButtonImage")));
            this.tb_NM_PURGRP.ChildMode = "";
            this.tb_NM_PURGRP.CodeName = "";
            this.tb_NM_PURGRP.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.tb_NM_PURGRP.CodeValue = "";
            this.tb_NM_PURGRP.ComboCheck = true;
            this.tb_NM_PURGRP.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PURGRP_SUB;
            this.tb_NM_PURGRP.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.tb_NM_PURGRP.Location = new System.Drawing.Point(362, 26);
            this.tb_NM_PURGRP.Name = "tb_NM_PURGRP";
            this.tb_NM_PURGRP.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.tb_NM_PURGRP.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.tb_NM_PURGRP.SearchCode = true;
            this.tb_NM_PURGRP.SelectCount = 0;
            this.tb_NM_PURGRP.SetDefaultValue = false;
            this.tb_NM_PURGRP.SetNoneTypeMsg = "Please! Set Help Type!";
            this.tb_NM_PURGRP.Size = new System.Drawing.Size(152, 21);
            this.tb_NM_PURGRP.TabIndex = 4;
            this.tb_NM_PURGRP.TabStop = false;
            // 
            // tb_NM_PARTNER
            // 
            this.tb_NM_PARTNER.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.tb_NM_PARTNER.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.tb_NM_PARTNER.ButtonImage = ((System.Drawing.Image)(resources.GetObject("tb_NM_PARTNER.ButtonImage")));
            this.tb_NM_PARTNER.ChildMode = "";
            this.tb_NM_PARTNER.CodeName = "";
            this.tb_NM_PARTNER.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.tb_NM_PARTNER.CodeValue = "";
            this.tb_NM_PARTNER.ComboCheck = true;
            this.tb_NM_PARTNER.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.tb_NM_PARTNER.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.tb_NM_PARTNER.Location = new System.Drawing.Point(616, 2);
            this.tb_NM_PARTNER.Name = "tb_NM_PARTNER";
            this.tb_NM_PARTNER.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.tb_NM_PARTNER.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.tb_NM_PARTNER.SearchCode = true;
            this.tb_NM_PARTNER.SelectCount = 0;
            this.tb_NM_PARTNER.SetDefaultValue = false;
            this.tb_NM_PARTNER.SetNoneTypeMsg = "Please! Set Help Type!";
            this.tb_NM_PARTNER.Size = new System.Drawing.Size(156, 21);
            this.tb_NM_PARTNER.TabIndex = 2;
            this.tb_NM_PARTNER.TabStop = false;
            // 
            // tb_NM_TRANS
            // 
            this.tb_NM_TRANS.Enabled = false;
            this.tb_NM_TRANS.Location = new System.Drawing.Point(184, 52);
            this.tb_NM_TRANS.Name = "tb_NM_TRANS";
            this.tb_NM_TRANS.SelectedAllEnabled = false;
            this.tb_NM_TRANS.Size = new System.Drawing.Size(71, 21);
            this.tb_NM_TRANS.TabIndex = 307;
            this.tb_NM_TRANS.UseKeyEnter = true;
            this.tb_NM_TRANS.UseKeyF3 = true;
            // 
            // panel18
            // 
            this.panel18.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel18.BackColor = System.Drawing.Color.Transparent;
            this.panel18.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel18.BackgroundImage")));
            this.panel18.Location = new System.Drawing.Point(5, 122);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(777, 1);
            this.panel18.TabIndex = 50;
            // 
            // panel13
            // 
            this.panel13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel13.BackColor = System.Drawing.Color.Transparent;
            this.panel13.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel13.BackgroundImage")));
            this.panel13.Location = new System.Drawing.Point(5, 98);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(777, 1);
            this.panel13.TabIndex = 49;
            // 
            // panel14
            // 
            this.panel14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel14.BackColor = System.Drawing.Color.Transparent;
            this.panel14.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel14.BackgroundImage")));
            this.panel14.Location = new System.Drawing.Point(5, 74);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(777, 1);
            this.panel14.TabIndex = 48;
            // 
            // panel12
            // 
            this.panel12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel12.BackColor = System.Drawing.Color.Transparent;
            this.panel12.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel12.BackgroundImage")));
            this.panel12.Location = new System.Drawing.Point(5, 49);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(777, 1);
            this.panel12.TabIndex = 47;
            // 
            // panel11
            // 
            this.panel11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel11.BackColor = System.Drawing.Color.Transparent;
            this.panel11.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel11.BackgroundImage")));
            this.panel11.Location = new System.Drawing.Point(5, 24);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(777, 1);
            this.panel11.TabIndex = 46;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel3.Controls.Add(this.lb_CD_DEPT);
            this.panel3.Controls.Add(this.lb_NM_PARTNER);
            this.panel3.Controls.Add(this.ib_NO_EMP);
            this.panel3.Controls.Add(this.lb_FG_UM);
            this.panel3.Controls.Add(this.lb_TP_TAX);
            this.panel3.Controls.Add(this.lb_CD_PJT);
            this.panel3.Location = new System.Drawing.Point(533, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(80, 146);
            this.panel3.TabIndex = 42;
            // 
            // lb_NM_PARTNER
            // 
            this.lb_NM_PARTNER.BackColor = System.Drawing.Color.Transparent;
            this.lb_NM_PARTNER.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_NM_PARTNER.Location = new System.Drawing.Point(3, 4);
            this.lb_NM_PARTNER.Name = "lb_NM_PARTNER";
            this.lb_NM_PARTNER.Resizeble = true;
            this.lb_NM_PARTNER.Size = new System.Drawing.Size(75, 18);
            this.lb_NM_PARTNER.TabIndex = 19;
            this.lb_NM_PARTNER.Text = "거래처명";
            this.lb_NM_PARTNER.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ib_NO_EMP
            // 
            this.ib_NO_EMP.BackColor = System.Drawing.Color.Transparent;
            this.ib_NO_EMP.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ib_NO_EMP.Location = new System.Drawing.Point(3, 29);
            this.ib_NO_EMP.Name = "ib_NO_EMP";
            this.ib_NO_EMP.Resizeble = true;
            this.ib_NO_EMP.Size = new System.Drawing.Size(75, 18);
            this.ib_NO_EMP.TabIndex = 21;
            this.ib_NO_EMP.Text = "담당자";
            this.ib_NO_EMP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_FG_UM
            // 
            this.lb_FG_UM.BackColor = System.Drawing.Color.Transparent;
            this.lb_FG_UM.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_FG_UM.Location = new System.Drawing.Point(2, 78);
            this.lb_FG_UM.Name = "lb_FG_UM";
            this.lb_FG_UM.Resizeble = true;
            this.lb_FG_UM.Size = new System.Drawing.Size(75, 18);
            this.lb_FG_UM.TabIndex = 24;
            this.lb_FG_UM.Text = "단가유형";
            this.lb_FG_UM.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_TP_TAX
            // 
            this.lb_TP_TAX.BackColor = System.Drawing.Color.Transparent;
            this.lb_TP_TAX.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_TP_TAX.Location = new System.Drawing.Point(3, 99);
            this.lb_TP_TAX.Name = "lb_TP_TAX";
            this.lb_TP_TAX.Resizeble = true;
            this.lb_TP_TAX.Size = new System.Drawing.Size(75, 18);
            this.lb_TP_TAX.TabIndex = 27;
            this.lb_TP_TAX.Text = "부가세여부";
            this.lb_TP_TAX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_CD_PJT
            // 
            this.lb_CD_PJT.BackColor = System.Drawing.Color.Transparent;
            this.lb_CD_PJT.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_CD_PJT.Location = new System.Drawing.Point(2, 126);
            this.lb_CD_PJT.Name = "lb_CD_PJT";
            this.lb_CD_PJT.Resizeble = true;
            this.lb_CD_PJT.Size = new System.Drawing.Size(75, 18);
            this.lb_CD_PJT.TabIndex = 28;
            this.lb_CD_PJT.Text = "Project";
            this.lb_CD_PJT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_NO_PO
            // 
            this.tb_NO_PO.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tb_NO_PO.Location = new System.Drawing.Point(84, 2);
            this.tb_NO_PO.Name = "tb_NO_PO";
            this.tb_NO_PO.ReadOnly = true;
            this.tb_NO_PO.SelectedAllEnabled = false;
            this.tb_NO_PO.Size = new System.Drawing.Size(172, 21);
            this.tb_NO_PO.TabIndex = 0;
            this.tb_NO_PO.TabStop = false;
            this.tb_NO_PO.UseKeyEnter = false;
            this.tb_NO_PO.UseKeyF3 = false;
            this.tb_NO_PO.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
            // 
            // tb_NM_EXCH
            // 
            this.tb_NM_EXCH.CurrencyDecimalDigits = 4;
            this.tb_NM_EXCH.CurrencyNegativePattern = 2;
            this.tb_NM_EXCH.CurrencyPositivePattern = 2;
            this.tb_NM_EXCH.DecimalValue = new decimal(new int[] {
            10000,
            0,
            0,
            262144});
            this.tb_NM_EXCH.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tb_NM_EXCH.ForeColor = System.Drawing.Color.Black;
            this.tb_NM_EXCH.Location = new System.Drawing.Point(433, 52);
            this.tb_NM_EXCH.Mask = null;
            this.tb_NM_EXCH.Name = "tb_NM_EXCH";
            this.tb_NM_EXCH.NullString = "0";
            this.tb_NM_EXCH.PositiveColor = System.Drawing.Color.Black;
            this.tb_NM_EXCH.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tb_NM_EXCH.Size = new System.Drawing.Size(97, 21);
            this.tb_NM_EXCH.TabIndex = 8;
            this.tb_NM_EXCH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tb_NM_EXCH.UseKeyEnter = false;
            this.tb_NM_EXCH.UseKeyF3 = false;
            this.tb_NM_EXCH.Leave += new System.EventHandler(this.tb_NM_EXCH_Leave);
            this.tb_NM_EXCH.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
            // 
            // tb_TAX
            // 
            this.tb_TAX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.tb_TAX.CurrencyDecimalDigits = 4;
            this.tb_TAX.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.tb_TAX.Enabled = false;
            this.tb_TAX.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tb_TAX.Location = new System.Drawing.Point(84, 100);
            this.tb_TAX.Mask = null;
            this.tb_TAX.Name = "tb_TAX";
            this.tb_TAX.NullString = "0";
            this.tb_TAX.PositiveColor = System.Drawing.Color.Black;
            this.tb_TAX.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tb_TAX.Size = new System.Drawing.Size(133, 21);
            this.tb_TAX.TabIndex = 13;
            this.tb_TAX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tb_TAX.UseKeyEnter = false;
            this.tb_TAX.UseKeyF3 = false;
            // 
            // tb_DC
            // 
            this.tb_DC.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tb_DC.Location = new System.Drawing.Point(84, 126);
            this.tb_DC.Name = "tb_DC";
            this.tb_DC.SelectedAllEnabled = false;
            this.tb_DC.Size = new System.Drawing.Size(446, 21);
            this.tb_DC.TabIndex = 15;
            this.tb_DC.UseKeyEnter = false;
            this.tb_DC.UseKeyF3 = false;
            this.tb_DC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_DC_KeyDown);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel2.Controls.Add(this.lb_DT_PO);
            this.panel2.Controls.Add(this.lb_NM_PURGRP);
            this.panel2.Controls.Add(this.lb_NM_EXCH);
            this.panel2.Controls.Add(this.lb_FG_TAX);
            this.panel2.Controls.Add(this.lb_Tax_Type);
            this.panel2.Location = new System.Drawing.Point(258, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(102, 122);
            this.panel2.TabIndex = 41;
            // 
            // lb_DT_PO
            // 
            this.lb_DT_PO.BackColor = System.Drawing.Color.Transparent;
            this.lb_DT_PO.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_DT_PO.Location = new System.Drawing.Point(0, 4);
            this.lb_DT_PO.Name = "lb_DT_PO";
            this.lb_DT_PO.Resizeble = true;
            this.lb_DT_PO.Size = new System.Drawing.Size(100, 18);
            this.lb_DT_PO.TabIndex = 18;
            this.lb_DT_PO.Text = "발주일자";
            this.lb_DT_PO.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_NM_PURGRP
            // 
            this.lb_NM_PURGRP.BackColor = System.Drawing.Color.Transparent;
            this.lb_NM_PURGRP.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_NM_PURGRP.Location = new System.Drawing.Point(0, 29);
            this.lb_NM_PURGRP.Name = "lb_NM_PURGRP";
            this.lb_NM_PURGRP.Resizeble = true;
            this.lb_NM_PURGRP.Size = new System.Drawing.Size(100, 18);
            this.lb_NM_PURGRP.TabIndex = 20;
            this.lb_NM_PURGRP.Text = "구매그룹명";
            this.lb_NM_PURGRP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_NM_EXCH
            // 
            this.lb_NM_EXCH.BackColor = System.Drawing.Color.Transparent;
            this.lb_NM_EXCH.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_NM_EXCH.Location = new System.Drawing.Point(0, 54);
            this.lb_NM_EXCH.Name = "lb_NM_EXCH";
            this.lb_NM_EXCH.Resizeble = true;
            this.lb_NM_EXCH.Size = new System.Drawing.Size(100, 18);
            this.lb_NM_EXCH.TabIndex = 29;
            this.lb_NM_EXCH.Text = "환정보";
            this.lb_NM_EXCH.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_FG_TAX
            // 
            this.lb_FG_TAX.BackColor = System.Drawing.Color.Transparent;
            this.lb_FG_TAX.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_FG_TAX.Location = new System.Drawing.Point(0, 78);
            this.lb_FG_TAX.Name = "lb_FG_TAX";
            this.lb_FG_TAX.Resizeble = true;
            this.lb_FG_TAX.Size = new System.Drawing.Size(100, 18);
            this.lb_FG_TAX.TabIndex = 26;
            this.lb_FG_TAX.Text = "과세구분";
            this.lb_FG_TAX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_Tax_Type
            // 
            this.lb_Tax_Type.BackColor = System.Drawing.Color.Transparent;
            this.lb_Tax_Type.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_Tax_Type.Location = new System.Drawing.Point(0, 102);
            this.lb_Tax_Type.Name = "lb_Tax_Type";
            this.lb_Tax_Type.Resizeble = true;
            this.lb_Tax_Type.Size = new System.Drawing.Size(100, 18);
            this.lb_Tax_Type.TabIndex = 28;
            this.lb_Tax_Type.Text = "계산서처리구분";
            this.lb_Tax_Type.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel1.Controls.Add(this.lb_NO_PO);
            this.panel1.Controls.Add(this.lb_DC);
            this.panel1.Controls.Add(this.lb_NM_PLANT);
            this.panel1.Controls.Add(this.lb_FG_PO_TR);
            this.panel1.Controls.Add(this.lb_FG_PAYMENT);
            this.panel1.Controls.Add(this.lb_TAX_RATE);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(80, 146);
            this.panel1.TabIndex = 2;
            // 
            // lb_NO_PO
            // 
            this.lb_NO_PO.BackColor = System.Drawing.Color.Transparent;
            this.lb_NO_PO.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_NO_PO.Location = new System.Drawing.Point(3, 4);
            this.lb_NO_PO.Name = "lb_NO_PO";
            this.lb_NO_PO.Resizeble = true;
            this.lb_NO_PO.Size = new System.Drawing.Size(75, 18);
            this.lb_NO_PO.TabIndex = 17;
            this.lb_NO_PO.Text = "발주번호";
            this.lb_NO_PO.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_DC
            // 
            this.lb_DC.BackColor = System.Drawing.Color.Transparent;
            this.lb_DC.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_DC.Location = new System.Drawing.Point(3, 126);
            this.lb_DC.Name = "lb_DC";
            this.lb_DC.Resizeble = true;
            this.lb_DC.Size = new System.Drawing.Size(75, 18);
            this.lb_DC.TabIndex = 33;
            this.lb_DC.Text = "비고";
            this.lb_DC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_NM_PLANT
            // 
            this.lb_NM_PLANT.BackColor = System.Drawing.Color.Transparent;
            this.lb_NM_PLANT.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_NM_PLANT.Location = new System.Drawing.Point(3, 29);
            this.lb_NM_PLANT.Name = "lb_NM_PLANT";
            this.lb_NM_PLANT.Resizeble = true;
            this.lb_NM_PLANT.Size = new System.Drawing.Size(75, 18);
            this.lb_NM_PLANT.TabIndex = 44;
            this.lb_NM_PLANT.Text = "공장명";
            this.lb_NM_PLANT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_FG_PO_TR
            // 
            this.lb_FG_PO_TR.BackColor = System.Drawing.Color.Transparent;
            this.lb_FG_PO_TR.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_FG_PO_TR.Location = new System.Drawing.Point(3, 54);
            this.lb_FG_PO_TR.Name = "lb_FG_PO_TR";
            this.lb_FG_PO_TR.Resizeble = true;
            this.lb_FG_PO_TR.Size = new System.Drawing.Size(75, 18);
            this.lb_FG_PO_TR.TabIndex = 23;
            this.lb_FG_PO_TR.Text = "발주형태";
            this.lb_FG_PO_TR.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_FG_PAYMENT
            // 
            this.lb_FG_PAYMENT.BackColor = System.Drawing.Color.Transparent;
            this.lb_FG_PAYMENT.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_FG_PAYMENT.Location = new System.Drawing.Point(3, 78);
            this.lb_FG_PAYMENT.Name = "lb_FG_PAYMENT";
            this.lb_FG_PAYMENT.Resizeble = true;
            this.lb_FG_PAYMENT.Size = new System.Drawing.Size(75, 18);
            this.lb_FG_PAYMENT.TabIndex = 25;
            this.lb_FG_PAYMENT.Text = "지급조건";
            this.lb_FG_PAYMENT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_TAX_RATE
            // 
            this.lb_TAX_RATE.BackColor = System.Drawing.Color.Transparent;
            this.lb_TAX_RATE.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_TAX_RATE.Location = new System.Drawing.Point(3, 102);
            this.lb_TAX_RATE.Name = "lb_TAX_RATE";
            this.lb_TAX_RATE.Resizeble = true;
            this.lb_TAX_RATE.Size = new System.Drawing.Size(75, 18);
            this.lb_TAX_RATE.TabIndex = 32;
            this.lb_TAX_RATE.Text = "부가세율";
            this.lb_TAX_RATE.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel23
            // 
            this.panel23.Controls.Add(this.rbtn_PRI);
            this.panel23.Controls.Add(this.rbtn_All);
            this.panel23.Location = new System.Drawing.Point(361, 100);
            this.panel23.Name = "panel23";
            this.panel23.Size = new System.Drawing.Size(169, 22);
            this.panel23.TabIndex = 14;
            // 
            // rbtn_PRI
            // 
            this.rbtn_PRI.Location = new System.Drawing.Point(89, 3);
            this.rbtn_PRI.Name = "rbtn_PRI";
            this.rbtn_PRI.Size = new System.Drawing.Size(65, 18);
            this.rbtn_PRI.TabIndex = 1;
            this.rbtn_PRI.TabStop = true;
            this.rbtn_PRI.Text = "건별";
            this.rbtn_PRI.UseKeyEnter = false;
            this.rbtn_PRI.Click += new System.EventHandler(this.rbtn_PRI_Click);
            this.rbtn_PRI.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
            // 
            // rbtn_All
            // 
            this.rbtn_All.Location = new System.Drawing.Point(5, 3);
            this.rbtn_All.Name = "rbtn_All";
            this.rbtn_All.Size = new System.Drawing.Size(65, 18);
            this.rbtn_All.TabIndex = 0;
            this.rbtn_All.TabStop = true;
            this.rbtn_All.Text = "일괄";
            this.rbtn_All.UseKeyEnter = false;
            this.rbtn_All.Click += new System.EventHandler(this.rbtn_All_Click);
            this.rbtn_All.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
            // 
            // cbo_NM_EXCH
            // 
            this.cbo_NM_EXCH.AutoDropDown = true;
            this.cbo_NM_EXCH.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cbo_NM_EXCH.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_NM_EXCH.Location = new System.Drawing.Point(362, 53);
            this.cbo_NM_EXCH.Name = "cbo_NM_EXCH";
            this.cbo_NM_EXCH.ShowCheckBox = false;
            this.cbo_NM_EXCH.Size = new System.Drawing.Size(70, 20);
            this.cbo_NM_EXCH.TabIndex = 7;
            this.cbo_NM_EXCH.UseKeyEnter = false;
            this.cbo_NM_EXCH.UseKeyF3 = false;
            this.cbo_NM_EXCH.SelectionChangeCommitted += new System.EventHandler(this.cbo_NM_EXCH_SelectionChangeCommitted);
            this.cbo_NM_EXCH.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
            // 
            // cbo_FG_UM
            // 
            this.cbo_FG_UM.AutoDropDown = true;
            this.cbo_FG_UM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cbo_FG_UM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_FG_UM.Location = new System.Drawing.Point(616, 79);
            this.cbo_FG_UM.Name = "cbo_FG_UM";
            this.cbo_FG_UM.ShowCheckBox = false;
            this.cbo_FG_UM.Size = new System.Drawing.Size(136, 20);
            this.cbo_FG_UM.TabIndex = 9;
            this.cbo_FG_UM.UseKeyEnter = false;
            this.cbo_FG_UM.UseKeyF3 = false;
            this.cbo_FG_UM.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
            // 
            // cbo_PAYment
            // 
            this.cbo_PAYment.AutoDropDown = true;
            this.cbo_PAYment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cbo_PAYment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_PAYment.Location = new System.Drawing.Point(84, 77);
            this.cbo_PAYment.Name = "cbo_PAYment";
            this.cbo_PAYment.ShowCheckBox = false;
            this.cbo_PAYment.Size = new System.Drawing.Size(136, 20);
            this.cbo_PAYment.TabIndex = 10;
            this.cbo_PAYment.UseKeyEnter = false;
            this.cbo_PAYment.UseKeyF3 = false;
            this.cbo_PAYment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
            // 
            // cbo_FG_TAX
            // 
            this.cbo_FG_TAX.AutoDropDown = true;
            this.cbo_FG_TAX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cbo_FG_TAX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_FG_TAX.Location = new System.Drawing.Point(363, 77);
            this.cbo_FG_TAX.Name = "cbo_FG_TAX";
            this.cbo_FG_TAX.ShowCheckBox = false;
            this.cbo_FG_TAX.Size = new System.Drawing.Size(144, 20);
            this.cbo_FG_TAX.TabIndex = 11;
            this.cbo_FG_TAX.UseKeyEnter = false;
            this.cbo_FG_TAX.UseKeyF3 = false;
            this.cbo_FG_TAX.SelectionChangeCommitted += new System.EventHandler(this.cbo_FG_TAX_SelectionChangeCommitted);
            this.cbo_FG_TAX.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
            // 
            // cbo_TP_TAX
            // 
            this.cbo_TP_TAX.AutoDropDown = true;
            this.cbo_TP_TAX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cbo_TP_TAX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_TP_TAX.Location = new System.Drawing.Point(616, 103);
            this.cbo_TP_TAX.Name = "cbo_TP_TAX";
            this.cbo_TP_TAX.ShowCheckBox = false;
            this.cbo_TP_TAX.Size = new System.Drawing.Size(135, 20);
            this.cbo_TP_TAX.TabIndex = 12;
            this.cbo_TP_TAX.UseKeyEnter = false;
            this.cbo_TP_TAX.UseKeyF3 = false;
            this.cbo_TP_TAX.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
            // 
            // cbo_CD_PLANT
            // 
            this.cbo_CD_PLANT.AutoDropDown = true;
            this.cbo_CD_PLANT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cbo_CD_PLANT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_CD_PLANT.Location = new System.Drawing.Point(84, 28);
            this.cbo_CD_PLANT.Name = "cbo_CD_PLANT";
            this.cbo_CD_PLANT.ShowCheckBox = false;
            this.cbo_CD_PLANT.Size = new System.Drawing.Size(172, 20);
            this.cbo_CD_PLANT.TabIndex = 3;
            this.cbo_CD_PLANT.UseKeyEnter = false;
            this.cbo_CD_PLANT.UseKeyF3 = false;
            this.cbo_CD_PLANT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
            // 
            // tb_DT_PO
            // 
            this.tb_DT_PO.CalendarBackColor = System.Drawing.Color.White;
            this.tb_DT_PO.DayColor = System.Drawing.Color.Black;
            this.tb_DT_PO.FriDayColor = System.Drawing.Color.Blue;
            this.tb_DT_PO.Location = new System.Drawing.Point(363, 2);
            this.tb_DT_PO.Mask = "####/##/##";
            this.tb_DT_PO.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.tb_DT_PO.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.tb_DT_PO.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.tb_DT_PO.Modified = false;
            this.tb_DT_PO.Name = "tb_DT_PO";
            this.tb_DT_PO.PaddingCharacter = '_';
            this.tb_DT_PO.PassivePromptCharacter = '_';
            this.tb_DT_PO.PromptCharacter = '_';
            this.tb_DT_PO.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.tb_DT_PO.ShowToDay = true;
            this.tb_DT_PO.ShowTodayCircle = true;
            this.tb_DT_PO.ShowUpDown = false;
            this.tb_DT_PO.Size = new System.Drawing.Size(92, 21);
            this.tb_DT_PO.SunDayColor = System.Drawing.Color.Red;
            this.tb_DT_PO.TabIndex = 1;
            this.tb_DT_PO.TitleBackColor = System.Drawing.SystemColors.Control;
            this.tb_DT_PO.TitleForeColor = System.Drawing.Color.White;
            this.tb_DT_PO.ToDayColor = System.Drawing.Color.Red;
            this.tb_DT_PO.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.tb_DT_PO.UseKeyF3 = false;
            this.tb_DT_PO.Value = new System.DateTime(2004, 1, 1, 0, 0, 0, 0);
            this.tb_DT_PO.Validated += new System.EventHandler(this.DataPickerValidated);
            // 
            // m_pnlGrid
            // 
            this.m_pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlGrid.Location = new System.Drawing.Point(3, 219);
            this.m_pnlGrid.Name = "m_pnlGrid";
            this.m_pnlGrid.Size = new System.Drawing.Size(787, 305);
            this.m_pnlGrid.TabIndex = 9;
            // 
            // btn_RE_PJT
            // 
            this.btn_RE_PJT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_RE_PJT.BackColor = System.Drawing.Color.White;
            this.btn_RE_PJT.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.btn_RE_PJT.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.btn_RE_PJT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_RE_PJT.Location = new System.Drawing.Point(20, 0);
            this.btn_RE_PJT.Name = "btn_RE_PJT";
            this.btn_RE_PJT.Size = new System.Drawing.Size(90, 24);
            this.btn_RE_PJT.TabIndex = 7;
            this.btn_RE_PJT.TabStop = false;
            this.btn_RE_PJT.Text = "PJT관련";
            this.btn_RE_PJT.UseVisualStyleBackColor = false;
            this.btn_RE_PJT.Click += new System.EventHandler(this.btn_RE_PJT_Click);
            // 
            // btn_RE_PR
            // 
            this.btn_RE_PR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_RE_PR.BackColor = System.Drawing.Color.White;
            this.btn_RE_PR.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.btn_RE_PR.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.btn_RE_PR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_RE_PR.Location = new System.Drawing.Point(154, 1);
            this.btn_RE_PR.Name = "btn_RE_PR";
            this.btn_RE_PR.Size = new System.Drawing.Size(90, 24);
            this.btn_RE_PR.TabIndex = 5;
            this.btn_RE_PR.TabStop = false;
            this.btn_RE_PR.Text = "요청적용";
            this.btn_RE_PR.UseVisualStyleBackColor = false;
            this.btn_RE_PR.Click += new System.EventHandler(this.btn_RE_PR_Click);
            // 
            // btn_ITEM_EXP
            // 
            this.btn_ITEM_EXP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ITEM_EXP.BackColor = System.Drawing.Color.White;
            this.btn_ITEM_EXP.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.btn_ITEM_EXP.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.btn_ITEM_EXP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ITEM_EXP.Location = new System.Drawing.Point(112, 0);
            this.btn_ITEM_EXP.Name = "btn_ITEM_EXP";
            this.btn_ITEM_EXP.Size = new System.Drawing.Size(90, 24);
            this.btn_ITEM_EXP.TabIndex = 6;
            this.btn_ITEM_EXP.TabStop = false;
            this.btn_ITEM_EXP.Text = "품목전개";
            this.btn_ITEM_EXP.UseVisualStyleBackColor = false;
            this.btn_ITEM_EXP.Click += new System.EventHandler(this.btn_ITEM_EXP_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            // 
            // ds_Ty1
            // 
            this.ds_Ty1.DataSetName = "NewDataSet";
            this.ds_Ty1.Locale = new System.Globalization.CultureInfo("ko-KR");
            this.ds_Ty1.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable1,
            this.dataTable2,
            this.dataTable3,
            this.dataTable4,
            this.dataTable5,
            this.dataTable6});
            // 
            // dataTable1
            // 
            this.dataTable1.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3,
            this.dataColumn4,
            this.dataColumn5,
            this.dataColumn6,
            this.dataColumn7,
            this.dataColumn8,
            this.dataColumn9,
            this.dataColumn10,
            this.dataColumn11,
            this.dataColumn12,
            this.dataColumn13,
            this.dataColumn14,
            this.dataColumn15,
            this.dataColumn16,
            this.dataColumn17,
            this.dataColumn18,
            this.dataColumn19,
            this.dataColumn20,
            this.dataColumn21,
            this.dataColumn22,
            this.dataColumn23,
            this.dataColumn24,
            this.dataColumn25,
            this.dataColumn26,
            this.dataColumn27,
            this.dataColumn28,
            this.dataColumn29,
            this.dataColumn30,
            this.dataColumn31,
            this.dataColumn32,
            this.dataColumn33,
            this.dataColumn34,
            this.dataColumn35,
            this.dataColumn36,
            this.dataColumn37,
            this.dataColumn38,
            this.dataColumn39,
            this.dataColumn40,
            this.dataColumn41,
            this.dataColumn42,
            this.dataColumn43,
            this.dataColumn44,
            this.dataColumn45,
            this.dataColumn46,
            this.dataColumn47,
            this.dataColumn48,
            this.dataColumn49,
            this.dataColumn50,
            this.dataColumn51,
            this.dataColumn52,
            this.dataColumn66,
            this.dataColumn67,
            this.dataColumn68,
            this.dataColumn69,
            this.dataColumn103,
            this.dataColumn104,
            this.dataColumn108});
            this.dataTable1.TableName = "Table1";
            // 
            // dataColumn1
            // 
            this.dataColumn1.ColumnName = "CD_PLANT";
            // 
            // dataColumn2
            // 
            this.dataColumn2.ColumnName = "NM_PLANT";
            // 
            // dataColumn3
            // 
            this.dataColumn3.ColumnName = "CD_ITEM";
            // 
            // dataColumn4
            // 
            this.dataColumn4.ColumnName = "NM_ITEM";
            // 
            // dataColumn5
            // 
            this.dataColumn5.ColumnName = "STND_ITEM";
            // 
            // dataColumn6
            // 
            this.dataColumn6.ColumnName = "UNIT_IM";
            // 
            // dataColumn7
            // 
            this.dataColumn7.ColumnName = "UM_EX_PO";
            this.dataColumn7.DataType = typeof(double);
            // 
            // dataColumn8
            // 
            this.dataColumn8.ColumnName = "UM_P";
            this.dataColumn8.DataType = typeof(double);
            // 
            // dataColumn9
            // 
            this.dataColumn9.ColumnName = "NM_SYSDEF";
            // 
            // dataColumn10
            // 
            this.dataColumn10.ColumnName = "UNIT_PO";
            // 
            // dataColumn11
            // 
            this.dataColumn11.ColumnName = "NM_SL";
            // 
            // dataColumn12
            // 
            this.dataColumn12.ColumnName = "CD_SL";
            // 
            // dataColumn13
            // 
            this.dataColumn13.ColumnName = "GR_SL";
            // 
            // dataColumn14
            // 
            this.dataColumn14.ColumnName = "AM";
            this.dataColumn14.DataType = typeof(double);
            // 
            // dataColumn15
            // 
            this.dataColumn15.ColumnName = "UM";
            this.dataColumn15.DataType = typeof(double);
            // 
            // dataColumn16
            // 
            this.dataColumn16.ColumnName = "CD_UNIT_MM";
            // 
            // dataColumn17
            // 
            this.dataColumn17.ColumnName = "NO_CONTRACT";
            // 
            // dataColumn18
            // 
            this.dataColumn18.ColumnName = "NM_PJT";
            // 
            // dataColumn19
            // 
            this.dataColumn19.ColumnName = "CD_PJT";
            // 
            // dataColumn20
            // 
            this.dataColumn20.ColumnName = "PNM_PJT";
            // 
            // dataColumn21
            // 
            this.dataColumn21.ColumnName = "QT_PO_MM";
            this.dataColumn21.DataType = typeof(double);
            // 
            // dataColumn22
            // 
            this.dataColumn22.ColumnName = "QT_PO";
            this.dataColumn22.DataType = typeof(double);
            // 
            // dataColumn23
            // 
            this.dataColumn23.ColumnName = "VAT";
            this.dataColumn23.DataType = typeof(double);
            // 
            // dataColumn24
            // 
            this.dataColumn24.ColumnName = "AM_EX";
            this.dataColumn24.DataType = typeof(double);
            // 
            // dataColumn25
            // 
            this.dataColumn25.ColumnName = "UM_EX";
            this.dataColumn25.DataType = typeof(double);
            // 
            // dataColumn26
            // 
            this.dataColumn26.ColumnName = "DT_LIMIT";
            // 
            // dataColumn27
            // 
            this.dataColumn27.ColumnName = "FG_POST";
            // 
            // dataColumn28
            // 
            this.dataColumn28.ColumnName = "FG_POCON";
            // 
            // dataColumn29
            // 
            this.dataColumn29.ColumnName = "NO_PR";
            // 
            // dataColumn30
            // 
            this.dataColumn30.ColumnName = "NO_PRLINE";
            this.dataColumn30.DataType = typeof(double);
            // 
            // dataColumn31
            // 
            this.dataColumn31.ColumnName = "NO_CTLINE";
            this.dataColumn31.DataType = typeof(double);
            // 
            // dataColumn32
            // 
            this.dataColumn32.ColumnName = "RT_PO";
            this.dataColumn32.DataType = typeof(double);
            // 
            // dataColumn33
            // 
            this.dataColumn33.ColumnName = "QT_REQ";
            this.dataColumn33.DataType = typeof(double);
            // 
            // dataColumn34
            // 
            this.dataColumn34.ColumnName = "QT_RCV";
            this.dataColumn34.DataType = typeof(double);
            // 
            // dataColumn35
            // 
            this.dataColumn35.ColumnName = "NO_LINE";
            this.dataColumn35.DataType = typeof(double);
            // 
            // dataColumn36
            // 
            this.dataColumn36.ColumnName = "CD_COMPANY";
            // 
            // dataColumn37
            // 
            this.dataColumn37.ColumnName = "FG_TAX";
            // 
            // dataColumn38
            // 
            this.dataColumn38.ColumnName = "FG_TRANS";
            // 
            // dataColumn39
            // 
            this.dataColumn39.ColumnName = "FG_RCV";
            // 
            // dataColumn40
            // 
            this.dataColumn40.ColumnName = "FG_PURCHASE";
            // 
            // dataColumn41
            // 
            this.dataColumn41.ColumnName = "YN_AUTORCV";
            // 
            // dataColumn42
            // 
            this.dataColumn42.ColumnName = "YN_RCV";
            // 
            // dataColumn43
            // 
            this.dataColumn43.ColumnName = "YN_RETURN";
            // 
            // dataColumn44
            // 
            this.dataColumn44.ColumnName = "YN_SUBCON";
            // 
            // dataColumn45
            // 
            this.dataColumn45.ColumnName = "YN_IMPORT";
            // 
            // dataColumn46
            // 
            this.dataColumn46.ColumnName = "YN_ORDER";
            // 
            // dataColumn47
            // 
            this.dataColumn47.ColumnName = "YN_REQ";
            // 
            // dataColumn48
            // 
            this.dataColumn48.ColumnName = "NO_PO";
            // 
            // dataColumn49
            // 
            this.dataColumn49.ColumnName = "YN_PURCHASE";
            // 
            // dataColumn50
            // 
            this.dataColumn50.ColumnName = "FG_TPPURCHASE";
            // 
            // dataColumn51
            // 
            this.dataColumn51.ColumnName = "NO_RCV";
            // 
            // dataColumn52
            // 
            this.dataColumn52.ColumnName = "NO_RCVLINE";
            this.dataColumn52.DataType = typeof(double);
            // 
            // dataColumn66
            // 
            this.dataColumn66.ColumnName = "QT_ATPC";
            this.dataColumn66.DataType = typeof(double);
            // 
            // dataColumn67
            // 
            this.dataColumn67.ColumnName = "QT_INVC";
            this.dataColumn67.DataType = typeof(double);
            // 
            // dataColumn68
            // 
            this.dataColumn68.ColumnName = "QT_REQC";
            this.dataColumn68.DataType = typeof(double);
            // 
            // dataColumn69
            // 
            this.dataColumn69.ColumnName = "QT_POC";
            this.dataColumn69.DataType = typeof(double);
            // 
            // dataColumn103
            // 
            this.dataColumn103.ColumnName = "NO_APP";
            // 
            // dataColumn104
            // 
            this.dataColumn104.ColumnName = "NO_APPLINE";
            this.dataColumn104.DataType = typeof(double);
            // 
            // dataColumn108
            // 
            this.dataColumn108.ColumnName = "STND_MA_ITEM";
            // 
            // dataTable2
            // 
            this.dataTable2.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn53,
            this.dataColumn54});
            this.dataTable2.TableName = "Table2";
            // 
            // dataColumn53
            // 
            this.dataColumn53.ColumnName = "NO_PR";
            // 
            // dataColumn54
            // 
            this.dataColumn54.ColumnName = "NO_PRLINE";
            // 
            // dataTable3
            // 
            this.dataTable3.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn55,
            this.dataColumn56,
            this.dataColumn57,
            this.dataColumn58,
            this.dataColumn59,
            this.dataColumn60,
            this.dataColumn61,
            this.dataColumn62,
            this.dataColumn63,
            this.dataColumn64,
            this.dataColumn65,
            this.dataColumn70});
            this.dataTable3.TableName = "Table3";
            // 
            // dataColumn55
            // 
            this.dataColumn55.ColumnName = "NM_TRANS";
            // 
            // dataColumn56
            // 
            this.dataColumn56.ColumnName = "YN_REQ";
            // 
            // dataColumn57
            // 
            this.dataColumn57.ColumnName = "YN_ORDER";
            // 
            // dataColumn58
            // 
            this.dataColumn58.ColumnName = "YN_IMPORT";
            // 
            // dataColumn59
            // 
            this.dataColumn59.ColumnName = "YN_SUBCON";
            // 
            // dataColumn60
            // 
            this.dataColumn60.ColumnName = "YN_RETURN";
            // 
            // dataColumn61
            // 
            this.dataColumn61.ColumnName = "YN_RCV";
            // 
            // dataColumn62
            // 
            this.dataColumn62.ColumnName = "YN_AUTORCV";
            // 
            // dataColumn63
            // 
            this.dataColumn63.ColumnName = "FG_TPPURCHASE";
            // 
            // dataColumn64
            // 
            this.dataColumn64.ColumnName = "FG_TPRCV";
            // 
            // dataColumn65
            // 
            this.dataColumn65.ColumnName = "FG_TRANS";
            // 
            // dataColumn70
            // 
            this.dataColumn70.ColumnName = "YN_AM";
            // 
            // dataTable4
            // 
            this.dataTable4.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn71,
            this.dataColumn72,
            this.dataColumn73,
            this.dataColumn74,
            this.dataColumn75,
            this.dataColumn76,
            this.dataColumn77,
            this.dataColumn78,
            this.dataColumn79,
            this.dataColumn80,
            this.dataColumn81,
            this.dataColumn82,
            this.dataColumn83,
            this.dataColumn84,
            this.dataColumn85,
            this.dataColumn86,
            this.dataColumn87,
            this.dataColumn88,
            this.dataColumn89,
            this.dataColumn90,
            this.dataColumn91,
            this.dataColumn92,
            this.dataColumn93,
            this.dataColumn102,
            this.dataColumn107});
            this.dataTable4.TableName = "Table4";
            // 
            // dataColumn71
            // 
            this.dataColumn71.ColumnName = "NO_PO";
            // 
            // dataColumn72
            // 
            this.dataColumn72.ColumnName = "CD_PLANT";
            // 
            // dataColumn73
            // 
            this.dataColumn73.ColumnName = "CD_PARTNER";
            // 
            // dataColumn74
            // 
            this.dataColumn74.ColumnName = "DT_PO";
            // 
            // dataColumn75
            // 
            this.dataColumn75.ColumnName = "CD_PURGRP";
            // 
            // dataColumn76
            // 
            this.dataColumn76.ColumnName = "NO_EMP";
            // 
            // dataColumn77
            // 
            this.dataColumn77.ColumnName = "CD_TPPO";
            // 
            // dataColumn78
            // 
            this.dataColumn78.ColumnName = "FG_UM";
            // 
            // dataColumn79
            // 
            this.dataColumn79.ColumnName = "FG_PAYMENT";
            // 
            // dataColumn80
            // 
            this.dataColumn80.ColumnName = "FG_TAX";
            // 
            // dataColumn81
            // 
            this.dataColumn81.ColumnName = "TP_UM_TAX";
            // 
            // dataColumn82
            // 
            this.dataColumn82.ColumnName = "CD_PJT";
            // 
            // dataColumn83
            // 
            this.dataColumn83.ColumnName = "CD_EXCH";
            // 
            // dataColumn84
            // 
            this.dataColumn84.ColumnName = "RT_EXCH";
            this.dataColumn84.DataType = typeof(double);
            // 
            // dataColumn85
            // 
            this.dataColumn85.ColumnName = "AM_EX";
            this.dataColumn85.DataType = typeof(double);
            // 
            // dataColumn86
            // 
            this.dataColumn86.ColumnName = "AM";
            this.dataColumn86.DataType = typeof(double);
            // 
            // dataColumn87
            // 
            this.dataColumn87.ColumnName = "VAT";
            this.dataColumn87.DataType = typeof(double);
            // 
            // dataColumn88
            // 
            this.dataColumn88.ColumnName = "DC50_PO";
            // 
            // dataColumn89
            // 
            this.dataColumn89.ColumnName = "TP_PROCESS";
            // 
            // dataColumn90
            // 
            this.dataColumn90.ColumnName = "FG_TAXP";
            // 
            // dataColumn91
            // 
            this.dataColumn91.ColumnName = "YN_AM";
            // 
            // dataColumn92
            // 
            this.dataColumn92.ColumnName = "CD_COMPANY";
            // 
            // dataColumn93
            // 
            this.dataColumn93.ColumnName = "FG_TRANS";
            // 
            // dataColumn102
            // 
            this.dataColumn102.ColumnName = "CD_DEPT";
            // 
            // dataColumn107
            // 
            this.dataColumn107.ColumnName = "DTS_UPDATE";
            // 
            // dataTable5
            // 
            this.dataTable5.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn94,
            this.dataColumn95,
            this.dataColumn96,
            this.dataColumn97,
            this.dataColumn98,
            this.dataColumn99,
            this.dataColumn100,
            this.dataColumn101});
            this.dataTable5.TableName = "PITEM";
            // 
            // dataColumn94
            // 
            this.dataColumn94.ColumnName = "CD_PLANT";
            // 
            // dataColumn95
            // 
            this.dataColumn95.ColumnName = "CD_COMPANY";
            // 
            // dataColumn96
            // 
            this.dataColumn96.ColumnName = "CD_ITEM";
            // 
            // dataColumn97
            // 
            this.dataColumn97.ColumnName = "FG_UM";
            // 
            // dataColumn98
            // 
            this.dataColumn98.ColumnName = "CD_EXCH";
            // 
            // dataColumn99
            // 
            this.dataColumn99.ColumnName = "SDT_UM";
            // 
            // dataColumn100
            // 
            this.dataColumn100.ColumnName = "CD_PARTNER";
            // 
            // dataColumn101
            // 
            this.dataColumn101.ColumnName = "CD_PURGRP";
            // 
            // dataTable6
            // 
            this.dataTable6.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn105,
            this.dataColumn106});
            this.dataTable6.TableName = "Table5";
            // 
            // dataColumn105
            // 
            this.dataColumn105.ColumnName = "NO_APP";
            // 
            // dataColumn106
            // 
            this.dataColumn106.ColumnName = "NO_APPLINE";
            this.dataColumn106.DataType = typeof(double);
            // 
            // btn_delete
            // 
            this.btn_delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_delete.BackColor = System.Drawing.Color.White;
            this.btn_delete.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.btn_delete.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.btn_delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_delete.Location = new System.Drawing.Point(266, 0);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(60, 24);
            this.btn_delete.TabIndex = 306;
            this.btn_delete.TabStop = false;
            this.btn_delete.Text = "삭제";
            this.btn_delete.UseVisualStyleBackColor = false;
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // btn_insert
            // 
            this.btn_insert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_insert.BackColor = System.Drawing.Color.White;
            this.btn_insert.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.btn_insert.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.btn_insert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_insert.Location = new System.Drawing.Point(204, 0);
            this.btn_insert.Name = "btn_insert";
            this.btn_insert.Size = new System.Drawing.Size(60, 24);
            this.btn_insert.TabIndex = 307;
            this.btn_insert.TabStop = false;
            this.btn_insert.Text = "추가";
            this.btn_insert.UseVisualStyleBackColor = false;
            this.btn_insert.Click += new System.EventHandler(this.btn_insert_Click);
            // 
            // btn_CM_INV
            // 
            this.btn_CM_INV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_CM_INV.BackColor = System.Drawing.Color.White;
            this.btn_CM_INV.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.btn_CM_INV.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.btn_CM_INV.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_CM_INV.Location = new System.Drawing.Point(702, 0);
            this.btn_CM_INV.Name = "btn_CM_INV";
            this.btn_CM_INV.Size = new System.Drawing.Size(80, 24);
            this.btn_CM_INV.TabIndex = 309;
            this.btn_CM_INV.TabStop = false;
            this.btn_CM_INV.Text = "재고확인";
            this.btn_CM_INV.UseVisualStyleBackColor = false;
            this.btn_CM_INV.Click += new System.EventHandler(this.btn_CM_INV_Click);
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.tb_QT_PO);
            this.panel5.Controls.Add(this.lb_qt_atp);
            this.panel5.Controls.Add(this.btn_CM_INV);
            this.panel5.Controls.Add(this.lb_qt_po);
            this.panel5.Controls.Add(this.tb_QT_INV);
            this.panel5.Controls.Add(this.lb_qt_inv);
            this.panel5.Controls.Add(this.tb_QT_REQ);
            this.panel5.Controls.Add(this.lb_qt_req);
            this.panel5.Controls.Add(this.tb_QT_ATP);
            this.panel5.Location = new System.Drawing.Point(3, 530);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(787, 28);
            this.panel5.TabIndex = 308;
            // 
            // tb_QT_PO
            // 
            this.tb_QT_PO.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.tb_QT_PO.Enabled = false;
            this.tb_QT_PO.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tb_QT_PO.Location = new System.Drawing.Point(85, 3);
            this.tb_QT_PO.Mask = null;
            this.tb_QT_PO.Name = "tb_QT_PO";
            this.tb_QT_PO.NullString = "0";
            this.tb_QT_PO.PositiveColor = System.Drawing.Color.Black;
            this.tb_QT_PO.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tb_QT_PO.Size = new System.Drawing.Size(80, 21);
            this.tb_QT_PO.TabIndex = 13;
            this.tb_QT_PO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tb_QT_PO.UseKeyEnter = true;
            this.tb_QT_PO.UseKeyF3 = true;
            // 
            // lb_qt_atp
            // 
            this.lb_qt_atp.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_qt_atp.Location = new System.Drawing.Point(532, 6);
            this.lb_qt_atp.Name = "lb_qt_atp";
            this.lb_qt_atp.Resizeble = true;
            this.lb_qt_atp.Size = new System.Drawing.Size(80, 18);
            this.lb_qt_atp.TabIndex = 6;
            this.lb_qt_atp.Text = "가용재고량";
            this.lb_qt_atp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_qt_po
            // 
            this.lb_qt_po.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_qt_po.Location = new System.Drawing.Point(2, 6);
            this.lb_qt_po.Name = "lb_qt_po";
            this.lb_qt_po.Resizeble = true;
            this.lb_qt_po.Size = new System.Drawing.Size(80, 18);
            this.lb_qt_po.TabIndex = 4;
            this.lb_qt_po.Text = "발주량";
            this.lb_qt_po.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_QT_INV
            // 
            this.tb_QT_INV.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.tb_QT_INV.Enabled = false;
            this.tb_QT_INV.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tb_QT_INV.Location = new System.Drawing.Point(437, 3);
            this.tb_QT_INV.Mask = null;
            this.tb_QT_INV.Name = "tb_QT_INV";
            this.tb_QT_INV.NullString = "0";
            this.tb_QT_INV.PositiveColor = System.Drawing.Color.Black;
            this.tb_QT_INV.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tb_QT_INV.Size = new System.Drawing.Size(80, 21);
            this.tb_QT_INV.TabIndex = 13;
            this.tb_QT_INV.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tb_QT_INV.UseKeyEnter = true;
            this.tb_QT_INV.UseKeyF3 = true;
            // 
            // lb_qt_inv
            // 
            this.lb_qt_inv.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_qt_inv.Location = new System.Drawing.Point(354, 6);
            this.lb_qt_inv.Name = "lb_qt_inv";
            this.lb_qt_inv.Resizeble = true;
            this.lb_qt_inv.Size = new System.Drawing.Size(80, 18);
            this.lb_qt_inv.TabIndex = 4;
            this.lb_qt_inv.Text = "현재고량";
            this.lb_qt_inv.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_QT_REQ
            // 
            this.tb_QT_REQ.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.tb_QT_REQ.Enabled = false;
            this.tb_QT_REQ.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tb_QT_REQ.Location = new System.Drawing.Point(260, 3);
            this.tb_QT_REQ.Mask = null;
            this.tb_QT_REQ.Name = "tb_QT_REQ";
            this.tb_QT_REQ.NullString = "0";
            this.tb_QT_REQ.PositiveColor = System.Drawing.Color.Black;
            this.tb_QT_REQ.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tb_QT_REQ.Size = new System.Drawing.Size(80, 21);
            this.tb_QT_REQ.TabIndex = 13;
            this.tb_QT_REQ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tb_QT_REQ.UseKeyEnter = true;
            this.tb_QT_REQ.UseKeyF3 = true;
            // 
            // lb_qt_req
            // 
            this.lb_qt_req.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_qt_req.Location = new System.Drawing.Point(177, 6);
            this.lb_qt_req.Name = "lb_qt_req";
            this.lb_qt_req.Resizeble = true;
            this.lb_qt_req.Size = new System.Drawing.Size(80, 18);
            this.lb_qt_req.TabIndex = 4;
            this.lb_qt_req.Text = "의뢰량";
            this.lb_qt_req.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_QT_ATP
            // 
            this.tb_QT_ATP.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.tb_QT_ATP.Enabled = false;
            this.tb_QT_ATP.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tb_QT_ATP.Location = new System.Drawing.Point(615, 3);
            this.tb_QT_ATP.Mask = null;
            this.tb_QT_ATP.Name = "tb_QT_ATP";
            this.tb_QT_ATP.NullString = "0";
            this.tb_QT_ATP.PositiveColor = System.Drawing.Color.Black;
            this.tb_QT_ATP.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tb_QT_ATP.Size = new System.Drawing.Size(80, 21);
            this.tb_QT_ATP.TabIndex = 13;
            this.tb_QT_ATP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tb_QT_ATP.UseKeyEnter = true;
            this.tb_QT_ATP.UseKeyF3 = true;
            // 
            // btn_RE_APP
            // 
            this.btn_RE_APP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_RE_APP.BackColor = System.Drawing.Color.White;
            this.btn_RE_APP.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.btn_RE_APP.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.btn_RE_APP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_RE_APP.Location = new System.Drawing.Point(65, 1);
            this.btn_RE_APP.Name = "btn_RE_APP";
            this.btn_RE_APP.Size = new System.Drawing.Size(90, 24);
            this.btn_RE_APP.TabIndex = 5;
            this.btn_RE_APP.TabStop = false;
            this.btn_RE_APP.Text = "품의적용";
            this.btn_RE_APP.UseVisualStyleBackColor = false;
            this.btn_RE_APP.Click += new System.EventHandler(this.btn_RE_APP_Click);
            // 
            // m_gridTmp
            // 
            this.m_gridTmp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_gridTmp.Controls.Add(this.btn_RE_APP);
            this.m_gridTmp.Controls.Add(this.btn_RE_PR);
            this.m_gridTmp.Location = new System.Drawing.Point(546, 3);
            this.m_gridTmp.Name = "m_gridTmp";
            this.m_gridTmp.Size = new System.Drawing.Size(244, 25);
            this.m_gridTmp.TabIndex = 311;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panelExt1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.m_gridTmp, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.m_pnlGrid, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel9, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 59);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(793, 561);
            this.tableLayoutPanel1.TabIndex = 310;
            // 
            // panelExt1
            // 
            this.panelExt1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelExt1.Controls.Add(this.btn_RE_PJT);
            this.panelExt1.Controls.Add(this.btn_ITEM_EXP);
            this.panelExt1.Controls.Add(this.btn_delete);
            this.panelExt1.Controls.Add(this.btn_insert);
            this.panelExt1.Location = new System.Drawing.Point(464, 189);
            this.panelExt1.Name = "panelExt1";
            this.panelExt1.Size = new System.Drawing.Size(326, 24);
            this.panelExt1.TabIndex = 311;
            // 
            // lb_CD_DEPT
            // 
            this.lb_CD_DEPT.BackColor = System.Drawing.Color.Transparent;
            this.lb_CD_DEPT.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_CD_DEPT.Location = new System.Drawing.Point(2, 54);
            this.lb_CD_DEPT.Name = "lb_CD_DEPT";
            this.lb_CD_DEPT.Resizeble = true;
            this.lb_CD_DEPT.Size = new System.Drawing.Size(75, 18);
            this.lb_CD_DEPT.TabIndex = 29;
            this.lb_CD_DEPT.Text = "담당부서";
            this.lb_CD_DEPT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_CD_DEPT
            // 
            this.tb_CD_DEPT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.tb_CD_DEPT.Enabled = false;
            this.tb_CD_DEPT.Location = new System.Drawing.Point(616, 53);
            this.tb_CD_DEPT.Name = "tb_CD_DEPT";
            this.tb_CD_DEPT.SelectedAllEnabled = false;
            this.tb_CD_DEPT.Size = new System.Drawing.Size(156, 21);
            this.tb_CD_DEPT.TabIndex = 308;
            this.tb_CD_DEPT.UseKeyEnter = true;
            this.tb_CD_DEPT.UseKeyF3 = true;
            // 
            // P_PU_PO_REG2
            // 
            this.Controls.Add(this.tableLayoutPanel1);
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Name = "P_PU_PO_REG2_OLD";
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tb_NM_EXCH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_TAX)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel23.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rbtn_PRI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtn_All)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_DT_PO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Ty1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable6)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb_QT_PO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_QT_INV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_QT_REQ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_QT_ATP)).EndInit();
            this.m_gridTmp.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelExt1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        #endregion

        #region ♣ 초기화

        #region -> Page_Load
        private void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                //페이지를 로드 하는중입니다.
                this.ShowStatusBarMessage(1);
                this.SetProgressBarValue(100, 10);

                InitControl();
                this.SetProgressBarValue(100, 50);
                InitGrid();

                this.SetProgressBarValue(100, 70);
                Application.DoEvents();
                this.SetProgressBarValue(100, 100);

            }
            catch (coDbException ex)
            {
                this.ShowErrorMessage(ex, this.PageName);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex, this.PageName);
            }
            finally
            {
                this.ToolBarSearchButtonEnabled = true;
                this.ShowStatusBarMessage(0);
                this.SetProgressBarValue(100, 0);
                Cursor.Current = Cursors.Default;
            }
        }
        #endregion

        #region -> InitControl

        private void InitControl()
        {
            try
            {

                lb_CD_PJT.Text = this.MainFrameInterface.GetDataDictionaryItem("PU", "CD_PJT");
                lb_DC.Text = this.MainFrameInterface.GetDataDictionaryItem("PU", "DC");
                lb_DT_PO.Text = this.MainFrameInterface.GetDataDictionaryItem("PU", "DT_PO");
                lb_FG_PAYMENT.Text = this.MainFrameInterface.GetDataDictionaryItem("PU", "FG_PAYMENT");

                lb_FG_PO_TR.Text = this.MainFrameInterface.GetDataDictionaryItem("PU", "PO_PT");
                lb_FG_TAX.Text = this.MainFrameInterface.GetDataDictionaryItem("PU", "FG_TAX");
                lb_FG_UM.Text = this.MainFrameInterface.GetDataDictionaryItem("PU", "FG_UM");
                lb_NM_EXCH.Text = this.MainFrameInterface.GetDataDictionaryItem("PU", "NM_EXCH");
                lb_NM_PARTNER.Text = this.MainFrameInterface.GetDataDictionaryItem("PU", "NM_PARTNER");
                lb_NM_PURGRP.Text = this.MainFrameInterface.GetDataDictionaryItem("PU", "NM_PURGRP");
                lb_NO_PO.Text = this.MainFrameInterface.GetDataDictionaryItem("PU", "NO_PO");
                lb_TP_TAX.Text = this.MainFrameInterface.GetDataDictionaryItem("PU", "TP_TAX");


                rbtn_All.Text = this.MainFrameInterface.GetDataDictionaryItem("PU", "T_IV");
                rbtn_PRI.Text = this.MainFrameInterface.GetDataDictionaryItem("PU", "M_IV");
                lb_Tax_Type.Text = this.MainFrameInterface.GetDataDictionaryItem("PU", "FG_TAXP");
                lb_TAX_RATE.Text = this.MainFrameInterface.GetDataDictionaryItem("PU", "RT_VAT");


                btn_RE_PJT.Text = this.MainFrameInterface.GetDataDictionaryItem("PU", "RE_PJT");
                btn_ITEM_EXP.Text = this.MainFrameInterface.GetDataDictionaryItem("PU", "ITEM_EXP");
                btn_RE_PR.Text = this.MainFrameInterface.GetDataDictionaryItem("PU", "RE_PR");
                btn_insert.Text = this.MainFrameInterface.GetDataDictionaryItem("CM", "ADD");
                btn_delete.Text = this.MainFrameInterface.GetDataDictionaryItem("CM", "DEL");
                btn_RE_APP.Text = this.MainFrameInterface.GetDataDictionaryItem("PU", "APPLY_APP");
                ib_NO_EMP.Text = this.MainFrameInterface.GetDataDictionaryItem("PU", "NO_EMP");

                lb_NM_PLANT.Text = this.MainFrameInterface.GetDataDictionaryItem("PU", "NM_PLANT");


                lb_qt_inv.Text = this.MainFrameInterface.GetDataDictionaryItem("PU", "QT_INV");
                lb_qt_po.Text = this.MainFrameInterface.GetDataDictionaryItem("PU", "QT_PO");
                lb_qt_req.Text = this.MainFrameInterface.GetDataDictionaryItem("PU", "QT_REQ");
                lb_qt_atp.Text = this.MainFrameInterface.GetDataDictionaryItem("PU", "QT_ATP");
                btn_CM_INV.Text = this.MainFrameInterface.GetDataDictionaryItem("PU", "CM_INV");


                _page_state = "Added";


                tb_CD_DEPT.Tag = this.LoginInfo.DeptCode;
                tb_CD_DEPT.Text = this.LoginInfo.DeptName.ToString();
                tb_NO_EMP.CodeName = this.LoginInfo.EmployeeName;
                tb_NO_EMP.CodeValue = this.LoginInfo.EmployeeNo;


                tb_NM_EXCH.Enabled = false;
                btn_ITEM_EXP.Enabled = false;
                //btn_RART_RECOM.Enabled = false;


                rbtn_All.Checked = true;
                rbtn_PRI.Checked = false;
                tb_TAX.Text = "10";

                tb_DT_PO.Mask = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
                tb_DT_PO.ToDayDate = this.MainFrameInterface.GetDateTimeToday();


                tb_DT_PO.Text = this.MainFrameInterface.GetStringToday;

                SetControlEnabled(true, 2);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region -> GetDDItem

        private string GetDDItem(params string[] colName)  //보여지는 칼럼에 넣기 위한 값을 가져옴
        {
            string temp = "";

            for (int i = 0; i < colName.Length; i++)
            {
                switch (colName[i])		// DataView 의 컬럼이름
                {
                    case "CD_ITEM":
                        temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU", "CD_ITEM");
                        break;
                    case "NM_ITEM":
                        temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU", "NM_ITEM"); ;
                        break;
                    case "STND_MA_ITEM":
                        temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU", "STND_ITEM");
                        break;
                    case "CD_UNIT_MM":
                        temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU", "UNIT_MM");
                        break;
                    case "DT_LIMIT":
                        temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU", "DT_LIMIT");
                        break;
                    case "QT_PO_MM":
                        temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU", "QT_PO_MM");
                        break;
                    case "UM_EX_PO":
                        temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU", "UM");
                        break;
                    case "AM_EX":
                        temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU", "AM");
                        break;
                    case "AM":
                        temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU", "AM_K");
                        break;
                    case "VAT":
                        temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU", "VAT");
                        break;
                    case "CD_SL":
                        temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU", "CD_SL");
                        break;
                    case "NM_SL":
                        temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU", "NM_SL");
                        break;
                    case "NO_CONTRACT":
                        temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU", "NO_CONTRACT"); ;
                        break;
                    case "CD_PJT":
                        temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU", "PJT");
                        break;
                    case "NM_PJT":
                        temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PR", "NM_PROJECT");
                        break;
                    case "NM_SYSDEF":
                        temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU", "PO_STATUS");
                        break;
                    case "UNIT_IM":
                        temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU", "UNIT_MNG");
                        break;
                    case "QT_PO":
                        temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU", "QT_UNIT");
                        break;
                    case "NO_PR":  //요청번호 추가
                        temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU", "NO_PR");
                        break;

                    case "NO_APP": //품의번호 추가
                        temp = temp + " + " + this.MainFrameInterface.GetDataDictionaryItem("PU", "NO_APP");
                        break;


                }
            }

            if (temp == "")
                return "";
            else
                return temp.Substring(3, temp.Length - 3);
        }


        #endregion

        #region -> InitGrid

        private void InitGrid()
        {
            Application.DoEvents();

            _flex = new Dass.FlexGrid.FlexGrid();

            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();

            this._flex.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
            this._flex.AutoResize = false;
            this._flex.BackColor = System.Drawing.SystemColors.Window;
            this._flex.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex.Name = "_flex";
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(785, 553);
            this._flex.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(@"Normal{Font:굴림체, 9pt;Trimming:EllipsisCharacter;}	Fixed{BackColor:Control;ForeColor:ControlText;TextAlign:CenterCenter;Trimming:EllipsisCharacter;Border:Flat,1,ControlDark,Both;}	Highlight{BackColor:Highlight;ForeColor:HighlightText;}	Search{BackColor:Highlight;ForeColor:HighlightText;}	Frozen{BackColor:Beige;}	EmptyArea{BackColor:AppWorkspace;Border:Flat,1,ControlDarkDark,Both;}	GrandTotal{BackColor:Black;ForeColor:White;}	Subtotal0{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal1{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal2{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal3{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal4{BackColor:ControlDarkDark;ForeColor:White;}	Subtotal5{BackColor:ControlDarkDark;ForeColor:White;}	");
            this._flex.TabIndex = 0;
            m_pnlGrid.Controls.Add(this._flex);
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();

            _flex.Redraw = false;

            //_flex.BeginSetting(1,1,true);
            _flex.Rows.Count = 1;			// 총 Row 수
            _flex.Rows.Fixed = 1;			// FixedRow 수
            _flex.Cols.Count = 20;			// 총 Col 수
            _flex.Cols.Fixed = 1;			// FixedCol 수			
            _flex.Rows.DefaultSize = 20;
            _flex.Cols[0].Width = 50;


            _flex.Cols[1].Name = "CD_ITEM";
            _flex.Cols[1].DataType = typeof(string);
            _flex.Cols[1].Width = 120;
            _flex.SetColMaxLength("CD_ITEM", 20);


            _flex.Cols[2].Name = "NM_ITEM";
            _flex.Cols[2].DataType = typeof(string);
            _flex.Cols[2].AllowEditing = false;
            _flex.Cols[2].Width = 150;

            _flex.Cols[3].Name = "STND_MA_ITEM";
            _flex.Cols[3].DataType = typeof(string);
            _flex.Cols[3].AllowEditing = false;
            _flex.Cols[3].Width = 80;

            _flex.Cols[4].Name = "CD_UNIT_MM";
            _flex.Cols[4].DataType = typeof(string);
            _flex.Cols[4].AllowEditing = false;
            _flex.Cols[4].Width = 100;


            _flex.Cols[5].Name = "DT_LIMIT";
            _flex.Cols[5].DataType = typeof(string);
            _flex.Cols[5].Width = 100;
            _flex.Cols[5].EditMask = this.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT).Replace("#", "9");

            //_flex.Cols[6].Format = @"####\/##\/##";
            _flex.Cols[5].Format = this.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            _flex.Cols[5].TextAlign = TextAlignEnum.CenterCenter;
            _flex.SetStringFormatCol("DT_LIMIT");
            _flex.SetNoMaskSaveCol("DT_LIMIT");


            _flex.Cols[6].Name = "QT_PO_MM";
            _flex.Cols[6].DataType = typeof(decimal);
            _flex.Cols[6].Width = 120;
            _flex.Cols[6].TextAlign = TextAlignEnum.RightCenter;
            _flex.Cols[6].Format = this.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY, FormatFgType.INSERT);
            _flex.SetColMaxLength("QT_PO_MM", 17);

            _flex.Cols[7].Name = "UM_EX_PO";
            _flex.Cols[7].DataType = typeof(decimal);
            _flex.Cols[7].Width = 100;
            _flex.Cols[7].TextAlign = TextAlignEnum.RightCenter;
            _flex.Cols[7].Format = this.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.FOREIGN_UNIT_COST, FormatFgType.INSERT);
            _flex.SetColMaxLength("UM_EX_PO", 17);


            _flex.Cols[8].Name = "AM_EX";
            _flex.Cols[8].DataType = typeof(decimal);
            _flex.Cols[8].Width = 150;
            _flex.Cols[8].TextAlign = TextAlignEnum.RightCenter;
            _flex.Cols[8].Format = this.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.FOREIGN_MONEY, FormatFgType.INSERT);
            _flex.SetColMaxLength("AM_EX", 17);



            _flex.Cols[9].Name = "AM";
            _flex.Cols[9].DataType = typeof(decimal);
            _flex.Cols[9].AllowEditing = false;
            _flex.Cols[9].Width = 150;
            _flex.Cols[9].TextAlign = TextAlignEnum.RightCenter;
            _flex.Cols[9].Format = this.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.MONEY, FormatFgType.INSERT);
            _flex.SetColMaxLength("AM", 17);


            _flex.Cols[10].Name = "VAT";
            _flex.Cols[10].DataType = typeof(decimal);
            _flex.Cols[10].Width = 150;
            _flex.Cols[10].TextAlign = TextAlignEnum.RightCenter;
            _flex.Cols[10].Format = this.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.MONEY, FormatFgType.INSERT);
            _flex.SetColMaxLength("VAT", 17);



            _flex.Cols[11].Name = "CD_SL";
            _flex.Cols[11].DataType = typeof(string);

            _flex.Cols[11].Width = 100;
            _flex.SetColMaxLength("CD_SL", 7);

            _flex.Cols[12].Name = "NM_SL";
            _flex.Cols[12].DataType = typeof(string);
            _flex.Cols[12].AllowEditing = false;
            _flex.Cols[12].Width = 120;


            //_flex.Cols[13].Name = "NO_CONTRACT";
            //_flex.Cols[13].DataType = typeof(string);
            //_flex.Cols[13].AllowEditing = false;
            //_flex.Cols[13].Width = 100;	

            _flex.Cols[13].Name = "CD_PJT";
            _flex.Cols[13].DataType = typeof(string);
            _flex.Cols[13].Width = 120;
            _flex.SetColMaxLength("CD_PJT", 20);


            _flex.Cols[14].Name = "NM_PJT";
            _flex.Cols[14].DataType = typeof(string);
            _flex.Cols[14].AllowEditing = false;
            _flex.Cols[14].Width = 120;


            _flex.Cols[15].Name = "NM_SYSDEF";
            _flex.Cols[15].DataType = typeof(string);
            _flex.Cols[15].AllowEditing = false;
            _flex.Cols[15].Width = 80;

            _flex.Cols[16].Name = "UNIT_IM";
            _flex.Cols[16].DataType = typeof(string);
            _flex.Cols[16].AllowEditing = false;
            _flex.Cols[16].Width = 80;

            _flex.Cols[17].Name = "QT_PO";
            _flex.Cols[17].DataType = typeof(decimal);
            _flex.Cols[17].Width = 120;
            _flex.Cols[17].TextAlign = TextAlignEnum.RightCenter;
            _flex.Cols[17].AllowEditing = false;
            _flex.Cols[17].Format = this.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY, FormatFgType.INSERT);
            _flex.SetColMaxLength("QT_PO", 17);


            _flex.Cols[18].Name = "NO_PR";
            _flex.Cols[18].DataType = typeof(string);
            _flex.Cols[18].AllowEditing = false;
            _flex.Cols[18].Width = 120;

            _flex.Cols[19].Name = "NO_APP";
            _flex.Cols[19].DataType = typeof(string);
            _flex.Cols[19].AllowEditing = false;
            _flex.Cols[19].Width = 120;


            //_flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.Top);

            _flex.AllowSorting = AllowSortingEnum.None;
            _flex.SumPosition = SumPositionEnum.Top;

            _flex.NewRowEditable = false;
            _flex.EnterKeyAddRow = true;

            _flex.SetExceptSumCol("QT_PO_MM");
            _flex.SetExceptSumCol("QT_PO");
            _flex.SetExceptSumCol("UM_EX_PO");
            _flex.SetCodeHelpCol("CD_ITEM", "DT_LIMIT", "CD_SL", "CD_PJT");



            _flex.GridStyle = GridStyleEnum.Green;

            this.SetUserGrid(this._flex);

            // 그리드 헤더캡션 표시하기
            for (int i = 0; i <= _flex.Cols.Count - 1; i++)
                _flex[0, i] = GetDDItem(_flex.Cols[i].Name);


            _flex.Redraw = true;


            _flex.AddRow += new System.EventHandler(this.btn_insert_Click);
            //	_flex.HelpClick += new System.EventHandler(this.OnShowHelp);
            _flex.CodeHelp += new Dass.FlexGrid.CodeHelpEventHandler(_flex_CodeHelp);

            _flex.ValidateEdit += new C1.Win.C1FlexGrid.ValidateEditEventHandler(_flex_ValidateEdit);
            _flex.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(_flex_AfterRowColChange);

            _flex.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);

        }


        #endregion

        #region -> Page_Paint

        private void Page_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            try	//반드시 예외처리 할것!
            {
                if (_isPainted == false)	// 페인트 이벤트를 총 한번만 호출하도록 함
                {
                    this._isPainted = true; //로드 된적이 있다.


                    // 콤보박스 초기화
                    InitCombo();

                    _flex.Redraw = false;
                    _flex.BindingStart();
                    _flex.DataSource = new DataView(ds_Ty1.Tables[0]);
                    _flex.BindingEnd();
                    _flex.Redraw = true;

                    if (is_PopUp)
                    {
                        Fill_REGPO(_nopoPop, "OK");
                        tb_NO_PO.Enabled = false;
                    }

                    this.Enabled = true; //페이지 전체 활성
                    tb_DT_PO.Focus();

                }

                //	base.OnPaint(e);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex, this.PageName);
            }
            finally
            {
                this.Enabled = true;
            }
        }
        #endregion

        #region -> InitCombo
        /// <summary>
        /// 콤보 박스 셋팅
        /// </summary>
        private void InitCombo()
        {
            try
            {

                //				string[] lsa_args = {"N;PU_C000001", "C;CC_MA_COMMON014", "N;PU_C000005", "N;MA_B000005","N;PU_C000009","S;MA_B000004","N;PU_C000014","P_N;"};
                //				object[] args = { this.LoginInfo.CompanyCode, lsa_args};
                //				_dsCombo = (DataSet)this.MainFrameInterface.InvokeRemoteMethod("MasterCommon", "master.CC_MA_COMBO", "CC_MA_COMBO.rem", "SettingCombos", args);

                _dsCombo = this.GetComboData("N;PU_C000001", "S;MA_CODEDTL_003", "N;PU_C000005", "N;MA_B000005", "N;PU_C000009", "S;MA_B000004", "N;PU_C000014", "NC;MA_PLANT");

                // 단가유형						
                cbo_FG_UM.DataSource = _dsCombo.Tables[0];
                cbo_FG_UM.DisplayMember = "NAME";
                cbo_FG_UM.ValueMember = "CODE";

                // 과세구분			
                cbo_FG_TAX.DataSource = _dsCombo.Tables[1];
                cbo_FG_TAX.DisplayMember = "NAME";
                cbo_FG_TAX.ValueMember = "CODE";
                cbo_FG_TAX.SelectedIndex = 0;




                // 부가세여부		
                cbo_TP_TAX.DataSource = _dsCombo.Tables[2];
                cbo_TP_TAX.DisplayMember = "NAME";
                cbo_TP_TAX.ValueMember = "CODE";
                cbo_TP_TAX.SelectedIndex = 1;


                // 환정보		
                cbo_NM_EXCH.DataSource = _dsCombo.Tables[3];
                cbo_NM_EXCH.DisplayMember = "NAME";
                cbo_NM_EXCH.ValueMember = "CODE";

                // 지급조건
                cbo_PAYment.DataSource = _dsCombo.Tables[6];
                cbo_PAYment.DisplayMember = "NAME";
                cbo_PAYment.ValueMember = "CODE";

                // 공장
                cbo_CD_PLANT.DataSource = _dsCombo.Tables[7];
                cbo_CD_PLANT.DisplayMember = "NAME";
                cbo_CD_PLANT.ValueMember = "CODE";



                try
                {
                    tb_TAX.Text = _dsCombo.Tables[1].Rows[0]["VAT_RATE"].ToString();
                }
                catch
                {
                }


                DataTable ldt_Unit_po = _dsCombo.Tables[5];

                _flex.SetDataMap("CD_UNIT_MM", ldt_Unit_po, "CODE", "CODE");


                DataRow[] ldrs_arg = _dsCombo.Tables[4].Select("CODE ='P'");

                if (ldrs_arg != null && ldrs_arg.Length > 0)
                {
                    _ComfirmState = ldrs_arg[0]["NAME"].ToString();
                }
            }
            catch (coDbException ex)
            {
                throw ex;
                //this.ShowErrorMessage(ex, this.PageName);
            }
            catch (Exception ex)
            {
                throw ex;
                //this.ShowErrorMessage(ex, this.PageName);
            }
            finally
            {
                this.Enabled = true;

                this.ToolBarAddButtonEnabled = true;
                this.ToolBarSearchButtonEnabled = true;
                this.ToolBarDeleteButtonEnabled = true;
                this.ToolBarSaveButtonEnabled = true;

                this.Cursor = Cursors.Default;
            }
        }
        // 

        #endregion

        #endregion

        #region ♣ 저장관련

        private void GetPU_RCV_Save(DataTable pdt_Head, DataTable pdt_Line)
        {
            try
            {

                gc_Rcvh = new pur.CDT_PU_RCVH();

                DataRow[] ldr_temp;

                // 의뢰 정보 수집
                for (int i = 0; i < pdt_Line.Rows.Count; i++)
                {
                    if (pdt_Line.Rows[i].RowState.ToString() != "Deleted")
                    {
                        // 이전에 입력된 것은 제외시키기 위해
                        if (pdt_Line.Rows[i]["NO_RCV"].ToString().Trim() == "")
                        {
                            string no_seq = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PU", "04", tb_DT_PO.Text.Substring(0, 6));
                            // 공장  ldt_result 분리
                            ldr_temp = pdt_Line.Select("CD_PLANT ='" + pdt_Line.Rows[i]["CD_PLANT"].ToString() + "'");

                            //의뢰 채번
                            //	ls_NORCV = GetNO_RCV(pdt_Head.Rows[0]["CD_COMPANY"].ToString());									
                            DataRow newRow = gc_Rcvh.DT_PURCVH.NewRow();
                            newRow["NO_RCV"] = no_seq;
                            newRow["CD_COMPANY"] = pdt_Head.Rows[0]["CD_COMPANY"].ToString();
                            newRow["CD_PLANT"] = pdt_Line.Rows[i]["CD_PLANT"].ToString();
                            newRow["CD_PARTNER"] = pdt_Head.Rows[0]["CD_PARTNER"].ToString();
                            newRow["DT_REQ"] = pdt_Head.Rows[0]["DT_PO"].ToString();
                            newRow["NO_EMP"] = pdt_Head.Rows[0]["NO_EMP"].ToString();
                            newRow["FG_TRANS"] = pdt_Head.Rows[0]["FG_TRANS"].ToString();
                            newRow["FG_PROCESS"] = pdt_Head.Rows[0]["FG_TAXP"].ToString();
                            newRow["YN_AM"] = pdt_Head.Rows[0]["YN_AM"].ToString();
                            newRow["CD_EXCH"] = pdt_Head.Rows[0]["CD_EXCH"].ToString();
                            newRow["YN_RETURN"] = "N";
                            newRow["CD_SL"] = pdt_Line.Rows[i]["CD_SL"].ToString();
                            newRow["YN_AUTORCV"] = pdt_Line.Rows[0]["YN_AUTORCV"].ToString();
                            newRow["DT_IO"] = pdt_Head.Rows[0]["DT_PO"].ToString();
                            newRow["CD_DEPT"] = pdt_Head.Rows[0]["CD_DEPT"].ToString();

                            //	newRow["ID_INSERT"] = ps_userid;
                            gc_Rcvh.DT_PURCVH.Rows.Add(newRow);

                            for (int j = 0; j < ldr_temp.Length; j++)
                            {
                                ldr_temp[j]["NO_RCV"] = no_seq;
                                ldr_temp[j]["NO_RCVLINE"] = j + 1;
                                ldr_temp[j]["FG_POCON"] = "002";
                            }
                        }
                    }
                }



                if (gc_Rcvh.DT_PURCVH != null)
                {
                    if (gc_Rcvh.DT_PURCVH.Rows.Count > 0)
                    {
                        for (int i = 0; i < gc_Rcvh.DT_PURCVH.Rows.Count; i++)
                        {

                            DataTable ldt_CopyCHG = pdt_Line.Clone();
                            DataRow[] ldra_temp = pdt_Line.Select("NO_RCV ='" + gc_Rcvh.DT_PURCVH.Rows[i]["NO_RCV"].ToString() + "'");

                            if (ldra_temp != null && ldra_temp.Length > 0)
                            {
                                for (int j = 0; j < ldra_temp.Length; j++)
                                {
                                    ldt_CopyCHG.ImportRow(ldra_temp[j]);
                                }

                                gc_Rcvl = new pur.CDT_PU_RCV();


                                DataRow row;
                                for (int k = 0; k < ldt_CopyCHG.Rows.Count; k++)
                                {
                                    row = ldt_CopyCHG.Rows[k];
                                    DataRow newrow = gc_Rcvl.DT_PURCV.NewRow();

                                    newrow["NO_RCV"] = gc_Rcvh.DT_PURCVH.Rows[i]["NO_RCV"].ToString();
                                    newrow["NO_LINE"] = k + 1;
                                    newrow["CD_COMPANY"] = pdt_Head.Rows[0]["CD_COMPANY"].ToString();
                                    newrow["DT_IO"] = gc_Rcvh.DT_PURCVH.Rows[i]["DT_IO"].ToString();

                                    //newrow["CD_PLANT"] =row["CD_PLANT"].ToString();
                                    newrow["NO_PO"] = row["NO_PO"].ToString();
                                    newrow["NO_POLINE"] = row["NO_LINE"];
                                    newrow["CD_PURGRP"] = pdt_Head.Rows[0]["CD_PURGRP"].ToString();
                                    newrow["CD_ITEM"] = row["CD_ITEM"].ToString();
                                    newrow["CD_UNIT_MM"] = row["CD_UNIT_MM"].ToString();
                                    newrow["QT_REQ_MM"] = row["QT_PO_MM"];
                                    newrow["QT_REQ"] = row["QT_PO"];
                                    newrow["DT_LIMIT"] = row["DT_LIMIT"];
                                    newrow["CD_EXCH"] = pdt_Head.Rows[0]["CD_EXCH"].ToString();
                                    newrow["RT_EXCH"] = pdt_Head.Rows[0]["RT_EXCH"];
                                    newrow["YN_INSP"] = "N";
                                    newrow["UM_EX_PO"] = row["UM_EX_PO"];
                                    newrow["UM_EX"] = row["UM_EX"];
                                    newrow["AM_EX"] = row["AM_EX"];
                                    newrow["AM"] = row["AM"];

                                    newrow["AM_EXREQ"] = row["AM_EX"].ToString();
                                    newrow["AM_REQ"] = row["AM"].ToString();


                                    newrow["VAT"] = row["VAT"];
                                    newrow["UM"] = row["UM"];
                                    newrow["UM"] = row["UM"];
                                    newrow["CD_PJT"] = row["CD_PJT"].ToString();
                                    newrow["YN_PURCHASE"] = row["YN_PURCHASE"].ToString();
                                    newrow["YN_RETURN"] = row["YN_RETURN"].ToString();
                                    newrow["FG_TPPURCHASE"] = row["FG_TPPURCHASE"].ToString();
                                    newrow["FG_TAXP"] = pdt_Head.Rows[0]["FG_TAXP"].ToString();
                                    newrow["FG_TAX"] = row["FG_TAX"].ToString();
                                    newrow["FG_RCV"] = row["FG_RCV"].ToString();
                                    newrow["FG_TRANS"] = row["FG_TRANS"].ToString();
                                    newrow["YN_AUTORCV"] = row["YN_AUTORCV"].ToString();
                                    newrow["YN_REQ"] = row["YN_REQ"].ToString();
                                    newrow["CD_SL"] = row["CD_SL"].ToString();
                                    newrow["NO_EMP"] = pdt_Head.Rows[0]["NO_EMP"].ToString();
                                    gc_Rcvl.DT_PURCV.Rows.Add(newrow);

                                }


                                //								gc_Rcvh.Rows[i]["NO_RCV"] ="";
                                //								DataTable ldt_Head = gc_Rcvh.Clone();
                                //								ldt_Head.ImportRow(gc_Rcvh.Rows[i]);
                                //
                                //								object[] ls_result = cc_pu_req.SaveREQ(ldt_Head,gc_Rcvl.DT_PURCV,null,ps_user);
                                //								if( (int)ls_result[0] <0)
                                //								{
                                //									ContextUtil.SetAbort();
                                //									ApplicationException lex = new ApplicationException("CM_M100010");
                                //									lex.Source = "100000";
                                //									lex.HelpLink = "InsertRCV : cc_pu_req.SaveREQ";
                                //									throw lex;
                                //								}
                                //								li_result = (int)ls_result[0];
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        #region -> IsChanged

        private bool IsChanged(string gubun)
        {
            if (gubun == null)
                return _flex.IsDataChanged;

            return false;
        }
        #endregion

        private bool MsgAndSave(bool displayDialog, bool isExit)
        {
            return MsgAndSave(displayDialog, isExit, false);
        }


        #region -> MsgAndSave

        private bool MsgAndSave(bool displayDialog, bool isExit, bool isSave)
        {
            bool isSaved = false;

            if (isSave)
            {
                isSaved = Save(isSave);
                return isSaved;
            }

            if (!IsChanged(null)) return true;

            //	bool isSaved = false;

            if (!displayDialog)								// 저장 버튼을 클릭한 경우이므로 다이알로그는 필요없음
            {
                if (IsChanged(null)) isSaved = Save();

                return isSaved;
            }

            DialogResult result;

            if (isExit)
            {
                result = result = this.ShowMessage("QY3_002");// this.ShowMessageBox(1001, this.PageName);	
                if (result == DialogResult.No)
                    return true;
                if (result == DialogResult.Cancel)
                    return false;
            }
            else
            {

                result = this.ShowMessage("QY2_001"); //MessageBoxEx.Show(this.GetMessageDictionaryItem("MA_M000073"), this.PageName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);			
                if (result == DialogResult.No)
                    return true;
            }

            Application.DoEvents();		// 대화상자 즉시 사라지게

            // "예"를 선택한 경우
            if (IsChanged(null)) isSaved = Save();

            return isSaved;
        }

        #endregion

        #region -> Check

        private bool Check()
        {
            int row;
            string colName;
            if (_flex.DataTable.Rows.Count > 0)
            {
                // 필요없는 행 삭제 : 리턴값이 False 이면 삭제후에 변경된 내용이 없다는 뜻
                if (!_flex.CheckView_DeleteIfNull(new string[] { "CD_ITEM" }, "OR"))
                {
                    // 변경된 내용이 없습니다.
                    this.ShowMessage("IK1_013");
                    return false;
                }

                // 필수입력항목 체크
                if (_flex.CheckView_HasNull(new string[] { "CD_ITEM", "DT_LIMIT" }, out row, out colName, "OR"))
                {
                    this.ShowMessage("WK1_004", GetDDItem(colName));
                    _flex.Select(row, colName);
                    _flex.Focus();
                    return false;
                }
                return true;
            }
            else
                return false;
        }

        #endregion

        #region -> Save
        private bool Save()
        {
            return Save(false);
        }

        /// <summary>
        /// 저장 함수
        /// </summary>
        /// <returns></returns>
        private bool Save(bool isSave)
        {
            try
            {
                lb_DC.Focus();

                // 발주등록 필드 체크
                if (!FieldCheck_Head())
                {
                    return false;
                }

                if (!Check())		// 널값체크 및 중복값체크 등에서 문제가 발생한 경우
                    return false;


                if (!DataAddForPOL())
                    return false;

                //합계 계산
                SUMFunction();

                // 헤더 정보 수집
                InDataHeadValue();

                DataTable ldt_result = _flex.GetChanges();
                if ((ldt_result != null && ldt_result.Rows.Count > 0) || isSave)
                {
                    bool is_RcvSave = false;
                    string no_seq = tb_NO_PO.Text;
                    if (_page_state == "Added")
                    {
                        no_seq = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PU", "03", tb_DT_PO.Text.Substring(0, 6));
                        ds_Ty1.Tables[3].Rows[0].BeginEdit();
                        ds_Ty1.Tables[3].Rows[0]["NO_PO"] = no_seq;
                        ds_Ty1.Tables[3].Rows[0].EndEdit();

                        for (int i = 0; i < _flex.DataView.Count; i++)
                        {
                            _flex.DataView[i].BeginEdit();
                            _flex.DataView[i]["NO_PO"] = no_seq;
                            _flex.DataView[i].EndEdit();
                        }


                        ldt_result = _flex.GetChanges();

                        if (ldt_result.Rows[0]["YN_REQ"].ToString().Trim() == "N")
                        {
                            gc_Rcvh.DT_PURCVH.Clear();
                            gc_Rcvl.DT_PURCV.Clear();
                            GetPU_RCV_Save(ds_Ty1.Tables[3], ldt_result);
                            is_RcvSave = true;
                        }

                    }

                    if (ldt_result != null && ldt_result.Rows.Count > 0)
                    {

                        DataRow lr_row;
                        for (int i = 0; i < ldt_result.Rows.Count; i++)
                        {
                            if (ldt_result.Rows[i].RowState.ToString() != "Deleted")
                            {
                                lr_row = ldt_result.Rows[i];

                                lr_row.BeginEdit();
                                if (lr_row["YN_ORDER"].ToString().Trim() == "Y" || lr_row["YN_REQ"].ToString().Trim() == "N")
                                {

                                    lr_row["FG_POST"] = "R";
                                    lr_row["FG_POCON"] = "002";
                                }

                                lr_row.EndEdit();
                            }
                        }
                    }

                    SpInfoCollection sic = new SpInfoCollection();

                    Duzon.Common.Util.SpInfo si = new Duzon.Common.Util.SpInfo();
                    si.DataValue = ds_Ty1.Tables[3]; 					//저장할 데이터 테이블
                    si.SpNameInsert = "SP_PU_POH_INSERT";			//Insert 프로시저명
                    si.SpNameUpdate = "SP_PU_POH_UPDATE";			//Update 프로시저명					


                    /*위 데이터테이블에서 각 프로시저에서 사용할 파라미터 Value에 대한 컬럼을 정의한다.*/
                    si.SpParamsInsert = new string[] { "NO_PO","CD_COMPANY","CD_PLANT","CD_PARTNER","DT_PO","CD_PURGRP","NO_EMP","CD_TPPO","FG_UM","FG_PAYMENT","FG_TAX",
														 "TP_UM_TAX","CD_PJT","CD_EXCH","RT_EXCH","AM_EX","AM","VAT","DC50_PO","TP_PROCESS", "FG_TAXP","YN_AM",
														"DTS_INSERT1","ID_INSERT1","FG_TRANS" };
                    si.SpParamsUpdate = new string[] { "NO_PO", "CD_COMPANY", "DT_PO", "CD_PURGRP", "NO_EMP", "CD_PJT", "AM_EX", "AM", "VAT", "DC50_PO", "FG_TAXP", "DTS_INSERT1", "ID_INSERT1" };
                    /*데이터테이블에는 존재하지 않지 않는 컬럼이지만 모든 데이터로우에 공통적으로 들어가는 값을 정의한다.*/
                    si.SpParamsValues.Add(ActionState.Insert, "DTS_INSERT1", this.MainFrameInterface.GetStringDetailToday);
                    si.SpParamsValues.Add(ActionState.Insert, "ID_INSERT1", this.LoginInfo.UserID);
                    si.SpParamsValues.Add(ActionState.Update, "DTS_INSERT1", this.MainFrameInterface.GetStringDetailToday);
                    si.SpParamsValues.Add(ActionState.Update, "ID_INSERT1", this.LoginInfo.UserID);

                    sic.Add(si);

                    si = new Duzon.Common.Util.SpInfo();
                    si.DataValue = ldt_result; 					//저장할 데이터 테이블
                    si.SpNameInsert = "SP_PU_POL_INSERT";			//Insert 프로시저명
                    si.SpNameUpdate = "SP_PU_POL_UPDATE";			//Update 프로시저명
                    si.SpNameDelete = "SP_PU_POL_DELETE";

                    /*위 데이터테이블에서 각 프로시저에서 사용할 파라미터 Value에 대한 컬럼을 정의한다.*/
                    si.SpParamsInsert = new string[] { 	"NO_PO", "NO_LINE","CD_COMPANY","CD_PLANT","NO_CONTRACT","NO_CTLINE","NO_PR","NO_PRLINE","FG_TRANS","CD_ITEM",
														 "CD_UNIT_MM","FG_RCV","FG_PURCHASE","DT_LIMIT","QT_PO_MM","QT_PO","QT_REQ","QT_RCV","FG_TAX","UM_EX_PO","UM_EX","AM_EX","UM","AM",
														 "VAT","CD_SL","FG_POST","FG_POCON","YN_RCV","YN_AUTORCV","YN_RETURN","YN_ORDER","YN_SUBCON","YN_IMPORT",
														 "RT_PO","YN_REQ","CD_PJT","NO_APP","NO_APPLINE" };

                    si.SpParamsUpdate = new string[] {  "NO_PO", "NO_LINE","CD_COMPANY","CD_PLANT","CD_UNIT_MM","DT_LIMIT","QT_PO_MM","QT_PO","UM_EX_PO","UM_EX","AM_EX","UM","AM",
														 "VAT","CD_SL","RT_PO","CD_PJT"};

                    si.SpParamsDelete = new string[] { "NO_PO", "NO_LINE", "CD_COMPANY" };


                    /*데이터테이블에는 존재하지 않지 않는 컬럼이지만 모든 데이터로우에 공통적으로 들어가는 값을 정의한다.*/

                    sic.Add(si);

                    // 자동의뢰 
                    if (is_RcvSave)
                    {
                        si = new Duzon.Common.Util.SpInfo();
                        gc_Rcvh.DT_PURCVH.Rows[0]["FG_RCV"] = strFG_RCV;    //자동의뢰일경우 자동의로의 입고형태(수불유형)를 넣어준다.
                        si.DataValue = gc_Rcvh.DT_PURCVH; 					//저장할 데이터 테이블
                        si.SpNameInsert = "SP_PU_RCVH_INSERT";			//Insert 프로시저명
                        si.SpNameUpdate = "SP_PU_RCVH_UPDATE";			//Update 프로시저명					

                        /*위 데이터테이블에서 각 프로시저에서 사용할 파라미터 Value에 대한 컬럼을 정의한다.*/
                        si.SpParamsInsert = new string[] { "NO_RCV", "CD_COMPANY", "CD_PLANT", "CD_PARTNER", "DT_REQ", "NO_EMP", "FG_TRANS", "FG_PROCESS", "CD_EXCH", "CD_SL", "YN_RETURN", "YN_AM", "DC_RMK", "DTS_INSERT1", "ID_INSERT1", "FG_RCV" };
                        si.SpParamsUpdate = new string[] { "CD_COMPANY", "NO_RCV", "DC_RMK", "DTS_INSERT1", "ID_INSERT1" };
                        /*데이터테이블에는 존재하지 않지 않는 컬럼이지만 모든 데이터로우에 공통적으로 들어가는 값을 정의한다.*/
                        si.SpParamsValues.Add(ActionState.Insert, "DTS_INSERT1", this.MainFrameInterface.GetStringDetailToday);
                        si.SpParamsValues.Add(ActionState.Insert, "ID_INSERT1", this.LoginInfo.UserID);
                        si.SpParamsValues.Add(ActionState.Update, "DTS_INSERT1", this.MainFrameInterface.GetStringDetailToday);
                        si.SpParamsValues.Add(ActionState.Update, "ID_INSERT1", this.LoginInfo.UserID);

                        sic.Add(si);

                        si = new Duzon.Common.Util.SpInfo();
                        si.DataValue = gc_Rcvl.DT_PURCV; 					//저장할 데이터 테이블
                        si.SpNameInsert = "SP_PU_REQ_INSERT";			//Insert 프로시저명


                        /*위 데이터테이블에서 각 프로시저에서 사용할 파라미터 Value에 대한 컬럼을 정의한다.*/
                        si.SpParamsInsert = new string[] { 	 "NO_RCV",
															 "NO_LINE", 
															 "CD_COMPANY",
															 "NO_PO",
															 "NO_POLINE", 
															 "CD_PURGRP",
															 "DT_LIMIT",
															 "CD_ITEM",
															 "QT_REQ" ,
															 "YN_INSP" ,
															 "QT_PASS" ,
															 "QT_REJECTION" ,
															 "CD_UNIT_MM",
															 "QT_REQ_MM" ,
															 "CD_EXCH",
															 "RT_EXCH" ,
															 "UM_EX_PO" ,
															 "UM_EX" ,
															 "AM_EX" ,//"AM_EXREQ" ,
															 "UM" ,
															 "AM" ,//"AM_REQ" ,
															 "VAT" ,
															 "RT_CUSTOMS" ,
															 "CD_PJT",
															 "YN_PURCHASE",
															 "YN_RETURN",
															 "FG_TPPURCHASE",
															 "FG_RCV",
															 "FG_TRANS",
															 "FG_TAX",
															 "FG_TAXP",
															 "YN_AUTORCV",////////////////
															 "YN_REQ",
															 "CD_SL",
															 "NO_LC",
															 "NO_LCLINE" ,
															 "RT_SPEC" ,
															 "NO_EMP",
															 "NO_TO",
															 "NO_TO_LINE",
															 "CD_PLANT1","CD_PARTNER1", "DT_REQ1", "DC_RMK"	};



                        si.SpParamsValues.Add(ActionState.Insert, "CD_PLANT1", gc_Rcvh.DT_PURCVH.Rows[0]["CD_PLANT"].ToString());
                        si.SpParamsValues.Add(ActionState.Insert, "CD_PARTNER1", gc_Rcvh.DT_PURCVH.Rows[0]["CD_PARTNER"].ToString());
                        si.SpParamsValues.Add(ActionState.Insert, "DT_REQ1", gc_Rcvh.DT_PURCVH.Rows[0]["DT_REQ"].ToString());
                        si.SpParamsValues.Add(ActionState.Insert, "DC_RMK", string.Empty);

                        /*데이터테이블에는 존재하지 않지 않는 컬럼이지만 모든 데이터로우에 공통적으로 들어가는 값을 정의한다.*/

                        sic.Add(si);

                        // 자동 입고 이면.
                        if (gc_Rcvh.DT_PURCVH.Rows[0]["YN_AUTORCV"].ToString() == "Y" && _page_state == "Added")
                        {
                            string no_ioseq = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PU", "06", tb_DT_PO.Text.Substring(0, 6));

                            si = new Duzon.Common.Util.SpInfo();

                            si.DataValue = gc_Rcvh.DT_PURCVH;  					//저장할 데이터 테이블
                            si.SpNameInsert = "SP_PU_MM_QTIOH_INSERT";			//Insert 프로시저명

                            /*위 데이터테이블에서 각 프로시저에서 사용할 파라미터 Value에 대한 컬럼을 정의한다.*/
                            si.SpParamsInsert = new string[] { "NO_IO1", "CD_COMPANY", "CD_PLANT", "CD_PARTNER", "FG_TRANS", "YN_RETURN", "DT_IO", "GI_PARTNER", "CD_DEPT", "NO_EMP", "DC_RMK", "DTS_INSERT1", "ID_INSERT1", "FG_RCV" };

                            /*데이터테이블에는 존재하지 않지 않는 컬럼이지만 모든 데이터로우에 공통적으로 들어가는 값을 정의한다.*/
                            si.SpParamsValues.Add(ActionState.Insert, "NO_IO1", no_ioseq);
                            si.SpParamsValues.Add(ActionState.Insert, "DTS_INSERT1", this.MainFrameInterface.GetStringDetailToday);
                            si.SpParamsValues.Add(ActionState.Insert, "ID_INSERT1", this.LoginInfo.UserID);
                            sic.Add(si);

                            si = new Duzon.Common.Util.SpInfo();
                            //	si.DataState = DataValueState.Added; 
                            si.DataValue = gc_Rcvl.DT_PURCV; 					//저장할 데이터 테이블
                            si.SpNameInsert = "SP_PU_GR_INSERT";			//Insert 프로시저명
                            //si.spType = "Added";

                            /*위 데이터테이블에서 각 프로시저에서 사용할 파라미터 Value에 대한 컬럼을 정의한다.*/
                            si.SpParamsInsert = new string[] { "YN_RETURN1", "NO_IO1","NO_LINE", "CD_COMPANY", "CD_PLANT1", "CD_SL", "DT_IO", "NO_RCV", "NO_LINE", "NO_PO", "NO_POLINE", "FG_PS1", 
																 "FG_TPPURCHASE", "FG_IO1", "FG_RCV", "FG_TRANS", "FG_TAX", "CD_PARTNER1","CD_ITEM", "QT_REQ","QT_BAD1", "CD_EXCH", "RT_EXCH", "UM_EX", "UM", "VAT", "FG_TAXP",
																 "YN_AM1", "CD_PJT", "NO_LC", "NO_LCLINE", "NO_EMP1", "CD_PURGRP","CD_UNIT_MM", "QT_REQ_MM","QT_BAD_MM1", "UM_EX_PO", "YN_INSP1"};
                            /*데이터테이블에는 존재하지 않지 않는 컬럼이지만 모든 데이터로우에 공통적으로 들어가는 값을 정의한다.*/
                            si.SpParamsValues.Add(ActionState.Insert, "YN_RETURN1", "N");
                            si.SpParamsValues.Add(ActionState.Insert, "CD_PLANT1", gc_Rcvh.DT_PURCVH.Rows[0]["CD_PLANT"].ToString());
                            si.SpParamsValues.Add(ActionState.Insert, "CD_PARTNER1", gc_Rcvh.DT_PURCVH.Rows[0]["CD_PARTNER"].ToString());
                            si.SpParamsValues.Add(ActionState.Insert, "YN_AM1", gc_Rcvh.DT_PURCVH.Rows[0]["YN_AM"].ToString());
                            si.SpParamsValues.Add(ActionState.Insert, "NO_IO1", no_ioseq);
                            si.SpParamsValues.Add(ActionState.Insert, "FG_PS1", "1");
                            si.SpParamsValues.Add(ActionState.Insert, "FG_IO1", "001");
                            si.SpParamsValues.Add(ActionState.Insert, "NO_EMP1", tb_NO_EMP.CodeValue.ToString());
                            si.SpParamsValues.Add(ActionState.Insert, "YN_INSP1", "N");
                            si.SpParamsValues.Add(ActionState.Insert, "QT_BAD1", 0);
                            si.SpParamsValues.Add(ActionState.Insert, "QT_BAD_MM1", 0);
                            sic.Add(si);

                            // 발주 .. 의뢰... 입고 형태
                            object obj = this.Save(sic);
                            ResultData[] result = (ResultData[])obj;
                            if (result[0].Result && result[1].Result && result[2].Result && result[3].Result && result[4].Result && result[5].Result)
                            {
                                tb_NO_PO.Text = no_seq;
                                tb_NO_PO.Enabled = false;

                                SetControlEnabled(false, 1);
                                ChagePoState();
                                tb_DT_PO.Focus();
                                _flex.DataTable.AcceptChanges();

                                this._page_state = "Modified";
                                return true;

                            }
                        }
                        else // 발주 .. 의뢰 형태
                        {
                            object obj = this.Save(sic);
                            ResultData[] result = (ResultData[])obj;
                            if (result[0].Result && result[1].Result && result[2].Result && result[3].Result)
                            {
                                tb_NO_PO.Text = no_seq;
                                tb_NO_PO.Enabled = false;

                                SetControlEnabled(false, 1);
                                ChagePoState();
                                tb_DT_PO.Focus();
                                _flex.DataTable.AcceptChanges();

                                this._page_state = "Modified";
                                return true;

                            }
                        }

                    }
                    else // 발주 형태 
                    {
                        object obj = this.Save(sic);
                        ResultData[] result = (ResultData[])obj;
                        if (result[0].Result && result[1].Result)
                        {
                            //tb_NO_APP.Enabled = false;		
                            tb_NO_PO.Text = no_seq;
                            tb_NO_PO.Enabled = false;

                            SetControlEnabled(false, 1);
                            ChagePoState();
                            tb_DT_PO.Focus();
                            _flex.DataTable.AcceptChanges();

                            this._page_state = "Modified";
                            return true;
                        }

                    }

                }

                    //				DataTable ldt_result = _flex.DataTable.GetChanges();				
                //				if((ldt_result !=null && ldt_result.Rows.Count >0) || isSave )
                //				{
                //
                //					object[] ls_result  = (object[])(this.MainFrameInterface.InvokeRemoteMethod("PurOrderControl", "pur.CC_PU_PO", "CC_PU_PO.rem","SavePuPoSu", 
                //						new object[]{ds_Ty1.Tables[3],ldt_result,null,null,this.LoginInfo.UserID}));
                //
                //					if((int)ls_result[0] >=0)
                //					{              				
                //						tb_NO_PO.Text = (string)ls_result[1].ToString();
                //						_dtsUpdate = (string)ls_result[2].ToString();			
                //						SetControlEnabled(false,1); 
                //
                //						ChagePoState();
                //
                //						tb_DT_PO.Focus();
                //						_flex.DataTable.AcceptChanges();						
                //						this._page_state = "Modified";				
                //						tb_NO_PO.Enabled = false;
                //
                //						return true;
                //					}
                //					return false;						
                //				}
                else
                {
                    this.ShowMessage("MA_M000017");
                    //Duzon.Common.Controls.MessageBoxEx.Show(this.MainFrameInterface.GetMessageDictionaryItem("MA_M000017"));
                }
                return false;

            }
            catch (coDbException ex)
            {
                this.ShowErrorMessage(ex, this.PageName);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex, this.PageName);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
            return false;
        }


        #endregion





        #endregion

        #region ♣ 메인버튼 이벤트


        #region -> DoContinue

        private bool DoContinue()
        {
            if (_flex.Editor != null)
            {
                return _flex.FinishEditing(false);
            }

            return true;
        }

        #endregion

        #region -> 조회버튼클릭
        /// <summary>
        /// 브라우저의 조회 버턴 클릭시 처리부
        /// </summary>
        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {

            try
            {
                if (!DoContinue())
                    return;
                Cursor.Current = Cursors.WaitCursor;

                if (!MsgAndSave(true, false))
                    return;

                Application.DoEvents();

                tb_DT_PO.Focus();

                Cursor.Current = Cursors.WaitCursor;
                pur.P_PU_PO_SUB2 m_dlg = new pur.P_PU_PO_SUB2(this.MainFrameInterface, _dsCombo.Tables[7]);
                Cursor.Current = Cursors.Default;
                if (m_dlg.ShowDialog(this) == DialogResult.OK)
                {
                    //lb_DT_PO.Focus();
                    FieldDataNULL();
                    Fill_REGPO(m_dlg.m_NO_PO, m_dlg.m_btnType);
                    _flex.Focus();
                    tb_NO_PO.Enabled = false;

                }
            }
            catch (coDbException ex)
            {
                this.ShowErrorMessage(ex, this.PageName);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex, this.PageName);
            }
            finally
            {
                this.SetProgressBarValue(100, 0);
                Cursor.Current = Cursors.Default;
                this.ShowStatusBarMessage(0);
                Cursor.Current = Cursors.Default;
            }
        }


        #endregion

        #region -> 삭제버튼클릭
        /// <summary>
        /// 브라우저의 삭제 버턴 클릭시 처리부
        /// </summary>
        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!DoContinue())
                    return;

                Cursor.Current = Cursors.WaitCursor;



                if (_page_state == "Added")
                {
                    FieldDataNULL();
                    _page_state = "Added";
                    Cursor.Current = Cursors.Default;
                    return;

                }

                DialogResult result = this.ShowMessage("QY2_003");

                if (result == DialogResult.Yes)
                {

                    object[] m_obj = new object[2];
                    m_obj[0] = tb_NO_PO.Text;
                    m_obj[1] = this.MainFrameInterface.LoginInfo.CompanyCode;


                    ResultData ret = (ResultData)this.ExecSp("SP_PU_POH_DELETE", m_obj);
                    if (ret.Result)
                    {
                        tb_DT_PO.Focus();
                        _flex.DataTable.AcceptChanges();
                        FieldDataNULL();
                        _page_state = "Added";//CM_M000009

                        this.ShowMessage("IK1_002");
                    }


                    //					int li_result = (int)(this.MainFrameInterface.InvokeRemoteMethod("PurOrderControl", "pur.CC_PU_PO","CC_PU_PO.rem", "DeletePo", m_obj));
                    //		
                    //					//	int li_result = DeletePo(tb_NO_PO.Text,this.MainFrameInterface.LoginInfo.CompanyCode);
                    //
                    //						
                    //					if(li_result >= 0)
                    //					{
                    //						tb_DT_PO.Focus();
                    //
                    //					
                    //						_flex.DataTable.AcceptChanges();
                    //						FieldDataNULL();  
                    //						_page_state="Added";//CM_M000009
                    //							
                    //						this.ShowMessage("IK1_002");
                    //					}					    
                }


            }
            catch (coDbException ex)
            {
                this.ShowErrorMessage(ex, this.PageName);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex, this.PageName);
            }
            finally
            {

                Cursor.Current = Cursors.Default;
            }
        }


        #endregion

        #region -> 저장버튼클릭
        /// <summary>
        /// 브라우저의 저장 버턴 클릭시 처리부
        /// </summary>
        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {

            try
            {
                if (!DoContinue())
                    return;
                Cursor.Current = Cursors.WaitCursor;

                if (MsgAndSave(false, false, true))
                {
                    this.ShowMessage("IK1_001");
                }
            }
            catch (coDbException ex)
            {
                this.ShowErrorMessage(ex, this.PageName);

            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex, this.PageName);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        #endregion

        #region -> 추가버튼클릭
        /// <summary>
        /// 브라우저의 추가 버턴 클릭시 처리부
        /// </summary>
        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {

            try
            {
                if (!DoContinue())
                    return;
                Cursor.Current = Cursors.WaitCursor;

                if (!MsgAndSave(true, false))
                    return;

                FieldDataNULL();
                tb_DT_PO.Focus();

            }
            finally
            {
                _flex.Focus();
                this.ShowStatusBarMessage(4);
            }
        }

        #endregion

        #region -> 종료버튼클릭
        public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
        {

            try
            {
                if (!DoContinue())
                    return false;

                if (!MsgAndSave(true, true))	// 저장이 실패하면
                    return false;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex, this.PageName);
            }
            finally
            {
                this.ShowStatusBarMessage(0);
                this.SetProgressBarValue(100, 0);
            }

            return true;
        }

        #endregion


        #endregion

        #region ♣ 그리드 이벤트 / 메서드

        #region -> _flex_AfterRowColChange

        private void _flex_AfterRowColChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
            try
            {
                //				if(_isChagePossible)
                //				{
                //					if( _flex.DataView[ _flex.DataIndex( _flex.Row)].Row.RowState.ToString() != "Added" && 
                //						( _flex.Cols[_flex.Col].Name == "CD_ITEM"))
                //					{
                //						_flex.AllowEditing = false;
                //					}
                //					else if( _flex.DataView[ _flex.DataIndex( _flex.Row)].Row.RowState.ToString() == "Added"  &&
                //						_flex[_flex.Row,"NO_PR"].ToString().Trim() != ""  && 
                //						( _flex.Cols[_flex.Col].Name == "CD_ITEM"))
                //					{
                //						_flex.AllowEditing = false;
                //					}
                //					else
                //					{
                //						_flex.AllowEditing = true;
                //					}
                //				}
                //				else
                //				{
                //					_flex.AllowEditing = false;
                //				}

                if (e.OldRange.r1 != e.NewRange.r1)
                {
                    SetQtValue(_flex.DataIndex(_flex.Row));
                }
            }
            catch
            {
            }
        }
        #endregion

        #region -> _flex_ValidateEdit
        private void _flex_ValidateEdit(object sender, C1.Win.C1FlexGrid.ValidateEditEventArgs e)
        {
            try
            {

                if (CheckIsChangePossible())
                {

                    if (_flex.GetData(e.Row, e.Col).ToString() != _flex.EditData)
                    {
                        switch (_flex.Cols[e.Col].Name)
                        {

                            case "CD_UNIT_MM":
                                if (_flex.EditData.Trim() == "")
                                {
                                    double ldb_qt_po = 0;
                                    //단위 환산량					
                                    double ldb_amex = 0;

                                    try
                                    {
                                        ldb_qt_po = System.Double.Parse(_flex[_flex.Row, "QT_PO"].ToString());
                                        ldb_amex = System.Double.Parse(_flex[_flex.Row, "AM_EX"].ToString());
                                    }
                                    catch
                                    {
                                    }
                                    _flex[_flex.Row, "QT_PO_MM"] = ldb_qt_po;

                                    if (ldb_qt_po == 0)
                                    {
                                        _flex[_flex.Row, "UM_EX_PO"] = 0;
                                    }
                                    else
                                    {
                                        _flex[_flex.Row, "UM_EX_PO"] = ldb_amex / ldb_qt_po;
                                    }
                                    _flex[_flex.Row, "RT_PO"] = 1;
                                    //_flex.DataView[ _flex.DataIndex( _flex.Row)].EndEdit();
                                }
                                else
                                {

                                    double ldb_qt_po = 0;
                                    //단위 환산량
                                    double ldb_qtremm = SetQTREMMSubsystem(_flex.EditData.Trim());

                                    double ldb_umexp = 0;
                                    double ldb_qtpomm = 0;
                                    double ldb_amex = 0;

                                    try
                                    {
                                        ldb_qt_po = System.Double.Parse(_flex[_flex.Row, "QT_PO"].ToString());
                                        ldb_umexp = System.Double.Parse(_flex[_flex.Row, "UM_EX_PO"].ToString());
                                        ldb_qtpomm = System.Double.Parse(_flex[_flex.Row, "QT_PO_MM"].ToString());
                                        ldb_amex = System.Double.Parse(_flex[_flex.Row, "AM_EX"].ToString());

                                    }
                                    catch
                                    {
                                    }
                                    _flex[_flex.Row, "QT_PO_MM"] = ldb_qt_po / ldb_qtremm;// _flex[_flex.Row,"QT_PO"] ;	
                                    if (ldb_qt_po == 0)
                                    {
                                        _flex[_flex.Row, "UM_EX_PO"] = 0;
                                    }
                                    else
                                    {
                                        _flex[_flex.Row, "UM_EX_PO"] = ldb_amex / (ldb_qt_po / ldb_qtremm);
                                    }

                                    _flex[_flex.Row, "RT_PO"] = ldb_qtremm;
                                    //_flex.DataView[ _flex.DataIndex( _flex.Row)].EndEdit();	
                                }
                                break;

                            case "QT_PO_MM":
                                if (_flex[_flex.Row, "RT_PO"] == null)
                                {
                                    _flex[_flex.Row, "RT_PO"] = 1;
                                }
                                else
                                {
                                    if (System.Double.Parse(_flex[_flex.Row, "RT_PO"].ToString()) == 0)
                                    {
                                        _flex[_flex.Row, "RT_PO"] = 1;
                                    }
                                }

                                double ll_qtpo = System.Double.Parse(_flex.EditData.Trim()) * System.Double.Parse(_flex[_flex.Row, "RT_PO"].ToString());     // 관리수량 = 발주량 * 구매단위계수
                                _flex[_flex.Row, "QT_PO"] = ll_qtpo;
                                //	_flex[_flex.Row,"QT_PO_MM"] =  System.Double.Parse(_flex.EditData.Trim());								
                                SetGridMoneyChange(e);
                                break;

                            case "UM_EX_PO":
                                double ldb_umexpo = 0;

                                try
                                {
                                    ldb_umexpo = System.Double.Parse(_flex.EditData.Trim());
                                }
                                catch
                                {
                                }
                                if (cbo_TP_TAX.SelectedValue.ToString() == "001")
                                {
                                    SetGridMoneyChange2(ldb_umexpo);
                                    //_flex.DataView[ _flex.DataIndex( _flex.Row)].EndEdit();

                                }
                                else
                                {
                                    //	_flex[_flex.Row,"UM_EX_PO"] = ldb_umexpo;	
                                    //_flex.DataView[ _flex.DataIndex( _flex.Row)].EndEdit();
                                    SetGridMoneyChange(e);
                                }
                                break;

                            case "AM_EX":
                                double m_lb_amex = 0;
                                double m_am = 1.0;

                                double ldb_vatrate = 0.1;

                                try
                                {
                                    m_lb_amex = System.Double.Parse(_flex.EditData.Trim());
                                    m_am = System.Double.Parse(tb_NM_EXCH.DecimalValue.ToString());
                                    ldb_vatrate = System.Double.Parse(tb_TAX.ClipText.ToString()) / 100;
                                }
                                catch
                                {
                                }

                                //_flex[_flex.Row,"AM_EX"] = m_lb_amex;
                                _flex[_flex.Row, "AM"] = (long)System.Math.Floor(m_lb_amex * m_am);


                                double lb_amforvat = (long)System.Math.Floor(m_lb_amex * m_am);
                                _flex[_flex.Row, "VAT"] = (long)System.Math.Floor(lb_amforvat * ldb_vatrate);
                                //	dzdwGrid[dzdwGrid.CurrentCell.RowIndex,11].CellValue = 	(long)System.Math.Floor(lb_amforvat * ldb_vatrate);			
                                //_flex.DataView[ _flex.DataIndex( _flex.Row)].EndEdit();
                                SUMFunction();
                                break;
                            case "VAT":
                                //	_flex[_flex.Row,"VAT"] =System.Double.Parse(_flex.EditData.Trim());
                                SUMFunction();
                                break;

                        }
                    }
                }
            }
            catch (coDbException ex)
            {
                this.ShowErrorMessage(ex, this.PageName);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex, this.PageName);
            }

        }
        #endregion

        #region -> _flex_StartEdit
        private void _flex_StartEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            try
            {
                if (!CheckIsChangePossible())
                {
                    e.Cancel = true;	// 셀 입력상태로 못 들어가게
                }
            }
            finally
            {
            }
        }
        #endregion

        #region -> CheckIsChangePossible

        private bool CheckIsChangePossible()
        {
            try
            {
                if (_isChagePossible)
                {
                    if (_flex.DataView[_flex.DataIndex(_flex.Row)].Row.RowState.ToString() != "Added" &&
                        (_flex.Cols[_flex.Col].Name == "CD_ITEM"))
                    {
                        return false;
                    }
                    else if (_flex.DataView[_flex.DataIndex(_flex.Row)].Row.RowState.ToString() == "Added" &&
                        _flex[_flex.Row, "NO_PR"].ToString().Trim() != "" &&
                        (_flex.Cols[_flex.Col].Name == "CD_ITEM"))
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

                return true;
            }
            catch
            {
            }
            return true;
        }

        #endregion

        #region -> 그리드 내의 품목 호출함수

        /// <summary>
        /// 품목 관련 정보를 가지고옴
        /// </summary>
        /// <param name="cd_plant"></param>
        /// <param name="cd_item"></param>
        /// <returns></returns>
        private bool SetITEM(string cd_plant, string cd_item)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (cd_item != "")
                {
                    //System.Diagnostics.Debugger.Break();

                    InDataHeadValue4(cd_plant, cd_item);

                    try
                    {
                        //						object[] m_obj = new object[1];
                        //						m_obj[0] = ds_Ty1.Tables[4];
                        //						ds = (DataSet)(this.MainFrameInterface.InvokeRemoteMethod("PurOrderControl_NTX", "pur.CC_PU_PO_NTX","CC_PU_PO_NTX.rem", "SelectPitemUMINV", m_obj));

                        object[] m_obj = new object[8];
                        m_obj[0] = cd_item;
                        m_obj[1] = cd_plant;
                        m_obj[2] = this.MainFrameInterface.LoginInfo.CompanyCode;
                        m_obj[3] = cbo_FG_UM.SelectedValue.ToString();
                        m_obj[4] = cbo_NM_EXCH.SelectedValue.ToString();
                        m_obj[5] = tb_DT_PO.MaskEditBox.ClipText;
                        m_obj[6] = tb_NM_PARTNER.CodeValue.ToString();
                        m_obj[7] = tb_NM_PURGRP.CodeValue.ToString();

                        Duzon.Common.Util.SpInfo si = new Duzon.Common.Util.SpInfo();
                        ResultData result = (ResultData)this.FillDataSet("SP_PU_PO_ITEMINFO_SELECT", m_obj);
                        DataSet ds = (DataSet)result.DataValue;

                        if (ds != null && ds.Tables.Count > 3)
                        {

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                //////_flex.DataView[ _flex.DataIndex( _flex.Row)].EndEdit();;;
                                _flex[_flex.Row, "CD_ITEM"] = ds.Tables[0].Rows[0]["CD_ITEM"].ToString();
                                _flex[_flex.Row, "NM_ITEM"] = ds.Tables[0].Rows[0]["NM_ITEM"].ToString();
                                _flex[_flex.Row, "STND_ITEM"] = ds.Tables[0].Rows[0]["STND_ITEM"].ToString();
                                _flex[_flex.Row, "STND_MA_ITEM"] = ds.Tables[0].Rows[0]["STND_ITEM"].ToString();
                                _flex[_flex.Row, "UNIT_IM"] = ds.Tables[0].Rows[0]["UNIT_IM"].ToString();
                                _flex[_flex.Row, "CD_UNIT_MM"] = ds.Tables[0].Rows[0]["UNIT_PO"].ToString();
                                _flex[_flex.Row, "CD_SL"] = ds.Tables[0].Rows[0]["CD_SL"].ToString();
                                _flex[_flex.Row, "NM_SL"] = ds.Tables[0].Rows[0]["NM_SL"].ToString();
                                _flex[_flex.Row, "RT_PO"] = System.Double.Parse(ds.Tables[0].Rows[0]["UNIT_PO_FACT"].ToString());

                                //_flex.Editor.Text = ds.Tables[0].Rows[0]["CD_ITEM"].ToString();

                                //								if( ds.Tables[2].Rows.Count > 0)
                                //								{
                                //									try
                                //									{
                                //										_flex[_flex.Row,"RT_PO"] = System.Double.Parse(ds.Tables[0].Rows[0]["RATE_EXCHG"].ToString());			
                                //									}
                                //									catch
                                //									{
                                //										_flex[_flex.Row,"RT_PO"] = 1;
                                //									}
                                //								}
                                //								else
                                //								{
                                //									_flex[_flex.Row,"RT_PO"] = 1;
                                //								}
                                //								
                                //GetQTCurrent();
                                if (ds.Tables[1].Rows.Count > 0)
                                {
                                    try
                                    {
                                        _flex[_flex.Row, "UM_EX_PO"] = System.Double.Parse(ds.Tables[1].Rows[0]["UM_ITEM"].ToString());// System.Double.Parse(_flex[_flex.Row,"RT_PO"].ToString()) * System.Double.Parse( ds.Tables[1].Rows[0]["UM_ITEM"].ToString());
                                        _flex[_flex.Row, "UM_EX"] = System.Double.Parse(ds.Tables[1].Rows[0]["UM_ITEM"].ToString()) / System.Double.Parse(_flex[_flex.Row, "RT_PO"].ToString());//  System.Double.Parse( ds.Tables[1].Rows[0]["UM_ITEM"].ToString());
                                    }
                                    catch
                                    {
                                        _flex[_flex.Row, "UM_EX_PO"] = 0;
                                        _flex[_flex.Row, "UM_EX"] = 0;

                                    }
                                }
                                else
                                {
                                    _flex[_flex.Row, "UM_EX_PO"] = 0;
                                    _flex[_flex.Row, "UM_EX"] = 0;
                                }

                                if (ds.Tables[2].Rows.Count > 0)
                                {
                                    // 발주량
                                    _flex[_flex.Row, "QT_POC"] = ds.Tables[2].Rows[0]["QT_POC"];
                                    // 의뢰량
                                    _flex[_flex.Row, "QT_REQC"] = ds.Tables[2].Rows[0]["QT_REQC"];

                                }
                                else
                                {
                                    _flex[_flex.Row, "QT_POC"] = 0;
                                    _flex[_flex.Row, "QT_REQC"] = 0;
                                }
                                if (ds.Tables[3].Rows.Count > 0)
                                {

                                    // 현재고량
                                    _flex[_flex.Row, "QT_INVC"] = ds.Tables[3].Rows[0]["QT_INVC"];

                                    // 가용재고량
                                    _flex[_flex.Row, "QT_ATPC"] = ds.Tables[3].Rows[0]["QT_ATPC"];

                                }
                                else
                                {
                                    _flex[_flex.Row, "QT_INVC"] = 0;
                                    _flex[_flex.Row, "QT_ATPC"] = 0;
                                }

                                //_flex.DataView[ _flex.DataIndex( _flex.Row)].EndEdit();

                                SetQtValue(_flex.DataIndex(_flex.Row));

                                Cursor.Current = Cursors.Default;
                                //btn_RART_RECOM.Enabled = true;
                                return true;
                            }
                        }

                    }
                    catch (coDbException ex)
                    {
                        this.ShowErrorMessage(ex, this.PageName);
                    }
                    catch (Exception ex)
                    {
                        this.ShowErrorMessage(ex, this.PageName);
                    }
                }

                NotExistsCD_ITEM();

            }
            catch
            {
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
            return false;
        }



        /// <summary>
        /// 품목관련정보를 테이블에 저장
        /// </summary>
        /// <param name="cd_plant"></param>
        /// <param name="cd_item"></param>	
        private void InDataHeadValue4(string cd_plant, string cd_item)
        {

            DataRow newrow;

            ds_Ty1.Tables[4].Clear();

            newrow = ds_Ty1.Tables[4].NewRow();
            newrow["CD_PLANT"] = cd_plant;
            newrow["CD_COMPANY"] = this.MainFrameInterface.LoginInfo.CompanyCode;

            ds_Ty1.Tables[4].Rows.Add(newrow);

            ds_Ty1.Tables[4].BeginInit();
            DataRow ldr_row = ds_Ty1.Tables[4].Rows[0];
            ldr_row["CD_ITEM"] = cd_item;
            ldr_row["CD_PARTNER"] = tb_NM_PARTNER.CodeValue.ToString();
            ldr_row["SDT_UM"] = tb_DT_PO.MaskEditBox.ClipText;
            ldr_row["CD_PURGRP"] = tb_NM_PURGRP.CodeValue.ToString();
            ldr_row["CD_EXCH"] = cbo_NM_EXCH.SelectedValue.ToString();
            ldr_row["FG_UM"] = cbo_FG_UM.SelectedValue.ToString();

            ds_Ty1.Tables[4].EndInit();
        }



        /// <summary>
        /// 품목코드가 존재하지 않을 경우 처리 
        /// </summary>
        private void NotExistsCD_ITEM()
        {
            try
            {
                //////_flex.DataView[ _flex.DataIndex( _flex.Row)].EndEdit();;;
                //	_flex[_flex.Row,"CD_ITEM"]= "";
                _flex[_flex.Row, "NM_ITEM"] = "";
                _flex[_flex.Row, "STND_ITEM"] = "";
                _flex[_flex.Row, "UNIT_IM"] = "";
                _flex[_flex.Row, "CD_UNIT_MM"] = "";
                _flex[_flex.Row, "RT_PO"] = 1;
                _flex[_flex.Row, "UM_EX"] = 0;
                _flex[_flex.Row, "UM_EX_PO"] = 0;
                _flex[_flex.Row, "QT_POC"] = 0;
                _flex[_flex.Row, "QT_REQC"] = 0;
                _flex[_flex.Row, "QT_INVC"] = 0;
                _flex[_flex.Row, "QT_ATPC"] = 0;
                //_flex.DataView[ _flex.DataIndex( _flex.Row)].EndEdit();

                tb_QT_ATP.Text = "0";
                tb_QT_INV.Text = "0";
                tb_QT_PO.Text = "0";
                tb_QT_REQ.Text = "0";
            }
            catch
            {
            }
        }


        #endregion

        #region -> 그리드의 금액 변경

        /// <summary>
        /// 그리드 금액변경 부가세 별도 일 경우
        /// </summary>
        /// <param name="e"></param>
        private void SetGridMoneyChange(C1.Win.C1FlexGrid.ValidateEditEventArgs e)
        {

            try
            {
                //발주량
                double ldb_rateExchg = 1;
                // 단가
                double ldb_umexp = 0;
                double m_am = 1.0;
                double ldb_umex = 0;
                double ldb_vatrate = 0.1;//AM_EX

                try
                {
                    // 단위 환산량
                    ldb_rateExchg = System.Double.Parse(_flex[_flex.Row, "RT_PO"].ToString());
                    // 표현단가
                    ldb_umexp = System.Double.Parse(_flex[_flex.Row, "UM_EX_PO"].ToString());
                    // 환율
                    m_am = System.Double.Parse(tb_NM_EXCH.DecimalValue.ToString());

                    ldb_vatrate = System.Double.Parse(tb_TAX.ClipText.ToString()) / 100;
                }
                catch
                {
                }
                try
                {
                    // 실제 단가
                    ldb_umex = ldb_umexp / ldb_rateExchg; //System.Math.Floor(ldb_umexp / ldb_rateExchg);                 
                }
                catch
                {
                }


                //단가
                _flex[_flex.Row, "UM_EX"] = ldb_umex;


                //  원화 단가 계산
                if (tb_NM_EXCH.Text != "")
                {
                    try
                    {
                        _flex[_flex.Row, "UM"] = (ldb_umex * m_am);/// ldb_rateExchg ; // System.Math.Floor((ldb_umex * m_am)/ ldb_rateExchg) ;
                        _flex[_flex.Row, "UM_P"] = ldb_umexp * m_am;

                    }
                    catch
                    {
                        _flex[_flex.Row, "UM"] = 0;
                        _flex[_flex.Row, "UM_P"] = 0;
                    }
                }
                else
                {
                    _flex[_flex.Row, "UM"] = 0;
                    _flex[_flex.Row, "UM_P"] = 0;
                }


                double ll_qtpo = 0;

                // 금액
                //				
                // 수배량에 의한 값 금액 계산
                if (ldb_rateExchg > 1 || ldb_rateExchg < 1)
                {


                    ll_qtpo = System.Double.Parse(_flex[_flex.Row, "QT_PO_MM"].ToString()) * ldb_umexp;//System.Math.Floor(System.Double.Parse(_flex[_flex.Row,"QT_PO_MM"].ToString()) * ldb_umexp);				
                }
                else // 발주량에 의한 값 결정
                {
                    ll_qtpo = System.Double.Parse(_flex[_flex.Row, "QT_PO"].ToString()) * ldb_umex;//System.Math.Floor(System.Double.Parse(_flex[_flex.Row,"QT_PO"].ToString()) * ldb_umex);				
                }



                //금액
                _flex[_flex.Row, "AM_EX"] = (double)ll_qtpo;

                // 원화금액
                _flex[_flex.Row, "AM"] = (long)System.Math.Floor(ll_qtpo * m_am);

                //부가세 (원화금액기준)

                double lb_amforvat = System.Math.Floor(ll_qtpo * m_am);
                _flex[_flex.Row, "VAT"] = (long)System.Math.Floor(lb_amforvat * ldb_vatrate);
                //_flex.DataView[ _flex.DataIndex( _flex.Row)].EndEdit();

                SUMFunction();
            }
            catch
            {
            }
            finally
            {
                //_flex.DataView[ _flex.DataIndex( _flex.Row)].EndEdit();
            }

        }

        /// <summary>
        /// 부가세포함일 경우 ( 원화금액에서 금액계산까지 )
        /// </summary>
        /// <param name="pdb_UM"></param>
        private void SetGridMoneyChange2(double pdb_UM)
        {


            try
            {
                //발주량
                double ldb_rateExchg = 1;
                // 단가
                double m_am = 1.0;
                double ldb_vatrate = 0.1;//AM_EX

                try
                {
                    // 단위 환산량
                    ldb_rateExchg = System.Double.Parse(_flex[_flex.Row, "RT_PO"].ToString());
                    // 환율
                    m_am = System.Double.Parse(tb_NM_EXCH.DecimalValue.ToString());
                    ldb_vatrate = System.Double.Parse(tb_TAX.ClipText.ToString()) / 100;
                }
                catch
                {
                }


                double ll_qtpo = 0;

                // 금액
                //				
                // 수배량에 의한 값 금액 계산
                if (ldb_rateExchg > 1 || ldb_rateExchg < 1)
                {
                    ll_qtpo = System.Double.Parse(_flex[_flex.Row, "QT_PO_MM"].ToString());//System.Math.Floor(System.Double.Parse(_flex[_flex.Row,"QT_PO_MM"].ToString()) * ldb_umexp);				
                }
                else // 발주량에 의한 값 결정
                {
                    ll_qtpo = System.Double.Parse(_flex[_flex.Row, "QT_PO"].ToString());//System.Math.Floor(System.Double.Parse(_flex[_flex.Row,"QT_PO"].ToString()) * ldb_umex);				
                }


                double ldb_VatKr = System.Math.Floor(System.Math.Round((ll_qtpo * m_am * pdb_UM) / (1 + ldb_vatrate), 9) * 0.1);

                double ldb_AmKr = System.Math.Floor(ll_qtpo * m_am * pdb_UM - ldb_VatKr);

                // 원화금액
                _flex[_flex.Row, "AM"] = System.Math.Floor(ll_qtpo * m_am * pdb_UM - ldb_VatKr);

                //부가세 (원화금액기준)								
                _flex[_flex.Row, "VAT"] = (double)ldb_VatKr;

                //  원화 단가 계산
                if (tb_NM_EXCH.Text != "")
                {
                    try
                    {
                        _flex[_flex.Row, "UM"] = (ldb_AmKr / (ll_qtpo * ldb_rateExchg));/// ldb_rateExchg ; // System.Math.Floor((ldb_umex * m_am)/ ldb_rateExchg) ;
                        _flex[_flex.Row, "UM_P"] = ldb_AmKr / (ll_qtpo);
                    }
                    catch
                    {
                        _flex[_flex.Row, "UM"] = 0;
                        _flex[_flex.Row, "UM_P"] = 0;
                    }
                }
                else
                {
                    _flex[_flex.Row, "UM"] = 0;
                    _flex[_flex.Row, "UM_P"] = 0;
                }



                double ldb_amEx = ldb_AmKr / m_am;
                //금액
                _flex[_flex.Row, "AM_EX"] = ldb_amEx;


                //단가
                _flex[_flex.Row, "UM_EX"] = ldb_amEx / (ll_qtpo * ldb_rateExchg);

                _flex[_flex.Row, "UM_EX_PO"] = ldb_amEx / ll_qtpo;

                _flex.Editor.Text = System.Convert.ToString(ldb_amEx / ll_qtpo);

                SUMFunction();
            }
            catch
            {
            }
        }


        #endregion

        #region -> 그리드 금액 계산 부분
        /// <summary>
        /// 그리드 금액 변경( 전체 )
        /// </summary>
        private void SetExchageMoney()
        {
            try
            {

                if (_flex.DataView == null)
                {
                    return;
                }
                if (_flex.DataView.Count <= 0)
                {
                    return;
                }

                double ldb_exch = System.Double.Parse(tb_NM_EXCH.ClipText);


                for (int i = 0; i < _flex.DataView.Count; i++)
                {
                    try
                    {
                        //발주량
                        double ldb_rateExchg = 1;
                        // 단가
                        double ldb_umexp = 0;
                        double m_am = 1.0;
                        double ldb_vatrate = 0.1;

                        try
                        {
                            // 단위 환산량
                            ldb_rateExchg = System.Double.Parse(_flex.DataView[i]["RT_PO"].ToString());
                            // 표현단가
                            ldb_umexp = System.Double.Parse(_flex.DataView[i]["UM_EX_PO"].ToString());
                            // 환율
                            m_am = System.Double.Parse(tb_NM_EXCH.ClipText.ToString());
                            ldb_vatrate = System.Double.Parse(tb_TAX.ClipText.ToString()) / 100;
                        }
                        catch
                        {
                        }

                        //  원화 단가 계산
                        if (tb_NM_EXCH.ClipText != "")
                        {
                            try
                            {
                                _flex.DataView[i]["UM_P"] = ldb_umexp * m_am;
                                _flex.DataView[i]["UM"] = (ldb_umexp / ldb_rateExchg) * m_am;
                                _flex.DataView[i]["UM_EX"] = (ldb_umexp / ldb_rateExchg);
                            }
                            catch
                            {
                                _flex.DataView[i]["UM_P"] = 0;
                                _flex.DataView[i]["UM"] = 0;
                                _flex.DataView[i]["UM_EX"] = 0;
                            }
                        }
                        else
                        {
                            _flex.DataView[i]["UM_P"] = 0;
                            _flex.DataView[i]["UM"] = 0;
                            _flex.DataView[i]["UM_EX"] = 0;
                        }


                        double ll_qtpo = 0;

                        // 금액
                        //				
                        // 수배량에 의한 값 금액 계산
                        if (ldb_rateExchg > 1 || ldb_rateExchg < 1)
                        {
                            ll_qtpo = System.Math.Floor(System.Double.Parse(_flex.DataView[i]["QT_PO_MM"].ToString()) * ldb_umexp);
                        }
                        else // 발주량에 의한 값 결정
                        {
                            ll_qtpo = System.Math.Floor(System.Double.Parse(_flex.DataView[i]["QT_PO"].ToString()) * System.Double.Parse(_flex.DataView[i]["UM_EX"].ToString()));
                        }


                        _flex.DataView[i]["AM_EX"] = ll_qtpo;

                        // 원화금액
                        _flex.DataView[i]["AM"] = (long)System.Math.Floor(ll_qtpo * m_am);

                        //부가세 (원화금액기준)

                        double lb_amforvat = System.Math.Floor(ll_qtpo * m_am);
                        _flex.DataView[i]["VAT"] = (long)System.Math.Floor(lb_amforvat * ldb_vatrate);

                    }
                    catch
                    {
                    }

                }
            }
            catch
            {
            }
            finally
            {

            }
            SUMFunction();
        }



        #endregion

        #endregion

        #region ♣ 도움창 이벤트 / 메소드

        #region -> 도움창 분기

        private void OnShowHelp(object sender, System.EventArgs e)
        {
            if (Duzon.Common.Forms.BasicInfo.ActiveDialog == true)
                return;

            string controlName = ((Control)sender).Name.ToString();

            try
            {
                if (CheckIsChangePossible())
                {
                    if (controlName == "_flex")
                    {
                        HelpReturn helpreturn = null;
                        HelpParam param = null;
                        lb_DT_PO.Focus();
                        switch (_flex.HelpColName)
                        {
                            case "CD_ITEM":
                                ShowDlgITEM();		// 품목 도움창
                                break;
                            case "DT_LIMIT":
                                ShowDlgCalendarGrid();
                                break;
                            case "CD_SL":
                                param = new Duzon.Common.Forms.Help.HelpParam(
                                    Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB, this.MainFrameInterface);
                                param.P01_CD_COMPANY = this.LoginInfo.CompanyCode;
                                param.P09_CD_PLANT = _flex[_flex.Row, "CD_PLANT"].ToString();
                                helpreturn = (HelpReturn)this.ShowHelp(param);
                                if (helpreturn.DialogResult == System.Windows.Forms.DialogResult.OK)
                                {
                                    _flex[_flex.Row, "CD_SL"] = helpreturn.CodeValue;
                                    _flex[_flex.Row, "NM_SL"] = helpreturn.CodeName;

                                }
                                break;
                            case "CD_PJT":
                                param = new Duzon.Common.Forms.Help.HelpParam(
                                    Duzon.Common.Forms.Help.HelpID.P_SA_PROJECT_SUB, this.MainFrameInterface);
                                helpreturn = (HelpReturn)this.ShowHelp(param);
                                if (helpreturn.DialogResult == System.Windows.Forms.DialogResult.OK)
                                {
                                    _flex[_flex.Row, "CD_PJT"] = helpreturn.CodeValue;
                                    _flex[_flex.Row, "NM_PJT"] = helpreturn.CodeName;

                                }
                                break;
                        }
                        _flex.Select(_flex.Row, _flex.Col + 1);
                        _flex.Focus();

                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex, this.PageName);
            }
        }

        // 	

        private void _flex_CodeHelp(object sender, Dass.FlexGrid.CodeHelpEventArgs e)
        {
            try
            {
                if (CheckIsChangePossible())
                {

                    HelpReturn helpReturn = null;
                    HelpParam param = null;

                    switch (_flex.Cols[e.Col].Name)
                    {
                        case "CD_ITEM":
                            if (e.Source == CodeHelpEnum.CodeSearch && e.EditValue == "")
                            {
                                NotExistsCD_ITEM();

                            }
                            else
                            {
                                param = new Duzon.Common.Forms.Help.HelpParam(
                                    Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB, this.MainFrameInterface);
                                param.P01_CD_COMPANY = this.LoginInfo.CompanyCode;
                                param.P09_CD_PLANT = _flex[_flex.Row, "CD_PLANT"].ToString();
                                param.ResultMode = ResultMode.FastMode;
                                if (e.Source == CodeHelpEnum.CodeSearch)
                                    param.P92_DETAIL_SEARCH_CODE = e.EditValue;

                                if (e.Source == CodeHelpEnum.CodeSearch) helpReturn = (HelpReturn)this.CodeSearch(param);
                                else helpReturn = (HelpReturn)this.ShowHelp(param);

                                if (helpReturn.DialogResult == DialogResult.OK)
                                {
                                    SetITEM(_flex[_flex.Row, "CD_PLANT"].ToString(), helpReturn.CodeValue);
                                }

                            }
                            break;

                        case "CD_SL":
                            if (e.Source == CodeHelpEnum.CodeSearch && e.EditValue == "")
                            {
                                _flex[_flex.Row, "CD_SL"] = "";
                                _flex[_flex.Row, "NM_SL"] = "";
                            }
                            else
                            {
                                param = new Duzon.Common.Forms.Help.HelpParam(
                                    Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB, this.MainFrameInterface);
                                param.P01_CD_COMPANY = this.LoginInfo.CompanyCode;
                                param.P09_CD_PLANT = _flex[_flex.Row, "CD_PLANT"].ToString();
                                param.ResultMode = ResultMode.FastMode;
                                if (e.Source == CodeHelpEnum.CodeSearch)
                                    param.P92_DETAIL_SEARCH_CODE = e.EditValue;

                                if (e.Source == CodeHelpEnum.CodeSearch) helpReturn = (HelpReturn)this.CodeSearch(param);
                                else helpReturn = (HelpReturn)this.ShowHelp(param);

                                if (helpReturn.DialogResult == System.Windows.Forms.DialogResult.OK)
                                {
                                    _flex[_flex.Row, "CD_SL"] = helpReturn.CodeValue;
                                    _flex[_flex.Row, "NM_SL"] = helpReturn.CodeName;

                                }
                            }
                            break;
                        case "CD_PJT"://@P09_CD_PLANT
                            if (e.Source == CodeHelpEnum.CodeSearch && e.EditValue == "")
                            {
                                _flex[_flex.Row, "CD_PJT"] = "";
                                _flex[_flex.Row, "NM_PJT"] = "";
                            }
                            else
                            {
                                param = new Duzon.Common.Forms.Help.HelpParam(
                                    Duzon.Common.Forms.Help.HelpID.P_SA_PROJECT_SUB, this.MainFrameInterface);
                                if (e.Source == CodeHelpEnum.CodeSearch)
                                    param.P92_DETAIL_SEARCH_CODE = e.EditValue;

                                if (e.Source == CodeHelpEnum.CodeSearch) helpReturn = (HelpReturn)this.CodeSearch(param);
                                else helpReturn = (HelpReturn)this.ShowHelp(param);

                                if (helpReturn.DialogResult == System.Windows.Forms.DialogResult.OK)
                                {
                                    _flex[_flex.Row, "CD_PJT"] = helpReturn.CodeValue;
                                    _flex[_flex.Row, "NM_PJT"] = helpReturn.CodeName;
                                }
                            }
                            break;
                        case "DT_LIMIT":
                            if (e.Source == CodeHelpEnum.CodeSearch)
                            {
                                if (e.EditValue != "")
                                {
                                    if (!_flex.IsDate(_flex.Cols[e.Col].Name))
                                    {
                                        // 날짜 입력형식이 잘못되었습니다.
                                        this.ShowMessage("WK1_003");
                                    }
                                }
                            }
                            else
                            {
                                ShowDlgCalendarGrid();		// 품목 도움창

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


        #endregion

        #region -> 그리드에서 호출 도움창들

        /// <summary>
        /// 품목 도움창
        /// </summary>
        private void ShowDlgITEM()
        {
            try
            {
                //System.Diagnostics.Debugger.Break();
                ////DataView ddv = (DataView)dzdwGrid.DataSource;
                if (_flex[_flex.Row, "CD_PLANT"].ToString() == "")
                {
                    this.ShowMessage("MA_M000047");
                    //		Duzon.Common.Controls.MessageBoxEx.Show(this.GetMessageDictionaryItem("MA_M000047"),this.PageName);
                    _flex.Select(_flex.Row, "CD_PLANT");
                    return;
                }


                Cursor.Current = Cursors.WaitCursor;
                object obj = this.LoadHelpWindow("P_MA_PITEM_SUB", new object[] { this.MainFrameInterface, _flex[_flex.Row, "CD_PLANT"].ToString() });
                Cursor.Current = Cursors.Default;
                if (((Duzon.Common.Forms.BaseSearchHelp)obj).ShowDialog(this) == DialogResult.OK)
                {
                    object[] row = (object[])((Duzon.Common.Forms.IHelpWindow)obj).ReturnValues;
                    lb_DT_PO.Focus();
                    SetITEM(_flex[_flex.Row, "CD_PLANT"].ToString(), row[0].ToString());
                    _flex.Focus();

                }
            }
            catch
            {
            }
        }


        /// <summary>
        /// 그리드네의 날짜 관련 도움창
        /// </summary>
        private void ShowDlgCalendarGrid()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                object obj = this.MainFrameInterface.LoadHelpWindow("P_PR_CALENDAR", new object[] { this.MainFrameInterface });
                Cursor.Current = Cursors.Default;
                if (((Duzon.Common.Forms.CommonDialog)obj).ShowDialog() == DialogResult.OK)
                {
                    object[] row = (object[])((Duzon.Common.Forms.IHelpWindow)obj).ReturnValues;
                    lb_DT_PO.Focus();
                    _flex[_flex.Row, "DT_LIMIT"] = row[0].ToString();
                    _flex.Focus();
                    _flex.Select(_flex.Row, "QT_PO_MM");
                }
            }
            catch
            {
            }
        }	//ShowDlgProjectGrid	

        /// <summary>
        /// 보관장소 도움창 
        /// </summary>
        /// <param name="ps_search"></param>
        private void ShowDlgSL(string ps_search)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                object obj = this.LoadHelpWindow("P_MA_SL_SUB", new object[3] { this.MainFrameInterface, ps_search, _flex[_flex.Row, "CD_PLANT"].ToString() });
                Cursor.Current = Cursors.Default;
                if (((Duzon.Common.Forms.BaseSearchHelp)obj).ShowDialog() == DialogResult.OK)
                {
                    object[] row = (object[])((Duzon.Common.Forms.IHelpWindow)obj).ReturnValues;

                    lb_DT_PO.Focus();
                    _flex[_flex.Row, "CD_SL"] = row[0].ToString();
                    _flex[_flex.Row, "NM_SL"] = row[1].ToString();
                    _flex.Focus();
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 그리드내의 프로젝트 도움창
        /// </summary>
        /// <param name="ps_search"></param>
        private void ShowDlgProjectGrid(string ps_search)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                object obj = this.MainFrameInterface.LoadHelpWindow("P_SA_PROJECT_SUB1", new object[] { this.MainFrameInterface, "", false });
                Cursor.Current = Cursors.Default;
                if (((Duzon.Common.Forms.CommonDialog)obj).ShowDialog() == DialogResult.OK)
                {
                    _flex[_flex.Row, "CD_PJT"] = ((object[])((IHelpWindow)obj).ReturnValues)[0].ToString();
                    _flex[_flex.Row, "NM_PJT"] = ((object[])((IHelpWindow)obj).ReturnValues)[1].ToString();
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 계약관련 도움창
        /// </summary>
        /// <param name="ps_search"></param>
        private void ShowDlgCONTRACT(string ps_search)
        {

            //			pur.P_PU_CT_SUB m_dlg = new pur.P_PU_CT_SUB(this.MainFrameInterface);
            //			
            //			if(m_dlg.ShowDialog(this) == DialogResult.OK)
            //			{ 		
            //				dzdwGrid.CurrentCell.EndEdit();
            //				////DataView ddv = (DataView)dzdwGrid.DataSource;
            //				dzdwGrid[dzdwGrid.CurrentCell.RowIndex,17].CellValue = m_dlg.M_Result;
            //				_flex[_flex.Row,"NO_CONTRACT"] = m_dlg.M_Result;	
            //				//	dzdwGrid.Refresh();
            //			}	

        }

        #endregion

        #endregion

        #region ♣ 기타 이벤트

        #region -> btn_insert_Click
        /// <summary>
        /// 내부 추가 버턴 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_insert_Click(object sender, System.EventArgs e)
        {
            try
            {

                if (!btn_insert.Enabled)
                    return;

                // 헤드 부분의 필수 항목 입력된지 검사 
                if (!FieldCheck_Head())
                {
                    return;
                }
                if (_flex.DataTable == null)
                {
                    return;
                }

                //m_lblTitle.Focus();

                _flex.Rows.Add();
                _flex.Row = _flex.Rows.Count - 1;


                _flex[_flex.Row, "CD_COMPANY"] = this.LoginInfo.CompanyCode;
                _flex[_flex.Row, "DT_LIMIT"] = this.MainFrameInterface.GetStringToday;

                _flex[_flex.Row, "NO_PO"] = tb_NO_PO.Text.Trim();

                //	_flex[_flex.Row,"CD_ITEM"] = "";
                _flex[_flex.Row, "FG_POCON"] = "001";
                _flex[_flex.Row, "NO_LINE"] = GetMaxPoLine();

                _flex[_flex.Row, "QT_REQ"] = 0;
                _flex[_flex.Row, "QT_RCV"] = 0;
                _flex[_flex.Row, "QT_PO_MM"] = 0;
                _flex[_flex.Row, "QT_PO"] = 0;

                _flex[_flex.Row, "AM"] = 0;
                _flex[_flex.Row, "UM"] = 0;

                _flex[_flex.Row, "AM_EX"] = 0;
                _flex[_flex.Row, "UM_EX"] = 0;
                _flex[_flex.Row, "VAT"] = 0;

                _flex[_flex.Row, "UM_P"] = 0;
                _flex[_flex.Row, "UM_EX_PO"] = 0;


                _flex[_flex.Row, "RT_PO"] = 1;
                _flex[_flex.Row, "UM"] = 0;
                _flex[_flex.Row, "CD_SL"] = "";


                _flex[_flex.Row, "FG_POST"] = "O";
                _flex[_flex.Row, "FG_POCON"] = "001";
                _flex[_flex.Row, "NM_SYSDEF"] = _ComfirmState;



                //구매요청
                _flex[_flex.Row, "NO_PR"] = "";
                _flex[_flex.Row, "NO_PRLINE"] = 0;

                //품의요청
                _flex[_flex.Row, "NO_APP"] = "";
                _flex[_flex.Row, "NO_APPLINE"] = 0;



                // 계약
                //_flex[_flex.Row,"NO_CONTRACT"] = "";
                _flex[_flex.Row, "NO_CTLINE"] = 0;


                // 프로젝트
                _flex[_flex.Row, "CD_PJT"] = tb_CD_PJT.CodeValue.ToString();
                _flex[_flex.Row, "NM_PJT"] = tb_CD_PJT.CodeName;
                _flex[_flex.Row, "PNM_PJT"] = tb_CD_PJT.CodeName;


                // 의뢰
                _flex[_flex.Row, "NO_RCV"] = "";
                _flex[_flex.Row, "NO_RCVLINE"] = 0;

                _flex[_flex.Row, "CD_PLANT"] = cbo_CD_PLANT.SelectedValue.ToString();

                _flex.AddFinished();
                _flex.Col = _flex.Cols.Fixed;


                _tp_process = "2";

                btn_RE_PR.Enabled = false;
                btn_RE_APP.Enabled = false;


                SetControlEnabled(false, 1);
                //BtnEnableFalse();
            }
            catch (coDbException ex)
            {
                this.ShowErrorMessage(ex, this.PageName);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex, this.PageName);
            }
            finally
            {
                _flex.Focus();
                this.ShowStatusBarMessage(4);
            }

        }


        #endregion

        #region -> btn_delete_Click
        /// <summary>
        /// 내부 삭제 버턴 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_delete_Click(object sender, System.EventArgs e)
        {
            // 발주내역등록 하면 일 경우 	

            try
            {

                //DataView ldv_src = (DataView)dzdwGrid.DataSource;

                // 확정여부가 미정일 경우
                if (_flex[_flex.Row, "FG_POCON"].ToString() == "001")
                {
                    // 품의적용 받아 온것을 삭제함....
                    if (_flex[_flex.Row, "NO_APP"].ToString().Trim() != "")
                    {
                        string ps_filter = "NO_APP ='" + _flex[_flex.Row, "NO_APP"].ToString() +
                            "' AND NO_APPLINE ='" + _flex[_flex.Row, "NO_APPLINE"].ToString() + "'";

                        DataRow[] rows = ds_Ty1.Tables[5].Select(ps_filter);

                        if (rows != null)
                        {
                            for (int i = 0; i < rows.Length; i++)
                            {
                                rows[i].Delete();
                            }
                            ds_Ty1.Tables[5].AcceptChanges();
                        }
                    }


                    // 요청적용 받아 온것을 삭제함....
                    if (_flex[_flex.Row, "NO_PR"].ToString().Trim() != "" &&
                        _flex[_flex.Row, "NO_APP"].ToString().Trim() == "")
                    {
                        string ps_filter = "NO_PR ='" + _flex[_flex.Row, "NO_PR"].ToString() +
                            "' AND NO_PRLINE ='" + _flex[_flex.Row, "NO_PRLINE"].ToString() + "'";

                        DataRow[] rows = ds_Ty1.Tables[1].Select(ps_filter);

                        if (rows != null)
                        {
                            for (int i = 0; i < rows.Length; i++)
                            {
                                rows[i].Delete();
                            }
                            ds_Ty1.Tables[1].AcceptChanges();
                        }
                    }


                    _flex.Rows.Remove(_flex.Row);



                    //	gbool_cellchanged = true;
                    if (_flex.DataView.Count <= 0)
                    {
                        btn_insert.Enabled = true;
                        btn_RE_PR.Enabled = true;
                        btn_RE_APP.Enabled = true;
                        btn_RE_PJT.Enabled = true;
                        btn_ITEM_EXP.Enabled = true;
                        SetControlEnabled(true, 1);
                    }


                }
                else
                {
                    this.ShowMessage("MA_M000094");
                }
            }
            catch
            {
            }
        }


        #endregion

        #region -> 요청적용 버턴 이벤트
        /// <summary>
        /// 요청적용 버턴이 클릭시.. 요청된 품목에 대한 정보를 가져옴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_RE_PR_Click(object sender, System.EventArgs e)
        {
            if (!FieldCheck_Head())
            {
                return;
            }

            Cursor.Current = Cursors.WaitCursor;
            pur.P_PU_POPR_SUB m_dlg = new pur.P_PU_POPR_SUB(this.MainFrameInterface, ds_Ty1.Tables[1],
                cbo_CD_PLANT.SelectedValue.ToString(), cbo_CD_PLANT.SelectedText.ToString(), tb_NM_PURGRP.CodeValue.ToString(), tb_NM_PURGRP.CodeName);
            Cursor.Current = Cursors.Default;

            if (m_dlg.ShowDialog(this) == DialogResult.OK)
            {
                //DataView ldv_src = (DataView)dzdwGrid.DataSource;
                //DataTable ldt_src = (DataTable)ldv_src.Table;
                DataTable ldt_dlg = m_dlg.gdt_Return;


                try
                {
                    if (ldt_dlg != null)
                    {
                        if (ldt_dlg.Rows.Count > 0)
                        {
                            //	DataRow lr_temp;

                            _tp_process = "1";
                            btn_insert.Enabled = false;
                            btn_RE_PJT.Enabled = false;
                            btn_ITEM_EXP.Enabled = false;
                            //btn_RART_RECOM.Enabled = true;
                            btn_RE_APP.Enabled = false;

                            string ls_cditemlist = "";
                            string ls_itemplant = "";
                            for (int i = 0; i < ldt_dlg.Rows.Count; i++)
                            {
                                if (ldt_dlg.Rows[i].RowState.ToString() == "Deleted")
                                {
                                    continue;
                                }
                                ls_cditemlist += ldt_dlg.Rows[i]["CD_ITEM"].ToString();

                                if (i + 1 < ldt_dlg.Rows.Count)
                                {
                                    ls_cditemlist += "','";
                                }
                                ls_itemplant += "(L.CD_ITEM ='" + ldt_dlg.Rows[i]["CD_ITEM"].ToString().Trim() + "' AND L.CD_PLANT = '" +
                                    ldt_dlg.Rows[i]["CD_PLANT"].ToString().Trim() + "') ";
                                if (i + 1 < ldt_dlg.Rows.Count)
                                {
                                    ls_itemplant += " OR ";
                                }
                            }

                            //	DataSet lds_ItemInfo = SelectItemInfo(ls_cditemlist,ls_itemplant);


                            _flex.Redraw = false;
                            for (int i = 0; i < ldt_dlg.Rows.Count; i++)
                            {
                                if (ldt_dlg.Rows[i].RowState.ToString() == "Deleted")
                                {
                                    continue;
                                }

                                _flex.Rows.Add();
                                _flex.Row = _flex.Rows.Count - 1;

                                _flex[_flex.Row, "NO_PO"] = tb_NO_PO.Text.Trim();
                                _flex[_flex.Row, "FG_POCON"] = "001";
                                _flex[_flex.Row, "NO_LINE"] = GetMaxPoLine();
                                _flex[_flex.Row, "QT_REQ"] = 0;
                                _flex[_flex.Row, "QT_RCV"] = 0;
                                _flex[_flex.Row, "QT_PO_MM"] = 0;

                                _flex[_flex.Row, "AM"] = 0;
                                _flex[_flex.Row, "UM"] = 0;

                                _flex[_flex.Row, "AM_EX"] = 0;
                                _flex[_flex.Row, "UM_EX"] = 0;
                                _flex[_flex.Row, "VAT"] = 0;

                                _flex[_flex.Row, "UM_P"] = 0;
                                _flex[_flex.Row, "UM_EX_PO"] = 0;

                                _flex[_flex.Row, "UM_P"] = 0;
                                _flex[_flex.Row, "UM_EX_PO"] = 0;
                                _flex[_flex.Row, "UM_EX"] = 0;
                                _flex[_flex.Row, "UM"] = 0;


                                _flex[_flex.Row, "CD_SL"] = "";

                                _flex[_flex.Row, "NO_CONTRACT"] = ldt_dlg.Rows[i]["NO_CONTRACT"].ToString();
                                _flex[_flex.Row, "NO_CTLINE"] = ldt_dlg.Rows[i]["NO_CTLINE"];

                                // 의뢰
                                _flex[_flex.Row, "NO_RCV"] = "";
                                _flex[_flex.Row, "NO_RCVLINE"] = 0;


                                // 요청적용에서 가지고 오는 것이므로..요청번호와 항번입력
                                _flex[_flex.Row, "NO_PR"] = ldt_dlg.Rows[i]["NO_PR"].ToString();
                                _flex[_flex.Row, "NO_PRLINE"] = ldt_dlg.Rows[i]["NO_PRLINE"];

                                _flex[_flex.Row, "NO_APP"] = "";
                                _flex[_flex.Row, "NO_APPLINE"] = 0;


                                _flex[_flex.Row, "FG_POST"] = "O";
                                _flex[_flex.Row, "FG_POCON"] = "001";
                                _flex[_flex.Row, "NM_SYSDEF"] = _ComfirmState;

                                _flex[_flex.Row, "NM_PLANT"] = ldt_dlg.Rows[i]["NM_PLANT"].ToString();
                                _flex[_flex.Row, "CD_PLANT"] = ldt_dlg.Rows[i]["CD_PLANT"].ToString();
                                _flex[_flex.Row, "CD_ITEM"] = ldt_dlg.Rows[i]["CD_ITEM"].ToString();
                                _flex[_flex.Row, "NM_ITEM"] = ldt_dlg.Rows[i]["NM_ITEM"].ToString();
                                _flex[_flex.Row, "CD_UNIT_MM"] = ldt_dlg.Rows[i]["UNIT_PO"].ToString();
                                _flex[_flex.Row, "UNIT_PO"] = ldt_dlg.Rows[i]["UNIT_PO"].ToString();
                                _flex[_flex.Row, "STND_ITEM"] = ldt_dlg.Rows[i]["STND_ITEM"].ToString();
                                _flex[_flex.Row, "STND_MA_ITEM"] = ldt_dlg.Rows[i]["STND_ITEM"].ToString();
                                _flex[_flex.Row, "UNIT_IM"] = ldt_dlg.Rows[i]["UNIT_IM"].ToString();
                                _flex[_flex.Row, "QT_PO"] = ldt_dlg.Rows[i]["QT_PR"];
                                _flex[_flex.Row, "DT_LIMIT"] = ldt_dlg.Rows[i]["DT_LIMIT"].ToString();

                                _flex[_flex.Row, "CD_SL"] = ldt_dlg.Rows[i]["CD_SL"].ToString();
                                _flex[_flex.Row, "NM_SL"] = ldt_dlg.Rows[i]["NM_SL"].ToString();
                                _flex[_flex.Row, "GR_SL"] = ldt_dlg.Rows[i]["NM_SL"].ToString();
                                _flex[_flex.Row, "RT_PO"] = ldt_dlg.Rows[i]["RT_PO"];
                                _flex[_flex.Row, "QT_PO_MM"] = 0;


                                if (System.Double.Parse(_flex[_flex.Row, "RT_PO"].ToString()) > 0)
                                {
                                    if (ldt_dlg.Rows[i]["UNIT_PO"] == null || ldt_dlg.Rows[i]["UNIT_PO"].ToString().Trim() == "")
                                    {
                                        _flex[_flex.Row, "QT_PO_MM"] = 0;
                                    }
                                    else if (ldt_dlg.Rows[i]["UNIT_PO"].ToString().Trim() != "")
                                    {
                                        _flex[_flex.Row, "QT_PO_MM"] = (System.Double.Parse(ldt_dlg.Rows[i]["QT_PR"].ToString()) / System.Double.Parse(_flex[_flex.Row, "RT_PO"].ToString()));
                                    }
                                }
                                else
                                {
                                    _flex[_flex.Row, "QT_PO_MM"] = 0;
                                }




                                object[] m_obj = new object[8];
                                m_obj[0] = _flex[_flex.Row, "CD_ITEM"].ToString();
                                m_obj[1] = _flex[_flex.Row, "CD_PLANT"].ToString();
                                m_obj[2] = this.MainFrameInterface.LoginInfo.CompanyCode;
                                m_obj[3] = cbo_FG_UM.SelectedValue.ToString();
                                m_obj[4] = cbo_NM_EXCH.SelectedValue.ToString();
                                m_obj[5] = tb_DT_PO.MaskEditBox.ClipText;
                                m_obj[6] = tb_NM_PARTNER.CodeValue.ToString();
                                m_obj[7] = tb_NM_PURGRP.CodeValue.ToString();


                                Duzon.Common.Util.SpInfo si = new Duzon.Common.Util.SpInfo();
                                ResultData result = (ResultData)this.FillDataSet("SP_PU_PO_ITEMINFO_SELECT", m_obj);
                                DataSet lds_ItemInfo = (DataSet)result.DataValue;


                                if (lds_ItemInfo != null && lds_ItemInfo.Tables.Count > 3)
                                {

                                    _flex[_flex.Row, "UM_EX_PO"] = System.Double.Parse(lds_ItemInfo.Tables[1].Rows[0]["UM_ITEM"].ToString());
                                    _flex[_flex.Row, "UM_EX"] = System.Double.Parse(lds_ItemInfo.Tables[1].Rows[0]["UM_ITEM"].ToString()) / System.Double.Parse(ldt_dlg.Rows[i]["RT_PO"].ToString());
                                    _flex[_flex.Row, "UM_P"] = System.Double.Parse(_flex[_flex.Row, "UM_EX_PO"].ToString()) * System.Double.Parse(tb_NM_EXCH.Text.Trim());
                                    _flex[_flex.Row, "UM"] = System.Double.Parse(_flex[_flex.Row, "UM_EX"].ToString()) * System.Double.Parse(tb_NM_EXCH.Text.Trim());

                                    _flex[_flex.Row, "AM_EX"] = System.Double.Parse(_flex[_flex.Row, "UM_EX_PO"].ToString()) * System.Double.Parse(_flex[_flex.Row, "QT_PO_MM"].ToString());
                                    _flex[_flex.Row, "AM"] = System.Math.Floor(System.Double.Parse(_flex[_flex.Row, "AM_EX"].ToString()) * System.Double.Parse(tb_NM_EXCH.Text.Trim()));
                                    _flex[_flex.Row, "VAT"] = System.Math.Floor(System.Math.Floor(System.Double.Parse(_flex[_flex.Row, "AM"].ToString()) * (System.Double.Parse(tb_TAX.ClipText.ToString()) / 100)));


                                    if (lds_ItemInfo.Tables[2] != null && lds_ItemInfo.Tables[2].Rows.Count > 0)
                                    {
                                        // 발주량
                                        _flex[_flex.Row, "QT_POC"] = lds_ItemInfo.Tables[2].Rows[0]["QT_POC"];
                                        _flex[_flex.Row, "QT_REQC"] = lds_ItemInfo.Tables[2].Rows[0]["QT_REQC"];
                                    }
                                    else
                                    {
                                        _flex[_flex.Row, "QT_POC"] = 0;
                                        _flex[_flex.Row, "QT_REQC"] = 0;
                                    }

                                    if (lds_ItemInfo.Tables[3] != null && lds_ItemInfo.Tables[3].Rows.Count > 0)
                                    {
                                        _flex[_flex.Row, "QT_INVC"] = lds_ItemInfo.Tables[3].Rows[0]["QT_INVC"];
                                        _flex[_flex.Row, "QT_ATPC"] = lds_ItemInfo.Tables[3].Rows[0]["QT_ATPC"];
                                    }
                                    else
                                    {
                                        _flex[_flex.Row, "QT_INVC"] = 0;
                                        _flex[_flex.Row, "QT_ATPC"] = 0;
                                    }
                                }
                                else
                                {
                                    _flex[_flex.Row, "QT_POC"] = 0;
                                    _flex[_flex.Row, "QT_REQC"] = 0;
                                    _flex[_flex.Row, "QT_INVC"] = 0;
                                    _flex[_flex.Row, "QT_ATPC"] = 0;
                                }


                                //								if(lds_ItemInfo != null && lds_ItemInfo.Tables.Count >0)
                                //								{
                                //									DataRow[] ldrs_rows = lds_ItemInfo.Tables[0].Select("CD_ITEM ='"+ldt_dlg.Rows[i]["CD_ITEM"].ToString()+"'");
                                //									if( ldrs_rows != null && ldrs_rows.Length >0)
                                //									{
                                //										_flex[_flex.Row,"UM_EX_PO"]=  System.Double.Parse( ldrs_rows[0]["UM_ITEM"].ToString());
                                //										_flex[_flex.Row,"UM_EX"]=  System.Double.Parse( ldrs_rows[0]["UM_ITEM"].ToString()) / System.Double.Parse(ldt_dlg.Rows[i]["RT_PO"].ToString());
                                //										_flex[_flex.Row,"UM_P"] = System.Double.Parse(_flex[_flex.Row,"UM_EX_PO"].ToString()) * System.Double.Parse(tb_NM_EXCH.Text.Trim());
                                //										_flex[_flex.Row,"UM"] = System.Double.Parse(_flex[_flex.Row,"UM_EX"].ToString()) * System.Double.Parse(tb_NM_EXCH.Text.Trim());
                                //									}
                                //								}
                                //
                                //							
                                //								if( System.Double.Parse(_flex[_flex.Row,"RT_PO"].ToString()) > 0)
                                //								{	
                                //									if(ldt_dlg.Rows[i]["UNIT_PO"] == null || ldt_dlg.Rows[i]["UNIT_PO"].ToString().Trim() =="")
                                //									{
                                //										_flex[_flex.Row,"QT_PO_MM"] =0;
                                //									}
                                //									else if(ldt_dlg.Rows[i]["UNIT_PO"].ToString().Trim() !="")
                                //									{
                                //										_flex[_flex.Row,"QT_PO_MM"] = (System.Double.Parse(ldt_dlg.Rows[i]["QT_PR"].ToString()) / System.Double.Parse(_flex[_flex.Row,"RT_PO"].ToString()));								
                                //									}
                                //								}
                                //								else
                                //								{
                                //									_flex[_flex.Row,"QT_PO_MM"] =0;
                                //								}
                                //
                                //								
                                //								_flex[_flex.Row,"AM_EX"] =  System.Double.Parse(_flex[_flex.Row,"UM_EX_PO"].ToString()) * System.Double.Parse(_flex[_flex.Row,"QT_PO_MM"].ToString());
                                //								_flex[_flex.Row,"AM"] =  System.Math.Floor(System.Double.Parse(_flex[_flex.Row,"AM_EX"].ToString()) * System.Double.Parse(tb_NM_EXCH.Text.Trim()));
                                //								_flex[_flex.Row,"VAT"] = System.Math.Floor(System.Math.Floor(System.Double.Parse(_flex[_flex.Row,"AM"].ToString()) * (System.Double.Parse(tb_TAX.ClipText.ToString())/100 )));											
                                //				
                                //
                                //							
                                //								try
                                //								{
                                //									if(lds_ItemInfo != null && lds_ItemInfo.Tables.Count >3)
                                //									{
                                //										string ls_filter = "CD_ITEM ='"+ldt_dlg.Rows[i]["CD_ITEM"].ToString()+"' AND CD_PLANT ='"+ldt_dlg.Rows[i]["CD_PLANT"].ToString()+"'";
                                //
                                //										DataRow[] ldrs_rowsQT_POC = lds_ItemInfo.Tables[1].Select(ls_filter );
                                //										if( ldrs_rowsQT_POC != null && ldrs_rowsQT_POC.Length >0)
                                //										{
                                //											// 발주량
                                //											_flex[_flex.Row,"QT_POC"]= ldrs_rowsQT_POC[0]["QT_POC"];
                                //										}
                                //										else
                                //										{
                                //											_flex[_flex.Row,"QT_POC"]= 0;
                                //										}
                                //										DataRow[] ldrs_rowsQT_REQC = lds_ItemInfo.Tables[2].Select(ls_filter );
                                //										if( ldrs_rowsQT_REQC != null && ldrs_rowsQT_REQC.Length >0)
                                //										{
                                //											// 발주량
                                //											_flex[_flex.Row,"QT_REQC"]= ldrs_rowsQT_REQC[0]["QT_REQC"];
                                //										}
                                //										else
                                //										{
                                //											_flex[_flex.Row,"QT_REQC"]= 0;
                                //										}
                                //
                                //										DataRow[] ldrs_rowsQT_INVC = lds_ItemInfo.Tables[3].Select(ls_filter );
                                //										if( ldrs_rowsQT_INVC != null && ldrs_rowsQT_INVC.Length >0)
                                //										{										
                                //											_flex[_flex.Row,"QT_INVC"]= ldrs_rowsQT_INVC[0]["QT_INVC"];
                                //											_flex[_flex.Row,"QT_ATPC"]= ldrs_rowsQT_INVC[0]["QT_ATPC"];
                                //										}
                                //										else
                                //										{
                                //											_flex[_flex.Row,"QT_INVC"]= 0;
                                //											_flex[_flex.Row,"QT_ATPC"]= 0;
                                //										}
                                //									}
                                //									else
                                //									{
                                //										_flex[_flex.Row,"QT_POC"]= 0;
                                //										_flex[_flex.Row,"QT_REQC"]= 0;
                                //										_flex[_flex.Row,"QT_INVC"]= 0;
                                //										_flex[_flex.Row,"QT_ATPC"]= 0;
                                //									}
                                //								}
                                //								catch
                                //								{
                                //								}
                                //

                                _flex.AddFinished();
                                _flex.Col = _flex.Cols.Fixed;

                            }

                            SUMFunction();
                            SetControlEnabled(false, 1);
                        }
                    }

                    //	btn_RE_RCV.Enabled = false;
                }
                catch (coDbException ex)
                {
                    this.ShowErrorMessage(ex, this.PageName);
                }
                catch (Exception ex)
                {
                    this.ShowErrorMessage(ex, this.PageName);
                }
                finally
                {
                    _flex.Redraw = true;
                    tb_DC.Focus();
                }
            }
        }

        /// <summary>
        /// 요청적용을 가져온 정보를 가지고 품목 관련 정보검색
        /// 단가, 발주량,의뢰량,재고량, 가용재고량등등
        /// </summary>
        /// <param name="ps_cditems"></param>
        /// <param name="ps_itemplants"></param>
        /// <returns></returns>
        private DataSet SelectItemInfo(string ps_cditems, string ps_itemplants)
        {
            try
            {
                string[] ls_args1 = new string[12];
                ls_args1[0] = cbo_FG_UM.SelectedValue.ToString();
                ls_args1[1] = cbo_NM_EXCH.SelectedValue.ToString();
                ls_args1[2] = this.MainFrameInterface.LoginInfo.CompanyCode;
                ls_args1[3] = tb_DT_PO.MaskEditBox.ClipText;
                ls_args1[4] = tb_NM_PARTNER.CodeValue.ToString();
                ls_args1[5] = cbo_FG_UM.SelectedValue.ToString();
                ls_args1[6] = cbo_NM_EXCH.SelectedValue.ToString();
                ls_args1[7] = this.MainFrameInterface.LoginInfo.CompanyCode;
                ls_args1[8] = tb_DT_PO.MaskEditBox.ClipText;
                ls_args1[9] = tb_NM_PURGRP.CodeValue.ToString();
                ls_args1[10] = this.MainFrameInterface.LoginInfo.CompanyCode;
                ls_args1[11] = ps_cditems;

                string[] ls_args2 = new string[2];
                ls_args2[0] = this.MainFrameInterface.LoginInfo.CompanyCode;
                ls_args2[1] = ps_itemplants;

                object[] m_obj = new object[2];
                m_obj[0] = ls_args1;
                m_obj[1] = ls_args2;

                DataSet ds = (DataSet)(this.MainFrameInterface.InvokeRemoteMethod("PurOrderControl_NTX", "pur.CC_PU_PO_NTX", "CC_PU_PO_NTX.rem", "SelectPitemUMINV2", m_obj));

                return ds;

            }
            catch (coDbException ex)
            {
                this.ShowErrorMessage(ex, this.PageName);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex, this.PageName);
            }
            return new DataSet();
        }

        #endregion

        #region -> 품의적용 클릭
        /// <summary>
        /// 품의적용
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_RE_APP_Click(object sender, System.EventArgs e)
        {
            if (!FieldCheck_Head())
            {
                return;
            }

            Cursor.Current = Cursors.WaitCursor;
            pur.P_PU_APP_SUB2 m_dlg = new pur.P_PU_APP_SUB2(this.MainFrameInterface, ds_Ty1.Tables[5],
                cbo_CD_PLANT.SelectedValue.ToString(), cbo_CD_PLANT.SelectedText.ToString(),
                tb_NM_PURGRP.CodeValue.ToString(), tb_NM_PURGRP.CodeName, tb_NM_PARTNER.CodeValue.ToString(), tb_NM_PARTNER.CodeName);
            Cursor.Current = Cursors.Default;

            if (m_dlg.ShowDialog(this) == DialogResult.OK)
            {
                //DataView ldv_src = (DataView)dzdwGrid.DataSource;
                //DataTable ldt_src = (DataTable)ldv_src.Table;
                DataTable ldt_dlg = m_dlg.gdt_Return;

                try
                {
                    if (ldt_dlg != null)
                    {
                        if (ldt_dlg.Rows.Count > 0)
                        {
                            _tp_process = "3";
                            btn_insert.Enabled = false;
                            //	btn_RE_PJT.Enabled = false;
                            btn_ITEM_EXP.Enabled = false;
                            btn_RE_PR.Enabled = false;
                            //btn_RART_RECOM.Enabled = true;


                            string ls_cditemlist = "";
                            string ls_itemplant = "";
                            for (int i = 0; i < ldt_dlg.Rows.Count; i++)
                            {
                                if (ldt_dlg.Rows[i].RowState.ToString() == "Deleted")
                                {
                                    continue;
                                }
                                ls_cditemlist += ldt_dlg.Rows[i]["CD_ITEM"].ToString();

                                if (i + 1 < ldt_dlg.Rows.Count)
                                {
                                    ls_cditemlist += "','";
                                }
                                ls_itemplant += "(L.CD_ITEM ='" + ldt_dlg.Rows[i]["CD_ITEM"].ToString().Trim() + "' AND L.CD_PLANT = '" +
                                    ldt_dlg.Rows[i]["CD_PLANT"].ToString().Trim() + "') ";
                                if (i + 1 < ldt_dlg.Rows.Count)
                                {
                                    ls_itemplant += " OR ";
                                }
                            }

                            //		DataSet lds_ItemInfo = SelectItemInfo(ls_cditemlist,ls_itemplant);


                            _flex.Redraw = false;
                            for (int i = 0; i < ldt_dlg.Rows.Count; i++)
                            {
                                if (ldt_dlg.Rows[i].RowState.ToString() == "Deleted")
                                {
                                    continue;
                                }

                                _flex.Rows.Add();
                                _flex.Row = _flex.Rows.Count - 1;

                                _flex[_flex.Row, "NO_PO"] = tb_NO_PO.Text.Trim();
                                _flex[_flex.Row, "FG_POCON"] = "001";
                                _flex[_flex.Row, "NO_LINE"] = GetMaxPoLine();
                                _flex[_flex.Row, "QT_REQ"] = 0;
                                _flex[_flex.Row, "QT_RCV"] = 0;



                                _flex[_flex.Row, "AM"] = 0;
                                _flex[_flex.Row, "UM"] = 0;

                                _flex[_flex.Row, "AM_EX"] = 0;
                                _flex[_flex.Row, "UM_EX"] = 0;
                                _flex[_flex.Row, "VAT"] = 0;

                                _flex[_flex.Row, "UM_P"] = 0;
                                _flex[_flex.Row, "UM_EX_PO"] = 0;
                                _flex[_flex.Row, "UM_EX"] = 0;
                                _flex[_flex.Row, "UM"] = 0;

                                //								if(lds_ItemInfo != null && lds_ItemInfo.Tables.Count >0)
                                //								{
                                //									DataRow[] ldrs_rows = lds_ItemInfo.Tables[0].Select("CD_ITEM ='"+ldt_dlg.Rows[i]["CD_ITEM"].ToString()+"'");
                                //									if( ldrs_rows != null && ldrs_rows.Length >0)
                                //									{
                                //										_flex[_flex.Row,"UM_EX_PO"]=  System.Double.Parse( ldrs_rows[0]["UM_ITEM"].ToString());
                                //										_flex[_flex.Row,"UM_EX"]=  System.Double.Parse( ldrs_rows[0]["UM_ITEM"].ToString()) / System.Double.Parse(ldt_dlg.Rows[i]["RT_PO"].ToString());
                                //										_flex[_flex.Row,"UM_P"] = System.Double.Parse(_flex[_flex.Row,"UM_EX_PO"].ToString()) * System.Double.Parse(tb_NM_EXCH.Text.Trim());
                                //										_flex[_flex.Row,"UM"] = System.Double.Parse(_flex[_flex.Row,"UM_EX"].ToString()) * System.Double.Parse(tb_NM_EXCH.Text.Trim());
                                //									}
                                //								}

                                _flex[_flex.Row, "QT_PO_MM"] = 0;
                                _flex[_flex.Row, "RT_PO"] = ldt_dlg.Rows[i]["RT_PO"];

                                if (System.Double.Parse(_flex[_flex.Row, "RT_PO"].ToString()) > 0)
                                {
                                    if (ldt_dlg.Rows[i]["UNIT_PO"] == null || ldt_dlg.Rows[i]["UNIT_PO"].ToString().Trim() == "")
                                    {
                                        _flex[_flex.Row, "QT_PO_MM"] = 0;
                                    }
                                    else if (ldt_dlg.Rows[i]["UNIT_PO"].ToString().Trim() != "")
                                    {
                                        _flex[_flex.Row, "QT_PO_MM"] = (System.Double.Parse(ldt_dlg.Rows[i]["QT_APP"].ToString()) / System.Double.Parse(_flex[_flex.Row, "RT_PO"].ToString()));
                                    }
                                }
                                else
                                {
                                    _flex[_flex.Row, "QT_PO_MM"] = 0;
                                }



                                _flex[_flex.Row, "UM_EX_PO"] = System.Double.Parse(ldt_dlg.Rows[i]["UM"].ToString()) * System.Double.Parse(ldt_dlg.Rows[i]["RT_PO"].ToString()); ;
                                _flex[_flex.Row, "UM_EX"] = System.Double.Parse(ldt_dlg.Rows[i]["UM"].ToString());
                                _flex[_flex.Row, "UM_P"] = System.Double.Parse(_flex[_flex.Row, "UM_EX_PO"].ToString()) * System.Double.Parse(tb_NM_EXCH.Text.Trim());
                                _flex[_flex.Row, "UM"] = System.Double.Parse(ldt_dlg.Rows[i]["UM"].ToString()) * System.Double.Parse(tb_NM_EXCH.Text.Trim());




                                _flex[_flex.Row, "AM_EX"] = System.Double.Parse(_flex[_flex.Row, "UM_EX_PO"].ToString()) * System.Double.Parse(_flex[_flex.Row, "QT_PO_MM"].ToString());
                                _flex[_flex.Row, "AM"] = System.Math.Floor(System.Double.Parse(_flex[_flex.Row, "AM_EX"].ToString()) * System.Double.Parse(tb_NM_EXCH.Text.Trim()));
                                _flex[_flex.Row, "VAT"] = System.Math.Floor(System.Math.Floor(System.Double.Parse(_flex[_flex.Row, "AM"].ToString()) * (System.Double.Parse(tb_TAX.ClipText.ToString()) / 100)));

                                _flex[_flex.Row, "NO_CONTRACT"] = ldt_dlg.Rows[i]["NO_CONTRACT"].ToString();
                                _flex[_flex.Row, "NO_CTLINE"] = ldt_dlg.Rows[i]["NO_CTLINE"];

                                // 의뢰
                                _flex[_flex.Row, "NO_RCV"] = ""; //UM_EXP
                                _flex[_flex.Row, "NO_RCVLINE"] = 0;


                                // 요청적용에서 가지고 오는 것이므로..요청번호와 항번입력
                                _flex[_flex.Row, "NO_PR"] = ldt_dlg.Rows[i]["NO_PR"].ToString();
                                _flex[_flex.Row, "NO_PRLINE"] = ldt_dlg.Rows[i]["NO_PRLINE"];

                                //품의적용
                                _flex[_flex.Row, "NO_APP"] = ldt_dlg.Rows[i]["NO_APP"];
                                _flex[_flex.Row, "NO_APPLINE"] = ldt_dlg.Rows[i]["NO_APPLINE"];

                                _flex[_flex.Row, "FG_POST"] = "O";
                                _flex[_flex.Row, "FG_POCON"] = "001";
                                _flex[_flex.Row, "NM_SYSDEF"] = _ComfirmState;

                                //	_flex[_flex.Row,"NM_PLANT"] = ldt_dlg.Rows[i]["NM_PLANT"].ToString();
                                _flex[_flex.Row, "CD_PLANT"] = ldt_dlg.Rows[i]["CD_PLANT"].ToString();
                                _flex[_flex.Row, "CD_ITEM"] = ldt_dlg.Rows[i]["CD_ITEM"].ToString();
                                _flex[_flex.Row, "NM_ITEM"] = ldt_dlg.Rows[i]["NM_ITEM"].ToString();
                                _flex[_flex.Row, "CD_UNIT_MM"] = ldt_dlg.Rows[i]["UNIT_PO"].ToString();
                                _flex[_flex.Row, "UNIT_PO"] = ldt_dlg.Rows[i]["UNIT_PO"].ToString();
                                _flex[_flex.Row, "STND_MA_ITEM"] = ldt_dlg.Rows[i]["STND_ITEM"].ToString();
                                _flex[_flex.Row, "STND_ITEM"] = ldt_dlg.Rows[i]["STND_ITEM"].ToString();
                                _flex[_flex.Row, "UNIT_IM"] = ldt_dlg.Rows[i]["UNIT_IM"].ToString();
                                _flex[_flex.Row, "QT_PO"] = ldt_dlg.Rows[i]["QT_APP"];
                                _flex[_flex.Row, "DT_LIMIT"] = ldt_dlg.Rows[i]["DT_LIMIT"].ToString();

                                _flex[_flex.Row, "CD_SL"] = ldt_dlg.Rows[i]["CD_SL"].ToString();
                                _flex[_flex.Row, "NM_SL"] = ldt_dlg.Rows[i]["NM_SL"].ToString();
                                _flex[_flex.Row, "GR_SL"] = ldt_dlg.Rows[i]["NM_SL"].ToString();

                                _flex[_flex.Row, "CD_PJT"] = tb_CD_PJT.CodeValue.ToString();
                                _flex[_flex.Row, "NM_PJT"] = tb_CD_PJT.CodeName;
                                _flex[_flex.Row, "PNM_PJT"] = tb_CD_PJT.CodeName;

                                object[] m_obj = new object[8];
                                m_obj[0] = _flex[_flex.Row, "CD_ITEM"].ToString();
                                m_obj[1] = _flex[_flex.Row, "CD_PLANT"].ToString();
                                m_obj[2] = this.MainFrameInterface.LoginInfo.CompanyCode;
                                m_obj[3] = cbo_FG_UM.SelectedValue.ToString();
                                m_obj[4] = cbo_NM_EXCH.SelectedValue.ToString();
                                m_obj[5] = tb_DT_PO.MaskEditBox.ClipText;
                                m_obj[6] = tb_NM_PARTNER.CodeValue.ToString();
                                m_obj[7] = tb_NM_PURGRP.CodeValue.ToString();


                                Duzon.Common.Util.SpInfo si = new Duzon.Common.Util.SpInfo();
                                ResultData result = (ResultData)this.FillDataSet("SP_PU_PO_ITEMINFO_SELECT", m_obj);
                                DataSet lds_ItemInfo = (DataSet)result.DataValue;


                                if (lds_ItemInfo != null && lds_ItemInfo.Tables.Count > 3)
                                {

                                    if (lds_ItemInfo.Tables[2] != null && lds_ItemInfo.Tables[2].Rows.Count > 0)
                                    {
                                        // 발주량
                                        _flex[_flex.Row, "QT_POC"] = lds_ItemInfo.Tables[2].Rows[0]["QT_POC"];
                                        _flex[_flex.Row, "QT_REQC"] = lds_ItemInfo.Tables[2].Rows[0]["QT_REQC"];
                                    }
                                    else
                                    {
                                        _flex[_flex.Row, "QT_POC"] = 0;
                                        _flex[_flex.Row, "QT_REQC"] = 0;
                                    }

                                    if (lds_ItemInfo.Tables[3] != null && lds_ItemInfo.Tables[3].Rows.Count > 0)
                                    {
                                        _flex[_flex.Row, "QT_INVC"] = lds_ItemInfo.Tables[3].Rows[0]["QT_INVC"];
                                        _flex[_flex.Row, "QT_ATPC"] = lds_ItemInfo.Tables[3].Rows[0]["QT_ATPC"];
                                    }
                                    else
                                    {
                                        _flex[_flex.Row, "QT_INVC"] = 0;
                                        _flex[_flex.Row, "QT_ATPC"] = 0;
                                    }
                                }
                                else
                                {
                                    _flex[_flex.Row, "QT_POC"] = 0;
                                    _flex[_flex.Row, "QT_REQC"] = 0;
                                    _flex[_flex.Row, "QT_INVC"] = 0;
                                    _flex[_flex.Row, "QT_ATPC"] = 0;
                                }



                                //
                                //								if(lds_ItemInfo != null && lds_ItemInfo.Tables.Count >3)
                                //								{
                                //									string ls_filter = "CD_ITEM ='"+ldt_dlg.Rows[i]["CD_ITEM"].ToString()+"' AND CD_PLANT ='"+ldt_dlg.Rows[i]["CD_PLANT"].ToString()+"'";
                                //
                                //									DataRow[] ldrs_rowsQT_POC = lds_ItemInfo.Tables[1].Select(ls_filter );
                                //									if( ldrs_rowsQT_POC != null && ldrs_rowsQT_POC.Length >0)
                                //									{
                                //										// 발주량
                                //										_flex[_flex.Row,"QT_POC"]= ldrs_rowsQT_POC[0]["QT_POC"];
                                //									}
                                //									else
                                //									{
                                //										_flex[_flex.Row,"QT_POC"]= 0;
                                //									}
                                //									DataRow[] ldrs_rowsQT_REQC = lds_ItemInfo.Tables[2].Select(ls_filter );
                                //									if( ldrs_rowsQT_REQC != null && ldrs_rowsQT_REQC.Length >0)
                                //									{
                                //										// 발주량
                                //										_flex[_flex.Row,"QT_REQC"]= ldrs_rowsQT_REQC[0]["QT_REQC"];
                                //									}
                                //									else
                                //									{
                                //										_flex[_flex.Row,"QT_REQC"]= 0;
                                //									}
                                //
                                //									DataRow[] ldrs_rowsQT_INVC = lds_ItemInfo.Tables[3].Select(ls_filter );
                                //									if( ldrs_rowsQT_INVC != null && ldrs_rowsQT_INVC.Length >0)
                                //									{										
                                //										_flex[_flex.Row,"QT_INVC"]= ldrs_rowsQT_INVC[0]["QT_INVC"];
                                //										_flex[_flex.Row,"QT_ATPC"]= ldrs_rowsQT_INVC[0]["QT_ATPC"];
                                //									}
                                //									else
                                //									{
                                //										_flex[_flex.Row,"QT_INVC"]= 0;
                                //										_flex[_flex.Row,"QT_ATPC"]= 0;
                                //									}
                                //								}
                                //								else
                                //								{
                                //									_flex[_flex.Row,"QT_POC"]= 0;
                                //									_flex[_flex.Row,"QT_REQC"]= 0;
                                //									_flex[_flex.Row,"QT_INVC"]= 0;
                                //									_flex[_flex.Row,"QT_ATPC"]= 0;
                                //								}


                                //								_flex[_flex.Row,"RT_PO"] =1;

                                _flex.AddFinished();
                                _flex.Col = _flex.Cols.Fixed;
                            }

                            SUMFunction();
                            SetControlEnabled(false, 1);
                        }
                    }
                }
                catch (coDbException ex)
                {
                    this.ShowErrorMessage(ex, this.PageName);
                }
                catch (Exception ex)
                {
                    this.ShowErrorMessage(ex, this.PageName);
                }
                finally
                {
                    _flex.Redraw = true;
                    lb_CD_PJT.Focus();
                    tb_DC.Focus();
                }
            }
        }


        #endregion

        #region -> 품목전개
        /// <summary>
        /// 품목 전개 버턴 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ITEM_EXP_Click(object sender, System.EventArgs e)
        {
            try
            {
                tb_DT_PO.Focus();
                if (!FieldCheck_Head())
                {
                    return;
                }

                if (cbo_CD_PLANT.SelectedValue.ToString().Trim() == "")
                {//PU_M000070

                    this.ShowMessage("PU_M000070");
                    //Duzon.Common.Controls.MessageBoxEx.Show(this.GetMessageDictionaryItem("PU_M000070"),this.PageName);
                    //	MessageBoxEx.Show("공장을 선택하십시오.");				
                    return;
                }

                pur.P_PU_PO_ITEMEXPSUB m_dlg = new pur.P_PU_PO_ITEMEXPSUB(this.MainFrameInterface,
                    cbo_CD_PLANT.SelectedValue.ToString().Trim(),
                    tb_NM_PARTNER.CodeValue.ToString(), tb_NM_PARTNER.CodeName, cbo_FG_UM.SelectedValue.ToString(),
                    tb_DT_PO.MaskEditBox.ClipText.Trim(), tb_DT_PO.MaskEditBox.ClipText, cbo_NM_EXCH.SelectedValue.ToString());

                if (m_dlg.ShowDialog(this) == DialogResult.OK)
                {
                    SetITEMEXP(m_dlg.gdt_return);
                }
            }
            catch
            {
            }
            finally
            {
                _flex.Focus();
            }
        }

        /// <summary>
        /// 품목전개되면 그리드에 뿌려줌
        /// </summary>
        /// <param name="ldt_dlg"></param>
        private void SetITEMEXP(DataTable ldt_dlg)
        {
            try
            {
                if (ldt_dlg != null && ldt_dlg.Rows.Count > 0)
                {

                    _flex.Redraw = false;
                    for (int i = 0; i < ldt_dlg.Rows.Count; i++)
                    {
                        if (ldt_dlg.Rows[i].RowState.ToString() == "Deleted")
                        {
                            continue;
                        }

                        _flex.Rows.Add();
                        _flex.Row = _flex.Rows.Count - 1;

                        _flex[_flex.Row, "NO_PO"] = tb_NO_PO.Text;
                        _flex[_flex.Row, "CD_COMPANY"] = this.LoginInfo.CompanyCode;
                        _flex[_flex.Row, "DT_LIMIT"] = this.MainFrameInterface.GetStringToday;

                        _flex[_flex.Row, "FG_POCON"] = "001";
                        _flex[_flex.Row, "NO_LINE"] = GetMaxPoLine();
                        _flex[_flex.Row, "QT_REQ"] = 0;
                        _flex[_flex.Row, "QT_RCV"] = 0;
                        _flex[_flex.Row, "QT_PO_MM"] = 0;

                        _flex[_flex.Row, "AM"] = 0;
                        _flex[_flex.Row, "AM_EX"] = 0;
                        _flex[_flex.Row, "VAT"] = 0;

                        _flex[_flex.Row, "RT_PO"] = 1;
                        _flex[_flex.Row, "CD_SL"] = "";

                        _flex[_flex.Row, "NO_CONTRACT"] = "";
                        _flex[_flex.Row, "NO_CTLINE"] = 0;

                        // 의뢰
                        _flex[_flex.Row, "NO_RCV"] = ""; //UM_EXP
                        _flex[_flex.Row, "NO_RCVLINE"] = 0;
                        //구매요청
                        _flex[_flex.Row, "NO_PR"] = "";
                        _flex[_flex.Row, "NO_PRLINE"] = 0;

                        // 품의
                        _flex[_flex.Row, "NO_APP"] = "";
                        _flex[_flex.Row, "NO_APPLINE"] = 0;

                        // 계약
                        _flex[_flex.Row, "NO_CONTRACT"] = "";
                        _flex[_flex.Row, "NO_CTLINE"] = 0;

                        // 프로젝트
                        _flex[_flex.Row, "CD_PJT"] = tb_CD_PJT.CodeValue.ToString();
                        _flex[_flex.Row, "NM_PJT"] = tb_CD_PJT.CodeName;
                        _flex[_flex.Row, "PNM_PJT"] = tb_CD_PJT.CodeName;

                        _flex[_flex.Row, "FG_POST"] = "O";
                        _flex[_flex.Row, "FG_POCON"] = "001";
                        _flex[_flex.Row, "NM_SYSDEF"] = _ComfirmState;

                        _flex[_flex.Row, "CD_PLANT"] = ldt_dlg.Rows[i]["CD_PLANT"].ToString();
                        _flex[_flex.Row, "CD_ITEM"] = ldt_dlg.Rows[i]["CD_ITEM"].ToString();
                        _flex[_flex.Row, "NM_ITEM"] = ldt_dlg.Rows[i]["NM_ITEM"].ToString();
                        _flex[_flex.Row, "CD_UNIT_MM"] = ldt_dlg.Rows[i]["CD_UNIT_MM"].ToString();
                        _flex[_flex.Row, "UNIT_PO"] = ldt_dlg.Rows[i]["CD_UNIT_MM"].ToString();
                        _flex[_flex.Row, "STND_ITEM"] = ldt_dlg.Rows[i]["STND_ITEM"].ToString();
                        _flex[_flex.Row, "UNIT_IM"] = ldt_dlg.Rows[i]["UNIT_IM"].ToString();

                        try
                        {
                            _flex[_flex.Row, "RT_PO"] = ldt_dlg.Rows[i]["RATE_EXCHG"].ToString();
                            _flex[_flex.Row, "UM_EX_PO"] = System.Double.Parse(ldt_dlg.Rows[i]["UM_ITEM"].ToString());
                            _flex[_flex.Row, "UM_EX"] = System.Double.Parse(ldt_dlg.Rows[i]["UM_ITEM"].ToString()) / System.Double.Parse(ldt_dlg.Rows[i]["RATE_EXCHG"].ToString());
                            _flex[_flex.Row, "UM_P"] = System.Double.Parse(_flex[_flex.Row, "UM_EX_PO"].ToString()) * System.Double.Parse(tb_NM_EXCH.Text.Trim());
                            _flex[_flex.Row, "UM"] = System.Double.Parse(_flex[_flex.Row, "UM_EX"].ToString()) * System.Double.Parse(tb_NM_EXCH.Text.Trim());
                        }
                        catch
                        {
                        }
                        _tp_process = "2";

                        _flex.AddFinished();
                        _flex.Col = _flex.Cols.Fixed;
                    }

                    btn_RE_PR.Enabled = false;
                    btn_RE_APP.Enabled = false;
                    this.ToolBarSaveButtonEnabled = true;
                    SetControlEnabled(false, 1);
                }
            }
            catch
            {
            }
            finally
            {
                _flex.Redraw = true;
            }

        }


        #endregion

        #region -> 날짜 에 관련된 함수 및 이벤트

        private void DataPickerValidated(object sender, System.EventArgs e)
        {
            try
            {
                if (!((DatePicker)sender).Modified)
                    return;

                if (((DatePicker)sender).Text == string.Empty)
                    return;

                // 유효성 검사
                if (!((DatePicker)sender).IsValidated)
                {
                    this.ShowMessage("WK1_003");
                    ((DatePicker)sender).Text = string.Empty;
                    ((DatePicker)sender).Focus();
                    return;
                }

                //				if(this.m_dtpFrom.Text != string.Empty && this.m_dtpTo.Text != string.Empty)
                //				{
                //					// From To 체크
                //					CommonFunction objComm = new CommonFunction();
                //					if(objComm.DiffDate(this.m_dtpFrom.Text, this.m_dtpTo.Text) > 0)
                //					{
                //						this.ShowMessage("WK1_007", GetDDItem("DT_DISCIP") );
                //						this.m_dtpTo.Focus();
                //						return;
                //					}
                //				}

            }
            catch
            {
            }
        }



        #endregion

        #region -> 발주형태 텍스트박스내의 이벤트들

        private bool SearchFGPOTR(string ps_value)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                object[] m_obj = new object[2];
                m_obj[0] = this.MainFrameInterface.LoginInfo.CompanyCode;
                m_obj[1] = ps_value;

                //UP_PU_TPPO_SEARCH_SELECT

                ResultData result = (ResultData)this.FillDataSet("UP_PU_TPPO_SEARCH_SELECT", m_obj);
                DataSet ds_TPPO = (DataSet)result.DataValue;

                //	DataSet ds_TPPO = (DataSet)(this.MainFrameInterface.InvokeRemoteMethod("PurOrderControl_NTX", "pur.CC_PU_PO_NTX","CC_PU_PO_NTX.rem", "SelectTppo", m_obj));					

                Cursor.Current = Cursors.Default;
                if (ds_TPPO != null && ds_TPPO.Tables[0].Rows.Count > 0)
                {
                    tb_FG_PO_TR.CodeName = ds_TPPO.Tables[0].Rows[0]["NM_TPPO"].ToString();
                    tb_FG_PO_TR.CodeValue = ds_TPPO.Tables[0].Rows[0]["CD_TPPO"].ToString();
                    tb_NM_TRANS.Text = ds_TPPO.Tables[0].Rows[0]["NM_TRANS"].ToString();

                    ds_Ty1.Tables[2].Clear();

                    DataRow newRow = ds_Ty1.Tables[2].NewRow();
                    newRow["FG_TRANS"] = ds_TPPO.Tables[0].Rows[0]["FG_TRANS"].ToString();
                    newRow["FG_TPRCV"] = ds_TPPO.Tables[0].Rows[0]["FG_TPRCV"].ToString();
                    newRow["FG_TPPURCHASE"] = ds_TPPO.Tables[0].Rows[0]["FG_TPPURCHASE"].ToString();
                    newRow["YN_AUTORCV"] = ds_TPPO.Tables[0].Rows[0]["YN_AUTORCV"].ToString();
                    newRow["YN_RCV"] = ds_TPPO.Tables[0].Rows[0]["YN_RCV"].ToString();
                    newRow["YN_RETURN"] = ds_TPPO.Tables[0].Rows[0]["YN_RETURN"].ToString();
                    newRow["YN_SUBCON"] = ds_TPPO.Tables[0].Rows[0]["YN_SUBCON"].ToString();
                    newRow["YN_IMPORT"] = ds_TPPO.Tables[0].Rows[0]["YN_IMPORT"].ToString();
                    newRow["YN_ORDER"] = ds_TPPO.Tables[0].Rows[0]["YN_ORDER"].ToString();
                    newRow["YN_REQ"] = ds_TPPO.Tables[0].Rows[0]["YN_REQ"].ToString();
                    newRow["NM_TRANS"] = ds_TPPO.Tables[0].Rows[0]["NM_TRANS"].ToString();//YN_AM
                    newRow["YN_AM"] = ds_TPPO.Tables[0].Rows[0]["YN_AM"].ToString();
                    ds_Ty1.Tables[2].Rows.Add(newRow);

                    SetVATType(ds_TPPO.Tables[0].Rows[0]["FG_TRANS"].ToString().Trim());

                    return true;

                }
                else
                {
                    return false;
                }
            }
            catch (coDbException ex)
            {
                this.ShowErrorMessage(ex, this.PageName);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex, this.PageName);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
            return false;
        }


        #endregion

        #region -> 환율관련 함수

        private void cbo_NM_EXCH_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            try
            {

                //DataView ddv = (DataView)dzdwGrid.DataSource;					
                if (cbo_NM_EXCH.SelectedValue.ToString() == "000")
                {
                    tb_NM_EXCH.Text = "1.0";
                    tb_NM_EXCH.Enabled = false;
                }
                else
                {
                    tb_NM_EXCH.Enabled = true;
                }

                SetExchageMoney();


            }
            catch
            {
            }
        }

        private void tb_NM_EXCH_Leave(object sender, System.EventArgs e)
        {
            // 확율 다시 적용
            SetExchageMoney();
        }

        #endregion

        #region -> 계산서 처리구분
        private void rbtn_All_Click(object sender, System.EventArgs e)
        {
            rbtn_All.Checked = true;
            rbtn_PRI.Checked = false;

        }

        private void rbtn_PRI_Click(object sender, System.EventArgs e)
        {
            rbtn_All.Checked = false;
            rbtn_PRI.Checked = true;
        }

        /// <summary>
        /// 세금 타입. (일괄, 건별)
        /// </summary>
        private string TaxType
        {
            get
            {
                if (rbtn_All.Checked)
                {
                    return "001";
                }
                else
                {
                    return "002";
                }
            }
        }
        #endregion

        #region -> KeyUp Event... 하하하



        private void Control_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData.ToString() == "Enter")
            {
                SendKeys.SendWait("{TAB}");
            }

        }


        private void tb_DC_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData.ToString() == "Enter")
            {
                tb_DT_PO.Focus();
            }
        }

        #endregion

        #region -> 재고확인 클릭

        /// <summary>
        /// 재고확인 버턴 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_CM_INV_Click(object sender, System.EventArgs e)
        {
            try
            {
                GetQTCurrent();
                SetQtValue(_flex.DataIndex(_flex.Row));
            }
            catch
            {
            }
        }

        #endregion

        #endregion

        #region ♣ 기타 메소드

        #region -> 단위 환산량 구하는 부분


        // 보완 한것 -- 20020906
        /// <summary>
        /// 단위환산량 가져옴
        /// </summary>
        /// <returns></returns>
        private double SetQTREMMSubsystem(string ps_ex) // 발주량 계산 로직 수정
        {

            double ldb_qtreqmm = 1;
            DataSet ds = new DataSet();
            ////DataView ddv = (DataView)dzdwGrid.DataSource;	
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (ps_ex.Trim() != "")
                {
                    object[] m_obj = new object[4];
                    m_obj[0] = this.MainFrameInterface.LoginInfo.CompanyCode;       //회사코드
                    m_obj[1] = _flex[_flex.Row, "CD_ITEM"].ToString();              //품목코드
                    m_obj[2] = cbo_CD_PLANT.SelectedValue.ToString();               //공장코드
                    m_obj[3] = "PU";                                                //바뀐 프로시져 매개변수 모듈별로 다름

                    //m_obj[1] = _flex[_flex.Row,"UNIT_IM"].ToString();             //관리단위
                    //m_obj[2] = ps_ex;					                            //발주단위     
                    //		m_obj[2] = _flex[_flex.Row,"CD_UNIT_MM"].ToString();
                    //UP_PU_RATE_EXCHG_SELECT UP_PU_RATE_EXCHG_SELECT

                    ResultData result = (ResultData)this.FillDataSet("UP_PU_PITEM_RATE_EXCHG_SELECT", m_obj);
                    ds = (DataSet)result.DataValue;


                    //	ds = (DataSet)(this.MainFrameInterface.InvokeRemoteMethod("PurOrderControl_NTX", "pur.CC_PU_PO_NTX","CC_PU_PO_NTX.rem", "SelectRateExchg", m_obj));			

                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            //////_flex.DataView[ _flex.DataIndex( _flex.Row)].EndEdit();;;
                            _flex[_flex.Row, "RT_PO"] = ds.Tables[0].Rows[0]["RATE_EXCHG"];      //구매단위계수 가져옴
                            //_flex.DataView[ _flex.DataIndex( _flex.Row)].EndEdit();
                            ldb_qtreqmm = System.Double.Parse(ds.Tables[0].Rows[0]["RATE_EXCHG"].ToString());
                        }
                        else
                        {
                            _flex[_flex.Row, "RT_PO"] = 1;
                            ldb_qtreqmm = 1;
                        }
                    }
                    else
                    {
                        _flex[_flex.Row, "RT_PO"] = 1;
                        ldb_qtreqmm = 1;
                    }
                }
                else
                {
                    _flex[_flex.Row, "RT_PO"] = 1;
                    ldb_qtreqmm = 1;
                }

                //	MessageBox.Show(this.MainFrameInterface.LoginInfo.CompanyCode+"   " + _flex[_flex.Row,"UNIT_IM"].ToString()+"      "+dzdwGrid[dzdwGrid.CurrentCell.RowIndex,6].CellValue.ToString()+"         "+ldb_qtreqmm.ToString());

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                _flex[_flex.Row, "RT_PO"] = 1;
                ldb_qtreqmm = 1;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
            return ldb_qtreqmm;
        }




        #endregion

        #region -> 최대항번

        /// <summary>
        /// 최대항번 게산
        /// </summary>
        private int GetMaxPoLine()
        {
            int li_line = 1;
            try
            {
                //DataView ddv = (DataView)dzdwGrid.DataSource;
                li_line = System.Int32.Parse(_flex.DataTable.Compute("MAX(NO_LINE)", "").ToString());
                li_line++;
            }
            catch
            {
            }
            return li_line;
        }

        #endregion

        #region -> 상태 변경


        /// <summary>
        /// 상태변경
        /// </summary>
        private void ChagePoState()
        {
            try
            {
                string ps_R = "Release";
                string ps_S = "Start";
                string ps_C = "Close";
                bool isChaged = false;

                for (int i = 0; i < _dsCombo.Tables[4].Rows.Count; i++)
                {
                    if (_dsCombo.Tables[4].Rows[i]["CODE"].ToString().Trim() == "R")
                    {
                        ps_R = _dsCombo.Tables[4].Rows[i]["NAME"].ToString();
                    }
                    else if (_dsCombo.Tables[4].Rows[i]["CODE"].ToString().Trim() == "S")
                    {
                        ps_S = _dsCombo.Tables[4].Rows[i]["NAME"].ToString();
                    }
                    else if (_dsCombo.Tables[4].Rows[i]["CODE"].ToString().Trim() == "C")
                    {
                        ps_C = _dsCombo.Tables[4].Rows[i]["NAME"].ToString();
                    }
                }


                for (int i = 0; i < _flex.DataView.Count; i++)
                {
                    if (_flex.DataView[i].Row.RowState.ToString() == "Added")
                    {
                        _flex.DataView[i]["NO_PO"] = tb_NO_PO.Text;

                        // 자동승인이면
                        if (_flex.DataView[i]["YN_ORDER"].ToString().Trim() == "Y")
                        {
                            _flex.DataView[i]["NM_SYSDEF"] = ps_R;
                            //	ldv_src[i]["FG_POCON"] = "002";
                            _flex.DataView[i]["FG_POCON"] = "001";
                            //	isChaged = true;
                        }

                        // 의뢰가 존재하지 않으면 
                        if (_flex.DataView[i]["YN_REQ"].ToString().Trim() == "N")
                        {
                            _flex.DataView[i]["NM_SYSDEF"] = ps_S;
                            _flex.DataView[i]["FG_POCON"] = "002";
                            isChaged = true;

                            // 자동 입고이면
                            if (_flex.DataView[i]["YN_AUTORCV"].ToString().Trim() == "Y")
                            {
                                _flex.DataView[i]["NM_SYSDEF"] = ps_C;
                                _flex.DataView[i]["FG_POCON"] = "003";
                            }
                        }
                    }
                }

                if (isChaged)
                {
                    SetControlEnabled(false, 2);
                    btn_RE_PR.Enabled = false;
                    btn_RE_APP.Enabled = false;
                    //_flex.AllowEditing = false;
                    _isChagePossible = false;

                    ToolBarSaveButtonEnabled = false;
                }
            }
            catch
            {
            }
        }

        #endregion

        #region -> 헤더 값 설정 부분

        /// <summary>
        /// 헤더 DataTable  생성 함수
        /// </summary>
        private void InDataHeadValue()
        {
            DataRow newrow;

            ds_Ty1.Tables[3].Clear();

            newrow = ds_Ty1.Tables[3].NewRow();
            newrow["NO_PO"] = tb_NO_PO.Text;
            newrow["CD_COMPANY"] = this.MainFrameInterface.LoginInfo.CompanyCode;
            newrow["DTS_UPDATE"] = _dtsUpdate;
            ds_Ty1.Tables[3].Rows.Add(newrow);

            if (_page_state == "Modified")
            {
                ds_Ty1.Tables[3].AcceptChanges();
            }

            ds_Ty1.Tables[3].BeginInit();
            DataRow ldr_row = ds_Ty1.Tables[3].Rows[0];

            ldr_row["CD_PLANT"] = cbo_CD_PLANT.SelectedValue.ToString();
            ldr_row["CD_PARTNER"] = tb_NM_PARTNER.CodeValue.ToString();
            ldr_row["DT_PO"] = tb_DT_PO.MaskEditBox.ClipText;
            ldr_row["CD_PURGRP"] = tb_NM_PURGRP.CodeValue.ToString();
            ldr_row["NO_EMP"] = tb_NO_EMP.CodeValue.ToString();
            ldr_row["CD_TPPO"] = tb_FG_PO_TR.CodeValue.ToString();
            ldr_row["FG_UM"] = cbo_FG_UM.SelectedValue.ToString();
            ldr_row["FG_PAYMENT"] = cbo_PAYment.SelectedValue.ToString();
            ldr_row["FG_TAX"] = cbo_FG_TAX.SelectedValue.ToString();
            ldr_row["TP_UM_TAX"] = cbo_TP_TAX.SelectedValue.ToString();
            ldr_row["CD_PJT"] = tb_CD_PJT.CodeValue;
            ldr_row["CD_EXCH"] = cbo_NM_EXCH.SelectedValue.ToString();
            ldr_row["RT_EXCH"] = (double)tb_NM_EXCH.DecimalValue;
            ldr_row["AM_EX"] = _db_am;
            ldr_row["AM"] = _db_amk;
            ldr_row["VAT"] = _db_vat;
            ldr_row["DC50_PO"] = tb_DC.Text;
            ldr_row["TP_PROCESS"] = _tp_process;
            ldr_row["FG_TAXP"] = TaxType;
            ldr_row["YN_AM"] = ds_Ty1.Tables[2].Rows[0]["YN_AM"].ToString();
            ldr_row["FG_TRANS"] = ds_Ty1.Tables[2].Rows[0]["FG_TRANS"].ToString();

            ldr_row["CD_DEPT"] = tb_CD_DEPT.Tag;
            //ldr_row["NM_DEPT"] = tb_CD_DEPT.Text.ToString();

            ds_Ty1.Tables[3].EndInit();
        }

        #endregion

        #region -> 저장 도움 함수들

        /// <summary>
        /// PU_POH 테이블의 발주형태에 대한 값을 Save시 처리하는 함수
        /// </summary>
        /// <returns></returns>
        private bool DataAddForPOL()
        {
            //DataView ddv = (DataView)dzdwGrid.DataSource;
            try
            {

                if (ds_Ty1.Tables[2].Rows.Count > 0)
                {
                    // 거래구분이 국내아니면 자동의뢰 및 입고 불가능
                    if (ds_Ty1.Tables[2].Rows[0]["YN_REQ"].ToString().Trim() == "N" &&
                        ds_Ty1.Tables[2].Rows[0]["FG_TRANS"].ToString().Trim() != "001")
                    {
                        this.ShowMessage("PU_M000121");

                        //MessageBoxEx.Show("거래구분이 국내일 때만 자동의뢰및 입고행위가 가능합니다. \n 발주 형태를 변경해 주십시오.",this.PageName);
                        return false;
                    }

                    // 자동입고일 경우 보관장소 필수 입력
                    if (ds_Ty1.Tables[2].Rows[0]["YN_AUTORCV"].ToString().Trim() == "Y" &&
                        ds_Ty1.Tables[2].Rows[0]["YN_REQ"].ToString().Trim() == "N")
                    {
                        for (int j = 0; j < _flex.DataView.Count; j++)
                        {
                            if (_flex.DataView[j].Row.RowState.ToString() != "Deleted")
                            {
                                if (_flex.DataView[j]["CD_SL"].ToString().Trim() == "")
                                {
                                    this.ShowMessage("WK1_002", this.GetDataDictionaryItem("PU", "NM_SL"));
                                    _flex.Select(j + 1, "CD_SL");
                                    return false;
                                }
                            }
                        }
                    }



                    // 발주상태를 라인에 추가
                    for (int i = 0; i < _flex.DataView.Count; i++)
                    {
                        if (_flex.DataView[i].Row.RowState.ToString() == "Added")
                        {
                            _flex.DataView[i].BeginEdit();
                            _flex.DataView[i]["CD_COMPANY"] = this.MainFrameInterface.LoginInfo.CompanyCode;
                            _flex.DataView[i]["FG_TAX"] = cbo_FG_TAX.SelectedValue.ToString();
                            _flex.DataView[i]["FG_TRANS"] = ds_Ty1.Tables[2].Rows[0]["FG_TRANS"].ToString();
                            _flex.DataView[i]["FG_RCV"] = ds_Ty1.Tables[2].Rows[0]["FG_TPRCV"].ToString();
                            _flex.DataView[i]["FG_PURCHASE"] = ds_Ty1.Tables[2].Rows[0]["FG_TPPURCHASE"].ToString();
                            _flex.DataView[i]["YN_AUTORCV"] = ds_Ty1.Tables[2].Rows[0]["YN_AUTORCV"].ToString();
                            _flex.DataView[i]["YN_RCV"] = ds_Ty1.Tables[2].Rows[0]["YN_RCV"].ToString();
                            _flex.DataView[i]["YN_RETURN"] = ds_Ty1.Tables[2].Rows[0]["YN_RETURN"].ToString();
                            _flex.DataView[i]["YN_SUBCON"] = ds_Ty1.Tables[2].Rows[0]["YN_SUBCON"].ToString();
                            _flex.DataView[i]["YN_IMPORT"] = ds_Ty1.Tables[2].Rows[0]["YN_IMPORT"].ToString();
                            _flex.DataView[i]["YN_ORDER"] = ds_Ty1.Tables[2].Rows[0]["YN_ORDER"].ToString();
                            _flex.DataView[i]["YN_REQ"] = ds_Ty1.Tables[2].Rows[0]["YN_REQ"].ToString();

                            // 추가 10.05 YN_PURCHASE // FG_TPPURCHASE
                            //	_flex.DataView[i]["YN_PURCHASE"] = ds_TPPO.Tables[0].Rows[0]["YN_PURCHASE"].ToString();
                            _flex.DataView[i]["FG_TPPURCHASE"] = ds_Ty1.Tables[2].Rows[0]["FG_TPPURCHASE"].ToString();

                            _flex.DataView[i].EndEdit();
                        }
                    }
                }
                else
                {
                    return false;
                }
                //
                //				if( ds_TPPO == null)
                //				{ 
                //					return false;
                //				}
                //				else if(ds_TPPO.Tables.Count <=0)
                //				{
                //					return false;
                //				}
                //				else if( gs_InitTppo != tb_FG_PO_TR.Tag.ToString() || gs_InitFGTax != cbo_FG_TAX.SelectedValue.ToString() )
                //				{
                //					if(ds_TPPO.Tables[0].Rows.Count >0)
                //					{
                //						for(int i = 0 ; i < ddv.Count ; i++)
                //						{
                //							_flex.DataView[i].BeginEdit();
                //							_flex.DataView[i]["CD_COMPANY"] = this.MainFrameInterface.LoginInfo.CompanyCode;
                //							_flex.DataView[i]["FG_TAX"] = cbo_FG_TAX.SelectedValue.ToString();
                //							_flex.DataView[i]["FG_TRANS"] = ds_TPPO.Tables[0].Rows[0]["FG_TRANS"].ToString();
                //							_flex.DataView[i]["FG_RCV"] = ds_TPPO.Tables[0].Rows[0]["FG_TPRCV"].ToString();
                //							_flex.DataView[i]["FG_PURCHASE"] = ds_TPPO.Tables[0].Rows[0]["FG_TPPURCHASE"].ToString();
                //							_flex.DataView[i]["YN_AUTORCV"] = ds_TPPO.Tables[0].Rows[0]["YN_AUTORCV"].ToString();
                //							_flex.DataView[i]["YN_RCV"] = ds_TPPO.Tables[0].Rows[0]["YN_RCV"].ToString();
                //							_flex.DataView[i]["YN_RETURN"] = ds_TPPO.Tables[0].Rows[0]["YN_RETURN"].ToString();
                //							_flex.DataView[i]["YN_SUBCON"] = ds_TPPO.Tables[0].Rows[0]["YN_SUBCON"].ToString();
                //							_flex.DataView[i]["YN_IMPORT"] = ds_TPPO.Tables[0].Rows[0]["YN_IMPORT"].ToString();
                //							_flex.DataView[i]["YN_ORDER"] = ds_TPPO.Tables[0].Rows[0]["YN_ORDER"].ToString();
                //
                //							// 추가 10.05 YN_PURCHASE // FG_TPPURCHASE
                //						//	_flex.DataView[i]["YN_PURCHASE"] = ds_TPPO.Tables[0].Rows[0]["YN_PURCHASE"].ToString();
                //							_flex.DataView[i]["FG_TPPURCHASE"] = ds_TPPO.Tables[0].Rows[0]["FG_TPPURCHASE"].ToString();
                //							_flex.DataView[i].EndEdit();
                //						}		
                //				
                //						gs_InitTppo = tb_FG_PO_TR.Tag.ToString() ;
                //						gs_InitFGTax = cbo_FG_TAX.SelectedValue.ToString();
                //					}
                //					else 
                //					{
                //						return false;
                //					}
                //				}

                return true;

            }
            catch
            {
            }
            return true;
        }




        #endregion

        #region -> 조회 호출 함수

        /// <summary>
        /// 발주등록 보조 화면에서 선택된 값이 확인 혹은 복사에 따른 값 설정
        /// </summary>
        /// <param name="ps_nopo">선택된 발주번호</param>
        /// <param name="ps_state">확인 혹은 복사 </param>
        private void Fill_REGPO(string ps_nopo, string ps_state)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                DataTable ldt_head = new DataTable();
                DataTable ldt_line = new DataTable();

                object[] m_obj = new object[2];
                m_obj[0] = this.MainFrameInterface.LoginInfo.CompanyCode;
                m_obj[1] = ps_nopo;

                Duzon.Common.Util.SpInfo si = new Duzon.Common.Util.SpInfo();
                ResultData result = (ResultData)this.FillDataSet("SP_PU_PO_SELECT", m_obj);
                DataSet ds = (DataSet)result.DataValue;


                //	DataSet ds = (DataSet)(this.MainFrameInterface.InvokeRemoteMethod("PurOrderControl_NTX", "pur.CC_PU_PO_NTX","CC_PU_PO_NTX.rem", "SelectPol", m_obj));

                if (ds != null && ds.Tables.Count > 1)
                {
                    // 헤더정보
                    ldt_head = ds.Tables[0];
                    // 라인정보
                    ldt_line = ds.Tables[1];

                    if (ldt_head != null && ldt_head.Rows.Count > 0)
                    {
                        DataRow row;
                        row = ldt_head.Rows[0];

                        tb_DT_PO.Text = row["DT_PO"].ToString();
                        tb_NM_PARTNER.CodeName = row["LN_PARTNER"].ToString();
                        tb_NM_PARTNER.CodeValue = row["CD_PARTNER"].ToString();
                        tb_NM_PURGRP.CodeName = row["NM_PURGRP"].ToString();
                        tb_NM_PURGRP.CodeValue = row["CD_PURGRP"].ToString();

                        //		tb_NM_PLANT.Text = row["NM_PLANT"].ToString();
                        cbo_CD_PLANT.SelectedValue = row["CD_PLANT"].ToString();

                        tb_NO_EMP.CodeName = row["NM_KOR"].ToString();
                        tb_NO_EMP.CodeValue = row["NO_EMP"].ToString();
                        tb_FG_PO_TR.CodeName = row["NM_TPPO"].ToString();
                        tb_FG_PO_TR.CodeValue = row["CD_TPPO"].ToString();
                        tb_NM_TRANS.Text = row["NM_TRANS"].ToString();

                        tb_DC.Text = row["DC50_PO"].ToString();

                        _dtsUpdate = row["DTS_UPDATE"].ToString();


                        if (tb_FG_PO_TR.CodeValue.ToString() != "")
                        {
                            ds_Ty1.Tables[2].Clear();

                            DataRow newRow = ds_Ty1.Tables[2].NewRow();
                            newRow["FG_TRANS"] = row["FG_TRANS"].ToString();
                            newRow["FG_TPRCV"] = row["FG_TPRCV"].ToString();
                            newRow["FG_TPPURCHASE"] = row["FG_TPPURCHASE"].ToString();
                            newRow["YN_AUTORCV"] = row["YN_AUTORCV"].ToString();
                            newRow["YN_RCV"] = row["YN_RCV"].ToString();
                            newRow["YN_RETURN"] = row["YN_RETURN"].ToString();
                            newRow["YN_SUBCON"] = row["YN_SUBCON"].ToString();
                            newRow["YN_IMPORT"] = row["YN_IMPORT"].ToString();
                            newRow["YN_ORDER"] = row["YN_ORDER"].ToString();
                            newRow["YN_REQ"] = row["YN_REQ"].ToString();
                            newRow["NM_TRANS"] = row["NM_TRANS"].ToString();
                            newRow["YN_AM"] = row["YN_AM"].ToString();

                            ds_Ty1.Tables[2].Rows.Add(newRow);



                            //	gs_InitTppo = tb_FG_PO_TR.Tag.ToString();
                        }
                        //		tb_FG_PAYMENT.Text = row["FG_PAYMENT"].ToString();

                        tb_CD_PJT.CodeName = row["CD_PJT"].ToString();
                        tb_CD_PJT.CodeValue = row["CD_PJT"].ToString();
                        tb_NM_EXCH.Text = row["RT_EXCH"].ToString();

                        //						tb_AM.Text = row["AM_EX"].ToString();
                        //						tb_AM_K.Text = row["AM"].ToString();
                        //						tb_VAT.Text = row["VAT"].ToString();
                        //						tb_DC.Text = row["DC50_PO"].ToString();


                        cbo_FG_UM.SelectedValue = row["FG_UM"].ToString();
                        cbo_FG_TAX.SelectedValue = row["FG_TAX"].ToString();
                        SettingTbTax(row["FG_TAX"].ToString());

                        cbo_TP_TAX.SelectedValue = row["TP_UM_TAX"].ToString();
                        cbo_NM_EXCH.SelectedValue = row["CD_EXCH"].ToString();
                        cbo_PAYment.SelectedValue = row["FG_PAYMENT"].ToString();


                        // 계산서 처리구분
                        if (row["FG_TAXP"].ToString() == "001")
                        {
                            rbtn_All.Checked = true;
                            rbtn_PRI.Checked = false;
                        }
                        else
                        {
                            rbtn_All.Checked = false;
                            rbtn_PRI.Checked = true;
                        }

                        if (row["CD_EXCH"].ToString() == "000")
                        {
                            tb_NM_EXCH.Enabled = false;
                        }
                        else
                        {
                            tb_NM_EXCH.Enabled = true;
                        }

                        // 서버창이 확인 버턴이 눌러졌으면
                        if (ps_state == "OK")
                        {
                            tb_NO_PO.Text = row["NO_PO"].ToString();

                            // TP_PROCESS
                            _tp_process = row["TP_PROCESS"].ToString();

                            // 요청적용에서 가져온 정보이면
                            if (_tp_process == "1")
                            {
                                btn_RE_PR.Enabled = true;
                                btn_RE_APP.Enabled = false;
                                btn_insert.Enabled = false;
                            }
                            // 요청적용에서 가져온 정보이면
                            if (_tp_process == "3")
                            {
                                btn_RE_PR.Enabled = false;
                                btn_RE_APP.Enabled = true;
                                btn_insert.Enabled = false;
                            }
                            else // 추가버턴에 입력된 정보이면
                            {
                                btn_RE_PR.Enabled = false;
                                btn_RE_APP.Enabled = false;
                                btn_insert.Enabled = true;
                            }


                            _flex.Redraw = false;
                            _flex.BindingStart();
                            _flex.DataSource = new DataView(ldt_line);
                            _flex.BindingEnd();
                            _flex.Redraw = true;

                            bool lb_enabled = true;

                            if (ldt_line != null)
                            {
                                if (ldt_line.Rows.Count > 0)
                                {
                                    for (int li = 0; li < ldt_line.Rows.Count; li++)
                                    {
                                        // 뒤의 프로세스가 진행되었으면
                                        if (ldt_line.Rows[li]["FG_POCON"].ToString() != "001")
                                        {
                                            lb_enabled = false;
                                        }
                                        //										if( _tp_process == "1")
                                        //										{
                                        //											if( ldt_line.Rows[li]["NO_PR"].ToString().Trim() != "")
                                        //											{	// 요청번호,항번 임시 저장
                                        //												DataRow NewRow = ds_Ty1.Tables[1].NewRow();
                                        //												NewRow["NO_PR"] = ldt_line.Rows[li]["NO_PR"].ToString();
                                        //												NewRow["NO_PRLINE"] = ldt_line.Rows[li]["NO_PRLINE"].ToString();
                                        //												ds_Ty1.Tables[1].Rows.Add(NewRow);
                                        //											}
                                        //										}
                                        //
                                        //										// 요청적용타입일 경우 요청번호 저장
                                        //										if( _tp_process == "3")
                                        //										{
                                        //											if(ldt_line.Rows[li]["NO_APP"].ToString().Trim() != "")
                                        //											{
                                        //												DataRow NewRow = ds_Ty1.Tables[5].NewRow();
                                        //												NewRow["NO_APP"] = ldt_line.Rows[li]["NO_APP"].ToString();
                                        //												NewRow["NO_APPLINE"] = ldt_line.Rows[li]["NO_APPLINE"].ToString();
                                        //												ds_Ty1.Tables[5].Rows.Add(NewRow);
                                        //											}
                                        //										}


                                    }
                                }
                            }

                            if (lb_enabled)
                            {	//변경가능한 모드이면
                                SetControlEnabled(false, 1);
                            }
                            else
                            {	// 변경 불가능모드
                                SetControlEnabled(false, 2);
                                _isChagePossible = false;
                            }

                            //							this.ToolBarSaveButtonEnabled = false;
                            //							btn_delete.Enabled = false;
                            //							btn_insert.Enabled = false;
                            //							btn_ITEM_EXP.Enabled = false;
                            //							//btn_RART_RECOM.Enabled = false;
                            //							btn_RE_PR.Enabled = false;	
                            //							btn_RE_APP.Enabled = false;
                            //							tb_NM_EXCH.Enabled = false;

                            //	MaxPoLine();
                            _page_state = "Modified";
                        }
                        else  //Copy 이면.. 복사 한것은 추가로 생각 한다.( 즉,구매요청 번호 없음 )
                        {
                            tb_NO_PO.Text = "";

                            _flex.DataTable.Clear();


                            // 추가로 설정한다..
                            _tp_process = "2";


                            _flex.Redraw = false;
                            for (int i = 0; i < ldt_line.Rows.Count; i++)
                            {
                                _flex.Rows.Add();
                                _flex.Row = _flex.Rows.Count - 1;

                                _flex[_flex.Row, "NO_LINE"] = GetMaxPoLine();
                                _flex[_flex.Row, "QT_REQ"] = 0;
                                _flex[_flex.Row, "QT_RCV"] = 0;

                                //QT_PO_MM

                                _flex[_flex.Row, "NO_PR"] = "";
                                _flex[_flex.Row, "NO_PRLINE"] = 0;
                                _flex[_flex.Row, "FG_POST"] = "O";				// 설계변경으로 ( P -> F 변경 )
                                _flex[_flex.Row, "FG_POCON"] = "001";
                                _flex[_flex.Row, "NM_SYSDEF"] = _ComfirmState;	// 
                                _flex[_flex.Row, "CD_SL"] = "";

                                // 계약
                                _flex[_flex.Row, "NO_CONTRACT"] = "";
                                _flex[_flex.Row, "NO_CTLINE"] = 0;

                                // 의뢰
                                _flex[_flex.Row, "NO_RCV"] = "";
                                _flex[_flex.Row, "NO_RCVLINE"] = 0;

                                _flex[_flex.Row, "NO_APP"] = "";
                                _flex[_flex.Row, "NO_APPLINE"] = 0;





                                _flex[_flex.Row, "NM_PLANT"] = ldt_line.Rows[i]["NM_PLANT"].ToString();
                                _flex[_flex.Row, "CD_PLANT"] = ldt_line.Rows[i]["CD_PLANT"].ToString();
                                _flex[_flex.Row, "CD_ITEM"] = ldt_line.Rows[i]["CD_ITEM"].ToString();
                                _flex[_flex.Row, "NM_ITEM"] = ldt_line.Rows[i]["NM_ITEM"].ToString();
                                _flex[_flex.Row, "UNIT_PO"] = ldt_line.Rows[i]["UNIT_PO"].ToString();
                                _flex[_flex.Row, "STND_ITEM"] = ldt_line.Rows[i]["STND_ITEM"].ToString();
                                _flex[_flex.Row, "UNIT_IM"] = ldt_line.Rows[i]["UNIT_IM"].ToString();
                                _flex[_flex.Row, "CD_UNIT_MM"] = ldt_line.Rows[i]["CD_UNIT_MM"].ToString();
                                _flex[_flex.Row, "QT_PO_MM"] = ldt_line.Rows[i]["QT_PO_MM"];
                                _flex[_flex.Row, "QT_PO"] = ldt_line.Rows[i]["QT_PO"];
                                _flex[_flex.Row, "DT_LIMIT"] = ldt_line.Rows[i]["DT_LIMIT"].ToString();
                                _flex[_flex.Row, "RT_PO"] = ldt_line.Rows[i]["RT_PO"];

                                _flex[_flex.Row, "UM"] = ldt_line.Rows[i]["UM"];
                                _flex[_flex.Row, "UM_P"] = ldt_line.Rows[i]["UM_P"];
                                _flex[_flex.Row, "UM_EX_PO"] = ldt_line.Rows[i]["UM_EX_PO"];
                                _flex[_flex.Row, "UM_EX"] = ldt_line.Rows[i]["UM_EX"];

                                _flex[_flex.Row, "AM"] = ldt_line.Rows[i]["AM"];
                                _flex[_flex.Row, "AM_EX"] = ldt_line.Rows[i]["AM_EX"];

                                _flex[_flex.Row, "VAT"] = ldt_line.Rows[i]["VAT"];


                                _flex.AddFinished();
                                _flex.Col = _flex.Cols.Fixed;
                            }

                            _page_state = "Added";
                            this.ToolBarSaveButtonEnabled = true;
                            btn_insert.Enabled = true;
                            btn_delete.Enabled = true;
                            //btn_RART_RECOM.Enabled = true;

                            btn_RE_PR.Enabled = false;
                            btn_RE_APP.Enabled = false;
                            SetVATType(row["FG_TRANS"].ToString().Trim());
                            SetControlEnabled(false, 1);
                        }
                    }
                    else
                    {
                    }
                }
                Cursor.Current = Cursors.Default;
            }
            catch (coDbException ex)
            {
                this.ShowErrorMessage(ex, this.PageName);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex, this.PageName);
            }
            finally
            {
                _flex.Redraw = true;
                Cursor.Current = Cursors.Default;
            }
        }

        #endregion

        #region -> 초기화와 페이지 컨트롤 관리 함수

        /// <summary>
        /// 초기화 시키는 함수 
        /// </summary>
        private void FieldDataNULL()
        {
            try
            {
                _page_state = "Added";

                _db_am = 0;
                _db_amk = 0;
                _db_vat = 0;
                tb_CD_PJT.CodeName = "";
                tb_CD_PJT.CodeValue = "";
                tb_DC.Text = "";
                tb_DT_PO.Text = "";
                //	tb_FG_PAYMENT.Text ="";
                tb_FG_PO_TR.CodeName = "";
                tb_FG_PO_TR.CodeValue = "";
                tb_NM_EXCH.Text = "1.0";
                tb_NM_PARTNER.CodeName = "";
                tb_NM_PARTNER.CodeValue = "";
                tb_NM_TRANS.Text = "";


                _isChagePossible = true;

                //				tb_NM_PLANT.Text = "";
                //				tb_NM_PLANT.Tag = "";

                tb_NO_PO.Text = "";
                //				tb_NO_PO.Enabled = true;

                _page_state = "Added";


                tb_NM_PURGRP.CodeName = "";
                tb_NM_PURGRP.CodeValue = "";
                tb_CD_DEPT.Tag = this.LoginInfo.DeptCode;
                tb_CD_DEPT.Text = this.LoginInfo.DeptName.ToString();
                tb_NO_EMP.CodeName = this.LoginInfo.EmployeeName;
                tb_NO_EMP.CodeValue = this.LoginInfo.EmployeeNo;
                tb_NO_PO.Text = "";
                tb_TAX.Text = "";

                tb_QT_ATP.Text = "0";
                tb_QT_INV.Text = "0";
                tb_QT_PO.Text = "0";
                tb_QT_REQ.Text = "0";


                SetControlEnabled(true, 2);

                this.ToolBarSaveButtonEnabled = true;
                btn_RE_PR.Enabled = true;
                btn_RE_APP.Enabled = true;
                btn_ITEM_EXP.Enabled = true;
                //btn_RART_RECOM.Enabled = false;
                btn_insert.Enabled = true;
                btn_delete.Enabled = true;

                rbtn_All.Checked = true;
                rbtn_PRI.Checked = false;

                tb_NM_EXCH.Enabled = false;

                try
                {
                    tb_TAX.Text = _dsCombo.Tables[1].Rows[0]["VAT_RATE"].ToString();
                }
                catch
                {
                }

                ds_Ty1.Tables[1].Clear();
                ds_Ty1.Tables[5].Clear();

                //			gbool_cellchanged = false;

                cbo_FG_TAX.SelectedIndex = 0;
                //	cbo_FG_TAX.Modified = true;
                cbo_FG_UM.SelectedIndex = 0;

                cbo_NM_EXCH.SelectedIndex = 0;

                cbo_TP_TAX.SelectedIndex = 1;

                cbo_PAYment.SelectedIndex = 0;
                cbo_CD_PLANT.SelectedIndex = 0;


                //_flex.AllowEditing = true;
                //	dzdwGrid.CurrentCell.Deactivate(true); 

                tb_NO_PO.Focus();

                try
                {
                    _flex.DataTable.Clear();
                    ds_Ty1.Tables[0].Clear();


                }
                catch
                {
                }

                ds_Ty1.Tables[1].Clear();
                ds_Ty1.Tables[5].Clear();

                tb_DT_PO.Text = this.MainFrameInterface.GetStringToday;

            }
            catch
            {
            }
        }



        /// <summary>
        /// UI 상태 활성화 상태 관련 함수
        ///  
        /// </summary>
        /// <param name="isEnabled"></param>
        /// <param name="pi_type">1 : 확정이 되지 않은상태의 활성화 조절
        ///                       2 : 확정이 이루어진 상태의 활성화 조절</param>
        private void SetControlEnabled(bool isEnabled, int pi_type)
        {
            //FG_POCON == 002 이면
            if (pi_type == 2)
            {
                tb_DT_PO.Enabled = isEnabled;
                //	btn_DT_PO.Enabled = isEnabled;
                tb_NM_PARTNER.Enabled = isEnabled;

                tb_NM_PURGRP.Enabled = isEnabled;

                tb_FG_PO_TR.Enabled = isEnabled;


                cbo_FG_UM.Enabled = isEnabled;
                cbo_PAYment.Enabled = isEnabled;
                cbo_FG_TAX.Enabled = isEnabled;
                cbo_TP_TAX.Enabled = isEnabled;
                tb_CD_PJT.Enabled = isEnabled;

                cbo_CD_PLANT.Enabled = isEnabled;

                cbo_NM_EXCH.Enabled = isEnabled;
                //	tb_NM_EXCH.Enabled = isEnabled;

                rbtn_All.Enabled = isEnabled;
                rbtn_PRI.Enabled = isEnabled;

                //	tb_AM.Enabled = isEnabled;


                btn_insert.Enabled = isEnabled;
                btn_delete.Enabled = isEnabled;
                //	btn_RE_PR.Enabled = isEnabled;
                btn_ITEM_EXP.Enabled = isEnabled;
                btn_RE_PJT.Enabled = isEnabled;


            }
            else
            {

                tb_FG_PO_TR.Enabled = isEnabled;
                cbo_FG_UM.Enabled = isEnabled;
                cbo_PAYment.Enabled = isEnabled;
                cbo_FG_TAX.Enabled = isEnabled;
                cbo_TP_TAX.Enabled = isEnabled;
                cbo_CD_PLANT.Enabled = isEnabled;

                cbo_NM_EXCH.Enabled = isEnabled;
                //	tb_NM_EXCH.Enabled = isEnabled;


                tb_NM_PARTNER.Enabled = isEnabled;

            }
        }


        #endregion

        #region -> 통합 금액 계산( 금액,원화금액,부가세)

        /// <summary>
        /// 합계 금액 계산
        /// </summary>
        private void SUMFunction()
        {

            try
            {
                _db_am = (double)_flex.DataView.Table.Compute("SUM(AM_EX)", "");
                _db_amk = (double)_flex.DataView.Table.Compute("SUM(AM)", "");
                _db_vat = (double)_flex.DataView.Table.Compute("SUM(VAT)", "");

            }
            catch
            {
                _db_am = 0;
                _db_amk = 0;
                _db_vat = 0;
            }
        }


        #endregion

        #region -> 체크 함수들의 모임

        /// <summary>
        /// 발주등록 체크 함수
        /// </summary>
        /// <returns></returns>
        private bool FieldCheck_Head()
        {
            try
            {
                //  발주등록 부분의 필드 검사
                if (tb_NM_PARTNER.CodeValue.ToString() == "")
                {

                    tb_NM_PARTNER.Focus();
                    this.ShowMessage("WK1_004", lb_NM_PARTNER.Text);
                    //	Duzon.Common.Controls.MessageBoxEx.Show(lb_NM_PARTNER.Text +  this.GetMessageDictionaryItem("CM_M000002"),this.PageName);

                    return false;
                }
                if (tb_DT_PO.MaskEditBox.ClipText == "")
                {

                    tb_DT_PO.Focus();
                    this.ShowMessage("WK1_004", lb_DT_PO.Text);
                    return false;
                }
                if (cbo_CD_PLANT.SelectedValue.ToString() == "")
                {

                    cbo_CD_PLANT.Focus();
                    this.ShowMessage("WK1_004", lb_NM_PLANT.Text);
                    return false;
                }
                if (tb_NM_PURGRP.CodeValue.ToString() == "")
                {
                    tb_NM_PURGRP.Focus();
                    this.ShowMessage("WK1_004", lb_NM_PURGRP.Text);
                    return false;
                }
                if (tb_NO_EMP.CodeValue.ToString() == "")
                {

                    tb_NO_EMP.Focus();
                    this.ShowMessage("WK1_004", ib_NO_EMP.Text);
                    return false;
                }
                if (tb_FG_PO_TR.CodeValue.ToString() == "")
                {
                    tb_FG_PO_TR.Focus();
                    this.ShowMessage("WK1_004", lb_FG_PO_TR.Text);
                    return false;
                }


                if (cbo_NM_EXCH.Text == "" || cbo_NM_EXCH.Text == null)
                {
                    this.ShowMessage("WK1_004", lb_NM_EXCH.Text);
                    cbo_NM_EXCH.Focus();
                    return false;
                }
                if (tb_NM_EXCH.Text == "")
                {
                    this.ShowMessage("WK1_004", lb_NM_EXCH.Text);
                    tb_NM_EXCH.Focus();
                    return false;
                }
                if (cbo_TP_TAX.Text == "" || cbo_TP_TAX.Text == null)
                {
                    this.ShowMessage("WK1_004", lb_TP_TAX.Text);
                    cbo_TP_TAX.Focus();
                    return false;
                }
            }
            catch (coDbException ex)
            {
                this.ShowErrorMessage(ex, this.PageName);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex, this.PageName);
            }
            return true;

        }

        /// <summary>
        /// 삭제 가능한지 검사 
        /// </summary>
        /// <returns></returns>
        private bool CheckDeleteYN()
        {
            //DataView ddv = (DataView)dzdwGrid.DataSource;


            DataRow[] ldrs_arg = _flex.DataTable.Select(" FG_POCON <> '001'");
            if (ldrs_arg != null && ldrs_arg.Length > 0)
                return false;
            return true;
        }



        /// <summary>
        /// 발주내역등록의 데이타가 있는지 검사
        /// </summary>
        /// <returns></returns>
        private bool CheckSaveYN()
        {
            //DataView ddv = (DataView)dzdwGrid.DataSource;

            try
            {
                if (_flex.DataView.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch
            {
            }
            return false;
        }

        #endregion

        #region -> 헤더의 콤보박스 이벤트들( 과세구분, 부가세 여부,)

        /// <summary>
        /// 과세 구분
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbo_FG_TAX_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            try
            {

                //DataView _flex.DataView = (DataView)dzdwGrid.DataSource;
                double m_am = 1.0;
                double m_vatrate = 0.1;

                try
                {
                    SettingTbTax(cbo_FG_TAX.SelectedValue.ToString());
                }
                catch
                {

                }

                try
                {
                    m_am = System.Double.Parse(tb_NM_EXCH.ClipText.ToString());
                    m_vatrate = System.Double.Parse(tb_TAX.ClipText.ToString()) / 100;
                }
                catch
                {
                }

                for (int i = 0; i < _flex.DataView.Count; i++)
                {
                    try
                    {
                        double ll_qtpo = System.Double.Parse(_flex.DataView[i]["AM"].ToString());

                        _flex.DataView[i]["VAT"] = (long)System.Math.Floor(ll_qtpo * m_vatrate);

                    }
                    catch
                    {
                        _flex.DataView[i]["VAT"] = 0;
                    }
                }

            }
            catch
            {
            }
            SUMFunction();

        }

        #endregion

        #region -> 부가세 비율 설정 함수

        /// <summary>
        /// 부가세 비율 설정
        /// </summary>
        /// <param name="ps_taxp">부가세</param>
        private void SettingTbTax(string ps_taxp)
        {
            try
            {

                DataRow[] ldrs_arg = _dsCombo.Tables[1].Select(" CODE = '" + ps_taxp + "'");

                if (ldrs_arg != null && ldrs_arg.Length > 0)
                {
                    tb_TAX.Text = System.Double.Parse(ldrs_arg[0]["VAT_RATE"].ToString()).ToString();
                }

            }
            catch
            {
                tb_TAX.Text = "0";
            }
        }

        #endregion

        #region -> 발주 형태에 따른 값 설정

        private void SetVATType(string ps_trans)
        {
            if (ps_trans == "001")
            {
                cbo_NM_EXCH.SelectedValue = "000";
                cbo_NM_EXCH.Enabled = false;
                cbo_NM_EXCH_SelectionChangeCommitted(null, null);
            }
            else
            {
                cbo_NM_EXCH.Enabled = true;
            }
            if (ps_trans == "001")
            {
                cbo_FG_TAX.Enabled = true;
                cbo_FG_TAX.SelectedValue = "21";
                cbo_FG_TAX_SelectionChangeCommitted(null, null);
            }
            else if (ps_trans == "002" || ps_trans == "003")
            {
                cbo_FG_TAX.SelectedValue = "23";
                cbo_FG_TAX.Enabled = false;
                cbo_FG_TAX_SelectionChangeCommitted(null, null);
            }
            else
            {
                cbo_FG_TAX.SelectedValue = "";
                cbo_FG_TAX.Enabled = false;
                cbo_FG_TAX_SelectionChangeCommitted(null, null);
            }
        }




        #endregion

        #region  -> 하부 재고량 계산부분

        // DB 에서 재고량 가져오는 부분
        private void GetQTCurrent()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                //	System.Diagnostics.Debugger.Break();
                if (_flex.Row >= _flex.Rows.Fixed)
                {//string ps_cdcompany, string ps_cdplant, string ps_cditem)

                    //DataView ddv = (DataView)dzdwGrid.DataSource;
                    //					DataSet lds_QTINV = new DataSet();
                    //					object[] m_obj;
                    //					m_obj = new object[3];
                    //					m_obj[0] = this.MainFrameInterface.LoginInfo.CompanyCode;
                    //					m_obj[1] = cbo_CD_PLANT.SelectedValue.ToString();
                    //					m_obj[2] = _flex[_flex.Row,"CD_ITEM"].ToString();
                    //					lds_QTINV = (DataSet)(this.MainFrameInterface.InvokeRemoteMethod("PurOrderControl_NTX", "pur.CC_PU_PO_NTX","CC_PU_PO_NTX.rem", "SelectCurrentQT", m_obj));


                    object[] m_obj = new object[3];
                    m_obj[0] = _flex[_flex.Row, "CD_ITEM"].ToString();
                    m_obj[1] = cbo_CD_PLANT.SelectedValue.ToString();
                    m_obj[2] = this.MainFrameInterface.LoginInfo.CompanyCode;

                    //SP_PU_PO_ITEMINFO_SELECT
                    Duzon.Common.Util.SpInfo si = new Duzon.Common.Util.SpInfo();
                    ResultData result = (ResultData)this.FillDataSet("SP_PU_COMMON_SELECT_2", m_obj);
                    DataSet lds_QTINV = (DataSet)result.DataValue;

                    if (lds_QTINV != null && lds_QTINV.Tables != null)
                    {
                        if (lds_QTINV.Tables[0].Rows.Count > 0)
                        {
                            // 발주량
                            _flex[_flex.Row, "QT_POC"] = lds_QTINV.Tables[0].Rows[0]["QT_POC"];								// 의뢰량
                            _flex[_flex.Row, "QT_REQC"] = lds_QTINV.Tables[0].Rows[0]["QT_REQC"];

                        }
                        if (lds_QTINV.Tables[1].Rows.Count > 0)
                        {
                            // 현재고량
                            _flex[_flex.Row, "QT_INVC"] = lds_QTINV.Tables[1].Rows[0]["QT_INVC"];
                            // 가용재고량
                            _flex[_flex.Row, "QT_ATPC"] = lds_QTINV.Tables[1].Rows[0]["QT_ATPC"];
                        }

                    }
                    else
                    {
                        _flex[_flex.Row, "QT_POC"] = 0;								// 의뢰량
                        _flex[_flex.Row, "QT_REQC"] = 0;
                        _flex[_flex.Row, "QT_INVC"] = 0;
                        _flex[_flex.Row, "QT_ATPC"] = 0;

                    }

                }
                else
                {
                    tb_QT_PO.Text = "0";
                    tb_QT_REQ.Text = "0";
                    tb_QT_INV.Text = "0";
                    tb_QT_ATP.Text = "0";
                }
            }
            catch (coDbException ex)
            {
                //	this.ShowErrorMessage(ex, this.PageName);
            }
            catch (Exception ex)
            {
                //	this.ShowErrorMessage(ex, this.PageName);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

        }

        // 수량 보여 주는 부분
        private void SetQtValue(int li_index)
        {
            try
            {
                if (li_index < 0)
                    return;
                Cursor.Current = Cursors.WaitCursor;

                tb_QT_PO.Text = _flex.DataView[li_index]["QT_POC"].ToString();
                tb_QT_REQ.Text = _flex.DataView[li_index]["QT_REQC"].ToString();
                tb_QT_INV.Text = _flex.DataView[li_index]["QT_INVC"].ToString();
                tb_QT_ATP.Text = _flex.DataView[li_index]["QT_ATPC"].ToString();
            }
            catch
            {
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }


        #endregion

        private void btn_RE_PJT_Click(object sender, System.EventArgs e)
        {

        }

        #region -> BpControl Event

        private void OnBpCodeTextBox_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {

            if (e.DialogResult == DialogResult.OK)
            {
                System.Data.DataRow[] rows = e.HelpReturn.Rows;
                switch (e.HelpID)
                {
                    case Duzon.Common.Forms.Help.HelpID.P_PU_TPPO_SUB:      //발주유형
                        if (!SearchFGPOTR(e.CodeValue))
                        {
                            this.ShowMessage("IK1_008");
                            tb_FG_PO_TR.CodeName = "";
                            tb_FG_PO_TR.CodeValue = "";
                            tb_NM_TRANS.Text = "";
                            tb_FG_PO_TR.Focus();
                        }
                        strFG_RCV = rows[0]["FG_TPRCV"].ToString();
                        break;

                    case Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB:
                        tb_CD_DEPT.Tag = rows[0]["CD_DEPT"];
                        tb_CD_DEPT.Text = rows[0]["NM_DEPT"].ToString();
                        break;
                    default:
                        break;
                }
            }
        }
        //        try
        //{
        //    if (e.DialogResult == DialogResult.OK)
        //    {
        //        DataRow row = e.HelpReturn.Rows[0];
        //        tb_cd_dept.Text = row["NM_DEPT"].ToString();
        //        tb_cd_dept.Tag = row["CD_DEPT"];
        //    }
        //}
        //catch (Exception ex)
        //{
        //    this.MsgEnd(ex);
        //}
        private void OnBpCodeTextBox_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            switch (e.HelpID)
            {
                case Duzon.Common.Forms.Help.HelpID.P_PU_TPPO_SUB:		// 발주유형 도움창
                    e.HelpParam.P61_CODE1 = "N";
                    break;


            }
        }

        #endregion

        private void OnBpCodeTextBox_CodeChanged(object sender, EventArgs e)
        {
            try
            {
                if (tb_NO_EMP.CodeValue == string.Empty)
                {
                    tb_CD_DEPT.Text = string.Empty;
                    tb_CD_DEPT.Tag = string.Empty;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }



        #endregion


    }
}

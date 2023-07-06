using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.Common.Controls;
using Duzon.Common.Forms.Help;

using C1.Win.C1FlexGrid;
using Dass.FlexGrid;

namespace prd
{
    /// <summary>
    /// P_PR_WO_REG_NEW에 대한 요약 설명입니다.
    /// 모듈명 : 생산관리
    /// 시스템명 : NEW 작업지시 관리
    /// 서브시스템명 : 작업지시등록
    /// 2003.11.25 안정훈 인수
    /// </summary>
    public class P_PR_WO_REG_NEW_OLD : Duzon.Common.Forms.PageBase
    {
        #region ♣ 변수 선언

        #region -> 멤버필드(일반)
        //페널
        private Duzon.Common.Controls.PanelExt panel16;
        private Duzon.Common.Controls.PanelExt panel17;
        private Duzon.Common.Controls.PanelExt panel18;
        private Duzon.Common.Controls.PanelExt panel19;
        private Duzon.Common.Controls.PanelExt panel20;
        private Duzon.Common.Controls.PanelExt panel21;
        private Duzon.Common.Controls.PanelExt panel22;
        private Duzon.Common.Controls.PanelExt panel23;
        private Duzon.Common.Controls.PanelExt panel24;
        private Duzon.Common.Controls.PanelExt panel9;
        private Duzon.Common.Controls.PanelExt panel10;
        private Duzon.Common.Controls.PanelExt panel12;
        private Duzon.Common.Controls.PanelExt panel14;
        private Duzon.Common.Controls.PanelExt panel25;
        private Duzon.Common.Controls.PanelExt panel26;
        private Duzon.Common.Controls.PanelExt panel4;
        private Duzon.Common.Controls.PanelExt panel5;
        private Duzon.Common.Controls.PanelExt panel7;
        private Duzon.Common.Controls.PanelExt panel8;
        private Duzon.Common.Controls.PanelExt panel27;
        private Duzon.Common.Controls.PanelExt panel28;
        private Duzon.Common.Controls.PanelExt m_pnlInfoRout;
        private Duzon.Common.Controls.PanelExt m_pnlReqMatl;
        private Duzon.Common.Controls.PanelExt m_pnlMain;

        //텝
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage m_tabRout;
        private System.Windows.Forms.TabPage m_tabMatl;

        //라벨
        private Duzon.Common.Controls.LabelExt label1;
        private Duzon.Common.Controls.LabelExt m_lblCdPlant;
        private Duzon.Common.Controls.LabelExt m_lblNoWo;
        private Duzon.Common.Controls.LabelExt m_lblFgWo;
        private Duzon.Common.Controls.LabelExt m_lblCdItem;
        private Duzon.Common.Controls.LabelExt m_lblQtWo;
        private Duzon.Common.Controls.LabelExt m_lblPeriodWork;
        private Duzon.Common.Controls.LabelExt m_lblQtWork;
        private Duzon.Common.Controls.LabelExt m_lblStWo;
        private Duzon.Common.Controls.LabelExt m_lblNoLot;
        private Duzon.Common.Controls.LabelExt m_lblNoTrack;
        private Duzon.Common.Controls.LabelExt m_lblTpWo;
        private Duzon.Common.Controls.LabelExt m_lblInfoRout;
        private Duzon.Common.Controls.LabelExt m_lblInfoBill;

        //버튼
        private Duzon.Common.Controls.ButtonExt m_btnTracking;

        //텍스트박스 
        private Duzon.Common.Controls.TextBoxExt m_txtNoWo;
        private Duzon.Common.Controls.TextBoxExt m_txtStndItem;
        private Duzon.Common.Controls.TextBoxExt m_txtUnitIm;
        private Duzon.Common.Controls.TextBoxExt m_txtNoLot;
        private Duzon.Common.Controls.TextBoxExt m_txtNoSo;
        private Duzon.Common.Controls.TextBoxExt m_txtNoLineSo;
        private Duzon.Common.BpControls.BpCodeNTextBox m_txtCdItem;

        //커런시
        private Duzon.Common.Controls.CurrencyTextBox m_ctxtQtWo;
        private Duzon.Common.Controls.CurrencyTextBox m_ctxtQtWork;

        //콤보
        private Duzon.Common.Controls.DropDownComboBox m_cboFgWo;
        private Duzon.Common.Controls.DropDownComboBox m_cboRout;
        private Duzon.Common.Controls.DropDownComboBox m_cboStWo;
        private Duzon.Common.Controls.DropDownComboBox m_cboCdPlant;
        private Duzon.Common.Controls.DropDownComboBox m_cboPath;

        //버튼
        private Duzon.Common.Controls.RoundedButton m_btnRespread_rout;
        private Duzon.Common.Controls.RoundedButton m_btnRespread_matl;
        private Duzon.Common.Controls.RoundedButton m_btnInsert_matl;
        private Duzon.Common.Controls.RoundedButton m_btnDelete_matl;
        private Duzon.Common.Controls.RoundedButton m_btnInsert_rout;
        private Duzon.Common.Controls.RoundedButton m_btnDelete_rout;

        //이미지리스트
        private System.Windows.Forms.ImageList imageList1;

        //날짜
        private Duzon.Common.Controls.DatePicker m_dtFrom;
        private Duzon.Common.Controls.DatePicker m_dtTo;

        #endregion

        #region -> 멤버필드(주요)
        private System.ComponentModel.IContainer components;

        private DataTable _dtOppath = null;			// 공정경로 세부내역
        private DataTable _dtYnWork = new DataTable();		// M/S여부
        private bool _isPainted = false;
        private string _stFgBf = "";				//B/F여부 판단
        private string _stFG_GIR = "";              // 불출여부

        // 그리드 선언
        private Dass.FlexGrid.FlexGrid _flex1;	// 경로정보 그리드 선언
        private Dass.FlexGrid.FlexGrid _flex2;	// 소요자재 그리드 선언

        #endregion

        #region -> 테이블
        private System.Data.DataSet _dsRoutBill;
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
        private System.Data.DataTable dataTable2;
        private System.Data.DataColumn dataColumn14;
        private System.Data.DataColumn dataColumn15;
        private System.Data.DataColumn dataColumn16;
        private System.Data.DataColumn dataColumn17;
        private System.Data.DataColumn dataColumn18;
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
        private System.Data.DataTable dataTable3;
        private System.Data.DataColumn dataColumn32;
        private System.Data.DataColumn dataColumn33;
        private System.Data.DataColumn dataColumn34;
        private System.Data.DataColumn dataColumn35;
        private System.Data.DataColumn dataColumn36;
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
        private System.Data.DataColumn dataColumn19;
        private System.Data.DataColumn dataColumn66;
        private System.Data.DataColumn dataColumn67;
        private System.Data.DataColumn dataColumn68;
        private System.Data.DataColumn dataColumn69;
        private System.Data.DataColumn dataColumn70;
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
        private System.Data.DataColumn dataColumn96;
        private System.Data.DataColumn dataColumn97;
        private System.Data.DataColumn dataColumn98;
        private System.Data.DataColumn dataColumn99;
        private System.Data.DataColumn dataColumn103;
        private System.Data.DataColumn dataColumn104;
        private System.Data.DataColumn dataColumn105;
        private System.Data.DataColumn dataColumn106;
        private System.Data.DataColumn dataColumn107;
        private System.Data.DataColumn dataColumn108;
        private System.Data.DataColumn dataColumn109;
        private System.Data.DataColumn dataColumn110;
        private System.Data.DataColumn dataColumn111;
        private System.Data.DataColumn dataColumn112;
        private System.Data.DataColumn dataColumn113;
        private System.Data.DataColumn dataColumn114;
        private System.Data.DataColumn dataColumn115;
        private System.Data.DataColumn dataColumn116;
        private System.Data.DataColumn dataColumn117;
        private System.Data.DataColumn dataColumn118;
        private System.Data.DataColumn dataColumn119;
        private System.Data.DataColumn dataColumn120;
        private System.Data.DataColumn dataColumn121;
        private System.Data.DataColumn dataColumn122;
        private System.Data.DataColumn dataColumn123;
        private System.Data.DataColumn dataColumn124;
        private System.Data.DataColumn dataColumn125;
        private System.Data.DataColumn dataColumn126;
        private System.Data.DataColumn dataColumn127;
        private System.Data.DataColumn dataColumn128;
        private System.Data.DataColumn dataColumn129;
        private System.Data.DataColumn dataColumn130;
        private System.Data.DataColumn dataColumn131;
        private System.Data.DataColumn dataColumn132;
        private System.Data.DataColumn dataColumn134;
        private System.Data.DataColumn dataColumn135;
        private System.Data.DataColumn dataColumn47;
        private System.Data.DataColumn dataColumn137;
        private TableLayoutPanel tableLayoutPanel1;
        private DataColumn dataColumn48;
        private DataColumn dataColumn49;
        private DataColumn dataColumn50;
        private DataColumn dataColumn51;
        private LabelExt labelExt1;
        private System.Data.DataColumn dataColumn136;
        #endregion

        #endregion

        #region ♣ 생성자/소멸자

        #region -> 생성자
        /// <summary>
        /// 생성자
        /// </summary>
        public P_PR_WO_REG_NEW_OLD()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(OnPageLoad);
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
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Windows Form Designer generated code
        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_PR_WO_REG_NEW_OLD));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.m_tabRout = new System.Windows.Forms.TabPage();
            this.panel9 = new Duzon.Common.Controls.PanelExt();
            this.m_btnRespread_rout = new Duzon.Common.Controls.RoundedButton(this.components);
            this.m_btnInsert_rout = new Duzon.Common.Controls.RoundedButton(this.components);
            this.m_btnDelete_rout = new Duzon.Common.Controls.RoundedButton(this.components);
            this.panel10 = new Duzon.Common.Controls.PanelExt();
            this.m_lblInfoRout = new Duzon.Common.Controls.LabelExt();
            this.m_pnlInfoRout = new Duzon.Common.Controls.PanelExt();
            this._flex1 = new Dass.FlexGrid.FlexGrid(this.components);
            this.panel12 = new Duzon.Common.Controls.PanelExt();
            this.panel14 = new Duzon.Common.Controls.PanelExt();
            this.panel25 = new Duzon.Common.Controls.PanelExt();
            this.panel26 = new Duzon.Common.Controls.PanelExt();
            this.m_tabMatl = new System.Windows.Forms.TabPage();
            this.panel4 = new Duzon.Common.Controls.PanelExt();
            this.m_btnRespread_matl = new Duzon.Common.Controls.RoundedButton(this.components);
            this.m_btnInsert_matl = new Duzon.Common.Controls.RoundedButton(this.components);
            this.m_btnDelete_matl = new Duzon.Common.Controls.RoundedButton(this.components);
            this.panel5 = new Duzon.Common.Controls.PanelExt();
            this.m_lblInfoBill = new Duzon.Common.Controls.LabelExt();
            this.m_pnlReqMatl = new Duzon.Common.Controls.PanelExt();
            this._flex2 = new Dass.FlexGrid.FlexGrid(this.components);
            this.panel7 = new Duzon.Common.Controls.PanelExt();
            this.panel8 = new Duzon.Common.Controls.PanelExt();
            this.panel27 = new Duzon.Common.Controls.PanelExt();
            this.panel28 = new Duzon.Common.Controls.PanelExt();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.m_pnlMain = new Duzon.Common.Controls.PanelExt();
            this.m_txtCdItem = new Duzon.Common.BpControls.BpCodeNTextBox();
            this.m_dtTo = new Duzon.Common.Controls.DatePicker();
            this.m_dtFrom = new Duzon.Common.Controls.DatePicker();
            this.m_txtNoLineSo = new Duzon.Common.Controls.TextBoxExt();
            this.m_txtNoSo = new Duzon.Common.Controls.TextBoxExt();
            this.m_cboPath = new Duzon.Common.Controls.DropDownComboBox();
            this.m_cboCdPlant = new Duzon.Common.Controls.DropDownComboBox();
            this.m_cboStWo = new Duzon.Common.Controls.DropDownComboBox();
            this.m_txtNoWo = new Duzon.Common.Controls.TextBoxExt();
            this.m_cboFgWo = new Duzon.Common.Controls.DropDownComboBox();
            this.m_txtStndItem = new Duzon.Common.Controls.TextBoxExt();
            this.m_txtUnitIm = new Duzon.Common.Controls.TextBoxExt();
            this.m_ctxtQtWo = new Duzon.Common.Controls.CurrencyTextBox();
            this.m_ctxtQtWork = new Duzon.Common.Controls.CurrencyTextBox();
            this.label1 = new Duzon.Common.Controls.LabelExt();
            this.m_cboRout = new Duzon.Common.Controls.DropDownComboBox();
            this.m_txtNoLot = new Duzon.Common.Controls.TextBoxExt();
            this.m_btnTracking = new Duzon.Common.Controls.ButtonExt();
            this.panel16 = new Duzon.Common.Controls.PanelExt();
            this.panel17 = new Duzon.Common.Controls.PanelExt();
            this.panel18 = new Duzon.Common.Controls.PanelExt();
            this.panel19 = new Duzon.Common.Controls.PanelExt();
            this.panel20 = new Duzon.Common.Controls.PanelExt();
            this.panel21 = new Duzon.Common.Controls.PanelExt();
            this.panel22 = new Duzon.Common.Controls.PanelExt();
            this.m_lblTpWo = new Duzon.Common.Controls.LabelExt();
            this.m_lblQtWork = new Duzon.Common.Controls.LabelExt();
            this.m_lblNoLot = new Duzon.Common.Controls.LabelExt();
            this.panel23 = new Duzon.Common.Controls.PanelExt();
            this.m_lblFgWo = new Duzon.Common.Controls.LabelExt();
            this.panel24 = new Duzon.Common.Controls.PanelExt();
            this.m_lblCdPlant = new Duzon.Common.Controls.LabelExt();
            this.m_lblNoWo = new Duzon.Common.Controls.LabelExt();
            this.m_lblCdItem = new Duzon.Common.Controls.LabelExt();
            this.m_lblQtWo = new Duzon.Common.Controls.LabelExt();
            this.m_lblPeriodWork = new Duzon.Common.Controls.LabelExt();
            this.m_lblStWo = new Duzon.Common.Controls.LabelExt();
            this.m_lblNoTrack = new Duzon.Common.Controls.LabelExt();
            this._dsRoutBill = new System.Data.DataSet();
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
            this.dataColumn79 = new System.Data.DataColumn();
            this.dataColumn98 = new System.Data.DataColumn();
            this.dataColumn99 = new System.Data.DataColumn();
            this.dataColumn109 = new System.Data.DataColumn();
            this.dataColumn110 = new System.Data.DataColumn();
            this.dataColumn111 = new System.Data.DataColumn();
            this.dataColumn112 = new System.Data.DataColumn();
            this.dataColumn113 = new System.Data.DataColumn();
            this.dataColumn134 = new System.Data.DataColumn();
            this.dataColumn48 = new System.Data.DataColumn();
            this.dataColumn49 = new System.Data.DataColumn();
            this.dataTable2 = new System.Data.DataTable();
            this.dataColumn14 = new System.Data.DataColumn();
            this.dataColumn15 = new System.Data.DataColumn();
            this.dataColumn16 = new System.Data.DataColumn();
            this.dataColumn17 = new System.Data.DataColumn();
            this.dataColumn18 = new System.Data.DataColumn();
            this.dataColumn66 = new System.Data.DataColumn();
            this.dataColumn67 = new System.Data.DataColumn();
            this.dataColumn68 = new System.Data.DataColumn();
            this.dataColumn69 = new System.Data.DataColumn();
            this.dataColumn70 = new System.Data.DataColumn();
            this.dataColumn71 = new System.Data.DataColumn();
            this.dataColumn72 = new System.Data.DataColumn();
            this.dataColumn73 = new System.Data.DataColumn();
            this.dataColumn74 = new System.Data.DataColumn();
            this.dataColumn75 = new System.Data.DataColumn();
            this.dataColumn76 = new System.Data.DataColumn();
            this.dataColumn77 = new System.Data.DataColumn();
            this.dataColumn78 = new System.Data.DataColumn();
            this.dataColumn80 = new System.Data.DataColumn();
            this.dataColumn82 = new System.Data.DataColumn();
            this.dataColumn83 = new System.Data.DataColumn();
            this.dataColumn84 = new System.Data.DataColumn();
            this.dataColumn85 = new System.Data.DataColumn();
            this.dataColumn96 = new System.Data.DataColumn();
            this.dataColumn97 = new System.Data.DataColumn();
            this.dataColumn103 = new System.Data.DataColumn();
            this.dataColumn104 = new System.Data.DataColumn();
            this.dataColumn105 = new System.Data.DataColumn();
            this.dataColumn106 = new System.Data.DataColumn();
            this.dataColumn114 = new System.Data.DataColumn();
            this.dataColumn115 = new System.Data.DataColumn();
            this.dataColumn116 = new System.Data.DataColumn();
            this.dataColumn117 = new System.Data.DataColumn();
            this.dataColumn118 = new System.Data.DataColumn();
            this.dataColumn119 = new System.Data.DataColumn();
            this.dataColumn120 = new System.Data.DataColumn();
            this.dataColumn121 = new System.Data.DataColumn();
            this.dataColumn122 = new System.Data.DataColumn();
            this.dataColumn123 = new System.Data.DataColumn();
            this.dataColumn124 = new System.Data.DataColumn();
            this.dataColumn125 = new System.Data.DataColumn();
            this.dataColumn126 = new System.Data.DataColumn();
            this.dataColumn127 = new System.Data.DataColumn();
            this.dataColumn128 = new System.Data.DataColumn();
            this.dataColumn135 = new System.Data.DataColumn();
            this.dataColumn137 = new System.Data.DataColumn();
            this.dataTable3 = new System.Data.DataTable();
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
            this.dataColumn81 = new System.Data.DataColumn();
            this.dataColumn86 = new System.Data.DataColumn();
            this.dataColumn87 = new System.Data.DataColumn();
            this.dataColumn88 = new System.Data.DataColumn();
            this.dataColumn107 = new System.Data.DataColumn();
            this.dataColumn108 = new System.Data.DataColumn();
            this.dataColumn129 = new System.Data.DataColumn();
            this.dataColumn130 = new System.Data.DataColumn();
            this.dataColumn131 = new System.Data.DataColumn();
            this.dataColumn132 = new System.Data.DataColumn();
            this.dataColumn136 = new System.Data.DataColumn();
            this.dataColumn47 = new System.Data.DataColumn();
            this.dataColumn50 = new System.Data.DataColumn();
            this.dataColumn51 = new System.Data.DataColumn();
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelExt1 = new Duzon.Common.Controls.LabelExt();
            this.tabControl1.SuspendLayout();
            this.m_tabRout.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel10.SuspendLayout();
            this.m_pnlInfoRout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex1)).BeginInit();
            this.m_tabMatl.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.m_pnlReqMatl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex2)).BeginInit();
            this.m_pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_ctxtQtWo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_ctxtQtWork)).BeginInit();
            this.panel22.SuspendLayout();
            this.panel23.SuspendLayout();
            this.panel24.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dsRoutBill)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable3)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.m_tabRout);
            this.tabControl1.Controls.Add(this.m_tabMatl);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.ItemSize = new System.Drawing.Size(150, 20);
            this.tabControl1.Location = new System.Drawing.Point(3, 186);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(787, 372);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 0;
            this.tabControl1.TabStop = false;
            // 
            // m_tabRout
            // 
            this.m_tabRout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.m_tabRout.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_tabRout.Controls.Add(this.panel9);
            this.m_tabRout.ImageIndex = 0;
            this.m_tabRout.Location = new System.Drawing.Point(4, 24);
            this.m_tabRout.Name = "m_tabRout";
            this.m_tabRout.Size = new System.Drawing.Size(779, 344);
            this.m_tabRout.TabIndex = 0;
            this.m_tabRout.Tag = "INFO_ROUT";
            this.m_tabRout.Text = "경로정보";
            // 
            // panel9
            // 
            this.panel9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel9.BackColor = System.Drawing.Color.White;
            this.panel9.Controls.Add(this.m_btnRespread_rout);
            this.panel9.Controls.Add(this.m_btnInsert_rout);
            this.panel9.Controls.Add(this.m_btnDelete_rout);
            this.panel9.Controls.Add(this.panel10);
            this.panel9.Controls.Add(this.m_pnlInfoRout);
            this.panel9.Controls.Add(this.panel12);
            this.panel9.Controls.Add(this.panel14);
            this.panel9.Controls.Add(this.panel25);
            this.panel9.Controls.Add(this.panel26);
            this.panel9.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panel9.Location = new System.Drawing.Point(7, 7);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(762, 328);
            this.panel9.TabIndex = 73;
            // 
            // m_btnRespread_rout
            // 
            this.m_btnRespread_rout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnRespread_rout.BackColor = System.Drawing.Color.White;
            this.m_btnRespread_rout.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.m_btnRespread_rout.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.m_btnRespread_rout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnRespread_rout.Location = new System.Drawing.Point(521, 6);
            this.m_btnRespread_rout.Name = "m_btnRespread_rout";
            this.m_btnRespread_rout.Size = new System.Drawing.Size(110, 24);
            this.m_btnRespread_rout.TabIndex = 0;
            this.m_btnRespread_rout.TabStop = false;
            this.m_btnRespread_rout.Tag = "RESPREAD_ROUT";
            this.m_btnRespread_rout.Text = "경로 재전개";
            this.m_btnRespread_rout.UseVisualStyleBackColor = false;
            this.m_btnRespread_rout.Click += new System.EventHandler(this.m_btn_respread_rout_Click);
            // 
            // m_btnInsert_rout
            // 
            this.m_btnInsert_rout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnInsert_rout.BackColor = System.Drawing.Color.White;
            this.m_btnInsert_rout.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.m_btnInsert_rout.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.m_btnInsert_rout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnInsert_rout.Location = new System.Drawing.Point(633, 6);
            this.m_btnInsert_rout.Name = "m_btnInsert_rout";
            this.m_btnInsert_rout.Size = new System.Drawing.Size(60, 24);
            this.m_btnInsert_rout.TabIndex = 0;
            this.m_btnInsert_rout.TabStop = false;
            this.m_btnInsert_rout.Tag = "INS";
            this.m_btnInsert_rout.Text = "추가";
            this.m_btnInsert_rout.UseVisualStyleBackColor = false;
            this.m_btnInsert_rout.Click += new System.EventHandler(this.insRoutButtonClick);
            // 
            // m_btnDelete_rout
            // 
            this.m_btnDelete_rout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnDelete_rout.BackColor = System.Drawing.Color.White;
            this.m_btnDelete_rout.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.m_btnDelete_rout.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.m_btnDelete_rout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnDelete_rout.Location = new System.Drawing.Point(695, 6);
            this.m_btnDelete_rout.Name = "m_btnDelete_rout";
            this.m_btnDelete_rout.Size = new System.Drawing.Size(60, 24);
            this.m_btnDelete_rout.TabIndex = 0;
            this.m_btnDelete_rout.TabStop = false;
            this.m_btnDelete_rout.Tag = "DEL";
            this.m_btnDelete_rout.Text = "삭제";
            this.m_btnDelete_rout.UseVisualStyleBackColor = false;
            this.m_btnDelete_rout.Click += new System.EventHandler(this.m_btn_delete_Rout_Click);
            // 
            // panel10
            // 
            this.panel10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel10.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel10.BackgroundImage")));
            this.panel10.Controls.Add(this.m_lblInfoRout);
            this.panel10.Location = new System.Drawing.Point(7, 7);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(511, 23);
            this.panel10.TabIndex = 78;
            // 
            // m_lblInfoRout
            // 
            this.m_lblInfoRout.BackColor = System.Drawing.Color.Transparent;
            this.m_lblInfoRout.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.m_lblInfoRout.Location = new System.Drawing.Point(20, 6);
            this.m_lblInfoRout.Name = "m_lblInfoRout";
            this.m_lblInfoRout.Resizeble = true;
            this.m_lblInfoRout.Size = new System.Drawing.Size(100, 16);
            this.m_lblInfoRout.TabIndex = 0;
            this.m_lblInfoRout.Tag = "INFO_ROUT";
            this.m_lblInfoRout.Text = "경로정보";
            this.m_lblInfoRout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_pnlInfoRout
            // 
            this.m_pnlInfoRout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_pnlInfoRout.Controls.Add(this._flex1);
            this.m_pnlInfoRout.Location = new System.Drawing.Point(7, 33);
            this.m_pnlInfoRout.Name = "m_pnlInfoRout";
            this.m_pnlInfoRout.Size = new System.Drawing.Size(748, 288);
            this.m_pnlInfoRout.TabIndex = 77;
            // 
            // _flex1
            // 
            this._flex1.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex1.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex1.AutoResize = false;
            this._flex1.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex1.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex1.EnabledHeaderCheck = true;
            this._flex1.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex1.Location = new System.Drawing.Point(0, 0);
            this._flex1.Name = "_flex1";
            this._flex1.Rows.Count = 1;
            this._flex1.Rows.DefaultSize = 18;
            this._flex1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex1.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex1.ShowSort = false;
            this._flex1.Size = new System.Drawing.Size(748, 288);
            this._flex1.StyleInfo = resources.GetString("_flex1.StyleInfo");
            this._flex1.TabIndex = 4;
            // 
            // panel12
            // 
            this.panel12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel12.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel12.BackgroundImage")));
            this.panel12.Location = new System.Drawing.Point(744, 310);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(18, 18);
            this.panel12.TabIndex = 76;
            // 
            // panel14
            // 
            this.panel14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel14.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel14.BackgroundImage")));
            this.panel14.Location = new System.Drawing.Point(0, 310);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(18, 18);
            this.panel14.TabIndex = 75;
            // 
            // panel25
            // 
            this.panel25.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel25.BackgroundImage")));
            this.panel25.Location = new System.Drawing.Point(0, 0);
            this.panel25.Name = "panel25";
            this.panel25.Size = new System.Drawing.Size(18, 18);
            this.panel25.TabIndex = 73;
            // 
            // panel26
            // 
            this.panel26.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel26.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel26.BackgroundImage")));
            this.panel26.Location = new System.Drawing.Point(744, 0);
            this.panel26.Name = "panel26";
            this.panel26.Size = new System.Drawing.Size(18, 18);
            this.panel26.TabIndex = 74;
            // 
            // m_tabMatl
            // 
            this.m_tabMatl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.m_tabMatl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_tabMatl.Controls.Add(this.panel4);
            this.m_tabMatl.ImageIndex = 1;
            this.m_tabMatl.Location = new System.Drawing.Point(4, 24);
            this.m_tabMatl.Name = "m_tabMatl";
            this.m_tabMatl.Size = new System.Drawing.Size(779, 344);
            this.m_tabMatl.TabIndex = 1;
            this.m_tabMatl.Tag = "INFO_BILL";
            this.m_tabMatl.Text = "소요자재";
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.m_btnRespread_matl);
            this.panel4.Controls.Add(this.m_btnInsert_matl);
            this.panel4.Controls.Add(this.m_btnDelete_matl);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.m_pnlReqMatl);
            this.panel4.Controls.Add(this.panel7);
            this.panel4.Controls.Add(this.panel8);
            this.panel4.Controls.Add(this.panel27);
            this.panel4.Controls.Add(this.panel28);
            this.panel4.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panel4.Location = new System.Drawing.Point(7, 7);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(762, 328);
            this.panel4.TabIndex = 74;
            // 
            // m_btnRespread_matl
            // 
            this.m_btnRespread_matl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnRespread_matl.BackColor = System.Drawing.Color.White;
            this.m_btnRespread_matl.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.m_btnRespread_matl.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.m_btnRespread_matl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnRespread_matl.Location = new System.Drawing.Point(521, 6);
            this.m_btnRespread_matl.Name = "m_btnRespread_matl";
            this.m_btnRespread_matl.Size = new System.Drawing.Size(110, 24);
            this.m_btnRespread_matl.TabIndex = 0;
            this.m_btnRespread_matl.TabStop = false;
            this.m_btnRespread_matl.Tag = "RESPREAD_MATL";
            this.m_btnRespread_matl.Text = "소요 재전개";
            this.m_btnRespread_matl.UseVisualStyleBackColor = false;
            this.m_btnRespread_matl.Click += new System.EventHandler(this.m_btn_respread_matl_Click);
            // 
            // m_btnInsert_matl
            // 
            this.m_btnInsert_matl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnInsert_matl.BackColor = System.Drawing.Color.White;
            this.m_btnInsert_matl.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.m_btnInsert_matl.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.m_btnInsert_matl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnInsert_matl.Location = new System.Drawing.Point(633, 6);
            this.m_btnInsert_matl.Name = "m_btnInsert_matl";
            this.m_btnInsert_matl.Size = new System.Drawing.Size(60, 24);
            this.m_btnInsert_matl.TabIndex = 0;
            this.m_btnInsert_matl.TabStop = false;
            this.m_btnInsert_matl.Tag = "INS";
            this.m_btnInsert_matl.Text = "추가";
            this.m_btnInsert_matl.UseVisualStyleBackColor = false;
            this.m_btnInsert_matl.Click += new System.EventHandler(this.insMatlButtonClick);
            // 
            // m_btnDelete_matl
            // 
            this.m_btnDelete_matl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnDelete_matl.BackColor = System.Drawing.Color.White;
            this.m_btnDelete_matl.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.m_btnDelete_matl.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.m_btnDelete_matl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnDelete_matl.Location = new System.Drawing.Point(695, 6);
            this.m_btnDelete_matl.Name = "m_btnDelete_matl";
            this.m_btnDelete_matl.Size = new System.Drawing.Size(60, 24);
            this.m_btnDelete_matl.TabIndex = 0;
            this.m_btnDelete_matl.TabStop = false;
            this.m_btnDelete_matl.Tag = "DEL";
            this.m_btnDelete_matl.Text = "삭제";
            this.m_btnDelete_matl.UseVisualStyleBackColor = false;
            this.m_btnDelete_matl.Click += new System.EventHandler(this.m_btn_delete_matl_Click);
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel5.BackgroundImage")));
            this.panel5.Controls.Add(this.m_lblInfoBill);
            this.panel5.Location = new System.Drawing.Point(7, 7);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(511, 23);
            this.panel5.TabIndex = 78;
            // 
            // m_lblInfoBill
            // 
            this.m_lblInfoBill.BackColor = System.Drawing.Color.Transparent;
            this.m_lblInfoBill.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.m_lblInfoBill.Location = new System.Drawing.Point(20, 6);
            this.m_lblInfoBill.Name = "m_lblInfoBill";
            this.m_lblInfoBill.Resizeble = true;
            this.m_lblInfoBill.Size = new System.Drawing.Size(100, 16);
            this.m_lblInfoBill.TabIndex = 0;
            this.m_lblInfoBill.Tag = "INFO_BILL";
            this.m_lblInfoBill.Text = "소요자재";
            this.m_lblInfoBill.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_pnlReqMatl
            // 
            this.m_pnlReqMatl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_pnlReqMatl.Controls.Add(this._flex2);
            this.m_pnlReqMatl.Location = new System.Drawing.Point(7, 33);
            this.m_pnlReqMatl.Name = "m_pnlReqMatl";
            this.m_pnlReqMatl.Size = new System.Drawing.Size(748, 288);
            this.m_pnlReqMatl.TabIndex = 77;
            // 
            // _flex2
            // 
            this._flex2.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex2.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex2.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex2.AutoResize = false;
            this._flex2.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex2.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex2.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex2.EnabledHeaderCheck = true;
            this._flex2.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex2.Location = new System.Drawing.Point(0, 0);
            this._flex2.Name = "_flex2";
            this._flex2.Rows.Count = 1;
            this._flex2.Rows.DefaultSize = 18;
            this._flex2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex2.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex2.ShowSort = false;
            this._flex2.Size = new System.Drawing.Size(748, 288);
            this._flex2.StyleInfo = resources.GetString("_flex2.StyleInfo");
            this._flex2.TabIndex = 4;
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel7.BackgroundImage")));
            this.panel7.Location = new System.Drawing.Point(744, 310);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(18, 18);
            this.panel7.TabIndex = 76;
            // 
            // panel8
            // 
            this.panel8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel8.BackgroundImage")));
            this.panel8.Location = new System.Drawing.Point(0, 310);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(18, 18);
            this.panel8.TabIndex = 75;
            // 
            // panel27
            // 
            this.panel27.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel27.BackgroundImage")));
            this.panel27.Location = new System.Drawing.Point(0, 0);
            this.panel27.Name = "panel27";
            this.panel27.Size = new System.Drawing.Size(18, 18);
            this.panel27.TabIndex = 73;
            // 
            // panel28
            // 
            this.panel28.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel28.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel28.BackgroundImage")));
            this.panel28.Location = new System.Drawing.Point(744, 0);
            this.panel28.Name = "panel28";
            this.panel28.Size = new System.Drawing.Size(18, 18);
            this.panel28.TabIndex = 74;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            // 
            // m_pnlMain
            // 
            this.m_pnlMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_pnlMain.Controls.Add(this.m_txtCdItem);
            this.m_pnlMain.Controls.Add(this.m_dtTo);
            this.m_pnlMain.Controls.Add(this.m_dtFrom);
            this.m_pnlMain.Controls.Add(this.m_txtNoLineSo);
            this.m_pnlMain.Controls.Add(this.m_txtNoSo);
            this.m_pnlMain.Controls.Add(this.m_cboPath);
            this.m_pnlMain.Controls.Add(this.m_cboCdPlant);
            this.m_pnlMain.Controls.Add(this.m_cboStWo);
            this.m_pnlMain.Controls.Add(this.m_txtNoWo);
            this.m_pnlMain.Controls.Add(this.m_cboFgWo);
            this.m_pnlMain.Controls.Add(this.m_txtStndItem);
            this.m_pnlMain.Controls.Add(this.m_txtUnitIm);
            this.m_pnlMain.Controls.Add(this.m_ctxtQtWo);
            this.m_pnlMain.Controls.Add(this.m_ctxtQtWork);
            this.m_pnlMain.Controls.Add(this.label1);
            this.m_pnlMain.Controls.Add(this.m_cboRout);
            this.m_pnlMain.Controls.Add(this.m_txtNoLot);
            this.m_pnlMain.Controls.Add(this.m_btnTracking);
            this.m_pnlMain.Controls.Add(this.panel16);
            this.m_pnlMain.Controls.Add(this.panel17);
            this.m_pnlMain.Controls.Add(this.panel18);
            this.m_pnlMain.Controls.Add(this.panel19);
            this.m_pnlMain.Controls.Add(this.panel20);
            this.m_pnlMain.Controls.Add(this.panel21);
            this.m_pnlMain.Controls.Add(this.panel22);
            this.m_pnlMain.Controls.Add(this.panel23);
            this.m_pnlMain.Controls.Add(this.panel24);
            this.m_pnlMain.Location = new System.Drawing.Point(3, 3);
            this.m_pnlMain.Name = "m_pnlMain";
            this.m_pnlMain.Size = new System.Drawing.Size(787, 177);
            this.m_pnlMain.TabIndex = 0;
            // 
            // m_txtCdItem
            // 
            this.m_txtCdItem.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.m_txtCdItem.ButtonImage = ((System.Drawing.Image)(resources.GetObject("m_txtCdItem.ButtonImage")));
            this.m_txtCdItem.ChildMode = "";
            this.m_txtCdItem.CodeName = "";
            this.m_txtCdItem.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.m_txtCdItem.CodeValue = "";
            this.m_txtCdItem.ComboCheck = true;
            this.m_txtCdItem.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB;
            this.m_txtCdItem.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.m_txtCdItem.Location = new System.Drawing.Point(108, 52);
            this.m_txtCdItem.Name = "m_txtCdItem";
            this.m_txtCdItem.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.m_txtCdItem.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.m_txtCdItem.SearchCode = true;
            this.m_txtCdItem.SelectCount = 0;
            this.m_txtCdItem.SetDefaultValue = false;
            this.m_txtCdItem.SetNoneTypeMsg = "Please! Set Help Type!";
            this.m_txtCdItem.Size = new System.Drawing.Size(388, 21);
            this.m_txtCdItem.TabIndex = 4;
            this.m_txtCdItem.TabStop = false;
            this.m_txtCdItem.CodeChanged += new System.EventHandler(this.OnBpControl_CodeChanged);
            this.m_txtCdItem.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpControl_QueryBefore);
            this.m_txtCdItem.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpControl_QueryAfter);
            // 
            // m_dtTo
            // 
            this.m_dtTo.CalendarBackColor = System.Drawing.Color.White;
            this.m_dtTo.DayColor = System.Drawing.Color.Black;
            this.m_dtTo.FriDayColor = System.Drawing.Color.Blue;
            this.m_dtTo.Location = new System.Drawing.Point(219, 103);
            this.m_dtTo.Mask = "####/##/##";
            this.m_dtTo.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.m_dtTo.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.m_dtTo.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.m_dtTo.Modified = false;
            this.m_dtTo.Name = "m_dtTo";
            this.m_dtTo.PaddingCharacter = '_';
            this.m_dtTo.PassivePromptCharacter = '_';
            this.m_dtTo.PromptCharacter = '_';
            this.m_dtTo.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.m_dtTo.ShowToDay = true;
            this.m_dtTo.ShowTodayCircle = true;
            this.m_dtTo.ShowUpDown = false;
            this.m_dtTo.Size = new System.Drawing.Size(92, 21);
            this.m_dtTo.SunDayColor = System.Drawing.Color.Red;
            this.m_dtTo.TabIndex = 7;
            this.m_dtTo.TitleBackColor = System.Drawing.SystemColors.Control;
            this.m_dtTo.TitleForeColor = System.Drawing.Color.White;
            this.m_dtTo.ToDayColor = System.Drawing.Color.Red;
            this.m_dtTo.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.m_dtTo.UseKeyF3 = false;
            this.m_dtTo.Value = new System.DateTime(((long)(0)));
            this.m_dtTo.Click += new System.EventHandler(this.m_dtTo_Click);
            this.m_dtTo.Validated += new System.EventHandler(this.OnControlValidated);
            // 
            // m_dtFrom
            // 
            this.m_dtFrom.CalendarBackColor = System.Drawing.Color.White;
            this.m_dtFrom.DayColor = System.Drawing.Color.Black;
            this.m_dtFrom.FriDayColor = System.Drawing.Color.Blue;
            this.m_dtFrom.Location = new System.Drawing.Point(108, 102);
            this.m_dtFrom.Mask = "####/##/##";
            this.m_dtFrom.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.m_dtFrom.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.m_dtFrom.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.m_dtFrom.Modified = false;
            this.m_dtFrom.Name = "m_dtFrom";
            this.m_dtFrom.PaddingCharacter = '_';
            this.m_dtFrom.PassivePromptCharacter = '_';
            this.m_dtFrom.PromptCharacter = '_';
            this.m_dtFrom.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.m_dtFrom.ShowToDay = true;
            this.m_dtFrom.ShowTodayCircle = true;
            this.m_dtFrom.ShowUpDown = false;
            this.m_dtFrom.Size = new System.Drawing.Size(92, 21);
            this.m_dtFrom.SunDayColor = System.Drawing.Color.Red;
            this.m_dtFrom.TabIndex = 6;
            this.m_dtFrom.TitleBackColor = System.Drawing.SystemColors.Control;
            this.m_dtFrom.TitleForeColor = System.Drawing.Color.White;
            this.m_dtFrom.ToDayColor = System.Drawing.Color.Red;
            this.m_dtFrom.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.m_dtFrom.UseKeyF3 = false;
            this.m_dtFrom.Value = new System.DateTime(((long)(0)));
            this.m_dtFrom.Validated += new System.EventHandler(this.OnControlValidated);
            // 
            // m_txtNoLineSo
            // 
            this.m_txtNoLineSo.BackColor = System.Drawing.SystemColors.Control;
            this.m_txtNoLineSo.Enabled = false;
            this.m_txtNoLineSo.Location = new System.Drawing.Point(280, 152);
            this.m_txtNoLineSo.Name = "m_txtNoLineSo";
            this.m_txtNoLineSo.ReadOnly = true;
            this.m_txtNoLineSo.SelectedAllEnabled = false;
            this.m_txtNoLineSo.Size = new System.Drawing.Size(108, 21);
            this.m_txtNoLineSo.TabIndex = 0;
            this.m_txtNoLineSo.TabStop = false;
            this.m_txtNoLineSo.UseKeyEnter = true;
            this.m_txtNoLineSo.UseKeyF3 = true;
            // 
            // m_txtNoSo
            // 
            this.m_txtNoSo.Location = new System.Drawing.Point(108, 152);
            this.m_txtNoSo.MaxLength = 10;
            this.m_txtNoSo.Name = "m_txtNoSo";
            this.m_txtNoSo.SelectedAllEnabled = false;
            this.m_txtNoSo.Size = new System.Drawing.Size(140, 21);
            this.m_txtNoSo.TabIndex = 12;
            this.m_txtNoSo.UseKeyEnter = true;
            this.m_txtNoSo.UseKeyF3 = false;
            this.m_txtNoSo.DoubleClick += new System.EventHandler(this.OnControlDoubleClick);
            // 
            // m_cboPath
            // 
            this.m_cboPath.AutoDropDown = true;
            this.m_cboPath.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboPath.Location = new System.Drawing.Point(497, 128);
            this.m_cboPath.Name = "m_cboPath";
            this.m_cboPath.ShowCheckBox = false;
            this.m_cboPath.Size = new System.Drawing.Size(171, 20);
            this.m_cboPath.TabIndex = 9;
            this.m_cboPath.UseKeyEnter = true;
            this.m_cboPath.UseKeyF3 = false;
            // 
            // m_cboCdPlant
            // 
            this.m_cboCdPlant.AutoDropDown = false;
            this.m_cboCdPlant.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.m_cboCdPlant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboCdPlant.Location = new System.Drawing.Point(109, 5);
            this.m_cboCdPlant.Name = "m_cboCdPlant";
            this.m_cboCdPlant.ShowCheckBox = false;
            this.m_cboCdPlant.Size = new System.Drawing.Size(258, 20);
            this.m_cboCdPlant.TabIndex = 1;
            this.m_cboCdPlant.UseKeyEnter = true;
            this.m_cboCdPlant.UseKeyF3 = false;
            // 
            // m_cboStWo
            // 
            this.m_cboStWo.AutoDropDown = true;
            this.m_cboStWo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboStWo.Enabled = false;
            this.m_cboStWo.Location = new System.Drawing.Point(108, 127);
            this.m_cboStWo.Name = "m_cboStWo";
            this.m_cboStWo.ShowCheckBox = false;
            this.m_cboStWo.Size = new System.Drawing.Size(205, 20);
            this.m_cboStWo.TabIndex = 10;
            this.m_cboStWo.UseKeyEnter = true;
            this.m_cboStWo.UseKeyF3 = false;
            // 
            // m_txtNoWo
            // 
            this.m_txtNoWo.BackColor = System.Drawing.SystemColors.Control;
            this.m_txtNoWo.Location = new System.Drawing.Point(108, 28);
            this.m_txtNoWo.MaxLength = 20;
            this.m_txtNoWo.Name = "m_txtNoWo";
            this.m_txtNoWo.SelectedAllEnabled = false;
            this.m_txtNoWo.Size = new System.Drawing.Size(279, 21);
            this.m_txtNoWo.TabIndex = 2;
            this.m_txtNoWo.TabStop = false;
            this.m_txtNoWo.UseKeyEnter = false;
            this.m_txtNoWo.UseKeyF3 = false;
            this.m_txtNoWo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnControlKeyDown);
            // 
            // m_cboFgWo
            // 
            this.m_cboFgWo.AutoDropDown = true;
            this.m_cboFgWo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboFgWo.Enabled = false;
            this.m_cboFgWo.Location = new System.Drawing.Point(497, 28);
            this.m_cboFgWo.Name = "m_cboFgWo";
            this.m_cboFgWo.ShowCheckBox = false;
            this.m_cboFgWo.Size = new System.Drawing.Size(185, 20);
            this.m_cboFgWo.TabIndex = 3;
            this.m_cboFgWo.UseKeyEnter = true;
            this.m_cboFgWo.UseKeyF3 = false;
            // 
            // m_txtStndItem
            // 
            this.m_txtStndItem.BackColor = System.Drawing.SystemColors.Control;
            this.m_txtStndItem.Location = new System.Drawing.Point(496, 52);
            this.m_txtStndItem.Name = "m_txtStndItem";
            this.m_txtStndItem.ReadOnly = true;
            this.m_txtStndItem.SelectedAllEnabled = false;
            this.m_txtStndItem.Size = new System.Drawing.Size(182, 21);
            this.m_txtStndItem.TabIndex = 0;
            this.m_txtStndItem.TabStop = false;
            this.m_txtStndItem.UseKeyEnter = true;
            this.m_txtStndItem.UseKeyF3 = true;
            // 
            // m_txtUnitIm
            // 
            this.m_txtUnitIm.BackColor = System.Drawing.SystemColors.Control;
            this.m_txtUnitIm.Location = new System.Drawing.Point(679, 52);
            this.m_txtUnitIm.Name = "m_txtUnitIm";
            this.m_txtUnitIm.ReadOnly = true;
            this.m_txtUnitIm.SelectedAllEnabled = false;
            this.m_txtUnitIm.Size = new System.Drawing.Size(100, 21);
            this.m_txtUnitIm.TabIndex = 0;
            this.m_txtUnitIm.TabStop = false;
            this.m_txtUnitIm.UseKeyEnter = true;
            this.m_txtUnitIm.UseKeyF3 = true;
            // 
            // m_ctxtQtWo
            // 
            this.m_ctxtQtWo.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.m_ctxtQtWo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_ctxtQtWo.Location = new System.Drawing.Point(108, 77);
            this.m_ctxtQtWo.Mask = null;
            this.m_ctxtQtWo.MaxLength = 13;
            this.m_ctxtQtWo.MaxValue = new decimal(new int[] {
            1316134911,
            2328,
            0,
            0});
            this.m_ctxtQtWo.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.m_ctxtQtWo.Name = "m_ctxtQtWo";
            this.m_ctxtQtWo.NullString = "0";
            this.m_ctxtQtWo.PositiveColor = System.Drawing.Color.Black;
            this.m_ctxtQtWo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.m_ctxtQtWo.Size = new System.Drawing.Size(204, 21);
            this.m_ctxtQtWo.TabIndex = 5;
            this.m_ctxtQtWo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_ctxtQtWo.UseKeyEnter = true;
            this.m_ctxtQtWo.UseKeyF3 = false;
            this.m_ctxtQtWo.Validated += new System.EventHandler(this.OnQtControlValidated);
            // 
            // m_ctxtQtWork
            // 
            this.m_ctxtQtWork.BackColor = System.Drawing.SystemColors.Control;
            this.m_ctxtQtWork.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.m_ctxtQtWork.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_ctxtQtWork.Location = new System.Drawing.Point(497, 77);
            this.m_ctxtQtWork.Mask = null;
            this.m_ctxtQtWork.Name = "m_ctxtQtWork";
            this.m_ctxtQtWork.NullString = "0";
            this.m_ctxtQtWork.PositiveColor = System.Drawing.Color.Black;
            this.m_ctxtQtWork.ReadOnly = true;
            this.m_ctxtQtWork.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.m_ctxtQtWork.Size = new System.Drawing.Size(184, 21);
            this.m_ctxtQtWork.TabIndex = 0;
            this.m_ctxtQtWork.TabStop = false;
            this.m_ctxtQtWork.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_ctxtQtWork.UseKeyEnter = true;
            this.m_ctxtQtWork.UseKeyF3 = true;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(202, 104);
            this.label1.Name = "label1";
            this.label1.Resizeble = true;
            this.label1.Size = new System.Drawing.Size(15, 14);
            this.label1.TabIndex = 104;
            this.label1.Text = "∼";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_cboRout
            // 
            this.m_cboRout.AutoDropDown = true;
            this.m_cboRout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboRout.Location = new System.Drawing.Point(497, 102);
            this.m_cboRout.Name = "m_cboRout";
            this.m_cboRout.ShowCheckBox = false;
            this.m_cboRout.Size = new System.Drawing.Size(110, 20);
            this.m_cboRout.TabIndex = 8;
            this.m_cboRout.UseKeyEnter = true;
            this.m_cboRout.UseKeyF3 = false;
            this.m_cboRout.SelectionChangeCommitted += new System.EventHandler(this.m_cbo_Rout_SelectedIndexChanged);
            // 
            // m_txtNoLot
            // 
            this.m_txtNoLot.Location = new System.Drawing.Point(497, 152);
            this.m_txtNoLot.MaxLength = 20;
            this.m_txtNoLot.Name = "m_txtNoLot";
            this.m_txtNoLot.SelectedAllEnabled = false;
            this.m_txtNoLot.Size = new System.Drawing.Size(280, 21);
            this.m_txtNoLot.TabIndex = 11;
            this.m_txtNoLot.Tag = "no_lot";
            this.m_txtNoLot.UseKeyEnter = true;
            this.m_txtNoLot.UseKeyF3 = false;
            // 
            // m_btnTracking
            // 
            this.m_btnTracking.BackColor = System.Drawing.SystemColors.Control;
            this.m_btnTracking.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnTracking.Image = ((System.Drawing.Image)(resources.GetObject("m_btnTracking.Image")));
            this.m_btnTracking.Location = new System.Drawing.Point(249, 152);
            this.m_btnTracking.Name = "m_btnTracking";
            this.m_btnTracking.Size = new System.Drawing.Size(30, 21);
            this.m_btnTracking.TabIndex = 0;
            this.m_btnTracking.TabStop = false;
            this.m_btnTracking.UseVisualStyleBackColor = false;
            this.m_btnTracking.Click += new System.EventHandler(this.OnControlClick);
            // 
            // panel16
            // 
            this.panel16.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel16.BackColor = System.Drawing.Color.Transparent;
            this.panel16.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel16.BackgroundImage")));
            this.panel16.Location = new System.Drawing.Point(5, 99);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(777, 1);
            this.panel16.TabIndex = 74;
            // 
            // panel17
            // 
            this.panel17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel17.BackColor = System.Drawing.Color.Transparent;
            this.panel17.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel17.BackgroundImage")));
            this.panel17.Location = new System.Drawing.Point(5, 74);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(777, 1);
            this.panel17.TabIndex = 73;
            // 
            // panel18
            // 
            this.panel18.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel18.BackColor = System.Drawing.Color.Transparent;
            this.panel18.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel18.BackgroundImage")));
            this.panel18.Location = new System.Drawing.Point(5, 149);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(777, 1);
            this.panel18.TabIndex = 76;
            // 
            // panel19
            // 
            this.panel19.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel19.BackColor = System.Drawing.Color.Transparent;
            this.panel19.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel19.BackgroundImage")));
            this.panel19.Location = new System.Drawing.Point(5, 124);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(777, 1);
            this.panel19.TabIndex = 75;
            // 
            // panel20
            // 
            this.panel20.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel20.BackColor = System.Drawing.Color.Transparent;
            this.panel20.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel20.BackgroundImage")));
            this.panel20.Location = new System.Drawing.Point(5, 49);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(777, 1);
            this.panel20.TabIndex = 72;
            // 
            // panel21
            // 
            this.panel21.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel21.BackColor = System.Drawing.Color.Transparent;
            this.panel21.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel21.BackgroundImage")));
            this.panel21.Location = new System.Drawing.Point(5, 25);
            this.panel21.Name = "panel21";
            this.panel21.Size = new System.Drawing.Size(777, 1);
            this.panel21.TabIndex = 71;
            // 
            // panel22
            // 
            this.panel22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel22.Controls.Add(this.labelExt1);
            this.panel22.Controls.Add(this.m_lblTpWo);
            this.panel22.Controls.Add(this.m_lblQtWork);
            this.panel22.Controls.Add(this.m_lblNoLot);
            this.panel22.Location = new System.Drawing.Point(390, 74);
            this.panel22.Name = "panel22";
            this.panel22.Size = new System.Drawing.Size(103, 102);
            this.panel22.TabIndex = 40;
            // 
            // m_lblTpWo
            // 
            this.m_lblTpWo.BackColor = System.Drawing.Color.Transparent;
            this.m_lblTpWo.Location = new System.Drawing.Point(2, 28);
            this.m_lblTpWo.Name = "m_lblTpWo";
            this.m_lblTpWo.Resizeble = true;
            this.m_lblTpWo.Size = new System.Drawing.Size(98, 18);
            this.m_lblTpWo.TabIndex = 1;
            this.m_lblTpWo.Tag = "TP_WO";
            this.m_lblTpWo.Text = "오더형태";
            this.m_lblTpWo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblQtWork
            // 
            this.m_lblQtWork.BackColor = System.Drawing.Color.Transparent;
            this.m_lblQtWork.Location = new System.Drawing.Point(2, 5);
            this.m_lblQtWork.Name = "m_lblQtWork";
            this.m_lblQtWork.Resizeble = true;
            this.m_lblQtWork.Size = new System.Drawing.Size(98, 18);
            this.m_lblQtWork.TabIndex = 1;
            this.m_lblQtWork.Tag = "QT_WORK";
            this.m_lblQtWork.Text = "작업수량";
            this.m_lblQtWork.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblNoLot
            // 
            this.m_lblNoLot.BackColor = System.Drawing.Color.Transparent;
            this.m_lblNoLot.Location = new System.Drawing.Point(2, 81);
            this.m_lblNoLot.Name = "m_lblNoLot";
            this.m_lblNoLot.Resizeble = true;
            this.m_lblNoLot.Size = new System.Drawing.Size(98, 23);
            this.m_lblNoLot.TabIndex = 1;
            this.m_lblNoLot.Tag = "LOT_NO";
            this.m_lblNoLot.Text = "Lot/ser. no";
            this.m_lblNoLot.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel23
            // 
            this.panel23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel23.Controls.Add(this.m_lblFgWo);
            this.panel23.Location = new System.Drawing.Point(390, 25);
            this.panel23.Name = "panel23";
            this.panel23.Size = new System.Drawing.Size(103, 25);
            this.panel23.TabIndex = 39;
            // 
            // m_lblFgWo
            // 
            this.m_lblFgWo.BackColor = System.Drawing.Color.Transparent;
            this.m_lblFgWo.Location = new System.Drawing.Point(2, 5);
            this.m_lblFgWo.Name = "m_lblFgWo";
            this.m_lblFgWo.Resizeble = true;
            this.m_lblFgWo.Size = new System.Drawing.Size(98, 18);
            this.m_lblFgWo.TabIndex = 1;
            this.m_lblFgWo.Tag = "FG_WO";
            this.m_lblFgWo.Text = "작업지시구분";
            this.m_lblFgWo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel24
            // 
            this.panel24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel24.Controls.Add(this.m_lblCdPlant);
            this.panel24.Controls.Add(this.m_lblNoWo);
            this.panel24.Controls.Add(this.m_lblCdItem);
            this.panel24.Controls.Add(this.m_lblQtWo);
            this.panel24.Controls.Add(this.m_lblPeriodWork);
            this.panel24.Controls.Add(this.m_lblStWo);
            this.panel24.Controls.Add(this.m_lblNoTrack);
            this.panel24.Location = new System.Drawing.Point(1, 1);
            this.panel24.Name = "panel24";
            this.panel24.Size = new System.Drawing.Size(103, 173);
            this.panel24.TabIndex = 38;
            // 
            // m_lblCdPlant
            // 
            this.m_lblCdPlant.BackColor = System.Drawing.Color.Transparent;
            this.m_lblCdPlant.Location = new System.Drawing.Point(2, 5);
            this.m_lblCdPlant.Name = "m_lblCdPlant";
            this.m_lblCdPlant.Resizeble = true;
            this.m_lblCdPlant.Size = new System.Drawing.Size(98, 18);
            this.m_lblCdPlant.TabIndex = 1;
            this.m_lblCdPlant.Tag = "PLANT";
            this.m_lblCdPlant.Text = "공장";
            this.m_lblCdPlant.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblNoWo
            // 
            this.m_lblNoWo.BackColor = System.Drawing.Color.Transparent;
            this.m_lblNoWo.Location = new System.Drawing.Point(2, 29);
            this.m_lblNoWo.Name = "m_lblNoWo";
            this.m_lblNoWo.Resizeble = true;
            this.m_lblNoWo.Size = new System.Drawing.Size(98, 18);
            this.m_lblNoWo.TabIndex = 1;
            this.m_lblNoWo.Tag = "NO_WO";
            this.m_lblNoWo.Text = "작업지시번호";
            this.m_lblNoWo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblCdItem
            // 
            this.m_lblCdItem.BackColor = System.Drawing.Color.Transparent;
            this.m_lblCdItem.Location = new System.Drawing.Point(2, 54);
            this.m_lblCdItem.Name = "m_lblCdItem";
            this.m_lblCdItem.Resizeble = true;
            this.m_lblCdItem.Size = new System.Drawing.Size(98, 18);
            this.m_lblCdItem.TabIndex = 1;
            this.m_lblCdItem.Tag = "ITEM_WORK";
            this.m_lblCdItem.Text = "작업품목";
            this.m_lblCdItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblQtWo
            // 
            this.m_lblQtWo.BackColor = System.Drawing.Color.Transparent;
            this.m_lblQtWo.Location = new System.Drawing.Point(2, 78);
            this.m_lblQtWo.Name = "m_lblQtWo";
            this.m_lblQtWo.Resizeble = true;
            this.m_lblQtWo.Size = new System.Drawing.Size(98, 18);
            this.m_lblQtWo.TabIndex = 1;
            this.m_lblQtWo.Tag = "QT_WO";
            this.m_lblQtWo.Text = "공정별 지시수량";
            this.m_lblQtWo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblPeriodWork
            // 
            this.m_lblPeriodWork.BackColor = System.Drawing.Color.Transparent;
            this.m_lblPeriodWork.Location = new System.Drawing.Point(2, 103);
            this.m_lblPeriodWork.Name = "m_lblPeriodWork";
            this.m_lblPeriodWork.Resizeble = true;
            this.m_lblPeriodWork.Size = new System.Drawing.Size(98, 18);
            this.m_lblPeriodWork.TabIndex = 1;
            this.m_lblPeriodWork.Tag = "PERIOD_WORK";
            this.m_lblPeriodWork.Text = "작업기간";
            this.m_lblPeriodWork.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblStWo
            // 
            this.m_lblStWo.BackColor = System.Drawing.Color.Transparent;
            this.m_lblStWo.Location = new System.Drawing.Point(2, 128);
            this.m_lblStWo.Name = "m_lblStWo";
            this.m_lblStWo.Resizeble = true;
            this.m_lblStWo.Size = new System.Drawing.Size(98, 18);
            this.m_lblStWo.TabIndex = 1;
            this.m_lblStWo.Tag = "ST_WO";
            this.m_lblStWo.Text = "상태";
            this.m_lblStWo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblNoTrack
            // 
            this.m_lblNoTrack.BackColor = System.Drawing.Color.Transparent;
            this.m_lblNoTrack.Location = new System.Drawing.Point(2, 153);
            this.m_lblNoTrack.Name = "m_lblNoTrack";
            this.m_lblNoTrack.Resizeble = true;
            this.m_lblNoTrack.Size = new System.Drawing.Size(98, 23);
            this.m_lblNoTrack.TabIndex = 1;
            this.m_lblNoTrack.Tag = "NO_TRACK";
            this.m_lblNoTrack.Text = "Tracking no.";
            this.m_lblNoTrack.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _dsRoutBill
            // 
            this._dsRoutBill.DataSetName = "NewDataSet";
            this._dsRoutBill.Locale = new System.Globalization.CultureInfo("ko-KR");
            this._dsRoutBill.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable1,
            this.dataTable2,
            this.dataTable3});
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
            this.dataColumn79,
            this.dataColumn98,
            this.dataColumn99,
            this.dataColumn109,
            this.dataColumn110,
            this.dataColumn111,
            this.dataColumn112,
            this.dataColumn113,
            this.dataColumn134,
            this.dataColumn48,
            this.dataColumn49});
            this.dataTable1.TableName = "PR_WO";
            // 
            // dataColumn1
            // 
            this.dataColumn1.ColumnName = "CD_COMPANY";
            // 
            // dataColumn2
            // 
            this.dataColumn2.ColumnName = "CD_PLANT";
            // 
            // dataColumn3
            // 
            this.dataColumn3.ColumnName = "NO_WO";
            // 
            // dataColumn4
            // 
            this.dataColumn4.ColumnName = "CD_ITEM";
            // 
            // dataColumn5
            // 
            this.dataColumn5.ColumnName = "FG_WO";
            // 
            // dataColumn6
            // 
            this.dataColumn6.Caption = "QT_ITEM";
            this.dataColumn6.ColumnName = "QT_ITEM";
            this.dataColumn6.DataType = typeof(double);
            this.dataColumn6.DefaultValue = 0;
            // 
            // dataColumn7
            // 
            this.dataColumn7.ColumnName = "QT_WORK";
            this.dataColumn7.DataType = typeof(double);
            this.dataColumn7.DefaultValue = 0;
            // 
            // dataColumn8
            // 
            this.dataColumn8.ColumnName = "DT_REL";
            // 
            // dataColumn9
            // 
            this.dataColumn9.ColumnName = "DT_DUE";
            // 
            // dataColumn10
            // 
            this.dataColumn10.ColumnName = "PATN_ROUT";
            // 
            // dataColumn11
            // 
            this.dataColumn11.ColumnName = "ST_WO";
            // 
            // dataColumn12
            // 
            this.dataColumn12.ColumnName = "NO_LOT";
            // 
            // dataColumn13
            // 
            this.dataColumn13.ColumnName = "NO_PJT";
            // 
            // dataColumn79
            // 
            this.dataColumn79.ColumnName = "TP_OPPATH";
            // 
            // dataColumn98
            // 
            this.dataColumn98.ColumnName = "NO_SO";
            // 
            // dataColumn99
            // 
            this.dataColumn99.ColumnName = "NO_LINE_SO";
            this.dataColumn99.DataType = typeof(int);
            // 
            // dataColumn109
            // 
            this.dataColumn109.ColumnName = "NO_EMP";
            // 
            // dataColumn110
            // 
            this.dataColumn110.ColumnName = "TP_ROUT";
            // 
            // dataColumn111
            // 
            this.dataColumn111.ColumnName = "ID_INSERT";
            // 
            // dataColumn112
            // 
            this.dataColumn112.ColumnName = "DTS_UPDATE";
            // 
            // dataColumn113
            // 
            this.dataColumn113.ColumnName = "ID_UPDATE";
            // 
            // dataColumn134
            // 
            this.dataColumn134.ColumnName = "DTS_INSERT";
            // 
            // dataColumn48
            // 
            this.dataColumn48.ColumnName = "TP_GI";
            // 
            // dataColumn49
            // 
            this.dataColumn49.ColumnName = "TP_GR";
            // 
            // dataTable2
            // 
            this.dataTable2.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn14,
            this.dataColumn15,
            this.dataColumn16,
            this.dataColumn17,
            this.dataColumn18,
            this.dataColumn66,
            this.dataColumn67,
            this.dataColumn68,
            this.dataColumn69,
            this.dataColumn70,
            this.dataColumn71,
            this.dataColumn72,
            this.dataColumn73,
            this.dataColumn74,
            this.dataColumn75,
            this.dataColumn76,
            this.dataColumn77,
            this.dataColumn78,
            this.dataColumn80,
            this.dataColumn82,
            this.dataColumn83,
            this.dataColumn84,
            this.dataColumn85,
            this.dataColumn96,
            this.dataColumn97,
            this.dataColumn103,
            this.dataColumn104,
            this.dataColumn105,
            this.dataColumn106,
            this.dataColumn114,
            this.dataColumn115,
            this.dataColumn116,
            this.dataColumn117,
            this.dataColumn118,
            this.dataColumn119,
            this.dataColumn120,
            this.dataColumn121,
            this.dataColumn122,
            this.dataColumn123,
            this.dataColumn124,
            this.dataColumn125,
            this.dataColumn126,
            this.dataColumn127,
            this.dataColumn128,
            this.dataColumn135,
            this.dataColumn137});
            this.dataTable2.TableName = "PR_WO_ROUT";
            // 
            // dataColumn14
            // 
            this.dataColumn14.ColumnName = "CHK";
            // 
            // dataColumn15
            // 
            this.dataColumn15.ColumnName = "CD_OP";
            // 
            // dataColumn16
            // 
            this.dataColumn16.ColumnName = "CD_WC";
            // 
            // dataColumn17
            // 
            this.dataColumn17.ColumnName = "NM_WC";
            // 
            // dataColumn18
            // 
            this.dataColumn18.ColumnName = "NM_SYSDEF";
            // 
            // dataColumn66
            // 
            this.dataColumn66.ColumnName = "FG_WC";
            // 
            // dataColumn67
            // 
            this.dataColumn67.ColumnName = "CD_WCOP";
            // 
            // dataColumn68
            // 
            this.dataColumn68.ColumnName = "NM_OP";
            // 
            // dataColumn69
            // 
            this.dataColumn69.ColumnName = "ST_OP";
            // 
            // dataColumn70
            // 
            this.dataColumn70.ColumnName = "QT_WO";
            this.dataColumn70.DataType = typeof(double);
            this.dataColumn70.DefaultValue = 0;
            // 
            // dataColumn71
            // 
            this.dataColumn71.ColumnName = "YN_BF";
            // 
            // dataColumn72
            // 
            this.dataColumn72.ColumnName = "YN_RECEIPT";
            // 
            // dataColumn73
            // 
            this.dataColumn73.ColumnName = "QT_WIP";
            this.dataColumn73.DataType = typeof(double);
            this.dataColumn73.DefaultValue = 0;
            // 
            // dataColumn74
            // 
            this.dataColumn74.ColumnName = "QT_WORK";
            this.dataColumn74.DataType = typeof(double);
            this.dataColumn74.DefaultValue = 0;
            // 
            // dataColumn75
            // 
            this.dataColumn75.ColumnName = "QT_REJECT";
            this.dataColumn75.DataType = typeof(double);
            this.dataColumn75.DefaultValue = 0;
            // 
            // dataColumn76
            // 
            this.dataColumn76.ColumnName = "QT_REWORK";
            this.dataColumn76.DataType = typeof(double);
            this.dataColumn76.DefaultValue = 0;
            // 
            // dataColumn77
            // 
            this.dataColumn77.ColumnName = "QT_MOVE";
            this.dataColumn77.DataType = typeof(double);
            this.dataColumn77.DefaultValue = 0;
            // 
            // dataColumn78
            // 
            this.dataColumn78.ColumnName = "NO_LINE";
            this.dataColumn78.DataType = typeof(int);
            this.dataColumn78.DefaultValue = 1;
            // 
            // dataColumn80
            // 
            this.dataColumn80.ColumnName = "NO_WO";
            // 
            // dataColumn82
            // 
            this.dataColumn82.ColumnName = "DT_REL";
            // 
            // dataColumn83
            // 
            this.dataColumn83.ColumnName = "DT_DUE";
            // 
            // dataColumn84
            // 
            this.dataColumn84.ColumnName = "CD_COMPANY";
            // 
            // dataColumn85
            // 
            this.dataColumn85.ColumnName = "CD_PLANT";
            // 
            // dataColumn96
            // 
            this.dataColumn96.ColumnName = "YN_PAR";
            // 
            // dataColumn97
            // 
            this.dataColumn97.ColumnName = "YN_QC";
            // 
            // dataColumn103
            // 
            this.dataColumn103.ColumnName = "DIS_CD_WC";
            // 
            // dataColumn104
            // 
            this.dataColumn104.ColumnName = "CD_OP_BASE";
            // 
            // dataColumn105
            // 
            this.dataColumn105.Caption = "YN_FINAL";
            this.dataColumn105.ColumnName = "YN_FINAL";
            // 
            // dataColumn106
            // 
            this.dataColumn106.ColumnName = "QT_START";
            this.dataColumn106.DataType = typeof(double);
            this.dataColumn106.DefaultValue = 0;
            // 
            // dataColumn114
            // 
            this.dataColumn114.ColumnName = "YN_WORK";
            // 
            // dataColumn115
            // 
            this.dataColumn115.ColumnName = "TM_SETUP";
            // 
            // dataColumn116
            // 
            this.dataColumn116.ColumnName = "CD_RSRC1";
            // 
            // dataColumn117
            // 
            this.dataColumn117.ColumnName = "CD_RSRC2";
            // 
            // dataColumn118
            // 
            this.dataColumn118.ColumnName = "TM_MACH";
            // 
            // dataColumn119
            // 
            this.dataColumn119.ColumnName = "TM_MOVE";
            // 
            // dataColumn120
            // 
            this.dataColumn120.ColumnName = "DY_SUBCON";
            // 
            // dataColumn121
            // 
            this.dataColumn121.ColumnName = "TM_LABOR_ACT";
            // 
            // dataColumn122
            // 
            this.dataColumn122.ColumnName = "TM_MACH_ACT";
            // 
            // dataColumn123
            // 
            this.dataColumn123.ColumnName = "CD_TOOL";
            // 
            // dataColumn124
            // 
            this.dataColumn124.ColumnName = "TM_REL";
            // 
            // dataColumn125
            // 
            this.dataColumn125.ColumnName = "TM_DUE";
            // 
            // dataColumn126
            // 
            this.dataColumn126.ColumnName = "TM";
            // 
            // dataColumn127
            // 
            this.dataColumn127.ColumnName = "QT_CLS";
            this.dataColumn127.DataType = typeof(double);
            this.dataColumn127.DefaultValue = 0;
            // 
            // dataColumn128
            // 
            this.dataColumn128.ColumnName = "QT_OUTPO";
            this.dataColumn128.DataType = typeof(double);
            this.dataColumn128.DefaultValue = 0;
            // 
            // dataColumn135
            // 
            this.dataColumn135.ColumnName = "TM_LABOR";
            // 
            // dataColumn137
            // 
            this.dataColumn137.ColumnName = "TP_OPPATH";
            // 
            // dataTable3
            // 
            this.dataTable3.Columns.AddRange(new System.Data.DataColumn[] {
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
            this.dataColumn81,
            this.dataColumn86,
            this.dataColumn87,
            this.dataColumn88,
            this.dataColumn107,
            this.dataColumn108,
            this.dataColumn129,
            this.dataColumn130,
            this.dataColumn131,
            this.dataColumn132,
            this.dataColumn136,
            this.dataColumn47,
            this.dataColumn50,
            this.dataColumn51});
            this.dataTable3.TableName = "PR_WO_BILL";
            // 
            // dataColumn32
            // 
            this.dataColumn32.ColumnName = "CHK";
            // 
            // dataColumn33
            // 
            this.dataColumn33.ColumnName = "CD_OP";
            // 
            // dataColumn34
            // 
            this.dataColumn34.ColumnName = "CD_WC";
            // 
            // dataColumn35
            // 
            this.dataColumn35.ColumnName = "NM_WC";
            // 
            // dataColumn36
            // 
            this.dataColumn36.ColumnName = "CD_MATL";
            // 
            // dataColumn37
            // 
            this.dataColumn37.ColumnName = "NM_ITEM";
            // 
            // dataColumn38
            // 
            this.dataColumn38.ColumnName = "STND_ITEM";
            // 
            // dataColumn39
            // 
            this.dataColumn39.ColumnName = "UNIT_IM";
            // 
            // dataColumn40
            // 
            this.dataColumn40.ColumnName = "YN_BF";
            // 
            // dataColumn41
            // 
            this.dataColumn41.ColumnName = "DT_NEED";
            // 
            // dataColumn42
            // 
            this.dataColumn42.ColumnName = "QT_NEED";
            this.dataColumn42.DataType = typeof(double);
            this.dataColumn42.DefaultValue = 0;
            // 
            // dataColumn43
            // 
            this.dataColumn43.ColumnName = "QT_REQ";
            this.dataColumn43.DataType = typeof(double);
            this.dataColumn43.DefaultValue = 0;
            // 
            // dataColumn44
            // 
            this.dataColumn44.ColumnName = "QT_ISU";
            this.dataColumn44.DataType = typeof(double);
            this.dataColumn44.DefaultValue = 0;
            // 
            // dataColumn45
            // 
            this.dataColumn45.ColumnName = "QT_USE";
            this.dataColumn45.DataType = typeof(double);
            this.dataColumn45.DefaultValue = 0;
            // 
            // dataColumn46
            // 
            this.dataColumn46.ColumnName = "NO_LINE";
            this.dataColumn46.DataType = typeof(int);
            this.dataColumn46.DefaultValue = 1;
            // 
            // dataColumn81
            // 
            this.dataColumn81.ColumnName = "NO_WO";
            // 
            // dataColumn86
            // 
            this.dataColumn86.ColumnName = "CD_COMPANY";
            // 
            // dataColumn87
            // 
            this.dataColumn87.ColumnName = "CD_PLANT";
            // 
            // dataColumn88
            // 
            this.dataColumn88.ColumnName = "DIS_CD_WC";
            // 
            // dataColumn107
            // 
            this.dataColumn107.ColumnName = "CD_WCOP";
            // 
            // dataColumn108
            // 
            this.dataColumn108.ColumnName = "NM_OP";
            // 
            // dataColumn129
            // 
            this.dataColumn129.ColumnName = "QT_RTN";
            this.dataColumn129.DataType = typeof(double);
            this.dataColumn129.DefaultValue = 0;
            // 
            // dataColumn130
            // 
            this.dataColumn130.ColumnName = "NO_REQ";
            // 
            // dataColumn131
            // 
            this.dataColumn131.ColumnName = "QT_NEED_NET";
            this.dataColumn131.DataType = typeof(double);
            // 
            // dataColumn132
            // 
            this.dataColumn132.ColumnName = "QT_REQ_RETURN";
            this.dataColumn132.DataType = typeof(double);
            this.dataColumn132.DefaultValue = 0;
            // 
            // dataColumn136
            // 
            this.dataColumn136.ColumnName = "FG_NEED";
            // 
            // dataColumn47
            // 
            this.dataColumn47.ColumnName = "QT_TRN";
            this.dataColumn47.DataType = typeof(double);
            this.dataColumn47.DefaultValue = 0;
            // 
            // dataColumn50
            // 
            this.dataColumn50.ColumnName = "FG_GIR";
            // 
            // dataColumn51
            // 
            this.dataColumn51.ColumnName = "TP_OPPATH";
            // 
            // dataColumn20
            // 
            this.dataColumn20.ColumnName = "CD_WCOP";
            // 
            // dataColumn21
            // 
            this.dataColumn21.ColumnName = "NM_OP";
            // 
            // dataColumn22
            // 
            this.dataColumn22.ColumnName = "ST_OP";
            // 
            // dataColumn23
            // 
            this.dataColumn23.ColumnName = "QT_WO";
            this.dataColumn23.DataType = typeof(double);
            // 
            // dataColumn24
            // 
            this.dataColumn24.ColumnName = "YN_BF";
            // 
            // dataColumn25
            // 
            this.dataColumn25.ColumnName = "YN_RECEIPT";
            // 
            // dataColumn26
            // 
            this.dataColumn26.ColumnName = "QT_WIP";
            this.dataColumn26.DataType = typeof(double);
            // 
            // dataColumn27
            // 
            this.dataColumn27.ColumnName = "QT_WORK";
            this.dataColumn27.DataType = typeof(double);
            // 
            // dataColumn28
            // 
            this.dataColumn28.ColumnName = "QT_REJECT";
            this.dataColumn28.DataType = typeof(double);
            // 
            // dataColumn29
            // 
            this.dataColumn29.ColumnName = "QT_REWORK";
            this.dataColumn29.DataType = typeof(double);
            // 
            // dataColumn30
            // 
            this.dataColumn30.ColumnName = "QT_MOVE";
            this.dataColumn30.DataType = typeof(double);
            // 
            // dataColumn31
            // 
            this.dataColumn31.ColumnName = "NO_LINE";
            this.dataColumn31.DataType = typeof(int);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.m_pnlMain, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 59);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(793, 561);
            this.tableLayoutPanel1.TabIndex = 72;
            // 
            // labelExt1
            // 
            this.labelExt1.BackColor = System.Drawing.Color.Transparent;
            this.labelExt1.Location = new System.Drawing.Point(2, 54);
            this.labelExt1.Name = "labelExt1";
            this.labelExt1.Resizeble = true;
            this.labelExt1.Size = new System.Drawing.Size(98, 18);
            this.labelExt1.TabIndex = 2;
            this.labelExt1.Tag = "TP_OPPATH";
            this.labelExt1.Text = "경로유형";
            this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // P_PR_WO_REG_NEW
            // 
            this.AutoScroll = true;
            this.Controls.Add(this.tableLayoutPanel1);
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Name = "P_PR_WO_REG_NEW";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPagePaint);
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.tabControl1.ResumeLayout(false);
            this.m_tabRout.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.m_pnlInfoRout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex1)).EndInit();
            this.m_tabMatl.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.m_pnlReqMatl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex2)).EndInit();
            this.m_pnlMain.ResumeLayout(false);
            this.m_pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_ctxtQtWo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_ctxtQtWork)).EndInit();
            this.panel22.ResumeLayout(false);
            this.panel23.ResumeLayout(false);
            this.panel24.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._dsRoutBill)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable3)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        #endregion

        #region ♣ 초기화 이벤트 / 메소드

        #region -> Page_Load
        /// <summary>
        /// 페이지 로드 이벤트 핸들러(화면 초기화 작업)
        /// </summary>
        private void OnPageLoad(object sender, EventArgs e)
        {
            this.Enabled = false;
            this.Show();
            _flex1.GridStyle = GridStyleEnum.Blue;
            _flex2.GridStyle = GridStyleEnum.Green;
            Application.DoEvents();
        }
        #endregion

        #region -> GetDDItem

        private string GetDDItem(params string[] colName)
        {
            string temp = "";

            for (int i = 0; i < colName.Length; i++)
            {
                switch (colName[i])		// DataView 의 컬럼이름
                {
                    case "CHK":			// 체크
                        temp = temp + " + " + "S";
                        break;
                    case "CD_OP":		// 공정순서(순번)
                        temp = temp + " + " + this.GetDataDictionaryItem("PR", "OP");
                        break;
                    case "CD_WC":		// W/C(작업장)
                        temp = temp + " + " + this.GetDataDictionaryItem("PR", "CD_WC");
                        break;
                    case "DIS_CD_WC":	// W/C(작업장명)
                    case "NM_WC":		// W/C(작업장명)
                        temp = temp + " + " + this.GetDataDictionaryItem("PR", "NM_CD_WC");
                        break;
                    case "NM_SYSDEF":	// 작업장타입명
                        temp = temp + " + " + this.GetDataDictionaryItem("PR", "TP_WC");
                        break;
                    case "CD_MATL":		// 품목코드
                        temp = temp + " + " + this.GetDataDictionaryItem("MA", "CD_ITEM");
                        break;
                    case "NM_ITEM":		// 품목명
                        temp = temp + " + " + this.GetDataDictionaryItem("MA", "NM_ITEM");
                        break;
                    case "STND_ITEM":	// 규격
                        temp = temp + " + " + this.GetDataDictionaryItem("MA", "STND_ITEM");
                        break;
                    case "UNIT_IM":		// 단위
                        temp = temp + " + " + this.GetDataDictionaryItem("MA", "UNIT_IM");
                        break;
                    case "CD_WCOP":		// 공정코드
                        temp = temp + " + " + this.GetDataDictionaryItem("PR", "CD_WCOP");
                        break;
                    case "NM_OP":		// 공정명
                        temp = temp + " + " + this.GetDataDictionaryItem("PR", "NM_OP");
                        break;
                    case "ST_OP":		// 공정상태
                        temp = temp + " + " + this.GetDataDictionaryItem("PR", "ST_OP");
                        break;
                    case "QT_WO":		// 공정별 지시수량
                        temp = temp + " + " + this.GetDataDictionaryItem("PR", "BYOP") + " " + this.GetDataDictionaryItem("PR", "QT_WO");
                        break;
                    case "BF":			// BF
                    case "YN_BF":		// B/F
                        temp = temp + " + " + this.GetDataDictionaryItem("PR", "YN_BF");
                        break;
                    case "YN_RECEIPT":	// M
                        temp = temp + " + " + this.GetDataDictionaryItem("PR", "YN_RECEIPT");
                        break;
                    case "YN_PAR":		// 병행여부
                        temp = temp + " + " + this.GetDataDictionaryItem("PR", "YN_PAR");
                        break;
                    case "YN_QC":		// 검사여부
                        temp = temp + " + " + this.GetDataDictionaryItem("PR", "YN_QC");
                        break;
                    case "QT_WIP":		// WIP수량
                        temp = temp + " + " + this.GetDataDictionaryItem("PR", "QT_WIP");
                        break;
                    case "QT_WORK":		// 실적수량
                        temp = temp + " + " + this.GetDataDictionaryItem("PR", "QT_WORK");
                        break;
                    case "QT_REJECT":	// 불량수량
                        temp = temp + " + " + this.GetDataDictionaryItem("PR", "QT_REJECT");
                        break;
                    case "QT_REWORK":	// 재작업
                        temp = temp + " + " + this.GetDataDictionaryItem("PR", "QT_REWORK");
                        break;
                    case "QT_MOVE":		// 이동수량
                        temp = temp + " + " + this.GetDataDictionaryItem("PR", "QT_MOVE");
                        break;
                    case "DT_NEED":		// 소요일자
                        temp = temp + " + " + this.GetDataDictionaryItem("PR", "DT_NEED");
                        break;
                    case "QT_NEED":		// 소요수량
                        temp = temp + " + " + this.GetDataDictionaryItem("PR", "QT_NEED");
                        break;
                    case "QT_REQ":		// 청구수량
                        temp = temp + " + " + this.GetDataDictionaryItem("PR", "QT_PRQ");
                        break;
                    case "QT_ISU":		// 불출수량
                        temp = temp + " + " + this.GetDataDictionaryItem("PR", "QT_ISU");
                        break;
                    case "QT_USE":		// 소모수량
                        temp = temp + " + " + this.GetDataDictionaryItem("PR", "QT_II");
                        break;
                    case "FG_GIR":		// 불출여부
                        temp = temp + " + " + this.GetDataDictionaryItem("MA", "FG_GIR");
                        break;
                    case "TP_OPPATH":		// 오더형태
                        temp = temp + " + " + this.GetDataDictionaryItem("PR", "TP_OPPATH");
                        break;
                    default:			// 기타
                        temp = temp + " + " + this.GetDataDictionaryItem("PR", colName[i]);
                        break;
                }
            }

            if (temp == "")
                return "";
            else
                return temp.Substring(3, temp.Length - 3);
        }

        #endregion

        #region -> OnPagePaint
        /// <summary>
        /// 페인트
        /// </summary>
        private void OnPagePaint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            try
            {
                if (_isPainted == false)
                {
                    this._isPainted = true;
                    InitGrid();
                    Application.DoEvents();

                    InitControl();
                    Application.DoEvents();

                    //시작일과 종료일 셋팅
                    m_dtFrom.Text = m_dtTo.Text = MainFrameInterface.GetStringToday;
                    Application.DoEvents();

                    SetControlEnabled(true);

                    //공정경로 버튼 설정
                    Check_Rout_Matl_Button("R", "T");
                    //자재소요 버튼 설정
                    Check_Rout_Matl_Button("M", "T");

                    _flex1.Redraw = false;
                    _flex1.Binding = _dsRoutBill.Tables[1].DefaultView;	//라우팅
                    _flex1.Redraw = true;

                    _flex2.Redraw = false;
                    _flex2.Binding = _dsRoutBill.Tables[2].DefaultView;	//자재소요
                    _flex2.Redraw = true;

                    this.Enabled = true;
                    this.Paint -= new System.Windows.Forms.PaintEventHandler(this.OnPagePaint);
                }
            }
            catch (Exception ex)
            {
                // 작업을 정상적으로 처리하지 못했습니다.
                this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
                return;
            }
            finally
            {
                ToolBarDeleteButtonEnabled = false;
                ToolBarSaveButtonEnabled = ToolBarSearchButtonEnabled = ToolBarAddButtonEnabled = true;
                this.m_cboCdPlant.Focus();
            }
        }
        #endregion

        #region -> InitGrid
        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitGrid()
        {
            #region -> 경로정보 그리드
            Application.DoEvents();

            _flex1.Redraw = false;

            _flex1.Rows.Count = 1;
            _flex1.Rows.Fixed = 1;
            _flex1.Cols.Count = 12;
            _flex1.Cols.Fixed = 1;
            _flex1.Rows.DefaultSize = 20;

            _flex1[0, 0] = " ";
            _flex1[0, 1] = "S";		// 체크박스 선택(S)
            _flex1.Cols[0].Width = 35;

            // 체크박스 선택(S)
            _flex1.Cols[1].Name = "CHK";
            _flex1.Cols[1].DataType = typeof(string);
            _flex1.Cols[1].Width = 20;
            _flex1.Cols[1].TextAlign = TextAlignEnum.CenterCenter;
            _flex1.Cols[1].AllowEditing = true;
            _flex1.Cols[1].Format = "T;F";
            _flex1.Cols[1].ImageAlign = ImageAlignEnum.CenterCenter;

            // 공정순서(순번)
            _flex1.Cols[2].Name = "CD_OP";
            _flex1.Cols[2].DataType = typeof(string);
            _flex1.Cols[2].Width = 90;
            _flex1.Cols[2].AllowEditing = true;
            _flex1.SetColMaxLength("CD_OP", 4);

            // W/C(작업장)
            _flex1.Cols[3].Name = "CD_WC";
            _flex1.Cols[3].DataType = typeof(string);
            _flex1.Cols[3].Width = 90;
            _flex1.Cols[3].AllowEditing = true;
            _flex1.SetColMaxLength("CD_WC", 14);

            // W/C(작업장명)
            _flex1.Cols[4].Name = "NM_WC";
            _flex1.Cols[4].DataType = typeof(string);
            _flex1.Cols[4].Width = 140;
            _flex1.Cols[4].AllowEditing = false;

            // 공정코드
            _flex1.Cols[5].Name = "CD_WCOP";
            _flex1.Cols[5].DataType = typeof(string);
            _flex1.Cols[5].Width = 95;
            _flex1.Cols[5].AllowEditing = true;
            _flex1.SetColMaxLength("CD_WCOP", 20);

            // 공정명
            _flex1.Cols[6].Name = "NM_OP";
            _flex1.Cols[6].DataType = typeof(string);
            _flex1.Cols[6].Width = 120;
            _flex1.Cols[6].AllowEditing = false;

            // 공정별 지시수량
            _flex1.Cols[7].Name = "QT_WO";
            _flex1.Cols[7].DataType = typeof(double);
            _flex1.Cols[7].Width = 120;
            _flex1.Cols[7].TextAlign = TextAlignEnum.RightCenter;
            _flex1.Cols[7].Format = "#,##0.####";
            //_flex1.Cols[7].Format = this.GetFormatDescription(DataDictionaryTypes.PR, FormatTpType.MONEY, FormatFgType.INSERT);
            _flex1.Cols[7].AllowEditing = true;
            _flex1.SetColMaxLength("QT_WO", 17);

            // M/S
            _flex1.Cols[8].Name = "YN_RECEIPT";
            _flex1.Cols[8].DataType = typeof(string);
            _flex1.Cols[8].Width = 70;
            _flex1.Cols[8].AllowEditing = false;
            _flex1.Cols[8].Visible = false;

            // 검사여부
            _flex1.Cols[9].Name = "YN_QC";
            _flex1.Cols[9].DataType = typeof(string);
            _flex1.Cols[9].Width = 70;
            _flex1.Cols[9].AllowEditing = true;

            // 최종실적
            _flex1.Cols[10].Name = "YN_FINAL";
            _flex1.Cols[10].DataType = typeof(string);
            _flex1.Cols[10].Width = 70;
            _flex1.Cols[10].AllowEditing = true;

            // TP_OPPATH
            _flex1.Cols[11].Name = "TP_OPPATH";
            _flex1.Cols[11].DataType = typeof(string);
            _flex1.Cols[11].Width = 70;
            _flex1.Cols[11].AllowEditing = false;
            _flex1.Cols[11].Visible = false;

            _flex1.AllowSorting = AllowSortingEnum.None;
            _flex1.NewRowEditable = false;
            _flex1.EnterKeyAddRow = true;

            _flex1.SumPosition = SumPositionEnum.None;

            // 그리드 헤더캡션 표시하기
            for (int i = 1; i <= _flex1.Cols.Count - 1; i++)
                _flex1[0, i] = GetDDItem(_flex1.Cols[i].Name);

            _flex1.Redraw = true;

            _flex1.AddRow += new System.EventHandler(this.insRoutButtonClick);
            _flex1.AfterDataRefresh += new System.ComponentModel.ListChangedEventHandler(this._flex_AfterDataRefresh);
            _flex1.HelpClick += new System.EventHandler(this.OnShowHelpGrid);
            _flex1.ValidateEdit += new C1.Win.C1FlexGrid.ValidateEditEventHandler(this._flex_ValidateEdit);
            #endregion

            #region -> 소요자재 그리드
            Application.DoEvents();

            _flex2.Redraw = false;

            _flex2.Rows.Count = 1;
            _flex2.Rows.Fixed = 1;
            _flex2.Cols.Count = 15;
            _flex2.Cols.Fixed = 1;
            _flex2.Rows.DefaultSize = 20;

            _flex2[0, 0] = " ";
            _flex2.Cols[0].Width = 35;

            // 체크박스 선택(S)
            _flex2.Cols[1].Name = "CHK";
            _flex2.Cols[1].DataType = typeof(string);
            _flex2.Cols[1].Width = 20;
            _flex2.Cols[1].TextAlign = TextAlignEnum.CenterCenter;
            _flex2.Cols[1].AllowEditing = true;
            _flex2.Cols[1].Format = "T;F";
            _flex2.Cols[1].ImageAlign = ImageAlignEnum.CenterCenter;

            // 공정순서명
            _flex2.Cols[2].Name = "CD_OP";
            _flex2.Cols[2].DataType = typeof(string);
            _flex2.Cols[2].Width = 80;
            _flex2.Cols[2].AllowEditing = true;
            _flex2.SetColMaxLength("CD_OP", 4);

            // W/C코드
            _flex2.Cols[3].Name = "CD_WC";
            _flex2.Cols[3].DataType = typeof(string);
            _flex2.Cols[3].Width = 80;
            _flex2.Cols[3].AllowEditing = true;
            _flex2.SetColMaxLength("CD_WC", 14);

            // W/C명
            _flex2.Cols[4].Name = "DIS_CD_WC";
            _flex2.Cols[4].DataType = typeof(string);
            _flex2.Cols[4].Width = 120;
            _flex2.Cols[4].AllowEditing = false;

            // 공정코드
            _flex2.Cols[5].Name = "CD_WCOP";
            _flex2.Cols[5].DataType = typeof(string);
            _flex2.Cols[5].Width = 95;
            _flex2.Cols[5].AllowEditing = true;
            _flex2.SetColMaxLength("CD_WCOP", 20);

            // 공정명
            _flex2.Cols[6].Name = "NM_OP";
            _flex2.Cols[6].DataType = typeof(string);
            _flex2.Cols[6].Width = 120;
            _flex2.Cols[6].AllowEditing = false;

            // 품목코드
            _flex2.Cols[7].Name = "CD_MATL";
            _flex2.Cols[7].DataType = typeof(string);
            _flex2.Cols[7].Width = 80;
            _flex2.Cols[7].AllowEditing = true;

            //품목명
            _flex2.Cols[8].Name = "NM_ITEM";
            _flex2.Cols[8].DataType = typeof(string);
            _flex2.Cols[8].Width = 120;
            _flex2.Cols[8].AllowEditing = false;

            //규격
            _flex2.Cols[9].Name = "STND_ITEM";
            _flex2.Cols[9].DataType = typeof(string);
            _flex2.Cols[9].Width = 80;
            _flex2.Cols[9].AllowEditing = false;

            // 소요수량
            _flex2.Cols[10].Name = "QT_NEED";
            _flex2.Cols[10].DataType = typeof(double);
            _flex2.Cols[10].Width = 70;
            _flex2.Cols[10].TextAlign = TextAlignEnum.RightCenter;
            _flex2.Cols[10].Format = "#,##0.####";
            //_flex2.Cols[10].Format = this.GetFormatDescription(DataDictionaryTypes.PR, FormatTpType.MONEY, FormatFgType.INSERT);
            _flex2.Cols[10].AllowEditing = true;
            _flex2.SetColMaxLength("QT_NEED", 17);

            // 단위
            _flex2.Cols[11].Name = "UNIT_IM";
            _flex2.Cols[11].DataType = typeof(string);
            _flex2.Cols[11].Width = 90;
            _flex2.Cols[11].AllowEditing = false;

            // 소요일자
            _flex2.Cols[12].Name = "DT_NEED";
            _flex2.Cols[12].DataType = typeof(string);
            _flex2.Cols[12].Width = 80;
            _flex2.Cols[12].TextAlign = TextAlignEnum.CenterCenter;
            _flex2.Cols[12].EditMask = this.GetFormatDescription(DataDictionaryTypes.PR, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            _flex2.Cols[12].Format = _flex2.Cols[12].EditMask;
            _flex2.SetStringFormatCol("DT_NEED");
            _flex2.SetNoMaskSaveCol("DT_NEED");

            // BF
            _flex2.Cols[13].Name = "YN_BF";
            _flex2.Cols[13].DataType = typeof(string);
            _flex2.Cols[13].Width = 70;
            _flex2.Cols[13].AllowEditing = true;

            // 불출여부
            _flex2.Cols[14].Name = "FG_GIR";
            _flex2.Cols[14].DataType = typeof(string);
            _flex2.Cols[14].Width = 60;
            _flex2.Cols[14].AllowEditing = false;



            _flex2.AllowSorting = AllowSortingEnum.None;
            _flex2.NewRowEditable = false;
            _flex2.EnterKeyAddRow = true;





            _flex2.SumPosition = SumPositionEnum.None;

            this.SetUserGrid(_flex2);

            // 그리드 헤더캡션 표시하기
            for (int i = 1; i <= _flex2.Cols.Count - 1; i++)
                _flex2[0, i] = GetDDItem(_flex2.Cols[i].Name);

            _flex2.Redraw = true;

            _flex2.AddRow += new System.EventHandler(this.insMatlButtonClick);
            _flex2.AfterDataRefresh += new System.ComponentModel.ListChangedEventHandler(this._flex_AfterDataRefresh);
            _flex2.ValidateEdit += new C1.Win.C1FlexGrid.ValidateEditEventHandler(this._flex_ValidateEdit);
            _flex2.HelpClick += new System.EventHandler(this.OnShowHelpGrid);
            #endregion
        }
        #endregion

        #region -> InitControl
        /// <summary>
        /// 헤더 콤보박스 셋팅
        /// </summary>
        private void InitControl()
        {
            try
            {
                m_lblCdPlant.Text = this.GetDDItem((string)m_lblCdPlant.Tag);		//공장명
                m_lblNoWo.Text = this.GetDDItem((string)m_lblNoWo.Tag);				//작업지시번호
                m_lblFgWo.Text = this.GetDDItem((string)m_lblFgWo.Tag);				//지시구분
                m_lblCdItem.Text = this.GetDDItem((string)m_lblCdItem.Tag);			//품목
                m_lblQtWo.Text = this.GetDDItem((string)m_lblQtWo.Tag);				//지시수량
                m_lblQtWork.Text = this.GetDDItem((string)m_lblQtWork.Tag);			//완료수량
                m_lblPeriodWork.Text = this.GetDDItem((string)m_lblPeriodWork.Tag); //작업기간
                m_lblStWo.Text = this.GetDDItem((string)m_lblStWo.Tag);				//오더상태
                m_lblNoLot.Text = this.GetDDItem((string)m_lblNoLot.Tag);			//LOT/Serial 번호
                m_lblNoTrack.Text = this.GetDDItem((string)m_lblNoTrack.Tag);		//Tracking 번호
                m_lblTpWo.Text = this.GetDDItem((string)m_lblTpWo.Tag);				//오더형태
                m_lblInfoBill.Text = m_tabMatl.Text = this.GetDDItem((string)m_lblInfoBill.Tag);//소요자재
                m_btnRespread_rout.Text = this.GetDDItem((string)m_btnRespread_rout.Tag);		//경로정보
                m_btnRespread_matl.Text = this.GetDDItem((string)m_btnRespread_matl.Tag);		//소요자재
                m_btnInsert_rout.Text = m_btnInsert_matl.Text = this.GetDDItem((string)m_btnInsert_rout.Tag);	//추가
                m_btnDelete_rout.Text = m_btnDelete_matl.Text = this.GetDDItem((string)m_btnDelete_rout.Tag);	//삭제
                Application.DoEvents();

                string[] args1 = { "CD_PLANT", "FLAG", "U", "U", "TP_WO", "Y_N", "PR" };
                string[] args2 = { "CD_PLANT", "PR_0000007", "PR_0000002", "PR_0000006", "TP_WO", "Y_N" };
                makeCommon mk = new makeCommon(args1, args2);
                DataSet _ds = mk.reDataSet(this.MainFrameInterface, this.LoginInfo.CompanyCode);
                if (_ds != null)
                {
                    // 공장
                    m_cboCdPlant.DataSource = _ds.Tables[0];
                    m_cboCdPlant.DisplayMember = "NAME";
                    m_cboCdPlant.ValueMember = "CODE";
                    if (this.LoginInfo.CdPlant.ToString() != "") m_cboCdPlant.SelectedValue = this.LoginInfo.CdPlant.ToString();

                    //작업지시구분(비계획만)
                    m_cboFgWo.DataSource = _ds.Tables[1].DefaultView;
                    m_cboFgWo.DisplayMember = "NAME";
                    m_cboFgWo.ValueMember = "CODE";
                    m_cboFgWo.SelectedValue = "003";

                    //오더형태
                    m_cboRout.DataSource = _ds.Tables[4];
                    m_cboRout.DisplayMember = "NAME";
                    m_cboRout.ValueMember = "CODE";
                    //if (m_cboRout.SelectedIndex != -1)
                    //    m_cboRout.SelectedIndex = 3;

                    //m_cboRout.SelectedIndex = 3; //디폴트 표준경로
                    //m_cboRout.SelectedValue = "S01";

                    // 오더상태
                    m_cboStWo.DataSource = _ds.Tables[3];
                    m_cboStWo.DisplayMember = "NAME";
                    m_cboStWo.ValueMember = "CODE";
                    m_cboStWo.SelectedIndex = 1; //확정 디폴트 0:마감 1:확정 2:계획 3:발행

                    //// 오더형태
                    //m_cboTpWo.DataSource = _ds.Tables[4];
                    //m_cboTpWo.DisplayMember = "NAME";
                    //m_cboTpWo.ValueMember = "CODE";		//오더형태 코드

                    _dtYnWork = _ds.Tables[4].Copy();
                    Application.DoEvents();
                    if (_ds.Tables[4] != null && _ds.Tables[4].Rows.Count > 0)
                    {
                        DataRow[] yn_de = _ds.Tables[4].Select("YN_DEFAULT = 'Y'");
                        //if(yn_de.Length > 0)
                        //    m_cboTpWo.SelectedValue = yn_de[0]["CODE"].ToString();
                    }
                    Application.DoEvents();

                    // M/S
                    _flex1.SetDataMap("YN_RECEIPT", _ds.Tables[5].Copy(), "CODE", "NAME");
                    _flex1.ShowButtons = ShowButtonsEnum.Always;

                    // 검사여부
                    _flex1.SetDataMap("YN_QC", _ds.Tables[5].Copy(), "CODE", "NAME");
                    _flex1.ShowButtons = ShowButtonsEnum.Always;

                    // 최종공정여부
                    _flex1.SetDataMap("YN_FINAL", _ds.Tables[5].Copy(), "CODE", "NAME");
                    _flex1.ShowButtons = ShowButtonsEnum.Always;

                    // 자재소요B/F
                    _flex2.SetDataMap("YN_BF", _ds.Tables[5].Copy(), "CODE", "NAME");
                    _flex2.ShowButtons = ShowButtonsEnum.Always;
                }
            }
            catch (Exception ex)
            {
                // 작업을 정상적으로 처리하지 못했습니다.
                this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
                return;
            }
        }
        #endregion

        #endregion

        #region ♣ 메인버튼 이벤트 / 메소드

        #region -> 조회조건체크

        /// <summary>
        /// 작업지시 헤드 부분의 필드 체크
        /// </summary>
        private bool SearchCondition()
        {
            try
            {
                if (this.m_dtFrom.Text.Replace("/", "").Replace("_", "") == "" || this.m_dtTo.Text.Replace("/", "").Replace("_", "") == "")
                {
                    //공장은 필수 입력입니다.
                    this.ShowMessage("WK1_004", this.m_lblPeriodWork.Text.ToString());
                    this.m_dtFrom.Focus();
                    return false;
                }

                if (this.m_txtCdItem.CodeValue == "")
                {
                    //품목은 필수 입력입니다.
                    this.ShowMessage("WK1_004", this.m_lblCdItem.Text.ToString());
                    this.m_txtCdItem.Focus();
                    return false;
                }

                if (this.m_cboRout.SelectedValue.ToString() == "")
                {
                    //공정경로가 존재하지 않습니다.
                    this.ShowMessage("PR_M100067");
                    this.m_cboRout.Focus();
                    return false;
                }

                try
                {
                    if (this.m_cboPath.SelectedValue.ToString() == "")
                    {
                        //공정경로의 내역이 존재하지 않습니다.
                        this.ShowMessage("PR_M100056", this.GetDDItem("ROUT_PATH"));
                        this.m_cboPath.Focus();
                        return false;
                    }
                }
                catch
                {
                    //공정경로의 내역이 존재하지 않습니다.
                    this.ShowMessage("PR_M100056", this.GetDDItem("ROUT_PATH"));
                    this.m_cboPath.Focus();
                    return false;
                }

                double d = 0;
                if (m_ctxtQtWo.Text.ToString() != "" && m_ctxtQtWo.Text != null)
                    d = Convert.ToDouble(m_ctxtQtWo.Text.ToString());

                if (d <= 0)
                {
                    // 지시수량은 0보다 커야 합니다.
                    this.ShowMessage("WK1_004", this.m_lblQtWo.Text.ToString());
                    this.m_ctxtQtWo.Focus();
                    return false;
                }
            }
            catch (Exception ex)
            {
                // 작업을 정상적으로 처리하지 못했습니다.
                this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
                return false;
            }

            return true;
        }
        #endregion

        #region -> 조회
        /// <summary>
        /// 조회
        /// </summary>
        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            //this.m_lblTitle.Focus();				

            if (!DoContinue1())
                return;

            if (!DoContinue2())
                return;

            try
            {
                if (!MsgAndSave(true, false))
                    return;

                //작업지시도움창
                object dlg = this.LoadHelpWindow("P_PR_WO_SUB", new object[5] {this.MainFrameInterface,
                                                                                  this.m_cboCdPlant.SelectedValue.ToString(), 
                                                                                  this.m_cboStWo.SelectedValue.ToString(),
                                                                                  (DataTable)m_cboCdPlant.DataSource, 
                                                                                  (DataTable)m_cboStWo.DataSource });

                if (((Duzon.Common.Forms.CommonDialog)dlg).ShowDialog() == DialogResult.OK)
                {
                    Application.DoEvents();

                    if (dlg is IHelpWindow)
                    {
                        if (_flex1.DataTable != null || _flex2.DataTable != null)
                        {
                            _flex1.DataTable.Rows.Clear();
                            _flex2.DataTable.Rows.Clear();
                        }

                        DataRow[] _drH = (DataRow[])(((IHelpWindow)dlg).ReturnValues)[0];
                        if (_drH.Length < 1)
                            return;

                        _dsRoutBill.Tables[0].Clear();
                        DataRow _dr = _dsRoutBill.Tables[0].NewRow();
                        _dr["CD_COMPANY"] = this.MainFrameInterface.LoginInfo.CompanyCode;
                        _dr["NO_WO"] = _drH[0]["NO_WO"].ToString();
                        _dsRoutBill.Tables[0].Rows.Add(_dr);

                        _dsRoutBill.Tables[0].Rows[0]["NO_LOT"] = this.m_txtNoLot.Text;						//Lot/Seriol번호
                        _dsRoutBill.Tables[0].Rows[0]["NO_PJT"] = "";
                        _dsRoutBill.Tables[0].Rows[0]["ID_INSERT"] = this.LoginInfo.UserID;
                        _dsRoutBill.Tables[0].Rows[0]["NO_EMP"] = this.LoginInfo.UserID;
                        _dsRoutBill.Tables[0].Rows[0]["CD_PLANT"] = this.m_cboCdPlant.SelectedValue = _drH[0]["CD_PLANT"].ToString();//공장코드
                        _dsRoutBill.Tables[0].Rows[0]["NO_WO"] = this.m_txtNoWo.Text = _drH[0]["NO_WO"].ToString();				//작업지시번호
                        _dsRoutBill.Tables[0].Rows[0]["FG_WO"] = this.m_cboFgWo.SelectedValue = _drH[0]["FG_WO"].ToString();		//작업지시구분
                        _dsRoutBill.Tables[0].Rows[0]["CD_ITEM"] = this.m_txtCdItem.CodeValue = _drH[0]["CD_ITEM"].ToString();		//품목코드
                        this.m_txtCdItem.CodeName = _drH[0]["NM_ITEM"].ToString();		//품목명
                        this.m_txtStndItem.Text = _drH[0]["STND_ITEM"].ToString();		//규격
                        this.m_txtUnitIm.Text = _drH[0]["UNIT_MO"].ToString();			//단위
                        _dsRoutBill.Tables[0].Rows[0]["QT_ITEM"] = this.m_ctxtQtWo.Text = _drH[0]["QT_ITEM"].ToString();			//지시수량
                        _dsRoutBill.Tables[0].Rows[0]["DT_REL"] = this.m_dtFrom.Text = _drH[0]["DT_REL"].ToString();				//시작일
                        _dsRoutBill.Tables[0].Rows[0]["DT_DUE"] = this.m_dtTo.Text = _drH[0]["DT_DUE"].ToString();				//종료일
                        _dsRoutBill.Tables[0].Rows[0]["TP_ROUT"] = this.m_cboRout.SelectedValue = _drH[0]["TP_ROUT"].ToString();	//작업경로
                        _dsRoutBill.Tables[0].Rows[0]["PATN_ROUT"] = _dsRoutBill.Tables[0].Rows[0]["TP_OPPATH"] = this.m_cboPath.SelectedValue = _drH[0]["PATN_ROUT"].ToString(); //작업경로PATH
                        _dsRoutBill.Tables[0].Rows[0]["ST_WO"] = this.m_cboStWo.SelectedValue = _drH[0]["ST_WO"].ToString();		//오더상태
                        _dsRoutBill.Tables[0].Rows[0]["NO_SO"] = this.m_txtNoSo.Text = _drH[0]["NO_SO"].ToString();				//수주번호
                        _dsRoutBill.Tables[0].Rows[0]["NO_LINE_SO"] = this.m_txtNoLineSo.Text = _drH[0]["NO_LINE_SO"].ToString();		//수주번호항번
                        //_dsRoutBill.Tables[0].Rows[0]["TP_WO"] = this.m_cboTpWo.SelectedValue = _drH[0]["TP_WO"].ToString();		//오더형태
                        this._stFgBf = _drH[0]["FG_BF"].ToString();						//B/F여부
                        this._stFG_GIR = _drH[0]["FG_GIR"].ToString();						//불출여부 

                        _dsRoutBill.Tables[0].Rows[0]["TP_GI"] = _drH[0]["TP_GI"].ToString().Trim();
                        _dsRoutBill.Tables[0].Rows[0]["TP_GR"] = _drH[0]["TP_GR"].ToString().Trim();

                        _dsRoutBill.Tables[0].AcceptChanges();
                        Application.DoEvents();


                        //라우팅,자재소요 detail 조회
                        this.SelectDetail();

                        this.SetControlEnabled(false);

                        //추가
                        this.HeaderEn(false);

                        //오더상태가 !확정
                        //if(m_cboStWo.SelectedValue.ToString() != "F")
                        if (m_cboStWo.SelectedValue.ToString() != "O")
                        {
                            Check_Rout_Matl_Button("R", "F");	//공정경로 버튼 비활성
                            Check_Rout_Matl_Button("M", "F");	//자재소요 버튼 비활성	

                            _flex1.AllowEditing = _flex2.AllowEditing = false;
                        }
                        else
                        {
                            Check_Rout_Matl_Button("R", "T");	//공정경로 버튼 활성
                            Check_Rout_Matl_Button("M", "T");	//자재소요 버튼 활성

                            _flex1.AllowEditing = _flex2.AllowEditing = true;
                        }

                        ToolBarDeleteButtonEnabled = true;
                        ToolBarSaveButtonEnabled = false;
                        Application.DoEvents();
                    }
                }
            }
            catch (Exception ex)
            {
                // 작업을 정상적으로 처리하지 못했습니다.
                this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
                return;
            }
        }

        /// <summary>
        /// 디테일 조회한다.
        /// </summary>
        private void SelectDetail()
        {
            try
            {
                Duzon.Common.Util.SpInfoCollection sc = new Duzon.Common.Util.SpInfoCollection();

                for (int i = 0; i < 3; i++)
                {
                    Duzon.Common.Util.SpInfo si = new Duzon.Common.Util.SpInfo();
                    if (i == 0)
                    {
                        object[] obj = new Object[41];
                        si.SpNameSelect = "UP_PR_WO_ROUT";//경로정보
                        obj[0] = "S";
                        obj[1] = this.LoginInfo.CompanyCode;
                        obj[2] = this.m_txtNoWo.Text.ToString();
                        obj[5] = this.m_cboCdPlant.SelectedValue.ToString();
                        si.SpParamsSelect = obj;
                    }
                    else if (i == 1)
                    {
                        object[] obj = new Object[21];
                        si.SpNameSelect = "UP_PR_WO_BILL";//소요자재정보
                        obj[0] = "S1";
                        obj[1] = this.LoginInfo.CompanyCode;
                        obj[2] = this.m_txtNoWo.Text.ToString();
                        obj[4] = this.m_cboCdPlant.SelectedValue.ToString();
                        si.SpParamsSelect = obj;
                    }
                    else
                    {
                        object[] obj = new Object[6];
                        si.SpNameSelect = "UP_PR_COMMON";//품목에따른 공정경로 조회 -> "PR_ROUT"
                        obj[0] = this.LoginInfo.CompanyCode;
                        obj[1] = this.m_cboCdPlant.SelectedValue.ToString();
                        obj[2] = "NO_OPPATH";
                        obj[3] = this.m_txtCdItem.CodeValue;
                        si.SpParamsSelect = obj;
                    }

                    sc.Add(si);
                }

                DataSet _dsInfo = (DataSet)this.FillDataSet(sc);
                _dsInfo.Tables[0].TableName = "PR_WO_ROUT";
                _dsInfo.Tables[1].TableName = "PR_WO_BILL";
                _dsInfo.Tables[2].TableName = "PR_ROUT";

                //공정경로PATH Setting...
                this.set_RoutPath_Combo(_dsInfo.Tables["PR_ROUT"]);
                Application.DoEvents();

                if (_dsInfo != null && _dsInfo.Tables.Count > 1)
                {
                    if (_dsInfo.Tables[0].Rows.Count > 0)
                    {
                        this.SetDataGridBinding_Rout(_dsInfo.Tables["PR_WO_ROUT"]);		//라우팅 
                    }
                    if (_dsInfo.Tables[1].Rows.Count > 0)
                    {
                        this.SetDataGridBinding_Matl(_dsInfo.Tables["PR_WO_BILL"]);		//자재소요
                    }
                }
            }
            catch (Exception ex)
            {
                // 작업을 정상적으로 처리하지 못했습니다.
                this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
                return;
            }
        }
        #endregion

        #region -> 추가

        #region ->-> 전표추가
        /// <summary>
        /// 브라우저의 추가 버턴 클릭시 처리부
        /// </summary>
        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            if (!DoContinue1())
                return;

            if (!DoContinue2())
                return;

            //this.m_lblTitle.Focus();	

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                // 변경된 내용 조사 후 변경된 내용 있을 경우 저장
                if (!MsgAndSave(true, false))
                    return;

                this.ShowStatusBarMessage(0);

                InitiDataValue();					//컨트롤초기화
                SetControlEnabled(true);
                Check_Rout_Matl_Button("R", "T");	//공정경로 버튼 활성
                Check_Rout_Matl_Button("M", "T");	//자재소요 버튼 활성
                Application.DoEvents();

                m_txtCdItem.Focus();
                ToolBarDeleteButtonEnabled = false;
                ToolBarSaveButtonEnabled = true;

                this.ShowStatusBarMessage(4);
            }
            catch (Exception ex)
            {
                // 작업을 정상적으로 처리하지 못했습니다.
                this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
                return;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        #endregion

        #region ->-> 경로추가
        /// <summary>
        /// 경로추가
        /// </summary>
        private void insRoutButtonClick(object sender, System.EventArgs e)
        {
            if (!DoContinue1())
                return;

            if (!DoContinue2())
                return;

            try
            {
                if (!SearchCondition())
                    return;

                this.ShowStatusBarMessage(0);

                _flex1.Rows.Add();
                _flex1.Row = _flex1.Rows.Count - 1;

                _flex1[_flex1.Row, "CHK"] = _flex1[_flex1.Row, "YN_BF"] = "N";
                _flex1[_flex1.Row, "QT_WO"] = _flex1[_flex1.Row, "QT_WIP"] = _flex1[_flex1.Row, "QT_WORK"] =
                _flex1[_flex1.Row, "QT_REJECT"] = _flex1[_flex1.Row, "QT_REWORK"] = _flex1[_flex1.Row, "QT_MOVE"] = 0;

                //M/S 여부 셋팅
                if (_dtYnWork != null && _dtYnWork.Rows.Count > 0)
                {
                    DataRow[] yn_work = _dtYnWork.Select("CODE = '" + this.m_cboRout.SelectedValue.ToString() + "'");
                    if (yn_work.Length > 0)
                    {
                        if (yn_work[0]["YN_WORK"].ToString() != "" && yn_work[0]["YN_WORK"] != null)
                            _flex1[_flex1.Row, "YN_RECEIPT"] = yn_work[0]["YN_WORK"].ToString();
                        else
                            _flex1[_flex1.Row, "YN_RECEIPT"] = "N";
                    }
                    else
                    {
                        _flex1[_flex1.Row, "YN_RECEIPT"] = "N";
                    }
                }

                //병행여부,검사여부,최종실적
                _flex1[_flex1.Row, "ST_OP"] = _flex1[_flex1.Row, "YN_PAR"] =
                _flex1[_flex1.Row, "YN_QC"] = _flex1[_flex1.Row, "YN_FINAL"] = "N";
                _flex1[_flex1.Row, "CD_COMPANY"] = this.LoginInfo.CompanyCode;
                _flex1[_flex1.Row, "CD_PLANT"] = this.m_cboCdPlant.SelectedValue.ToString();
                _flex1[_flex1.Row, "NO_WO"] = this.m_txtNoWo.Text.ToString();
                _flex1[_flex1.Row, "DT_REL"] = this.m_dtFrom.Text.ToString().Replace("/", "").Replace("_", "");
                _flex1[_flex1.Row, "DT_DUE"] = this.m_dtTo.Text.ToString().Replace("/", "").Replace("_", "");
                _flex1[_flex1.Row, "NO_LINE"] = _flex1.Row;
                _flex1[_flex1.Row, "QT_WO"] = this.m_ctxtQtWo.Text.ToString();
                _flex1.AddFinished();

                _flex1.Col = _flex1.Cols.Fixed;
                _flex1.Focus();

                //최종실적셋팅
                if (_flex1.HasNormalRow)
                {
                    int cnt = _flex1.FindRow("YES", _flex1.Rows.Fixed, _flex1.Cols["YN_FINAL"].Index, false, true, true);
                    if (cnt > 0)
                    {
                        _flex1[cnt, "YN_FINAL"] = "N";
                        _flex1[_flex1.Row, "YN_FINAL"] = "Y";
                    }
                }

                this.ShowStatusBarMessage(4);
            }
            catch (Exception ex)
            {
                // 작업을 정상적으로 처리하지 못했습니다.
                this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
                return;
            }
        }
        #endregion

        #region ->-> 소요자재추가
        /// <summary>
        /// 소요자재추가
        /// </summary>
        private void insMatlButtonClick(object sender, System.EventArgs e)
        {
            if (!DoContinue1())
                return;

            if (!DoContinue2())
                return;

            try
            {
                if (!SearchCondition())
                    return;

                this.ShowStatusBarMessage(0);

                _flex2.Rows.Add();
                _flex2.Row = _flex2.Rows.Count - 1;

                _flex2[_flex2.Row, "CHK"] = "N";

                if (_flex1.HasNormalRow)
                    _flex2[_flex2.Row, "CD_OP"] = _flex1[_flex1.Row, "CD_OP"].ToString();

                _flex2[_flex2.Row, "QT_NEED"] = _flex2[_flex2.Row, "QT_REQ"] = _flex2[_flex2.Row, "QT_ISU"] =
                _flex2[_flex2.Row, "QT_USE"] = _flex2[_flex2.Row, "QT_NEED_NET"] = 0;
                _flex2[_flex2.Row, "YN_BF"] = this._stFgBf; //BF
                _flex2[_flex2.Row, "FG_GIR"] = this._stFG_GIR; //불출여부
                _flex2[_flex2.Row, "DT_NEED"] = this.MainFrameInterface.GetStringToday;
                _flex2[_flex2.Row, "NO_LINE"] = _flex2.Row;
                _flex2[_flex2.Row, "CD_COMPANY"] = this.LoginInfo.CompanyCode;
                _flex2[_flex2.Row, "CD_PLANT"] = this.m_cboCdPlant.SelectedValue.ToString();
                _flex2[_flex2.Row, "NO_WO"] = this.m_txtNoWo.Text.ToString();
                _flex2.AddFinished();

                _flex2.Col = _flex2.Cols.Fixed;
                _flex2.Focus();

                this.ShowStatusBarMessage(4);
            }
            catch (Exception ex)
            {
                // 작업을 정상적으로 처리하지 못했습니다.
                this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
                return;
            }
        }
        #endregion

        #endregion

        #region -> 삭제

        #region ->-> 전표삭제
        /// <summary>
        /// 브라우저의 삭제 버턴 클릭시 처리부
        /// </summary>
        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            if (!DoContinue1())
                return;

            if (!DoContinue2())
                return;

            try
            {
                //오더상태를 체크
                //				if(this.m_cboStWo.SelectedValue.ToString() != "F")
                //				{
                //					//확정이 아닌 오더 상태는 삭제 하실수 없습니다!
                //					MessageBoxEx.Show(m_cboStWo.SelectedText.ToString() + this.MainFrameInterface.GetMessageDictionaryItem("PR_M100033"),this.PageName);
                //					return;
                //				}

                if (this.m_txtNoWo.Text == string.Empty)
                {
                    this.ShowMessage("WK1_004", this.m_lblNoWo.Text);
                    return;
                }

                DialogResult result = this.ShowMessageBox(1003, this.PageName);

                if (result == DialogResult.Yes)
                {
                    this.ShowStatusBarMessage(0);

                    object[] obj = new object[25];
                    obj[0] = "D1";
                    obj[1] = this.LoginInfo.CompanyCode;
                    obj[2] = this.m_txtNoWo.Text.ToString();
                    obj[3] = this.m_cboCdPlant.SelectedValue.ToString();

                    ResultData ret = (ResultData)this.ExecSp("UP_PR_WO", obj);

                    if (ret.Result == true)
                    {
                        //모든컨트롤을 초기화
                        InitiDataValue();

                        _dsRoutBill.Tables[0].AcceptChanges();
                        _flex1.DataTable.AcceptChanges();
                        _flex2.DataTable.AcceptChanges();

                        ToolBarSaveButtonEnabled = false;
                        Check_Rout_Matl_Button("R", "F");	//공정경로 버튼 비활성
                        Check_Rout_Matl_Button("M", "F");	//자재소요 버튼 비활성

                        this.ShowStatusBarMessage(5);

                        MessageBoxEx.Show(this.GetMessageDictionaryItem("CM_M000009"), this.PageName);

                        // 삭제후 전표를 추가 한다.
                        SetControlEnabled(true);
                        Check_Rout_Matl_Button("R", "T");	//공정경로 버튼 활성
                        Check_Rout_Matl_Button("M", "T");	//자재소요 버튼 활성
                        ToolBarSaveButtonEnabled = true;
                        ToolBarDeleteButtonEnabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                // 작업을 정상적으로 처리하지 못했습니다.
                this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
                return;
            }
        }
        #endregion

        #region ->-> 경로삭제
        /// <summary>
        /// 경로삭제
        /// </summary>
        private void m_btn_delete_Rout_Click(object sender, System.EventArgs e)
        {
            if (!DoContinue1())
                return;

            if (!DoContinue2())
                return;

            try
            {
                if (!_flex1.HasNormalRow)
                    return;

                this.ShowStatusBarMessage(0);

                int cnt = _flex1.FindRow("T", _flex1.Rows.Fixed, _flex1.Cols["CHK"].Index, false, true, true);
                if (cnt < 1)
                {
                    _flex1.Rows.Remove(_flex1.Row);
                }
                else
                {
                    int i = cnt;
                    while (i < _flex1.Rows.Count && i > 0)
                    {
                        if (_flex1[i, "CHK"].ToString() == "T")
                        {
                            _flex1.Rows.Remove(i);

                            i = _flex1.FindRow("T", i, _flex1.Cols["CHK"].Index, false, true, true);
                        }
                    }
                }

                //최종실적셋팅
                if (_flex1.HasNormalRow)
                {
                    int c = _flex1.FindRow("YES", _flex1.Rows.Fixed, _flex1.Cols["YN_FINAL"].Index, false, true, true);
                    if (c < 1)
                    {
                        _flex1[_flex1.Rows.Count - 1, "YN_FINAL"] = "Y";
                    }
                }

                this.ShowStatusBarMessage(5);
            }
            catch (Exception ex)
            {
                // 작업을 정상적으로 처리하지 못했습니다.
                this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
                return;
            }
        }
        #endregion

        #region ->-> 소요자재삭제

        /// <summary>
        /// 소요자재삭제
        /// </summary>
        private void m_btn_delete_matl_Click(object sender, System.EventArgs e)
        {
            if (!DoContinue1())
                return;

            if (!DoContinue2())
                return;

            try
            {
                if (!_flex2.HasNormalRow)
                    return;

                this.ShowStatusBarMessage(0);

                int cnt = _flex2.FindRow("T", _flex2.Rows.Fixed, _flex2.Cols["CHK"].Index, false, true, true);
                if (cnt < 1)
                {
                    _flex2.Rows.Remove(_flex2.Row);
                }
                else
                {
                    int i = cnt;
                    while (i < _flex2.Rows.Count && i > 0)
                    {
                        if (_flex2[i, "CHK"].ToString() == "T")
                        {
                            _flex2.Rows.Remove(i);

                            i = _flex2.FindRow("T", i, _flex2.Cols["CHK"].Index, false, true, true);
                        }
                    }
                }

                this.ShowStatusBarMessage(5);
            }
            catch (Exception ex)
            {
                // 작업을 정상적으로 처리하지 못했습니다.
                this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
                return;
            }
        }
        #endregion

        #endregion

        #region -> 저장

        #region -> DoContinue

        private bool DoContinue1()
        {
            if (_flex1.Editor != null)
            {
                return _flex1.FinishEditing(false);
            }

            return true;
        }

        private bool DoContinue2()
        {
            if (_flex2.Editor != null)
            {
                return _flex2.FinishEditing(false);
            }

            return true;
        }

        #endregion

        #region -> IsChanged
        /// <summary>
        /// 변경유무
        /// </summary>
        /// <param name="gubun"></param>
        /// <returns></returns>
        private bool IsChanged(string gubun)
        {
            if (gubun == null)
                return _flex1.IsDataChanged || _flex2.IsDataChanged || _dsRoutBill.Tables[0].GetChanges() != null;
            if (gubun == "_flex1")
                return _flex1.IsDataChanged;
            if (gubun == "_flex2")
                return _flex2.IsDataChanged;

            return false;
        }
        #endregion

        #region -> MsgAndSave

        private bool MsgAndSave(bool displayDialog, bool isExit)
        {
            if (!IsChanged(null)) return true;

            bool isSaved = false;

            if (!displayDialog)								// 저장 버튼을 클릭한 경우이므로 다이알로그는 필요없음
            {
                if (IsChanged(null)) isSaved = Save();

                return isSaved;
            }


            DialogResult result;

            if (isExit)
            {
                // 변경된 내용이 있습니다. 저장하시겠습니까?
                result = this.ShowMessage("QY3_002");
                if (result == DialogResult.No)
                    return true;
                if (result == DialogResult.Cancel)
                    return false;
            }
            else
            {
                // 변경된 내용이 있습니다. 저장하시겠습니까?
                result = this.ShowMessage("QY2_001");
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

            // 필요없는 행 삭제
            if (!_flex1.CheckTable_DeleteIfNull(new string[] { "CD_OP", "CD_WC", "CD_WCOP" }, "AND") && !_flex2.CheckTable_DeleteIfNull(new string[] { "CD_OP", "CD_WC", "CD_MATL" }, "AND") && _dsRoutBill.Tables[0].GetChanges() == null)
            {
                // 변경된 내용이 없습니다.
                this.ShowMessage("IK1_013");
                return false;
            }

            Application.DoEvents();

            if (_flex1.HasNormalRow)
            {
                // 필수입력항목 체크
                if (_flex1.CheckView_HasNull(new string[] { "CD_OP", "CD_WC", "YN_FINAL" }, out row, out colName, "OR"))
                {
                    this.ShowMessage("WK1_004", GetDDItem(colName));
                    _flex1.Select(row, colName);
                    _flex1.Focus();
                    return false;
                }

                DataRow[] yn_fi = _flex1.DataTable.Select("YN_FINAL = 'Y'");
                if (yn_fi.Length != 1)
                {
                    //최종실적이 'Y'인 공정은 1개만 존재합니다.
                    this.ShowMessage("PR_M100066");
                    _flex1.Focus();
                    return false;
                }
            }

            if (_flex2.HasNormalRow)
            {
                // 필수입력항목 체크
                if (_flex2.CheckView_HasNull(new string[] { "CD_OP", "CD_WC", "CD_MATL" }, out row, out colName, "OR"))
                {
                    this.ShowMessage("WK1_004", GetDDItem(colName));
                    _flex2.Select(row, colName);
                    _flex2.Focus();
                    return false;
                }

                DataRow[] _r = _flex2.DataTable.Select("QT_NEED > 9999999999999.9999");
                if (_r.Length > 0)
                {
                    this.ShowMessage("PR_M110027");
                    _flex2.Select(_flex2.FindRow(_r[0]["CD_MATL"].ToString(), _flex2.Rows.Fixed, _flex2.Cols["CD_MATL"].Index, false, true, true), "QT_NEED");
                    _flex2.Focus();
                    return false;
                }
            }

            if (_flex1.HasNormalRow)
            {
                // 중복값 체크
                if (_flex1.CheckTable_HasDup(new string[] { "CD_OP" }, out row))
                {
                    this.ShowMessage("WK1_001", GetDDItem("CD_OP"));
                    _flex1.Select(row, "CD_OP");
                    _flex1.Focus();
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region -> 헤더 데이터 저장
        /// <summary>
        /// 작업지시 헤더
        /// </summary>
        private bool InDataHeadValue()
        {

            try
            {
                /*********** 채번해온다 *************/
                try
                {
                    if (this.m_txtNoWo.Text.ToString().Replace(" ", "") == "")
                    {
                        string no = "";

                        no = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PR", "03");

                        if (no == "" || no == null)
                        {
                            //채번하는 중 에러가 발생했습니다.
                            this.ShowMessage("PR_M100040");
                            return false;
                        }
                        else
                        {
                            this.m_txtNoWo.Text = no;

                            _dsRoutBill.Tables[0].Clear();

                            DataRow _dr = _dsRoutBill.Tables[0].NewRow();

                            _dr["NO_WO"] = this.m_txtNoWo.Text.ToString();
                            _dr["CD_COMPANY"] = this.MainFrameInterface.LoginInfo.CompanyCode;

                            _dsRoutBill.Tables[0].Rows.Add(_dr);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // 작업을 정상적으로 처리하지 못했습니다.
                    this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "", "", "MA_M000011");
                    return false;
                }
                /************************************/

                if (_dsRoutBill.Tables[0].Rows.Count > 0)
                {
                    if (_dsRoutBill.Tables[0].Rows[0].RowState.ToString() == "Added")
                    {
                        //MessageBox.Show(_dsRoutBill.Tables[0].Rows[0].RowState.ToString());

                        _dsRoutBill.Tables[0].BeginInit();
                        _dsRoutBill.Tables[0].Rows[0]["CD_PLANT"] = this.m_cboCdPlant.SelectedValue.ToString();
                        _dsRoutBill.Tables[0].Rows[0]["CD_ITEM"] = this.m_txtCdItem.CodeValue;
                        _dsRoutBill.Tables[0].Rows[0]["FG_WO"] = this.m_cboFgWo.SelectedValue.ToString();
                        _dsRoutBill.Tables[0].Rows[0]["QT_ITEM"] = this.m_ctxtQtWo.Text.ToString();
                        _dsRoutBill.Tables[0].Rows[0]["DT_REL"] = this.m_dtFrom.Text.ToString().Replace("/", "").Replace("_", "");
                        _dsRoutBill.Tables[0].Rows[0]["DT_DUE"] = this.m_dtTo.Text.ToString().Replace("/", "").Replace("_", "");
                        _dsRoutBill.Tables[0].Rows[0]["TP_ROUT"] = this.m_cboRout.SelectedValue.ToString();		//공정경로
                        _dsRoutBill.Tables[0].Rows[0]["PATN_ROUT"] = this.m_cboPath.SelectedValue.ToString();	//공정경로
                        _dsRoutBill.Tables[0].Rows[0]["TP_OPPATH"] = _dtOppath.Rows[0][3].ToString();// this.m_cboPath.SelectedValue.ToString();	//공정경로내역
                        _dsRoutBill.Tables[0].Rows[0]["ST_WO"] = this.m_cboStWo.SelectedValue.ToString();		//오더상태
                        _dsRoutBill.Tables[0].Rows[0]["NO_LOT"] = this.m_txtNoLot.Text;						//Lot/Seriol번호
                        _dsRoutBill.Tables[0].Rows[0]["NO_PJT"] = "";
                        _dsRoutBill.Tables[0].Rows[0]["NO_SO"] = this.m_txtNoSo.Text;						//수주번호
                        _dsRoutBill.Tables[0].Rows[0]["ID_INSERT"] = this.LoginInfo.UserID;
                        _dsRoutBill.Tables[0].Rows[0]["NO_EMP"] = this.LoginInfo.UserID;

                        DataTable dtTemp = m_cboRout.DataSource as DataTable;
                        _dsRoutBill.Tables[0].Rows[0]["TP_GI"] = dtTemp.Rows[m_cboRout.SelectedIndex]["TP_GI"];
                        _dsRoutBill.Tables[0].Rows[0]["TP_GR"] = dtTemp.Rows[m_cboRout.SelectedIndex]["TP_GR"];

                        if (m_txtNoLineSo.Text == string.Empty)
                            _dsRoutBill.Tables[0].Rows[0]["NO_LINE_SO"] = 0;
                        else
                            _dsRoutBill.Tables[0].Rows[0]["NO_LINE_SO"] = this.m_txtNoLineSo.Text;			//수주번호항번

                        //_dsRoutBill.Tables[0].Rows[0]["TP_WO"] = this.m_cboTpWo.SelectedValue.ToString();	//오더형태				
                        _dsRoutBill.Tables[0].EndInit();
                    }
                }
                Application.DoEvents();

            }
            catch (Exception ex)
            {
                // 작업을 정상적으로 처리하지 못했습니다.
                this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
                return false;
            }

            return true;
        }
        #endregion

        /// <summary>
        /// 저장 버튼 클릭시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            if (!DoContinue1())
                return;

            if (!DoContinue2())
                return;

            try
            {
                this.m_cboCdPlant.Focus();

                if (!SearchCondition())
                    return;

                //헤더DATA TABLE
                if (!InDataHeadValue())
                    return;

                if (MsgAndSave(false, false))
                {
                    // 저장되었습니다.
                    this.ShowMessage("IK1_001");
                    this.SetStatusBarMessage(this.GetMessageDictionaryItem("PR_M000001"));
                    this.ToolBarSaveButtonEnabled = false;
                }
            }
            catch (Exception ex)
            {
                // 작업을 정상적으로 처리하지 못했습니다.
                this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
            }
        }

        /// <summary>
        /// 저장루틴
        /// </summary>
        private bool Save()
        {

            if (!Check())
                return false;

            DataTable _dtRout = new DataTable();					//경로정보
            DataTable _dtMatl = new DataTable();					//소요자재
            DataTable _dtWo = _dsRoutBill.Tables[0].GetChanges();	//작업지시

            if (_flex1.DataTable != null)
            {
                #region /* M/S 여부 셋팅 */
                if (_dtYnWork != null && _dtYnWork.Rows.Count > 0)
                {
                    string work = "N"; //M/S 여부
                    DataRow[] yn_work = _dtYnWork.Select("CODE = '" + this.m_cboRout.SelectedValue.ToString() + "'");
                    if (yn_work.Length > 0)
                    {
                        if (yn_work[0]["YN_WORK"].ToString() != "" && yn_work[0]["YN_WORK"] != null)
                            work = yn_work[0]["YN_WORK"].ToString();
                    }

                    for (int i = 1; i < _flex1.Rows.Count; i++)
                    {
                        if (_flex1[i, "YN_RECEIPT"].ToString() != work)
                            _flex1[i, "YN_RECEIPT"] = work;
                    }
                }
                #endregion

                _dtRout = _flex1.DataTable.GetChanges();	//경로정보

                if (_dtRout != null)
                {
                    foreach (DataRow _dr in _dtRout.Rows)
                    {
                        if (_dr.RowState.ToString() == "Added")
                            _dr["NO_WO"] = this.m_txtNoWo.Text.ToString();
                    }
                }
            }

            if (_flex2.DataTable != null)
            {
                _dtMatl = _flex2.DataTable.GetChanges();	//소요자재

                if (_dtMatl != null)
                {
                    foreach (DataRow _dr in _dtMatl.Rows)
                    {
                        if (_dr.RowState.ToString() == "Added")
                            _dr["NO_WO"] = this.m_txtNoWo.Text.ToString();
                    }
                }
            }

            //MessageBox.Show(_dsRoutBill.Tables[0].Rows[0].RowState.ToString());

            string ins_flag = "I";
            //바로저장을 누르면 자동으로 경로전개, 품목전개를 해준다.
            if (_dtRout == null && _dtMatl == null && _dsRoutBill.Tables[0].Rows[0].RowState.ToString() == "Added")
            {
                ins_flag = "I1";

                _dtRout = _flex1.DataTable.Clone();
                _dtMatl = _flex2.DataTable.Clone();

                DataRow _drR = _dtRout.NewRow();
                _drR["NO_WO"] = this.m_txtNoWo.Text.ToString();
                _drR["CD_COMPANY"] = this.MainFrameInterface.LoginInfo.CompanyCode;
                _drR["CD_PLANT"] = this.m_cboCdPlant.SelectedValue.ToString();
                _drR["CD_WC"] = this.m_cboPath.SelectedValue.ToString();
                _drR["CD_TOOL"] = this.m_txtCdItem.CodeValue;
                _drR["QT_WO"] = Convert.ToDouble(m_ctxtQtWo.Text);
                _drR["DT_REL"] = this.m_dtFrom.Text.ToString().Replace("/", "").Replace("_", "");
                _drR["DT_DUE"] = this.m_dtTo.Text.ToString().Replace("/", "").Replace("_", "");
                _drR["TP_OPPATH"] = _dtOppath.Rows[0][3].ToString(); //m_cboRout.SelectedValue.ToString();
                _dtRout.Rows.Add(_drR);

                DataRow _drM = _dtMatl.NewRow();
                _drM["NO_WO"] = this.m_txtNoWo.Text.ToString();
                _drM["CD_COMPANY"] = this.MainFrameInterface.LoginInfo.CompanyCode;
                _drM["CD_PLANT"] = this.m_cboCdPlant.SelectedValue.ToString();
                _drM["DT_NEED"] = this.m_dtFrom.Text.ToString().Replace("/", "").Replace("_", "");
                _drM["QT_NEED"] = this.m_ctxtQtWo.Text.ToString();
                _drM["YN_BF"] = this._stFgBf;
                _drM["FG_GIR"] = this._stFG_GIR;
                _drM["CD_MATL"] = this.m_txtCdItem.CodeValue;
                _drM["NO_REQ"] = this.m_cboPath.SelectedValue.ToString();
                _drM["TP_OPPATH"] = _dtOppath.Rows[0][3].ToString(); //m_cboRout.SelectedValue.ToString();
                _dtMatl.Rows.Add(_drM);
            }

            //foreach (DataRow drMatl in _dtMatl.Rows)
            //{
            //    DataRow[] drArr = _dtRout.Select(" CD_OP = '" + drMatl["CD_OP"].ToString().Trim() + "' AND CD_WC = '" + drMatl["CD_WC"].ToString().Trim() + "' AND CD_WCOP = '" + drMatl["CD_WCOP"].ToString().Trim() + "' ");

            //    if (drArr == null || drArr.Length <= 0)
            //        return false;
            //}

            try
            {
                Duzon.Common.Util.SpInfoCollection sc = new Duzon.Common.Util.SpInfoCollection();

                #region /** 변수선언 **/
                string[] _sWo = new string[] {  "STATE","CD_COMPANY",
                                                "NO_WO",
                                                "CD_PLANT",
                                                "NO_EMP",
                                                "CD_ITEM",
                                                "QT_ITEM",
                                                "FG_WO",
                                                "ST_WO",
                                                "PATN_ROUT",
                                                "TP_ROUT",
                                                "NO_LOT",
                                                "NO_SO",
                                                "NO_PJT",
                                                "DT_REL",
                                                "DT_DUE",
                                                "DTS_INSERT",
                                                "ID_INSERT",
                                                "DTS_UPDATE",
                                                "ID_UPDATE",
                                                "QT_WORK",
                                                "NO_LINE_SO", 
                                                "TP_GI", 
                                                "TP_GR"
                                            };

                string[] _sRout = new string[] {  "STATE","CD_COMPANY",
                                                "NO_WO",
                                                "NO_LINE",
                                                "CD_OP",
                                                "CD_PLANT",
                                                "CD_WC",
                                                "CD_WCOP",
                                                "DT_REL",
                                                "DT_DUE",
                                                "ST_OP",
                                                "FG_WC",
                                                "YN_WORK",
                                                "QT_WO",
                                                "QT_WIP",
                                                "QT_WORK",
                                                "QT_REJECT",
                                                "QT_REWORK",
                                                "QT_MOVE",
                                                "TM_SETUP",
                                                "CD_RSRC1",
                                                "TM_LABOR",
                                                "CD_RSRC2",
                                                "TM_MACH",
                                                "TM_MOVE",
                                                "DY_SUBCON",
                                                "TM_LABOR_ACT",
                                                "TM_MACH_ACT",
                                                "CD_TOOL",
                                                "TM_REL",
                                                "TM_DUE",
                                                "YN_BF",
                                                "YN_RECEIPT",
                                                "TM",
                                                "YN_PAR",
                                                "YN_QC",
                                                "QT_CLS",
                                                "CD_OP_BASE",
                                                "YN_FINAL",
                                                "QT_START", 
                                                "TP_OPPATH"
                };

                string[] _sBill = new string[] {   "STATE","CD_COMPANY",
                                                   "NO_WO",
                                                   "NO_LINE",
                                                   "CD_PLANT",
                                                   "CD_OP",
                                                   "CD_MATL",
                                                   "CD_WC",
                                                   "DT_NEED",
                                                   "FG_NEED",
                                                   "QT_NEED",
                                                   "QT_REQ",
                                                   "QT_ISU",
                                                   "QT_USE",
                                                   "QT_RTN",
                                                   "NO_REQ",
                                                   "YN_BF",
                                                   "QT_NEED_NET",
                                                   "CD_WCOP",
                                                   "QT_REQ_RETURN",
                                                   "QT_TRN", 
                                                   "FG_GIR", 
                                                   "TP_OPPATH"
                };
                #endregion

                for (int i = 0; i < 3; i++)
                {
                    Duzon.Common.Util.SpInfo si = new Duzon.Common.Util.SpInfo();

                    if (i == 0)
                    {
                        if (_dtWo != null && _dtWo.Rows != null && _dtWo.Rows.Count > 0)
                        {
                            si.DataValue = _dtWo;
                            si.SpNameInsert = si.SpNameUpdate = si.SpNameDelete = "UP_PR_WO";
                            si.SpParamsDelete = si.SpParamsInsert = si.SpParamsUpdate = _sWo;
                            si.SpParamsValues.Add(ActionState.Insert, "STATE", "I");
                        }
                        else
                            continue;
                    }
                    else if (i == 1)
                    {
                        if (_dtRout != null && _dtRout.Rows != null && _dtRout.Rows.Count > 0)
                        {
                            si.DataValue = _dtRout;
                            si.SpNameInsert = si.SpNameUpdate = si.SpNameDelete = "UP_PR_WO_ROUT";
                            si.SpParamsDelete = si.SpParamsInsert = si.SpParamsUpdate = _sRout;
                            si.SpParamsValues.Add(ActionState.Insert, "STATE", ins_flag);
                        }
                        else
                            continue;
                    }
                    else if (i == 2)
                    {
                        if (_dtMatl != null && _dtMatl.Rows != null && _dtMatl.Rows.Count > 0)
                        {
                            si.DataValue = _dtMatl;
                            si.SpNameInsert = si.SpNameUpdate = si.SpNameDelete = "UP_PR_WO_BILL";
                            si.SpParamsDelete = si.SpParamsInsert = si.SpParamsUpdate = _sBill;
                            si.SpParamsValues.Add(ActionState.Insert, "STATE", ins_flag);
                        }
                        else
                            continue;
                    }

                    si.SpParamsValues.Add(ActionState.Update, "STATE", "U");
                    si.SpParamsValues.Add(ActionState.Delete, "STATE", "D");
                    sc.Add(si);
                }

                ResultData[] result = (ResultData[])this.Save(sc);

                if ((result.Length == 1 && result[0].Result) ||
                    (result.Length == 2 && result[0].Result && result[1].Result) ||
                    (result.Length == 3 && result[0].Result && result[1].Result && result[2].Result))
                {
                    _flex1.DataTable.AcceptChanges();
                    _flex2.DataTable.AcceptChanges();

                    if (_dsRoutBill.Tables[0].Rows.Count > 0)
                    {
                        if (_dsRoutBill.Tables[0].Rows[0].RowState.ToString() == "Added")
                        {
                            this.SelectDetail();
                            ToolBarDeleteButtonEnabled = true;
                        }
                    }

                    this.HeaderEn(false);
                    _dsRoutBill.Tables[0].AcceptChanges();
                    return true;
                }

            }
            catch (coDbException ex)
            {
                this.ShowErrorMessage(ex, this.PageName, this.MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.PR, "SEQ"), this.MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.PR, "SEQ"), string.Empty);
                return false;
            }
            catch (Exception ex)
            {
                // 작업을 정상적으로 처리하지 못했습니다.
                this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "", "", "MA_M000011");
                return false;
            }

            return false;

        }

        #endregion

        #region -> 인쇄

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            if (!DoContinue1())
                return;

            if (!DoContinue2())
                return;
        }

        #endregion

        #region -> 종료

        /// <summary>
        /// 종료
        /// </summary>
        public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
        {
            if (!DoContinue1())
                return false;

            if (!DoContinue2())
                return false;

            try
            {
                //				if(!MsgAndSave(true,true))	// 저장이 실패하면
                //					return false;					
            }
            catch (Exception ex)
            {
                // 작업을 정상적으로 처리하지 못했습니다.
                this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
            }
            return true;
        }


        #endregion

        #region -> 헤더활성여부

        private void HeaderEn(bool b)
        {
            m_dtFrom.Enabled = m_dtTo.Enabled =
            m_cboFgWo.Enabled = m_cboPath.Enabled = m_cboRout.Enabled =
            m_txtNoLot.Enabled = m_txtNoSo.Enabled = b;

            //			if(!b)
            //				m_txtCdItem.ReadOnly = false;
            //			else
            //				m_txtCdItem.ReadOnly = false;

        }


        #endregion

        #endregion

        #region ♣ 기타 이벤트 / 메소드

        #region -> 그리드 바인딩

        /// <summary>
        /// 그리드 바인딩(라우팅)
        /// </summary>
        /// <param name="ps_DataSet">바인딩할 DataSet</param>
        private void SetDataGridBinding_Rout(DataTable _dtData1)
        {
            try
            {
                _dtData1.AcceptChanges();

                // 경로정보 바인딩
                _flex1.Redraw = false;
                _flex1.Binding = _dtData1.DefaultView;
                _flex1.Redraw = true;

                if (!_flex1.HasNormalRow && !_flex2.HasNormalRow)
                    this.ShowMessage("IK1_003");
            }
            catch (Exception ex)
            {
                // 작업을 정상적으로 처리하지 못했습니다.
                this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
                return;
            }
        }

        /// <summary>
        /// 그리드 바인딩(자재소요)
        /// </summary>
        /// <param name="ps_DataSet">바인딩할 DataSet</param>
        private void SetDataGridBinding_Matl(DataTable _dtData2)
        {
            try
            {
                _dtData2.AcceptChanges();

                // 소요자재 바인딩
                _flex2.Redraw = false;
                _flex2.Binding = _dtData2.DefaultView;
                _flex2.Redraw = true;

                if (!_flex1.HasNormalRow && !_flex2.HasNormalRow)
                    this.ShowMessage("IK1_003");
            }
            catch (Exception ex)
            {
                // 작업을 정상적으로 처리하지 못했습니다.
                this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
                return;
            }
        }
        #endregion

        #region -> 컨트롤들 초기화

        /// <summary>
        /// 모든 컨트롤들의 값을 초기화 시킴
        /// </summary>
        private void InitiDataValue()
        {
            this.HeaderEn(true);

            m_cboFgWo.SelectedValue = "003";
            m_cboCdPlant.SelectedIndex = 0;
            m_cboStWo.SelectedIndex = 1; //오더상태 확정으로 셋팅...

            m_txtNoWo.Text = m_txtCdItem.CodeValue = m_txtCdItem.CodeName = m_txtStndItem.Text = m_txtUnitIm.Text =
            m_txtNoLot.Text = m_txtNoSo.Text = m_txtNoLineSo.Text = m_ctxtQtWo.Text = string.Empty;

            if (_flex1.DataTable != null)
                _flex1.DataTable.Rows.Clear();	//라우팅 클리어
            else
                this.SetDataGridBinding_Rout(null);

            if (_flex2.DataTable != null)
                _flex2.DataTable.Rows.Clear();	//자재소요 클리어
            else
                this.SetDataGridBinding_Matl(null);

            if (m_cboPath.DataSource != null)
            {
                System.Type tp = m_cboPath.DataSource.GetType();
                if (tp.FullName.Equals("System.Data.DataView"))
                    ((DataView)m_cboPath.DataSource).Table.Rows.Clear();
                else if (tp.FullName.Equals("System.Data.DataTable"))
                    ((DataTable)m_cboPath.DataSource).Rows.Clear();
                m_cboPath.DataSource = null;

            }

            if (m_cboRout.Items.Count > 0 && m_cboRout.SelectedValue == null)
            {
                m_cboRout.SelectedIndex = 0;
            }
        }

        private void InitiDeleteValue()
        {

        }

        /// <summary>
        /// 버튼 활성
        /// </summary>
        /// <param name="isEnabled"></param>
        private void SetControlEnabled(bool isEnabled)
        {
            m_cboCdPlant.Enabled = m_txtNoWo.Enabled = isEnabled;
            //m_pnlMain.Enabled = isEnabled;
            //m_cboFgWo.Enabled = isEnabled;
        }
        #endregion

        #region -> 작업경로, 리드타임 셋팅
        /// <summary>
        /// 작업경로 콤보박스 셋팅 와 리드타임을 계산하여 종료일을 셋팅...
        /// </summary>
        private void Rout_Combo_LtSetting(DataSet _ds)
        {
            try
            {
                _dtOppath = _ds.Tables["NO_OPPATH"];

                //공정경로PATH 
                if (_dtOppath != null && _dtOppath.Rows.Count > 1)
                {
                    m_cboPath.DataSource = _dtOppath;
                    m_cboPath.DisplayMember = "PATH";
                    m_cboPath.ValueMember = "NO_OPPATH";
                    m_cboPath.SelectedIndex = 0;

                    //공정경로에따라 공정경로PATH셋팅...
                    DataView dv_oppath = _dtOppath.DefaultView;
                    dv_oppath.BeginInit();
                    dv_oppath.RowFilter = "TP_OPPATH = '" + m_cboRout.SelectedValue.ToString() + "'";
                    dv_oppath.EndInit();

                    if (dv_oppath.Table.Rows.Count > 0)
                    {
                        m_cboPath.DataSource = dv_oppath;
                        m_cboPath.DisplayMember = "PATH";
                        m_cboPath.ValueMember = "NO_OPPATH";
                    }
                }
                else
                {
                    m_cboPath.DataSource = _dtOppath;
                    m_cboPath.DisplayMember = "PATH";
                    m_cboPath.ValueMember = "NO_OPPATH";
                }

                //리드타임 날짜.
                if (_ds.Tables["LT_ITEM"] != null && _ds.Tables["LT_ITEM"].Rows.Count > 0)
                {
                    if (_ds.Tables["LT_ITEM"].Rows[0]["LT_ITEM"].ToString() != "0")
                        this.m_dtTo.Text = _ds.Tables["LT_ITEM"].Rows[0]["END_DATE"].ToString();
                    else
                        this.m_dtTo.Text = this.m_dtFrom.Text.ToString().Replace("/", "").Replace("_", "");
                }
                else
                {
                    this.m_dtTo.Text = this.m_dtFrom.Text.ToString().Replace("/", "").Replace("_", "");
                }
            }
            catch (Exception ex)
            {
                // 작업을 정상적으로 처리하지 못했습니다.
                this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
                return;
            }
        }
        #endregion

        #region -> 공정경로에따른 세부내역셋팅
        /// <summar>y
        /// 공정경로에따른 세부내역셋팅
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_cbo_Rout_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                //if (_dsRoutBill.Tables != null && _dsRoutBill.Tables[0].Rows != null && _dsRoutBill.Tables[0].Rows.Count > 0)
                //{
                //    DataTable dtTemp = m_cboRout.DataSource as DataTable;
                //    _dsRoutBill.Tables[0].Rows[0]["TP_GI"] = dtTemp.Rows[0]["TP_GI"];
                //    _dsRoutBill.Tables[0].Rows[0]["TP_GR"] = dtTemp.Rows[0]["TP_GR"];
                //}

                if (_dtOppath != null)
                {
                    DataView dv_oppath = _dtOppath.DefaultView;
                    dv_oppath.BeginInit();
                    dv_oppath.RowFilter = "TP_OPPATH = '" + m_cboRout.SelectedValue.ToString() + "'";
                    dv_oppath.EndInit();

                    if (dv_oppath.Table.Rows.Count > 0)
                    {
                        m_cboPath.DataSource = dv_oppath;
                        m_cboPath.DisplayMember = "PATH";
                        m_cboPath.ValueMember = "NO_OPPATH";
                    }
                }
            }
            catch (Exception ex)
            {
                // 작업을 정상적으로 처리하지 못했습니다.
                this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
            }
        }
        #endregion

        #region -> 작업경로 콤보박스 셋팅

        /// <summary>
        /// 작업경로 콤보박스 셋팅
        /// </summary>
        private void set_RoutPath_Combo(DataTable _dt)
        {
            try
            {
                // 작업경로PATH
                if (_dt.Rows.Count > 1)
                {
                    m_cboPath.DataSource = _dt;
                    m_cboPath.DisplayMember = "PATH";
                    m_cboPath.ValueMember = "NO_OPPATH";
                    m_cboPath.SelectedIndex = 0;

                    if (_dsRoutBill != null && _dsRoutBill.Tables != null && _dsRoutBill.Tables[0].Rows != null && _dsRoutBill.Tables[0].Rows.Count > 0)
                        m_cboPath.SelectedValue = _dsRoutBill.Tables[0].Rows[0]["PATN_ROUT"].ToString().Trim();

                    if (m_cboRout.Items.Count > 0 && m_cboRout.SelectedValue == null)
                        m_cboRout.SelectedIndex = 0;
                    //공정경로에따라 공정경로PATH셋팅...
                    DataView dv_oppath = _dt.DefaultView;
                    dv_oppath.BeginInit();
                    dv_oppath.RowFilter = "TP_OPPATH = '" + m_cboRout.SelectedValue.ToString() + "'";
                    dv_oppath.EndInit();

                    if (dv_oppath.Table.Rows.Count > 0)
                    {
                        m_cboPath.DataSource = dv_oppath;
                        m_cboPath.DisplayMember = "PATH";
                        m_cboPath.ValueMember = "NO_OPPATH";
                        if (_dsRoutBill != null && _dsRoutBill.Tables != null && _dsRoutBill.Tables[0].Rows != null && _dsRoutBill.Tables[0].Rows.Count > 0)
                            m_cboPath.SelectedValue = _dsRoutBill.Tables[0].Rows[0]["PATN_ROUT"].ToString().Trim();
                    }
                }
                else
                {
                    m_cboPath.DataSource = _dt;
                    m_cboPath.DisplayMember = "PATH";
                    m_cboPath.ValueMember = "NO_OPPATH";
                    if (_dsRoutBill != null && _dsRoutBill.Tables != null && _dsRoutBill.Tables[0].Rows != null && _dsRoutBill.Tables[0].Rows.Count > 0)
                        m_cboPath.SelectedValue = _dsRoutBill.Tables[0].Rows[0]["PATN_ROUT"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                // 작업을 정상적으로 처리하지 못했습니다.
                this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
                return;
            }
        }
        #endregion

        #region -> 작업지시 헤드 부분의 필드 체크
        /// <summary>
        /// 공정경로 자재소요 버튼 활성화 여부
        /// </summary>
        /// <param name="gabun"></param>
        /// <param name="tf"></param>
        private void Check_Rout_Matl_Button(string gabun, string tf)
        {
            try
            {
                //공정경로
                if (gabun == "R")
                {
                    if (tf == "T")
                        m_btnRespread_rout.Enabled = m_btnInsert_rout.Enabled = m_btnDelete_rout.Enabled = true;
                    else if (tf == "F")
                        m_btnRespread_rout.Enabled = m_btnInsert_rout.Enabled = m_btnDelete_rout.Enabled = false;

                }//소요자재
                else if (gabun == "M")
                {
                    if (tf == "T")
                        m_btnRespread_matl.Enabled = m_btnInsert_matl.Enabled = m_btnDelete_matl.Enabled = true;
                    else if (tf == "F")
                        m_btnRespread_matl.Enabled = m_btnInsert_matl.Enabled = m_btnDelete_matl.Enabled = false;

                }
            }
            catch (Exception ex)
            {
                // 작업을 정상적으로 처리하지 못했습니다.
                this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
                return;
            }

        }
        #endregion

        #endregion

        #region ♣ 그리드 이벤트/메소드

        #region -> 경로정보 & 소요자재 그리드 이벤트
        /// <summary>
        /// DataRefresh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _flex_AfterDataRefresh(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            try
            {
                if (IsChanged(null))
                {
                    //if(m_cboStWo.SelectedValue.ToString() == "F")
                    if (m_cboStWo.SelectedValue.ToString() == "O")
                    {
                        if (!ToolBarSaveButtonEnabled)
                            this.ToolBarSaveButtonEnabled = true;
                    }
                    else
                    {
                        this.ToolBarSaveButtonEnabled = true;
                    }
                }
                else
                {
                    this.ToolBarSaveButtonEnabled = false;
                }

                switch (((Dass.FlexGrid.FlexGrid)sender).Name.ToString())
                {
                    case "_flex1":
                        #region -> 경로정보 그리드
                        //if(!_flex1.HasNormalRow || m_cboStWo.SelectedValue.ToString() != "F")
                        if (!_flex1.HasNormalRow || m_cboStWo.SelectedValue.ToString() != "O")
                            this.m_btnDelete_rout.Enabled = false;
                        else
                            this.m_btnDelete_rout.Enabled = true;

                        #endregion
                        break;

                    case "_flex2":
                        #region -> 소요자재 그리드

                        //if(!_flex2.HasNormalRow || m_cboStWo.SelectedValue.ToString() != "F")
                        if (!_flex2.HasNormalRow || m_cboStWo.SelectedValue.ToString() != "O")
                            this.m_btnDelete_matl.Enabled = false;
                        else
                            this.m_btnDelete_matl.Enabled = true;

                        #endregion
                        break;

                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, this.PageName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        /// <summary>
        /// ValidateEdit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _flex_ValidateEdit(object sender, C1.Win.C1FlexGrid.ValidateEditEventArgs e)
        {
            try
            {
                if (!this.onValidateChk(sender))
                {
                    e.Cancel = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, this.PageName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        /// <summary>
        /// OnShowHelp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnShowHelpGrid(object sender, System.EventArgs e)
        {
            try
            {
                if (Duzon.Common.Forms.BasicInfo.ActiveDialog == true)
                    return;

                onShowHelp(sender);
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, this.PageName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        #region -> Validate Check
        /// <summary>
        /// Validate Check
        /// </summary>
        /// <param name="col"></param>
        /// <param name="sender"></param>
        private bool onValidateChk(object sender)
        {
            try
            {
                //Validate Check 대상이될 그리드를 받는다.
                Dass.FlexGrid.FlexGrid _flex = (Dass.FlexGrid.FlexGrid)sender;

                switch (_flex.Cols[_flex.Col].Name)
                {
                    case "CD_OP":

                        #region -> Op.

                        if (_flex.Name == "_flex1")
                        {
                            if (_flex.EditData == "")
                            {

                                _flex[_flex.Row, "CD_WC"] = _flex[_flex.Row, "NM_WC"] = _flex[_flex.Row, "FG_WC"] =
                                _flex[_flex.Row, "NM_SYSDEF"] = _flex[_flex.Row, "CD_WCOP"] = _flex[_flex.Row, "NM_OP"] = "";

                                //Op.(는)은 필수 입력입니다.
                                string messge = "Op." + this.MainFrameInterface.GetMessageDictionaryItem("CM_M000002");
                                this.MainFrameInterface.SetStatusBarMessage(messge);
                                return true;
                            }

                            _flex[_flex.Row, "CD_OP_BASE"] = _flex.Editor.Text.ToUpper();
                        }
                        else
                        {
                            DataRow[] ChkRows = _flex2.DataTable.Select("CD_OP = '" + _flex.EditData.ToString() + "'");

                            if (ChkRows != null && ChkRows.Length > 0)
                            {
                                _flex[_flex.Row, "CD_WC"] = ChkRows[0]["CD_WC"].ToString();
                                _flex[_flex.Row, "DIS_CD_WC"] = ChkRows[0]["NM_WC"].ToString();
                                _flex[_flex.Row, "CD_WCOP"] = ChkRows[0]["CD_WCOP"].ToString();
                                _flex[_flex.Row, "NM_OP"] = ChkRows[0]["NM_OP"].ToString();
                            }
                        }

                        _flex.Editor.Text = _flex.Editor.Text.ToUpper();

                        #endregion

                        break;

                    case "CD_WC":

                        #region -> 작업장
                        try
                        {
                            if (_flex.EditData == "")
                            {
                                if (_flex.Name == "_flex1")
                                    _flex[_flex.Row, "NM_WC"] = _flex[_flex.Row, "FG_WC"] = _flex[_flex.Row, "NM_SYSDEF"] = "";
                                else
                                    _flex[_flex.Row, "DIS_CD_WC"] = "";

                                //작업장(는)은 필수 입력입니다.
                                string messge = this.GetDDItem("CD_WC") + this.MainFrameInterface.GetMessageDictionaryItem("CM_M000002");
                                this.MainFrameInterface.SetStatusBarMessage(messge);
                                return true;
                            }

                            HelpParam param = new HelpParam(HelpID.P_MA_WC_SUB, this.MainFrameInterface);
                            param.P09_CD_PLANT = this.m_cboCdPlant.SelectedValue.ToString();
                            param.P92_DETAIL_SEARCH_CODE = _flex.EditData;

                            HelpReturn helpreturn = (HelpReturn)this.CodeSearch(param);
                            if (helpreturn.DialogResult == System.Windows.Forms.DialogResult.OK)
                            {
                                DataRow[] _dr = helpreturn.Rows;

                                if (_flex.Name == "_flex1")
                                {
                                    _flex["CD_WC"] = _dr[0]["CD_WC"].ToString();
                                    _flex["NM_WC"] = _dr[0]["NM_WC"].ToString();
                                    _flex["FG_WC"] = _dr[0]["TP_WC"].ToString();
                                    _flex["NM_SYSDEF"] = _dr[0]["NM_TPWC"].ToString();
                                }
                                else
                                {
                                    _flex["CD_WC"] = _dr[0]["CD_WC"].ToString();
                                    _flex["DIS_CD_WC"] = _dr[0]["NM_WC"].ToString();
                                }

                                _flex.Editor.Text = _flex.Editor.Text.ToUpper();
                                this.ShowStatusBarMessage(0);
                            }
                            else
                            {
                                //작업장코드이(가) 존재하지 않습니다..
                                MessageBoxEx.Show(this.GetDataDictionaryItem(DataDictionaryTypes.PR, "CD_WC") + this.MainFrameInterface.GetMessageDictionaryItem("FI_M100001"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _flex.Editor.Text = "";
                                _flex["CD_WCOP"] = "";
                                return false;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBoxEx.Show(ex.Message, this.PageName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                        #endregion

                        break;

                    case "CD_WCOP":

                        #region -> 공정코드
                        try
                        {
                            if (_flex.EditData == "")
                            {
                                _flex[_flex.Row, "NM_OP"] = "";

                                //공정코드(는)은 필수 입력입니다.
                                string messge = this.GetDDItem("CD_WCOP") + this.MainFrameInterface.GetMessageDictionaryItem("CM_M000002");
                                this.MainFrameInterface.SetStatusBarMessage(messge);
                                return true;
                            }

                            if (_flex[_flex.Row, "CD_WC"].ToString() == "")
                            {
                                string message = this.MainFrameInterface.GetMessageDictionaryItem("MA_M000015");
                                MessageBoxEx.Show(message.Replace("@", this.GetDDItem("CD_WC")), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _flex.Editor.Text = "";
                                return false;
                            }

                            HelpParam param = new HelpParam(HelpID.P_PR_WCOP_SUB, this.MainFrameInterface);
                            param.P09_CD_PLANT = this.m_cboCdPlant.SelectedValue.ToString();
                            param.P20_CD_WC = _flex[_flex.Row, "CD_WC"].ToString();
                            param.P92_DETAIL_SEARCH_CODE = _flex.EditData;

                            HelpReturn helpreturn = (HelpReturn)this.CodeSearch(param);
                            if (helpreturn.DialogResult == System.Windows.Forms.DialogResult.OK)
                            {
                                _flex["CD_WCOP"] = helpreturn.CodeValue;
                                _flex["NM_OP"] = helpreturn.CodeName;
                                _flex.Editor.Text = _flex.Editor.Text.ToUpper();
                                this.ShowStatusBarMessage(0);
                            }
                            else
                            {
                                //공정코드이(가) 존재하지 않습니다..
                                MessageBoxEx.Show(this.GetDataDictionaryItem(DataDictionaryTypes.PR, "CD_WCOP") + this.MainFrameInterface.GetMessageDictionaryItem("FI_M100001"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _flex.Editor.Text = "";
                                return false;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBoxEx.Show(ex.Message, this.PageName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                        #endregion

                        break;

                    case "CD_MATL":

                        #region -> 소요자재품목
                        try
                        {
                            if (_flex.EditData == "")
                            {
                                _flex[_flex.Row, "NM_ITEM"] = _flex[_flex.Row, "STND_ITEM"] = _flex[_flex.Row, "UNIT_IM"] = "";

                                //품목(는)은 필수 입력입니다.
                                string messge = this.GetDDItem("CD_MATL") + this.MainFrameInterface.GetMessageDictionaryItem("CM_M000002");
                                this.MainFrameInterface.SetStatusBarMessage(messge);
                                return true;
                            }

                            HelpParam param = new HelpParam(HelpID.P_MA_PITEM_SUB, this.MainFrameInterface);
                            param.P09_CD_PLANT = this.m_cboCdPlant.SelectedValue.ToString();
                            param.P92_DETAIL_SEARCH_CODE = _flex.EditData;

                            HelpReturn helpreturn = (HelpReturn)this.CodeSearch(param);
                            if (helpreturn.DialogResult == System.Windows.Forms.DialogResult.OK)
                            {
                                DataRow[] _dr = helpreturn.Rows;

                                _flex["CD_MATL"] = _dr[0]["CD_ITEM"].ToString();
                                _flex["NM_ITEM"] = _dr[0]["NM_ITEM"].ToString();
                                _flex["STND_ITEM"] = _dr[0]["STND_ITEM"].ToString();
                                _flex["UNIT_IM"] = _dr[0]["UNIT_IM"].ToString();

                                _flex.Editor.Text = _flex.Editor.Text.ToUpper();
                                this.ShowStatusBarMessage(0);
                            }
                            else
                            {
                                //품목이(가) 존재하지 않습니다..
                                MessageBoxEx.Show(this.GetDDItem("CD_MATL") + this.MainFrameInterface.GetMessageDictionaryItem("FI_M100001"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _flex.Editor.Text = "";
                                return false;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBoxEx.Show(ex.Message, this.PageName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                        #endregion

                        break;

                    case "DT_NEED":

                        #region -> 날짜
                        string Indate = _flex.EditData.ToString().Replace("/", "").Replace("_", "").Replace(" ", "");

                        if (Indate == "" || Indate == "00000000")
                            return true;

                        try
                        {
                            System.Convert.ToDateTime(Indate.Substring(0, 4) + "/" + Indate.Substring(4, 2) + "/" + Indate.Substring(6, 2));
                        }
                        catch
                        {
                            // 날짜가 잘 못 입력되었습니다. 예)2002/01/01
                            MessageBoxEx.Show(this.MainFrameInterface.GetMessageDictionaryItem("TR_M000019"), this.PageName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _flex.Editor.Text = "";
                            return false;
                        }
                        #endregion

                        break;

                    case "QT_NEED":

                        #region -> 소요수량
                        if (_flex.EditData.ToString() == "" || _flex.EditData.ToString() == "0")
                        {
                            _flex[_flex.Row, "QT_NEED_NET"] = "0";
                        }
                        else
                        {
                            _flex[_flex.Row, "QT_NEED_NET"] = System.Convert.ToDouble(_flex.EditData.ToString()) / System.Convert.ToDouble(this.m_ctxtQtWo.Text.ToString());
                        }
                        #endregion

                        break;


                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, this.PageName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        #endregion

        #region -> 도움창 호출
        /// <summary>
        /// 도움창 호출
        /// </summary>
        /// <param name="menu">어떤창</param>
        /// <param name="sender">sender</param>
        private void onShowHelp(object sender)
        {
            //this.m_lblTitle.Focus();

            try
            {
                object dlg = null;
                object[] sub = null;

                //도움창의 대상이될 그리드를 받는다.
                Dass.FlexGrid.FlexGrid _flex = (Dass.FlexGrid.FlexGrid)sender;

                switch (_flex.Cols[_flex.Col].Name)
                {
                    case "CD_WC":
                        #region -> 작업장(W/C)
                        try
                        {
                            HelpParam param = new HelpParam(HelpID.P_MA_WC_SUB, this.MainFrameInterface);
                            param.P09_CD_PLANT = this.m_cboCdPlant.SelectedValue.ToString();

                            HelpReturn helpreturn = (HelpReturn)this.ShowHelp(param);
                            if (helpreturn.DialogResult == System.Windows.Forms.DialogResult.OK)
                            {
                                DataRow[] _dr = helpreturn.Rows;

                                if (_flex.Name == "_flex1")
                                {

                                    _flex["CD_WC"] = _dr[0]["CD_WC"].ToString();
                                    _flex["NM_WC"] = _dr[0]["NM_WC"].ToString();
                                    _flex["FG_WC"] = _dr[0]["TP_WC"].ToString();
                                    _flex["NM_SYSDEF"] = _dr[0]["NM_TPWC"].ToString();
                                }
                                else
                                {
                                    _flex["CD_WC"] = _dr[0]["CD_WC"].ToString();
                                    _flex["NM_WC"] = _dr[0]["NM_WC"].ToString();
                                    _flex["DIS_CD_WC"] = _dr[0]["NM_WC"].ToString();
                                }

                                _flex.Focus();
                                _flex.Select(_flex.Row, "CD_WCOP");
                            }
                        }
                        catch (Exception ex)
                        {
                            // 작업을 정상적으로 처리하지 못했습니다.
                            this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
                            return;
                        }
                        #endregion
                        break;

                    case "CD_WCOP":
                        #region -> 공정코드 도움창
                        try
                        {
                            HelpParam param = new HelpParam(HelpID.P_PR_WCOP_SUB, this.MainFrameInterface);
                            param.P09_CD_PLANT = this.m_cboCdPlant.SelectedValue.ToString();
                            param.P20_CD_WC = _flex[_flex.Row, "CD_WC"].ToString();

                            HelpReturn helpreturn = (HelpReturn)this.ShowHelp(param);
                            if (helpreturn.DialogResult == System.Windows.Forms.DialogResult.OK)
                            {
                                _flex["CD_WCOP"] = helpreturn.CodeValue;
                                _flex["NM_OP"] = helpreturn.CodeName;
                                _flex.Focus();
                                if (_flex.Name == "_flex1")
                                    _flex.Select(_flex.Row, "QT_WO");
                                else
                                    _flex.Select(_flex.Row, "CD_MATL");
                            }
                        }
                        catch (Exception ex)
                        {
                            // 작업을 정상적으로 처리하지 못했습니다.
                            this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
                            return;
                        }
                        #endregion
                        break;

                    case "CD_MATL":
                        #region -> 자재 도움창
                        try
                        {
                            HelpParam param = new HelpParam(HelpID.P_MA_PITEM_SUB, this.MainFrameInterface);
                            param.P09_CD_PLANT = this.m_cboCdPlant.SelectedValue.ToString();
                            param.P65_CODE5 = m_cboCdPlant.Text.Replace(" ", "").Remove(0, m_cboCdPlant.Text.Replace(" ", "").IndexOf(")", 0) + 1);

                            HelpReturn helpreturn = (HelpReturn)this.ShowHelp(param);
                            if (helpreturn.DialogResult == System.Windows.Forms.DialogResult.OK)
                            {
                                DataRow[] _dr = helpreturn.Rows;

                                _flex["CD_MATL"] = _dr[0]["CD_ITEM"].ToString();
                                _flex["NM_ITEM"] = _dr[0]["NM_ITEM"].ToString();
                                _flex["STND_ITEM"] = _dr[0]["STND_ITEM"].ToString();
                                _flex["UNIT_IM"] = _dr[0]["UNIT_IM"].ToString();
                                _flex.Focus();
                                _flex.Select(_flex.Row, "QT_NEED");
                            }
                        }
                        catch (Exception ex)
                        {
                            // 작업을 정상적으로 처리하지 못했습니다.
                            this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
                            return;
                        }
                        #endregion
                        break;

                    case "DT_NEED":
                        #region -> 달력 도움창
                        try
                        {
                            dlg = this.LoadHelpWindow("P_PR_CALENDAR", new object[] { this.MainFrameInterface, _flex[_flex.Row, "DT_NEED"].ToString() });
                            if (((Duzon.Common.Forms.CommonDialog)dlg).ShowDialog() == DialogResult.OK)
                            {
                                if (dlg is IHelpWindow)
                                {
                                    sub = ((IHelpWindow)dlg).ReturnValues;
                                    _flex[_flex.Row, "DT_NEED"] = sub[0].ToString();
                                    _flex.Focus();
                                    _flex.Select(_flex.Row, "YN_BF");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // 작업을 정상적으로 처리하지 못했습니다.
                            this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
                            return;
                        }
                        #endregion
                        break;

                    case "CD_OP":
                        #region -> 공정 도움창
                        try
                        {
                            HelpParam param = new HelpParam(HelpID.P_PR_ROUT_SUB, this.MainFrameInterface);
                            param.P09_CD_PLANT = this.m_cboCdPlant.SelectedValue.ToString();
                            param.P12_CD_ITEM = this.m_txtCdItem.CodeValue;
                            param.P61_CODE1 = "001";

                            HelpReturn helpreturn = (HelpReturn)this.ShowHelp(param);

                            if (helpreturn.DialogResult == System.Windows.Forms.DialogResult.OK)
                            {
                                _flex["CD_OP"] = helpreturn.CodeValue;
                                _flex.Select(_flex.Row, "CD_WC");
                                _flex.Focus();
                            }
                        }
                        catch (Exception ex)
                        {
                            // 작업을 정상적으로 처리하지 못했습니다.
                            this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
                            return;
                        }
                        #endregion
                        break;

                    default:
                        break;

                }
            }
            catch (Exception ex)
            {
                // 작업을 정상적으로 처리하지 못했습니다.
                this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
                return;
            }
        }

        #endregion
        #endregion

        #endregion

        #region ♣ 경로재전개, 소요자재전개

        #region -> 경로정보 버튼 이벤트
        /// <summary>
        /// 경로재전개 
        /// </summary>
        private void m_btn_respread_rout_Click(object sender, System.EventArgs e)
        {
            respread_rout(); //경로재전개
        }

        /// <summary>
        /// 경로 재전개 라우팅 메소드
        /// </summary>
        private void respread_rout()
        {
            if (!DoContinue1())
                return;

            if (!DoContinue2())
                return;

            try
            {
                // 헤드 필드 체크
                if (!SearchCondition())
                    return;

                Duzon.Common.Util.SpInfo si = new Duzon.Common.Util.SpInfo();
                object[] obj = new Object[27];
                si.SpNameSelect = "UP_PR_ROUT_L";
                obj[0] = "S2";
                obj[4] = this.m_txtCdItem.CodeValue;
                obj[5] = this.m_cboPath.SelectedValue.ToString();
                obj[6] = this.m_cboCdPlant.SelectedValue.ToString();
                obj[7] = this.LoginInfo.CompanyCode;
                obj[26] = m_cboRout.SelectedValue.ToString();
                si.SpParamsSelect = obj;

                object ret = this.FillDataTable(si);
                DataTable _dtRout = (DataTable)((ResultData)ret).DataValue;

                if (_dtRout != null)
                {
                    if (_dtRout.Rows.Count > 0)
                    {
                        if (_flex1.HasNormalRow)
                            _flex1.RemoveViewAll();

                        #region /* M/S 여부 셋팅 */
                        string work = "N"; //M/S 여부
                        if (_dtYnWork != null && _dtYnWork.Rows.Count > 0)
                        {
                            DataRow[] yn_work = _dtYnWork.Select("CODE = '" + this.m_cboRout.SelectedValue.ToString() + "'");
                            if (yn_work.Length > 0)
                            {
                                if (yn_work[0]["YN_WORK"].ToString() != "" && yn_work[0]["YN_WORK"] != null)
                                    work = yn_work[0]["YN_WORK"].ToString();
                            }
                        }
                        #endregion

                        _flex1.Redraw = false;

                        for (int i = 0; i < _dtRout.Rows.Count; i++)
                        {
                            _flex1.Rows.Add();
                            _flex1.Row = i + 1;
                            _flex1[_flex1.Row, "CD_COMPANY"] = _dtRout.Rows[i]["CD_COMPANY"].ToString();
                            _flex1[_flex1.Row, "CD_PLANT"] = _dtRout.Rows[i]["CD_PLANT"].ToString();
                            _flex1[_flex1.Row, "CD_OP"] = _dtRout.Rows[i]["CD_OP"].ToString();
                            _flex1[_flex1.Row, "CD_OP_BASE"] = _dtRout.Rows[i]["CD_OP_BASE"].ToString();
                            _flex1[_flex1.Row, "CD_WC"] = _dtRout.Rows[i]["CD_WC"].ToString();
                            _flex1[_flex1.Row, "NM_WC"] = _dtRout.Rows[i]["NM_WC"].ToString();
                            _flex1[_flex1.Row, "DIS_CD_WC"] = _dtRout.Rows[i]["DIS_CD_WC"].ToString();
                            _flex1[_flex1.Row, "FG_WC"] = _dtRout.Rows[i]["FG_WC"].ToString();
                            _flex1[_flex1.Row, "NM_SYSDEF"] = _dtRout.Rows[i]["NM_SYSDEF"].ToString();
                            _flex1[_flex1.Row, "CD_WCOP"] = _dtRout.Rows[i]["CD_WCOP"].ToString();
                            _flex1[_flex1.Row, "NM_OP"] = _dtRout.Rows[i]["NM_OP"].ToString();
                            _flex1[_flex1.Row, "ST_OP"] = _dtRout.Rows[i]["ST_OP"].ToString();
                            _flex1[_flex1.Row, "QT_WO"] = Convert.ToDouble(m_ctxtQtWo.Text);
                            _flex1[_flex1.Row, "YN_BF"] = _dtRout.Rows[i]["YN_BF"].ToString();
                            _flex1[_flex1.Row, "YN_RECEIPT"] = work;
                            _flex1[_flex1.Row, "YN_PAR"] = _dtRout.Rows[i]["YN_PAR"].ToString();
                            _flex1[_flex1.Row, "YN_QC"] = _dtRout.Rows[i]["YN_QC"].ToString();
                            _flex1[_flex1.Row, "QT_WIP"] = _dtRout.Rows[i]["QT_WIP"].ToString();
                            _flex1[_flex1.Row, "QT_WORK"] = _dtRout.Rows[i]["QT_WORK"].ToString();
                            _flex1[_flex1.Row, "QT_REJECT"] = _dtRout.Rows[i]["QT_REJECT"].ToString();
                            _flex1[_flex1.Row, "QT_REWORK"] = _dtRout.Rows[i]["QT_REWORK"].ToString();
                            _flex1[_flex1.Row, "QT_MOVE"] = _dtRout.Rows[i]["QT_MOVE"].ToString();
                            _flex1[_flex1.Row, "NO_LINE"] = i + 1;
                            _flex1[_flex1.Row, "DT_REL"] = this.m_dtFrom.Text.ToString().Replace("/", "").Replace("_", "");
                            _flex1[_flex1.Row, "DT_DUE"] = this.m_dtTo.Text.ToString().Replace("/", "").Replace("_", "");

                            if (i == _dtRout.Rows.Count - 1)
                                _flex1[_flex1.Row, "YN_FINAL"] = "Y";
                            else
                                _flex1[_flex1.Row, "YN_FINAL"] = "N";

                            _flex1.AddFinished();
                        }

                        _flex1.Redraw = true;
                        _flex1.Col = _flex1.Cols.Fixed;
                        _flex1.Focus();

                        ToolBarDeleteButtonEnabled = ToolBarSaveButtonEnabled = true;
                        if (_flex1.HasNormalRow)
                            m_btnDelete_rout.Enabled = true;
                    }
                    else
                    {	//(이)가 공정경로가 존재하지 않습니다!
                        this.ShowMessage("PR_M100030", m_txtCdItem.CodeValue);
                        m_btnInsert_rout.Enabled = m_btnDelete_rout.Enabled = true;
                    }
                }
                else
                {
                    //(이)가 공정경로가 존재하지 않습니다!
                    this.ShowMessage("PR_M100030", m_txtCdItem.CodeValue);
                    m_btnInsert_rout.Enabled = m_btnDelete_rout.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                // 작업을 정상적으로 처리하지 못했습니다.
                this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
                return;
            }
        }

        #endregion

        #region -> 소요자재 버튼 이벤트
        /// <summary>
        /// 소요자재 전개 
        /// </summary>
        private void m_btn_respread_matl_Click(object sender, System.EventArgs e)
        {
            respread_matl(); //자재소요 전개 메소드		

        }

        /// <summary>
        /// 소요자재 전개 
        /// </summary>  
        private void respread_matl()
        {
            try
            {
                // 헤드 필드 체크
                if (!SearchCondition())
                    return;

                DataTable _dtBom = new DataTable();
                _dtBom.Clear();

                //자재소요 표준경로일땐 BOM 전개
                if (m_cboPath.SelectedValue.ToString() == "001")
                {

                    Duzon.Common.Util.SpInfo si = new Duzon.Common.Util.SpInfo();
                    object[] obj = new Object[24];
                    si.SpNameSelect = "UP_PR_BOM";
                    obj[0] = "S7";
                    obj[1] = this.LoginInfo.CompanyCode;
                    obj[2] = this.m_txtCdItem.CodeValue;
                    obj[3] = this.m_cboCdPlant.SelectedValue.ToString();
                    obj[8] = this.m_ctxtQtWo.Text.ToString();
                    obj[12] = this.m_dtFrom.Text.ToString().Replace("/", "").Replace("_", "");		//시작일
                    si.SpParamsSelect = obj;

                    object ret = this.FillDataTable(si);
                    _dtBom = (DataTable)((ResultData)ret).DataValue;
                }
                else
                {
                    Duzon.Common.Util.SpInfo si = new Duzon.Common.Util.SpInfo();
                    object[] obj = new Object[13];
                    si.SpNameSelect = "UP_PR_ROUT_ASN";
                    obj[0] = "S3";
                    obj[1] = this.LoginInfo.CompanyCode;
                    obj[2] = this.m_cboCdPlant.SelectedValue.ToString();
                    obj[3] = this.m_txtCdItem.CodeValue;
                    obj[6] = this.m_cboPath.SelectedValue.ToString();
                    obj[8] = this.m_ctxtQtWo.Text.ToString();
                    obj[9] = this.m_dtFrom.Text.ToString().Replace("/", "").Replace("_", "");
                    si.SpParamsSelect = obj;

                    object ret = this.FillDataTable(si);
                    _dtBom = (DataTable)((ResultData)ret).DataValue;
                }

                if (_dtBom != null)
                {
                    if (_dtBom != null && _dtBom.Rows.Count >= 1)
                    {
                        if (_flex2.HasNormalRow)
                            _flex2.RemoveViewAll();

                        _flex2.Redraw = false;

                        for (int i = 0; i < _dtBom.Rows.Count; i++)
                        {
                            _flex2.Rows.Add();
                            _flex2.Row = i + 1;
                            _flex2[_flex2.Row, "CD_OP"] = _dtBom.Rows[i]["CD_OP"].ToString();
                            _flex2[_flex2.Row, "CD_WC"] = _dtBom.Rows[i]["CD_WC"].ToString();
                            _flex2[_flex2.Row, "NM_WC"] = _dtBom.Rows[i]["NM_WC"].ToString();
                            _flex2[_flex2.Row, "CD_MATL"] = _dtBom.Rows[i]["CD_MATL"].ToString();
                            _flex2[_flex2.Row, "NM_ITEM"] = _dtBom.Rows[i]["NM_ITEM"].ToString();
                            _flex2[_flex2.Row, "STND_ITEM"] = _dtBom.Rows[i]["STND_ITEM"].ToString();
                            _flex2[_flex2.Row, "UNIT_IM"] = _dtBom.Rows[i]["UNIT_IM"].ToString();
                            _flex2[_flex2.Row, "YN_BF"] = _dtBom.Rows[i]["YN_BF"].ToString();
                            _flex2[_flex2.Row, "FG_GIR"] = _dtBom.Rows[i]["FG_GIR"].ToString();
                            _flex2[_flex2.Row, "DT_NEED"] = _dtBom.Rows[i]["DT_NEED"].ToString();
                            _flex2[_flex2.Row, "QT_NEED"] = _dtBom.Rows[i]["QT_NEED"].ToString();
                            _flex2[_flex2.Row, "QT_REQ"] = _flex2[_flex2.Row, "QT_ISU"] = _flex2[_flex2.Row, "QT_USE"] = 0;
                            _flex2[_flex2.Row, "NO_LINE"] = i + 1;
                            _flex2[_flex2.Row, "QT_NEED_NET"] = _dtBom.Rows[i]["QT_ITEM_NET"].ToString();
                            _flex2[_flex2.Row, "NO_WO"] = this.m_txtNoWo.Text.ToString();
                            _flex2[_flex2.Row, "CD_COMPANY"] = _dtBom.Rows[i]["CD_COMPANY"].ToString();
                            _flex2[_flex2.Row, "CD_PLANT"] = _dtBom.Rows[i]["CD_PLANT"].ToString();
                            _flex2[_flex2.Row, "DIS_CD_WC"] = _dtBom.Rows[i]["DIS_CD_WC"].ToString();
                            _flex2[_flex2.Row, "CD_WCOP"] = _dtBom.Rows[i]["CD_WCOP"].ToString();
                            _flex2[_flex2.Row, "NM_OP"] = _dtBom.Rows[i]["NM_OP"].ToString();
                            _flex2.AddFinished();
                        }

                        _flex2.Redraw = true;
                        _flex2.Col = _flex2.Cols.Fixed;
                        _flex2.Focus();

                        ToolBarDeleteButtonEnabled = ToolBarSaveButtonEnabled = true;
                        if (_flex2.HasNormalRow)
                            m_btnDelete_matl.Enabled = true;
                    }
                }
                else
                {
                    //(이)가 소요자재가 존재하지 않습니다!
                    this.ShowMessage("PR_M100031", m_txtCdItem.CodeValue);
                }
            }
            catch (Exception ex)
            {
                // 작업을 정상적으로 처리하지 못했습니다.
                this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
                return;
            }
        }

        #endregion

        #endregion

        #region ♣ 메인버튼 도움창, Validate Check

        private void OnBpControl_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            switch (e.HelpID)
            {
                case Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB: // 사업자등록번호
                    e.HelpParam.P09_CD_PLANT = this.m_cboCdPlant.SelectedValue.ToString();
                    e.HelpParam.P65_CODE5 = m_cboCdPlant.Text.Replace(" ", "").Remove(0, m_cboCdPlant.Text.Replace(" ", "").IndexOf(")", 0) + 1);
                    break;
            }
        }

        private void OnBpControl_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                if (e.DialogResult == System.Windows.Forms.DialogResult.OK)
                {

                    switch (e.HelpID)
                    {
                        case Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB: // 사업자등록번호

                            DataRow[] _dr = e.HelpReturn.Rows;
                            m_txtStndItem.Text = _dr[0]["STND_ITEM"].ToString();
                            m_txtUnitIm.Text = _dr[0]["UNIT_IMNM"].ToString();
                            this._stFgBf = _dr[0]["FG_BF"].ToString();
                            this._stFG_GIR = _dr[0]["FG_GIR"].ToString();
                            Application.DoEvents();

                            //경로, 리드타임 계산
                            Duzon.Common.Util.SpInfoCollection sc = new Duzon.Common.Util.SpInfoCollection();

                            for (int i = 0; i < 2; i++)
                            {
                                Duzon.Common.Util.SpInfo si = new Duzon.Common.Util.SpInfo();

                                if (i == 0)
                                {
                                    object[] obj = new Object[6];
                                    si.SpNameSelect = "UP_PR_COMMON";
                                    obj[0] = this.LoginInfo.CompanyCode;
                                    obj[1] = this.m_cboCdPlant.SelectedValue.ToString();
                                    obj[2] = "NO_OPPATH";
                                    obj[3] = this.m_txtCdItem.CodeValue;
                                    si.SpParamsSelect = obj;
                                }
                                else
                                {
                                    object[] obj = new Object[6];
                                    si.SpNameSelect = "UP_PR_COMMON";
                                    obj[0] = this.LoginInfo.CompanyCode;
                                    obj[1] = this.m_cboCdPlant.SelectedValue.ToString();
                                    obj[2] = "LT_ITEM";
                                    obj[3] = this.m_txtCdItem.CodeValue;
                                    obj[4] = this.m_dtFrom.Text.ToString().Replace("_", "").Replace("/", "").Replace(" ", "");
                                    si.SpParamsSelect = obj;
                                }

                                sc.Add(si);
                            }

                            DataSet _dsHelp = (DataSet)this.FillDataSet(sc);
                            _dsHelp.Tables[0].TableName = "NO_OPPATH";
                            _dsHelp.Tables[1].TableName = "LT_ITEM";

                            Rout_Combo_LtSetting(_dsHelp);
                            Application.DoEvents();

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                // 작업을 정상적으로 처리하지 못했습니다.
                this.MainFrameInterface.ShowErrorMessage(ex, this.PageName, "MA_M000011");
                return;
            }
        }

        private void OnBpControl_CodeChanged(object sender, System.EventArgs e)
        {
            m_cboPath.SelectedValue = this._stFgBf = this._stFG_GIR = m_txtStndItem.Text = m_txtUnitIm.Text = string.Empty;
        }

        /// <summary>
        /// Click
        /// </summary>
        private void OnControlClick(object sender, System.EventArgs e)
        {
            OnShowHelp(sender, e);
        }

        /// <summary>
        /// 도움창
        /// </summary>
        private void OnShowHelp(object sender, System.EventArgs e)
        {
            if (Duzon.Common.Forms.BasicInfo.ActiveDialog == true)
                return;

            object dlg = null;
            string control_name = ((Control)sender).Name.ToString();

            switch (control_name)
            {
                //tracking 번호 도움창
                case "m_txtNoSo":
                case "m_btnTracking":

                    #region -- tracking 번호 --
                    if (m_cboCdPlant.SelectedValue.ToString() == "")
                    {
                        MessageBoxEx.Show("[" + m_lblCdPlant.Text + "] " + this.MainFrameInterface.GetMessageDictionaryItem("TR_M000006"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.m_cboCdPlant.Focus();
                        return;
                    }

                    dlg = this.MainFrameInterface.LoadHelpWindow("P_SA_SO_SUB1", new object[] { this.MainFrameInterface, m_cboCdPlant.SelectedValue.ToString() });
                    if (((Duzon.Common.Forms.CommonDialog)dlg).ShowDialog() == DialogResult.OK)
                    {
                        //TAACKING 도움창에서 넘어온값들...
                        if (dlg is IHelpWindow)
                        {
                            object[] m_return = (object[])((IHelpWindow)dlg).ReturnValues;

                            DataRowView _dvSo = (DataRowView)m_return[0];
                            if (_dvSo != null)
                            {
                                this.m_txtNoSo.Text = _dvSo["NO_SEQ"].ToString().Substring(0, 12);
                                this.m_txtNoLineSo.Text = Convert.ToInt32(_dvSo["SEQ_SO"].ToString()).ToString("000");
                                this.m_txtCdItem.CodeValue = _dvSo["CD_ITEM"].ToString();		//품목코드
                                this.m_txtCdItem.CodeName = _dvSo["NM_ITEM"].ToString();		//품목명
                                this.m_txtStndItem.Text = _dvSo["STND_ITEM"].ToString();	//규격
                                this.m_txtUnitIm.Text = _dvSo["UNIT_IM"].ToString();		//단위
                                Application.DoEvents();

                                Duzon.Common.Util.SpInfoCollection sc = new Duzon.Common.Util.SpInfoCollection();

                                for (int i = 0; i < 2; i++)
                                {
                                    Duzon.Common.Util.SpInfo si = new Duzon.Common.Util.SpInfo();

                                    if (i == 0)
                                    {
                                        object[] obj = new Object[6];
                                        si.SpNameSelect = "UP_PR_COMMON";
                                        obj[0] = this.LoginInfo.CompanyCode;
                                        obj[1] = this.m_cboCdPlant.SelectedValue.ToString();
                                        obj[2] = "NO_OPPATH";
                                        obj[3] = _dvSo["CD_ITEM"].ToString();
                                        si.SpParamsSelect = obj;
                                    }
                                    else
                                    {
                                        object[] obj = new Object[6];
                                        si.SpNameSelect = "UP_PR_COMMON";
                                        obj[0] = this.LoginInfo.CompanyCode;
                                        obj[1] = this.m_cboCdPlant.SelectedValue.ToString();
                                        obj[2] = "LT_ITEM";
                                        obj[3] = _dvSo["CD_ITEM"].ToString();
                                        obj[4] = this.m_dtFrom.Text.ToString().Replace("_", "").Replace("/", "").Replace(" ", "");
                                        si.SpParamsSelect = obj;
                                    }

                                    sc.Add(si);
                                }

                                DataSet _dsHelp = (DataSet)this.FillDataSet(sc);
                                _dsHelp.Tables[0].TableName = "NO_OPPATH";
                                _dsHelp.Tables[1].TableName = "LT_ITEM";

                                Rout_Combo_LtSetting(_dsHelp);
                                Application.DoEvents();

                                //this.m_cboTpWo.Focus();
                            }
                        }

                    }
                    #endregion
                    break;

                default:
                    break;

            }
        }


        /// <summary>
        /// DoubleClick
        /// </summary>
        private void OnControlDoubleClick(object sender, System.EventArgs e)
        {
            OnShowHelp(sender, e);
        }

        /// <summary>
        /// KeyDown
        /// </summary>
        private void OnControlKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (((Control)sender).Name.ToString() == "m_cboTpWo")
                {
                    m_cboCdPlant.Focus();
                }
                else
                    SendKeys.Send("{TAB}");
            }
            else if (e.KeyCode == Keys.F3)
            {
                // F3 키를 눌렀을 경우 -- 해당도움창을 불러준다.
                OnShowHelp(sender, e);
            }
        }

        /// <summary>
        /// Validating
        /// </summary>
        private void OnControlValidating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string control_name = ((Control)sender).Name.ToString();

            switch (control_name)
            {
                //tracking 번호 도움창
                case "m_txtNoSo":

                    #region -- tracking 번호 --
                    try
                    {

                    }
                    catch (Exception ex)
                    {
                        MessageBoxEx.Show(ex.Message, this.PageName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    #endregion
                    break;

                //case "m_cboTpWo":

                //    this.m_cboCdPlant.Focus();

                //    break;

                default:
                    break;

            }
        }

        /// <summary>
        /// Validated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnControlValidated(object sender, System.EventArgs e)
        {
            try
            {
                Duzon.Common.Controls.DatePicker dt = (Duzon.Common.Controls.DatePicker)sender;

                if (dt.Text.ToString().Replace("/", "").Replace("_", "").Replace(" ", "") == "")
                {
                    if (dt.Name.ToString() == "m_dtTo")
                        this.m_cboRout.Focus();

                    return;
                }

                if (!dt.IsValidated)
                {
                    // 날짜 입력형식이 잘못되었습니다.
                    this.ShowMessage("WK1_003");
                    dt.Text = "";
                    dt.Focus();
                    return;
                }

                int from = 0;
                int to = 0;

                string f_day = m_dtFrom.Text.ToString().Replace(" ", "").Replace("/", "").Replace("_", "");
                string t_day = m_dtTo.Text.ToString().Replace(" ", "").Replace("/", "").Replace("_", "");

                if (t_day.Trim().ToString() == "")
                    return;

                if (f_day.Trim().ToString() != "")
                    from = System.Int32.Parse(f_day);

                if (t_day.Trim().ToString() != "")
                    to = System.Int32.Parse(t_day);

                if (from > to)
                {
                    // 시작일이 종료일보다 늦을 수 없습니다.
                    MessageBoxEx.Show(this.MainFrameInterface.GetMessageDictionaryItem("SA_M000010"), this.PageName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    m_dtTo.Text = "";
                    m_dtTo.Focus();
                    return;
                }

                if (dt.Name.ToString() == "m_dtTo")
                    this.m_cboRout.Focus();
            }
            catch
            {
                // 날짜가 잘 못 입력되었습니다. 예)2002/01/01
                MessageBoxEx.Show(this.MainFrameInterface.GetMessageDictionaryItem("TR_M000019"), this.PageName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                m_dtTo.Focus();
                return;
            }
        }

        private void OnQtControlValidated(object sender, System.EventArgs e)
        {
            if (_dsRoutBill.Tables[0] == null)
                return;

            if (_dsRoutBill.Tables[0].Rows.Count > 0)
            {
                if (this.m_ctxtQtWo.Modified)
                {
                    _dsRoutBill.Tables[0].Rows[0]["QT_ITEM"] = this.m_ctxtQtWo.Text.ToString();			//지시수량
                    ToolBarSaveButtonEnabled = true;

                    if (_flex1.HasNormalRow)
                    {
                        //경로 재전개
                        respread_rout();
                    }

                    if (_flex2.HasNormalRow)
                    {
                        //소요량 재전개
                        respread_matl();
                    }
                }
            }
        }
        #endregion

        private void m_dtTo_Click(object sender, EventArgs e)
        {

        }

    }
}

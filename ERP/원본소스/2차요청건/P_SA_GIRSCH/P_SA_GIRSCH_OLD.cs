//********************************************************************
// 최 초 작성자 : ?? (누군지 모름) -> 정말 엉망임...
// 작   성   자 : 유이열
// 재 작  성 일 : 2003-04-22
// 모   듈   명 : 영업
// 시 스  템 명 : 영업관리
// 서브시스템명 : 현황
// 페 이 지  명 : 납품의뢰현황
// 프로젝트  명 : P_SA_GIRSCH
//********************************************************************
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Threading;
using System.Windows.Forms;

// Declear custom namespace
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using System.IO;

using System.Data.SqlClient;
using System.Data.OleDb;

using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Windows.Print;

namespace sale
{
	/// <summary>
	/// P_SA_GIRSCH에 대한 요약 설명입니다.
	/// </summary>
	public class P_SA_GIRSCH_OLD : Duzon.Common.Forms.PageBase                                                                               
	{
		#region ♣ 멤버필드

		#region -> 멤버필드(일반)
		// Panel
		private Duzon.Common.Controls.PanelExt panel4;
		private Duzon.Common.Controls.PanelExt panel8;
		private Duzon.Common.Controls.PanelExt panel7;
		private Duzon.Common.Controls.PanelExt panel6;
        private Duzon.Common.Controls.PanelExt panel5;
        private Duzon.Common.Controls.PanelExt panel10;
		private Duzon.Common.Controls.PanelExt pnlPage2_2;
		private Duzon.Common.Controls.PanelExt pnlPage2_1;
		private Duzon.Common.Controls.PanelExt pnlPage3_2;
		private Duzon.Common.Controls.PanelExt pnlPage3_1;
		private Duzon.Common.Controls.PanelExt pnlPage4_2;
		private Duzon.Common.Controls.PanelExt pnlPage4_1;
		private Duzon.Common.Controls.PanelExt pnlPage5_2;
		private Duzon.Common.Controls.PanelExt pnlPage5_1;
		private Duzon.Common.Controls.PanelExt pnlPage6_2;
		private Duzon.Common.Controls.PanelExt pnlPage6_1;
        private Duzon.Common.Controls.PanelExt pnlPage7_1;
        private Duzon.Common.Controls.PanelExt pnlPage7_2;
        private Duzon.Common.Controls.PanelExt pnlPage8_2;


        // Label
		private Duzon.Common.Controls.LabelExt m_lblDtGir;
		private Duzon.Common.Controls.LabelExt m_lblSlUnit;
		private Duzon.Common.Controls.LabelExt m_lblYnAm;
		private Duzon.Common.Controls.LabelExt m_lblFgGi;
		private Duzon.Common.Controls.LabelExt label2;
		private Duzon.Common.Controls.LabelExt label6;
		private Duzon.Common.Controls.LabelExt label5;
		private Duzon.Common.Controls.LabelExt label4;
		private Duzon.Common.Controls.LabelExt label7;

		// ImageList
		private System.Windows.Forms.ImageList imageList1;

		// IContainer
		private System.ComponentModel.IContainer components;

		
		/// <summary>
		/// DataSet 선언
		/// </summary>
		private DataSet m_dsCombo;

		private Duzon.Common.Controls.LabelExt m_lblBizarea;
		private Duzon.Common.Controls.DropDownComboBox m_cboBizArea;
		private Duzon.Common.Controls.DropDownComboBox cboPlantGir;
		private Duzon.Common.Controls.DropDownComboBox cboTpBusi;
		private Duzon.Common.Controls.DropDownComboBox cboStaGir;
        private Duzon.Common.Controls.DropDownComboBox m_cboFgGi;
		private Duzon.Common.Controls.DropDownComboBox m_cboSlUnit;
		private Duzon.Common.Controls.DatePicker mskDtGirStart;
		private Duzon.Common.Controls.DatePicker mskDtGirEnd;

		// 필터될 DataView
		private DataView gdv_LineNO_GIR = new DataView();		
		private DataView gdv_LineCD_ITEM = new DataView();
		private DataView gdv_LineCD_PARTNER = new DataView();
		private DataView gdv_LineCD_GROUP = new DataView();
		private DataView gdv_LineDT_GIR = new DataView();
		private DataView gdv_LineCD_QTIOTP= new DataView();
		private DataView gdv_LineCD_PJT = new DataView();
        private DataView gdv_LineCD_SUMITEM = new DataView();

		//보관장소
		private string gstb_sl ="";

		#endregion

		#region -> 멤버필드(주요)	

		/// <summary>
		/// Load여부 변수(Paint Event에서 사용)
		/// </summary>
		private bool _isPainted = false;

		private Dass.FlexGrid.FlexGrid _flexM_1;
		private Dass.FlexGrid.FlexGrid _flexD_1;
		private Dass.FlexGrid.FlexGrid _flexM_2;
		private Dass.FlexGrid.FlexGrid _flexD_2;
		private Dass.FlexGrid.FlexGrid _flexM_3;
		private Dass.FlexGrid.FlexGrid _flexD_3;
		private Dass.FlexGrid.FlexGrid _flexM_4;
		private Dass.FlexGrid.FlexGrid _flexD_4;
		private Dass.FlexGrid.FlexGrid _flexM_5;
		private Dass.FlexGrid.FlexGrid _flexD_5;
        private Dass.FlexGrid.FlexGrid _flexM_6;
        private Dass.FlexGrid.FlexGrid _flexD_6;
        private Dass.FlexGrid.FlexGrid _flexM_7;
        private Dass.FlexGrid.FlexGrid _flexD_7;
        private Dass.FlexGrid.FlexGrid _flexD_8;
        private Dass.FlexGrid.FlexGrid _flexH;
        private Dass.FlexGrid.FlexGrid _flexD;

		private Duzon.Common.BpControls.BpCodeTextBox bpNm_Sl;
		private Duzon.Common.Controls.TabControlExt m_tabC;
		private System.Windows.Forms.TabPage tab1;
		private System.Windows.Forms.TabPage tab2;
		private System.Windows.Forms.TabPage tab3;
		private System.Windows.Forms.TabPage tab4;
		private System.Windows.Forms.TabPage tab5;
		private System.Windows.Forms.TabPage tab6;
		private System.Windows.Forms.TabPage tab7;
        private System.Windows.Forms.TabPage tab8;
		

		#endregion
        private TableLayoutPanel tableLayoutPanel1;
        private SplitContainer splitContainer1;

		StreamWriter s;
        private Duzon.Common.BpControls.BpCodeTextBox bpTpSo;
        private DropDownComboBox cboFgTransport;
        private PanelExt panelExt1;
        private LabelExt labelExt3;
        private PanelExt panelExt2;
        private DropDownComboBox m_cboClsItemS;
        private LabelExt labelExt1;
        //private PanelExt pnlPage8_2;
     
      
        private P_SA_GIRSCH_BIZ _biz;
		#endregion

		#region ♣ 생성자/소멸자

		#region -> 생성자

		public P_SA_GIRSCH_OLD()
		{
			// 이 호출은 Windows.Forms Form 디자이너에 필요합니다.
			InitializeComponent();

			Load += new System.EventHandler(Page_Load);
			Paint += new System.Windows.Forms.PaintEventHandler(Page_Paint);

	
		}

		#endregion

		#region -> 소멸자
		/// <summary> 
		/// 사용 중인 모든 리소스를 정리합니다.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region ☞ Component Designer generated code

		/// <summary>
		/// 디자이너 지원에 필요한 메서드입니다. 
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( P_SA_GIRSCH_OLD ) );
            this.panel4 = new Duzon.Common.Controls.PanelExt();
            this.m_cboClsItemS = new Duzon.Common.Controls.DropDownComboBox();
            this.cboFgTransport = new Duzon.Common.Controls.DropDownComboBox();
            this.bpTpSo = new Duzon.Common.BpControls.BpCodeTextBox();
            this.label4 = new Duzon.Common.Controls.LabelExt();
            this.bpNm_Sl = new Duzon.Common.BpControls.BpCodeTextBox();
            this.m_lblBizarea = new Duzon.Common.Controls.LabelExt();
            this.mskDtGirEnd = new Duzon.Common.Controls.DatePicker();
            this.mskDtGirStart = new Duzon.Common.Controls.DatePicker();
            this.m_cboBizArea = new Duzon.Common.Controls.DropDownComboBox();
            this.panel10 = new Duzon.Common.Controls.PanelExt();
            this.panelExt1 = new Duzon.Common.Controls.PanelExt();
            this.panel8 = new Duzon.Common.Controls.PanelExt();
            this.panel7 = new Duzon.Common.Controls.PanelExt();
            this.m_lblSlUnit = new Duzon.Common.Controls.LabelExt();
            this.label5 = new Duzon.Common.Controls.LabelExt();
            this.label6 = new Duzon.Common.Controls.LabelExt();
            this.panel6 = new Duzon.Common.Controls.PanelExt();
            this.m_lblYnAm = new Duzon.Common.Controls.LabelExt();
            this.panel5 = new Duzon.Common.Controls.PanelExt();
            this.m_lblFgGi = new Duzon.Common.Controls.LabelExt();
            this.label2 = new Duzon.Common.Controls.LabelExt();
            this.m_lblDtGir = new Duzon.Common.Controls.LabelExt();
            this.label7 = new Duzon.Common.Controls.LabelExt();
            this.cboPlantGir = new Duzon.Common.Controls.DropDownComboBox();
            this.cboTpBusi = new Duzon.Common.Controls.DropDownComboBox();
            this.cboStaGir = new Duzon.Common.Controls.DropDownComboBox();
            this.m_cboFgGi = new Duzon.Common.Controls.DropDownComboBox();
            this.m_cboSlUnit = new Duzon.Common.Controls.DropDownComboBox();
            this.panelExt2 = new Duzon.Common.Controls.PanelExt();
            this.labelExt3 = new Duzon.Common.Controls.LabelExt();
            this.labelExt1 = new Duzon.Common.Controls.LabelExt();
            this.m_tabC = new Duzon.Common.Controls.TabControlExt();
            this.tab1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._flexM_1 = new Dass.FlexGrid.FlexGrid( this.components );
            this._flexD_1 = new Dass.FlexGrid.FlexGrid( this.components );
            this.tab2 = new System.Windows.Forms.TabPage();
            this.pnlPage3_2 = new Duzon.Common.Controls.PanelExt();
            this._flexD_2 = new Dass.FlexGrid.FlexGrid( this.components );
            this.pnlPage3_1 = new Duzon.Common.Controls.PanelExt();
            this._flexM_2 = new Dass.FlexGrid.FlexGrid( this.components );
            this.tab3 = new System.Windows.Forms.TabPage();
            this.pnlPage2_2 = new Duzon.Common.Controls.PanelExt();
            this._flexD_3 = new Dass.FlexGrid.FlexGrid( this.components );
            this.pnlPage2_1 = new Duzon.Common.Controls.PanelExt();
            this._flexM_3 = new Dass.FlexGrid.FlexGrid( this.components );
            this.tab4 = new System.Windows.Forms.TabPage();
            this.pnlPage4_2 = new Duzon.Common.Controls.PanelExt();
            this._flexD_4 = new Dass.FlexGrid.FlexGrid( this.components );
            this.pnlPage4_1 = new Duzon.Common.Controls.PanelExt();
            this._flexM_4 = new Dass.FlexGrid.FlexGrid( this.components );
            this.tab5 = new System.Windows.Forms.TabPage();
            this.pnlPage5_2 = new Duzon.Common.Controls.PanelExt();
            this._flexD_5 = new Dass.FlexGrid.FlexGrid( this.components );
            this.pnlPage5_1 = new Duzon.Common.Controls.PanelExt();
            this._flexM_5 = new Dass.FlexGrid.FlexGrid( this.components );
            this.tab6 = new System.Windows.Forms.TabPage();
            this.pnlPage6_2 = new Duzon.Common.Controls.PanelExt();
            this.pnlPage6_1 = new Duzon.Common.Controls.PanelExt();
            this._flexM_6 = new Dass.FlexGrid.FlexGrid( this.components );
            this.tab7 = new System.Windows.Forms.TabPage();
            this.pnlPage7_2 = new Duzon.Common.Controls.PanelExt();
            this._flexD_7 = new Dass.FlexGrid.FlexGrid( this.components );
            this.pnlPage7_1 = new Duzon.Common.Controls.PanelExt();
            this._flexM_7 = new Dass.FlexGrid.FlexGrid( this.components );
            this.tab8 = new System.Windows.Forms.TabPage();
            this.pnlPage8_2 = new Duzon.Common.Controls.PanelExt();
            this._flexD_8 = new Dass.FlexGrid.FlexGrid( this.components );
            this.imageList1 = new System.Windows.Forms.ImageList( this.components );
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._flexD_6 = new Dass.FlexGrid.FlexGrid( this.components );
            this.mDataArea.SuspendLayout();
            this.panel4.SuspendLayout();
            ( ( System.ComponentModel.ISupportInitialize )( this.mskDtGirEnd ) ).BeginInit();
            ( ( System.ComponentModel.ISupportInitialize )( this.mskDtGirStart ) ).BeginInit();
            this.panel10.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panelExt2.SuspendLayout();
            this.m_tabC.SuspendLayout();
            this.tab1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ( ( System.ComponentModel.ISupportInitialize )( this._flexM_1 ) ).BeginInit();
            ( ( System.ComponentModel.ISupportInitialize )( this._flexD_1 ) ).BeginInit();
            this.tab2.SuspendLayout();
            this.pnlPage3_2.SuspendLayout();
            ( ( System.ComponentModel.ISupportInitialize )( this._flexD_2 ) ).BeginInit();
            this.pnlPage3_1.SuspendLayout();
            ( ( System.ComponentModel.ISupportInitialize )( this._flexM_2 ) ).BeginInit();
            this.tab3.SuspendLayout();
            this.pnlPage2_2.SuspendLayout();
            ( ( System.ComponentModel.ISupportInitialize )( this._flexD_3 ) ).BeginInit();
            this.pnlPage2_1.SuspendLayout();
            ( ( System.ComponentModel.ISupportInitialize )( this._flexM_3 ) ).BeginInit();
            this.tab4.SuspendLayout();
            this.pnlPage4_2.SuspendLayout();
            ( ( System.ComponentModel.ISupportInitialize )( this._flexD_4 ) ).BeginInit();
            this.pnlPage4_1.SuspendLayout();
            ( ( System.ComponentModel.ISupportInitialize )( this._flexM_4 ) ).BeginInit();
            this.tab5.SuspendLayout();
            this.pnlPage5_2.SuspendLayout();
            ( ( System.ComponentModel.ISupportInitialize )( this._flexD_5 ) ).BeginInit();
            this.pnlPage5_1.SuspendLayout();
            ( ( System.ComponentModel.ISupportInitialize )( this._flexM_5 ) ).BeginInit();
            this.tab6.SuspendLayout();
            this.pnlPage6_2.SuspendLayout();
            this.pnlPage6_1.SuspendLayout();
            ( ( System.ComponentModel.ISupportInitialize )( this._flexM_6 ) ).BeginInit();
            this.tab7.SuspendLayout();
            this.pnlPage7_2.SuspendLayout();
            ( ( System.ComponentModel.ISupportInitialize )( this._flexD_7 ) ).BeginInit();
            this.pnlPage7_1.SuspendLayout();
            ( ( System.ComponentModel.ISupportInitialize )( this._flexM_7 ) ).BeginInit();
            this.tab8.SuspendLayout();
            this.pnlPage8_2.SuspendLayout();
            ( ( System.ComponentModel.ISupportInitialize )( this._flexD_8 ) ).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ( ( System.ComponentModel.ISupportInitialize )( this._flexD_6 ) ).BeginInit();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add( this.tableLayoutPanel1 );
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add( this.m_cboClsItemS );
            this.panel4.Controls.Add( this.cboFgTransport );
            this.panel4.Controls.Add( this.bpTpSo );
            this.panel4.Controls.Add( this.label4 );
            this.panel4.Controls.Add( this.bpNm_Sl );
            this.panel4.Controls.Add( this.m_lblBizarea );
            this.panel4.Controls.Add( this.mskDtGirEnd );
            this.panel4.Controls.Add( this.mskDtGirStart );
            this.panel4.Controls.Add( this.m_cboBizArea );
            this.panel4.Controls.Add( this.panel10 );
            this.panel4.Controls.Add( this.panel8 );
            this.panel4.Controls.Add( this.panel7 );
            this.panel4.Controls.Add( this.panel6 );
            this.panel4.Controls.Add( this.panel5 );
            this.panel4.Controls.Add( this.label7 );
            this.panel4.Controls.Add( this.cboPlantGir );
            this.panel4.Controls.Add( this.cboTpBusi );
            this.panel4.Controls.Add( this.cboStaGir );
            this.panel4.Controls.Add( this.m_cboFgGi );
            this.panel4.Controls.Add( this.m_cboSlUnit );
            this.panel4.Controls.Add( this.panelExt2 );
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point( 3, 3 );
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size( 787, 77 );
            this.panel4.TabIndex = 0;
            // 
            // m_cboClsItemS
            // 
            this.m_cboClsItemS.AutoDropDown = true;
            this.m_cboClsItemS.BackColor = System.Drawing.Color.White;
            this.m_cboClsItemS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboClsItemS.Location = new System.Drawing.Point( 254, 28 );
            this.m_cboClsItemS.Name = "m_cboClsItemS";
            this.m_cboClsItemS.ShowCheckBox = false;
            this.m_cboClsItemS.Size = new System.Drawing.Size( 84, 20 );
            this.m_cboClsItemS.TabIndex = 16;
            this.m_cboClsItemS.UseKeyEnter = false;
            this.m_cboClsItemS.UseKeyF3 = false;
            // 
            // cboFgTransport
            // 
            this.cboFgTransport.AutoDropDown = true;
            this.cboFgTransport.BackColor = System.Drawing.Color.White;
            this.cboFgTransport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFgTransport.Location = new System.Drawing.Point( 254, 54 );
            this.cboFgTransport.Name = "cboFgTransport";
            this.cboFgTransport.ShowCheckBox = false;
            this.cboFgTransport.Size = new System.Drawing.Size( 84, 20 );
            this.cboFgTransport.TabIndex = 14;
            this.cboFgTransport.UseKeyEnter = false;
            this.cboFgTransport.UseKeyF3 = false;
            // 
            // bpTpSo
            // 
            this.bpTpSo.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpTpSo.ButtonImage = ( ( System.Drawing.Image )( resources.GetObject( "bpTpSo.ButtonImage" ) ) );
            this.bpTpSo.ChildMode = "";
            this.bpTpSo.CodeName = "";
            this.bpTpSo.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpTpSo.CodeValue = "";
            this.bpTpSo.ComboCheck = true;
            this.bpTpSo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bpTpSo.HelpID = Duzon.Common.Forms.Help.HelpID.P_SA_TPSO_SUB;
            this.bpTpSo.ItemBackColor = System.Drawing.Color.White;
            this.bpTpSo.Location = new System.Drawing.Point( 414, 52 );
            this.bpTpSo.Name = "bpTpSo";
            this.bpTpSo.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpTpSo.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.bpTpSo.SearchCode = true;
            this.bpTpSo.SelectCount = 0;
            this.bpTpSo.SetDefaultValue = false;
            this.bpTpSo.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpTpSo.Size = new System.Drawing.Size( 160, 21 );
            this.bpTpSo.TabIndex = 12;
            this.bpTpSo.TabStop = false;
            this.bpTpSo.Tag = "TP_SO;NM_SO";
            this.bpTpSo.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler( this.Control_QueryBefore );
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
            this.label4.Location = new System.Drawing.Point( 350, 30 );
            this.label4.Name = "label4";
            this.label4.Resizeble = true;
            this.label4.Size = new System.Drawing.Size( 56, 18 );
            this.label4.TabIndex = 0;
            this.label4.Tag = "TP_BUSI";
            this.label4.Text = "거래구분";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpNm_Sl
            // 
            this.bpNm_Sl.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpNm_Sl.ButtonImage = ( ( System.Drawing.Image )( resources.GetObject( "bpNm_Sl.ButtonImage" ) ) );
            this.bpNm_Sl.ChildMode = "";
            this.bpNm_Sl.CodeName = "";
            this.bpNm_Sl.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpNm_Sl.CodeValue = "";
            this.bpNm_Sl.ComboCheck = true;
            this.bpNm_Sl.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB;
            this.bpNm_Sl.ItemBackColor = System.Drawing.Color.White;
            this.bpNm_Sl.Location = new System.Drawing.Point( 84, 28 );
            this.bpNm_Sl.Name = "bpNm_Sl";
            this.bpNm_Sl.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpNm_Sl.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.bpNm_Sl.SearchCode = true;
            this.bpNm_Sl.SelectCount = 0;
            this.bpNm_Sl.SetDefaultValue = false;
            this.bpNm_Sl.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpNm_Sl.Size = new System.Drawing.Size( 102, 21 );
            this.bpNm_Sl.TabIndex = 4;
            this.bpNm_Sl.TabStop = false;
            this.bpNm_Sl.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler( this.Control_QueryBefore );
            this.bpNm_Sl.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler( this.Control_QueryAfter );
            // 
            // m_lblBizarea
            // 
            this.m_lblBizarea.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
            this.m_lblBizarea.Location = new System.Drawing.Point( 352, 5 );
            this.m_lblBizarea.Name = "m_lblBizarea";
            this.m_lblBizarea.Resizeble = true;
            this.m_lblBizarea.Size = new System.Drawing.Size( 46, 18 );
            this.m_lblBizarea.TabIndex = 0;
            this.m_lblBizarea.Tag = "CD_BIZAREA";
            this.m_lblBizarea.Text = "사업장";
            this.m_lblBizarea.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mskDtGirEnd
            // 
            this.mskDtGirEnd.CalendarBackColor = System.Drawing.Color.White;
            this.mskDtGirEnd.DayColor = System.Drawing.SystemColors.ControlText;
            this.mskDtGirEnd.FriDayColor = System.Drawing.Color.Blue;
            this.mskDtGirEnd.Location = new System.Drawing.Point( 186, 2 );
            this.mskDtGirEnd.Mask = "####/##/##";
            this.mskDtGirEnd.MaskBackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 226 ) ) ) ), ( ( int )( ( ( byte )( 239 ) ) ) ), ( ( int )( ( ( byte )( 243 ) ) ) ) );
            this.mskDtGirEnd.MaxDate = new System.DateTime( 9999, 12, 31, 23, 59, 59, 999 );
            this.mskDtGirEnd.MinDate = new System.DateTime( 1800, 1, 1, 0, 0, 0, 0 );
            this.mskDtGirEnd.Modified = false;
            this.mskDtGirEnd.Name = "mskDtGirEnd";
            this.mskDtGirEnd.PaddingCharacter = '_';
            this.mskDtGirEnd.PassivePromptCharacter = '_';
            this.mskDtGirEnd.PromptCharacter = '_';
            this.mskDtGirEnd.SelectedDayColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 253 ) ) ) ), ( ( int )( ( ( byte )( 228 ) ) ) ), ( ( int )( ( ( byte )( 153 ) ) ) ) );
            this.mskDtGirEnd.ShowToDay = true;
            this.mskDtGirEnd.ShowTodayCircle = true;
            this.mskDtGirEnd.ShowUpDown = false;
            this.mskDtGirEnd.Size = new System.Drawing.Size( 85, 21 );
            this.mskDtGirEnd.SunDayColor = System.Drawing.Color.Red;
            this.mskDtGirEnd.TabIndex = 1;
            this.mskDtGirEnd.TitleBackColor = System.Drawing.SystemColors.Control;
            this.mskDtGirEnd.TitleForeColor = System.Drawing.Color.Black;
            this.mskDtGirEnd.ToDayColor = System.Drawing.Color.Red;
            this.mskDtGirEnd.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.mskDtGirEnd.UseKeyF3 = false;
            this.mskDtGirEnd.Value = new System.DateTime( ( ( long )( 0 ) ) );
            this.mskDtGirEnd.Validated += new System.EventHandler( this.mskDtGirEnd_Validated );
            // 
            // mskDtGirStart
            // 
            this.mskDtGirStart.CalendarBackColor = System.Drawing.Color.White;
            this.mskDtGirStart.DayColor = System.Drawing.SystemColors.ControlText;
            this.mskDtGirStart.FriDayColor = System.Drawing.Color.Blue;
            this.mskDtGirStart.Location = new System.Drawing.Point( 84, 2 );
            this.mskDtGirStart.Mask = "####/##/##";
            this.mskDtGirStart.MaskBackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 226 ) ) ) ), ( ( int )( ( ( byte )( 239 ) ) ) ), ( ( int )( ( ( byte )( 243 ) ) ) ) );
            this.mskDtGirStart.MaxDate = new System.DateTime( 9999, 12, 31, 23, 59, 59, 999 );
            this.mskDtGirStart.MinDate = new System.DateTime( 1800, 1, 1, 0, 0, 0, 0 );
            this.mskDtGirStart.Modified = false;
            this.mskDtGirStart.Name = "mskDtGirStart";
            this.mskDtGirStart.PaddingCharacter = '_';
            this.mskDtGirStart.PassivePromptCharacter = '_';
            this.mskDtGirStart.PromptCharacter = '_';
            this.mskDtGirStart.SelectedDayColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 253 ) ) ) ), ( ( int )( ( ( byte )( 228 ) ) ) ), ( ( int )( ( ( byte )( 153 ) ) ) ) );
            this.mskDtGirStart.ShowToDay = true;
            this.mskDtGirStart.ShowTodayCircle = true;
            this.mskDtGirStart.ShowUpDown = false;
            this.mskDtGirStart.Size = new System.Drawing.Size( 85, 21 );
            this.mskDtGirStart.SunDayColor = System.Drawing.Color.Red;
            this.mskDtGirStart.TabIndex = 0;
            this.mskDtGirStart.TitleBackColor = System.Drawing.SystemColors.Control;
            this.mskDtGirStart.TitleForeColor = System.Drawing.Color.Black;
            this.mskDtGirStart.ToDayColor = System.Drawing.Color.Red;
            this.mskDtGirStart.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.mskDtGirStart.UseKeyF3 = false;
            this.mskDtGirStart.Value = new System.DateTime( ( ( long )( 0 ) ) );
            this.mskDtGirStart.Validated += new System.EventHandler( this.mskDtGirStart_Validated );
            // 
            // m_cboBizArea
            // 
            this.m_cboBizArea.AutoDropDown = true;
            this.m_cboBizArea.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 226 ) ) ) ), ( ( int )( ( ( byte )( 239 ) ) ) ), ( ( int )( ( ( byte )( 243 ) ) ) ) );
            this.m_cboBizArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboBizArea.Location = new System.Drawing.Point( 414, 3 );
            this.m_cboBizArea.Name = "m_cboBizArea";
            this.m_cboBizArea.ShowCheckBox = false;
            this.m_cboBizArea.Size = new System.Drawing.Size( 160, 20 );
            this.m_cboBizArea.TabIndex = 2;
            this.m_cboBizArea.UseKeyEnter = false;
            this.m_cboBizArea.UseKeyF3 = false;
            this.m_cboBizArea.KeyDown += new System.Windows.Forms.KeyEventHandler( this.CommonComboBox_KeyEvent );
            // 
            // panel10
            // 
            this.panel10.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.panel10.BackColor = System.Drawing.Color.Transparent;
            this.panel10.BackgroundImage = ( ( System.Drawing.Image )( resources.GetObject( "panel10.BackgroundImage" ) ) );
            this.panel10.Controls.Add( this.panelExt1 );
            this.panel10.Location = new System.Drawing.Point( 4, 50 );
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size( 777, 1 );
            this.panel10.TabIndex = 11;
            // 
            // panelExt1
            // 
            this.panelExt1.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
            this.panelExt1.Location = new System.Drawing.Point( 188, 0 );
            this.panelExt1.Name = "panelExt1";
            this.panelExt1.Size = new System.Drawing.Size( 60, 27 );
            this.panelExt1.TabIndex = 13;
            // 
            // panel8
            // 
            this.panel8.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.panel8.BackColor = System.Drawing.Color.Transparent;
            this.panel8.BackgroundImage = ( ( System.Drawing.Image )( resources.GetObject( "panel8.BackgroundImage" ) ) );
            this.panel8.Location = new System.Drawing.Point( 5, 24 );
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size( 777, 1 );
            this.panel8.TabIndex = 10;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
            this.panel7.Controls.Add( this.m_lblSlUnit );
            this.panel7.Controls.Add( this.label5 );
            this.panel7.Controls.Add( this.label6 );
            this.panel7.Location = new System.Drawing.Point( 581, 3 );
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size( 63, 69 );
            this.panel7.TabIndex = 2;
            // 
            // m_lblSlUnit
            // 
            this.m_lblSlUnit.Location = new System.Drawing.Point( 2, 56 );
            this.m_lblSlUnit.Name = "m_lblSlUnit";
            this.m_lblSlUnit.Resizeble = true;
            this.m_lblSlUnit.Size = new System.Drawing.Size( 57, 14 );
            this.m_lblSlUnit.TabIndex = 0;
            this.m_lblSlUnit.Tag = "SL_UNIT";
            this.m_lblSlUnit.Text = "단위선택";
            this.m_lblSlUnit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point( 2, 6 );
            this.label5.Name = "label5";
            this.label5.Resizeble = true;
            this.label5.Size = new System.Drawing.Size( 36, 14 );
            this.label5.TabIndex = 0;
            this.label5.Tag = "CD_PLANT";
            this.label5.Text = "공장";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point( 3, 29 );
            this.label6.Name = "label6";
            this.label6.Resizeble = true;
            this.label6.Size = new System.Drawing.Size( 56, 14 );
            this.label6.TabIndex = 0;
            this.label6.Tag = "STA_IV";
            this.label6.Text = "처리상태";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
            this.panel6.Controls.Add( this.m_lblYnAm );
            this.panel6.Location = new System.Drawing.Point( 344, 1 );
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size( 67, 73 );
            this.panel6.TabIndex = 1;
            // 
            // m_lblYnAm
            // 
            this.m_lblYnAm.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
            this.m_lblYnAm.Location = new System.Drawing.Point( 5, 53 );
            this.m_lblYnAm.Name = "m_lblYnAm";
            this.m_lblYnAm.Resizeble = true;
            this.m_lblYnAm.Size = new System.Drawing.Size( 57, 18 );
            this.m_lblYnAm.TabIndex = 0;
            this.m_lblYnAm.Tag = "YN_AM";
            this.m_lblYnAm.Text = "수주형태";
            this.m_lblYnAm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
            this.panel5.Controls.Add( this.m_lblFgGi );
            this.panel5.Controls.Add( this.label2 );
            this.panel5.Controls.Add( this.m_lblDtGir );
            this.panel5.Location = new System.Drawing.Point( 1, 1 );
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size( 80, 73 );
            this.panel5.TabIndex = 0;
            // 
            // m_lblFgGi
            // 
            this.m_lblFgGi.Location = new System.Drawing.Point( 3, 54 );
            this.m_lblFgGi.Name = "m_lblFgGi";
            this.m_lblFgGi.Resizeble = true;
            this.m_lblFgGi.Size = new System.Drawing.Size( 75, 18 );
            this.m_lblFgGi.TabIndex = 0;
            this.m_lblFgGi.Tag = "FG_GI";
            this.m_lblFgGi.Text = "출하구분";
            this.m_lblFgGi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point( 3, 28 );
            this.label2.Name = "label2";
            this.label2.Resizeble = true;
            this.label2.Size = new System.Drawing.Size( 75, 18 );
            this.label2.TabIndex = 0;
            this.label2.Tag = "SL_GIR";
            this.label2.Text = "창고";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblDtGir
            // 
            this.m_lblDtGir.Location = new System.Drawing.Point( 3, 5 );
            this.m_lblDtGir.Name = "m_lblDtGir";
            this.m_lblDtGir.Resizeble = true;
            this.m_lblDtGir.Size = new System.Drawing.Size( 75, 18 );
            this.m_lblDtGir.TabIndex = 0;
            this.m_lblDtGir.Tag = "DT_GIR";
            this.m_lblDtGir.Text = "의뢰일자";
            this.m_lblDtGir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point( 168, 4 );
            this.label7.Name = "label7";
            this.label7.Resizeble = true;
            this.label7.Size = new System.Drawing.Size( 18, 18 );
            this.label7.TabIndex = 0;
            this.label7.Text = "∼";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboPlantGir
            // 
            this.cboPlantGir.AutoDropDown = true;
            this.cboPlantGir.BackColor = System.Drawing.Color.White;
            this.cboPlantGir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPlantGir.Location = new System.Drawing.Point( 650, 3 );
            this.cboPlantGir.Name = "cboPlantGir";
            this.cboPlantGir.ShowCheckBox = false;
            this.cboPlantGir.Size = new System.Drawing.Size( 130, 20 );
            this.cboPlantGir.TabIndex = 3;
            this.cboPlantGir.UseKeyEnter = false;
            this.cboPlantGir.UseKeyF3 = false;
            this.cboPlantGir.KeyDown += new System.Windows.Forms.KeyEventHandler( this.CommonComboBox_KeyEvent );
            // 
            // cboTpBusi
            // 
            this.cboTpBusi.AutoDropDown = true;
            this.cboTpBusi.BackColor = System.Drawing.Color.White;
            this.cboTpBusi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTpBusi.Location = new System.Drawing.Point( 414, 27 );
            this.cboTpBusi.Name = "cboTpBusi";
            this.cboTpBusi.ShowCheckBox = false;
            this.cboTpBusi.Size = new System.Drawing.Size( 160, 20 );
            this.cboTpBusi.TabIndex = 5;
            this.cboTpBusi.UseKeyEnter = false;
            this.cboTpBusi.UseKeyF3 = false;
            this.cboTpBusi.KeyDown += new System.Windows.Forms.KeyEventHandler( this.CommonComboBox_KeyEvent );
            // 
            // cboStaGir
            // 
            this.cboStaGir.AutoDropDown = true;
            this.cboStaGir.BackColor = System.Drawing.Color.White;
            this.cboStaGir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStaGir.Location = new System.Drawing.Point( 650, 28 );
            this.cboStaGir.Name = "cboStaGir";
            this.cboStaGir.ShowCheckBox = false;
            this.cboStaGir.Size = new System.Drawing.Size( 130, 20 );
            this.cboStaGir.TabIndex = 6;
            this.cboStaGir.UseKeyEnter = false;
            this.cboStaGir.UseKeyF3 = false;
            this.cboStaGir.KeyDown += new System.Windows.Forms.KeyEventHandler( this.CommonComboBox_KeyEvent );
            // 
            // m_cboFgGi
            // 
            this.m_cboFgGi.AutoDropDown = true;
            this.m_cboFgGi.BackColor = System.Drawing.Color.White;
            this.m_cboFgGi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboFgGi.Location = new System.Drawing.Point( 84, 53 );
            this.m_cboFgGi.Name = "m_cboFgGi";
            this.m_cboFgGi.ShowCheckBox = false;
            this.m_cboFgGi.Size = new System.Drawing.Size( 102, 20 );
            this.m_cboFgGi.TabIndex = 7;
            this.m_cboFgGi.UseKeyEnter = false;
            this.m_cboFgGi.UseKeyF3 = false;
            this.m_cboFgGi.KeyDown += new System.Windows.Forms.KeyEventHandler( this.CommonComboBox_KeyEvent );
            // 
            // m_cboSlUnit
            // 
            this.m_cboSlUnit.AutoDropDown = true;
            this.m_cboSlUnit.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 226 ) ) ) ), ( ( int )( ( ( byte )( 239 ) ) ) ), ( ( int )( ( ( byte )( 243 ) ) ) ) );
            this.m_cboSlUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboSlUnit.Location = new System.Drawing.Point( 650, 53 );
            this.m_cboSlUnit.Name = "m_cboSlUnit";
            this.m_cboSlUnit.ShowCheckBox = false;
            this.m_cboSlUnit.Size = new System.Drawing.Size( 130, 20 );
            this.m_cboSlUnit.TabIndex = 9;
            this.m_cboSlUnit.UseKeyEnter = false;
            this.m_cboSlUnit.UseKeyF3 = false;
            this.m_cboSlUnit.KeyDown += new System.Windows.Forms.KeyEventHandler( this.CommonComboBox_KeyEvent );
            // 
            // panelExt2
            // 
            this.panelExt2.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
            this.panelExt2.Controls.Add( this.labelExt3 );
            this.panelExt2.Controls.Add( this.labelExt1 );
            this.panelExt2.Location = new System.Drawing.Point( 189, 25 );
            this.panelExt2.Name = "panelExt2";
            this.panelExt2.Size = new System.Drawing.Size( 62, 49 );
            this.panelExt2.TabIndex = 1;
            // 
            // labelExt3
            // 
            this.labelExt3.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
            this.labelExt3.Location = new System.Drawing.Point( 4, 5 );
            this.labelExt3.Name = "labelExt3";
            this.labelExt3.Resizeble = true;
            this.labelExt3.Size = new System.Drawing.Size( 54, 17 );
            this.labelExt3.TabIndex = 0;
            this.labelExt3.Tag = "STA_IV";
            this.labelExt3.Text = "계정구분";
            this.labelExt3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelExt1
            // 
            this.labelExt1.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
            this.labelExt1.Location = new System.Drawing.Point( 3, 29 );
            this.labelExt1.Name = "labelExt1";
            this.labelExt1.Resizeble = true;
            this.labelExt1.Size = new System.Drawing.Size( 54, 17 );
            this.labelExt1.TabIndex = 15;
            this.labelExt1.Tag = "STA_IV";
            this.labelExt1.Text = "운송방법";
            this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_tabC
            // 
            this.m_tabC.Controls.Add( this.tab1 );
            this.m_tabC.Controls.Add( this.tab2 );
            this.m_tabC.Controls.Add( this.tab3 );
            this.m_tabC.Controls.Add( this.tab4 );
            this.m_tabC.Controls.Add( this.tab5 );
            this.m_tabC.Controls.Add( this.tab6 );
            this.m_tabC.Controls.Add( this.tab7 );
            this.m_tabC.Controls.Add( this.tab8 );
            this.m_tabC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_tabC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tabC.ImageList = this.imageList1;
            this.m_tabC.ItemSize = new System.Drawing.Size( 111, 20 );
            this.m_tabC.Location = new System.Drawing.Point( 3, 86 );
            this.m_tabC.Name = "m_tabC";
            this.m_tabC.SelectedIndex = 0;
            this.m_tabC.Size = new System.Drawing.Size( 787, 488 );
            this.m_tabC.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.m_tabC.TabIndex = 79;
            this.m_tabC.Click += new System.EventHandler( this.m_tabWork_Click );
            // 
            // tab1
            // 
            this.tab1.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
            this.tab1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tab1.Controls.Add( this.splitContainer1 );
            this.tab1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tab1.ImageIndex = 0;
            this.tab1.Location = new System.Drawing.Point( 4, 24 );
            this.tab1.Name = "tab1";
            this.tab1.Size = new System.Drawing.Size( 779, 460 );
            this.tab1.TabIndex = 0;
            this.tab1.Text = "의뢰번호별";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point( 0, 0 );
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add( this._flexM_1 );
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add( this._flexD_1 );
            this.splitContainer1.Size = new System.Drawing.Size( 775, 456 );
            this.splitContainer1.SplitterDistance = 248;
            this.splitContainer1.TabIndex = 2;
            // 
            // _flexM_1
            // 
            //this._flexM_1.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            //this._flexM_1.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            //this._flexM_1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexM_1.AutoResize = false;
            this._flexM_1.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexM_1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexM_1.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexM_1.EnabledHeaderCheck = true;
            this._flexM_1.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexM_1.Location = new System.Drawing.Point( 0, 0 );
            this._flexM_1.Name = "_flexM_1";
            this._flexM_1.Rows.Count = 1;
            this._flexM_1.Rows.DefaultSize = 20;
            this._flexM_1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexM_1.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexM_1.ShowSort = false;
            this._flexM_1.Size = new System.Drawing.Size( 775, 248 );
            this._flexM_1.StyleInfo = resources.GetString( "_flexM_1.StyleInfo" );
            this._flexM_1.TabIndex = 0;
            // 
            // _flexD_1
            // 
            this._flexD_1.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexD_1.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexD_1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexD_1.AutoResize = false;
            this._flexD_1.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexD_1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexD_1.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexD_1.EnabledHeaderCheck = true;
            this._flexD_1.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexD_1.Location = new System.Drawing.Point( 0, 0 );
            this._flexD_1.Name = "_flexD_1";
            this._flexD_1.Rows.Count = 1;
            this._flexD_1.Rows.DefaultSize = 20;
            this._flexD_1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexD_1.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexD_1.ShowSort = false;
            this._flexD_1.Size = new System.Drawing.Size( 775, 204 );
            this._flexD_1.StyleInfo = resources.GetString( "_flexD_1.StyleInfo" );
            this._flexD_1.TabIndex = 1;
            // 
            // tab2
            // 
            this.tab2.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
            this.tab2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tab2.Controls.Add( this.pnlPage3_2 );
            this.tab2.Controls.Add( this.pnlPage3_1 );
            this.tab2.Cursor = System.Windows.Forms.Cursors.Default;
            this.tab2.ImageIndex = 2;
            this.tab2.Location = new System.Drawing.Point( 4, 24 );
            this.tab2.Name = "tab2";
            this.tab2.Size = new System.Drawing.Size( 779, 460 );
            this.tab2.TabIndex = 2;
            this.tab2.Text = "품목코드별";
            this.tab2.Visible = false;
            // 
            // pnlPage3_2
            // 
            this.pnlPage3_2.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.pnlPage3_2.Controls.Add( this._flexD_2 );
            this.pnlPage3_2.Location = new System.Drawing.Point( 253, 0 );
            this.pnlPage3_2.Name = "pnlPage3_2";
            this.pnlPage3_2.Size = new System.Drawing.Size( 522, 456 );
            this.pnlPage3_2.TabIndex = 1;
            // 
            // _flexD_2
            // 
            this._flexD_2.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexD_2.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexD_2.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexD_2.AutoResize = false;
            this._flexD_2.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexD_2.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexD_2.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexD_2.EnabledHeaderCheck = true;
            this._flexD_2.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexD_2.Location = new System.Drawing.Point( 0, 0 );
            this._flexD_2.Name = "_flexD_2";
            this._flexD_2.Rows.Count = 1;
            this._flexD_2.Rows.DefaultSize = 20;
            this._flexD_2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexD_2.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexD_2.ShowSort = false;
            this._flexD_2.Size = new System.Drawing.Size( 522, 456 );
            this._flexD_2.StyleInfo = resources.GetString( "_flexD_2.StyleInfo" );
            this._flexD_2.TabIndex = 1;
            // 
            // pnlPage3_1
            // 
            this.pnlPage3_1.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left ) ) );
            this.pnlPage3_1.Controls.Add( this._flexM_2 );
            this.pnlPage3_1.Location = new System.Drawing.Point( 0, 0 );
            this.pnlPage3_1.Name = "pnlPage3_1";
            this.pnlPage3_1.Size = new System.Drawing.Size( 250, 456 );
            this.pnlPage3_1.TabIndex = 0;
            // 
            // _flexM_2
            // 
            this._flexM_2.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexM_2.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexM_2.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexM_2.AutoResize = false;
            this._flexM_2.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexM_2.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexM_2.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexM_2.EnabledHeaderCheck = true;
            this._flexM_2.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexM_2.Location = new System.Drawing.Point( 0, 0 );
            this._flexM_2.Name = "_flexM_2";
            this._flexM_2.Rows.Count = 1;
            this._flexM_2.Rows.DefaultSize = 20;
            this._flexM_2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexM_2.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexM_2.ShowSort = false;
            this._flexM_2.Size = new System.Drawing.Size( 250, 456 );
            this._flexM_2.StyleInfo = resources.GetString( "_flexM_2.StyleInfo" );
            this._flexM_2.TabIndex = 0;
            // 
            // tab3
            // 
            this.tab3.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
            this.tab3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tab3.Controls.Add( this.pnlPage2_2 );
            this.tab3.Controls.Add( this.pnlPage2_1 );
            this.tab3.Cursor = System.Windows.Forms.Cursors.Default;
            this.tab3.ImageIndex = 1;
            this.tab3.Location = new System.Drawing.Point( 4, 24 );
            this.tab3.Name = "tab3";
            this.tab3.Size = new System.Drawing.Size( 779, 460 );
            this.tab3.TabIndex = 1;
            this.tab3.Text = "거래처별";
            this.tab3.Visible = false;
            // 
            // pnlPage2_2
            // 
            this.pnlPage2_2.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.pnlPage2_2.Controls.Add( this._flexD_3 );
            this.pnlPage2_2.Location = new System.Drawing.Point( 253, 0 );
            this.pnlPage2_2.Name = "pnlPage2_2";
            this.pnlPage2_2.Size = new System.Drawing.Size( 522, 456 );
            this.pnlPage2_2.TabIndex = 1;
            // 
            // _flexD_3
            // 
            this._flexD_3.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexD_3.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexD_3.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexD_3.AutoResize = false;
            this._flexD_3.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexD_3.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexD_3.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexD_3.EnabledHeaderCheck = true;
            this._flexD_3.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexD_3.Location = new System.Drawing.Point( 0, 0 );
            this._flexD_3.Name = "_flexD_3";
            this._flexD_3.Rows.Count = 1;
            this._flexD_3.Rows.DefaultSize = 20;
            this._flexD_3.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexD_3.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexD_3.ShowSort = false;
            this._flexD_3.Size = new System.Drawing.Size( 522, 456 );
            this._flexD_3.StyleInfo = resources.GetString( "_flexD_3.StyleInfo" );
            this._flexD_3.TabIndex = 1;
            // 
            // pnlPage2_1
            // 
            this.pnlPage2_1.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left ) ) );
            this.pnlPage2_1.Controls.Add( this._flexM_3 );
            this.pnlPage2_1.Location = new System.Drawing.Point( 0, 0 );
            this.pnlPage2_1.Name = "pnlPage2_1";
            this.pnlPage2_1.Size = new System.Drawing.Size( 250, 456 );
            this.pnlPage2_1.TabIndex = 0;
            // 
            // _flexM_3
            // 
            this._flexM_3.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexM_3.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexM_3.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexM_3.AutoResize = false;
            this._flexM_3.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexM_3.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexM_3.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexM_3.EnabledHeaderCheck = true;
            this._flexM_3.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexM_3.Location = new System.Drawing.Point( 0, 0 );
            this._flexM_3.Name = "_flexM_3";
            this._flexM_3.Rows.Count = 1;
            this._flexM_3.Rows.DefaultSize = 20;
            this._flexM_3.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexM_3.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexM_3.ShowSort = false;
            this._flexM_3.Size = new System.Drawing.Size( 250, 456 );
            this._flexM_3.StyleInfo = resources.GetString( "_flexM_3.StyleInfo" );
            this._flexM_3.TabIndex = 0;
            // 
            // tab4
            // 
            this.tab4.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
            this.tab4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tab4.Controls.Add( this.pnlPage4_2 );
            this.tab4.Controls.Add( this.pnlPage4_1 );
            this.tab4.Cursor = System.Windows.Forms.Cursors.Default;
            this.tab4.ImageIndex = 3;
            this.tab4.Location = new System.Drawing.Point( 4, 24 );
            this.tab4.Name = "tab4";
            this.tab4.Size = new System.Drawing.Size( 779, 460 );
            this.tab4.TabIndex = 3;
            this.tab4.Text = "영업그룹별";
            this.tab4.Visible = false;
            // 
            // pnlPage4_2
            // 
            this.pnlPage4_2.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.pnlPage4_2.Controls.Add( this._flexD_4 );
            this.pnlPage4_2.Location = new System.Drawing.Point( 253, 0 );
            this.pnlPage4_2.Name = "pnlPage4_2";
            this.pnlPage4_2.Size = new System.Drawing.Size( 522, 456 );
            this.pnlPage4_2.TabIndex = 1;
            // 
            // _flexD_4
            // 
            this._flexD_4.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexD_4.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexD_4.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexD_4.AutoResize = false;
            this._flexD_4.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexD_4.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexD_4.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexD_4.EnabledHeaderCheck = true;
            this._flexD_4.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexD_4.Location = new System.Drawing.Point( 0, 0 );
            this._flexD_4.Name = "_flexD_4";
            this._flexD_4.Rows.Count = 1;
            this._flexD_4.Rows.DefaultSize = 20;
            this._flexD_4.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexD_4.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexD_4.ShowSort = false;
            this._flexD_4.Size = new System.Drawing.Size( 522, 456 );
            this._flexD_4.StyleInfo = resources.GetString( "_flexD_4.StyleInfo" );
            this._flexD_4.TabIndex = 2;
            // 
            // pnlPage4_1
            // 
            this.pnlPage4_1.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left ) ) );
            this.pnlPage4_1.Controls.Add( this._flexM_4 );
            this.pnlPage4_1.Location = new System.Drawing.Point( 0, 0 );
            this.pnlPage4_1.Name = "pnlPage4_1";
            this.pnlPage4_1.Size = new System.Drawing.Size( 250, 456 );
            this.pnlPage4_1.TabIndex = 0;
            // 
            // _flexM_4
            // 
            this._flexM_4.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexM_4.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexM_4.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexM_4.AutoResize = false;
            this._flexM_4.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexM_4.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexM_4.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexM_4.EnabledHeaderCheck = true;
            this._flexM_4.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexM_4.Location = new System.Drawing.Point( 0, 0 );
            this._flexM_4.Name = "_flexM_4";
            this._flexM_4.Rows.Count = 1;
            this._flexM_4.Rows.DefaultSize = 20;
            this._flexM_4.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexM_4.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexM_4.ShowSort = false;
            this._flexM_4.Size = new System.Drawing.Size( 250, 456 );
            this._flexM_4.StyleInfo = resources.GetString( "_flexM_4.StyleInfo" );
            this._flexM_4.TabIndex = 1;
            // 
            // tab5
            // 
            this.tab5.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
            this.tab5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tab5.Controls.Add( this.pnlPage5_2 );
            this.tab5.Controls.Add( this.pnlPage5_1 );
            this.tab5.Cursor = System.Windows.Forms.Cursors.Default;
            this.tab5.ImageIndex = 5;
            this.tab5.Location = new System.Drawing.Point( 4, 24 );
            this.tab5.Name = "tab5";
            this.tab5.Size = new System.Drawing.Size( 779, 460 );
            this.tab5.TabIndex = 5;
            this.tab5.Text = "의뢰일자별";
            this.tab5.Visible = false;
            // 
            // pnlPage5_2
            // 
            this.pnlPage5_2.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.pnlPage5_2.Controls.Add( this._flexD_5 );
            this.pnlPage5_2.Location = new System.Drawing.Point( 183, 0 );
            this.pnlPage5_2.Name = "pnlPage5_2";
            this.pnlPage5_2.Size = new System.Drawing.Size( 592, 456 );
            this.pnlPage5_2.TabIndex = 1;
            // 
            // _flexD_5
            // 
            this._flexD_5.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexD_5.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexD_5.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexD_5.AutoResize = false;
            this._flexD_5.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexD_5.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexD_5.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexD_5.EnabledHeaderCheck = true;
            this._flexD_5.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexD_5.Location = new System.Drawing.Point( 0, 0 );
            this._flexD_5.Name = "_flexD_5";
            this._flexD_5.Rows.Count = 1;
            this._flexD_5.Rows.DefaultSize = 20;
            this._flexD_5.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexD_5.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexD_5.ShowSort = false;
            this._flexD_5.Size = new System.Drawing.Size( 592, 456 );
            this._flexD_5.StyleInfo = resources.GetString( "_flexD_5.StyleInfo" );
            this._flexD_5.TabIndex = 1;
            // 
            // pnlPage5_1
            // 
            this.pnlPage5_1.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left ) ) );
            this.pnlPage5_1.Controls.Add( this._flexM_5 );
            this.pnlPage5_1.Location = new System.Drawing.Point( 0, 0 );
            this.pnlPage5_1.Name = "pnlPage5_1";
            this.pnlPage5_1.Size = new System.Drawing.Size( 180, 456 );
            this.pnlPage5_1.TabIndex = 0;
            // 
            // _flexM_5
            // 
            this._flexM_5.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexM_5.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexM_5.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexM_5.AutoResize = false;
            this._flexM_5.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexM_5.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexM_5.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexM_5.EnabledHeaderCheck = true;
            this._flexM_5.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexM_5.Location = new System.Drawing.Point( 0, 0 );
            this._flexM_5.Name = "_flexM_5";
            this._flexM_5.Rows.Count = 1;
            this._flexM_5.Rows.DefaultSize = 20;
            this._flexM_5.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexM_5.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexM_5.ShowSort = false;
            this._flexM_5.Size = new System.Drawing.Size( 180, 456 );
            this._flexM_5.StyleInfo = resources.GetString( "_flexM_5.StyleInfo" );
            this._flexM_5.TabIndex = 1;
            // 
            // tab6
            // 
            this.tab6.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
            this.tab6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tab6.Controls.Add( this.pnlPage6_2 );
            this.tab6.Controls.Add( this.pnlPage6_1 );
            this.tab6.Cursor = System.Windows.Forms.Cursors.Default;
            this.tab6.ImageIndex = 4;
            this.tab6.Location = new System.Drawing.Point( 4, 24 );
            this.tab6.Name = "tab6";
            this.tab6.Size = new System.Drawing.Size( 779, 460 );
            this.tab6.TabIndex = 4;
            this.tab6.Text = "출하형태별";
            this.tab6.Visible = false;
            // 
            // pnlPage6_2
            // 
            this.pnlPage6_2.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.pnlPage6_2.Controls.Add( this._flexD_6 );
            this.pnlPage6_2.Location = new System.Drawing.Point( 253, 0 );
            this.pnlPage6_2.Name = "pnlPage6_2";
            this.pnlPage6_2.Size = new System.Drawing.Size( 522, 456 );
            this.pnlPage6_2.TabIndex = 1;
            // 
            // pnlPage6_1
            // 
            this.pnlPage6_1.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left ) ) );
            this.pnlPage6_1.Controls.Add( this._flexM_6 );
            this.pnlPage6_1.Location = new System.Drawing.Point( 0, 0 );
            this.pnlPage6_1.Name = "pnlPage6_1";
            this.pnlPage6_1.Size = new System.Drawing.Size( 250, 456 );
            this.pnlPage6_1.TabIndex = 0;
            // 
            // _flexM_6
            // 
            this._flexM_6.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexM_6.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexM_6.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexM_6.AutoResize = false;
            this._flexM_6.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexM_6.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexM_6.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexM_6.EnabledHeaderCheck = true;
            this._flexM_6.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexM_6.Location = new System.Drawing.Point( 0, 0 );
            this._flexM_6.Name = "_flexM_6";
            this._flexM_6.Rows.Count = 1;
            this._flexM_6.Rows.DefaultSize = 20;
            this._flexM_6.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexM_6.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexM_6.ShowSort = false;
            this._flexM_6.Size = new System.Drawing.Size( 250, 456 );
            this._flexM_6.StyleInfo = resources.GetString( "_flexM_6.StyleInfo" );
            this._flexM_6.TabIndex = 1;
            // 
            // tab7
            // 
            this.tab7.BackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ), ( ( int )( ( ( byte )( 234 ) ) ) ) );
            this.tab7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tab7.Controls.Add( this.pnlPage7_2 );
            this.tab7.Controls.Add( this.pnlPage7_1 );
            this.tab7.Cursor = System.Windows.Forms.Cursors.Default;
            this.tab7.ImageIndex = 6;
            this.tab7.Location = new System.Drawing.Point( 4, 24 );
            this.tab7.Name = "tab7";
            this.tab7.Size = new System.Drawing.Size( 779, 460 );
            this.tab7.TabIndex = 6;
            this.tab7.Text = "프로젝트별";
            this.tab7.Visible = false;
            // 
            // pnlPage7_2
            // 
            this.pnlPage7_2.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.pnlPage7_2.Controls.Add( this._flexD_7 );
            this.pnlPage7_2.Location = new System.Drawing.Point( 253, 0 );
            this.pnlPage7_2.Name = "pnlPage7_2";
            this.pnlPage7_2.Size = new System.Drawing.Size( 522, 456 );
            this.pnlPage7_2.TabIndex = 1;
            // 
            // _flexD_7
            // 
            this._flexD_7.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexD_7.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexD_7.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexD_7.AutoResize = false;
            this._flexD_7.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexD_7.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexD_7.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexD_7.EnabledHeaderCheck = true;
            this._flexD_7.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexD_7.Location = new System.Drawing.Point( 0, 0 );
            this._flexD_7.Name = "_flexD_7";
            this._flexD_7.Rows.Count = 1;
            this._flexD_7.Rows.DefaultSize = 20;
            this._flexD_7.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexD_7.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexD_7.ShowSort = false;
            this._flexD_7.Size = new System.Drawing.Size( 522, 456 );
            this._flexD_7.StyleInfo = resources.GetString( "_flexD_7.StyleInfo" );
            this._flexD_7.TabIndex = 1;
            // 
            // pnlPage7_1
            // 
            this.pnlPage7_1.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left ) ) );
            this.pnlPage7_1.Controls.Add( this._flexM_7 );
            this.pnlPage7_1.Location = new System.Drawing.Point( 0, 0 );
            this.pnlPage7_1.Name = "pnlPage7_1";
            this.pnlPage7_1.Size = new System.Drawing.Size( 250, 456 );
            this.pnlPage7_1.TabIndex = 0;
            // 
            // _flexM_7
            // 
            this._flexM_7.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexM_7.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexM_7.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexM_7.AutoResize = false;
            this._flexM_7.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexM_7.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexM_7.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexM_7.EnabledHeaderCheck = true;
            this._flexM_7.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexM_7.Location = new System.Drawing.Point( 0, 0 );
            this._flexM_7.Name = "_flexM_7";
            this._flexM_7.Rows.Count = 1;
            this._flexM_7.Rows.DefaultSize = 20;
            this._flexM_7.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexM_7.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexM_7.ShowSort = false;
            this._flexM_7.Size = new System.Drawing.Size( 250, 456 );
            this._flexM_7.StyleInfo = resources.GetString( "_flexM_7.StyleInfo" );
            this._flexM_7.TabIndex = 1;
            // 
            // tab8
            // 
            this.tab8.Controls.Add( this.pnlPage8_2 );
            this.tab8.Location = new System.Drawing.Point( 4, 24 );
            this.tab8.Name = "tab8";
            this.tab8.Padding = new System.Windows.Forms.Padding( 3 );
            this.tab8.Size = new System.Drawing.Size( 779, 460 );
            this.tab8.TabIndex = 7;
            this.tab8.Text = "품목집계";
            this.tab8.UseVisualStyleBackColor = true;
            // 
            // pnlPage8_2
            // 
            this.pnlPage8_2.Controls.Add( this._flexD_8 );
            this.pnlPage8_2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPage8_2.Location = new System.Drawing.Point( 3, 3 );
            this.pnlPage8_2.Name = "pnlPage8_2";
            this.pnlPage8_2.Size = new System.Drawing.Size( 773, 454 );
            this.pnlPage8_2.TabIndex = 2;
            // 
            // _flexD_8
            // 
            this._flexD_8.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexD_8.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexD_8.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexD_8.AutoResize = false;
            this._flexD_8.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexD_8.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexD_8.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexD_8.EnabledHeaderCheck = true;
            this._flexD_8.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexD_8.Location = new System.Drawing.Point( 0, 0 );
            this._flexD_8.Name = "_flexD_8";
            this._flexD_8.Rows.Count = 1;
            this._flexD_8.Rows.DefaultSize = 20;
            this._flexD_8.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexD_8.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexD_8.ShowSort = false;
            this._flexD_8.Size = new System.Drawing.Size( 773, 454 );
            this._flexD_8.StyleInfo = resources.GetString( "_flexD_8.StyleInfo" );
            this._flexD_8.TabIndex = 1;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ( ( System.Windows.Forms.ImageListStreamer )( resources.GetObject( "imageList1.ImageStream" ) ) );
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName( 0, "" );
            this.imageList1.Images.SetKeyName( 1, "" );
            this.imageList1.Images.SetKeyName( 2, "" );
            this.imageList1.Images.SetKeyName( 3, "" );
            this.imageList1.Images.SetKeyName( 4, "" );
            this.imageList1.Images.SetKeyName( 5, "" );
            this.imageList1.Images.SetKeyName( 6, "" );
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 100F ) );
            this.tableLayoutPanel1.Controls.Add( this.panel4, 0, 0 );
            this.tableLayoutPanel1.Controls.Add( this.m_tabC, 0, 1 );
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point( 0, 0 );
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 100F ) );
            this.tableLayoutPanel1.Size = new System.Drawing.Size( 793, 577 );
            this.tableLayoutPanel1.TabIndex = 80;
            // 
            // _flexD_6
            // 
            this._flexD_6.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexD_6.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexD_6.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexD_6.AutoResize = false;
            this._flexD_6.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexD_6.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexD_6.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexD_6.EnabledHeaderCheck = true;
            this._flexD_6.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexD_6.Location = new System.Drawing.Point( 0, 0 );
            this._flexD_6.Name = "_flexD_6";
            this._flexD_6.Rows.Count = 1;
            this._flexD_6.Rows.DefaultSize = 20;
            this._flexD_6.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexD_6.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexD_6.ShowSort = false;
            this._flexD_6.Size = new System.Drawing.Size( 522, 456 );
            this._flexD_6.StyleInfo = resources.GetString( "_flexD_6.StyleInfo" );
            this._flexD_6.TabIndex = 2;
            // 
            // P_SA_GIRSCH
            // 
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Name = "P_SA_GIRSCH";
            this.mDataArea.ResumeLayout( false );
            this.panel4.ResumeLayout( false );
            ( ( System.ComponentModel.ISupportInitialize )( this.mskDtGirEnd ) ).EndInit();
            ( ( System.ComponentModel.ISupportInitialize )( this.mskDtGirStart ) ).EndInit();
            this.panel10.ResumeLayout( false );
            this.panel7.ResumeLayout( false );
            this.panel6.ResumeLayout( false );
            this.panel5.ResumeLayout( false );
            this.panelExt2.ResumeLayout( false );
            this.m_tabC.ResumeLayout( false );
            this.tab1.ResumeLayout( false );
            this.splitContainer1.Panel1.ResumeLayout( false );
            this.splitContainer1.Panel2.ResumeLayout( false );
            this.splitContainer1.ResumeLayout( false );
            ( ( System.ComponentModel.ISupportInitialize )( this._flexM_1 ) ).EndInit();
            ( ( System.ComponentModel.ISupportInitialize )( this._flexD_1 ) ).EndInit();
            this.tab2.ResumeLayout( false );
            this.pnlPage3_2.ResumeLayout( false );
            ( ( System.ComponentModel.ISupportInitialize )( this._flexD_2 ) ).EndInit();
            this.pnlPage3_1.ResumeLayout( false );
            ( ( System.ComponentModel.ISupportInitialize )( this._flexM_2 ) ).EndInit();
            this.tab3.ResumeLayout( false );
            this.pnlPage2_2.ResumeLayout( false );
            ( ( System.ComponentModel.ISupportInitialize )( this._flexD_3 ) ).EndInit();
            this.pnlPage2_1.ResumeLayout( false );
            ( ( System.ComponentModel.ISupportInitialize )( this._flexM_3 ) ).EndInit();
            this.tab4.ResumeLayout( false );
            this.pnlPage4_2.ResumeLayout( false );
            ( ( System.ComponentModel.ISupportInitialize )( this._flexD_4 ) ).EndInit();
            this.pnlPage4_1.ResumeLayout( false );
            ( ( System.ComponentModel.ISupportInitialize )( this._flexM_4 ) ).EndInit();
            this.tab5.ResumeLayout( false );
            this.pnlPage5_2.ResumeLayout( false );
            ( ( System.ComponentModel.ISupportInitialize )( this._flexD_5 ) ).EndInit();
            this.pnlPage5_1.ResumeLayout( false );
            ( ( System.ComponentModel.ISupportInitialize )( this._flexM_5 ) ).EndInit();
            this.tab6.ResumeLayout( false );
            this.pnlPage6_2.ResumeLayout( false );
            this.pnlPage6_1.ResumeLayout( false );
            ( ( System.ComponentModel.ISupportInitialize )( this._flexM_6 ) ).EndInit();
            this.tab7.ResumeLayout( false );
            this.pnlPage7_2.ResumeLayout( false );
            ( ( System.ComponentModel.ISupportInitialize )( this._flexD_7 ) ).EndInit();
            this.pnlPage7_1.ResumeLayout( false );
            ( ( System.ComponentModel.ISupportInitialize )( this._flexM_7 ) ).EndInit();
            this.tab8.ResumeLayout( false );
            this.pnlPage8_2.ResumeLayout( false );
            ( ( System.ComponentModel.ISupportInitialize )( this._flexD_8 ) ).EndInit();
            this.tableLayoutPanel1.ResumeLayout( false );
            ( ( System.ComponentModel.ISupportInitialize )( this._flexD_6 ) ).EndInit();
            this.ResumeLayout( false );

		}
		#endregion

		#endregion
		
		#region ♣ 초기화

		#region -> Page_Load

		private void Page_Load(object sender, EventArgs e)
		{
			try
			{
                _biz = new P_SA_GIRSCH_BIZ(this.MainFrameInterface);

				this.Enabled = false;
				//this.m_lblTitle.Visible = false;

				this.ShowStatusBarMessage(1);
				
				InitGridM_1();
				InitGridD_1();
			
				InitGridM_2();
				InitGridD_2();
			
				InitGridM_3();
				InitGridD_3();
				
				InitGridM_4();
				InitGridD_4();
				
				InitGridM_5();
				InitGridD_5();
				
				InitGridM_6();
				InitGridD_6();
				
				InitGridM_7();
				InitGridD_7();

                InitGridD_8();

				InitControl();
			}
			catch(Exception ex)
			{	
				MsgEnd(ex);
			}
		}

		#endregion

		#region -> Page_Paint

		private void Page_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			try
			{
				if(this._isPainted)
					return;
				
				this._isPainted = true;
				Application.DoEvents();

                //this.m_lblTitle.Visible = true;
                //this.m_lblTitle.Text	= this.PageName;
                //this.m_lblTitle.Show();

				// 콤보박스 초기화
				InitCombo();
				Application.DoEvents();

				mskDtGirStart.Text = DateTime.Now.AddMonths(-1).ToShortDateString();
				mskDtGirEnd.Text = DateTime.Now.ToShortDateString();
				ShowStatusBarMessage(0);
				
				this.Enabled = true; //페이지 전체 활성
				
				mskDtGirStart.Focus();
				
			}
			catch(Exception ex)
			{
				MsgEnd(ex);
			}
			finally
			{
				this.ToolBarSearchButtonEnabled = true;
			}
		}

		
		#endregion

		#region -> InitControl

		/// <summary>
		/// 컨트롤들의 캡션을 데이터 사전을 이용하여 설정한다.
		/// </summary>
		private void InitControl()
		{
			foreach(Control ctr in this.Controls)
			{
				if(ctr is Panel)
				{
					foreach(Control ctrl in ((Panel)ctr).Controls)
					{
						if(ctrl is Panel)
						{
							foreach(Control ctrls in ((Panel)ctrl).Controls)
							{
								if(ctrls is Label)
									((Label)ctrls).Text = GetDataDictionaryItem(DataDictionaryTypes.SA, (string)((Label)ctrls).Tag);
							}
						}
					}
				}
			}

			bpNm_Sl.Tag ="";
		}


		#endregion

		#region -> InitCombo
		/// <summary>
		/// 콤보 초기화
		/// </summary>
		private void InitCombo()
		{
			try
			{
                m_dsCombo = this.GetComboData( "S;PU_C000016", "S;SA_B000028", "N;MA_PLANT", "S;PU_C000027", "N;SA_B000031", "S;MA_BIZAREA", "S;TR_IM00008", "S;MA_B000010" );

				//거래구분ComboBox에 Add
				cboTpBusi.DataSource = m_dsCombo.Tables[0];
				cboTpBusi.ValueMember = "CODE";
				cboTpBusi.DisplayMember = "NAME";

                //처리상태ComboBox에 Add  --  SA_B000016
				cboStaGir.DataSource = m_dsCombo.Tables[1];
				cboStaGir.ValueMember = "CODE";
				cboStaGir.DisplayMember = "NAME";

				//출하공장ComboBox에 Add
				cboPlantGir.DataSource = m_dsCombo.Tables[2];
				cboPlantGir.ValueMember = "CODE";
				cboPlantGir.DisplayMember = "NAME";

				if(this.LoginInfo.CdPlant.ToString() != "")	
					cboPlantGir.SelectedValue = this.LoginInfo.CdPlant.ToString();
				else
					cboPlantGir.SelectedIndex  = 0;
				
				//출하구분ComboBox에 Add
				m_cboFgGi.DataSource = m_dsCombo.Tables[3];
				m_cboFgGi.ValueMember = "CODE";
				m_cboFgGi.DisplayMember = "NAME";

				//단위선택ComboBox에 Add
				m_cboSlUnit.DataSource = m_dsCombo.Tables[4];
				m_cboSlUnit.ValueMember = "CODE";
				m_cboSlUnit.DisplayMember = "NAME";

				// 사업장
				m_cboBizArea.DataSource = m_dsCombo.Tables[5];
				m_cboBizArea.DisplayMember = "NAME";
				m_cboBizArea.ValueMember = "CODE";

                // 운송수단
                cboFgTransport.DataSource = m_dsCombo.Tables [6];
                cboFgTransport.DisplayMember = "NAME";
                cboFgTransport.ValueMember = "CODE";

                _flexD_1.SetDataMap( "FG_TRANSPORT", m_dsCombo.Tables [6], "CODE", "NAME" );       
                _flexD_2.SetDataMap( "FG_TRANSPORT", m_dsCombo.Tables [6], "CODE", "NAME" );     
                _flexD_3.SetDataMap( "FG_TRANSPORT", m_dsCombo.Tables [6], "CODE", "NAME" );      
                _flexD_4.SetDataMap( "FG_TRANSPORT", m_dsCombo.Tables [6], "CODE", "NAME" );     
                _flexD_5.SetDataMap( "FG_TRANSPORT", m_dsCombo.Tables [6], "CODE", "NAME" );
                _flexD_6.SetDataMap( "FG_TRANSPORT", m_dsCombo.Tables [6], "CODE", "NAME" );
                _flexD_7.SetDataMap( "FG_TRANSPORT", m_dsCombo.Tables [6], "CODE", "NAME" );      
              

                // 계정구분
                this.m_cboClsItemS.DataSource = m_dsCombo.Tables [7];
                this.m_cboClsItemS.DisplayMember = "NAME";
                this.m_cboClsItemS.ValueMember = "CODE";

                _flexD_1.SetDataMap( "CLS_ITEM", m_dsCombo.Tables [7], "CODE", "NAME" );     
                _flexD_2.SetDataMap( "CLS_ITEM", m_dsCombo.Tables [7], "CODE", "NAME" );      
                _flexD_3.SetDataMap( "CLS_ITEM", m_dsCombo.Tables [7], "CODE", "NAME" );      
                _flexD_4.SetDataMap( "CLS_ITEM", m_dsCombo.Tables [7], "CODE", "NAME" );      
                _flexD_5.SetDataMap( "CLS_ITEM", m_dsCombo.Tables [7], "CODE", "NAME" );
                _flexD_6.SetDataMap( "CLS_ITEM", m_dsCombo.Tables [7], "CODE", "NAME" );
                _flexD_7.SetDataMap( "CLS_ITEM", m_dsCombo.Tables [7], "CODE", "NAME" );
                _flexD_8.SetDataMap( "CLS_ITEM", m_dsCombo.Tables [7], "CODE", "NAME" );       
              
                

				if(this.LoginInfo.BizAreaCode != string.Empty)
					m_cboBizArea.SelectedValue = this.LoginInfo.BizAreaCode;
				else
					m_cboBizArea.SelectedIndex  = 0;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		#endregion

		#region -> GetDDItem

		private string GetDDItem(params string[] colName)
		{
			string temp = "";
			
			for(int i = 0; i < colName.Length; i++)
			{
				switch(colName[i])		// DataView 의 컬럼이름
				{
					case "S":			// S
						temp = temp + " + " +  "S";
						break;
					case "NO_GIR":		
						temp = temp + " + " + DD("NO_GIR");
						break;
					case "DT_GIR":		
						temp = temp + " + " + DD("DT_GIR");
						break;
					case "NM_PARTNER":		
						temp = temp + " + " + DD("CD_PARTNER");
						break;
					case "NM_PLANT":		
						temp = temp + " + " + DD("PLANT_GIR");
						break;
					case "NM_RETURN":			
						temp = temp + " + " + DD("FG_GI");
						break;
					case "CD_ITEM":		// 품번
						temp = temp + " + " + DD("CD_ITEM");
						break;
					case "NM_ITEM":		// 품명
						temp = temp + " + " + DD("NM_ITEM");
						break;
					case "STND_ITEM":	// 규격
						temp = temp + " + " + DD("STND_ITEM");
						break;
					case "UNIT_IM":		// 단위
					case "UNIT":		// 단위
						temp = temp + " + " + DD("UNIT");
						break;
					case "NM_SL":		// 창고명
						temp = temp + " + " + DD("SL_GIR");
						break;
					case "NM_BUSI":		
						temp = temp + " + " + DD("TP_BUSI");
						break;
					case "QT_GIR":		// 수배의뢰량
						temp = temp + " + " + DD("QT_GIR");
						break;
					case "QT_GI":		// 환종
						temp = temp + " + " + DD("QT_GI");
						break;
					case "NM_EXCH":			// 환종단가
						temp = temp + " + " + DD("CD_EXCH");
						break;
					case "UM":		// 금액
						temp = temp + " + " + DD("PRICE");
						break;					
					case "AM_GIR":		// 부가세
						temp = temp + " + " + DD("AM");
						break;
					case "AM_GIRAMT":		// 재고단위
						temp = temp + " + " + DD("AM_WON");
						break;
					case "NM_QTIOTP":		// 관련수주번호
						temp = temp + " + " + DD("TP_GI");
						break;
					case "NM_SALEGRP":// 납품처
						temp = temp + " + " + DD("CD_SALEGRP");
						break;
					case "DT_DUEDATE":		// 환종
						temp = temp + " + " + DD("DT_DUEDATE");
						break;				
					case "NO_SO":		// 환종
						temp = temp + " + " + DD("NO_SO");
						break;
					case "SEQ_SO":			// 환종단가
						temp = temp + " + " + DD("SEQ");
						break;
					case "TP_GI":			// 출하형태
						temp = temp + " + " + DD("TP_GI");
						break;
					case "CD_SALEGRP":			// 환종단가
						temp = temp + " + " + DD("CD_SALEGRP");
						break;
					case "CD_PARTNER":			// 환종단가
						temp = temp + " + " + DD("CD_PARTNER");
						break;
					case "LN_PARTNER":			// 환종단가
						temp = temp + " + " + DD("NM_PARTNER");
						break;
					case "NO_PROJECT":			// 환종단가
						temp = temp + " + " + DD("NO_PROJECT");
						break;
					case "NM_PROJECT":			// 환종단가
						temp = temp + " + " + DD("NM_PROJECT");
						break;
                    case "NM_TP_GI":			// 출하형태
                        temp = temp + " + " + DD("TP_GI");
                        break;
                    case "NM_SO":			// 수주형태
                        temp = temp + " + " + "수주형태";
                        break;
                    case "FG_TRANSPORT":			// 운송방법
                        temp = temp + " + " + "운송방법";
                        break;
                    case "CLS_ITEM":			// 계정구분
                        temp = temp + " + " + "계정구분";
                        break;
                    case "QT_NOTPROC" :
                        temp = temp + " + " + "미처리량";
                        break;
					default :
						break;

				}
			}
			
			if(temp == "")
				return "";
			else
				return temp.Substring(3,temp.Length-3);
		}


		#endregion

		#region -> 의뢰번호별 그리드 초기화
		
		#region -> InitGridM_1

		/// <summary>
		/// Grid Initialization
		/// </summary>
		private void InitGridM_1()
		{	
			Application.DoEvents();
			
			_flexM_1.Redraw = false;

			_flexM_1.Rows.Count = 1;
			_flexM_1.Rows.Fixed = 1;
			_flexM_1.Cols.Count = 8;
			_flexM_1.Cols.Fixed = 1;
			_flexM_1.Rows.DefaultSize = 20;	

			_flexM_1.Cols[0].Width = 50;

			_flexM_1.Cols[1].Name = "S";
			_flexM_1.Cols[1].DataType = typeof(string);
			_flexM_1.Cols[1].Format = "Y;N";
			_flexM_1.Cols[1].ImageAlign = ImageAlignEnum.CenterCenter;
			_flexM_1.Cols[1].Width = 40;
			
			_flexM_1.Cols[2].Name = "NO_GIR";
			_flexM_1.Cols[2].DataType = typeof(string);
			_flexM_1.Cols[2].Width = 120;
			
			_flexM_1.Cols[3].Name = "DT_GIR";
			_flexM_1.Cols[3].DataType = typeof(string);
			_flexM_1.Cols[3].Width = 100;
			_flexM_1.Cols[3].EditMask = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT).Replace("#","9");
			_flexM_1.Cols[3].Format = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT);
			_flexM_1.Cols[3].TextAlign = TextAlignEnum.CenterCenter;
			_flexM_1.SetStringFormatCol("DT_GIR");
			
			_flexM_1.Cols[4].Name = "NM_PARTNER";
			_flexM_1.Cols[4].DataType = typeof(string);
			_flexM_1.Cols[4].Width = 140;
			
			_flexM_1.Cols[5].Name = "NM_PLANT";
			_flexM_1.Cols[5].DataType = typeof(string);
			_flexM_1.Cols[5].Width = 100;
			
			_flexM_1.Cols[6].Name = "NM_RETURN";
			_flexM_1.Cols[6].DataType = typeof(string);
			_flexM_1.Cols[6].Width = 100;

            _flexM_1.Cols[7].Name = "NM_TP_GI";
            _flexM_1.Cols[7].DataType = typeof(string);
            _flexM_1.Cols[7].Width = 100;
			
			_flexM_1.AllowSorting = AllowSortingEnum.None;
			_flexM_1.NewRowEditable = false;
			_flexM_1.EnterKeyAddRow = false;

			_flexM_1.SumPosition = SumPositionEnum.None;
			_flexM_1.GridStyle = GridStyleEnum.Green;
			_flexM_1.AllowResizing = AllowResizingEnum.Columns;

			MainFrameInterface.SetUserGrid(_flexM_1);
			
			// 그리드 헤더캡션 표시하기
			for(int i = 0; i <= _flexM_1.Cols.Count-1; i++)
				_flexM_1[0, i] = GetDDItem(_flexM_1.Cols[i].Name);

			_flexM_1.Redraw = true;		
	
			_flexM_1.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(_flexM_AfterRowColChange);	
			_flexM_1.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);	
		}

		#endregion

		#region -> InitGridD_1

		/// <summary>
		/// Grid Initialization
		/// </summary>
		private void InitGridD_1()
		{	
			Application.DoEvents();
			
			_flexD_1.Redraw = false;
	
			_flexD_1.Rows.Count = 1;
			_flexD_1.Rows.Fixed = 1;
			_flexD_1.Cols.Count = 17;
			_flexD_1.Cols.Fixed = 1;
			_flexD_1.Rows.DefaultSize = 20;	

			_flexD_1.Cols[0].Width = 50;

			_flexD_1.Cols[1].Name = "CD_ITEM";
			_flexD_1.Cols[1].DataType = typeof(string);
			_flexD_1.Cols[1].Width = 90;
			
			_flexD_1.Cols[2].Name = "NM_ITEM";
			_flexD_1.Cols[2].DataType = typeof(string);
			_flexD_1.Cols[2].Width = 120;
			
			_flexD_1.Cols[3].Name = "STND_ITEM";
			_flexD_1.Cols[3].DataType = typeof(string);
			_flexD_1.Cols[3].Width = 90;
			
			_flexD_1.Cols[4].Name = "UNIT";
			_flexD_1.Cols[4].ImageAlign = ImageAlignEnum.RightCenter;
			_flexD_1.Cols[4].DataType = typeof(string);
			_flexD_1.Cols[4].Width = 90;
			
			_flexD_1.Cols[5].Name = "NM_SL";
			_flexD_1.Cols[5].DataType = typeof(string);
			_flexD_1.Cols[5].Width = 90;

            _flexD_1.Cols[6].Name = "NM_SO";
            _flexD_1.Cols[6].DataType = typeof(string);
            _flexD_1.Cols[6].Width = 90;

            _flexD_1.Cols[7].Name = "NM_BUSI";
			_flexD_1.Cols[7].DataType = typeof(string);
			_flexD_1.Cols[7].Width = 100;		
			
			_flexD_1.Cols[8].Name = "QT_GIR";
			_flexD_1.Cols[8].DataType = typeof(decimal);
			_flexD_1.Cols[8].Width = 120;
			_flexD_1.Cols[8].TextAlign = TextAlignEnum.RightCenter;
			_flexD_1.Cols[8].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_1.SetColMaxLength("QT_GIR",17);

			_flexD_1.Cols[9].Name = "QT_GI";
			_flexD_1.Cols[9].DataType = typeof(decimal);
			_flexD_1.Cols[9].Width = 100;		
			_flexD_1.Cols[9].TextAlign = TextAlignEnum.RightCenter;
			_flexD_1.Cols[9].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.UNIT_COST,FormatFgType.SELECT);
			_flexD_1.SetColMaxLength("QT_GI",17);
			
			_flexD_1.Cols[10].Name = "NM_EXCH";
			_flexD_1.Cols[10].DataType = typeof(string);
			_flexD_1.Cols[10].Width = 100;		
			
			// 14.금액
			_flexD_1.Cols[11].Name = "UM";
			_flexD_1.Cols[11].DataType = typeof(decimal);
			_flexD_1.Cols[11].Width = 90;
			SetFormat(_flexD_1.Cols[11], DataDictionaryTypes.SA, FormatTpType.FOREIGN_UNIT_COST, FormatFgType.SELECT);

			// 14.금액
			_flexD_1.Cols[12].Name = "AM_GIR";
			_flexD_1.Cols[12].DataType = typeof(decimal);
			_flexD_1.Cols[12].Width = 90;
			SetFormat(_flexD_1.Cols[12], DataDictionaryTypes.SA, FormatTpType.FOREIGN_MONEY, FormatFgType.SELECT);

			// 14.금액
			_flexD_1.Cols[13].Name = "AM_GIRAMT";
			_flexD_1.Cols[13].DataType = typeof(decimal);
			_flexD_1.Cols[13].Width = 90;
			SetFormat(_flexD_1.Cols[13], DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.SELECT);

            _flexD_1.Cols [14].Name = "NO_SO";
            _flexD_1.Cols [14].DataType = typeof( string );
            _flexD_1.Cols [14].Width = 100;

            _flexD_1.Cols [15].Name = "FG_TRANSPORT";
            _flexD_1.Cols [15].DataType = typeof( string );
            _flexD_1.Cols [15].Width = 100;

            _flexD_1.Cols [16].Name = "CLS_ITEM";
            _flexD_1.Cols [16].DataType = typeof( string );
            _flexD_1.Cols [16].Width = 100;		
			
			_flexD_1.AllowSorting = AllowSortingEnum.None;
			_flexD_1.NewRowEditable = false;
			_flexD_1.EnterKeyAddRow = false;
		
			_flexD_1.SumPosition = SumPositionEnum.None;
			_flexD_1.GridStyle = GridStyleEnum.Blue;
			_flexD_1.AllowResizing = AllowResizingEnum.Columns;

			_flexD_1.SetDummyColumn("S");

			MainFrameInterface.SetUserGrid(_flexD_1);
			
			// 그리드 헤더캡션 표시하기
			for(int i = 0; i <= _flexD_1.Cols.Count-1; i++)
				_flexD_1[0, i] = GetDDItem(_flexD_1.Cols[i].Name);

			_flexD_1.Redraw = true;

			_flexD_1.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);

		}

		#endregion

		#endregion

		#region -> 품목코드별 그리드 초기화
		
		#region -> InitGridM_2

		/// <summary>
		/// Grid Initialization
		/// </summary>
		private void InitGridM_2()
		{	
			Application.DoEvents();
			
			_flexM_2.Redraw = false;

			_flexM_2.Rows.Count = 1;
			_flexM_2.Rows.Fixed = 1;
			_flexM_2.Cols.Count = 6;
			_flexM_2.Cols.Fixed = 1;
			_flexM_2.Rows.DefaultSize = 20;	

			_flexM_2.Cols[0].Width = 50;

			_flexM_2.Cols[1].Name = "S";
			_flexM_2.Cols[1].DataType = typeof(string);
			_flexM_2.Cols[1].Format = "Y;N";
			_flexM_2.Cols[1].ImageAlign = ImageAlignEnum.CenterCenter;
			_flexM_2.Cols[1].Width = 40;
			
			_flexM_2.Cols[2].Name = "CD_ITEM";
			_flexM_2.Cols[2].DataType = typeof(string);
			_flexM_2.Cols[2].Width = 90;
			
			_flexM_2.Cols[3].Name = "NM_ITEM";
			_flexM_2.Cols[3].DataType = typeof(string);
			_flexM_2.Cols[3].Width = 120;
			
			_flexM_2.Cols[4].Name = "STND_ITEM";
			_flexM_2.Cols[4].DataType = typeof(string);
			_flexM_2.Cols[4].Width = 100;
			
			_flexM_2.Cols[5].Name = "UNIT_IM";
			_flexM_2.Cols[5].DataType = typeof(string);
			_flexM_2.Cols[5].Width = 100;
			
			_flexM_2.AllowSorting = AllowSortingEnum.None;
			_flexM_2.NewRowEditable = false;
			_flexM_2.EnterKeyAddRow = false;

			_flexM_2.SumPosition = SumPositionEnum.None;
			_flexM_2.GridStyle = GridStyleEnum.Green;
			_flexM_2.AllowResizing = AllowResizingEnum.Columns;
			
			MainFrameInterface.SetUserGrid(_flexM_2);
			
			// 그리드 헤더캡션 표시하기
			for(int i = 0; i <= _flexM_2.Cols.Count-1; i++)
				_flexM_2[0, i] = GetDDItem(_flexM_2.Cols[i].Name);

			_flexM_2.Redraw = true;		
	
			_flexM_2.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(_flexM_AfterRowColChange);	
			_flexM_2.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);
		}

		#endregion

		#region -> InitGridD_2

		/// <summary>
		/// Grid Initialization
		/// </summary>
		private void InitGridD_2()
		{	
			Application.DoEvents();
			
			_flexD_2.Redraw = false;

			_flexD_2.Rows.Count = 1;
			_flexD_2.Rows.Fixed = 1;
			_flexD_2.Cols.Count =17;
			_flexD_2.Cols.Fixed = 1;
			_flexD_2.Rows.DefaultSize = 20;	

			_flexD_2.Cols[0].Width = 50;

			_flexD_2.Cols[1].Name = "NO_GIR";
			_flexD_2.Cols[1].DataType = typeof(string);
			_flexD_2.Cols[1].Width = 120;
						
			_flexD_2.Cols[2].Name = "DT_GIR";
			_flexD_2.Cols[2].DataType = typeof(string);
			_flexD_2.Cols[2].Width = 100;
			_flexD_2.Cols[2].EditMask = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT).Replace("#","9");
			_flexD_2.Cols[2].Format = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT);
			_flexD_2.Cols[2].TextAlign = TextAlignEnum.CenterCenter;
			_flexD_2.SetStringFormatCol("DT_GIR");
			
			_flexD_2.Cols[3].Name = "NM_QTIOTP";
			_flexD_2.Cols[3].DataType = typeof(string);
			_flexD_2.Cols[3].Width = 90;
					
			_flexD_2.Cols[4].Name = "NM_SALEGRP";
			_flexD_2.Cols[4].DataType = typeof(string);
			_flexD_2.Cols[4].Width = 90;
			
			_flexD_2.Cols[5].Name = "DT_DUEDATE";
			_flexD_2.Cols[5].DataType = typeof(string);
			_flexD_2.Cols[5].Width = 100;
			_flexD_2.Cols[5].EditMask = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT).Replace("#","9");
			_flexD_2.Cols[5].Format = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT);
			_flexD_2.Cols[5].TextAlign = TextAlignEnum.CenterCenter;
			_flexD_2.SetStringFormatCol("DT_DUEDATE");
								
			_flexD_2.Cols[6].Name = "QT_GIR";
			_flexD_2.Cols[6].DataType = typeof(decimal);
			_flexD_2.Cols[6].Width = 120;
			_flexD_2.Cols[6].TextAlign = TextAlignEnum.RightCenter;
			_flexD_2.Cols[6].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_2.SetColMaxLength("QT_GIR",17);

			_flexD_2.Cols[7].Name = "QT_GI";
			_flexD_2.Cols[7].DataType = typeof(decimal);
			_flexD_2.Cols[7].Width = 100;		
			_flexD_2.Cols[7].TextAlign = TextAlignEnum.RightCenter;
			_flexD_2.Cols[7].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.UNIT_COST,FormatFgType.SELECT);
			_flexD_2.SetColMaxLength("QT_GI",17);
			
			_flexD_2.Cols[8].Name = "NM_EXCH";
			_flexD_2.Cols[8].DataType = typeof(string);
			_flexD_2.Cols[8].Width = 90;
		
			// 14.금액
			_flexD_2.Cols[9].Name = "AM_GIR";
			_flexD_2.Cols[9].DataType = typeof(decimal);
			_flexD_2.Cols[9].Width = 90;
			SetFormat(_flexD_2.Cols[9], DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.SELECT);
						
			// 14.금액
			_flexD_2.Cols[10].Name = "AM_GIRAMT";
			_flexD_2.Cols[10].DataType = typeof(decimal);
			_flexD_2.Cols[10].Width = 90;
			SetFormat(_flexD_2.Cols[10], DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.SELECT);
		
			_flexD_2.Cols[11].Name = "NO_SO";
			_flexD_2.Cols[11].DataType = typeof(string);
			_flexD_2.Cols[11].Width = 100;		
						
			_flexD_2.Cols[12].Name = "SEQ_SO";
			_flexD_2.Cols[12].DataType = typeof(string);
			_flexD_2.Cols[12].Width = 100;		
						
			_flexD_2.Cols[13].Name = "NM_RETURN";
			_flexD_2.Cols[13].DataType = typeof(string);
			_flexD_2.Cols[13].Width = 100;

            _flexD_2.Cols[14].Name = "NM_TP_GI";
            _flexD_2.Cols[14].DataType = typeof(string);
            _flexD_2.Cols[14].Width = 100;

            _flexD_2.Cols [15].Name = "FG_TRANSPORT";
            _flexD_2.Cols [15].DataType = typeof( string );
            _flexD_2.Cols [15].Width = 100;

            _flexD_2.Cols [16].Name = "CLS_ITEM";
            _flexD_2.Cols [16].DataType = typeof( string );
            _flexD_2.Cols [16].Width = 100;		
						
			_flexD_2.AllowSorting = AllowSortingEnum.None;
			_flexD_2.NewRowEditable = false;
			_flexD_2.EnterKeyAddRow = false;
		
			_flexD_2.SumPosition = SumPositionEnum.None;
			_flexD_2.GridStyle = GridStyleEnum.Blue;
			_flexD_2.AllowResizing = AllowResizingEnum.Columns;
			_flexD_2.SetDummyColumn("S");

			MainFrameInterface.SetUserGrid(_flexD_2);
			
			// 그리드 헤더캡션 표시하기
			for(int i = 0; i <= _flexD_2.Cols.Count-1; i++)
				_flexD_2[0, i] = GetDDItem(_flexD_2.Cols[i].Name);

			_flexD_2.Redraw = true;

			_flexD_2.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);
		}

		#endregion	
		
		#endregion
	
		#region -> 거래처별 그리드 초기화
		
		#region -> InitGridM_3

		/// <summary>
		/// Grid Initialization
		/// </summary>
		private void InitGridM_3()
		{	
			Application.DoEvents();
			
			_flexM_3.Redraw = false;

			_flexM_3.Rows.Count = 1;
			_flexM_3.Rows.Fixed = 1;
			_flexM_3.Cols.Count = 4;
			_flexM_3.Cols.Fixed = 1;
			_flexM_3.Rows.DefaultSize = 20;	

			_flexM_3.Cols[0].Width = 50;

			_flexM_3.Cols[1].Name = "S";
			_flexM_3.Cols[1].DataType = typeof(string);
			_flexM_3.Cols[1].Format = "Y;N";
			_flexM_3.Cols[1].ImageAlign = ImageAlignEnum.CenterCenter;
			_flexM_3.Cols[1].Width = 40;
			
			_flexM_3.Cols[2].Name = "CD_PARTNER";
			_flexM_3.Cols[2].DataType = typeof(string);
			_flexM_3.Cols[2].Width = 90;
			
			_flexM_3.Cols[3].Name = "LN_PARTNER";
			_flexM_3.Cols[3].DataType = typeof(string);
			_flexM_3.Cols[3].Width = 100;
			
			_flexM_3.AllowSorting = AllowSortingEnum.None;
			_flexM_3.NewRowEditable = false;
			_flexM_3.EnterKeyAddRow = false;

			_flexM_3.SumPosition = SumPositionEnum.None;
			_flexM_3.GridStyle = GridStyleEnum.Green;
			_flexM_3.AllowResizing = AllowResizingEnum.Columns;
			
			MainFrameInterface.SetUserGrid(_flexM_3);
			
			// 그리드 헤더캡션 표시하기
			for(int i = 0; i <= _flexM_3.Cols.Count-1; i++)
				_flexM_3[0, i] = GetDDItem(_flexM_3.Cols[i].Name);

			_flexM_3.Redraw = true;		
	
			_flexM_3.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(_flexM_AfterRowColChange);	
			_flexM_3.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);
		}

		#endregion

		#region -> InitGridD_3

		/// <summary>
		/// Grid Initialization
		/// </summary>
		private void InitGridD_3()
		{	
			Application.DoEvents();
			
			_flexD_3.Redraw = false;

			_flexD_3.Rows.Count = 1;
			_flexD_3.Rows.Fixed = 1;
			_flexD_3.Cols.Count =19;
			_flexD_3.Cols.Fixed = 1;
			_flexD_3.Rows.DefaultSize = 20;	

			_flexD_3.Cols[0].Width = 50;

			_flexD_3.Cols[1].Name = "NO_GIR";
			_flexD_3.Cols[1].DataType = typeof(string);
			_flexD_3.Cols[1].Width = 120;
			
			_flexD_3.Cols[2].Name = "DT_GIR";
			_flexD_3.Cols[2].DataType = typeof(string);
			_flexD_3.Cols[2].Width = 100;
			_flexD_3.Cols[2].EditMask = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT).Replace("#","9");
			_flexD_3.Cols[2].Format = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT);
			_flexD_3.Cols[2].TextAlign = TextAlignEnum.CenterCenter;
			_flexD_3.SetStringFormatCol("DT_GIR");
		
			_flexD_3.Cols[3].Name = "CD_ITEM";
			_flexD_3.Cols[3].DataType = typeof(string);
			_flexD_3.Cols[3].Width = 90;
			
			_flexD_3.Cols[4].Name = "NM_ITEM";
			_flexD_3.Cols[4].DataType = typeof(string);
			_flexD_3.Cols[4].Width = 120;
			
			_flexD_3.Cols[5].Name = "STND_ITEM";
			_flexD_3.Cols[5].DataType = typeof(string);
			_flexD_3.Cols[5].Width = 90;
			
			_flexD_3.Cols[6].Name = "UNIT";
			_flexD_3.Cols[6].ImageAlign = ImageAlignEnum.RightCenter;
			_flexD_3.Cols[6].DataType = typeof(string);
			_flexD_3.Cols[6].Width = 90;
			
			_flexD_3.Cols[7].Name = "QT_GIR";
			_flexD_3.Cols[7].DataType = typeof(decimal);
			_flexD_3.Cols[7].Width = 120;
			_flexD_3.Cols[7].TextAlign = TextAlignEnum.RightCenter;
			_flexD_3.Cols[7].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_3.SetColMaxLength("QT_GIR",17);

			_flexD_3.Cols[8].Name = "QT_GI";
			_flexD_3.Cols[8].DataType = typeof(decimal);
			_flexD_3.Cols[8].Width = 100;		
			_flexD_3.Cols[8].TextAlign = TextAlignEnum.RightCenter;
			_flexD_3.Cols[8].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.UNIT_COST,FormatFgType.SELECT);
			_flexD_3.SetColMaxLength("QT_GI",17);
			
			_flexD_3.Cols[9].Name = "NM_EXCH";
			_flexD_3.Cols[9].DataType = typeof(string);
			_flexD_3.Cols[9].Width = 100;		
			
			_flexD_3.Cols[10].Name = "UM";
			_flexD_3.Cols[10].DataType = typeof(decimal);
			_flexD_3.Cols[10].Width = 90;
			SetFormat(_flexD_3.Cols[10], DataDictionaryTypes.SA, FormatTpType.UNIT_COST, FormatFgType.SELECT);

			_flexD_3.Cols[11].Name = "AM_GIR";
			_flexD_3.Cols[11].DataType = typeof(decimal);
			_flexD_3.Cols[11].Width = 90;
			SetFormat(_flexD_3.Cols[11], DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.SELECT);

			_flexD_3.Cols[12].Name = "AM_GIRAMT";
			_flexD_3.Cols[12].DataType = typeof(decimal);
			_flexD_3.Cols[12].Width = 90;
			SetFormat(_flexD_3.Cols[12], DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.SELECT);

			_flexD_3.Cols[13].Name = "NO_SO";
			_flexD_3.Cols[13].DataType = typeof(string);
			_flexD_3.Cols[13].Width = 100;		
			
			_flexD_3.Cols[14].Name = "SEQ_SO";
			_flexD_3.Cols[14].DataType = typeof(string);
			_flexD_3.Cols[14].Width = 100;		
			
			_flexD_3.Cols[15].Name = "NM_RETURN";
			_flexD_3.Cols[15].DataType = typeof(string);
			_flexD_3.Cols[15].Width = 100;

            _flexD_3.Cols[16].Name = "NM_TP_GI";
            _flexD_3.Cols[16].DataType = typeof(string);
            _flexD_3.Cols[16].Width = 100;

            _flexD_3.Cols [17].Name = "FG_TRANSPORT";
            _flexD_3.Cols [17].DataType = typeof( string );
            _flexD_3.Cols [17].Width = 100;

            _flexD_3.Cols [18].Name = "CLS_ITEM";
            _flexD_3.Cols [18].DataType = typeof( string );
            _flexD_3.Cols [18].Width = 100;		
			
			_flexD_3.AllowSorting = AllowSortingEnum.None;
			_flexD_3.NewRowEditable = false;
			_flexD_3.EnterKeyAddRow = false;
		
			_flexD_3.SumPosition = SumPositionEnum.None;
			_flexD_3.GridStyle = GridStyleEnum.Blue;
			_flexD_3.AllowResizing = AllowResizingEnum.Columns;

			_flexD_3.SetDummyColumn("S");

			MainFrameInterface.SetUserGrid(_flexD_3);
			
			// 그리드 헤더캡션 표시하기
			for(int i = 0; i <= _flexD_3.Cols.Count-1; i++)
				_flexD_3[0, i] = GetDDItem(_flexD_3.Cols[i].Name);

			_flexD_3.Redraw = true;

			_flexD_3.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);
		}

		#endregion

		#endregion 
	
		#region -> 영업그룹별 그리드 초기화
		
		#region -> InitGridM_4

		/// <summary>
		/// Grid Initialization
		/// </summary>
		private void InitGridM_4()
		{	
			Application.DoEvents();
			
			_flexM_4.Redraw = false;

			_flexM_4.Rows.Count = 1;
			_flexM_4.Rows.Fixed = 1;
			_flexM_4.Cols.Count = 4;
			_flexM_4.Cols.Fixed = 1;
			_flexM_4.Rows.DefaultSize = 20;	

			_flexM_4.Cols[0].Width = 50;

			_flexM_4.Cols[1].Name = "S";
			_flexM_4.Cols[1].DataType = typeof(string);
			_flexM_4.Cols[1].Format = "Y;N";
			_flexM_4.Cols[1].ImageAlign = ImageAlignEnum.CenterCenter;
			_flexM_4.Cols[1].Width =40;;
			
			_flexM_4.Cols[2].Name = "CD_SALEGRP";
			_flexM_4.Cols[2].DataType = typeof(string);
			_flexM_4.Cols[2].Width = 90;
			
			_flexM_4.Cols[3].Name = "NM_SALEGRP";
			_flexM_4.Cols[3].DataType = typeof(string);
			_flexM_4.Cols[3].Width = 100;
			
			_flexM_4.AllowSorting = AllowSortingEnum.None;
			_flexM_4.NewRowEditable = false;
			_flexM_4.EnterKeyAddRow = false;

			_flexM_4.SumPosition = SumPositionEnum.None;
			_flexM_4.GridStyle = GridStyleEnum.Green;
			_flexM_4.AllowResizing = AllowResizingEnum.Columns;
			
			MainFrameInterface.SetUserGrid(_flexM_4);
			
			// 그리드 헤더캡션 표시하기
			for(int i = 0; i <= _flexM_4.Cols.Count-1; i++)
				_flexM_4[0, i] = GetDDItem(_flexM_4.Cols[i].Name);

			_flexM_4.Redraw = true;		
	
			_flexM_4.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(_flexM_AfterRowColChange);	
			_flexM_4.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);
		}

		#endregion

		#region -> InitGridD_4

		/// <summary>
		/// Grid Initialization
		/// </summary>
		private void InitGridD_4()
		{	
			Application.DoEvents();
			
			_flexD_4.Redraw = false;

			_flexD_4.Rows.Count = 1;
			_flexD_4.Rows.Fixed = 1;
			_flexD_4.Cols.Count =14;
			_flexD_4.Cols.Fixed = 1;
			_flexD_4.Rows.DefaultSize = 20;	

			_flexD_4.Cols[0].Width = 50;

			_flexD_4.Cols[1].Name = "NO_GIR";
			_flexD_4.Cols[1].DataType = typeof(string);
			_flexD_4.Cols[1].Width = 120;
			
			_flexD_4.Cols[2].Name = "DT_GIR";
			_flexD_4.Cols[2].DataType = typeof(string);
			_flexD_4.Cols[2].Width = 100;
			_flexD_4.Cols[2].EditMask = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT).Replace("#","9");
			_flexD_4.Cols[2].Format = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT);
			_flexD_4.Cols[2].TextAlign = TextAlignEnum.CenterCenter;
			_flexD_4.SetStringFormatCol("DT_GIR");

			_flexD_4.Cols[3].Name = "CD_ITEM";
			_flexD_4.Cols[3].DataType = typeof(string);
			_flexD_4.Cols[3].Width = 90;
			
			_flexD_4.Cols[4].Name = "NM_ITEM";
			_flexD_4.Cols[4].DataType = typeof(string);
			_flexD_4.Cols[4].Width = 90;
			
			_flexD_4.Cols[2].Name = "DT_DUEDATE";
			_flexD_4.Cols[2].DataType = typeof(string);
			_flexD_4.Cols[2].Width = 100;
			_flexD_4.Cols[2].EditMask = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT).Replace("#","9");
			_flexD_4.Cols[2].Format = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT);
			_flexD_4.Cols[2].TextAlign = TextAlignEnum.CenterCenter;
			_flexD_4.SetStringFormatCol("DT_DUEDATE");

			_flexD_4.Cols[3].Name = "QT_GIR";
			_flexD_4.Cols[3].DataType = typeof(decimal);
			_flexD_4.Cols[3].Width = 120;
			_flexD_4.Cols[3].TextAlign = TextAlignEnum.RightCenter;
			_flexD_4.Cols[3].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_4.SetColMaxLength("QT_GIR",17);

			_flexD_4.Cols[4].Name = "QT_GI";
			_flexD_4.Cols[4].DataType = typeof(decimal);
			_flexD_4.Cols[4].Width = 100;		
			_flexD_4.Cols[4].TextAlign = TextAlignEnum.RightCenter;
			_flexD_4.Cols[4].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.UNIT_COST,FormatFgType.SELECT);
			_flexD_4.SetColMaxLength("QT_GI",17);
			
			_flexD_4.Cols[5].Name = "NM_EXCH";
			_flexD_4.Cols[5].DataType = typeof(string);
			_flexD_4.Cols[5].Width = 90;
			
			_flexD_4.Cols[6].Name = "AM_GIR";
			_flexD_4.Cols[6].DataType = typeof(decimal);
			_flexD_4.Cols[6].Width = 90;
			SetFormat(_flexD_4.Cols[6], DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.SELECT);

			_flexD_4.Cols[7].Name = "AM_GIRAMT";
			_flexD_4.Cols[7].DataType = typeof(decimal);
			_flexD_4.Cols[7].Width = 90;
			SetFormat(_flexD_4.Cols[7], DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.SELECT);

			_flexD_4.Cols[8].Name = "NO_SO";
			_flexD_4.Cols[8].DataType = typeof(string);
			_flexD_4.Cols[8].Width = 100;		
			
			_flexD_4.Cols[9].Name = "SEQ_SO";
			_flexD_4.Cols[9].DataType = typeof(string);
			_flexD_4.Cols[9].Width = 100;		
			
			_flexD_4.Cols[10].Name = "NM_RETURN";
			_flexD_4.Cols[10].DataType = typeof(string);
			_flexD_4.Cols[10].Width = 100;		
			_flexD_4.Cols[10].AllowEditing = false;

            _flexD_4.Cols[11].Name = "NM_TP_GI";
            _flexD_4.Cols[11].DataType = typeof(string);
            _flexD_4.Cols[11].Width = 100;
            _flexD_4.Cols[11].AllowEditing = false;

            _flexD_4.Cols [12].Name = "FG_TRANSPORT";
            _flexD_4.Cols [12].DataType = typeof( string );
            _flexD_4.Cols [12].Width = 100;

            _flexD_4.Cols [13].Name = "CLS_ITEM";
            _flexD_4.Cols [13].DataType = typeof( string );
            _flexD_4.Cols [13].Width = 100;		
			
			_flexD_4.AllowSorting = AllowSortingEnum.None;
			_flexD_4.NewRowEditable = false;
			_flexD_4.EnterKeyAddRow = false;
			_flexD_4.AllowEditing = false;

			_flexD_4.SumPosition = SumPositionEnum.None;
			_flexD_4.GridStyle = GridStyleEnum.Blue;
			_flexD_4.AllowResizing = AllowResizingEnum.Columns;

			_flexD_4.SetDummyColumn("S");

			MainFrameInterface.SetUserGrid(_flexD_4);
			
			// 그리드 헤더캡션 표시하기
			for(int i = 0; i <= _flexD_4.Cols.Count-1; i++)
				_flexD_4[0, i] = GetDDItem(_flexD_4.Cols[i].Name);

			_flexD_4.Redraw = true;

			_flexD_4.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);
		}

		#endregion
		
		#endregion 
   
		#region -> 의뢰일자별 그리드 초기화
		
		#region -> InitGridM_5

		/// <summary>
		/// Grid Initialization
		/// </summary>
		private void InitGridM_5()
		{	
			Application.DoEvents();
			
			_flexM_5.Redraw = false;

			_flexM_5.Rows.Count = 1;
			_flexM_5.Rows.Fixed = 1;
			_flexM_5.Cols.Count = 3;
			_flexM_5.Cols.Fixed = 1;
			_flexM_5.Rows.DefaultSize = 20;	

			_flexM_5.Cols[0].Width = 50;

			_flexM_5.Cols[1].Name = "S";
			_flexM_5.Cols[1].DataType = typeof(string);
			_flexM_5.Cols[1].Format = "Y;N";
			_flexM_5.Cols[1].ImageAlign = ImageAlignEnum.CenterCenter;
			_flexM_5.Cols[1].Width = 40;
			
			_flexM_5.Cols[2].Name = "DT_GIR";
			_flexM_5.Cols[2].DataType = typeof(string);
			_flexM_5.Cols[2].Width = 85;
			_flexM_5.Cols[2].EditMask = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT).Replace("#","9");
			_flexM_5.Cols[2].Format = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT);
			_flexM_5.Cols[2].TextAlign = TextAlignEnum.CenterCenter;
			_flexM_5.SetStringFormatCol("DT_GIR");	
			
			_flexM_5.AllowSorting = AllowSortingEnum.None;
			_flexM_5.NewRowEditable = false;
			_flexM_5.EnterKeyAddRow = false;

			_flexM_5.SumPosition = SumPositionEnum.None;
			_flexM_5.GridStyle = GridStyleEnum.Green;
			_flexM_5.AllowResizing = AllowResizingEnum.Columns;
			
			MainFrameInterface.SetUserGrid(_flexM_5);
			
			// 그리드 헤더캡션 표시하기
			for(int i = 0; i <= _flexM_5.Cols.Count-1; i++)
				_flexM_5[0, i] = GetDDItem(_flexM_5.Cols[i].Name);

			_flexM_5.Redraw = true;		
	
			_flexM_5.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(_flexM_AfterRowColChange);	
			_flexM_5.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);
		}

		#endregion

		#region -> InitGridD_5

		/// <summary>
		/// Grid Initialization
		/// </summary>
		private void InitGridD_5()
		{	
			Application.DoEvents();
			
			_flexD_5.Redraw = false;

			_flexD_5.Rows.Count = 1;
			_flexD_5.Rows.Fixed = 1;
			_flexD_5.Cols.Count =19;
			_flexD_5.Cols.Fixed = 1;
			_flexD_5.Rows.DefaultSize = 20;	

			_flexD_5.Cols[0].Width = 50;

			_flexD_5.Cols[1].Name = "LN_PARTNER";
			_flexD_5.Cols[1].DataType = typeof(string);
			_flexD_5.Cols[1].Width = 90;
		
			_flexD_5.Cols[2].Name = "NO_GIR";
			_flexD_5.Cols[2].DataType = typeof(string);
			_flexD_5.Cols[2].Width = 120;
			
			_flexD_5.Cols[3].Name = "DT_GIR";
			_flexD_5.Cols[3].DataType = typeof(string);
			_flexD_5.Cols[3].Width = 100;
			_flexD_5.Cols[3].EditMask = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT).Replace("#","9");
			_flexD_5.Cols[3].Format = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT);
			_flexD_5.Cols[3].TextAlign = TextAlignEnum.CenterCenter;
			_flexD_5.SetStringFormatCol("DT_GIR");

			_flexD_5.Cols[4].Name = "CD_ITEM";
			_flexD_5.Cols[4].DataType = typeof(string);
			_flexD_5.Cols[4].Width = 90;
			
			_flexD_5.Cols[5].Name = "NM_ITEM";
			_flexD_5.Cols[5].DataType = typeof(string);
			_flexD_5.Cols[5].Width = 120;
			
			_flexD_5.Cols[6].Name = "STND_ITEM";
			_flexD_5.Cols[6].DataType = typeof(string);
			_flexD_5.Cols[6].Width = 90;
			
			_flexD_5.Cols[7].Name = "DT_DUEDATE";
			_flexD_5.Cols[7].DataType = typeof(string);
			_flexD_5.Cols[7].Width = 100;
			_flexD_5.Cols[7].EditMask = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT).Replace("#","9");
			_flexD_5.Cols[7].Format = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT);
			_flexD_5.Cols[7].TextAlign = TextAlignEnum.CenterCenter;
			_flexD_5.SetStringFormatCol("DT_DUEDATE");

			_flexD_5.Cols[8].Name = "QT_GIR";
			_flexD_5.Cols[8].DataType = typeof(decimal);
			_flexD_5.Cols[8].Width = 120;
			_flexD_5.Cols[8].TextAlign = TextAlignEnum.RightCenter;
			_flexD_5.Cols[8].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_5.SetColMaxLength("QT_GIR",17);

			_flexD_5.Cols[9].Name = "QT_GI";
			_flexD_5.Cols[9].DataType = typeof(decimal);
			_flexD_5.Cols[9].Width = 100;		
			_flexD_5.Cols[9].TextAlign = TextAlignEnum.RightCenter;
			_flexD_5.Cols[9].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.UNIT_COST,FormatFgType.SELECT);
			_flexD_5.SetColMaxLength("QT_GI",17);
			
			_flexD_5.Cols[10].Name = "NM_EXCH";
			_flexD_5.Cols[10].DataType = typeof(string);
			_flexD_5.Cols[10].Width = 90;
			
			_flexD_5.Cols[11].Name = "AM_GIR";
			_flexD_5.Cols[11].DataType = typeof(decimal);
			_flexD_5.Cols[11].Width = 90;
			SetFormat(_flexD_5.Cols[11], DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.SELECT);

			_flexD_5.Cols[12].Name = "AM_GIRAMT";
			_flexD_5.Cols[12].DataType = typeof(decimal);
			_flexD_5.Cols[12].Width = 90;
			SetFormat(_flexD_5.Cols[12], DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.SELECT);

			_flexD_5.Cols[13].Name = "NO_SO";
			_flexD_5.Cols[13].DataType = typeof(string);
			_flexD_5.Cols[13].Width = 100;		
			
			_flexD_5.Cols[14].Name = "SEQ_SO";
			_flexD_5.Cols[14].DataType = typeof(string);
			_flexD_5.Cols[14].Width = 100;		
			
			_flexD_5.Cols[15].Name = "NM_RETURN";
			_flexD_5.Cols[15].DataType = typeof(string);
			_flexD_5.Cols[15].Width = 100;

            _flexD_5.Cols[16].Name = "NM_TP_GI";
            _flexD_5.Cols[16].DataType = typeof(string);
            _flexD_5.Cols[16].Width = 100;

            _flexD_5.Cols [17].Name = "FG_TRANSPORT";
            _flexD_5.Cols [17].DataType = typeof( string );
            _flexD_5.Cols [17].Width = 100;

            _flexD_5.Cols [18].Name = "CLS_ITEM";
            _flexD_5.Cols [18].DataType = typeof( string );
            _flexD_5.Cols [18].Width = 100;		
			
			_flexD_5.AllowSorting = AllowSortingEnum.None;
			_flexD_5.NewRowEditable = false;
			_flexD_5.EnterKeyAddRow = false;
		
			_flexD_5.SumPosition = SumPositionEnum.None;
			_flexD_5.GridStyle = GridStyleEnum.Blue;
			_flexD_5.AllowResizing = AllowResizingEnum.Columns;

			_flexD_5.SetDummyColumn("S");

			MainFrameInterface.SetUserGrid(_flexD_5);
			
			// 그리드 헤더캡션 표시하기
			for(int i = 0; i <= _flexD_5.Cols.Count-1; i++)
				_flexD_5[0, i] = GetDDItem(_flexD_5.Cols[i].Name);

			_flexD_5.Redraw = true;

			_flexD_5.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);
		}

		#endregion
	
		#endregion

		#region -> 출하형태별 그리드 초기화
		
		#region -> InitGridM_6

		/// <summary>
		/// Grid Initialization
		/// </summary>
		private void InitGridM_6()
		{	
			Application.DoEvents();
			
			_flexM_6.Redraw = false;

			_flexM_6.Rows.Count = 1;
			_flexM_6.Rows.Fixed = 1;
			_flexM_6.Cols.Count = 4;
			_flexM_6.Cols.Fixed = 1;
			_flexM_6.Rows.DefaultSize = 20;	

			_flexM_6.Cols[0].Width = 50;

			_flexM_6.Cols[1].Name = "S";
			_flexM_6.Cols[1].DataType = typeof(string);
			_flexM_6.Cols[1].Format = "Y;N";
			_flexM_6.Cols[1].ImageAlign = ImageAlignEnum.CenterCenter;
			_flexM_6.Cols[1].Width = 40;
			
			_flexM_6.Cols[2].Name = "TP_GI";
			_flexM_6.Cols[2].DataType = typeof(string);
			_flexM_6.Cols[2].Width = 90;
			
			_flexM_6.Cols[3].Name = "NM_QTIOTP";
			_flexM_6.Cols[3].DataType = typeof(string);
			_flexM_6.Cols[3].Width = 100;
		
			_flexM_6.AllowSorting = AllowSortingEnum.None;
			_flexM_6.NewRowEditable = false;
			_flexM_6.EnterKeyAddRow = false;

			_flexM_6.SumPosition = SumPositionEnum.None;
			_flexM_6.GridStyle = GridStyleEnum.Green;
			_flexM_6.AllowResizing = AllowResizingEnum.Columns;

			MainFrameInterface.SetUserGrid(_flexM_6);
			
			// 그리드 헤더캡션 표시하기
			for(int i = 0; i <= _flexM_6.Cols.Count-1; i++)
				_flexM_6[0, i] = GetDDItem(_flexM_6.Cols[i].Name);

			_flexM_6.Redraw = true;		
	
			_flexM_6.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(_flexM_AfterRowColChange);	
			_flexM_6.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);
		}

		#endregion

		#region -> InitGridD_6

		/// <summary>
		/// Grid Initialization
		/// </summary>
		private void InitGridD_6()
		{	
			Application.DoEvents();
			
			_flexD_6.Redraw = false;

            _flexD_6.Rows.Count = 1;
            _flexD_6.Rows.Fixed = 1;
            _flexD_6.Cols.Count = 18;
            _flexD_6.Cols.Fixed = 1;
            _flexD_6.Rows.DefaultSize = 20;

            _flexD_6.Cols [0].Width = 50;

            _flexD_6.Cols [1].Name = "NO_GIR";
            _flexD_6.Cols [1].DataType = typeof( string );
            _flexD_6.Cols [1].Width = 120;

            _flexD_6.Cols [2].Name = "DT_GIR";
            _flexD_6.Cols [2].DataType = typeof( string );
            _flexD_6.Cols [2].Width = 100;
            _flexD_6.Cols [2].EditMask = this.MainFrameInterface.GetFormatDescription( DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT ).Replace( "#", "9" );
            _flexD_6.Cols [2].Format = this.MainFrameInterface.GetFormatDescription( DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT );
            _flexD_6.Cols [2].TextAlign = TextAlignEnum.CenterCenter;
            _flexD_6.SetStringFormatCol( "DT_GIR" );

            _flexD_6.Cols [3].Name = "CD_ITEM";
            _flexD_6.Cols [3].DataType = typeof( string );
            _flexD_6.Cols [3].Width = 90;

            _flexD_6.Cols [4].Name = "NM_ITEM";
            _flexD_6.Cols [4].DataType = typeof( string );
            _flexD_6.Cols [4].Width = 90;

            _flexD_6.Cols [5].Name = "STND_ITEM";
            _flexD_6.Cols [5].ImageAlign = ImageAlignEnum.RightCenter;
            _flexD_6.Cols [5].DataType = typeof( string );
            _flexD_6.Cols [5].Width = 90;

            _flexD_6.Cols [6].Name = "UNIT";
            _flexD_6.Cols [6].DataType = typeof( string );
            _flexD_6.Cols [6].Width = 90;

            _flexD_6.Cols [7].Name = "QT_GIR";
            _flexD_6.Cols [7].DataType = typeof( decimal );
            _flexD_6.Cols [7].Width = 120;
            _flexD_6.Cols [7].TextAlign = TextAlignEnum.RightCenter;
            _flexD_6.Cols [7].Format = this.MainFrameInterface.GetFormatDescription( DataDictionaryTypes.PU, FormatTpType.QUANTITY, FormatFgType.SELECT );
            _flexD_6.SetColMaxLength( "QT_GIR", 17 );

            _flexD_6.Cols [8].Name = "QT_GI";
            _flexD_6.Cols [8].DataType = typeof( decimal );
            _flexD_6.Cols [8].Width = 100;
            _flexD_6.Cols [8].TextAlign = TextAlignEnum.RightCenter;
            _flexD_6.Cols [8].Format = this.MainFrameInterface.GetFormatDescription( DataDictionaryTypes.PU, FormatTpType.UNIT_COST, FormatFgType.SELECT );
            _flexD_6.SetColMaxLength( "QT_GI", 17 );

            _flexD_6.Cols [9].Name = "NM_EXCH";
            _flexD_6.Cols [9].DataType = typeof( string );
            _flexD_6.Cols [9].Width = 90;
			
			// 14.금액
            _flexD_6.Cols [10].Name = "AM_GIR";
            _flexD_6.Cols [10].DataType = typeof( decimal );
            _flexD_6.Cols [10].Width = 90;
            SetFormat( _flexD_6.Cols [10], DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.SELECT );

			// 14.금액
            _flexD_6.Cols [11].Name = "AM_GIRAMT";
            _flexD_6.Cols [11].DataType = typeof( decimal );
            _flexD_6.Cols [11].Width = 90;
            SetFormat( _flexD_6.Cols [11], DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.SELECT );

            _flexD_6.Cols [12].Name = "NO_SO";
            _flexD_6.Cols [12].DataType = typeof( string );
            _flexD_6.Cols [12].Width = 100;

            _flexD_6.Cols [13].Name = "SEQ_SO";
            _flexD_6.Cols [13].DataType = typeof( string );
            _flexD_6.Cols [13].Width = 100;

            _flexD_6.Cols [14].Name = "NM_RETURN";
            _flexD_6.Cols [14].DataType = typeof( string );
            _flexD_6.Cols [14].Width = 100;

            _flexD_6.Cols [15].Name = "NM_TP_GI";
            _flexD_6.Cols [15].DataType = typeof( string );
            _flexD_6.Cols [15].Width = 100;

            _flexD_6.Cols [16].Name = "FG_TRANSPORT";
            _flexD_6.Cols [16].DataType = typeof( string );
            _flexD_6.Cols [16].Width = 100;

            _flexD_6.Cols [17].Name = "CLS_ITEM";
            _flexD_6.Cols [17].DataType = typeof( string );
            _flexD_6.Cols [17].Width = 100;

            _flexD_6.AllowSorting = AllowSortingEnum.None;
            _flexD_6.NewRowEditable = false;
            _flexD_6.EnterKeyAddRow = false;

            _flexD_6.SumPosition = SumPositionEnum.None;
            _flexD_6.GridStyle = GridStyleEnum.Blue;
            _flexD_6.AllowResizing = AllowResizingEnum.Columns;

            _flexD_6.SetDummyColumn( "S" );

            MainFrameInterface.SetUserGrid( _flexD_6 );
			
			// 그리드 헤더캡션 표시하기
            for ( int i = 0 ; i <= _flexD_6.Cols.Count - 1 ; i++ )
                _flexD_6 [0, i] = GetDDItem( _flexD_6.Cols [i].Name );

            _flexD_6.Redraw = true;

            _flexD_6.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler( _flex_StartEdit );
		}

		#endregion

		#endregion

		#region -> 프로젝트별 그리드 초기화

		#region -> InitGridM_7

		/// <summary>
		/// Grid Initialization
		/// </summary>
		private void InitGridM_7()
		{	
			Application.DoEvents();
			
			_flexM_7.Redraw = false;

			_flexM_7.Rows.Count = 1;
			_flexM_7.Rows.Fixed = 1;
			_flexM_7.Cols.Count = 4;
			_flexM_7.Cols.Fixed = 1;
			_flexM_7.Rows.DefaultSize = 20;	

			_flexM_7.Cols[0].Width = 50;

			_flexM_7.Cols[1].Name = "S";
			_flexM_7.Cols[1].DataType = typeof(string);
			_flexM_7.Cols[1].Format = "Y;N";
			_flexM_7.Cols[1].ImageAlign = ImageAlignEnum.CenterCenter;
			_flexM_7.Cols[1].Width = 40;
			
			_flexM_7.Cols[2].Name = "NO_PROJECT";
			_flexM_7.Cols[2].DataType = typeof(string);
			_flexM_7.Cols[2].Width = 90;
			
			_flexM_7.Cols[3].Name = "NM_PROJECT";
			_flexM_7.Cols[3].DataType = typeof(string);
			_flexM_7.Cols[3].Width = 100;
		
			_flexM_7.AllowSorting = AllowSortingEnum.None;
			_flexM_7.NewRowEditable = false;
			_flexM_7.EnterKeyAddRow = false;

			_flexM_7.SumPosition = SumPositionEnum.None;
			_flexM_7.GridStyle = GridStyleEnum.Green;
			_flexM_7.AllowResizing = AllowResizingEnum.Columns;

			MainFrameInterface.SetUserGrid(_flexM_7);
			
			// 그리드 헤더캡션 표시하기
			for(int i = 0; i <= _flexM_7.Cols.Count-1; i++)
				_flexM_7[0, i] = GetDDItem(_flexM_7.Cols[i].Name);

			_flexM_7.Redraw = true;		
	
			_flexM_7.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(_flexM_AfterRowColChange);	
			_flexM_7.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);
		}

		#endregion

		#region -> InitGridD_7

		/// <summary>
		/// Grid Initialization
		/// </summary>
		private void InitGridD_7()
		{	
			Application.DoEvents();
			
			_flexD_7.Redraw = false;

			_flexD_7.Rows.Count = 1;
			_flexD_7.Rows.Fixed = 1;
			_flexD_7.Cols.Count = 19;
			_flexD_7.Cols.Fixed = 1;
			_flexD_7.Rows.DefaultSize = 20;	

			_flexD_7.Cols[0].Width = 50;

			_flexD_7.Cols[1].Name = "NO_GIR";
			_flexD_7.Cols[1].DataType = typeof(string);
			_flexD_7.Cols[1].Width = 120;
			
			_flexD_7.Cols[2].Name = "DT_GIR";
			_flexD_7.Cols[2].DataType = typeof(string);
			_flexD_7.Cols[2].Width = 100;
			_flexD_7.Cols[2].EditMask = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT).Replace("#","9");
			_flexD_7.Cols[2].Format = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT);
			_flexD_7.Cols[2].TextAlign = TextAlignEnum.CenterCenter;
			_flexD_7.SetStringFormatCol("DT_GIR");

			_flexD_7.Cols[3].Name = "CD_ITEM";
			_flexD_7.Cols[3].DataType = typeof(string);
			_flexD_7.Cols[3].Width = 90;
			
			_flexD_7.Cols[4].Name = "NM_ITEM";
			_flexD_7.Cols[4].DataType = typeof(string);
			_flexD_7.Cols[4].Width = 120;
		
			_flexD_7.Cols[5].Name = "STND_ITEM";
			_flexD_7.Cols[5].DataType = typeof(string);
			_flexD_7.Cols[5].Width = 90;
		
			_flexD_7.Cols[6].Name = "DT_DUEDATE";
			_flexD_7.Cols[6].DataType = typeof(string);
			_flexD_7.Cols[6].Width = 100;
			_flexD_7.Cols[6].EditMask = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT).Replace("#","9");
			_flexD_7.Cols[6].Format = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT);
			_flexD_7.Cols[6].TextAlign = TextAlignEnum.CenterCenter;
			_flexD_7.SetStringFormatCol("DT_DUEDATE");
						
			_flexD_7.Cols[7].Name = "QT_GIR";
			_flexD_7.Cols[7].DataType = typeof(decimal);
			_flexD_7.Cols[7].Width = 120;
			_flexD_7.Cols[7].TextAlign = TextAlignEnum.RightCenter;
			_flexD_7.Cols[7].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_7.SetColMaxLength("QT_GIR",17);

			_flexD_7.Cols[8].Name = "QT_GI";
			_flexD_7.Cols[8].DataType = typeof(decimal);
			_flexD_7.Cols[8].Width = 100;		
			_flexD_7.Cols[8].TextAlign = TextAlignEnum.RightCenter;
			_flexD_7.Cols[8].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.UNIT_COST,FormatFgType.SELECT);
			_flexD_7.SetColMaxLength("QT_GI",17);
						
			_flexD_7.Cols[9].Name = "NM_EXCH";
			_flexD_7.Cols[9].DataType = typeof(string);
			_flexD_7.Cols[9].Width = 100;		
			
			// 14.금액
			_flexD_7.Cols[10].Name = "AM_GIR";
			_flexD_7.Cols[10].DataType = typeof(decimal);
			_flexD_7.Cols[10].Width = 90;
			SetFormat(_flexD_7.Cols[10], DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.SELECT);

			// 14.금액
			_flexD_7.Cols[11].Name = "AM_GIRAMT";
			_flexD_7.Cols[11].DataType = typeof(decimal);
			_flexD_7.Cols[11].Width = 90;
			SetFormat(_flexD_7.Cols[11], DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.SELECT);

			_flexD_7.Cols[12].Name = "NO_SO";
			_flexD_7.Cols[12].DataType = typeof(string);
			_flexD_7.Cols[12].Width = 100;		
			
			_flexD_7.Cols[13].Name = "SEQ_SO";
			_flexD_7.Cols[13].DataType = typeof(string);
			_flexD_7.Cols[13].Width = 100;		
			
			_flexD_7.Cols[14].Name = "NM_SALEGRP";
			_flexD_7.Cols[14].DataType = typeof(string);
			_flexD_7.Cols[14].Width = 100;		
			
			_flexD_7.Cols[15].Name = "NM_RETURN";
			_flexD_7.Cols[15].DataType = typeof(string);
			_flexD_7.Cols[15].Width = 100;

            _flexD_7.Cols[16].Name = "NM_TP_GI";
            _flexD_7.Cols[16].DataType = typeof(string);
            _flexD_7.Cols[16].Width = 100;

            _flexD_7.Cols [17].Name = "FG_TRANSPORT";
            _flexD_7.Cols [17].DataType = typeof( string );
            _flexD_7.Cols [17].Width = 100;

            _flexD_7.Cols [18].Name = "CLS_ITEM";
            _flexD_7.Cols [18].DataType = typeof( string );
            _flexD_7.Cols [18].Width = 100;		
			
			_flexD_7.AllowSorting = AllowSortingEnum.None;
			_flexD_7.NewRowEditable = false;
			_flexD_7.EnterKeyAddRow = false;
			
			_flexD_7.SumPosition = SumPositionEnum.None;
			_flexD_7.GridStyle = GridStyleEnum.Blue;
			_flexD_7.AllowResizing = AllowResizingEnum.Columns;

			_flexD_7.SetDummyColumn("S");

			MainFrameInterface.SetUserGrid(_flexD_7);
			
			// 그리드 헤더캡션 표시하기
			for(int i = 0; i <= _flexD_7.Cols.Count-1; i++)
				_flexD_7[0, i] = GetDDItem(_flexD_7.Cols[i].Name);

			_flexD_7.Redraw = true;

			_flexD_7.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);
		}

		#endregion
		
		#endregion 

        #region -> 품목집계별 그리드 초기화

      
        #region -> InitGridD_8

        /// <summary>
        /// Grid Initialization
        /// </summary>
        private void InitGridD_8()
        {
            Application.DoEvents();

            _flexD_8.Redraw = false;

            _flexD_8.Rows.Count = 1;
            _flexD_8.Rows.Fixed = 1;
            _flexD_8.Cols.Count = 11;
            _flexD_8.Cols.Fixed = 1;
            _flexD_8.Rows.DefaultSize = 20;

            _flexD_8.Cols [0].Width = 50;

            /* 품목코드 */
            _flexD_8.Cols [1].Name = "CD_ITEM";
            _flexD_8.Cols [1].DataType = typeof( string );
            _flexD_8.Cols [1].Width = 90;
             
            /* 품목명 */
            _flexD_8.Cols [2].Name = "NM_ITEM";
            _flexD_8.Cols [2].DataType = typeof( string );
            _flexD_8.Cols [2].Width = 120;

            /* 규격*/
            _flexD_8.Cols [3].Name = "STND_ITEM";
            _flexD_8.Cols [3].DataType = typeof( string );
            _flexD_8.Cols [3].Width = 90;

            /* 단위*/
            _flexD_8.Cols [4].Name = "UNIT";
            _flexD_8.Cols [4].ImageAlign = ImageAlignEnum.RightCenter;
            _flexD_8.Cols [4].DataType = typeof( string );
            _flexD_8.Cols [4].Width = 90;

            /* 의뢰량*/
            _flexD_8.Cols [5].Name = "QT_GIR";
            _flexD_8.Cols [5].DataType = typeof( decimal );
            _flexD_8.Cols [5].Width = 120;
            _flexD_8.Cols [5].TextAlign = TextAlignEnum.RightCenter;
            _flexD_8.Cols [5].Format = this.MainFrameInterface.GetFormatDescription( DataDictionaryTypes.PU, FormatTpType.QUANTITY, FormatFgType.SELECT );
            _flexD_8.SetColMaxLength( "QT_GIR", 17 );

            /* 출하량*/
            _flexD_8.Cols [6].Name = "QT_GI";
            _flexD_8.Cols [6].DataType = typeof( decimal );
            _flexD_8.Cols [6].Width = 100;
            _flexD_8.Cols [6].TextAlign = TextAlignEnum.RightCenter;
            _flexD_8.Cols [6].Format = this.MainFrameInterface.GetFormatDescription( DataDictionaryTypes.PU, FormatTpType.UNIT_COST, FormatFgType.SELECT );
            _flexD_8.SetColMaxLength( "QT_GI", 17 );

            /* 미처리수량 */
            _flexD_8.Cols [7].Name = "QT_NOTPROC";
            _flexD_8.Cols [7].DataType = typeof( decimal );
            _flexD_8.Cols [7].Width = 100;
            _flexD_8.Cols [7].TextAlign = TextAlignEnum.RightCenter;
            _flexD_8.Cols [7].Format = this.MainFrameInterface.GetFormatDescription( DataDictionaryTypes.PU, FormatTpType.QUANTITY, FormatFgType.SELECT );
            _flexD_8.SetColMaxLength( "QT_NOTPROC", 17 );

            /* 금액 */
            _flexD_8.Cols [8].Name = "AM_GIR";
            _flexD_8.Cols [8].DataType = typeof( decimal );
            _flexD_8.Cols [8].Width = 90;
            SetFormat( _flexD_8.Cols [8], DataDictionaryTypes.SA, FormatTpType.FOREIGN_MONEY, FormatFgType.SELECT );
         
            _flexD_8.Cols [9].Name = "AM_GIRAMT";
            _flexD_8.Cols [9].DataType = typeof( decimal );
            _flexD_8.Cols [9].Width = 90;
            SetFormat( _flexD_8.Cols [9], DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.SELECT );

            /* 계정품목 */
            _flexD_8.Cols [10].Name = "CLS_ITEM";
            _flexD_8.Cols [10].DataType = typeof( string );
            _flexD_8.Cols [10].Width = 100;

            _flexD_8.AllowSorting = AllowSortingEnum.None;
            _flexD_8.NewRowEditable = false;
            _flexD_8.EnterKeyAddRow = false;

            _flexD_8.SumPosition = SumPositionEnum.None;
            _flexD_8.GridStyle = GridStyleEnum.Blue;
            _flexD_8.AllowResizing = AllowResizingEnum.Columns;

            _flexD_8.SetDummyColumn( "S" );

            MainFrameInterface.SetUserGrid( _flexD_8 );

            // 그리드 헤더캡션 표시하기
            for ( int i = 0 ; i <= _flexD_8.Cols.Count - 1 ; i++ )
                _flexD_8 [0, i] = GetDDItem( _flexD_8.Cols [i].Name );

            _flexD_8.Redraw = true;

            _flexD_8.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler( _flex_StartEdit );

        }

        #endregion

        #endregion

		#endregion

		#region ♣ 메인버튼 이벤트

        #region -> 조회버튼

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			//검색 조건 항목 체크
			if(FieldCheck() == false)
				return;

			switch(m_tabC.SelectedIndex.ToString())
			{
				case "0" :
					if(_flexM_1.DataTable != null)
					{
						_flexM_1.DataTable.Rows.Clear();
						_flexD_1.DataTable.Rows.Clear();
					}
					break;
				case "1" :
					if(_flexM_2.DataTable != null)
					{
						_flexM_2.DataTable.Rows.Clear();
						_flexD_2.DataTable.Rows.Clear();
					}
					break;
				case "2" :
					if(_flexM_3.DataTable != null)
					{
						_flexM_3.DataTable.Rows.Clear();
						_flexD_3.DataTable.Rows.Clear();
					}
					break;
				case "3" :
					if(_flexM_4.DataTable != null)
					{
						_flexM_4.DataTable.Rows.Clear();
						_flexD_4.DataTable.Rows.Clear();
					}
					break;
				case "4" :
					if(_flexM_5.DataTable != null)
					{
						_flexM_5.DataTable.Rows.Clear();
						_flexD_5.DataTable.Rows.Clear();
					}
					break;
				case "5" :
					if(_flexM_6.DataTable != null)
					{
						_flexM_6.DataTable.Rows.Clear();
                        _flexD_6.DataTable.Rows.Clear();
					}
					break;
				case "6" :
					if(_flexM_7.DataTable != null)
					{
						_flexM_7.DataTable.Rows.Clear();
						_flexD_7.DataTable.Rows.Clear();
					}
					break;

                case "7":
                    if ( _flexD_8.DataTable != null )
                    {
                        //_flexM_8.DataTable.Rows.Clear();
                        _flexD_8.DataTable.Rows.Clear();
                    }
                    break;
				default :
					break;
			}

			// 0:회사코드, 1:의뢰일자(FROM), 2:의뢰일자(TO), 3:사업장, 4:거래구분, 5:출하공장, 6:출하창고, 7:처리상태, 8:출하구분, 9:유무환구분, 10:단위선택, 11:운송수단
            DataSet m_dsSet = _biz.Search( LoginInfo.CompanyCode,     // 회사코드
                                                mskDtGirStart.Text, mskDtGirEnd.Text, // 의뢰일자
                                                m_cboBizArea.SelectedValue.ToString(), // 사업장
                                                cboTpBusi.SelectedValue.ToString(),     // 거래구분
                                                cboPlantGir.SelectedValue.ToString(),     // 출하공장
                                                bpNm_Sl.CodeValue,                            // 
                                                cboStaGir.SelectedValue.ToString(),      // 처리상태
                                                m_cboFgGi.SelectedValue.ToString(),   // 출하구분  
                                                m_cboSlUnit.SelectedValue.ToString(),  // 단위선택
                                                bpTpSo.CodeValue,                                // 수주형태
                                                m_tabC.SelectedIndex.ToString(),           //  상태 
                                                cboFgTransport.SelectedValue.ToString(),  // 운송수단
                                                m_cboClsItemS.SelectedValue.ToString()
                                                );

			try
			{
				//납품의뢰현황 - 선택된 Tab
				switch(m_tabC.SelectedIndex)
				{
					case 0 :	//의뢰번호별 조회
						
						// Detail 바인딩
                        _flexD_1.Binding = m_dsSet.Tables[1].DefaultView;				

						// Master 바인딩
						_flexM_1.Binding = m_dsSet.Tables[0].DefaultView;	// 요기에서 곧바로 AfterRowColChange 이벤트 발생
								
						gdv_LineNO_GIR = new DataView( m_dsSet.Tables[1]);
						if(m_dsSet.Tables[0].Rows.Count >0)
						DataBinding0(1);
						
						SetEndMsg(_flexM_1);

						break;

					case 1 :	//품목코드별 조회
						// Detail 바인딩
						_flexD_2.Binding = m_dsSet.Tables[1].DefaultView;				
                        // Master 바인딩
						_flexM_2.Binding = m_dsSet.Tables[0].DefaultView;	// 요기에서 곧바로 AfterRowColChange 이벤트 발생
					    gdv_LineCD_ITEM = new DataView( m_dsSet.Tables[1]);
						if(m_dsSet.Tables[0].Rows.Count >0)
						DataBinding1(1);

						SetEndMsg(_flexM_2);

						break;

					case 2 :	//거래처별 조회
						// Detail 바인딩
						_flexD_3.Binding = m_dsSet.Tables[1].DefaultView;				
						// Master 바인딩
						_flexM_3.Binding = m_dsSet.Tables[0].DefaultView;	// 요기에서 곧바로 AfterRowColChange 이벤트 발생
				        gdv_LineCD_PARTNER = new DataView( m_dsSet.Tables[1]);
						if(m_dsSet.Tables[0].Rows.Count >0)
						DataBinding2(1);
						
						SetEndMsg(_flexM_3);

						break;

					case 3 :	//영업그룹별 조회
						// Detail 바인딩
						_flexD_4.Binding = m_dsSet.Tables[1].DefaultView;				
						// Master 바인딩
						_flexM_4.Binding = m_dsSet.Tables[0].DefaultView;	// 요기에서 곧바로 AfterRowColChange 이벤트 발생
						gdv_LineCD_GROUP = new DataView( m_dsSet.Tables[1]);
						if(m_dsSet.Tables[0].Rows.Count >0)
						DataBinding3(1);
						
						SetEndMsg(_flexM_4);

						break;

					case 4 :	//의뢰일자별 조회
						// Detail 바인딩
						_flexD_5.Binding = m_dsSet.Tables[1].DefaultView;				
						// Master 바인딩
						_flexM_5.Binding = m_dsSet.Tables[0].DefaultView;	// 요기에서 곧바로 AfterRowColChange 이벤트 발생
                        gdv_LineDT_GIR = new DataView( m_dsSet.Tables[1]);
						if(m_dsSet.Tables[0].Rows.Count >0)
						DataBinding4(1);

						SetEndMsg(_flexM_5);

						break;

					case 5 :	//출하형태별 조회
						if(m_dsSet == null)
							return;
						// Detail 바인딩
                        _flexD_6.Binding = m_dsSet.Tables [1].DefaultView;				
                        // Master 바인딩
						_flexM_6.Binding = m_dsSet.Tables[0].DefaultView;	// 요기에서 곧바로 AfterRowColChange 이벤트 발생
						gdv_LineCD_QTIOTP = new DataView( m_dsSet.Tables[1]);
						if(m_dsSet.Tables[0].Rows.Count >0)
						DataBinding5(1);
                         SetEndMsg(_flexM_6);

						break;

					case 6 :	//프로젝트별 조회
						if(m_dsSet == null)
							return;
						// Detail 바인딩
						_flexD_7.Binding = m_dsSet.Tables[1].DefaultView;				
						// Master 바인딩
						_flexM_7.Binding = m_dsSet.Tables[0].DefaultView;	// 요기에서 곧바로 AfterRowColChange 이벤트 발생
						gdv_LineCD_PJT = new DataView( m_dsSet.Tables[1]);
						if(m_dsSet.Tables[0].Rows.Count >0)
						DataBinding6(1);
                        SetEndMsg(_flexM_7);

						break;

                    case 7:	//품목집계별 조회
                        if ( m_dsSet == null )
                            return;
                   
                        _flexD_8.Binding = m_dsSet.Tables [0].DefaultView;	
                        gdv_LineCD_SUMITEM = new DataView( m_dsSet.Tables [0] );
                   
                        SetEndMsg( _flexD_8 );

                        break;
				}
			}
			catch(Exception ex)
			{
				MsgEnd(ex);
			}
        }

        #endregion

        #region -> SetEndMsg

        public void SetEndMsg(Dass.FlexGrid.FlexGrid _flex)
		{
			if(!_flex.HasNormalRow)
				this.ShowMessage("IK1_003");
			else 
				this.ToolBarPrintButtonEnabled = true;
        }

        #endregion

        #region -> 인쇄버튼

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
		{
			try
			{
				m_lblBizarea.Focus();

//				DataTable ldt_Report = new DataTable();
//
//				if( m_tabC.SelectedIndex ==0 )
//				{
//					ldt_Report = SelectedTabNO_GIR();
//				}
//				else if( m_tabC.SelectedIndex ==1 )
//				{
//					ldt_Report = SelectedTabCD_ITEM();
//				}
//				else if( m_tabC.SelectedIndex ==2 )
//				{
//					ldt_Report = SelectedTabCD_PARTNER();
//				}
//				else if( m_tabC.SelectedIndex ==3 )
//				{
//					ldt_Report = SelectedTabCD_GROUP();
//				}
//				else if( m_tabC.SelectedIndex ==4 )
//				{
//					ldt_Report = SelectedTabDT();
//				}
//				else if( m_tabC.SelectedIndex ==5 )
//				{
//					ldt_Report = SelectedTabCD_QTIOTP();
//				}
//				else if( m_tabC.SelectedIndex ==6 )
//				{
//					ldt_Report = SelectedTabCD_PJT();
//				}
//
//				if( ldt_Report == null || ldt_Report.Rows.Count <=0)
//				{
//					ShowMessage("CM_M100007");	
//					return ;
//				}
//							
//				PrintSetting(ldt_Report);

				PrintSetting();
			}
			catch(coDbException ex)
			{
				this.ShowErrorMessage(ex, this.PageName);
			}
			catch(Exception ex)
			{
				this.ShowErrorMessage(ex, this.PageName);
			}
			finally
			{

				Cursor.Current = Cursors.Default;
			}

        }

        #endregion

        #region -> 출력 보조 함수

        private DataTable SelectedTabNO_GIR()
		{
			DataTable ldt_report = gdv_LineNO_GIR.Table.Clone();

			try
			{			
				DataTable ldt_Selected = _flexM_1.DataTable;

				DataRow[] ldr_Args = ldt_Selected.Select("S ='Y'");
				if( ldr_Args != null && ldr_Args.Length >0)
				{
					for( int i= 0 ; i < ldr_Args.Length ; i++)
					{
						string ls_filter = "NO_GIR = '" +ldr_Args[i]["NO_GIR"].ToString().Trim()+ "'";	

						DataRow[] ldr_Selectd = gdv_LineNO_GIR.Table.Select(ls_filter,"",DataViewRowState.Unchanged);
						if( ldr_Selectd != null && ldr_Selectd.Length >0)
						{
							for(int j= 0 ; j < ldr_Selectd.Length ; j++)
							{
								ldt_report.ImportRow(ldr_Selectd[j]);
							}
						}						
					}
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return ldt_report;
		}

				
		private DataTable SelectedTabCD_ITEM()
		{
			DataTable ldt_report = gdv_LineCD_ITEM.Table.Clone();

			try
			{				
				DataTable ldt_Selected = _flexM_2.DataTable;

				DataRow[] ldr_Args = ldt_Selected.Select("S ='Y'");
				if( ldr_Args != null && ldr_Args.Length >0)
				{
					for( int i= 0 ; i < ldr_Args.Length ; i++)
					{
						string ls_CD_ITEM = ldr_Args[i]["CD_ITEM"].ToString();
						
						string ls_filter="";						
						ls_filter = "CD_ITEM = '" +ls_CD_ITEM + "'";
						

						DataRow[] ldr_Selectd = gdv_LineCD_ITEM.Table.Select(ls_filter,"",DataViewRowState.Unchanged);
						if( ldr_Selectd != null && ldr_Selectd.Length >0)
						{
							for(int j= 0 ; j < ldr_Selectd.Length ; j++)
							{
								ldt_report.ImportRow(ldr_Selectd[j]);
							}
						}						
					}
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return ldt_report;
		}
		

		private DataTable SelectedTabCD_PARTNER()
		{
			DataTable ldt_report = gdv_LineCD_PARTNER.Table.Clone();

			try
			{
				DataTable ldt_Selected = _flexM_3.DataTable;

				DataRow[] ldr_Args = ldt_Selected.Select("S ='Y'");
				if( ldr_Args != null && ldr_Args.Length >0)
				{
					for( int i= 0 ; i < ldr_Args.Length ; i++)
					{
						string ls_CD_PARTNER = ldr_Args[i]["CD_PARTNER"].ToString();
						string ls_filter="";				
						ls_filter = "CD_PARTNER = '" +ls_CD_PARTNER + "'";		

						DataRow[] ldr_Selectd = gdv_LineCD_PARTNER.Table.Select(ls_filter,"",DataViewRowState.Unchanged);
						if( ldr_Selectd != null && ldr_Selectd.Length >0)
						{
							for(int j= 0 ; j < ldr_Selectd.Length ; j++)
							{
								ldt_report.ImportRow(ldr_Selectd[j]);
							}
						}						
					}
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return ldt_report;
		}


		private DataTable SelectedTabCD_GROUP()
		{
			DataTable ldt_report = gdv_LineCD_GROUP.Table.Clone();

			try
			{
				DataTable ldt_Selected = _flexM_4.DataTable;

				DataRow[] ldr_Args = ldt_Selected.Select("S ='Y'");
				if( ldr_Args != null && ldr_Args.Length >0)
				{
					for( int i= 0 ; i < ldr_Args.Length ; i++)
					{
						string ls_CD_PURGRP = ldr_Args[i]["CD_GROUP"].ToString();
				

						string ls_filter="";				
						ls_filter = "CD_GROUP = '" +ls_CD_PURGRP + "'";			
						DataRow[] ldr_Selectd = gdv_LineCD_GROUP.Table.Select(ls_filter,"",DataViewRowState.Unchanged);
						if( ldr_Selectd != null && ldr_Selectd.Length >0)
						{
							for(int j= 0 ; j < ldr_Selectd.Length ; j++)
							{
								ldt_report.ImportRow(ldr_Selectd[j]);
							}
						}						
					}
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return ldt_report;
		}

		
		private DataTable SelectedTabDT()
		{
			DataTable ldt_report = gdv_LineDT_GIR.Table.Clone();

			try
			{
				DataTable ldt_Selected = _flexM_5.DataTable;

				DataRow[] ldr_Args = ldt_Selected.Select("S ='Y'");
				if( ldr_Args != null && ldr_Args.Length >0)
				{
					for( int i= 0 ; i < ldr_Args.Length ; i++)
					{
						string ls_DT = ldr_Args[i]["DT_GIR"].ToString();
						string ls_filter="";				
						ls_filter = "DT_GIR = '" +ls_DT + "'";	
					
						DataRow[] ldr_Selectd = gdv_LineDT_GIR.Table.Select(ls_filter,"",DataViewRowState.Unchanged);
						if( ldr_Selectd != null && ldr_Selectd.Length >0)
						{
							for(int j= 0 ; j < ldr_Selectd.Length ; j++)
							{
								ldt_report.ImportRow(ldr_Selectd[j]);
							}
						}						
					}
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return ldt_report;
		}


		private DataTable SelectedTabCD_QTIOTP()
		{
			DataTable ldt_report = gdv_LineCD_QTIOTP.Table.Clone();

			try
			{
				DataTable ldt_Selected = _flexM_6.DataTable;

				DataRow[] ldr_Args = ldt_Selected.Select("S ='Y'");
				if( ldr_Args != null && ldr_Args.Length >0)
				{
					for( int i= 0 ; i < ldr_Args.Length ; i++)
					{
						string ls_CD_QTIOTP = ldr_Args[i]["TP_GI"].ToString();
				

						string ls_filter="";				
						ls_filter = "TP_GI = '" +ls_CD_QTIOTP + "'";					
						DataRow[] ldr_Selectd = gdv_LineCD_QTIOTP.Table.Select(ls_filter,"",DataViewRowState.Unchanged);
						if( ldr_Selectd != null && ldr_Selectd.Length >0)
						{
							for(int j= 0 ; j < ldr_Selectd.Length ; j++)
							{
								ldt_report.ImportRow(ldr_Selectd[j]);
							}
						}						
					}
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return ldt_report;
		}

		
		private DataTable SelectedTabCD_PJT()
		{
			DataTable ldt_report = gdv_LineCD_PJT.Table.Clone();

			try
			{				
				DataTable ldt_Selected = _flexM_7.DataTable;

				DataRow[] ldr_Args = ldt_Selected.Select("S ='Y'");
				if( ldr_Args != null && ldr_Args.Length >0)
				{
					for( int i= 0 ; i < ldr_Args.Length ; i++)
					{
						string ls_CD_PJT = ldr_Args[i]["CD_PJT"].ToString();
				

						string ls_filter="";				
						ls_filter = "CD_PJT = '" +ls_CD_PJT + "'";					


						DataRow[] ldr_Selectd = gdv_LineCD_PJT.Table.Select(ls_filter,"",DataViewRowState.Unchanged);
						if( ldr_Selectd != null && ldr_Selectd.Length >0)
						{
							for(int j= 0 ; j < ldr_Selectd.Length ; j++)
							{
								ldt_report.ImportRow(ldr_Selectd[j]);
							}
						}						
					}
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return ldt_report;
		}
	
 
		private void PrintSetting()//DataTable pdt_Report)
		{
            try
            {
                string 메뉴ID = string.Empty;
                string 메뉴명 = string.Empty;

                if ( m_tabC.SelectedIndex == 0 )
                {

                    /*  기존 소스
                    //name[20] = name[20] + "(" + this.tab1.Text.ToString() + ")";
                    //this.viewReport("R_SA_GIRSCH_0",code, name);
                    ////designer.Start("R_SA_GIRSCH_0", pdt_Report);
                    //designer.Start("R_SA_GIRSCH_0", this.MakeRelation(_flexM_1, _flexD_1, "NO_GIR"));
                     */
                    메뉴ID = "R_SA_GIRSCH_0";
                    메뉴명 = "납품의뢰현황-의뢰번호";
                    _flexH = _flexM_1;
                    _flexD = _flexD_1;
                }
                else if ( m_tabC.SelectedIndex == 1 )
                {
                    메뉴ID = "R_SA_GIRSCH_1";
                    메뉴명 = "납품의뢰현황-품목코드별";

                    _flexH = _flexM_2;
                    _flexD = _flexD_2;

                }
                else if ( m_tabC.SelectedIndex == 2 )
                {
                    메뉴ID = "R_SA_GIRSCH_2";
                    메뉴명 = "납품의뢰현황-거래처별";

                    _flexH = _flexM_3;
                    _flexD = _flexD_3;
                }
                else if ( m_tabC.SelectedIndex == 3 )
                {
                    메뉴ID = "R_SA_GIRSCH_3";
                    메뉴명 = "납품의뢰현황-영업그룹별";

                    _flexH = _flexM_4;
                    _flexD = _flexD_4;
                }
                else if ( m_tabC.SelectedIndex == 4 )
                {
                    메뉴ID = "R_SA_GIRSCH_4";
                    메뉴명 = "납품의뢰현황-납품일자별";

                    _flexH = _flexM_5;
                    _flexD = _flexD_5;
                }
                else if ( m_tabC.SelectedIndex == 5 )
                {
                    메뉴ID = "R_SA_GIRSCH_5";
                    메뉴명 = "납품의뢰현황-출하형태별";

                    _flexH = _flexM_6;
                    _flexD = _flexD_8;
                }
                else if ( m_tabC.SelectedIndex == 6 )
                {
                    메뉴ID = "R_SA_GIRSCH_6";
                    메뉴명 = "납품의뢰현황-프로젝트별";

                    _flexH = _flexM_7;
                    _flexD = _flexD_7;
                }
                string No_PK_Multi = "";

                DataRow [] ldt_Report = _flexH.DataTable.Select( "S = 'Y'" );

                if ( ldt_Report == null || ldt_Report.Length == 0 )
                {
                    ShowMessage( 공통메세지.선택된자료가없습니다 );
                    return;
                }
                else
                {

                    switch ( 메뉴명 )
                    {
                        case "납품의뢰현황-의뢰번호":
                            for ( int i = _flexH.Rows.Fixed ; i < _flexH.Rows.Count ; i++ )
                            {
                                if ( _flexH [i, "S"].ToString() == "Y" )
                                    No_PK_Multi += _flexH [i, "NO_GIR"].ToString() + "|";
                            }
                            break;

                        case "납품의뢰현황-품목코드별":
                            for ( int i = _flexH.Rows.Fixed ; i < _flexH.Rows.Count ; i++ )
                            {
                                if ( _flexH [i, "S"].ToString() == "Y" )
                                    No_PK_Multi += _flexH [i, "CD_ITEM"].ToString() + "|";
                            }
                            break;
                        case "납품의뢰현황-거래처별":
                            for ( int i = _flexH.Rows.Fixed ; i < _flexH.Rows.Count ; i++ )
                            {
                                if ( _flexH [i, "S"].ToString() == "Y" )
                                    No_PK_Multi += _flexH [i, "CD_PARTNER"].ToString() + "|";
                            }
                            break;
                        case "납품의뢰현황-영업그룹별":
                            for ( int i = _flexH.Rows.Fixed ; i < _flexH.Rows.Count ; i++ )
                            {
                                if ( _flexH [i, "S"].ToString() == "Y" )
                                    No_PK_Multi += _flexH [i, "CD_SALEGRP"].ToString() + "|";
                            }
                            break;
                        case "납품의뢰현황-납품일자별":
                            for ( int i = _flexH.Rows.Fixed ; i < _flexH.Rows.Count ; i++ )
                            {
                                if ( _flexH [i, "S"].ToString() == "Y" )
                                    No_PK_Multi += _flexH [i, "DT_GIR"].ToString() + "|";
                            }
                            break;
                        case "납품의뢰현황-출하형태별":
                            for ( int i = _flexH.Rows.Fixed ; i < _flexH.Rows.Count ; i++ )
                            {
                                if ( _flexH [i, "S"].ToString() == "Y" )
                                    No_PK_Multi += _flexH [i, "TP_GI"].ToString() + "|";
                            }
                            break;
                        case "납품의뢰현황-프로젝트별":
                            for ( int i = _flexH.Rows.Fixed ; i < _flexH.Rows.Count ; i++ )
                            {
                                if ( _flexH [i, "S"].ToString() == "Y" )
                                    No_PK_Multi += _flexH [i, "NO_PROJECT"].ToString() + "|";
                            }
                            break;
                        default :
                            break;
                    }
                }

                if ( !_flexH.HasNormalRow && _flexD.HasNormalRow )
                    return;

                ReportHelper rptHelper = new ReportHelper( 메뉴ID, 메뉴명 );

                try
                {
                    DataTable dt = _biz.Search_req( LoginInfo.CompanyCode,     // 회사코드
                                   mskDtGirStart.Text, mskDtGirEnd.Text, // 의뢰일자
                                   m_cboBizArea.SelectedValue.ToString(), // 사업장
                                   cboTpBusi.SelectedValue.ToString(),     // 거래구분
                                   cboPlantGir.SelectedValue.ToString(),     // 출하공장
                                   bpNm_Sl.CodeValue,                            // 
                                   cboStaGir.SelectedValue.ToString(),      // 처리상태
                                   m_cboFgGi.SelectedValue.ToString(),   // 출하구분  
                                   m_cboSlUnit.SelectedValue.ToString(),  // 단위선택
                                   bpTpSo.CodeValue,                                // 수주형태
                                   m_tabC.SelectedIndex.ToString(),           //  상태 
                                   cboFgTransport.SelectedValue.ToString(), No_PK_Multi );                                                              

                    rptHelper.SetDataTable( dt );
                    rptHelper.SetData( "의뢰일자Fr", mskDtGirStart.Text.Substring( 0, 4 ) + "년" + mskDtGirStart.Text.Substring( 4, 2 ) + "월" + mskDtGirStart.Text.Substring( 6, 2 ) + "일");
                    rptHelper.SetData( "의뢰일자To", mskDtGirEnd.Text.Substring( 0, 4 ) + "년" + mskDtGirEnd.Text.Substring( 4, 2 ) + "월" + mskDtGirEnd.Text.Substring( 6, 2 ) + "일" );
                    rptHelper.SetData( "처리상태", cboStaGir.Text );
                    rptHelper.SetData( "출고창고", bpNm_Sl.CodeName );
                    rptHelper.SetData( "거래구분", cboTpBusi.Text );
                    rptHelper.SetData( "출고구분", m_cboFgGi.Text );
                    rptHelper.Print();
                }
                catch ( Exception Ex )
                {
                    this.MsgEnd( Ex );
                }


            }
            catch ( Exception ex )
            {
                this.MsgEnd( ex );
            }
		}

		private void viewReport(string p, string[] code, string[] name)
		{
			FileInfo  fileInfo = new FileInfo("./DownLoad/print/sale/영업인쇄헤더.html");
			s = fileInfo.CreateText();
			s.NewLine = "\n";
				
			string a= "<html><head><meta http-equiv='Content-Type' content= 'text/html; charset=euc-kr'></head><body style='font-family:돋움체;'>";
			a += "<< <b> 레포트 관리등록의 시스템코드는 "+p+" 입니다.<br> 그리드 테이타 값은 ctrl + alt+ shift 값으로 확인하세요..>><br></b>";
			for(int i = 0; i < code.Length; i++)
			{
				a += "<font color = 'blue'>" + code[i] + " </font> : <font color = 'red'>" + name[i] + "</font>";
				a += "<br></body></html>";
			}
			s.WriteLine(a);
			s.Close();
		}
		
		/// <summary>
		/// 릴레이션
		/// </summary>
		/// <param name="_dtH"></param>
		/// <param name="_dtD"></param>
		/// <param name="r_name"></param>
		/// <returns></returns>
		private DataTable MakeRelation(FlexGrid _gM, FlexGrid _gD, string r_name)
		{
		
			DataTable _dtM = _gM.DataTable.Copy();
			DataTable _dtD = _gD.DataTable.Copy();

			DataTable _dtCp = new DataTable();
			_dtCp = _dtD.Clone();

			for(int i = 0; i < _dtM.Columns.Count; i++)
			{
				if(_dtCp.Columns.IndexOf(_dtM.Columns[i].ColumnName) < 0)
				{
					_dtCp.Columns.Add(_dtM.Columns[i].ColumnName);
				}
			}
			_dtCp.Clear();

            DataSet ds = new DataSet();
            ds.Tables.Add(_dtM);
            ds.Tables.Add(_dtD);

			DataRelation myDataRelation = new DataRelation("parent2Child", _dtM.Columns[r_name], _dtD.Columns[r_name], false);

            ds.Relations.Add(myDataRelation);

			foreach (DataRow _drD in _dtD.Rows)
			{
				DataRow dr = _dtCp.NewRow();
				dr.ItemArray = _drD.ItemArray;

				foreach(DataRow _drM in _drD.GetParentRows(myDataRelation))
				{
					for(int i = 0; i < _dtM.Columns.Count; i++)
					{
						if(_dtCp.Columns.IndexOf(_dtM.Columns[i].ColumnName) >= 0)
						{
							dr[_dtM.Columns[i].ColumnName] = _drM[_dtM.Columns[i].ColumnName];
						}
					}
				}

				_dtCp.Rows.Add(dr);
			}

			int c1 = _gM.FindRow("Y",_gM.Rows.Fixed, _gM.Cols["S"].Index, false, true, true);
			if(c1 > 0)
			{
				DataRow [] _dr = _dtCp.Select("S = 'Y'");
				DataTable _dt = _dtCp.Clone();

				for(int i = 0; i < _dr.Length; i++)
				{
					_dt.ImportRow(_dr[i]);
				}
				_dtCp.Clear();
				_dtCp = _dt;
			}

			return _dtCp;
		}
		
		#endregion

        #region -> 종료버튼

        public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
		{
			return true;
        }

        #endregion

        #endregion

        #region ♣ 그리드 이벤트

        #region -> _flex_StartEdit

        private void _flex_StartEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
		{
			try
			{
				Dass.FlexGrid.FlexGrid flex = (Dass.FlexGrid.FlexGrid)sender;

				if( flex.Cols[e.Col].Name != "S")
				{
					e.Cancel = true;	// 셀 입력상태로 못 들어가게

				}				
			}
			catch(Exception ex)
			{
				MsgEnd(ex);
			}
					
		}

		#endregion

		#region -> _flexM_AfterRowColChange

		private void _flexM_AfterRowColChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
		{
			try
			{
				if(e.OldRange.r1!=e.NewRange.r1)
				{	
					if(((Dass.FlexGrid.FlexGrid)sender).Name =="_flexM_1")
						DataBinding0(e.NewRange.r1);				
					else if(((Dass.FlexGrid.FlexGrid)sender).Name =="_flexM_2")
						DataBinding1(e.NewRange.r1);					
					else if(((Dass.FlexGrid.FlexGrid)sender).Name =="_flexM_3")
						DataBinding2(e.NewRange.r1);					
					else if(((Dass.FlexGrid.FlexGrid)sender).Name =="_flexM_4")
						DataBinding3(e.NewRange.r1);					
					else if(((Dass.FlexGrid.FlexGrid)sender).Name =="_flexM_5")
						DataBinding4(e.NewRange.r1);					
					else if(((Dass.FlexGrid.FlexGrid)sender).Name =="_flexM_6")
						DataBinding5(e.NewRange.r1);					
					else if(((Dass.FlexGrid.FlexGrid)sender).Name =="_flexM_7")
						DataBinding6(e.NewRange.r1);
                    //else if ( ( ( Dass.FlexGrid.FlexGrid )sender ).Name == "_flexM_8" )
                    //    DataBinding7( e.NewRange.r1 );	
				}				
					
			}
			catch
			{
				
			}	
		}
						
		#endregion	

		#region -> 필터 부분

		private void DataBinding0(int pi_rowindex)
		{
			try
			{						
				string ls_NO_GIR = _flexM_1[pi_rowindex,"NO_GIR"].ToString();

				string ls_filter="";				
				ls_filter = "NO_GIR = '" +ls_NO_GIR + "'";	

				_flexD_1.DataView.RowFilter = ls_filter;
				
			}
			catch(Exception ex)
			{
				throw ex;		
			}
			
		}
	
	    private void DataBinding1(int pi_rowindex)
		{
			try
			{
				string ls_CD_ITEM = _flexM_2[pi_rowindex,"CD_ITEM"].ToString();
				
				string ls_filter="";				
				ls_filter = "CD_ITEM = '" +ls_CD_ITEM + "'";	
							
				_flexD_2.DataView.RowFilter = ls_filter;

			}
			catch(Exception ex)
			{
				throw ex;		
			}			
			
		}
	
        private void DataBinding2(int pi_rowindex)
		{
			try
			{			
				string ls_CD_PARTNER = _flexM_3[pi_rowindex,"CD_PARTNER"].ToString();

				string ls_filter="";				
				ls_filter = "CD_PARTNER = '" +ls_CD_PARTNER + "'";	
				
				_flexD_3.DataView.RowFilter = ls_filter;

			}
			catch(Exception ex)
			{
				throw ex;		
			}
		}
	
	    private void DataBinding3(int pi_rowindex)
		{
			try
			{				
				string ls_CD_SALEGRP = _flexM_4[pi_rowindex,"CD_SALEGRP"].ToString();
				

				string ls_filter="";				
				ls_filter = "CD_SALEGRP = '" +ls_CD_SALEGRP + "'";
					
				_flexD_4.DataView.RowFilter = ls_filter;			
	
			}
			catch(Exception ex)
			{
				throw ex;		
			}
		}

		private void DataBinding4(int pi_rowindex)
		{
			try
			{
				string ls_DT_GIR = _flexM_5[pi_rowindex,"DT_GIR"].ToString();

				string ls_filter="";				
				ls_filter = "DT_GIR = '" +ls_DT_GIR + "'";	

				_flexD_5.DataView.RowFilter = ls_filter;	
			}
			catch(Exception ex)
			{
				throw ex;		
			}
		}

	    private void DataBinding5(int pi_rowindex)
		{
			try
			{
				string ls_TP_GI = _flexM_6[pi_rowindex,"TP_GI"].ToString();
				
				string ls_filter="";				
				ls_filter = "TP_GI = '" +ls_TP_GI + "'";
					
				_flexD_6.DataView.RowFilter = ls_filter;

			}
			catch(Exception ex)
			{
				throw ex;		
			}
		}

		private void DataBinding6(int pi_rowindex)
		{
			try
			{
				string ls_NO_PROJECT = _flexM_7[pi_rowindex,"NO_PROJECT"].ToString();
				
				string ls_filter="";				
				ls_filter = "NO_PROJECT = '" +ls_NO_PROJECT + "'";	
				
				_flexD_7.DataView.RowFilter = ls_filter;
			}
			catch(Exception ex)
			{
				throw ex;		
			}
		}

        //private void DataBinding7(int pi_rowindex)
        //{
        //    try
        //    {
        //        string ls_CD_SUMITEM = _flexD_8 [pi_rowindex, "CD_ITEM"].ToString();

        //        string ls_filter = "";
        //        ls_filter = "CD_ITEM = '" + ls_CD_SUMITEM + "'";

        //        _flexD_8.DataView.RowFilter = ls_filter;
        //    }
        //    catch ( Exception ex )
        //    {
        //        throw ex;
        //    }
        //}

        #endregion

		#endregion	

		#region ♣ 기타 이벤트

		#region -> 텝선택 
		/// <summary>
		/// 텝선택
		/// </summary>
		private void m_tabWork_Click(object sender, System.EventArgs e)
		{
			string tabName = ((TabControl)sender).SelectedTab.Name.ToString();

			switch(tabName)
			{
				case "tab1" :
				{
					if(_flexM_1.HasNormalRow)
						this.ToolBarPrintButtonEnabled = true;
					else
						this.ToolBarPrintButtonEnabled = false;
					break;
				}
				case "tab2" :
				{
					if(_flexM_2.HasNormalRow)
						this.ToolBarPrintButtonEnabled = true;
					else
						this.ToolBarPrintButtonEnabled = false;				
					break;
				}
				case "tab3" :
				{
					if(_flexM_3.HasNormalRow)
						this.ToolBarPrintButtonEnabled = true;
					else
						this.ToolBarPrintButtonEnabled = false;		
					break;
				}
				case "tab4" :
				{
					if(_flexM_4.HasNormalRow)
						this.ToolBarPrintButtonEnabled = true;
					else
						this.ToolBarPrintButtonEnabled = false;		
					break;
				}
				case "tab5" :
				{
					if(_flexM_5.HasNormalRow)
						this.ToolBarPrintButtonEnabled = true;
					else
						this.ToolBarPrintButtonEnabled = false;				
					break;
				}
				case "tab6" :
				{
					if(_flexM_6.HasNormalRow)
						this.ToolBarPrintButtonEnabled = true;
					else
						this.ToolBarPrintButtonEnabled = false;		
					break;
				}
				case "tab7" :
				{
					if(_flexM_7.HasNormalRow)
						this.ToolBarPrintButtonEnabled = true;
					else
						this.ToolBarPrintButtonEnabled = false;		
					break;
				}
                case "tab8":
                {
                    if ( _flexD_8.HasNormalRow )
                        this.ToolBarPrintButtonEnabled = true;
                    else
                        this.ToolBarPrintButtonEnabled = false;
                    break;
                }
				default :
					break;
			}
		}	
		#endregion
	
		#region -> 필수 항목 체크

		/// <summary>
		/// 조회항목 체크 함수
		/// </summary>
		private bool FieldCheck()
		{
			//의뢰일자
			if((mskDtGirStart.Text.Trim() == "") && (mskDtGirEnd.Text.Trim() == ""))
			{
				//의뢰일자 은(는) 필수 입력입니다.
				ShowMessage("WK1_004",m_lblDtGir.Text);
				mskDtGirStart.Focus();
				return false;
			}

			//의뢰일자
			if(((mskDtGirStart.Text.Trim() != "")  && (mskDtGirEnd.Text.Trim() == "")) ||
			  ((mskDtGirStart.Text.Trim() == "") && (mskDtGirEnd.Text.Trim() != "")))
			{
				//의뢰일자 은(는) 필수 입력입니다.
				ShowMessage("WK1_004",m_lblDtGir.Text);
				return false;
			}

			if(m_cboBizArea.SelectedValue.ToString() == "" || m_cboBizArea.SelectedValue.ToString() == null)
			{
				//의뢰일자 은(는) 필수 입력입니다.
				ShowMessage("WK1_004",m_lblBizarea.Text);
				return false;
			}

			return true;
		}

		#endregion
	
		#region -> Control Event 정의(거래처, 창고)
		
		private void CommonComboBox_KeyEvent(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyData == Keys.Enter)
				System.Windows.Forms.SendKeys.SendWait("{TAB}");
		}
		
		#endregion

		#region -> 날짜 관련 이벤트

		private void mskDtGirStart_Validated(object sender, System.EventArgs e)
		{
			if(!this.mskDtGirStart.IsValidated)
			{
				ShowMessage("WK1_003", m_lblDtGir.Text);
				this.mskDtGirStart.Focus();
			}

		}

		private void mskDtGirEnd_Validated(object sender, System.EventArgs e)
		{
			if(!this.mskDtGirEnd.IsValidated)
			{
				ShowMessage("WK1_003", m_lblDtGir.Text);
				this.mskDtGirEnd.Focus();
			}

		}

		#endregion

		#region -> 출고 보관장소 이벤트들

		private void txtSlGir_Enter(object sender, System.EventArgs e)
		{

			gstb_sl = bpNm_Sl.Text;
			bpNm_Sl.Text = bpNm_Sl.Tag.ToString();						
		
		}

		private void txtSlGir_Leave(object sender, System.EventArgs e)
		{
			if(bpNm_Sl.Text=="" || bpNm_Sl.Text ==null)
			{
				bpNm_Sl.Text = "";
				bpNm_Sl.Tag = "";
				return;
			}
			else if( bpNm_Sl.Tag.ToString() != bpNm_Sl.Text)
			{
				try
				{
					if( cboPlantGir.SelectedValue.ToString() == "")
					{
						Duzon.Common.Controls.MessageBoxEx.Show( this.MainFrameInterface.GetMessageDictionaryItem("PU_M000070"),this.PageName);			
						cboPlantGir.Focus();
						return;
					}


					if(SearchSL())
					{
						bpNm_Sl.Text = gstb_sl;
					}
					else
					{
						Duzon.Common.Controls.MessageBoxEx.Show( this.MainFrameInterface.GetMessageDictionaryItem("CM_M100009"),this.PageName);			
						bpNm_Sl.Tag="";
						bpNm_Sl.Text="";
						//ShowDlgPURGRPSub(tb_NM_PURGRP.Text);
						bpNm_Sl.Focus();
					}	
				}
				catch(Exception ex)
				{
					MsgEnd(ex);	
				}
			}
			else
			{
				bpNm_Sl.Text = gstb_sl;
				//	tb_dt_pr.Focus();
			}		
		
		
		}

		private bool SearchSL()
		{
			try
			{
				DataTable ds = new DataTable();	
			
                SpInfo si = new SpInfo();
                si.SpNameSelect = "SP_SA_GIRSCH_SELECT1";
                si.SpParamsSelect = new object[] { this.MainFrameInterface.LoginInfo.CompanyCode, this.bpNm_Sl.Text, cboPlantGir.SelectedValue.ToString() };
                ResultData result = (ResultData)this.FillDataTable(si);
                ds =  (DataTable)result.DataValue;
                
                
                if( ds != null && ds.Rows.Count > 0)
				{
					gstb_sl = ds.Rows[0]["NM_SL"].ToString();
					bpNm_Sl.Tag = ds.Rows[0]["CD_SL"].ToString();
					return true;
				}
				else
				{						
					return false;				
				}
			}
			catch(Exception ex)
			{
				MsgEnd(ex);	
			}
			return false;

		}


		private void txtSlGir_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyData.ToString() =="F3")
			{
				ShowDlgSLSub("");	
			}		
		
		}	

		private void btn_NM_SL_Click(object sender, System.EventArgs e)
		{
			ShowDlgSLSub("")	;		
		}

		/// <summary>
		/// 보관장소 도움창 띄우는 부분
		/// </summary>
		/// <param name="ps_search"> 검색어</param>		
		private void ShowDlgSLSub(string ps_search)
		{			
			
			if( cboPlantGir.SelectedValue.ToString() == "")
			{
				Duzon.Common.Controls.MessageBoxEx.Show( this.MainFrameInterface.GetMessageDictionaryItem("PU_M000070"),this.PageName);			
				cboPlantGir.Focus();
				return;
			}

			object obj = this.LoadHelpWindow("P_MA_SL_SUB", new object[3] {this.MainFrameInterface, ps_search,cboPlantGir.SelectedValue.ToString()});
		
			if(((Duzon.Common.Forms.BaseSearchHelp)obj).ShowDialog() == DialogResult.OK)
			{
				object[] row = (object[])((Duzon.Common.Forms.IHelpWindow)obj).ReturnValues;				
				bpNm_Sl.Text = row[0].ToString();
				bpNm_Sl.Tag= row[0].ToString();
				gstb_sl = row[1].ToString();
				cboTpBusi.Focus();
				bpNm_Sl.Text = row[1].ToString();					
			}
			else
			{					
				if(bpNm_Sl.Focused)
				{
					bpNm_Sl.Text = bpNm_Sl.Tag.ToString();
					cboTpBusi.Focus();
					bpNm_Sl.Text = gstb_sl;
				}				
				
			}			
		}


		#endregion

        #region -> TextBox Enter 이벤트

        private void TextBoxEnterEvent(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData.ToString() == "Enter" || e.KeyData.ToString() == "Down")
            {
                SendKeys.SendWait("{TAB}");
            }
            else if (e.KeyData.ToString() == "Up")
                SendKeys.SendWait("+{TAB}");
        }

        #endregion

        #region -> Control_QueryAfter

        private void Control_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            if (e.DialogResult == DialogResult.Cancel)
                return;

            switch (e.ControlName)
            {
                case "bpNm_Sl":		// 창고									
                    bpNm_Sl.CodeName = e.HelpReturn.Rows[0]["NM_SL"].ToString();
                    bpNm_Sl.CodeValue = e.HelpReturn.Rows[0]["CD_SL"].ToString();

                    break;
            }

        }

        #endregion

        #region -> Control_QueryBefore

        private void Control_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            if (cboPlantGir.SelectedValue.ToString() == string.Empty)
            {
                //의뢰일자 은(는) 필수 입력입니다.
                ShowMessage("WK1_004", label5.Text);

            }
            switch (e.HelpID)
            {
                case Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB:
                    e.HelpParam.P09_CD_PLANT = cboPlantGir.SelectedValue.ToString();

                    break;
                case Duzon.Common.Forms.Help.HelpID.P_SA_TPSO_SUB:
                    e.HelpParam.P61_CODE1 = "N";
                    e.HelpParam.P62_CODE2 = "Y";
                    break;
            }
        }

        #endregion		


        #endregion
    }
}

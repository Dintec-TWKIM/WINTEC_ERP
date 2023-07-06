//********************************************************************
// 작   성   자 : 유이열
// 작   성   일 : 2003-12-18
// 모   듈   명 : 영업
// 시 스  템 명 : 영업관리
// 서브시스템명 : 수주관리
// 페 이 지  명 : 수주진행현황 조회2
// 프로젝트  명 : P_SA_SOSCH2
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

namespace sale
{
	/// <summary>
	/// P_SA_SOSCH2에 대한 요약 설명입니다.
	/// </summary>
	public class P_SA_SOSCH2_BAK : Duzon.Common.Forms.PageBase
	{
		#region ♣ 멤버필드

		#region -> 멤버필드(일반)

        private Duzon.Common.Controls.PanelExt panel4;
		private Duzon.Common.Controls.PanelExt panel5;
		private Duzon.Common.Controls.PanelExt panel6;
		private Duzon.Common.Controls.PanelExt panel7;
		private Duzon.Common.Controls.PanelExt panel8;
		private Duzon.Common.Controls.LabelExt label5;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.TabPage tabPage5;
		private System.Windows.Forms.TabPage tabPage6;
		private Duzon.Common.Controls.PanelExt panel10;
		private Duzon.Common.Controls.PanelExt m_pnlLine4;
		private Duzon.Common.Controls.PanelExt m_pnlHead4;
		private Duzon.Common.Controls.PanelExt m_pnlLine5;
		private Duzon.Common.Controls.PanelExt m_pnlHead5;
		private Duzon.Common.Controls.PanelExt m_pnlLine3;
		private Duzon.Common.Controls.PanelExt m_pnlHead3;
		private Duzon.Common.Controls.LabelExt m_lblCD_BIZAREA;
		private Duzon.Common.Controls.DzComboBox m_cboCD_BIZAREA;
		private Duzon.Common.Controls.PanelExt panel12;
		private Duzon.Common.Controls.PanelExt panel13;
		private Duzon.Common.Controls.PanelExt panel14;
		private Duzon.Common.Controls.PanelExt panel15;
		private System.Windows.Forms.ImageList imageList1;
        private Duzon.Common.Controls.PanelExt panel11;
		private Duzon.Common.Controls.LabelExt m_lblStndItem1;
		private Duzon.Common.Controls.TextBoxExt m_txtUnit1;
		private Duzon.Common.Controls.LabelExt m_lblCdItem1;
		private Duzon.Common.Controls.LabelExt m_lblUnit1;
		private Duzon.Common.Controls.TextBoxExt m_txtStndItem1;
		private Duzon.Common.Controls.TextBoxExt m_txtCdItem1;
		private Duzon.Common.Controls.LabelExt m_lblStndItem2;
		private Duzon.Common.Controls.TextBoxExt m_txtUnit2;
		private Duzon.Common.Controls.LabelExt m_lblCdItem2;
		private Duzon.Common.Controls.LabelExt m_lblUnit2;
		private Duzon.Common.Controls.TextBoxExt m_txtStndItem2;
		private Duzon.Common.Controls.TextBoxExt m_txtCdItem2;
		private Duzon.Common.Controls.LabelExt m_lblStndItem3;
		private Duzon.Common.Controls.TextBoxExt m_txtUnit3;
		private Duzon.Common.Controls.LabelExt m_lblCdItem3;
		private Duzon.Common.Controls.LabelExt m_lblUnit3;
		private Duzon.Common.Controls.TextBoxExt m_txtStndItem3;
		private Duzon.Common.Controls.TextBoxExt m_txtCdItem3;
		private Duzon.Common.Controls.LabelExt m_lblStndItem4;
		private Duzon.Common.Controls.TextBoxExt m_txtUnit4;
		private Duzon.Common.Controls.LabelExt m_lblCdItem4;
		private Duzon.Common.Controls.LabelExt m_lblUnit4;
		private Duzon.Common.Controls.TextBoxExt m_txtStndItem4;
		private Duzon.Common.Controls.TextBoxExt m_txtCdItem4;
		private Duzon.Common.Controls.LabelExt m_lblDT_SO;
		private Duzon.Common.Controls.LabelExt m_lblCD_SALEGRP;
		private Duzon.Common.Controls.TabControlExt m_tabTAB_SO;
        private Duzon.Common.Controls.PanelExt m_pnlLine0;
		private Duzon.Common.Controls.DzComboBox m_cboFgTrans;
		private Duzon.Common.Controls.DzComboBox m_cboSlUnit;
		private Duzon.Common.Controls.DzComboBox m_cboStaSo;
		private Duzon.Common.Controls.LabelExt m_lblSlUnit;
		private Duzon.Common.Controls.LabelExt m_lblFgTrans;
        private Duzon.Common.Controls.LabelExt m_lblStaSo;
		private Duzon.Common.Controls.LabelExt m_lblCdItem;
		private Duzon.Common.Controls.TextBoxExt m_txtCdItem;
		private Duzon.Common.Controls.LabelExt m_lblStndItem;
		private Duzon.Common.Controls.TextBoxExt m_txtStndItem;
		private Duzon.Common.Controls.TextBoxExt m_txtUnit;
		private Duzon.Common.Controls.LabelExt m_lblUnit;
		private Duzon.Common.Controls.PanelExt m_pnlHead0;
		private Duzon.Common.Controls.PanelExt m_pnlLine1;
		private Duzon.Common.Controls.PanelExt m_pnlHead1;
		private Duzon.Common.Controls.PanelExt m_pnlLine2;
		private Duzon.Common.Controls.PanelExt m_pnlHead2;
		private System.ComponentModel.IContainer components;

//		private GridDataBoundGrid m_grdNoSoH = null;
//		private GridDataBoundGrid m_grdNoSoL = null;
//		private GridDataBoundGrid m_grdPartnerH = null;
//		private GridDataBoundGrid m_grdPartnerL = null;
//		private GridDataBoundGrid m_grdItemH = null;
//		private GridDataBoundGrid m_grdItemL = null;
//		private GridDataBoundGrid m_grdSaleGrpH = null;
//		private GridDataBoundGrid m_grdSaleGrpL = null;
//		private GridDataBoundGrid m_grdDuedteH = null;
//		private GridDataBoundGrid m_grdDuedteL = null;
//		private GridDataBoundGrid m_grdTpSoH = null;
//		private GridDataBoundGrid m_grdTpSoL = null;
	

		private DataSet m_dsSet;


		
		// 필터될 DataView
		private DataView gdv_LineNO_SO = new DataView();		
		private DataView gdv_LineCD_ITEM = new DataView();
		private DataView gdv_LineCD_PARTNER = new DataView();
		private DataView gdv_LineCD_GROUP = new DataView();
		private DataView gdv_LineDT_DUEDATE = new DataView();
		private DataView gdv_LineTP_SO= new DataView();
	
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
		private Duzon.Common.Controls.DatePicker m_txtDT_SO1;
		private Duzon.Common.Controls.DatePicker m_txtDT_SO2;
		private Duzon.Common.BpControls.BpCodeTextBox bpSalegrp;
        private TableLayoutPanel tableLayoutPanel1;
		private Dass.FlexGrid.FlexGrid _flexD_6;

		#endregion

		#endregion

		#region ♣ 생성자/소멸자

		#region -> 생성자
        public P_SA_SOSCH2_BAK()
		{
			m_dsSet = new DataSet();

			// 이 호출은 Windows.Forms Form 디자이너에 필요합니다.
			InitializeComponent();

			// TODO: InitForm을 호출한 다음 초기화 작업을 추가합니다.
			Load += new System.EventHandler(Page_Load);
			Paint += new System.Windows.Forms.PaintEventHandler(Page_Paint);

			base.AddAutoAnchorControl(this,m_tabTAB_SO,ControlPositionType.Single);

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

		#region Component Designer generated code
		/// <summary> 
		/// 디자이너 지원에 필요한 메서드입니다. 
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_SA_SOSCH2_BAK));
            this.panel4 = new Duzon.Common.Controls.PanelExt();
            this.bpSalegrp = new Duzon.Common.BpControls.BpCodeTextBox();
            this.m_txtDT_SO2 = new Duzon.Common.Controls.DatePicker();
            this.m_txtDT_SO1 = new Duzon.Common.Controls.DatePicker();
            this.m_cboFgTrans = new Duzon.Common.Controls.DzComboBox();
            this.m_cboSlUnit = new Duzon.Common.Controls.DzComboBox();
            this.m_cboStaSo = new Duzon.Common.Controls.DzComboBox();
            this.panel10 = new Duzon.Common.Controls.PanelExt();
            this.m_cboCD_BIZAREA = new Duzon.Common.Controls.DzComboBox();
            this.panel8 = new Duzon.Common.Controls.PanelExt();
            this.panel7 = new Duzon.Common.Controls.PanelExt();
            this.panel6 = new Duzon.Common.Controls.PanelExt();
            this.m_lblCD_BIZAREA = new Duzon.Common.Controls.LabelExt();
            this.m_lblSlUnit = new Duzon.Common.Controls.LabelExt();
            this.m_lblCD_SALEGRP = new Duzon.Common.Controls.LabelExt();
            this.panel5 = new Duzon.Common.Controls.PanelExt();
            this.m_lblFgTrans = new Duzon.Common.Controls.LabelExt();
            this.m_lblDT_SO = new Duzon.Common.Controls.LabelExt();
            this.m_lblStaSo = new Duzon.Common.Controls.LabelExt();
            this.label5 = new Duzon.Common.Controls.LabelExt();
            this.m_tabTAB_SO = new Duzon.Common.Controls.TabControlExt();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel11 = new Duzon.Common.Controls.PanelExt();
            this.m_lblStndItem = new Duzon.Common.Controls.LabelExt();
            this.m_txtUnit = new Duzon.Common.Controls.TextBoxExt();
            this.m_lblCdItem = new Duzon.Common.Controls.LabelExt();
            this.m_lblUnit = new Duzon.Common.Controls.LabelExt();
            this.m_txtStndItem = new Duzon.Common.Controls.TextBoxExt();
            this.m_txtCdItem = new Duzon.Common.Controls.TextBoxExt();
            this.m_pnlLine0 = new Duzon.Common.Controls.PanelExt();
            this._flexD_1 = new Dass.FlexGrid.FlexGrid(this.components);
            this.m_pnlHead0 = new Duzon.Common.Controls.PanelExt();
            this._flexM_1 = new Dass.FlexGrid.FlexGrid(this.components);
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel12 = new Duzon.Common.Controls.PanelExt();
            this.m_lblStndItem1 = new Duzon.Common.Controls.LabelExt();
            this.m_txtUnit1 = new Duzon.Common.Controls.TextBoxExt();
            this.m_lblCdItem1 = new Duzon.Common.Controls.LabelExt();
            this.m_lblUnit1 = new Duzon.Common.Controls.LabelExt();
            this.m_txtStndItem1 = new Duzon.Common.Controls.TextBoxExt();
            this.m_txtCdItem1 = new Duzon.Common.Controls.TextBoxExt();
            this.m_pnlLine1 = new Duzon.Common.Controls.PanelExt();
            this._flexD_2 = new Dass.FlexGrid.FlexGrid(this.components);
            this.m_pnlHead1 = new Duzon.Common.Controls.PanelExt();
            this._flexM_2 = new Dass.FlexGrid.FlexGrid(this.components);
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.m_pnlLine2 = new Duzon.Common.Controls.PanelExt();
            this._flexD_3 = new Dass.FlexGrid.FlexGrid(this.components);
            this.m_pnlHead2 = new Duzon.Common.Controls.PanelExt();
            this._flexM_3 = new Dass.FlexGrid.FlexGrid(this.components);
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.panel13 = new Duzon.Common.Controls.PanelExt();
            this.m_lblStndItem2 = new Duzon.Common.Controls.LabelExt();
            this.m_txtUnit2 = new Duzon.Common.Controls.TextBoxExt();
            this.m_lblCdItem2 = new Duzon.Common.Controls.LabelExt();
            this.m_lblUnit2 = new Duzon.Common.Controls.LabelExt();
            this.m_txtStndItem2 = new Duzon.Common.Controls.TextBoxExt();
            this.m_txtCdItem2 = new Duzon.Common.Controls.TextBoxExt();
            this.m_pnlLine4 = new Duzon.Common.Controls.PanelExt();
            this._flexD_4 = new Dass.FlexGrid.FlexGrid(this.components);
            this.m_pnlHead4 = new Duzon.Common.Controls.PanelExt();
            this._flexM_4 = new Dass.FlexGrid.FlexGrid(this.components);
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.panel14 = new Duzon.Common.Controls.PanelExt();
            this.m_lblStndItem3 = new Duzon.Common.Controls.LabelExt();
            this.m_txtUnit3 = new Duzon.Common.Controls.TextBoxExt();
            this.m_lblCdItem3 = new Duzon.Common.Controls.LabelExt();
            this.m_lblUnit3 = new Duzon.Common.Controls.LabelExt();
            this.m_txtStndItem3 = new Duzon.Common.Controls.TextBoxExt();
            this.m_txtCdItem3 = new Duzon.Common.Controls.TextBoxExt();
            this.m_pnlLine5 = new Duzon.Common.Controls.PanelExt();
            this._flexD_5 = new Dass.FlexGrid.FlexGrid(this.components);
            this.m_pnlHead5 = new Duzon.Common.Controls.PanelExt();
            this._flexM_5 = new Dass.FlexGrid.FlexGrid(this.components);
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.panel15 = new Duzon.Common.Controls.PanelExt();
            this.m_lblStndItem4 = new Duzon.Common.Controls.LabelExt();
            this.m_txtUnit4 = new Duzon.Common.Controls.TextBoxExt();
            this.m_lblCdItem4 = new Duzon.Common.Controls.LabelExt();
            this.m_lblUnit4 = new Duzon.Common.Controls.LabelExt();
            this.m_txtStndItem4 = new Duzon.Common.Controls.TextBoxExt();
            this.m_txtCdItem4 = new Duzon.Common.Controls.TextBoxExt();
            this.m_pnlLine3 = new Duzon.Common.Controls.PanelExt();
            this._flexD_6 = new Dass.FlexGrid.FlexGrid(this.components);
            this.m_pnlHead3 = new Duzon.Common.Controls.PanelExt();
            this._flexM_6 = new Dass.FlexGrid.FlexGrid(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtDT_SO2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtDT_SO1)).BeginInit();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.m_tabTAB_SO.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel11.SuspendLayout();
            this.m_pnlLine0.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexD_1)).BeginInit();
            this.m_pnlHead0.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexM_1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.panel12.SuspendLayout();
            this.m_pnlLine1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexD_2)).BeginInit();
            this.m_pnlHead1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexM_2)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.m_pnlLine2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexD_3)).BeginInit();
            this.m_pnlHead2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexM_3)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.panel13.SuspendLayout();
            this.m_pnlLine4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexD_4)).BeginInit();
            this.m_pnlHead4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexM_4)).BeginInit();
            this.tabPage5.SuspendLayout();
            this.panel14.SuspendLayout();
            this.m_pnlLine5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexD_5)).BeginInit();
            this.m_pnlHead5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexM_5)).BeginInit();
            this.tabPage6.SuspendLayout();
            this.panel15.SuspendLayout();
            this.m_pnlLine3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexD_6)).BeginInit();
            this.m_pnlHead3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexM_6)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.bpSalegrp);
            this.panel4.Controls.Add(this.m_txtDT_SO2);
            this.panel4.Controls.Add(this.m_txtDT_SO1);
            this.panel4.Controls.Add(this.m_cboFgTrans);
            this.panel4.Controls.Add(this.m_cboSlUnit);
            this.panel4.Controls.Add(this.m_cboStaSo);
            this.panel4.Controls.Add(this.panel10);
            this.panel4.Controls.Add(this.m_cboCD_BIZAREA);
            this.panel4.Controls.Add(this.panel8);
            this.panel4.Controls.Add(this.panel7);
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(787, 54);
            this.panel4.TabIndex = 0;
            // 
            // bpSalegrp
            // 
            this.bpSalegrp.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpSalegrp.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpSalegrp.ButtonImage")));
            this.bpSalegrp.ChildMode = "";
            this.bpSalegrp.CodeName = "";
            this.bpSalegrp.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpSalegrp.CodeValue = "";
            this.bpSalegrp.ComboCheck = true;
            this.bpSalegrp.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_SALEGRP_SUB;
            this.bpSalegrp.ItemBackColor = System.Drawing.Color.White;
            this.bpSalegrp.Location = new System.Drawing.Point(361, 27);
            this.bpSalegrp.Name = "bpSalegrp";
            this.bpSalegrp.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpSalegrp.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.bpSalegrp.SearchCode = true;
            this.bpSalegrp.SelectCount = 0;
            this.bpSalegrp.SetDefaultValue = false;
            this.bpSalegrp.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpSalegrp.Size = new System.Drawing.Size(183, 21);
            this.bpSalegrp.TabIndex = 5;
            this.bpSalegrp.TabStop = false;
            this.bpSalegrp.Text = "bpCodeTextBox1";
            this.bpSalegrp.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
            // 
            // m_txtDT_SO2
            // 
            this.m_txtDT_SO2.CalendarBackColor = System.Drawing.Color.White;
            this.m_txtDT_SO2.DayColor = System.Drawing.SystemColors.ControlText;
            this.m_txtDT_SO2.FriDayColor = System.Drawing.Color.Blue;
            this.m_txtDT_SO2.Location = new System.Drawing.Point(189, 2);
            this.m_txtDT_SO2.Mask = "####/##/##";
            this.m_txtDT_SO2.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.m_txtDT_SO2.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.m_txtDT_SO2.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.m_txtDT_SO2.Modified = false;
            this.m_txtDT_SO2.Name = "m_txtDT_SO2";
            this.m_txtDT_SO2.PaddingCharacter = '_';
            this.m_txtDT_SO2.PassivePromptCharacter = '_';
            this.m_txtDT_SO2.PromptCharacter = '_';
            this.m_txtDT_SO2.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.m_txtDT_SO2.ShowToDay = true;
            this.m_txtDT_SO2.ShowTodayCircle = true;
            this.m_txtDT_SO2.ShowUpDown = false;
            this.m_txtDT_SO2.Size = new System.Drawing.Size(84, 21);
            this.m_txtDT_SO2.SunDayColor = System.Drawing.Color.Red;
            this.m_txtDT_SO2.TabIndex = 1;
            this.m_txtDT_SO2.TitleBackColor = System.Drawing.SystemColors.Control;
            this.m_txtDT_SO2.TitleForeColor = System.Drawing.Color.Black;
            this.m_txtDT_SO2.ToDayColor = System.Drawing.Color.Red;
            this.m_txtDT_SO2.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.m_txtDT_SO2.UseKeyF3 = false;
            this.m_txtDT_SO2.Value = new System.DateTime(((long)(0)));
            this.m_txtDT_SO2.Validated += new System.EventHandler(this.m_txtDT_SO2_Validated);
            // 
            // m_txtDT_SO1
            // 
            this.m_txtDT_SO1.CalendarBackColor = System.Drawing.Color.White;
            this.m_txtDT_SO1.DayColor = System.Drawing.SystemColors.ControlText;
            this.m_txtDT_SO1.FriDayColor = System.Drawing.Color.Blue;
            this.m_txtDT_SO1.Location = new System.Drawing.Point(84, 2);
            this.m_txtDT_SO1.Mask = "####/##/##";
            this.m_txtDT_SO1.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.m_txtDT_SO1.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.m_txtDT_SO1.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.m_txtDT_SO1.Modified = false;
            this.m_txtDT_SO1.Name = "m_txtDT_SO1";
            this.m_txtDT_SO1.PaddingCharacter = '_';
            this.m_txtDT_SO1.PassivePromptCharacter = '_';
            this.m_txtDT_SO1.PromptCharacter = '_';
            this.m_txtDT_SO1.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.m_txtDT_SO1.ShowToDay = true;
            this.m_txtDT_SO1.ShowTodayCircle = true;
            this.m_txtDT_SO1.ShowUpDown = false;
            this.m_txtDT_SO1.Size = new System.Drawing.Size(84, 21);
            this.m_txtDT_SO1.SunDayColor = System.Drawing.Color.Red;
            this.m_txtDT_SO1.TabIndex = 0;
            this.m_txtDT_SO1.TitleBackColor = System.Drawing.SystemColors.Control;
            this.m_txtDT_SO1.TitleForeColor = System.Drawing.Color.Black;
            this.m_txtDT_SO1.ToDayColor = System.Drawing.Color.Red;
            this.m_txtDT_SO1.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.m_txtDT_SO1.UseKeyF3 = false;
            this.m_txtDT_SO1.Value = new System.DateTime(((long)(0)));
            this.m_txtDT_SO1.Validated += new System.EventHandler(this.m_txtDT_SO1_Validated);
            // 
            // m_cboFgTrans
            // 
            this.m_cboFgTrans.AutoDropDown = true;
            this.m_cboFgTrans.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboFgTrans.Location = new System.Drawing.Point(84, 27);
            this.m_cboFgTrans.Name = "m_cboFgTrans";
            this.m_cboFgTrans.Size = new System.Drawing.Size(191, 20);
            this.m_cboFgTrans.TabIndex = 4;
            this.m_cboFgTrans.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnEvent_KeyDown);
            // 
            // m_cboSlUnit
            // 
            this.m_cboSlUnit.AutoDropDown = true;
            this.m_cboSlUnit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.m_cboSlUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboSlUnit.Location = new System.Drawing.Point(633, 3);
            this.m_cboSlUnit.Name = "m_cboSlUnit";
            this.m_cboSlUnit.Size = new System.Drawing.Size(150, 20);
            this.m_cboSlUnit.TabIndex = 7;
            this.m_cboSlUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnEvent_KeyDown);
            // 
            // m_cboStaSo
            // 
            this.m_cboStaSo.AutoDropDown = true;
            this.m_cboStaSo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboStaSo.Location = new System.Drawing.Point(633, 27);
            this.m_cboStaSo.Name = "m_cboStaSo";
            this.m_cboStaSo.Size = new System.Drawing.Size(150, 20);
            this.m_cboStaSo.TabIndex = 6;
            this.m_cboStaSo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnEvent_KeyDown);
            // 
            // panel10
            // 
            this.panel10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel10.BackColor = System.Drawing.Color.Transparent;
            this.panel10.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel10.BackgroundImage")));
            this.panel10.Location = new System.Drawing.Point(5, 49);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(777, 1);
            this.panel10.TabIndex = 72;
            // 
            // m_cboCD_BIZAREA
            // 
            this.m_cboCD_BIZAREA.AutoDropDown = true;
            this.m_cboCD_BIZAREA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.m_cboCD_BIZAREA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboCD_BIZAREA.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.m_cboCD_BIZAREA.ItemHeight = 12;
            this.m_cboCD_BIZAREA.Location = new System.Drawing.Point(361, 3);
            this.m_cboCD_BIZAREA.Name = "m_cboCD_BIZAREA";
            this.m_cboCD_BIZAREA.Size = new System.Drawing.Size(187, 20);
            this.m_cboCD_BIZAREA.TabIndex = 2;
            this.m_cboCD_BIZAREA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnEvent_KeyDown);
            // 
            // panel8
            // 
            this.panel8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel8.BackColor = System.Drawing.Color.Transparent;
            this.panel8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel8.BackgroundImage")));
            this.panel8.Location = new System.Drawing.Point(5, 24);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(777, 1);
            this.panel8.TabIndex = 3;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel7.Controls.Add(this.m_lblSlUnit);
            this.panel7.Controls.Add(this.m_lblStaSo);
            this.panel7.Location = new System.Drawing.Point(550, 1);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(80, 49);
            this.panel7.TabIndex = 2;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel6.Controls.Add(this.m_lblCD_BIZAREA);
            this.panel6.Controls.Add(this.m_lblCD_SALEGRP);
            this.panel6.Location = new System.Drawing.Point(278, 1);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(80, 49);
            this.panel6.TabIndex = 1;
            // 
            // m_lblCD_BIZAREA
            // 
            this.m_lblCD_BIZAREA.Location = new System.Drawing.Point(3, 5);
            this.m_lblCD_BIZAREA.Name = "m_lblCD_BIZAREA";
            this.m_lblCD_BIZAREA.Resizeble = true;
            this.m_lblCD_BIZAREA.Size = new System.Drawing.Size(75, 18);
            this.m_lblCD_BIZAREA.TabIndex = 74;
            this.m_lblCD_BIZAREA.Tag = "CD_BIZAREA";
            this.m_lblCD_BIZAREA.Text = "사업장";
            this.m_lblCD_BIZAREA.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblSlUnit
            // 
            this.m_lblSlUnit.Location = new System.Drawing.Point(4, 4);
            this.m_lblSlUnit.Name = "m_lblSlUnit";
            this.m_lblSlUnit.Resizeble = true;
            this.m_lblSlUnit.Size = new System.Drawing.Size(75, 18);
            this.m_lblSlUnit.TabIndex = 73;
            this.m_lblSlUnit.Tag = "SL_UNIT";
            this.m_lblSlUnit.Text = "단위선택";
            this.m_lblSlUnit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblCD_SALEGRP
            // 
            this.m_lblCD_SALEGRP.Location = new System.Drawing.Point(3, 29);
            this.m_lblCD_SALEGRP.Name = "m_lblCD_SALEGRP";
            this.m_lblCD_SALEGRP.Resizeble = true;
            this.m_lblCD_SALEGRP.Size = new System.Drawing.Size(75, 18);
            this.m_lblCD_SALEGRP.TabIndex = 72;
            this.m_lblCD_SALEGRP.Tag = "CD_SALEGRP";
            this.m_lblCD_SALEGRP.Text = "영업그룹";
            this.m_lblCD_SALEGRP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel5.Controls.Add(this.m_lblFgTrans);
            this.panel5.Controls.Add(this.m_lblDT_SO);
            this.panel5.Location = new System.Drawing.Point(1, 1);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(80, 49);
            this.panel5.TabIndex = 0;
            // 
            // m_lblFgTrans
            // 
            this.m_lblFgTrans.Location = new System.Drawing.Point(3, 28);
            this.m_lblFgTrans.Name = "m_lblFgTrans";
            this.m_lblFgTrans.Resizeble = true;
            this.m_lblFgTrans.Size = new System.Drawing.Size(75, 18);
            this.m_lblFgTrans.TabIndex = 74;
            this.m_lblFgTrans.Tag = "TP_BUSI";
            this.m_lblFgTrans.Text = "거래구분";
            this.m_lblFgTrans.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblDT_SO
            // 
            this.m_lblDT_SO.Location = new System.Drawing.Point(3, 5);
            this.m_lblDT_SO.Name = "m_lblDT_SO";
            this.m_lblDT_SO.Resizeble = true;
            this.m_lblDT_SO.Size = new System.Drawing.Size(75, 18);
            this.m_lblDT_SO.TabIndex = 71;
            this.m_lblDT_SO.Tag = "DT_SO";
            this.m_lblDT_SO.Text = "수주일자";
            this.m_lblDT_SO.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblStaSo
            // 
            this.m_lblStaSo.Location = new System.Drawing.Point(4, 29);
            this.m_lblStaSo.Name = "m_lblStaSo";
            this.m_lblStaSo.Resizeble = true;
            this.m_lblStaSo.Size = new System.Drawing.Size(75, 18);
            this.m_lblStaSo.TabIndex = 73;
            this.m_lblStaSo.Tag = "STA_SO";
            this.m_lblStaSo.Text = "진행상태";
            this.m_lblStaSo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(168, 5);
            this.label5.Name = "label5";
            this.label5.Resizeble = true;
            this.label5.Size = new System.Drawing.Size(18, 18);
            this.label5.TabIndex = 1;
            this.label5.Text = "∼";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_tabTAB_SO
            // 
            this.m_tabTAB_SO.Controls.Add(this.tabPage1);
            this.m_tabTAB_SO.Controls.Add(this.tabPage2);
            this.m_tabTAB_SO.Controls.Add(this.tabPage3);
            this.m_tabTAB_SO.Controls.Add(this.tabPage4);
            this.m_tabTAB_SO.Controls.Add(this.tabPage5);
            this.m_tabTAB_SO.Controls.Add(this.tabPage6);
            this.m_tabTAB_SO.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_tabTAB_SO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tabTAB_SO.ImageList = this.imageList1;
            this.m_tabTAB_SO.ItemSize = new System.Drawing.Size(111, 20);
            this.m_tabTAB_SO.Location = new System.Drawing.Point(3, 63);
            this.m_tabTAB_SO.Name = "m_tabTAB_SO";
            this.m_tabTAB_SO.SelectedIndex = 0;
            this.m_tabTAB_SO.Size = new System.Drawing.Size(787, 495);
            this.m_tabTAB_SO.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.m_tabTAB_SO.TabIndex = 73;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage1.Controls.Add(this.panel11);
            this.tabPage1.Controls.Add(this.m_pnlLine0);
            this.tabPage1.Controls.Add(this.m_pnlHead0);
            this.tabPage1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabPage1.ImageIndex = 0;
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(779, 467);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Tag = "TAB_SO";
            this.tabPage1.Text = "수주번호별";
            // 
            // panel11
            // 
            this.panel11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel11.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel11.Controls.Add(this.m_lblStndItem);
            this.panel11.Controls.Add(this.m_txtUnit);
            this.panel11.Controls.Add(this.m_lblCdItem);
            this.panel11.Controls.Add(this.m_lblUnit);
            this.panel11.Controls.Add(this.m_txtStndItem);
            this.panel11.Controls.Add(this.m_txtCdItem);
            this.panel11.Location = new System.Drawing.Point(0, 433);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(775, 30);
            this.panel11.TabIndex = 2;
            // 
            // m_lblStndItem
            // 
            this.m_lblStndItem.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.m_lblStndItem.Location = new System.Drawing.Point(268, 6);
            this.m_lblStndItem.Name = "m_lblStndItem";
            this.m_lblStndItem.Resizeble = true;
            this.m_lblStndItem.Size = new System.Drawing.Size(90, 18);
            this.m_lblStndItem.TabIndex = 4;
            this.m_lblStndItem.Tag = "STND_ITEM";
            this.m_lblStndItem.Text = "규격";
            this.m_lblStndItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtUnit
            // 
            this.m_txtUnit.Location = new System.Drawing.Point(617, 4);
            this.m_txtUnit.Name = "m_txtUnit";
            this.m_txtUnit.ReadOnly = true;
            this.m_txtUnit.SelectedAllEnabled = false;
            this.m_txtUnit.Size = new System.Drawing.Size(150, 21);
            this.m_txtUnit.TabIndex = 7;
            this.m_txtUnit.TabStop = false;
            this.m_txtUnit.UseKeyEnter = false;
            this.m_txtUnit.UseKeyF3 = false;
            // 
            // m_lblCdItem
            // 
            this.m_lblCdItem.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.m_lblCdItem.Location = new System.Drawing.Point(13, 6);
            this.m_lblCdItem.Name = "m_lblCdItem";
            this.m_lblCdItem.Resizeble = true;
            this.m_lblCdItem.Size = new System.Drawing.Size(90, 18);
            this.m_lblCdItem.TabIndex = 2;
            this.m_lblCdItem.Tag = "NM_ITEM";
            this.m_lblCdItem.Text = "품목명";
            this.m_lblCdItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblUnit
            // 
            this.m_lblUnit.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.m_lblUnit.Location = new System.Drawing.Point(523, 6);
            this.m_lblUnit.Name = "m_lblUnit";
            this.m_lblUnit.Resizeble = true;
            this.m_lblUnit.Size = new System.Drawing.Size(90, 18);
            this.m_lblUnit.TabIndex = 6;
            this.m_lblUnit.Tag = "UNIT";
            this.m_lblUnit.Text = "단위";
            this.m_lblUnit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtStndItem
            // 
            this.m_txtStndItem.Location = new System.Drawing.Point(361, 4);
            this.m_txtStndItem.Name = "m_txtStndItem";
            this.m_txtStndItem.ReadOnly = true;
            this.m_txtStndItem.SelectedAllEnabled = false;
            this.m_txtStndItem.Size = new System.Drawing.Size(150, 21);
            this.m_txtStndItem.TabIndex = 5;
            this.m_txtStndItem.TabStop = false;
            this.m_txtStndItem.UseKeyEnter = false;
            this.m_txtStndItem.UseKeyF3 = false;
            // 
            // m_txtCdItem
            // 
            this.m_txtCdItem.Location = new System.Drawing.Point(106, 4);
            this.m_txtCdItem.Name = "m_txtCdItem";
            this.m_txtCdItem.ReadOnly = true;
            this.m_txtCdItem.SelectedAllEnabled = false;
            this.m_txtCdItem.Size = new System.Drawing.Size(150, 21);
            this.m_txtCdItem.TabIndex = 3;
            this.m_txtCdItem.TabStop = false;
            this.m_txtCdItem.UseKeyEnter = false;
            this.m_txtCdItem.UseKeyF3 = false;
            // 
            // m_pnlLine0
            // 
            this.m_pnlLine0.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_pnlLine0.Controls.Add(this._flexD_1);
            this.m_pnlLine0.Cursor = System.Windows.Forms.Cursors.Default;
            this.m_pnlLine0.Location = new System.Drawing.Point(0, 207);
            this.m_pnlLine0.Name = "m_pnlLine0";
            this.m_pnlLine0.Size = new System.Drawing.Size(775, 223);
            this.m_pnlLine0.TabIndex = 1;
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
            this._flexD_1.Location = new System.Drawing.Point(0, 0);
            this._flexD_1.Name = "_flexD_1";
            this._flexD_1.Rows.Count = 1;
            this._flexD_1.Rows.DefaultSize = 20;
            this._flexD_1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexD_1.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexD_1.ShowSort = false;
            this._flexD_1.Size = new System.Drawing.Size(775, 223);
            this._flexD_1.StyleInfo = resources.GetString("_flexD_1.StyleInfo");
            this._flexD_1.TabIndex = 1;
            // 
            // m_pnlHead0
            // 
            this.m_pnlHead0.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_pnlHead0.Controls.Add(this._flexM_1);
            this.m_pnlHead0.Cursor = System.Windows.Forms.Cursors.Default;
            this.m_pnlHead0.Location = new System.Drawing.Point(0, 0);
            this.m_pnlHead0.Name = "m_pnlHead0";
            this.m_pnlHead0.Size = new System.Drawing.Size(775, 205);
            this.m_pnlHead0.TabIndex = 0;
            // 
            // _flexM_1
            // 
            this._flexM_1.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexM_1.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexM_1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexM_1.AutoResize = false;
            this._flexM_1.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexM_1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexM_1.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexM_1.EnabledHeaderCheck = true;
            this._flexM_1.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexM_1.Location = new System.Drawing.Point(0, 0);
            this._flexM_1.Name = "_flexM_1";
            this._flexM_1.Rows.Count = 1;
            this._flexM_1.Rows.DefaultSize = 20;
            this._flexM_1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexM_1.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexM_1.ShowSort = false;
            this._flexM_1.Size = new System.Drawing.Size(775, 205);
            this._flexM_1.StyleInfo = resources.GetString("_flexM_1.StyleInfo");
            this._flexM_1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.tabPage2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage2.Controls.Add(this.panel12);
            this.tabPage2.Controls.Add(this.m_pnlLine1);
            this.tabPage2.Controls.Add(this.m_pnlHead1);
            this.tabPage2.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabPage2.ImageIndex = 1;
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(779, 443);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Tag = "TAB_PARTNER";
            this.tabPage2.Text = "거래처별";
            // 
            // panel12
            // 
            this.panel12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel12.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel12.Controls.Add(this.m_lblStndItem1);
            this.panel12.Controls.Add(this.m_txtUnit1);
            this.panel12.Controls.Add(this.m_lblCdItem1);
            this.panel12.Controls.Add(this.m_lblUnit1);
            this.panel12.Controls.Add(this.m_txtStndItem1);
            this.panel12.Controls.Add(this.m_txtCdItem1);
            this.panel12.Location = new System.Drawing.Point(253, 409);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(522, 30);
            this.panel12.TabIndex = 3;
            // 
            // m_lblStndItem1
            // 
            this.m_lblStndItem1.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.m_lblStndItem1.Location = new System.Drawing.Point(177, 6);
            this.m_lblStndItem1.Name = "m_lblStndItem1";
            this.m_lblStndItem1.Resizeble = true;
            this.m_lblStndItem1.Size = new System.Drawing.Size(60, 18);
            this.m_lblStndItem1.TabIndex = 4;
            this.m_lblStndItem1.Tag = "STND_ITEM";
            this.m_lblStndItem1.Text = "규격";
            this.m_lblStndItem1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtUnit1
            // 
            this.m_txtUnit1.Location = new System.Drawing.Point(414, 4);
            this.m_txtUnit1.Name = "m_txtUnit1";
            this.m_txtUnit1.ReadOnly = true;
            this.m_txtUnit1.SelectedAllEnabled = false;
            this.m_txtUnit1.Size = new System.Drawing.Size(100, 21);
            this.m_txtUnit1.TabIndex = 7;
            this.m_txtUnit1.TabStop = false;
            this.m_txtUnit1.UseKeyEnter = false;
            this.m_txtUnit1.UseKeyF3 = false;
            // 
            // m_lblCdItem1
            // 
            this.m_lblCdItem1.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.m_lblCdItem1.Location = new System.Drawing.Point(3, 6);
            this.m_lblCdItem1.Name = "m_lblCdItem1";
            this.m_lblCdItem1.Resizeble = true;
            this.m_lblCdItem1.Size = new System.Drawing.Size(60, 18);
            this.m_lblCdItem1.TabIndex = 2;
            this.m_lblCdItem1.Tag = "NM_ITEM";
            this.m_lblCdItem1.Text = "품목명";
            this.m_lblCdItem1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblUnit1
            // 
            this.m_lblUnit1.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.m_lblUnit1.Location = new System.Drawing.Point(351, 6);
            this.m_lblUnit1.Name = "m_lblUnit1";
            this.m_lblUnit1.Resizeble = true;
            this.m_lblUnit1.Size = new System.Drawing.Size(60, 18);
            this.m_lblUnit1.TabIndex = 6;
            this.m_lblUnit1.Tag = "UNIT";
            this.m_lblUnit1.Text = "단위";
            this.m_lblUnit1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtStndItem1
            // 
            this.m_txtStndItem1.Location = new System.Drawing.Point(239, 4);
            this.m_txtStndItem1.Name = "m_txtStndItem1";
            this.m_txtStndItem1.ReadOnly = true;
            this.m_txtStndItem1.SelectedAllEnabled = false;
            this.m_txtStndItem1.Size = new System.Drawing.Size(100, 21);
            this.m_txtStndItem1.TabIndex = 5;
            this.m_txtStndItem1.TabStop = false;
            this.m_txtStndItem1.UseKeyEnter = false;
            this.m_txtStndItem1.UseKeyF3 = false;
            // 
            // m_txtCdItem1
            // 
            this.m_txtCdItem1.Location = new System.Drawing.Point(67, 4);
            this.m_txtCdItem1.Name = "m_txtCdItem1";
            this.m_txtCdItem1.ReadOnly = true;
            this.m_txtCdItem1.SelectedAllEnabled = false;
            this.m_txtCdItem1.Size = new System.Drawing.Size(100, 21);
            this.m_txtCdItem1.TabIndex = 3;
            this.m_txtCdItem1.TabStop = false;
            this.m_txtCdItem1.UseKeyEnter = false;
            this.m_txtCdItem1.UseKeyF3 = false;
            // 
            // m_pnlLine1
            // 
            this.m_pnlLine1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_pnlLine1.Controls.Add(this._flexD_2);
            this.m_pnlLine1.Cursor = System.Windows.Forms.Cursors.Default;
            this.m_pnlLine1.Location = new System.Drawing.Point(253, 0);
            this.m_pnlLine1.Name = "m_pnlLine1";
            this.m_pnlLine1.Size = new System.Drawing.Size(522, 405);
            this.m_pnlLine1.TabIndex = 1;
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
            this._flexD_2.Location = new System.Drawing.Point(0, 0);
            this._flexD_2.Name = "_flexD_2";
            this._flexD_2.Rows.Count = 1;
            this._flexD_2.Rows.DefaultSize = 20;
            this._flexD_2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexD_2.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexD_2.ShowSort = false;
            this._flexD_2.Size = new System.Drawing.Size(522, 405);
            this._flexD_2.StyleInfo = resources.GetString("_flexD_2.StyleInfo");
            this._flexD_2.TabIndex = 1;
            // 
            // m_pnlHead1
            // 
            this.m_pnlHead1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.m_pnlHead1.Controls.Add(this._flexM_2);
            this.m_pnlHead1.Cursor = System.Windows.Forms.Cursors.Default;
            this.m_pnlHead1.Location = new System.Drawing.Point(0, 0);
            this.m_pnlHead1.Name = "m_pnlHead1";
            this.m_pnlHead1.Size = new System.Drawing.Size(250, 439);
            this.m_pnlHead1.TabIndex = 0;
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
            this._flexM_2.Location = new System.Drawing.Point(0, 0);
            this._flexM_2.Name = "_flexM_2";
            this._flexM_2.Rows.Count = 1;
            this._flexM_2.Rows.DefaultSize = 20;
            this._flexM_2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexM_2.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexM_2.ShowSort = false;
            this._flexM_2.Size = new System.Drawing.Size(250, 439);
            this._flexM_2.StyleInfo = resources.GetString("_flexM_2.StyleInfo");
            this._flexM_2.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.tabPage3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage3.Controls.Add(this.m_pnlLine2);
            this.tabPage3.Controls.Add(this.m_pnlHead2);
            this.tabPage3.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabPage3.ImageIndex = 2;
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(779, 443);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Tag = "TAB_ITEM";
            this.tabPage3.Text = "품목코드별";
            // 
            // m_pnlLine2
            // 
            this.m_pnlLine2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_pnlLine2.Controls.Add(this._flexD_3);
            this.m_pnlLine2.Cursor = System.Windows.Forms.Cursors.Default;
            this.m_pnlLine2.Location = new System.Drawing.Point(294, 0);
            this.m_pnlLine2.Name = "m_pnlLine2";
            this.m_pnlLine2.Size = new System.Drawing.Size(481, 439);
            this.m_pnlLine2.TabIndex = 1;
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
            this._flexD_3.Location = new System.Drawing.Point(0, 0);
            this._flexD_3.Name = "_flexD_3";
            this._flexD_3.Rows.Count = 1;
            this._flexD_3.Rows.DefaultSize = 20;
            this._flexD_3.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexD_3.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexD_3.ShowSort = false;
            this._flexD_3.Size = new System.Drawing.Size(481, 439);
            this._flexD_3.StyleInfo = resources.GetString("_flexD_3.StyleInfo");
            this._flexD_3.TabIndex = 1;
            // 
            // m_pnlHead2
            // 
            this.m_pnlHead2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.m_pnlHead2.Controls.Add(this._flexM_3);
            this.m_pnlHead2.Cursor = System.Windows.Forms.Cursors.Default;
            this.m_pnlHead2.Location = new System.Drawing.Point(0, 0);
            this.m_pnlHead2.Name = "m_pnlHead2";
            this.m_pnlHead2.Size = new System.Drawing.Size(291, 439);
            this.m_pnlHead2.TabIndex = 0;
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
            this._flexM_3.Location = new System.Drawing.Point(0, 0);
            this._flexM_3.Name = "_flexM_3";
            this._flexM_3.Rows.Count = 1;
            this._flexM_3.Rows.DefaultSize = 20;
            this._flexM_3.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexM_3.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexM_3.ShowSort = false;
            this._flexM_3.Size = new System.Drawing.Size(291, 439);
            this._flexM_3.StyleInfo = resources.GetString("_flexM_3.StyleInfo");
            this._flexM_3.TabIndex = 1;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.tabPage4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage4.Controls.Add(this.panel13);
            this.tabPage4.Controls.Add(this.m_pnlLine4);
            this.tabPage4.Controls.Add(this.m_pnlHead4);
            this.tabPage4.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabPage4.ImageIndex = 3;
            this.tabPage4.Location = new System.Drawing.Point(4, 24);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(779, 443);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Tag = "DT_DUEBY";
            this.tabPage4.Text = "납기일별";
            // 
            // panel13
            // 
            this.panel13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel13.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel13.Controls.Add(this.m_lblStndItem2);
            this.panel13.Controls.Add(this.m_txtUnit2);
            this.panel13.Controls.Add(this.m_lblCdItem2);
            this.panel13.Controls.Add(this.m_lblUnit2);
            this.panel13.Controls.Add(this.m_txtStndItem2);
            this.panel13.Controls.Add(this.m_txtCdItem2);
            this.panel13.Location = new System.Drawing.Point(183, 409);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(592, 30);
            this.panel13.TabIndex = 4;
            // 
            // m_lblStndItem2
            // 
            this.m_lblStndItem2.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.m_lblStndItem2.Location = new System.Drawing.Point(205, 6);
            this.m_lblStndItem2.Name = "m_lblStndItem2";
            this.m_lblStndItem2.Resizeble = true;
            this.m_lblStndItem2.Size = new System.Drawing.Size(60, 18);
            this.m_lblStndItem2.TabIndex = 4;
            this.m_lblStndItem2.Tag = "STND_ITEM";
            this.m_lblStndItem2.Text = "규격";
            this.m_lblStndItem2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtUnit2
            // 
            this.m_txtUnit2.Location = new System.Drawing.Point(464, 4);
            this.m_txtUnit2.Name = "m_txtUnit2";
            this.m_txtUnit2.ReadOnly = true;
            this.m_txtUnit2.SelectedAllEnabled = false;
            this.m_txtUnit2.Size = new System.Drawing.Size(120, 21);
            this.m_txtUnit2.TabIndex = 7;
            this.m_txtUnit2.TabStop = false;
            this.m_txtUnit2.UseKeyEnter = false;
            this.m_txtUnit2.UseKeyF3 = false;
            // 
            // m_lblCdItem2
            // 
            this.m_lblCdItem2.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.m_lblCdItem2.Location = new System.Drawing.Point(8, 6);
            this.m_lblCdItem2.Name = "m_lblCdItem2";
            this.m_lblCdItem2.Resizeble = true;
            this.m_lblCdItem2.Size = new System.Drawing.Size(60, 18);
            this.m_lblCdItem2.TabIndex = 2;
            this.m_lblCdItem2.Tag = "NM_ITEM";
            this.m_lblCdItem2.Text = "품목명";
            this.m_lblCdItem2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblUnit2
            // 
            this.m_lblUnit2.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.m_lblUnit2.Location = new System.Drawing.Point(401, 6);
            this.m_lblUnit2.Name = "m_lblUnit2";
            this.m_lblUnit2.Resizeble = true;
            this.m_lblUnit2.Size = new System.Drawing.Size(60, 18);
            this.m_lblUnit2.TabIndex = 6;
            this.m_lblUnit2.Tag = "UNIT";
            this.m_lblUnit2.Text = "단위";
            this.m_lblUnit2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtStndItem2
            // 
            this.m_txtStndItem2.Location = new System.Drawing.Point(267, 4);
            this.m_txtStndItem2.Name = "m_txtStndItem2";
            this.m_txtStndItem2.ReadOnly = true;
            this.m_txtStndItem2.SelectedAllEnabled = false;
            this.m_txtStndItem2.Size = new System.Drawing.Size(120, 21);
            this.m_txtStndItem2.TabIndex = 5;
            this.m_txtStndItem2.TabStop = false;
            this.m_txtStndItem2.UseKeyEnter = false;
            this.m_txtStndItem2.UseKeyF3 = false;
            // 
            // m_txtCdItem2
            // 
            this.m_txtCdItem2.Location = new System.Drawing.Point(72, 4);
            this.m_txtCdItem2.Name = "m_txtCdItem2";
            this.m_txtCdItem2.ReadOnly = true;
            this.m_txtCdItem2.SelectedAllEnabled = false;
            this.m_txtCdItem2.Size = new System.Drawing.Size(120, 21);
            this.m_txtCdItem2.TabIndex = 3;
            this.m_txtCdItem2.TabStop = false;
            this.m_txtCdItem2.UseKeyEnter = false;
            this.m_txtCdItem2.UseKeyF3 = false;
            // 
            // m_pnlLine4
            // 
            this.m_pnlLine4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_pnlLine4.Controls.Add(this._flexD_4);
            this.m_pnlLine4.Cursor = System.Windows.Forms.Cursors.Default;
            this.m_pnlLine4.Location = new System.Drawing.Point(183, 0);
            this.m_pnlLine4.Name = "m_pnlLine4";
            this.m_pnlLine4.Size = new System.Drawing.Size(592, 406);
            this.m_pnlLine4.TabIndex = 1;
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
            this._flexD_4.Location = new System.Drawing.Point(0, 0);
            this._flexD_4.Name = "_flexD_4";
            this._flexD_4.Rows.Count = 1;
            this._flexD_4.Rows.DefaultSize = 20;
            this._flexD_4.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexD_4.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexD_4.ShowSort = false;
            this._flexD_4.Size = new System.Drawing.Size(592, 406);
            this._flexD_4.StyleInfo = resources.GetString("_flexD_4.StyleInfo");
            this._flexD_4.TabIndex = 1;
            // 
            // m_pnlHead4
            // 
            this.m_pnlHead4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.m_pnlHead4.Controls.Add(this._flexM_4);
            this.m_pnlHead4.Cursor = System.Windows.Forms.Cursors.Default;
            this.m_pnlHead4.Location = new System.Drawing.Point(0, 0);
            this.m_pnlHead4.Name = "m_pnlHead4";
            this.m_pnlHead4.Size = new System.Drawing.Size(180, 439);
            this.m_pnlHead4.TabIndex = 0;
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
            this._flexM_4.Location = new System.Drawing.Point(0, 0);
            this._flexM_4.Name = "_flexM_4";
            this._flexM_4.Rows.Count = 1;
            this._flexM_4.Rows.DefaultSize = 20;
            this._flexM_4.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexM_4.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexM_4.ShowSort = false;
            this._flexM_4.Size = new System.Drawing.Size(180, 439);
            this._flexM_4.StyleInfo = resources.GetString("_flexM_4.StyleInfo");
            this._flexM_4.TabIndex = 1;
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.tabPage5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage5.Controls.Add(this.panel14);
            this.tabPage5.Controls.Add(this.m_pnlLine5);
            this.tabPage5.Controls.Add(this.m_pnlHead5);
            this.tabPage5.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabPage5.ImageIndex = 4;
            this.tabPage5.Location = new System.Drawing.Point(4, 24);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(779, 443);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Tag = "TAB_TPSO";
            this.tabPage5.Text = "수주형태별";
            // 
            // panel14
            // 
            this.panel14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel14.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel14.Controls.Add(this.m_lblStndItem3);
            this.panel14.Controls.Add(this.m_txtUnit3);
            this.panel14.Controls.Add(this.m_lblCdItem3);
            this.panel14.Controls.Add(this.m_lblUnit3);
            this.panel14.Controls.Add(this.m_txtStndItem3);
            this.panel14.Controls.Add(this.m_txtCdItem3);
            this.panel14.Location = new System.Drawing.Point(253, 409);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(522, 30);
            this.panel14.TabIndex = 4;
            // 
            // m_lblStndItem3
            // 
            this.m_lblStndItem3.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.m_lblStndItem3.Location = new System.Drawing.Point(177, 6);
            this.m_lblStndItem3.Name = "m_lblStndItem3";
            this.m_lblStndItem3.Resizeble = true;
            this.m_lblStndItem3.Size = new System.Drawing.Size(60, 18);
            this.m_lblStndItem3.TabIndex = 4;
            this.m_lblStndItem3.Tag = "STND_ITEM";
            this.m_lblStndItem3.Text = "규격";
            this.m_lblStndItem3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtUnit3
            // 
            this.m_txtUnit3.Location = new System.Drawing.Point(414, 4);
            this.m_txtUnit3.Name = "m_txtUnit3";
            this.m_txtUnit3.ReadOnly = true;
            this.m_txtUnit3.SelectedAllEnabled = false;
            this.m_txtUnit3.Size = new System.Drawing.Size(100, 21);
            this.m_txtUnit3.TabIndex = 7;
            this.m_txtUnit3.TabStop = false;
            this.m_txtUnit3.UseKeyEnter = false;
            this.m_txtUnit3.UseKeyF3 = false;
            // 
            // m_lblCdItem3
            // 
            this.m_lblCdItem3.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.m_lblCdItem3.Location = new System.Drawing.Point(3, 6);
            this.m_lblCdItem3.Name = "m_lblCdItem3";
            this.m_lblCdItem3.Resizeble = true;
            this.m_lblCdItem3.Size = new System.Drawing.Size(60, 18);
            this.m_lblCdItem3.TabIndex = 2;
            this.m_lblCdItem3.Tag = "NM_ITEM";
            this.m_lblCdItem3.Text = "품목명";
            this.m_lblCdItem3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblUnit3
            // 
            this.m_lblUnit3.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.m_lblUnit3.Location = new System.Drawing.Point(351, 6);
            this.m_lblUnit3.Name = "m_lblUnit3";
            this.m_lblUnit3.Resizeble = true;
            this.m_lblUnit3.Size = new System.Drawing.Size(60, 18);
            this.m_lblUnit3.TabIndex = 6;
            this.m_lblUnit3.Tag = "UNIT";
            this.m_lblUnit3.Text = "단위";
            this.m_lblUnit3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtStndItem3
            // 
            this.m_txtStndItem3.Location = new System.Drawing.Point(239, 4);
            this.m_txtStndItem3.Name = "m_txtStndItem3";
            this.m_txtStndItem3.ReadOnly = true;
            this.m_txtStndItem3.SelectedAllEnabled = false;
            this.m_txtStndItem3.Size = new System.Drawing.Size(100, 21);
            this.m_txtStndItem3.TabIndex = 5;
            this.m_txtStndItem3.TabStop = false;
            this.m_txtStndItem3.UseKeyEnter = false;
            this.m_txtStndItem3.UseKeyF3 = false;
            // 
            // m_txtCdItem3
            // 
            this.m_txtCdItem3.Location = new System.Drawing.Point(67, 4);
            this.m_txtCdItem3.Name = "m_txtCdItem3";
            this.m_txtCdItem3.ReadOnly = true;
            this.m_txtCdItem3.SelectedAllEnabled = false;
            this.m_txtCdItem3.Size = new System.Drawing.Size(100, 21);
            this.m_txtCdItem3.TabIndex = 3;
            this.m_txtCdItem3.TabStop = false;
            this.m_txtCdItem3.UseKeyEnter = false;
            this.m_txtCdItem3.UseKeyF3 = false;
            // 
            // m_pnlLine5
            // 
            this.m_pnlLine5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_pnlLine5.Controls.Add(this._flexD_5);
            this.m_pnlLine5.Cursor = System.Windows.Forms.Cursors.Default;
            this.m_pnlLine5.Location = new System.Drawing.Point(253, 0);
            this.m_pnlLine5.Name = "m_pnlLine5";
            this.m_pnlLine5.Size = new System.Drawing.Size(522, 406);
            this.m_pnlLine5.TabIndex = 1;
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
            this._flexD_5.Location = new System.Drawing.Point(0, 0);
            this._flexD_5.Name = "_flexD_5";
            this._flexD_5.Rows.Count = 1;
            this._flexD_5.Rows.DefaultSize = 20;
            this._flexD_5.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexD_5.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexD_5.ShowSort = false;
            this._flexD_5.Size = new System.Drawing.Size(522, 406);
            this._flexD_5.StyleInfo = resources.GetString("_flexD_5.StyleInfo");
            this._flexD_5.TabIndex = 1;
            // 
            // m_pnlHead5
            // 
            this.m_pnlHead5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.m_pnlHead5.Controls.Add(this._flexM_5);
            this.m_pnlHead5.Cursor = System.Windows.Forms.Cursors.Default;
            this.m_pnlHead5.Location = new System.Drawing.Point(0, 0);
            this.m_pnlHead5.Name = "m_pnlHead5";
            this.m_pnlHead5.Size = new System.Drawing.Size(250, 439);
            this.m_pnlHead5.TabIndex = 0;
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
            this._flexM_5.Location = new System.Drawing.Point(0, 0);
            this._flexM_5.Name = "_flexM_5";
            this._flexM_5.Rows.Count = 1;
            this._flexM_5.Rows.DefaultSize = 20;
            this._flexM_5.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexM_5.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexM_5.ShowSort = false;
            this._flexM_5.Size = new System.Drawing.Size(250, 439);
            this._flexM_5.StyleInfo = resources.GetString("_flexM_5.StyleInfo");
            this._flexM_5.TabIndex = 1;
            // 
            // tabPage6
            // 
            this.tabPage6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.tabPage6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage6.Controls.Add(this.panel15);
            this.tabPage6.Controls.Add(this.m_pnlLine3);
            this.tabPage6.Controls.Add(this.m_pnlHead3);
            this.tabPage6.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabPage6.ImageIndex = 5;
            this.tabPage6.Location = new System.Drawing.Point(4, 24);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(779, 443);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Tag = "TAB_SALEGRP";
            this.tabPage6.Text = "영업그룹별";
            // 
            // panel15
            // 
            this.panel15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel15.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel15.Controls.Add(this.m_lblStndItem4);
            this.panel15.Controls.Add(this.m_txtUnit4);
            this.panel15.Controls.Add(this.m_lblCdItem4);
            this.panel15.Controls.Add(this.m_lblUnit4);
            this.panel15.Controls.Add(this.m_txtStndItem4);
            this.panel15.Controls.Add(this.m_txtCdItem4);
            this.panel15.Location = new System.Drawing.Point(193, 409);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(582, 30);
            this.panel15.TabIndex = 4;
            // 
            // m_lblStndItem4
            // 
            this.m_lblStndItem4.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.m_lblStndItem4.Location = new System.Drawing.Point(200, 6);
            this.m_lblStndItem4.Name = "m_lblStndItem4";
            this.m_lblStndItem4.Resizeble = true;
            this.m_lblStndItem4.Size = new System.Drawing.Size(60, 18);
            this.m_lblStndItem4.TabIndex = 4;
            this.m_lblStndItem4.Tag = "STND_ITEM";
            this.m_lblStndItem4.Text = "규격";
            this.m_lblStndItem4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtUnit4
            // 
            this.m_txtUnit4.Location = new System.Drawing.Point(457, 4);
            this.m_txtUnit4.Name = "m_txtUnit4";
            this.m_txtUnit4.ReadOnly = true;
            this.m_txtUnit4.SelectedAllEnabled = false;
            this.m_txtUnit4.Size = new System.Drawing.Size(117, 21);
            this.m_txtUnit4.TabIndex = 7;
            this.m_txtUnit4.TabStop = false;
            this.m_txtUnit4.UseKeyEnter = false;
            this.m_txtUnit4.UseKeyF3 = false;
            // 
            // m_lblCdItem4
            // 
            this.m_lblCdItem4.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.m_lblCdItem4.Location = new System.Drawing.Point(3, 6);
            this.m_lblCdItem4.Name = "m_lblCdItem4";
            this.m_lblCdItem4.Resizeble = true;
            this.m_lblCdItem4.Size = new System.Drawing.Size(60, 18);
            this.m_lblCdItem4.TabIndex = 2;
            this.m_lblCdItem4.Tag = "NM_ITEM";
            this.m_lblCdItem4.Text = "품목명";
            this.m_lblCdItem4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblUnit4
            // 
            this.m_lblUnit4.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.m_lblUnit4.Location = new System.Drawing.Point(393, 6);
            this.m_lblUnit4.Name = "m_lblUnit4";
            this.m_lblUnit4.Resizeble = true;
            this.m_lblUnit4.Size = new System.Drawing.Size(60, 18);
            this.m_lblUnit4.TabIndex = 6;
            this.m_lblUnit4.Tag = "UNIT";
            this.m_lblUnit4.Text = "단위";
            this.m_lblUnit4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtStndItem4
            // 
            this.m_txtStndItem4.Location = new System.Drawing.Point(264, 4);
            this.m_txtStndItem4.Name = "m_txtStndItem4";
            this.m_txtStndItem4.ReadOnly = true;
            this.m_txtStndItem4.SelectedAllEnabled = false;
            this.m_txtStndItem4.Size = new System.Drawing.Size(117, 21);
            this.m_txtStndItem4.TabIndex = 5;
            this.m_txtStndItem4.TabStop = false;
            this.m_txtStndItem4.UseKeyEnter = false;
            this.m_txtStndItem4.UseKeyF3 = false;
            // 
            // m_txtCdItem4
            // 
            this.m_txtCdItem4.Location = new System.Drawing.Point(67, 4);
            this.m_txtCdItem4.Name = "m_txtCdItem4";
            this.m_txtCdItem4.ReadOnly = true;
            this.m_txtCdItem4.SelectedAllEnabled = false;
            this.m_txtCdItem4.Size = new System.Drawing.Size(117, 21);
            this.m_txtCdItem4.TabIndex = 3;
            this.m_txtCdItem4.TabStop = false;
            this.m_txtCdItem4.UseKeyEnter = false;
            this.m_txtCdItem4.UseKeyF3 = false;
            // 
            // m_pnlLine3
            // 
            this.m_pnlLine3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_pnlLine3.Controls.Add(this._flexD_6);
            this.m_pnlLine3.Cursor = System.Windows.Forms.Cursors.Default;
            this.m_pnlLine3.Location = new System.Drawing.Point(193, 0);
            this.m_pnlLine3.Name = "m_pnlLine3";
            this.m_pnlLine3.Size = new System.Drawing.Size(582, 405);
            this.m_pnlLine3.TabIndex = 1;
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
            this._flexD_6.Location = new System.Drawing.Point(0, 0);
            this._flexD_6.Name = "_flexD_6";
            this._flexD_6.Rows.Count = 1;
            this._flexD_6.Rows.DefaultSize = 20;
            this._flexD_6.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexD_6.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexD_6.ShowSort = false;
            this._flexD_6.Size = new System.Drawing.Size(582, 405);
            this._flexD_6.StyleInfo = resources.GetString("_flexD_6.StyleInfo");
            this._flexD_6.TabIndex = 1;
            // 
            // m_pnlHead3
            // 
            this.m_pnlHead3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.m_pnlHead3.Controls.Add(this._flexM_6);
            this.m_pnlHead3.Cursor = System.Windows.Forms.Cursors.Default;
            this.m_pnlHead3.Location = new System.Drawing.Point(0, 0);
            this.m_pnlHead3.Name = "m_pnlHead3";
            this.m_pnlHead3.Size = new System.Drawing.Size(190, 439);
            this.m_pnlHead3.TabIndex = 0;
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
            this._flexM_6.Location = new System.Drawing.Point(0, 0);
            this._flexM_6.Name = "_flexM_6";
            this._flexM_6.Rows.Count = 1;
            this._flexM_6.Rows.DefaultSize = 20;
            this._flexM_6.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexM_6.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexM_6.ShowSort = false;
            this._flexM_6.Size = new System.Drawing.Size(190, 439);
            this._flexM_6.StyleInfo = resources.GetString("_flexM_6.StyleInfo");
            this._flexM_6.TabIndex = 1;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "");
            this.imageList1.Images.SetKeyName(6, "");
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.m_tabTAB_SO, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 59);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(793, 561);
            this.tableLayoutPanel1.TabIndex = 79;
            // 
            // P_SA_SOSCH2_BAK
            // 
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "P_SA_SOSCH2_BAK";
            this.TitleText = "수주진행현황";
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_txtDT_SO2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtDT_SO1)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.m_tabTAB_SO.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.m_pnlLine0.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexD_1)).EndInit();
            this.m_pnlHead0.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexM_1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            this.m_pnlLine1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexD_2)).EndInit();
            this.m_pnlHead1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexM_2)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.m_pnlLine2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexD_3)).EndInit();
            this.m_pnlHead2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexM_3)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            this.m_pnlLine4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexD_4)).EndInit();
            this.m_pnlHead4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexM_4)).EndInit();
            this.tabPage5.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            this.panel14.PerformLayout();
            this.m_pnlLine5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexD_5)).EndInit();
            this.m_pnlHead5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexM_5)).EndInit();
            this.tabPage6.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            this.panel15.PerformLayout();
            this.m_pnlLine3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexD_6)).EndInit();
            this.m_pnlHead3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexM_6)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		#endregion

		#region ♣ 초기화

		#region -> Page_Load
		/// <summary>
		/// 페이지 로드 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Page_Load(object sender, EventArgs e)
		{
			try
			{
				Cursor.Current = Cursors.WaitCursor;

				this.Enabled = false;
                //this.m_lblTitle.Visible = false;

				this.ShowStatusBarMessage(1);
				this.SetProgressBarValue(100, 20);

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
				
				Application.DoEvents();

				// 조회 버튼 활성화
				ToolBarSearchButtonEnabled = true;

				ShowStatusBarMessage(0);
				Cursor.Current = Cursors.Default;
			}
			catch(Exception ex)
			{
				ShowErrorMessage(ex, PageName);
				Cursor.Current = Cursors.Default;
			}	
		}

		#endregion

		#region -> InitControl
		/// <summary>
		/// 컨트롤들의 캡션을 데이터 사전을 이용하여 설정한다.
		/// </summary>
		private void InitControl()
		{
            //m_lblTitle.Text = GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_lblTitle.Tag);

            //m_lblDT_SO.Text = GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_lblDT_SO.Tag);
            //m_lblFgSo.Text = GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_lblFgSo.Tag);
            //m_lblCD_BIZAREA.Text = GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_lblCD_BIZAREA.Tag);
            //m_lblCD_SALEGRP.Text = GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_lblCD_SALEGRP.Tag);

            //m_lblFgTrans.Text = GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_lblFgTrans.Tag);
            //m_lblStaSo.Text = GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_lblStaSo.Tag);
            //m_lblSlUnit.Text = GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_lblSlUnit.Tag);

            //m_btnAtp.Text = GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_btnAtp.Tag);

            //m_lblCdItem.Text = GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_lblCdItem.Tag);
            //m_lblStndItem.Text = GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_lblStndItem.Tag);
            //m_lblUnit.Text = GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_lblUnit.Tag);

            //m_lblCdItem1.Text = GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_lblCdItem1.Tag);
            //m_lblStndItem1.Text = GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_lblStndItem1.Tag);
            //m_lblUnit1.Text = GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_lblUnit1.Tag);

            //m_lblCdItem2.Text = GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_lblCdItem2.Tag);
            //m_lblStndItem2.Text = GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_lblStndItem2.Tag);
            //m_lblUnit2.Text = GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_lblUnit2.Tag);

            //m_lblCdItem3.Text = GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_lblCdItem3.Tag);
            //m_lblStndItem3.Text = GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_lblStndItem3.Tag);
            //m_lblUnit3.Text = GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_lblUnit3.Tag);

            //m_lblCdItem4.Text = GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_lblCdItem4.Tag);
            //m_lblStndItem4.Text = GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_lblStndItem4.Tag);
            //m_lblUnit4.Text = GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_lblUnit4.Tag);
			
            //m_tabTAB_SO.TabPages[0].Text = GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_tabTAB_SO.TabPages[0].Tag);
            //m_tabTAB_SO.TabPages[1].Text = GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_tabTAB_SO.TabPages[1].Tag);
            //m_tabTAB_SO.TabPages[2].Text = GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_tabTAB_SO.TabPages[2].Tag);
            //m_tabTAB_SO.TabPages[3].Text = GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_tabTAB_SO.TabPages[3].Tag);
            //m_tabTAB_SO.TabPages[4].Text = GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_tabTAB_SO.TabPages[4].Tag);
            //m_tabTAB_SO.TabPages[5].Text = GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_tabTAB_SO.TabPages[5].Tag);
		}

		#endregion

		#region -> InitCombo
		/// <summary>
		/// 콤보박스 초기화
		/// </summary>
		private void InitCombo()
		{
//			string [] separateCodes = {"B_N;", "S;PU_C000027", "S;PU_C000016", "S;SA_B000016", "N;SA_B000031"};
//			object [] args = {LoginInfo.CompanyCode, separateCodes};

//			DataSet lds_Combo = (DataSet)InvokeRemoteMethod("MasterCommon", "master.CC_MA_COMBO", "CC_MA_COMBO.rem", "SettingCombos", args);
				
			DataSet lds_Combo = this.GetComboData("S;MA_BIZAREA", "S;PU_C000027", "S;PU_C000016", "S;SA_B000016", "N;SA_B000031");//S;MA_BIZAREA;코드값

			//사업장
			m_cboCD_BIZAREA.DataSource = lds_Combo.Tables[0];
			m_cboCD_BIZAREA.DisplayMember = "NAME";
			m_cboCD_BIZAREA.ValueMember = "CODE";
			m_cboCD_BIZAREA.SelectedIndex  = 1;

			//거래구분
			m_cboFgTrans.DataSource = lds_Combo.Tables[2];
			m_cboFgTrans.DisplayMember = "NAME";
			m_cboFgTrans.ValueMember = "CODE";

			//진행상태
			m_cboStaSo.DataSource = lds_Combo.Tables[3];
			m_cboStaSo.DisplayMember = "NAME";
			m_cboStaSo.ValueMember = "CODE";

			//단위선택
			m_cboSlUnit.DataSource = lds_Combo.Tables[4];
			m_cboSlUnit.DisplayMember = "NAME";
			m_cboSlUnit.ValueMember = "CODE";
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
					case "CHOOSE":			// S
						temp = temp + " + " + "S";
						break;
					case "NO_SO":		
						temp = temp + " + " + this.GetDataDictionaryItem("SA","NO_SO");
						break;
					case "DT_SO":		
						temp = temp + " + " + this.GetDataDictionaryItem("SA","DT_SO");
						break;
					case "TP_SO":		
						temp = temp + " + " + this.GetDataDictionaryItem("SA","TP_SO");
						break;						
					case "CD_PARTNER":		
						temp = temp + " + " + this.GetDataDictionaryItem("SA","SO_CDPARTNER");
						break;
					case "NM_PARTNER":		
						temp = temp + " + " + this.GetDataDictionaryItem("SA","NM_PARTNER");
						break;					
					case "NM_EXCH":			
						temp = temp + " + " + this.GetDataDictionaryItem("SA","CD_EXCH");
						break;
					case "AM_SO":		
						temp = temp + " + " + this.GetDataDictionaryItem("SA","AM_SO");
						break;
					case "RT_EXCH":		
						temp = temp + " + " + this.GetDataDictionaryItem("SA","RT_EXCH");
						break;
					case "AM_WON":		
						temp = temp + " + " + this.GetDataDictionaryItem("SA","AM_WON");
						break;
					case "NM_BIZAREA":		
						temp = temp + " + " + this.GetDataDictionaryItem("SA","CD_BIZAREA");
						break;
					case "NM_SALEGRP":		
						temp = temp + " + " + this.GetDataDictionaryItem("SA","NM_SALEGRP");
						break;		
					
									
					case "CD_ITEM":		// 품번
						temp = temp + " + " + this.GetDataDictionaryItem("SA","CD_ITEM");
						break;
					case "NM_ITEM":		// 품명
						temp = temp + " + " + this.GetDataDictionaryItem("SA","NM_ITEM");
						break;
					case "DT_DUEDATE":		// 품명
						temp = temp + " + " + this.GetDataDictionaryItem("SA","DT_DUEDATE");
						break;
					case "QT_SO":		// 환종
						temp = temp + " + " + this.GetDataDictionaryItem("SA","QT_SO");
						break;
					case "QT_GIR":			// 환종단가
						temp = temp + " + " + this.GetDataDictionaryItem("SA","QT_GIR");
						break;
					case "QT_GI":		// 금액
						temp = temp + " + " + this.GetDataDictionaryItem("SA","QT_GI");
						break;	
					case "QT_REMAINS":		// 금액
						temp = temp + " + " + this.GetDataDictionaryItem("SA","QT_REMAINS");
						break;
					case "QT_RETURN":		// 금액
						temp = temp + " + " + this.GetDataDictionaryItem("SA","QT_RETURN");
						break;
					case "QT_IV":		// 부가세
						temp = temp + " + " + this.GetDataDictionaryItem("SA","QT_IV");
						break;
					case "QT_IVREMAINS":		// 부가세
						temp = temp + " + " + this.GetDataDictionaryItem("SA","QT_IVREMAINS");
						break;
					case "NM_STA_SO":// 납품처
						temp = temp + " + " + this.GetDataDictionaryItem("SA","STA_SO");
						break;

					case "STND_ITEM":		// 부가세
						temp = temp + " + " + this.GetDataDictionaryItem("SA","STND_ITEM");
						break;
					case "UNIT":// 납품처
						temp = temp + " + " + this.GetDataDictionaryItem("SA","UNIT");
						break;

					case "NM_RET":// 납품처
						temp = temp + " + " + this.GetDataDictionaryItem("SA","FG_SO");
						break;

					case "NM_SO":// 납품처
						temp = temp + " + " + this.GetDataDictionaryItem("SA","TP_SO");
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

		#region -> 수주번호별 그리드 초기화
		
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
			_flexM_1.Cols.Count = 15;
			_flexM_1.Cols.Fixed = 1;
			_flexM_1.Rows.DefaultSize = 20;	

			_flexM_1.Cols[0].Width = 50;

			_flexM_1.Cols[1].Name = "CHOOSE";
			_flexM_1.Cols[1].DataType = typeof(string);
			_flexM_1.Cols[1].Format = "Y;N";
			_flexM_1.Cols[1].ImageAlign = ImageAlignEnum.CenterCenter;
			_flexM_1.Cols[1].Width = 40;
			
			_flexM_1.Cols[2].Name = "NO_SO";
			_flexM_1.Cols[2].DataType = typeof(string);
			_flexM_1.Cols[2].Width = 140;
			
			_flexM_1.Cols[3].Name = "DT_SO";
			_flexM_1.Cols[3].DataType = typeof(string);
			_flexM_1.Cols[3].Width = 100;
			_flexM_1.Cols[3].EditMask = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT).Replace("#","9");
			_flexM_1.Cols[3].Format = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT);
			_flexM_1.Cols[3].TextAlign = TextAlignEnum.CenterCenter;
			_flexM_1.SetStringFormatCol("DT_SO");
			
			_flexM_1.Cols[4].Name = "CD_PARTNER";
			_flexM_1.Cols[4].DataType = typeof(string);
			_flexM_1.Cols[4].Width = 100;
			
			_flexM_1.Cols[5].Name = "NM_PARTNER";
			_flexM_1.Cols[5].DataType = typeof(string);
			_flexM_1.Cols[5].Width = 100;
			
			_flexM_1.Cols[6].Name = "NM_RET";
			_flexM_1.Cols[6].DataType = typeof(string);
			_flexM_1.Cols[6].Width = 100;	
			
			_flexM_1.Cols[7].Name = "NM_SO";
			_flexM_1.Cols[7].DataType = typeof(string);
			_flexM_1.Cols[7].Width = 100;	
			_flexM_1.Cols[7].Visible = false;
			
			_flexM_1.Cols[8].Name = "NM_BUSI";
			_flexM_1.Cols[8].DataType = typeof(string);
			_flexM_1.Cols[8].Width = 100;
			_flexM_1.Cols[8].Visible = false;
			
			_flexM_1.Cols[9].Name = "NM_EXCH";
			_flexM_1.Cols[9].DataType = typeof(string);
			_flexM_1.Cols[9].Width = 100;	
			_flexM_1.Cols[9].Visible = false;
			
			_flexM_1.Cols[10].Name = "AM_SO";
			_flexM_1.Cols[10].DataType = typeof(string);
			_flexM_1.Cols[10].Width = 100;
			SetFormat(_flexM_1.Cols[10], DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.SELECT);
			_flexM_1.Cols[10].Visible = false;
			
			_flexM_1.Cols[11].Name = "RT_EXCH";
			_flexM_1.Cols[11].DataType = typeof(string);
			_flexM_1.Cols[11].Width = 100;
			_flexM_1.Cols[11].Visible = false;
			
			_flexM_1.Cols[12].Name = "AM_WON";
			_flexM_1.Cols[12].DataType = typeof(string);
			_flexM_1.Cols[12].Width = 100;
			SetFormat(_flexM_1.Cols[12], DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.SELECT);
			_flexM_1.Cols[12].Visible = false;
			
			_flexM_1.Cols[13].Name = "NM_BIZAREA";
			_flexM_1.Cols[13].DataType = typeof(string);
			_flexM_1.Cols[13].Width = 100;
			_flexM_1.Cols[13].Visible = false;
			
			_flexM_1.Cols[14].Name = "NM_SALEGRP";
			_flexM_1.Cols[14].DataType = typeof(string);
			_flexM_1.Cols[14].Width = 100;
			_flexM_1.Cols[14].Visible = false;
			
			_flexM_1.AllowSorting = AllowSortingEnum.None;
			_flexM_1.NewRowEditable = false;
			_flexM_1.EnterKeyAddRow = false;

			_flexM_1.SumPosition = SumPositionEnum.None;
			_flexM_1.GridStyle = GridStyleEnum.Green;
			
			MainFrameInterface.SetUserGrid(_flexM_1);
			
			// 그리드 헤더캡션 표시하기
			for(int i = 0; i <= _flexM_1.Cols.Count-1; i++)
				_flexM_1[0, i] = GetDDItem(_flexM_1.Cols[i].Name);

			_flexM_1.Redraw = true;	
			_flexM_1.AllowResizing = AllowResizingEnum.Columns;

			_flexM_1.StartEdit			+= new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);
			_flexM_1.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(_flexM_AfterRowColChange);	
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
			_flexD_1.Cols.Count =12;
			_flexD_1.Cols.Fixed = 1;
			_flexD_1.Rows.DefaultSize = 20;	

			_flexD_1.Cols[0].Width = 50;

			_flexD_1.Cols[1].Name = "CD_ITEM";
			_flexD_1.Cols[1].DataType = typeof(string);
			_flexD_1.Cols[1].Width = 90;
			
			_flexD_1.Cols[2].Name = "NM_ITEM";
			_flexD_1.Cols[2].DataType = typeof(string);
			_flexD_1.Cols[2].Width = 90;
			
			_flexD_1.Cols[3].Name = "DT_DUEDATE";
			_flexD_1.Cols[3].DataType = typeof(string);
			_flexD_1.Cols[3].Width = 100;
			_flexD_1.Cols[3].EditMask = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT).Replace("#","9");
			_flexD_1.Cols[3].Format = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT);
			_flexD_1.Cols[3].TextAlign = TextAlignEnum.CenterCenter;
			_flexD_1.SetStringFormatCol("DT_DUEDATE");
		
			_flexD_1.Cols[4].Name = "QT_SO";
			_flexD_1.Cols[4].DataType = typeof(decimal);
			_flexD_1.Cols[4].Width = 120;
			_flexD_1.Cols[4].TextAlign = TextAlignEnum.RightCenter;
			_flexD_1.Cols[4].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_1.SetColMaxLength("QT_SO",17);
			
			_flexD_1.Cols[5].Name = "QT_GIR";
			_flexD_1.Cols[5].DataType = typeof(decimal);
			_flexD_1.Cols[5].Width = 120;
			_flexD_1.Cols[5].TextAlign = TextAlignEnum.RightCenter;
			_flexD_1.Cols[5].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_1.SetColMaxLength("QT_GIR",17);

			_flexD_1.Cols[6].Name = "QT_GI";
			_flexD_1.Cols[6].DataType = typeof(decimal);
			_flexD_1.Cols[6].Width = 120;
			_flexD_1.Cols[6].TextAlign = TextAlignEnum.RightCenter;
			_flexD_1.Cols[6].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_1.SetColMaxLength("QT_GI",17);

			_flexD_1.Cols[7].Name = "QT_REMAINS";
			_flexD_1.Cols[7].DataType = typeof(decimal);
			_flexD_1.Cols[7].Width = 120;
			_flexD_1.Cols[7].TextAlign = TextAlignEnum.RightCenter;
			_flexD_1.Cols[7].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_1.SetColMaxLength("QT_REMAINS",17);

			_flexD_1.Cols[8].Name = "QT_RETURN";
			_flexD_1.Cols[8].DataType = typeof(decimal);
			_flexD_1.Cols[8].Width = 120;
			_flexD_1.Cols[8].TextAlign = TextAlignEnum.RightCenter;
			_flexD_1.Cols[8].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_1.SetColMaxLength("QT_RETURN",17);

			_flexD_1.Cols[9].Name = "QT_IV";
			_flexD_1.Cols[9].DataType = typeof(decimal);
			_flexD_1.Cols[9].Width = 120;
			_flexD_1.Cols[9].TextAlign = TextAlignEnum.RightCenter;
			_flexD_1.Cols[9].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_1.SetColMaxLength("QT_IV",17);

			_flexD_1.Cols[10].Name = "QT_IVREMAINS";
			_flexD_1.Cols[10].DataType = typeof(decimal);
			_flexD_1.Cols[10].Width = 120;
			_flexD_1.Cols[10].TextAlign = TextAlignEnum.RightCenter;
			_flexD_1.Cols[10].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_1.SetColMaxLength("QT_IVREMAINS",17);
	
			_flexD_1.Cols[11].Name = "NM_STA_SO";
			_flexD_1.Cols[11].DataType = typeof(string);
			_flexD_1.Cols[11].Width = 100;		
						
			_flexD_1.AllowSorting = AllowSortingEnum.None;
			_flexD_1.NewRowEditable = false;
			_flexD_1.EnterKeyAddRow = false;
			
			_flexD_1.SumPosition = SumPositionEnum.None;
			_flexD_1.GridStyle = GridStyleEnum.Blue;

			//_flexD_1.SubtotalPosition = SubtotalPositionEnum.BelowData;
		
			_flexD_1.SetDummyColumn("S");

			MainFrameInterface.SetUserGrid(_flexD_1);
			
			// 그리드 헤더캡션 표시하기
			for(int i = 0; i <= _flexD_1.Cols.Count-1; i++)
				_flexD_1[0, i] = GetDDItem(_flexD_1.Cols[i].Name);

			_flexD_1.Redraw = true;
			_flexD_1.AllowResizing = AllowResizingEnum.Columns;
			_flexD_1.StartEdit			+= new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);


		}

		#endregion

		#endregion
		
		#region -> 거래처별 그리드 초기화
		
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
			_flexM_2.Cols.Count = 4;
			_flexM_2.Cols.Fixed = 1;
			_flexM_2.Rows.DefaultSize = 20;	

			_flexM_2.Cols[0].Width = 50;

			_flexM_2.Cols[1].Name = "CHOOSE";
			_flexM_2.Cols[1].DataType = typeof(string);
			_flexM_2.Cols[1].Format = "Y;N";
			_flexM_2.Cols[1].ImageAlign = ImageAlignEnum.CenterCenter;
			_flexM_2.Cols[1].Width = 40;
			
			_flexM_2.Cols[2].Name = "CD_PARTNER";
			_flexM_2.Cols[2].DataType = typeof(string);
			_flexM_2.Cols[2].Width = 90;
					
			_flexM_2.Cols[3].Name = "NM_PARTNER";
			_flexM_2.Cols[3].DataType = typeof(string);
			_flexM_2.Cols[3].Width = 100;
			
			_flexM_2.AllowSorting = AllowSortingEnum.None;
			_flexM_2.NewRowEditable = false;
			_flexM_2.EnterKeyAddRow = false;

			_flexM_2.SumPosition = SumPositionEnum.None;
			_flexM_2.GridStyle = GridStyleEnum.Green;
			
			MainFrameInterface.SetUserGrid(_flexM_2);
			
			// 그리드 헤더캡션 표시하기
			for(int i = 0; i <= _flexM_2.Cols.Count-1; i++)
				_flexM_2[0, i] = GetDDItem(_flexM_2.Cols[i].Name);

			_flexM_2.Redraw = true;		
			_flexM_2.AllowResizing = AllowResizingEnum.Columns;
			_flexM_2.StartEdit			+= new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);
			_flexM_2.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(_flexM_AfterRowColChange);	
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
			_flexD_2.Cols.Count =16;
			_flexD_2.Cols.Fixed = 1;
			_flexD_2.Rows.DefaultSize = 20;	

			_flexD_2.Cols[0].Width = 50;

			_flexD_2.Cols[1].Name = "NM_RET";
			_flexD_2.Cols[1].DataType = typeof(string);
			_flexD_2.Cols[1].Width = 90;
	
			_flexD_2.Cols[2].Name = "CD_ITEM";
			_flexD_2.Cols[2].DataType = typeof(string);
			_flexD_2.Cols[2].Width = 100;		
			
			_flexD_2.Cols[3].Name = "NM_ITEM";
			_flexD_2.Cols[3].DataType = typeof(string);
			_flexD_2.Cols[3].Width = 100;		
			
			_flexD_2.Cols[4].Name = "DT_SO";
			_flexD_2.Cols[4].DataType = typeof(string);
			_flexD_2.Cols[4].Width = 100;
			_flexD_2.Cols[4].EditMask = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT).Replace("#","9");
			_flexD_2.Cols[4].Format = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT);
			_flexD_2.Cols[4].TextAlign = TextAlignEnum.CenterCenter;
			_flexD_2.SetStringFormatCol("DT_SO");

			_flexD_2.Cols[5].Name = "DT_DUEDATE";
			_flexD_2.Cols[5].DataType = typeof(string);
			_flexD_2.Cols[5].Width = 100;
			_flexD_2.Cols[5].EditMask = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT).Replace("#","9");
			_flexD_2.Cols[5].Format = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT);
			_flexD_2.Cols[5].TextAlign = TextAlignEnum.CenterCenter;
			_flexD_2.SetStringFormatCol("DT_DUEDATE");		

			_flexD_2.Cols[6].Name = "QT_SO";
			_flexD_2.Cols[6].DataType = typeof(decimal);
			_flexD_2.Cols[6].Width = 120;
			_flexD_2.Cols[6].TextAlign = TextAlignEnum.RightCenter;
			_flexD_2.Cols[6].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_2.SetColMaxLength("QT_SO",17);

			_flexD_2.Cols[7].Name = "QT_GIR";
			_flexD_2.Cols[7].DataType = typeof(decimal);
			_flexD_2.Cols[7].Width = 120;
			_flexD_2.Cols[7].TextAlign = TextAlignEnum.RightCenter;
			_flexD_2.Cols[7].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_2.SetColMaxLength("QT_GIR",17);

			_flexD_2.Cols[8].Name = "QT_GI";
			_flexD_2.Cols[8].DataType = typeof(decimal);
			_flexD_2.Cols[8].Width = 120;
			_flexD_2.Cols[8].TextAlign = TextAlignEnum.RightCenter;
			_flexD_2.Cols[8].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_2.SetColMaxLength("QT_GI",17);

			_flexD_2.Cols[9].Name = "QT_REMAINS";
			_flexD_2.Cols[9].DataType = typeof(decimal);
			_flexD_2.Cols[9].Width = 120;
			_flexD_2.Cols[9].TextAlign = TextAlignEnum.RightCenter;
			_flexD_2.Cols[9].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_2.SetColMaxLength("QT_REMAINS",17);

			_flexD_2.Cols[10].Name = "QT_RETURN";
			_flexD_2.Cols[10].DataType = typeof(decimal);
			_flexD_2.Cols[10].Width = 120;
			_flexD_2.Cols[10].TextAlign = TextAlignEnum.RightCenter;
			_flexD_2.Cols[10].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_2.SetColMaxLength("QT_RETURN",17);

			_flexD_2.Cols[11].Name = "QT_IV";
			_flexD_2.Cols[11].DataType = typeof(decimal);
			_flexD_2.Cols[11].Width = 120;
			_flexD_2.Cols[11].TextAlign = TextAlignEnum.RightCenter;
			_flexD_2.Cols[11].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_2.SetColMaxLength("QT_IV",17);
			
			_flexD_2.Cols[12].Name = "QT_IVREMAINS";
			_flexD_2.Cols[12].DataType = typeof(decimal);
			_flexD_2.Cols[12].Width = 120;
			_flexD_2.Cols[12].TextAlign = TextAlignEnum.RightCenter;
			_flexD_2.Cols[12].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_2.SetColMaxLength("QT_IVREMAINS",17);
			
			_flexD_2.Cols[13].Name = "NM_STA_SO";
			_flexD_2.Cols[13].DataType = typeof(string);
			_flexD_2.Cols[13].Width = 100;		
					
			_flexD_2.Cols[14].Name = "NM_SO";
			_flexD_2.Cols[14].DataType = typeof(string);
			_flexD_2.Cols[14].Width = 100;		
						
			_flexD_2.Cols[15].Name = "NO_SO";
			_flexD_2.Cols[15].DataType = typeof(string);
			_flexD_2.Cols[15].Width = 100;		
						

			_flexD_2.AllowSorting = AllowSortingEnum.None;
			_flexD_2.NewRowEditable = false;
			_flexD_2.EnterKeyAddRow = false;
			
			_flexD_2.SumPosition = SumPositionEnum.None;
			_flexD_2.GridStyle = GridStyleEnum.Blue;

			_flexD_2.SetDummyColumn("S");

			MainFrameInterface.SetUserGrid(_flexD_2);
			
			// 그리드 헤더캡션 표시하기
			for(int i = 0; i <= _flexD_2.Cols.Count-1; i++)
				_flexD_2[0, i] = GetDDItem(_flexD_2.Cols[i].Name);

			_flexD_2.Redraw = true;
			_flexD_2.AllowResizing = AllowResizingEnum.Columns;
			_flexD_2.StartEdit			+= new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);

		}

		#endregion

		#endregion 
	
		#region -> 품목별 그리드 초기화
		
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
			_flexM_3.Cols.Count = 6;
			_flexM_3.Cols.Fixed = 1;
			_flexM_3.Rows.DefaultSize = 20;	

			_flexM_3.Cols[0].Width = 50;

			_flexM_3.Cols[1].Name = "CHOOSE";
			_flexM_3.Cols[1].DataType = typeof(string);
			_flexM_3.Cols[1].Format = "T;F";
			_flexM_3.Cols[1].ImageAlign = ImageAlignEnum.CenterCenter;
			_flexM_3.Cols[1].Width = 40;
			
			_flexM_3.Cols[2].Name = "CD_ITEM";
			_flexM_3.Cols[2].DataType = typeof(string);
			_flexM_3.Cols[2].Width = 90;
						
			_flexM_3.Cols[3].Name = "NM_ITEM";
			_flexM_3.Cols[3].DataType = typeof(string);
			_flexM_3.Cols[3].Width = 100;
			
			_flexM_3.Cols[4].Name = "STND_ITEM";
			_flexM_3.Cols[4].DataType = typeof(string);
			_flexM_3.Cols[4].Width = 90;
						
			_flexM_3.Cols[5].Name = "UNIT";
			_flexM_3.Cols[5].DataType = typeof(string);
			_flexM_3.Cols[5].Width = 100;
		
			_flexM_3.AllowSorting = AllowSortingEnum.None;
			_flexM_3.NewRowEditable = false;
			_flexM_3.EnterKeyAddRow = false;

			_flexM_3.SumPosition = SumPositionEnum.None;
			_flexM_3.GridStyle = GridStyleEnum.Green;
			
			MainFrameInterface.SetUserGrid(_flexM_3);
			
			// 그리드 헤더캡션 표시하기
			for(int i = 0; i <= _flexM_3.Cols.Count-1; i++)
				_flexM_3[0, i] = GetDDItem(_flexM_3.Cols[i].Name);

			_flexM_3.Redraw = true;	
			_flexM_3.AllowResizing = AllowResizingEnum.Columns;
			_flexM_3.StartEdit			+= new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);	
			_flexM_3.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(_flexM_AfterRowColChange);	
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
			_flexD_3.Cols.Count =15;
			_flexD_3.Cols.Fixed = 1;
			_flexD_3.Rows.DefaultSize = 20;	

			_flexD_3.Cols[0].Width = 50;

			_flexD_3.Cols[1].Name = "NM_RET";
			_flexD_3.Cols[1].DataType = typeof(string);
			_flexD_3.Cols[1].Width = 90;
		
			_flexD_3.Cols[2].Name = "NM_PARTNER";
			_flexD_3.Cols[2].DataType = typeof(string);
			_flexD_3.Cols[2].Width = 90;
						
			_flexD_3.Cols[3].Name = "DT_SO";
			_flexD_3.Cols[3].DataType = typeof(string);
			_flexD_3.Cols[3].Width = 100;
			_flexD_3.Cols[3].EditMask = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT).Replace("#","9");
			_flexD_3.Cols[3].Format = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT);
			_flexD_3.Cols[3].TextAlign = TextAlignEnum.CenterCenter;
			_flexD_3.SetStringFormatCol("DT_SO");

			_flexD_3.Cols[4].Name = "DT_DUEDATE";
			_flexD_3.Cols[4].DataType = typeof(string);
			_flexD_3.Cols[4].Width = 100;
			_flexD_3.Cols[4].EditMask = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT).Replace("#","9");
			_flexD_3.Cols[4].Format = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT);
			_flexD_3.Cols[4].TextAlign = TextAlignEnum.CenterCenter;
			_flexD_3.SetStringFormatCol("DT_DUEDATE");
						
			_flexD_3.Cols[5].Name = "QT_SO";
			_flexD_3.Cols[5].DataType = typeof(decimal);
			_flexD_3.Cols[5].Width = 120;
			_flexD_3.Cols[5].TextAlign = TextAlignEnum.RightCenter;
			_flexD_3.Cols[5].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_3.SetColMaxLength("QT_SO",17);
			
			_flexD_3.Cols[6].Name = "QT_GIR";
			_flexD_3.Cols[6].DataType = typeof(decimal);
			_flexD_3.Cols[6].Width = 120;
			_flexD_3.Cols[6].TextAlign = TextAlignEnum.RightCenter;
			_flexD_3.Cols[6].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_3.SetColMaxLength("QT_GIR",17);

			_flexD_3.Cols[7].Name = "QT_GI";
			_flexD_3.Cols[7].DataType = typeof(decimal);
			_flexD_3.Cols[7].Width = 120;
			_flexD_3.Cols[7].TextAlign = TextAlignEnum.RightCenter;
			_flexD_3.Cols[7].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_3.SetColMaxLength("QT_GI",17);

			_flexD_3.Cols[8].Name = "QT_REMAINS";
			_flexD_3.Cols[8].DataType = typeof(decimal);
			_flexD_3.Cols[8].Width = 120;
			_flexD_3.Cols[8].TextAlign = TextAlignEnum.RightCenter;
			_flexD_3.Cols[8].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_3.SetColMaxLength("QT_REMAINS",17);

			_flexD_3.Cols[9].Name = "QT_RETURN";
			_flexD_3.Cols[9].DataType = typeof(decimal);
			_flexD_3.Cols[9].Width = 120;
			_flexD_3.Cols[9].TextAlign = TextAlignEnum.RightCenter;
			_flexD_3.Cols[9].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_3.SetColMaxLength("QT_RETURN",17);
			
			_flexD_3.Cols[10].Name = "QT_IV";
			_flexD_3.Cols[10].DataType = typeof(decimal);
			_flexD_3.Cols[10].Width = 120;
			_flexD_3.Cols[10].TextAlign = TextAlignEnum.RightCenter;
			_flexD_3.Cols[10].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_3.SetColMaxLength("QT_IV",17);

			_flexD_3.Cols[11].Name = "QT_IVREMAINS";
			_flexD_3.Cols[11].DataType = typeof(decimal);
			_flexD_3.Cols[11].Width = 120;
			_flexD_3.Cols[11].TextAlign = TextAlignEnum.RightCenter;
			_flexD_3.Cols[11].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_3.SetColMaxLength("QT_IVREMAINS",17);
	
			_flexD_3.Cols[12].Name = "NM_STA_SO";
			_flexD_3.Cols[12].DataType = typeof(string);
			_flexD_3.Cols[12].Width = 100;		
				
			_flexD_3.Cols[13].Name = "NM_SO";
			_flexD_3.Cols[13].DataType = typeof(string);
			_flexD_3.Cols[13].Width = 100;		
			
			_flexD_3.Cols[14].Name = "NO_SO";
			_flexD_3.Cols[14].DataType = typeof(string);
			_flexD_3.Cols[14].Width = 100;		
			
			_flexD_3.AllowSorting = AllowSortingEnum.None;
			_flexD_3.NewRowEditable = false;
			_flexD_3.EnterKeyAddRow = false;
			
			_flexD_3.SumPosition = SumPositionEnum.None;
			_flexD_3.GridStyle = GridStyleEnum.Blue;

			_flexD_3.SetDummyColumn("S");

			MainFrameInterface.SetUserGrid(_flexD_3);
			
			// 그리드 헤더캡션 표시하기
			for(int i = 0; i <= _flexD_3.Cols.Count-1; i++)
				_flexD_3[0, i] = GetDDItem(_flexD_3.Cols[i].Name);

			_flexD_3.Redraw = true;
			_flexD_3.AllowResizing = AllowResizingEnum.Columns;
			_flexD_3.StartEdit			+= new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);

		}

		#endregion	
		
		#endregion		
   
		#region -> 납기일별 그리드 초기화
		
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
			_flexM_4.Cols.Count = 3;
			_flexM_4.Cols.Fixed = 1;
			_flexM_4.Rows.DefaultSize = 20;	

			_flexM_4.Cols[0].Width = 50;

			_flexM_4.Cols[1].Name = "CHOOSE";
			_flexM_4.Cols[1].DataType = typeof(string);
			_flexM_4.Cols[1].Format = "Y;N";
			_flexM_4.Cols[1].ImageAlign = ImageAlignEnum.CenterCenter;
			_flexM_4.Cols[1].Width = 40;
			
			_flexM_4.Cols[2].Name = "DT_DUEDATE";
			_flexM_4.Cols[2].DataType = typeof(string);
			_flexM_4.Cols[2].Width = 100;
			_flexM_4.Cols[2].EditMask = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT).Replace("#","9");
			_flexM_4.Cols[2].Format = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT);
			_flexM_4.Cols[2].TextAlign = TextAlignEnum.CenterCenter;
			_flexM_4.SetStringFormatCol("DT_DUEDATE");	
					
			_flexM_4.AllowSorting = AllowSortingEnum.None;
			_flexM_4.NewRowEditable = false;
			_flexM_4.EnterKeyAddRow = false;

			_flexM_4.SumPosition = SumPositionEnum.None;
			_flexM_4.GridStyle = GridStyleEnum.Green;
			
			MainFrameInterface.SetUserGrid(_flexM_4);
			
			// 그리드 헤더캡션 표시하기
			for(int i = 0; i <= _flexM_4.Cols.Count-1; i++)
				_flexM_4[0, i] = GetDDItem(_flexM_4.Cols[i].Name);

			_flexM_4.Redraw = true;	
			_flexM_4.AllowResizing = AllowResizingEnum.Columns;
			_flexM_4.StartEdit			+= new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);	
			_flexM_4.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(_flexM_AfterRowColChange);	
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
			_flexD_4.Cols.Count =17;
			_flexD_4.Cols.Fixed = 1;
			_flexD_4.Rows.DefaultSize = 20;	

			_flexD_4.Cols[0].Width = 50;

			_flexD_4.Cols[1].Name = "NM_RET";
			_flexD_4.Cols[1].DataType = typeof(string);
			_flexD_4.Cols[1].Width = 90;

			_flexD_4.Cols[2].Name = "CD_PARTNER";
			_flexD_4.Cols[2].DataType = typeof(string);
			_flexD_4.Cols[2].Width = 90;

			_flexD_4.Cols[3].Name = "NM_PARTNER";
			_flexD_4.Cols[3].DataType = typeof(string);
			_flexD_4.Cols[3].Width = 90;			

			_flexD_4.Cols[4].Name = "CD_ITEM";
			_flexD_4.Cols[4].DataType = typeof(string);
			_flexD_4.Cols[4].Width = 90;
			
			_flexD_4.Cols[5].Name = "NM_ITEM";
			_flexD_4.Cols[5].DataType = typeof(string);
			_flexD_4.Cols[5].Width = 90;
			
			_flexD_4.Cols[6].Name = "DT_SO";
			_flexD_4.Cols[6].DataType = typeof(string);
			_flexD_4.Cols[6].Width = 100;
			_flexD_4.Cols[6].EditMask = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT).Replace("#","9");
			_flexD_4.Cols[6].Format = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT);
			_flexD_4.Cols[6].TextAlign = TextAlignEnum.CenterCenter;
			_flexD_4.SetStringFormatCol("DT_SO");
		
			_flexD_4.Cols[7].Name = "QT_SO";
			_flexD_4.Cols[7].DataType = typeof(decimal);
			_flexD_4.Cols[7].Width = 120;
			_flexD_4.Cols[7].TextAlign = TextAlignEnum.RightCenter;
			_flexD_4.Cols[7].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_4.SetColMaxLength("QT_SO",17);

			_flexD_4.Cols[8].Name = "QT_GIR";
			_flexD_4.Cols[8].DataType = typeof(decimal);
			_flexD_4.Cols[8].Width = 120;
			_flexD_4.Cols[8].TextAlign = TextAlignEnum.RightCenter;
			_flexD_4.Cols[8].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_4.SetColMaxLength("QT_GIR",17);

			_flexD_4.Cols[9].Name = "QT_GI";
			_flexD_4.Cols[9].DataType = typeof(decimal);
			_flexD_4.Cols[9].Width = 120;
			_flexD_4.Cols[9].TextAlign = TextAlignEnum.RightCenter;
			_flexD_4.Cols[9].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_4.SetColMaxLength("QT_GI",17);

			_flexD_4.Cols[10].Name = "QT_REMAINS";
			_flexD_4.Cols[10].DataType = typeof(decimal);
			_flexD_4.Cols[10].Width = 120;
			_flexD_4.Cols[10].TextAlign = TextAlignEnum.RightCenter;
			_flexD_4.Cols[10].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_4.SetColMaxLength("QT_REMAINS",17);

			_flexD_4.Cols[11].Name = "QT_RETURN";
			_flexD_4.Cols[11].DataType = typeof(decimal);
			_flexD_4.Cols[11].Width = 120;
			_flexD_4.Cols[11].TextAlign = TextAlignEnum.RightCenter;
			_flexD_4.Cols[11].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_4.SetColMaxLength("QT_RETURN",17);

			_flexD_4.Cols[12].Name = "QT_IV";
			_flexD_4.Cols[12].DataType = typeof(decimal);
			_flexD_4.Cols[12].Width = 120;
			_flexD_4.Cols[12].TextAlign = TextAlignEnum.RightCenter;
			_flexD_4.Cols[12].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_4.SetColMaxLength("QT_IV",17);

			_flexD_4.Cols[13].Name = "QT_IVREMAINS";
			_flexD_4.Cols[13].DataType = typeof(decimal);
			_flexD_4.Cols[13].Width = 120;
			_flexD_4.Cols[13].TextAlign = TextAlignEnum.RightCenter;
			_flexD_4.Cols[13].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_4.SetColMaxLength("QT_IVREMAINS",17);

			_flexD_4.Cols[14].Name = "NM_STA_SO";
			_flexD_4.Cols[14].DataType = typeof(string);
			_flexD_4.Cols[14].Width = 100;		
			
			_flexD_4.Cols[15].Name = "NM_SO";
			_flexD_4.Cols[15].DataType = typeof(string);
			_flexD_4.Cols[15].Width = 100;		
		
			_flexD_4.Cols[16].Name = "NO_SO";
			_flexD_4.Cols[16].DataType = typeof(string);
			_flexD_4.Cols[16].Width = 100;		
						
			_flexD_4.AllowSorting = AllowSortingEnum.None;
			_flexD_4.NewRowEditable = false;
			_flexD_4.EnterKeyAddRow = false;
			
			_flexD_4.SumPosition = SumPositionEnum.None;
			_flexD_4.GridStyle = GridStyleEnum.Blue;

			_flexD_4.SetDummyColumn("S");

			MainFrameInterface.SetUserGrid(_flexD_4);
			
			// 그리드 헤더캡션 표시하기
			for(int i = 0; i <= _flexD_4.Cols.Count-1; i++)
				_flexD_4[0, i] = GetDDItem(_flexD_4.Cols[i].Name);

			_flexD_4.Redraw = true;
			_flexD_4.AllowResizing = AllowResizingEnum.Columns;
			_flexD_4.StartEdit			+= new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);
			_flexD_4.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(_flexD_AfterRowColChange);	
			
		}

		#endregion
		
		#endregion

		#region -> 수주형태별 그리드 초기화
		
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
			_flexM_5.Cols.Count = 4;
			_flexM_5.Cols.Fixed = 1;
			_flexM_5.Rows.DefaultSize = 20;	

			_flexM_5.Cols[0].Width = 50;

			_flexM_5.Cols[1].Name = "CHOOSE";
			_flexM_5.Cols[1].DataType = typeof(string);
			_flexM_5.Cols[1].Format = "Y;N";
			_flexM_5.Cols[1].ImageAlign = ImageAlignEnum.CenterCenter;
			_flexM_5.Cols[1].Width = 40;
			
			_flexM_5.Cols[2].Name = "TP_SO";
			_flexM_5.Cols[2].DataType = typeof(string);
			_flexM_5.Cols[2].Width = 90;
			
			_flexM_5.Cols[3].Name = "NM_SO";
			_flexM_5.Cols[3].DataType = typeof(string);
			_flexM_5.Cols[3].Width = 100;
			
			_flexM_5.AllowSorting = AllowSortingEnum.None;
			_flexM_5.NewRowEditable = false;
			_flexM_5.EnterKeyAddRow = false;

			_flexM_5.SumPosition = SumPositionEnum.None;
			_flexM_5.GridStyle = GridStyleEnum.Green;
			
			MainFrameInterface.SetUserGrid(_flexM_5);
			
			// 그리드 헤더캡션 표시하기
			for(int i = 0; i <= _flexM_5.Cols.Count-1; i++)
				_flexM_5[0, i] = GetDDItem(_flexM_5.Cols[i].Name);

			_flexM_5.Redraw = true;		
			_flexM_5.AllowResizing = AllowResizingEnum.Columns;
			_flexM_5.StartEdit			+= new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);
			_flexM_5.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(_flexM_AfterRowColChange);	
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
			_flexD_5.Cols.Count =15;
			_flexD_5.Cols.Fixed = 1;
			_flexD_5.Rows.DefaultSize = 20;	

			_flexD_5.Cols[0].Width = 50;

			_flexD_5.Cols[1].Name = "NM_RET";
			_flexD_5.Cols[1].DataType = typeof(string);
			_flexD_5.Cols[1].Width = 90;
			
			_flexD_5.Cols[2].Name = "CD_ITEM";
			_flexD_5.Cols[2].DataType = typeof(string);
			_flexD_5.Cols[2].Width = 90;
			
			_flexD_5.Cols[3].Name = "NM_ITEM";
			_flexD_5.Cols[3].DataType = typeof(string);
			_flexD_5.Cols[3].Width = 90;
			
			_flexD_5.Cols[4].Name = "DT_SO";
			_flexD_5.Cols[4].DataType = typeof(string);
			_flexD_5.Cols[4].Width = 100;
			_flexD_5.Cols[4].EditMask = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT).Replace("#","9");
			_flexD_5.Cols[4].Format = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT);
			_flexD_5.Cols[4].TextAlign = TextAlignEnum.CenterCenter;
			_flexD_5.SetStringFormatCol("DT_SO");

			_flexD_5.Cols[5].Name = "DT_DUEDATE";
			_flexD_5.Cols[5].DataType = typeof(string);
			_flexD_5.Cols[5].Width = 100;
			_flexD_5.Cols[5].EditMask = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT).Replace("#","9");
			_flexD_5.Cols[5].Format = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT);
			_flexD_5.Cols[5].TextAlign = TextAlignEnum.CenterCenter;
			_flexD_5.SetStringFormatCol("DT_DUEDATE");
									
			_flexD_5.Cols[6].Name = "QT_SO";
			_flexD_5.Cols[6].DataType = typeof(decimal);
			_flexD_5.Cols[6].Width = 120;
			_flexD_5.Cols[6].TextAlign = TextAlignEnum.RightCenter;
			_flexD_5.Cols[6].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_5.SetColMaxLength("QT_SO",17);

			_flexD_5.Cols[7].Name = "QT_GIR";
			_flexD_5.Cols[7].DataType = typeof(decimal);
			_flexD_5.Cols[7].Width = 120;
			_flexD_5.Cols[7].TextAlign = TextAlignEnum.RightCenter;
			_flexD_5.Cols[7].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_5.SetColMaxLength("QT_GIR",17);

			_flexD_5.Cols[8].Name = "QT_GI";
			_flexD_5.Cols[8].DataType = typeof(decimal);
			_flexD_5.Cols[8].Width = 120;
			_flexD_5.Cols[8].TextAlign = TextAlignEnum.RightCenter;
			_flexD_5.Cols[8].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_5.SetColMaxLength("QT_GI",17);

			_flexD_5.Cols[9].Name = "QT_REMAINS";
			_flexD_5.Cols[9].DataType = typeof(decimal);
			_flexD_5.Cols[9].Width = 120;
			_flexD_5.Cols[9].TextAlign = TextAlignEnum.RightCenter;
			_flexD_5.Cols[9].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_5.SetColMaxLength("QT_REMAINS",17);

			_flexD_5.Cols[10].Name = "QT_RETURN";
			_flexD_5.Cols[10].DataType = typeof(decimal);
			_flexD_5.Cols[10].Width = 120;
			_flexD_5.Cols[10].TextAlign = TextAlignEnum.RightCenter;
			_flexD_5.Cols[10].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_5.SetColMaxLength("QT_RETURN",17);

			_flexD_5.Cols[11].Name = "QT_IV";
			_flexD_5.Cols[11].DataType = typeof(decimal);
			_flexD_5.Cols[11].Width = 120;
			_flexD_5.Cols[11].TextAlign = TextAlignEnum.RightCenter;
			_flexD_5.Cols[11].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_5.SetColMaxLength("QT_IV",17);

			_flexD_5.Cols[12].Name = "QT_IVREMAINS";
			_flexD_5.Cols[12].DataType = typeof(decimal);
			_flexD_5.Cols[12].Width = 120;
			_flexD_5.Cols[12].TextAlign = TextAlignEnum.RightCenter;
			_flexD_5.Cols[12].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_5.SetColMaxLength("QT_IVREMAINS",17);

			_flexD_5.Cols[13].Name = "NM_STA_SO";
			_flexD_5.Cols[13].DataType = typeof(string);
			_flexD_5.Cols[13].Width = 100;		
						
			_flexD_5.Cols[14].Name = "NO_SO";
			_flexD_5.Cols[14].DataType = typeof(string);
			_flexD_5.Cols[14].Width = 100;		
			
			_flexD_5.AllowSorting = AllowSortingEnum.None;
			_flexD_5.NewRowEditable = false;
			_flexD_5.EnterKeyAddRow = false;
			
			_flexD_5.SumPosition = SumPositionEnum.None;
			_flexD_5.GridStyle = GridStyleEnum.Blue;

			_flexD_5.SetDummyColumn("S");

			MainFrameInterface.SetUserGrid(_flexD_5);
			
			// 그리드 헤더캡션 표시하기
			for(int i = 0; i <= _flexD_5.Cols.Count-1; i++)
				_flexD_5[0, i] = GetDDItem(_flexD_5.Cols[i].Name);

			_flexD_5.Redraw = true;
			_flexD_5.AllowResizing = AllowResizingEnum.Columns;
			_flexD_5.StartEdit			+= new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);
			_flexD_4.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(_flexD_AfterRowColChange);
		}

		#endregion

		#endregion

		#region -> 영업그룹별 그리드 초기화
		
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
			_flexM_6.Cols.Count = 3;
			_flexM_6.Cols.Fixed = 1;
			_flexM_6.Rows.DefaultSize = 20;	

			_flexM_6.Cols[0].Width = 50;

			_flexM_6.Cols[1].Name = "CHOOSE";
			_flexM_6.Cols[1].DataType = typeof(string);
			_flexM_6.Cols[1].Format = "Y;N";
			_flexM_6.Cols[1].ImageAlign = ImageAlignEnum.CenterCenter;
			_flexM_6.Cols[1].Width = 40;
					
			_flexM_6.Cols[2].Name = "NM_SALEGRP";
			_flexM_6.Cols[2].DataType = typeof(string);
			_flexM_6.Cols[2].Width = 100;
			
			_flexM_6.AllowSorting = AllowSortingEnum.None;
			_flexM_6.NewRowEditable = false;
			_flexM_6.EnterKeyAddRow = false;

			_flexM_6.SumPosition = SumPositionEnum.None;
			_flexM_6.GridStyle = GridStyleEnum.Green;
			
			MainFrameInterface.SetUserGrid(_flexM_6);
			
			// 그리드 헤더캡션 표시하기
			for(int i = 0; i <= _flexM_6.Cols.Count-1; i++)
				_flexM_6[0, i] = GetDDItem(_flexM_6.Cols[i].Name);

			_flexM_6.Redraw = true;	
			_flexM_6.AllowResizing = AllowResizingEnum.Columns;
			_flexM_6.StartEdit			+= new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);
			_flexM_6.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(_flexM_AfterRowColChange);	
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
			_flexD_6.Cols.Count =16;
			_flexD_6.Cols.Fixed = 1;
			_flexD_6.Rows.DefaultSize = 20;	

			_flexD_6.Cols[0].Width = 50;

			_flexD_6.Cols[1].Name = "NM_RET";
			_flexD_6.Cols[1].DataType = typeof(string);
			_flexD_6.Cols[1].Width = 90;
			
			_flexD_6.Cols[2].Name = "CD_ITEM";
			_flexD_6.Cols[2].DataType = typeof(string);
			_flexD_6.Cols[2].Width = 90;
			
			_flexD_6.Cols[3].Name = "NM_ITEM";
			_flexD_6.Cols[3].DataType = typeof(string);
			_flexD_6.Cols[3].Width = 90;
			
			_flexD_6.Cols[4].Name = "DT_SO";
			_flexD_6.Cols[4].DataType = typeof(string);
			_flexD_6.Cols[4].Width = 100;
			_flexD_6.Cols[4].EditMask = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT).Replace("#","9");
			_flexD_6.Cols[4].Format = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT);
			_flexD_6.Cols[4].TextAlign = TextAlignEnum.CenterCenter;
			_flexD_6.SetStringFormatCol("DT_SO");

			_flexD_6.Cols[5].Name = "DT_DUEDATE";
			_flexD_6.Cols[5].DataType = typeof(string);
			_flexD_6.Cols[5].Width = 100;
			_flexD_6.Cols[5].EditMask = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT).Replace("#","9");
			_flexD_6.Cols[5].Format = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY,FormatFgType.SELECT);
			_flexD_6.Cols[5].TextAlign = TextAlignEnum.CenterCenter;
			_flexD_6.SetStringFormatCol("DT_DUEDATE");
									
			_flexD_6.Cols[6].Name = "QT_SO";
			_flexD_6.Cols[6].DataType = typeof(decimal);
			_flexD_6.Cols[6].Width = 120;
			_flexD_6.Cols[6].TextAlign = TextAlignEnum.RightCenter;
			_flexD_6.Cols[6].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_6.SetColMaxLength("QT_SO",17);

			_flexD_6.Cols[7].Name = "QT_GIR";
			_flexD_6.Cols[7].DataType = typeof(decimal);
			_flexD_6.Cols[7].Width = 120;
			_flexD_6.Cols[7].TextAlign = TextAlignEnum.RightCenter;
			_flexD_6.Cols[7].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_6.SetColMaxLength("QT_GIR",17);

			_flexD_6.Cols[8].Name = "QT_GI";
			_flexD_6.Cols[8].DataType = typeof(decimal);
			_flexD_6.Cols[8].Width = 120;
			_flexD_6.Cols[8].TextAlign = TextAlignEnum.RightCenter;
			_flexD_6.Cols[8].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_6.SetColMaxLength("QT_GI",17);

			_flexD_6.Cols[9].Name = "QT_REMAINS";
			_flexD_6.Cols[9].DataType = typeof(decimal);
			_flexD_6.Cols[9].Width = 120;
			_flexD_6.Cols[9].TextAlign = TextAlignEnum.RightCenter;
			_flexD_6.Cols[9].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_6.SetColMaxLength("QT_REMAINS",17);

			_flexD_6.Cols[10].Name = "QT_RETURN";
			_flexD_6.Cols[10].DataType = typeof(decimal);
			_flexD_6.Cols[10].Width = 120;
			_flexD_6.Cols[10].TextAlign = TextAlignEnum.RightCenter;
			_flexD_6.Cols[10].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_6.SetColMaxLength("QT_RETURN",17);

			_flexD_6.Cols[11].Name = "QT_IV";
			_flexD_6.Cols[11].DataType = typeof(decimal);
			_flexD_6.Cols[11].Width = 120;
			_flexD_6.Cols[11].TextAlign = TextAlignEnum.RightCenter;
			_flexD_6.Cols[11].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_6.SetColMaxLength("QT_IV",17);

			_flexD_6.Cols[12].Name = "QT_IVREMAINS";
			_flexD_6.Cols[12].DataType = typeof(decimal);
			_flexD_6.Cols[12].Width = 120;
			_flexD_6.Cols[12].TextAlign = TextAlignEnum.RightCenter;
			_flexD_6.Cols[12].Format =this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY,FormatFgType.SELECT);
			_flexD_6.SetColMaxLength("QT_IVREMAINS",17);

			_flexD_6.Cols[13].Name = "NM_STA_SO";
			_flexD_6.Cols[13].DataType = typeof(string);
			_flexD_6.Cols[13].Width = 100;		
			
			_flexD_6.Cols[14].Name = "NM_SO";
			_flexD_6.Cols[14].DataType = typeof(string);
			_flexD_6.Cols[14].Width = 100;		
			
			_flexD_6.Cols[15].Name = "NO_SO";
			_flexD_6.Cols[15].DataType = typeof(string);
			_flexD_6.Cols[15].Width = 100;		
						
			_flexD_6.AllowSorting = AllowSortingEnum.None;
			_flexD_6.NewRowEditable = false;
			_flexD_6.EnterKeyAddRow = false;
			
			_flexD_6.SumPosition = SumPositionEnum.None;
			_flexD_6.GridStyle = GridStyleEnum.Blue;

			_flexD_6.SetDummyColumn("S");

			MainFrameInterface.SetUserGrid(_flexD_6);
			
			// 그리드 헤더캡션 표시하기
			for(int i = 0; i <= _flexD_6.Cols.Count-1; i++)
				_flexD_6[0, i] = GetDDItem(_flexD_6.Cols[i].Name);

			_flexD_6.Redraw = true;
			_flexD_6.AllowResizing = AllowResizingEnum.Columns;
			_flexD_6.StartEdit			+= new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);
			_flexD_4.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(_flexD_AfterRowColChange);
		}

		#endregion
		
		#endregion 

		#region -> Page_Paint
		/// <summary>
		/// 조회조건 ComboBox 조회
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Page_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{			
			try	//반드시 예외처리 할것!
			{	
				if(this._isPainted)
					return;
					
				this._isPainted = true;
				Application.DoEvents();
					
				

				// 콤보박스 초기화
				InitCombo();
					    				
				// 수주일자
				DateTime dateEnd = MainFrameInterface.GetDateTimeToday();
				DateTime dateStart = dateEnd.AddMonths( -1 );

				m_txtDT_SO1.Text = dateStart.ToShortDateString();				
				m_txtDT_SO2.Text = dateEnd.ToShortDateString();	

				this.Enabled = true; //페이지 전체 활성				
				
			}
			catch(Exception ex)
			{
				// 작업을 정상적으로 처리하지 못했습니다.
				ShowMessage("MA_M000011");
			}			
		}

		
		#endregion

		#endregion		
		
		#region ♣ 메인버튼 이벤트
		/// <summary>
		/// 조회
		/// </summary>
		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			//검색 조건 항목 체크
			if(FieldCheck() == false)
				return;

			if(m_dsSet.Tables.Count > 0)
				m_dsSet.Clear();

			object[] args = {	LoginInfo.CompanyCode, 
								m_txtDT_SO1.Text, 
								m_txtDT_SO2.Text,
								m_cboCD_BIZAREA.SelectedValue.ToString(), 
								m_cboSlUnit.SelectedValue.ToString(), 
								m_cboFgTrans.SelectedValue.ToString(), 
								bpSalegrp.CodeValue, 
								m_cboStaSo.SelectedValue.ToString()};

			try
		    {
			    ResultData result = (ResultData)this.FillDataSet("SP_SA_SOSCH2_SELECT", args);
			    m_dsSet = (DataSet)result.DataValue;	
			
			//미결현황 - 선택된 Tab
				switch(m_tabTAB_SO.SelectedIndex)
				{
					case 0 :	//수주번호별 조회
					
						// Detail 바인딩
						_flexD_1.Redraw=false;
						_flexD_1.BindingStart();
						_flexD_1.DataSource = m_dsSet.Tables[1].DefaultView;				
						_flexD_1.BindingEnd();			
						_flexD_1.EmptyRowFilter();								// 처음에 아무것도 안 보이게
						
						// Master 바인딩
						_flexM_1.Redraw=false;
						_flexM_1.BindingStart();
						_flexM_1.DataSource = m_dsSet.Tables[0].DefaultView;	// 요기에서 곧바로 AfterRowColChange 이벤트 발생
						_flexM_1.BindingEnd();
								
						_flexD_1.Redraw=true;
						_flexM_1.Redraw=true;
						
						gdv_LineNO_SO = new DataView(m_dsSet.Tables[1]);
						if(m_dsSet.Tables[0].Rows.Count>0)						
						DataBinding0(1);

						break;

					case 1 :	//거래처별 조회
					
						// Detail 바인딩
						_flexD_2.Redraw=false;
						_flexD_2.BindingStart();
						_flexD_2.DataSource = m_dsSet.Tables[3].DefaultView;				
						_flexD_2.BindingEnd();				
						_flexD_2.EmptyRowFilter();								// 처음에 아무것도 안 보이게

						// Master 바인딩
						_flexM_2.Redraw=false;
						_flexM_2.BindingStart();
						_flexM_2.DataSource = m_dsSet.Tables[2].DefaultView;	// 요기에서 곧바로 AfterRowColChange 이벤트 발생
						_flexM_2.BindingEnd();
								
						_flexD_2.Redraw=true;
						_flexM_2.Redraw=true;
						
						gdv_LineCD_PARTNER = new DataView( m_dsSet.Tables[3]);
						if(m_dsSet.Tables[2].Rows.Count>0)	
						DataBinding1(1);

						break;

					case 2 :	//품목코드별 조회
					
						// Detail 바인딩
						_flexD_3.Redraw=false;
						_flexD_3.BindingStart();
						_flexD_3.DataSource = m_dsSet.Tables[5].DefaultView;				
						_flexD_3.BindingEnd();				
						_flexD_3.EmptyRowFilter();								// 처음에 아무것도 안 보이게

						// Master 바인딩
						_flexM_3.Redraw=false;
						_flexM_3.BindingStart();
						_flexM_3.DataSource = m_dsSet.Tables[4].DefaultView;	// 요기에서 곧바로 AfterRowColChange 이벤트 발생
						_flexM_3.BindingEnd();
								
						_flexD_3.Redraw=true;
						_flexM_3.Redraw=true;
						
						gdv_LineCD_ITEM = new DataView( m_dsSet.Tables[5]);
						if(m_dsSet.Tables[4].Rows.Count>0)	
						DataBinding2(1);

						break;

					case 3 :	//납기일별 조회
					
						// Detail 바인딩
						_flexD_4.Redraw=false;
						_flexD_4.BindingStart();
						_flexD_4.DataSource = m_dsSet.Tables[7].DefaultView;				
						_flexD_4.BindingEnd();				
						_flexD_4.EmptyRowFilter();								// 처음에 아무것도 안 보이게

						// Master 바인딩
						_flexM_4.Redraw=false;
						_flexM_4.BindingStart();
						_flexM_4.DataSource = m_dsSet.Tables[6].DefaultView;	// 요기에서 곧바로 AfterRowColChange 이벤트 발생
						_flexM_4.BindingEnd();
								
						_flexD_4.Redraw=true;
						_flexM_4.Redraw=true;
						
						gdv_LineDT_DUEDATE = new DataView( m_dsSet.Tables[7]);
						if(m_dsSet.Tables[6].Rows.Count>0)	
						DataBinding3(1);

						break;

					case 4 :	//수주형태별 조회
					
						// Detail 바인딩
						_flexD_5.Redraw=false;
						_flexD_5.BindingStart();
						_flexD_5.DataSource = m_dsSet.Tables[9].DefaultView;				
						_flexD_5.BindingEnd();				
						_flexD_5.EmptyRowFilter();								// 처음에 아무것도 안 보이게

						// Master 바인딩
						_flexM_5.Redraw=false;
						_flexM_5.BindingStart();
						_flexM_5.DataSource = m_dsSet.Tables[8].DefaultView;	// 요기에서 곧바로 AfterRowColChange 이벤트 발생
						_flexM_5.BindingEnd();
								
						_flexD_5.Redraw=true;
						_flexM_5.Redraw=true;
						
						gdv_LineTP_SO = new DataView( m_dsSet.Tables[9]);
						if(m_dsSet.Tables[8].Rows.Count>0)	
						DataBinding4(1);
				
						break;

					case 5 :	//영업그룹별 조회
					
						// Detail 바인딩
						_flexD_6.Redraw=false;
						_flexD_6.BindingStart();
						_flexD_6.DataSource = m_dsSet.Tables[11].DefaultView;				
						_flexD_6.BindingEnd();				
						_flexD_6.EmptyRowFilter();								// 처음에 아무것도 안 보이게

						// Master 바인딩
						_flexM_6.Redraw=false;
						_flexM_6.BindingStart();
						_flexM_6.DataSource = m_dsSet.Tables[10].DefaultView;	// 요기에서 곧바로 AfterRowColChange 이벤트 발생
						_flexM_6.BindingEnd();
								
						_flexD_6.Redraw=true;
						_flexM_6.Redraw=true;
						
						gdv_LineCD_GROUP = new DataView( m_dsSet.Tables[11]);
						if(m_dsSet.Tables[10].Rows.Count>0)	
						DataBinding5(1);
					
						break;
				}
					Cursor.Current = Cursors.Default;

					//검색된 내용이 존재하지 않습니다.
					if (m_dsSet.Tables[0].Rows.Count == 0)
						ShowMessage("SA_M000025");//MessageBoxEx.Show(GetMessageDictionaryItem("SA_M000025"), PageName, MessageBoxButtons.OK,MessageBoxIcon.Information);
			}
			catch(Exception ex)
			{
				MsgEnd(ex);
				
			}
		}

		/// <summary>
		/// 인쇄
		/// </summary>
		public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
		{
			try
			{
			}
			catch(System.Exception ex)
			{
				ShowErrorMessage(ex, PageName);
			}
		}

		/// <summary>
		/// 종료
		/// </summary>
		public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
		{
			return true;
		}
		#endregion

		#region ♣ 그리드 이벤트

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
					{
						DataBinding3(e.NewRange.r1);	
						DataBinding_ITEM_1(1);
					}
					else if(((Dass.FlexGrid.FlexGrid)sender).Name =="_flexM_5")
					{
						DataBinding4(e.NewRange.r1);
						DataBinding_ITEM_2(1);
					}
					else if(((Dass.FlexGrid.FlexGrid)sender).Name =="_flexM_6")
					{
						DataBinding5(e.NewRange.r1);
						DataBinding_ITEM_3(1);
					}
				}					
			}
			catch
			{
			}		
		}
						
		#endregion	

		#region -> _flexD_AfterRowColChange

		private void _flexD_AfterRowColChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
		{
			try
			{
				if(e.OldRange.r1!=e.NewRange.r1)
				{	
					if(((Dass.FlexGrid.FlexGrid)sender).Name =="_flexD_4")
						DataBinding_ITEM_1(e.NewRange.r1);				
					else if(((Dass.FlexGrid.FlexGrid)sender).Name =="_flexD_5")
						DataBinding_ITEM_2(e.NewRange.r1);					
					else if(((Dass.FlexGrid.FlexGrid)sender).Name =="_flexD_6")
						DataBinding_ITEM_3(e.NewRange.r1);
				}					
			}
			catch
			{
			}		
		}
						
		#endregion	

		#region -> _flex_StartEdit
		private void _flex_StartEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
		{
			try
			{
				Dass.FlexGrid.FlexGrid flex = (Dass.FlexGrid.FlexGrid)sender;

				if( flex.Cols[e.Col].Name != "CHOOSE")
				{
					e.Cancel = true;	// 셀 입력상태로 못 들어가게

				}				
			}
			finally
			{
			}			
		}
		#endregion

		private void DataBinding_ITEM_1(int pi_rowindex)
		{
			try
			{
				Cursor.Current = Cursors.WaitCursor;
			
				m_txtCdItem2.Text = _flexD_4[pi_rowindex,"NM_ITEM"].ToString();
				m_txtStndItem2.Text = _flexD_4[pi_rowindex,"STND_ITEM"].ToString();
				m_txtUnit2.Text = _flexD_4[pi_rowindex,"UNIT"].ToString();

			}
			catch//(Exception ex)
			{
				//m_grdLine2.DataSource =new DataView(gdt_Empy);		
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}


		private void DataBinding_ITEM_2(int pi_rowindex)
		{
			try
			{
				Cursor.Current = Cursors.WaitCursor;
			
				m_txtCdItem2.Text = _flexD_5[pi_rowindex,"NM_ITEM"].ToString();
				m_txtStndItem2.Text = _flexD_5[pi_rowindex,"STND_ITEM"].ToString();
				m_txtUnit2.Text = _flexD_5[pi_rowindex,"UNIT"].ToString();

			}
			catch//(Exception ex)
			{
				//m_grdLine2.DataSource =new DataView(gdt_Empy);		
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		private void DataBinding_ITEM_3(int pi_rowindex)
		{
			try
			{
				Cursor.Current = Cursors.WaitCursor;
			
				m_txtCdItem2.Text = _flexD_6[pi_rowindex,"NM_ITEM"].ToString();
				m_txtStndItem2.Text = _flexD_6[pi_rowindex,"STND_ITEM"].ToString();
				m_txtUnit2.Text = _flexD_6[pi_rowindex,"UNIT"].ToString();

			}
			catch//(Exception ex)
			{
				//m_grdLine2.DataSource =new DataView(gdt_Empy);		
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		#endregion	

		#region 필터 부분

		private void DataBinding0(int pi_rowindex)
		{
			try
			{
                if (!_flexM_1.IsBindingEnd || !_flexM_1.HasNormalRow) return;

                string ls_NO_SO = _flexM_1[pi_rowindex,"NO_SO"].ToString();

				string ls_filter="";				
				ls_filter = "NO_SO = '" +ls_NO_SO + "'";

                _flexD_1.DataView.RowFilter = ls_filter;
                _flexM_1.DetailQueryNeed = false;		
				
				// 아래전체 합계를 위해서 한 코딩
				//				_flexD_1.SubtotalPosition = SubtotalPositionEnum.BelowData;
				//				_flexD_1.SelectionMode=SelectionModeEnum.Cell;
		
				//				string graCaption = this.GetDataDictionaryItem("MA","GRAND");
				//				
				//				CellStyle s = _flexD_1.Styles[CellStyleEnum.Subtotal0];
				//				s.BackColor = Color.FromArgb(179,217,255);		
				//				s.ForeColor = Color.Black;
				//				s.Name = graCaption;
				//				
				//				s.Font = new Font(_flexD_1.Font, FontStyle.Bold);
				//								
				//				_flexD_1.Subtotal(AggregateEnum.Clear);//MA, GRAND
				//				_flexD_1.Subtotal(AggregateEnum.Sum,0,-1,_flexD_1.Cols["AM"].Index);	
				//				_flexD_1.Subtotal(AggregateEnum.Sum,0,-1,_flexD_1.Cols["AM_WON"].Index);
				
			}
			catch//(Exception ex)
			{
				//MessageBox.Show(ex.ToString());		
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}
		

		private void DataBinding1(int pi_rowindex)
		{
			try
			{
                if (!_flexM_2.IsBindingEnd || !_flexM_2.HasNormalRow) return;

				string ls_CD_PARTNER = _flexM_2[pi_rowindex,"CD_PARTNER"].ToString();
				
				string ls_filter="";	
				
				ls_filter = "CD_PARTNER = '" +ls_CD_PARTNER + "'";				
							
				_flexD_2.DataView.RowFilter = ls_filter;
                _flexM_2.DetailQueryNeed = false;

			}
			catch//(Exception ex)
			{
				//m_grdLine2.DataSource =new DataView(gdt_Empy);		
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		private void DataBinding2(int pi_rowindex)
		{
			try
			{
				Cursor.Current = Cursors.WaitCursor;
							
				string ls_CD_ITEM = _flexM_3[pi_rowindex,"CD_ITEM"].ToString();

				string ls_filter="";				
				ls_filter = "CD_ITEM = '" +ls_CD_ITEM + "'";	
				
				_flexD_3.DataView.RowFilter = ls_filter;

			}
			catch//(Exception ex)
			{
				//m_grdLine1.DataSource =new DataView(gdt_Empy);		
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}
	
		private void DataBinding3(int pi_rowindex)
		{
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				
				string ls_DT = _flexM_4[pi_rowindex,"DT_DUEDATE"].ToString();

				string ls_filter="";				
				ls_filter = "DT_DUEDATE = '" +ls_DT + "'";	

				_flexD_4.DataView.RowFilter = ls_filter;	
			}
			catch//(Exception ex)
			{
				//m_grdLine4.DataSource =new DataView(gdt_Empy);	
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}
	
			

		private void DataBinding4(int pi_rowindex)
		{
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				
				string ls_CD_QTIOTP = _flexM_5[pi_rowindex,"TP_SO"].ToString();
				
				string ls_filter="";				
				ls_filter = "TP_SO = '" +ls_CD_QTIOTP + "'";
					
				_flexD_5.DataView.RowFilter = ls_filter;

			}
			catch//(Exception ex)
			{
				//m_grdLine5.DataSource =new DataView(gdt_Empy);		
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		private void DataBinding5(int pi_rowindex)
		{
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				
				string ls_CD_SALEGRP = _flexM_6[pi_rowindex,"CD_SALEGRP"].ToString();
				

				string ls_filter="";				
				ls_filter = "CD_SALEGRP = '" +ls_CD_SALEGRP + "'";
					
				_flexD_6.DataView.RowFilter = ls_filter;			
	
			}
			catch//(Exception ex)
			{
				//m_grdLine3.DataSource =new DataView(gdt_Empy);			
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}
		



		#endregion

		#region ▶ 도움창 버튼 이벤트
		/// <summary>
		/// 거래처 도움창 클릭 이벤트
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">e</param>
		private void OnHelp_Click(object sender, System.EventArgs e)
		{
			ShowHelp(sender, "");
		}

		/// <summary>
		/// 각 항목에 맞는 도움창 띄우기
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">검색조건</param>
		private void ShowHelp(object sender, string ps_search)
		{
			Cursor.Current = Cursors.WaitCursor;

			switch(((Control)sender).Name)
			{
				//수주일자
				case "m_btnDT_SO":
				case "m_txtDT_SO1":
				case "m_txtDT_SO2":
					object dlg_Carlender = LoadHelpWindow("P_PR_CALENDAR2", new object[] {MainFrameInterface, m_txtDT_SO1.Text, m_txtDT_SO2.Text});

					if(((Duzon.Common.Forms.CommonDialog)dlg_Carlender).ShowDialog() == DialogResult.OK)
					{
						if(dlg_Carlender is IHelpWindow)
						{
							if(((object[])((IHelpWindow)dlg_Carlender).ReturnValues)[0].ToString().Trim() != "")
								m_txtDT_SO1.Text = ((object[])((IHelpWindow)dlg_Carlender).ReturnValues)[0].ToString();

							if(((object[])((IHelpWindow)dlg_Carlender).ReturnValues)[1].ToString().Trim() != "")
								m_txtDT_SO2.Text = ((object[])((IHelpWindow)dlg_Carlender).ReturnValues)[1].ToString();

							m_cboCD_BIZAREA.Focus();
						}
					}
					break;

				//영업그룹
				case "m_btnCD_SALEGRP" :
				case "bpSalegrp" :
					object dlg_MA_SALEGRP_SUB = LoadHelpWindow("P_MA_SALEGRP_SUB", new object[] {MainFrameInterface, ps_search});

					if(((Duzon.Common.Forms.BaseSearchHelp)dlg_MA_SALEGRP_SUB).ShowDialog() == DialogResult.OK)
					{
						if(dlg_MA_SALEGRP_SUB is IHelpWindow)
						{								
							bpSalegrp.CodeName = (((IHelpWindow)dlg_MA_SALEGRP_SUB).ReturnValues)[2].ToString();
							bpSalegrp.CodeValue = (((IHelpWindow)dlg_MA_SALEGRP_SUB).ReturnValues)[1].ToString();
//							bpSalegrp.IsConfirmed = true;
							m_cboStaSo.Focus();
						}
					}		
					break;
			}

			Cursor.Current = Cursors.Default;
		}
		#endregion

		#region ▶ 필수 항목 체크
		/// <summary>
		/// 조회항목 체크 함수
		/// </summary>
		private bool FieldCheck()
		{
			//수주일자
			if((m_txtDT_SO1.Text.Trim() == "") && (m_txtDT_SO2.Text.Trim() == ""))
			{
				//수주일자 은(는) 필수 입력입니다.
				MessageBoxEx.Show(m_lblDT_SO.Text + GetMessageDictionaryItem("SA_M000009"), PageName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				m_txtDT_SO1.Focus();
				return false;
			}

			//수주일자
			if(((m_txtDT_SO1.Text.Trim() != "") && (m_txtDT_SO2.Text.Trim() == "")) ||
				((m_txtDT_SO1.Text.Trim() == "") && (m_txtDT_SO2.Text.Trim() != "")))
			{
				//수주일자 은(는) 필수 입력입니다.
				MessageBoxEx.Show(m_lblDT_SO.Text + GetMessageDictionaryItem("SA_M000009"), PageName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			return true;
		}
		#endregion

		#region ▶ Control Event 정의
		/// <summary>
		/// 도움창을 갖고 있는 Control 유효성검사
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">e</param>
		private void OnEvent_Validated(object sender, System.EventArgs e)
		{
			switch(((TextBox)sender).Name)
			{
					//영업그룹
				case "bpSalegrp":
//					if(bpSalegrp.IsConfirmed == false)
//					{
//						if(bpSalegrp.CodeValue != "")
//						{
//							object[] ls_args = new object[2]{LoginInfo.CompanyCode, bpSalegrp.CodeValue};
//							DataSet lds_temp = (DataSet)(InvokeRemoteMethod("MasterSale_NTX", "master.CC_MA_SALEGRP_NTX", "CC_MA_SALEGRP_NTX.rem", "Select_Detail", ls_args));
//
//							if(lds_temp.Tables[0].Rows.Count == 0)
//							{
//								ShowHelp(sender, bpSalegrp.CodeValue);
//								return;
//							}
//
//							bpSalegrp.CodeName = lds_temp.Tables[0].Rows[0]["NM_SALEGRP"].ToString();
//							bpSalegrp.CodeValue = lds_temp.Tables[0].Rows[0]["CD_SALEGRP"].ToString();
//							bpSalegrp.IsConfirmed = true;
//						}
//						else
//						{
//							bpSalegrp.CodeValue = "";
//							bpSalegrp.CodeName = "";
//						}
//					}
					break;
			}
		}

		/// <summary>
		/// Enter Key입력시 Tab Key 이동
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">e</param>
		private void OnEvent_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			//Focus 다음으로 이동
			if(e.KeyData == Keys.Enter)
				System.Windows.Forms.SendKeys.Send("{TAB}");

			//도움창 띄우기
			if(e.KeyData == Keys.F3)
				OnHelp_Click(sender, e);
		}

		private void m_txtDT_SO1_Validated(object sender, System.EventArgs e)
		{
			if(!this.m_txtDT_SO1.IsValidated)
			{
				ShowMessage("WK1_003", m_lblDT_SO.Text);
				this.m_txtDT_SO1.Focus();
			}

		}

		private void m_txtDT_SO2_Validated(object sender, System.EventArgs e)
		{
			if(!this.m_txtDT_SO2.IsValidated)
			{
				ShowMessage("WK1_003", m_lblDT_SO.Text);
				this.m_txtDT_SO2.Focus();
			}
		}

		#endregion

		private void Control_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
		{
			if(e.DialogResult == DialogResult.Cancel)
				return;

			switch(e.ControlName)
			{				
				case "bpSalegrp" :	// 영업그룹				
					bpSalegrp.CodeName = e.HelpReturn.Rows[0]["NM_SALEGRP"].ToString();
					bpSalegrp.CodeValue = e.HelpReturn.Rows[0]["CD_SALEGRP"].ToString();
					
					break;
			}
					
		}
	}
}

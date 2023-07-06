using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Diagnostics;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.Common.Controls;
//using C1.Common;
using System.Text;

using C1.Win.C1FlexGrid;
using Dass.FlexGrid;


namespace sale
{
	/// <summary>
	/// P_SA_SO_MNG에 대한 요약 설명입니다.
	/// </summary>
	public class P_SA_SO_MNG_BAK : Duzon.Common.Forms.PageBase
	{
		#region ♣ 멤버필드

		#region -> 멤버필드(일반)

		// Panel
		private Duzon.Common.Controls.PanelExt panel4;
		private Duzon.Common.Controls.PanelExt panel17;
		private Duzon.Common.Controls.PanelExt panel6;
		private Duzon.Common.Controls.PanelExt panel5;
		private Duzon.Common.Controls.PanelExt panel8;
        private Duzon.Common.Controls.PanelExt panel7;
		private Duzon.Common.Controls.PanelExt m_pnlHead;
		private Duzon.Common.Controls.PanelExt m_pnlLine;

		// Label
		private Duzon.Common.Controls.LabelExt m_lblTpSo;
		private Duzon.Common.Controls.LabelExt m_lblSalegrp;
		private Duzon.Common.Controls.LabelExt m_lblPartner;
		private Duzon.Common.Controls.LabelExt m_lblDtSo;
		private Duzon.Common.Controls.LabelExt m_lblStaSo;
		private Duzon.Common.Controls.LabelExt m_lblBizarea;
        private Duzon.Common.Controls.LabelExt label3;

		// RoundedButton
		private Duzon.Common.Controls.RoundedButton m_btnChkAtp;
		private Duzon.Common.Controls.RoundedButton m_btnCloseL;
		private Duzon.Common.Controls.RoundedButton m_btnDel;
		private Duzon.Common.Controls.RoundedButton m_btnCloseH;
		private Duzon.Common.Controls.RoundedButton m_btnUnConfirm;
		private Duzon.Common.Controls.RoundedButton m_btnConfirm;
		private Duzon.Common.Controls.RoundedButton m_btnReleaseL;
		private Duzon.Common.Controls.RoundedButton m_btnSelect;

		// RadioButton
		private Duzon.Common.Controls.RadioButtonExt m_rdoReturn;
		private Duzon.Common.Controls.RadioButtonExt m_rdoSo;

		// DzComboBox
		private Duzon.Common.Controls.DropDownComboBox m_cboStaSo;

		// IContainer
		private System.ComponentModel.IContainer components;

		object[] m_obj_DEL;
		#endregion

		#region -> 멤버필드(주요)

		/// <summary>
		/// 헤더 그리드
		/// </summary>
		private Dass.FlexGrid.FlexGrid _flexH;

		/// <summary>
		/// 라인 그리드
		/// </summary>
		private Dass.FlexGrid.FlexGrid _flexL;
		
		// DataSet
		private DataSet g_dsCombo, g_dsSoMng_H, g_dsSoMng_L = null;
				
		// DataTable
		private DataTable g_dtDelete;
		private Duzon.Common.Controls.DatePicker m_mskDtStart;
		private Duzon.Common.Controls.DatePicker m_mskDtEnd;
		private Duzon.Common.BpControls.BpCodeTextBox bpSalegrp;
		private Duzon.Common.BpControls.BpCodeTextBox bpNoEmp;
		private Duzon.Common.BpControls.BpCodeTextBox bpSoPartner;
		private Duzon.Common.BpControls.BpCodeTextBox bpTpSo;
        private PanelExt panelExt1;
        private PanelExt m_pnlBasecredit;
        private TableLayoutPanel tableLayoutPanel1;

		/// <summary>
		/// Load여부 변수(Paint Event에서 사용)
		/// </summary>
		private bool _isPainted = false;

		#endregion

		#endregion

		#region ♣ 생성자/소멸자

		#region -> 생성자

        public P_SA_SO_MNG_BAK()
		{
			InitializeComponent();

			InitializeTable();

			this.Load += new System.EventHandler(Page_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(Page_Paint);
		}

		#endregion

		#region -> Component Designer generated code
		/// <summary>
		/// 디자이너 지원에 필요한 메서드입니다. 
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_SA_SO_MNG_BAK));
            this.m_btnChkAtp = new Duzon.Common.Controls.RoundedButton(this.components);
            this.m_pnlLine = new Duzon.Common.Controls.PanelExt();
            this._flexL = new Dass.FlexGrid.FlexGrid(this.components);
            this.panel4 = new Duzon.Common.Controls.PanelExt();
            this.bpTpSo = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpSoPartner = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpNoEmp = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpSalegrp = new Duzon.Common.BpControls.BpCodeTextBox();
            this.m_mskDtEnd = new Duzon.Common.Controls.DatePicker();
            this.m_mskDtStart = new Duzon.Common.Controls.DatePicker();
            this.m_cboStaSo = new Duzon.Common.Controls.DropDownComboBox();
            this.label3 = new Duzon.Common.Controls.LabelExt();
            this.panel17 = new Duzon.Common.Controls.PanelExt();
            this.panel6 = new Duzon.Common.Controls.PanelExt();
            this.m_lblTpSo = new Duzon.Common.Controls.LabelExt();
            this.m_lblSalegrp = new Duzon.Common.Controls.LabelExt();
            this.panel5 = new Duzon.Common.Controls.PanelExt();
            this.m_lblPartner = new Duzon.Common.Controls.LabelExt();
            this.m_lblDtSo = new Duzon.Common.Controls.LabelExt();
            this.panel8 = new Duzon.Common.Controls.PanelExt();
            this.m_lblStaSo = new Duzon.Common.Controls.LabelExt();
            this.m_lblBizarea = new Duzon.Common.Controls.LabelExt();
            this.panel7 = new Duzon.Common.Controls.PanelExt();
            this.m_rdoReturn = new Duzon.Common.Controls.RadioButtonExt();
            this.m_rdoSo = new Duzon.Common.Controls.RadioButtonExt();
            this.m_pnlHead = new Duzon.Common.Controls.PanelExt();
            this._flexH = new Dass.FlexGrid.FlexGrid(this.components);
            this.m_btnCloseL = new Duzon.Common.Controls.RoundedButton(this.components);
            this.m_btnDel = new Duzon.Common.Controls.RoundedButton(this.components);
            this.m_btnCloseH = new Duzon.Common.Controls.RoundedButton(this.components);
            this.m_btnUnConfirm = new Duzon.Common.Controls.RoundedButton(this.components);
            this.m_btnConfirm = new Duzon.Common.Controls.RoundedButton(this.components);
            this.m_btnReleaseL = new Duzon.Common.Controls.RoundedButton(this.components);
            this.m_btnSelect = new Duzon.Common.Controls.RoundedButton(this.components);
            this.panelExt1 = new Duzon.Common.Controls.PanelExt();
            this.m_pnlBasecredit = new Duzon.Common.Controls.PanelExt();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.m_pnlLine.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexL)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_mskDtEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_mskDtStart)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_rdoReturn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_rdoSo)).BeginInit();
            this.m_pnlHead.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).BeginInit();
            this.panelExt1.SuspendLayout();
            this.m_pnlBasecredit.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_btnChkAtp
            // 
            this.m_btnChkAtp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnChkAtp.BackColor = System.Drawing.Color.White;
            this.m_btnChkAtp.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.m_btnChkAtp.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.m_btnChkAtp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnChkAtp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnChkAtp.Location = new System.Drawing.Point(707, 0);
            this.m_btnChkAtp.Name = "m_btnChkAtp";
            this.m_btnChkAtp.Size = new System.Drawing.Size(80, 24);
            this.m_btnChkAtp.TabIndex = 76;
            this.m_btnChkAtp.TabStop = false;
            this.m_btnChkAtp.Tag = "CHK_ATP";
            this.m_btnChkAtp.Text = "APT체크";
            this.m_btnChkAtp.UseVisualStyleBackColor = false;
            this.m_btnChkAtp.Click += new System.EventHandler(this.OpenAtpCheck);
            // 
            // m_pnlLine
            // 
            this.m_pnlLine.Controls.Add(this._flexL);
            this.m_pnlLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlLine.Location = new System.Drawing.Point(3, 347);
            this.m_pnlLine.Name = "m_pnlLine";
            this.m_pnlLine.Size = new System.Drawing.Size(787, 211);
            this.m_pnlLine.TabIndex = 75;
            // 
            // _flexL
            // 
            this._flexL.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexL.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexL.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexL.AutoResize = false;
            this._flexL.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexL.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexL.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexL.EnabledHeaderCheck = true;
            this._flexL.IsDataChanged = false;
            this._flexL.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexL.Location = new System.Drawing.Point(0, 0);
            this._flexL.Name = "_flexL";
            this._flexL.RowFilter = "";
            this._flexL.Rows.Count = 1;
            this._flexL.Rows.DefaultSize = 20;
            this._flexL.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexL.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexL.ShowSort = false;
            this._flexL.Size = new System.Drawing.Size(787, 211);
            this._flexL.StyleInfo = resources.GetString("_flexL.StyleInfo");
            this._flexL.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.bpTpSo);
            this.panel4.Controls.Add(this.bpSoPartner);
            this.panel4.Controls.Add(this.bpNoEmp);
            this.panel4.Controls.Add(this.bpSalegrp);
            this.panel4.Controls.Add(this.m_mskDtEnd);
            this.panel4.Controls.Add(this.m_mskDtStart);
            this.panel4.Controls.Add(this.m_cboStaSo);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.panel17);
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.panel8);
            this.panel4.Controls.Add(this.panel7);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 36);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(787, 55);
            this.panel4.TabIndex = 0;
            // 
            // bpTpSo
            // 
            this.bpTpSo.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpTpSo.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpTpSo.ButtonImage")));
            this.bpTpSo.ChildMode = "";
            this.bpTpSo.CodeName = "";
            this.bpTpSo.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpTpSo.CodeValue = "";
            this.bpTpSo.ComboCheck = true;
            this.bpTpSo.HelpID = Duzon.Common.Forms.Help.HelpID.P_SA_TPSO_SUB;
            this.bpTpSo.ItemBackColor = System.Drawing.Color.White;
            this.bpTpSo.Location = new System.Drawing.Point(354, 30);
            this.bpTpSo.Name = "bpTpSo";
            this.bpTpSo.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpTpSo.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpTpSo.SearchCode = true;
            this.bpTpSo.SelectCount = 0;
            this.bpTpSo.SetDefaultValue = false;
            this.bpTpSo.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpTpSo.Size = new System.Drawing.Size(128, 21);
            this.bpTpSo.TabIndex = 5;
            this.bpTpSo.Text = "bpCodeTextBox4";
            this.bpTpSo.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryBefore);
            this.bpTpSo.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
            // 
            // bpSoPartner
            // 
            this.bpSoPartner.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpSoPartner.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpSoPartner.ButtonImage")));
            this.bpSoPartner.ChildMode = "";
            this.bpSoPartner.CodeName = "";
            this.bpSoPartner.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpSoPartner.CodeValue = "";
            this.bpSoPartner.ComboCheck = true;
            this.bpSoPartner.HelpID = Duzon.Common.Forms.Help.HelpID.P_SA_TPPTR_SUB;
            this.bpSoPartner.ItemBackColor = System.Drawing.Color.White;
            this.bpSoPartner.Location = new System.Drawing.Point(79, 30);
            this.bpSoPartner.Name = "bpSoPartner";
            this.bpSoPartner.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpSoPartner.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpSoPartner.SearchCode = true;
            this.bpSoPartner.SelectCount = 0;
            this.bpSoPartner.SetDefaultValue = false;
            this.bpSoPartner.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpSoPartner.Size = new System.Drawing.Size(192, 21);
            this.bpSoPartner.TabIndex = 4;
            this.bpSoPartner.Text = "bpCodeTextBox3";
            this.bpSoPartner.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryBefore);
            this.bpSoPartner.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
            // 
            // bpNoEmp
            // 
            this.bpNoEmp.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpNoEmp.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpNoEmp.ButtonImage")));
            this.bpNoEmp.ChildMode = "";
            this.bpNoEmp.CodeName = "";
            this.bpNoEmp.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpNoEmp.CodeValue = "";
            this.bpNoEmp.ComboCheck = true;
            this.bpNoEmp.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
            this.bpNoEmp.ItemBackColor = System.Drawing.Color.White;
            this.bpNoEmp.Location = new System.Drawing.Point(568, 4);
            this.bpNoEmp.Name = "bpNoEmp";
            this.bpNoEmp.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpNoEmp.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpNoEmp.SearchCode = true;
            this.bpNoEmp.SelectCount = 0;
            this.bpNoEmp.SetDefaultValue = false;
            this.bpNoEmp.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpNoEmp.Size = new System.Drawing.Size(128, 21);
            this.bpNoEmp.TabIndex = 3;
            this.bpNoEmp.Text = "bpCodeTextBox2";
            this.bpNoEmp.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryBefore);
            this.bpNoEmp.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
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
            this.bpSalegrp.Location = new System.Drawing.Point(353, 3);
            this.bpSalegrp.Name = "bpSalegrp";
            this.bpSalegrp.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpSalegrp.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpSalegrp.SearchCode = true;
            this.bpSalegrp.SelectCount = 0;
            this.bpSalegrp.SetDefaultValue = false;
            this.bpSalegrp.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpSalegrp.Size = new System.Drawing.Size(128, 21);
            this.bpSalegrp.TabIndex = 2;
            this.bpSalegrp.Text = "bpCodeTextBox1";
            this.bpSalegrp.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryBefore);
            this.bpSalegrp.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
            // 
            // m_mskDtEnd
            // 
            this.m_mskDtEnd.CalendarBackColor = System.Drawing.Color.White;
            this.m_mskDtEnd.DayColor = System.Drawing.SystemColors.ControlText;
            this.m_mskDtEnd.FriDayColor = System.Drawing.Color.Blue;
            this.m_mskDtEnd.Location = new System.Drawing.Point(184, 4);
            this.m_mskDtEnd.Mask = "####/##/##";
            this.m_mskDtEnd.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.m_mskDtEnd.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.m_mskDtEnd.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.m_mskDtEnd.Modified = false;
            this.m_mskDtEnd.Name = "m_mskDtEnd";
            this.m_mskDtEnd.PaddingCharacter = '_';
            this.m_mskDtEnd.PassivePromptCharacter = '_';
            this.m_mskDtEnd.PromptCharacter = '_';
            this.m_mskDtEnd.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.m_mskDtEnd.ShowToDay = true;
            this.m_mskDtEnd.ShowTodayCircle = true;
            this.m_mskDtEnd.ShowUpDown = false;
            this.m_mskDtEnd.Size = new System.Drawing.Size(85, 21);
            this.m_mskDtEnd.SunDayColor = System.Drawing.Color.Red;
            this.m_mskDtEnd.TabIndex = 1;
            this.m_mskDtEnd.TitleBackColor = System.Drawing.SystemColors.Control;
            this.m_mskDtEnd.TitleForeColor = System.Drawing.Color.Black;
            this.m_mskDtEnd.ToDayColor = System.Drawing.Color.Red;
            this.m_mskDtEnd.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.m_mskDtEnd.UseKeyF3 = false;
            this.m_mskDtEnd.Value = new System.DateTime(((long)(0)));
            this.m_mskDtEnd.Validated += new System.EventHandler(this.m_mskDtEnd_Validated);
            this.m_mskDtEnd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mskDtEnd_KeyDown);
            // 
            // m_mskDtStart
            // 
            this.m_mskDtStart.CalendarBackColor = System.Drawing.Color.White;
            this.m_mskDtStart.DayColor = System.Drawing.SystemColors.ControlText;
            this.m_mskDtStart.FriDayColor = System.Drawing.Color.Blue;
            this.m_mskDtStart.Location = new System.Drawing.Point(80, 4);
            this.m_mskDtStart.Mask = "####/##/##";
            this.m_mskDtStart.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.m_mskDtStart.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.m_mskDtStart.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.m_mskDtStart.Modified = false;
            this.m_mskDtStart.Name = "m_mskDtStart";
            this.m_mskDtStart.PaddingCharacter = '_';
            this.m_mskDtStart.PassivePromptCharacter = '_';
            this.m_mskDtStart.PromptCharacter = '_';
            this.m_mskDtStart.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.m_mskDtStart.ShowToDay = true;
            this.m_mskDtStart.ShowTodayCircle = true;
            this.m_mskDtStart.ShowUpDown = false;
            this.m_mskDtStart.Size = new System.Drawing.Size(85, 21);
            this.m_mskDtStart.SunDayColor = System.Drawing.Color.Red;
            this.m_mskDtStart.TabIndex = 0;
            this.m_mskDtStart.TitleBackColor = System.Drawing.SystemColors.Control;
            this.m_mskDtStart.TitleForeColor = System.Drawing.Color.Black;
            this.m_mskDtStart.ToDayColor = System.Drawing.Color.Red;
            this.m_mskDtStart.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.m_mskDtStart.UseKeyF3 = false;
            this.m_mskDtStart.Value = new System.DateTime(((long)(0)));
            this.m_mskDtStart.Validated += new System.EventHandler(this.m_mskDtStart_Validated);
            this.m_mskDtStart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mskDtStart_KeyDown);
            // 
            // m_cboStaSo
            // 
            this.m_cboStaSo.AutoDropDown = true;
            this.m_cboStaSo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboStaSo.Font = new System.Drawing.Font("GulimChe", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.m_cboStaSo.ItemHeight = 12;
            this.m_cboStaSo.Location = new System.Drawing.Point(568, 31);
            this.m_cboStaSo.Name = "m_cboStaSo";
            this.m_cboStaSo.ShowCheckBox = false;
            this.m_cboStaSo.Size = new System.Drawing.Size(132, 20);
            this.m_cboStaSo.TabIndex = 6;
            this.m_cboStaSo.UseKeyEnter = false;
            this.m_cboStaSo.UseKeyF3 = false;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(164, 7);
            this.label3.Name = "label3";
            this.label3.Resizeble = true;
            this.label3.Size = new System.Drawing.Size(18, 18);
            this.label3.TabIndex = 74;
            this.label3.Text = "∼";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel17
            // 
            this.panel17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel17.BackColor = System.Drawing.Color.Transparent;
            this.panel17.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel17.BackgroundImage")));
            this.panel17.Location = new System.Drawing.Point(5, 27);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(777, 1);
            this.panel17.TabIndex = 70;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel6.Controls.Add(this.m_lblTpSo);
            this.panel6.Controls.Add(this.m_lblSalegrp);
            this.panel6.Location = new System.Drawing.Point(274, 1);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(75, 51);
            this.panel6.TabIndex = 14;
            // 
            // m_lblTpSo
            // 
            this.m_lblTpSo.BackColor = System.Drawing.Color.Transparent;
            this.m_lblTpSo.Location = new System.Drawing.Point(2, 31);
            this.m_lblTpSo.Name = "m_lblTpSo";
            this.m_lblTpSo.Resizeble = true;
            this.m_lblTpSo.Size = new System.Drawing.Size(70, 18);
            this.m_lblTpSo.TabIndex = 9;
            this.m_lblTpSo.Tag = "TP_SO";
            this.m_lblTpSo.Text = "수주형태";
            this.m_lblTpSo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblSalegrp
            // 
            this.m_lblSalegrp.BackColor = System.Drawing.Color.Transparent;
            this.m_lblSalegrp.Location = new System.Drawing.Point(2, 6);
            this.m_lblSalegrp.Name = "m_lblSalegrp";
            this.m_lblSalegrp.Resizeble = true;
            this.m_lblSalegrp.Size = new System.Drawing.Size(70, 18);
            this.m_lblSalegrp.TabIndex = 4;
            this.m_lblSalegrp.Tag = "CD_SALEGRP";
            this.m_lblSalegrp.Text = "영업그룹";
            this.m_lblSalegrp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel5.Controls.Add(this.m_lblPartner);
            this.panel5.Controls.Add(this.m_lblDtSo);
            this.panel5.Location = new System.Drawing.Point(1, 1);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(75, 51);
            this.panel5.TabIndex = 13;
            // 
            // m_lblPartner
            // 
            this.m_lblPartner.BackColor = System.Drawing.Color.Transparent;
            this.m_lblPartner.Location = new System.Drawing.Point(2, 31);
            this.m_lblPartner.Name = "m_lblPartner";
            this.m_lblPartner.Resizeble = true;
            this.m_lblPartner.Size = new System.Drawing.Size(70, 18);
            this.m_lblPartner.TabIndex = 8;
            this.m_lblPartner.Tag = "SO_PARTNER";
            this.m_lblPartner.Text = "수주처";
            this.m_lblPartner.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblDtSo
            // 
            this.m_lblDtSo.BackColor = System.Drawing.Color.Transparent;
            this.m_lblDtSo.Location = new System.Drawing.Point(2, 6);
            this.m_lblDtSo.Name = "m_lblDtSo";
            this.m_lblDtSo.Resizeble = true;
            this.m_lblDtSo.Size = new System.Drawing.Size(70, 18);
            this.m_lblDtSo.TabIndex = 0;
            this.m_lblDtSo.Tag = "DT_SO";
            this.m_lblDtSo.Text = "수주일자";
            this.m_lblDtSo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel8.Controls.Add(this.m_lblStaSo);
            this.panel8.Controls.Add(this.m_lblBizarea);
            this.panel8.Location = new System.Drawing.Point(489, 1);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(75, 51);
            this.panel8.TabIndex = 71;
            // 
            // m_lblStaSo
            // 
            this.m_lblStaSo.BackColor = System.Drawing.Color.Transparent;
            this.m_lblStaSo.Location = new System.Drawing.Point(8, 31);
            this.m_lblStaSo.Name = "m_lblStaSo";
            this.m_lblStaSo.Resizeble = true;
            this.m_lblStaSo.Size = new System.Drawing.Size(64, 18);
            this.m_lblStaSo.TabIndex = 9;
            this.m_lblStaSo.Tag = "STA_SO";
            this.m_lblStaSo.Text = "진행상태";
            this.m_lblStaSo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblBizarea
            // 
            this.m_lblBizarea.BackColor = System.Drawing.Color.Transparent;
            this.m_lblBizarea.Location = new System.Drawing.Point(2, 6);
            this.m_lblBizarea.Name = "m_lblBizarea";
            this.m_lblBizarea.Resizeble = true;
            this.m_lblBizarea.Size = new System.Drawing.Size(70, 18);
            this.m_lblBizarea.TabIndex = 4;
            this.m_lblBizarea.Tag = "NO_EMP";
            this.m_lblBizarea.Text = "담당자";
            this.m_lblBizarea.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel7.Controls.Add(this.m_rdoReturn);
            this.panel7.Controls.Add(this.m_rdoSo);
            this.panel7.Location = new System.Drawing.Point(706, 1);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(78, 51);
            this.panel7.TabIndex = 87;
            // 
            // m_rdoReturn
            // 
            this.m_rdoReturn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_rdoReturn.Location = new System.Drawing.Point(6, 31);
            this.m_rdoReturn.Name = "m_rdoReturn";
            this.m_rdoReturn.Size = new System.Drawing.Size(70, 18);
            this.m_rdoReturn.TabIndex = 1;
            this.m_rdoReturn.TabStop = true;
            this.m_rdoReturn.Tag = "RETURN";
            this.m_rdoReturn.Text = "반품";
            this.m_rdoReturn.UseKeyEnter = false;
            // 
            // m_rdoSo
            // 
            this.m_rdoSo.Checked = true;
            this.m_rdoSo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_rdoSo.Location = new System.Drawing.Point(6, 5);
            this.m_rdoSo.Name = "m_rdoSo";
            this.m_rdoSo.Size = new System.Drawing.Size(70, 18);
            this.m_rdoSo.TabIndex = 0;
            this.m_rdoSo.TabStop = true;
            this.m_rdoSo.Tag = "SO";
            this.m_rdoSo.Text = "수주";
            this.m_rdoSo.UseKeyEnter = false;
            // 
            // m_pnlHead
            // 
            this.m_pnlHead.Controls.Add(this._flexH);
            this.m_pnlHead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlHead.Location = new System.Drawing.Point(3, 97);
            this.m_pnlHead.Name = "m_pnlHead";
            this.m_pnlHead.Size = new System.Drawing.Size(787, 211);
            this.m_pnlHead.TabIndex = 77;
            // 
            // _flexH
            // 
            this._flexH.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexH.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexH.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexH.AutoResize = false;
            this._flexH.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexH.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexH.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexH.EnabledHeaderCheck = true;
            this._flexH.IsDataChanged = false;
            this._flexH.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexH.Location = new System.Drawing.Point(0, 0);
            this._flexH.Name = "_flexH";
            this._flexH.RowFilter = "";
            this._flexH.Rows.Count = 1;
            this._flexH.Rows.DefaultSize = 20;
            this._flexH.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexH.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexH.ShowSort = false;
            this._flexH.Size = new System.Drawing.Size(787, 211);
            this._flexH.StyleInfo = resources.GetString("_flexH.StyleInfo");
            this._flexH.TabIndex = 0;
            // 
            // m_btnCloseL
            // 
            this.m_btnCloseL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCloseL.BackColor = System.Drawing.Color.White;
            this.m_btnCloseL.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.m_btnCloseL.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.m_btnCloseL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnCloseL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnCloseL.Location = new System.Drawing.Point(563, 0);
            this.m_btnCloseL.Name = "m_btnCloseL";
            this.m_btnCloseL.Size = new System.Drawing.Size(60, 24);
            this.m_btnCloseL.TabIndex = 78;
            this.m_btnCloseL.TabStop = false;
            this.m_btnCloseL.Tag = "CLOSE_L";
            this.m_btnCloseL.Text = "종결";
            this.m_btnCloseL.UseVisualStyleBackColor = false;
            this.m_btnCloseL.Click += new System.EventHandler(this.CloseSoDetail);
            // 
            // m_btnDel
            // 
            this.m_btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnDel.BackColor = System.Drawing.Color.White;
            this.m_btnDel.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.m_btnDel.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.m_btnDel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnDel.Enabled = false;
            this.m_btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnDel.Location = new System.Drawing.Point(501, 0);
            this.m_btnDel.Name = "m_btnDel";
            this.m_btnDel.Size = new System.Drawing.Size(60, 24);
            this.m_btnDel.TabIndex = 79;
            this.m_btnDel.TabStop = false;
            this.m_btnDel.Tag = "DEL";
            this.m_btnDel.Text = "삭제";
            this.m_btnDel.UseVisualStyleBackColor = false;
            this.m_btnDel.Click += new System.EventHandler(this.DeleteRowBySoMng);
            // 
            // m_btnCloseH
            // 
            this.m_btnCloseH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCloseH.BackColor = System.Drawing.Color.White;
            this.m_btnCloseH.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.m_btnCloseH.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.m_btnCloseH.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnCloseH.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnCloseH.Location = new System.Drawing.Point(708, 0);
            this.m_btnCloseH.Name = "m_btnCloseH";
            this.m_btnCloseH.Size = new System.Drawing.Size(80, 24);
            this.m_btnCloseH.TabIndex = 81;
            this.m_btnCloseH.TabStop = false;
            this.m_btnCloseH.Tag = "CLOSE_H";
            this.m_btnCloseH.Text = "오더종결";
            this.m_btnCloseH.UseVisualStyleBackColor = false;
            this.m_btnCloseH.Click += new System.EventHandler(this.CloseSoHead);
            // 
            // m_btnUnConfirm
            // 
            this.m_btnUnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnUnConfirm.BackColor = System.Drawing.Color.White;
            this.m_btnUnConfirm.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.m_btnUnConfirm.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.m_btnUnConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnUnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnUnConfirm.Location = new System.Drawing.Point(626, 0);
            this.m_btnUnConfirm.Name = "m_btnUnConfirm";
            this.m_btnUnConfirm.Size = new System.Drawing.Size(80, 24);
            this.m_btnUnConfirm.TabIndex = 80;
            this.m_btnUnConfirm.TabStop = false;
            this.m_btnUnConfirm.Tag = "B_UNCONFIRM";
            this.m_btnUnConfirm.Text = "확정취소";
            this.m_btnUnConfirm.UseVisualStyleBackColor = false;
            this.m_btnUnConfirm.Click += new System.EventHandler(this.UnConfirmSo);
            // 
            // m_btnConfirm
            // 
            this.m_btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnConfirm.BackColor = System.Drawing.Color.White;
            this.m_btnConfirm.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.m_btnConfirm.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.m_btnConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnConfirm.Location = new System.Drawing.Point(544, 0);
            this.m_btnConfirm.Name = "m_btnConfirm";
            this.m_btnConfirm.Size = new System.Drawing.Size(80, 24);
            this.m_btnConfirm.TabIndex = 82;
            this.m_btnConfirm.TabStop = false;
            this.m_btnConfirm.Tag = "B_CONFIRM";
            this.m_btnConfirm.Text = "확정";
            this.m_btnConfirm.UseVisualStyleBackColor = false;
            this.m_btnConfirm.Click += new System.EventHandler(this.ConfirmSo);
            // 
            // m_btnReleaseL
            // 
            this.m_btnReleaseL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnReleaseL.BackColor = System.Drawing.Color.White;
            this.m_btnReleaseL.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.m_btnReleaseL.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.m_btnReleaseL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnReleaseL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnReleaseL.Location = new System.Drawing.Point(625, 0);
            this.m_btnReleaseL.Name = "m_btnReleaseL";
            this.m_btnReleaseL.Size = new System.Drawing.Size(80, 24);
            this.m_btnReleaseL.TabIndex = 83;
            this.m_btnReleaseL.TabStop = false;
            this.m_btnReleaseL.Tag = "RELEASE_L";
            this.m_btnReleaseL.Text = "종결취소";
            this.m_btnReleaseL.UseVisualStyleBackColor = false;
            this.m_btnReleaseL.Click += new System.EventHandler(this.UnCloseSoDetail);
            // 
            // m_btnSelect
            // 
            this.m_btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnSelect.BackColor = System.Drawing.Color.White;
            this.m_btnSelect.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.m_btnSelect.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.m_btnSelect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnSelect.Location = new System.Drawing.Point(420, 0);
            this.m_btnSelect.Name = "m_btnSelect";
            this.m_btnSelect.Size = new System.Drawing.Size(80, 24);
            this.m_btnSelect.TabIndex = 84;
            this.m_btnSelect.TabStop = false;
            this.m_btnSelect.Tag = "Q_SELECT";
            this.m_btnSelect.Text = "선택/취소";
            this.m_btnSelect.UseVisualStyleBackColor = false;
            this.m_btnSelect.Click += new System.EventHandler(this.m_btnSelect_Click);
            // 
            // panelExt1
            // 
            this.panelExt1.AutoSize = true;
            this.panelExt1.Controls.Add(this.m_btnChkAtp);
            this.panelExt1.Controls.Add(this.m_btnCloseL);
            this.panelExt1.Controls.Add(this.m_btnDel);
            this.panelExt1.Controls.Add(this.m_btnSelect);
            this.panelExt1.Controls.Add(this.m_btnReleaseL);
            this.panelExt1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExt1.Location = new System.Drawing.Point(3, 314);
            this.panelExt1.Name = "panelExt1";
            this.panelExt1.Size = new System.Drawing.Size(787, 27);
            this.panelExt1.TabIndex = 87;
            // 
            // m_pnlBasecredit
            // 
            this.m_pnlBasecredit.AutoSize = true;
            this.m_pnlBasecredit.Controls.Add(this.m_btnConfirm);
            this.m_pnlBasecredit.Controls.Add(this.m_btnUnConfirm);
            this.m_pnlBasecredit.Controls.Add(this.m_btnCloseH);
            this.m_pnlBasecredit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlBasecredit.Location = new System.Drawing.Point(3, 3);
            this.m_pnlBasecredit.Name = "m_pnlBasecredit";
            this.m_pnlBasecredit.Size = new System.Drawing.Size(787, 27);
            this.m_pnlBasecredit.TabIndex = 86;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panelExt1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.m_pnlBasecredit, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.m_pnlLine, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.m_pnlHead, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 59);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(793, 561);
            this.tableLayoutPanel1.TabIndex = 85;
            // 
            // P_SA_SO_MNG_BAK
            // 
            this.Controls.Add(this.tableLayoutPanel1);
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Name = "P_SA_SO_MNG_BAK";
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.m_pnlLine.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexL)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_mskDtEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_mskDtStart)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_rdoReturn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_rdoSo)).EndInit();
            this.m_pnlHead.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).EndInit();
            this.panelExt1.ResumeLayout(false);
            this.m_pnlBasecredit.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

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
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		#endregion

		#endregion

		#region ♣ 초기화

		#region -> InitializeTable

		private void InitializeTable()
		{
			// 라인삭제를 위한 테이블
			g_dtDelete = new DataTable();

			g_dtDelete.Columns.Add(new DataColumn("CD_COMPANY"));
			g_dtDelete.Columns.Add(new DataColumn("NO_SO"));
			g_dtDelete.Columns.Add(new DataColumn("SEQ_SO"));
			g_dtDelete.Columns.Add(new DataColumn("CONF"));
			g_dtDelete.Columns.Add(new DataColumn("GIR"));
		}

		#endregion

		#region -> Page_Load

		/// <summary>
		/// 페이지 로드 이벤트 핸들러(화면 초기화 작업)
		/// </summary>
		private void Page_Load(object sender, EventArgs e)
		{
			try
			{
				this.Enabled   = false;

				// 페이지를 로드하는 중입니다.
				this.ShowStatusBarMessage(1);
				this.SetProgressBarValue(100, 10);			
			
				// 그리드 컨트롤을 초기화 한다.
				InitGridH();
				InitGridL();
				this.SetProgressBarValue(100, 30);

				InitControl();
				this.SetProgressBarValue(100, 70);

				Application.DoEvents();			
			}
			catch(Exception ex)
			{
				MsgEnd(ex);
			}
		}

		#endregion

		#region -> GetDDItem

		private string GetDDItem(params string[] colName)
		{
			string temp = "";
			
			for(int i = 0; i < colName.Length; i++)
			{
				switch(colName[i])
				{
						// Header Grid의 컬럼이름
					case "CHK":			// 1.선택
						temp = temp + " + " +  "S";
						break;
					case "NO_SO":		// 2.수주번호
						temp = temp + " + " + this.GetDataDictionaryItem("SA","NO_SO");
						break;
					case "DT_SO":		// 3.수주일자
						temp = temp + " + " + this.GetDataDictionaryItem("SA","DT_SO");
						break;
					case "LN_PARTNER":	// 4.수주처
						temp = temp + " + " + this.GetDataDictionaryItem("SA","CD_PARTNER");
						break;
					case "NM_SO":		// 5.수주형태
						temp = temp + " + " + this.GetDataDictionaryItem("SA","TP_SO");
						break;
					case "NM_EXCH":		// 6.화폐단위
						temp = temp + " + " + this.GetDataDictionaryItem("SA","CD_EXCH");
						break;
					case "AM_SO":		// 7.수주금액
						temp = temp + " + " + this.GetDataDictionaryItem("SA","AM_SO");
						break;
					case "NM_SALEGRP":	// 8.영업그룹
						temp = temp + " + " + this.GetDataDictionaryItem("SA","CD_SALEGRP");
						break;
					case "NM_BIZAREA":	// 9.사업장
						temp = temp + " + " + this.GetDataDictionaryItem("SA","CD_BIZAREA");
						break;
					case "NM_EMP":		// 10. 사원명
						temp = temp + " + " + this.GetDataDictionaryItem("SA","NM_EMP");
						break;
					case "STA_SO":		// 11. 진행상태
						temp = temp + " + " + this.GetDataDictionaryItem("SA","STA_SO");
						break;
					case "TP_VAT":		// 12. 부가세 구분
						temp = temp + " + " + this.GetDataDictionaryItem("SA","TP_VAT");
						break;
					case "FG_VAT":		// 13. 부가세 포함
						temp = temp + " + " + this.GetDataDictionaryItem("SA","FG_VAT");
						break;
		
						// Line Grid의 컬럼이름
					case "SEQ_SO":		//2.항번
						temp = temp + " + " + this.GetDataDictionaryItem("SA","SEQ");
						break;
					case "CD_ITEM":		// 3.품목코드
						temp = temp + " + " + this.GetDataDictionaryItem("SA","CD_ITEM");
						break;
					case "NM_ITEM":		// 4.품목명
						temp = temp + " + " + this.GetDataDictionaryItem("SA","NM_ITEM");
						break;
					case "STND_ITEM":	// 5.규격
						temp = temp + " + " + this.GetDataDictionaryItem("SA","SPEC_ITEM");
						break;
					case "UNIT_SO":		// 6.단위
						temp = temp + " + " + this.GetDataDictionaryItem("SA","UNIT");
						break;
					case "QT_SO":		// 7.수주수량
						temp = temp + " + " + this.GetDataDictionaryItem("SA","QT");
						break;
					case "UM_SO":		// 8.수주단가
						temp = temp + " + " + this.GetDataDictionaryItem("SA","PRICE");
						break;
					case "NM_PLANT":	// 10.공장
						temp = temp + " + " + this.GetDataDictionaryItem("SA","CD_PLANT");
						break;
					case "NO_PROJECT":	// 11.프로젝트
						temp = temp + " + " + this.GetDataDictionaryItem("SA","NO_PROJECT");
						break;
					case "NM_SYSDEF":	// 12.처리상태
						temp = temp + " + " + this.GetDataDictionaryItem("SA","STA_SO");
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

		#region -> InitGridH

		private void InitGridH()
		{
			Application.DoEvents();
			
			_flexH.Redraw = false;

			_flexH.Rows.Count = 1;
			_flexH.Rows.Fixed = 1;
			_flexH.Cols.Count = 14;
			_flexH.Cols.Fixed = 1;
			_flexH.Rows.DefaultSize = 20;

			_flexH.Cols[0].Width = 50;

			// 1.선택
			_flexH.Cols[1].Name = "CHK";
			_flexH.Cols[1].DataType = typeof(string);
			_flexH.Cols[1].Width = 40;
			_flexH.Cols[1].Format = "Y;N";
			_flexH.Cols[1].ImageAlign = ImageAlignEnum.CenterCenter;

			// 2.수주번호
			_flexH.Cols[2].Name = "NO_SO";
			_flexH.Cols[2].DataType = typeof(string);
			_flexH.Cols[2].Width = 120;
//			_flexH.Cols[2].AllowEditing = false;
		
			// 3.수주일자
			_flexH.Cols[3].Name = "DT_SO";
			_flexH.Cols[3].DataType = typeof(string);
			_flexH.Cols[3].Width = 70;
			_flexH.Cols[3].EditMask = this.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT);
			_flexH.Cols[3].Format = _flexH.Cols[3].EditMask;
//			_flexH.Cols[3].AllowEditing = false;
			_flexH.SetStringFormatCol("DT_SO");
			_flexH.SetNoMaskSaveCol("DT_SO");

			// 4.수주처
			_flexH.Cols[4].Name = "LN_PARTNER";
			_flexH.Cols[4].DataType = typeof(string);
			_flexH.Cols[4].Width = 120;
//			_flexH.Cols[4].AllowEditing = false;

			// 5.수주형태
			_flexH.Cols[5].Name = "NM_SO";
			_flexH.Cols[5].DataType = typeof(string);
			_flexH.Cols[5].Width = 90;
//			_flexH.Cols[5].AllowEditing = false;

			// 6.화폐단위
			_flexH.Cols[6].Name = "NM_EXCH";
			_flexH.Cols[6].DataType = typeof(string);
			_flexH.Cols[6].Width = 65;
//			_flexH.Cols[6].AllowEditing = false;

			// 7.수주금액
			_flexH.Cols[7].Name = "AM_SO";
			_flexH.Cols[7].DataType = typeof(decimal);
			_flexH.Cols[7].Width = 120;
//			_flexH.Cols[7].AllowEditing = false;
			this.SetFormat(_flexH.Cols[7], DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.SELECT);

			// 8.영업그룹
			_flexH.Cols[8].Name = "NM_SALEGRP";
			_flexH.Cols[8].DataType = typeof(string);
			_flexH.Cols[8].Width = 100;
//			_flexH.Cols[8].AllowEditing = false;

			// 9.사업장
			_flexH.Cols[9].Name = "NM_BIZAREA";
			_flexH.Cols[9].DataType = typeof(string);
			_flexH.Cols[9].Width = 110;
//			_flexH.Cols[9].AllowEditing = false;

			// 10.사원명
			_flexH.Cols[10].Name = "NM_EMP";
			_flexH.Cols[10].DataType = typeof(string);
			_flexH.Cols[10].Width = 60;

			// 11.수주상태
			_flexH.Cols[11].Name = "STA_SO";
			_flexH.Cols[11].DataType = typeof(string);
			_flexH.Cols[11].Width = 70;

			// 12.VAT구분
			_flexH.Cols[12].Name = "TP_VAT";
			_flexH.Cols[12].DataType = typeof(string);
			_flexH.Cols[12].Width = 130;

			// 13.VAT포함
			_flexH.Cols[13].Name = "FG_VAT";
			_flexH.Cols[13].DataType = typeof(string);
			_flexH.Cols[13].Width = 70;

			_flexH.AllowSorting = AllowSortingEnum.None;
			_flexH.NewRowEditable = true;
			_flexH.EnterKeyAddRow = true;

			_flexH.SumPosition = SumPositionEnum.None;
			_flexH.GridStyle = GridStyleEnum.Green;

			this.SetUserGrid(_flexH);

			// 그리드 헤더캡션 표시하기
			for(int i = 0; i <= _flexH.Cols.Count-1; i++)
				_flexH[0, i] = GetDDItem(_flexH.Cols[i].Name);
			
			_flexH.Redraw = true;

			// 그리드 이벤트 선언
			_flexH.AfterRowColChange	+= new C1.Win.C1FlexGrid.RangeEventHandler(_flexH_AfterRowColChange);
			//_flexH.MouseDown			+= new System.Windows.Forms.MouseEventHandler(OnCheckBoxClick);
			_flexH.StartEdit			+= new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);
		}

		#endregion

		#region -> InitGridL

		private void InitGridL()
		{
			Application.DoEvents();
			
			_flexL.Redraw = false;

			_flexL.Rows.Count = 1;
			_flexL.Rows.Fixed = 1;
			_flexL.Cols.Count = 13;
			_flexL.Cols.Fixed = 1;
			_flexL.Rows.DefaultSize = 20;

			_flexL.Cols[0].Width = 50;

			// 1.선택
			_flexL.Cols[1].Name = "CHK";
			_flexL.Cols[1].DataType = typeof(string);
			_flexL.Cols[1].Width = 40;
			_flexL.Cols[1].Format = "Y;N";
			_flexL.Cols[1].ImageAlign = ImageAlignEnum.CenterCenter;
			
			// 2.항번
			_flexL.Cols[2].Name = "SEQ_SO";
			_flexL.Cols[2].DataType = typeof(string);
			_flexL.Cols[2].Width = 0;

			// 3.품목코드
			_flexL.Cols[3].Name = "CD_ITEM";
			_flexL.Cols[3].DataType = typeof(string);
			_flexL.Cols[3].Width = 80;
			
			// 4.품목명
			_flexL.Cols[4].Name = "NM_ITEM";
			_flexL.Cols[4].DataType = typeof(string);
			_flexL.Cols[4].Width = 110;
		
			// 5.규격
			_flexL.Cols[5].Name = "STND_ITEM";
			_flexL.Cols[5].DataType = typeof(string);
			_flexL.Cols[5].Width = 70;
		
			// 6.단위
			_flexL.Cols[6].Name = "UNIT_SO";
			_flexL.Cols[6].DataType = typeof(string);
			_flexL.Cols[6].Width = 70;
		
			// 7.수주수량
			_flexL.Cols[7].Name = "QT_SO";
			_flexL.Cols[7].DataType = typeof(decimal);
			_flexL.Cols[7].Width = 100;			
			this.SetFormat(_flexL.Cols[7], DataDictionaryTypes.SA, FormatTpType.QUANTITY, FormatFgType.SELECT);

			// 8.수주단가
			_flexL.Cols[8].Name = "UM_SO";
			_flexL.Cols[8].DataType = typeof(decimal);
			_flexL.Cols[8].Width = 100;			
			this.SetFormat(_flexL.Cols[8], DataDictionaryTypes.SA, FormatTpType.UNIT_COST, FormatFgType.SELECT);

			// 9.수주금액
			_flexL.Cols[9].Name = "AM_SO";
			_flexL.Cols[9].DataType = typeof(decimal);
			_flexL.Cols[9].Width = 120;			
			this.SetFormat(_flexL.Cols[9], DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.SELECT);

			// 10.공장
			_flexL.Cols[10].Name = "NM_PLANT";
			_flexL.Cols[10].DataType = typeof(string);
			_flexL.Cols[10].Width = 120;
			
			// 11.프로젝트
			_flexL.Cols[11].Name = "NO_PROJECT";
			_flexL.Cols[11].DataType = typeof(string);
			_flexL.Cols[11].Width = 100;

            // 12.처리상태
            _flexL.Cols[12].Name = "STA_SO";
            _flexL.Cols[12].DataType = typeof(string);
            _flexL.Cols[12].Width = 70;
			
			_flexL.AllowSorting   = AllowSortingEnum.None;
			_flexL.NewRowEditable = true;
			_flexL.EnterKeyAddRow = false;
			
			_flexL.SumPosition = SumPositionEnum.None;
			_flexL.GridStyle   = GridStyleEnum.Blue;

            _flexL.SetDummyColumn("CHK");
			
			this.SetUserGrid(_flexL);

			// 그리드 헤더캡션 표시하기
			for(int i = 0; i <= _flexL.Cols.Count-1; i++)
				_flexL[0, i] = GetDDItem(_flexL.Cols[i].Name);
			
			_flexL.Redraw = true;

			// 그리드 이벤트 선언
			//_flexL.MouseDown			+= new System.Windows.Forms.MouseEventHandler(OnCheckBoxClick);
			_flexL.StartEdit			+= new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);
		}

		#endregion

		#region -> InitControl

		private void InitControl()
		{
			try
			{
				//마스크 셋팅
				this.m_mskDtStart.Mask	= this.GetFormatDescription(DataDictionaryTypes.SA,FormatTpType.YEAR_MONTH_DAY,FormatFgType.INSERT);
				this.m_mskDtEnd.Mask	= this.GetFormatDescription(DataDictionaryTypes.SA,FormatTpType.YEAR_MONTH_DAY,FormatFgType.INSERT);
		
				//////////////
				// 라벨초기화
				/////////////
				foreach(Control ctr in this.Controls)
				{
					if(ctr is Duzon.Common.Controls.PanelExt)
					{
						foreach(Control ctrl in ((Duzon.Common.Controls.PanelExt)ctr).Controls)
						{
							if(ctrl is Duzon.Common.Controls.PanelExt)
							{
								foreach(Control ctrls in ((Duzon.Common.Controls.PanelExt)ctrl).Controls)
								{
									if(ctrls is System.Windows.Forms.Label)
										((System.Windows.Forms.Label)ctrls).Text = GetDataDictionaryItem(DataDictionaryTypes.SA, (string)((System.Windows.Forms.Label)ctrls).Tag);
								}
							}
						}
					}
				}

				m_btnConfirm.Text	= MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_btnConfirm.Tag);
				m_btnUnConfirm.Text = MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_btnUnConfirm.Tag);
				m_btnCloseH.Text	= MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_btnCloseH.Tag);
				m_rdoSo.Text		= MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_rdoSo.Tag);
				m_rdoReturn.Text	= MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_rdoReturn.Tag);
				m_btnSelect.Text	= MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_btnSelect.Tag);
				m_btnDel.Text		= MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_btnDel.Tag);
				m_btnCloseL.Text	= MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_btnCloseL.Tag);
				m_btnReleaseL.Text	= MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_btnReleaseL.Tag);
				m_btnChkAtp.Text	= MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.SA, (string)m_btnChkAtp.Tag);

				//////////////	
				// 콤보초기화
				//////////////
				g_dsCombo = new DataSet();

				g_dsCombo = this.GetComboData("S;SA_B000016", "N;SA_B000016", "N;MA_CODEDTL_005;MA_B000040");

				// 화패단위
				m_cboStaSo.DataSource		= g_dsCombo.Tables[0];
				m_cboStaSo.DisplayMember	= "NAME";
				m_cboStaSo.ValueMember		= "CODE";

				DataTable _dt_STA_SO = g_dsCombo.Tables[1];
				
				_flexH.SetDataMap("STA_SO", _dt_STA_SO, "CODE", "NAME");
                _flexL.SetDataMap("STA_SO", _dt_STA_SO, "CODE", "NAME");

				DataTable _dt_TP_VAT = g_dsCombo.Tables[2];

				_flexH.SetDataMap("TP_VAT", _dt_TP_VAT, "CODE", "NAME");

				this.m_mskDtStart.Text	= MainFrameInterface.GetStringFirstDayInMonth;
				this.m_mskDtEnd.Text	= MainFrameInterface.GetStringToday;
				bpNoEmp.CodeValue	= MainFrameInterface.LoginInfo.EmployeeNo;
				bpNoEmp.CodeName		= MainFrameInterface.LoginInfo.EmployeeName;
			}
			catch(Exception ex)
			{
				throw ex;
			}			
		}

		#endregion

		#region -> Page_Paint

		private void Page_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if(this._isPainted)
				return;

			try
			{
				this._isPainted = true;
				Application.DoEvents();
				
				this.Visible = true;
				Application.DoEvents();
			
				SetProgressBarValue(100, 65);
				Application.DoEvents();
				
				SetProgressBarValue(100, 100);

				this.Enabled = true; //페이지 전체 활성
				
				m_mskDtStart.Focus();

				//20040406
				m_btnCloseH.Enabled = false;// 오더종결
				m_btnCloseL.Enabled = false;// 종결
				m_btnReleaseL.Enabled = false;// 종결취소
				m_btnChkAtp.Enabled = false;// APT체크

			}
			catch(Exception ex)
			{
				MsgEnd(ex);
			}
			finally
			{				
				this.SetToolBarButtonState(true,true, true, false,false);				
			}
		}

		#endregion

		#endregion

		#region ♣ 저장관련

		#region -> IsChanged

		private bool IsChanged(string gubun)
		{
			try
			{
				if(gubun == null)
					return _flexL.IsDataChanged;

				return false;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		#endregion

		#region -> MsgAndSave

		private bool MsgAndSave(bool displayDialog, bool isExit)
		{
			if(!IsChanged(null)) return true;
			
			bool isSaved = false;

			if(!displayDialog)								// 저장 버튼을 클릭한 경우이므로 다이알로그는 필요없음
			{
				if(IsChanged(null)) isSaved = Save();
				
				return isSaved;
			}

			DialogResult result;

			if(isExit)
			{
				// 변경된 내용이 있습니다. 저장하시겠습니까?
				result = this.ShowMessage("QY3_002");
				if(result == DialogResult.No)
					return true;
				if(result == DialogResult.Cancel)
					return false;
			}
			else
			{
				// 변경된 내용이 있습니다. 저장하시겠습니까?
				result = this.ShowMessage("QY2_001");
				if(result == DialogResult.No)
					return true;
			}

			Application.DoEvents();		// 대화상자 즉시 사라지게

			// "예"를 선택한 경우
			if(IsChanged(null)) isSaved = Save();

			return isSaved;
		}

		#endregion

		#region -> Save

		private bool Save()
		{
			ResultData ret = (ResultData)this.ExecSp("SP_SA_SOL_DELETE", m_obj_DEL);
			if(ret.Result)
			{
				_flexL.DataTable.AcceptChanges();

				g_dtDelete.Clear();

				return true;
			}
			return false;
		}

		#endregion

		#endregion
		
		#region ♣ 메인버튼 이벤트 / 메소드

		#region -> DoContinue

		private bool DoContinue()
		{
			if(_flexL.Editor != null)
			{
				return _flexL.FinishEditing(false);
			}
			
			return true;
		}

		#endregion

		#region -> 조회조건체크

		private bool SearchCondition()
		{
			//			if(this.m_cboCdPlant.SelectedValue.ToString() == "")
			//			{
			//				this.ShowMessage("WK1_004", GetDDItem("CD_PLANT"));
			//				this.m_cboCdPlant.Focus();
			//				return false;
			//			}
			//
			//			if(m_txtCdItem.Text == "")
			//			{
			//				this.ShowMessage("WK1_004", GetDDItem("CD_ITEM"));
			//				this.m_txtCdItem.Focus();
			//				return false;
			//			}

			return true;
		}

		#endregion

		#region -> 조회버튼클릭

		// 브라우저의 조회 버턴이 클릭될때 처리 부분
		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			if(!DoContinue())
				return;
			
			try
			{			
				if(!MsgAndSave(true,false))
					return;

				string ls_So;

				// 수주, 반품 여부
				if(m_rdoSo.Checked == true)
					ls_So = "N";
				else
					ls_So = "Y";

				ResultData result = (ResultData)this.FillDataSet("SP_SA_SOMNG_SELECT_H", new object[]{LoginInfo.CompanyCode,	
																										 m_mskDtStart.Text, 
																										 m_mskDtEnd.Text,	
																										 bpSalegrp.CodeValue,
																										 bpNoEmp.CodeValue,	
																										 bpSoPartner.CodeValue, 
																										 bpTpSo.CodeValue,	
																										 m_cboStaSo.SelectedValue,
																										 ls_So });
				g_dsSoMng_H = (DataSet)result.DataValue;

				
				ResultData result_1 = (ResultData)this.FillDataSet("SP_SA_SOMNG_SELECT_L", new object[]{ LoginInfo.CompanyCode,	
																										   m_mskDtStart.Text, 
																										   m_mskDtEnd.Text,	
																										   bpSalegrp.CodeValue,
																										   bpNoEmp.CodeValue,	
																										   bpSoPartner.CodeValue, 
																										   bpTpSo.CodeValue,	
																										   m_cboStaSo.SelectedValue,
																										   ls_So });
				g_dsSoMng_L = (DataSet)result_1.DataValue;

				// Detail 바인딩
				_flexL.Redraw = false;
				_flexL.BindingStart();
				_flexL.DataSource = g_dsSoMng_L.Tables[0].DefaultView;
				_flexL.BindingEnd();
				_flexL.EmptyRowFilter();	// 처음에 아무것도 안 보이게
				_flexL.Redraw = true;

				// Master 바인딩
				_flexH.Redraw = false;
				_flexH.BindingStart();
				_flexH.DataSource = g_dsSoMng_H.Tables[0].DefaultView;	// 요기에서 곧바로 AfterRowColChange 이벤트 발생
				_flexH.BindingEnd();

				if(_flexH.HasNormalRow)		// 처음 조회시 강제로 AfterRowColChange 메소드 호출
				{
					int row = _flexH.Row;

					_flexH.Row = -1;
					_flexH.Row = row;
				}

				_flexH.Redraw = true;	

				if(!_flexH.HasNormalRow)
				{					
					// 검색된 내용이 존재하지 않습니다..
					this.ShowMessage("IK1_003");
				}
			}
			catch(Exception ex)
			{	
				MsgEnd(ex);
			}
			finally
			{
				ToolBarSearchButtonEnabled = true;
				ToolBarPrintButtonEnabled = true;
			
				// 20040406
				m_btnCloseH.Enabled = true;// 오더종결
				m_btnCloseL.Enabled = true;// 종결
				m_btnReleaseL.Enabled = true;// 종결취소
                m_btnChkAtp.Enabled = false;// APT체크
			}
		}

		#endregion

		#region -> 추가버튼클릭

		// 브라우저의 추가 버턴이 클릭될때 처리 부분
		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{		
			if(!DoContinue())
				return;
		}

		#endregion

		#region -> 삭제버튼클릭

		// 브라우저의 삭제 버턴이 클릭될때 처리 부분
		public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{
			if(!DoContinue())
				return;

			string ls_Ret;

			//반품여부
			if(m_rdoReturn.Checked == true)
				ls_Ret = "Y";
			else
				ls_Ret = "N";


			DataRow[] ldra = _flexH.DataTable.Select("CHK = 'Y' AND CONF = 'N' AND GIR = 'Y' AND STA_SO = 'O'");//CONF수정
			DataRow[] ldrb = _flexH.DataTable.Select("CHK = 'Y' AND CONF = 'N' AND GIR = 'N' AND STA_SO = 'O'");//CONF수정
			DataRow[] ldrc = _flexH.DataTable.Select("CHK = 'Y' AND CONF = 'Y' AND GIR = 'Y' AND STA_SO = 'R'");//CONF수정
			DataRow[] ldrd = _flexH.DataTable.Select("CHK = 'Y' AND CONF = 'Y' AND GIR = 'N' AND STA_SO = 'R'");//CONF수정

			DataRow[] ldrchk = _flexH.DataTable.Select("CHK = 'Y'");

			if(ldrchk.Length == 0)
			{
				//선택된 자료가 없습니다.
				this.ShowMessage("SA_M000092", "IK1");
				return;
			}

			if(	(ldra.Length + ldrb.Length + ldrc.Length + ldrd.Length) == 0 ||
				(ldra.Length + ldrb.Length + ldrc.Length + ldrd.Length) != ldrchk.Length )
			{
				//이미 확정된 데이터가 포함되어 있습니다.
				this.ShowMessage("SA_M000090", "IK1");
				return;
			}

			try
			{
				DialogResult ldr_result = ShowMessageBox(1003, PageName);

				if(ldr_result == DialogResult.Yes)
				{					
					if( ldrchk != null && ldrchk.Length > 0)
					{
						for(int r = _flexH.Rows.Count-1;r >= _flexH.Rows.Fixed; r--)
						{
							if(_flexH[r,"CHK"].ToString() == "Y")
							{							
								for(int rowL = _flexL.Rows.Count -1; rowL > 0; rowL--)
								{
									if(_flexL[_flexL.Row, "NO_SO"].ToString() == _flexH[_flexH.Row, "NO_SO"].ToString())
										_flexL.Rows.Remove(rowL);
								}

                                _flexH.Rows.Remove(r);
							}							
						}	
					}
			
					DataTable g_dtChangeH = _flexH.GetChanges();

					Duzon.Common.Util.SpInfo si = new Duzon.Common.Util.SpInfo();
					si.DataValue					= g_dtChangeH;								// 저장할 데이터 테이블				
					si.SpNameDelete					= "SP_SA_SOH_SO_DELETE";					// Delete 프로시저명					
					si.SpParamsDelete				= new string[] {"NO_SO","CD_COMPANY", "STA_SO", "CONF", "GIR", "RET", "NO_NEGO"};

					Duzon.Common.Util.ResultData ret = (Duzon.Common.Util.ResultData)this.Save(si);

					if(ret.Result)
					{		
						_flexH.DataTable.AcceptChanges();
						_flexL.DataTable.AcceptChanges();

						ShowMessage("IK1_002");	
					}
				}
			}
			catch(Exception ex)
			{
				MsgEnd(ex);
			}			
		}

		#endregion

		#region -> 저장버튼클릭

		// 브라우저의 저장 버턴이 클릭될때 처리 부분
		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			if(!DoContinue())
				return;			

			try
			{				
				if(MsgAndSave(false, false))
				{
					this.ShowMessageBox(1);		// 저장되었습니다.
				}
			}
			catch(Exception ex)
			{
				MsgEnd(ex);
			}
		}

		#endregion

		#region -> 인쇄버튼클릭

		public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
		{
			if(!DoContinue())
				return;
		}

		#endregion

		#region -> 종료버튼클릭

		// 브라우저의 닫기 버턴이 클릭될때 처리 부분
		public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
		{
			if(!DoContinue())
				return false;

			try
			{
				if(!MsgAndSave(true,true))	// 저장이 실패하면
					return false;			// 창 닫지 않음
			}
			catch(Exception ex)
			{
				MsgEnd(ex);
			}
			return true;
		}

		#endregion

		#endregion

		#region ♣ 그리드 이벤트 / 메소드

		#region -> _flexH_AfterRowColChange

		private void _flexH_AfterRowColChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
		{
			if(!_flexH.IsBindingEnd || !_flexH.HasNormalRow)
			{
				_flexL.EmptyRowFilter();
				return;
			}

			if(_flexL.DataSource != null && e.OldRange.r1 != e.NewRange.r1)
			{
				string filter = "NO_SO = '" + _flexH[_flexH.Row,"NO_SO"].ToString() + "'";
				_flexL.DataView.RowFilter  = filter;
			}
		}
		
		#endregion

		#region -> _flex_StartEdit
		private void _flex_StartEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
		{
			try
			{
				Dass.FlexGrid.FlexGrid flex = (Dass.FlexGrid.FlexGrid)sender;

				if( flex.Cols[e.Col].Name != "CHK")
				{
					e.Cancel = true;	// 셀 입력상태로 못 들어가게

				}				
			}
			finally
			{
			}
		}
		#endregion

		#endregion

		#region ♣ 도움창 이벤트 / 메소드

		private void OnHelpFormOpen(object sender, System.EventArgs e)
		{
			if(Duzon.Common.Forms.BasicInfo.ActiveDialog)
				return;

			ShowHelp(sender, "");			
		}
		
		#endregion

		#region ♣ 기타 이벤트 / 메소드

		#region -> OnControlValidating

		private void OnControlValidating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if(sender.GetType().Name == "CodeTextBox")
			{
				if(((CodeTextBox)sender).IsConfirmed == false)
				{
					switch(((CodeTextBox)sender).Name)
					{
							// 영업그룹
						case "bpSalegrp" :
							
							if(bpSalegrp.CodeValue == "")
							{
								bpSalegrp.CodeName = "";
								
								return;
							}
							
							//CheckSearchOption(10, 1);
							break;

							//담당자
						case "bpNoEmp" :
							if(bpNoEmp.CodeValue == "")
							{
								bpNoEmp.CodeName = "";
								
								return;
							}
							
							//CheckSearchOption(10, 2);	
							break;

							//수주처
						case "bpSoPartner" :
							if(bpSoPartner.CodeValue == "")
							{
								bpSoPartner.CodeName = "";
								
								return;
							}

							//CheckSearchOption(10, 3);
							break;

							//수주유형
						case "bpTpSo":
							if(bpTpSo.CodeValue == "")
							{
								bpTpSo.CodeName = "";
								
								return;
							}
							//CheckSearchOption(10, 4);
							
							break;
					}
				}
			}
		}

		#endregion

		#region -> m_btnSelect_Click

		private void m_btnSelect_Click(object sender, System.EventArgs e)
		{
			if(_flexL.DataView == null)
				return;

			try
			{
				switch(_flexL[1,"CHK"].ToString())
				{
					case "Y" :
						for(int i = 1 ; i < _flexL.Rows.Count; i++)
						{
							_flexL[i,"CHK"] = "N";
							_flexL.SetCellCheck(i, 1, CheckEnum.Unchecked);
						}
						break;

					case "N" :
						for(int i = 1 ; i < _flexL.Rows.Count; i++)
						{
							_flexL[i,"CHK"] = "Y";
							_flexL.SetCellCheck(i, 1, CheckEnum.Checked);
						}
						break;
				}
			}
			catch(Exception ex)
			{
				MsgEnd(ex);
			}
		}

		#endregion

		#region -> OnControlKeyDown

		private void OnControlKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{			
			switch(e.KeyData)
			{
				case Keys.Enter :
					
					SendKeys.SendWait("{TAB}");	
					break;
				
					// 도움창 띄우기
				case Keys.F3 :
				switch(((Control)sender).Name)
				{
					case "bpSalegrp":
					case "bpNoEmp":
					case "bpSoPartner":
					case "bpTpSo":						
					ShowHelp(sender, "");
					break;
						
				}
				break;
			}			
		}

		#endregion


		#region -> OnHelp_Click(프리폼)

		/// <summary>
		///  각 도움창 클릭 이벤트
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">e</param>
		private void OnHelp_Click(object sender, System.EventArgs e)
		{
			if(Duzon.Common.Forms.BasicInfo.ActiveDialog)
				return;

			ShowHelp(sender, "");
		}

		/// <summary>
		/// 각 항목에 맞는 도움창 띄우기
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="ps_search">검색조건</param>
		private void ShowHelp(object sender, string ps_search)
		{
			object dlg = null;

			switch(((Control)sender).Name)
			{
					// 영업그룹
				case "bpSalegrp" :
				case "m_btnSalegrp" :
					dlg = this.LoadHelpWindow("P_MA_SALEGRP_SUB", new object[] {MainFrameInterface, ps_search});
					if(((Duzon.Common.Forms.BaseSearchHelp)dlg).ShowDialog() == DialogResult.OK)
					{					
						if(dlg is IHelpWindow)
						{
							bpSalegrp.CodeValue = (((IHelpWindow)dlg).ReturnValues)[2].ToString();
							bpSalegrp.CodeName = (((IHelpWindow)dlg).ReturnValues)[1].ToString();
							
						}
					}
					break;
					
					//담당자
				case "bpNoEmp":
				case "m_btnEmp":
					dlg = LoadHelpWindow("P_MA_EMP_SUB", new object[]{MainFrameInterface, ps_search});

					if(((Duzon.Common.Forms.BaseSearchHelp)dlg).ShowDialog() == DialogResult.OK)
					{		
						if(dlg is IHelpWindow)
						{	
							bpNoEmp.CodeValue = (((IHelpWindow)dlg).ReturnValues)[0].ToString();
							bpNoEmp.CodeName = (((IHelpWindow)dlg).ReturnValues)[1].ToString();
							
						}
					}
					break;

					//수주유형
				case "bpTpSo":
				case "m_btnTpSo":
					sale.P_SA_TPSO_SUB dlg_SA_TPSO_SUB = new sale.P_SA_TPSO_SUB(MainFrameInterface, "", "Y");
					
					if(dlg_SA_TPSO_SUB.ShowDialog() == DialogResult.OK)
					{
						bpTpSo.CodeValue = dlg_SA_TPSO_SUB.ReturnValues[0].ToString();
						bpTpSo.CodeName = dlg_SA_TPSO_SUB.ReturnValues[1].ToString();
						
					}
					break;

					//수주처
				case "bpSoPartner":
				case "m_btnPartner":
//					dlg = MainFrameInterface.LoadHelpWindow("P_MA_PARTNER_SUB", new object[] {MainFrameInterface, null, bpSoPartner.CodeValue});
//					if(((Duzon.Common.Forms.BaseSearchHelp)dlg).ShowDialog() == DialogResult.OK)
//					{	
//						if(dlg is IHelpWindow)
//						{							
//							bpSoPartner.CodeValue = (((IHelpWindow)dlg).ReturnValues)[0].ToString();
//							bpSoPartner.CodeName = (((IHelpWindow)dlg).ReturnValues)[1].ToString();
//							bpSoPartner.IsConfirmed = true;
//						}
//					}
//					break;	
			

					dlg = LoadHelpWindow("P_SA_TPPTR_SUB", new object[]{MainFrameInterface, "002", ps_search});
					if(((Duzon.Common.Forms.BaseSearchHelp)dlg).ShowDialog() == DialogResult.OK)
					{		
						if(dlg is IHelpWindow)
						{	
							this.bpSoPartner.CodeValue = (((IHelpWindow)dlg).ReturnValues)[1].ToString();
							this.bpSoPartner.CodeName = (((IHelpWindow)dlg).ReturnValues)[2].ToString();
							
							this.bpSoPartner.Focus();
						}
					}
					break;
			}

		}

		#endregion

		///******************************///
		///******* 상단 버튼 이벤트 ******///
		///******************************///
		
		#region -> ConfirmSo(수주확정)

		private void ConfirmSo(object sender, System.EventArgs e)
		{
			object[] lobj_parms, args;
			int li_chk;

			try
			{
				if(!DoContinue())
					return;

				if(_flexH.DataView == null)
					return;

				if(_flexH.DataTable.Select("CHK = 'Y'").Length == 0)
				{
					//선택된 자료가 없습니다.
					this.ShowMessage("SA_M000092", "IK1");
					return;
				}

				// 계획이외에 것이 포함되면 리턴
				DataRow[] ldr_row = _flexH.DataTable.Select("CHK = 'Y' AND STA_SO <> 'O'");

				if(ldr_row.Length > 0)
				{
					//수주상태에 계획 이외의 것이 포함되어 있어 확정할 수 없습니다.
					this.ShowMessage("SA_M000082", "EK1");
					return;
				}

				//확정처리 하시겠습니까 ?
				DialogResult ldr_result = this.ShowMessage("SA_M000075", "QK2");

				if(ldr_result == DialogResult.OK)
				{
					string ls_filter_so = "";
					
					for(int rowH = 1 ;  rowH < _flexH.DataView.Count + 1 ;rowH++)//(int rowH = _flexH.Rows.Count; rowH > 0 ; rowH--)
					{
						// A타입
						if(_flexH[rowH, "CHK"].ToString() == "Y" && _flexH[rowH, "CONF"].ToString() == "N" && _flexH[rowH, "GIR"].ToString() == "Y")//CONF수정
						{
							_flexH[rowH, "STA_SO"]		= "R";
							_flexH[rowH, "YN_CONFIRM"]	= "Y";
							
							if(ls_filter_so == "")
								ls_filter_so = "NO_SO = '" + _flexH[rowH, "NO_SO"].ToString() + "'";
							else
								ls_filter_so += "OR NO_SO = '" + _flexH[rowH, "NO_SO"].ToString() + "'";
							for(int rowL = _flexL.Rows.Count -1; rowL > 0; rowL--)
							{
								if(_flexL[rowL, "NO_SO"].ToString() == _flexH[rowH, "NO_SO"].ToString())
								{
									_flexL[rowL, "STA_SO"] = "R";
									//_flexL[rowL, "NM_SYSDEF"] = "확정";
								}
							}
						}
							// B타입
						else if(_flexH[rowH, "CHK"].ToString() == "Y" && (_flexH[rowH, "CONF"].ToString() != "N" || _flexH[rowH, "GIR"].ToString() != "Y"))//CONF수정
						{
							_flexH[rowH, "STA_SO"]		= "R";
							_flexH[rowH, "YN_CONFIRM"]	= "Y";

							if(ls_filter_so == "")
								ls_filter_so = "NO_SO = '" + _flexH[rowH, "NO_SO"].ToString() + "'";
							else
								ls_filter_so += "OR NO_SO = '" + _flexH[rowH, "NO_SO"].ToString() + "'";

							for(int rowL = _flexL.Rows.Count -1; rowL > 0; rowL--)
							{
								if(_flexL[rowL, "NO_SO"].ToString() == _flexH[rowH, "NO_SO"].ToString())
								{
									_flexL[rowL, "STA_SO"]		= "R";
									//_flexL[rowL, "NM_SYSDEF"]	= "의뢰";
									_flexL[rowL, "QT_GIR"]		= _flexL[rowL, "QT_SO"];
								}
							}
						}
					}
					DataRow[] ldr = _flexH.DataTable.Select("CHK = 'Y'", "", DataViewRowState.CurrentRows);

					//헤더의 NO_SO와 같은 NO_SO만 추출
					DataRow[] ldr_line = _flexL.DataTable.Select(ls_filter_so, "CD_PLANT, CD_SL");

					string ls_date = MainFrameInterface.GetStringDetailToday;

					for(int row = 0; row < ldr.Length; row++)
                    {
                        #region -> 여신체크로직 -> 임시 주석처리함 -> 주석된 부분 지우면 않됨
                        //거래처정보(MA_PARTNER)에 여신관리여부가 "Y"일 경우 여신체크를 한다.
                        if(ldr[row]["FG_CREDIT"].ToString() == "Y")
                        {
                            li_chk = 0;

                            SpInfo si = new SpInfo();
                            si.SpNameSelect = "UP_SA_CHECKCREDIT_SELECT";
                            si.SpParamsSelect = new object[] { ldr[row]["CD_COMPANY"]
                                                        , ldr[row]["CD_PARTNER"]
                                                        , "001"
                                                        , ldr[row]["AM_SO"]
                                                        , ls_date.Substring(0, 8) };

                            ResultData result = (ResultData)this.FillDataTable(si);
                            DataTable dtReturn = (DataTable)result.DataValue;

                            li_chk = Convert.ToInt32(dtReturn.Rows[0][0].ToString());

                            // 결과값
                            switch (li_chk)
                            {
                                // 통제안함(통과)
                                case 0:
                                    break;

                                // 경고(Warning)
                                case 1:
                                    //여신한도를 초과합니다. 계속하시겠습니까 ?
                                    if (this.ShowMessage("SA_M000023", "QY2") == DialogResult.No)
                                        return;
                                    break;

                                // 오류(Error)
                                case 2:
                                    //여신한도를 초과했습니다.
                                    this.ShowMessage("SA_M000024", "IK1");
                                    return;
                            }
                        }
                        #endregion
                    }

					DataTable ldt_src = MakeHeadTable(ldr);
					DataTable ldt_line = MakeLineTable(ldr_line);

                    // 수주확정 코드(CC 단을 프로시져로 뺀것) 시작---------------------------------------------

                    SpInfoCollection sic = new SpInfoCollection();
                    SpInfo siH = new SpInfo();
                    SpInfo siL = new SpInfo();

                    siH.DataValue = ldt_src;
                    siH.SpNameInsert = "UP_SA_SOH_CONFIRM_UPDATE";
                    siH.SpParamsInsert = new string[] { 
                        "NO_SO", "CD_COMPANY", "NO_HST", "CD_BIZAREA", "DT_SO", 
                        "CD_PARTNER", "GI_PARTNER", "SO_PARTNER", "BILL_PARTNER", "CD_EXCH", 
                        "RT_EXCH", "AM_SO", "AM_WONAMT", "CD_SALEGRP", "NO_EMP", 
                        "TP_SO", "TP_PRICE", "AM_VAT", "TP_VAT", "FG_VAT", 
                        "FG_TRANSPORT", "FG_TAXP", "FG_BILL", "NO_PROJECT", "NO_NEGO", 
                        "STA_SO", "YN_CONFIRM", "DC_RMK", "ID_INSERT", "DTS_INSERT", 
                        "ID_UPDATE", "DTS_UPDATE", "CONF", "GIR" };

                    sic.Add(siH);

                    siL.DataValue = ldt_line;
                    siL.SpNameInsert = "UP_SA_SOL_CONFIRM_UPDATE";
                    siL.SpParamsInsert = new string[] {
                        "NO_SO", "CD_COMPANY", "SEQ_SO", "NO_HST", "CD_ITEM", 
                        "TP_ITEM", "CD_PLANT", "CD_SL", "DT_DUEDATE", "DT_REQGI", 
                        "QT_SO", "UM_SO", "AM_SO", "UNIT_SO", "AM_WONAMT", 
                        "QT_IM", "UNIT_IM", "TP_VAT", "QT_GIR", "QT_GI",
                        "QT_IV", "QT_LC", "QT_RETURN", "NO_PROJECT", "AM_VAT",
                        "RT_VAT", "FG_VAT", "TP_GI", "TP_IV", "TP_BUSI",
                        "NO_TRACKING", "NO_RELATION", "SEQ_RELATION", "CONF", "GIR",
                        "GI", "IV", "CM", "TRADE", "RET", 
                        "SUBCONT", "STA_SO", "DTS_INSERT", "ID_INSERT", "DTS_UPDATE", 
                        "ID_UPDATE", "FG_SQC", "STA", "NO_IOLINE_MGMT", "NO_IO_MGMT", 
                        "NO_SOLINE_MGMT", "NO_SO_MGMT" };

                    sic.Add(siL);

                    ResultData[] rs = (ResultData[])Save(sic);

                    if (rs[0].Result && rs[1].Result)
                    {
                        _flexH.DataTable.AcceptChanges();
                        _flexL.DataTable.AcceptChanges();

                        // 건이 확정처리 되었습니다.
                        MessageBoxEx.Show(ldr.Length.ToString() + GetMessageDictionaryItem("SA_M000076"), PageName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                    // 수주확정 코드(CC 단을 프로시져로 뺀것) 끝-----------------------------------------------

                    //int li_result  = (int)InvokeRemoteMethod("SaleOrder", "sale.CC_SA_SO", "CC_SA_SO.rem", "ConfirmSo", new object[]{ldt_src, ldt_line});

                    //_flexH.DataTable.AcceptChanges();
                    //_flexL.DataTable.AcceptChanges();

                    //// 건이 확정처리 되었습니다.
                    //MessageBoxEx.Show(ldr.Length.ToString() + GetMessageDictionaryItem("SA_M000076"), PageName, MessageBoxButtons.OK, MessageBoxIcon.Information);

				}

			}
			catch(Exception ex)
			{
				MsgEnd(ex);
			}			
		}

		#endregion

		#region -> UnConfirmSo(확정처리취소)

		private void UnConfirmSo(object sender, System.EventArgs e)
		{
			try
			{
				if(!DoContinue())
					return;

				if(_flexH.DataView == null || _flexH.DataView.Count == 0)
					return;

				// 확정이외에 것이 포함되면 리턴
				DataRow[] ldr_chk	= _flexH.DataTable.Select("CHK = 'Y'");
				DataRow[] ldr_a		= _flexH.DataTable.Select("CHK = 'Y' AND CONF = 'N' AND GIR = 'Y' AND STA_SO = 'R'");//CONF수정
				DataRow[] ldr_b		= _flexH.DataTable.Select("CHK = 'Y' AND CONF = 'N' AND GIR = 'N' AND STA_SO = 'R'");//CONF수정

				if(ldr_chk.Length == 0)
				{
					//선택된 자료가 없습니다.
					this.ShowMessage("SA_M000092", "IK1");
					return;
				}

				if((ldr_a.Length + ldr_b.Length) == 0 || (ldr_a.Length + ldr_b.Length != ldr_chk.Length))
				{
					//수주상태에 확정 이외의 것이 포함되어 있어 확정 취소가 불가능합니다.
					this.ShowMessage("SA_M000083", "IK1");
					return;
				}
				
				//확정 취소하시겠습니까 ?
				DialogResult ldr_result = this.ShowMessage("SA_M000081", "QK2");

				if(ldr_result == DialogResult.OK)
				{					
					string ls_filter_so = "";

					for(int rowH = 1 ;  rowH < _flexH.DataView.Count + 1 ;rowH++)
					{
						if(_flexH[rowH, "CHK"].ToString() == "Y")
						{
							_flexH[rowH, "STA_SO"] = "O"; 
							
							if(ls_filter_so == "")
								ls_filter_so = "NO_SO = '" + _flexH[rowH, "NO_SO"].ToString() + "'";
							else
								ls_filter_so += "OR NO_SO = '" + _flexH[rowH, "NO_SO"].ToString() + "'";

							for(int rowL = _flexL.Rows.Count -1; rowL > 0; rowL--)
							{
								if(_flexL[rowL, "NO_SO"].ToString() == _flexH[rowH, "NO_SO"].ToString())
								{
									_flexL[rowL, "STA_SO"]		= "O";
									//_flexL[rowL, "NM_SYSDEF"]	= "계획";
								}
							}
						}
					}
					//헤더의 NO_SO와 같은 NO_SO만 추출
					DataRow[] ldr_line	= _flexL.DataTable.Select(ls_filter_so,"CD_PLANT,CD_SL");
					DataRow[] ldr		= _flexH.DataTable.Select("CHK = 'Y'","",DataViewRowState.ModifiedCurrent);

					if(ldr.Length == 0)
						return;

					DataTable ldt_src  = MakeHeadTable(ldr);
					DataTable ldt_line = MakeLineTable(ldr_line);

                    //int li_result  = (int)InvokeRemoteMethod("SaleOrder","sale.CC_SA_SO","CC_SA_SO.rem","UnConfirmSo",new object[]{ldt_line});
                    int li_result = UnConfirmSoT(ldt_line);

					_flexH.DataTable.AcceptChanges();
					_flexL.DataTable.AcceptChanges();

					// 건이 확정취소 되었습니다.
					MessageBoxEx.Show(ldr.Length.ToString() + GetMessageDictionaryItem("SA_M000084"), PageName, MessageBoxButtons.OK, MessageBoxIcon.Information);

				}

			}
			catch(Exception ex)
			{
				MsgEnd(ex);
			}
		}

        public int UnConfirmSoT(DataTable pdt_table)
        {
            try
            {
                SpInfo si;
                int iCount = 0;
                for (int row = 0; row < pdt_table.Rows.Count; row++)
                {
                    si = new SpInfo();
                    si.SpNameSelect = "UP_SA_UNCONFIRMSO_UPDATE";
                    si.SpParamsSelect = new object[]{   pdt_table.Rows[row]["CD_COMPANY"], 
                                                pdt_table.Rows[row]["NO_SO"], 
                                                pdt_table.Rows[row]["SEQ_SO"], 
                                                pdt_table.Rows[row]["STA_SO"] };
                    ResultData result = (ResultData)this.FillDataTable(si);
                    DataTable dtReturn = (DataTable)result.DataValue;
                    if (result.Result)
                    {
                        iCount++;
                    }
                }
                return iCount;

                //StringBuilder ls_sql = new StringBuilder("");
                //string[] lsa_args = new string[17];
                //try
                //{
                //    for (int row = 0; row < pdt_table.Rows.Count; row++)
                //    {
                //        lsa_args[0] = pdt_table.Rows[row]["STA_SO"].ToString();
                //        lsa_args[1] = pdt_table.Rows[row]["CD_COMPANY"].ToString();
                //        lsa_args[2] = pdt_table.Rows[row]["NO_SO"].ToString();
                //        lsa_args[3] = pdt_table.Rows[row]["STA_SO"].ToString();
                //        lsa_args[4] = pdt_table.Rows[row]["CD_COMPANY"].ToString();
                //        lsa_args[5] = pdt_table.Rows[row]["NO_SO"].ToString();
                //        lsa_args[6] = pdt_table.Rows[row]["SEQ_SO"].ToString();
                //        lsa_args[7] = pdt_table.Rows[row]["CD_COMPANY"].ToString();
                //        lsa_args[8] = pdt_table.Rows[row]["NO_SO"].ToString();
                //        lsa_args[9] = pdt_table.Rows[row]["SEQ_SO"].ToString();
                //        lsa_args[10] = pdt_table.Rows[row]["CD_COMPANY"].ToString();
                //        lsa_args[11] = pdt_table.Rows[row]["CD_COMPANY"].ToString();
                //        lsa_args[12] = pdt_table.Rows[row]["NO_SO"].ToString();
                //        lsa_args[13] = pdt_table.Rows[row]["SEQ_SO"].ToString();
                //        lsa_args[14] = pdt_table.Rows[row]["CD_COMPANY"].ToString();
                //        lsa_args[15] = pdt_table.Rows[row]["NO_SO"].ToString();
                //        lsa_args[16] = pdt_table.Rows[row]["SEQ_SO"].ToString();
                //        ls_sql.Append("\n");
                //        ls_sql.Append(SA_DBScript.GetScript("CC_SA_SO053", lsa_args));
                //    }
                //    return this.ExecuteNonQuery(ls_sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

		#endregion

		#region -> CloseSoHead(오더종결처리)

		private void CloseSoHead(object sender, System.EventArgs e)
		{
			if(!DoContinue())
				return;
			try
			{				
				if(_flexH.DataView == null || _flexH.DataView.Count == 0)
					return;

				DataRow[] ldr_row  = _flexH.DataTable.Select("CHK = 'Y' AND STA_SO = 'C'");
				DataRow[] ldr_row2 = _flexH.DataTable.Select("CHK = 'Y' AND STA_SO = 'O'");
				DataRow[] ldr_row3 = _flexH.DataTable.Select("CHK = 'Y' AND STA_SO = 'R'");
                DataRow[] drCHK = _flexH.DataTable.Select(" CHK = 'Y' AND STA_SO <> 'C' ");

				if(ldr_row.Length > 0)
				{
					//수주상태에 이미 종결건이 포함되어 있습니다.
					this.ShowMessage("SA_M000085", "IK1");
					return;
				}

				if(ldr_row2.Length > 0)
				{
                    this.ShowMessage("승인이 되지 않은 오더는 종결처리가 불가능합니다.");
					return;
				}

                if (drCHK.Length == 0)
                {
                    this.ShowMessage("체크된 헤더의 오더상태가 모두 '마감'이거나 체크된 라인이 없습니다.");
                    return;
                }

				//오더 종결 하시겠습니까 ?
				DialogResult ldr_result = this.ShowMessage("SA_M000077", "QK2");

                if (ldr_result == DialogResult.OK)
                {
                    DataTable ldt_src = MakeHeadTable(drCHK);

                    //int li_result = (int)this.InvokeRemoteMethod("SaleOrder", "sale.CC_SA_SO", "CC_SA_SO.rem", "CloseSoHead", new object[] { ldt_src });
                    int li_result = CloseSoHeadT(ldt_src);

                    _flexH.DataTable.AcceptChanges();
                    _flexL.DataTable.AcceptChanges();

                    OnToolBarSearchButtonClicked(sender, e);

                    // 개가 오더종결 되었습니다
                    MessageBoxEx.Show(li_result.ToString() + GetMessageDictionaryItem("SA_M000078"), PageName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                //if(ldr_result == DialogResult.OK)
                //{
                //    for(int rowH =  _flexH.Rows.Count; rowH > 0; rowH--)
                //    {
                //        if(_flexH[rowH, "CHK"].ToString() == "Y")
                //        {
                //            _flexH[rowH, "STA_SO"] = "C";
						
                //            for(int rowL = _flexL.Rows.Count -1; rowL > 0; rowL--)
                //            {
                //                if(_flexL[rowL, "NO_SO"].ToString() == _flexH[rowH, "NO_SO"].ToString())
                //                {
                //                    _flexL[rowL, "STA_SO"]		= "C";
                //                    //_flexL[rowL, "NM_SYSDEF"]	= "종결";
                //                }
                //            }
                //        }
                //    }

                //    DataRow[] ldr = _flexH.DataTable.Select("CHK = 'Y'","",DataViewRowState.CurrentRows);
                //    object[] lsa_args = new object[]{ldr};

                //    if(ldr.Length == 0)
                //        return;

                //    DataTable ldt_src = MakeHeadTable(ldr);

                //    int li_result  = (int)this.InvokeRemoteMethod("SaleOrder","sale.CC_SA_SO","CC_SA_SO.rem","CloseSoHead",new object[]{ldt_src});

                //    _flexH.DataTable.AcceptChanges();
                //    _flexL.DataTable.AcceptChanges();

                //    // 개가 오더종결 되었습니다
                //    MessageBoxEx.Show(li_result.ToString() + GetMessageDictionaryItem("SA_M000078"), PageName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
			}
			catch(Exception ex)
			{
				MsgEnd(ex);
			}	
		}

        public int CloseSoHeadT(DataTable ldt_src)
        {
            SpInfo si;
            string strSTA_SO = "C";
            int iCount = 0;
            for (int row = 0; row < ldt_src.Rows.Count; row++)
            {
                si = new SpInfo();
                si.SpNameSelect = "UP_SA_CLOSESOHEAD_UPDATE";
                si.SpParamsSelect = new object[]{   ldt_src.Rows[row]["CD_COMPANY"], 
													                ldt_src.Rows[row]["NO_SO"],
                                                                    strSTA_SO   };
                ResultData result = (ResultData)this.FillDataTable(si);
                DataTable dtReturn = (DataTable)result.DataValue;

                iCount++;
            }
            return iCount;
        }

		#endregion

		///******************************///
		///******* 하단 버튼 이벤트******///
		///******************************///
		
		#region -> CloseSoDetail(라인종결)

		private void CloseSoDetail(object sender, System.EventArgs e)
		{
			if(!DoContinue())
				return;

			try
			{				
				if(_flexL.DataView == null || _flexL.DataView.Count == 0)
					return;

				string noso = _flexH[_flexH.Row, "NO_SO"].ToString();

                DataRow[] drCHK = _flexL.DataTable.Select(" CHK = 'Y' AND STA_SO <> 'C' ");
                if (drCHK.Length == 0)
                {
                    this.ShowMessage("체크된 라인의 오더상태가 모두 '마감'이거나 체크된 라인이 없습니다.");
                    return;
                }

                DataRow[] drCHK1 = _flexL.DataTable.Select(" CHK = 'Y' AND STA_SO = 'O' ");
                if (drCHK1.Length > 0)
                {
                    this.ShowMessage("승인이 되지 않은 오더는 '마감' 처리를 할 수 없습니다.");
                    return;
                }

                DataTable ldt_src = null;

                //정말 잔량 취소처리 하시겠습니까 ?
                DialogResult ldr_result = this.ShowMessage("SA_M000079", "QK2");
                if (ldr_result == DialogResult.OK)
                {
                    ldt_src = MakeLineTable(drCHK);

                    //int li_result  = (int)this.InvokeRemoteMethod("SaleOrder","sale.CC_SA_SO","CC_SA_SO.rem","CloseSoDetail",new object[]{ldt_src});
                    int li_result = CloseSoDetailT(ldt_src);

                    _flexL.DataTable.AcceptChanges();

                    OnToolBarSearchButtonClicked(sender, e);

                    // 개가 종결 되었습니다.
                    MessageBoxEx.Show(li_result.ToString() + GetMessageDictionaryItem("SA_M000080"), PageName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
			}
			catch(Exception ex)
			{
				MsgEnd(ex);
			}		
		}

        public int CloseSoDetailT(DataTable ldt_src)
        {
            SpInfo si;
            string strSTA_SO = "C";
            int iCount = 0;
            for (int row = 0; row < ldt_src.Rows.Count; row++)
            {
                si = new SpInfo();
                si.SpNameSelect = "UP_SA_CLOSESODETAIL_UPDATE";
                si.SpParamsSelect = new object[]{   ldt_src.Rows[row]["CD_COMPANY"], 
													                ldt_src.Rows[row]["NO_SO"],
                                                                    ldt_src.Rows[row]["SEQ_SO"], 
                                                                    strSTA_SO   };
                ResultData result = (ResultData)this.FillDataTable(si);
                DataTable dtReturn = (DataTable)result.DataValue;

                iCount++;
            }
            return iCount;
        }

		#endregion

		#region -> UnCloseSoDetail

		private void UnCloseSoDetail(object sender, System.EventArgs e)
		{
			if(!DoContinue())
				return;

			try
			{
                if (_flexL.DataView == null || _flexL.DataView.Count == 0)
                    return;

                string noso = _flexH[_flexH.Row, "NO_SO"].ToString();

                DataRow[] drCHK = _flexL.DataTable.Select(" CHK = 'Y' AND STA_SO = 'C' AND QT_GI <> QT_SO ");

                if (drCHK.Length == 0)
                {
                    this.ShowMessage("체크된 라인의 오더상태가 모두 '출고마감' 이거나 체크된 라인이 없습니다.");
                    return;
                }

                DataTable ldt_src = null;

                //종결 취소하시겠습니까 ?
                DialogResult ldr_result = this.ShowMessage("SA_M000088", "QK2");
                if (ldr_result == DialogResult.OK)
                {
                    ldt_src = MakeLineTable(drCHK);

                    //int li_result  = (int)this.InvokeRemoteMethod("SaleOrder","sale.CC_SA_SO","CC_SA_SO.rem","CloseSoDetail",new object[]{ldt_src});
                    int li_result = UnCloseSoDetailT(ldt_src);

                    _flexL.DataTable.AcceptChanges();

                    OnToolBarSearchButtonClicked(sender, e);

                    // 개가 종결 취소되었습니다.
                    MessageBoxEx.Show(li_result.ToString() + GetMessageDictionaryItem("SA_M000089"), PageName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


                //if(_flexL.DataView == null || _flexL.DataView.Count == 0)
                //    return;

                //string noso = _flexL[_flexL.Row, "NO_SO"].ToString();

                //DataRow[] ldr_row = _flexL.DataTable.Select("CHK = 'Y' AND NO_SO = '" + noso + "' AND  STA_SO <> 'C'");
                //DataRow[] ldr_chk = _flexL.DataTable.Select("CHK = 'Y'");
				
                //if(ldr_chk.Length == 0)
                //{
                //    //삭제할 자료가 선택되어 있지 않습니다.
                //    this.ShowMessage("SA_M000093", "IK1");
                //    return;
                //}
                //if(ldr_row.Length > 0)
                //{
                //    //수주상태에 종결이외의 것이 포함되어 있어 종결취소가 불가능합니다.
                //    this.ShowMessage("SA_M000087", "IK1");
                //    return;
                //}

                ////종결 취소하시겠습니까 ?
                //DialogResult ldr_result = this.ShowMessage("SA_M000088", "QK2");

                //if(ldr_result == DialogResult.OK)
                //{
                //    for(int row = _flexL.Rows.Count -1; row > 0 ; row--)
                //    {
                //        if(_flexL[row, "CHK"].ToString() == "Y")
                //        {
                //            if( _flexL.CDecimal(_flexL[row, "QT_GI"].ToString()) > 0)
                //            {
                //                _flexL[row, "STA_SO"] = "S";
                //                //_flexL[row, "NM_SYSDEF"] = "진행";
                //            }
                //            else
                //            {
                //                _flexL[row, "STA_SO"] = "R";
                //                //_flexL[row, "NM_SYSDEF"] = "의뢰";
                //            }
                //        }
                //    }

                //    DataRow[] ldrclose = _flexL.DataTable.Select("NO_SO = '" + noso + "' AND STA_SO <> 'C'");
                //    DataRow[] ldrstart = _flexL.DataTable.Select("NO_SO = '" + noso + "' AND STA_SO = 'S'");

                //    if(ldrclose.Length == _flexL.DataView.Count)
                //    {
                //        if(ldrstart.Length > 0)
                //            _flexH[_flexH.Row, "STA_SO"] = "S";
                //        else
                //            _flexH[_flexH.Row, "STA_SO"] = "R";
						
                //        for(int row1 = 1 ; row1 < _flexL.Rows.Count; row1++)
                //            _flexL[row1, "STA"] = "ALL";
                //    }

                //    DataRow[] ldr = _flexL.DataTable.Select("CHK = 'Y' AND NO_SO = '" + noso + "'","",DataViewRowState.CurrentRows);
                //    object[] lsa_args = new object[]{ldr};
                //    if(ldr.Length == 0)
                //        return;
                //    DataTable ldt_src = MakeLineTable(ldr);
					
                //    int li_result  = (int)this.InvokeRemoteMethod("SaleOrder","sale.CC_SA_SO","CC_SA_SO.rem","CloseSoDetail",new object[]{ldt_src});
                //    _flexL.DataTable.AcceptChanges();

                //    // 개가 종결 취소되었습니다.
                //    MessageBoxEx.Show(li_result.ToString() +GetMessageDictionaryItem("SA_M000089"), PageName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				//}
			}
			catch(Exception ex)
			{
				MsgEnd(ex);
			}	
		}

        public int UnCloseSoDetailT(DataTable ldt_src)
        {
            SpInfo si;
            string strSTA_SO;
            int iCount = 0;
            for (int row = 0; row < ldt_src.Rows.Count; row++)
            {
                string strNum = NumericToString(ldt_src.Rows[row]["QT_GI"]);
                if (Convert.ToDecimal(NumericToString(ldt_src.Rows[row]["QT_GI"])) > 0)
                    strSTA_SO = "S";
                else
                    strSTA_SO = "R";

                si = new SpInfo();
                si.SpNameSelect = "UP_SA_CLOSESODETAIL_UPDATE";
                si.SpParamsSelect = new object[]{   ldt_src.Rows[row]["CD_COMPANY"], 
													                ldt_src.Rows[row]["NO_SO"],
                                                                    ldt_src.Rows[row]["SEQ_SO"], 
                                                                    strSTA_SO   };
                ResultData result = (ResultData)this.FillDataTable(si);
                DataTable dtReturn = (DataTable)result.DataValue;

                iCount++;
            }
            return iCount;
        }

		#endregion

		#region -> DeleteRowBySoMng

		private void DeleteRowBySoMng(object sender, System.EventArgs e)
		{	
			if(!DoContinue())
				return;

			try
			{
				string noso = _flexH[_flexH.Row, "NO_SO"].ToString();

				if(_flexL.DataView == null)
					return;

				// A타입
				DataRow[] ldra = _flexL.DataTable.Select("CHK = 'Y' AND NO_SO = '" + noso + "' AND CONF = 'N' AND GIR = 'Y' AND STA_SO = 'O'");//CONF수정
				// B타입
				DataRow[] ldrb = _flexL.DataTable.Select("CHK = 'Y' AND NO_SO = '" + noso + "' AND  CONF = 'N' AND GIR = 'N' AND STA_SO = 'O'");//CONF수정
				// C타입
				DataRow[] ldrc = _flexL.DataTable.Select("CHK = 'Y' AND NO_SO = '" + noso + "' AND  CONF = 'Y' AND GIR = 'Y' AND STA_SO = 'R'");//CONF수정
				// D타입
				DataRow[] ldrd = _flexL.DataTable.Select("CHK = 'Y' AND NO_SO = '" + noso + "' AND  CONF = 'Y' AND GIR = 'N' AND STA_SO = 'R'");//CONF수정

				DataRow[] ldrchk = _flexL.DataTable.Select("CHK = 'Y' AND NO_SO = '" + noso + "'");

				if(ldrchk.Length == 0)
				{
					//삭제할 자료가 선택되어 있지 않습니다.
					this.ShowMessage("SA_M000093", "IK1");
					return;
				}

				if((ldra.Length + ldrb.Length + ldrc.Length + ldrd.Length) == 0 || (ldra.Length + ldrb.Length + ldrc.Length + ldrd.Length) != ldrchk.Length)
				{
					//이미 확정된 데이터가 포함되어 있습니다.
					this.ShowMessage("SA_M000090", "IK1");
					return;
				}

				if(ldrchk.Length == _flexL.DataView.Count)
				{
					//라인 정보가 모두 삭제되므로 헤더 정보를 삭제하십시요.
					this.ShowMessage("SA_M000097", "IK1");
					return;
				}

				for(int row = _flexL.Rows.Count -1; row > 0 ; row--)
				{
					if(_flexL[row, "CHK"].ToString() == "Y")
					{
						//20040607
						m_obj_DEL = new object[3];
					
						m_obj_DEL[0] = _flexL[row, "NO_SO"];
						m_obj_DEL[1] = this.MainFrameInterface.LoginInfo.CompanyCode;
						m_obj_DEL[2] = _flexL[row, "SEQ_SO"];

						_flexL.RemoveItem(row);

						
					}
				}
				this.ToolBarSaveButtonEnabled = true;
			}
			catch(Exception ex)
			{
				MsgEnd(ex);
			}
		}

		#endregion

		#region -> OpenAtpCheck

		private void OpenAtpCheck(object sender, System.EventArgs e)
		{
			if(_flexL.DataView == null)
				return;

			try
			{
				sale.P_SA_ATPCHECK obj_atp = new sale.P_SA_ATPCHECK(this.MainFrameInterface, _flexL[_flexL.Row,"CD_ITEM"].ToString(), _flexL[_flexL.Row,"NM_ITEM"].ToString(), _flexL[_flexL.Row,"CD_PLANT"].ToString());
				
				obj_atp.ShowDialog();
			}
			catch(Exception ex)
			{
				MsgEnd(ex);
			}
		}

		#endregion

		///******************************///
		///******* 사용자 메소드******///
		///******************************///

		#region -> MakeLineTable
		/// <summary>
		/// 수주Line Table 생성
		/// </summary>
		/// <param name="ldr">DataRow</param>
		/// <returns>DataTable</returns>
		private DataTable MakeLineTable(DataRow[] ldr)
		{
			DataTable ldt_src = new DataTable();

			ldt_src.Columns.Add(new DataColumn("NO_SO"));
			ldt_src.Columns.Add(new DataColumn("CD_COMPANY"));
			ldt_src.Columns.Add(new DataColumn("SEQ_SO"));
			ldt_src.Columns.Add(new DataColumn("NO_HST"));
			ldt_src.Columns.Add(new DataColumn("CD_ITEM"));
			ldt_src.Columns.Add(new DataColumn("TP_ITEM"));
			ldt_src.Columns.Add(new DataColumn("CD_PLANT"));
			ldt_src.Columns.Add(new DataColumn("CD_SL"));
			ldt_src.Columns.Add(new DataColumn("DT_DUEDATE"));
			ldt_src.Columns.Add(new DataColumn("DT_REQGI"));
			ldt_src.Columns.Add(new DataColumn("QT_SO"));
			ldt_src.Columns.Add(new DataColumn("UM_SO"));
			ldt_src.Columns.Add(new DataColumn("AM_SO"));
			ldt_src.Columns.Add(new DataColumn("UNIT_SO"));
			ldt_src.Columns.Add(new DataColumn("AM_WONAMT"));
			ldt_src.Columns.Add(new DataColumn("QT_IM"));
			ldt_src.Columns.Add(new DataColumn("UNIT_IM"));
			ldt_src.Columns.Add(new DataColumn("TP_VAT"));
			ldt_src.Columns.Add(new DataColumn("QT_GIR"));
			ldt_src.Columns.Add(new DataColumn("QT_GI"));
			ldt_src.Columns.Add(new DataColumn("QT_IV"));
			ldt_src.Columns.Add(new DataColumn("QT_LC"));
			ldt_src.Columns.Add(new DataColumn("QT_RETURN"));
			ldt_src.Columns.Add(new DataColumn("NO_PROJECT"));
			ldt_src.Columns.Add(new DataColumn("AM_VAT"));
			ldt_src.Columns.Add(new DataColumn("RT_VAT"));
			ldt_src.Columns.Add(new DataColumn("FG_VAT"));
			ldt_src.Columns.Add(new DataColumn("TP_GI"));
			ldt_src.Columns.Add(new DataColumn("TP_IV"));
			ldt_src.Columns.Add(new DataColumn("TP_BUSI"));
			ldt_src.Columns.Add(new DataColumn("NO_TRACKING"));
			ldt_src.Columns.Add(new DataColumn("NO_RELATION"));
			ldt_src.Columns.Add(new DataColumn("SEQ_RELATION"));
			ldt_src.Columns.Add(new DataColumn("CONF"));
			ldt_src.Columns.Add(new DataColumn("GIR"));
			ldt_src.Columns.Add(new DataColumn("GI"));
			ldt_src.Columns.Add(new DataColumn("IV"));
			ldt_src.Columns.Add(new DataColumn("CM"));
			ldt_src.Columns.Add(new DataColumn("TRADE"));
			ldt_src.Columns.Add(new DataColumn("RET"));
			ldt_src.Columns.Add(new DataColumn("SUBCONT"));
			ldt_src.Columns.Add(new DataColumn("STA_SO"));
			ldt_src.Columns.Add(new DataColumn("DTS_INSERT"));
			ldt_src.Columns.Add(new DataColumn("ID_INSERT"));
			ldt_src.Columns.Add(new DataColumn("DTS_UPDATE"));
			ldt_src.Columns.Add(new DataColumn("ID_UPDATE"));
			ldt_src.Columns.Add(new DataColumn("FG_SQC"));
			ldt_src.Columns.Add(new DataColumn("STA")); // 종결시킬때 필요해서 넣음

			//20040429
			ldt_src.Columns.Add(new DataColumn("NO_IOLINE_MGMT"));
			ldt_src.Columns.Add(new DataColumn("NO_IO_MGMT"));
			ldt_src.Columns.Add(new DataColumn("NO_SOLINE_MGMT"));
			ldt_src.Columns.Add(new DataColumn("NO_SO_MGMT"));
			
			for(int i = 0 ; i < ldr.Length ; i++)
			{
				DataRow myRow = ldt_src.NewRow();

				myRow["NO_SO"] = ldr[i]["NO_SO"];
				myRow["CD_COMPANY"] = ldr[i]["CD_COMPANY"];
				myRow["SEQ_SO"] = ldr[i]["SEQ_SO"];
				myRow["NO_HST"] = ldr[i]["NO_HST"];
				myRow["CD_ITEM"] = ldr[i]["CD_ITEM"];
				myRow["TP_ITEM"] = ldr[i]["TP_ITEM"];
				myRow["CD_PLANT"] = ldr[i]["CD_PLANT"];
				myRow["CD_SL"] = ldr[i]["CD_SL"];
				myRow["DT_DUEDATE"] = ldr[i]["DT_DUEDATE"];
				myRow["DT_REQGI"] = ldr[i]["DT_REQGI"];
				myRow["QT_SO"] = ldr[i]["QT_SO"];
				myRow["UM_SO"] = ldr[i]["UM_SO"];
				myRow["SEQ_SO"] = ldr[i]["SEQ_SO"];
				myRow["AM_SO"] = ldr[i]["AM_SO"];
				myRow["UNIT_SO"] = ldr[i]["UNIT_SO"];
				myRow["AM_WONAMT"] = ldr[i]["AM_WONAMT"];
				myRow["QT_IM"] = ldr[i]["QT_IM"];
				myRow["UNIT_IM"] = ldr[i]["UNIT_IM"];
				myRow["TP_VAT"] = ldr[i]["TP_VAT"];
				myRow["QT_GIR"] = ldr[i]["QT_GIR"];
				myRow["QT_GI"] = ldr[i]["QT_GI"];
				myRow["QT_IV"] = ldr[i]["QT_IV"];
				myRow["QT_LC"] = ldr[i]["QT_LC"];
				myRow["QT_RETURN"] = ldr[i]["QT_RETURN"];
				myRow["NO_PROJECT"] = ldr[i]["NO_PROJECT"];
				myRow["AM_VAT"] = ldr[i]["AM_VAT"];
				myRow["RT_VAT"] = ldr[i]["RT_VAT"];
				myRow["FG_VAT"] = ldr[i]["FG_VAT"];
				myRow["TP_GI"] = ldr[i]["TP_GI"];
				myRow["TP_IV"] = ldr[i]["TP_IV"];
				myRow["TP_BUSI"] = ldr[i]["TP_BUSI"];
				myRow["NO_TRACKING"] = ldr[i]["NO_TRACKING"];
				myRow["NO_RELATION"] = ldr[i]["NO_RELATION"];
				myRow["SEQ_RELATION"] = ldr[i]["SEQ_RELATION"];
				myRow["CONF"] = ldr[i]["CONF"];
				myRow["GIR"] = ldr[i]["GIR"];
				myRow["GI"] = ldr[i]["GI"];
				myRow["IV"] = ldr[i]["IV"];
				myRow["CM"] = ldr[i]["CM"];
				myRow["TRADE"] = ldr[i]["TRADE"];
				myRow["RET"] = ldr[i]["RET"];
				myRow["SUBCONT"] = ldr[i]["SUBCONT"];
				myRow["STA_SO"] = ldr[i]["STA_SO"];
				myRow["DTS_INSERT"] = ldr[i]["DTS_INSERT"];
				myRow["ID_INSERT"] = ldr[i]["ID_INSERT"];
				myRow["DTS_UPDATE"] = ldr[i]["DTS_UPDATE"];
				myRow["ID_UPDATE"] = ldr[i]["ID_UPDATE"];
				myRow["FG_SQC"] = ldr[i]["FG_SQC"];
				myRow["STA"] = ldr[i]["STA"];

				ldt_src.Rows.Add(myRow);
			}
			return ldt_src;
		}

		#endregion

		#region -> MakeHeadTable

		private DataTable MakeHeadTable(DataRow[] ldr)
		{
			DataTable ldt_src = new DataTable();
			ldt_src.Columns.Add(new DataColumn("NO_SO"));
			ldt_src.Columns.Add(new DataColumn("CD_COMPANY"));
			ldt_src.Columns.Add(new DataColumn("NO_HST"));
			ldt_src.Columns.Add(new DataColumn("CD_BIZAREA"));
			ldt_src.Columns.Add(new DataColumn("DT_SO"));
			
			ldt_src.Columns.Add(new DataColumn("CD_PARTNER"));
			ldt_src.Columns.Add(new DataColumn("GI_PARTNER"));
			ldt_src.Columns.Add(new DataColumn("SO_PARTNER"));
			ldt_src.Columns.Add(new DataColumn("BILL_PARTNER"));
			ldt_src.Columns.Add(new DataColumn("CD_EXCH"));
			ldt_src.Columns.Add(new DataColumn("RT_EXCH"));
			ldt_src.Columns.Add(new DataColumn("AM_SO"));
			ldt_src.Columns.Add(new DataColumn("AM_WONAMT"));
			ldt_src.Columns.Add(new DataColumn("CD_SALEGRP"));

			ldt_src.Columns.Add(new DataColumn("NO_EMP"));
			ldt_src.Columns.Add(new DataColumn("TP_SO"));
			ldt_src.Columns.Add(new DataColumn("TP_PRICE"));
			ldt_src.Columns.Add(new DataColumn("AM_VAT"));
			ldt_src.Columns.Add(new DataColumn("TP_VAT"));

			ldt_src.Columns.Add(new DataColumn("FG_VAT"));
			ldt_src.Columns.Add(new DataColumn("FG_TRANSPORT"));
			ldt_src.Columns.Add(new DataColumn("FG_TAXP"));//20040429
			ldt_src.Columns.Add(new DataColumn("FG_BILL"));
			ldt_src.Columns.Add(new DataColumn("NO_PROJECT"));
			ldt_src.Columns.Add(new DataColumn("NO_NEGO"));

			ldt_src.Columns.Add(new DataColumn("STA_SO"));
			ldt_src.Columns.Add(new DataColumn("YN_CONFIRM"));
			ldt_src.Columns.Add(new DataColumn("DC_RMK"));
			ldt_src.Columns.Add(new DataColumn("ID_INSERT"));
			ldt_src.Columns.Add(new DataColumn("DTS_INSERT"));
			ldt_src.Columns.Add(new DataColumn("ID_UPDATE"));
			ldt_src.Columns.Add(new DataColumn("DTS_UPDATE"));
			ldt_src.Columns.Add(new DataColumn("CONF"));
			ldt_src.Columns.Add(new DataColumn("GIR"));

			for(int i = 0 ; i < ldr.Length ; i++)
			{
				DataRow myRow = ldt_src.NewRow();
				myRow["NO_SO"] = ldr[i]["NO_SO"];
				myRow["CD_COMPANY"] = ldr[i]["CD_COMPANY"];
				myRow["NO_HST"] = ldr[i]["NO_HST"];
				myRow["CD_BIZAREA"] = ldr[i]["CD_BIZAREA"];
				myRow["DT_SO"] = ldr[i]["DT_SO"];

				myRow["SO_PARTNER"] = ldr[i]["SO_PARTNER"];
				myRow["GI_PARTNER"] = ldr[i]["GI_PARTNER"];
				myRow["CD_PARTNER"] = ldr[i]["CD_PARTNER"];
				myRow["BILL_PARTNER"] = ldr[i]["BILL_PARTNER"];
				myRow["CD_EXCH"] = ldr[i]["CD_EXCH"];

				myRow["RT_EXCH"] = ldr[i]["RT_EXCH"];
				myRow["AM_SO"] = ldr[i]["AM_SO"];
				myRow["AM_WONAMT"] = ldr[i]["AM_WONAMT"];
				myRow["CD_SALEGRP"] = ldr[i]["CD_SALEGRP"];
				myRow["NO_EMP"] = ldr[i]["NO_EMP"];

				myRow["TP_SO"] = ldr[i]["TP_SO"];
				myRow["TP_PRICE"] = ldr[i]["TP_PRICE"];
				myRow["AM_VAT"] = ldr[i]["AM_VAT"];
				myRow["TP_VAT"] = ldr[i]["TP_VAT"];
				myRow["FG_VAT"] = ldr[i]["FG_VAT"];

				myRow["FG_TRANSPORT"] = ldr[i]["FG_TRANSPORT"];
				myRow["FG_BILL"] = ldr[i]["FG_BILL"];
				myRow["NO_PROJECT"] = ldr[i]["NO_PROJECT"];
				myRow["STA_SO"] = ldr[i]["STA_SO"];
				myRow["YN_CONFIRM"] = ldr[i]["YN_CONFIRM"];

				myRow["DC_RMK"] = ldr[i]["DC_RMK"];
				myRow["ID_INSERT"] = ldr[i]["ID_INSERT"];
				myRow["DTS_INSERT"] = ldr[i]["DTS_INSERT"];
				myRow["ID_UPDATE"] = ldr[i]["ID_UPDATE"];
				myRow["DTS_UPDATE"] = ldr[i]["DTS_UPDATE"];
				myRow["CONF"] = ldr[i]["CONF"];
				myRow["GIR"] = ldr[i]["GIR"];
				ldt_src.Rows.Add(myRow);
			}
			return ldt_src;
		}

		#endregion

		#region -> CheckSearchOption -> 코드가 있는 이유를 알수가 없어 주석처리.

        //private bool CheckSearchOption(int piOption, int piRow)
        //{
        //    bool lb_ReturnMsg = true;
        //    DataSet lds_src = null;
        //    DataTable ldt_src = null;

        //    switch(piOption)
        //    {
        //        case 10 :		
        //        switch(piRow)
        //        {
        //            case 1 ://영업그룹
        //                lds_src = (DataSet)InvokeRemoteMethod("MasterSale_NTX","master.CC_MA_SALEGRP_NTX","CC_MA_SALEGRP_NTX.rem","Select_Detail", new object[] {LoginInfo.CompanyCode, bpSalegrp.CodeValue});

        //                if(lds_src == null || lds_src.Tables[0].Rows.Count == 0)
        //                {
        //                    // 등록되지않은 코드입니다.
        //                    bpSalegrp.CodeValue = string.Empty;
        //                    bpSalegrp.CodeName = string.Empty;
							
        //                    return false;
        //                }
        //                else if(lds_src.Tables[0].Rows.Count == 1)
        //                {
        //                    bpSalegrp.CodeName = lds_src.Tables[0].Rows[0]["NM_SALEGRP"].ToString();
        //                    bpSalegrp.CodeValue = lds_src.Tables[0].Rows[0]["CD_SALEGRP"].ToString();
						
        //                    return true;
        //                }
        //                break;

        //            case 2://담당자
        //                DataTable ldt_src1 = (DataTable)InvokeRemoteMethod("MasterBasicInfo_NTX", "master.CC_MA_EMP_NTX", "CC_MA_EMP_NTX.rem", "SelectGetEmpOne",new object[]{LoginInfo.CompanyCode, bpNoEmp.CodeValue});

        //                if(ldt_src1.Rows.Count == 0)
        //                {
        //                    ShowHelp(bpNoEmp, bpNoEmp.CodeValue);
        //                    return true;
        //                }

        //                bpNoEmp.CodeName = ldt_src1.Rows[0]["NM_KOR"].ToString();
						
        //                return true;

        //            case 3://수주처
        //                ldt_src = (DataTable)InvokeRemoteMethod("SaleBasicInfo_NTX", "sale.CC_SA_TPPTR_NTX", "CC_SA_TPPTR_NTX.rem", "SelectNmTpptr", new object[]{LoginInfo.CompanyCode, "001",bpSoPartner.CodeValue});

        //                if(ldt_src.Rows.Count == 0)
        //                {
        //                    ShowHelp(bpSoPartner, bpSoPartner.CodeValue);
        //                    return true;
        //                }

        //                bpSoPartner.CodeName = ldt_src.Rows[0]["NM_TPPTR"].ToString();
						
        //                return true;

        //            case 4://수주유형
        //                ldt_src = (DataTable)InvokeRemoteMethod("SaleBasicInfo_NTX", "sale.CC_SA_TPSO_NTX", "CC_SA_TPSO_NTX.rem", "SelectNmsoByTpso", new object[]{LoginInfo.CompanyCode, bpTpSo.CodeValue});

        //                if(ldt_src.Rows.Count == 0)
        //                {
        //                    bpTpSo.CodeValue = "";
        //                    bpTpSo.CodeName = string.Empty;
							
        //                    // 등록되지않은 코드입니다.
        //                    ShowMessageBox(108);
        //                    return false;
        //                }
        //                else if(ldt_src.Rows.Count == 1)
        //                {
        //                    bpTpSo.CodeName = ldt_src.Rows[0]["NM_SO"].ToString();
        //                    bpTpSo.CodeValue = ldt_src.Rows[0]["TP_SO"].ToString();
							
        //                    return true;
        //                }
        //                break;
        //        }
        //            break;
        //    }
        //    return lb_ReturnMsg;
        //}

		#endregion

		#endregion

		#region >> 날짜 관련 이벤트
		
		private void OnDateKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if((e.KeyData == Keys.Left) || (e.KeyData == Keys.Right) ||
				(e.KeyData == Keys.Home) || (e.KeyData == Keys.End))
				return;

			Duzon.Common.Controls.DatePicker maskBox = (Duzon.Common.Controls.DatePicker)sender;
       
			if(Keys.Enter == e.KeyData || maskBox.Text.Length == 7)
			{
				System.Windows.Forms.SendKeys.SendWait("{TAB}");
			}
			else if(Keys.Up == e.KeyData)
			{
				System.Windows.Forms.SendKeys.SendWait("+{TAB}");
			}
			else if(Keys.Down == e.KeyData)
			{
				System.Windows.Forms.SendKeys.SendWait("{TAB}");
			}			
		}

		private void m_mskDtStart_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			OnDateKeyDown(sender, e);

		}

		private void m_mskDtEnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			OnDateKeyDown(sender, e);

		}

		private void m_mskDtStart_Validated(object sender, System.EventArgs e)
		{
			if(!this.m_mskDtStart.IsValidated)
			{
				ShowMessage("WK1_003", m_lblDtSo.Text);
				this.m_mskDtStart.Focus();
			}

		}

		private void m_mskDtEnd_Validated(object sender, System.EventArgs e)
		{
			if(!this.m_mskDtStart.IsValidated)
			{
				ShowMessage("WK1_003", m_lblDtSo.Text);
				this.m_mskDtStart.Focus();
			}

		}

		#endregion		

		#region -> BP Control

		private void Control_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
		{
			switch(e.HelpID)
			{				
				case Duzon.Common.Forms.Help.HelpID.P_SA_TPPTR_SUB:// 수주처
					e.HelpParam.P61_CODE1 = "002";
					e.HelpParam.P14_CD_PARTNER= bpSoPartner.CodeValue.ToString();
					
					break;	
				case Duzon.Common.Forms.Help.HelpID.P_SA_TPSO_SUB:// 수주형태
					
					e.HelpParam.P61_CODE1 = "";
					e.HelpParam.P62_CODE2 = "Y";
					
					break;
					
			}

		}

		private void Control_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
		{
			if(e.DialogResult == DialogResult.Cancel)
				return;

			switch(e.ControlName)
			{
					// 영업그룹
				case "bpSalegrp" :					
					bpSalegrp.CodeName = e.HelpReturn.Rows[0]["NM_SALEGRP"].ToString();
					bpSalegrp.CodeValue = e.HelpReturn.Rows[0]["CD_SALEGRP"].ToString();
										
					break;

					// 담당자
				case "bpNoEmp":											
					bpNoEmp.CodeName = bpNoEmp.Text = e.HelpReturn.Rows[0]["NM_KOR"].ToString();
					bpNoEmp.CodeValue = e.HelpReturn.Rows[0]["NO_EMP"].ToString();
					
					break;

				case "bpSoPartner":	//수주처				
					bpSoPartner.Text = e.HelpReturn.Rows[0]["CD_TPPTR"].ToString();
					bpSoPartner.CodeName = e.HelpReturn.Rows[0]["NM_TPPTR"].ToString();
						
					break;

				case "bpTpSo":						
					bpTpSo.CodeName = e.HelpReturn.Rows[0]["NM_SO"].ToString();
					bpTpSo.CodeValue = e.HelpReturn.Rows[0]["TP_SO"].ToString();
					
					break;
			}
		}

		#endregion
	}
}

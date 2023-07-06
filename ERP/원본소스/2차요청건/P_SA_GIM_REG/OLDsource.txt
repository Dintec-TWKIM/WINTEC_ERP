using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.Common.Controls;
//using C1.Common;
using System.Threading;

using C1.Win.C1FlexGrid;
using Dass.FlexGrid;

namespace sale
{
	/// <summary>
	/// UserControl1에 대한 요약 설명입니다.
	/// </summary>
	public class P_SA_GIM_REG_BAK: Duzon.Common.Forms.PageBase
	{		
		#region ♣ 멤버필드

		#region -> 멤버필드(일반)

		private Duzon.Common.Controls.PanelExt panel4;
		private Duzon.Common.Controls.PanelExt panel5;
		private Duzon.Common.Controls.PanelExt panel6;
		private Duzon.Common.Controls.PanelExt panel7;
		private Duzon.Common.Controls.PanelExt panel9;
		private Duzon.Common.Controls.PanelExt panel10;
		private Duzon.Common.Controls.PanelExt panel11;
		private Duzon.Common.Controls.PanelExt panel12;
		private Duzon.Common.Controls.LabelExt lb_fg_gr;
		private Duzon.Common.Controls.LabelExt lb_no_emp;
		private Duzon.Common.Controls.LabelExt lb_nm_sl;
		private Duzon.Common.Controls.LabelExt lb_fg_po_tr;
		private Duzon.Common.Controls.LabelExt lb_gr_plant;
		private Duzon.Common.Controls.LabelExt lb_dt_rcv;
		private Duzon.Common.Controls.LabelExt lb_gr_partner;
		private Duzon.Common.Controls.LabelExt lb_rcv_pt;
		private Duzon.Common.Controls.LabelExt label2;

		// DataSet
		private System.Data.DataSet dSet_qtioH;
		private System.Data.DataSet dSet_qtioL;

		// DataTable
		private System.Data.DataTable dataTable1;
		private System.Data.DataTable dataTable2;
		private Duzon.Common.Controls.DropDownComboBox cb_fg_gr;
		private Duzon.Common.Controls.DropDownComboBox cb_cd_plant;
		private Duzon.Common.Controls.DropDownComboBox cb_fg_po_tr;

		// 
		private System.ComponentModel.IContainer components;

//		private DataTable dt_QTIO = null;

		DataSet gds_dzdwGrid1_DEL = new DataSet();		
		DataSet gds_dzdwGrid2_DEL = new DataSet();

		private DataTable ldt_del = new DataTable();

		private string m_page_state;
		private Duzon.Common.Controls.DatePicker maskedEditBox1;
		private Duzon.Common.Controls.DatePicker maskedEditBox2;

		private string  gstb_qtio,gs_fgtp,gs_tpqtio;
		
		private Duzon.Common.BpControls.BpCodeTextBox bpNm_Sl;
		private Duzon.Common.BpControls.BpCodeTextBox bpRcv_Pt;
		private Duzon.Common.BpControls.BpCodeTextBox bpNo_Emp;
		private Duzon.Common.BpControls.BpCodeTextBox bpNm_Partner;

		#endregion

		#region -> 멤버필드(주요)
		/// <summary>
		/// Load여부 변수(Paint Event에서 사용)
		/// </summary>
		private bool _isPainted = false;

		private Dass.FlexGrid.FlexGrid _flexH;
        private TableLayoutPanel tableLayoutPanel1;
        private SplitContainer splitContainer1;
		private Dass.FlexGrid.FlexGrid _flexL;	

		#endregion

		#endregion

		#region ♣ 생성자/소멸자

		#region -> 생성자(1)

		public P_SA_GIM_REG_BAK()
		{
			InitializeComponent();

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_SA_GIM_REG_BAK));
            this.panel5 = new Duzon.Common.Controls.PanelExt();
            this._flexH = new Dass.FlexGrid.FlexGrid(this.components);
            this.panel4 = new Duzon.Common.Controls.PanelExt();
            this.bpNo_Emp = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpRcv_Pt = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpNm_Partner = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpNm_Sl = new Duzon.Common.BpControls.BpCodeTextBox();
            this.maskedEditBox2 = new Duzon.Common.Controls.DatePicker();
            this.maskedEditBox1 = new Duzon.Common.Controls.DatePicker();
            this.cb_cd_plant = new Duzon.Common.Controls.DropDownComboBox();
            this.cb_fg_gr = new Duzon.Common.Controls.DropDownComboBox();
            this.cb_fg_po_tr = new Duzon.Common.Controls.DropDownComboBox();
            this.panel12 = new Duzon.Common.Controls.PanelExt();
            this.panel11 = new Duzon.Common.Controls.PanelExt();
            this.panel10 = new Duzon.Common.Controls.PanelExt();
            this.lb_fg_gr = new Duzon.Common.Controls.LabelExt();
            this.lb_nm_sl = new Duzon.Common.Controls.LabelExt();
            this.panel9 = new Duzon.Common.Controls.PanelExt();
            this.lb_fg_po_tr = new Duzon.Common.Controls.LabelExt();
            this.lb_gr_plant = new Duzon.Common.Controls.LabelExt();
            this.lb_no_emp = new Duzon.Common.Controls.LabelExt();
            this.panel7 = new Duzon.Common.Controls.PanelExt();
            this.lb_dt_rcv = new Duzon.Common.Controls.LabelExt();
            this.lb_gr_partner = new Duzon.Common.Controls.LabelExt();
            this.lb_rcv_pt = new Duzon.Common.Controls.LabelExt();
            this.label2 = new Duzon.Common.Controls.LabelExt();
            this.panel6 = new Duzon.Common.Controls.PanelExt();
            this._flexL = new Dass.FlexGrid.FlexGrid(this.components);
            this.dSet_qtioH = new System.Data.DataSet();
            this.dataTable1 = new System.Data.DataTable();
            this.dSet_qtioL = new System.Data.DataSet();
            this.dataTable2 = new System.Data.DataTable();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maskedEditBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maskedEditBox1)).BeginInit();
            this.panel10.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSet_qtioH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSet_qtioL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable2)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this._flexH);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(787, 203);
            this.panel5.TabIndex = 0;
            // 
            // _flexH
            // 
            this._flexH.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexH.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexH.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexH.AutoResize = false;
            this._flexH.ColumnInfo = "1,1,0,0,0,75,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
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
            this._flexH.Size = new System.Drawing.Size(787, 203);
            this._flexH.StyleInfo = resources.GetString("_flexH.StyleInfo");
            this._flexH.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.bpNo_Emp);
            this.panel4.Controls.Add(this.bpRcv_Pt);
            this.panel4.Controls.Add(this.bpNm_Partner);
            this.panel4.Controls.Add(this.bpNm_Sl);
            this.panel4.Controls.Add(this.maskedEditBox2);
            this.panel4.Controls.Add(this.maskedEditBox1);
            this.panel4.Controls.Add(this.cb_cd_plant);
            this.panel4.Controls.Add(this.cb_fg_gr);
            this.panel4.Controls.Add(this.cb_fg_po_tr);
            this.panel4.Controls.Add(this.panel12);
            this.panel4.Controls.Add(this.panel11);
            this.panel4.Controls.Add(this.panel10);
            this.panel4.Controls.Add(this.panel9);
            this.panel4.Controls.Add(this.panel7);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(787, 79);
            this.panel4.TabIndex = 0;
            // 
            // bpNo_Emp
            // 
            this.bpNo_Emp.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpNo_Emp.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpNo_Emp.ButtonImage")));
            this.bpNo_Emp.ChildMode = "";
            this.bpNo_Emp.CodeName = "";
            this.bpNo_Emp.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpNo_Emp.CodeValue = "";
            this.bpNo_Emp.ComboCheck = true;
            this.bpNo_Emp.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
            this.bpNo_Emp.ItemBackColor = System.Drawing.Color.White;
            this.bpNo_Emp.Location = new System.Drawing.Point(370, 54);
            this.bpNo_Emp.Name = "bpNo_Emp";
            this.bpNo_Emp.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpNo_Emp.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.bpNo_Emp.SearchCode = true;
            this.bpNo_Emp.SelectCount = 0;
            this.bpNo_Emp.SetDefaultValue = false;
            this.bpNo_Emp.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpNo_Emp.Size = new System.Drawing.Size(156, 21);
            this.bpNo_Emp.TabIndex = 9;
            this.bpNo_Emp.TabStop = false;
            this.bpNo_Emp.Text = "bpCodeTextBox4";
            this.bpNo_Emp.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryBefore);
            this.bpNo_Emp.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
            // 
            // bpRcv_Pt
            // 
            this.bpRcv_Pt.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpRcv_Pt.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpRcv_Pt.ButtonImage")));
            this.bpRcv_Pt.ChildMode = "";
            this.bpRcv_Pt.CodeName = "";
            this.bpRcv_Pt.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpRcv_Pt.CodeValue = "";
            this.bpRcv_Pt.ComboCheck = true;
            this.bpRcv_Pt.HelpID = Duzon.Common.Forms.Help.HelpID.P_PU_EJTP_SUB;
            this.bpRcv_Pt.ItemBackColor = System.Drawing.Color.White;
            this.bpRcv_Pt.Location = new System.Drawing.Point(90, 54);
            this.bpRcv_Pt.Name = "bpRcv_Pt";
            this.bpRcv_Pt.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpRcv_Pt.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.bpRcv_Pt.SearchCode = true;
            this.bpRcv_Pt.SelectCount = 0;
            this.bpRcv_Pt.SetDefaultValue = false;
            this.bpRcv_Pt.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpRcv_Pt.Size = new System.Drawing.Size(188, 21);
            this.bpRcv_Pt.TabIndex = 7;
            this.bpRcv_Pt.TabStop = false;
            this.bpRcv_Pt.Text = "bpCodeTextBox3";
            this.bpRcv_Pt.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryBefore);
            this.bpRcv_Pt.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
            // 
            // bpNm_Partner
            // 
            this.bpNm_Partner.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpNm_Partner.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpNm_Partner.ButtonImage")));
            this.bpNm_Partner.ChildMode = "";
            this.bpNm_Partner.CodeName = "";
            this.bpNm_Partner.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpNm_Partner.CodeValue = "";
            this.bpNm_Partner.ComboCheck = true;
            this.bpNm_Partner.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.bpNm_Partner.ItemBackColor = System.Drawing.Color.White;
            this.bpNm_Partner.Location = new System.Drawing.Point(90, 30);
            this.bpNm_Partner.Name = "bpNm_Partner";
            this.bpNm_Partner.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpNm_Partner.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.bpNm_Partner.SearchCode = true;
            this.bpNm_Partner.SelectCount = 0;
            this.bpNm_Partner.SetDefaultValue = false;
            this.bpNm_Partner.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpNm_Partner.Size = new System.Drawing.Size(188, 21);
            this.bpNm_Partner.TabIndex = 4;
            this.bpNm_Partner.TabStop = false;
            this.bpNm_Partner.Text = "bpCodeTextBox2";
            this.bpNm_Partner.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryBefore);
            this.bpNm_Partner.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
            // 
            // bpNm_Sl
            // 
            this.bpNm_Sl.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpNm_Sl.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpNm_Sl.ButtonImage")));
            this.bpNm_Sl.ChildMode = "";
            this.bpNm_Sl.CodeName = "";
            this.bpNm_Sl.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpNm_Sl.CodeValue = "";
            this.bpNm_Sl.ComboCheck = true;
            this.bpNm_Sl.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB;
            this.bpNm_Sl.ItemBackColor = System.Drawing.Color.White;
            this.bpNm_Sl.Location = new System.Drawing.Point(621, 3);
            this.bpNm_Sl.Name = "bpNm_Sl";
            this.bpNm_Sl.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpNm_Sl.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.bpNm_Sl.SearchCode = true;
            this.bpNm_Sl.SelectCount = 0;
            this.bpNm_Sl.SetDefaultValue = false;
            this.bpNm_Sl.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpNm_Sl.Size = new System.Drawing.Size(156, 21);
            this.bpNm_Sl.TabIndex = 3;
            this.bpNm_Sl.TabStop = false;
            this.bpNm_Sl.Text = "bpCodeTextBox1";
            this.bpNm_Sl.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryBefore);
            this.bpNm_Sl.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
            // 
            // maskedEditBox2
            // 
            this.maskedEditBox2.CalendarBackColor = System.Drawing.Color.White;
            this.maskedEditBox2.DayColor = System.Drawing.SystemColors.ControlText;
            this.maskedEditBox2.FriDayColor = System.Drawing.Color.Blue;
            this.maskedEditBox2.Location = new System.Drawing.Point(194, 3);
            this.maskedEditBox2.Mask = "####/##/##";
            this.maskedEditBox2.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.maskedEditBox2.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.maskedEditBox2.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.maskedEditBox2.Modified = false;
            this.maskedEditBox2.Name = "maskedEditBox2";
            this.maskedEditBox2.PaddingCharacter = '_';
            this.maskedEditBox2.PassivePromptCharacter = '_';
            this.maskedEditBox2.PromptCharacter = '_';
            this.maskedEditBox2.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.maskedEditBox2.ShowToDay = true;
            this.maskedEditBox2.ShowTodayCircle = true;
            this.maskedEditBox2.ShowUpDown = false;
            this.maskedEditBox2.Size = new System.Drawing.Size(85, 21);
            this.maskedEditBox2.SunDayColor = System.Drawing.Color.Red;
            this.maskedEditBox2.TabIndex = 1;
            this.maskedEditBox2.TitleBackColor = System.Drawing.SystemColors.Control;
            this.maskedEditBox2.TitleForeColor = System.Drawing.Color.Black;
            this.maskedEditBox2.ToDayColor = System.Drawing.Color.Red;
            this.maskedEditBox2.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.maskedEditBox2.UseKeyF3 = false;
            this.maskedEditBox2.Value = new System.DateTime(((long)(0)));
            this.maskedEditBox2.Validated += new System.EventHandler(this.maskedEditBox2_Validated);
            // 
            // maskedEditBox1
            // 
            this.maskedEditBox1.CalendarBackColor = System.Drawing.Color.White;
            this.maskedEditBox1.DayColor = System.Drawing.SystemColors.ControlText;
            this.maskedEditBox1.FriDayColor = System.Drawing.Color.Blue;
            this.maskedEditBox1.Location = new System.Drawing.Point(91, 3);
            this.maskedEditBox1.Mask = "####/##/##";
            this.maskedEditBox1.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.maskedEditBox1.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.maskedEditBox1.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.maskedEditBox1.Modified = false;
            this.maskedEditBox1.Name = "maskedEditBox1";
            this.maskedEditBox1.PaddingCharacter = '_';
            this.maskedEditBox1.PassivePromptCharacter = '_';
            this.maskedEditBox1.PromptCharacter = '_';
            this.maskedEditBox1.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.maskedEditBox1.ShowToDay = true;
            this.maskedEditBox1.ShowTodayCircle = true;
            this.maskedEditBox1.ShowUpDown = false;
            this.maskedEditBox1.Size = new System.Drawing.Size(85, 21);
            this.maskedEditBox1.SunDayColor = System.Drawing.Color.Red;
            this.maskedEditBox1.TabIndex = 0;
            this.maskedEditBox1.TitleBackColor = System.Drawing.SystemColors.Control;
            this.maskedEditBox1.TitleForeColor = System.Drawing.Color.Black;
            this.maskedEditBox1.ToDayColor = System.Drawing.Color.Red;
            this.maskedEditBox1.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.maskedEditBox1.UseKeyF3 = false;
            this.maskedEditBox1.Value = new System.DateTime(((long)(0)));
            this.maskedEditBox1.Validated += new System.EventHandler(this.maskedEditBox1_Validated);
            // 
            // cb_cd_plant
            // 
            this.cb_cd_plant.AutoDropDown = true;
            this.cb_cd_plant.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cb_cd_plant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_cd_plant.Location = new System.Drawing.Point(370, 3);
            this.cb_cd_plant.Name = "cb_cd_plant";
            this.cb_cd_plant.ShowCheckBox = false;
            this.cb_cd_plant.Size = new System.Drawing.Size(160, 20);
            this.cb_cd_plant.TabIndex = 2;
            this.cb_cd_plant.UseKeyEnter = false;
            this.cb_cd_plant.UseKeyF3 = false;
            this.cb_cd_plant.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonComboBox_KeyEvent);
            // 
            // cb_fg_gr
            // 
            this.cb_fg_gr.AutoDropDown = true;
            this.cb_fg_gr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_fg_gr.Location = new System.Drawing.Point(620, 29);
            this.cb_fg_gr.Name = "cb_fg_gr";
            this.cb_fg_gr.ShowCheckBox = false;
            this.cb_fg_gr.Size = new System.Drawing.Size(160, 20);
            this.cb_fg_gr.TabIndex = 6;
            this.cb_fg_gr.UseKeyEnter = false;
            this.cb_fg_gr.UseKeyF3 = false;
            this.cb_fg_gr.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonComboBox_KeyEvent);
            // 
            // cb_fg_po_tr
            // 
            this.cb_fg_po_tr.AutoDropDown = true;
            this.cb_fg_po_tr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_fg_po_tr.Location = new System.Drawing.Point(370, 29);
            this.cb_fg_po_tr.Name = "cb_fg_po_tr";
            this.cb_fg_po_tr.ShowCheckBox = false;
            this.cb_fg_po_tr.Size = new System.Drawing.Size(160, 20);
            this.cb_fg_po_tr.TabIndex = 5;
            this.cb_fg_po_tr.UseKeyEnter = false;
            this.cb_fg_po_tr.UseKeyF3 = false;
            this.cb_fg_po_tr.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonComboBox_KeyEvent);
            // 
            // panel12
            // 
            this.panel12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel12.BackColor = System.Drawing.Color.Transparent;
            this.panel12.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel12.BackgroundImage")));
            this.panel12.Location = new System.Drawing.Point(5, 51);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(777, 1);
            this.panel12.TabIndex = 22;
            // 
            // panel11
            // 
            this.panel11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel11.BackColor = System.Drawing.Color.Transparent;
            this.panel11.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel11.BackgroundImage")));
            this.panel11.Location = new System.Drawing.Point(5, 26);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(777, 1);
            this.panel11.TabIndex = 21;
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel10.Controls.Add(this.lb_fg_gr);
            this.panel10.Controls.Add(this.lb_nm_sl);
            this.panel10.Location = new System.Drawing.Point(532, 1);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(85, 49);
            this.panel10.TabIndex = 3;
            // 
            // lb_fg_gr
            // 
            this.lb_fg_gr.BackColor = System.Drawing.Color.Transparent;
            this.lb_fg_gr.Location = new System.Drawing.Point(3, 30);
            this.lb_fg_gr.Name = "lb_fg_gr";
            this.lb_fg_gr.Resizeble = true;
            this.lb_fg_gr.Size = new System.Drawing.Size(80, 18);
            this.lb_fg_gr.TabIndex = 0;
            this.lb_fg_gr.Text = "출고구분";
            this.lb_fg_gr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_nm_sl
            // 
            this.lb_nm_sl.BackColor = System.Drawing.Color.Transparent;
            this.lb_nm_sl.Location = new System.Drawing.Point(3, 5);
            this.lb_nm_sl.Name = "lb_nm_sl";
            this.lb_nm_sl.Resizeble = true;
            this.lb_nm_sl.Size = new System.Drawing.Size(80, 18);
            this.lb_nm_sl.TabIndex = 19;
            this.lb_nm_sl.Text = "창고";
            this.lb_nm_sl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel9.Controls.Add(this.lb_fg_po_tr);
            this.panel9.Controls.Add(this.lb_gr_plant);
            this.panel9.Controls.Add(this.lb_no_emp);
            this.panel9.Location = new System.Drawing.Point(282, 1);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(85, 75);
            this.panel9.TabIndex = 2;
            // 
            // lb_fg_po_tr
            // 
            this.lb_fg_po_tr.BackColor = System.Drawing.Color.Transparent;
            this.lb_fg_po_tr.Location = new System.Drawing.Point(16, 30);
            this.lb_fg_po_tr.Name = "lb_fg_po_tr";
            this.lb_fg_po_tr.Resizeble = true;
            this.lb_fg_po_tr.Size = new System.Drawing.Size(64, 18);
            this.lb_fg_po_tr.TabIndex = 0;
            this.lb_fg_po_tr.Text = "거래구분";
            this.lb_fg_po_tr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_gr_plant
            // 
            this.lb_gr_plant.BackColor = System.Drawing.Color.Transparent;
            this.lb_gr_plant.Location = new System.Drawing.Point(3, 5);
            this.lb_gr_plant.Name = "lb_gr_plant";
            this.lb_gr_plant.Resizeble = true;
            this.lb_gr_plant.Size = new System.Drawing.Size(80, 18);
            this.lb_gr_plant.TabIndex = 0;
            this.lb_gr_plant.Text = "공장명";
            this.lb_gr_plant.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_no_emp
            // 
            this.lb_no_emp.BackColor = System.Drawing.Color.Transparent;
            this.lb_no_emp.Location = new System.Drawing.Point(-1, 56);
            this.lb_no_emp.Name = "lb_no_emp";
            this.lb_no_emp.Resizeble = true;
            this.lb_no_emp.Size = new System.Drawing.Size(80, 18);
            this.lb_no_emp.TabIndex = 1;
            this.lb_no_emp.Text = "담당자";
            this.lb_no_emp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel7.Controls.Add(this.lb_dt_rcv);
            this.panel7.Controls.Add(this.lb_gr_partner);
            this.panel7.Controls.Add(this.lb_rcv_pt);
            this.panel7.Location = new System.Drawing.Point(1, 1);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(85, 75);
            this.panel7.TabIndex = 0;
            // 
            // lb_dt_rcv
            // 
            this.lb_dt_rcv.BackColor = System.Drawing.Color.Transparent;
            this.lb_dt_rcv.Location = new System.Drawing.Point(3, 5);
            this.lb_dt_rcv.Name = "lb_dt_rcv";
            this.lb_dt_rcv.Resizeble = true;
            this.lb_dt_rcv.Size = new System.Drawing.Size(80, 18);
            this.lb_dt_rcv.TabIndex = 0;
            this.lb_dt_rcv.Text = "기간";
            this.lb_dt_rcv.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_gr_partner
            // 
            this.lb_gr_partner.BackColor = System.Drawing.Color.Transparent;
            this.lb_gr_partner.Location = new System.Drawing.Point(3, 30);
            this.lb_gr_partner.Name = "lb_gr_partner";
            this.lb_gr_partner.Resizeble = true;
            this.lb_gr_partner.Size = new System.Drawing.Size(80, 18);
            this.lb_gr_partner.TabIndex = 0;
            this.lb_gr_partner.Text = "거래처명";
            this.lb_gr_partner.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_rcv_pt
            // 
            this.lb_rcv_pt.BackColor = System.Drawing.Color.Transparent;
            this.lb_rcv_pt.Location = new System.Drawing.Point(3, 55);
            this.lb_rcv_pt.Name = "lb_rcv_pt";
            this.lb_rcv_pt.Resizeble = true;
            this.lb_rcv_pt.Size = new System.Drawing.Size(80, 18);
            this.lb_rcv_pt.TabIndex = 0;
            this.lb_rcv_pt.Text = "출고형태";
            this.lb_rcv_pt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(176, 5);
            this.label2.Name = "label2";
            this.label2.Resizeble = true;
            this.label2.Size = new System.Drawing.Size(17, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "~";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this._flexL);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(787, 263);
            this.panel6.TabIndex = 0;
            // 
            // _flexL
            // 
            this._flexL.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexL.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexL.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexL.AutoResize = false;
            this._flexL.ColumnInfo = "1,1,0,0,0,75,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
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
            this._flexL.Size = new System.Drawing.Size(787, 263);
            this._flexL.StyleInfo = resources.GetString("_flexL.StyleInfo");
            this._flexL.TabIndex = 0;
            // 
            // dSet_qtioH
            // 
            this.dSet_qtioH.DataSetName = "NewDataSet";
            this.dSet_qtioH.Locale = new System.Globalization.CultureInfo("ko-KR");
            this.dSet_qtioH.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable1});
            // 
            // dataTable1
            // 
            this.dataTable1.TableName = "MM_QTIOH";
            // 
            // dSet_qtioL
            // 
            this.dSet_qtioL.DataSetName = "NewDataSet";
            this.dSet_qtioL.Locale = new System.Globalization.CultureInfo("ko-KR");
            this.dSet_qtioL.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable2});
            // 
            // dataTable2
            // 
            this.dataTable2.TableName = "MM_QTIO";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 59);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(793, 561);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 88);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel5);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel6);
            this.splitContainer1.Size = new System.Drawing.Size(787, 470);
            this.splitContainer1.SplitterDistance = 203;
            this.splitContainer1.TabIndex = 1;
            // 
            // P_SA_GIM_REG_BAK
            // 
            this.Controls.Add(this.tableLayoutPanel1);
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Name = "P_SA_GIM_REG_BAK";
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.maskedEditBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maskedEditBox1)).EndInit();
            this.panel10.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSet_qtioH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSet_qtioL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable2)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		#region -> 생성자(2)

		public P_SA_GIM_REG_BAK(string sdt_dt, string edt_dt, string ps_cd_partner,string ps_nm_partner,string ps_cd_gi_plant,string ps_nm_gi_plant,string ps_cdsl,string ps_nmsl,string ps_noemp,string ps_nmkr)
		{

			InitializeComponent();
			
			this.Load += new System.EventHandler(Page_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(Page_Paint);		
		}

		#endregion

		#endregion
	
		#region ♣ 초기화 이벤트 / 메소드

		#region -> Page_Load 

		private void Page_Load(object sender, EventArgs e)
		{
			try
			{				
				this.Enabled = false;

				// 그리드 초기화
				this.InitGridH();
				this.InitGridL();
				// 컨트롤 초기화
				InitControl();
				
				m_page_state = "Deleted";

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
					case "S":				// 1.체크박스
						temp = temp + " + " + this.GetDataDictionaryItem("SA","S");
						break;
					case "NO_IO":			// 2.수불번호
						temp = temp + " + " + this.GetDataDictionaryItem("PU","NO_IO");
						break;
					case "DT_IO":			// 3.수불일
						temp = temp + " + " + this.GetDataDictionaryItem("PU","DT_IO");
						break;
					case "CD_PARTNER":		// 4.거래코드
						temp = temp + " + " + this.GetDataDictionaryItem("PU","CD_PARTNER");
						break;
					case "LN_PARTNER":		// 5.거래처명
						temp = temp + " + " + this.GetDataDictionaryItem("PU","NM_PARTNER");
						break;
					case "NM_YN_RETURN":	// 6.입고구분
						temp = temp + " + " + this.GetDataDictionaryItem("PU","FG_GI");
						break;
					case "NM_CD_EXCH":		// 7.환종
						temp = temp + " + " + this.GetDataDictionaryItem("PU","CD_EXCH");
						break;
					case "YN_AM":			// 8.유무환구분
						temp = temp + " + " + this.GetDataDictionaryItem("PU","YN_AM");
						break;
					case "NM_PLANT":		// 9.공장명
						temp = temp + " + " + this.GetDataDictionaryItem("PU","NM_PLANT");
						break;
					case "NM_KOR":			// 10.담당자
						temp = temp + " + " + this.GetDataDictionaryItem("PU","NO_EMP");
						break;
					case "DC_RMK":			// 11.비고
						temp = temp + " + " + this.GetDataDictionaryItem("PU","DC");
						break;

						// Line Grid의 컬럼이름
					case "CD_ITEM":		// 2.품목코드
						temp = temp + " + " + this.GetDataDictionaryItem("PU","CD_ITEM");
						break;
					case "NM_ITEM":		// 3.품목명
						temp = temp + " + " + this.GetDataDictionaryItem("PU","NM_ITEM");
						break;
					case "STND_ITEM":	// 4.규격
						temp = temp + " + " + this.GetDataDictionaryItem("PU","STND_ITEM");
						break;
					case "CD_UNIT_MM":	// 5.수배단위
						temp = temp + " + " + this.GetDataDictionaryItem("SA","UNIT_SO");
						break;
					case "NO_LOT":		// 6. LOT여부
						temp = temp + " + " + this.GetDataDictionaryItem("PU","YN_LOT");
						break;
					case "NM_FG_TRANS":	// 7. 거래구분
						temp = temp + " + " + this.GetDataDictionaryItem("PU","FG_PO_TR");
						break;
					case "NM_SL":		// 8. 창고
						temp = temp + " + " + this.GetDataDictionaryItem("PU","NM_SL");
						break;
					case "QT_UNIT_MM":	// 9.수불수량
						temp = temp + " + " + this.GetDataDictionaryItem("PU","QT_IO");
						break;
					case "UNIT_IM":		// 10. 재고단위
						temp = temp + " + " + this.GetDataDictionaryItem("PU","UNIT_IM");
						break;
					case "QT_GOOD_INV":	// 11. 양품수량
						temp = temp + " + " + this.GetDataDictionaryItem("PU","QT_GOOD");
						break;
					case "QT_REJECT_INV":// 12.불량수량
						temp = temp + " + " + this.GetDataDictionaryItem("PU","QT_INFERIOR");
						break;
					case "AM":			// 13.원화금액
						temp = temp + " + " + this.GetDataDictionaryItem("PU","AM_K");
						break;
					case "QT_CLS":		// 14.마감수량
						temp = temp + " + " + this.GetDataDictionaryItem("PU","QT_CLS");
						break;
					case "NO_ISURCV":	// 15.의뢰번호
						temp = temp + " + " + this.GetDataDictionaryItem("PU","NO_REQ");
						break;
					case "NO_PSO_MGMT":	// 16.발주번호
						temp = temp + " + " + this.GetDataDictionaryItem("PU","NO_PO");
						break;
                    case "NM_TP_GI":	// 출하형태
                        temp = temp + " + " + this.GetDataDictionaryItem("SA", "TP_GI");
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
			_flexH.Cols.Count = 13;
			_flexH.Cols.Fixed = 1;
			_flexH.Rows.DefaultSize = 20;

			_flexH.Cols[0].Width = 50;

			// 1.체크박스
			_flexH.Cols[1].Name = "S";
			_flexH.Cols[1].DataType = typeof(string);
			_flexH.Cols[1].Width = 40;
			_flexH.Cols[1].Format = "1;0";
			_flexH.Cols[1].ImageAlign = ImageAlignEnum.CenterCenter;

			// 2.수불번호
			_flexH.Cols[2].Name = "NO_IO";
			_flexH.Cols[2].DataType = typeof(string);
			_flexH.Cols[2].Width = 100;
		
			// 3.의뢰일자
			_flexH.Cols[3].Name = "DT_IO";
			_flexH.Cols[3].DataType = typeof(string);
			_flexH.Cols[3].Width = 70;
			_flexH.Cols[3].EditMask = this.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT);
			_flexH.Cols[3].Format = _flexH.Cols[3].EditMask;			
			_flexH.SetStringFormatCol("DT_IO");
			_flexH.SetNoMaskSaveCol("DT_IO");

			// 4.CD_PARTNER
			_flexH.Cols[4].Name = "CD_PARTNER";
			_flexH.Cols[4].DataType = typeof(string);
			_flexH.Cols[4].Width = 70;
		
			// 5.거래처명
			_flexH.Cols[5].Name = "LN_PARTNER";
			_flexH.Cols[5].DataType = typeof(string);
			_flexH.Cols[5].Width = 150;
			
			// 6.입고구분
			_flexH.Cols[6].Name = "NM_YN_RETURN";
			_flexH.Cols[6].DataType = typeof(string);
			_flexH.Cols[6].Width = 70;
			
			// 7.환종
			_flexH.Cols[7].Name = "NM_CD_EXCH";
			_flexH.Cols[7].DataType = typeof(string);
			_flexH.Cols[7].Width = 0;
		
			// 8.유무환구분
			_flexH.Cols[8].Name = "YN_AM";
			_flexH.Cols[8].DataType = typeof(string);
			_flexH.Cols[8].Width = 80;
			
			// 9.공장명
			_flexH.Cols[9].Name = "NM_PLANT";
			_flexH.Cols[9].DataType = typeof(string);
			_flexH.Cols[9].Width = 0;
			
			// 10.담당자
			_flexH.Cols[10].Name = "NM_KOR";
			_flexH.Cols[10].DataType = typeof(string);
			_flexH.Cols[10].Width = 70;

            // 11.출하형태
            _flexH.Cols[11].Name = "NM_TP_GI";
            _flexH.Cols[11].DataType = typeof(string);
            _flexH.Cols[11].Width = 100;
		
			// 12.비고
			_flexH.Cols[12].Name = "DC_RMK";
			_flexH.Cols[12].DataType = typeof(string);
			_flexH.Cols[12].Width = 200;
		
			_flexH.AllowSorting = AllowSortingEnum.MultiColumn;
			_flexH.NewRowEditable = false;
			_flexH.EnterKeyAddRow = false;

			_flexH.SumPosition = SumPositionEnum.None;
			_flexH.GridStyle = GridStyleEnum.Green;
			_flexH.Styles.Fixed.TextAlign = TextAlignEnum.CenterCenter;

			this.SetUserGrid(_flexH);

			// 그리드 헤더캡션 표시하기
			for(int i = 0; i <= _flexH.Cols.Count-1; i++)
				_flexH[0, i] = GetDDItem(_flexH.Cols[i].Name);
			
			_flexH.Redraw = true;

			// 그리드 이벤트 선언
			_flexH.AfterRowColChange	+= new C1.Win.C1FlexGrid.RangeEventHandler(_flexH_AfterRowColChange);
			_flexH.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);
		}

		#endregion

		#region -> InitGridL

		private void InitGridL()
		{
			
			Application.DoEvents();
			
			_flexL.Redraw = false;

			_flexL.Rows.Count = 1;
			_flexL.Rows.Fixed = 1;
			_flexL.Cols.Count = 17;
			_flexL.Cols.Fixed = 1;
			_flexL.Rows.DefaultSize = 20;

			_flexL.Cols[0].Width = 50;

			// 1.체크박스
			_flexL.Cols[1].Name = "S";
			_flexL.Cols[1].DataType = typeof(string);
			_flexL.Cols[1].Width = 40;
			_flexL.Cols[1].Format = "Y;N";
			_flexL.Cols[1].ImageAlign = ImageAlignEnum.CenterCenter;
			
			// 2.품목코드
			_flexL.Cols[2].Name = "CD_ITEM";
			_flexL.Cols[2].DataType = typeof(string);
			_flexL.Cols[2].Width = 80;
		
			// 3.품목명
			_flexL.Cols[3].Name = "NM_ITEM";
			_flexL.Cols[3].DataType = typeof(string);
			_flexL.Cols[3].Width = 80;
			
			// 4.규격
			_flexL.Cols[4].Name = "STND_ITEM";
			_flexL.Cols[4].DataType = typeof(string);
			_flexL.Cols[4].Width = 80;

			// 5.수배단위
			_flexL.Cols[5].Name = "CD_UNIT_MM";
			_flexL.Cols[5].DataType = typeof(string);
			_flexL.Cols[5].Width = 60;
	
			// 6.LOT여부
			_flexL.Cols[6].Name = "NO_LOT";
			_flexL.Cols[6].DataType = typeof(string);
			_flexL.Cols[6].Width = 60;

			// 7.거래구분
			_flexL.Cols[7].Name = "NM_FG_TRANS";
			_flexL.Cols[7].DataType = typeof(string);
			_flexL.Cols[7].Width = 60;

			// 8.창고
			_flexL.Cols[8].Name = "NM_SL";
			_flexL.Cols[8].DataType = typeof(string);
			_flexL.Cols[8].Width = 80;

			// 9.수불수량
			_flexL.Cols[9].Name = "QT_UNIT_MM";
			_flexL.Cols[9].DataType = typeof(decimal);
			_flexL.Cols[9].Width = 70;
			this.SetFormat(_flexL.Cols[9], DataDictionaryTypes.SA, FormatTpType.UNIT_COST, FormatFgType.INSERT);
			
			// 10.재고단위
			_flexL.Cols[10].Name = "UNIT_IM";
			_flexL.Cols[10].DataType = typeof(string);
			_flexL.Cols[10].Width = 70;

			// 11.양품수량
			_flexL.Cols[11].Name = "QT_GOOD_INV";
			_flexL.Cols[11].DataType = typeof(decimal);
			_flexL.Cols[11].Width = 80;
			this.SetFormat(_flexL.Cols[10], DataDictionaryTypes.SA, FormatTpType.QUANTITY, FormatFgType.SELECT);

			// 12.불량수량
			_flexL.Cols[12].Name = "UNIT_IM";
			_flexL.Cols[12].DataType = typeof(decimal);
			_flexL.Cols[12].Width = 80;
			_flexL.SetColMaxLength("UNIT_IM", 17);
			this.SetFormat(_flexL.Cols[12], DataDictionaryTypes.SA, FormatTpType.QUANTITY, FormatFgType.INSERT);
			_flexL.Cols[12].Visible = false;

			// 13.원화금액
			_flexL.Cols[13].Name = "AM";
			_flexL.Cols[13].DataType = typeof(decimal);
			_flexL.Cols[13].Width = 80;
			this.SetFormat(_flexL.Cols[13], DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.SELECT);

			// 14.마감수량
			_flexL.Cols[14].Name = "QT_CLS";
			_flexL.Cols[14].DataType = typeof(decimal);
			_flexL.Cols[14].Width = 80;
			this.SetFormat(_flexL.Cols[14], DataDictionaryTypes.SA, FormatTpType.QUANTITY, FormatFgType.SELECT);

			// 15.의뢰번호
			_flexL.Cols[15].Name = "NO_ISURCV";
			_flexL.Cols[15].DataType = typeof(string);
			_flexL.Cols[15].Width = 100;

			// 16.발주번호
			_flexL.Cols[16].Name = "NO_PSO_MGMT";
			_flexL.Cols[16].DataType = typeof(string);
			_flexL.Cols[16].Width = 100;

			_flexL.AllowSorting = AllowSortingEnum.MultiColumn;
			_flexL.NewRowEditable = true;
			_flexL.EnterKeyAddRow = false;
						
			_flexL.SumPosition = SumPositionEnum.None;
			_flexL.GridStyle = GridStyleEnum.Blue;
			_flexL.Styles.Fixed.TextAlign = TextAlignEnum.CenterCenter;
			
			this.SetUserGrid(_flexL);

			// 그리드 헤더캡션 표시하기
			for(int i = 0; i <= _flexL.Cols.Count-1; i++)
				_flexL[0, i] = GetDDItem(_flexL.Cols[i].Name);
			
			_flexL.Redraw = true;

			// 그리드 이벤트 선언
			_flexL.AfterDataRefresh		+= new System.ComponentModel.ListChangedEventHandler(_flexL_AfterDataRefresh);
			_flexL.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);
		}

		#endregion

		#region -> InitControl

		private void InitControl()
		{
			try
			{
				//마스크 셋팅
				this.maskedEditBox1.Mask	= this.GetFormatDescription(DataDictionaryTypes.SA,FormatTpType.YEAR_MONTH_DAY,FormatFgType.INSERT);
				this.maskedEditBox2.Mask	= this.GetFormatDescription(DataDictionaryTypes.SA,FormatTpType.YEAR_MONTH_DAY,FormatFgType.INSERT);
		
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
									if(ctrls is Duzon.Common.Controls.LabelExt)
										((Duzon.Common.Controls.LabelExt)ctrls).Text = GetDataDictionaryItem(DataDictionaryTypes.SA, (string)((Duzon.Common.Controls.LabelExt)ctrls).Tag);
								}
							}
						}
					}
				}
				//각 라벨 초기화
				lb_dt_rcv.Text		= this.MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.SA, "DT_QTIO");		//출고일자
				lb_gr_plant.Text	= this.MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.SA, "GI_PLANT");	//출고공장
				lb_nm_sl.Text		= this.MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.SA, "NM_SL");		//창고
				lb_gr_partner.Text	= this.MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.SA, "NM_PARTNER");	//거래처명
				lb_fg_po_tr.Text	= this.MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.SA, "TP_BUSI");		//거래구분
				lb_fg_gr.Text		= this.MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.SA, "FG_GI");		//출고구분
				lb_rcv_pt.Text		= this.MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.SA, "TP_GI");		//출고형태			
				lb_no_emp.Text		= this.MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.SA, "NO_EMP");		//담당자
				//			lb_fg_cls.Text		= this.MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.SA, "FG_CLOSE");		//마감구분

				/*************************************************/

				bpNm_Partner.Text="";
				bpNm_Partner.Tag="";
	
				cb_cd_plant.SelectedValue="";
				bpRcv_Pt.Text="";
				bpRcv_Pt.Tag="";
				bpNm_Sl.Text="";
				bpNm_Sl.Tag="";			
				bpNo_Emp.Text="";
				bpNo_Emp.Tag="";				

				DataSet ds_Combo = this.GetComboData("N;MA_PLANT","S;PU_C000016", "S;PU_C000027","N;PU_C000029");

				// 공장 콤보
				cb_cd_plant.DataSource		= ds_Combo.Tables[0].DefaultView;
				cb_cd_plant.DisplayMember	= "NAME";
				cb_cd_plant.ValueMember		= "CODE";

				//거래구분
				cb_fg_po_tr.DataSource		= ds_Combo.Tables[1].DefaultView;
				cb_fg_po_tr.DisplayMember	= "NAME";
				cb_fg_po_tr.ValueMember		= "CODE";

				//입고구분
				cb_fg_gr.DataSource			= ds_Combo.Tables[2].DefaultView;
				cb_fg_gr.DisplayMember		= "NAME";
				cb_fg_gr.ValueMember		= "CODE";
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
				this._isPainted = true; //로드 된적이 있다.
				Application.DoEvents();

				this.Enabled = true;

				bpNo_Emp.CodeName = LoginInfo.EmployeeName;
				bpNo_Emp.CodeValue = LoginInfo.EmployeeNo;

				//시스템 날짜
				string ls_day = MainFrameInterface.GetStringToday;
				
				maskedEditBox1.Text = ls_day.Substring(0,6)+"01";
				maskedEditBox2.Text = ls_day.Substring(0,8);
					
				maskedEditBox1.Select();

				this.SetProgressBarValue(100, 100);
			}
			catch(Exception ex)
			{
				MsgEnd(ex);
			}
			finally
			{
				this.SetToolBarButtonState(true,false, true, false,false);				
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
			try
			{
				object[] m_obj = new object[2];

				m_obj[0] = m_page_state;
				m_obj[1] = gds_dzdwGrid2_DEL.Tables[0];							
						
				//int rtn = (int)(InvokeRemoteMethod("PurStorageControl", "pur.CC_PU_GRM", "CC_PU_GRM.rem", "SaveGrm", m_obj));
                int rtn = SaveGrm(m_page_state, gds_dzdwGrid2_DEL.Tables[0]);
			
				if(rtn >= 0)
				{
					_flexH.DataTable.AcceptChanges();
					_flexL.DataTable.AcceptChanges();
					
					ShowMessageBox(1);

					// 페이지의 상태를 수정으로 변경
					m_page_state = "Deleted";

					// 버턴 처리
					ToolBarDeleteButtonEnabled = false;
					ToolBarSaveButtonEnabled = true;
					return true;
				}
			}
			catch(Exception ex)
			{
				MsgEnd(ex);								
			}
			
			return false;	
		}

        public int SaveGrm(string ps_state, DataTable pdt_Delete)
        {
            try
            {
                int li_result = 0;
                if (ps_state == "Deleted")
                {
                    if (pdt_Delete != null)
                    {
                        if (pdt_Delete.Rows.Count > 0)
                        {
                            DataRow pdr_row;

                            for (int j = 0; j < pdt_Delete.Rows.Count; j++)
                            {
                                pdr_row = pdt_Delete.Rows[j];

                                if (pdr_row.RowState.ToString() == "Deleted")
                                    continue;

                                li_result = Delete_RCV(pdr_row["NO_IO"].ToString().Trim(), pdr_row["CD_COMPANY"].ToString().Trim());
                                if (li_result < 0)
                                {
                                    ApplicationException lex = new ApplicationException("CM_M100010");
                                    lex.Source = "100000";
                                    lex.HelpLink = "SaveGrm : m_cc_mmqtio.DeleteMMQTIO";
                                    throw lex;
                                }
                            }
                        }
                    }
                }

                return li_result;
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                ex.HelpLink = "SaveGR";
                throw ex;
            }
        }

        private int Delete_RCV(string ano_io, string acd_company)
        {
            try
            {
                int li_result = 0;

                SpInfo si = new SpInfo();
                si.SpNameSelect = "UP_PU_GRM_MM_QTIO_SELECT";
                si.SpParamsSelect = new object[]{acd_company, ano_io};
                ResultData result = (ResultData)this.FillDataTable(si);
                DataTable ldt_QTIO = (DataTable)result.DataValue;

                if (ldt_QTIO != null && ldt_QTIO.Rows.Count > 0)
                {
                    if (ldt_QTIO.Rows[0]["YN_RETURN"].ToString().Trim() != "Y")
                    {
                        li_result = Check_PURCV_GR(ldt_QTIO, true);
                        if (li_result < 0)
                        {
                            ApplicationException lex = new ApplicationException("CM_M100010");
                            lex.Source = "100000";
                            lex.HelpLink = "m_cc_mmqtio.DeleteMMQTIOH";
                            throw lex;
                        }

                        li_result = Check_PUPO_GR(ldt_QTIO, true);
                        if (li_result < 0)
                        {
                            ApplicationException lex = new ApplicationException("CM_M100010");
                            lex.Source = "100000";
                            lex.HelpLink = "m_cc_mmqtio.DeleteMMQTIOH";
                            throw lex;
                        }
                    }
                    else
                    {
                        li_result = Check_PURCV_GR(ldt_QTIO, true);
                        if (li_result < 0)
                        {
                            ApplicationException lex = new ApplicationException("CM_M100010");
                            lex.Source = "100000";
                            lex.HelpLink = "m_cc_mmqtio.DeleteMMQTIOH";
                            throw lex;
                        }

                        li_result = Check_PUPO_GRR(ldt_QTIO, true);
                        if (li_result < 0)
                        {
                            ApplicationException lex = new ApplicationException("CM_M100010");
                            lex.Source = "100000";
                            lex.HelpLink = "m_cc_mmqtio.DeleteMMQTIOH";
                            throw lex;
                        }
                    }
                }

                li_result = DeleteMMQTIOH(ano_io, acd_company);

                if (li_result < 0)
                {
                    ApplicationException lex = new ApplicationException("CM_M100010");
                    lex.Source = "100000";
                    lex.HelpLink = "m_cc_mmqtio.DeleteMMQTIOH";
                    throw lex;
                }

                return li_result;
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                ex.HelpLink = "SaveGR";
                throw ex;
            }
        }

        public int Check_PURCV_GR(DataTable pdt_Line, bool isDeleted)
        {
            try
            {
                int li_result = 0;

                DataRow ps_row;

                for (int li_i = 0; li_i < pdt_Line.Rows.Count; li_i++)
                {
                    ps_row = pdt_Line.Rows[li_i];

                    DataTable ldt_PuReq = SelectGrreq(ps_row["CD_COMPANY"].ToString(),
                        ps_row["NO_ISURCV"].ToString(),
                        ps_row["NO_ISURCVLINE"].ToString());

                    double ldb_qtreq = 0;
                    double ldb_qtgr = 0;
                    double ldb_qtpass = 0;
                    double ldb_qtreq_mm = 0;
                    double qt_gr_tot = 0;
                    double qt_req_mm = 0;
                    double ldb_qtgr_mm = 0;
                    double ldb_vat = 0;
                    string ldb_yn_insp = "";

                    if (ldt_PuReq != null && ldt_PuReq.Rows.Count > 0)
                    {
                        //db값
                        ldb_qtreq = System.Double.Parse(ldt_PuReq.Rows[0]["QT_REQ"].ToString());//의뢰량
                        ldb_qtgr = System.Double.Parse(ldt_PuReq.Rows[0]["QT_GR"].ToString()); //입고량

                        ldb_qtreq_mm = System.Double.Parse(ldt_PuReq.Rows[0]["QT_REQ_MM"].ToString());//수배의뢰량
                        ldb_qtgr_mm = System.Double.Parse(ldt_PuReq.Rows[0]["QT_GR_MM"].ToString()); //수배입고량

                        ldb_qtpass = System.Double.Parse(ldt_PuReq.Rows[0]["QT_PASS"].ToString()); //합격량
                        ldb_yn_insp = ldt_PuReq.Rows[0]["YN_INSP"].ToString(); //검사유무

                        // ui값
                        //					ldb_qtreq_mm = System.Double.Parse(ps_row["QT_MM"].ToString());//(수배)의뢰수량
                        qt_gr_tot = System.Double.Parse(ps_row["QT_GOOD_INV"].ToString());//양품입고량
                        qt_req_mm = System.Double.Parse(ps_row["QT_UNIT_MM"].ToString());//(수배)입고량
                        ldb_vat = System.Double.Parse(ps_row["VAT"].ToString());

                        if (isDeleted)
                        {
                            qt_req_mm = 0 - qt_req_mm;
                            qt_gr_tot = 0 - qt_gr_tot;
                            ldb_vat = 0 - ldb_vat;
                        }

                        if (ldb_yn_insp == "N" && !isDeleted)
                        {
                            double ldTot = ldb_qtgr + qt_gr_tot;

                            if (ldb_qtreq < ldTot)
                            {
                                //입고량이 의뢰량보다 많습니다.
                                ApplicationException lex = new ApplicationException("PU_M000020");
                                lex.Source = "100000";
                                lex.HelpLink = "ldb_qtreq < ldTot";
                                throw lex;
                            }
                        }

                        // RCV에 Update				
                        li_result = UpdateGrreq(System.Convert.ToString(qt_req_mm),
                            System.Convert.ToString(qt_gr_tot),
                            ps_row["CD_COMPANY"].ToString(),
                            ps_row["NO_ISURCV"].ToString(),
                            ps_row["NO_ISURCVLINE"].ToString(),
                            ps_row["DT_IO"].ToString(),
                            System.Convert.ToString(ldb_vat));

                        if (li_result < 0)
                        {
                            ApplicationException lex = new ApplicationException("PU_M000110");
                            lex.Source = "100000";
                            lex.HelpLink = "UpdateGrreq";
                            throw lex;
                        }
                    }
                    else
                    {
                        ApplicationException lex = new ApplicationException("PU_M000110");
                        lex.Source = "100000";
                        lex.HelpLink = "UpdateGrreq ;";
                        throw lex;
                    }
                }
                return li_result;
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectGrreq(string acd_company, string ano_rcv, string ano_line)
        {
            //SELECT QT_REQ, QT_GR, QT_GR_MM, QT_PASS, QT_REQ_MM, YN_INSP
            //FROM PU_RCVL
            //WHERE CD_COMPANY = '@'
            //    AND NO_RCV = '@'
            //    AND NO_LINE = @
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_PU_RCVL_SELECT";
            si.SpParamsSelect = new object[] { acd_company, ano_rcv, ano_line };
            ResultData result = (ResultData)this.FillDataTable(si);
            DataTable dtReturn = (DataTable)result.DataValue;

            return dtReturn;
        }

        public int UpdateGrreq(string aqt_req_mm, string aqt_gr, string ps_cdcompany, string ps_norcv, string ps_line, string ps_dtio, string vat)
        {
            try
            {
                //UPDATE PU_RCVL
                //SET QT_GR_MM = QT_GR_MM + @QT_REQ_MM
                //    , QT_GR = QT_GR + @QT_GR
                //    , DT_GRLAST = @DT_IO
                //    , VAT_CLS = VAT_CLS + @VAT
                //WHERE CD_COMPANY = @CD_COMPANY
                //    AND NO_RCV = @NO_RCV
                //    AND NO_LINE = @NO_LINE
                SpInfo si = new SpInfo();
                si.SpNameSelect = "UP_PU_RCVL_UPDATE";
                si.SpParamsSelect = new object[] { Convert.ToDecimal(aqt_req_mm.Trim())
                    , Convert.ToDecimal(aqt_gr.Trim())
                    , ps_dtio
                    , Convert.ToDecimal(vat.Trim())
                    , ps_cdcompany
                    , ps_norcv
                    , Convert.ToDecimal(ps_line.Trim())
                };
                ResultData result = (ResultData)this.FillDataTable(si);
                DataTable dtReturn = (DataTable)result.DataValue;

                return Convert.ToInt32(dtReturn.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Check_PUPO_GR(DataTable pdt_Line, bool isDeleted)
        {
            try
            {
                int li_result = 0;
                for (int li_i = 0; li_i < pdt_Line.Rows.Count; li_i++)
                {
                    if (pdt_Line.Rows[li_i]["NO_PSO_MGMT"].ToString().Trim() != null)
                    {
                        li_result = UpdataPol_GR(pdt_Line.Rows[li_i], isDeleted);
                        if (li_result < 0)
                        {
                            ApplicationException lex = new ApplicationException("PU_M000110");
                            lex.Source = "100000";
                            lex.HelpLink = "UpdateGrreq";
                            throw lex;
                        }
                    }
                }
                return li_result;
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdataPol_GR(DataRow pdr_Row, bool isDeleted)
        {
            try
            {
                int li_result = 0;

                string[] lsa_args = new string[8];

                int iQT_IO = Convert.ToInt32(pdr_Row["QT_IO"].ToString().Trim());
                int iQT_UNIT_MM = Convert.ToInt32(pdr_Row["QT_UNIT_MM"].ToString().Trim());

                if (isDeleted)
                {
                    iQT_IO = 0 - iQT_IO;
                    iQT_UNIT_MM = 0 - iQT_UNIT_MM;
                }

                SpInfo si = new SpInfo();
                si.SpNameSelect = "UP_PU_RCVL_UPDATE2";
                si.SpParamsSelect = new object[] { iQT_IO
                    , iQT_UNIT_MM
                    , pdr_Row["CD_COMPANY"].ToString()
                    , pdr_Row["NO_PSO_MGMT"].ToString()
                    , pdr_Row["NO_PSOLINE_MGMT"].ToString()
                };
                ResultData result = (ResultData)this.FillDataTable(si);
                DataTable dtReturn = (DataTable)result.DataValue;

                li_result = Convert.ToInt32(dtReturn.Rows[0][0].ToString());

                return li_result;
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Check_PUPO_GRR(DataTable pdt_Line, bool isDeleted)
        {
            try
            {
                int li_result = 0;

                for (int li_i = 0; li_i < pdt_Line.Rows.Count; li_i++)
                {
                    if (pdt_Line.Rows[li_i]["NO_PSO_MGMT"].ToString().Trim() != "")
                    {
                        li_result = UpdataPol_GRR(pdt_Line.Rows[li_i], isDeleted);
                        if (li_result < 0)
                        {
                            ApplicationException lex = new ApplicationException("PU_M000110");
                            lex.Source = "100000";
                            lex.HelpLink = "UpdateGrreq";
                            throw lex;
                        }
                    }
                }
                return li_result;
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdataPol_GRR(DataRow pdr_Row, bool isDeleted)
        {
            try
            {
                int li_result = 0;
                string[] lsa_args = new string[5];

                int iQT_IO = Convert.ToInt32(pdr_Row["QT_IO"].ToString().Trim());
                int iQT_UNIT_MM = Convert.ToInt32(pdr_Row["QT_UNIT_MM"].ToString().Trim());

                if (isDeleted)
                {
                    iQT_IO = 0 - iQT_IO;
                    iQT_UNIT_MM = 0 - iQT_UNIT_MM;
                }

                //UPDATE PU_POL
                //SET QT_RETURN = QT_RETURN + @QT_IO
                //    , QT_RETURN_MM = QT_RETURN_MM + @QT_UNIT_MM
                //WHERE CD_COMPANY = @CD_COMPANY
                //    AND NO_PO = @NO_PO
                //    AND NO_LINE = @NO_LINE
                SpInfo si = new SpInfo();
                si.SpNameSelect = "UP_PU_POL_UPDATE2";
                si.SpParamsSelect = new object[] { iQT_IO
                    , iQT_UNIT_MM
                    , pdr_Row["CD_COMPANY"].ToString()
                    , pdr_Row["NO_PSO_MGMT"].ToString()
                    , pdr_Row["NO_PSOLINE_MGMT"].ToString()
                };
                ResultData result = (ResultData)this.FillDataTable(si);
                DataTable dtReturn = (DataTable)result.DataValue;

                li_result = Convert.ToInt32(dtReturn.Rows[0][0].ToString());

                return li_result;
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteMMQTIOH(string ps_noio, string ps_cdcompany)
        {
            try
            {
                int li_return = 0;

                string[] lsa_args = new string[2];

                lsa_args[0] = ps_noio;
                lsa_args[1] = ps_cdcompany;

                SpInfo si = new SpInfo();
                si.SpNameSelect = "UP_PU_GRM_MM_QTIO_SELECT";
                si.SpParamsSelect = new object[] { ps_cdcompany, ps_noio };
                ResultData result = (ResultData)this.FillDataTable(si);
                DataTable dtLine = (DataTable)result.DataValue;

                li_return = DeleteAllCheak(dtLine);

                //DELETE MM_QTIOH
                //WHERE NO_IO = @NO_IO
                //    AND CD_COMPANY =@CD_COMPANY
                si = new SpInfo();
                si.SpNameSelect = "UP_PU_POL_DELETE";
                si.SpParamsSelect = new object[] { ps_cdcompany, ps_noio };
                ResultData result1 = (ResultData)this.FillDataTable(si);
                DataTable dtResult = (DataTable)result1.DataValue;

                li_return = Convert.ToInt32(dtResult.Rows[0][0].ToString());

                return li_return;
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                ex.HelpLink = "DeleteMMQTIOH";
                throw ex;
            }
        }

        private int DeleteAllCheak(DataTable pdt_QTIO)
        {
            try
            {
                //pur.ICC_MM_OHPLINV cc_mmohplinv = new CC_MM_OHPLINV();
                //pur.ICC_MM_OHSLINV cc_mmohslinv = new pur.CC_MM_OHSLINV();
                //pur.ICC_MM_OHSLINVM cc_mmohslinvm = new CC_MM_OHSLINVM();

                int li_result = 0;
                if (pdt_QTIO == null || pdt_QTIO.Rows.Count <= 0)
                {
                    return 0;
                }

                // 마감여부 검색 ( 회사코드, 공장코드, 수불일 )
                li_result = Check_CONTROL(pdt_QTIO);
                if (li_result < 0)
                {
                    ApplicationException lex = new ApplicationException("CM_M100010");
                    lex.Source = "100000";
                    lex.HelpLink = "Check_CONTROL";
                    throw lex;
                }

                DataRow ldr_row;
                for (int li_i = 0; li_i < pdt_QTIO.Rows.Count; li_i++)
                {
                    ldr_row = pdt_QTIO.Rows[li_i];
                    if (ldr_row.RowState.ToString() == "Deleted")
                        continue;

                    //  구매입고, 반품, 판매출고,반품,외주입고,반품일 경우 마감된것은 삭제 불가 
                    if ((ldr_row["FG_IO"].ToString().Trim() == "001" || //구매입고
                        ldr_row["FG_IO"].ToString().Trim() == "030" ||	// 구매반품
                        ldr_row["FG_IO"].ToString().Trim() == "010" ||	// 판매출고
                        ldr_row["FG_IO"].ToString().Trim() == "041" || //  판매반품
                        ldr_row["FG_IO"].ToString().Trim() == "005" || // 외주입고 20031124 김대영
                        ldr_row["FG_IO"].ToString().Trim() == "051") &&  // 외주입고반품
                        System.Double.Parse(ldr_row["QT_CLS"].ToString().Trim()) > 0)
                    {
                        ApplicationException lex = new ApplicationException("PU_M000056");
                        lex.Source = "100000";
                        lex.HelpLink = "Check_CONTROL";
                        throw lex;
                    }
                }

                // MM_OHSLINV 삭제
                //li_result = cc_mmohslinv.UpdateMM_OHSLINV_QtioDelete(pdt_QTIO);
                li_result = UpdateMM_OHSLINV_QtioDelete(pdt_QTIO);
                if (li_result < 0)
                {
                    ApplicationException lex = new ApplicationException("CM_M100010");
                    lex.Source = "100000";
                    lex.HelpLink = "Check_ISVL";
                    throw lex;
                }
                // MM_OHPLINV 삭제
                //li_result = cc_mmohplinv.UpdateMM_OHPLINV_QtioDelete(pdt_QTIO);
                li_result = UpdateMM_OHPLINV_QtioDelete(pdt_QTIO);
                if (li_result < 0)
                {
                    ApplicationException lex = new ApplicationException("CM_M100010");
                    lex.Source = "100000";
                    lex.HelpLink = "Check_ISVL";
                    throw lex;
                }
                // MM_OHSLINVM 삭제
                //li_result = cc_mmohslinvm.UpdateMM_OHSLINVM_QtioDelete(pdt_QTIO);
                li_result = UpdateMM_OHSLINVM_QtioDelete(pdt_QTIO);
                if (li_result < 0)
                {
                    ApplicationException lex = new ApplicationException("CM_M100010");
                    lex.Source = "100000";
                    lex.HelpLink = "Check_ISVL";
                    throw lex;
                }

                // 구매입고, 반품일 경우 매입집계쪽 입고량 수정
                if (pdt_QTIO.Rows[0]["FG_IO"].ToString().Trim() == "001" ||
                    pdt_QTIO.Rows[0]["FG_IO"].ToString().Trim() == "030")
                {
                    li_result = UpdatePU_APQtioDelete(pdt_QTIO);
                    if (li_result < 0)
                    {
                        ApplicationException lex = new ApplicationException("CM_M100010");
                        lex.Source = "100000";
                        lex.HelpLink = "m_cc_puap.UpdatePU_APQtioDelete";
                        throw lex;
                    }
                }

                //  MM_QTIO(라인) 삭제 -- 
                li_result = DeleteQtio_QtioDelete(pdt_QTIO);
                if (li_result < 0)
                {
                    ApplicationException lex = new ApplicationException("CM_M100010");
                    lex.Source = "100000";
                    lex.HelpLink = "Check_ISVL";
                    throw lex;
                }
                return li_result;
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                ex.HelpLink = "DeleteAllCheak";
                throw ex;
            }
        }

        public int DeleteQtio_QtioDelete(DataTable pdt_Line)
        {
            try
            {
                int li_result = 0;
                if (pdt_Line == null || pdt_Line.Rows.Count <= 0)
                    return li_result;

                DataRow ldr_row;
                SpInfoCollection siInfo = new SpInfoCollection();
                SpInfo si = new SpInfo();
                for (int li_i = 0; li_i < pdt_Line.Rows.Count; li_i++)
                {
                    ldr_row = pdt_Line.Rows[li_i];
                    if (ldr_row.RowState.ToString() == "Deleted")
                        continue;
                    //----------------------------------------

                    string[] obj_args = new string[3];

                    obj_args[0] = ldr_row["CD_COMPANY"].ToString();
                    obj_args[1] = ldr_row["NO_IO"].ToString();
                    obj_args[2] = ldr_row["NO_IOLINE"].ToString();

                    //DELETE MM_QTIO 
                    //WHERE NO_IO ='@' 
                    //    AND NO_IOLINE ='@' 
                    //    AND CD_COMPANY ='@'
                    si = new SpInfo();
                    si.SpNameSelect = "UP_MM_QTIO_DELETE";
                    si.SpParamsSelect = obj_args;
                    siInfo.Add(si);

                    //----------------------------------------
                }

                ResultData[] rs;
                if (siInfo.Count > 10) //ls_Query.Length > 10)
                {
                    rs = (ResultData[])this.Save(siInfo);
                    for (int i = 0; i < rs.Length; i++)
                    {
                        if (!rs[i].Result)
                        {
                            li_result = -1;
                            break;
                        }
                    }
                }
                return li_result;
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                ex.HelpLink = "DeleteAllCheak";
                throw ex;
            }
        }

        public int UpdatePU_APQtioDelete(DataTable pdt_Line)
        {
            try
            {
                int li_result = 0;
                if (pdt_Line == null || pdt_Line.Rows.Count <= 0)
                {
                    return 0;
                }

                StringBuilder ls_Query = new StringBuilder("");

                DataRow row;
                SpInfoCollection siInfo = new SpInfoCollection();
                SpInfo si = new SpInfo();

                object[] obj_args = new object[14];
                for (int j = 0; j < pdt_Line.Rows.Count; j++)
                {
                    row = pdt_Line.Rows[j];

                    // 유환이 아니면 
                    if (row["YN_AM"].ToString().Trim() != "Y")
                        continue;

                    // 구매입고 , 구매반품일 경우 
                    if (row["FG_IO"].ToString().Trim() == "001" ||
                        row["FG_IO"].ToString().Trim() == "030")
                    {

                        double ldb_am = 0 - System.Double.Parse(row["AM"].ToString().Trim());
                        double ldb_vat = 0 - System.Double.Parse(row["VAT"].ToString().Trim());
                        double ldb_qt = 0 - System.Double.Parse(row["QT_IO"].ToString().Trim());

                        obj_args[0] = row["CD_PARTNER"].ToString().Trim();
                        obj_args[1] = row["CD_GROUP"].ToString().Trim();
                        obj_args[2] = row["CD_BIZAREA"].ToString().Trim();
                        obj_args[3] = row["CD_COMPANY"].ToString().Trim();
                        obj_args[4] = row["DT_IO"].ToString().Trim();
                        obj_args[5] = ldb_am.ToString();
                        obj_args[6] = ldb_vat.ToString();
                        obj_args[7] = row["YN_RETURN"].ToString().Trim();
                        obj_args[8] = "Y";
                        obj_args[9] = row["CD_ITEM"].ToString().Trim();
                        obj_args[10] = row["FG_TRANS"].ToString().Trim();
                        obj_args[11] = row["CD_PLANT"].ToString().Trim();
                        obj_args[12] = row["CD_QTIOTP"].ToString().Trim();
                        obj_args[13] = ldb_qt.ToString();

                        si = new SpInfo();
                        si.SpNameSelect = "SP_PU_AP_UPDATE";
                        si.SpParamsSelect = obj_args;
                        siInfo.Add(si);
                    }
                }

                ResultData[] rs;
                if (siInfo.Count > 10) //ls_Query.Length > 10)
                {
                    rs = (ResultData[])this.Save(siInfo);
                    for (int i = 0; i < rs.Length; i++)
                    {
                        if (!rs[i].Result)
                        {
                            ApplicationException lex = new ApplicationException("CM_M100010");
                            lex.Source = "100000";
                            lex.HelpLink = "SavePuIV : EXEC SP_PU_AP_UPDATE";
                            throw lex;
                        }
                    }
                }

                return li_result;
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                ex.HelpLink = "SavePuiv";
                throw ex;
            }
        }

        public int UpdateMM_OHSLINV_QtioDelete(DataTable pdt_Line)
        {
            try
            {
                StringBuilder ls_Query = new StringBuilder("");

                int li_result = 0;
                if (pdt_Line == null || pdt_Line.Rows.Count <= 0)
                    return li_result;

                DataRow ldr_row;
                SpInfoCollection siInfo = new SpInfoCollection();
                SpInfo si = new SpInfo();
                for (int li_i = 0; li_i < pdt_Line.Rows.Count; li_i++)
                {
                    ldr_row = pdt_Line.Rows[li_i];
                    if (ldr_row.RowState.ToString() == "Deleted")
                        continue;

                    //-----------------------------------------
                    object[] obj_args = new object[10];
                    obj_args[0] = ldr_row["CD_SL"].ToString();
                    obj_args[1] = ldr_row["CD_PLANT"].ToString();
                    obj_args[2] = ldr_row["CD_COMPANY"].ToString();
                    obj_args[3] = ldr_row["CD_ITEM"].ToString();
                    obj_args[4] = ldr_row["QT_GOOD_INV"].ToString();
                    obj_args[5] = ldr_row["QT_REJECT_INV"].ToString();
                    obj_args[6] = ldr_row["QT_TRANS_INV"].ToString();
                    obj_args[7] = ldr_row["QT_INSP_INV"].ToString();
                    obj_args[8] = ldr_row["FG_PS"].ToString().Trim();
                    obj_args[9] = ldr_row["YN_RETURN"].ToString().Trim();

                    // 집계쪽 값을 빼주기 위해.. 
                    // (  그런데 이렇게 하는 것이 맡을까. 아님 수량을 수정할까 ?... )
                    if (ldr_row["FG_PS"].ToString().Trim() == "1")
                    {
                        obj_args[8] = "2";
                    }
                    else if (ldr_row["FG_PS"].ToString().Trim() == "2")
                    {
                        obj_args[8] = "1";
                    }

                    si = new SpInfo();
                    si.SpNameSelect = "SP_PU_MM_OHSLINV_UPDATE";
                    si.SpParamsSelect = obj_args;
                    siInfo.Add(si);

                    //-----------------------------------------
                }
                ResultData[] rs;
                if (siInfo.Count > 10) //ls_Query.Length > 10)
                {
                    rs = (ResultData[])this.Save(siInfo);
                    for (int i = 0; i < rs.Length; i++)
                    {
                        if (!rs[i].Result)
                        {
                            li_result = -1;
                            break;
                        }
                    }
                }
                return li_result;
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                ex.HelpLink = "DeleteAllCheak";
                throw ex;
            }
        }

        #region -> 주석처리

        //private string sqlUpdateMM_OHSLINV_QtioDelete(DataRow pdr_row)
        //{
        //    try
        //    {
        //        string ls_sql = "";
        //        if (pdr_row == null)
        //            return ls_sql;

        //        string[] lsa_args = new string[10];
        //        lsa_args[0] = pdr_row["CD_SL"].ToString();
        //        lsa_args[1] = pdr_row["CD_PLANT"].ToString();
        //        lsa_args[2] = pdr_row["CD_COMPANY"].ToString();
        //        lsa_args[3] = pdr_row["CD_ITEM"].ToString();
        //        lsa_args[4] = pdr_row["QT_GOOD_INV"].ToString();
        //        lsa_args[5] = pdr_row["QT_REJECT_INV"].ToString();
        //        lsa_args[6] = pdr_row["QT_TRANS_INV"].ToString();
        //        lsa_args[7] = pdr_row["QT_INSP_INV"].ToString();
        //        lsa_args[8] = pdr_row["FG_PS"].ToString().Trim();
        //        lsa_args[9] = pdr_row["YN_RETURN"].ToString().Trim();

        //        // 집계쪽 값을 빼주기 위해.. 
        //        // (  그런데 이렇게 하는 것이 맡을까. 아님 수량을 수정할까 ?... )
        //        if (pdr_row["FG_PS"].ToString().Trim() == "1")
        //        {
        //            lsa_args[8] = "2";
        //        }
        //        else if (pdr_row["FG_PS"].ToString().Trim() == "2")
        //        {
        //            lsa_args[8] = "1";
        //        }

        //        //--EXEC SP_MM_OHSLINV_APPEND '창고', '공장' , '회사','품목', 100,100,100,100,'1' ,'N'

        //        SpInfo si = new SpInfo();
        //        si.SpNameSelect = "SP_PU_MM_OHSLINV_UPDATE";
        //        si.SpParamsSelect = lsa_args;//new object[] { ps_cdcompany, ps_noio };
        //        ResultData result1 = (ResultData)this.FillDataTable(si);
        //        DataTable dtResult = (DataTable)result1.DataValue;

        //        ls_sql = sqlData("CC_MM_OHSLINV008", lsa_args);
        //        ls_sql += " \n";

        //        return ls_sql;

        //    }
        //    catch (ApplicationException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.HelpLink = "DeleteAllCheak";
        //        throw ex;
        //    }
        //}

        //private string sqlData(string ps_ccScript, string[] psa_args)
        //{
        //    try
        //    {
        //        string ls_sql = "";
        //        pur.PU_DBScript.SetUp();
        //        ls_sql = pur.PU_DBScript.GetScript(ps_ccScript, psa_args);

        //        if (ls_sql.Trim() == "" || ls_sql == null || ls_sql == string.Empty)
        //        {
        //            ApplicationException lex = new ApplicationException("CM_M100013");
        //            lex.Source = "100000";
        //            lex.HelpLink = ps_ccScript;
        //            throw lex;
        //        }
        //        return ls_sql;
        //    }
        //    catch
        //    {
        //        ApplicationException lex = new ApplicationException("CM_M100012");
        //        lex.Source = "100000";
        //        throw lex;
        //    }
        //}

        #endregion -> 주석처리

        public int UpdateMM_OHPLINV_QtioDelete(DataTable pdt_Line)
        {
            try
            {
                StringBuilder ls_Query = new StringBuilder("");

                int li_result = 0;
                if (pdt_Line == null || pdt_Line.Rows.Count <= 0)
                    return li_result;

                DataRow ldr_row;
                SpInfoCollection siInfo = new SpInfoCollection();
                SpInfo si = new SpInfo();
                for (int li_i = 0; li_i < pdt_Line.Rows.Count; li_i++)
                {
                    ldr_row = pdt_Line.Rows[li_i];
                    if (ldr_row.RowState.ToString() == "Deleted")
                        continue;

                    //-----------------------------------------

                    double ldb_am_inv = 0;

                    //입고면...
                    if (ldr_row["FG_PS"].ToString().Trim() == "1")
                    {
                        ldb_am_inv = System.Double.Parse(ldr_row["AM"].ToString().Trim());
                    }
                    else if (ldr_row["FG_PS"].ToString().Trim() == "2")
                    {
                        ldb_am_inv = System.Math.Floor(System.Double.Parse(ldr_row["QT_IO"].ToString().Trim()) *
                            System.Double.Parse(ldr_row["UM_STOCK"].ToString().Trim()));
                    }
                    else
                    {
                        ApplicationException lex = new ApplicationException("CM_M100010");
                        lex.Source = "100000";
                        lex.HelpLink = "FG_PS" + ldr_row["FG_PS"].ToString().Trim();
                        throw lex;
                    }

                    object[] obj_args = new object[6];
                    obj_args[0] = ldr_row["CD_PLANT"].ToString();
                    obj_args[1] = ldr_row["CD_COMPANY"].ToString();
                    obj_args[2] = ldr_row["CD_ITEM"].ToString();
                    obj_args[3] = ldb_am_inv.ToString();
                    obj_args[4] = ldr_row["FG_PS"].ToString().Trim();
                    obj_args[5] = ldr_row["YN_RETURN"].ToString().Trim();

                    if (ldr_row["FG_PS"].ToString().Trim() == "1")
                    {
                        obj_args[4] = "2";
                    }
                    else if (ldr_row["FG_PS"].ToString().Trim() == "2")
                    {
                        obj_args[4] = "1";
                    }

                    si = new SpInfo();
                    si.SpNameSelect = "SP_PU_MM_OHPLINV_UPDATE";
                    si.SpParamsSelect = obj_args;
                    siInfo.Add(si);

                    //-----------------------------------------
                }

                ResultData[] rs;
                if (siInfo.Count > 10) //ls_Query.Length > 10)
                {
                    rs = (ResultData[])this.Save(siInfo);
                    for (int i = 0; i < rs.Length; i++)
                    {
                        if (!rs[i].Result)
                        {
                            li_result = -1;
                            break;
                        }
                    }
                }
                return li_result;
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                ex.HelpLink = "DeleteAllCheak";
                throw ex;
            }
        }

        #region -> 주석처리(sqlUpdateMM_OHPLINV_QtioDelete)

        //private string sqlUpdateMM_OHPLINV_QtioDelete(DataRow pdr_row)
        //{
        //    try
        //    {
        //        string ls_sql = "";
        //        if (pdr_row == null)
        //            return ls_sql;

        //        double ldb_am_inv = 0;

        //        //입고면...
        //        if (pdr_row["FG_PS"].ToString().Trim() == "1")
        //        {
        //            ldb_am_inv = System.Double.Parse(pdr_row["AM"].ToString().Trim());
        //        }
        //        else if (pdr_row["FG_PS"].ToString().Trim() == "2")
        //        {
        //            ldb_am_inv = System.Math.Floor(System.Double.Parse(pdr_row["QT_IO"].ToString().Trim()) *
        //                System.Double.Parse(pdr_row["UM_STOCK"].ToString().Trim()));
        //        }
        //        else
        //        {
        //            ApplicationException lex = new ApplicationException("CM_M100010");
        //            lex.Source = "100000";
        //            lex.HelpLink = "FG_PS" + pdr_row["FG_PS"].ToString().Trim();
        //            throw lex;
        //        }

        //        string[] lsa_args = new string[6];
        //        lsa_args[0] = pdr_row["CD_PLANT"].ToString();
        //        lsa_args[1] = pdr_row["CD_COMPANY"].ToString();
        //        lsa_args[2] = pdr_row["CD_ITEM"].ToString();
        //        lsa_args[3] = ldb_am_inv.ToString();
        //        lsa_args[4] = pdr_row["FG_PS"].ToString().Trim();
        //        lsa_args[5] = pdr_row["YN_RETURN"].ToString().Trim();

        //        if (pdr_row["FG_PS"].ToString().Trim() == "1")
        //        {
        //            lsa_args[4] = "2";
        //        }
        //        else if (pdr_row["FG_PS"].ToString().Trim() == "2")
        //        {
        //            lsa_args[4] = "1";
        //        }

        //        //EXEC SP_PU_MM_OHPLINV_UPDATE '@','@','@', @ , '@' ,'@'
        //        ls_sql = sqlData("CC_MM_OHPLINV009", lsa_args);
        //        ls_sql += " \n";

        //        return ls_sql;

        //    }
        //    catch (ApplicationException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.HelpLink = "DeleteAllCheak";
        //        throw ex;
        //    }
        //}

        #endregion -> 주석처리(sqlUpdateMM_OHPLINV_QtioDelete)

        public int UpdateMM_OHSLINVM_QtioDelete(DataTable pdt_Line)
        {
            try
            {
                StringBuilder ls_Query = new StringBuilder("");

                int li_result = 0;
                if (pdt_Line == null || pdt_Line.Rows.Count <= 0)
                    return li_result;

                DataRow ldr_row;
                SpInfoCollection siInfo = new SpInfoCollection();
                SpInfo si = new SpInfo();
                for (int li_i = 0; li_i < pdt_Line.Rows.Count; li_i++)
                {
                    ldr_row = pdt_Line.Rows[li_i];
                    if (ldr_row.RowState.ToString() == "Deleted")
                        continue;

                    //-----------------------------------------
                    // 삭제이기 때문에 수량을 음수 처리 해서 집계 SP를 돌림
                    double ldb_qtgood = 0 - System.Double.Parse(ldr_row["QT_GOOD_INV"].ToString().Trim());
                    double ldb_qtreject = 0 - System.Double.Parse(ldr_row["QT_REJECT_INV"].ToString().Trim());
                    double ldb_qttrans = 0 - System.Double.Parse(ldr_row["QT_TRANS_INV"].ToString().Trim());
                    double ldb_qtinsp = 0 - System.Double.Parse(ldr_row["QT_INSP_INV"].ToString().Trim());

                    object[] obj_args = new object[13];

                    obj_args[0] = ldr_row["CD_SL"].ToString();
                    obj_args[1] = ldr_row["CD_PLANT"].ToString();
                    obj_args[2] = ldr_row["CD_COMPANY"].ToString();
                    obj_args[3] = ldr_row["CD_ITEM"].ToString();
                    obj_args[4] = ldr_row["DT_IO"].ToString().Trim().Substring(0, 4);	// 년도		
                    obj_args[5] = ldr_row["DT_IO"].ToString().Trim().Substring(0, 6);	// 년월
                    obj_args[6] = ldb_qtgood.ToString();
                    obj_args[7] = ldb_qtreject.ToString();
                    obj_args[8] = ldb_qttrans.ToString();
                    obj_args[9] = ldb_qtinsp.ToString();
                    obj_args[10] = ldr_row["FG_PS"].ToString().Trim();					// 입고:1, 출고: 2
                    obj_args[11] = ldr_row["YN_RETURN"].ToString().Trim();				// 반품여부
                    obj_args[12] = ldr_row["CD_QTIOTP"].ToString();						// 수불형태코드 ( 2003.12.01 )

                    si = new SpInfo();
                    si.SpNameSelect = "SP_PU_MM_OHSLINVM_UPDATE";
                    si.SpParamsSelect = obj_args;
                    siInfo.Add(si);
                    //-----------------------------------------
                }
                ResultData[] rs;
                if (siInfo.Count > 10) //ls_Query.Length > 10)
                {
                    rs = (ResultData[])this.Save(siInfo);
                    for (int i = 0; i < rs.Length; i++)
                    {
                        if (!rs[i].Result)
                        {
                            li_result = -1;
                            break;
                        }
                    }
                }
                return li_result;
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                ex.HelpLink = "UpdateMM_OHSLINVM_QtioDelete";
                throw ex;
            }
        }

        public int Check_CONTROL(DataTable pdt_Line)
        {
            try
            {
                DataTable ldt_temp = pdt_Line.Copy();
                for (int i = 0; i < ldt_temp.Rows.Count; i++)
                {
                    DataRow[] rows = ldt_temp.Select("CD_PLANT ='" + ldt_temp.Rows[i]["CD_PLANT"].ToString() + "'");
                    if (rows != null && rows.Length > 1)
                    {
                        for (int j = 1; j < rows.Length; j++)
                        {
                            rows[j].Delete();
                        }
                        ldt_temp.AcceptChanges();
                    }
                }

                string ps_plant = "";
                for (int i = 0; i < ldt_temp.Rows.Count; i++)
                {
                    ps_plant += ldt_temp.Rows[i]["CD_PLANT"].ToString();

                    if (i + 1 < ldt_temp.Rows.Count)
                    {
                        ps_plant += "'',''";
                    }
                }

                //추가 ( 20030519 )
                DataTable ldt_Contl = SelectGrcontrol(pdt_Line.Rows[0]["CD_COMPANY"].ToString(), ps_plant);

                if (ldt_Contl == null)
                {
                    return 1;
                }

                if (ldt_Contl.Rows.Count <= 0)
                {
                    return 1;
                }

                double ldb_ym_control = 0;		// 마감통제월	
                double ldb_adt_ios = 0;			// 수불월	

                for (int i = 0; i < pdt_Line.Rows.Count; i++)
                {
                    DataRow[] rowplant = ldt_Contl.Select("CD_PLANT ='" + pdt_Line.Rows[i]["CD_PLANT"].ToString() + "'");

                    if (rowplant != null && rowplant.Length > 0)
                    {
                        try
                        {
                            ldb_ym_control = System.Double.Parse(rowplant[0]["YM_CONTROL"].ToString());
                            ldb_adt_ios = System.Double.Parse(pdt_Line.Rows[i]["DT_IO"].ToString().Substring(0, 6));
                        }
                        catch
                        {
                        }

                        if (ldb_ym_control >= ldb_adt_ios)
                        {
                            ApplicationException lex = new ApplicationException("PU_M000052");
                            lex.Source = "100000";
                            lex.HelpLink = "Check_CONTROL";
                            throw lex;
                        }
                    }
                }

                return 1;
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                ex.HelpLink = "Check_CONTROL";
                throw ex;
            }
        }

        public DataTable SelectGrcontrol(string acd_company, string acd_plant)
        {
            try
            {
                //SELECT YM_CONTROL, CD_PLANT
                //FROM MM_CONTROL
                //WHERE CD_COMPANY = ''' + @CD_COMPANY + '''
                //    AND CD_PLANT IN( ''' + @CD_PLANT + ''' ) '
                SpInfo si = new SpInfo();
                si.SpNameSelect = "UP_MA_CONTROL_CHECK_SELECT";
                si.SpParamsSelect = new object[] { acd_company, acd_plant };
                ResultData result = (ResultData)this.FillDataTable(si);
                DataTable dtResult = (DataTable)result.DataValue;

                return dtResult;
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                ex.HelpLink = "SelectGrcontrol";
                throw ex;
            }
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
				
				if(!SearchCondition())
					return;

				if(_flexH.DataTable != null)
				{
					_flexH.DataTable.Rows.Clear();
					_flexL.DataTable.Rows.Clear();
					Application.DoEvents();
					Thread.Sleep(50);
				}

				DataSet ds_qtioH = null;
		
				//필수항목 체크
				if(Field_Check() == false)
					return;	

				ResultData result = (ResultData)this.FillDataSet("SP_SA_GIM_SELECT_H", new object[]{ this.MainFrameInterface.LoginInfo.CompanyCode, 
																									   maskedEditBox1.Text.ToString(),
																									   maskedEditBox2.Text.ToString(), 
																									   cb_cd_plant.SelectedValue.ToString(),
																									   bpNm_Sl.CodeValue.ToString(),
																									   bpNm_Partner.CodeValue.ToString(),
																									   cb_fg_po_tr.SelectedValue.ToString(),
																									   cb_fg_gr.SelectedValue.ToString(),
																									   bpRcv_Pt.CodeValue.ToString(), 
																									   bpNo_Emp.CodeValue.ToString(),
																									   ""});
				ds_qtioH = (DataSet)result.DataValue;

				
				ResultData result_1 = (ResultData)this.FillDataSet("SP_SA_GIM_SELECT_L", new object[]{ this.MainFrameInterface.LoginInfo.CompanyCode, 
																									   maskedEditBox1.Text.ToString(),
																									   maskedEditBox2.Text.ToString(), 
																									   cb_cd_plant.SelectedValue.ToString(),
																									   bpNm_Sl.CodeValue.ToString(),
																									   bpNm_Partner.CodeValue.ToString(),
																									   cb_fg_po_tr.SelectedValue.ToString(),
																									   bpRcv_Pt.CodeValue.ToString(), 
																									   bpNo_Emp.CodeValue.ToString()});
				 DataSet ds_qtioL = (DataSet)result_1.DataValue;

				gds_dzdwGrid2_DEL = ds_qtioL.Clone(); // 라인 데이터셋
				gds_dzdwGrid1_DEL = ds_qtioH.Clone(); // 헤더 데이터셋

				// Detail 바인딩
				_flexL.Redraw = false;
				_flexL.BindingStart();
				_flexL.DataSource = ds_qtioL.Tables[0].DefaultView;
				_flexL.BindingEnd();
				_flexL.EmptyRowFilter();			// 처음에 아무것도 안 보이게
				_flexL.Redraw = true;

				// Master 바인딩
				_flexH.Redraw = false;
				_flexH.BindingStart();
				_flexH.DataSource = ds_qtioH.Tables[0].DefaultView;	// 요기에서 곧바로 AfterRowColChange 이벤트 발생
				_flexH.BindingEnd();

				_flexH.Styles.Fixed.TextAlign  = TextAlignEnum.CenterCenter;
				_flexL.Styles.Fixed.TextAlign = TextAlignEnum.CenterCenter;

				if(_flexH.HasNormalRow)				// 처음 조회시 강제로 AfterRowColChange 메소드 호출
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
				m_page_state = "Deleted";

				this.SetProgressBarValue(100, 100);	
				Application.DoEvents();
			}
			catch(Exception ex)
			{	
				MsgEnd(ex);
			}
			finally
			{				
				ToolBarSearchButtonEnabled = true;
				ToolBarPrintButtonEnabled = true;				
			}
		}

		#endregion

		#region -> 삭제버튼클릭

		public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{
			if(!DoContinue())
				return;

			try
			{
				DialogResult result = this.ShowMessage("MA_M000016","QY3");

				if(result == DialogResult.Yes)
				{
					DataRow[] rows = _flexH.DataTable.Select("S= '1'");

					if( rows == null || rows.Length <=0)
					{
						this.ShowMessage("IK1_007");
						return;
					}
					else
					{
						_flexH.Redraw = false;		
				
						if( rows != null && rows.Length > 0)
						{
							for(int r = _flexH.Rows.Count-1;r >= _flexH.Rows.Fixed; r--)
							{
								if(_flexH[r,"S"].ToString() == "1")
								{							
									_flexH.Rows.Remove(r);
								}							
							}	
						}
				
						DataTable g_dtChangeH = _flexH.DataTable.GetChanges();

						Duzon.Common.Util.SpInfo si = new Duzon.Common.Util.SpInfo();
						si.DataValue = g_dtChangeH;											// 저장할 데이터 테이블
		
						si.SpNameDelete = "SP_SA_GIM_DELETE";								// Delete 프로시저명
						si.SpParamsDelete = new string[] {"NO_IO","CD_COMPANY"};
								
						Duzon.Common.Util.ResultData result_1 = (Duzon.Common.Util.ResultData)Save(si);
						
						if(result_1.Result)
						{							
							_flexH.DataTable.AcceptChanges();	
							ShowMessage("CM_M000005");					
						}

						if(_flexH.HasNormalRow)			
						{
							int row = _flexH.Row;
							_flexH.Row = -1;
							_flexH.Row = row;
						}
						else
						{
							_flexL.Redraw=false;
							_flexL.EmptyRowFilter();	
							_flexL.Redraw=true;
						}
						_flexH.Redraw = true;	
					}
					this.ToolBarDeleteButtonEnabled = true;		
				}				
			}			
			catch(Exception ex)
			{				
				MsgEnd(ex);									
			}
			finally
			{
				_flexH.Redraw = true;	
				_flexH.Refresh();					
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
			finally
			{
				this.ShowStatusBarMessage(0);
				this.SetProgressBarValue(100 ,0);
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
				string filter = "NO_IO = '" + _flexH[_flexH.Row,"NO_IO"].ToString() + "'";

				_flexL.DataView.RowFilter  = filter;
			}
		}
		
		#endregion

		#region	-> _flexL_AfterDataRefresh
		
		private void _flexL_AfterDataRefresh(object sender, System.ComponentModel.ListChangedEventArgs e)
		{
			if(IsChanged(null))
				SetToolBarButtonState(true,false, true,true,false);
			else
				SetToolBarButtonState(true,false,true,false,false);

			if(!_flexL.HasNormalRow)
				ToolBarDeleteButtonEnabled = false;
		}
	
		#endregion	

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
			catch
			{
			}			
		}

		#endregion

		#endregion

		#region ♣ 기타 이벤트 / 메소드

		#region -> 계산
		/// <summary>
		/// QT_MM(수배수량), QT_R_MM(의뢰수량) 값을 계산합니다.
		/// </summary>
		private void CalReq()
		{		
			for(int li_i = 1; li_i < _flexL.Rows.Count; li_i++)
			{
				Double qt_req_mm	= System.Convert.ToDouble(_flexL[li_i, "QT_REQ_MM"].ToString());
				Double qt_gr_mm		= System.Convert.ToDouble(_flexL[li_i, "QT_GR_MM"].ToString());
				Double qt_req		= System.Convert.ToDouble(_flexL[li_i, "QT_REQ"].ToString());
				Double qt_gr		= System.Convert.ToDouble(_flexL[li_i, "QT_GR"].ToString());

				Double qt_mm	= Math.Abs(qt_req_mm-qt_gr_mm);
				Double qt_r_mm	= Math.Abs(qt_req-qt_gr);				

				//수배수량
				_flexL[li_i, "QT_MM"]	= System.Convert.ToString(qt_mm);
				//의뢰수량
				_flexL[li_i, "QT_R_MM"] = System.Convert.ToString(qt_r_mm);
			}
		}

		#endregion
		
		#region -> 날짜 텍스트박스 이벤트
		
		private void maskedEditBox1_Validated(object sender, System.EventArgs e)
		{
			if(!this.maskedEditBox1.IsValidated)
			{
				ShowMessage("WK1_003", lb_dt_rcv.Text);
				this.maskedEditBox1.Focus();
			}

		}

		private void maskedEditBox2_Validated(object sender, System.EventArgs e)
		{
			if(!this.maskedEditBox2.IsValidated)
			{
				ShowMessage("WK1_003", lb_dt_rcv.Text);
				this.maskedEditBox2.Focus();
			}
		}
		
		#endregion				

		#region -> 데이터 체크항목
		/// <summary>
		/// 필수항목체크
		/// </summary>
		private bool Field_Check()
		{
			//입고일자 시작일
			if(maskedEditBox1.Text.Trim() == "")
			{
				//필수입력사항이 누락되었습니다.
				this.ShowMessage("WK1_004", lb_dt_rcv.Text);
				this.maskedEditBox1.Focus();
				return false;
			}
			//입고일자 종료일
			if(maskedEditBox2.Text.Trim() == "")
			{
				//필수입력사항이 누락되었습니다.
				this.ShowMessage("WK1_004", lb_dt_rcv.Text);
				maskedEditBox2.Focus();
				return false;
			}
			//입고공장
			if(cb_cd_plant.SelectedValue.ToString() == "")
			{
				//필수입력사항이 누락되었습니다.
				this.ShowMessage("WK1_004", lb_gr_plant.Text);
				cb_cd_plant.Focus();
				return false;
			}		
			return true;			
		}
		
		#endregion

		#region >> 콤보박스 키 이벤트

		/// <summary>
		/// 콤보 박스 공통 키 이벤트 처리
		/// </summary>
		/// <param name="e"></param>
		/// <param name="backControl"></param>
		/// <param name="nextControl"></param>
		private void CommonComboBox_KeyEvent(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyData == Keys.Enter)
				System.Windows.Forms.SendKeys.SendWait("{TAB}");
		}

		#endregion

		#region >> TextBox Enter 이벤트

		/// <summary>
		/// TextBox Enter 이벤트
		/// </summary>
		private void TextBoxEnterEvent(object sender, System.Windows.Forms.KeyEventArgs e)
		{							
			if(e.KeyData.ToString() == "Enter" || e.KeyData.ToString() == "Down")
			{			
				SendKeys.SendWait("{TAB}");
			}	
			else if(e.KeyData.ToString() == "Up")
				SendKeys.SendWait("+{TAB}");			
		}
		#endregion		

		#region -> BPControl
		private void Control_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
		{
			try
			{
				if(e.DialogResult == DialogResult.Cancel)
					return;

				switch(e.ControlName)
				{					
					case "bpNm_Partner":	// 거래처						
						bpNm_Partner.CodeValue = e.HelpReturn.Rows[0]["CD_PARTNER"].ToString();
						bpNm_Partner.CodeName = e.HelpReturn.Rows[0]["LN_PARTNER"].ToString();					
						break;
				
					case "bpNm_Sl":			// 창고									
						bpNm_Sl.CodeName = e.HelpReturn.Rows[0]["NM_SL"].ToString();
						bpNm_Sl.CodeValue = e.HelpReturn.Rows[0]["CD_SL"].ToString();
						break;

					case "bpRcv_Pt":		// 출고형태									
						bpRcv_Pt.CodeName = e.HelpReturn.Rows[0]["NM_QTIOTP"].ToString();
						bpRcv_Pt.CodeValue = e.HelpReturn.Rows[0]["CD_QTIOTP"].ToString();

						gstb_qtio = e.HelpReturn.Rows[0]["NM_QTIOTP"].ToString();
						//					bpRcv_Pt.Tag = ds.Rows[0]["CD_QTIOTP"].ToString();
						gs_tpqtio = e.HelpReturn.Rows[0]["TP_QTIO"].ToString();

						if( gs_tpqtio =="3")
						{
							gs_fgtp = e.HelpReturn.Rows[0]["FG_TPPURCHASE"].ToString();
						}
						else
						{
							gs_fgtp = e.HelpReturn.Rows[0]["FG_TPSALE"].ToString();
						}
						break;

					case "bpNo_Emp":		// 담당자									
						bpNo_Emp.CodeName = e.HelpReturn.Rows[0]["NM_KOR"].ToString();
						bpNo_Emp.CodeValue = e.HelpReturn.Rows[0]["NO_EMP"].ToString();
						break;				
				}
			}
			catch(Exception ex)
			{				
				MsgEnd(ex);									
			}
		}
		private void Control_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
		{
			switch(e.HelpID)
			{
				case Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB:
					e.HelpParam.P09_CD_PLANT = cb_cd_plant.SelectedValue.ToString();
					break;
                case Duzon.Common.Forms.Help.HelpID.P_PU_EJTP_SUB:
                    e.HelpParam.P61_CODE1 = "010|041|042|";
                    break;
			}
		}

		#endregion
		
		#endregion
	}
}

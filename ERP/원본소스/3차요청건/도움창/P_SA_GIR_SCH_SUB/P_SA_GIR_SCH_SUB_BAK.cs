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
	/// P_SA_GIR_SCH_SUB에 대한 요약 설명입니다.
	/// </summary>
	public class P_SA_GIR_SCH_SUB_BAK : Duzon.Common.Forms.CommonDialog, IHelpWindow
	{
		#region ♣ 멤버필드

		#region -> 멤버필드(일반)

		// IContainer
		private System.ComponentModel.IContainer components;

		// Panel
		private Duzon.Common.Controls.PanelExt m_pnlGrid;
		private Duzon.Common.Controls.PanelExt panel1;
		private Duzon.Common.Controls.PanelExt panel2;
		private Duzon.Common.Controls.PanelExt panel3;
		private Duzon.Common.Controls.PanelExt panel4;
		private Duzon.Common.Controls.PanelExt panel5;

		// Label
		private Duzon.Common.Controls.LabelExt m_lblPlantGir;
		private Duzon.Common.Controls.LabelExt m_lblEmpGir;
		private Duzon.Common.Controls.LabelExt m_lblDtGir;
		private Duzon.Common.Controls.LabelExt m_lblSlGir;
		private Duzon.Common.Controls.LabelExt m_lblTitle;
		private Duzon.Common.Controls.LabelExt label6;
		
		// RoundedButton
		private Duzon.Common.Controls.RoundedButton m_btnCancel;
		private Duzon.Common.Controls.RoundedButton m_btnConfirm;
		private Duzon.Common.Controls.RoundedButton m_btnQuery;

		// DzComboBox
		private Duzon.Common.Controls.DropDownComboBox m_cboPlantGir;
		
		// DataRow[]
		public object[] m_return = new object[1];

		private string m_stplant = null;
		private Duzon.Common.Controls.DatePicker m_mskStart;
		private Duzon.Common.Controls.DatePicker m_mskEnd;
		private string m_FgGirq = null;

		#endregion

		#region -> 멤버필드(주요)

		// IMainFrame
		private Duzon.Common.Forms.IMainFrame m_imain;
        private Duzon.Common.BpControls.BpCodeTextBox bpNm_Partner;
        private Duzon.Common.BpControls.BpCodeTextBox bpTpGi;

		// GridDataBoundGrid
		private Dass.FlexGrid.FlexGrid _flex;

		#endregion 

		#endregion

		#region ♣ 생성자/소멸자

		#region -> 생성자

		public P_SA_GIR_SCH_SUB_BAK(Duzon.Common.Forms.IMainFrame pi_main, string ps_plant, string ps_nmsl, string ps_cdsl,string ps_fggirq)
		{
			InitializeComponent();

			m_imain						= pi_main;
			m_FgGirq					= ps_fggirq;
		
			m_stplant					= ps_plant;
			//bpNm_Sl.CodeName			= ps_nmsl;
			//bpNm_Sl.CodeValue			= ps_cdsl;

            bpTpGi.MainFrame = m_imain;
			
			Load += new System.EventHandler(Page_Load);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_SA_GIR_SCH_SUB));
            this.label6 = new Duzon.Common.Controls.LabelExt();
            this.m_lblSlGir = new Duzon.Common.Controls.LabelExt();
            this.m_lblPlantGir = new Duzon.Common.Controls.LabelExt();
            this.m_lblEmpGir = new Duzon.Common.Controls.LabelExt();
            this.m_lblDtGir = new Duzon.Common.Controls.LabelExt();
            this.panel4 = new Duzon.Common.Controls.PanelExt();
            this.bpNm_Partner = new Duzon.Common.BpControls.BpCodeTextBox();
            this.m_mskEnd = new Duzon.Common.Controls.DatePicker();
            this.m_mskStart = new Duzon.Common.Controls.DatePicker();
            this.m_cboPlantGir = new Duzon.Common.Controls.DropDownComboBox();
            this.panel3 = new Duzon.Common.Controls.PanelExt();
            this.panel2 = new Duzon.Common.Controls.PanelExt();
            this.panel1 = new Duzon.Common.Controls.PanelExt();
            this.m_btnCancel = new Duzon.Common.Controls.RoundedButton(this.components);
            this.m_btnConfirm = new Duzon.Common.Controls.RoundedButton(this.components);
            this.m_btnQuery = new Duzon.Common.Controls.RoundedButton(this.components);
            this.panel5 = new Duzon.Common.Controls.PanelExt();
            this.m_lblTitle = new Duzon.Common.Controls.LabelExt();
            this.m_pnlGrid = new Duzon.Common.Controls.PanelExt();
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this.bpTpGi = new Duzon.Common.BpControls.BpCodeTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_mskEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_mskStart)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.m_pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            this.closeButton.Font = new System.Drawing.Font("GulimChe", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.closeButton.ForeColor = System.Drawing.Color.Black;
            this.closeButton.Location = new System.Drawing.Point(778, 3);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(200, 8);
            this.label6.Name = "label6";
            this.label6.Resizeble = true;
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "∼";
            // 
            // m_lblSlGir
            // 
            this.m_lblSlGir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.m_lblSlGir.Location = new System.Drawing.Point(4, 32);
            this.m_lblSlGir.Name = "m_lblSlGir";
            this.m_lblSlGir.Resizeble = true;
            this.m_lblSlGir.Size = new System.Drawing.Size(95, 18);
            this.m_lblSlGir.TabIndex = 0;
            this.m_lblSlGir.Tag = "TP_GI";
            this.m_lblSlGir.Text = "출하형태";
            this.m_lblSlGir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblPlantGir
            // 
            this.m_lblPlantGir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.m_lblPlantGir.Location = new System.Drawing.Point(5, 32);
            this.m_lblPlantGir.Name = "m_lblPlantGir";
            this.m_lblPlantGir.Resizeble = true;
            this.m_lblPlantGir.Size = new System.Drawing.Size(95, 18);
            this.m_lblPlantGir.TabIndex = 0;
            this.m_lblPlantGir.Tag = "PLANT_GIR";
            this.m_lblPlantGir.Text = "출하공장";
            this.m_lblPlantGir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblEmpGir
            // 
            this.m_lblEmpGir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.m_lblEmpGir.Location = new System.Drawing.Point(4, 6);
            this.m_lblEmpGir.Name = "m_lblEmpGir";
            this.m_lblEmpGir.Resizeble = true;
            this.m_lblEmpGir.Size = new System.Drawing.Size(95, 18);
            this.m_lblEmpGir.TabIndex = 0;
            this.m_lblEmpGir.Tag = "CD_PARTNER";
            this.m_lblEmpGir.Text = "거래처";
            this.m_lblEmpGir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblDtGir
            // 
            this.m_lblDtGir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.m_lblDtGir.Location = new System.Drawing.Point(5, 6);
            this.m_lblDtGir.Name = "m_lblDtGir";
            this.m_lblDtGir.Resizeble = true;
            this.m_lblDtGir.Size = new System.Drawing.Size(95, 18);
            this.m_lblDtGir.TabIndex = 0;
            this.m_lblDtGir.Tag = "DT_GIR";
            this.m_lblDtGir.Text = "의뢰일자";
            this.m_lblDtGir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.bpTpGi);
            this.panel4.Controls.Add(this.bpNm_Partner);
            this.panel4.Controls.Add(this.m_mskEnd);
            this.panel4.Controls.Add(this.m_mskStart);
            this.panel4.Controls.Add(this.m_cboPlantGir);
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Controls.Add(this.m_lblDtGir);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.m_lblPlantGir);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Location = new System.Drawing.Point(10, 55);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(785, 56);
            this.panel4.TabIndex = 0;
            // 
            // bpNm_Partner
            // 
            this.bpNm_Partner.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Sub;
            this.bpNm_Partner.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpNm_Partner.ButtonImage")));
            this.bpNm_Partner.ChildMode = "";
            this.bpNm_Partner.CodeName = "";
            this.bpNm_Partner.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpNm_Partner.CodeValue = "";
            this.bpNm_Partner.ComboCheck = true;
            this.bpNm_Partner.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.bpNm_Partner.ItemBackColor = System.Drawing.Color.White;
            this.bpNm_Partner.Location = new System.Drawing.Point(500, 3);
            this.bpNm_Partner.Name = "bpNm_Partner";
            this.bpNm_Partner.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpNm_Partner.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.bpNm_Partner.SearchCode = true;
            this.bpNm_Partner.SelectCount = 0;
            this.bpNm_Partner.SetDefaultValue = false;
            this.bpNm_Partner.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpNm_Partner.Size = new System.Drawing.Size(250, 21);
            this.bpNm_Partner.TabIndex = 3;
            this.bpNm_Partner.Text = "bpCodeTextBox1";
            this.bpNm_Partner.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryBefore);
            this.bpNm_Partner.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Contro_QueryAfter);
            // 
            // m_mskEnd
            // 
            this.m_mskEnd.CalendarBackColor = System.Drawing.Color.White;
            this.m_mskEnd.DayColor = System.Drawing.SystemColors.ControlText;
            this.m_mskEnd.FriDayColor = System.Drawing.Color.Blue;
            this.m_mskEnd.Location = new System.Drawing.Point(220, 4);
            this.m_mskEnd.Mask = "####/##/##";
            this.m_mskEnd.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.m_mskEnd.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.m_mskEnd.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.m_mskEnd.Modified = false;
            this.m_mskEnd.Name = "m_mskEnd";
            this.m_mskEnd.PaddingCharacter = '_';
            this.m_mskEnd.PassivePromptCharacter = '_';
            this.m_mskEnd.PromptCharacter = '_';
            this.m_mskEnd.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.m_mskEnd.ShowToDay = true;
            this.m_mskEnd.ShowTodayCircle = true;
            this.m_mskEnd.ShowUpDown = false;
            this.m_mskEnd.Size = new System.Drawing.Size(90, 21);
            this.m_mskEnd.SunDayColor = System.Drawing.Color.Red;
            this.m_mskEnd.TabIndex = 2;
            this.m_mskEnd.TitleBackColor = System.Drawing.SystemColors.Control;
            this.m_mskEnd.TitleForeColor = System.Drawing.Color.Black;
            this.m_mskEnd.ToDayColor = System.Drawing.Color.Red;
            this.m_mskEnd.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.m_mskEnd.UseKeyF3 = false;
            this.m_mskEnd.Value = new System.DateTime(((long)(0)));
            this.m_mskEnd.Validated += new System.EventHandler(this.m_mskEnd_Validated);
            // 
            // m_mskStart
            // 
            this.m_mskStart.CalendarBackColor = System.Drawing.Color.White;
            this.m_mskStart.DayColor = System.Drawing.SystemColors.ControlText;
            this.m_mskStart.FriDayColor = System.Drawing.Color.Blue;
            this.m_mskStart.Location = new System.Drawing.Point(108, 4);
            this.m_mskStart.Mask = "####/##/##";
            this.m_mskStart.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.m_mskStart.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.m_mskStart.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.m_mskStart.Modified = false;
            this.m_mskStart.Name = "m_mskStart";
            this.m_mskStart.PaddingCharacter = '_';
            this.m_mskStart.PassivePromptCharacter = '_';
            this.m_mskStart.PromptCharacter = '_';
            this.m_mskStart.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.m_mskStart.ShowToDay = true;
            this.m_mskStart.ShowTodayCircle = true;
            this.m_mskStart.ShowUpDown = false;
            this.m_mskStart.Size = new System.Drawing.Size(90, 21);
            this.m_mskStart.SunDayColor = System.Drawing.Color.Red;
            this.m_mskStart.TabIndex = 1;
            this.m_mskStart.TitleBackColor = System.Drawing.SystemColors.Control;
            this.m_mskStart.TitleForeColor = System.Drawing.Color.Black;
            this.m_mskStart.ToDayColor = System.Drawing.Color.Red;
            this.m_mskStart.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.m_mskStart.UseKeyF3 = false;
            this.m_mskStart.Value = new System.DateTime(((long)(0)));
            this.m_mskStart.Validated += new System.EventHandler(this.m_mskStart_Validated);
            // 
            // m_cboPlantGir
            // 
            this.m_cboPlantGir.AutoDropDown = true;
            this.m_cboPlantGir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboPlantGir.Location = new System.Drawing.Point(108, 31);
            this.m_cboPlantGir.Name = "m_cboPlantGir";
            this.m_cboPlantGir.ShowCheckBox = false;
            this.m_cboPlantGir.Size = new System.Drawing.Size(164, 20);
            this.m_cboPlantGir.TabIndex = 4;
            this.m_cboPlantGir.UseKeyEnter = false;
            this.m_cboPlantGir.UseKeyF3 = true;
            this.m_cboPlantGir.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnComboKeyDown);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
            this.panel3.Location = new System.Drawing.Point(5, 27);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(775, 1);
            this.panel3.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel2.Controls.Add(this.m_lblSlGir);
            this.panel2.Controls.Add(this.m_lblEmpGir);
            this.panel2.Location = new System.Drawing.Point(393, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(103, 52);
            this.panel2.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(103, 52);
            this.panel1.TabIndex = 0;
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.BackColor = System.Drawing.Color.White;
            this.m_btnCancel.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.m_btnCancel.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.m_btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnCancel.Location = new System.Drawing.Point(729, 123);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(60, 22);
            this.m_btnCancel.TabIndex = 0;
            this.m_btnCancel.TabStop = false;
            this.m_btnCancel.Tag = "Q_QCancel";
            this.m_btnCancel.Text = "취소";
            this.m_btnCancel.UseVisualStyleBackColor = false;
            this.m_btnCancel.Click += new System.EventHandler(this.m_btnCancel_Click);
            // 
            // m_btnConfirm
            // 
            this.m_btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnConfirm.BackColor = System.Drawing.Color.White;
            this.m_btnConfirm.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.m_btnConfirm.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.m_btnConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnConfirm.Location = new System.Drawing.Point(668, 123);
            this.m_btnConfirm.Name = "m_btnConfirm";
            this.m_btnConfirm.Size = new System.Drawing.Size(60, 22);
            this.m_btnConfirm.TabIndex = 0;
            this.m_btnConfirm.TabStop = false;
            this.m_btnConfirm.Tag = "Q_QConfirm";
            this.m_btnConfirm.Text = "확인";
            this.m_btnConfirm.UseVisualStyleBackColor = false;
            this.m_btnConfirm.Click += new System.EventHandler(this.OnApply);
            // 
            // m_btnQuery
            // 
            this.m_btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnQuery.BackColor = System.Drawing.Color.White;
            this.m_btnQuery.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.m_btnQuery.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.m_btnQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnQuery.Location = new System.Drawing.Point(605, 123);
            this.m_btnQuery.Name = "m_btnQuery";
            this.m_btnQuery.Size = new System.Drawing.Size(60, 22);
            this.m_btnQuery.TabIndex = 0;
            this.m_btnQuery.TabStop = false;
            this.m_btnQuery.Tag = "Q_QQuery";
            this.m_btnQuery.Text = "조회";
            this.m_btnQuery.UseVisualStyleBackColor = false;
            this.m_btnQuery.Click += new System.EventHandler(this.OnSearchButtonClicked);
            // 
            // panel5
            // 
            this.panel5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel5.BackgroundImage")));
            this.panel5.Controls.Add(this.m_lblTitle);
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(805, 47);
            this.panel5.TabIndex = 0;
            // 
            // m_lblTitle
            // 
            this.m_lblTitle.AutoSize = true;
            this.m_lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.m_lblTitle.Font = new System.Drawing.Font("GulimChe", 10F, System.Drawing.FontStyle.Bold);
            this.m_lblTitle.ForeColor = System.Drawing.Color.White;
            this.m_lblTitle.Location = new System.Drawing.Point(15, 16);
            this.m_lblTitle.Name = "m_lblTitle";
            this.m_lblTitle.Resizeble = false;
            this.m_lblTitle.Size = new System.Drawing.Size(97, 14);
            this.m_lblTitle.TabIndex = 0;
            this.m_lblTitle.Tag = "P_SA_GIR_SCH_SUB";
            this.m_lblTitle.Text = "납품의뢰조회";
            // 
            // m_pnlGrid
            // 
            this.m_pnlGrid.Controls.Add(this._flex);
            this.m_pnlGrid.Location = new System.Drawing.Point(10, 152);
            this.m_pnlGrid.Name = "m_pnlGrid";
            this.m_pnlGrid.Size = new System.Drawing.Size(785, 464);
            this.m_pnlGrid.TabIndex = 7;
            // 
            // _flex
            // 
            this._flex.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex.AutoResize = false;
            this._flex.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex.EnabledHeaderCheck = true;
            this._flex.IsDataChanged = false;
            this._flex.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex.Location = new System.Drawing.Point(0, 0);
            this._flex.Name = "_flex";
            this._flex.RowFilter = "";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 18;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(785, 464);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 0;
            // 
            // bpTpGi
            // 
            this.bpTpGi.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpTpGi.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpTpGi.ButtonImage")));
            this.bpTpGi.ChildMode = "";
            this.bpTpGi.CodeName = "";
            this.bpTpGi.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpTpGi.CodeValue = "";
            this.bpTpGi.ComboCheck = true;
            this.bpTpGi.HelpID = Duzon.Common.Forms.Help.HelpID.P_PU_EJTP_SUB;
            this.bpTpGi.ItemBackColor = System.Drawing.Color.White;
            this.bpTpGi.Location = new System.Drawing.Point(500, 31);
            this.bpTpGi.Name = "bpTpGi";
            this.bpTpGi.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpTpGi.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.bpTpGi.SearchCode = true;
            this.bpTpGi.SelectCount = 0;
            this.bpTpGi.SetDefaultValue = false;
            this.bpTpGi.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpTpGi.Size = new System.Drawing.Size(250, 21);
            this.bpTpGi.TabIndex = 9;
            this.bpTpGi.Text = "bpCodeTextBox3";
            this.bpTpGi.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryBefore);
            this.bpTpGi.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Contro_QueryAfter);
            // 
            // P_SA_GIR_SCH_SUB
            // 
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(804, 624);
            this.Controls.Add(this.m_pnlGrid);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.m_btnQuery);
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.m_btnConfirm);
            this.Font = new System.Drawing.Font("GulimChe", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "P_SA_GIR_SCH_SUB";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_mskEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_mskStart)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.m_pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		#region  -> 소멸자

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

		#region -> Page_Load

		/// <summary>
		/// 페이지 로드 이벤트 핸들러(화면 초기화 작업)
		/// </summary>
		private void Page_Load(object sender, EventArgs e)
		{
			try
			{
				this.Show();
				
				// 그리드 컨트롤을 초기화 한다.
				InitGrid();
				Application.DoEvents();

				// 해당하는 언어에 맞게 Label 초기화
				InitControl();
				Application.DoEvents();

				m_mskStart.Text = m_imain.GetStringFirstDayInMonth;
				m_mskEnd.Text = m_imain.GetStringToday;

				m_btnConfirm.Enabled=false;
			}
			catch(Exception ex)
			{
				m_imain.MsgEnd(ex);
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
					case "NO_GIR":		// 1.의뢰번호
						temp = temp + " + " + this.m_imain.GetDataDictionaryItem("SA","NO_GIR");
						break;
					case "DT_GIR":		// 2.의뢰일자
						temp = temp + " + " + this.m_imain.GetDataDictionaryItem("SA","DT_GIR");
						break;
					case "LN_PARTNER":	// 3.거래처
						temp = temp + " + " + this.m_imain.GetDataDictionaryItem("SA","CD_PARTNER");
						break;
					case "NM_KOR":		// 4.출하의뢰자
						temp = temp + " + " + this.m_imain.GetDataDictionaryItem("SA","NO_EMP");
						break;
					case "NM_PLANT":	// 5.의뢰공장명
						temp = temp + " + " + this.m_imain.GetDataDictionaryItem("SA","PLANT_GIR");
						break;
					case "NM_SL":		// 6.창고명
						temp = temp + " + " + this.m_imain.GetDataDictionaryItem("SA","NM_SL");
						break;
					case "NM_BUSI":		// 7.거래구분
						temp = temp + " + " + this.m_imain.GetDataDictionaryItem("SA","TP_BUSI");
						break;
					case "STA_GIRNM":	// 8.처리상태
						temp = temp + " + " + this.m_imain.GetDataDictionaryItem("SA","STA_GIR");
						break;
                    case "NM_TP_GI":    // 9.출하형태
                        temp = temp + " + " + this.m_imain.GetDataDictionaryItem("SA", "TP_GI");
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

		#region -> InitGrid

		private void InitGrid()
		{
			Application.DoEvents();
			
			_flex.Redraw = false;

			_flex.Rows.Count = 1;
			_flex.Rows.Fixed = 1;
			_flex.Cols.Count = 9;
			_flex.Cols.Fixed = 1;
			_flex.Rows.DefaultSize = 20;
			
			_flex.Cols[0].Width = 50;

			// 1.의뢰번호
			_flex.Cols[1].Name = "NO_GIR";
			_flex.Cols[1].DataType = typeof(string);
			_flex.Cols[1].Width = 110;
			_flex.Cols[1].AllowEditing = false;
			
			// 2.의뢰일자
			_flex.Cols[2].Name = "DT_GIR";
			_flex.Cols[2].DataType = typeof(string);
			_flex.Cols[2].Width = 70;
			_flex.Cols[2].AllowEditing = false;
			_flex.Cols[2].EditMask = this.m_imain.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT);
			_flex.Cols[2].Format = _flex.Cols[2].EditMask;
			_flex.SetStringFormatCol("DT_GIR");
			_flex.SetNoMaskSaveCol("DT_GIR");

			// 3.거래처
			_flex.Cols[3].Name = "LN_PARTNER";
			_flex.Cols[3].DataType = typeof(string);
			_flex.Cols[3].Width = 120;
			_flex.Cols[3].AllowEditing = false;

			// 4.출하의뢰자
			_flex.Cols[4].Name = "NM_KOR";
			_flex.Cols[4].DataType = typeof(string);
			_flex.Cols[4].Width = 90;
			_flex.Cols[4].AllowEditing = false;

			// 5.의뢰공장명
			_flex.Cols[5].Name = "NM_PLANT";
			_flex.Cols[5].DataType = typeof(string);
			_flex.Cols[5].Width = 114;
			_flex.Cols[5].AllowEditing = false;

			// 6.거래구분
			_flex.Cols[6].Name = "NM_BUSI";
			_flex.Cols[6].DataType = typeof(string);
			_flex.Cols[6].Width = 68;
			_flex.Cols[6].AllowEditing = false;
			
			// 7.처리상태
			_flex.Cols[7].Name = "STA_GIRNM";
			_flex.Cols[7].DataType = typeof(string);
			_flex.Cols[7].Width = 68;
			_flex.Cols[7].AllowEditing = false;

            // 8.출하형태
            _flex.Cols[8].Name = "NM_TP_GI";
            _flex.Cols[8].DataType = typeof(string);
            _flex.Cols[8].Width = 100;
            _flex.Cols[8].AllowEditing = false;

			_flex.AllowSorting = AllowSortingEnum.None;
			_flex.NewRowEditable = true;
			_flex.EnterKeyAddRow = true;

			_flex.SumPosition = SumPositionEnum.None;
			_flex.GridStyle = GridStyleEnum.Green;

			this.m_imain.SetUserGrid(_flex);
			
			// 그리드 헤더캡션 표시하기
			for(int i = 0; i <= _flex.Cols.Count-1; i++)
				_flex[0, i] = GetDDItem(_flex.Cols[i].Name);

			_flex.Redraw = true;

			// 그리드 이벤트 선언
			_flex.HelpClick			+= new System.EventHandler(this.OnShowHelp);
		}

		#endregion

		#region -> InitControl

		/// <summary>
		/// 컨트롤들의 캡션을 데이터 사전을 이용하여 설정한다.
		/// </summary>
		private void InitControl()
		{
			try
			{
				//마스크 셋팅
				this.m_mskStart.Mask	= this.m_imain.GetFormatDescription(DataDictionaryTypes.SA,FormatTpType.YEAR_MONTH_DAY,FormatFgType.INSERT);
				this.m_mskEnd.Mask		= this.m_imain.GetFormatDescription(DataDictionaryTypes.SA,FormatTpType.YEAR_MONTH_DAY,FormatFgType.INSERT);

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
										((Duzon.Common.Controls.LabelExt)ctrls).Text = m_imain.GetDataDictionaryItem(DataDictionaryTypes.SA, (string)((Duzon.Common.Controls.LabelExt)ctrls).Tag);
								}
							}
						}
					}
				}
				m_lblTitle.Text		= m_imain.GetDataDictionaryItem(DataDictionaryTypes.SA, "P_SA_GIR_SCH_SUB");
				m_btnQuery.Text		= m_imain.GetDataDictionaryItem(DataDictionaryTypes.SA, "Q_QUERY");
				m_btnConfirm.Text	= m_imain.GetDataDictionaryItem(DataDictionaryTypes.SA, "Q_CONFIRM");
				m_btnCancel.Text	= m_imain.GetDataDictionaryItem(DataDictionaryTypes.SA, "Q_CANCEL");

				DataSet g_dsCombo = this.m_imain.GetComboData("NC;MA_PLANT");

				// 거래구분
				m_cboPlantGir.DataSource	= g_dsCombo.Tables[0];
				m_cboPlantGir.DisplayMember = "NAME";
				m_cboPlantGir.ValueMember	= "CODE";

				m_cboPlantGir.SelectedValue = m_stplant;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		#endregion

		#endregion

		#region ♣ 메인버튼 이벤트 / 메소드

		#region -> DoContinue

		private bool DoContinue()
		{
			if(_flex.Editor != null)
			{
				return _flex.FinishEditing(false);
			}
			
			return true;
		}

		#endregion

		#region -> 조회조건체크

		private bool SearchCondition()
		{
			//의뢰일자
			if((m_mskStart.Text.Trim() == "") || (m_mskEnd.Text.Trim() == ""))
			{
				//의뢰일자 은(는) 필수 입력입니다.
				this.m_imain.ShowMessage("WK1_004", m_lblDtGir.Text);
				m_mskStart.Focus();
				return false;
			}

			return true;
		}

		#endregion

		#region -> 조회버튼클릭

		/// <summary>
		/// 조회
		/// </summary>
		private void OnSearchButtonClicked(object sender, System.EventArgs e)
		{		
			if(!DoContinue())
				return;
			try
			{
				this.m_lblTitle.Focus();
				
				if(!SearchCondition())
					return;

				Duzon.Common.Util.SpInfo si = new Duzon.Common.Util.SpInfo();
				
				si.SpNameSelect = "SP_SA_GIR_SELECT_SUB";
				si.SpParamsSelect = new Object[]{m_imain.LoginInfo.CompanyCode, 
													m_mskStart.Text, 
													m_mskEnd.Text, 
													bpNm_Partner.CodeValue, 
													m_cboPlantGir.SelectedValue.ToString(), 
													bpTpGi.CodeValue,
													m_FgGirq};

				object ret = this.m_imain.FillDataTable(si);
				ResultData result = (ResultData)m_imain.FillDataTable(si);
				DataTable dt= (DataTable)result.DataValue; 

				_flex.Redraw = false;
				_flex.BindingStart();
				_flex.DataSource = dt.DefaultView;
				_flex.BindingEnd();
				_flex.Redraw = true;

				if(!_flex.HasNormalRow)
				{
					// 검색된 내용이 존재하지 않습니다..
					this.m_imain.ShowMessage("IK1_003");
					return;
				}
				else
				{
					m_btnConfirm.Enabled=true;
				}
			}
			catch(Exception ex)
			{
				m_imain.MsgEnd(ex);
			}			
		}

		#endregion

		#region -> 적용버튼클릭

		private void OnApply(object sender, System.EventArgs e)
		{
			OnShowHelp(sender, e);
		}

		#endregion

		#endregion

		#region ♣ 그리드 이벤트 / 메소드

		#region -> OnShowHelp

		private void OnShowHelp(object sender, System.EventArgs e)
		{
			object dlg = null;
			DataRowView ldr_src = null;

			try
			{
				switch(sender.GetType().Name)
				{
					case "FlexGrid" :
						if(_flex.DataView == null || _flex.DataView.Count == 0)
							return;

						ldr_src = _flex.DataView[_flex.DataIndex(_flex.Row)];
						
						m_return[0] = ldr_src;
						this.DialogResult= DialogResult.OK;
						break;
					case "RoundedButton" :
						if(((RoundedButton)sender).Name == "m_btnConfirm")
						{
							if(_flex.DataView == null || _flex.DataView.Count == 0)
								return;

							ldr_src = _flex.DataView[_flex.DataIndex(_flex.Row)];
							m_return[0] = ldr_src;
							this.DialogResult= DialogResult.OK;
						}
						break;
					
					default :
						break;
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		#endregion

		#endregion

		#region ♣ 이벤트 / 메소드

		#region -> OnComboKeyDown

		private void OnComboKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyData == Keys.Enter)
				SendKeys.Send("{TAB}");	
		}

		#endregion
		
		#region -> m_btnCancel_Click

		private void m_btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		#endregion

		//===== 메소드 =====//
	
		#region >> 날짜 관련 함수 및 이벤트 
		
		private void m_mskStart_Validated(object sender, System.EventArgs e)
		{
			if(!this.m_mskStart.IsValidated)
			{
				m_imain.ShowMessage("WK1_003", m_lblDtGir.Text);
				this.m_mskStart.Focus();
			}

		}

		private void m_mskEnd_Validated(object sender, System.EventArgs e)
		{
			if(!this.m_mskEnd.IsValidated)
			{
				m_imain.ShowMessage("WK1_003", m_lblDtGir.Text);
				this.m_mskEnd.Focus();
			}

		}
		#endregion

		private void Contro_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
		{
			if(e.DialogResult == DialogResult.Cancel)
				return;

			switch(e.ControlName)
			{
				case "bpNm_Partner":	// 거래처						
					bpNm_Partner.CodeValue = e.HelpReturn.Rows[0]["CD_PARTNER"].ToString();
					bpNm_Partner.CodeName = e.HelpReturn.Rows[0]["LN_PARTNER"].ToString();					
					break;
                case "bpTpGi":
                    bpTpGi.CodeValue = e.CodeValue;
                    bpTpGi.CodeName = e.CodeName;
                    break;
				
                //case "bpNm_Sl":			// 창고									
                //    bpNm_Sl.CodeName = e.HelpReturn.Rows[0]["NM_SL"].ToString();
                //    bpNm_Sl.CodeValue = e.HelpReturn.Rows[0]["CD_SL"].ToString();
                //    break;
			}
		}

		private void Control_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
		{
			e.HelpParam.MainFrame = m_imain;

			if(m_cboPlantGir.SelectedValue.ToString() ==string.Empty)
			{
				//창고 서브창을 띄우기 위해서는 공장데이터가 있어야 합니다.() 은(는) 필수 입력입니다.
				this.m_imain.ShowMessage("WK1_004", m_lblPlantGir.Text);
				m_cboPlantGir.Focus();
			}
			switch(e.HelpID)
			{				
				case Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB:
					e.HelpParam.P09_CD_PLANT = m_cboPlantGir.SelectedValue.ToString();
					break;
                case Duzon.Common.Forms.Help.HelpID.P_PU_EJTP_SUB:
                    e.HelpParam.P61_CODE1 = "010|";
                    break;
			}
		}

		#region -> 데이터ROW 리턴

		object[] IHelpWindow.ReturnValues
		{
			get { return m_return;	}
		}
		
		#endregion

		#endregion

	}
}
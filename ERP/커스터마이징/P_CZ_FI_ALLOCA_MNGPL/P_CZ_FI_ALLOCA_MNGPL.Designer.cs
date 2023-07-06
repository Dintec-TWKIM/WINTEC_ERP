using System.ComponentModel;
using Duzon.Common.Controls;
using Duzon.Erpiu.Windows.OneControls;
using Duzon.Common.BpControls;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using System.Drawing;
using Duzon.Common.Forms.Help;
using Dass.FlexGrid;

namespace cz
{
    partial class P_CZ_FI_ALLOCA_MNGPL
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_FI_ALLOCA_MNGPL));
            this.tableLayoutPanel1 = new cz.DoubleBufferedPanel();
            this.m_pnlGrid = new Duzon.Common.Controls.PanelExt();
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this.oneGrid = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            this.chk증감율표시 = new Duzon.Common.Controls.CheckBoxExt();
            this.bppnl양식명 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl양식명 = new Duzon.Common.Controls.LabelExt();
            this.cbo양식명 = new Duzon.Common.Controls.DropDownComboBox();
            this.bppnl당기기준일자 = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp당기기준일자 = new Duzon.Common.Controls.PeriodPicker();
            this.lbl당기기준일자 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp전기기준일자 = new Duzon.Common.Controls.PeriodPicker();
            this.lbl전기기준일자 = new Duzon.Common.Controls.LabelExt();
            this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.chk배부단위그룹표시 = new Duzon.Common.Controls.CheckBoxExt();
            this.bppnl계정레벨 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl계정레벨 = new Duzon.Common.Controls.LabelExt();
            this.cbo계정레벨 = new Duzon.Common.Controls.DropDownComboBox();
            this.bppnl화폐단위 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl화폐단위 = new Duzon.Common.Controls.LabelExt();
            this.cbo화폐단위 = new Duzon.Common.Controls.DropDownComboBox();
            this.bppnl사용언어 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl사용언어 = new Duzon.Common.Controls.LabelExt();
            this.cbo사용언어 = new Duzon.Common.Controls.DropDownComboBox();
            this.oneGridItem3 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl10 = new Duzon.Common.BpControls.BpPanelControl();
            this.chk단위필터 = new Duzon.Common.Controls.CheckBoxExt();
            this.bppnl관리내역 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl관리내역 = new Duzon.Common.Controls.LabelExt();
            this.bpc관리내역 = new Duzon.Common.BpControls.BpComboBox();
            this.bppnl단위구분 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl단위구분 = new Duzon.Common.Controls.LabelExt();
            this.cbo단위구분 = new Duzon.Common.Controls.DropDownComboBox();
            this.bppnl배부구분 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl배부구분 = new Duzon.Common.Controls.LabelExt();
            this.cbo배부구분 = new Duzon.Common.Controls.DropDownComboBox();
            this.chk배부단위표시 = new Duzon.Common.Controls.CheckBoxExt();
            this.mDataArea.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.m_pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl3.SuspendLayout();
            this.bppnl양식명.SuspendLayout();
            this.bppnl당기기준일자.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            this.oneGridItem2.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            this.bppnl계정레벨.SuspendLayout();
            this.bppnl화폐단위.SuspendLayout();
            this.bppnl사용언어.SuspendLayout();
            this.oneGridItem3.SuspendLayout();
            this.bpPanelControl10.SuspendLayout();
            this.bppnl관리내역.SuspendLayout();
            this.bppnl단위구분.SuspendLayout();
            this.bppnl배부구분.SuspendLayout();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this.tableLayoutPanel1);
            this.mDataArea.Size = new System.Drawing.Size(1199, 756);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.m_pnlGrid, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.oneGrid, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 92F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1199, 756);
            this.tableLayoutPanel1.TabIndex = 98;
            // 
            // m_pnlGrid
            // 
            this.m_pnlGrid.Controls.Add(this._flex);
            this.m_pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlGrid.Location = new System.Drawing.Point(0, 92);
            this.m_pnlGrid.Margin = new System.Windows.Forms.Padding(0);
            this.m_pnlGrid.Name = "m_pnlGrid";
            this.m_pnlGrid.Size = new System.Drawing.Size(1199, 664);
            this.m_pnlGrid.TabIndex = 4;
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
            this._flex.Font = new System.Drawing.Font("굴림", 9F);
            this._flex.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex.Location = new System.Drawing.Point(0, 0);
            this._flex.Margin = new System.Windows.Forms.Padding(0);
            this._flex.Name = "_flex";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 20;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(1199, 664);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 5;
            this._flex.UseGridCalculator = true;
            // 
            // oneGrid
            // 
            this.oneGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oneGrid.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2,
            this.oneGridItem3});
            this.oneGrid.Location = new System.Drawing.Point(3, 3);
            this.oneGrid.Name = "oneGrid";
            this.oneGrid.Size = new System.Drawing.Size(1193, 86);
            this.oneGrid.TabIndex = 95;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bpPanelControl3);
            this.oneGridItem1.Controls.Add(this.bppnl양식명);
            this.oneGridItem1.Controls.Add(this.bppnl당기기준일자);
            this.oneGridItem1.Controls.Add(this.bpPanelControl1);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(1183, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl3
            // 
            this.bpPanelControl3.Controls.Add(this.chk증감율표시);
            this.bpPanelControl3.Location = new System.Drawing.Point(884, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl3.TabIndex = 4;
            // 
            // chk증감율표시
            // 
            this.chk증감율표시.AutoSize = true;
            this.chk증감율표시.Checked = true;
            this.chk증감율표시.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk증감율표시.Dock = System.Windows.Forms.DockStyle.Left;
            this.chk증감율표시.Location = new System.Drawing.Point(0, 0);
            this.chk증감율표시.Margin = new System.Windows.Forms.Padding(0);
            this.chk증감율표시.Name = "chk증감율표시";
            this.chk증감율표시.Size = new System.Drawing.Size(84, 23);
            this.chk증감율표시.TabIndex = 118;
            this.chk증감율표시.Text = "증감율표시";
            this.chk증감율표시.TextDD = null;
            this.chk증감율표시.UseVisualStyleBackColor = true;
            // 
            // bppnl양식명
            // 
            this.bppnl양식명.Controls.Add(this.lbl양식명);
            this.bppnl양식명.Controls.Add(this.cbo양식명);
            this.bppnl양식명.Location = new System.Drawing.Point(590, 1);
            this.bppnl양식명.Name = "bppnl양식명";
            this.bppnl양식명.Size = new System.Drawing.Size(292, 23);
            this.bppnl양식명.TabIndex = 1;
            // 
            // lbl양식명
            // 
            this.lbl양식명.BackColor = System.Drawing.Color.Transparent;
            this.lbl양식명.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl양식명.Location = new System.Drawing.Point(0, 0);
            this.lbl양식명.Margin = new System.Windows.Forms.Padding(0);
            this.lbl양식명.Name = "lbl양식명";
            this.lbl양식명.Size = new System.Drawing.Size(100, 23);
            this.lbl양식명.TabIndex = 0;
            this.lbl양식명.Tag = "";
            this.lbl양식명.Text = "양식명";
            this.lbl양식명.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbo양식명
            // 
            this.cbo양식명.AutoDropDown = true;
            this.cbo양식명.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.cbo양식명.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbo양식명.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo양식명.ItemHeight = 12;
            this.cbo양식명.Location = new System.Drawing.Point(107, 0);
            this.cbo양식명.Margin = new System.Windows.Forms.Padding(0);
            this.cbo양식명.Name = "cbo양식명";
            this.cbo양식명.Size = new System.Drawing.Size(185, 20);
            this.cbo양식명.TabIndex = 3;
            // 
            // bppnl당기기준일자
            // 
            this.bppnl당기기준일자.Controls.Add(this.dtp당기기준일자);
            this.bppnl당기기준일자.Controls.Add(this.lbl당기기준일자);
            this.bppnl당기기준일자.Location = new System.Drawing.Point(296, 1);
            this.bppnl당기기준일자.Name = "bppnl당기기준일자";
            this.bppnl당기기준일자.Size = new System.Drawing.Size(292, 23);
            this.bppnl당기기준일자.TabIndex = 0;
            // 
            // dtp당기기준일자
            // 
            this.dtp당기기준일자.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp당기기준일자.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtp당기기준일자.IsNecessaryCondition = true;
            this.dtp당기기준일자.Location = new System.Drawing.Point(107, 0);
            this.dtp당기기준일자.MaximumSize = new System.Drawing.Size(185, 21);
            this.dtp당기기준일자.MinimumSize = new System.Drawing.Size(185, 21);
            this.dtp당기기준일자.Name = "dtp당기기준일자";
            this.dtp당기기준일자.Size = new System.Drawing.Size(185, 21);
            this.dtp당기기준일자.TabIndex = 1;
            // 
            // lbl당기기준일자
            // 
            this.lbl당기기준일자.BackColor = System.Drawing.Color.Transparent;
            this.lbl당기기준일자.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl당기기준일자.Location = new System.Drawing.Point(0, 0);
            this.lbl당기기준일자.Margin = new System.Windows.Forms.Padding(0);
            this.lbl당기기준일자.Name = "lbl당기기준일자";
            this.lbl당기기준일자.Size = new System.Drawing.Size(100, 23);
            this.lbl당기기준일자.TabIndex = 0;
            this.lbl당기기준일자.Tag = "";
            this.lbl당기기준일자.Text = "당기기준일자";
            this.lbl당기기준일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.dtp전기기준일자);
            this.bpPanelControl1.Controls.Add(this.lbl전기기준일자);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl1.TabIndex = 2;
            // 
            // dtp전기기준일자
            // 
            this.dtp전기기준일자.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp전기기준일자.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtp전기기준일자.IsNecessaryCondition = true;
            this.dtp전기기준일자.Location = new System.Drawing.Point(107, 0);
            this.dtp전기기준일자.MaximumSize = new System.Drawing.Size(185, 21);
            this.dtp전기기준일자.MinimumSize = new System.Drawing.Size(185, 21);
            this.dtp전기기준일자.Name = "dtp전기기준일자";
            this.dtp전기기준일자.Size = new System.Drawing.Size(185, 21);
            this.dtp전기기준일자.TabIndex = 1;
            // 
            // lbl전기기준일자
            // 
            this.lbl전기기준일자.BackColor = System.Drawing.Color.Transparent;
            this.lbl전기기준일자.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl전기기준일자.Location = new System.Drawing.Point(0, 0);
            this.lbl전기기준일자.Margin = new System.Windows.Forms.Padding(0);
            this.lbl전기기준일자.Name = "lbl전기기준일자";
            this.lbl전기기준일자.Size = new System.Drawing.Size(100, 23);
            this.lbl전기기준일자.TabIndex = 0;
            this.lbl전기기준일자.Tag = "";
            this.lbl전기기준일자.Text = "전기기준일자";
            this.lbl전기기준일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // oneGridItem2
            // 
            this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem2.Controls.Add(this.bpPanelControl2);
            this.oneGridItem2.Controls.Add(this.bppnl계정레벨);
            this.oneGridItem2.Controls.Add(this.bppnl화폐단위);
            this.oneGridItem2.Controls.Add(this.bppnl사용언어);
            this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem2.Name = "oneGridItem2";
            this.oneGridItem2.Size = new System.Drawing.Size(1183, 23);
            this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 1;
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.chk배부단위표시);
            this.bpPanelControl2.Controls.Add(this.chk배부단위그룹표시);
            this.bpPanelControl2.Location = new System.Drawing.Point(884, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl2.TabIndex = 4;
            // 
            // chk배부단위그룹표시
            // 
            this.chk배부단위그룹표시.AutoSize = true;
            this.chk배부단위그룹표시.Checked = true;
            this.chk배부단위그룹표시.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk배부단위그룹표시.Dock = System.Windows.Forms.DockStyle.Left;
            this.chk배부단위그룹표시.Location = new System.Drawing.Point(0, 0);
            this.chk배부단위그룹표시.Margin = new System.Windows.Forms.Padding(0);
            this.chk배부단위그룹표시.Name = "chk배부단위그룹표시";
            this.chk배부단위그룹표시.Size = new System.Drawing.Size(120, 23);
            this.chk배부단위그룹표시.TabIndex = 117;
            this.chk배부단위그룹표시.Text = "배부단위그룹표시";
            this.chk배부단위그룹표시.TextDD = null;
            this.chk배부단위그룹표시.UseVisualStyleBackColor = true;
            // 
            // bppnl계정레벨
            // 
            this.bppnl계정레벨.Controls.Add(this.lbl계정레벨);
            this.bppnl계정레벨.Controls.Add(this.cbo계정레벨);
            this.bppnl계정레벨.Location = new System.Drawing.Point(590, 1);
            this.bppnl계정레벨.Name = "bppnl계정레벨";
            this.bppnl계정레벨.Size = new System.Drawing.Size(292, 23);
            this.bppnl계정레벨.TabIndex = 2;
            // 
            // lbl계정레벨
            // 
            this.lbl계정레벨.BackColor = System.Drawing.Color.Transparent;
            this.lbl계정레벨.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl계정레벨.Location = new System.Drawing.Point(0, 0);
            this.lbl계정레벨.Margin = new System.Windows.Forms.Padding(0);
            this.lbl계정레벨.Name = "lbl계정레벨";
            this.lbl계정레벨.Size = new System.Drawing.Size(100, 23);
            this.lbl계정레벨.TabIndex = 0;
            this.lbl계정레벨.Tag = "";
            this.lbl계정레벨.Text = "계정레벨";
            this.lbl계정레벨.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbo계정레벨
            // 
            this.cbo계정레벨.AutoDropDown = true;
            this.cbo계정레벨.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.cbo계정레벨.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbo계정레벨.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo계정레벨.ItemHeight = 12;
            this.cbo계정레벨.Location = new System.Drawing.Point(107, 0);
            this.cbo계정레벨.Margin = new System.Windows.Forms.Padding(0);
            this.cbo계정레벨.Name = "cbo계정레벨";
            this.cbo계정레벨.Size = new System.Drawing.Size(185, 20);
            this.cbo계정레벨.TabIndex = 4;
            // 
            // bppnl화폐단위
            // 
            this.bppnl화폐단위.Controls.Add(this.lbl화폐단위);
            this.bppnl화폐단위.Controls.Add(this.cbo화폐단위);
            this.bppnl화폐단위.Location = new System.Drawing.Point(296, 1);
            this.bppnl화폐단위.Name = "bppnl화폐단위";
            this.bppnl화폐단위.Size = new System.Drawing.Size(292, 23);
            this.bppnl화폐단위.TabIndex = 2;
            // 
            // lbl화폐단위
            // 
            this.lbl화폐단위.BackColor = System.Drawing.Color.Transparent;
            this.lbl화폐단위.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl화폐단위.Location = new System.Drawing.Point(0, 0);
            this.lbl화폐단위.Margin = new System.Windows.Forms.Padding(0);
            this.lbl화폐단위.Name = "lbl화폐단위";
            this.lbl화폐단위.Size = new System.Drawing.Size(100, 23);
            this.lbl화폐단위.TabIndex = 0;
            this.lbl화폐단위.Tag = "";
            this.lbl화폐단위.Text = "화폐단위";
            this.lbl화폐단위.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbo화폐단위
            // 
            this.cbo화폐단위.AutoDropDown = true;
            this.cbo화폐단위.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.cbo화폐단위.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbo화폐단위.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo화폐단위.ItemHeight = 12;
            this.cbo화폐단위.Location = new System.Drawing.Point(107, 0);
            this.cbo화폐단위.Margin = new System.Windows.Forms.Padding(0);
            this.cbo화폐단위.Name = "cbo화폐단위";
            this.cbo화폐단위.Size = new System.Drawing.Size(185, 20);
            this.cbo화폐단위.TabIndex = 8;
            // 
            // bppnl사용언어
            // 
            this.bppnl사용언어.Controls.Add(this.lbl사용언어);
            this.bppnl사용언어.Controls.Add(this.cbo사용언어);
            this.bppnl사용언어.Location = new System.Drawing.Point(2, 1);
            this.bppnl사용언어.Name = "bppnl사용언어";
            this.bppnl사용언어.Size = new System.Drawing.Size(292, 23);
            this.bppnl사용언어.TabIndex = 1;
            // 
            // lbl사용언어
            // 
            this.lbl사용언어.BackColor = System.Drawing.Color.Transparent;
            this.lbl사용언어.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl사용언어.Location = new System.Drawing.Point(0, 0);
            this.lbl사용언어.Margin = new System.Windows.Forms.Padding(0);
            this.lbl사용언어.Name = "lbl사용언어";
            this.lbl사용언어.Size = new System.Drawing.Size(100, 23);
            this.lbl사용언어.TabIndex = 0;
            this.lbl사용언어.Tag = "";
            this.lbl사용언어.Text = "사용언어";
            this.lbl사용언어.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbo사용언어
            // 
            this.cbo사용언어.AutoDropDown = true;
            this.cbo사용언어.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.cbo사용언어.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbo사용언어.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo사용언어.ItemHeight = 12;
            this.cbo사용언어.Location = new System.Drawing.Point(107, 0);
            this.cbo사용언어.Margin = new System.Windows.Forms.Padding(0);
            this.cbo사용언어.Name = "cbo사용언어";
            this.cbo사용언어.Size = new System.Drawing.Size(185, 20);
            this.cbo사용언어.TabIndex = 7;
            // 
            // oneGridItem3
            // 
            this.oneGridItem3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem3.Controls.Add(this.bpPanelControl10);
            this.oneGridItem3.Controls.Add(this.bppnl관리내역);
            this.oneGridItem3.Controls.Add(this.bppnl단위구분);
            this.oneGridItem3.Controls.Add(this.bppnl배부구분);
            this.oneGridItem3.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem3.Location = new System.Drawing.Point(0, 46);
            this.oneGridItem3.Name = "oneGridItem3";
            this.oneGridItem3.Size = new System.Drawing.Size(1183, 23);
            this.oneGridItem3.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem3.TabIndex = 2;
            // 
            // bpPanelControl10
            // 
            this.bpPanelControl10.Controls.Add(this.chk단위필터);
            this.bpPanelControl10.Location = new System.Drawing.Point(884, 1);
            this.bpPanelControl10.Name = "bpPanelControl10";
            this.bpPanelControl10.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl10.TabIndex = 3;
            // 
            // chk단위필터
            // 
            this.chk단위필터.AutoSize = true;
            this.chk단위필터.Checked = true;
            this.chk단위필터.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk단위필터.Dock = System.Windows.Forms.DockStyle.Left;
            this.chk단위필터.Location = new System.Drawing.Point(0, 0);
            this.chk단위필터.Margin = new System.Windows.Forms.Padding(0);
            this.chk단위필터.Name = "chk단위필터";
            this.chk단위필터.Size = new System.Drawing.Size(72, 23);
            this.chk단위필터.TabIndex = 115;
            this.chk단위필터.Text = "단위필터";
            this.chk단위필터.TextDD = null;
            this.chk단위필터.UseVisualStyleBackColor = true;
            // 
            // bppnl관리내역
            // 
            this.bppnl관리내역.Controls.Add(this.lbl관리내역);
            this.bppnl관리내역.Controls.Add(this.bpc관리내역);
            this.bppnl관리내역.Location = new System.Drawing.Point(590, 1);
            this.bppnl관리내역.Name = "bppnl관리내역";
            this.bppnl관리내역.Size = new System.Drawing.Size(292, 23);
            this.bppnl관리내역.TabIndex = 2;
            // 
            // lbl관리내역
            // 
            this.lbl관리내역.BackColor = System.Drawing.Color.Transparent;
            this.lbl관리내역.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl관리내역.Location = new System.Drawing.Point(0, 0);
            this.lbl관리내역.Margin = new System.Windows.Forms.Padding(0);
            this.lbl관리내역.Name = "lbl관리내역";
            this.lbl관리내역.Size = new System.Drawing.Size(100, 23);
            this.lbl관리내역.TabIndex = 0;
            this.lbl관리내역.Tag = "";
            this.lbl관리내역.Text = "관리내역";
            this.lbl관리내역.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpc관리내역
            // 
            this.bpc관리내역.ComboCheck = false;
            this.bpc관리내역.Dock = System.Windows.Forms.DockStyle.Right;
            this.bpc관리내역.HelpID = Duzon.Common.Forms.Help.HelpID.P_FI_ALLOCAUNIT_SUB1;
            this.bpc관리내역.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.bpc관리내역.Location = new System.Drawing.Point(107, 0);
            this.bpc관리내역.Margin = new System.Windows.Forms.Padding(0);
            this.bpc관리내역.Name = "bpc관리내역";
            this.bpc관리내역.SetNoneTypeMsg = "관리항목이 설정 되어 있지않습니다.";
            this.bpc관리내역.Size = new System.Drawing.Size(185, 21);
            this.bpc관리내역.TabIndex = 6;
            this.bpc관리내역.TabStop = false;
            this.bpc관리내역.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.bpc관리내역_QueryBefore);
            // 
            // bppnl단위구분
            // 
            this.bppnl단위구분.Controls.Add(this.lbl단위구분);
            this.bppnl단위구분.Controls.Add(this.cbo단위구분);
            this.bppnl단위구분.Location = new System.Drawing.Point(296, 1);
            this.bppnl단위구분.Name = "bppnl단위구분";
            this.bppnl단위구분.Size = new System.Drawing.Size(292, 23);
            this.bppnl단위구분.TabIndex = 1;
            // 
            // lbl단위구분
            // 
            this.lbl단위구분.BackColor = System.Drawing.Color.Transparent;
            this.lbl단위구분.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl단위구분.Location = new System.Drawing.Point(0, 0);
            this.lbl단위구분.Margin = new System.Windows.Forms.Padding(0);
            this.lbl단위구분.Name = "lbl단위구분";
            this.lbl단위구분.Size = new System.Drawing.Size(100, 23);
            this.lbl단위구분.TabIndex = 0;
            this.lbl단위구분.Tag = "";
            this.lbl단위구분.Text = "단위구분";
            this.lbl단위구분.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbo단위구분
            // 
            this.cbo단위구분.AutoDropDown = true;
            this.cbo단위구분.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.cbo단위구분.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbo단위구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo단위구분.ItemHeight = 12;
            this.cbo단위구분.Location = new System.Drawing.Point(107, 0);
            this.cbo단위구분.Margin = new System.Windows.Forms.Padding(0);
            this.cbo단위구분.Name = "cbo단위구분";
            this.cbo단위구분.Size = new System.Drawing.Size(185, 20);
            this.cbo단위구분.TabIndex = 106;
            // 
            // bppnl배부구분
            // 
            this.bppnl배부구분.Controls.Add(this.lbl배부구분);
            this.bppnl배부구분.Controls.Add(this.cbo배부구분);
            this.bppnl배부구분.Location = new System.Drawing.Point(2, 1);
            this.bppnl배부구분.Name = "bppnl배부구분";
            this.bppnl배부구분.Size = new System.Drawing.Size(292, 23);
            this.bppnl배부구분.TabIndex = 3;
            // 
            // lbl배부구분
            // 
            this.lbl배부구분.BackColor = System.Drawing.Color.Transparent;
            this.lbl배부구분.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl배부구분.Location = new System.Drawing.Point(0, 0);
            this.lbl배부구분.Margin = new System.Windows.Forms.Padding(0);
            this.lbl배부구분.Name = "lbl배부구분";
            this.lbl배부구분.Size = new System.Drawing.Size(100, 23);
            this.lbl배부구분.TabIndex = 1;
            this.lbl배부구분.Tag = "";
            this.lbl배부구분.Text = "배부구분";
            this.lbl배부구분.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbo배부구분
            // 
            this.cbo배부구분.AutoDropDown = true;
            this.cbo배부구분.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.cbo배부구분.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbo배부구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo배부구분.ItemHeight = 12;
            this.cbo배부구분.Location = new System.Drawing.Point(107, 0);
            this.cbo배부구분.Margin = new System.Windows.Forms.Padding(0);
            this.cbo배부구분.Name = "cbo배부구분";
            this.cbo배부구분.Size = new System.Drawing.Size(185, 20);
            this.cbo배부구분.TabIndex = 105;
            // 
            // chk배부단위표시
            // 
            this.chk배부단위표시.AutoSize = true;
            this.chk배부단위표시.Checked = true;
            this.chk배부단위표시.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk배부단위표시.Dock = System.Windows.Forms.DockStyle.Left;
            this.chk배부단위표시.Location = new System.Drawing.Point(120, 0);
            this.chk배부단위표시.Margin = new System.Windows.Forms.Padding(0);
            this.chk배부단위표시.Name = "chk배부단위표시";
            this.chk배부단위표시.Size = new System.Drawing.Size(96, 23);
            this.chk배부단위표시.TabIndex = 118;
            this.chk배부단위표시.Text = "배부단위표시";
            this.chk배부단위표시.TextDD = null;
            this.chk배부단위표시.UseVisualStyleBackColor = true;
            // 
            // P_CZ_FI_ALLOCA_MNGPL
            // 
            this.Name = "P_CZ_FI_ALLOCA_MNGPL";
            this.Size = new System.Drawing.Size(1199, 796);
            this.TitleText = "배부단위별 손익현황";
            this.mDataArea.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.m_pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl3.ResumeLayout(false);
            this.bpPanelControl3.PerformLayout();
            this.bppnl양식명.ResumeLayout(false);
            this.bppnl당기기준일자.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            this.oneGridItem2.ResumeLayout(false);
            this.bpPanelControl2.ResumeLayout(false);
            this.bpPanelControl2.PerformLayout();
            this.bppnl계정레벨.ResumeLayout(false);
            this.bppnl화폐단위.ResumeLayout(false);
            this.bppnl사용언어.ResumeLayout(false);
            this.oneGridItem3.ResumeLayout(false);
            this.bpPanelControl10.ResumeLayout(false);
            this.bpPanelControl10.PerformLayout();
            this.bppnl관리내역.ResumeLayout(false);
            this.bppnl단위구분.ResumeLayout(false);
            this.bppnl배부구분.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private DoubleBufferedPanel tableLayoutPanel1;
        private LabelExt lbl당기기준일자;
        private PanelExt m_pnlGrid;
        private FlexGrid _flex;
        private LabelExt lbl사용언어;
        private LabelExt lbl양식명;
        private LabelExt lbl화폐단위;
        private LabelExt lbl계정레벨;
        private DropDownComboBox cbo양식명;
        private DropDownComboBox cbo사용언어;
        private DropDownComboBox cbo계정레벨;
        private DropDownComboBox cbo화폐단위;
        private DropDownComboBox cbo배부구분;
        private LabelExt lbl배부구분;
        private LabelExt lbl단위구분;
        private DropDownComboBox cbo단위구분;
        private LabelExt lbl관리내역;
        private BpComboBox bpc관리내역;
        private CheckBoxExt chk단위필터;
        private OneGrid oneGrid;
        private OneGridItem oneGridItem1;
        private BpPanelControl bppnl계정레벨;
        private BpPanelControl bppnl양식명;
        private BpPanelControl bppnl당기기준일자;
        private OneGridItem oneGridItem2;
        private BpPanelControl bppnl배부구분;
        private BpPanelControl bppnl화폐단위;
        private BpPanelControl bppnl사용언어;
        private OneGridItem oneGridItem3;
        private BpPanelControl bpPanelControl10;
        private BpPanelControl bppnl관리내역;
        private BpPanelControl bppnl단위구분;
        private PeriodPicker dtp당기기준일자;
        #endregion
        private BpPanelControl bpPanelControl1;
        private PeriodPicker dtp전기기준일자;
        private LabelExt lbl전기기준일자;
        private BpPanelControl bpPanelControl3;
        private BpPanelControl bpPanelControl2;
        private CheckBoxExt chk배부단위그룹표시;
        private CheckBoxExt chk증감율표시;
        private CheckBoxExt chk배부단위표시;
    }
}
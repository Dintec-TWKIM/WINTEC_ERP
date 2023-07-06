using System.ComponentModel;
using Duzon.Common.Controls;
using Duzon.Common.BpControls;
using System.Windows.Forms;
using System.Drawing;
using System;
using C1.Win.C1FlexGrid;
using Duzon.Common.Forms.Help;

namespace cz
{
    partial class P_CZ_PU_PO_SUB2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_PU_PO_SUB2));
            this.btn조회 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn복사 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn적용 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn취소 = new Duzon.Common.Controls.RoundedButton(this.components);
            this._flexH = new Dass.FlexGrid.FlexGrid(this.components);
            this._flexD = new Dass.FlexGrid.FlexGrid(this.components);
            this.dtp지급일자 = new Duzon.Common.Controls.PeriodPicker();
            this.txt지급번호 = new Duzon.Common.Controls.TextBoxExt();
            this.ctx발주유형 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl담당자 = new Duzon.Common.Controls.LabelExt();
            this.ctx담당자 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.ctx매입처 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.ctx구매그룹 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.cbo공장 = new Duzon.Common.Controls.DropDownComboBox();
            this.lbl지급일자 = new Duzon.Common.Controls.LabelExt();
            this.lbl발주유형 = new Duzon.Common.Controls.LabelExt();
            this.lbl프로젝트 = new Duzon.Common.Controls.LabelExt();
            this.lbl지급번호 = new Duzon.Common.Controls.LabelExt();
            this.lbl구매그룹 = new Duzon.Common.Controls.LabelExt();
            this.lbl공장 = new Duzon.Common.Controls.LabelExt();
            this.lbl매입처 = new Duzon.Common.Controls.LabelExt();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
            this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl8 = new Duzon.Common.BpControls.BpPanelControl();
            this.bpPanelControl6 = new Duzon.Common.BpControls.BpPanelControl();
            this.txt프로젝트 = new Duzon.Common.Controls.TextBoxExt();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.oneGridItem3 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl7 = new Duzon.Common.BpControls.BpPanelControl();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexD)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl4.SuspendLayout();
            this.bpPanelControl5.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            this.oneGridItem2.SuspendLayout();
            this.bpPanelControl8.SuspendLayout();
            this.bpPanelControl6.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            this.oneGridItem3.SuspendLayout();
            this.bpPanelControl7.SuspendLayout();
            this.bpPanelControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn조회
            // 
            this.btn조회.BackColor = System.Drawing.Color.White;
            this.btn조회.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn조회.Location = new System.Drawing.Point(644, 3);
            this.btn조회.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn조회.Name = "btn조회";
            this.btn조회.Size = new System.Drawing.Size(60, 19);
            this.btn조회.TabIndex = 117;
            this.btn조회.TabStop = false;
            this.btn조회.Text = "조회";
            this.btn조회.UseVisualStyleBackColor = false;
            // 
            // btn복사
            // 
            this.btn복사.BackColor = System.Drawing.Color.White;
            this.btn복사.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn복사.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn복사.Location = new System.Drawing.Point(710, 3);
            this.btn복사.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn복사.Name = "btn복사";
            this.btn복사.Size = new System.Drawing.Size(60, 19);
            this.btn복사.TabIndex = 116;
            this.btn복사.TabStop = false;
            this.btn복사.Text = "복사";
            this.btn복사.UseVisualStyleBackColor = false;
            // 
            // btn적용
            // 
            this.btn적용.BackColor = System.Drawing.Color.White;
            this.btn적용.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn적용.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn적용.Location = new System.Drawing.Point(776, 3);
            this.btn적용.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn적용.Name = "btn적용";
            this.btn적용.Size = new System.Drawing.Size(60, 19);
            this.btn적용.TabIndex = 115;
            this.btn적용.TabStop = false;
            this.btn적용.Text = "적용";
            this.btn적용.UseVisualStyleBackColor = false;
            // 
            // btn취소
            // 
            this.btn취소.BackColor = System.Drawing.Color.White;
            this.btn취소.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn취소.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn취소.Location = new System.Drawing.Point(842, 3);
            this.btn취소.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn취소.Name = "btn취소";
            this.btn취소.Size = new System.Drawing.Size(60, 19);
            this.btn취소.TabIndex = 114;
            this.btn취소.TabStop = false;
            this.btn취소.Text = "취소";
            this.btn취소.UseVisualStyleBackColor = false;
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
            this._flexH.Font = new System.Drawing.Font("굴림", 9F);
            this._flexH.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexH.Location = new System.Drawing.Point(0, 0);
            this._flexH.Name = "_flexH";
            this._flexH.Rows.Count = 1;
            this._flexH.Rows.DefaultSize = 18;
            this._flexH.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexH.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexH.ShowSort = false;
            this._flexH.Size = new System.Drawing.Size(905, 182);
            this._flexH.StyleInfo = resources.GetString("_flexH.StyleInfo");
            this._flexH.TabIndex = 3;
            this._flexH.UseGridCalculator = true;
            // 
            // _flexD
            // 
            this._flexD.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexD.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexD.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexD.AutoResize = false;
            this._flexD.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexD.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexD.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexD.EnabledHeaderCheck = true;
            this._flexD.Font = new System.Drawing.Font("굴림", 9F);
            this._flexD.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexD.Location = new System.Drawing.Point(0, 0);
            this._flexD.Name = "_flexD";
            this._flexD.Rows.Count = 1;
            this._flexD.Rows.DefaultSize = 18;
            this._flexD.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexD.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexD.ShowSort = false;
            this._flexD.Size = new System.Drawing.Size(905, 196);
            this._flexD.StyleInfo = resources.GetString("_flexD.StyleInfo");
            this._flexD.TabIndex = 3;
            this._flexD.UseGridCalculator = true;
            // 
            // dtp지급일자
            // 
            this.dtp지급일자.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp지급일자.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtp지급일자.IsNecessaryCondition = true;
            this.dtp지급일자.Location = new System.Drawing.Point(107, 0);
            this.dtp지급일자.MaximumSize = new System.Drawing.Size(185, 21);
            this.dtp지급일자.MinimumSize = new System.Drawing.Size(185, 21);
            this.dtp지급일자.Name = "dtp지급일자";
            this.dtp지급일자.Size = new System.Drawing.Size(185, 21);
            this.dtp지급일자.TabIndex = 226;
            // 
            // txt지급번호
            // 
            this.txt지급번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt지급번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt지급번호.Dock = System.Windows.Forms.DockStyle.Right;
            this.txt지급번호.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt지급번호.Location = new System.Drawing.Point(106, 0);
            this.txt지급번호.Name = "txt지급번호";
            this.txt지급번호.Size = new System.Drawing.Size(186, 21);
            this.txt지급번호.TabIndex = 9;
            this.txt지급번호.UseKeyEnter = false;
            this.txt지급번호.UseKeyF3 = false;
            // 
            // ctx발주유형
            // 
            this.ctx발주유형.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.ctx발주유형.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx발주유형.HelpID = Duzon.Common.Forms.Help.HelpID.P_PU_TPPO_SUB;
            this.ctx발주유형.ItemBackColor = System.Drawing.Color.Empty;
            this.ctx발주유형.Location = new System.Drawing.Point(106, 0);
            this.ctx발주유형.Name = "ctx발주유형";
            this.ctx발주유형.Size = new System.Drawing.Size(186, 21);
            this.ctx발주유형.TabIndex = 7;
            this.ctx발주유형.TabStop = false;
            this.ctx발주유형.Tag = "CD_TPPO;NM_TPPO";
            // 
            // lbl담당자
            // 
            this.lbl담당자.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl담당자.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl담당자.Location = new System.Drawing.Point(0, 0);
            this.lbl담당자.Name = "lbl담당자";
            this.lbl담당자.Size = new System.Drawing.Size(100, 23);
            this.lbl담당자.TabIndex = 9;
            this.lbl담당자.Tag = "";
            this.lbl담당자.Text = "담당자";
            this.lbl담당자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ctx담당자
            // 
            this.ctx담당자.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx담당자.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
            this.ctx담당자.Location = new System.Drawing.Point(106, 0);
            this.ctx담당자.Name = "ctx담당자";
            this.ctx담당자.Size = new System.Drawing.Size(186, 21);
            this.ctx담당자.TabIndex = 8;
            this.ctx담당자.TabStop = false;
            this.ctx담당자.Tag = "NO_EMP;NM_KOR";
            // 
            // ctx매입처
            // 
            this.ctx매입처.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx매입처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.ctx매입처.ItemBackColor = System.Drawing.Color.Empty;
            this.ctx매입처.Location = new System.Drawing.Point(106, 0);
            this.ctx매입처.Name = "ctx매입처";
            this.ctx매입처.Size = new System.Drawing.Size(186, 21);
            this.ctx매입처.TabIndex = 3;
            this.ctx매입처.TabStop = false;
            // 
            // ctx구매그룹
            // 
            this.ctx구매그룹.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx구매그룹.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PURGRP_SUB;
            this.ctx구매그룹.ItemBackColor = System.Drawing.Color.Empty;
            this.ctx구매그룹.Location = new System.Drawing.Point(106, 0);
            this.ctx구매그룹.Name = "ctx구매그룹";
            this.ctx구매그룹.Size = new System.Drawing.Size(186, 21);
            this.ctx구매그룹.TabIndex = 6;
            this.ctx구매그룹.TabStop = false;
            // 
            // cbo공장
            // 
            this.cbo공장.AutoDropDown = true;
            this.cbo공장.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbo공장.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo공장.ItemHeight = 12;
            this.cbo공장.Location = new System.Drawing.Point(106, 0);
            this.cbo공장.Name = "cbo공장";
            this.cbo공장.Size = new System.Drawing.Size(186, 20);
            this.cbo공장.TabIndex = 0;
            this.cbo공장.UseKeyEnter = false;
            this.cbo공장.UseKeyF3 = false;
            // 
            // lbl지급일자
            // 
            this.lbl지급일자.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl지급일자.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl지급일자.Location = new System.Drawing.Point(0, 0);
            this.lbl지급일자.Name = "lbl지급일자";
            this.lbl지급일자.Size = new System.Drawing.Size(100, 23);
            this.lbl지급일자.TabIndex = 9;
            this.lbl지급일자.Tag = "";
            this.lbl지급일자.Text = "지급일자";
            this.lbl지급일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl발주유형
            // 
            this.lbl발주유형.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl발주유형.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl발주유형.Location = new System.Drawing.Point(0, 0);
            this.lbl발주유형.Name = "lbl발주유형";
            this.lbl발주유형.Size = new System.Drawing.Size(100, 23);
            this.lbl발주유형.TabIndex = 8;
            this.lbl발주유형.Tag = "";
            this.lbl발주유형.Text = "발주유형";
            this.lbl발주유형.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl프로젝트
            // 
            this.lbl프로젝트.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl프로젝트.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl프로젝트.Location = new System.Drawing.Point(0, 0);
            this.lbl프로젝트.Name = "lbl프로젝트";
            this.lbl프로젝트.Size = new System.Drawing.Size(100, 23);
            this.lbl프로젝트.TabIndex = 7;
            this.lbl프로젝트.Tag = "";
            this.lbl프로젝트.Text = "프로젝트";
            this.lbl프로젝트.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl지급번호
            // 
            this.lbl지급번호.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl지급번호.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl지급번호.Location = new System.Drawing.Point(0, 0);
            this.lbl지급번호.Name = "lbl지급번호";
            this.lbl지급번호.Size = new System.Drawing.Size(100, 22);
            this.lbl지급번호.TabIndex = 8;
            this.lbl지급번호.Tag = "";
            this.lbl지급번호.Text = "지급번호";
            this.lbl지급번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl구매그룹
            // 
            this.lbl구매그룹.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl구매그룹.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl구매그룹.Location = new System.Drawing.Point(0, 0);
            this.lbl구매그룹.Name = "lbl구매그룹";
            this.lbl구매그룹.Size = new System.Drawing.Size(100, 23);
            this.lbl구매그룹.TabIndex = 6;
            this.lbl구매그룹.Tag = "";
            this.lbl구매그룹.Text = "구매그룹";
            this.lbl구매그룹.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl공장
            // 
            this.lbl공장.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl공장.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl공장.Location = new System.Drawing.Point(0, 0);
            this.lbl공장.Name = "lbl공장";
            this.lbl공장.Size = new System.Drawing.Size(100, 23);
            this.lbl공장.TabIndex = 7;
            this.lbl공장.Tag = "";
            this.lbl공장.Text = "공장";
            this.lbl공장.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl매입처
            // 
            this.lbl매입처.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl매입처.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl매입처.Location = new System.Drawing.Point(0, 0);
            this.lbl매입처.Name = "lbl매입처";
            this.lbl매입처.Size = new System.Drawing.Size(100, 23);
            this.lbl매입처.TabIndex = 1;
            this.lbl매입처.Text = "매입처";
            this.lbl매입처.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 49);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 93F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(911, 513);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 128);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._flexH);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._flexD);
            this.splitContainer1.Size = new System.Drawing.Size(905, 382);
            this.splitContainer1.SplitterDistance = 182;
            this.splitContainer1.TabIndex = 112;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btn취소);
            this.flowLayoutPanel1.Controls.Add(this.btn적용);
            this.flowLayoutPanel1.Controls.Add(this.btn복사);
            this.flowLayoutPanel1.Controls.Add(this.btn조회);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 96);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(905, 26);
            this.flowLayoutPanel1.TabIndex = 113;
            // 
            // oneGrid1
            // 
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2,
            this.oneGridItem3});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(905, 87);
            this.oneGrid1.TabIndex = 114;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bpPanelControl4);
            this.oneGridItem1.Controls.Add(this.bpPanelControl5);
            this.oneGridItem1.Controls.Add(this.bpPanelControl1);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(895, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl4
            // 
            this.bpPanelControl4.Controls.Add(this.lbl지급번호);
            this.bpPanelControl4.Controls.Add(this.txt지급번호);
            this.bpPanelControl4.Location = new System.Drawing.Point(590, 1);
            this.bpPanelControl4.Name = "bpPanelControl4";
            this.bpPanelControl4.Size = new System.Drawing.Size(292, 22);
            this.bpPanelControl4.TabIndex = 0;
            this.bpPanelControl4.Text = "bpPanelControl4";
            // 
            // bpPanelControl5
            // 
            this.bpPanelControl5.Controls.Add(this.dtp지급일자);
            this.bpPanelControl5.Controls.Add(this.lbl지급일자);
            this.bpPanelControl5.Location = new System.Drawing.Point(296, 1);
            this.bpPanelControl5.Name = "bpPanelControl5";
            this.bpPanelControl5.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl5.TabIndex = 1;
            this.bpPanelControl5.Text = "bpPanelControl5";
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.lbl공장);
            this.bpPanelControl1.Controls.Add(this.cbo공장);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // oneGridItem2
            // 
            this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem2.Controls.Add(this.bpPanelControl8);
            this.oneGridItem2.Controls.Add(this.bpPanelControl6);
            this.oneGridItem2.Controls.Add(this.bpPanelControl2);
            this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem2.Name = "oneGridItem2";
            this.oneGridItem2.Size = new System.Drawing.Size(895, 23);
            this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 1;
            // 
            // bpPanelControl8
            // 
            this.bpPanelControl8.Controls.Add(this.lbl담당자);
            this.bpPanelControl8.Controls.Add(this.ctx담당자);
            this.bpPanelControl8.Location = new System.Drawing.Point(590, 1);
            this.bpPanelControl8.Name = "bpPanelControl8";
            this.bpPanelControl8.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl8.TabIndex = 1;
            this.bpPanelControl8.Text = "bpPanelControl8";
            // 
            // bpPanelControl6
            // 
            this.bpPanelControl6.Controls.Add(this.txt프로젝트);
            this.bpPanelControl6.Controls.Add(this.lbl프로젝트);
            this.bpPanelControl6.Location = new System.Drawing.Point(296, 1);
            this.bpPanelControl6.Name = "bpPanelControl6";
            this.bpPanelControl6.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl6.TabIndex = 1;
            this.bpPanelControl6.Text = "bpPanelControl6";
            // 
            // txt프로젝트
            // 
            this.txt프로젝트.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt프로젝트.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt프로젝트.Dock = System.Windows.Forms.DockStyle.Right;
            this.txt프로젝트.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt프로젝트.Location = new System.Drawing.Point(106, 0);
            this.txt프로젝트.Name = "txt프로젝트";
            this.txt프로젝트.Size = new System.Drawing.Size(186, 21);
            this.txt프로젝트.TabIndex = 10;
            this.txt프로젝트.UseKeyEnter = false;
            this.txt프로젝트.UseKeyF3 = false;
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.lbl매입처);
            this.bpPanelControl2.Controls.Add(this.ctx매입처);
            this.bpPanelControl2.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl2.TabIndex = 0;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // oneGridItem3
            // 
            this.oneGridItem3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem3.Controls.Add(this.bpPanelControl7);
            this.oneGridItem3.Controls.Add(this.bpPanelControl3);
            this.oneGridItem3.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem3.Location = new System.Drawing.Point(0, 46);
            this.oneGridItem3.Name = "oneGridItem3";
            this.oneGridItem3.Size = new System.Drawing.Size(895, 23);
            this.oneGridItem3.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem3.TabIndex = 2;
            // 
            // bpPanelControl7
            // 
            this.bpPanelControl7.Controls.Add(this.lbl발주유형);
            this.bpPanelControl7.Controls.Add(this.ctx발주유형);
            this.bpPanelControl7.Location = new System.Drawing.Point(296, 1);
            this.bpPanelControl7.Name = "bpPanelControl7";
            this.bpPanelControl7.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl7.TabIndex = 1;
            this.bpPanelControl7.Text = "bpPanelControl7";
            // 
            // bpPanelControl3
            // 
            this.bpPanelControl3.Controls.Add(this.lbl구매그룹);
            this.bpPanelControl3.Controls.Add(this.ctx구매그룹);
            this.bpPanelControl3.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl3.TabIndex = 0;
            this.bpPanelControl3.Text = "bpPanelControl3";
            // 
            // P_CZ_PU_PO_SUB2
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.btn취소;
            this.CaptionPaint = true;
            this.ClientSize = new System.Drawing.Size(914, 565);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "P_CZ_PU_PO_SUB2";
            this.Tag = "";
            this.Text = "ERP iU";
            this.TitleText = "지급등록 조회";
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexD)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl4.ResumeLayout(false);
            this.bpPanelControl4.PerformLayout();
            this.bpPanelControl5.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            this.oneGridItem2.ResumeLayout(false);
            this.bpPanelControl8.ResumeLayout(false);
            this.bpPanelControl6.ResumeLayout(false);
            this.bpPanelControl6.PerformLayout();
            this.bpPanelControl2.ResumeLayout(false);
            this.oneGridItem3.ResumeLayout(false);
            this.bpPanelControl7.ResumeLayout(false);
            this.bpPanelControl3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private RoundedButton btn조회;
        private RoundedButton btn복사;
        private RoundedButton btn적용;
        private RoundedButton btn취소;
        private Dass.FlexGrid.FlexGrid _flexH;
        private Dass.FlexGrid.FlexGrid _flexD;
        private BpCodeTextBox ctx매입처;
        private BpCodeTextBox ctx구매그룹;
        private DropDownComboBox cbo공장;
        private LabelExt lbl프로젝트;
        private LabelExt lbl구매그룹;
        private LabelExt lbl공장;
        private LabelExt lbl매입처;
        private TableLayoutPanel tableLayoutPanel1;
        private SplitContainer splitContainer1;
        private BpCodeTextBox ctx담당자;
        private LabelExt lbl발주유형;
        private LabelExt lbl담당자;
        private LabelExt lbl지급일자;
        private BpCodeTextBox ctx발주유형;
        private LabelExt lbl지급번호;
        private TextBoxExt txt지급번호;
        private PeriodPicker dtp지급일자;
        private FlowLayoutPanel flowLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private BpPanelControl bpPanelControl5;
        private BpPanelControl bpPanelControl1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private BpPanelControl bpPanelControl6;
        private BpPanelControl bpPanelControl2;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem3;
        private BpPanelControl bpPanelControl7;
        private BpPanelControl bpPanelControl3;
        private BpPanelControl bpPanelControl8;
        private BpPanelControl bpPanelControl4;
        private TextBoxExt txt프로젝트;
    }
}
using System.ComponentModel;
using System.Windows.Forms;
using Duzon.Erpiu.Windows.OneControls;
using Duzon.Common.BpControls;
using Dass.FlexGrid;
using Duzon.Common.Controls;
using System.Drawing;
using C1.Win.C1FlexGrid;

namespace cz
{
    partial class P_CZ_SA_DEFERRED_DELIVERY_MNG
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_SA_DEFERRED_DELIVERY_MNG));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl8 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt매입처발주번호 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl매입처발주번호 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt수주번호 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl수주번호 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.dtp수주일자 = new Duzon.Common.Controls.PeriodPicker();
			this.lbl수주일자 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem3 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl7 = new Duzon.Common.BpControls.BpPanelControl();
			this.bpc매입처 = new Duzon.Common.BpControls.BpComboBox();
			this.lbl매입처 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl6 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx매출처 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl매출처 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx호선 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl호선 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem4 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl15 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbx물류담당자 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl물류담당자 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl10 = new Duzon.Common.BpControls.BpPanelControl();
			this.bpc매출처그룹 = new Duzon.Common.BpControls.BpComboBox();
			this.lbl매출처그룹 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl9 = new Duzon.Common.BpControls.BpPanelControl();
			this.bpc영업그룹 = new Duzon.Common.BpControls.BpComboBox();
			this.lbl영업그룹 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.rdo출고전체 = new Duzon.Common.Controls.RadioButtonExt();
			this.rdo미출고 = new Duzon.Common.Controls.RadioButtonExt();
			this.rdo출고 = new Duzon.Common.Controls.RadioButtonExt();
			this.lbl출고상태 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.rdo입고전체 = new Duzon.Common.Controls.RadioButtonExt();
			this.rdo미입고 = new Duzon.Common.Controls.RadioButtonExt();
			this.rdo입고 = new Duzon.Common.Controls.RadioButtonExt();
			this.lbl입고상태 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl11 = new Duzon.Common.BpControls.BpPanelControl();
			this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
			this.rdo의뢰전체 = new Duzon.Common.Controls.RadioButtonExt();
			this.rdo미의뢰 = new Duzon.Common.Controls.RadioButtonExt();
			this.rdo의뢰 = new Duzon.Common.Controls.RadioButtonExt();
			this.lbl의뢰상태 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem5 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl13 = new Duzon.Common.BpControls.BpPanelControl();
			this.labelExt1 = new Duzon.Common.Controls.LabelExt();
			this.cur지연일수 = new Duzon.Common.Controls.CurrencyTextBox();
			this.lbl지연일수 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl14 = new Duzon.Common.BpControls.BpPanelControl();
			this.chk셀병합 = new Duzon.Common.Controls.CheckBoxExt();
			this.lbl셀병합여부 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl12 = new Duzon.Common.BpControls.BpPanelControl();
			this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
			this.rdo마감전체 = new Duzon.Common.Controls.RadioButtonExt();
			this.rdo미마감 = new Duzon.Common.Controls.RadioButtonExt();
			this.rdo마감 = new Duzon.Common.Controls.RadioButtonExt();
			this.lbl마감상태 = new Duzon.Common.Controls.LabelExt();
			this.tabControl = new Duzon.Common.Controls.TabControlExt();
			this.tpg수주기준파일 = new System.Windows.Forms.TabPage();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this._flex수주기준파일H = new Dass.FlexGrid.FlexGrid(this.components);
			this._flex수주기준파일L = new Dass.FlexGrid.FlexGrid(this.components);
			this.tpg수주기준품목 = new System.Windows.Forms.TabPage();
			this._flex수주기준품목H = new Dass.FlexGrid.FlexGrid(this.components);
			this.tpg발주기준파일 = new System.Windows.Forms.TabPage();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this._flex발주기준파일H = new Dass.FlexGrid.FlexGrid(this.components);
			this._flex발주기준파일L = new Dass.FlexGrid.FlexGrid(this.components);
			this.tpg발주기준품목 = new System.Windows.Forms.TabPage();
			this._flex발주기준품목H = new Dass.FlexGrid.FlexGrid(this.components);
			this.btn메일발송 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn발송옵션 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn재고납기업로드 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.mDataArea.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl8.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.oneGridItem3.SuspendLayout();
			this.bpPanelControl7.SuspendLayout();
			this.bpPanelControl6.SuspendLayout();
			this.bpPanelControl5.SuspendLayout();
			this.oneGridItem4.SuspendLayout();
			this.bpPanelControl15.SuspendLayout();
			this.bpPanelControl10.SuspendLayout();
			this.bpPanelControl9.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.bpPanelControl4.SuspendLayout();
			this.flowLayoutPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.rdo출고전체)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo미출고)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo출고)).BeginInit();
			this.bpPanelControl3.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.rdo입고전체)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo미입고)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo입고)).BeginInit();
			this.bpPanelControl11.SuspendLayout();
			this.flowLayoutPanel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.rdo의뢰전체)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo미의뢰)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo의뢰)).BeginInit();
			this.oneGridItem5.SuspendLayout();
			this.bpPanelControl13.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur지연일수)).BeginInit();
			this.bpPanelControl14.SuspendLayout();
			this.bpPanelControl12.SuspendLayout();
			this.flowLayoutPanel4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.rdo마감전체)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo미마감)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo마감)).BeginInit();
			this.tabControl.SuspendLayout();
			this.tpg수주기준파일.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex수주기준파일H)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._flex수주기준파일L)).BeginInit();
			this.tpg수주기준품목.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex수주기준품목H)).BeginInit();
			this.tpg발주기준파일.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex발주기준파일H)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._flex발주기준파일L)).BeginInit();
			this.tpg발주기준품목.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex발주기준품목H)).BeginInit();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.tableLayoutPanel1);
			this.mDataArea.Size = new System.Drawing.Size(1091, 579);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.tabControl, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 140F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 108F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1091, 579);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem3,
            this.oneGridItem4,
            this.oneGridItem2,
            this.oneGridItem5});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(1085, 134);
			this.oneGrid1.TabIndex = 0;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl8);
			this.oneGridItem1.Controls.Add(this.bpPanelControl2);
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(1075, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl8
			// 
			this.bpPanelControl8.Controls.Add(this.txt매입처발주번호);
			this.bpPanelControl8.Controls.Add(this.lbl매입처발주번호);
			this.bpPanelControl8.Location = new System.Drawing.Point(590, 1);
			this.bpPanelControl8.Name = "bpPanelControl8";
			this.bpPanelControl8.Size = new System.Drawing.Size(293, 23);
			this.bpPanelControl8.TabIndex = 2;
			this.bpPanelControl8.Text = "bpPanelControl8";
			// 
			// txt매입처발주번호
			// 
			this.txt매입처발주번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt매입처발주번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt매입처발주번호.Dock = System.Windows.Forms.DockStyle.Right;
			this.txt매입처발주번호.Location = new System.Drawing.Point(106, 0);
			this.txt매입처발주번호.Name = "txt매입처발주번호";
			this.txt매입처발주번호.Size = new System.Drawing.Size(187, 21);
			this.txt매입처발주번호.TabIndex = 1;
			// 
			// lbl매입처발주번호
			// 
			this.lbl매입처발주번호.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl매입처발주번호.Location = new System.Drawing.Point(0, 0);
			this.lbl매입처발주번호.Name = "lbl매입처발주번호";
			this.lbl매입처발주번호.Size = new System.Drawing.Size(100, 23);
			this.lbl매입처발주번호.TabIndex = 0;
			this.lbl매입처발주번호.Text = "매입처발주번호";
			this.lbl매입처발주번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.txt수주번호);
			this.bpPanelControl2.Controls.Add(this.lbl수주번호);
			this.bpPanelControl2.Location = new System.Drawing.Point(295, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(293, 23);
			this.bpPanelControl2.TabIndex = 1;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// txt수주번호
			// 
			this.txt수주번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt수주번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt수주번호.Dock = System.Windows.Forms.DockStyle.Right;
			this.txt수주번호.Location = new System.Drawing.Point(106, 0);
			this.txt수주번호.Name = "txt수주번호";
			this.txt수주번호.Size = new System.Drawing.Size(187, 21);
			this.txt수주번호.TabIndex = 1;
			// 
			// lbl수주번호
			// 
			this.lbl수주번호.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl수주번호.Location = new System.Drawing.Point(0, 0);
			this.lbl수주번호.Name = "lbl수주번호";
			this.lbl수주번호.Size = new System.Drawing.Size(100, 23);
			this.lbl수주번호.TabIndex = 0;
			this.lbl수주번호.Text = "수주번호";
			this.lbl수주번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.dtp수주일자);
			this.bpPanelControl1.Controls.Add(this.lbl수주일자);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(291, 23);
			this.bpPanelControl1.TabIndex = 0;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// dtp수주일자
			// 
			this.dtp수주일자.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp수주일자.Dock = System.Windows.Forms.DockStyle.Right;
			this.dtp수주일자.IsNecessaryCondition = true;
			this.dtp수주일자.Location = new System.Drawing.Point(106, 0);
			this.dtp수주일자.MaximumSize = new System.Drawing.Size(185, 21);
			this.dtp수주일자.MinimumSize = new System.Drawing.Size(185, 21);
			this.dtp수주일자.Name = "dtp수주일자";
			this.dtp수주일자.Size = new System.Drawing.Size(185, 21);
			this.dtp수주일자.TabIndex = 1;
			// 
			// lbl수주일자
			// 
			this.lbl수주일자.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl수주일자.Location = new System.Drawing.Point(0, 0);
			this.lbl수주일자.Name = "lbl수주일자";
			this.lbl수주일자.Size = new System.Drawing.Size(100, 23);
			this.lbl수주일자.TabIndex = 0;
			this.lbl수주일자.Text = "수주일자";
			this.lbl수주일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem3
			// 
			this.oneGridItem3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem3.Controls.Add(this.bpPanelControl7);
			this.oneGridItem3.Controls.Add(this.bpPanelControl6);
			this.oneGridItem3.Controls.Add(this.bpPanelControl5);
			this.oneGridItem3.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem3.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem3.Name = "oneGridItem3";
			this.oneGridItem3.Size = new System.Drawing.Size(1075, 23);
			this.oneGridItem3.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem3.TabIndex = 1;
			// 
			// bpPanelControl7
			// 
			this.bpPanelControl7.Controls.Add(this.bpc매입처);
			this.bpPanelControl7.Controls.Add(this.lbl매입처);
			this.bpPanelControl7.Location = new System.Drawing.Point(590, 1);
			this.bpPanelControl7.Name = "bpPanelControl7";
			this.bpPanelControl7.Size = new System.Drawing.Size(293, 23);
			this.bpPanelControl7.TabIndex = 4;
			this.bpPanelControl7.Text = "bpPanelControl7";
			// 
			// bpc매입처
			// 
			this.bpc매입처.Dock = System.Windows.Forms.DockStyle.Right;
			this.bpc매입처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB1;
			this.bpc매입처.Location = new System.Drawing.Point(106, 0);
			this.bpc매입처.Name = "bpc매입처";
			this.bpc매입처.Size = new System.Drawing.Size(187, 21);
			this.bpc매입처.TabIndex = 1;
			this.bpc매입처.TabStop = false;
			this.bpc매입처.Text = "bpComboBox1";
			// 
			// lbl매입처
			// 
			this.lbl매입처.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl매입처.Location = new System.Drawing.Point(0, 0);
			this.lbl매입처.Name = "lbl매입처";
			this.lbl매입처.Size = new System.Drawing.Size(100, 23);
			this.lbl매입처.TabIndex = 0;
			this.lbl매입처.Text = "매입처";
			this.lbl매입처.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl6
			// 
			this.bpPanelControl6.Controls.Add(this.ctx매출처);
			this.bpPanelControl6.Controls.Add(this.lbl매출처);
			this.bpPanelControl6.Location = new System.Drawing.Point(295, 1);
			this.bpPanelControl6.Name = "bpPanelControl6";
			this.bpPanelControl6.Size = new System.Drawing.Size(293, 23);
			this.bpPanelControl6.TabIndex = 3;
			this.bpPanelControl6.Text = "bpPanelControl6";
			// 
			// ctx매출처
			// 
			this.ctx매출처.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx매출처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
			this.ctx매출처.Location = new System.Drawing.Point(106, 0);
			this.ctx매출처.Name = "ctx매출처";
			this.ctx매출처.Size = new System.Drawing.Size(187, 21);
			this.ctx매출처.TabIndex = 1;
			this.ctx매출처.TabStop = false;
			this.ctx매출처.Text = "bpCodeTextBox1";
			// 
			// lbl매출처
			// 
			this.lbl매출처.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl매출처.Location = new System.Drawing.Point(0, 0);
			this.lbl매출처.Name = "lbl매출처";
			this.lbl매출처.Size = new System.Drawing.Size(100, 23);
			this.lbl매출처.TabIndex = 0;
			this.lbl매출처.Text = "매출처";
			this.lbl매출처.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl5
			// 
			this.bpPanelControl5.Controls.Add(this.ctx호선);
			this.bpPanelControl5.Controls.Add(this.lbl호선);
			this.bpPanelControl5.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl5.Name = "bpPanelControl5";
			this.bpPanelControl5.Size = new System.Drawing.Size(291, 23);
			this.bpPanelControl5.TabIndex = 2;
			this.bpPanelControl5.Text = "bpPanelControl5";
			// 
			// ctx호선
			// 
			this.ctx호선.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx호선.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
			this.ctx호선.Location = new System.Drawing.Point(106, 0);
			this.ctx호선.Name = "ctx호선";
			this.ctx호선.Size = new System.Drawing.Size(185, 21);
			this.ctx호선.TabIndex = 1;
			this.ctx호선.TabStop = false;
			this.ctx호선.Text = "bpCodeTextBox1";
			this.ctx호선.UserCodeName = "NO_HULL";
			this.ctx호선.UserCodeValue = "NO_IMO";
			this.ctx호선.UserHelpID = "H_CZ_MA_HULL_SUB";
			this.ctx호선.UserParams = "호선;H_CZ_MA_HULL_SUB";
			// 
			// lbl호선
			// 
			this.lbl호선.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl호선.Location = new System.Drawing.Point(0, 0);
			this.lbl호선.Name = "lbl호선";
			this.lbl호선.Size = new System.Drawing.Size(100, 23);
			this.lbl호선.TabIndex = 0;
			this.lbl호선.Text = "호선";
			this.lbl호선.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem4
			// 
			this.oneGridItem4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem4.Controls.Add(this.bpPanelControl15);
			this.oneGridItem4.Controls.Add(this.bpPanelControl10);
			this.oneGridItem4.Controls.Add(this.bpPanelControl9);
			this.oneGridItem4.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem4.Location = new System.Drawing.Point(0, 46);
			this.oneGridItem4.Name = "oneGridItem4";
			this.oneGridItem4.Size = new System.Drawing.Size(1075, 23);
			this.oneGridItem4.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem4.TabIndex = 2;
			// 
			// bpPanelControl15
			// 
			this.bpPanelControl15.Controls.Add(this.cbx물류담당자);
			this.bpPanelControl15.Controls.Add(this.lbl물류담당자);
			this.bpPanelControl15.Location = new System.Drawing.Point(590, 1);
			this.bpPanelControl15.Name = "bpPanelControl15";
			this.bpPanelControl15.Size = new System.Drawing.Size(293, 23);
			this.bpPanelControl15.TabIndex = 5;
			this.bpPanelControl15.Text = "bpPanelControl15";
			// 
			// cbx물류담당자
			// 
			this.cbx물류담당자.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbx물류담당자.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
			this.cbx물류담당자.Location = new System.Drawing.Point(106, 0);
			this.cbx물류담당자.Name = "cbx물류담당자";
			this.cbx물류담당자.Size = new System.Drawing.Size(187, 21);
			this.cbx물류담당자.TabIndex = 1;
			this.cbx물류담당자.TabStop = false;
			this.cbx물류담당자.Text = "bpCodeTextBox1";
			// 
			// lbl물류담당자
			// 
			this.lbl물류담당자.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl물류담당자.Location = new System.Drawing.Point(0, 0);
			this.lbl물류담당자.Name = "lbl물류담당자";
			this.lbl물류담당자.Size = new System.Drawing.Size(100, 23);
			this.lbl물류담당자.TabIndex = 0;
			this.lbl물류담당자.Text = "물류담당자";
			this.lbl물류담당자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl10
			// 
			this.bpPanelControl10.Controls.Add(this.bpc매출처그룹);
			this.bpPanelControl10.Controls.Add(this.lbl매출처그룹);
			this.bpPanelControl10.Location = new System.Drawing.Point(295, 1);
			this.bpPanelControl10.Name = "bpPanelControl10";
			this.bpPanelControl10.Size = new System.Drawing.Size(293, 23);
			this.bpPanelControl10.TabIndex = 4;
			this.bpPanelControl10.Text = "bpPanelControl10";
			// 
			// bpc매출처그룹
			// 
			this.bpc매출처그룹.Dock = System.Windows.Forms.DockStyle.Right;
			this.bpc매출처그룹.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB1;
			this.bpc매출처그룹.Location = new System.Drawing.Point(106, 0);
			this.bpc매출처그룹.Name = "bpc매출처그룹";
			this.bpc매출처그룹.Size = new System.Drawing.Size(187, 21);
			this.bpc매출처그룹.TabIndex = 16;
			this.bpc매출처그룹.TabStop = false;
			this.bpc매출처그룹.Text = "bpComboBox1";
			// 
			// lbl매출처그룹
			// 
			this.lbl매출처그룹.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl매출처그룹.Location = new System.Drawing.Point(0, 0);
			this.lbl매출처그룹.Name = "lbl매출처그룹";
			this.lbl매출처그룹.Size = new System.Drawing.Size(100, 23);
			this.lbl매출처그룹.TabIndex = 0;
			this.lbl매출처그룹.Text = "매출처그룹";
			this.lbl매출처그룹.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl9
			// 
			this.bpPanelControl9.Controls.Add(this.bpc영업그룹);
			this.bpPanelControl9.Controls.Add(this.lbl영업그룹);
			this.bpPanelControl9.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl9.Name = "bpPanelControl9";
			this.bpPanelControl9.Size = new System.Drawing.Size(291, 23);
			this.bpPanelControl9.TabIndex = 3;
			this.bpPanelControl9.Text = "bpPanelControl9";
			// 
			// bpc영업그룹
			// 
			this.bpc영업그룹.Dock = System.Windows.Forms.DockStyle.Right;
			this.bpc영업그룹.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_SALEGRP_SUB1;
			this.bpc영업그룹.Location = new System.Drawing.Point(106, 0);
			this.bpc영업그룹.Name = "bpc영업그룹";
			this.bpc영업그룹.Size = new System.Drawing.Size(185, 21);
			this.bpc영업그룹.TabIndex = 1;
			this.bpc영업그룹.TabStop = false;
			this.bpc영업그룹.Text = "bpComboBox1";
			// 
			// lbl영업그룹
			// 
			this.lbl영업그룹.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl영업그룹.Location = new System.Drawing.Point(0, 0);
			this.lbl영업그룹.Name = "lbl영업그룹";
			this.lbl영업그룹.Size = new System.Drawing.Size(100, 23);
			this.lbl영업그룹.TabIndex = 0;
			this.lbl영업그룹.Text = "영업그룹";
			this.lbl영업그룹.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.bpPanelControl4);
			this.oneGridItem2.Controls.Add(this.bpPanelControl3);
			this.oneGridItem2.Controls.Add(this.bpPanelControl11);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 69);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(1075, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 3;
			// 
			// bpPanelControl4
			// 
			this.bpPanelControl4.Controls.Add(this.flowLayoutPanel2);
			this.bpPanelControl4.Controls.Add(this.lbl출고상태);
			this.bpPanelControl4.Location = new System.Drawing.Point(590, 1);
			this.bpPanelControl4.Name = "bpPanelControl4";
			this.bpPanelControl4.Size = new System.Drawing.Size(293, 23);
			this.bpPanelControl4.TabIndex = 1;
			this.bpPanelControl4.Text = "bpPanelControl4";
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.Controls.Add(this.rdo출고전체);
			this.flowLayoutPanel2.Controls.Add(this.rdo미출고);
			this.flowLayoutPanel2.Controls.Add(this.rdo출고);
			this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Right;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(106, 0);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(187, 23);
			this.flowLayoutPanel2.TabIndex = 1;
			// 
			// rdo출고전체
			// 
			this.rdo출고전체.AutoSize = true;
			this.rdo출고전체.Checked = true;
			this.rdo출고전체.Location = new System.Drawing.Point(3, 3);
			this.rdo출고전체.Name = "rdo출고전체";
			this.rdo출고전체.Size = new System.Drawing.Size(47, 16);
			this.rdo출고전체.TabIndex = 0;
			this.rdo출고전체.TabStop = true;
			this.rdo출고전체.Text = "전체";
			this.rdo출고전체.TextDD = null;
			this.rdo출고전체.UseKeyEnter = true;
			this.rdo출고전체.UseVisualStyleBackColor = true;
			// 
			// rdo미출고
			// 
			this.rdo미출고.AutoSize = true;
			this.rdo미출고.Location = new System.Drawing.Point(56, 3);
			this.rdo미출고.Name = "rdo미출고";
			this.rdo미출고.Size = new System.Drawing.Size(59, 16);
			this.rdo미출고.TabIndex = 1;
			this.rdo미출고.TabStop = true;
			this.rdo미출고.Text = "미출고";
			this.rdo미출고.TextDD = null;
			this.rdo미출고.UseKeyEnter = true;
			this.rdo미출고.UseVisualStyleBackColor = true;
			// 
			// rdo출고
			// 
			this.rdo출고.AutoSize = true;
			this.rdo출고.Location = new System.Drawing.Point(121, 3);
			this.rdo출고.Name = "rdo출고";
			this.rdo출고.Size = new System.Drawing.Size(47, 16);
			this.rdo출고.TabIndex = 2;
			this.rdo출고.TabStop = true;
			this.rdo출고.Text = "출고";
			this.rdo출고.TextDD = null;
			this.rdo출고.UseKeyEnter = true;
			this.rdo출고.UseVisualStyleBackColor = true;
			// 
			// lbl출고상태
			// 
			this.lbl출고상태.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl출고상태.Location = new System.Drawing.Point(0, 0);
			this.lbl출고상태.Name = "lbl출고상태";
			this.lbl출고상태.Size = new System.Drawing.Size(100, 23);
			this.lbl출고상태.TabIndex = 0;
			this.lbl출고상태.Text = "출고상태";
			this.lbl출고상태.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.flowLayoutPanel1);
			this.bpPanelControl3.Controls.Add(this.lbl입고상태);
			this.bpPanelControl3.Location = new System.Drawing.Point(297, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(291, 23);
			this.bpPanelControl3.TabIndex = 0;
			this.bpPanelControl3.Text = "bpPanelControl3";
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.rdo입고전체);
			this.flowLayoutPanel1.Controls.Add(this.rdo미입고);
			this.flowLayoutPanel1.Controls.Add(this.rdo입고);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(104, 0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(187, 23);
			this.flowLayoutPanel1.TabIndex = 1;
			// 
			// rdo입고전체
			// 
			this.rdo입고전체.AutoSize = true;
			this.rdo입고전체.Checked = true;
			this.rdo입고전체.Location = new System.Drawing.Point(3, 3);
			this.rdo입고전체.Name = "rdo입고전체";
			this.rdo입고전체.Size = new System.Drawing.Size(47, 16);
			this.rdo입고전체.TabIndex = 0;
			this.rdo입고전체.TabStop = true;
			this.rdo입고전체.Text = "전체";
			this.rdo입고전체.TextDD = null;
			this.rdo입고전체.UseKeyEnter = true;
			this.rdo입고전체.UseVisualStyleBackColor = true;
			// 
			// rdo미입고
			// 
			this.rdo미입고.AutoSize = true;
			this.rdo미입고.Location = new System.Drawing.Point(56, 3);
			this.rdo미입고.Name = "rdo미입고";
			this.rdo미입고.Size = new System.Drawing.Size(59, 16);
			this.rdo미입고.TabIndex = 1;
			this.rdo미입고.TabStop = true;
			this.rdo미입고.Text = "미입고";
			this.rdo미입고.TextDD = null;
			this.rdo미입고.UseKeyEnter = true;
			this.rdo미입고.UseVisualStyleBackColor = true;
			// 
			// rdo입고
			// 
			this.rdo입고.AutoSize = true;
			this.rdo입고.Location = new System.Drawing.Point(121, 3);
			this.rdo입고.Name = "rdo입고";
			this.rdo입고.Size = new System.Drawing.Size(47, 16);
			this.rdo입고.TabIndex = 2;
			this.rdo입고.TabStop = true;
			this.rdo입고.Text = "입고";
			this.rdo입고.TextDD = null;
			this.rdo입고.UseKeyEnter = true;
			this.rdo입고.UseVisualStyleBackColor = true;
			// 
			// lbl입고상태
			// 
			this.lbl입고상태.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl입고상태.Location = new System.Drawing.Point(0, 0);
			this.lbl입고상태.Name = "lbl입고상태";
			this.lbl입고상태.Size = new System.Drawing.Size(100, 23);
			this.lbl입고상태.TabIndex = 0;
			this.lbl입고상태.Text = "입고상태";
			this.lbl입고상태.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl11
			// 
			this.bpPanelControl11.Controls.Add(this.flowLayoutPanel3);
			this.bpPanelControl11.Controls.Add(this.lbl의뢰상태);
			this.bpPanelControl11.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl11.Name = "bpPanelControl11";
			this.bpPanelControl11.Size = new System.Drawing.Size(293, 23);
			this.bpPanelControl11.TabIndex = 2;
			this.bpPanelControl11.Text = "bpPanelControl11";
			// 
			// flowLayoutPanel3
			// 
			this.flowLayoutPanel3.Controls.Add(this.rdo의뢰전체);
			this.flowLayoutPanel3.Controls.Add(this.rdo미의뢰);
			this.flowLayoutPanel3.Controls.Add(this.rdo의뢰);
			this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Right;
			this.flowLayoutPanel3.Location = new System.Drawing.Point(106, 0);
			this.flowLayoutPanel3.Name = "flowLayoutPanel3";
			this.flowLayoutPanel3.Size = new System.Drawing.Size(187, 23);
			this.flowLayoutPanel3.TabIndex = 1;
			// 
			// rdo의뢰전체
			// 
			this.rdo의뢰전체.AutoSize = true;
			this.rdo의뢰전체.Checked = true;
			this.rdo의뢰전체.Location = new System.Drawing.Point(3, 3);
			this.rdo의뢰전체.Name = "rdo의뢰전체";
			this.rdo의뢰전체.Size = new System.Drawing.Size(47, 16);
			this.rdo의뢰전체.TabIndex = 0;
			this.rdo의뢰전체.TabStop = true;
			this.rdo의뢰전체.Text = "전체";
			this.rdo의뢰전체.TextDD = null;
			this.rdo의뢰전체.UseKeyEnter = true;
			this.rdo의뢰전체.UseVisualStyleBackColor = true;
			// 
			// rdo미의뢰
			// 
			this.rdo미의뢰.AutoSize = true;
			this.rdo미의뢰.Location = new System.Drawing.Point(56, 3);
			this.rdo미의뢰.Name = "rdo미의뢰";
			this.rdo미의뢰.Size = new System.Drawing.Size(59, 16);
			this.rdo미의뢰.TabIndex = 1;
			this.rdo미의뢰.TabStop = true;
			this.rdo미의뢰.Text = "미의뢰";
			this.rdo미의뢰.TextDD = null;
			this.rdo미의뢰.UseKeyEnter = true;
			this.rdo미의뢰.UseVisualStyleBackColor = true;
			// 
			// rdo의뢰
			// 
			this.rdo의뢰.AutoSize = true;
			this.rdo의뢰.Location = new System.Drawing.Point(121, 3);
			this.rdo의뢰.Name = "rdo의뢰";
			this.rdo의뢰.Size = new System.Drawing.Size(47, 16);
			this.rdo의뢰.TabIndex = 2;
			this.rdo의뢰.TabStop = true;
			this.rdo의뢰.Text = "의뢰";
			this.rdo의뢰.TextDD = null;
			this.rdo의뢰.UseKeyEnter = true;
			this.rdo의뢰.UseVisualStyleBackColor = true;
			// 
			// lbl의뢰상태
			// 
			this.lbl의뢰상태.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl의뢰상태.Location = new System.Drawing.Point(0, 0);
			this.lbl의뢰상태.Name = "lbl의뢰상태";
			this.lbl의뢰상태.Size = new System.Drawing.Size(100, 23);
			this.lbl의뢰상태.TabIndex = 0;
			this.lbl의뢰상태.Text = "의뢰상태";
			this.lbl의뢰상태.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem5
			// 
			this.oneGridItem5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem5.Controls.Add(this.bpPanelControl13);
			this.oneGridItem5.Controls.Add(this.bpPanelControl14);
			this.oneGridItem5.Controls.Add(this.bpPanelControl12);
			this.oneGridItem5.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem5.Location = new System.Drawing.Point(0, 92);
			this.oneGridItem5.Name = "oneGridItem5";
			this.oneGridItem5.Size = new System.Drawing.Size(1075, 23);
			this.oneGridItem5.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem5.TabIndex = 4;
			// 
			// bpPanelControl13
			// 
			this.bpPanelControl13.Controls.Add(this.labelExt1);
			this.bpPanelControl13.Controls.Add(this.cur지연일수);
			this.bpPanelControl13.Controls.Add(this.lbl지연일수);
			this.bpPanelControl13.Location = new System.Drawing.Point(590, 1);
			this.bpPanelControl13.Name = "bpPanelControl13";
			this.bpPanelControl13.Size = new System.Drawing.Size(293, 23);
			this.bpPanelControl13.TabIndex = 5;
			this.bpPanelControl13.Text = "bpPanelControl13";
			// 
			// labelExt1
			// 
			this.labelExt1.AutoSize = true;
			this.labelExt1.Location = new System.Drawing.Point(191, 5);
			this.labelExt1.Name = "labelExt1";
			this.labelExt1.Size = new System.Drawing.Size(85, 12);
			this.labelExt1.TabIndex = 2;
			this.labelExt1.Text = "일 이상 경과건";
			// 
			// cur지연일수
			// 
			this.cur지연일수.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur지연일수.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur지연일수.DecimalValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.cur지연일수.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur지연일수.Location = new System.Drawing.Point(106, 0);
			this.cur지연일수.Name = "cur지연일수";
			this.cur지연일수.NullString = "0";
			this.cur지연일수.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur지연일수.Size = new System.Drawing.Size(81, 21);
			this.cur지연일수.TabIndex = 1;
			this.cur지연일수.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// lbl지연일수
			// 
			this.lbl지연일수.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl지연일수.Location = new System.Drawing.Point(0, 0);
			this.lbl지연일수.Name = "lbl지연일수";
			this.lbl지연일수.Size = new System.Drawing.Size(100, 23);
			this.lbl지연일수.TabIndex = 0;
			this.lbl지연일수.Text = "지연일수";
			this.lbl지연일수.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl14
			// 
			this.bpPanelControl14.Controls.Add(this.chk셀병합);
			this.bpPanelControl14.Controls.Add(this.lbl셀병합여부);
			this.bpPanelControl14.Location = new System.Drawing.Point(297, 1);
			this.bpPanelControl14.Name = "bpPanelControl14";
			this.bpPanelControl14.Size = new System.Drawing.Size(291, 23);
			this.bpPanelControl14.TabIndex = 6;
			this.bpPanelControl14.Text = "bpPanelControl14";
			// 
			// chk셀병합
			// 
			this.chk셀병합.AutoSize = true;
			this.chk셀병합.Checked = true;
			this.chk셀병합.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk셀병합.Location = new System.Drawing.Point(104, 3);
			this.chk셀병합.Name = "chk셀병합";
			this.chk셀병합.Size = new System.Drawing.Size(60, 16);
			this.chk셀병합.TabIndex = 1;
			this.chk셀병합.Text = "셀병합";
			this.chk셀병합.TextDD = null;
			this.chk셀병합.UseVisualStyleBackColor = true;
			// 
			// lbl셀병합여부
			// 
			this.lbl셀병합여부.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl셀병합여부.Location = new System.Drawing.Point(0, 0);
			this.lbl셀병합여부.Name = "lbl셀병합여부";
			this.lbl셀병합여부.Size = new System.Drawing.Size(98, 23);
			this.lbl셀병합여부.TabIndex = 0;
			this.lbl셀병합여부.Text = "셀병합여부";
			this.lbl셀병합여부.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl12
			// 
			this.bpPanelControl12.Controls.Add(this.flowLayoutPanel4);
			this.bpPanelControl12.Controls.Add(this.lbl마감상태);
			this.bpPanelControl12.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl12.Name = "bpPanelControl12";
			this.bpPanelControl12.Size = new System.Drawing.Size(293, 23);
			this.bpPanelControl12.TabIndex = 5;
			this.bpPanelControl12.Text = "bpPanelControl12";
			// 
			// flowLayoutPanel4
			// 
			this.flowLayoutPanel4.Controls.Add(this.rdo마감전체);
			this.flowLayoutPanel4.Controls.Add(this.rdo미마감);
			this.flowLayoutPanel4.Controls.Add(this.rdo마감);
			this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Right;
			this.flowLayoutPanel4.Location = new System.Drawing.Point(106, 0);
			this.flowLayoutPanel4.Name = "flowLayoutPanel4";
			this.flowLayoutPanel4.Size = new System.Drawing.Size(187, 23);
			this.flowLayoutPanel4.TabIndex = 1;
			// 
			// rdo마감전체
			// 
			this.rdo마감전체.AutoSize = true;
			this.rdo마감전체.Checked = true;
			this.rdo마감전체.Location = new System.Drawing.Point(3, 3);
			this.rdo마감전체.Name = "rdo마감전체";
			this.rdo마감전체.Size = new System.Drawing.Size(47, 16);
			this.rdo마감전체.TabIndex = 0;
			this.rdo마감전체.TabStop = true;
			this.rdo마감전체.Text = "전체";
			this.rdo마감전체.TextDD = null;
			this.rdo마감전체.UseKeyEnter = true;
			this.rdo마감전체.UseVisualStyleBackColor = true;
			// 
			// rdo미마감
			// 
			this.rdo미마감.AutoSize = true;
			this.rdo미마감.Location = new System.Drawing.Point(56, 3);
			this.rdo미마감.Name = "rdo미마감";
			this.rdo미마감.Size = new System.Drawing.Size(59, 16);
			this.rdo미마감.TabIndex = 1;
			this.rdo미마감.TabStop = true;
			this.rdo미마감.Text = "미마감";
			this.rdo미마감.TextDD = null;
			this.rdo미마감.UseKeyEnter = true;
			this.rdo미마감.UseVisualStyleBackColor = true;
			// 
			// rdo마감
			// 
			this.rdo마감.AutoSize = true;
			this.rdo마감.Location = new System.Drawing.Point(121, 3);
			this.rdo마감.Name = "rdo마감";
			this.rdo마감.Size = new System.Drawing.Size(47, 16);
			this.rdo마감.TabIndex = 2;
			this.rdo마감.TabStop = true;
			this.rdo마감.Text = "마감";
			this.rdo마감.TextDD = null;
			this.rdo마감.UseKeyEnter = true;
			this.rdo마감.UseVisualStyleBackColor = true;
			// 
			// lbl마감상태
			// 
			this.lbl마감상태.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl마감상태.Location = new System.Drawing.Point(0, 0);
			this.lbl마감상태.Name = "lbl마감상태";
			this.lbl마감상태.Size = new System.Drawing.Size(102, 23);
			this.lbl마감상태.TabIndex = 0;
			this.lbl마감상태.Text = "마감상태";
			this.lbl마감상태.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tpg수주기준파일);
			this.tabControl.Controls.Add(this.tpg수주기준품목);
			this.tabControl.Controls.Add(this.tpg발주기준파일);
			this.tabControl.Controls.Add(this.tpg발주기준품목);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(3, 143);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(1085, 433);
			this.tabControl.TabIndex = 1;
			// 
			// tpg수주기준파일
			// 
			this.tpg수주기준파일.Controls.Add(this.splitContainer1);
			this.tpg수주기준파일.Location = new System.Drawing.Point(4, 22);
			this.tpg수주기준파일.Name = "tpg수주기준파일";
			this.tpg수주기준파일.Padding = new System.Windows.Forms.Padding(3);
			this.tpg수주기준파일.Size = new System.Drawing.Size(1077, 407);
			this.tpg수주기준파일.TabIndex = 0;
			this.tpg수주기준파일.Text = "수주기준(파일)";
			this.tpg수주기준파일.UseVisualStyleBackColor = true;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(3, 3);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this._flex수주기준파일H);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this._flex수주기준파일L);
			this.splitContainer1.Size = new System.Drawing.Size(1071, 401);
			this.splitContainer1.SplitterDistance = 244;
			this.splitContainer1.TabIndex = 1;
			// 
			// _flex수주기준파일H
			// 
			this._flex수주기준파일H.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex수주기준파일H.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex수주기준파일H.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex수주기준파일H.AutoResize = false;
			this._flex수주기준파일H.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex수주기준파일H.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex수주기준파일H.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex수주기준파일H.EnabledHeaderCheck = true;
			this._flex수주기준파일H.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex수주기준파일H.Location = new System.Drawing.Point(0, 0);
			this._flex수주기준파일H.Name = "_flex수주기준파일H";
			this._flex수주기준파일H.Rows.Count = 1;
			this._flex수주기준파일H.Rows.DefaultSize = 20;
			this._flex수주기준파일H.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex수주기준파일H.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex수주기준파일H.ShowSort = false;
			this._flex수주기준파일H.Size = new System.Drawing.Size(1071, 244);
			this._flex수주기준파일H.StyleInfo = resources.GetString("_flex수주기준파일H.StyleInfo");
			this._flex수주기준파일H.TabIndex = 0;
			this._flex수주기준파일H.UseGridCalculator = true;
			// 
			// _flex수주기준파일L
			// 
			this._flex수주기준파일L.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex수주기준파일L.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex수주기준파일L.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex수주기준파일L.AutoResize = false;
			this._flex수주기준파일L.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex수주기준파일L.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex수주기준파일L.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex수주기준파일L.EnabledHeaderCheck = true;
			this._flex수주기준파일L.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex수주기준파일L.Location = new System.Drawing.Point(0, 0);
			this._flex수주기준파일L.Name = "_flex수주기준파일L";
			this._flex수주기준파일L.Rows.Count = 1;
			this._flex수주기준파일L.Rows.DefaultSize = 20;
			this._flex수주기준파일L.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex수주기준파일L.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex수주기준파일L.ShowSort = false;
			this._flex수주기준파일L.Size = new System.Drawing.Size(1071, 153);
			this._flex수주기준파일L.StyleInfo = resources.GetString("_flex수주기준파일L.StyleInfo");
			this._flex수주기준파일L.TabIndex = 0;
			this._flex수주기준파일L.UseGridCalculator = true;
			// 
			// tpg수주기준품목
			// 
			this.tpg수주기준품목.Controls.Add(this._flex수주기준품목H);
			this.tpg수주기준품목.Location = new System.Drawing.Point(4, 22);
			this.tpg수주기준품목.Name = "tpg수주기준품목";
			this.tpg수주기준품목.Size = new System.Drawing.Size(1077, 407);
			this.tpg수주기준품목.TabIndex = 2;
			this.tpg수주기준품목.Text = "수주기준(품목)";
			this.tpg수주기준품목.UseVisualStyleBackColor = true;
			// 
			// _flex수주기준품목H
			// 
			this._flex수주기준품목H.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex수주기준품목H.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex수주기준품목H.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex수주기준품목H.AutoResize = false;
			this._flex수주기준품목H.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex수주기준품목H.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex수주기준품목H.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex수주기준품목H.EnabledHeaderCheck = true;
			this._flex수주기준품목H.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex수주기준품목H.Location = new System.Drawing.Point(0, 0);
			this._flex수주기준품목H.Name = "_flex수주기준품목H";
			this._flex수주기준품목H.Rows.Count = 1;
			this._flex수주기준품목H.Rows.DefaultSize = 20;
			this._flex수주기준품목H.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex수주기준품목H.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex수주기준품목H.ShowSort = false;
			this._flex수주기준품목H.Size = new System.Drawing.Size(1077, 407);
			this._flex수주기준품목H.StyleInfo = resources.GetString("_flex수주기준품목H.StyleInfo");
			this._flex수주기준품목H.TabIndex = 0;
			this._flex수주기준품목H.UseGridCalculator = true;
			// 
			// tpg발주기준파일
			// 
			this.tpg발주기준파일.Controls.Add(this.splitContainer2);
			this.tpg발주기준파일.Location = new System.Drawing.Point(4, 22);
			this.tpg발주기준파일.Name = "tpg발주기준파일";
			this.tpg발주기준파일.Padding = new System.Windows.Forms.Padding(3);
			this.tpg발주기준파일.Size = new System.Drawing.Size(1077, 407);
			this.tpg발주기준파일.TabIndex = 1;
			this.tpg발주기준파일.Text = "발주기준(파일)";
			this.tpg발주기준파일.UseVisualStyleBackColor = true;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(3, 3);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this._flex발주기준파일H);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this._flex발주기준파일L);
			this.splitContainer2.Size = new System.Drawing.Size(1071, 401);
			this.splitContainer2.SplitterDistance = 214;
			this.splitContainer2.TabIndex = 1;
			// 
			// _flex발주기준파일H
			// 
			this._flex발주기준파일H.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex발주기준파일H.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex발주기준파일H.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex발주기준파일H.AutoResize = false;
			this._flex발주기준파일H.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex발주기준파일H.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex발주기준파일H.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex발주기준파일H.EnabledHeaderCheck = true;
			this._flex발주기준파일H.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex발주기준파일H.Location = new System.Drawing.Point(0, 0);
			this._flex발주기준파일H.Name = "_flex발주기준파일H";
			this._flex발주기준파일H.Rows.Count = 1;
			this._flex발주기준파일H.Rows.DefaultSize = 20;
			this._flex발주기준파일H.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex발주기준파일H.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex발주기준파일H.ShowSort = false;
			this._flex발주기준파일H.Size = new System.Drawing.Size(1071, 214);
			this._flex발주기준파일H.StyleInfo = resources.GetString("_flex발주기준파일H.StyleInfo");
			this._flex발주기준파일H.TabIndex = 0;
			this._flex발주기준파일H.UseGridCalculator = true;
			// 
			// _flex발주기준파일L
			// 
			this._flex발주기준파일L.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex발주기준파일L.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex발주기준파일L.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex발주기준파일L.AutoResize = false;
			this._flex발주기준파일L.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex발주기준파일L.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex발주기준파일L.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex발주기준파일L.EnabledHeaderCheck = true;
			this._flex발주기준파일L.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex발주기준파일L.Location = new System.Drawing.Point(0, 0);
			this._flex발주기준파일L.Name = "_flex발주기준파일L";
			this._flex발주기준파일L.Rows.Count = 1;
			this._flex발주기준파일L.Rows.DefaultSize = 20;
			this._flex발주기준파일L.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex발주기준파일L.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex발주기준파일L.ShowSort = false;
			this._flex발주기준파일L.Size = new System.Drawing.Size(1071, 183);
			this._flex발주기준파일L.StyleInfo = resources.GetString("_flex발주기준파일L.StyleInfo");
			this._flex발주기준파일L.TabIndex = 0;
			this._flex발주기준파일L.UseGridCalculator = true;
			// 
			// tpg발주기준품목
			// 
			this.tpg발주기준품목.Controls.Add(this._flex발주기준품목H);
			this.tpg발주기준품목.Location = new System.Drawing.Point(4, 22);
			this.tpg발주기준품목.Name = "tpg발주기준품목";
			this.tpg발주기준품목.Size = new System.Drawing.Size(1077, 407);
			this.tpg발주기준품목.TabIndex = 3;
			this.tpg발주기준품목.Text = "발주기준(품목)";
			this.tpg발주기준품목.UseVisualStyleBackColor = true;
			// 
			// _flex발주기준품목H
			// 
			this._flex발주기준품목H.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex발주기준품목H.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex발주기준품목H.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex발주기준품목H.AutoResize = false;
			this._flex발주기준품목H.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex발주기준품목H.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex발주기준품목H.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex발주기준품목H.EnabledHeaderCheck = true;
			this._flex발주기준품목H.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex발주기준품목H.Location = new System.Drawing.Point(0, 0);
			this._flex발주기준품목H.Name = "_flex발주기준품목H";
			this._flex발주기준품목H.Rows.Count = 1;
			this._flex발주기준품목H.Rows.DefaultSize = 20;
			this._flex발주기준품목H.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex발주기준품목H.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex발주기준품목H.ShowSort = false;
			this._flex발주기준품목H.Size = new System.Drawing.Size(1077, 407);
			this._flex발주기준품목H.StyleInfo = resources.GetString("_flex발주기준품목H.StyleInfo");
			this._flex발주기준품목H.TabIndex = 0;
			this._flex발주기준품목H.UseGridCalculator = true;
			// 
			// btn메일발송
			// 
			this.btn메일발송.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn메일발송.BackColor = System.Drawing.Color.Transparent;
			this.btn메일발송.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn메일발송.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn메일발송.Location = new System.Drawing.Point(1004, 12);
			this.btn메일발송.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn메일발송.Name = "btn메일발송";
			this.btn메일발송.Size = new System.Drawing.Size(87, 19);
			this.btn메일발송.TabIndex = 3;
			this.btn메일발송.TabStop = false;
			this.btn메일발송.Text = "메일발송";
			this.btn메일발송.UseVisualStyleBackColor = false;
			// 
			// btn발송옵션
			// 
			this.btn발송옵션.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn발송옵션.BackColor = System.Drawing.Color.Transparent;
			this.btn발송옵션.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn발송옵션.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn발송옵션.Location = new System.Drawing.Point(915, 12);
			this.btn발송옵션.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn발송옵션.Name = "btn발송옵션";
			this.btn발송옵션.Size = new System.Drawing.Size(83, 19);
			this.btn발송옵션.TabIndex = 4;
			this.btn발송옵션.TabStop = false;
			this.btn발송옵션.Text = "발송옵션";
			this.btn발송옵션.UseVisualStyleBackColor = false;
			// 
			// btn재고납기업로드
			// 
			this.btn재고납기업로드.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn재고납기업로드.BackColor = System.Drawing.Color.Transparent;
			this.btn재고납기업로드.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn재고납기업로드.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn재고납기업로드.Location = new System.Drawing.Point(791, 12);
			this.btn재고납기업로드.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn재고납기업로드.Name = "btn재고납기업로드";
			this.btn재고납기업로드.Size = new System.Drawing.Size(118, 19);
			this.btn재고납기업로드.TabIndex = 5;
			this.btn재고납기업로드.TabStop = false;
			this.btn재고납기업로드.Text = "재고납기업로드";
			this.btn재고납기업로드.UseVisualStyleBackColor = false;
			// 
			// P_CZ_SA_DEFERRED_DELIVERY_MNG
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Controls.Add(this.btn재고납기업로드);
			this.Controls.Add(this.btn발송옵션);
			this.Controls.Add(this.btn메일발송);
			this.Name = "P_CZ_SA_DEFERRED_DELIVERY_MNG";
			this.Size = new System.Drawing.Size(1091, 619);
			this.TitleText = "납기지연현황";
			this.Controls.SetChildIndex(this.mDataArea, 0);
			this.Controls.SetChildIndex(this.btn메일발송, 0);
			this.Controls.SetChildIndex(this.btn발송옵션, 0);
			this.Controls.SetChildIndex(this.btn재고납기업로드, 0);
			this.mDataArea.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl8.ResumeLayout(false);
			this.bpPanelControl8.PerformLayout();
			this.bpPanelControl2.ResumeLayout(false);
			this.bpPanelControl2.PerformLayout();
			this.bpPanelControl1.ResumeLayout(false);
			this.oneGridItem3.ResumeLayout(false);
			this.bpPanelControl7.ResumeLayout(false);
			this.bpPanelControl6.ResumeLayout(false);
			this.bpPanelControl5.ResumeLayout(false);
			this.oneGridItem4.ResumeLayout(false);
			this.bpPanelControl15.ResumeLayout(false);
			this.bpPanelControl10.ResumeLayout(false);
			this.bpPanelControl9.ResumeLayout(false);
			this.oneGridItem2.ResumeLayout(false);
			this.bpPanelControl4.ResumeLayout(false);
			this.flowLayoutPanel2.ResumeLayout(false);
			this.flowLayoutPanel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.rdo출고전체)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo미출고)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo출고)).EndInit();
			this.bpPanelControl3.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.rdo입고전체)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo미입고)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo입고)).EndInit();
			this.bpPanelControl11.ResumeLayout(false);
			this.flowLayoutPanel3.ResumeLayout(false);
			this.flowLayoutPanel3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.rdo의뢰전체)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo미의뢰)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo의뢰)).EndInit();
			this.oneGridItem5.ResumeLayout(false);
			this.bpPanelControl13.ResumeLayout(false);
			this.bpPanelControl13.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur지연일수)).EndInit();
			this.bpPanelControl14.ResumeLayout(false);
			this.bpPanelControl14.PerformLayout();
			this.bpPanelControl12.ResumeLayout(false);
			this.flowLayoutPanel4.ResumeLayout(false);
			this.flowLayoutPanel4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.rdo마감전체)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo미마감)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo마감)).EndInit();
			this.tabControl.ResumeLayout(false);
			this.tpg수주기준파일.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex수주기준파일H)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._flex수주기준파일L)).EndInit();
			this.tpg수주기준품목.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex수주기준품목H)).EndInit();
			this.tpg발주기준파일.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex발주기준파일H)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._flex발주기준파일L)).EndInit();
			this.tpg발주기준품목.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex발주기준품목H)).EndInit();
			this.ResumeLayout(false);

        }

        private TableLayoutPanel tableLayoutPanel1;
        private OneGrid oneGrid1;
        private OneGridItem oneGridItem1;
        private BpPanelControl bpPanelControl1;
        private PeriodPicker dtp수주일자;
        private LabelExt lbl수주일자;
        private BpPanelControl bpPanelControl2;
        private TextBoxExt txt수주번호;
        private LabelExt lbl수주번호;
        #endregion
        private TabControlExt tabControl;
        private TabPage tpg수주기준파일;
        private TabPage tpg발주기준파일;
        private FlexGrid _flex수주기준파일H;
        private FlexGrid _flex발주기준파일H;
        private OneGridItem oneGridItem2;
        private BpPanelControl bpPanelControl4;
        private FlowLayoutPanel flowLayoutPanel2;
        private RadioButtonExt rdo출고전체;
        private RadioButtonExt rdo미출고;
        private RadioButtonExt rdo출고;
        private LabelExt lbl출고상태;
        private BpPanelControl bpPanelControl3;
        private FlowLayoutPanel flowLayoutPanel1;
        private RadioButtonExt rdo입고전체;
        private RadioButtonExt rdo미입고;
        private RadioButtonExt rdo입고;
        private LabelExt lbl입고상태;
        private OneGridItem oneGridItem3;
        private BpPanelControl bpPanelControl5;
        private BpCodeTextBox ctx호선;
        private LabelExt lbl호선;
        private BpPanelControl bpPanelControl7;
        private LabelExt lbl매입처;
        private BpPanelControl bpPanelControl6;
        private BpCodeTextBox ctx매출처;
        private LabelExt lbl매출처;
        private BpPanelControl bpPanelControl8;
        private TextBoxExt txt매입처발주번호;
        private LabelExt lbl매입처발주번호;
        private OneGridItem oneGridItem4;
        private BpPanelControl bpPanelControl9;
        private LabelExt lbl영업그룹;
        private BpPanelControl bpPanelControl10;
        private LabelExt lbl매출처그룹;
        private BpPanelControl bpPanelControl11;
        private FlowLayoutPanel flowLayoutPanel3;
        private RadioButtonExt rdo의뢰전체;
        private RadioButtonExt rdo미의뢰;
        private RadioButtonExt rdo의뢰;
        private LabelExt lbl의뢰상태;
        private BpComboBox bpc영업그룹;
        private SplitContainer splitContainer1;
        private FlexGrid _flex수주기준파일L;
        private SplitContainer splitContainer2;
        private FlexGrid _flex발주기준파일L;
        private BpPanelControl bpPanelControl12;
        private FlowLayoutPanel flowLayoutPanel4;
        private RadioButtonExt rdo마감전체;
        private RadioButtonExt rdo미마감;
        private RadioButtonExt rdo마감;
        private LabelExt lbl마감상태;
        private TabPage tpg수주기준품목;
        private TabPage tpg발주기준품목;
        private FlexGrid _flex수주기준품목H;
        private FlexGrid _flex발주기준품목H;
        private BpPanelControl bpPanelControl13;
        private LabelExt labelExt1;
        private CurrencyTextBox cur지연일수;
        private LabelExt lbl지연일수;
        private OneGridItem oneGridItem5;
        private BpComboBox bpc매입처;
        private BpPanelControl bpPanelControl14;
        private CheckBoxExt chk셀병합;
        private LabelExt lbl셀병합여부;
        private BpComboBox bpc매출처그룹;
        private RoundedButton btn메일발송;
        private RoundedButton btn발송옵션;
        private BpPanelControl bpPanelControl15;
        private BpCodeTextBox cbx물류담당자;
        private LabelExt lbl물류담당자;
		private RoundedButton btn재고납기업로드;
	}
}
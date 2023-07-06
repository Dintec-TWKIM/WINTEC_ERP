
using C1.Win.C1FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms.Help;
using Duzon.Erpiu.Windows.OneControls;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
	partial class P_CZ_PR_ROUT_REG
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_PR_ROUT_REG));
			this.btn붙여넣기 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn복사하기 = new Duzon.Common.Controls.RoundedButton(this.components);
			this._tlayMain = new System.Windows.Forms.TableLayoutPanel();
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl공정경로유형 = new Duzon.Common.Controls.LabelExt();
			this.cbo공정경로유형 = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPnl_plant = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl공장 = new Duzon.Common.Controls.LabelExt();
			this.cbo공장 = new Duzon.Common.Controls.DropDownComboBox();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl9 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx품목To = new Duzon.Common.BpControls.BpCodeNTextBox();
			this.lbl품목To = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl품목From = new Duzon.Common.Controls.LabelExt();
			this.ctx품목From = new Duzon.Common.BpControls.BpCodeNTextBox();
			this.oneGridItem3 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl품목군S = new Duzon.Common.Controls.LabelExt();
			this.ctx품목군S = new Duzon.Common.BpControls.BpCodeTextBox();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl조달구분 = new Duzon.Common.Controls.LabelExt();
			this.cbo조달구분 = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl품목계정 = new Duzon.Common.Controls.LabelExt();
			this.cbo품목계정 = new Duzon.Common.Controls.DropDownComboBox();
			this.oneGridItem4 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPnl_radioButton = new Duzon.Common.BpControls.BpPanelControl();
			this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
			this.rdo전체조회 = new Duzon.Common.Controls.RadioButtonExt();
			this.rdo등록된품목만조회 = new Duzon.Common.Controls.RadioButtonExt();
			this.rdo등록되지않은품목만조회 = new Duzon.Common.Controls.RadioButtonExt();
			this.rdo작업장미등록품목조회 = new Duzon.Common.Controls.RadioButtonExt();
			this.oneGridItem5 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl8 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx소분류 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl소분류 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl7 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx중분류 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl중분류 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl6 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx대분류 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl대분류 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem6 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl16 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx제품군 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl제품군 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl11 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl공정 = new Duzon.Common.Controls.LabelExt();
			this.bpc공정 = new Duzon.Common.BpControls.BpComboBox();
			this.bpPanelControl10 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl작업장 = new Duzon.Common.Controls.LabelExt();
			this.bpc작업장 = new Duzon.Common.BpControls.BpComboBox();
			this.oneGridItem7 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl13 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo작업지침서등록 = new Duzon.Common.Controls.DropDownComboBox();
			this.lbl작업지침서등록 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl12 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo측정항목등록 = new Duzon.Common.Controls.DropDownComboBox();
			this.lbl측정항목등록 = new Duzon.Common.Controls.LabelExt();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this._flex품목 = new Dass.FlexGrid.FlexGrid(this.components);
			this._flex공정경로 = new Dass.FlexGrid.FlexGrid(this.components);
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this._flex공정 = new Dass.FlexGrid.FlexGrid(this.components);
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.btn삭제 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn추가 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.chk공정사용여부 = new Duzon.Common.Controls.CheckBoxExt();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tpg작업지침서 = new System.Windows.Forms.TabPage();
			this.imagePanel2 = new Duzon.Common.Controls.ImagePanel(this.components);
			this.splitContainer4 = new System.Windows.Forms.SplitContainer();
			this._flex작업지침서 = new Dass.FlexGrid.FlexGrid(this.components);
			this.web미리보기 = new Duzon.Common.Controls.WebBrowserExt();
			this.btn작업지침서미리보기 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn작업지침서열기 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn작업지침서삭제 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn작업지침서추가 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.tpg측정항목 = new System.Windows.Forms.TabPage();
			this.imagePanel3 = new Duzon.Common.Controls.ImagePanel(this.components);
			this.chk측정항목사용여부 = new Duzon.Common.Controls.CheckBoxExt();
			this.btn측정항목저장 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn측정항목삭제 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn측정항목추가 = new Duzon.Common.Controls.RoundedButton(this.components);
			this._flex측정항목 = new Dass.FlexGrid.FlexGrid(this.components);
			this.tpg작업내용 = new System.Windows.Forms.TabPage();
			this.imagePanel1 = new Duzon.Common.Controls.ImagePanel(this.components);
			this.panelExt1 = new Duzon.Common.Controls.PanelExt();
			this.txt작업내용 = new Duzon.Common.Controls.TextBoxExt();
			this.tpg설비 = new System.Windows.Forms.TabPage();
			this.imagePanel4 = new Duzon.Common.Controls.ImagePanel(this.components);
			this._flex설비 = new Dass.FlexGrid.FlexGrid(this.components);
			this.btn설비저장 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn설비삭제 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn설비추가 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btn측정항목붙여넣기 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn측정항목복사 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn측정항목 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn배치그룹관리 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btnAPS연동 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.mDataArea.SuspendLayout();
			this._tlayMain.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.bpPnl_plant.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.bpPanelControl9.SuspendLayout();
			this.bpPanelControl3.SuspendLayout();
			this.oneGridItem3.SuspendLayout();
			this.bpPanelControl5.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.bpPanelControl4.SuspendLayout();
			this.oneGridItem4.SuspendLayout();
			this.bpPnl_radioButton.SuspendLayout();
			this.flowLayoutPanel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.rdo전체조회)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo등록된품목만조회)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo등록되지않은품목만조회)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo작업장미등록품목조회)).BeginInit();
			this.oneGridItem5.SuspendLayout();
			this.bpPanelControl8.SuspendLayout();
			this.bpPanelControl7.SuspendLayout();
			this.bpPanelControl6.SuspendLayout();
			this.oneGridItem6.SuspendLayout();
			this.bpPanelControl16.SuspendLayout();
			this.bpPanelControl11.SuspendLayout();
			this.bpPanelControl10.SuspendLayout();
			this.oneGridItem7.SuspendLayout();
			this.bpPanelControl13.SuspendLayout();
			this.bpPanelControl12.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex품목)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._flex공정경로)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex공정)).BeginInit();
			this.flowLayoutPanel2.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tpg작업지침서.SuspendLayout();
			this.imagePanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
			this.splitContainer4.Panel1.SuspendLayout();
			this.splitContainer4.Panel2.SuspendLayout();
			this.splitContainer4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex작업지침서)).BeginInit();
			this.tpg측정항목.SuspendLayout();
			this.imagePanel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex측정항목)).BeginInit();
			this.tpg작업내용.SuspendLayout();
			this.imagePanel1.SuspendLayout();
			this.panelExt1.SuspendLayout();
			this.tpg설비.SuspendLayout();
			this.imagePanel4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex설비)).BeginInit();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this._tlayMain);
			this.mDataArea.Size = new System.Drawing.Size(1479, 929);
			// 
			// btn붙여넣기
			// 
			this.btn붙여넣기.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn붙여넣기.BackColor = System.Drawing.Color.White;
			this.btn붙여넣기.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn붙여넣기.Enabled = false;
			this.btn붙여넣기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn붙여넣기.Location = new System.Drawing.Point(337, 3);
			this.btn붙여넣기.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn붙여넣기.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn붙여넣기.Name = "btn붙여넣기";
			this.btn붙여넣기.Size = new System.Drawing.Size(80, 19);
			this.btn붙여넣기.TabIndex = 6;
			this.btn붙여넣기.TabStop = false;
			this.btn붙여넣기.Tag = "붙여넣기";
			this.btn붙여넣기.Text = "붙여넣기";
			this.btn붙여넣기.UseVisualStyleBackColor = false;
			// 
			// btn복사하기
			// 
			this.btn복사하기.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn복사하기.BackColor = System.Drawing.Color.White;
			this.btn복사하기.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn복사하기.Enabled = false;
			this.btn복사하기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn복사하기.Location = new System.Drawing.Point(259, 3);
			this.btn복사하기.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn복사하기.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn복사하기.Name = "btn복사하기";
			this.btn복사하기.Size = new System.Drawing.Size(75, 19);
			this.btn복사하기.TabIndex = 5;
			this.btn복사하기.TabStop = false;
			this.btn복사하기.Tag = "복사";
			this.btn복사하기.Text = "복사하기";
			this.btn복사하기.UseVisualStyleBackColor = false;
			// 
			// _tlayMain
			// 
			this._tlayMain.AutoSize = true;
			this._tlayMain.ColumnCount = 1;
			this._tlayMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this._tlayMain.Controls.Add(this.oneGrid1, 0, 0);
			this._tlayMain.Controls.Add(this.splitContainer2, 0, 1);
			this._tlayMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this._tlayMain.Location = new System.Drawing.Point(0, 0);
			this._tlayMain.Name = "_tlayMain";
			this._tlayMain.RowCount = 2;
			this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 183F));
			this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this._tlayMain.Size = new System.Drawing.Size(1479, 929);
			this._tlayMain.TabIndex = 5;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2,
            this.oneGridItem3,
            this.oneGridItem4,
            this.oneGridItem5,
            this.oneGridItem6,
            this.oneGridItem7});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(1473, 177);
			this.oneGrid1.TabIndex = 76;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl2);
			this.oneGridItem1.Controls.Add(this.bpPnl_plant);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(1463, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.lbl공정경로유형);
			this.bpPanelControl2.Controls.Add(this.cbo공정경로유형);
			this.bpPanelControl2.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl2.TabIndex = 0;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// lbl공정경로유형
			// 
			this.lbl공정경로유형.BackColor = System.Drawing.Color.Transparent;
			this.lbl공정경로유형.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl공정경로유형.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl공정경로유형.Location = new System.Drawing.Point(0, 0);
			this.lbl공정경로유형.Name = "lbl공정경로유형";
			this.lbl공정경로유형.Size = new System.Drawing.Size(100, 23);
			this.lbl공정경로유형.TabIndex = 0;
			this.lbl공정경로유형.Tag = "공정경로유형";
			this.lbl공정경로유형.Text = "공정경로유형";
			this.lbl공정경로유형.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo공정경로유형
			// 
			this.cbo공정경로유형.AutoDropDown = false;
			this.cbo공정경로유형.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo공정경로유형.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo공정경로유형.ItemHeight = 12;
			this.cbo공정경로유형.Location = new System.Drawing.Point(106, 0);
			this.cbo공정경로유형.MaxLength = 50;
			this.cbo공정경로유형.Name = "cbo공정경로유형";
			this.cbo공정경로유형.Size = new System.Drawing.Size(186, 20);
			this.cbo공정경로유형.TabIndex = 5;
			this.cbo공정경로유형.UseKeyF3 = false;
			// 
			// bpPnl_plant
			// 
			this.bpPnl_plant.Controls.Add(this.lbl공장);
			this.bpPnl_plant.Controls.Add(this.cbo공장);
			this.bpPnl_plant.Location = new System.Drawing.Point(2, 1);
			this.bpPnl_plant.Name = "bpPnl_plant";
			this.bpPnl_plant.Size = new System.Drawing.Size(292, 23);
			this.bpPnl_plant.TabIndex = 0;
			this.bpPnl_plant.Text = "bpPanelControl1";
			// 
			// lbl공장
			// 
			this.lbl공장.BackColor = System.Drawing.Color.Transparent;
			this.lbl공장.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl공장.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl공장.Location = new System.Drawing.Point(0, 0);
			this.lbl공장.Name = "lbl공장";
			this.lbl공장.Size = new System.Drawing.Size(100, 23);
			this.lbl공장.TabIndex = 0;
			this.lbl공장.Tag = "공장";
			this.lbl공장.Text = "공장";
			this.lbl공장.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo공장
			// 
			this.cbo공장.AutoDropDown = true;
			this.cbo공장.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.cbo공장.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo공장.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo공장.ItemHeight = 12;
			this.cbo공장.Location = new System.Drawing.Point(106, 0);
			this.cbo공장.Name = "cbo공장";
			this.cbo공장.Size = new System.Drawing.Size(186, 20);
			this.cbo공장.TabIndex = 1;
			this.cbo공장.UseKeyF3 = false;
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.bpPanelControl9);
			this.oneGridItem2.Controls.Add(this.bpPanelControl3);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(1463, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// bpPanelControl9
			// 
			this.bpPanelControl9.Controls.Add(this.ctx품목To);
			this.bpPanelControl9.Controls.Add(this.lbl품목To);
			this.bpPanelControl9.Location = new System.Drawing.Point(444, 1);
			this.bpPanelControl9.Name = "bpPanelControl9";
			this.bpPanelControl9.Size = new System.Drawing.Size(440, 23);
			this.bpPanelControl9.TabIndex = 1;
			this.bpPanelControl9.Text = "bpPanelControl9";
			// 
			// ctx품목To
			// 
			this.ctx품목To.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx품목To.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
			this.ctx품목To.Location = new System.Drawing.Point(106, 0);
			this.ctx품목To.Name = "ctx품목To";
			this.ctx품목To.Size = new System.Drawing.Size(334, 21);
			this.ctx품목To.TabIndex = 3;
			this.ctx품목To.TabStop = false;
			this.ctx품목To.UserCodeName = "NM_ITEM";
			this.ctx품목To.UserCodeValue = "CD_ITEM";
			this.ctx품목To.UserHelpID = "H_MA_PITEM_SUB";
			this.ctx품목To.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpControl_QueryAfter);
			this.ctx품목To.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpControl_QueryBefore);
			// 
			// lbl품목To
			// 
			this.lbl품목To.BackColor = System.Drawing.Color.Transparent;
			this.lbl품목To.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl품목To.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl품목To.Location = new System.Drawing.Point(0, 0);
			this.lbl품목To.Name = "lbl품목To";
			this.lbl품목To.Size = new System.Drawing.Size(100, 23);
			this.lbl품목To.TabIndex = 3;
			this.lbl품목To.Tag = "공장";
			this.lbl품목To.Text = "품목To";
			this.lbl품목To.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.lbl품목From);
			this.bpPanelControl3.Controls.Add(this.ctx품목From);
			this.bpPanelControl3.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(440, 23);
			this.bpPanelControl3.TabIndex = 0;
			this.bpPanelControl3.Text = "bpPanelControl3";
			// 
			// lbl품목From
			// 
			this.lbl품목From.BackColor = System.Drawing.Color.Transparent;
			this.lbl품목From.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl품목From.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl품목From.Location = new System.Drawing.Point(0, 0);
			this.lbl품목From.Name = "lbl품목From";
			this.lbl품목From.Size = new System.Drawing.Size(100, 23);
			this.lbl품목From.TabIndex = 3;
			this.lbl품목From.Tag = "공장";
			this.lbl품목From.Text = "품목From";
			this.lbl품목From.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ctx품목From
			// 
			this.ctx품목From.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx품목From.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
			this.ctx품목From.LabelWidth = 156;
			this.ctx품목From.Location = new System.Drawing.Point(106, 0);
			this.ctx품목From.Name = "ctx품목From";
			this.ctx품목From.Size = new System.Drawing.Size(334, 21);
			this.ctx품목From.TabIndex = 2;
			this.ctx품목From.TabStop = false;
			this.ctx품목From.UserCodeName = "NM_ITEM";
			this.ctx품목From.UserCodeValue = "CD_ITEM";
			this.ctx품목From.UserHelpID = "H_MA_PITEM_SUB";
			this.ctx품목From.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpControl_QueryAfter);
			this.ctx품목From.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpControl_QueryBefore);
			// 
			// oneGridItem3
			// 
			this.oneGridItem3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem3.Controls.Add(this.bpPanelControl5);
			this.oneGridItem3.Controls.Add(this.bpPanelControl1);
			this.oneGridItem3.Controls.Add(this.bpPanelControl4);
			this.oneGridItem3.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem3.Location = new System.Drawing.Point(0, 46);
			this.oneGridItem3.Name = "oneGridItem3";
			this.oneGridItem3.Size = new System.Drawing.Size(1463, 23);
			this.oneGridItem3.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem3.TabIndex = 2;
			// 
			// bpPanelControl5
			// 
			this.bpPanelControl5.Controls.Add(this.lbl품목군S);
			this.bpPanelControl5.Controls.Add(this.ctx품목군S);
			this.bpPanelControl5.Location = new System.Drawing.Point(590, 1);
			this.bpPanelControl5.Name = "bpPanelControl5";
			this.bpPanelControl5.Size = new System.Drawing.Size(294, 23);
			this.bpPanelControl5.TabIndex = 9;
			this.bpPanelControl5.Text = "bpPanelControl5";
			// 
			// lbl품목군S
			// 
			this.lbl품목군S.BackColor = System.Drawing.Color.Transparent;
			this.lbl품목군S.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl품목군S.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl품목군S.Location = new System.Drawing.Point(0, 0);
			this.lbl품목군S.Name = "lbl품목군S";
			this.lbl품목군S.Size = new System.Drawing.Size(100, 23);
			this.lbl품목군S.TabIndex = 0;
			this.lbl품목군S.Tag = "GRP_ITEM";
			this.lbl품목군S.Text = "품목군";
			this.lbl품목군S.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ctx품목군S
			// 
			this.ctx품목군S.CodeName = null;
			this.ctx품목군S.CodeValue = null;
			this.ctx품목군S.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx품목군S.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_ITEMGP_SUB;
			this.ctx품목군S.Location = new System.Drawing.Point(108, 0);
			this.ctx품목군S.Name = "ctx품목군S";
			this.ctx품목군S.Size = new System.Drawing.Size(186, 21);
			this.ctx품목군S.TabIndex = 2;
			this.ctx품목군S.TabStop = false;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.lbl조달구분);
			this.bpPanelControl1.Controls.Add(this.cbo조달구분);
			this.bpPanelControl1.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl1.TabIndex = 2;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// lbl조달구분
			// 
			this.lbl조달구분.BackColor = System.Drawing.Color.Transparent;
			this.lbl조달구분.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl조달구분.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl조달구분.Location = new System.Drawing.Point(0, 0);
			this.lbl조달구분.Name = "lbl조달구분";
			this.lbl조달구분.Size = new System.Drawing.Size(100, 23);
			this.lbl조달구분.TabIndex = 0;
			this.lbl조달구분.Tag = "TP_PROC";
			this.lbl조달구분.Text = "조달구분";
			this.lbl조달구분.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo조달구분
			// 
			this.cbo조달구분.AutoDropDown = true;
			this.cbo조달구분.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo조달구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo조달구분.ItemHeight = 12;
			this.cbo조달구분.Location = new System.Drawing.Point(106, 0);
			this.cbo조달구분.Name = "cbo조달구분";
			this.cbo조달구분.Size = new System.Drawing.Size(186, 20);
			this.cbo조달구분.TabIndex = 1;
			this.cbo조달구분.Tag = "";
			// 
			// bpPanelControl4
			// 
			this.bpPanelControl4.Controls.Add(this.lbl품목계정);
			this.bpPanelControl4.Controls.Add(this.cbo품목계정);
			this.bpPanelControl4.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl4.Name = "bpPanelControl4";
			this.bpPanelControl4.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl4.TabIndex = 0;
			this.bpPanelControl4.Text = "bpPanelControl4";
			// 
			// lbl품목계정
			// 
			this.lbl품목계정.BackColor = System.Drawing.Color.Transparent;
			this.lbl품목계정.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl품목계정.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl품목계정.Location = new System.Drawing.Point(0, 0);
			this.lbl품목계정.Name = "lbl품목계정";
			this.lbl품목계정.Size = new System.Drawing.Size(100, 23);
			this.lbl품목계정.TabIndex = 0;
			this.lbl품목계정.Tag = "품목계정";
			this.lbl품목계정.Text = "품목계정";
			this.lbl품목계정.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo품목계정
			// 
			this.cbo품목계정.AutoDropDown = true;
			this.cbo품목계정.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo품목계정.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo품목계정.ItemHeight = 12;
			this.cbo품목계정.Location = new System.Drawing.Point(106, 0);
			this.cbo품목계정.MaxLength = 50;
			this.cbo품목계정.Name = "cbo품목계정";
			this.cbo품목계정.Size = new System.Drawing.Size(186, 20);
			this.cbo품목계정.TabIndex = 4;
			this.cbo품목계정.UseKeyF3 = false;
			// 
			// oneGridItem4
			// 
			this.oneGridItem4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem4.Controls.Add(this.bpPnl_radioButton);
			this.oneGridItem4.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem4.Location = new System.Drawing.Point(0, 69);
			this.oneGridItem4.Name = "oneGridItem4";
			this.oneGridItem4.Size = new System.Drawing.Size(1463, 23);
			this.oneGridItem4.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem4.TabIndex = 3;
			// 
			// bpPnl_radioButton
			// 
			this.bpPnl_radioButton.Controls.Add(this.flowLayoutPanel3);
			this.bpPnl_radioButton.Location = new System.Drawing.Point(2, 1);
			this.bpPnl_radioButton.Name = "bpPnl_radioButton";
			this.bpPnl_radioButton.Size = new System.Drawing.Size(882, 23);
			this.bpPnl_radioButton.TabIndex = 1;
			this.bpPnl_radioButton.Text = "bpPanelControl5";
			// 
			// flowLayoutPanel3
			// 
			this.flowLayoutPanel3.Controls.Add(this.rdo전체조회);
			this.flowLayoutPanel3.Controls.Add(this.rdo등록된품목만조회);
			this.flowLayoutPanel3.Controls.Add(this.rdo등록되지않은품목만조회);
			this.flowLayoutPanel3.Controls.Add(this.rdo작업장미등록품목조회);
			this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel3.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel3.Name = "flowLayoutPanel3";
			this.flowLayoutPanel3.Size = new System.Drawing.Size(882, 23);
			this.flowLayoutPanel3.TabIndex = 218;
			// 
			// rdo전체조회
			// 
			this.rdo전체조회.BackColor = System.Drawing.Color.Transparent;
			this.rdo전체조회.Checked = true;
			this.rdo전체조회.Cursor = System.Windows.Forms.Cursors.Hand;
			this.rdo전체조회.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.rdo전체조회.Location = new System.Drawing.Point(3, 3);
			this.rdo전체조회.Name = "rdo전체조회";
			this.rdo전체조회.Size = new System.Drawing.Size(72, 16);
			this.rdo전체조회.TabIndex = 215;
			this.rdo전체조회.TabStop = true;
			this.rdo전체조회.Tag = "";
			this.rdo전체조회.Text = "전체조회";
			this.rdo전체조회.TextDD = null;
			this.rdo전체조회.UseKeyEnter = false;
			this.rdo전체조회.UseVisualStyleBackColor = false;
			// 
			// rdo등록된품목만조회
			// 
			this.rdo등록된품목만조회.BackColor = System.Drawing.Color.Transparent;
			this.rdo등록된품목만조회.Cursor = System.Windows.Forms.Cursors.Hand;
			this.rdo등록된품목만조회.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.rdo등록된품목만조회.ForeColor = System.Drawing.SystemColors.ActiveCaption;
			this.rdo등록된품목만조회.Location = new System.Drawing.Point(81, 3);
			this.rdo등록된품목만조회.Name = "rdo등록된품목만조회";
			this.rdo등록된품목만조회.Size = new System.Drawing.Size(143, 16);
			this.rdo등록된품목만조회.TabIndex = 216;
			this.rdo등록된품목만조회.TabStop = true;
			this.rdo등록된품목만조회.Tag = "";
			this.rdo등록된품목만조회.Text = "등록된 품목만 조회";
			this.rdo등록된품목만조회.TextDD = null;
			this.rdo등록된품목만조회.UseKeyEnter = false;
			this.rdo등록된품목만조회.UseVisualStyleBackColor = false;
			// 
			// rdo등록되지않은품목만조회
			// 
			this.rdo등록되지않은품목만조회.BackColor = System.Drawing.Color.Transparent;
			this.rdo등록되지않은품목만조회.Cursor = System.Windows.Forms.Cursors.Hand;
			this.rdo등록되지않은품목만조회.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.rdo등록되지않은품목만조회.ForeColor = System.Drawing.Color.Red;
			this.rdo등록되지않은품목만조회.Location = new System.Drawing.Point(230, 3);
			this.rdo등록되지않은품목만조회.Name = "rdo등록되지않은품목만조회";
			this.rdo등록되지않은품목만조회.Size = new System.Drawing.Size(183, 16);
			this.rdo등록되지않은품목만조회.TabIndex = 217;
			this.rdo등록되지않은품목만조회.TabStop = true;
			this.rdo등록되지않은품목만조회.Tag = "";
			this.rdo등록되지않은품목만조회.Text = "등록되지 않은 품목만 조회";
			this.rdo등록되지않은품목만조회.TextDD = null;
			this.rdo등록되지않은품목만조회.UseKeyEnter = false;
			this.rdo등록되지않은품목만조회.UseVisualStyleBackColor = false;
			// 
			// rdo작업장미등록품목조회
			// 
			this.rdo작업장미등록품목조회.BackColor = System.Drawing.Color.Transparent;
			this.rdo작업장미등록품목조회.Cursor = System.Windows.Forms.Cursors.Hand;
			this.rdo작업장미등록품목조회.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.rdo작업장미등록품목조회.ForeColor = System.Drawing.Color.DarkOliveGreen;
			this.rdo작업장미등록품목조회.Location = new System.Drawing.Point(419, 3);
			this.rdo작업장미등록품목조회.Name = "rdo작업장미등록품목조회";
			this.rdo작업장미등록품목조회.Size = new System.Drawing.Size(178, 16);
			this.rdo작업장미등록품목조회.TabIndex = 215;
			this.rdo작업장미등록품목조회.TabStop = true;
			this.rdo작업장미등록품목조회.Tag = "";
			this.rdo작업장미등록품목조회.Text = "작업장 미등록 품목만 조회";
			this.rdo작업장미등록품목조회.TextDD = null;
			this.rdo작업장미등록품목조회.UseKeyEnter = false;
			this.rdo작업장미등록품목조회.UseVisualStyleBackColor = false;
			// 
			// oneGridItem5
			// 
			this.oneGridItem5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem5.Controls.Add(this.bpPanelControl8);
			this.oneGridItem5.Controls.Add(this.bpPanelControl7);
			this.oneGridItem5.Controls.Add(this.bpPanelControl6);
			this.oneGridItem5.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem5.Location = new System.Drawing.Point(0, 92);
			this.oneGridItem5.Name = "oneGridItem5";
			this.oneGridItem5.Size = new System.Drawing.Size(1463, 23);
			this.oneGridItem5.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem5.TabIndex = 4;
			// 
			// bpPanelControl8
			// 
			this.bpPanelControl8.Controls.Add(this.ctx소분류);
			this.bpPanelControl8.Controls.Add(this.lbl소분류);
			this.bpPanelControl8.Location = new System.Drawing.Point(590, 1);
			this.bpPanelControl8.Name = "bpPanelControl8";
			this.bpPanelControl8.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl8.TabIndex = 0;
			this.bpPanelControl8.Text = "bpPanelControl8";
			// 
			// ctx소분류
			// 
			this.ctx소분류.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx소분류.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB;
			this.ctx소분류.Location = new System.Drawing.Point(108, 0);
			this.ctx소분류.Name = "ctx소분류";
			this.ctx소분류.Size = new System.Drawing.Size(184, 21);
			this.ctx소분류.TabIndex = 114;
			this.ctx소분류.TabStop = false;
			this.ctx소분류.Tag = "";
			this.ctx소분류.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpControl_QueryBefore);
			// 
			// lbl소분류
			// 
			this.lbl소분류.BackColor = System.Drawing.Color.Transparent;
			this.lbl소분류.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl소분류.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl소분류.Location = new System.Drawing.Point(0, 0);
			this.lbl소분류.Name = "lbl소분류";
			this.lbl소분류.Size = new System.Drawing.Size(100, 23);
			this.lbl소분류.TabIndex = 113;
			this.lbl소분류.Tag = "";
			this.lbl소분류.Text = "소분류";
			this.lbl소분류.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl7
			// 
			this.bpPanelControl7.Controls.Add(this.ctx중분류);
			this.bpPanelControl7.Controls.Add(this.lbl중분류);
			this.bpPanelControl7.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl7.Name = "bpPanelControl7";
			this.bpPanelControl7.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl7.TabIndex = 0;
			this.bpPanelControl7.Text = "bpPanelControl7";
			// 
			// ctx중분류
			// 
			this.ctx중분류.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx중분류.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB;
			this.ctx중분류.Location = new System.Drawing.Point(106, 0);
			this.ctx중분류.Name = "ctx중분류";
			this.ctx중분류.Size = new System.Drawing.Size(186, 21);
			this.ctx중분류.TabIndex = 114;
			this.ctx중분류.TabStop = false;
			this.ctx중분류.Tag = "";
			this.ctx중분류.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpControl_QueryBefore);
			// 
			// lbl중분류
			// 
			this.lbl중분류.BackColor = System.Drawing.Color.Transparent;
			this.lbl중분류.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl중분류.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl중분류.Location = new System.Drawing.Point(0, 0);
			this.lbl중분류.Name = "lbl중분류";
			this.lbl중분류.Size = new System.Drawing.Size(100, 23);
			this.lbl중분류.TabIndex = 113;
			this.lbl중분류.Tag = "";
			this.lbl중분류.Text = "중분류";
			this.lbl중분류.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl6
			// 
			this.bpPanelControl6.Controls.Add(this.ctx대분류);
			this.bpPanelControl6.Controls.Add(this.lbl대분류);
			this.bpPanelControl6.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl6.Name = "bpPanelControl6";
			this.bpPanelControl6.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl6.TabIndex = 0;
			this.bpPanelControl6.Text = "bpPanelControl6";
			// 
			// ctx대분류
			// 
			this.ctx대분류.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx대분류.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB;
			this.ctx대분류.Location = new System.Drawing.Point(106, 0);
			this.ctx대분류.Name = "ctx대분류";
			this.ctx대분류.Size = new System.Drawing.Size(186, 21);
			this.ctx대분류.TabIndex = 114;
			this.ctx대분류.TabStop = false;
			this.ctx대분류.Tag = "";
			this.ctx대분류.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpControl_QueryBefore);
			// 
			// lbl대분류
			// 
			this.lbl대분류.BackColor = System.Drawing.Color.Transparent;
			this.lbl대분류.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl대분류.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl대분류.Location = new System.Drawing.Point(0, 0);
			this.lbl대분류.Name = "lbl대분류";
			this.lbl대분류.Size = new System.Drawing.Size(100, 23);
			this.lbl대분류.TabIndex = 113;
			this.lbl대분류.Tag = "";
			this.lbl대분류.Text = "대분류";
			this.lbl대분류.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem6
			// 
			this.oneGridItem6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem6.Controls.Add(this.bpPanelControl16);
			this.oneGridItem6.Controls.Add(this.bpPanelControl11);
			this.oneGridItem6.Controls.Add(this.bpPanelControl10);
			this.oneGridItem6.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem6.Location = new System.Drawing.Point(0, 115);
			this.oneGridItem6.Name = "oneGridItem6";
			this.oneGridItem6.Size = new System.Drawing.Size(1463, 23);
			this.oneGridItem6.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem6.TabIndex = 5;
			// 
			// bpPanelControl16
			// 
			this.bpPanelControl16.Controls.Add(this.ctx제품군);
			this.bpPanelControl16.Controls.Add(this.lbl제품군);
			this.bpPanelControl16.Location = new System.Drawing.Point(590, 1);
			this.bpPanelControl16.Name = "bpPanelControl16";
			this.bpPanelControl16.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl16.TabIndex = 119;
			this.bpPanelControl16.Text = "bpPanelControl16";
			// 
			// ctx제품군
			// 
			this.ctx제품군.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx제품군.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB;
			this.ctx제품군.Location = new System.Drawing.Point(106, 0);
			this.ctx제품군.Name = "ctx제품군";
			this.ctx제품군.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
			this.ctx제품군.Size = new System.Drawing.Size(186, 21);
			this.ctx제품군.TabIndex = 249;
			this.ctx제품군.TabStop = false;
			this.ctx제품군.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpControl_QueryBefore);
			// 
			// lbl제품군
			// 
			this.lbl제품군.BackColor = System.Drawing.Color.Transparent;
			this.lbl제품군.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl제품군.Location = new System.Drawing.Point(0, 0);
			this.lbl제품군.Name = "lbl제품군";
			this.lbl제품군.Size = new System.Drawing.Size(100, 23);
			this.lbl제품군.TabIndex = 248;
			this.lbl제품군.Text = "제품군";
			this.lbl제품군.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl11
			// 
			this.bpPanelControl11.Controls.Add(this.lbl공정);
			this.bpPanelControl11.Controls.Add(this.bpc공정);
			this.bpPanelControl11.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl11.Name = "bpPanelControl11";
			this.bpPanelControl11.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl11.TabIndex = 121;
			this.bpPanelControl11.Text = "bpPanelControl11";
			// 
			// lbl공정
			// 
			this.lbl공정.BackColor = System.Drawing.Color.Transparent;
			this.lbl공정.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl공정.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl공정.Location = new System.Drawing.Point(0, 0);
			this.lbl공정.Name = "lbl공정";
			this.lbl공정.Size = new System.Drawing.Size(100, 23);
			this.lbl공정.TabIndex = 113;
			this.lbl공정.Tag = "";
			this.lbl공정.Text = "공정";
			this.lbl공정.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpc공정
			// 
			this.bpc공정.Dock = System.Windows.Forms.DockStyle.Right;
			this.bpc공정.HelpID = Duzon.Common.Forms.Help.HelpID.P_PR_WCOP_SUB1;
			this.bpc공정.LabelWidth = 156;
			this.bpc공정.Location = new System.Drawing.Point(106, 0);
			this.bpc공정.Name = "bpc공정";
			this.bpc공정.Size = new System.Drawing.Size(186, 21);
			this.bpc공정.TabIndex = 118;
			this.bpc공정.TabStop = false;
			this.bpc공정.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpControl_QueryBefore);
			// 
			// bpPanelControl10
			// 
			this.bpPanelControl10.Controls.Add(this.lbl작업장);
			this.bpPanelControl10.Controls.Add(this.bpc작업장);
			this.bpPanelControl10.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl10.Name = "bpPanelControl10";
			this.bpPanelControl10.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl10.TabIndex = 120;
			this.bpPanelControl10.Text = "bpPanelControl10";
			// 
			// lbl작업장
			// 
			this.lbl작업장.BackColor = System.Drawing.Color.Transparent;
			this.lbl작업장.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl작업장.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl작업장.Location = new System.Drawing.Point(0, 0);
			this.lbl작업장.Name = "lbl작업장";
			this.lbl작업장.Size = new System.Drawing.Size(100, 23);
			this.lbl작업장.TabIndex = 113;
			this.lbl작업장.Tag = "";
			this.lbl작업장.Text = "작업장";
			this.lbl작업장.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpc작업장
			// 
			this.bpc작업장.Dock = System.Windows.Forms.DockStyle.Right;
			this.bpc작업장.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_WC_SUB1;
			this.bpc작업장.LabelWidth = 156;
			this.bpc작업장.Location = new System.Drawing.Point(106, 0);
			this.bpc작업장.Name = "bpc작업장";
			this.bpc작업장.Size = new System.Drawing.Size(186, 21);
			this.bpc작업장.TabIndex = 116;
			this.bpc작업장.TabStop = false;
			this.bpc작업장.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpControl_QueryBefore);
			// 
			// oneGridItem7
			// 
			this.oneGridItem7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem7.Controls.Add(this.bpPanelControl13);
			this.oneGridItem7.Controls.Add(this.bpPanelControl12);
			this.oneGridItem7.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem7.Location = new System.Drawing.Point(0, 138);
			this.oneGridItem7.Name = "oneGridItem7";
			this.oneGridItem7.Size = new System.Drawing.Size(1463, 23);
			this.oneGridItem7.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem7.TabIndex = 6;
			// 
			// bpPanelControl13
			// 
			this.bpPanelControl13.Controls.Add(this.cbo작업지침서등록);
			this.bpPanelControl13.Controls.Add(this.lbl작업지침서등록);
			this.bpPanelControl13.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl13.Name = "bpPanelControl13";
			this.bpPanelControl13.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl13.TabIndex = 1;
			this.bpPanelControl13.Text = "bpPanelControl13";
			// 
			// cbo작업지침서등록
			// 
			this.cbo작업지침서등록.AutoDropDown = true;
			this.cbo작업지침서등록.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo작업지침서등록.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo작업지침서등록.FormattingEnabled = true;
			this.cbo작업지침서등록.ItemHeight = 12;
			this.cbo작업지침서등록.Location = new System.Drawing.Point(106, 0);
			this.cbo작업지침서등록.Name = "cbo작업지침서등록";
			this.cbo작업지침서등록.Size = new System.Drawing.Size(186, 20);
			this.cbo작업지침서등록.TabIndex = 1;
			// 
			// lbl작업지침서등록
			// 
			this.lbl작업지침서등록.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl작업지침서등록.Location = new System.Drawing.Point(0, 0);
			this.lbl작업지침서등록.Name = "lbl작업지침서등록";
			this.lbl작업지침서등록.Size = new System.Drawing.Size(100, 23);
			this.lbl작업지침서등록.TabIndex = 0;
			this.lbl작업지침서등록.Text = "작업지침서등록";
			this.lbl작업지침서등록.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl12
			// 
			this.bpPanelControl12.Controls.Add(this.cbo측정항목등록);
			this.bpPanelControl12.Controls.Add(this.lbl측정항목등록);
			this.bpPanelControl12.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl12.Name = "bpPanelControl12";
			this.bpPanelControl12.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl12.TabIndex = 0;
			this.bpPanelControl12.Text = "bpPanelControl12";
			// 
			// cbo측정항목등록
			// 
			this.cbo측정항목등록.AutoDropDown = true;
			this.cbo측정항목등록.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo측정항목등록.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo측정항목등록.FormattingEnabled = true;
			this.cbo측정항목등록.ItemHeight = 12;
			this.cbo측정항목등록.Location = new System.Drawing.Point(106, 0);
			this.cbo측정항목등록.Name = "cbo측정항목등록";
			this.cbo측정항목등록.Size = new System.Drawing.Size(186, 20);
			this.cbo측정항목등록.TabIndex = 1;
			// 
			// lbl측정항목등록
			// 
			this.lbl측정항목등록.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl측정항목등록.Location = new System.Drawing.Point(0, 0);
			this.lbl측정항목등록.Name = "lbl측정항목등록";
			this.lbl측정항목등록.Size = new System.Drawing.Size(100, 23);
			this.lbl측정항목등록.TabIndex = 0;
			this.lbl측정항목등록.Text = "측정항목등록";
			this.lbl측정항목등록.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(3, 186);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
			this.splitContainer2.Size = new System.Drawing.Size(1473, 740);
			this.splitContainer2.SplitterDistance = 302;
			this.splitContainer2.TabIndex = 77;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this._flex품목);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this._flex공정경로);
			this.splitContainer1.Size = new System.Drawing.Size(1473, 302);
			this.splitContainer1.SplitterDistance = 991;
			this.splitContainer1.TabIndex = 75;
			// 
			// _flex품목
			// 
			this._flex품목.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex품목.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex품목.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
			this._flex품목.AutoResize = false;
			this._flex품목.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex품목.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex품목.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex품목.EnabledHeaderCheck = true;
			this._flex품목.Font = new System.Drawing.Font("굴림", 9F);
			this._flex품목.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex품목.Location = new System.Drawing.Point(0, 0);
			this._flex품목.Name = "_flex품목";
			this._flex품목.Rows.Count = 1;
			this._flex품목.Rows.DefaultSize = 20;
			this._flex품목.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex품목.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex품목.ShowSort = false;
			this._flex품목.Size = new System.Drawing.Size(991, 302);
			this._flex품목.StyleInfo = resources.GetString("_flex품목.StyleInfo");
			this._flex품목.TabIndex = 7;
			this._flex품목.UseGridCalculator = true;
			// 
			// _flex공정경로
			// 
			this._flex공정경로.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex공정경로.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex공정경로.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
			this._flex공정경로.AutoResize = false;
			this._flex공정경로.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex공정경로.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex공정경로.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex공정경로.EnabledHeaderCheck = true;
			this._flex공정경로.Font = new System.Drawing.Font("굴림", 9F);
			this._flex공정경로.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex공정경로.Location = new System.Drawing.Point(0, 0);
			this._flex공정경로.Name = "_flex공정경로";
			this._flex공정경로.Rows.Count = 1;
			this._flex공정경로.Rows.DefaultSize = 20;
			this._flex공정경로.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex공정경로.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex공정경로.ShowSort = false;
			this._flex공정경로.Size = new System.Drawing.Size(478, 302);
			this._flex공정경로.StyleInfo = resources.GetString("_flex공정경로.StyleInfo");
			this._flex공정경로.TabIndex = 6;
			this._flex공정경로.UseGridCalculator = true;
			// 
			// splitContainer3
			// 
			this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer3.Location = new System.Drawing.Point(0, 0);
			this.splitContainer3.Name = "splitContainer3";
			// 
			// splitContainer3.Panel1
			// 
			this.splitContainer3.Panel1.Controls.Add(this.tableLayoutPanel1);
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.Controls.Add(this.tabControl1);
			this.splitContainer3.Size = new System.Drawing.Size(1473, 434);
			this.splitContainer3.SplitterDistance = 825;
			this.splitContainer3.TabIndex = 0;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this._flex공정, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(825, 434);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// _flex공정
			// 
			this._flex공정.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex공정.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex공정.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex공정.AutoResize = false;
			this._flex공정.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex공정.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex공정.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex공정.EnabledHeaderCheck = true;
			this._flex공정.Font = new System.Drawing.Font("굴림", 9F);
			this._flex공정.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex공정.Location = new System.Drawing.Point(3, 34);
			this._flex공정.Name = "_flex공정";
			this._flex공정.Rows.Count = 1;
			this._flex공정.Rows.DefaultSize = 20;
			this._flex공정.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex공정.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex공정.ShowSort = false;
			this._flex공정.Size = new System.Drawing.Size(819, 397);
			this._flex공정.StyleInfo = resources.GetString("_flex공정.StyleInfo");
			this._flex공정.TabIndex = 11;
			this._flex공정.UseGridCalculator = true;
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.AutoSize = true;
			this.flowLayoutPanel2.Controls.Add(this.btn삭제);
			this.flowLayoutPanel2.Controls.Add(this.btn추가);
			this.flowLayoutPanel2.Controls.Add(this.chk공정사용여부);
			this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(822, 25);
			this.flowLayoutPanel2.TabIndex = 9;
			// 
			// btn삭제
			// 
			this.btn삭제.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn삭제.BackColor = System.Drawing.Color.White;
			this.btn삭제.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn삭제.Enabled = false;
			this.btn삭제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn삭제.Location = new System.Drawing.Point(760, 3);
			this.btn삭제.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn삭제.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn삭제.Name = "btn삭제";
			this.btn삭제.Size = new System.Drawing.Size(62, 19);
			this.btn삭제.TabIndex = 8;
			this.btn삭제.TabStop = false;
			this.btn삭제.Tag = "삭제";
			this.btn삭제.Text = "삭제";
			this.btn삭제.UseVisualStyleBackColor = false;
			// 
			// btn추가
			// 
			this.btn추가.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn추가.BackColor = System.Drawing.Color.White;
			this.btn추가.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn추가.Enabled = false;
			this.btn추가.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn추가.Location = new System.Drawing.Point(695, 3);
			this.btn추가.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn추가.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn추가.Name = "btn추가";
			this.btn추가.Size = new System.Drawing.Size(62, 19);
			this.btn추가.TabIndex = 7;
			this.btn추가.TabStop = false;
			this.btn추가.Tag = "추가";
			this.btn추가.Text = "추가";
			this.btn추가.UseVisualStyleBackColor = false;
			// 
			// chk공정사용여부
			// 
			this.chk공정사용여부.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chk공정사용여부.AutoSize = true;
			this.chk공정사용여부.Checked = true;
			this.chk공정사용여부.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk공정사용여부.Location = new System.Drawing.Point(605, 3);
			this.chk공정사용여부.Name = "chk공정사용여부";
			this.chk공정사용여부.Size = new System.Drawing.Size(84, 16);
			this.chk공정사용여부.TabIndex = 11;
			this.chk공정사용여부.Text = "사용항목만";
			this.chk공정사용여부.TextDD = null;
			this.chk공정사용여부.UseVisualStyleBackColor = true;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tpg작업지침서);
			this.tabControl1.Controls.Add(this.tpg측정항목);
			this.tabControl1.Controls.Add(this.tpg작업내용);
			this.tabControl1.Controls.Add(this.tpg설비);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(644, 434);
			this.tabControl1.TabIndex = 0;
			// 
			// tpg작업지침서
			// 
			this.tpg작업지침서.Controls.Add(this.imagePanel2);
			this.tpg작업지침서.Location = new System.Drawing.Point(4, 22);
			this.tpg작업지침서.Name = "tpg작업지침서";
			this.tpg작업지침서.Padding = new System.Windows.Forms.Padding(3);
			this.tpg작업지침서.Size = new System.Drawing.Size(636, 408);
			this.tpg작업지침서.TabIndex = 0;
			this.tpg작업지침서.Text = "작업지침서";
			this.tpg작업지침서.UseVisualStyleBackColor = true;
			// 
			// imagePanel2
			// 
			this.imagePanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.imagePanel2.Controls.Add(this.splitContainer4);
			this.imagePanel2.Controls.Add(this.btn작업지침서미리보기);
			this.imagePanel2.Controls.Add(this.btn작업지침서열기);
			this.imagePanel2.Controls.Add(this.btn작업지침서삭제);
			this.imagePanel2.Controls.Add(this.btn작업지침서추가);
			this.imagePanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imagePanel2.LeftImage = null;
			this.imagePanel2.Location = new System.Drawing.Point(3, 3);
			this.imagePanel2.Name = "imagePanel2";
			this.imagePanel2.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.imagePanel2.PatternImage = null;
			this.imagePanel2.RightImage = null;
			this.imagePanel2.Size = new System.Drawing.Size(630, 402);
			this.imagePanel2.TabIndex = 1;
			this.imagePanel2.TitleText = "작업지침서";
			// 
			// splitContainer4
			// 
			this.splitContainer4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer4.Location = new System.Drawing.Point(3, 28);
			this.splitContainer4.Name = "splitContainer4";
			this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer4.Panel1
			// 
			this.splitContainer4.Panel1.Controls.Add(this._flex작업지침서);
			// 
			// splitContainer4.Panel2
			// 
			this.splitContainer4.Panel2.Controls.Add(this.web미리보기);
			this.splitContainer4.Size = new System.Drawing.Size(624, 371);
			this.splitContainer4.SplitterDistance = 178;
			this.splitContainer4.TabIndex = 9;
			// 
			// _flex작업지침서
			// 
			this._flex작업지침서.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex작업지침서.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex작업지침서.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex작업지침서.AutoResize = false;
			this._flex작업지침서.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex작업지침서.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex작업지침서.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex작업지침서.EnabledHeaderCheck = true;
			this._flex작업지침서.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex작업지침서.Location = new System.Drawing.Point(0, 0);
			this._flex작업지침서.Name = "_flex작업지침서";
			this._flex작업지침서.Rows.Count = 1;
			this._flex작업지침서.Rows.DefaultSize = 20;
			this._flex작업지침서.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex작업지침서.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex작업지침서.ShowSort = false;
			this._flex작업지침서.Size = new System.Drawing.Size(624, 178);
			this._flex작업지침서.StyleInfo = resources.GetString("_flex작업지침서.StyleInfo");
			this._flex작업지침서.TabIndex = 8;
			this._flex작업지침서.UseGridCalculator = true;
			// 
			// web미리보기
			// 
			this.web미리보기.Dock = System.Windows.Forms.DockStyle.Fill;
			this.web미리보기.Location = new System.Drawing.Point(0, 0);
			this.web미리보기.MinimumSize = new System.Drawing.Size(20, 20);
			this.web미리보기.Name = "web미리보기";
			this.web미리보기.Size = new System.Drawing.Size(624, 189);
			this.web미리보기.TabIndex = 2;
			// 
			// btn작업지침서미리보기
			// 
			this.btn작업지침서미리보기.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn작업지침서미리보기.BackColor = System.Drawing.Color.Transparent;
			this.btn작업지침서미리보기.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn작업지침서미리보기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn작업지침서미리보기.ForeColor = System.Drawing.Color.Transparent;
			this.btn작업지침서미리보기.Location = new System.Drawing.Point(557, 3);
			this.btn작업지침서미리보기.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn작업지침서미리보기.Name = "btn작업지침서미리보기";
			this.btn작업지침서미리보기.Size = new System.Drawing.Size(70, 19);
			this.btn작업지침서미리보기.TabIndex = 7;
			this.btn작업지침서미리보기.TabStop = false;
			this.btn작업지침서미리보기.Text = "미리보기";
			this.btn작업지침서미리보기.UseVisualStyleBackColor = false;
			// 
			// btn작업지침서열기
			// 
			this.btn작업지침서열기.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn작업지침서열기.BackColor = System.Drawing.Color.Transparent;
			this.btn작업지침서열기.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn작업지침서열기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn작업지침서열기.ForeColor = System.Drawing.Color.Transparent;
			this.btn작업지침서열기.Location = new System.Drawing.Point(481, 3);
			this.btn작업지침서열기.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn작업지침서열기.Name = "btn작업지침서열기";
			this.btn작업지침서열기.Size = new System.Drawing.Size(70, 19);
			this.btn작업지침서열기.TabIndex = 6;
			this.btn작업지침서열기.TabStop = false;
			this.btn작업지침서열기.Text = "열기";
			this.btn작업지침서열기.UseVisualStyleBackColor = false;
			// 
			// btn작업지침서삭제
			// 
			this.btn작업지침서삭제.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn작업지침서삭제.BackColor = System.Drawing.Color.Transparent;
			this.btn작업지침서삭제.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn작업지침서삭제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn작업지침서삭제.ForeColor = System.Drawing.Color.Transparent;
			this.btn작업지침서삭제.Location = new System.Drawing.Point(405, 3);
			this.btn작업지침서삭제.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn작업지침서삭제.Name = "btn작업지침서삭제";
			this.btn작업지침서삭제.Size = new System.Drawing.Size(70, 19);
			this.btn작업지침서삭제.TabIndex = 5;
			this.btn작업지침서삭제.TabStop = false;
			this.btn작업지침서삭제.Text = "삭제";
			this.btn작업지침서삭제.UseVisualStyleBackColor = false;
			// 
			// btn작업지침서추가
			// 
			this.btn작업지침서추가.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn작업지침서추가.BackColor = System.Drawing.Color.Transparent;
			this.btn작업지침서추가.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn작업지침서추가.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn작업지침서추가.ForeColor = System.Drawing.Color.Transparent;
			this.btn작업지침서추가.Location = new System.Drawing.Point(329, 3);
			this.btn작업지침서추가.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn작업지침서추가.Name = "btn작업지침서추가";
			this.btn작업지침서추가.Size = new System.Drawing.Size(70, 19);
			this.btn작업지침서추가.TabIndex = 4;
			this.btn작업지침서추가.TabStop = false;
			this.btn작업지침서추가.Text = "추가";
			this.btn작업지침서추가.UseVisualStyleBackColor = false;
			// 
			// tpg측정항목
			// 
			this.tpg측정항목.Controls.Add(this.imagePanel3);
			this.tpg측정항목.Location = new System.Drawing.Point(4, 22);
			this.tpg측정항목.Name = "tpg측정항목";
			this.tpg측정항목.Size = new System.Drawing.Size(636, 408);
			this.tpg측정항목.TabIndex = 2;
			this.tpg측정항목.Text = "측정항목";
			this.tpg측정항목.UseVisualStyleBackColor = true;
			// 
			// imagePanel3
			// 
			this.imagePanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.imagePanel3.Controls.Add(this.chk측정항목사용여부);
			this.imagePanel3.Controls.Add(this.btn측정항목저장);
			this.imagePanel3.Controls.Add(this.btn측정항목삭제);
			this.imagePanel3.Controls.Add(this.btn측정항목추가);
			this.imagePanel3.Controls.Add(this._flex측정항목);
			this.imagePanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imagePanel3.LeftImage = null;
			this.imagePanel3.Location = new System.Drawing.Point(0, 0);
			this.imagePanel3.Name = "imagePanel3";
			this.imagePanel3.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.imagePanel3.PatternImage = null;
			this.imagePanel3.RightImage = null;
			this.imagePanel3.Size = new System.Drawing.Size(636, 408);
			this.imagePanel3.TabIndex = 1;
			this.imagePanel3.TitleText = "측정항목";
			// 
			// chk측정항목사용여부
			// 
			this.chk측정항목사용여부.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chk측정항목사용여부.AutoSize = true;
			this.chk측정항목사용여부.Checked = true;
			this.chk측정항목사용여부.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk측정항목사용여부.Location = new System.Drawing.Point(321, 6);
			this.chk측정항목사용여부.Name = "chk측정항목사용여부";
			this.chk측정항목사용여부.Size = new System.Drawing.Size(84, 16);
			this.chk측정항목사용여부.TabIndex = 10;
			this.chk측정항목사용여부.Text = "사용항목만";
			this.chk측정항목사용여부.TextDD = null;
			this.chk측정항목사용여부.UseVisualStyleBackColor = true;
			// 
			// btn측정항목저장
			// 
			this.btn측정항목저장.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn측정항목저장.BackColor = System.Drawing.Color.Transparent;
			this.btn측정항목저장.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn측정항목저장.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn측정항목저장.Location = new System.Drawing.Point(561, 4);
			this.btn측정항목저장.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn측정항목저장.Name = "btn측정항목저장";
			this.btn측정항목저장.Size = new System.Drawing.Size(69, 19);
			this.btn측정항목저장.TabIndex = 9;
			this.btn측정항목저장.TabStop = false;
			this.btn측정항목저장.Text = "저장";
			this.btn측정항목저장.UseVisualStyleBackColor = false;
			// 
			// btn측정항목삭제
			// 
			this.btn측정항목삭제.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn측정항목삭제.BackColor = System.Drawing.Color.Transparent;
			this.btn측정항목삭제.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn측정항목삭제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn측정항목삭제.Location = new System.Drawing.Point(486, 4);
			this.btn측정항목삭제.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn측정항목삭제.Name = "btn측정항목삭제";
			this.btn측정항목삭제.Size = new System.Drawing.Size(69, 19);
			this.btn측정항목삭제.TabIndex = 7;
			this.btn측정항목삭제.TabStop = false;
			this.btn측정항목삭제.Text = "삭제";
			this.btn측정항목삭제.UseVisualStyleBackColor = false;
			// 
			// btn측정항목추가
			// 
			this.btn측정항목추가.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn측정항목추가.BackColor = System.Drawing.Color.Transparent;
			this.btn측정항목추가.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn측정항목추가.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn측정항목추가.Location = new System.Drawing.Point(411, 4);
			this.btn측정항목추가.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn측정항목추가.Name = "btn측정항목추가";
			this.btn측정항목추가.Size = new System.Drawing.Size(69, 19);
			this.btn측정항목추가.TabIndex = 8;
			this.btn측정항목추가.TabStop = false;
			this.btn측정항목추가.Text = "추가";
			this.btn측정항목추가.UseVisualStyleBackColor = false;
			// 
			// _flex측정항목
			// 
			this._flex측정항목.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex측정항목.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex측정항목.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex측정항목.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._flex측정항목.AutoResize = false;
			this._flex측정항목.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex측정항목.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex측정항목.EnabledHeaderCheck = true;
			this._flex측정항목.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex측정항목.Location = new System.Drawing.Point(3, 28);
			this._flex측정항목.Name = "_flex측정항목";
			this._flex측정항목.Rows.Count = 1;
			this._flex측정항목.Rows.DefaultSize = 20;
			this._flex측정항목.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex측정항목.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex측정항목.ShowSort = false;
			this._flex측정항목.Size = new System.Drawing.Size(630, 377);
			this._flex측정항목.StyleInfo = resources.GetString("_flex측정항목.StyleInfo");
			this._flex측정항목.TabIndex = 1;
			this._flex측정항목.UseGridCalculator = true;
			// 
			// tpg작업내용
			// 
			this.tpg작업내용.Controls.Add(this.imagePanel1);
			this.tpg작업내용.Location = new System.Drawing.Point(4, 22);
			this.tpg작업내용.Name = "tpg작업내용";
			this.tpg작업내용.Padding = new System.Windows.Forms.Padding(3);
			this.tpg작업내용.Size = new System.Drawing.Size(636, 408);
			this.tpg작업내용.TabIndex = 1;
			this.tpg작업내용.Text = "작업내용";
			this.tpg작업내용.UseVisualStyleBackColor = true;
			// 
			// imagePanel1
			// 
			this.imagePanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.imagePanel1.Controls.Add(this.panelExt1);
			this.imagePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imagePanel1.LeftImage = null;
			this.imagePanel1.Location = new System.Drawing.Point(3, 3);
			this.imagePanel1.Name = "imagePanel1";
			this.imagePanel1.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.imagePanel1.PatternImage = null;
			this.imagePanel1.RightImage = null;
			this.imagePanel1.Size = new System.Drawing.Size(630, 402);
			this.imagePanel1.TabIndex = 0;
			this.imagePanel1.TitleText = "작업내용";
			// 
			// panelExt1
			// 
			this.panelExt1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelExt1.Controls.Add(this.txt작업내용);
			this.panelExt1.Location = new System.Drawing.Point(3, 28);
			this.panelExt1.Name = "panelExt1";
			this.panelExt1.Size = new System.Drawing.Size(624, 371);
			this.panelExt1.TabIndex = 0;
			// 
			// txt작업내용
			// 
			this.txt작업내용.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt작업내용.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt작업내용.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txt작업내용.Location = new System.Drawing.Point(0, 0);
			this.txt작업내용.Multiline = true;
			this.txt작업내용.Name = "txt작업내용";
			this.txt작업내용.Size = new System.Drawing.Size(624, 371);
			this.txt작업내용.TabIndex = 1;
			this.txt작업내용.Tag = "DC_OP";
			this.txt작업내용.UseKeyEnter = false;
			// 
			// tpg설비
			// 
			this.tpg설비.Controls.Add(this.imagePanel4);
			this.tpg설비.Location = new System.Drawing.Point(4, 22);
			this.tpg설비.Name = "tpg설비";
			this.tpg설비.Size = new System.Drawing.Size(636, 408);
			this.tpg설비.TabIndex = 3;
			this.tpg설비.Text = "설비";
			this.tpg설비.UseVisualStyleBackColor = true;
			// 
			// imagePanel4
			// 
			this.imagePanel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.imagePanel4.Controls.Add(this._flex설비);
			this.imagePanel4.Controls.Add(this.btn설비저장);
			this.imagePanel4.Controls.Add(this.btn설비삭제);
			this.imagePanel4.Controls.Add(this.btn설비추가);
			this.imagePanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imagePanel4.LeftImage = null;
			this.imagePanel4.Location = new System.Drawing.Point(0, 0);
			this.imagePanel4.Name = "imagePanel4";
			this.imagePanel4.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.imagePanel4.PatternImage = null;
			this.imagePanel4.RightImage = null;
			this.imagePanel4.Size = new System.Drawing.Size(636, 408);
			this.imagePanel4.TabIndex = 2;
			this.imagePanel4.TitleText = "설비";
			// 
			// _flex설비
			// 
			this._flex설비.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex설비.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex설비.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex설비.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._flex설비.AutoResize = false;
			this._flex설비.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex설비.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex설비.EnabledHeaderCheck = true;
			this._flex설비.Font = new System.Drawing.Font("굴림", 9F);
			this._flex설비.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex설비.Location = new System.Drawing.Point(3, 28);
			this._flex설비.Name = "_flex설비";
			this._flex설비.Rows.Count = 1;
			this._flex설비.Rows.DefaultSize = 20;
			this._flex설비.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex설비.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex설비.ShowSort = false;
			this._flex설비.Size = new System.Drawing.Size(630, 377);
			this._flex설비.StyleInfo = resources.GetString("_flex설비.StyleInfo");
			this._flex설비.TabIndex = 13;
			this._flex설비.UseGridCalculator = true;
			// 
			// btn설비저장
			// 
			this.btn설비저장.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn설비저장.BackColor = System.Drawing.Color.Transparent;
			this.btn설비저장.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn설비저장.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn설비저장.Location = new System.Drawing.Point(561, 4);
			this.btn설비저장.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn설비저장.Name = "btn설비저장";
			this.btn설비저장.Size = new System.Drawing.Size(69, 19);
			this.btn설비저장.TabIndex = 9;
			this.btn설비저장.TabStop = false;
			this.btn설비저장.Text = "저장";
			this.btn설비저장.UseVisualStyleBackColor = false;
			// 
			// btn설비삭제
			// 
			this.btn설비삭제.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn설비삭제.BackColor = System.Drawing.Color.Transparent;
			this.btn설비삭제.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn설비삭제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn설비삭제.Location = new System.Drawing.Point(486, 4);
			this.btn설비삭제.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn설비삭제.Name = "btn설비삭제";
			this.btn설비삭제.Size = new System.Drawing.Size(69, 19);
			this.btn설비삭제.TabIndex = 7;
			this.btn설비삭제.TabStop = false;
			this.btn설비삭제.Text = "삭제";
			this.btn설비삭제.UseVisualStyleBackColor = false;
			// 
			// btn설비추가
			// 
			this.btn설비추가.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn설비추가.BackColor = System.Drawing.Color.Transparent;
			this.btn설비추가.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn설비추가.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn설비추가.Location = new System.Drawing.Point(411, 4);
			this.btn설비추가.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn설비추가.Name = "btn설비추가";
			this.btn설비추가.Size = new System.Drawing.Size(69, 19);
			this.btn설비추가.TabIndex = 8;
			this.btn설비추가.TabStop = false;
			this.btn설비추가.Text = "추가";
			this.btn설비추가.UseVisualStyleBackColor = false;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanel1.Controls.Add(this.btn측정항목붙여넣기);
			this.flowLayoutPanel1.Controls.Add(this.btn측정항목복사);
			this.flowLayoutPanel1.Controls.Add(this.btn측정항목);
			this.flowLayoutPanel1.Controls.Add(this.btn붙여넣기);
			this.flowLayoutPanel1.Controls.Add(this.btn복사하기);
			this.flowLayoutPanel1.Controls.Add(this.btn배치그룹관리);
			this.flowLayoutPanel1.Controls.Add(this.btnAPS연동);
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(766, 10);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(717, 23);
			this.flowLayoutPanel1.TabIndex = 8;
			// 
			// btn측정항목붙여넣기
			// 
			this.btn측정항목붙여넣기.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn측정항목붙여넣기.BackColor = System.Drawing.Color.White;
			this.btn측정항목붙여넣기.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn측정항목붙여넣기.Enabled = false;
			this.btn측정항목붙여넣기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn측정항목붙여넣기.Location = new System.Drawing.Point(608, 3);
			this.btn측정항목붙여넣기.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn측정항목붙여넣기.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn측정항목붙여넣기.Name = "btn측정항목붙여넣기";
			this.btn측정항목붙여넣기.Size = new System.Drawing.Size(109, 19);
			this.btn측정항목붙여넣기.TabIndex = 7;
			this.btn측정항목붙여넣기.TabStop = false;
			this.btn측정항목붙여넣기.Tag = "복사";
			this.btn측정항목붙여넣기.Text = "측정항목붙여넣기";
			this.btn측정항목붙여넣기.UseVisualStyleBackColor = false;
			// 
			// btn측정항목복사
			// 
			this.btn측정항목복사.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn측정항목복사.BackColor = System.Drawing.Color.White;
			this.btn측정항목복사.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn측정항목복사.Enabled = false;
			this.btn측정항목복사.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn측정항목복사.Location = new System.Drawing.Point(516, 3);
			this.btn측정항목복사.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn측정항목복사.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn측정항목복사.Name = "btn측정항목복사";
			this.btn측정항목복사.Size = new System.Drawing.Size(89, 19);
			this.btn측정항목복사.TabIndex = 8;
			this.btn측정항목복사.TabStop = false;
			this.btn측정항목복사.Tag = "복사";
			this.btn측정항목복사.Text = "측정항목복사";
			this.btn측정항목복사.UseVisualStyleBackColor = false;
			// 
			// btn측정항목
			// 
			this.btn측정항목.BackColor = System.Drawing.Color.Transparent;
			this.btn측정항목.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn측정항목.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn측정항목.Location = new System.Drawing.Point(420, 3);
			this.btn측정항목.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn측정항목.Name = "btn측정항목";
			this.btn측정항목.Size = new System.Drawing.Size(90, 19);
			this.btn측정항목.TabIndex = 11;
			this.btn측정항목.TabStop = false;
			this.btn측정항목.Text = "측정항목";
			this.btn측정항목.UseVisualStyleBackColor = false;
			// 
			// btn배치그룹관리
			// 
			this.btn배치그룹관리.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn배치그룹관리.BackColor = System.Drawing.Color.White;
			this.btn배치그룹관리.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn배치그룹관리.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn배치그룹관리.Location = new System.Drawing.Point(167, 3);
			this.btn배치그룹관리.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn배치그룹관리.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn배치그룹관리.Name = "btn배치그룹관리";
			this.btn배치그룹관리.Size = new System.Drawing.Size(89, 19);
			this.btn배치그룹관리.TabIndex = 12;
			this.btn배치그룹관리.TabStop = false;
			this.btn배치그룹관리.Tag = "복사";
			this.btn배치그룹관리.Text = "배치그룹관리";
			this.btn배치그룹관리.UseVisualStyleBackColor = false;
			// 
			// btnAPS연동
			// 
			this.btnAPS연동.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAPS연동.BackColor = System.Drawing.Color.White;
			this.btnAPS연동.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnAPS연동.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnAPS연동.Location = new System.Drawing.Point(75, 3);
			this.btnAPS연동.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btnAPS연동.MaximumSize = new System.Drawing.Size(0, 19);
			this.btnAPS연동.Name = "btnAPS연동";
			this.btnAPS연동.Size = new System.Drawing.Size(89, 19);
			this.btnAPS연동.TabIndex = 13;
			this.btnAPS연동.TabStop = false;
			this.btnAPS연동.Tag = "복사";
			this.btnAPS연동.Text = "APS연동";
			this.btnAPS연동.UseVisualStyleBackColor = false;
			// 
			// P_CZ_PR_ROUT_REG
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Controls.Add(this.flowLayoutPanel1);
			this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.Name = "P_CZ_PR_ROUT_REG";
			this.Size = new System.Drawing.Size(1479, 969);
			this.TitleText = "공정경로등록";
			this.Controls.SetChildIndex(this.flowLayoutPanel1, 0);
			this.Controls.SetChildIndex(this.mDataArea, 0);
			this.mDataArea.ResumeLayout(false);
			this.mDataArea.PerformLayout();
			this._tlayMain.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl2.ResumeLayout(false);
			this.bpPnl_plant.ResumeLayout(false);
			this.oneGridItem2.ResumeLayout(false);
			this.bpPanelControl9.ResumeLayout(false);
			this.bpPanelControl3.ResumeLayout(false);
			this.oneGridItem3.ResumeLayout(false);
			this.bpPanelControl5.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.bpPanelControl4.ResumeLayout(false);
			this.oneGridItem4.ResumeLayout(false);
			this.bpPnl_radioButton.ResumeLayout(false);
			this.flowLayoutPanel3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.rdo전체조회)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo등록된품목만조회)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo등록되지않은품목만조회)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo작업장미등록품목조회)).EndInit();
			this.oneGridItem5.ResumeLayout(false);
			this.bpPanelControl8.ResumeLayout(false);
			this.bpPanelControl7.ResumeLayout(false);
			this.bpPanelControl6.ResumeLayout(false);
			this.oneGridItem6.ResumeLayout(false);
			this.bpPanelControl16.ResumeLayout(false);
			this.bpPanelControl11.ResumeLayout(false);
			this.bpPanelControl10.ResumeLayout(false);
			this.oneGridItem7.ResumeLayout(false);
			this.bpPanelControl13.ResumeLayout(false);
			this.bpPanelControl12.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex품목)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._flex공정경로)).EndInit();
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
			this.splitContainer3.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex공정)).EndInit();
			this.flowLayoutPanel2.ResumeLayout(false);
			this.flowLayoutPanel2.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tpg작업지침서.ResumeLayout(false);
			this.imagePanel2.ResumeLayout(false);
			this.splitContainer4.Panel1.ResumeLayout(false);
			this.splitContainer4.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
			this.splitContainer4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex작업지침서)).EndInit();
			this.tpg측정항목.ResumeLayout(false);
			this.imagePanel3.ResumeLayout(false);
			this.imagePanel3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex측정항목)).EndInit();
			this.tpg작업내용.ResumeLayout(false);
			this.imagePanel1.ResumeLayout(false);
			this.panelExt1.ResumeLayout(false);
			this.panelExt1.PerformLayout();
			this.tpg설비.ResumeLayout(false);
			this.imagePanel4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex설비)).EndInit();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        private RoundedButton btn붙여넣기;
        private RoundedButton btn복사하기;
        private TableLayoutPanel _tlayMain;
        private DropDownComboBox cbo공정경로유형;
        private LabelExt lbl공정경로유형;
        private BpCodeNTextBox ctx품목To;
        private BpCodeNTextBox ctx품목From;
        private DropDownComboBox cbo공장;
        private DropDownComboBox cbo품목계정;
        private LabelExt lbl품목계정;
        private LabelExt lbl공장;
        private RoundedButton btn추가;
        private RoundedButton btn삭제;
        private SplitContainer splitContainer1;
        private Dass.FlexGrid.FlexGrid _flex품목;
        private Dass.FlexGrid.FlexGrid _flex공정경로;
        private OneGrid oneGrid1;
        private FlowLayoutPanel flowLayoutPanel2;
        private OneGridItem oneGridItem1;
        private BpPanelControl bpPanelControl2;
        private BpPanelControl bpPnl_plant;
        private OneGridItem oneGridItem2;
        private OneGridItem oneGridItem3;
        private BpPanelControl bpPanelControl4;
        private FlowLayoutPanel flowLayoutPanel1;
        private RadioButtonExt rdo작업장미등록품목조회;
        private OneGridItem oneGridItem4;
        private BpPanelControl bpPanelControl1;
        private LabelExt lbl조달구분;
        private DropDownComboBox cbo조달구분;
        private BpPanelControl bpPnl_radioButton;
        private RadioButtonExt rdo전체조회;
        private RadioButtonExt rdo등록되지않은품목만조회;
        private RadioButtonExt rdo등록된품목만조회;
        private BpPanelControl bpPanelControl5;
        private LabelExt lbl품목군S;
        private BpCodeTextBox ctx품목군S;
        private OneGridItem oneGridItem5;
        private BpPanelControl bpPanelControl8;
        private BpPanelControl bpPanelControl7;
        private BpPanelControl bpPanelControl6;
        private BpCodeTextBox ctx대분류;
        private LabelExt lbl대분류;
        private BpCodeTextBox ctx중분류;
        private LabelExt lbl중분류;
        private BpCodeTextBox ctx소분류;
        private LabelExt lbl소분류;
        private OneGridItem oneGridItem6;
        private BpComboBox bpc작업장;
        private BpComboBox bpc공정;
        private SplitContainer splitContainer2;
        private TableLayoutPanel tableLayoutPanel1;
        private BpPanelControl bpPanelControl16;
        private BpCodeTextBox ctx제품군;
        private LabelExt lbl제품군;

		#endregion

		private BpPanelControl bpPanelControl9;
		private LabelExt lbl품목To;
		private BpPanelControl bpPanelControl3;
		private LabelExt lbl품목From;
		private FlowLayoutPanel flowLayoutPanel3;
		private BpPanelControl bpPanelControl11;
		private LabelExt lbl공정;
		private BpPanelControl bpPanelControl10;
		private LabelExt lbl작업장;
		private SplitContainer splitContainer3;
		private TabControl tabControl1;
		private TabPage tpg작업지침서;
		private TabPage tpg작업내용;
		private ImagePanel imagePanel2;
		private RoundedButton btn작업지침서미리보기;
		private RoundedButton btn작업지침서열기;
		private RoundedButton btn작업지침서삭제;
		private RoundedButton btn작업지침서추가;
		private SplitContainer splitContainer4;
		private Dass.FlexGrid.FlexGrid _flex작업지침서;
		private WebBrowserExt web미리보기;
		private ImagePanel imagePanel1;
		private PanelExt panelExt1;
		private TextBoxExt txt작업내용;
		private OneGridItem oneGridItem7;
		private BpPanelControl bpPanelControl13;
		private LabelExt lbl작업지침서등록;
		private BpPanelControl bpPanelControl12;
		private LabelExt lbl측정항목등록;
		private DropDownComboBox cbo작업지침서등록;
		private DropDownComboBox cbo측정항목등록;
		private RoundedButton btn측정항목붙여넣기;
		private RoundedButton btn측정항목복사;
		private TabPage tpg측정항목;
		private ImagePanel imagePanel3;
		private Dass.FlexGrid.FlexGrid _flex측정항목;
		private RoundedButton btn측정항목삭제;
		private RoundedButton btn측정항목추가;
		private RoundedButton btn측정항목저장;
		private RoundedButton btn측정항목;
		private CheckBoxExt chk측정항목사용여부;
		private CheckBoxExt chk공정사용여부;
		private Dass.FlexGrid.FlexGrid _flex공정;
		private TabPage tpg설비;
		private ImagePanel imagePanel4;
		private Dass.FlexGrid.FlexGrid _flex설비;
		private RoundedButton btn설비저장;
		private RoundedButton btn설비삭제;
		private RoundedButton btn설비추가;
		private RoundedButton btn배치그룹관리;
		private RoundedButton btnAPS연동;
	}
}

namespace cz
{
	partial class P_CZ_PR_CFG_PLANT
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_PR_CFG_PLANT));
			this._tlayMain = new System.Windows.Forms.TableLayoutPanel();
			this.oneGrid2 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem5 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpP_Plant = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl공장 = new Duzon.Common.Controls.LabelExt();
			this.cbo공장 = new Duzon.Common.Controls.DropDownComboBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this._flexM = new Dass.FlexGrid.FlexGrid(this.components);
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.oneGridItem17 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btn삭제 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn추가 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl캘린더코드 = new Duzon.Common.Controls.LabelExt();
			this.cbo캘린더코드 = new Duzon.Common.Controls.DropDownComboBox();
			this.mDataArea.SuspendLayout();
			this._tlayMain.SuspendLayout();
			this.oneGridItem5.SuspendLayout();
			this.bpP_Plant.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flexM)).BeginInit();
			this.flowLayoutPanel1.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this._tlayMain);
			this.mDataArea.Size = new System.Drawing.Size(1060, 756);
			// 
			// _tlayMain
			// 
			this._tlayMain.AutoSize = true;
			this._tlayMain.ColumnCount = 1;
			this._tlayMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this._tlayMain.Controls.Add(this.oneGrid2, 0, 0);
			this._tlayMain.Controls.Add(this.tableLayoutPanel1, 0, 1);
			this._tlayMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this._tlayMain.Location = new System.Drawing.Point(0, 0);
			this._tlayMain.Name = "_tlayMain";
			this._tlayMain.RowCount = 2;
			this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this._tlayMain.Size = new System.Drawing.Size(1060, 756);
			this._tlayMain.TabIndex = 111;
			// 
			// oneGrid2
			// 
			this.oneGrid2.Dock = System.Windows.Forms.DockStyle.Top;
			this.oneGrid2.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem5});
			this.oneGrid2.Location = new System.Drawing.Point(3, 3);
			this.oneGrid2.Name = "oneGrid2";
			this.oneGrid2.Size = new System.Drawing.Size(1054, 42);
			this.oneGrid2.TabIndex = 108;
			// 
			// oneGridItem5
			// 
			this.oneGridItem5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem5.Controls.Add(this.bpPanelControl1);
			this.oneGridItem5.Controls.Add(this.bpP_Plant);
			this.oneGridItem5.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem5.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem5.Name = "oneGridItem5";
			this.oneGridItem5.Size = new System.Drawing.Size(1044, 23);
			this.oneGridItem5.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem5.TabIndex = 0;
			// 
			// bpP_Plant
			// 
			this.bpP_Plant.Controls.Add(this.lbl공장);
			this.bpP_Plant.Controls.Add(this.cbo공장);
			this.bpP_Plant.Location = new System.Drawing.Point(2, 1);
			this.bpP_Plant.Name = "bpP_Plant";
			this.bpP_Plant.Size = new System.Drawing.Size(292, 23);
			this.bpP_Plant.TabIndex = 4;
			this.bpP_Plant.Text = "bpPanelControl2";
			// 
			// lbl공장
			// 
			this.lbl공장.BackColor = System.Drawing.Color.Transparent;
			this.lbl공장.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl공장.Location = new System.Drawing.Point(0, 0);
			this.lbl공장.Name = "lbl공장";
			this.lbl공장.Size = new System.Drawing.Size(100, 23);
			this.lbl공장.TabIndex = 1;
			this.lbl공장.Tag = "공장";
			this.lbl공장.Text = "공장";
			this.lbl공장.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo공장
			// 
			this.cbo공장.AutoDropDown = false;
			this.cbo공장.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(242)))));
			this.cbo공장.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo공장.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo공장.ItemHeight = 12;
			this.cbo공장.Location = new System.Drawing.Point(107, 0);
			this.cbo공장.Name = "cbo공장";
			this.cbo공장.Size = new System.Drawing.Size(185, 20);
			this.cbo공장.TabIndex = 1;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this._flexM, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 51);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1054, 702);
			this.tableLayoutPanel1.TabIndex = 109;
			// 
			// _flexM
			// 
			this._flexM.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flexM.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flexM.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
			this._flexM.AutoResize = false;
			this._flexM.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flexM.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flexM.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flexM.EnabledHeaderCheck = true;
			this._flexM.Font = new System.Drawing.Font("굴림", 9F);
			this._flexM.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flexM.Location = new System.Drawing.Point(3, 37);
			this._flexM.Name = "_flexM";
			this._flexM.Rows.Count = 1;
			this._flexM.Rows.DefaultSize = 20;
			this._flexM.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flexM.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flexM.ShowSort = false;
			this._flexM.Size = new System.Drawing.Size(1048, 662);
			this._flexM.StyleInfo = resources.GetString("_flexM.StyleInfo");
			this._flexM.TabIndex = 5;
			this._flexM.UseGridCalculator = true;
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// oneGridItem17
			// 
			this.oneGridItem17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem17.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
			this.oneGridItem17.Location = new System.Drawing.Point(0, 253);
			this.oneGridItem17.Name = "oneGridItem17";
			this.oneGridItem17.Size = new System.Drawing.Size(765, 23);
			this.oneGridItem17.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem17.TabIndex = 11;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
			this.flowLayoutPanel1.Controls.Add(this.btn삭제);
			this.flowLayoutPanel1.Controls.Add(this.btn추가);
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.ForeColor = System.Drawing.Color.Transparent;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(855, 3);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(196, 28);
			this.flowLayoutPanel1.TabIndex = 7;
			// 
			// btn삭제
			// 
			this.btn삭제.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn삭제.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.btn삭제.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn삭제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn삭제.Location = new System.Drawing.Point(134, 1);
			this.btn삭제.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.btn삭제.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn삭제.Name = "btn삭제";
			this.btn삭제.Size = new System.Drawing.Size(62, 19);
			this.btn삭제.TabIndex = 0;
			this.btn삭제.TabStop = false;
			this.btn삭제.Tag = "삭제";
			this.btn삭제.Text = "삭제";
			this.btn삭제.UseVisualStyleBackColor = false;
			// 
			// btn추가
			// 
			this.btn추가.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn추가.BackColor = System.Drawing.Color.Transparent;
			this.btn추가.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn추가.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn추가.Location = new System.Drawing.Point(69, 1);
			this.btn추가.Margin = new System.Windows.Forms.Padding(0, 1, 3, 0);
			this.btn추가.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn추가.Name = "btn추가";
			this.btn추가.Size = new System.Drawing.Size(62, 19);
			this.btn추가.TabIndex = 0;
			this.btn추가.TabStop = false;
			this.btn추가.Tag = "추가";
			this.btn추가.Text = "추가";
			this.btn추가.UseVisualStyleBackColor = false;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.lbl캘린더코드);
			this.bpPanelControl1.Controls.Add(this.cbo캘린더코드);
			this.bpPanelControl1.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl1.TabIndex = 5;
			this.bpPanelControl1.Text = "bpPanelControl2";
			// 
			// lbl캘린더코드
			// 
			this.lbl캘린더코드.BackColor = System.Drawing.Color.Transparent;
			this.lbl캘린더코드.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl캘린더코드.Location = new System.Drawing.Point(0, 0);
			this.lbl캘린더코드.Name = "lbl캘린더코드";
			this.lbl캘린더코드.Size = new System.Drawing.Size(100, 23);
			this.lbl캘린더코드.TabIndex = 1;
			this.lbl캘린더코드.Tag = "공장";
			this.lbl캘린더코드.Text = "캘린더코드";
			this.lbl캘린더코드.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo캘린더코드
			// 
			this.cbo캘린더코드.AutoDropDown = false;
			this.cbo캘린더코드.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(242)))));
			this.cbo캘린더코드.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo캘린더코드.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo캘린더코드.ItemHeight = 12;
			this.cbo캘린더코드.Location = new System.Drawing.Point(107, 0);
			this.cbo캘린더코드.Name = "cbo캘린더코드";
			this.cbo캘린더코드.Size = new System.Drawing.Size(185, 20);
			this.cbo캘린더코드.TabIndex = 1;
			// 
			// P_CZ_PR_CFG_PLANT
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.Name = "P_CZ_PR_CFG_PLANT";
			this.Size = new System.Drawing.Size(1060, 796);
			this.TitleText = "공장캘린더설정";
			this.mDataArea.ResumeLayout(false);
			this.mDataArea.PerformLayout();
			this._tlayMain.ResumeLayout(false);
			this.oneGridItem5.ResumeLayout(false);
			this.bpP_Plant.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flexM)).EndInit();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel _tlayMain;
        private Duzon.Common.Controls.DropDownComboBox cbo공장;
        private Duzon.Common.Controls.LabelExt lbl공장;
        private System.Windows.Forms.ImageList imageList1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid2;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem5;
        private Duzon.Common.BpControls.BpPanelControl bpP_Plant;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem17;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private Dass.FlexGrid.FlexGrid _flexM;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private Duzon.Common.Controls.RoundedButton btn삭제;
		private Duzon.Common.Controls.RoundedButton btn추가;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
		private Duzon.Common.Controls.LabelExt lbl캘린더코드;
		private Duzon.Common.Controls.DropDownComboBox cbo캘린더코드;
	}
}
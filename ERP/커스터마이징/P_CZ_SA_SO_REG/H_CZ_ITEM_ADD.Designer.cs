namespace cz
{
	partial class H_CZ_ITEM_ADD
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(H_CZ_ITEM_ADD));
			this.btn저장 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn취소 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.oneS = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl18 = new Duzon.Common.BpControls.BpPanelControl();
			this.labelExt7 = new Duzon.Common.Controls.LabelExt();
			this.flexL = new Dass.FlexGrid.FlexGrid(this.components);
			this.btn조회 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn더미 = new Duzon.Common.Controls.RoundedButton(this.components);
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl18.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.flexL)).BeginInit();
			this.SuspendLayout();
			// 
			// btn저장
			// 
			this.btn저장.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn저장.BackColor = System.Drawing.Color.White;
			this.btn저장.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn저장.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn저장.Location = new System.Drawing.Point(843, 22);
			this.btn저장.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn저장.Name = "btn저장";
			this.btn저장.Size = new System.Drawing.Size(70, 19);
			this.btn저장.TabIndex = 9;
			this.btn저장.TabStop = false;
			this.btn저장.Text = "저장";
			this.btn저장.UseVisualStyleBackColor = false;
			// 
			// btn취소
			// 
			this.btn취소.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn취소.BackColor = System.Drawing.Color.Transparent;
			this.btn취소.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn취소.Location = new System.Drawing.Point(918, 22);
			this.btn취소.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn취소.Name = "btn취소";
			this.btn취소.Size = new System.Drawing.Size(70, 19);
			this.btn취소.TabIndex = 10;
			this.btn취소.TabStop = false;
			this.btn취소.Text = "취소";
			this.btn취소.UseVisualStyleBackColor = false;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.oneS, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.flexL, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 50);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(988, 516);
			this.tableLayoutPanel1.TabIndex = 11;
			// 
			// oneS
			// 
			this.oneS.Dock = System.Windows.Forms.DockStyle.Top;
			this.oneS.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
			this.oneS.Location = new System.Drawing.Point(3, 3);
			this.oneS.Name = "oneS";
			this.oneS.Size = new System.Drawing.Size(982, 39);
			this.oneS.TabIndex = 0;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl18);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(972, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl18
			// 
			this.bpPanelControl18.Controls.Add(this.labelExt7);
			this.bpPanelControl18.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl18.Name = "bpPanelControl18";
			this.bpPanelControl18.Size = new System.Drawing.Size(533, 23);
			this.bpPanelControl18.TabIndex = 10;
			this.bpPanelControl18.Text = "bpPanelControl18";
			// 
			// labelExt7
			// 
			this.labelExt7.AutoSize = true;
			this.labelExt7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(247)))), ((int)(((byte)(248)))));
			this.labelExt7.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.labelExt7.ForeColor = System.Drawing.Color.Blue;
			this.labelExt7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.labelExt7.Location = new System.Drawing.Point(17, 5);
			this.labelExt7.Name = "labelExt7";
			this.labelExt7.Size = new System.Drawing.Size(307, 12);
			this.labelExt7.TabIndex = 1;
			this.labelExt7.Text = "※ 견적서에 존재하는 아이템만 추가 가능합니다.";
			// 
			// flexL
			// 
			this.flexL.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.flexL.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.flexL.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.flexL.AutoResize = false;
			this.flexL.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.flexL.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flexL.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.flexL.EnabledHeaderCheck = true;
			this.flexL.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.flexL.Location = new System.Drawing.Point(3, 48);
			this.flexL.Name = "flexL";
			this.flexL.Rows.Count = 1;
			this.flexL.Rows.DefaultSize = 18;
			this.flexL.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.flexL.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.flexL.ShowSort = false;
			this.flexL.Size = new System.Drawing.Size(982, 465);
			this.flexL.StyleInfo = resources.GetString("flexL.StyleInfo");
			this.flexL.TabIndex = 1;
			this.flexL.UseGridCalculator = true;
			// 
			// btn조회
			// 
			this.btn조회.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn조회.BackColor = System.Drawing.Color.White;
			this.btn조회.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn조회.Location = new System.Drawing.Point(767, 22);
			this.btn조회.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn조회.Name = "btn조회";
			this.btn조회.Size = new System.Drawing.Size(70, 19);
			this.btn조회.TabIndex = 12;
			this.btn조회.TabStop = false;
			this.btn조회.Text = "조회";
			this.btn조회.UseVisualStyleBackColor = false;
			// 
			// btn더미
			// 
			this.btn더미.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn더미.BackColor = System.Drawing.Color.White;
			this.btn더미.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn더미.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn더미.Location = new System.Drawing.Point(511, 22);
			this.btn더미.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn더미.Name = "btn더미";
			this.btn더미.Size = new System.Drawing.Size(70, 19);
			this.btn더미.TabIndex = 13;
			this.btn더미.TabStop = false;
			this.btn더미.Text = "더미";
			this.btn더미.UseVisualStyleBackColor = false;
			// 
			// H_CZ_ITEM_ADD
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(994, 569);
			this.Controls.Add(this.btn더미);
			this.Controls.Add(this.btn조회);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.btn취소);
			this.Controls.Add(this.btn저장);
			this.Name = "H_CZ_ITEM_ADD";
			this.Padding = new System.Windows.Forms.Padding(3, 50, 3, 3);
			this.ShowInTaskbar = false;
			this.Text = "H_CZ_ITEM_ADD";
			this.TitleText = "수주등록";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl18.ResumeLayout(false);
			this.bpPanelControl18.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.flexL)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Duzon.Common.Controls.RoundedButton btn저장;
		private Duzon.Common.Controls.RoundedButton btn취소;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneS;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Common.Controls.RoundedButton btn조회;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl18;
		private Duzon.Common.Controls.LabelExt labelExt7;
		private Dass.FlexGrid.FlexGrid flexL;
		private Duzon.Common.Controls.RoundedButton btn더미;
	}
}
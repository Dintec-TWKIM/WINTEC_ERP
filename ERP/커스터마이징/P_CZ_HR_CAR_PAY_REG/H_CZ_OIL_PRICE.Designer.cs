namespace cz
{
	partial class H_CZ_OIL_PRICE
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(H_CZ_OIL_PRICE));
			this.tlayM = new System.Windows.Forms.TableLayoutPanel();
			this.flexC = new Dass.FlexGrid.FlexGrid(this.components);
			this.one = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.pnlKM단가 = new Duzon.Common.BpControls.BpPanelControl();
			this.curKM단가 = new Duzon.Common.Controls.CurrencyTextBox();
			this.labelExt2 = new Duzon.Common.Controls.LabelExt();
			this.labelExt5 = new Duzon.Common.Controls.LabelExt();
			this.pnl운행거리 = new Duzon.Common.BpControls.BpPanelControl();
			this.cur유류단가 = new Duzon.Common.Controls.CurrencyTextBox();
			this.labelExt3 = new Duzon.Common.Controls.LabelExt();
			this.labelExt1 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.pnl출장목적 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt비고 = new Duzon.Common.Controls.TextBoxExt();
			this.labelExt4 = new Duzon.Common.Controls.LabelExt();
			this.btn조회 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn추가 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn삭제 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn저장 = new Duzon.Common.Controls.RoundedButton(this.components);
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.tlayM.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.flexC)).BeginInit();
			this.oneGridItem1.SuspendLayout();
			this.pnlKM단가.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.curKM단가)).BeginInit();
			this.pnl운행거리.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur유류단가)).BeginInit();
			this.oneGridItem2.SuspendLayout();
			this.pnl출장목적.SuspendLayout();
			this.SuspendLayout();
			// 
			// tlayM
			// 
			this.tlayM.ColumnCount = 1;
			this.tlayM.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlayM.Controls.Add(this.flexC, 0, 1);
			this.tlayM.Controls.Add(this.one, 0, 0);
			this.tlayM.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlayM.Location = new System.Drawing.Point(3, 50);
			this.tlayM.Name = "tlayM";
			this.tlayM.RowCount = 2;
			this.tlayM.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlayM.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlayM.Size = new System.Drawing.Size(470, 416);
			this.tlayM.TabIndex = 0;
			// 
			// flexC
			// 
			this.flexC.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.flexC.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.flexC.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.flexC.AutoResize = false;
			this.flexC.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.flexC.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flexC.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.flexC.EnabledHeaderCheck = true;
			this.flexC.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.flexC.Location = new System.Drawing.Point(3, 72);
			this.flexC.Name = "flexC";
			this.flexC.Rows.Count = 1;
			this.flexC.Rows.DefaultSize = 18;
			this.flexC.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.flexC.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.flexC.ShowSort = false;
			this.flexC.Size = new System.Drawing.Size(464, 341);
			this.flexC.StyleInfo = resources.GetString("flexC.StyleInfo");
			this.flexC.TabIndex = 0;
			this.flexC.UseGridCalculator = true;
			// 
			// one
			// 
			this.one.Dock = System.Windows.Forms.DockStyle.Top;
			this.one.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2});
			this.one.Location = new System.Drawing.Point(3, 3);
			this.one.Name = "one";
			this.one.Size = new System.Drawing.Size(464, 63);
			this.one.TabIndex = 1;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.pnlKM단가);
			this.oneGridItem1.Controls.Add(this.pnl운행거리);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(454, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// pnlKM단가
			// 
			this.pnlKM단가.Controls.Add(this.curKM단가);
			this.pnlKM단가.Controls.Add(this.labelExt2);
			this.pnlKM단가.Controls.Add(this.labelExt5);
			this.pnlKM단가.Location = new System.Drawing.Point(214, 1);
			this.pnlKM단가.Name = "pnlKM단가";
			this.pnlKM단가.Size = new System.Drawing.Size(210, 23);
			this.pnlKM단가.TabIndex = 19;
			this.pnlKM단가.Text = "bpPanelControl1";
			// 
			// curKM단가
			// 
			this.curKM단가.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.curKM단가.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.curKM단가.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.curKM단가.ForeColor = System.Drawing.SystemColors.ControlText;
			this.curKM단가.Location = new System.Drawing.Point(84, 1);
			this.curKM단가.Mask = null;
			this.curKM단가.Name = "curKM단가";
			this.curKM단가.NullString = "0";
			this.curKM단가.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.curKM단가.Size = new System.Drawing.Size(70, 21);
			this.curKM단가.TabIndex = 14;
			this.curKM단가.Tag = "UM_KM";
			this.curKM단가.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.curKM단가.UseKeyEnter = true;
			this.curKM단가.UseKeyF3 = true;
			// 
			// labelExt2
			// 
			this.labelExt2.Location = new System.Drawing.Point(157, 4);
			this.labelExt2.Name = "labelExt2";
			this.labelExt2.Resizeble = true;
			this.labelExt2.Size = new System.Drawing.Size(50, 16);
			this.labelExt2.TabIndex = 13;
			this.labelExt2.Text = "원/KM";
			this.labelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelExt5
			// 
			this.labelExt5.Location = new System.Drawing.Point(17, 4);
			this.labelExt5.Name = "labelExt5";
			this.labelExt5.Resizeble = true;
			this.labelExt5.Size = new System.Drawing.Size(65, 16);
			this.labelExt5.TabIndex = 1;
			this.labelExt5.Text = "KM단가";
			this.labelExt5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// pnl운행거리
			// 
			this.pnl운행거리.Controls.Add(this.cur유류단가);
			this.pnl운행거리.Controls.Add(this.labelExt3);
			this.pnl운행거리.Controls.Add(this.labelExt1);
			this.pnl운행거리.Location = new System.Drawing.Point(2, 1);
			this.pnl운행거리.Name = "pnl운행거리";
			this.pnl운행거리.Size = new System.Drawing.Size(210, 23);
			this.pnl운행거리.TabIndex = 18;
			this.pnl운행거리.Text = "bpPanelControl1";
			// 
			// cur유류단가
			// 
			this.cur유류단가.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.cur유류단가.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur유류단가.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur유류단가.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur유류단가.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur유류단가.Location = new System.Drawing.Point(84, 1);
			this.cur유류단가.Mask = null;
			this.cur유류단가.Name = "cur유류단가";
			this.cur유류단가.NullString = "0";
			this.cur유류단가.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur유류단가.Size = new System.Drawing.Size(70, 21);
			this.cur유류단가.TabIndex = 14;
			this.cur유류단가.Tag = "UM_OIL";
			this.cur유류단가.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.cur유류단가.UseKeyEnter = true;
			this.cur유류단가.UseKeyF3 = true;
			// 
			// labelExt3
			// 
			this.labelExt3.Location = new System.Drawing.Point(157, 4);
			this.labelExt3.Name = "labelExt3";
			this.labelExt3.Resizeble = true;
			this.labelExt3.Size = new System.Drawing.Size(50, 16);
			this.labelExt3.TabIndex = 13;
			this.labelExt3.Text = "원/L";
			this.labelExt3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelExt1
			// 
			this.labelExt1.Location = new System.Drawing.Point(17, 4);
			this.labelExt1.Name = "labelExt1";
			this.labelExt1.Resizeble = true;
			this.labelExt1.Size = new System.Drawing.Size(65, 16);
			this.labelExt1.TabIndex = 1;
			this.labelExt1.Text = "유류단가";
			this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.pnl출장목적);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(454, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// pnl출장목적
			// 
			this.pnl출장목적.Controls.Add(this.txt비고);
			this.pnl출장목적.Controls.Add(this.labelExt4);
			this.pnl출장목적.Location = new System.Drawing.Point(2, 1);
			this.pnl출장목적.Name = "pnl출장목적";
			this.pnl출장목적.Size = new System.Drawing.Size(422, 23);
			this.pnl출장목적.TabIndex = 20;
			this.pnl출장목적.Text = "bpPanelControl3";
			// 
			// txt비고
			// 
			this.txt비고.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt비고.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt비고.Location = new System.Drawing.Point(84, 1);
			this.txt비고.Name = "txt비고";
			this.txt비고.SelectedAllEnabled = false;
			this.txt비고.Size = new System.Drawing.Size(323, 21);
			this.txt비고.TabIndex = 9;
			this.txt비고.Tag = "DC_RMK";
			this.txt비고.UseKeyEnter = true;
			this.txt비고.UseKeyF3 = true;
			// 
			// labelExt4
			// 
			this.labelExt4.Location = new System.Drawing.Point(17, 4);
			this.labelExt4.Name = "labelExt4";
			this.labelExt4.Resizeble = true;
			this.labelExt4.Size = new System.Drawing.Size(65, 16);
			this.labelExt4.TabIndex = 1;
			this.labelExt4.Text = "비고";
			this.labelExt4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btn조회
			// 
			this.btn조회.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn조회.BackColor = System.Drawing.Color.White;
			this.btn조회.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn조회.Location = new System.Drawing.Point(170, 24);
			this.btn조회.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn조회.Name = "btn조회";
			this.btn조회.Size = new System.Drawing.Size(70, 19);
			this.btn조회.TabIndex = 1;
			this.btn조회.TabStop = false;
			this.btn조회.Text = "조회";
			this.btn조회.UseVisualStyleBackColor = false;
			// 
			// btn추가
			// 
			this.btn추가.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn추가.BackColor = System.Drawing.Color.White;
			this.btn추가.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn추가.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn추가.Location = new System.Drawing.Point(246, 24);
			this.btn추가.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn추가.Name = "btn추가";
			this.btn추가.Size = new System.Drawing.Size(70, 19);
			this.btn추가.TabIndex = 5;
			this.btn추가.TabStop = false;
			this.btn추가.Text = "추가";
			this.btn추가.UseVisualStyleBackColor = false;
			// 
			// btn삭제
			// 
			this.btn삭제.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn삭제.BackColor = System.Drawing.Color.White;
			this.btn삭제.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn삭제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn삭제.Location = new System.Drawing.Point(322, 24);
			this.btn삭제.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn삭제.Name = "btn삭제";
			this.btn삭제.Size = new System.Drawing.Size(70, 19);
			this.btn삭제.TabIndex = 4;
			this.btn삭제.TabStop = false;
			this.btn삭제.Text = "삭제";
			this.btn삭제.UseVisualStyleBackColor = false;
			// 
			// btn저장
			// 
			this.btn저장.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn저장.BackColor = System.Drawing.Color.White;
			this.btn저장.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn저장.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn저장.Location = new System.Drawing.Point(398, 24);
			this.btn저장.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn저장.Name = "btn저장";
			this.btn저장.Size = new System.Drawing.Size(70, 19);
			this.btn저장.TabIndex = 6;
			this.btn저장.TabStop = false;
			this.btn저장.Text = "저장";
			this.btn저장.UseVisualStyleBackColor = false;
			// 
			// H_CZ_OIL_PRICE
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(476, 469);
			this.Controls.Add(this.btn저장);
			this.Controls.Add(this.btn추가);
			this.Controls.Add(this.btn삭제);
			this.Controls.Add(this.btn조회);
			this.Controls.Add(this.tlayM);
			this.Name = "H_CZ_OIL_PRICE";
			this.Padding = new System.Windows.Forms.Padding(3, 50, 3, 3);
			this.ShowInTaskbar = false;
			this.Text = "H_CZ_OIL_PRICE";
			this.TitleText = "유가등록";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.tlayM.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.flexC)).EndInit();
			this.oneGridItem1.ResumeLayout(false);
			this.pnlKM단가.ResumeLayout(false);
			this.pnlKM단가.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.curKM단가)).EndInit();
			this.pnl운행거리.ResumeLayout(false);
			this.pnl운행거리.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur유류단가)).EndInit();
			this.oneGridItem2.ResumeLayout(false);
			this.pnl출장목적.ResumeLayout(false);
			this.pnl출장목적.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tlayM;
		private Duzon.Common.Controls.RoundedButton btn조회;
		private Duzon.Common.Controls.RoundedButton btn추가;
		private Duzon.Common.Controls.RoundedButton btn삭제;
		private Dass.FlexGrid.FlexGrid flexC;
		private Duzon.Erpiu.Windows.OneControls.OneGrid one;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
		private Duzon.Common.BpControls.BpPanelControl pnl출장목적;
		private Duzon.Common.Controls.TextBoxExt txt비고;
		private Duzon.Common.Controls.LabelExt labelExt4;
		private Duzon.Common.BpControls.BpPanelControl pnl운행거리;
		private Duzon.Common.Controls.CurrencyTextBox cur유류단가;
		private Duzon.Common.Controls.LabelExt labelExt3;
		private Duzon.Common.Controls.LabelExt labelExt1;
		private Duzon.Common.BpControls.BpPanelControl pnlKM단가;
		private Duzon.Common.Controls.CurrencyTextBox curKM단가;
		private Duzon.Common.Controls.LabelExt labelExt2;
		private Duzon.Common.Controls.LabelExt labelExt5;
		private Duzon.Common.Controls.RoundedButton btn저장;


	}
}
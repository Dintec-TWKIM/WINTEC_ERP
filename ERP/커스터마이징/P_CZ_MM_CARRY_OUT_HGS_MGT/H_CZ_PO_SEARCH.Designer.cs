namespace cz
{
	partial class H_CZ_PO_SEARCH
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(H_CZ_PO_SEARCH));
			this.tlayMain = new System.Windows.Forms.TableLayoutPanel();
			this.grdList = new Dass.FlexGrid.FlexGrid(this.components);
			this.oneMain = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl11 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo번호 = new Duzon.Common.Controls.DropDownComboBox();
			this.tbx번호 = new Duzon.Common.Controls.TextBoxExt();
			this.btn확정 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn조회 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn취소 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.tbx포커스 = new Duzon.Common.Controls.TextBoxExt();
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.tlayMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl11.SuspendLayout();
			this.SuspendLayout();
			// 
			// tlayMain
			// 
			this.tlayMain.ColumnCount = 1;
			this.tlayMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlayMain.Controls.Add(this.grdList, 0, 1);
			this.tlayMain.Controls.Add(this.oneMain, 0, 0);
			this.tlayMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlayMain.Location = new System.Drawing.Point(3, 50);
			this.tlayMain.Name = "tlayMain";
			this.tlayMain.RowCount = 2;
			this.tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlayMain.Size = new System.Drawing.Size(868, 462);
			this.tlayMain.TabIndex = 0;
			// 
			// grdList
			// 
			this.grdList.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.grdList.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.grdList.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.grdList.AutoResize = false;
			this.grdList.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdList.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.grdList.EnabledHeaderCheck = true;
			this.grdList.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.grdList.Location = new System.Drawing.Point(3, 49);
			this.grdList.Name = "grdList";
			this.grdList.Rows.Count = 1;
			this.grdList.Rows.DefaultSize = 18;
			this.grdList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.grdList.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.grdList.ShowSort = false;
			this.grdList.Size = new System.Drawing.Size(862, 410);
			this.grdList.StyleInfo = resources.GetString("grdList.StyleInfo");
			this.grdList.TabIndex = 4;
			this.grdList.UseGridCalculator = true;
			// 
			// oneMain
			// 
			this.oneMain.Dock = System.Windows.Forms.DockStyle.Top;
			this.oneMain.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
			this.oneMain.Location = new System.Drawing.Point(3, 3);
			this.oneMain.Name = "oneMain";
			this.oneMain.Size = new System.Drawing.Size(862, 40);
			this.oneMain.TabIndex = 1;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl11);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(852, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl11
			// 
			this.bpPanelControl11.Controls.Add(this.cbo번호);
			this.bpPanelControl11.Controls.Add(this.tbx번호);
			this.bpPanelControl11.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl11.Name = "bpPanelControl11";
			this.bpPanelControl11.Size = new System.Drawing.Size(280, 23);
			this.bpPanelControl11.TabIndex = 24;
			this.bpPanelControl11.Text = "bpPanelControl11";
			// 
			// cbo번호
			// 
			this.cbo번호.AutoDropDown = true;
			this.cbo번호.DisplayMember = "NAME";
			this.cbo번호.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo번호.FormattingEnabled = true;
			this.cbo번호.ItemHeight = 12;
			this.cbo번호.Location = new System.Drawing.Point(12, 1);
			this.cbo번호.Name = "cbo번호";
			this.cbo번호.Size = new System.Drawing.Size(80, 20);
			this.cbo번호.TabIndex = 18;
			this.cbo번호.ValueMember = "CODE";
			// 
			// tbx번호
			// 
			this.tbx번호.BackColor = System.Drawing.Color.White;
			this.tbx번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.tbx번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbx번호.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.tbx번호.Location = new System.Drawing.Point(94, 1);
			this.tbx번호.Name = "tbx번호";
			this.tbx번호.Size = new System.Drawing.Size(185, 21);
			this.tbx번호.TabIndex = 4;
			this.tbx번호.Tag = "";
			// 
			// btn확정
			// 
			this.btn확정.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn확정.BackColor = System.Drawing.Color.Transparent;
			this.btn확정.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn확정.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn확정.Location = new System.Drawing.Point(719, 22);
			this.btn확정.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn확정.Name = "btn확정";
			this.btn확정.Size = new System.Drawing.Size(70, 19);
			this.btn확정.TabIndex = 9;
			this.btn확정.TabStop = false;
			this.btn확정.Text = "확정";
			this.btn확정.UseVisualStyleBackColor = false;
			// 
			// btn조회
			// 
			this.btn조회.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn조회.BackColor = System.Drawing.Color.White;
			this.btn조회.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn조회.Location = new System.Drawing.Point(643, 22);
			this.btn조회.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn조회.Name = "btn조회";
			this.btn조회.Size = new System.Drawing.Size(70, 19);
			this.btn조회.TabIndex = 8;
			this.btn조회.TabStop = false;
			this.btn조회.Text = "조회";
			this.btn조회.UseVisualStyleBackColor = false;
			// 
			// btn취소
			// 
			this.btn취소.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn취소.BackColor = System.Drawing.Color.Transparent;
			this.btn취소.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn취소.Location = new System.Drawing.Point(794, 22);
			this.btn취소.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn취소.Name = "btn취소";
			this.btn취소.Size = new System.Drawing.Size(70, 19);
			this.btn취소.TabIndex = 7;
			this.btn취소.TabStop = false;
			this.btn취소.Text = "취소";
			this.btn취소.UseVisualStyleBackColor = false;
			// 
			// tbx포커스
			// 
			this.tbx포커스.BackColor = System.Drawing.Color.White;
			this.tbx포커스.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.tbx포커스.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbx포커스.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.tbx포커스.Location = new System.Drawing.Point(128, 8);
			this.tbx포커스.Name = "tbx포커스";
			this.tbx포커스.Size = new System.Drawing.Size(81, 21);
			this.tbx포커스.TabIndex = 10;
			this.tbx포커스.Tag = "";
			// 
			// H_CZ_PO_SEARCH
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(874, 515);
			this.Controls.Add(this.tbx포커스);
			this.Controls.Add(this.btn확정);
			this.Controls.Add(this.btn조회);
			this.Controls.Add(this.btn취소);
			this.Controls.Add(this.tlayMain);
			this.Name = "H_CZ_PO_SEARCH";
			this.Padding = new System.Windows.Forms.Padding(3, 50, 3, 3);
			this.ShowInTaskbar = false;
			this.Text = "H_CZ_PO_SEARCH";
			this.TitleText = "발주 검색";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.tlayMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl11.ResumeLayout(false);
			this.bpPanelControl11.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tlayMain;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneMain;
		private Dass.FlexGrid.FlexGrid grdList;
		private Duzon.Common.Controls.RoundedButton btn확정;
		private Duzon.Common.Controls.RoundedButton btn조회;
		private Duzon.Common.Controls.RoundedButton btn취소;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl11;
		private Duzon.Common.Controls.TextBoxExt tbx번호;
		private Duzon.Common.Controls.DropDownComboBox cbo번호;
		private Duzon.Common.Controls.TextBoxExt tbx포커스;
	}
}
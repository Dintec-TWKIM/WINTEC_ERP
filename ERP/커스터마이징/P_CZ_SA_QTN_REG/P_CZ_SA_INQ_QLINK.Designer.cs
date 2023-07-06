namespace cz
{
	partial class P_CZ_SA_INQ_QLINK
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_SA_INQ_QLINK));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.grdList = new Dass.FlexGrid.FlexGrid(this.components);
			this.oneMain = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.pnl품목명 = new Duzon.Common.BpControls.BpPanelControl();
			this.tbx품목명 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl품목명 = new Duzon.Common.Controls.LabelExt();
			this.pnl품목코드 = new Duzon.Common.BpControls.BpPanelControl();
			this.tbx품목코드 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl품목코드 = new Duzon.Common.Controls.LabelExt();
			this.pnl주제 = new Duzon.Common.BpControls.BpPanelControl();
			this.tbx주제 = new Duzon.Common.Controls.TextBoxExt();
			this.labelExt7 = new Duzon.Common.Controls.LabelExt();
			this.btn확정 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn취소 = new Duzon.Common.Controls.RoundedButton(this.components);
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
			this.oneGridItem1.SuspendLayout();
			this.pnl품목명.SuspendLayout();
			this.pnl품목코드.SuspendLayout();
			this.pnl주제.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.grdList, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.oneMain, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 50);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1288, 516);
			this.tableLayoutPanel1.TabIndex = 0;
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
			this.grdList.Location = new System.Drawing.Point(3, 131);
			this.grdList.Name = "grdList";
			this.grdList.Rows.Count = 1;
			this.grdList.Rows.DefaultSize = 18;
			this.grdList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.grdList.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.grdList.ShowSort = false;
			this.grdList.Size = new System.Drawing.Size(1282, 382);
			this.grdList.StyleInfo = resources.GetString("grdList.StyleInfo");
			this.grdList.TabIndex = 8;
			this.grdList.UseGridCalculator = true;
			// 
			// oneMain
			// 
			this.oneMain.Dock = System.Windows.Forms.DockStyle.Top;
			this.oneMain.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
			this.oneMain.Location = new System.Drawing.Point(3, 3);
			this.oneMain.Name = "oneMain";
			this.oneMain.Size = new System.Drawing.Size(1282, 122);
			this.oneMain.TabIndex = 7;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.pnl품목명);
			this.oneGridItem1.Controls.Add(this.pnl품목코드);
			this.oneGridItem1.Controls.Add(this.pnl주제);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(1272, 105);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoWidth;
			this.oneGridItem1.TabIndex = 0;
			// 
			// pnl품목명
			// 
			this.pnl품목명.Controls.Add(this.tbx품목명);
			this.pnl품목명.Controls.Add(this.lbl품목명);
			this.pnl품목명.Location = new System.Drawing.Point(751, 1);
			this.pnl품목명.Name = "pnl품목명";
			this.pnl품목명.Size = new System.Drawing.Size(500, 105);
			this.pnl품목명.TabIndex = 9;
			this.pnl품목명.Text = "bpPanelControl2";
			// 
			// tbx품목명
			// 
			this.tbx품목명.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.tbx품목명.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbx품목명.Location = new System.Drawing.Point(94, 1);
			this.tbx품목명.Multiline = true;
			this.tbx품목명.Name = "tbx품목명";
			this.tbx품목명.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbx품목명.Size = new System.Drawing.Size(405, 103);
			this.tbx품목명.TabIndex = 6;
			this.tbx품목명.Tag = "NM_SUBJECT";
			this.tbx품목명.UseKeyEnter = false;
			this.tbx품목명.UseKeyF3 = false;
			// 
			// lbl품목명
			// 
			this.lbl품목명.Location = new System.Drawing.Point(17, 4);
			this.lbl품목명.Name = "lbl품목명";
			this.lbl품목명.Size = new System.Drawing.Size(75, 16);
			this.lbl품목명.TabIndex = 5;
			this.lbl품목명.Text = "품목명";
			this.lbl품목명.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// pnl품목코드
			// 
			this.pnl품목코드.Controls.Add(this.tbx품목코드);
			this.pnl품목코드.Controls.Add(this.lbl품목코드);
			this.pnl품목코드.Location = new System.Drawing.Point(504, 1);
			this.pnl품목코드.Name = "pnl품목코드";
			this.pnl품목코드.Size = new System.Drawing.Size(245, 105);
			this.pnl품목코드.TabIndex = 8;
			this.pnl품목코드.Text = "bpPanelControl1";
			// 
			// tbx품목코드
			// 
			this.tbx품목코드.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.tbx품목코드.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbx품목코드.Location = new System.Drawing.Point(94, 1);
			this.tbx품목코드.Multiline = true;
			this.tbx품목코드.Name = "tbx품목코드";
			this.tbx품목코드.Size = new System.Drawing.Size(150, 103);
			this.tbx품목코드.TabIndex = 6;
			this.tbx품목코드.Tag = "NM_SUBJECT";
			this.tbx품목코드.UseKeyEnter = false;
			this.tbx품목코드.UseKeyF3 = false;
			// 
			// lbl품목코드
			// 
			this.lbl품목코드.Location = new System.Drawing.Point(17, 4);
			this.lbl품목코드.Name = "lbl품목코드";
			this.lbl품목코드.Size = new System.Drawing.Size(75, 16);
			this.lbl품목코드.TabIndex = 5;
			this.lbl품목코드.Text = "품목코드";
			this.lbl품목코드.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// pnl주제
			// 
			this.pnl주제.Controls.Add(this.tbx주제);
			this.pnl주제.Controls.Add(this.labelExt7);
			this.pnl주제.Location = new System.Drawing.Point(2, 1);
			this.pnl주제.Name = "pnl주제";
			this.pnl주제.Size = new System.Drawing.Size(500, 105);
			this.pnl주제.TabIndex = 7;
			this.pnl주제.Text = "bpPanelControl4";
			// 
			// tbx주제
			// 
			this.tbx주제.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.tbx주제.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbx주제.Location = new System.Drawing.Point(94, 1);
			this.tbx주제.Multiline = true;
			this.tbx주제.Name = "tbx주제";
			this.tbx주제.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbx주제.Size = new System.Drawing.Size(405, 103);
			this.tbx주제.TabIndex = 6;
			this.tbx주제.Tag = "NM_SUBJECT";
			this.tbx주제.UseKeyEnter = false;
			this.tbx주제.UseKeyF3 = false;
			// 
			// labelExt7
			// 
			this.labelExt7.Location = new System.Drawing.Point(17, 4);
			this.labelExt7.Name = "labelExt7";
			this.labelExt7.Size = new System.Drawing.Size(75, 16);
			this.labelExt7.TabIndex = 5;
			this.labelExt7.Text = "주제";
			this.labelExt7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btn확정
			// 
			this.btn확정.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn확정.BackColor = System.Drawing.Color.Transparent;
			this.btn확정.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn확정.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn확정.Location = new System.Drawing.Point(1142, 22);
			this.btn확정.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn확정.Name = "btn확정";
			this.btn확정.Size = new System.Drawing.Size(70, 19);
			this.btn확정.TabIndex = 11;
			this.btn확정.TabStop = false;
			this.btn확정.Text = "확정";
			this.btn확정.UseVisualStyleBackColor = false;
			// 
			// btn취소
			// 
			this.btn취소.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn취소.BackColor = System.Drawing.Color.Transparent;
			this.btn취소.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn취소.Location = new System.Drawing.Point(1218, 22);
			this.btn취소.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn취소.Name = "btn취소";
			this.btn취소.Size = new System.Drawing.Size(70, 19);
			this.btn취소.TabIndex = 10;
			this.btn취소.TabStop = false;
			this.btn취소.Text = "취소";
			this.btn취소.UseVisualStyleBackColor = false;
			// 
			// H_CZ_QLINK
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(1294, 569);
			this.Controls.Add(this.btn확정);
			this.Controls.Add(this.btn취소);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "H_CZ_QLINK";
			this.Padding = new System.Windows.Forms.Padding(3, 50, 3, 3);
			this.Text = "H_CZ_QLINK";
			this.TitleText = "퀵링크";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
			this.oneGridItem1.ResumeLayout(false);
			this.pnl품목명.ResumeLayout(false);
			this.pnl품목명.PerformLayout();
			this.pnl품목코드.ResumeLayout(false);
			this.pnl품목코드.PerformLayout();
			this.pnl주제.ResumeLayout(false);
			this.pnl주제.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneMain;
		private Dass.FlexGrid.FlexGrid grdList;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Common.BpControls.BpPanelControl pnl주제;
		private Duzon.Common.Controls.TextBoxExt tbx주제;
		private Duzon.Common.Controls.LabelExt labelExt7;
		private Duzon.Common.BpControls.BpPanelControl pnl품목명;
		private Duzon.Common.Controls.TextBoxExt tbx품목명;
		private Duzon.Common.Controls.LabelExt lbl품목명;
		private Duzon.Common.BpControls.BpPanelControl pnl품목코드;
		private Duzon.Common.Controls.TextBoxExt tbx품목코드;
		private Duzon.Common.Controls.LabelExt lbl품목코드;
		private Duzon.Common.Controls.RoundedButton btn확정;
		private Duzon.Common.Controls.RoundedButton btn취소;

	}
}
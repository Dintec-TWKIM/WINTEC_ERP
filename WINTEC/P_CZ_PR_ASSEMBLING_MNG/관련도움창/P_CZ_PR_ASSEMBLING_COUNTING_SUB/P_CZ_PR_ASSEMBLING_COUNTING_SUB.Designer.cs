
namespace cz
{
	partial class P_CZ_PR_ASSEMBLING_COUNTING_SUB
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_PR_ASSEMBLING_COUNTING_SUB));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btn실사취소 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn재고실사 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn조회 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.txtQR코드스캔 = new Duzon.Common.Controls.TextBoxExt();
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.chk사용유무 = new Duzon.Common.Controls.CheckBoxExt();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.txtID번호 = new Duzon.Common.Controls.TextBoxExt();
			this.lblID번호 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx품목 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl품목 = new Duzon.Common.Controls.LabelExt();
			this._flex재고실사 = new Dass.FlexGrid.FlexGrid(this.components);
			this.btn대기 = new Duzon.Common.Controls.RoundedButton(this.components);
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex재고실사)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this._flex재고실사, 0, 2);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 50);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(992, 622);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.btn실사취소);
			this.flowLayoutPanel1.Controls.Add(this.btn재고실사);
			this.flowLayoutPanel1.Controls.Add(this.btn대기);
			this.flowLayoutPanel1.Controls.Add(this.btn조회);
			this.flowLayoutPanel1.Controls.Add(this.txtQR코드스캔);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 50);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(986, 26);
			this.flowLayoutPanel1.TabIndex = 2;
			// 
			// btn실사취소
			// 
			this.btn실사취소.BackColor = System.Drawing.Color.Transparent;
			this.btn실사취소.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn실사취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn실사취소.Location = new System.Drawing.Point(913, 3);
			this.btn실사취소.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn실사취소.Name = "btn실사취소";
			this.btn실사취소.Size = new System.Drawing.Size(70, 19);
			this.btn실사취소.TabIndex = 3;
			this.btn실사취소.TabStop = false;
			this.btn실사취소.Text = "실사취소";
			this.btn실사취소.UseVisualStyleBackColor = false;
			// 
			// btn재고실사
			// 
			this.btn재고실사.BackColor = System.Drawing.Color.Transparent;
			this.btn재고실사.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn재고실사.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn재고실사.Location = new System.Drawing.Point(837, 3);
			this.btn재고실사.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn재고실사.Name = "btn재고실사";
			this.btn재고실사.Size = new System.Drawing.Size(70, 19);
			this.btn재고실사.TabIndex = 2;
			this.btn재고실사.TabStop = false;
			this.btn재고실사.Text = "재고실사";
			this.btn재고실사.UseVisualStyleBackColor = false;
			// 
			// btn조회
			// 
			this.btn조회.BackColor = System.Drawing.Color.Transparent;
			this.btn조회.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn조회.Location = new System.Drawing.Point(685, 3);
			this.btn조회.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn조회.Name = "btn조회";
			this.btn조회.Size = new System.Drawing.Size(70, 19);
			this.btn조회.TabIndex = 1;
			this.btn조회.TabStop = false;
			this.btn조회.Text = "조회";
			this.btn조회.UseVisualStyleBackColor = false;
			// 
			// txtQR코드스캔
			// 
			this.txtQR코드스캔.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txtQR코드스캔.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtQR코드스캔.Location = new System.Drawing.Point(563, 3);
			this.txtQR코드스캔.Name = "txtQR코드스캔";
			this.txtQR코드스캔.Size = new System.Drawing.Size(116, 21);
			this.txtQR코드스캔.TabIndex = 4;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(986, 41);
			this.oneGrid1.TabIndex = 0;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.chk사용유무);
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.Controls.Add(this.bpPanelControl2);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(976, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// chk사용유무
			// 
			this.chk사용유무.Location = new System.Drawing.Point(590, 1);
			this.chk사용유무.Name = "chk사용유무";
			this.chk사용유무.Size = new System.Drawing.Size(80, 24);
			this.chk사용유무.TabIndex = 4;
			this.chk사용유무.Text = "사용유무";
			this.chk사용유무.TextDD = null;
			this.chk사용유무.UseVisualStyleBackColor = true;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.txtID번호);
			this.bpPanelControl1.Controls.Add(this.lblID번호);
			this.bpPanelControl1.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl1.TabIndex = 3;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// txtID번호
			// 
			this.txtID번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txtID번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtID번호.Dock = System.Windows.Forms.DockStyle.Right;
			this.txtID번호.Location = new System.Drawing.Point(106, 0);
			this.txtID번호.Name = "txtID번호";
			this.txtID번호.Size = new System.Drawing.Size(186, 21);
			this.txtID번호.TabIndex = 1;
			// 
			// lblID번호
			// 
			this.lblID번호.Dock = System.Windows.Forms.DockStyle.Left;
			this.lblID번호.Location = new System.Drawing.Point(0, 0);
			this.lblID번호.Name = "lblID번호";
			this.lblID번호.Size = new System.Drawing.Size(100, 23);
			this.lblID번호.TabIndex = 0;
			this.lblID번호.Text = "ID번호";
			this.lblID번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.ctx품목);
			this.bpPanelControl2.Controls.Add(this.lbl품목);
			this.bpPanelControl2.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl2.TabIndex = 2;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// ctx품목
			// 
			this.ctx품목.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx품목.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB;
			this.ctx품목.Location = new System.Drawing.Point(106, 0);
			this.ctx품목.Name = "ctx품목";
			this.ctx품목.Size = new System.Drawing.Size(186, 21);
			this.ctx품목.TabIndex = 1;
			this.ctx품목.TabStop = false;
			this.ctx품목.Text = "bpCodeTextBox1";
			// 
			// lbl품목
			// 
			this.lbl품목.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl품목.Location = new System.Drawing.Point(0, 0);
			this.lbl품목.Name = "lbl품목";
			this.lbl품목.Size = new System.Drawing.Size(100, 23);
			this.lbl품목.TabIndex = 0;
			this.lbl품목.Text = "품목";
			this.lbl품목.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _flex재고실사
			// 
			this._flex재고실사.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex재고실사.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex재고실사.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex재고실사.AutoResize = false;
			this._flex재고실사.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex재고실사.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex재고실사.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex재고실사.EnabledHeaderCheck = true;
			this._flex재고실사.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex재고실사.Location = new System.Drawing.Point(3, 82);
			this._flex재고실사.Name = "_flex재고실사";
			this._flex재고실사.Rows.Count = 1;
			this._flex재고실사.Rows.DefaultSize = 18;
			this._flex재고실사.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex재고실사.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex재고실사.ShowSort = false;
			this._flex재고실사.Size = new System.Drawing.Size(986, 537);
			this._flex재고실사.StyleInfo = resources.GetString("_flex재고실사.StyleInfo");
			this._flex재고실사.TabIndex = 3;
			this._flex재고실사.UseGridCalculator = true;
			// 
			// btn대기
			// 
			this.btn대기.BackColor = System.Drawing.Color.Transparent;
			this.btn대기.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn대기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn대기.Location = new System.Drawing.Point(761, 3);
			this.btn대기.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn대기.Name = "btn대기";
			this.btn대기.Size = new System.Drawing.Size(70, 19);
			this.btn대기.TabIndex = 5;
			this.btn대기.TabStop = false;
			this.btn대기.Text = "대기";
			this.btn대기.UseVisualStyleBackColor = false;
			// 
			// P_CZ_PR_ASSEMBLING_COUNTING_SUB
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(997, 675);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "P_CZ_PR_ASSEMBLING_COUNTING_SUB";
			this.Text = "P_CZ_PR_ASSEMBLING_COUNTING_SUB";
			this.TitleText = "재고실사";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.bpPanelControl1.PerformLayout();
			this.bpPanelControl2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex재고실사)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
		private Duzon.Common.Controls.LabelExt lbl품목;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
		private Duzon.Common.Controls.TextBoxExt txtID번호;
		private Duzon.Common.Controls.LabelExt lblID번호;
		private Duzon.Common.BpControls.BpCodeTextBox ctx품목;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private Duzon.Common.Controls.RoundedButton btn재고실사;
		private Duzon.Common.Controls.RoundedButton btn조회;
		private Dass.FlexGrid.FlexGrid _flex재고실사;
		private Duzon.Common.Controls.RoundedButton btn실사취소;
		private Duzon.Common.Controls.TextBoxExt txtQR코드스캔;
		private Duzon.Common.Controls.CheckBoxExt chk사용유무;
		private Duzon.Common.Controls.RoundedButton btn대기;
	}
}
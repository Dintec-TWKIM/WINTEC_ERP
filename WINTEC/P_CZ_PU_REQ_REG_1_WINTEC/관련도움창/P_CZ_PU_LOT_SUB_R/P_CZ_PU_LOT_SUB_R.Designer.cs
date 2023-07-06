
namespace cz
{
	partial class P_CZ_PU_LOT_SUB_R
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_PU_LOT_SUB_R));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panel4 = new System.Windows.Forms.Panel();
			this._flexM = new Dass.FlexGrid.FlexGrid(this.components);
			this.panel5 = new System.Windows.Forms.Panel();
			this._flexD = new Dass.FlexGrid.FlexGrid(this.components);
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btn종료 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn확인 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn자동채번 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.btn삭제 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn추가 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn엑셀업로드 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn출고LOT내역 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.cur잔량 = new Duzon.Common.Controls.CurrencyTextBox();
			this.lbl_잔량 = new Duzon.Common.Controls.LabelExt();
			this.lbl_잔령 = new Duzon.Common.Controls.LabelExt();
			this.pnl빈공간 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flexM)).BeginInit();
			this.panel5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flexD)).BeginInit();
			this.flowLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur잔량)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.panel5, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 2);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 47);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(803, 579);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this._flexM);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel4.Location = new System.Drawing.Point(3, 38);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(797, 231);
			this.panel4.TabIndex = 131;
			// 
			// _flexM
			// 
			this._flexM.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flexM.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flexM.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flexM.AutoResize = false;
			this._flexM.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flexM.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flexM.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flexM.EnabledHeaderCheck = true;
			this._flexM.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flexM.Location = new System.Drawing.Point(0, 0);
			this._flexM.Name = "_flexM";
			this._flexM.Rows.Count = 1;
			this._flexM.Rows.DefaultSize = 18;
			this._flexM.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flexM.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flexM.ShowSort = false;
			this._flexM.Size = new System.Drawing.Size(797, 231);
			this._flexM.StyleInfo = resources.GetString("_flexM.StyleInfo");
			this._flexM.TabIndex = 131;
			this._flexM.UseGridCalculator = true;
			// 
			// panel5
			// 
			this.panel5.Controls.Add(this._flexD);
			this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel5.Location = new System.Drawing.Point(3, 310);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(797, 266);
			this.panel5.TabIndex = 132;
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
			this._flexD.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flexD.Location = new System.Drawing.Point(0, 0);
			this._flexD.Name = "_flexD";
			this._flexD.Rows.Count = 1;
			this._flexD.Rows.DefaultSize = 18;
			this._flexD.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flexD.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flexD.ShowSort = false;
			this._flexD.Size = new System.Drawing.Size(797, 266);
			this._flexD.StyleInfo = resources.GetString("_flexD.StyleInfo");
			this._flexD.TabIndex = 132;
			this._flexD.UseGridCalculator = true;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.btn종료);
			this.flowLayoutPanel1.Controls.Add(this.btn확인);
			this.flowLayoutPanel1.Controls.Add(this.btn자동채번);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(797, 29);
			this.flowLayoutPanel1.TabIndex = 133;
			// 
			// btn종료
			// 
			this.btn종료.BackColor = System.Drawing.Color.White;
			this.btn종료.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn종료.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btn종료.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn종료.Location = new System.Drawing.Point(734, 3);
			this.btn종료.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn종료.Name = "btn종료";
			this.btn종료.Size = new System.Drawing.Size(60, 19);
			this.btn종료.TabIndex = 133;
			this.btn종료.TabStop = false;
			this.btn종료.Text = "종료";
			this.btn종료.UseVisualStyleBackColor = false;
			// 
			// btn확인
			// 
			this.btn확인.BackColor = System.Drawing.Color.White;
			this.btn확인.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn확인.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn확인.Location = new System.Drawing.Point(668, 3);
			this.btn확인.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn확인.Name = "btn확인";
			this.btn확인.Size = new System.Drawing.Size(60, 19);
			this.btn확인.TabIndex = 134;
			this.btn확인.TabStop = false;
			this.btn확인.Text = "확인";
			this.btn확인.UseVisualStyleBackColor = false;
			// 
			// btn자동채번
			// 
			this.btn자동채번.BackColor = System.Drawing.Color.White;
			this.btn자동채번.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn자동채번.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn자동채번.Location = new System.Drawing.Point(592, 3);
			this.btn자동채번.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn자동채번.Name = "btn자동채번";
			this.btn자동채번.Size = new System.Drawing.Size(70, 19);
			this.btn자동채번.TabIndex = 135;
			this.btn자동채번.TabStop = false;
			this.btn자동채번.Text = "자동채번";
			this.btn자동채번.UseVisualStyleBackColor = false;
			this.btn자동채번.Visible = false;
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.Controls.Add(this.btn삭제);
			this.flowLayoutPanel2.Controls.Add(this.btn추가);
			this.flowLayoutPanel2.Controls.Add(this.btn엑셀업로드);
			this.flowLayoutPanel2.Controls.Add(this.btn출고LOT내역);
			this.flowLayoutPanel2.Controls.Add(this.cur잔량);
			this.flowLayoutPanel2.Controls.Add(this.lbl_잔량);
			this.flowLayoutPanel2.Controls.Add(this.pnl빈공간);
			this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 275);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(797, 29);
			this.flowLayoutPanel2.TabIndex = 134;
			// 
			// btn삭제
			// 
			this.btn삭제.BackColor = System.Drawing.Color.White;
			this.btn삭제.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn삭제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn삭제.Location = new System.Drawing.Point(734, 3);
			this.btn삭제.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn삭제.Name = "btn삭제";
			this.btn삭제.Size = new System.Drawing.Size(60, 19);
			this.btn삭제.TabIndex = 133;
			this.btn삭제.TabStop = false;
			this.btn삭제.Text = "삭제";
			this.btn삭제.UseVisualStyleBackColor = false;
			// 
			// btn추가
			// 
			this.btn추가.BackColor = System.Drawing.Color.White;
			this.btn추가.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn추가.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn추가.Location = new System.Drawing.Point(668, 3);
			this.btn추가.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn추가.Name = "btn추가";
			this.btn추가.Size = new System.Drawing.Size(60, 19);
			this.btn추가.TabIndex = 134;
			this.btn추가.TabStop = false;
			this.btn추가.Text = "추가";
			this.btn추가.UseVisualStyleBackColor = false;
			// 
			// btn엑셀업로드
			// 
			this.btn엑셀업로드.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn엑셀업로드.BackColor = System.Drawing.Color.White;
			this.btn엑셀업로드.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn엑셀업로드.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn엑셀업로드.Location = new System.Drawing.Point(574, 3);
			this.btn엑셀업로드.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn엑셀업로드.Name = "btn엑셀업로드";
			this.btn엑셀업로드.Size = new System.Drawing.Size(88, 19);
			this.btn엑셀업로드.TabIndex = 135;
			this.btn엑셀업로드.TabStop = false;
			this.btn엑셀업로드.Tag = "엑셀업로드";
			this.btn엑셀업로드.Text = "엑셀업로드";
			this.btn엑셀업로드.UseVisualStyleBackColor = true;
			// 
			// btn출고LOT내역
			// 
			this.btn출고LOT내역.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn출고LOT내역.BackColor = System.Drawing.Color.White;
			this.btn출고LOT내역.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn출고LOT내역.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn출고LOT내역.Location = new System.Drawing.Point(480, 3);
			this.btn출고LOT내역.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn출고LOT내역.Name = "btn출고LOT내역";
			this.btn출고LOT내역.Size = new System.Drawing.Size(88, 19);
			this.btn출고LOT내역.TabIndex = 136;
			this.btn출고LOT내역.TabStop = false;
			this.btn출고LOT내역.Text = "출고LOT내역";
			this.btn출고LOT내역.UseVisualStyleBackColor = true;
			// 
			// cur잔량
			// 
			this.cur잔량.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur잔량.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur잔량.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur잔량.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur잔량.Location = new System.Drawing.Point(374, 3);
			this.cur잔량.Name = "cur잔량";
			this.cur잔량.NullString = "0";
			this.cur잔량.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur잔량.Size = new System.Drawing.Size(100, 21);
			this.cur잔량.TabIndex = 218;
			this.cur잔량.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// lbl_잔량
			// 
			this.lbl_잔량.Location = new System.Drawing.Point(328, 0);
			this.lbl_잔량.Name = "lbl_잔량";
			this.lbl_잔량.Size = new System.Drawing.Size(40, 24);
			this.lbl_잔량.TabIndex = 217;
			this.lbl_잔량.Text = "잔량";
			this.lbl_잔량.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl_잔령
			// 
			this.lbl_잔령.Location = new System.Drawing.Point(328, 0);
			this.lbl_잔령.Name = "lbl_잔령";
			this.lbl_잔령.Size = new System.Drawing.Size(40, 24);
			this.lbl_잔령.TabIndex = 217;
			this.lbl_잔령.Text = "잔량";
			this.lbl_잔령.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// pnl빈공간
			// 
			this.pnl빈공간.ForeColor = System.Drawing.Color.White;
			this.pnl빈공간.Location = new System.Drawing.Point(9, 3);
			this.pnl빈공간.Name = "pnl빈공간";
			this.pnl빈공간.Size = new System.Drawing.Size(313, 21);
			this.pnl빈공간.TabIndex = 216;
			// 
			// P_CZ_PU_LOT_SUB_R
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btn종료;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(804, 626);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "P_CZ_PU_LOT_SUB_R";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "ERP iU";
			this.TitleText = "LOT도움창(입고)";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flexM)).EndInit();
			this.panel5.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flexD)).EndInit();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel2.ResumeLayout(false);
			this.flowLayoutPanel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur잔량)).EndInit();
			this.ResumeLayout(false);

        }

		#endregion
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private Duzon.Common.Controls.RoundedButton btn추가;
		private Duzon.Common.Controls.RoundedButton btn삭제;
		private Duzon.Common.Controls.RoundedButton btn확인;
		private Duzon.Common.Controls.RoundedButton btn종료;
		private System.Windows.Forms.Panel panel4;
		private Dass.FlexGrid.FlexGrid _flexM;
		private System.Windows.Forms.Panel panel5;
		private Dass.FlexGrid.FlexGrid _flexD;
		private Duzon.Common.Controls.RoundedButton btn엑셀업로드;
		private Duzon.Common.Controls.RoundedButton btn출고LOT내역;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
		private Duzon.Common.Controls.RoundedButton btn자동채번;
		private Duzon.Common.Controls.CurrencyTextBox cur잔량;
		private Duzon.Common.Controls.LabelExt lbl_잔량;
		private Duzon.Common.Controls.LabelExt lbl_잔령;
		private System.Windows.Forms.Panel pnl빈공간;
	}
}
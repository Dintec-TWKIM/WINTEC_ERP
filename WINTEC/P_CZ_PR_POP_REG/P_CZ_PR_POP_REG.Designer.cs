
namespace cz
{
	partial class P_CZ_PR_POP_REG
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_PR_POP_REG));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this._flex = new Dass.FlexGrid.FlexGrid(this.components);
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt작업지시번호 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl작업지시번호 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx설비 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl설비 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo공장 = new Duzon.Common.Controls.DropDownComboBox();
			this.lbl공장 = new Duzon.Common.Controls.LabelExt();
			this.btn지침서보기 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn작업 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn재작업 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.mDataArea.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl3.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.tableLayoutPanel1);
			this.mDataArea.Size = new System.Drawing.Size(1206, 675);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this._flex, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1206, 675);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// _flex
			// 
			this._flex.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex.AutoResize = false;
			this._flex.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex.EnabledHeaderCheck = true;
			this._flex.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex.Location = new System.Drawing.Point(3, 49);
			this._flex.Name = "_flex";
			this._flex.Rows.Count = 1;
			this._flex.Rows.DefaultSize = 20;
			this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex.ShowSort = false;
			this._flex.Size = new System.Drawing.Size(1200, 623);
			this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
			this._flex.TabIndex = 0;
			this._flex.UseGridCalculator = true;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(1200, 40);
			this.oneGrid1.TabIndex = 1;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl3);
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.Controls.Add(this.bpPanelControl2);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(1190, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.txt작업지시번호);
			this.bpPanelControl3.Controls.Add(this.lbl작업지시번호);
			this.bpPanelControl3.Location = new System.Drawing.Point(590, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl3.TabIndex = 2;
			this.bpPanelControl3.Text = "bpPanelControl3";
			// 
			// txt작업지시번호
			// 
			this.txt작업지시번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt작업지시번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt작업지시번호.Dock = System.Windows.Forms.DockStyle.Right;
			this.txt작업지시번호.Location = new System.Drawing.Point(106, 0);
			this.txt작업지시번호.Name = "txt작업지시번호";
			this.txt작업지시번호.Size = new System.Drawing.Size(186, 21);
			this.txt작업지시번호.TabIndex = 1;
			// 
			// lbl작업지시번호
			// 
			this.lbl작업지시번호.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl작업지시번호.Location = new System.Drawing.Point(0, 0);
			this.lbl작업지시번호.Name = "lbl작업지시번호";
			this.lbl작업지시번호.Size = new System.Drawing.Size(100, 23);
			this.lbl작업지시번호.TabIndex = 0;
			this.lbl작업지시번호.Text = "작업지시번호";
			this.lbl작업지시번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.ctx설비);
			this.bpPanelControl1.Controls.Add(this.lbl설비);
			this.bpPanelControl1.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl1.TabIndex = 0;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// ctx설비
			// 
			this.ctx설비.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx설비.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_TABLE_SUB;
			this.ctx설비.Location = new System.Drawing.Point(106, 0);
			this.ctx설비.Name = "ctx설비";
			this.ctx설비.Size = new System.Drawing.Size(186, 21);
			this.ctx설비.TabIndex = 1;
			this.ctx설비.TabStop = false;
			this.ctx설비.Text = "bpCodeTextBox1";
			// 
			// lbl설비
			// 
			this.lbl설비.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl설비.Location = new System.Drawing.Point(0, 0);
			this.lbl설비.Name = "lbl설비";
			this.lbl설비.Size = new System.Drawing.Size(100, 23);
			this.lbl설비.TabIndex = 0;
			this.lbl설비.Text = "설비";
			this.lbl설비.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.cbo공장);
			this.bpPanelControl2.Controls.Add(this.lbl공장);
			this.bpPanelControl2.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl2.TabIndex = 1;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// cbo공장
			// 
			this.cbo공장.AutoDropDown = true;
			this.cbo공장.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo공장.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo공장.FormattingEnabled = true;
			this.cbo공장.ItemHeight = 12;
			this.cbo공장.Location = new System.Drawing.Point(106, 0);
			this.cbo공장.Name = "cbo공장";
			this.cbo공장.Size = new System.Drawing.Size(186, 20);
			this.cbo공장.TabIndex = 1;
			// 
			// lbl공장
			// 
			this.lbl공장.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl공장.Location = new System.Drawing.Point(0, 0);
			this.lbl공장.Name = "lbl공장";
			this.lbl공장.Size = new System.Drawing.Size(100, 23);
			this.lbl공장.TabIndex = 0;
			this.lbl공장.Text = "공장";
			this.lbl공장.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btn지침서보기
			// 
			this.btn지침서보기.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn지침서보기.BackColor = System.Drawing.Color.Transparent;
			this.btn지침서보기.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn지침서보기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn지침서보기.Location = new System.Drawing.Point(1119, 10);
			this.btn지침서보기.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn지침서보기.Name = "btn지침서보기";
			this.btn지침서보기.Size = new System.Drawing.Size(84, 19);
			this.btn지침서보기.TabIndex = 0;
			this.btn지침서보기.TabStop = false;
			this.btn지침서보기.Text = "지침서보기";
			this.btn지침서보기.UseVisualStyleBackColor = false;
			// 
			// btn작업
			// 
			this.btn작업.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn작업.BackColor = System.Drawing.Color.Transparent;
			this.btn작업.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn작업.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn작업.Location = new System.Drawing.Point(967, 10);
			this.btn작업.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn작업.Name = "btn작업";
			this.btn작업.Size = new System.Drawing.Size(70, 19);
			this.btn작업.TabIndex = 1;
			this.btn작업.TabStop = false;
			this.btn작업.Text = "작업";
			this.btn작업.UseVisualStyleBackColor = false;
			// 
			// btn재작업
			// 
			this.btn재작업.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn재작업.BackColor = System.Drawing.Color.Transparent;
			this.btn재작업.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn재작업.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn재작업.Location = new System.Drawing.Point(1043, 10);
			this.btn재작업.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn재작업.Name = "btn재작업";
			this.btn재작업.Size = new System.Drawing.Size(70, 19);
			this.btn재작업.TabIndex = 3;
			this.btn재작업.TabStop = false;
			this.btn재작업.Text = "재작업";
			this.btn재작업.UseVisualStyleBackColor = false;
			// 
			// P_CZ_PR_POP_REG
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Controls.Add(this.btn재작업);
			this.Controls.Add(this.btn지침서보기);
			this.Controls.Add(this.btn작업);
			this.Name = "P_CZ_PR_POP_REG";
			this.Size = new System.Drawing.Size(1206, 715);
			this.TitleText = "P_CZ_PR_POP";
			this.Controls.SetChildIndex(this.mDataArea, 0);
			this.Controls.SetChildIndex(this.btn작업, 0);
			this.Controls.SetChildIndex(this.btn지침서보기, 0);
			this.Controls.SetChildIndex(this.btn재작업, 0);
			this.mDataArea.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl3.ResumeLayout(false);
			this.bpPanelControl3.PerformLayout();
			this.bpPanelControl1.ResumeLayout(false);
			this.bpPanelControl2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private Dass.FlexGrid.FlexGrid _flex;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
		private Duzon.Common.BpControls.BpCodeTextBox ctx설비;
		private Duzon.Common.Controls.LabelExt lbl설비;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
		private Duzon.Common.Controls.DropDownComboBox cbo공장;
		private Duzon.Common.Controls.LabelExt lbl공장;
		private Duzon.Common.Controls.RoundedButton btn지침서보기;
		private Duzon.Common.Controls.RoundedButton btn작업;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
		private Duzon.Common.Controls.TextBoxExt txt작업지시번호;
		private Duzon.Common.Controls.LabelExt lbl작업지시번호;
		private Duzon.Common.Controls.RoundedButton btn재작업;
	}
}
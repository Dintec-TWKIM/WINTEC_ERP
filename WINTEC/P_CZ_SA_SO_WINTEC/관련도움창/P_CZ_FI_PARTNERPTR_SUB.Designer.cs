namespace cz
{
	partial class P_CZ_FI_PARTNERPTR_SUB
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_FI_PARTNERPTR_SUB));
			this._flex = new Dass.FlexGrid.FlexGrid(this.components);
			this.btnSave = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btnEnd = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btnAdd = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btnDel = new Duzon.Common.Controls.RoundedButton(this.components);
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// _flex
			// 
			this._flex.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex.AutoResize = false;
			this._flex.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex.EnabledHeaderCheck = true;
			this._flex.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex.Location = new System.Drawing.Point(3, 36);
			this._flex.Name = "_flex";
			this._flex.Rows.Count = 1;
			this._flex.Rows.DefaultSize = 18;
			this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex.ShowSort = false;
			this._flex.Size = new System.Drawing.Size(807, 336);
			this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
			this._flex.TabIndex = 0;
			this._flex.UseGridCalculator = true;
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.BackColor = System.Drawing.Color.White;
			this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnSave.Location = new System.Drawing.Point(678, 3);
			this.btnSave.MaximumSize = new System.Drawing.Size(0, 19);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(60, 19);
			this.btnSave.TabIndex = 12;
			this.btnSave.TabStop = false;
			this.btnSave.Text = "저장";
			this.btnSave.UseVisualStyleBackColor = true;
			// 
			// btnEnd
			// 
			this.btnEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnEnd.BackColor = System.Drawing.Color.White;
			this.btnEnd.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnEnd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnEnd.Location = new System.Drawing.Point(744, 3);
			this.btnEnd.MaximumSize = new System.Drawing.Size(0, 19);
			this.btnEnd.Name = "btnEnd";
			this.btnEnd.Size = new System.Drawing.Size(60, 19);
			this.btnEnd.TabIndex = 13;
			this.btnEnd.TabStop = false;
			this.btnEnd.Text = "적용";
			this.btnEnd.UseVisualStyleBackColor = true;
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAdd.BackColor = System.Drawing.Color.White;
			this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnAdd.Location = new System.Drawing.Point(546, 3);
			this.btnAdd.MaximumSize = new System.Drawing.Size(0, 19);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(60, 19);
			this.btnAdd.TabIndex = 14;
			this.btnAdd.TabStop = false;
			this.btnAdd.Text = "추가";
			this.btnAdd.UseVisualStyleBackColor = true;
			// 
			// btnDel
			// 
			this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDel.BackColor = System.Drawing.Color.White;
			this.btnDel.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnDel.Location = new System.Drawing.Point(612, 3);
			this.btnDel.MaximumSize = new System.Drawing.Size(0, 19);
			this.btnDel.Name = "btnDel";
			this.btnDel.Size = new System.Drawing.Size(60, 19);
			this.btnDel.TabIndex = 15;
			this.btnDel.TabStop = false;
			this.btnDel.Text = "삭제";
			this.btnDel.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this._flex, 0, 1);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 48);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(813, 375);
			this.tableLayoutPanel1.TabIndex = 16;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.btnEnd);
			this.flowLayoutPanel1.Controls.Add(this.btnSave);
			this.flowLayoutPanel1.Controls.Add(this.btnDel);
			this.flowLayoutPanel1.Controls.Add(this.btnAdd);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(807, 27);
			this.flowLayoutPanel1.TabIndex = 0;
			// 
			// P_CZ_FI_PARTNERPTR_SUB
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(815, 422);
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "P_CZ_FI_PARTNERPTR_SUB";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "ERP iU";
			this.TitleText = "담당자정보";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Dass.FlexGrid.FlexGrid _flex;
		private Duzon.Common.Controls.RoundedButton btnSave;
		private Duzon.Common.Controls.RoundedButton btnEnd;
		private Duzon.Common.Controls.RoundedButton btnAdd;
		private Duzon.Common.Controls.RoundedButton btnDel;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
	}
}
namespace cz
{
	partial class P_CZ_PR_LINK_MES_REG_SUB
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_PR_LINK_MES_REG_SUB));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.chk_Delete = new Duzon.Common.Controls.CheckBoxExt();
			this.chk_Update = new Duzon.Common.Controls.CheckBoxExt();
			this.chk_Insert = new Duzon.Common.Controls.CheckBoxExt();
			this.bpPnl기간 = new Duzon.Common.BpControls.BpPanelControl();
			this.per기간 = new Duzon.Common.Controls.PeriodPicker();
			this.lbl기간 = new Duzon.Common.Controls.LabelExt();
			this.bpPnl공장 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl공장 = new Duzon.Common.Controls.LabelExt();
			this.cbo공장 = new Duzon.Common.Controls.DropDownComboBox();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btn닫기 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn조회 = new Duzon.Common.Controls.RoundedButton(this.components);
			this._flex = new Dass.FlexGrid.FlexGrid(this.components);
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.bpPnl기간.SuspendLayout();
			this.bpPnl공장.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this._flex, 0, 2);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 50);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(858, 516);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Top;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(852, 39);
			this.oneGrid1.TabIndex = 0;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.Controls.Add(this.bpPnl기간);
			this.oneGridItem1.Controls.Add(this.bpPnl공장);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(842, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.chk_Delete);
			this.bpPanelControl1.Controls.Add(this.chk_Update);
			this.bpPanelControl1.Controls.Add(this.chk_Insert);
			this.bpPanelControl1.Location = new System.Drawing.Point(540, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(267, 23);
			this.bpPanelControl1.TabIndex = 2;
			// 
			// chk_Delete
			// 
			this.chk_Delete.Checked = true;
			this.chk_Delete.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk_Delete.Location = new System.Drawing.Point(171, 1);
			this.chk_Delete.Name = "chk_Delete";
			this.chk_Delete.Size = new System.Drawing.Size(67, 24);
			this.chk_Delete.TabIndex = 2;
			this.chk_Delete.Text = "DELETE";
			this.chk_Delete.TextDD = null;
			this.chk_Delete.UseVisualStyleBackColor = true;
			// 
			// chk_Update
			// 
			this.chk_Update.Location = new System.Drawing.Point(102, 1);
			this.chk_Update.Name = "chk_Update";
			this.chk_Update.Size = new System.Drawing.Size(67, 24);
			this.chk_Update.TabIndex = 1;
			this.chk_Update.Text = "UPDATE";
			this.chk_Update.TextDD = null;
			this.chk_Update.UseVisualStyleBackColor = true;
			// 
			// chk_Insert
			// 
			this.chk_Insert.Checked = true;
			this.chk_Insert.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk_Insert.Location = new System.Drawing.Point(33, 1);
			this.chk_Insert.Name = "chk_Insert";
			this.chk_Insert.Size = new System.Drawing.Size(67, 24);
			this.chk_Insert.TabIndex = 0;
			this.chk_Insert.Text = "INSERT";
			this.chk_Insert.TextDD = null;
			this.chk_Insert.UseVisualStyleBackColor = true;
			// 
			// bpPnl기간
			// 
			this.bpPnl기간.Controls.Add(this.per기간);
			this.bpPnl기간.Controls.Add(this.lbl기간);
			this.bpPnl기간.Location = new System.Drawing.Point(271, 1);
			this.bpPnl기간.Name = "bpPnl기간";
			this.bpPnl기간.Size = new System.Drawing.Size(267, 23);
			this.bpPnl기간.TabIndex = 2;
			// 
			// per기간
			// 
			this.per기간.Cursor = System.Windows.Forms.Cursors.Hand;
			this.per기간.IsNecessaryCondition = true;
			this.per기간.Location = new System.Drawing.Point(81, 1);
			this.per기간.MaximumSize = new System.Drawing.Size(185, 21);
			this.per기간.MinimumSize = new System.Drawing.Size(185, 21);
			this.per기간.Name = "per기간";
			this.per기간.Size = new System.Drawing.Size(185, 21);
			this.per기간.TabIndex = 2;
			// 
			// lbl기간
			// 
			this.lbl기간.Location = new System.Drawing.Point(0, 3);
			this.lbl기간.Name = "lbl기간";
			this.lbl기간.Size = new System.Drawing.Size(80, 16);
			this.lbl기간.TabIndex = 1;
			this.lbl기간.Text = "기간";
			this.lbl기간.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPnl공장
			// 
			this.bpPnl공장.Controls.Add(this.lbl공장);
			this.bpPnl공장.Controls.Add(this.cbo공장);
			this.bpPnl공장.Location = new System.Drawing.Point(2, 1);
			this.bpPnl공장.Name = "bpPnl공장";
			this.bpPnl공장.Size = new System.Drawing.Size(267, 23);
			this.bpPnl공장.TabIndex = 1;
			// 
			// lbl공장
			// 
			this.lbl공장.Location = new System.Drawing.Point(0, 3);
			this.lbl공장.Name = "lbl공장";
			this.lbl공장.Size = new System.Drawing.Size(80, 16);
			this.lbl공장.TabIndex = 0;
			this.lbl공장.Text = "공장";
			this.lbl공장.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo공장
			// 
			this.cbo공장.AutoDropDown = true;
			this.cbo공장.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.cbo공장.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo공장.ItemHeight = 12;
			this.cbo공장.Location = new System.Drawing.Point(81, 1);
			this.cbo공장.Name = "cbo공장";
			this.cbo공장.Size = new System.Drawing.Size(186, 20);
			this.cbo공장.TabIndex = 1;
			this.cbo공장.UseKeyF3 = false;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanel1.Controls.Add(this.btn닫기);
			this.flowLayoutPanel1.Controls.Add(this.btn조회);
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 48);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(852, 24);
			this.flowLayoutPanel1.TabIndex = 1;
			// 
			// btn닫기
			// 
			this.btn닫기.BackColor = System.Drawing.Color.Transparent;
			this.btn닫기.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn닫기.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btn닫기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn닫기.Location = new System.Drawing.Point(779, 3);
			this.btn닫기.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn닫기.Name = "btn닫기";
			this.btn닫기.Size = new System.Drawing.Size(70, 19);
			this.btn닫기.TabIndex = 0;
			this.btn닫기.TabStop = false;
			this.btn닫기.Text = "닫기";
			this.btn닫기.UseVisualStyleBackColor = false;
			// 
			// btn조회
			// 
			this.btn조회.BackColor = System.Drawing.Color.Transparent;
			this.btn조회.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn조회.Location = new System.Drawing.Point(703, 3);
			this.btn조회.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn조회.Name = "btn조회";
			this.btn조회.Size = new System.Drawing.Size(70, 19);
			this.btn조회.TabIndex = 1;
			this.btn조회.TabStop = false;
			this.btn조회.Text = "조회";
			this.btn조회.UseVisualStyleBackColor = false;
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
			this._flex.Location = new System.Drawing.Point(3, 78);
			this._flex.Name = "_flex";
			this._flex.Rows.Count = 1;
			this._flex.Rows.DefaultSize = 18;
			this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex.ShowSort = false;
			this._flex.Size = new System.Drawing.Size(852, 435);
			this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
			this._flex.TabIndex = 2;
			this._flex.UseGridCalculator = true;
			// 
			// P_CZ_PR_LINK_MES_REG_SUB
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btn닫기;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(862, 569);
			this.ControlBox = false;
			this.Controls.Add(this.tableLayoutPanel1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "P_CZ_PR_LINK_MES_REG_SUB";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "ERP iU";
			this.TitleText = "연동 이력 도움창";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.bpPnl기간.ResumeLayout(false);
			this.bpPnl공장.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
			this.ResumeLayout(false);

        }

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private Duzon.Common.Controls.RoundedButton btn닫기;
		private Duzon.Common.Controls.RoundedButton btn조회;
		private Dass.FlexGrid.FlexGrid _flex;
		private Duzon.Common.BpControls.BpPanelControl bpPnl공장;
		private Duzon.Common.Controls.LabelExt lbl공장;
		private Duzon.Common.Controls.DropDownComboBox cbo공장;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
		private Duzon.Common.Controls.CheckBoxExt chk_Delete;
		private Duzon.Common.Controls.CheckBoxExt chk_Update;
		private Duzon.Common.Controls.CheckBoxExt chk_Insert;
		private Duzon.Common.BpControls.BpPanelControl bpPnl기간;
		private Duzon.Common.Controls.PeriodPicker per기간;
		private Duzon.Common.Controls.LabelExt lbl기간;
		#endregion
	}
}
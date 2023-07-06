namespace cz
{
	partial class P_CZ_MA_HULL_ITEM_MGT
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_MA_HULL_ITEM_MGT));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.flexH = new Dass.FlexGrid.FlexGrid(this.components);
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.pnl단가대상 = new Duzon.Common.BpControls.BpPanelControl();
			this.labelExt7 = new Duzon.Common.Controls.LabelExt();
			this.cboSearchType = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
			this.cboKeyword1 = new Duzon.Common.Controls.DropDownComboBox();
			this.txtKeyword1 = new Duzon.Common.Controls.TextBoxExt();
			this.bpPanelControl6 = new Duzon.Common.BpControls.BpPanelControl();
			this.cboKeyword2 = new Duzon.Common.Controls.DropDownComboBox();
			this.txtKeyword2 = new Duzon.Common.Controls.TextBoxExt();
			this.pnlMatching = new Duzon.Common.BpControls.BpPanelControl();
			this.cboJoin2 = new Duzon.Common.Controls.DropDownComboBox();
			this.cboJoin1 = new Duzon.Common.Controls.DropDownComboBox();
			this.labelExt13 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.cboKeyword3 = new Duzon.Common.Controls.DropDownComboBox();
			this.txtKeyword3 = new Duzon.Common.Controls.TextBoxExt();
			this.btnConfirm = new Duzon.Common.Controls.RoundedButton(this.components);
			this.mDataArea.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.flexH)).BeginInit();
			this.oneGridItem1.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.pnl단가대상.SuspendLayout();
			this.bpPanelControl5.SuspendLayout();
			this.bpPanelControl6.SuspendLayout();
			this.pnlMatching.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.tableLayoutPanel1);
			this.mDataArea.Size = new System.Drawing.Size(1300, 610);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.flexH, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1300, 610);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Top;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(1294, 68);
			this.oneGrid1.TabIndex = 0;
			// 
			// flexH
			// 
			this.flexH.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.flexH.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.flexH.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.flexH.AutoResize = false;
			this.flexH.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.flexH.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flexH.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.flexH.EnabledHeaderCheck = true;
			this.flexH.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.flexH.Location = new System.Drawing.Point(3, 77);
			this.flexH.Name = "flexH";
			this.flexH.Rows.Count = 1;
			this.flexH.Rows.DefaultSize = 20;
			this.flexH.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.flexH.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.flexH.ShowSort = false;
			this.flexH.Size = new System.Drawing.Size(1294, 530);
			this.flexH.StyleInfo = resources.GetString("flexH.StyleInfo");
			this.flexH.TabIndex = 3;
			this.flexH.UseGridCalculator = true;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.pnlMatching);
			this.oneGridItem1.Controls.Add(this.pnl단가대상);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(1284, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.bpPanelControl1);
			this.oneGridItem2.Controls.Add(this.bpPanelControl6);
			this.oneGridItem2.Controls.Add(this.bpPanelControl5);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(1284, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// pnl단가대상
			// 
			this.pnl단가대상.Controls.Add(this.cboSearchType);
			this.pnl단가대상.Controls.Add(this.labelExt7);
			this.pnl단가대상.Location = new System.Drawing.Point(2, 1);
			this.pnl단가대상.Name = "pnl단가대상";
			this.pnl단가대상.Size = new System.Drawing.Size(280, 23);
			this.pnl단가대상.TabIndex = 32;
			this.pnl단가대상.Text = "bpPanelControl3";
			// 
			// labelExt7
			// 
			this.labelExt7.Location = new System.Drawing.Point(17, 4);
			this.labelExt7.Name = "labelExt7";
			this.labelExt7.Size = new System.Drawing.Size(75, 16);
			this.labelExt7.TabIndex = 1;
			this.labelExt7.Text = "조회대상";
			this.labelExt7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cboSearchType
			// 
			this.cboSearchType.AutoDropDown = true;
			this.cboSearchType.DisplayMember = "NAME";
			this.cboSearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSearchType.FormattingEnabled = true;
			this.cboSearchType.ItemHeight = 12;
			this.cboSearchType.Location = new System.Drawing.Point(94, 1);
			this.cboSearchType.Name = "cboSearchType";
			this.cboSearchType.Size = new System.Drawing.Size(185, 20);
			this.cboSearchType.TabIndex = 4;
			this.cboSearchType.ValueMember = "CODE";
			// 
			// bpPanelControl5
			// 
			this.bpPanelControl5.Controls.Add(this.cboKeyword1);
			this.bpPanelControl5.Controls.Add(this.txtKeyword1);
			this.bpPanelControl5.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl5.Name = "bpPanelControl5";
			this.bpPanelControl5.Size = new System.Drawing.Size(280, 23);
			this.bpPanelControl5.TabIndex = 26;
			this.bpPanelControl5.Text = "bpPanelControl5";
			// 
			// cboKeyword1
			// 
			this.cboKeyword1.AutoDropDown = true;
			this.cboKeyword1.DisplayMember = "NAME";
			this.cboKeyword1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboKeyword1.FormattingEnabled = true;
			this.cboKeyword1.ItemHeight = 12;
			this.cboKeyword1.Location = new System.Drawing.Point(7, 1);
			this.cboKeyword1.Name = "cboKeyword1";
			this.cboKeyword1.Size = new System.Drawing.Size(85, 20);
			this.cboKeyword1.TabIndex = 13;
			this.cboKeyword1.Tag = "";
			this.cboKeyword1.ValueMember = "CODE";
			// 
			// txtKeyword1
			// 
			this.txtKeyword1.BackColor = System.Drawing.Color.White;
			this.txtKeyword1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txtKeyword1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtKeyword1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.txtKeyword1.Location = new System.Drawing.Point(94, 1);
			this.txtKeyword1.Name = "txtKeyword1";
			this.txtKeyword1.Size = new System.Drawing.Size(185, 21);
			this.txtKeyword1.TabIndex = 4;
			this.txtKeyword1.Tag = "";
			// 
			// bpPanelControl6
			// 
			this.bpPanelControl6.Controls.Add(this.cboKeyword2);
			this.bpPanelControl6.Controls.Add(this.txtKeyword2);
			this.bpPanelControl6.Location = new System.Drawing.Point(284, 1);
			this.bpPanelControl6.Name = "bpPanelControl6";
			this.bpPanelControl6.Size = new System.Drawing.Size(280, 23);
			this.bpPanelControl6.TabIndex = 27;
			this.bpPanelControl6.Text = "bpPanelControl6";
			// 
			// cboKeyword2
			// 
			this.cboKeyword2.AutoDropDown = true;
			this.cboKeyword2.DisplayMember = "NAME";
			this.cboKeyword2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboKeyword2.FormattingEnabled = true;
			this.cboKeyword2.ItemHeight = 12;
			this.cboKeyword2.Location = new System.Drawing.Point(7, 1);
			this.cboKeyword2.Name = "cboKeyword2";
			this.cboKeyword2.Size = new System.Drawing.Size(85, 20);
			this.cboKeyword2.TabIndex = 13;
			this.cboKeyword2.Tag = "";
			this.cboKeyword2.ValueMember = "CODE";
			// 
			// txtKeyword2
			// 
			this.txtKeyword2.BackColor = System.Drawing.Color.White;
			this.txtKeyword2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txtKeyword2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtKeyword2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.txtKeyword2.Location = new System.Drawing.Point(94, 1);
			this.txtKeyword2.Name = "txtKeyword2";
			this.txtKeyword2.Size = new System.Drawing.Size(185, 21);
			this.txtKeyword2.TabIndex = 4;
			this.txtKeyword2.Tag = "";
			// 
			// pnlMatching
			// 
			this.pnlMatching.Controls.Add(this.cboJoin2);
			this.pnlMatching.Controls.Add(this.cboJoin1);
			this.pnlMatching.Controls.Add(this.labelExt13);
			this.pnlMatching.Location = new System.Drawing.Point(284, 1);
			this.pnlMatching.Name = "pnlMatching";
			this.pnlMatching.Size = new System.Drawing.Size(280, 23);
			this.pnlMatching.TabIndex = 33;
			this.pnlMatching.Text = "bpPanelControl5";
			// 
			// cboJoin2
			// 
			this.cboJoin2.AutoDropDown = true;
			this.cboJoin2.DisplayMember = "NAME";
			this.cboJoin2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboJoin2.FormattingEnabled = true;
			this.cboJoin2.ItemHeight = 12;
			this.cboJoin2.Location = new System.Drawing.Point(188, 1);
			this.cboJoin2.Name = "cboJoin2";
			this.cboJoin2.Size = new System.Drawing.Size(91, 20);
			this.cboJoin2.TabIndex = 17;
			this.cboJoin2.Tag = "CD_PARTNER_GRP";
			this.cboJoin2.ValueMember = "CODE";
			// 
			// cboJoin1
			// 
			this.cboJoin1.AutoDropDown = true;
			this.cboJoin1.DisplayMember = "NAME";
			this.cboJoin1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboJoin1.FormattingEnabled = true;
			this.cboJoin1.ItemHeight = 12;
			this.cboJoin1.Location = new System.Drawing.Point(94, 1);
			this.cboJoin1.Name = "cboJoin1";
			this.cboJoin1.Size = new System.Drawing.Size(92, 20);
			this.cboJoin1.TabIndex = 16;
			this.cboJoin1.Tag = "";
			this.cboJoin1.ValueMember = "CODE";
			// 
			// labelExt13
			// 
			this.labelExt13.Location = new System.Drawing.Point(17, 4);
			this.labelExt13.Name = "labelExt13";
			this.labelExt13.Size = new System.Drawing.Size(75, 16);
			this.labelExt13.TabIndex = 3;
			this.labelExt13.Text = "결합대상";
			this.labelExt13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.cboKeyword3);
			this.bpPanelControl1.Controls.Add(this.txtKeyword3);
			this.bpPanelControl1.Location = new System.Drawing.Point(566, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(280, 23);
			this.bpPanelControl1.TabIndex = 28;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// cboKeyword3
			// 
			this.cboKeyword3.AutoDropDown = true;
			this.cboKeyword3.DisplayMember = "NAME";
			this.cboKeyword3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboKeyword3.FormattingEnabled = true;
			this.cboKeyword3.ItemHeight = 12;
			this.cboKeyword3.Location = new System.Drawing.Point(7, 1);
			this.cboKeyword3.Name = "cboKeyword3";
			this.cboKeyword3.Size = new System.Drawing.Size(85, 20);
			this.cboKeyword3.TabIndex = 13;
			this.cboKeyword3.Tag = "";
			this.cboKeyword3.ValueMember = "CODE";
			// 
			// txtKeyword3
			// 
			this.txtKeyword3.BackColor = System.Drawing.Color.White;
			this.txtKeyword3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txtKeyword3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtKeyword3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.txtKeyword3.Location = new System.Drawing.Point(94, 1);
			this.txtKeyword3.Name = "txtKeyword3";
			this.txtKeyword3.Size = new System.Drawing.Size(185, 21);
			this.txtKeyword3.TabIndex = 4;
			this.txtKeyword3.Tag = "";
			// 
			// btnConfirm
			// 
			this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnConfirm.BackColor = System.Drawing.Color.White;
			this.btnConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnConfirm.Location = new System.Drawing.Point(1227, 10);
			this.btnConfirm.MaximumSize = new System.Drawing.Size(0, 19);
			this.btnConfirm.Name = "btnConfirm";
			this.btnConfirm.Size = new System.Drawing.Size(70, 19);
			this.btnConfirm.TabIndex = 10;
			this.btnConfirm.TabStop = false;
			this.btnConfirm.Text = "확정";
			this.btnConfirm.UseVisualStyleBackColor = false;
			// 
			// P_CZ_MA_HULL_ITEM_MGT
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnConfirm);
			this.Name = "P_CZ_MA_HULL_ITEM_MGT";
			this.Size = new System.Drawing.Size(1300, 650);
			this.TitleText = "P_CZ_MA_HULL_ITEM_MGT";
			this.Controls.SetChildIndex(this.mDataArea, 0);
			this.Controls.SetChildIndex(this.btnConfirm, 0);
			this.mDataArea.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.flexH)).EndInit();
			this.oneGridItem1.ResumeLayout(false);
			this.oneGridItem2.ResumeLayout(false);
			this.pnl단가대상.ResumeLayout(false);
			this.bpPanelControl5.ResumeLayout(false);
			this.bpPanelControl5.PerformLayout();
			this.bpPanelControl6.ResumeLayout(false);
			this.bpPanelControl6.PerformLayout();
			this.pnlMatching.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.bpPanelControl1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
		private Dass.FlexGrid.FlexGrid flexH;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
		private Duzon.Common.BpControls.BpPanelControl pnl단가대상;
		private Duzon.Common.Controls.LabelExt labelExt7;
		private Duzon.Common.Controls.DropDownComboBox cboSearchType;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl5;
		private Duzon.Common.Controls.DropDownComboBox cboKeyword1;
		private Duzon.Common.Controls.TextBoxExt txtKeyword1;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl6;
		private Duzon.Common.Controls.DropDownComboBox cboKeyword2;
		private Duzon.Common.Controls.TextBoxExt txtKeyword2;
		private Duzon.Common.BpControls.BpPanelControl pnlMatching;
		private Duzon.Common.Controls.DropDownComboBox cboJoin2;
		private Duzon.Common.Controls.DropDownComboBox cboJoin1;
		private Duzon.Common.Controls.LabelExt labelExt13;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
		private Duzon.Common.Controls.DropDownComboBox cboKeyword3;
		private Duzon.Common.Controls.TextBoxExt txtKeyword3;
		private Duzon.Common.Controls.RoundedButton btnConfirm;
	}
}
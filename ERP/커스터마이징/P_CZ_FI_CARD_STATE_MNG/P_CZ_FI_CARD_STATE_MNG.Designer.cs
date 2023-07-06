
namespace cz
{
	partial class P_CZ_FI_CARD_STATE_MNG
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_FI_CARD_STATE_MNG));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt카드번호 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl카드번호S = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt부서코드 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl부서코드 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.dtp명세년월 = new Duzon.Common.Controls.DatePicker();
			this.lbl명세년월 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.btn명세서보기 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl카드번호 = new Duzon.Common.Controls.LabelExt();
			this.bpc카드번호 = new Duzon.Common.BpControls.BpComboBox();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.webBrowser = new Duzon.Common.Controls.WebBrowserExt();
			this._flex = new Dass.FlexGrid.FlexGrid(this.components);
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btn명세서업로드하나 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn명세서업로드BC = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn카드정보업로드 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.mDataArea.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl4.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtp명세년월)).BeginInit();
			this.oneGridItem2.SuspendLayout();
			this.bpPanelControl3.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.tableLayoutPanel1);
			this.mDataArea.Size = new System.Drawing.Size(1281, 745);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 68F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1281, 745);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(1275, 62);
			this.oneGrid1.TabIndex = 0;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.btn명세서보기);
			this.oneGridItem1.Controls.Add(this.bpPanelControl3);
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(1265, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl4
			// 
			this.bpPanelControl4.Controls.Add(this.txt카드번호);
			this.bpPanelControl4.Controls.Add(this.lbl카드번호S);
			this.bpPanelControl4.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl4.Name = "bpPanelControl4";
			this.bpPanelControl4.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl4.TabIndex = 5;
			this.bpPanelControl4.Text = "bpPanelControl4";
			// 
			// txt카드번호
			// 
			this.txt카드번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt카드번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt카드번호.Dock = System.Windows.Forms.DockStyle.Right;
			this.txt카드번호.Location = new System.Drawing.Point(106, 0);
			this.txt카드번호.Name = "txt카드번호";
			this.txt카드번호.Size = new System.Drawing.Size(186, 21);
			this.txt카드번호.TabIndex = 1;
			// 
			// lbl카드번호S
			// 
			this.lbl카드번호S.BackColor = System.Drawing.Color.Transparent;
			this.lbl카드번호S.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl카드번호S.Location = new System.Drawing.Point(0, 0);
			this.lbl카드번호S.Name = "lbl카드번호S";
			this.lbl카드번호S.Size = new System.Drawing.Size(100, 23);
			this.lbl카드번호S.TabIndex = 0;
			this.lbl카드번호S.Text = "카드번호(UP)";
			this.lbl카드번호S.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.txt부서코드);
			this.bpPanelControl2.Controls.Add(this.lbl부서코드);
			this.bpPanelControl2.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl2.TabIndex = 4;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// txt부서코드
			// 
			this.txt부서코드.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt부서코드.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt부서코드.Dock = System.Windows.Forms.DockStyle.Right;
			this.txt부서코드.Location = new System.Drawing.Point(106, 0);
			this.txt부서코드.Name = "txt부서코드";
			this.txt부서코드.Size = new System.Drawing.Size(186, 21);
			this.txt부서코드.TabIndex = 1;
			// 
			// lbl부서코드
			// 
			this.lbl부서코드.BackColor = System.Drawing.Color.Transparent;
			this.lbl부서코드.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl부서코드.Location = new System.Drawing.Point(0, 0);
			this.lbl부서코드.Name = "lbl부서코드";
			this.lbl부서코드.Size = new System.Drawing.Size(100, 23);
			this.lbl부서코드.TabIndex = 0;
			this.lbl부서코드.Text = "부서코드(UP)";
			this.lbl부서코드.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.dtp명세년월);
			this.bpPanelControl1.Controls.Add(this.lbl명세년월);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl1.TabIndex = 0;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// dtp명세년월
			// 
			this.dtp명세년월.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp명세년월.Location = new System.Drawing.Point(106, 1);
			this.dtp명세년월.Mask = "####/##";
			this.dtp명세년월.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
			this.dtp명세년월.MaximumSize = new System.Drawing.Size(0, 21);
			this.dtp명세년월.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this.dtp명세년월.Modified = true;
			this.dtp명세년월.Name = "dtp명세년월";
			this.dtp명세년월.ShowUpDown = true;
			this.dtp명세년월.Size = new System.Drawing.Size(90, 21);
			this.dtp명세년월.TabIndex = 1;
			this.dtp명세년월.Value = new System.DateTime(((long)(0)));
			// 
			// lbl명세년월
			// 
			this.lbl명세년월.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl명세년월.Location = new System.Drawing.Point(0, 0);
			this.lbl명세년월.Name = "lbl명세년월";
			this.lbl명세년월.Size = new System.Drawing.Size(100, 23);
			this.lbl명세년월.TabIndex = 0;
			this.lbl명세년월.Text = "명세년월";
			this.lbl명세년월.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.bpPanelControl4);
			this.oneGridItem2.Controls.Add(this.bpPanelControl2);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(1265, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// btn명세서보기
			// 
			this.btn명세서보기.BackColor = System.Drawing.Color.Transparent;
			this.btn명세서보기.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn명세서보기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn명세서보기.Location = new System.Drawing.Point(590, 1);
			this.btn명세서보기.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn명세서보기.Name = "btn명세서보기";
			this.btn명세서보기.Size = new System.Drawing.Size(85, 19);
			this.btn명세서보기.TabIndex = 3;
			this.btn명세서보기.TabStop = false;
			this.btn명세서보기.Text = "명세서보기";
			this.btn명세서보기.UseVisualStyleBackColor = false;
			// 
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.lbl카드번호);
			this.bpPanelControl3.Controls.Add(this.bpc카드번호);
			this.bpPanelControl3.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl3.TabIndex = 3;
			this.bpPanelControl3.Text = "bpPanelControl3";
			// 
			// lbl카드번호
			// 
			this.lbl카드번호.BackColor = System.Drawing.Color.Transparent;
			this.lbl카드번호.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl카드번호.Location = new System.Drawing.Point(0, 0);
			this.lbl카드번호.Name = "lbl카드번호";
			this.lbl카드번호.Size = new System.Drawing.Size(100, 23);
			this.lbl카드번호.TabIndex = 0;
			this.lbl카드번호.Text = "카드번호";
			this.lbl카드번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpc카드번호
			// 
			this.bpc카드번호.ComboCheck = false;
			this.bpc카드번호.Dock = System.Windows.Forms.DockStyle.Right;
			this.bpc카드번호.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
			this.bpc카드번호.ItemBackColor = System.Drawing.Color.White;
			this.bpc카드번호.Location = new System.Drawing.Point(106, 0);
			this.bpc카드번호.Name = "bpc카드번호";
			this.bpc카드번호.Size = new System.Drawing.Size(186, 21);
			this.bpc카드번호.TabIndex = 2;
			this.bpc카드번호.TabStop = false;
			this.bpc카드번호.UserCodeName = "NM_CARD";
			this.bpc카드번호.UserCodeValue = "ACCT_NO";
			this.bpc카드번호.UserHelpID = "H_FI_CARD_DEPT";
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 702F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.webBrowser, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this._flex, 0, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 71);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(1275, 671);
			this.tableLayoutPanel2.TabIndex = 1;
			// 
			// webBrowser
			// 
			this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
			this.webBrowser.Location = new System.Drawing.Point(705, 3);
			this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.webBrowser.Name = "webBrowser";
			this.webBrowser.Size = new System.Drawing.Size(567, 665);
			this.webBrowser.TabIndex = 0;
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
			this._flex.Location = new System.Drawing.Point(3, 3);
			this._flex.Name = "_flex";
			this._flex.Rows.Count = 1;
			this._flex.Rows.DefaultSize = 20;
			this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex.ShowSort = false;
			this._flex.Size = new System.Drawing.Size(696, 665);
			this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
			this._flex.TabIndex = 0;
			this._flex.UseGridCalculator = true;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanel1.Controls.Add(this.btn명세서업로드하나);
			this.flowLayoutPanel1.Controls.Add(this.btn명세서업로드BC);
			this.flowLayoutPanel1.Controls.Add(this.btn카드정보업로드);
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(900, 6);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(379, 26);
			this.flowLayoutPanel1.TabIndex = 3;
			// 
			// btn명세서업로드하나
			// 
			this.btn명세서업로드하나.BackColor = System.Drawing.Color.Transparent;
			this.btn명세서업로드하나.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn명세서업로드하나.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn명세서업로드하나.Location = new System.Drawing.Point(261, 3);
			this.btn명세서업로드하나.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn명세서업로드하나.Name = "btn명세서업로드하나";
			this.btn명세서업로드하나.Size = new System.Drawing.Size(115, 19);
			this.btn명세서업로드하나.TabIndex = 2;
			this.btn명세서업로드하나.TabStop = false;
			this.btn명세서업로드하나.Text = "명세서업로드(하나)";
			this.btn명세서업로드하나.UseVisualStyleBackColor = false;
			// 
			// btn명세서업로드BC
			// 
			this.btn명세서업로드BC.BackColor = System.Drawing.Color.Transparent;
			this.btn명세서업로드BC.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn명세서업로드BC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn명세서업로드BC.Location = new System.Drawing.Point(140, 3);
			this.btn명세서업로드BC.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn명세서업로드BC.Name = "btn명세서업로드BC";
			this.btn명세서업로드BC.Size = new System.Drawing.Size(115, 19);
			this.btn명세서업로드BC.TabIndex = 0;
			this.btn명세서업로드BC.TabStop = false;
			this.btn명세서업로드BC.Text = "명세서업로드(BC)";
			this.btn명세서업로드BC.UseVisualStyleBackColor = false;
			// 
			// btn카드정보업로드
			// 
			this.btn카드정보업로드.BackColor = System.Drawing.Color.Transparent;
			this.btn카드정보업로드.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn카드정보업로드.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn카드정보업로드.Location = new System.Drawing.Point(7, 3);
			this.btn카드정보업로드.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn카드정보업로드.Name = "btn카드정보업로드";
			this.btn카드정보업로드.Size = new System.Drawing.Size(127, 19);
			this.btn카드정보업로드.TabIndex = 1;
			this.btn카드정보업로드.TabStop = false;
			this.btn카드정보업로드.Text = "카드정보업로드(BC)";
			this.btn카드정보업로드.UseVisualStyleBackColor = false;
			// 
			// P_CZ_FI_CARD_STATE_MNG
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Controls.Add(this.flowLayoutPanel1);
			this.Name = "P_CZ_FI_CARD_STATE_MNG";
			this.Size = new System.Drawing.Size(1281, 785);
			this.TitleText = "법인카드명세서관리";
			this.Controls.SetChildIndex(this.mDataArea, 0);
			this.Controls.SetChildIndex(this.flowLayoutPanel1, 0);
			this.mDataArea.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl4.ResumeLayout(false);
			this.bpPanelControl4.PerformLayout();
			this.bpPanelControl2.ResumeLayout(false);
			this.bpPanelControl2.PerformLayout();
			this.bpPanelControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dtp명세년월)).EndInit();
			this.oneGridItem2.ResumeLayout(false);
			this.bpPanelControl3.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private Duzon.Common.Controls.RoundedButton btn명세서업로드BC;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
		private Duzon.Common.Controls.LabelExt lbl명세년월;
		private Duzon.Common.Controls.DatePicker dtp명세년월;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
		private Duzon.Common.Controls.LabelExt lbl카드번호;
		private Duzon.Common.BpControls.BpComboBox bpc카드번호;
		private Duzon.Common.Controls.RoundedButton btn명세서업로드하나;
		private Duzon.Common.Controls.RoundedButton btn카드정보업로드;
		private Dass.FlexGrid.FlexGrid _flex;
		private Duzon.Common.Controls.WebBrowserExt webBrowser;
		private Duzon.Common.Controls.RoundedButton btn명세서보기;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
		private Duzon.Common.Controls.TextBoxExt txt부서코드;
		private Duzon.Common.Controls.LabelExt lbl부서코드;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
		private Duzon.Common.Controls.TextBoxExt txt카드번호;
		private Duzon.Common.Controls.LabelExt lbl카드번호S;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
	}
}
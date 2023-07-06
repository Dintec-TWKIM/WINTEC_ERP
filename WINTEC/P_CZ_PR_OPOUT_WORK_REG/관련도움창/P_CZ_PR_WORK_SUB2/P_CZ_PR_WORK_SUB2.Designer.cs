
namespace cz
{
	partial class P_CZ_PR_WORK_SUB2
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_PR_WORK_SUB2));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panel3 = new Duzon.Common.Controls.PanelExt();
			this.txtLOT번호 = new Duzon.Common.Controls.TextBoxExt();
			this.panelExt1 = new Duzon.Common.Controls.PanelExt();
			this.lblLOT번호 = new Duzon.Common.Controls.LabelExt();
			this.txt불량수량 = new Duzon.Common.Controls.TextBoxExt();
			this.txt작업지시번호 = new Duzon.Common.Controls.TextBoxExt();
			this.m_txtUnitIm = new Duzon.Common.Controls.TextBoxExt();
			this.m_txtStndItem = new Duzon.Common.Controls.TextBoxExt();
			this.m_txtNmItem = new Duzon.Common.Controls.TextBoxExt();
			this.m_txtCdItem = new Duzon.Common.Controls.TextBoxExt();
			this.txtOP = new Duzon.Common.Controls.TextBoxExt();
			this.txtWC = new Duzon.Common.Controls.TextBoxExt();
			this.panel8 = new Duzon.Common.Controls.PanelExt();
			this.panel6 = new Duzon.Common.Controls.PanelExt();
			this.panel7 = new Duzon.Common.Controls.PanelExt();
			this.panel4 = new Duzon.Common.Controls.PanelExt();
			this.lblOP = new Duzon.Common.Controls.LabelExt();
			this.lbl작업지시번호 = new Duzon.Common.Controls.LabelExt();
			this.panel5 = new Duzon.Common.Controls.PanelExt();
			this.lbl불량수량 = new Duzon.Common.Controls.LabelExt();
			this.lbl품목 = new Duzon.Common.Controls.LabelExt();
			this.lblWC = new Duzon.Common.Controls.LabelExt();
			this.lbl공장 = new Duzon.Common.Controls.LabelExt();
			this.txt공장 = new Duzon.Common.Controls.TextBoxExt();
			this.btn확인 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn취소 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this._flex = new Dass.FlexGrid.FlexGrid(this.components);
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.lbl불량종류 = new Duzon.Common.Controls.LabelExt();
			this.ctx불량종류 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl불량원인 = new Duzon.Common.Controls.LabelExt();
			this.ctx불량원인 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.btn적용 = new Duzon.Common.Controls.RoundedButton(this.components);
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panelExt1.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel5.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
			this.oneGridItem1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this._flex, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 3);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 47);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(696, 583);
			this.tableLayoutPanel1.TabIndex = 36;
			// 
			// panel3
			// 
			this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel3.Controls.Add(this.txtLOT번호);
			this.panel3.Controls.Add(this.panelExt1);
			this.panel3.Controls.Add(this.txt불량수량);
			this.panel3.Controls.Add(this.txt작업지시번호);
			this.panel3.Controls.Add(this.m_txtUnitIm);
			this.panel3.Controls.Add(this.m_txtStndItem);
			this.panel3.Controls.Add(this.m_txtNmItem);
			this.panel3.Controls.Add(this.m_txtCdItem);
			this.panel3.Controls.Add(this.txtOP);
			this.panel3.Controls.Add(this.txtWC);
			this.panel3.Controls.Add(this.panel8);
			this.panel3.Controls.Add(this.panel6);
			this.panel3.Controls.Add(this.panel7);
			this.panel3.Controls.Add(this.panel4);
			this.panel3.Controls.Add(this.panel5);
			this.panel3.Controls.Add(this.txt공장);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(3, 3);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(690, 101);
			this.panel3.TabIndex = 30;
			// 
			// txtLOT번호
			// 
			this.txtLOT번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txtLOT번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtLOT번호.Location = new System.Drawing.Point(414, 75);
			this.txtLOT번호.Name = "txtLOT번호";
			this.txtLOT번호.ReadOnly = true;
			this.txtLOT번호.Size = new System.Drawing.Size(170, 21);
			this.txtLOT번호.TabIndex = 9;
			this.txtLOT번호.TabStop = false;
			this.txtLOT번호.Visible = false;
			// 
			// panelExt1
			// 
			this.panelExt1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
			this.panelExt1.Controls.Add(this.lblLOT번호);
			this.panelExt1.Location = new System.Drawing.Point(326, 73);
			this.panelExt1.Name = "panelExt1";
			this.panelExt1.Size = new System.Drawing.Size(85, 25);
			this.panelExt1.TabIndex = 6;
			this.panelExt1.Visible = false;
			// 
			// lblLOT번호
			// 
			this.lblLOT번호.Location = new System.Drawing.Point(3, 4);
			this.lblLOT번호.Name = "lblLOT번호";
			this.lblLOT번호.Size = new System.Drawing.Size(80, 18);
			this.lblLOT번호.TabIndex = 3;
			this.lblLOT번호.Text = "LOT번호";
			this.lblLOT번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txt불량수량
			// 
			this.txt불량수량.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt불량수량.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt불량수량.Location = new System.Drawing.Point(89, 75);
			this.txt불량수량.Name = "txt불량수량";
			this.txt불량수량.ReadOnly = true;
			this.txt불량수량.Size = new System.Drawing.Size(170, 21);
			this.txt불량수량.TabIndex = 8;
			this.txt불량수량.TabStop = false;
			this.txt불량수량.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txt작업지시번호
			// 
			this.txt작업지시번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt작업지시번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt작업지시번호.Location = new System.Drawing.Point(414, 3);
			this.txt작업지시번호.Name = "txt작업지시번호";
			this.txt작업지시번호.ReadOnly = true;
			this.txt작업지시번호.Size = new System.Drawing.Size(170, 21);
			this.txt작업지시번호.TabIndex = 0;
			this.txt작업지시번호.TabStop = false;
			// 
			// m_txtUnitIm
			// 
			this.m_txtUnitIm.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.m_txtUnitIm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.m_txtUnitIm.Location = new System.Drawing.Point(517, 51);
			this.m_txtUnitIm.Name = "m_txtUnitIm";
			this.m_txtUnitIm.ReadOnly = true;
			this.m_txtUnitIm.Size = new System.Drawing.Size(67, 21);
			this.m_txtUnitIm.TabIndex = 0;
			this.m_txtUnitIm.TabStop = false;
			// 
			// m_txtStndItem
			// 
			this.m_txtStndItem.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.m_txtStndItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.m_txtStndItem.Location = new System.Drawing.Point(414, 51);
			this.m_txtStndItem.Name = "m_txtStndItem";
			this.m_txtStndItem.ReadOnly = true;
			this.m_txtStndItem.Size = new System.Drawing.Size(100, 21);
			this.m_txtStndItem.TabIndex = 0;
			this.m_txtStndItem.TabStop = false;
			// 
			// m_txtNmItem
			// 
			this.m_txtNmItem.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.m_txtNmItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.m_txtNmItem.Location = new System.Drawing.Point(262, 51);
			this.m_txtNmItem.Name = "m_txtNmItem";
			this.m_txtNmItem.ReadOnly = true;
			this.m_txtNmItem.Size = new System.Drawing.Size(149, 21);
			this.m_txtNmItem.TabIndex = 0;
			this.m_txtNmItem.TabStop = false;
			// 
			// m_txtCdItem
			// 
			this.m_txtCdItem.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.m_txtCdItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.m_txtCdItem.Location = new System.Drawing.Point(89, 51);
			this.m_txtCdItem.Name = "m_txtCdItem";
			this.m_txtCdItem.ReadOnly = true;
			this.m_txtCdItem.Size = new System.Drawing.Size(170, 21);
			this.m_txtCdItem.TabIndex = 0;
			this.m_txtCdItem.TabStop = false;
			// 
			// txtOP
			// 
			this.txtOP.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txtOP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtOP.Location = new System.Drawing.Point(414, 27);
			this.txtOP.Name = "txtOP";
			this.txtOP.ReadOnly = true;
			this.txtOP.Size = new System.Drawing.Size(170, 21);
			this.txtOP.TabIndex = 0;
			this.txtOP.TabStop = false;
			// 
			// txtWC
			// 
			this.txtWC.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txtWC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtWC.Location = new System.Drawing.Point(89, 27);
			this.txtWC.Name = "txtWC";
			this.txtWC.ReadOnly = true;
			this.txtWC.Size = new System.Drawing.Size(170, 21);
			this.txtWC.TabIndex = 0;
			this.txtWC.TabStop = false;
			// 
			// panel8
			// 
			this.panel8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel8.BackColor = System.Drawing.Color.Transparent;
			this.panel8.Location = new System.Drawing.Point(5, 73);
			this.panel8.Name = "panel8";
			this.panel8.Size = new System.Drawing.Size(680, 1);
			this.panel8.TabIndex = 7;
			// 
			// panel6
			// 
			this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel6.BackColor = System.Drawing.Color.Transparent;
			this.panel6.Location = new System.Drawing.Point(5, 49);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(680, 1);
			this.panel6.TabIndex = 6;
			// 
			// panel7
			// 
			this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel7.BackColor = System.Drawing.Color.Transparent;
			this.panel7.Location = new System.Drawing.Point(6, 25);
			this.panel7.Name = "panel7";
			this.panel7.Size = new System.Drawing.Size(680, 1);
			this.panel7.TabIndex = 4;
			// 
			// panel4
			// 
			this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
			this.panel4.Controls.Add(this.lblOP);
			this.panel4.Controls.Add(this.lbl작업지시번호);
			this.panel4.Location = new System.Drawing.Point(326, 1);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(85, 49);
			this.panel4.TabIndex = 5;
			// 
			// lblOP
			// 
			this.lblOP.Location = new System.Drawing.Point(3, 28);
			this.lblOP.Name = "lblOP";
			this.lblOP.Size = new System.Drawing.Size(80, 18);
			this.lblOP.TabIndex = 2;
			this.lblOP.Tag = "OP";
			this.lblOP.Text = "OP";
			this.lblOP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl작업지시번호
			// 
			this.lbl작업지시번호.Location = new System.Drawing.Point(3, 4);
			this.lbl작업지시번호.Name = "lbl작업지시번호";
			this.lbl작업지시번호.Size = new System.Drawing.Size(80, 18);
			this.lbl작업지시번호.TabIndex = 1;
			this.lbl작업지시번호.Tag = "NO_WO";
			this.lbl작업지시번호.Text = "작업지시번호";
			this.lbl작업지시번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel5
			// 
			this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
			this.panel5.Controls.Add(this.lbl불량수량);
			this.panel5.Controls.Add(this.lbl품목);
			this.panel5.Controls.Add(this.lblWC);
			this.panel5.Controls.Add(this.lbl공장);
			this.panel5.Location = new System.Drawing.Point(1, 1);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(85, 97);
			this.panel5.TabIndex = 3;
			// 
			// lbl불량수량
			// 
			this.lbl불량수량.Location = new System.Drawing.Point(3, 76);
			this.lbl불량수량.Name = "lbl불량수량";
			this.lbl불량수량.Size = new System.Drawing.Size(80, 18);
			this.lbl불량수량.TabIndex = 4;
			this.lbl불량수량.Tag = "QT_REJECT";
			this.lbl불량수량.Text = "불량수량";
			this.lbl불량수량.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl품목
			// 
			this.lbl품목.Location = new System.Drawing.Point(3, 52);
			this.lbl품목.Name = "lbl품목";
			this.lbl품목.Size = new System.Drawing.Size(80, 18);
			this.lbl품목.TabIndex = 3;
			this.lbl품목.Tag = "ITEM";
			this.lbl품목.Text = "품목";
			this.lbl품목.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblWC
			// 
			this.lblWC.Location = new System.Drawing.Point(3, 28);
			this.lblWC.Name = "lblWC";
			this.lblWC.Size = new System.Drawing.Size(80, 18);
			this.lblWC.TabIndex = 2;
			this.lblWC.Tag = "WC";
			this.lblWC.Text = "W/C";
			this.lblWC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl공장
			// 
			this.lbl공장.Location = new System.Drawing.Point(3, 4);
			this.lbl공장.Name = "lbl공장";
			this.lbl공장.Size = new System.Drawing.Size(80, 18);
			this.lbl공장.TabIndex = 0;
			this.lbl공장.Tag = "PLANT";
			this.lbl공장.Text = "공장";
			this.lbl공장.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txt공장
			// 
			this.txt공장.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt공장.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt공장.Location = new System.Drawing.Point(89, 3);
			this.txt공장.Name = "txt공장";
			this.txt공장.ReadOnly = true;
			this.txt공장.Size = new System.Drawing.Size(170, 21);
			this.txt공장.TabIndex = 0;
			this.txt공장.TabStop = false;
			// 
			// btn확인
			// 
			this.btn확인.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn확인.BackColor = System.Drawing.Color.White;
			this.btn확인.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn확인.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn확인.Location = new System.Drawing.Point(561, 3);
			this.btn확인.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn확인.Name = "btn확인";
			this.btn확인.Size = new System.Drawing.Size(60, 19);
			this.btn확인.TabIndex = 3;
			this.btn확인.TabStop = false;
			this.btn확인.Tag = "OK";
			this.btn확인.Text = "확인";
			this.btn확인.UseVisualStyleBackColor = false;
			// 
			// btn취소
			// 
			this.btn취소.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn취소.BackColor = System.Drawing.Color.White;
			this.btn취소.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn취소.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btn취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn취소.Location = new System.Drawing.Point(627, 3);
			this.btn취소.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn취소.Name = "btn취소";
			this.btn취소.Size = new System.Drawing.Size(60, 19);
			this.btn취소.TabIndex = 4;
			this.btn취소.TabStop = false;
			this.btn취소.Tag = "CANCEL";
			this.btn취소.Text = "취소";
			this.btn취소.UseVisualStyleBackColor = false;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.btn취소);
			this.flowLayoutPanel1.Controls.Add(this.btn확인);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 110);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(690, 25);
			this.flowLayoutPanel1.TabIndex = 31;
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
			this._flex.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
			this._flex.Location = new System.Drawing.Point(3, 141);
			this._flex.Name = "_flex";
			this._flex.Rows.Count = 1;
			this._flex.Rows.DefaultSize = 18;
			this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex.ShowSort = false;
			this._flex.Size = new System.Drawing.Size(690, 393);
			this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
			this._flex.TabIndex = 2;
			this._flex.UseGridCalculator = true;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
			this.oneGrid1.Location = new System.Drawing.Point(3, 540);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(690, 40);
			this.oneGrid1.TabIndex = 32;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.btn적용);
			this.oneGridItem1.Controls.Add(this.ctx불량원인);
			this.oneGridItem1.Controls.Add(this.lbl불량원인);
			this.oneGridItem1.Controls.Add(this.ctx불량종류);
			this.oneGridItem1.Controls.Add(this.lbl불량종류);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(680, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// lbl불량종류
			// 
			this.lbl불량종류.Location = new System.Drawing.Point(2, 1);
			this.lbl불량종류.Name = "lbl불량종류";
			this.lbl불량종류.Size = new System.Drawing.Size(80, 18);
			this.lbl불량종류.TabIndex = 6;
			this.lbl불량종류.Tag = "QT_REJECT";
			this.lbl불량종류.Text = "불량종류";
			this.lbl불량종류.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ctx불량종류
			// 
			this.ctx불량종류.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_TABLE_SUB;
			this.ctx불량종류.Location = new System.Drawing.Point(84, 1);
			this.ctx불량종류.Name = "ctx불량종류";
			this.ctx불량종류.Size = new System.Drawing.Size(170, 21);
			this.ctx불량종류.TabIndex = 17;
			this.ctx불량종류.TabStop = false;
			this.ctx불량종류.Text = "bpCodeTextBox1";
			// 
			// lbl불량원인
			// 
			this.lbl불량원인.Location = new System.Drawing.Point(256, 1);
			this.lbl불량원인.Name = "lbl불량원인";
			this.lbl불량원인.Size = new System.Drawing.Size(80, 18);
			this.lbl불량원인.TabIndex = 20;
			this.lbl불량원인.Tag = "QT_REJECT";
			this.lbl불량원인.Text = "불량원인";
			this.lbl불량원인.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ctx불량원인
			// 
			this.ctx불량원인.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_TABLE_SUB1;
			this.ctx불량원인.Location = new System.Drawing.Point(338, 1);
			this.ctx불량원인.Name = "ctx불량원인";
			this.ctx불량원인.Size = new System.Drawing.Size(170, 21);
			this.ctx불량원인.TabIndex = 21;
			this.ctx불량원인.TabStop = false;
			this.ctx불량원인.Text = "bpCodeTextBox2";
			// 
			// btn적용
			// 
			this.btn적용.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn적용.BackColor = System.Drawing.Color.White;
			this.btn적용.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn적용.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn적용.Location = new System.Drawing.Point(510, 1);
			this.btn적용.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn적용.Name = "btn적용";
			this.btn적용.Size = new System.Drawing.Size(60, 19);
			this.btn적용.TabIndex = 22;
			this.btn적용.TabStop = false;
			this.btn적용.Tag = "OK";
			this.btn적용.Text = "적용";
			this.btn적용.UseVisualStyleBackColor = false;
			// 
			// P_CZ_PR_WORK_SUB2
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.CancelButton = this.btn취소;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(696, 630);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.ForeColor = System.Drawing.Color.Black;
			this.MaximizeBox = false;
			this.Name = "P_CZ_PR_WORK_SUB2";
			this.Padding = new System.Windows.Forms.Padding(4);
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "ERP iU";
			this.TitleText = "불량내역등록";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.panelExt1.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel5.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
			this.oneGridItem1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private Duzon.Common.Controls.PanelExt panel3;
		private Duzon.Common.Controls.TextBoxExt txtLOT번호;
		private Duzon.Common.Controls.PanelExt panelExt1;
		private Duzon.Common.Controls.LabelExt lblLOT번호;
		private Duzon.Common.Controls.TextBoxExt txt불량수량;
		private Duzon.Common.Controls.TextBoxExt txt작업지시번호;
		private Duzon.Common.Controls.TextBoxExt m_txtUnitIm;
		private Duzon.Common.Controls.TextBoxExt m_txtStndItem;
		private Duzon.Common.Controls.TextBoxExt m_txtNmItem;
		private Duzon.Common.Controls.TextBoxExt m_txtCdItem;
		private Duzon.Common.Controls.TextBoxExt txtOP;
		private Duzon.Common.Controls.TextBoxExt txtWC;
		private Duzon.Common.Controls.PanelExt panel8;
		private Duzon.Common.Controls.PanelExt panel6;
		private Duzon.Common.Controls.PanelExt panel7;
		private Duzon.Common.Controls.PanelExt panel4;
		private Duzon.Common.Controls.LabelExt lblOP;
		private Duzon.Common.Controls.LabelExt lbl작업지시번호;
		private Duzon.Common.Controls.PanelExt panel5;
		private Duzon.Common.Controls.LabelExt lbl불량수량;
		private Duzon.Common.Controls.LabelExt lbl품목;
		private Duzon.Common.Controls.LabelExt lblWC;
		private Duzon.Common.Controls.LabelExt lbl공장;
		private Duzon.Common.Controls.TextBoxExt txt공장;
		private Dass.FlexGrid.FlexGrid _flex;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private Duzon.Common.Controls.RoundedButton btn취소;
		private Duzon.Common.Controls.RoundedButton btn확인;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Common.Controls.LabelExt lbl불량종류;
		private Duzon.Common.Controls.RoundedButton btn적용;
		private Duzon.Common.BpControls.BpCodeTextBox ctx불량원인;
		private Duzon.Common.Controls.LabelExt lbl불량원인;
		private Duzon.Common.BpControls.BpCodeTextBox ctx불량종류;
	}
}
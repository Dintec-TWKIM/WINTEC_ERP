
using C1.Win.C1FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Erpiu.Windows.OneControls;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
	partial class P_CZ_MA_PITEM_COMPANY_TRANS_SUB
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_MA_PITEM_COMPANY_TRANS_SUB));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this._flex = new Dass.FlexGrid.FlexGrid(this.components);
			this.panel3 = new Duzon.Common.Controls.PanelExt();
			this.m_lblTitle = new Duzon.Common.Controls.LabelExt();
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this._lblPlant = new Duzon.Common.Controls.LabelExt();
			this._txtCdPlant_F = new Duzon.Common.Controls.TextBoxExt();
			this.bpPanelControl20 = new Duzon.Common.BpControls.BpPanelControl();
			this._lblCompany = new Duzon.Common.Controls.LabelExt();
			this._txtCdCompany_F = new Duzon.Common.Controls.TextBoxExt();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btn확인 = new Duzon.Common.Controls.RoundedButton(this.components);
			this._btnSave = new Duzon.Common.Controls.RoundedButton(this.components);
			this._btnDel = new Duzon.Common.Controls.RoundedButton(this.components);
			this._btnAdd = new Duzon.Common.Controls.RoundedButton(this.components);
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
			this.panel3.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.bpPanelControl20.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this._flex, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 69F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(590, 489);
			this.tableLayoutPanel1.TabIndex = 0;
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
			this._flex.Location = new System.Drawing.Point(3, 123);
			this._flex.Name = "_flex";
			this._flex.Rows.Count = 1;
			this._flex.Rows.DefaultSize = 18;
			this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex.ShowSort = false;
			this._flex.Size = new System.Drawing.Size(584, 363);
			this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
			this._flex.TabIndex = 122;
			this._flex.UseGridCalculator = true;
			// 
			// panel3
			// 
			this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.panel3.Controls.Add(this.m_lblTitle);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(3, 3);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(584, 45);
			this.panel3.TabIndex = 119;
			// 
			// m_lblTitle
			// 
			this.m_lblTitle.AutoSize = true;
			this.m_lblTitle.BackColor = System.Drawing.Color.Transparent;
			this.m_lblTitle.Font = new System.Drawing.Font("굴림체", 10F, System.Drawing.FontStyle.Bold);
			this.m_lblTitle.ForeColor = System.Drawing.Color.White;
			this.m_lblTitle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.m_lblTitle.Location = new System.Drawing.Point(15, 15);
			this.m_lblTitle.Name = "m_lblTitle";
			this.m_lblTitle.Resizeble = false;
			this.m_lblTitle.Size = new System.Drawing.Size(158, 14);
			this.m_lblTitle.TabIndex = 8;
			this.m_lblTitle.Tag = "BL_TEXT";
			this.m_lblTitle.Text = "회사간 품목전송 설정";
			this.m_lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Top;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2});
			this.oneGrid1.Location = new System.Drawing.Point(3, 54);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(584, 62);
			this.oneGrid1.TabIndex = 123;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.Controls.Add(this.bpPanelControl20);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(574, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this._lblPlant);
			this.bpPanelControl1.Controls.Add(this._txtCdPlant_F);
			this.bpPanelControl1.Location = new System.Drawing.Point(287, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(283, 23);
			this.bpPanelControl1.TabIndex = 2;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// _lblPlant
			// 
			this._lblPlant.BackColor = System.Drawing.Color.Transparent;
			this._lblPlant.Location = new System.Drawing.Point(4, 2);
			this._lblPlant.Name = "_lblPlant";
			this._lblPlant.Size = new System.Drawing.Size(105, 18);
			this._lblPlant.TabIndex = 124;
			this._lblPlant.Text = "현재공장";
			this._lblPlant.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _txtCdPlant_F
			// 
			this._txtCdPlant_F.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this._txtCdPlant_F.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this._txtCdPlant_F.Location = new System.Drawing.Point(115, 1);
			this._txtCdPlant_F.Name = "_txtCdPlant_F";
			this._txtCdPlant_F.ReadOnly = true;
			this._txtCdPlant_F.Size = new System.Drawing.Size(165, 21);
			this._txtCdPlant_F.TabIndex = 128;
			this._txtCdPlant_F.TabStop = false;
			// 
			// bpPanelControl20
			// 
			this.bpPanelControl20.Controls.Add(this._lblCompany);
			this.bpPanelControl20.Controls.Add(this._txtCdCompany_F);
			this.bpPanelControl20.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl20.Name = "bpPanelControl20";
			this.bpPanelControl20.Size = new System.Drawing.Size(283, 23);
			this.bpPanelControl20.TabIndex = 1;
			this.bpPanelControl20.Text = "bpPanelControl20";
			// 
			// _lblCompany
			// 
			this._lblCompany.BackColor = System.Drawing.Color.Transparent;
			this._lblCompany.Location = new System.Drawing.Point(5, 2);
			this._lblCompany.Name = "_lblCompany";
			this._lblCompany.Size = new System.Drawing.Size(107, 18);
			this._lblCompany.TabIndex = 124;
			this._lblCompany.Text = "현재회사";
			this._lblCompany.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _txtCdCompany_F
			// 
			this._txtCdCompany_F.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this._txtCdCompany_F.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this._txtCdCompany_F.Location = new System.Drawing.Point(118, 1);
			this._txtCdCompany_F.Name = "_txtCdCompany_F";
			this._txtCdCompany_F.ReadOnly = true;
			this._txtCdCompany_F.Size = new System.Drawing.Size(165, 21);
			this._txtCdCompany_F.TabIndex = 128;
			this._txtCdCompany_F.TabStop = false;
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.flowLayoutPanel1);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(574, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanel1.Controls.Add(this.btn확인);
			this.flowLayoutPanel1.Controls.Add(this._btnSave);
			this.flowLayoutPanel1.Controls.Add(this._btnDel);
			this.flowLayoutPanel1.Controls.Add(this._btnAdd);
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(2, 1);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(568, 23);
			this.flowLayoutPanel1.TabIndex = 122;
			// 
			// btn확인
			// 
			this.btn확인.BackColor = System.Drawing.Color.White;
			this.btn확인.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn확인.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn확인.Location = new System.Drawing.Point(509, 3);
			this.btn확인.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn확인.Name = "btn확인";
			this.btn확인.Size = new System.Drawing.Size(56, 19);
			this.btn확인.TabIndex = 121;
			this.btn확인.TabStop = false;
			this.btn확인.Text = "확인";
			this.btn확인.UseVisualStyleBackColor = true;
			// 
			// _btnSave
			// 
			this._btnSave.BackColor = System.Drawing.Color.White;
			this._btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
			this._btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this._btnSave.Location = new System.Drawing.Point(447, 3);
			this._btnSave.MaximumSize = new System.Drawing.Size(0, 19);
			this._btnSave.Name = "_btnSave";
			this._btnSave.Size = new System.Drawing.Size(56, 19);
			this._btnSave.TabIndex = 120;
			this._btnSave.TabStop = false;
			this._btnSave.Text = "저장";
			this._btnSave.UseVisualStyleBackColor = true;
			// 
			// _btnDel
			// 
			this._btnDel.BackColor = System.Drawing.Color.White;
			this._btnDel.Cursor = System.Windows.Forms.Cursors.Hand;
			this._btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this._btnDel.Location = new System.Drawing.Point(385, 3);
			this._btnDel.MaximumSize = new System.Drawing.Size(0, 19);
			this._btnDel.Name = "_btnDel";
			this._btnDel.Size = new System.Drawing.Size(56, 19);
			this._btnDel.TabIndex = 122;
			this._btnDel.TabStop = false;
			this._btnDel.Text = "삭제";
			this._btnDel.UseVisualStyleBackColor = true;
			this._btnDel.Visible = false;
			// 
			// _btnAdd
			// 
			this._btnAdd.BackColor = System.Drawing.Color.White;
			this._btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
			this._btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this._btnAdd.Location = new System.Drawing.Point(323, 3);
			this._btnAdd.MaximumSize = new System.Drawing.Size(0, 19);
			this._btnAdd.Name = "_btnAdd";
			this._btnAdd.Size = new System.Drawing.Size(56, 19);
			this._btnAdd.TabIndex = 123;
			this._btnAdd.TabStop = false;
			this._btnAdd.Text = "추가";
			this._btnAdd.UseVisualStyleBackColor = true;
			this._btnAdd.Visible = false;
			// 
			// P_CZ_MA_PITEM_COMPANY_TRANS_SUB
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(590, 489);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "P_CZ_MA_PITEM_COMPANY_TRANS_SUB";
			this.Text = "전용코드 등록 상세도움창";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.bpPanelControl1.PerformLayout();
			this.bpPanelControl20.ResumeLayout(false);
			this.bpPanelControl20.PerformLayout();
			this.oneGridItem2.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

		#endregion

		private TableLayoutPanel tableLayoutPanel1;
		private PanelExt panel3;
		private LabelExt m_lblTitle;
		private RoundedButton btn확인;
		private RoundedButton _btnSave;
		private Dass.FlexGrid.FlexGrid _flex;
		private FlowLayoutPanel flowLayoutPanel1;
		private LabelExt _lblCompany;
		private TextBoxExt _txtCdCompany_F;
		private OneGrid oneGrid1;
		private OneGridItem oneGridItem1;
		private BpPanelControl bpPanelControl1;
		private LabelExt _lblPlant;
		private TextBoxExt _txtCdPlant_F;
		private BpPanelControl bpPanelControl20;
		private OneGridItem oneGridItem2;
		private RoundedButton _btnDel;
		private RoundedButton _btnAdd;
	}
}
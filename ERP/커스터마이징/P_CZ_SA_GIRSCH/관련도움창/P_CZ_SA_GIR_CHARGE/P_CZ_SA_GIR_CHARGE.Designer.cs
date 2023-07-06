
namespace cz
{
    partial class P_CZ_SA_GIR_CHARGE
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_SA_GIR_CHARGE));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btn저장 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn제거 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn추가 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn조회 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo비용청구방법 = new Duzon.Common.Controls.DropDownComboBox();
			this.lbl비용청구방법 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt통화명 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl통화명 = new Duzon.Common.Controls.LabelExt();
			this._flex = new Dass.FlexGrid.FlexGrid(this.components);
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
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
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this._flex, 0, 2);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 49);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(745, 537);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.btn저장);
			this.flowLayoutPanel1.Controls.Add(this.btn제거);
			this.flowLayoutPanel1.Controls.Add(this.btn추가);
			this.flowLayoutPanel1.Controls.Add(this.btn조회);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 51);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(739, 25);
			this.flowLayoutPanel1.TabIndex = 1;
			// 
			// btn저장
			// 
			this.btn저장.BackColor = System.Drawing.Color.Transparent;
			this.btn저장.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn저장.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn저장.Location = new System.Drawing.Point(666, 3);
			this.btn저장.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn저장.Name = "btn저장";
			this.btn저장.Size = new System.Drawing.Size(70, 19);
			this.btn저장.TabIndex = 3;
			this.btn저장.TabStop = false;
			this.btn저장.Text = "저장";
			this.btn저장.UseVisualStyleBackColor = false;
			// 
			// btn제거
			// 
			this.btn제거.BackColor = System.Drawing.Color.Transparent;
			this.btn제거.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn제거.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn제거.Location = new System.Drawing.Point(590, 3);
			this.btn제거.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn제거.Name = "btn제거";
			this.btn제거.Size = new System.Drawing.Size(70, 19);
			this.btn제거.TabIndex = 1;
			this.btn제거.TabStop = false;
			this.btn제거.Text = "제거";
			this.btn제거.UseVisualStyleBackColor = false;
			// 
			// btn추가
			// 
			this.btn추가.BackColor = System.Drawing.Color.Transparent;
			this.btn추가.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn추가.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn추가.Location = new System.Drawing.Point(514, 3);
			this.btn추가.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn추가.Name = "btn추가";
			this.btn추가.Size = new System.Drawing.Size(70, 19);
			this.btn추가.TabIndex = 0;
			this.btn추가.TabStop = false;
			this.btn추가.Text = "추가";
			this.btn추가.UseVisualStyleBackColor = false;
			// 
			// btn조회
			// 
			this.btn조회.BackColor = System.Drawing.Color.Transparent;
			this.btn조회.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn조회.Location = new System.Drawing.Point(438, 3);
			this.btn조회.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn조회.Name = "btn조회";
			this.btn조회.Size = new System.Drawing.Size(70, 19);
			this.btn조회.TabIndex = 2;
			this.btn조회.TabStop = false;
			this.btn조회.Text = "조회";
			this.btn조회.UseVisualStyleBackColor = false;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(739, 42);
			this.oneGrid1.TabIndex = 2;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl2);
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(729, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.cbo비용청구방법);
			this.bpPanelControl2.Controls.Add(this.lbl비용청구방법);
			this.bpPanelControl2.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl2.TabIndex = 2;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// cbo비용청구방법
			// 
			this.cbo비용청구방법.AutoDropDown = true;
			this.cbo비용청구방법.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo비용청구방법.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo비용청구방법.FormattingEnabled = true;
			this.cbo비용청구방법.ItemHeight = 12;
			this.cbo비용청구방법.Location = new System.Drawing.Point(106, 0);
			this.cbo비용청구방법.Name = "cbo비용청구방법";
			this.cbo비용청구방법.Size = new System.Drawing.Size(186, 20);
			this.cbo비용청구방법.TabIndex = 1;
			// 
			// lbl비용청구방법
			// 
			this.lbl비용청구방법.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl비용청구방법.Location = new System.Drawing.Point(0, 0);
			this.lbl비용청구방법.Name = "lbl비용청구방법";
			this.lbl비용청구방법.Size = new System.Drawing.Size(100, 23);
			this.lbl비용청구방법.TabIndex = 0;
			this.lbl비용청구방법.Text = "비용청구방법";
			this.lbl비용청구방법.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.txt통화명);
			this.bpPanelControl1.Controls.Add(this.lbl통화명);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl1.TabIndex = 1;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// txt통화명
			// 
			this.txt통화명.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt통화명.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt통화명.Dock = System.Windows.Forms.DockStyle.Right;
			this.txt통화명.Enabled = false;
			this.txt통화명.Location = new System.Drawing.Point(106, 0);
			this.txt통화명.Name = "txt통화명";
			this.txt통화명.Size = new System.Drawing.Size(186, 21);
			this.txt통화명.TabIndex = 1;
			// 
			// lbl통화명
			// 
			this.lbl통화명.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl통화명.Location = new System.Drawing.Point(0, 0);
			this.lbl통화명.Name = "lbl통화명";
			this.lbl통화명.Size = new System.Drawing.Size(100, 23);
			this.lbl통화명.TabIndex = 0;
			this.lbl통화명.Text = "통화명";
			this.lbl통화명.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			this._flex.Location = new System.Drawing.Point(3, 82);
			this._flex.Name = "_flex";
			this._flex.Rows.Count = 1;
			this._flex.Rows.DefaultSize = 18;
			this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex.ShowSort = false;
			this._flex.Size = new System.Drawing.Size(739, 452);
			this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
			this._flex.TabIndex = 3;
			this._flex.UseGridCalculator = true;
			// 
			// P_CZ_SA_GIR_CHARGE
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(749, 589);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "P_CZ_SA_GIR_CHARGE";
			this.Text = "P_CZ_SA_GIR_CHARGE";
			this.TitleText = "부대비용등록";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl2.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.bpPanelControl1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Duzon.Common.Controls.RoundedButton btn제거;
        private Duzon.Common.Controls.RoundedButton btn추가;
        private Duzon.Common.Controls.RoundedButton btn조회;
        private Duzon.Common.Controls.RoundedButton btn저장;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
		private Dass.FlexGrid.FlexGrid _flex;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Common.Controls.LabelExt lbl통화명;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
		private Duzon.Common.Controls.TextBoxExt txt통화명;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
		private Duzon.Common.Controls.DropDownComboBox cbo비용청구방법;
		private Duzon.Common.Controls.LabelExt lbl비용청구방법;
	}
}
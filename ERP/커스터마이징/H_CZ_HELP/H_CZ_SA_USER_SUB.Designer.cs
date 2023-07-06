namespace cz
{
    partial class H_CZ_SA_USER_SUB
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(H_CZ_SA_USER_SUB));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo재직구분 = new Duzon.Common.Controls.DropDownComboBox();
			this.lbl재직구분 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo담당업무 = new Duzon.Common.Controls.DropDownComboBox();
			this.lbl담당업무 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.bpc회사 = new Duzon.Common.BpControls.BpComboBox();
			this.lbl회사 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt검색 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl검색 = new Duzon.Common.Controls.LabelExt();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btn취소 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn확인 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn검색 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn멀티검색 = new Duzon.Common.Controls.RoundedButton(this.components);
			this._flex = new Dass.FlexGridLight.FlexGrid(this.components);
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl4.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.bpPanelControl3.SuspendLayout();
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
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 47);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(867, 404);
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
			this.oneGrid1.Size = new System.Drawing.Size(861, 62);
			this.oneGrid1.TabIndex = 0;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl4);
			this.oneGridItem1.Controls.Add(this.bpPanelControl2);
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(851, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl4
			// 
			this.bpPanelControl4.Controls.Add(this.cbo재직구분);
			this.bpPanelControl4.Controls.Add(this.lbl재직구분);
			this.bpPanelControl4.Location = new System.Drawing.Point(566, 1);
			this.bpPanelControl4.Name = "bpPanelControl4";
			this.bpPanelControl4.Size = new System.Drawing.Size(280, 23);
			this.bpPanelControl4.TabIndex = 3;
			this.bpPanelControl4.Text = "bpPanelControl4";
			// 
			// cbo재직구분
			// 
			this.cbo재직구분.AutoDropDown = true;
			this.cbo재직구분.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo재직구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo재직구분.FormattingEnabled = true;
			this.cbo재직구분.ItemHeight = 12;
			this.cbo재직구분.Location = new System.Drawing.Point(106, 0);
			this.cbo재직구분.Name = "cbo재직구분";
			this.cbo재직구분.Size = new System.Drawing.Size(174, 20);
			this.cbo재직구분.TabIndex = 1;
			// 
			// lbl재직구분
			// 
			this.lbl재직구분.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl재직구분.Location = new System.Drawing.Point(0, 0);
			this.lbl재직구분.Name = "lbl재직구분";
			this.lbl재직구분.Size = new System.Drawing.Size(100, 23);
			this.lbl재직구분.TabIndex = 0;
			this.lbl재직구분.Text = "재직구분";
			this.lbl재직구분.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.cbo담당업무);
			this.bpPanelControl2.Controls.Add(this.lbl담당업무);
			this.bpPanelControl2.Location = new System.Drawing.Point(284, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(280, 23);
			this.bpPanelControl2.TabIndex = 2;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// cbo담당업무
			// 
			this.cbo담당업무.AutoDropDown = true;
			this.cbo담당업무.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo담당업무.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo담당업무.FormattingEnabled = true;
			this.cbo담당업무.ItemHeight = 12;
			this.cbo담당업무.Location = new System.Drawing.Point(106, 0);
			this.cbo담당업무.Name = "cbo담당업무";
			this.cbo담당업무.Size = new System.Drawing.Size(174, 20);
			this.cbo담당업무.TabIndex = 1;
			// 
			// lbl담당업무
			// 
			this.lbl담당업무.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl담당업무.Location = new System.Drawing.Point(0, 0);
			this.lbl담당업무.Name = "lbl담당업무";
			this.lbl담당업무.Size = new System.Drawing.Size(100, 23);
			this.lbl담당업무.TabIndex = 0;
			this.lbl담당업무.Text = "담당업무";
			this.lbl담당업무.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.bpc회사);
			this.bpPanelControl1.Controls.Add(this.lbl회사);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(280, 23);
			this.bpPanelControl1.TabIndex = 0;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// bpc회사
			// 
			this.bpc회사.Dock = System.Windows.Forms.DockStyle.Right;
			this.bpc회사.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
			this.bpc회사.Location = new System.Drawing.Point(106, 0);
			this.bpc회사.Name = "bpc회사";
			this.bpc회사.Size = new System.Drawing.Size(174, 21);
			this.bpc회사.TabIndex = 1;
			this.bpc회사.TabStop = false;
			this.bpc회사.Text = "bpComboBox1";
			this.bpc회사.UserCodeName = "NM_COMPANY";
			this.bpc회사.UserCodeValue = "CD_COMPANY";
			this.bpc회사.UserHelpID = "H_CZ_MA_COMPANY_SUB";
			this.bpc회사.UserParams = "회사;H_CZ_MA_COMPANY_SUB";
			// 
			// lbl회사
			// 
			this.lbl회사.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl회사.Location = new System.Drawing.Point(0, 0);
			this.lbl회사.Name = "lbl회사";
			this.lbl회사.Size = new System.Drawing.Size(100, 23);
			this.lbl회사.TabIndex = 0;
			this.lbl회사.Text = "회사";
			this.lbl회사.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.bpPanelControl3);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(851, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.txt검색);
			this.bpPanelControl3.Controls.Add(this.lbl검색);
			this.bpPanelControl3.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(845, 23);
			this.bpPanelControl3.TabIndex = 2;
			this.bpPanelControl3.Text = "bpPanelControl3";
			// 
			// txt검색
			// 
			this.txt검색.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt검색.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt검색.Dock = System.Windows.Forms.DockStyle.Right;
			this.txt검색.Location = new System.Drawing.Point(105, 0);
			this.txt검색.Name = "txt검색";
			this.txt검색.Size = new System.Drawing.Size(740, 21);
			this.txt검색.TabIndex = 1;
			// 
			// lbl검색
			// 
			this.lbl검색.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl검색.Location = new System.Drawing.Point(0, 0);
			this.lbl검색.Name = "lbl검색";
			this.lbl검색.Size = new System.Drawing.Size(100, 23);
			this.lbl검색.TabIndex = 0;
			this.lbl검색.Text = "검색";
			this.lbl검색.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.btn취소);
			this.flowLayoutPanel1.Controls.Add(this.btn확인);
			this.flowLayoutPanel1.Controls.Add(this.btn검색);
			this.flowLayoutPanel1.Controls.Add(this.btn멀티검색);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 71);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(861, 25);
			this.flowLayoutPanel1.TabIndex = 2;
			// 
			// btn취소
			// 
			this.btn취소.BackColor = System.Drawing.Color.Transparent;
			this.btn취소.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn취소.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btn취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn취소.Location = new System.Drawing.Point(788, 3);
			this.btn취소.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn취소.Name = "btn취소";
			this.btn취소.Size = new System.Drawing.Size(70, 19);
			this.btn취소.TabIndex = 0;
			this.btn취소.TabStop = false;
			this.btn취소.Text = "취소";
			this.btn취소.UseVisualStyleBackColor = false;
			// 
			// btn확인
			// 
			this.btn확인.BackColor = System.Drawing.Color.Transparent;
			this.btn확인.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn확인.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn확인.Location = new System.Drawing.Point(712, 3);
			this.btn확인.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn확인.Name = "btn확인";
			this.btn확인.Size = new System.Drawing.Size(70, 19);
			this.btn확인.TabIndex = 1;
			this.btn확인.TabStop = false;
			this.btn확인.Text = "확인";
			this.btn확인.UseVisualStyleBackColor = false;
			// 
			// btn검색
			// 
			this.btn검색.BackColor = System.Drawing.Color.Transparent;
			this.btn검색.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn검색.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn검색.Location = new System.Drawing.Point(636, 3);
			this.btn검색.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn검색.Name = "btn검색";
			this.btn검색.Size = new System.Drawing.Size(70, 19);
			this.btn검색.TabIndex = 2;
			this.btn검색.TabStop = false;
			this.btn검색.Text = "검색";
			this.btn검색.UseVisualStyleBackColor = false;
			// 
			// btn멀티검색
			// 
			this.btn멀티검색.BackColor = System.Drawing.Color.Transparent;
			this.btn멀티검색.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn멀티검색.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn멀티검색.Location = new System.Drawing.Point(560, 3);
			this.btn멀티검색.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn멀티검색.Name = "btn멀티검색";
			this.btn멀티검색.Size = new System.Drawing.Size(70, 19);
			this.btn멀티검색.TabIndex = 3;
			this.btn멀티검색.TabStop = false;
			this.btn멀티검색.Text = "멀티검색";
			this.btn멀티검색.UseVisualStyleBackColor = false;
			// 
			// _flex
			// 
			this._flex.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex.AutoResize = false;
			this._flex.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex.Location = new System.Drawing.Point(3, 102);
			this._flex.Name = "_flex";
			this._flex.Rows.Count = 1;
			this._flex.Rows.DefaultSize = 18;
			this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex.ShowSort = false;
			this._flex.Size = new System.Drawing.Size(861, 299);
			this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
			this._flex.TabIndex = 3;
			// 
			// H_CZ_SA_USER_SUB
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.CancelButton = this.btn취소;
			this.ClientSize = new System.Drawing.Size(867, 451);
			this.Controls.Add(this.tableLayoutPanel1);
			this.MinimizeBox = false;
			this.Name = "H_CZ_SA_USER_SUB";
			this.ShowIcon = true;
			this.ShowInTaskbar = true;
			this.Text = "ERP iU";
			this.TitleText = "담당자";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl4.ResumeLayout(false);
			this.bpPanelControl2.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.oneGridItem2.ResumeLayout(false);
			this.bpPanelControl3.ResumeLayout(false);
			this.bpPanelControl3.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Duzon.Common.Controls.RoundedButton btn취소;
        private Duzon.Common.Controls.RoundedButton btn확인;
        private Duzon.Common.Controls.RoundedButton btn검색;
        private Dass.FlexGridLight.FlexGrid _flex;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.LabelExt lbl회사;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.Controls.LabelExt lbl담당업무;
        private Duzon.Common.BpControls.BpComboBox bpc회사;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.Controls.TextBoxExt txt검색;
        private Duzon.Common.Controls.LabelExt lbl검색;
        private Duzon.Common.Controls.DropDownComboBox cbo담당업무;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.Controls.DropDownComboBox cbo재직구분;
        private Duzon.Common.Controls.LabelExt lbl재직구분;
        private Duzon.Common.Controls.RoundedButton btn멀티검색;
    }
}
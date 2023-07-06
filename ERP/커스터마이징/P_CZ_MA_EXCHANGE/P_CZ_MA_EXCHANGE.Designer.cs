namespace cz
{
    partial class P_CZ_MA_EXCHANGE
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_MA_EXCHANGE));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
            this.cbo고시회차 = new Duzon.Common.Controls.DropDownComboBox();
            this.lbl고시회차 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp년월 = new Duzon.Common.Controls.DatePicker();
            this.lbl년월 = new Duzon.Common.Controls.LabelExt();
            this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            this.cbo원화화폐 = new Duzon.Common.Controls.DropDownComboBox();
            this.lbl원화화폐 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.cbo외화화폐 = new Duzon.Common.Controls.DropDownComboBox();
            this.lbl외화화폐 = new Duzon.Common.Controls.LabelExt();
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this.btn환율정보가져오기 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn복사 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn환율정보동기화 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.mDataArea.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl4.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp년월)).BeginInit();
            this.oneGridItem2.SuspendLayout();
            this.bpPanelControl3.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this.tableLayoutPanel1);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._flex, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(827, 579);
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
            this.oneGrid1.Size = new System.Drawing.Size(821, 63);
            this.oneGrid1.TabIndex = 0;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bpPanelControl4);
            this.oneGridItem1.Controls.Add(this.bpPanelControl1);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(811, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl4
            // 
            this.bpPanelControl4.Controls.Add(this.cbo고시회차);
            this.bpPanelControl4.Controls.Add(this.lbl고시회차);
            this.bpPanelControl4.Location = new System.Drawing.Point(307, 1);
            this.bpPanelControl4.Name = "bpPanelControl4";
            this.bpPanelControl4.Size = new System.Drawing.Size(303, 23);
            this.bpPanelControl4.TabIndex = 4;
            this.bpPanelControl4.Text = "bpPanelControl4";
            // 
            // cbo고시회차
            // 
            this.cbo고시회차.AutoDropDown = true;
            this.cbo고시회차.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbo고시회차.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo고시회차.FormattingEnabled = true;
            this.cbo고시회차.ItemHeight = 12;
            this.cbo고시회차.Location = new System.Drawing.Point(106, 0);
            this.cbo고시회차.Name = "cbo고시회차";
            this.cbo고시회차.Size = new System.Drawing.Size(197, 20);
            this.cbo고시회차.TabIndex = 1;
            // 
            // lbl고시회차
            // 
            this.lbl고시회차.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl고시회차.Location = new System.Drawing.Point(0, 0);
            this.lbl고시회차.Name = "lbl고시회차";
            this.lbl고시회차.Size = new System.Drawing.Size(100, 23);
            this.lbl고시회차.TabIndex = 0;
            this.lbl고시회차.Text = "고시회차";
            this.lbl고시회차.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.dtp년월);
            this.bpPanelControl1.Controls.Add(this.lbl년월);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(303, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // dtp년월
            // 
            this.dtp년월.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp년월.Location = new System.Drawing.Point(106, 1);
            this.dtp년월.Mask = "####/##";
            this.dtp년월.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.dtp년월.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp년월.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtp년월.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp년월.Modified = true;
            this.dtp년월.Name = "dtp년월";
            this.dtp년월.ShowUpDown = true;
            this.dtp년월.Size = new System.Drawing.Size(90, 21);
            this.dtp년월.TabIndex = 1;
            this.dtp년월.Value = new System.DateTime(((long)(0)));
            // 
            // lbl년월
            // 
            this.lbl년월.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl년월.Location = new System.Drawing.Point(0, 0);
            this.lbl년월.Name = "lbl년월";
            this.lbl년월.Size = new System.Drawing.Size(100, 23);
            this.lbl년월.TabIndex = 0;
            this.lbl년월.Text = "년월";
            this.lbl년월.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // oneGridItem2
            // 
            this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem2.Controls.Add(this.bpPanelControl3);
            this.oneGridItem2.Controls.Add(this.bpPanelControl2);
            this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem2.Name = "oneGridItem2";
            this.oneGridItem2.Size = new System.Drawing.Size(811, 23);
            this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 1;
            // 
            // bpPanelControl3
            // 
            this.bpPanelControl3.Controls.Add(this.cbo원화화폐);
            this.bpPanelControl3.Controls.Add(this.lbl원화화폐);
            this.bpPanelControl3.Location = new System.Drawing.Point(307, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(303, 23);
            this.bpPanelControl3.TabIndex = 3;
            this.bpPanelControl3.Text = "bpPanelControl3";
            // 
            // cbo원화화폐
            // 
            this.cbo원화화폐.AutoDropDown = true;
            this.cbo원화화폐.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbo원화화폐.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo원화화폐.FormattingEnabled = true;
            this.cbo원화화폐.ItemHeight = 12;
            this.cbo원화화폐.Location = new System.Drawing.Point(106, 0);
            this.cbo원화화폐.Name = "cbo원화화폐";
            this.cbo원화화폐.Size = new System.Drawing.Size(197, 20);
            this.cbo원화화폐.TabIndex = 1;
            // 
            // lbl원화화폐
            // 
            this.lbl원화화폐.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl원화화폐.Location = new System.Drawing.Point(0, 0);
            this.lbl원화화폐.Name = "lbl원화화폐";
            this.lbl원화화폐.Size = new System.Drawing.Size(100, 23);
            this.lbl원화화폐.TabIndex = 0;
            this.lbl원화화폐.Text = "원화화폐";
            this.lbl원화화폐.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.cbo외화화폐);
            this.bpPanelControl2.Controls.Add(this.lbl외화화폐);
            this.bpPanelControl2.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(303, 23);
            this.bpPanelControl2.TabIndex = 2;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // cbo외화화폐
            // 
            this.cbo외화화폐.AutoDropDown = true;
            this.cbo외화화폐.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbo외화화폐.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo외화화폐.FormattingEnabled = true;
            this.cbo외화화폐.ItemHeight = 12;
            this.cbo외화화폐.Location = new System.Drawing.Point(106, 0);
            this.cbo외화화폐.Name = "cbo외화화폐";
            this.cbo외화화폐.Size = new System.Drawing.Size(197, 20);
            this.cbo외화화폐.TabIndex = 1;
            // 
            // lbl외화화폐
            // 
            this.lbl외화화폐.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl외화화폐.Location = new System.Drawing.Point(0, 0);
            this.lbl외화화폐.Name = "lbl외화화폐";
            this.lbl외화화폐.Size = new System.Drawing.Size(100, 23);
            this.lbl외화화폐.TabIndex = 0;
            this.lbl외화화폐.Text = "외화화폐";
            this.lbl외화화폐.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this._flex.Location = new System.Drawing.Point(3, 72);
            this._flex.Name = "_flex";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 20;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(821, 504);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 1;
            this._flex.UseGridCalculator = true;
            // 
            // btn환율정보가져오기
            // 
            this.btn환율정보가져오기.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn환율정보가져오기.BackColor = System.Drawing.Color.Transparent;
            this.btn환율정보가져오기.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn환율정보가져오기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn환율정보가져오기.Location = new System.Drawing.Point(632, 10);
            this.btn환율정보가져오기.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn환율정보가져오기.Name = "btn환율정보가져오기";
            this.btn환율정보가져오기.Size = new System.Drawing.Size(116, 19);
            this.btn환율정보가져오기.TabIndex = 3;
            this.btn환율정보가져오기.TabStop = false;
            this.btn환율정보가져오기.Text = "환율정보 가져오기";
            this.btn환율정보가져오기.UseVisualStyleBackColor = false;
            // 
            // btn복사
            // 
            this.btn복사.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn복사.BackColor = System.Drawing.Color.Transparent;
            this.btn복사.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn복사.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn복사.Location = new System.Drawing.Point(754, 10);
            this.btn복사.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn복사.Name = "btn복사";
            this.btn복사.Size = new System.Drawing.Size(70, 19);
            this.btn복사.TabIndex = 4;
            this.btn복사.TabStop = false;
            this.btn복사.Text = "복사";
            this.btn복사.UseVisualStyleBackColor = false;
            // 
            // btn환율정보동기화
            // 
            this.btn환율정보동기화.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn환율정보동기화.BackColor = System.Drawing.Color.Transparent;
            this.btn환율정보동기화.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn환율정보동기화.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn환율정보동기화.Location = new System.Drawing.Point(513, 10);
            this.btn환율정보동기화.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn환율정보동기화.Name = "btn환율정보동기화";
            this.btn환율정보동기화.Size = new System.Drawing.Size(113, 19);
            this.btn환율정보동기화.TabIndex = 5;
            this.btn환율정보동기화.TabStop = false;
            this.btn환율정보동기화.Text = "환율정보동기화";
            this.btn환율정보동기화.UseVisualStyleBackColor = false;
            // 
            // P_CZ_MA_EXCHANGE
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.btn환율정보동기화);
            this.Controls.Add(this.btn복사);
            this.Controls.Add(this.btn환율정보가져오기);
            this.Name = "P_CZ_MA_EXCHANGE";
            this.TitleText = "환율정보등록";
            this.Controls.SetChildIndex(this.mDataArea, 0);
            this.Controls.SetChildIndex(this.btn환율정보가져오기, 0);
            this.Controls.SetChildIndex(this.btn복사, 0);
            this.Controls.SetChildIndex(this.btn환율정보동기화, 0);
            this.mDataArea.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl4.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtp년월)).EndInit();
            this.oneGridItem2.ResumeLayout(false);
            this.bpPanelControl3.ResumeLayout(false);
            this.bpPanelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Dass.FlexGrid.FlexGrid _flex;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.LabelExt lbl년월;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.Controls.DropDownComboBox cbo고시회차;
        private Duzon.Common.Controls.LabelExt lbl고시회차;
        private Duzon.Common.Controls.DatePicker dtp년월;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.Controls.DropDownComboBox cbo원화화폐;
        private Duzon.Common.Controls.LabelExt lbl원화화폐;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.Controls.DropDownComboBox cbo외화화폐;
        private Duzon.Common.Controls.LabelExt lbl외화화폐;
        private Duzon.Common.Controls.RoundedButton btn환율정보가져오기;
        private Duzon.Common.Controls.RoundedButton btn복사;
        private Duzon.Common.Controls.RoundedButton btn환율정보동기화;
    }
}
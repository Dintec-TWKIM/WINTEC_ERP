namespace cz
{
    partial class P_CZ_HR_PEVALU_OBJECT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_HR_PEVALU_OBJECT));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bppnl평가유형 = new Duzon.Common.BpControls.BpPanelControl();
            this.cbo평가유형 = new Duzon.Common.Controls.DropDownComboBox();
            this.lbl평가유형 = new Duzon.Common.Controls.LabelExt();
            this.bppnl평가코드 = new Duzon.Common.BpControls.BpPanelControl();
            this.bpc평가코드 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl평가코드 = new Duzon.Common.Controls.LabelExt();
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this.mDataArea.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.oneGridItem1.SuspendLayout();
            this.bppnl평가유형.SuspendLayout();
            this.bppnl평가코드.SuspendLayout();
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
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Top;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(821, 39);
            this.oneGrid1.TabIndex = 0;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bppnl평가유형);
            this.oneGridItem1.Controls.Add(this.bppnl평가코드);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(811, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bppnl평가유형
            // 
            this.bppnl평가유형.Controls.Add(this.cbo평가유형);
            this.bppnl평가유형.Controls.Add(this.lbl평가유형);
            this.bppnl평가유형.Location = new System.Drawing.Point(271, 1);
            this.bppnl평가유형.Name = "bppnl평가유형";
            this.bppnl평가유형.Size = new System.Drawing.Size(267, 23);
            this.bppnl평가유형.TabIndex = 1;
            this.bppnl평가유형.Text = "bpPanelControl2";
            // 
            // cbo평가유형
            // 
            this.cbo평가유형.AutoDropDown = true;
            this.cbo평가유형.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.cbo평가유형.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo평가유형.ItemHeight = 12;
            this.cbo평가유형.Location = new System.Drawing.Point(86, 1);
            this.cbo평가유형.Name = "cbo평가유형";
            this.cbo평가유형.Size = new System.Drawing.Size(180, 20);
            this.cbo평가유형.TabIndex = 185;
            this.cbo평가유형.Tag = "";
            // 
            // lbl평가유형
            // 
            this.lbl평가유형.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl평가유형.Location = new System.Drawing.Point(0, 2);
            this.lbl평가유형.Name = "lbl평가유형";
            this.lbl평가유형.Size = new System.Drawing.Size(80, 16);
            this.lbl평가유형.TabIndex = 184;
            this.lbl평가유형.Tag = "";
            this.lbl평가유형.Text = "평가유형";
            this.lbl평가유형.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bppnl평가코드
            // 
            this.bppnl평가코드.Controls.Add(this.bpc평가코드);
            this.bppnl평가코드.Controls.Add(this.lbl평가코드);
            this.bppnl평가코드.Location = new System.Drawing.Point(2, 1);
            this.bppnl평가코드.Name = "bppnl평가코드";
            this.bppnl평가코드.Size = new System.Drawing.Size(267, 23);
            this.bppnl평가코드.TabIndex = 0;
            this.bppnl평가코드.Text = "bpPanelControl1";
            // 
            // bpc평가코드
            // 
            this.bpc평가코드.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_TABLE_SUB;
            this.bpc평가코드.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.bpc평가코드.Location = new System.Drawing.Point(86, 1);
            this.bpc평가코드.Name = "bpc평가코드";
            this.bpc평가코드.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.bpc평가코드.Size = new System.Drawing.Size(180, 21);
            this.bpc평가코드.TabIndex = 13;
            this.bpc평가코드.TabStop = false;
            this.bpc평가코드.Tag = "";
            // 
            // lbl평가코드
            // 
            this.lbl평가코드.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl평가코드.Location = new System.Drawing.Point(0, 2);
            this.lbl평가코드.Name = "lbl평가코드";
            this.lbl평가코드.Size = new System.Drawing.Size(80, 16);
            this.lbl평가코드.TabIndex = 12;
            this.lbl평가코드.Tag = "";
            this.lbl평가코드.Text = "평가코드";
            this.lbl평가코드.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this._flex.Location = new System.Drawing.Point(3, 48);
            this._flex.Name = "_flex";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 20;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(821, 528);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 1;
            this._flex.UseGridCalculator = true;
            // 
            // P_CZ_HR_PEVALU_OBJECT
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Name = "P_CZ_HR_PEVALU_OBJECT";
            this.TitleText = "목표등록";
            this.mDataArea.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.oneGridItem1.ResumeLayout(false);
            this.bppnl평가유형.ResumeLayout(false);
            this.bppnl평가코드.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bppnl평가유형;
        private Duzon.Common.Controls.DropDownComboBox cbo평가유형;
        private Duzon.Common.Controls.LabelExt lbl평가유형;
        private Duzon.Common.BpControls.BpPanelControl bppnl평가코드;
        private Duzon.Common.BpControls.BpCodeTextBox bpc평가코드;
        private Duzon.Common.Controls.LabelExt lbl평가코드;
        private Dass.FlexGrid.FlexGrid _flex;
        #endregion
    }
}
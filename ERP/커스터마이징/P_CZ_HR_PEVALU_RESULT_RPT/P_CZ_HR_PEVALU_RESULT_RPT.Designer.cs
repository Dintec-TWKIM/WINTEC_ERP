namespace cz
{
    partial class P_CZ_HR_PEVALU_RESULT_RPT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_HR_PEVALU_RESULT_RPT));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            this.bpc부서 = new Duzon.Common.BpControls.BpComboBox();
            this.lbl부서 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.bpc사업장 = new Duzon.Common.BpControls.BpComboBox();
            this.lbl사업장 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx평가코드 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl평가코드 = new Duzon.Common.Controls.LabelExt();
            this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
            this.bpc피평가자 = new Duzon.Common.BpControls.BpComboBox();
            this.lbl피평가자 = new Duzon.Common.Controls.LabelExt();
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this.mDataArea.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl3.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            this.oneGridItem2.SuspendLayout();
            this.bpPanelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this.tableLayoutPanel1);
            this.mDataArea.Size = new System.Drawing.Size(852, 579);
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 181F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(852, 579);
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
            this.oneGrid1.Size = new System.Drawing.Size(846, 62);
            this.oneGrid1.TabIndex = 0;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bpPanelControl3);
            this.oneGridItem1.Controls.Add(this.bpPanelControl2);
            this.oneGridItem1.Controls.Add(this.bpPanelControl1);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(836, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl3
            // 
            this.bpPanelControl3.Controls.Add(this.bpc부서);
            this.bpPanelControl3.Controls.Add(this.lbl부서);
            this.bpPanelControl3.Location = new System.Drawing.Point(558, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(276, 23);
            this.bpPanelControl3.TabIndex = 2;
            this.bpPanelControl3.Text = "bpPanelControl3";
            // 
            // bpc부서
            // 
            this.bpc부서.Dock = System.Windows.Forms.DockStyle.Right;
            this.bpc부서.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_DEPT_SUB1;
            this.bpc부서.Location = new System.Drawing.Point(106, 0);
            this.bpc부서.Name = "bpc부서";
            this.bpc부서.Size = new System.Drawing.Size(170, 21);
            this.bpc부서.TabIndex = 1;
            this.bpc부서.TabStop = false;
            this.bpc부서.Text = "bpComboBox1";
            // 
            // lbl부서
            // 
            this.lbl부서.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl부서.Location = new System.Drawing.Point(0, 0);
            this.lbl부서.Name = "lbl부서";
            this.lbl부서.Size = new System.Drawing.Size(100, 23);
            this.lbl부서.TabIndex = 0;
            this.lbl부서.Text = "부서";
            this.lbl부서.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.bpc사업장);
            this.bpPanelControl2.Controls.Add(this.lbl사업장);
            this.bpPanelControl2.Location = new System.Drawing.Point(280, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(276, 23);
            this.bpPanelControl2.TabIndex = 1;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // bpc사업장
            // 
            this.bpc사업장.Dock = System.Windows.Forms.DockStyle.Right;
            this.bpc사업장.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_BIZAREA_SUB1;
            this.bpc사업장.Location = new System.Drawing.Point(106, 0);
            this.bpc사업장.Name = "bpc사업장";
            this.bpc사업장.Size = new System.Drawing.Size(170, 21);
            this.bpc사업장.TabIndex = 1;
            this.bpc사업장.TabStop = false;
            this.bpc사업장.Text = "bpComboBox1";
            // 
            // lbl사업장
            // 
            this.lbl사업장.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl사업장.Location = new System.Drawing.Point(0, 0);
            this.lbl사업장.Name = "lbl사업장";
            this.lbl사업장.Size = new System.Drawing.Size(100, 23);
            this.lbl사업장.TabIndex = 0;
            this.lbl사업장.Text = "사업장";
            this.lbl사업장.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.ctx평가코드);
            this.bpPanelControl1.Controls.Add(this.lbl평가코드);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(276, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // ctx평가코드
            // 
            this.ctx평가코드.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx평가코드.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_TABLE_SUB;
            this.ctx평가코드.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.ctx평가코드.Location = new System.Drawing.Point(106, 0);
            this.ctx평가코드.Name = "ctx평가코드";
            this.ctx평가코드.Size = new System.Drawing.Size(170, 21);
            this.ctx평가코드.TabIndex = 1;
            this.ctx평가코드.TabStop = false;
            this.ctx평가코드.Text = "bpCodeTextBox1";
            // 
            // lbl평가코드
            // 
            this.lbl평가코드.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl평가코드.Location = new System.Drawing.Point(0, 0);
            this.lbl평가코드.Name = "lbl평가코드";
            this.lbl평가코드.Size = new System.Drawing.Size(100, 23);
            this.lbl평가코드.TabIndex = 0;
            this.lbl평가코드.Text = "평가코드";
            this.lbl평가코드.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // oneGridItem2
            // 
            this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem2.Controls.Add(this.bpPanelControl4);
            this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem2.Name = "oneGridItem2";
            this.oneGridItem2.Size = new System.Drawing.Size(836, 23);
            this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 1;
            // 
            // bpPanelControl4
            // 
            this.bpPanelControl4.Controls.Add(this.bpc피평가자);
            this.bpPanelControl4.Controls.Add(this.lbl피평가자);
            this.bpPanelControl4.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl4.Name = "bpPanelControl4";
            this.bpPanelControl4.Size = new System.Drawing.Size(276, 23);
            this.bpPanelControl4.TabIndex = 2;
            this.bpPanelControl4.Text = "bpPanelControl4";
            // 
            // bpc피평가자
            // 
            this.bpc피평가자.Dock = System.Windows.Forms.DockStyle.Right;
            this.bpc피평가자.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_TABLE_SUB1;
            this.bpc피평가자.Location = new System.Drawing.Point(106, 0);
            this.bpc피평가자.Name = "bpc피평가자";
            this.bpc피평가자.Size = new System.Drawing.Size(170, 21);
            this.bpc피평가자.TabIndex = 1;
            this.bpc피평가자.TabStop = false;
            this.bpc피평가자.Text = "bpComboBox1";
            // 
            // lbl피평가자
            // 
            this.lbl피평가자.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl피평가자.Location = new System.Drawing.Point(0, 0);
            this.lbl피평가자.Name = "lbl피평가자";
            this.lbl피평가자.Size = new System.Drawing.Size(100, 23);
            this.lbl피평가자.TabIndex = 0;
            this.lbl피평가자.Text = "피평가자";
            this.lbl피평가자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this._flex.Location = new System.Drawing.Point(3, 71);
            this._flex.Name = "_flex";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 20;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(846, 505);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 1;
            this._flex.UseGridCalculator = true;
            // 
            // P_CZ_HR_PEVALU_RESULT_RPT
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Name = "P_CZ_HR_PEVALU_RESULT_RPT";
            this.Size = new System.Drawing.Size(852, 619);
            this.TitleText = "인사평가현황(딘텍)";
            this.mDataArea.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl3.ResumeLayout(false);
            this.bpPanelControl2.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            this.oneGridItem2.ResumeLayout(false);
            this.bpPanelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.BpControls.BpCodeTextBox ctx평가코드;
        private Duzon.Common.Controls.LabelExt lbl평가코드;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.Controls.LabelExt lbl부서;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.BpControls.BpComboBox bpc사업장;
        private Duzon.Common.Controls.LabelExt lbl사업장;
        private Duzon.Common.BpControls.BpComboBox bpc부서;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.BpControls.BpComboBox bpc피평가자;
        private Duzon.Common.Controls.LabelExt lbl피평가자;
        private Dass.FlexGrid.FlexGrid _flex;
    }
}
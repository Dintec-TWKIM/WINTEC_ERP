namespace cz
{
    partial class P_CZ_SA_EXPENSE_MNG
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_SA_EXPENSE_MNG));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx비용코드 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl비용코드 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.cbo구분 = new Duzon.Common.Controls.DropDownComboBox();
            this.lbl구분 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp일자 = new Duzon.Common.Controls.PeriodPicker();
            this.lbl일자 = new Duzon.Common.Controls.LabelExt();
            this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl6 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx영업담당자 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl영업담당자 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
            this.txt프로젝트 = new Duzon.Common.Controls.TextBoxExt();
            this.lbl프로젝트 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx거래처 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl거래처 = new Duzon.Common.Controls.LabelExt();
            this.oneGridItem3 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this.bpPanelControl8 = new Duzon.Common.BpControls.BpPanelControl();
            this.chk10000원이상 = new Duzon.Common.Controls.CheckBoxExt();
            this.mDataArea.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl3.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            this.oneGridItem2.SuspendLayout();
            this.bpPanelControl6.SuspendLayout();
            this.bpPanelControl5.SuspendLayout();
            this.bpPanelControl4.SuspendLayout();
            this.oneGridItem3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this.bpPanelControl8.SuspendLayout();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this.tableLayoutPanel1);
            this.mDataArea.Size = new System.Drawing.Size(896, 579);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._flex, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 92F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 73F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(896, 579);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // oneGrid1
            // 
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2,
            this.oneGridItem3});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(890, 86);
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
            this.oneGridItem1.Size = new System.Drawing.Size(880, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl3
            // 
            this.bpPanelControl3.Controls.Add(this.ctx비용코드);
            this.bpPanelControl3.Controls.Add(this.lbl비용코드);
            this.bpPanelControl3.Location = new System.Drawing.Point(584, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(289, 23);
            this.bpPanelControl3.TabIndex = 3;
            this.bpPanelControl3.Text = "bpPanelControl2";
            // 
            // ctx비용코드
            // 
            this.ctx비용코드.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx비용코드.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB;
            this.ctx비용코드.Location = new System.Drawing.Point(106, 0);
            this.ctx비용코드.Name = "ctx비용코드";
            this.ctx비용코드.Size = new System.Drawing.Size(183, 21);
            this.ctx비용코드.TabIndex = 1;
            this.ctx비용코드.TabStop = false;
            this.ctx비용코드.Text = "bpCodeTextBox1";
            // 
            // lbl비용코드
            // 
            this.lbl비용코드.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl비용코드.Location = new System.Drawing.Point(0, 0);
            this.lbl비용코드.Name = "lbl비용코드";
            this.lbl비용코드.Size = new System.Drawing.Size(100, 23);
            this.lbl비용코드.TabIndex = 0;
            this.lbl비용코드.Text = "비용코드";
            this.lbl비용코드.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.cbo구분);
            this.bpPanelControl2.Controls.Add(this.lbl구분);
            this.bpPanelControl2.Location = new System.Drawing.Point(293, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(289, 23);
            this.bpPanelControl2.TabIndex = 2;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // cbo구분
            // 
            this.cbo구분.AutoDropDown = true;
            this.cbo구분.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbo구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo구분.FormattingEnabled = true;
            this.cbo구분.ItemHeight = 12;
            this.cbo구분.Location = new System.Drawing.Point(106, 0);
            this.cbo구분.Name = "cbo구분";
            this.cbo구분.Size = new System.Drawing.Size(183, 20);
            this.cbo구분.TabIndex = 3;
            // 
            // lbl구분
            // 
            this.lbl구분.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl구분.Location = new System.Drawing.Point(0, 0);
            this.lbl구분.Name = "lbl구분";
            this.lbl구분.Size = new System.Drawing.Size(100, 23);
            this.lbl구분.TabIndex = 2;
            this.lbl구분.Text = "구분";
            this.lbl구분.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.dtp일자);
            this.bpPanelControl1.Controls.Add(this.lbl일자);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(289, 23);
            this.bpPanelControl1.TabIndex = 1;
            this.bpPanelControl1.Text = "bpPanelControl2";
            // 
            // dtp일자
            // 
            this.dtp일자.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp일자.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtp일자.IsNecessaryCondition = true;
            this.dtp일자.Location = new System.Drawing.Point(104, 0);
            this.dtp일자.MaximumSize = new System.Drawing.Size(185, 21);
            this.dtp일자.MinimumSize = new System.Drawing.Size(185, 21);
            this.dtp일자.Name = "dtp일자";
            this.dtp일자.Size = new System.Drawing.Size(185, 21);
            this.dtp일자.TabIndex = 1;
            // 
            // lbl일자
            // 
            this.lbl일자.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl일자.Location = new System.Drawing.Point(0, 0);
            this.lbl일자.Name = "lbl일자";
            this.lbl일자.Size = new System.Drawing.Size(100, 23);
            this.lbl일자.TabIndex = 0;
            this.lbl일자.Text = "일자";
            this.lbl일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // oneGridItem2
            // 
            this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem2.Controls.Add(this.bpPanelControl6);
            this.oneGridItem2.Controls.Add(this.bpPanelControl5);
            this.oneGridItem2.Controls.Add(this.bpPanelControl4);
            this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem2.Name = "oneGridItem2";
            this.oneGridItem2.Size = new System.Drawing.Size(880, 23);
            this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 1;
            // 
            // bpPanelControl6
            // 
            this.bpPanelControl6.Controls.Add(this.ctx영업담당자);
            this.bpPanelControl6.Controls.Add(this.lbl영업담당자);
            this.bpPanelControl6.Location = new System.Drawing.Point(584, 1);
            this.bpPanelControl6.Name = "bpPanelControl6";
            this.bpPanelControl6.Size = new System.Drawing.Size(289, 23);
            this.bpPanelControl6.TabIndex = 6;
            this.bpPanelControl6.Text = "bpPanelControl2";
            // 
            // ctx영업담당자
            // 
            this.ctx영업담당자.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx영업담당자.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
            this.ctx영업담당자.Location = new System.Drawing.Point(106, 0);
            this.ctx영업담당자.Name = "ctx영업담당자";
            this.ctx영업담당자.Size = new System.Drawing.Size(183, 21);
            this.ctx영업담당자.TabIndex = 1;
            this.ctx영업담당자.TabStop = false;
            this.ctx영업담당자.Text = "bpCodeTextBox1";
            // 
            // lbl영업담당자
            // 
            this.lbl영업담당자.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl영업담당자.Location = new System.Drawing.Point(0, 0);
            this.lbl영업담당자.Name = "lbl영업담당자";
            this.lbl영업담당자.Size = new System.Drawing.Size(100, 23);
            this.lbl영업담당자.TabIndex = 0;
            this.lbl영업담당자.Text = "영업담당자";
            this.lbl영업담당자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl5
            // 
            this.bpPanelControl5.Controls.Add(this.txt프로젝트);
            this.bpPanelControl5.Controls.Add(this.lbl프로젝트);
            this.bpPanelControl5.Location = new System.Drawing.Point(293, 1);
            this.bpPanelControl5.Name = "bpPanelControl5";
            this.bpPanelControl5.Size = new System.Drawing.Size(289, 23);
            this.bpPanelControl5.TabIndex = 5;
            this.bpPanelControl5.Text = "bpPanelControl2";
            // 
            // txt프로젝트
            // 
            this.txt프로젝트.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt프로젝트.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt프로젝트.Dock = System.Windows.Forms.DockStyle.Right;
            this.txt프로젝트.Location = new System.Drawing.Point(106, 0);
            this.txt프로젝트.Name = "txt프로젝트";
            this.txt프로젝트.Size = new System.Drawing.Size(183, 21);
            this.txt프로젝트.TabIndex = 1;
            // 
            // lbl프로젝트
            // 
            this.lbl프로젝트.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl프로젝트.Location = new System.Drawing.Point(0, 0);
            this.lbl프로젝트.Name = "lbl프로젝트";
            this.lbl프로젝트.Size = new System.Drawing.Size(100, 23);
            this.lbl프로젝트.TabIndex = 0;
            this.lbl프로젝트.Text = "프로젝트";
            this.lbl프로젝트.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl4
            // 
            this.bpPanelControl4.Controls.Add(this.ctx거래처);
            this.bpPanelControl4.Controls.Add(this.lbl거래처);
            this.bpPanelControl4.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl4.Name = "bpPanelControl4";
            this.bpPanelControl4.Size = new System.Drawing.Size(289, 23);
            this.bpPanelControl4.TabIndex = 4;
            this.bpPanelControl4.Text = "bpPanelControl2";
            // 
            // ctx거래처
            // 
            this.ctx거래처.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx거래처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.ctx거래처.Location = new System.Drawing.Point(104, 0);
            this.ctx거래처.Name = "ctx거래처";
            this.ctx거래처.Size = new System.Drawing.Size(185, 21);
            this.ctx거래처.TabIndex = 1;
            this.ctx거래처.TabStop = false;
            this.ctx거래처.Text = "bpCodeTextBox1";
            // 
            // lbl거래처
            // 
            this.lbl거래처.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl거래처.Location = new System.Drawing.Point(0, 0);
            this.lbl거래처.Name = "lbl거래처";
            this.lbl거래처.Size = new System.Drawing.Size(100, 23);
            this.lbl거래처.TabIndex = 0;
            this.lbl거래처.Text = "거래처";
            this.lbl거래처.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // oneGridItem3
            // 
            this.oneGridItem3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem3.Controls.Add(this.bpPanelControl8);
            this.oneGridItem3.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem3.Location = new System.Drawing.Point(0, 46);
            this.oneGridItem3.Name = "oneGridItem3";
            this.oneGridItem3.Size = new System.Drawing.Size(880, 23);
            this.oneGridItem3.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem3.TabIndex = 2;
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
            this._flex.Location = new System.Drawing.Point(3, 95);
            this._flex.Name = "_flex";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 20;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(890, 481);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 1;
            this._flex.UseGridCalculator = true;
            // 
            // bpPanelControl8
            // 
            this.bpPanelControl8.Controls.Add(this.chk10000원이상);
            this.bpPanelControl8.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl8.Name = "bpPanelControl8";
            this.bpPanelControl8.Size = new System.Drawing.Size(203, 23);
            this.bpPanelControl8.TabIndex = 1;
            this.bpPanelControl8.Text = "bpPanelControl8";
            // 
            // chk10000원이상
            // 
            this.chk10000원이상.Checked = true;
            this.chk10000원이상.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk10000원이상.Dock = System.Windows.Forms.DockStyle.Right;
            this.chk10000원이상.Location = new System.Drawing.Point(103, 0);
            this.chk10000원이상.Name = "chk10000원이상";
            this.chk10000원이상.Size = new System.Drawing.Size(100, 23);
            this.chk10000원이상.TabIndex = 2;
            this.chk10000원이상.Text = "10000원 이상";
            this.chk10000원이상.TextDD = "";
            this.chk10000원이상.UseVisualStyleBackColor = true;
            // 
            // P_CZ_SA_EXPENSE_MNG
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Name = "P_CZ_SA_EXPENSE_MNG";
            this.Size = new System.Drawing.Size(896, 619);
            this.TitleText = "가격조정관리";
            this.mDataArea.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl3.ResumeLayout(false);
            this.bpPanelControl2.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            this.oneGridItem2.ResumeLayout(false);
            this.bpPanelControl6.ResumeLayout(false);
            this.bpPanelControl5.ResumeLayout(false);
            this.bpPanelControl5.PerformLayout();
            this.bpPanelControl4.ResumeLayout(false);
            this.oneGridItem3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this.bpPanelControl8.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Dass.FlexGrid.FlexGrid _flex;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.BpControls.BpCodeTextBox ctx비용코드;
        private Duzon.Common.Controls.LabelExt lbl비용코드;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.Controls.DropDownComboBox cbo구분;
        private Duzon.Common.Controls.LabelExt lbl구분;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.PeriodPicker dtp일자;
        private Duzon.Common.Controls.LabelExt lbl일자;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.BpControls.BpCodeTextBox ctx거래처;
        private Duzon.Common.Controls.LabelExt lbl거래처;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl6;
        private Duzon.Common.BpControls.BpCodeTextBox ctx영업담당자;
        private Duzon.Common.Controls.LabelExt lbl영업담당자;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl5;
        private Duzon.Common.Controls.TextBoxExt txt프로젝트;
        private Duzon.Common.Controls.LabelExt lbl프로젝트;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem3;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl8;
        private Duzon.Common.Controls.CheckBoxExt chk10000원이상;
    }
}
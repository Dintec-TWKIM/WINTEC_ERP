namespace cz
{
    partial class P_CZ_FI_BANK_SEND_SUB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_FI_BANK_SEND_SUB));
            this.dtp파일작성일자 = new Duzon.Common.Controls.DatePicker();
            this.btn확인 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn취소 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn삭제 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.txt파일순번 = new Duzon.Common.Controls.CurrencyTextBox();
            this.lbl파일순번 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl파일작성일자 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl = new Duzon.Common.BpControls.BpPanelControl();
            this.txt출금계좌번호 = new Duzon.Common.Controls.TextBoxExt();
            this.lbl출금계좌번호 = new Duzon.Common.Controls.LabelExt();
            this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.rdo건별이체 = new Duzon.Common.Controls.RadioButtonExt();
            this.rdo일괄이체 = new Duzon.Common.Controls.RadioButtonExt();
            this.lbl이체유형 = new Duzon.Common.Controls.LabelExt();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp파일작성일자)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt파일순번)).BeginInit();
            this.bpPanelControl1.SuspendLayout();
            this.bpPanelControl.SuspendLayout();
            this.oneGridItem2.SuspendLayout();
            this.bpPanelControl3.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdo건별이체)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdo일괄이체)).BeginInit();
            this.SuspendLayout();
            // 
            // dtp파일작성일자
            // 
            this.dtp파일작성일자.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.dtp파일작성일자.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp파일작성일자.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtp파일작성일자.Location = new System.Drawing.Point(109, 0);
            this.dtp파일작성일자.Mask = "####/##/##";
            this.dtp파일작성일자.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.dtp파일작성일자.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp파일작성일자.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtp파일작성일자.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp파일작성일자.Name = "dtp파일작성일자";
            this.dtp파일작성일자.Size = new System.Drawing.Size(84, 21);
            this.dtp파일작성일자.TabIndex = 1;
            this.dtp파일작성일자.Value = new System.DateTime(2004, 1, 1, 0, 0, 0, 0);
            // 
            // btn확인
            // 
            this.btn확인.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn확인.BackColor = System.Drawing.Color.White;
            this.btn확인.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn확인.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn확인.ForeColor = System.Drawing.Color.Black;
            this.btn확인.Location = new System.Drawing.Point(505, 3);
            this.btn확인.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn확인.Name = "btn확인";
            this.btn확인.Size = new System.Drawing.Size(64, 19);
            this.btn확인.TabIndex = 89;
            this.btn확인.TabStop = false;
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
            this.btn취소.ForeColor = System.Drawing.Color.Black;
            this.btn취소.Location = new System.Drawing.Point(575, 3);
            this.btn취소.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn취소.Name = "btn취소";
            this.btn취소.Size = new System.Drawing.Size(64, 19);
            this.btn취소.TabIndex = 88;
            this.btn취소.TabStop = false;
            this.btn취소.Text = "취소";
            this.btn취소.UseVisualStyleBackColor = false;
            // 
            // btn삭제
            // 
            this.btn삭제.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn삭제.BackColor = System.Drawing.Color.White;
            this.btn삭제.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn삭제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn삭제.ForeColor = System.Drawing.Color.Black;
            this.btn삭제.Location = new System.Drawing.Point(645, 3);
            this.btn삭제.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn삭제.Name = "btn삭제";
            this.btn삭제.Size = new System.Drawing.Size(64, 19);
            this.btn삭제.TabIndex = 118;
            this.btn삭제.TabStop = false;
            this.btn삭제.Text = "삭제";
            this.btn삭제.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this._flex, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 48);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 71F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 112F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(718, 414);
            this.tableLayoutPanel1.TabIndex = 120;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btn삭제);
            this.flowLayoutPanel1.Controls.Add(this.btn취소);
            this.flowLayoutPanel1.Controls.Add(this.btn확인);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 74);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(712, 28);
            this.flowLayoutPanel1.TabIndex = 0;
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
            this._flex.Location = new System.Drawing.Point(3, 108);
            this._flex.Name = "_flex";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 18;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(712, 303);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 1;
            this._flex.UseGridCalculator = true;
            // 
            // oneGrid1
            // 
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(712, 65);
            this.oneGrid1.TabIndex = 2;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bpPanelControl2);
            this.oneGridItem1.Controls.Add(this.bpPanelControl1);
            this.oneGridItem1.Controls.Add(this.bpPanelControl);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(702, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.txt파일순번);
            this.bpPanelControl2.Controls.Add(this.lbl파일순번);
            this.bpPanelControl2.Location = new System.Drawing.Point(516, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(178, 23);
            this.bpPanelControl2.TabIndex = 2;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // txt파일순번
            // 
            this.txt파일순번.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt파일순번.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt파일순번.CurrencyGroupSeparator = "";
            this.txt파일순번.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txt파일순번.Dock = System.Windows.Forms.DockStyle.Right;
            this.txt파일순번.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txt파일순번.Location = new System.Drawing.Point(106, 0);
            this.txt파일순번.Name = "txt파일순번";
            this.txt파일순번.NullString = "0";
            this.txt파일순번.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txt파일순번.Size = new System.Drawing.Size(72, 21);
            this.txt파일순번.TabIndex = 91;
            this.txt파일순번.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbl파일순번
            // 
            this.lbl파일순번.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl파일순번.Location = new System.Drawing.Point(0, 0);
            this.lbl파일순번.Name = "lbl파일순번";
            this.lbl파일순번.Size = new System.Drawing.Size(100, 23);
            this.lbl파일순번.TabIndex = 0;
            this.lbl파일순번.Text = "파일순번";
            this.lbl파일순번.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.lbl파일작성일자);
            this.bpPanelControl1.Controls.Add(this.dtp파일작성일자);
            this.bpPanelControl1.Location = new System.Drawing.Point(321, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(193, 23);
            this.bpPanelControl1.TabIndex = 1;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // lbl파일작성일자
            // 
            this.lbl파일작성일자.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl파일작성일자.Location = new System.Drawing.Point(0, 0);
            this.lbl파일작성일자.Name = "lbl파일작성일자";
            this.lbl파일작성일자.Size = new System.Drawing.Size(100, 23);
            this.lbl파일작성일자.TabIndex = 0;
            this.lbl파일작성일자.Text = "파일작성일자";
            this.lbl파일작성일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl
            // 
            this.bpPanelControl.Controls.Add(this.txt출금계좌번호);
            this.bpPanelControl.Controls.Add(this.lbl출금계좌번호);
            this.bpPanelControl.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl.Name = "bpPanelControl";
            this.bpPanelControl.Size = new System.Drawing.Size(317, 23);
            this.bpPanelControl.TabIndex = 0;
            this.bpPanelControl.Text = "bpPanelControl1";
            // 
            // txt출금계좌번호
            // 
            this.txt출금계좌번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt출금계좌번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt출금계좌번호.Dock = System.Windows.Forms.DockStyle.Right;
            this.txt출금계좌번호.Location = new System.Drawing.Point(106, 0);
            this.txt출금계좌번호.Name = "txt출금계좌번호";
            this.txt출금계좌번호.ReadOnly = true;
            this.txt출금계좌번호.Size = new System.Drawing.Size(211, 21);
            this.txt출금계좌번호.TabIndex = 1;
            this.txt출금계좌번호.TabStop = false;
            // 
            // lbl출금계좌번호
            // 
            this.lbl출금계좌번호.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl출금계좌번호.Location = new System.Drawing.Point(0, 0);
            this.lbl출금계좌번호.Name = "lbl출금계좌번호";
            this.lbl출금계좌번호.Size = new System.Drawing.Size(100, 23);
            this.lbl출금계좌번호.TabIndex = 0;
            this.lbl출금계좌번호.Text = "출금계좌번호";
            this.lbl출금계좌번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // oneGridItem2
            // 
            this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem2.Controls.Add(this.bpPanelControl3);
            this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem2.Name = "oneGridItem2";
            this.oneGridItem2.Size = new System.Drawing.Size(702, 23);
            this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 1;
            this.oneGridItem2.Visible = false;
            // 
            // bpPanelControl3
            // 
            this.bpPanelControl3.Controls.Add(this.flowLayoutPanel2);
            this.bpPanelControl3.Controls.Add(this.lbl이체유형);
            this.bpPanelControl3.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(317, 23);
            this.bpPanelControl3.TabIndex = 0;
            this.bpPanelControl3.Text = "bpPanelControl3";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.rdo건별이체);
            this.flowLayoutPanel2.Controls.Add(this.rdo일괄이체);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(106, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(211, 23);
            this.flowLayoutPanel2.TabIndex = 2;
            // 
            // rdo건별이체
            // 
            this.rdo건별이체.AutoSize = true;
            this.rdo건별이체.Checked = true;
            this.rdo건별이체.Location = new System.Drawing.Point(3, 3);
            this.rdo건별이체.Name = "rdo건별이체";
            this.rdo건별이체.Size = new System.Drawing.Size(71, 16);
            this.rdo건별이체.TabIndex = 0;
            this.rdo건별이체.TabStop = true;
            this.rdo건별이체.Text = "건별이체";
            this.rdo건별이체.TextDD = null;
            this.rdo건별이체.UseKeyEnter = true;
            this.rdo건별이체.UseVisualStyleBackColor = true;
            // 
            // rdo일괄이체
            // 
            this.rdo일괄이체.AutoSize = true;
            this.rdo일괄이체.Location = new System.Drawing.Point(80, 3);
            this.rdo일괄이체.Name = "rdo일괄이체";
            this.rdo일괄이체.Size = new System.Drawing.Size(71, 16);
            this.rdo일괄이체.TabIndex = 1;
            this.rdo일괄이체.TabStop = true;
            this.rdo일괄이체.Text = "일괄이체";
            this.rdo일괄이체.TextDD = null;
            this.rdo일괄이체.UseKeyEnter = true;
            this.rdo일괄이체.UseVisualStyleBackColor = true;
            // 
            // lbl이체유형
            // 
            this.lbl이체유형.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl이체유형.Location = new System.Drawing.Point(0, 0);
            this.lbl이체유형.Name = "lbl이체유형";
            this.lbl이체유형.Size = new System.Drawing.Size(100, 23);
            this.lbl이체유형.TabIndex = 1;
            this.lbl이체유형.Text = "이체유형";
            this.lbl이체유형.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // P_CZ_FI_BANK_SEND_SUB
            // 
            this.CancelButton = this.btn취소;
            this.CaptionPaint = true;
            this.ClientSize = new System.Drawing.Size(720, 463);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimizeBox = false;
            this.Name = "P_CZ_FI_BANK_SEND_SUB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.TitleText = "이체파일생성";
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp파일작성일자)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl2.ResumeLayout(false);
            this.bpPanelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt파일순번)).EndInit();
            this.bpPanelControl1.ResumeLayout(false);
            this.bpPanelControl.ResumeLayout(false);
            this.bpPanelControl.PerformLayout();
            this.oneGridItem2.ResumeLayout(false);
            this.bpPanelControl3.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdo건별이체)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdo일괄이체)).EndInit();
            this.ResumeLayout(false);

        }
        private Duzon.Common.Controls.DatePicker dtp파일작성일자;
        private Duzon.Common.Controls.RoundedButton btn확인;
        private Duzon.Common.Controls.RoundedButton btn취소;
        private Duzon.Common.Controls.RoundedButton btn삭제;
        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Dass.FlexGrid.FlexGrid _flex;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.Controls.CurrencyTextBox txt파일순번;
        private Duzon.Common.Controls.LabelExt lbl파일순번;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.LabelExt lbl파일작성일자;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl;
        private Duzon.Common.Controls.TextBoxExt txt출금계좌번호;
        private Duzon.Common.Controls.LabelExt lbl출금계좌번호;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private Duzon.Common.Controls.RadioButtonExt rdo건별이체;
        private Duzon.Common.Controls.RadioButtonExt rdo일괄이체;
        private Duzon.Common.Controls.LabelExt lbl이체유형;
    }
}
namespace cz
{
    partial class P_CZ_SA_LOG_CHARGE_SUB
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl가로 = new Duzon.Common.Controls.LabelExt();
            this.cur가로 = new Duzon.Common.Controls.CurrencyTextBox();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.cur세로 = new Duzon.Common.Controls.CurrencyTextBox();
            this.lbl세로 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            this.cur높이 = new Duzon.Common.Controls.CurrencyTextBox();
            this.lbl높이 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
            this.curCBM = new Duzon.Common.Controls.CurrencyTextBox();
            this.lblCBM = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
            this.cur금액 = new Duzon.Common.Controls.CurrencyTextBox();
            this.lbl금액 = new Duzon.Common.Controls.LabelExt();
            this.btn포장비계산 = new Duzon.Common.Controls.RoundedButton(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.oneGridItem1.SuspendLayout();
            this.oneGridItem2.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur가로)).BeginInit();
            this.bpPanelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur세로)).BeginInit();
            this.bpPanelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur높이)).BeginInit();
            this.bpPanelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.curCBM)).BeginInit();
            this.bpPanelControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur금액)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 50);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(906, 69);
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
            this.oneGrid1.Size = new System.Drawing.Size(900, 63);
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
            this.oneGridItem1.Size = new System.Drawing.Size(890, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // oneGridItem2
            // 
            this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem2.Controls.Add(this.btn포장비계산);
            this.oneGridItem2.Controls.Add(this.bpPanelControl5);
            this.oneGridItem2.Controls.Add(this.bpPanelControl4);
            this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem2.Name = "oneGridItem2";
            this.oneGridItem2.Size = new System.Drawing.Size(890, 23);
            this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 1;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.cur가로);
            this.bpPanelControl1.Controls.Add(this.lbl가로);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(291, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // lbl가로
            // 
            this.lbl가로.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl가로.Location = new System.Drawing.Point(0, 0);
            this.lbl가로.Name = "lbl가로";
            this.lbl가로.Size = new System.Drawing.Size(100, 23);
            this.lbl가로.TabIndex = 0;
            this.lbl가로.Text = "가로";
            this.lbl가로.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cur가로
            // 
            this.cur가로.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.cur가로.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cur가로.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur가로.Dock = System.Windows.Forms.DockStyle.Right;
            this.cur가로.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur가로.Location = new System.Drawing.Point(106, 0);
            this.cur가로.Name = "cur가로";
            this.cur가로.NullString = "0";
            this.cur가로.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur가로.Size = new System.Drawing.Size(185, 21);
            this.cur가로.TabIndex = 1;
            this.cur가로.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.cur세로);
            this.bpPanelControl2.Controls.Add(this.lbl세로);
            this.bpPanelControl2.Location = new System.Drawing.Point(295, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(291, 23);
            this.bpPanelControl2.TabIndex = 1;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // cur세로
            // 
            this.cur세로.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.cur세로.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cur세로.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur세로.Dock = System.Windows.Forms.DockStyle.Right;
            this.cur세로.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur세로.Location = new System.Drawing.Point(106, 0);
            this.cur세로.Name = "cur세로";
            this.cur세로.NullString = "0";
            this.cur세로.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur세로.Size = new System.Drawing.Size(185, 21);
            this.cur세로.TabIndex = 1;
            this.cur세로.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbl세로
            // 
            this.lbl세로.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl세로.Location = new System.Drawing.Point(0, 0);
            this.lbl세로.Name = "lbl세로";
            this.lbl세로.Size = new System.Drawing.Size(100, 23);
            this.lbl세로.TabIndex = 0;
            this.lbl세로.Text = "세로";
            this.lbl세로.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl3
            // 
            this.bpPanelControl3.Controls.Add(this.cur높이);
            this.bpPanelControl3.Controls.Add(this.lbl높이);
            this.bpPanelControl3.Location = new System.Drawing.Point(588, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(291, 23);
            this.bpPanelControl3.TabIndex = 2;
            this.bpPanelControl3.Text = "bpPanelControl3";
            // 
            // cur높이
            // 
            this.cur높이.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.cur높이.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cur높이.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur높이.Dock = System.Windows.Forms.DockStyle.Right;
            this.cur높이.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur높이.Location = new System.Drawing.Point(106, 0);
            this.cur높이.Name = "cur높이";
            this.cur높이.NullString = "0";
            this.cur높이.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur높이.Size = new System.Drawing.Size(185, 21);
            this.cur높이.TabIndex = 1;
            this.cur높이.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbl높이
            // 
            this.lbl높이.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl높이.Location = new System.Drawing.Point(0, 0);
            this.lbl높이.Name = "lbl높이";
            this.lbl높이.Size = new System.Drawing.Size(100, 23);
            this.lbl높이.TabIndex = 0;
            this.lbl높이.Text = "높이";
            this.lbl높이.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl4
            // 
            this.bpPanelControl4.Controls.Add(this.curCBM);
            this.bpPanelControl4.Controls.Add(this.lblCBM);
            this.bpPanelControl4.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl4.Name = "bpPanelControl4";
            this.bpPanelControl4.Size = new System.Drawing.Size(291, 23);
            this.bpPanelControl4.TabIndex = 2;
            this.bpPanelControl4.Text = "bpPanelControl4";
            // 
            // curCBM
            // 
            this.curCBM.BackColor = System.Drawing.SystemColors.Control;
            this.curCBM.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.curCBM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.curCBM.CurrencyDecimalDigits = 2;
            this.curCBM.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.curCBM.Dock = System.Windows.Forms.DockStyle.Right;
            this.curCBM.ForeColor = System.Drawing.SystemColors.ControlText;
            this.curCBM.Location = new System.Drawing.Point(106, 0);
            this.curCBM.Name = "curCBM";
            this.curCBM.NullString = "0";
            this.curCBM.ReadOnly = true;
            this.curCBM.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.curCBM.Size = new System.Drawing.Size(185, 21);
            this.curCBM.TabIndex = 1;
            this.curCBM.TabStop = false;
            this.curCBM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblCBM
            // 
            this.lblCBM.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCBM.Location = new System.Drawing.Point(0, 0);
            this.lblCBM.Name = "lblCBM";
            this.lblCBM.Size = new System.Drawing.Size(100, 23);
            this.lblCBM.TabIndex = 0;
            this.lblCBM.Text = "CBM";
            this.lblCBM.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl5
            // 
            this.bpPanelControl5.Controls.Add(this.cur금액);
            this.bpPanelControl5.Controls.Add(this.lbl금액);
            this.bpPanelControl5.Location = new System.Drawing.Point(295, 1);
            this.bpPanelControl5.Name = "bpPanelControl5";
            this.bpPanelControl5.Size = new System.Drawing.Size(291, 23);
            this.bpPanelControl5.TabIndex = 3;
            this.bpPanelControl5.Text = "bpPanelControl5";
            // 
            // cur금액
            // 
            this.cur금액.BackColor = System.Drawing.SystemColors.Control;
            this.cur금액.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.cur금액.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cur금액.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur금액.Dock = System.Windows.Forms.DockStyle.Right;
            this.cur금액.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur금액.Location = new System.Drawing.Point(106, 0);
            this.cur금액.Name = "cur금액";
            this.cur금액.NullString = "0";
            this.cur금액.ReadOnly = true;
            this.cur금액.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur금액.Size = new System.Drawing.Size(185, 21);
            this.cur금액.TabIndex = 1;
            this.cur금액.TabStop = false;
            this.cur금액.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbl금액
            // 
            this.lbl금액.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl금액.Location = new System.Drawing.Point(0, 0);
            this.lbl금액.Name = "lbl금액";
            this.lbl금액.Size = new System.Drawing.Size(100, 23);
            this.lbl금액.TabIndex = 0;
            this.lbl금액.Text = "금액";
            this.lbl금액.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn포장비계산
            // 
            this.btn포장비계산.BackColor = System.Drawing.Color.Transparent;
            this.btn포장비계산.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn포장비계산.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn포장비계산.Location = new System.Drawing.Point(588, 1);
            this.btn포장비계산.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn포장비계산.Name = "btn포장비계산";
            this.btn포장비계산.Size = new System.Drawing.Size(127, 19);
            this.btn포장비계산.TabIndex = 4;
            this.btn포장비계산.TabStop = false;
            this.btn포장비계산.Text = "포장비계산";
            this.btn포장비계산.UseVisualStyleBackColor = false;
            // 
            // P_CZ_SA_LOG_CHARGE_SUB
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CaptionPaint = true;
            this.ClientSize = new System.Drawing.Size(912, 123);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "P_CZ_SA_LOG_CHARGE_SUB";
            this.Text = "P_CZ_SA_LOG_CHARGE_SUB";
            this.TitleText = "포장비계산";
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.oneGridItem1.ResumeLayout(false);
            this.oneGridItem2.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            this.bpPanelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur가로)).EndInit();
            this.bpPanelControl2.ResumeLayout(false);
            this.bpPanelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur세로)).EndInit();
            this.bpPanelControl3.ResumeLayout(false);
            this.bpPanelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur높이)).EndInit();
            this.bpPanelControl4.ResumeLayout(false);
            this.bpPanelControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.curCBM)).EndInit();
            this.bpPanelControl5.ResumeLayout(false);
            this.bpPanelControl5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur금액)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.Controls.CurrencyTextBox cur높이;
        private Duzon.Common.Controls.LabelExt lbl높이;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.Controls.CurrencyTextBox cur세로;
        private Duzon.Common.Controls.LabelExt lbl세로;
        private Duzon.Common.Controls.CurrencyTextBox cur가로;
        private Duzon.Common.Controls.LabelExt lbl가로;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.Controls.CurrencyTextBox curCBM;
        private Duzon.Common.Controls.LabelExt lblCBM;
        private Duzon.Common.Controls.RoundedButton btn포장비계산;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl5;
        private Duzon.Common.Controls.CurrencyTextBox cur금액;
        private Duzon.Common.Controls.LabelExt lbl금액;
    }
}
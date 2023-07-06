namespace cz
{
    partial class P_CZ_FI_EPDOCU_BUDGET
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
            this.panelExt1 = new Duzon.Common.Controls.PanelExt();
            this.btnOK = new Duzon.Common.Controls.RoundedButton(this.components);
            this.txt메세지 = new Duzon.Common.Controls.TextBoxExt();
            this.panelExt2 = new Duzon.Common.Controls.PanelExt();
            this.panelExt6 = new Duzon.Common.Controls.PanelExt();
            this.labelExt1 = new Duzon.Common.Controls.LabelExt();
            this.cur집행율 = new Duzon.Common.Controls.CurrencyTextBox();
            this.cur집행신청 = new Duzon.Common.Controls.CurrencyTextBox();
            this.cur집행실적 = new Duzon.Common.Controls.CurrencyTextBox();
            this.cur실행예산 = new Duzon.Common.Controls.CurrencyTextBox();
            this.cur잔여예산 = new Duzon.Common.Controls.CurrencyTextBox();
            this.panel3 = new Duzon.Common.Controls.PanelExt();
            this.m_lblRtJsum = new Duzon.Common.Controls.LabelExt();
            this.cur = new Duzon.Common.Controls.LabelExt();
            this.m_lblDO_JPRO = new Duzon.Common.Controls.LabelExt();
            this.m_lblDO_JSUM = new Duzon.Common.Controls.LabelExt();
            this.m_lblAM_ACTBUGT = new Duzon.Common.Controls.LabelExt();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.panelExt1.SuspendLayout();
            this.panelExt2.SuspendLayout();
            this.panelExt6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur집행율)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cur집행신청)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cur집행실적)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cur실행예산)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cur잔여예산)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelExt1
            // 
            this.panelExt1.Controls.Add(this.btnOK);
            this.panelExt1.Controls.Add(this.txt메세지);
            this.panelExt1.Location = new System.Drawing.Point(0, 0);
            this.panelExt1.Name = "panelExt1";
            this.panelExt1.Size = new System.Drawing.Size(394, 33);
            this.panelExt1.TabIndex = 119;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.White;
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Location = new System.Drawing.Point(322, 7);
            this.btnOK.MaximumSize = new System.Drawing.Size(0, 19);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(62, 19);
            this.btnOK.TabIndex = 116;
            this.btnOK.TabStop = false;
            this.btnOK.Text = "확인";
            this.btnOK.UseVisualStyleBackColor = false;
            // 
            // txt메세지
            // 
            this.txt메세지.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt메세지.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt메세지.Location = new System.Drawing.Point(8, 6);
            this.txt메세지.Name = "txt메세지";
            this.txt메세지.ReadOnly = true;
            this.txt메세지.Size = new System.Drawing.Size(311, 21);
            this.txt메세지.TabIndex = 115;
            this.txt메세지.TabStop = false;
            // 
            // panelExt2
            // 
            this.panelExt2.Controls.Add(this.panelExt6);
            this.panelExt2.Controls.Add(this.panelExt1);
            this.panelExt2.Location = new System.Drawing.Point(0, 49);
            this.panelExt2.Name = "panelExt2";
            this.panelExt2.Size = new System.Drawing.Size(394, 187);
            this.panelExt2.TabIndex = 120;
            // 
            // panelExt6
            // 
            this.panelExt6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelExt6.Controls.Add(this.labelExt1);
            this.panelExt6.Controls.Add(this.cur집행율);
            this.panelExt6.Controls.Add(this.cur집행신청);
            this.panelExt6.Controls.Add(this.cur집행실적);
            this.panelExt6.Controls.Add(this.cur실행예산);
            this.panelExt6.Controls.Add(this.cur잔여예산);
            this.panelExt6.Controls.Add(this.panel3);
            this.panelExt6.Location = new System.Drawing.Point(5, 39);
            this.panelExt6.Name = "panelExt6";
            this.panelExt6.Size = new System.Drawing.Size(377, 143);
            this.panelExt6.TabIndex = 17;
            // 
            // labelExt1
            // 
            this.labelExt1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelExt1.Location = new System.Drawing.Point(352, 87);
            this.labelExt1.Name = "labelExt1";
            this.labelExt1.Size = new System.Drawing.Size(16, 23);
            this.labelExt1.TabIndex = 26;
            this.labelExt1.Text = "%";
            this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cur집행율
            // 
            this.cur집행율.BackColor = System.Drawing.SystemColors.Control;
            this.cur집행율.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.cur집행율.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cur집행율.CurrencyDecimalDigits = 2;
            this.cur집행율.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur집행율.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur집행율.Location = new System.Drawing.Point(127, 87);
            this.cur집행율.Name = "cur집행율";
            this.cur집행율.NullString = "0";
            this.cur집행율.PositiveColor = System.Drawing.Color.Blue;
            this.cur집행율.ReadOnly = true;
            this.cur집행율.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur집행율.Size = new System.Drawing.Size(217, 21);
            this.cur집행율.TabIndex = 25;
            this.cur집행율.TabStop = false;
            this.cur집행율.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cur집행신청
            // 
            this.cur집행신청.BackColor = System.Drawing.SystemColors.Control;
            this.cur집행신청.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.cur집행신청.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cur집행신청.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur집행신청.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur집행신청.Location = new System.Drawing.Point(127, 58);
            this.cur집행신청.Name = "cur집행신청";
            this.cur집행신청.NullString = "0";
            this.cur집행신청.PositiveColor = System.Drawing.Color.Black;
            this.cur집행신청.ReadOnly = true;
            this.cur집행신청.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur집행신청.Size = new System.Drawing.Size(241, 21);
            this.cur집행신청.TabIndex = 24;
            this.cur집행신청.TabStop = false;
            this.cur집행신청.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cur집행실적
            // 
            this.cur집행실적.BackColor = System.Drawing.SystemColors.Control;
            this.cur집행실적.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.cur집행실적.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cur집행실적.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur집행실적.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur집행실적.Location = new System.Drawing.Point(127, 31);
            this.cur집행실적.Name = "cur집행실적";
            this.cur집행실적.NullString = "0";
            this.cur집행실적.PositiveColor = System.Drawing.Color.Black;
            this.cur집행실적.ReadOnly = true;
            this.cur집행실적.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur집행실적.Size = new System.Drawing.Size(241, 21);
            this.cur집행실적.TabIndex = 23;
            this.cur집행실적.TabStop = false;
            this.cur집행실적.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cur실행예산
            // 
            this.cur실행예산.BackColor = System.Drawing.SystemColors.Control;
            this.cur실행예산.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.cur실행예산.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cur실행예산.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur실행예산.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur실행예산.Location = new System.Drawing.Point(127, 4);
            this.cur실행예산.Name = "cur실행예산";
            this.cur실행예산.NullString = "0";
            this.cur실행예산.PositiveColor = System.Drawing.Color.Black;
            this.cur실행예산.ReadOnly = true;
            this.cur실행예산.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur실행예산.Size = new System.Drawing.Size(241, 21);
            this.cur실행예산.TabIndex = 22;
            this.cur실행예산.TabStop = false;
            this.cur실행예산.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cur잔여예산
            // 
            this.cur잔여예산.BackColor = System.Drawing.SystemColors.Control;
            this.cur잔여예산.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.cur잔여예산.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cur잔여예산.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur잔여예산.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur잔여예산.Location = new System.Drawing.Point(127, 115);
            this.cur잔여예산.Name = "cur잔여예산";
            this.cur잔여예산.NullString = "0";
            this.cur잔여예산.PositiveColor = System.Drawing.Color.Black;
            this.cur잔여예산.ReadOnly = true;
            this.cur잔여예산.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur잔여예산.Size = new System.Drawing.Size(241, 21);
            this.cur잔여예산.TabIndex = 21;
            this.cur잔여예산.TabStop = false;
            this.cur잔여예산.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel3.Controls.Add(this.m_lblRtJsum);
            this.panel3.Controls.Add(this.cur);
            this.panel3.Controls.Add(this.m_lblDO_JPRO);
            this.panel3.Controls.Add(this.m_lblDO_JSUM);
            this.panel3.Controls.Add(this.m_lblAM_ACTBUGT);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(121, 137);
            this.panel3.TabIndex = 1;
            // 
            // m_lblRtJsum
            // 
            this.m_lblRtJsum.Location = new System.Drawing.Point(6, 88);
            this.m_lblRtJsum.Name = "m_lblRtJsum";
            this.m_lblRtJsum.Size = new System.Drawing.Size(110, 18);
            this.m_lblRtJsum.TabIndex = 5;
            this.m_lblRtJsum.Tag = "RT_JSUM";
            this.m_lblRtJsum.Text = "집행율";
            this.m_lblRtJsum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cur
            // 
            this.cur.Location = new System.Drawing.Point(6, 116);
            this.cur.Name = "cur";
            this.cur.Size = new System.Drawing.Size(110, 18);
            this.cur.TabIndex = 4;
            this.cur.Tag = "DO_JBUDGET";
            this.cur.Text = "잔여예산";
            this.cur.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblDO_JPRO
            // 
            this.m_lblDO_JPRO.Location = new System.Drawing.Point(6, 59);
            this.m_lblDO_JPRO.Name = "m_lblDO_JPRO";
            this.m_lblDO_JPRO.Size = new System.Drawing.Size(110, 18);
            this.m_lblDO_JPRO.TabIndex = 3;
            this.m_lblDO_JPRO.Tag = "DO_JPRO";
            this.m_lblDO_JPRO.Text = "집행신청";
            this.m_lblDO_JPRO.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblDO_JSUM
            // 
            this.m_lblDO_JSUM.Location = new System.Drawing.Point(6, 33);
            this.m_lblDO_JSUM.Name = "m_lblDO_JSUM";
            this.m_lblDO_JSUM.Size = new System.Drawing.Size(110, 18);
            this.m_lblDO_JSUM.TabIndex = 2;
            this.m_lblDO_JSUM.Tag = "DO_JSUM";
            this.m_lblDO_JSUM.Text = "집행실적";
            this.m_lblDO_JSUM.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblAM_ACTBUGT
            // 
            this.m_lblAM_ACTBUGT.Location = new System.Drawing.Point(6, 5);
            this.m_lblAM_ACTBUGT.Name = "m_lblAM_ACTBUGT";
            this.m_lblAM_ACTBUGT.Size = new System.Drawing.Size(110, 18);
            this.m_lblAM_ACTBUGT.TabIndex = 1;
            this.m_lblAM_ACTBUGT.Tag = "AM_ACTBUGT";
            this.m_lblAM_ACTBUGT.Text = "실행예산";
            this.m_lblAM_ACTBUGT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // P_CZ_FI_EPDOCU_BUDGET
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CaptionPaint = true;
            this.ClientSize = new System.Drawing.Size(394, 237);
            this.Controls.Add(this.panelExt2);
            this.Name = "P_CZ_FI_EPDOCU_BUDGET";
            this.Text = "예산조회";
            this.TitleText = "예산조회";
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.panelExt1.ResumeLayout(false);
            this.panelExt1.PerformLayout();
            this.panelExt2.ResumeLayout(false);
            this.panelExt6.ResumeLayout(false);
            this.panelExt6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur집행율)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cur집행신청)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cur집행실적)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cur실행예산)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cur잔여예산)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private Duzon.Common.Controls.PanelExt panelExt1;
        private Duzon.Common.Controls.RoundedButton btnOK;
        private Duzon.Common.Controls.TextBoxExt txt메세지;
        private Duzon.Common.Controls.PanelExt panelExt2;
        private Duzon.Common.Controls.PanelExt panelExt6;
        private Duzon.Common.Controls.LabelExt labelExt1;
        private Duzon.Common.Controls.CurrencyTextBox cur집행율;
        private Duzon.Common.Controls.CurrencyTextBox cur집행신청;
        private Duzon.Common.Controls.CurrencyTextBox cur집행실적;
        private Duzon.Common.Controls.CurrencyTextBox cur실행예산;
        private Duzon.Common.Controls.CurrencyTextBox cur잔여예산;
        private Duzon.Common.Controls.PanelExt panel3;
        private Duzon.Common.Controls.LabelExt m_lblRtJsum;
        private Duzon.Common.Controls.LabelExt cur;
        private Duzon.Common.Controls.LabelExt m_lblDO_JPRO;
        private Duzon.Common.Controls.LabelExt m_lblDO_JSUM;
        private Duzon.Common.Controls.LabelExt m_lblAM_ACTBUGT;
        #endregion
    }
}
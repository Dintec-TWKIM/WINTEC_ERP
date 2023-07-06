namespace cz
{
    partial class P_CZ_FI_BANK_SEND_BAN_DOCU
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
            this.rdo등록계정 = new Duzon.Common.Controls.RadioButtonExt();
            this.btn전표처리 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn전표취소 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl처리방법 = new Duzon.Common.Controls.LabelExt();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl상대계정 = new Duzon.Common.Controls.LabelExt();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.rdo일괄처리 = new Duzon.Common.Controls.RadioButtonExt();
            this.rdo건별처리 = new Duzon.Common.Controls.RadioButtonExt();
            this.rdo입력계정 = new Duzon.Common.Controls.RadioButtonExt();
            this.panelExt1 = new Duzon.Common.Controls.PanelExt();
            this.ctx입력계정 = new Duzon.Common.BpControls.BpCodeTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdo등록계정)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdo일괄처리)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdo건별처리)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdo입력계정)).BeginInit();
            this.panelExt1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdo등록계정
            // 
            this.rdo등록계정.AutoSize = true;
            this.rdo등록계정.Checked = true;
            this.rdo등록계정.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdo등록계정.Location = new System.Drawing.Point(3, 3);
            this.rdo등록계정.Name = "rdo등록계정";
            this.rdo등록계정.Size = new System.Drawing.Size(77, 37);
            this.rdo등록계정.TabIndex = 0;
            this.rdo등록계정.TabStop = true;
            this.rdo등록계정.Text = "등록계정";
            this.rdo등록계정.TextDD = null;
            this.rdo등록계정.UseKeyEnter = true;
            this.rdo등록계정.UseVisualStyleBackColor = true;
            // 
            // btn전표처리
            // 
            this.btn전표처리.BackColor = System.Drawing.Color.White;
            this.btn전표처리.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn전표처리.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn전표처리.Location = new System.Drawing.Point(249, 3);
            this.btn전표처리.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn전표처리.Name = "btn전표처리";
            this.btn전표처리.Size = new System.Drawing.Size(64, 19);
            this.btn전표처리.TabIndex = 27;
            this.btn전표처리.TabStop = false;
            this.btn전표처리.Text = "전표처리";
            this.btn전표처리.UseVisualStyleBackColor = false;
            // 
            // btn전표취소
            // 
            this.btn전표취소.BackColor = System.Drawing.Color.White;
            this.btn전표취소.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn전표취소.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn전표취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn전표취소.Location = new System.Drawing.Point(319, 3);
            this.btn전표취소.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn전표취소.Name = "btn전표취소";
            this.btn전표취소.Size = new System.Drawing.Size(64, 19);
            this.btn전표취소.TabIndex = 28;
            this.btn전표취소.TabStop = false;
            this.btn전표취소.Text = "전표취소";
            this.btn전표취소.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 48);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 163F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(392, 228);
            this.tableLayoutPanel1.TabIndex = 29;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btn전표취소);
            this.flowLayoutPanel1.Controls.Add(this.btn전표처리);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(386, 26);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.03125F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.96875F));
            this.tableLayoutPanel2.Controls.Add(this.lbl처리방법, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lbl상대계정, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 1, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 35);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(386, 190);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // lbl처리방법
            // 
            this.lbl처리방법.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.lbl처리방법.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl처리방법.Location = new System.Drawing.Point(5, 96);
            this.lbl처리방법.Name = "lbl처리방법";
            this.lbl처리방법.Size = new System.Drawing.Size(115, 92);
            this.lbl처리방법.TabIndex = 28;
            this.lbl처리방법.Text = "처리방법";
            this.lbl처리방법.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.20158F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.79842F));
            this.tableLayoutPanel3.Controls.Add(this.rdo입력계정, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.rdo등록계정, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.panelExt1, 1, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(128, 5);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(253, 86);
            this.tableLayoutPanel3.TabIndex = 29;
            // 
            // lbl상대계정
            // 
            this.lbl상대계정.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.lbl상대계정.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl상대계정.Location = new System.Drawing.Point(5, 2);
            this.lbl상대계정.Name = "lbl상대계정";
            this.lbl상대계정.Size = new System.Drawing.Size(115, 92);
            this.lbl상대계정.TabIndex = 27;
            this.lbl상대계정.Text = "상대계정";
            this.lbl상대계정.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Controls.Add(this.rdo일괄처리, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.rdo건별처리, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(128, 99);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(253, 86);
            this.tableLayoutPanel4.TabIndex = 30;
            // 
            // rdo일괄처리
            // 
            this.rdo일괄처리.Checked = true;
            this.rdo일괄처리.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdo일괄처리.Location = new System.Drawing.Point(3, 3);
            this.rdo일괄처리.Name = "rdo일괄처리";
            this.rdo일괄처리.Size = new System.Drawing.Size(247, 37);
            this.rdo일괄처리.TabIndex = 6;
            this.rdo일괄처리.TabStop = true;
            this.rdo일괄처리.Text = "일괄처리";
            this.rdo일괄처리.TextDD = null;
            this.rdo일괄처리.UseKeyEnter = true;
            this.rdo일괄처리.UseVisualStyleBackColor = true;
            // 
            // rdo건별처리
            // 
            this.rdo건별처리.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdo건별처리.Location = new System.Drawing.Point(3, 46);
            this.rdo건별처리.Name = "rdo건별처리";
            this.rdo건별처리.Size = new System.Drawing.Size(247, 37);
            this.rdo건별처리.TabIndex = 8;
            this.rdo건별처리.TabStop = true;
            this.rdo건별처리.Text = "건별처리";
            this.rdo건별처리.TextDD = null;
            this.rdo건별처리.UseKeyEnter = true;
            this.rdo건별처리.UseVisualStyleBackColor = true;
            // 
            // rdo입력계정
            // 
            this.rdo입력계정.AutoSize = true;
            this.rdo입력계정.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdo입력계정.Location = new System.Drawing.Point(3, 46);
            this.rdo입력계정.Name = "rdo입력계정";
            this.rdo입력계정.Size = new System.Drawing.Size(77, 37);
            this.rdo입력계정.TabIndex = 18;
            this.rdo입력계정.TabStop = true;
            this.rdo입력계정.Text = "입력계정";
            this.rdo입력계정.TextDD = null;
            this.rdo입력계정.UseKeyEnter = true;
            this.rdo입력계정.UseVisualStyleBackColor = true;
            // 
            // panelExt1
            // 
            this.panelExt1.Controls.Add(this.ctx입력계정);
            this.panelExt1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExt1.Location = new System.Drawing.Point(86, 46);
            this.panelExt1.Name = "panelExt1";
            this.panelExt1.Size = new System.Drawing.Size(164, 37);
            this.panelExt1.TabIndex = 19;
            // 
            // ctx입력계정
            // 
            this.ctx입력계정.HelpID = Duzon.Common.Forms.Help.HelpID.P_FI_ACCTCODE_SUB;
            this.ctx입력계정.Location = new System.Drawing.Point(3, 8);
            this.ctx입력계정.Name = "ctx입력계정";
            this.ctx입력계정.Size = new System.Drawing.Size(158, 21);
            this.ctx입력계정.TabIndex = 0;
            this.ctx입력계정.TabStop = false;
            this.ctx입력계정.Text = "bpCodeTextBox1";
            // 
            // P_CZ_FI_BANK_SEND_BAN_DOCU
            // 
            this.CancelButton = this.btn전표취소;
            this.CaptionPaint = true;
            this.ClientSize = new System.Drawing.Size(394, 277);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.Name = "P_CZ_FI_BANK_SEND_BAN_DOCU";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ERP iU";
            this.TitleText = "전표처리옵션";
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdo등록계정)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rdo일괄처리)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdo건별처리)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdo입력계정)).EndInit();
            this.panelExt1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        private Duzon.Common.Controls.RoundedButton btn전표처리;
        private Duzon.Common.Controls.RoundedButton btn전표취소;
        private Duzon.Common.Controls.RadioButtonExt rdo등록계정;
        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Duzon.Common.Controls.LabelExt lbl처리방법;
        private Duzon.Common.Controls.LabelExt lbl상대계정;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private Duzon.Common.Controls.RadioButtonExt rdo일괄처리;
        private Duzon.Common.Controls.RadioButtonExt rdo건별처리;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private Duzon.Common.Controls.RadioButtonExt rdo입력계정;
        private Duzon.Common.Controls.PanelExt panelExt1;
        private Duzon.Common.BpControls.BpCodeTextBox ctx입력계정;
    }
}
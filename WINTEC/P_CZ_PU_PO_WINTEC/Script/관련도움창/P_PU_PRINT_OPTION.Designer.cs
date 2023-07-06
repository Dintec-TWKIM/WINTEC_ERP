
namespace cz
{
    partial class P_PU_PRINT_OPTION
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
            this.panel5 = new Duzon.Common.Controls.PanelExt();
            this.label1 = new Duzon.Common.Controls.LabelExt();
            this.m_btn_apply = new Duzon.Common.Controls.RoundedButton(this.components);
            this.m_btn_exit = new Duzon.Common.Controls.RoundedButton(this.components);
            this.m_pnlPlanFix = new Duzon.Common.Controls.PanelExt();
            this.tb_DT_PO = new Duzon.Common.Controls.DatePicker();
            this.labelExt1 = new Duzon.Common.Controls.LabelExt();
            this.panel19 = new Duzon.Common.Controls.PanelExt();
            this.panelExt1 = new Duzon.Common.Controls.PanelExt();
            this.lblTitle01 = new Duzon.Common.Controls.LabelExt();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.panel5.SuspendLayout();
            this.m_pnlPlanFix.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb_DT_PO)).BeginInit();
            this.panelExt1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label1);
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(555, 47);
            this.panel5.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("굴림체", 10F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Resizeble = false;
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 20;
            this.label1.Tag = "SO_CONFM";
            this.label1.Text = "출력옵션";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_btn_apply
            // 
            this.m_btn_apply.BackColor = System.Drawing.Color.White;
            this.m_btn_apply.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btn_apply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btn_apply.Location = new System.Drawing.Point(305, 51);
            this.m_btn_apply.MaximumSize = new System.Drawing.Size(0, 19);
            this.m_btn_apply.Name = "m_btn_apply";
            this.m_btn_apply.Size = new System.Drawing.Size(60, 19);
            this.m_btn_apply.TabIndex = 16;
            this.m_btn_apply.TabStop = false;
            this.m_btn_apply.Text = "반영";
            this.m_btn_apply.UseVisualStyleBackColor = false;
            // 
            // m_btn_exit
            // 
            this.m_btn_exit.BackColor = System.Drawing.Color.White;
            this.m_btn_exit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btn_exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btn_exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btn_exit.Location = new System.Drawing.Point(367, 51);
            this.m_btn_exit.MaximumSize = new System.Drawing.Size(0, 19);
            this.m_btn_exit.Name = "m_btn_exit";
            this.m_btn_exit.Size = new System.Drawing.Size(60, 19);
            this.m_btn_exit.TabIndex = 15;
            this.m_btn_exit.TabStop = false;
            this.m_btn_exit.Text = "종료";
            this.m_btn_exit.UseVisualStyleBackColor = false;
            // 
            // m_pnlPlanFix
            // 
            this.m_pnlPlanFix.Controls.Add(this.tb_DT_PO);
            this.m_pnlPlanFix.Controls.Add(this.labelExt1);
            this.m_pnlPlanFix.Controls.Add(this.panel19);
            this.m_pnlPlanFix.Controls.Add(this.panelExt1);
            this.m_pnlPlanFix.Location = new System.Drawing.Point(3, 77);
            this.m_pnlPlanFix.Name = "m_pnlPlanFix";
            this.m_pnlPlanFix.Size = new System.Drawing.Size(424, 85);
            this.m_pnlPlanFix.TabIndex = 19;
            // 
            // tb_DT_PO
            // 
            this.tb_DT_PO.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tb_DT_PO.DayColor = System.Drawing.Color.Black;
            this.tb_DT_PO.FriDayColor = System.Drawing.Color.Blue;
            this.tb_DT_PO.Location = new System.Drawing.Point(99, 8);
            this.tb_DT_PO.Mask = "####/##/##";
            this.tb_DT_PO.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.tb_DT_PO.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.tb_DT_PO.MaximumSize = new System.Drawing.Size(0, 21);
            this.tb_DT_PO.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.tb_DT_PO.Name = "tb_DT_PO";
            this.tb_DT_PO.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.tb_DT_PO.Size = new System.Drawing.Size(92, 21);
            this.tb_DT_PO.SunDayColor = System.Drawing.Color.Red;
            this.tb_DT_PO.TabIndex = 145;
            this.tb_DT_PO.Tag = "DT_PO";
            this.tb_DT_PO.TitleBackColor = System.Drawing.SystemColors.Control;
            this.tb_DT_PO.TitleForeColor = System.Drawing.Color.Black;
            this.tb_DT_PO.Value = new System.DateTime(2004, 1, 1, 0, 0, 0, 0);
            // 
            // labelExt1
            // 
            this.labelExt1.BackColor = System.Drawing.Color.Transparent;
            this.labelExt1.Location = new System.Drawing.Point(3, 50);
            this.labelExt1.Name = "labelExt1";
            this.labelExt1.Size = new System.Drawing.Size(397, 18);
            this.labelExt1.TabIndex = 144;
            this.labelExt1.Tag = "";
            this.labelExt1.Text = "* 발주 출력일을 변경합니다.";
            this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel19
            // 
            this.panel19.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel19.BackColor = System.Drawing.Color.Transparent;
            this.panel19.Location = new System.Drawing.Point(-180, 35);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(606, 1);
            this.panel19.TabIndex = 143;
            // 
            // panelExt1
            // 
            this.panelExt1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panelExt1.Controls.Add(this.lblTitle01);
            this.panelExt1.Location = new System.Drawing.Point(3, 2);
            this.panelExt1.Name = "panelExt1";
            this.panelExt1.Size = new System.Drawing.Size(90, 34);
            this.panelExt1.TabIndex = 16;
            // 
            // lblTitle01
            // 
            this.lblTitle01.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle01.Location = new System.Drawing.Point(3, 9);
            this.lblTitle01.Name = "lblTitle01";
            this.lblTitle01.Size = new System.Drawing.Size(85, 18);
            this.lblTitle01.TabIndex = 0;
            this.lblTitle01.Tag = "";
            this.lblTitle01.Text = "출력일";
            this.lblTitle01.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // P_PU_PRINT_OPTION
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 165);
            this.Controls.Add(this.m_pnlPlanFix);
            this.Controls.Add(this.m_btn_apply);
            this.Controls.Add(this.m_btn_exit);
            this.Controls.Add(this.panel5);
            this.Name = "P_PU_PRINT_OPTION";
            this.Text = "ERP iU";
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.m_pnlPlanFix.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tb_DT_PO)).EndInit();
            this.panelExt1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Duzon.Common.Controls.PanelExt panel5;
        private Duzon.Common.Controls.LabelExt label1;
        private Duzon.Common.Controls.RoundedButton m_btn_apply;
        private Duzon.Common.Controls.RoundedButton m_btn_exit;
        private Duzon.Common.Controls.PanelExt m_pnlPlanFix;
        private Duzon.Common.Controls.PanelExt panelExt1;
        private Duzon.Common.Controls.LabelExt lblTitle01;
        private Duzon.Common.Controls.PanelExt panel19;
        private Duzon.Common.Controls.LabelExt labelExt1;
        private Duzon.Common.Controls.DatePicker tb_DT_PO;

    }
}
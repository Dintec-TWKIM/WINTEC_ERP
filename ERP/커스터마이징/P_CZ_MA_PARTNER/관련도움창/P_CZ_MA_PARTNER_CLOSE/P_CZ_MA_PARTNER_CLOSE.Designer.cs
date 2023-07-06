namespace cz
{
    partial class P_CZ_MA_PARTNER_CLOSE
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel39 = new Duzon.Common.Controls.PanelExt();
            this.meb사업자등록번호 = new Duzon.Common.Controls.MaskedEditBox();
            this.txt거래처명 = new Duzon.Common.Controls.TextBoxExt();
            this.txt가져올회사코드 = new Duzon.Common.Controls.TextBoxExt();
            this.panelExt1 = new Duzon.Common.Controls.PanelExt();
            this.panel45 = new Duzon.Common.Controls.PanelExt();
            this.panel46 = new Duzon.Common.Controls.PanelExt();
            this.lbl사업자등록번호 = new Duzon.Common.Controls.LabelExt();
            this.lbl거래처명 = new Duzon.Common.Controls.LabelExt();
            this.lbl가져올회사코드 = new Duzon.Common.Controls.LabelExt();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt조회내역 = new Duzon.Common.Controls.TextBoxExt();
            this.btn확인 = new Duzon.Common.Controls.RoundedButton(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel39.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.meb사업자등록번호)).BeginInit();
            this.panel46.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel39);
            this.groupBox1.Location = new System.Drawing.Point(12, 55);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(378, 106);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "거래처정보(조회대상)";
            // 
            // panel39
            // 
            this.panel39.BackColor = System.Drawing.Color.White;
            this.panel39.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel39.Controls.Add(this.meb사업자등록번호);
            this.panel39.Controls.Add(this.txt거래처명);
            this.panel39.Controls.Add(this.txt가져올회사코드);
            this.panel39.Controls.Add(this.panelExt1);
            this.panel39.Controls.Add(this.panel45);
            this.panel39.Controls.Add(this.panel46);
            this.panel39.Location = new System.Drawing.Point(6, 20);
            this.panel39.Name = "panel39";
            this.panel39.Size = new System.Drawing.Size(366, 79);
            this.panel39.TabIndex = 34;
            // 
            // meb사업자등록번호
            // 
            this.meb사업자등록번호.AccessibleDescription = "MaskedEdit TextBox";
            this.meb사업자등록번호.AccessibleName = "MaskedEditBox";
            this.meb사업자등록번호.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.meb사업자등록번호.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.meb사업자등록번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.meb사업자등록번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.meb사업자등록번호.Culture = new System.Globalization.CultureInfo("ko-KR");
            this.meb사업자등록번호.Location = new System.Drawing.Point(108, 53);
            this.meb사업자등록번호.Mask = "###-##-#####";
            this.meb사업자등록번호.MaxLength = 12;
            this.meb사업자등록번호.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.meb사업자등록번호.Name = "meb사업자등록번호";
            this.meb사업자등록번호.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.meb사업자등록번호.Size = new System.Drawing.Size(140, 21);
            this.meb사업자등록번호.TabIndex = 256;
            this.meb사업자등록번호.Tag = "NO_COMPANY";
            this.meb사업자등록번호.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt거래처명
            // 
            this.txt거래처명.BackColor = System.Drawing.Color.White;
            this.txt거래처명.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt거래처명.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt거래처명.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txt거래처명.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txt거래처명.Location = new System.Drawing.Point(108, 29);
            this.txt거래처명.MaxLength = 200;
            this.txt거래처명.Name = "txt거래처명";
            this.txt거래처명.Size = new System.Drawing.Size(253, 21);
            this.txt거래처명.TabIndex = 255;
            this.txt거래처명.Tag = "NM_CONTRACT";
            // 
            // txt가져올회사코드
            // 
            this.txt가져올회사코드.BackColor = System.Drawing.Color.White;
            this.txt가져올회사코드.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt가져올회사코드.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt가져올회사코드.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txt가져올회사코드.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txt가져올회사코드.Location = new System.Drawing.Point(108, 3);
            this.txt가져올회사코드.MaxLength = 200;
            this.txt가져올회사코드.Name = "txt가져올회사코드";
            this.txt가져올회사코드.Size = new System.Drawing.Size(125, 21);
            this.txt가져올회사코드.TabIndex = 254;
            this.txt가져올회사코드.Tag = "NM_CONTRACT";
            // 
            // panelExt1
            // 
            this.panelExt1.BackColor = System.Drawing.Color.Transparent;
            this.panelExt1.Location = new System.Drawing.Point(-18, 52);
            this.panelExt1.Name = "panelExt1";
            this.panelExt1.Size = new System.Drawing.Size(400, 1);
            this.panelExt1.TabIndex = 38;
            // 
            // panel45
            // 
            this.panel45.BackColor = System.Drawing.Color.Transparent;
            this.panel45.Location = new System.Drawing.Point(5, 26);
            this.panel45.Name = "panel45";
            this.panel45.Size = new System.Drawing.Size(400, 1);
            this.panel45.TabIndex = 37;
            // 
            // panel46
            // 
            this.panel46.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel46.Controls.Add(this.lbl사업자등록번호);
            this.panel46.Controls.Add(this.lbl거래처명);
            this.panel46.Controls.Add(this.lbl가져올회사코드);
            this.panel46.Location = new System.Drawing.Point(1, 1);
            this.panel46.Name = "panel46";
            this.panel46.Size = new System.Drawing.Size(103, 75);
            this.panel46.TabIndex = 21;
            // 
            // lbl사업자등록번호
            // 
            this.lbl사업자등록번호.Location = new System.Drawing.Point(2, 54);
            this.lbl사업자등록번호.Name = "lbl사업자등록번호";
            this.lbl사업자등록번호.Size = new System.Drawing.Size(98, 18);
            this.lbl사업자등록번호.TabIndex = 10;
            this.lbl사업자등록번호.Text = "사업자등록번호";
            this.lbl사업자등록번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl거래처명
            // 
            this.lbl거래처명.Location = new System.Drawing.Point(3, 30);
            this.lbl거래처명.Name = "lbl거래처명";
            this.lbl거래처명.Size = new System.Drawing.Size(98, 18);
            this.lbl거래처명.TabIndex = 9;
            this.lbl거래처명.Text = "거래처명";
            this.lbl거래처명.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl가져올회사코드
            // 
            this.lbl가져올회사코드.Location = new System.Drawing.Point(3, 5);
            this.lbl가져올회사코드.Name = "lbl가져올회사코드";
            this.lbl가져올회사코드.Size = new System.Drawing.Size(98, 18);
            this.lbl가져올회사코드.TabIndex = 8;
            this.lbl가져올회사코드.Text = "가져올회사코드";
            this.lbl가져올회사코드.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txt조회내역);
            this.groupBox2.Location = new System.Drawing.Point(12, 167);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(378, 106);
            this.groupBox2.TabIndex = 35;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "휴페업조회내역";
            // 
            // txt조회내역
            // 
            this.txt조회내역.BackColor = System.Drawing.Color.White;
            this.txt조회내역.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt조회내역.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt조회내역.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txt조회내역.Location = new System.Drawing.Point(6, 18);
            this.txt조회내역.MaxLength = 100000;
            this.txt조회내역.Multiline = true;
            this.txt조회내역.Name = "txt조회내역";
            this.txt조회내역.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt조회내역.Size = new System.Drawing.Size(366, 80);
            this.txt조회내역.TabIndex = 269;
            this.txt조회내역.Tag = "NOTE";
            this.txt조회내역.UseKeyEnter = false;
            this.txt조회내역.UseKeyF3 = false;
            // 
            // btn확인
            // 
            this.btn확인.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn확인.BackColor = System.Drawing.Color.White;
            this.btn확인.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn확인.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn확인.ForeColor = System.Drawing.Color.White;
            this.btn확인.Location = new System.Drawing.Point(159, 279);
            this.btn확인.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn확인.Name = "btn확인";
            this.btn확인.Size = new System.Drawing.Size(93, 19);
            this.btn확인.TabIndex = 36;
            this.btn확인.TabStop = false;
            this.btn확인.Text = "확인";
            this.btn확인.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn확인.UseVisualStyleBackColor = false;
            // 
            // P_CZ_MA_PARTNER_CLOSE
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CaptionPaint = true;
            this.ClientSize = new System.Drawing.Size(402, 305);
            this.Controls.Add(this.btn확인);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "P_CZ_MA_PARTNER_CLOSE";
            this.Text = "P_MA_PARTNER_CLOSE";
            this.TitleText = "휴폐업조회";
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panel39.ResumeLayout(false);
            this.panel39.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.meb사업자등록번호)).EndInit();
            this.panel46.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private Duzon.Common.Controls.PanelExt panel39;
        private Duzon.Common.Controls.PanelExt panel45;
        private Duzon.Common.Controls.PanelExt panel46;
        private Duzon.Common.Controls.LabelExt lbl거래처명;
        private Duzon.Common.Controls.LabelExt lbl가져올회사코드;
        private Duzon.Common.Controls.TextBoxExt txt거래처명;
        private Duzon.Common.Controls.TextBoxExt txt가져올회사코드;
        private Duzon.Common.Controls.PanelExt panelExt1;
        private Duzon.Common.Controls.LabelExt lbl사업자등록번호;
        private Duzon.Common.Controls.MaskedEditBox meb사업자등록번호;
        private System.Windows.Forms.GroupBox groupBox2;
        private Duzon.Common.Controls.TextBoxExt txt조회내역;
        private Duzon.Common.Controls.RoundedButton btn확인;
    }
}
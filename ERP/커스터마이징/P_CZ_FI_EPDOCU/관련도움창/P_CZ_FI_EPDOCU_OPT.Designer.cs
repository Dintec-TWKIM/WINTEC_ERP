namespace cz
{
    partial class P_CZ_FI_EPDOCU_OPT
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
            this._btn닫기 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.roundedButton1 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panelEx1 = new Duzon.Common.Controls.PanelEx();
            this.dt_작성일자 = new Duzon.Common.Controls.DatePicker();
            this.lbl작성일자 = new Duzon.Common.Controls.LabelExt();
            this.dt_회계일자 = new Duzon.Common.Controls.DatePicker();
            this.lbl회계일자 = new Duzon.Common.Controls.LabelExt();
            this.panelEx2 = new Duzon.Common.Controls.PanelEx();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt_작성일자)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_회계일자)).BeginInit();
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _btn닫기
            // 
            this._btn닫기.BackColor = System.Drawing.Color.White;
            this._btn닫기.Cursor = System.Windows.Forms.Cursors.Hand;
            this._btn닫기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btn닫기.Location = new System.Drawing.Point(10, 9);
            this._btn닫기.MaximumSize = new System.Drawing.Size(0, 19);
            this._btn닫기.Name = "_btn닫기";
            this._btn닫기.Size = new System.Drawing.Size(112, 19);
            this._btn닫기.TabIndex = 114;
            this._btn닫기.TabStop = false;
            this._btn닫기.Text = "건별 전표처리";
            this._btn닫기.UseVisualStyleBackColor = true;
            this._btn닫기.Click += new System.EventHandler(this._btn닫기_Click);
            // 
            // roundedButton1
            // 
            this.roundedButton1.BackColor = System.Drawing.Color.White;
            this.roundedButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.roundedButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roundedButton1.Location = new System.Drawing.Point(128, 9);
            this.roundedButton1.MaximumSize = new System.Drawing.Size(0, 19);
            this.roundedButton1.Name = "roundedButton1";
            this.roundedButton1.Size = new System.Drawing.Size(112, 19);
            this.roundedButton1.TabIndex = 115;
            this.roundedButton1.TabStop = false;
            this.roundedButton1.Text = "일괄 전표처리";
            this.roundedButton1.UseVisualStyleBackColor = true;
            this.roundedButton1.Click += new System.EventHandler(this.roundedButton1_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.panelEx1);
            this.flowLayoutPanel1.Controls.Add(this.panelEx2);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 50);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(263, 117);
            this.flowLayoutPanel1.TabIndex = 116;
            // 
            // panelEx1
            // 
            this.panelEx1.ColorA = System.Drawing.Color.Empty;
            this.panelEx1.ColorB = System.Drawing.Color.Empty;
            this.panelEx1.Controls.Add(this.dt_작성일자);
            this.panelEx1.Controls.Add(this.lbl작성일자);
            this.panelEx1.Controls.Add(this.dt_회계일자);
            this.panelEx1.Controls.Add(this.lbl회계일자);
            this.panelEx1.Location = new System.Drawing.Point(3, 3);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(256, 59);
            this.panelEx1.TabIndex = 0;
            // 
            // dt_작성일자
            // 
            this.dt_작성일자.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dt_작성일자.Location = new System.Drawing.Point(87, 30);
            this.dt_작성일자.Mask = "####/##/##";
            this.dt_작성일자.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.dt_작성일자.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dt_작성일자.MaximumSize = new System.Drawing.Size(0, 21);
            this.dt_작성일자.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dt_작성일자.Name = "dt_작성일자";
            this.dt_작성일자.Size = new System.Drawing.Size(85, 21);
            this.dt_작성일자.TabIndex = 118;
            this.dt_작성일자.Value = new System.DateTime(((long)(0)));
            // 
            // lbl작성일자
            // 
            this.lbl작성일자.BackColor = System.Drawing.Color.Transparent;
            this.lbl작성일자.Location = new System.Drawing.Point(7, 31);
            this.lbl작성일자.Name = "lbl작성일자";
            this.lbl작성일자.Size = new System.Drawing.Size(73, 18);
            this.lbl작성일자.TabIndex = 117;
            this.lbl작성일자.Tag = "";
            this.lbl작성일자.Text = "작성일자";
            this.lbl작성일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dt_회계일자
            // 
            this.dt_회계일자.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dt_회계일자.Location = new System.Drawing.Point(87, 6);
            this.dt_회계일자.Mask = "####/##/##";
            this.dt_회계일자.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.dt_회계일자.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dt_회계일자.MaximumSize = new System.Drawing.Size(0, 21);
            this.dt_회계일자.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dt_회계일자.Name = "dt_회계일자";
            this.dt_회계일자.Size = new System.Drawing.Size(85, 21);
            this.dt_회계일자.TabIndex = 116;
            this.dt_회계일자.Value = new System.DateTime(((long)(0)));
            // 
            // lbl회계일자
            // 
            this.lbl회계일자.BackColor = System.Drawing.Color.Transparent;
            this.lbl회계일자.Location = new System.Drawing.Point(7, 7);
            this.lbl회계일자.Name = "lbl회계일자";
            this.lbl회계일자.Size = new System.Drawing.Size(73, 18);
            this.lbl회계일자.TabIndex = 115;
            this.lbl회계일자.Tag = "";
            this.lbl회계일자.Text = "회계일자";
            this.lbl회계일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelEx2
            // 
            this.panelEx2.ColorA = System.Drawing.Color.Empty;
            this.panelEx2.ColorB = System.Drawing.Color.Empty;
            this.panelEx2.Controls.Add(this._btn닫기);
            this.panelEx2.Controls.Add(this.roundedButton1);
            this.panelEx2.Location = new System.Drawing.Point(3, 68);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(256, 40);
            this.panelEx2.TabIndex = 1;
            // 
            // P_CZ_FI_EPDOCU_OPT
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CaptionPaint = true;
            this.ClientSize = new System.Drawing.Size(268, 169);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "P_CZ_FI_EPDOCU_OPT";
            this.Text = "P_FI_EPDOCU_OPT";
            this.TitleText = "전표처리";
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dt_작성일자)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_회계일자)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private Duzon.Common.Controls.RoundedButton _btn닫기;
        private Duzon.Common.Controls.RoundedButton roundedButton1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Duzon.Common.Controls.PanelEx panelEx1;
        private Duzon.Common.Controls.PanelEx panelEx2;
        private Duzon.Common.Controls.DatePicker dt_작성일자;
        private Duzon.Common.Controls.LabelExt lbl작성일자;
        private Duzon.Common.Controls.DatePicker dt_회계일자;
        private Duzon.Common.Controls.LabelExt lbl회계일자;
        #endregion
    }
}
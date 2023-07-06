namespace cz
{
    partial class P_CZ_FI_EPDOCU_ETAXDT
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
            this.btn확인 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn취소 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.dt발행년월 = new Duzon.Common.Controls.DatePicker();
            this.panelExt2 = new Duzon.Common.Controls.PanelExt();
            this.labelExt1 = new Duzon.Common.Controls.LabelExt();
            this.m_FDT = new Duzon.Common.Controls.DropDownComboBox();
            this.labelExt2 = new Duzon.Common.Controls.LabelExt();
            this.m_TDT = new Duzon.Common.Controls.DropDownComboBox();
            this.panelExt1 = new Duzon.Common.Controls.PanelExt();
            this.cbo발행구분 = new Duzon.Common.Controls.DropDownComboBox();
            this.panelExt3 = new Duzon.Common.Controls.PanelExt();
            this.labelExt3 = new Duzon.Common.Controls.LabelExt();
            this.panelExt4 = new Duzon.Common.Controls.PanelExt();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt발행년월)).BeginInit();
            this.panelExt2.SuspendLayout();
            this.panelExt1.SuspendLayout();
            this.panelExt3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn확인
            // 
            this.btn확인.BackColor = System.Drawing.Color.White;
            this.btn확인.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn확인.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn확인.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn확인.Location = new System.Drawing.Point(82, 112);
            this.btn확인.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn확인.Name = "btn확인";
            this.btn확인.Size = new System.Drawing.Size(62, 19);
            this.btn확인.TabIndex = 114;
            this.btn확인.TabStop = false;
            this.btn확인.Text = "확인";
            this.btn확인.UseVisualStyleBackColor = true;
            this.btn확인.Click += new System.EventHandler(this.btn확인_Click);
            // 
            // btn취소
            // 
            this.btn취소.BackColor = System.Drawing.Color.White;
            this.btn취소.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn취소.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn취소.Location = new System.Drawing.Point(147, 112);
            this.btn취소.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn취소.Name = "btn취소";
            this.btn취소.Size = new System.Drawing.Size(62, 19);
            this.btn취소.TabIndex = 115;
            this.btn취소.TabStop = false;
            this.btn취소.Text = "취소";
            this.btn취소.UseVisualStyleBackColor = true;
            // 
            // dt발행년월
            // 
            this.dt발행년월.BackColor = System.Drawing.Color.White;
            this.dt발행년월.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dt발행년월.Location = new System.Drawing.Point(92, 2);
            this.dt발행년월.Mask = "####/##";
            this.dt발행년월.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.dt발행년월.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dt발행년월.MaximumSize = new System.Drawing.Size(0, 21);
            this.dt발행년월.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dt발행년월.Modified = true;
            this.dt발행년월.Name = "dt발행년월";
            this.dt발행년월.ShowUpDown = true;
            this.dt발행년월.Size = new System.Drawing.Size(69, 21);
            this.dt발행년월.TabIndex = 136;
            this.dt발행년월.Value = new System.DateTime(((long)(0)));
            // 
            // panelExt2
            // 
            this.panelExt2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panelExt2.Controls.Add(this.labelExt1);
            this.panelExt2.Location = new System.Drawing.Point(1, 1);
            this.panelExt2.Name = "panelExt2";
            this.panelExt2.Size = new System.Drawing.Size(86, 26);
            this.panelExt2.TabIndex = 135;
            // 
            // labelExt1
            // 
            this.labelExt1.Location = new System.Drawing.Point(3, 4);
            this.labelExt1.Name = "labelExt1";
            this.labelExt1.Size = new System.Drawing.Size(79, 18);
            this.labelExt1.TabIndex = 0;
            this.labelExt1.Text = "발행일자";
            this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_FDT
            // 
            this.m_FDT.AutoDropDown = true;
            this.m_FDT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_FDT.ItemHeight = 12;
            this.m_FDT.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31"});
            this.m_FDT.Location = new System.Drawing.Point(162, 2);
            this.m_FDT.Name = "m_FDT";
            this.m_FDT.Size = new System.Drawing.Size(42, 20);
            this.m_FDT.TabIndex = 137;
            // 
            // labelExt2
            // 
            this.labelExt2.Location = new System.Drawing.Point(208, 3);
            this.labelExt2.Name = "labelExt2";
            this.labelExt2.Size = new System.Drawing.Size(13, 18);
            this.labelExt2.TabIndex = 138;
            this.labelExt2.Text = "∼";
            this.labelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_TDT
            // 
            this.m_TDT.AutoDropDown = true;
            this.m_TDT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_TDT.ItemHeight = 12;
            this.m_TDT.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31"});
            this.m_TDT.Location = new System.Drawing.Point(223, 2);
            this.m_TDT.Name = "m_TDT";
            this.m_TDT.Size = new System.Drawing.Size(42, 20);
            this.m_TDT.TabIndex = 139;
            // 
            // panelExt1
            // 
            this.panelExt1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelExt1.Controls.Add(this.cbo발행구분);
            this.panelExt1.Controls.Add(this.panelExt3);
            this.panelExt1.Controls.Add(this.m_TDT);
            this.panelExt1.Controls.Add(this.labelExt2);
            this.panelExt1.Controls.Add(this.panelExt2);
            this.panelExt1.Controls.Add(this.m_FDT);
            this.panelExt1.Controls.Add(this.dt발행년월);
            this.panelExt1.Location = new System.Drawing.Point(10, 52);
            this.panelExt1.Name = "panelExt1";
            this.panelExt1.Size = new System.Drawing.Size(271, 55);
            this.panelExt1.TabIndex = 140;
            // 
            // cbo발행구분
            // 
            this.cbo발행구분.AutoDropDown = true;
            this.cbo발행구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo발행구분.ItemHeight = 12;
            this.cbo발행구분.Location = new System.Drawing.Point(92, 30);
            this.cbo발행구분.Name = "cbo발행구분";
            this.cbo발행구분.Size = new System.Drawing.Size(173, 20);
            this.cbo발행구분.TabIndex = 141;
            // 
            // panelExt3
            // 
            this.panelExt3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panelExt3.Controls.Add(this.labelExt3);
            this.panelExt3.Controls.Add(this.panelExt4);
            this.panelExt3.Location = new System.Drawing.Point(1, 26);
            this.panelExt3.Name = "panelExt3";
            this.panelExt3.Size = new System.Drawing.Size(86, 26);
            this.panelExt3.TabIndex = 140;
            // 
            // labelExt3
            // 
            this.labelExt3.Location = new System.Drawing.Point(3, 4);
            this.labelExt3.Name = "labelExt3";
            this.labelExt3.Size = new System.Drawing.Size(79, 18);
            this.labelExt3.TabIndex = 0;
            this.labelExt3.Text = "발행구분";
            this.labelExt3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelExt4
            // 
            this.panelExt4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelExt4.BackColor = System.Drawing.Color.Transparent;
            this.panelExt4.BackgroundImage = global::cz.Properties.Resources.BackgroundImage;
            this.panelExt4.Location = new System.Drawing.Point(5, 0);
            this.panelExt4.Name = "panelExt4";
            this.panelExt4.Size = new System.Drawing.Size(261, 1);
            this.panelExt4.TabIndex = 109;
            // 
            // P_CZ_FI_EPDOCU_ETAXDT
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CaptionPaint = true;
            this.ClientSize = new System.Drawing.Size(291, 137);
            this.Controls.Add(this.panelExt1);
            this.Controls.Add(this.btn취소);
            this.Controls.Add(this.btn확인);
            this.Name = "P_CZ_FI_EPDOCU_ETAXDT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "데이타내려받기";
            this.TitleText = "데이타내려받기";
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt발행년월)).EndInit();
            this.panelExt2.ResumeLayout(false);
            this.panelExt1.ResumeLayout(false);
            this.panelExt3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private Duzon.Common.Controls.RoundedButton btn확인;
        private Duzon.Common.Controls.RoundedButton btn취소;
        private Duzon.Common.Controls.DatePicker dt발행년월;
        private Duzon.Common.Controls.PanelExt panelExt2;
        private Duzon.Common.Controls.LabelExt labelExt1;
        private Duzon.Common.Controls.DropDownComboBox m_FDT;
        private Duzon.Common.Controls.LabelExt labelExt2;
        private Duzon.Common.Controls.DropDownComboBox m_TDT;
        private Duzon.Common.Controls.PanelExt panelExt1;
        private Duzon.Common.Controls.PanelExt panelExt4;
        private Duzon.Common.Controls.PanelExt panelExt3;
        private Duzon.Common.Controls.LabelExt labelExt3;
        private Duzon.Common.Controls.DropDownComboBox cbo발행구분;
        #endregion
    }
}
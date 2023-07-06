namespace sale
{
    partial class P_SA_GIRSCH_SUB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_SA_GIRSCH_SUB));
            this.m_titlePanel = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_lblTitle = new System.Windows.Forms.Label();
            this.pnlMain = new Duzon.Common.Controls.PanelExt();
            this.rdo전체 = new Duzon.Common.Controls.RadioButtonExt();
            this.rdo출고검사 = new Duzon.Common.Controls.RadioButtonExt();
            this.btn취소 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn확인 = new Duzon.Common.Controls.RoundedButton(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.m_titlePanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdo전체)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdo출고검사)).BeginInit();
            this.SuspendLayout();
            // 
            // m_titlePanel
            // 
            this.m_titlePanel.BackColor = System.Drawing.Color.White;
            this.m_titlePanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_titlePanel.BackgroundImage")));
            this.m_titlePanel.Controls.Add(this.panel3);
            this.m_titlePanel.Controls.Add(this.panel1);
            this.m_titlePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_titlePanel.ForeColor = System.Drawing.Color.Black;
            this.m_titlePanel.Location = new System.Drawing.Point(0, 0);
            this.m_titlePanel.Name = "m_titlePanel";
            this.m_titlePanel.Size = new System.Drawing.Size(218, 47);
            this.m_titlePanel.TabIndex = 25;
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(139, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(79, 47);
            this.panel3.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Controls.Add(this.m_lblTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(210, 47);
            this.panel1.TabIndex = 2;
            // 
            // m_lblTitle
            // 
            this.m_lblTitle.AutoSize = true;
            this.m_lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.m_lblTitle.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.m_lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(44)))), ((int)(((byte)(80)))));
            this.m_lblTitle.Location = new System.Drawing.Point(21, 15);
            this.m_lblTitle.Name = "m_lblTitle";
            this.m_lblTitle.Size = new System.Drawing.Size(87, 15);
            this.m_lblTitle.TabIndex = 1;
            this.m_lblTitle.Text = "출력도움창";
            this.m_lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlMain
            // 
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.rdo출고검사);
            this.pnlMain.Controls.Add(this.rdo전체);
            this.pnlMain.Location = new System.Drawing.Point(8, 53);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(202, 59);
            this.pnlMain.TabIndex = 34;
            // 
            // rdo전체
            // 
            this.rdo전체.Checked = true;
            this.rdo전체.Location = new System.Drawing.Point(34, 5);
            this.rdo전체.Name = "rdo전체";
            this.rdo전체.Size = new System.Drawing.Size(123, 18);
            this.rdo전체.TabIndex = 35;
            this.rdo전체.TabStop = true;
            this.rdo전체.Text = "선택내용전체";
            this.rdo전체.TextDD = null;
            this.rdo전체.UseKeyEnter = true;
            this.rdo전체.UseVisualStyleBackColor = true;
            // 
            // rdo출고검사
            // 
            this.rdo출고검사.Location = new System.Drawing.Point(34, 31);
            this.rdo출고검사.Name = "rdo출고검사";
            this.rdo출고검사.Size = new System.Drawing.Size(123, 18);
            this.rdo출고검사.TabIndex = 36;
            this.rdo출고검사.Text = "출고검사";
            this.rdo출고검사.TextDD = null;
            this.rdo출고검사.UseKeyEnter = true;
            this.rdo출고검사.UseVisualStyleBackColor = true;
            // 
            // btn취소
            // 
            this.btn취소.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn취소.BackColor = System.Drawing.Color.White;
            this.btn취소.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn취소.Location = new System.Drawing.Point(115, 118);
            this.btn취소.MaximumSize = new System.Drawing.Size(0, 22);
            this.btn취소.Name = "btn취소";
            this.btn취소.Size = new System.Drawing.Size(70, 22);
            this.btn취소.TabIndex = 36;
            this.btn취소.TabStop = false;
            this.btn취소.Text = "취소";
            this.btn취소.UseVisualStyleBackColor = true;
            // 
            // btn확인
            // 
            this.btn확인.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn확인.BackColor = System.Drawing.Color.White;
            this.btn확인.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn확인.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn확인.Location = new System.Drawing.Point(39, 118);
            this.btn확인.MaximumSize = new System.Drawing.Size(0, 22);
            this.btn확인.Name = "btn확인";
            this.btn확인.Size = new System.Drawing.Size(70, 22);
            this.btn확인.TabIndex = 35;
            this.btn확인.TabStop = false;
            this.btn확인.Text = "확인";
            this.btn확인.UseVisualStyleBackColor = true;
            // 
            // P_SA_GIRSCH_SUB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 145);
            this.Controls.Add(this.btn취소);
            this.Controls.Add(this.btn확인);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.m_titlePanel);
            this.Name = "P_SA_GIRSCH_SUB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.m_titlePanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rdo전체)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdo출고검사)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel m_titlePanel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label m_lblTitle;
        private Duzon.Common.Controls.PanelExt pnlMain;
        private Duzon.Common.Controls.RadioButtonExt rdo출고검사;
        private Duzon.Common.Controls.RadioButtonExt rdo전체;
        private Duzon.Common.Controls.RoundedButton btn취소;
        private Duzon.Common.Controls.RoundedButton btn확인;
    }
}
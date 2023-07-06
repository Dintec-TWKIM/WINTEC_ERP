namespace cz
{
    partial class P_CZ_BI_WEB_BROWSER
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
            this.webBrowserExt1 = new Duzon.Common.Controls.WebBrowserExt();
            this.pnlHide = new Duzon.Common.Controls.PanelEx();
            this.mDataArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this.pnlHide);
            this.mDataArea.Controls.Add(this.webBrowserExt1);
            this.mDataArea.Size = new System.Drawing.Size(1090, 756);
            // 
            // webBrowserExt1
            // 
            this.webBrowserExt1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserExt1.Location = new System.Drawing.Point(0, 0);
            this.webBrowserExt1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserExt1.Name = "webBrowserExt1";
            this.webBrowserExt1.Size = new System.Drawing.Size(1090, 756);
            this.webBrowserExt1.TabIndex = 0;
            // 
            // pnlHide
            // 
            this.pnlHide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlHide.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.pnlHide.ColorA = System.Drawing.Color.Empty;
            this.pnlHide.ColorB = System.Drawing.Color.Empty;
            this.pnlHide.Location = new System.Drawing.Point(865, 720);
            this.pnlHide.Name = "pnlHide";
            this.pnlHide.Size = new System.Drawing.Size(225, 36);
            this.pnlHide.TabIndex = 1;
            // 
            // P_CZ_BI_WEB_BROWSER
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "P_CZ_BI_WEB_BROWSER";
            this.Size = new System.Drawing.Size(1090, 796);
            this.TitleText = "P_CZ_BI_TEST";
            this.mDataArea.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Duzon.Common.Controls.WebBrowserExt webBrowserExt1;
        private Duzon.Common.Controls.PanelEx pnlHide;
    }
}
namespace Dintec.AutoWeb
{
    partial class AutoWebBroswer
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
            this.web메인 = new Dintec.DxWebBrowser();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.SuspendLayout();
            // 
            // web메인
            // 
            this.web메인.Dock = System.Windows.Forms.DockStyle.Fill;
            this.web메인.Location = new System.Drawing.Point(0, 0);
            this.web메인.MinimumSize = new System.Drawing.Size(20, 20);
            this.web메인.Name = "web메인";
            this.web메인.ScriptErrorsSuppressed = true;
            this.web메인.Size = new System.Drawing.Size(1643, 959);
            this.web메인.TabIndex = 0;
            // 
            // AutoWebBroswer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1643, 959);
            this.Controls.Add(this.web메인);
            this.Name = "AutoWebBroswer";
            this.Text = "AutoWebBroswer";
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DxWebBrowser web메인;
    }
}
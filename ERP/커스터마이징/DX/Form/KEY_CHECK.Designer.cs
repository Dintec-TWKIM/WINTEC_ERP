
namespace DX
{
	partial class KEY_CHECK
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
			this.spc메인 = new System.Windows.Forms.SplitContainer();
			this.panel1 = new System.Windows.Forms.Panel();
			this.web아이템 = new System.Windows.Forms.WebBrowser();
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spc메인)).BeginInit();
			this.spc메인.Panel1.SuspendLayout();
			this.spc메인.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// spc메인
			// 
			this.spc메인.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spc메인.Location = new System.Drawing.Point(6, 53);
			this.spc메인.Name = "spc메인";
			// 
			// spc메인.Panel1
			// 
			this.spc메인.Panel1.Controls.Add(this.panel1);
			this.spc메인.Size = new System.Drawing.Size(868, 817);
			this.spc메인.SplitterDistance = 776;
			this.spc메인.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.web아이템);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(776, 817);
			this.panel1.TabIndex = 0;
			// 
			// web아이템
			// 
			this.web아이템.Dock = System.Windows.Forms.DockStyle.Fill;
			this.web아이템.Location = new System.Drawing.Point(0, 0);
			this.web아이템.MinimumSize = new System.Drawing.Size(20, 20);
			this.web아이템.Name = "web아이템";
			this.web아이템.ScrollBarsEnabled = false;
			this.web아이템.Size = new System.Drawing.Size(774, 815);
			this.web아이템.TabIndex = 2;
			// 
			// KEY_CHECK
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(880, 876);
			this.Controls.Add(this.spc메인);
			this.Name = "KEY_CHECK";
			this.Padding = new System.Windows.Forms.Padding(6, 53, 6, 6);
			this.ShowInTaskbar = false;
			this.Text = "KEY_CHECK";
			this.TitleText = "키워드 체크";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.spc메인.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.spc메인)).EndInit();
			this.spc메인.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer spc메인;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.WebBrowser web아이템;
	}
}
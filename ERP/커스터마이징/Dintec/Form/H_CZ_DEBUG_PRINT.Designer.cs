namespace Dintec
{
	partial class H_CZ_DEBUG_PRINT
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
			this.txtDebug = new Duzon.Common.Controls.TextBoxExt();
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.SuspendLayout();
			// 
			// txtDebug
			// 
			this.txtDebug.BackColor = System.Drawing.Color.White;
			this.txtDebug.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txtDebug.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtDebug.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtDebug.Location = new System.Drawing.Point(10, 10);
			this.txtDebug.Multiline = true;
			this.txtDebug.Name = "txtDebug";
			this.txtDebug.Size = new System.Drawing.Size(713, 443);
			this.txtDebug.TabIndex = 3;
			this.txtDebug.Tag = "DC_RMK_INQ";
			this.txtDebug.UseKeyEnter = false;
			// 
			// H_CZ_DEBUG_PRINT
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(733, 463);
			this.Controls.Add(this.txtDebug);
			this.Name = "H_CZ_DEBUG_PRINT";
			this.Padding = new System.Windows.Forms.Padding(10);
			this.Text = "H_CZ_DEBUG_PRINT";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Duzon.Common.Controls.TextBoxExt txtDebug;
	}
}
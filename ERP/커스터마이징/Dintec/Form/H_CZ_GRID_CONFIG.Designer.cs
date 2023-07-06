namespace Dintec
{
	partial class H_CZ_GRID_CONFIG
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
			this.flexibleRoundedCornerBox1 = new Duzon.Common.Controls.FlexibleRoundedCornerBox();
			this.btn설정적용 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn설정저장 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.labelExt2 = new Duzon.Common.Controls.LabelExt();
			this.btn초기화 = new Duzon.Common.Controls.RoundedButton(this.components);
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.flexibleRoundedCornerBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// flexibleRoundedCornerBox1
			// 
			this.flexibleRoundedCornerBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(247)))), ((int)(((byte)(248)))));
			this.flexibleRoundedCornerBox1.Controls.Add(this.btn설정적용);
			this.flexibleRoundedCornerBox1.Controls.Add(this.btn설정저장);
			this.flexibleRoundedCornerBox1.Controls.Add(this.labelExt2);
			this.flexibleRoundedCornerBox1.Controls.Add(this.btn초기화);
			this.flexibleRoundedCornerBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flexibleRoundedCornerBox1.Location = new System.Drawing.Point(6, 6);
			this.flexibleRoundedCornerBox1.Name = "flexibleRoundedCornerBox1";
			this.flexibleRoundedCornerBox1.Size = new System.Drawing.Size(281, 88);
			this.flexibleRoundedCornerBox1.TabIndex = 2;
			// 
			// btn설정적용
			// 
			this.btn설정적용.BackColor = System.Drawing.Color.White;
			this.btn설정적용.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn설정적용.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn설정적용.Location = new System.Drawing.Point(102, 46);
			this.btn설정적용.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn설정적용.Name = "btn설정적용";
			this.btn설정적용.Size = new System.Drawing.Size(70, 19);
			this.btn설정적용.TabIndex = 14;
			this.btn설정적용.TabStop = false;
			this.btn설정적용.Text = "설정적용";
			this.btn설정적용.UseVisualStyleBackColor = false;
			// 
			// btn설정저장
			// 
			this.btn설정저장.BackColor = System.Drawing.Color.White;
			this.btn설정저장.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn설정저장.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn설정저장.Location = new System.Drawing.Point(26, 46);
			this.btn설정저장.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn설정저장.Name = "btn설정저장";
			this.btn설정저장.Size = new System.Drawing.Size(70, 19);
			this.btn설정저장.TabIndex = 13;
			this.btn설정저장.TabStop = false;
			this.btn설정저장.Text = "설정저장";
			this.btn설정저장.UseVisualStyleBackColor = false;
			// 
			// labelExt2
			// 
			this.labelExt2.Location = new System.Drawing.Point(28, 20);
			this.labelExt2.Name = "labelExt2";
			this.labelExt2.Size = new System.Drawing.Size(196, 14);
			this.labelExt2.TabIndex = 12;
			this.labelExt2.Text = "작업을 선택하세요";
			// 
			// btn초기화
			// 
			this.btn초기화.BackColor = System.Drawing.Color.White;
			this.btn초기화.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn초기화.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn초기화.Location = new System.Drawing.Point(178, 46);
			this.btn초기화.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn초기화.Name = "btn초기화";
			this.btn초기화.Size = new System.Drawing.Size(70, 19);
			this.btn초기화.TabIndex = 11;
			this.btn초기화.TabStop = false;
			this.btn초기화.Text = "초기화";
			this.btn초기화.UseVisualStyleBackColor = false;
			// 
			// H_CZ_GRID_CONFIG
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(293, 100);
			this.Controls.Add(this.flexibleRoundedCornerBox1);
			this.Name = "H_CZ_GRID_CONFIG";
			this.Padding = new System.Windows.Forms.Padding(6);
			this.Text = "H_CZ_GRID_CONFIG";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.flexibleRoundedCornerBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Duzon.Common.Controls.FlexibleRoundedCornerBox flexibleRoundedCornerBox1;
		private Duzon.Common.Controls.RoundedButton btn설정적용;
		private Duzon.Common.Controls.RoundedButton btn설정저장;
		private Duzon.Common.Controls.LabelExt labelExt2;
		private Duzon.Common.Controls.RoundedButton btn초기화;
	}
}
namespace Dintec
{
	partial class H_CZ_CHECK_PW
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
			this.labelExt2 = new Duzon.Common.Controls.LabelExt();
			this.btn확인 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.txt비밀번호 = new Duzon.Common.Controls.TextBoxExt();
			this.labelExt1 = new Duzon.Common.Controls.LabelExt();
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.flexibleRoundedCornerBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// flexibleRoundedCornerBox1
			// 
			this.flexibleRoundedCornerBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(247)))), ((int)(((byte)(248)))));
			this.flexibleRoundedCornerBox1.Controls.Add(this.labelExt2);
			this.flexibleRoundedCornerBox1.Controls.Add(this.btn확인);
			this.flexibleRoundedCornerBox1.Controls.Add(this.txt비밀번호);
			this.flexibleRoundedCornerBox1.Controls.Add(this.labelExt1);
			this.flexibleRoundedCornerBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flexibleRoundedCornerBox1.Location = new System.Drawing.Point(10, 10);
			this.flexibleRoundedCornerBox1.Name = "flexibleRoundedCornerBox1";
			this.flexibleRoundedCornerBox1.Size = new System.Drawing.Size(329, 107);
			this.flexibleRoundedCornerBox1.TabIndex = 0;
			// 
			// labelExt2
			// 
			this.labelExt2.Location = new System.Drawing.Point(28, 20);
			this.labelExt2.Name = "labelExt2";
			this.labelExt2.Resizeble = true;
			this.labelExt2.Size = new System.Drawing.Size(196, 14);
			this.labelExt2.TabIndex = 12;
			this.labelExt2.Text = "비밀번호를 입력하세요";
			// 
			// btn확인
			// 
			this.btn확인.BackColor = System.Drawing.Color.White;
			this.btn확인.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn확인.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn확인.Location = new System.Drawing.Point(196, 44);
			this.btn확인.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn확인.Name = "btn확인";
			this.btn확인.Size = new System.Drawing.Size(70, 19);
			this.btn확인.TabIndex = 11;
			this.btn확인.TabStop = false;
			this.btn확인.Text = "확인";
			this.btn확인.UseVisualStyleBackColor = false;
			// 
			// txt비밀번호
			// 
			this.txt비밀번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt비밀번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt비밀번호.Location = new System.Drawing.Point(28, 42);
			this.txt비밀번호.Name = "txt비밀번호";
			this.txt비밀번호.PasswordChar = '*';
			this.txt비밀번호.SelectedAllEnabled = false;
			this.txt비밀번호.Size = new System.Drawing.Size(156, 21);
			this.txt비밀번호.TabIndex = 10;
			this.txt비밀번호.Tag = "";
			this.txt비밀번호.UseKeyEnter = true;
			this.txt비밀번호.UseKeyF3 = true;
			// 
			// labelExt1
			// 
			this.labelExt1.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.labelExt1.ForeColor = System.Drawing.Color.Red;
			this.labelExt1.Location = new System.Drawing.Point(26, 76);
			this.labelExt1.Name = "labelExt1";
			this.labelExt1.Resizeble = true;
			this.labelExt1.Size = new System.Drawing.Size(298, 14);
			this.labelExt1.TabIndex = 0;
			this.labelExt1.Text = "주의! 삭제한 자료는 복구할 수 없습니다.";
			// 
			// H_CZ_CHECK_PW
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(349, 127);
			this.Controls.Add(this.flexibleRoundedCornerBox1);
			this.Name = "H_CZ_CHECK_PW";
			this.Padding = new System.Windows.Forms.Padding(10);
			this.Text = "CheckOneMore";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.flexibleRoundedCornerBox1.ResumeLayout(false);
			this.flexibleRoundedCornerBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private Duzon.Common.Controls.FlexibleRoundedCornerBox flexibleRoundedCornerBox1;
		private Duzon.Common.Controls.LabelExt labelExt1;
		private Duzon.Common.Controls.TextBoxExt txt비밀번호;
		private Duzon.Common.Controls.LabelExt labelExt2;
		private Duzon.Common.Controls.RoundedButton btn확인;
	}
}
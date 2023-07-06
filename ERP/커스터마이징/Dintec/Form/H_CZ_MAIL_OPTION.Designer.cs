namespace Dintec
{
	partial class H_CZ_MAIL_OPTION
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
			this.rdo본문방식2 = new Duzon.Common.Controls.RadioButtonExt();
			this.rdo본문방식1 = new Duzon.Common.Controls.RadioButtonExt();
			this.labelExt2 = new Duzon.Common.Controls.LabelExt();
			this.btn보내기 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.labelExt1 = new Duzon.Common.Controls.LabelExt();
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.flexibleRoundedCornerBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.rdo본문방식2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo본문방식1)).BeginInit();
			this.SuspendLayout();
			// 
			// flexibleRoundedCornerBox1
			// 
			this.flexibleRoundedCornerBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(247)))), ((int)(((byte)(248)))));
			this.flexibleRoundedCornerBox1.Controls.Add(this.rdo본문방식2);
			this.flexibleRoundedCornerBox1.Controls.Add(this.rdo본문방식1);
			this.flexibleRoundedCornerBox1.Controls.Add(this.labelExt2);
			this.flexibleRoundedCornerBox1.Controls.Add(this.btn보내기);
			this.flexibleRoundedCornerBox1.Controls.Add(this.labelExt1);
			this.flexibleRoundedCornerBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flexibleRoundedCornerBox1.Location = new System.Drawing.Point(6, 6);
			this.flexibleRoundedCornerBox1.Name = "flexibleRoundedCornerBox1";
			this.flexibleRoundedCornerBox1.Size = new System.Drawing.Size(411, 108);
			this.flexibleRoundedCornerBox1.TabIndex = 1;
			// 
			// rdo본문방식2
			// 
			this.rdo본문방식2.Location = new System.Drawing.Point(114, 41);
			this.rdo본문방식2.Name = "rdo본문방식2";
			this.rdo본문방식2.Size = new System.Drawing.Size(131, 24);
			this.rdo본문방식2.TabIndex = 14;
			this.rdo본문방식2.TabStop = true;
			this.rdo본문방식2.Text = "상용구 + 메일회신";
			this.rdo본문방식2.TextDD = null;
			this.rdo본문방식2.UseKeyEnter = true;
			this.rdo본문방식2.UseVisualStyleBackColor = true;
			// 
			// rdo본문방식1
			// 
			this.rdo본문방식1.Checked = true;
			this.rdo본문방식1.Location = new System.Drawing.Point(30, 41);
			this.rdo본문방식1.Name = "rdo본문방식1";
			this.rdo본문방식1.Size = new System.Drawing.Size(75, 24);
			this.rdo본문방식1.TabIndex = 13;
			this.rdo본문방식1.TabStop = true;
			this.rdo본문방식1.Text = "상용구";
			this.rdo본문방식1.TextDD = null;
			this.rdo본문방식1.UseKeyEnter = true;
			this.rdo본문방식1.UseVisualStyleBackColor = true;
			// 
			// labelExt2
			// 
			this.labelExt2.Location = new System.Drawing.Point(28, 20);
			this.labelExt2.Name = "labelExt2";
			this.labelExt2.Size = new System.Drawing.Size(196, 14);
			this.labelExt2.TabIndex = 12;
			this.labelExt2.Text = "메일 본문 방식을 선택하세요";
			// 
			// btn보내기
			// 
			this.btn보내기.BackColor = System.Drawing.Color.White;
			this.btn보내기.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn보내기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn보내기.Location = new System.Drawing.Point(251, 44);
			this.btn보내기.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn보내기.Name = "btn보내기";
			this.btn보내기.Size = new System.Drawing.Size(70, 19);
			this.btn보내기.TabIndex = 11;
			this.btn보내기.TabStop = false;
			this.btn보내기.Text = "보내기";
			this.btn보내기.UseVisualStyleBackColor = false;
			// 
			// labelExt1
			// 
			this.labelExt1.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.labelExt1.ForeColor = System.Drawing.Color.Red;
			this.labelExt1.Location = new System.Drawing.Point(26, 76);
			this.labelExt1.Name = "labelExt1";
			this.labelExt1.Size = new System.Drawing.Size(380, 14);
			this.labelExt1.TabIndex = 0;
			this.labelExt1.Text = "주의! 메일회신은 원본메일이 존재해야 사용 가능합니다.";
			// 
			// H_CZ_MAIL_OPTION
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(423, 120);
			this.Controls.Add(this.flexibleRoundedCornerBox1);
			this.Name = "H_CZ_MAIL_OPTION";
			this.Padding = new System.Windows.Forms.Padding(6);
			this.Text = "H_CZ_MAIL_OPTION";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.flexibleRoundedCornerBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.rdo본문방식2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo본문방식1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Duzon.Common.Controls.FlexibleRoundedCornerBox flexibleRoundedCornerBox1;
		private Duzon.Common.Controls.LabelExt labelExt2;
		private Duzon.Common.Controls.RoundedButton btn보내기;
		private Duzon.Common.Controls.LabelExt labelExt1;
		private Duzon.Common.Controls.RadioButtonExt rdo본문방식2;
		private Duzon.Common.Controls.RadioButtonExt rdo본문방식1;
	}
}
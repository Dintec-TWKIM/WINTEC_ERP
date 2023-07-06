
using Duzon.Common.Controls;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
	partial class P_FI_Z_SEAHST_NOTICE_SUB
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
			this.textBoxExt1 = new Duzon.Common.Controls.TextBoxExt();
			this.btn확인 = new Duzon.Common.Controls.RoundedButton(this.components);
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.SuspendLayout();
			// 
			// textBoxExt1
			// 
			this.textBoxExt1.BorderColor = System.Drawing.Color.LightSteelBlue;
			this.textBoxExt1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxExt1.Location = new System.Drawing.Point(12, 12);
			this.textBoxExt1.Multiline = true;
			this.textBoxExt1.Name = "textBoxExt1";
			this.textBoxExt1.ReadOnly = true;
			this.textBoxExt1.Size = new System.Drawing.Size(390, 190);
			this.textBoxExt1.TabIndex = 0;
			this.textBoxExt1.TabStop = false;
			this.textBoxExt1.Text = "\r\n<★★ 아래의 유형에 대해서 부가가치세 체크 금지★★>\r\n\r\n\r\n1. 택시요금, KTX, 고속버스, 항공권, 골프, 일반 및 등기 우편\r\n\r\n" +
    "\r\n2. 간이과세자 및 면세사업자 매입\r\n\r\n\r\n3. 접대비, 직원선물, 소형승용차 관련(대리운전, 유류비 등) 매입\r\n\r\n\r\n4. 상품권 매입" +
    ", 도서 구입\r\n\r\n\r\n\r\n";
			// 
			// btn확인
			// 
			this.btn확인.BackColor = System.Drawing.Color.Transparent;
			this.btn확인.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn확인.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn확인.Location = new System.Drawing.Point(172, 208);
			this.btn확인.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn확인.Name = "btn확인";
			this.btn확인.Size = new System.Drawing.Size(70, 19);
			this.btn확인.TabIndex = 1;
			this.btn확인.TabStop = false;
			this.btn확인.Text = "확인";
			this.btn확인.UseVisualStyleBackColor = false;
			// 
			// P_FI_Z_SEAHST_NOTICE_SUB
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.ClientSize = new System.Drawing.Size(414, 239);
			this.Controls.Add(this.btn확인);
			this.Controls.Add(this.textBoxExt1);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(420, 270);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(420, 270);
			this.Name = "P_FI_Z_SEAHST_NOTICE_SUB";
			this.Text = "더존 ERP-iU";
			this.TitleText = "ERP-iU";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

		private TextBoxExt textBoxExt1;
		private RoundedButton btn확인;

		#endregion
	}
}
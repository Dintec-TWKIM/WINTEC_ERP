
namespace SMSTest
{
	partial class Form1
	{
		/// <summary>
		/// 필수 디자이너 변수입니다.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 사용 중인 모든 리소스를 정리합니다.
		/// </summary>
		/// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form 디자이너에서 생성한 코드

		/// <summary>
		/// 디자이너 지원에 필요한 메서드입니다. 
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
		/// </summary>
		private void InitializeComponent()
		{
			this.btn문자보내기 = new System.Windows.Forms.Button();
			this.btn이미지전송 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btn문자보내기
			// 
			this.btn문자보내기.Location = new System.Drawing.Point(22, 22);
			this.btn문자보내기.Name = "btn문자보내기";
			this.btn문자보내기.Size = new System.Drawing.Size(75, 23);
			this.btn문자보내기.TabIndex = 0;
			this.btn문자보내기.Text = "문자보내기";
			this.btn문자보내기.UseVisualStyleBackColor = true;
			// 
			// btn이미지전송
			// 
			this.btn이미지전송.Location = new System.Drawing.Point(103, 22);
			this.btn이미지전송.Name = "btn이미지전송";
			this.btn이미지전송.Size = new System.Drawing.Size(75, 23);
			this.btn이미지전송.TabIndex = 1;
			this.btn이미지전송.Text = "이미지전송";
			this.btn이미지전송.UseVisualStyleBackColor = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.btn이미지전송);
			this.Controls.Add(this.btn문자보내기);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btn문자보내기;
		private System.Windows.Forms.Button btn이미지전송;
	}
}


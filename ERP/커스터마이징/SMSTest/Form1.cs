using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SMSTest
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

			this.btn문자보내기.Click += Btn문자보내기_Click;
			this.btn이미지전송.Click += Btn이미지전송_Click;
		}

		private void Btn이미지전송_Click(object sender, EventArgs e)
		{
			MessagingLib.Response imgResp = MessagingLib.UploadImage(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "//img.jpg");
			string imageId = imgResp.Data.SelectToken("fileId").ToString();

			Console.WriteLine(imageId);

			MessagingLib.Messages messages = new MessagingLib.Messages();

			messages.Add(new MessagingLib.Message()
			{
				to = "01098098088",
				from = "01098098088",
				imageId = imageId,
				subject = "MMS 제목",
				text = "이미지 아이디가 입력되면 MMS로 발송됩니다."
			});

			MessagingLib.Response response = MessagingLib.SendMessages(messages);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				Console.WriteLine("전송 결과");
				Console.WriteLine("Group ID:" + response.Data.SelectToken("groupId").ToString());
				Console.WriteLine("Status:" + response.Data.SelectToken("status").ToString());
				Console.WriteLine("Count:" + response.Data.SelectToken("count").ToString());
			}
			else
			{
				Console.WriteLine("Error Code:" + response.ErrorCode);
				Console.WriteLine("Error Message:" + response.ErrorMessage);
			}
		}

		private void Btn문자보내기_Click(object sender, EventArgs e)
		{
			MessagingLib.Messages messages = new MessagingLib.Messages();
			messages.Add(new MessagingLib.Message()
			{
				to = "01098098088",
				from = "01098098088",
				text = "문자 테스트"
			});

			MessagingLib.Response response = MessagingLib.SendMessages(messages);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				Console.WriteLine("전송 결과");
				Console.WriteLine("Group ID:" + response.Data.SelectToken("groupId").ToString());
				Console.WriteLine("Status:" + response.Data.SelectToken("status").ToString());
				Console.WriteLine("Count:" + response.Data.SelectToken("count").ToString());
			}
			else
			{
				Console.WriteLine("Error Code:" + response.ErrorCode);
				Console.WriteLine("Error Message:" + response.ErrorMessage);
			}
		}
	}
}

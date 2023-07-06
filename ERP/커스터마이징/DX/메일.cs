using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using Aspose.Email.Outlook;

namespace DX
{
	public class 메일
	{
		MapiMessage msg;
		메일첨부파일[] files;

		public 메일첨부파일[] 첨부파일 => files;

		public string 발신자 => msg.SenderEmailAddress;

		public string 내용 => msg.Body;

		public 메일(string 파일)
		{
			// 원본 메세지
			msg = MapiMessage.FromFile(파일);

			// 첨부파일
			files = new 메일첨부파일[msg.Attachments.Count];
			for (int i = 0; i < msg.Attachments.Count; i++)
				files[i] = new 메일첨부파일(msg.Attachments[i]);
		}

		public static bool 주소검사(string 메일주소)
		{
			try
			{
				MailAddress m = new MailAddress(메일주소);
				return true;
			}
			catch (FormatException)
			{
				return false;
			}
		}
	}


	public class 메일첨부파일
	{
		MapiAttachment file;

		public string 파일이름 => file.LongFileName;

		public string 확장자 => 파일.확장자(파일이름);    // 가끔 확장자가 null 떠서 Mapi꺼 못씀

		public 메일첨부파일(MapiAttachment file)
		{
			this.file = file;
		}

		public string 저장()
		{			
			return 저장(경로.임시() + @"\" + 파일.파일이름(파일이름));
		}

		public string 저장(string 파일이름)
		{						
			string 파일이름_신규 = 경로.경로만(파일이름) + @"\" + 파일.파일이름_고유(파일이름);
			file.Save(파일이름_신규);

			return 파일이름_신규;
		}
	}
}

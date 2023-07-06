using Aspose.Email.Outlook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DX
{
	public class 아웃룩
	{
		readonly MapiMessage msg;
		readonly 아웃룩첨부파일[] files;

		public 아웃룩첨부파일[] 첨부파일 => files;

		public string 발신자 => msg.SenderEmailAddress;

		public 아웃룩(string 파일)
		{
			// 원본 메세지
			msg = MapiMessage.FromFile(파일);

			// 첨부파일
			files = new 아웃룩첨부파일[msg.Attachments.Count];
			for (int i =0; i < msg.Attachments.Count; i++)
				files[i] = new 아웃룩첨부파일(msg.Attachments[i]);
		}
	}

	public class 아웃룩첨부파일
	{
		readonly MapiAttachment file;

		public string 이름 => file.LongFileName;

		public string 확장자 => 파일.확장자(이름);	// 가끔 확장자가 null 떠서 Mapi꺼 못씀

		public 아웃룩첨부파일(MapiAttachment file)
		{
			this.file = file;
		}

		public void 저장(string 파일이름)
		{
			file.Save(파일이름);
		}
	}
}

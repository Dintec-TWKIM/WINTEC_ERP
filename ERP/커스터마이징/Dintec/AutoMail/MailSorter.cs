using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Aspose.Email.Outlook;

namespace Dintec.AutoMail
{
    public class MailSorter
    {
        string _fileName = "";

        public string BuyerCode
        {
            get; set;
        }
        
        public string RefNumber
        {
            get; set;
        }

        public string WebLink
        {
            get; set;
        }

        public MailSorter(string fileName)
        {
			_fileName = fileName;

			BuyerCode = "";
			RefNumber = "";
			WebLink = "";			
        }

        public bool Sort()
        {
			if (Path.GetExtension(_fileName).ToLower() != ".msg")
				return false;

			MapiMessage message = MapiMessage.FromFile(_fileName);

            // 메일
            string subject = message.Subject;            
            string body = message.Body;
            string bodyHtml = message.BodyHtml;
            string sender = message.SentRepresentingEmailAddress;

            // 현대글로비스(지마린서비스)
            if (sender == "ehcho@gmarineservice.com" && subject.Substring(0, 16) == "Subject : RFQ No")
            {
                BuyerCode = "11845";
                RefNumber = subject.Substring(17, 8);
            }
            // SHIP PROCUREMENT SERVICES S.A.
            else if (sender == "noreply@mail.procureship.com" && subject.Substring(0, 21) == "New RFQ from SPS S.A.")
            {
                BuyerCode = "11397";
                RefNumber = subject.Substring(subject.LastIndexOf("|") + 1).Trim();
                
                string temp = bodyHtml.Substring(0, bodyHtml.IndexOf("View RFQ"));
                int linkStart = temp.LastIndexOf("href=") + 6;
                int linkEnd = temp.IndexOf(">", linkStart);
                WebLink = temp.Substring(linkStart, linkEnd - linkStart - 1);
            }
            else if (sender == "orders@seaproc.com" && subject.Substring(0, 29) == "SeaProc Request For Quotation")
            {
                // SCORPIO
                BuyerCode = "07304";
                //RefNumber = subject.Substring(17, 8);

                int linkStart = body.IndexOf("<https://seaproc.com/Seller/Dashboard.aspx?dvid=");
                int linkEnd = body.IndexOf(">", linkStart + 1);
                WebLink = body.Substring(linkStart + 1, linkEnd - linkStart - 1).Replace("\r\n", "");
            }

            if (BuyerCode != "")
                return true;
            else
                return false;
        }
    }
}

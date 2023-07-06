using Aspose.Email.Outlook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace cz
{
	class MailToSearch
	{
		public string mailToSearch(MapiMessage msg, Outlook.MailItem mailitem, string MAIL_TO)
		{
			if (!string.IsNullOrEmpty(msg.Headers["To"]))              // 받는사람
			{
				string mail_to = msg.Headers["To"];
				int idx_s = -1;

				if (mail_to.Contains("<") && mail_to.Contains(">"))
				{
					string[] mailToSpl = mail_to.Split(',');

					if (mailToSpl.Length > 0)
					{
						for (int c = 0; c < mailToSpl.Length; c++)
						{
							idx_s = mailToSpl[c].IndexOf("<");
							if (idx_s != -1)
							{
								MAIL_TO += mailToSpl[c].Substring(idx_s, mailToSpl[c].Length - idx_s).Replace("<", "").Replace(">", "").Trim() + ";";
							}
							else
							{
								MAIL_TO += mailToSpl[c] + ";";
							}
						}
					}
				}
				else
				{
					MAIL_TO = mail_to;
				}

				if (mailitem != null)
				{
					if (!string.IsNullOrEmpty(mailitem.To) && mailitem.To != null && string.IsNullOrEmpty(MAIL_TO))
						MAIL_TO = mailitem.To.ToString().Replace("'", "");
				}
			}
			else
			{
				if (mailitem != null)
				{
					if (!string.IsNullOrEmpty(mailitem.To) && mailitem.To != null)
						MAIL_TO = mailitem.To.ToString().Replace("'", "");
				}
			}

			MAIL_TO = MAIL_TO.Replace("'", "").Replace(",", ";").Trim();


			return MAIL_TO;
		}


		public string mailCCSearch(MapiMessage msg, Outlook.MailItem mailitem, string MAIL_CC)
		{
			if (!string.IsNullOrEmpty(msg.Headers["Cc"]))
			{
				string mail_cc = msg.Headers["Cc"];
				int idx_s = -1;

				if (mail_cc.Contains("<") && mail_cc.Contains(">"))
				{
					string[] mailCcSpl = mail_cc.Split(',');

					if (mailCcSpl.Length > 0)
					{
						for (int c = 0; c < mailCcSpl.Length; c++)
						{
							idx_s = mailCcSpl[c].IndexOf("<");
							if (idx_s != -1)
							{
								MAIL_CC += mailCcSpl[c].Substring(idx_s, mailCcSpl[c].Length - idx_s).Replace("<", "").Replace(">", "").Trim() + ";";
								idx_s = -1;
							}
							else
							{
								MAIL_CC += mailCcSpl[c] + ";";
							}
						}
					}
				}
				else
				{
					MAIL_CC = mail_cc;
				}

				if (mailitem != null)
				{
					if (!string.IsNullOrEmpty(mailitem.CC) && mailitem.CC != null && string.IsNullOrEmpty(MAIL_CC))
						MAIL_CC = mailitem.CC.ToString();                                        // 참조
				}
			}
			else
			{
				if (mailitem != null)
				{
					if (!string.IsNullOrEmpty(mailitem.CC) && mailitem.CC != null)
						MAIL_CC = mailitem.CC.ToString();                                        // 참조
				}
			}


			MAIL_CC = MAIL_CC.Replace("'", "").Replace(",", ";").Trim();

			return MAIL_CC;
		}

		public string mailToCcSearch(MapiMessage msg, Outlook.MailItem mailitem, string MAIL_CC, string MAIL_TO, string MAIL_BCC, string CD_COMPANY, string MAIL_FROM_DB_S)
		{
			string[] mailToSplSt = MAIL_TO.Split(';');
			string reMail_to = string.Empty;
			if (mailToSplSt.Length > 0)
			{

				for (int c = 0; c < mailToSplSt.Length; c++)
				{
					if (mailToSplSt[c].ToString().ToLower().Contains("@dintec.co.kr") || mailToSplSt[c].ToString().ToLower().Contains("@dubheco.com"))
					{
						reMail_to += mailToSplSt[c].ToString().Trim() + ";";
					}
				}
			}

			string[] mailCcSplSt = MAIL_CC.Split(';');
			string reMail_cc = string.Empty;
			if (mailCcSplSt.Length > 0)
			{

				for (int c = 0; c < mailCcSplSt.Length; c++)
				{
					if (mailCcSplSt[c].ToString().ToLower().Contains("@dintec.co.kr") || mailCcSplSt[c].ToString().ToLower().Contains("@dubheco.com"))
					{
						reMail_cc += mailCcSplSt[c].ToString().Trim() + ";";
					}
				}
			}

			if (!string.IsNullOrEmpty(reMail_to))
				MAIL_TO = reMail_to.ToLower().Trim();

			if (!string.IsNullOrEmpty(reMail_cc))
				MAIL_CC = reMail_cc.ToLower().Trim();


			if (MAIL_TO.EndsWith(";"))
				MAIL_TO = MAIL_TO.Substring(0, MAIL_TO.Length - 1).Trim();

			if (MAIL_CC.EndsWith(";"))
				MAIL_CC = MAIL_CC.Substring(0, MAIL_CC.Length - 1).Trim();

			if (mailitem != null)
			{
				if (!string.IsNullOrEmpty(mailitem.BCC) && mailitem.BCC != null)
					MAIL_BCC = mailitem.BCC.ToString();                                   // 숨은참조
			}


			if (MAIL_TO.ToLower().Contains("@dintec.co.kr") || MAIL_CC.ToLower().Contains("@dintec.co.kr"))
				CD_COMPANY = "K100";
			else if (MAIL_TO.ToLower().Contains("@dubheco.com") || MAIL_CC.ToLower().Contains("@dubheco.com"))
				CD_COMPANY = "K200";
			else
				CD_COMPANY = "K100";

			// 숨은 참조 수정 사항
			if (!MAIL_TO.ToLower().Contains("@dintec.co.kr") && !MAIL_TO.ToLower().Contains("@dubheco.com") &&
				!MAIL_CC.ToLower().Contains("@dintec.co.kr") && !MAIL_CC.ToLower().Contains("@dubheco.com"))
			{
				string _bcc = msg.Headers["Received"];

				int idx_s = _bcc.IndexOf("<");
				int idx_e = _bcc.IndexOf(">");

				if (idx_s != -1 && idx_e != -1)
					MAIL_TO = _bcc.Substring(idx_s, idx_e - idx_s).Replace("<", "").Replace(">", "");
			}


			if (MAIL_CC.Contains(MAIL_FROM_DB_S.Replace(";", "")))
			{
				MAIL_CC = MAIL_CC.Replace(MAIL_FROM_DB_S.Replace(";", ""), "");
			}


			return MAIL_CC;
		}
	}
}

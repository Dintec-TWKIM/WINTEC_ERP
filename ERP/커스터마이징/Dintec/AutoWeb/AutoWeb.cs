using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

using Aspose.Email.Outlook;
using Dintec.AutoMail;
using Duzon.Common.Forms;



namespace Dintec.AutoWeb
{
    public class AutoWeb
    {
        private string _outlookFile = "";

		public string CompanyCode
		{
			get
			{
				return Global.MainFrame.LoginInfo.CompanyCode;
			}
		}

        public string BuyerCode
        {
            get; set;
        }

		public string FileNumber
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

        public string ErrorMessage
        {
            get; set;
        }

        public string OutlookFile
        {
            get
            {
                return _outlookFile;
            }
            set
            {
                _outlookFile = value;

                MailSorter ms = new MailSorter(_outlookFile);
                ms.Sort();

                BuyerCode = ms.BuyerCode;
                RefNumber = ms.RefNumber;
                WebLink = ms.WebLink;                
            }
        }

        public string DownloadInquiry(bool run)
        {
            // 중복파일 체크
            string query = @"
SELECT
*
FROM CZ_SA_QTNH WITH(NOLOCK)
WHERE 1 = 1
    AND CD_COMPANY = '" + CompanyCode + @"'
    AND NO_REF = '" + RefNumber + "'";

            DataTable dt = DBMgr.GetDataTable(query);

            //if (dt.Rows.Count > 0)
            //{
            //    ErrorMessage = "중복파일 입니다.";
            //    return "";
            //}

            // 임시폴더 생성
            string inqPath = Application.StartupPath + @"\temp\";
            string inqFile = "";
            FileMgr.CreateDirectory(inqPath);

			if (BuyerCode == "11845")
			{
				Glovis o = new Glovis();
				o.RefNumber = RefNumber;
				inqFile = o.DownloadInquiry();
				ErrorMessage = o.ErrorMessage;
			}
			else if (BuyerCode == "11397")
			{
				Sps o = new Sps();
				o.RefNumber = RefNumber;
				o.WebLink = WebLink;
				inqFile = o.DownloadInquiry();
				ErrorMessage = o.ErrorMessage;
			}

			if (run)
				Process.Start(inqFile);

            return inqFile; 
        }

        public string DownloadInquiryWithMessage(bool run)
        {
			string inqFile = DownloadInquiry(false);
			string newFile = Application.StartupPath + @"\temp\" + Path.GetFileNameWithoutExtension(_outlookFile) + "_Added" + Path.GetExtension(_outlookFile);

			if (inqFile == "")
				return "";

            // 메일에 해당 견적서 첨부
			MapiMessage message = MapiMessage.FromFile(_outlookFile);				
			message.Attachments.Add(Path.GetFileName(inqFile), File.ReadAllBytes(inqFile));
			message.Save(newFile);

			if (run)
				Process.Start(newFile);

			return newFile;
        }

        public void Submit(DataTable dtHead, DataTable dtLine)
        {            
            if (BuyerCode == "11845")
            {
                Glovis o = new Glovis();
                o.RefNumber = RefNumber;
                o.Submit(dtHead, dtLine);
            }
			else if (BuyerCode == "11397")
			{
				// 견적 아웃룩 파일 다운로드
				string query = @"
SELECT
	*
FROM CZ_MA_WORKFLOWL WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + CompanyCode + @"'
	AND NO_KEY = '" + FileNumber + @"'
	AND TP_STEP = '01'
	AND RIGHT(NM_FILE, 4) = '.msg'
ORDER BY NO_LINE DESC";

				DataTable dt = DBMgr.GetDataTable(query);

				if (dt.Rows.Count > 0)
				{
					string path = Application.StartupPath + @"\temp\";

					OutlookFile = path + FileMgr.Download_WF(CompanyCode, FileNumber, (string)dt.Rows[0]["NM_FILE_REAL"], false);

					MapiMessage message = MapiMessage.FromFile(OutlookFile);
					string excelFile = "";

					// 엑셀 파일 찾기
					foreach (MapiAttachment file in message.Attachments)
					{
						if (file.Extension.ToLower() == ".xlsx")
						{
							excelFile = path + message.Attachments[0].LongFileName;
							file.Save(excelFile);
							break;
						}
					}

					Sps o = new Sps();
					o.WebLink = WebLink;
					o.ExcelFileName = excelFile;
					o.Submit(dtHead, dtLine);
				}
			}
		}
    }
}

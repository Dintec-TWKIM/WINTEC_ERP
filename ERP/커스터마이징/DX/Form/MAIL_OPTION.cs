using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Aspose.Email.Outlook;
using Dintec;
using cz;

namespace DX
{
	public partial class MAIL_OPTION : Duzon.Common.Forms.CommonDialog
	{
		public string 회사코드 { get; set; } = "";
		//public string 원래회사코드 { get; set; }
		public string 파일번호 { get; set; } = "";
		public string 거래처코드 { get; set; } = "";

		public string 보낸사람 { get; set; } = "";
		public string 받는사람 { get; set; } = "";
		public string 참조 { get; set; } = "";
		public string 숨은참조 { get; set; } = "";		

		public string 제목 { get; set; } = "";
		public string 서명 { get; set; } = "";
		public List<string> 첨부파일 { get; set; } = new List<string>();
				
		readonly MapiMessage 답장대상메일;

		#region ==================================================================================================== Constructor
		
		public MAIL_OPTION(string 회사코드, string 파일번호, MAIL_TYPE 메일종류)
		{			
			InitializeComponent();

			this.페이지초기화();
			this.회사코드 = 회사코드;
			this.파일번호 = 파일번호;

			// ********** 인터컴퍼니 체크
			string 원래회사코드 = 회사코드;
			string query = @"
SELECT
	C.CD_COMPANY
,	C.NO_EMP
,	D.NO_EMAIL	-- 현재 담당자 이메일 (인터컴퍼니 된 담당자 말고 실제 담당자)
FROM CZ_SA_QTNH			AS A
JOIN CZ_SA_INTERCOMPANY	AS B ON A.CD_COMPANY = B.CD_COMPANY AND LEFT(A.NO_FILE, 2) = B.CD_PREFIX
JOIN CZ_SA_QTNH			AS C ON B.CD_COMPANY_SO = C.CD_COMPANY AND A.NO_FILE = C.NO_FILE
JOIN MA_EMP				AS D ON A.CD_COMPANY = D.CD_COMPANY AND A.NO_EMP = D.NO_EMP
WHERE 1 = 1
	AND A.CD_COMPANY = '" + 회사코드 + @"'
	AND A.NO_FILE = '" + 파일번호 + "'";

			DataTable dtIC = SQL.GetDataTable(query);

			if (dtIC.Rows.Count > 0)
			{
				// 인터컴퍼니 회사, 담당자 변경
				회사코드 = dtIC.Rows[0]["CD_COMPANY"].ToString();
				숨은참조 = dtIC.Rows[0]["NO_EMAIL"].ToString();	// 인터컴퍼니 경우 현재 담당자가 숨은참조가 됨
			}

			// ********** 이것저것 가져오기
			DataTable dt = SQL.GetDataTable("PS_CZ_DX_MAIL_OPTION", 회사코드, 파일번호, 메일종류);
			거래처코드 = dt.Rows[0]["CD_PARTNER"].ToString();
			보낸사람 = dt.Rows[0]["FROM"].ToString();
			참조 = dt.Rows[0]["CC"].ToString();

			if (메일종류 == MAIL_TYPE.Quotation)
				받는사람 = dt.Rows[0]["TO"].ToString();
			else if (메일종류 == MAIL_TYPE.Acknowledge)
				받는사람 = 답장대상메일.SenderEmailAddress;

			제목 = dt.Rows[0]["TITLE"].ToString();
			서명 = dt.Rows[0]["SIGN_TEXT"].ToString();

			// ********** 회신 메일
			// 답장 해야할 워크플로우 단계
			string replyStep = "";
			if (메일종류 == MAIL_TYPE.Quotation)		replyStep = "01";
			if (메일종류 == MAIL_TYPE.Acknowledge)	replyStep = "08";

			// 답장 대상 메일 가져오기
			query = @"
SELECT
	*
FROM
(
	SELECT
		NM_FILE_REAL
	,	RN = ROW_NUMBER() OVER (ORDER BY NO_LINE DESC)
	FROM CZ_MA_WORKFLOWL WITH(NOLOCK)
	WHERE 1 = 1
		AND CD_COMPANY = '" + 원래회사코드 + @"'
		AND NO_KEY = '" + 파일번호 + @"'
		AND TP_STEP = '" + replyStep + @"'
		AND RIGHT(NM_FILE_REAL, 3) = 'MSG'
) AS A
WHERE RN = 1";

			DataTable dtMsg = SQL.GetDataTable(query);

			if (dtMsg.Rows.Count > 0)
			{
				string fileName = FileMgr.Download_WF(원래회사코드, 파일번호, dtMsg.Rows[0][0].ToString(), false);
				답장대상메일 = MapiMessage.FromFile(Application.StartupPath + @"\temp\" + fileName);
			}			

			// ********** 첨부파일
			// 타입에 따라 단계 설정
			string attachedStep = "";
			if (메일종류 == MAIL_TYPE.Quotation)		attachedStep = "'05','58'";
			if (메일종류 == MAIL_TYPE.Acknowledge)	attachedStep = "'09'";

			query = @"
SELECT
	DC_FILE	= NM_FILE
,	NM_FILE	= NM_FILE_REAL
,	FILE_DATA
FROM FT_CZ_MA_WORKFLOWL('" + 원래회사코드 + "', '" + 파일번호 + @"')
WHERE 1 = 1
	AND TP_STEP IN (" + attachedStep + @")
	AND RN = 1";

			DataTable dtFile = SQL.GetDataTable(query);

			foreach (DataRow row in dtFile.Rows)
			{
				string fileDisp = row["DC_FILE"].ToString();
				string fileName = row["NM_FILE"].ToString();
				var fileData = row["FILE_DATA"];

				if (fileName != "")
					첨부파일.Add(fileDisp + "|" + fileName);
				else if (fileData.ToString() != "")
					첨부파일.Add(fileDisp + "|BINARY" + Convert.ToBase64String((byte[])fileData));
			}

			// ********** Cert 및 Letter 첨부
			if (원래회사코드 == "K100" && 메일종류 == MAIL_TYPE.Quotation)
			{			
				query = @"
SELECT
	'Upload\' + ID_MENU + '\' + CD_FILE + '\' + FILE_NAME
FROM MA_FILEINFO AS A WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + 회사코드 + @"'
	AND CD_MODULE = 'MA'
	AND ID_MENU = 'P_CZ_MA_PARTNER'
	AND CD_FILE IN (SELECT CD_SUPPLIER FROM CZ_SA_QTNL WHERE CD_COMPANY = A.CD_COMPANY AND NO_FILE = '" + 파일번호 + @"')
	AND FILE_NAME LIKE 'CERT%.PDF'";

				DataTable dtCert = SQL.GetDataTable(query);
				for (int i = 0; i < dtCert.Rows.Count; i++)
					첨부파일.Add(dtCert.Rows[i][0].ToString());
			}

			// 디폴트 설정
			if (답장대상메일 == null)
			{
				panelExt21.사용(false);
				panelExt1.사용(false);
			}
			else
			{
				rdo보낸사람.Checked = true;
				rdo메일회신.Checked = true;
			}

			lbl참조.MinimumSize = new Size(368, 0);
			Rdo본문_CheckedChanged(null, null);
			ChkCC포함_CheckedChanged(null, null);			
		}

		#endregion

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{
			InitEvent();

			// 라이선스 인증
			Aspose.Email.License license = new Aspose.Email.License();
			license.SetLicense("Aspose.Email.lic");
		}

		private void InitEvent()
		{
			btn보내기.Click += Btn보내기_Click;

			rdo견적담당.CheckedChanged += Rdo본문_CheckedChanged;
			rdo보낸사람.CheckedChanged += Rdo본문_CheckedChanged;
			
			chkCC포함.CheckedChanged += ChkCC포함_CheckedChanged;
		}
		
		private void Rdo본문_CheckedChanged(object sender, EventArgs e)
		{
			chkCC포함.Checked = false;
			chkCC포함.사용(rdo보낸사람.Checked);

			// 받는사람
			lbl받는사람.Text = rdo견적담당.Checked ? 받는사람 : 답장대상메일.SenderEmailAddress;
		}

		private void ChkCC포함_CheckedChanged(object sender, EventArgs e)
		{
			lbl참조.AutoEllipsis = true;
			// 디폴트 참조 넣음 (본인)
			lbl참조.Text = 참조;

			// 참조 선택에 따라 결정
			if (chkCC포함.Checked && 답장대상메일.DisplayCc != "")
				lbl참조.Text = 답장대상메일.DisplayCc + "; " + 참조;
		}

		#endregion

		#region ==================================================================================================== Event

		private void Btn보내기_Click(object sender, EventArgs e)
		{
			받는사람 = lbl받는사람.Text;
			참조 = lbl참조.Text;
			
			// 시작
			string html = "";

			if (rdo메일회신.Checked)
			{
				// 작성메일 제목
				제목 = "RE: " + 답장대상메일.Subject + " (" + 파일번호 + ")";

				// 원본메일 본문
				string oBody = 답장대상메일.BodyHtml;

				// 원본메일 보내는 사람
				string oFrom = 답장대상메일.SenderName + "&lt;" + 답장대상메일.SenderEmailAddress + "&gt;";

				// 원본메일 받는사람, 참조
				string oTo = "";
				string oCc = "";

				foreach (MapiRecipient r in 답장대상메일.Recipients)
				{
					if (r.RecipientType == MapiRecipientType.MAPI_TO)
					{
						if (r.DisplayName == r.EmailAddress)
							oTo += "; " + r.DisplayName;
						else
							oTo += "; " + r.DisplayName + "&lt;" + r.EmailAddress + "&gt;";
					}
					else if (r.RecipientType == MapiRecipientType.MAPI_CC)
					{
						if (r.DisplayName == r.EmailAddress)
							oCc += "; " + r.DisplayName;
						else
							oCc += "; " + r.DisplayName + "&lt;" + r.EmailAddress + "&gt;";
					}
				}

				if (oTo != "")
					oTo = oTo.Substring(2);

				if (oCc != "")
					oCc = oCc.Substring(2);

				// 원본메일 발송시간
				string oSent = 답장대상메일.DeliveryTime.ToString();

				// 원본메일 제목
				string oSubject = 답장대상메일.Subject;

				// Html 만들기
				html = @"
<br /><br />
<div>
-----Original Message-----<br />
<strong>From:</strong> " + oFrom + @"<br />
<strong>To:</strong> " + oTo + @"<br />
<strong>Cc:</strong> " + oCc + @"<br />
<strong>Sent:</strong> " + oSent + @"<br />
<strong>Subject:</strong> " + oSubject + @"<br />
</div>
<br /><br />" + oBody;

			}

			// 메일발송 팝업
			P_CZ_MA_EMAIL_SUB f = new P_CZ_MA_EMAIL_SUB(보낸사람, 받는사람, 참조, 숨은참조, 제목, 첨부파일.ToArray(), null, 서명, html, 파일번호, 거래처코드, false);
			f.ShowDialog();
			DialogResult = f.DialogResult;
		}

		#endregion
	}

	public enum MAIL_TYPE
	{
		Quotation
	,	Acknowledge
	,	PurchaseOrder
	,	Inquiry
	}
}

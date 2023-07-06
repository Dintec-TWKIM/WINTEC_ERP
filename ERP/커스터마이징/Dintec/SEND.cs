using cz;
using Duzon.Common.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dintec
{

	public class SEND
	{
		public object SendedDate
		{
			get; set;
		}

		public bool ChangedAddress
		{
			get; set;
		}

		public string ErrorMessage
		{
			get; set;
		}

		public bool IsTest
		{
			get; set;
		}


		public bool InquiryMail(string companyCode, string fileNumber, string partnerCode, bool boAuto)
		{
			DataTable dtHead = SQL.GetDataTable("PS_CZ_PU_INQ_RPT_H", companyCode, fileNumber, partnerCode);
			DataTable dtLine = SQL.GetDataTable("PS_CZ_PU_INQ_RPT_L", companyCode, fileNumber, partnerCode);
			DataRow headRow = dtHead.Rows[0];

			// ********** 경고마스터 확인
			// 업데이트 모드로 변경 (그래야 경고마스터에서 인식함)
			foreach (DataRow row in dtLine.Rows)
				row.SetModified();

			Warning warning = new Warning(WarningFlag.QTN);
			warning.WarningCode = "007";
			warning.FileCode = fileNumber.Left(2);
			warning.BuyerCode = headRow["CD_BUYER"].ToString();
			warning.ImoNumber = headRow["NO_IMO"].ToString();
			warning.SupplierCode = partnerCode;
			warning.Item = dtLine;
			warning.Check();

			if (warning.Count > 0)
			{
				ErrorMessage = warning.Message;
				return false;
			}

			// ********** 필수값 체크
			if (headRow["EN_EMP"].ToString() == "")
			{
				ErrorMessage = "영문 이름이 설정되어 있지 않습니다.";
				return false;
			}

			// ********** 서광산전 제목/내용 아무것도 안넣기 (해킹업체)
			bool isHack = false;

            if (partnerCode.In("08012"))
                isHack = true;

			// 딘텍 담당자 정보
			string empNameKor = (string)headRow["NM_EMP"];
			string empNameEng = (string)headRow["EN_EMP"];
			string empTelNumber = (string)headRow["NO_TEL_EMP"];			
			string empMail = (string)headRow["NO_EMAIL_EMP"];

			// 메일 주소
			string from = fileNumber.Left(2).In("FB", "DS", "TB") ? "service@dintec.co.kr" : empMail;
			string to = headRow["E_MAIL_PARTNER"].ToString();
			string cc = empMail;

			// 테스트 상태에 따라 로그인 사용자로 메일 발송
			string query = "SELECT CD_FLAG1 FROM V_CZ_MA_CODEDTL WHERE CD_COMPANY = 'K100' AND CD_FIELD = 'CZ_DX00001' AND CD_SYSDEF = '001'";
			DataTable dtOption = SQL.GetDataTable(query);

			if (dtOption.Rows[0][0].ToString() == "Y")
				to = Global.MainFrame.LoginInfo.EMail;

			// 테스트 주소로 변경
			if (IsTest)
				to = "it@dintec.co.kr";

			// 주제에 따라 발송메일 주소 변경
			DataTable dtMailBySubj = DBMgr.GetDataTable("PS_CZ_PU_INQ_SUBJMAIL", companyCode, fileNumber, partnerCode);

			if (dtMailBySubj.Rows.Count > 0)
			{
				to = (string)dtMailBySubj.Rows[0]["MAIL"];
				ChangedAddress = true;	// ShowMessage("메일 주소가 변경됩니다.");
			}

			if (to == "")
			{
				ErrorMessage = "발신자가 없습니다.";
				return false;
			}

			// 제목
			string title = headRow["NM_VESSEL"] + " / " + headRow["NM_SHIP_YARD"] + " " + headRow["NO_HULL"] + " (IMO:" + headRow["NO_IMO"] + ") _" + partnerCode;

            if (!isHack)
            {
                if (companyCode == "TEST") title = "DINTEC - INQUIRY(" + fileNumber + ") " + title;
                else if (companyCode == "K100") title = "DINTEC - INQUIRY(" + fileNumber + ") " + title;
                else if (companyCode == "K200") title = "DUBHECO - INQUIRY(" + fileNumber + ") " + title;
                else if (companyCode == "S100") title = "DINTEC - INQUIRY(" + fileNumber + ") " + title;
            }
            else
            {
                if (companyCode == "TEST") title = "DINTEC";
                else if (companyCode == "K100") title = "DINTEC";
                else if (companyCode == "K200") title = "DUBHECO";
                else if (companyCode == "S100") title = "DINTEC";
            }

			// 기본파일
			string[] files1 = { headRow["NM_FILE"].ToString() };

			// 추가파일
			query = @"
SELECT
	NM_FILE_REAL
FROM CZ_MA_WORKFLOWL WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + companyCode + @"'
	AND NO_KEY = '" + fileNumber + @"'
	AND TP_STEP IN ('01', '54')";	// 01:INQUIRY,54:관련도면

			DataTable dtFiles = DBMgr.GetDataTable(query);
			string[] files2 = new string[dtFiles.Rows.Count];

			for (int i = 0; i < dtFiles.Rows.Count; i++)
				files2[i] = dtFiles.Rows[i][0].ToString();

			// ********** SRM 링크 생성
			query = @"
SELECT NEOE.SF_SYSDATE(GETDATE())
SELECT TOP 1 CD_AUTH FROM CZ_SC_AUTH_CODE WHERE CD_COMPANY = '" + companyCode + @"' AND CD_STEP = '1' ORDER BY DT_UPDATE DESC
SELECT TOP 1 CD_AUTH FROM CZ_SC_AUTH_CODE WHERE CD_COMPANY = '" + companyCode + @"' AND CD_STEP = '2' ORDER BY DT_UPDATE DESC";

			DataSet dtCrypt = DBMgr.GetDataSet(query);

			string today = dtCrypt.Tables[0].Rows[0][0].ToString().Substring(0, 8);
			string dateCode = today.Substring(2);
			string encrypted1 = Crypt.Encrypt(dateCode, dtCrypt.Tables[1].Rows[0][0].ToString()).Substring(0, 22);
			string encrypted2 = Crypt.Encrypt(partnerCode + "/" + fileNumber, dtCrypt.Tables[2].Rows[0][0].ToString());
						
			// html 형식으로 변환
			string dateFormat = "{0:yyyy년 MM월 dd일}";
			string srmLink = "";

			if (companyCode == "K100")
			{
				srmLink = @"
<a href='" + "http://srm.dintec.co.kr/RFQ/View.aspx?p=" + Uri.EscapeDataString(encrypted1 + encrypted2) + @"' style='color:#0000ff; text-decoration:underline;' target='_blank'>딘텍 SRM 단가 입력 바로가기</a>
(위 링크는 " + string.Format(dateFormat, DateTime.ParseExact(today, "yyyyMMdd", null).AddDays(6)) + " 까지 유효합니다.)";
			}
			else if (companyCode == "K200")
			{
				srmLink = @"
<a href='" + "http://srm.dubheco.com/RFQ/View.aspx?p=" + Uri.EscapeDataString(encrypted1 + encrypted2) + @"' style='color:#0000ff; text-decoration:underline;' target='_blank'>두베코 SRM 단가 입력 바로가기</a>
(위 링크는 " + string.Format(dateFormat, DateTime.ParseExact(today, "yyyyMMdd", null).AddDays(6)) + " 까지 유효합니다.)";
			}

			// 서명
			string sign = "";

            if (!isHack)
            {
                if (fileNumber.Left(2) == "FB" && (string)headRow["CD_AREA"] == "100") sign = MailSign.GetInquiryKR(srmLink);
                else if (fileNumber.Left(2) == "FB" && (string)headRow["CD_AREA"] != "100") sign = MailSign.GetInquiryEN(srmLink);
                else if (fileNumber.Left(2) == "DS" && (string)headRow["CD_AREA"] == "100") sign = MailSign.GetInquiryKR(srmLink);
                else if (fileNumber.Left(2) == "DS" && (string)headRow["CD_AREA"] != "100") sign = MailSign.GetInquiryEN(srmLink);
                else if (fileNumber.Left(2) != "FB" && (string)headRow["CD_AREA"] == "100") sign = MailSign.GetInquiryKR(empNameKor, empTelNumber, empMail, srmLink);
                else if (fileNumber.Left(2) != "FB" && (string)headRow["CD_AREA"] != "100") sign = MailSign.GetInquiryEN(empNameEng, empTelNumber, empMail, srmLink);
            }

			// ********** 메일발송
			P_CZ_MA_EMAIL_SUB f = new P_CZ_MA_EMAIL_SUB(from, to, cc, "", title, files1, files2, sign, fileNumber, partnerCode, boAuto) { CompanyCode = companyCode };

			// 메일 발송에 성공한 경우 수신자 업데이트
			if (f.ShowDialog() == DialogResult.OK)
			{
				// 발송에 성공한 경우 수신자, 발송일 업데이트 =====================> 발송타입도 컬럼 만들어서 저장하자
				query = @"
DECLARE @DT_SEND	NVARCHAR(14) = NEOE.SF_SYSDATE(GETDATE())

UPDATE CZ_PU_QTNH SET
	  YN_SEND = 'Y'
	, DT_SEND_MAIL = @DT_SEND
	, MAIL_SEND = '" + to + @"'
	, YN_SRM  = 'Y'
WHERE 1 = 1
	AND CD_COMPANY = '" + companyCode + @"'
	AND NO_FILE = '" + fileNumber + @"'
	AND CD_PARTNER = '" + partnerCode + @"'

SELECT @DT_SEND";

				DataTable dtSend = DBMgr.GetDataTable(query);
				SendedDate = dtSend.Rows[0][0];
							
				return true;
			}
			
			return false;			
		}

		public bool InquiryWorkFlow(string companyCode, string fileNumber, string partnerCode)
		{
			try
			{
				SQL sql = new SQL("SP_CZ_MA_WORKFLOW_SUPPLIER_AUTO", SQLType.Procedure);
				sql.Parameter.Add2("@P_CD_COMPANY"	, companyCode);
				sql.Parameter.Add2("@P_NO_FILE"		, fileNumber);
				sql.Parameter.Add2("@P_CD_PARTNER"	, partnerCode);
				sql.ExecuteNonQuery();

				// 발송에 성공한 경우 수신자, 발송일 업데이트 =====================> 발송타입도 컬럼 만들어서 저장하자
				string query = @"
DECLARE @DT_SEND	NVARCHAR(14) = NEOE.SF_SYSDATE(GETDATE())

UPDATE CZ_PU_QTNH SET
	  YN_SEND = 'Y'
	, DT_SEND_WORK = @DT_SEND
WHERE 1 = 1
	AND CD_COMPANY = '" + companyCode + @"'
	AND NO_FILE = '" + fileNumber + @"'
	AND CD_PARTNER = '" + partnerCode + @"'

SELECT @DT_SEND";

				DataTable dtSend = DBMgr.GetDataTable(query);
				SendedDate = dtSend.Rows[0][0];

				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool CheckWorkSend(string companyCode, string fileNumber, string partnerCode)
		{
			// 코드관리에서 워크로 강제 할당시키는지 여부 판단
			DataTable dtChkWf = SQL.GetDataTable("PS_CZ_PU_INQ_CHK_WF", SQLDebug.Print, companyCode, fileNumber, partnerCode);

			if (dtChkWf.Rows.Count > 0)
				return true;
			else
				return false;
		}
	}
}

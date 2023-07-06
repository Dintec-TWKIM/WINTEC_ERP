using System.Data;
using Dintec;
using cz;
using Duzon.Common.Forms;
using System;
using TABS.MailCarrier.WebSDK;
using TABS.MailCarrier.WebSDK.Mail;

namespace DintecExt
{
	public class Preregister
	{
		#region ===================================================================================================== Property

		public string FileNumber
		{
			get; set;
		}

		public string ErrorMessage
		{
			get; set;
		}

		#endregion

		public bool Inquiry(string companyCode, string preregNumber, string filePath)
		{
			//Message message = new Message(new Guid(""));
			////message.Subject
			//message.Copy(new Guid(""));

			try
			{ 
				// Step.1 파일번호 채번, 매출처, 담당자 가져오기
				DataTable dtCode = DBMgr.GetDataTable("PX_CZ_EZREG_INQ_CODE", companyCode, preregNumber);

				// Step.2 워크플로우 1단계 추가 및 파일번호 채번
				AutoInquiry ai = new AutoInquiry();
				string fileNumber = ai.SaveEmail(companyCode, (string)dtCode.Rows[0]["NO_FILE"], (string)dtCode.Rows[0]["CD_FILE"], "01", (string)dtCode.Rows[0]["NO_EMP"], "", filePath);

				// Step.3 매입처 지정 : PREREG 테이블의 컬럼에 VENDOR 업데이트만 함
				DataTable dtVendor = DBMgr.GetDataTable("PX_CZ_EZREG_INQ_VENDOR", companyCode, preregNumber);

				if (dtVendor.Select("ISNULL(CD_VENDOR, '') = ''").Length > 0)
				{
					ErrorMessage = "매입처 없는 항목 있음";
					return false;
				}

				// Step.4 견적서 생성
				DBMgr dbm = new DBMgr
				{
					DebugMode = DebugMode.None
				,	Procedure = "PX_CZ_EZREG_INQ_RUN"
				};
				dbm.AddParameter("@CD_COMPANY", companyCode);
				dbm.AddParameter("@NO_PREREG", preregNumber);
				dbm.AddParameter("@NO_FILE", dtCode.Rows[0]["NO_FILE"]);
				dbm.AddParameter("@NO_EMP", dtCode.Rows[0]["NO_EMP"]);
				dbm.AddParameter("@CD_PARTNER", dtCode.Rows[0]["CD_PARTNER"]);
				dbm.AddParameter("@NO_IMO", dtCode.Rows[0]["NO_IMO"]);
				DataTable dtPuInq = dbm.GetDataTable();

				// Step.5 매입견적서 워크파일 생성 및 메일(워크) 보내기
				foreach (DataRow row in dtPuInq.Rows)
				{
					string partnerCode = (string)row["CD_PARTNER"];
					string inqCode = (string)row["CD_PINQ"];

					P_CZ_PU_INQ f = new P_CZ_PU_INQ();
					f.Print(companyCode, fileNumber, partnerCode, null, false);

					// INQ 발송방법에 따라 발송
					if (inqCode == "EML")
					{
						f.SendMail(companyCode, fileNumber, partnerCode, true);
					}
					else if (inqCode == "WFP")
						f.SendWorkFlow(companyCode, fileNumber, partnerCode);
				}

				FileNumber = fileNumber;
				return true;
			}
			catch (Exception ex)
			{
				ErrorMessage = ex.Message;
				return false;
			}			
		}
	}
}

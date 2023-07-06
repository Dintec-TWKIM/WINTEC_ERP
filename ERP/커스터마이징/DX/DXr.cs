using Dintec;
using Duzon.Common.Forms;
//using Parsing;
using System;
using System.Data;

namespace DX
{
	public class DXr
	{
		#region ===================================================================================================== Property

		public string CompanyCode
		{
			get; set;
		}

		public string PreregNumber
		{
			get; set;
		}

		public string FromAddress
		{
			get; set;
		}

		public string FileNumber
		{
			get; set;
		}

		public string Tag
		{
			get; set;
		}

		public string Message
		{
			get; set;
		}

		public bool IsTest
		{
			get; set;
		}

		#endregion ===================================================================================================== Property

		public DXr()
		{
			Tag = "";
			Message = "";
			IsTest = false;
		}

		public bool Inquiry(string companyCode, string preregNumber, string filePath)
		{
			CompanyCode = companyCode;
			PreregNumber = preregNumber;

			// 테스트 여부 판단
			if (Global.MainFrame.LoginInfo.UserID.In("S-343", "S-391", "S-458"))
				IsTest = true;

			if (FromAddress.In("dandyszero@naver.com", "k2568088@naver.com", "licadiom@naver.com"))
				IsTest = true;

			try
			{
				if (preregNumber == "")
					throw new Exception("사전입력 번호가 없습니다.");

				// ********** Step.1 매입처 지정 : PREREG 테이블의 컬럼에 VENDOR 업데이트만 함, 매입처에 따라 채번, 담당자가 달라지므로 먼저 함
				DataTable dtVendor = SQL.GetDataTable("PX_CZ_DXREG_INQ_VENDOR", companyCode, preregNumber);

				// ********** Step.2 파일번호 채번, 매출처, 담당자 가져오기
				DataTable dtHead = SQL.GetDataTable("PX_CZ_DXREG_INQ_HEAD_R2", companyCode, preregNumber);

				if (dtHead.Rows.Count == 0)
					throw new Exception("헤더정보를 가져오지 못했습니다.");

				// ********** Step.3 워크플로우 1단계 추가 및 파일번호 채번
				//AutoInquiry ai = new AutoInquiry();
				//FileNumber = ai.SaveEmail(companyCode, (string)dtHead.Rows[0]["NO_FILE"], (string)dtHead.Rows[0]["CD_FILE"], "01", (string)dtHead.Rows[0]["NO_EMP"], "", filePath);

				// HGS 체크
				if (dtVendor.Select("ISNULL(DXVENDOR_CODE, '') = '11823'").Length > 0)
				{
					Message = "HGS! 자동 등록을 취소합니다.";
					Tag = "/HGS";
					SaveLog(true);

					// ********** RPA 큐 추가
					if (companyCode == "K100")
					{
						RPA rpa = new RPA() { Process = "INQ", FileNumber = FileNumber, PartnerCode = "11823" };
						rpa.AddQueue();
					}

					return true;
				}

				// ********** Step.4 견적서 생성 (HGS는 안함)
				SQL sql = new SQL("PX_CZ_DXREG_INQ_RUN", SQLType.Procedure, SQLDebug.Popup);
				sql.Parameter.Add2("@CD_COMPANY", companyCode);
				sql.Parameter.Add2("@NO_PREREG", preregNumber);
				sql.Parameter.Add2("@NO_FILE", dtHead.Rows[0]["NO_FILE"]);
				sql.Parameter.Add2("@NO_EMP", dtHead.Rows[0]["NO_EMP"]);
				sql.Parameter.Add2("@CD_PARTNER", dtHead.Rows[0]["CD_PARTNER"]);
				sql.Parameter.Add2("@NO_IMO", dtHead.Rows[0]["NO_IMO"]);
				DataTable dtPuInq = sql.GetDataTable();

				// ********** Step.5 매입견적서 워크파일 생성 및 메일(워크) 보내기
				bool holded = false;
				bool sendYn = true;

				// 매입처 체크
				if (dtVendor.Select("ISNULL(DXVENDOR_CODE, '') = ''").Length > 0)
				{
					// 80% 이상 일치되어 있으면 그냥 보냄

					// 80% 미만일 경우는 fail
					Message = "매입처 없는 항목 있음";
					Tag = "/INQ_ONLY";
					sendYn = false;
				}

				if (sendYn)
				{
					foreach (DataRow row in dtPuInq.Rows)
					{
						string partnerCode = row["CD_PARTNER"].ToString();
						string inqCode = row["CD_PINQ"].ToString();
						string holding = row["YN_HOLD"].ToString();

						PRT.PInq(companyCode, FileNumber, partnerCode, null, false);

						// INQ 발송방법에 따라 발송 (홀딩이 아닐 경우만 발송)
						if (holding != "Y")
						{
							SEND s = new SEND { IsTest = IsTest };

							if (s.CheckWorkSend(companyCode, FileNumber, partnerCode))
							{
								s.InquiryWorkFlow(companyCode, FileNumber, partnerCode);
							}
							else if (inqCode == "EML")
							{
								if (!s.InquiryMail(companyCode, FileNumber, partnerCode, true))
								{
									holded = true;
									Message = s.ErrorMessage;
								}
							}
						}
					}

					// 태그
					if (dtPuInq.Select("YN_HOLD = 'Y'").Length > 0 || holded)
						Tag = "/HOLD";
					else if (dtPuInq.Rows.Count == 0)
						Tag = "/INQ_ONLY(CHECK_ORIGIN)";
					else
						Tag = "/SENT";
				}

				// ********** Step.6 DX데이터 생성
				//DXs.SaveInq(companyCode, FileNumber);
				키워드.견적저장(companyCode, FileNumber);

				SaveLog(true);
				return true;
			}
			catch (Exception ex)
			{
				Message = ex.Message;
				SaveLog(false);
				return false;
			}
		}

		private void SaveLog(bool completed)
		{
			SQL sql = new SQL("PX_CZ_DXREG_INQ_LOG", SQLType.Procedure);
			sql.Parameter.Add2("@CD_COMPANY", CompanyCode);
			sql.Parameter.Add2("@NO_PREREG", PreregNumber);
			sql.Parameter.Add2("@TAG", Tag);
			sql.Parameter.Add2("@MESSAGE", Message);
			sql.Parameter.Add2("@COMPLETED", completed ? "Y" : "N");
			sql.ExecuteNonQuery();
		}
	}
}
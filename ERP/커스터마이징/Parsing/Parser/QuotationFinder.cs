using System.Linq;
using Aspose.Email.Outlook;
using System.Data;
using System.Windows.Forms;
using Dintec;

namespace Parsing
{
	public class QuotationFinder
	{		
		public static bool IsPossible(string companyCode, string partnerCode, string fileName)
		{
			string flag = CheckParseable(partnerCode, fileName, false);

			if (flag == "")
				return false;
			else
				return true;
		}

		public static string GetFileFromWorkFlow(string companyCode, string fileNumber, string partnerCode)
		{
			// 워크플로우에서 파일 자동 검색
			string query = @"
SELECT
	NM_FILE_REAL
FROM CZ_MA_WORKFLOWL
WHERE 1 = 1
	AND CD_COMPANY = '" + companyCode + @"'
	AND NO_KEY = '" + fileNumber + @"'
	AND CD_SUPPLIER = '" + partnerCode + @"'
	AND TP_STEP = '04'
	AND RIGHT(NM_FILE_REAL, 4) IN ('.MSG', '.PDF', '.XLS')
	AND ISNULL(YN_INCLUDED, 'N') = 'N'
ORDER BY NO_LINE DESC";

			DataTable dtFile = DBMgr.GetDataTable(query);	
			string downPath = Application.StartupPath + @"\temp\";
			string fileName = "";

			foreach (DataRow row in dtFile.Rows)
			{
				// 워크플로우 파일의 확장자 분석
				string wfFile = row[0].ToString();
				string wfExts = wfFile.Split('.').Last();
				string wfName = wfFile.Replace("." + wfExts, "");
                
				// 아웃룩의 경우는 파일 다운로드 후 파싱여부 분석
				if (wfExts.ToLower() == "msg")
				{
					wfFile = downPath + FileMgr.Download_WF(companyCode, fileNumber, wfFile, false);
				}

				fileName = CheckParseable(partnerCode, wfFile, true);
				
				// 찾았으면 루프 종료
				if (fileName != "") break;
			}
			
			
			return fileName;
		}

		// 파싱 가능한 파일의 경우 파일이름 또는 "Y"를 리턴함, 불가능한 경우는 ""를 리턴, 아웃룩의 경우는 Full Path를, 나머지는 파일명만 파라미터로 받음
		public static string CheckParseable(string partnerCode, string fileName, bool isSave)
		{
			// 파일 분석
			string chkFile = fileName.Split('\\').Last();			// 파일명
			string chkExts = chkFile.Split('.').Last();				// 확장자 (. 이후로)
			string chkName = chkFile.Replace("." + chkExts, "");	// 이름 (확장자 제외한)

			// ********** 아웃룩 파일을 분석해야 하는 업체
			if (chkExts.ToLower() == "msg")
			{
				MapiMessage msg = MapiMessage.FromFile(fileName);


				// 재고코드등록(메일)

				// 동화뉴텍
				if (msg.Body.Contains("동화뉴텍"))
				{
					return fileName;
				}
				// 하이텍오션
				else if (msg.Body.Contains("hitechocean") || msg.Body.Contains("하이텍오션"))
				{
					return fileName;
				}
				// 알파라발
				else if (msg.Body.Contains("Alfa Laval") || msg.Body.Contains("alfalaval"))
				{
					foreach (MapiAttachment attachment in msg.Attachments)
					{
						string attFile = attachment.FileName;
						string attExts = attFile.Split('.').Last();
						string attName = attFile.Replace("." + attExts, "");
						string attLongFile = attachment.LongFileName;

						// ***** 아웃룩 파일중 첨부파일 중 pdf 파일을 분석해야 하는 업체
						if (attExts.ToLower() == "pdf" && attLongFile.Contains("QUOTATION"))
						{
							   return GetFileAndSave(attachment, isSave);
						}
					}

					return fileName;
				}
                else if (msg.Body.Contains("E-mail : hymax@chol.com"))
                {
                    return fileName;
                }
				// 임시 우양선기  html - xml - 파싱
                //else if (msg.Body.Contains("wooyang.moatt"))
                //{
                //    //return fileName;
                //}

				foreach (MapiAttachment attachment in msg.Attachments)
				{
					string attFile = attachment.FileName;
					string attExts = attFile.Split('.').Last();
					string attName = attFile.Replace("." + attExts, "");
					string attLongFile = attachment.LongFileName;

                    // ***** 아웃룩 파일중 첨부파일 중 pdf 파일을 분석해야 하는 업체
                    if (attExts.ToLower() == "pdf")
                    {
                        if (partnerCode == "00587" && attName.Length == 6 && GetTo.Decimal(attName) > 0)
                        {
                            // 위너스 마린
                            return GetFileAndSave(attachment, isSave);
                        }
                        else if (attName.Length > 2 && partnerCode == "00740" && attName.Contains("ASP"))
                        {
                            // 하이에어코리아
                            return GetFileAndSave(attachment, isSave);
                        }
                        else if (attName.Length > 1 && partnerCode == "00432")
                        {
                            // 삼건세기 -> 삼건엠에스에서 변경됨
                            return GetFileAndSave(attachment, isSave);
                        }
                        else if (partnerCode == "00004")
                        {
                            // 경성에스알엠
                            return GetFileAndSave(attachment, isSave);
                        }
                        else if (partnerCode == "05430")
                        {
                            // AC FLYNN REFRIGERATION LIMITED
                            return GetFileAndSave(attachment, isSave);
                        }
                        else if (partnerCode == "00003")
                        {
                            // 경남드라이어
                            return GetFileAndSave(attachment, isSave);
                        }
                        else if (partnerCode == "00509")
                        {
                            // 신성밸브
                            return GetFileAndSave(attachment, isSave);
                        }
                        else if (partnerCode == "11278")
                        {
                            // 신우이앤티
                            return GetFileAndSave(attachment, isSave);
                        }
                        else if (partnerCode == "11362" && (attName.Contains("딘") || attName.Contains("두")))
                        {
                            // 극동일렉콤
                            return GetFileAndSave(attachment, isSave);
                        }
                        else if (partnerCode == "12414")
                        {
                            // 케스콤
                            return GetFileAndSave(attachment, isSave);
                        }
                        else if (partnerCode == "06731" && attName.StartsWith("JI"))
                        {
                            // 에스엔제이마린
                            return GetFileAndSave(attachment, isSave);
                        }
                        else if (partnerCode == "00673" && msg.SenderEmailAddress.ToString().Contains("spare@consilium.co.kr"))
                        {
                            // 컨실리움마린코리아
                            return GetFileAndSave(attachment, isSave);
                        }
                        else if (partnerCode == "11124" && attName.StartsWith("TC"))
                        {
                            // 더센텀무역
                            return GetFileAndSave(attachment, isSave);
                        }
                        else if (partnerCode == "07249" && attName.Length == 10)
                        {
                            // 우양선기
                            return GetFileAndSave(attachment, isSave);
                        }
                        else if (partnerCode == "07249")
                        {
                            // 우양선기 2
                            return GetFileAndSave(attachment, isSave);
                        }
                        else if (partnerCode.Equals("01340") && attName.StartsWith("PA"))
                        {
                            // 두산엔진
                            return GetFileAndSave(attachment, isSave);
                        }
                        else if (partnerCode.Equals("00449") && attName.StartsWith("OFF"))
                        {
                            // 삼주이엔지
                            return GetFileAndSave(attachment, isSave);
                        }
                        else if (partnerCode.Equals("00141") && attName.Length == 8)
                        {
                            // 동화뉴텍
                            return GetFileAndSave(attachment, isSave);
                        }
                        else if (partnerCode.Equals("06305") && attLongFile.Length < 25)
                        {
                            // 양산마린
                            return GetFileAndSave(attachment, isSave);
                        }
                        else if (partnerCode.Equals("10116") && attLongFile.StartsWith("YN"))
                        {
                            // 나미테크
                            return GetFileAndSave(attachment, isSave);
                        }
                        else if (partnerCode.Equals("05571") && attName.Length == 8)
                        {
                            // 테크플로우
                            return GetFileAndSave(attachment, isSave);
                        }
                        else if (partnerCode.Equals("03231") && attLongFile.Contains("견적"))
                        {
                            // DEX
                            return GetFileAndSave(attachment, isSave);
                        }
                        else if (partnerCode.Equals("10642") && attLongFile.StartsWith("SHSQ"))
                        {
                            return GetFileAndSave(attachment, isSave);
                        }
                        // 선진종합
                        else if (partnerCode.Equals("06486") && attLongFile.EndsWith("딘텍.pdf") || attLongFile.EndsWith("두베코.pdf"))
                        {
                            return GetFileAndSave(attachment, isSave);
                        }
                        // AGM
                        else if (partnerCode.Equals("04923"))
                        {
                            return GetFileAndSave(attachment, isSave);
                        }
                        // 경성에스알엠 두베코용
                        else if (partnerCode.Equals("00004"))
                        {
                            return GetFileAndSave(attachment, isSave);
                        }
                        // 다코스
                        else if (partnerCode.Equals("11758") && attLongFile.StartsWith("A"))
                        {
                            return GetFileAndSave(attachment, isSave);
                        }
                        // 위더스월드
                        else if (partnerCode.Equals("12098"))
                        {
                            return GetFileAndSave(attachment, isSave);
                        }
                        // 하나테크
                        else if (partnerCode.Equals("07734") && attLongFile.StartsWith("HN"))
                        {
                            return GetFileAndSave(attachment, isSave);
                        }
                        // 서원텍
                        else if (partnerCode.Equals("12609"))
                        {
                            return GetFileAndSave(attachment, isSave);
                        }
                        // 한국피에이치이
                        else if (partnerCode.Equals("11566"))
                        {
                            return GetFileAndSave(attachment, isSave);
                        }
                        // MJ CORPORATION
                        else if (partnerCode.Equals("07436"))
                        {
                            return GetFileAndSave(attachment, isSave);
                        }
                        // JETS KOREA
                        else if (partnerCode.Equals("00626"))
                        {
                            return GetFileAndSave(attachment, isSave);
                        }
                        else if (partnerCode.Equals("08507"))
                        {
                            return GetFileAndSave(attachment, isSave);
                        }
                        else if (partnerCode.Equals("00401"))
                        {
                            // 파나시아 잠정중단
                            //return GetFileAndSave(attachment, isSave);
                        }
                        else if (partnerCode.Equals("10658"))
                        {
                            return GetFileAndSave(attachment, isSave);
                        }
                        else if (partnerCode.Equals("06636"))
                        {
                            return GetFileAndSave(attachment, isSave);
                        }
                        // 오엠씨이스트
                        else if (partnerCode.Equals("03225"))
                        {
                            return GetFileAndSave(attachment, isSave);
                        }
                        // 한영기연
                        else if (partnerCode.Equals("00196"))
                        {
                            return GetFileAndSave(attachment, isSave);
                        }
                        // 한국에머슨
                        else if (partnerCode.Equals("00312") && !attLongFile.ToUpper().Contains("STANDARD TERMS"))
                        {
                            return GetFileAndSave(attachment, isSave);
                        }
						else if (partnerCode.Equals("11943"))
						{
							return GetFileAndSave(attachment, isSave);
						}
						else if (partnerCode.Equals("12977"))
						{
							return GetFileAndSave(attachment, isSave);
						}
                        else if (partnerCode.Equals("08286"))
                        {
                            return GetFileAndSave(attachment, isSave);
                        }
                        else if (partnerCode.Equals("04189"))
                        {
                            return GetFileAndSave(attachment, isSave);
                        }
                        else if (partnerCode.Equals("00069"))
                        {
                            return GetFileAndSave(attachment, isSave);
                        }
                        else if (partnerCode.Equals("00047"))
                        {
                            return GetFileAndSave(attachment, isSave);
                        }
                        // 대송유니텍
                        else if (partnerCode.Equals("15427"))
						{
                            return GetFileAndSave(attachment, isSave);
                        }
                        else if (partnerCode.Equals("15424"))
						{
                            return GetFileAndSave(attachment, isSave);
						}
                        else if (partnerCode.Equals("05817"))
						{
                            return GetFileAndSave(attachment, isSave);
                        }
                        else if (partnerCode.Equals("12238"))
						{
                            return GetFileAndSave(attachment, isSave);
                        }
                        // 백드레인코리아
                        else if (partnerCode.Equals("15493"))
						{
                            return GetFileAndSave(attachment, isSave);
                        }
                        // 서주해양
                        else if (partnerCode.Equals("17468"))
                        {
                            return GetFileAndSave(attachment, isSave);
                        }
                        else if (partnerCode.Equals("08208"))
						{
                            return GetFileAndSave(attachment, isSave);
                        }
                        else if (partnerCode.Equals("15075"))
						{
                            return GetFileAndSave(attachment, isSave);
                        }
                        else if (partnerCode.Equals("04774"))
                        {
                            return GetFileAndSave(attachment, isSave);
                        }
                        // 해동메탈 개발 중단
                        //                  else if (partnerCode.Equals("00778"))
                        //{
                        //                      return GetFileAndSave(attachment, isSave);
                        //                  }
                    }
					else if (attExts.ToLower() == "xls")
					{
                        if (attName.Length > 5 && partnerCode == "00133")
						{
							// 정아마린
							return GetFileAndSave(attachment, isSave);
						}
						else if (attName.Length > 1 && partnerCode.Equals("12188") && attName.StartsWith("PK"))
						{
							return GetFileAndSave(attachment, isSave);
						}
                        else if (attName.Length > 1 && partnerCode.Equals("00507"))
                        {
                            return GetFileAndSave(attachment, isSave);
                        }
					}
                    
                    else if (attExts.ToLower().Equals("msg"))
                    {

                    }
				}
			}
            else if (chkExts.ToLower() == "pdf")
            {
                if (chkFile.Length > 2 && partnerCode == "00740" && chkFile.Contains("ASP"))
                {
                    // 하이에어코리아
                    return GetFile(isSave, chkFile);
                    // 수정해야함
                    //fileName = pplication.StartupPath + @"\temp\" + FileMgr.Download_WF(companyCode, fileNumber, chkFile.ToString(), false);
                    //fileName = Application.StartupPath + @"\temp\" + FileMgr.Download_WF(companyCode, fileNumber, serverFile, run);
                }
            }

			return "";
		}

		private static string GetFileAndSave(MapiAttachment attachment, bool isSave)
		{
			string fileName = "";
			string path = Application.StartupPath + @"\temp\";

			if (isSave)
			{
				fileName = path + FileMgr.GetUniqueFileName(path + attachment.FileName);
				attachment.Save(fileName);
			}
			else
			{
				fileName = "Y";
			}

			return fileName;
		}


        private static string GetFile(bool isSave, string _fileName)
        {
            string fileName = "";
            string path = Application.StartupPath + @"\temp\";

            if (isSave)
            {
                fileName = path + FileMgr.GetUniqueFileName(path + _fileName);
            }
            else
            {
                fileName = "Y";
            }

            return fileName;
        }
	}
}

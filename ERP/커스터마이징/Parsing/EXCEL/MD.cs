using Dintec;
using System;
using System.Data;
using System.Net;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;

namespace Parsing.EXCEL
{
	public class MD
	{
		public static Excel.Application application = null;
		public static Excel.Worksheet worksheet = null;
		public static Excel.Workbook workbook = null;


		public static string MD_EXCEL(string fileNo)
		{
			string returnStr = string.Empty;

			WebClient client;

			try
			{
				string excelFileName = string.Empty;

				string iTemNm = string.Empty;
				string nmUser = string.Empty;
				string cdPartner = string.Empty;
				string noEmail = string.Empty;
				string noTel = string.Empty;
				string datetime = string.Empty;
				string saveFileName = string.Empty;
				string imgUrl = string.Empty;
				string no_po_partner = string.Empty;
				string nmVessel = string.Empty;
				string subject = string.Empty;
				string savePdfName = string.Empty;


				string query = string.Empty;
				query = @"DECLARE @URL	NVARCHAR(100)
SET @URL = (SELECT BUSINESS_HOST_SERVER FROM CM_SERVER_CONFIG WHERE SERVER_KEY = 'DINTEC2') + '/shared/image/human/sign/K100/'

SELECT 
	QTNL.NO_FILE, 
	QTNL.NM_SUBJECT, 
	QTNL.NM_ITEM_PARTNER, 
	SOH.NO_SO, 
	SOH.NO_EMP,
	EMP.NM_ENG, 
	EMP.NO_EMAIL,
	EMP.NO_TEL,
	SOH.NO_PO_PARTNER, 
	PA.LN_PARTNER,
	HULL.NM_VESSEL,
	@URL + VEMP.DC_SIGN AS SIGN_URL
FROM CZ_SA_QTNL AS QTNL
JOIN SA_SOH AS SOH ON SOH.CD_COMPANY = QTNL.CD_COMPANY AND SOH.NO_SO = QTNL.NO_FILE
JOIN MA_EMP AS EMP ON EMP.CD_COMPANY = SOH.CD_COMPANY AND EMP.NO_EMP = SOH.NO_EMP
JOIN V_CZ_MA_EMP AS VEMP ON VEMP.CD_COMPANY = SOH.CD_COMPANY AND VEMP.NO_EMP = SOH.NO_EMP
JOIN CZ_MA_PARTNER AS PA ON PA.CD_COMPANY = SOH.CD_COMPANY AND PA.CD_PARTNER = SOH.CD_PARTNER 
JOIN CZ_MA_HULL AS HULL ON HULL.NO_IMO = SOH.NO_IMO
WHERE NO_FILE = '" + fileNo + "'";
				DataTable dt = DBMgr.GetDataTable(query);

				no_po_partner = dt.Rows[0]["NO_PO_PARTNER"].ToString().Trim();
				//saveFileName = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\MD_" + fileNo + "_" + no_po_partner + ".xls";
				//savePdfName = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\MD_" + fileNo + "_" + dt.Rows[0]["NO_PO_PARTNER"].ToString().Trim() + ".pdf";
				savePdfName = @"c:\ERPU\Browser\temp\test1234.pdf";
				nmUser = dt.Rows[0]["NM_ENG"].ToString().Trim();
				cdPartner = dt.Rows[0]["LN_PARTNER"].ToString().Trim();
				imgUrl = dt.Rows[0]["SIGN_URL"].ToString().Trim();
				noEmail = dt.Rows[0]["NO_EMAIL"].ToString().Trim();
				noTel = dt.Rows[0]["NO_TEL"].ToString().Trim();
				nmVessel = dt.Rows[0]["NM_VESSEL"].ToString().Trim();
				subject = dt.Rows[0]["NM_SUBJECT"].ToString().Trim();
				excelFileName = @"C:\\ERPU\\Browser\\MD.xls";


				object missing = System.Reflection.Missing.Value;
				application = new Excel.Application();
				workbook = application.Workbooks.Open(excelFileName, missing, missing
					, missing, missing, missing, missing, missing, missing
					, missing, missing, missing, missing, missing, missing);
				worksheet = (Excel.Worksheet)workbook.Sheets["MD"];


				// 출력일자
				worksheet.Cells[6, 5] = DateTime.Now.ToString("yyyy-MM-dd");
				worksheet.Cells[73, 3] = DateTime.Now.ToString("yyyy-MM-dd");


				// 파일번호
				worksheet.Cells[17, 21] = "SDoC-" + fileNo;
				worksheet.Cells[9, 5] = fileNo;

				// 매출처
				worksheet.Cells[12, 5] = cdPartner;
				worksheet.Cells[13, 5] = nmVessel;
				worksheet.Cells[14, 5] = no_po_partner;


				

				// 영업담당자
				worksheet.Cells[13, 21] = nmUser;

				// 총 수량
				worksheet.Cells[22, 15] = dt.Rows.Count.ToString();


				if (noTel.StartsWith("0"))
				{
					worksheet.Cells[14, 21] = "82-" + noTel.Substring(1, noTel.Length - 1).Trim();
				}
				else
				{
					// 전화번호
					worksheet.Cells[14, 21] = "82-" + noTel;
				}

				// 메일
				worksheet.Cells[16, 21] = noEmail;

				subject = subject.Replace("\r\n", " ").Trim();

				// 주제
				if (subject.ToUpper().StartsWith("FOR"))
				{
					if (subject.Length > 30)
					{
						worksheet.Cells[22, 2] = "SPARES " + subject.Substring(0, 25);
						worksheet.Cells[22, 19] = subject.Substring(25, subject.Length - 25);
					}
					else
					{
						worksheet.Cells[22, 2] = "SPARES " + subject;
					}
				}
				else
				{
					if (subject.Length > 40)
					{
						worksheet.Cells[22, 2] = "SPARES FOR" + subject.Substring(0,25);
						worksheet.Cells[22, 19] = subject.Substring(25, subject.Length - 25);
					}
					else
					{
						worksheet.Cells[22, 2] = "SPARES FOR" + subject;
					}
				}

				// 사인
				client = new WebClient();

				Microsoft.Office.Interop.Excel.Range oRange = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[72, 7];
				float Left = (float)((double)oRange.Left);
				float Top = (float)((double)oRange.Top);
				worksheet.Shapes.AddPicture(imgUrl, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left, Top, 130, 36);



				Excel.XlFixedFormatType paramExportFormat = Excel.XlFixedFormatType.xlTypePDF;
				Excel.XlFixedFormatQuality paramExportQuality = Excel.XlFixedFormatQuality.xlQualityStandard;
				bool paramOpenAfterPublish = false;
				bool paramIncludeDocProps = false;
				bool paramIgnorePrintAreas = false;
				object paramFromPage = Type.Missing;
				object paramToPage = Type.Missing;

				workbook.ExportAsFixedFormat(paramExportFormat,
							savePdfName, paramExportQuality,
							paramIncludeDocProps, paramIgnorePrintAreas, paramFromPage,
							paramToPage, paramOpenAfterPublish,
							missing);


				//// 저장 부분
				//workbook.SaveAs(Filename: savePdfName);

				//workbook.Close(false, Type.Missing, Type.Missing);


				returnStr = "출력이 완료되었습니다.";
			}
			catch (Exception ee)
			{
				ee.Message.ToString();
			}
			finally
			{
				workbook.Close(false);
				workbook = null;

				application.Quit();
				application = null;

				//ReleaseExcelObject(workbook);
				//ReleaseExcelObject(worksheet);
				//ReleaseExcelObject(application);

				//workbook = null;
				//if (application != null)
				//{
				//	Process[] pProcess;
				//	pProcess = System.Diagnostics.Process.GetProcessesByName("Excel");
				//	pProcess[0].Kill();
				//}
				//application = null;
			}

			return returnStr;
		}

		private static void ReleaseExcelObject(object obj)
		{
			try
			{
				if (obj != null)
				{ System.Runtime.InteropServices.Marshal.ReleaseComObject(obj); obj = null; }
			}
			catch (Exception ex)
			{
				obj = null; throw ex;
			}
			finally
			{
				GC.Collect();
			}
		}


	}
}

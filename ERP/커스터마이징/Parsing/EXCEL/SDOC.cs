using Dintec;
using System;
using System.Data;
using System.Net;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using Duzon.Common.Forms;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using Global = Duzon.Common.Forms.Global;
using System.Drawing;
using System.Windows.Forms;
using Application = Microsoft.Office.Interop.Excel.Application;

namespace Parsing.EXCEL
{
	public class SDOC
	{
		public static Excel.Application application = null;
		public static Excel.Worksheet worksheet = null;
		public static Excel.Workbook workbook = null;

		public static string CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
		public static string NO_EMP = Global.MainFrame.LoginInfo.UserID;

		public static Excel.Application eApplication = null;
		public static Excel.Workbook eWorkBook = null;
		public static Excel.Sheets eSheets = null;
		public static Excel.Worksheet eWorkSheet = null;
		public static Excel.Range srcRange = null;
		public static Excel.Range dstRange = null;

		public static string SDOC_EXCEL(string fileNo)
		{
			string returnStr = string.Empty;

			WebClient client;

			try
			{
				string excelFileName = string.Empty;

				string iTemNm = string.Empty;
				string nmUser = string.Empty;
				string cdPartner = string.Empty;
				string datetime = string.Empty;
				string saveFileName = string.Empty;
				string savePdfName = string.Empty;
				string imgUrl = string.Empty;

				string subject = string.Empty;
				//string imgFileName = string.Empty;


				string query = string.Empty;
				query = string.Format(@"DECLARE @URL	NVARCHAR(100)
SET @URL = (SELECT BUSINESS_HOST_SERVER FROM CM_SERVER_CONFIG WHERE SERVER_KEY = 'DINTEC2') + '/shared/image/human/sign/{0}/'

SELECT 
	QTNL.NO_FILE, 
	QTNL.NM_SUBJECT, 
	REPLACE(REPLACE(QTNL.NM_ITEM_PARTNER, CHAR(10), ''), CHAR(13), '') AS NM_ITEM_PARTNER, 
	SOL.NO_SO, 
	SOH.NO_EMP,
	EMP.NM_ENG, 
	SOH.NO_PO_PARTNER, 
	PA.LN_PARTNER,
	@URL + VEMP.DC_SIGN AS SIGN_URL
FROM SA_SOL AS SOL
JOIN CZ_SA_QTNL AS QTNL ON QTNL.CD_COMPANY = SOL.CD_COMPANY AND QTNL.NO_FILE = SOL.NO_SO AND QTNL.NO_LINE = SOL.SEQ_SO
JOIN SA_SOH AS SOH ON SOH.CD_COMPANY = SOL.CD_COMPANY AND SOH.NO_SO = SOL.NO_SO
JOIN MA_EMP AS EMP ON EMP.CD_COMPANY = SOH.CD_COMPANY AND EMP.NO_EMP = SOH.NO_EMP
JOIN V_CZ_MA_EMP AS VEMP ON VEMP.CD_COMPANY = SOH.CD_COMPANY AND VEMP.NO_EMP = SOH.NO_EMP
JOIN CZ_MA_PARTNER AS PA ON PA.CD_COMPANY = SOH.CD_COMPANY AND PA.CD_PARTNER = SOH.CD_PARTNER 
WHERE NO_FILE ='{1}'
AND SOL.CD_COMPANY = '{2}'
ORDER BY SOL.SEQ_SO", CD_COMPANY, fileNo, CD_COMPANY);
				DataTable dt = DBMgr.GetDataTable(query);


				//saveFileName = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\SDOC_" + fileNo + "_" + dt.Rows[0]["NO_PO_PARTNER"].ToString().Trim() + ".xls";
				//savePdfName = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\SDOC_" + fileNo + "_" + dt.Rows[0]["NO_PO_PARTNER"].ToString().Trim() + ".pdf";
				//savePdfName = "P:\\SDOC_" + fileNo + "_" + dt.Rows[0]["NO_PO_PARTNER"].ToString().Trim() + ".pdf";
				if (CD_COMPANY.Contains("S100") || NO_EMP.Equals("SYSADMIN"))
				{
					saveFileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\SDOC_" + fileNo + "_" + dt.Rows[0]["NO_PO_PARTNER"].ToString().Replace(" ", "_").Replace("-", "_").Replace("/", "_").Replace(":", "").Replace(";", "").Trim() + ".xls";
					savePdfName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\SDOC_" + fileNo + "_" + dt.Rows[0]["NO_PO_PARTNER"].ToString().Replace(" ", "_").Replace("-", "_").Replace("/", "_").Replace(":", "").Replace(";", "").Trim() + ".pdf";
				}
				else
				{
					saveFileName = "P:\\SDOC_" + fileNo + "_" + dt.Rows[0]["NO_PO_PARTNER"].ToString().Replace(" ", "_").Replace("-", "_").Replace("/", "_").Replace(":", "").Replace(";", "").Trim() + ".xls";
					savePdfName = "P:\\SDOC_" + fileNo + "_" + dt.Rows[0]["NO_PO_PARTNER"].ToString().Replace(" ", "_").Replace("-", "_").Replace("/", "_").Replace(":", "").Replace(";", "").Trim() + ".pdf";
				}


				nmUser = dt.Rows[0]["NM_ENG"].ToString().Trim();
				cdPartner = dt.Rows[0]["LN_PARTNER"].ToString().Trim();
				imgUrl = dt.Rows[0]["SIGN_URL"].ToString().Trim();

				if (CD_COMPANY.Contains("S100"))
				{
					excelFileName = @"C:\\ERPU\\Browser\\SDoC_S100.xls";
				}
				else if (CD_COMPANY.Contains("K100"))
				{
					excelFileName = @"C:\\ERPU\\Browser\\SDoC.xls";
				}
				else if (CD_COMPANY.Contains("K200"))
				{
					excelFileName = @"C:\\ERPU\\Browser\\SDoC_K200.xls";
				}


				object missing = System.Reflection.Missing.Value;
				application = new Excel.Application();
				workbook = application.Workbooks.Open(excelFileName, missing, missing
					, missing, missing, missing, missing, missing, missing
					, missing, missing, missing, missing, missing, missing);
				worksheet = (Excel.Worksheet)workbook.Sheets["SDoC"];
				//worksheet.Activate();


				// 파일번호
				worksheet.Cells[9, 10] = "SDoC-" + fileNo;

				// 매출처
				worksheet.Cells[11, 10] = cdPartner;


				subject = dt.Rows[0]["NM_SUBJECT"].ToString().Trim();
				subject = subject.Replace("\r\n", " ").Trim();

				// 주제

				//if (dt.Rows.Count > 10)
				//{
				for (int r = 0; r < dt.Rows.Count; r++)
				{
					Range line = (Range)worksheet.Rows[19 + r];
					line.Insert();

					Range lineColor = (Range)worksheet.Range[worksheet.Cells[19 + r - 1, 10], worksheet.Cells[19 + r - 1, 22]];
					lineColor.Interior.Color = Color.FromArgb(255, 255, 153);
					lineColor.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;

					if (r == 0)
					{
						worksheet.Cells[19 + r - 1, 3] = "3)";
						worksheet.Cells[19 + r - 1, 4] = "Object(s) of the declaration:";
					}

					worksheet.Cells[19 + r - 1, 9] = r + 1 + ")";

					if (dt.Rows[r]["NM_ITEM_PARTNER"].ToString().Length > 50)
					{
						worksheet.Cells[19 + r - 1, 10] =  dt.Rows[r]["NM_ITEM_PARTNER"].ToString().Trim().Substring(0,49);
					}
					else
					{
						worksheet.Cells[19 + r - 1, 10] = dt.Rows[r]["NM_ITEM_PARTNER"].ToString().Trim();
					}

					
				}

				// 출력일자
				worksheet.Cells[61 + dt.Rows.Count - 10, 10] = DateTime.Now.ToString("yyyy-MM-dd");


				// 담당자
				worksheet.Cells[66 + dt.Rows.Count - 10, 4] = nmUser + ", SALES STAFF";

				// 사인
				client = new WebClient();


				Microsoft.Office.Interop.Excel.Range oRange = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[64 + dt.Rows.Count - 10, 13];
				float Left = (float)((double)oRange.Left);
				float Top = (float)((double)oRange.Top);
				worksheet.Shapes.AddPicture(imgUrl, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left, Top, 130, 40);

				//}
				//else if (dt.Rows.Count <= 10)
				//{

				//	if (subject.ToLower().StartsWith("for"))
				//	{
				//		worksheet.Cells[19, 10] = "SPARES " + subject.Trim();
				//	}
				//	else
				//	{
				//		worksheet.Cells[19, 10] = "SPARES FOR " + subject.Trim();
				//	}

				//	worksheet.Cells[20, 10] = "";
				//	worksheet.Cells[21, 10] = "";
				//	worksheet.Cells[22, 10] = "";
				//	worksheet.Cells[23, 10] = "";
				//	worksheet.Cells[24, 10] = "";
				//	worksheet.Cells[25, 10] = "";
				//	worksheet.Cells[26, 10] = "";
				//	worksheet.Cells[27, 10] = "";
				//	worksheet.Cells[28, 10] = "";


				//	// 출력일자
				//	worksheet.Cells[61, 10] = DateTime.Now.ToString("yyyy-MM-dd");


				//	// 담당자
				//	worksheet.Cells[66, 4] = nmUser + ", SALES STAFF";


				//	// 사인
				//	client = new WebClient();


				//	Microsoft.Office.Interop.Excel.Range oRange = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[64, 13];
				//	float Left = (float)((double)oRange.Left);
				//	float Top = (float)((double)oRange.Top);
				//	worksheet.Shapes.AddPicture(imgUrl, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left, Top, 130, 40);
				//}

				// excel 다른이름으로 저장해서 pdf 형식으로...
				//Excel.XlFixedFormatType paramExportFormat = Excel.XlFixedFormatType.xlTypePDF;
				//Excel.XlFixedFormatQuality paramExportQuality = Excel.XlFixedFormatQuality.xlQualityStandard;
				//bool paramOpenAfterPublish = false;
				//bool paramIncludeDocProps = false;		// true
				//bool paramIgnorePrintAreas = false;		// true
				//object paramFromPage = Type.Missing;
				//object paramToPage = Type.Missing;

				//workbook.ExportAsFixedFormat(paramExportFormat,
				//			savePdfName, paramExportQuality,
				//			paramIncludeDocProps, paramIgnorePrintAreas, paramFromPage,
				//			paramToPage, paramOpenAfterPublish,
				//			missing);



				// 다른이름으로 저장 부분
				workbook.SaveAs(Filename: saveFileName);
				workbook.Close(false, Type.Missing, Type.Missing);

				eApplication = new Excel.Application();     //Execute Excel
				eApplication.Visible = false;

				eWorkBook = eApplication.Workbooks.Open(saveFileName);
				eSheets = eWorkBook.Sheets;
				eWorkSheet = eSheets["SDoC"] as Excel.Worksheet;

				/*Excel to PDF*/

				object paramMissing = Type.Missing;
				string paramExportFilePath = savePdfName;
				//string paramExportFilePath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
				//								+ @"\1_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".pdf";
				Excel.XlFixedFormatType paramExportFormat = Excel.XlFixedFormatType.xlTypePDF;
				Excel.XlFixedFormatQuality paramExportQuality = Excel.XlFixedFormatQuality.xlQualityStandard;
				bool paramOpenAfterPublish = false;
				bool paramIncludeDocProps = true;
				bool paramIgnorePrintAreas = true;
				object paramFromPage = Type.Missing;
				object paramToPage = Type.Missing;

				eWorkBook.ExportAsFixedFormat(paramExportFormat,
							paramExportFilePath, paramExportQuality,
							paramIncludeDocProps, paramIgnorePrintAreas, paramFromPage,
							paramToPage, paramOpenAfterPublish,
							paramMissing);
				eWorkBook.Close(false);
				eWorkBook = null;

				eApplication.Quit();
				eApplication = null;


				//returnStr = "출력이 완료되었습니다.";
			}
			catch (Exception ee)
			{
				if (workbook != null)
				{
					workbook.Close(false);
					workbook = null;
				}

				if (application != null)
				{
					application.Quit();
					application = null;
				}

				if (eWorkBook != null)
				{
					eWorkBook.Close(false);
					eWorkBook = null;
				}


				if (eApplication != null)
				{
					eApplication.Quit();
					eApplication = null;
				}

				MsgControl.ShowMsg(ee.ToString());
			}
			finally
			{
				//workbook.Close(false);
				//workbook = null;

				//application.Quit();
				//application = null;




				////ReleaseExcelObject(workbook);
				////ReleaseExcelObject(worksheet);
				////ReleaseExcelObject(application);

				////workbook = null;
				////if (application != null)
				////{
				////	Process[] pProcess;
				////	pProcess = System.Diagnostics.Process.GetProcessesByName("Excel");
				////	pProcess[0].Kill();
				////}
				////application = null;
			}
			return MD_EXCEL(fileNo);
			//return returnStr;
		}

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
				string itemName = string.Empty;


				string query = string.Empty;
				query = string.Format(@"DECLARE @URL	NVARCHAR(100)
SET @URL = (SELECT BUSINESS_HOST_SERVER FROM CM_SERVER_CONFIG WHERE SERVER_KEY = 'DINTEC2') + '/shared/image/human/sign/{0}/'

SELECT 
	QTNL.NO_FILE, 
	QTNL.NM_SUBJECT, 
	REPLACE(REPLACE(QTNL.NM_ITEM_PARTNER, CHAR(10), ''), CHAR(13), '') AS NM_ITEM_PARTNER, 
	QTNL.CD_ITEM_PARTNER,
	SOL.NO_SO, 
	SOL.UNIT_SO,
	SOH.NO_EMP,
	EMP.NM_ENG, 
	EMP.NO_EMAIL,
	EMP.NO_TEL,
	SOH.NO_PO_PARTNER, 
	PA.LN_PARTNER,
	SOL.QT_SO,
	HULL.NM_VESSEL,
	@URL + VEMP.DC_SIGN AS SIGN_URL
FROM SA_SOL AS SOL
JOIN CZ_SA_QTNL AS QTNL ON QTNL.CD_COMPANY = SOL.CD_COMPANY AND QTNL.NO_FILE = SOL.NO_SO AND QTNL.NO_LINE = SOL.SEQ_SO
JOIN SA_SOH AS SOH ON SOH.CD_COMPANY = SOL.CD_COMPANY AND SOH.NO_SO = SOL.NO_SO
JOIN MA_EMP AS EMP ON EMP.CD_COMPANY = SOH.CD_COMPANY AND EMP.NO_EMP = SOH.NO_EMP
JOIN V_CZ_MA_EMP AS VEMP ON VEMP.CD_COMPANY = SOH.CD_COMPANY AND VEMP.NO_EMP = SOH.NO_EMP
JOIN CZ_MA_PARTNER AS PA ON PA.CD_COMPANY = SOH.CD_COMPANY AND PA.CD_PARTNER = SOH.CD_PARTNER 
JOIN CZ_MA_HULL AS HULL ON HULL.NO_IMO = SOH.NO_IMO
WHERE NO_FILE = '{1}'
AND SOL.CD_COMPANY = '{2}'
ORDER BY SOL.SEQ_SO", CD_COMPANY, fileNo, CD_COMPANY);
				DataTable dt = DBMgr.GetDataTable(query);

				no_po_partner = dt.Rows[0]["NO_PO_PARTNER"].ToString().Trim();
				//saveFileName = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\MD_" + fileNo + "_" + no_po_partner + ".xls";
				//savePdfName = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\MD_" + fileNo + "_" + dt.Rows[0]["NO_PO_PARTNER"].ToString().Trim() + ".pdf";
				//savePdfName = "P:\\MD_" + fileNo + "_" + dt.Rows[0]["NO_PO_PARTNER"].ToString().Trim() + ".pdf";
				if (CD_COMPANY.Contains("S100") || NO_EMP.Equals("SYSADMIN"))
				{
					saveFileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\MD_" + fileNo + "_" + dt.Rows[0]["NO_PO_PARTNER"].ToString().Replace(" ", "_").Replace("-", "_").Replace("/", "_").Replace(":", "").Replace(";", "").Trim() + ".xls";
					savePdfName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\MD_" + fileNo + "_" + dt.Rows[0]["NO_PO_PARTNER"].ToString().Replace(" ", "_").Replace("-", "_").Replace("/", "_").Replace(":", "").Replace(";", "").Trim() + ".pdf";
				}
				else
				{
					saveFileName = "P:\\MD_" + fileNo + "_" + dt.Rows[0]["NO_PO_PARTNER"].ToString().Replace(" ", "_").Replace("-", "_").Replace("/", "_").Replace(":", "").Replace(";", "").Trim() + ".xls";
					savePdfName = "P:\\MD_" + fileNo + "_" + dt.Rows[0]["NO_PO_PARTNER"].ToString().Replace(" ", "_").Replace("-", "_").Replace("/", "_").Replace(":", "").Replace(";", "").Trim() + ".pdf";
				}

				//saveFileName = "P:\\MD_" + fileNo + "_" + dt.Rows[0]["NO_PO_PARTNER"].ToString().Replace(" ", "_").Replace("-", "_").Replace("/", "_").Replace(":", "").Replace(";", "").Trim() + ".xls";
				nmUser = dt.Rows[0]["NM_ENG"].ToString().Trim();
				cdPartner = dt.Rows[0]["LN_PARTNER"].ToString().Trim();
				imgUrl = dt.Rows[0]["SIGN_URL"].ToString().Trim();
				noEmail = dt.Rows[0]["NO_EMAIL"].ToString().Trim();
				noTel = dt.Rows[0]["NO_TEL"].ToString().Trim();
				nmVessel = dt.Rows[0]["NM_VESSEL"].ToString().Trim();
				subject = dt.Rows[0]["NM_SUBJECT"].ToString().Trim();
				

				if (CD_COMPANY.Contains("S100"))
				{
					excelFileName = @"C:\\ERPU\\Browser\\MD_S100.xls";
				}
				else if (CD_COMPANY.Contains("K100"))
				{
					excelFileName = @"C:\\ERPU\\Browser\\MD.xls";
				}
				else if (CD_COMPANY.Contains("K200"))
				{
					excelFileName = @"C:\\ERPU\\Browser\\MD_K200.xls";
				}


				object missing = System.Reflection.Missing.Value;
				application = new Excel.Application();
				workbook = application.Workbooks.Open(excelFileName, missing, missing
					, missing, missing, missing, missing, missing, missing
					, missing, missing, missing, missing, missing, missing);
				worksheet = (Excel.Worksheet)workbook.Sheets["MD"];


				// 출력일자
				worksheet.Cells[6, 5] = DateTime.Now.ToString("yyyy-MM-dd");



				// 파일번호
				worksheet.Cells[17, 21] = "SDoC-" + fileNo;
				worksheet.Cells[9, 5] = fileNo;

				// 매출처
				worksheet.Cells[12, 5] = cdPartner;
				worksheet.Cells[13, 5] = nmVessel;
				worksheet.Cells[14, 5] = no_po_partner;




				// 영업담당자
				worksheet.Cells[13, 21] = nmUser;

				double outValue = 0;
				double resultValue = 0;

				for (int r = 0; r < dt.Rows.Count; r++)
				{
					double.TryParse(dt.Rows[r]["QT_SO"].ToString(), out outValue);
					resultValue += outValue;
				}

				string countNo = string.Format("{0:0}", resultValue);

				// 총 수량
				//worksheet.Cells[22, 15] = countNo;

				if (CD_COMPANY.Contains("S100"))
				{
					if (noTel.StartsWith("0"))
					{
						worksheet.Cells[14, 21] = noTel.Substring(1, noTel.Length - 1).Trim();
					}
					else
					{
						// 전화번호
						worksheet.Cells[14, 21] = noTel;
					}
				}
				else
				{
					if (noTel.StartsWith("0"))
					{
						worksheet.Cells[14, 21] = "82-" + noTel.Substring(1, noTel.Length - 1).Trim();
					}
					else
					{
						// 전화번호
						worksheet.Cells[14, 21] = "82-" + noTel;
					}
				}


				// 메일
				worksheet.Cells[16, 21] = noEmail;

				subject = subject.Replace("\r\n", " ").Trim();


				for (int r = 0; r < dt.Rows.Count; r++)
				{
					itemName = dt.Rows[r]["NM_ITEM_PARTNER"].ToString().Trim();

					Range line = (Range)worksheet.Rows[22 + r];
					line.Insert();

					

					Range lineColor = (Range)worksheet.Range[worksheet.Cells[22 + r, 2], worksheet.Cells[22 + r, 33]];
					lineColor.Interior.Color = Color.FromArgb(255, 255, 153);
					lineColor.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;


					Range productNm = (Range)worksheet.Range[worksheet.Cells[22 + r, 2], worksheet.Cells[22 + r, 8]];
					productNm.Merge(true);
					productNm.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
					productNm.Font.Size = 9;

					Range productNo = (Range)worksheet.Range[worksheet.Cells[22 + r, 9], worksheet.Cells[22 + r, 14]];
					productNo.Merge(true);
					productNo.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

					Range amount = (Range)worksheet.Range[worksheet.Cells[22 + r, 15], worksheet.Cells[22 + r, 16]];
					amount.Merge(true);
					amount.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;


					Range unit = (Range)worksheet.Range[worksheet.Cells[22 + r, 17], worksheet.Cells[22 + r, 18]];
					unit.Merge(true);
					unit.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

					Range productIf = (Range)worksheet.Range[worksheet.Cells[22 + r, 19], worksheet.Cells[22 + r, 33]];
					productIf.Merge(true);
					productIf.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
					productIf.Font.Size = 9;
					//productIf.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
					//productIf.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;


					worksheet.Cells[22 + r, 9] = dt.Rows[r]["CD_ITEM_PARTNER"].ToString().Trim();
					worksheet.Cells[22 + r, 15] = dt.Rows[r]["QT_SO"].ToString().Trim();
					worksheet.Cells[22 + r, 17] = dt.Rows[r]["UNIT_SO"].ToString().Trim();
					//worksheet.Cells[22 + r, 19] = dt.Rows[r]["NM_ITEM_PARTNER"].ToString().Trim();


					//if(dt.Rows[r]["NM_ITEM_PARTNER"].ToString())

					// 주제

					if (itemName.Length > 30 && itemName.Length < 90)
					{

						string firstStr = itemName.Substring(29, itemName.Length - 29).Trim();

						worksheet.Cells[22 + r, 2] = itemName.Substring(0, 29).Trim();

						if (firstStr.StartsWith("="))
							worksheet.Cells[22 + r, 19] = itemName.Replace("=",":").Substring(29, itemName.Length - 29).Trim();
						else
							worksheet.Cells[22 + r, 19] = itemName.Substring(29, itemName.Length - 29).Trim();
					}
					else if(itemName.Length <= 30)
					{
						worksheet.Cells[22 + r, 2] = itemName.Trim();
					}
					else
					{
						string firstStr = itemName.Substring(29, itemName.Length - 29).Trim();

						worksheet.Cells[22 + r, 2] = itemName.Substring(0, 29).Trim();

						if (firstStr.StartsWith("="))
							worksheet.Cells[22 + r, 19] = itemName.Replace("=", ":").Substring(29, itemName.Length - 29).Trim();
						else
							worksheet.Cells[22 + r, 19] = itemName.Substring(29, 60).Trim();


					}
				}

				Range bottomDate = (Range)worksheet.Range[worksheet.Cells[73 + dt.Rows.Count, 1], worksheet.Cells[73 + dt.Rows.Count, 3]];
				bottomDate.Merge(true);
				bottomDate.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

				// 출력일자 (하단)
				worksheet.Cells[73 + dt.Rows.Count, 1] = DateTime.Now.ToString("yyyy-MM-dd");


				// 사인
				client = new WebClient();

				Microsoft.Office.Interop.Excel.Range oRange = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[72 + dt.Rows.Count - 1, 7];
				float Left = (float)((double)oRange.Left);
				float Top = (float)((double)oRange.Top);
				worksheet.Shapes.AddPicture(imgUrl, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left, Top, 130, 36);



				//Excel.XlFixedFormatType paramExportFormat = Excel.XlFixedFormatType.xlTypePDF;
				//Excel.XlFixedFormatQuality paramExportQuality = Excel.XlFixedFormatQuality.xlQualityStandard;
				//bool paramOpenAfterPublish = false;
				//bool paramIncludeDocProps = false;
				//bool paramIgnorePrintAreas = false;
				//object paramFromPage = Type.Missing;
				//object paramToPage = Type.Missing;

				//workbook.ExportAsFixedFormat(paramExportFormat,
				//			savePdfName, paramExportQuality,
				//			paramIncludeDocProps, paramIgnorePrintAreas, paramFromPage,
				//			paramToPage, paramOpenAfterPublish,
				//			missing);


				//// 저장 부분
				workbook.SaveAs(Filename: saveFileName);
				workbook.Close(false, Type.Missing, Type.Missing);


				


				eApplication = new Excel.Application();     //Execute Excel
				eApplication.Visible = false;

				eWorkBook = eApplication.Workbooks.Open(saveFileName);
				eSheets = eWorkBook.Sheets;
				eWorkSheet = eSheets["MD"] as Excel.Worksheet;

				/*Excel to PDF*/

				object paramMissing = Type.Missing;
				string paramExportFilePath = savePdfName;
				Excel.XlFixedFormatType paramExportFormat = Excel.XlFixedFormatType.xlTypePDF;
				Excel.XlFixedFormatQuality paramExportQuality = Excel.XlFixedFormatQuality.xlQualityStandard;
				bool paramOpenAfterPublish = false;
				bool paramIncludeDocProps = true;
				bool paramIgnorePrintAreas = true;
				object paramFromPage = Type.Missing;
				object paramToPage = Type.Missing;

				eWorkBook.ExportAsFixedFormat(paramExportFormat,
							paramExportFilePath, paramExportQuality,
							paramIncludeDocProps, paramIgnorePrintAreas, paramFromPage,
							paramToPage, paramOpenAfterPublish,
							paramMissing);
				eWorkBook.Close(false);
				eWorkBook = null;

				eApplication.Quit();
				eApplication = null;


				returnStr = "출력이 완료되었습니다.";
			}
			catch (Exception ee)
			{
				if (workbook != null)
				{
					workbook.Close(false);
					workbook = null;
				}

				if (application != null)
				{
					application.Quit();
					application = null;
				}

				if(eWorkBook != null)
				{
					eWorkBook.Close(false);
					eWorkBook = null;
				}


				if (eApplication != null)
				{
					eApplication.Quit();
					eApplication = null;
				}

				MsgControl.ShowMsg(ee.ToString());
			}
			finally
			{
				//workbook.Close(false);
				//workbook = null;

				//application.Quit();
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


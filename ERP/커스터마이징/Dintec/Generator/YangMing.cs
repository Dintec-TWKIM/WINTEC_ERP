using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GemBox.Spreadsheet;
using System.Data;

namespace Dintec.Generator
{
	public class YangMing
	{
		public static string GetExcel(string fileNumber, string fullPathFile, DataTable dataTable)
		{
			SpreadsheetInfo.SetLicense("EAAN-UCCU-1F8C-X668");


			ExcelFile excel = ExcelFile.Load(fullPathFile);


			// 기본시트 불러오기
			ExcelWorksheet sheet = excel.Worksheets[0];

			// 
			int itemStartRow = 0;

			for (int i = 0; i < sheet.Rows.Count; i++)
			{
				for (int j = 0; j < sheet.Columns.Count; j++)
				{
					string str = GetTo.String(sheet.Cells[i, j].Value);

					
					if (str == "Estimate Delivery Port :") sheet.Cells[i, j + 1].Value = "EX";
					else if (str == "Vendor’s Ref.(Quotation No.):") sheet.Cells[i, j + 1].Value = fileNumber;

					if (str == "No.")
					{
						itemStartRow = i;
						break;
					}
				}

				if (itemStartRow > 0) break;
			}

			int cUnitPrice = 8;
			int cDiscount = 9;



			for (int i = 10; i < sheet.Rows.Count; i++)
			{
				//string no = GetTo.String(sheet.Cells[i, j].Value); 
				

			}


			return "";
		}
	}
}

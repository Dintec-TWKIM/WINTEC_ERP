using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

using Duzon.Common.Forms;
using GemBox.Spreadsheet;


namespace DX
{
	public class 엑셀
	{
		public string 파일이름_경로포함 { get; set; }

		public string 파일이름 => 파일.파일이름(파일이름_경로포함);

		/// <summary>
		/// 엑셀의 행번호를 입력 (1부터 시작), 0이면 컬럼이름 만들지 않음
		/// </summary>
		public int 헤더행 { get; set; } = 1;

		/// <summary>
		/// 엑셀의 행번호를 입력 (1부터 시작)
		/// </summary>
		public int 데이터행 { get; set; } = 2;

		public bool 대문자변환 { get; set; } = false;

		public DataTable 데이터테이블 { get; set; }

		public DataRowCollection 시트 => 데이터테이블.Rows;

		public 엑셀() {	}

		public 엑셀(string 파일이름_경로포함) => this.파일이름_경로포함 = 파일이름_경로포함;

		/// <summary>
		/// 파일 선택창 팝업 후 파일선택하면 엑셀파일 읽어서 데이터테이블까지 만듬
		/// </summary>
		/// <returns>성공하면 true</returns>
		public bool 파일선택()
		{
			OpenFileDialog f = new OpenFileDialog { Filter = Global.MainFrame.DD("엑셀 파일") + "|*.xls;*.xlsx" };

			if (f.ShowDialog() == DialogResult.OK)
			{
				파일이름_경로포함 = f.FileName;
				return true;
			}
			else
				return false;
		}

		public void 읽기()
		{
			SpreadsheetInfo.SetLicense("EAAN-UCCU-1F8C-X668");

			// 엑셀파일 불러오기
			ExcelFile 엑셀 = ExcelFile.Load(파일이름_경로포함, LoadOptions.XlsxDefault);
			ExcelWorksheet 시트 = 엑셀.Worksheets[0];

			// 데이터 테이블 변환 옵션
			CreateDataTableOptions 옵션 = new CreateDataTableOptions();
			옵션.ColumnHeaders = 헤더행 > 0;
			옵션.StartRow = 헤더행 == 0 ? 0 : 헤더행 - 1;
			옵션.NumberOfColumns = 시트.GetUsedCellRange(false).LastColumnIndex + 1;
			옵션.NumberOfRows = 시트.GetUsedCellRange(false).LastRowIndex + 1;

			// 데이터테이블 변환
			데이터테이블 = 시트.CreateDataTable(옵션);
		}

		

		//public DataTable 읽기()
		//{
		//	Excel.Application app = new Excel.Application();
		//	Excel.Workbook workbook = app.Workbooks.Open(파일이름, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
		//	Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets.get_Item(1);
		//	Excel.Range range = worksheet.UsedRange;

		//	//worksheet.Cells.Find("*", worksheet.Range("A1"), xlf)


		//	데이터테이블 = new DataTable();
		//	object[,] data = range.Value as object[,];

		//	// 컬럼 추가
		//	for (int j = 1; j <= data.GetLength(1); j++)
		//	{
		//		데이터테이블.컬럼추가(헤더행 == 0 ? "Column" + j : data[헤더행, j].문자().트림());
		//	}

		//	// 데이터 바인딩
		//	for (int i = 데이터행; i <= data.GetLength(0); i++)
		//	{
		//		데이터테이블.Rows.Add();

		//		for (int j = 1; j <= data.GetLength(1); j++)
		//		{
		//			string str = data[i, j].문자().트림();

		//			if (str != "")
		//				데이터테이블.Rows[데이터테이블.Rows.Count - 1][j - 1] = 대문자변환 ? str.대문자() : str;
		//		}
		//	}

		//	workbook.Close(false);
		//	app.Quit();

		//	return 데이터테이블.Copy();
		//}





		//public void ReadFirstColumn()
		//{
		//	Excel.Application app = new Excel.Application();
		//	Excel.Workbook workbook = app.Workbooks.Open(파일이름);
		//	Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets.get_Item(1);
		//	Excel.Range range = worksheet.UsedRange;

		//	데이터테이블 = new DataTable();
		//	object[,] data = range.Value as object[,];

		//	// 컬럼 1개만 추가
		//	데이터테이블.Columns.Add();

		//	// 데이터 바인딩
		//	for (int i = 1; i <= data.GetLength(0); i++)
		//	{
		//		string str = data[i, 1].ToString2().Trim();
		//		if (str != "")
		//			데이터테이블.Rows.Add(str);
		//	}

		//	workbook.Close(false);
		//	app.Quit();
		//}








	}
}

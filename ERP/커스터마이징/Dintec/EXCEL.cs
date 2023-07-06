using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

using Duzon.Common.Forms;
using GemBox.Spreadsheet;

namespace Dintec
{
	public class EXCEL
	{
		public string FileName { get; set; }

		public string SafeFileName { get; set; }

		/// <summary>
		/// 엑셀의 행번호를 입력 (1부터 시작), 0이면 컬럼이름 만들지 않음
		/// </summary>
		public int HeaderRowIndex { get; set; }

		/// <summary>
		/// 엑셀의 행번호를 입력 (1부터 시작)
		/// </summary>
		public int StartDataIndex { get; set; }

		public bool ToUpper { get; set; } = false;

		public DataTable Data { get; set; }

		public DataRowCollection Sheet
		{
			get
			{
				return Data.Rows;
			}
		}

		public EXCEL()
		{
			HeaderRowIndex = 1;
			StartDataIndex = 2;
		}

		public EXCEL(string fileName)
		{
			FileName = fileName;
			HeaderRowIndex = 1;
			StartDataIndex = 2;
		}

		public DialogResult OpenDialog()
		{
			OpenFileDialog f = new OpenFileDialog() { Filter = Global.MainFrame.DD("엑셀 파일") + "|*.xls;*.xlsx" };

			if (f.ShowDialog() == DialogResult.OK)
			{
				FileName = f.FileName;
				SafeFileName = f.SafeFileName;
				return DialogResult.OK;
			}
			else
				return DialogResult.Cancel;
		}

		//public void TEST()
		//{
		//	//ExcelFile excel = ExcelFile.Load(ExcelFileName);
		//	//excel.Save()
		//}

		public DataTable Read()
		{
			Excel.Application app = new Excel.Application();
			Excel.Workbook workbook = app.Workbooks.Open(FileName);
			Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets.get_Item(1);
			Excel.Range range = worksheet.UsedRange;

			Data = new DataTable();
			object[,] data = range.Value as object[,];

			// 컬럼 추가
			for (int j = 1; j <= data.GetLength(1); j++)
			{
				string colName = HeaderRowIndex == 0 ? "" : data[HeaderRowIndex, j].ToString2().Trim();

				if (colName == "")
					Data.Columns.Add("Column" + j);
				else
					Data.Columns.Add(colName);
			}

			// 데이터 바인딩
			for (int i = StartDataIndex; i <= data.GetLength(0); i++)
			{
				Data.Rows.Add();

				for (int j = 1; j <= data.GetLength(1); j++)
				{
					string str = data[i, j].ToString2().Trim();

					if (str != "")
						Data.Rows[Data.Rows.Count - 1][j - 1] = ToUpper ? str.ToUpper() : str;
				}
			}

			workbook.Close(false);
			app.Quit();

			return Data.Copy();
		}

		public DataTable Read2()
		{
			Excel.Application app = new Excel.Application();
			Excel.Workbook workbook = app.Workbooks.Open(FileName);
			Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets.get_Item(2);
			Excel.Range range = worksheet.UsedRange;

			Data = new DataTable();
			object[,] data = range.Value as object[,];

			// 컬럼 추가
			for (int j = 1; j <= data.GetLength(1); j++)
			{
				string colName = HeaderRowIndex == 0 ? "" : data[HeaderRowIndex, j].ToString2().Trim();

				if (colName == "")
					Data.Columns.Add("Column" + j);
				else
					Data.Columns.Add(colName);
			}

			// 데이터 바인딩
			for (int i = StartDataIndex; i <= data.GetLength(0); i++)
			{
				Data.Rows.Add();

				for (int j = 1; j <= data.GetLength(1); j++)
				{
					string str = data[i, j].ToString2().Trim();
					if (str != "")
						Data.Rows[Data.Rows.Count - 1][j - 1] = str;
				}
			}

			worksheet.SaveAs(@"d:\abc.xlsx");
			workbook.Close(false);
			app.Quit();

			
			return Data.Copy();
		}

		public void ReadFirstColumn()
		{
			Excel.Application app = new Excel.Application();
			Excel.Workbook workbook = app.Workbooks.Open(FileName);
			Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets.get_Item(1);
			Excel.Range range = worksheet.UsedRange;

			Data = new DataTable();
			object[,] data = range.Value as object[,];

			// 컬럼 1개만 추가
			Data.Columns.Add();

			// 데이터 바인딩
			for (int i = 1; i <= data.GetLength(0); i++)
			{
				string str = data[i, 1].ToString2().Trim();
				if (str != "")
					Data.Rows.Add(str);
			}			

			workbook.Close(false);
			app.Quit();
		}










		public static DataTable ReadFile()
		{
			// 엑셀 파일 불러오기
			OpenFileDialog f = new OpenFileDialog
			{
				Filter = Global.MainFrame.DD("엑셀 파일") + "|*.xls;*.xlsx"
			};

			if (f.ShowDialog() != DialogResult.OK)
				return null;

			// 읽기
			DataTable dtExcel = Read(f.FileName);

			if (dtExcel.Rows.Count == 0)
				return null;
			else
				return dtExcel;
		}

		public static DataTable Read(string fileName)
		{
			Excel.Application app = new Excel.Application();
			Excel.Workbook workbook = app.Workbooks.Open(fileName);
			Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets.get_Item(1);
			Excel.Range range = worksheet.UsedRange;

			DataTable dt = new DataTable();

			try
			{
				object[,] data = range.Value as object[,];
				dt.Columns.Add();

				for (int i = 1; i <= data.GetLength(0); i++)
				{
					string str = GetTo.String(data[i, 1]).Trim();
					if (str != "")
						dt.Rows.Add(str);
				}
			}
			catch (Exception ex)
			{
				Util.ShowMessage(Util.GetErrorMessage(ex.Message));
			}

			workbook.Close(false);
			app.Quit();

			return dt;
		}

	}
}

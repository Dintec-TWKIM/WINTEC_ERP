using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using Excel = Microsoft.Office.Interop.Excel;

namespace Dintec
{
	public class ExcelReader
	{
		public DataTable Read(string fileName)
		{
			Excel.Application app = new Excel.Application();
			Excel.Workbook workbook = app.Workbooks.Open(fileName);
			Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets.get_Item(1);
			Excel.Range range = worksheet.UsedRange;

			DataTable dt = new DataTable();

			try
			{
				//object[,] data = range.Value;
				object[,] data = range.Value as object[,];
				dt.Columns.Add();

				for (int i = 1; i <= data.GetLength(0); i++)
				{
					string str = GetTo.String(data[i, 1]).Trim();
					if (str != "") dt.Rows.Add(str);
				}
			}
			catch (Exception ex)
			{
				//Util.ShowMessage(Util.GetErrorMessage(ex.Message));
			}

			workbook.Close(false);
			app.Quit();

			return dt;
		}

		public DataTable Read(string fileName, int headerRowIndex, int startDataIndex)
		{
			Excel.Application app = new Excel.Application();
			Excel.Workbook workbook = app.Workbooks.Open(fileName);
			Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets.get_Item(1);
			Excel.Range range = worksheet.UsedRange;

			DataTable dt = new DataTable();

			//try
			//{
			object[,] data = range.Value as object[,];

			// 컬럼 추가
			for (int j = 1; j <= data.GetLength(1); j++)
			{
				string colName = GetTo.String(data[headerRowIndex, j]).Trim();

				if (colName == "")
					dt.Columns.Add("Column" + j);
				else
					dt.Columns.Add(colName);
			}

			// 데이터 바인딩
			for (int i = startDataIndex; i <= data.GetLength(0); i++)
			{
				dt.Rows.Add();

				for (int j = 1; j <= data.GetLength(1); j++)
				{
					string str = GetTo.String(data[i, j]).Trim();
					if (str != "") dt.Rows[dt.Rows.Count - 1][j - 1] = str;
				}
			}
			//}
			//catch (Exception ex)
			//{
			//    Util.ShowMessage(Util.GetErrorMessage(ex.Message));
			//}

			workbook.Close(false);
			app.Quit();

			return dt;
		}

		public static DataTable Read2(string fileName, int headerRowIndex, int startDataIndex)
		{
			Excel.Application app = new Excel.Application();
			Excel.Workbook workbook = app.Workbooks.Open(fileName);
			Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets.get_Item(1);
			Excel.Range range = worksheet.UsedRange;

			DataTable dt = new DataTable();

			object[,] data = range.Value as object[,];

			// 컬럼 추가
			for (int j = 1; j <= data.GetLength(1); j++)
			{
				string colName = GetTo.String(data[headerRowIndex, j]).Trim();

				if (colName == "")
					dt.Columns.Add("Column" + j);
				else
					dt.Columns.Add(colName);
			}

			// 데이터 바인딩
			for (int i = startDataIndex; i <= data.GetLength(0); i++)
			{
				dt.Rows.Add();

				for (int j = 1; j <= data.GetLength(1); j++)
				{
					string str = GetTo.String(data[i, j]).Trim();
					if (str != "") dt.Rows[dt.Rows.Count - 1][j - 1] = str;
				}
			}

			workbook.Close(false);
			app.Quit();

			return dt;
		}

		public static DataSet ToDataSet(string fileName)
		{
			DataSet ds = new DataSet();
			Excel.Application app = new Excel.Application();
			Excel.Workbook workbook = app.Workbooks.Open(fileName);

			try
			{
				foreach (Excel.Worksheet sheet in workbook.Worksheets)
				{
					Excel.Range range = sheet.UsedRange;

					object[,] data = null;

					//string test = range.Value2.GetType().ToString();

					if (range.Value2.GetType().ToString() != null)
					{

						if (range.Value2.GetType().ToString() == "System.Object[,]")
						{
							data = (object[,])range.Value2;
						}

						DataTable dt = new DataTable();

						if (data != null)
						{
							// 컬럼 추가
							for (int j = 1; j <= data.GetLength(1); j++)
							{
								dt.Columns.Add();
							}
							//int test1 = data.GetLength(0);
							// 데이터 바인딩
							for (int i = 1; i <= data.GetLength(0); i++)
							{
								dt.Rows.Add();

								//  int test = data.GetLength(1);

								for (int j = 1; j <= data.GetLength(1); j++)
								{
									string str = GetTo.String(data[i, j]).Trim();
									if (str != "") dt.Rows[dt.Rows.Count - 1][j - 1] = str;
								}
							}

							// DataSet에 추가
							ds.Tables.Add(dt);
						}
					}
				}
			}
			catch (Exception ex)
			{
				//Util.ShowMessage(Util.GetErrorMessage(ex.Message));
			}

			workbook.Close(false);
			app.Quit();
			return ds;
		}

		public static ArrayList ToDataTableArray(string fileName)
		{
			ArrayList dtArray = new ArrayList();

			// OleDb를 이용하여 엑셀 파일 읽기
			OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=\"Excel 12.0;HDR=No;IMEX=1\"");
			conn.Open();

			OleDbCommand cmd = new OleDbCommand();
			cmd.Connection = conn;

			OleDbDataAdapter da = new OleDbDataAdapter();
			da.SelectCommand = cmd;

			// 엑셀 시트 읽어오기
			DataTable dtSheet = conn.GetSchema("TABLES");

			foreach (DataRow row in dtSheet.Rows)
			{
				DataTable dt = new DataTable();

				cmd.CommandText = "select * from [" + row["TABLE_NAME"] + "]";
				da.SelectCommand = cmd;
				da.Fill(dt);

				dtArray.Add(dt);
			}

			conn.Close();
			return dtArray;
		}
	}
}

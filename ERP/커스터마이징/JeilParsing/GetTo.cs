using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace OutParsing
{
	public class GetTo
	{
		public static int Int(object value)
		{
			Regex alphabet = new Regex("[a-zA-Z]");

			if (value == null || value.ToString() == "")
				return 0;
			else if (alphabet.IsMatch(value.ToString()))
				return 0;
			else
			{
				string changeValue = value.ToString().Replace(",", "");
				double valueDo = Convert.ToDouble(changeValue);
				int _changeValue = Convert.ToInt32(valueDo);

				return _changeValue;
			}
		}

		public static DateTime Date(object value)
		{
			// 8글자를 변환
			string s = value.ToString();
			if (s.Length == 8) s = s.Substring(0, 4) + "-" + s.Substring(4, 2) + "-" + s.Substring(6, 2);
			return Convert.ToDateTime(s);
		}

		//public static string DateString(object value)
		//{
		//    if (value.ToString() == "") return "";
		//    return Convert.ToDateTime(value).ToShortDateString().Replace("-", "");
		//}

		//public static string GetTo_DateStringS(object value)
		//{
		//    if (value.ToString() == "") return "";
		//    return value.ToString().Substring(0, 4) + "/" + value.ToString().Substring(4, 2) + "/" + value.ToString().Substring(6, 2);
		//}

		//public static string GetToDatePrint(DateTime value)
		//{
		//    string s = string.Format("{0:yyyyMMdd}", value);
		//    return GetToDatePrint(s);
		//}

		public static string InternationalCall(object value)
		{
			string tel = value.ToString();
			if (tel.Length > 5 && tel.Substring(0, 3).IndexOf("82") < 0) tel = "+82-" + tel.Substring(1);
			return tel;
		}

		public static string DatePrint(object value)
		{
			if (value.ToString() == "")
				return "";

			string s = value.ToString().Replace("-", "");
			string yyyy = s.Substring(0, 4);
			string mm = s.Substring(4, 2);
			string dd = s.Substring(6, 2);

			if (mm == "01") mm = "JAN";
			if (mm == "02") mm = "FEB";
			if (mm == "03") mm = "MAR";
			if (mm == "04") mm = "APR";
			if (mm == "05") mm = "MAY";
			if (mm == "06") mm = "JUN";
			if (mm == "07") mm = "JUL";
			if (mm == "08") mm = "AUG";
			if (mm == "09") mm = "SEP";
			if (mm == "10") mm = "OCT";
			if (mm == "11") mm = "NOV";
			if (mm == "12") mm = "DEC";

			return dd + "/" + mm + "/" + yyyy;
		}

		public static decimal Decimal(object value)
		{
			if (value == null)
				return 0;
			
			if (decimal.TryParse(value.ToString(), out decimal d))
				return d;
			else
				return 0;
		}

		public static double Double(object value)
		{
			if (value == null || value.ToString() == "") return 0;
			return Convert.ToDouble(value);
		}

		public static string String(object value)
		{
			if (value == null) return "";
			return value.ToString();
		}

		

		public static string Money(object value)
		{
			if (value == null) return "";
			return string.Format("{0:#,##0.##}", value);
		}




		public static DataTable DataTable(DataRow[] rows)
		{
			if (rows.Length == 0)
				return null;
			else
				return rows.CopyToDataTable();
		}

		public static DataTable DataTable(DataTable table, params string[] columnNames)
		{
			return new DataView(table).ToTable(false, columnNames);
		}

		public static DataTable DataTable(DataTable table, string filter, DataViewRowState rowState, params string[] columnNames)
		{
			System.Data.DataTable dt = new DataView(table, filter, null, rowState).ToTable(false, columnNames);

			if (rowState == DataViewRowState.Deleted)
			{
				while (dt.Rows.Count > 0)
					dt.Rows[0].Delete();
			}

			//foreach (DataRow row in dt.Rows)
			//{
			//    if (rowState == DataViewRowState.Deleted)
			//        row.Delete();
			//    else if (rowState == DataViewRowState.ModifiedCurrent)
			//        row.SetModified();
			//}

			return dt;
		}

		//public static DataTable DataTable(DataTable table, string filter, string sort, DataViewRowState rowState, params string[] columnNames)
		//{
		//    if (columnNames == null || columnNames[0] == "")
		//        return new DataView(table, filter, sort, rowState).ToTable();
		//    else
		//        return new DataView(table, filter, sort, rowState).ToTable(false, columnNames);
		//}

		// -----------------------------------------------------------------------------------------------------------------------------------------------
		//public static string Xml(DataRow[] rows)
		//{
		//	return Xml(rows.CopyToDataTable(), "");
		//}

		//public static string Xml(DataTable dataTable)
		//{
		//	return Xml(dataTable, "");
		//}

		//public static string Xml(DataTable dataTable, string filter)
		//{
		//	return Xml(dataTable, "", "");
		//}

		//public static string Xml(DataTable dataTable, string filter, params string[] columnNames)
		//{
		//	if (dataTable == null)
		//		return "<XML></XML>";

		//	//DataTable dt = dataTable.Copy();

		//	//DataTable dt = GetTo.DataTable(dataTable, filter, null, columnNames);

		//	//new DataView(dataTable, filter, null, null).ToTable(false, columnNames);
		//	//DataView dv = new DataView(dataTable);
		//	//dv.RowFilter = filter;

		//	//DataTable dt;

		//	//if (columnNames[0] == "")
		//	//    dt = dv.ToTable();
		//	//else
		//	//    dt = dv.ToTable(false, columnNames);

		//	//DataTable dt = dataTable.Copy();
		//	//DataTable dtD = new DataView(dt, filter, null, DataViewRowState.Deleted).ToTable();
		//	//DataTable dtI = new DataView(dt, filter, null, DataViewRowState.Added).ToTable();
		//	//DataTable dtU = new DataView(dt, filter, null, DataViewRowState.ModifiedCurrent).ToTable();
		//	//DataTable dtO = new DataView(dt, filter, null, DataViewRowState.Unchanged).ToTable();

		//	DataTable dt = dataTable.Copy();
		//	DataTable dtD;
		//	DataTable dtI;
		//	DataTable dtU;
		//	DataTable dtO;

		//	if (columnNames[0] == "")
		//	{
		//		dtD = new DataView(dt, filter, null, DataViewRowState.Deleted).ToTable();
		//		dtI = new DataView(dt, filter, null, DataViewRowState.Added).ToTable();
		//		dtU = new DataView(dt, filter, null, DataViewRowState.ModifiedCurrent).ToTable();
		//		dtO = new DataView(dt, filter, null, DataViewRowState.Unchanged).ToTable();
		//	}
		//	else
		//	{
		//		dtD = new DataView(dt, filter, null, DataViewRowState.Deleted).ToTable(false, columnNames);
		//		dtI = new DataView(dt, filter, null, DataViewRowState.Added).ToTable(false, columnNames);
		//		dtU = new DataView(dt, filter, null, DataViewRowState.ModifiedCurrent).ToTable(false, columnNames);
		//		dtO = new DataView(dt, filter, null, DataViewRowState.Unchanged).ToTable(false, columnNames);
		//	}

		//	dt.Columns.Add("XML_FLAG", typeof(string));
		//	dtD.Columns.Add("XML_FLAG", typeof(string), "'D'");
		//	dtI.Columns.Add("XML_FLAG", typeof(string), "'I'");
		//	dtU.Columns.Add("XML_FLAG", typeof(string), "'U'");
		//	dtO.Columns.Add("XML_FLAG", typeof(string), "'O'");

		//	dt.Clear();
		//	dt.Merge(dtD);
		//	dt.Merge(dtI);
		//	dt.Merge(dtU);
		//	dt.Merge(dtO);

		//	DataSet ds = new DataSet();
		//	dt.TableName = "ROW";
		//	ds.Tables.Add(dt);
		//	return Xml(ds);
		//}

		//public static string Xml(DataTable dataTable, RowState rowState)
		//{
		//	if (dataTable == null)
		//		return "<XML></XML>";

		//	DataTable dt = dataTable.Copy();
		//	string flag = "";

		//	if (rowState == RowState.Deleted)
		//		flag = "D";
		//	else if (rowState == RowState.Added)
		//		flag = "I";
		//	else if (rowState == RowState.Modified)
		//		flag = "U";
		//	else if (rowState == RowState.Unchanged)
		//		flag = "O";

		//	dt.Columns.Add("XML_FLAG", typeof(string), "'" + flag + "'");

		//	DataSet ds = new DataSet();
		//	dt.TableName = "ROW";
		//	ds.Tables.Add(dt);
		//	return Xml(ds);
		//}

		//public static string Xml(DataTable dataTable1, DataTable dataTable2)
		//{
		//}

		//private static string Xml(DataSet ds)
		//{
		//	// 컬럼 자동 추가
		//	foreach (DataTable dt in ds.Tables)
		//	{
		//		if (dt.Rows.Count > 0)
		//		{
		//			// 필요없는 컬럼 삭제
		//			if (dt.Columns.Contains("_IS_DETAILGET"))
		//				dt.Columns.Remove("_IS_DETAILGET");

		//			// 필수 컬럼 추가
		//			if (!dt.Columns.Contains("CD_COMPANY"))
		//				dt.Columns.Add("CD_COMPANY", typeof(string), "'" + Global.MainFrame.LoginInfo.CompanyCode + "'").SetOrdinal(0);

		//			if (!dt.Columns.Contains("NO_EMP"))
		//				dt.Columns.Add("NO_EMP", typeof(string), "'" + Global.MainFrame.LoginInfo.UserID + "'").SetOrdinal(dt.Columns.Count - 2);   // 마지막 앞에 넣음

		//			if (!dt.Columns.Contains("ID_USER"))
		//				dt.Columns.Add("ID_USER", typeof(string), "'" + Global.MainFrame.LoginInfo.UserID + "'").SetOrdinal(dt.Columns.Count - 2);  // 마지막 앞에 넣음
		//		}
		//	}

		//	// XML 변환
		//	StringWriter sw = new StringWriter();
		//	ds.DataSetName = "XML";
		//	ds.WriteXml(sw, XmlWriteMode.IgnoreSchema);
		//	return sw.ToString();
		//}

	



		public static bool IsInt(object value)
		{
			string s = value.ToString();
			if (s.Length > 0 && s.Right(1) == ".") s = s.Substring(0, s.Length - 1);

			if (s.Length > 1 && s.Right(2) == ".0") s = s.Substring(0, s.Length - 2);

			if (s.Length > 2 && s.Right(3) == ".00") s = s.Substring(0, s.Length - 3);

			if (s.Length > 3 && s.Right(4) == ".000") s = s.Substring(0, s.Length - 4);

			int temp;
			return int.TryParse(s, out temp);
		}
		                                                                        
		public static string CompanyShortName(object value)
		{
			string s = value.ToString();

			return s.Replace("주식회사", "").Replace("(주)", "").Trim();
		}
	}
}

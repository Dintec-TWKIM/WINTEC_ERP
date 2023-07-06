using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace OutParsing
{
	public static class ExtensionMethods
	{
		public static DataRow GetFirstRow(this DataRow[] dataRows)
		{
			if (dataRows.Length > 0)
				return dataRows[0];
			else
				return null;
		}

		public static DataTable ToDataTable(this DataRow[] dataRows)
		{
			if (dataRows.Length > 0)
				return dataRows.CopyToDataTable();
			else
				return null;
		}

		public static DataTable ToDataTable(this DataRow[] dataRows, bool distinct, params string[] columnNames)
		{
			if (dataRows.Length > 0)
				return dataRows.ToDataTable().ToDataTable(distinct, columnNames);
			else
				return null;
		}



		public static DataTable ToDataTable(this DataTable dataTable, bool distinct, params string[] columnNames)
		{
			return new DataView(dataTable).ToTable(distinct, columnNames);
		}

		public static DataTable Copy(this DataTable dataTable, string filter)
		{
			return dataTable.Select(filter).CopyToDataTable();
		}



		public static DataTable Delete(this DataTable dataTable, string filter)
		{			
			dataTable.Select(filter).Delete();
			return dataTable;
		}
		public static void Delete(this IEnumerable<DataRow> rows)
		{
			foreach (var row in rows)
				row.Delete();
		}



		public static int ToInt(this object value)
		{
			if (value == null || value.ToString() == "")
				return 0;
			else
				return Convert.ToInt32(value);
		}

		public static bool ToBoolean(this object value)
		{
			return Convert.ToBoolean(value);
		}

		public static string ToStr(this object value)
		{
			if (value == null)
				return "";
			else
				return value.ToString();
		}

		public static string ToString2(this object value)
		{
			if (value == null)
				return "";
			else
				return value.ToString();
		}

		public static decimal ToDecimal(this object value)
		{
			if (value == null)
				return 0;

			if (decimal.TryParse(value.ToString(), out decimal d))
				return d;
			else
				return 0;
		}

		public static string ToXml(this DataRow[] o)
		{
			return o.ToDataTable().ToXml("");
		}

		public static string ToXml(this DataRow[] o, params string[] columnNames)
		{
			return o.ToDataTable().ToXml(columnNames);
		}

		public static string ToXml(this DataTable dataTable)
		{
			return dataTable.ToXml("");
		}

		public static string ToXml(this DataTable dataTable, DataRowState rowState)
		{
			DataTable dt = dataTable.Copy();
			dt.AcceptChanges();

			// 상태변경
			if (rowState == DataRowState.Added)			
				foreach (DataRow row in dt.Rows) row.SetAdded();
			else if (rowState == DataRowState.Modified)
				foreach (DataRow row in dt.Rows) row.SetModified();

			return dt.ToXml("");
		}

		public static string ToXml(this DataTable dataTable, params string[] columnNames)
		{
			if (dataTable == null)
				return "<XML />";

			// XML_FLAG 값 만들기 => 이 방법을 안쓰면 deleted 된 행에 값을 쓸 수가 없음 (XML_FLAG 값 쓰기 불가)
			DataTable dt = columnNames[0] == "" ? dataTable.Clone() : new DataView(dataTable.Clone()).ToTable(false, columnNames);
			DataTable dtD;
			DataTable dtI;
			DataTable dtU;
			DataTable dtO;

			if (columnNames[0] == "")
			{
				dtD = new DataView(dataTable, null, null, DataViewRowState.Deleted).ToTable();
				dtI = new DataView(dataTable, null, null, DataViewRowState.Added).ToTable();
				dtU = new DataView(dataTable, null, null, DataViewRowState.ModifiedCurrent).ToTable();
				dtO = new DataView(dataTable, null, null, DataViewRowState.Unchanged).ToTable();
			}
			else
			{
				dtD = new DataView(dataTable, null, null, DataViewRowState.Deleted).ToTable(false, columnNames);
				dtI = new DataView(dataTable, null, null, DataViewRowState.Added).ToTable(false, columnNames);
				dtU = new DataView(dataTable, null, null, DataViewRowState.ModifiedCurrent).ToTable(false, columnNames);
				dtO = new DataView(dataTable, null, null, DataViewRowState.Unchanged).ToTable(false, columnNames);
			}

			dt.Columns.Add("XML_FLAG", typeof(string));
			dtD.Columns.Add("XML_FLAG", typeof(string), "'D'");
			dtI.Columns.Add("XML_FLAG", typeof(string), "'I'");
			dtU.Columns.Add("XML_FLAG", typeof(string), "'U'");
			dtO.Columns.Add("XML_FLAG", typeof(string), "'O'");

			dt.Merge(dtD);
			dt.Merge(dtI);
			dt.Merge(dtU);
			dt.Merge(dtO);

			// 제어문자 삭제			
			//for (int j = 0; j < dt.Columns.Count; j++)
			//{
			//	if (dt.Columns[j].DataType == typeof(string))
			//	{
			//		for (int i = 0; i < dt.Rows.Count; i++)
			//		{
			//			Regex regex = new Regex("[\x00-\x1F\x7F]");
			//			if (regex.IsMatch(dt.Rows[i][j].ToString()))
			//				dt.Rows[i][j] = regex.Replace(dt.Rows[i][j].ToString(), "");
			//		}
			//	}
			//}			

			// 컬럼 자동 추가
			if (dt.Rows.Count > 0)
			{
				// 필요없는 컬럼 삭제
				if (dt.Columns.Contains("_IS_DETAILGET"))
					dt.Columns.Remove("_IS_DETAILGET");

				// 필수 컬럼 추가
				if (!dt.Columns.Contains("CD_COMPANY"))
					dt.Columns.Add("CD_COMPANY", typeof(string), "'K100'").SetOrdinal(0);
				
				if (!dt.Columns.Contains("ID_USER"))
					dt.Columns.Add("ID_USER", typeof(string), "'K100'").SetOrdinal(dt.Columns.Count - 2);  // 마지막 앞에 넣음
			}

			

			// 맵핑 타입 변경
			foreach (DataColumn dc in dt.Columns)
				dc.ColumnMapping = MappingType.Attribute;

			// XML 변환			
			DataSet ds = new DataSet();
			dt.TableName = "ROW";
			ds.Tables.Add(dt);

			StringWriter sw = new StringWriter();
			ds.DataSetName = "XML";
			ds.WriteXml(sw, XmlWriteMode.IgnoreSchema);

			return sw.ToString();
		}





		public static string Left(this object value, int length)
		{
			string s = value.ToString();
			return s.Substring(0, length);
		}

		public static string Left(this string value, int length)
		{
			string s = value;
			return s.Substring(0, length);
		}
		public static string Right(this object value, int length)
		{
			string s = value.ToString();
			return s.Substring(s.Length - length);
		}

		public static string Right(this string value, int length)
		{
			string s = value;
			return s.Substring(s.Length - length);
		}

		public static int Count(this string value, string str)
		{
			string s = value;
			Regex cntStr = new Regex(str);
			int count = int.Parse(cntStr.Matches(s, 0).Count.ToString());

			return count;
		}


		public static void RemoveCol(this DataColumnCollection o, string name)
		{
			if (o.Contains(name))
				o.Remove(name);			
		}


		public static bool In(this string item, params string[] items)
		{
			//if (items == null)
			//    throw new ArgumentNullException("items");
			List<string> list = new List<string>(items);

			return list.Contains(item);
		}
		

		
	}
}

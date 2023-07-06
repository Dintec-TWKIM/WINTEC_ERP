using Duzon.Common.Forms;
using Duzon.Common.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Dintec
{
	public class SQL
	{
		private readonly string connStr;
		private readonly SqlCommand cmd;
		
		 

		public static string GetConnectString()
		{
			string connStr = "Server=113.130.254.143; Database=NEOE; Uid=NEOE; Password=NEOE";

			if (ClientRepository.DatabaseCallType == "Direct")
				connStr = ClientRepository.ConString;

			return connStr;
		}

		public string Procedure
		{
			get
			{
				return cmd.CommandText;
			}
			set
			{
				cmd.CommandText = value;
				
			}
		}

		public SqlParameterCollection Parameter
		{
			get
			{
				return cmd.Parameters;
			}
		}

		public SQLDebug SQLDebug { get; set; }

		public SQL(string sqlText, SQLType sqlType)
		{
			connStr = GetConnectString();
			cmd = new SqlCommand
			{
				CommandText = sqlText
			,	CommandType = sqlType == SQLType.Procedure ? CommandType.StoredProcedure : CommandType.Text
			,	CommandTimeout = 300
			};
		}

		public SQL(string sqlText, SQLType sqlType, SQLDebug sqlDebug)
		{
			connStr = GetConnectString();
			cmd = new SqlCommand
			{
				CommandText = sqlText
			,	CommandType = sqlType == SQLType.Procedure ? CommandType.StoredProcedure : CommandType.Text
			,	CommandTimeout = 300
			};

			SQLDebug = sqlDebug;
		}


		

		// ================================================== 쿼리 실행
		public void ExecuteNonQuery()
		{
			SqlConnection conn = new SqlConnection(connStr);
			
			conn.Open();
			cmd.Connection = conn;
			cmd.ShowDebug(SQLDebug);
			cmd.ExecuteNonQuery();
			conn.Close();
		}

		public DataTable GetDataTable()
		{
			SqlConnection conn = new SqlConnection(connStr);
			SqlDataAdapter da = new SqlDataAdapter();
			DataTable dt = new DataTable();
			
			conn.Open();
			cmd.Connection = conn;
			cmd.ShowDebug(SQLDebug);
			da.SelectCommand = cmd;
			da.Fill(dt);
			conn.Close();

			return dt;
		}

		public DataSet GetDataSet()
		{
			SqlConnection conn = new SqlConnection(connStr);
			SqlDataAdapter da = new SqlDataAdapter();
			DataSet ds = new DataSet();
			
			conn.Open();
			cmd.Connection = conn;
			cmd.ShowDebug(SQLDebug);
			da.SelectCommand = cmd;
			
			da.Fill(ds);			
			conn.Close();

			return ds;
		}

		public static void ShowDebug(SQLDebug sqlDebug, SqlCommand cmd)
		{
			string s = "";

			foreach (SqlParameter p in cmd.Parameters)
			{
				if (p.Value == DBNull.Value || p.Value == null)
					s += "\r\n," + p.ParameterName + "=NULL";
				else
					s += "\r\n," + p.ParameterName + "='" + p.Value.ToString().Replace("'", "''") + "'";
			}

			// 첫번째 콤마(,) 제거
			if (s != "")
				s = " " + s.Substring(3);
			
			s = cmd.CommandText + "\r\n" +s;

			if (sqlDebug == SQLDebug.Print)
			{
				Debug.WriteLine(s);
			}
			else if (sqlDebug == SQLDebug.Popup)
			{
				if (!Global.MainFrame.LoginInfo.UserID.In("S-343", "S-391", "S-458"))
					return;

				// 팝업
				H_CZ_DEBUG_PRINT f = new H_CZ_DEBUG_PRINT(s);
				f.ShowDialog();
			}
		}

		// ================================================== 쿼리 실행
		public static void ExecuteNonQuery(string query)
		{
			SqlConnection conn = new SqlConnection(GetConnectString());
			conn.Open();

			SqlCommand cmd = new SqlCommand
			{
				CommandText = query
			,	CommandTimeout = 300
			,	CommandType = CommandType.Text
			,	Connection = conn
			};

			cmd.ExecuteNonQuery();
			conn.Close();
		}

		public static void ExecuteNonQuery(string spName, SQLDebug sqlDebug, params object[] parameters)
		{
			SqlConnection conn = new SqlConnection(GetConnectString());
			conn.Open();

			SqlCommand cmd = new SqlCommand
			{
				CommandText = spName
			,	CommandTimeout = 300
			,	CommandType = CommandType.StoredProcedure
			,	Connection = conn
			};

			cmd.SetParameter(parameters);
			ShowDebug(sqlDebug, cmd);
			cmd.ExecuteNonQuery();
			conn.Close();
		}


		public static DataTable GetDataTable(string query)
		{
			SqlCommand cmd = new SqlCommand
			{
				CommandText = query
			,	CommandType = CommandType.Text
			,	CommandTimeout = 300
			};

			SqlConnection conn = new SqlConnection(GetConnectString());
			SqlDataAdapter da = new SqlDataAdapter();
			DataTable dt = new DataTable();

			conn.Open();			
			cmd.Connection = conn;
			da.SelectCommand = cmd;
			da.Fill(dt);
			conn.Close();

			return dt;
		}


		public static DataTable GetDataTable(string spName, params object[] parameters)
		{
			return GetDataTable(spName, SQLDebug.Print, parameters);
		}

		public static DataTable GetDataTable(string spName, SQLDebug sqlDebug, params object[] parameters)
		{			
			SqlConnection conn = new SqlConnection(GetConnectString());
			SqlCommand cmd = new SqlCommand();
			SqlDataAdapter da = new SqlDataAdapter();
			DataTable dt = new DataTable();

			conn.Open();
			cmd.CommandText = spName;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 300;
			cmd.Connection = conn;
			cmd.SetParameter(parameters);
			cmd.ShowDebug(sqlDebug);
			da.SelectCommand = cmd;
			da.Fill(dt);
			conn.Close();

			return dt;
		}

		public static DataSet GetDataSet(string query)
		{
			SqlCommand cmd = new SqlCommand();
			SqlConnection conn = new SqlConnection(GetConnectString());
			SqlDataAdapter da = new SqlDataAdapter();
			DataSet ds = new DataSet();

			conn.Open();
			cmd.CommandText = query;
			cmd.CommandType = CommandType.Text;
			cmd.CommandTimeout = 300;
			cmd.Connection = conn;
			da.SelectCommand = cmd;
			da.Fill(ds);
			conn.Close();

			return ds;
		}

		public static DataSet GetDataSet(string spName, params object[] parameters)
		{
			SqlCommand cmd = new SqlCommand();
			SqlConnection conn = new SqlConnection(GetConnectString());
			SqlDataAdapter da = new SqlDataAdapter();
			DataSet ds = new DataSet();

			conn.Open();
			cmd.CommandText = spName;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 300;
			cmd.Connection = conn;
			SetParameter(cmd, parameters);
			da.SelectCommand = cmd;
			da.Fill(ds);
			conn.Close();

			return ds;
		}

		private static void SetParameter(SqlCommand cmd, object[] parameters)
		{
			SqlCommandBuilder.DeriveParameters(cmd);
			int flag = 0;

			if (cmd.Parameters.Count > 0 && cmd.Parameters[0].ParameterName == "@RETURN_VALUE")
				flag = 1;

			for (int i = 0; i < parameters.Length; i++)
				cmd.Parameters[i + flag].Value = parameters[i];
		}
	}

	//public class SQLParameter
	//{
	//	readonly Dictionary<string, object> pars = new Dictionary<string, object>();

	//	public object this[string parameterName]
	//	{
	//		set
	//		{
	//			pars[parameterName] = value;
	//		}
	//	}

	//	public SqlParameter[] Parameters
	//	{
	//		get
	//		{
	//			SqlParameter[] p = new SqlParameter[pars.Count];
	//			int i = 0;

	//			foreach (KeyValuePair<string, object> kv in pars)
	//			{
	//				p[i] = new SqlParameter();
	//				p[i].ParameterName = kv.Key;
	//				p[i].Value = kv.Value;
	//				i++;
	//			}

	//			return p;
	//		}
	//	}

	//	public void Add(string parameterName, object value)
	//	{
	//		if (value != null && value.ToString() != "") pars.Add(parameterName, value);
	//	}
	//}

	public enum SQLType
	{
		Procedure
	,	Text
	}

	public enum SQLDebug
	{
		None
	,	Print
	,	Popup
	}


	public enum SQLEmpty
	{
		Value
	}
}

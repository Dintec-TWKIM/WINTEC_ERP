using Devart.Data.Oracle;
//ystem.Diagnostics.Debug
using Duzon.Common.Forms;
using Duzon.Common.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;



namespace Dintec
{
	public class DBMgr
	{
		private DBProvider provider;

		private string connStr;
		private string text;
		private CommandType cmdType;

		private List<SqlParameter> parameters_s;    // SQL
		private List<OracleParameter> parameters_o; // 오라클

		// =================================================== Property
		public string Query
		{
			get
			{
				return text;
			}
			set
			{
				text = value;
				cmdType = CommandType.Text;
			}
		}

		public string Procedure
		{
			get
			{
				return text;
			}
			set
			{
				text = value;
				cmdType = CommandType.StoredProcedure;
			}
		}

		public DebugMode DebugMode { get; set; }

		// ================================================== Constructor
		public DBMgr()
		{
			connStr = "SQL:" + GetConnectString();
			provider = DBProvider.SqlServer;

			parameters_s = new List<SqlParameter>();
			DebugMode = DebugMode.None;
		}

		public DBMgr(string text, QueryType queryType, DebugMode debugMode)
		{
			connStr = "SQL:" + GetConnectString();
			provider = DBProvider.SqlServer;

			parameters_s = new List<SqlParameter>();
			DebugMode = debugMode;

			if (queryType == QueryType.Text)
				Query = text;
			else if (queryType == QueryType.Procedure)
				Procedure = text;
		}

		public DBMgr(DBConn dbConn)
		{
			//if (dbConn == DBConn.Old_DINTEC)	connStr = "ORA:User Id=develop; Password=pctrain1; Server=113.130.254.133; Direct=True; Sid=dintecdb; Port=1521";
			if (dbConn == DBConn.Old_DINTEC) connStr = "ORA:User Id=develop; Password=pctrain1; Server=192.168.2.128; Direct=True; Sid=dintecdb; Port=1521";
			if (dbConn == DBConn.Old_DUBHECO) connStr = "ORA:User Id=dubheco; Password=pctrain5; Server=192.168.2.128; Direct=True; Sid=dintecdb; Port=1521";
			if (dbConn == DBConn.Old_GMI) connStr = "ORA:User Id=rtm; Password=pctrain2; Server=113.130.254.133; Direct=True; Sid=dintecdb; Port=1521";
			if (dbConn == DBConn.iU) connStr = "SQL:" + GetConnectString();
			if (dbConn == DBConn.GroupWare) connStr = "SQL:Server=113.130.254.143; Database=NeoBizboxS2; Uid=sa; Password=skm0828!";
			if (dbConn == DBConn.Mail) connStr = "SQL:Server=113.130.254.131; Database=MCE7; Uid=sa; Password=!q7hfnl3sh62@";
			if (dbConn == DBConn.MES) connStr = "SQL:server=113.130.254.139; Database=WWiMES; Uid=sa; Password=skm0828!";

			if (connStr.Substring(0, 3) == "SQL") provider = DBProvider.SqlServer;
			else provider = DBProvider.Oracle;

			this.parameters_s = new List<SqlParameter>();
			this.parameters_o = new List<OracleParameter>();
		}

		private SqlCommand SetCommand(SqlConnection conn)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = text;
			cmd.CommandTimeout = 300;
			cmd.CommandType = cmdType;
			cmd.Connection = conn;

			//if (cmd.CommandType == CommandType.StoredProcedure)
			//{
			// 파라미터 등록
			cmd.Parameters.Clear();
			foreach (SqlParameter par in parameters_s) cmd.Parameters.Add(par);
			//}
			

			return cmd;
		}

		private OracleCommand SetCommand(OracleConnection conn)
		{
			OracleCommand cmd = new OracleCommand();
			cmd.CommandText = text;
			cmd.CommandType = cmdType;
			cmd.Connection = conn;

			if (cmd.CommandType == CommandType.StoredProcedure)
			{
				cmd.Parameters.Clear();
				foreach (OracleParameter par in parameters_o) cmd.Parameters.Add(par);
			}

			return cmd;
		}

		// ================================================== 파라메타 관련
		public void AddParameter(string parameterName, object value)
		{
			string s = GetTo.String(value);

			if (provider == DBProvider.SqlServer)
			{
				// DBNull.Value을 넣으면 프로시저에서 기본값 자체를 설정못하므로 파라미터를 안넘기게 해주고 싶으나 그러면 더존 프로시저를 못씀. 변수 안넘기면 에러뜸
				if (s.Replace(" ", "") == "")
					parameters_s.Add(new SqlParameter(parameterName, DBNull.Value));
				else
					parameters_s.Add(new SqlParameter(parameterName, s));
			}
			if (provider == DBProvider.Oracle)
			{
				if (s.Replace(" ", "") == "")
					parameters_o.Add(new OracleParameter(parameterName, DBNull.Value));
				else
					parameters_o.Add(new OracleParameter(parameterName, s));
			}
		}

		public void AddParameter(SqlParameter sqlParameter)
		{
			parameters_s.Add(sqlParameter);
		}

		public void AddParameterRange(SqlParameter[] sqlParameter)
		{
			parameters_s.AddRange(sqlParameter);
		}

		public void ClearParameter()
		{
			parameters_s.Clear();
			parameters_o.Clear();
		}

		// ================================================== 쿼리 실행
		public int ExecuteNonQuery()
		{
			int retVal = 0;

			if (provider == DBProvider.SqlServer)
			{
				// 연결
				SqlConnection conn = new SqlConnection(connStr.Substring(4));
				conn.Open();

				// 커맨드 설정
				SqlCommand cmd = SetCommand(conn);

				// 디버그프린트
				if (DebugMode == DebugMode.Popup)
					PopupDebug(cmd);
				else if (DebugMode == DebugMode.Print)
					PrintDebug(cmd);

				// 실행
				retVal = cmd.ExecuteNonQuery();
				conn.Close();
			}
			if (provider == DBProvider.Oracle)
			{
				OracleConnection conn = new OracleConnection(connStr.Substring(4));
				conn.Unicode = true;
				conn.Open();

				OracleCommand cmd = SetCommand(conn);
				retVal = cmd.ExecuteNonQuery();
				conn.Close();
			}

			return retVal;
		}

		public DataTable GetDataTable()
		{
			DataTable dt = new DataTable();

			if (provider == DBProvider.SqlServer)
			{
				// 연결
				SqlConnection conn = new SqlConnection(connStr.Substring(4));
				conn.Open();

				// 커맨드 설정
				SqlCommand cmd = SetCommand(conn);

				// 디버그프린트
				if (DebugMode == DebugMode.Popup)
					PopupDebug(cmd);
				else if (DebugMode == DebugMode.Print)
					PrintDebug(cmd);

				// 실행
				SqlDataAdapter da = new SqlDataAdapter();
				da.SelectCommand = cmd;
				da.Fill(dt);
				conn.Close();
			}
			if (provider == DBProvider.Oracle)
			{
				OracleConnection conn = new OracleConnection(connStr.Substring(4));
				conn.Unicode = true;
				conn.Open();

				OracleDataAdapter da = new OracleDataAdapter();
				da.SelectCommand = SetCommand(conn);
				da.Fill(dt);
				conn.Close();
			}

			return dt;
		}

		public DataSet GetDataSet()
		{
			DataSet ds = new DataSet();

			if (provider == DBProvider.SqlServer)
			{
				// 연결
				SqlConnection conn = new SqlConnection(connStr.Substring(4));
				conn.Open();

				// 커맨드 설정
				SqlCommand cmd = SetCommand(conn);

				// 디버그프린트
				if (DebugMode == DebugMode.Popup)
					PopupDebug(cmd);
				else if (DebugMode == DebugMode.Print)
					PrintDebug(cmd);

				// 실행
				SqlDataAdapter da = new SqlDataAdapter();
				da.SelectCommand = cmd;
				da.Fill(ds);
				conn.Close();
			}
			if (provider == DBProvider.Oracle)
			{
				OracleConnection conn = new OracleConnection(connStr.Substring(4));
				conn.Unicode = true;
				conn.Open();

				OracleDataAdapter da = new OracleDataAdapter();
				da.SelectCommand = SetCommand(conn);
				da.Fill(ds);
				conn.Close();
			}

			return ds;
		}

		// ================================================== Static 함수
		public static int ExecuteNonQuery(string query)
		{
			SqlConnection conn = new SqlConnection(GetConnectString());
			conn.Open();

			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = query;
			cmd.CommandTimeout = 300;
			cmd.CommandType = CommandType.Text;
			cmd.Connection = conn;

			int ret = cmd.ExecuteNonQuery();
			conn.Close();

			return ret;
		}

		public static int ExecuteNonQuery(string spName, params object[] parameters)
		{
			return ExecuteNonQuery(spName, DebugMode.None, parameters);
		}

		public static int ExecuteNonQuery(string spName, DebugMode debugMode, params object[] parameters)
		{
			// 연결
			SqlConnection conn = new SqlConnection(GetConnectString());
			conn.Open();

			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = spName;
			cmd.CommandTimeout = 300;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;

			// 파라미터 추가
			SqlCommandBuilder.DeriveParameters(cmd);

			// 정체불명의 @RETURN_VALUE 제거
			if (cmd.Parameters.Count > 0 && cmd.Parameters[0].ParameterName == "@RETURN_VALUE")
				cmd.Parameters.Remove(cmd.Parameters[0]);

			// 추가
			for (int i = 0; i < parameters.Length; i++)
			{
				cmd.Parameters[i].Value = parameters[i];
				cmd.Parameters[i].TypeName = "";
			}

			// 디버그
			//if (printDebug)
			//    DebugPrint(cmd);

			//if (popupDebug)
			//    PopupDebugPrint(cmd);
			if (debugMode == DebugMode.Popup)
				PopupDebug(cmd);
			else if (debugMode == DebugMode.Print)
				PrintDebug(cmd);

			// 실행
			int ret = cmd.ExecuteNonQuery();
			conn.Close();

			return ret;
		}

		#region DataTable
		public static DataTable GetDataTable(string query)
		{
			DataTable dt = new DataTable();

			SqlConnection conn = new SqlConnection(GetConnectString());
			conn.Open();

			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = query;
			cmd.CommandTimeout = 300;
			cmd.CommandType = CommandType.Text;
			cmd.Connection = conn;

			SqlDataAdapter da = new SqlDataAdapter();
			da.SelectCommand = cmd;
			da.Fill(dt);
			conn.Close();

			return dt;
		}

		public static DataTable GetDataTable(string spName, params object[] parameters)
		{
			return GetDataTable(spName, true, false, parameters);
		}

		public static DataTable GetDataTable(string spName, DebugMode debugMode, params object[] parameters)
		{
			// ********** 연결
			SqlConnection conn = new SqlConnection(GetConnectString());
			conn.Open();

			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = spName;
			cmd.CommandTimeout = 300;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;

			// *********** 파라미터 추가
			SqlCommandBuilder.DeriveParameters(cmd);

			// 정체불명의 @RETURN_VALUE 제거
			if (cmd.Parameters.Count > 0 && cmd.Parameters[0].ParameterName == "@RETURN_VALUE")
				cmd.Parameters.Remove(cmd.Parameters[0]);
			 
			// 추가
			for (int i = 0; i < parameters.Length; i++)
				cmd.Parameters[i].Value = parameters[i];

			// *********** 디버그
			if (debugMode == DebugMode.Popup)
				PopupDebug(cmd);
			else if (debugMode == DebugMode.Print)
				PrintDebug(cmd);

			// *********** 실행
			SqlDataAdapter da = new SqlDataAdapter();
			da.SelectCommand = cmd;

			DataTable dt = new DataTable();
			da.Fill(dt);
			conn.Close();

			return dt;
		}

		public static DataTable GetDataTable(string spName, bool printDebug, bool popupDebug, params object[] parameters)
		{
			// 연결
			SqlConnection conn = new SqlConnection(GetConnectString());
			conn.Open();

			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = spName;
			cmd.CommandTimeout = 300;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;

			// 파라미터 추가
			SqlCommandBuilder.DeriveParameters(cmd);

			// 정체불명의 @RETURN_VALUE 제거
			if (cmd.Parameters.Count > 0 && cmd.Parameters[0].ParameterName == "@RETURN_VALUE")
				cmd.Parameters.Remove(cmd.Parameters[0]);

			// 추가
			for (int i = 0; i < parameters.Length; i++)
				cmd.Parameters[i].Value = parameters[i];

			// 디버그
			if (printDebug)
				PrintDebug(cmd);

			if (popupDebug)
				PopupDebug(cmd);

			// 실행
			SqlDataAdapter da = new SqlDataAdapter();
			da.SelectCommand = cmd;

			DataTable dt = new DataTable();
			da.Fill(dt);
			conn.Close();

			return dt;
		}
		#endregion

		public static DataTable GetDataTable(string spName, DBParameters parameters)
		{
			DataTable dt = new DataTable();

			SqlConnection conn = new SqlConnection(GetConnectString());
			conn.Open();

			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = spName;
			cmd.CommandTimeout = 300;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;
			cmd.Parameters.AddRange(parameters.Parameters);

			PrintDebug(cmd);    // 디버그프린트
			SqlDataAdapter da = new SqlDataAdapter();
			da.SelectCommand = cmd;
			da.Fill(dt);
			conn.Close();

			return dt;
		}

		public static void PrintDebug(SqlCommand cmd)
		{
			string s = "";

			foreach (SqlParameter p in cmd.Parameters)
			{
				if (p.Value == DBNull.Value || p.Value == null)
					s += "," + p.ParameterName + "=NULL" + "\n";
				else
					s += "," + p.ParameterName + "='" + p.Value.ToString().Replace("'", "''") + "'" + "\n";
			}

			// 첫번째 콤마(,) 제거
			if (s.IndexOf(",") == 0) s = " " + s.Substring(1);

			// 프로시저 작성
			s = ""
				+ "\n" + "--------------------------------------------------"
				+ "\n" + cmd.CommandText
				+ "\n" + s;

			Debug.WriteLine(s);
		}

		public static void PopupDebug(SqlCommand cmd)
		{
			if (!Global.MainFrame.LoginInfo.UserID.In("S-343", "S-391", "S-458"))
				return;

			string s = "";

			foreach (SqlParameter p in cmd.Parameters)
			{
				if (p.Value ==  null || p.Value == DBNull.Value)
					s += "," + p.ParameterName + "=NULL" + "\r\n";
				else
					s += "," + p.ParameterName + "='" + p.Value.ToString().Replace("'", "''") + "'" + "\r\n";
			}

			// 첫번째 콤마(,) 제거
			if (s.IndexOf(",") == 0) s = " " + s.Substring(1);

			// 프로시저 작성
			s = cmd.CommandText + "\r\n" + s;

			// 팝업
			H_CZ_DEBUG_PRINT f = new H_CZ_DEBUG_PRINT(s);
			f.ShowDialog();
		}



		#region DataSet

		public static DataSet GetDataSet(string query)
		{
			DataSet ds = new DataSet();

			SqlConnection conn = new SqlConnection(GetConnectString());
			conn.Open();

			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = query;
			cmd.CommandTimeout = 300;
			cmd.CommandType = CommandType.Text;
			cmd.Connection = conn;

			SqlDataAdapter da = new SqlDataAdapter();
			da.SelectCommand = cmd;
			da.Fill(ds);
			conn.Close();

			return ds;
		}

		public static DataSet GetDataSet(string spName, params object[] parameters)
		{
			return GetDataSet(spName, false, false, parameters);
		}

		public static DataSet GetDataSet(string spName, bool printDebug, bool popupDebug, params object[] parameters)
		{
			// 연결
			SqlConnection conn = new SqlConnection(GetConnectString());
			conn.Open();

			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = spName;
			cmd.CommandTimeout = 300;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = conn;

			// 파라미터 추가
			SqlCommandBuilder.DeriveParameters(cmd);
			int flag = (cmd.Parameters.Count > 0 && cmd.Parameters[0].ParameterName == "@RETURN_VALUE") ? 1 : 0;
			for (int i = 0; i < parameters.Length; i++)
				cmd.Parameters[i + flag].Value = parameters[i];

			// 디버그 프린트
			if (printDebug) PrintDebug(cmd);
			if (popupDebug) PopupDebug(cmd);

			// 실행
			SqlDataAdapter da = new SqlDataAdapter();
			da.SelectCommand = cmd;

			DataSet ds = new DataSet();
			da.Fill(ds);
			conn.Close();

			return ds;
		}

		#endregion

		public static string GetConnectString()
		{
			string connStr = "Server=113.130.254.143; Database=NEOE; Uid=NEOE; Password=NEOE";

			if (ClientRepository.DatabaseCallType == "Direct")
				connStr = ClientRepository.ConString;

			return connStr;
		}
	}








	public class DBParameters
	{
		Dictionary<string, object> pars = new Dictionary<string, object>();

		public object this[string parameterName]
		{
			set
			{
				pars[parameterName] = value;
			}
		}

		public SqlParameter[] Parameters
		{
			get
			{
				SqlParameter[] p = new SqlParameter[pars.Count];
				int i = 0;

				foreach (KeyValuePair<string, object> kv in pars)
				{
					p[i] = new SqlParameter();
					p[i].ParameterName = kv.Key;
					p[i].Value = kv.Value;
					i++;
				}

				return p;
			}
		}

		public void Add(string parameterName, object value)
		{
			if (value != null && value.ToString() != "") pars.Add(parameterName, value);
		}
	}

	public enum DBConn
	{
		Old_DINTEC
		, Old_DUBHECO
		, Old_GMI
		, iU
		, GroupWare
		, Mail
		, Fax
		, MES
		, Cloudoc
	}

	public enum DBProvider
	{
		SqlServer
		, Oracle
	}

	public enum DebugMode
	{
		None
	,	Print
	,	Popup
	}

	public enum  QueryType
	{
		Procedure
	,	Text
	}
}

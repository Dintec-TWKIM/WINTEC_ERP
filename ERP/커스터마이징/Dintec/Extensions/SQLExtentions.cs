using System;
using System.Data.SqlClient;
using System.Diagnostics;
using Duzon.Common.Forms;
using System.Data;

namespace Dintec
{
	public static class SQLExtentions
	{
		public static void Add2(this SqlParameterCollection o, string parameterName, object value)
		{
			// STRING 뿐만 아니라 INT 형 같은 애들이 ""로 들어오면 곤란하므로 ""때에는 아예 파라메타 추가를 안함
			if (value != null)
			{
				string s = value.ToString();

				if (s.Replace(" ", "") != "")
					o.Add(new SqlParameter(parameterName, value));
			}
		}

		public static void Add2(this SqlParameterCollection o, string parameterName, SQLEmpty sqlEmpty)
		{
			if (sqlEmpty == SQLEmpty.Value)
				o.Add(new SqlParameter(parameterName, ""));
		}

		public static void Add2(this SqlParameterCollection o, string parameterName, object value, bool allowEmpty)
		{		
			o.Add(new SqlParameter(parameterName, value));			
		}

		public static void Add2(this SqlParameterCollection o, string parameterName, string typeName, DataTable value)
		{
			o.Add(new SqlParameter(parameterName, SqlDbType.Structured) { TypeName = typeName, Value = value });
		}

		public static void Add2(this SqlParameterCollection o, SqlParameter sp)
		{
			o.Add(sp);
		}

		public static void Add2(this SqlParameterCollection o, SqlParameter[] sp)
		{
			foreach (System.Data.SqlClient.SqlParameter s in sp)
				o.Add2(s.ParameterName, s.Value);
		}		

		public static void SetParameter(this SqlCommand cmd, object[] parameters)
		{
			SqlCommandBuilder.DeriveParameters(cmd);

			if (cmd.Parameters.Count > 0 && cmd.Parameters[0].ParameterName == "@RETURN_VALUE")
				cmd.Parameters.Remove(cmd.Parameters[0]);

			for (int i = 0; i < parameters.Length; i++)
				cmd.Parameters[i].Value = parameters[i];

			//int flag = (cmd.Parameters.Count > 0 && cmd.Parameters[0].ParameterName == "@RETURN_VALUE") ? 1 : 0;

			//for (int i = 0; i < parameters.Length; i++)
			//	cmd.Parameters[i + flag].Value = parameters[i];
		}

		public static void ShowDebug(this SqlCommand cmd, SQLDebug sqlDebug)
		{
			string s = "";

			foreach (SqlParameter p in cmd.Parameters)
			{
				if (p.Value == DBNull.Value || p.Value == null)
				{
					s += "\r\n," + p.ParameterName + "=NULL";
				}
				else
				{
					if (p.ParameterName.StartsWith("@JSON"))
						s += "\r\n," + p.ParameterName + "='" + p.Value.ToString().Replace("'", "''").Replace("[{", "\r\n[{").Replace(",{", "\r\n,{") + "'";
					else
						s += "\r\n," + p.ParameterName + "='" + p.Value.ToString().Replace("'", "''") + "'";
				}
			}

			// 첫번째 콤마(,) 제거
			if (s != "")
				s = " " + s.Substring(3);

			s = cmd.CommandText + "\r\n" + s;

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
	}
}


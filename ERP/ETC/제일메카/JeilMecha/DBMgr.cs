using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace JeilMecha
{
	public class DBMgr
	{
		private string strConnString;

		public DBMgr(bool isDebug)
		{
			if (isDebug == true)
				strConnString = "Data Source=192.168.0.155;Initial Catalog=cus_dat;Persist Security Info=True;User ID=sa;Password=dintec5771";
			else
				strConnString = "Data Source=192.168.0.2;Initial Catalog=cus_dat;Persist Security Info=True;User ID=sa;Password=@ecis7000";
		}

	    public DataTable Select(string query)
		{
			SqlCommand cmd;
			SqlDataAdapter dataAdapter;
			DataTable dt;

			using (SqlConnection conn = new SqlConnection(strConnString))
			{
				conn.Open();

				cmd = new SqlCommand(query, conn);
				dataAdapter = new SqlDataAdapter(query, conn);
				dt = new DataTable();

				dataAdapter.Fill(dt);
			}

			return dt;
		}

		public int ExecuteNonQuery(string query)
		{
			SqlConnection conn = new SqlConnection(strConnString);
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


	}
}

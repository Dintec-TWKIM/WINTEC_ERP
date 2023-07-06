using Duzon.Common.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;

namespace DX
{
	public class TSQL
	{
		private readonly SqlConnection 연결;
		private readonly SqlCommand 명령어;
		private readonly SqlDataAdapter 데이터;

		private static string 연결문자_ERP => ClientRepository.DatabaseCallType == "Direct" ? ClientRepository.ConString : "Server=113.130.254.143; Database=NEOE; Uid=NEOE; Password=NEOE";
		private static string 연결문자_GW => "Server=113.130.254.143; Database=NeoBizboxS2; Uid=NEOE; Password=NEOE";

		public 디버그모드 디버그모드 { get; set; }

		public SqlParameterCollection 변수 => 명령어.Parameters;

		public string 프로시저
		{
			get => 명령어.CommandText;
			set => 명령어.CommandText = value;
		}

		public TSQL(string 쿼리)
		{			
			디버그모드 = Control.ModifierKeys == (Keys.Control | Keys.Alt) ? 디버그모드.팝업 : 디버그모드.출력;
			연결 = new SqlConnection(연결문자_ERP);
			명령어 = new SqlCommand(쿼리, 연결);
			데이터 = new SqlDataAdapter(명령어);
		}


		public void 실행()
		{
			실행<int>();
		}

		public DataTable 결과()
		{
			return 실행<DataTable>();
		}

		public DataSet 결과s()
		{
			return 실행<DataSet>();
		}

		public T 실행<T>() where T : new()
		{
			명령어.열기();
			명령어.디버그(디버그모드);

			if (typeof(T) == typeof(int))
			{
				return (T)Convert.ChangeType(명령어.ExecuteNonQuery(), typeof(T));
			}
			else if (typeof(T) == typeof(DataTable))
			{
				T dt = new T();
				데이터.Fill(dt as DataTable);
				연결.Close();

				return dt;
			}

			return default;
		}


		public static void 실행(string 쿼리, params object[] 변수) => 실행<int>(쿼리, 변수);

		public static DataTable 결과(string 쿼리, params object[] 변수) => 실행<DataTable>(쿼리, 변수);

		public static DataTable 결과(디비연결 디비연결, string 쿼리, params object[] 변수) => 실행<DataTable>(디비연결, 쿼리, 변수);

		public static DataSet 결과s(string 쿼리, params object[] 변수) => 실행<DataSet>(쿼리, 변수);

		public static T 실행<T>(string 쿼리, params object[] 변수) where T : new() => 실행<T>(디비연결.ERP, 쿼리, 변수);

		public static T 실행<T>(디비연결 디비연결, string 쿼리, params object[] 변수) where T : new()
		{			
			디버그모드 디버그모드 = Control.ModifierKeys == (Keys.Control | Keys.Alt) ? 디버그모드.팝업 : 디버그모드.출력;
			SqlConnection 연결 = new SqlConnection(연결문자(디비연결));
			SqlCommand 명령어 = new SqlCommand(쿼리, 연결);
			SqlDataAdapter 데이터 = new SqlDataAdapter(명령어);
			
			명령어.열기(변수);
			명령어.디버그(디버그모드);			

			if (typeof(T) == typeof(int))
			{
				return (T)Convert.ChangeType(명령어.ExecuteNonQuery(), typeof(T));
			}
			else if (typeof(T) == typeof(DataTable))
			{
				T dt = new T();
				데이터.Fill(dt as DataTable);
				연결.Close();

				return dt;
			}
			else if (typeof(T) == typeof(DataSet))
			{
				T ds = new T();
				데이터.Fill(ds as DataSet);
				연결.Close();

				return ds;
			}

			return default;
		}

		public static string 연결문자(디비연결 t)
		{
			if (t == 디비연결.ERP)
				return ClientRepository.DatabaseCallType == "Direct" ? ClientRepository.ConString : "Server=113.130.254.143; Database=NEOE; Uid=NEOE; Password=NEOE";
			else if (t == 디비연결.그룹웨어)
				return "Server=113.130.254.143; Database=NeoBizboxS2; Uid=NEOE; Password=NEOE";
			else
				return "";
		}
	}

	
}

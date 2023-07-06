using System.Data;
using Duzon.BizOn.Erpu.Security;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.UEncryption;

namespace cz
{
	internal class P_CZ_FI_BANK_SEND_RPT_BIZ
	{
		public DataTable Search(object[] Params)
		{
			DataTable dataTable;

			dataTable = DBHelper.GetDataTable("SP_CZ_FI_BANK_SEND_RPT_S", Params);

			return new UEncryption().SearchDecryptionTable(dataTable, new string[] { "NO_ACCT" }, new DataType[] { DataType.Account });
		}

		public DataTable SearchDetail(string 파일작성일자, string 파일작성순번)
		{
			DataTable dataTable;

			dataTable = DBHelper.GetDataTable("SP_CZ_FI_BANK_SEND_RPTL_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																						  파일작성일자,
																						  파일작성순번 });

			return new UEncryption().SearchDecryptionTable(dataTable, new string[] { "TRANS_ACCT_NO" }, new DataType[] { DataType.Account });
		}

		internal bool Save(DataTable dtH, DataTable dtL)
		{
			SpInfoCollection spInfoCollection = new SpInfoCollection();

			if (dtH != null)
			{
				SpInfo spInfo = new SpInfo();
                spInfo.DataValue = dtH;
				spInfo.CompanyID = MA.Login.회사코드;
				spInfo.UserID = MA.Login.사용자아이디;

				spInfo.SpNameDelete = "UP_FI_BANK_SEND_JAN_DELETE";
				spInfo.SpParamsDelete = new string[] { "CD_COMPANY",
													   "TRANS_DATE",
													   "TRANS_SEQ",
													   "NO_LIMITE" };
				spInfoCollection.Add(spInfo);
			}

			if (dtL != null)
			{
                SpInfo spInfo = new SpInfo();
                spInfo.DataValue = dtL;
                spInfo.CompanyID = MA.Login.회사코드;
                spInfo.UserID = MA.Login.사용자아이디;

                spInfo.SpNameUpdate = "SP_CZ_FI_BANK_SEND_RPTL_U";
                spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
													   "TRANS_DATE",
													   "TRANS_SEQ",
													   "SEQ",
                                                       "TRANS_ACCT_NO",
													   "CLIENT_NOTE",
                                                       "TRANS_NOTE",
                                                       "TP_CHARGE",
                                                       "TP_SEND_BY",
                                                       "DC_RELATION" };

                spInfo.SpNameDelete = "UP_FI_BANK_SEND_JAN_DELETE2";
                spInfo.SpParamsDelete = new string[] { "CD_COMPANY",
													   "TRANS_DATE",
													   "TRANS_SEQ",
													   "SEQ",
													   "NO_LIMITE" };

                spInfoCollection.Add(spInfo);
			}

			return DBHelper.Save(spInfoCollection);
		}

		internal string Sender_EMail()
		{
			string str1 = string.Empty;
			string str2 = @"SELECT NO_EMAIL
							FROM MA_EMP WITH(NOLOCK) 
							WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + 
						   "AND NO_EMP = '" + MA.Login.사원번호 + "'";

			DataTable dataTable = Global.MainFrame.FillDataTable(str2);

			if (dataTable != null && dataTable.Rows.Count > 0)
				str1 = dataTable.Rows[0]["NO_EMAIL"].ToString();
			
			return str1;
		}

		internal bool SavePrintLog(DataTable dt)
		{
			if (dt == null || dt.Rows.Count == 0) return false;

			SpInfo spInfo = new SpInfo();
			spInfo.DataValue = dt;
			spInfo.DataState = DataValueState.Added;
			spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
			spInfo.SpNameInsert = "SP_CZ_FI_BANK_SEND_PRINT_LOG_I";
            spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
                                                   "TRANS_DATE",
                                                   "TRANS_SEQ",
                                                   "SEQ",
                                                   "CUST_CODE",
                                                   "TRANS_BANK_CODE",
                                                   "TRANS_ACCT_NO",
                                                   "TRANS_NAME",
                                                   "CD_EXCH",
                                                   "TRANS_AMT_EX",
                                                   "TRANS_AMT", 
                                                   "CLIENT_NOTE",
                                                   "TRANS_NOTE",
                                                   "NM_BANK_EN",
                                                   "CD_BANK_NATION",
                                                   "NO_SORT",
                                                   "NO_SWIFT",
                                                   "DC_DEPOSIT_TEL",
                                                   "CD_DEPOSIT_NATION",
                                                   "DC_DEPOSIT_ADDRESS",
												   "NO_BANK_BIC",
                                                   "TP_CHARGE",
                                                   "TP_SEND_BY",
                                                   "DC_RELATION",
                                                   "ID_INSERT" };

			return DBHelper.Save(spInfo);
		}
	}
}

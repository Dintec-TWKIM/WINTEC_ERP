using Dintec;
using Duzon.Common.Forms;
using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace cz
{
	internal class P_CZ_FI_CARD_STATE_MNG_BIZ
	{
        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_FI_CARD_STATE_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

		internal bool SaveExcel(string 명세년월, DataTable dtExcel)
		{
			DataSet ds = new DataSet();
			DataRow[] dataRowArray;
			string xml = string.Empty;
			int groupUnit = 1000, index = 0;

			try
			{
				dataRowArray = dtExcel.AsEnumerable().ToArray();

				for (int i = 0; i < dataRowArray.Length; i += groupUnit)
				{
					ds.Merge(dataRowArray.AsEnumerable().Skip(i).Take(groupUnit).ToArray());
				}

				foreach (DataTable dt1 in ds.Tables)
				{
					MsgControl.ShowMsg("데이터 저장 중입니다.\n잠시만 기다려 주세요 (@/@)", new string[] { D.GetString(++index), D.GetString(ds.Tables.Count) });

					xml = Util.GetTO_Xml(dt1);
					DBHelper.ExecuteNonQuery("SP_CZ_FI_CARD_STATE_EXCEL", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																						 명세년월,
																						 xml,
																						 Global.MainFrame.LoginInfo.UserID });
				}

				return true;
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
			finally
			{
				MsgControl.CloseMsg();
			}

			return false;
		}

		public void SaveFileInfo(string cdCompany, string cdFile, FileInfo fileInfo, string 업로드위치, string 메뉴명)
		{
			string query;

			try
			{
				query = @"SELECT MAX(NO_SEQ) 
                          FROM MA_FILEINFO WITH(NOLOCK)
                          WHERE CD_COMPANY = '" + cdCompany + "'" + Environment.NewLine +
						@"AND CD_MODULE = 'CZ'
                          AND ID_MENU = '" + 메뉴명 + "'" + Environment.NewLine +
						 "AND CD_FILE = '" + cdFile + "'";

				DBHelper.ExecuteNonQuery("UP_MA_FILEINFO_INSERT", new object[] { cdCompany,
																				 "CZ",
																				 메뉴명,
																				 cdFile,
																				 (D.GetDecimal(DBHelper.ExecuteScalar(query)) + 1),
																				 fileInfo.Name,
																				 업로드위치,
																				 fileInfo.Extension.Replace(".", ""),
																				 fileInfo.LastWriteTime.ToString("yyyyMMdd"),
																				 fileInfo.LastWriteTime.ToString("hhmm"),
																				 string.Format("{0:0.00}M", ((Decimal)fileInfo.Length / new Decimal(1048576))),
																				 Global.MainFrame.LoginInfo.UserID });
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}
	}
}

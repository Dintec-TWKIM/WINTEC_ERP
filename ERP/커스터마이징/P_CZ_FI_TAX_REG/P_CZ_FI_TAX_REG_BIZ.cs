using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.ERPU;
using Duzon.Common.Util;
using Duzon.Common.Forms;
using Dintec;

namespace cz
{
	internal class P_CZ_FI_TAX_REG_BIZ
	{
		public DataTable Search(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_FI_TAX_REG_S", obj);
			T.SetDefaultValue(dt);
			return dt;
		}

		public DataTable SearchExportH(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_FI_TAX_EXPORTH_S", obj);
			T.SetDefaultValue(dt);
			return dt;
		}

		public DataTable SearchExportL(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_FI_TAX_EXPORTL_S", obj);
			T.SetDefaultValue(dt);
			return dt;
		}

		public DataTable SearchExportD(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_FI_TAX_EXPORTD_S", obj);
			T.SetDefaultValue(dt);
			return dt;
		}

		internal bool Save(DataTable dtH, DataTable dt, DataTable dtD)
		{
			SpInfoCollection sc = new SpInfoCollection();

			if (dtH != null && dtH.Rows.Count != 0)
			{
				SpInfo si = new SpInfo();

				si.DataValue = dtH;
				si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
				si.UserID = Global.MainFrame.LoginInfo.UserID;

				si.SpNameUpdate = "SP_CZ_FI_TAX_EXPORTH_U";
				si.SpParamsUpdate = new string[] { "CD_COMPANY",
												   "NO_TAX",
												   "YN_CHECK",
												   "DC_IMPORT",
												   "DC_RMK",
												   "ID_UPDATE" };
				sc.Add(si);
			}

			if (dtD != null && dtD.Rows.Count != 0)
			{
				SpInfo si = new SpInfo();

				si.DataValue = dtD;
				si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
				si.UserID = Global.MainFrame.LoginInfo.UserID;

				si.SpNameInsert = "SP_CZ_FI_TAX_EXPORTD_I";
				si.SpParamsInsert = new string[] { "CD_COMPANY",
												   "NO_TO",
												   "NO_IO",
												   "ID_INSERT" };
				sc.Add(si);
			}

			if (dt != null && dt.Rows.Count != 0)
			{
				SpInfo si = new SpInfo();

				si.DataValue = dt;
				si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
				si.UserID = Global.MainFrame.LoginInfo.UserID;

				si.SpNameUpdate = "SP_CZ_FI_TAX_REG_U";
				si.SpParamsUpdate = new string[] { "CD_COMPANY",
											       "NO_IV",
											       "NO_IO",
											       "NO_SO",
											       "NO_TO",
											       "DT_TO",
											       "CD_EXCH_TAX",
											       "RT_EXCH_TAX",
											       "AM_EX_TAX",
											       "AM_TAX",
											       "DT_SHIPPING",
											       "DC_RMK",
											       "ID_UPDATE" };
				sc.Add(si);
			}

			return DBHelper.Save(sc);
		}

		internal bool SaveExcel(DataTable dtExcel)
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
					DBHelper.ExecuteNonQuery("SP_CZ_FI_TAX_EXPORT_EXCEL", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
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

		internal bool SaveParsing(DataTable dt)
		{
			string xml = string.Empty;

			try
			{
				xml = Util.GetTO_Xml(dt);
				DBHelper.ExecuteNonQuery("SP_CZ_FI_TAX_EXPORT_PARSING", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																					   xml,
																					   Global.MainFrame.LoginInfo.UserID });

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
	}
}

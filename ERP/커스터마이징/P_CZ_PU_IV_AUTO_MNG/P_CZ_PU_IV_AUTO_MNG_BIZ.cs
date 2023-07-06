using Dintec;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using System;
using System.Data;
using System.IO;

namespace cz
{
	internal class P_CZ_PU_IV_AUTO_MNG_BIZ
	{
        public DataTable Search(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_PU_IV_AUTO_MNGH_S", obj);
        }

		public DataTable Search2(object[] obj)
		{
			return DBHelper.GetDataTable("SP_CZ_PU_IV_AUTO_MNGH2_S", obj);
		}

		public DataTable SearchDetail(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_PU_IV_AUTO_MNGL_S", obj);
        }

		public DataTable SearchDetail2(object[] obj)
		{
			return DBHelper.GetDataTable("SP_CZ_PU_IV_AUTO_MNGL2_S", obj);
		}

		public DataTable SearchDetail3(object[] obj)
		{
			return DBHelper.GetDataTable("SP_CZ_PU_IV_AUTO_MNGL3_S", obj);
		}

		public DataTable SearchDetail4(object[] obj)
		{
			return DBHelper.GetDataTable("SP_CZ_PU_IV_AUTO_MNGL4_S", obj);
		}

		public DataSet SearchIv(object[] obj)
		{
			DataSet dataSet = DBHelper.GetDataSet("SP_CZ_PU_IV_REG_SUB_S", obj);
			T.SetDefaultValue(dataSet);
			return dataSet;
		}

        internal bool Save(DataTable dt, DataTable dtH, DataTable dtL, DataTable dtF)
        {
			SpInfo si = new SpInfo();

			if (dt != null && dt.Rows.Count != 0)
            {
                si.DataValue = Util.GetXmlTable(dt);
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_PU_IV_AUTO_MNG_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };
            }

			if (dtH != null && dtH.Rows.Count != 0)
			{
				si.DataValue = dtH;
				si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
				si.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
				si.SpNameUpdate = "SP_CZ_PU_IV_AUTOH_U";
				si.SpParamsUpdate = new string[] { "CD_COMPANY",
												   "CD_PARTNER",
												   "DT_MONTH",
												   "IDX",
												   "DC_RMK",
												   "ID_UPDATE" };

				si.SpNameDelete = "SP_CZ_PU_IV_AUTOH_D";
				si.SpParamsDelete = new string[] { "CD_COMPANY",
												   "CD_PARTNER",
												   "DT_MONTH",
												   "IDX" };
			}

			if (dtL != null && dtL.Rows.Count != 0)
			{
				si.DataValue = dtL;
				si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
				si.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
				si.SpNameUpdate = "SP_CZ_PU_IV_AUTO_U";
                si.SpParamsUpdate = new string[] { "CD_COMPANY",
												   "CD_PARTNER",
												   "DT_MONTH",
												   "IDX",
												   "SEQ",
												   "DC_RMK",
												   "ID_UPDATE" };
			}

			if (dtF != null && dtF.Rows.Count != 0)
			{
				si.DataValue = dtF;
				si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
				si.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
				si.SpNameUpdate = "SP_CZ_PU_IV_AUTO_U";
				si.SpParamsUpdate = new string[] { "CD_COMPANY",
												   "CD_PARTNER",
												   "DT_MONTH",
												   "IDX",
												   "SEQ",
												   "DC_RMK_TAX",
												   "ID_UPDATE" };
			}

			return DBHelper.Save(si);
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

		public bool 부가세변경(string 마감번호)
		{
			SpInfo si = new SpInfo();
			si.SpNameSelect = "UP_PU_IVH_MODIFY";
			si.SpParamsSelect = new object[] { 마감번호, Global.MainFrame.LoginInfo.CompanyCode };
			ResultData _rtn = (ResultData)Global.MainFrame.FillDataTable(si);

			return true;
		}

		public Decimal 환율(string 매입일자, string 통화명)
		{
			SpInfo spInfo = new SpInfo();
			spInfo.SpNameSelect = "SP_CZ_SA_IV_SUB_EXCH_S";
			spInfo.SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
												   매입일자,
												   통화명 };

			return Convert.ToDecimal(((ResultData)(Global.MainFrame.FillDataTable(spInfo))).OutParamsSelect[0, 0]);
		}
	}
}

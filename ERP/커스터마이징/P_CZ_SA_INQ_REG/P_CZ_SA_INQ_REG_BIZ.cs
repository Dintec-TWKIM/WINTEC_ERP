using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.ERPU;
using Duzon.Common.Util;
using Duzon.Common.Forms;

namespace cz
{
	class P_CZ_SA_INQ_REG_BIZ
	{
		public DataTable Search(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_INQ_REGH_S", obj);
			T.SetDefaultValue(dt);
			return dt;
		}

		public DataTable SearchDetail(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_INQ_REGL_S", obj);
			T.SetDefaultValue(dt);
			return dt;
		}

        public DataTable 입력지원현황(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_INQ_REGD_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

		public DataTable 입력지원코드(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_INQ_REGC_S", obj);
			T.SetDefaultValue(dt);
			return dt;
		}

		public DataTable 입력지원자동할당(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_INQ_REGA_S", obj);
			T.SetDefaultValue(dt);
			return dt;
		}

		public DataSet 기본정보(object[] obj)
        {
            DataSet ds = DBHelper.GetDataSet("SP_CZ_SA_INQ_REG_DEFAULT", obj);
            T.SetDefaultValue(ds);
            return ds;
        }

		public bool SaveData(DataTable dtH, DataTable dtL, string 파일유형, string 회사)
		{
			SpInfoCollection sc = new SpInfoCollection();

			if (dtH != null && dtH.Rows.Count > 0)
			{
				SpInfo si = new SpInfo();
				si.DataValue = dtH;
                si.CompanyID = 회사;
				si.UserID = Global.MainFrame.LoginInfo.UserID;
				si.SpNameInsert = "SP_CZ_SA_INQ_REGH_I";
				si.SpParamsInsert = new string[] { "CD_COMPANY",
												   "TP_STEP",		
												   "NO_KEY",
												   "TP_SALES",		
												   "ID_SALES",		
												   "ID_TYPIST",	
												   "ID_PUR",		
												   "ID_LOG",		
												   "DC_RMK",		
												   "ID_INSERT" };
				si.SpNameUpdate = "SP_CZ_SA_INQ_REGH_U";
				si.SpParamsUpdate = new string[] { "CD_COMPANY",
												   "TP_STEP",		
												   "NO_KEY",		
												   "TP_SALES",		
												   "ID_SALES",		
												   "ID_TYPIST",	
												   "ID_PUR",		
												   "ID_LOG",		
												   "DC_RMK",		
												   "ID_UPDATE" };
				si.SpNameDelete = "SP_CZ_SA_INQ_REGH_D";
				si.SpParamsDelete = new string[] { "CD_COMPANY",
												   "TP_STEP",		
												   "NO_KEY" };

				si.SpParamsValues.Add(ActionState.Insert, "TP_STEP", 파일유형);
				si.SpParamsValues.Add(ActionState.Update, "TP_STEP", 파일유형);
				si.SpParamsValues.Add(ActionState.Delete, "TP_STEP", 파일유형);

				sc.Add(si);
			}

			if (dtL != null && dtL.Rows.Count > 0)
			{
				SpInfo si = new SpInfo();
				si.DataValue = dtL;
				si.CompanyID = 회사;
				si.UserID = Global.MainFrame.LoginInfo.UserID;
				si.SpNameInsert = "SP_CZ_SA_INQ_REGL_I";
				si.SpParamsInsert = new string[] { "CD_COMPANY",	
												   "TP_STEP",			
												   "NO_KEY",
												   "NM_FILE",
	                                               "NM_FILE_REAL",
												   "CD_SUPPLIER",
                                                   "YN_PARSING",
												   "YN_INCLUDED",
												   "ID_INSERT" };
				si.SpNameUpdate = "SP_CZ_SA_INQ_REGL_U";
				si.SpParamsUpdate = new string[] { "CD_COMPANY",   
												   "TP_STEP",      	
												   "NO_KEY",
      	                                           "NO_LINE",
												   "NM_FILE",
												   "NM_FILE_REAL",
                                                   "CD_SUPPLIER",
                                                   "YN_PARSING",
												   "ID_UPDATE" };		
				si.SpNameDelete = "SP_CZ_SA_INQ_REGL_D";
				si.SpParamsDelete = new string[] { "CD_COMPANY",
												   "TP_STEP",			
												   "NO_KEY",
                                                   "NO_LINE" };

				si.SpParamsValues.Add(ActionState.Insert, "TP_STEP", 파일유형);
				si.SpParamsValues.Add(ActionState.Update, "TP_STEP", 파일유형);
				si.SpParamsValues.Add(ActionState.Delete, "TP_STEP", 파일유형);

				sc.Add(si);
			}

            if (sc.List != null && sc.List.Count > 0)
                return DBHelper.Save(sc);
            else
                return true;
		}

        public string 입력담당자자동지정(string 회사, DataTable dtH, bool isMaps)
        {
            DataTable dt, dt1;
            DataRow[] dataRowArray;
            string columnName;

            try
            {
                if (isMaps)
                    columnName = "YN_MAPS";
                else
                    columnName = "YN_AUTO";

                dt = this.입력지원자동할당(new object[] { 회사,
														  Global.MainFrame.LoginInfo.UserID });


                dt1 = dt.DefaultView.ToTable(true, "ID_USER", "QT_PROGRESS", columnName);

                foreach (DataRow dr in dt1.Rows)
                {
                    dr["QT_PROGRESS"] = D.GetDecimal(dr["QT_PROGRESS"]) + dtH.Select("ID_TYPIST ='" + D.GetString(dr["ID_USER"]) + "'", string.Empty, DataViewRowState.Added).Count();
                }

                dataRowArray = dt1.Select(columnName + " = 'Y'", "QT_PROGRESS ASC");

                if (dataRowArray.Length > 0)
                    return D.GetString(dataRowArray[0]["ID_USER"]);
                else
                    return string.Empty;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return string.Empty;
        }
	}
}

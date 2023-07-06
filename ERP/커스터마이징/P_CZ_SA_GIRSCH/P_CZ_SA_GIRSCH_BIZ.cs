using System.Data;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using System;
using Dass.FlexGrid;


namespace cz
{
	class P_CZ_SA_GIRSCH_BIZ
	{
		string 회사코드 = Global.MainFrame.LoginInfo.CompanyCode;

		public DataTable Search_req(object[] obj)
		{
            DataTable dt = DBHelper.GetDataTable("UP_SA_GIRSCH_SELECT_PRINT", obj);
            T.SetDefaultValue(dt);
            return dt;
		}

		public DataTable SearchHeader(object[] obj)
		{
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_GIRSCHH_S", obj);
            T.SetDefaultValue(dt);
            return dt;
		}

		public DataTable SearchMiddle(object[] obj)
		{
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_GIRSCHM_S", obj);
            T.SetDefaultValue(dt);
            return dt;
		}

		public DataTable SearchLine(object[] obj)
		{
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_GIRSCHL_S", obj);
            T.SetDefaultValue(dt);
            return dt;
		}

		public DataSet 포장데이터(object[] obj)
		{
            DataSet ds = DBHelper.GetDataSet("SP_CZ_SA_GIRSCH_PACK_S", obj);
			T.SetDefaultValue(ds);
			return ds;
		}

        public DataSet DHL(object[] obj)
        {
            DataSet ds = DBHelper.GetDataSet("SP_CZ_SA_GIRSCH_DHL", obj);
            T.SetDefaultValue(ds);
            return ds;
        }

        public bool 협조전삭제(DataTable dt)
		{
			SpInfo si = new SpInfo();

			si.DataValue = dt;
			si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
			si.UserID = Global.MainFrame.LoginInfo.UserID;

			si.SpNameDelete = "SP_CZ_SA_GIRSCH_D";
			si.SpParamsDelete = new string[] 
			{ 
				"CD_COMPANY",
				"NO_GIR",
				"CD_TYPE",
                "ID_UPDATE"
			};

			return DBHelper.Save(si);
		}

        public bool SaveData(DataTable dt)
        {
            SpInfo spInfo = new SpInfo();

            if (dt != null)
            {
                spInfo.DataValue = dt;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
                spInfo.SpNameUpdate = "SP_CZ_SA_GIRSCH_U";
                spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
													   "NO_GIR",
													   "CD_TYPE",
                                                       "DT_START",
                                                       "DT_COMPLETE",
                                                       "DT_BILL",
                                                       "DC_RMK",
                                                       "DC_RMK1",
													   "DC_RMK2",
                                                       "DC_RMK_CI",
                                                       "DC_RMK_PL",
                                                       "NO_DELIVERY_EMAIL",
                                                       "YN_EXCLUDE_CPR",
                                                       "DT_IV",
                                                       "CD_TAEGBAE",
                                                       "ID_UPDATE" };
            }

            return DBHelper.Save(spInfo);
        }

		public bool 상태저장(DataTable dt, string 확정여부)
		{
			SpInfo spInfo = new SpInfo();

			if (dt != null)
			{
				spInfo.DataValue = dt;
				spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
				spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
				spInfo.SpNameUpdate = "SP_CZ_SA_GIRSCH_CONFIRM";
				spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
													   "NO_GIR",
													   "CD_TYPE",
													   "STA_GIR",
                                                       "YN_CONFIRM",
													   "ID_UPDATE" };

                spInfo.SpParamsValues.Add(ActionState.Update, "YN_CONFIRM", 확정여부);
			}

			return DBHelper.Save(spInfo);
		}

        internal void SaveContent(ContentType contentType, Dass.FlexGrid.CommandType commandType, string noGir, string value, string companyCode, string type)
        {
            string sqlQuery = string.Empty;
            string columnName = string.Empty;
            string table = string.Empty;

            if (contentType == ContentType.Memo)
                columnName = "MEMO_CD";
            else if (contentType == ContentType.CheckPen)
                columnName = "CHECK_PEN";

            if (type == "001")
                table = "SA_GIRH";
            else
                table = "CZ_SA_GIRH_PACK";

            if (commandType == Dass.FlexGrid.CommandType.Add)
            {
                sqlQuery = "UPDATE " + table + Environment.NewLine +
                           "SET " + columnName + " = '" + value + "'" + Environment.NewLine +
                           "WHERE CD_COMPANY = '" + companyCode + "'" + Environment.NewLine +
                           "AND NO_GIR = '" + noGir + "'";
            }
            else if (commandType == Dass.FlexGrid.CommandType.Delete)
            {
                sqlQuery = "UPDATE " + table + Environment.NewLine +
                           "SET " + columnName + " = NULL" + Environment.NewLine +
                           "WHERE CD_COMPANY = '" + companyCode + "'" + Environment.NewLine +
                           "AND NO_GIR = '" + noGir + "'";
            }

            Global.MainFrame.ExecuteScalar(sqlQuery);
        }
	}
}
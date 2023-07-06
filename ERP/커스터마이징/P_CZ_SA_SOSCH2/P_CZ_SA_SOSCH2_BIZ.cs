using System;
using System.Data;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;

namespace cz
{
    class P_CZ_SA_SOSCH2_BIZ
    {
        public DataTable SearchHeader(수주진행현황탭 탭구분, object[] obj)
        {
            DataTable dt;

            switch(탭구분)
            {
                case 수주진행현황탭.수주번호:
                    if (string.IsNullOrEmpty(obj[2].ToString()))
                        dt = DBHelper.GetDataTable("SP_CZ_SA_SOSCH2H_S0_1", obj);
                    else
                        dt = DBHelper.GetDataTable("SP_CZ_SA_SOSCH2H_S0_2", obj);
                    break;
                case 수주진행현황탭.발주번호:
                    dt = DBHelper.GetDataTable("SP_CZ_SA_SOSCH2H_S1", obj);
                    break;
                case 수주진행현황탭.입고일:
                    dt = DBHelper.GetDataTable("SP_CZ_SA_SOSCH2H_S2", obj);
                    break;
                case 수주진행현황탭.납기일:
                    dt = DBHelper.GetDataTable("SP_CZ_SA_SOSCH2H_S3", obj);
                    break;
                case 수주진행현황탭.매입처:
                    dt = DBHelper.GetDataTable("SP_CZ_SA_SOSCH2H_S4", obj);
                    break;
                case 수주진행현황탭.매출처:
                    dt = DBHelper.GetDataTable("SP_CZ_SA_SOSCH2H_S5", obj);
                    break;
                case 수주진행현황탭.품목:
                    dt = DBHelper.GetDataTable("SP_CZ_SA_SOSCH2H_S6", obj);
                    break;
                case 수주진행현황탭.수주유형:
                    dt = DBHelper.GetDataTable("SP_CZ_SA_SOSCH2H_S7", obj);
                    break;
                case 수주진행현황탭.영업그룹:
                    dt = DBHelper.GetDataTable("SP_CZ_SA_SOSCH2H_S8", obj);
                    break;
                case 수주진행현황탭.리스트:
                    dt = DBHelper.GetDataTable("SP_CZ_SA_SOSCH2H_S9", obj);
                    break;
                default:
                    return null;
            }

            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchMiddle(수주진행현황탭 탭구분, object[] obj)
        {
            DataTable dt;

            switch (탭구분)
            {
                case 수주진행현황탭.발주번호:
                    dt = DBHelper.GetDataTable("SP_CZ_SA_SOSCH2M_S1", obj);
                    break;
                case 수주진행현황탭.입고일:
                    dt = DBHelper.GetDataTable("SP_CZ_SA_SOSCH2M_S2", obj);
                    break;
                case 수주진행현황탭.납기일:
                    dt = DBHelper.GetDataTable("SP_CZ_SA_SOSCH2M_S3", obj);
                    break;
                case 수주진행현황탭.매입처:
                    dt = DBHelper.GetDataTable("SP_CZ_SA_SOSCH2M_S4", obj);
                    break;
                case 수주진행현황탭.매출처:
                    dt = DBHelper.GetDataTable("SP_CZ_SA_SOSCH2M_S5", obj);
                    break;
                case 수주진행현황탭.품목:
                    dt = DBHelper.GetDataTable("SP_CZ_SA_SOSCH2M_S6", obj);
                    break;
                case 수주진행현황탭.수주유형:
                    dt = DBHelper.GetDataTable("SP_CZ_SA_SOSCH2M_S7", obj);
                    break;
                case 수주진행현황탭.영업그룹:
                    dt = DBHelper.GetDataTable("SP_CZ_SA_SOSCH2M_S8", obj);
                    break;
                default:
                    return null;
            }

            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchLine(수주진행현황탭 탭구분, object[] obj)
        {
            DataTable dt;

            switch (탭구분)
            {
                case 수주진행현황탭.수주번호:
                    dt = DBHelper.GetDataTable("SP_CZ_SA_SOSCH2L_S0", obj);
                    break;
                case 수주진행현황탭.발주번호:
                    dt = DBHelper.GetDataTable("SP_CZ_SA_SOSCH2L_S1", obj);
                    break;
                case 수주진행현황탭.입고일:
                    dt = DBHelper.GetDataTable("SP_CZ_SA_SOSCH2L_S2", obj);
                    break;
                case 수주진행현황탭.납기일:
                    dt = DBHelper.GetDataTable("SP_CZ_SA_SOSCH2L_S3", obj);
                    break;
                case 수주진행현황탭.매입처:
                    dt = DBHelper.GetDataTable("SP_CZ_SA_SOSCH2L_S4", obj);
                    break;
                case 수주진행현황탭.매출처:
                    dt = DBHelper.GetDataTable("SP_CZ_SA_SOSCH2L_S5", obj);
                    break;
                case 수주진행현황탭.품목:
                    dt = DBHelper.GetDataTable("SP_CZ_SA_SOSCH2L_S6", obj);
                    break;
                case 수주진행현황탭.수주유형:
                    dt = DBHelper.GetDataTable("SP_CZ_SA_SOSCH2L_S7", obj);
                    break;
                case 수주진행현황탭.영업그룹:
                    dt = DBHelper.GetDataTable("SP_CZ_SA_SOSCH2L_S8", obj);
                    break;
                default:
                    return null;
            }

            T.SetDefaultValue(dt);
            return dt;
        }

        public bool SaveData(string companyCode, DataTable dt)
        {
            SpInfo si = new SpInfo();
            si.DataValue = dt;
            si.CompanyID = companyCode;
            si.UserID = Global.MainFrame.LoginInfo.UserID;

            si.SpNameUpdate = "SP_CZ_SA_SOSCH2H_U";
            si.SpParamsUpdate = new string[] { "CD_COMPANY",
                                               "NO_SO",
                                               "DC_RMK_TEXT2",
                                               "DC_RMK1",
                                               "TXT_USERDEF1",
                                               "ID_UPDATE" };

            si.SpParamsValues.Add(ActionState.Update, "ID_UPDATE", Global.MainFrame.LoginInfo.UserID);

            return DBHelper.Save(si);
        }

        public bool SaveDetailData(string companyCode, DataTable dt)
        {
            SpInfo si = new SpInfo();
            si.DataValue = dt;
            si.CompanyID = companyCode;
            si.UserID = Global.MainFrame.LoginInfo.UserID;

            si.SpNameUpdate = "SP_CZ_SA_SOSCH2L_U";
            si.SpParamsUpdate = new string[] { "CD_COMPANY",
                                               "CD_PLANT",
                                               "CD_ITEM",
                                               "DC_RMK_LOG",
                                               "ID_UPDATE" };

            si.SpParamsValues.Add(ActionState.Update, "ID_UPDATE", Global.MainFrame.LoginInfo.UserID);

            return DBHelper.Save(si);
        }

        internal void SaveContent(ContentType contentType, Dass.FlexGrid.CommandType commandType, string noSo, string value, string companyCode)
        {
            string sqlQuery = string.Empty;
            string columnName = string.Empty;

            if (contentType == ContentType.Memo)
                columnName = "MEMO_CD";
            else if (contentType == ContentType.CheckPen)
                columnName = "CHECK_PEN";

            if (commandType == Dass.FlexGrid.CommandType.Add)
            {
                sqlQuery = "UPDATE SA_SOH" + Environment.NewLine +
                           "SET " + columnName + " = '" + value + "'" + Environment.NewLine +
                           "WHERE CD_COMPANY = '" + companyCode + "'" + Environment.NewLine +
                           "AND NO_SO = '" + noSo + "'";
            }
            else if (commandType == Dass.FlexGrid.CommandType.Delete)
            {
                sqlQuery = "UPDATE SA_SOH" + Environment.NewLine +
                           "SET " + columnName + " = NULL" + Environment.NewLine +
                           "WHERE CD_COMPANY = '" + companyCode + "'" + Environment.NewLine +
                           "AND NO_SO = '" + noSo + "'";
            }
            
            Global.MainFrame.ExecuteScalar(sqlQuery);
        }
    }
}

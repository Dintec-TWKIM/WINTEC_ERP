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
    internal class P_CZ_SA_DEFERRED_DELIVERY_MNG_BIZ
    {
        internal DataTable Search(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_DEFERRED_DELIVERYH_S0", obj);
            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search1(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_DEFERRED_DELIVERYH_S1", obj);
            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search2(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_DEFERRED_DELIVERYH_S2", obj);
            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search3(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_DEFERRED_DELIVERYH_S3", obj);
            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable SearchDetail(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_DEFERRED_DELIVERYL_S", obj);
            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        public bool SaveDataHeader(string 표시유형, DataTable dt)
        {
            SpInfo si = new SpInfo();
            si.DataValue = dt;
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            si.UserID = Global.MainFrame.LoginInfo.UserID;

            si.SpNameUpdate = "SP_CZ_SA_DEFERRED_DELIVERYH_U";
            si.SpParamsUpdate = new string[] { "CD_COMPANY",
                                               "TP_TYPE",
                                               "NO_SO",
                                               "NO_KEY",
                                               "DT_LIMIT",
                                               "DT_EXPECT",
                                               "DT_REPLY",
                                               "DC_RMK_TEXT2",
                                               "DC_RMK",
                                               "CD_DELAY",
                                               "TP_SEND",
                                               "DTS_SEND",
                                               "ID_UPDATE" };

            si.SpParamsValues.Add(ActionState.Update, "TP_TYPE", 표시유형);
            si.SpParamsValues.Add(ActionState.Update, "ID_UPDATE", Global.MainFrame.LoginInfo.UserID);

            return DBHelper.Save(si);
        }

        public bool SaveDataLine(string 표시유형, DataTable dt)
        {
            SpInfo si = new SpInfo();
            si.DataValue = dt;
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            si.UserID = Global.MainFrame.LoginInfo.UserID;

            si.SpNameUpdate = "SP_CZ_SA_DEFERRED_DELIVERYL_U";
            si.SpParamsUpdate = new string[] { "CD_COMPANY",
                                               "TP_TYPE",
                                               "NO_KEY",
                                               "NO_LINE",
                                               "DT_EXPECT",
                                               "DT_REPLY",
                                               "DC_RMK_LOG",
                                               "ID_UPDATE" };

            si.SpParamsValues.Add(ActionState.Update, "TP_TYPE", 표시유형);
            si.SpParamsValues.Add(ActionState.Update, "ID_UPDATE", Global.MainFrame.LoginInfo.UserID);

            return DBHelper.Save(si);
        }

        internal bool SaveExcel(DataTable dtExcel)
        {
            string xml = string.Empty;

            try
            {
                xml = Util.GetTO_Xml(dtExcel);
                DBHelper.ExecuteNonQuery("SP_CZ_SA_DEFERRED_DELIVERY_EXCEL", new object[] { xml, Global.MainFrame.LoginInfo.UserID });

                return true;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return false;
        }
    }
}

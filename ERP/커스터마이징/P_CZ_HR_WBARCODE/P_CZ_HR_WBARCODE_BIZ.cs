using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace cz
{
    class P_CZ_HR_WBARCODE_BIZ
    {
        internal DataTable Search(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("UP_HR_WECMATCH_SELECT", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search1(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_HR_WBARCODE_S2", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search2(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_HR_WBARCODE_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search3(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_HR_WBARCODE_S1", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal bool Save(DataTable dt)
        {
            SpInfo spInfo = new SpInfo();
            spInfo.DataValue = dt;
            spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
            spInfo.DataState = DataValueState.Modified;
            spInfo.SpNameUpdate = "UP_HR_WECMATCH_UPDATE";
            spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
                                                   "NO_EMP",
                                                   "NO_CARD",
                                                   "YN_CARD",
                                                   "DT_START",
                                                   "DT_CLOSE",
                                                   "ID_UPDATE" };

            return DBHelper.Save(spInfo);
        }

        internal bool Save1(DataTable dt)
        {
            SpInfo spInfo = new SpInfo();
            spInfo.DataValue = dt;
            spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
            spInfo.SpNameInsert = "UP_HR_WBARCODE_INSERT";
            spInfo.SpNameUpdate = "UP_HR_WBARCODE_UPDATE";
            spInfo.SpNameDelete = "UP_HR_WBARCODE_DELETE";
            spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
                                                   "NO_CARD",
                                                   "DT_WORK",
                                                   "TM_CARD",
                                                   "CD_WCODE",
                                                   "ID_INSERT" };
            spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
                                                   "NO_CARD",
                                                   "DT_WORK",
                                                   "TM_CARD",
                                                   "CD_WCODE",
                                                   "OLD_TM_CARD",
                                                   "ID_UPDATE" };
            spInfo.SpParamsDelete = new string[] { "CD_COMPANY",
                                                   "NO_CARD",
                                                   "DT_WORK",
                                                   "OLD_TM_CARD" };

            return DBHelper.Save(spInfo);
        }

        internal bool Save2(DataTable dt)
        {
            SpInfo spInfo = new SpInfo();
            spInfo.DataValue = dt;
            spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
            spInfo.SpNameUpdate = "SP_CZ_HR_WBARCODE_U";
            spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
                                                   "NO_CARD",
                                                   "DT_WORK",
                                                   "TM_001",
                                                   "TM_002",
                                                   "TM_003",
                                                   "TM_004",
                                                   "TM_001_OLD",
                                                   "TM_002_OLD",
                                                   "TM_003_OLD",
                                                   "TM_004_OLD",
                                                   "ID_UPDATE" };

            return DBHelper.Save(spInfo);
        }

        internal bool Save3(DataTable dt)
        {
            SpInfo spInfo = new SpInfo();
            spInfo.DataValue = dt;
            spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
            spInfo.SpNameUpdate = "SP_CZ_HR_WBARCODE_U1";
            spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
                                                   "NO_CARD",
                                                   "DT_WORK",
                                                   "TM_001",
                                                   "TM_002",
                                                   "TM_003",
                                                   "TM_004",
                                                   "TM_001_OLD",
                                                   "TM_002_OLD",
                                                   "TM_003_OLD",
                                                   "TM_004_OLD",
                                                   "ID_UPDATE" };

            return DBHelper.Save(spInfo);
        }
    }
}

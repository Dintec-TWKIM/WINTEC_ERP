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
    internal class P_CZ_SA_FORWARDER_RPT_BIZ
    {
        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_FORWARDER_RPT_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public bool SaveData(DataTable dt)
        {
            SpInfo si = new SpInfo();
            si.DataValue = dt;
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            si.UserID = Global.MainFrame.LoginInfo.UserID;

            si.SpNameUpdate = "SP_CZ_SA_FORWARDER_RPT_U";
            si.SpParamsUpdate = new string[] { "CD_COMPANY",
                                               "NO_GIR",
                                               "WEIGHT",
                                               "CD_SUB_CATEGORY",
                                               "AM_PRICE_001",
                                               "AM_PRICE_002",
                                               "AM_PRICE_004",
                                               "CD_FORWARDER",
                                               "FG_REASON",
                                               "ID_UPDATE" };

            return DBHelper.Save(si);
        }
    }
}

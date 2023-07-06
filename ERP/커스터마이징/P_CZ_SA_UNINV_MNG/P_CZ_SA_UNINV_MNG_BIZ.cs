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
    internal class P_CZ_SA_UNINV_MNG_BIZ
    {
        public DataTable Search(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_SA_UNINV_MNGH_S", obj);
        }

        public DataTable SearchDetail(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_SA_UNINV_MNGL_S", obj);
        }

        public bool Save(DataTable dtH, DataTable dtL)
        {
            SpInfoCollection sc = new SpInfoCollection();
            SpInfo si;

            if (dtH != null && dtH.Rows.Count > 0)
            {
                si = new SpInfo();

                si.DataValue = dtH;
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;
                si.SpNameUpdate = "SP_CZ_SA_UNINV_MNGH_U";

                si.SpParamsUpdate = new string[] { "CD_COMPANY",
                                                   "NO_SO",
                                                   "TP_SO",
                                                   "ID_UPDATE" };

                sc.Add(si);
            }

            if (dtL != null && dtL.Rows.Count > 0)
            {
                si = new SpInfo();

                si.DataValue = dtL;
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;
                si.SpNameUpdate = "SP_CZ_SA_UNINV_MNGL_U";

                si.SpParamsUpdate = new string[] { "CD_COMPANY",
                                                   "NO_SO",
                                                   "SEQ_SO",
                                                   "NO_IO",
                                                   "NO_IOLINE",
                                                   "YN_AM",
                                                   "TP_GI",
                                                   "DC2",
                                                   "DC_RMK1",
                                                   "ID_UPDATE" };

                sc.Add(si);
            }

            return DBHelper.Save(sc);
        }
    }
}

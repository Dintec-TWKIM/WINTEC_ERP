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
    internal class P_CZ_SA_IVMNG_INVOICE_BIZ
    {
        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_IVMNG_INVOICE_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        internal bool Save(DataTable dt)
        {
            SpInfo spInfo = new SpInfo();
            spInfo.DataValue = dt;
            spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            spInfo.UserID = Global.MainFrame.LoginInfo.UserID;

            spInfo.SpNameUpdate = "SP_CZ_SA_IVMNG_INVOICE_U";
            spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
                                                   "NO_INVOICE",
                                                   "DC_COMPANY",
                                                   "DC_ADDRESS",
                                                   "DC_TEL",
                                                   "CD_NATION",
                                                   "NO_IV",
                                                   "NO_IO",
                                                   "NM_CITY",
                                                   "CD_POSTAL",
                                                   "ID_UPDATE" };

            return DBHelper.Save(spInfo);
        }
    }
}

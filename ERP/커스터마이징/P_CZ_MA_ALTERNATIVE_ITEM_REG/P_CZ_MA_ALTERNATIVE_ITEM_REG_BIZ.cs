using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.ERPU;
using Duzon.Common.Util;
using Dintec;
using Duzon.Common.Forms;

namespace cz
{
    internal class P_CZ_MA_ALTERNATIVE_ITEM_REG_BIZ
    {
        internal DataTable Search1(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_MA_ALTERNATIVE_ITEM_REGH_S1", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search2(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_MA_ALTERNATIVE_ITEM_REGH_S2", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable SearchDetail(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_MA_ALTERNATIVE_ITEM_REGL_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal bool Save(DataTable dtH, DataTable dtL)
        {
            string xmlH = Util.GetTO_Xml(dtH);
            string xmlL = Util.GetTO_Xml(dtL);

            return (DBMgr.ExecuteNonQuery("SP_CZ_MA_ALTERNATIVE_ITEM_REG_XML", new object[] { xmlH,
                                                                                              xmlL,
                                                                                              Global.MainFrame.LoginInfo.UserID }) == -1 ? false : true);
        }
    }
}

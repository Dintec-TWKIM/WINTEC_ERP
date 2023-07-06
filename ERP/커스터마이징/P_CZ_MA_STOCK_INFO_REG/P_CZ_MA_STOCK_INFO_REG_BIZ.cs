using System.Data;
using Dintec;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;

namespace cz
{
    public class P_CZ_MA_STOCK_INFO_REG_BIZ
    {
        internal DataTable Search(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_MA_STOCK_INFO_REG_KCODE", obj);
        }

        internal DataTable Search1(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_MA_STOCK_INFO_REG_PARTNER", obj);
        }

        internal DataTable Search2(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_MA_STOCK_INFO_REG_UCODE", obj);
        }

        internal DataTable Search3(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_MA_STOCK_INFO_REG_ITEM", obj);
        }

        internal bool Save(DataTable dt)
        {
            return DBHelper.ExecuteNonQuery("SP_CZ_MA_STOCK_INFO_REG_XML", new object[] { GetTo.Xml(dt) });
        }

        internal bool Save1(DataTable dt)
        {
            return DBHelper.ExecuteNonQuery("SP_CZ_MA_STOCK_INFO_REG_UCODE_XML", new object[] { GetTo.Xml(dt) });
        }

        internal bool Save2(DataTable dt)
        {
            return DBHelper.ExecuteNonQuery("SP_CZ_MA_STOCK_INFO_REG_ITEM_XML", new object[] { GetTo.Xml(dt) });
        }
    }
}

using Dintec;
using Duzon.Common.Forms;
using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace cz
{
	internal class P_CZ_PU_HSD_STOCK_RPT_BIZ
	{
        internal DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PU_HSD_STOCK_RPT_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        internal DataTable Search1(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PU_HSD_STOCK_RPT_S1", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        internal bool SaveExcel(DataTable dt)
        {
            DataSet ds = new DataSet();
            DataRow[] dataRowArray;
            string xml = string.Empty;
            int groupUnit = 5000, index = 0;

            try
            {
                DBHelper.ExecuteScalar("DELETE FROM CZ_SA_HSD_DATA_LOG");

                dataRowArray = dt.AsEnumerable().ToArray();

                for (int i = 0; i < dataRowArray.Length; i += groupUnit)
                {
                    ds.Merge(dataRowArray.AsEnumerable().Skip(i).Take(groupUnit).ToArray());
                }

                foreach (DataTable dt1 in ds.Tables)
                {
                    MsgControl.ShowMsg("데이터 저장 중입니다.\n잠시만 기다려 주세요 (@/@)", new string[] { D.GetString(++index), D.GetString(ds.Tables.Count) });

                    xml = Util.GetTO_Xml(dt1);
                    DBHelper.ExecuteNonQuery("SP_CZ_PU_HSD_STOCK_RPT_EXCEL", new object[] { xml, Global.MainFrame.LoginInfo.UserID });
                }

                return true;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
            }

            return false;
        }
    }
}

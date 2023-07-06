using System.Data;
using Dintec;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;

namespace cz
{
    internal class P_CZ_FI_ACCT_EVIDENCE_REG_BIZ
    {
        internal DataTable Search(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_FI_ACCT_EVIDENCE_REGH_S", obj);
            return dataTable;
        }

        internal DataTable SearchDetail(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_FI_ACCT_EVIDENCE_REGL_S", obj);
            return dataTable;
        }

        internal bool Save(DataTable dtH, DataTable dtL)
        {
            SpInfoCollection sc = new SpInfoCollection();

            if (dtH != null && dtH.Rows.Count != 0)
            {
                #region Header
                SpInfo si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dtH);
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_FI_ACCT_EVIDENCE_REGH_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
                #endregion
            }

            if (dtL != null && dtL.Rows.Count != 0)
            {
                #region Line
                SpInfo si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dtL);
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_FI_ACCT_EVIDENCE_REGL_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                sc.Add(si);
                #endregion
            }

            return DBHelper.Save(sc);
        }
    }
}

using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using System.Data;

namespace cz
{
    internal class P_CZ_PR_OPOUT_IV_REG_BIZ
    {
        private string CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
        private string ID_USER = Global.MainFrame.LoginInfo.UserID;

        public DataSet search(object[] obj) => DBHelper.GetDataSet("UP_PR_OPOUT_IV_REG_SELECT", obj);

        public DataTable Day(string 거래처) => DBHelper.GetDataTable("UP_MA_PARTNER_SELECT_D", new object[] { this.CD_COMPANY, 거래처 });

        public bool save(DataTable dt, DataTable dtLine, string 처리일자, string 담당자)
        {
            SpInfoCollection spInfoCollection = new SpInfoCollection();
            if (dt != null)
            {
                SpInfo spInfo = new SpInfo();
                spInfo.DataValue = dt;
                spInfo.CompanyID = this.CD_COMPANY;
                spInfo.UserID = this.ID_USER;
                spInfo.SpNameInsert = "UP_PR_OPOUT_IVH_INSERT";
                spInfo.SpNameUpdate = "UP_PR_OPOUT_IVH_UPDATE";
                spInfo.SpNameDelete = "UP_PR_OPOUT_IVH_DELETE";
                spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
                                                       "CD_PLANT",
                                                       "NO_IV",
                                                       "DT_IV",
                                                       "CD_PARTNER",
                                                       "NO_EMP",
                                                       "CD_EXCH",
                                                       "RT_EXCH",
                                                       "FG_TAX",
                                                       "FG_TAXP",
                                                       "AM_CLS",
                                                       "AM_VAT",
                                                       "AM_HAP",
                                                       "YN_DOCU",
                                                       "NO_DOCU",
                                                       "CD_CC",
                                                       "FG_TPPURCHASE",
                                                       "DC_RMK",
                                                       "ID_INSERT",
                                                       "CD_BIZAREA_TAX",
                                                       "DT_PAY_PREARRANGED",
                                                       "FG_PAYBILL",
                                                       "DT_DUE" };
                spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
                                                       "CD_PLANT",
                                                       "NO_IV",
                                                       "AM_CLS",
                                                       "AM_VAT",
                                                       "AM_HAP",
                                                       "DC_RMK",
                                                       "ID_UPDATE",
                                                       "CD_BIZAREA_TAX",
                                                       "DT_PAY_PREARRANGED",
                                                       "FG_PAYBILL",
                                                       "DT_DUE" };
                spInfo.SpParamsDelete = new string[] { "CD_COMPANY",
                                                       "CD_PLANT",
                                                       "NO_IV" };
                spInfo.SpParamsValues.Add(ActionState.Insert, "DT_IV", 처리일자);
                spInfo.SpParamsValues.Add(ActionState.Insert, "NO_EMP", 담당자);
                spInfo.SpParamsValues.Add(ActionState.Insert, "FG_TAXP", "001");
                spInfoCollection.Add(spInfo);
            }
            if (dtLine != null)
                spInfoCollection.Add(new SpInfo()
                {
                    DataValue = dtLine,
                    CompanyID = this.CD_COMPANY,
                    UserID = this.ID_USER,
                    SpNameInsert = "UP_PR_OPOUT_IVL_INSERT",
                    SpNameUpdate = "UP_PR_OPOUT_IVL_UPDATE",
                    SpNameDelete = "UP_PR_OPOUT_IVL_DELETE",
                    SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "CD_PLANT",
                                                    "NO_IV",
                                                    "NO_LINE",
                                                    "CD_ITEM",
                                                    "QT_CLS",
                                                    "UM_EXCLS",
                                                    "UM_CLS",
                                                    "AM_EXCLS",
                                                    "AM_CLS",
                                                    "AM_VAT",
                                                    "AM_HAP",
                                                    "NO_WO",
                                                    "NO_WORK",
                                                    "NO_PO",
                                                    "NO_POLINE",
                                                    "CD_OP",
                                                    "CD_WC",
                                                    "CD_WCOP",
                                                    "CD_PJT",
                                                    "ID_INSERT",
                                                    "UM_MATL",
                                                    "UM_SOUL",
                                                    "SEQ_PROJECT",
                                                    "FG_TPPURCHASE" },
                    SpParamsUpdate = new string[] { "CD_COMPANY",
                                                    "CD_PLANT",
                                                    "NO_IV",
                                                    "NO_LINE",
                                                    "CD_ITEM",
                                                    "QT_CLS",
                                                    "UM_EXCLS",
                                                    "UM_CLS",
                                                    "AM_EXCLS",
                                                    "AM_CLS",
                                                    "AM_VAT",
                                                    "AM_HAP",
                                                    "NO_WO",
                                                    "NO_WORK",
                                                    "NO_PO",
                                                    "NO_POLINE",
                                                    "CD_OP",
                                                    "CD_WC",
                                                    "CD_WCOP",
                                                    "CD_PJT",
                                                    "ID_UPDATE",
                                                    "UM_MATL",
                                                    "UM_SOUL",
                                                    "SEQ_PROJECT",
                                                    "FG_TPPURCHASE" },
                    SpParamsDelete = new string[] { "CD_COMPANY",
                                                    "CD_PLANT",
                                                    "NO_IV",
                                                    "NO_LINE" }
                });
            return DBHelper.Save(spInfoCollection);
        }
    }
}
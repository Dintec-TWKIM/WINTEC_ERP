using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using System.Data;

namespace cz
{
	internal class P_PU_PI_REG_BIZ
	{
        public DataSet Search(string P_CD_COMPANY, string P_NO_SV, string P_Fg_Acct, string A, string B, string C, string P_CD_PJT, string YN_PJT)
        {
            ResultData resultData = (ResultData)Global.MainFrame.FillDataSet("UP_PU_PI_SELECT", new object[8] { P_CD_COMPANY,
                                                                                                                P_NO_SV,
                                                                                                                P_Fg_Acct,
                                                                                                                A,
                                                                                                                B,
                                                                                                                C,
                                                                                                                P_CD_PJT,
                                                                                                                Global.SystemLanguage.MultiLanguageLpoint });
            DataSet dataValue = (DataSet)resultData.DataValue;
            DataTable table = dataValue.Tables[0];
            table.Columns["CD_PLANT"].DefaultValue = Global.MainFrame.LoginInfo.CdPlant;
            table.Columns.Add("QT_YN", typeof(string));
            table.Columns["QT_YN"].DefaultValue = "N";
            T.SetDefaultValue(dataValue);
            return (DataSet)resultData.DataValue;
        }

        public ResultData Delete(object[] m_obj) => (ResultData)Global.MainFrame.ExecSp("UP_PU_MM_ISVH_DELETE", m_obj);

        public ResultData[] Save(DataTable dtH, DataTable dtL)
        {
            SpInfoCollection spCollection = new SpInfoCollection();
            string mngLot = Global.MainFrame.LoginInfo.MngLot;
            spCollection.Add(new SpInfo()
            {
                DataValue = dtH,
                SpNameInsert = "UP_PU_MM_ISVH_INSERT",
                SpNameUpdate = "UP_PU_MM_ISVH_UPDATE",
                SpParamsInsert = new string[] { "NO_SV",
                                                "CD_COMPANY",
                                                "CD_SL",
                                                "CD_PLANT",
                                                "DT_SV",
                                                "CD_DEPT",
                                                "NO_EMP",
                                                "DC_RMK",
                                                "DTS_INSERT1",
                                                "ID_INSERT1" },
                SpParamsUpdate = new string[] { "NO_SV",
                                                "CD_COMPANY",
                                                "CD_DEPT",
                                                "NO_EMP",
                                                "DC_RMK",
                                                "DTS_INSERT1",
                                                "ID_INSERT1" },
                SpParamsValues = { { ActionState.Insert,
                                     "DTS_INSERT1",
                                     Global.MainFrame.GetStringDetailToday },
                                   { ActionState.Insert,
                                     "ID_INSERT1",
                                     Global.MainFrame.LoginInfo.UserID },
                                   { ActionState.Update,
                                     "DTS_INSERT1",
                                     Global.MainFrame.GetStringDetailToday },
                                   { ActionState.Update,
                                     "ID_INSERT1",
                                     Global.MainFrame.LoginInfo.UserID } }
            });
            SpInfo spInfo = new SpInfo();
            dtL.RemotingFormat = SerializationFormat.Binary;
            spInfo.DataValue = dtL;
            spInfo.SpNameInsert = "UP_PU_MM_ISVL_INSERT";
            spInfo.SpNameUpdate = "UP_PU_MM_ISVL_UPDATE";
            spInfo.SpNameDelete = "UP_PU_MM_ISVL_DELETE";
            spInfo.SpParamsInsert = new string[] { "NO_SV",
                                                   "CD_COMPANY",
                                                   "CD_ITEM",
                                                   "QT_GOOD_INV",
                                                   "QT_GOOD_INV2",
                                                   "QT_REJECT_INV",
                                                   "QT_REJECT_INV2",
                                                   "QT_TRANS_INV",
                                                   "QT_TRANS_INV2",
                                                   "QT_INSP_INV",
                                                   "QT_INSP_INV2",
                                                   "YN_BLOCK",
                                                   "CD_PJT",
                                                   "SEQ_PROJECT" };
            spInfo.SpParamsUpdate = new string[] { "NO_SV",
                                                   "CD_COMPANY",
                                                   "CD_ITEM",
                                                   "QT_GOOD_INV2",
                                                   "QT_REJECT_INV2",
                                                   "QT_TRANS_INV2",
                                                   "QT_INSP_INV2",
                                                   "YN_BLOCK",
                                                   "CD_PJT",
                                                   "SEQ_PROJECT" };
            spInfo.SpParamsDelete = new string[] { "NO_SV",
                                                   "CD_COMPANY",
                                                   "CD_ITEM" };
            spCollection.Add(spInfo);
            return (ResultData[])Global.MainFrame.Save(spCollection);
        }

        public DataSet 품목전개(object[] m_obj) => DBHelper.GetDataSet("UP_CZ_PU_PI_ITEM_EXP_SELECT", m_obj, new string[] { "CD_ITEM" });

        public bool SaveMES(object[] m_obj) => DBHelper.ExecuteNonQuery("UP_PU_Z_ONEGENE_PI_MES_I", m_obj);

        public string 재고실사등록여부(string p_CD_SL, string p_DT_MON)
        {
            string empty = string.Empty;
            DataTable dataTable = DBHelper.GetDataTable(string.Format("SELECT NO_SV \r\n                                     FROM MM_ISVH \r\n                                    WHERE CD_COMPANY = '{0}' \r\n                                      AND CD_SL = '{1}' \r\n                                      AND DT_SV LIKE '{2}%'", Global.MainFrame.LoginInfo.CompanyCode, p_CD_SL, p_DT_MON.Substring(0, 6)));
            if (dataTable != null && dataTable.Rows.Count > 0)
                empty = D.GetString(dataTable.Rows[0]["NO_SV"]);
            return empty;
        }

        public DataTable Get_PJTInfo(string p_cd_pjt_pipe) => DBHelper.GetDataTable("UP_PU_COMMON_PJT_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                                         p_cd_pjt_pipe,
                                                                                                                         Global.SystemLanguage.MultiLanguageLpoint });

        public DataTable Get_ITEMInfo(string p_cd_ITEM_pipe, string cd_plant) => DBHelper.GetDataTable("UP_MM_PITEM_SELECT", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                                                            cd_plant,
                                                                                                                                            p_cd_ITEM_pipe,
                                                                                                                                            Global.SystemLanguage.MultiLanguageLpoint });

        public DataTable Get_QTINV(object[] obj) => DBHelper.GetDataTable("UP_PU_PI_ITEM_QT_S", obj);
    }
}
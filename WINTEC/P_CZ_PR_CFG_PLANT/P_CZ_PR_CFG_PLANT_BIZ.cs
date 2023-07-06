using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using System;
using System.Data;

namespace cz
{
	internal class P_CZ_PR_CFG_PLANT_BIZ
	{
        private string CD_COMPNAY = Global.MainFrame.LoginInfo.CompanyCode;
        private string ID_USER = Global.MainFrame.LoginInfo.UserID;

        public DataTable Search(string CD_PLANT, string NO_CAL)
        {
            return DBHelper.GetDataTable("SP_CZ_PR_CFG_PLANT_SELECT", new string[] { CD_COMPNAY, CD_PLANT, NO_CAL });
        }

        public bool Save(DataTable _dt_Sft)
        {
            SpInfoCollection spc = new SpInfoCollection();
            if (_dt_Sft != null && _dt_Sft.Rows.Count > 0)
                spc.Add(new SpInfo()
                {
                    DataValue = _dt_Sft,
                    CompanyID = this.CD_COMPNAY,
                    UserID = this.ID_USER,
                    SpNameInsert = "SP_CZ_PR_SHIFT_INSERT",
                    SpNameUpdate = "SP_CZ_PR_SHIFT_UPDATE",
                    SpNameDelete = "SP_CZ_PR_SHIFT_DELETE",
                    SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "CD_PLANT",
                                                    "NO_CAL",
                                                    "TP_START",
                                                    "TM_START",
                                                    "TP_END",
                                                    "TM_END",
                                                    "TM_STOP",
                                                    "TM_SFT",
                                                    "QT_WORKER",
                                                    "CD_DEPT",
                                                    "YN_USE",
                                                    "DC_RMK",
                                                    "ID_INSERT" },
                    SpParamsUpdate = new string[] { "CD_COMPANY",
                                                    "CD_PLANT",
                                                    "NO_CAL",
                                                    "TP_START",
                                                    "TM_START",
                                                    "TP_END",
                                                    "TM_END",
                                                    "TM_STOP",
                                                    "TM_SFT",
                                                    "QT_WORKER",
                                                    "CD_DEPT",
                                                    "YN_USE",
                                                    "DC_RMK",
                                                    "ID_UPDATE"},
                    SpParamsDelete = new string[] { "CD_COMPANY",
                                                    "CD_PLANT",
                                                    "NO_CAL",
                                                    "TP_START",
                                                    "TM_START" }
                });
            return DBHelper.Save(spc);
        }
    }
}
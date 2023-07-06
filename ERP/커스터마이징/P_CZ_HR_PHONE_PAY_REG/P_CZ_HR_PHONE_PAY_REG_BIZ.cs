using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Duzon.ERPU;
using System.Data;
using Duzon.Common.Util;
using Duzon.Common.Forms;


namespace cz 
{
    internal class P_CZ_HR_PHONE_PAY_REG_BIZ
    {
        //  CZ_HR_PHONE_PAY_REG
        internal DataTable Search(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_HR_PHONE_PAY_REG_S", obj);
        }

        internal bool Save(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return true;

            SpInfo spInfo = new SpInfo();
            spInfo.DataValue = dt;
            spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
            spInfo.SpNameInsert = "SP_CZ_HR_PHONE_PAY_REG_I";
            spInfo.SpParamsInsert = new string[] { "YM",
                                                   "CD_COMPANY",
                                                   "NO_EMP",
                                                   "GUBUN",
                                                   "DC_RMK",
                                                   "ID_INSERT"
            };
            spInfo.SpNameUpdate = "SP_CZ_HR_PHONE_PAY_REG_U";
            spInfo.SpParamsUpdate = new string[] { "SEQ",
                                                   "YM",
                                                   "CD_COMPANY",
                                                   "NO_EMP",
                                                   "GUBUN",
                                                   "DC_RMK",
                                                   "ID_UPDATE" 
            };
            spInfo.SpNameDelete = "SP_CZ_HR_PHONE_PAY_REG_D";
            spInfo.SpParamsDelete = new string[] { "SEQ"};
            return DBHelper.Save(spInfo);
        }

        public bool CopyPaste(string YM, string YM2, string ID_INSERT, string CD_COMPANY)
        {
            object[] data = new string[] { YM,YM2,ID_INSERT,CD_COMPANY };
            return DBHelper.ExecuteNonQuery("SP_CZ_HR_PHONE_PAY_REG_C", data);
        }

    }
}

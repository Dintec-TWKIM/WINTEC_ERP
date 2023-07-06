using System;
using System.Data;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using System.IO;
using Dintec;

namespace cz
{
    internal class P_MA_IPLIST_BIZ
    {
        internal DataTable Search(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_MA_IP_LIST", obj);
        }

        internal bool Save(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return true;


            dt.Rows[0]["ID_UPDATE"] = Global.MainFrame.LoginInfo.UserID;
            dt.Rows[0]["ID_INSERT"] = Global.MainFrame.LoginInfo.UserID;

            SpInfo spInfo = new SpInfo();
            spInfo.DataValue = dt;
            spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            spInfo.UserID = Global.MainFrame.LoginInfo.UserName;
            spInfo.SpNameInsert = "UP_MA_IP_LIST_INSERT";
            spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
                                                   "NO_EMP",
                                                   "IP_ADDR",
                                                   "MAC_ADDR",
                                                   "NM_PC",
                                                   "DT_PC",
                                                   "SERIAL_PC",
                                                   "KEY_PC",
                                                   "OFFICE_KEY",
                                                   "OS_KEY",
                                                   "SPEC",
                                                   //"CD_COMPANY_1",
                                                   //"NO_EMP_1",
                                                   //"CD_COMPANY_2",
                                                   //"NO_EMP_2",
                                                   //"CD_COMPANY_3",
                                                   //"NO_EMP_3",
                                                   //"CD_COMPANY_4",
                                                   //"NO_EMP_4",
                                                   //"CD_COMPANY_5",
                                                   //"NO_EMP_5",
                                                   //"CD_COMPANY_6",
                                                   //"NO_EMP_6",
                                                   //"CD_COMPANY_7",
                                                   //"NO_EMP_7",
                                                   //"CD_COMPANY_8",
                                                   //"NO_EMP_8",
                                                   //"CD_COMPANY_9",
                                                   //"NO_EMP_9",
                                                   //"CD_COMPANY_10",
                                                   //"NO_EMP_10",
                                                   "DC_RMK",
                                                   "ID_INSERT"
            };
            spInfo.SpNameUpdate = "UP_MA_IP_LIST_UPDATE";
            spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
                                                   "SEQ",
                                                   "NO_EMP",
                                                   "IP_ADDR",
                                                   "MAC_ADDR",
                                                   "NM_PC",
                                                   "DT_PC",
                                                   "SERIAL_PC",
                                                   "KEY_PC",
                                                   "OFFICE_KEY",
                                                   "OS_KEY",
                                                   "SPEC",
                                                   //"CD_COMPANY_1",
                                                   //"NO_EMP_1",
                                                   //"CD_COMPANY_2",
                                                   //"NO_EMP_2",
                                                   //"CD_COMPANY_3",
                                                   //"NO_EMP_3",
                                                   //"CD_COMPANY_4",
                                                   //"NO_EMP_4",
                                                   //"CD_COMPANY_5",
                                                   //"NO_EMP_5",
                                                   //"CD_COMPANY_6",
                                                   //"NO_EMP_6",
                                                   //"CD_COMPANY_7",
                                                   //"NO_EMP_7",
                                                   //"CD_COMPANY_8",
                                                   //"NO_EMP_8",
                                                   //"CD_COMPANY_9",
                                                   //"NO_EMP_9",
                                                   //"CD_COMPANY_10",
                                                   //"NO_EMP_10",
                                                   "DC_RMK",
                                                   "ID_UPDATE" 
            };
            spInfo.SpNameDelete = "UP_MA_IP_LIST_DELETE";
            spInfo.SpParamsDelete = new string[] { "CD_COMPANY",
                                                   "SEQ"};
            return DBHelper.Save(spInfo);
        }
    }
}

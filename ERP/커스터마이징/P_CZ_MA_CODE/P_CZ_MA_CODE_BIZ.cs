using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.ERPU;
using Duzon.Common.Forms;
using Duzon.Common.Util;

namespace P_CZ_MA_CODE
{
    public class P_CZ_MA_CODE_BIZ
    {
        internal DataTable Search(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_MA_IP_LIST", obj);
        }

        internal bool Save(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return true;


            //dt.Rows[0]["ID_UPDATE"] = Global.MainFrame.LoginInfo.UserID;
            //dt.Rows[0]["ID_INSERT"] = Global.MainFrame.LoginInfo.UserID;

            SpInfo spInfo = new SpInfo();
            spInfo.DataValue = dt;
            spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
            spInfo.SpNameInsert = "UP_MA_CODE_INSERT";
            spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
                                                   "CD_FIELD",
                                                   "CD_SYSDEF",
                                                   "FG1_SYSCODE",
                                                   "NM_SYSDEF",
                                                   "USE_YN",
                                                   "CD_FLAG1",
                                                   "CD_FLAG2",
                                                   "CD_FLAG3",
                                                   //"NM_SYSDEF_E",
												   "NM_SYSDEF_L1",
                                                   "ID_INSERT",
                                                   "NO_ORDER",
                                                   //"NM_SYSDEF_CH",
												   "NM_SYSDEF_L2",
                                                   //"NM_SYSDEF_JP",
												   "NM_SYSDEF_L3",
                                                   "NM_SYSDEF_L4",
                                                   "NM_SYSDEF_L5",
                                                   "NM_USERDEF_L1",
                                                   "NM_USERDEF_L2",
            };
            spInfo.SpNameUpdate = "UP_MA_CODE_UPDATE";
            spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
												   "CD_FIELD",
												   "CD_SYSDEF",
												   "FG1_SYSCODE",
												   "NM_SYSDEF",
												   "USE_YN",
												   "CD_FLAG1",
												   "CD_FLAG2",
												   "CD_FLAG3",
                                                   //"NM_SYSDEF_E",
												   "NM_SYSDEF_L1",
												   "ID_INSERT",
												   "NO_ORDER",
                                                   //"NM_SYSDEF_CH",
												   "NM_SYSDEF_L2",
                                                   //"NM_SYSDEF_JP",
												   "NM_SYSDEF_L3",
												   "NM_SYSDEF_L4",
												   "NM_SYSDEF_L5",
												   "NM_USERDEF_L1",
												   "NM_USERDEF_L2",
			};
            spInfo.SpNameDelete = "UP_MA_CODE_DELETE";
            spInfo.SpParamsDelete = new string[] { "CD_COMPANY",
                                                   "CD_FIELD",
                                                   "CD_SYSDEF"};
            return DBHelper.Save(spInfo);
        }


		internal bool SaveL1(DataTable dt)
		{
			if (dt == null || dt.Rows.Count == 0)
				return true;


			SpInfo spInfo = new SpInfo();
			spInfo.DataValue = dt;
			spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
			spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
			
			spInfo.SpNameUpdate = "UP_MA_CODE_UPDATE";
			spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
												   "CD_FIELD",
												   "CD_SYSDEF",
												   "FG1_SYSCODE",
												   "NM_SYSDEF",
												   "USE_YN",
												   "CD_FLAG1",
												   "CD_FLAG2",
												   "CD_FLAG3",
												   "NM_SYSDEF_L1",
												   "ID_UPDATE",
												   "NO_ORDER"
                                                   //"",
                                                   //"",
                                                   //"",
                                                   //"",
                                                   //"",
                                                   //""
            };
			return DBHelper.Save(spInfo);
		}
	}
}

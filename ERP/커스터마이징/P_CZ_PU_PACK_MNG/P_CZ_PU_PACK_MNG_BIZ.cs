using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.ERPU;
using Duzon.Common.Util;
using Duzon.Common.Forms;

namespace cz
{
    internal class P_CZ_PU_PACK_MNG_BIZ
    {
        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PU_PACK_MNGH_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchDetail(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PU_PACK_MNGL_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        internal bool Save(DataTable dtH)
        {
            SpInfo si = new SpInfo();

            si.DataValue = dtH;
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            si.UserID = Global.MainFrame.LoginInfo.UserID;

            si.SpNameUpdate = "SP_CZ_PU_PACK_MNGH_U";
            si.SpParamsUpdate = new string[] 
			{ 
				"CD_COMPANY",
				"NO_PO",
				"NO_PACK",			
				"DC_RMK",	
				"ID_UPDATE"				
			};

            si.SpNameDelete = "SP_CZ_PU_PACK_MNG_D";
            si.SpParamsDelete = new string[] 
			{ 
				"CD_COMPANY",
				"NO_PO",
				"NO_PACK"
			};

            return DBHelper.Save(si);
        }
    }
}

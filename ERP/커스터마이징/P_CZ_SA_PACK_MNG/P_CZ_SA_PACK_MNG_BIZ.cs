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
	class P_CZ_SA_PACK_MNG_BIZ
	{
		public DataTable Search(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_PACK_MNGH_S", obj);
			T.SetDefaultValue(dt);
			return dt;
		}

		public DataTable SearchDetail(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_PACK_MNGL_S", obj);
			T.SetDefaultValue(dt);
			return dt;
		}

        public DataSet 협조전데이터(object[] obj)
        {
            DataSet ds = DBHelper.GetDataSet("SP_CZ_SA_PACK_MNG_GIR_S", obj);
            T.SetDefaultValue(ds);
            return ds;
        }

		internal bool Save(DataTable dtH)
		{
            SpInfo si = new SpInfo();

            si.DataValue = dtH;
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            si.UserID = Global.MainFrame.LoginInfo.UserID;

            si.SpNameUpdate = "SP_CZ_SA_PACK_REGH_U";
            si.SpParamsUpdate = new string[] 
		    { 
		    	"CD_COMPANY",
		    	"NO_GIR",
		    	"NO_PACK",
		    	"NM_PACK",
		    	"DT_PACK",
		    	"CD_TYPE",		
		    	"CD_SIZE",		
		    	"QT_NET_WEIGHT",			
		    	"QT_GROSS_WEIGHT",				
		    	"QT_WIDTH",		
		    	"QT_LENGTH",			
		    	"QT_HEIGHT",			
		    	"DC_RMK",	
		    	"ID_UPDATE"				
		    };

            si.SpNameDelete = "SP_CZ_SA_PACK_MNG_D";
            si.SpParamsDelete = new string[] 
			{ 
				"CD_COMPANY",
				"NO_GIR",
				"NO_PACK",
                "ID_DELETE"
			};

            si.SpParamsValues.Add(ActionState.Delete, "ID_DELETE", Global.MainFrame.LoginInfo.UserID);

            return DBHelper.Save(si);
		}
	}
}

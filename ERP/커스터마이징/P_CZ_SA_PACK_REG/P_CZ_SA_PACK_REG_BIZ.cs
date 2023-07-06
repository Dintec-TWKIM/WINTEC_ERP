using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Duzon.ERPU;
using Duzon.Common.Util;
using System.Data;
using Duzon.Common.Forms;
using System.IO;
using Dintec;

namespace cz
{
	internal class P_CZ_SA_PACK_REG_BIZ
	{
		public DataTable Search(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_PACK_REGH_S", obj);
			T.SetDefaultValue(dt);
			return dt;
		}

		public DataTable SearchDetail(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_PACK_REGL_S", obj);
			T.SetDefaultValue(dt);
			return dt;
		}

        public bool Delete(object[] obj)
        {
            ResultData result = (ResultData)Global.MainFrame.ExecSp("SP_CZ_SA_PACK_REG_D", obj);
            return result.Result;
        }

		internal bool Save(string 회사코드, string 의뢰번호, DataTable dtH, DataTable dtL)
		{
			SpInfoCollection sc = new SpInfoCollection();

			if (dtH != null && dtH.Rows.Count != 0)
			{
				#region Header
				SpInfo si = new SpInfo();

				si.DataValue = dtH;
                si.CompanyID = 회사코드;
				si.UserID = Global.MainFrame.LoginInfo.UserID;

				si.SpNameInsert = "SP_CZ_SA_PACK_REGH_I";
				si.SpParamsInsert = new string[] 
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
					"ID_INSERT"		
				};

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

				si.SpNameDelete = "SP_CZ_SA_PACK_REGH_D";
				si.SpParamsDelete = new string[] 
				{ 
					"CD_COMPANY",
					"NO_GIR",
					"NO_PACK",
                    "ID_DELETE"
				};

				si.SpParamsValues.Add(ActionState.Insert, "NO_GIR", 의뢰번호);
				si.SpParamsValues.Add(ActionState.Update, "NO_GIR", 의뢰번호);
				si.SpParamsValues.Add(ActionState.Delete, "NO_GIR", 의뢰번호);
                si.SpParamsValues.Add(ActionState.Delete, "ID_DELETE", Global.MainFrame.LoginInfo.UserID);

				sc.Add(si);
				#endregion
			}

			if (dtL != null && dtL.Rows.Count != 0)
			{
				#region Line
				SpInfo si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dtL);
                si.CompanyID = 회사코드;
				si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_SA_PACK_REG_XML";
                si.SpParamsInsert = new string[] { "CD_COMPANY", "NO_GIR", "XML", "ID_INSERT" };

				si.SpParamsValues.Add(ActionState.Insert, "NO_GIR", 의뢰번호);

				sc.Add(si);
				#endregion
			}

			if (sc.List == null) return false;

			return DBHelper.Save(sc);
		}
	}
}

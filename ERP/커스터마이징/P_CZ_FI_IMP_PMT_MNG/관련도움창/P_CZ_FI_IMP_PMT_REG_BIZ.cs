using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.Common.Util;
using Duzon.Common.Forms;
using Duzon.ERPU;
using System.IO;
using Dintec;

namespace cz
{
	internal class P_CZ_FI_IMP_PMT_REG_BIZ
	{
        internal DataTable Search(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_FI_IMP_PMT_REGH_S", obj);
			T.SetDefaultValue(dt);
			return dt;
		}

		internal DataTable SearchDetail(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_FI_IMP_PMT_REGL_S", obj);
			T.SetDefaultValue(dt);
			return dt;
		}

		internal bool SaveData(DataTable dtH, DataTable dtL, object[] obj)
		{
			SpInfoCollection sc = new SpInfoCollection();

            if (dtH != null && dtH.Rows.Count > 0)
            {
                SpInfo si = new SpInfo();
                si.DataValue = dtH;
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;
                si.SpNameInsert = "SP_CZ_FI_IMP_PMT_REGH_I";
                si.SpParamsInsert = new string[] { "CD_COMPANY",
                                                   "NO_IMPORT",	
	                                               "NO_BL",
                                                   "DT_IMPORT",
                                                   "CD_PAYMENT",
		                                           "DT_LIMIT",
                                                   "AM_VAT_BASE",
                                                   "AM_VAT",
                                                   "NO_EMP",
                                                   "ID_INSERT" };

                si.SpParamsValues.Add(ActionState.Insert, "NO_IMPORT", D.GetString(obj[0]));
                si.SpParamsValues.Add(ActionState.Insert, "NO_BL", D.GetString(obj[1]));
                si.SpParamsValues.Add(ActionState.Insert, "DT_IMPORT", D.GetString(obj[2]));
                si.SpParamsValues.Add(ActionState.Insert, "CD_PAYMENT", D.GetString(obj[3]));
                si.SpParamsValues.Add(ActionState.Insert, "DT_LIMIT", D.GetString(obj[4]));
                si.SpParamsValues.Add(ActionState.Insert, "AM_VAT_BASE", D.GetDecimal(obj[5]));
                si.SpParamsValues.Add(ActionState.Insert, "AM_VAT", D.GetDecimal(obj[6]));
                si.SpParamsValues.Add(ActionState.Insert, "NO_EMP", Global.MainFrame.LoginInfo.EmployeeNo);

                sc.Add(si);
            }

			if (dtL != null && dtL.Rows.Count > 0)
			{
				SpInfo si = new SpInfo();
				si.DataValue = Util.GetXmlTable(dtL);
				si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
				si.UserID = Global.MainFrame.LoginInfo.UserID;
                si.SpNameInsert = "SP_CZ_FI_IMP_PMT_MNGL_XML";
				si.SpParamsInsert = new string[] { "CD_COMPANY", "NO_IMPORT", "XML", "ID_INSERT" };

				si.SpParamsValues.Add(ActionState.Insert, "NO_IMPORT", D.GetString(obj[0]));

				sc.Add(si);
			}

			if (sc.List != null && sc.List.Count > 0)
				return DBHelper.Save(sc);
			else
				return true;
		}
	}
}

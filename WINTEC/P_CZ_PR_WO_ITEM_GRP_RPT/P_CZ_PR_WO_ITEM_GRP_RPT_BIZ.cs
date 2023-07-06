using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace cz
{
	internal class P_CZ_PR_WO_ITEM_GRP_RPT_BIZ
	{
		public DataTable Search(string 품목그룹, object[] obj)
		{
			DataTable dt = null;

			switch (품목그룹)
			{
				case "UEC01":
					dt = DBHelper.GetDataTable("SP_CZ_PR_WO_ITEM_GRP_RPT_UEC01_S", obj);
					break;
				case "VC01":
					dt = DBHelper.GetDataTable("SP_CZ_PR_WO_ITEM_GRP_RPT_VC01_S", obj);
					break;
				case "VC02":
					dt = DBHelper.GetDataTable("SP_CZ_PR_WO_ITEM_GRP_RPT_VC02_S", obj);
					break;
				case "VC03":
					dt = DBHelper.GetDataTable("SP_CZ_PR_WO_ITEM_GRP_RPT_VC03_S", obj);
					break;
				case "VC04":
					dt = DBHelper.GetDataTable("SP_CZ_PR_WO_ITEM_GRP_RPT_VC04_S", obj);
					break;
				case "VC05":
					dt = DBHelper.GetDataTable("SP_CZ_PR_WO_ITEM_GRP_RPT_VC05_S", obj);
					break;
				case "VP01":
					dt = DBHelper.GetDataTable("SP_CZ_PR_WO_ITEM_GRP_RPT_VP01_S", obj);
					break;
				case "VC06":
					dt = DBHelper.GetDataTable("SP_CZ_PR_WO_ITEM_GRP_RPT_VC06_S", obj);
					break;
				default:
					return null;
			}

			T.SetDefaultValue(dt);
			return dt;
		}

		public DataTable SearchDetail(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_WO_ITEM_GRP_RPT_L_S", obj);
			return dt;
		}

		public DataTable SearchDetail1(object[] obj)
        {
			DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_WO_ITEM_GRP_RPT_DL_S", obj);
			return dt;
        }

		public void TRUST마킹(object[] obj)
		{
			DBHelper.ExecuteNonQuery("SP_CZ_PR_WO_ITEM_GRP_RPT_TSMK_I", obj);
        }
		public void 검사신청(object[] obj)
		{
			DBHelper.ExecuteNonQuery("SP_CZ_QU_INSP_SCHEDULE_I", obj);
		}

		public void 납품의뢰(object[] obj)
		{
			DBHelper.ExecuteNonQuery("SP_CZ_PR_WO_ITEM_GRP_RPT_GIR_I", obj);
		}

		public void 포장완료(object[] obj)
		{
			DBHelper.ExecuteNonQuery("SP_CZ_PR_WO_ITEM_GRP_RPT_GIR_PACK", obj);
		}
    }
}

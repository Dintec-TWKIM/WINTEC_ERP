using Dintec;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace cz
{
	internal class P_CZ_PR_WO_ROUT_EQUIP_MNG_BIZ
	{
		public DataTable Search(object[] obj)
		{
			return DBHelper.GetDataTable("SP_CZ_PR_WO_ROUT_EQUIP_MNG_S", obj);
		}

		public DataTable Search1(object[] obj)
		{
			return DBHelper.GetDataTable("SP_CZ_PR_WO_ROUT_EQUIP_MNG_S1", obj);
		}

		public DataTable SearchEq(object[] obj)
		{
			return DBHelper.GetDataTable("SP_CZ_PR_WO_ROUT_EQUIP_MNG_EQ", obj);
		}

		public DataTable SearchWo(object[] obj)
		{
			return DBHelper.GetDataTable("SP_CZ_PR_WO_ROUT_EQUIP_MNG_WO", obj);
		}

		internal void Save(object[] obj)
		{
			string query = @"UPDATE A
                             SET A.CD_EQUIP = '{3}',
							     A.ID_UPDATE = '{4}', 
							     A.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
							 FROM PR_WO_ROUT A
							 WHERE A.CD_COMPANY = '{0}'
							 AND A.NO_WO = '{1}'
							 AND A.NO_LINE = '{2}'";

			query = string.Format(query, obj);

			DBHelper.ExecuteScalar(query);
		}
	}
}

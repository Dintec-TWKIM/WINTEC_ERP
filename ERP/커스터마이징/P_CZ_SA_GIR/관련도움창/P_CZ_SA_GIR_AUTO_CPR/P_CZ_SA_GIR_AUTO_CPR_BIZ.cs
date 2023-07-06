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
	internal class P_CZ_SA_GIR_AUTO_CPR_BIZ
	{
		public DataTable Search(string 회사코드, string 협조전번호)
		{
			return DBHelper.GetDataTable("SP_CZ_SA_GIR_AUTO_CPR_S", new object[] { 회사코드, 협조전번호 });
		}

		internal void Save(string 회사코드, string 의뢰번호, string CPR발송메일, string 자동발송제외, string 상업송장비고, string 전달사항, string CPR발송완료)
		{
			string query = @"UPDATE A
SET A.NO_DELIVERY_EMAIL = '{2}',
	A.YN_EXCLUDE_CPR = '{3}',
	A.DC_RMK_CI = '{4}',
	A.DC_RMK_CPR = '{5}',
	A.YN_CPR = '{6}'
FROM CZ_SA_GIRH_WORK_DETAIL A
WHERE A.CD_COMPANY = '{0}'
AND A.NO_GIR = '{1}'";

			DBHelper.ExecuteScalar(string.Format(query, new object[] { 회사코드, 의뢰번호, CPR발송메일, 자동발송제외, 상업송장비고, 전달사항, CPR발송완료 }));
		}
	}
}

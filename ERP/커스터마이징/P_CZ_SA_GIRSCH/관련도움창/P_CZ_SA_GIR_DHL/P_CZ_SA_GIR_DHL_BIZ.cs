using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace cz
{
	internal class P_CZ_SA_GIR_DHL_BIZ
	{
        public DataTable Search(string 회사코드, string 협조전번호)
        {
            string query;

            query = @"SELECT GL.CD_COMPANY,
	   GL.NO_INV,
	   IH.REMARK1,
	   IH.REMARK3,
	   IH.REMARK4,
	   IH.REMARK5,
	   IH.NO_TO AS CD_COUNTRY,
	   MC.NM_SYSDEF AS NM_COUNTRY,
	   IH.PORT_ARRIVER,
	   ISNULL(IH.YN_INSURANCE, 'N') AS YN_INSURANCE,
	   ISNULL(IH.YN_DUTY, 'N') AS YN_DUTY,
	   IH.ADDR1_CONSIGNEE,
       IH.ADDR2_CONSIGNEE
FROM (SELECT GL.CD_COMPANY, GL.NO_GIR,
		     MAX(NO_INV) AS NO_INV
	  FROM SA_GIRL GL WITH(NOLOCK)
	  GROUP BY GL.CD_COMPANY, GL.NO_GIR
	  UNION ALL
	  SELECT GL.CD_COMPANY, GL.NO_GIR,
		     MAX(NO_INV) AS NO_INV
	  FROM CZ_SA_GIRL_PACK GL WITH(NOLOCK)
	  GROUP BY GL.CD_COMPANY, GL.NO_GIR) GL
JOIN CZ_TR_INVH IH WITH(NOLOCK) ON IH.CD_COMPANY = GL.CD_COMPANY AND IH.NO_INV = GL.NO_INV
LEFT JOIN CZ_MA_CODEDTL MC ON MC.CD_COMPANY = GL.CD_COMPANY AND MC.CD_FIELD = 'CZ_MA00004' AND MC.CD_SYSDEF = IH.NO_TO
WHERE GL.CD_COMPANY = '{0}'
AND GL.NO_GIR = '{1}'";

            query = string.Format(query, 회사코드, 협조전번호);

            return DBHelper.GetDataTable(query);
        }

        internal void Save(string 회사코드, string 송장번호, string TEL, string PIC, string BAN, string 우편번호, string 국가, string 보험유무, string 관세납부여부, string 도착지, string 주소1, string 주소2)
        {
			string query = @"UPDATE CZ_TR_INVH
						     SET REMARK1 = '{2}',
								 REMARK3 = '{3}',
								 REMARK4 = '{4}',
								 REMARK5 = '{5}',
								 NO_TO = '{6}',
							     YN_INSURANCE = '{7}',
								 YN_DUTY = '{8}',
								 PORT_ARRIVER = '{9}',
							     ADDR1_CONSIGNEE = '{10}',
								 ADDR2_CONSIGNEE = '{11}'
						     WHERE CD_COMPANY = '{0}'
						     AND NO_INV = '{1}'";

			query = string.Format(query, 회사코드, 송장번호, TEL, PIC, BAN, 우편번호, 국가, 보험유무, 관세납부여부, 도착지, 주소1, 주소2);

			DBHelper.ExecuteScalar(query);
        }
    }
}

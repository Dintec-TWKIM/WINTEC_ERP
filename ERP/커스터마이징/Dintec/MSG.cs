using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Dintec
{
	public class MSG
	{
		public static bool SendMSG(string sender, string[] recipients, string Contents)
		{
			for (int i = 0; i < recipients.Length; i++)
				recipients[i] = "'" + recipients[i] + "'";

			// 쿼리
			string ids = string.Join(",", recipients);
			string query = @"
-- 발신자
SELECT
	USER_ID
FROM NeoBizboxS2.BX.TCMG_USER
WHERE LOGON_CD = '" + sender + @"'

-- 수신자
SELECT
	COID		= '1'	
,	USER_ID		= CONVERT(NVARCHAR, USER_ID)
,	USER_NM_KR
,	GRPID		= '2330'
FROM NeoBizboxS2.BX.TCMG_USER 
WHERE LOGON_CD IN (" + ids + ")";
			
			DataSet ds = SQL.GetDataSet(query);

			if (ds.Tables[1].Rows.Count == 0)
				return false;

			// 발신자
			string nUserID = ds.Tables[0].Rows[0][0].ToString();

			// 수신자
			string sCOIDs = ds.Tables[1].Concatenate(",", "COID");
			string sUserIDs = ds.Tables[1].Concatenate(",", "USER_ID");
			string sUserNMs = ds.Tables[1].Concatenate(",", "USER_NM_KR");
			string sGrpIDs = ds.Tables[1].Concatenate(",", "GRPID");

			// 실행
			SQL sql = new SQL("NeoBizboxS2.BX.PMS_MSG_C", SQLType.Procedure);
			sql.Parameter.Add2("@nGrpID"		, 2330);
			sql.Parameter.Add2("@nCOID"			, 1);
			sql.Parameter.Add2("@nUserID"		, nUserID);
			sql.Parameter.Add2("@sBiz_yn"		, "0");
			sql.Parameter.Add2("@sRev_yn"		, "0");
			sql.Parameter.Add2("@sContent"		, Contents);
			sql.Parameter.Add2("@sTarget_url"	, "", true);
			sql.Parameter.Add2("@sCOIDs"		, sCOIDs);
			sql.Parameter.Add2("@sUserIDs"		, sUserIDs);
			sql.Parameter.Add2("@sDeptIDs"		, "", true);
			sql.Parameter.Add2("@sUserNMs"		, sUserNMs);
			sql.Parameter.Add2("@sDeptNMs"		, "", true);
			sql.Parameter.Add2("@nEffect"		, 1073741824);
			sql.Parameter.Add2("@nHeight"		, 180);
			sql.Parameter.Add2("@nOffSet"		, 0);
			sql.Parameter.Add2("@nTextclr"		, 0);
			sql.Parameter.Add2("@sFaceNM"		, "맑은 고딕");
			sql.Parameter.Add2("@nAttachMID"	, 0);
			sql.Parameter.Add2("@nAttachDID"	, 0);
			sql.Parameter.Add2("@sFileNMs"		, "", true);
			sql.Parameter.Add2("@sFileSZs"		, "", true);
			sql.Parameter.Add2("@sMsgID"		, "", true);
			sql.Parameter.Add2("@sInsertINF"	, "Y");		// 이걸 해야 메신저에 쪽지 PUSH 가 됨. 이유는 모르겠음
			sql.Parameter.Add2("@sGrpIDs"		, sGrpIDs);
			sql.ExecuteNonQuery();

			return true;
		}
	}
}

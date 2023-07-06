using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DX
{
	public class 메신져
	{
		public static bool 메세지보내기(string 보내는사람, string[] 받는사람s, string 내용)
		{
			// 사용자 정보 가져오기
			string query = @"
SELECT	-- 발신자
	USER_ID
FROM NeoBizboxS2.BX.TCMG_USER
WHERE LOGON_CD = '" + 보내는사람 + @"'

SELECT	-- 수신자
	COID		= '1'	
,	USER_ID		= CONVERT(NVARCHAR, USER_ID)
,	USER_NM_KR
,	GRPID		= '2330'
FROM NeoBizboxS2.BX.TCMG_USER 
WHERE LOGON_CD IN (" + 받는사람s.결합_따옴표(",") + ")";

			DataSet ds = 디비.결과s(query);

			if (ds.Tables[1].Rows.Count == 0)
				return false;

			// 발신자
			string nUserID	= ds.Tables[0].Rows[0][0].문자();

			// 수신자
			string sCOIDs	= ds.Tables[1].결합(",", "COID");
			string sUserIDs	= ds.Tables[1].결합(",", "USER_ID");
			string sUserNMs	= ds.Tables[1].결합(",", "USER_NM_KR");
			string sGrpIDs	= ds.Tables[1].결합(",", "GRPID");

			// 실행
			디비 db = new 디비("NeoBizboxS2.BX.PMS_MSG_C");
			db.변수.추가("@nGrpID"		, 2330);
			db.변수.추가("@nCOID"		, 1);
			db.변수.추가("@nUserID"		, nUserID);
			db.변수.추가("@sBiz_yn"		, "0");
			db.변수.추가("@sRev_yn"		, "0");
			db.변수.추가("@sContent"		, 내용);
			db.변수.추가("@sTarget_url"	, "", true);
			db.변수.추가("@sCOIDs"		, sCOIDs);
			db.변수.추가("@sUserIDs"		, sUserIDs);
			db.변수.추가("@sDeptIDs"		, "", true);
			db.변수.추가("@sUserNMs"		, sUserNMs);
			db.변수.추가("@sDeptNMs"		, "", true);
			db.변수.추가("@nEffect"		, 1073741824);
			db.변수.추가("@nHeight"		, 180);
			db.변수.추가("@nOffSet"		, 0);
			db.변수.추가("@nTextclr"		, 0);
			db.변수.추가("@sFaceNM"		, "맑은 고딕");
			db.변수.추가("@nAttachMID"	, 0);
			db.변수.추가("@nAttachDID"	, 0);
			db.변수.추가("@sFileNMs"		, "", true);
			db.변수.추가("@sFileSZs"		, "", true);
			db.변수.추가("@sMsgID"		, "", true);
			db.변수.추가("@sInsertINF"	, "Y");  // 이걸 해야 메신저에 쪽지 PUSH 가 됨. 이유는 모르겠음
			db.변수.추가("@sGrpIDs"		, sGrpIDs);
			db.실행();

			return true;
		}

		public static void 재고예약쪽지(DataTable dt예약)
		{			
			// 사원별로 구분			
			DataTable dtEmp = dt예약.중복제거("NO_EMP");

			foreach (DataRow empRow in dtEmp.Rows)
			{
				string 사원번호 = empRow["NO_EMP"].문자();

				// 본문 생성
				string body;

				if (상수.언어 == "KR")
					body = @"** 재고 입고 알람
{0}

해당 수량 출고예약 완료되었습니다.


※ 본 쪽지는 발신 전용 쪽지입니다.";
				else
					body = @"** Notice for Received Stock
{0}

Please be sure to reserve as many as 'GI Booking' quantity.

※ You can't reply back to this message.";

				// 재고코드 라인 생성
				string item = "";

				foreach (DataRow itemRow in dt예약.선택("NO_EMP = '" + 사원번호 + "'"))
				{
					if (상수.언어 == "KR")
						item += string.Format(@"
-. {0} / {1} / 입고예약 : {2:#,##0} → 출고예약 : {3:#,##0}", itemRow["NO_FILE"], itemRow["CD_ITEM"], itemRow["QT_HOLD"], itemRow["QT_BOOK"]);
					else
						item += string.Format(@"
-. {0} / {1} / GR Booking : {2:#,##0} → GI Booking : {3:#,##0}", itemRow["NO_FILE"], itemRow["CD_ITEM"], itemRow["QT_HOLD"], itemRow["QT_BOOK"]);
				}

				string 내용 = string.Format(body, item);
				메세지보내기("ERP", new string[] { 사원번호 }, 내용);
			}
		}
	}
}

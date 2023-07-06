using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DX
{
	public class 그룹웨어
	{
		public static void 저장(string 문서번호, string 제목, string 본문, int 양식번호)
		{
			// 저장
			TSQL sql = new TSQL("PX_FI_GWDOCU");
			sql.변수.추가("@NO_DOCU"			, 문서번호);
			sql.변수.추가("@ID_WRITE"			, 상수.사원번호);
			sql.변수.추가("@NM_PUMM"			, 제목);
			sql.변수.추가("@NM_NOTE"			, 본문);
			sql.변수.추가("@APP_FORM_KIND"	, 양식번호);
			sql.실행();

			// 그룹웨어 팝업
			팝업(문서번호, 상수.사원번호);
		}

		public static void 팝업(string 문서번호, string 사원번호)
		{
			string url = "http://gw.dintec.co.kr/kor_webroot/src/cm/tims/index.aspx"
					+ "?cd_company=K100"
					+ "&cd_pc=010000"
					+ "&no_docu=" + 문서번호
					+ "&login_id=" + 사원번호;

			Process.Start("msedge.exe", url);
		}

		public static bool 상태체크(string 문서번호)
		{
			//DataTable dt = DBMgr.GetDataTable("SELECT ST_STAT FROM FI_GWDOCU WHERE NO_DOCU = '" + NO_DOCU + "'");
			//string ST_STAT = (dt.Rows.Count == 1) ? dt.Rows[0]["ST_STAT"].ToString() : "";

			//if (ST_STAT == "0") return Util.ShowMessage("결재 진행중인 문서입니다.");
			//if (ST_STAT == "1") return Util.ShowMessage("결재 완료된 문서입니다.");

			return true;
		}

		
	}
}

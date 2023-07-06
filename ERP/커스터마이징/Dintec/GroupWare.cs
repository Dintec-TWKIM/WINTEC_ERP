using Duzon.Common.Forms;
using Duzon.ERPU;
using System.Data;
using System.Diagnostics;

namespace Dintec
{
	public class GroupWare
	{
		public static string Save(string title, string body, object no_docu, int form_kind, string updateQuery, bool boPop)
		{
			// 그룹웨어 세팅 정보
			string CD_COMPANY = GetERP_CD_COMPANY();
			string CD_PC = GetERP_CD_PC();

			// XML 테이블 생성
			DataTable dtXml = new DataTable();
			dtXml.Columns.Add("CD_COMPANY_REAL");
			dtXml.Columns.Add("CD_COMPANY");
			dtXml.Columns.Add("CD_PC");
			dtXml.Columns.Add("NO_DOCU");
			dtXml.Columns.Add("ID_WRITE");
			dtXml.Columns.Add("DT_ACCT");
			dtXml.Columns.Add("NM_PUMM");
			dtXml.Columns.Add("NM_NOTE");
			dtXml.Columns.Add("APP_FORM_KIND");
			dtXml.Rows.Add();

			dtXml.Rows[0]["CD_COMPANY_REAL"] = Global.MainFrame.LoginInfo.CompanyCode;  // 진짜 회사코드
			dtXml.Rows[0]["CD_COMPANY"] = CD_COMPANY;                                   // 그룹웨어에 세팅된 회사코드
			dtXml.Rows[0]["CD_PC"] = CD_PC;                                             // 그룹웨어에 세팅된 회계단위
			dtXml.Rows[0]["NO_DOCU"] = no_docu;                                         // 전표번호
			dtXml.Rows[0]["ID_WRITE"] = Global.MainFrame.LoginInfo.UserID;              // 작성자
			dtXml.Rows[0]["DT_ACCT"] = Util.GetToday();                                 // 회계일자
			dtXml.Rows[0]["NM_PUMM"] = title;                                           // 품의내역
			dtXml.Rows[0]["NM_NOTE"] = body;                                            // 내용(HTML)
			dtXml.Rows[0]["APP_FORM_KIND"] = form_kind;                                 // 기안종류

			// 저장
			string xml = Util.GetTO_Xml(dtXml);
			DataTable dtKey = DBHelper.GetDataTable("PX_CZ_FI_GWDOCU", new object[] { xml, updateQuery });

			// 그룹웨어 팝업
			if (boPop)
			{
				//string url = "http://gw.dintec.co.kr" + "/kor_webroot/src/cm/tims/index.aspx"
				//        + "?cd_company=" + Global.MainFrame.LoginInfo.CompanyCode
				//        + "&cd_pc=" + Global.MainFrame.LoginInfo.CdPc
				//        + "&no_docu=" + dtKey.Rows[0][0]

				//        + "&login_id=" + Global.MainFrame.LoginInfo.UserID;
				string url = "http://gw.dintec.co.kr/kor_webroot/src/cm/tims/index.aspx"
						+ "?cd_company=" + CD_COMPANY
						+ "&cd_pc=" + CD_PC
						+ "&no_docu=" + dtKey.Rows[0][0]
						+ "&login_id=" + Global.MainFrame.LoginInfo.UserID;

				Process.Start("msedge.exe", url);
			}

			return dtKey.Rows[0][0].ToString();
		}

		public static void Popup(object NO_DOCU)
		{
			string url = "http://gw.dintec.co.kr/kor_webroot/src/cm/tims/index.aspx"
						+ "?cd_company=" + GetERP_CD_COMPANY()
						+ "&cd_pc=010000"
						+ "&no_docu=" + NO_DOCU
						+ "&login_id=" + Global.MainFrame.LoginInfo.UserID;

			Process.Start("msedge.exe", url);
		}

		public static bool CheckSTAT(string NO_DOCU)
		{
			DataTable dt = DBMgr.GetDataTable("SELECT ST_STAT FROM FI_GWDOCU WHERE NO_DOCU = '" + NO_DOCU + "'");
			string ST_STAT = (dt.Rows.Count == 1) ? dt.Rows[0]["ST_STAT"].ToString() : "";

			if (ST_STAT == "0") return Util.ShowMessage("결재 진행중인 문서입니다.");
			if (ST_STAT == "1") return Util.ShowMessage("결재 완료된 문서입니다.");

			return true;
		}


		public static string GetERP_CD_COMPANY()
		{
			DBMgr dbm = new DBMgr(DBConn.GroupWare);
			dbm.Query = "SELECT * FROM BX.TCMG_CO";
			DataTable dt = dbm.GetDataTable();

			return dt.Rows[0]["erp_co_cd"].ToString();
		}

		public static string GetERP_CD_PC()
		{
			return "010000";
		}
	}
}

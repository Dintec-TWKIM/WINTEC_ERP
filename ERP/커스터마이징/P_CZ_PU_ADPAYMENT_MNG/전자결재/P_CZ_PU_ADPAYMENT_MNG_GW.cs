using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Web;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Dintec;

namespace cz
{
	internal class P_CZ_PU_ADPAYMENT_MNG_GW
	{
        string 결재라인Master = string.Empty,
               결재라인Detail = string.Empty,
               결재라인Activity = string.Empty,
               결재라인사용자 = string.Empty,
               결재라인회사 = string.Empty,
               결재라인부서 = string.Empty,
               결재라인직급 = string.Empty,
               결재라인직책 = string.Empty,
               결재라인상태 = string.Empty,
               결재라인구분 = string.Empty,
               결재여부 = string.Empty,
               전결가능여부 = string.Empty,
               부서결재여부 = string.Empty,
               
               수신참조_유형 = string.Empty,
               수신참조_구분 = string.Empty,
               수신참조_아이디 = string.Empty,
               수신참조_회사 = string.Empty,
               수신참조_부서 = string.Empty,
               수신참조_순서 = string.Empty;

        internal bool 전자결재(DataRow header)
		{
			bool isSuccess;
			string strURL, key;
            int 양식번호;

            key = (MA.Login.회사코드 + "-" + D.GetString(header["NO_ADPAY"]));

            if (Global.MainFrame.LoginInfo.Language == "US")
                양식번호 = 1006;
            else
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "K100")
                    양식번호 = 1008;
                else
                    양식번호 = 1004;
            }

            isSuccess = 결재상신(new object[] { GroupWare.GetERP_CD_COMPANY(),
                                                GroupWare.GetERP_CD_PC(),
                                                key,
                                                MA.Login.사원번호,
                                                Global.MainFrame.GetStringToday,
                                                this.GetHtml(header, true),
                                                (Global.MainFrame.DD("선지급품의서") + "_" + D.GetString(header["NM_SUPPLIER"]) + "_" + D.GetString(header["NO_PO"])),
                                                "Y",
                                                양식번호 });

			if (!isSuccess) return false;

            strURL = "http://gw.dintec.co.kr" + "/kor_webroot/src/cm/tims/index.aspx"
                     + "?cd_company=" + GroupWare.GetERP_CD_COMPANY()
                     + "&cd_pc=" + GroupWare.GetERP_CD_PC()
                     + "&no_docu=" + HttpUtility.UrlEncode(key, Encoding.UTF8)
					 + "&login_id=" + MA.Login.사원번호;

			Process.Start("msedge.exe", strURL);

			return isSuccess;
		}

        internal bool 전자결재_자동(DataRow header)
        {
            bool isSuccess;
            string strURL, key, 제목;
            int 양식번호;

            key = (MA.Login.회사코드 + "-" + D.GetString(header["NO_ADPAY"]));

            if (Global.MainFrame.LoginInfo.Language == "US")
                양식번호 = 1006;
            else
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "K100")
                    양식번호 = 1008;
                else
                    양식번호 = 1004;
            }

            제목 = (Global.MainFrame.DD("선지급품의서") + "_" + D.GetString(header["NM_SUPPLIER"]) + "_" + D.GetString(header["NO_PO"]));

            isSuccess = 결재상신(new object[] { GroupWare.GetERP_CD_COMPANY(),
                                                GroupWare.GetERP_CD_PC(),
                                                key,
                                                MA.Login.사원번호,
                                                Global.MainFrame.GetStringToday,
                                                this.GetHtml(header, false),
                                                제목,
                                                "Y",
                                                양식번호 });

            if (!isSuccess) return false;

			this.자동상신(D.GetDecimal(header["AM_PRE"]), key, 제목, MA.Login.사원번호);

			//strURL = "http://gw.dintec.co.kr" + "/kor_webroot/src/cm/tims/index.aspx"
			//		 + "?cd_company=" + GroupWare.GetERP_CD_COMPANY()
			//		 + "&cd_pc=" + GroupWare.GetERP_CD_PC()
			//		 + "&no_docu=" + HttpUtility.UrlEncode(key, Encoding.UTF8)
			//		 + "&login_id=" + MA.Login.사원번호;

			//Process.Start("msedge.exe", strURL);

			return isSuccess;
        }

        internal void 미리보기(DataRow header)
        {
            string html;

            html = this.GetHtml(header, true);
            P_CZ_MA_HTML_VIEWER dialog = new P_CZ_MA_HTML_VIEWER(Global.MainFrame.DD("선지급품의서"), html);

            dialog.ShowDialog();
        }

        private string GetHtml(DataRow header, bool isShowMsg)
		{
			string path, body, query;
            decimal 외화금액;
            bool 외화정보표시여부;
            DataTable dt;

            body = string.Empty;

            if (Global.MainFrame.LoginInfo.Language == "US")
                path = Application.StartupPath + "\\download\\gw\\HT_P_CZ_PU_ADPAYMENT_MNG_BODY_ENG.htm";
            else
                path = Application.StartupPath + "\\download\\gw\\HT_P_CZ_PU_ADPAYMENT_MNG_BODY.htm";

            //body = File.ReadAllText(path, System.Text.UTF8Encoding.UTF8);
            using (StreamReader reader = new StreamReader(path, Encoding.Default))
            {
                body = reader.ReadToEnd();
            }

            query = @"SELECT DATEDIFF(MONTH, MAX(IH.DT_PROCESS), GETDATE()) AS QT_MONTH
FROM PU_IVH IH WITH(NOLOCK)
WHERE IH.CD_COMPANY = '{0}'
AND IH.CD_PARTNER = '{1}'";

            dt = DBHelper.GetDataTable(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode, header["CD_PARTNER"].ToString() }));

            if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
			{
                if (dt == null || dt.Rows.Count == 0)
                {
                    if (isShowMsg) Global.MainFrame.ShowMessage("No purchase history.\nMake sure the supplier info and bank account number are correct.\nThen please indicate any differences and changes.");
                    body = body.Replace("@@DC_WARNING", "<div style='font-weight:bold; color:#ff0000; margin-top:30px'>No purchase history.<br>Make sure the supplier info and bank account number are correct.<br>Then please indicate any differences and changes.</div><br><br>");
                }
                else if (D.GetInt(dt.Rows[0]["QT_MONTH"]) >= 6)
                {
                    if (isShowMsg) Global.MainFrame.ShowMessage("No purchase history in the last 6 months.\nMake sure the supplier info and bank account number are correct.\nThen please indicate any differences and changes.");
                    body = body.Replace("@@DC_WARNING", "<div style='font-weight:bold; color:#ff0000; margin-top:30px'>No purchase history in the last 6 months.<br>Make sure the supplier info and bank account number are correct.<br>Then please indicate any differences and changes.</div><br><br>");
                }
                else
                    body = body.Replace("@@DC_WARNING", string.Empty);
            }
            else
			{
                if (dt == null || dt.Rows.Count == 0)
                {
                    if (isShowMsg) Global.MainFrame.ShowMessage("매입 이력이 없습니다.\n사업자등록번호 및 계좌정보 재확인 및 결과 리마크하여 주세요");
                    body = body.Replace("@@DC_WARNING", "<div style='font-weight:bold; color:#ff0000; margin-top:30px'>매입 이력이 없습니다.<br>사업자등록번호 및 계좌정보 재확인 및 결과 리마크하여 주세요</div><br><br>");
                }
                else if (D.GetInt(dt.Rows[0]["QT_MONTH"]) >= 6)
                {
                    if (isShowMsg) Global.MainFrame.ShowMessage("최근 6개월 매입 이력이 없습니다.\n사업자등록번호 및 계좌정보 재확인 및 결과 리마크하여 주세요");
                    body = body.Replace("@@DC_WARNING", "<div style='font-weight:bold; color:#ff0000; margin-top:30px'>최근 6개월 매입 이력이 없습니다.<br>사업자등록번호 및 계좌정보 재확인 및 결과 리마크하여 주세요</div><br><br>");
                }
                else
                    body = body.Replace("@@DC_WARNING", string.Empty);
            }

            외화금액 = D.GetDecimal(header["AM_EX_PRE"]);

            if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                body = body.Replace("@@NM_EXCH_STD", "USD");
            else
                body = body.Replace("@@NM_EXCH_STD", "KRW");
            
            body = body.Replace("@@NM_SUPPLIER", D.GetString(header["NM_SUPPLIER"]));
            body = body.Replace("@@NM_TPPO", D.GetString(header["NM_TPPO"]));

            body = body.Replace("@@AM_EX_PRE_HAN", D.GetHangleAmt(외화금액));
            body = body.Replace("@@NM_EXCH", D.GetString(header["NM_EXCH"]));
            body = body.Replace("@@AM_EX_PRE", 외화금액.ToString("N"));

            body = body.Replace("@@NO_ADPAY", D.GetString(header["NO_ADPAY"]));
            body = body.Replace("@@DT_ADPAY", Util.GetTo_DateStringS(header["DT_ADPAY"]));
            body = body.Replace("@@AM_EX_REMAIN", D.GetDecimal(header["AM_REMAIN"]).ToString("N"));
            body = body.Replace("@@NO_DOCU", D.GetString(header["NO_DOCU"]));

            body = body.Replace("@@DC_REMARK", D.GetString(header["TXT_USERDEF1"]));

            dt = this.수주정보(D.GetString(header["CD_PJT"]));

            if (dt != null && dt.Rows.Count > 0 && 
                (D.GetString(dt.Rows[0]["NM_EXCH_SO"]) == D.GetString(dt.Rows[0]["NM_EXCH_PO"])) &&
                D.GetDecimal(dt.Rows[0]["QT_EXCH"]) == 1 &&
                D.GetDecimal(dt.Rows[0]["RT_EXCH"]) != 1 &&
                D.GetDecimal(dt.Rows[0]["AM_STOCK"]) == 0)
                외화정보표시여부 = true;
            else
                외화정보표시여부 = false;

            if (dt != null && dt.Rows.Count > 0)
            {
                body = body.Replace("@@NO_SO", D.GetString(dt.Rows[0]["NO_SO"]));
                body = body.Replace("@@DT_SO", Util.GetTo_DateStringS(dt.Rows[0]["DT_SO"]));
                body = body.Replace("@@NM_PARTNER", D.GetString(dt.Rows[0]["NM_PARTNER"]));
                body = body.Replace("@@NM_VESSEL", D.GetString(dt.Rows[0]["NM_VESSEL"]));
                body = body.Replace("@@AM_SO", D.GetDecimal(dt.Rows[0]["AM_SO"]).ToString("N"));
                body = body.Replace("@@AM_PO", D.GetDecimal(dt.Rows[0]["AM_PO"]).ToString("N"));
                body = body.Replace("@@AM_PROFIT", D.GetDecimal(dt.Rows[0]["AM_PROFIT"]).ToString("N"));
                body = body.Replace("@@RT_PROFIT", D.GetDecimal(dt.Rows[0]["RT_PROFIT"]).ToString("N"));

                if (외화정보표시여부)
                    body = body.Replace("@@TXT_SO_EX_INFO", this.외화수주정보(dt.Rows[0]));
                else
                    body = body.Replace("@@TXT_SO_EX_INFO", string.Empty);
            }
            else
            {
                body = body.Replace("@@NO_SO", string.Empty);
                body = body.Replace("@@DT_SO", string.Empty);
                body = body.Replace("@@NM_PARTNER", string.Empty);
                body = body.Replace("@@NM_VESSEL", string.Empty);
                body = body.Replace("@@AM_SO", string.Empty);
                body = body.Replace("@@AM_PO", string.Empty);
                body = body.Replace("@@AM_PROFIT", string.Empty);
                body = body.Replace("@@RT_PROFIT", string.Empty);

                body = body.Replace("@@TXT_SO_EX_INFO", string.Empty);
            }

            dt = this.발주정보(D.GetString(header["NO_PO"]));

            body = body.Replace("@@NO_PO", D.GetString(header["NO_PO"]));
            body = body.Replace("@@DT_PO", Util.GetTo_DateStringS(header["DT_PO"]));

            if (dt != null && dt.Rows.Count > 0)
            {
                body = body.Replace("@@AM_PU_SO", D.GetDecimal(dt.Rows[0]["AM_PU_SO"]).ToString("N"));
                body = body.Replace("@@AM_PU_PO", D.GetDecimal(dt.Rows[0]["AM_PU_PO"]).ToString("N"));
                body = body.Replace("@@AM_PU_PROFIT", D.GetDecimal(dt.Rows[0]["AM_PU_PROFIT"]).ToString("N"));
                body = body.Replace("@@RT_PU_PROFIT", D.GetDecimal(dt.Rows[0]["RT_PU_PROFIT"]).ToString("N"));

                if (외화정보표시여부)
                    body = body.Replace("@@TXT_PO_EX_INFO", this.외화발주정보(dt.Rows[0]));
                else
                    body = body.Replace("@@TXT_PO_EX_INFO", string.Empty);
            }
            else
            {
                body = body.Replace("@@AM_PU_SO", string.Empty);
                body = body.Replace("@@AM_PU_PO", string.Empty);
                body = body.Replace("@@AM_PU_PROFIT", string.Empty);
                body = body.Replace("@@RT_PU_PROFIT", string.Empty);

                body = body.Replace("@@TXT_PO_EX_INFO", string.Empty);
            }

            body = body.Replace("@@AM_ADPAY_PO", D.GetDecimal(header["AM_PO"]).ToString("N"));
            body = body.Replace("@@VAT", D.GetDecimal(header["VAT"]).ToString("N"));
            body = body.Replace("@@AM_ADPAY", D.GetDecimal(header["AM_ADPAY"]).ToString("N"));
            body = body.Replace("@@RT_ADPAY", Decimal.Round((D.GetDecimal(header["AM_ADPAY"]) / (D.GetDecimal(header["AM_PO"]) + D.GetDecimal(header["VAT"]))) * 100, 2, MidpointRounding.AwayFromZero).ToString("N"));

            body = body.Replace("@@TXT_DOCU", this.전표정보(D.GetString(header["NO_DOCU"])));

            dt = this.매입처계좌정보(D.GetString(header["CD_PARTNER"]));

            if (dt != null && dt.Rows.Count > 0)
            {
                body = body.Replace("@@NM_BANK_PARTNER", D.GetString(dt.Rows[0]["NM_BANK"]));
                body = body.Replace("@@NO_DEPOSIT", D.GetString(dt.Rows[0]["NO_DEPOSIT"]));
                body = body.Replace("@@NM_DEPOSIT", D.GetString(dt.Rows[0]["NM_DEPOSIT"]));
            }
            else
            {
                body = body.Replace("@@NM_BANK_PARTNER", string.Empty);
                body = body.Replace("@@NO_DEPOSIT", string.Empty);
                body = body.Replace("@@NM_DEPOSIT", string.Empty);
            }

            body = body.Replace("@@ATTACH_FILE", this.첨부파일(header["NO_PO"].ToString()));

            body = body.Replace("@@DT_END", Util.GetTo_DateStringS(header["DT_ACCT"]));

            return body;
		}

		private bool 결재상신(object[] obj)
		{
			return DBHelper.ExecuteNonQuery("SP_CZ_FI_GWDOCU", obj);
		}

		private bool 자동상신(decimal 선지급금액, string key, string 제목, string 상신자)
		{
			string query, 결재보관함;
			DataTable 상신자정보, 결재자정보, 결재정보;

			//BX.TCMG_MENU
			if (Global.MainFrame.LoginInfo.CompanyCode == "K100")
				결재보관함 = "2000491"; //선지급품의서
			else if (Global.MainFrame.LoginInfo.CompanyCode == "K200")
				결재보관함 = "2000492"; //관리부_결재문서
			else if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
				결재보관함 = "2000472"; //DINTEC S'pore 결재보관함
			else
				return false;

			DBMgr gw = new DBMgr(DBConn.GroupWare);

			query = @"SELECT T1.user_id,
							 T1.user_nm_kr,
							 T2.dept_id,
							 T2.grade_cd,
							 (SELECT TOP 1 NM_KR FROM BX.TCMG_CDD WHERE CM_CD = 'CM0002' AND CD = T2.GRADE_CD AND (CO_ID = 0 OR CO_ID = T2.CO_ID) ORDER BY CO_ID DESC) GRADE_NM,
							 T2.duty_cd,
							 (SELECT TOP 1 NM_KR FROM BX.TCMG_CDD WHERE CM_CD = 'CM0003' AND CD = T2.DUTY_CD AND (CO_ID = 0 OR CO_ID = T2.CO_ID) ORDER BY CO_ID DESC) DUTY_NM,
							 T2.tel
				      FROM BX.TCMG_USER T1 WITH (NOLOCK)      
				      JOIN BX.TCMG_USERDEPT T2 WITH (NOLOCK) ON T1. USER_ID = T2.USER_ID   
				      WHERE T1.logon_cd = '{0}'";

			gw.Query = string.Format(query, 상신자);
			상신자정보 = gw.GetDataTable();

			if (상신자정보 == null || 상신자정보.Rows.Count != 1)
				return false;

            결재라인Master = string.Empty;
            결재라인Detail = string.Empty;
            결재라인Activity = string.Empty;
            결재라인사용자 = string.Empty;
            결재라인회사 = string.Empty;
            결재라인부서 = string.Empty;
            결재라인직급 = string.Empty;
            결재라인직책 = string.Empty;
            결재라인상태 = string.Empty;
            결재라인구분 = string.Empty;
            결재여부 = string.Empty;
            전결가능여부 = string.Empty;
            부서결재여부 = string.Empty;

            수신참조_유형 = string.Empty;
            수신참조_구분 = string.Empty;
            수신참조_아이디 = string.Empty;
            수신참조_회사 = string.Empty;
            수신참조_부서 = string.Empty;
            수신참조_순서 = string.Empty;

            query = @"SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

;WITH A AS
(
    SELECT ME.CD_COMPANY,
           ME.NO_EMP,
           ME.NM_KOR,
           ME.CD_CC,
           ME.CD_DEPT,
           ME.CD_DUTY_RESP 
    FROM MA_EMP ME
    WHERE ME.CD_COMPANY = '{0}'
    AND ME.CD_INCOM <> '099'
    AND ISNULL(ME.DT_RETIRE, '00000000') = '00000000'
    AND ME.TP_EMP IN ('100', '200')
),
B AS
(
    SELECT * 
    FROM A
    WHERE A.CD_DUTY_RESP = '400'
),
C AS
(
    SELECT * 
    FROM A
    WHERE (A.CD_DUTY_RESP = '200' OR A.NO_EMP = 'S-343')
    AND A.NO_EMP <> 'S-250'
)
SELECT A.NO_EMP,
       A.NM_KOR,
       B.NO_EMP AS NO_EMP1,
       B.NM_KOR AS NM_KOR1,
       C.NO_EMP AS NO_EMP2,
       C.NM_KOR AS NM_KOR2
FROM A
LEFT JOIN B ON B.CD_COMPANY = A.CD_COMPANY AND B.CD_CC = A.CD_CC
LEFT JOIN C ON C.CD_COMPANY = A.CD_COMPANY AND C.CD_DEPT = A.CD_DEPT
WHERE A.NO_EMP = '{1}'";

            결재자정보 = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 상신자));

            #region 결재라인
            if (Global.MainFrame.LoginInfo.CompanyCode == "K100")
			{
                #region 딘텍
				int index = 1;

				if (!string.IsNullOrEmpty(결재자정보.Rows[0]["NO_EMP1"].ToString()) &&
					(결재자정보.Rows[0]["NO_EMP1"].ToString() != 상신자 || string.IsNullOrEmpty(결재자정보.Rows[0]["NO_EMP2"].ToString())))
					this.결재라인생성(index++.ToString(), "3000", 결재자정보.Rows[0]["NO_EMP1"].ToString(), "20"); //결재, 팀장

                if (index == 1)
				    this.결재라인생성(index++.ToString(), "4000", "S-265", "20"); //협의, 김예돈 이사님
                else
                    this.결재라인생성(index++.ToString(), "4000", "S-265", "10"); //협의, 김예돈 이사님

                this.결재라인생성(index++.ToString(), "4000", "S-046", "10"); //협의, 손영봉 상무님

				if (선지급금액 > 1000000 &&
					!string.IsNullOrEmpty(결재자정보.Rows[0]["NO_EMP2"].ToString()) &&
					결재자정보.Rows[0]["NO_EMP1"].ToString() != 결재자정보.Rows[0]["NO_EMP2"].ToString())
					this.결재라인생성(index++.ToString(), "3000", 결재자정보.Rows[0]["NO_EMP2"].ToString(), "10"); //결재, 부서장

				this.결재라인생성(index++.ToString(), "4000", "S-373", "10"); //협의, 최정환 전무님

				if (선지급금액 > 3000000)
					this.결재라인생성(index++.ToString(), "3000", "S-347", "10"); //결재, 사장

				if (선지급금액 > 10000000)
					this.결재라인생성(index++.ToString(), "3000", "S-001", "10"); //결재, 회장님

				#region 수신참조
				this.수신참조생성("D", "41", "1"); //경리팀
				this.수신참조생성("U", "S-568", "2"); //정민주
				this.수신참조생성("U", "S-484", "3"); //최성주
				this.수신참조생성("U", "S-145", "4"); //조현민
				#endregion

				#endregion
			}
			else if (Global.MainFrame.LoginInfo.CompanyCode == "K200")
			{
                #region 두베코

                this.결재라인생성("1", "3000", 결재자정보.Rows[0]["NO_EMP1"].ToString(), "20"); //결재, 팀장

                if (선지급금액 > 1000000)
                    this.결재라인생성("2", "3000", 결재자정보.Rows[0]["NO_EMP2"].ToString(), "10"); //결재, 부서장

                #region 수신참조
                this.수신참조생성("D", "78", "1"); //관리부
                #endregion

                #endregion
            }
            else if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
			{
                #region 딘텍싱가폴

                this.결재라인생성("1", "4000", "G-030", "20"); //협의, 장백수
                this.결재라인생성("2", "3000", "G-015", "10"); //결재, 김동진

                if (선지급금액 > 1000)
				{
                    this.결재라인생성("3", "4000", "S-046", "10"); //협의, 손영봉 상무님
                    this.결재라인생성("4", "4000", "S-373", "10"); //협의, 최정환 전무님
                    this.결재라인생성("5", "3000", "S-347", "10"); //결재, 사장
                }

                #region 수신참조
                this.수신참조생성("D", "41", "1"); //경리팀
                this.수신참조생성("U", "S-568", "2"); //정민주
                this.수신참조생성("D", "59", "3"); //DINTEC S`pore
                #endregion

                #endregion
            }
            #endregion

            #region 결재상신
            query = @"exec bx.PEA_SETAPPDOCSAVE @nGrpID=2330,
											    @nCoID=1,
											    @nDeptID='" + 상신자정보.Rows[0]["dept_id"].ToString() + @"',
											    @nUserID='" + 상신자정보.Rows[0]["user_id"].ToString() + @"',
											    
											    @nDocID=0,
											    @nFormID=83,
											    @sNumbering_id=N'1001',
											    @sRep_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + @"',
											    @nAppCoID=1,
											    @nAppDeptID='" + 상신자정보.Rows[0]["dept_id"].ToString() + @"',
											    @nAppUserID='" + 상신자정보.Rows[0]["user_id"].ToString() + @"',
											    @sDocTitle=N'" + 제목 + @"',
											    @sDocSts=N'20',
											    @sArbitraryYN=NULL,
											    @sInserviceTime=N'0',
											    @nRelatedDocNo=NULL,
											    @sTransferYN=NULL,
											    @nTransferMenuID=NULL,
											    @sDocLevel=N'1',
											    @sUseYN=N'1',
											    
											    @sInterlockUrl=NULL,
											    @sInterlockBtnNm=NULL,
											    @nRcvCoID=NULL,
											    @nRcvDeptID=NULL,
											    @nRcvUserID=NULL,
											    @sRcvUserNm=NULL,
											    @sApproKey='K100▥010000▥" + key + @"',
											    @sPhone=NULL,
											    @sEmail=N'',
											    @sHomepage=NULL,
											    @sAddress=N'  ',
											    @sFax=NULL,
											    @sD_phone=N'" + 상신자정보.Rows[0]["tel"].ToString() + @"',
											    @sSealUseYN=N'0',
											    @sRepDtModYN=N'0',
											    @sEmergencyLevel=N'1',
											    
											    @sContentsTP=N'10',
											    @sDocContents=N'<HTML><HEAD><TITLE>HTML Document</TITLE></HEAD></HTML>',

												@sDocLineMSeqs = N'" + 결재라인Master + @"',
											    @sDocLineDSeqs = N'" + 결재라인Detail + @"',
											    @sLineActIDs = N'" + 결재라인Activity + @"',
											    @sLineUserIDs = '" + 결재라인사용자 + @"',
											    @sLineCoIDs = N'" + 결재라인회사 + @"',
											    @sLineDeptIDs = '" + 결재라인부서 + @"',
											    @sLineGradeCDs = '" + 결재라인직급 + @"',
												@sLineDutyCDs = '" + 결재라인직책 + @"',
											    @sDocLineStss = N'" + 결재라인상태 + @"',
											    @sDocLineGBs = N'" + 결재라인구분 + @"',
											    @sLineAppYNs = N'" + 결재여부 + @"',
											    @sLineArbitaryYNs = N'" + 전결가능여부 + @"',
											    
											    @sItemIDs = N'',
											    @sItemTPs = N'',
											    @sItemValues = N'',
											    
											    @sInDispFileNMs = N'',
											    @sInFileNMs = N'',
											    @sInFileSZs = N'',
											    @sInFileDiv = N'',
											    @sInFilePath = N'',
											    @sInEtcSeq = N'',
											    @sInVerID = N'',
											    
											    @sReceiveDIVs = N'" + 수신참조_유형 + @"',
											    @sReceiveOrgDIVs = N'" + 수신참조_구분 + @"',
											    @sReceiveOrgIDs = N'" + 수신참조_아이디 + @"',
											    @sReceiveCoIDs = N'" + 수신참조_회사 + @"',
											    @sReceiveDeptIDs = N'" + 수신참조_부서 + @"',
											    @sReceiveAppLineDSeqs = N'" + 수신참조_순서 + @"',
											    
											    @sRefDocNOs = N'',
											    @sRefDocSeqs = N'',
											    @sMigDocYNs = N'',
											    
											    @sMenuIDs = '" + 결재보관함 + @"',
											    @sFunction1000 = N'1000',
											    @sModifyReason = N'',
											    @sDeptLineYNs = N'" + 부서결재여부 + @"',
											    @sReceipient = NULL,
												@sReceipt = NULL";

			gw.Query = query;
			결재정보 = gw.GetDataTable();

			if (결재정보.Rows[0]["RESULTVALUE"].ToString() == "0")
			{
				Global.MainFrame.ShowMessage(결재정보.Rows[0]["RETURN_MESSAGE"].ToString());
				return false;
			}

			query = @"SELECT DOC_ID, DOC_NO 
					  FROM BX.TEAG_APPDOC
					  WHERE doc_id = '" + 결재정보.Rows[0]["RESULTVALUE"] + "'";

			gw.Query = query;
			결재정보 = gw.GetDataTable();
			#endregion

			#region ERP 정보 반영
			DBMgr erp = new DBMgr(DBConn.iU);

			query = @"UPDATE FI_GWDOCU 
					  SET ST_STAT = '0', 
						  APP_NO_EMP = '" + 상신자 + @"', 
						  APP_DT = '" + DateTime.Now.ToString("yyyyMMdd") + @"',
						  APP_DOC_ID = '" + 결재정보.Rows[0]["DOC_ID"].ToString() + @"',
						  DOC_NO = '" + 결재정보.Rows[0]["DOC_NO"].ToString() + @"'
					  WHERE CD_COMPANY = '" + GroupWare.GetERP_CD_COMPANY() + "'" + Environment.NewLine +
					 "AND CD_PC = '" + GroupWare.GetERP_CD_PC() + "'" + Environment.NewLine +
					 "AND NO_DOCU = '" + key + "'";

			erp.Query = query;
			erp.ExecuteNonQuery();
			#endregion

			return true;
		}

        private void 결재라인생성(string 결재순서, string 결재구분, string 사원번호, string 결재상태)
		{
            DBMgr gw = new DBMgr(DBConn.GroupWare);

            string query = @"SELECT T1.user_id,
					         	    T1.user_nm_kr,
					         	    T2.dept_id,
					         	    T2.grade_cd,
					         	    (SELECT TOP 1 NM_KR FROM BX.TCMG_CDD WHERE CM_CD = 'CM0002' AND CD = T2.GRADE_CD AND (CO_ID = 0 OR CO_ID = T2.CO_ID) ORDER BY CO_ID DESC) GRADE_NM,
					         	    T2.duty_cd,
					         	    (SELECT TOP 1 NM_KR FROM BX.TCMG_CDD WHERE CM_CD = 'CM0003' AND CD = T2.DUTY_CD AND (CO_ID = 0 OR CO_ID = T2.CO_ID) ORDER BY CO_ID DESC) DUTY_NM,
					         	    T2.tel
				             FROM BX.TCMG_USER T1 WITH (NOLOCK)      
				             JOIN BX.TCMG_USERDEPT T2 WITH (NOLOCK) ON T1. USER_ID = T2.USER_ID   
				             WHERE T1.logon_cd = '{0}'";

            gw.Query = string.Format(query, 사원번호);
            DataTable 결재자정보 = gw.GetDataTable();

            if (결재자정보 != null && 결재자정보.Rows.Count > 0)
			{
                결재라인Master += (string.IsNullOrEmpty(결재라인Master) ? string.Empty : ",") + 결재순서;
                결재라인Detail += (string.IsNullOrEmpty(결재라인Detail) ? string.Empty : ",") + "1";
                결재라인Activity += (string.IsNullOrEmpty(결재라인Activity) ? string.Empty : ",") + 결재구분;
                결재라인사용자 += (string.IsNullOrEmpty(결재라인사용자) ? string.Empty : ",") + 결재자정보.Rows[0]["user_id"].ToString();
                결재라인회사 += (string.IsNullOrEmpty(결재라인회사) ? string.Empty : ",") + "1";
                결재라인부서 += (string.IsNullOrEmpty(결재라인부서) ? string.Empty : ",") + 결재자정보.Rows[0]["dept_id"].ToString();
                결재라인직급 += (string.IsNullOrEmpty(결재라인직급) ? string.Empty : ",") + 결재자정보.Rows[0]["grade_cd"].ToString();
                결재라인직책 += (string.IsNullOrEmpty(결재라인직책) ? string.Empty : ",") + 결재자정보.Rows[0]["duty_cd"].ToString();
                결재라인상태 += (string.IsNullOrEmpty(결재라인상태) ? string.Empty : ",") + 결재상태;
                결재라인구분 += (string.IsNullOrEmpty(결재라인구분) ? string.Empty : ",") + "1";
                결재여부 += (string.IsNullOrEmpty(결재여부) ? string.Empty : ",") + "0";
                전결가능여부 += (string.IsNullOrEmpty(전결가능여부) ? string.Empty : ",") + "0";
                부서결재여부 += (string.IsNullOrEmpty(부서결재여부) ? string.Empty : ",") + "0";
            }
        }

        private void 수신참조생성(string 대상구분, string 아이디, string 순서)
		{
            if (대상구분 == "D")
			{
                수신참조_유형 += (string.IsNullOrEmpty(수신참조_유형) ? string.Empty : ",") + "10";
                수신참조_구분 += (string.IsNullOrEmpty(수신참조_구분) ? string.Empty : ",") + 대상구분;
                수신참조_아이디 += (string.IsNullOrEmpty(수신참조_아이디) ? string.Empty : ",") + 아이디;
                수신참조_회사 += (string.IsNullOrEmpty(수신참조_회사) ? string.Empty : ",") + "1";
                수신참조_부서 += (string.IsNullOrEmpty(수신참조_부서) ? string.Empty : ",") + 아이디;
                수신참조_순서 += (string.IsNullOrEmpty(수신참조_순서) ? string.Empty : ",") + 순서;
            }
            else
			{
                DBMgr gw = new DBMgr(DBConn.GroupWare);

                string query = @"SELECT T1.user_id,
					         	    T1.user_nm_kr,
					         	    T2.dept_id,
					         	    T2.grade_cd,
					         	    (SELECT TOP 1 NM_KR FROM BX.TCMG_CDD WHERE CM_CD = 'CM0002' AND CD = T2.GRADE_CD AND (CO_ID = 0 OR CO_ID = T2.CO_ID) ORDER BY CO_ID DESC) GRADE_NM,
					         	    T2.duty_cd,
					         	    (SELECT TOP 1 NM_KR FROM BX.TCMG_CDD WHERE CM_CD = 'CM0003' AND CD = T2.DUTY_CD AND (CO_ID = 0 OR CO_ID = T2.CO_ID) ORDER BY CO_ID DESC) DUTY_NM,
					         	    T2.tel
				             FROM BX.TCMG_USER T1 WITH (NOLOCK)      
				             JOIN BX.TCMG_USERDEPT T2 WITH (NOLOCK) ON T1. USER_ID = T2.USER_ID   
				             WHERE T1.logon_cd = '{0}'";

                gw.Query = string.Format(query, 아이디);
                DataTable 수신참조정보 = gw.GetDataTable();

                if (수신참조정보 != null && 수신참조정보.Rows.Count > 0)
                {
                    수신참조_유형 += (string.IsNullOrEmpty(수신참조_유형) ? string.Empty : ",") + "10";
                    수신참조_구분 += (string.IsNullOrEmpty(수신참조_구분) ? string.Empty : ",") + 대상구분;
                    수신참조_아이디 += (string.IsNullOrEmpty(수신참조_아이디) ? string.Empty : ",") + 수신참조정보.Rows[0]["user_id"].ToString();
                    수신참조_회사 += (string.IsNullOrEmpty(수신참조_회사) ? string.Empty : ",") + "1";
                    수신참조_부서 += (string.IsNullOrEmpty(수신참조_부서) ? string.Empty : ",") + 수신참조정보.Rows[0]["dept_id"].ToString();
                    수신참조_순서 += (string.IsNullOrEmpty(수신참조_순서) ? string.Empty : ",") + 순서;
                }
            }
        }

		private DataTable 수주정보(string 수주번호)
        {
            string query;

            query = @"SELECT SH.NO_SO, 
                      	     SH.DT_SO, 
                      	     MP.LN_PARTNER AS NM_PARTNER, 
                      	     MH.NM_VESSEL,
                             MC.NM_SYSDEF AS NM_EXCH_SO,
                             MC1.NM_SYSDEF AS NM_EXCH_PO,
                             SH.RT_EXCH,
                             SL.QT_EXCH,
                             ISNULL(SL.AM_EX_SO, 0) AS AM_EX_SO,
                      	     ISNULL(SL.AM_SO, 0) AS AM_SO,
                             ISNULL(SL.AM_EX_PO, 0) AS AM_EX_PO,
                      	     (ISNULL(SL.AM_STOCK, 0) + ISNULL(SL.AM_PO, 0)) AS AM_PO,
                             ISNULL(SL.AM_STOCK, 0) AS AM_STOCK,
                             (ISNULL(SL.AM_EX_SO, 0) - ISNULL(SL.AM_EX_PO, 0)) AS AM_EX_PROFIT,
                      	     (ISNULL(SL.AM_SO, 0) - (ISNULL(SL.AM_STOCK, 0) + ISNULL(SL.AM_PO, 0))) AS AM_PROFIT,
                             (CASE WHEN ISNULL(SL.AM_EX_SO, 0) = 0 THEN 0 
											                       ELSE ROUND(((1 - (ISNULL(SL.AM_EX_PO, 0) / ISNULL(SL.AM_EX_SO, 0))) * 100), 2) END) AS RT_EX_PROFIT, 
                      	     (CASE WHEN ISNULL(SL.AM_SO, 0) = 0 THEN 0 
                                                                ELSE ROUND(((1 - ((ISNULL(SL.AM_PO, 0) + ISNULL(SL.AM_STOCK, 0)) / ISNULL(SL.AM_SO, 0))) * 100), 2) END) AS RT_PROFIT 
                      FROM SA_SOH SH WITH(NOLOCK)
                      LEFT JOIN MA_PARTNER MP WITH(NOLOCK) ON MP.CD_COMPANY = SH.CD_COMPANY AND MP.CD_PARTNER = SH.CD_PARTNER
                      LEFT JOIN CZ_MA_HULL MH WITH(NOLOCK) ON MH.NO_IMO = SH.NO_IMO
                      LEFT JOIN (SELECT SH.CD_COMPANY, SH.NO_SO,
                                 	    PL.CD_EXCH,
                                 	    PL.QT_EXCH,
                                 	    SL.AM_EX_SO,
                                 	    SL.AM_SO,
                                 	    PL.AM_EX_PO,
                                 	    PL.AM_PO,
                                 	    SL.AM_STOCK 
                                 FROM SA_SOH SH WITH(NOLOCK)
                                 LEFT JOIN (SELECT SL.CD_COMPANY,
                                 				   SL.NO_SO,
                                 				   SUM(SL.AM_EX_S) AS AM_EX_SO,
                                 				   SUM(SL.AM_KR_S) AS AM_SO,
                                 				   SUM(SB.UM_KR * SB.QT_STOCK) AS AM_STOCK 
                                 		   FROM SA_SOL SL WITH(NOLOCK)
                                 		   LEFT JOIN CZ_SA_STOCK_BOOK SB WITH(NOLOCK) ON SB.CD_COMPANY = SL.CD_COMPANY AND SB.NO_FILE = SL.NO_SO AND SB.NO_LINE = SL.SEQ_SO
                                 		   GROUP BY SL.CD_COMPANY, SL.NO_SO) SL 
                                 ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
                                 LEFT JOIN (SELECT PL.CD_COMPANY, PL.NO_SO,
                                 				   MAX(PH.CD_EXCH) AS CD_EXCH,
                                                   COUNT(DISTINCT PH.CD_EXCH) AS QT_EXCH,
                                 				   SUM(PL.AM_EX) AS AM_EX_PO,
                                                   SUM(PL.AM) AS AM_PO
                                 		   FROM PU_POL PL WITH(NOLOCK)
                                 		   LEFT JOIN PU_POH PH WITH(NOLOCK) ON PH.CD_COMPANY = PL.CD_COMPANY AND PH.NO_PO = PL.NO_PO
                                 		   GROUP BY PL.CD_COMPANY, PL.NO_SO) PL
                                 ON PL.CD_COMPANY = SH.CD_COMPANY AND PL.NO_SO = SH.NO_SO) SL
                      ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
                      LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = SH.CD_COMPANY AND MC.CD_FIELD = 'MA_B000005' AND MC.CD_SYSDEF = SH.CD_EXCH
                      LEFT JOIN MA_CODEDTL MC1 ON MC1.CD_COMPANY = SL.CD_COMPANY AND MC1.CD_FIELD = 'MA_B000005' AND MC1.CD_SYSDEF = SL.CD_EXCH
                      WHERE SH.CD_COMPANY = '{0}'
                      AND SH.NO_SO = '{1}'";

            query = string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 수주번호);

            return Global.MainFrame.FillDataTable(query);
        }

        private DataTable 발주정보(string 발주번호)
        {
            string query, 반올림;

            if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                반올림 = "2";
            else
                반올림 = "0";

            query = @"SELECT PH.CD_COMPANY, PH.NO_PO,
                             MAX(SL.NM_SYSDEF) AS NM_EXCH_SO,
                             MAX(MC.NM_SYSDEF) AS NM_EXCH_PO,
                             ISNULL(SUM(SL.AM_EX_SO), 0) AS AM_PU_EX_SO,
                      	     ISNULL(SUM(SL.AM_SO), 0) AS AM_PU_SO,
                             ISNULL(SUM(PL.AM_EX_PO), 0) AS AM_PU_EX_PO,
                      	     ISNULL(SUM(PL.AM_PO), 0) AS AM_PU_PO,
                             (CASE WHEN MAX(PH.CD_TPPO) IN ('1300', '1400', '2300', '2400') THEN 0 ELSE (ISNULL(SUM(SL.AM_EX_SO), 0) - ISNULL(SUM(PL.AM_EX_PO), 0)) END) AS AM_PU_EX_PROFIT,
	                         (CASE WHEN MAX(PH.CD_TPPO) IN ('1300', '1400', '2300', '2400') THEN 0 ELSE (ISNULL(SUM(SL.AM_SO), 0) - ISNULL(SUM(PL.AM_PO), 0)) END) AS AM_PU_PROFIT,
                             (CASE WHEN ISNULL(SUM(SL.AM_EX_SO), 0) = 0 THEN 0 
                      												    ELSE ROUND(((1 - (ISNULL(SUM(PL.AM_EX_PO), 0) / ISNULL(SUM(SL.AM_EX_SO), 0))) * 100), 2) END) AS RT_PU_EX_PROFIT,
                      	     (CASE WHEN ISNULL(SUM(SL.AM_SO), 0) = 0 THEN 0 
                      												 ELSE ROUND(((1 - (ISNULL(SUM(PL.AM_PO), 0) / ISNULL(SUM(SL.AM_SO), 0))) * 100), 2) END) AS RT_PU_PROFIT
                      FROM PU_POH PH WITH(NOLOCK)
                      LEFT JOIN (SELECT CD_COMPANY, NO_PO, NO_SO, NO_SOLINE,
                                        SUM(AM_EX) AS AM_EX_PO,
                      				    SUM(AM) AS AM_PO
                      		     FROM PU_POL WITH(NOLOCK)
                      		     GROUP BY CD_COMPANY, NO_PO, NO_SO, NO_SOLINE) PL 
                      ON PL.CD_COMPANY = PH.CD_COMPANY AND PL.NO_PO = PH.NO_PO
                      LEFT JOIN (SELECT SL.CD_COMPANY, SL.NO_SO, SL.SEQ_SO,
                                        MC.NM_SYSDEF,
                                        ROUND(SL.UM_EX_S * ISNULL(SL.QT_PO, 0), {0}) AS AM_EX_SO,  
                      				    ROUND(SL.UM_EX_S * ISNULL(SL.QT_PO, 0) * SH.RT_EXCH, {0}) AS AM_SO  
                      		     FROM SA_SOL SL WITH(NOLOCK)
                      		     LEFT JOIN SA_SOH SH WITH(NOLOCK) ON SH.CD_COMPANY = SL.CD_COMPANY AND SH.NO_SO = SL.NO_SO
                                 LEFT JOIN MA_CODEDTL MC WITH(NOLOCK) ON MC.CD_COMPANY = SH.CD_COMPANY AND MC.CD_FIELD = 'MA_B000005' AND MC.CD_SYSDEF = SH.CD_EXCH) SL 
                      ON SL.CD_COMPANY = PL.CD_COMPANY AND SL.NO_SO = PL.NO_SO AND SL.SEQ_SO = PL.NO_SOLINE
                      LEFT JOIN MA_CODEDTL MC WITH(NOLOCK) ON MC.CD_COMPANY = PH.CD_COMPANY AND MC.CD_FIELD = 'MA_B000005' AND MC.CD_SYSDEF = PH.CD_EXCH
                      WHERE PH.CD_COMPANY = '{1}'
                      AND PH.NO_PO = '{2}'
                      GROUP BY PH.CD_COMPANY, PH.NO_PO";

            query = string.Format(query, 반올림, Global.MainFrame.LoginInfo.CompanyCode, 발주번호);

            return Global.MainFrame.FillDataTable(query);
        }

        private string 전표정보(string noDocu)
        {
            string query, html;
            DataTable dt;

            query = @"SELECT FD.NO_DOCU,
                             FD.NO_DOLINE,
                             FD.CD_ACCT,
                             FA.NM_ACCT,
                             FD.NM_NOTE,
                             FD.AM_DR,
                             FD.AM_CR
                      FROM FI_DOCU FD WITH(NOLOCK)
                      LEFT JOIN FI_ACCTCODE FA WITH(NOLOCK) ON FA.CD_COMPANY = FD.CD_COMPANY AND FA.CD_ACCT = FD.CD_ACCT
                      WHERE FD.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                     "AND FD.NO_DOCU = '" + noDocu + "'" + Environment.NewLine +
                     "ORDER BY NO_DOLINE";

            dt = Global.MainFrame.FillDataTable(query);
            html = string.Empty;

            foreach(DataRow dr in dt.Rows)
            {
                html += "<tr style='height:30px'>" + Environment.NewLine +
                        "   <td style='border:solid 1px black; text-align:center'>" + D.GetString(dr["NM_ACCT"]) + "</td>" + Environment.NewLine +
                        "   <td style='border:solid 1px black; text-align:center'>" + D.GetString(dr["NM_NOTE"]) + "</td>" + Environment.NewLine +
                        "   <td style='border:solid 1px black; text-align:right; padding-right:10px'>" + D.GetDecimal(dr["AM_DR"]).ToString("N") + "</td>" + Environment.NewLine +
                        "   <td style='border:solid 1px black; text-align:right; padding-right:10px'>" + D.GetDecimal(dr["AM_CR"]).ToString("N") + "</td>" + Environment.NewLine +
                        "</tr>";
            }

            return html;
        }

        private DataTable 매입처계좌정보(string cdPartner)
        {
            string query;

            query = @"SELECT MP.NO_DEPOSIT,
                             MP.NM_DEPOSIT,
                             MP.CD_BANK,
                             (SELECT (CASE '" + Global.MainFrame.LoginInfo.Language + @"' WHEN 'KR' THEN NM_SYSDEF
		                      						                                      WHEN 'US' THEN NM_SYSDEF_E
		                      						                                      WHEN 'JP' THEN NM_SYSDEF_JP
		                      						                                      WHEN 'CH' THEN NM_SYSDEF_CH END)
		                      FROM MA_CODEDTL
		                      WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + @"'
		                      AND CD_FIELD = 'MA_B000043'
		                      AND CD_SYSDEF = MP.CD_BANK) AS NM_BANK
                      FROM MA_PARTNER MP WITH(NOLOCK)
                      WHERE MP.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                     "AND MP.CD_PARTNER = '" + cdPartner + "'";

            return Global.MainFrame.FillDataTable(query);
        }

        private string 외화수주정보(DataRow dr)
        {
            string html;

            html = @"<tr style='height:30px'>
                        <th style='border:solid 1px black; text-align:center; background-color:Silver'>" + (Global.MainFrame.LoginInfo.Language == "US" ? "SO Amount(" + D.GetString(dr["NM_EXCH_SO"]) + ")" : "수 주 금 액(" + D.GetString(dr["NM_EXCH_SO"]) + ")") + @"</th>
                        <th style='border:solid 1px black; text-align:center; background-color:Silver'>" + (Global.MainFrame.LoginInfo.Language == "US" ? "PO Amount(" + D.GetString(dr["NM_EXCH_PO"]) + ")" : "발 주 금 액(" + D.GetString(dr["NM_EXCH_PO"]) + ")") + @"</th>
                        <th style='border:solid 1px black; text-align:center; background-color:Silver'>" + (Global.MainFrame.LoginInfo.Language == "US" ? "Profit" : "이 윤") + @"</th>
                        <th style='border:solid 1px black; text-align:center; background-color:Silver'>" + (Global.MainFrame.LoginInfo.Language == "US" ? "Profit Rate" : "이 윤 율") + @"</th>
                     </tr>
                     <tr style='height:30px'>
                        <td style='border:solid 1px black; text-align:right; padding-right:10px'>" + D.GetDecimal(dr["AM_EX_SO"]).ToString("N") + @"</td>
                        <td style='border:solid 1px black; text-align:right; padding-right:10px'>" + D.GetDecimal(dr["AM_EX_PO"]).ToString("N") + @"</td>
                        <td style='border:solid 1px black; text-align:right; padding-right:10px'>" + D.GetDecimal(dr["AM_EX_PROFIT"]).ToString("N") + @"</td>
                        <td style='border:solid 1px black; text-align:right; padding-right:10px'>" + D.GetDecimal(dr["RT_EX_PROFIT"]).ToString("N") + @"</td>
                     </tr>";
            
            return html;
        }

        private string 외화발주정보(DataRow dr)
        {
            string html;

            html = @"<tr style='height:30px'>
                        <th style='border:solid 1px black; text-align:center; background-color:Silver'>" + (Global.MainFrame.LoginInfo.Language == "US" ? "SO Amount(" + D.GetString(dr["NM_EXCH_SO"]) + ")" : "수 주 금 액(" + D.GetString(dr["NM_EXCH_SO"]) + ")") + @"</th>
                        <th style='border:solid 1px black; text-align:center; background-color:Silver'>" + (Global.MainFrame.LoginInfo.Language == "US" ? "PO Amount(" + D.GetString(dr["NM_EXCH_PO"]) + ")" : "발 주 금 액(" + D.GetString(dr["NM_EXCH_PO"]) + ")") + @"</th>
                        <th style='border:solid 1px black; text-align:center; background-color:Silver'>" + (Global.MainFrame.LoginInfo.Language == "US" ? "Profit" : "이 윤") + @"</th>
                        <th style='border:solid 1px black; text-align:center; background-color:Silver'>" + (Global.MainFrame.LoginInfo.Language == "US" ? "Profit Rate" : "이 윤 율") + @"</th>
                     </tr>
                     <tr style='height:30px'>
                        <td style='border:solid 1px black; text-align:right; padding-right:10px'>" + D.GetDecimal(dr["AM_PU_EX_SO"]).ToString("N") + @"</td>
                        <td style='border:solid 1px black; text-align:right; padding-right:10px'>" + D.GetDecimal(dr["AM_PU_EX_PO"]).ToString("N") + @"</td>
                        <td style='border:solid 1px black; text-align:right; padding-right:10px'>" + D.GetDecimal(dr["AM_PU_EX_PROFIT"]).ToString("N") + @"</td>
                        <td style='border:solid 1px black; text-align:right; padding-right:10px'>" + D.GetDecimal(dr["RT_PU_EX_PROFIT"]).ToString("N") + @"</td>
                     </tr>";

            return html;
        }

        private string 첨부파일(string 발주번호)
		{
            string html, html1, query;

            query = @"SELECT FILE_PATH, FILE_NAME, CD_FILE
FROM MA_FILEINFO WITH(NOLOCK)
WHERE CD_COMPANY = '{0}'
AND CD_MODULE = 'MA'
AND ID_MENU = 'P_CZ_PU_ADPAYMENT_MNG'
AND CD_FILE = '{1}'";

            DataTable dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 발주번호));

            html = string.Empty;
            html1 = string.Empty;

            if (dt != null && dt.Rows.Count > 0)
			{
                html = @"<div style='font-weight:bold; margin-bottom:10px; margin-top:30px'>5. 첨부 파일</div>
<table style='width:100%; border:2px solid black; margin-bottom: 10px; font-size: 9pt; font-family: 굴림'>
    <colgroup width='100%' align='center'></colgroup>
        <tbody>
            <tr style='height: 30px'>
                <td style='border:solid 1px black; text-align:left; padding-left:10px'>{0}</td>
            </tr>
        </tbody>
</table>";
                foreach (DataRow dr in dt.Rows)
                {
                    html1 += @"<a href='http://113.130.254.144:85/ERP-U/" + dr["FILE_PATH"].ToString() + "/" + dr["CD_FILE"].ToString() + "/" + Uri.EscapeUriString(dr["FILE_NAME"].ToString()) + "' style='color:#0000ff; text-decoration:underline; font-weight:bold' target='_blank'>" + dr["FILE_NAME"].ToString() + @" </a><br><br>";
                }

                html = string.Format(html, html1);
            }

            return html;
		}
	}
}

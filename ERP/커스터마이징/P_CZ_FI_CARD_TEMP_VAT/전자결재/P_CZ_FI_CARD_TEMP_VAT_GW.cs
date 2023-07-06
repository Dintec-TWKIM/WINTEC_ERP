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
	internal class P_CZ_FI_CARD_TEMP_VAT_GW
	{
		internal bool 전자결재(DataRow header, DataTable dt1, DataTable dt2, DataTable dt3)
		{
			bool isSuccess;
			string strURL, key, 제목;
            int 양식번호; 

            key = (MA.Login.회사코드 + "-" + D.GetString(header["NO_DOCU"]));

			양식번호 = 1022;
			제목 = (Global.MainFrame.DD("법인카드 사용 품의서") + "_" + D.GetString(header["NO_DOCU"]));

			isSuccess = 결재상신(new object[] { GroupWare.GetERP_CD_COMPANY(),
                                                GroupWare.GetERP_CD_PC(),
                                                key,
                                                MA.Login.사원번호,
                                                Global.MainFrame.GetStringToday,
                                                this.GetHtml(header, dt1, dt2, dt3),
                                                (Global.MainFrame.DD("법인카드 사용 품의서") + "_" + D.GetString(header["NO_DOCU"])),
                                                "Y",
                                                양식번호 });

			if (!isSuccess) return false;

			//this.자동결재상신(key, 제목, MA.Login.사원번호, header["NO_EMPMNG"].ToString(), D.GetDecimal(header["AM_AMT"]));

			strURL = "http://gw.dintec.co.kr" + "/kor_webroot/src/cm/tims/index.aspx"
					 + "?cd_company=" + GroupWare.GetERP_CD_COMPANY()
					 + "&cd_pc=" + GroupWare.GetERP_CD_PC()
					 + "&no_docu=" + HttpUtility.UrlEncode(key, Encoding.UTF8)
					 + "&login_id=" + MA.Login.사원번호;

			Process.Start("msedge.exe", strURL);

			return isSuccess;
		}

        internal void 미리보기(DataRow header, DataTable dt1, DataTable dt2, DataTable dt3)
        {
            string html;

            html = this.GetHtml(header, dt1, dt2, dt3);
            P_CZ_MA_HTML_VIEWER dialog = new P_CZ_MA_HTML_VIEWER(Global.MainFrame.DD("법인카드사용품의서"), html);

            dialog.ShowDialog();
        }

        private string GetHtml(DataRow header, DataTable dt1, DataTable dt2, DataTable dt3)
		{
			string path, body, 전표정보1, 전표정보2, 원화형식;
			
            body = string.Empty;
			path = Application.StartupPath + "\\download\\gw\\HT_P_CZ_FI_CARD_TEMP_VAT_BODY.htm";
			
            //body = File.ReadAllText(path, System.Text.UTF8Encoding.UTF8);
            using (StreamReader reader = new StreamReader(path, Encoding.Default))
            {
                body = reader.ReadToEnd();
            }

			if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
				원화형식 = "N";
			else
				원화형식 = "N0";

			body = body.Replace("@@NO_DOCU", header["NO_DOCU"].ToString());
			body = body.Replace("@@DT_WRITE", Util.GetTo_DateStringS(dt1.Rows[0]["DT_WRITE"]));
			body = body.Replace("@@DT_ACCT", Util.GetTo_DateStringS(dt1.Rows[0]["DT_ACCT"]));
			body = body.Replace("@@FI_TYPE", dt1.Rows[0]["FI_TYPE"].ToString());

			body = body.Replace("@@NM_PUMM", dt1.Rows[0]["NM_PUMM"].ToString());

			body = body.Replace("@@AM_HAN", D.GetHangleAmt(D.GetDecimal(dt1.Rows[0]["AM_AMT"])));
			body = body.Replace("@@AM_KR", D.GetDecimal(dt1.Rows[0]["AM_AMT"]).ToString(원화형식));
			
			전표정보1 = string.Empty;
			전표정보2 = string.Empty;

			foreach (DataRow dr in dt2.Rows)
			{
				전표정보1 += "<tr style='height:30px'>" + Environment.NewLine +
							"   <td style='border:solid 1px black; text-align:left; padding-left:10px'>" + D.GetString(dr["NMD_MNGD"]) + "</td>" + Environment.NewLine +
							"   <td style='border:solid 1px black; text-align:right; padding-right:10px'>" + D.GetDecimal(dr["AM_AMT"]).ToString(원화형식) + "</td>" + Environment.NewLine +
						    "</tr>";
			}

			foreach(DataRow dr in dt3.Rows)
			{
				전표정보2 += "<tr style='height:30px'>" + Environment.NewLine +
							"   <td style='border:solid 1px black; text-align:left; padding-left:10px'>" + D.GetString(dr["NM_ACCT"]) + "</td>" + Environment.NewLine +
							"   <td style='border:solid 1px black; text-align:left; padding-left:10px'>" + D.GetString(dr["NM_NOTE"]) + "</td>" + Environment.NewLine +
							"   <td style='border:solid 1px black; text-align:right; padding-right:10px'>" + D.GetDecimal(dr["AM_DR"]).ToString(원화형식) + "</td>" + Environment.NewLine +
						    "   <td style='border:solid 1px black; text-align:right; padding-right:10px'>" + D.GetDecimal(dr["AM_CR"]).ToString(원화형식) + "</td>" + Environment.NewLine +
						    "</tr>";
			}
			
			body = body.Replace("@@1_TXT_DOCU", 전표정보1);
			body = body.Replace("@@2_TXT_DOCU", 전표정보2);

			body = body.Replace("@@AM_CR_TOTAL", D.GetDecimal(dt2.Compute("SUM(AM_CR)", string.Empty)).ToString(원화형식));
			body = body.Replace("@@AM_DR_TOTAL", D.GetDecimal(dt2.Compute("SUM(AM_DR)", string.Empty)).ToString(원화형식));

			return body;
		}

		private bool 결재상신(object[] obj)
		{
			return DBHelper.ExecuteNonQuery("SP_CZ_FI_GWDOCU", obj);
		}

		private bool 자동결재상신(string key, string 제목, string 상신자, string 결재자, decimal 전표금액)
		{
			string query, 결재보관함;
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
				   부서결재여부 = string.Empty;
			DataTable 상신자정보, 결재자정보, 결재정보;

			//BX.TCMG_MENU
			if (Global.MainFrame.LoginInfo.CompanyCode == "K100")
				결재보관함 = "2000493"; //비용관련
			else if (Global.MainFrame.LoginInfo.CompanyCode == "K200")
				결재보관함 = "2000494"; //두베코 비용관련
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
				      JOIN BX.TCMG_USERDEPT T2 WITH (NOLOCK) ON T1.USER_ID = T2.USER_ID   
				      WHERE T1.logon_cd = '{0}'";

			gw.Query = string.Format(query, 상신자);
			상신자정보 = gw.GetDataTable();

			if (상신자정보 == null || 상신자정보.Rows.Count != 1)
				return false;

			gw.Query = string.Format(query, 결재자); // 부서장
			결재자정보 = gw.GetDataTable();

			if (결재자정보 == null || 결재자정보.Rows.Count != 1)
				return false;

			결재라인Master += ",3";
			결재라인Detail += ",1";
			결재라인Activity += ",3000"; //결재
			결재라인사용자 += "," + 결재자정보.Rows[0]["user_id"].ToString();
			결재라인회사 += ",1";
			결재라인부서 += "," + 결재자정보.Rows[0]["dept_id"].ToString();
			결재라인직급 += "," + 결재자정보.Rows[0]["grade_cd"].ToString();
			결재라인직책 += "," + 결재자정보.Rows[0]["duty_cd"].ToString();
			결재라인상태 += ",20";
			결재라인구분 += ",1";
			결재여부 += ",0";
			전결가능여부 += ",0";
			부서결재여부 += ",0";

			#region 결재라인
			if (Global.MainFrame.LoginInfo.CompanyCode == "K100")
			{
				#region 딘텍
				gw.Query = string.Format(query, "S-046"); // 손영봉 이사님
				결재자정보 = gw.GetDataTable();

				if (결재자정보 == null || 결재자정보.Rows.Count != 1)
					return false;

				결재라인Master += ",2";
				결재라인Detail += ",1";
				결재라인Activity += ",4000"; //협의
				결재라인사용자 += "," + 결재자정보.Rows[0]["user_id"].ToString();
				결재라인회사 += ",1";
				결재라인부서 += "," + 결재자정보.Rows[0]["dept_id"].ToString();
				결재라인직급 += "," + 결재자정보.Rows[0]["grade_cd"].ToString();
				결재라인직책 += "," + 결재자정보.Rows[0]["duty_cd"].ToString();
				결재라인상태 += ",20";
				결재라인구분 += ",1";
				결재여부 += ",0";
				전결가능여부 += ",0";
				부서결재여부 += ",0";

				if (전표금액 >= 300000) // 30만원 이상
				{
					gw.Query = string.Format(query, "S-279"); // 사장님
					결재자정보 = gw.GetDataTable();

					if (결재자정보 == null || 결재자정보.Rows.Count != 1)
						return false;

					결재라인Master += ",3";
					결재라인Detail += ",1";
					결재라인Activity += ",3000"; //결재
					결재라인사용자 += "," + 결재자정보.Rows[0]["user_id"].ToString();
					결재라인회사 += ",1";
					결재라인부서 += "," + 결재자정보.Rows[0]["dept_id"].ToString();
					결재라인직급 += "," + 결재자정보.Rows[0]["grade_cd"].ToString();
					결재라인직책 += "," + 결재자정보.Rows[0]["duty_cd"].ToString();
					결재라인상태 += ",20";
					결재라인구분 += ",1";
					결재여부 += ",0";
					전결가능여부 += ",0";
					부서결재여부 += ",0";
				}

				if (전표금액 >= 1000000) // 100만원 이상
				{
					gw.Query = string.Format(query, "S-001"); // 회장님
					결재자정보 = gw.GetDataTable();

					if (결재자정보 == null || 결재자정보.Rows.Count != 1)
						return false;

					결재라인Master += ",3";
					결재라인Detail += ",1";
					결재라인Activity += ",3000"; //결재
					결재라인사용자 += "," + 결재자정보.Rows[0]["user_id"].ToString();
					결재라인회사 += ",1";
					결재라인부서 += "," + 결재자정보.Rows[0]["dept_id"].ToString();
					결재라인직급 += "," + 결재자정보.Rows[0]["grade_cd"].ToString();
					결재라인직책 += "," + 결재자정보.Rows[0]["duty_cd"].ToString();
					결재라인상태 += ",20";
					결재라인구분 += ",1";
					결재여부 += ",0";
					전결가능여부 += ",0";
					부서결재여부 += ",0";
				}
				#endregion
			}
			else if (Global.MainFrame.LoginInfo.CompanyCode == "K200")
			{
				#region 두베코
				gw.Query = string.Format(query, "D-004"); // 권혜원 부장님
				결재자정보 = gw.GetDataTable();

				if (결재자정보 == null || 결재자정보.Rows.Count != 1)
					return false;

				결재라인Master += ",2";
				결재라인Detail += ",1";
				결재라인Activity += ",4000"; //협의
				결재라인사용자 += "," + 결재자정보.Rows[0]["user_id"].ToString();
				결재라인회사 += ",1";
				결재라인부서 += "," + 결재자정보.Rows[0]["dept_id"].ToString();
				결재라인직급 += "," + 결재자정보.Rows[0]["grade_cd"].ToString();
				결재라인직책 += "," + 결재자정보.Rows[0]["duty_cd"].ToString();
				결재라인상태 += ",20";
				결재라인구분 += ",1";
				결재여부 += ",0";
				전결가능여부 += ",0";
				부서결재여부 += ",0";

				gw.Query = string.Format(query, "D-038"); // 최규대 대표님
				결재자정보 = gw.GetDataTable();

				if (결재자정보 == null || 결재자정보.Rows.Count != 1)
					return false;

				결재라인Master += ",3";
				결재라인Detail += ",1";
				결재라인Activity += ",3000"; //결재
				결재라인사용자 += "," + 결재자정보.Rows[0]["user_id"].ToString();
				결재라인회사 += ",1";
				결재라인부서 += "," + 결재자정보.Rows[0]["dept_id"].ToString();
				결재라인직급 += "," + 결재자정보.Rows[0]["grade_cd"].ToString();
				결재라인직책 += "," + 결재자정보.Rows[0]["duty_cd"].ToString();
				결재라인상태 += ",20";
				결재라인구분 += ",1";
				결재여부 += ",0";
				전결가능여부 += ",0";
				부서결재여부 += ",0";
				#endregion
			}
			#endregion

			#region 결재상신
			query = @"exec bx.PEA_SETAPPDOCSAVE @nGrpID=2330,
											    @nCoID=1,
											    @nDeptID='" + 상신자정보.Rows[0]["dept_id"].ToString() + @"',
											    @nUserID='" + 상신자정보.Rows[0]["user_id"].ToString() + @"',
											    
											    @nDocID=0,
											    @nFormID=101,
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
											    
											    @sReceiveDIVs = N'',
											    @sReceiveOrgDIVs = N'',
											    @sReceiveOrgIDs = N'',
											    @sReceiveCoIDs = N'',
											    @sReceiveDeptIDs = N'',
											    @sReceiveAppLineDSeqs = N'',
											    
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
	}
}
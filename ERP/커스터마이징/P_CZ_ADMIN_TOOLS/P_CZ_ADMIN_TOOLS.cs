using Aspose.Email.Outlook;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;
using Duzon.Windows.Print;
using DX;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Parsing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;

namespace cz
{
	public partial class P_CZ_ADMIN_TOOLS : PageBase
	{
		private bool 첫번째창 = true;

		private DataGridView 결과창
		{
			get
			{
				if (this.첫번째창 == true)
					return this.dgv결과1;
				else
					return this.dgv결과2;
			}
		}

		private TextBoxExt 쿼리
		{
			get
			{
				if (this.첫번째창 == true)
					return this.txt쿼리1;
				else
					return this.txt쿼리2;
			}
		}

		public enum UserServiceFlag
		{
			None = 0,
			Smtp = 1,
			Pop3 = 2,
			WebMail = 4,
			Imap = 8,
			ActiveSync = 16, // 0x00000010
			WebMobile = 32, // 0x00000020
			All = 255, // 0x000000FF
			SmtpInternet = 65536, // 0x00010000
			Pop3Internet = 131072, // 0x00020000
			WebMailInternet = 262144, // 0x00040000
			ImapInternet = 524288, // 0x00080000
		}

		#region 초기화
		public P_CZ_ADMIN_TOOLS()
		{
			StartUp.Certify(this);
			InitializeComponent();
		}

		protected override void InitLoad()
		{
			base.InitLoad();

			this.InitEvent();
		}

		protected override void InitPaint()
		{
			base.InitPaint();

			this.splitContainer1.SplitterDistance = 810;
			this.splitContainer2.SplitterDistance = 810;

			this.cboDB유형.DataSource = MA.GetCodeUser(new string[] { "",
																	  "000",
																	  "001",
																	  "002",
																	  "003",
																	  "004",
																	  "005",
																	  "006",
																	  "007" }, new string[] { "",
																							  "딘텍(구전산)",
																							  "두베코(구전산)",
																							  "GMI(구전산)",
																							  "ERP",
																							  "그룹웨어",
																							  "메일",
																							  "팩스",
																							  "MES" });
			this.cboDB유형.ValueMember = "CODE";
			this.cboDB유형.DisplayMember = "NAME";
		}

		private void InitEvent()
		{
			this.txt쿼리1.GotFocus += new EventHandler(this.txt쿼리_GotFocus);
			this.txt쿼리2.GotFocus += new EventHandler(this.txt쿼리_GotFocus);

			this.btnERP조회.Click += new EventHandler(this.btnERP조회_Click);
			this.btn외부DB조회.Click += new EventHandler(this.btn외부DB조회_Click);
			this.btn관리자권한조회.Click += new EventHandler(this.btn관리자권한조회_Click);
			this.btn프로시저.Click += new EventHandler(this.btn프로시저_Click);
			this.btn테이블.Click += new EventHandler(this.btn테이블_Click);
			this.btn종속성확인.Click += new EventHandler(this.btn종속성확인_Click);
			this.btn제약조건.Click += new EventHandler(this.btn제약조건_Click);
			this.btn인덱스.Click += new EventHandler(this.btn인덱스_Click);
			this.btn컬럼사용처.Click += new EventHandler(this.btn컬럼사용처_Click);
			this.btnText검색.Click += new EventHandler(this.btnText검색_Click);
			this.btn사용자정보ERP.Click += new EventHandler(this.btn사용자정보ERP_Click);
			this.btn암호화복호화.Click += new EventHandler(this.btn암호화복호화_Click);
			this.btnFAX발송.Click += new EventHandler(this.btnFAX발송_Click);
			this.btn메일발송.Click += new EventHandler(this.btn메일발송_Click);
			this.btn메일발송2.Click += new EventHandler(this.btn메일발송2_Click);
			this.btn쪽지보내기.Click += new EventHandler(this.btn쪽지보내기_Click);
			this.btn사용자정보메일.Click += new EventHandler(this.btn사용자정보메일_Click);
			this.btn메일링리스트.Click += new EventHandler(this.btn메일링리스트_Click);
			this.btn사용자정보GW.Click += new EventHandler(this.btn사용자정보GW_Click);
			this.btn그룹웨어정보갱신.Click += new EventHandler(this.btn그룹웨어정보갱신_Click);
			this.btn그룹웨어문서삭제.Click += new EventHandler(this.btn그룹웨어문서삭제_Click);
			this.btn코드동기화.Click += new EventHandler(this.btn코드동기화_Click);
			this.btn불용어제거.Click += new EventHandler(this.btn불용어제거_Click);
			this.cur원금액.DecimalValueChanged += new EventHandler(this.cur원금액_DecimalValueChanged);
			this.btn단체메일발송.Click += new EventHandler(this.btn단체메일발송_Click);
			this.btn사용자정보클라우독.Click += new EventHandler(this.btn사용자정보클라우독_Click);

			this.ctxEWS테스트.QueryAfter += new BpQueryHandler(this.ctxEWS테스트_QueryAfter);
            this.btnPythonTest.Click += new EventHandler(this.btnPythonTest_Click);
			this.btn메일등록.Click += new EventHandler(this.btn메일등록_Click);
			this.btn첨부파일분리.Click += new EventHandler(this.btn첨부파일분리_Click);
            this.btn워크파일다운로드.Click += Btn워크파일다운로드_Click;

            this.btn정규식테스트.Click += Btn정규식테스트_Click;
			this.btn문자보내기.Click += Btn문자보내기_Click;

			this.btnPDF압축.Click += BtnPDF압축_Click;
			this.btn사원등록GW.Click += Btn사원등록GW_Click;
			this.btn사진DB에저장.Click += Btn사진DB에저장_Click;
			this.btn호선위치.Click += Btn호선위치_Click;
		}

		private void Btn호선위치_Click(object sender, EventArgs e)
		{
			try
			{
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
				//ServicePointManager.Expect100Continue = true;
				//ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

				Uri address = new Uri("https://api.datalastic.com/api/v0/vessel_pro?api-key=9e05a083-1135-4359-a043-05f54fb4418b&imo=9376749");

				WebClient webClient = new WebClient() { Encoding = Encoding.UTF8 };
				string source = webClient.DownloadString(address);

				JObject obj = (JObject)JsonConvert.DeserializeObject(source);

				string dep_port = ((JValue)obj["data"]["dep_port"]).Value.ToString();
				string dep_port_unlocode = ((JValue)obj["data"]["dep_port_unlocode"]).Value.ToString();
				string destination = ((JValue)obj["data"]["destination"]).Value.ToString();
				string dest_port = ((JValue)obj["data"]["dest_port"]).Value.ToString();
				string dest_port_unlocode = ((JValue)obj["data"]["dest_port_unlocode"]).Value.ToString();
				
				TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("Korea Standard Time"); // 한국 시간대

				DateTime 업데이트시간_UTC = (DateTime)((JValue)obj["data"]["last_position_UTC"]).Value;
				DateTime 업데이트시간 = TimeZoneInfo.ConvertTimeFromUtc(업데이트시간_UTC, timeZone);

				DateTime ATD_UTC = (DateTime)((JValue)obj["data"]["atd_UTC"]).Value;
				DateTime ATD = TimeZoneInfo.ConvertTimeFromUtc(ATD_UTC, timeZone);

				DateTime ETA_UTC = (DateTime)((JValue)obj["data"]["eta_UTC"]).Value;
				DateTime ETA = TimeZoneInfo.ConvertTimeFromUtc(ETA_UTC, timeZone);

				string strURL = "http://maps.google.co.kr/maps?q=" + HttpUtility.UrlEncode(obj["data"]["lat"].ToString(), Encoding.UTF8) + "," + HttpUtility.UrlEncode(obj["data"]["lon"].ToString(), Encoding.UTF8);
				Process.Start("msedge.exe", strURL);

				this.ShowMessage("업데이트 : " + 업데이트시간.ToString("yyyy.MM.dd HH:mm:ss") + Environment.NewLine +
								 dep_port + " -> " + dest_port + "(" + destination + ")" + Environment.NewLine +
								 "출발 : " + ATD.ToString("yyyy.MM.dd HH:mm:ss") + " (" + dep_port_unlocode + ")" + Environment.NewLine +
								 "도착 : " + ETA.ToString("yyyy.MM.dd HH:mm:ss") + " (" + dest_port_unlocode + ")");
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn사진DB에저장_Click(object sender, EventArgs e)
		{
			DataTable dt;
			string query, imagePath;
			byte[] imageData;

			try
			{
				query = @"SELECT A.CD_COMPANY, 
       A.NO_EMP,
       A.DC_PHOTO 
FROM HR_PHOTO A WITH(NOLOCK)
WHERE ISNULL(A.DC_PHOTO, '') <> ''
AND LEFT(A.NO_EMP, 2) IN ('S-', 'D-')
AND NOT EXISTS (SELECT 1 
                FROM CZ_HR_PHOTO B
                WHERE B.CD_COMPANY = A.CD_COMPANY
                AND B.NO_EMP = A.NO_EMP)";

				dt = DBHelper.GetDataTable(query);
				WebClient wc = new WebClient();

				foreach (DataRow dr in dt.Rows)
				{
					imagePath = "http://113.130.254.144:85/ERP-U/shared/image/human/photo/" + dr["CD_COMPANY"].ToString() + "/" + dr["DC_PHOTO"].ToString();

					try
					{
						wc.DownloadFile(imagePath, @"C:\HR_PHOTO\" + dr["DC_PHOTO"].ToString());
					}
					catch
					{
						continue;
					}

					using (FileStream fs = new FileStream(@"C:\HR_PHOTO\" + dr["DC_PHOTO"].ToString(), FileMode.Open))
					{
						using (BinaryReader br = new BinaryReader(fs))
						{
							imageData = br.ReadBytes((int)fs.Length);
						}
					}

					query = @"INSERT INTO CZ_HR_PHOTO 
							  (CD_COMPANY,
							   NO_EMP,
							   DC_PHOTO, 
							   ID_INSERT, 
							   DTS_INSERT) 
							  VALUES 
							  (@CD_COMPANY,
							   @NO_EMP,
							   @ImageData, 
							   'SYSTEM', 
							   NEOE.SF_SYSDATE(GETDATE()))";

					using (SqlConnection connection = new SqlConnection("Server=113.130.254.143; Database=NEOE; Uid=NEOE; Password=NEOE"))
					{
						using (SqlCommand command = new SqlCommand(query, connection))
						{
							SqlParameter param = new SqlParameter("@ImageData", SqlDbType.VarBinary, imageData.Length);
							param.Value = imageData;
							command.Parameters.Add(param);

							param = new SqlParameter("@CD_COMPANY", dr["CD_COMPANY"].ToString());
							command.Parameters.Add(param);

							param = new SqlParameter("@NO_EMP", dr["NO_EMP"].ToString());
							command.Parameters.Add(param);

							connection.Open();
							command.ExecuteNonQuery();
						}
					}
				}

				this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn사진DB에저장.Text);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn사원등록GW_Click(object sender, EventArgs e)
		{
			string query, domain, 사원번호, 이름;
			DBMgr dbMgr;
			DataTable mailDt, userDt;
			DataRow[] dataRowArray;

			try
			{
				사원번호 = this.쿼리.Text.Split('/')[0].ToUpper();
				이름 = this.쿼리.Text.Split('/')[1];

				if (Global.MainFrame.ShowMessage(string.Format("사원등록({0}/{1}) 진행 하시겠습니까?", 사원번호, 이름), "QY2") != DialogResult.Yes)
					return;

				query = @"SELECT * INTO #USER FROM BX.TCMG_USER WHERE USER_ID = 503
						  SELECT * INTO #DEPT FROM BX.TCMG_USERDEPT WHERE USER_ID = 503
						  SELECT * INTO #ROLE FROM BX.TCMG_ROLEUSER WHERE USER_ID = 503
						  
						  DECLARE @USER_ID INT = (SELECT MAX(USER_ID) FROM BX.TCMG_USER) + 1
						  UPDATE #USER SET USER_ID = @USER_ID, LOGON_CD = '{0}', USER_NM_KR = '{1}'
						  UPDATE #DEPT SET USER_ID = @USER_ID
						  UPDATE #ROLE SET USER_ID = @USER_ID
						  
						  INSERT INTO BX.TCMG_USER SELECT * FROM #USER
						  INSERT INTO BX.TCMG_USERDEPT SELECT * FROM #DEPT
						  INSERT INTO BX.TCMG_ROLEUSER SELECT * FROM #ROLE
						  INSERT INTO BX.TMSG_USER VALUES (@USER_ID, 0, 0, NULL, NULL, NULL, 0)						  

						  DROP TABLE #USER
						  DROP TABLE #DEPT
						  DROP TABLE #ROLE";

				dbMgr = new DBMgr(DBConn.GroupWare);
				dbMgr.Query = string.Format(query, 사원번호, 이름);

				dbMgr.ExecuteNonQuery();

				query = @"SELECT TU.LOGON_CD, 
								 TU.USER_NM_KR,
								 TU.EMAIL_ID,
								 TU.OTHER_EMAIL,
								 '' AS NEW_EMAIL,
								 UD.EMP_NO
						  FROM BX.TCMG_USER TU WITH(NOLOCK)
						  LEFT JOIN BX.TCMG_USERDEPT UD WITH(NOLOCK) ON UD.USER_ID = TU.USER_ID
						  WHERE TU.LOGON_CD = '{0}'
						  AND UD.USER_ID IS NOT NULL AND ISNULL(UD.FIRE_DT, '') = ''";

				dbMgr = new DBMgr(DBConn.GroupWare);
				dbMgr.Query = string.Format(query, 사원번호);

				userDt = dbMgr.GetDataTable();

				query = @"SELECT DU.DU_NAME,
								 DM.DM_NAME,
								 (DU.DU_USERID + '@' + DM.DM_NAME) MAIL_ADDR
						  FROM MCDOMAINUSER DU WITH(NOLOCK)
						  LEFT JOIN MCDOMAIN DM WITH(NOLOCK) ON DM.DM_ID = DU.DM_ID";

				dbMgr = new DBMgr(DBConn.Mail);
				dbMgr.Query = query;

				mailDt = dbMgr.GetDataTable();

				dbMgr = new DBMgr(DBConn.GroupWare);

				foreach (DataRow dr in userDt.Rows)
				{
					domain = string.Empty;

					switch (D.GetString(dr["LOGON_CD"]).Left(2))
					{
						case "S-":
							domain = "dintec.co.kr";
							break;
						case "D-":
							domain = "dubheco.com";
							break;
						case "G-":
							domain = "dintec.com.sg";
							break;
					}

					dataRowArray = mailDt.Select("DU_NAME = '" + dr["USER_NM_KR"].ToString() + "(" + dr["LOGON_CD"].ToString().ToUpper() + ")" + "' AND DM_NAME = '" + domain + "'");

					if (dataRowArray.Length == 1)
					{
						if (D.GetString(dr["EMAIL_ID"]) != D.GetString(dataRowArray[0]["MAIL_ADDR"]) ||
							D.GetString(dr["OTHER_EMAIL"]) != D.GetString(dataRowArray[0]["MAIL_ADDR"]))
						{
							dr["NEW_EMAIL"] = D.GetString(dataRowArray[0]["MAIL_ADDR"]);

							query = @"UPDATE BX.TCMG_USER
									  SET EMAIL_ID = '" + D.GetString(dr["NEW_EMAIL"]) + "'," + Environment.NewLine +
										 "OTHER_EMAIL = '" + D.GetString(dr["NEW_EMAIL"]) + "'" + Environment.NewLine +
									 "WHERE LOGON_CD = '" + D.GetString(dr["LOGON_CD"]) + "'";

							dbMgr.Query = query;
							dbMgr.ExecuteNonQuery();
						}
					}
				}

				this.결과창.DataSource = userDt;

				query = @"UPDATE UD
						  SET UD.EMP_NO = TU.LOGON_CD
						  FROM BX.TCMG_USERDEPT UD
						  JOIN BX.TCMG_USER TU ON TU.USER_ID = UD.USER_ID
						  WHERE TU.LOGON_CD = '{0}'
						  AND ISNULL(UD.EMP_NO, '') <> TU.LOGON_CD";

				dbMgr = new DBMgr(DBConn.GroupWare);
				dbMgr.Query = string.Format(query, 사원번호);

				dbMgr.ExecuteNonQuery();

				this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn사원등록GW.Text);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void BtnPDF압축_Click(object sender, EventArgs e)
		{
			//PDF.Optimizer(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "//WO21041980_K100.pdf", Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "//result.pdf");
		}

		private void Btn문자보내기_Click(object sender, EventArgs e)
		{
			try
			{
				return;

				MessagingLib.Messages messages = new MessagingLib.Messages();
				messages.Add(new MessagingLib.Message()
				{
					to = "01098098088",
					from = "01098098088",
					text = "문자 테스트"
				});

				MessagingLib.Response response = MessagingLib.SendMessages(messages);
				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					Console.WriteLine("전송 결과");
					Console.WriteLine("Group ID:" + response.Data.SelectToken("groupId").ToString());
					Console.WriteLine("Status:" + response.Data.SelectToken("status").ToString());
					Console.WriteLine("Count:" + response.Data.SelectToken("count").ToString());
				}
				else
				{
					Console.WriteLine("Error Code:" + response.ErrorCode);
					Console.WriteLine("Error Message:" + response.ErrorMessage);
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn워크파일다운로드_Click(object sender, EventArgs e)
        {
			string query;

			try
            {
				FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
				if (folderBrowserDialog.ShowDialog() != DialogResult.OK) return;

				query = @"EXEC PS_CZ_MA_WORKFLOW_FILE_REG_H_R2
 @CD_COMPANY='K200'
,@NO_FILE=NULL
,@CD_FILE=NULL
,@DT_F='20041104'
,@DT_T='20201204'
,@NO_EMP_SALE=NULL
,@NO_EMP_TYPE=NULL
,@CD_PARTNER=NULL
,@NO_IMO=NULL
,@CD_VENDOR='10377|'
,@TP_STEP='04'
,@NO_REF=NULL
,@CD_EXTENSION='01|04|05|'
,@YN_INCLUDED='Y'
,@YN_LIMIT=NULL
,@YN_DXREG=NULL";

				DataTable dt = DBHelper.GetDataTable(query);

				string localPath = folderBrowserDialog.SelectedPath;

				foreach (DataRow dr in dt.Rows)
				{
					DataTable dt1 = DBMgr.GetDataTable(@"SELECT ISNULL(QH.NO_IMO, 0000000) AS NO_IMO,
																WH.DTS_INSERT
														 FROM CZ_MA_WORKFLOWH WH WITH(NOLOCK)
														 LEFT JOIN CZ_SA_QTNH QH WITH(NOLOCK) ON QH.CD_COMPANY = WH.CD_COMPANY AND QH.NO_FILE = WH.NO_KEY
														 WHERE WH.CD_COMPANY = '" + dr["CD_COMPANY"].ToString() + "'" + Environment.NewLine +
														"AND WH.NO_KEY = '" + dr["NO_FILE"].ToString() + "'" + Environment.NewLine +
														"ORDER BY WH.TP_STEP");

					string yyyy = (dt1.Rows.Count > 0 ? dt1.Rows[0]["DTS_INSERT"].ToString().Substring(0, 4) : Util.GetToday().Substring(0, 4));

					DataTable dt2 = DBHelper.GetDataTable(@"SELECT WL.NM_FILE_REAL 
															FROM CZ_MA_WORKFLOWL WL WITH(NOLOCK) 
														    WHERE WL.CD_COMPANY = '" + dr["CD_COMPANY"].ToString() + "'" + Environment.NewLine +
														   "AND WL.NO_KEY = '" + dr["NO_FILE"].ToString() + "'" + Environment.NewLine +
														   "AND WL.TP_STEP NOT IN ('05', '02', '09', '10', '11', '57', '08', '50', '51', '53', '56')" + Environment.NewLine +
														   "AND RIGHT(WL.NM_FILE_REAL, 3) IN ('PDF', 'PEG', 'JPG', 'PNG', 'GIF', 'IFF', 'TIF', 'ZIP', '.7Z')" + Environment.NewLine +
														   "AND ISNULL(WL.NM_FILE_REAL, '') <> ''");

					int index = 1;

					foreach (DataRow dr1 in dt2.Rows)
					{
						string serverPath = "WorkFlow/" + dr["CD_COMPANY"].ToString() + "/" + yyyy + "/" + dr["NO_FILE"].ToString();

						string downFileName = dr1["NM_FILE_REAL"].ToString().Substring(dr1["NM_FILE_REAL"].ToString().LastIndexOf(@"\") + 1);

						FileInfo file = new FileInfo(localPath + @"\" + downFileName);

						string newFileName = dr["NO_FILE"].ToString() + "_" + dt1.Rows[0]["NO_IMO"].ToString() + "_" + index.ToString() + file.Extension;

						WebClient wc = new WebClient();

						try
						{
							wc.DownloadFile(Global.MainFrame.HostURL + "/" + serverPath + "/" + Uri.EscapeDataString(downFileName), localPath + @"\" + newFileName);
						}
						catch
						{
							continue;
						}

						index++;
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

        private void Btn정규식테스트_Click(object sender, EventArgs e)
        {
			DataTable dt, dt1;
			string query, query1, query2;
			List<string> plateNoList;

			try
            {
				query = @"SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT WH.CD_COMPANY, 
	   WH.NO_KEY,
	   QH1.NO_PREREG,
	   QH1.NO_IMO,
	   QH1.CD_PARTNER,
	   MC.CD_FLAG1 AS ID_EMP,
	   QH1.CLS_S,
	   QH1.CLS_L,
	   QH1.NM_ENGINE,
	   QH1.QT_CLS_S,
	   QH1.QT_ENGINE,
	   (CASE WHEN QH1.NO_FILE IS NOT NULL THEN 'Y' ELSE 'N' END) AS YN_AUTO  
FROM CZ_MA_WORKFLOWH WH
LEFT JOIN CZ_SA_QTNH QH ON QH.CD_COMPANY = WH.CD_COMPANY AND QH.NO_FILE = WH.NO_KEY
LEFT JOIN CZ_MA_CODEDTL MC ON MC.CD_COMPANY = WH.CD_COMPANY AND MC.CD_FIELD = 'CZ_MA00044' AND MC.CD_FLAG2 = WH.ID_SALES
LEFT JOIN (SELECT WL.CD_COMPANY,
				  WL.NO_KEY
		   FROM CZ_MA_WORKFLOWL WL
		   WHERE WL.TP_STEP = '01'
		   AND WL.YN_PARSING = 'Y'
		   AND (WL.YN_INCLUDED IS NULL OR WL.YN_INCLUDED = 'N')
		   GROUP BY WL.CD_COMPANY, WL.NO_KEY) WL
ON WL.CD_COMPANY = WH.CD_COMPANY AND WL.NO_KEY = WH.NO_KEY
LEFT JOIN (SELECT QH.CD_COMPANY,
			      QH.NO_PREREG,
				  QH.NO_FILE,
			      QH.CD_PARTNER,
				  QH.NO_IMO,
				  QH.NO_EMP,
				  MAX(MC.CD_FLAG2) AS CLS_S,
				  MAX(MC2.CD_FLAG1) AS CLS_L,
				  MAX(HE.NM_MODEL) AS NM_ENGINE,
				  COUNT(DISTINCT MC.CD_FLAG2) QT_CLS_S,
				  COUNT(DISTINCT HE.NM_MODEL) QT_ENGINE
		   FROM CZ_SA_QTN_PREREG_HEAD QH
		   JOIN CZ_SA_QTN_PREREG QP ON QP.CD_COMPANY = QH.CD_COMPANY AND QP.NO_PREREG = QH.NO_PREREG
		   LEFT JOIN CZ_MA_CODEDTL MC ON MC.CD_COMPANY = 'K100' AND MC.CD_FIELD = 'CZ_MA00045' AND (ISNULL(QP.NM_SUBJECT, '') + ' ' + ISNULL(QP.CD_ITEM_PARTNER, '') + ' ' + ISNULL(QP.NM_ITEM_PARTNER, '')) LIKE '%' + CD_FLAG1 + '%'
		   LEFT JOIN MA_CODEDTL MC1 ON MC1.CD_COMPANY = 'K100' AND MC1.CD_FIELD = 'MA_B000032' AND MC1.CD_SYSDEF = MC.CD_FLAG2
		   LEFT JOIN MA_CODEDTL MC2 ON MC2.CD_COMPANY = 'K100' AND MC2.CD_FIELD = 'MA_B000031' AND MC2.CD_SYSDEF = MC1.CD_FLAG1
		   LEFT JOIN CZ_MA_HULL_ENGINE HE ON HE.NO_IMO = QH.NO_IMO AND HE.CLS_S = MC.CD_FLAG2
		   WHERE EXISTS (SELECT 1 
					     FROM CZ_SA_QTN_PREREG_VENDOR QV 
					     WHERE QV.CD_COMPANY = QP.CD_COMPANY 
					     AND QV.NO_PREREG = QP.NO_PREREG 
					     AND QV.NO_LINE = QP.NO_LINE
					     AND QV.DXVENDOR_CODE = '11823')
		   GROUP BY QH.CD_COMPANY,
			        QH.NO_PREREG,
				    QH.NO_FILE,
			        QH.CD_PARTNER,
				    QH.NO_IMO,
				    QH.NO_EMP) QH1
ON QH1.CD_COMPANY = WH.CD_COMPANY AND QH1.NO_FILE = WH.NO_KEY
WHERE WH.CD_COMPANY = 'K100'
AND WH.TP_STEP = '02'
AND WH.NO_KEY = 'DB21011769'
--AND ISNULL(WH.YN_DONE, 'N') = 'N'
AND ISNULL(QH.YN_CLOSE, 'N') = 'N'
AND QH1.NO_FILE IS NOT NULL
--AND ((WH.DC_RMK LIKE '현대웹%' AND WL.NO_KEY IS NOT NULL) OR QH1.NO_FILE IS NOT NULL)";

				dt = DBHelper.GetDataTable(query);

				query1 = @"UPDATE CZ_SA_QTN_PREREG2
						   SET CLS_S = '{3}',
							   QT_CLS_S = '{4}',
							   NM_MODEL = '{5}',
							   QT_MODEL = '{6}',
							   CD_BEFORE = '{7}',
							   QT_CD_BEFORE = '{8}',
							   NO_PLATE = '{9}',
							   QT_PLATE = '{10}'
						   WHERE CD_COMPANY = '{0}'
						   AND NO_PREREG = '{1}'
						   AND NO_LINE = '{2}'";

				query2 = @"SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT B.NO_LINE,
       (CASE WHEN B.IDX = 1 THEN B.NO_PLATE ELSE B.NO_PLATE + ' ' + CONVERT(CHAR, B.IDX - 1) END) AS NO_PLATE,
	   B.DESCRIPTION,
	   B.NM_SUBJECT,
	   B.CD_ITEM_PARTNER,
	   B.NM_ITEM_PARTNER,
	   B.UNIT,
	   B.QT 
FROM (SELECT A.NO_LINE,
             SUBSTRING(A.NO_PLATE, 1, 60) AS NO_PLATE,
	         ROW_NUMBER() OVER (PARTITION BY A.NO_PLATE ORDER BY A.NO_PLATE) AS IDX,
	         A.DESCRIPTION,
	         A.NM_SUBJECT,
			 A.CD_ITEM_PARTNER,
			 A.NM_ITEM_PARTNER,
	         A.UNIT,
	         A.QT
FROM (SELECT QP.NO_LINE,
			 (CASE WHEN ISNULL(QP.CD_ITEM_PARTNER, '') <> '' THEN REPLACE(QP.CD_ITEM_PARTNER, CHAR(10), ' ')
				   WHEN ISNULL(QP.NM_ITEM_PARTNER, '') <> '' THEN REPLACE(QP.NM_ITEM_PARTNER, CHAR(10), ' ')
															 ELSE REPLACE(QP.NM_SUBJECT, CHAR(10), ' ') END) AS NO_PLATE,
			 (CASE WHEN ISNULL(QP.NM_ITEM_PARTNER, '') <> '' THEN REPLACE(QP.NM_ITEM_PARTNER, CHAR(10), ' ')
				   WHEN ISNULL(QP.CD_ITEM_PARTNER, '') <> '' THEN REPLACE(QP.CD_ITEM_PARTNER, CHAR(10), ' ')
														     ELSE REPLACE(QP.NM_SUBJECT, CHAR(10), ' ') END) AS DESCRIPTION,
		     ISNULL(QP.NM_SUBJECT, '') AS NM_SUBJECT,
			 ISNULL(QP.CD_ITEM_PARTNER, '') AS CD_ITEM_PARTNER,
			 ISNULL(QP.NM_ITEM_PARTNER, '') AS NM_ITEM_PARTNER,
	         QP.UNIT,
	         CONVERT(INT, QP.QT) AS QT
	  FROM CZ_SA_QTN_PREREG QP
	  WHERE QP.CD_COMPANY = 'K100'
	  AND QP.NO_PREREG = '{0}'
	  AND QP.NM_ITEM_PARTNER <> 'PACKING AND HANDLING') A) B
ORDER BY B.NO_LINE ASC";

				foreach (DataRow dr in dt.Rows)
                {
					dt1 = DBHelper.GetDataTable(string.Format(query2, dr["NO_PREREG"].ToString()));

					foreach(DataRow dr1 in dt1.Rows)
                    {
						this.GetPlateNo(dr["NO_IMO"].ToString(), dr["CLS_S"].ToString(), dr1, out plateNoList);

						if (plateNoList.Count != 1)
                        {

                        }

						//DBHelper.ExecuteScalar(string.Format(query1, "K100", 
						//											 dr["NO_PREREG"].ToString(), 
						//											 dr1["NO_LINE"].ToString(), 
						//											 dr["CLS_S"].ToString(),
						//											 dr["QT_CLS_S"].ToString(),
						//											 dr["NM_ENGINE"].ToString(),
						//											 dr["QT_ENGINE"].ToString(),
						//											 (codeList.Count > 0 ? codeList[0] : string.Empty),
						//											 codeList.Count.ToString(),
						//											 (plateNoList.Count > 0 ? plateNoList[0] : string.Empty), 
						//											 plateNoList.Count.ToString()));
					}
				}
            }
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void GetPlateNo(string imo, string engine, DataRow drItem, out List<string> plateNoList)
        {
			plateNoList = new List<string>();

			try
            {
				string query = @"SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

								 SELECT EI.NO_PLATE_ORG AS NO_PLATE,
								 	    EI.NM_PLATE,
								 	    (CASE WHEN PATINDEX('%[^0-9A-Z]%', EI.NO_PLATE_ORG) = 0 THEN EI.NO_PLATE_ORG 
								 														        ELSE SUBSTRING(EI.NO_PLATE_ORG, 1, PATINDEX('%[^0-9A-Z]%', EI.NO_PLATE_ORG)-1) END) AS BEFORE_CODE,
								        (CASE WHEN CHARINDEX(' ', EI.NM_PLATE, CHARINDEX(' ', EI.NM_PLATE) + 1) = 0 THEN EI.NM_PLATE 
								 																				    ELSE SUBSTRING(EI.NM_PLATE, 1, CHARINDEX(' ', EI.NM_PLATE, CHARINDEX(' ', EI.NM_PLATE) + 1)-1) END) AS BEFORE_NAME
								 FROM CZ_MA_HULL_ENGINE HE
								 JOIN CZ_MA_HULL_ENGINE_ITEM EI ON EI.NO_IMO = HE.NO_IMO AND EI.NO_ENGINE = HE.NO_ENGINE
								 WHERE HE.NO_IMO = '{0}'
								 AND HE.CLS_S = '{1}'
								 AND EI.YN_UPLOAD = 'Y'";


				DataTable dt = DBHelper.GetDataTable(string.Format(query, imo, engine));

				DataTable dt1 = dt.Copy();
				dt1.Columns.Remove("NO_PLATE");
				dt1.Columns.Remove("NM_PLATE");
				dt1.Columns.Remove("BEFORE_NAME");
				dt1 = dt1.DefaultView.ToTable(true, new string[] { "BEFORE_CODE" });

				DataTable dt2 = dt.Copy();
				dt2.Columns.Remove("NM_PLATE");
				dt2.Columns.Remove("BEFORE_CODE");
				dt2.Columns.Remove("BEFORE_NAME");
				dt2 = dt2.DefaultView.ToTable(true, new string[] { "NO_PLATE" });

				#region 기부속찾기
				string tmpText = (drItem["NM_SUBJECT"].ToString() + " " + drItem["CD_ITEM_PARTNER"].ToString() + " " + drItem["NM_ITEM_PARTNER"].ToString()).ToUpper().Replace("\n", " ");
				tmpText = Regex.Replace(tmpText, "(^|[^a-zA-Z0-9]{1,})[0-9]{4}-{1}[0-9]{2}-{1}[0-9]{2}([^a-zA-Z0-9]{1,}|$)", string.Empty);
				tmpText = Regex.Replace(tmpText, "[^0-9A-Z]", " ");

				List<string> keywordList = tmpText.Split(" ".ToCharArray())
												  .Where(x => !string.IsNullOrEmpty(x))
												  .Where(x => Regex.IsMatch(x, "[0-9]"))
												  .Distinct()
												  .ToList<string>();

				List<string> keywordList1 = tmpText.Split(" ".ToCharArray())
												   .Where(x => !string.IsNullOrEmpty(x))
												   .ToList<string>();

				keywordList = keywordList.Where(x => dt2.AsEnumerable()
														.Where(x1 => Regex.IsMatch(Regex.Replace(x1["NO_PLATE"].ToString(), "[A-Z]", string.Empty),
																				   "(^[A-Z0]{0,}|[^0-9A-Z]{1,1}0{0,})" + (Regex.Replace(x, "[A-Z]", string.Empty).Length > 0 && Regex.Replace(x, "[A-Z]", string.Empty).Length <= 9 ? Convert.ToInt32(Regex.Replace(x, "[A-Z]", string.Empty)).ToString() : x) + "($|[^0-9A-Z]{1,1})"))
														.Count() > 0).ToList<string>();
				#region 앞자리 4자리 이상

				#region 시작 코드 찾기
				List<string> codeList = keywordList.Where(x => dt1.Select(string.Format("LEN(BEFORE_CODE) >= 4 AND BEFORE_CODE = '{0}'", x)).Length > 0).ToList<string>();
				List<string> tempList = keywordList.Where(x => dt1.AsEnumerable().Where(x1 => x1["BEFORE_CODE"].ToString().Length >= 4 &&
																							  Regex.Replace(x1["BEFORE_CODE"].ToString(), "[A-Z]", string.Empty) == Regex.Replace(x, "[A-Z]", string.Empty)).Count() > 0).ToList<string>();

				foreach (string code in tempList)
				{
					if (Regex.IsMatch(code, "^[0-9]*$"))
					{
						if (dt1.AsEnumerable().Where(x => x["BEFORE_CODE"].ToString().Length >= 4 &&
														  x["BEFORE_CODE"].ToString() == "P" + code).Count() > 0 &&
							!codeList.Contains("P" + code))
						{
							codeList.Add("P" + code);
						}

						if (dt1.AsEnumerable().Where(x => x["BEFORE_CODE"].ToString().Length >= 4 &&
														  x["BEFORE_CODE"].ToString() == "A" + code).Count() > 0 &&
							!codeList.Contains("A" + code))
						{
							codeList.Add("A" + code);
						}

						for (int i = 0; i < keywordList1.Count; i++)
						{
							if (keywordList1[i] == code)
							{
								if (i - 1 >= 0 &&
									dt1.AsEnumerable().Where(x => x["BEFORE_CODE"].ToString().Length >= 4 &&
																  x["BEFORE_CODE"].ToString() == keywordList1[i - 1] + keywordList1[i]).Count() > 0 &&
									!codeList.Contains(keywordList1[i - 1] + keywordList1[i]))
								{
									codeList.Add(keywordList1[i - 1] + keywordList1[i]);
								}
								else if (i + 1 < keywordList1.Count &&
										 dt1.AsEnumerable().Where(x => x["BEFORE_CODE"].ToString().Length >= 4 &&
																	   x["BEFORE_CODE"].ToString() == keywordList1[i] + keywordList1[i + 1]).Count() > 0 &&
										 !codeList.Contains(keywordList1[i] + keywordList1[i + 1]))
								{
									codeList.Add(keywordList1[i] + keywordList1[i + 1]);
								}
							}
						}
					}
					else
					{
						if (code.StartsWith("P") &&
							dt1.AsEnumerable().Where(x => x["BEFORE_CODE"].ToString().Length >= 4 &&
														  x["BEFORE_CODE"].ToString() == code.Substring(1, code.Length - 1)).Count() > 0 &&
							!codeList.Contains(code.Substring(1, code.Length - 1)))
						{
							codeList.Add(code.Substring(1, code.Length - 1));
						}
					}
				}
				#endregion

				int index;
				bool isFinded;

				foreach (string code in codeList)
				{
					#region 2번째 자리
					index = keywordList.IndexOf(code);

					if (index == -1)
						index = keywordList.IndexOf(Regex.Replace(code, "[A-Z]", string.Empty));

					string regex = "^" + code;
					List<string> dataList = dt2.AsEnumerable().Where(x => Regex.IsMatch(x["NO_PLATE"].ToString(), regex + "([^0-9A-Z]{1,1}|$)")).Select(x => x["NO_PLATE"].ToString()).ToList<string>();
					List<string> codeList1 = keywordList.Where(x => x != code &&
																	keywordList.IndexOf(x) > index &&
																	dataList.Where(x1 => Regex.IsMatch(x1.ToString(), regex + "[^0-9A-Z]{1,1}" + x + "([^0-9A-Z]{1,1}|$)")).Count() > 0)
														.OrderBy(x => keywordList.IndexOf(x))
														.ToList<string>();
					isFinded = false;

					if (codeList1.Count > 0)
					{
						regex += "[^0-9A-Z]{1,1}";
						isFinded = true;
					}

					if (plateNoList.Count == 0)
					{
						if (isFinded == false)
						{
							codeList1 = keywordList.Where(x => x != code &&
																   keywordList.IndexOf(x) < index &&
																   dataList.Where(x1 => Regex.IsMatch(x1.ToString(), regex + "[^0-9A-Z]{1,1}" + x + "([^0-9A-Z]{1,1}|$)")).Count() > 0)
													   .OrderByDescending(x => keywordList.IndexOf(x))
													   .ToList<string>();

							if (codeList1.Count > 0)
							{
								regex += "[^0-9A-Z]{1,1}";
								isFinded = true;
							}
						}

						if (isFinded == false)
						{
							codeList1 = keywordList.Where(x => x != code &&
																	   keywordList.IndexOf(x) > index)
														   .Select(x => Regex.IsMatch(x, "^[0-9]*$") && x.Length <= 9 ? Convert.ToInt32(x).ToString() : x)
														   .Where(x => dataList.Where(x1 => Regex.IsMatch(x1.ToString(), regex + "[^0-9A-Z]{1,1}0{0,}" + x + "([^0-9A-Z]{1,1}|$)")).Count() > 0)
														   .OrderBy(x => keywordList.IndexOf(x))
														   .ToList<string>();

							if (codeList1.Count > 0)
							{
								regex += "[^0-9A-Z]{1,1}0{0,}";
								isFinded = true;
							}
						}

						if (isFinded == false)
						{
							codeList1 = keywordList.Where(x => x != code &&
																		   keywordList.IndexOf(x) < index)
															   .Select(x => Regex.IsMatch(x, "^[0-9]*$") && x.Length <= 9 ? Convert.ToInt32(x).ToString() : x)
															   .Where(x => dataList.Where(x1 => Regex.IsMatch(x1.ToString(), regex + "[^0-9A-Z]{1,1}0{0,}" + x + "([^0-9A-Z]{1,1}|$)")).Count() > 0)
															   .OrderByDescending(x => keywordList.IndexOf(x))
															   .ToList<string>();

							if (codeList1.Count > 0)
							{
								regex += "[^0-9A-Z]{1,1}0{0,}";
								isFinded = true;
							}
						}

						if (isFinded == false)
						{
							List<string> tmpList = keywordList.Where(x => x != code &&
																						  keywordList.IndexOf(x) > index)
																			  .OrderBy(x => keywordList.IndexOf(x))
																			  .ToList<string>();

							foreach (string keyword in tmpList)
							{
								if (Regex.IsMatch(keyword, "^[0-9]*$") &&
									dataList.Where(x1 => Regex.IsMatch(x1.ToString(), regex + "[^0-9A-Z]{1,1}[0-9A-Z]{0,}" + keyword + "[0-9A-Z]{0,}([^0-9A-Z]{1,1}|$)")).Count() > 0)
								{
									if (dataList.Where(x => Regex.IsMatch(x.ToString(), regex + "[^0-9A-Z]{1,1}" + keyword + "E" + "([^0-9A-Z]{1,1}|$)")).Count() > 0 &&
														!codeList1.Contains(keyword + "E"))
										codeList1.Add(keyword + "E");
									else
									{
										for (int i = 0; i < keywordList1.Count; i++)
										{
											if (keywordList1[i] == keyword)
											{
												if (i - 1 >= 0 &&
													dataList.Where(x => Regex.IsMatch(x.ToString(), regex + "[^0-9A-Z]{1,1}" + keywordList1[i - 1] + keyword + "([^0-9A-Z]{1,1}|$)")).Count() > 0 &&
													!codeList1.Contains(keywordList1[i - 1] + keyword))
													codeList1.Add(keywordList1[i - 1] + keyword);
												else if (i + 1 < keywordList1.Count &&
														 dataList.Where(x => Regex.IsMatch(x.ToString(), regex + "[^0-9A-Z]{1,1}" + keyword + keywordList1[i + 1] + "([^0-9A-Z]{1,1}|$)")).Count() > 0 &&
														 !codeList1.Contains(keyword + keywordList1[i + 1]))
													codeList1.Add(keyword + keywordList1[i + 1]);
											}
										}
									}
								}
								else
								{
									codeList1.AddRange(dataList.Select(x => Regex.Replace(Regex.Replace(x, regex + "[^0-9A-Z]{1,1}", string.Empty), "[^0-9A-Z]", " ").Split(" ".ToCharArray())[0])
															   .Where(x => keyword.Length >= x.Length &&
																		   x == keyword.Substring(0, x.Length))
															   .ToList<string>());
								}
							}

							if (codeList1.Count > 0)
							{
								regex += "[^0-9A-Z]{1,1}";
								isFinded = true;
							}
						}

						if (isFinded == false)
						{
							List<string> tmpList = keywordList.Where(x => x != code &&
																		  keywordList.IndexOf(x) < index)
															  .OrderByDescending(x => keywordList.IndexOf(x))
															  .ToList<string>();

							foreach (string keyword in tmpList)
							{
								if (Regex.IsMatch(keyword, "^[0-9]*$") &&
									dataList.Where(x1 => Regex.IsMatch(x1.ToString(), regex + "[^0-9A-Z]{1,1}[0-9A-Z]{0,}" + keyword + "[0-9A-Z]{0,}([^0-9A-Z]{1,1}|$)")).Count() > 0)
								{
									if (dataList.Where(x => Regex.IsMatch(x.ToString(), regex + "[^0-9A-Z]{1,1}" + keyword + "E" + "([^0-9A-Z]{1,1}|$)")).Count() > 0 &&
														!codeList1.Contains(keyword + "E"))
										codeList1.Add(keyword + "E");
									else
									{
										for (int i = 0; i < keywordList1.Count; i++)
										{
											if (keywordList1[i] == keyword)
											{
												if (i - 1 >= 0 &&
													dataList.Where(x => Regex.IsMatch(x.ToString(), regex + "[^0-9A-Z]{1,1}" + keywordList1[i - 1] + keyword + "([^0-9A-Z]{1,1}|$)")).Count() > 0 &&
													!codeList1.Contains(keywordList1[i - 1] + keyword))
													codeList1.Add(keywordList1[i - 1] + keyword);
												else if (i + 1 < keywordList1.Count &&
														 dataList.Where(x => Regex.IsMatch(x.ToString(), regex + "[^0-9A-Z]{1,1}" + keyword + keywordList1[i + 1] + "([^0-9A-Z]{1,1}|$)")).Count() > 0 &&
														 !codeList1.Contains(keyword + keywordList1[i + 1]))
													codeList1.Add(keyword + keywordList1[i + 1]);
											}
										}
									}
								}
								else
								{
									codeList1.AddRange(dataList.Select(x => Regex.Replace(Regex.Replace(x, regex + "[^0-9A-Z]{1,1}", string.Empty), "[^0-9A-Z]", " ").Split(" ".ToCharArray())[0])
															   .Where(x => keyword.Length >= x.Length &&
																		   x == keyword.Substring(0, x.Length))
															   .ToList<string>());
								}
							}

							if (codeList1.Count > 0)
							{
								regex += "[^0-9A-Z]{1,1}";
								isFinded = true;
							}
						}
					}
					#endregion

					foreach (string plateNo in dataList.Where(x => Regex.IsMatch(x.ToString(), "^" + code + "(\\({1,}|$)")))
					{
						if (!plateNoList.Contains(plateNo))
							plateNoList.Add(plateNo);
					}

					foreach (string code1 in codeList1)
					{
						#region 3번째 자리
						index = keywordList.IndexOf(code1);

						if (index == -1)
							index = keywordList.IndexOf(Regex.Replace(code1, "[A-Z]", string.Empty));

						string regex1 = regex + code1;

						List<string> dataList1 = dataList.Where(x => Regex.IsMatch(x.ToString(), regex1 + "([^0-9A-Z]{1,1}|$)")).ToList<string>();
						List<string> codeList2 = keywordList.Where(x => x != code &&
																		x != code1 &&
																		keywordList.IndexOf(x) > index &&
																		dataList1.Where(x1 => Regex.IsMatch(x1.ToString(), regex1 + "[^0-9A-Z]{1,1}" + x + "([^0-9A-Z]{1,1}|$)")).Count() > 0)
															.OrderBy(x => keywordList.IndexOf(x))
															.ToList<string>();

						isFinded = false;

						if (codeList2.Count > 0)
						{
							regex1 += "[^0-9A-Z]{1,1}";
							isFinded = true;
						}

						if (plateNoList.Count == 0)
						{
							if (isFinded == false)
							{
								codeList2 = keywordList.Where(x => x != code &&
																   x != code1 &&
																   keywordList.IndexOf(x) < index &&
																   dataList1.Where(x1 => Regex.IsMatch(x1.ToString(), regex1 + "[^0-9A-Z]{1,1}" + x + "([^0-9A-Z]{1,1}|$)")).Count() > 0)
													   .OrderByDescending(x => keywordList.IndexOf(x))
													   .ToList<string>();

								if (codeList2.Count > 0)
								{
									regex1 += "[^0-9A-Z]{1,1}";
									isFinded = true;
								}
							}

							if (isFinded == false)
							{
								codeList2 = keywordList.Where(x => x != code &&
																	   x != code1 &&
																	   keywordList.IndexOf(x) > index)
														   .Select(x => Regex.IsMatch(x, "^[0-9]*$") && x.Length <= 9 ? Convert.ToInt32(x).ToString() : x)
														   .Where(x => dataList1.Where(x1 => Regex.IsMatch(x1.ToString(), regex1 + "[^0-9A-Z]{1,1}0{0,}" + x + "([^0-9A-Z]{1,1}|$)")).Count() > 0)
														   .OrderBy(x => keywordList.IndexOf(x))
														   .ToList<string>();

								if (codeList2.Count > 0)
								{
									regex1 += "[^0-9A-Z]{1,1}0{0,}";
									isFinded = true;
								}

							}

							if (isFinded == false)
							{
								codeList2 = keywordList.Where(x => x != code &&
																		   x != code1 &&
																		   keywordList.IndexOf(x) < index)
															   .Select(x => Regex.IsMatch(x, "^[0-9]*$") && x.Length <= 9 ? Convert.ToInt32(x).ToString() : x)
															   .Where(x => dataList1.Where(x1 => Regex.IsMatch(x1.ToString(), regex1 + "[^0-9A-Z]{1,1}0{0,}" + x + "([^0-9A-Z]{1,1}|$)")).Count() > 0)
															   .OrderByDescending(x => keywordList.IndexOf(x))
															   .ToList<string>();

								if (codeList2.Count > 0)
								{
									regex1 += "[^0-9A-Z]{1,1}0{0,}";
									isFinded = true;
								}
							}

							if (isFinded == false)
							{
								List<string> tmpList = keywordList.Where(x => x != code &&
																			  x != code1 &&
																			  keywordList.IndexOf(x) > index &&
																			  Regex.IsMatch(x, "^[0-9]*$") &&
																			  dataList1.Where(x1 => Regex.IsMatch(x1.ToString(), regex1 + "[^0-9A-Z]{1,1}[0-9A-Z]{0,}" + x + "[0-9A-Z]{0,}([^0-9A-Z]{1,1}|$)")).Count() > 0)
																  .OrderBy(x => keywordList.IndexOf(x))
																  .ToList<string>();

								foreach (string keyword in tmpList)
								{
									for (int i = 0; i < keywordList1.Count; i++)
									{
										if (keywordList1[i] == keyword)
										{
											if (i - 1 >= 0 &&
												dataList1.Where(x => Regex.IsMatch(x.ToString(), regex1 + "[^0-9A-Z]{1,1}" + keywordList1[i - 1] + keyword + "([^0-9A-Z]{1,1}|$)")).Count() > 0 &&
												!codeList2.Contains(keywordList1[i - 1] + keyword))
												codeList2.Add(keywordList1[i - 1] + keyword);
											else if (i + 1 < keywordList1.Count &&
													 dataList1.Where(x => Regex.IsMatch(x.ToString(), regex1 + "[^0-9A-Z]{1,1}" + keyword + keywordList1[i + 1] + "([^0-9A-Z]{1,1}|$)")).Count() > 0 &&
													 !codeList2.Contains(keyword + keywordList1[i + 1]))
												codeList2.Add(keyword + keywordList1[i + 1]);
										}
									}
								}

								if (codeList2.Count > 0)
								{
									regex1 += "[^0-9A-Z]{1,1}";
									isFinded = true;
								}
							}

							if (isFinded == false)
							{
								List<string> tmpList = keywordList.Where(x => x != code &&
																			  x != code1 &&
																			  keywordList.IndexOf(x) < index &&
																			  Regex.IsMatch(x, "^[0-9]*$") &&
																			  dataList1.Where(x1 => Regex.IsMatch(x1.ToString(), regex1 + "[^0-9A-Z]{1,1}[0-9A-Z]{0,}" + x + "[0-9A-Z]{0,}([^0-9A-Z]{1,1}|$)")).Count() > 0)
																  .OrderByDescending(x => keywordList.IndexOf(x))
																  .ToList<string>();

								foreach (string keyword in tmpList)
								{
									for (int i = 0; i < keywordList1.Count; i++)
									{
										if (keywordList1[i] == keyword)
										{
											if (i - 1 >= 0 &&
												dataList1.Where(x => Regex.IsMatch(x.ToString(), regex1 + "[^0-9A-Z]{1,1}" + keywordList1[i - 1] + keyword + "([^0-9A-Z]{1,1}|$)")).Count() > 0 &&
												!codeList2.Contains(keywordList1[i - 1] + keyword))
												codeList2.Add(keywordList1[i - 1] + keyword);
											else if (i + 1 < keywordList1.Count &&
													 dataList1.Where(x => Regex.IsMatch(x.ToString(), regex1 + "[^0-9A-Z]{1,1}" + keyword + keywordList1[i + 1] + "([^0-9A-Z]{1,1}|$)")).Count() > 0 &&
													 !codeList2.Contains(keyword + keywordList1[i + 1]))
												codeList2.Add(keyword + keywordList1[i + 1]);
										}
									}
								}

								if (codeList2.Count > 0)
								{
									regex1 += "[^0-9A-Z]{1,1}";
									isFinded = true;
								}
							}
						}
						#endregion

						foreach (string plateNo in dataList1.Where(x => Regex.IsMatch(x.ToString(), regex + code1 + "(\\({1,}|\\/{1,}|$)")))
						{
							if (!plateNoList.Contains(plateNo))
								plateNoList.Add(plateNo);
						}

						foreach (string code2 in codeList2)
						{
							#region 4번째 자리
							index = keywordList.IndexOf(code2);

							if (index == -1)
								index = keywordList.IndexOf(Regex.Replace(code2, "[A-Z]", string.Empty));

							string regex2 = regex1 + code2;
							List<string> dataList2 = dataList1.Where(x => Regex.IsMatch(x.ToString(), regex2 + "([^0-9A-Z]{1,1}|$)")).ToList<string>();
							List<string> codeList3 = keywordList.Where(x => x != code &&
																			x != code1 &&
																			x != code2 &&
																			keywordList.IndexOf(x) > index &&
																			dataList2.Where(x1 => Regex.IsMatch(x1.ToString(), regex2 + "[^0-9A-Z]{1,1}" + x + "([^0-9A-Z]{1,1}|$)")).Count() > 0)
																.OrderBy(x => keywordList.IndexOf(x))
																.ToList<string>();

							isFinded = false;

							if (codeList3.Count > 0)
							{
								regex2 += "[^0-9A-Z]{1,1}";
								isFinded = true;
							}

							if (plateNoList.Count == 0)
                            {
								if (isFinded == false)
								{
									codeList3 = keywordList.Where(x => x != code &&
																	   x != code1 &&
																	   x != code2 &&
																	   keywordList.IndexOf(x) < index &&
																	   dataList2.Where(x1 => Regex.IsMatch(x1.ToString(), regex2 + "[^0-9A-Z]{1,1}" + x + "([^0-9A-Z]{1,1}|$)")).Count() > 0)
														   .OrderByDescending(x => keywordList.IndexOf(x))
														   .ToList<string>();

									if (codeList3.Count > 0)
									{
										regex2 += "[^0-9A-Z]{1,1}";
										isFinded = true;
									}
								}

								if (isFinded == false)
								{
									codeList3 = keywordList.Where(x => x != code &&
																	   x != code1 &&
																	   x != code2 &&
																	   keywordList.IndexOf(x) > index)
														   .Select(x => Regex.IsMatch(x, "^[0-9]*$") && x.Length <= 9 ? Convert.ToInt32(x).ToString() : x)
														   .Where(x => dataList2.Where(x1 => Regex.IsMatch(x1.ToString(), regex2 + "[^0-9A-Z]{1,1}0{0,}" + x + "([^0-9A-Z]{1,1}|$)")).Count() > 0)
														   .OrderBy(x => keywordList.IndexOf(x))
														   .ToList<string>();

									if (codeList3.Count > 0)
									{
										regex2 += "[^0-9A-Z]{1,1}0{0,}";
										isFinded = true;
									}
								}

								if (isFinded == false)
								{
									codeList3 = keywordList.Where(x => x != code &&
																	   x != code1 &&
																	   x != code2 &&
																	   keywordList.IndexOf(x) < index)
														   .Select(x => Regex.IsMatch(x, "^[0-9]*$") && x.Length <= 9 ? Convert.ToInt32(x).ToString() : x)
														   .Where(x => dataList2.Where(x1 => Regex.IsMatch(x1.ToString(), regex2 + "[^0-9A-Z]{1,1}0{0,}" + x + "([^0-9A-Z]{1,1}|$)")).Count() > 0)
														   .OrderByDescending(x => keywordList.IndexOf(x))
														   .ToList<string>();

									if (codeList3.Count > 0)
									{
										regex2 += "[^0-9A-Z]{1,1}0{0,}";
										isFinded = true;
									}
								}

								if (isFinded == false)
								{
									List<string> tmpList = keywordList.Where(x => x != code &&
																				  x != code1 &&
																				  x != code2 &&
																				  keywordList.IndexOf(x) > index &&
																				  Regex.IsMatch(x, "^[0-9]*$") &&
																				  dataList2.Where(x1 => Regex.IsMatch(x1.ToString(), regex2 + "[^0-9A-Z]{1,1}[0-9A-Z]{0,}" + x + "[0-9A-Z]{0,}([^0-9A-Z]{1,1}|$)")).Count() > 0)
																	  .OrderBy(x => keywordList.IndexOf(x))
																	  .ToList<string>();

									foreach (string keyword in tmpList)
									{
										for (int i = 0; i < keywordList1.Count; i++)
										{
											if (keywordList1[i] == keyword)
											{
												if (i - 1 >= 0 &&
													dataList2.Where(x => Regex.IsMatch(x.ToString(), regex2 + "[^0-9A-Z]{1,1}" + keywordList1[i - 1] + keyword + "([^0-9A-Z]{1,1}|$)")).Count() > 0 &&
													!codeList3.Contains(keywordList1[i - 1] + keyword))
													codeList3.Add(keywordList1[i - 1] + keyword);
												else if (i + 1 < keywordList1.Count &&
														 dataList2.Where(x => Regex.IsMatch(x.ToString(), regex2 + "[^0-9A-Z]{1,1}" + keyword + keywordList1[i + 1] + "([^0-9A-Z]{1,1}|$)")).Count() > 0 &&
														 !codeList3.Contains(keyword + keywordList1[i + 1]))
													codeList3.Add(keyword + keywordList1[i + 1]);
											}
										}
									}

									if (codeList3.Count > 0)
									{
										regex2 += "[^0-9A-Z]{1,1}";
										isFinded = true;
									}
								}

								if (isFinded == false)
								{
									List<string> tmpList = keywordList.Where(x => x != code &&
																					x != code1 &&
																					x != code2 &&
																					keywordList.IndexOf(x) < index &&
																					Regex.IsMatch(x, "^[0-9]*$") &&
																					dataList2.Where(x1 => Regex.IsMatch(x1.ToString(), regex2 + "[^0-9A-Z]{1,1}[0-9A-Z]{0,}" + x + "[0-9A-Z]{0,}([^0-9A-Z]{1,1}|$)")).Count() > 0)
																	  .OrderByDescending(x => keywordList.IndexOf(x))
																	  .ToList<string>();

									foreach (string keyword in tmpList)
									{
										for (int i = 0; i < keywordList1.Count; i++)
										{
											if (keywordList1[i] == keyword)
											{
												if (i - 1 >= 0 &&
													dataList2.Where(x => Regex.IsMatch(x.ToString(), regex2 + "[^0-9A-Z]{1,1}" + keywordList1[i - 1] + keyword + "([^0-9A-Z]{1,1}|$)")).Count() > 0 &&
													!codeList3.Contains(keywordList1[i - 1] + keyword))
													codeList3.Add(keywordList1[i - 1] + keyword);
												else if (i + 1 < keywordList1.Count &&
														 dataList2.Where(x => Regex.IsMatch(x.ToString(), regex2 + "[^0-9A-Z]{1,1}" + keyword + keywordList1[i + 1] + "([^0-9A-Z]{1,1}|$)")).Count() > 0 &&
														 !codeList3.Contains(keyword + keywordList1[i + 1]))
													codeList3.Add(keyword + keywordList1[i + 1]);
											}
										}
									}

									if (codeList3.Count > 0)
									{
										regex2 += "[^0-9A-Z]{1,1}";
										isFinded = true;
									}
								}
							}
							#endregion

							foreach (string plateNo in dataList2.Where(x => Regex.IsMatch(x.ToString(), regex1 + code2 + "$")))
							{
								if (!plateNoList.Contains(plateNo))
									plateNoList.Add(plateNo);
							}

							foreach (string code3 in codeList3)
							{
								#region 5번째 자리
								index = keywordList.IndexOf(code3);

								if (index == -1)
									index = keywordList.IndexOf(Regex.Replace(code3, "[A-Z]", string.Empty));

								string regex3 = regex2 + code3;

								List<string> dataList3 = dataList2.Where(x => Regex.IsMatch(x.ToString(), regex3 + "([^0-9A-Z]{1,1}|$)")).ToList<string>();
								List<string> codeList4 = keywordList.Where(x => x != code &&
																				x != code1 &&
																				x != code2 &&
																				x != code3 &&
																				keywordList.IndexOf(x) > index &&
																				dataList3.Where(x1 => Regex.IsMatch(x1.ToString(), regex3 + "[^0-9A-Z]{1,1}" + x + "([^0-9A-Z]{1,1}|$)")).Count() > 0)
																	.OrderBy(x => keywordList.IndexOf(x))
																	.ToList<string>();

								isFinded = false;

								if (codeList4.Count > 0)
								{
									regex3 += "[^0-9A-Z]{1,1}";
									isFinded = true;
								}

								if (plateNoList.Count == 0)
                                {
									if (isFinded == false)
									{
										codeList4 = keywordList.Where(x => x != code &&
																		   x != code1 &&
																		   x != code2 &&
																		   x != code3 &&
																		   keywordList.IndexOf(x) < index &&
																		   dataList3.Where(x1 => Regex.IsMatch(x1.ToString(), regex3 + "[^0-9A-Z]{1,1}" + x + "([^0-9A-Z]{1,1}|$)")).Count() > 0)
															   .OrderByDescending(x => keywordList.IndexOf(x))
															   .ToList<string>();

										if (codeList4.Count > 0)
										{
											regex3 += "[^0-9A-Z]{1,1}";
											isFinded = true;
										}
									}

									if (isFinded == false)
									{
										codeList4 = keywordList.Where(x => x != code &&
																		   x != code1 &&
																		   x != code2 &&
																		   x != code3 &&
																		   keywordList.IndexOf(x) > index)
															   .Select(x => Regex.IsMatch(x, "^[0-9]*$") && x.Length <= 9 ? Convert.ToInt32(x).ToString() : x)
															   .Where(x => dataList3.Where(x1 => Regex.IsMatch(x1.ToString(), regex3 + "[^0-9A-Z]{1,1}0{0,}" + x + "([^0-9A-Z]{1,1}|$)")).Count() > 0)
															   .OrderBy(x => keywordList.IndexOf(x))
															   .ToList<string>();

										if (codeList4.Count > 0)
										{
											regex3 += "[^0-9A-Z]{1,1}0{0,}";
											isFinded = true;
										}
									}

									if (isFinded == false)
									{
										codeList4 = keywordList.Where(x => x != code &&
																		   x != code1 &&
																		   x != code2 &&
																		   x != code3 &&
																		   keywordList.IndexOf(x) < index)
															   .Select(x => Regex.IsMatch(x, "^[0-9]*$") && x.Length <= 9 ? Convert.ToInt32(x).ToString() : x)
															   .Where(x => dataList3.Where(x1 => Regex.IsMatch(x1.ToString(), regex3 + "[^0-9A-Z]{1,1}0{0,}" + x + "([^0-9A-Z]{1,1}|$)")).Count() > 0)
															   .OrderByDescending(x => keywordList.IndexOf(x))
															   .ToList<string>();

										if (codeList4.Count > 0)
										{
											regex3 += "[^0-9A-Z]{1,1}0{0,}";
											isFinded = true;
										}
									}

									if (isFinded == false)
									{
										List<string> tmpList = keywordList.Where(x => x != code &&
																					  x != code1 &&
																					  x != code2 &&
																					  x != code3 &&
																					  keywordList.IndexOf(x) > index &&
																					  Regex.IsMatch(x, "^[0-9]*$") &&
																					  dataList3.Where(x1 => Regex.IsMatch(x1.ToString(), regex3 + "[^0-9A-Z]{1,1}[0-9A-Z]{0,}" + x + "[0-9A-Z]{0,}([^0-9A-Z]{1,1}|$)")).Count() > 0)
																		  .OrderBy(x => keywordList.IndexOf(x))
																		  .ToList<string>();

										foreach (string keyword in tmpList)
										{
											for (int i = 0; i < keywordList1.Count; i++)
											{
												if (keywordList1[i] == keyword)
												{
													if (i - 1 >= 0 &&
														dataList3.Where(x => Regex.IsMatch(x.ToString(), regex3 + "[^0-9A-Z]{1,1}" + keywordList1[i - 1] + keyword + "([^0-9A-Z]{1,1}|$)")).Count() > 0 &&
														!codeList4.Contains(keywordList1[i - 1] + keyword))
														codeList4.Add(keywordList1[i - 1] + keyword);
													else if (i + 1 < keywordList1.Count &&
															 dataList3.Where(x => Regex.IsMatch(x.ToString(), regex3 + "[^0-9A-Z]{1,1}" + keyword + keywordList1[i + 1] + "([^0-9A-Z]{1,1}|$)")).Count() > 0 &&
															 !codeList4.Contains(keyword + keywordList1[i + 1]))
														codeList4.Add(keyword + keywordList1[i + 1]);
												}
											}
										}

										if (codeList4.Count > 0)
										{
											regex3 += "[^0-9A-Z]{1,1}";
											isFinded = true;
										}
									}

									if (isFinded == false)
									{
										List<string> tmpList = keywordList.Where(x => x != code &&
																					  x != code1 &&
																					  x != code2 &&
																					  x != code3 &&
																					  keywordList.IndexOf(x) < index &&
																					  Regex.IsMatch(x, "^[0-9]*$") &&
																					  dataList3.Where(x1 => Regex.IsMatch(x1.ToString(), regex3 + "[^0-9A-Z]{1,1}[0-9A-Z]{0,}" + x + "[0-9A-Z]{0,}([^0-9A-Z]{1,1}|$)")).Count() > 0)
																		  .OrderByDescending(x => keywordList.IndexOf(x))
																		  .ToList<string>();

										foreach (string keyword in tmpList)
										{
											for (int i = 0; i < keywordList1.Count; i++)
											{
												if (keywordList1[i] == keyword)
												{
													if (i - 1 >= 0 &&
														dataList3.Where(x => Regex.IsMatch(x.ToString(), regex3 + "[^0-9A-Z]{1,1}" + keywordList1[i - 1] + keyword + "([^0-9A-Z]{1,1}|$)")).Count() > 0 &&
														!codeList4.Contains(keywordList1[i - 1] + keyword))
														codeList4.Add(keywordList1[i - 1] + keyword);
													else if (i + 1 < keywordList1.Count &&
															 dataList3.Where(x => Regex.IsMatch(x.ToString(), regex3 + "[^0-9A-Z]{1,1}" + keyword + keywordList1[i + 1] + "([^0-9A-Z]{1,1}|$)")).Count() > 0 &&
															 !codeList4.Contains(keyword + keywordList1[i + 1]))
														codeList4.Add(keyword + keywordList1[i + 1]);
												}
											}
										}

										if (codeList4.Count > 0)
										{
											regex3 += "[^0-9A-Z]{1,1}";
											isFinded = true;
										}
									}
								}
								#endregion

								foreach (string plateNo in dataList3.Where(x => Regex.IsMatch(x.ToString(), regex2 + code3 + "$")))
								{
									if (!plateNoList.Contains(plateNo))
										plateNoList.Add(plateNo);
								}

								foreach (string code4 in codeList4)
								{
									#region 6번째 자리
									string regex4 = regex3 + code4;

									List<string> dataList4 = dataList3.Where(x => Regex.IsMatch(x.ToString(), regex4 + "([^0-9A-Z]{1,1}|$)")).ToList<string>();

									if (dataList4.Where(x => Regex.IsMatch(x.ToString(), regex3 + code4 + "$")).Count() == 1)
									{
										string plateNo = dataList4.Where(x => Regex.IsMatch(x.ToString(), regex3 + code4 + "$")).First();

										if (!plateNoList.Contains(plateNo))
											plateNoList.Add(plateNo);

										continue;
									}
									#endregion
								}
							}
						}
					}
				}
				#endregion

				#region plateNo 하나 이상일 경우 처리
				if (plateNoList.Count > 0)
				{
					bool isFirstSubject, isFirstItem, isFirstName, isLastSubject, isLastItem, isLastName;

					tmpText = drItem["NM_SUBJECT"].ToString().ToUpper().Replace("\n", " ");
					tmpText = Regex.Replace(tmpText, "(^|[^a-zA-Z0-9]{1,})[0-9]{4}-{1}[0-9]{2}-{1}[0-9]{2}([^a-zA-Z0-9]{1,}|$)", string.Empty);
					tmpText = Regex.Replace(tmpText, "[^0-9A-Z]", " ");

					List<string> subjectKeyword = tmpText.Split(" ".ToCharArray())
														 .Where(x => !string.IsNullOrEmpty(x))
														 .Where(x => Regex.IsMatch(x, "[0-9]"))
														 .Distinct()
														 .ToList<string>();

					tmpText = drItem["CD_ITEM_PARTNER"].ToString().ToUpper().Replace("\n", " ");
					tmpText = Regex.Replace(tmpText, "(^|[^a-zA-Z0-9]{1,})[0-9]{4}-{1}[0-9]{2}-{1}[0-9]{2}([^a-zA-Z0-9]{1,}|$)", string.Empty);
					tmpText = Regex.Replace(tmpText, "[^0-9A-Z]", " ");

					List<string> itemKeyword = tmpText.Split(" ".ToCharArray())
													  .Where(x => !string.IsNullOrEmpty(x))
													  .Where(x => Regex.IsMatch(x, "[0-9]"))
													  .Distinct()
													  .ToList<string>();

					tmpText = drItem["NM_ITEM_PARTNER"].ToString().ToUpper().Replace("\n", " ");
					tmpText = Regex.Replace(tmpText, "(^|[^a-zA-Z0-9]{1,})[0-9]{4}-{1}[0-9]{2}-{1}[0-9]{2}([^a-zA-Z0-9]{1,}|$)", string.Empty);
					tmpText = Regex.Replace(tmpText, "[^0-9A-Z]", " ");

					List<string> nameKeyword = tmpText.Split(" ".ToCharArray())
													  .Where(x => !string.IsNullOrEmpty(x))
													  .Where(x => Regex.IsMatch(x, "[0-9]"))
													  .Distinct()
													  .ToList<string>();

					Dictionary<string, int> tmpDic = new Dictionary<string, int>();

					foreach (string plateNo in plateNoList)
					{
						#region 첫번째 자리 확인
						isFirstSubject = false;
						isFirstItem = false;
						isFirstName = false;

						tmpText = Regex.Replace(plateNo, "[^0-9A-Z]", " ").Split(" ".ToCharArray())[0];
						string tmpText1 = string.Empty;

						if (keywordList.Where(x => x == tmpText).Count() > 0)
						{
							tmpText1 = keywordList.Where(x => x == tmpText)
												  .OrderByDescending(x => x.Length)
												  .First();

							if (subjectKeyword.Where(x => x == tmpText1).Count() > 0)
								isFirstSubject = true;
							if (itemKeyword.Where(x => x == tmpText1).Count() > 0)
								isFirstItem = true;
							if (nameKeyword.Where(x => x == tmpText1).Count() > 0)
								isFirstName = true;
						}
						else if (keywordList.Where(x => Regex.IsMatch(tmpText, "^[0-9A-Z]{1,}" + x + "$")).Count() > 0)
						{
							tmpText1 = keywordList.Where(x => Regex.IsMatch(tmpText, "^[0-9A-Z]{1,}" + x + "$"))
											      .OrderByDescending(x => x.Length)
												  .First();

							if (subjectKeyword.Where(x => x == tmpText1).Count() > 0)
								isFirstSubject = true;
							if (itemKeyword.Where(x => x == tmpText1).Count() > 0)
								isFirstItem = true;
							if (nameKeyword.Where(x => x == tmpText1).Count() > 0)
								isFirstName = true;
						}
						else if (keywordList.Where(x => Regex.IsMatch(tmpText, "^" + x + "[0-9A-Z]{1,}$")).Count() > 0)
						{
							tmpText1 = keywordList.Where(x => Regex.IsMatch(tmpText, "^" + x + "[0-9A-Z]{1,}$"))
												  .OrderByDescending(x => x.Length)
												  .First();

							if (subjectKeyword.Where(x => x == tmpText1).Count() > 0)
								isFirstSubject = true;
							if (itemKeyword.Where(x => x == tmpText1).Count() > 0)
								isFirstItem = true;
							if (nameKeyword.Where(x => x == tmpText1).Count() > 0)
								isFirstName = true;
						}
						else if (keywordList.Where(x => x.StartsWith("P") && tmpText == x.Substring(1, x.Length - 1)).Count() > 0)
						{
							tmpText1 = keywordList.Where(x => x.StartsWith("P") && tmpText == x.Substring(1, x.Length - 1))
												  .OrderByDescending(x => x.Length)
												  .First();

							if (subjectKeyword.Where(x => x == tmpText1).Count() > 0)
								isFirstSubject = true;
							if (itemKeyword.Where(x => x == tmpText1).Count() > 0)
								isFirstItem = true;
							if (nameKeyword.Where(x => x == tmpText1).Count() > 0)
								isFirstName = true;
						}
						#endregion

						#region 마지막 자리 확인
						string[] textArray = Regex.Replace(plateNo, "[^0-9A-Z]", " ").Split(" ".ToCharArray())
																					 .Where(x => !string.IsNullOrEmpty(x))
																					 .ToArray();

						if (tmpText == textArray[textArray.Length - 1])
						{
							if (plateNoList.Where(x => Regex.IsMatch(x, tmpText1)).Count() == 1)
							{
								if (isFirstItem == true)
									tmpDic.Add(plateNo, 1);
								else if (isFirstName == true)
									tmpDic.Add(plateNo, 2);
								else
									tmpDic.Add(plateNo, 3);
							}
						}
						else
						{
							isLastSubject = false;
							isLastItem = false;
							isLastName = false;

							tmpText = textArray[textArray.Length - 1];

							if (keywordList.Where(x => x == tmpText).Count() > 0)
							{
								tmpText1 = keywordList.Where(x => x == tmpText)
													  .OrderByDescending(x => x.Length)
													  .First();

								if (subjectKeyword.Where(x => x == tmpText1).Count() > 0)
									isLastSubject = true;
								if (itemKeyword.Where(x => x == tmpText1).Count() > 0)
									isLastItem = true;
								if (nameKeyword.Where(x => x == tmpText1).Count() > 0)
									isLastName = true;
							}
							else if (keywordList.Select(x => Regex.IsMatch(x, "^[0-9]*$") && x.Length <= 9 ? Convert.ToInt32(x).ToString() : x)
												.Where(x => Regex.IsMatch(tmpText, "^0{0,}" + x + "$")).Count() > 0)
							{
								tmpText1 = keywordList.Select(x => Regex.IsMatch(x, "^[0-9]*$") && x.Length <= 9 ? Convert.ToInt32(x).ToString() : x)
													  .Where(x => Regex.IsMatch(tmpText, "^0{0,}" + x + "$"))
													  .OrderByDescending(x => x.Length)
													  .First();

								if (subjectKeyword.Select(x => Regex.IsMatch(x, "^[0-9]*$") && x.Length <= 9 ? Convert.ToInt32(x).ToString() : x)
												  .Where(x => x == tmpText1).Count() > 0)
								{
									isLastSubject = true;
								}
								if (itemKeyword.Select(x => Regex.IsMatch(x, "^[0-9]*$") && x.Length <= 9 ? Convert.ToInt32(x).ToString() : x)
											   .Where(x => x == tmpText1).Count() > 0)
								{
									isLastItem = true;
								}
								if (nameKeyword.Select(x => Regex.IsMatch(x, "^[0-9]*$") && x.Length <= 9 ? Convert.ToInt32(x).ToString() : x)
											   .Where(x => x == tmpText1).Count() > 0)
								{
									isLastName = true;
								}
							}
							else if (keywordList.Where(x => Regex.IsMatch(tmpText, "^[0-9A-Z]{1,}" + x + "$")).Count() > 0)
							{
								tmpText1 = keywordList.Where(x => Regex.IsMatch(tmpText, "^[0-9A-Z]{1,}" + x + "$"))
													  .OrderByDescending(x => x.Length)
													  .First();

								if (subjectKeyword.Where(x => x == tmpText1).Count() > 0)
									isLastSubject = true;
								if (itemKeyword.Where(x => x == tmpText1).Count() > 0)
									isLastItem = true;
								if (nameKeyword.Where(x => x == tmpText1).Count() > 0)
									isLastName = true;
							}
							else if (keywordList.Where(x => Regex.IsMatch(tmpText, "^" + x + "[0-9A-Z]{1,}$")).Count() > 0)
							{
								tmpText1 = keywordList.Where(x => Regex.IsMatch(tmpText, "^" + x + "[0-9A-Z]{1,}$"))
													  .OrderByDescending(x => x.Length)
													  .First();

								if (subjectKeyword.Where(x => x == tmpText1).Count() > 0)
									isLastSubject = true;
								if (itemKeyword.Where(x => x == tmpText1).Count() > 0)
									isLastItem = true;
								if (nameKeyword.Where(x => x == tmpText1).Count() > 0)
									isLastName = true;
							}
							else if (keywordList.Where(x => x.Length >= tmpText.Length &&
															x.Substring(0, tmpText.Length) == tmpText).Count() > 0)
							{
								tmpText1 = keywordList.Where(x => x.Length >= tmpText.Length &&
																  x.Substring(0, tmpText.Length) == tmpText)
													  .OrderByDescending(x => x.Length)
													  .First();

								if (subjectKeyword.Where(x => x == tmpText1).Count() > 0)
									isLastSubject = true;
								if (itemKeyword.Where(x => x == tmpText1).Count() > 0)
									isLastItem = true;
								if (nameKeyword.Where(x => x == tmpText1).Count() > 0)
									isLastName = true;
							}

							if (isFirstItem == true && isLastItem == true && !tmpDic.ContainsKey(plateNo))
								tmpDic.Add(plateNo, 1);
							if (isFirstSubject == true && isLastItem == true && !tmpDic.ContainsKey(plateNo))
								tmpDic.Add(plateNo, 2);
							if (isFirstName == true && isLastItem == true && !tmpDic.ContainsKey(plateNo))
								tmpDic.Add(plateNo, 2);
							if (isFirstSubject == true && isLastSubject == true && !tmpDic.ContainsKey(plateNo))
								tmpDic.Add(plateNo, 3);
							if (isFirstName == true && isLastName == true && !tmpDic.ContainsKey(plateNo))
								tmpDic.Add(plateNo, 3);
							if (isFirstItem == true && isLastName == true && !tmpDic.ContainsKey(plateNo))
								tmpDic.Add(plateNo, 4);
							if (isFirstSubject == true && isLastName == true && !tmpDic.ContainsKey(plateNo))
							{

							}
							if (isFirstItem == true && isLastSubject == true && !tmpDic.ContainsKey(plateNo))
							{

							}
							if (isFirstName == true && isLastSubject == true && !tmpDic.ContainsKey(plateNo))
							{

							}
						}
						#endregion
					}

					plateNoList = new List<string>();

					if (tmpDic.Where(x => x.Value == 1).Count() > 0)
					{
						List<string> tmpList = tmpDic.Where(x => x.Value == 1).Select(x => x.Key).ToList();
						plateNoList = tmpList.Where(x => tmpList.Where(x1 => Regex.IsMatch(Regex.Replace(x1, "[^0-9A-Z]", " "), Regex.Replace(x, "[^0-9A-Z]", " "))).Count() == 1).ToList();
					}
					else if (tmpDic.Where(x => x.Value == 2).Count() > 0)
					{
						List<string> tmpList = tmpDic.Where(x => x.Value == 2).Select(x => x.Key).ToList();
						plateNoList = tmpList.Where(x => tmpList.Where(x1 => Regex.IsMatch(Regex.Replace(x1, "[^0-9A-Z]", " "), Regex.Replace(x, "[^0-9A-Z]", " "))).Count() == 1).ToList();
					}
					else if (tmpDic.Where(x => x.Value == 3).Count() > 0)
					{
						List<string> tmpList = tmpDic.Where(x => x.Value == 3).Select(x => x.Key).ToList();
						plateNoList = tmpList.Where(x => tmpList.Where(x1 => Regex.IsMatch(Regex.Replace(x1, "[^0-9A-Z]", " "), Regex.Replace(x, "[^0-9A-Z]", " "))).Count() == 1).ToList();
					}
					else if (tmpDic.Where(x => x.Value == 4).Count() > 0)
					{
						List<string> tmpList = tmpDic.Where(x => x.Value == 4).Select(x => x.Key).ToList();
						plateNoList = tmpList.Where(x => tmpList.Where(x1 => Regex.IsMatch(Regex.Replace(x1, "[^0-9A-Z]", " "), Regex.Replace(x, "[^0-9A-Z]", " "))).Count() == 1).ToList();
					}
				}
				#endregion

				#region plateNo 하나일 경우 처리
				if (plateNoList.Count == 1)
				{
					tmpText = plateNoList[0];
					string text = (drItem["NM_SUBJECT"].ToString() + " " + drItem["CD_ITEM_PARTNER"].ToString() + " " + drItem["NM_ITEM_PARTNER"].ToString()).ToUpper().Replace("\n", " ");

					foreach (string plateNo in text.Split(" ".ToCharArray()).Where(x => Regex.IsMatch(x, tmpText + "([^0-9A-Z\\(\\)\\{\\}\\[\\]]{1,1}[0-9A-Z]{1,}){1,}")))
					{
						if (!plateNoList.Contains(plateNo) && 
							dt2.Select("NO_PLATE = '" + plateNo + "'").Length > 0)
							plateNoList.Add(plateNo);
					}

					List<string> tmpList = plateNoList;
					plateNoList = tmpList.Where(x => tmpList.Where(x1 => Regex.IsMatch(Regex.Replace(x1, "[^0-9A-Z]", " "), Regex.Replace(x, "[^0-9A-Z]", " "))).Count() == 1).ToList();
				}
				#endregion

				#endregion
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}
		#endregion

		#region 컨트롤 이벤트
		private void txt쿼리_GotFocus(object sender, EventArgs e)
		{
			
			try
			{
				if (((Control)sender).Name == this.txt쿼리1.Name)
					this.첫번째창 = true;
				else
					this.첫번째창 = false;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btnERP조회_Click(object sender, EventArgs e)
		{
			try
			{
				if (string.IsNullOrEmpty(this.쿼리.Text))
					return;

				this.결과창.DataSource = Global.MainFrame.FillDataTable(this.쿼리.Text);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn관리자권한조회_Click(object sender, EventArgs e)
		{
			SqlConnection sqlConnection;
			SqlCommand sqlCommand;
			SqlDataAdapter sqlDataAdapter;
			DataTable dataTable;

			string connctionString;

			if (string.IsNullOrEmpty(this.쿼리.Text))
			{
				this.쿼리.Text = "DBCC OPENTRAN -- 활성 트랙잭션" + Environment.NewLine +
								 "EXEC SP_WHO" + Environment.NewLine +
								 "EXEC SP_WHO2" + Environment.NewLine +
								 "EXEC SP_LOCK -- LOCK ID" + Environment.NewLine +
								 "DBCC INPUTBUFFER(116) -- ID 상세내용" + Environment.NewLine +
								 "--KILL 74 -- ID 죽이기" + Environment.NewLine +
								 "SELECT [TRANSACTION NAME] FROM SYS.FN_DBLOG(NULL,NULL) WHERE [TRANSACTION NAME] IS NOT NULL -- 트랜잭션 로그" + Environment.NewLine +
								 "SELECT * FROM SYS.SYSPROCESSES WHERE BLOCKED != 0 ORDER BY BLOCKED -- BLOCK 리스트";

				return;
			}

			connctionString = "Server=113.130.254.143; Uid=sa; Password=skm0828!";
			sqlConnection = new SqlConnection(connctionString);

			try
			{
				sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				sqlCommand.CommandText = this.쿼리.Text;

				sqlConnection.Open();

				dataTable = new DataTable();

				sqlDataAdapter = new SqlDataAdapter();
				sqlDataAdapter.SelectCommand = sqlCommand;
				sqlDataAdapter.Fill(dataTable);

				this.결과창.DataSource = dataTable;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
			finally
			{
				sqlConnection.Close();
			}
		}

		private void btn프로시저_Click(object sender, EventArgs e)
		{
			string text;

			try
			{
				text = "EXEC SP_HELPTEXT " + this.쿼리.Text;
				this.실행(text);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn종속성확인_Click(object sender, EventArgs e)
		{
			string text;

			try
			{
				text = "EXEC SP_DEPENDS " + this.쿼리.Text;
				this.실행(text);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn제약조건_Click(object sender, EventArgs e)
		{
			string text;

			try
			{
				text = "EXEC SP_HELPCONSTRAINT " + this.쿼리.Text;
				this.실행(text);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn인덱스_Click(object sender, EventArgs e)
		{
			string text;

			try
			{
				text = "EXEC SP_HELPINDEX " + this.쿼리.Text;
				this.실행(text);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btnText검색_Click(object sender, EventArgs e)
		{
			string text;

			try
			{
				//text = "SELECT ROUTINE_NAME " +
				//       "FROM INFORMATION_SCHEMA.ROUTINES " +
				//       "WHERE ROUTINE_DEFINITION LIKE '%" + this.txt쿼리.Text + "%' " +
				//       "AND ROUTINE_TYPE = 'PROCEDURE' " +
				//       "ORDER BY ROUTINE_NAME ";

				text = @"SELECT DISTINCT SO.NAME 
						 FROM DBO.SYSOBJECTS SO WITH(NOLOCK), DBO.SYSCOMMENTS SC WITH(NOLOCK)
						 WHERE SO.ID = SC.ID
						 AND TYPE = 'P'
						 AND SC.TEXT LIKE '%" + this.쿼리.Text + "%' " +
						"ORDER BY SO.NAME";

				this.실행(text);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn컬럼사용처_Click(object sender, EventArgs e)
		{
			string text;

			try
			{
				text = @"SELECT B.NAME AS 테이블명,
								A.NAME AS 컬럼명,
								TYPE_NAME(USER_TYPE_ID) AS 데이타타입,
								CONVERT(INT, MAX_LENGTH) AS 길이,
								B.CRDATE AS 테이블생성일
						FROM SYS.ALL_COLUMNS AS A WITH(NOLOCK)
						JOIN SYSOBJECTS AS B WITH(NOLOCK) ON A.OBJECT_ID=B.ID
						AND B.XTYPE='U'
						WHERE A.NAME='" + this.쿼리.Text + "'";

				this.실행(text);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn테이블_Click(object sender, EventArgs e)
		{
			string text;

			try
			{
				text = @"SELECT A.TABLE_NAME,
								C.VALUE AS TABLE_COMMENT,
								A.COLUMN_NAME,
								A.DATA_TYPE,
								ISNULL(CAST(A.CHARACTER_MAXIMUM_LENGTH AS VARCHAR),
								CAST(A.NUMERIC_PRECISION AS VARCHAR) + ',' + CAST(A.NUMERIC_SCALE AS VARCHAR)) AS COLUMN_LENGTH,
								A.COLUMN_DEFAULT,
								A.IS_NULLABLE,
								B.VALUE AS COLUM_COMMENT
						 FROM INFORMATION_SCHEMA.COLUMNS A WITH(NOLOCK)
						 LEFT JOIN SYS.EXTENDED_PROPERTIES B WITH(NOLOCK) ON B.major_id = object_id(A.TABLE_NAME) AND A.ORDINAL_POSITION = B.minor_id
						 LEFT JOIN (SELECT object_id(objname) AS TABLE_ID,
										   VALUE
									FROM ::FN_LISTEXTENDEDPROPERTY (NULL, 'User','dbo','table',NULL, NULL, NULL)) C
						 ON object_id(A.TABLE_NAME) = C.TABLE_ID
						 WHERE A.TABLE_NAME = '" + this.쿼리.Text + "'" +
						"ORDER BY A.TABLE_NAME, A.ORDINAL_POSITION ";

				this.실행(text);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn사용자정보ERP_Click(object sender, EventArgs e)
		{
			string password, key, companyCode, userID;

			DataTable dt;

			try
			{
				dt = Global.MainFrame.FillDataTable(@"SELECT MU.CD_COMPANY, 
                                                             MU.ID_USER, 
                                                             MU.NM_USER, 
                                                             MG.CD_GROUP, 
                                                             MU.PASS_WORD, 
                                                             '' AS PASSWORD,
															 'http://m.dintec.co.kr/QR/NameCard.aspx?U=' + NEOE.SFN_BASE64_ENCODE(MU.ID_USER) AS ID_ENCODED 
													  FROM MA_USER MU WITH(NOLOCK)
													  LEFT JOIN MA_GRANT MG WITH(NOLOCK) ON MG.CD_COMPANY = MU.CD_COMPANY AND MG.ID_USER = MU.ID_USER");

				foreach (DataRow dr in dt.Rows)
				{
					companyCode = D.GetString(dr["CD_COMPANY"]);
					userID = D.GetString(dr["ID_USER"]);
					key = companyCode + userID;
					password = D.GetString(dr["PASS_WORD"]);

					try
					{
						dr["PASSWORD"] = this.DecryptString(password, key);
					}
					catch
					{
						dr["PASSWORD"] = D.GetString(dr["PASS_WORD"]);
					}
				}

				this.결과창.DataSource = dt;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn암호화복호화_Click(object sender, EventArgs e)
		{
			string password, key, companyCode, userID;
			string[] text;

			try
			{
				text = this.쿼리.Text.Split('|');

				if (text.Length != 2)
				{
					this.ShowMessage("입력형식이 올바르지 않습니다.\n입력형식 : 사용자ID|패스워드");
					return;
				}

				companyCode = Global.MainFrame.LoginInfo.CompanyCode;
				userID = text[0];
				key = companyCode + userID;
				password = text[1];

				if (this.chk암호화.Checked == true)
					this.쿼리.Text = this.EncryptString(password, key);
				else
					this.쿼리.Text = this.DecryptString(password, key);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn외부DB조회_Click(object sender, EventArgs e)
		{
			try
			{
				if (string.IsNullOrEmpty(this.cboDB유형.SelectedValue.ToString()) || string.IsNullOrEmpty(this.쿼리.Text))
					return;

				if (this.chkOpenQuery.Checked == true)
					this.결과창.DataSource = this.OpenQuery(D.GetString(this.cboDB유형.SelectedValue), this.쿼리.Text);       
				else
					this.결과창.DataSource = this.DirectQuery(D.GetString(this.cboDB유형.SelectedValue), this.쿼리.Text);      
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btnFAX발송_Click(object sender, EventArgs e)
		{
			try
			{
				P_CZ_MA_FAX_SUB dialog = new P_CZ_MA_FAX_SUB("462-7908", "TEST", "SB15000004_01_06154_PINQ_20150921.pdf", "SB15000004");
				dialog.ShowDialog();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn메일발송_Click(object sender, EventArgs e)
		{
			try
			{
				P_CZ_MA_EMAIL_SUB dialog = new P_CZ_MA_EMAIL_SUB("khkim@dintec.co.kr/딘텍 김기현 대리",
																 "khkim@dintec.co.kr",
																 "khkim@dintec.co.kr",
																 "dykim@dintec.co.kr",
																 Global.MainFrame.LoginInfo.CompanyName + " -INQUERY(DY17090107)",
																 new string[] { "DY17090107_20222_PORD_20181031(3).pdf", "라벨_DY17090107-02_20222(3).pdf" },
																 null,
																 "TEST",
																 "DY17090107",
																 "00001", 
																 false);
				dialog.ShowDialog();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn메일발송2_Click(object sender, EventArgs e)
		{
			try
			{
				P_CZ_MA_EMAIL_SUB1 dialog = new P_CZ_MA_EMAIL_SUB1(new string[] { string.Empty }, string.Empty, new ReportHelper[] { new ReportHelper("R_CZ_SA_CLAIM", "클레임") }, null);
				dialog.ShowDialog();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn단체메일발송_Click(object sender, EventArgs e)
		{
			try
			{
				P_CZ_MA_EMAIL_SUB2 dialog = new P_CZ_MA_EMAIL_SUB2();
				dialog.ShowDialog();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn쪽지보내기_Click(object sender, EventArgs e)
		{
			string contents;

			try
			{
				contents = "TEST";

				//Messenger.SendMSG(new string[] { "S-359", "S-343", "S-391", "S-347" }, contents);
				if (Messenger.SendMSG(new string[] { "S-391" }, contents) == true)
				{
					this.ShowMessage(공통메세지._작업을완료하였습니다, this.DD("쪽지보내기"));
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn사용자정보메일_Click(object sender, EventArgs e)
		{
			string query, 회사코드;
			DBMgr dbMgr;
			DataTable dt, dt1;
            DataRow[] dataRowArray;
			UserServiceFlag flag;

			try
			{
				query = @"SELECT DU.DU_USERID,
								 DU.DU_NAME,
								 DU.DU_PWD,
								 DM.DM_NAME,
								 (DU.DU_USERID + '@' + DM.DM_NAME) MAIL_ADDR,
                                 '' AS NO_EMP,
                                 '' AS DT_RETIRE,
								 DU.DU_FORWARD,
								 DU.DU_FORWARDLIST,
								 DU.DU_PWDSETDATE,
								 DU.DU_PERMITSENTDAILY,
								 (CASE WHEN ISNULL(DU.DU_OTPSECRET, '') = '' THEN 'N' ELSE 'Y' END) AS YN_OTP,
								 DU.DU_PERMITPROTOCOL,
								 '' AS YN_SMTP,
								 '' AS YN_POP3,
								 '' AS YN_IMAP,
								 '' AS YN_WEBMOBILE,
								 '' AS YN_WEBMAIL,
								 '' AS YN_SMTP_EX,
								 '' AS YN_POP3_EX,
								 '' AS YN_IMAP_EX
						  FROM MCDOMAINUSER DU WITH(NOLOCK)
						  LEFT JOIN MCDOMAIN DM WITH(NOLOCK) ON DM.DM_ID = DU.DM_ID";

				dbMgr = new DBMgr(DBConn.Mail);
				dbMgr.Query = query;
				dt = dbMgr.GetDataTable();

                query = @"SELECT ME.CD_COMPANY,
                                 ME.NO_EMP,
                                 ISNULL(ME.DT_RETIRE, '00000000') AS DT_RETIRE
						  FROM MA_EMP ME WITH(NOLOCK)
						  WHERE ME.CD_COMPANY IN ('K100', 'K200', 'S100')";

                dbMgr = new DBMgr(DBConn.iU);
                dbMgr.Query = query;

                dt1 = dbMgr.GetDataTable();

				foreach(DataRow dr in dt.Rows)
				{
                    if (dr["DU_NAME"].ToString().IndexOf('(') > 0)
                    {
                        dr["NO_EMP"] = dr["DU_NAME"].ToString().Split('(')[1].Split(')')[0];

                        switch (dr["DM_NAME"].ToString())
                        {
                            case "dintec.co.kr":
                                회사코드 = "K100";
                                break;
                            case "dubheco.com":
                                회사코드 = "K200";
                                break;
                            case "dintec.com.sg":
                                회사코드 = "S100";
                                break;
                            default:
                                회사코드 = string.Empty;
                                break;
                        }

                        dataRowArray = dt1.Select("CD_COMPANY = '" + 회사코드 + "' AND NO_EMP = '" + dr["NO_EMP"].ToString() + "'");

                        if (dataRowArray == null || dataRowArray.Length == 0)
                            dr["NO_EMP"] = string.Empty;
                        else
                        {
                            dr["DT_RETIRE"] = dataRowArray[0]["DT_RETIRE"].ToString();

                            if (dr["DT_RETIRE"].ToString() == "00000000")
                                dr["DT_RETIRE"] = string.Empty;
                            
                            dr["DU_NAME"] = dr["DU_NAME"].ToString().Split('(')[0];
                        }
                    }

					flag = (UserServiceFlag)dr["DU_PERMITPROTOCOL"];

					dr["YN_SMTP"] = ((flag & UserServiceFlag.Smtp) > UserServiceFlag.None ? "Y" : "N");
					dr["YN_POP3"] = ((flag & UserServiceFlag.Pop3) > UserServiceFlag.None ? "Y" : "N");
					dr["YN_IMAP"] = ((flag & UserServiceFlag.Imap) > UserServiceFlag.None ? "Y" : "N");
					dr["YN_WEBMOBILE"] = ((flag & UserServiceFlag.WebMobile) > UserServiceFlag.None ? "Y" : "N");
					dr["YN_WEBMAIL"] = ((flag & UserServiceFlag.WebMail) > UserServiceFlag.None ? "Y" : "N");
					dr["YN_SMTP_EX"] = ((flag & UserServiceFlag.SmtpInternet) > UserServiceFlag.None ? "Y" : "N");
					dr["YN_POP3_EX"] = ((flag & UserServiceFlag.Pop3Internet) > UserServiceFlag.None ? "Y" : "N");
					dr["YN_IMAP_EX"] = ((flag & UserServiceFlag.ImapInternet) > UserServiceFlag.None ? "Y" : "N");
				}

				this.결과창.DataSource = dt;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn메일링리스트_Click(object sender, EventArgs e)
		{
			string query, 회사코드;
			string[] addrList;
			DBMgr dbMgr;
			DataTable dt, dt1, dt2, tmpDt;
			DataRow[] dataRowArray;
			DataRow tmpRow;

			try
			{
				query = @"SELECT DM.DM_NAME,
								 ML.ML_MAILID,
								 ML.ML_NAME,
								 ML.ML_LIST 
						  FROM MCMAILINGLIST ML WITH(NOLOCK)
						  LEFT JOIN MCDOMAIN DM WITH(NOLOCK) ON DM.DM_ID = ML.DM_ID";

				dbMgr = new DBMgr(DBConn.Mail);
				dbMgr.Query = query;

				dt = dbMgr.GetDataTable();

				query = @"SELECT ME.CD_COMPANY,
                                 ME.NO_EMP,
                                 ISNULL(ME.DT_RETIRE, '00000000') AS DT_RETIRE
						  FROM MA_EMP ME WITH(NOLOCK)
						  WHERE ME.CD_COMPANY IN ('K100', 'K200', 'S100')";

				dbMgr = new DBMgr(DBConn.iU);
				dbMgr.Query = query;

				dt2 = dbMgr.GetDataTable();

				dt1 = new DataTable();
				dt1.Columns.Add("DM_NAME");
				dt1.Columns.Add("ML_MAILID");
				dt1.Columns.Add("ML_NAME");
				dt1.Columns.Add("MAIL_ADDR");
				dt1.Columns.Add("USER_NAME");
				dt1.Columns.Add("NO_EMP");
				dt1.Columns.Add("DT_RETIRE");

				foreach (DataRow dr in dt.Rows)
				{
					addrList = dr["ML_LIST"].ToString().Split(';');

					foreach (string addr in addrList)
					{
						tmpRow = dt1.NewRow();

						tmpRow["DM_NAME"] = dr["DM_NAME"].ToString();
						tmpRow["ML_MAILID"] = dr["ML_MAILID"].ToString();
						tmpRow["ML_NAME"] = dr["ML_NAME"].ToString();
						tmpRow["MAIL_ADDR"] = addr;

						query = @"SELECT DU.DU_NAME
								  FROM MCDOMAINUSER DU WITH(NOLOCK)
								  LEFT JOIN MCDOMAIN DM WITH(NOLOCK) ON DM.DM_ID = DU.DM_ID
								  WHERE (DU.DU_USERID + '@' + DM.DM_NAME) = '" + addr + "'";

						dbMgr = new DBMgr(DBConn.Mail);
						dbMgr.Query = query;
						tmpDt = dbMgr.GetDataTable();

						if (tmpDt.Rows.Count > 0)
						{
							tmpRow["USER_NAME"] = tmpDt.Rows[0]["DU_NAME"].ToString();

							switch (tmpRow["DM_NAME"].ToString())
							{
								case "dintec.co.kr":
									회사코드 = "K100";
									break;
								case "dubheco.com":
									회사코드 = "K200";
									break;
								case "dintec.com.sg":
									회사코드 = "S100";
									break;
								default:
									회사코드 = string.Empty;
									break;
							}

							if (tmpRow["USER_NAME"].ToString().IndexOf('(') > 0)
							{
								tmpRow["NO_EMP"] = tmpRow["USER_NAME"].ToString().Split('(')[1].Split(')')[0];

								dataRowArray = dt2.Select("CD_COMPANY = '" + 회사코드 + "' AND NO_EMP = '" + tmpRow["NO_EMP"].ToString() + "'");

								if (dataRowArray == null || dataRowArray.Length == 0)
									tmpRow["NO_EMP"] = string.Empty;
								else
								{
									tmpRow["DT_RETIRE"] = dataRowArray[0]["DT_RETIRE"].ToString();

									if (tmpRow["DT_RETIRE"].ToString() == "00000000")
										tmpRow["DT_RETIRE"] = string.Empty;
								}
							}
						}
						
						dt1.Rows.Add(tmpRow);
					}
				}

				this.결과창.DataSource = dt1;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn사용자정보GW_Click(object sender, EventArgs e)
		{
			string query;
			DBMgr dbMgr;
			DataTable gwDt, erpDt;
			DataRow[] dataRowArray;

			try
			{
				query = @"SELECT TU.LOGON_CD AS ID,
								 TU.USER_NM_KR AS NAME,
								 TU.EMAIL_ID AS EMAIL,
								 TU.LOGON_PW,
								 TU.EA_PW,
								 TU.HR_PAY_PW,
								 UD.EMP_NO AS ERP_NO,
								 UD.ENTER_DT,
								 UD.FIRE_DT,
								 '' AS DT_ENTER,
								 '' AS DT_RETIRE,
								 '' AS NO_EMAIL,
								 '' AS NO_TEL,
								 '' AS NO_TEL_EMER,
								 '' AS DC_RMK1
						  FROM BX.TCMG_USER TU WITH(NOLOCK)
						  JOIN BX.TCMG_USERDEPT UD WITH(NOLOCK) ON UD.USER_ID = TU.USER_ID
                          WHERE ISNULL(UD.FIRE_DT, '') = ''
                          ORDER BY TU.LOGON_CD";

				dbMgr = new DBMgr(DBConn.GroupWare);
				dbMgr.Query = query;

				gwDt = dbMgr.GetDataTable();

				query = @"SELECT NO_EMP,
								 NM_KOR,
								 DT_ENTER,
								 ISNULL(DT_RETIRE, '00000000') AS DT_RETIRE,
								 NO_EMAIL,
								 NO_TEL,
								 NO_TEL_EMER,
								 DC_RMK1 
						  FROM MA_EMP WITH(NOLOCK)
						  WHERE CD_COMPANY IN ('K100', 'K200', 'S100')";

				dbMgr = new DBMgr(DBConn.iU);
				dbMgr.Query = query;

				erpDt = dbMgr.GetDataTable();

				foreach (DataRow dr in gwDt.Rows)
				{
					dataRowArray = erpDt.Select("NO_EMP = '" + D.GetString(dr["ID"]) + "'");

					if (dataRowArray != null && dataRowArray.Length > 0)
					{
						dr["DT_ENTER"] = dataRowArray[0]["DT_ENTER"];
						dr["DT_RETIRE"] = dataRowArray[0]["DT_RETIRE"];
                        if (dr["DT_RETIRE"].ToString() == "00000000")
                            dr["DT_RETIRE"] = string.Empty;
						dr["NO_EMAIL"] = dataRowArray[0]["NO_EMAIL"];
						dr["NO_TEL"] = dataRowArray[0]["NO_TEL"];
						dr["NO_TEL_EMER"] = dataRowArray[0]["NO_TEL_EMER"];
						dr["DC_RMK1"] = dataRowArray[0]["DC_RMK1"];
					}
				}

				this.결과창.DataSource = gwDt;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn그룹웨어정보갱신_Click(object sender, EventArgs e)
		{
			string query, domain;
			DBMgr dbMgr;
			DataTable mailDt, userDt;
			DataRow[] dataRowArray;
 
			try
			{
				query = @"SELECT TU.LOGON_CD, 
								 TU.USER_NM_KR,
								 TU.EMAIL_ID,
								 TU.OTHER_EMAIL,
								 '' AS NEW_EMAIL,
								 UD.EMP_NO
						  FROM BX.TCMG_USER TU WITH(NOLOCK)
						  LEFT JOIN BX.TCMG_USERDEPT UD WITH(NOLOCK) ON UD.USER_ID = TU.USER_ID
						  WHERE (TU.LOGON_CD LIKE 'S-%' OR TU.LOGON_CD LIKE 'D-%' OR TU.LOGON_CD LIKE 'G-%')
						  AND UD.USER_ID IS NOT NULL AND ISNULL(UD.FIRE_DT, '') = ''";

				dbMgr = new DBMgr(DBConn.GroupWare);
				dbMgr.Query = query;

				userDt = dbMgr.GetDataTable();

				query = @"SELECT DU.DU_NAME,
								 DM.DM_NAME,
								 (DU.DU_USERID + '@' + DM.DM_NAME) MAIL_ADDR
						  FROM MCDOMAINUSER DU WITH(NOLOCK)
						  LEFT JOIN MCDOMAIN DM WITH(NOLOCK) ON DM.DM_ID = DU.DM_ID";

				dbMgr = new DBMgr(DBConn.Mail);
				dbMgr.Query = query;

				mailDt = dbMgr.GetDataTable();

				dbMgr = new DBMgr(DBConn.GroupWare);

				foreach (DataRow dr in userDt.Rows)
				{
					domain = string.Empty;

					switch(D.GetString(dr["LOGON_CD"]).Left(2))
					{
						case "S-":
							domain = "dintec.co.kr";
							break;
						case "D-":
							domain = "dubheco.com";
							break;
						case "G-":
							domain = "dintec.com.sg";
							break;
					}

                    dataRowArray = mailDt.Select("DU_NAME = '" + dr["USER_NM_KR"].ToString() + "(" + dr["LOGON_CD"].ToString().ToUpper() + ")" + "' AND DM_NAME = '" + domain + "'");

					if (dataRowArray.Length == 1)
					{
						if (D.GetString(dr["EMAIL_ID"]) != D.GetString(dataRowArray[0]["MAIL_ADDR"]) || 
							D.GetString(dr["OTHER_EMAIL"]) != D.GetString(dataRowArray[0]["MAIL_ADDR"]))
						{
							dr["NEW_EMAIL"] = D.GetString(dataRowArray[0]["MAIL_ADDR"]);

							query = @"UPDATE BX.TCMG_USER
									  SET EMAIL_ID = '" + D.GetString(dr["NEW_EMAIL"]) + "'," + Environment.NewLine +
										 "OTHER_EMAIL = '" + D.GetString(dr["NEW_EMAIL"]) + "'" + Environment.NewLine +									 
									 "WHERE LOGON_CD = '" + D.GetString(dr["LOGON_CD"]) + "'";

							dbMgr.Query = query;
							dbMgr.ExecuteNonQuery();
						}
					}
				}

				this.결과창.DataSource = userDt;

				query = @"UPDATE UD
						  SET UD.EMP_NO = TU.LOGON_CD
						  FROM BX.TCMG_USERDEPT UD
						  JOIN BX.TCMG_USER TU ON TU.USER_ID = UD.USER_ID
						  WHERE (TU.LOGON_CD LIKE 'S-%' OR TU.LOGON_CD LIKE 'D-%' OR TU.LOGON_CD LIKE 'G-%')
						  AND ISNULL(UD.EMP_NO, '') <> TU.LOGON_CD";

				dbMgr = new DBMgr(DBConn.GroupWare);
				dbMgr.Query = query;

				dbMgr.ExecuteNonQuery();

				this.ShowMessage(공통메세지._작업을완료하였습니다, "갱신");
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn그룹웨어문서삭제_Click(object sender, EventArgs e)
		{
			DBMgr dbMgr;
			string query, 문서번호;
			//DataTable dt;

			SqlConnection sqlConnection;
			SqlCommand sqlCommand;

			string connctionString;

			try
			{
				문서번호 = this.쿼리.Text;

				if (string.IsNullOrEmpty(문서번호)) return;
				if (this.ShowMessage("그룹웨어 문서가 삭제 됩니다.\n문서번호 : " + 문서번호 + "\n진행하시겠습니까 ?", "QY2") != DialogResult.Yes)
					return;

				//query = "SELECT * FROM FI_GWDOCU WHERE APP_DOC_ID = '" + 문서번호 + "'";

				//dbMgr = new DBMgr(DBConn.iU);
				//dbMgr.Query = query;
				//dt = dbMgr.GetDataTable();

				//if (dt == null || dt.Rows.Count <= 0)
				//{
				//    this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
				//    return;
				//}

				#region 그룹웨어 문서 제거
				if (this.ShowMessage(공통메세지.자료를삭제하시겠습니까, "QY2") != DialogResult.Yes)
					return;

				connctionString = "Server=113.130.254.143; Database=NeoBizboxS2; Uid=sa; Password=skm0828!";

				sqlConnection = new SqlConnection(connctionString);

				sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;

				try
				{
					sqlConnection.Open();

					sqlCommand.Transaction = sqlCommand.Connection.BeginTransaction();

					sqlCommand.CommandText = "DELETE FROM BX.TEAG_APPDOC WHERE DOC_ID = '" + 문서번호 + "'";
					sqlCommand.ExecuteNonQuery();

					sqlCommand.CommandText = "DELETE FROM BX.TEAG_APPDOC_COLUMN WHERE DOC_ID = '" + 문서번호 + "'";
					sqlCommand.ExecuteNonQuery();

					sqlCommand.CommandText = "DELETE FROM BX.TEAG_APPDOC_COMMENT WHERE DOC_ID = '" + 문서번호 + "'";
					sqlCommand.ExecuteNonQuery();

					sqlCommand.CommandText = "DELETE FROM BX.TEAG_APPDOC_CONTENTS WHERE DOC_ID = '" + 문서번호 + "'";
					sqlCommand.ExecuteNonQuery();

					sqlCommand.CommandText = "DELETE FROM BX.TEAG_APPDOC_DEPTLINE WHERE DOC_ID = '" + 문서번호 + "'";
					sqlCommand.ExecuteNonQuery();

					sqlCommand.CommandText = "DELETE FROM BX.TEAG_APPDOC_INTERLOCK WHERE DOC_ID = '" + 문서번호 + "'";
					sqlCommand.ExecuteNonQuery();

					sqlCommand.CommandText = "DELETE FROM BX.TEAG_APPDOC_LINE WHERE DOC_ID = '" + 문서번호 + "'";
					sqlCommand.ExecuteNonQuery();

					sqlCommand.CommandText = "DELETE FROM BX.TEAG_APPDOC_LINE_D WHERE DOC_ID = '" + 문서번호 + "'";
					sqlCommand.ExecuteNonQuery();

					sqlCommand.CommandText = "DELETE FROM BX.TEAG_APPDOC_LINE_M WHERE DOC_ID = '" + 문서번호 + "'";
					sqlCommand.ExecuteNonQuery();

					sqlCommand.CommandText = "DELETE FROM BX.TEAG_APPDOC_RECEIVE WHERE DOC_ID = '" + 문서번호 + "'";
					sqlCommand.ExecuteNonQuery();

					sqlCommand.CommandText = "DELETE FROM BX.TEAG_APPDOC_RECEIVEUSER WHERE DOC_ID = '" + 문서번호 + "'";
					sqlCommand.ExecuteNonQuery();

					sqlCommand.CommandText = "DELETE FROM BX.TEAG_APPDOC_REF WHERE DOC_ID = '" + 문서번호 + "'";
					sqlCommand.ExecuteNonQuery();

					sqlCommand.CommandText = "DELETE FROM BX.TEAG_FILE WHERE DOC_ID = '" + 문서번호 + "'";
					sqlCommand.ExecuteNonQuery();

					sqlCommand.Transaction.Commit();

					this.ShowMessage("그룹웨어 문서 삭제 완료");

					query = "DELETE FROM FI_GWDOCU WHERE APP_DOC_ID = '" + 문서번호 + "'";

					dbMgr = new DBMgr(DBConn.iU);
					dbMgr.Query = query;
					dbMgr.ExecuteNonQuery();

					this.ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
				}
				catch (Exception ex)
				{
					if (sqlCommand.Transaction != null)
						sqlCommand.Transaction.Rollback();

					this.MsgEnd(ex);
				}
				finally
				{
					sqlConnection.Close();
				}
				#endregion
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn코드동기화_Click(object sender, EventArgs e)
		{
			string query;

			try
			{
				if (string.IsNullOrEmpty(this.쿼리.Text)) return;

				query = @"BEGIN TRAN

						  DELETE FROM MA_CODE
						  WHERE CD_FIELD = '{0}'
						  AND CD_COMPANY <> '{1}'
						  
						  DELETE FROM MA_CODEDTL
						  WHERE CD_FIELD = '{0}'
						  AND CD_COMPANY <> '{1}'

						  INSERT INTO MA_CODE
						  (
							CD_FIELD,
							CD_COMPANY,
							NM_FIELD,
							FG1_SYSCODE,
							ID_INSERT,
							DTS_INSERT
						  )
						  SELECT CD.CD_FIELD,
								 MC.CD_COMPANY,
								 CD.NM_FIELD,
								 CD.FG1_SYSCODE,
								 'SYSTEM' AS ID_INSERT,
								 NEOE.SF_SYSDATE(GETDATE()) AS DTS_INSERT  
						  FROM MA_CODE CD WITH(NOLOCK)
						  LEFT JOIN MA_COMPANY MC ON MC.CD_COMPANY <> '{1}'
						  WHERE CD.CD_FIELD = '{0}'
						  AND CD.CD_COMPANY = '{1}'

						  INSERT INTO MA_CODEDTL
						  (
							CD_FIELD,
							CD_SYSDEF,
							CD_COMPANY,
							FG1_SYSCODE,
							NM_SYSDEF,
							USE_YN,
							CD_FLAG1,
							CD_FLAG2,
							CD_FLAG3,
							NM_SYSDEF_E,
							ID_INSERT,
							DTS_INSERT
						  )
						  SELECT CD.CD_FIELD,
								 CD.CD_SYSDEF,
								 MC.CD_COMPANY,
								 CD.FG1_SYSCODE,
								 CD.NM_SYSDEF,
								 CD.USE_YN,
								 CD.CD_FLAG1,
								 CD.CD_FLAG2,
								 CD.CD_FLAG3,
								 CD.NM_SYSDEF_E,
								 'SYSTEM' AS ID_INSERT,
								 NEOE.SF_SYSDATE(GETDATE()) AS DTS_INSERT  
						  FROM MA_CODEDTL CD WITH(NOLOCK)
						  LEFT JOIN MA_COMPANY MC ON MC.CD_COMPANY <> '{1}'
						  WHERE CD.CD_FIELD = '{0}'
						  AND CD.CD_COMPANY = '{1}'
						  
						  COMMIT";

				query = string.Format(query, this.쿼리.Text, Global.MainFrame.LoginInfo.CompanyCode);

				DBHelper.ExecuteScalar(query);

				this.ShowMessage(공통메세지._작업을완료하였습니다, this.DD("동기화"));
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn불용어제거_Click(object sender, EventArgs e)
		{
			string query;
			char[] splitChars = { ' ', '\n', '\r', '\t' };
			List<string> exceptString = new List<string>();

			try
			{
				query = @"SELECT LOWER(DC_STOP_WORD) AS DC_STOP_WORD 
						  FROM CZ_LN_STOP_WORD WITH(NOLOCK)";

				DataTable stopWord = DBHelper.GetDataTable(query);
				
				foreach(DataRow dr in stopWord.Rows)
				{
					exceptString.Add(dr["DC_STOP_WORD"].ToString());
				}


				//query = @"SELECT A.CD_COMPANY, A.NO_FILE, QH.NO_FILE, A.NO_LINE, A.NM_SUBJECT, A.CD_ITEM_PARTNER, A.NM_ITEM_PARTNER,
				//		  	     LOWER(ISNULL(QL.NM_SUBJECT, '') + ' ' + ISNULL(QL.CD_ITEM_PARTNER, '') + ' ' + ISNULL(QL.NM_ITEM_PARTNER, '')) AS DC_KEYWORD, 
				//		  	     ISNULL(NP.CD_NEW_PARTNER, A.CD_STOCK) AS CD_PO_PARTNER, MP.LN_PARTNER, A.CD_STOCK_RATE,
				//		  	     QL.CD_SUPPLIER, MP1.LN_PARTNER,
				//		  	     A.DTS_INSERT
				//		  FROM CZ_SA_QTN_PREREG A
				//		  JOIN CZ_SA_QTNH QH ON QH.CD_COMPANY = A.CD_COMPANY AND QH.NO_REF = A.NO_REF
				//		  JOIN CZ_SA_QTNL QL ON QL.CD_COMPANY = QH.CD_COMPANY AND QL.NO_FILE = QH.NO_FILE AND QL.NO_LINE = A.NO_LINE
				//		  LEFT JOIN CZ_LN_NEW_PARTNER NP ON NP.CD_OLD_PARTNER = A.CD_STOCK
				//		  LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = A.CD_COMPANY AND MP.CD_PARTNER = ISNULL(NP.CD_NEW_PARTNER, A.CD_STOCK)
				//		  LEFT JOIN MA_PARTNER MP1 ON MP1.CD_COMPANY = QL.CD_COMPANY AND MP1.CD_PARTNER = QL.CD_SUPPLIER
				//		  WHERE QH.NO_FILE LIKE '%19%'
				//		  AND ISNULL(A.NO_REF, '') <> ''
				//		  AND QL.CD_SUPPLIER IS NOT NULL
				//		  AND A.DTS_INSERT >= '20191115103716'
				//		  AND ISNULL(NP.CD_NEW_PARTNER, A.CD_STOCK) <> QL.CD_SUPPLIER
				//		  ORDER BY A.DTS_INSERT DESC";

				query = @"SELECT TOP 200000
								 LOWER(ISNULL(QL.NM_SUBJECT, '') + ' ' + ISNULL(QL.CD_ITEM_PARTNER, '') + ' ' + ISNULL(QL.NM_ITEM_PARTNER, '')) AS DC_KEYWORD 
						  FROM CZ_SA_QTNL QL WITH(NOLOCK)
						  WHERE QL.CD_COMPANY = 'K100'
						  AND QL.CD_SUPPLIER IS NOT NULL";

				DataTable keyWord = DBHelper.GetDataTable(query);

				this.dgv결과1.DataSource = keyWord;

				var temp = keyWord.AsEnumerable().SelectMany(x => Regex.Replace(x["DC_KEYWORD"].ToString(), "[^a-zA-Z0-9]", " ").Split(splitChars, StringSplitOptions.RemoveEmptyEntries))
												 .Except(exceptString)
												 .GroupBy(x => x, y => y, (x, y) => new { key = x, count = y.Count() })
												 .OrderByDescending(x => x.count);

				DataTable dt = new DataTable();
				dt.Columns.Add("KEYWORD");
				dt.Columns.Add("COUNT");

				DataRow newRow;

				foreach(var a in temp)
				{
					newRow = dt.NewRow();
					newRow["KEYWORD"] = a.key.ToString();
					newRow["COUNT"] = a.count.ToString();
					dt.Rows.Add(newRow);
				}

				this.dgv결과2.DataSource = dt;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void cur원금액_DecimalValueChanged(object sender, EventArgs e)
		{
			try
			{
				this.cur변환금액.DecimalValue = Unit.외화금액(DataDictionaryTypes.CZ, this.cur원금액.DecimalValue);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void ctxEWS테스트_QueryAfter(object sender, BpQueryArgs e)
		{
			string 경고문구 = string.Empty;
			string 제외여부 = string.Empty;
			string 지불조건제외여부 = string.Empty;
			WarningLevel warningLevel = WarningLevel.정상;

			try
			{
				EalryWarningSystem EWS = new EalryWarningSystem();
				EWS.미수금확인(e.CodeValue, ref warningLevel, ref 경고문구, ref 제외여부, ref 지불조건제외여부);

				if (warningLevel == WarningLevel.사용불가)
				{
					this.ctxEWS테스트.CodeValue = string.Empty;
					this.ctxEWS테스트.CodeName = string.Empty;
				}

				if (!string.IsNullOrEmpty(경고문구))
					this.ShowMessage(경고문구);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

        private void btn사용자정보클라우독_Click(object sender, EventArgs e)
        {
            string query;
            DBMgr dbMgr;
            DataTable dt, dt1;
            DataRow[] dataRowArray;

            try
            {
                query = @"SELECT UserID, 
                          	     Name,
                                 Email,
                                 '' AS NO_EMP,
                                 '' AS DT_RETIRE
                          FROM PDiskUsers WITH(NOLOCK)";

                dbMgr = new DBMgr(DBConn.Cloudoc);
                dbMgr.Query = query;
                dt = dbMgr.GetDataTable();

                query = @"SELECT ME.NO_EMP, 
                                 ISNULL(ME.DT_RETIRE, '00000000') AS DT_RETIRE
						  FROM MA_EMP ME WITH(NOLOCK)
						  WHERE CD_COMPANY IN ('K100', 'K200', 'S100')";

                dbMgr = new DBMgr(DBConn.iU);
                dbMgr.Query = query;
                dt1 = dbMgr.GetDataTable();

                foreach (DataRow dr in dt.Rows)
                {
                    dataRowArray = dt1.Select("NO_EMP = '" + dr["UserID"].ToString().ToUpper() + "'");

                    if (dataRowArray != null && dataRowArray.Length > 0)
                    {
                        dr["NO_EMP"] = dataRowArray[0]["NO_EMP"].ToString();
                        dr["DT_RETIRE"] = dataRowArray[0]["DT_RETIRE"].ToString();

                        if (dr["DT_RETIRE"].ToString() == "00000000")
                            dr["DT_RETIRE"] = string.Empty;
                    }
                }

                this.결과창.DataSource = dt;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

		private void btnPythonTest_Click(object sender, EventArgs e)
		{
			Dintec.Python python = new Dintec.Python();

			DataTable dt, dt1;

			dt = Global.MainFrame.FillDataTable(@"SELECT CD_COMPANY, NO_FILE 
												  FROM CZ_DXITEM_INQ_TEST WITH(NOLOCK)
												  WHERE YN_TRAIN = 'N'
											      AND CD_SUPPLIER6 IS NULL
												  GROUP BY CD_COMPANY, NO_FILE");

			foreach (DataRow dr in dt.Rows)
			{
				dt1 = python.FindSupplier(dr["CD_COMPANY"].ToString(), dr["NO_FILE"].ToString());

				if (dt1 == null) continue;

				foreach (DataRow dr1 in dt1.Rows)
				{
					Global.MainFrame.ExecuteScalar(@"UPDATE CZ_DXITEM_INQ_TEST
													 SET CD_SUPPLIER = '" + dr1["CD_SUPPLIER"].ToString() + "'," + Environment.NewLine +
													"	 RT_SUPPLIER = '" + dr1["RATE"].ToString() + "'" + Environment.NewLine +
													"WHERE CD_COMPANY = '" + dr["CD_COMPANY"].ToString() + "'" + Environment.NewLine +
													"AND NO_FILE = '" + dr["NO_FILE"].ToString() + "'" + Environment.NewLine +
													"AND NO_LINE = '" + dr1["NO_LINE"].ToString() + "'");
				}
			}

			//DataTable dt1 = python.FindStock(Global.MainFrame.LoginInfo.CompanyCode, "BE18000013");
		}

		private void btn메일등록_Click(object sender, EventArgs e)
		{
			string 파일번호;

			AutoInquiry inquiry = new AutoInquiry();

			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Multiselect = true;
			openFileDialog.RestoreDirectory = true;

			if (openFileDialog.ShowDialog() != DialogResult.OK) return;

			string[] fileNames = openFileDialog.FileNames;

			foreach (string fileName in fileNames)
			{
				파일번호 = inquiry.SaveEmail("K100", string.Empty, "BE", "01", "S-391", string.Empty, fileName);
				inquiry.SaveEmail("K100", 파일번호, string.Empty, "04", string.Empty, "00000", fileName);
				inquiry.SaveEmail("K100", 파일번호, string.Empty, "08", string.Empty, string.Empty, fileName);
			}
		}

		private void btn첨부파일분리_Click(object sender, EventArgs e)
		{
			string query, image1, fileName, localpath;
			string[] separator, temp;
			List<string> imageList;

			try
			{
				FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
				if (folderBrowserDialog.ShowDialog() != DialogResult.OK) return;

				// 라이선스 인증
				Aspose.Email.License license = new Aspose.Email.License();
				license.SetLicense("Aspose.Email.lic");

				query = @"SELECT WL.CD_COMPANY,
							 WL.TP_STEP,
					  	     WL.NO_KEY,
						     WL.CD_SUPPLIER,
					  	     WL.NM_FILE_REAL,
							 WL.ID_INSERT,
					  	     WL.DTS_INSERT 
					  FROM CZ_MA_WORKFLOWL WL WITH(NOLOCK)
					  WHERE WL.CD_COMPANY = 'K100'
					  AND WL.TP_STEP = '04'
					  AND WL.CD_SUPPLIER IN 
					  (
						'00047',
						'05483',
						'00141',
						'00028',
						'00740',
						'00796',
						'04342',
						'00140',
						'00133',
						'04886',
						'11278',
						'03316',
						'00067',
						'09680',
						'03971',
						'05570'
					  )
					  AND WL.NM_FILE LIKE '%.MSG'
					  AND NOT EXISTS (SELECT 1 
					  				  FROM CZ_MA_WORKFLOWL WL1 WITH(NOLOCK)
					  				  WHERE WL1.CD_COMPANY = WL.CD_COMPANY
					  				  AND WL1.NO_KEY = WL.NO_KEY
					  				  AND WL1.TP_STEP = WL.TP_STEP 
					  				  AND WL1.YN_INCLUDED = 'Y') 
					  ORDER BY WL.DTS_INSERT DESC";

				DataTable dt = DBHelper.GetDataTable(query);

				Stopwatch time = new Stopwatch();
				time.Start();

				int index = 0;
				foreach (DataRow dr in dt.Rows)
				{
					MsgControl.ShowMsg("CZ_처리중입니다. 잠시만 기다려주세요. (@/@)", new string[] { D.GetString(++index), D.GetString(dt.Rows.Count) });

					try
					{
						FileMgr.Download_WF("K100", D.GetString(dr["NO_KEY"]), dr["NM_FILE_REAL"].ToString(), folderBrowserDialog.SelectedPath, false);

						#region MSG
						MapiMessage msg = MapiMessage.FromFile(folderBrowserDialog.SelectedPath + "\\" + dr["NM_FILE_REAL"].ToString());

						#region 본문 이미지 제거
						separator = new string[] { "cid:" };
						temp = msg.BodyHtml.Split(separator, StringSplitOptions.None);

						temp[0] = string.Empty;
						imageList = new List<string>();

						foreach (string image in temp)
						{
							if (string.IsNullOrEmpty(image))
								continue;

							image1 = image.Split('.')[0];

							if (!imageList.Contains(image1))
								imageList.Add(image1);
						}
						#endregion

						string filePath = folderBrowserDialog.SelectedPath + "\\";
						FileMgr.CreateDirectory(filePath);

						foreach (MapiAttachment item in msg.Attachments)
						{
							fileName = Regex.Replace(item.LongFileName, @"[^a-zA-Z0-9가-힣ㄱ-ㅎㅏ-ㅣ\[\]_ .]", string.Empty);
							FileInfo fileInfo = new FileInfo(fileName);

							if (imageList.Contains(fileInfo.Name.Replace(fileInfo.Extension, string.Empty))) continue;

							fileName = FileMgr.GetUniqueFileName(folderBrowserDialog.SelectedPath + "\\" + fileName);
							localpath = folderBrowserDialog.SelectedPath + "\\" + fileName;
							item.Save(localpath);

							this.AddDataRow(dr, localpath, new FileInfo(localpath), true);
						}
						#endregion
					}
					catch
					{
						continue;
					}
				}

				this.ShowMessage(time.Elapsed.ToString());


				//string query;

				//FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
				//if (folderBrowserDialog.ShowDialog() != DialogResult.OK) return;

				//query = @"SELECT MF.FILE_PATH,
				//				 ('Upload/P_CZ_MA_HULL/' + A.NO_IMO) AS FILE_PATH1,
				//		  	     A.NO_IMO + '_SUPPLIER_' + CONVERT(NVARCHAR, A.NO_TYPE) AS CD_FILE,
				//				 MF.CD_FILE AS CD_FILE1,
				//		  	     MF.FILE_NAME
				//		  FROM CZ_MA_HULL_SUPPLIER_TYPE A
				//		  JOIN MA_FILEINFO MF ON MF.CD_COMPANY = 'K100' AND MF.CD_MODULE = 'MA' AND MF.ID_MENU = 'P_CZ_MA_HULL' AND MF.CD_FILE = A.NO_IMO + '_' + A.CD_SUPPLIER + '_' + CONVERT(NVARCHAR, A.NO_TYPE)";

				//   DataTable dt = DBHelper.GetDataTable(query);

				//WebClient wc = new WebClient();
				//foreach (DataRow dr in dt.Rows)
				//{
				//	try
				//	{
				//		wc.DownloadFile(Global.MainFrame.HostURL + "/" + dr["FILE_PATH"].ToString() + "/" + dr["CD_FILE"].ToString() + "/" + dr["FILE_NAME"].ToString(), folderBrowserDialog.SelectedPath + @"\" + dr["FILE_NAME"].ToString());
				//		FileUploader.UploadFile(dr["FILE_NAME"].ToString(), folderBrowserDialog.SelectedPath + @"\" + dr["FILE_NAME"].ToString(), "", dr["FILE_PATH1"].ToString() + "/" + dr["CD_FILE1"].ToString());
				//		FileUploader.DeleteFile(dr["FILE_PATH"].ToString(), dr["CD_FILE"].ToString(), dr["FILE_NAME"].ToString());
				//	}
				//	catch
				//	{
				//		continue;
				//	}	
				//}
			}
			catch
			{
				return;
			}
			finally
			{
				MsgControl.CloseMsg();
			}
		}

		private void AddDataRow(DataRow dr, string 파일경로, FileInfo 파일정보, bool 첨부여부)
		{
			string 파싱가능여부 = "N", query;

			try
			{
				#region 파싱가능여부
				if (dr["TP_STEP"].ToString() == "01" && 첨부여부 == false)
				{
					try
					{
						InquiryParser parser = new InquiryParser(파일경로);

						if (parser.Parse(false) == true)
							파싱가능여부 = "Y";
						else
							파싱가능여부 = "N";
					}
					catch
					{
						파싱가능여부 = "N";
					}
				}
				else if (dr["TP_STEP"].ToString() == "04" && 첨부여부 == false)
				{
					try
					{
						if (QuotationFinder.IsPossible(dr["CD_COMPANY"].ToString(), dr["CD_SUPPLIER"].ToString(), 파일정보.FullName) == true)
							파싱가능여부 = "Y";
						else
							파싱가능여부 = "N";
					}
					catch
					{
						파싱가능여부 = "N";
					}
				}
				#endregion

				query = @"DECLARE @V_NO_LINE INT

SELECT @V_NO_LINE = ISNULL(MAX(NO_LINE), 0) + 1
FROM CZ_MA_WORKFLOWL WITH(NOLOCK)
WHERE CD_COMPANY = '{0}'
AND TP_STEP = '{1}'
AND NO_KEY = '{2}'

INSERT INTO CZ_MA_WORKFLOWL
(
	CD_COMPANY,
	TP_STEP,
	NO_KEY,
	NO_LINE,
	CD_SUPPLIER,
	NM_FILE,
	NM_FILE_REAL,
	YN_DONE,
	YN_PARSING,
	YN_INCLUDED,
	ID_INSERT,
	DTS_INSERT
)
VALUES
(
	'{0}',
	'{1}',
	'{2}',
	@V_NO_LINE,
	'{5}',
	'{3}',
	'{4}',
	'N',
	'{6}',
	'{7}',
	'{8}',
	'{9}'
)";

				query = string.Format(query, new object[] { dr["CD_COMPANY"].ToString(),
															dr["TP_STEP"].ToString(),
															dr["NO_KEY"].ToString(),
															파일정보.Name,
															FileMgr.Upload_WF(dr["CD_COMPANY"].ToString(), dr["NO_KEY"].ToString(), 파일정보.FullName, false),
															dr["CD_SUPPLIER"].ToString(),
															파싱가능여부,
															(첨부여부 == true ? "Y" : "N"),
															dr["ID_INSERT"].ToString(),
															dr["DTS_INSERT"].ToString() });

				DBHelper.ExecuteScalar(query);
			}
			catch (Exception)
			{
				return;
			}
		}
		#endregion

		#region 기타 메소드
		private string EncryptString(string data, string key)
		{
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			byte[] bytes1 = Encoding.Unicode.GetBytes(data);
			byte[] bytes2 = Encoding.ASCII.GetBytes(key.Length.ToString());
			PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(key, bytes2);
			ICryptoTransform encryptor = rijndaelManaged.CreateEncryptor(passwordDeriveBytes.GetBytes(32), passwordDeriveBytes.GetBytes(16));
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
			cryptoStream.Write(bytes1, 0, bytes1.Length);
			cryptoStream.FlushFinalBlock();
			byte[] inArray = memoryStream.ToArray();
			memoryStream.Close();
			cryptoStream.Close();
			return Convert.ToBase64String(inArray);
		}

		private string DecryptString(string data, string key)
		{
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			byte[] buffer = Convert.FromBase64String(data);
			byte[] bytes = Encoding.ASCII.GetBytes(key.Length.ToString());
			PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(key, bytes);
			ICryptoTransform decryptor = rijndaelManaged.CreateDecryptor(passwordDeriveBytes.GetBytes(32), passwordDeriveBytes.GetBytes(16));
			MemoryStream memoryStream = new MemoryStream(buffer);
			CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
			byte[] numArray = new byte[buffer.Length];
			int count = cryptoStream.Read(numArray, 0, numArray.Length);
			memoryStream.Close();
			cryptoStream.Close();
			return Encoding.Unicode.GetString(numArray, 0, count);
		}

		private void 실행(string text)
		{
			try
			{
				if (this.chk쿼리로보기.Checked == true)
					this.쿼리.Text = text;
				else
					this.결과창.DataSource = Global.MainFrame.FillDataTable(text);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		public DataTable OpenQuery(string DB유형, string Query)
		{
			DataTable dt = null;
			string 연결된서버;

			try
			{
				연결된서버 = string.Empty;

				switch (DB유형)
				{
					case "000":
						연결된서버 = "DINTEC";
						break;
					case "001":
						연결된서버 = "DUBHECO";
						break;
					case "002":
						연결된서버 = "HEESC";
						break;
					case "008":
						연결된서버 = "MES";
						break;
				}

				if (string.IsNullOrEmpty(연결된서버))
					return null;
				else
					return DBHelper.GetDataTable("SELECT * FROM OPENQUERY(" + 연결된서버 + ", '" + Query.Replace("'", "''") + "')");
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}

			return dt;
		}

		public DataTable DirectQuery(string DB유형, string Query)
		{
			DataTable dt = null;
			DBMgr dbMgr;

			try
			{
				dbMgr = new DBMgr((DBConn)D.GetInt(DB유형));
				dbMgr.Query = Query;
				return dbMgr.GetDataTable();
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}

			return dt;
		}
		#endregion
	}
}
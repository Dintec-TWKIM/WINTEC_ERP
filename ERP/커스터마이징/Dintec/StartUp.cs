using Duzon.Common.Forms;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;

namespace Dintec
{
	public class StartUp
	{
		private static bool isFirst = false;

		public static void Certify(PageBase form)
		{
			string companyCode;
			string bizCode;
			string language;
			string deptCode;
			string empNumber;


			try
			{
				// 견적등록 ROOT 폴더에서 삭제
				//string path = Path.Combine(@"C:\ERPU\Browser", "P_CZ_SA_QTN_REG.dll");
				//System.IO.FileInfo fi = new System.IO.FileInfo(path);
				//if (fi != null)
				//	fi.Delete();
			}
			catch (Exception e)
			{

			}



			// ********** 폰트 체크
			string fontName = "Agency FB";
			Font font = new Font(fontName, 12);

			if (font.Name != fontName)
			{
				bool added = false;

				// 파일 복사
				string fontFile1 = "AGENCYR.TTF";
				string fontFile2 = "AGENCYB.TTF";

				string pathFile1 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Fonts", fontFile1);
				string pathFile2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Fonts", fontFile2);

				if (!File.Exists(pathFile1))
				{
					added = true;
					File.Copy(Application.StartupPath + @"\" + fontFile1, pathFile1);
				}

				if (!File.Exists(pathFile2))
				{
					added = true;
					File.Copy(Application.StartupPath + @"\" + fontFile2, pathFile2);
				}

				// 레지스트리 등록
				string regName1 = "Agency FB (TrueType)";
				string regName2 = "Agency FB Bold (TrueType)";

				RegistryKey key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts");

				if (key.GetValue(regName1) == null)
				{
					added = true;
					key.SetValue(regName1, fontFile1);
				}

				if (key.GetValue(regName2) == null)
				{
					added = true;
					key.SetValue(regName2, fontFile2);
				}

				key.Close();

				if (added)
					Util.ShowMessage("폰트설치가 완료되었습니다.\nERP 재시작 바랍니다!!");
			}

			companyCode = Global.MainFrame.LoginInfo.CompanyCode;
			bizCode = Global.MainFrame.LoginInfo.BizAreaCode;
			language = Global.MainFrame.LoginInfo.Language;
			deptCode = Global.MainFrame.LoginInfo.DeptCode;
			empNumber = Global.MainFrame.LoginInfo.UserID;

			// 루트에 있어 삭제되어야 할 Dll
			//string path = Path.Combine(Application.StartupPath, "P_CZ_HR_OVERTIME_GW.dll");
			//FileInfo fi = new FileInfo(path);
			//fi.Delete();

			// 클라우독 체크
			if (!CheckCloudoc())
				form.Dispose();

			// ********** MAC Address 검증
			if (!CheckMacAddress())
				form.Dispose();

			// 서버키 인증
			if (!Global.MainFrame.ServerKey.In("DINTEC", "DINTEC2"))
				form.Dispose();

			// 로그 저장 이벤트
			form.Load += Form_Load;
			form.SearchButtonClick += Form_SearchButtonClick;
			form.AddButtonClick += Form_AddButtonClick;
			form.DeleteButtonClick += Form_DeleteButtonClick;
			form.SaveButtonClick += Form_SaveButtonClick;
			form.PrintButtonClick += Form_PrintButtonClick;
			form.ExitButtonClick += Form_ExitButtonClick;
		}
		
		private static bool CheckCloudoc()
		{
			string empNumber = Global.MainFrame.LoginInfo.UserID;

			string[] exception = {
				"S-343"
			,   "S-347"	// 이정철
			,   "S-279"	// 문명국
			,   "S-250"	// 마대준
			,   "S-267"	// 신승엽
			,   "S-223"	// 김철영
			,   "S-391"	// 김기현
			,   "S-458"	// 하상원
			,   "S-332"	// 김진아
			,   "D-038"	// 최규대
			,   "S-373" // 최정환
			,   "SYSADMIN"// 시스템관리자
			,   "S-231D" // 신동영
			,   "S-576"  // 김태완
			,   "S-220"  // 손성헌
			};

			if (Global.MainFrame.LoginInfo.CompanyCode != "W100" && empNumber.Left(1).In("S", "D") && !empNumber.In(exception) && Process.GetProcessesByName("PlusDrive").Length < 1)
				return false;
			else
				return true;
			//if (empNumber.Left(1).In("S", "D") && Process.GetProcessesByName("PlusDrive").Length < 1 && empNumber != "S-293" && empNumber != "S-002" &&
			//	empNumber != "S-347" && empNumber != "S-343" && empNumber != "S-279" && empNumber != "S-250" && empNumber != "S-305" && empNumber != "S-019" &&
			//	empNumber != "S-231D" && empNumber != "S-267" && empNumber != "S-223" && empNumber != "S-278" && empNumber != "S-391" && empNumber != "S-458"
			//	&& empNumber != "D-004A" && empNumber != "D-024" && empNumber != "D-038" && empNumber != "S-332")

		}

		private static bool CheckMacAddress()
		{			
			foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
			{
				//if (nic.NetworkInterfaceType != NetworkInterfaceType.Ethernet) continue;

				// esvpn인 경우 업링크 죽어버림..
				//if (nic.OperationalStatus == OperationalStatus.Up)
				//{
				string mac = nic.GetPhysicalAddress().ToString();

				// MAC 검증
				string query = @"
SELECT
	1
FROM MA_CODEDTL
WHERE 1 = 1
	--AND CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + @"'
	AND CD_FIELD = 'CZ_MA00014'
	--AND CD_SYSDEF = '" + Global.MainFrame.LoginInfo.UserID.Right(3) + @"'
	AND (  REPLACE(CD_FLAG1, '-', '') = '" + mac + @"'
		OR REPLACE(CD_FLAG2, '-', '') = '" + mac + @"'
		OR REPLACE(CD_FLAG3, '-', '') = '" + mac + "')";

				DataTable dt = DBMgr.GetDataTable(query);

				if (dt.Rows.Count > 0)
					return true;				
			}

			return false;

		}

		private static void Form_Load(object sender, EventArgs e)
		{
			SaveActionLog("3");
		}

		private static void Form_SearchButtonClick(object sender, EventArgs e)
		{
			SaveActionLog("7");
		}

		private static void Form_AddButtonClick(object sender, EventArgs e)
		{
			SaveActionLog("8");
		}

		private static void Form_DeleteButtonClick(object sender, EventArgs e)
		{
			SaveActionLog("9");
		}

		private static void Form_SaveButtonClick(object sender, EventArgs e)
		{
			SaveActionLog("10");
		}

		private static void Form_PrintButtonClick(object sender, EventArgs e)
		{
			SaveActionLog("11");
		}

		private static void Form_ExitButtonClick(object sender, EventArgs e)
		{
			SaveActionLog("12");
		}

		private static void SaveActionLog(string flag)
		{
			string companyCode = Global.MainFrame.LoginInfo.CompanyCode;
			string userId = Global.MainFrame.LoginInfo.UserID;
			string pageId = Global.MainFrame.CurrentPageID;
			string pageName = Global.MainFrame.CurrentPageName;

			// 저장
			DBMgr dbm = new DBMgr
			{
				Procedure = "PX_CZ_MA_USER_LOG"
			};
			dbm.AddParameter("@CD_COMPANY"	, companyCode);
			dbm.AddParameter("@ID_USER"		, userId);
			dbm.AddParameter("@PAGE_ID"		, pageId);
			dbm.AddParameter("@PAGE_NAME"	, pageName);
			dbm.AddParameter("@FLAG"		, flag);
			dbm.AddParameter("@IP"		, Util.GetLocalIpAddress());
			dbm.ExecuteNonQuery();

			// 시간 팝업
			if (Process.GetProcessesByName("NeoWeb").Length == 1)
				isFirst = true;

			if (isFirst)
			{
				if (Application.OpenForms["H_CZ_WORKING_HOURS"] == null)
				{
					// 대상자만 팝업함
					string query = @"
SELECT 1
FROM V_CZ_MA_CODEDTL
WHERE 1 = 1
	AND CD_COMPANY = '" + companyCode + @"'
	AND CD_FIELD = 'CZ_HR00008'
	AND CD_SYSDEF = '" + userId + "'";

					if (DBMgr.GetDataTable(query).Rows.Count == 1)
					{
						H_CZ_WORKING_HOURS f = new H_CZ_WORKING_HOURS();
						f.StartPosition = FormStartPosition.CenterParent;
						f.Show();
					}
				}
				else
				{
					((H_CZ_WORKING_HOURS)Application.OpenForms["H_CZ_WORKING_HOURS"]).ShowWorkingHours();
				}
			}
		}
	}
}

using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Duzon.Common.Forms;

namespace Dintec
{
	public static class PATH
	{
		public static void CreateDirectory(string path)
		{
			Directory.CreateDirectory(path);
		}

		public static string GetPath(object fullPathFile)
		{
			string path = fullPathFile.ToString();

			if (path.IndexOf(@"\") >= 0) 		return path.Substring(0, path.LastIndexOf(@"\"));
			else if (path.IndexOf("/") >= 0)	return path.Substring(0, path.LastIndexOf("/"));
			
			return "";
		}

		public static string GetTempPath()
		{
			string path = Application.StartupPath + @"\temp";
			Directory.CreateDirectory(path);
			return path;
		}

		public static string GetDesktopPath()
		{
			return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
		}

		

		public static string GetWorkFlowPath(string fileNumber)
		{
			return GetWorkFlowPath(Global.MainFrame.LoginInfo.CompanyCode, fileNumber);
		}

		public static string GetWorkFlowPath(string companyCode, string fileNumber)
		{
			string query = "SELECT YYYY FROM V_CZ_MA_WORKFLOWH WITH(NOLOCK) WHERE CD_COMPANY = '" + companyCode + "' AND NO_FILE = '" + fileNumber + "' ORDER BY TP_STEP";
			DataTable dt = SQL.GetDataTable(query);
			string yyyy;

			if (dt.Rows.Count > 0)
				yyyy = CT.String(dt.Rows[0]["YYYY"]);
			else
				yyyy = "20" + Regex.Match(fileNumber, @"\d+").Value.Left(2);

			return "WorkFlow/" + companyCode + "/" + yyyy + "/" + fileNumber;
		}
	}
}

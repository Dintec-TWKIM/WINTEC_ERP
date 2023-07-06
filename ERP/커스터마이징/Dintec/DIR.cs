using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Dintec
{
	public static class DIR
	{
        public static void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public static string GetPath(string fullPathFile)
        {
            return fullPathFile.Substring(0, fullPathFile.LastIndexOf(@"\"));
        }

        public static string GetTempPath()
        {
            string path = Application.StartupPath + @"\temp";
            Directory.CreateDirectory(path);
            return path;
        }

        public static string GetServerPathWF(string companyCode, string fileNumber)
        {
            string query = "SELECT DTS_INSERT FROM CZ_MA_WORKFLOWH WITH(NOLOCK) WHERE CD_COMPANY = '" + companyCode + "' AND NO_KEY = '" + fileNumber + "' ORDER BY TP_STEP";
            DataTable dt = SQL.GetDataTable(query);
            string yyyy;

            if (dt.Rows.Count > 0)
                yyyy = SQL.GetDataTable(query).Rows[0]["DTS_INSERT"].Left(4);
            else
                yyyy = "20" + Regex.Match(fileNumber, @"\d+").Value.Left(2);
            
            return "WorkFlow/" + companyCode + "/" + yyyy + "/" + fileNumber;
        }

		public static void Rename(string sourceDirName, string destDirName)
		{
			Directory.Move(sourceDirName, destDirName);
		}

		public static void RenameWF(string companyCode, string sourceFileNumber, string destFileNumber, DataTable fileList)
		{
            foreach (DataRow row in fileList.Rows)
            {
                string sourceFileName = (string)row["NM_FILE_REAL"];
                string destFileName = FILE.DownloadWF(companyCode, sourceFileNumber, sourceFileName, false);
                FILE.UploadWF(companyCode, destFileNumber, destFileName, sourceFileName);   // 원본이름으로 올라가도록 해줌
                FILE.DeleteWF(companyCode, sourceFileNumber, sourceFileName);
            }                            
        }
    }
}

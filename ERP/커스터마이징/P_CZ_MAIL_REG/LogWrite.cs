using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cz
{
	class LogWrite
	{
		public void WriteText(string txtLog)
		{
			string folder = Application.StartupPath + @"MailLog";

			DirectoryInfo dirinfo = new DirectoryInfo(folder);
			if (!dirinfo.Exists) dirinfo.Create();

			string txtFileName = folder + @"\Mail_Log_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";

			FileStream fileStream = new FileStream(txtFileName, FileMode.Append, FileAccess.Write);
			StreamWriter streamWriter = new StreamWriter(fileStream, System.Text.Encoding.Default);

			streamWriter.Write(String.Format("[{0}] ", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")));
			streamWriter.WriteLine(txtLog);
			streamWriter.Flush();
			streamWriter.Close();
			fileStream.Close();
		}
	}
}

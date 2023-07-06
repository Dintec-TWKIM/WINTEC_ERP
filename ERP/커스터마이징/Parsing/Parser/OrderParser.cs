using Parsing.Parser.Order;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Parsing.Parser
{
	public class OrderParser
	{
		string fileName = string.Empty;
		string FileNameOnly = string.Empty;
		string returnStr = string.Empty;
		DataTable dtItem;

		public DataTable Item
		{
			get
			{
				return dtItem;
			}
		}


		public OrderParser()
		{
			dtItem = new DataTable();
		}


		public string Parse()
		{
			string FolderName = "C:\\HIPRO_RPA\\";
			System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(FolderName);
			if (di.Exists)
			{
				foreach (System.IO.FileInfo File in di.GetFiles())
				{
					if (File.Extension.ToLower().CompareTo(".xls") == 0)
					{
						FileNameOnly = File.Name.Substring(0, File.Name.Length - 4);
						fileName = File.FullName;
					}
				}

				string extension = Path.GetExtension(fileName);


				// HHI
				if (extension.ToUpper().EndsWith(".XLS"))
				{
					if (FileNameOnly.StartsWith("HHI"))
					{
						HHIOrder_excel p = new HHIOrder_excel(fileName);
						p.Parse();

						dtItem = p.Item;

						if (dtItem != null)
						{
							DirectoryInfo dir = new DirectoryInfo(FolderName);
							System.IO.FileInfo[] files = dir.GetFiles("*.*", SearchOption.AllDirectories);

							foreach (System.IO.FileInfo file in files)
								file.Attributes = FileAttributes.Normal;

							Directory.Delete(FolderName, true);

							returnStr = "성공";
						}
						else
						{
							returnStr = "실패";
						}
					}
					else if (FileNameOnly.StartsWith("HGS"))
					{
						HHIOrder_excel p = new HHIOrder_excel(fileName);
						p.Parse();

						dtItem = p.Item;

						if (dtItem != null)
						{
							DirectoryInfo dir = new DirectoryInfo(FolderName);
							System.IO.FileInfo[] files = dir.GetFiles("*.*", SearchOption.AllDirectories);

							foreach (System.IO.FileInfo file in files)
								file.Attributes = FileAttributes.Normal;

							//Directory.Delete(FolderName, true);

							returnStr = "성공";
						}
						else
						{
							returnStr = "실패";
						}
					}
				}
				else if (extension.ToUpper().EndsWith(".PDF"))
				{
					// 없음.
				}
			}
			else
			{



				FolderName = "C:\\STX_RPA\\";
				di = new System.IO.DirectoryInfo(FolderName);

				if (di.Exists)
				{
					foreach (System.IO.FileInfo File in di.GetFiles())
					{
						if (File.Extension.ToLower().CompareTo(".csv") == 0)
						{
							FileNameOnly = File.Name.Substring(0, File.Name.Length - 4);
							fileName = File.FullName;

						}
					}

					string extension = Path.GetExtension(fileName);


					if (extension.ToUpper().EndsWith(".CSV"))
					{
						if (fileName.Contains("STX2"))
						{
							STXOrder_csv2 p = new STXOrder_csv2(fileName);
							p.Parse();

							dtItem = p.Item;
						}
						else
						{
							STXOrder_csv p = new STXOrder_csv(fileName);
							p.Parse();

							dtItem = p.Item;
						}

						if (dtItem != null)
						{
							DirectoryInfo dir = new DirectoryInfo(FolderName);
							System.IO.FileInfo[] files = dir.GetFiles("*.*", SearchOption.AllDirectories);

							foreach (System.IO.FileInfo file in files)
								file.Attributes = FileAttributes.Normal;

							//Directory.Delete(FolderName, true);

							returnStr = "성공";
						}
						else
						{
							returnStr = "실패";
						}
					}
				}
			}

			return returnStr;
		}
	}
}

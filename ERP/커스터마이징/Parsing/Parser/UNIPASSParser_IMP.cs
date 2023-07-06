using Parsing.Parser.UNIPASS;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Parsing.Parser
{
	public class UNIPASSParser_IMP
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


		public UNIPASSParser_IMP()
		{
			dtItem = new DataTable();

			dtItem.Columns.Add("NO_TAX");           // 신고번호	
			dtItem.Columns.Add("NO_IMPORT");        // 수입신고번호
			dtItem.Columns.Add("DC_TAX");           // 신고인기재란
		}


		public string Parse()
		{
			string FolderName = "C:\\UNIPASS\\";
			System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(FolderName);
			if (di.Exists)
			{
				foreach (System.IO.FileInfo File in di.GetFiles())
				{
					if (File.Extension.ToLower().CompareTo(".pdf") == 0)
					{

						UNIPASS_2 p = new UNIPASS_2(File.FullName);
						p.Parse();

					}
				}

				returnStr = "성공";

			}

			return returnStr;

		}
	}
}

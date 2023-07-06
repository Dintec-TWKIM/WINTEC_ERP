using Parsing.Parser.UNIPASS;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Parsing.Parser
{
	public class UNIPASSParser
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


		public UNIPASSParser()
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

						UNIPASS_1 p = new UNIPASS_1(File.FullName);
						p.Parse();

						dtItem.Rows.Add();
						dtItem.Rows[dtItem.Rows.Count - 1]["NO_TAX"] = p.Item.Rows[0]["NO_TAX"];
						dtItem.Rows[dtItem.Rows.Count - 1]["NO_IMPORT"] = p.Item.Rows[0]["NO_IMPORT"];
						dtItem.Rows[dtItem.Rows.Count - 1]["DC_TAX"] = p.Item.Rows[0]["DC_TAX"];
					}
				}

				returnStr = "성공";

			}

			return returnStr;

		}
	}
}

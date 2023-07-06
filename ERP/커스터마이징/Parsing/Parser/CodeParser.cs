using Dintec;
using System.Data;
using System.IO;

namespace Parsing
{
	public class CodeParser
	{
		private string fileName = string.Empty;

		private DataTable dtItem;
		private DataTable dtItemAll;
		private string pdfText;

		public DataTable Item
		{
			get
			{
				return dtItem;
			}
		}

		public DataTable ItemAll
		{
			get
			{
				return dtItemAll;
			}
		}

		public string textpdf
		{
			get
			{
				return pdfText;
			}
		}

		public CodeParser(string fileName, int pageNo)
		{
			dtItem = new DataTable();
			dtItemAll = new DataTable();
			this.fileName = fileName;

			string extension = Path.GetExtension(fileName);

			if (extension.ToUpper().ToString().Equals(".PDF"))
			{
				string text = PdfReader.ToText(fileName);

				//string text2 = PdfReader.GetText(fileName);
				// 지금은 텍스트 사용 안함.
				//pdfText = text2;

				if (text.StartsWith("MAN B&W"))
				{
					Doosan_1 p = new Doosan_1(fileName);
					p.Parse();

					dtItem = p.Item;
				}
				else if (text.Contains("Dongnam Marine Crane"))
				{
					sangsangin_1 p = new sangsangin_1(fileName, pageNo);
					p.Parse();

					dtItem = p.Item;
					dtItemAll = p.ItemAll;
				}
				else if (text.Contains("hanla"))
				{
					Parsing.Parser.Inquiry.Hanla_01 p = new Parser.Inquiry.Hanla_01(fileName, pageNo);
					p.Parse();

					dtItem = p.Item;
					dtItemAll = p.ItemAll;
				}
				else
				{
					sangsangin_1 p = new sangsangin_1(fileName, pageNo);
					p.Parse();

					dtItem = p.Item;
					dtItemAll = p.ItemAll;
					//Doosan_2 p = new Doosan_2(fileName);
					//p.Parse();

					//dtItem = p.Item;
				}
			}
			else if (extension.ToUpper().Equals(".XLSX") || extension.ToUpper().Equals(".XLS"))
			{
				DataSet ds = ExcelReader.ToDataSet(fileName);

				//int rowCount = ds.Tables[0].Rows.Count;
				//int colCount = ds.Tables[0].Columns.Count;

				sangsangin_excel p = new sangsangin_excel(fileName);
				p.Parse();

				dtItem = p.Item;
				dtItemAll = p.ItemAll;
			}
		}
	}
}
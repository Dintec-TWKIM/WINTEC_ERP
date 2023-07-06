using Bytescout.PDFExtractor;
using SautinSoft;
using System.Data;
using System.IO;

namespace Dintec
{
	public class PdfReader
	{

		/// <summary>
		/// ByteScout PDF Extractor (1000$)
		/// </summary>		
		public static string GetXml(string fileName)
		{
			XMLExtractor extractor = new XMLExtractor();
			extractor.RegistrationName = "dykim@dintec.co.kr";
			extractor.RegistrationKey = "6585-166F-FF29-D53B-F0C8-F2DF-0DA";

			extractor.LoadDocumentFromFile(fileName);
			string xml = extractor.GetXML();
			xml = xml.Replace("ﬁ", "fi");

			extractor.Dispose();

			return xml;
		}

		public static string GetXml(string fileName, int startPage, int endPage)
		{			
			XMLExtractor extractor = new XMLExtractor();
			extractor.RegistrationName = "dykim@dintec.co.kr";
			extractor.RegistrationKey = "6585-166F-FF29-D53B-F0C8-F2DF-0DA";

			extractor.LoadDocumentFromFile(fileName);
			string xml = extractor.GetXML(startPage - 1, endPage - 1);
			xml = xml.Replace("ﬁ", "fi");

			extractor.Dispose();

			return xml;
		}

		public static string GetText(string fileName)
		{
			TextExtractor extractor = new TextExtractor();
			extractor.RegistrationName = "dykim@dintec.co.kr";
			extractor.RegistrationKey = "6585-166F-FF29-D53B-F0C8-F2DF-0DA";

			extractor.LoadDocumentFromFile(fileName);
			string text = extractor.GetText();

			extractor.Dispose();

			return text;
		}

		public static string GetExcel(string fileName)
		{
			string _filename = fileName.Replace(".pdf", ".xls");
			XLSExtractor extractor = new XLSExtractor();
			extractor.RegistrationName = "dykim@dintec.co.kr";
			extractor.RegistrationKey = "6585-166F-FF29-D53B-F0C8-F2DF-0DA";

			extractor.LoadDocumentFromFile(fileName);
			extractor.SaveToXLSFile(_filename);


			extractor.Dispose();


			return _filename;
		}



		/// <summary>`
		/// SautinSoft PDF Focus (500$)
		/// </summary>
		public static string ToExcel(string fileName)
		{
			string pdfFile = fileName;
			string xlsFile = Path.ChangeExtension(fileName, ".xls");

			PdfFocus focus = new PdfFocus();
			focus.Serial = "10234289374";
			focus.OpenPdf(pdfFile);

			int result = focus.ToExcel(xlsFile);
			focus.ClosePdf();

			if (result == 0)
				return xlsFile;
			else
				return "";
		}


		/////////////////////////////////////////////////////////////////////////////////////////////////////////


		/// <summary>
		/// SautinSoft PDF Focus (500$)
		/// </summary>
		public static string ToText(string fileName)
		{
			PdfFocus focus = new PdfFocus();
			focus.Serial = "10234289374";
			focus.OpenPdf(fileName);

			string text = focus.ToText();
			focus.ClosePdf();

			return text;
		}

		/// <summary>
		/// SautinSoft PDF Focus (500$)
		/// </summary>
		public static string ToXml(string fileName)
		{
			PdfFocus focus = new PdfFocus();
			focus.Serial = "10234289374";
			focus.OpenPdf(fileName);

			string xml = focus.ToXml();
			focus.ClosePdf();

			return xml;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static DataSet ToDataSet(string xml)
		{
			DataSet dsRt = new DataSet();

			// ********** ByteScout PDF Extractor (1000$)
			if (xml.IndexOf("<?xml") == 0)
			{
				string newPageXml = "";
				string newRowXml = "";
				string newColXml = "";
				int colNo = 1;
				int tableId = 1;

				// ***** page 태그 검색
				while (true)
				{
					// page 시작 지점 찾기 → "<page"
					int page_s = xml.IndexOf("<page");
					if (page_s == -1) break;

					// page 종료 지점 찾기 → "</page>" (7글자)
					int page_e = xml.IndexOf("</page>", page_s);

					// page xml 가져오기
					string pageXml = xml.Substring(page_s, page_e - page_s + 7);

					// ***** row 태그 검색
					newRowXml = "";

					while (true)
					{
						// row 시작지점 찾기 → "<row"
						int row_s = pageXml.IndexOf("<row");
						if (row_s == -1) break;

						// row 종료지점 찾기 → "</row>" (6글자)
						int row_e = pageXml.IndexOf("</row>", row_s);

						// row xml 가져오기
						string rowXml = pageXml.Substring(row_s, row_e - row_s + 6);

						// ***** column 태그 검색
						colNo = 1;
						newColXml = "";

						while (true)
						{
							// column 시작지점 찾기 → "<column"
							int column_s = rowXml.IndexOf("<column");
							if (column_s == -1) break;

							// column 종료지점 찾기 → "</column>" (9글자)
							int column_e = rowXml.IndexOf("</column>", column_s);


							// column xml 가져오기
							string columnXml = string.Empty;
							if (column_e < 0 || column_s < 0)
							{
								columnXml = rowXml;
							}
							else
							{
								columnXml = rowXml.Substring(column_s, column_e - column_s + 9);
							}



							if (!columnXml.Contains("<control"))
							{
								// ***** text 태그 검색
								// text 순수 값 시작지점 찾기 → "<text" 다음으로 오는 첫 ">"(&gt;) 의 다음 글자 부터
								int text_s = columnXml.IndexOf(">", columnXml.IndexOf("<text")) + 1;

								// text 종료지점 찾기 → "</text>"
								int text_e = columnXml.IndexOf("</text>");



								string text = string.Empty;
								if (column_e < 0 || column_s < 0)
								{
									text = columnXml;
								}
								else
								{
									text = columnXml.Substring(text_s, text_e - text_s).Trim();
								}
								// column 안에 text 값 가져오기
								newColXml += "\n" + "    <COL" + colNo + ">" + text + "</COL" + colNo + ">";
							}

							// 신규 컬럼 태그 생성

							colNo++;

							// 다음 검색대상
							rowXml = rowXml.Substring(column_e);
						}

						// 저장 및 다음 검색대상
						newRowXml += "\n  <ROW>" + newColXml + "\n  </ROW>";
						pageXml = pageXml.Substring(row_e + 6);
					}

					// 저장 및 다음 검색대상
					newPageXml = "\n<PAGE>" + newRowXml + "\n</PAGE>";
					xml = xml.Substring(page_e + 8);

					// ***** 만들어진 Xml Page로 아이템 DataTable을 만듬 (반드시 ROW 태그가 있어야 함)
					if (newPageXml.IndexOf("<ROW>") > 0)
					{
						DataSet ds = new DataSet();
						ds.ReadXml(new StringReader(newPageXml));

						// 테이블 이름 변경 (동일한 이름이 여러 테이블에 안들어감)
						ds.Tables[0].TableName = "TABLE" + tableId;
						tableId++;

						dsRt.Tables.Add(ds.Tables[0].Copy());
					}
				}
			}
			// ********** SautinSoft PDF Focus (500$)
			else
			{
				string newTableXml = "";
				string newRowXml = "";
				string newColXml = "";
				int colNo = 1;
				int tableId = 1;

				// ***** table 태그 검색
				while (true)
				{
					// table 시작 지점 찾기 → "<table"
					int table_s = xml.IndexOf("<table");
					if (table_s == -1) break;

					// table 종료 지점 찾기 → "</table>" (8글자)
					int table_e = xml.IndexOf("</table>", table_s);

					// table xml 가져오기
					string tableXml = xml.Substring(table_s, table_e - table_s + 8);

					// ***** row 태그 검색
					newRowXml = "";

					while (true)
					{
						// row 시작지점 찾기 → "<row"
						int row_s = tableXml.IndexOf("<row");
						if (row_s == -1) break;

						// row 종료지점 찾기 → "</row>" (6글자)
						int row_e = tableXml.IndexOf("</row>", row_s);

						// row xml 가져오기
						string rowXml = tableXml.Substring(row_s, row_e - row_s + 6);

						// ***** cell 태그 검색
						colNo = 1;
						newColXml = "";

						while (true)
						{
							// cell 시작지점 찾기 → "<cell"
							int cell_s = rowXml.IndexOf("<cell");
							if (cell_s == -1) break;

							// cell 종료지점 찾기 → "\r\n"
							int cell_e = rowXml.IndexOf("\r\n", cell_s);

							// cell xml 가져오기
							string cellXml = rowXml.Substring(cell_s, cell_e - cell_s);

							// cell 값 가져오기
							string value = "";

							if (cellXml.IndexOf("</cell>") > 0) // 종료 태그가 있는 경우만, 없으면 빈값임
							{
								int value_s = cellXml.IndexOf(">") + 1;
								int value_e = cellXml.IndexOf("</cell>");
								value = cellXml.Substring(value_s, value_e - value_s);
							}

							// cell 태그안에 colspan이 있는지 검사
							if (cellXml.IndexOf("colspan=") > 0)
							{
								// colspan이 있으면 있는것 만큼 컬럼 태그 생성
								int colspan_s = cellXml.IndexOf("\"") + 1;
								int colspan_e = cellXml.IndexOf("\"", colspan_s);
								int colspan = GetTo.Int(cellXml.Substring(colspan_s, colspan_e - colspan_s));

								for (int i = 1; i <= colspan; i++)
								{
									newColXml += "\n" + "    <COL" + colNo + ">" + ((i == 1) ? value : "") + "</COL" + colNo + ">";
									colNo++;
								}
							}
							else
							{
								// colspan이 없으면 1개만 컬럼 태그 생성
								newColXml += "\n" + "    <COL" + colNo + ">" + value + "</COL" + colNo + ">";
								colNo++;
							}

							// 다음 검색대상
							rowXml = rowXml.Substring(cell_e);
						}

						// 저장 및 다음 검색대상
						newRowXml += "\n  <ROW>" + newColXml + "\n  </ROW>";
						tableXml = tableXml.Substring(row_e + 6);
					}

					// 저장 및 다음 검색대상
					newTableXml = "\n<TABLE>" + newRowXml + "\n</TABLE>";
					xml = xml.Substring(table_e + 8);

					// ***** 만들어진 Xml Table로 아이템 DataTable을 만듬
					DataSet ds = new DataSet();
					ds.ReadXml(new StringReader(newTableXml));

					// 테이블 이름 변경 (동일한 이름이 여러 테이블에 안들어감)
					ds.Tables[0].TableName = "TABLE" + tableId;
					tableId++;

					dsRt.Tables.Add(ds.Tables[0].Copy());
				}
			}

			return dsRt;
		}
	}
}

using Aspose.Email.Outlook;
using MetroFramework;
using MetroFramework.Forms;
using OutParsing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace JeilMecha
{
	public partial class FileSearch : MetroForm
	{
		//서버계정 정보 : web / dintec5771
		private int _rowIndex;
		private bool isDebug = false;
		private string 아이피주소;

		public FileSearch()
		{
			InitializeComponent();

			if (this.isDebug == true)
				this.아이피주소 = "192.168.0.155";
			else
				this.아이피주소 = "192.168.0.2";

			this.InitGrid();
			this.InitEvent();
		}

		private void InitGrid()
		{
			#region 로그
			this._gridParsingLog.Columns.Add("FILE_NAME", "파일이름");
			this._gridParsingLog.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this._gridParsingLog.Columns[0].Width = 200;

			this._gridParsingLog.Columns.Add("ORD_NO", "문서번호");
			this._gridParsingLog.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this._gridParsingLog.Columns[1].Width = 200;

			this._gridParsingLog.Columns.Add("DC_LOG", "로그");
			this._gridParsingLog.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this._gridParsingLog.Columns[2].Width = 200;
			#endregion
		}

		private void InitEvent()
		{
			this.btn조회.Click += Btn조회_Click;
			this.btn파일업로드.Click += Btn파일업로드_Click;
			this.btn파일열기.Click += Btn파일열기_Click;
			this.btn파일보기.Click += Btn파일보기_Click;
			this.btn다운로드.Click += Btn다운로드_Click;

			this._gridParsingLog.CellClick += _gridParsingLog_CellClick;
			this._gridFileH.CellClick += _gridFileH_CellClick;
			this._gridFileL.CellClick += _gridFileL_CellClick;

			this.FormClosing += FileSearch_FormClosing;
		}

		private void Btn다운로드_Click(object sender, EventArgs e)
		{
			string 문서번호, 로컬경로, fileName;

			try
			{
				if (this._gridFileH.Rows.Count == 0) return;

				문서번호 = this._gridFileH.CurrentRow.Cells[0].Value.ToString();

				DBMgr dbMgr = new DBMgr(this.isDebug);
				DataTable dt = dbMgr.Select(string.Format("EXEC SP_CZ_MA_FILEINFOL_S '{0}'", 문서번호));

				List<DataRow> dataRowList = dt.AsEnumerable().Where(x => x["TP_FILE"].ToString() != "문의서").ToList();

				if (dataRowList.Count == 0)
				{
					MetroMessageBox.Show(this, "다운로드 받을 파일이 없습니다.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				WebClient wc = new WebClient();
				로컬경로 = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\문의서첨부\" + 문서번호;

				if (Directory.Exists(로컬경로))
					Directory.Delete(로컬경로, true);

				Directory.CreateDirectory(로컬경로);

				this.txt문서번호S1.Text = 문서번호;
				this.txt경로1.Text = string.Empty;
				this.txt경로2.Text = string.Empty;
				this.txt경로3.Text = string.Empty;
				this.txt경로4.Text = string.Empty;
				this.txt경로5.Text = string.Empty;

				for (int i = 0; i < dataRowList.Count; i++)
				{
					if (dataRowList[i]["TP_FILE"].ToString() == "문의서")
						continue;

					fileName = this.GetUniqueFileName(로컬경로 + @"\" + dataRowList[i]["FILE_NAME"].ToString());
					wc.DownloadFile("http://" + 아이피주소 + ":8080/Upload/" + dataRowList[i]["FILE_NAME"].ToString(), 로컬경로 + @"\" + fileName);

					switch (i)
					{
						case 0:
							this.txt경로1.Text = 로컬경로 + @"\" + fileName;
							break;
						case 1:
							this.txt경로2.Text = 로컬경로 + @"\" + fileName;
							break;
						case 2:
							this.txt경로3.Text = 로컬경로 + @"\" + fileName;
							break;
						case 3:
							this.txt경로4.Text = 로컬경로 + @"\" + fileName;
							break;
						case 4:
							this.txt경로5.Text = 로컬경로 + @"\" + fileName;
							break;
					}
				}

				MetroMessageBox.Show(this, "다운로드 완료", "정보", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MetroMessageBox.Show(this, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void _gridFileH_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				DBMgr dbMgr = new DBMgr(this.isDebug);

				this._gridFileL.DataSource = dbMgr.Select(string.Format("EXEC SP_CZ_MA_FILEINFOL_S '{0}'", this._gridFileH.CurrentRow.Cells[0].Value.ToString()));

				DataGridViewColumn column = this._gridFileL.Columns[0];
				column.Width = 70;
				column.HeaderText = "순번";
				column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

				column = this._gridFileL.Columns[1];
				column.Width = 100;
				column.HeaderText = "문서번호";
				column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

				column = this._gridFileL.Columns[2];
				column.Width = 100;
				column.HeaderText = "파일유형";
				column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

				column = this._gridFileL.Columns[3];
				column.Width = 100;
				column.HeaderText = "등록일자";
				column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

				column = this._gridFileL.Columns[4];
				column.Width = 200;
				column.HeaderText = "파일이름";
				column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

				this._rowIndex = -1;
				this.txt문서번호S1.Text = string.Empty;
				this.txt경로1.Text = string.Empty;
				this.txt경로2.Text = string.Empty;
				this.txt경로3.Text = string.Empty;
				this.txt경로4.Text = string.Empty;
				this.txt경로5.Text = string.Empty;
			}
			catch (Exception ex)
			{
				MetroMessageBox.Show(this, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void Btn파일열기_Click(object sender, EventArgs e)
		{
			string fileName;

			try
			{
				if (this._rowIndex < 0 || this._gridFileL.Rows.Count == 0) return;

				fileName = this._gridFileL.Rows[this._rowIndex].Cells[4].Value.ToString();

				WebClient wc = new WebClient();
				string 로컬경로 = @"C:\InqTemp\";

				Directory.CreateDirectory(로컬경로);
				wc.DownloadFile("http://" + 아이피주소 + ":8080/Upload/" + fileName, 로컬경로 + fileName);
				Process.Start(로컬경로 + fileName);
			}
			catch (Exception ex)
			{
				MetroMessageBox.Show(this, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void _gridParsingLog_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (e.RowIndex < 0 || this._gridParsingLog.Rows.Count == 0) return;

				this.txt문서번호.Text = this._gridParsingLog.Rows[e.RowIndex].Cells[1].Value.ToString();
			}
			catch (Exception ex)
			{
				MetroMessageBox.Show(this, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void _gridFileL_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				this._rowIndex = e.RowIndex;

				if (e.RowIndex < 0) return;

				this.txt문서번호S1.Text = this._gridFileL.Rows[e.RowIndex].Cells[1].Value.ToString();
			}
			catch (Exception ex)
			{
				MetroMessageBox.Show(this, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void FileSearch_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				if (MetroMessageBox.Show(this, "종료하시겠습니까?", "종료", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					e.Cancel = false;
				else
					e.Cancel = true;

				this.임시파일제거();
			}
			catch (Exception ex)
			{
				MetroMessageBox.Show(this, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void Btn조회_Click(object sender, EventArgs e)
		{
			try
			{
				DBMgr dbMgr = new DBMgr(this.isDebug);

				this._gridFileH.DataSource = dbMgr.Select(string.Format("EXEC SP_CZ_MA_FILEINFOH_S '{0}', '{1}', '{2}'", this.txt문서번호S.Text,
																													     this.dtp작성일자From.Text.Replace("-", ""),
																													     this.dtp작성일자To.Text.Replace("-", "")));

				DataGridViewColumn column = this._gridFileH.Columns[0];
				column.Width = 100;
				column.HeaderText = "문서번호";
				column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

				this._gridFileH_CellClick(null, null);
			}
			catch (Exception ex)
			{
				MetroMessageBox.Show(this, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void Btn파일보기_Click(object sender, EventArgs e)
		{
			string fileName;

			try
			{
				if (this._rowIndex < 0 || this._gridFileL.Rows.Count == 0) return;

				fileName = this._gridFileL.Rows[this._rowIndex].Cells[4].Value.ToString();

				if (string.IsNullOrEmpty(fileName))
				{
					this.webBrowser.Navigate(string.Empty);
					return;
				}

				this.webBrowser.Navigate("http://" + 아이피주소 + ":8080/Upload/" + fileName);
			}
			catch (Exception ex)
			{
				MetroMessageBox.Show(this, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void Btn파일업로드_Click(object sender, EventArgs e)
		{
			try
			{
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Multiselect = true;
				openFileDialog.RestoreDirectory = true;

				if (openFileDialog.ShowDialog() != DialogResult.OK) return;

				string[] fileNames = openFileDialog.FileNames;

				string 로컬경로 = @"C:\InqTemp\";

				if (Directory.Exists(로컬경로))
					Directory.Delete(로컬경로, true);

				Directory.CreateDirectory(로컬경로);

				DBMgr dbMgr = new DBMgr(this.isDebug);

				foreach (string fileName in fileNames)
				{
					string 문서번호 = dbMgr.Select(@"SELECT 'JO' + RIGHT(CONVERT(CHAR(8), GETDATE(), 112), 6) + RIGHT('000' + CAST(ISNULL(CONVERT(INT, MAX(SUBSTRING(A.ORD_NO, 9, 3))), 0) + 1 AS NVARCHAR), 3) AS ORD_NO
FROM ST_ESTIMATE_M A
WHERE A.ORD_NO LIKE 'JO' + RIGHT(CONVERT(CHAR(8), GETDATE(), 112), 6) + '%'").Rows[0]["ORD_NO"].ToString();

					if (Directory.Exists(로컬경로 + 문서번호 + @"\"))
						Directory.Delete(로컬경로 + 문서번호 + @"\", true);

					Directory.CreateDirectory(로컬경로 + 문서번호 + @"\");

					FileInfo fileInfo = new FileInfo(fileName);

					if (fileInfo.Extension == ".msg")
					{
						#region MSG
						MapiMessage msg = MapiMessage.FromFile(fileName);

						#region 본문 이미지 제거
						string[] temp = msg.BodyHtml.Split(new string[] { "cid:" }, StringSplitOptions.None);

						temp[0] = string.Empty;
						List<string> imageList = new List<string>();
						
						foreach (string image in temp)
						{
							if (string.IsNullOrEmpty(image))
								continue;

							string image1 = image.Split('.')[0];

							if (!imageList.Contains(image1))
								imageList.Add(image1);
						}
						#endregion

						List<string> 문의서파일 = new List<string>();

						foreach (MapiAttachment item in msg.Attachments)
						{
							if (string.IsNullOrEmpty(item.LongFileName))
								continue;

							string fileName1 = Regex.Replace(item.LongFileName, @"[^a-zA-Z0-9가-힣ㄱ-ㅎㅏ-ㅣ\[\]_ .]", string.Empty);
							FileInfo fileInfo1 = new FileInfo(fileName1);

							if (fileInfo1.Extension.Length == 0)
								continue;

							if (imageList.Contains(fileInfo1.Name.Replace(fileInfo1.Extension, string.Empty)))
								continue;

							fileName1 = this.GetUniqueFileName(로컬경로 + 문서번호 + @"\" + fileName1);

							if (File.Exists(로컬경로 + 문서번호 + @"\" + fileName1))
								File.Delete(로컬경로 + 문서번호 + @"\" + fileName1);

							item.Save(로컬경로 + 문서번호 + @"\" + fileName1);

							InquiryParser parser = new InquiryParser(로컬경로 + 문서번호 + @"\" + fileName1);

							if (parser.Parse(true))
								문의서파일.Add(로컬경로 + 문서번호 + @"\" + fileName1);
						}

						if (문의서파일.Count == 1)
						{
							foreach (string file in Directory.GetFiles(로컬경로 + 문서번호 + @"\"))
							{
								if (문의서파일.Contains(file))
								{
									this.Parsing(문서번호, file);
									FileInfo fileInfo1 = new FileInfo(file);
									this._gridParsingLog.Rows.Add(fileInfo.Name + "->" + fileInfo1.Name, 문서번호, this.UploadFile(로컬경로 + 문서번호 + @"\", fileInfo1.FullName, 문서번호, "문의서"));
								}
								else
								{
									FileInfo fileInfo1 = new FileInfo(file);
									this._gridParsingLog.Rows.Add(fileInfo.Name + "->" + fileInfo1.Name, 문서번호, this.UploadFile(로컬경로 + 문서번호 + @"\", fileInfo1.FullName, 문서번호, "기타"));
								}
							}
						}
						else
							this._gridParsingLog.Rows.Add(fileInfo.Name, string.Empty, "문의서 파일 여러개 : " + 문의서파일.Count.ToString());
						#endregion
					}
					else
					{
						#region PDF, EXCEL
						if (this.Parsing(문서번호, fileName))
							this._gridParsingLog.Rows.Add(fileInfo.Name, 문서번호, this.UploadFile(로컬경로 + 문서번호 + @"\", fileInfo.FullName, 문서번호, "문의서"));
						#endregion
					}
				}

				MetroMessageBox.Show(this, "파일업로드 완료", "정보", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MetroMessageBox.Show(this, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private bool Parsing(string 문서번호, string fileName)
		{
			FileInfo fileInfo = new FileInfo(fileName);

			try
			{
				DBMgr dbMgr = new DBMgr(this.isDebug);
				InquiryParser parser = new InquiryParser(fileName);

				if (!parser.Parse(true))
				{
					this._gridParsingLog.Rows.Add(fileInfo.Name, string.Empty, "파싱실패");
					return false;
				}
				
				string 거래처명 = string.Empty;
				string 담당자 = string.Empty; //MEMBERS

				switch (parser.Company)
				{
					case "한일후지":
						거래처명 = "한일후지코리아(주)";
						담당자 = "JO003"; //윤정미 차장 (717-3748), 장서이 사원 (717-3744)
						break;
					case "딘텍":
						거래처명 = "(주)DINTEC";
						담당자 = "JO002"; //지원정 과장 (717-3747)
						break;
					case "두베코":
						거래처명 = "두베코";
						담당자 = "JO002"; //지원정
						break;
				}

				DataTable line = new DataTable();
				line.TableName = "ROW";

				line.Columns.Add("ORD_LINENO", typeof(int));
				line.Columns.Add("SEQNO", typeof(decimal));
				line.Columns.Add("ITEM_NO", typeof(string));
				line.Columns.Add("ITEM_CD", typeof(string));
				line.Columns.Add("ITEM_NM", typeof(string));
				line.Columns.Add("ITEM_UNIT", typeof(string));
				line.Columns.Add("QTY", typeof(decimal));
				line.Columns.Add("OPT", typeof(string));

				int lineNo = 1;
				int itemNo = 1;

				DataRow newRow;

				foreach (DataRow dr in parser.Item.Rows)
				{
					if (!string.IsNullOrEmpty(dr["SUBJ"].ToString()))
					{
						string[] subjectArray = dr["SUBJ"].ToString().Split('\n').Where(x => x.Trim() != string.Empty).ToArray();

						for (int i = 0; i < subjectArray.Length; i++)
						{
							newRow = line.NewRow();

							if (i == 0)
								newRow["OPT"] = "+";
							else
								newRow["OPT"] = "-";

							newRow["ORD_LINENO"] = lineNo;
							newRow["SEQNO"] = lineNo;

							newRow["ITEM_NM"] = subjectArray[i];

							line.Rows.Add(newRow);
							lineNo++;
						}
					}

					string[] descArray = dr["DESC"].ToString().Split('\n').Where(x => x.Trim() != string.Empty).ToArray();

					if (descArray.Length > 1)
					{
						for (int i = 0; i < descArray.Length; i++)
						{
							if (i == 0)
							{
								newRow = line.NewRow();

								newRow["OPT"] = string.Empty;
								newRow["ORD_LINENO"] = lineNo;
								newRow["SEQNO"] = lineNo;
								newRow["ITEM_NO"] = itemNo;
								newRow["ITEM_CD"] = dr["ITEM"];
								newRow["ITEM_NM"] = descArray[i];
								newRow["ITEM_UNIT"] = dr["UNIT"];
								newRow["QTY"] = dr["QT"];

								line.Rows.Add(newRow);
								lineNo++;
							}
							else
							{
								newRow = line.NewRow();

								newRow["OPT"] = "=";
								newRow["ORD_LINENO"] = lineNo;
								newRow["SEQNO"] = lineNo;
								newRow["ITEM_NM"] = descArray[i];

								line.Rows.Add(newRow);
								lineNo++;
							}
						}
					}
					else
					{
						newRow = line.NewRow();

						newRow["ORD_LINENO"] = lineNo;
						newRow["SEQNO"] = lineNo;
						newRow["ITEM_NO"] = itemNo;
						newRow["ITEM_CD"] = dr["ITEM"];
						newRow["ITEM_NM"] = dr["DESC"];
						newRow["ITEM_UNIT"] = dr["UNIT"];
						newRow["QTY"] = dr["QT"];

						line.Rows.Add(newRow);
						lineNo++;
					}

					itemNo++;
				}

				DataSet ds = new DataSet();
				ds.Tables.Add(line);

				StringWriter sw = new StringWriter();
				ds.DataSetName = "XML";
				ds.WriteXml(sw, XmlWriteMode.IgnoreSchema);

				dbMgr.ExecuteNonQuery(string.Format(@"EXEC SP_ST_ESTIMATE_I '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}'",
													new object[] { 문서번호, 
																   담당자,
																   parser.Company.Replace("'", "''"),
														           거래처명,
																   parser.Vessel.Replace("'", "''"),
																   parser.ImoNumber.Replace("'", "''"),
																   parser.Reference.Replace("'", "''"),
																   sw.ToString().Replace("'", "''") }));

				return true;
			}
			catch (Exception ex)
			{
				MetroMessageBox.Show(this, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			this._gridParsingLog.Rows.Add(fileInfo.Name, string.Empty, "저장실패");
			return false;
		}

		private string UploadFile(string 로컬경로, string 파일경로, string 문서번호, string 문서유형)
		{
			try
			{
				FileInfo fileInfo = new FileInfo(파일경로);

				string 서버파일명 = this.GetUniqueFileName("http://" + 아이피주소 + ":8080/Upload", 파일경로);

				if (파일경로 != 로컬경로 + 서버파일명)
					fileInfo.CopyTo(로컬경로 + 서버파일명, true);

				WebClient webClient = new WebClient();
				string uriString = "http://" + 아이피주소 + ":8080/UploadFile.aspx";
				byte[] responseArray = webClient.UploadFile(uriString, 로컬경로 + 서버파일명);

				DBMgr dbMgr = new DBMgr(this.isDebug);
				dbMgr.ExecuteNonQuery(string.Format(@"EXEC SP_CZ_MA_FILEINFO_I '{0}', '{1}', '{2}'", 문서번호, 문서유형, 서버파일명));

				return Regex.Replace(Encoding.ASCII.GetString(responseArray).Replace(Environment.NewLine, string.Empty), @"<(.|\n)*?>", string.Empty).Trim();
			}
			catch (Exception ex)
			{
				MetroMessageBox.Show(this, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			return "업로드실패";
		}

		private string GetUniqueFileName(string url, string localFile)
		{
			try
			{
				FileInfo file = new FileInfo(localFile);
				string newName = Regex.Replace(file.Name.Replace(file.Extension, ""), @"[^a-zA-Z0-9가-힣ㄱ-ㅎㅏ-ㅣ\[\]_ ]", string.Empty, RegexOptions.Singleline);
				int index = 0;

				while (Exists(url, newName + file.Extension))
				{
					index++;
					newName = Regex.Replace(file.Name.Replace(file.Extension, ""), @"[^a-zA-Z0-9가-힣ㄱ-ㅎㅏ-ㅣ\[\]_ ]", string.Empty, RegexOptions.Singleline) + "(" + index + ")";
				}

				return newName + file.Extension;
			}
			catch (Exception ex)
			{
				MetroMessageBox.Show(this, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			return string.Empty;
		}

	    private string GetUniqueFileName(string fullPathFile)
		{
			FileInfo file = new FileInfo(fullPathFile);

			string newName = string.Empty;
			if (!string.IsNullOrEmpty(file.Name))
				newName = Regex.Replace(file.Name.Replace(file.Extension, ""), @"[^a-zA-Z0-9가-힣ㄱ-ㅎㅏ-ㅣ\[\]_ ]", string.Empty, RegexOptions.Singleline);
			else
				newName = "temp";


			string path = fullPathFile.Substring(0, fullPathFile.LastIndexOf(@"\"));

			int index = 0;

			while (File.Exists(path + @"\" + newName + file.Extension))
			{
				index++;
				newName = Regex.Replace(file.Name.Replace(file.Extension, ""), @"[^a-zA-Z0-9가-힣ㄱ-ㅎㅏ-ㅣ\[\]_ ]", string.Empty, RegexOptions.Singleline) + "(" + index + ")";
			}

			return newName + file.Extension;
		}

		private bool Exists(string url, string fileName)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "/" + fileName);
			HttpWebResponse response = null;
			bool flag = false;

			try
			{
				response = (HttpWebResponse)request.GetResponse();
				flag = true;
			}
			catch
			{
				flag = false;
			}
			finally
			{
				if (response != null) response.Close();
			}

			return flag;
		}

		private void 임시파일제거()
		{
			DirectoryInfo dirInfo;
			
			try
			{
				dirInfo = new DirectoryInfo(@"C:\InqTemp");

				if (dirInfo.Exists == true)
					dirInfo.Delete(true);
			}
			catch (Exception ex)
			{
				MetroMessageBox.Show(this, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Duzon.Common.Forms;
using Duzon.Common.BpControls;
using DzHelpFormLib;
using Duzon.Windows.Print;

using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.ERPU;
using Dintec;
using Parsing.Parser.UNIPASS;
using Duzon.Common.Util;
using System.Text.RegularExpressions;

namespace cz
{
	public partial class P_CZ_UT_HHI_XML : PageBase
	{

		string NO_EMP = Global.MainFrame.LoginInfo.EmployeeNo.ToString();
		string NM_EMP = Global.MainFrame.LoginInfo.EmployeeName.ToString();
		public P_CZ_UT_HHI_XML()
		{
			InitializeComponent();
		}

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{
			InitControl();
			InitGrid();
			InitEvent();
		}

		private void InitControl()
		{
			
		}

		private void InitGrid()
		{
			flexGrid1.BeginSetting(1, 1, false);

			flexGrid1.SetCol("NO_IMO"		, "NO_IMO"		, true);
			flexGrid1.SetCol("NO_ENGINE"	, "NO_ENGINE"	, true);
			flexGrid1.SetCol("SERIAL"		, "SERIAL"		, true);
			flexGrid1.SetCol("NO_DRAWING"	, "NO_DRAWING"	, true);
			flexGrid1.SetCol("WEIGHT"		, "WEIGHT"		, true);
			flexGrid1.SetCol("NM_PLATE"		, "NM_PLATE"	, true);
			flexGrid1.SetCol("NO_PLATE"		, "NO_PLATE"	, true);
			flexGrid1.SetCol("UNIT"			, "UNIT"		, true);			

			flexGrid1.KeyActionEnter = KeyActionEnum.MoveDown;
			flexGrid1.SettingVersion = "16.10.10.02";
			flexGrid1.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

			////////////////////////////////////////////////////////////////////////////

			flexGrid2.BeginSetting(1, 1, false);
					
			flexGrid2.SetCol("NO_IMO"		, "NO_IMO"		, true);
			flexGrid2.SetCol("NO_ENGINE"	, "NO_ENGINE"	, true);
			flexGrid2.SetCol("SERIAL"		, "SERIAL"		, true);
			flexGrid2.SetCol("NO_DRAWING"	, "NO_DRAWING"	, true);
			flexGrid2.SetCol("PL_DRAWING"	, "PL_DRAWING"	, true);
			flexGrid2.SetCol("WEIGHT"		, "WEIGHT"		, true);
			flexGrid2.SetCol("NM_PLATE"		, "NM_PLATE"	, true);
			flexGrid2.SetCol("NO_PLATE"		, "NO_PLATE"	, true);
			flexGrid2.SetCol("UNIT"			, "UNIT"		, true);		
					
			flexGrid2.KeyActionEnter = KeyActionEnum.MoveDown;
			flexGrid2.SettingVersion = "17.01.23.02";
			flexGrid2.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
		}

		private void InitEvent()
		{
			button1.Click += new EventHandler(button1_Click);
			button2.Click += new EventHandler(button2_Click);
		}

		

		protected override void InitPaint()
		{
			tabControlExt1.SelectedIndex = 1;
		}

		#endregion

		private void button1_Click(object sender, EventArgs e)
		{
			textBox1.Text = "";

			if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
			{
				DirectoryInfo dir = new DirectoryInfo(folderBrowserDialog1.SelectedPath);
				textBox1.Text = "Start...";

				foreach (FileInfo file in dir.GetFiles())
				{					
					if (file.Extension.ToUpper() == ".PDF")
					{
						textBox1.Text += "\r\n" + file.Name;
						string s = file.FullName;

						try
						{
							UNIPASS_2 p = new UNIPASS_2(s);
							p.Parse();

							FileMgr.CreateDirectory(folderBrowserDialog1.SelectedPath + "\\Completed");
							File.Move(file.FullName, folderBrowserDialog1.SelectedPath + "\\Completed\\" + file.Name);

							textBox1.Text += "==> Success";
						}
						catch (Exception ex)
						{
							textBox1.Text += "==> Fail";

							if (ex.Message.IndexOf("StartIndex는") >= 0)
							{
								textBox1.Text += " (잘못된 파일)";
							}
							else if (ex.Message.IndexOf("IMO번호 없음") >= 0)
							{
								textBox1.Text += " (" + ex.Message + ")";
							}
							else if (ex.Message.IndexOf("엔진시리얼 없음") >= 0)
							{
								textBox1.Text += " (" + ex.Message + ")";
							}
							else if (ex.Message.IndexOf("조회되는 DATA 없음") >= 0)
							{
								textBox1.Text += " (조회되는 DATA 없음)";
							}
							else if (ex.Message.IndexOf("길이제한 초과") >= 0)
							{
								textBox1.Text += " (" + ex.Message +")";
							}
							else if (ex.Message.IndexOf("문자열이나 이진 데이터는 잘립니다.") >= 0)
							{
								textBox1.Text += " (자리수 초과)";
							}
							else if (ex.Message.IndexOf("파일이 이미 있으므로") >= 0)
							{
								textBox1.Text += " (파일 이동 오류:이미 있음)";
							}
							else
							{
								textBox1.Text += " (알수없음)";
							}							
						}						
					}
				}

				textBox1.Text += "\r\nEnd...";
				ShowMessage("완료");
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			textBox2.Text = "";

			if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
			{
				DirectoryInfo dir = new DirectoryInfo(folderBrowserDialog1.SelectedPath);
				textBox2.Text = "Start...";

				foreach (FileInfo file in dir.GetFiles())
				{
					if (file.Extension.ToUpper() == "XLS" || file.Extension.ToUpper() == ".XLSX")
					{
						if (file.Name.IndexOf("~$") >= 0) continue;

						textBox2.Text += "\r\n" + file.Name;

						ExcelReader excel = new ExcelReader();
						DataTable dt = excel.Read(file.FullName, 1, 2);

                        dt.Columns.Remove("PRINT NO");
                        dt.Columns.Remove("QTY");

                        dt.Columns["PLATE NO"].ColumnName = "NO_PLATE";
						dt.Columns["DESCRIPTION"].ColumnName = "NM_PLATE";
						dt.Columns["U-CODE"].ColumnName = "UCODE";
						dt.Columns["UNIT"].ColumnName = "UNIT";
						//dt.Columns["STOCK"].ColumnName = "STOCK";
						dt.Columns["LEAD TIME"].ColumnName = "DEL";
						dt.Columns["WEIGHT (KG)"].ColumnName = "WEIGHT";
						dt.Columns["DWG_NO"].ColumnName = "NO_DRAWING";
						dt.Columns["PL_DWG"].ColumnName = "PL_DRAWING";

                        foreach (DataRow row in dt.Rows)
                        {
                            if (!string.IsNullOrEmpty(row["UNIT"].ToString()))
                                row["UNIT"] = row["UNIT"].ToString().Split('(')[1].Split(')')[0];
                        }

						try
						{
							// IMO 번호 가져오기
							string fileName = file.Name.Replace(file.Extension, "");
							string imoNumber = fileName.Substring(0, fileName.IndexOf("_"));

							// 시리얼 번호 가져오기
							string serial = fileName.Substring(fileName.IndexOf("_") + 1);

							if (serial.Length == 10)
							{

							}
							else if (serial.Length == 13)
							{
								
							}	
							else
							{
								throw new Exception("잘못된 파일");
							}

							string xml = GetTo.Xml(dt);

                            xml = xml.Replace("&#", "");

                            //if (Regex.IsMatch(xml, @"[^a-zA-Z0-9가-힣_\<\>\-\.\/\n\r\(\)\,\=\*\'\+""Φ]"))
                            //{
                            //    textBox2.Text += Regex.Replace(xml, @"[a-zA-Z0-9가-힣_\<\>\-\.\/\n\r\(\)\,\=\*\'\+""Φ]", "", RegexOptions.Multiline).Trim().Replace(" ", "");
                            //}

                            DataTable dtResult = DBMgr.GetDataTable("HHI_XML_MAPS", DebugMode.Print, xml, imoNumber, serial, file.Name);
							flexGrid2.Binding = dtResult;

							FileMgr.CreateDirectory(folderBrowserDialog1.SelectedPath + "\\Completed");
							File.Move(file.FullName, folderBrowserDialog1.SelectedPath + "\\Completed\\" + file.Name);

							textBox2.Text += "==> Success";
							textBox2.SelectionStart = textBox2.TextLength;
							textBox2.ScrollToCaret();
						}
						catch (Exception ex)
						{
							textBox2.Text += "==> Fail";

							if (ex.Message.IndexOf("StartIndex는") >= 0)
							{
								textBox2.Text += " (잘못된 파일)";
							}
							else if (ex.Message.IndexOf("IMO번호 없음") >= 0)
							{
								textBox2.Text += " (" + ex.Message + ")";
							}
							else if (ex.Message.IndexOf("엔진시리얼 없음") >= 0)
							{
								textBox2.Text += " (" + ex.Message + ")";
							}
							else if (ex.Message.IndexOf("조회되는 DATA 없음") >= 0)
							{
								textBox2.Text += " (조회되는 DATA 없음)";
							}
							else if (ex.Message.IndexOf("길이제한 초과") >= 0)
							{
								textBox2.Text += " (" + ex.Message + ")";
							}
							else if (ex.Message.IndexOf("문자열이나 이진 데이터는 잘립니다.") >= 0)
							{
								textBox2.Text += " (자리수 초과)";
							}
							else if (ex.Message.IndexOf("파일이 이미 있으므로") >= 0)
							{
								textBox2.Text += " (파일 이동 오류:이미 있음)";
							}
							else
							{
								textBox2.Text += " (알수없음:" + ex.Message + ")";
							}		
						}
					}
				}

				textBox2.Text += "\r\nEnd...";
				
			}

			string contents = @"** 현대 MAPS 기부속 업로드 완료

※ 본 쪽지는 발신 전용 입니다." +Environment.NewLine + "업로드 담당자: " + NO_EMP + " / " + NM_EMP;

			contents = string.Format(contents);

			Messenger.SendMSG(new string[] { "S-458", "S-495","S-556" }, contents);

			Util.ShowMessage("End");
		}
	}
}

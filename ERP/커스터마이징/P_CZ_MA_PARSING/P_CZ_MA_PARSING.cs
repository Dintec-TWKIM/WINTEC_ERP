using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Duzon.Common.Forms;
using Dintec;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Parsing;

namespace cz
{
    public partial class P_CZ_MA_PARSING : PageBase
    {
        public string fileName = string.Empty;
        public string outFileName = string.Empty;
        public int TotalPageCount;
        string fileFailNo = string.Empty;
        

        public P_CZ_MA_PARSING()
        {
            StartUp.Certify(this);
            InitializeComponent();
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this.flexGridEdit };

            flexGridEdit.BeginSetting(1, 1, true);


            flexGridEdit.SetCol("PAGENO", "PAGE", 50);
            flexGridEdit.SetCol("NO", "NO", 50);

            flexGridEdit.SetCol("SHIPYARD", "조선소", 100);
            flexGridEdit.SetCol("HULLNO", "HULLNO", 100);
            flexGridEdit.SetCol("DWGNO", "도면번호", 100);

            flexGridEdit.SetCol("PROJECT", "PROJECT", 100);
            flexGridEdit.SetCol("TITLE", "TITLE", 100);
            
            flexGridEdit.SetCol("DESC", "DESCRIPTION", 200);
            flexGridEdit.SetCol("SIZE", "SIZE", 100);
            flexGridEdit.SetCol("MALT", "MALT", 100);
            flexGridEdit.SetCol("QT", "QT", 100);
			flexGridEdit.SetCol("WT", "WT", 100);
            flexGridEdit.SetCol("CODE", "CODE", 100);
            flexGridEdit.SetCol("RMK", "REMARKS", 100);
			
            flexGridEdit.SetCol("SYMBOL", "SYMBOL", 100);
            flexGridEdit.SetCol("SPECIFICATION", "SPECIFICATION", 100);

            flexGridEdit.SetCol("MODEL", "MODEL", 100);
            flexGridEdit.SetCol("MAKER", "MAKER", 100);

            flexGridEdit.SetCol("LOC", "LOC", 100);

            flexGridEdit.Cols["QT"].TextAlign = TextAlignEnum.CenterCenter;

            flexGridEdit.SettingVersion = "0.0.0.14";
            flexGridEdit.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);


            this.MainGrids = new FlexGrid[] { this.flexGridAll };

            flexGridAll.BeginSetting(1, 1, true);


            flexGridAll.SetCol("col0", "col0", 50);
            flexGridAll.SetCol("col1", "col1", 50);
            flexGridAll.SetCol("col2", "col2", 50);
            flexGridAll.SetCol("col3", "col3", 50);
            flexGridAll.SetCol("col4", "col4", 50);
            flexGridAll.SetCol("col5", "col5", 50);
            flexGridAll.SetCol("col6", "col6", 50);
            flexGridAll.SetCol("col7", "col7", 50);
            flexGridAll.SetCol("col8", "col8", 50);
            flexGridAll.SetCol("col9", "col9", 50);
            flexGridAll.SetCol("col10", "col10", 50);
            flexGridAll.SetCol("col11", "col11", 50);
            flexGridAll.SetCol("col12", "col12", 50);
            flexGridAll.SetCol("col13", "col13", 50);
            flexGridAll.SetCol("col14", "col14", 50);
            flexGridAll.SetCol("col15", "col15", 50);
            flexGridAll.SetCol("col16", "col16", 50);
            flexGridAll.SetCol("col17", "col17", 50);
            flexGridAll.SetCol("col18", "col18", 50);
            flexGridAll.SetCol("col19", "col19", 50);
            flexGridAll.SetCol("col20", "col20", 50);
            flexGridAll.SetCol("col21", "col21", 50);
            flexGridAll.SetCol("col22", "col22", 50);
            flexGridAll.SetCol("col23", "col23", 50);
            flexGridAll.SetCol("col24", "col24", 50);
            flexGridAll.SetCol("col25", "col25", 50);
            flexGridAll.SetCol("col26", "col26", 50);
            flexGridAll.SetCol("col27", "col27", 50);
            flexGridAll.SetCol("col28", "col28", 50);
            flexGridAll.SetCol("col29", "col29", 50);
            flexGridAll.SetCol("col30", "col30", 50);
            flexGridAll.SetCol("col31", "col30", 50);
            flexGridAll.SetCol("col32", "col30", 50);
            flexGridAll.SetCol("col33", "col30", 50);
            flexGridAll.SetCol("col34", "col30", 50);
            flexGridAll.SetCol("col35", "col30", 50);
            flexGridAll.SetCol("col36", "col30", 50);
            flexGridAll.SetCol("col37", "col30", 50);
            flexGridAll.SetCol("col38", "col30", 50);
            flexGridAll.SetCol("col39", "col30", 50);
            flexGridAll.SetCol("col40", "col30", 50);
            flexGridAll.SetCol("col41", "col41", 50);
            flexGridAll.SetCol("col42", "col42", 50);
            flexGridAll.SetCol("col43", "col43", 50);
            flexGridAll.SetCol("col44", "col44", 50);
            flexGridAll.SetCol("col45", "col45", 50);
            flexGridAll.SetCol("col46", "col46", 50);
            flexGridAll.SetCol("col47", "col47", 50);
            flexGridAll.SetCol("col48", "col48", 50);
            flexGridAll.SetCol("col49", "col49", 50);
            flexGridAll.SetCol("col50", "col50", 50);
            flexGridAll.SetCol("col51", "col51", 50);
            flexGridAll.SetCol("col52", "col52", 50);
            flexGridAll.SetCol("col53", "col53", 50);
            flexGridAll.SetCol("col54", "col54", 50);
            flexGridAll.SetCol("col55", "col55", 50);
            flexGridAll.SetCol("col56", "col56", 50);
            flexGridAll.SetCol("col57", "col57", 50);
            flexGridAll.SetCol("col58", "col58", 50);
            flexGridAll.SetCol("col59", "col59", 50);
            flexGridAll.SetCol("col60", "col60", 50);
            flexGridAll.SetCol("col61", "col61", 50);
            flexGridAll.SetCol("col62", "col62", 50);
            flexGridAll.SetCol("col63", "col63", 50);
            flexGridAll.SetCol("col64", "col64", 50);
            flexGridAll.SetCol("col65", "col65", 50);
            flexGridAll.SetCol("col66", "col66", 50);
            flexGridAll.SetCol("col67", "col67", 50);
            flexGridAll.SetCol("col68", "col68", 50);
            flexGridAll.SetCol("col69", "col69", 50);
            flexGridAll.SetCol("col70", "col70", 50);
            flexGridAll.SetCol("col71", "col71", 50);
            flexGridAll.SetCol("col72", "col72", 50);
            flexGridAll.SetCol("col73", "col73", 50);
            flexGridAll.SetCol("col74", "col74", 50);
            flexGridAll.SetCol("col75", "col75", 50);
            flexGridAll.SetCol("col76", "col76", 50);
            flexGridAll.SetCol("col77", "col77", 50);
            flexGridAll.SetCol("col78", "col78", 50);
            flexGridAll.SetCol("col79", "col79", 50);
            flexGridAll.SetCol("col80", "col80", 50);

            flexGridAll.SettingVersion = "0.0.0.11";
            flexGridAll.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        private void InitEvent()
        {
            //btn두산.Click += new EventHandler(btn두산_Click);
            btn파일열기.Click += new EventHandler(btn파일열기_Click);
            btn자르기.Click += new EventHandler(btn자르기_Click);
            btn초기화.Click += new EventHandler(btn초기화_Click);
            btn홀수.Click += new EventHandler(btn홀수_Click);
            btn짝수.Click += new EventHandler(btn짝수_Click);
            btn기부속업로드.Click += new EventHandler(btn기부속업로드_Click);

			btn이미지.Click += new EventHandler(btn이미지_Click);

			btn지정파싱.Click += Btn지정파싱_Click;

            btn추가.Click += Btn추가_Click;
            btn삭제.Click += Btn삭제_Click;

            btn조선소.Click += Btn조선소_Click;
            btnHull.Click += BtnHull_Click;
            btnDWGNO.Click += BtnDWGNO_Click;
        }

        private void BtnDWGNO_Click(object sender, EventArgs e)
        {
            for (int c = 1; c < flexGridEdit.Rows.Count; c++)
            {
                flexGridEdit.Rows[flexGridEdit.Rows.Count - c]["DWGNO"] = tbxDWGNO.Text;
            }
        }

        private void BtnHull_Click(object sender, EventArgs e)
        {
            for (int c = 1; c < flexGridEdit.Rows.Count; c++)
            {
                flexGridEdit.Rows[flexGridEdit.Rows.Count - c]["HULLNO"] = tbxHull.Text;
            }
        }

        private void Btn조선소_Click(object sender, EventArgs e)
        {
            for (int c = 1; c < flexGridEdit.Rows.Count; c++)
            {
                flexGridEdit.Rows[flexGridEdit.Rows.Count - c]["SHIPYARD"] = tbx조선소.Text;
            }
        }

        private void Btn삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeDelete() || !this.flexGridEdit.HasNormalRow) return;
                this.flexGridEdit.Rows.Remove(flexGridEdit.Row);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Btn추가_Click(object sender, EventArgs e)
        {
            flexGridEdit.Rows.Add();
        }

		private void Btn지정파싱_Click(object sender, EventArgs e)
		{
			try
			{
                string extension = Path.GetExtension(fileName);

                if (extension.ToUpper().Equals(".PDF"))
                {
                    fileFailNo = txt추출페이지.Text;

                    //MsgControl.ShowMsg(DD("파싱 중 입니다."));
                    int pageNo = Convert.ToInt16(cbo페이지.Text) + 1;
                    CodeParser parser = new CodeParser(outFileName, pageNo);
                    //parser.(true);

                    MsgControl.CloseMsg();

                    //rtbx파싱.AppendText(parser.textpdf);
                    //flexGridAll.Binding = parser.ItemAll;

                    //flexGridAll.SetDataBinding(parser.ItemAll, null, true);

                    foreach (DataRow row in parser.Item.Rows)
                    {
                        flexGridEdit.Rows.Add();
                        flexGridEdit.Row = flexGridEdit.Rows.Count - 1;

                        flexGridEdit["SYMBOL"] = row["SYMBOL"].ToString().ToUpper();
                        flexGridEdit["DESC"] = row["DESC"].ToString().ToUpper();
                        flexGridEdit["MALT"] = row["MALT"].ToString().ToUpper();
                        flexGridEdit["QT"] = row["QT"].ToString().ToUpper();
                        flexGridEdit["WT"] = row["WT"].ToString().ToUpper();
                        flexGridEdit["RMK"] = row["RMK"].ToString().ToUpper();
                        flexGridEdit["DWGNO"] = row["DWGNO"].ToString().ToUpper();
                        flexGridEdit["SIZE"] = row["SIZE"].ToString().ToUpper();
                        flexGridEdit["MODEL"] = row["MODEL"].ToString().ToUpper();
                        flexGridEdit["MAKER"] = row["MAKER"].ToString().ToUpper();
                        flexGridEdit["CODE"] = row["CODE"].ToString().ToUpper();
                        flexGridEdit["SPECIFICATION"] = row["SPECIFICATION"].ToString().ToUpper();
                        flexGridEdit["LOC"] = row["LOC"].ToString().ToUpper();
                        flexGridEdit["NO"] = row["NO"].ToString().ToUpper();
                        flexGridEdit["PAGENO"] = row["PAGENO"].ToString().ToUpper();

                        flexGridEdit["SHIPYARD"] = row["SHIPYARD"].ToString().ToUpper();
                        flexGridEdit["HULLNO"] = row["HULLNO"].ToString().ToUpper();
                        flexGridEdit["DWGNO"] = row["DWGNO"].ToString().ToUpper();
                        flexGridEdit["PROJECT"] = row["PROJECT"].ToString().ToUpper();
                        flexGridEdit["TITLE"] = row["TITLE"].ToString().ToUpper();

                        string pageNoStr = row["PAGENO"].ToString().Trim();
                        // 누락된 페이지
                        if (!TotalPageCount.Equals(pageNoStr))
                        {
                            if (fileFailNo.StartsWith(pageNoStr + ","))
                            {
                                fileFailNo = fileFailNo.Substring(2, fileFailNo.Length - 2);
                            }
                            else
                            {
                                fileFailNo = fileFailNo.Replace("," + pageNoStr + ",", ",");
                            }
                        }
                        else if (TotalPageCount.Equals(pageNoStr))
                        {
                            fileFailNo = fileFailNo.Replace(pageNoStr, "");
                        }

                        //fileFailNo = fileFailNo.Replace(row["NO"].ToString().Trim(), "");

                        flexGridEdit.AddFinished();
                    }

                    //flexGridAll.Binding = parser.ItemAll;

                    int itemAllCol = parser.ItemAll.Columns.Count;

                    if (itemAllCol > 79)
                        itemAllCol = 79;

                    foreach (DataRow row in parser.ItemAll.Rows)
                    {
                        flexGridAll.Rows.Add();
                        flexGridAll.Row = flexGridAll.Rows.Count - 1;

                        for (int c = 0; c < itemAllCol; c++)
                        {
                            flexGridAll["col" + c] = row[c].ToString().ToUpper();
                        }

                        flexGridAll.AddFinished();
                    }

                    tbx실패.Text = fileFailNo.Trim();
                    MsgControl.CloseMsg();
                }
                else if (extension.ToUpper().Equals(".XLSX"))
                {
                    CodeParser parser = new CodeParser(outFileName, 0);

                    int itemAllCol = parser.ItemAll.Columns.Count;

                    if (itemAllCol > 79)
                        itemAllCol = 79;

                    foreach (DataRow row in parser.ItemAll.Rows)
                    {
                        flexGridAll.Rows.Add();
                        flexGridAll.Row = flexGridAll.Rows.Count - 1;

                        for (int c = 0; c < itemAllCol; c++)
                        {
                            flexGridAll["col" + c] = row[c].ToString().ToUpper();
                        }

                        flexGridAll.AddFinished();
                    }
                }
			}
			catch (Exception ex)
			{
				MsgControl.CloseMsg();
				ShowMessage(ex.Message);
			}
		}

		private void btn이미지_Click(object sender, EventArgs e)
		{
			
		}


		private void btn두산_Click(object sender, EventArgs e)
        {
            //MsgControl.ShowMsg(DD("파싱 중 입니다."));
            try
            {
                //CodeParser parser = new CodeParser(outFileName,1);
                ////parser.(true);

                //foreach (DataRow row in parser.Item.Rows)
                //{
                //    _flex.Rows.Add();
                //    _flex.Row = _flex.Rows.Count - 1;

                //    _flex["CD_ITEM"] = row["CD"].ToString().ToUpper();
                //    _flex["NM_ITEM"] = row["NM"].ToString().ToUpper();

                //    _flex.AddFinished();
                //}
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
            }
            //MsgControl.CloseMsg();
        }

        private void btn파일열기_Click(object sender, EventArgs e)
        {
            MsgControl.CloseMsg();

            OpenFileDialog fileDlg = new OpenFileDialog();
            fileDlg.Filter = "Inquiry|*.pdf;*.xls;*.xlsx;*.msg";

            if (fileDlg.ShowDialog() != DialogResult.OK) return;
            fileName = fileDlg.FileName;

            string extension = Path.GetExtension(fileName);

            if (extension.ToUpper().Equals(".PDF"))
            {
                // 파일열어 바로 두산 파싱
                outFileName = fileName;


                // pdf 뿌리기
                axAcroPDF1.src = fileName;

                btn자르기.Enabled = true;


                iTextSharp.text.pdf.PdfReader PReader = new iTextSharp.text.pdf.PdfReader(fileName);
                int pageCount = Convert.ToInt16(PReader.NumberOfPages.ToString());

                txt추출페이지.Text = "";
                tbx파일이름.Text = fileName;

                for (int i = 1; i <= pageCount; i++)
                {
                    txt추출페이지.Text = txt추출페이지.Text + "," + i;
                    cbo페이지.Items.Add(i);
                    TotalPageCount = i;
                }

                if (txt추출페이지.Text.StartsWith(","))
                    txt추출페이지.Text = txt추출페이지.Text.Substring(1, txt추출페이지.Text.Length - 1).Trim();

                cbo페이지.SelectedIndex = 0;
            }
            else if (extension.ToUpper().Equals(".XLSX"))
            {
                outFileName = fileName;
                tbx파일이름.Text = fileName;
            }
		}

        private void btn자르기_Click(object sender, EventArgs e)
        {
            // Split 버튼 클릭시
            string outFile = fileName + "\\output2.pdf";

            // 추출 페이지 가지고 오기
            string[] pageCount = txt추출페이지.Text.Split(',');

            // 세이브 파일 위치 지정
            using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
            {

                //저장 파일 기본 정보 일단은 pdf만
                saveFileDialog1.FileName = "split.pdf";
                saveFileDialog1.Filter = "pdf files (*.pdf)|*.pdf";
                

                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK
                    && saveFileDialog1.FileName.Length > 0)
                {
                    // 저장파일명
                    outFile = saveFileDialog1.FileName;

                    // 전역에다가 -> 바로 파싱 가능하게 하기위해서
                    outFileName = outFile;


                    SplitPDFs(fileName, outFile, pageCount);

                    axAcroPDF1.src = outFile;

                    Util.ShowMessage("페이지 추출 완료");

                    btn자르기.Enabled = false;

                    btn두산_Click(null, null);
                }
            }
        }



        private void btn초기화_Click(object sender, EventArgs e)
        {
			//rtbx파싱.Text = "";
            tbx조선소.Text = "";
            tbxHull.Text = "";
            tbxDWGNO.Text = "";
            txt추출페이지.Text = "";
            tbx실패.Text = "";
            tbx파일이름.Text = "";


            if (flexGridAll.Rows.Count - 1 > 0)
            {
                this.flexGridAll.Rows.RemoveRange(1, flexGridAll.Rows.Count - 1);
            }

            if (flexGridEdit.Rows.Count - 1 > 0)
            {
                this.flexGridEdit.Rows.RemoveRange(1, flexGridEdit.Rows.Count - 1);
            }
        }




        private void btn홀수_Click(object sender, EventArgs e)
        {
            // 전체 페이지 수 가져오기
            iTextSharp.text.pdf.PdfReader PReader = new iTextSharp.text.pdf.PdfReader(fileName);
            int pageCount = Convert.ToInt16(PReader.NumberOfPages.ToString());

            txt추출페이지.Text = "";

            for (int i = 1; i <= pageCount; i++)
            {
                if (i % 2 == 1)
                    txt추출페이지.Text = txt추출페이지.Text + "," + i;
            }

            if (txt추출페이지.Text.StartsWith(","))
                txt추출페이지.Text = txt추출페이지.Text.Substring(1, txt추출페이지.Text.Length - 1).Trim();
        }

        private void btn짝수_Click(object sender, EventArgs e)
        {
            // 전체 페이지 수 가져오기
            iTextSharp.text.pdf.PdfReader PReader = new iTextSharp.text.pdf.PdfReader(fileName);
            int pageCount = Convert.ToInt16(PReader.NumberOfPages.ToString());

            txt추출페이지.Text = "";

            for (int i = 1; i <= pageCount; i++)
            {
                if (i % 2 == 0)
                    txt추출페이지.Text = txt추출페이지.Text + "," + i;
            }

            if (txt추출페이지.Text.StartsWith(","))
                txt추출페이지.Text = txt추출페이지.Text.Substring(1, txt추출페이지.Text.Length - 1).Trim();
        }

        private void btn기부속업로드_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                DirectoryInfo dir = new DirectoryInfo(folderBrowserDialog1.SelectedPath);

                foreach (FileInfo file in dir.GetFiles())
                {
                    if (file.Extension.ToUpper() == "XLS" || file.Extension.ToUpper() == ".XLSX")
                    {
                        if (file.Name.IndexOf("~$") >= 0) continue;


                        ExcelReader excel = new ExcelReader();
                        DataTable dt = excel.Read(file.FullName, 1, 2);

                        dt.Columns["PLATE NO"].ColumnName = "NO_PLATE";
                        dt.Columns["DESCRIPTION"].ColumnName = "NM_PLATE";
                        dt.Columns["U-CODE"].ColumnName = "UCODE";
                        dt.Columns["UNIT"].ColumnName = "UNIT";
                        dt.Columns["STOCK"].ColumnName = "STOCK";
                        dt.Columns["DEL"].ColumnName = "DEL";
                        dt.Columns["WEIGHT(KG)"].ColumnName = "WEIGHT";
                        dt.Columns["DWG_NO"].ColumnName = "NO_DRAWING";
                        dt.Columns["PL_DWG"].ColumnName = "PL_DRAWING";


                        


                        try
                        {
                            // IMO 번호 가져오기
                            string fileName = file.Name.Replace(file.Extension, "");
                            string imoNumber = fileName.Substring(0, fileName.IndexOf("_"));

                            // 시리얼 번호 가져오기
                            string serial = fileName.Substring(fileName.IndexOf("_") + 1);

                            if (serial.Length == 10)
                            {
                                serial = "K" + serial.Substring(0, 2) + "00" + serial.Substring(2);
                            }
                            else if (serial.Length == 13)
                            {

                            }
                            else
                            {
                                throw new Exception("잘못된 파일");
                            }

                            string xml = GetTo.Xml(dt);

                            xml = xml.Replace("&#x5;", "");
                            xml = xml.Replace("&#x6;", "");

                            DataTable dtResult = DBMgr.GetDataTable("HHI_XML_MAPS", new object[] { xml, imoNumber, serial, file.Name });
                         //   flexMaps.Binding = dtResult;

                            FileMgr.CreateDirectory(folderBrowserDialog1.SelectedPath + "\\Completed");
                            File.Move(file.FullName, folderBrowserDialog1.SelectedPath + "\\Completed\\" + file.Name);
                        }
                        catch (Exception ex)
                        {
                           
                        }
                    }
                }

            }
            Util.ShowMessage("아이템 동기화 완료");
        }



        public void SplitPDFs(String InputPDFName, string OutPDFName, string[] pageCount)
        {
            int POffset = 0;
            int FileCount = 0;
            int TempPageNumber = 0;

            Document document = null;
            PdfCopy PCopyPage = null;

            try
            {
                //PDF 파일 열기
                iTextSharp.text.pdf.PdfReader PReader = new iTextSharp.text.pdf.PdfReader(fileName);
                PReader.ConsolidateNamedDestinations();

                //PDF 파일 페이지 정보 넣기
                TempPageNumber = PReader.NumberOfPages;
                POffset += TempPageNumber;

                //PDF 첫 파일을 기준으로 페이지생성 소스
                if (FileCount == 0)
                {
                    document = new Document(PReader.GetPageSizeWithRotation(1));
                    PCopyPage = new PdfCopy(document, new FileStream(OutPDFName, FileMode.Create));
                    document.Open();
                }

                //PDF 페이지 추가(페이지 단위는 1부터)
                //int StartPage = Convert.ToInt32(33);
                //int EndPage = Convert.ToInt32(33);

                if (!string.IsNullOrEmpty(pageCount[0].ToString()))
                {
                    for (int i = 0; i < pageCount.Length; i++)
                    {
                        if (PCopyPage != null)
                        {
                            PdfImportedPage ImPage = PCopyPage.GetImportedPage(PReader, Convert.ToInt16(pageCount[i]));
                            PCopyPage.AddPage(ImPage);
                        }
                    }

                    PRAcroForm form = PReader.AcroForm;
                    if (form != null && PCopyPage != null)
                    {
                        PCopyPage.Close();
                    }

                    if (document != null)
                    {
                        document.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

		private void axAcroPDF1_OnError(object sender, EventArgs e)
		{

		}
	}
}

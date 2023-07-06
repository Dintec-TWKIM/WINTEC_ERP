using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Duzon.Common.Forms;
using System.Windows.Forms;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using System.IO;
using Duzon.ERPU;
using Duzon.Common.Util;
using Microsoft.Win32;
using System.Diagnostics;
using Duzon.ERPU.Utils;
using Duzon.Common.Forms.Help;
using Dintec;
using DX;

namespace cz
{
    [UserHelpDescription("첨부파일 업/다운로드", "첨부파일을 서버로 전송합니다.")]
    public partial class P_CZ_MA_FILE_SUB : Duzon.Common.Forms.CommonDialog
    {
        private string 회사 = string.Empty;
        private string 모듈 = string.Empty;
        private string 메뉴 = string.Empty;
        private string 파일코드 = string.Empty;
        private DirectoryInfo dirInfo = null;
        private string regeditDefaut1 = string.Empty;
        private object regeditDefautCommand1 = null;
        private string regeditDefaut2 = string.Empty;
        private object regeditDefautCommand2 = null;
        private string 임시폴더;

        public string FileNames { get; private set; }

        public int FileCount { get; private set; }

        [DefaultValue(true)]
        public bool UseGrant { get; set; }

        public P_CZ_MA_FILE_SUB()
        {
            this.InitializeComponent();

            this.UseGrant = true;
        }

        public P_CZ_MA_FILE_SUB(string 회사, string 모듈, string 메뉴, string 파일코드, string 경로)
            : this()
        {
            this.회사 = 회사;
            this.모듈 = 모듈;
            this.메뉴 = 메뉴;
            this.파일코드 = 파일코드;
            this.txt서버파일경로.Text = "Upload/" + 경로;
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.임시폴더 = "temp";

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);

            this._flex.SetCol("URL", "URL", false);
            this._flex.SetCol("FILE_PATH", "FILE_PATH", false);
            this._flex.SetCol("FILE_EXT", "파일형식", false);
            this._flex.SetCol("NO_SEQ", "순번", false);
            this._flex.SetCol("CD_MODULE", "모듈", false);
            this._flex.SetCol("ID_MENU", "페이지명", false);
            this._flex.SetCol("CD_FILE", "파일코드", false);
            this._flex.SetCol("S", "S", 25, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("FILE_NAME", "관련파일명", 240);
            this._flex.SetCol("FILE_DATE", "파일날짜", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("FILE_TIME", "시간", 50);
            this._flex.SetCol("FILE_SIZE", "용량", 50);
            this._flex.SetCol("INSERT_DATE", "업로드일자", 120);
            this._flex.SetCol("INSERT_NAME", "업로더", 60);
            this._flex.SetCol("FILE_STATE", "상태", 80);

            this._flex.Cols["FILE_TIME"].TextAlign = TextAlignEnum.CenterCenter;
            this._flex.Cols["FILE_SIZE"].TextAlign = TextAlignEnum.RightCenter;
            this._flex.Cols["INSERT_DATE"].TextAlign = TextAlignEnum.CenterCenter;
            this._flex.Cols["INSERT_NAME"].TextAlign = TextAlignEnum.CenterCenter;
            this._flex.Cols["FILE_STATE"].TextAlign = TextAlignEnum.CenterCenter;
            this._flex.Cols["FILE_TIME"].Format = this._flex.Cols["FILE_TIME"].EditMask = "0#:##";
            this._flex.SetStringFormatCol("FILE_TIME");
            this._flex.SetNoMaskSaveCol("FILE_TIME");
            this._flex.SetDummyColumn("S");

            this._flex.SettingVersion = "1.0.0.0";
            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        private void InitEvent()
        {
            this.btn파일추가.Click += new EventHandler(this.btn파일추가_Click);
            this.btn선택삭제.Click += new EventHandler(this.btn선택삭제_Click);
            this.btn변경사항저장.Click += new EventHandler(this.btn변경사항저장_Click);
            this.btn선택다운로드.Click += new EventHandler(this.btn선택다운로드_Click);
            this.btn닫기.Click += new EventHandler(this.btn닫기_Click);

            this._flex.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this._flex.Binding = this.FileInfoSearch(this.모듈, this.파일코드);
            this.btn파일추가.Enabled = this.btn선택삭제.Enabled = this.btn변경사항저장.Enabled = this.UseGrant;
        }

        private void btn파일추가_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = true;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() != DialogResult.OK) return;

                DataTable tableFromGrid = this._flex.GetTableFromGrid();
                string[] fileNames = openFileDialog.FileNames;
                Decimal num = this.MaxNoSeq();
                
                foreach (string fileName in fileNames)
                {
                    FileInfo fileInfo = new FileInfo(fileName);
                    DataRow row = tableFromGrid.NewRow();
                    row["URL"] = fileInfo.FullName;
                    row["NO_SEQ"] = num++;
                    row["CD_MODULE"] = this.모듈;
                    row["ID_MENU"] = this.메뉴;
                    row["CD_FILE"] = this.파일코드;
                    row["FILE_PATH"] = this.txt서버파일경로.Text;
                    row["FILE_EXT"] = fileInfo.Extension.Replace(".", "");
                    row["FILE_NAME"] = fileInfo.Name;
                    row["FILE_DATE"] = fileInfo.LastWriteTime.ToString("yyyyMMdd");
                    row["FILE_TIME"] = fileInfo.LastWriteTime.ToString("hhmm");
                    row["FILE_SIZE"] = string.Format("{0:0.00}M", ((Decimal)fileInfo.Length / new Decimal(1048576)));
                    row["INSERT_DATE"] = DateTime.Now.ToString("yyyy/MM/dd hh:mm");
                    row["INSERT_NAME"] = Global.MainFrame.LoginInfo.UserName;
                    row["FILE_STATE"] = D.GetString(P_CZ_MA_FILE_SUB.상태.업로드대기);
                    tableFromGrid.Rows.Add(row);
                }

                this._flex.Binding = tableFromGrid;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn선택삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow) return;

                DataRow[] dataRowArray = this._flex.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);
                
                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    if (Global.MainFrame.ShowMessage("선택된 파일을 삭제하시겠습니까?\n('변경사항저장'을 클릭하면 실제로 삭제처리됩니다.)", "QY2") != DialogResult.Yes)
                        return;
                    
                    foreach (DataRow dataRow in dataRowArray)
                    {
                        switch (D.GetString(dataRow["FILE_STATE"]))
                        {
                            case "업로드대기":
                                dataRow.Delete();
                                break;
                            default:
                                dataRow["FILE_STATE"] = D.GetString(P_CZ_MA_FILE_SUB.상태.삭제대기);
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn변경사항저장_Click(object sender, EventArgs e)
        {
            string result;

            try
            {
                if (!this._flex.HasNormalRow)
                {
                    this.DialogResult = DialogResult.Cancel;
                }
                else
                {
                    DataRow[] dataRowArray = this._flex.DataTable.Select("FILE_STATE IS NOT NULL AND FILE_STATE <> ''", string.Empty, DataViewRowState.CurrentRows);

                    if (dataRowArray == null || dataRowArray.Length == 0)
                    {
                        Global.MainFrame.ShowMessage(공통메세지.변경된내용이없습니다);
                    }
                    else
                    {
                        string detail = string.Empty;
                        int pageCount = 0;

                        if (this.메뉴 == "P_CZ_SA_GIM_REG")
						{
                            string 회사코드 = this.파일코드.Split('_')[1];
                            string 협조전번호 = this.파일코드.Split('_')[0];

                            if (회사코드 == "K100" || 회사코드 == "K200")
							{
                                DataTable dt = DBHelper.GetDataTable(string.Format(@"SELECT ISNULL(WD.QT_RECIEPT_PAGE, 0) AS QT_RECIEPT_PAGE
                                                                                     FROM SA_GIRH GH WITH(NOLOCK)
                                                                                     JOIN CZ_SA_GIRH_WORK_DETAIL WD WITH(NOLOCK) ON WD.CD_COMPANY = GH.CD_COMPANY AND WD.NO_GIR = GH.NO_GIR
                                                                                     WHERE GH.CD_COMPANY = '{0}'
                                                                                     AND GH.NO_GIR = '{1}'
                                                                                     AND GH.TP_BUSI = '005'", 회사코드, 협조전번호));

                                if (dt == null || dt.Rows.Count == 0)
                                    pageCount = 0;
                                else
                                    pageCount = D.GetInt(dt.Rows[0]["QT_RECIEPT_PAGE"]);
                            }
                        }

                        bool isExists = false;

                        string filePath = Path.Combine(Application.StartupPath, this.임시폴더);
                        this.dirInfo = new DirectoryInfo(filePath);

                        if (!this.dirInfo.Exists)
                            this.dirInfo.Create();

                        foreach (DataRow dataRow in dataRowArray)
                        {
                            switch (D.GetString(dataRow["FILE_STATE"]))
                            {
                                case "업로드대기":
                                    if (this.메뉴 == "P_CZ_SA_GIM_REG" && pageCount > 0 && dataRow["FILE_EXT"].ToString().ToLower() == "pdf")
									{
                                        int pdfcount = PDF.GetPageCount(dataRow["URL"].ToString());

                                        if (pdfcount == pageCount)
                                            isExists = true;
									}

                                    dataRow["FILE_NAME"] = FileMgr.GetUniqueFileName((Global.MainFrame.HostURL + "/" + this.txt서버파일경로.Text + "/" + this.파일코드), D.GetString(dataRow["URL"]));
                                    result = FileUploader.UploadFile(D.GetString(dataRow["FILE_NAME"]), D.GetString(dataRow["URL"]), this.txt서버파일경로.Text, this.파일코드);

                                    if (result == "Success")
                                    {
                                        try
										{
                                            FileUploader.DownloadFile(D.GetString(dataRow["FILE_NAME"]), filePath, this.txt서버파일경로.Text, this.파일코드);
                                            FileInfo fileInfo = new FileInfo(filePath + "\\" + D.GetString(dataRow["FILE_NAME"]));
                                            fileInfo.Delete();
                                        }
                                        catch
										{
                                            detail += string.Format("- 파일명 : {0}\n  {1}\n", D.GetString(dataRow["FILE_NAME"]), result);
                                            dataRow.Delete();
                                            break;
                                        }

                                        dataRow["FILE_STATE"] = D.GetString(P_CZ_MA_FILE_SUB.상태.전송완료);
                                    }
                                    else
                                    {
                                        //dataRow["FILE_STATE"] = D.GetString(P_CZ_MA_FILE_SUB.상태.전송실패);
                                        detail += string.Format("- 파일명 : {0}\n  {1}\n", D.GetString(dataRow["FILE_NAME"]), result);
                                        dataRow.Delete();
                                    }
                                    break;
                                case "삭제대기":
                                    FileUploader.DeleteFile(this.txt서버파일경로.Text, this.파일코드, D.GetString(dataRow["FILE_NAME"]));
                                    dataRow.Delete();
                                    break;
                            }
                        }

                        if (this.메뉴 == "P_CZ_SA_GIM_REG" && pageCount > 0 && isExists == false)
                            Global.MainFrame.ShowMessage(string.Format("인수증 페이지수({0})와 같은 파일이 없습니다.", pageCount));

                        this.SaveData();

                        this.임시파일제거();

                        this._flex.Binding = this.FileInfoSearch(this.모듈, this.파일코드);

                        if (!string.IsNullOrEmpty(detail))
                            Global.MainFrame.ShowDetailMessage("파일 전송 중 오류가 발생했습니다.\n상세내역을 확인하세요. ↓", detail);
                        else
                            this.btn닫기_Click(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn선택다운로드_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow) return;

                DataRow[] dataRowArray = this._flex.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);
                
                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    Global.MainFrame.ShowMessage("다운로드할 파일을 선택해 주십시오.");
                }
                else
                {
                    FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                    if (folderBrowserDialog.ShowDialog() != DialogResult.OK) return;

                    MsgControl.ShowMsg("파일 다운로드 작업 중입니다..");

                    foreach (DataRow dataRow in dataRowArray)
                    {
                        FileUploader.DownloadFile(D.GetString(dataRow["FILE_NAME"]), folderBrowserDialog.SelectedPath, this.txt서버파일경로.Text, this.파일코드);
                    }

                    MsgControl.ShowMsg("파일 다운로드 작업을 완료했습니다.");
                    MsgControl.CloseMsg();
                }
            }
            catch (Exception ex)
            {
                MsgControl.CloseMsg();
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
            }
        }

        private void btn닫기_Click(object sender, EventArgs e)
        {
            try
            {
                this.FileCount = this._flex.Rows.Count;
                DataRow[] dataRowArray = this._flex.DataTable.Select("FILE_STATE = '전송완료'", "", DataViewRowState.CurrentRows);

                if (dataRowArray == null || dataRowArray.Length == 0)
                    this.DialogResult = DialogResult.Cancel;
                
                foreach (DataRow dataRow in dataRowArray)
                {
                    P_CZ_MA_FILE_SUB pMaFileSub = this;
                    string str = pMaFileSub.FileNames + D.GetString(dataRow["FILE_NAME"]) + "|";
                    pMaFileSub.FileNames = str;
                }

                this.DialogResult = DialogResult.OK;
                this.임시파일제거();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void SaveData()
        {
            if (this._flex.GetChanges() == null) return;

            SpInfo si = new SpInfo();
            si.DataValue = this._flex.GetChanges();
            si.CompanyID = this.회사;
            si.UserID = Global.MainFrame.LoginInfo.UserID;
            si.SpNameInsert = "UP_MA_FILEINFO_INSERT";
            si.SpParamsInsert = new string[] { "CD_COMPANY",
                                               "CD_MODULE",
                                               "ID_MENU",
                                               "CD_FILE",
                                               "NO_SEQ",
                                               "FILE_NAME",
                                               "FILE_PATH",
                                               "FILE_EXT",
                                               "FILE_DATE",
                                               "FILE_TIME",
                                               "FILE_SIZE",
                                               "ID_INSERT" };
            si.SpNameUpdate = "UP_MA_FILEINFO_UPDATE";
            si.SpParamsUpdate = new string[] { "CD_COMPANY",
                                               "CD_MODULE",
                                               "ID_MENU",
                                               "CD_FILE",
                                               "NO_SEQ",
                                               "FILE_NAME",
                                               "FILE_PATH",
                                               "FILE_EXT",
                                               "FILE_DATE",
                                               "FILE_TIME",
                                               "FILE_SIZE",
                                               "ID_UPDATE" };
            si.SpNameDelete = "UP_MA_FILEINFO_DELETE";
            si.SpParamsDelete = new string[] { "CD_COMPANY",
                                               "CD_MODULE",
                                               "ID_MENU",
                                               "CD_FILE",
                                               "NO_SEQ",
                                               "FILE_NAME" };
            DBHelper.Save(si);
            this._flex.AcceptChanges();
        }

        private void _flex_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                #region Old
                //bool regeditSetting = false;

                //if (!this._flex.HasNormalRow) return;

                //if (this._flex.MouseRow >= this._flex.Rows.Fixed && e.Button == MouseButtons.Left)
                //{
                //    if (D.GetString(this._flex["FILE_STATE"]) == D.GetString(P_CZ_MA_FILE_SUB.상태.삭제대기))
                //    {
                //        Global.MainFrame.ShowMessage("상태가 삭제대기인 파일은 수정하실 수 없습니다.");
                //    }
                //    else
                //    {
                //        RegistryKey registryKey1 = Registry.ClassesRoot.OpenSubKey("Excel.Sheet.8\\shell\\Open\\command", true);
                //        RegistryKey registryKey2 = Registry.ClassesRoot.OpenSubKey("Excel.Sheet.12\\shell\\Open\\command", true);
                //        string string1 = D.GetString(registryKey1.GetValue(""));
                //        string string2 = D.GetString(registryKey2.GetValue(""));
                //        this.regeditDefaut1 = string1;
                //        this.regeditDefaut2 = string2;
                //        this.regeditDefautCommand1 = registryKey1.GetValue("command");
                //        this.regeditDefautCommand2 = registryKey2.GetValue("command");

                //        if (!string1.Contains("/en") || !string2.Contains("/en"))
                //        {
                //            this.regeditUpdateStart();
                //            regeditSetting = true;
                //        }

                //        if (!string.IsNullOrEmpty(D.GetString(this._flex["URL"])))
                //        {
                //            Process.Start(D.GetString(this._flex["URL"]));
                //        }
                //        else
                //        {
                //            string str1 = Path.Combine(Application.StartupPath, this.임시폴더);
                //            string string3 = D.GetString(this._flex["FILE_NAME"]);
                //            string fileName = Path.Combine(str1, string3);
                //            this.dirInfo = new DirectoryInfo(str1);

                //            if (!this.dirInfo.Exists)
                //                this.dirInfo.Create();

                //            FileUploader.DownloadFile(string3, str1, this.txt서버파일경로.Text, this.파일코드);

                //            string string4 = D.GetString(new FileInfo(fileName).LastWriteTime);
                //            Process process = new Process();
                //            process.StartInfo.FileName = str1 + "\\" + string3;
                //            process.Start();
                //            process.WaitForExit();
                //            FileInfo fileInfo1 = new FileInfo(fileName);

                //            if (string4 != D.GetString(fileInfo1.LastWriteTime))
                //            {
                //                this._flex["FILE_STATE"] = P_CZ_MA_FILE_SUB.상태.삭제대기;

                //                if (D.GetString(this._flex["FILE_STATE"]) != D.GetString(P_CZ_MA_FILE_SUB.상태.업로드대기))
                //                {
                //                    DataTable tableFromGrid = this._flex.GetTableFromGrid();
                //                    Decimal num2 = this.MaxNoSeq();
                //                    FileInfo fileInfo2 = new FileInfo(fileName);
                //                    DataRow row = tableFromGrid.NewRow();
                //                    row["URL"] = fileInfo2.FullName;
                //                    DataRow dataRow1 = row;
                //                    string index1 = "NO_SEQ";
                //                    Decimal d = num2;
                //                    dataRow1[index1] = d++;
                //                    row["CD_MODULE"] = this.모듈;
                //                    row["ID_MENU"] = this.메뉴;
                //                    row["CD_FILE"] = this.파일코드;
                //                    row["FILE_PATH"] = this.txt서버파일경로.Text;
                //                    row["FILE_EXT"] = fileInfo2.Extension.Replace(".", "");
                //                    row["FILE_NAME"] = fileInfo2.Name;
                //                    row["FILE_DATE"] = fileInfo2.LastWriteTime.ToString("yyyyMMdd");
                //                    DataRow dataRow2 = row;
                //                    string index2 = "FILE_TIME";
                //                    DateTime dateTime = fileInfo2.LastWriteTime;
                //                    string str2 = dateTime.ToString("hhmm");
                //                    dataRow2[index2] = str2;
                //                    row["FILE_SIZE"] = string.Format("{0:0.00}M", ((Decimal)fileInfo2.Length / new Decimal(1048576)));
                //                    DataRow dataRow3 = row;
                //                    string index3 = "INSERT_DATE";
                //                    dateTime = DateTime.Now;
                //                    string str3 = dateTime.ToString("yyyy/MM/dd hh:mm");
                //                    dataRow3[index3] = str3;
                //                    row["INSERT_NAME"] = Global.MainFrame.LoginInfo.UserName;
                //                    row["FILE_STATE"] = D.GetString(P_CZ_MA_FILE_SUB.상태.업로드대기);
                //                    tableFromGrid.Rows.Add(row);
                //                    this._flex.Binding = tableFromGrid;
                //                }

                //                this.btn변경사항저장_Click(null, null);
                //            }
                //        }

                //        if (regeditSetting) this.regeditUpdateEnd();
                //    }
                //}
                #endregion

                if (this._flex.HasNormalRow == false) return;
                if (this._flex.MouseRow < this._flex.Rows.Fixed) return;

                if (!string.IsNullOrEmpty(D.GetString(this._flex["URL"])))
                {
                    Process.Start(D.GetString(this._flex["URL"]));
                }
                else
                {
                    string str1 = Path.Combine(Application.StartupPath, this.임시폴더);
                    string string3 = D.GetString(this._flex["FILE_NAME"]);
                    string fileName = Path.Combine(str1, string3);
                    this.dirInfo = new DirectoryInfo(str1);

                    if (!this.dirInfo.Exists)
                        this.dirInfo.Create();

                    FileUploader.DownloadFile(string3, str1, this.txt서버파일경로.Text, this.파일코드);

                    string string4 = D.GetString(new FileInfo(fileName).LastWriteTime);
                    Process process = new Process();
                    process.StartInfo.FileName = str1 + "\\" + string3;
                    process.Start();
                }
            }
            catch (Exception ex)
            {
                this.regeditUpdateEnd();
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private DataTable FileInfoSearch(string MODULE, string FILECODE)
        {
            return DBHelper.GetDataTable("UP_MA_FILEINFO_SELECT", new object[] { this.회사,
                                                                                 MODULE,
                                                                                 FILECODE });
        }

        private decimal MaxNoSeq()
        {
            decimal d = 1;

            for (int @fixed = this._flex.Rows.Fixed; @fixed < this._flex.Rows.Count; ++@fixed)
            {
                decimal @decimal = D.GetDecimal(this._flex.Rows[@fixed]["NO_SEQ"]);
                if (d < @decimal) 
                    d = @decimal;
            }

            return ++d;
        }

        protected void regeditUpdateStart()
        {
            RegistryKey registryKey1 = Registry.ClassesRoot.OpenSubKey(".xls", true);
            RegistryKey registryKey2 = Registry.ClassesRoot.OpenSubKey(".xlsx", true);

            if (D.GetString(registryKey1.GetValue("")) == "Excel.Sheet.8")
            {
                if (Registry.ClassesRoot.OpenSubKey("Excel.Sheet.8\\shell\\Open\\ddeexec\\application", true) != null)
                    Registry.ClassesRoot.DeleteSubKey("Excel.Sheet.8\\shell\\Open\\ddeexec\\application");

                if (Registry.ClassesRoot.OpenSubKey("Excel.Sheet.8\\shell\\Open\\ddeexec\\topic", true) != null)
                    Registry.ClassesRoot.DeleteSubKey("Excel.Sheet.8\\shell\\Open\\ddeexec\\topic");

                if (Registry.ClassesRoot.OpenSubKey("Excel.Sheet.8\\shell\\Open\\ddeexec", true) != null)
                    Registry.ClassesRoot.DeleteSubKey("Excel.Sheet.8\\shell\\Open\\ddeexec");

                if (Registry.ClassesRoot.OpenSubKey("Excel.Sheet.8\\shell\\Open\\command", true) != null)
                {
                    RegistryKey registryKey3 = Registry.ClassesRoot.OpenSubKey("Excel.Sheet.8\\shell\\Open\\command", true);
                    string str1 = registryKey3.GetValue("").ToString();
                    int length = str1.LastIndexOf("EXCEL.EXE");
                    string str2 = str1.Substring(0, length) + "EXCEL.EXE\" /en \"%1\"";
                    registryKey3.SetValue("", str2);

                    if (D.GetString(registryKey3.GetValue("command")) != "")
                        registryKey3.DeleteValue("command");
                }
            }

            if (!(D.GetString(registryKey2.GetValue("")) == "Excel.Sheet.12"))
                return;

            if (Registry.ClassesRoot.OpenSubKey("Excel.Sheet.12\\shell\\Open\\ddeexec\\application", true) != null)
                Registry.ClassesRoot.DeleteSubKey("Excel.Sheet.12\\shell\\Open\\ddeexec\\application");

            if (Registry.ClassesRoot.OpenSubKey("Excel.Sheet.12\\shell\\Open\\ddeexec\\topic", true) != null)
                Registry.ClassesRoot.DeleteSubKey("Excel.Sheet.12\\shell\\Open\\ddeexec\\topic");

            if (Registry.ClassesRoot.OpenSubKey("Excel.Sheet.12\\shell\\Open\\ddeexec", true) != null)
                Registry.ClassesRoot.DeleteSubKey("Excel.Sheet.12\\shell\\Open\\ddeexec");

            if (Registry.ClassesRoot.OpenSubKey("Excel.Sheet.12\\shell\\Open\\command", true) != null)
            {
                RegistryKey registryKey3 = Registry.ClassesRoot.OpenSubKey("Excel.Sheet.12\\shell\\Open\\command", true);
                string str1 = registryKey3.GetValue("").ToString();
                int length = str1.LastIndexOf("EXCEL.EXE");
                string str2 = str1.Substring(0, length) + "EXCEL.EXE\" /en \"%1\"";
                registryKey3.SetValue("", str2);

                if (D.GetString(registryKey3.GetValue("command")) != "")
                    registryKey3.DeleteValue("command");
            }
        }

        protected void regeditUpdateEnd()
        {
            RegistryKey registryKey1 = Registry.ClassesRoot.OpenSubKey(".xls", true);
            RegistryKey registryKey2 = Registry.ClassesRoot.OpenSubKey(".xlsx", true);
            string str;

            if (D.GetString(registryKey1.GetValue("")) == "Excel.Sheet.8")
            {
                RegistryKey registryKey3 = Registry.ClassesRoot.OpenSubKey("Excel.Sheet.8\\shell\\Open\\command", true);

                if (Registry.ClassesRoot.OpenSubKey("Excel.Sheet.8\\shell\\Open\\command", true) != null)
                    registryKey3.SetValue("", this.regeditDefaut1);

                if (!this.regeditDefaut1.Contains("/en"))
                {
                    str = string.Empty;
                    string[] strArray2;

                    if (this.regeditDefautCommand1 != null)
                    {
                        if (this.regeditDefautCommand1 is string)
                            strArray2 = new string[] { D.GetString(this.regeditDefautCommand1) };
                        else
                            strArray2 = this.regeditDefautCommand1 as string[];
                    }
                    else
                        strArray2 = new string[] { "vUpAV5!!!!!!!!!MKKSkEXCELFiles>tW{~$4Q]c@II=l2xaTO5 /e" };

                    if (D.GetString(registryKey3.GetValue("command")) == "")
                        registryKey3.SetValue("command", strArray2, RegistryValueKind.MultiString);
                    
                    if (Registry.ClassesRoot.OpenSubKey("Excel.Sheet.8\\shell\\Open\\ddeexec", true) == null)
                    {
                        Registry.ClassesRoot.CreateSubKey("Excel.Sheet.8\\shell\\Open\\ddeexec");
                        Registry.ClassesRoot.OpenSubKey("Excel.Sheet.8\\shell\\Open\\ddeexec", true).SetValue("", "[open(\"%1\")]");
                    }

                    if (Registry.ClassesRoot.OpenSubKey("Excel.Sheet.8\\shell\\Open\\ddeexec\\application", true) == null)
                    {
                        Registry.ClassesRoot.CreateSubKey("Excel.Sheet.8\\shell\\Open\\ddeexec\\application");
                        Registry.ClassesRoot.OpenSubKey("Excel.Sheet.8\\shell\\Open\\ddeexec\\application", true).SetValue("", "Excel");
                    }

                    if (Registry.ClassesRoot.OpenSubKey("Excel.Sheet.8\\shell\\Open\\ddeexec\\topic", true) == null)
                    {
                        Registry.ClassesRoot.CreateSubKey("Excel.Sheet.8\\shell\\Open\\ddeexec\\topic");
                        Registry.ClassesRoot.OpenSubKey("Excel.Sheet.8\\shell\\Open\\ddeexec\\topic", true).SetValue("", "system");
                    }
                }
            }

            if (!(D.GetString(registryKey2.GetValue("")) == "Excel.Sheet.12")) return;

            RegistryKey registryKey4 = Registry.ClassesRoot.OpenSubKey("Excel.Sheet.12\\shell\\Open\\command", true);

            if (Registry.ClassesRoot.OpenSubKey("Excel.Sheet.12\\shell\\Open\\command", true) != null)
                registryKey4.SetValue("", this.regeditDefaut2);

            if (!this.regeditDefaut2.Contains("/en"))
            {
                str = string.Empty;
                string[] strArray2;
                
                if (this.regeditDefautCommand2 != null)
                {
                    if (this.regeditDefautCommand2 is string)
                        strArray2 = new string[] { D.GetString(this.regeditDefautCommand2) };
                    else
                        strArray2 = this.regeditDefautCommand2 as string[];
                }
                else
                    strArray2 = new string[] { "vUpAV5!!!!!!!!!MKKSkEXCELFiles>tW{~$4Q]c@II=l2xaTO5 /e" };
                
                if (D.GetString(registryKey4.GetValue("command")) == "")
                    registryKey4.SetValue("command", strArray2, RegistryValueKind.MultiString);
                
                if (Registry.ClassesRoot.OpenSubKey("Excel.Sheet.12\\shell\\Open\\ddeexec", true) == null)
                {
                    Registry.ClassesRoot.CreateSubKey("Excel.Sheet.12\\shell\\Open\\ddeexec");
                    Registry.ClassesRoot.OpenSubKey("Excel.Sheet.12\\shell\\Open\\ddeexec", true).SetValue("", "[open(\"%1\")]");
                }
                
                if (Registry.ClassesRoot.OpenSubKey("Excel.Sheet.12\\shell\\Open\\ddeexec\\application", true) == null)
                {
                    Registry.ClassesRoot.CreateSubKey("Excel.Sheet.12\\shell\\Open\\ddeexec\\application");
                    Registry.ClassesRoot.OpenSubKey("Excel.Sheet.12\\shell\\Open\\ddeexec\\application", true).SetValue("", "Excel");
                }
                
                if (Registry.ClassesRoot.OpenSubKey("Excel.Sheet.12\\shell\\Open\\ddeexec\\topic", true) == null)
                {
                    Registry.ClassesRoot.CreateSubKey("Excel.Sheet.12\\shell\\Open\\ddeexec\\topic");
                    Registry.ClassesRoot.OpenSubKey("Excel.Sheet.12\\shell\\Open\\ddeexec\\topic", true).SetValue("", "system");
                }
            }
        }

        private enum 상태
        {
            None,
            업로드대기,
            삭제대기,
            전송중,
            서버처리중,
            전송완료,
            전송실패,
        }

        private void 임시파일제거()
        {
            DirectoryInfo dirInfo;
            bool isExistFile;

            try
            {
                dirInfo = new DirectoryInfo(Path.Combine(Application.StartupPath, this.임시폴더));
                isExistFile = false;

                if (dirInfo.Exists == true)
                {
                    foreach (FileInfo file in dirInfo.GetFiles("*", SearchOption.AllDirectories))
                    {
                        try
                        {
                            file.Delete();
                        }
                        catch
                        {
                            isExistFile = true;
                            continue;
                        }
                    }

                    if (isExistFile == false)
                        dirInfo.Delete(true);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
    }
}

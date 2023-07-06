using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Duzon.Common.Forms;
using Duzon.Common.BpControls;
using DzHelpFormLib;
using Duzon.Windows.Print;

using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.ERPU;
using Dintec;
using Duzon.Common.Util;
using System.Data.SqlClient;
using System.IO;
using System.Diagnostics;
using System.Net;
using Duzon.ERPU.Utils;
using Duzon.ERPU.MF;


namespace cz
{
    public partial class P_CZ_HR_PHONE_PAY_LIST : PageBase
    {
        #region 생성자
        P_CZ_HR_PHONE_PAY_LIST_BIZ _biz = new P_CZ_HR_PHONE_PAY_LIST_BIZ();

        //private DirectoryInfo dirInfo = null;

        public string CD_COMPANY { get; set; }

        public string YM { get; set; }

        public string NO_EMP { get; set; }

        public string YM_PAY { get; set; }

        public string GUBUN { get; set; }

        public string NO_DOCU = string.Empty;
        #endregion 생성자


        #region 초기화
        public P_CZ_HR_PHONE_PAY_LIST()
        {
            StartUp.Certify(this);
            CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
            InitializeComponent();
        }

        protected override void InitLoad()
        {
            InitControl();
            InitGrid();
            InitEvent();
        }

        private void InitControl()
        {
            dtp급여반영월.Text = Util.GetToday();

            YM = dtp급여반영월.Text;

            DataTable dt = new DataTable();
            dt.Columns.Add("CODE");
            dt.Columns.Add("NAME");
            dt.Rows.Add("", "");
            dt.Rows.Add("Y", "승인");
            dt.Rows.Add("N", "미승인");

            cbo처리구분.ValueMember = "CODE";
            cbo처리구분.DisplayMember = "NAME";
            cbo처리구분.DataSource = dt;

            this.btn승인.Enabled = false;
            this.btn승인취소.Enabled = false;
            this.btn전표처리.Enabled = false;
            this.btn전표이동.Enabled = false;
            this.btn전표취소.Enabled = false;
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flexH };

            _flexH.BeginSetting(1, 1, false);

            _flexH.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            _flexH.SetCol("YN_PAY", "승인", 40, true, CheckTypeEnum.Y_N);
            _flexH.SetCol("YM", "지급반영월", 70, false, typeof(string), FormatTpType.YEAR_MONTH);
            _flexH.SetCol("NO_EMP", "사번", 50);
            _flexH.SetCol("NM_CC", "코스트센터", 70);
            _flexH.SetCol("NM_DUTY_RANK", "직급", 70);
            _flexH.SetCol("NM_KOR", "성명", 60);
            _flexH.SetCol("AM_TOTAL", "명세금액", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flexH.SetCol("AM_S", "국내통화", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flexH.SetCol("AM_ROAMING", "국제통화", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flexH.SetCol("AM", "지급액", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flexH.SetCol("NM_TITLE", "제목", 230);
            _flexH.SetCol("NO_BANK1", "입금계좌", 200);
            _flexH.SetCol("NO_GWDOCU", "문서번호", false);
            //_flexH.SetCol("FILE_PATH_MNG", "첨부파일", false);
            _flexH.SetCol("DT_REG", "작성일", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexH.SetCol("DC_RMK", " 비고", 180);
            _flexH.SetCol("GUBUN", "구분", false);
            _flexH.SetCol("NO_DOCU", "전표번호", false);

            _flexH.Cols["NO_BANK1"].TextAlign = TextAlignEnum.CenterCenter;
            _flexH.Cols["NO_EMP"].TextAlign = TextAlignEnum.CenterCenter;
            _flexH.Cols["NM_KOR"].TextAlign = TextAlignEnum.CenterCenter;
            _flexH.Cols["NM_CC"].TextAlign = TextAlignEnum.CenterCenter;
            _flexH.Cols["YN_PAY"].TextAlign = TextAlignEnum.CenterCenter;
            _flexH.Cols["NM_DUTY_RANK"].TextAlign = TextAlignEnum.CenterCenter;

            _flexH.Cols["NO_DOCU"].TextAlign = TextAlignEnum.CenterCenter;

            _flexH.SettingVersion = "0.0.0.1";
            _flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            _flexH.SetDummyColumn(new string[] { "S" });
        }

        private void InitEvent()
        {
            this.btn승인.Click += new EventHandler(this.btn승인_Click);
            this.btn승인취소.Click += new EventHandler(this.btn승인_Click);
            this.btn전표처리.Click += new EventHandler(this.btn전표처리_Click);
            this.btn전표취소.Click += new EventHandler(this.btn전표처리_Click);
            this.btn전표이동.Click += new EventHandler(this.btn전표이동_Click);
            this.btn파일보기.Click += new EventHandler(this.btn파일보기_Click);
            this._flexH.Click += new EventHandler(this.flexH_Click);
            this._flexH.DoubleClick += new EventHandler(this.flexH_DoubleClick);
            this.btn자동승인.Click += new EventHandler(this.btn자동승인_Click);
        }
        #endregion 초기화
        

        #region 버튼
        private void btn승인_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;
            string name;

            try
            {
                if (!this._flexH.HasNormalRow) return;

                name = ((Control)sender).Name;
                dataRowArray = this._flexH.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    foreach (DataRow dr in dataRowArray)
                    {
                        if (name == this.btn승인.Name)
                            dr["YN_PAY"] = "Y";
                        else
                            dr["YN_PAY"] = "N";
                    }

                    if (this._biz.Save(this._flexH.GetChanges()))
                    {
                        this._flexH.AcceptChanges();
                        this.ShowMessage(공통메세지._작업을완료하였습니다, ((Control)sender).Text);
                        this.OnToolBarSearchButtonClicked(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn전표처리_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;
            string name, 참조번호;
            bool result;

            try
            {
                if (!this._flexH.HasNormalRow) return;

                name = ((Control)sender).Name;

                dataRowArray = this._flexH.DataTable.Select("YN_PAY = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage("승인처리된 자료가 존재하지 않습니다.");
                    return;
                }
                else
                {
                    if (name == this.btn전표처리.Name)
                    {
                        result = this._biz.전표처리(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                   this.dtp급여반영월.Text,
                                                                   "D01",
                                                                   Global.MainFrame.LoginInfo.UserID });

                        if (result)
                        {
                            this.OnToolBarSearchButtonClicked(null, null);
                            this.ShowMessage("전표가 처리되었습니다");
                        }
                    }
                    else
                    {
                        참조번호 = "PHONE_" + this.dtp급여반영월.Text;

                        result = this._biz.전표취소(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                   "D01",
                                                                   참조번호,
                                                                   Global.MainFrame.LoginInfo.UserID });

                        if (result)
                        {
                            this.OnToolBarSearchButtonClicked(null, null);
                            this.ShowMessage("전표가 취소되었습니다");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn전표이동_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(D.GetString(this._flexH["NO_DOCU"])))
                {
                    this.ShowMessage("생성된 전표가 없습니다.");
                    return;
                }


                // 코스트센터 : 전략구매팀 --> 영업총괄 일괄 변경
                //for (int i = 0; i < _flexH.Rows.Count - 1; i++)
                //{
                //    string NM_CC = _flexH[i,"NM_CC"].ToString();

                //    if (NM_CC.Equals("전략구매팀"))
                //    {
                //        _flexH[i,"NM_CC"] = "영업총괄";
                    
                //    }
                //}


                this.CallOtherPageMethod("P_FI_DOCU", "전표입력(" + this.PageName + ")", "P_FI_DOCU", this.Grant, new object[] { D.GetString(this._flexH["NO_DOCU"]),
                                                                                                                                 "1",
                                                                                                                                 Global.MainFrame.LoginInfo.CdPc,
                                                                                                                                 Global.MainFrame.LoginInfo.CompanyCode });
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #region 파일보기
        
        private void btn파일보기_Click(object sender, EventArgs e)
        {

            string fileName = string.Empty;

            try
            {
                if (!this._flexH.HasNormalRow)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    YM = dtp급여반영월.Text;

                    string _ym = this._flexH["YM"].ToString();
                    string _noemp = this._flexH["NO_EMP"].ToString();

                    string file_code = D.GetString("HP" + _ym + "_" + _noemp + "_" + Global.MainFrame.LoginInfo.CompanyCode); //파일 PK설정 
                    P_CZ_MA_FILE_SUB m_dlg = new P_CZ_MA_FILE_SUB(Global.MainFrame.LoginInfo.CompanyCode, "CZ", "P_CZ_HR_PHONE_PAY", file_code, "P_CZ_HR_PHONE_PAY");
                    m_dlg.ShowDialog(this);

                    fileName = this._biz.SearchFileInfo(Global.MainFrame.LoginInfo.CompanyCode, file_code);

                    if (!string.IsNullOrEmpty(fileName))
                    {
                        tbx첨부파일.Text = fileName;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }


            


            //try
            //{
            //    if (_flexH.Rows.Count > 2)
            //    {
            //        string serverFile = _flexH["FILE_PATH_MNG"].ToString();
    
            //        YM = dtp급여반영월.Text;

            //        string file_code = D.GetString("HP" + YM + "_" + NO_EMP + "_" + Global.MainFrame.LoginInfo.CompanyCode); //파일 PK설정 

            //        string str1 = Path.Combine(Application.StartupPath, "temp");
            //        string string3 = D.GetString(this.tbx첨부파일.Text);
            //        string fileName = Path.Combine(str1, string3);
            //        this.dirInfo = new DirectoryInfo(str1);

            //        if (!this.dirInfo.Exists)
            //            this.dirInfo.Create();

            //        FileUploader.DownloadFile(string3, str1, "upload/P_CZ_HR_PHONE_PAY", file_code);

            //        string string4 = D.GetString(new FileInfo(fileName).LastWriteTime);
            //        Process process = new Process();
            //        process.StartInfo.FileName = str1 + "\\" + string3;
            //        process.Start();
            //        process.WaitForExit();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //MsgEnd(ex);
            //}
        }


        public void TestMethod()
        {
            if (_flexH.Rows.Count > 2)
            {
                string serverFile = _flexH["FILE_PATH_MNG"].ToString();

                YM = dtp급여반영월.Text;

                string file_code = D.GetString("");
            }
        }

        public static string GetUniqueFileName(string fullPathFile)
        {
            FileInfo file = new FileInfo(fullPathFile);
            string newName = file.Name.Replace(file.Extension, "");
            string path = GetPath(fullPathFile);

            int index = 0;

            while (File.Exists(path + @"\" + newName + file.Extension))
            {
                index++;
                newName = file.Name.Replace(file.Extension, "") + "(" + index + ")";
            }

            return newName + file.Extension;
        }

        public static string GetPath(string fullPathFile)
        {
            return fullPathFile.Substring(0, fullPathFile.LastIndexOf(@"\"));
        }

        public static void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }
        #endregion 파일보기


        #region 자동승인

        private void btn자동승인_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global.MainFrame.ShowMessage("자동등록을 실행 하시겠습니까?", "QY2") == DialogResult.Yes)
                {
                    //CD_COMPANY = "K100";

                    string query = "SELECT * FROM CZ_HR_PHONE_PAY_REG WHERE YM = '" + YM + "' AND CD_COMPANY='" + CD_COMPANY + "' AND GUBUN ='002' AND NO_EMP NOT IN (SELECT NO_EMP FROM CZ_HR_PHONE_PAY WHERE YM = '" + YM + "')";
                    DataTable dt = DBMgr.GetDataTable(query);

                    if (dt.Rows.Count >= 1)
                    {
                        foreach (DataRow datarow in dt.Rows)
                        {
                            DBHelper.ExecuteNonQuery("SP_CZ_HR_PHONE_PAY_AUTO_I", new object[] { 
                            datarow["CD_COMPANY"],
                            datarow["YM"],
                            datarow["NO_EMP"],
                            datarow["NO_EMP"] + "_자동등록",
                            Global.MainFrame.LoginInfo.UserID
                            });
                        }
                        this.OnToolBarSearchButtonClicked(null, null);

                        this.ShowMessage("자동등록 완료 되었습니다.");
                    }
                    else
                    {
                        this.ShowMessage("자동등록 인원이 없습니다.");
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion 자동승인


        #endregion 버튼


        #region 조회
        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch()) return false;

            return true;
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {

                if (!BeforeSearch()) return;

                this._flexH.Binding = this._biz.Search(new object[] { CD_COMPANY,
                                                                      dtp급여반영월.Text,
                                                                      cbx사원.QueryWhereIn_Pipe,
                                                                      cbx부서.QueryWhereIn_Pipe,
                                                                      cbo처리구분.SelectedValue
                                                                       });

                if (!_flexH.HasNormalRow)
                {
                    ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
                else
                {
                    this.btn승인.Enabled = true;
                    this.btn승인취소.Enabled = true;
                    this.btn전표처리.Enabled = true;
                    this.btn전표이동.Enabled = true;
                    this.btn전표취소.Enabled = true;


                    flexH_Click(null, null);
                    NO_EMP = _flexH["NO_EMP"].ToString();
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion 조회


        #region 인쇄
        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            ReportHelper reportHelper;
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            //int counti = 0;
               

            try
            {
                
                if (this._flexH.HasNormalRow == false) return;
                //-------------------------------------------dt1
                //NM_CC,         AM,           QT_P
                //코스트센터     금액합계      인원합계
                dt1 = DBMgr.GetDataTable("SP_CZ_HR_PHONE_PAY_P_1", new object[] {CD_COMPANY,YM});


                //-------------------------------------------dt2
                //NM_CC,        NM_DUTY_RANK,   NM_KOR,     AM_TOTAL,       AM_ALL,         AM_ROAMING,         AM,             NO_BANK1,   DC_RMK     
                //코스트센터    직급            성명        명세서요금      기본지원금      해외통화요금        지급액          계좌번호    비고
                dt2 = DBMgr.GetDataTable("SP_CZ_HR_PHONE_PAY_P_2", new object[] { CD_COMPANY, YM });

                //DataRow[] findRow = dt2.Select("NO_BANK1 = '부산은행 / 112-2041-4322-01'");

                //if (findRow != null)
                //{
                //    for (int i = 0; i < dt2.Rows.Count - 1; i++)
                //    {
                //        if (dt2.Rows[i][7].ToString().Equals("부산은행 / 112-2041-4322-01"))
                //        {
                //            counti = i;
                //        }

                //        dt2.Rows[counti][7] = "부산은행 / 051-12-086150-2";
                //    }
                //}

                ////인쇄에서만 사용하는 것이고, 조회화면에서는 쿼리에서 수정했음. 
                //DataRow[] findRow2 = dt2.Select("NO_BANK1 = '부산은행 / 105-12-010330-1'"); //부산은행 / 105-12-010330-1

                //if (findRow != null)
                //{
                //    for (int i = 0; i < dt2.Rows.Count - 1; i++)
                //    {
                //        if (dt2.Rows[i][7].ToString().Equals("부산은행 / 105-12-010330-1"))
                //        {
                //            counti = i;
                //        }

                //        dt2.Rows[counti][7] = "국민은행 / 102-21-1014-922";
                //    }
                //}
                    

                //reportHelper = Util.SetRPT("R_CZ_HR_PHONE_PAY", "휴대폰요금정산처리", "", dt1, dt2);
                reportHelper = Util.SetReportHelper(Util.GetReportFileName("R_CZ_HR_PHONE_PAY", this.LoginInfo.CompanyCode), "휴대폰요금정산처리", this.LoginInfo.CompanyCode);

                reportHelper.SetDataTable(dt1, 1);
                reportHelper.SetDataTable(dt2, 2);
                reportHelper.SetData("YM", YM);

                Util.RPT_Print(reportHelper);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion 인쇄


        #region 그리드이벤트

        private void flexH_Click(object sender, EventArgs e)
        {
            try
            {
                if (_flexH.Rows.Count > 2) 
                {
                    NO_EMP = _flexH["NO_EMP"].ToString();
                    YM = _flexH["YM"].ToString();

                    string file_code = D.GetString("HP" + YM + "_" + NO_EMP + "_" + Global.MainFrame.LoginInfo.CompanyCode); //파일 PK설정 
                    tbx첨부파일.Text = _biz.SearchFileInfo(Global.MainFrame.LoginInfo.CompanyCode, file_code);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public void flexH_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (this._flexH.Cols[_flexH.Col].Name == "FILE_PATH_MNG")
                {
                    btn파일보기_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
       
        #endregion 그리드이벤트
    }
}

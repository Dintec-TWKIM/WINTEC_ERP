using System;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.BizOn.Tax.Common;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.OLD;

namespace cz
{
    public partial class P_CZ_FI_TAX_MNG : Duzon.Common.Forms.PageBase
    {
        #region 생성자 & 전역변수
        P_CZ_FI_TAX_MNG_BIZ _biz;

        private string RPT_TM
        {
            get
            {
                if (this.dtp청구일자.EndDateToString != null && this.dtp청구일자.EndDateToString.Length >= 6)
                {
                    DateTime dateTime = this.dtp청구일자.EndDate;
                    if (dateTime.Month >= 1 && dateTime.Month <= 3)
                        return this.dtp청구일자.EndDateToString.Substring(0, 4) + "11";
                    if (dateTime.Month >= 4 && dateTime.Month <= 6)
                        return this.dtp청구일자.EndDateToString.Substring(0, 4) + "12";
                    if (dateTime.Month >= 7 && dateTime.Month <= 9)
                        return this.dtp청구일자.EndDateToString.Substring(0, 4) + "21";
                    if (dateTime.Month >= 10 && dateTime.Month <= 12)
                        return this.dtp청구일자.EndDateToString.Substring(0, 4) + "22";
                }
                return string.Empty;
            }
        }

        private string 부가세신고여부
        {
            get
            {
                if (this.rdo부가세전체.Checked == true)
                    return "A";
                else if (this.rdo부가세신고.Checked == true)
                    return "S";
                else
                    return "M";
            }
        }

        private string 전표승인여부
        {
            get
            {
                if (this.rbo전표전체.Checked == true)
                    return "A";
                else if (this.rbo전표승인.Checked == true)
                    return "2";
                else
                    return "1";
            }
        }

        public P_CZ_FI_TAX_MNG()
        {
            StartUp.Certify(this);
            InitializeComponent();

            this.MainGrids = new FlexGrid[] { this._flex };
            this._biz = new P_CZ_FI_TAX_MNG_BIZ();
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);

            this._flex.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("DT_PROCESS", "청구일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("NO_IV", "계산서번호", 100);
            this._flex.SetCol("NO_SO", "수주번호", 100);
            this._flex.SetCol("NM_SO", "수주유형", 100);
            this._flex.SetCol("NM_VESSEL", "호선명", 120);
            this._flex.SetCol("NM_PARTNER", "매출처명", 120);
            this._flex.SetCol("NO_PO_PARTNER", "주문번호", 100);
            this._flex.SetCol("AM_CLS", "비용", 80, false, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("NM_EXCH", "수주통화", 80);
            this._flex.SetCol("AM_IV_EX", "매출외화", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex.SetCol("AM_IV", "매출원화", 80, false, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("AM_EX", "신고외화", 80, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex.SetCol("AM", "신고원화", 80, true, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("RT_DELIVERY", "납품환율", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flex.SetCol("DT_LOADING", "적재일자", 100, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("DT_VAT", "부가세신고일자", 100, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("DT_TO", "수출신고일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("NO_TO", "수출신고번호", 120);
            this._flex.SetCol("DT_SHIPPING", "수출선적일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("NO_IO", "출고번호", 100);
            this._flex.SetCol("DC_RMK", "비고", 120, true);
            this._flex.SetCol("NM_TRANS_PARTNER", "배송업체", 120);
            this._flex.SetCol("TP_EXPORT", "수출구분", 80, true);
            this._flex.SetCol("NM_MAIN_CATEGORY", "구분1", 100);
            this._flex.SetCol("NM_SUB_CATEGORY", "구분2", 100);
            this._flex.SetCol("NO_HEESC", "HEESC 번호", 100);
            this._flex.SetCol("NO_SINGAPORE", "SINGAPORE 번호", 100);
            this._flex.SetCol("FILE_PATH_MNG", "인수증", 200);
            this._flex.SetCol("FILE_PATH_MNG1", "기타파일", 200);

            this._flex.SetDummyColumn(new string[] { "S", "FILE_PATH_MNG", "FILE_PATH_MNG1" });

            this._flex.SetDataMap("TP_EXPORT", this.GetComboDataCombine("S;CZ_FI00001"), "CODE", "NAME");
            
            this._flex.SettingVersion = "1.0.0.0";
            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex.SetExceptSumCol(new string[] { "RT_DELIVERY" });

            this._flex.Cols.Frozen = 4;
            this._flex.SetStringFormatCol2("NO_TO", "AAA-AA-AAAAAAAAAA");
        }

        private void InitEvent()
        {
            this.btn엑셀양식다운로드.Click += new EventHandler(this.btn엑셀양식다운로드_Click);
            this.btn엑셀업로드.Click += new EventHandler(this.btn엑셀업로드_Click);

            this.btn전산매체작성.Click += new EventHandler(this.btn전산매체작성_Click);

            this._flex.DoubleClick += new EventHandler(this._flex_DoubleClick);
        }

        protected override void InitPaint()
        {
            this.dtp청구일자.Mask = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.FI, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            this.dtp청구일자.StartDate = Global.MainFrame.GetDateTimeToday().AddMonths(-1);
            this.dtp청구일자.EndDate = Global.MainFrame.GetDateTimeToday();

            this.dtp부가세신고일자.Mask = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.FI, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            this.dtp적재일자.Mask = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.FI, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            this.dtp수출신고일자.Mask = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.FI, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            this.dtp수출선적일자.Mask = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.FI, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);

            this.cbo수출구분.DataSource = this.GetComboDataCombine("S;CZ_FI00001");
            this.cbo수출구분.ValueMember = "CODE";
            this.cbo수출구분.DisplayMember = "NAME";
        }
        #endregion

        #region 메인버튼 이벤트
        protected override void OnDataChanged(object sender, EventArgs e)
        {
            try
            {
                base.OnDataChanged(sender, e);

                this.SetToolBarButtonState(true, false, false, this.ToolBarSaveButtonEnabled, false);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                this._flex.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                     this.부가세신고여부,
                                                                     this.전표승인여부,
                                                                     this.cbo수출구분.SelectedValue,
                                                                     this.dtp청구일자.StartDateToString,
                                                                     this.dtp청구일자.EndDateToString,
                                                                     this.dtp부가세신고일자.StartDateToString,
                                                                     this.dtp부가세신고일자.EndDateToString,
                                                                     this.dtp적재일자.StartDateToString,
                                                                     this.dtp적재일자.EndDateToString,
                                                                     this.dtp수출신고일자.StartDateToString,
                                                                     this.dtp수출신고일자.EndDateToString,
                                                                     this.dtp수출선적일자.StartDateToString,
                                                                     this.dtp수출선적일자.EndDateToString });

                if (!this._flex.HasNormalRow)
                {
                    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSaveButtonClicked(sender, e);

                if (!this.MsgAndSave(PageActionMode.Save)) return;

                this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            if (!this.BeforeSave() || !base.SaveData()) return false;

            if (!this._biz.Save(this._flex.GetChanges()))
                return false;

            this._flex.AcceptChanges();
            return true;
        }
        #endregion

        #region 그리드 이벤트
        public void _flex_DoubleClick(object sender, EventArgs e)
        {
            string fileCode, query;

            try
            {
                if (this._flex.HasNormalRow == false) return;
                if (this._flex.MouseRow < this._flex.Rows.Fixed) return;

                if (this._flex.Cols[this._flex.Col].Name == "FILE_PATH_MNG")
                {
                    fileCode = D.GetString(this._flex["NO_GIR"]) + "_" + Global.MainFrame.LoginInfo.CompanyCode;
                    P_CZ_MA_FILE_SUB dlg = new P_CZ_MA_FILE_SUB(Global.MainFrame.LoginInfo.CompanyCode, "SA", "P_CZ_SA_GIM_REG", fileCode, "P_CZ_SA_GIM_REG" + "/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + D.GetString(this._flex["DT_GIR"]).Substring(0, 4));
                    dlg.ShowDialog(this);

                    query = @"SELECT MAX(FILE_NAME) + '(' + CONVERT(VARCHAR, COUNT(1)) + ')' FILE_PATH_MNG
                              FROM MA_FILEINFO WITH(NOLOCK)
                              WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                            @"AND CD_MODULE = 'SA'
                              AND ID_MENU = 'P_CZ_SA_GIM_REG'
                              AND CD_FILE = '" + fileCode + "'";

                    this._flex["FILE_PATH_MNG"] = D.GetString(Global.MainFrame.ExecuteScalar(query));
                }
                else if (this._flex.Cols[this._flex.Col].Name == "FILE_PATH_MNG1")
				{
                    fileCode = D.GetString(this._flex["NO_GIR"]) + "_" + Global.MainFrame.LoginInfo.CompanyCode + "_ETC";
                    P_CZ_MA_FILE_SUB dlg = new P_CZ_MA_FILE_SUB(Global.MainFrame.LoginInfo.CompanyCode, "SA", "P_CZ_SA_GIM_REG", fileCode, "P_CZ_SA_GIM_REG" + "/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + D.GetString(this._flex["DT_GIR"]).Substring(0, 4));
                    dlg.ShowDialog(this);

                    query = @"SELECT MAX(FILE_NAME) + '(' + CONVERT(VARCHAR, COUNT(1)) + ')' FILE_PATH_MNG
                              FROM MA_FILEINFO WITH(NOLOCK)
                              WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                            @"AND CD_MODULE = 'SA'
                              AND ID_MENU = 'P_CZ_SA_GIM_REG'
                              AND CD_FILE = '" + fileCode + "'";

                    this._flex["FILE_PATH_MNG1"] = D.GetString(Global.MainFrame.ExecuteScalar(query));
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region 버튼 이벤트
        private void btn전산매체작성_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;
            ExportDiskManager exportDiskManager;

            try
            {
                if (this._flex.HasNormalRow == false) return;

                dataRowArray = this._flex.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    if (this._flex.DataTable.Select("S = 'Y' AND ISNULL(TP_EXPORT, '') = ''").Length > 0)
                    {
                        this.ShowMessage("CZ_수출구분이 지정되지 않은 건이 포함되어 있습니다.");
                        return;
                    }

                    exportDiskManager = new ExportDiskManager();

                    DataTable dataTable = this.DivInfo();
                    string arg_RegNb = dataTable.Rows[0]["REG_NB"].ToString();
                    exportDiskManager.FileName = string.Format("A{0}.{1}", arg_RegNb.Substring(0, 7), arg_RegNb.Substring(arg_RegNb.Length - 3, 3));

                    exportDiskManager.SetHeader(this.dtp청구일자.StartDateToString.Substring(0, 6) + "01", CommonUtility.GetDateStringWithLastDay(this.dtp청구일자.EndDateToString.Substring(0, 6)), this.RPT_TM, dataTable.Rows[0]);

                    for (int index = 0; index < dataRowArray.Length; ++index)
                    {
                        exportDiskManager.SetBody(this.dtp청구일자.StartDateToString.Substring(0, 6), this.dtp청구일자.EndDateToString.Substring(0, 6), this.RPT_TM, arg_RegNb, dataTable.Rows[0]["DIV_CD"].ToString(), dataTable.Rows[0]["DIV_NM"].ToString(), dataRowArray[index], index + 1);
                    }

                    exportDiskManager.SetTail(this.dtp청구일자.StartDateToString.Substring(0, 6), this.dtp청구일자.EndDateToString.Substring(0, 6), arg_RegNb);

                    string header = exportDiskManager.Header;
                    string tail2 = exportDiskManager.Tail;

                    if (!exportDiskManager.SetEnd())
                    {
                        string msg = "마감오류내역" + Environment.NewLine + Environment.NewLine;

                        foreach (string error in exportDiskManager.m_ErrorMsg)
                        {
                            msg += error + Environment.NewLine;
                        }

                        msg += Environment.NewLine + "진행 하시겠습니까 ?";

                        if (this.ShowMessage(msg, "QY2") != DialogResult.Yes)
                            return;
                    }

                    FolderBrowserDialog fileDialog = new FolderBrowserDialog();
                    if (fileDialog.ShowDialog() != DialogResult.OK)
                        return;

                    if (exportDiskManager.CreateFileOrderHeaderTailBody2(fileDialog.SelectedPath))
                    {
                        this.ShowMessage("CZ_전산매체 제작이 성공적으로 끝났습니다.");

                        foreach (DataRow dr in dataRowArray)
                        {
                            dr["DT_VAT"] = Global.MainFrame.GetStringToday;
                        }
                    }
                    else
                        this.ShowMessage("CZ_전산매체 제작 중 오류가 발생하였습니다.");
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private DataTable DivInfo()
        {
            string query = @"SELECT	DIV_CD,
                                    DIV_NM,
                                    REG_NB,
                                    CEO_NM,
                                    DIV_ADDR + DIV_ADDR1 AS ADDR,
                                    BUSINESS,
                                    JONGMOK
                             FROM SDIV WITH(NOLOCK)
                             WHERE CO_CD = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                            "AND DIV_CD = '" + Global.MainFrame.LoginInfo.BizAreaCode + "'";

            return Global.MainFrame.FillDataTable(query);
        }

        private void btn엑셀양식다운로드_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = null;

            try
            {
                bool bState = true;
                FolderBrowserDialog dlg = new FolderBrowserDialog();
                if (dlg.ShowDialog() != DialogResult.OK) return;

                string localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_부가세관리_" + Global.MainFrame.GetStringToday + ".xls";
                string serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_FI_TAX_MNG.xls";

                System.Net.WebClient client = new System.Net.WebClient();
                client.DownloadFile(serverPath, localPath);

                if (_flex.HasNormalRow)
                {
                    if (ShowMessage("기본데이터가 있습니다. UPDATE하시겠습니까?", "QY2") != DialogResult.Yes)
                        bState = false;
                }

                ShowMessage("4번째 줄부터 저장됩니다. 4번째 줄부터 입력하세요.\r\n'Microsoft Office 2007'인 경우 반드시 통합문서(97~2003)로 저장하세요.");

                if (bState == false) return;

                // 확장명 XLS (Excel 97~2003 용)
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + localPath + @";Extended Properties=Excel 8.0;";

                conn = new OleDbConnection(strConn);
                conn.Open();

                OleDbCommand Cmd = null;
                OleDbDataAdapter OleDBAdap = null;

                string sTableName = string.Empty;

                DataTable dtSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                DataSet ds = new DataSet();

                // 엑셀의 1번째 시트만 데이터 만들어 주면 되므로 한 루프후 끝내면 됩니다.
                foreach (DataRow DR in dtSchema.Rows)
                {
                    OleDBAdap = new OleDbDataAdapter(DR["TABLE_NAME"].ToString(), conn);

                    OleDBAdap.SelectCommand.CommandType = System.Data.CommandType.TableDirect;
                    OleDBAdap.AcceptChangesDuringFill = false;

                    sTableName = DR["TABLE_NAME"].ToString().Replace("$", String.Empty).Replace("'", String.Empty);

                    if (DR["TABLE_NAME"].ToString().Contains("$"))
                        OleDBAdap.Fill(ds, sTableName);
                    break;
                }

                StringBuilder FldsInfo = new StringBuilder();
                StringBuilder Flds = new StringBuilder();

                // Create Field(s) String : 현재 테이블의 Field 명 생성
                foreach (DataColumn Column in ds.Tables[0].Columns)
                {
                    if (FldsInfo.Length > 0)
                    {
                        FldsInfo.Append(",");
                        Flds.Append(",");
                    }

                    FldsInfo.Append("[" + Column.ColumnName.Replace("'", "''") + "] NVARCHAR(4000)");
                    Flds.Append(Column.ColumnName.Replace("'", "''"));
                }

                DataTable dt_Copy = this._flex.DataTable.DefaultView.ToTable();

                // Insert Data
                foreach (DataRow DR in dt_Copy.Rows)
                {
                    StringBuilder Values = new StringBuilder();

                    foreach (DataColumn Column in ds.Tables[0].Columns)
                    {
                        if (!dt_Copy.Columns.Contains(Column.ColumnName)) continue;

                        if (Values.Length > 0) Values.Append(",");
                        Values.Append("'" + DR[Column.ColumnName].ToString().Replace("'", "''") + "'");
                    }

                    Cmd = new OleDbCommand(
                        "INSERT INTO [" + sTableName + "$]" +
                        "(" + Flds.ToString() + ") " +
                        "VALUES (" + Values.ToString() + ")",
                        conn);
                    Cmd.ExecuteNonQuery();
                }

                bState = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        private void btn엑셀업로드_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDlg;
            DataRow[] drs;
            int index;

            try
            {
                #region btn엑셀업로드
                fileDlg = new OpenFileDialog();
                fileDlg.Filter = "엑셀 파일 (*.xls)|*.xls";

                if (fileDlg.ShowDialog() != DialogResult.OK) return;

                Application.DoEvents();

                this._flex.Redraw = false;

                string FileName = fileDlg.FileName;

                Excel excel = new Excel();
                DataTable dtExcel = null;
                dtExcel = excel.StartLoadExcel(FileName, 0, 3); // 3번째 라인부터 저장

                // 필요한 컬럼 존재 유무 파악
                string[] argsPk = new string[] { "NO_IV", "NO_SO" };
                string[] argsPkNm = new string[] { this.DD("계산서번호"), this.DD("수주번호") };

                for (int i = 0; i < argsPk.Length; i++)
                {
                    if (!dtExcel.Columns.Contains(argsPk[i]))
                    {
                        ShowMessage("필수항목이 존재하지 않습니다. -> @ : @", new string[] { argsPk[i], argsPkNm[i] });
                        return;
                    }
                }

                // 데이터 읽으면서 해당하는 값 셋팅
                index = 0;
                foreach (DataRow dr in dtExcel.Rows)
                {
                    MsgControl.ShowMsg("[자료저장중] 잠시만 기다려 주세요 (@/@)", new string[] { D.GetString(++index), D.GetString(dtExcel.Rows.Count) });

                    drs = this._flex.DataTable.Select("NO_IO = '" + D.GetString(dr["NO_IO"]) + "' AND NO_SO = '" + D.GetString(dr["NO_SO"]) + "'");

                    foreach (DataRow dr1 in drs)
                    {
                        dr1["DT_LOADING"] = D.GetString(dr["DT_LOADING"]);
                        dr1["DT_VAT"] = D.GetString(dr["DT_VAT"]);
                        dr1["TP_EXPORT"] = D.GetString(dr["TP_EXPORT"]);
                        dr1["DC_RMK"] = D.GetString(dr["DC_RMK"]);
                    }
                }
                MsgControl.CloseMsg();

                if (this._flex.HasNormalRow)
                {
                    ShowMessage("엑셀업로드 작업을 완료하였습니다.");
                    ToolBarSaveButtonEnabled = true;
                }
                else
                {
                    ShowMessage("엑셀업로드 작업을 완료하였습니다.");
                }

                this._flex.Redraw = true;
                #endregion
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this._flex.Redraw = true;
                MsgControl.CloseMsg();
            }
        }
        #endregion
    }
}

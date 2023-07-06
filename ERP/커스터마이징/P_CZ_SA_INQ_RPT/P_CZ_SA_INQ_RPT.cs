using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using System.Diagnostics;

namespace cz
{
    public partial class P_CZ_SA_INQ_RPT : PageBase
    {
        #region 생성자 & 초기화
        private P_CZ_SA_INQ_RPT_BIZ _biz;
        private string 임시폴더, IMO번호, 호선번호, 호선명;
        private DataTable InterCompany;
        private bool _isPageLoad = false;

        public P_CZ_SA_INQ_RPT()
        {
            StartUp.Certify(this);
            InitializeComponent();

            this.MainGrids = new FlexGrid[] { this._flexH, this._flexL };
            this._flexH.DetailGrids = new FlexGrid[] { this._flexL }; 
        }

        public P_CZ_SA_INQ_RPT(string IMO번호, string 호선번호, string 호선명)
        {
            StartUp.Certify(this);
            InitializeComponent();

            this.IMO번호 = IMO번호;
            this.호선번호 = 호선번호;
            this.호선명 = 호선명;

            this.MainGrids = new FlexGrid[] { this._flexH, this._flexL };
            this._flexH.DetailGrids = new FlexGrid[] { this._flexL };
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this._biz = new P_CZ_SA_INQ_RPT_BIZ();
            this.임시폴더 = "temp";

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            #region Header
            this._flexH.BeginSetting(1, 1, false);

            this._flexH.SetCol("NM_COMPANY", "회사명", 100);
            this._flexH.SetCol("NO_KEY", "파일번호", 70);
            this._flexH.SetCol("NO_REF", "문의번호", 70);
            this._flexH.SetCol("NO_PO_PARTNER", "주문번호", 70);
            this._flexH.SetCol("DT_FILE", "등록일자",70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("NM_SALES", "영업담당자", 80);
            this._flexH.SetCol("NM_TYPIST", "입력담당자", 80);
            this._flexH.SetCol("NM_PUR", "구매담당자", 80);
            this._flexH.SetCol("NM_LOG", "물류담당자", 80);
            this._flexH.SetCol("NM_VESSEL", "호선", 120);
            this._flexH.SetCol("NM_PARTNER", "매출처", 150);
            
            this._flexH.SetDummyColumn("S");
            this._flexH.ExtendLastCol = true;

            this._flexH.SettingVersion = "1.0";
            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region Line
            this._flexL.BeginSetting(1, 1, false);

            this._flexL.SetCol("NM_STEP", "유형", 80);
            this._flexL.SetCol("NO_LINE", "항번", 40);
            this._flexL.SetCol("NM_FILE", "파일명", 200);
            this._flexL.SetCol("NM_SUPPLIER", "매입처", 120);
            this._flexL.SetCol("NM_INSERT", "등록자", 80);
            this._flexL.SetCol("DTS_INSERT", "등록일자", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexL.SetCol("DC_RMK", "비고", 150, true);

            this._flexL.Cols["DTS_INSERT"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexL.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";

            this._flexL.SetDummyColumn("S");
            this._flexL.ExtendLastCol = true;

            this._flexL.SettingVersion = "1.0";
            this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.None);
            #endregion
        }

        private void InitEvent()
        {
            this.ctx영업담당자.QueryBefore += new BpQueryHandler(this.ctx영업담당자_QueryBefore);
            this.ctx호선.QueryAfter += new BpQueryHandler(this.ctx호선_QueryAfter);
            this.ctx호선.CodeChanged += new EventHandler(this.ctx호선_CodeChanged);
            this.btn영업정보등록.Click += new EventHandler(this.btn영업정보등록_Click);
            this.btn첨부파일등록.Click += new EventHandler(this.btn첨부파일등록_Click);

            this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
            this._flexH.MouseDoubleClick += new MouseEventHandler(this._flexH_MouseDoubleClick);
            
            this._flexL.MouseDoubleClick += new MouseEventHandler(this._flexL_MouseDoubleClick);
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.splitContainer1.SplitterDistance = 756;

            this.cbo검색유형.DataSource = MA.GetCodeUser(new string[] { "000", "001", "002", "003" }, new string[] { this.DD("파일번호"), this.DD("문의번호"), this.DD("주문번호"), this.DD("계산서번호") });
            this.cbo검색유형.ValueMember = "CODE";
            this.cbo검색유형.DisplayMember = "NAME";

            this.cbo검색방법.DataSource = MA.GetCodeUser(new string[] { "000", "001", "002" }, new string[] { this.DD("일치"), this.DD("앞글자"), this.DD("포함") });
            this.cbo검색방법.ValueMember = "CODE";
            this.cbo검색방법.DisplayMember = "NAME";

            this.cbo파일유형.DataSource = Global.MainFrame.GetComboDataCombine("S;CZ_MA00005");
            this.cbo파일유형.ValueMember = "CODE";
            this.cbo파일유형.DisplayMember = "NAME";

            this.dtp등록일자.StartDate = Global.MainFrame.GetDateTimeToday().AddMonths(-1);
            this.dtp등록일자.EndDate = Global.MainFrame.GetDateTimeToday();

            this.ctx회사.CodeValue = Global.MainFrame.LoginInfo.CompanyCode;
            this.ctx회사.CodeName = Global.MainFrame.LoginInfo.CompanyName;

            InterCompany = DBHelper.GetDataTable("SELECT * FROM CZ_SA_INTERCOMPANY");

            this.SetToolBarButtonState(true, true, false, this.ToolBarSaveButtonEnabled, false);

            if (!string.IsNullOrEmpty(this.IMO번호))
            {
                this.ctx호선.CodeValue = this.IMO번호;
                this.ctx호선.CodeName = this.호선번호;
                this.txt호선.Text = this.호선명;

                this.OnToolBarSearchButtonClicked(null, null);
            }
        }
        #endregion

        #region 메인버튼 이벤트
        protected override void OnDataChanged(object sender, EventArgs e)
        {
            base.OnDataChanged(sender, e);
            this.SetToolBarButtonState(true, true, false, this.ToolBarSaveButtonEnabled, false);
        }

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarAddButtonClicked(sender, e);

                this.btn첨부파일등록_Click(null, null);
            }
            catch (Exception Ex)
            {
                MsgEnd(Ex);
            }
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            object[] obj;

            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!BeforeSearch()) return;

                obj = new object[] { this.ctx회사.CodeValue,
                                     this.txt파일번호.Text.Trim(),
                                     this.dtp등록일자.StartDateToString,
                                     this.dtp등록일자.EndDateToString,
                                     this.ctx영업담당자.CodeValue,
                                     this.ctx입력담당자.CodeValue,
                                     this.ctx매출처.CodeValue,
                                     this.ctx매입처.CodeValue,
                                     this.ctx호선.CodeValue,
                                     this.cbo파일유형.SelectedValue.ToString() };

                this._flexH.Binding = this._biz.Search(obj, this.cbo검색유형.SelectedValue.ToString() + "_" + this.cbo검색방법.SelectedValue.ToString());

                if (this._flexH.HasNormalRow == false)
                    this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
            }
            catch (Exception Ex)
            {
                MsgEnd(Ex);
            }
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSaveButtonClicked(sender, e);

                if (!this.BeforeSave()) return;

                if (MsgAndSave(PageActionMode.Save))
                    ShowMessage(PageResultMode.SaveGood);
            }
            catch (Exception Ex)
            {
                MsgEnd(Ex);
            }
        }

        protected override bool SaveData()
        {
            if (!this.BeforeSave() || !base.SaveData()) return false;

            DataTable dt = ComFunc.getGridGroupBy(this._flexL.GetChanges(), new string[] { "CD_COMPANY", "TP_STEP", "NO_KEY", "DC_RMK" }, true);
            dt.AcceptChanges();

            foreach (DataRow dr in dt.Rows)
            {
                dr.SetModified();
            }

            if (!this._biz.SaveData(dt))
                return false;

            this._flexL.AcceptChanges();
            return true;
        }

        public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
        {
            try
            {
                this.임시파일제거();
                return base.OnToolBarExitButtonClicked(sender, e);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return false;
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
        #endregion

        #region 버튼 이벤트
        private void btn영업정보등록_Click(object sender, EventArgs e)
        {
            try
            {
                P_CZ_SA_USER_SUB subDlg = new P_CZ_SA_USER_SUB();
                subDlg.ShowDialog(this);
            }
            catch (Exception Ex)
            {
                MsgEnd(Ex);
            }
        }

        private void btn첨부파일등록_Click(object sender, EventArgs e)
        {
            DataTable dt;
            string query, key;

            try
            {
                query = @"SELECT * 
                          FROM CZ_SA_USER WITH(NOLOCK)" + Environment.NewLine +
                         "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                         "AND ID_USER = '" + Global.MainFrame.LoginInfo.UserID + "'";

                dt = Global.MainFrame.FillDataTable(query);

                if (dt.Rows.Count == 0)
                {
                    this.ShowMessage("CZ_@ 이(가) 없습니다.", this.DD("영업정보"));
                    return;
                }

                if (Global.MainFrame.LoginInfo.CompanyCode == D.GetString(this._flexH["CD_COMPANY"]))
                    key = D.GetString(this._flexH["NO_KEY"]);
                else
                    key = string.Empty;

                P_CZ_SA_INQ_REG subDlg = new P_CZ_SA_INQ_REG(key);
                subDlg.Show(this);
            }
            catch (Exception Ex)
            {
                MsgEnd(Ex);
            }
        }

        private void ctx영업담당자_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                e.HelpParam.P41_CD_FIELD1 = Global.MainFrame.LoginInfo.CompanyCode;
                e.HelpParam.P42_CD_FIELD2 = Global.MainFrame.LoginInfo.CompanyName;
                e.HelpParam.P61_CODE1 = "SA";
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void ctx호선_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                this.txt호선.Text = e.HelpReturn.Rows[0]["NM_VESSEL"].ToString();
            }
            catch (Exception Ex)
            {
                MsgEnd(Ex);
            }
        }

        private void ctx호선_CodeChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ctx호선.Text))
                {
                    this.txt호선.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region 그리드 이벤트
        private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            string filter;
            DataTable dt = null;

            try
            {
                filter = "CD_COMPANY = '" + D.GetString(this._flexH["CD_COMPANY"]) + "' AND NO_KEY = '" + D.GetString(this._flexH["NO_KEY"]) + "'";

                if (this._flexH.DetailQueryNeed == true)
                {
                    dt = _biz.SearchDetail(new object[] { D.GetString(this._flexH["CD_COMPANY"]),
                                                          this.LoginInfo.Language,
                                                          D.GetString(this._flexH["NO_KEY"]),
                                                          this.ctx매입처.CodeValue,
                                                          this.cbo파일유형.SelectedValue.ToString() });
                }

                this._flexL.BindingAdd(dt, filter);
                this.셀병합();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flexH_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string pageId, pageName, query;

            try
            {
                if (this._flexH.HasNormalRow == false) return;
                if (this._flexH.MouseRow < this._flexH.Rows.Fixed) return;
                if (this._flexH.ColSel != this._flexH.Cols["NO_KEY"].Index) return;
                if (string.IsNullOrEmpty(D.GetString(this._flexH["NO_KEY"]))) return;
                if (D.GetString(this._flexH["CD_COMPANY"]) != Global.MainFrame.LoginInfo.CompanyCode) return;
                if (this._isPageLoad == true) return;

                this._isPageLoad = true;

                query = @"SELECT 1 
                          FROM MA_CODEDTL WITH(NOLOCK)
                          WHERE CD_COMPANY = '" + D.GetString(this._flexH["CD_COMPANY"]) + "'" +
                        @"AND CD_FIELD = 'CZ_SA00023'
                          AND CD_FLAG2 = 'GS'
                          AND CD_SYSDEF = '" + D.GetString(this._flexH["NO_KEY"]).Substring(0, 2) + "'";

                if (D.GetDecimal(DBHelper.ExecuteScalar(query)) > 0)
                {
                    pageId = "P_CZ_SA_QTN_REG_GS";
                    pageName = Global.MainFrame.DD("견적등록(선용)");
                }
                else
                {
                    pageId = "P_CZ_SA_QTN_REG";
                    pageName = Global.MainFrame.DD("견적등록");
                }

                if (Global.MainFrame.IsExistPage(pageId, false))
                    Global.MainFrame.UnLoadPage(pageId, false);

                Global.MainFrame.LoadPageFrom(pageId, pageName, this.Grant, new object[] { D.GetString(this._flexH["NO_KEY"]) });

                this._isPageLoad = false;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flexL_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (this._flexL.HasNormalRow == false) return;
                if (this._flexL.MouseRow < this._flexL.Rows.Fixed) return;
                if (this._flexL.ColSel != this._flexL.Cols["NM_FILE"].Index) return;
                if (this.첨부파일권한확인(D.GetString(this._flexH["CD_COMPANY"]), Global.MainFrame.LoginInfo.CompanyCode, D.GetString(this._flexH["NO_KEY"])) == false) return;

                if (!string.IsNullOrEmpty(this._flexL["NM_FILE_REAL"].ToString()))
                    FileMgr.Download_WF(D.GetString(this._flexH["CD_COMPANY"]), D.GetString(this._flexL["NO_KEY"]), this._flexL["NM_FILE_REAL"].ToString(), true);
                else if (D.GetString(this._flexL["YN_DB_FILE"]) == "Y")
                {
                    DataTable dt = DBHelper.GetDataTable(@"SELECT FILE_DATA 
                                                           FROM CZ_SRM_QTNH_ATTACHMENT WITH(NOLOCK)
                                                           WHERE CD_COMPANY = '" + D.GetString(this._flexH["CD_COMPANY"]) + "'" + Environment.NewLine +
                                                          "AND NO_FILE = '" + D.GetString(this._flexL["NO_KEY"]) + "'" + Environment.NewLine +
                                                          "AND CD_PARTNER = '" + D.GetString(this._flexL["CD_SUPPLIER"]) + "'" + Environment.NewLine +
                                                          "AND SEQ = '" + D.GetString(this._flexL["NO_LINE"]) + "'");

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string localPath = Application.StartupPath + "\\temp\\";
                        string filename = FileMgr.GetUniqueFileName(localPath + this._flexL["NM_FILE"].ToString());
                        byte[] documentBinary = (byte[])dt.Rows[0]["FILE_DATA"];

                        FileMgr.CreateDirectory(localPath);

                        FileStream fStream = new FileStream(localPath + filename, FileMode.Create);
                        fStream.Write(documentBinary, 0, documentBinary.Length);
                        fStream.Close();
                        fStream.Dispose();

                        Process.Start(localPath + filename);
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region 기타 메소드
        private void 셀병합()
        {
            CellRange cellRange;
            string key;

            try
            {
                if (this._flexL.HasNormalRow == false) return;

                this._flexL.Redraw = false;

                for (int row = this._flexL.Rows.Fixed; row < this._flexL.Rows.Count; row++)
                {
                    key = D.GetString(this._flexL.GetData(row, "NM_STEP"));

                    cellRange = this._flexL.GetCellRange(row, "NM_STEP", row, "NM_STEP");
                    cellRange.UserData = key + "_" + "NM_STEP";

                    cellRange = this._flexL.GetCellRange(row, "DC_RMK", row, "DC_RMK");
                    cellRange.UserData = key + "_" + "DC_RMK";
                }

                this._flexL.DoMerge();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                this._flexL.Redraw = true;
            }
        }

        private bool 첨부파일권한확인(string 첨부파일회사, string 로그인회사, string 파일번호)
        {
            try
            {
                if (this.InterCompany == null || this.InterCompany.Rows.Count == 0)
                {
                    if (첨부파일회사 == 로그인회사) return true;
                    else return false;
                }
                else
                {
                    if (InterCompany.Select("CD_COMPANY = '" + 첨부파일회사 + "' AND CD_PREFIX = '" + 파일번호.Substring(0, 2) + "' AND CD_COMPANY_SO = '" + 로그인회사 + "'").Length > 0) 
                        return true;
                    else if (첨부파일회사 == 로그인회사) return true;
                    else return false;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return false;
        }
        #endregion
    }
}

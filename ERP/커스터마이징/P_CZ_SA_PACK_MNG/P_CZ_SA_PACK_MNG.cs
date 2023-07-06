using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.Windows.Print;
using DX;

namespace cz
{
    public partial class P_CZ_SA_PACK_MNG : PageBase
    {
        #region 생성자 & 전역변수
        P_CZ_SA_PACK_MNG_BIZ _biz;
        private string 의뢰번호;
        private string 임시폴더;

        public P_CZ_SA_PACK_MNG()
        {
            StartUp.Certify(this);
            InitializeComponent();
        }

        public P_CZ_SA_PACK_MNG(string 의뢰번호)
        {
            StartUp.Certify(this);
            InitializeComponent();

            this.의뢰번호 = 의뢰번호;
        }
        #endregion

        #region 초기화
        protected override void InitLoad()
        {
            base.InitLoad();

            this.임시폴더 = "temp";
            this._biz = new P_CZ_SA_PACK_MNG_BIZ();

            this.InitGrid();
            this.InitEvent();
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.dtp포장일자.Mask = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.FI, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            this.dtp포장일자.StartDate = Global.MainFrame.GetDateTimeToday().AddMonths(-1);
            this.dtp포장일자.EndDate = Global.MainFrame.GetDateTimeToday();

            if (!string.IsNullOrEmpty(this.의뢰번호))
            {
                this.txt의뢰번호.Text = this.의뢰번호;
                this.OnToolBarSearchButtonClicked(null, null);
            }
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flexH, this._flexL };
            this._flexH.DetailGrids = new FlexGrid[] { this._flexL };

            #region Header
            this._flexH.BeginSetting(1, 1, false);

            this._flexH.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flexH.SetCol("YN_OUTSOURCING", "외주여부", 40, false, CheckTypeEnum.Y_N);
            this._flexH.SetCol("NM_COMPANY", "회사", 100);
            this._flexH.SetCol("NO_GIR", "의뢰번호", 100);
            this._flexH.SetCol("DT_GIR", "의뢰일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("NM_PARTNER", "매출처", 100);
            this._flexH.SetCol("NM_VESSEL", "호선", 100);
            this._flexH.SetCol("NM_PACK", "포장명", 100, true, typeof(decimal));
            this._flexH.SetCol("DT_PACK", "포장일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("NM_TYPE", "포장유형", 100);
            this._flexH.SetCol("NM_SIZE", "포장사이즈", 150);
            this._flexH.SetCol("QT_ITEM", "종수", 100, typeof(decimal), FormatTpType.QUANTITY);
            this._flexH.SetCol("QT_NET_WEIGHT", "순중량", 60);
            this._flexH.SetCol("QT_GROSS_WEIGHT", "총중량", 60);
            this._flexH.SetCol("QT_WIDTH", "가로", 60);
            this._flexH.SetCol("QT_LENGTH", "세로", 60);
            this._flexH.SetCol("QT_HEIGHT", "높이", 60);
            this._flexH.SetCol("CD_LOCATION", "로케이션", 80);
            this._flexH.SetCol("FILE_NAME", "포장사진", 200);
            this._flexH.SetCol("NM_INSERT", "등록자", 60);
            this._flexH.SetCol("DTS_INSERT", "등록일자", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("NM_UPDATE", "수정자", 60);
            this._flexH.SetCol("DTS_UPDATE", "수정일자", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("NO_FILE", "수주번호", 100);
            this._flexH.SetCol("NM_CC", "코스트센터", 100);
            this._flexH.SetCol("DC_RMK", "비고", 100, true);

            this._flexH.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";
            this._flexH.Cols["DTS_UPDATE"].Format = "####/##/##/##:##:##";

            this._flexH.ExtendLastCol = true;
            this._flexH.SetDummyColumn(new string[] { "S", "YN_OUTSOURCING" });

            this._flexH.SettingVersion = "0.0.0.1";
            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flexH.SetExceptSumCol("NM_PACK");
            #endregion

            #region Line
            this._flexL.BeginSetting(1, 1, false);

            this._flexL.SetCol("SEQ_GIR", "의뢰항번", 80);
            this._flexL.SetCol("NO_FILE", "파일번호", 80);
            this._flexL.SetCol("NO_PO_PARTNER", "매출처발주번호", 100);
            this._flexL.SetCol("NO_DSP", "파일항번", 80);
            this._flexL.SetCol("CD_ITEM", "품목코드", 100);
            this._flexL.SetCol("NM_ITEM", "품목명", 100);
            this._flexL.SetCol("CD_ITEM_PARTNER", "매출처품목코드", 100);
            this._flexL.SetCol("NM_ITEM_PARTNER", "매출처품목명", 100);
            this._flexL.SetCol("QT_PACK", "포장수량", 60, false, typeof(decimal));

            this._flexL.SettingVersion = "0.0.0.1";
            this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion
        }

        private void InitEvent()
        {
            this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
            this._flexH.DoubleClick += new EventHandler(this._flexH_DoubleClick);
            this._flexH.CheckHeaderClick += new EventHandler(this._flexH_CheckHeaderClick);

            this.btn포장명세서출력.Click += new EventHandler(this.btn포장명세서출력_Click);
            this.btn상업송장출력.Click += new EventHandler(this.btn상업송장출력_Click);
            this.btn인수증출력.Click +=new EventHandler(this.btn인수증출력_Click);
        }
        #endregion

        #region 메인버튼 이벤트
        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch()) return false;

            if (string.IsNullOrEmpty(this.bpc회사.QueryWhereIn_Pipe))
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl회사.Text);
                return false;
            }

            return true;
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!this.BeforeSearch()) return;

                this._flexH.Binding = this._biz.Search(new object[] { Global.MainFrame.ServerKey,
                                                                      this.bpc회사.QueryWhereIn_Pipe,
                                                                      Global.MainFrame.LoginInfo.Language,
                                                                      this.dtp포장일자.StartDateToString,
                                                                      this.dtp포장일자.EndDateToString,
                                                                      this.txt의뢰번호.Text,
                                                                      this.txt수주번호.Text,
                                                                      this.ctx담당자.CodeValue });

                if (!this._flexH.HasNormalRow)
                    ShowMessage(공통메세지.조건에해당하는내용이없습니다);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarAddButtonClicked(sender, e);

                this.CallOtherPageMethod("P_CZ_SA_PACK_REG", "포장등록", "P_CZ_SA_PACK_REG", this.Grant, new object[] { });
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override bool BeforeDelete()
        {
            if (this._flexH["YN_OUTSOURCING"].ToString() == "Y")
            {
                this.ShowMessage("외주포장 건은 삭제 할 수 없습니다.");
                return false;
            }

            return base.BeforeDelete();
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarDeleteButtonClicked(sender, e);

                if (!this.BeforeDelete()) return;

                this._flexH.Rows.Remove(this._flexH.Row);
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

                if (MsgAndSave(PageActionMode.Save))
                    ShowMessage(PageResultMode.SaveGood);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            try
            {
                if (!base.SaveData() || !base.Verify() || !this.BeforeSave()) return false;
                if (this._flexH.IsDataChanged == false) return false;

                DataTable dtH = this._flexH.GetChanges();
                
                if (!this._biz.Save(dtH)) return false;

                this._flexH.AcceptChanges();
                
                return true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }

            return false;
        }

        public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
        {
            this.임시파일제거();
            return base.OnToolBarExitButtonClicked(sender, e);
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

        #region 그리드 이벤트
        private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dtL;
            string key, key1, key2, filter;

            try
            {
                if (this._flexH.HasNormalRow == false) return;

                key = D.GetString(this._flexH["CD_COMPANY"]);
                key1 = D.GetString(this._flexH["NO_GIR"]);
                key2 = D.GetString(this._flexH["NO_PACK"]);
                
                filter = "CD_COMPANY = '" + key + "' AND NO_GIR = '" + key1 + "' AND NO_PACK = '" + key2 + "'";
                dtL = null;

                if (this._flexH.DetailQueryNeed == true)
                {
                    dtL = this._biz.SearchDetail(new object[] { key,
                                                                key1,
                                                                key2 });
                }

                this._flexL.BindingAdd(dtL, filter);
                this._flexH.DetailQueryNeed = false;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flexH_CheckHeaderClick(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow) return;

                this._flexH.Redraw = false;

                //데이타 테이블의 체크값을 변경해주어도 화면에 보이는 값을 변경시키지 못하므로 화면의 값도 같이 변경시켜줌
                this._flexH.Row = 1; // 맨처음row에 자동위치하도록 해야 틀어지지 않는다.

                for (int h = 0; h < this._flexH.Rows.Count - 1; h++)
                {
                    MsgControl.ShowMsg("자료를 조회 중 입니다.잠시만 기다려 주세요.(@/@)", new string[] { D.GetString(h + 1), D.GetString(this._flexH.Rows.Count - 1) });

                    this._flexH.Row = h + 1; //Row가 순차적으로 변경되면서 RowChange() 이벤트를 자동 실행하게 유도한다.
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
                this._flexH.Redraw = true;
            }
        }

        private void _flexH_DoubleClick(object sender, EventArgs e)
        {
            string pageId, pageName;

            try
            {
                if (this._flexH.HasNormalRow == false) return;
                if (this._flexH.MouseRow < this._flexH.Rows.Fixed) return;

                if (this._flexH.Cols[this._flexH.Col].Name == "NO_GIR")
                {
                    pageId = "P_CZ_SA_PACK_REG";
                    pageName = "포장등록";

                    if (this.IsExistPage(pageId, false))
                        this.UnLoadPage(pageId, false);

                    this.LoadPageFrom(pageId, pageName, this.Grant, new object[] { D.GetString(this._flexH["CD_COMPANY"]),
                                                                                   D.GetString(this._flexH["NM_COMPANY"]),
                                                                                   D.GetString(this._flexH["NO_GIR"]),
                                                                                   D.GetString(this._flexH["DT_GIR"]),
                                                                                   D.GetString(this._flexH["CD_PARTNER"]),
                                                                                   D.GetString(this._flexH["NM_PARTNER"]),
                                                                                   D.GetString(this._flexH["NO_IMO"]),
                                                                                   D.GetString(this._flexH["NO_HULL"]),
                                                                                   D.GetString(this._flexH["NM_VESSEL"]) });
                }
                else if (this._flexH.Cols[this._flexH.Col].Name == "FILE_NAME" && !string.IsNullOrEmpty(this._flexH["FILE_NAME"].ToString()))
                {
                    DataTable dt = DBHelper.GetDataTable(@"SELECT A.FILE_DATA
		                                                   FROM CZ_SA_PACKH_ATTACHMENT A WITH(NOLOCK)
		                                                   JOIN (SELECT CD_COMPANY, NO_GIR, NO_PACK,
		                                                   				MAX(SEQ) AS SEQ 
		                                                   		 FROM CZ_SA_PACKH_ATTACHMENT WITH(NOLOCK)
		                                                   		 GROUP BY CD_COMPANY, NO_GIR, NO_PACK) B
		                                                   ON B.CD_COMPANY = A.CD_COMPANY AND B.NO_GIR = A.NO_GIR AND B.NO_PACK = A.NO_PACK AND B.SEQ = A.SEQ
                                                           WHERE A.CD_COMPANY = '" + D.GetString(this._flexH["CD_COMPANY"]) + "'" + Environment.NewLine +
                                                          "AND A.NO_GIR = '" + D.GetString(this._flexH["NO_GIR"]) + "'" + Environment.NewLine +
                                                          "AND A.NO_PACK = '" + D.GetString(this._flexH["NO_PACK"]) + "'");

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string localPath = Application.StartupPath + "\\temp\\";
                        string filename = FileMgr.GetUniqueFileName(localPath + this._flexH["FILE_NAME"].ToString());
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
                MsgEnd(ex);
            }
        }
        #endregion

        #region 버튼 이벤트
        private void btn포장명세서출력_Click(object sender, EventArgs e)
        {
            ReportHelper reportHelper;
            DataTable 포장명세서헤더, 포장명세서라인;
            DataRow[] dataRowArray1;

            try
            {
                if (!this._flexH.HasNormalRow) return;

                dataRowArray1 = this._flexH.DataTable.Select("S = 'Y'");
                if (dataRowArray1 == null || dataRowArray1.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    foreach (DataRow dr in ComFunc.getGridGroupBy(dataRowArray1, new string[] { "CD_COMPANY" }, true).Rows)
                    {
                        포장명세서헤더 = new DataTable();
                        포장명세서라인 = new DataTable();

                        this.포장명세서데이터(this._flexH.DataTable.Select("S = 'Y' AND CD_COMPANY = '" + dr["CD_COMPANY"].ToString() + "'"), ref 포장명세서헤더, ref 포장명세서라인);

                        reportHelper = Util.SetRPT("R_CZ_SA_GIRSCH_1", "납품의뢰현황-포장명세서", dr["CD_COMPANY"].ToString(), 포장명세서헤더, 포장명세서라인);
                        reportHelper.SetDataTable(포장명세서헤더, 1);

                        Util.RPT_Print(reportHelper);
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn상업송장출력_Click(object sender, EventArgs e)
        {
            ReportHelper reportHelper;
            DataTable 상업송장헤더, 상업송장라인, 인수증라인, 포장명세서헤더, 포장명세서라인;
            DataRow[] dataRowArray1;

            try
            {
                if (!this._flexH.HasNormalRow) return;

                dataRowArray1 = this._flexH.DataTable.Select("S = 'Y'");
                if (dataRowArray1 == null || dataRowArray1.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    foreach (DataRow dr in ComFunc.getGridGroupBy(dataRowArray1, new string[] { "CD_COMPANY" }, true).Rows)
                    {
                        상업송장헤더 = new DataTable();
                        상업송장라인 = new DataTable();
                        인수증라인 = new DataTable();
                        포장명세서헤더 = new DataTable();
                        포장명세서라인 = new DataTable();

                        this.상업송장데이터(dr["CD_COMPANY"].ToString(), this._flexH.DataTable.Select("S = 'Y' AND CD_COMPANY = '" + dr["CD_COMPANY"].ToString() + "'"), ref 상업송장헤더, ref 상업송장라인, ref 인수증라인);
                        this.포장명세서데이터(this._flexH.DataTable.Select("S = 'Y' AND CD_COMPANY = '" + dr["CD_COMPANY"].ToString() + "'"), ref 포장명세서헤더, ref 포장명세서라인);

                        Util.SetRPT_DataTable(포장명세서라인, this.열넓이("R_CZ_SA_GIRSCH_1"));
                        Util.SetRPT_DataTable(인수증라인, this.열넓이("R_CZ_SA_GIRSCH_3"));

                        if (상업송장헤더.Rows.Count != 0)
                        {
                            reportHelper = Util.SetRPT("R_CZ_SA_GIRSCH_2", "납품의뢰현황-상업송장", dr["CD_COMPANY"].ToString(), 상업송장헤더, 상업송장라인);

                            reportHelper.SetDataTable(상업송장헤더, 1);
                            reportHelper.SetDataTable(상업송장라인, 2);
                            reportHelper.SetDataTable(인수증라인, 3);
                            reportHelper.SetDataTable(포장명세서헤더, 4);
                            reportHelper.SetDataTable(포장명세서라인, 5);

                            Util.RPT_Print(reportHelper);
                        }
                        else
                        {
                            this.ShowMessage("@ 가 존재하지 않음", this.DD("송장정보"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn인수증출력_Click(object sender, EventArgs e)
        {
            ReportHelper reportHelper;
            DataTable 협조전헤더, 협조전헤더1, 협조전라인, 협조전라인1;
            DataRow[] dataRowArray1;
            string filePath, fileName;

            try
            {
                if (!this._flexH.HasNormalRow) return;

                dataRowArray1 = this._flexH.DataTable.Select("S = 'Y'");
                if (dataRowArray1 == null || dataRowArray1.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    foreach (DataRow dr in ComFunc.getGridGroupBy(dataRowArray1, new string[] { "CD_COMPANY" }, true).Rows)
                    {
                        협조전헤더 = new DataTable();
                        협조전라인 = new DataTable();
                        
                        this.협조전데이터(dr["CD_COMPANY"].ToString(), this._flexH.DataTable.Select("S = 'Y' AND CD_COMPANY = '" + dr["CD_COMPANY"].ToString() + "'"), ref 협조전헤더, ref 협조전라인);

                        협조전라인.Columns.Add("CD_DM_RCT");
                        협조전라인.Columns.Add("CD_BAR_RCT");

                        협조전헤더1 = 협조전헤더.Clone();
                        협조전라인1 = 협조전라인.Clone();

                        foreach (DataRow dr1 in 협조전헤더.Rows)
                        {
                            if (dr1["CD_TYPE"].ToString() != "001" || dr1["YN_RETURN"].ToString() == "Y")
                                continue;

                            협조전헤더1.Clear();
                            협조전라인1.Clear();

                            협조전헤더1.ImportRow(dr1);

                            foreach (DataRow dr2 in 협조전라인.Select(string.Format("NO_GIR = '{0}'", dr1["NO_GIR"].ToString())))
                            {
                                협조전라인1.ImportRow(dr2);
                            }

                            #region 임시폴더생성
                            filePath = Path.Combine(Application.StartupPath, "temp") + "\\" + D.GetString(dr1["NO_GIR"]) + "\\";

                            if (Directory.Exists(filePath))
                            {
                                string[] files = Directory.GetFiles(filePath);

                                foreach (string file in files)
                                    File.Delete(file);
                            }
                            else
                                Directory.CreateDirectory(filePath);
                            #endregion

                            #region 인수증장수
                            reportHelper = Util.SetRPT("R_CZ_SA_GIRSCH_3", "납품의뢰현황-인수증", dr1["CD_COMPANY"].ToString(), 협조전헤더1, 협조전라인1);

                            foreach (DataRow dr2 in 협조전라인1.Rows)
                            {
                                #region QR, DataMetrix, Barcode 생성
                                dr2["NO_KEY1"] = (dr2["NO_GIR"].ToString() + "-" + dr2["NO_SO"].ToString());

                                fileName = filePath + dr2["NO_KEY1"] + "_RCT_DM.png";

                                if (!string.IsNullOrEmpty(dr2["CD_QR_RCT"].ToString()) && !File.Exists(fileName))
                                {
                                    string[] qr코드Array = dr2["CD_QR_RCT"].ToString().Split('/');
                                    QR.데이터매트릭스(qr코드Array[qr코드Array.Length - 1], fileName);
                                }

                                dr2["CD_DM_RCT"] = fileName;

                                fileName = filePath + dr2["NO_KEY1"] + "_RCT_BAR.png";

                                if (!string.IsNullOrEmpty(dr2["CD_QR_RCT"].ToString()) && !File.Exists(fileName))
                                {
                                    string[] qr코드Array = dr2["CD_QR_RCT"].ToString().Split('/');
                                    QR.바코드(qr코드Array[qr코드Array.Length - 1], fileName);
                                }

                                dr2["CD_BAR_RCT"] = fileName;
                                #endregion
                            }

                            reportHelper.SetDataTable(협조전헤더1, 1);
                            reportHelper.SetDataTable(협조전라인1, 2);
                            reportHelper.PrintHelper.UseUserFontStyle();

                            if (dr1["CD_COMPANY"].ToString() == "K100")
                                reportHelper.PrintDirect("R_CZ_SA_GIRSCH_3_K100_A_RCT.DRF", false, true, filePath + dr1["NO_GIR"].ToString() + "_RCT.pdf", new Dictionary<string, string>());
                            else if (dr1["CD_COMPANY"].ToString() == "K200")
                                reportHelper.PrintDirect("R_CZ_SA_GIRSCH_3_K200_A_RCT.DRF", false, true, filePath + dr1["NO_GIR"].ToString() + "_RCT.pdf", new Dictionary<string, string>());
                            else
                                reportHelper.PrintDirect("R_CZ_SA_GIRSCH_3_S100_A_RCT.DRF", false, true, filePath + dr1["NO_GIR"].ToString() + "_RCT.pdf", new Dictionary<string, string>());

                            int 인수증장수 = PDF.GetPageCount(filePath + dr1["NO_GIR"].ToString() + "_RCT.pdf");

                            DBHelper.ExecuteScalar(string.Format(@"UPDATE SA_GIRH
                                                                   SET CD_USERDEF2 = '{2}'
                                                                   WHERE CD_COMPANY = '{0}'
                                                                   AND NO_GIR = '{1}'", dr1["CD_COMPANY"].ToString(), dr1["NO_GIR"].ToString(), 인수증장수));
                            #endregion
                        }

                        reportHelper = Util.SetRPT("R_CZ_SA_GIRSCH_3", "납품의뢰현황-인수증", dr["CD_COMPANY"].ToString(), 협조전헤더, 협조전라인);

                        foreach (DataRow dr1 in 협조전라인.Rows)
                        {
                            filePath = Path.Combine(Application.StartupPath, "temp") + "\\" + dr1["NO_GIR"].ToString() + "\\";

                            if (!Directory.Exists(filePath))
                                Directory.CreateDirectory(filePath);

                            #region QR, DataMetrix, Barcode 생성
                            dr1["NO_KEY1"] = (dr1["NO_GIR"].ToString() + "-" + dr1["NO_SO"].ToString());

                            fileName = filePath + dr1["NO_KEY1"] + "_RCT_DM.png";

                            if (!string.IsNullOrEmpty(dr1["CD_QR_RCT"].ToString()) && !File.Exists(fileName))
							{
                                string[] qr코드Array = dr1["CD_QR_RCT"].ToString().Split('/');
                                QR.데이터매트릭스(qr코드Array[qr코드Array.Length - 1], fileName);
                            }
                            
                            dr1["CD_DM_RCT"] = fileName;

                            fileName = filePath + dr1["NO_KEY1"] + "_RCT_BAR.png";

                            if (!string.IsNullOrEmpty(dr1["CD_QR_RCT"].ToString()) && !File.Exists(fileName))
							{
                                string[] qr코드Array = dr1["CD_QR_RCT"].ToString().Split('/');
                                QR.바코드(qr코드Array[qr코드Array.Length - 1], fileName);
                            }
                            
                            dr1["CD_BAR_RCT"] = fileName;
							#endregion
						}

						reportHelper.SetDataTable(협조전헤더, 1);
                        reportHelper.SetDataTable(협조전라인, 2);

                        Util.RPT_Print(reportHelper);
                    }   
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void 상업송장데이터(string 회사코드, DataRow[] dataRowArray, ref DataTable 상업송장헤더, ref DataTable 상업송장라인, ref DataTable 인수증라인)
        {
            DataRow tmpRow;
            string 협조전번호, 수주번호;
            
            try
            {
                협조전번호 = string.Empty;
                foreach (DataRow dr in ComFunc.getGridGroupBy(dataRowArray, new string[] { "NO_GIR" }, true).Rows)
				{
                    this.QR코드생성(회사코드, D.GetString(dr["NO_GIR"]));
                    협조전번호 += "|" + D.GetString(dr["NO_GIR"]);
                }
                
                DataSet ds = this._biz.협조전데이터(new object[] { Global.MainFrame.ServerKey,
                                                                   회사코드,
                                                                   Global.MainFrame.LoginInfo.Language,
                                                                   협조전번호 });

                상업송장헤더 = ds.Tables[0];
                상업송장라인 = ds.Tables[1].Clone();
                인수증라인 = ds.Tables[1].Clone();

                상업송장라인.Columns.Add("TP_ROW");
                상업송장라인.Columns.Add("CD_DM_CI");
                상업송장라인.Columns.Add("CD_BAR_CI");

                인수증라인.Columns.Add("납품처정보");
                인수증라인.Columns.Add("CD_DM_RCT");
                인수증라인.Columns.Add("CD_BAR_RCT");

                수주번호 = string.Empty;

                string filePath = Path.Combine(Application.StartupPath, "temp");
                string fileName = string.Empty;
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);

                foreach (DataRow dr in 상업송장헤더.Rows)
				{
                    string 납품처정보 = string.Empty;

                    if (dr["CD_COMPANY"].ToString() == "S100" &&
                        (((dr["CD_PARTNER"].ToString() == "01993" || dr["CD_PARTNER"].ToString() == "17066" || dr["CD_PARTNER"].ToString() == "10750" || dr["CD_PARTNER"].ToString() == "01463") && dr["CD_DELIVERY_TO"].ToString() == "DVR210600003") ||
                         (dr["CD_PARTNER"].ToString() == "13660" && dr["CD_DELIVERY_TO"].ToString() == "DVR220500001")))
                    {
                        납품처정보 = dr["NM_CONSIGNEE"].ToString() + Environment.NewLine +
                                    dr["ADDR1_CONSIGNEE"].ToString() + Environment.NewLine +
                                    dr["ADDR2_CONSIGNEE"].ToString() + Environment.NewLine +
                                    "TEL : " + dr["TEL"].ToString() + Environment.NewLine +
                                    "PIC : " + dr["PIC"].ToString();
                    }

                    foreach (DataRow dr1 in ds.Tables[1].Select("NO_GIR = '" + dr["NO_GIR"].ToString() + "'"))
                    {
                        // 자품목 제외
                        if (D.GetString(dr1["TP_BOM"]) != "C")
                        {
                            if (수주번호 != D.GetString(dr1["NO_SO"]))
                            {
                                tmpRow = 상업송장라인.NewRow();

                                tmpRow["NO_GIR"] = dr1["NO_GIR"];
                                tmpRow["NO_SO"] = dr1["NO_SO"];
                                tmpRow["NO_PO_PARTNER"] = dr1["NO_PO_PARTNER"];
                                tmpRow["NM_ITEM_PARTNER"] = "YOUR ORDER NO. : " + D.GetString(dr1["NO_PO_PARTNER"]) + " / " + "OUR REF. : " + D.GetString(dr1["NO_SO"]);

                                상업송장라인.Rows.Add(tmpRow);

                                수주번호 = D.GetString(dr1["NO_SO"]);
                            }

                            상업송장라인.ImportRow(dr1);
                            상업송장라인.Rows[상업송장라인.Rows.Count - 1]["TP_ROW"] = "I";

                            fileName = filePath + "\\" + dr1["NO_GIR"].ToString() + "_CI_DM.png";

                            if (!string.IsNullOrEmpty(dr1["CD_QR_CI"].ToString()) && !File.Exists(fileName))
                            {
                                string[] qr코드Array = dr1["CD_QR_CI"].ToString().Split('/');
                                QR.데이터매트릭스(qr코드Array[qr코드Array.Length - 1], fileName);
                            }

                            상업송장라인.Rows[상업송장라인.Rows.Count - 1]["CD_DM_CI"] = fileName;

                            fileName = filePath + "\\" + dr1["NO_GIR"].ToString() + "_CI_BAR.png";

                            if (!string.IsNullOrEmpty(dr1["CD_QR_CI"].ToString()) && !File.Exists(fileName))
                            {
                                string[] qr코드Array = dr1["CD_QR_CI"].ToString().Split('/');
                                QR.바코드(qr코드Array[qr코드Array.Length - 1], fileName);
                            }

                            상업송장라인.Rows[상업송장라인.Rows.Count - 1]["CD_BAR_CI"] = fileName;

                            if (D.GetString(dr1["CD_ITEM"]).Substring(0, 2) != "SD")
							{
                                인수증라인.ImportRow(dr1);
                                인수증라인.Rows[인수증라인.Rows.Count - 1]["납품처정보"] = 납품처정보;

                                fileName = filePath + "\\" + dr1["NO_GIR"].ToString() + "-" + dr1["NO_SO"].ToString() + "_RCT_DM.png";

                                if (!string.IsNullOrEmpty(dr1["CD_QR_RCT"].ToString()) && !File.Exists(fileName))
								{
                                    string[] qr코드Array = dr1["CD_QR_RCT"].ToString().Split('/');
                                    QR.데이터매트릭스(qr코드Array[qr코드Array.Length - 1], fileName);
                                }
                                
                                인수증라인.Rows[인수증라인.Rows.Count - 1]["CD_DM_RCT"] = fileName;

                                fileName = filePath + "\\" + dr1["NO_GIR"].ToString() + "-" + dr1["NO_SO"].ToString() + "_RCT_BAR.png";

                                if (!string.IsNullOrEmpty(dr1["CD_QR_RCT"].ToString()) && !File.Exists(fileName))
								{
                                    string[] qr코드Array = dr1["CD_QR_RCT"].ToString().Split('/');
                                    QR.바코드(qr코드Array[qr코드Array.Length - 1], fileName);
                                }
                                
                                인수증라인.Rows[인수증라인.Rows.Count - 1]["CD_BAR_RCT"] = fileName;
                            }   
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void 포장명세서데이터(DataRow[] dataRowArray, ref DataTable 포장명세서헤더, ref DataTable 포장명세서라인)
        {
            DataRow[] dataRowArray2;
            DataRow tmpRow;
            string 수주번호;

            try
            {
                포장명세서헤더 = this._flexH.DataTable.Clone();
                포장명세서헤더.Columns.Add("CD_QR");
                포장명세서헤더.Columns.Add("CD_DM_PL");
                포장명세서헤더.Columns.Add("CD_BAR_PL");

                포장명세서라인 = this._flexL.DataTable.Clone();
                포장명세서라인.Columns.Add("TP_ROW");

                수주번호 = string.Empty;

                string filePath = Path.Combine(Application.StartupPath, "temp");
                string fileName = string.Empty;
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);

                foreach (DataRow dr in dataRowArray)
                {
                    포장명세서헤더.ImportRow(dr);

                    if (dr["CD_QR_PL"].ToString() != "PACK")
					{
                        this.QR코드생성(dr["CD_COMPANY"].ToString(), dr["NO_GIR"].ToString());

                        DataTable 협조전라인 = DBHelper.GetDataTable(string.Format(@"SELECT ISNULL(GL.TXT_USERDEF3, 'PL') AS CD_QR_PL
																	                FROM SA_GIRL GL WITH(NOLOCK)
																	                WHERE GL.CD_COMPANY = '{0}'
																	                AND GL.NO_GIR = '{1}'", dr["CD_COMPANY"].ToString(), dr["NO_GIR"].ToString()));

                        dr["CD_QR_PL"] = 협조전라인.Rows[0]["CD_QR_PL"].ToString();
                    }

                    포장명세서헤더.Rows[포장명세서헤더.Rows.Count - 1]["CD_QR"] = dr["NM_VESSEL"].ToString() + " / " + dr["NO_PO_PARTNER"].ToString();

                    fileName = filePath + "\\" + dr["NO_GIR"].ToString() + "_PL_DM.png";

                    if (!string.IsNullOrEmpty(dr["CD_QR_PL"].ToString()) && !File.Exists(fileName))
					{
                        string[] qr코드Array = dr["CD_QR_PL"].ToString().Split('/');
                        QR.데이터매트릭스(qr코드Array[qr코드Array.Length - 1], fileName);
                    }
                    
                    포장명세서헤더.Rows[포장명세서헤더.Rows.Count - 1]["CD_DM_PL"] = fileName;

                    fileName = filePath + "\\" + dr["NO_GIR"].ToString() + "_PL_BAR.png";

                    if (!string.IsNullOrEmpty(dr["CD_QR_PL"].ToString()) && !File.Exists(fileName))
					{
                        string[] qr코드Array = dr["CD_QR_PL"].ToString().Split('/');
                        QR.바코드(qr코드Array[qr코드Array.Length - 1], fileName);
                    }
                    
                    포장명세서헤더.Rows[포장명세서헤더.Rows.Count - 1]["CD_BAR_PL"] = fileName;

                    dataRowArray2 = this._flexL.DataTable.Select("CD_COMPANY = '" + dr["CD_COMPANY"].ToString() + "' AND NO_KEY = '" + dr["NO_KEY"].ToString() + "'");

                    foreach (DataRow dr1 in dataRowArray2)
                    {
                        if (수주번호 != D.GetString(dr1["NO_FILE"]))
                        {
                            tmpRow = 포장명세서라인.NewRow();

                            tmpRow["NO_GIR"] = dr1["NO_GIR"];
                            tmpRow["NO_KEY"] = dr1["NO_KEY"];
                            tmpRow["NO_FILE"] = dr1["NO_FILE"];
                            tmpRow["NO_PO_PARTNER"] = dr1["NO_PO_PARTNER"];
                            tmpRow["NM_ITEM_PARTNER"] = "YOUR ORDER NO. : " + D.GetString(dr1["NO_PO_PARTNER"]) + " / " + "OUR REF. : " + D.GetString(dr1["NO_FILE"]);

                            포장명세서라인.Rows.Add(tmpRow);

                            수주번호 = D.GetString(dr1["NO_FILE"]);
                        }

                        포장명세서라인.ImportRow(dr1);
                        포장명세서라인.Rows[포장명세서라인.Rows.Count - 1]["TP_ROW"] = "I";
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void 협조전데이터(string 회사코드, DataRow[] dataRowArray, ref DataTable 협조전헤더, ref DataTable 협조전라인)
        {
            string 협조전번호;

            try
            {
                협조전번호 = string.Empty;

                foreach (DataRow dr in ComFunc.getGridGroupBy(dataRowArray, new string[] { "NO_GIR" }, true).Rows)
				{
                    this.QR코드생성(회사코드, D.GetString(dr["NO_GIR"]));
                    협조전번호 += "|" + D.GetString(dr["NO_GIR"]);
                }
                
                DataSet ds = this._biz.협조전데이터(new object[] { Global.MainFrame.ServerKey,
                                                                   회사코드,
                                                                   Global.MainFrame.LoginInfo.Language,
                                                                   협조전번호 });

                협조전헤더 = ds.Tables[0];
                협조전라인 = ds.Tables[1];
                협조전라인 = new DataView(협조전라인, "TP_BOM <> 'C' AND SUBSTRING(CD_ITEM, 1, 2) <> 'SD'", "", DataViewRowState.CurrentRows).ToTable();
                협조전라인.Columns.Add("납품처정보");

                foreach (DataRow dr in 협조전헤더.Rows)
				{
                    string 납품처정보 = string.Empty;

                    if (dr["CD_COMPANY"].ToString() == "S100" &&
                        (((dr["CD_PARTNER"].ToString() == "01993" || dr["CD_PARTNER"].ToString() == "17066" || dr["CD_PARTNER"].ToString() == "10750" || dr["CD_PARTNER"].ToString() == "01463") && dr["CD_DELIVERY_TO"].ToString() == "DVR210600003") ||
                         (dr["CD_PARTNER"].ToString() == "13660" && dr["CD_DELIVERY_TO"].ToString() == "DVR220500001")))
                    {
                        납품처정보 = dr["NM_CONSIGNEE"].ToString() + Environment.NewLine +
                                    dr["ADDR1_CONSIGNEE"].ToString() + Environment.NewLine +
                                    dr["ADDR2_CONSIGNEE"].ToString() + Environment.NewLine +
                                    "TEL : " + dr["TEL"].ToString() + Environment.NewLine +
                                    "PIC : " + dr["PIC"].ToString();
                    }

                    foreach (DataRow dr1 in 협조전라인.Select("NO_GIR = '" + dr["NO_GIR"].ToString() + "'"))
                    {
                        dr1["납품처정보"] = 납품처정보;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private int 열넓이(string 리포트코드)
        {
            DataTable dt;
            int width = 0;
            string query;

            try
            {
                query = @"SELECT TOP 1 *
                          FROM MA_REPORTL WITH(NOLOCK) 
                          WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + @"'
                          AND CD_SYSTEM = '" + 리포트코드 + @"'
                          AND ISNULL(CD_FLAG, '') != ''";

                dt = DBHelper.GetDataTable(query);

                if (dt.Rows.Count == 1)
                    width = D.GetInt(dt.Rows[0]["CD_FLAG"]);
                else
                    width = 220;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return width;
        }

        private bool QR코드생성(string 회사코드, string 의뢰번호)
        {
            string query;

            try
            {
                query = @"SELECT GL.NO_SO 
						  FROM SA_GIRH GH
						  JOIN SA_GIRL GL ON GL.CD_COMPANY = GH.CD_COMPANY AND GL.NO_GIR = GH.NO_GIR
						  WHERE GH.CD_COMPANY = '{0}'
						  AND GH.NO_GIR = '{1}'
						  AND ISNULL(GH.YN_RETURN, 'N') <> 'Y'
						  AND EXISTS (SELECT 1 
						  			  FROM SA_GIRL GL
						  			  WHERE GL.CD_COMPANY = GH.CD_COMPANY
						  			  AND GL.NO_GIR = GH.NO_GIR
						  			  AND ISNULL(GL.TXT_USERDEF1, '') = '')
						  GROUP BY GL.NO_SO";

                DataTable tmpDt = DBHelper.GetDataTable(string.Format(query, 회사코드, 의뢰번호));

                if (tmpDt != null && tmpDt.Rows.Count > 0)
                {
                    string 상업송장qr = SHORTNER.상업송장(string.Format("{0}/{1}", 회사코드, 의뢰번호));
                    string 포장명세서qr = SHORTNER.포장명세서(string.Format("{0}/{1}", 회사코드, 의뢰번호));

                    foreach (DataRow dr1 in tmpDt.Rows)
                    {
                        string 인수증qr = SHORTNER.인수증(string.Format("{0}/{1}/{2}", 회사코드, 의뢰번호, dr1["NO_SO"].ToString()));

                        DBHelper.ExecuteScalar(string.Format(@"UPDATE SA_GIRL
                                                               SET TXT_USERDEF1 = '{3}',
                                                                   TXT_USERDEF2 = '{4}',
                                                                   TXT_USERDEF3 = '{5}'
                                                               WHERE CD_COMPANY = '{0}'
                                                               AND NO_GIR = '{1}'
                                                               AND NO_SO = '{2}'", 회사코드, 의뢰번호, dr1["NO_SO"].ToString(), 인수증qr, 상업송장qr, 포장명세서qr));
                    }

                    return true;
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

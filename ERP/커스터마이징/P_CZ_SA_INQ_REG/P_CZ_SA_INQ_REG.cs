using Aspose.Email.Outlook;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Dintec.AutoMail;
using Dintec.AutoWeb;
using Duzon.Common.Forms;
using Duzon.Erpiu.ComponentModel;
using Duzon.ERPU;
using Parsing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace cz
{
	public partial class P_CZ_SA_INQ_REG : Duzon.Common.Forms.CommonDialog
    {
        #region 생성자 & 초기화
        private P_CZ_SA_INQ_REG_BIZ _biz;
        private DataRow defaultValue;
        private string 임시폴더;

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

        private string 파일유형
        {
            get
            {
                if (this.rdo고객문의서.Checked == true)
                    return "01";
                else if (this.rdo고객문의서첨부.Checked == true)
                    return "011";
                else if (this.rdo매입견적.Checked == true)
                    return "04";
                else if (this.rdo고객발주서.Checked == true)
                    return "08";
                else if (this.rdo매입확정.Checked == true)
                    return "50";
                else if (this.rdo납품지침.Checked == true)
                    return "51";
                else if (this.rdo클레임.Checked == true)
                    return "53";
                else if (this.rdo관련도면.Checked == true)
                    return "54";
                else if (this.rdo서트.Checked == true)
                    return "61";
                else
                    return "55";
            }
        }

        public P_CZ_SA_INQ_REG(string noFile)
        {
            InitializeComponent();

            this.txt파일번호S.Text = noFile;
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.AllowDrop = true;
            this._biz = new P_CZ_SA_INQ_REG_BIZ();
            this.임시폴더 = "temp";

			// 라이선스 인증
			Aspose.Email.License license = new Aspose.Email.License();
			license.SetLicense("Aspose.Email.lic");

			this.InitGrid();
            this.InitEvent();
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            if (Settings.Default.입력담당할당 == this.rdo자동.Text)
                this.rdo자동.Checked = true;
            else if (Settings.Default.입력담당할당 == this.rdo현대.Text)
                this.rdo현대.Checked = true;
            else
                this.rdo수동.Checked = true;

            this.ctx회사.CodeValue = Global.MainFrame.LoginInfo.CompanyCode;
            this.ctx회사.CodeName = Global.MainFrame.LoginInfo.CompanyName;

            this.SetEnableControl(false);
            this.SetComboData();

            this.rdo고객문의서.Checked = true;
            this.Control_Click(this.rdo고객문의서, null);
        }

        private void InitGrid()
        {
            this._flexH.DetailGrids = new FlexGrid[] { this._flexL };

            #region Header
            this._flexH.BeginSetting(1, 1, false);

            this._flexH.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            this._flexH.SetCol("NO_KEY", "파일번호", 80, false);
            this._flexH.SetCol("TP_SALES", "영업유형", 80, false);
            this._flexH.SetCol("ID_SALES", "영업담당", 80, false);
            this._flexH.SetCol("ID_TYPIST", "입력담당", 80, false);
            this._flexH.SetCol("ID_PUR", "구매담당", 80, false);
            this._flexH.SetCol("ID_LOG", "영업물류", 80, false);
            this._flexH.SetCol("DC_RMK", "비고", 80, true);

            this._flexH.SetOneGridBinding(new object[] { this.txt파일번호 }, new IUParentControl[] { this.pnlHeader, this.pnl비고 });

            this._flexH.SetDummyColumn("S");
            this._flexH.ExtendLastCol = true;
            this._flexH.KeyActionEnter = KeyActionEnum.MoveDown;

            this._flexH.SettingVersion = "1.0.0.0";
            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region Line
            this._flexL.BeginSetting(1, 1, false);

            this._flexL.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            this._flexL.SetCol("YN_PARSING", "파싱", 45, false, CheckTypeEnum.Y_N);
			this._flexL.SetCol("YN_INCLUDED", "첨부여부", 45, false, CheckTypeEnum.Y_N);
			this._flexL.SetCol("NO_LINE", "파일항번", false);
            this._flexL.SetCol("FULL_PATH", "로컬파일경로", false);
            this._flexL.SetCol("NM_FILE", "파일명", 200);
            this._flexL.SetCol("CD_SUPPLIER", "매입처코드", false);
            this._flexL.SetCol("NM_SUPPLIER", "매입처", false);
            this._flexL.SetCol("DTS_INSERT", "등록일자", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexL.SetCol("FILE_STATE", "상태", 80);
            
            this._flexL.Cols["DTS_INSERT"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexL.Cols["FILE_STATE"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexL.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";

            this._flexL.SetCodeHelpCol("NM_SUPPLIER", "H_CZ_SA_PARTNER_SUB", ShowHelpEnum.Always, new string[] { "CD_SUPPLIER", "NM_SUPPLIER" }, new string[] { "CD_PARTNER", "NM_PARTNER" }); 

            this._flexL.SetDummyColumn("S");

            this._flexL.SettingVersion = "1.0.0.0";
            this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region Detail
            this._flexD.BeginSetting(1, 1, false);

            this._flexD.SetCol("YN_DEFAULT", "기본", 40, true, CheckTypeEnum.Y_N);
            this._flexD.SetCol("YN_AUTO", "자동", 40, true, CheckTypeEnum.Y_N);
            this._flexD.SetCol("YN_MAPS", "현대", 40, true, CheckTypeEnum.Y_N);
            this._flexD.SetCol("NM_USER", "이름", 80);
            this._flexD.SetCol("QT_ALL", "전체", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("QT_DONE", "완료", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("QT_DONE_DAY", "오늘완료", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("QT_PROGRESS", "진행", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("YN_EXPECT", "제외", 40, false, CheckTypeEnum.Y_N);
            this._flexD.SetCol("NM_WCODE", "근무구분", 80);

            this._flexD.SetDummyColumn("YN_DEFAULT");
            this._flexD.EnabledHeaderCheck = false;

            this._flexD.SettingVersion = "1.0.0.0";
            this._flexD.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion
        }

        private void InitEvent()
        {
            this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
            this._flexH.ValidateEdit += new ValidateEditEventHandler(this._flexH_ValidateEdit);
            this._flexH.AfterEdit += new RowColEventHandler(this._flexH_AfterEdit);
            this._flexH.CheckHeaderClick += new EventHandler(this._flex_CheckHeaderClick);
            
            this._flexL.ValidateEdit += new ValidateEditEventHandler(this._flexL_ValidateEdit);
            this._flexL.CheckHeaderClick += new EventHandler(this._flex_CheckHeaderClick);
            this._flexL.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flexL_BeforeCodeHelp);
            this._flexL.MouseDoubleClick += new MouseEventHandler(this._flexL_MouseDoubleClick);

            this._flexD.ValidateEdit += new ValidateEditEventHandler(this._flexD_ValidateEdit);
            this._flexD.AfterEdit += new RowColEventHandler(this._flexD_AfterEdit);

            this.DragOver += new DragEventHandler(this.Control_DragOver);
            this.DragDrop += new DragEventHandler(this.Control_DragDrop);

            this.txt파일번호S.KeyDown += new KeyEventHandler(this.txt파일번호S_KeyDown);
            this.txt파일번호.Leave += new EventHandler(this.txt파일번호_Leave);

            this.btn초기화.Click += new EventHandler(this.btn초기화_Click);
            this.btn파일추가.Click += new EventHandler(this.btn파일추가_Click);
            this.btn파일삭제.Click += new EventHandler(this.btn파일삭제_Click);
            this.btn다운로드.Click += new EventHandler(this.btn다운로드_Click);
            this.btn변경사항저장.Click += new EventHandler(this.btn변경사항저장_Click);
            this.btn조회.Click += new EventHandler(this.btn갱신_Click);
            this.btn닫기.Click += new EventHandler(this.btn닫기_Click);

            this.rdo고객문의서.Click += new EventHandler(this.Control_Click);
            this.rdo매입견적.Click += new EventHandler(this.Control_Click);
            this.rdo고객발주서.Click += new EventHandler(this.Control_Click);
            this.rdo매입확정.Click += new EventHandler(this.Control_Click);
            this.rdo납품지침.Click += new EventHandler(this.Control_Click);
            this.rdo클레임.Click += new EventHandler(this.Control_Click);
            this.rdo관련도면.Click += new EventHandler(this.Control_Click);
            this.rdo기타.Click += new EventHandler(this.Control_Click);
            this.rdo고객문의서첨부.Click += new EventHandler(this.Control_Click);
			this.rdo서트.Click += new EventHandler(this.Control_Click);
        }
		#endregion

		#region 버튼 이벤트
		private void btn초기화_Click(object sender, EventArgs e)
        {
            
            try
            {
                this.txt파일번호S.Text = string.Empty;

                this.초기화();

                this.txt파일번호S.Focus();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn파일추가_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = true;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() != DialogResult.OK) return;

                string[] fileNames = openFileDialog.FileNames;

                this.SetEnableControl(false);

                foreach (string fileName in fileNames)
                {
                    this.AddData(fileName);
                }

                this.SetEnableControl(true);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn파일삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexL.HasNormalRow) return;

                DataRow[] dataRowArray = this._flexL.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

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
                        dataRow["FILE_STATE"] = D.GetString(P_CZ_SA_INQ_REG.상태.삭제대기);
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn다운로드_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexL.HasNormalRow) return;

                DataRow[] dataRowArray = this._flexL.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

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
                        FileMgr.Download_WF(this.ctx회사.CodeValue, D.GetString(dataRow["NO_KEY"]), dataRow["NM_FILE_REAL"].ToString(), (folderBrowserDialog.SelectedPath + "\\" + dataRow["NM_FILE_REAL"].ToString()), false);
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

        private void btn변경사항저장_Click(object sender, EventArgs e)
        {
            string fileNo, tmpNo;
            DataTable dtRPAQueue, dtMsgSend;

            try
            {
                if (!this._flexH.IsDataChanged && !this._flexL.IsDataChanged) return;

                this.Enabled = false;

                if (rdo고객문의서.Checked == true)
                {
                    if (this._flexH.DataTable.Select("ISNULL(TP_SALES, '') = ''").Length > 0)
                    {
                        Global.MainFrame.ShowMessage("CZ_@ 이(가) 지정되지 않은 행이 있습니다.", Global.MainFrame.DD("영업유형"));
                        return;
                    }

                    if (this._flexH.DataTable.Select("ISNULL(ID_SALES, '') = ''").Length > 0)
                    {
                        Global.MainFrame.ShowMessage("CZ_@ 이(가) 지정되지 않은 행이 있습니다.", Global.MainFrame.DD("영업담당"));
                        return;
                    }

                    if (this._flexH.DataTable.Select("ISNULL(ID_TYPIST, '') = ''").Length > 0)
                    {
                        Global.MainFrame.ShowMessage("CZ_@ 이(가) 지정되지 않은 행이 있습니다.", Global.MainFrame.DD("입력담당"));
                        return;
                    }
                }
                else
                {
                    if (this._flexH.DataTable.Select("ISNULL(NO_KEY, '') = ''").Length > 0)
                    {
                        Global.MainFrame.ShowMessage("CZ_@ 이(가) 지정되지 않은 행이 있습니다.", Global.MainFrame.DD("파일번호"));
                        return;
                    }

                    if (this.rdo고객발주서.Checked == true)
                    {
                        if (this._flexH.DataTable.Select("ISNULL(ID_PUR, '') = ''").Length > 0)
                        {
                            Global.MainFrame.ShowMessage("CZ_@ 이(가) 지정되지 않은 행이 있습니다.", Global.MainFrame.DD("구매담당"));
                            return;
                        }

                        if (Global.MainFrame.LoginInfo.CompanyCode != "K100" && 
                            this._flexH.DataTable.Select("ISNULL(ID_LOG, '') = ''").Length > 0)
                        {
                            Global.MainFrame.ShowMessage("CZ_@ 이(가) 지정되지 않은 행이 있습니다.", Global.MainFrame.DD("물류담당"));
                            return;
                        }
                    }

                    if (this.rdo매입견적.Checked || this.rdo매입확정.Checked)
                    {
                        if (this._flexL.DataTable.Select("ISNULL(CD_SUPPLIER, '') = '' AND YN_INCLUDED <> 'Y'").Length > 0)
                        {
                            Global.MainFrame.ShowMessage("CZ_@ 이(가) 지정되지 않은 행이 있습니다.", Global.MainFrame.DD("매입처"));
                            return;
                        }
                    }
                }

                foreach (DataRow drL in this._flexL.DataTable.Select("FILE_STATE = '업로드대기' OR FILE_STATE = '삭제대기'"))
                {
                    if (drL["FILE_STATE"].ToString() == "업로드대기" && this.IsFileLocked(drL["FULL_PATH"].ToString())) 
                        return;
                    else if (drL["FILE_STATE"].ToString() == "삭제대기")
                        drL.Delete();
                }

                dtRPAQueue = new DataTable();

                dtRPAQueue.Columns.Add("NO_KEY");
                dtRPAQueue.Columns.Add("ID_SALES");
                dtRPAQueue.Columns.Add("FULL_PATH");

                dtMsgSend = new DataTable();

                dtMsgSend.Columns.Add("NO_KEY");
                dtMsgSend.Columns.Add("NM_SUPPLIER");

                foreach (DataRow drH in this._flexH.DataTable.Rows)
                {
                    fileNo = string.Empty;
                    tmpNo = drH["NO_KEY"].ToString();
                    
                    if (this.rdo고객문의서.Checked == true)
                    {
                        if (drH.RowState == DataRowState.Added)
                        {
                            if (this._flexL.DataTable.Select("NO_KEY = '" + tmpNo + "'").Length == 0)
                            {
                                drH.Delete();
                                continue;
                            }
                            else
                            {
                                fileNo = (string)Global.MainFrame.GetSeq(this.ctx회사.CodeValue, "CZ", D.GetString(drH["TP_SALES"]), Global.MainFrame.GetStringToday.Substring(2, 2));
                                drH["NO_KEY"] = fileNo;
                            }
                        }
                        else
                        {
                            fileNo = D.GetString(drH["NO_KEY"]);
                        }
                    }
                    else
                    {
                        fileNo = D.GetString(drH["NO_KEY"]);
                    }

                    foreach (DataRow drL in this._flexL.DataTable.Select("NO_KEY = '" + tmpNo + "' AND FILE_STATE = '업로드대기'"))
                    {
                        drL["NO_KEY"] = fileNo;

                        if (this.rdo고객문의서.Checked == true && 
                            drL["NM_FILE"].ToString().StartsWith("현대웹") &&
                            dtRPAQueue.Select("NO_KEY = '" + drL["NO_KEY"].ToString() + "'").Length == 0)
                        {
                            DataRow dr = dtRPAQueue.NewRow();

                            dr["NO_KEY"] = drL["NO_KEY"].ToString();
                            dr["ID_SALES"] = drH["ID_SALES"].ToString();
                            dr["FULL_PATH"] = drL["FULL_PATH"].ToString();

                            dtRPAQueue.Rows.Add(dr);
                        }
                        else if (this.rdo매입견적.Checked == true)
                        {
                            if (Global.MainFrame.LoginInfo.CompanyCode == "K100" && 
                                drL["CD_SUPPLIER"].ToString() == "00047")
                            {
                                DataTable dt = DBHelper.GetDataTable(string.Format(@"SELECT 1 
                                                                                     FROM CZ_SA_QTNH QH
                                                                                     JOIN CZ_PU_QTNH QH1 ON QH1.CD_COMPANY = QH.CD_COMPANY AND QH1.NO_FILE = QH.NO_FILE
                                                                                     JOIN MA_EMP ME ON ME.CD_COMPANY = QH.CD_COMPANY AND ME.NO_EMP = QH.NO_EMP_QTN
                                                                                     WHERE QH.CD_COMPANY = 'K100'
                                                                                     AND QH.NO_FILE = '{0}'
                                                                                     AND QH1.CD_PARTNER = '00047'
                                                                                     AND ME.CD_DEPT = '010900'", drL["NO_KEY"].ToString()));

                                if (dt != null && 
                                    dt.Rows.Count > 0 &&
                                    dtMsgSend.Select("NO_KEY = '" + drL["NO_KEY"].ToString() + "'").Length == 0)
                                {
                                    DataRow dr = dtMsgSend.NewRow();

                                    dr["NO_KEY"] = drL["NO_KEY"].ToString();
                                    dr["NM_SUPPLIER"] = drL["NM_SUPPLIER"].ToString();

                                    dtMsgSend.Rows.Add(dr);
                                }
                            }

                            try
                            {
                                if (QuotationFinder.IsPossible(this.ctx회사.CodeValue, D.GetString(drL["CD_SUPPLIER"]), D.GetString(drL["FULL_PATH"])) == true)
                                    drL["YN_PARSING"] = "Y";
                                else
                                    drL["YN_PARSING"] = "N";
                            }
                            catch
                            {
                                drL["YN_PARSING"] = "N";
                            }
                        }

                        try
                        {
                            drL["NM_FILE_REAL"] = FileMgr.Upload_WF(this.ctx회사.CodeValue, D.GetString(drL["NO_KEY"]), D.GetString(drL["FULL_PATH"]), false);
                            drL["FILE_STATE"] = P_CZ_SA_INQ_REG.상태.전송완료;
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message.IndexOf("System.ArgumentException") >= 0)
                                Global.MainFrame.ShowMessage(@"파일이름에 사용할 수 없는 문자가 포함되어 있습니다." + Environment.NewLine +
                                                              ex.Message.Split('\n')[1].Replace("\r", string.Empty) + Environment.NewLine +
                                                              "해당 파일 제외 후 저장됩니다." + Environment.NewLine +
                                                              "파일이름 변경 후 다시 시도하시기 바랍니다.");
                            else
                                Global.MainFrame.MsgEnd(ex);
                            
                            drL.Delete();
                        }
                    }

                    if (this.rdo고객문의서.Checked == true &&
                        drH.RowState == DataRowState.Added &&
                        this._flexL.DataTable.Select("NO_KEY = '" + fileNo + "'").Length == 0)
                        drH.Delete();
					else if (this._flexL.DataTable.Select("NO_KEY = '" + fileNo + "'").Length == 0)
						drH.Delete();
				}

                if (!this._flexH.IsDataChanged && !this._flexL.IsDataChanged)
                {
                    Global.MainFrame.ShowMessage(공통메세지.변경된내용이없습니다);
                }
                else if (this._biz.SaveData(this._flexH.GetChanges(), this._flexL.GetChanges(), this.파일유형, this.ctx회사.CodeValue) == true)
                {
                    foreach(DataRow dr in dtRPAQueue.Rows)
                    {
                        this.HGS(dr["NO_KEY"].ToString(), Global.MainFrame.LoginInfo.CompanyCode, dr["ID_SALES"].ToString(), dr["FULL_PATH"].ToString());
                    }

                    string contents;

                    foreach (DataRow dr in dtMsgSend.Rows)
                    {
                        contents = @"** 매입견적서 등록 알림

- 매입처 : {0}
- 파일번호 : {1}

※ 본 쪽지는 발신 전용 입니다.";

                        contents = string.Format(contents, dr["NM_SUPPLIER"].ToString(), dr["NO_KEY"].ToString());

                        Messenger.SendMSG(new string[] { "S-495", "S-596", "S-579" }, contents);
                    }

                    this._flexH.AcceptChanges();
                    this._flexL.AcceptChanges();

                    Global.MainFrame.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                this.Enabled = true;
            }
        }

        private void HGS(string fileHGSNo, string cd_companyHGS, string no_empHGS, string fileHgsName)
        {
            try
            {
                InquiryParser parser = new InquiryParser(fileHgsName);
                parser.Parse(true);

                if (parser.Item.Rows.Count > 0)
                {
                    // 호선 가져오기
                    string query = @"
SELECT
	  A.NO_IMO
	, A.NO_HULL
	, A.NM_VESSEL
	, B.CD_PARTNER
	, B.LN_PARTNER
	, B.CD_PARTNER_GRP
FROM	  CZ_MA_HULL	AS A WITH(NOLOCK)
LEFT JOIN MA_PARTNER	AS B WITH(NOLOCK) ON A.CD_PARTNER = B.CD_PARTNER AND B.CD_COMPANY = @CD_COMPANY
WHERE A.NO_IMO = @NO_IMO OR (@NO_IMO IS NULL AND A.NM_VESSEL LIKE '%' + @NM_VESSEL + '%')";

                    DBMgr dbm = new DBMgr();
                    dbm.DebugMode = DebugMode.Print;
                    dbm.Query = query;
                    dbm.AddParameter("@CD_COMPANY", cd_companyHGS);
                    dbm.AddParameter("@NO_IMO", parser.ImoNumber);
                    dbm.AddParameter("@NM_VESSEL", parser.Vessel);
                    DataTable dt = dbm.GetDataTable();

                    string imonumber = string.Empty;
                    string partnercode = string.Empty;
                    string referencenumber = string.Empty;


                    int no_line = 1;
                    string nm_subject = string.Empty;
                    string unit = string.Empty;
                    string cd_item_partner = string.Empty;
                    string nm_item_partner = string.Empty;
                    string cd_uniq_partner = string.Empty;
                    string qt = string.Empty;
                    string cd_po_partner = string.Empty;
                    string cd_stock = string.Empty;
                    string cd_rate = string.Empty;
                    string vesselName = string.Empty;
                    string tnid = string.Empty;
                    string buyer = string.Empty;

                    string contact = string.Empty;
                    string filenameStr = string.Empty;

                    partnercode = "11823";

                    if (dt.Rows.Count == 1)
                        imonumber = dt.Rows[0]["NO_IMO"].ToString().Trim();
                    else
                        imonumber = parser.ImoNumber;

                    // 문의번호
                    referencenumber = parser.Reference;

                    if (referencenumber.Equals(DateTime.Now.ToString("yyyy.MM.dd")))
                        referencenumber = referencenumber + "." + partnercode + DateTime.Now.ToString("mmm");


                    tnid = parser.Tnid;
                    vesselName = parser.Vessel;
                    // BUYER COMMENT 따로 저장
                    buyer = parser.Buyer;
                    contact = parser.Contact;
                    filenameStr = parser.InqFileName;

                    DataTable dtItem = new DataTable();


                    dtItem.Columns.Add("CD_COMPANY");
                    dtItem.Columns.Add("NO_PREREG");
                    dtItem.Columns.Add("NO_LINE");
                    dtItem.Columns.Add("CD_PARTNER");
                    dtItem.Columns.Add("NO_IMO");
                    dtItem.Columns.Add("SHIPSERV_TNID");
                    dtItem.Columns.Add("NM_VESSEL");
                    dtItem.Columns.Add("NO_REF");
                    dtItem.Columns.Add("NO_REQREF");
                    dtItem.Columns.Add("NM_SUBJECT");
                    dtItem.Columns.Add("CD_ITEM_PARTNER");
                    dtItem.Columns.Add("NM_ITEM_PARTNER");
                    dtItem.Columns.Add("CD_UNIQ_PARTNER");
                    dtItem.Columns.Add("UNIT");
                    dtItem.Columns.Add("QT");
                    dtItem.Columns.Add("SORTED_BY");
                    dtItem.Columns.Add("COMMENT");
                    dtItem.Columns.Add("CONTACT");
                    dtItem.Columns.Add("NM_FILE");
                    dtItem.Columns.Add("DXCODE_SUBJ");
                    dtItem.Columns.Add("DXCODE_ITEM");


                    DataTable dtItemVendor = new DataTable();
                    dtItemVendor.Columns.Add("CD_COMPANY");
                    dtItemVendor.Columns.Add("NO_PREREG");
                    dtItemVendor.Columns.Add("NO_LINE");
                    dtItemVendor.Columns.Add("DXVENDOR_CODE");


                    DataTable dtItemHead = new DataTable();
                    dtItemHead.Columns.Add("CD_COMPANY");
                    dtItemHead.Columns.Add("NO_PREREG");
                    dtItemHead.Columns.Add("NO_FILE");
                    dtItemHead.Columns.Add("NO_EMP");
                    dtItemHead.Columns.Add("CD_PARTNER");
                    dtItemHead.Columns.Add("NO_IMO");



                    foreach (DataRow row in parser.Item.Rows)
                    {
                        nm_subject = row["SUBJ"].ToString().ToUpper();
                        unit = row["UNIT"].ToString().ToUpper();
                        cd_item_partner = row["ITEM"].ToString().ToUpper();
                        nm_item_partner = row["DESC"].ToString().ToUpper();
                        cd_uniq_partner = row["UNIQ"].ToString().ToUpper();
                        qt = row["QT"].ToString();

                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["CD_COMPANY"] = cd_companyHGS;
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO_PREREG"] = fileHGSNo;
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO_LINE"] = no_line;
                        dtItem.Rows[dtItem.Rows.Count - 1]["CD_PARTNER"] = partnercode;
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO_IMO"] = imonumber;
                        dtItem.Rows[dtItem.Rows.Count - 1]["SHIPSERV_TNID"] = tnid;
                        dtItem.Rows[dtItem.Rows.Count - 1]["NM_VESSEL"] = vesselName;
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO_REF"] = referencenumber;
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO_REQREF"] = "";
                        dtItem.Rows[dtItem.Rows.Count - 1]["NM_SUBJECT"] = nm_subject;
                        dtItem.Rows[dtItem.Rows.Count - 1]["CD_ITEM_PARTNER"] = cd_item_partner;
                        dtItem.Rows[dtItem.Rows.Count - 1]["NM_ITEM_PARTNER"] = nm_item_partner;
                        dtItem.Rows[dtItem.Rows.Count - 1]["CD_UNIQ_PARTNER"] = cd_uniq_partner;
                        if (unit.Contains(",")) unit = "";
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = unit;
                        if (string.IsNullOrEmpty(qt)) qt = "0";
                        if (qt.Contains(",")) qt = qt.Replace(",", "").Trim();
                        dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = qt;
                        dtItem.Rows[dtItem.Rows.Count - 1]["SORTED_BY"] = "HGS";
                        dtItem.Rows[dtItem.Rows.Count - 1]["COMMENT"] = buyer;
                        dtItem.Rows[dtItem.Rows.Count - 1]["CONTACT"] = contact;
                        dtItem.Rows[dtItem.Rows.Count - 1]["NM_FILE"] = filenameStr;
                        dtItem.Rows[dtItem.Rows.Count - 1]["DXCODE_SUBJ"] = Util.GetDxCode(nm_subject.ToUpper());
                        dtItem.Rows[dtItem.Rows.Count - 1]["DXCODE_ITEM"] = Util.GetDxCode(cd_item_partner.ToUpper()) + "‡" + nm_item_partner.ToUpper();

                        dtItemVendor.Rows.Add();
                        dtItemVendor.Rows[dtItem.Rows.Count - 1]["CD_COMPANY"] = cd_companyHGS;
                        dtItemVendor.Rows[dtItem.Rows.Count - 1]["NO_PREREG"] = fileHGSNo;
                        dtItemVendor.Rows[dtItem.Rows.Count - 1]["NO_LINE"] = no_line;
                        dtItemVendor.Rows[dtItem.Rows.Count - 1]["DXVENDOR_CODE"] = partnercode;

                        no_line += 1;
                    }

                    dtItemHead.Rows.Add();
                    dtItemHead.Rows[0]["CD_COMPANY"] = cd_companyHGS;
                    dtItemHead.Rows[0]["NO_PREREG"] = fileHGSNo;
                    dtItemHead.Rows[0]["NO_FILE"] = fileHGSNo;
                    dtItemHead.Rows[0]["NO_EMP"] = no_empHGS;
                    dtItemHead.Rows[0]["CD_PARTNER"] = partnercode;
                    dtItemHead.Rows[0]["NO_IMO"] = imonumber;

                    string xml = Util.GetTO_Xml(dtItem);
                    string xmlVendor = Util.GetTO_Xml(dtItemVendor);
                    string xmlHead = Util.GetTO_Xml(dtItemHead);

                    SQL.ExecuteNonQuery("SP_CZ_SA_QTN_PREREG", SQLDebug.Print, xml);
                    SQL.ExecuteNonQuery("SP_CZ_SA_QTN_PREREG_VENDOR", SQLDebug.Print, xmlVendor);
                    SQL.ExecuteNonQuery("SP_CZ_SA_QTN_PREREG_HEAD", SQLDebug.Print, xmlHead);

                    RPA rpa = new RPA() { Process = "INQ", FileNumber = fileHGSNo, PartnerCode = "11823" };
                    rpa.AddQueue();
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn갱신_Click(object sender, EventArgs e)
        {
            try
            {
                this._flexD.Binding = this._biz.입력지원현황(new object[] { this.ctx회사.CodeValue,
                                                                            Global.MainFrame.LoginInfo.UserID });
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn닫기_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
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
            DataTable dt;
            string filter;

            try
            {
                dt = null;
                filter = "NO_KEY = '" + this._flexH["NO_KEY"].ToString() +"'";

                if (this._flexH.DetailQueryNeed == true)
                {
                    dt = this._biz.SearchDetail(new object[] { this.ctx회사.CodeValue,
                                                               this.파일유형,
                                                               this._flexH["NO_KEY"].ToString(),
															   (this.chk첨부파일표시.Checked == true ? "Y" : "N") });
                }

                this._flexL.BindingAdd(dt, filter);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flex_CheckHeaderClick(object sender, EventArgs e)
        {
            try
            {
                FlexGrid flex = sender as FlexGrid;

                switch (flex.Name)
                {
                    case "_flexH":  //상단 그리드 Header Click 이벤트

                        if (!this._flexH.HasNormalRow) return;

                        //데이타 테이블의 체크값을 변경해주어도 화면에 보이는 값을 변경시키지 못하므로 화면의 값도 같이 변경시켜줌
                        this._flexH.Row = 1; // 맨처음row에 자동위치하도록 해야 틀어지지 않는다.

                        if (!this._flexH.HasNormalRow || !this._flexL.HasNormalRow) return;

                        for (int h = 0; h < this._flexH.Rows.Count - 1; h++)
                        {
                            this._flexH.Row = h + 1; //Row가 순차적으로 변경되면서 RowChange() 이벤트를 자동 실행하게 유도한다.

                            for (int i = this._flexL.Rows.Fixed; i < this._flexL.Rows.Count; i++)
                            {
                                if (this._flexL.RowState(i) == DataRowState.Deleted) continue;

                                this._flexL[i, "S"] = this._flexH["S"].ToString();
                            }
                        }
                        break;

                    case "_flexL":  //하단 그리드 Header Click 이벤트

                        if (!this._flexL.HasNormalRow) return;

                        this._flexH["S"] = D.GetString(this._flexL["S"]);

                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flexH_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            FlexGrid grid;
            string colname, oldValue, newValue, message;

            try
            {
                grid = sender as FlexGrid;
                if (grid == null) return;

                colname = grid.Cols[e.Col].Name;
                oldValue = D.GetString(grid.GetData(e.Row, e.Col));
                newValue = grid.EditData;
                
                if (colname == "NO_KEY")
                {
                    message = this.CheckFileNo(newValue);

                    if (!string.IsNullOrEmpty(message))
                    {
                        Global.MainFrame.ShowMessage(message);
                        grid["NO_KEY"] = oldValue;
                        e.Cancel = true;
                    }
                    else
                    {
                        this.txt파일번호.Text = newValue;

                        foreach (DataRow dr in this._flexL.DataTable.Select("NO_KEY = '" + oldValue + "'"))
                            dr["NO_KEY"] = newValue;

                        this._flexL.RowFilter = "NO_KEY = '" + newValue + "'";
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flexH_AfterEdit(object sender, RowColEventArgs e)
        {
            try
            {
                switch (this._flexH.Cols[e.Col].Name)
                {
                    case "S":
                        if (this._flexH["S"].ToString() == "Y") //클릭하는 순간은 N이므로
                        {
                            for (int i = this._flexL.Rows.Fixed; i < this._flexL.Rows.Count; i++)
                                this._flexL.SetCellCheck(i, 1, CheckEnum.Checked);
                        }
                        else
                        {
                            for (int i = this._flexL.Rows.Fixed; i < this._flexL.Rows.Count; i++)
                                this._flexL.SetCellCheck(i, 1, CheckEnum.Unchecked);
                        }
                        break;
                    case "DC_RMK":
                        this.txt비고.Text = D.GetString(this._flexH["DC_RMK"]);
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flexL_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            FlexGrid grid;

            try
            {
                grid = sender as FlexGrid;
                if (grid == null) return;

                if (grid.Cols[e.Col].Name == "S")
                {
                    grid["S"] = e.Checkbox == CheckEnum.Checked ? "Y" : "N";

                    if (grid.Name == this._flexL.Name)
                    {
                        DataRow[] drArr = grid.DataView.ToTable().Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                        if (drArr.Length != 0)
                            this._flexH.SetCellCheck(this._flexH.Row, this._flexH.Cols["S"].Index, CheckEnum.Checked);
                        else
                            this._flexH.SetCellCheck(this._flexH.Row, this._flexH.Cols["S"].Index, CheckEnum.Unchecked);
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flexL_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                if (this.rdo매입견적.Checked != true && this.rdo매입확정.Checked != true)
                {
                    e.Cancel = true;
                    return;
                }

                if (string.IsNullOrEmpty(D.GetString(this._flexL["NO_KEY"])))
                {
                    Global.MainFrame.ShowMessage("CZ_@ 이(가) 지정되어 있지 않습니다.", Global.MainFrame.DD("파일번호"));
                    e.Cancel = true;
                    return;
                }

                if (this.rdo매입견적.Checked == true)
                    e.Parameter.P00_CHILD_MODE = "001";
                else if (this.rdo매입확정.Checked == true)
                    e.Parameter.P00_CHILD_MODE = "002";

                e.Parameter.P61_CODE1 = D.GetString(this._flexL["NO_KEY"]);
                e.Parameter.P62_CODE2 = this.ctx회사.CodeValue;
                e.Parameter.UserParams = "매입처 도움창;H_CZ_SA_PARTNER_SUB;";
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
                if (Global.MainFrame.LoginInfo.CompanyCode != this.ctx회사.CodeValue) return;

                if (!string.IsNullOrEmpty(D.GetString(this._flexL["FULL_PATH"])))
                {
                    Process.Start(D.GetString(this._flexL["FULL_PATH"]));
                }
                else if (!string.IsNullOrEmpty(D.GetString(this._flexL["NM_FILE"])))
                {
                    FileMgr.Download_WF(Global.MainFrame.LoginInfo.CompanyCode, D.GetString(this._flexL["NO_KEY"]), this._flexL["NM_FILE_REAL"].ToString(), true);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flexD_AfterEdit(object sender, RowColEventArgs e)
        {
            FlexGrid flexGrid;
            DataRow[] dataRowArray;
            string query;

            try
            {
                flexGrid = ((FlexGrid)sender);

                if (flexGrid.HasNormalRow == false) return;

                if (flexGrid.Cols[e.Col].Name == "YN_DEFAULT")
                {
                    dataRowArray = this._flexD.DataTable.Select("YN_DEFAULT = 'Y'");

                    if (dataRowArray.Length > 0)
                    {
                        query = "UPDATE CZ_SA_USER" + Environment.NewLine +
                                "SET ID_TYPIST_DEF = '" + D.GetString(dataRowArray[0]["ID_USER"]) + "'" + Environment.NewLine +
                                "WHERE CD_COMPANY = '" + this.ctx회사.CodeValue + "'" +
                                "AND ID_USER = '" + Global.MainFrame.LoginInfo.UserID + "'";

                        Global.MainFrame.ExecuteScalar(query);

                        this.defaultValue["ID_TYPIST_DEF"] = D.GetString(dataRowArray[0]["ID_USER"]);
                    }
                }
                else if (flexGrid.Cols[e.Col].Name == "YN_AUTO")
                {
                    dataRowArray = this._flexD.DataTable.Select("YN_AUTO = 'Y'", "ID_USER ASC");
                    string typistAuto = string.Empty;

                    foreach (DataRow dr in dataRowArray)
                    {
                        typistAuto += string.Format("{0}|", D.GetString(dr["ID_USER"]));
                    }

                    query = "UPDATE CZ_SA_USER" + Environment.NewLine +
                            "SET ID_TYPIST_AUTO = '" + typistAuto.TrimEnd('|') + "'" + Environment.NewLine +
                            "WHERE CD_COMPANY = '" + this.ctx회사.CodeValue + "'" +
                            "AND ID_USER = '" + Global.MainFrame.LoginInfo.UserID + "'";

                    Global.MainFrame.ExecuteScalar(query);
                }
                else if (flexGrid.Cols[e.Col].Name == "YN_MAPS")
                {
                    dataRowArray = this._flexD.DataTable.Select("YN_MAPS = 'Y'", "ID_USER ASC");
                    string typistMaps = string.Empty;

                    foreach (DataRow dr in dataRowArray)
                    {
                        typistMaps += string.Format("{0}|", D.GetString(dr["ID_USER"]));
                    }

                    query = "UPDATE CZ_SA_USER" + Environment.NewLine +
                            "SET ID_TYPIST_MAPS = '" + typistMaps.TrimEnd('|') + "'" + Environment.NewLine +
                            "WHERE CD_COMPANY = '" + this.ctx회사.CodeValue + "'" +
                            "AND ID_USER = '" + Global.MainFrame.LoginInfo.UserID + "'";

                    Global.MainFrame.ExecuteScalar(query);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flexD_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            FlexGrid grid;

            try
            {
                grid = sender as FlexGrid;
                if (grid == null) return;
                if (grid.Cols[e.Col].Name != "YN_DEFAULT") return;

                if (e.Checkbox == CheckEnum.Unchecked)
                    e.Cancel = true;
                else
                {
                    for (int row = this._flexD.Rows.Fixed; row < this._flexD.Rows.Count; row++)
                    {
                        if (row != e.Row)
                            this._flexD.SetCellCheck(row, e.Col, CheckEnum.Unchecked);
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region 기타 이벤트
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape) return false;

            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnClosed(EventArgs e)
        {
            try
            {
                base.OnClosed(e);

                if (this.rdo자동.Checked == true)
                    Settings.Default.입력담당할당 = this.rdo자동.Text;
                else if (this.rdo현대.Checked == true)
                    Settings.Default.입력담당할당 = this.rdo현대.Text;
                else
                    Settings.Default.입력담당할당 = this.rdo수동.Text;

                this.임시파일제거();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
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

        private void txt파일번호S_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    this.초기화();
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void txt파일번호_Leave(object sender, EventArgs e)
        {
            string message;
            
            try
            {
                if (this.rdo고객문의서.Checked == true || string.IsNullOrEmpty(this.txt파일번호.Text)) return;

                message = this.CheckFileNo(this.txt파일번호.Text);

                if (!string.IsNullOrEmpty(message))
                {
                    Global.MainFrame.ShowMessage(message);
                    this.txt파일번호.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void Control_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void Control_DragDrop(object sender, DragEventArgs e)
        {
            string[] paths, fileNames;
            
            try
            {
                paths = (string[])e.Data.GetData(DataFormats.FileDrop, false);

                this.SetEnableControl(false);

                foreach (string path in paths)
                {
                    if (Directory.Exists(path) == true)
                    {
                        fileNames = Directory.GetFiles(path, "*.*");

                        foreach (string fileName in fileNames)
                        {
                            this.AddData(fileName);
                        }
                    }
                    else if (File.Exists(path) == true)
                    {
                        this.AddData(path);
                    }
                }

                this.SetEnableControl(true);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void Control_Click(object sender, EventArgs e)
        {
            Control control;
            DataTable dt;

            try
            {
                control = ((Control)sender);

                this.txt파일번호S.Text = EngHanConverter.KorToEng(this.txt파일번호S.Text);
                this.txt파일번호S.Text = this.txt파일번호S.Text.ToUpper();

                this.txt파일번호.Text = string.Empty;
                this.cbo영업유형.SelectedItem = null;
                this.cbo영업담당.SelectedItem = null;
                this.cbo입력담당.SelectedItem = null;
                this.cbo구매담당.SelectedItem = null;
                this.cbo영업물류.SelectedItem = null;
                this.txt비고.Text = string.Empty;

                if (control.Name == this.rdo고객문의서.Name)
                {
                    this.txt파일번호.ReadOnly = true;
                    this.cbo영업유형.Enabled = true;
                    this.cbo영업담당.Enabled = true;

                    if (this.defaultValue["TP_BIZ"].ToString() == "TP" && Global.MainFrame.LoginInfo.UserID != "S-316")
                        this.cbo입력담당.Enabled = false;
                    else
                        this.cbo입력담당.Enabled = true;
                    
                    this.btn조회.Enabled = true;

                    this._flexH.Cols["NO_KEY"].AllowEditing = false;
                    this._flexL.Cols["YN_PARSING"].Visible = true;
                }
                else
                {
                    this.txt파일번호.ReadOnly = false;
                    this.cbo영업유형.Enabled = false;
                    this.cbo영업담당.Enabled = false;
                    this.cbo입력담당.Enabled = false;
                    this.btn조회.Enabled = false;

                    this._flexH.Cols["NO_KEY"].AllowEditing = true;
                    this._flexL.Cols["YN_PARSING"].Visible = false;
                }

                if (control.Name == this.rdo고객발주서.Name)
                {
                    this.cbo구매담당.Enabled = true;

                    if (Global.MainFrame.LoginInfo.CompanyCode != "K100")
                        this.cbo영업물류.Enabled = true;
                    else
                        this.cbo영업물류.Enabled = false;
                }
                else
                {
                    this.cbo구매담당.Enabled = false;
                    this.cbo영업물류.Enabled = false;
                }

                if (control.Name == this.rdo고객문의서.Name)
                {
                    this._flexH.Cols["TP_SALES"].Visible = true;
                    this._flexH.Cols["ID_SALES"].Visible = true;
                    this._flexH.Cols["ID_TYPIST"].Visible = true;
                    this._flexH.Cols["ID_PUR"].Visible = false;
                    this._flexH.Cols["ID_LOG"].Visible = false;
                }
                else if (control.Name == this.rdo고객발주서.Name)
                {
                    this._flexH.Cols["TP_SALES"].Visible = false;
                    this._flexH.Cols["ID_SALES"].Visible = false;
                    this._flexH.Cols["ID_TYPIST"].Visible = false;
                    this._flexH.Cols["ID_PUR"].Visible = true;
                    this._flexH.Cols["ID_LOG"].Visible = true;
                }
                else
                {
                    this._flexH.Cols["TP_SALES"].Visible = false;
                    this._flexH.Cols["ID_SALES"].Visible = false;
                    this._flexH.Cols["ID_TYPIST"].Visible = false;
                    this._flexH.Cols["ID_PUR"].Visible = false;
                    this._flexH.Cols["ID_LOG"].Visible = false;
                }

                if (control.Name == this.rdo매입견적.Name || control.Name == this.rdo매입확정.Name)
                    this._flexL.Cols["NM_SUPPLIER"].Visible = true;
                else
                    this._flexL.Cols["NM_SUPPLIER"].Visible = false;

                dt = this._biz.Search(new object[] { this.ctx회사.CodeValue,
                                                     this.파일유형,
                                                     this.txt파일번호S.Text });

                this._flexH.Binding = dt;

                this.SetEnableControl(true);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region 기타 메소드
        private void SetComboData()
        {
            DataSet ds;

            try
            {
                ds = this._biz.기본정보(new object[] { this.ctx회사.CodeValue,
                                                       Global.MainFrame.LoginInfo.UserID });

                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    this.defaultValue = ds.Tables[0].Rows[0];

                    this.cbo영업유형적용.DataSource = this.AddEmptyRow(ds.Tables[1].Copy());
                    this.cbo영업유형적용.ValueMember = "CODE";
                    this.cbo영업유형적용.DisplayMember = "NAME";

                    this.cbo영업유형.DataSource = this.AddEmptyRow(ds.Tables[1].Copy());
                    this.cbo영업유형.ValueMember = "CODE";
                    this.cbo영업유형.DisplayMember = "NAME";

                    this.cbo영업담당.DataSource = this.AddEmptyRow(ds.Tables[2]);
                    this.cbo영업담당.ValueMember = "CODE";
                    this.cbo영업담당.DisplayMember = "NAME";

                    this.cbo구매담당.DataSource = this.AddEmptyRow(ds.Tables[3]);
                    this.cbo구매담당.ValueMember = "CODE";
                    this.cbo구매담당.DisplayMember = "NAME";

                    this.cbo영업물류.DataSource = this.AddEmptyRow(ds.Tables[4]);
                    this.cbo영업물류.ValueMember = "CODE";
                    this.cbo영업물류.DisplayMember = "NAME";

                    this.cbo입력담당.DataSource = this.AddEmptyRow(this._biz.입력지원코드(new object[] { this.ctx회사.CodeValue,
                                                                                                         Global.MainFrame.LoginInfo.UserID }));
                    this.cbo입력담당.ValueMember = "ID_USER";
                    this.cbo입력담당.DisplayMember = "NM_USER";

                    if (this.defaultValue["TP_BIZ"].ToString() == "TP" && Global.MainFrame.LoginInfo.UserID != "S-316")
                        this.cbo입력담당.Enabled = false;
                    else
                        this.cbo입력담당.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private DataTable AddEmptyRow(DataTable dt)
        {
            DataRow emptyRow;

            try
            {
                if (dt == null || dt.Rows.Count == 0) return dt;

                emptyRow = dt.NewRow();
                dt.Rows.InsertAt(emptyRow, 0);

                return dt;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return dt;
        }

        private void SetEnableControl(bool isEnabled)
        {
            try
            {
                if (isEnabled == true)
                {
                    this.btn초기화.Enabled = true;
                    this.btn파일추가.Enabled = true;
                    this.btn파일삭제.Enabled = true;
                    this.btn다운로드.Enabled = true;
                    this.btn변경사항저장.Enabled = true;
                }
                else
                {
                    this.btn초기화.Enabled = false;
                    this.btn파일추가.Enabled = false;
                    this.btn파일삭제.Enabled = false;
                    this.btn다운로드.Enabled = false;
                    this.btn변경사항저장.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void AddData(string 파일경로)
        {
            FileInfo 파일정보;
            string 첨부파일이름, 파일번호, 매입처코드;
            string[] 첨부파일이름배열;
            
            try
            {
                if (this._flexH.DataTable == null) return;

                #region 선사 웹에서 파일 다운로드
                if (Global.MainFrame.LoginInfo.EmployeeNo.In("S-343"))
                {
					MsgControl.ShowMsg("선사웹에 접속중입니다.\n잠시만 기다려 주세요.");

					MailSorter ms = new MailSorter(파일경로);

                    if (ms.Sort())
                    {
                        AutoWeb autoWeb = new AutoWeb();
                        autoWeb.OutlookFile = 파일경로;
                        파일경로 = autoWeb.DownloadInquiryWithMessage(false);

                        if (string.IsNullOrEmpty(파일경로))
                        {
                            Global.MainFrame.ShowMessage(autoWeb.ErrorMessage);
                            return;
                        }
                    }

					MsgControl.CloseMsg();
				}
                #endregion

                #region 파일이름분해
                파일정보 = new FileInfo(파일경로);

                첨부파일이름 = 파일정보.Name.Replace(파일정보.Extension, string.Empty);
                첨부파일이름배열 = 첨부파일이름.Split('_');

                if (!string.IsNullOrEmpty(this.txt파일번호S.Text))
                    파일번호 = this.txt파일번호S.Text.ToUpper();
                else
                {
                    if (Global.MainFrame.LoginInfo.CompanyCode == "K200")
                        파일번호 = Regex.Replace(첨부파일이름배열[0], @"[^a-zA-Z0-9\-]", string.Empty, RegexOptions.Singleline).ToUpper();
                    else
                        파일번호 = Regex.Replace(첨부파일이름배열[0], @"[^a-zA-Z0-9]", string.Empty, RegexOptions.Singleline).ToUpper();
                }

                매입처코드 = (첨부파일이름배열.Length > 1 ? Regex.Replace(첨부파일이름배열[1], @"[^0-9]", string.Empty, RegexOptions.Singleline) : string.Empty);
                #endregion

                if (!string.IsNullOrEmpty(파일번호))
                {
					#region 파일번호가 화면상에 존재할 경우 처리
					if (this._flexH.DataTable.AsEnumerable().Where(x => x["NO_KEY"].ToString() == 파일번호 && x.RowState != DataRowState.Added).Count() > 0)
					{
						this.AddLineData(파일번호, 매입처코드, 파일경로);
						return;
					}
					#endregion

					#region 파일번호가 데이터베이스상에 존재할 경우 처리
					DataTable dtH = this._biz.Search(new object[] { this.ctx회사.CodeValue,
                                                                    this.파일유형,
                                                                    파일번호 });
                    if (dtH.Rows.Count > 0)
                    {
                        this._flexH.Redraw = false;
                        this._flexH.DataTable.ImportRow(dtH.Rows[0]);
                        this._flexH.Redraw = true;
                        this._flexH.Row = this._flexH.Rows.Count - 1;
                        
                        this.AddLineData(파일번호, 매입처코드, 파일경로);
                        return;
                    }
                    #endregion
                }

                #region 파일번호가 없을 경우 처리
                this.AddLineData(this.AddHeaderData(파일번호, 첨부파일이름), 매입처코드, 파일경로);
                #endregion
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
			finally
			{
				MsgControl.CloseMsg();
			}
        }

        private string AddHeaderData(string 파일번호, string 첨부파일이름)
        {
            DataRow dataRow;
            string userId;

            try
            {
                this._flexH.Redraw = false;
                dataRow = this._flexH.DataTable.Rows.Add();
                
                if (this.rdo고객문의서.Checked == true)
                {
                    #region 고객문의서
                    dataRow["NO_KEY"] = D.GetString(this._flexH.Rows.Count - 1);
                    dataRow["TP_SALES"] = (!string.IsNullOrEmpty(D.GetString(this.cbo영업유형적용.SelectedValue)) ? D.GetString(this.cbo영업유형적용.SelectedValue) : D.GetString(this.defaultValue["TP_SALES_DEF"]));
                    dataRow["ID_SALES"] = D.GetString(this.defaultValue["ID_SALES_DEF"]);
                    dataRow["DC_RMK"] = 첨부파일이름;

                    if (Global.MainFrame.LoginInfo.CompanyCode == "K100" && 
                        (dataRow["TP_SALES"].ToString() == "FB" || dataRow["TP_SALES"].ToString() == "DS") && 
                        첨부파일이름.StartsWith("현대웹"))
                        dataRow["ID_TYPIST"] = this._biz.입력담당자자동지정(this.ctx회사.CodeValue, this._flexH.DataTable, true);
                    else if (this.rdo자동.Checked == true)
                        dataRow["ID_TYPIST"] = this._biz.입력담당자자동지정(this.ctx회사.CodeValue, this._flexH.DataTable, false);
                    else if (this.rdo현대.Checked == true)
                        dataRow["ID_TYPIST"] = this._biz.입력담당자자동지정(this.ctx회사.CodeValue, this._flexH.DataTable, true);
                    else
                    {
                        userId = D.GetString(this.defaultValue["ID_TYPIST_DEF"]);
                        if (this._flexD.DataTable.Select("ID_USER = '" + userId + "'").Length > 0)
                            dataRow["ID_TYPIST"] = userId;
                    }
                    #endregion
                }
                else
                {
                    #region 나머지
                    if (string.IsNullOrEmpty(this.CheckFileNo(파일번호)))
                        dataRow["NO_KEY"] = 파일번호;
                    else
                        dataRow["NO_KEY"] = D.GetString(this._flexH.Rows.Count - 1);
                    
                    if (this.rdo고객발주서.Checked == true)
                    {
                        dataRow["ID_PUR"] = D.GetString(this.defaultValue["ID_PUR_DEF"]);
                        dataRow["ID_LOG"] = D.GetString(this.defaultValue["ID_LOG_DEF"]);
                    }
                    #endregion
                }

                this._flexH.Redraw = true;
                this._flexH.IsDataChanged = true;
                this._flexH.Row = this._flexH.Rows.Count - 1;

                return dataRow["NO_KEY"].ToString();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return string.Empty;
        }

        private void AddLineData(string 파일번호, string 매입처코드, string 파일경로)
        {
			FileInfo 파일정보;
			List<string> imageList;
			string[] separator, temp;
			string image1, fileName, localpath;

            try
            {
				파일정보 = new FileInfo(파일경로);

				this._flexL.Redraw = false;

				this.AddDataRow(파일번호, 매입처코드, 파일경로, 파일정보, false);

				if (파일정보.Extension == ".msg")
				{
					#region MSG
					MapiMessage msg = MapiMessage.FromFile(파일경로);

					#region 본문 이미지 제거
					separator = new string[] { "cid:" };
					temp = msg.BodyHtml.Split(separator, StringSplitOptions.None);

					temp[0] = string.Empty;
					imageList = new List<string>();

					foreach (string image in temp)
					{
						if (string.IsNullOrEmpty(image))
							continue;

						image1 = image.Split('.')[0];

						if (!imageList.Contains(image1))
							imageList.Add(image1);
					}
					#endregion

					string filePath = Path.Combine(Application.StartupPath, this.임시폴더) + "\\";
					FileMgr.CreateDirectory(filePath);

					foreach (MapiAttachment item in msg.Attachments)
                    {
                        if (string.IsNullOrEmpty(item.LongFileName))
                            continue;

                        fileName = Regex.Replace(item.LongFileName, @"[^a-zA-Z0-9가-힣ㄱ-ㅎㅏ-ㅣ\[\]_ .]", string.Empty);
                        FileInfo fileInfo = new FileInfo(fileName);

                        if (fileInfo.Extension.Length == 0)
                            continue;

                        if (imageList.Contains(fileInfo.Name.Replace(fileInfo.Extension, string.Empty)))
                            continue;

                        fileName = FileMgr.GetUniqueFileName(Path.Combine(Application.StartupPath, this.임시폴더) + "\\" + fileName);
						localpath = Path.Combine(Application.StartupPath, this.임시폴더) + "\\" + fileName;
						item.Save(localpath);

						this.AddDataRow(파일번호, 매입처코드, localpath, new FileInfo(localpath), true);
					}
					#endregion
				}

				this._flexL.Redraw = true;
				this._flexL.IsDataChanged = true;
				this._flexL.Row = this._flexL.Rows.Count - 1;
			}
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

		private void AddDataRow(string 파일번호, string 매입처코드, string 파일경로, FileInfo 파일정보, bool 첨부여부)
		{
			DataRow dataRow;
			DataTable dataTable;
			DataRow[] dataRowArray;
			string query;

			try
			{
				dataRow = this._flexL.DataTable.Rows.Add();

				dataRow["NO_KEY"] = 파일번호;
				dataRow["FULL_PATH"] = 파일정보.FullName;
				dataRow["NM_FILE"] = 파일정보.Name;
				dataRow["NM_FILE_REAL"] = 파일정보.Name;

				if (this.rdo고객문의서.Checked == true && 첨부여부 == false)
				{
					#region 고객문의서
					try
					{
						InquiryParser parser = new InquiryParser(파일경로);

						if (parser.Parse(false) == true)
						{
							dataRow["YN_PARSING"] = "Y";
							this._flexL.SetCellCheck((this._flexL.Rows.Count - 1), this._flexL.Cols["YN_PARSING"].Index, CheckEnum.Checked);
						}
						else
						{
							dataRow["YN_PARSING"] = "N";
							this._flexL.SetCellCheck((this._flexL.Rows.Count - 1), this._flexL.Cols["YN_PARSING"].Index, CheckEnum.Unchecked);
						}
					}
					catch
					{
						dataRow["YN_PARSING"] = "N";
						this._flexL.SetCellCheck((this._flexL.Rows.Count - 1), this._flexL.Cols["YN_PARSING"].Index, CheckEnum.Unchecked);
					}
                    #endregion
                }
				else if (this.rdo매입견적.Checked == true)
				{
					#region 매입견적
					query = @"SELECT DISTINCT PQ.CD_PARTNER, 
                                              MP.LN_PARTNER 
                              FROM CZ_PU_QTNH PQ WITH(NOLOCK)
                              LEFT JOIN MA_PARTNER MP WITH(NOLOCK) ON MP.CD_COMPANY = PQ.CD_COMPANY AND MP.CD_PARTNER = PQ.CD_PARTNER
                              WHERE PQ.CD_COMPANY = '" + this.ctx회사.CodeValue + "'" + Environment.NewLine +
							 "AND PQ.NO_FILE = '" + 파일번호 + "'";

					dataTable = Global.MainFrame.FillDataTable(query);

					if (dataTable.Rows.Count > 0)
					{
						dataRowArray = dataTable.Select("CD_PARTNER = '" + 매입처코드 + "'");

						if (dataRowArray.Length > 0)
						{
							dataRow["CD_SUPPLIER"] = dataRowArray[0]["CD_PARTNER"];
							dataRow["NM_SUPPLIER"] = dataRowArray[0]["LN_PARTNER"];
						}
						else if (dataTable.Rows.Count == 1)
						{
							dataRow["CD_SUPPLIER"] = dataTable.Rows[0]["CD_PARTNER"];
							dataRow["NM_SUPPLIER"] = dataTable.Rows[0]["LN_PARTNER"];
						}
					}
					#endregion
				}
				else if (this.rdo매입확정.Checked == true)
				{
					#region 매입확정
					query = @"SELECT DISTINCT PH.CD_PARTNER,
                                              MP.LN_PARTNER 
                              FROM PU_POH PH WITH(NOLOCK)
                              LEFT JOIN MA_PARTNER MP WITH(NOLOCK) ON MP.CD_COMPANY = PH.CD_COMPANY AND MP.CD_PARTNER = PH.CD_PARTNER
                              WHERE PH.CD_COMPANY = '" + this.ctx회사.CodeValue + "'" + Environment.NewLine +
							 "AND PH.CD_PJT = '" + 파일번호 + "'";

					dataTable = Global.MainFrame.FillDataTable(query);

					if (dataTable.Rows.Count > 0)
					{
						dataRowArray = dataTable.Select("CD_PARTNER = '" + 매입처코드 + "'");

						if (dataRowArray.Length > 0)
						{
							dataRow["CD_SUPPLIER"] = dataRowArray[0]["CD_PARTNER"];
							dataRow["NM_SUPPLIER"] = dataRowArray[0]["LN_PARTNER"];
						}
						else if (dataTable.Rows.Count == 1)
						{
							dataRow["CD_SUPPLIER"] = dataTable.Rows[0]["CD_PARTNER"];
							dataRow["NM_SUPPLIER"] = dataTable.Rows[0]["LN_PARTNER"];
						}
					}
					#endregion
				}

				if (첨부여부 == true)
				{
					dataRow["YN_INCLUDED"] = "Y";
					this._flexL.SetCellCheck((this._flexL.Rows.Count - 1), this._flexL.Cols["YN_INCLUDED"].Index, CheckEnum.Checked);
				}
				else
				{
					dataRow["YN_INCLUDED"] = "N";
					this._flexL.SetCellCheck((this._flexL.Rows.Count - 1), this._flexL.Cols["YN_INCLUDED"].Index, CheckEnum.Unchecked);
				}

				dataRow["FILE_STATE"] = D.GetString(P_CZ_SA_INQ_REG.상태.업로드대기);
				dataRow["DTS_INSERT"] = Global.MainFrame.GetStringDetailToday;
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

        private string CheckFileNo(string fileNo)
        {
            DataTable dt;

            try
            {
                if (this.rdo매입견적.Checked == true)
                {
                    dt = Global.MainFrame.FillDataTable(@"SELECT * 
                                                          FROM CZ_PU_QTNH WITH(NOLOCK) 
                                                          WHERE CD_COMPANY = '" + this.ctx회사.CodeValue + "'" + Environment.NewLine + 
                                                         "AND NO_FILE = '" + fileNo + "'");
                    if (dt.Rows.Count == 0)
                        return "견적건이 존재하지 않습니다.";
                }
                else if (this.rdo매입확정.Checked == true)
                {
                    dt = Global.MainFrame.FillDataTable(@"SELECT * 
                                                          FROM PU_POH WITH(NOLOCK)
                                                          WHERE CD_COMPANY = '" + this.ctx회사.CodeValue + "'" + Environment.NewLine +
                                                         "AND CD_PJT = '" + fileNo + "'");
                    if (dt.Rows.Count == 0)
                        return "발주건이 존재하지 않습니다.";
                }
                else if (this.rdo고객문의서.Checked == false)
                {
                    dt = Global.MainFrame.FillDataTable(@"SELECT * 
                                                          FROM CZ_MA_WORKFLOWH WITH(NOLOCK) 
                                                          WHERE CD_COMPANY = '" + this.ctx회사.CodeValue + "'" + Environment.NewLine +
                                                         "AND TP_STEP = '01' AND NO_KEY = '" + fileNo + "'");
                    if (dt.Rows.Count == 0)
                        return "고객문의서가 존재하지 않습니다.";
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return string.Empty;
        }

        private void 초기화()
        {
            try
            {
                if (this.rdo고객문의서.Checked == true)
                    this.Control_Click(this.rdo고객문의서, null);
                else if (this.rdo매입견적.Checked == true)
                    this.Control_Click(this.rdo매입견적, null);
                else if (this.rdo고객발주서.Checked == true)
                    this.Control_Click(this.rdo고객발주서, null);
                else if (this.rdo매입확정.Checked == true)
                    this.Control_Click(this.rdo매입확정, null);
                else if (this.rdo납품지침.Checked == true)
                    this.Control_Click(this.rdo납품지침, null);
                else if (this.rdo클레임.Checked == true)
                    this.Control_Click(this.rdo클레임, null);
                else if (this.rdo관련도면.Checked == true)
                    this.Control_Click(this.rdo관련도면, null);
                else
                    this.Control_Click(this.rdo기타, null);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private bool IsFileLocked(string fileName)
        {
            FileStream stream = null;

            try
            {
                stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            }
            catch (Exception ex)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                Global.MainFrame.MsgEnd(ex);
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            //file is not locked
            return false;
        }
        #endregion
    }
}
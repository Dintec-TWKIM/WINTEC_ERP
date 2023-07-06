using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.Utils;
using DX;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace cz
{
	public partial class P_CZ_SA_GIM_REG : PageBase
    {
        #region ★ 멤버필드
        private P_CZ_SA_GIM_REG_BIZ _biz = new P_CZ_SA_GIM_REG_BIZ();

        string _수불일자 = string.Empty;
        string _매출처코드 = string.Empty;
        string _매출처명 = string.Empty;
        string _사번 = string.Empty;
        string _이름 = string.Empty;
        string _의뢰번호 = string.Empty;

        bool b수량권한 = true;
        bool b단가권한 = true;
        bool b금액권한 = true;

        string 시리얼여부 = string.Empty;
        Image fileIcon = null;
        #endregion

        #region ★ 초기화
        public P_CZ_SA_GIM_REG()
        {
            StartUp.Certify(this);
            InitializeComponent();
        }

        public P_CZ_SA_GIM_REG(string 의뢰번호)
        {
            StartUp.Certify(this);
            InitializeComponent();

            this._의뢰번호 = 의뢰번호;
        }

        public P_CZ_SA_GIM_REG(string 수불일자, string 매출처코드, string 매출처명, string 사번, string 이름)
        {
            StartUp.Certify(this);
            InitializeComponent();
            _수불일자 = 수불일자;
            _매출처코드 = 매출처코드;
            _매출처명 = 매출처명;
            _사번 = 사번;
            _이름 = 이름;
        }

        public P_CZ_SA_GIM_REG(string callPage, string noIo, string dtIo, string fgCls)
            : this()
        {
            StartUp.Certify(this);
            this.txt수불번호.Text = noIo;
            _수불일자 = dtIo;
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.AllowDrop = true;

            DataTable dt = BASIC.MFG_AUTH("P_SA_GIM_REG");
            if (dt.Rows.Count > 0)
            {
                b수량권한 = (dt.Rows[0]["YN_QT"].ToString() == "Y") ? false : true;
                b단가권한 = (dt.Rows[0]["YN_UM"].ToString() == "Y") ? false : true;
                b금액권한 = (dt.Rows[0]["YN_AM"].ToString() == "Y") ? false : true;
            }

            this.MainGrids = new FlexGrid[] { this._flexH, this._flexL };
            this._flexH.DetailGrids = new FlexGrid[] { this._flexL };

            InitGridH();
            InitGridL();
            InitEvent();

            fileIcon = imageList1.Images[0];
        }

        private void InitEvent()
        {
            this.DragOver += new DragEventHandler(this.Control_DragOver);
            this.DragDrop += new DragEventHandler(this.Control_DragDrop);

            this.ctx영업그룹.QueryBefore += new BpQueryHandler(this.ctx영업그룹_QueryBefore);
            this.ctx영업조직.QueryAfter += new BpQueryHandler(this.ctx영업조직_QueryAfter);
            this.bp프로젝트.QueryBefore += new BpQueryHandler(this.Control_QueryBefore); //2011.04.05 추가 
            
            this.cbo출고구분.KeyDown += new KeyEventHandler(this.CommonComboBox_KeyEvent);
            this.cbo거래구분.KeyDown += new KeyEventHandler(this.CommonComboBox_KeyEvent);
            this.btn선적등록.Click += new EventHandler(this.btn선적등록_Click);

            this._flexH.DoubleClick += new EventHandler(this._flexH_DoubleClick);
            this._flexH.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);
            this._flexH.AfterDataRefresh += new ListChangedEventHandler(this._flexH_AfterDataRefresh);
            this._flexH.CheckHeaderClick += new EventHandler(this._flexH_CheckHeaderClick);
            this._flexH.ValidateEdit += new ValidateEditEventHandler(this._flexH_ValidateEdit);

            this._flexH.CellContentChanged += new CellContentEventHandler(this._flexH_CellContentChanged);
        }

        private void InitGridH()
        {
            this._flexH.BeginSetting(1, 1, false);

            this._flexH.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            if (Global.MainFrame.LoginInfo.CompanyCode == "K100" || 
                Global.MainFrame.LoginInfo.CompanyCode == "K200")
                this._flexH.SetCol("NM_GW_STATUS", "결재상태", 80);
            this._flexH.SetCol("NM_COMPANY", "회사", 100);
            this._flexH.SetCol("NO_ISURCV", "협조전번호", 100);
            this._flexH.SetCol("NO_IO", "수불번호", 100);
            this._flexH.SetCol("NO_SO", "수주번호", false);
            this._flexH.SetCol("DT_LOADING", "선적일자", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("DT_IO", "수불일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("LN_PARTNER", "매출처", 120);
            this._flexH.SetCol("NM_DELIVERY_TO", "납품처", 120);
            this._flexH.SetCol("NM_VESSEL", "호선명", 120);
            this._flexH.SetCol("NM_KOR", "담당자", 100);
            this._flexH.SetCol("YN_RETURN", "출고구분", 60);
            this._flexH.SetCol("DC_RMK", "비고", 160, true);
            this._flexH.SetCol("DTS_INSERT", "등록일자", 120);
            this._flexH.SetCol("FILE_PATH_MNG", "인수증", 200, false);
            this._flexH.SetCol("FILE_PATH_MNG1", "기타파일", 200, false);
            this._flexH.SetCol("QT_ITEM", "종수", 40, false, typeof(decimal));

            this._flexH.SetDummyColumn("S", "FILE_PATH_MNG", "FILE_PATH_MNG1", "MEMO_CD", "CHECK_PEN");
            this._flexH.ExtendLastCol = true;

            this._flexH.SettingVersion = "1.0.0.0";
            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            //Memo기능 관련 설정
            this._flexH.CellNoteInfo.EnabledCellNote = true;
            this._flexH.CellNoteInfo.CategoryID = this.Name;
            this._flexH.CellNoteInfo.DisplayColumnForDefaultNote = "NO_GIR";

            //CheckPen기능 관련 설정
            this._flexH.CheckPenInfo.EnabledCheckPen = true;

            // true이면 Header의 Row가 바뀔때마다 Line 내용을 그때 그때 가져온다는 뜻.
            this._flexH.WhenRowChangeThenGetDetail = true;
        }

        private void InitGridL()
        {
            this._flexL.BeginSetting(1, 1, false);

            this._flexL.SetCol("CD_ITEM", "품목코드", 80);
            this._flexL.SetCol("NM_ITEM", "품목명", 100);
            this._flexL.SetCol("STND_ITEM", "규격", 60);
            this._flexL.SetCol("FG_TRANS", "거래구분", 80);
            this._flexL.SetCol("CD_SL", "창고코드", 100);
            this._flexL.SetCol("NM_SL", "창고명", 100);
            this._flexL.SetCol("UNIT_IM", "단위", 65);
            this._flexL.SetCol("CD_UNIT_MM", "수배단위", 65);

            if (b수량권한)
            {
                this._flexL.SetCol("QT_IO", "수불수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
                this._flexL.SetCol("QT_UNIT_MM", "수배수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
                this._flexL.SetCol("QT_CLS", "마감수량", 70, false, typeof(decimal), FormatTpType.QUANTITY);
            }
            if (b단가권한)
            {
                this._flexL.SetCol("UM_EX", "외화단가", 90, 17, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                this._flexL.SetCol("UM", "원화단가", 90, 17, false, typeof(decimal), FormatTpType.UNIT_COST);
            }
            if (b금액권한)
            {
                this._flexL.SetCol("AM_EX", "외화금액", 90, 17, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flexL.SetCol("AM", "원화금액", 80, false, typeof(decimal), FormatTpType.MONEY);
                this._flexL.SetCol("VAT", "부가세", 80, false, typeof(decimal), FormatTpType.MONEY);
            }

            this._flexL.SetCol("FG_MNG", "관리단위", 65);
            this._flexL.SetCol("YN_AM", "유무환구분", 60);
            this._flexL.SetCol("NM_QTIOTP", "출고형태", 80);
            this._flexL.SetCol("NO_ISURCV", "의뢰번호", 100);
            this._flexL.SetCol("NM_PROJECT", "프로젝트명", 100);
            this._flexL.SetCol("NO_PSO_MGMT", "수주번호", 100);
            this._flexL.SetCol("STA_SO", "수주상태", 60);
            this._flexL.SetCol("GI_PARTNER", "납품처코드", 100);
            this._flexL.SetCol("LN_GI_PARTNER", "납품처명", 100);
            this._flexL.SetCol("CD_ZONE", "LOCATION", 200);
            this._flexL.SetCol("FG_SERNO", "LOT/SN", 80);
            this._flexL.SetCol("NM_ITEMGRP", "품목군", 100);
            this._flexL.SetCol("DT_DUEDATE", "납기일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexL.SetCol("NM_EXCH", "통화명", 60, false);
            this._flexL.SetCol("DC_RMK_L", "비고", 100, false);
            this._flexL.SetCol("MAT_ITEM", "재질", false);
            this._flexL.SetCol("FG_USE2", "수주용도2", 100, false);
            this._flexL.SetCol("NO_PO_PARTNER", "매출처발주번호", 100, false);
            this._flexL.SetCol("NO_POLINE_PARTNER", "매출처발주항번", 100, false);

            this._flexL.SettingVersion = "1.0.0.6";
            this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            if (b단가권한)
                this._flexL.SetExceptSumCol("UM_EX", "UM");
        }

        protected override void InitPaint()
        {
            DataSet g_dsCombo;

            try
            {
                base.InitPaint();

                oneGrid1.UseCustomLayout = true;
                bpPanelControl1.IsNecessaryCondition = true;
                oneGrid1.InitCustomLayout();

                this.chk자동생성건제외.Checked = true;

                pp납기일.StartDateToString = "";
                pp납기일.EndDateToString = "";

                this.ctx회사.AddItem(this.LoginInfo.CompanyCode, this.LoginInfo.CompanyName);

                g_dsCombo = this.GetComboData("N;MA_PLANT",
                                              "S;PU_C000016",
                                              "S;PU_C000027",
                                              "S;SA_B000016",
                                              "S;SA_B000063",
                                              "N;MA_B000015");

                //거래구분
                cbo거래구분.DataSource = g_dsCombo.Tables[1].Copy();
                cbo거래구분.DisplayMember = "NAME";
                cbo거래구분.ValueMember = "CODE";

                //출고구분
                cbo출고구분.DataSource = g_dsCombo.Tables[2].Copy();
                cbo출고구분.DisplayMember = "NAME";
                cbo출고구분.ValueMember = "CODE";

                _flexH.SetDataMap("YN_RETURN", g_dsCombo.Tables[2].Copy(), "CODE", "NAME"); //출고구분
                _flexL.SetDataMap("FG_TRANS", g_dsCombo.Tables[1].Copy(), "CODE", "NAME"); //거래구분
                _flexL.SetDataMap("STA_SO", g_dsCombo.Tables[3], "CODE", "NAME");   //수주상태

                _flexL.SetDataMap("FG_USE2", g_dsCombo.Tables[4], "CODE", "NAME");

                DataTable dtFG_SERNO = g_dsCombo.Tables[5];
                DataRow[] drs = dtFG_SERNO.Select("CODE = '001'", "", DataViewRowState.CurrentRows);
                if (drs.Length == 1)
                    drs[0]["NAME"] = "";
                _flexL.SetDataMap("FG_SERNO", dtFG_SERNO, "CODE", "NAME");

                this.txt의뢰번호.Text = this._의뢰번호;

                if (_수불일자 != string.Empty)
                {
                    dtp출고일.StartDateToString = _수불일자;
                    dtp출고일.EndDateToString = _수불일자;
                    bp매출처.CodeValue = _매출처코드;
                    bp매출처.CodeName = _매출처명;
                    bp담당자.CodeValue = _사번;
                    bp담당자.CodeName = _이름;

                    this.OnToolBarSearchButtonClicked(null, null);
                }
                else
                {
                    dtp출고일.StartDateToString = MainFrameInterface.GetDateTimeToday().AddMonths(-1).ToString("yyyyMMdd");
                    dtp출고일.EndDateToString = MainFrameInterface.GetStringToday;
                    bp담당자.CodeValue = LoginInfo.EmployeeNo;
                    bp담당자.CodeName = LoginInfo.EmployeeName;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region ★ 메인버튼 클릭
        protected override bool BeforeSearch()
        {
            return base.BeforeSearch();
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!this.BeforeSearch()) return;

                if (!Chk출고일) return;

                DataTable dt = null;

                int 기간 = Math.Abs((this.dtp출고일.StartDate - this.dtp출고일.EndDate).Days);

                string startDate, endDate;
                DataTable tmpDataTable;

                for (int i = 0; i <= 기간 / 365; i++)
                {
                    startDate = this.dtp출고일.StartDate.AddDays(i * 365).ToString("yyyyMMdd");

                    if (기간 >= (i + 1) * 365)
                        endDate = this.dtp출고일.StartDate.AddDays(((i + 1) * 365) - 1).ToString("yyyyMMdd");
                    else
                        endDate = this.dtp출고일.EndDateToString;

                    MsgControl.ShowMsg("조회 중 입니다. 잠시만 기다려 주세요.\n조회기간 (@ ~ @)", new string[] { Util.GetTo_DateStringS(startDate), Util.GetTo_DateStringS(endDate) });          

                    tmpDataTable = this._biz.Search(new object[] { this.ctx회사.QueryWhereIn_Pipe,
                                                                   startDate,
                                                                   endDate,
                                                                   this.pp납기일.StartDateToString,
                                                                   this.pp납기일.EndDateToString,
                                                                   D.GetString(this.bp매출처.CodeValue),
                                                                   D.GetString(this.bp담당자.CodeValue),
                                                                   D.GetString(this.cbo거래구분.SelectedValue),
                                                                   D.GetString(this.cbo출고구분.SelectedValue),
                                                                   D.GetString(this.bp출고형태.CodeValue),
                                                                   this.ctx영업조직.QueryWhereIn_Pipe,
                                                                   this.ctx영업그룹.QueryWhereIn_Pipe,
                                                                   D.GetString(this.bp프로젝트.CodeValue),
                                                                   this.txt의뢰번호.Text,
                                                                   this.txt수불번호.Text,
                                                                   MA.Login.사원번호,
                                                                   this.txt수주번호.Text,
                                                                   (this.chk자동생성건제외.Checked == true ? "Y" : "N") });

                    if (i == 0)
                        dt = tmpDataTable;
                    else
                        dt.Merge(tmpDataTable);
                }

                this._flexH.Binding = dt;

                if (!this._flexH.HasNormalRow)
                {
                    ShowMessage(PageResultMode.SearchNoData);
                    return;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
            }
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            DataRow[] dataRowArray, dataRowArray1;
            StringBuilder 검증리스트, 마감검증리스트;
            string 메시지, 메시지마감, NO_IO, MULTI_IO, query;
            bool 검증여부, 검증여부_마감;


            try
            {
                base.OnToolBarDeleteButtonClicked(sender, e);

                if (!base.BeforeDelete()) return;

                dataRowArray = this._flexH.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    검증여부 = false;
                    검증여부_마감 = false;

                    검증리스트 = new StringBuilder();
                    마감검증리스트 = new StringBuilder();

                    메시지 = DD("품목코드") + "  " + DD("품목명") + "\t " + DD("수주상태") + "\t " + DD("출고번호") + "\t\t " + DD("수주번호") + "\t";
                    메시지마감 = DD("마감") + "/" + DD("송장번호") + " " + DD("항번") + "   " + DD("매출처") + "\t " + DD("품목코드") + "  " + DD("품목명") + "   " + DD("규격") + "  " + DD("마감수량") + "  " + DD("출고번호") + "  " + DD("출고항번");

                    검증리스트.AppendLine(메시지);
                    마감검증리스트.AppendLine(메시지마감);

                    메시지 = "-".PadRight(90, '-');
                    검증리스트.AppendLine(메시지);
                    마감검증리스트.AppendLine(메시지);

                    NO_IO = string.Empty;
                    MULTI_IO = string.Empty;

                    foreach (DataRow dr in dataRowArray)
                    {
                        dataRowArray1 = this._flexL.DataTable.Select("NO_IO = '" + D.GetString(dr["NO_IO"]) + "'");

                        if (NO_IO != D.GetString(dr["NO_IO"]))
                        {
                            NO_IO = D.GetString(dr["NO_IO"]);
                            MULTI_IO += NO_IO + "|";
                        }

                        foreach (DataRow dr1 in dataRowArray1)
                        {
                            if (D.GetString(dr1["NO_IO_MGMT"]) != string.Empty) continue;

                            if (dr1["STA_SO"].ToString() == "C")
                            {
                                query = @"UPDATE SL
SET SL.STA_SO = 'R',
    SL.STA_SO_HST = SL.STA_SO,
    SL.DTS_STA_HST = ISNULL(SL.DTS_UPDATE, SL.DTS_INSERT),
    SL.ID_STA_HST = ISNULL(SL.ID_UPDATE, SL.ID_INSERT)  
FROM SA_SOL SL
WHERE SL.CD_COMPANY = '{0}'
AND SL.NO_SO = '{1}'
AND SL.SEQ_SO = '{2}'";

                                DBHelper.ExecuteScalar(string.Format(query, new object[] { dr1["CD_COMPANY"].ToString(),
                                                                                           dr1["NO_PSO_MGMT"].ToString(),
                                                                                           dr1["NO_PSOLINE_MGMT"].ToString() }));

                                query = @"UPDATE SH
SET SH.STA_SO = 'R' 
FROM SA_SOH SH
WHERE SH.CD_COMPANY = '{0}'
AND SH.NO_SO = '{1}'
AND NOT EXISTS (SELECT 1 
                FROM SA_SOL SL
                WHERE SL.CD_COMPANY = SH.CD_COMPANY
                AND SL.NO_SO = SH.NO_SO
                AND SL.STA_SO <> 'R')";

                                DBHelper.ExecuteScalar(string.Format(query, new object[] { dr1["CD_COMPANY"].ToString(),
                                                                                           dr1["NO_PSO_MGMT"].ToString() }));

                            }
                        }
                    }

                    DataTable chkDt = this._biz.CloseMessage(MULTI_IO);

                    if (chkDt != null && chkDt.Rows.Count > 0)
                    {
                        string 출고번호 = string.Empty;
                        string msg_cls2 = string.Empty;
                        string 마감번호 = string.Empty;
                        string 마감항번 = string.Empty;
                        string 매출처 = string.Empty;
                        string 품목코드 = string.Empty;
                        string 품목명 = string.Empty;
                        string 규격 = string.Empty;
                        string 마감수량 = string.Empty;
                        string 출고항번 = string.Empty;

                        foreach (DataRow dr in chkDt.Rows)
                        {
                            마감번호 = D.GetString(dr["NO_IV"]).PadRight(8, ' ');
                            마감항번 = D.GetString(dr["NO_LINE"]).PadRight(2, ' ');
                            매출처 = D.GetString(dr["LN_PARTNER"]).PadRight(8, ' ');
                            품목코드 = D.GetString(dr["CD_ITEM"]).PadRight(5, ' ');
                            품목명 = D.GetString(dr["NM_ITEM"]).PadRight(8, ' ');
                            규격 = D.GetString(dr["STND_ITEM"]).PadRight(8, ' ');
                            마감수량 = D.GetString(dr["QT_CLS"]).PadRight(8, ' ');
                            출고번호 = D.GetString(dr["NO_IO"]).PadRight(8, ' ');
                            출고항번 = D.GetString(dr["NO_IOLINE"]).PadRight(2, ' ');

                            msg_cls2 = 마감번호 + " " + 마감항번 + " " + 매출처 + " " + 품목코드 + " " + 품목명 + " " + 규격 + " " + 마감수량 + " " + 출고번호 + " " + 출고항번;
                            마감검증리스트.AppendLine(msg_cls2);
                            검증여부_마감 = true;
                        }
                    }

                    if (검증여부_마감)
                    {
                        this.ShowDetailMessage(" 이미 적용받아 진행한 건이 존재합니다. 해당 출고건을 확인하기 위해서는 \n " +
                                               " \n ▼ 버튼을 눌러서 후처리된 목록을 확인하세요!", 마감검증리스트.ToString());

                    }

                    if (검증여부 || 검증여부_마감)
                        return;

                    string POP = BASIC.GetMAEXC("POP연동");
                    string MES = BASIC.GetMAEXC("MES_POP연동옵션");

                    if (POP != "000" || MES != "000")
                    {
                        if (this.ShowMessage("타 시스템으로부터 연동된 처리건입니다. 삭제하시겠습니까?", "QY2") != DialogResult.Yes)
                            return;
                    }

                    this._flexH.Redraw = false;
                    this._flexL.Redraw = false;

                    foreach (DataRow drH in dataRowArray)
                    {
                        dataRowArray1 = this._flexL.DataTable.Select("NO_IO = '" + D.GetString(drH["NO_IO"]) + "'");

                        foreach (DataRow drL in dataRowArray1)
                        {
                            drL.Delete();
                        }

                        drH.Delete();
                    }

                    this._flexH.Redraw = true;
                    this._flexL.Redraw = true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this._flexH.Redraw = true;
                this._flexL.Redraw = true;
            }
        }

		protected override bool BeforeSave()
		{
            if (this._flexH.GetChanges().Select(string.Format("ISNULL(DT_LOADING, '') <> '' AND DT_LOADING <= '{0}'", Global.MainFrame.GetDateTimeToday().AddYears(-1).ToString("yyyyMMdd"))).Length > 0)
            {
                this.ShowMessage("선적일자는 현재일자 기준으로 1년 이전 일자를 입력할 수 없습니다.");
                return false;
            }

            if (this._flexH.GetChanges().Select(string.Format("NOT (CD_MAIN_CATEGORY = '003' AND CD_SUB_CATEGORY = '001') AND ISNULL(DT_LOADING, '') <> '' AND DT_LOADING > '{0}'", Global.MainFrame.GetStringToday)).Length > 0)
			{
                this.ShowMessage("선적일자는 현재일자를 초과해서 입력 할 수 없습니다.");
                return false;
			}

            if (Global.MainFrame.LoginInfo.CompanyCode != "S100" &&
                !Global.MainFrame.LoginInfo.GroupID.Contains("ADM") &&
                this._flexH.GetChanges().Select().Where(x => this._flexL.DataTable.Select("NO_IO = '" + x["NO_IO"].ToString() + "' AND FG_TRANS = '001'").Length == 0 &&
                                                             !string.IsNullOrEmpty(x["DT_LOADING"].ToString()) &&
                                                             x["YN_RETURN"].ToString() != "Y" &&
                                                             ((Convert.ToDateTime(x["DT_LOADING"].Left(4) + "-" + x["DT_LOADING"].ToString().Substring(4, 2) + "-01") <= Convert.ToDateTime(DateTime.Now.AddMonths(-2).ToString("yyyy-MM") + "-01")) || 
                                                              (DateTime.Now.Day >= 16 && (Convert.ToDateTime(x["DT_LOADING"].Left(4) + "-" + x["DT_LOADING"].ToString().Substring(4, 2) + "-01") <= Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToString("yyyy-MM") + "-01"))))).Count() > 0)
			{
                this.ShowMessage("마감 완료건으로 정산담당자에게 연락 바랍니다.");
                return false;
			}

			return base.BeforeSave();
		}

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSaveButtonClicked(sender, e);

                if (MsgAndSave(PageActionMode.Save))
                    ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            if (!base.SaveData()) return false;
            if (!this.BeforeSave()) return false;

            if (this._biz.Save(this._flexH.GetChanges()))
            {
                this._flexH.AcceptChanges();
                this._flexL.AcceptChanges();
                return true;
            }
            else
                return false;
        }
        #endregion

        #region ★ 그리드 이벤트
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
                MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
                this._flexH.Redraw = true;
            }
        }

        public void _flexH_DoubleClick(object sender, EventArgs e)
        {
            string fileCode, fileName;

            try
            {
                if (!this._flexH.HasNormalRow) return;
                if (this._flexH.MouseRow < this._flexH.Rows.Fixed) return;
                if (string.IsNullOrEmpty(D.GetString(this._flexH["NO_ISURCV"]))) return;

                if (this._flexH.Cols[_flexH.Col].Name == "FILE_PATH_MNG")
                {
                    fileCode = D.GetString(this._flexH["NO_ISURCV"]) + "_" + D.GetString(this._flexH["CD_COMPANY"]);
                    P_CZ_MA_FILE_SUB dlg = new P_CZ_MA_FILE_SUB(D.GetString(this._flexH["CD_COMPANY"]), "SA", "P_CZ_SA_GIM_REG", fileCode, "P_CZ_SA_GIM_REG" + "/" + D.GetString(this._flexH["CD_COMPANY"]) + "/" + D.GetString(this._flexH["DT_GIR"]).Substring(0, 4));
                    dlg.ShowDialog(this);

                    fileName = this._biz.SearchFileInfo(D.GetString(this._flexH["CD_COMPANY"]), fileCode);

                    if (!string.IsNullOrEmpty(fileName))
                    {
                        this._flexH["FILE_PATH_MNG"] = fileName;
                        this._flexH.SetCellImage(this._flexH.Row, this._flexH.Cols["FILE_PATH_MNG"].Index, fileIcon);
                    }
                    else
                    {
                        this._flexH["FILE_PATH_MNG"] = string.Empty;
                        this._flexH.SetCellImage(this._flexH.Row, this._flexH.Cols["FILE_PATH_MNG"].Index, null);
                    }
                }
                else if (this._flexH.Cols[_flexH.Col].Name == "FILE_PATH_MNG1")
                {
                    fileCode = D.GetString(this._flexH["NO_ISURCV"]) + "_" + D.GetString(this._flexH["CD_COMPANY"]);
                    P_CZ_MA_FILE_SUB dlg = new P_CZ_MA_FILE_SUB(D.GetString(this._flexH["CD_COMPANY"]), "SA", "P_CZ_SA_GIM_REG", fileCode + "_ETC", "P_CZ_SA_GIM_REG" + "/" + D.GetString(this._flexH["CD_COMPANY"]) + "/" + D.GetString(this._flexH["DT_GIR"]).Substring(0, 4));
                    dlg.ShowDialog(this);

                    fileName = this._biz.SearchFileInfo(D.GetString(this._flexH["CD_COMPANY"]), fileCode + "_ETC");

                    if (!string.IsNullOrEmpty(fileName))
                    {
                        this._flexH["FILE_PATH_MNG1"] = fileName;
                        this._flexH.SetCellImage(this._flexH.Row, this._flexH.Cols["FILE_PATH_MNG1"].Index, fileIcon);
                    }
                    else
                    {
                        this._flexH["FILE_PATH_MNG1"] = string.Empty;
                        this._flexH.SetCellImage(this._flexH.Row, this._flexH.Cols["FILE_PATH_MNG1"].Index, null);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                DataTable dt = null;
                string Key = _flexH[e.NewRange.r1, "NO_IO"].ToString();
                string Filter = "NO_IO = '" + Key + "'";
                if (_flexH.DetailQueryNeed)
                {
                    dt = _biz.SearchDetail(new object[] { this._flexH["CD_COMPANY"].ToString(), 
                                                          dtp출고일.StartDateToString, 
                                                          dtp출고일.EndDateToString, 
                                                          pp납기일.StartDateToString, 
                                                          pp납기일.EndDateToString, 
                                                          D.GetString(bp매출처.CodeValue), 
                                                          D.GetString(bp담당자.CodeValue), 
                                                          D.GetString(cbo거래구분.SelectedValue), 
                                                          D.GetString(cbo출고구분.SelectedValue), 
                                                          D.GetString(bp출고형태.CodeValue),
                                                          ctx영업조직.QueryWhereIn_Pipe,
                                                          ctx영업그룹.QueryWhereIn_Pipe,
                                                          D.GetString(bp프로젝트.CodeValue), 
                                                          string.Empty, 
                                                          string.Empty, 
                                                          string.Empty, 
                                                          string.Empty, 
                                                          Key,
                                                          txt수주번호.Text });
                }

                _flexL.BindingAdd(dt, Filter);
                _flexH.DetailQueryNeed = false;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flexH_AfterDataRefresh(object sender, ListChangedEventArgs e)
        {
            int rowIndex = 1, columnIndex, columnIndex1;

            try
            {
                columnIndex = this._flexH.Cols["FILE_PATH_MNG"].Index;
                columnIndex1 = this._flexH.Cols["FILE_PATH_MNG1"].Index;

                for (rowIndex = this._flexH.Rows.Fixed; rowIndex < this._flexH.Rows.Count; rowIndex++)
                {
                    if (!string.IsNullOrEmpty(D.GetString(this._flexH.Rows[rowIndex]["FILE_PATH_MNG"])))
                        this._flexH.SetCellImage(rowIndex, columnIndex, fileIcon);
                    else
                        this._flexH.SetCellImage(rowIndex, columnIndex, null);

                    if (!string.IsNullOrEmpty(D.GetString(this._flexH.Rows[rowIndex]["FILE_PATH_MNG1"])))
                        this._flexH.SetCellImage(rowIndex, columnIndex1, fileIcon);
                    else
                        this._flexH.SetCellImage(rowIndex, columnIndex1, null);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flexH_CellContentChanged(object sender, CellContentEventArgs e)
        {
            try
            {
                _biz.SaveContent(e.ContentType, e.CommandType, D.GetString(_flexH[e.Row, "NO_IO"]), e.SettingValue);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flexH_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                string @string = D.GetString(this._flexH.GetData(e.Row, e.Col));
                string editData = this._flexH.EditData;

                if (@string == editData) return;

                switch (this._flexH.Cols[e.Col].Name)
                {
                    case "DT_LOADING":
                        if (string.IsNullOrEmpty(D.GetString(this._flexH["NO_ISURCV"])))
                        {
                            e.Cancel = true;
                            this._flexH["DT_LOADING"] = string.Empty;
                            break;
                        }

                        if (this.매출등록여부확인(D.GetString(this._flexH["CD_COMPANY"]), D.GetString(this._flexH["NO_ISURCV"])))
                        {
                            e.Cancel = true;
                            this.ShowMessage("매출등록된 건은 수정할 수 없습니다.");
                            break;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private bool 매출등록여부확인(string 회사, string 협조전번호)
        {
            string query;

            try
            {
                query = @"SELECT 1 FROM MM_QTIO WITH(NOLOCK) " +
                         "WHERE CD_COMPANY = '" + 회사 + "' " +
                         "AND NO_ISURCV = '" + 협조전번호 + "' " +
                        @"AND FG_PS = 2
                          AND QT_CLS > 0";

                if (D.GetDecimal(Global.MainFrame.ExecuteScalar(query)) > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return false;
        }
        #endregion

        #region ★ 기타 이벤트
        private void Control_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void Control_DragDrop(object sender, DragEventArgs e)
        {
            string[] paths, fileNames;
            string message, messageList;

            try
            {
                paths = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                messageList = string.Empty;

                foreach (string path in paths)
                {
                    if (Directory.Exists(path) == true)
                    {
                        fileNames = Directory.GetFiles(path, "*.*");

                        foreach (string fileName in fileNames)
                        {
                            message = this.AddFile(fileName);
                            if (!string.IsNullOrEmpty(message))
                                messageList += message + Environment.NewLine;
                        }
                    }
                    else if (File.Exists(path) == true)
                    {
                        message = this.AddFile(path);
                        if (!string.IsNullOrEmpty(message))
                            messageList += message + Environment.NewLine;
                    }
                }

                this.ShowDetailMessage("File Upload 작업을 완료 했습니다." + Environment.NewLine + "[더보기] 버튼을 눌러 결과를 확인하세요.", messageList);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private string AddFile(string path)
        {
            string fileCode, 회사코드, 협조전번호, 의뢰일자, 업로드위치;
            string[] tempStr;
            FileInfo fileInfo;

            try
            {
                fileInfo = new FileInfo(path);

                tempStr = fileInfo.Name.Replace(fileInfo.Extension, string.Empty).Split('_');

                if (tempStr.Length == 2)
                {
                    협조전번호 = tempStr[0];
                    회사코드 = tempStr[1];
                    의뢰일자 = this._biz.협조전존재여부(회사코드, 협조전번호);

                    if (!string.IsNullOrEmpty(의뢰일자))
                        fileCode = 협조전번호 + "_" + 회사코드;
                    else
                        return "Upload Fail : " + path;
                }
                else if (tempStr.Length == 3 && tempStr[2] == "ETC")
				{
                    협조전번호 = tempStr[0];
                    회사코드 = tempStr[1];
                    의뢰일자 = this._biz.협조전존재여부(회사코드, 협조전번호);

                    if (!string.IsNullOrEmpty(의뢰일자))
                        fileCode = 협조전번호 + "_" + 회사코드 + "_ETC";
                    else
                        return "Upload Fail : " + path;
                }
                else
                {
                    협조전번호 = D.GetString(this._flexH["NO_ISURCV"]);
                    회사코드 = D.GetString(this._flexH["CD_COMPANY"]);
                    의뢰일자 = D.GetString(this._flexH["DT_GIR"]);

                    fileCode = 협조전번호 + "_" + 회사코드;
                }

                int pageCount = 0;
                bool isExistsFile = false;
                
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

                    dt = DBHelper.GetDataTable(string.Format(@"SELECT 1 
                                                               FROM MA_FILEINFO WITH(NOLOCK)
                                                               WHERE CD_COMPANY = '{0}'
                                                               AND CD_MODULE = 'SA'
                                                               AND ID_MENU = 'P_CZ_SA_GIM_REG'
                                                               AND CD_FILE = '{1}'", 회사코드, fileCode));

                    if (dt != null && dt.Rows.Count > 0)
                        isExistsFile = true;
                }

                bool isReciept = false;

                if (pageCount > 0 && fileInfo.Extension.Replace(".", "").ToLower() == "pdf")
                {
                    int pdfcount = PDF.GetPageCount(fileInfo.FullName);

                    if (pdfcount == pageCount)
                        isReciept = true;
                }

                업로드위치 = "Upload/P_CZ_SA_GIM_REG" + "/" + 회사코드 + "/" + 의뢰일자.Substring(0, 4);
                FileUploader.UploadFile(fileInfo.Name, fileInfo.FullName, 업로드위치, fileCode);
                this._biz.SaveFileInfo(회사코드, fileCode, fileInfo, 업로드위치);

                if (회사코드 == "K100" || 회사코드 == "K200")
				{
                    return "Upload Success : " + fileInfo.Name + "|R:" + (isReciept == true ? "Y" : "N") + "|E:" + (isExistsFile == true ? "Y" : "N");
                }
                else
                    return "Upload Success : " + fileInfo.Name;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return "Error : " + path;
        }

        private void Control_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            switch (e.HelpID)
            {
                case Duzon.Common.Forms.Help.HelpID.P_PU_EJTP_SUB:
                    e.HelpParam.P61_CODE1 = "010|041|042|";
                    break;

                //2011.04.05 추가
                case Duzon.Common.Forms.Help.HelpID.P_USER:
                    if (bp프로젝트.UserHelpID == "H_SA_PRJ_SUB")
                    {
                        e.HelpParam.P41_CD_FIELD1 = "프로젝트";             //도움창 이름 찍어줄 값
                    }
                    break;
            }
        }

        private void CommonComboBox_KeyEvent(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                System.Windows.Forms.SendKeys.SendWait("{TAB}");
        }

        private void btn선적등록_Click(object sender, EventArgs e)
        {
            try
            {
                P_CZ_SA_GIM_REG_SUB subDlg = new P_CZ_SA_GIM_REG_SUB(D.GetString(this._flexH["CD_COMPANY"]), D.GetString(this._flexH["NM_COMPANY"]), D.GetString(this._flexH["NO_ISURCV"]));
                subDlg.Show(this);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void ctx영업그룹_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                if (!ctx영업조직.IsEmpty())
                {
                    e.HelpParam.P61_CODE1 = ctx영업조직.QueryWhereIn_Pipe;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void ctx영업조직_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                ctx영업그룹.Clear();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region ★ 속성
        bool Chk출고일 { get { return Checker.IsValid(this.dtp출고일, true, this.lbl출고일.Text); } }
        #endregion
    }
}
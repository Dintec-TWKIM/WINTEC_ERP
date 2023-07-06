using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.Windows.Print;

namespace sale
{
    // **************************************
    // 작   성   자 : 허성철
    // 재 작  성 일 : 2006-10-25
    // 모   듈   명 : 영업
    // 시 스  템 명 : 영업관리
    // 서브시스템명 : 출하관리
    // 페 이 지  명 : 출하 관리
    // 프로젝트  명 : P_SA_GIM_REG
    // **************************************
    public partial class P_SA_GIM_REG : Duzon.Common.Forms.PageBase
    {
        #region ★ 멤버필드

        private P_SA_GIM_REG_BIZ _biz = new P_SA_GIM_REG_BIZ();

        string _수불일자 = string.Empty;
        string _공장 = string.Empty;
        string _거래처코드 = string.Empty;
        string _거래처명 = string.Empty;
        string _사번 = string.Empty;
        string _이름 = string.Empty;
        string _수불번호 = string.Empty;

        bool bCheckMsg = false;
        bool b수량권한 = true;
        bool b단가권한 = true;
        bool b금액권한 = true;

        string 시리얼여부 = string.Empty;
        Image file_Icon = null;
        #endregion

        #region ★ 초기화

        #region -> 생성자

        public P_SA_GIM_REG()
        {
            InitializeComponent();

            MainGrids = new FlexGrid[] { _flexH, _flexL };
            DataChanged += new EventHandler(Page_DataChanged);

            btn_Serial.Enabled = App.SystemEnv.SERIAL사용;
        }

        public P_SA_GIM_REG(string 수불일자, string 공장, string 거래처코드, string 거래처명, string 사번, string 이름)
        {

            InitializeComponent();
            _수불일자 = 수불일자;
            _공장 = 공장;
            _거래처코드 = 거래처코드;
            _거래처명 = 거래처명;
            _사번 = 사번;
            _이름 = 이름;
            MainGrids = new FlexGrid[] { _flexH, _flexL };
            DataChanged += new EventHandler(Page_DataChanged);
        }

        public P_SA_GIM_REG(string callPage, string noIo, string dtIo, string cdPlant, string fgCls)
            : this()
        {
            _수불번호 = noIo;
            _수불일자 = dtIo;
            _공장 = cdPlant;
        }

        #endregion

        #region -> InitLoad

        protected override void InitLoad()
        {
            base.InitLoad();

            DataTable dt = BASIC.MFG_AUTH("P_SA_GIM_REG");
            if (dt.Rows.Count > 0)
            {
                b수량권한 = (dt.Rows[0]["YN_QT"].ToString() == "Y") ? false : true;
                b단가권한 = (dt.Rows[0]["YN_UM"].ToString() == "Y") ? false : true;
                b금액권한 = (dt.Rows[0]["YN_AM"].ToString() == "Y") ? false : true;
            }

            InitGridH();
            InitGridL();
            InitEvent();

            _flexH.DetailGrids = new FlexGrid[] { _flexL };
            _flexH.WhenRowChangeThenGetDetail = true;          // true이면 Header의 Row가 바뀔때마다 Line 내용을 그때 그때 가져온다는 뜻.

            file_Icon = imageList1.Images[0];

            if (Global.MainFrame.ServerKeyCommon == "CHOSUNHOTELBA")
            {
                //pnlHeader.Size = new System.Drawing.Size(pnlHeader.Size.Width, pnlHeader.Size.Height + 27);
                oneGrid2.Visible = true;
            }
        }

        private void InitEvent()
        {
            bpc영업그룹.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(bpc영업그룹_QueryBefore);
            bpc영업조직.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(bpc영업조직_QueryAfter);
            bp프로젝트.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(Control_QueryBefore); //2011.04.05 추가 
        }

        #endregion

        #region -> InitGridH

        void InitGridH()
        {
            _flexH.BeginSetting(1, 1, false);
            _flexH.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            _flexH.SetDummyColumn("S", "MEMO_CD", "CHECK_PEN");
            _flexH.SetCol("NO_IO", "수불번호", 100);
            _flexH.SetCol("DT_IO", "수불일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexH.SetCol("LN_PARTNER", "거래처", 120);
            _flexH.SetCol("NM_KOR", "담당자", 100);
            _flexH.SetCol("YN_RETURN", "출하구분", 60);
            _flexH.SetCol("DC_RMK", "비고", 160, true);
            _flexH.SetCol("FILE_PATH_MNG", "첨부파일", 200, true);
            if (Global.MainFrame.ServerKeyCommon == "CHOSUNHOTELBA")
            {
                _flexH.SetCol("CD_PARTNER_GRP", "거래처그룹", 150, false);
                _flexH.SetCol("CD_PARTNER_GRP_2", "거래처그룹2", 150, false);
            }
            _flexH.ExtendLastCol = true;
            _flexH.EnabledHeaderCheck = true;
            _flexH.SettingVersion = "1.0.0.5";
            _flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flexH.StartEdit += new RowColEventHandler(_flex_StartEdit);
            _flexH.DoubleClick += new EventHandler(_flexH_DoubleClick);
            _flexH.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
            _flexH.BeforeRowChange += new RangeEventHandler(_flexH_BeforeRowChange);
            _flexH.CheckHeaderClick += new EventHandler(_flexH_CheckHeaderClick);
            //메뉴추가
            _flexH.AddMenuSeperator();
            ToolStripMenuItem parent = _flexH.AddPopup("첨부파일");
            _flexH.AddMenuItem(parent, "다운로드", File_Download);
            _flexH.AddMenuItem(parent, "삭제", File_Delete);

            //Memo기능 관련 설정
            _flexH.CellNoteInfo.EnabledCellNote = true;
            _flexH.CellNoteInfo.CategoryID = this.Name;
            _flexH.CellNoteInfo.DisplayColumnForDefaultNote = "NO_GIR";

            //CheckPen기능 관련 설정
            _flexH.CheckPenInfo.EnabledCheckPen = true;

            _flexH.CellContentChanged += new CellContentEventHandler(_flexH_CellContentChanged);
        }

        #endregion

        #region -> InitGridL

        private void InitGridL()
        {
            _flexL.BeginSetting(1, 1, false);
            _flexL.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            _flexL.SetDummyColumn("S");
            _flexL.SetCol("CD_ITEM", "품목코드", 80);
            _flexL.SetCol("NM_ITEM", "품목명", 100);
            _flexL.SetCol("STND_ITEM", "규격", 60);
            _flexL.SetCol("FG_TRANS", "거래구분", 80);
            _flexL.SetCol("CD_SL", "창고코드", 100);
            _flexL.SetCol("NM_SL", "창고명", 100);
            _flexL.SetCol("UNIT_IM", "단위", 65);
            _flexL.SetCol("CD_UNIT_MM", "수배단위", 65);

            if (b수량권한)
            {
                _flexL.SetCol("QT_IO", "수불수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
                _flexL.SetCol("QT_UNIT_MM", "수배수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
                _flexL.SetCol("QT_CLS", "마감수량", 70, false, typeof(decimal), FormatTpType.QUANTITY);
            }
            if (b단가권한)
            {
                _flexL.SetCol("UM_EX", "외화단가", 90, 17, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                _flexL.SetCol("UM", "원화단가", 90, 17, false, typeof(decimal), FormatTpType.UNIT_COST);
            }
            if (b금액권한)
            {
                _flexL.SetCol("AM_EX", "외화금액", 90, 17, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                _flexL.SetCol("AM", "원화금액", 80, false, typeof(decimal), FormatTpType.MONEY);
                _flexL.SetCol("VAT", "부가세", 80, false, typeof(decimal), FormatTpType.MONEY);
            }

            _flexL.SetCol("FG_MNG", "관리단위", 65);
            _flexL.SetCol("YN_AM", "유무환구분", 60);
            _flexL.SetCol("NM_QTIOTP", "출하형태", 80);
            _flexL.SetCol("NO_ISURCV", "의뢰번호", 100);
            _flexL.SetCol("NM_PROJECT", "프로젝트명", 100);
            _flexL.SetCol("NO_PSO_MGMT", "수주번호", 100);
            _flexL.SetCol("STA_SO", "수주상태", 60);
            _flexL.SetCol("GI_PARTNER", "납품처코드", 100);
            _flexL.SetCol("LN_GI_PARTNER", "납품처명", 100);
            _flexL.SetCol("CD_ZONE", "LOCATION", 200);
            _flexL.SetCol("FG_SERNO", "LOT/SN", 80);
            _flexL.SetCol("NM_ITEMGRP", "품목군", 100);
            _flexL.SetCol("DT_DUEDATE", "납기일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexL.SetCol("NM_EXCH", "환종명", 80, false);
            _flexL.SetCol("DC_RMK_L", "비고", 100, false);
            _flexL.SetCol("MAT_ITEM", "재질", false);
            _flexL.SetCol("FG_USE2", "수주용도2", 100, false);
            _flexL.SetCol("NO_PO_PARTNER", "거래처P0번호", 100, false);
            _flexL.SetCol("NO_POLINE_PARTNER", "거래처PO항번", 100, false);

            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "MHIK")  //엠에이치파워시스템즈코리아
                _flexL.SetCol("SOH_TXT_USERDEF4", "전용수주번호", 150, false);

            if (MA.ServerKey(false, new string[] { "GNSD" })) // GNSD CHINA
            {
                _flexL.SetCol("UMVAT_GI", "세금포함단가", 100, false, typeof(decimal), FormatTpType.UNIT_COST);
                _flexL.SetCol("TP_UM_TAX", "세금포함여부", 100);
            }

            _flexL.SettingVersion = "1.0.0.6";
            _flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            if (b단가권한)
                _flexL.SetExceptSumCol("UM_EX", "UM");
        }

        #endregion

        #region -> InitPaint

        protected override void InitPaint()
        {
            base.InitPaint();

            oneGrid1.UseCustomLayout = oneGrid2.UseCustomLayout = true;
            bpPanelControl1.IsNecessaryCondition = bpPanelControl16.IsNecessaryCondition = true;
            oneGrid1.InitCustomLayout();
            oneGrid2.InitCustomLayout();

            if (Global.MainFrame.ServerKeyCommon == "CHOSUNHOTELBA")
            {
                //1. 거래처그룹, 2.거래처그룹2, 3. 사용자정의, 4. 사용자정의2
                DataSet dsCHOSUNHOTELBA = GetComboData("S;MA_B000065", "S;MA_B000067", "S;MA_B000102", "S;MA_B000103");

                cbo거래처그룹.DataSource = dsCHOSUNHOTELBA.Tables[0];
                cbo거래처그룹.DisplayMember = "NAME";
                cbo거래처그룹.ValueMember = "CODE";

                cbo거래처그룹2.DataSource = dsCHOSUNHOTELBA.Tables[1];
                cbo거래처그룹2.DisplayMember = "NAME";
                cbo거래처그룹2.ValueMember = "CODE";

                cbo사용자정의.DataSource = dsCHOSUNHOTELBA.Tables[2];
                cbo사용자정의.DisplayMember = "NAME";
                cbo사용자정의.ValueMember = "CODE";

                cbo사용자정의2.DataSource = dsCHOSUNHOTELBA.Tables[3];
                cbo사용자정의2.DisplayMember = "NAME";
                cbo사용자정의2.ValueMember = "CODE";

                if (_flexH.Cols.Contains("CD_PARTNER_GRP"))
                    _flexH.SetDataMap("CD_PARTNER_GRP", dsCHOSUNHOTELBA.Tables[0].Copy(), "CODE", "NAME");
                if (_flexH.Cols.Contains("CD_PARTNER_GRP_2"))
                    _flexH.SetDataMap("CD_PARTNER_GRP_2", dsCHOSUNHOTELBA.Tables[1].Copy(), "CODE", "NAME");
            }


            //pp출하일.Mask = GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT);
            pp출하일.StartDateToString = Global.MainFrame.GetStringFirstDayInMonth;
            //pp출하일.EndDateToString = Global.MainFrame.GetStringToday;

            //dp출하일끝.Mask = GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT);
            //dp출하일끝.ToDayDate = MainFrameInterface.GetDateTimeToday();

            //dp납기일시작.Mask = GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT);
            //dp납기일시작.ToDayDate = MainFrameInterface.GetDateTimeToday();
            //dp납기일시작.Text = "";
            pp납기일.StartDateToString = "";
            pp납기일.EndDateToString = "";
            //dp납기일끝.Mask = GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT);
            //dp납기일끝.ToDayDate = MainFrameInterface.GetDateTimeToday();
            //dp납기일끝.Text = "";

            DataSet g_dsCombo = GetComboData("N;MA_PLANT", "S;PU_C000016", "S;PU_C000027", "S;SA_B000016");

            //공장
            cbo공장.DataSource = g_dsCombo.Tables[0];
            cbo공장.DisplayMember = "NAME";
            cbo공장.ValueMember = "CODE";

            //거래구분
            cbo거래구분.DataSource = g_dsCombo.Tables[1];
            cbo거래구분.DisplayMember = "NAME";
            cbo거래구분.ValueMember = "CODE";

            //출하구분
            cbo출하구분.DataSource = g_dsCombo.Tables[2];
            cbo출하구분.DisplayMember = "NAME";
            cbo출하구분.ValueMember = "CODE";

            _flexH.SetDataMap("YN_RETURN", g_dsCombo.Tables[2], "CODE", "NAME"); //출하구분
            _flexL.SetDataMap("FG_TRANS", g_dsCombo.Tables[1], "CODE", "NAME"); //거래구분
            _flexL.SetDataMap("STA_SO", g_dsCombo.Tables[3], "CODE", "NAME");   //수주상태

            _flexL.SetDataMap("FG_USE2", MA.GetCode("SA_B000063", true), "CODE", "NAME");

            DataTable dtFG_SERNO = MA.GetCode("MA_B000015");
            DataRow[] drs = dtFG_SERNO.Select("CODE = '001'", "", DataViewRowState.CurrentRows);
            if (drs.Length == 1)
                drs[0]["NAME"] = "";
            _flexL.SetDataMap("FG_SERNO", dtFG_SERNO, "CODE", "NAME");

            if (_수불일자 != string.Empty)
            {
                pp출하일.StartDateToString = _수불일자;
                pp출하일.EndDateToString = _수불일자;
                cbo공장.SelectedValue = _공장;
                bp거래처.CodeValue = _거래처코드;
                bp거래처.CodeName = _거래처명;
                bp담당자.CodeValue = _사번;
                bp담당자.CodeName = _이름;

                OnToolBarSearchButtonClicked(null, null);
                _수불번호 = string.Empty;   //처음 호출시에 해당 수불번호만 조회되도록 하고 그 이후엔 조회조건대로 조회되도록 한다.(2012.04.05)
            }
            else
            {
                pp출하일.StartDateToString = MainFrameInterface.GetStringFirstDayInMonth;
                pp출하일.EndDateToString = MainFrameInterface.GetStringToday;
                bp담당자.CodeValue = LoginInfo.EmployeeNo;
                bp담당자.CodeName = LoginInfo.EmployeeName;
            }

            서버키enable();

            string 수주종결자동취소_라인선택기준 = BASIC.GetMAEXC("수주종결자동취소_라인선택기준");

            if (수주종결자동취소_라인선택기준 == "100")
            {
                btn삭제.Enabled = true;
                panelExt6.Visible = true;
            }

            if (Global.MainFrame.ServerKeyCommon == "KOWA")
            {
                cbo출하구분.SelectedValue = "Y";
                cbo출하구분.Enabled = false;
            }

            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "MHIK")  //엠에이치파워시스템즈코리아
                lbl수주번호.Text = "전용수주번호";
        }

        #endregion

        #endregion

        #region ★ 메인버튼 클릭

        #region -> Field_Check

        private bool Field_Check()
        {
            Hashtable hList = new Hashtable();

            //hList.Add(dp출하일시작, lbl출하일); //입고일자 시작일
            //hList.Add(dp출하일끝, dp출하일끝);   //입고일자 종료일
            hList.Add(cbo공장, lbl공장);           //수주유형

            return ComFunc.NullCheck(hList);
        }

        #endregion

        #region -> SaveData

        protected override bool SaveData()
        {
            if (!base.SaveData())
                return false;

            DataTable dt_H = null;
            DataTable dt_L = null;

            dt_H = new DataTable();
            dt_H = _flexH.GetChanges();

            dt_L = new DataTable();
            dt_L = _flexL.GetChanges();

            //DataTable dtL = _flexL.GetChanges();

            if ((dt_H == null || dt_H.Rows.Count == 0) && (dt_L == null || dt_L.Rows.Count == 0))
                return true;

            MsgControl.ShowMsg(DD("저장중입니다."));

            bool bSuccess = _biz.Save(dt_H, dt_L);

            MsgControl.CloseMsg();

            if (!bSuccess) return false;

            _flexH.AcceptChanges();
            _flexL.AcceptChanges();

            OnToolBarSearchButtonClicked(null, null);

            return true;
        }

        #endregion

        #region -> 조회버튼 클릭

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSearch()) return;

                if (!Field_Check() || !Chk출하일) return;

                object[] obj = new object[] {
                    LoginInfo.CompanyCode, 
                    pp출하일.StartDateToString, 
                    pp출하일.EndDateToString, 
                    pp납기일.StartDateToString, 
                    pp납기일.EndDateToString, 
                    D.GetString(cbo공장.SelectedValue), 
                    D.GetString(bp거래처.CodeValue), 
                    D.GetString(bp담당자.CodeValue), 
                    D.GetString(cbo거래구분.SelectedValue), 
                    D.GetString(cbo출하구분.SelectedValue), 
                    D.GetString(bp출하형태.CodeValue), 
                    D.GetString(bp창고.CodeValue),
                    bpc영업조직.QueryWhereIn_Pipe,
                    bpc영업그룹.QueryWhereIn_Pipe,
                    D.GetString(bp프로젝트.CodeValue), 
                    D.GetString(cbo거래처그룹.SelectedValue), 
                    D.GetString(cbo거래처그룹2.SelectedValue), 
                    D.GetString(cbo사용자정의.SelectedValue), 
                    D.GetString(cbo사용자정의2.SelectedValue),
                    _수불번호,
                    MA.Login.사원번호,
                    txt수주번호.Text
                };

                _flexH.Binding = _biz.Search(obj);

                int i_dr = 1;
                foreach (DataRow dr in _flexH.DataTable.Rows)
                {
                    if (dr["FILE_PATH_MNG"].ToString() != string.Empty)
                        _flexH.SetCellImage(i_dr, _flexH.Cols["FILE_PATH_MNG"].Index, file_Icon);

                    i_dr++;
                }

                //_flexH.AcceptChanges();

                _flexH.SetDummyColumn("S");

                if (!_flexH.HasNormalRow)
                {
                    btn_Serial.Enabled = false;
                    ShowMessage(PageResultMode.SearchNoData);
                    return;
                }

                btn_Serial.Enabled = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 삭제버튼 클릭

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!base.BeforeDelete()) return;

                string NO_IO = string.Empty;
                string MULTI_IO = string.Empty;

                DataRow[] drH = _flexH.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                if (drH == null || drH.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                string 출하번호Key = string.Empty;

                bool 검증여부 = false; bool 검증여부_마감 = false;
                StringBuilder 검증리스트 = new StringBuilder();
                StringBuilder 마감검증리스트 = new StringBuilder();

                //string msg = "품목코드  품목명\t 수주상태\t 출하번호\t\t 수주번호\t";
                string msg = DD("품목코드") + "  " + DD("품목명") + "\t " + DD("수주상태") + "\t " + DD("출하번호") + "\t\t " + DD("수주번호") + "\t";

                //string msg_cls = "마감/송장번호 항번   거래처\t 품목코드  품목명   규격  마감수량  출하번호  출하항번";
                string msg_cls = DD("마감") + "/" + DD("송장번호") + " " + DD("항번") + "   " + DD("거래처") + "\t " + DD("품목코드") + "  " + DD("품목명") + "   " + DD("규격") + "  " + DD("마감수량") + "  " + DD("출하번호") + "  " + DD("출하항번");

                검증리스트.AppendLine(msg); 마감검증리스트.AppendLine(msg_cls);

                msg = "-".PadRight(90, '-');
                검증리스트.AppendLine(msg); 마감검증리스트.AppendLine(msg);

                foreach (DataRow drHr in drH)
                {
                    출하번호Key = drHr["NO_IO"].ToString();

                    DataRow[] DRS_D1 = _flexL.DataTable.Select("NO_IO = '" + 출하번호Key + "'", "", DataViewRowState.CurrentRows);

                    if (NO_IO != drHr["NO_IO"].ToString())
                    {
                        NO_IO = drHr["NO_IO"].ToString();
                        MULTI_IO = MULTI_IO + NO_IO + "|";
                    }

                    foreach (DataRow DR in DRS_D1)
                    {
                        if (D.GetString(DR["NO_IO_MGMT"]) != string.Empty) continue;

                        if (DR["STA_SO"].ToString() == "C")
                        {
                            string 품목코드 = DR["CD_ITEM"].ToString().PadRight(10, ' ');
                            string 품목명 = DR["NM_ITEM"].ToString().PadRight(10, ' ');
                            string 수주상태코드 = DR["STA_SO"].ToString().PadRight(10, ' ');
                            string 출하번호 = DR["NO_IO"].ToString().PadRight(20, ' ');
                            string 수주번호 = DR["NO_PSO_MGMT"].ToString().PadRight(40, ' ');
                            string msg2 = 품목코드 + " " + 품목명 + " " + 수주상태코드 + " " + 출하번호Key + "          " + 수주번호;
                            검증리스트.AppendLine(msg2);
                            검증여부 = true;
                        }
                    }
                }
                ToolBarSaveButtonEnabled = !검증여부;

                if (검증여부)
                {
                    ShowDetailMessage("관련 수주번호의 상태가 '종결' 처리되었습니다. 해당 출하건을 삭제하기 위해서는 \n " +
                    "관련 수주번호의 상태값을 'R'(진행)으로 변경후, 삭제해 주십시요 \n" +
                    " \n ▼ 버튼을 눌러서 종결처리된 목록을 확인하세요!", 검증리스트.ToString());
                }

                DataTable chkDt = _biz.CloseMessage(MULTI_IO);

                if (chkDt != null && chkDt.Rows.Count > 0)
                {
                    string 출하번호 = string.Empty; string msg_cls2 = string.Empty;
                    string 마감번호 = string.Empty;
                    string 마감항번 = string.Empty;
                    string 거래처 = string.Empty;
                    string 품목코드 = string.Empty;
                    string 품목명 = string.Empty;
                    string 규격 = string.Empty;
                    string 마감수량 = string.Empty;
                    string 출하항번 = string.Empty;


                    foreach (DataRow dr in chkDt.Rows)
                    {
                        마감번호 = dr["NO_IV"].ToString().PadRight(8, ' ');
                        마감항번 = dr["NO_LINE"].ToString().PadRight(2, ' ');
                        거래처 = dr["LN_PARTNER"].ToString().PadRight(8, ' ');
                        품목코드 = dr["CD_ITEM"].ToString().PadRight(5, ' ');
                        품목명 = dr["NM_ITEM"].ToString().PadRight(8, ' ');
                        규격 = dr["STND_ITEM"].ToString().PadRight(8, ' ');
                        마감수량 = dr["QT_CLS"].ToString().PadRight(8, ' ');
                        출하번호 = dr["NO_IO"].ToString().PadRight(8, ' ');
                        출하항번 = dr["NO_IOLINE"].ToString().PadRight(2, ' ');

                        msg_cls2 = 마감번호 + " " + 마감항번 + " " + 거래처 + " " + 품목코드 + " " + 품목명 + " " + 규격 + " " + 마감수량 + " " + 출하번호 + " " + 출하항번;
                        마감검증리스트.AppendLine(msg_cls2);
                        검증여부_마감 = true;
                    }
                }

                if (검증여부_마감)
                {
                    ShowDetailMessage(" 이미 적용받아 진행한 건이 존재합니다. 해당 출하건을 확인하기 위해서는 \n " +
                    " \n ▼ 버튼을 눌러서 후처리된 목록을 확인하세요!", 마감검증리스트.ToString());

                }

                if (검증여부 || 검증여부_마감)
                    return;

                string POP = BASIC.GetMAEXC("POP연동");
                string MES = BASIC.GetMAEXC("MES_POP연동옵션");

                if (Global.MainFrame.ShowMessage("헤더와 라인 모두 일괄삭제됩니다. 삭제하시겠습니까?", "QY2") == DialogResult.Yes)
                {
                    if (POP != "000" || MES != "000")
                    {
                        DialogResult RESULT = ShowMessage("타 시스템으로부터 연동된 처리건입니다. 삭제하시겠습니까?", "QY2");
                        if (RESULT != DialogResult.Yes)
                            return;

                        _flexH.Redraw = false;

                        foreach (DataRow rowH in drH)
                        {
                            rowH.Delete();
                        }

                        string[] Args = MULTI_IO.Split('|');
                        DataRow[] drL;

                        for (int i = 0; i < Args.Length; i++)
                        {
                            drL = _flexL.DataTable.Select("NO_IO = '" + Args[i] + "'", "", DataViewRowState.CurrentRows);

                            if (drL != null && drL.Length > 0)
                            {
                                {
                                    foreach (DataRow rowL in drL)
                                        rowL.Delete();
                                }
                            }
                        }


                        bool bSuccess = _biz.Delete(MULTI_IO);
                        if (!bSuccess)
                        {
                            _flexH.Redraw = true;
                            return;
                        }

                        _flexH.AcceptChanges();
                        _flexL.AcceptChanges();

                        _flexH.Redraw = true;
                    }
                    else
                    {
                        _flexH.Redraw = false;

                        foreach (DataRow rowH in drH)
                        {
                            rowH.Delete();
                        }

                        string[] Args = MULTI_IO.Split('|');
                        DataRow[] drL;

                        for (int i = 0; i < Args.Length; i++)
                        {
                            drL = _flexL.DataTable.Select("NO_IO = '" + Args[i] + "'", "", DataViewRowState.CurrentRows);

                            if (drL != null && drL.Length > 0)
                            {
                                {
                                    foreach (DataRow rowL in drL)
                                        rowL.Delete();
                                }
                            }
                        }


                        bool bSuccess = _biz.Delete(MULTI_IO);
                        if (!bSuccess)
                        {
                            _flexH.Redraw = true;
                            return;
                        }

                        _flexH.AcceptChanges();
                        _flexL.AcceptChanges();

                        _flexH.Redraw = true;

                    }

                    OnToolBarSearchButtonClicked(null, null);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
                OnToolBarSearchButtonClicked(null, null);

            }
        }

        #endregion

        #region -> 저장버튼 클릭

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSave()) return;

                if (MsgAndSave(PageActionMode.Save))
                {
                    ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
                }
            }
            catch (Exception ex)
            {
                MsgControl.CloseMsg();
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 인쇄버튼

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            if (!_flexH.HasNormalRow)
                return;

            string ManuID = string.Empty;
            string ManuName = "출하관리";
            string PKColumn = "NO_IO";

            DataTable dt = null;

            try
            {
                string No_PK_Multi = "";

                DataRow[] ldt_Report = _flexH.DataTable.Select("S ='Y'");

                if (ldt_Report == null || ldt_Report.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
                    {
                        if (_flexH[i, "S"].ToString() == "Y")
                            No_PK_Multi += _flexH[i, PKColumn].ToString() + "|";
                    }
                }

                dt = new DataTable();

                List<string> List = new List<string>();
                List.Add(MA.Login.회사코드);
                List.Add(pp출하일.StartDateToString);
                List.Add(pp출하일.EndDateToString);
                List.Add(D.GetString(cbo공장.SelectedValue));
                List.Add(bp거래처.CodeValue);
                List.Add(bp담당자.CodeValue);
                List.Add(D.GetString(cbo거래구분.SelectedValue));
                List.Add(D.GetString(cbo출하구분.SelectedValue));
                List.Add(bp출하형태.CodeValue);
                List.Add(bp창고.CodeValue);
                List.Add(bpc영업조직.QueryWhereIn_Pipe);
                List.Add(bpc영업그룹.QueryWhereIn_Pipe);
                List.Add(D.GetString(bp프로젝트.CodeValue));
                List.Add(D.GetString(cbo거래처그룹.SelectedValue));
                List.Add(D.GetString(cbo거래처그룹2.SelectedValue));
                List.Add(D.GetString(cbo사용자정의.SelectedValue));
                List.Add(D.GetString(cbo사용자정의2.SelectedValue));
                List.Add(No_PK_Multi);

                if (rdo일반출력.Checked)
                {
                    ManuID = "R_SA_GIM_0";
                    dt = _biz.Search_Print(List.ToArray());
                }
                else if (rdo시리얼출력.Checked)
                {
                    ManuID = "R_SA_GIM_1";
                    dt = _biz.Search_Print_SERIAL(List.ToArray());
                }

                ReportHelper rptHelper = new ReportHelper(ManuID, ManuName);

                rptHelper.SetData("기간from", pp출하일.StartDateToFormatString.Replace("/", ""));
                rptHelper.SetData("기간to", pp출하일.EndDateToFormatString.Replace("/", ""));
                rptHelper.SetData("공장코드", cbo공장.SelectedValue.ToString());
                rptHelper.SetData("공장명", cbo공장.Text);
                rptHelper.SetData("거래처코드", bp거래처.CodeValue.ToString());
                rptHelper.SetData("거래처명", bp거래처.CodeName.ToString());
                rptHelper.SetData("담당자", bp담당자.CodeName.ToString());
                rptHelper.SetData("거래구분", cbo거래구분.SelectedText.ToString());
                rptHelper.SetData("출하구분", cbo출하구분.SelectedText.ToString());
                rptHelper.SetData("출하형태", bp출하형태.CodeName.ToString());
                rptHelper.SetData("창고코드", bp창고.CodeValue.ToString());
                rptHelper.SetData("창고명", bp창고.CodeName.ToString());

                rptHelper.가로출력();
                rptHelper.SetDataTable(dt);
                rptHelper.Print();
                _flexH.AcceptChanges();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region ★ 그리드 이벤트

        #region -> 헤더 클릭할때 [전체선택] 기능

        void _flexH_CheckHeaderClick(object sender, EventArgs e)
        {
            try
            {
                /* 유의 : 클릭하자마자 헤더 그리드는 선택 상태가 변경된다(ex) Checked->UnChecked  */

                if (_flexH == null || _flexL == null) return;

                if (!_flexH.HasNormalRow || !_flexL.HasNormalRow) return;

                bCheckMsg = true;
                MsgControl.ShowMsg(DD("체크박스를 체크중입니다."));

                _flexH.Row = 1; // 맨처음row에 자동위치하도록 해야 틀어지지 않는다.
                DataRow[] dr;

                if (_flexH[_flexH.Rows.Fixed, "S"].ToString() == "N" && _flexL[_flexL.Rows.Fixed, "S"].ToString() == "Y") // 만일 체크된 row들을 해제 할 경우
                {
                    for (int k = _flexH.Rows.Fixed; k <= _flexH.Rows.Count - 1; k++)
                    {
                        _flexH.Row = k;  // Row가 순차적으로 변경되면서 RowChange() 이벤트를 자동 실행하게 유도한다.

                        dr = _flexL.DataTable.Select("NO_IO = '" + _flexH[k, "NO_IO"].ToString() + "'", "", DataViewRowState.CurrentRows);  // 라인 자동선택위함.

                        for (int j = _flexL.Rows.Fixed; j < dr.Length + _flexL.Rows.Fixed; j++)
                            _flexL.SetCellCheck(j, 1, CheckEnum.Unchecked);
                    }
                    return;
                }

                for (int i = _flexH.Rows.Fixed; i <= _flexH.Rows.Count - 1; i++)
                {
                    _flexH.Row = i;

                    dr = _flexL.DataTable.Select("NO_IO = '" + _flexH[i, "NO_IO"].ToString() + "'", "", DataViewRowState.CurrentRows);

                    for (int j = _flexL.Rows.Fixed; j < dr.Length + _flexL.Rows.Fixed; j++)
                        _flexL.SetCellCheck(j, 1, CheckEnum.Checked);
                }
            }
            catch (Exception ex)
            {
                MsgControl.CloseMsg();
                bCheckMsg = false;
                MsgEnd(ex);
            }
            finally
            {
                bCheckMsg = false;
                MsgControl.CloseMsg();
            }
        }

        #endregion

        #region -> _flex_StartEdit

        void _flex_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                DataRow[] dr = _flexL.DataTable.Select("NO_IO = '" + _flexH[e.Row, "NO_IO"].ToString() + "'", "", DataViewRowState.CurrentRows);

                if (_flexH[e.Row, "S"].ToString() == "N") //클릭하는 순간은 N이므로
                {
                    for (int i = _flexL.Rows.Fixed; i <= dr.Length + 1; i++)
                        _flexL.SetCellCheck(i, 1, CheckEnum.Checked);
                }
                else
                {
                    for (int i = _flexL.Rows.Fixed; i <= dr.Length + 1; i++)
                        _flexL.SetCellCheck(i, 1, CheckEnum.Unchecked);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> _flex_AfterRowChange

        #region 첨부파일 업로드 / 다운로드

        #region 첨부파일 업로드
        public void _flexH_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (_flexH.Cols[_flexH.Col].Name == "FILE_PATH_MNG")
                {
                    OpenFileDialog dlg = new OpenFileDialog();

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        if (!dlg.SafeFileName.Contains(_flexH["NO_IO"].ToString()))
                        {
                            //MessageBox.Show("첨부하려고하는 파일명과 수불번호가 일치하여야 합니다.");
                            ShowMessage("첨부하려고하는 파일명과 수불번호가 일치하여야 합니다.");
                            return;
                        }

                        // 서버에 파일을 업로드 한다.
                        string serveraddress = MainFrameInterface.HostURL;     //http://202.167.215.108/ERP-U
                        string filename = dlg.SafeFileName;                             //bcl.log
                        string localpath = dlg.FileName;                                //C:\bcl.log
                        string serverdir = "/shared/MF_File_Mng/" + Global.MainFrame.LoginInfo.CompanyCode + "/GI"; //shared/MF_File_Mng/1130/GI
                        string action = "etc";
                        string date = "20090819";

                        ComFunc.UpLoad(serveraddress, filename, localpath, serverdir, action, date);

                        // DB 에 파일 이름을 저장할 수 있게 한다. 
                        _flexH["FILE_PATH_MNG"] = dlg.SafeFileName;

                        _flexH.AcceptChanges();

                        SaveData();
                        //MessageBox.Show("업로드완료");
                        ShowMessage("업로드완료");
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);

            }
        }
        #endregion

        #region 첨부파일 다운로드
        public void File_Download(object sender, EventArgs e)
        {
            try
            {
                if (_flexH["FILE_PATH_MNG"].ToString() == string.Empty || _flexH.Cols[_flexH.Col].Name != "FILE_PATH_MNG")
                    return;

                string localPath = string.Empty;
                string serverPath = string.Empty;

                FolderBrowserDialog fb_dlg = new FolderBrowserDialog();
                DialogResult result = fb_dlg.ShowDialog();

                //C:\Documents and Settings\Administrator\바탕 화면\1\GIZ20090800019.zip
                if (result == DialogResult.OK) //확인 버튼이 눌렸을 때 작동할 코드
                {
                    localPath = fb_dlg.SelectedPath + "\\" + _flexH["FILE_PATH_MNG"].ToString();

                    string serveraddress = MainFrameInterface.HostURL;     //http://202.167.215.108/ERP-U
                    string serverdir = "/shared/MF_File_Mng/" + Global.MainFrame.LoginInfo.CompanyCode + "/GI";

                    //http://202.167.215.108/ERP-U/shared/MF_File_Mng/GIZ20090800019.zip
                    serverPath = serveraddress + serverdir + "\\" + _flexH["FILE_PATH_MNG"].ToString();

                    ComFunc.DownLoad(serverPath, localPath);
                    //MessageBox.Show("다운로드완료");
                    ShowMessage("다운로드완료");
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region 첨부파일 삭제
        public void File_Delete(object sender, EventArgs e)
        {
            try
            {
                if (_flexH["FILE_PATH_MNG"].ToString() == string.Empty || _flexH.Cols[_flexH.Col].Name != "FILE_PATH_MNG")
                    return;

                if (Global.MainFrame.ShowMessage("첨부파일을 삭제하시겠습니까?", "QY2") == DialogResult.Yes)
                {

                    // 서버에 파일을 업로드 한다.
                    string hosturl = MainFrameInterface.HostURL;
                    string serverdir = "/shared/MF_File_Mng/" + Global.MainFrame.LoginInfo.CompanyCode + "/GI";
                    string filename = _flexH["FILE_PATH_MNG"].ToString();

                    ComFunc.DeleteFile(hosturl, serverdir, filename);
                    //MessageBox.Show("파일 삭제 완료");
                    ShowMessage("파일 삭제 완료");

                    // DB 에 파일 이름을 삭제할 수 있게 한다. 
                    _flexH["FILE_PATH_MNG"] = string.Empty;
                    SaveData();
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #endregion

        #region -> _flexH_BeforeRowChange

        void _flexH_BeforeRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (_flexL.DataTable != null)
                {
                    if (_flexL.DataTable.Select(_flexL.RowFilter, "", DataViewRowState.Deleted).Length > 0)
                    {
                        ShowMessage("삭제된 자료가 있습니다. 행변경 이전에 저장해야 합니다.");
                        e.Cancel = true;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> _flex_AfterRowChange

        void _flex_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                DataTable dt = null;
                string Key = _flexH[e.NewRange.r1, "NO_IO"].ToString();
                string Filter = "NO_IO = '" + Key + "'";
                if (_flexH.DetailQueryNeed)
                {
                    if (!bCheckMsg) MsgControl.ShowMsg("자료를 조회 중 입니다.");
                    object[] obj = new object[]
                    {
                        LoginInfo.CompanyCode, 
                        pp출하일.StartDateToString, 
                        pp출하일.EndDateToString, 
                        pp납기일.StartDateToString, 
                        pp납기일.EndDateToString, 
                        D.GetString(cbo공장.SelectedValue), 
                        D.GetString(bp거래처.CodeValue), 
                        D.GetString(bp담당자.CodeValue), 
                        D.GetString(cbo거래구분.SelectedValue), 
                        D.GetString(cbo출하구분.SelectedValue), 
                        D.GetString(bp출하형태.CodeValue), 
                        D.GetString(bp창고.CodeValue),
                        bpc영업조직.QueryWhereIn_Pipe,
                        bpc영업그룹.QueryWhereIn_Pipe,
                        D.GetString(bp프로젝트.CodeValue), 
                        D.GetString(cbo거래처그룹.SelectedValue), 
                        D.GetString(cbo거래처그룹2.SelectedValue), 
                        D.GetString(cbo사용자정의.SelectedValue), 
                        D.GetString(cbo사용자정의2.SelectedValue), 
                        Key,
                        txt수주번호.Text
                    };

                    dt = _biz.SearchDetail(obj);

                    if (!bCheckMsg) MsgControl.CloseMsg();
                }

                _flexL.BindingAdd(dt, Filter);
                _flexL.SetDummyColumn("S");
                _flexH.DetailQueryNeed = false;

                ToolBarSaveButtonEnabled = true;
            }
            catch (Exception ex)
            {
                if (!bCheckMsg) MsgControl.CloseMsg();
                MsgEnd(ex);
            }
            finally
            {
                if (!bCheckMsg) MsgControl.CloseMsg();
            }
        }

        #endregion

        #endregion

        #region -> _flexH_CellContentChanged

        void _flexH_CellContentChanged(object sender, CellContentEventArgs e)
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

        #endregion

        #endregion

        #region ★ 기타 이벤트

        #region -> Page_DataChanged

        void Page_DataChanged(object sender, EventArgs e)
        {
            if (_flexH.HasNormalRow)
            {
                if (_flexL.HasNormalRow)
                    ToolBarDeleteButtonEnabled = true;
                else
                    ToolBarDeleteButtonEnabled = false;
            }

            ToolBarAddButtonEnabled = false;
        }

        #endregion

        #region -> IsChanged

        protected override bool IsChanged()
        {
            //if (base.IsChanged()) return false;

            DataTable dt = _flexH.GetChanges();

            if (dt != null && dt.Rows[0].RowState != DataRowState.Deleted && (dt.Rows[0]["NO_IO"].ToString() != "" || dt.Rows[0]["NO_IO"].ToString() != string.Empty))
            {
                return true;
            }

            DataTable dtL = _flexL.GetChanges(DataRowState.Deleted);
            if (dtL != null && dtL.Rows.Count > 0)
            {
                return true;
            }

            return false;
        }

        #endregion

        #region -> Control_QueryBefore

        private void Control_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            switch (e.HelpID)
            {
                case Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB:
                    //e.HelpParam.P09_CD_PLANT = cb_cd_plant.SelectedValue.ToString();
                    e.HelpParam.P09_CD_PLANT = cbo공장.SelectedValue == null ? string.Empty : cbo공장.SelectedValue.ToString();
                    break;
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

        #endregion

        #region -> 콤보박스 키이벤트

        private void CommonComboBox_KeyEvent(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                System.Windows.Forms.SendKeys.SendWait("{TAB}");
        }

        #endregion

        #region -> _flexL_DoubleClick

        private void _flexL_DoubleClick(object sender, EventArgs e)
        {
            if (App.SystemEnv.SERIAL사용)
            {
                if (((FlexGrid)sender).HelpColName == "QT_IO")
                {
                    if (_flexL[_flexL.Row, "FG_MNG"].ToString().Trim() == "S/N")
                    {
                        P_SA_SERIAL_SUB m_dlg = new P_SA_SERIAL_SUB(_flexL[_flexL.Row, "NO_IO"].ToString(), _flexL.CDecimal(_flexL[_flexL.Row, "NO_IOLINE"].ToString()), _flexL[_flexL.Row, "NM_ITEM"].ToString(), _flexL.CDecimal(_flexL[_flexL.Row, "QT_IO"].ToString()), _flexL[_flexL.Row, "CD_ITEM"].ToString(), _flexL[_flexL.Row, "CD_QTIOTP"].ToString(), _flexL[_flexL.Row, "FG_IO"].ToString());
                        m_dlg.ShowDialog(this);
                    }
                }
            }
        }
        #endregion

        #region -> 시리얼등록

        private void btn_Serial_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flexL.HasNormalRow) return;

                DataTable dt_SERL = _flexL.DataTable.Clone();
                DataRow[] drs = _flexL.DataView.ToTable().Select("FG_MNG = 'S/N'");

                if (drs.Length == 0)
                {
                    ShowMessage(공통메세지._이가존재하지않습니다, DD("시리얼사용품목"));
                    return;
                }

                foreach (DataRow dr in drs)
                    dt_SERL.ImportRow(dr);

                pur.P_PU_SERL_SUB_I m_dlg = new pur.P_PU_SERL_SUB_I(dt_SERL);

                if (m_dlg.ShowDialog(this) == DialogResult.OK)
                    dt_SERL = m_dlg.dtL;
                else
                    return;

                if (dt_SERL == null) return;

                bool bSuccess = _biz.Save_Serial(dt_SERL);

                if (!bSuccess)
                    return;
                else
                    ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> btn_시리얼출력_Click

        private void btn_시리얼출력_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flexL.HasNormalRow) return;

                DataRow[] drs = _flexL.DataTable.Select("S = 'Y'");
                string Multi_NO_IO = string.Empty;

                if (drs == null || drs.Length == 0)
                {
                    ShowMessage("하단그리드에 선택된 자료가 없습니다.");
                    return;
                }

                foreach (DataRow row in drs)
                    Multi_NO_IO += row["NO_IO"] + "|";

                DataTable dt = _biz.Search_Print_Barcode(new object[] { MA.Login.회사코드, Multi_NO_IO });

                DataTable dt시리얼 = dt.Clone();

                foreach (DataRow row in drs)
                {
                    string filter = "NO_IO = '" + D.GetString(row["NO_IO"]) + "' AND NO_IOLINE = " + D.GetString(row["NO_IOLINE"]);
                    DataRow[] dr = dt.Select(filter);
                    foreach (DataRow row2 in dr)
                    {
                        dt시리얼.ImportRow(row2);
                    }
                }

                if (dt시리얼 == null || dt시리얼.Rows.Count == 0)
                {
                    ShowMessage("선택된 출고건[" + Multi_NO_IO + "]에 대한 시리얼이 존재하지 않습니다.");
                    return;
                }

                MSCommLib.MSCommClass com = new MSCommLib.MSCommClass();
                ASCIIEncoding ascii = new ASCIIEncoding();

                string 시리얼번호 = "";
                int li_max = 시리얼번호.Length;
                int li_seq = 0, ll_len = 0, ll_start = 0, ll_tot = 0;
                long ll_pos_temp = 0;
                string ls_type_pre = "", ls_type_temp, 품목명 = "", ls_split_nm_item = "";
                char ls_char;
                List<object> ll_pos = new List<object>();
                List<object> ls_type2 = new List<object>();
                List<object> ls_type = new List<object>();

                com.CommPort = 1;//시리얼 포트 지정
                com.Settings = "9600,n,8,1"; //시리얼 통신값 지정
                com.PortOpen = true;

                foreach (DataRow drSERL in dt시리얼.Rows)
                {
                    시리얼번호 = drSERL["NO_SERIAL"].ToString();
                    품목명 = drSERL["NM_ITEM"].ToString();

                    li_seq = 0;
                    ll_len = 0;
                    ll_start = 0;
                    ll_pos.Clear();
                    ls_type2.Clear();
                    ls_type.Clear();

                    li_max = 품목명.Length;
                    com.Output = "{C|}";       //버퍼삭제

                    for (int i = 0; i < li_max; i++)
                    {
                        ls_char = Convert.ToChar(품목명.Substring(i, 1));
                        //byte[] ascii_byte = Encoding.ASCII.GetBytes(ls_char);
                        if (Convert.ToInt32(ls_char) > 0 && Convert.ToInt32(ls_char) < 128)
                            ls_type.Add("N");
                        else // UNICODE
                            ls_type.Add("Y");
                    }

                    for (int i = 0; i < li_max; i++)
                    {
                        if (i == 0)
                            ls_type_pre = ls_type[i].ToString();

                        ls_type_temp = ls_type[i].ToString();

                        if (ls_type_pre == ls_type_temp)
                        {
                            ll_len++;
                            if (i == li_max - 1)
                            {
                                li_seq++;
                                ll_pos.Add(ll_len);
                                ls_type2.Add(ls_type_temp);
                            }
                        }
                        else
                        {
                            li_seq++;
                            ll_pos.Add(ll_len);
                            ls_type2.Add(ls_type_pre);
                            ll_len = 1;
                        }
                        ls_type_pre = ls_type[i].ToString();
                    }


                    com.Output = "{D0130,0550,0100|}"; //라벨싸이즈 갭+세로,가로,세로	

                    for (int i = 0; i < li_seq; i++)
                    {
                        ls_type_temp = ls_type2[i].ToString();
                        ll_len = Convert.ToInt32(ll_pos[i]);

                        if (i > 0)
                            ll_start = ll_tot;
                        else
                        {
                            ll_pos_temp = 60;
                            ll_tot = 0;
                        }
                        ls_split_nm_item = 품목명.Substring(ll_start, ll_len);


                        if (ls_type_temp == "N")
                        {
                            com.Output = "{PV04;" + ll_pos_temp.ToString("0000") + ",0020,0020,0020,A,-006,00,B,+0000000000|}"; //글자 찍기 위치밑 크기
                            com.Output = "{RV04;" + ls_split_nm_item + "|}"; //글자 내용
                        }
                        else
                        {
                            com.Output = "{PC001;" + ll_pos_temp.ToString("0000") + ",0025,10,20,51,+00,00,B|}";  //한글
                            com.Output = "{RC001;" + ls_split_nm_item + "|}"; //글자 내용	
                        }
                        ll_tot = ll_len + ll_tot;
                        if (ls_type_temp == "N")
                            ll_pos_temp = ll_pos_temp + (ll_len) * 15;
                        else
                            ll_pos_temp = ll_pos_temp + (ll_len / 2) * 15;
                    }
                    com.Output = "{PV04;0060,0050,0020,0020,A,-006,00,B,+0000000000|}"; //글자 찍기 위치밑 크기
                    com.Output = "{RV04;" + 시리얼번호 + "|}"; //글자 내용			   
                    com.Output = "{XB01;0040,0055,C,1,02,0,0045,+0000000000,000,0,01|}";
                    com.Output = "{RB01;" + 시리얼번호 + "|}"; //바코드 내용	
                    com.Output = "{XS;I," + "0001" + ",0002C5101|}"; //출력 수량
                }
                com.PortOpen = false;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 서버키 따른 컨트롤 셋팅
        private void 서버키enable()
        {
            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "MDSTEC") //MDS테크놀로지
            {
                btn_시리얼출력.Visible = true;
            }
        }
        #endregion

        #region ->  bpc영업그룹_QueryBefore

        void bpc영업그룹_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                if (!bpc영업조직.IsEmpty())
                {
                    e.HelpParam.P61_CODE1 = bpc영업조직.QueryWhereIn_Pipe;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region ->  bpc영업조직_QueryAfter

        void bpc영업조직_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                bpc영업그룹.Clear();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region -> btn삭제_Click

        private void btn삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flexL.HasNormalRow)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                string strFilter = _flexL.RowFilter + " AND S = 'Y' ";
                DataRow[] drs = _flexL.DataTable.Select(strFilter, "", DataViewRowState.CurrentRows);

                if (drs.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                foreach (DataRow dr in drs)
                {
                    dr.Delete();
                }

                _flexL.SumRefresh();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region ★ 속성
        #region -> Chk출하일
        bool Chk출하일 { get { return Checker.IsValid(pp출하일, true, lbl출하일.Text); } }
        #endregion
        #endregion
    }
}
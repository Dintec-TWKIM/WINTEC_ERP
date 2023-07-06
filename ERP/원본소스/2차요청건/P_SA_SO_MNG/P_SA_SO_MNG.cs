using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;
using Duzon.Windows.Print;

namespace sale
{
    // **************************************
    // 작   성   자 : 허성철
    // 재 작  성 일 : 2007-01-30
    // 모   듈   명 : 영업
    // 시 스  템 명 : 영업관리
    // 서브시스템명 : 수주관리
    // 페 이 지  명 : 수주관리
    // 프로젝트  명 : P_SA_SO_MNG
    // **************************************
    // Change List
    // 2009.12.06 - 안종호 - 비고컬럼추가
    // 2010.05.04 - 안종호 - 이전변경자, 변경자 컬럼추가,AM_WONAMT + AM_VAT 합계추가 합계추가
    // 2013.02.07 - 오준회 - 비고1컬럼추가
    // **************************************
    public partial class P_SA_SO_MNG : PageBase
    {
        #region ★ 멤버필드

        private P_SA_SO_MNG_BIZ _biz = new P_SA_SO_MNG_BIZ();
        bool yn_confirm = false;
        string 버튼유형 = string.Empty;

        string 공장output = string.Empty;
        string 수주일자output = string.Empty;
        string 수주번호output = string.Empty;


        #endregion

        #region ★ 초기화

        #region -> 생성자
        public P_SA_SO_MNG()
        {
            try
            {
                InitializeComponent();

                //이렇게 해주면 위에 툴바가 자동으로 움직여브러유~~~케헤헤헤
                MainGrids = new FlexGrid[] { _flexL, _flexH };

                //DetailQueryNeed 이거 사용 하려면 ~ 여기서 요거 셋팅해줘야 함~
                _flexH.DetailGrids = new FlexGrid[] { _flexL };

                this.DataChanged += new EventHandler(Page_DataChanged);


            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public P_SA_SO_MNG(string 공장, string 수주일자, string 수주번호)
        {
            try
            {
                InitializeComponent();

                //이렇게 해주면 위에 툴바가 자동으로 움직여브러유~~~케헤헤헤
                MainGrids = new FlexGrid[] { _flexL, _flexH };

                //DetailQueryNeed 이거 사용 하려면 ~ 여기서 요거 셋팅해줘야 함~
                _flexH.DetailGrids = new FlexGrid[] { _flexL };

                this.DataChanged += new EventHandler(Page_DataChanged);

                공장output = 공장;
                수주일자output = 수주일자;
                수주번호output = 수주번호;

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnCallExistingPageMethod(object sender, Duzon.Common.Forms.PageEventArgs e)
        {
            object[] obj = e.Args;

            this.공장output = D.GetString(obj[0]);
            this.수주일자output = D.GetString(obj[1]);
            this.수주번호output = D.GetString(obj[2]);

            InitPaint();
        }
        #endregion

        #region -> InitLoad

        protected override void InitLoad()
        {
            base.InitLoad();
            InitGridH();
            InitGridL();
            InitEvent();
        }

        #endregion

        #region -> InitGridH
        private void InitGridH()
        {
            _flexH.BeginSetting(1, 1, false);
            _flexH.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flexH.SetCol("NO_SO", "수주번호", 120);
            _flexH.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexH.SetCol("CD_PARTNER", "거래처코드", 100);
            _flexH.SetCol("LN_PARTNER", "거래처명", 120);
            _flexH.SetCol("NM_SO", "수주형태", 100);
            _flexH.SetCol("CD_EXCH", "환종", 80);
            _flexH.SetCol("RT_EXCH", "환율", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            _flexH.SetCol("NM_SALEORG", "영업조직", 100);
            _flexH.SetCol("NM_SALEGRP", "영업그룹", 100);
            _flexH.SetCol("NM_KOR", "수주담당자", 80);
            _flexH.SetCol("NO_PROJECT", "프로젝트코드", 120);
            _flexH.SetCol("NM_PROJECT", "프로젝트명", 120);
            _flexH.SetCol("QT_SO", "수량", 65, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexH.SetCol("AM_SO", "수주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flexH.SetCol("AM_WONAMT", "수주원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flexH.SetCol("AM_VAT", "부가세", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flexH.SetCol("AMVAT_SO", "합계금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flexH.SetCol("DC_RMK", "비고", 120, true);
            _flexH.SetCol("NO_PO_PARTNER", "거래처PO번호", false);
            _flexH.SetCol("DTS_INSERT", "입력일시", 150, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexH.SetCol("DC_RMK1", "비고1", 120, false);

            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "ICONIX")
            {
                _flexH.SetCol("AM_CREDIT", "여신금액", 100, false, typeof(decimal), FormatTpType.MONEY); _flexH.Cols["AM_CREDIT"].Visible = false;
                _flexH.SetCol("AM_PRE_CREDIT", "예상여신금액", 100, false, typeof(decimal), FormatTpType.MONEY); _flexH.Cols["AM_PRE_CREDIT"].Visible = false;
            }
            else if (Global.MainFrame.ServerKeyCommon.ToUpper() == "SONYENT")
            {
                _flexH.SetCol("AM_CREDIT_JAN", "여신잔액", 90, false, typeof(decimal), FormatTpType.MONEY);
            }

            _flexH.SetCol("CD_USERDEF1", "CODE사용자정의1", 100, false); _flexH.Cols["CD_USERDEF1"].Visible = false;

            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "TRIGEM")
            {
                _flexH.SetCol("ST_STAT", "결재상태", 100, false); _flexH.Cols["ST_STAT"].Visible = false;
            }
            
            if (MA.ServerKey(false, new string[] { "GALAXIA", "WONIK" }))
            {
                _flexH.SetCol("ST_STAT", "전자결재상태", 100, false);
            }

            _flexH.SetCol("NM_ID_INSERT", "입력자", 80);

            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "MHIK")  //엠에이치파워시스템즈코리아
                _flexH.SetCol("TXT_USERDEF4", "전용수주번호", 150, false);

            _flexH.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";
            _flexH.ExtendLastCol = true;
            _flexH.SetDummyColumn("S", "MEMO_CD", "CHECK_PEN");
            _flexH.SettingVersion = "1.1.1.4";
            _flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            _flexH.SetExceptSumCol("RT_EXCH");
            _flexH.StartEdit += new RowColEventHandler(_flexH_StartEdit);
            _flexH.CheckHeaderClick += new EventHandler(_flexH_CheckHeaderClick);
            _flexH.AfterRowChange += new RangeEventHandler(_flexH_AfterRowChange);
            _flexH.HelpClick += new EventHandler(_flexH_HelpClick);

            //Memo기능 관련 설정
            _flexH.CellNoteInfo.EnabledCellNote = true;
            _flexH.CellNoteInfo.CategoryID = this.Name;
            _flexH.CellNoteInfo.DisplayColumnForDefaultNote = "NO_SO";

            //CheckPen기능 관련 설정
            _flexH.CheckPenInfo.EnabledCheckPen = true;

            _flexH.CellContentChanged += new CellContentEventHandler(_flexH_CellContentChanged);
        }

        #endregion

        #region -> InitGridL
        private void InitGridL()
        {
            _flexL.BeginSetting(1, 1, false);
            _flexL.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);   //   -> 팅크웨어 업무상 라인 선택 불필요함으로 일괄삭제하게끔 작업됨...
            _flexL.SetCol("CD_PLANT", "공장", 100);
            _flexL.SetCol("CD_ITEM", "품목코드", 100);
            _flexL.SetCol("NM_ITEM", "품목명", 120);
            if(Global.MainFrame.ServerKeyCommon == "WISE")
            _flexL.SetCol("EN_ITEM", "품목명(영)", 120);

            _flexL.SetCol("STND_ITEM", "규격", 65);
            _flexL.SetCol("STND_DETAIL_ITEM", "세부규격", 65);
            _flexL.SetCol("UNIT_SO", "단위", 65);
            _flexL.SetCol("DT_DUEDATE", "납기요구일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexL.SetCol("DT_REQGI", "출하예정일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexL.SetCol("QT_SO", "수량", 65, false, typeof(decimal), FormatTpType.QUANTITY);

            string disCount_YN = BASIC.GetSAENV("002");

            if (disCount_YN == "Y")
            {
                _flexL.SetCol("UM_BASE", "기준단가", 100, 15, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                _flexL.SetCol("RT_DSCNT", "할인율", 100, 15, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            }

            _flexL.SetCol("UM_SO", "단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flexL.SetCol("AM_VAT", "부가세", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flexL.SetCol("UMVAT_SO", "단가(부가세포함)", 150, false, typeof(decimal), FormatTpType.MONEY);
            _flexL.Cols["UMVAT_SO"].Visible = false;
            _flexL.SetCol("AM_SO", "금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flexL.SetCol("AM_WONAMT", "원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flexL.SetCol("TP_BUSI", "거래구분", 80, false);
            _flexL.SetCol("STA_SO", "수주상태", 60, false);
            _flexL.SetCol("NM_SL", "창고", 120, false);
            _flexL.SetCol("UNIT_IM", "관리단위", 65, false);
            _flexL.SetCol("QT_IM", "관리수량", 65, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("GI_PARTNER", "납품처", 100, false);
            _flexL.SetCol("NM_GI_PARTNER", "납품처명", 120, false);
            _flexL.SetCol("CD_ITEM_PARTNER", "거래처품번", 100, false);  //20090317 추가
            _flexL.SetCol("NM_ITEM_PARTNER", "거래처품명", 100, false);  //20090317 추가
            _flexL.SetCol("DC1", "비고1", 100, true);
            _flexL.SetCol("DC2", "비고2", 100, true);
            _flexL.SetCol("QT_INV", "현재고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("NM_ID_STA_HST", "이전변경자", 100, false);
            _flexL.SetCol("DTS_STA_HST", "이전변경일", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexL.SetCol("NM_ID_UPDATE", "변경자", 100, false);
            _flexL.SetCol("DTS_UPDATE", "변경일", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexL.SetCol("AM_SUM", "합계금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flexL.SetCol("NO_PO_PARTNER", "거래처PO번호", 140, false);
            _flexL.SetCol("NO_POLINE_PARTNER", "거래처PO항번", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("CD_EXCH", "환종", 0, false);
            _flexL.SetCol("FG_USE", "수주용도", 100, true);
            _flexL.SetCol("FG_USE2", "수주용도2", 100, true);
            _flexL.SetCol("NO_DESIGN", "도면번호", false);
            _flexL.SetCol("NUM_USERDEF1", "사용자정의1", 80, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST); _flexL.Cols["NUM_USERDEF1"].Visible = false;
            _flexL.SetCol("NUM_USERDEF2", "사용자정의2", 80, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST); _flexL.Cols["NUM_USERDEF2"].Visible = false;
            _flexL.SetCol("NUM_USERDEF3", "사용자정의3", 80, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST); _flexL.Cols["NUM_USERDEF3"].Visible = false;
            _flexL.SetCol("NUM_USERDEF4", "사용자정의4", 80, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST); _flexL.Cols["NUM_USERDEF4"].Visible = false;
            _flexL.SetCol("NUM_USERDEF5", "사용자정의5", 80, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST); _flexL.Cols["NUM_USERDEF5"].Visible = false;
            _flexL.SetCol("NUM_USERDEF6", "사용자정의6", 80, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST); _flexL.Cols["NUM_USERDEF6"].Visible = false;
            _flexL.SetCol("TXT_USERDEF1", "TEXT사용자정의1", 150, true); _flexL.Cols["TXT_USERDEF1"].Visible = false;
            _flexL.SetCol("TXT_USERDEF2", "TEXT사용자정의2", 150, true); _flexL.Cols["TXT_USERDEF2"].Visible = false;
           
            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "SONYENT")
            {
                _flexL.SetCol("QT_USEINV", "주문가능량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
                _flexL.SetCol("UM_FIXED", "기준단가", 90, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            }

            _flexL.SetCol("MAT_ITEM", "재질", false);
            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "SIMMONS")
            {
                _flexL.SetCol("ST_PROC", "배차확정여부", 100);
            }

            _flexL.SetCol("CLS_ITEM", "품목계정", false);

            _flexL.SetCol("NO_PROJECT", "프로젝트코드", 120);
            _flexL.SetCol("NM_PROJECT", "프로젝트명", 120);

            _flexL.SetCol("CD_ITEMGRP", "품목군코드", false);
            _flexL.SetCol("NM_ITEMGRP", "품목군명", false);
            _flexL.SetCol("GRP_MFG", "제품군코드", false);
            _flexL.SetCol("NM_GRP_MFG", "제품군명", false);
            _flexL.SetCol("NO_RELATION", "연동번호", false);
            _flexL.SetCol("SEQ_RELATION", "연동항번", false);

            _flexL.SetDummyColumn("S");
            _flexL.SettingVersion = "1.1.1.9";
            _flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            //헤더에 필요없는 SUM 제거 && 반듯이 EndSetting 다음에 코딩해줘야한다.
            _flexL.SetExceptSumCol("UM_SO", "UMVAT_SO", "NO_POLINE_PARTNER");

            _flexL.StartEdit += new RowColEventHandler(_flexL_StartEdit);
        }
        #endregion

        #region -> InitPaint

        protected override void InitPaint()
        {
            base.InitPaint();

            oneGrid1.UseCustomLayout = true;
            bpPanelControl1.IsNecessaryCondition = true;
            bpPanelControl2.IsNecessaryCondition = true;
            oneGrid1.InitCustomLayout();

            //m_mskDtStart.Mask = GetFormatDescription(DataDictionaryTypes.FI, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT);
            //m_mskDtStart.ToDayDate = Global.MainFrame.GetDateTimeToday();
            //m_mskDtStart.Text = Global.MainFrame.GetStringFirstDayInMonth;

            //m_mskDtEnd.Mask = GetFormatDescription(DataDictionaryTypes.FI, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT);
            //m_mskDtEnd.ToDayDate = Global.MainFrame.GetDateTimeToday();
            //m_mskDtEnd.Text = Global.MainFrame.GetStringToday;

            periodPicker1.StartDateToString = Global.MainFrame.GetStringFirstDayInMonth;
            periodPicker1.EndDateToString = Global.MainFrame.GetStringToday;

            Page_DataChanged(null, null);

            //Combo Box 셋팅시 옵션 => N:공백없음, S:공백추가, SC:공백&괄호추가, NC:괄호추가
            DataSet g_dsCombo = this.GetComboData("N;MA_PLANT", "S;PU_C000016", "S;SA_B000016", "S;MA_B000005", "S;MA_B000065", "S;MA_B000067");

            // 공장
            m_cboPlant.DataSource = g_dsCombo.Tables[0];
            m_cboPlant.DisplayMember = "NAME";
            m_cboPlant.ValueMember = "CODE";

            if (g_dsCombo.Tables[0] != null && g_dsCombo.Tables[0].Rows.Count > 0)
            {
                if (Global.MainFrame.ServerKeyCommon == "NICEGR")
                    m_cboPlant.SelectedValue = Global.MainFrame.LoginInfo.CdPlant;
                else
                    m_cboPlant.SelectedIndex = 0;
            }
            // 거래구분
            m_cboBusi.DataSource = g_dsCombo.Tables[1];
            m_cboBusi.DisplayMember = "NAME";
            m_cboBusi.ValueMember = "CODE";

            // 수주상태
            m_cboStaSo.DataSource = g_dsCombo.Tables[2];
            m_cboStaSo.DisplayMember = "NAME";
            m_cboStaSo.ValueMember = "CODE";

            SetControl str = new SetControl();
            str.SetCombobox(cbo거래처그룹, MA.GetCode("MA_B000065", true));
            str.SetCombobox(cbo거래처그룹2, MA.GetCode("MA_B000067", true));
            str.SetCombobox(cbo전자결재상태, MA.GetCode("SA_B000060", true));
            str.SetCombobox(cbo배차확정여부, MA.GetCodeUser(new string[] { "R", "O" }, new string[] { "확정", "미확정" }, true));

            //수주일자, 납기일자 콤보박스를 위한 데이터 테이블 생성
            DataTable dt = g_dsCombo.Tables[0].Clone();

            DataRow dr = dt.NewRow();
            dr["CODE"] = "SO";
            dr["NAME"] = DD("수주일자");
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["CODE"] = "DU";
            dr["NAME"] = DD("납기일자");
            dt.Rows.Add(dr);

            cbo_Dt_So.DataSource = dt;
            cbo_Dt_So.DisplayMember = "NAME";
            cbo_Dt_So.ValueMember = "CODE";

            _flexH.SetDataMap("CD_EXCH", g_dsCombo.Tables[3], "CODE", "NAME");  //환종
            _flexL.SetDataMap("CD_PLANT", g_dsCombo.Tables[0], "CODE", "NAME"); //공장
            _flexL.SetDataMap("TP_BUSI", g_dsCombo.Tables[1], "CODE", "NAME");  //거래구분
            _flexL.SetDataMap("STA_SO", g_dsCombo.Tables[2], "CODE", "NAME");   //수주상태
            _flexL.SetDataMap("FG_USE", MA.GetCode("SA_B000057", true), "CODE", "NAME");
            _flexL.SetDataMap("FG_USE2", MA.GetCode("SA_B000063", true), "CODE", "NAME");
            _flexH.SetDataMap("CD_USERDEF1", MA.GetCode("SA_B000111"), "CODE", "NAME"); // 사용자정의CODE1
            _flexL.SetDataMap("CLS_ITEM", MA.GetCode("MA_B000010"), "CODE", "NAME");


            string 권한 = Global.MainFrame.CurrentGrantMenu;
            if (권한 == "B" ||//사업장
                권한 == "S" ||//공장
                권한 == "C" ||//C/C
                권한 == "D" || //부서
                권한 == "E") //사원
                m_cboPlant.Enabled = false;

            if (_biz.GetATP사용여부 == "000")
                btn_ATP.Visible = false;

            사용자정의세팅();
            SettingControl();
        }

        #endregion

        #region -> InitEvent

        void InitEvent()
        {
            bpc영업조직.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(bpc영업조직_QueryAfter);
            bpc영업그룹.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(ctx영업그룹_QueryBefore);
            bp프로젝트.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(Control_QueryBefore);  // 2011.04.04 추가
            btn_ATP.Click += new EventHandler(btn_ATP_Click);
            bpcTpSo.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(Control_QueryBefore);

            btn삭제.Click += new EventHandler(btn삭제_Click);
        }

        #endregion

        #endregion

        #region ★ 메인버튼 클릭

        #region -> SaveData
        protected override bool SaveData()
        {
            if (!base.SaveData()) return false;

            DataTable dt_H = null;
            DataTable dt_L = null;

            dt_H = new DataTable();
            dt_H = _flexH.GetChanges();

            dt_L = new DataTable();
            dt_L = _flexL.GetChanges();

            //DataTable dtL = _flexL.GetChanges();

            if ((dt_H == null || dt_H.Rows.Count == 0) && (dt_L == null || dt_L.Rows.Count == 0))
            {
                ShowMessage(공통메세지.변경된내용이없습니다);
                return false;
            }

            bool bSuccess = _biz.Save(dt_H, dt_L, 버튼유형);
            if (!bSuccess) return false;

            _flexH.AcceptChanges();
            _flexL.AcceptChanges();
            버튼유형 = "";
            return true;
        }
        #endregion

        #region -> 조회버튼 클릭
        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSearch() || !chkDate) return;

                object[] obj = new object[21];

                obj[0] = LoginInfo.CompanyCode;
                obj[1] = D.GetString(m_cboPlant.SelectedValue);
                //obj[2] = m_mskDtStart.Text;
                //obj[3] = m_mskDtEnd.Text;
                obj[2] = periodPicker1.StartDateToString;
                obj[3] = periodPicker1.EndDateToString;
                obj[4] = bpc영업그룹.QueryWhereIn_Pipe;
                obj[5] = bpNoEmp.CodeValue;
                obj[6] = D.GetString(m_cboBusi.SelectedValue);
                obj[7] = bpCdPartner.CodeValue;
                obj[8] = bpcTpSo.QueryWhereIn_Pipe;
                obj[9] = D.GetString(m_cboStaSo.SelectedValue);
                obj[10] = bp프로젝트.CodeValue;  //2011.04.01 변경//string.Empty;
                obj[11] = string.Empty;
                obj[12] = string.Empty;
                obj[13] = D.GetString(cbo_Dt_So.SelectedValue);
                obj[14] = bpc영업조직.QueryWhereIn_Pipe;
                obj[15] = D.GetString(cbo거래처그룹.SelectedValue);
                obj[16] = D.GetString(cbo거래처그룹2.SelectedValue);
                obj[17] = MA.Login.사원번호;
                obj[18] = txt수주번호.Text;
                obj[19] = D.GetString(cbo전자결재상태.SelectedValue);
                obj[20] = D.GetString(cbo배차확정여부.SelectedValue);

                _flexH.Binding = _biz.Search(obj);

                if (!_flexH.HasNormalRow) ShowMessage(공통메세지.조건에해당하는내용이없습니다);
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
                if (!BeforeDelete()) return;

                string NO_SO = string.Empty;
                string MULTI_SO = string.Empty;

                DataRow[] drH = _flexH.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                if (drH == null || drH.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }


                //foreach (DataRow rowL in drL)            -> 트리거로 처리
                //{
                //    if (rowL["STA_SO"].ToString() != "O")
                //    {
                //        ShowMessage(메세지.이미수주확정되어수정삭제가불가능합니다);
                //        return;
                //    }
                //}

                _flexL.Redraw = false;

                foreach (DataRow rowH in drH)
                {
                    if (NO_SO != rowH["NO_SO"].ToString())
                    {
                        NO_SO = rowH["NO_SO"].ToString();
                        MULTI_SO = MULTI_SO + NO_SO + "|";
                    }
                }


                foreach (DataRow rowH in drH)
                {
                    rowH.Delete();
                }

                _flexL.Redraw = true;

                string[] Args = MULTI_SO.Split('|');

                DataRow[] drL1;

                _flexH.Redraw = false;

                for (int i = 0; i < Args.Length; i++)
                {
                    drL1 = _flexL.DataTable.Select("NO_SO = '" + Args[i] + "'", "", DataViewRowState.CurrentRows);

                    if (drL1 != null && drL1.Length > 0)
                    {
                        {
                            foreach (DataRow rowL in drL1)
                                rowL.Delete();
                        }
                    }
                }


                if (Global.MainFrame.ShowMessage("헤더와 라인 모두 일괄삭제됩니다. 삭제하시겠습니까?", "QY2") == DialogResult.Yes)
                {

                    bool bSuccess = _biz.Delete(MULTI_SO);
                    if (!bSuccess)
                        return;

                    _flexH.AcceptChanges();
                    _flexL.AcceptChanges();

                }

                _flexH.Redraw = true;

                OnToolBarSearchButtonClicked(null, null);
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
                    this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 인쇄버튼 클릭

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!_flexH.HasNormalRow) return;

                if (!BeforePrint()) return;

                DataRow[] drArr = _flexH.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                if (drArr.Length < 1)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                DataTable dt_Print = _biz.Search_Print("XXXXXXX");

                //
                // 수주번호를 파이프라인으로 묶어서 조회를 해도되지만,
                // 수주등록과 같은 SP 를 타게 하려고 아래와 같이 처리함.
                //
                foreach (DataRow row in drArr)
                {
                    DataTable dt = _biz.Search_Print(D.GetString(row["NO_SO"]));

                    foreach (DataRow dr in dt.Rows)
                        dt_Print.ImportRow(dr);
                }


                ReportHelper rptHelper = new ReportHelper("R_SA_SO_MNG", "수주서");

                rptHelper.SetData("공장코드", D.GetString(m_cboPlant.SelectedValue));
                rptHelper.SetData("공장명", D.GetString(m_cboPlant.Text));
                rptHelper.SetData("거래구분", D.GetString(m_cboBusi.SelectedValue));
                rptHelper.SetData("거래구분명", D.GetString(m_cboBusi.Text));
                rptHelper.SetData("거래처그룹", D.GetString(cbo거래처그룹.SelectedValue));
                rptHelper.SetData("거래처그룹명", D.GetString(cbo거래처그룹.Text));
                rptHelper.SetData("거래처그룹2", D.GetString(cbo거래처그룹2.SelectedValue));
                rptHelper.SetData("거래처그룹명2", D.GetString(cbo거래처그룹2.Text));
                rptHelper.SetData("수주상태", D.GetString(m_cboStaSo.SelectedValue));
                rptHelper.SetData("수주상태명", D.GetString(m_cboStaSo.Text));

                rptHelper.SetDataTable(dt_Print, 1);
                rptHelper.SetDataTable(dt_Print, 2);
                rptHelper.Print();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region ★ 그리드 이벤트

        #region -> _flexH_StartEdit
        void _flexH_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (_flexH.Cols[e.Col].Name != "S") return;

                DataRow[] dr = _flexL.DataTable.Select("NO_SO = '" + _flexH[e.Row, "NO_SO"].ToString() + "'", "", DataViewRowState.CurrentRows);

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

        #region -> _flexH_CheckHeaderClick
        void _flexH_CheckHeaderClick(object sender, EventArgs e)
        {
            if (!_flexH.HasNormalRow) return;

            Common.Setting set = new Common.Setting();

            #region -> 전제 체크해제하는 경우
            if (_flexH.GetCellCheck(_flexH.Row, _flexH.Cols["S"].Index) == CheckEnum.Unchecked)
            {
                set.GridCheck(_flexL, "S = 'Y'", false);
                return;
            }
            #endregion

            #region -> 전제 체크하는 경우
            string multiNoSo = string.Empty;

            for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
            {
                if (!_flexH.DetailQueryNeedByRow(i)) continue;

                multiNoSo += D.GetString(_flexH[i, "NO_SO"]) + "|";
                _flexH.SetDetailQueryNeedByRow(i, false);
            }

            if (multiNoSo == string.Empty)
            {
                set.GridCheck(_flexL, "S = 'N'", true);
                return;
            }

            List<string> list = new List<string>();
            list.Add(MA.Login.회사코드);
            list.Add(string.Empty);
            list.Add(D.GetString(m_cboPlant.SelectedValue));
            list.Add(D.GetString(m_cboBusi.SelectedValue));
            list.Add(D.GetString(m_cboStaSo.SelectedValue));
            list.Add(bp프로젝트.CodeValue);
            list.Add(string.Empty);
            //list.Add(m_mskDtStart.Text);
            //list.Add(m_mskDtEnd.Text);
            list.Add(periodPicker1.StartDateToString);
            list.Add(periodPicker1.EndDateToString);
            list.Add(D.GetString(cbo_Dt_So.SelectedValue));

            DataTable dt = _biz.SearchCheckHeader(multiNoSo, list.ToArray());

            string filter = "NO_SO = '" + D.GetString(_flexH["NO_SO"]) + "'";

            _flexL.BindingAdd(dt, filter);
            set.GridCheck(_flexL, "S = 'N'", true);
            #endregion

            #region -> Old Source
            /* 유의 : 클릭하자마자 헤더 그리드는 선택 상태가 변경된다(ex) Checked->UnChecked

            if(_flexH == null || _flexL == null) return;

            if(!_flexH.HasNormalRow || !_flexL.HasNormalRow) return;

            _flexH.Row = 1; // 맨처음row에 자동위치하도록 해야 틀어지지 않는다.
            DataRow[] dr;

            //_flexH.SetDetailQueryNeedByRow(int row);

            if(_flexH[_flexH.Rows.Fixed, "S"].ToString() == "N" && _flexL[_flexL.Rows.Fixed, "S"].ToString() == "Y") // 만일 체크된 row들을 해제 할 경우
            {
                for(int k = _flexH.Rows.Fixed;k <= _flexH.Rows.Count - 1;k++)
                {
                    _flexH.Row = k;  // Row가 순차적으로 변경되면서 RowChange() 이벤트를 자동 실행하게 유도한다.
                    
                    dr = _flexL.DataTable.Select("NO_SO = '" + _flexH[k, "NO_SO"].ToString() + "'", "", DataViewRowState.CurrentRows);  // 라인 자동선택위함.

                    for(int j = _flexL.Rows.Fixed;j <= dr.Length + 1;j++)  
                        _flexL.SetCellCheck(j, 1, CheckEnum.Unchecked);
                }
                return;
            }

            for(int i = _flexH.Rows.Fixed;i <= _flexH.Rows.Count - 1;i++)
            {
                _flexH.Row = i;

                dr = _flexL.DataTable.Select("NO_SO = '" + _flexH[i, "NO_SO"].ToString() + "'", "", DataViewRowState.CurrentRows);

                for(int j = _flexL.Rows.Fixed;j <= dr.Length + 1;j++)
                    _flexL.SetCellCheck(j, 1, CheckEnum.Checked);
            } */
            #endregion
        }
        #endregion

        #region -> _flexH_AfterRowChange
        void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                DataTable dt = null;

                string Key = _flexH[e.NewRange.r1, "NO_SO"].ToString();
                string Filter = "NO_SO = '" + Key + "'";

                if (_flexH.DetailQueryNeed)
                {
                    object[] obj = new object[10];

                    obj[0] = LoginInfo.CompanyCode;
                    obj[1] = Key;
                    obj[2] = m_cboPlant.SelectedValue == null ? string.Empty : m_cboPlant.SelectedValue.ToString();
                    obj[3] = m_cboBusi.SelectedValue == null ? string.Empty : m_cboBusi.SelectedValue.ToString();
                    obj[4] = m_cboStaSo.SelectedValue == null ? string.Empty : m_cboStaSo.SelectedValue.ToString();
                    obj[5] = bp프로젝트.CodeValue;  //2011.04.01 변경//string.Empty;
                    obj[6] = string.Empty;
                    //obj[7] = m_mskDtStart.Text;//수주일자 납기일자 조회시 라인도 함께 처리하기 위해 날짜를 받음
                    //obj[8] = m_mskDtEnd.Text;//수주일자 납기일자 조회시 라인도 함께 처리하기 위해 날짜를 받음
                    obj[7] = periodPicker1.StartDateToString;
                    obj[8] = periodPicker1.EndDateToString;
                    obj[9] = cbo_Dt_So.SelectedValue == null ? string.Empty : cbo_Dt_So.SelectedValue.ToString();//수주일자 납기일자 구분

                    dt = _biz.SearchDetail(obj);

                    //dt = _biz.SearchDetail(Key, m_cboPlant.SelectedValue == null ? string.Empty : m_cboPlant.SelectedValue.ToString(), 
                    //                        m_cboBusi.SelectedValue == null ? string.Empty : m_cboBusi.SelectedValue.ToString(), 
                    //                        m_cboStaSo.SelectedValue == null ? string.Empty : m_cboStaSo.SelectedValue.ToString());
                }
                _flexL.BindingAdd(dt, Filter);
                //_flexL.SetDummyColumn("S"); 

                //ToolBarSaveButtonEnabled = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region -> _flexH_HelpClick

        void _flexH_HelpClick(object sender, EventArgs e)
        {
            try
            {
                if (!_flexH.HasNormalRow) return;

                if (Global.MainFrame.ServerKeyCommon.ToUpper() != "SHINKI" && Global.MainFrame.ServerKeyCommon.ToUpper() != "WISE") return;

                if (_flexH.Cols[_flexH.Col].Name == "NO_SO")
                {
                    if (Global.MainFrame.ServerKeyCommon == "WISE")
                    {
                        if (D.GetDecimal(_flexH["NO_HST"]) == 0)
                        {
                            if (IsExistPage("P_SA_Z_WISE_SO", false) == true) UnLoadPage("P_SA_Z_WISE_SO", false);
                            LoadPageFrom("P_SA_Z_WISE_SO", "수주등록(WISE)", Grant, new object[] { D.GetString(_flexH["NO_SO"]) });
                        }
                        else
                        {
                            if (IsExistPage("P_SA_Z_WISE_SOHST", false) == true) UnLoadPage("P_SA_Z_WISE_SOHST", false);
                            LoadPageFrom("P_SA_Z_WISE_SOHST", "수주이력등록(WISE)", Grant, new object[] { D.GetString(_flexH["NO_SO"]) });
                        }
                    }
                    else
                    {
                        if (IsExistPage("P_SA_SO", false) == true) UnLoadPage("P_SA_SO", false);
                        LoadPageFrom("P_SA_SO", "수주등록", Grant, new object[] { D.GetString(_flexH["NO_SO"]) });
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> _flexH_CellContentChanged

        void _flexH_CellContentChanged(object sender, CellContentEventArgs e)
        {
            try
            {
                _biz.SaveContent(e.ContentType, e.CommandType, D.GetString(_flexH[e.Row, "NO_SO"]), e.SettingValue);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> _flexL_StartEdit
        void _flexL_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                switch (_flexL.Cols[e.Col].Name)
                {
                    case "S":
                        break;

                    default:
                        if (D.GetString(_flexL[e.Row, "STA_SO"]) == "C")
                            e.Cancel = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #endregion

        #region ★ 화면내버튼 클릭

        #region -> 확정버튼 클릭
        private void m_btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flexL.HasNormalRow)
                {
                    ShowMessage("확정될 데이터가 없습니다.");
                    return;
                }

                DataRow[] dr = _flexL.DataTable.Select("S = 'Y'", "NO_SO", DataViewRowState.CurrentRows);

                if (dr == null || dr.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                if (_biz.GetATP사용여부 == "001")
                {
                    if (!ATP체크로직(true)) return;
                }

                if (_biz.GetExcCredit == "300")
                {
                    if (!거래처환종별체크())
                        return;
                }
                else
                {
                    if (!거래처별체크(dr, "O"))
                        return;
                }

                if (!IsAgingCheck(dr)) return;

                if (Global.MainFrame.ServerKeyCommon.ToUpper() == "TRIGEM") // 삼보컴퓨터전용
                {
                    if (!IsTrigemCheck(dr)) return;
                }

                dr = _flexL.DataTable.Select("S = 'Y'", "NO_SO", DataViewRowState.CurrentRows);

                bool 검증여부 = false; //bool yn_confirm = false;

                StringBuilder 검증리스트 = new StringBuilder();

                string msg = "수주번호     항번 상태 수주량    의뢰량   출하량    LC적용량";
                검증리스트.AppendLine(msg);
                msg = "-".PadRight(60, '-');
                검증리스트.AppendLine(msg);

                foreach (DataRow row in dr)
                {
                    if (row["STA_SO"].ToString() == "O") //수주미정
                    {
                        row["STA_SO"] = "R"; //수주확정
                        yn_confirm = true;
                    }
                    else
                    {
                        string NO_SO = row["NO_SO"].ToString().PadRight(14, ' ');
                        string NO_LINE = row["SEQ_SO"].ToString().PadRight(3, ' ');
                        string STA_SO = row["STA_SO"].ToString().PadRight(3, ' ');
                        string QT_SO = String.Format("{0:n}", row["QT_SO"]).PadRight(10, ' ');
                        string QT_GI = String.Format("{0:n}", row["QT_GI"]).PadRight(10, ' ');
                        string QT_GIR = String.Format("{0:n}", row["QT_GIR"]).PadRight(10, ' ');
                        string QT_LC = String.Format("{0:n}", row["QT_LC"]).PadRight(10, ' ');

                        string msg2 = NO_SO + " " + NO_LINE + " " + STA_SO + " " + QT_SO + " " + QT_GIR + " " + QT_GI + " " + QT_LC;
                        검증리스트.AppendLine(msg2);
                        검증여부 = true;
                    }
                }

                if (검증여부)
                {
                    ShowDetailMessage("수주 상태를 변경하지 못한 건이 있습니다 \n" +
                         " 미정건에 대해서만 [확정]이 가능합니다. " +
                         " \n ▼ 버튼을 눌러서 불일치 목록을 확인하세요!", 검증리스트.ToString());
                }

                if (yn_confirm)
                {

                    if (!BeforeSave()) return;

                    if (SaveData())
                        ShowMessage(공통메세지._작업을완료하였습니다, m_btnConfirm.Text);
                }

                //   OnToolBarSearchButtonClicked(null, null);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
                if (!yn_confirm)
                {
                    //원상복구
                    DataRow[] dr = _flexL.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);
                    foreach (DataRow row in dr)
                    {
                        if (row["STA_SO"].ToString() == "R") // 수주미정
                            row["STA_SO"] = "O"; //수주확정
                    }
                    _flexL.AcceptChanges();
                    Page_DataChanged(null, null);
                }
            }
        }

        #endregion

        #region -> 확정취소버튼 클릭
        private void m_btnUnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flexL.HasNormalRow)
                {
                    ShowMessage("확정될 데이터가 없습니다.");
                    return;
                }

                DataRow[] dr = _flexL.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                if (dr == null || dr.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                //if (!거래처별체크(dr, "R")) return;

                bool 검증여부 = false;// bool yn_confirm = false;

                StringBuilder 검증리스트 = new StringBuilder();

                string msg = "수주번호       항번 수주상태 수주량    의뢰량   출하량    LC적용량";
                검증리스트.AppendLine(msg);
                msg = "-".PadRight(70, '-');
                검증리스트.AppendLine(msg);

                foreach (DataRow row in dr)
                {
                    if (row["STA_SO"].ToString() == "R" && Convert.ToDecimal(row["QT_GIR"]) == 0 && Convert.ToDecimal(row["QT_LC"]) == 0) //수주확정
                    {
                        row["STA_SO"] = "O"; //수주미정
                        yn_confirm = true;
                    }
                    else
                    {
                        string NO_SO = row["NO_SO"].ToString().PadRight(14, ' ');
                        string NO_LINE = row["SEQ_SO"].ToString().PadRight(3, ' ');
                        string STA_SO = row["STA_SO"].ToString().PadRight(3, ' ');
                        string QT_SO = String.Format("{0:n}", row["QT_SO"]).PadRight(10, ' ');
                        string QT_GI = String.Format("{0:n}", row["QT_GI"]).PadRight(10, ' ');
                        string QT_GIR = String.Format("{0:n}", row["QT_GIR"]).PadRight(10, ' ');
                        string QT_LC = String.Format("{0:n}", row["QT_LC"]).PadRight(10, ' ');

                        string msg2 = NO_SO + " " + NO_LINE + " " + STA_SO + " " + QT_SO + " " + QT_GIR + " " + QT_GI + " " + QT_LC;
                        검증리스트.AppendLine(msg2);
                        검증여부 = true;
                    }
                }

                if (검증여부)
                {
                    ShowDetailMessage("수주 상태를 변경하지 못한 건이 있습니다 \n" +
                         " [확정]한 건에 대해서만 [확정취소]가 가능합니다. " +
                         " \n ▼ 버튼을 눌러서 불일치 목록을 확인하세요!", 검증리스트.ToString());
                }

                if (yn_confirm)
                {
                    if (!BeforeSave()) return;

                    if (SaveData())
                        ShowMessage(공통메세지._작업을완료하였습니다, m_btnUnConfirm.Text);
                }

                //     OnToolBarSearchButtonClicked(null, null);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);

                if (!yn_confirm)
                {
                    //원상복구
                    DataRow[] dr = _flexL.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);
                    foreach (DataRow row in dr)
                    {
                        if (row["STA_SO"].ToString() == "O") //수주확정
                            row["STA_SO"] = "R"; //수주미정
                    }
                    _flexL.AcceptChanges();
                    Page_DataChanged(null, null);
                }
                //OnToolBarSearchButtonClicked(null, null);
            }
        }
        #endregion

        #region -> 종결버튼 클릭
        private void m_btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flexL.HasNormalRow)
                {
                    ShowMessage("확정될 데이터가 없습니다.");
                    return;
                }

                DataRow[] dr = _flexL.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                if (dr == null || dr.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }


                bool 검증여부 = false; //bool 품목적합 = false; bool yn_confirm = false;

                StringBuilder 검증리스트 = new StringBuilder();

                string msg = "수주번호       항번 수주상태 수주량    의뢰량   출하량    LC적용량";
                검증리스트.AppendLine(msg);
                msg = "-".PadRight(60, '-');
                검증리스트.AppendLine(msg);


                foreach (DataRow row in dr)
                {
                    if (row["STA_SO"].ToString() == "R" || row["STA_SO"].ToString() == "O") //수주확정
                    {
                        row["STA_SO"] = "C"; //수주종결
                        yn_confirm = true;
                    }
                    else
                    {
                        string NO_SO = row["NO_SO"].ToString().PadRight(14, ' ');
                        string NO_LINE = row["SEQ_SO"].ToString().PadRight(3, ' ');
                        string STA_SO = row["STA_SO"].ToString().PadRight(3, ' ');
                        string QT_SO = String.Format("{0:n}", row["QT_SO"]).PadRight(10, ' ');
                        string QT_GI = String.Format("{0:n}", row["QT_GI"]).PadRight(10, ' ');
                        string QT_GIR = String.Format("{0:n}", row["QT_GIR"]).PadRight(10, ' ');
                        string QT_LC = String.Format("{0:n}", row["QT_LC"]).PadRight(10, ' ');

                        string msg2 = NO_SO + " " + NO_LINE + " " + STA_SO + " " + QT_SO + " " + QT_GIR + " " + QT_GI + " " + QT_LC;
                        검증리스트.AppendLine(msg2);
                        검증여부 = true;
                    }
                }

                if (검증여부)
                {
                    ShowDetailMessage("수주 상태를 변경하지 못한 건이 있습니다 \n" +
                             " [미정]된 건이나 [확정]된 건에 대해서만 [종결]이 가능합니다. " +
                         " \n ▼ 버튼을 눌러서 불일치 목록을 확인하세요!", 검증리스트.ToString());
                }

                if (yn_confirm)
                {
                    if (!BeforeSave()) return;
                    버튼유형 = "종결";
                    if (SaveData())
                        ShowMessage(공통메세지._작업을완료하였습니다, m_btnClose.Text);
                }

                //   OnToolBarSearchButtonClicked(null, null);

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
                if (!yn_confirm)
                {
                    //원상복구
                    DataRow[] dr = _flexL.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);
                    foreach (DataRow row in dr)
                    {
                        if (row["STA_SO"].ToString() == "C") //수주종결
                            row["STA_SO"] = "R"; //수주확정
                    }
                    _flexL.AcceptChanges();
                    Page_DataChanged(null, null);
                }
            }
        }
        #endregion

        #region -> 종결취소버튼 클릭
        private void m_btnUnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flexL.HasNormalRow)
                {
                    ShowMessage("확정될 데이터가 없습니다.");
                    return;
                }

                DataRow[] dr = _flexL.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                if (dr == null || dr.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                if (!ChkCancelClose(dr)) return;

                bool 검증여부 = false;// bool 품목적합 = false; bool yn_confirm = false;

                StringBuilder 검증리스트 = new StringBuilder();

                string msg = "수주번호       항번 수주상태 수주량    의뢰량   출하량    LC적용량";
                검증리스트.AppendLine(msg);
                msg = "-".PadRight(60, '-');
                검증리스트.AppendLine(msg);


                foreach (DataRow row in dr)
                {
                    if (row["STA_SO"].ToString() == "C") //수주종결
                    {
                        row["STA_SO"] = "R"; //수주확정
                        yn_confirm = true;
                    }
                    else
                    {
                        string NO_SO = row["NO_SO"].ToString().PadRight(14, ' ');
                        string NO_LINE = row["SEQ_SO"].ToString().PadRight(3, ' ');
                        string STA_SO = row["STA_SO"].ToString().PadRight(3, ' ');
                        string QT_SO = String.Format("{0:n}", row["QT_SO"]).PadRight(10, ' ');
                        string QT_GI = String.Format("{0:n}", row["QT_GI"]).PadRight(10, ' ');
                        string QT_GIR = String.Format("{0:n}", row["QT_GIR"]).PadRight(10, ' ');
                        string QT_LC = String.Format("{0:n}", row["QT_LC"]).PadRight(10, ' ');

                        string msg2 = NO_SO + " " + NO_LINE + " " + STA_SO + " " + QT_SO + " " + QT_GIR + " " + QT_GI + " " + QT_LC;
                        검증리스트.AppendLine(msg2);
                        검증여부 = true;
                    }
                }

                if (검증여부)
                {
                    ShowDetailMessage("수주 상태를 변경하지 못한 건이 있습니다 \n" +
                        " [종결]건에 대해서만 [종결취소]가 가능합니다. " +
                         " \n ▼ 버튼을 눌러서 불일치 목록을 확인하세요!", 검증리스트.ToString());
                }

                if (yn_confirm)
                {

                    if (!BeforeSave()) return;
                    버튼유형 = "종결취소";
                    if (SaveData())
                        ShowMessage(공통메세지._작업을완료하였습니다, m_btnUnClose.Text);
                }

                //  OnToolBarSearchButtonClicked(null, null);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);

                if (!yn_confirm)
                {
                    DataRow[] dr = _flexL.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);
                    foreach (DataRow row in dr)
                    {
                        if (row["STA_SO"].ToString() == "R")
                            row["STA_SO"] = "C";
                    }
                    _flexL.AcceptChanges();
                    Page_DataChanged(null, null);
                }
            }
        }

        #endregion

        #region -> ATP CHECK 버튼 클릭

        void btn_ATP_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flexH.HasNormalRow || !_flexL.HasNormalRow)
                    return;

                DataRow[] dr = _flexL.DataTable.Select("S = 'Y'", "NO_SO", DataViewRowState.CurrentRows);

                if (dr == null || dr.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                if (ATP체크로직(false))
                    ShowMessage("납기일에 이상이 없습니다.");
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 전용버튼클릭

        #region -> 우진산전 전자결재

        void btn수주예정품의_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flexH.HasNormalRow || !_flexL.HasNormalRow) return;

                if (!ChkBefore전자결재()) return;
                SoInterWork siw = new SoInterWork();

                if (siw.전자결재(_flexH.GetDataRow(_flexH.Row), _flexL.GetTableFromGrid(), true))
                    ShowMessage("수주예정품의가 완료되었습니다.");
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        void btn수주확정품의_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flexH.HasNormalRow || !_flexL.HasNormalRow) return;
            
                if (!ChkBefore전자결재()) return;
                SoInterWork siw = new SoInterWork();

                if (siw.전자결재(_flexH.GetDataRow(_flexH.Row), _flexL.GetTableFromGrid(), false))
                    ShowMessage("수주확정품의가 완료되었습니다.");
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 신라파이어

        /// <summary>
        /// 신라파이어전용 - 재발주
        /// 종결취소와 같은 로직 + 라인비고1 클리어
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn재발주_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flexH.HasNormalRow || !_flexL.HasNormalRow) return;

                DataRow[] dr = _flexL.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                if (dr == null || dr.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                if (!ChkCancelClose(dr)) return;

                bool 검증여부 = false;// bool 품목적합 = false; bool yn_confirm = false;

                StringBuilder 검증리스트 = new StringBuilder();

                string msg = "수주번호       항번 수주상태 수주량    의뢰량   출하량    LC적용량";
                검증리스트.AppendLine(msg);
                msg = "-".PadRight(60, '-');
                검증리스트.AppendLine(msg);


                foreach (DataRow row in dr)
                {
                    if (row["STA_SO"].ToString() == "C") //수주종결
                    {
                        row["STA_SO"] = "R"; //수주확정
                        yn_confirm = true;

                        row["DC1"] = "";
                    }
                    else
                    {
                        string NO_SO = row["NO_SO"].ToString().PadRight(14, ' ');
                        string NO_LINE = row["SEQ_SO"].ToString().PadRight(3, ' ');
                        string STA_SO = row["STA_SO"].ToString().PadRight(3, ' ');
                        string QT_SO = String.Format("{0:n}", row["QT_SO"]).PadRight(10, ' ');
                        string QT_GI = String.Format("{0:n}", row["QT_GI"]).PadRight(10, ' ');
                        string QT_GIR = String.Format("{0:n}", row["QT_GIR"]).PadRight(10, ' ');
                        string QT_LC = String.Format("{0:n}", row["QT_LC"]).PadRight(10, ' ');

                        string msg2 = NO_SO + " " + NO_LINE + " " + STA_SO + " " + QT_SO + " " + QT_GIR + " " + QT_GI + " " + QT_LC;
                        검증리스트.AppendLine(msg2);
                        검증여부 = true;
                    }
                }

                if (검증여부)
                {
                    ShowDetailMessage("수주 상태를 변경하지 못한 건이 있습니다 \n" +
                        " [종결]건에 대해서만 [종결취소]가 가능합니다. " +
                         " \n ▼ 버튼을 눌러서 불일치 목록을 확인하세요!", 검증리스트.ToString());
                }

                if (yn_confirm)
                {

                    if (!BeforeSave()) return;
                    버튼유형 = "종결취소";
                    if (SaveData())
                        ShowMessage(공통메세지._작업을완료하였습니다, DD("재발주"));
                }

                //  OnToolBarSearchButtonClicked(null, null);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);

                if (!yn_confirm)
                {
                    DataRow[] dr = _flexL.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);
                    foreach (DataRow row in dr)
                    {
                        if (row["STA_SO"].ToString() == "R")
                            row["STA_SO"] = "C";
                    }
                    _flexL.AcceptChanges();
                    Page_DataChanged(null, null);
                }
            }
        }

        /// <summary>
        /// 신라파이어전용 - 발주취소
        /// 종결과 같은 로직 + 라인비고1에 발주취소 마킹 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn발주취소_Click(object sender, EventArgs e)
        { 
            try
            {
                if (!_flexH.HasNormalRow || !_flexL.HasNormalRow) return;

                DataRow[] dr = _flexL.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                if (dr == null || dr.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }


                bool 검증여부 = false; //bool 품목적합 = false; bool yn_confirm = false;

                StringBuilder 검증리스트 = new StringBuilder();

                string msg = "수주번호       항번 수주상태 수주량    의뢰량   출하량    LC적용량";
                검증리스트.AppendLine(msg);
                msg = "-".PadRight(60, '-');
                검증리스트.AppendLine(msg);


                foreach (DataRow row in dr)
                {
                    if (row["STA_SO"].ToString() == "R" || row["STA_SO"].ToString() == "O") //수주확정
                    {
                        row["STA_SO"] = "C"; //수주종결
                        yn_confirm = true;

                        row["DC1"] = "발주취소";
                    }
                    else
                    {
                        string NO_SO = row["NO_SO"].ToString().PadRight(14, ' ');
                        string NO_LINE = row["SEQ_SO"].ToString().PadRight(3, ' ');
                        string STA_SO = row["STA_SO"].ToString().PadRight(3, ' ');
                        string QT_SO = String.Format("{0:n}", row["QT_SO"]).PadRight(10, ' ');
                        string QT_GI = String.Format("{0:n}", row["QT_GI"]).PadRight(10, ' ');
                        string QT_GIR = String.Format("{0:n}", row["QT_GIR"]).PadRight(10, ' ');
                        string QT_LC = String.Format("{0:n}", row["QT_LC"]).PadRight(10, ' ');

                        string msg2 = NO_SO + " " + NO_LINE + " " + STA_SO + " " + QT_SO + " " + QT_GIR + " " + QT_GI + " " + QT_LC;
                        검증리스트.AppendLine(msg2);
                        검증여부 = true;
                    }
                }

                if (검증여부)
                {
                    ShowDetailMessage("수주 상태를 변경하지 못한 건이 있습니다 \n" +
                             " [미정]된 건이나 [확정]된 건에 대해서만 [종결]이 가능합니다. " +
                         " \n ▼ 버튼을 눌러서 불일치 목록을 확인하세요!", 검증리스트.ToString());
                }

                if (yn_confirm)
                {
                    if (!BeforeSave()) return;
                    버튼유형 = "종결";
                    if (SaveData())
                        ShowMessage(공통메세지._작업을완료하였습니다, DD("발주취소"));
                }

                //   OnToolBarSearchButtonClicked(null, null);

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
                if (!yn_confirm)
                {
                    //원상복구
                    DataRow[] dr = _flexL.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);
                    foreach (DataRow row in dr)
                    {
                        if (row["STA_SO"].ToString() == "C") //수주종결
                            row["STA_SO"] = "R"; //수주확정
                    }
                    _flexL.AcceptChanges();
                    Page_DataChanged(null, null);
                }
            }
        }

        #endregion

        #endregion

        void btn전자결재_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ChkBefore전자결재()) return;
                SoInterWork siw = new SoInterWork();

                if (MA.ServerKey(false, new string[] { "WONIK" }))
                {
                    if (siw.전자결재(_flexH.GetDataRow(_flexH.Row), IsWonikGwDataProcessing(_flexL.DataView.ToTable())))
                        ShowMessage("전자결재가 완료되었습니다.");
                }
                else
                {
                    if (siw.전자결재(_flexH.GetDataRow(_flexH.Row), _flexL.GetTableFromGrid()))
                        ShowMessage("전자결재가 완료되었습니다.");
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        void btn삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flexL.HasNormalRow) return;

                DataRow[] dr = _flexL.DataTable.Select("S='Y'", "", DataViewRowState.CurrentRows);

                if (dr == null || dr.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                DataRow[] chkDr = _flexL.DataTable.Select("S='Y' AND STA_SO <> 'O'", "", DataViewRowState.CurrentRows);

                if (chkDr.Length > 0)
                {
                    ShowMessage("이미 수주확정, 종결 되어 수정, 삭제가 불가능합니다.");
                    return;
                }

                foreach (DataRow row in dr)
                {
                    row.Delete();
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        
        #endregion

        #region ★ 기타 이벤트

        #region -> Control_QueryBefore
        private void Control_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            switch (e.HelpID)
            {
                case Duzon.Common.Forms.Help.HelpID.P_SA_TPSO_SUB1:
                    e.HelpParam.P61_CODE1 = "N";
                    e.HelpParam.P62_CODE2 = "Y";
                    break;

                //2011.04.04 추가
                case Duzon.Common.Forms.Help.HelpID.P_USER:
                    if (bp프로젝트.UserHelpID == "H_SA_PRJ_SUB")
                    {
                        e.HelpParam.P41_CD_FIELD1 = "프로젝트";             //도움창 이름 찍어줄 값

                    }
                    break;
            }
        }
        #endregion

        #region -> Control_QueryAfter

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

        void ctx영업그룹_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
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

        #region -> Page_DataChanged
        void Page_DataChanged(object sender, EventArgs e)
        {
            if (_flexH.HasNormalRow)
            {
                if (_flexL.HasNormalRow)
                {
                    //ToolBarSaveButtonEnabled = true;
                    ToolBarDeleteButtonEnabled = true;
                }
                else
                {
                    //  ToolBarSaveButtonEnabled = false;
                    ToolBarDeleteButtonEnabled = false;
                }
            }

            ToolBarAddButtonEnabled = false;

            //try
            //{
            //    if (_flexH.HasNormalRow)
            //    {
            //        DataTable dt = _flexL.GetChanges();
            //        if (dt == null)
            //        {
            //            m_btnConfirm.Enabled = true;
            //            m_btnUnConfirm.Enabled = true;
            //            m_btnClose.Enabled = true;
            //            m_btnUnClose.Enabled = true;
            //        }
            //        else
            //        {
            //            m_btnConfirm.Enabled = false;
            //            m_btnUnConfirm.Enabled = false;
            //            m_btnClose.Enabled = false;
            //            m_btnUnClose.Enabled = false;
            //        }

            //    }
            //    else
            //    {
            //        m_btnConfirm.Enabled = false;
            //        m_btnUnConfirm.Enabled = false;
            //        m_btnClose.Enabled = false;
            //        m_btnUnClose.Enabled = false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    this.MsgEnd(ex);
            //}
        }

        #endregion

        #region -> isChanged 라인부분을 수정해도 getChanged가 되지않아서 이부분을 일단 주석처리해주었습니다.
        //protected override bool IsChanged()
        //{
        //    //if ( base.IsChanged() )
        //    //    return false;

        //    DataTable dt = _flexH.GetChanges();
        //    DataTable dt1 = _flexL.GetChanges();

        //    if (dt != null && dt.Rows[0].RowState != DataRowState.Deleted && (dt.Rows[0]["NO_SO"].ToString() != "" || dt.Rows[0]["NO_SO"].ToString() != string.Empty))
        //    {
        //        return true;
        //    }


        //    return false;
        //}
        #endregion

        #region -> OnEvent_KeyDown
        private void OnEvent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                SendKeys.SendWait("{TAB}");
        }
        #endregion

        #endregion

        #region ★ 기타 메소드

        #region -> ShowMessage
        private DialogResult ShowMessage(메세지 msg, params object[] paras)
        {
            switch (msg)
            {
                case 메세지.이미수주확정되어수정삭제가불가능합니다:
                    return ShowMessage("SA_M000116");
            }

            return DialogResult.None;
        }
        #endregion

        #region -> 여신체크

        private bool 거래처별체크(DataRow[] dr, string 수주상태)
        {
            string 거래처 = string.Empty;
            string 멀티거래처 = string.Empty;

            //선택한 데이터중 거래처를 가져온다.
            foreach (DataRow row in dr)
            {
                if (D.GetString(row["STA_SO"]) == 수주상태 && D.GetString(row["TP_BUSI"]) == "001") //거래구분이 국내인거만
                {
                    if (거래처 != D.GetString(row["CD_PARTNER"]))
                    {
                        거래처 = D.GetString(row["CD_PARTNER"]);
                        멀티거래처 = 멀티거래처 + 거래처 + "|";
                    }
                }
            }

            string[] Args = 멀티거래처.Split('|');
            decimal 금액 = 0;

            //거래처수만큼 여신체크를 한다.
            for (int i = 0; i < Args.Length - 1; i++)
            {
                foreach (DataRow row in dr)
                {
                    if (Global.MainFrame.ServerKeyCommon.ToUpper() == "TRIGEM" && D.GetString(row["YN_AM"]) == "N") //삼보컴퓨터 전용 여신체크로직
                        continue;

                    if (D.GetString(row["STA_SO"]) == 수주상태) //수주미정
                    {
                        if (Args[i].ToString() == D.GetString(row["CD_PARTNER"]))
                            금액 = 금액 + D.GetDecimal(row["AM_WONAMT"]) + D.GetDecimal(row["AM_VAT"]);
                    }
                }
                if (Global.MainFrame.ServerKeyCommon.ToUpper() == "TRIGEM") //삼보컴퓨터 전용 여신체크로직
                {
                    if (!_biz.CheckCredit_TRIGEM(D.GetString(Args[i]), 금액, "R")) return false;
                }
                else
                {
                    if (!_biz.CheckCredit(D.GetString(Args[i]), 금액)) return false;
                }
                금액 = 0;
            }

            return true;
        }

        private bool 거래처환종별체크()
        {
            DataTable dt = _flexL.DataTable.Copy().DefaultView.ToTable(true, new string[] { "CD_PARTNER", "CD_EXCH", "STA_SO" });
            string cdPartner = string.Empty;
            string cdExch = string.Empty;
            decimal sumAmSo = 0m;

            foreach (DataRow row in dt.Rows)
            {
                if (D.GetString(row["STA_SO"]) != "O") continue;

                cdPartner = D.GetString(row["CD_PARTNER"]);
                cdExch = D.GetString(row["CD_EXCH"]);
                object obj = _flexL.DataTable.Compute("SUM(AM_SO)", "S = 'Y' AND STA_SO = 'O' AND CD_PARTNER ='" + cdPartner + "' AND CD_EXCH = '" + cdExch + "'");
                sumAmSo = D.GetDecimal(obj);

                if (!_biz.CheckCreditExec(cdPartner, cdExch, sumAmSo)) return false;
            }

            return true;
        }
        #endregion

        #region -> 미수채권일자 통제

        private bool IsAgingCheck(DataRow[] drs)
        {
            Duzon.ERPU.SA.Common.채권연령관리 aging = new Duzon.ERPU.SA.Common.채권연령관리();
            DataTable dtReturn;
            DataTable dtCheck = _flexL.DataTable.Copy();
            dtCheck.DefaultView.RowFilter = "S = 'Y' AND STA_SO = 'O'";

            DataRow[] dr = dtCheck.DefaultView.ToTable(true, new string[] { "CD_PARTNER" }).Select();

            aging.채권연령체크(dr, Duzon.ERPU.SA.Common.AgingCheckPoint.수주확정, out dtReturn);

            if (dtReturn == null || dtReturn.Rows.Count == 0) return true;

            P_SA_CUST_CREDIT_CHECK_SUB dlg = new P_SA_CUST_CREDIT_CHECK_SUB(dtReturn);

            if (dlg.ShowDialog() != DialogResult.OK) return false;

            DataTable dt = dlg.dtReturn;
            dt.PrimaryKey = new DataColumn[] { dt.Columns["CD_PARTNER"] };

            foreach (DataRow row in drs)
            {
                DataRow rowError = dt.Rows.Find(D.GetString(row["CD_PARTNER"]));

                if (rowError == null) continue;
                if (D.GetString(rowError["S"]) == "Y") continue;

                row["S"] = "N";
            }

            return true;
        }

        #endregion

        #region -> ATP관련

        bool ATP체크로직(bool 자동체크)
        {
            DataRow[] drsPlant = _flexL.DataTable.DefaultView.ToTable(true, new string[] { "S", "CD_PLANT" }).Select("S = 'Y'");

            if (drsPlant.Length > 1)
            {
                ShowMessage("두개 이상의 공장이 선택되어 ATP체크가 불가합니다");
                return false;
            }

            Duzon.ERPU.MF.Common.ATP ATP = new Duzon.ERPU.MF.Common.ATP();

            string ATP사용유무 = ATP.ATP환경설정_사용유무(LoginInfo.BizAreaCode, D.GetString(drsPlant[0]["CD_PLANT"]));
            if (ATP사용유무 == "N") return true;

            string 메뉴별ATP처리 = ATP.ATP자동체크_저장로직(D.GetString(drsPlant[0]["CD_PLANT"]), "200");
            if (메뉴별ATP처리 != "000" && 메뉴별ATP처리 != "001") return true;

            DataTable dt = _flexL.DataTable.Copy();

            DataRow[] drs = dt.Select("S = 'Y' AND YN_ATP = 'Y' AND STA_SO = 'O'", "", DataViewRowState.CurrentRows);
            DataRow[] drs2 = dt.Select("S = 'Y' AND YN_ATP = 'Y'", "", DataViewRowState.CurrentRows);

            if (drs.Length == 0) return true;

            if (drs.Length != drs2.Length)
            {
                ShowMessage("수주상태가 미확정인 건에 대해서만 ATP체크를 합니다.");
            }

            if (drs.Length != dt.DefaultView.ToTable(true, new string[] { "CD_ITEM", "YN_ATP", "STA_SO" }).Select("YN_ATP = 'Y' AND STA_SO = 'O'").Length)
            {
                if (ShowMessage("동일품목이 존재 할 경우 정확한 ATP체크를 할 수 없습니다." + Environment.NewLine + "계속 진행하시겠습니까?", "QY2") != DialogResult.Yes)
                    return false;
            }

            string s_Message = string.Empty;

            ATP.Set메뉴ID = PageID;

            bool ATPGood = ATP.ATP_Check(drs, out s_Message);

            if (ATPGood == false)
            {
                if (자동체크 == false)
                {
                    ShowDetailMessage("납기일을 맞출 수 없습니다." + Environment.NewLine + "[︾] 버튼을 눌러 내역을 확인하세요.", s_Message);
                    return false;
                }
                else
                {
                    if (메뉴별ATP처리 == "000")
                    {
                        if (ShowDetailMessage("납기일을 맞출 수 없습니다." + Environment.NewLine + "[︾] 버튼을 눌러 내역을 확인하세요." + Environment.NewLine + "그래도 확정하시겠습니까?", "", s_Message, "QY2") != DialogResult.Yes)
                            return false;
                        else
                            return true;
                    }
                    else if (메뉴별ATP처리 == "001")
                    {
                        ShowDetailMessage("납기일을 맞출 수 없습니다." + Environment.NewLine + "[︾] 버튼을 눌러 내역을 확인하세요.", s_Message);
                        return false;
                    }

                    return true;
                }
            }

            return true;
        }

        #endregion

        #region -> 전자결재 미확정여부 체크

        private bool Chk미확정여부()
        {
            if (_biz.IsConfirm(D.GetString(_flexH["NO_SO"])))
            {
                ShowMessage("해당수주 [" + D.GetString(_flexH["NO_SO"]) + "]건에 대해서 확정이후 진행된 건이 있습니다.");
                return false;
            }

            return true;
        }

        #endregion

        #region -> 전자결재 전 Check

        private bool ChkBefore전자결재()
        {
            if (IsChanged())
            {
                ShowMessage("저장후 결재버튼을 클릭하세요.");
                return false;
            }

            if (!Chk미확정여부()) return false;

            return true;
        }

        #endregion

        #region -> 사용자정의세팅
        private void 사용자정의세팅()
        {
            DataTable dtHeaderCodeUser = MA.GetCode("SA_B000110");
            DataRow[] drsCode = dtHeaderCodeUser.Select("CD_FLAG1 = 'CODE'");

            for (int i = 1; i <= drsCode.Length; i++)
            {
                if (i <= 1)
                {
                    string Name = D.GetString(drsCode[i - 1]["NAME"]);
                    _flexH.Cols["CD_USERDEF" + D.GetString(i)].Caption = Name;
                    _flexH.Cols["CD_USERDEF" + D.GetString(i)].Visible = true;
                }
            }

            for (int i = 1; i <= 6; i++)
            {
                _flexL.Cols["NUM_USERDEF" + D.GetString(i)].Visible = false;
            }
            DataTable dtLineNumberUser = MA.GetCode("SA_B000069");
            for (int i = 1; (i <= dtLineNumberUser.Rows.Count && i <= 6); i++)
            {
                string Name = D.GetString(dtLineNumberUser.Rows[i - 1]["NAME"]);
                _flexL.Cols["NUM_USERDEF" + D.GetString(i)].Caption = Name;
                _flexL.Cols["NUM_USERDEF" + D.GetString(i)].Visible = true;
            }
            for (int i = 1; i <= 2; i++)
            {
                _flexL.Cols["TXT_USERDEF" + D.GetString(i)].Visible = false;
            }
            DataTable dtLineTextUser = MA.GetCode("SA_B000112");
            for (int i = 1; (i <= dtLineTextUser.Rows.Count && i <= 2); i++)
            {
                string Name = D.GetString(dtLineTextUser.Rows[i - 1]["NAME"]);
                _flexL.Cols["TXT_USERDEF" + D.GetString(i)].Caption = Name;
                _flexL.Cols["TXT_USERDEF" + D.GetString(i)].Visible = true;
            }
        }
        #endregion

        #region -> 삼보컴퓨터 수주확정여부
        private bool IsTrigemCheck(DataRow[] drs)
        {
            DataTable dtH = _flexH.DataView.ToTable();
            dtH.PrimaryKey = new DataColumn[] { dtH.Columns["NO_SO"] };

            DataTable dtL = _flexL.DataTable.Clone();

            DataTable dtCode = MA.GetCode("PU_C000065");
            dtCode.PrimaryKey = new DataColumn[] { dtCode.Columns["CODE"] };

            bool result = true;
            StringBuilder 검증리스트 = new StringBuilder();
            검증리스트.AppendLine("수주번호".PadRight(25, ' ') + "결재상태");
            검증리스트.AppendLine("-".PadRight(50, '-'));

            foreach (DataRow dr in drs)
                dtL.ImportRow(dr);

            DataRow[] dr_temp = dtL.DefaultView.ToTable(true, new string[] { "NO_SO" }).Select();

            foreach (DataRow dr in dr_temp)
            {
                DataRow rowFind = dtH.Rows.Find(D.GetString(dr["NO_SO"]));
                if (rowFind == null) continue;

                if (D.GetString(rowFind["CD_USERDEF1"]) == "Y" && D.GetString(rowFind["ST_STAT"]) != "1")
                {
                    result = false;
                    DataRow rowCodeFind = dtCode.Rows.Find(D.GetString(rowFind["ST_STAT"]));
                    검증리스트.AppendLine(D.GetString(dr["NO_SO"]).PadRight(25, ' ') + D.GetString(rowCodeFind["NAME"]));
                }
            }
            if (!result)
            {
                ShowDetailMessage("상신대상여부가 YES인 건은 결재상태가 완료일 경우에만 확정처리가 가능합니다." + Environment.NewLine + "[더보기] 버튼을 눌러 목록을 확인하세요!! ", 검증리스트.ToString());
                return false;
            }


            return true;
        }
        #endregion

        #region -> 종결취소시 체크

        private bool ChkCancelClose(DataRow[] drs)
        {
            //일단 삼보컴퓨터에 대한 전용로직이므로 다른업체인 경우 리턴
            if (Global.MainFrame.ServerKeyCommon.ToUpper() != "TRIGEM") return true;

            string multiPartner = Common.MultiString(drs, "CD_PARTNER", "|");
            string[] argsPartner = multiPartner.Split('|');
            decimal sumAmWonamt = decimal.Zero;
            decimal amWonamt = decimal.Zero;
            decimal amVat = decimal.Zero;

            //거래처별로 체크를 한다.
            for (int i = 0; i < argsPartner.Length - 1; i++)
            {
                foreach (DataRow row in drs)
                {
                    if (D.GetString(row["CD_PARTNER"]) == D.GetString(argsPartner[i]) && D.GetString(row["STA_SO"]) == "C" && D.GetString(row["YN_AM"]) == "Y") //종결된 수주건 && 유환인것만
                    {
                        //출하수량과 수주수량이 같다면 이미 채권집계테이블에 해당 수주건의 금액이 반영 되었기 때문에 제외
                        if (D.GetDecimal(row["QT_GI"]) == D.GetDecimal(row["QT_SO"])) continue;
                        //분할되었다면 이미 출하된 금액은 제외하고 미출하된 금액만 계산해주기 위함
                        if (D.GetDecimal(row["QT_GI"]) == decimal.Zero)
                        {
                            amWonamt = D.GetDecimal(row["AM_WONAMT"]);
                            amVat = D.GetDecimal(row["AM_VAT"]);
                        }
                        else
                        {
                            amWonamt = (D.GetDecimal(row["QT_SO"]) - D.GetDecimal(row["QT_GI"])) * D.GetDecimal(row["UM_SO"]) * D.GetDecimal(row["RT_EXCH"]);
                            amVat = amWonamt / (D.GetDecimal(row["RT_VAT"]) * 100m);
                        }

                        sumAmWonamt += (amWonamt + amVat);
                    }
                }

                if (Global.MainFrame.ServerKeyCommon.ToUpper() == "TRIGEM") //삼보컴퓨터 전용 여신체크로직
                {
                    if (!_biz.CheckCredit_TRIGEM(D.GetString(argsPartner[i]), sumAmWonamt, "C")) return false;
                }
                else
                {
                    if (!_biz.CheckCredit(D.GetString(argsPartner[i]), sumAmWonamt)) return false;
                }

                sumAmWonamt = decimal.Zero;
            }

            return true;
        }

        #endregion

        #region -> 원익 전자결재 데이터 가공
        private DataTable IsWonikGwDataProcessing(DataTable dt)
        {
            dt.Columns.Add("UM_INV", typeof(decimal));              //재고평가단가
            dt.Columns.Add("AM_INV", typeof(decimal));              //재고평가단가 * 수량
            dt.Columns.Add("AM_PROFIT", typeof(decimal));           //이익
            dt.Columns.Add("DT_INV", typeof(string));               //재고평가월
            dt.Columns.Add("AM_STD_SALE", typeof(decimal));         //기준판매금액
            dt.Columns.Add("RT_STD_SALE", typeof(decimal));         //할인율

            string multiCdItem = Common.MultiString(dt, "CD_ITEM", "|");

            DataTable dtUmInv = _biz.Search_EstimateCost(D.GetString(m_cboPlant.SelectedValue), multiCdItem);
            dtUmInv.PrimaryKey = new DataColumn[] { dtUmInv.Columns["CD_ITEM"] };
            decimal umInv = decimal.Zero;
            string dtUnv = string.Empty;

            decimal sumAmGirAmtVat = D.GetDecimal(dt.Compute("SUM(AM_SUM)", ""));
            decimal sumAmGirAmt = D.GetDecimal(dt.Compute("SUM(AM_WONAMT)", ""));

            foreach (DataRow row in dt.Rows)
            {
                DataRow rowUmInv = dtUmInv.Rows.Find(row["CD_ITEM"]);
                if (rowUmInv == null)
                {
                    umInv = decimal.Zero;
                    dtUnv = string.Empty;
                }
                else
                {
                    umInv = D.GetDecimal(rowUmInv["UM_INV"]);
                    dtUnv = D.GetString(rowUmInv["DT_INV"]);
                }

                row["UM_INV"] = umInv;
                row["AM_INV"] = D.GetDecimal(row["QT_SO"]) * umInv;
                row["AM_PROFIT"] = D.GetDecimal(row["AM_WONAMT"]) - D.GetDecimal(row["AM_INV"]);
                row["DT_INV"] = dtUnv;
                row["AM_STD_SALE"] = D.GetDecimal(row["PITEM_NUM_USERDEF1"]) * D.GetDecimal(row["QT_SO"]);
                row["RT_STD_SALE"] = D.GetDecimal(row["PITEM_NUM_USERDEF1"]) * D.GetDecimal(row["QT_SO"]) == 0 ? 0 : Math.Round((((D.GetDecimal(row["PITEM_NUM_USERDEF1"]) * D.GetDecimal(row["QT_SO"])) - D.GetDecimal(row["AM_WONAMT"])) / (D.GetDecimal(row["PITEM_NUM_USERDEF1"]) * D.GetDecimal(row["QT_SO"]))) * 100, 2);
            }

            dt.AcceptChanges();

            return dt;
        } 
        #endregion

        //bool chkDate { get { return Checker.IsValid(m_mskDtStart, m_mskDtEnd, true, DD(D.GetString(cbo_Dt_So.Text) + "From"), DD(D.GetString(cbo_Dt_So.Text) + "To")); } }
        bool chkDate { get { return Checker.IsValid(periodPicker1, true, cbo_Dt_So.Text); } }

        #endregion

        #region ★ 업체별 컨트롤 세팅

        private void SettingControl()
        {
            //서버키개발 테스트용 (84, 108)
            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "DZSQL" || 
                Global.MainFrame.ServerKeyCommon.ToUpper() == "SQL_")
            {
                //pnl삭제.Visible = true;

                //m_btnConfirm.Visible = false;
                //m_btnUnConfirm.Visible = false;
                //btn업체전용1.Visible = true;
                //btn업체전용2.Visible = true;
                //btn업체전용1.Text = DD("수주확정품의");
                //btn업체전용2.Text = DD("수주예정품의");
                //btn업체전용1.Click += new EventHandler(btn수주확정품의_Click);
                //btn업체전용2.Click += new EventHandler(btn수주예정품의_Click);

                //btn업체전용1.Visible = true;
                //btn업체전용2.Visible = true;
                //btn업체전용1.Text = DD("재발주");
                //btn업체전용2.Text = DD("발주취소");
                //btn업체전용1.Click += new EventHandler(btn재발주_Click);
                //btn업체전용2.Click += new EventHandler(btn발주취소_Click);
            }

            //ServerKeyCommon.ToUpper() -> 서버키 대문자 및 숫자 제외
            switch (Global.MainFrame.ServerKeyCommon.ToUpper())
            {
                case "WJIS":        //우진
                    m_btnConfirm.Visible = false;
                    m_btnUnConfirm.Visible = false;
                    btn업체전용1.Visible = true;
                    btn업체전용2.Visible = true;
                    btn업체전용1.Text = DD("수주확정품의");
                    btn업체전용2.Text = DD("수주예정품의");
                    btn업체전용1.Click += new EventHandler(btn수주확정품의_Click);
                    btn업체전용2.Click += new EventHandler(btn수주예정품의_Click);
                    break;
                case "TRIGEM":          //삼보컴퓨터
                    _flexH.SetDataMap("ST_STAT", MA.GetCode("SA_B000060"), "CODE", "NAME");
                    break;
                case "GALAXIA":         //갤럭시아
                case "WONIK":           //원익
                    btn전자결재.Visible = true;
                    bpPanelControl8.Visible = true; //전자결재상태
                    btn전자결재.Click += new EventHandler(btn전자결재_Click);
                    _flexH.SetDataMap("ST_STAT", MA.GetCode("SA_B000060"), "CODE", "NAME");
                    break;
                case "SIMMONS":         //시몬스
                    bpPanelControl9.Visible = true;
                    _flexL.SetDataMap("ST_PROC", MA.GetCodeUser(new string[] { "R", "O" }, new string[] { "확정", "미확정" }, true), "CODE", "NAME");
                    break;
                case "CHOSUNHOTELBA":   //조선호텔베이커리
                    pnl삭제.Visible = true;
                    break;
                case "MHIK":            //엠에이치파워시스템즈코리아
                    lbl수주번호.Text = "전용수주번호";
                    break;
                case "WISE":            //와이즈산전
                    if (수주번호output != "")
                    {
                        m_cboPlant.SelectedValue = 공장output;
                        periodPicker1.StartDateToString = 수주일자output;
                        periodPicker1.EndDateToString = 수주일자output;
                        txt수주번호.Text = 수주번호output;

                        OnToolBarSearchButtonClicked(null, null);
                    }
                    break;
                case "SLFIRE":          //신라파이어
                    btn업체전용1.Visible = true;
                    btn업체전용2.Visible = true;
                    btn업체전용1.Text = DD("재발주");
                    btn업체전용2.Text = DD("발주취소");
                    btn업체전용1.Click += new EventHandler(btn재발주_Click);
                    btn업체전용2.Click += new EventHandler(btn발주취소_Click);
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
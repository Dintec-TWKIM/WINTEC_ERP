using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Duzon.Common.Controls;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.Common.Forms.Help;

using Duzon.ERPU;

using System.Threading;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Windows.Print;
using Duzon.ERPU.PR;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;

namespace prd
{
    //**************************************
    // 작   성   자 : 김현정
    // 재 작  성 일 : 2006-09-21
    // 모   듈   명 : 생산
    // 시 스  템 명 : 생산관리
    // 서브시스템명 : 공정관리
    // 페 이 지  명 : 작업실적등록(W/O별)
    // 프로젝트  명 : P_PR_WORKWO_REG
    //**************************************
    /*************************************
     *************** HISTORY *************
     *2012.12.05 CCJ - USE_MAT(사용자재)추가
     *2012.12.17 CCJ - 조회 (F4) 버튼 클릭 시 PR_WO_MES에서 실적 수량 불러옴
     *                 저장 버튼 클릭 시 PR_WO_MES 테이블의 NO_IO, NO_LINE, YN_INSERT, YN_WORK 업데이트
    *************************************/


    public partial class P_PR_WORKWO_REG_OLD3 : Duzon.Common.Forms.PageBase
    {
        #region ♣ 생성자 & 변수 선언

        private string strErrCol, str_cd_plant, str_no_wo, str_tp_rout, str_cd_item, str_nm_item, str_stnd_item, str_unit_im, str_dt_rel, str_dt_due, str_fg_gubun;

        private string YN_PR_MNG_LOT;

        private DataTable dtWork;

        private DataRow[] drs;

        private DataTable _dtReject;

        private DataTable _dtLotItem;

        private DataTable _dtSERL;

        private Hashtable htDetailQueryCollection;

        //키값으로 사용할 stDetailQuery struct 를 정의하고 선언한다.
        private stDetailQuery st;

        public struct stDetailQuery
        {
            public string NO_IO;
            public string NO_LOT;
            public int NO_LINE2;
        }

        private DataTable _dtMatl;

        private DataTable _dtLotMatl;

        //private DataTable dtSFT;

        private bool bSaveCalled = false;

        bool bGridrowChanging = false;

        bool bDetailGridrowChanging = true;

        private P_PR_WORKWO_REG_BIZ _biz = new P_PR_WORKWO_REG_BIZ();

        public P_PR_WORKWO_REG_OLD3()
        {
            try
            {
                InitializeComponent();
                MainGrids = new FlexGrid[] { _flexD };
                DataChanged += new EventHandler(Page_DataChanged);

                chk_작업지시분할.Checked = Settings1.Default.Work_Detail_Rel_YN;
                chK_작업지시비고적용여부.Checked = Settings1.Default.Wo_DcRmk_Apply_YN;

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        // 메뉴 연결을 위한 메소드(작업실적현황->작업실적등록(W/0), 작업실적리스트->작업실적등록(W/0))
        public P_PR_WORKWO_REG_OLD3(string pg_cd_plant, string pg_no_wo, string pg_tp_rout, string pg_cd_item, string pg_nm_item, string pg_stnd_item, string pg_unit_im, string pg_dt_rel, string pg_dt_due, string pg_fg_gubun)
        {
            try
            {
                InitializeComponent();
                MainGrids = new FlexGrid[] { _flexD };
                DataChanged += new EventHandler(Page_DataChanged);

                chk_작업지시분할.Checked = Settings1.Default.Work_Detail_Rel_YN;
                chK_작업지시비고적용여부.Checked = Settings1.Default.Wo_DcRmk_Apply_YN;


                str_cd_plant = pg_cd_plant;
                str_no_wo    = pg_no_wo; 
                str_tp_rout  = pg_tp_rout; 
                str_cd_item  = pg_cd_item;
                str_nm_item = pg_nm_item;
                str_stnd_item = pg_stnd_item;
                str_unit_im = pg_unit_im;
                str_dt_rel   = pg_dt_rel;
                str_dt_due   = pg_dt_due;
                str_fg_gubun = pg_fg_gubun;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ♣ 초기화 이벤트 / 메소드

        #region -> InitLoad

        protected override void InitLoad()
        {
            base.InitLoad();

            _dtReject = new DataTable();        //불량수량 저장을 위한 테이블
            _dtLotItem = new DataTable();       //제품의 Lot 저장을 위한 테이블
            _dtSERL = new DataTable();          //제품의 Serial 저장을 위한 테이블
            _dtMatl = new DataTable();          //투입자재 저장을 위한 테이블
            _dtLotMatl = new DataTable();       //투입자재의 Lot 저장을 위한 테이블
            
            htDetailQueryCollection = new Hashtable();
            st = new stDetailQuery();

            InitGridM();    //작업지시그리드
            InitGridD();    //작업공정그리드

            _flexM.DetailGrids = new FlexGrid[] { _flexD };
        }

        #endregion

        #region -> InitGridM

        private void InitGridM()
        {
            _flexM.BeginSetting(1, 1, false);
            _flexM.SetCol("NO_WO", "작업지시번호", 100, false);
            _flexM.SetCol("FG_WO", "작업지시구분", 90, false);
            _flexM.SetCol("ST_WO", "상태", 40, false);
            _flexM.SetCol("NM_TP_WO", "오더형태", 100, false);
            _flexM.SetCol("CD_ITEM", "품목", 100, false);
            _flexM.SetCol("NM_ITEM", "품목명", 140, false);
            _flexM.SetCol("STND_ITEM", "규격", 120, false);
            _flexM.SetCol("UNIT_IM", "단위", 40, false);
            _flexM.SetCol("QT_ITEM", "지시수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexM.SetCol("DT_REL", "시작일", 70,false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexM.SetCol("DT_DUE", "종료일", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexM.SetCol("YN_LOT", "LOT사용유무", 100, false, CheckTypeEnum.Y_N);
            _flexM.SetCol("NO_LOT", "LOT번호", 100, 50, false);
            _flexM.SetCol("YN_SERL", "SERIAL사용유무", 120, false, CheckTypeEnum.Y_N);
            _flexM.SetCol("CD_SL", "입고창고코드", 100, 50, false);
            _flexM.SetCol("NM_SL", "입고창고명", 140, 50, false);
            _flexM.SetCol("NO_PJT", "프로젝트코드", 100, false);
            _flexM.SetCol("NM_PJT", "프로젝트", 140, false);
            //시스템통제로 처리할 예정임.
            _flexM.SetCol("DT_CLOSE", "마감일", 100, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
        
            
            //20110303 최인성 김성호 정찬용
            _flexM.SetCol("NO_SO", "수주번호", 140, false);
            _flexM.SetCol("DT_DUEDATE", "납기요구일", 110, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            
            _flexM.SetCol("DC_RMK", "비고", 120, false);


            _flexM.SetCol("CD_ITEM_ORIGIN", "관련품목", 100, false);
            _flexM.SetCol("NM_ITEM_ORIGIN", "관련품목명", 120, false);
            _flexM.SetCol("STND_ITEM_ORIGIN", "관련품목규격", 100, false);


            _flexM.SettingVersion = "1.0.0.0.1";

            _flexM.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            _flexM.Cols["CD_SL"].Visible = false;
            _flexM.Cols["NM_SL"].Visible = false;

            _flexM.BeforeRowChange += new RangeEventHandler(_flexM_BeforeRowChange);
            _flexM.AfterRowChange += new RangeEventHandler(_flexM_AfterRowChange);

            _flexM.CellNoteInfo.EnabledCellNote = true;
            _flexM.CellNoteInfo.CategoryID = this.Name;
            _flexM.CellNoteInfo.DisplayColumnForDefaultNote = "NO_WO";

            _flexM.CellContentChanged += new CellContentEventHandler(_flexM_CellContentChanged);

            _flexM.CheckPenInfo.EnabledCheckPen = true;
        }

        #endregion

        #region -> InitGridD

        private void InitGridD()
        {
            _flexD.BeginSetting(1, 1, false);
            _flexD.SetCol("CD_OP", "OP", 25);
            _flexD.SetCol("NM_OP", "공정명", 100);
            _flexD.SetCol("CD_WC", "작업장", 50);
            _flexD.SetCol("NM_WC", "작업장명", 100);
            _flexD.SetCol("ST_OP", "상태", 35);
            _flexD.SetCol("QT_WO", "지시수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexD.SetCol("QT_START", "입고수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexD.SetCol("QT_WO_WORK", "실적수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexD.SetCol("QT_REMAIN", "작업잔량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexD.SetCol("QT_MOVE", "이동수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexD.SetCol("QT_REWORKREMAIN", "재작업잔량", 70, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexD.SetCol("DT_WORK", "실적일", 70, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexD.SetCol("NO_EMP", "담당자코드", 80, 10, true);
            _flexD.SetCol("NM_KOR", "담당자명", 100, false);
            _flexD.SetCol("NO_SFT", "SFT", 40, true);
            _flexD.SetCol("NM_SFT", "SFT명", 100, false);
            _flexD.SetCol("CD_EQUIP", "설비코드", 60, true);
            _flexD.SetCol("NM_EQUIP", "설비명", 100, false);
            _flexD.SetCol("QT_WORK", "실적입력량", 80, true, typeof(decimal), FormatTpType.QUANTITY);
            _flexD.SetCol("QT_REJECT", "불량입력량", 60, true, typeof(decimal), FormatTpType.QUANTITY);
            _flexD.SetCol("YN_QC",     "QC검사",       70,  false, typeof(string));
            _flexD.SetCol("NM_WCOP_SUB", "SUB공정명", 100, 50, true, typeof(string));
            _flexD.SetCol("NO_LOT", "LOT번호", 100, 50, true, typeof(string));
            _flexD.SetCol("DT_LIMIT", "유효일자", 100, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexD.SetCol("DC_RMK1", "비고1", 100, 100, true);
            _flexD.SetCol("DC_RMK2", "비고2", 100, 100, true);
            _flexD.SetCol("YN_CLOSE", "분할마감여부", 100, false);
            _flexD.SetCol("USE_MAT", "사용자재", 100, false);



            //20110812 최인성 김헌섭 실적 초과를 사용할 경우 기본 그리드의 셋팅값을 통제 할 방법이 없다.
            //공장환경에 따른 통제를 걸었기 때문에 사용할 방법이 없음.
            //_flexD.VerifyCompare(_flexD.Cols["QT_WORK"], _flexD.Cols["QT_REMAIN"], OperatorEnum.LessOrEqual);
            _flexD.VerifyCompare(_flexD.Cols["QT_WORK"], _flexD.Cols["QT_REJECT"], OperatorEnum.GreaterOrEqual);
            _flexD.VerifyCompare(_flexD.Cols["QT_WORK"], 0, OperatorEnum.Greater);
            _flexD.VerifyNotNull = new string[] { "DT_WORK", "NO_EMP" };

            _flexD.SetCodeHelpCol("NO_EMP", HelpID.P_MA_EMP_SUB, ShowHelpEnum.Always, new string[] { "NO_EMP", "NM_KOR", "CD_DEPT" }, new string[] { "NO_EMP", "NM_KOR", "CD_DEPT" }, new string[] { "NO_EMP", "NM_KOR", "CD_DEPT" }, ResultMode.SlowMode);
            _flexD.SetCodeHelpCol("NO_SFT", "H_PR_SFT_SUB", ShowHelpEnum.Always, new string[] { "NO_SFT", "NM_SFT" }, new string[] { "NO_SFT", "NM_SFT" });
            _flexD.SetCodeHelpCol("CD_EQUIP", "H_PR_EQUIP_SUB", ShowHelpEnum.Always, new string[] { "CD_EQUIP", "NM_EQUIP" }, new string[] { "CD_EQUIP", "NM_EQUIP" });
            _flexD.SetCodeHelpCol("NM_WCOP_SUB", "H_PR_WCOP_SUB_SUB", ShowHelpEnum.Always, new string[] { "CD_WCOP_SUB", "NM_WCOP_SUB" }, new string[] { "CD_WCOP_SUB", "NM_WCOP_SUB" });

            _flexD.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.None, SumPositionEnum.Top);

            if (LoginInfo.MngLot != "Y")
                _flexD.Cols["NO_LOT"].Visible = false;

            _flexD.Cols["USE_MAT"].Visible = false;

            _flexD.AfterRowChange += new RangeEventHandler(_flexD_AfterRowChange);
            _flexD.BeforeRowChange += new RangeEventHandler(_flexD_BeforeRowChange);
            _flexD.BeforeCodeHelp += new BeforeCodeHelpEventHandler(_flexD_BeforeCodeHelp);
            //_flexD.AfterCodeHelp += new AfterCodeHelpEventHandler(_flexD_AfterCodeHelp);
            _flexD.ValidateEdit += new ValidateEditEventHandler(_flexD_ValidateEdit);
            _flexD.StartEdit += new RowColEventHandler(_flexD_StartEdit);
            //_flexD.DoubleClick += new EventHandler(_flexD_DoubleClick);
            _flexD.OwnerDrawCell += new OwnerDrawCellEventHandler(_flexD_OwnerDrawCell);

            _flexD.CellNoteInfo.EnabledCellNote = true;
            _flexD.CellNoteInfo.CategoryID = this.Name;
            _flexD.CellNoteInfo.DisplayColumnForDefaultNote = "DC_RMK1";

            _flexD.CellContentChanged += new CellContentEventHandler(_flexD_CellContentChanged);

            _flexD.CheckPenInfo.EnabledCheckPen = true;
        }

        #endregion

        #region -> InitPaint

        protected override void InitPaint()
        {
            base.InitPaint();

            InitControl();

            if (str_fg_gubun == "작업실적현황" || str_fg_gubun == "작업실적리스트" || str_fg_gubun == "자재투입리스트")
            {
                m_cboCdPlant.SelectedValue = str_cd_plant;
                m_txtWoFrom.CodeValue = str_no_wo;
                m_txtWoFrom.CodeName  = str_no_wo;
                m_txtWoTo.CodeValue   = str_no_wo;
                m_txtWoTo.CodeName    = str_no_wo;
                m_bptxtCdItem.CodeValue = str_cd_item;
                m_bptxtCdItem.CodeName = str_nm_item;
                m_txtStndItem.Text = str_stnd_item;
                m_txtUnit.Text = str_unit_im;
                m_dtpFrom.Text = str_dt_rel;
                m_dtpTo.Text = str_dt_due;

                OnToolBarSearchButtonClicked(null, null);
            }
        }

        #endregion

        #region -> InitControl

        private void InitControl()
        {
            //PageBase 의 부모함수 이용 : 폼이 올라올 후 폼위에 올라온 컨트롤들을 초기화 시켜주는 역할을 한다.
            //Combo Box 셋팅시 옵션 => N:공백없음, S:공백추가, SC:공백&괄호추가, NC:괄호추가
            //뒤의 코드값들은 DB 에서 정해놓은 파라미터와 매치시켜줘야한다
            //공장, 상태,  작업지시구분, 작업지시상태
            DataSet ds = GetComboData("NC;MA_PLANT", "N;PR_0000009", "N;PR_0000007", "N;PR_0000006");

            m_cboCdPlant.DataSource = ds.Tables[0];
            m_cboCdPlant.DisplayMember = "NAME";
            m_cboCdPlant.ValueMember = "CODE";

            if (m_cboCdPlant.Items.Count > 0)
            {
                if (ds.Tables[0].Select("CODE = '" + LoginInfo.CdPlant + "'").Length == 1)
                    m_cboCdPlant.SelectedValue = LoginInfo.CdPlant;
                else if (m_cboCdPlant.Items.Count > 0)
                    m_cboCdPlant.SelectedIndex = 0;
            }

            _flexD.SetDataMap("ST_OP", ds.Tables[1], "CODE", "NAME");
            _flexM.SetDataMap("FG_WO", ds.Tables[2], "CODE", "NAME");
            _flexM.SetDataMap("ST_WO", ds.Tables[3], "CODE", "NAME");


            //분할작업지시 마감여부
            DataTable dt = ds.Tables[1].Clone();
            DataRow dr_Temp = dt.NewRow();
            dr_Temp["CODE"] = "N";
            dr_Temp["NAME"] = "미마감";
            dt.Rows.Add(dr_Temp.ItemArray);
            dr_Temp = dt.NewRow();
            dr_Temp["CODE"] = "Y";
            dr_Temp["NAME"] = "마감";
            dt.Rows.Add(dr_Temp.ItemArray);

            _flexD.SetDataMap("YN_CLOSE", dt, "CODE", "NAME");

            m_dtpFrom.Text = DateTime.Now.AddMonths(-1).ToShortDateString();
            m_dtpTo.Text = DateTime.Now.AddMonths(1).ToShortDateString();


            if (Pr_Global.bMfg_AuthH_YN == true)
            {
                //회사, 부서, 사번
                object[] obj = new object[]{Global.MainFrame.LoginInfo.CompanyCode, 
                                            string.Empty, 
                                            Global.MainFrame.LoginInfo.EmployeeNo,
                                            LoginInfo.CdPlant};

                DataSet ds_Auth = _biz.Search_AUTH(obj);

                //추후에 이부분 공통으로 처리 하도록 수정할 예정임. 최인성
                //컨트롤만 넣으면 권한 적용 받아서 처리하게끔 할것임.
                //오더형태 넣는 부분
                if (ds_Auth != null && ds_Auth.Tables[7].Rows.Count != 0)
                {
                    foreach (DataRow dr in ds_Auth.Tables[7].Rows)
                    {
                        if (D.GetString(dr["FG_USE"]) == "" || D.GetString(dr["FG_USE"]) == "N") continue;

                        bp_Tp_Wo.AddItem2(D.GetString(dr["CD_AUTH"]), D.GetString(dr["NM_AUTH"]));
                    }
                }

                //W/C넣는 부분
                if (ds_Auth != null && ds_Auth.Tables[6].Rows.Count != 0)
                {
                    foreach (DataRow dr in ds_Auth.Tables[6].Rows)
                    {
                        if (D.GetString(dr["FG_USE"]) == "" || D.GetString(dr["FG_USE"]) == "N") continue;

                        bp_Cd_WC.AddItem2(D.GetString(dr["CD_AUTH"]), D.GetString(dr["NM_AUTH"]));
                    }
                }
            }


            //20110113 최인성 김성호
            //작업실적일 - 기본 셋팅
            //* 작업실적일 컨트롤
            //* 1. 000 : 컨트롤 미사용(비활성)
            //* 2. 100 : 컨트롤 사용(활성)
            if (Pr_Global.Dat_Work_Control_YN == "000")
            {
                panelExt4.Visible = false;
                dat_Work_Dt.Visible = false;
            }
            else
            {
                panelExt4.Visible = true;
                dat_Work_Dt.Visible = true;
            }


            //20110113 최인성 김성호 전정식
            //작업실적등록(WO별)_마감사용여부
            //* 마감버튼 사용여부
            //* 1. 'N' : 컨트롤 미사용(비활성)
            //* 2. 'Y' : 컨트롤 사용(활성)
            if (Pr_Global.pr_Wo_Close_YN.ToUpper() == "Y")
            {
                btn_작업지시마감.Visible = true;
                btn_작업지시마감취소.Visible = true;
            }

            

            //사용여부 상관없이 일단 오늘날짜로 바인딩 한다.
            dat_Work_Dt.Text = Global.MainFrame.GetStringToday;



            //우진전용 일일작업공수버튼 활성화 및 비활성화
            if (Global.MainFrame.ServerKeyCommon == "WJIS" || Global.MainFrame.ServerKeyCommon == "SQL_"
                || Global.MainFrame.ServerKeyCommon == "DZSQL")
                btn_일일작업공수.Visible = true;

        }

        #endregion

        #region -> Page_DataChanged

        void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                ToolBarAddButtonEnabled = false;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region ♣ 메인버튼 이벤트 / 메소드

        #region -> BeforeSearch Override

        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch())
            {
                if (_flexD.Cols.Contains(strErrCol))
                    _flexD.Col = _flexD.Cols[strErrCol].Index;
                return false;
            }

            if (!공장선택여부)
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, m_lblplant.Text);
                m_cboCdPlant.Focus();
                return false;
            }

            if (!Checker.IsValid(m_dtpFrom, m_dtpTo, false, m_lblperiodwork.Text, m_lblperiodwork.Text))
                return false;

            return true;
        }

        #endregion

        #region -> 조회

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSearch())    return;
                //Oracle사용시에 문제가 발생됨. NO_REL 작업지시분할 컬럼이 추가되었는데
                //분할체크 여부에 따라 문제가 발생함에 따라 아래와 같이 초기화 후에 조회하도록함.
                _flexD.Binding = null;
                _flexM.Binding = null;


                object[] obj = new object[] {
                    LoginInfo.CompanyCode, 
                    m_cboCdPlant.SelectedValue.ToString(), 
                    m_bptxtCdItem.CodeValue, 
                    m_txtWoFrom.CodeValue, 
                    m_txtWoTo.CodeValue, 
                    m_dtpFrom.Text, 
                    m_dtpTo.Text, 
                    마감구분, 
                    bp프로젝트.CodeValue,
                    bp_Tp_Wo.QueryWhereIn_Pipe,
                    bp_Cd_WC.QueryWhereIn_Pipe,
                    bp_Wc_Op.QueryWhereIn_Pipe,
                    bpc_Tp_Item.QueryWhereIn_Pipe,
                    Global.MainFrame.LoginInfo.EmployeeNo,
                    chk_작업지시분할.Checked == true ? "Y" : "N",
                    chk_작업잔량여부.Checked == true ? "Y" : "N",
                    chk_재작업잔량여부.Checked == true ? "Y" : "N",
                    bp_SFT.CodeValue,

                };

                DataTable dt = _biz.Search(obj);

                _flexM.Binding = dt;

                if (!_flexM.HasNormalRow)
                {
                    ShowMessage(PageResultMode.SearchNoData);
                    return;
                }

                if (dtWork != null)
                {
                    dtWork.Rows.Clear();
                    dtWork.AcceptChanges();
                }
                if (_dtReject != null)
                {
                    _dtReject.Rows.Clear();
                    _dtReject.AcceptChanges();
                }
                if (_dtLotItem != null)
                {
                    _dtLotItem.Rows.Clear();
                    _dtLotItem.AcceptChanges();
                }
                if (_dtSERL != null)
                {
                    _dtSERL.Rows.Clear();
                    _dtSERL.AcceptChanges();
                }
                if (_dtMatl != null)
                {
                    _dtMatl.Rows.Clear();
                    _dtMatl.AcceptChanges();
                }
                if (_dtLotMatl != null)
                {
                    _dtLotMatl.Rows.Clear();
                    _dtLotMatl.AcceptChanges();
                }
                //if (dtSFT != null)
                //{
                //    dtSFT.Rows.Clear();
                //    dtSFT.AcceptChanges();
                //}

                _flexM.Enabled = true;
                _flexD.Enabled = true;

                //Error가 발생했을 때 그리드의 포커스를 가게할 컬럼을 지정한다. 디폴트로 "QT_WORK"로 지정한다.
                strErrCol = "QT_WORK";

                _flexD.Focus();
                _flexD.Col = _flexD.Cols["QT_WORK"].Index;
            }
            catch (Exception ex)
            { 
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 조회후해당행찾기

        private void 조회후해당행찾기(string strFindCol)
        {
            string strNO_WO = _flexM["NO_WO"].ToString();
            string strCD_OP = _flexD["CD_OP"].ToString();
            string strNO_REL = _flexD["NO_REL"].ToString();

            string Filter = "NO_WO = '" + _flexM["NO_WO"].ToString() + "'";

            object[] obj = new object[]{Global.MainFrame.LoginInfo.CompanyCode, 
                                                m_cboCdPlant.SelectedValue.ToString(),
                                                _flexM["NO_WO"].ToString(),
                                                bp_Tp_Wo.QueryWhereIn_Pipe,
                                                bp_Cd_WC.QueryWhereIn_Pipe,
                                                bp_Wc_Op.QueryWhereIn_Pipe,
                                                Global.MainFrame.LoginInfo.EmployeeNo,
                                                chk_작업지시분할.Checked == true ? "Y" : "N", //작업지시 분할 여부
                                                chk_작업잔량여부.Checked == true ? "Y" : "N",
                                                chk_재작업잔량여부.Checked == true ? "Y" : "N",
                                                bp_SFT.CodeValue,
                                                chK_작업지시비고적용여부.Checked == true ? "Y" : "N"
                                                };

            DataTable dt = _biz.SearchDetail(obj);

            //DataTable dt = _biz.SearchDetail(m_cboCdPlant.SelectedValue.ToString(), _flexM["NO_WO"].ToString());

            //if (_flexM.DataTable.Columns["NO_REL"] != null)
            //{
            //    _flexM.DetailGrids[0].DataTable.PrimaryKey = new DataColumn[] { 
            //    _flexM.DetailGrids[0].DataTable.Columns["NO_WO"], 
            //    _flexM.DetailGrids[0].DataTable.Columns["CD_OP"],
            //    _flexM.DetailGrids[0].DataTable.Columns["NO_REL"]
            //    };
            //}
            //else
            //{
            if (!_flexM.DataTable.Columns.Contains("NO_REL"))
            {
                _flexM.DataTable.Columns.Add("NO_REL", typeof(string));
                
            }

            if (strNO_REL == string.Empty)
            {
                _flexM.DetailGrids[0].DataTable.PrimaryKey = new DataColumn[] { 
                _flexM.DetailGrids[0].DataTable.Columns["NO_WO"], 
                _flexM.DetailGrids[0].DataTable.Columns["CD_OP"]};
            }
            else
            {
                _flexM.DetailGrids[0].DataTable.PrimaryKey = new DataColumn[] { 
                _flexM.DetailGrids[0].DataTable.Columns["NO_WO"], 
                _flexM.DetailGrids[0].DataTable.Columns["CD_OP"],
                _flexM.DetailGrids[0].DataTable.Columns["NO_REL"]};
            }
            //     };
            //}

            foreach (DataRow dr in dt.Rows)
            {
                _flexM.DetailGrids[0].DataTable.LoadDataRow(dr.ItemArray, true);
            }

            _flexM.DetailGrids[0].AcceptChanges();

            if (dtWork != null)
            {
                dtWork.Rows.Clear();
                dtWork.AcceptChanges();
            }
            if (_dtReject != null)
            {
                _dtReject.Rows.Clear();
                _dtReject.AcceptChanges();
            }
            if (_dtLotItem != null)
            {
                _dtLotItem.Rows.Clear();
                _dtLotItem.AcceptChanges();
            }
            if (_dtSERL != null)
            {
                _dtSERL.Rows.Clear();
                _dtSERL.AcceptChanges();
            }
            if (_dtMatl != null)
            {
                _dtMatl.Rows.Clear();
                _dtMatl.AcceptChanges();
            }
            if (_dtLotMatl != null)
            {
                _dtLotMatl.Rows.Clear();
                _dtLotMatl.AcceptChanges();
            }
            //if (dtSFT != null)
            //{
            //    dtSFT.Rows.Clear();
            //    dtSFT.AcceptChanges();
            //}

            if (_flexD.HasNormalRow)
            {
                int iRowD = 0;

                if (chk_작업지시분할.Checked == false)
                {
                    iRowD = _flexD.FindRow(strCD_OP, _flexD.Rows.Fixed, _flexD.Cols["CD_OP"].Index, false, true, true);
                }
                else
                {
                    iRowD = _flexD.FindRow(strNO_REL, _flexD.Rows.Fixed, _flexD.Cols["CD_OP"].Index, false, true, true);
                }
                if (iRowD < 0) iRowD = _flexD.Rows.Fixed;
                _flexD.Focus();
                _flexD.Select(iRowD, _flexD.Cols[strFindCol].Index);

                //20110120 최인성 한성욱
                //작업실적일 - 기본 셋팅
                //* 작업실적일 컨트롤
                //* 1. 000 : 컨트롤 미사용(비활성)
                //* 2. 100 : 컨트롤 사용(활성)
                if (Pr_Global.Dat_Work_Control_YN == "100")
                {
                    for (int i = 0; i < _flexD.Rows.Count - _flexD.Rows.Fixed; i++)
                    {
                        _flexD.DataView[i]["DT_WORK"] = dat_Work_Dt.Text;

                        //유효기간
                        if (ComFunc.DateCheck(dat_Work_Dt.Text))
                        {
                            DateTime date = DateTime.Parse(dat_Work_Dt.Text.Substring(0, 4) + "-" + dat_Work_Dt.Text.Substring(4, 2) + "-" + dat_Work_Dt.Text.Substring(6, 2));

                            date = date.AddDays(D.GetInt(_flexD.DataView[i]["DY_VALID"]));
                            _flexD.DataView[i]["DT_LIMIT"] = date.ToShortDateString().Replace("-", ""); ;
                        }
                        else
                        {
                            _flexD.DataView[i]["DT_LIMIT"] = dat_Work_Dt.Text;
                        }
                    }

                    _flexD.AcceptChanges();
                }
            }
        }

        #endregion

        #region -> 저장

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSave()) return;

                if (MsgAndSave(PageActionMode.Save))
                {
                    bSaveCalled = true;

                    ShowMessage(PageResultMode.SaveGood);

                    조회후해당행찾기("QT_WORK");

                }
                else
                {
                    ToolBarSaveButtonEnabled = true;
                    bSaveCalled = true;

                    string strCD_OP = drs[0]["CD_OP"].ToString();
                    int iRowD = _flexD.FindRow(strCD_OP, _flexD.Rows.Fixed, _flexD.Cols["CD_OP"].Index, false, true, true);
                    if (iRowD < 0) iRowD = _flexD.Rows.Fixed;
                    _flexD.Focus();
                    _flexD.Select(iRowD, _flexD.Cols[strErrCol].Index);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                bSaveCalled = false;
            }
        }

        #endregion

        #region -> BeforeSave Override

        protected override bool BeforeSave()
        {
            ToolBarSaveButtonEnabled = false;

            if (!base.BeforeSave()) return false;

            return true;
        }

        #endregion

        #region -> Verify Override

        protected override bool Verify()
        {
            if (!base.BeforeSave()) return false;

            dtWork = _flexD.GetChanges();

            if (dtWork == null) return false;

            if (dtWork.Rows.Count > 1)
            {
                //ShowMessage("변경이 행이 " + dtWork.Rows.Count + "개입니다. 로직상 변경된 행은 1개만 되야합니다.");
                ShowMessage("변경이 행이 @개입니다. 로직상 변경된 행은 1개만 되야합니다.", dtWork.Rows.Count);
                return false;
            }

            
            //작업지시분할로 인하여 아래와 같이 변경함.
            //drs = _flexD.DataTable.Select("NO_WO = '" + dtWork.Rows[0]["NO_WO"].ToString() + "' AND CD_OP = '" + dtWork.Rows[0]["CD_OP"].ToString() + "'");
            drs = _flexD.DataTable.Select("NO_WO = '" + dtWork.Rows[0]["NO_WO"].ToString() + "' AND CD_OP = '" + dtWork.Rows[0]["CD_OP"].ToString() + "' AND ISNULL(NO_REL, '') = '" + dtWork.Rows[0]["NO_REL"].ToString() + "'");


            //시스템 통제쪽에서 LOT을 사용하지 않으면 LOT은 아무의미가 없다.
            //또한 품목이 LOT를 사용하지 않으면 아무 의미가 없다.
            if (LoginInfo.MngLot == "Y" && _flexM["YN_LOT"].ToString() == "Y")
            {
                //씨젠인 경우에 LOT번호가 없다면 자동으로 채번하도록 한다.
                if (Global.MainFrame.ServerKeyCommon.ToUpper() == "SQL_108" || Global.MainFrame.ServerKeyCommon.ToUpper() == "DZSQL"
                    || Global.MainFrame.ServerKeyCommon.ToUpper() == "SEEGENE")
                {
                    if (dtWork.Rows[0]["NO_LOT"].ToString() == string.Empty)
                    {
                        dtWork.Rows[0]["NO_LOT"] = (string)GetSeq(LoginInfo.CompanyCode, "PR", "71", drs[0]["DT_WORK"].ToString().Substring(0, 6));
                        drs[0]["NO_LOT"] = dtWork.Rows[0]["NO_LOT"];
                    }
                }

                //작업지시에서 NOT_NO 를 미리 넣은 경우는 입력할 필요가 없다.
                if (_flexM["NO_LOT"].ToString().Trim().Length == 0)
                {
                    //1. 공장환경설정의 LOT필수여부 가 'Y' 이거나
                    //2. 공장환경설정의 LOT필수여부 가 'N' 이고 마지막공정인 경우
                    //---> 위의 1번, 2번 중 하나에 해당되면서 초기공정이면(if절 안의 CD_OP_BASE 조건) 실적을 입력해야한다.
                    if ((YN_PR_MNG_LOT == "Y") || (YN_PR_MNG_LOT == "N" && drs[0]["YN_FINAL"].ToString() == "Y"))
                    {
                        //현재 입력할 실적의 공정이 작지의 초기공정(CD_OP_BASE)이면 LOT번호를 필수로 입력해야한다.
                        if (drs[0]["CD_OP_BASE"].ToString().Trim() != "")
                        {
                            if (drs[0]["NO_LOT"].ToString() == "")
                            {
                               // string strMessage = "LOT번호를 필수로 입력해야 한다.";

                                if (YN_PR_MNG_LOT == "N")
                                {
                                    ShowMessage("LOT번호가 필수가 아니더라도 마지막 공정이면서 작업지시의 상태가 생산품자동입고처리일때는\nLOT번호를 필수로 입력해야 한다");
                                }

                                strErrCol = "NO_LOT";
                                ShowMessage("@ 를 바르게 입력하세요", DD("LOT번호"));
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    //시스템 통제쪽에서 LOT을 사용하면서 품목이 LOT를 사용하는 경우
                    //작업지시시점에서 NOT_NO 을 넣은 경우는 실적의 NO_LOT을 작업지시품목의 LOT으로 넣어준다.
                    dtWork.Rows[0]["NO_LOT"] = _flexM["NO_LOT"];
                }
            }

            decimal reject = _flexD.CDecimal(drs[0]["QT_REJECT"]);

            if (reject > 0)
            {
                if (_dtReject == null || _dtReject.Rows.Count < 1)
                {
                    ShowMessage(공통메세지._이가존재하지않습니다, DD("불량내역"));

                    if (!ShowRejectHelp())
                    {
                        _flexD.Focus();
                        return false;
                    }
                    else
                        return true;
                }
                else
                {
                    //불량내역 조정시 기존 수량과 실적입력에 입력된 불량수량과 다시한번 비교하는 로직 추가
                    decimal dQt_Reject_Sum = D.GetDecimal(_dtReject.Compute("SUM(QT_REJECT)", ""));

                    if (dQt_Reject_Sum != reject)
                    {
                        ShowMessage("불량내역에 입력된 수량과 실적불량수량이 일치 하지 않습니다\n 입력된 불량내역은 모두 삭제됩니다.");
                        _dtReject.Clear();
                        return false;
                    }
                }
            }

            return base.Verify();
        }

        #endregion

        #region -> 실제저장구문(SaveData)

        protected override bool SaveData()
        {
            if (!Verify()) return false;

            string strNO_II = "";

            string str입고창고코드 = "";

            DataSet ds = new DataSet();
            DataTable dt_Manday = new DataTable();          //작업공수 테이블
            DataTable dt_Auto_Bad_Work = new DataTable();   //자동 불량처리내역를 실적테이블
            DataTable dt_Auto_Bad = new DataTable();        //자동 불량처리내역 테이블
            DataTable dt_AutoBad_ReqH = new DataTable();    //자동 불량의뢰처리 H 테이블
            DataTable dt_AutoBad_ReqL = new DataTable();    //자동 불량의뢰처리 L 테이블

            DataTable dt_Item_Location = null;              //제품Location
            DataTable dt_Matl_Location = null;              //자제Location

            DataTable dt_Use_Matl = null; //원지 사용량

            if (_flexD.HasNormalRow)
            {
                string day = dtWork.Rows[0]["DT_WORK"].ToString();
                string no = (string)GetSeq(LoginInfo.CompanyCode, "PR", "05", day.Substring(0, 6));

                if (day == "")
                    day = MainFrameInterface.GetStringToday.Substring(0, 6) + "01";
                
                if (no == "")
                {
                    ShowMessage(공통메세지.자료저장중오류가발생하였습니다);
                    return false;
                }
                else
                {
                    DataRow[] dr_Select = _flexD.DataTable.Select("NO_WO = '" + dtWork.Rows[0]["NO_WO"].ToString() + "' AND CD_OP ='" + dtWork.Rows[0]["CD_OP"].ToString() + "' AND NO_LINE =" + dtWork.Rows[0]["NO_LINE"].ToString());

                    dtWork.Rows[0]["DT_WORK"] = day;
                    dtWork.Rows[0]["NO_WORK"] = no;
                    dtWork.Rows[0]["YN_REWORK"] = "N";

                    if (_dtReject != null)
                    {
                        for (int i = 0; i < _dtReject.Rows.Count; i++)
                        {
                            _dtReject.Rows[i]["QT_WORK"] = dtWork.Rows[0]["QT_WORK"];
                            _dtReject.Rows[i]["NO_WORK"] = no;
                        }
                    }

                    //현재 입력할 실적의 공정이 작지의 초기공정(CD_OP_BASE)이 아니면 공정제품Lot도움창을 띄운다.
                    //실적수량(QT_WORK)과 불량수랭(QT_REJECT)이 같으면 이동할 수량이 없으므로 이때는 도움창을 띄우지 않는다.
                    if (
                        //(drs[0]["CD_OP_BASE"].ToString().Trim() == "" &&
                        //_flexD.CDecimal(dtWork.Rows[0]["QT_WORK"]) - _flexD.CDecimal(dtWork.Rows[0]["QT_REJECT"]) != 0) ||
                        //(drs[0]["YN_FINAL"].ToString() == "Y" &&
                        //_flexD.CDecimal(dtWork.Rows[0]["QT_WORK"]) - _flexD.CDecimal(dtWork.Rows[0]["QT_REJECT"]) != 0 &&
                        LoginInfo.MngLot == "Y" && _flexM["YN_LOT"].ToString() == "Y" && drs[0]["NO_LOT"].ToString().Trim() == "")
                    {
                        DataTable dtWorkCopy = dtWork.Copy();

                        dtWorkCopy.Columns["DT_WORK"].ColumnName = "DT_IO";

                        dtWorkCopy.Columns.Add("QT_IO", typeof(decimal), "QT_WORK - QT_REJECT");

                        P_PR_LOT_ITEM_SUB dlgItem = new P_PR_LOT_ITEM_SUB(dtWorkCopy, YN_PR_MNG_LOT, "N");

                        if (dlgItem.ShowDialog() != DialogResult.OK) return false;

                        if (dlgItem.dtL != null && dlgItem.dtL.Rows.Count > 0)
                            _dtLotItem = dlgItem.dtL.Copy();
                    }

                    #region -> 제품 Lot 공정수불번호와 항번을 구하는 로직

                    foreach (DataRow drWork in dtWork.Rows)
                    {
                        //공정수불(공정이동입고, 공정이동출고)
                        drWork["NO_IO_202_102"] = (string)GetSeq(LoginInfo.CompanyCode, "PR", "06", day.Substring(0, 6));
                        //공정수불(생산출고)
                        drWork["NO_IO_203"] = (string)GetSeq(LoginInfo.CompanyCode, "PR", "13", day.Substring(0, 6));

                        drWork["NO_LINE_202"] = 1;   //공정이동입고 항번
                        drWork["NO_LINE_102"] = 2;   //공정이동출고 항번
                        drWork["NO_LINE_203"] = 1;   //생산출고 항번

                        //제품공정이 없으면 의미가 없다. 첫번째 공정에서는 입력하는 LOT_NO를 KEY 입력을 하므로 대상에서 제외
                        if (_dtLotItem != null && _dtLotItem.Rows.Count > 0)
                        {
                            DataRow[] rows = _dtLotItem.Select("");

                            foreach (DataRow drLot in rows)
                            {
                                drLot["NO_IO_202_102"] = drWork["NO_IO_202_102"];
                                drLot["NO_IO_203"] = drWork["NO_IO_203"];
                                drLot["NO_LINE_202"] = drWork["NO_LINE_202"];
                                drLot["NO_LINE_102"] = drWork["NO_LINE_102"];
                                drLot["NO_LINE_203"] = drWork["NO_LINE_203"];

                                drLot["CD_OP"] = drWork["CD_OP"];
                                drLot["CD_WC"] = drWork["CD_WC"];
                            }
                        }
                    }

                    #endregion

                    #region -> LOT번호 중복여부체크하여(실제로 PR_QTIOLOT의 P.K. 로 체크)

                    if (_dtLotItem != null && _dtLotItem.Rows.Count > 0)
                    {
                        DataRow[] drsCheckNO_LOT = _dtLotItem.Select("CHK = 'Y'");

                        foreach (DataRow dr in drsCheckNO_LOT)
                        {
                            st.NO_IO = dr["NO_IO_202_102"].ToString();
                            st.NO_LINE2 = _flexD.CInt32(dr["NO_IOLINE2"]);
                            st.NO_LOT = dr["NO_LOT"].ToString();

                            중복체크함수(ref st);
                            if (_flexD.CDecimal(dr["NO_IOLINE2"]) != _flexD.CDecimal(st.NO_LINE2))
                                dr["NO_IOLINE2"] = _flexD.CDecimal(st.NO_LINE2);

                            st.NO_IO = dr["NO_IO_203"].ToString();
                            st.NO_LINE2 = _flexD.CInt32(dr["NO_IOLINE2"]);
                            st.NO_LOT = dr["NO_LOT"].ToString();

                            중복체크함수(ref st);
                            if (_flexD.CDecimal(dr["NO_IOLINE2"]) != _flexD.CDecimal(st.NO_LINE2))
                                dr["NO_IOLINE2"] = _flexD.CDecimal(st.NO_LINE2);
                        }
                    }

                    #endregion



                    string str입고창고명 = "";

                    if ((Global.MainFrame.ServerKeyCommon.Contains("SRPACK")) && D.GetString(dtWork.Rows[0]["USE_MAT"]) == "100")
                    {
                        #region -> 원지 사용량 조정 도움창을 띄운다

                        P_PR_USE_MATL_SUB dlgMatl = new P_PR_USE_MATL_SUB(dtWork.Rows[0], _flexM["CD_SL"].ToString(), _flexM["NM_SL"].ToString(), no);

                        if (dlgMatl.ShowDialog() != DialogResult.OK && dlgMatl.DialogResult != DialogResult.Ignore) return false;

                        if (dlgMatl.HasMatl)
                        {
                            if (dlgMatl.DialogResult == DialogResult.OK)
                            {
                                if (dlgMatl is IHelpWindow)
                                {
                                    object[] Matl_Sub = ((IHelpWindow)dlgMatl).ReturnValues;

                                    dtWork.Rows[0]["TM_LABOR"] = "000000";
                                    dtWork.Rows[0]["TM_MACH"] = "000000";
                                    dtWork.Rows[0]["QT_RSRC_LABOR"] = 0;

                                    _dtMatl = (DataTable)Matl_Sub[1];

                                    //라인번호를 생성하기 위함
                                    int iRowCont = 1;

                                    foreach (DataRow drRow in _dtMatl.Rows)
                                    {
                                        if (drRow.RowState == DataRowState.Deleted)
                                            continue;

                                        drRow["QT_INPUT"] = drRow["QT_BL"];//원지사용량이 투입수량
                                        drRow["DT_WORK"] = dtWork.Rows[0]["DT_WORK"];
                                        drRow["NO_WO"] = dtWork.Rows[0]["NO_WO"].ToString();
                                        drRow["NO_LINE"] = iRowCont++;
                                    }

                                    //원지 사용량 테이블 복사
                                    dt_Use_Matl = _dtMatl.Copy();

                                    //Lot
                                    DataTable dt = Matl_Sub[3] as DataTable;

                                    if (LoginInfo.MngLot == "Y" && dt != null)//우리텍때문에 쉉하였음.
                                    {

                                        DataTable dtCopy = dt.Clone();

                                        DataRow[] drsInsert = dt.Select("", "", DataViewRowState.CurrentRows);

                                        foreach (DataRow drInsert in drsInsert)
                                        {
                                            dtCopy.Rows.Add(drInsert.ItemArray);
                                        }

                                        _dtLotMatl = dtCopy;
                                    }
                                }
                                else
                                {
                                    return false;
                                }

                                #region -> 자재 Lot 공정수불번호와 항번을 구하는 로직

                                string strNO_IO_MM = "";

                                _dtMatl.AcceptChanges();

                                foreach (DataRow dr in _dtMatl.Rows)
                                {
                                    dr.SetAdded();
                                }

                                if (_dtMatl.Rows.Count > 0)
                                {
                                    strNO_IO_MM = (string)GetSeq(LoginInfo.CompanyCode, "PU", "17", day.Substring(0, 6));
                                    strNO_II = (string)GetSeq(LoginInfo.CompanyCode, "PR", "04", day.Substring(0, 6));
                                }

                                foreach (DataRow drMatl in _dtMatl.Rows)
                                {
                                    if (drMatl.RowState == DataRowState.Deleted)
                                        continue;

                                    //공정수불(투입)
                                    //drMatl["NO_IO_201"] = (string)GetSeq(LoginInfo.CompanyCode, "PR", "04", day.Substring(0, 6));
                                    drMatl["NO_IO_201"] = strNO_II;

                                    //공정수불(생산입고) -> B/F 인 경우만 생성
                                    if (drMatl["YN_BF"].ToString() == "Y")
                                    {
                                        drMatl["NO_IO_101"] = (string)GetSeq(LoginInfo.CompanyCode, "PR", "11", day.Substring(0, 6));
                                        drMatl["NO_IO_MM"] = strNO_IO_MM;
                                    }

                                    //Lot을 사용하지 않으면 _dtLotMatl 테이블이 의미가 없다.
                                    if (_dtLotMatl != null && _dtLotMatl.Rows.Count > 0)
                                    {
                                        string filter = string.Format("CD_PLANT = '{0}' AND CD_ITEM = '{1}' AND CD_SL = '{2}' AND BARCODE = '{3}'",
                                                        drMatl["CD_PLANT"].ToString(), 
                                                        drMatl["CD_MATL"].ToString(), 
                                                        drMatl["CD_SL"].ToString(),
                                                        drMatl["BARCODE"].ToString());

                                        DataRow[] rows = _dtLotMatl.Select(filter, "", DataViewRowState.CurrentRows);

                                        foreach (DataRow drLot in rows)
                                        {
                                            drLot["DT_IO"] = dtWork.Rows[0]["DT_WORK"];
                                            drLot["NO_IO_201"] = drMatl["NO_IO_201"];
                                            drLot["NO_IO_101"] = drMatl["NO_IO_101"];
                                            drLot["NO_IO_MM"] = drMatl["NO_IO_MM"];
                                            drLot["NO_LINE"] = drMatl["NO_LINE"];
                                        }
                                    }
                                }

                                #endregion

                                
                            }
                        }
                        else
                        {
                            if (dlgMatl.DialogResult == DialogResult.OK)
                            {
                                object[] Matl_Sub = ((IHelpWindow)dlgMatl).ReturnValues;
                                DataRow dr = (DataRow)Matl_Sub[0];

                                dtWork.Rows[0]["TM_LABOR"] = "000000";
                                dtWork.Rows[0]["TM_MACH"] = "000000";
                                dtWork.Rows[0]["QT_RSRC_LABOR"] = 0;
                            }
                        }

                        str입고창고코드 = dlgMatl.입고창고코드;
                        str입고창고명 = dlgMatl.입고창고명;

                        #endregion
                    }
                    else
                    {

                        #region -> 자재 도움창을 띄운다(자재수량변경과 자재 Lot 구해오는 도움창)

                        P_PR_INPUT_MATL_SUB dlgMatl = new P_PR_INPUT_MATL_SUB(dtWork.Rows[0], _flexM["CD_SL"].ToString(), _flexM["NM_SL"].ToString());

                        if (dlgMatl.ShowDialog() != DialogResult.OK && dlgMatl.DialogResult != DialogResult.Ignore) return false;

                        //자재도움창에서 받아온 자재가 없으면 이 구문을 실행하지 않는다.
                        if (dlgMatl.HasMatl)
                        {
                            if (dlgMatl.DialogResult == DialogResult.OK)
                            {
                                if (dlgMatl is IHelpWindow)
                                {
                                    object[] Matl_Sub = ((IHelpWindow)dlgMatl).ReturnValues;
                                    DataRow dr = (DataRow)Matl_Sub[0];
                                    dtWork.Rows[0]["FG_MOVE"] = dr["FG_MOVE"];
                                    dtWork.Rows[0]["FG_ISU"] = dr["FG_ISU"];
                                    dtWork.Rows[0]["FG_CLOSE"] = dr["FG_CLOSE"];
                                    dtWork.Rows[0]["CD_RSRC_LABOR"] = dr["CD_RSRC_LABOR"];
                                    dtWork.Rows[0]["TM_LABOR"] = dr["TM_LABOR"];
                                    dtWork.Rows[0]["CD_RSRC_MACH"] = dr["CD_RSRC_MACH"];
                                    dtWork.Rows[0]["TM_MACH"] = dr["TM_MACH"];
                                    dtWork.Rows[0]["QT_RSRC_LABOR"] = dr["QT_RSRC_LABOR"];

                                    _dtMatl = (DataTable)Matl_Sub[1];
                                    foreach (DataRow drRow in _dtMatl.Rows)
                                    {
                                        if (drRow.RowState == DataRowState.Deleted)
                                            continue;

                                        drRow["DT_WORK"] = dtWork.Rows[0]["DT_WORK"];
                                    }

                                    DataTable dt = Matl_Sub[2] as DataTable;

                                    //dt = null 인 경우는 lot를 사용하지 않거나 lot 품목이 0개인 경우다.
                                    if (LoginInfo.MngLot == "Y" && dt != null)//우리텍때문에 쉉하였음.
                                    //if (LoginInfo.MngLot == "Y" && _flexM["YN_LOT"].ToString() == "Y" && dt != null)
                                    {
                                        DataTable dtCopy = dt.Clone();

                                        DataRow[] drsInsert = dt.Select("", "", DataViewRowState.CurrentRows);

                                        foreach (DataRow drInsert in drsInsert)
                                        {
                                            dtCopy.Rows.Add(drInsert.ItemArray);
                                        }

                                        _dtLotMatl = dtCopy;
                                    }
                                }
                                else
                                {
                                    return false;
                                }

                                #region -> 자재 Lot 도움창을 띄워서 구하는 로직(자재 도움창과 통합으로 주석처리)

                                ////시스템 통제쪽에서 LOT을 사용하지 않으면 LOT은 아무의미가 없다.
                                ////또한 품목이 LOT를 사용하지 않으면 아무 의미가 없다.
                                ////또 작업지시에서 NOT_NO 를 미리 넣은 경우도 자재 LOT은 필요하다.
                                //if (LoginInfo.MngLot == "Y" && _flexM["YN_LOT"].ToString() == "Y")
                                //{
                                //    DataRow[] drsMatl = _dtMatl.Select("YN_LOT = 'Y'");

                                //    DataTable dtMatl = _dtMatl.Clone();

                                //    foreach (DataRow dr in drsMatl)
                                //    {
                                //        dtMatl.Rows.Add(dr.ItemArray);
                                //    }

                                //    dtMatl.Columns["CD_MATL"].ColumnName = "CD_ITEM";
                                //    dtMatl.Columns["QT_INPUT"].ColumnName = "QT_IO";
                                //    dtMatl.Columns["DT_WORK"].ColumnName = "DT_IO";

                                //    P_PR_LOT_MATL_SUB dlgLot = new P_PR_LOT_MATL_SUB(dtMatl, "N");

                                //    if (dlgLot.ShowDialog() != DialogResult.OK) return false;

                                //    _dtLotMatl = dlgLot.dtL.Copy();
                                //}

                                #endregion

                                #region -> 자재 Lot 공정수불번호와 항번을 구하는 로직

                                string strNO_IO_MM = "";

                                _dtMatl.AcceptChanges();

                                foreach (DataRow dr in _dtMatl.Rows)
                                {
                                    dr.SetAdded();
                                }

                                if (_dtMatl.Rows.Count > 0)
                                {
                                    strNO_IO_MM = (string)GetSeq(LoginInfo.CompanyCode, "PU", "17", day.Substring(0, 6));
                                    strNO_II = (string)GetSeq(LoginInfo.CompanyCode, "PR", "04", day.Substring(0, 6));
                                }

                                foreach (DataRow drMatl in _dtMatl.Rows)
                                {
                                    if (drMatl.RowState == DataRowState.Deleted)
                                        continue;

                                    //공정수불(투입)
                                    //drMatl["NO_IO_201"] = (string)GetSeq(LoginInfo.CompanyCode, "PR", "04", day.Substring(0, 6));
                                    drMatl["NO_IO_201"] = strNO_II;

                                    //공정수불(생산입고) -> B/F 인 경우만 생성
                                    if (drMatl["YN_BF"].ToString() == "Y")
                                    {
                                        drMatl["NO_IO_101"] = (string)GetSeq(LoginInfo.CompanyCode, "PR", "11", day.Substring(0, 6));
                                        drMatl["NO_IO_MM"] = strNO_IO_MM;
                                    }

                                    //Lot을 사용하지 않으면 _dtLotMatl 테이블이 의미가 없다.
                                    if (_dtLotMatl != null && _dtLotMatl.Rows.Count > 0)
                                    {
                                        string CD_PLANT = D.GetString(drMatl["CD_PLANT"]);
                                        string CD_ITEM = D.GetString(drMatl["CD_MATL"]);
                                        string CD_SL = D.GetString(drMatl["CD_SL"]);
                                        string NO_SEQ = D.GetString(drMatl["NO_LINE"]);

                                        string filter = "CD_PLANT = '" + CD_PLANT + "' AND CD_ITEM = '" + CD_ITEM + "' AND CD_SL = '" + CD_SL + "' AND NO_SEQ = '" + NO_SEQ + "'";
                                        DataRow[] rows = _dtLotMatl.Select(filter, "", DataViewRowState.CurrentRows);
                                        //DataRow[] rows = _dtLotMatl.Select("CD_PLANT = '" + drMatl["CD_PLANT"].ToString() + "' AND CD_ITEM = '" + drMatl["CD_MATL"].ToString() + "' ");

                                        foreach (DataRow drLot in rows)
                                        {
                                            drLot["NO_IO_201"] = drMatl["NO_IO_201"];
                                            drLot["NO_IO_101"] = drMatl["NO_IO_101"];
                                            drLot["NO_IO_MM"] = drMatl["NO_IO_MM"];
                                            drLot["NO_LINE"] = drMatl["NO_LINE"];
                                        }
                                    }
                                }

                                #endregion
                            }
                        }
                        else
                        {
                            if (dlgMatl.DialogResult == DialogResult.OK)
                            {
                                object[] Matl_Sub = ((IHelpWindow)dlgMatl).ReturnValues;
                                DataRow dr = (DataRow)Matl_Sub[0];

                                dtWork.Rows[0]["FG_MOVE"] = dr["FG_MOVE"];
                                dtWork.Rows[0]["FG_ISU"] = dr["FG_ISU"];
                                dtWork.Rows[0]["FG_CLOSE"] = dr["FG_CLOSE"];
                                dtWork.Rows[0]["CD_RSRC_LABOR"] = dr["CD_RSRC_LABOR"];
                                dtWork.Rows[0]["TM_LABOR"] = dr["TM_LABOR"];
                                dtWork.Rows[0]["CD_RSRC_MACH"] = dr["CD_RSRC_MACH"];
                                dtWork.Rows[0]["TM_MACH"] = dr["TM_MACH"];
                                dtWork.Rows[0]["QT_RSRC_LABOR"] = dr["QT_RSRC_LABOR"];
                            }
                        }

                        str입고창고코드 = dlgMatl.입고창고코드;
                        str입고창고명 = dlgMatl.입고창고명;

                        #endregion
                    }

                    #region -> SERIAL 사용시 SERIAL 도움창 띄우는 구문(우선은 주석처리)

                    if (App.SystemEnv.SERIAL사용 && _flexM["YN_SERL"].ToString() == "Y" && drs[0]["YN_FINAL"].ToString() == "Y" && drs[0]["YN_AUTORCV"].ToString() == "Y" && Convert.ToDecimal(drs[0]["QT_WORK"].ToString()) - Convert.ToDecimal(drs[0]["QT_REJECT"].ToString()) > 0)
                    {
                        //_dtSERL 테이블을 사용해야함...

                        //Test DataTable 생성
                        DataTable dtTest = drs[0].Table.Clone();

                        dtTest.Columns.Add("NO_IO", typeof(string));
                        dtTest.Columns.Add("NO_IOLINE", typeof(decimal));
                        dtTest.Columns.Add("DT_IO", typeof(string));
                        dtTest.Columns.Add("FG_IO", typeof(string));
                        dtTest.Columns.Add("CD_SL", typeof(string));
                        dtTest.Columns.Add("NM_SL", typeof(string));
                        dtTest.Columns.Add("CD_QTIOTP", typeof(string));
                        dtTest.Columns.Add("QT_GOOD_INV", typeof(decimal));

                        dtTest.Columns["NO_IO"].DefaultValue = "";
                        dtTest.Columns["NO_IOLINE"].DefaultValue = 0d;
                        dtTest.Columns["DT_IO"].DefaultValue = drs[0]["DT_WORK"].ToString();
                        dtTest.Columns["FG_IO"].DefaultValue = "002";
                        dtTest.Columns["CD_SL"].DefaultValue = _flexM["CD_SL"].ToString();
                        dtTest.Columns["NM_SL"].DefaultValue = _flexM["NM_SL"].ToString();
                        dtTest.Columns["CD_QTIOTP"].DefaultValue = _flexM["TP_GR"].ToString();
                        dtTest.Columns["QT_GOOD_INV"].DefaultValue = Convert.ToDecimal(drs[0]["QT_WORK"].ToString()) - Convert.ToDecimal(drs[0]["QT_REJECT"].ToString());

                        dtTest.Rows.Add(drs[0].ItemArray);

                        pur.P_PU_SERL_SUB_R dlgSerl = new pur.P_PU_SERL_SUB_R(dtTest, "PR");

                        dlgSerl.ShowDialog();

                        if (dlgSerl.DialogResult != DialogResult.OK && dlgSerl.DialogResult != DialogResult.Ignore) return false;

                        if (dlgSerl.DialogResult == DialogResult.OK)
                        {
                            _dtSERL = dlgSerl.dtL;
                        }
                    }

                    #endregion

                    #region -> 작업실적 건에 대한 공수 관리
                    DataSet dr_Plant = _biz.Search_PlantSetting(new object[] {Global.MainFrame.LoginInfo.CompanyCode,
                                                                              D.GetString(_flexD["CD_PLANT"])});

                    if (dr_Plant != null && dr_Plant.Tables[1].Rows.Count != 0)
                    {
                        if (D.GetString(dr_Plant.Tables[1].Rows[0]["YN_LINK_HR"]) == "Y")
                        {
                            P_PR_WORK_MANDAY_SUB dlg_Manday = new P_PR_WORK_MANDAY_SUB(dtWork.Rows[0]);

                            if (dlg_Manday.ShowDialog() != DialogResult.OK) return false;

                            dt_Manday = dlg_Manday.Return_Dt;
                        }
                    }

                    #endregion

                    #region 재작업 처리 및 자동 불량 처리
                    if (_dtReject != null && _dtReject.Rows.Count > 0)
                    {
                        #region 재작업처리
                        //20110418 최인성 김성호 김광석
                        //공장환경설정 프로젝트 사용시 프로젝트 값 필수
                         ds = Pr_ComFunc.Get_Plant_Cfg(new object[] 
                                                        {Global.MainFrame.LoginInfo.CompanyCode,
                                                         dtWork.Rows[0]["CD_PLANT"].ToString() });
                        //자동공정불량처리 사용여부
                        if (D.GetString(ds.Tables[1].Rows[0]["YN_AUTOBAD"]) == "Y" && (D.GetString(ds.Tables[1].Rows[0]["FG_AUTOBAD"]) == "000" || D.GetString(ds.Tables[1].Rows[0]["FG_AUTOBAD"]) == "001"))
                        {
                            //자동공정불량처리 건이 있는지 체크
                            DataRow[] dr_Reject_Select = _dtReject.Select("ISNULL(CHK, 'N') = 'Y'");

                            if (dr_Reject_Select != null && dr_Reject_Select.Length > 0)
                            {
                                #region 불량내역자동처리
                                
                                //공정불량처리를 위한 실적번호 채번로직
                                day = dat_Work_Dt.Text;
                                no = (string)Global.MainFrame.GetSeq(Global.MainFrame.LoginInfo.CompanyCode, "PR", "05", day.Substring(0, 6));

                                if (day == "")
                                    day = Global.MainFrame.GetStringToday.Substring(0, 6) + "01";

                                #region 불량처리내역 생성
                                dt_Auto_Bad = _dtReject.Clone();

                                foreach (DataRow dr in dr_Reject_Select)
                                {
                                    dt_Auto_Bad.ImportRow(dr);
                                    dt_Auto_Bad.Rows[dt_Auto_Bad.Rows.Count - 1]["NO_WORK"] = no;
                                }
                                #endregion

                                #region 불량처리내역에 대한 실적 및 공정 수불 조정
                                //자동으로 불량을 처리할 수량을 만든다.
                                decimal d_Qty_Bad_Sum = D.GetDecimal(dt_Auto_Bad.Compute("SUM(QT_REJECT)", "ISNULL(CHK, 'N') = 'Y'"));

                                
                                dt_Auto_Bad_Work = dtWork.Clone();
                                dt_Auto_Bad_Work.ImportRow(dtWork.Rows[0]);

                                dt_Auto_Bad_Work.Columns.Add("NO_PO", typeof(string));
                                dt_Auto_Bad_Work.Columns.Add("NO_POLINE", typeof(decimal));


                                //공정수불(공정이동입고, 공정이동출고)
                                dt_Auto_Bad_Work.Rows[0]["NO_IO_202_102"] = (string)Global.MainFrame.GetSeq(Global.MainFrame.LoginInfo.CompanyCode, "PR", "06", day.Substring(0, 6));
                                //공정수불(생산출고)
                                dt_Auto_Bad_Work.Rows[0]["NO_IO_203"] = (string)Global.MainFrame.GetSeq(Global.MainFrame.LoginInfo.CompanyCode, "PR", "13", day.Substring(0, 6));

                                dt_Auto_Bad_Work.Rows[0]["NO_LINE_202"] = 1;   //공정이동입고 항번
                                dt_Auto_Bad_Work.Rows[0]["NO_LINE_102"] = 2;   //공정이동출고 항번
                                dt_Auto_Bad_Work.Rows[0]["NO_LINE_203"] = 1;   //생산출고 항번


                                dt_Auto_Bad_Work.Rows[0]["DT_WORK"] = dtWork.Rows[0]["DT_WORK"];
                                dt_Auto_Bad_Work.Rows[0]["NO_WORK"] = no;

                                dt_Auto_Bad_Work.Rows[0]["QT_WORK"] = d_Qty_Bad_Sum;
                                //실제 이동수량을 만든다(실적수량에서 불량수량을 구한다)
                                dt_Auto_Bad_Work.Rows[0]["QT_MOVE"] = D.GetDecimal(dtWork.Rows[0]["QT_WORK"]) - d_Qty_Bad_Sum;
                                dt_Auto_Bad_Work.Rows[0]["QT_REJECT"] = 0;
                                dt_Auto_Bad_Work.Rows[0]["YN_REWORK"] = "N";
                                dt_Auto_Bad_Work.Rows[0]["YN_BAD_PROC"] = "Y";
                                dt_Auto_Bad_Work.Rows[0]["DC_REJECT"] = "";
                                dt_Auto_Bad_Work.Rows[0]["FG_MOVE"] = "Y";
                                dt_Auto_Bad_Work.Rows[0]["NO_PO"] = string.Empty;
                                dt_Auto_Bad_Work.Rows[0]["NO_POLINE"] = 0;
                                #endregion

                                #endregion

                                if (D.GetString(ds.Tables[1].Rows[0]["YN_AUTOBAD_REQ"]) == "Y")
                                {
                                    #region 불량자동입고의뢰 및 처리
                                    string sNo_Io = string.Empty;
                                    string sYN_AUTOBAD_RCV = D.GetString(ds.Tables[1].Rows[0]["YN_AUTOBAD_RCV"]);

                                    //불량처리의뢰에 대한 스키마를 가져온다.
                                    ds = _biz.Search_AutoBad_Req(new object[] {string.Empty, string.Empty, string.Empty});

                                    dt_AutoBad_ReqH = ds.Tables[0].Clone();
                                    dt_AutoBad_ReqH.Columns.Add("NO_IO",typeof(string));
                                    dt_AutoBad_ReqH.Columns.Add("CD_DEPT", typeof(string));
                                    dt_AutoBad_ReqH.Columns.Add("YN_AUTOBAD_RCV", typeof(string));//자동입고처리여부

                                    dt_AutoBad_ReqL = ds.Tables[1].Clone();
                                    dt_AutoBad_ReqL.Columns.Add("NO_IO", typeof(string));
                                    dt_AutoBad_ReqL.Columns.Add("QT_INSP", typeof(decimal));//무조건0값인가???
                                    dt_AutoBad_ReqL.Columns.Add("YN_AUTOBAD_RCV", typeof(string));//자동입고처리여부
                                    
                                    


                                    day = dtWork.Rows[0]["DT_WORK"].ToString();
                                    no = (string)GetSeq(LoginInfo.CompanyCode, "PR", "06", dat_Work_Dt.Text);

                                    dt_AutoBad_ReqH.Rows.Add(dt_AutoBad_ReqH.NewRow());
                                    dt_AutoBad_ReqH.Rows[0]["CD_PLANT"] = dtWork.Rows[0]["CD_PLANT"].ToString();
                                    dt_AutoBad_ReqH.Rows[0]["NO_REQ"] = no;
                                    dt_AutoBad_ReqH.Rows[0]["DT_REQ"] = dtWork.Rows[0]["DT_WORK"];
                                    dt_AutoBad_ReqH.Rows[0]["NO_EMP"] = dtWork.Rows[0]["NO_EMP"];
                                    dt_AutoBad_ReqH.Rows[0]["CD_DEPT"] = dtWork.Rows[0]["CD_DEPT"];

                                    //자동입고처리시에만 채번
                                    if (sYN_AUTOBAD_RCV == "Y")
                                    {
                                        sNo_Io= (string)GetSeq(LoginInfo.CompanyCode, "PU", "18", dat_Work_Dt.Text);

                                        dt_AutoBad_ReqH.Rows[0]["NO_IO"] = sNo_Io;
                                        dt_AutoBad_ReqH.Rows[0]["YN_AUTOBAD_RCV"] = sYN_AUTOBAD_RCV;//자동입고처리여부
                                    }

                                    int iRowCnt = 1;

                                    foreach (DataRow dr in dt_Auto_Bad.Rows)
                                    {
                                        if (D.GetString(dr["CD_SL_BAD"]) == string.Empty)
                                        {
                                            Global.MainFrame.ShowMessage("자동입고처리가 체크되어있는 경우 불량창고는 필수 항목입니다.");
                                            return false;
                                        }

                                        DataRow dr_Ins = dt_AutoBad_ReqL.NewRow();

                                        dr_Ins["CD_PLANT"] = dtWork.Rows[0]["CD_PLANT"];
                                        dr_Ins["NO_REQ"] = no;
                                        dr_Ins["NO_LINE"] = iRowCnt++;
                                        dr_Ins["CD_WC"] = dtWork.Rows[0]["CD_WC"];
                                        dr_Ins["CD_ITEM"] = dtWork.Rows[0]["CD_ITEM"];
                                        dr_Ins["DT_REQ"] = dtWork.Rows[0]["DT_WORK"];
                                        dr_Ins["QT_REQ"] = dr["QT_REJECT"];
                                        dr_Ins["QT_REQ_W"] = 0;
                                        dr_Ins["QT_REQ_B"] = 0;
                                        //dr_Ins["YN_QC"] = "";//검사로직인데 이건 잘모르겠음.
                                        dr_Ins["QT_RCV"] = 0;
                                        dr_Ins["CD_SL"] = dr["CD_SL_BAD"];
                                        dr_Ins["NO_WO"] = dr["NO_WO"];
                                        dr_Ins["NO_WORK"] = dr["NO_WORK"];
                                        dr_Ins["TP_WB"] = "0";//????
                                        dr_Ins["TP_GR"] = "981";//입고형태를 절대값으로 주어도 돼는지 모르겠음.
                                        dr_Ins["NO_IO"] = sNo_Io;//자동입고처리 처리번호
                                        dr_Ins["YN_AUTOBAD_RCV"] = sYN_AUTOBAD_RCV;//자동입고처리여부
                                        dr_Ins["NO_EMP"] = dtWork.Rows[0]["NO_EMP"];
                                        dt_AutoBad_ReqL.Rows.Add(dr_Ins);

                                    }
                                    #endregion
                                }
                            }
                        }
                        #endregion
                    }
                    #endregion

                    #region Location(로케이션)
                    //로케이션 사용시에만 사용함 lot랑 같이 사용 할 수 없음"
                    //모든 생산입고, 자재출고가 SP에서 처리됨 안탁까움...
                    //그리하여 관련 전표번호를 최대한 끌어와서 SP에서 동일 수불번호를 찾아서 처리하도록 함.
                    if (Config.MA_ENV.YN_LOCATION == "Y")
                    {
                        DataTable dt_Work_Copy = dtWork.Clone();
                        DataTable dt_Matl_Copy = _dtMatl.Clone();

                        #region 생산품 셋팅

                        object[] obj = new object[] {"", ""};

                        //로케이션 사용 창고 마스터 조회
                        DataTable dt_Sl_Location_YN = _biz.Search_Ma_Sl_Location_YN(new object[] { dtWork.Rows[0]["CD_PLANT"].ToString(), "" });

                        dr_Select = dt_Sl_Location_YN.Select("CD_SL = '" + str입고창고코드 + "'");

                        //공장환경 설정의 생산품자동입고처리 사용여부 조회
                        ds = Pr_ComFunc.Get_Plant_Cfg(new object[] { Global.MainFrame.LoginInfo.CompanyCode, dtWork.Rows[0]["CD_PLANT"].ToString() });

                        if (dr_Select.Length > 0 && (D.GetString(ds.Tables[1].Rows[0]["YN_AUTORCV"]) == "Y"))
                        {
                            dt_Work_Copy = dtWork.Copy();

                            obj = new object[] { str입고창고코드, str입고창고명 };


                            if (dt_Work_Copy.Columns.Contains("CD_SL") == false)
                                dt_Work_Copy.Columns.Add("CD_SL", typeof(string));

                            if (dt_Work_Copy.Columns.Contains("NM_SL") == false)
                                dt_Work_Copy.Columns.Add("NM_SL", typeof(string));

                            foreach (DataRow dr in dt_Work_Copy.Rows)
                            {
                                dr["CD_SL"] = str입고창고코드;
                                dr["NM_SL"] = str입고창고명;
                            }
                        }
                        #endregion

                        #region 자재셋팅
                        //bf이면서 출고창고가 location 사용인 경우에만 탄다.
                        //IN조건을 이용하여 뽑도록함 SP를 사용하여도 어짜피 창고 구해와야함으로
                        //LOCATION Y인 창고 가져와서 비교 하는 것보다 IN 조건으로 빼버림.
                        
                        string sSl_Location = string.Empty;
                        
                        if (dt_Sl_Location_YN.Rows.Count != 0)
                        {
                            foreach (DataRow dr in dt_Sl_Location_YN.Rows)
                            {
                                if (sSl_Location == string.Empty)
                                {
                                    sSl_Location = "'"+ dr["CD_SL"].ToString() +"'";
                                }
                                else
                                {
                                    sSl_Location = sSl_Location + ", '" + dr["CD_SL"].ToString() +"'";
                                }
                            }
                        }

                        #endregion

                        if (_dtMatl.Rows.Count > 0 && sSl_Location != string.Empty)
                        {
                            DataRow[] dr_select = _dtMatl.Select("YN_BF = 'Y' AND CD_SL IN (" + sSl_Location + ")");
                            
                            foreach (DataRow dr_Ins in dr_select)
	                        {
                                dt_Matl_Copy.Rows.Add(dr_Ins.ItemArray);
	                        }
                            
                        }


                        //Location사용자재가 없는 것으로 판단하여 그냥 뛰어넘김
                        if (dt_Work_Copy.Rows.Count > 0 || dt_Matl_Copy.Rows.Count > 0)
                        {
                            P_PR_LOCATION_SUB dlg_Location = new P_PR_LOCATION_SUB(dt_Work_Copy, dt_Matl_Copy, obj);
                            if (dlg_Location.ShowDialog() != DialogResult.OK) return false; ;

                            #region 로케이션 처리

                            dt_Item_Location = dlg_Location.Rtn_dt_Item;
                            dt_Matl_Location = dlg_Location.Rtn_dt_Matl;


                            //생산이보의뢰번호를 넣어줌
                            if (dt_Item_Location != null)
                            {
                                foreach (DataRow dr in dt_Item_Location.Rows)
                                {
                                    dr["NO_IO_203"] = dtWork.Rows[0]["NO_IO_203"];
                                    dr["NO_LINE_202"] = dtWork.Rows[0]["NO_LINE_202"];
                                    dr["NO_WORK"] = dtWork.Rows[0]["NO_WORK"];
                                    dr["NO_WO"] = dtWork.Rows[0]["NO_WO"];
                                    dr["CD_ITEM"] = dtWork.Rows[0]["CD_ITEM"];
                                }

                            }


                            //생산이보의뢰번호를 넣어줌
                            if (dt_Matl_Location != null)
                            {
                                foreach (DataRow dr in dt_Matl_Location.Rows)
                                {
                                    dr["NO_WORK"] = dtWork.Rows[0]["NO_WORK"];
                                }

                            }

                            #endregion
                        }

                    }
                    
                    #endregion
                  
                }
            }

            if (!_biz.Save(dtWork, _dtReject, _dtLotItem, _dtMatl, _dtLotMatl, _dtSERL, dt_Manday, dt_Auto_Bad_Work, dt_Auto_Bad,
                           dt_AutoBad_ReqH, dt_AutoBad_ReqL,
                           dt_Item_Location, dt_Matl_Location,
                           dt_Use_Matl,
                           dtWork.Rows[0]["NO_LINE"].ToString(), strNO_II, str입고창고코드)) return false;

            #region 저장 후  Acceptchange 처리
            _flexD.AcceptChanges();

            if (dtWork != null)
            {
                dtWork.Rows.Clear();
                dtWork.AcceptChanges();
            }

            if (_dtReject != null)
            {
                _dtReject.Rows.Clear();
                _dtReject.AcceptChanges();
            }

            if (_dtLotItem != null)
            {
                _dtLotItem.Rows.Clear();
                _dtLotItem.AcceptChanges();
            }

            if (_dtSERL != null)
            {
                _dtSERL.Rows.Clear();
                _dtSERL.AcceptChanges();
            }

            if (_dtMatl != null)
            {
                _dtMatl.Rows.Clear();
                _dtMatl.AcceptChanges();
            }

            if (_dtLotMatl != null)
            {
                _dtLotMatl.Rows.Clear();
                _dtLotMatl.AcceptChanges();
            }
            #endregion

            return true;
        }

        #endregion

        #region -> 중복체크함수

        private void 중복체크함수(ref stDetailQuery st인자)
        {
            if (htDetailQueryCollection.Contains(st))
            {
                st.NO_LINE2++;
                중복체크함수(ref st인자);
            }
            else
            {
                htDetailQueryCollection.Add(st, st);
                return;
            }
        }

        #endregion

        #region -> 인쇄

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforePrint()) return;
                if (!MsgAndSave(PageActionMode.Print)) return;

                ReportHelper rptHelper = new ReportHelper("R_PR_WORK_REG_0", "작업실적전표");

                DataTable dt = _biz.print(m_cboCdPlant.SelectedValue.ToString(), _flexM["NO_WO"].ToString());
                rptHelper.SetDataTable(dt);
                rptHelper.가로출력();
                rptHelper.Print();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> BeforeDelete Override

        protected override bool BeforeDelete()
        {
            if (!base.BeforeDelete()) return false;

            if (!_flexD.HasNormalRow) return false;

            if (_flexD.Row >= _flexD.Rows.Fixed && _flexD.Row < _flexD.Rows.Count)
            {
                if (_flexD.CDecimal(_flexD["QT_WO_WORK"]) <= 0)
                {
                    ShowMessage(공통메세지._은_보다커야합니다, _flexD.Cols["QT_WO_WORK"].Caption, "0");
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region -> 삭제

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!공장선택여부)
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, m_lblplant.Text);
                    m_cboCdPlant.Focus();
                    return;
                }

                //작업지시의 실적 전체를 삭제하는 기능이므로 저장체크 로직은 제거
                //if (!BeforeDelete()) return;
                //if (!MsgAndSave(PageActionMode.Delete)) return;

               // DialogResult result = MessageBoxEx.Show("작업지시(" + _flexM["NO_WO"].ToString() + ")의 모든 실적자료를 삭제하시겠습니까?", "작업지시의 실적 삭제", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                DialogResult result =ShowMessage("작업지시(@)의 모든 실적자료를 삭제하시겠습니까?",_flexM["NO_WO"].ToString(), "QY2");

                if (result != DialogResult.Yes) return;

                object[] obj = new object[] { LoginInfo.CompanyCode, 
                    _flexM["CD_PLANT"].ToString(), 
                    _flexM["NO_WO"].ToString()
                };

                if (_biz.DELETE_NO_WO(obj))
                {
                    조회후해당행찾기("QT_WORK");
                    ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
                    return;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region 종료
        public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
        {
            try
            {
                Settings1.Default.Work_Detail_Rel_YN = chk_작업지시분할.Checked;
                Settings1.Default.Wo_DcRmk_Apply_YN = chK_작업지시비고적용여부.Checked;
                Settings1.Default.Save();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }

            return true;
        }
        #endregion

        #endregion

        #region ♣ 그리드 이벤트 / 메소드

        #region -> 그리드 컬럼 변경전 체크 이벤트(_flexD_StartEdit)

        void _flexD_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                FlexGrid _flex = sender as FlexGrid;
                if (_flex == null) return;

                switch (_flex.Cols[e.Col].Name)
                {
                    case "NO_LOT":
                        #region NO_LOT
                        //작업지시에서 NOT_NO 를 미리 넣은 경우는 입력할 필요가 없다.
                        if (_flexM["NO_LOT"].ToString().Trim().Length == 0)
                        {
                            //분할 릴리즈는 LOT를 일단 허용하지 않도록 한다.
                            //20110109 최인성
                            if (D.GetString(_flexD["NO_REL"]) != string.Empty)
                            {
                                ShowMessage("작업지시 분할 처리된 건은 LOT 처리를 할 수 없습니다.");
                                e.Cancel = true;
                                return;
                            }

                            //자재의 Lot사용유무가 'Y'이고, 생산의 Lot필수여부가 'Y'이고, 마지막 공정이면서 첫번째 공정이 아니면 입력을 못한다.(마지막 공정이면서 첫번째 공정이면 단공정이다.)
                            if (LoginInfo.MngLot == "Y" && YN_PR_MNG_LOT == "Y" && _flex["YN_FINAL"].ToString() == "Y" && _flex.Rows.Count - _flex.Rows.Fixed != 1)
                            {
                                e.Cancel = true;
                                return;
                            }
                            //마지막 공정이 아니고 초기 고정이 아니면 입력을 못한다.(초기공정과 마지막 공정에만 입력할 수 있다.)
                            if (_flex["YN_FINAL"].ToString() != "Y" && _flex["CD_OP_BASE"].ToString() == "")
                            {
                                e.Cancel = true;
                                return;
                            }
                        }

                        if (_flexD.CDecimal(_flexD["QT_WORK"]) <= 0)
                        {
                            ShowMessage(공통메세지._은는필수입력항목입니다, _flex.Cols["QT_WORK"].Caption);
                            e.Cancel = true;
                            _flexD.Select(e.Row, _flex.Cols["QT_WORK"].Index);
                            return;
                        }
                        #endregion
                        break;
                    case "QT_WORK":
                        #region QT_WORK

                        if (_flexD["YN_CLOSE"].ToString() == "Y")
                        {
                            ShowMessage("해당처리건은 마감되어 입력 할 수 없습니다.");
                            e.Cancel = true;
                            return;
                        }

                        //if (D.GetDecimal(_flex["QT_REMAIN"]) < 0)
                        //{
                        //    ShowMessage("잔업잔량이 0보다 작은 경우에 실적입력은 불가합니다.");
                        //    e.Cancel = true;
                        //    return;
                        //}
                        #endregion
                        break;
                    case "DT_LIMIT":
                        //유효일자는 최종공정일때만 가능하다
                        if (_flex["YN_FINAL"].ToString() == "N")
                        {
                            e.Cancel = true;
                            return;
                        }

                        //열어줬더니만 김성호 차장이 막으라고 함 김도훈 차장이 다시 열어 달라고 함
                        //if (Global.MainFrame.ServerKeyCommon.Contains("SEEGENE"))
                        //{
                        //    e.Cancel = true;
                        //    return;
                        //}

                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 그리드 Validate Check 이벤트(_flexD_ValidateEdit)

        void _flexD_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                FlexGrid _flex = sender as FlexGrid;
                if (_flex == null) return;

                decimal dQT_WORK = _flexD.CDecimal(_flex["QT_WORK"]);
                decimal iQT_REJECT = _flexD.CDecimal(_flex["QT_REJECT"]);
                decimal dQT_REMAIN = _flexD.CDecimal(_flex["QT_REMAIN"]);

                switch (_flex.Cols[e.Col].Name)
                {
                    case "DT_WORK":
                        
                        if(!ComFunc.DateCheck(dat_Work_Dt.Text))
                        {
                            e.Cancel = true;
                            ShowMessage(공통메세지.입력형식이올바르지않습니다);
                            return;
                        }

                        if (Global.MainFrame.ServerKeyCommon.Contains("SEEGENE") || Global.MainFrame.ServerKeyCommon.Contains("ALOE"))
                        {
                            DateTime date = DateTime.Parse(_flexD["DT_WORK"].ToString().Substring(0, 4) + "-" + _flexD["DT_WORK"].ToString().Substring(4, 2) + "-" + _flexD["DT_WORK"].ToString().Substring(6, 2));

                            date = date.AddDays(D.GetInt(_flexD["DY_VALID"]));
                            _flexD["DT_LIMIT"] = date.ToShortDateString().Replace("-", ""); ;
                        }
                       
                        break;
                    case "QT_WORK":
                        // 해당 컬럼을 다시 읽어들인다. 변경가능한 컬럼일 경우 그리드의 값을 읽을경우 변경전의 값이 읽히기 때문
                        dQT_WORK = _flexD.CDecimal(_flex.EditData);
                        if (dQT_WORK > dQT_REMAIN)
                        {
                            object[] obj = new object[]{Global.MainFrame.LoginInfo.CompanyCode, _flexD["CD_PLANT"].ToString()};

                            DataSet ds = Pr_ComFunc.Get_Plant_Cfg(obj);

                            if (ds.Tables[1].Rows[0]["YN_QT_WORK"].ToString() == "N")
                            {
                                ShowMessage("실적입력량이 작업잔량보다 많습니다.");
                                e.Cancel = true;
                                return;
                            }
                        }
                        else if (dQT_WORK < 0)
                        {
                            ShowMessage(공통메세지._은_보다커야합니다, DD("실적입력량"),"0");
                            e.Cancel = true;
                            return;
                        }
                        break;
                    case "QT_REJECT":
                        // 해당 컬럼을 다시 읽어들인다. 변경가능한 컬럼일 경우 그리드의 값을 읽을경우 변경전의 값이 읽히기 때문
                        iQT_REJECT = _flexD.CDecimal(_flex.EditData);
                        if (iQT_REJECT > dQT_WORK)
                        {
                            ShowMessage("불량입력량이 실적수량보다 많습니다.");
                            e.Cancel = true;
                            return;
                        }
                        else if (iQT_REJECT < 0)
                        {
                            ShowMessage(공통메세지._은_보다커야합니다, DD("불량입력량"), "0");
                            e.Cancel = true;
                            return;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 상위 그리드 행변경 이전 이벤트(_flexM_BeforeRowChange)

        void _flexM_BeforeRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (!bGridrowChanging)
                {
                    e.Cancel = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 상위 그리드 행변경 이후 이벤트(_flexM_AfterRowChange)

        private void _flexM_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                ToolBarSearchButtonEnabled = false;
                bGridrowChanging = false;

                DataTable dt = null;

                string Filter = "NO_WO = '" + _flexM["NO_WO"].ToString() + "'";

                if (LoginInfo.MngLot == "N" || _flexM["YN_LOT"].ToString() == "N" || _flexM["NO_LOT"].ToString().Trim().Length > 0)
                    _flexD.Cols["NO_LOT"].Visible = false;
                else
                    _flexD.Cols["NO_LOT"].Visible = true;

                if (_flexM.DetailQueryNeed)
                {
                    object[] obj = new object[]{Global.MainFrame.LoginInfo.CompanyCode, 
                                                m_cboCdPlant.SelectedValue.ToString(),
                                                _flexM["NO_WO"].ToString(),
                                                bp_Tp_Wo.QueryWhereIn_Pipe,
                                                bp_Cd_WC.QueryWhereIn_Pipe,
                                                bp_Wc_Op.QueryWhereIn_Pipe,
                                                Global.MainFrame.LoginInfo.EmployeeNo,
                                                chk_작업지시분할.Checked == true ? "Y" : "N", //작업지시 분할 여부
                                                chk_작업잔량여부.Checked == true ? "Y" : "N",
                                                chk_재작업잔량여부.Checked == true ? "Y" : "N",
                                                chK_작업지시비고적용여부.Checked == true ? "Y" : "N"
                                                };

                    dt = _biz.SearchDetail(obj);

                    //20110120 최인성 한성욱 
                    //작업지시일 셋팅되어있는 날짜로 가져오도록한다.
                    if (Pr_Global.Dat_Work_Control_YN == "100")
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            dr["DT_WORK"] = dat_Work_Dt.Text;


                            //유효기간
                            if (ComFunc.DateCheck(dat_Work_Dt.Text))
                            {
                                DateTime date = DateTime.Parse(dat_Work_Dt.Text.Substring(0, 4) + "-" + dat_Work_Dt.Text.Substring(4, 2) + "-" + dat_Work_Dt.Text.Substring(6, 2));

                                date = date.AddDays(D.GetInt(dr["DY_VALID"]));
                                dr["DT_LIMIT"] = date.ToShortDateString().Replace("-","");
                            }
                            else
                            {
                                dr["DT_LIMIT"] = dat_Work_Dt.Text;
                            }
                        }
                    }

                    dt.AcceptChanges();

                    //dt = _biz.SearchDetail(m_cboCdPlant.SelectedValue.ToString(), _flexM["NO_WO"].ToString());
                }
                _flexD.BindingAdd(dt, Filter);

                //_flexD.Styles.Highlight.ForeColor = Color.Black;

                //컬럼 전체의 스타일을 변경할 필요가 없어서 우선은 주석처리.
                //색깔변경할컬럼지정변경(ref _flexD, "QT_WORK");
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                ToolBarSearchButtonEnabled = true;
                bGridrowChanging = true;
            }
        }

        #endregion

        #region -> 하위 그리드 행변경 이전 이벤트(_flexD_BeforeRowChange)

        void _flexD_BeforeRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (!bDetailGridrowChanging)
                {
                    e.Cancel = true;
                    return;
                }

                bDetailGridrowChanging = false;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                bDetailGridrowChanging = true;
            }
        }

        #endregion

        #region -> 하위 그리드 행변경 이후 이벤트(_flexD_AfterRowChange)

        void _flexD_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                //사용자하일라이트("사용자하이라이트");
                //색깔변경할컬럼지정변경(ref _flexD, "QT_WORK");

                if (IsChanged() && !bSaveCalled)
                {
                    DialogResult result = ShowMessage(공통메세지.변경된사항이있습니다저장하시겠습니까);
                    if (result == DialogResult.No)  // DialogResult.No 일 경우 저장 내역을 취소한다.
                    {
                        _dtReject.Rows.Clear();
                        _flexD.RejectChanges();
                    }
                    else
                    {
                        OnToolBarSaveButtonClicked(null, null);

                        _flexD.Col = _flexD.Cols[strErrCol].Index;

                        strErrCol = "QT_WORK";
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region _flexM_CellContentChanged
        void _flexM_CellContentChanged(object sender, CellContentEventArgs e)
        {
            string str_query = string.Empty;

            object[] obj = new object[] 
            { 
                LoginInfo.CompanyCode,    
                D.GetString(m_cboCdPlant.SelectedValue), 
                D.GetString(_flexM[e.Row, "NO_WO"]),
            };

            if (e.ContentType == ContentType.Memo)
            {
                if (e.CommandType == Dass.FlexGrid.CommandType.Add)
                {
                    str_query = string.Format("UPDATE PR_WO SET MEMO_CD = '{0}' WHERE CD_COMPANY = '{1}' AND CD_PLANT = '{2}' AND NO_WO = '{3}'", e.SettingValue, obj[0], obj[1], obj[2]);
                    Global.MainFrame.ExecuteScalar(str_query);
                }
                else if (e.CommandType == Dass.FlexGrid.CommandType.Delete)
                {
                    str_query = string.Format("UPDATE PR_WO SET MEMO_CD = NULL WHERE CD_COMPANY = '{0}' AND CD_PLANT = '{1}' AND NO_WO = '{2}'", obj[0], obj[1], obj[2]);
                    Global.MainFrame.ExecuteScalar(str_query);
                }
            }
            else if (e.ContentType == ContentType.CheckPen)
            {
                if (e.CommandType == Dass.FlexGrid.CommandType.Add)
                {
                    str_query = string.Format("UPDATE PR_WO SET CHECK_PEN = '{0}' WHERE CD_COMPANY = '{1}' AND CD_PLANT = '{2}' AND NO_WO = '{3}'", e.SettingValue, obj[0], obj[1], obj[2]);
                    Global.MainFrame.ExecuteScalar(str_query);
                }
                else if (e.CommandType == Dass.FlexGrid.CommandType.Delete)
                {
                    str_query = string.Format("UPDATE PR_WO SET CHECK_PEN = NULL WHERE CD_COMPANY = '{0}' AND CD_PLANT = '{1}' AND NO_WO = '{2}'", obj[0], obj[1], obj[2]);
                    Global.MainFrame.ExecuteScalar(str_query);
                }
            }
        }
        #endregion

        #region _flexD_CellContentChanged
        void _flexD_CellContentChanged(object sender, CellContentEventArgs e)
        {
            string str_query = string.Empty;

            object[] obj = new object[] 
            { 
                LoginInfo.CompanyCode,    
                D.GetString(m_cboCdPlant.SelectedValue), 
                D.GetString(_flexD[e.Row, "NO_WO"]),
            };

            if (e.ContentType == ContentType.Memo)
            {
                if (e.CommandType == Dass.FlexGrid.CommandType.Add)
                {
                    str_query = string.Format("UPDATE PR_WO_ROUT SET MEMO_CD = '{0}' WHERE CD_COMPANY = '{1}' AND CD_PLANT = '{2}' AND NO_WO = '{3}'", e.SettingValue, obj[0], obj[1], obj[2]);
                    Global.MainFrame.ExecuteScalar(str_query);
                }
                else if (e.CommandType == Dass.FlexGrid.CommandType.Delete)
                {
                    str_query = string.Format("UPDATE PR_WO_ROUT SET MEMO_CD = NULL WHERE CD_COMPANY = '{0}' AND CD_PLANT = '{1}' AND NO_WO = '{2}'", obj[0], obj[1], obj[2]);
                    Global.MainFrame.ExecuteScalar(str_query);
                }
            }
            else if (e.ContentType == ContentType.CheckPen)
            {
                if (e.CommandType == Dass.FlexGrid.CommandType.Add)
                {
                    str_query = string.Format("UPDATE PR_WO_ROUT SET CHECK_PEN = '{0}' WHERE CD_COMPANY = '{1}' AND CD_PLANT = '{2}' AND NO_WO = '{3}'", e.SettingValue, obj[0], obj[1], obj[2]);
                    Global.MainFrame.ExecuteScalar(str_query);
                }
                else if (e.CommandType == Dass.FlexGrid.CommandType.Delete)
                {
                    str_query = string.Format("UPDATE PR_WO_ROUT SET CHECK_PEN = NULL WHERE CD_COMPANY = '{0}' AND CD_PLANT = '{1}' AND NO_WO = '{2}'", obj[0], obj[1], obj[2]);
                    Global.MainFrame.ExecuteScalar(str_query);
                }
            }
        }
        #endregion

        #region 사용자하일라이트
        private bool 사용자하일라이트(string str사용자하이라이트)
        {
            int iColStart = _flexD.Cols.Fixed;
            int iColEnd = _flexD.Cols.Count - 1;
            int iRowStart = _flexD.Rows.Fixed;
            int iRowEnd = _flexD.Rows.Count - 1;
            int iRow = _flexD.Row;

            if (iRowStart > iRowEnd)
                return false;

            CellRange crHLT = _flexD.GetCellRange(iRow, iColStart, iRow, iColEnd);

            crHLT.Style = GetSumStyle하이라이트(ref _flexD, str사용자하이라이트);

            return true;
        }
        #endregion

        #region -> _flexD_BeforeCodeHelp

        void _flexD_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                switch (e.Parameter.HelpID)
                {
                    case HelpID.P_USER:
                        string str컨트롤구분자 = "|&&|";
                        string str속성구분자 = "|&|";

                        if (e.Parameter.UserHelpID == "H_PR_SFT_SUB")     // SFT 도움창
                        {
                            e.Parameter.P41_CD_FIELD1 = "SFT";

                            e.Parameter.P09_CD_PLANT = _flexM["CD_PLANT"].ToString(); //공장코드
                            e.Parameter.P65_CODE5 = m_cboCdPlant.Text;

                            //[DropDownComboBox][TextBoxExt][RadioButtonExt]등 일반컬트롤 예제
                            e.Parameter.UserParams += "USE_Y" + str속성구분자 + "Y" + str속성구분자 + "Enabled" + str컨트롤구분자;
                            e.Parameter.UserParams += "USE_N" + str속성구분자 + "N" + str속성구분자 + "Enabled" + str컨트롤구분자;
                            e.Parameter.UserParams += "USE_C" + str속성구분자 + "C" + str속성구분자 + "Enabled" + str컨트롤구분자;

                            //[BpCodeTextBox][BpCodeNTextBox] 예제
                            //bpN.UserParams += "GRP_ITEM" + str속성구분자 + "1;AB" + str속성구분자 + "Unabled" + str컨트롤구분자;

                            e.Parameter.P61_CODE1 = str컨트롤구분자;
                            e.Parameter.P62_CODE2 = str속성구분자;
                        }
                        if (e.Parameter.UserHelpID == "H_PR_EQUIP_SUB") // 설비 도움창
                        {
                            e.Parameter.P41_CD_FIELD1 = "EQUIP";

                            e.Parameter.P09_CD_PLANT = _flexM["CD_PLANT"].ToString();   //공장코드
                            e.Parameter.P63_CODE3 = _flexD["CD_WC"].ToString();         //작업장
                            e.Parameter.P64_CODE4 = _flexD["CD_WCOP"].ToString();       //작업공정
                            e.Parameter.P65_CODE5 = m_cboCdPlant.Text;

                            //[DropDownComboBox][TextBoxExt][RadioButtonExt]등 일반컬트롤 예제
                            //e.Parameter.UserParams += "USE_Y" + str속성구분자 + "Y" + str속성구분자 + "Enabled" + str컨트롤구분자;
                            //e.Parameter.UserParams += "USE_N" + str속성구분자 + "N" + str속성구분자 + "Enabled" + str컨트롤구분자;
                            //e.Parameter.UserParams += "USE_C" + str속성구분자 + "C" + str속성구분자 + "Enabled" + str컨트롤구분자;

                            //[BpCodeTextBox][BpCodeNTextBox] 예제
                            //bpN.UserParams += "GRP_ITEM" + str속성구분자 + "1;AB" + str속성구분자 + "Unabled" + str컨트롤구분자;

                            e.Parameter.P61_CODE1 = str컨트롤구분자;
                            e.Parameter.P62_CODE2 = str속성구분자;
                        }
                        if (e.Parameter.UserHelpID == "H_PR_WCOP_SUB_SUB")  // 
                        {
                            e.Parameter.P41_CD_FIELD1 = "SUB공정";

                            e.Parameter.P09_CD_PLANT = _flexM["CD_PLANT"].ToString();   //공장코드
                            e.Parameter.P20_CD_WC = _flexD["CD_WC"].ToString();         //작업장
                            e.Parameter.P63_CODE3 = _flexD["NM_WC"].ToString();         //작업장명
                            e.Parameter.P42_CD_FIELD2 = _flexD["CD_WCOP"].ToString();   //작업공정코드
                            e.Parameter.P43_CD_FIELD3 = _flexD["NM_OP"].ToString();     //작업공정명
                            e.Parameter.P65_CODE5 = m_cboCdPlant.Text;

                            //[DropDownComboBox][TextBoxExt][RadioButtonExt]등 일반컬트롤 예제
                            //e.Parameter.UserParams += "USE_Y" + str속성구분자 + "Y" + str속성구분자 + "Enabled" + str컨트롤구분자;
                            //e.Parameter.UserParams += "USE_N" + str속성구분자 + "N" + str속성구분자 + "Enabled" + str컨트롤구분자;
                            //e.Parameter.UserParams += "USE_C" + str속성구분자 + "C" + str속성구분자 + "Enabled" + str컨트롤구분자;

                            //[BpCodeTextBox][BpCodeNTextBox] 예제
                            //bpN.UserParams += "GRP_ITEM" + str속성구분자 + "1;AB" + str속성구분자 + "Unabled" + str컨트롤구분자;

                            e.Parameter.P61_CODE1 = str컨트롤구분자;
                            e.Parameter.P62_CODE2 = str속성구분자;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> _flexD_OwnerDrawCell

        void _flexD_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            int iRow = _flexD.Selection.r1;
            int iCol = _flexD.Cols["QT_WORK"].Index;

            if (iRow == e.Row && iCol == e.Col)
            {
                //if (_flexD.Styles.Contains("QT_WORK"))
                //    e.Style = _flexD.Styles["QT_WORK"];
                //LinearGradientBrush lgBrush = new LinearGradientBrush(e.Bounds, e.Style.BackColor, e.Style.BackColor, 1);
                //e.Graphics.FillRectangle(lgBrush, e.Bounds);
                //e.DrawCell(DrawCellFlags.Content);
                //e.Handled = true;

                //1. 연노량
                //Color clr = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(139)))));

                //2. 연분홍(M3)
                //Color clr = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(215)))), ((int)(((byte)(255)))));
                //e.Style.ForeColor = Color.Black;

                //3. 진한색
                Color clr = Color.SeaGreen;

                //Color.YellowGreen

                LinearGradientBrush lgBrush = new LinearGradientBrush(e.Bounds, clr, clr, 1);
                //LinearGradientBrush lgBrushString = new LinearGradientBrush(e.Bounds, Color.Black, Color.Black, 1);

                //string strContent = _flexD[iRow, iCol].ToString();

                //Font fnt = _flexD.Font;

                e.Graphics.FillRectangle(lgBrush, e.Bounds);
                //e.Graphics.DrawString(strContent, _flexD.Font, lgBrushString, e.Bounds.X, e.Bounds.Y);

                e.DrawCell(DrawCellFlags.Content);
                e.Handled = true;
            }
        }

        #endregion

        #region -> 하위 그리드 더블클릭 이벤트(_flexD_DoubleClick) --> 주석처리

        //void _flexD_DoubleClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        FlexGrid _flex = sender as FlexGrid;
        //        if (_flex == null) return;

        //        if (_flex.Row < _flex.Rows.Fixed) return;

        //        switch (_flex.Cols[_flex.Col].Name)
        //        {
        //            case "NO_EMP":  //SetCpdeHelpCol 을 사용.
        //                return;
        //        }

        //        P_PR_SFT_SUB dlg = new P_PR_SFT_SUB(_flexM["CD_PLANT"].ToString(), false, "Y");

        //        if (dlg.ShowDialog() == DialogResult.OK)
        //        {
        //            DataRow dr = dlg.ReturnDataRow;

        //            DataRow[] drsSFT = dtSFT.Select("CD_OP = '" + drs[0]["CD_OP"].ToString() + "'", "", DataViewRowState.CurrentRows);

        //            if (drsSFT.Length == 0)
        //            {
        //                DataRow drInsert = dtSFT.NewRow();
        //                drInsert["CD_OP"] = _flex["CD_OP"].ToString();
        //                drInsert["NO_SFT"] = dr["NO_SFT"];
        //                drInsert["NM_SFT"] = dr["NM_SFT"];
        //                drInsert["CD_DEPT"] = dr["CD_DEPT"];
        //                drInsert["NM_DEPT"] = dr["NM_DEPT"];
        //                drInsert["YN_USE"] = dr["YN_USE"];
        //                drInsert["DC_RMK"] = dr["DC_RMK"];
        //                dtSFT.Rows.Add(drInsert);
        //            }
        //            else if (drsSFT.Length == 1)
        //            {
        //                dtSFT.Rows[0]["CD_OP"] = _flex["CD_OP"].ToString();
        //                dtSFT.Rows[0]["NO_SFT"] = dr["NO_SFT"];
        //                dtSFT.Rows[0]["NM_SFT"] = dr["NM_SFT"];
        //                dtSFT.Rows[0]["CD_DEPT"] = dr["CD_DEPT"];
        //                dtSFT.Rows[0]["NM_DEPT"] = dr["NM_DEPT"];
        //                dtSFT.Rows[0]["YN_USE"] = dr["YN_USE"];
        //                dtSFT.Rows[0]["DC_RMK"] = dr["DC_RMK"];
        //            }
        //            else
        //            {
        //                ShowMessage("해당공정(" + _flex["CD_OP"].ToString() + ")에 대해서 SFT(Shop Flow Control) 값이 2건이상 있습니다. 화면을 닫고 다시 작업을 진행해야 합니다.");
        //                return;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MsgEnd(ex);
        //    }
        //}

        #endregion

        #endregion

        #region ♣ 버튼 이벤트 / 메소드

        #region -> 불량내역등록 버튼 이벤트(m_btnRejectDtl_Click)

        private void m_btnRejectDtl_Click(object sender, System.EventArgs e)
        {
            try
            {
                ShowRejectHelp();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 실제 불량내역등록 구문(ShowRejectHelp)

        private bool ShowRejectHelp()
        {
            if (BasicInfo.ActiveDialog == true) return false;

            if (!_flexM.HasNormalRow || !_flexD.HasNormalRow)
            {
                ShowMessage(공통메세지.선택된자료가없습니다);
                return false;
            }



            //작업지시분할로 인하여 수정
            //DataRow[] cur_row = _flexD.DataTable.Select("NO_WO = '" + _flexM["NO_WO"].ToString() + "'", "", DataViewRowState.CurrentRows);
            DataRow[] cur_row = _flexD.DataTable.Select("NO_WO = '" + _flexM["NO_WO"].ToString() + "' AND ISNULL(NO_REL, '') = '"+ _flexD["NO_REL"].ToString() +"'", "", DataViewRowState.CurrentRows);

            Decimal dQT_REJECT = 0;

            if (drs != null && drs.Length == 1 && (drs[0].RowState == DataRowState.Unchanged || drs[0].RowState == DataRowState.Modified))
                dQT_REJECT = D.GetDecimal(drs[0]["QT_REJECT"]);

            if (dQT_REJECT == 0)
            {
                ShowMessage(공통메세지._은_보다커야합니다, DD("불량수량"), "0");
                return false;
            }

            if (_dtReject != null && _dtReject.Rows.Count > 0)
            {
                ShowMessage("불량내역이 등록되어 있습니다.");
                return false;
            }

            //DataRow[] drsWORKL = dtSFT.Select("CD_OP = '" + drs[0]["CD_OP"].ToString() + "'", "", DataViewRowState.CurrentRows);

            //string strNO_SFT = "";

            //foreach (DataRow dr in drsWORKL)
            //{
            //    strNO_SFT = dr["NO_SFT"].ToString();
            //}

            string strNO_SFT = drs[0]["NO_SFT"].ToString();

            P_PR_WORK_SUB02 dlg = new P_PR_WORK_SUB02(cur_row, MainFrameInterface, m_cboCdPlant.Text, dQT_REJECT,
                _flexM["NM_ITEM"].ToString(), _flexM["STND_ITEM"].ToString(), _flexM["UNIT_IM"].ToString(), strNO_SFT);

            if (dlg.ShowDialog() != DialogResult.OK) return false;

            if (dlg is IHelpWindow)
            {
                object[] wo_sub = ((IHelpWindow)dlg).ReturnValues;
                _dtReject = (DataTable)wo_sub[0];
                return true;
            }
            else
                return false;
        }

        #endregion

        #region -> 공정재작업처리 버튼 클릭 이벤트(m_btnproc_rework_Click)

        private void m_btnproc_rework_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (BasicInfo.ActiveDialog == true) return;

                if (!_flexM.HasNormalRow || !_flexD.HasNormalRow)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                if (_flexD.Row >= _flexD.Rows.Fixed && _flexD.Row < _flexD.Rows.Count)
                {
                    if (_flexD.CDecimal(_flexD["QT_REWORKREMAIN"]) <= 0)
                    {
                        ShowMessage(공통메세지._은_보다커야합니다, _flexD.Cols["QT_REWORKREMAIN"].Caption, "0");
                        return;
                    }
                }

                string[] loa_param = new string[21];

                int lrow = _flexD.Row;

                loa_param[0] = _flexD["CD_PLANT"].ToString();	//공장
                loa_param[1] = m_cboCdPlant.Text.Substring(0, m_cboCdPlant.Text.IndexOf("(")); //공장명
                loa_param[2] = _flexD["NO_WO"].ToString();		//작지번호
                loa_param[3] = _flexD["CD_WC"].ToString();		//작업장
                loa_param[4] = _flexD["NM_WC"].ToString();		//작업장명
                loa_param[5] = _flexD["CD_OP"].ToString();		//공정
                loa_param[6] = _flexD["NM_OP"].ToString();		//공정명
                loa_param[7] = _flexD["CD_ITEM"].ToString();		//품목
                loa_param[8] = _flexM["NM_ITEM"].ToString();
                loa_param[9] = _flexM["STND_ITEM"].ToString();	//규격
                loa_param[10] = _flexM["UNIT_IM"].ToString();	//단위
                loa_param[11] = _flexD["QT_WO_REJECT"].ToString();//불량수량
                loa_param[12] = _flexD["QT_REWORK"].ToString();//재작업수량
                loa_param[13] = _flexD["QT_BAD"].ToString();//불량처리수량
                loa_param[14] = _flexD["QT_REWORKREMAIN"].ToString();//재작업잔량
                loa_param[15] = _flexM["YN_LOT"].ToString();    //작업지시품목의 Lot관리 사용하는지 여부
                loa_param[16] = YN_PR_MNG_LOT;                  //공장환경설정의 Lot필수여부
                loa_param[17] = "N";    //PAGE YN_SUBCON
                loa_param[18] = _flexD["YN_SUBCON"].ToString();
                loa_param[19] = "";     //공정외주발주번호
                loa_param[20] = "0";     //공정외주발주항번


                //재작업처리에 이상한 값이 들어감...
                //그래서 SELECT로 값을 찾아 처리하도록함.

                DataRow drH = _flexM.DataView[_flexM.Row - _flexM.Rows.Fixed].Row;
                DataRow drRework = _flexD.DataView[_flexD.Row - _flexD.Rows.Fixed].Row;

                //drH : 작업실적등록의 상위 그리드
                //drRework : 작업실적등록 하위그리드의 현재행의 Data
                //loa_param : 화면의 컨트롤들과 설정들 세팅할 Data
                //_flexD.DataView : 작업실적등록 하위그리드의 DataView(현재 화면에 보이는 모든 하위그리드 데이타
                //lrow : 작업실적등록 하위그리드의 현재 행 위치
                P_PR_WORK_SUB dlg = new P_PR_WORK_SUB(drH, drRework, loa_param, _flexD.DataView, lrow, _flexD.TopRow);

                if (dlg.ShowDialog() != DialogResult.OK) return;

                _flexD.AcceptChanges();

                조회후해당행찾기("QT_WORK");
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 투입상세 버튼 클릭 이벤트(m_btndtl_isu_Click)

        private void m_btndtl_isu_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (Duzon.Common.Forms.BasicInfo.ActiveDialog == true)  return;

                if (!_flexM.HasNormalRow || !_flexD.HasNormalRow)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else if (_flexD.Row >= _flexD.Rows.Fixed && _flexD.Row < _flexD.Rows.Count)
                {
                    if (_flexD.CDecimal(_flexD["QT_WO_WORK"]) <= 0)
                    {
                        ShowMessage(공통메세지._은_보다커야합니다, _flexD.Cols["QT_WO_WORK"].Caption, "0");
                        return;
                    }
                }
                
                string[] args = new string[8]{  m_cboCdPlant.SelectedValue.ToString(),
                                                _flexM["DT_REL"].ToString(),		//시작일
                                                _flexM["DT_DUE"].ToString(),		//종료일
                                                _flexD["CD_WC"].ToString(),		    //작업장코드
                                                _flexM["CD_ITEM"].ToString(),		//품목코드
                                                _flexM["NO_WO"].ToString(), 		//작업지시번호
                                                _flexD["NM_WC"].ToString(),		    //작업장명
                                                _flexM["NM_ITEM"].ToString(),		//품목명
											 };

                object[] obj_args = new object[2] { args, MainFrameInterface };

                if (IsExistPage("P_PR_II_SCH01", true) == true)
                    UnLoadPage("P_PR_II_SCH01", false);     //페이지를 먼저 닫는다.
           
                CallOtherPageMethod("P_PR_II_SCH01", "자재투입List", Grant, obj_args);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 실적이력 버튼 클릭 이벤트(m_btnhst_work_Click)

        private void m_btnhst_work_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (BasicInfo.ActiveDialog == true) return;

                if (!_flexM.HasNormalRow || !_flexD.HasNormalRow)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else if (_flexD.Row >= _flexD.Rows.Fixed && _flexD.Row < _flexD.Rows.Count)
                {
                    if (_flexD.CDecimal(_flexD["QT_WO_WORK"]) <= 0)
                    {
                        ShowMessage(공통메세지._은_보다커야합니다, _flexD.Cols["QT_WO_WORK"].Caption, "0");
                        return;
                    }
                }

                DataRow[] cur_row = _flexD.DataTable.Select("NO_WO = '" + _flexM["NO_WO"].ToString() + "' AND CD_OP = '" + _flexD["CD_OP"].ToString() + "'", "", DataViewRowState.CurrentRows);
                
                if (cur_row == null || cur_row.Length < 1)
                {
                    ShowMessage(공통메세지._이가존재하지않습니다, DD("실적이력"));
                    return;
                }

                int iQT_START = -1;
                int iQT_WORK = -1;

                if (_flexD.Row < _flexD.DataView.Count)//맨 마지막 행을 제외한 행일 때
                {
                    iQT_START = Convert.ToInt32(_flexD[_flexD.Row + 1, "QT_START"]);
                    iQT_WORK = Convert.ToInt32(_flexD[_flexD.Row + 1, "QT_WO_WORK"]);
                }

                object dlg = LoadHelpWindow("P_PR_WORK_HST_SUB01", new object[] { cur_row, MainFrameInterface, iQT_START, iQT_WORK, 
                    m_cboCdPlant.Text.Substring(0, m_cboCdPlant.Text.IndexOf("(")), _flexM["NM_ITEM"].ToString(), _flexM["STND_ITEM"].ToString(), _flexM["UNIT_IM"].ToString(), _flexM["NO_PJT"].ToString(), _flexM["NM_PJT"].ToString()});

                ((Duzon.Common.Forms.CommonDialog)dlg).ShowDialog();

                조회후해당행찾기("QT_WORK");
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion		

        #region -> 공정실적삭제 버튼 클릭 이벤트(m_btnDeleteWork_Click)

        private void m_btnDeleteWork_Click(object sender, EventArgs e)
        {
            try
            {
                if (!공장선택여부)
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, m_lblplant.Text);
                    m_cboCdPlant.Focus();
                    return;
                }

                //20101229 최인성 헌섭대리 요청으로 실적수량을 넣었을대 저장보다는 삭제로직을 타도록함.
                //if (!BeforeSearch()) return;
                //if (!BeforeDelete()) return;
                //if (!MsgAndSave(PageActionMode.Delete)) return;

                //20101229 최인성 헌섭대리 요청으로 실적수량을 넣었을대 저장보다는 삭제로직을 타도록함.
                if (D.GetDecimal(_flexD["QT_WO_WORK"]) == 0)
                {
                    ShowMessage("실적수량이 없습니다.");
                    return;
                }

               // DialogResult result = MessageBoxEx.Show("작업지시(" + _flexD["NO_WO"].ToString() + "), 공정(" + _flexD["CD_OP"].ToString() + ")의 모든 실적자료를 삭제하시겠습니까?", "공정의 실적 삭제", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                DialogResult result = ShowMessage("작업지시(@), 공정(@)의 모든 실적자료를 삭제하시겠습니까?",new object[]{_flexD["NO_WO"].ToString(),_flexD["CD_OP"].ToString() }, "QY2");

                if (result != DialogResult.Yes) return;

                object[] obj = new object[] { LoginInfo.CompanyCode, 
                    _flexD["CD_PLANT"].ToString(), 
                    _flexD["NO_WO"].ToString(), 
                    _flexD["CD_OP"].ToString(),
                    _flexD["NO_REL"].ToString()
                };

                if (_biz.DELETE_CD_OP(obj))
                {
                    ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
                    조회후해당행찾기("QT_WORK");
                    return;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 공정불량처리 버튼 클릭 이벤트(btn공정불량처리_Click)

        private void btn공정불량처리_Click(object sender, EventArgs e)
        {
            try
            {
                if (BasicInfo.ActiveDialog == true) return;

                if (!_flexM.HasNormalRow || !_flexD.HasNormalRow)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                if (_flexD.Row >= _flexD.Rows.Fixed && _flexD.Row < _flexD.Rows.Count)
                {
                    if (_flexD.CDecimal(_flexD["QT_REWORKREMAIN"]) <= 0)
                    {
                        ShowMessage(공통메세지._은_보다커야합니다, _flexD.Cols["QT_REWORKREMAIN"].Caption, "0");
                        return;
                    }
                }

                DataRow drH = _flexM.DataView[_flexM.Row - _flexM.Rows.Fixed].Row;
                DataRow drRework = _flexD.DataView[_flexD.Row - _flexM.Rows.Fixed].Row;

                string[] strParams = new string[] { _flexD["CD_PLANT"].ToString(), 
                    m_cboCdPlant.Text.Substring(0, m_cboCdPlant.Text.IndexOf("(")), 
                    _flexD["NO_WO"].ToString(), 
                    _flexD["CD_WC"].ToString(), 
                    _flexD["NM_WC"].ToString(), 
                    _flexD["CD_OP"].ToString(), 
                    _flexD["NM_OP"].ToString(), 
                    _flexD["CD_ITEM"].ToString(), 
                    _flexM["NM_ITEM"].ToString(), 
                    _flexM["STND_ITEM"].ToString(), 
                    _flexM["UNIT_IM"].ToString(), 
                    _flexD["QT_WO_REJECT"].ToString(), 
                    _flexD["QT_REWORK"].ToString(), 
                    _flexD["QT_BAD"].ToString(), 
                    _flexD["QT_REWORKREMAIN"].ToString(), 
                    _flexM["YN_LOT"].ToString(), 
                    YN_PR_MNG_LOT, 
                    "Y",        //PAGE YN_SUBCON
                    "Y",        //YN_SUBCON
                    "", 
                    "0"
                };

                P_PR_BADWORK_SUB dlg = new P_PR_BADWORK_SUB(drH, drRework, strParams);

                if (dlg.ShowDialog() != DialogResult.OK) return;

                _flexD.AcceptChanges();

                조회후해당행찾기("QT_WORK");
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region ♣ 도움창 이벤트 / 메소드

        #region -> 도움창 호출전 이벤트(OnBpControl_QueryBefore)

        private void OnBpControl_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                BpCodeTextBox bp = sender as BpCodeTextBox;
                if (bp == null) return;

                switch (bp.HelpID)
                {
                    case HelpID.P_MA_PITEM_SUB:     //공장품목
                    case HelpID.P_PR_WO_REG_SUB:    //작업지시번호
                        if (!공장선택여부)
                        {
                            ShowMessage(공통메세지._은는필수입력항목입니다, m_lblplant.Text);
                            e.QueryCancel = true;
                            m_cboCdPlant.Focus();
                            return;
                        }
                        e.QueryCancel = true;

                        P_PR_WO_NEW_SUB dlg = new P_PR_WO_NEW_SUB(m_cboCdPlant.SelectedValue.ToString(), null, e.HelpParam.P92_DETAIL_SEARCH_CODE);

                        if (dlg.drReturn == null)
                        {
                            if (dlg.ShowDialog() != DialogResult.OK) return;
                        }


                        bp.CodeValue = D.GetString(dlg.ReturnRow[0]["NO_WO"]);
                        bp.CodeName = D.GetString(dlg.ReturnRow[0]["NO_WO"]);

                        //e.HelpParam.P09_CD_PLANT = m_cboCdPlant.SelectedValue.ToString();
                        break;
                    case HelpID.P_USER:
                        if (bp.UserHelpID == "H_SA_PRJ_SUB")   // 프로젝트 도움창
                        {
                            e.HelpParam.P41_CD_FIELD1 = "프로젝트";

                            e.HelpParam.P42_CD_FIELD2 = "";

                            e.HelpParam.P07_NO_EMP = "";
                            e.HelpParam.P17_CD_SALEGRP = "";
                            e.HelpParam.P14_CD_PARTNER = "";
                            e.HelpParam.P61_CODE1 = "";
                        }
                        break;
                    case HelpID.P_MA_WC_SUB1:
                        //e.HelpParam.P41_CD_FIELD1 = "작업장 조회 도움창";
                        e.HelpParam.P09_CD_PLANT = m_cboCdPlant.SelectedValue.ToString();
                        e.HelpParam.P07_NO_EMP = Global.MainFrame.LoginInfo.EmployeeNo;

                        break;

                    case HelpID.P_PR_TPWO_SUB1:
                        //사용자 도움창 명
                        //e.HelpParam.P41_CD_FIELD1 = "오더형태 조회 도움창";
                        e.HelpParam.P07_NO_EMP = Global.MainFrame.LoginInfo.EmployeeNo;

                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region 도움창 호출전 이벤트(Control_QueryBefore)
        private void Control_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                string sCtrName = ((Control)sender).Name;

                switch (sCtrName)
                {
                    case "bp_Cd_WC":
                        //e.HelpParam.P41_CD_FIELD1 = "작업장 조회 도움창";
                        e.HelpParam.P09_CD_PLANT = m_cboCdPlant.SelectedValue.ToString();
                        e.HelpParam.P07_NO_EMP = Global.MainFrame.LoginInfo.EmployeeNo;

                        break;

                    case "bp_Tp_Wo":
                        //사용자 도움창 명
                        //e.HelpParam.P41_CD_FIELD1 = "오더형태 조회 도움창";
                        e.HelpParam.P07_NO_EMP = Global.MainFrame.LoginInfo.EmployeeNo;

                        break;

                    case "bp_Wc_Op":
                        if (D.GetString(bp_Cd_WC.SelectedValue) == string.Empty)
                        {
                            Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, lbl_Cd_Wc.Text);
                            e.QueryCancel = true;
                            return;
                        }

                        e.HelpParam.P09_CD_PLANT = m_cboCdPlant.SelectedValue.ToString();
                        e.HelpParam.P20_CD_WC = bp_Cd_WC.QueryWhereIn_Pipe;

                        break;

                    case "bpc_Tp_Item":
                        e.HelpParam.P41_CD_FIELD1 = "MA_B000011";//품목타입
                        break;

                    case "bp_SFT":
                        #region H_PR_SFT_SUB

                        if (!공장선택여부)
                        {
                            ShowMessage(공통메세지._은는필수입력항목입니다, m_lblplant.Text);
                            e.QueryCancel = true;
                            m_cboCdPlant.Focus();
                            return;
                        }

                        e.HelpParam.UserHelpID = "H_PR_SFT_SUB";
                        e.HelpParam.P41_CD_FIELD1 = "Y";//사용여부, 사용"Y", 미사용 "N", 전체 ""
                        e.HelpParam.P09_CD_PLANT = D.GetString(m_cboCdPlant.SelectedValue.ToString());//공장코드
                        e.HelpParam.P65_CODE5 = D.GetString(m_cboCdPlant.Text);
                        #endregion
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region -> 도움창 호출후 이벤트(OnBpControl_QueryAfter)

        private void OnBpControl_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                switch (e.HelpID)
                {
                    case HelpID.P_MA_PITEM_SUB:     //공장품목
                        DataRow[] _dr = e.HelpReturn.Rows;
                        m_txtStndItem.Text = _dr[0]["STND_ITEM"].ToString();
                        m_txtUnit.Text = _dr[0]["UNIT_IMNM"].ToString();
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 도움창 변경 이벤트(OnBpControl_CodeChanged)

        private void OnBpControl_CodeChanged(object sender, System.EventArgs e)
        {
            try
            {
                m_txtStndItem.Text = m_txtUnit.Text = "";
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 도움창 호출전 이벤트(OnBpControl_QueryBefore2)

        private void OnBpControl_QueryBefore2(object sender, BpQueryArgs e)
        {
            try
            {
                BpCodeNTextBox bpN = sender as BpCodeNTextBox;
                if (bpN == null) return;

                switch (bpN.HelpID)
                {
                    case HelpID.P_MA_PITEM_SUB:     //공장품목
                        if (!공장선택여부)
                        {
                            ShowMessage(공통메세지._은는필수입력항목입니다, m_lblplant.Text);
                            e.QueryCancel = true;
                            m_cboCdPlant.Focus();
                            return;
                        }
                        e.HelpParam.P09_CD_PLANT = m_cboCdPlant.SelectedValue.ToString();
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        
        #endregion

        #region btn_일일작업공수_Click
        private void btn_일일작업공수_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flexM.HasNormalRow) return;

                //작업짓상태가 마감이 아닌건
                if (_flexM["ST_WO"].ToString() == "C")
                {
                    ShowMessage("일일작업공수등록 이동은 미마감인 경우에만 가능합니다.");
                    return;
                }


                object[] obj = new object[] 
                { 
                    Global.MainFrame.CurrentPageID,
                    D.GetString(m_cboCdPlant.SelectedValue), 
                    m_dtpFrom.Text,
                    m_dtpTo.Text,
                    _flexM["NO_WO"].ToString()
                };

                if (MainFrameInterface.IsExistPage("P_PR_WORK_MANDAY_REG", false))
                    UnLoadPage("P_PR_WORK_MANDAY_REG", false);//- 특정 페이지 닫기

                CallOtherPageMethod("P_PR_WORK_MANDAY_REG", "일일작업공수등록", Grant, obj);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #endregion

        #region ♣ Control 이벤트 / 메소드

        #region -> 날짜 Validate Check 이벤트(OnControlValidating)

        private void OnControlValidating(object sender, CancelEventArgs e)
        {
            try
            {
                DatePicker dp = sender as DatePicker;
                if (dp == null) return;

                if (dp.Text == "") return;

                if (!dp.IsValidated)
                {
                    ShowMessage(공통메세지.입력형식이올바르지않습니다);
                    e.Cancel = true;
                    return;
                }

                //20110113 최인성 김성호 추가 작업지시 일자 변경시 라인 Row에 작업일을 변경한다.
                //20110120 최인성 한성욱 추가 작업지시 일자 변경시 모든 라인변경 -> 기존에 해당라인만 변경됨.
                //작업지시일 컨트롤이 활성화 되었을 때만 돈다.
                if (dp.Name == dat_Work_Dt.Name)
                {
                    //_flexD["DT_WORK"] = dat_Work_Dt.Text;
                    for (int i = 0; i < _flexD.Rows.Count - _flexD.Rows.Fixed; i++)
                    {
                        _flexD.DataView[i]["DT_WORK"] = dat_Work_Dt.Text;

                        //유효기간
                        if (ComFunc.DateCheck(dat_Work_Dt.Text))
                        {
                            DateTime date = DateTime.Parse(dat_Work_Dt.Text.Substring(0, 4) + "-" + dat_Work_Dt.Text.Substring(4, 2) + "-" + dat_Work_Dt.Text.Substring(6, 2));

                            date = date.AddDays(D.GetInt(_flexD.DataView[i]["DY_VALID"]));
                            _flexD.DataView[i]["DT_LIMIT"] = date.ToShortDateString().Replace("-", ""); ;
                        }
                        else
                        {
                            _flexD.DataView[i]["DT_LIMIT"] = dat_Work_Dt.Text;
                        }


                        if (D.GetDecimal(_flexD.DataView[i]["QT_WORK"]) == 0)
                        {
                            _flexD.DataView[i].Row.AcceptChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 공장 콤보 변경 이벤트(m_cboCdPlant_SelectedIndexChanged)

        private void m_cboCdPlant_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_cboCdPlant.Items.Count > 0 && m_cboCdPlant.SelectedValue != null && m_cboCdPlant.SelectedValue.ToString() != "")
                {
                    object[] obj = new object[] { LoginInfo.CompanyCode, m_cboCdPlant.SelectedValue.ToString() };

                    YN_PR_MNG_LOT = _biz.SELECT_YN_PR_MNG_LOT(obj);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                _flexM.Enabled = false;
                _flexD.Enabled = false;
            }
        }

        #endregion

        #endregion

        #region ♣ 기타메서드

        #region -> 메세지

        private DialogResult ShowMessage(메세지 msg, params object[] paras)
        {
            switch (msg)
            {
                case 메세지.불량수량이작업수량을초과합니다:
                    return ShowMessage("PR_M200003");
            }

            return DialogResult.None;
        }

        #endregion

        #region -> 색깔변경할컬럼지정변경

        private bool 색깔변경할컬럼지정변경(ref FlexGrid _flex, string strChangeColumn)
        {
            if (!_flex.Cols.Contains(strChangeColumn))
                return false;

            int iCol = _flexD.Cols[strChangeColumn].Index;
            int iRowStart = _flexD.Rows.Fixed;
            int iRowEnd = _flexD.Rows.Count - 1;

            if (iRowStart > iRowEnd)
                return false;

            CellRange crQT_WORK = _flexD.GetCellRange(iRowStart, iCol, iRowEnd, iCol);

            crQT_WORK.Style = GetSumStyle(ref _flexD, strChangeColumn);

            return true;
        }

        #endregion

        #region -> GetSumStyle

        private CellStyle GetSumStyle(ref FlexGrid _flex, string strStyleName)
        {
            CellStyle cs = _flex.Styles[strStyleName];

            if (cs == null)
            {
                cs = _flex.Styles.Add(strStyleName);

                if (_flex.GridStyle != GridStyleEnum.None)
                {
                    //cs.BackColor = System.Drawing.Color.FromArgb(247, 222, 180);
                    //cs.ForeColor = System.Drawing.Color.FromArgb(177, 73, 78);
                    cs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(215)))), ((int)(((byte)(255)))));
                    cs.ForeColor = Color.Black;
                }
            }

            return cs;
        }

        #endregion

        #region -> GetSumStyle하이라이트

        private CellStyle GetSumStyle하이라이트(ref FlexGrid _flex, string strStyleName)
        {
            CellStyle cs = _flex.Styles[strStyleName];

            if (cs == null)
            {
                cs = _flex.Styles.Add(strStyleName);

                if (_flex.GridStyle != GridStyleEnum.None)
                {
                    
                    cs.BackColor = _flexD.Styles.Highlight.BackColor;
                    cs.ForeColor = _flexD.Styles.Highlight.ForeColor;

                    _flexD.Styles.Highlight.Display = DisplayEnum.None;
                }
            }

            return cs;
        }

        #endregion

        #endregion

        #region ♣ 속성들

        string 진행상태
        {
            get
            {
                string StWo = "";
                if (m_chkNonClose.Checked == true && m_chkClose.Checked == true)
                    StWo = "SRC";
                else if (m_chkNonClose.Checked == true && m_chkClose.Checked == false)
                    StWo = "SR";
                else if (m_chkNonClose.Checked == false && m_chkClose.Checked == true)
                    StWo = "SC";
                else if (m_chkNonClose.Checked == false && m_chkClose.Checked == false)
                    StWo = "S";
                return StWo;
            }
        }

        string 마감구분
        {
            get
            {
                string FgClose = "";

                if (m_chkNonClose.Checked == true && m_chkClose.Checked == true)
                    FgClose = "S|R|C|";
                else if (m_chkNonClose.Checked == true && m_chkClose.Checked == false)
                    FgClose = "S|R|";
                else if (m_chkNonClose.Checked == false && m_chkClose.Checked == true)
                    FgClose = "C|";
                else if (m_chkNonClose.Checked == false && m_chkClose.Checked == false)
                    FgClose = "|";

                return FgClose;
            }
        }

        bool 공장선택여부
        {
            get
            {
                if (m_cboCdPlant.SelectedValue == null || m_cboCdPlant.SelectedValue.ToString() == "")
                    return false;
                return true;
            }
        }

        #endregion

        #region -> 작업지시마감

        private void btn_작업지시마감_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flexM.HasNormalRow)
                {
                    ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                    return;
                }


                if (_flexM["ST_WO"].ToString().ToUpper() == "C")
                {
                    ShowMessage("마감처리된 작업지시건 입니다.");
                    return;
                }



                if (_flexM["DT_CLOSE"].ToString() == "")
                {
                    ShowMessage("마감일은 필수 입력값입니다.");
                    return;
                }


                DataTable dt_Temp = _flexM.DataTable.Clone();

                DataRow[] dr_Select= _flexM.DataTable.Select("NO_WO = '"+ _flexM["NO_WO"].ToString() +"'");

                foreach (DataRow dr_Ins in dr_Select)
                {
                    DataRow dr = dt_Temp.NewRow();
                    dr["CD_PLANT"] = dr_Ins["CD_PLANT"];
                    dr["NO_WO"] = dr_Ins["NO_WO"];
                    dr["DT_CLOSE"] = dr_Ins["DT_CLOSE"];

                    dt_Temp.Rows.Add(dr.ItemArray);
                }

                if (_biz.Wo_Close(dt_Temp))
                {
                    OnToolBarSearchButtonClicked(sender, e);
                    ShowMessage("작업지시가 마감되었습니다.");

                    if (!_flexM.HasNormalRow) return;

                    조회후해당행찾기("QT_WORK");
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 작업지시마감취소

        private void btn_작업지시마감취소_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flexM.HasNormalRow)
                {
                    ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                    return;
                }

                if (_flexM["ST_WO"].ToString().ToUpper() != "C")
                {
                    ShowMessage("마감처리되지 않은 작업지시건입니다.");
                    return;
                }


                DataTable dt_Temp = _flexM.DataTable.Clone();

                DataRow[] dr_Select = _flexM.DataTable.Select("NO_WO = '" + _flexM["NO_WO"].ToString() + "'");

                foreach (DataRow dr_Ins in dr_Select)
                {
                    DataRow dr = dt_Temp.NewRow();
                    dr["CD_PLANT"] = dr_Ins["CD_PLANT"];
                    dr["NO_WO"] = dr_Ins["NO_WO"];

                    if(dr["DT_CLOSE"].ToString() == string.Empty)
                        dr["DT_CLOSE"] = Global.MainFrame.GetStringToday;

                    dt_Temp.Rows.Add(dr.ItemArray);
                }


                if (_biz.Wo_CloseCancel(dt_Temp))
                {
                    OnToolBarSearchButtonClicked(sender, e);
                    ShowMessage("작업지시가 마감이 취소되었습니다.");

                    if (!_flexM.HasNormalRow) return;

                    조회후해당행찾기("QT_WORK");
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

    }
}

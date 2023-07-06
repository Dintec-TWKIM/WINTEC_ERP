using System;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.PR;
using Duzon.ERPU.MF.Common;
using Duzon.Windows.Print;
using DzHelpFormLib;
using Duzon.ERPU.MF;
using Duzon.ERPU.SA.Settng;
using Duzon.Common.ConstLib;

namespace prd
{
    // **************************************
    // 재 작  성 일 : 2006-09-20
    // 모   듈   명 : 생산
    // 시 스  템 명 : 생산관리
    // 서브시스템명 : 작업지시관리
    // 페 이 지  명 : 작업지시등록
    // 프로젝트  명 : P_PR_WO_REG_NEW
    // 오더상태는 무조건 확정
    // **************************************
    public partial class P_PR_WO_REG_NEW : Duzon.Common.Forms.PageBase
    {
        #region ♣ 생성자/초기화    ♣

        private bool b_StateMode = false;    //이벤트가 실행중인지를 확인 한다.

        P_PR_WO_REG_NEW_BIZ _biz = new P_PR_WO_REG_NEW_BIZ();
        FreeBinding _header = null;

        string s_대체품적용여부 = "N";
        string PLANT = "";
        string NO_WO = "";
        decimal dNO_WO_LINE = 0m;
        string strSOURCE = string.Empty;

        bool b복사여부 = false;

        //20110511 최인성 제거
        //왜 전역변수 사용했는지 전혀 알수 없음.
        //private DataSet   _dsCfg   = null; 
        //private DataTable _dtPlant = null;
        
        /// <summary>
        /// 오더형태별에 따른 자동프로세스 사용여부
        /// </summary>
        string sTp_Wo_Auto = "000";

        /// <summary>
        /// 오더형태별에 따른 생산유형설정 ( 001:일반, 002:재작업, 003:검사)
        /// </summary>
        string sTp_Wo_Prod = "001";

        string str_Fg_Option = "000";

        /// <summary>
        /// 시리얼사용 여부 FG_SERNO(001: 없음, 002 = LOT, 003 = S/N
        /// 20110418 최인성 김형석 쏠리테크 rmk2과 rmk3에 시리얼번호 채번 구성을 위한 변수
        /// </summary>
        string sFg_SerNo = "";

        /// <summary>
        /// 경로유형 컨트롤의 변경전 경로유형값
        /// </summary>
        string sPatnRout_Old_Value = string.Empty;

        /// <summary>
        /// 물류/제조환경설정 구매재고 - LOT유효일자관리여부 (N : 사용안함, Y : 사용)
        /// </summary>
        string sLot_DtLimit = BASIC.GetMAEXC("LOT유효일자관리여부");
        string sLOT번호자동생성 = BASIC.GetMAEXC("작업지시번호-생산품 LOT번호 자동적용");

        #region -> 이벤트 실행 상태 초기화 및 프로그레스바 죽이기
        private void initState(bool state)
        {
            /*
             * b_SaveMode 가 true 상태인경우에는 실행중이므로 다른 이벤트가 호출되지 않게 한다.
             * b_SaveMode 가 false 상태인 경우에는 실행 가능한 상태로 만들어준다. 
             * 프로그레스바 종료로직을 여기에 넣은 이유는 리턴 될때 위 2가지 모드를 모두 해주어야 한다.
             */
            b_StateMode = state;
            MsgControl.CloseMsg();  //프로그레스바 종료 
        }
        #endregion

        
        public P_PR_WO_REG_NEW() : this("", "") { }

        public P_PR_WO_REG_NEW(string strCD_Plant, string strNO_WO) : this(strCD_Plant, strNO_WO, 0m, "") { }

        public P_PR_WO_REG_NEW(string strCD_Plant, string strNO_WO, decimal dNO_WO_LINE, string strSOURCE)
        {
            try
            {
                InitializeComponent();

                //
                // 업체 요청에 따른 디자인 변경
                // 전용으로 컨트롤 사용시 Tag값 설정도 해주어야함
                //
                Z_ChangedComponent();

                //
                // 사용자정의 콤보박스 세팅(안전공업 제외)
                //
                SetUserDefine_CD_USERDEF();

                //
                // 사용자정의 숫자컨트롤 세팅
                //
                SetUserDefine_NUM_USERDEF();

                MainGrids = new FlexGrid[] { _flex01, _flex02, _flex03, _flex04 };
                DataChanged += new EventHandler(Page_DataChanged);

                PLANT = strCD_Plant;
                NO_WO = strNO_WO;
                this.dNO_WO_LINE = dNO_WO_LINE;
                this.strSOURCE = strSOURCE;

                _header = new FreeBinding();
                _header.JobModeChanged += new FreeBindingEventHandler(_header_JobModeChanged);
                _header.ControlValueChanged += new FreeBindingEventHandler(_header_ControlValueChanged);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public P_PR_WO_REG_NEW(PageBaseConst.CallType pageCallType, string ID_MEMO) : this(string.Empty, string.Empty, decimal.Zero, string.Empty)
        {
            try
            {
                DataTable dt_WO = _biz.Search_PMS_NO_WO(ID_MEMO);

                if (dt_WO.Rows.Count == 1)
                {
                    PLANT = D.GetString(dt_WO.Rows[0]["CD_PLANT"]);
                    NO_WO = D.GetString(dt_WO.Rows[0]["NO_WO"]);
                    this.dNO_WO_LINE = decimal.Zero;
                    this.strSOURCE = "PMS";
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #region -> InitLoad

        protected override void InitLoad()
        {
            base.InitLoad();

            str_Fg_Option = Duzon.ERPU.MF.ComFunc.전용코드("수주등록-사양등록 사용여부");

            InitGrid01();
            InitGrid02();
            InitGrid03();
            InitGrid04();
        }

        #endregion

        #region -> InitGrid01

        private void InitGrid01()
        {
            _flex01.BeginSetting(1, 1, false);
            _flex01.SetCol("S", "S", 30, true, CheckTypeEnum.Y_N);
            _flex01.SetCol("CD_OP", "OP", 30, 4, true);
            _flex01.SetCol("CD_WC", "작업장", 90, 14,true);
            _flex01.SetCol("NM_WC", "작업장명", 120, false);
            _flex01.SetCol("CD_WCOP", "공정코드", 90, 20, true);
            _flex01.SetCol("NM_WCOP", "공정명", 120, false);
            _flex01.SetCol("QT_WO", "공정별지시수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            if (Global.MainFrame.ServerKeyCommon.ToUpper().Contains("GSITM"))       // 파워카본테크놀로지(주)
            {
                _flex01.SetCol("RT_YIELD", "공정별수율", 90, true, typeof(decimal), FormatTpType.QUANTITY);
            }
            _flex01.SetCol("YN_QC", "QC검사", 70, true);
            _flex01.SetCol("YN_FINAL", "최종실적", 70, true);
            _flex01.SetCol("DC_RMK", "비고", 120, true);
            _flex01.SetCol("DC_RMK_1", "비고1", 120, true);
            _flex01.SetCol("NO_SFT", "SFT", 40, true);
            _flex01.SetCol("NM_SFT", "SFT명", 100, false);
            _flex01.SetCol("CD_EQUIP", "설비코드", 60, true);
            _flex01.SetCol("NM_EQUIP", "설비명",  100, false);
            _flex01.SetCol("CD_POST", "위치코드", 80, true);
            _flex01.SetCol("NM_POST", "위치명", 80, false);

            if (MA.ServerKey(false, new string[] { "HDWIA", "HDWIA2" }))
            {
                _flex01.SetCol("NUM_USERDEF1", "사용자정의숫자컬럼1", 100, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                _flex01.Cols["NUM_USERDEF1"].Format = "#,###.##";
            }
            else
                _flex01.SetCol("NUM_USERDEF1", "사용자정의숫자컬럼1", 100, true, typeof(decimal), FormatTpType.QUANTITY);

            _flex01.SetCol("NUM_USERDEF2", "사용자정의숫자컬럼2", 100, true, typeof(decimal), FormatTpType.QUANTITY);
            _flex01.SetCol("NUM_USERDEF3", "사용자정의숫자컬럼3", 100, true, typeof(decimal), FormatTpType.QUANTITY);

            _flex01.SetCol("TXT_USERDEF1", "사용자정의텍스트컬럼1", 100, true, typeof(string));
            _flex01.SetCol("TXT_USERDEF2", "사용자정의텍스트컬럼2", 100, true, typeof(string));
            _flex01.SetCol("TXT_USERDEF3", "사용자정의텍스트컬럼3", 100, true, typeof(string));

            UserDefinedSetting_PR_WO_ROUT();

            _flex01.SetExceptEditCol("NM_WC", "NM_WCOP");
            _flex01.SetCodeHelpCol("CD_OP", HelpID.P_PR_ROUT_SUB, ShowHelpEnum.Always, new string[] { "CD_OP", "CD_WC", "NM_WC", "CD_WCOP", "NM_WCOP", "FG_WC" }, new string[] { "CD_OP", "CD_WC", "NM_WC", "CD_WCOP", "NM_OP", "TP_WC" }, new string[] { "CD_OP", "CD_WC", "NM_WC", "CD_WCOP", "NM_WCOP", "FG_WC", "CD_WC", "NM_WC", "CD_WCOP", "NM_WCOP" }, ResultMode.SlowMode);
            _flex01.SetCodeHelpCol("CD_WC", HelpID.P_MA_WC_SUB, ShowHelpEnum.Always, new string[] { "CD_WC", "NM_WC", "FG_WC" }, new string[] { "CD_WC", "NM_WC", "TP_WC" }, new string[] { "CD_WC", "NM_WC", "FG_WC", "CD_WCOP", "NM_WCOP" }, ResultMode.FastMode);
            _flex01.SetCodeHelpCol("CD_WCOP", HelpID.P_PR_WCOP_SUB, ShowHelpEnum.Always, new string[] { "CD_WCOP", "NM_WCOP" }, new string[] { "CD_WCOP", "NM_OP" }, new string[] { "CD_WCOP", "NM_WCOP" }, ResultMode.SlowMode);
            _flex01.SetCodeHelpCol("NO_SFT", "H_PR_SFT_SUB", ShowHelpEnum.Always, new string[] { "NO_SFT", "NM_SFT" }, new string[] { "NO_SFT", "NM_SFT" });
            _flex01.SetCodeHelpCol("CD_EQUIP", "H_PR_EQUIP_SUB", ShowHelpEnum.Always, new string[] { "CD_EQUIP", "NM_EQUIP", "CD_POST", "NM_POST" }, new string[] { "CD_EQUIP", "NM_EQUIP", "CD_POST", "NM_POST" }, new string[] { "CD_EQUIP", "NM_EQUIP", "CD_POST", "NM_POST" }, ResultMode.SlowMode);
            _flex01.SetCodeHelpCol("CD_POST", "H_PR_POST_SUB", ShowHelpEnum.Always, new string[] { "CD_POST", "NM_POST" }, new string[] { "CD_POST", "NM_POST" }, new string[] { "CD_POST", "NM_POST" }, ResultMode.SlowMode);

            _flex01.NewRowEditable = true;
            _flex01.EnterKeyAddRow = true;

            //그리드 마지막 컬럼 체움
            _flex01.ExtendLastCol = true;

            _flex01.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            _flex01.SetDummyColumn("S");
            _flex01.VerifyPrimaryKey = new string[] { "CD_OP" };
            _flex01.VerifyAutoDelete = new string[] { "CD_OP" };
            _flex01.VerifyNotNull = new string[] { "CD_WC", "CD_WCOP", "YN_FINAL" };
            _flex01.VerifyCompare(_flex01.Cols["QT_WO"], 0, OperatorEnum.Greater);

            _flex01.AfterAddRow += new RowColEventHandler(경로정보추가_Click);
            _flex01.BeforeCodeHelp += new BeforeCodeHelpEventHandler(Grid_BeforeCodeHelp);
            _flex01.AfterCodeHelp += new AfterCodeHelpEventHandler(Grid_AfterCodeHelp);
            _flex01.ValidateEdit += new ValidateEditEventHandler(Grid_ValidateEdit);
        }

        #endregion

        #region -> InitGrid02

        private void InitGrid02()
        {
            _flex02.BeginSetting(1, 1, true);
            _flex02.SetCol("S", "S", 30, true, CheckTypeEnum.Y_N);
            _flex02.SetCol("CD_OP", "OP", 30, 4);
            _flex02.SetCol("CD_WC", "작업장", 90, 14);
            _flex02.SetCol("NM_WC", "작업장명", 120, false);
            _flex02.SetCol("CD_WCOP", "공정코드", 90, 20);
            _flex02.SetCol("NM_WCOP", "공정명", 120, false);
            _flex02.SetCol("CD_MATL", "품목코드", 100);
            _flex02.SetCol("NM_ITEM", "품목명", 120, false);
            _flex02.SetCol("STND_ITEM", "규격", 80, false);
            _flex02.SetCol("STND_DETAIL_ITEM", "세부규격", 100, false);
            _flex02.SetCol("UNIT_MO", "단위", 40, false);
            _flex02.SetCol("NO_MODEL", "모델코드", 100, false);
            _flex02.SetCol("QT_NEED", "소요수량", 90, true, typeof(decimal), FormatTpType.QUANTITY);
            _flex02.SetCol("QT_NEED_NET", "단위소요량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex02.Cols["QT_NEED_NET"].Format = "#,###,###.######";
            _flex02.SetCol("DT_NEED", "소요일자", 70, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex02.SetCol("YN_BF", "B/F여부", 70);
            _flex02.SetCol("FG_GIR", "불출여부", 70);
            _flex02.SetCol("NO_LINE", "항번", 40, 17, false, typeof(decimal), FormatTpType.QUANTITY);

            //20100701 최인성 추가
            //일단 출고창고는 안보이도록 하고 입고창고는 그냥 창고명으로 함.
            //_flex02.SetCol("CD_SL_IN", "창고코드", 80, 7, true);
            //_flex02.SetCol("NM_SL_IN", "창고명", 120, false);
            //_flex02.SetCol("CD_SL_IN", "입고창고", 80, 7, true);
            //_flex02.SetCol("NM_SL_IN", "입고창고명", 120, false);
            _flex02.SetCol("CD_SL_OT", "출고창고", 80, 7, true);
            _flex02.SetCol("NM_SL_OT", "출고창고명", 120, false);
            _flex02.SetCol("DC_RMK", "비고", 150, 100, true);
            _flex02.SetCol("FG_SERNO", "LOT/SN", 100, false);
            _flex02.SetCol("MAT_ITEM", "재질", 100, false);


            _flex02.SetExceptEditCol("NM_WC", "NM_WCOP", "NM_ITEM", "STND_ITEM", "UNIT_MO", "NO_MODEL", "NM_SL_OT", "FG_SERNO", "STND_DETAIL_ITEM");
            _flex02.SetCodeHelpCol("CD_OP", HelpID.P_PR_ROUT_SUB, ShowHelpEnum.Always, new string[] { "CD_OP", "CD_WC", "NM_WC", "CD_WCOP", "NM_WCOP" }, new string[] { "CD_OP", "CD_WC", "NM_WC", "CD_WCOP", "NM_OP" }, new string[] { "CD_OP", "CD_WC", "NM_WC", "CD_WCOP", "NM_WCOP" }, ResultMode.SlowMode);
            _flex02.SetCodeHelpCol("CD_WC", HelpID.P_MA_WC_SUB, ShowHelpEnum.Always, new string[] { "CD_WC", "NM_WC" }, new string[] { "CD_WC", "NM_WC" }, new string[] { "CD_WC", "NM_WC", "CD_WCOP", "NM_WCOP" }, ResultMode.FastMode);
            _flex02.SetCodeHelpCol("CD_WCOP", HelpID.P_PR_WCOP_SUB, ShowHelpEnum.Always, new string[] { "CD_WCOP", "NM_WCOP" }, new string[] { "CD_WCOP", "NM_OP" }, new string[] { "CD_WCOP", "NM_WCOP" }, ResultMode.SlowMode);
            _flex02.SetCodeHelpCol("CD_MATL", "H_MA_PITEM_SUB", ShowHelpEnum.Always, new string[] { "CD_MATL", "NM_ITEM", "STND_ITEM", "UNIT_MO", "YN_BF", "FG_GIR", "NO_MODEL", "FG_SERNO", "STND_DETAIL_ITEM", "MAT_ITEM" }, new string[] { "CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT_MO", "FG_BF", "FG_GIR", "NO_MODEL", "FG_SERNO", "STND_DETAIL_ITEM_MATL", "MAT_ITEM" });
            //_flex02.SetCodeHelpCol("CD_MATL", HelpID.P_MA_PITEM_SUB, ShowHelpEnum.Always, new string[] { "CD_MATL", "NM_ITEM", "STND_ITEM", "UNIT_MO","YN_BF","FG_GIR"}, new string[] { "CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT_MO","FG_BF","FG_GIR"}, ResultMode.SlowMode);

            //20100701 최인성 CD_SL_IN
            _flex02.SetCodeHelpCol("CD_SL_IN", HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CD_SL_IN", "NM_SL_IN" }, new string[] { "CD_SL", "NM_SL" });
            _flex02.SetCodeHelpCol("CD_SL_OT", HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CD_SL_OT", "NM_SL_OT" }, new string[] { "CD_SL", "NM_SL" });

            _flex02.SetCol("QT_INV", "현재고", 60, false, typeof(decimal), FormatTpType.QUANTITY);

            _flex02.NewRowEditable = true;
            _flex02.EnterKeyAddRow = true;
            _flex02.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            _flex02.SetDummyColumn("S");
            //_flex02.VerifyPrimaryKey = new string[] { "CD_OP", "CD_MATL" };
            //_flex02.VerifyAutoDelete = new string[] { "CD_OP" };
            _flex02.VerifyNotNull = new string[] { "CD_OP", "CD_WC", "CD_WCOP", "CD_MATL" };

            _flex02.VerifyCompare(_flex02.Cols["QT_NEED"], 0, OperatorEnum.Greater);

            _flex02.Cols["NO_LINE"].Visible = false;
            _flex02.Cols["QT_NEED_NET"].Visible = false;
            

            //
            // 신라파이어의 경우 소요수량 수정을 막아달라는 요청
            // QT_NEED, QT_NEED_NET 둘 다 막음
            //
            if (MA.ServerKey(false, "SLFIRE"))
            {
                _flex02.Cols["QT_NEED"].AllowEditing = false;
                _flex02.Cols["QT_NEED_NET"].AllowEditing = false;
            }

            //그리드 우클릭 메뉴 추가
            _flex02.AddMyMenu = true;
            _flex02.AddMenuSeperator();
            ToolStripMenuItem parent2 = _flex02.AddPopup(DD("엑셀양식"));
            _flex02.AddMenuItem(parent2, DD("작업지시등록-소요자재"), Menu_Click);//menu_Clil은 이벤트 명

            _flex02.AfterAddRow += new RowColEventHandler(소요자재추가_Click);
            _flex02.ValidateEdit += new ValidateEditEventHandler(Grid_ValidateEdit);
            _flex02.BeforeCodeHelp += new BeforeCodeHelpEventHandler(Grid_BeforeCodeHelp);
            _flex02.AfterCodeHelp += new AfterCodeHelpEventHandler(Grid_AfterCodeHelp);
        }
        #endregion

        #region -> InitGrid03

        private void InitGrid03()
        {
            _flex03.BeginSetting(1, 1, false);
            _flex03.SetCol("CHK", "S", 30, true, CheckTypeEnum.Y_N);
            _flex03.SetCol("NO_SO", "수주번호", 120, 20, false);
            _flex03.SetCol("SEQ_SO", "항번", 40, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex03.SetCol("QT_IM", "수주수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex03.SetCol("QT_GI_IM", "출하수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex03.SetCol("QT_GI_REMAIN", "미출하수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex03.SetCol("QT_WO", "작업지시수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex03.SetCol("QT_WORK", "실적수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex03.SetCol("QT_REMAIN", "잔량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex03.SetCol("QT_APPLY", "적용수량", 90, 17, true, typeof(decimal), FormatTpType.QUANTITY);
            _flex03.SetCol("DT_DUEDATE", "납기일", 80, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex03.SetCol("CD_PARTNER", "거래처코드", 100, 20, false);
            _flex03.SetCol("LN_PARTNER", "거래처명", 140, 20, false);
            _flex03.SetCol("DC_RMKH", "수주헤더비고", 200, false);
            _flex03.SetCol("DC_RMKL", "수주라인비고", 200, false);
            _flex03.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            _flex03.SetExceptEditCol("NO_SO", "SEQ_SO", "QT_IM", "QT_GI_IM", "QT_GI_REMAIN", "QT_WO", "QT_WORK", "QT_REMAIN", "DT_DUEDATE", "CD_PARTNER", "LN_PARTNER");
            _flex03.SetExceptSumCol("SEQ_SO");

            _flex03.SetDummyColumn("CHK");
            _flex03.VerifyPrimaryKey = new string[] { "NO_SO", "SEQ_SO" };

            _flex03.VerifyCompare(_flex03.Cols["QT_APPLY"], 0, OperatorEnum.Greater);

            _flex03.ValidateEdit += new ValidateEditEventHandler(_flex03_ValidateEdit);
        }

        #endregion

        #region -> InitGrid04

        private void InitGrid04()
        {
            _flex04.BeginSetting(1, 1, false);
            _flex04.SetCol("CHK", "S", 30, true, CheckTypeEnum.Y_N);

            if (Pr_Global.Prq_Reg_YN == "000")
            {
                _flex04.SetCol("NO_PRQ", "요청번호", 120, 20, false);
                _flex04.SetCol("NO_PRQ_LINE", "항번", 40, 17, false, typeof(decimal), FormatTpType.QUANTITY);
                _flex04.SetCol("NO_PRQD", "요청분할번호", 0, 20, false);
                _flex04.SetCol("NO_PRQD_LINE", "항번", 0, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            }
            else if (Pr_Global.Prq_Reg_YN == "100")
            {
                _flex04.SetCol("NO_PRQ", "요청번호", 0, 20, false);
                _flex04.SetCol("NO_PRQ_LINE", "항번", 0, 17, false, typeof(decimal), FormatTpType.QUANTITY);
                _flex04.SetCol("NO_PRQD", "요청분할번호", 120, 20, false);
                _flex04.SetCol("NO_PRQD_LINE", "항번", 40, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            }

            _flex04.SetCol("CD_PJT", "프로젝트",   100, false);
            _flex04.SetCol("NM_PROJECT", "프로젝트명", 150, false);
            _flex04.SetCol("CD_PJT_SEQ", "항번", 0, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex04.SetCol("NO_SO", "수주번호", 0, 20, false);
            _flex04.SetCol("NO_SO_LINE", "수주항번", 0, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex04.SetCol("QT_ACT", "접수수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex04.SetCol("QT_APPLY", "적용수량", 90, 17, true, typeof(decimal), FormatTpType.QUANTITY);
            _flex04.SetCol("QT_ITEM", "지시된수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex04.SetCol("QT_WORK", "실적수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex04.SetCol("QT_REMAIN", "잔량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex04.SetCol("DT_DLV", "납기일", 80, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            if (MA.ServerKey(true, new string[] { "HANSU" }))
            {
                _flex04.SetCol("CD_PACKUNIT", "포장단위", 100);
                _flex04.SetCol("NM_PACKUNIT", "포장명", 100);
            }

            _flex04.SetCol("DT_REQ", "작업요청일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex04.SetCol("DT_PRQ", "요청일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            _flex04.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            _flex04.SetExceptEditCol("NO_PRQ", "NO_PRQ_LINE", "NO_PRQD", "NO_PRQD_LINE", "CD_PJT",
                                     "CD_PJT_SEQ", "NO_SO", "NO_SO_LINE", "QT_ACT", "QT_ITEM",
                                     "QT_WORK", "QT_REMAIN", "DT_DLV");
            _flex04.SetExceptSumCol("NO_PRQ_LINE", "NO_PRQD_LINE", "CD_PJT_SEQ", "NO_SO_LINE");

            _flex04.Cols["QT_ACT"].Visible = false;
            //_flex04.Cols["QT_ITEM"].Visible = false;
            _flex04.Cols["NO_PRQ_LINE"].TextAlign = TextAlignEnum.CenterCenter;
            _flex04.Cols["NO_PRQD_LINE"].TextAlign = TextAlignEnum.CenterCenter;
            _flex04.Cols["CD_PJT_SEQ"].TextAlign = TextAlignEnum.CenterCenter;
            _flex04.Cols["NO_SO_LINE"].TextAlign = TextAlignEnum.CenterCenter;

            _flex04.SetDummyColumn("CHK");
            _flex04.VerifyPrimaryKey = new string[] { "NO_PRQ", "NO_PRQ_LINE", "NO_PRQD", "NO_PRQD_LINE" };

            _flex04.VerifyCompare(_flex04.Cols["QT_APPLY"], 0, OperatorEnum.Greater);

            _flex04.ValidateEdit += new ValidateEditEventHandler(_flex04_ValidateEdit);
        }

        #endregion

        #region -> InitControl

        private void InitControl()
        {
            DataSet ds = GetComboData("NC;MA_PLANT", "N;PR_0000007", "N;PR_TPWO", "N;PR_0000006", "N;MA_B000057", "N;MA_B000015");

            // 공장
            _cboCdPlant.DataSource = ds.Tables[0];
            _cboCdPlant.ValueMember = "CODE";
            _cboCdPlant.DisplayMember = "NAME";

            if (ds.Tables[0].Select("CODE = '" + LoginInfo.CdPlant + "'").Length == 1)
                _cboCdPlant.SelectedValue = LoginInfo.CdPlant;
            else if (_cboCdPlant.Items.Count > 0)
                _cboCdPlant.SelectedIndex = 0;

            // 작업지시구분
            _cboFgWo.DataSource = ds.Tables[1];
            _cboFgWo.ValueMember = "CODE";
            _cboFgWo.DisplayMember = "NAME";

            // 오더형태
            //20110113 최인성 김성호 물류사용자 세부권한 사용시 권한에 맞는 오더형태가 나오도록 변경
            if (Pr_Global.bMfg_AuthH_YN == true)
            {
                //구분자 "WC":W/C, TPWO:오더형태,
                DataTable dt_Tp_Wo = Pr_ComFunc.Get_MA_MFG_AUTH(new object[]{Global.MainFrame.LoginInfo.CompanyCode, 
                                                                       Global.MainFrame.LoginInfo.DeptCode,
                                                                       Global.MainFrame.LoginInfo.EmployeeNo,
                                                                       "TPWO"
                                                                       });
                _cboTpRout.DataSource = dt_Tp_Wo;
                _cboTpRout.ValueMember = "CODE";
                _cboTpRout.DisplayMember = "NAME";
            }
            else
            {
                _cboTpRout.DataSource = ds.Tables[2];
                _cboTpRout.ValueMember = "CODE";
                _cboTpRout.DisplayMember = "NAME";
            }

            // 오더상태
            _cboStWo.DataSource = ds.Tables[3];
            _cboStWo.ValueMember = "CODE";
            _cboStWo.DisplayMember = "NAME";


            object[] obj = new object[3];
            obj[0] = Global.MainFrame.LoginInfo.CompanyCode;
            obj[1] = D.GetString(_cboCdPlant.SelectedValue);
            obj[2] = D.GetString(_bptxtCdItem.CodeValue);
            DataTable dt = _biz.getOPPathSetting(obj);

            // 경로유형(품목에 대하여 DB에서 가져옴)
            _cboPatnRout.DataSource = dt;
            _cboPatnRout.ValueMember = "CODE";
            _cboPatnRout.DisplayMember = "NAME";

            _flex01.SetDataMap("YN_QC", ds.Tables[4], "CODE", "NAME");
            _flex01.SetDataMap("YN_FINAL", ds.Tables[4].Copy(), "CODE", "NAME");

            _flex02.SetDataMap("YN_BF", ds.Tables[4].Copy(), "CODE", "NAME");
            _flex02.SetDataMap("FG_GIR", ds.Tables[4].Copy(), "CODE", "NAME");
            _flex02.SetDataMap("FG_SERNO", ds.Tables[5].Copy(), "CODE", "NAME");

            
            //20110511 최인성
            //다시 화면 로딩시점의 공장환경을 불러올려고 했던것 같음.
            //공장이 바뀐다면????? 어디선가 다시 로딩해야하는데;;;;; 아무곳에도 추가 안됨.
            //_dtPlant = _biz.SearchCfg(_cboCdPlant.SelectedValue.ToString()).Tables[1];           

            #region -> 작업지시수량의 소수점을 결정한다.

            string strFormat = _flex01.Cols["QT_WO"].Format;

            int iLength = 0;

            if (strFormat.Contains("."))
            {
                int iStartIndex = strFormat.IndexOf('.');

                strFormat = strFormat.Substring(iStartIndex + 1, strFormat.Length - iStartIndex - 1);

                iLength = strFormat.Length;
            }

            _curQtItem.CurrencyDecimalDigits = iLength;

            #endregion

            //작업지시등록은 저장이나 조회 후 사용 가능 함.
            btn_PR_WorkWo.Visible = false;
            btn_PR_WorkWo.Enabled = false;

            if (Pr_Global.Pr_Wo_Batch == "000")             // 일반
            {
                #region 일반

                bpP_txtNoFr.Visible = false;
                bpP_curQtBatch.Visible = false;

                //
                // 2014.05.28 oneGridItem16.Visible을 false로 준다고해서
                // 나머지 원그리드아이템이 위로 붙지 않는다.
                // 그리고 oneGridItem16에 다른 컨트롤을 추가하였다.
                //
                //oneGridItem16.Visible = false;

                if (Global.MainFrame.ServerKeyCommon.ToUpper().Contains("GSITM"))        // 파워카본테크놀로지(주)
                {
                    bpP_txtNoFr.Visible = true;
                    bpP_curQtBatch.Visible = true;

                    _lblBatch.Text = "목표수율(%)";
                }

                #endregion
            }
            else if (Pr_Global.Pr_Wo_Batch == "100")        // 배합
            {
                #region 배합

                bpP_txtNoFr.Visible = true;

                if (Global.MainFrame.ServerKeyCommon.ToUpper().Contains("GSITM"))        // 파워카본테크놀로지(주)
                {
                    bpP_txtNoFr.Visible = false;
                    bpP_curQtBatch.Visible = false;
                }
                else
                {
                    bpP_txtNoFr.Visible = true;
                    bpP_curQtBatch.Visible = true;

                    _lblBatch.Text = "BATCH 수량";
                }

                #endregion
            }
        }

        #endregion

        #region -> InitPaint

        protected override void InitPaint()
        {
            base.InitPaint();

            oneGrid1.UseCustomLayout = true;
            oneGrid1.IsSearchControl = false;
            bpP_Item.IsNecessaryCondition = true;
            bpP_Rout.IsNecessaryCondition = true;
            bpP_Tp_Wo.IsNecessaryCondition = true;
            bpP_Wo_Qty.IsNecessaryCondition = true;
            oneGrid1.InitCustomLayout();
            
            InitControl();
            
            //
            //2014.07.11 D20140711034 - 화면 오픈시 작지번호에 ""가들어갈경우 데이터조회가 되므로 화면오픈 시간이 지연됨
            //이를 해결하기 위해 작지번호에 "XXXXX"를 넣어줌
            //
            DataSet ds = _biz.Search(_cboCdPlant.SelectedValue == null ? string.Empty : _cboCdPlant.SelectedValue.ToString(), "XXXXXXXXXX");
            _header.SetBinding(ds.Tables[0], oneGrid1);
            _header.ClearAndNewRow();

            _flex01.Binding = ds.Tables[1];
            _flex02.Binding = ds.Tables[2];
            _flex03.Binding = ds.Tables[3];
            _flex04.Binding = ds.Tables[4];

            //시스템환경설정의 LOT관리 여부에 따라 사용여부 변경
            if (LoginInfo.MngLot == "Y")
                bpP_NoLot.Visible = true;
            else
                bpP_NoLot.Visible = false;
            
            //시스템환경설정의 LOT관리 여부와 물류/제조환경설정(구매재고)의 LOT유효일자관리여부에 따라 유효일자 VISIBLE변경
            if (LoginInfo.MngLot == "Y" && sLot_DtLimit == "Y")
                bpP_DtLimit.Visible = true;
            else
                bpP_DtLimit.Visible = false;

            if (NO_WO != "")
            {
                DataSet dsWo = _biz.Search(PLANT, NO_WO);

                _header.SetDataTable(dsWo.Tables[0]);
                _flex01.Binding = dsWo.Tables[1];
                _flex02.Binding = dsWo.Tables[2];
                _flex03.Binding = dsWo.Tables[3];
                _flex04.Binding = dsWo.Tables[4];
                경로유형콤보셋팅();

                //
                // 2015.06.12 김현수
                // 여러개의 경로유형을 가지는 품목의 경우
                // 경로유형콤보셋팅()을 태우게 되면
                // 경로유형 콤보박스가 저장된 값과 다른
                // 1번 경로유형을 보여주게되는 오류가 발생한다.
                // 따라서 헤더에 DataTable을 한 번 더 넣어주도록 하였다.
                //
                _header.SetDataTable(dsWo.Tables[0]);
            }
            else
            {
                //20100107 최인성 - 김성호 과장
                //상단에서 SetBinding을 해줘서 이부분에 초기 셋팅값을 설정함.
                //오더형태 존재여부에 따라 바인딩 처리를 함.
                if (D.GetDecimal(_cboTpRout.Items.Count) > 0)
                {
                    object Control = _cboTpRout;

                    _cboTpRout.SelectedIndex = 0;

                    //20110107 최인성 이벤트 강제로 태워야 오더형태 정보를 가져옮????
                    _header_ControlValueChanged(Control, null);
                }
            }

            DataRow[] drs = null;

            if (strSOURCE == "PR_WO")
            {
                if (_flex01.DataTable != null)
                    drs = _flex02.DataTable.Select("NO_LINE = " + dNO_WO_LINE.ToString());

                if (_flex01.HasNormalRow)
                    _flex01.Row = _flex01.Rows.Fixed;

                _tab.SelectedTab = _tab.TabPages["_tpg01"];
            }

            if (strSOURCE == "PR_WO_BILL")
            {
                if (_flex01.DataTable != null)
                    drs = _flex02.DataTable.Select("NO_LINE = " + dNO_WO_LINE.ToString());

                if (_flex01.HasNormalRow)
                    _flex01.Row = _flex01.FindRow(drs[0]["CD_OP"].ToString(), _flex01.Rows.Fixed, _flex02.Cols["CD_OP"].Index, true);
                if (_flex02.HasNormalRow)
                    _flex02.Row = _flex02.FindRow(_flex02.CDecimal(drs[0]["NO_LINE"]), _flex02.Rows.Fixed, _flex02.Cols["NO_LINE"].Index, true);
                _tab.SelectedTab = _tab.TabPages["_tpg02"];
            }

            btn_PR_WorkWo.Visible = false;
            btn_PR_WorkWo.Enabled = false;

            btnBF적용.Click += new EventHandler(btnBF적용_Click);

            _cboCdPlant_SelectionChangeCommitted(null, null);


            //
            // 안전공업전용 서버키처리
            // 작업품목, 지시수량, 경로유형을 화면 닫을 때
            // Settings에 저장된 내용을 다음번 화면 오픈시 넣어달라는 요청
            //
            // + 작업지시현황에서 열었을 때에는 여기를 타면 안된다.
            //
            if (MA.ServerKey(false, new string[] { "ANJUN", "ANJUN2" }) && strSOURCE != "작업지시현황")
            {
                SettingControl_ANJUN();
            }
            //
            // 한일도요에서 사용자정의 숫자1은 무조건 1을 넣어달라는 요청
            //
            else if (MA.ServerKey(false, new string[] { "HANILTOYO" }))
            {
                cur_NUM_USERDEF1.DecimalValue = 1;
                _header.CurrentRow["NUM_USERDEF1"] = 1;
            }

            //
            // LOT번호 자동채번 할 경우 LOT번호 컨트롤을
            // 읽기전용으로 만든다.
            //
            if (LoginInfo.MngLot == "Y" && sLOT번호자동생성 == "100")
            {
                _txtNoLot.ReadOnly = true;
            }

            
            //
            // PMS 사용일 경우에만
            //
            if (App.SystemEnv.PMS사용)
                btn_업무공유WBS.Visible = true;
        }

        #endregion

        #region -> OnCallExistingPageMethod

        public override void OnCallExistingPageMethod(object sender, Duzon.Common.Forms.PageEventArgs e)
        {
            try
            {
                object[] obj = e.Args;

                if (e.Args[0].GetType() == typeof(PageBaseConst.CallType))
                {
                    DataTable dt_WO = _biz.Search_PMS_NO_WO(D.GetString(obj[1]));

                    if (dt_WO.Rows.Count == 1)
                    {
                        NO_WO = D.GetString(dt_WO.Rows[0]["NO_WO"]);
                        PLANT = D.GetString(dt_WO.Rows[0]["CD_PLANT"]);
                        this.dNO_WO_LINE = decimal.Zero;
                        this.strSOURCE = "PMS";
                    }
                }

                InitPaint();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region ♣ 메인버튼 이벤트  ♣

        #region -> 조회버튼

        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch()) return false;

            if (!공장선택여부)
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, _lblCdPlant.Text);
                _cboCdPlant.Focus();
                return false;
            }
            return true;
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSearch()) return;

              //  P_PR_WO_SUB dlg = new P_PR_WO_SUB(_cboCdPlant.SelectedValue.ToString(), _cboStWo.SelectedValue == null ? string.Empty : _cboStWo.SelectedValue.ToString());
                P_PR_WO_NEW_SUB dlg = new P_PR_WO_NEW_SUB(_cboCdPlant.SelectedValue.ToString(), _cboStWo.SelectedValue == null ? string.Empty : _cboStWo.SelectedValue.ToString());
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    OnToolBarAddButtonClicked(null, null);

                    if (dlg.구분 == "복사")
                        b복사여부 = true;

                    DataSet ds = _biz.Search(dlg.공장코드, dlg.작업지시번호);
                    
                    _header.SetDataTable(ds.Tables[0]);

                    if (MA.ServerKey(false, new string[] { "ANJUN", "ANJUN2" }))
                    {
                        txt최근발행번호.Text = _biz.Z_ANJUN_Search_WO( D.GetString(_cboCdPlant.SelectedValue),
                                                                       D.GetString(_bptxtCdItem.CodeValue) );

                        cur계획량.DecimalValue = _biz.Z_ANJUN_Search_계획량( D.GetString(_cboCdPlant.SelectedValue),
                                                                             D.GetString(_bptxtCdItem.CodeValue),
                                                                             D.GetString(dt기준월.Text) );

                        cur발행량.DecimalValue = _biz.Z_ANJUN_Search_발행량( D.GetString(_cboCdPlant.SelectedValue),
                                                                             D.GetString(_bptxtCdItem.CodeValue),
                                                                             D.GetString(dt기준월.Text) );
                    }


                    if (b복사여부)
                    {
                        if (ds.Tables[1].Rows.Count > 0 || ds.Tables[1] != null)
                        {
                            _flex01.Redraw = false;

                            foreach (DataRow dr in ds.Tables[1].Rows)
                            {
                                dr["NO_WO"] = string.Empty;

                                DataRow row = _flex01.DataTable.NewRow();
                                Data.DataCopy(dr, row);
                                _flex01.DataTable.Rows.Add(row);
                            }

                            _flex01.Redraw = true;

                            _flex01.SumRefresh();
                            _flex01.Row = _flex01.Rows.Count - 1;
                            _flex01.Col = _flex01.Cols.Fixed;
                            _flex01.Focus();
                            _flex01.IsDataChanged = true;
                        }

                        if (ds.Tables[2].Rows.Count > 0 || ds.Tables[2] != null)
                        {
                            _flex02.Redraw = false;

                            foreach (DataRow dr in ds.Tables[2].Rows)
                            {
                                dr["NO_WO"] = string.Empty;
                                dr["QT_NEED"] = decimal.Zero;

                                DataRow row = _flex02.DataTable.NewRow();
                                Data.DataCopy(dr, row);
                                _flex02.DataTable.Rows.Add(row);
                            }

                            _flex02.Redraw = true;

                            _flex02.SumRefresh();
                            _flex02.Row = _flex02.Rows.Count - 1;
                            _flex02.Col = _flex02.Cols.Fixed;
                            _flex02.Focus();
                            _flex02.IsDataChanged = true;
                        }

                        if (ds.Tables[3].Rows.Count > 0 || ds.Tables[3] != null)
                        {
                            _flex03.Redraw = false;

                            foreach (DataRow dr in ds.Tables[3].Rows)
                            {
                                DataRow row = _flex03.DataTable.NewRow();
                                Data.DataCopy(dr, row);
                                _flex03.DataTable.Rows.Add(row);
                            }

                            _flex03.Redraw = true;

                            _flex03.SumRefresh();
                            _flex03.Row = _flex03.Rows.Count - 1;
                            _flex03.Col = _flex03.Cols.Fixed;
                            _flex03.Focus();
                            _flex03.IsDataChanged = true;
                        }

                        if (ds.Tables[4].Rows.Count > 0 || ds.Tables[4] != null)
                        {
                            _flex04.Redraw = false;

                            foreach (DataRow dr in ds.Tables[4].Rows)
                            {
                                DataRow row = _flex04.DataTable.NewRow();
                                Data.DataCopy(dr, row);
                                _flex04.DataTable.Rows.Add(row);
                            }

                            _flex04.Redraw = true;

                            _flex04.SumRefresh();
                            _flex04.Row = _flex04.Rows.Count - 1;
                            _flex04.Col = _flex04.Cols.Fixed;
                            _flex04.Focus();
                            _flex04.IsDataChanged = true;
                        }
                    }
                    else
                    {
                        _flex01.Binding = ds.Tables[1];
                        _flex02.Binding = ds.Tables[2];
                        _flex03.Binding = ds.Tables[3];
                        _flex04.Binding = ds.Tables[4];
                    }

                    //_dtPlant = ds.Tables[5];

                    경로유형콤보셋팅();

                    //20100701 소트기능이 SELECT문에 있어서 빼고 CS단에서 처리함. -- 최인성
                    _flex01.DataView.Sort = "NO_LINE ASC";
                    _flex02.DataView.Sort = "NO_LINE ASC";

                    //경로유형을 콤보박스셋팅을 경로유형콤보셋팅(); 이 함수 이후에 해주고 있어서 
                    //헤더를 바인딩해도 데이터가 들어오지 않는다. 
                    //맨 처음 헤더에 셋팅된 정보를 가지고 경로유형콤보를 셋팅하고 있으니...
                    _header.SetDataTable(ds.Tables[0]);

                    _header.AcceptChanges();

                    //자동프로세스 사용여부 에 따른 버튼 활성화 여부
                    sAutoWorkChk();

                    //계획, 확정 상태에 따른 오더형태와 경로 변경 기능 추가로 인하여 RollBack값이 필요함.
                    //처음조회한 Rout 값을 가진다.
                    sPatnRout_Old_Value = _header.CurrentRow[_cboPatnRout.Tag.ToString()].ToString();

                    if (s_대체품적용여부 == "Y")
                    {
                        _tab.SelectedTab = _tpg02;

                        for (int i = _flex02.Rows.Fixed; i < _flex02.Rows.Count; i++)
                        {
                            //
                            // 그리드에서 특정 행의 색상을 바꿈(시작Row, 시작Column, 마지막Row, 마지막Column)
                            //
                            CellRange cr = _flex02.GetCellRange(i, "S", i, "NM_ITEM");

                            //
                            // 스타일 이름 지정
                            //
                            cr.Style = _flex02.Styles.Add("STYLE" + D.GetString(_flex02[i, "NO_LINE"]));

                            if (D.GetString(_flex02[i, "ROW_COLOR"]) == "Y")
                                cr.Style.BackColor = System.Drawing.Color.Yellow;
                            else if (D.GetString(_flex02[i, "ROW_COLOR"]) == "B")
                                cr.Style.BackColor = System.Drawing.Color.RoyalBlue;
                            else
                                cr.Style.BackColor = System.Drawing.Color.White;
                        }
                    }

                    if (b복사여부)
                    {
                        _cboStWo.SelectedValue = "O";
                        _header.CurrentRow["ST_WO"] = "O";

                        _curQtItem.DecimalValue = 0;
                        _header.CurrentRow["QT_ITEM"] = _curQtItem.DecimalValue;
                        _header.CurrentRow["QT_WORK"] = decimal.Zero;

                        _dtpDtRel.Text = Global.MainFrame.GetStringToday;
                        _header.CurrentRow["DT_REL"] = _dtpDtRel.Text;

                        //
                        // SK케미칼의 경우
                        // 시작일자 변경시 공장품목의 유효일자를 가져와 납기일 자동계산
                        //
                        if (MA.ServerKey(true, new string[] { "SKCHEMICAL", "SKPLASMA" }))
                        {
                            string sDtLimit = _dtpDtRel.Text;

                            if (D.GetString(_header.CurrentRow["CD_ITEM"]) != string.Empty)
                            {
                                int dDY_VALID = _biz.Z_SKCHEMICAL_Search_DY_VALID(D.GetString(_header.CurrentRow["CD_PLANT"]), D.GetString(_header.CurrentRow["CD_ITEM"]));

                                if (dDY_VALID != decimal.Zero)
                                {
                                    DateTime tmDtLimit = DateTime.ParseExact(_dtpDtRel.Text, "yyyyMMdd", null);
                                    tmDtLimit = tmDtLimit.AddDays(dDY_VALID);

                                    string sYear = D.GetString(tmDtLimit.Year);
                                    string sMonth = D.GetString(tmDtLimit.Month);
                                    string sDay = D.GetString(tmDtLimit.Day);

                                    if (sYear.Length != 4)
                                    {
                                        switch (sYear.Length)
                                        {
                                            case 1:
                                                sYear = "000" + sYear;
                                                break;
                                            case 2:
                                                sYear = "00" + sYear;
                                                break;
                                            case 3:
                                                sYear = "0" + sYear;
                                                break;
                                            default:
                                                sYear = "9999";
                                                break;
                                        }
                                    }

                                    if (sMonth.Length != 2)
                                        sMonth = "0" + sMonth;

                                    if (sDay.Length != 2)
                                        sDay = "0" + sDay;

                                    sDtLimit = sYear + sMonth + sDay;
                                }
                            }

                            _dpDtLimit.Text = sDtLimit;
                            _header.CurrentRow["DT_LIMIT"] = _dpDtLimit.Text;
                        }

                        // 경로유형 조회해오기
                        object[] Return = _biz.경로유형(_cboCdPlant.SelectedValue == null ? string.Empty : _cboCdPlant.SelectedValue.ToString(), _bptxtCdItem.CodeValue, _dtpDtRel.Text);
                        _dtpDtDue.Text = Return[1].ToString();  // 리드타임
                        _header.CurrentRow["DT_DUE"] = _dtpDtDue.Text;

                        _txtNoWo.Text = string.Empty;
                        _header.CurrentRow["NO_WO"] = string.Empty;

                        _txtNoLot.Text = string.Empty;
                        _header.CurrentRow["NO_LOT"] = string.Empty;

                        _header.JobMode = JobModeEnum.추가후수정;
                        ToolBarDeleteButtonEnabled = false;
                        ToolBarSaveButtonEnabled = true;

                        _cboCdPlant.Enabled = true;
                        m_txt_PJT.Enabled = true;
                        _txtNoWo.Enabled = true;
                        _cboFgWo.Enabled = true;
                        _curQtItem.Enabled = true;
                        _curQtWork.Enabled = true;
                        _cboTpRout.Enabled = true;
                        _cboStWo.Enabled = true;
                        _cboPatnRout.Enabled = true;
                        _txtNoSo.Enabled = true;
                        _btnNoSo.Enabled = true;
                        _txtNoLineSo.Enabled = true;
                        btn수주추적.Enabled = true;
                        _txtNoLot.Enabled = true;
                        _dpDtLimit.Enabled = true;
                        _bptxtNoFr.Enabled = true;
                        _curQtBatch.Enabled = true;
                        txt비고.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 추가버튼

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeAdd()) return;

                _flex01.DataTable.Rows.Clear();
                _flex02.DataTable.Rows.Clear();
                _flex03.DataTable.Rows.Clear();
                _flex04.DataTable.Rows.Clear();
                _flex01.AcceptChanges();
                _flex02.AcceptChanges();
                _flex03.AcceptChanges();
                _flex04.AcceptChanges();

                _header.ClearAndNewRow();
                _header.CurrentRow["CD_PLANT"] = _cboCdPlant.SelectedValue;
                경로유형콤보셋팅();

                _txtNoLot.ReadOnly = false;

                sAutoWorkChk();

                //btn_PR_WorkWo.Visible = false;
                //btn_PR_WorkWo.Enabled = false;

                //20110125 최인성 이형준 
                //추가 버튼 클릭시 오더형태 기본바인딩 안됌.
                if (_cboTpRout.Items.Count > 0)
                {
                    _cboTpRout.SelectedIndex = 0;
                    _header_ControlValueChanged((object)(_cboTpRout), null);
                }

                // 경로유형콤보셋팅을 타게되면 작업기간TO 가 NULL로 변함
                // 작업기간 시작일이 들어올수 있도록 수정
                _dtpDtDue.Text = _dtpDtRel.Text;
                _header.CurrentRow["DT_DUE"] = _dtpDtDue.Text;


                // 안전공업의 경우 화면 초기화시 마지막 셋팅값으로 셋팅
                if (MA.ServerKey(false, new string[] { "ANJUN", "ANJUN2" }))
                {
                    SettingControl_ANJUN();
                }
                //
                // 한일도요에서 사용자정의 숫자1은 무조건 1을 넣어달라는 요청
                //
                else if (MA.ServerKey(false, new string[] { "HANILTOYO" }))
                {
                    cur_NUM_USERDEF1.DecimalValue = 1;
                    _header.CurrentRow["NUM_USERDEF1"] = 1;
                }

                //
                // LOT번호 자동채번 할 경우 LOT번호 컨트롤을
                // 읽기전용으로 만든다.
                //
                if (LoginInfo.MngLot == "Y" && sLOT번호자동생성 == "100")
                {
                    _txtNoLot.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 삭제버튼

        protected override bool BeforeDelete()
        {
            if (!base.BeforeDelete()) return false;

            if (!공장선택여부)
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, _lblCdPlant.Text);
                _cboCdPlant.Focus();
                return false;
            }

            if (_header.JobMode == JobModeEnum.조회후수정)
            {
                //20110121 최인성 정기현 삭제시 아니오 선택해도 삭제돼는 버그 수정
                if (ShowMessage(공통메세지.자료를삭제하시겠습니까) != DialogResult.Yes)
                    return false;

                sAutoWorkChk();
            }
            return true;
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeDelete()) return;

                if (_header.JobMode == JobModeEnum.조회후수정)
                {
                    _biz.Delete(_cboCdPlant.SelectedValue.ToString(), _txtNoWo.Text);
                }
                ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
                OnToolBarAddButtonClicked(sender, e);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 저장버튼

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!base.BeforeSave()) return;

                if (MsgAndSave(PageActionMode.Save))
                {
                    this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);

                    DataSet ds = _biz.Search(D.GetString(_cboCdPlant.SelectedValue), _txtNoWo.Text);

                    _header.SetDataTable(ds.Tables[0]);

                    _flex01.Binding = ds.Tables[1];
                    _flex02.Binding = ds.Tables[2];
                    _flex03.Binding = ds.Tables[3];
                    _flex04.Binding = ds.Tables[4];

                    //_dtPlant = ds.Tables[5];

                    경로유형콤보셋팅();

                    //20100701 소트기능이 SELECT문에 있어서 빼고 CS단에서 처리함. -- 최인성
                    _flex01.DataView.Sort = "NO_LINE ASC";
                    _flex02.DataView.Sort = "NO_LINE ASC";

                    //경로유형을 콤보박스셋팅을 경로유형콤보셋팅(); 이 함수 이후에 해주고 있어서 
                    //헤더를 바인딩해도 데이터가 들어오지 않는다. 
                    //맨 처음 헤더에 셋팅된 정보를 가지고 경로유형콤보를 셋팅하고 있으니...
                    _header.SetDataTable(ds.Tables[0]);

                    _header.AcceptChanges();

                    //자동프로세스 사용여부 에 따른 버튼 활성화 여부
                    sAutoWorkChk();

                    //계획, 확정 상태에 따른 오더형태와 경로 변경 기능 추가로 인하여 RollBack값이 필요함.
                    //처음조회한 Rout 값을 가진다.
                    sPatnRout_Old_Value = _header.CurrentRow[_cboPatnRout.Tag.ToString()].ToString();


                    //오더형태에 따른 자동프로세스 버튼활성화 여부
                    sAutoWorkChk();

                    //
                    //안전공업의 경우 컨트롤 셋팅값 저장및 전용 컨트롤에 대해 재조회
                    //메인 추가버튼을 이용한 화면 초기화시 마지막 저장데이터를 가져오기 위함
                    //저장된데이터의 정보가 전용컨트롤에 재 조회되기 위함
                    //
                    if (MA.ServerKey(false, new string[] { "ANJUN", "ANJUN2" }))
                    {

                        txt최근발행번호.Text = _biz.Z_ANJUN_Search_WO(D.GetString(_cboCdPlant.SelectedValue),
                                                                       D.GetString(_bptxtCdItem.CodeValue));

                        cur계획량.DecimalValue = _biz.Z_ANJUN_Search_계획량(D.GetString(_cboCdPlant.SelectedValue),
                                                                             D.GetString(_bptxtCdItem.CodeValue),
                                                                             D.GetString(dt기준월.Text));

                        cur발행량.DecimalValue = _biz.Z_ANJUN_Search_발행량(D.GetString(_cboCdPlant.SelectedValue),
                                                                             D.GetString(_bptxtCdItem.CodeValue),
                                                                             D.GetString(dt기준월.Text));

                        Settings.Default.회사코드 = Global.MainFrame.LoginInfo.CompanyCode;
                        Settings.Default.작업품목코드 = _bptxtCdItem.CodeValue;
                        Settings.Default.작업품목명 = _bptxtCdItem.CodeName;
                        Settings.Default.작업지시수량 = _curQtItem.DecimalValue;
                        Settings.Default.경로유형코드 = D.GetString(_cboPatnRout.SelectedValue);
                        Settings.Default.기준월 = dt기준월.Text;

                        Settings.Default.Save();
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 인쇄버튼

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforePrint() || 추가모드여부) return;

                ReportHelper rptHelper = new ReportHelper("R_PR_WO_REG_0", "작업지시서");
                 
                rptHelper.SetData("작업지시번호", _header.CurrentRow["NO_WO"].ToString());
                rptHelper.SetData("지시품목코드", _header.CurrentRow["CD_ITEM"].ToString());
                rptHelper.SetData("지시품목명", _header.CurrentRow["NM_ITEM"].ToString());
                rptHelper.SetData("지시품목규격", _header.CurrentRow["STND_ITEM"].ToString());
                rptHelper.SetData("지시품목재고단위", _header.CurrentRow["UNIT_IM"].ToString());
                rptHelper.SetData("지시품목생산단위", _header.CurrentRow["UNIT_MO"].ToString());
                rptHelper.SetData("지시수량", NumericToString(_curQtItem.DecimalValue)); //NumericToString
                rptHelper.SetData("오더형태", _cboTpRout.Text);
                rptHelper.SetData("TRACKING_NO", _header.CurrentRow["NO_SO"].ToString());
                rptHelper.SetData("TRACKING_NO_LINE", _header.CurrentRow["NO_LINE_SO"].ToString());
                rptHelper.SetData("공장", _cboCdPlant.Text.Replace("(", "").Replace(")", "").Replace(_cboCdPlant.SelectedValue.ToString(), ""));
                rptHelper.SetData("작업기간시작", _dtpDtRel.Text);
                rptHelper.SetData("작업기간끝", _dtpDtDue.Text);
                rptHelper.SetData("경로유형", _cboPatnRout.Text);
                rptHelper.SetData("LOT_NO", _txtNoLot.Text);
                rptHelper.SetData("프로젝트코드", m_txt_PJT.CodeValue);//20110126 최인성, 김성호, 정찬용
                rptHelper.SetData("프로젝트명", m_txt_PJT.CodeName);//20110126 최인성, 김성호, 정찬용
                rptHelper.SetData("비고", txt비고.Text);//20110126 최인성, 김성호, 정찬용
                rptHelper.SetData("목표수율", D.GetString(_curQtBatch.DecimalValue));

                if (Global.MainFrame.ServerKeyCommon.ToUpper().Contains("GSITM"))
                {
                    //파워카본테크놀러지 전용
                    rptHelper.SetDataTable(_flex01.DataTable, 1, 0);
                    rptHelper.SetDataTable(_flex01.DataTable, 1, 17);
                    rptHelper.SetDataTable(_flex02.DataTable, 1, 20);
                }
                else if (MA.ServerKey(false, new string[] { "ANJUN", "ANJUN2" }))
                {

                    rptHelper.SetData("계획량", D.GetString(cur계획량.DecimalValue));
                    rptHelper.SetData("발행량", D.GetString(cur발행량.DecimalValue));
                    rptHelper.SetData("기준월", dt기준월.Text);
                    rptHelper.SetData("최근발행번호", txt최근발행번호.Text);

                    DataTable dt01 = _biz.Z_ANJUN_Print((string)_cboCdPlant.SelectedValue, _txtNoWo.Text);
                    rptHelper.SetDataTable(dt01, 1, 0);
                    rptHelper.SetDataTable(_flex01.DataTable, 1, 17);
                    rptHelper.SetDataTable(_flex02.DataTable, 1, 20);
                    
                }
                else if (MA.ServerKey(false, new string[] { "KSSYSTEM" }))
                {
                    object[] obj = {   Global.MainFrame.LoginInfo.CompanyCode,
                                       D.GetString(_cboCdPlant.SelectedValue), 
                                       D.GetString(_header.CurrentRow["CD_ITEM"]), 
                                       D.GetString(_cboPatnRout.SelectedValue)      };

                    DataSet ds = _biz.Z_KSSYSTEM_Print(obj);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        rptHelper.SetData("지시품목상세규격", D.GetString(ds.Tables[0].Rows[0]["STND_DETAIL_ITEM"]));
                        rptHelper.SetData("지시품목재질", D.GetString(ds.Tables[0].Rows[0]["MAT_ITEM"]));
                    }

                    DataRow[] drs = _flex03.DataTable.Select("CD_ITEM = '" + D.GetString(_header.CurrentRow["CD_ITEM"]) + "' ");
                    if (drs.Length > 0)
                    {
                        rptHelper.SetData("수주업체명", D.GetString(drs[0]["LN_PARTNER"]));
                        rptHelper.SetData("수주수량", D.GetString(drs[0]["QT_IM"]));
                    }


                    rptHelper.SetDataTable(_flex02.DataTable, 1, 0);

                    if (ds.Tables[1].Rows.Count > 0)
                        rptHelper.SetDataTable(ds.Tables[1], 1, 17);

                    if (ds.Tables[2].Rows.Count > 0)
                        rptHelper.SetDataTable(ds.Tables[2], 1, 20);

                    rptHelper.SetDataTable(_flex01.DataTable, 1, 23);
                }
                else if (MA.ServerKey(false, new string[] { "HANILTOYO" }))
                {
                    object[] obj = new object[]
                    {
                        Global.MainFrame.LoginInfo.CompanyCode,
                        D.GetString(_cboCdPlant.SelectedValue),
                        _txtNoWo.Text
                    };

                    DataSet dsHANILTOYO = _biz.Z_HANILTOYO_Print(obj);

                    for (int i = 0; i < dsHANILTOYO.Tables.Count; i++)
                    {
                        rptHelper.SetDataTable(dsHANILTOYO.Tables[i], i + 1);
                    }
                }
                else if (MA.ServerKey(false, new string[] { "DHC", "DHC2" }))
                {
                    object[] obj = new object[]
                    {
                        Global.MainFrame.LoginInfo.CompanyCode,
                        _txtNoWo.Text + "|"
                    };

                    rptHelper.SetDataTable(_biz.Z_DHC_Print(obj));
                }
                else
                {
                    rptHelper.SetDataTable(_flex02.DataTable);
                }

                rptHelper.가로출력();
                rptHelper.Print();
                 
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 종료버튼
        
        public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
        {
            if (MA.ServerKey(false, new string[] { "ANJUN", "ANJUN2" }))
            {
                Settings.Default.회사코드 = Global.MainFrame.LoginInfo.CompanyCode;
                Settings.Default.작업품목코드 = _bptxtCdItem.CodeValue;
                Settings.Default.작업품목명 = _bptxtCdItem.CodeName;
                Settings.Default.작업지시수량 = _curQtItem.DecimalValue;
                Settings.Default.경로유형코드 = D.GetString(_cboPatnRout.SelectedValue);
                Settings.Default.기준월 = dt기준월.Text;

                Settings.Default.Save();
            }

            return base.OnToolBarExitButtonClicked(sender, e);
        }

        #endregion

        #endregion

        #region ♣ 화면내버튼이벤트 ♣

        #region -> 경로관리_Click

        private void btn경로관리_Click(object sender, EventArgs e)
        {
            try
            {
                if (!HeaderCheck()) return;

                //공정경로가 세팅되어 있다면 경로관리세팅 화면을 띄우지 못하게 한다.
                if (_cboPatnRout.SelectedValue != null && _cboPatnRout.SelectedValue.ToString() != "")
                {
                    ShowMessage("공정경로가 세팅되 있습니다. 경로관리세팅 화면을 띄울 수 없습니다.");
                    return;
                }

                if (_flex01.HasNormalRow)
                {
                    if (ShowMessage(공통메세지.기존에등록된자료가있습니다삭제후다시작업하시겠습니까) != DialogResult.Yes)
                    {
                        return;
                    }
                }

                P_PR_WO_ROUT_SET_SUB dlg = new P_PR_WO_ROUT_SET_SUB(_cboCdPlant.SelectedValue.ToString(), _bptxtCdItem.CodeValue);

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    DataTable dt = dlg.ReturnDataTable;

                    if (dt == null) return;

                    #region -> 경로정보삭제

                    if (_flex01.HasNormalRow)
                    {
                        DataRow[] drRout = _flex01.DataTable.Select("S <> 'Y'", "", DataViewRowState.CurrentRows);

                        _flex01.Redraw = false;
                        foreach (DataRow row in drRout)
                        {
                            row["S"] = "Y";
                        }
                        _flex01.Redraw = true;

                        경로정보삭제_Click(null, null);
                    }

                    #endregion

                    #region -> 소요자재정보삭제

                    if (_flex02.HasNormalRow)
                    {
                        DataRow[] drMatl = _flex02.DataTable.Select("S <> 'Y'", "", DataViewRowState.CurrentRows);

                        _flex02.Redraw = false;
                        foreach (DataRow row in drMatl)
                        {
                            row["S"] = "Y";
                        }
                        _flex02.Redraw = true;

                        소요자재삭제_Click(null, null);
                    }

                    #endregion

                    //--------------------------------------------

                    decimal MaxSeq = 0;

                    _flex01.Redraw = false;
                    DataRow NewRow = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        NewRow = _flex01.DataTable.NewRow();
                        NewRow["NO_WO"] = _txtNoWo.Text;
                        NewRow["NO_LINE"] = ++MaxSeq;
                        NewRow["CD_PLANT"] = _cboCdPlant.SelectedValue.ToString();
                        NewRow["CD_OP"] = row["CD_OP"];
                        NewRow["CD_WC"] = row["CD_WC"];
                        NewRow["NM_WC"] = row["NM_WC"];
                        NewRow["FG_WC"] = row["FG_WC"];
                        NewRow["CD_WCOP"] = row["CD_WCOP"];
                        NewRow["NM_WCOP"] = row["NM_WCOP"];
                        NewRow["ST_OP"] = "O";
                        NewRow["QT_WO"] = _curQtItem.DecimalValue;
                        NewRow["YN_BF"] = row["YN_BF"];
                        NewRow["YN_PAR"] = row["YN_PAR"];
                        NewRow["YN_QC"] = row["YN_QC"];
                        NewRow["YN_RECEIPT"] = 경로유형MS여부;
                        NewRow["DT_REL"] = _dtpDtRel.Text;
                        NewRow["DT_DUE"] = _dtpDtDue.Text;
                        NewRow["YN_FINAL"] = "N";
                        NewRow["YN_SUBCON"] = row["YN_SUBCON"];
                        NewRow["NO_SFT"] = string.Empty;
                        _flex01.DataTable.Rows.Add(NewRow);
                    }
                    _flex01.Redraw = true;
                    _flex01.Row = _flex01.Rows.Count - 1;
                    _flex01[_flex01.Row, "YN_FINAL"] = "Y";
                    _flex01.IsDataChanged = true;

                    //--------------------------------------------
                }

                Page_DataChanged(null, null);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 경로재전개_Click

        private void 경로재전개_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MsgAndSave(PageActionMode.Search)) return;
                if (!HeaderCheck()) return;

                if (_flex01.HasNormalRow)
                {
                    if (ShowMessage(공통메세지.기존에등록된자료가있습니다삭제후다시작업하시겠습니까) != DialogResult.Yes)
                    {
                        return;
                    }
                }

                if (!경로재전개())
                {
                    ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                    return;
                }

                Page_DataChanged(null, null);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        bool 경로재전개()
        {
            string str경로유형 = _cboPatnRout.SelectedValue == null ? "" : _cboPatnRout.SelectedValue.ToString();
            string str_max_op = string.Empty, str_min_op = string.Empty;

            DataTable dt = null;

            if (chk최근지시.Checked == false)
            {
                dt = _biz.경로재전개(_cboCdPlant.SelectedValue.ToString(), _bptxtCdItem.CodeValue, str경로유형);
            }
            else if (chk최근지시.Checked == true)
            {
                dt = _biz.PR_ROUT_MAX_LIST(_cboCdPlant.SelectedValue.ToString(), _bptxtCdItem.CodeValue, str경로유형);
            }

            if (dt == null || dt.Rows.Count == 0)
            {
                return false;
            }

            _flex01.Redraw = false;
            _flex01.RemoveViewAll();
            _flex01.Redraw = true;

            decimal MaxSeq = 0;

            _flex01.Redraw = false;
            DataRow NewRow = null;

            foreach (DataRow row in dt.Rows)
            {
                NewRow = _flex01.DataTable.NewRow();

                NewRow["NO_WO"] = _txtNoWo.Text;
                NewRow["NO_LINE"] = ++MaxSeq;
                NewRow["CD_PLANT"] = _cboCdPlant.SelectedValue.ToString();
                NewRow["CD_OP"] = row["CD_OP"];
                NewRow["CD_WC"] = row["CD_WC"];
                NewRow["NM_WC"] = row["NM_WC"];
                NewRow["FG_WC"] = row["FG_WC"];
                NewRow["CD_WCOP"] = row["CD_WCOP"];
                NewRow["NM_WCOP"] = row["NM_WCOP"];
                NewRow["ST_OP"] = "O";
                NewRow["QT_WO"] = _curQtItem.DecimalValue;
                NewRow["YN_BF"] = row["YN_BF"];
                NewRow["YN_PAR"] = row["YN_PAR"];
                NewRow["YN_QC"] = row["YN_QC"];
                NewRow["YN_RECEIPT"] = 경로유형MS여부;
                NewRow["DT_REL"] = _dtpDtRel.Text;
                NewRow["DT_DUE"] = _dtpDtDue.Text;
                NewRow["YN_FINAL"] = "N";
                NewRow["YN_SUBCON"] = row["YN_SUBCON"];
                NewRow["NO_SFT"] = string.Empty;
                NewRow["CD_EQUIP"] = row["CD_EQUIP"];
                NewRow["NM_EQUIP"] = row["NM_EQUIP"];
                NewRow["RT_YIELD"] = row["RT_YIELD"];
                NewRow["YN_ROUT_SU_IV"] = row["YN_ROUT_SU_IV"];
                NewRow["CD_POST"] = row["CD_POST"];
                NewRow["NM_POST"] = row["NM_POST"];
                NewRow["NUM_USERDEF1"] = row["NUM_USERDEF1"];
                NewRow["NUM_USERDEF2"] = row["NUM_USERDEF2"];
                NewRow["NUM_USERDEF3"] = row["NUM_USERDEF3"];
                NewRow["TXT_USERDEF1"] = row["TXT_USERDEF1"];
                NewRow["TXT_USERDEF2"] = row["TXT_USERDEF2"];
                NewRow["TXT_USERDEF3"] = row["TXT_USERDEF3"];

                _flex01.DataTable.Rows.Add(NewRow);
            }

            _flex01.Redraw = true;
            _flex01.Row = _flex01.Rows.Count - 1;
            _flex01[_flex01.Row, "YN_FINAL"] = "Y";
            _flex01.IsDataChanged = true;

            //
            // 파워카본테크놀로지(주)
            //
            if (Global.MainFrame.ServerKeyCommon.ToUpper().Contains("GSITM"))
            {
                for (int i = _flex01.Rows.Count - 1; i >= _flex01.Rows.Fixed; i--)
                {
                    if (D.GetDecimal(_flex01[i, "RT_YIELD"]) != 0)
                    {
                        if (i == _flex01.Rows.Count - 1)
                            _flex01[i, "QT_WO"] = Unit.수량(DataDictionaryTypes.PR, (D.GetDecimal(_flex01[i, "QT_WO"]) / (D.GetDecimal(_flex01[i, "RT_YIELD"]) / 100)));
                        else
                            _flex01[i, "QT_WO"] = Unit.수량(DataDictionaryTypes.PR, (D.GetDecimal(_flex01[i + 1, "QT_WO"]) / (D.GetDecimal(_flex01[i, "RT_YIELD"]) / 100)));
                    }
                }

                object obj_max = _flex01.DataTable.Compute("MAX(CD_OP)", "");
                str_max_op = D.GetString(obj_max);

                object obj_min = _flex01.DataTable.Compute("MIN(CD_OP)", "");
                str_min_op = D.GetString(obj_min);

                DataRow[] dr_max = _flex01.DataTable.Select("CD_OP = '" + str_max_op + "'", "", DataViewRowState.CurrentRows);
                DataRow[] dr_min = _flex01.DataTable.Select("CD_OP = '" + str_min_op + "'", "", DataViewRowState.CurrentRows);

                if (dr_max.Length > 0 && dr_min.Length > 0)
                {
                    _curQtBatch.DecimalValue = Unit.수량(DataDictionaryTypes.PR, (D.GetDecimal(dr_max[0]["QT_WO"]) / D.GetDecimal(dr_min[0]["QT_WO"]) * 100));
                }
            }

            //
            // 한일도요 전용로직
            //
            if (MA.ServerKey(false, new string[] { "HANILTOYO" }))
            {
                if (cur_NUM_USERDEF1.DecimalValue > 0)
                {
                    for (int i = _flex01.Rows.Count - 1; i >= _flex01.Rows.Fixed; i--)
                    {
                        _flex01[i, "DC_RMK"] = Math.Round(_curQtItem.DecimalValue / cur_NUM_USERDEF1.DecimalValue, 4).ToString("###.###0");
                    }
                }
            }

            //
            // 현대위아IHI 전용로직
            //
            if (MA.ServerKey(false, new string[] { "HDWIA", "HDWIA2" }))
            {
                DataTable dt_Pitem = _biz.Search_MA_Pitem(D.GetString(_cboCdPlant.SelectedValue), _bptxtCdItem.CodeValue);

                if (dt_Pitem.Rows.Count == 1)
                {
                    for (int i = _flex01.Rows.Count - 1; i >= _flex01.Rows.Fixed; i--)
                    {
                        decimal d_UPH = D.GetDecimal(dt_Pitem.Rows[0]["UPH"]);
                        //decimal d_소요시간 = _curQtItem.DecimalValue * d_UPH;
                        decimal d_소요시간 = d_UPH == decimal.Zero? decimal.Zero : 
                            (_curQtItem.DecimalValue * (3600m / d_UPH)) / 3600m;

                        _flex01[i, "NUM_USERDEF1"] = Unit.수량(DataDictionaryTypes.PR, d_소요시간);
                    }
                }
            }

            return true;
        }

        #endregion

        #region -> btnBF적용_Click
        void btnBF적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flex02.HasNormalRow) return;

                DataRow[] drs = _flex02.DataTable.Select("S = 'Y'");

                if (drs == null || drs.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                string sYN = chkBF.Checked == true ? "Y" : "N";

                foreach (DataRow dr in drs)
                {
                    dr["YN_BF"] = sYN;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region -> 소요량재전개_Click

        private void 소요량재전개_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MsgAndSave(PageActionMode.Search)) return;
                if (!HeaderCheck()) return;

                if (_flex02.HasNormalRow)
                {
                    if (ShowMessage(공통메세지.기존에등록된자료가있습니다삭제후다시작업하시겠습니까) != DialogResult.Yes)
                    {
                        return;
                    }
                }

                if (!소요량재전개())
                {
                    ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                    return;
                }

                Page_DataChanged(null, null);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        bool 소요량재전개()
        {
            DataTable dt = null, dt_Temp = null, dt_Opt_Temp = null;

            string strTpRout = "000";

            if (_cboTpRout.SelectedIndex >= 0)
            {
                DataTable dtTpRout = _cboTpRout.DataSource as DataTable;

                strTpRout = D.GetString(dtTpRout.Rows[_cboTpRout.SelectedIndex]["FG_TYPE"]);
                if (strTpRout == null || strTpRout == "") strTpRout = "000";
            }
            
            string str경로유형 = _cboPatnRout.SelectedValue == null ? "" : _cboPatnRout.SelectedValue.ToString();

            if (chk최근지시.Checked == false)
            {
                if (strTpRout == "000")                 // 일반
                {
                    #region 일반
                    //수주등록-사양등록 사용여부 //000:사용안함: 001:사용함
                    if (str_Fg_Option == "000")
                    {
                        #region //수주등록-사양등록 사용여부 //000:사용안함

                        object[] obj = new object[] 
                        { 
                          Global.MainFrame.LoginInfo.CompanyCode, 
                          D.GetString(_cboCdPlant.SelectedValue),
                          _bptxtCdItem.CodeValue,
                          str경로유형,
                          string.Empty,
                          _dtpDtRel.Text
                        };

                        dt_Temp = _biz.ReSearch_Material(obj);

                        #endregion
                    }
                    else if (str_Fg_Option == "001")
                    {
                        #region 수주등록-사양등록 사용여부 001:사용함

                        //수주등록 사양등록을 사용한 수주건인지 여부 체크
                        //False : "N", True :"Y"
                        bool bSa_Yn_Option = false; 

                        object[] obj = new object[] 
                        { 
                          Global.MainFrame.LoginInfo.CompanyCode, 
                          D.GetString(_cboCdPlant.SelectedValue),
                          D.GetString(_bptxtCdItem.CodeValue),
                          null,
                          null
                        };

                        dt_Temp = _biz.ReSearch_Opt_Material(obj);

                        for (int i = _flex03.Rows.Fixed; i < _flex03.Rows.Count; i++)
                        {
                            if (D.GetString(_flex03[i, "YN_OPTION"]) == "Y")
                            {
                                object[] obj2 = new object[] 
                                { 
                                  Global.MainFrame.LoginInfo.CompanyCode, 
                                  D.GetString(_cboCdPlant.SelectedValue),
                                  D.GetString(_bptxtCdItem.CodeValue),
                                  _flex03[i, "NO_SO"],
                                  _flex03[i, "SEQ_SO"]
                                };


                                dt_Opt_Temp = _biz.ReSearch_Opt_Material(obj2);

                                foreach (DataRow dr in dt_Opt_Temp.Rows)
                                {
                                    dt_Temp.LoadDataRow(dr.ItemArray, false);
                                }

                                bSa_Yn_Option = true;
                            }
                        }

                        if (_flex03.DataView.Count == 0 || bSa_Yn_Option == false)
                        {
                            obj = new object[] {Global.MainFrame.LoginInfo.CompanyCode, 
                                                D.GetString(_cboCdPlant.SelectedValue),
                                                _bptxtCdItem.CodeValue,
                                                str경로유형,
                                                string.Empty,
                                                _dtpDtRel.Text
                                                };

                            dt_Temp = _biz.ReSearch_Material(obj);

                            if (s_대체품적용여부 == "Y")
                            {
                                for (int i = _flex02.Rows.Fixed; i < _flex02.Rows.Count - 1; i++)
                                {
                                    //
                                    // 그리드에서 특정 행의 색상을 바꿈(시작Row, 시작Column, 마지막Row, 마지막Column)
                                    //
                                    CellRange cr = _flex02.GetCellRange(i, "S", i, "NM_ITEM");

                                    //
                                    // 스타일 이름 지정
                                    //
                                    cr.Style = _flex02.Styles.Add("STYLE" + D.GetString(i));

                                    if (D.GetString(_flex02[i, "ROW_COLOR"]) == "Y")
                                        cr.Style.BackColor = System.Drawing.Color.Yellow;
                                    else if (D.GetString(_flex02[i, "ROW_COLOR"]) == "B")
                                        cr.Style.BackColor = System.Drawing.Color.RoyalBlue;
                                    else
                                        cr.Style.BackColor = System.Drawing.Color.White;
                                }
                            }
                        }
                        #endregion
                    }

                    #endregion
                }
                else if (strTpRout == "001")            // 배합
                {
                    #region 배함
                    object[] obj = new object[] 
                    { 
                      Global.MainFrame.LoginInfo.CompanyCode, 
                      D.GetString(_cboCdPlant.SelectedValue),
                      D.GetString(_bptxtNoFr.CodeValue),
                      _bptxtCdItem.CodeValue,
                      str경로유형,
                      string.Empty,
                      _dtpDtRel.Text
                    };

                    dt_Temp = _biz.ReSearch_Formula(obj);
                    #endregion
                }
            }
            else if (chk최근지시.Checked == true)
            {
                #region 최근작업지시적용
                object[] obj = new object[] 
                { 
                    Global.MainFrame.LoginInfo.CompanyCode, 
                    D.GetString(_cboCdPlant.SelectedValue),
                    _bptxtCdItem.CodeValue,
                    str경로유형,
                    string.Empty,
                    _dtpDtRel.Text
                };

                dt_Temp = _biz.PR_MATL_MAX_LIST(obj);
                #endregion
            }

            if (dt_Temp == null || dt_Temp.Rows.Count == 0) return false;

            //생산유형설정 ( 001:일반, 002:재작업, 003:검사)
            //재작업과 검사일 경우에는 헤더 품목을 그대로 내려준다. 
            if (sTp_Wo_Prod != "001")
            {
                dt = dt_Temp.Clone();
                DataRow dr = dt.NewRow();
                dr = dt_Temp.Rows[0];
                dt.Rows.Add(dr.ItemArray);
                dt.Rows[0]["CD_MATL"] = _bptxtCdItem.CodeValue;
                dt.Rows[0]["NM_ITEM"] = _bptxtCdItem.CodeName;
                dt.Rows[0]["STND_ITEM"] = _txtStndItem.Text;
                dt.Rows[0]["UNIT_MO"] = _txtUnitMo.Text;
                dt.Rows[0]["QT_NEED"] = 1;
                dt.Rows[0]["STND_DETAIL_ITEM"] = txt_세부규격.Text;
                dt.Rows[0]["MAT_ITEM"] = txt_재질.Text;
            }
            else
                dt = dt_Temp.Copy();

            _flex02.Redraw = false;
            _flex02.RemoveViewAll();
            _flex02.Redraw = true;

            decimal MaxSeq = 0;

            if (s_대체품적용여부 == "Y")
                _tab.SelectedTab = _tpg02;

            _flex02.Redraw = false;

            //DataRow NewRow = null;

            if (!dt.Columns.Contains("CD_TUIP_GROUP"))
                dt.Columns.Add("CD_TUIP_GROUP", typeof(string));
            if (!dt.Columns.Contains("NO_TUIP_SEQ"))
                dt.Columns.Add("NO_TUIP_SEQ", typeof(decimal));

            foreach (DataRow row in dt.Rows)
            {
                decimal d_QT_NEED = decimal.Zero;
                decimal d_QT_NEED_NET = decimal.Zero;

                //
                // 2015.02.10 김현수.
                // D20150130001 삼원산업 전용로직
                // 자재 소요수량 올림처리하여 정수로 변경함.
                //
                if (MA.ServerKey(false, new string[] { "SAMWON" }))
                {
                    d_QT_NEED = Math.Ceiling(_curQtItem.DecimalValue * D.GetDecimal(row["QT_NEED"]));
                    d_QT_NEED_NET = Unit.수량(DataDictionaryTypes.PR, d_QT_NEED / _curQtItem.DecimalValue);
                }
                //
                // 2015.02.10 김현수.
                // D20150130001 삼원산업 전용로직
                // 자재 소요수량 올림처리하여 정수로 변경함. END
                //

                //
                // 2015.04.08 김현수.
                // D20150408047 SK플라즈마 전용로직
                // 배합표사용시 배합표에 등록된 구성량을 단위소요량으로 보고,
                // 소요수량에도 단위소요량 * BATCH수량을 해준다.
                //
                else if ((MA.ServerKey(true, new string[] { "SKCHEMICAL", "SKPLASMA" })) &&
                         (strTpRout == "001"))
                {
                    d_QT_NEED = Unit.수량(DataDictionaryTypes.PR, D.GetDecimal(row["QT_NEED"]) * _curQtBatch.DecimalValue);
                    d_QT_NEED_NET = D.GetDecimal(row["QT_NEED"]);
                }
                //
                // 2015.04.08 김현수.
                // D20150408047 SK플라즈마 전용로직
                // 배합표사용시 배합표에 등록된 구성량을 단위소요량으로 본다.
                //

                else
                {
                    d_QT_NEED = _curQtItem.DecimalValue * Convert.ToDecimal(row["QT_NEED"]);
                    d_QT_NEED_NET = Unit.수량(DataDictionaryTypes.PR, d_QT_NEED / _curQtItem.DecimalValue);
                }

                _flex02.Rows.Add();
                _flex02.Row = _flex02.Rows.Count - 1;

                _flex02["NO_WO"] = _txtNoWo.Text;
                _flex02["NO_LINE"] = ++MaxSeq;
                _flex02["CD_PLANT"] = D.GetString(_cboCdPlant.SelectedValue);
                _flex02["CD_OP"] = row["CD_OP"];
                _flex02["CD_WC"] = row["CD_WC"];
                _flex02["NM_WC"] = row["NM_WC"];
                _flex02["CD_WCOP"] = row["CD_WCOP"];
                _flex02["NM_WCOP"] = row["NM_WCOP"];
                _flex02["CD_MATL"] = row["CD_MATL"];
                _flex02["NM_ITEM"] = row["NM_ITEM"];
                _flex02["STND_ITEM"] = row["STND_ITEM"];
                _flex02["UNIT_MO"] = row["UNIT_MO"];
                _flex02["FG_SERNO"] = row["FG_SERNO"];
                _flex02["QT_NEED"] = d_QT_NEED;
                _flex02["QT_NEED_NET"] = d_QT_NEED_NET;
                _flex02["YN_BF"] = row["YN_BF"];
                _flex02["FG_GIR"] = row["FG_GIR"];
                _flex02["CD_SL_OT"] = row["CD_SL_OT"];
                _flex02["NM_SL_OT"] = row["NM_SL_OT"];
                _flex02["QT_INV"] = row["QT_INV"];       //20110328 최인성 출고창고 현재고 가져오도록 수정
                _flex02["DC_RMK"] = row["DC_RMK"];
                _flex02["CD_TUIP_GROUP"] = row["CD_TUIP_GROUP"];
                _flex02["NO_TUIP_SEQ"] = row["NO_TUIP_SEQ"];
                _flex02["STND_DETAIL_ITEM"] = row["STND_DETAIL_ITEM"];
                _flex02["MAT_ITEM"] = row["MAT_ITEM"];

                if (s_대체품적용여부 == "Y")
                {
                    _flex02["ROW_COLOR"] = row["ROW_COLOR"];

                    //
                    // 그리드에서 특정 행의 색상을 바꿈(시작Row, 시작Column, 마지막Row, 마지막Column)
                    //
                    CellRange cr = _flex02.GetCellRange(_flex02.Row, "S", _flex02.Row, "NM_ITEM");

                    //
                    // 스타일 이름 지정
                    //
                    cr.Style = _flex02.Styles.Add("STYLE" + D.GetString(_flex02.Row));

                    if (D.GetString(_flex02["ROW_COLOR"]) == "Y")
                        cr.Style.BackColor = System.Drawing.Color.Yellow;
                    else if (D.GetString(_flex02["ROW_COLOR"]) == "B")
                        cr.Style.BackColor = System.Drawing.Color.RoyalBlue;
                    else
                        cr.Style.BackColor = System.Drawing.Color.White;
                }

                _flex02.AddFinished();
            }

            _flex02.Redraw = true;

            _flex02.Row = _flex02.Rows.Count - 1;
            _flex02.IsDataChanged = true;



            return true;
        }

        #endregion

        #region -> 경로정보추가_Click

        private void 경로정보추가_Click(object sender, EventArgs e)
        {
            try
            {
                if (!공장선택여부)
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, _lblCdPlant.Text);
                    _cboCdPlant.Focus();
                    return;
                }

                if (!작업지시수량입력여부)
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, _lblQtItem.Text);
                    _curQtItem.Focus();
                    return;
                }

                decimal MaxNoLine = _flex01.GetMaxValue("NO_LINE");
                MaxNoLine++;

                decimal dMaxOP = _flex01.GetMaxValue("CD_OP");

                dMaxOP = dMaxOP == 0m ? 100 : dMaxOP + 10m;

                _flex01.Rows.Add();
                _flex01.Row = _flex01.Rows.Count - 1;
                _flex01[_flex01.Row, "CD_PLANT"] = _cboCdPlant.SelectedValue;
                _flex01[_flex01.Row, "NO_WO"] = _txtNoWo.Text;
                _flex01[_flex01.Row, "CD_OP"] = dMaxOP.ToString("####################");
                _flex01[_flex01.Row, "NO_LINE"] = MaxNoLine;
                _flex01[_flex01.Row, "DT_REL"] = _dtpDtRel.Text;
                _flex01[_flex01.Row, "DT_DUE"] = _dtpDtDue.Text;
                _flex01[_flex01.Row, "YN_RECEIPT"] = 경로유형MS여부;
                _flex01[_flex01.Row, "QT_WO"] = _curQtItem.DecimalValue;
                _flex01[_flex01.Row, "NO_SFT"] = string.Empty;
                _flex01.AddFinished();
                _flex01.Col = _flex01.Cols.Fixed;

                _btnDel01.Enabled = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 경로정보삭제_Click

        private void 경로정보삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flex01.HasNormalRow) return;

                DataRow[] dr = _flex01.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);
                if (dr == null || dr.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                _flex01.Redraw = false;
                foreach (DataRow row in dr)
                    row.Delete();
                _flex01.Redraw = true;
                _flex01.IsDataChanged = true;
                경로최종실적셋팅();
                Page_DataChanged(null, null);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 소요자재추가_Click

        private void 소요자재추가_Click(object sender, EventArgs e)
        {
            try
            {
                if (!공장선택여부)
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, _lblCdPlant.Text);
                    _cboCdPlant.Focus();
                    return;
                }

                // 오더형태
                if (_cboTpRout.SelectedValue == null || _cboTpRout.SelectedValue.ToString() == string.Empty)
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, _lblTpWo.Text);
                    _cboTpRout.Focus();
                    return;
                }

                if (!작업지시수량입력여부)
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, _lblQtItem.Text);
                    _curQtItem.Focus();
                    return;
                }

                if (!_flex01.HasNormalRow)
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, DD("공정경로"));
                    return;
                }

                decimal MaxNoLine = _flex02.GetMaxValue("NO_LINE");
                MaxNoLine++;

                _flex02.Rows.Add();
                _flex02.Row = _flex02.Rows.Count - 1;
                _flex02[_flex02.Row, "CD_PLANT"] = _cboCdPlant.SelectedValue;
                _flex02[_flex02.Row, "NO_WO"] = _txtNoWo.Text;
                _flex02[_flex02.Row, "NO_LINE"] = MaxNoLine;
                _flex02[_flex02.Row, "QT_INV"] = 0;

                if (_flex01.HasNormalRow)
                {
                    _flex02[_flex02.Row, "CD_OP"] = _flex01[_flex01.Row, "CD_OP"];
                    _flex02[_flex02.Row, "CD_WC"] = _flex01[_flex01.Row, "CD_WC"];
                    _flex02[_flex02.Row, "NM_WC"] = _flex01[_flex01.Row, "NM_WC"];
                    _flex02[_flex02.Row, "CD_WCOP"] = _flex01[_flex01.Row, "CD_WCOP"];
                    _flex02[_flex02.Row, "NM_WCOP"] = _flex01[_flex01.Row, "NM_WCOP"];
                }
                //_flex02[_flex02.Row, "YN_BF"] = "N";
                //_flex02[_flex02.Row, "FG_GIR"] = "N";
                _flex02.AddFinished();
                _flex02.Col = _flex02.Cols.Fixed;

                _btnDel02.Enabled = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 소요자재삭제_Click

        private void 소요자재삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flex02.HasNormalRow) return;

                DataRow[] dr = _flex02.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);
                if (dr == null || dr.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                _flex02.Redraw = false;
                foreach (DataRow row in dr)
                    row.Delete();
                _flex02.Redraw = true;
                _flex02.IsDataChanged = true;
                Page_DataChanged(null, null);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 수주정보추적정보 클릭 이벤트(_btnDel03_Click)

        private void _btnDel03_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flex03.HasNormalRow) return;

                DataRow[] drs = _flex03.DataTable.Select("CHK = 'Y'", "", DataViewRowState.CurrentRows);
                if (drs == null || drs.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                _flex03.Redraw = false;
                foreach (DataRow row in drs)
                    row.Delete();
                _flex03.Redraw = true;
                _flex03.IsDataChanged = true;

                DataRow[] drsSum = _flex03.DataTable.Select("", "", DataViewRowState.CurrentRows);

                decimal dAPPLY = 0m;

                foreach (DataRow dr in drsSum)
                {
                    dAPPLY += _flex03.CDecimal(dr["QT_APPLY"]);
                }

                ChangeQtWoFromQtSo();

                _curQtItem.DecimalValue = dAPPLY;
                _header.CurrentRow["QT_ITEM"] = _curQtItem.DecimalValue;

                ChangeQtWo();

                Page_DataChanged(null, null);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #region -> TrackingNo 도움창

        private void Control_Click(object sender, EventArgs e)
        {
            try
            {
                if (!공장선택여부)
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, _lblCdPlant.Text);
                    _cboCdPlant.Focus();
                    return;
                }

                //20110120 최인성 김성호 도움창 변경
                bool b수량체크여부 = false;

                //'002' : MRP, '004' " 소요자재
                if (_cboFgWo.SelectedValue.ToString() == "002" || _cboFgWo.SelectedValue.ToString() == "004")
                {
                    b수량체크여부 = true;
                }

                P_PR_SA_SO_TRACKING_SUB dlg = new P_PR_SA_SO_TRACKING_SUB(_cboCdPlant.SelectedValue.ToString(), _bptxtCdItem.CodeValue, _bptxtCdItem.CodeName, _curQtItem.DecimalValue, b수량체크여부);
                if (dlg.ShowDialog() == DialogResult.OK && dlg.ReturnDataTable != null && dlg.ReturnDataTable.Rows.Count > 0)
                {
                    _bptxtCdItem.CodeValue = dlg.리턴품목코드;
                    _bptxtCdItem.CodeName = dlg.리턴품목명;
                    _txtStndItem.Text = dlg.리턴규격;
                    _txtUnitMo.Text = dlg.리턴단위;
                    txt_세부규격.Text = dlg.리턴세부규격;
                    txt_재질.Text = dlg.리턴재질;

                    _header.CurrentRow["CD_ITEM"] = dlg.리턴품목코드;
                    _header.CurrentRow["NM_ITEM"] = dlg.리턴품목명;
                    _header.CurrentRow["STND_ITEM"] = dlg.리턴규격;
                    _header.CurrentRow["UNIT_IM"] = dlg.리턴단위;
                    _header.CurrentRow["STND_DETAIL_ITEM"] = dlg.리턴세부규격;
                    _header.CurrentRow["MAT_ITEM"] = dlg.리턴재질;

                    _txtNoSo.Text = dlg.ReturnDataTable.Rows[0]["NO_SO"].ToString();
                    _txtNoLineSo.Text = dlg.ReturnDataTable.Rows[0]["SEQ_SO"].ToString();
                    _header.CurrentRow["NO_SO"] = _txtNoSo.Text;
                    _header.CurrentRow["NO_LINE_SO"] = _txtNoLineSo.Text;

                    DataTable dt = dlg.ReturnDataTable;

                    dt.AcceptChanges();

                    foreach (DataRow dr in dt.Rows)
                    {
                        dr.SetAdded();
                    }


                    _flex03.Binding = dlg.ReturnDataTable;

                    경로유형콤보셋팅();

                    ChangeQtWoFromQtSo();

                    ChangeQtWo();

                    _tab.SelectedIndex = 2;
                }
                #region 기존도움창
                //object dlg = this.MainFrameInterface.LoadHelpWindow("P_SA_SO_SUB1", new object[] { _cboCdPlant.SelectedValue.ToString() });
                //if (((Duzon.Common.Forms.CommonDialog)dlg).ShowDialog() == DialogResult.OK)
                //{
                //    object[] m_Return = (object[])((IHelpWindow)dlg).ReturnValues;
                //    DataRow Head = (DataRow)m_Return[0];
                //    DataRow Detail = (DataRow)m_Return[1];

                //    _txtNoSo.Text = Detail["NO_SO"].ToString();
                //    _txtNoLineSo.Text = Detail["SEQ_SO"].ToString();
                //    _bptxtCdItem.CodeValue = Head["CD_ITEM"].ToString();
                //    _bptxtCdItem.CodeName = Head["NM_ITEM"].ToString();
                //    _txtStndItem.Text = Head["STND_ITEM"].ToString();
                //    _txtUnitMo.Text = Head["UNIT_MO"].ToString();

                //    _header.CurrentRow["NO_SO"] = _txtNoSo.Text;
                //    _header.CurrentRow["NO_LINE_SO"] = _txtNoLineSo.Text;
                //    _header.CurrentRow["CD_ITEM"] = Head["CD_ITEM"];
                //    _header.CurrentRow["NM_ITEM"] = Head["NM_ITEM"];
                //    _header.CurrentRow["STND_ITEM"] = Head["STND_ITEM"];
                //    _header.CurrentRow["UNIT_IM"] = Head["UNIT_IM"];
                //    _header.CurrentRow["UNIT_MO"] = Head["UNIT_MO"];
                //    경로유형콤보셋팅();
                //}
                #endregion
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 생산요청삭제_Click(_btnDel04_Click)

        private void _btnDel04_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flex04.HasNormalRow) return;

                DataRow[] drs = _flex04.DataTable.Select("CHK = 'Y'", "", DataViewRowState.CurrentRows);
                if (drs == null || drs.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                _flex04.Redraw = false;
                foreach (DataRow row in drs)
                    row.Delete();
                _flex04.Redraw = true;
                _flex04.IsDataChanged = true;

                DataRow[] drsSum = _flex04.DataTable.Select("", "", DataViewRowState.CurrentRows);

                decimal dAPPLY = 0m;

                foreach (DataRow dr in drsSum)
                {
                    dAPPLY += _flex04.CDecimal(dr["QT_APPLY"]);
                }

                ChangeQtWoFromQtPrq();

                _curQtItem.DecimalValue = dAPPLY;
                _header.CurrentRow["QT_ITEM"] = _curQtItem.DecimalValue;

                ChangeQtWo();

                Page_DataChanged(null, null);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 수주수량변경에따른작지수량변경구문

        /// <summary>
        /// 수주수량 변경에 따른 작지수량 변경 구문.
        /// </summary>

        private void ChangeQtWoFromQtSo()
        {
            if (!_flex03.HasNormalRow) return;

            decimal dSUM_QT_APPLY = 0m;
            decimal dcurQtItem = 0m;
            object obj = _flex03.DataTable.Compute("SUM(QT_APPLY)", "");
            decimal.TryParse(obj.ToString(), out dSUM_QT_APPLY);

            //재적용 받을 시 수량계산이 잘못되어서~~~ 일단은 sum수량으로 찍어줌 아래문구는 협의 후 주석처리함.
            dcurQtItem = D.GetDecimal(dSUM_QT_APPLY);
            //dcurQtItem = D.GetDecimal(_curQtItem.DecimalValue) + D.GetDecimal(dSUM_QT_APPLY);

            _curQtItem.DecimalValue = dcurQtItem;
            _header.CurrentRow["QT_ITEM"] = dcurQtItem;
        }

        #endregion

        #region -> 생산요청수량변경에따른작지수량변경구문

        /// <summary>
        /// 생산요청수량 변경에 따른 작지수량 변경 구문.
        /// </summary>

        private void ChangeQtWoFromQtPrq()
        {
            if (!_flex04.HasNormalRow) return;

            decimal dSUM_QT_APPLY = 0m;
            object obj = _flex04.DataTable.Compute("SUM(QT_APPLY)", "");
            decimal.TryParse(obj.ToString(), out dSUM_QT_APPLY);

            _curQtItem.DecimalValue = dSUM_QT_APPLY;
            _header.CurrentRow["QT_ITEM"] = dSUM_QT_APPLY;
        }

        #endregion

        #region -> 작업지시상태를 계획('P')에서 확정('O')로 바꾸는 이벤트(_btnConfirm_Click)

        private void _btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                object[] obj = new object[4];
                obj[0] = Global.MainFrame.LoginInfo.CompanyCode;
                obj[1] = D.GetString(_cboCdPlant.SelectedValue);
                obj[2] = _txtNoWo.Text;
                obj[3] = D.GetString(_cboStWo.SelectedValue);
                _biz.getST_WO_update(obj);

                _cboStWo.SelectedValue = "O";
                _header.CurrentRow["ST_WO"] = "O";

                ShowMessage("확정되었습니다.");
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> btn수주추적_Click

        private void btn수주추적_Click(object sender, EventArgs e)
        {
            try
            {
                if (!공장선택여부)
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, _lblCdPlant.Text);
                    _cboCdPlant.Focus();
                    return;
                }
                                
                bool b수량체크여부 = false;
                string strTpRout = "000";
                
                //'002' : MRP, '004' " 소요자재
                if (_cboFgWo.SelectedValue.ToString() == "002" || _cboFgWo.SelectedValue.ToString() == "004")
                {
                    b수량체크여부 = true;
                }

                if (_cboTpRout.SelectedIndex >= 0)
                {
                    DataTable dtTpRout = _cboTpRout.DataSource as DataTable;

                    strTpRout = D.GetString(dtTpRout.Rows[_cboTpRout.SelectedIndex]["FG_TYPE"]);
                    if (strTpRout == null || strTpRout == "") strTpRout = "000";
                }

                P_PR_SA_SO_TRACKING_SUB dlg = new P_PR_SA_SO_TRACKING_SUB(_cboCdPlant.SelectedValue.ToString(), _bptxtCdItem.CodeValue, _bptxtCdItem.CodeName, _curQtItem.DecimalValue, b수량체크여부);

                if (dlg.ShowDialog() == DialogResult.OK && dlg.ReturnDataTable != null && dlg.ReturnDataTable.Rows.Count > 0)
                {
                    _bptxtCdItem.CodeValue = dlg.리턴품목코드;
                    _bptxtCdItem.CodeName = dlg.리턴품목명;
                    _txtStndItem.Text = dlg.리턴규격;
                    _txtUnitMo.Text = dlg.리턴단위;
                    txt_세부규격.Text = dlg.리턴세부규격;
                    txt_재질.Text = dlg.리턴재질;

                    _header.CurrentRow["CD_ITEM"] = dlg.리턴품목코드;
                    _header.CurrentRow["NM_ITEM"] = dlg.리턴품목명;
                    _header.CurrentRow["STND_ITEM"] = dlg.리턴규격;
                    _header.CurrentRow["UNIT_IM"] = dlg.리턴단위;
                    _header.CurrentRow["STND_DETAIL_ITEM"] = dlg.리턴세부규격;
                    _header.CurrentRow["MAT_ITEM"] = dlg.리턴재질;


                    string sNo_So = D.GetString(dlg.ReturnDataTable.Compute("MAX(NO_SO)", ""));
                    string sSeq_So = D.GetString(dlg.ReturnDataTable.Compute("MAX(SEQ_SO)", "NO_SO ='" + sNo_So.ToString() + "'"));

                    _txtNoSo.Text = sNo_So;
                    _txtNoLineSo.Text = sSeq_So;
                    _header.CurrentRow["NO_SO"] = _txtNoSo.Text;
                    _header.CurrentRow["NO_LINE_SO"] = _txtNoLineSo.Text;

                     
                    //20110329 최인성 일단 
                    //프로젝트가 하나만 있는 경우에는 어떤걸로 해야할까요??????
                    //일단은 수주랑 연결되어서 가져가야 하는지 확인해봐야합니다.
                    DataRow[] dr_Pjt = dlg.ReturnDataTable.Select("NO_SO = '" + sNo_So + "'");

                    if (dr_Pjt != null || dr_Pjt.Length > 0)
                    {
                        m_txt_PJT.CodeValue = dr_Pjt[0]["NO_PROJECT"].ToString();
                        m_txt_PJT.CodeName = dr_Pjt[0]["NM_PROJECT"].ToString();
                    }
                    else
                    {
                        m_txt_PJT.CodeValue = string.Empty;
                        m_txt_PJT.CodeName = string.Empty;
                    }

                    _header.CurrentRow["NO_PJT"] = m_txt_PJT.CodeValue;
                    _header.CurrentRow["NM_PJT"] = m_txt_PJT.CodeName;

                    DataTable dt = dlg.ReturnDataTable;
                  
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["NO_WO"] = _txtNoWo.Text;
                        _flex03.DataTable.LoadDataRow(dr.ItemArray, false);
                    }

                    _flex03.BindingAdd(null, "");

                    경로유형콤보셋팅();

                    ChangeQtWoFromQtSo();

                    ChangeQtWo();

                    _tab.SelectedIndex = 2;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> btn생산요청적용_Click

        private void btn생산요청적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!공장선택여부)
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, _lblCdPlant.Text);
                    _cboCdPlant.Focus();
                    return;
                }

                if (_flex04.HasNormalRow)
                {
                    ShowMessage("등록된 생산요청정보가 있습니다. 요청적용을 할 수 없습니다.");
                    _flex04.Focus();
                    return;
                }

                string sMAXCD_PJT = "";

                P_PR_REQ_APPLY_SUB dlg = new P_PR_REQ_APPLY_SUB();

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _bptxtCdItem.CodeValue = dlg.ReturnTable.Rows[0]["CD_ITEM"].ToString();
                    _bptxtCdItem.CodeName = dlg.ReturnTable.Rows[0]["NM_ITEM"].ToString();
                    _txtStndItem.Text = dlg.ReturnTable.Rows[0]["STND_ITEM"].ToString();
                    _txtUnitMo.Text = dlg.ReturnTable.Rows[0]["UNIT_IM"].ToString();
                    txt_세부규격.Text = dlg.ReturnTable.Rows[0]["STND_DETAIL_ITEM"].ToString();
                    txt_재질.Text = dlg.ReturnTable.Rows[0]["MAT_ITEM"].ToString();

                    _header.CurrentRow["CD_ITEM"] = dlg.ReturnTable.Rows[0]["CD_ITEM"].ToString();
                    _header.CurrentRow["NM_ITEM"] = dlg.ReturnTable.Rows[0]["NM_ITEM"].ToString();
                    _header.CurrentRow["STND_ITEM"] = dlg.ReturnTable.Rows[0]["STND_ITEM"].ToString();
                    _header.CurrentRow["UNIT_IM"] = dlg.ReturnTable.Rows[0]["UNIT_IM"].ToString();
                    _header.CurrentRow["STND_DETAIL_ITEM"] = dlg.ReturnTable.Rows[0]["STND_DETAIL_ITEM"].ToString();
                    _header.CurrentRow["MAT_ITEM"] = dlg.ReturnTable.Rows[0]["MAT_ITEM"].ToString();

                    decimal d작업지시수량 = 0m;

                    foreach (DataRow dr in dlg.ReturnDataRowArr)
                    {
                        d작업지시수량 += _flex04.CDecimal(dr["QT_APPLY"]);
                    }

                    _curQtItem.DecimalValue = d작업지시수량;
                    _header.CurrentRow["QT_ITEM"] = d작업지시수량;

                    DataTable dt = dlg.ReturnTable;

                    object obj = dt.Compute("MAX(CD_PJT)", "");
                    sMAXCD_PJT = D.GetString(obj);

                    DataTable dt_Cd_Pjt = _biz.Get_No_Project(new object[] { D.GetString(Global.MainFrame.LoginInfo.CompanyCode), sMAXCD_PJT });

                    if (dt_Cd_Pjt.Rows.Count > 0)
                    {
                        _header.CurrentRow["NO_PJT"] = sMAXCD_PJT;
                        _header.CurrentRow["NM_PJT"] = D.GetString(dt_Cd_Pjt.Rows[0]["NM_PROJECT"]);

                        m_txt_PJT.CodeValue = sMAXCD_PJT;
                        m_txt_PJT.CodeName = D.GetString(dt_Cd_Pjt.Rows[0]["NM_PROJECT"]);
                    }
    
                    _flex04.Binding = dt;

                    if (D.GetString(_flex04["NO_SO"]) != "")
                    {
                        if (ShowMessage("수주번호가 있습니다. 수주추적을 하시겠습니까?", "QY2") == DialogResult.Yes)
                        {
                            object[] obj2 = new object[] {
                                        Global.MainFrame.LoginInfo.CompanyCode, 
                                        D.GetString(_flex04["CD_PLANT"]),
                                        D.GetString(_flex04["NO_SO"]),
                                        D.GetString(_flex04["NO_SO_LINE"])
                                    };

                            DataTable dt_수주추적 = _biz.Get수주추적(obj2);

                            foreach (DataRow dr in dt_수주추적.Rows)
                            {
                                DataRow dr_So_Tracking = _flex03.DataTable.NewRow();

                                dr_So_Tracking["CD_PLANT"] = dr["CD_PLANT"];
                                dr_So_Tracking["CD_ITEM"] = dr["CD_ITEM"];

                                dr_So_Tracking["NM_ITEM"] = dr["NM_ITEM"];

                                dr_So_Tracking["STND_ITEM"] = dr["STND_ITEM"];
                                dr_So_Tracking["UNIT_IM"] = dr["UNIT_IM"];

                                dr_So_Tracking["NO_SO"] = dr["NO_SO"];
                                dr_So_Tracking["SEQ_SO"] = dr["SEQ_SO"];
                                dr_So_Tracking["QT_IM"] = dr["QT_IM"];

                                dr_So_Tracking["QT_GI_IM"] = dr["QT_GI_IM"];
                                dr_So_Tracking["QT_GI_REMAIN"] = dr["QT_GI_REMAIN"];

                                dr_So_Tracking["QT_REMAIN"] = dr["QT_REMAIN"];
                                dr_So_Tracking["QT_APPLY"] = _curQtItem.DecimalValue;
                                dr_So_Tracking["QT_APPLY_ORIGIN"] = dr["QT_APPLY_ORIGIN"];
                                dr_So_Tracking["QT_WO"] = dr["QT_WO"];
                                dr_So_Tracking["QT_WORK"] = dr["QT_WORK"];
                                dr_So_Tracking["DT_DUEDATE"] = dr["DT_DUEDATE"];
                                dr_So_Tracking["CD_PARTNER"] = dr["CD_PARTNER"];

                                dr_So_Tracking["LN_PARTNER"] = dr["LN_PARTNER"];

                                _flex03.DataTable.Rows.Add(dr_So_Tracking);
                            }

                            Object obj_maxNoSo = dt_수주추적.Compute("MAX(NO_SO)", "");
                            DataRow[] dr_NoLineSo = dt_수주추적.Select("NO_SO = '" + D.GetString(obj_maxNoSo) + "' ");

                            _txtNoSo.Text = D.GetString(obj_maxNoSo);
                            _txtNoLineSo.Text = dr_NoLineSo[0]["SEQ_SO"].ToString();

                            _header.CurrentRow["NO_SO"] = _txtNoSo.Text;
                            _header.CurrentRow["NO_LINE_SO"] = _txtNoLineSo.Text;
                        }
                    }

                    경로유형콤보셋팅();

                    ChangeQtWo();

                    
                    //
                    // 2014.05.28 한수전용로직
                    // 생산요청적용시 작업지시등록 포장단위에 CD_PACKUNIT을 넣어준다.
                    //
                    if (MA.ServerKey(true, new string[] { "HANSU" }))
                    {
                        //
                        // 해당 컬럼이 있는지 확인한다.
                        //
                        if (dlg.ReturnTable.Columns.Contains("CD_PACKUNIT"))
                        {
                            bp_PackUnit.CodeValue = D.GetString(dlg.ReturnTable.Rows[0]["CD_PACKUNIT"]);
                            bp_PackUnit.CodeName = D.GetString(dlg.ReturnTable.Rows[0]["NM_PACKUNIT"]);

                            _header.CurrentRow["CD_PACKUNIT"] = dlg.ReturnTable.Rows[0]["CD_PACKUNIT"].ToString();
                            _header.CurrentRow["NM_PACKUNIT"] = dlg.ReturnTable.Rows[0]["NM_PACKUNIT"].ToString();
                        }
                        
                        if (dlg.ReturnTable.Columns.Contains("CD_USERDEF1"))
                        {
                            string s전용품목 = string.Empty;

                            DataTable dt_CD_USERDEF1 = dlg.ReturnTable.DefaultView.ToTable(true, "CD_USERDEF1", "NM_USERDEF1");
                            DataRow[] drs_CD_USERDEF1 = dt_CD_USERDEF1.Select("CD_USERDEF1 <> ''");

                            if (drs_CD_USERDEF1.Length > 0)
                            {
                                foreach (DataRow dr in dt_CD_USERDEF1.Rows)
                                {
                                    if (drs_CD_USERDEF1[0] != dr)
                                        s전용품목 += ", ";

                                    s전용품목 += D.GetString(dr["NM_USERDEF1"]) + "/" + D.GetString(dr["CD_USERDEF1"]);
                                }
                            }

                            txt비고.Text = s전용품목;
                            _header.CurrentRow["DC_RMK"] = s전용품목;
                        }
                    }

                    _tab.SelectedIndex = 3;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> Control_SelectionChangeCommitt
        private void Control_SelectionChangeCommitt(object sender, EventArgs e)
        {
            string sCtrName = ((Control)sender).Name;

            try
            {
                if(sCtrName == _cboTpRout.Name)
                {
                    #region 오더형태 선택에 따른 자동프로세스 활성화 및 비활성화

                    sAutoWorkChk();

                    #endregion
                }
                else if (sCtrName == _cboPatnRout.Name)
                {
                    if (_header.CurrentRow.RowState == DataRowState.Added) return;

                    DialogResult dlg = ShowMessage("@변경시 경로전개 및 소요자재가 재전개 됩니다. 계속하시겠습니까?", new object[] { DD(_lblPantRout.Text) }, "QY2");

                    if (dlg != DialogResult.Yes)
                    {
                        _cboPatnRout.SelectedValue = sPatnRout_Old_Value;

                        //2010-10-31
                        //허정민 과장 요청 공정경로 등록에 있는 오더형태를 바라보도록한다.
                        //공정경로의 오더형태가 없는 경우는 기존처럼 그냥 Default값을 넣고
                        //있는 경우에는 경로등록에 있는 오더형태를 가져가도록한다.
                        DataTable dt = (DataTable)_cboPatnRout.DataSource;

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataRow[] dr_Select = dt.Select("CODE = '" + D.GetString(_cboPatnRout.SelectedValue) + "'");

                            if (dr_Select.Length > 0)
                            {
                                if (D.GetString(dr_Select[0]["TP_WO"]) != string.Empty)
                                    _cboTpRout.SelectedValue = dr_Select[0]["TP_WO"];
                            }
                        }


                        return;
                    }
                    else
                    {
                        //2010-10-31
                        //허정민 과장 요청 공정경로 등록에 있는 오더형태를 바라보도록한다.
                        //공정경로의 오더형태가 없는 경우는 기존처럼 그냥 Default값을 넣고
                        //있는 경우에는 경로등록에 있는 오더형태를 가져가도록한다.
                        DataTable dt = (DataTable)_cboPatnRout.DataSource;

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataRow[] dr_Select = dt.Select("CODE = '" + D.GetString(_cboPatnRout.SelectedValue) + "'");

                            if (dr_Select.Length > 0)
                            {
                                if (D.GetString(dr_Select[0]["TP_WO"]) != string.Empty)
                                    _cboTpRout.SelectedValue = dr_Select[0]["TP_WO"];
                            }
                        }
                    }


                    #region 경로재전개

                    //기존내용을 그냥 쓰려 했으나 상단메뉴가 바뀌어서 저장하시겠습니까를 먼저 물어본다.
                    //그래서 경로전개의 이벤트와는 분리한다.

                    //현재 행을 다 삭제하고 태운다
                    //이걸안하면 이벤트에서 처리한 현재 자료가 존재합니다를 회피하기위함.
                    _flex01.Redraw = false;
                    _flex01.RemoveViewAll();
                    _flex01.Redraw = true;


                    if (!HeaderCheck()) return;

                    if (!경로재전개())
                    {
                        ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                        return;
                    }

                    Page_DataChanged(null, null);

                    #endregion

                    #region 소요량재전개

                    //현재 행을 다 삭제하고 태운다
                    //이걸안하면 이벤트에서 처리한 현재 자료가 존재합니다를 회피하기위함.
                    _flex02.Redraw = false;
                    _flex02.RemoveViewAll();
                    _flex02.Redraw = true;

                    
                    if (!HeaderCheck()) return;

                    if (!소요량재전개())
                    {
                        ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                        return;
                    }

                    Page_DataChanged(null, null);

                    #endregion

                    //계획, 확정 상태에 따른 오더형태와 경로 변경 기능 추가로 인하여 RollBack값이 필요함.
                    //바뀌어진 값이 이제는 다음 RollBack 값이므로 여기서 셋팅
                    //Old값 Roll을 어찌 시킬지 모르겠음.....Cancel도 없구 이전값도 존재 하지 않음.
                    //이벤트도 내가 아는건 다써봤지만 ㅠ.ㅜ 추후 알게되면 수정예정
                    sPatnRout_Old_Value = _cboPatnRout.SelectedValue.ToString();
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region -> 대체품적용 버튼 클릭
        
        private void btn_MATL_REPLACE_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flex02.HasNormalRow)
                    return;

                #region 쏠리테크 전용 대체품적용로직
                
                //
                // 2014.06.05 대체품적용로직 분리
                // 쏠리테크 전용 ATP환경설정에 대체품사용여부
                //
                if (MA.ServerKey(false, new string[] { "SOLIDTECH", "SOLIDTECH1" }))
                {
                    DataRow[] drs_Select = _flex02.DataTable.Select("S = 'Y'");

                    if (drs_Select.Length == 0)
                    {
                        ShowMessage(공통메세지.선택된자료가없습니다);
                        return;
                    }

                    string s_Multi_CD_MATL = Duzon.ERPU.MF.Common.Common.MultiString(drs_Select, "CD_MATL", "|");

                    string[] s_CD_MATL = s_Multi_CD_MATL.Split('|');

                    P_PR_MATL_REPLACE_SUB2 dlg = new P_PR_MATL_REPLACE_SUB2("P_PR_WO_REG_NEW", D.GetString(_cboCdPlant.SelectedValue), s_CD_MATL, _dtpDtRel.Text);

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        DataTable dt = dlg.ReturnValues;

                        if (dt.Rows.Count == 0)
                            return;

                        _flex02.Redraw = false;

                        foreach (DataRow dr in dt.Rows)
                        {
                            DataRow[] drs_Origin_Select = _flex02.DataTable.Select("CD_MATL = '" + D.GetString(dr["CD_ITEM_O"]) + "'");

                            string s_CD_OP_O = string.Empty;
                            string s_CD_WC_O = string.Empty;
                            string s_NM_WC_O = string.Empty;
                            string s_CD_WCOP_O = string.Empty;
                            string s_NM_WCOP_O = string.Empty;
                            string s_CD_MATL_O = string.Empty;
                            decimal d_NO_LINE_O = decimal.Zero;
                            decimal d_QT_NEED_O = decimal.Zero;

                            if (drs_Origin_Select.Length > 1)
                            {
                                ShowMessage("소요자재 [" + D.GetString(drs_Origin_Select[0]["CD_MATL"]) + "]가 중복되었습니다. " + D.GetString(drs_Origin_Select.Length) + "개 입니다.", "EK1");
                                return;
                            }
                            else if (drs_Origin_Select.Length == 0)
                            {
                                s_CD_OP_O = string.Empty;
                                s_CD_WC_O = string.Empty;
                                s_NM_WC_O = string.Empty;
                                s_CD_WCOP_O = string.Empty;
                                s_NM_WCOP_O = string.Empty;
                                s_CD_MATL_O = string.Empty;
                                d_NO_LINE_O = decimal.Zero;
                                d_QT_NEED_O = decimal.Zero;
                            }
                            else
                            {
                                s_CD_OP_O = D.GetString(drs_Origin_Select[0]["CD_OP"]);
                                s_CD_WC_O = D.GetString(drs_Origin_Select[0]["CD_WC"]);
                                s_NM_WC_O = D.GetString(drs_Origin_Select[0]["NM_WC"]);
                                s_CD_WCOP_O = D.GetString(drs_Origin_Select[0]["CD_WCOP"]);
                                s_NM_WCOP_O = D.GetString(drs_Origin_Select[0]["NM_WCOP"]);
                                s_CD_MATL_O = D.GetString(drs_Origin_Select[0]["CD_MATL"]);
                                d_NO_LINE_O = D.GetDecimal(drs_Origin_Select[0]["NO_LINE"]);
                                d_QT_NEED_O = D.GetDecimal(drs_Origin_Select[0]["QT_NEED"]);
                            }

                            int i = 0;

                            _flex02.Rows.Add();
                            _flex02.Row = _flex02.Rows.Count - 1;

                            i = _flex02.Row;

                            _flex02[i, "CD_OP"] = s_CD_OP_O;
                            _flex02[i, "CD_WC"] = s_CD_WC_O;
                            _flex02[i, "NM_WC"] = s_NM_WC_O;
                            _flex02[i, "CD_WCOP"] = s_CD_WCOP_O;
                            _flex02[i, "NM_WCOP"] = s_NM_WCOP_O;
                            _flex02[i, "NO_WO"] = _txtNoWo.Text;
                            _flex02[i, "NO_LINE"] = _flex02.GetMaxValue("NO_LINE") + 1;
                            _flex02[i, "CD_PLANT"] = D.GetString(_cboCdPlant.SelectedValue);
                            _flex02[i, "CD_MATL"] = D.GetString(dr["CD_ITEM"]);
                            _flex02[i, "NM_ITEM"] = D.GetString(dr["NM_ITEM"]);
                            _flex02[i, "STND_ITEM"] = D.GetString(dr["STND_ITEM"]);
                            _flex02[i, "STND_DETAIL_ITEM"] = D.GetString(dr["STND_DETAIL_ITEM"]);
                            _flex02[i, "UNIT_MO"] = D.GetString(dr["UNIT_MO"]);
                            _flex02[i, "NO_MODEL"] = D.GetString(dr["NO_MODEL"]);
                            _flex02[i, "YN_BF"] = D.GetString(dr["FG_BF"]);
                            _flex02[i, "FG_GIR"] = D.GetString(dr["FG_GIR"]);
                            _flex02[i, "FG_SERNO"] = D.GetString(dr["FG_SERNO"]);
                            _flex02[i, "MAT_ITEM"] = D.GetString(dr["MAT_ITEM"]);
                            _flex02[i, "QT_NEED"] = D.GetDecimal(dr["QT_APPLY"]);

                            _flex02[i, "CD_MATL_O"] = s_CD_MATL_O;
                            _flex02[i, "NO_LINE_O"] = d_NO_LINE_O;
                            _flex02[i, "QT_NEED_O"] = d_QT_NEED_O;
                            _flex02[i, "YN_REPLACE"] = "Y";

                            decimal d_ISU_CAN_SO = D.GetDecimal(dr["ISU_CAN_SO"]);

                            CellRange cr = _flex02.GetCellRange(i, "S", i, "NM_ITEM");

                            //
                            // 스타일 이름 지정
                            //
                            cr.Style = _flex02.Styles.Add("STYLE" + D.GetString(_flex02["NO_LINE"]));

                            // 0보다 크면 노란색을 뿌려줌
                            if (d_ISU_CAN_SO > 0)
                                cr.Style.BackColor = System.Drawing.Color.Yellow;
                            else if (d_ISU_CAN_SO == 0)
                                cr.Style.BackColor = System.Drawing.Color.White;
                            else
                                cr.Style.BackColor = System.Drawing.Color.RoyalBlue;

                            _flex02.AddFinished();
                            _flex02.Focus();
                        }

                        _flex02.Redraw = true;
                    }
                }

                #endregion

                #region 나머지 업체 대체품적용로직

                //
                // 2014.06.05
                // 나머지업체는 갤럭시아 대체품로직을 사용하도록 한다.
                //
                
                else
                {
                    P_PR_Z_GALAXIA_MATL_CHNG_SUB dlg = new P_PR_Z_GALAXIA_MATL_CHNG_SUB(D.GetString(_cboCdPlant.SelectedValue), _bptxtCdItem.CodeValue, _bptxtCdItem.CodeName, _txtNoWo.Text);

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        DataRow[] drs = dlg._dataRows;

                        MsgControl.ShowMsg("대체품을 적용하고 있습니다.");

                        _flex02.Redraw = false;

                        foreach (DataRow dr in drs)
                        {
                            DataRow[] drs_Select = _flex02.DataTable.Select("CD_MATL = '" + D.GetString(dr["CD_MATL"]) + "'");

                            // 대체하려는 자재가 중복되어 있을 경우 어떠한 자재를 대체하여야 하는지 모른다.
                            if (drs_Select.Length > 1)
                            {
                                Global.MainFrame.ShowMessage("[" + D.GetString(dr["CD_MATL"]) + "] 품목이 중복되었습니다.", "EK1");
                                continue;
                            }

                            // 대체하려는 자재가 없을 경우도 고려한다.
                            else if (drs_Select.Length == 0)
                            {
                                Global.MainFrame.ShowMessage("[" + D.GetString(dr["CD_MATL"]) + "] 품목이 없습니다.", "EK1");
                                continue;
                            }

                            // 자재를 대체하도록 한다.
                            else
                            {
                                drs_Select[0]["YN_REPLACE"] = "Y";
                                drs_Select[0]["CD_MATL_O"] = D.GetString(dr["CD_MATL"]);
                                drs_Select[0]["CD_MATL"] = D.GetString(dr["CD_MATL_CHNG"]);
                                drs_Select[0]["NM_ITEM"] = D.GetString(dr["NM_MATL_CHNG"]);
                                drs_Select[0]["STND_ITEM"] = D.GetString(dr["STND_MATL_CHNG"]);
                                drs_Select[0]["UNIT_IM"] = D.GetString(dr["UNIT_IM_CHNG"]);
                                drs_Select[0]["UNIT_MO"] = D.GetString(dr["UNIT_MO_CHNG"]);
                                drs_Select[0]["YN_BF"] = D.GetString(dr["YN_BF_CHNG"]);
                                drs_Select[0]["CLS_ITEM"] = D.GetString(dr["CLS_ITEM_CHNG"]);
                                drs_Select[0]["STND_DETAIL_ITEM"] = D.GetString(dr["STND_DETAIL_MATL_CHNG"]);
                                drs_Select[0]["MAT_ITEM"] = D.GetString(dr["MAT_ITEM_MATL"]);

                            }
                        }

                        MsgControl.CloseMsg();

                        _flex02.Redraw = true;
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                MsgControl.CloseMsg();
                _flex02.Redraw = true;
                MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
                _flex02.Redraw = true;
            }
        }

        #endregion

        #region -> 업무공유WBS

        private void btn_업무공유WBS_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsChanged())
                    OnToolBarSaveButtonClicked(null, null);

                if (_txtNoWo.Text == string.Empty)
                    return;

                DataTable dt = _biz.Search_PMS(D.GetString(_cboCdPlant.SelectedValue), _txtNoWo.Text);


                if (dt.Rows.Count == 0)
                    return;


                if (D.GetString(dt.Rows[0]["ID_MEMO"]) == string.Empty)
                {
                    ShowMessage("WS_TR00015");
                    return;
                }

                object[] obj = new object[]
                {
                    "C02",
                    string.Empty,
                    Global.MainFrame.LoginInfo.CompanyCode,
                    D.GetString(dt.Rows[0]["NO_PJT"]),
                    D.GetString(dt.Rows[0]["CD_WBS"]),
                    D.GetString(dt.Rows[0]["NO_SHARE"]),
                    D.GetString(dt.Rows[0]["NO_ISSUE"]),
                    "05"
                };

                P_WS_PM_S_JOBSHARE_SUB1 wnd = new P_WS_PM_S_JOBSHARE_SUB1(this, D.GetString(dt.Rows[0]["ID_MEMO"]), obj);

                wnd.ShowDialog();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 업체전용버튼

        #region 한일도요

        private void btn_작업조건등록_Click(object sender, EventArgs e)
        {
            try
            {
                if (_bptxtCdItem.CodeValue == string.Empty)
                    return;

                P_PR_Z_HANILTOYO_BATCHWORK_SUB dlg = new P_PR_Z_HANILTOYO_BATCHWORK_SUB(D.GetString(_cboCdPlant.SelectedValue), _bptxtCdItem.CodeValue, _bptxtCdItem.CodeName, false);

                dlg.ShowDialog();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region 쏠리드

        private void btn_장납자재전개_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!MsgAndSave(PageActionMode.Search)) return;
                if (!HeaderCheck()) return;

                if (_flex02.HasNormalRow)
                {
                    if (ShowMessage(공통메세지.기존에등록된자료가있습니다삭제후다시작업하시겠습니까) != DialogResult.Yes)
                    {
                        return;
                    }
                }

                _flex02.Redraw = false;

                if (!장납자재전개())
                {
                    ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                    return;
                }

                Page_DataChanged(null, null);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                _flex02.Redraw = true;
            }
        }

        bool 장납자재전개()
        {
            object[] obj = new object[] 
                        { 
                          Global.MainFrame.LoginInfo.CompanyCode, 
                          D.GetString(_cboCdPlant.SelectedValue),
                          _bptxtCdItem.CodeValue,
                          D.GetString(_cboPatnRout.SelectedValue),
                          string.Empty,
                          _dtpDtRel.Text
                        };

            DataTable dt = _biz.Z_SOLIDTECH_Search_Material(obj);

            if (dt == null || dt.Rows.Count == 0) return false;

            _flex02.RemoveViewAll();

            decimal MaxSeq = 0;

            if (s_대체품적용여부 == "Y")
                _tab.SelectedTab = _tpg02;

            foreach (DataRow row in dt.Rows)
            {
                decimal d_QT_NEED = decimal.Zero;
                decimal d_QT_NEED_NET = decimal.Zero;

                d_QT_NEED = _curQtItem.DecimalValue * Convert.ToDecimal(row["QT_NEED"]);
                d_QT_NEED_NET = Unit.수량(DataDictionaryTypes.PR, d_QT_NEED / _curQtItem.DecimalValue);
                
                _flex02.Rows.Add();
                _flex02.Row = _flex02.Rows.Count - 1;

                _flex02["NO_WO"] = _txtNoWo.Text;
                _flex02["NO_LINE"] = ++MaxSeq;
                _flex02["CD_PLANT"] = D.GetString(_cboCdPlant.SelectedValue);
                _flex02["CD_OP"] = row["CD_OP"];
                _flex02["CD_WC"] = row["CD_WC"];
                _flex02["NM_WC"] = row["NM_WC"];
                _flex02["CD_WCOP"] = row["CD_WCOP"];
                _flex02["NM_WCOP"] = row["NM_WCOP"];
                _flex02["CD_MATL"] = row["CD_MATL"];
                _flex02["NM_ITEM"] = row["NM_ITEM"];
                _flex02["STND_ITEM"] = row["STND_ITEM"];
                _flex02["UNIT_MO"] = row["UNIT_MO"];
                _flex02["FG_SERNO"] = row["FG_SERNO"];
                _flex02["QT_NEED"] = d_QT_NEED;
                _flex02["QT_NEED_NET"] = d_QT_NEED_NET;
                _flex02["YN_BF"] = row["YN_BF"];
                _flex02["FG_GIR"] = row["FG_GIR"];
                _flex02["CD_SL_OT"] = row["CD_SL_OT"];
                _flex02["NM_SL_OT"] = row["NM_SL_OT"];
                _flex02["QT_INV"] = row["QT_INV"];
                _flex02["DC_RMK"] = row["DC_RMK"];
                _flex02["CD_TUIP_GROUP"] = string.Empty;
                _flex02["NO_TUIP_SEQ"] = decimal.Zero;
                _flex02["STND_DETAIL_ITEM"] = row["STND_DETAIL_ITEM"];
                _flex02["MAT_ITEM"] = row["MAT_ITEM"];

                if (s_대체품적용여부 == "Y")
                {
                    _flex02["ROW_COLOR"] = row["ROW_COLOR"];

                    //
                    // 그리드에서 특정 행의 색상을 바꿈(시작Row, 시작Column, 마지막Row, 마지막Column)
                    //
                    CellRange cr = _flex02.GetCellRange(_flex02.Row, "S", _flex02.Row, "NM_ITEM");

                    //
                    // 스타일 이름 지정
                    //
                    cr.Style = _flex02.Styles.Add("STYLE" + D.GetString(_flex02.Row));

                    if (D.GetString(_flex02["ROW_COLOR"]) == "Y")
                        cr.Style.BackColor = System.Drawing.Color.Yellow;
                    else if (D.GetString(_flex02["ROW_COLOR"]) == "B")
                        cr.Style.BackColor = System.Drawing.Color.RoyalBlue;
                    else
                        cr.Style.BackColor = System.Drawing.Color.White;
                }

                _flex02.AddFinished();
            }

            _flex02.Row = _flex02.Rows.Count - 1;
            _flex02.IsDataChanged = true;

            return true;
        }

        #endregion

        #endregion

        #endregion

        #region ♣ 저장관련메소드   ♣

        #region -> IsChanged

        protected override bool IsChanged()
        {
            if (base.IsChanged())       // 그리드가 변경되었거나
                return true;

            return 헤더변경여부;        // 헤더가 변경되었거나
        }

        #endregion

        #region -> Verify

        protected override bool Verify()
        {
            if (!BeforeSave()) return false;

            if (!base.SaveData()) return false;

            if (!HeaderCheck()) return false;

            string strTpRout = "000";
            int bYnCount = 0;
            bool bLastRowY = true;

            for (int i = _flex01.Rows.Fixed; i < _flex01.Rows.Count; i++)
            {
                if (_flex01[i, "YN_FINAL"].Equals("Y"))
                {
                    bYnCount++;
                }
                else if (_flex01.Rows.Count - 1 == i)
                {
                    bLastRowY = false;
                }
            }

            if (_flex01.HasNormalRow && bYnCount == 0)
            {
                ShowMessage(공통메세지._이가존재하지않습니다, DD("최종실적") + "Y");
                return false;
            }

            if (_flex01.HasNormalRow && bYnCount > 1)
            {
                ShowMessage(공통메세지._의값이중복되었습니다, DD("최종실적") + "Y");
                return false;
            }

            if (_flex01.HasNormalRow && !bLastRowY)
            {
                ShowMessage("마지막 OP.의 최종실적 값이 N 입니다.");
                return false;
            }
            
            if (_cboTpRout.SelectedIndex >= 0)
            {
                DataTable dtTpRout = _cboTpRout.DataSource as DataTable;

                strTpRout = D.GetString(dtTpRout.Rows[_cboTpRout.SelectedIndex]["FG_TYPE"]);
                if (strTpRout == null || strTpRout == "") strTpRout = "000";
            }
            
            //20110418 최인성 김성호 김광석
            //공장환경설정 프로젝트 사용시 프로젝트 값 필수
            DataSet ds = Pr_ComFunc.Get_Plant_Cfg(new object[] 
                                                    {Global.MainFrame.LoginInfo.CompanyCode,
                                                    _cboCdPlant.SelectedValue == null ? string.Empty : _cboCdPlant.SelectedValue.ToString()});

            if (ds.Tables[1].Rows.Count == 0)
            {
                ShowMessage("해당 공장의 공장환경설정 초기값이 없습니다.", "EK1");
                return false;
            }

            if (ds != null && ds.Tables.Count > 1)
            {
                if (D.GetString(ds.Tables[1].Rows[0]["YN_PROJECT"]) == "Y" && m_txt_PJT.CodeValue == string.Empty)
                {
                    ShowMessage("프로젝트 코드는 필수 항목입니다.");
                    m_txt_PJT.Focus();
                    return false;
                }
            }


            for (int i = _flex02.Rows.Fixed; i < _flex02.Rows.Count; i++)
            {

                //생산유형설정 ( 001:일반, 002:재작업, 003:검사)
                //재작업과 검사일 경우에는 헤더 품목을 그대로 내려준다.  

                //
                // 2014.04.09 김현수. 문종철차장. 바이오랜드
                // 원래는 생산유형이 001:일반 유형일 경우 작업지시품목과 자재가 같을 경우 저장 할 수 없음.
                // 하지만 바이오랜드에서 재작업도 원가에 포함되어야 하는 로직을 원하여
                // 재작업도 원가에 포함시키는 것이 아니라, 재작업의 생산유형을 001:일반 으로 변경하였음.
                // 이렇게 되면 재작업이 001:일반 유형이 되면서 작업지시품목과 자재가 같은 것도 저장되어야 함.
                // 따라서 바이오랜드 서버키 처리하였음.
                //
                if (MA.ServerKey(false, new string[] { "BIOLAND" }))
                    continue;

                if (_flex02[i, "CD_MATL"].Equals(_bptxtCdItem.CodeValue) && sTp_Wo_Prod == "001" && strTpRout == "000")
                {
                    ShowMessage(공통메세지._와_은는같을수없습니다, _flex02.Cols["CD_MATL"].Caption, _bptxtCdItem.Text);
                    _flex02.Select(i, _flex02.Cols["CD_MATL"].Index);
                    _flex02.Focus();
                    return false;
                }
            }

            if (_flex02.HasNormalRow)   //소요자재 정보가 존재하면...
            {
                for (int i = 0; i < _flex02.DataView.Count; i++)
                {
                    DataRow[] dr = _flex01.DataView.ToTable().Select("CD_OP = '" + _flex02.DataView[i]["CD_OP"].ToString() + "' AND CD_WC = '" + _flex02.DataView[i]["CD_WC"].ToString() + "' AND CD_WCOP = '" + _flex02.DataView[i]["CD_WCOP"].ToString() + "' ");
                    if (dr.Length == 0)
                    {
                        ShowMessage("존재하지 않는 공정경로 [@(@), @(@), @(@)] 에 소요자재정보가 존재합니다. 수정해주세요!",
                            new object[] { _flex02.Cols["CD_OP"].Caption, _flex02.DataView[i]["CD_OP"].ToString() ,  _flex02.Cols["CD_WC"].Caption,  _flex02.DataView[i]["CD_WC"].ToString(),  _flex02.Cols["CD_WCOP"].Caption,  _flex02.DataView[i]["CD_WCOP"].ToString()  });
                        return false;
                    }

                }

                
                //20110616 최인성, 김성호, 김형석, 한성욱
                //B/F건인 경우에 출고창고를 지정여부를 체크한다.
                //통제설정에서받아온다. 체크(Y)가 기본이다.
                if (Pr_Global.Tp_Bf_Matl_Sl_YN == "Y")
                {
                    //20110428 최인성 김성호 김형석
                    DataRow[] dr_BF = _flex02.DataView.ToTable().Select("ISNULL(YN_BF, 'N') = 'Y' AND ISNULL(CD_SL_OT, '') = '' ");
                    if (dr_BF.Length > 0)
                    {
                        ShowMessage("@(@) B/F 품목의 출고창고가 없습니다.", new object[] {dr_BF[0]["CD_MATL"].ToString(), dr_BF[0]["NM_ITEM"].ToString() } );
                        return false;
                    }
                }
            }


            //생산요청 적용의 총수량과 작업지시 수량이 다른 경우 메세지 처리
            //한건인 경우는 조절이 가능하지만 여러건인 경우에는 어딜껄 빼줘야할지 모르기 때문이다.
            if (_flex04.HasNormalRow)   //생산요청을 적용받은 경우
            {
                decimal dQt_Apply_Sum = D.GetDecimal(_flex04.DataTable.Compute("SUM(QT_APPLY)", ""));

                if (D.GetDecimal(_curQtItem.DecimalValue) != dQt_Apply_Sum)
                {
                    ShowMessage("작업지시 수량과 생산요청 적용 수량이 다릅니다. 생산요청 Tab의 적용수량을 수정하세요");
                    _tab.SelectedTab = _tab.TabPages[_tab04.Name];
                    return false;
                }

                DataRow[] dr_Chk = _flex04.DataTable.Select("ISNULL(QT_REMAIN, 0) < 0", "", DataViewRowState.CurrentRows);

                if (dr_Chk.Length > 0 && Duzon.ERPU.MF.ComFunc.전용코드("작업지시등록 생산요청적용 수량 초과 허용") == "000")
                {
                    ShowMessage("잔량이 마이너스일 수 없습니다 요청수량을 수정하세요.");
                    return false;
                }
            }

            
            if (!base.Verify()) return false;
            return true;
        }

        #endregion

        #region -> HeaderCheck

        bool HeaderCheck()
        {
            if (!공장선택여부)
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, _lblCdPlant.Text);
                _cboCdPlant.Focus();
                return false;
            }
            if (!작업품목선택여부)
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, _bptxtCdItem.Text);
                _bptxtCdItem.Focus();
                return false;
            }
            if (_dtpDtRel.Text == string.Empty)
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, _lblDtWork.Text);
                _dtpDtRel.Focus();
                return false;
            }
            if (_dtpDtDue.Text == string.Empty)
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, _lblDtWork.Text);
                _dtpDtDue.Focus();
                return false;
            }
            if (!_dtpDtRel.IsValidated)
            {
                ShowMessage(공통메세지.입력형식이올바르지않습니다);
                _dtpDtRel.Focus();
                return false;
            }
            if (!_dtpDtDue.IsValidated)
            {
                ShowMessage(공통메세지.입력형식이올바르지않습니다);
                _dtpDtDue.Focus();
                return false;
            }
            if (D.GetInt(_dtpDtRel.Text) > D.GetInt(_dtpDtDue.Text))
            {
                ShowMessage(공통메세지.시작일자보다종료일자가클수없습니다);
                _dtpDtRel.Focus();
                return false;
            }
            // 오더형태
            if (_cboTpRout.SelectedValue == null || _cboTpRout.SelectedValue.ToString() == string.Empty)
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, _lblTpWo.Text);
                _cboTpRout.Focus();
                return false;
            }
            // 지시수량 0보다 큰지 검사
            if (_curQtItem.DecimalValue <= 0)
            {
                ShowMessage(공통메세지._은_보다커야합니다, _lblQtItem.Text, "0");
                _curQtItem.Focus();
                return false;
            }
            return true;
        }

        #endregion

        #region -> SaveData

        protected override bool SaveData()
        {
            if (!Verify()) return false;

            DataTable dt_Realease = new DataTable();

            object[] obj = null;

            if (_header.JobMode == JobModeEnum.추가후수정)
            {
                string NoWo = "";

                //자동프로세스 처리시 realease 처리 부분

                if (_txtNoWo.Text.Trim() == "")
                    NoWo = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PR", "03");
                else
                    NoWo = _txtNoWo.Text.Trim();

                _header.CurrentRow["NO_WO"] = NoWo;

                for (int i = _flex01.Rows.Fixed; i < _flex01.Rows.Count; i++)
                {
                    _flex01[i, "NO_WO"] = NoWo;
                }

                for (int i = _flex02.Rows.Fixed; i < _flex02.Rows.Count; i++)
                {
                    _flex02[i, "NO_WO"] = NoWo;
                }

                _txtNoWo.Text = NoWo;

                if (LoginInfo.MngLot == "Y" && sLOT번호자동생성 == "100")
                {
                    _txtNoLot.Text = NoWo;
                    _header.CurrentRow["NO_LOT"] = NoWo;
                }

                // 나중에 속도 안나오면 수정할것.....
                if (!_flex01.HasNormalRow)
                {
                    경로재전개();
                }

                if (!_flex02.HasNormalRow)
                {
                    소요량재전개();

                    //20110428 최인성 김성호 김형석
                    //소요량전개후에 B/F의 출고창고 필수 여부 체크
                    if (!Verify()) return false;
                }
            }

            //광진윈텍의 이상한 작업지시 등록 방법 때문에 변경 20100906
            //등록 후 소요자재 등록 하는 경우 때문에 처리함.
            //경로 없으면 자동Release 안되도록 처리함.
            if (_flex01.HasNormalRow)
            {
                //오더형태에 따른 자동처리 프로세스에 대하여 처리건을 확인하여
                //작업지시 Release 전용 테이블을 생성하여 처리하도록 하였음.
                //"",    "사용안함"
                //"005", "작업지시 + 작업REL");
                //"010", "작업지시 + 작업REL(확정)");
                //"015", "작업지시 + 작업REL + 작업실적");
                sAutoWorkChk();//오더형태의 최종값을 체크한다.
                if (sTp_Wo_Auto != "000")
                {
                    dt_Realease = _header.CurrentRow.Table.Clone();

                    dt_Realease.ImportRow(_header.CurrentRow);
                    dt_Realease.Rows[0]["ST_WO"] = "R";
                }
            }

            //20110125 최인성 변경된 오더형태, 경로를 다시 입력시킨다.
            //일단은 아래것들만 추가한다.
            //아래주석을  모든 해더 데이터를 다시 입력받는 것이다.
            _header.CurrentRow["CD_PLANT"]  = _cboCdPlant.SelectedValue;
            _header.CurrentRow["TP_ROUT"]   = _cboTpRout.SelectedValue;
            _header.CurrentRow["PATN_ROUT"] = _cboPatnRout.SelectedValue;


            //20110418 최인성 김형석
            //솔리테크 DC_RMK2, DC_RMK3 시리얼번호 채번 로직
            if (MA.ServerKey(false, new string[] { "SOLIDTECH", "SOLIDTECH1" }))
            {
                if (D.GetString(_cboPatnRout.SelectedValue) == "300")
                {
                    obj = new object[4];
                    obj[0] = Global.MainFrame.LoginInfo.CompanyCode;
                    obj[1] = D.GetString(_cboCdPlant.SelectedValue);
                    obj[2] = D.GetString(_bptxtCdItem.CodeValue);
                    obj[3] = D.GetDecimal(_curQtItem.DecimalValue);

                    DataTable dtSOLISN = _biz.Get_SOLIDTECH_SN(obj);

                    //20110624 최인성 김형석 
                    //품목정보의 오더가 SERIAL인 경우에만 채번됨.
                    //기존에 SERIAL사용이 아닌 품목은 문제 발생으로  추가됨
                    if (dtSOLISN != null && dtSOLISN.Rows.Count != 0)
                    {
                        _header.CurrentRow["DC_RMK2"] = dtSOLISN.Rows[0]["DC_RMK2"];
                        _header.CurrentRow["DC_RMK3"] = dtSOLISN.Rows[0]["DC_RMK3"];
                    }
                }
            }

            //공장환경설정 프로젝트 사용시 프로젝트 값 필수
            DataSet ds = Pr_ComFunc.Get_Plant_Cfg(new object[] 
                                                    {Global.MainFrame.LoginInfo.CompanyCode,
                                                    _cboCdPlant.SelectedValue == null ? string.Empty : _cboCdPlant.SelectedValue.ToString()});

            #region MBOM 공정경로 BOM 체크
            //20110511 최인성 수정
            //_dtPlant이걸로 보면안된다.
            //공장이 바뀐경우에도 읽어야한다.
            //공통에서 공장환경설정을 받아오도록하낟.
            if (D.GetString(ds.Tables[1].Rows[0]["YN_WOBOM_CHK"]) == "Y")
            {
                //object[] obj = new object[] {_cboCdPlant.SelectedValue.ToString(),
                //                             _bptxtCdItem.CodeValue,
                //                             D.GetString(_cboPatnRout.SelectedValue)};

                //DataTable dtBomChk = _biz.BomChk(_cboCdPlant.SelectedValue.ToString(), _bptxtCdItem.CodeValue, D.GetString(_cboPatnRout.SelectedValue));

                obj = new object[]{Global.MainFrame.LoginInfo.CompanyCode,
                                    _cboCdPlant.SelectedValue.ToString(),
                                    _bptxtCdItem.CodeValue,
                                    D.GetString(_cboPatnRout.SelectedValue),
                                    _dtpDtRel.Text
                                   };

                DataTable dtBomChk = _biz.BomChk(obj);
                

                string sBomChk = D.GetString(dtBomChk.Rows[0][0]);

                if (D.GetString(ds.Tables[1].Rows[0]["FG_WOBOM_CHK"]) == "001")
                {
                    if (sBomChk != "SAME")
                    {
                        ShowMessage("MBOM과 공정BOM이 달라 저장할 수 없습니다.");
                        return false;
                    }
                }
                else if (D.GetString(ds.Tables[1].Rows[0]["FG_WOBOM_CHK"]) == "002")
                {
                    if (sBomChk != "SAME")
                    {
                        if (ShowMessage("MBOM과 공정BOM이 다릅니다. 저장하시겠습니까?", "QY2") != DialogResult.Yes)
                            return false;
                    }
                }

            }
            #endregion


            //공정경로 체크
            if (_flex01.DataView.Count == 0)
            {
                ShowMessage("공정경로없이 저장 할 수 없습니다.\n");
                return false;
            }


            obj = new object[]{Global.MainFrame.LoginInfo.CompanyCode,
                                _cboCdPlant.SelectedValue.ToString()
                               };

            ds = Pr_ComFunc.Get_Plant_Cfg(obj);

            //테이블 카운트가2가 아닌 경우 공장환경설정이 없다라고 본다
            if (ds.Tables.Count == 2)
            {
                #region 소요자재가 존재 여부 체크

                if (ds.Tables[1].Rows[0]["YN_WO_BILL_CHK"].ToString().ToUpper() == "Y")
                {
                    //공정경로Bom 존재 여부
                    if (_flex02.DataView.Count == 0)
                    {
                        DialogResult dlg_Msg = DialogResult.Cancel;

                        //저장불가 001: 경고처리 002
                        if (ds.Tables[1].Rows[0]["FG_WO_BILL_CHK"].ToString().ToUpper() == "001")
                        {
                            dlg_Msg = ShowMessage("소요자재가 존재하지 않는 작업지시가 있습니다\n", "QY1");
                        }
                        else
                        {
                            dlg_Msg = ShowMessage("소요자재가 존재하지 않는 작업지시가 있습니다\n"
                                                 + "계속 진행하시겠습니까?", "QY2");
                        }

                        if (dlg_Msg != DialogResult.Yes)
                            return false;
                    }
                }

                #endregion
            }

            DataTable dtH = _header.GetChanges();
            DataTable dt01 = _flex01.GetChanges();
            DataTable dt02 = _flex02.GetChanges();
            DataTable dt03 = _flex03.GetChanges();
            DataTable dt04 = _flex04.GetChanges();
            DataTable dtRelease = dt_Realease.GetChanges();

            //
            // 안전공업 전용 PR_SHIFT 생성 로직
            //
            if (MA.ServerKey(true, new string[] { "ANJUN", "ANJUN2" }))
            {
                if (!Insert_PR_SHIFT(dt01))
                    return false;
            }

            if (dtH == null && dt01 == null && dt02 == null && dt03 == null && dt04 == null)
                return true;

            //
            // 2014.09.15 D20140905010
            // 작업지시수량과 공정별지시수량이 다른 경우 메세지 처리
            //
            if (dt01 != null)
            {
                foreach (DataRow dr in dt01.Rows)
                {
                    if (dr.RowState == DataRowState.Deleted)
                        continue;

                    if (D.GetDecimal(dr["QT_WO"]) != D.GetDecimal(_curQtItem.DecimalValue))
                    {
                        ShowMessage("작업지시수량과 공정별지시수량이 다릅니다. 수정해주세요!");
                        return false;
                    }
                }
            }

            bool bSucess = _biz.Save(dtH, dt01, dt02, dt03, dt04, _header.CurrentRow, dtRelease, b복사여부);
            if (!bSucess) return false;

            sAutoWorkChk();

            _header.AcceptChanges();
            _flex01.AcceptChanges();
            _flex02.AcceptChanges();
            _flex03.AcceptChanges();
            _flex04.AcceptChanges();
            return true;
        }

        #endregion

        #endregion

        #region ♣ 이벤트관련       ♣

        #region -> _header_JobModeChanged

        void _header_JobModeChanged(object sender, FreeBindingArgs e)
        {
            try
            {
                if (_header.JobMode == JobModeEnum.조회후수정)
                {
                    _header.SetControlEnabled(false);
                    _btnNoSo.Enabled = false;
                    btn수주추적.Enabled = false;
                }
                else
                {
                    _header.SetControlEnabled(true);
                    _btnNoSo.Enabled = true;
                    btn수주추적.Enabled = true;
                    _bptxtCdItem.Enabled = true;
                }


                if (_cboFgWo.SelectedValue == null || _cboStWo.SelectedValue == null) return;

                string strTpRout = "000";

                if (_cboTpRout.SelectedIndex >= 0)
                {
                    DataTable dtTpRout = _cboTpRout.DataSource as DataTable;

                    strTpRout = D.GetString(dtTpRout.Rows[_cboTpRout.SelectedIndex]["FG_TYPE"]);
                    if (strTpRout == null || strTpRout == "") strTpRout = "000";
                }      

             
                if (_cboStWo.SelectedValue.ToString() == "P" || _cboStWo.SelectedValue.ToString() == "O")   //작업지시의 상태가 계획이나 확정상태
                {
                    btn수주추적.Enabled = true;

                    //이형준대리 추가 요청 - 쏠리드(쏠리테크)
                    //계획(p), 확정(o)인 경우에는 오더형태과 경로유형 변경가능하도록 수정
                    _cboTpRout.Enabled = true;
                    _cboPatnRout.Enabled = true;

                    //
                    // 안전공업 전용 컨트롤 초기화
                    //
                    if (MA.ServerKey(false, new string[] { "ANJUN", "ANJUN2" }))
                    {
                        dt기준월.Enabled = true;
                    }
                }
                else
                {
                    btn수주추적.Enabled = false;

                    //이형준대리 추가 요청 - 쏠리드(쏠리테크)
                    //계획(p), 확정(o)인 경우에는 오더형태과 경로유형 변경가능하도록 수정
                    _cboTpRout.Enabled = false;
                    _cboPatnRout.Enabled = false;


                    //
                    // 2014.12.15 김현수 이형준(안전공업)
                    // 1. 이미 RELEASE 된 건이라도
                    // 2. 실적이 없고
                    // 3. 오더형태의 FG_AUTO 값이 005(작업지시 + 작업REL) 인 경우
                    // 경로유형 콤보박스를 오픈해 달라는 요청
                    //
                    object[] obj_Process = new object[2];
                    obj_Process[0] = Global.MainFrame.LoginInfo.CompanyCode;
                    obj_Process[1] = D.GetString(_cboTpRout.SelectedValue);
                    DataRow dr_Process = Pr_ComFunc.Get_TpWo(obj_Process);

                    if (D.GetString(_cboStWo.SelectedValue) == "R" && D.GetString(dr_Process["FG_AUTO"]) == "005")
                    {
                        _cboPatnRout.Enabled = true;

                        //
                        // 안전공업 전용 컨트롤 초기화
                        //
                        if (MA.ServerKey(false, new string[] { "ANJUN", "ANJUN2" }))
                        {
                            dt기준월.Enabled = true;
                        }
                    }
                }

                if (_cboStWo.SelectedValue.ToString() != "C")
                {
                    _dtpDtRel.Enabled = true;
                    _dtpDtDue.Enabled = true;
                }

                if (_cboStWo.SelectedValue.ToString() == "P" || _cboStWo.SelectedValue.ToString() == "O")
                {
                    txt비고.Enabled = true;

                    if (strTpRout == "001")
                    {
                        _bptxtNoFr.Enabled = true;
                        _curQtBatch.Enabled = true;
                    }
                }

                if (!오더상태RELEASE여부)
                {
                    _curQtItem.Enabled = true;
                }
                else
                {
                    _curQtItem.Enabled = false;
                }

                //ControlEnabledDisable();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> _header_ControlValueChanged

        void _header_ControlValueChanged(object sender, FreeBindingArgs e)
        {
            try
            {
                Control Ctrl = sender as Control;

                if(Ctrl.Name == _bptxtCdItem.Name)
                {
                    #region 작업품목

                        if (_bptxtCdItem.CodeValue == string.Empty)
                        {
                            _txtStndItem.Text = string.Empty;
                            _txtUnitMo.Text = string.Empty;
                            txt_세부규격.Text = string.Empty;
                            txt_재질.Text = string.Empty;

                            _header.CurrentRow["STND_ITEM"] = string.Empty;
                            _header.CurrentRow["UNIT_MO"] = string.Empty;
                            _header.CurrentRow["UNIT_IM"] = string.Empty;
                            _header.CurrentRow["STND_DETAIL_ITEM"] = string.Empty;
                            _header.CurrentRow["MAT_ITEM"] = string.Empty;
                        }
                        else
                        {
                            DataRow row = _bptxtCdItem.HelpReturn.Rows[0];
                            _txtStndItem.Text = row["STND_ITEM"].ToString();
                            _txtUnitMo.Text = row["UNIT_MO"].ToString();
                            txt_세부규격.Text = D.GetString(row["STND_DETAIL_ITEM"]);
                            txt_재질.Text = D.GetString(row["MAT_ITEM"]);

                            _header.CurrentRow["STND_ITEM"] = row["STND_ITEM"];
                            _header.CurrentRow["UNIT_IM"] = row["UNIT_IM"];
                            _header.CurrentRow["UNIT_MO"] = row["UNIT_MO"];
                            _header.CurrentRow["STND_DETAIL_ITEM"] = row["STND_DETAIL_ITEM"];
                            _header.CurrentRow["MAT_ITEM"] = row["MAT_ITEM"];
                        }
                        
                        경로유형콤보셋팅();

                        //
                        // SK케미칼의 경우
                        // 시작일자 변경시 공장품목의 유효일자를 가져와 납기일 자동계산
                        //
                        if (MA.ServerKey(true, new string[] { "SKCHEMICAL", "SKPLASMA" }))
                        {
                            string sDtLimit = _dtpDtRel.Text;

                            if (D.GetString(_header.CurrentRow["CD_ITEM"]) != string.Empty)
                            {
                                int dDY_VALID = _biz.Z_SKCHEMICAL_Search_DY_VALID(D.GetString(_header.CurrentRow["CD_PLANT"]), D.GetString(_header.CurrentRow["CD_ITEM"]));

                                if (dDY_VALID != decimal.Zero)
                                {
                                    DateTime tmDtLimit = DateTime.ParseExact(_dtpDtRel.Text, "yyyyMMdd", null);
                                    tmDtLimit = tmDtLimit.AddDays(dDY_VALID);

                                    string sYear = D.GetString(tmDtLimit.Year);
                                    string sMonth = D.GetString(tmDtLimit.Month);
                                    string sDay = D.GetString(tmDtLimit.Day);

                                    if (sYear.Length != 4)
                                    {
                                        switch (sYear.Length)
                                        {
                                            case 1:
                                                sYear = "000" + sYear;
                                                break;
                                            case 2:
                                                sYear = "00" + sYear;
                                                break;
                                            case 3:
                                                sYear = "0" + sYear;
                                                break;
                                            default:
                                                sYear = "9999";
                                                break;
                                        }
                                    }

                                    if (sMonth.Length != 2)
                                        sMonth = "0" + sMonth;

                                    if (sDay.Length != 2)
                                        sDay = "0" + sDay;

                                    sDtLimit = sYear + sMonth + sDay;
                                }
                            }

                            _dpDtLimit.Text = sDtLimit;
                            _header.CurrentRow["DT_LIMIT"] = _dpDtLimit.Text;
                        }

                        #endregion
                }
                else if(Ctrl.Name == _curQtItem.Name)
                {
                    #region 지시수량

                    소요수량계산();

                    #endregion
                }
                else if(Ctrl.Name == _cboTpRout.Name)
                {
                    #region 오더형태

                    if (_cboTpRout.SelectedIndex >= 0)
                    {
                        DataTable dtTemp = _cboTpRout.DataSource as DataTable;

                        _header.CurrentRow["TP_ROUT"] = dtTemp.Rows[_cboTpRout.SelectedIndex]["CODE"];
                        _header.CurrentRow["TP_GI"] = dtTemp.Rows[_cboTpRout.SelectedIndex]["TP_GI"];
                        _header.CurrentRow["TP_GR"] = dtTemp.Rows[_cboTpRout.SelectedIndex]["TP_GR"];
                        _header.CurrentRow["FG_TPPURCHASE"] = dtTemp.Rows[_cboTpRout.SelectedIndex]["FG_TPPURCHASE"];

                        //20110512 최인성 김성호 한성욱
                        //시스템통제설정의 오더형태별 기준 공정경로에 따른 기준공정 셋팅
                        //N : 미사용, Y: 사용
                        if (Pr_Global.Tp_Wo_Stad_Rout_YN == "Y")
                        {
                            string sTp_Wo_Std_Rout = _biz.Get_Pr_TpWo_Std_Rout(new object[] { D.GetString(_cboCdPlant.SelectedValue), _bptxtCdItem.CodeValue, 
                                                                                              D.GetString(_cboTpRout.SelectedValue) == null ? string.Empty : D.GetString(_cboTpRout.SelectedValue) });

                            if (sTp_Wo_Std_Rout != string.Empty)
                            {
                                _cboPatnRout.SelectedValue = sTp_Wo_Std_Rout;
                                _header.CurrentRow["PATN_ROUT"] = sTp_Wo_Std_Rout;
                            }
                        }
                    }

                        #endregion
                }
                else if(Ctrl.Name == _bptxtNoFr.Name)
                {
                    #region 배합표

                    if (_bptxtNoFr.CodeValue == string.Empty)
                    {
                        _bptxtCdItem.CodeValue = string.Empty;
                        _bptxtCdItem.CodeName = string.Empty;
                        _txtStndItem.Text = string.Empty;
                        _txtUnitMo.Text = string.Empty;
                        txt_세부규격.Text = string.Empty;
                        txt_재질.Text = string.Empty;

                        _header.CurrentRow["CD_ITEM"] = string.Empty;
                        _header.CurrentRow["NM_ITEM"] = string.Empty;
                        _header.CurrentRow["STND_ITEM"] = string.Empty;
                        _header.CurrentRow["UNIT_IM"] = string.Empty;
                        _header.CurrentRow["UNIT_MO"] = string.Empty;
                        _header.CurrentRow["STND_DETAIL_ITEM"] = string.Empty;
                        _header.CurrentRow["MAT_ITEM"] = string.Empty;
                    }
                    else
                    {
                        object[] obj = new object[] 
                        { 
                            LoginInfo.CompanyCode, 
                            D.GetString(_cboCdPlant.SelectedValue),
                            D.GetString(_bptxtNoFr.CodeValue)
                        };

                        DataTable dt = _biz.Search_Formula_List(obj);

                        if (dt.Rows.Count > 0)
                        {
                            _bptxtCdItem.CodeValue = D.GetString(dt.Rows[0]["CD_ITEM"]);
                            _bptxtCdItem.CodeName = D.GetString(dt.Rows[0]["NM_ITEM"]);
                            _txtStndItem.Text = D.GetString(dt.Rows[0]["STND_ITEM"]);
                            _txtUnitMo.Text = D.GetString(dt.Rows[0]["UNIT_IM"]);

                            txt_세부규격.Text = D.GetString(dt.Rows[0]["STND_DETAIL_ITEM"]);
                            txt_재질.Text = D.GetString(dt.Rows[0]["MAT_ITEM"]);

                            //_curQtBatch.DecimalValue = 1;
                            //_curQtItem.DecimalValue = D.GetDecimal(_curQtBatch.DecimalValue * D.GetDecimal(dt.Rows[0]["QT_BATCH_SIZE"]));
                            txt비고.Text = D.GetString(dt.Rows[0]["DC_RMK"]);

                            _header.CurrentRow["CD_ITEM"] = D.GetString(dt.Rows[0]["CD_ITEM"]);
                            //_header.CurrentRow["QT_BATCH_SIZE"] = 1;
                            //_header.CurrentRow["QT_ITEM"] = _curQtBatch.DecimalValue * D.GetDecimal(dt.Rows[0]["QT_BATCH_SIZE"]);
                            _header.CurrentRow["DC_RMK"] = D.GetString(dt.Rows[0]["DC_RMK"]);
                        }

                        경로유형콤보셋팅();
                    }

                        #endregion
                }
                else if(Ctrl.Name == _cboPatnRout.Name)
                {
                    #region 경로유형
                    //2010-10-31
                    //허정민 과장 요청 공정경로 등록에 있는 오더형태를 바라보도록한다.
                    //공정경로의 오더형태가 없는 경우는 기존처럼 그냥 Default값을 넣고
                    //있는 경우에는 경로등록에 있는 오더형태를 가져가도록한다.
                    DataTable dt = (DataTable)_cboPatnRout.DataSource;

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow[] dr_Select = dt.Select("CODE = '"+ D.GetString(_cboPatnRout.SelectedValue) +"'");

                        if (dr_Select.Length > 0)
                        {
                            if(D.GetString(dr_Select[0]["TP_WO"]) != string.Empty)
                                _cboTpRout.SelectedValue = dr_Select[0]["TP_WO"];
                        }
                    }

                    #endregion
                }
                else if (Ctrl.Name == _dtpDtRel.Name)
                {
                    #region 시작일자

                    if (MA.ServerKey(false, new string[] { "ACADEMY" }))
                    {
                        Page_DataChanged(null, null);
                        return;
                    }

                    if (_flex01.HasNormalRow)
                    {
                        DataTable dt = _flex01.DataTable;

                        foreach (DataRow dr in dt.Rows)
                        {
                            dr["DT_REL"] = _dtpDtRel.Text;
                        }
                    }


                    //
                    // 현대위아IHI 의 경우
                    // 시작일자 변경시 종료일자도 함께 변경해달라는 요청사항.
                    //
                    if (MA.ServerKey(false, new string[] { "HDWIA", "HDWIA2" }))
                    {
                        _dtpDtDue.Text = _dtpDtRel.Text;
                        _header.CurrentRow["DT_DUE"] = _header.CurrentRow["DT_REL"];
                    }

                    //
                    // SK케미칼의 경우
                    // 시작일자 변경시 공장품목의 유효일자를 가져와 납기일 자동계산
                    //
                    if (MA.ServerKey(false, new string[] { "SKCHEMICAL", "SKPLASMA" }))
                    {
                        string sDtLimit = _dtpDtRel.Text;

                        if (D.GetString(_header.CurrentRow["CD_ITEM"]) != string.Empty)
                        {
                            int dDY_VALID = _biz.Z_SKCHEMICAL_Search_DY_VALID(D.GetString(_header.CurrentRow["CD_PLANT"]), D.GetString(_header.CurrentRow["CD_ITEM"]));

                            if (dDY_VALID != decimal.Zero)
                            {
                                DateTime tmDtLimit = DateTime.ParseExact(_dtpDtRel.Text, "yyyyMMdd", null);
                                tmDtLimit = tmDtLimit.AddDays(dDY_VALID);

                                string sYear = D.GetString(tmDtLimit.Year);
                                string sMonth = D.GetString(tmDtLimit.Month);
                                string sDay = D.GetString(tmDtLimit.Day);

                                if (sYear.Length != 4)
                                {
                                    switch (sYear.Length)
                                    {
                                        case 1:
                                            sYear = "000" + sYear;
                                            break;
                                        case 2:
                                            sYear = "00" + sYear;
                                            break;
                                        case 3:
                                            sYear = "0" + sYear;
                                            break;
                                        default :
                                            sYear = "9999";
                                            break;
                                    }
                                }

                                if (sMonth.Length != 2)
                                    sMonth = "0" + sMonth;

                                if (sDay.Length != 2)
                                    sDay = "0" + sDay;

                                sDtLimit = sYear + sMonth + sDay;
                            }
                        }

                        _dpDtLimit.Text = sDtLimit;
                        _header.CurrentRow["DT_LIMIT"] = _dpDtLimit.Text;
                    }

                    #endregion
                }
                else if (Ctrl.Name == _dtpDtDue.Name)
                {
                    #region 종료일자

                    if (MA.ServerKey(false, new string[] { "ACADEMY" }))
                    {
                        Page_DataChanged(null, null);
                        return;
                    }
                    
                    if (_flex01.HasNormalRow)
                    {
                        DataTable dt = _flex01.DataTable;

                        foreach (DataRow dr in dt.Rows)
                        {
                            dr["DT_DUE"] = _dtpDtDue.Text;
                        }
                    }

                    #endregion
                }
              
                Page_DataChanged(null, null);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> Grid_BeforeCodeHelp

        void Grid_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                FlexGrid flex = ((FlexGrid)sender);

                switch (e.Parameter.HelpID)
                {
                    // 공정
                    case HelpID.P_PR_ROUT_SUB:
                        #region 공정
                        if (!공장선택여부)
                        {
                            ShowMessage(공통메세지._은는필수입력항목입니다, _lblCdPlant.Text);
                            e.Cancel = true;
                            return;
                        }
                        if (!작업품목선택여부)
                        {
                            ShowMessage(공통메세지._은는필수입력항목입니다, _bptxtCdItem.Text);
                            e.Cancel = true;
                            return;
                        }
                        e.Parameter.P09_CD_PLANT = _cboCdPlant.SelectedValue.ToString();
                        e.Parameter.P12_CD_ITEM = _bptxtCdItem.CodeValue;
                        //                        e.Parameter.P61_CODE1 = "001";

                        string str경로유형 = _cboPatnRout.SelectedValue == null ? "" : _cboPatnRout.SelectedValue.ToString();
                        e.Parameter.P61_CODE1 = str경로유형;
                        #endregion
                        break;
                    // 작업장
                    case HelpID.P_MA_WC_SUB:
                        #region 작업장
                        if (!공장선택여부)
                        {
                            ShowMessage(공통메세지._은는필수입력항목입니다, _lblCdPlant.Text);
                            e.Cancel = true;
                            return;
                        }
                        e.Parameter.P09_CD_PLANT = _cboCdPlant.SelectedValue.ToString();
                        #endregion
                        break;
                    // 공정코드
                    case HelpID.P_PR_WCOP_SUB:
                        #region 공정
                        if (!공장선택여부)
                        {
                            ShowMessage(공통메세지._은는필수입력항목입니다, _lblCdPlant.Text);
                            e.Cancel = true;
                            return;
                        }
                        if (flex["CD_WC"].ToString() == string.Empty)
                        {
                            ShowMessage(공통메세지._은는필수입력항목입니다, flex.Cols["CD_WC"].Caption);
                            e.Cancel = true;
                            return;
                        }
                        e.Parameter.P09_CD_PLANT = _cboCdPlant.SelectedValue.ToString();
                        e.Parameter.P20_CD_WC = flex["CD_WC"].ToString();
                        #endregion
                        break;
                    // 품목번호
                    //case HelpID.P_MA_PITEM_SUB:
                    //    if (!공장선택여부)
                    //    {
                    //        ShowMessage(공통메세지._은는필수입력항목입니다, _lblCdPlant.Text);
                    //        e.Cancel = true;
                    //        return;
                    //    }
                    //    e.Parameter.P09_CD_PLANT = _cboCdPlant.SelectedValue.ToString();
                    //    e.Parameter.P65_CODE5 = _cboCdPlant.Text.Substring(0, _cboCdPlant.Text.IndexOf("("));
                    //    break;
                    case HelpID.P_USER:                             // 사용자정의도움창
                        #region 사용자정의 도움창
                        string str컨트롤구분자 = "|&&|";
                        string str속성구분자 = "|&|";

                        if (e.Parameter.UserHelpID == "H_MA_PITEM_SUB")     // 공장품목
                        {
                            if (!공장선택여부)
                            {
                                ShowMessage(공통메세지._은는필수입력항목입니다, _lblCdPlant.Text);
                                e.Cancel = true;
                                return;
                            }

                            e.Parameter.P41_CD_FIELD1 = "공장품목";

                            e.Parameter.P09_CD_PLANT = _cboCdPlant.SelectedValue.ToString();
                            e.Parameter.P65_CODE5 = _cboCdPlant.Text.Substring(0, _cboCdPlant.Text.IndexOf("("));

                            //[DropDownComboBox][TextBoxExt] 예제
                            e.Parameter.UserParams = "YN_PHANTOM" + str속성구분자 + "N" + str속성구분자 + "Unabled" + str컨트롤구분자;

                            //[BpCodeTextBox][BpCodeNTextBox] 예제
                            //bpN.UserParams += "GRP_ITEM" + str속성구분자 + "1;AB" + str속성구분자 + "Unabled" + str컨트롤구분자;

                            e.Parameter.P61_CODE1 = str컨트롤구분자;
                            e.Parameter.P62_CODE2 = str속성구분자;
                        }
                        else if (e.Parameter.UserHelpID == "H_PR_ROUT_SUB")     // 공정경로
                        {
                            if (!공장선택여부)
                            {
                                ShowMessage(공통메세지._은는필수입력항목입니다, _lblCdPlant.Text);
                                e.Cancel = true;
                                return;
                            }

                            e.Parameter.P41_CD_FIELD1 = "공정경로";

                            e.Parameter.P09_CD_PLANT = _cboCdPlant.SelectedValue.ToString();
                            e.Parameter.P65_CODE5 = _cboCdPlant.Text.Substring(0, _cboCdPlant.Text.IndexOf("("));

                            if (_flex01.HasNormalRow)
                            {
                                DataRow[] drs = _flex01.DataTable.Select("", "", DataViewRowState.CurrentRows);
                                e.Parameter.MultiRow = drs;
                            }

                            e.Parameter.P61_CODE1 = str컨트롤구분자;
                            e.Parameter.P62_CODE2 = str속성구분자;
                        }
                        else if (e.Parameter.UserHelpID == "H_PR_SFT_SUB")     // SFT 도움창
                        {
                            e.Parameter.P41_CD_FIELD1 = "SFT";

                            e.Parameter.P09_CD_PLANT = D.GetString(_cboCdPlant.SelectedValue);//공장코드
                            e.Parameter.P65_CODE5    = D.GetString(_cboCdPlant.Text);

                            e.Parameter.UserParams += "USE_Y" + str속성구분자 + "Y" + str속성구분자 + "Enabled" + str컨트롤구분자;
                            e.Parameter.UserParams += "USE_N" + str속성구분자 + "N" + str속성구분자 + "Enabled" + str컨트롤구분자;
                            e.Parameter.UserParams += "USE_C" + str속성구분자 + "C" + str속성구분자 + "Enabled" + str컨트롤구분자;

                            e.Parameter.P61_CODE1 = str컨트롤구분자;
                            e.Parameter.P62_CODE2 = str속성구분자;
                        }
                        else if (e.Parameter.UserHelpID == "H_PR_EQUIP_SUB")     // 설비코드 도움창
                        {
                            e.Parameter.P41_CD_FIELD1 = "설비 도움창"; //제목(공백이면 Default)
                            e.Parameter.P09_CD_PLANT  = D.GetString(_cboCdPlant.SelectedValue);
                            e.Parameter.P65_CODE5     = D.GetString(_cboCdPlant.Text);
                        }
                        else if (e.Parameter.UserHelpID == "H_PR_POST_SUB") //위치코드 도움창
                        {
                            e.Parameter.P41_CD_FIELD1 = "위치정보 도움창";
                            e.Parameter.P09_CD_PLANT = D.GetString(_cboCdPlant.SelectedValue);
                            e.Parameter.P65_CODE5 = D.GetString(_cboCdPlant.Text);

                        }
                        #endregion
                        break;
                    case HelpID.P_MA_SL_SUB://창고도움창
                        #region 창고도움창

                        //입고창고, 출하창고 추가로 인한 추가 20100630 - 최인성
                        e.Parameter.P09_CD_PLANT = flex["CD_PLANT"].ToString();

                        if (flex.Cols[e.Col].Name == "CD_SL_OT")
                        {
                            if (e.EditValue == "")
                                flex["QT_INV"] = 0;
                        }

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

        #region -> Grid_AfterCodeHelp

        void Grid_AfterCodeHelp(object sender, AfterCodeHelpEventArgs e)
        {
            try
            {
                FlexGrid flex = (FlexGrid)sender;
                //if (flex.Name != "_flex01")
                //    return;

                //
                if (flex.Name == "_flex01")
                {
                    #region _flex01
                    string YNSubcon = "N";

                    switch (flex.Cols[e.Col].Name)
                    {
                        case "CD_OP":
                        case "CD_WC":
                            YNSubcon = e.Result.Rows[0]["TP_WC"].ToString();
                            if (YNSubcon == null)
                                return;
                            if (YNSubcon == "002")
                                _flex01["YN_SUBCON"] = 'Y';
                            else
                                _flex01["YN_SUBCON"] = 'N';
                            break;
                    }

                    string strCD_OP = _flex01[e.Row, "CD_OP"].ToString();

                    DataRow[] drs = _flex02.DataTable.Select("CD_OP = '" + strCD_OP + "'", "", DataViewRowState.CurrentRows);

                    switch (flex.Cols[e.Col].Name)
                    {
                        case "CD_WC":
                            _flex01["CD_WCOP"] = "";
                            _flex01["NM_WCOP"] = "";

                            if (!_flex02.HasNormalRow) return;

                            string strCD_WC = e.Result.Rows[0]["CD_WC"].ToString();
                            string strNM_WC = e.Result.Rows[0]["NM_WC"].ToString();

                            foreach (DataRow dr in drs)
                            {
                                dr["CD_WC"] = strCD_WC;
                                dr["NM_WC"] = strNM_WC;
                                dr["CD_WCOP"] = "";
                                dr["NM_WCOP"] = "";
                            }
                            break;
                        case "CD_WCOP":
                            if (!_flex02.HasNormalRow) return;

                            string strCD_WCOP = e.Result.Rows[0]["CD_WCOP"].ToString();
                            string strNM_WCOP = e.Result.Rows[0]["NM_OP"].ToString();

                            foreach (DataRow dr in drs)
                            {
                                dr["CD_WCOP"] = strCD_WCOP;
                                dr["NM_WCOP"] = strNM_WCOP;
                            }
                            break;
                    }
                    #endregion
                }
                else if (flex.Name == "_flex02")
                {
                    switch (flex.Cols[e.Col].Name)
                    {
                        case "CD_MATL":

                            foreach (DataRow dr in e.Result.Rows)
                            {
                                flex["CD_MATL"] = dr["CD_ITEM"];
                                flex["NM_ITEM"] = dr["NM_ITEM"];
                                flex["STND_ITEM"] = dr["STND_ITEM"];
                                flex["UNIT_MO"] = dr["UNIT_MO"];

                                //입고창고, 출하창고 추가로 인한 추가 20100630 - 최인성
                                flex["CD_SL_IN"] = dr["CD_SL"];
                                ////flex["CD_SL_OT"] = dr["CD_GISL"];
                                flex["NM_SL_IN"] = dr["NM_SL"];
                                ////flex["NM_SL_OT"] = dr["NM_GISL"];

                                ////if (D.GetString(dr["CD_GISL"]) == "" || D.GetString(dr["CD_GISL"]) == null)
                                ////{
                                DataTable dt_Cd_SL = _biz.Get_Pr_Cd_SL(new object[] { D.GetString(Global.MainFrame.LoginInfo.CompanyCode), 
                                                                                      D.GetString(_cboCdPlant.SelectedValue), 
                                                                                      D.GetString(_flex02["CD_WC"]),
                                                                                      D.GetString(_flex02["CD_WCOP"]) });

                                if (dt_Cd_SL.Rows.Count > 0)
                                {
                                    flex["CD_SL_OT"] = D.GetString(dt_Cd_SL.Rows[0]["CD_SL_OT"]);
                                    flex["NM_SL_OT"] = D.GetString(dt_Cd_SL.Rows[0]["NM_SL_OT"]);
                                }
                                ////}

                                object[] obj = new object[4];
                                obj[0] = Global.MainFrame.LoginInfo.CompanyCode;
                                obj[1] = D.GetString(_cboCdPlant.SelectedValue);
                                obj[2] = D.GetString(flex["CD_MATL"]);
                                obj[3] = D.GetString(flex["CD_SL_OT"]);

                                DataTable dt = _biz.get_QtInv(obj);

                                if (dt == null || dt.Rows == null || dt.Rows.Count == 0)
                                    flex["QT_INV"] = decimal.Zero;
                                else
                                    flex["QT_INV"] = D.GetDecimal(dt.Rows[0]["QT_INV"]);

                                if (s_대체품적용여부 == "Y")
                                {
                                    object[] obj_Replace = new object[]
                                    {
                                        Global.MainFrame.LoginInfo.CompanyCode,
                                        D.GetString(_cboCdPlant.SelectedValue),
                                        _dtpDtRel.Text.Substring(0, 4) + "0101",
                                        _dtpDtRel.Text,
                                        D.GetString(dr["CD_ITEM"]),
                                        string.Empty,
                                        "Y"
                                    };

                                    DataTable dtD = _biz.GetATPSearch(obj_Replace);

                                    DataRow[] drs = dtD.Select("CD_ITEM = '" + D.GetString(dr["CD_ITEM"]) + "'");

                                    // 없으면 Default 값을 뿌려줌
                                    if (drs.Length == 0)
                                        continue;

                                    decimal d_ISU_CAN_SO = D.GetDecimal(drs[0]["ISU_CAN_SO"]);

                                    CellRange cr = _flex02.GetCellRange(e.Row, "S", e.Row, "NM_ITEM");

                                    //
                                    // 스타일 이름 지정
                                    //
                                    cr.Style = _flex02.Styles.Add("STYLE" + D.GetString(_flex02["NO_LINE"]));

                                    // 0보다 크면 노란색을 뿌려줌
                                    if (d_ISU_CAN_SO > 0)
                                        cr.Style.BackColor = System.Drawing.Color.Yellow;
                                    else if (d_ISU_CAN_SO == 0)
                                        cr.Style.BackColor = System.Drawing.Color.White;
                                    else
                                        cr.Style.BackColor = System.Drawing.Color.RoyalBlue;
                                }
                            }

                            break;

                        case "CD_SL_OT":

                            object[] obj2 = new object[4];
                            obj2[0] = Global.MainFrame.LoginInfo.CompanyCode;
                            obj2[1] = D.GetString(_cboCdPlant.SelectedValue);
                            obj2[2] = D.GetString(flex["CD_MATL"]);
                            obj2[3] = e.Result.CodeValue;

                            DataTable dt2 = _biz.get_QtInv(obj2);

                            if (dt2 == null || dt2.Rows == null || dt2.Rows.Count == 0)
                            {
                                flex["QT_INV"] = 0; 
                                return;
                            }

                            flex["QT_INV"] = D.GetDecimal(dt2.Rows[0]["QT_INV"]);

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }

        }

        #endregion

        #region -> Grid_ValidateEdit

        void Grid_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                FlexGrid flex = sender as FlexGrid;
                if (flex == null) return;

                string oldValue = flex.GetData(e.Row, e.Col).ToString();
                string newValue = flex.EditData;
                string str_max_op = string.Empty;
                string str_min_op = string.Empty;

                if (oldValue.ToUpper() == newValue.ToUpper()) return;

                string ColName = flex.Cols[e.Col].Name;

                switch (ColName)
                {
                    case "RT_YIELD" :
                        if (Global.MainFrame.ServerKeyCommon.ToUpper().Contains("GSITM"))        // 파워카본테크놀로지(주)
                        {
                            for (int i = e.Row; i >= _flex01.Rows.Fixed; i--)
                            {
                                if (D.GetDecimal(_flex01[i, "RT_YIELD"]) != 0)
                                {
                                    if (D.GetString(flex["YN_FINAL"]) == "Y")
                                    {
                                        _flex01[i, "QT_WO"] = Unit.수량(DataDictionaryTypes.PR, (_curQtItem.DecimalValue / (D.GetDecimal(_flex01[i, "RT_YIELD"]) / 100)));
                                    }
                                    else
                                    {
                                        _flex01[i, "QT_WO"] = Unit.수량(DataDictionaryTypes.PR, (D.GetDecimal(_flex01[i + 1, "QT_WO"]) / (D.GetDecimal(_flex01[i, "RT_YIELD"]) / 100)));
                                    }
                                }
                            }

                            object obj_max = _flex01.DataTable.Compute("MAX(CD_OP)", "");
                            str_max_op = D.GetString(obj_max);

                            object obj_min = _flex01.DataTable.Compute("MIN(CD_OP)", "");
                            str_min_op = D.GetString(obj_min);

                            DataRow[] dr_max = _flex01.DataTable.Select("CD_OP = '" + str_max_op + "'", "", DataViewRowState.CurrentRows);
                            DataRow[] dr_min = _flex01.DataTable.Select("CD_OP = '" + str_min_op + "'", "", DataViewRowState.CurrentRows);

                            if (dr_max.Length > 0 && dr_min.Length > 0)
                            {
                                _curQtBatch.DecimalValue = Unit.수량(DataDictionaryTypes.PR, (D.GetDecimal(dr_max[0]["QT_WO"]) / D.GetDecimal(dr_min[0]["QT_WO"]) * 100));
                            }
                        }

                        break;
                    case "QT_NEED" :
                        if (ColName != "QT_NEED") return;

                        #region -> 정해진 소수점 이하의 자리수로 입력을 하였을 경우 취소시켜주는 구문

                        if (newValue.Contains("."))
                        {
                            string strFormat = _flex02.Cols["QT_NEED"].Format;

                            string str소수점이하포맷 = strFormat.Substring(strFormat.IndexOf('.') + 1, strFormat.Length - strFormat.IndexOf('.') - 1);

                            string str소수점이하수량 = newValue.Substring(newValue.IndexOf('.') + 1, newValue.Length - newValue.IndexOf('.') - 1);

                            if (str소수점이하수량.Length > str소수점이하포맷.Length)
                            {
                                ShowMessage("소수점의 입력가능한 자리수(@)를 초과하였습니다.",  str소수점이하포맷.Length);
                                e.Cancel = true;
                                return;
                            }
                        }

                        #endregion

                        if (_curQtItem.DecimalValue == 0)
                            _flex02[e.Row, "QT_NEED_NET"] = 0;
                        else
                            _flex02[e.Row, "QT_NEED_NET"] = Convert.ToDecimal(newValue) / _curQtItem.DecimalValue;

                        break;
                    case "FG_GIR" :
                        if (newValue == "N")
                            _flex02["YN_BF"] = "N";
                        break;
                    case "YN_BF" :
                        if (newValue == "Y" && D.GetString(_flex02["FG_GIR"]) == "N")
                        {
                            ShowMessage("불출여부가 Y 이므로 수정하실 수 없습니다.");
                            e.Cancel = true;
                            return;
                        }
                        break;

                    case "CD_USERDEF1":
                    case "CD_USERDEF2":
                    case "CD_USERDEF3":
                        
                        //
                        // 안전공업 전용로직
                        //
                        if (MA.ServerKey(false, new string[] { "ANJUN", "ANJUN2" }))
                        {
                            MakeCode_NO_SFT(ColName, D.GetString(_flex01.EditData));
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

        #region -> _flex03_ValidateEdit

        void _flex03_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                FlexGrid flex = sender as FlexGrid;
                if (flex == null) return;

                string oldValue = flex.GetData(e.Row, e.Col).ToString();
                string newValue = flex.EditData;

                if (oldValue.ToUpper() == newValue.ToUpper())
                    return;

                string ColName = flex.Cols[e.Col].Name;

                if (ColName != "QT_APPLY") return;

                #region -> 정해진 소수점 이하의 자리수로 입력을 하였을 경우 취소시켜주는 구문

                if (newValue.Contains("."))
                {
                    string strFormat = _flex02.Cols["QT_APPLY"].Format;

                    string str소수점이하포맷 = strFormat.Substring(strFormat.IndexOf('.') + 1, strFormat.Length - strFormat.IndexOf('.') - 1);

                    string str소수점이하수량 = newValue.Substring(newValue.IndexOf('.') + 1, newValue.Length - newValue.IndexOf('.') - 1);

                    if (str소수점이하수량.Length > str소수점이하포맷.Length)
                    {
                        ShowMessage("소수점의 입력가능한 자리수(@)를 초과하였습니다.",  str소수점이하포맷.Length );
                        e.Cancel = true;
                        return;
                    }
                }

                #endregion

                //ChangeQtWoFromQtSo();

                decimal dOldQtApply = 0m, dNewQtApply = 0m;

                decimal.TryParse(oldValue, out dOldQtApply);

                decimal.TryParse(newValue, out dNewQtApply);

                decimal dcurQtItem = 0m;
              
                dcurQtItem = D.GetDecimal(_flex03.DataTable.Compute("SUM(QT_APPLY)", "")) - dOldQtApply + dNewQtApply;

                _curQtItem.DecimalValue = dcurQtItem;
                _header.CurrentRow["QT_ITEM"] = dcurQtItem;

                ChangeQtWo();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> _flex04_ValidateEdit

        void _flex04_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                FlexGrid flex = sender as FlexGrid;
                if (flex == null) return;

                string oldValue = flex.GetData(e.Row, e.Col).ToString();
                string newValue = flex.EditData;

                if (oldValue.ToUpper() == newValue.ToUpper())
                    return;

                string ColName = flex.Cols[e.Col].Name;

                if (ColName != "QT_APPLY") return;

                decimal dEdit = D.GetDecimal(_flex04.EditData);                    // 수정금액
                decimal dPreData = D.GetDecimal(_flex04.GetData(e.Row, e.Col));    // 수정전금액

                decimal dACT = D.GetDecimal(flex["QT_ACT"]);                       // 접수수량
                decimal dAPPLY = D.GetDecimal(flex["QT_APPLY"]);                   // 적용수량
                decimal dITEM = D.GetDecimal(flex["QT_ITEM"]);                     // 지시수량
                decimal dWORK = D.GetDecimal(flex["QT_WORK"]);                     // 실적수량
                decimal dREMAIN = D.GetDecimal(flex["QT_REMAIN"]);                 // 잔량
                decimal dAPPLY_OLD = D.GetDecimal(flex["QT_APPLY_OLD"]);           // 원천접수수량
                decimal dQT_PRQ = D.GetDecimal(flex["QT_PRQ"]);           // 생산요청수량

                //로직 다 수정해야 할것 같음....
                //작업지시 등록 후에 가져오는 값과 요청적용 값이 틀리다.
                //그래서 계산식을 서로 다르게 적용하도록함....후더덜;;;
                if (_txtNoWo.Text != string.Empty)
                {


                    if (dACT - dITEM < dEdit - dAPPLY_OLD && Duzon.ERPU.MF.ComFunc.전용코드("작업지시등록 생산요청적용 수량 초과 허용") == "000")
                        {
                            Global.MainFrame.ShowMessage(공통메세지._은_보다작아야합니다, flex.Cols["QT_APPLY"].Caption, flex.Cols["QT_ACT"].Caption + DD("과 ") + flex.Cols["QT_ITEM"].Caption + DD("의 합"));
                            e.Cancel = true;
                            return;
                        }

                        if (dEdit <= 0)
                        {
                            Global.MainFrame.ShowMessage(공통메세지._은_보다커야합니다, flex.Cols["QT_APPLY"].Caption, "0");
                            e.Cancel = true;
                            return;
                        }

                        dREMAIN = dACT - dEdit;
                   



                    //////요청분할인경우
                    //if (Pr_Global.Prq_Reg_YN == "100")
                    //{
                    //    if (dACT - dITEM < dEdit - dAPPLY_OLD)
                    //    {
                    //        Global.MainFrame.ShowMessage(공통메세지._은_보다작아야합니다, flex.Cols["QT_APPLY"].Caption, flex.Cols["QT_ACT"].Caption + DD("과 ") + flex.Cols["QT_ITEM"].Caption + DD("의 합"));
                    //        e.Cancel = true;
                    //        return;
                    //    }

                    //    if (dEdit <= 0)
                    //    {
                    //        Global.MainFrame.ShowMessage(공통메세지._은_보다커야합니다, flex.Cols["QT_APPLY"].Caption, "0");
                    //        e.Cancel = true;
                    //        return;
                    //    }

                    //    dREMAIN = dACT - dEdit;
                    //}
                    //else
                    //{

                    //    if (dEdit > dPreData)
                    //    {
                    //        if (dACT < dITEM + dEdit - dAPPLY_OLD)
                    //        {
                    //            Global.MainFrame.ShowMessage(공통메세지._은_보다작아야합니다, flex.Cols["QT_APPLY"].Caption, flex.Cols["QT_ACT"].Caption + DD("과 ") + flex.Cols["QT_ITEM"].Caption + DD("의 합"));
                    //            e.Cancel = true;
                    //            return;
                    //        }
                    //    }

                    //    dREMAIN = dACT - dITEM - dEdit + dAPPLY_OLD;

                    //}
                }
                else
                {
                    if (dREMAIN < dEdit - dPreData && Duzon.ERPU.MF.ComFunc.전용코드("작업지시등록 생산요청적용 수량 초과 허용") == "000")
                    {
                        Global.MainFrame.ShowMessage(공통메세지._은_보다작아야합니다, flex.Cols["QT_APPLY"].Caption, flex.Cols["QT_ACT"].Caption + DD("과 ") + flex.Cols["QT_ITEM"].Caption + DD("의 합"));
                        e.Cancel = true;
                        return;
                    }

                    dREMAIN = dACT - dITEM - dEdit;
                }

                if (dEdit <= 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지._은_보다커야합니다, flex.Cols["QT_APPLY"].Caption, "0");
                    e.Cancel = true;
                    return;
                }

               
                flex["QT_REMAIN"] = dREMAIN;


                #region -> 정해진 소수점 이하의 자리수로 입력을 하였을 경우 취소시켜주는 구문

                if (newValue.Contains("."))
                {
                    string strFormat = _flex04.Cols["QT_APPLY"].Format;

                    string str소수점이하포맷 = strFormat.Substring(strFormat.IndexOf('.') + 1, strFormat.Length - strFormat.IndexOf('.') - 1);

                    string str소수점이하수량 = newValue.Substring(newValue.IndexOf('.') + 1, newValue.Length - newValue.IndexOf('.') - 1);

                    if (str소수점이하수량.Length > str소수점이하포맷.Length)
                    {
                        ShowMessage("소수점의 입력가능한 자리수(@)를 초과하였습니다.",  str소수점이하포맷.Length );
                        e.Cancel = true;
                        return;
                    }
                }

                #endregion

                ChangeQtWoFromQtPrq();

                decimal dOldQtPrq = 0m, dNewQtPrq = 0m;

                decimal.TryParse(oldValue, out dOldQtPrq);

                decimal.TryParse(newValue, out dNewQtPrq);

                _curQtItem.DecimalValue = _curQtItem.DecimalValue - dOldQtPrq + dNewQtPrq;
                _header.CurrentRow["QT_ITEM"] = _curQtItem.DecimalValue;

                if (_flex03 != null || _flex03.Rows.Count > 2)
                {
                    DataRow[] drs = _flex03.DataTable.Select();
                    foreach (DataRow dr in drs)
                    {
                        dr["QT_APPLY"] = _curQtItem.DecimalValue;
                    }
                }

                ChangeQtWo();
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
            try
            {
                string strTpRout = "000";

                if (IsChanged())
                    this.ToolBarSaveButtonEnabled = true;
                else
                    this.ToolBarSaveButtonEnabled = false;

                _btnDel01.Enabled = _flex01.HasNormalRow;
                _btnDel02.Enabled = _flex02.HasNormalRow;
                _btnDel03.Enabled = _flex03.HasNormalRow;
                _cboCdPlant.Enabled = !_flex01.HasNormalRow;

                if (_txtNoWo.Text != "")
                {
                    _cboCdPlant.Enabled = false;
                }

                if (_cboTpRout.SelectedIndex >= 0)
                {
                    DataTable dtTpRout = _cboTpRout.DataSource as DataTable;

                    strTpRout = D.GetString(dtTpRout.Rows[_cboTpRout.SelectedIndex]["FG_TYPE"]);
                    if (strTpRout == null || strTpRout == "") strTpRout = "000";
                }

                if (strTpRout == "000")
                {
                    if (_flex03.HasNormalRow)
                    {
                        _bptxtCdItem.ReadOnly = ReadOnly.TotalReadOnly;
                        //_curQtItem.ReadOnly = true;
                    }
                    else
                    {
                        _bptxtCdItem.ReadOnly = ReadOnly.None;
                        //_curQtItem.ReadOnly = false;
                    }
                }
                else if (strTpRout == "001")
                {
                    _bptxtCdItem.ReadOnly = ReadOnly.None;
                    //_curQtItem.ReadOnly = false;
                }


                if (_cboFgWo.SelectedValue == null || _cboStWo.SelectedValue == null) return;

                //20110107 한국화장품 요청으로 인한 작업지시구분 구분없이 비릴리즈건은 수정가능 하도록 요청됨 일단.
                //작업지시 구분과 상관없이 릴리즈 상태가 아닌경우에는 수정가능하도록 변경
                //// '002' : MRP, '004' : 자재소요
                //if (_cboFgWo.SelectedValue.ToString() == "002" || _cboFgWo.SelectedValue.ToString() == "004")
                //{
                //    _curQtItem.ReadOnly = true;
                //}

                if (_cboFgWo.SelectedValue.ToString() == "002" || _cboFgWo.SelectedValue.ToString() == "004")  //'002' : MRP, '004' : 자재소요
                {
                    if (_cboStWo.SelectedValue.ToString() == "P" || _cboStWo.SelectedValue.ToString() == "O")   //작업지시의 상태가 계획이나 확정상태
                    {
                        btn수주추적.Enabled = true;
                    }
                }

                if (_cboStWo.SelectedValue.ToString() != "C")
                {
                    _dtpDtRel.Enabled = true;
                    _dtpDtDue.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> Control_Deselecting

        private void Control_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            try
            {
                // 탭페이지 변경되기 전
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> Control_Selected

        private void Control_Selected(object sender, TabControlEventArgs e)
        {
            try
            {
                // 탭페이지 변경후
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> Control_QueryBefore

        private void Control_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                BpCodeNTextBox bpN = sender as BpCodeNTextBox;
                if (bpN == null) return;

                string str컨트롤구분자 = "|&&|";
                string str속성구분자 = "|&|";

                switch (e.HelpID)
                {
                    case HelpID.P_USER:                             // 사용자정의도움창
                        if (bpN.UserHelpID == "H_MA_PITEM_SUB")     // 공장품목
                        {
                            if (!공장선택여부)
                            {
                                ShowMessage(공통메세지._은는필수입력항목입니다, _lblCdPlant.Text);
                                e.QueryCancel = true;
                                return;
                            }

                            e.HelpParam.P41_CD_FIELD1 = "공장품목";

                            e.HelpParam.P09_CD_PLANT = _cboCdPlant.SelectedValue.ToString();
                            e.HelpParam.P65_CODE5 = _cboCdPlant.Text.Substring(0, _cboCdPlant.Text.IndexOf("("));

                            //[DropDownComboBox][TextBoxExt] 예제
                            bpN.UserParams = "YN_PHANTOM" + str속성구분자 + "N" + str속성구분자 + "Unabled" + str컨트롤구분자;
                            bpN.UserParams += "TP_PROC" + str속성구분자 + "M" + str속성구분자 + "Enabled" + str컨트롤구분자;

                            //[BpCodeTextBox][BpCodeNTextBox] 예제
                            //bpN.UserParams += "GRP_ITEM" + str속성구분자 + "1;AB" + str속성구분자 + "Unabled" + str컨트롤구분자;

                            e.HelpParam.P61_CODE1 = str컨트롤구분자;
                            e.HelpParam.P62_CODE2 = str속성구분자;
                        }
                        break;

                    case HelpID.P_MA_PITEM_SUB:
                        if (!공장선택여부)
                        {
                            ShowMessage(공통메세지._은는필수입력항목입니다, _lblCdPlant.Text);
                            e.QueryCancel = true;
                            return;
                        }

                        e.HelpParam.P09_CD_PLANT = D.GetString(_cboCdPlant.SelectedValue);

                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> Control_QueryBefore2

        private void Control_QueryBefore2(object sender, BpQueryArgs e)
        {
            try
            {
                BpCodeTextBox bp = sender as BpCodeTextBox;
                if (bp == null) return;

                switch (e.HelpID)
                {
                    case HelpID.P_USER:                         // 사용자정의도움창
                        if (bp.UserHelpID == "H_SA_PRJ_SUB")   // 프로젝트 도움창
                        {
                            if (!공장선택여부)
                            {
                                ShowMessage(공통메세지._은는필수입력항목입니다, _lblCdPlant.Text);
                                e.QueryCancel = true;
                                return;
                            }

                            e.HelpParam.P41_CD_FIELD1 = "프로젝트";

                            e.HelpParam.P42_CD_FIELD2 = "";

                            e.HelpParam.P07_NO_EMP = "";
                            e.HelpParam.P17_CD_SALEGRP = "";
                            e.HelpParam.P14_CD_PARTNER = "";
                            e.HelpParam.P61_CODE1 = "";
                        }
                        else if (bp.UserHelpID == "H_PR_FORMULA_SUB")
                        {
                            if (!공장선택여부)
                            {
                                ShowMessage(공통메세지._은는필수입력항목입니다, _lblCdPlant.Text);
                                e.QueryCancel = true;
                                return;
                            }

                            e.HelpParam.P09_CD_PLANT = D.GetString(_cboCdPlant.SelectedValue);
                            e.HelpParam.P65_CODE5    = D.GetString(_cboCdPlant.Text);
                            e.HelpParam.P12_CD_ITEM  = D.GetString(_bptxtCdItem.CodeValue);
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

        #region -> Control_QueryAfter

        private void Control_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                switch (e.HelpID)
                {
                    case HelpID.P_USER:                             // 사용자정의도움창
                        if (e.HelpParam.UserHelpID == "H_MA_PITEM_SUB")     // 공장품목
                        {
                            if (LoginInfo.MngLot == "N") return;

                            if (e.HelpReturn.Rows[0]["FG_SERNO"].ToString() == "002")
                                _txtNoLot.ReadOnly = false;
                            else
                            {
                                _txtNoLot.ReadOnly = true;
                                _txtNoLot.Text = "";
                            }   

                            sFg_SerNo = e.HelpReturn.Rows[0]["FG_SERNO"].ToString();
                            txt비고.Text= e.HelpReturn.Rows[0]["DC_BOM_RMK"].ToString();

                            if (MA.ServerKey(false, new string[] { "ANJUN", "ANJUN2" }))
                            {
                                txt최근발행번호.Text = _biz.Z_ANJUN_Search_WO(D.GetString(_cboCdPlant.SelectedValue),
                                                                               D.GetString(e.HelpReturn.Rows[0]["CD_ITEM"]));

                                cur계획량.DecimalValue = _biz.Z_ANJUN_Search_계획량(D.GetString(_cboCdPlant.SelectedValue),
                                                                                     D.GetString(e.HelpReturn.Rows[0]["CD_ITEM"]),
                                                                                     D.GetString(dt기준월.Text));

                                cur발행량.DecimalValue = _biz.Z_ANJUN_Search_발행량(D.GetString(_cboCdPlant.SelectedValue),
                                                                                     D.GetString(e.HelpReturn.Rows[0]["CD_ITEM"]),
                                                                                     D.GetString(dt기준월.Text));
                            }
                        }
                        else if (e.HelpParam.UserHelpID == "H_PR_FORMULA_SUB")
                        {
                            object[] obj = new object[] 
                            { 
                                LoginInfo.CompanyCode, 
                                D.GetString(_cboCdPlant.SelectedValue),
                                D.GetString(_bptxtNoFr.CodeValue)
                            };

                            DataTable dt = _biz.Search_Formula_List(obj);
        
                            if (dt.Rows.Count > 0)
                            {
                                _bptxtCdItem.CodeValue = D.GetString(dt.Rows[0]["CD_ITEM"]);
                                _bptxtCdItem.CodeName = D.GetString(dt.Rows[0]["NM_ITEM"]);
                                _txtStndItem.Text = D.GetString(dt.Rows[0]["STND_ITEM"]);
                                _txtUnitMo.Text   = D.GetString(dt.Rows[0]["UNIT_IM"]);
                                //_curQtBatch.DecimalValue = 1;
                                txt비고.Text = D.GetString(dt.Rows[0]["DC_RMK"]);
                                txt_세부규격.Text = D.GetString(dt.Rows[0]["STND_DETAIL_ITEM"]);
                                txt_재질.Text = D.GetString(dt.Rows[0]["MAT_ITEM"]);
                            }
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

        #region -> Control_CodeChanged

        private void Control_CodeChanged(object sender, EventArgs e)
        {
            try
            {
                if (_bptxtCdItem.CodeValue == string.Empty)
                {
                    _txtStndItem.Text = string.Empty;
                    _txtUnitMo.Text = string.Empty;
                    //_txtFgBf.Text = string.Empty;
                    //_txtFgGir.Text = string.Empty;

                    txt_세부규격.Text = string.Empty;
                    txt_재질.Text = string.Empty;

                    _header.CurrentRow["STND_ITEM"] = string.Empty;
                    _header.CurrentRow["UNIT_IM"] = string.Empty;
                    _header.CurrentRow["UNIT_MO"] = string.Empty;
                    //_header.CurrentRow["FG_BF"] = string.Empty;
                    //_header.CurrentRow["FG_GIR"] = string.Empty;

                    _header.CurrentRow["STND_DETAIL_ITEM"] = string.Empty;
                    _header.CurrentRow["MAT_ITEM"] = string.Empty;

                    //시스템환경설정의 LOT관리 여부에 따라 사용여부 변경
                    if (LoginInfo.MngLot == "Y")
                        _txtNoLot.ReadOnly = false;
                    else
                    {
                        _txtNoLot.ReadOnly = true;
                        _txtNoLot.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 저장된 작업지시의 수량을 변경할때 발생하는 이벤트(_curQtItem_Validated)

        private void _curQtItem_Validated(object sender, EventArgs e)
        {
            try
            {
                if (_flex03.DataTable != null || _flex03.DataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in _flex03.DataTable.Rows)
                    {
                        dr["QT_APPLY"] = _curQtItem.DecimalValue;
                    }
                }
                ChangeQtWo();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 작업지시수량변경구문(ChangeQtWo)

        /// <summary>
        /// 작업지시수량 변경에 따른 경로와 소요량 재전개 구문
        /// </summary>

        private void ChangeQtWo()
        {
            try
            {
                string strTpRout = "000";

                if (_header.CurrentRow.RowState != DataRowState.Added && _flex01.CDecimal(_header.CurrentRow["QT_ITEM"]) != _curQtItem.DecimalValue)
                {
                    if (_cboTpRout.SelectedIndex >= 0)
                    {
                        DataTable dtTpRout = _cboTpRout.DataSource as DataTable;

                        strTpRout = D.GetString(dtTpRout.Rows[_cboTpRout.SelectedIndex]["FG_TYPE"]);
                        if (strTpRout == null || strTpRout == "") strTpRout = "000";
                    }

                    경로재전개();

                    if (strTpRout == "000")
                        소요량재전개();

                    Page_DataChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 작업지시상태의 상태값이 계획('P')이 아닐 경우 확정버튼 비활성화 이벤트(_cboStWo_SelectedValueChanged)

        private void _cboStWo_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (_cboStWo.SelectedValue == null || _cboStWo.SelectedValue.ToString() != "P" )
                    _btnConfirm.Enabled = false;
                else
                    _btnConfirm.Enabled = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region btn_PR_WorkWo_Click (일괄작업지시등록 도움창)
        private void btn_PR_WorkWo_Click(object sender, EventArgs e)
        {
            try
            {
                P_PR_WORKWO_REG_PROCESS_SUB dlg = new P_PR_WORKWO_REG_PROCESS_SUB(_cboCdPlant.SelectedValue.ToString(), _txtNoWo.Text);

                if (dlg.ShowDialog() != DialogResult.OK) return;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region -> Menu_Click
        public void Menu_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem tsMenuItem = sender as ToolStripMenuItem;

                if (tsMenuItem == null) return;

                if (tsMenuItem.Name == DD("작업지시등록-소요자재"))
                {
                    master.ExcelDN_Convert.PopUpSetting("P_PR_WO_REG_NEW");
                }

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region btn_ExcelUpload_Click(엑셀업로드 클릭)
        private void btn_ExcelUpload_Click(object sender, EventArgs e)
        {
            try
            {

                #region 엑셀 파일 READ

                if (!_flex01.HasNormalRow)
                {
                    ShowMessage("경로정보가 등록되어있지 않습니다");
                    return;
                }

                if (b_StateMode) return; //실행중일 경우에 또 다른버튼을 누르면 리턴한다. 
                initState(true);   //실행 불가능한 상태로 만들어준다.

                //MsgControl.ShowMsg(" 엑셀업로드 중입니다. \r\n잠시만 기다려주세요!");  //프로그레스바 호출


                Duzon.Common.Util.Excel excel = new Duzon.Common.Util.Excel();

                OpenFileDialog m_FileDlg = new OpenFileDialog();

                m_FileDlg.Filter = "엑셀 파일 (*.xls)|*.xls";

                if (m_FileDlg.ShowDialog() != DialogResult.OK)
                {
                    initState(false);   //실행 불가능한 상태로 만들어준다.
                    return;
                }

                #endregion

                string FileName = m_FileDlg.FileName;

                DataTable dt = excel.StartLoadExcel(FileName, 0, 2);
                DataTable dt_EXCEL = dt.Clone(); // 엑셀 내용

                foreach (DataRow dr in dt.Rows)
                {
                    dt_EXCEL.Rows.Add(dr.ItemArray);
                }
                if (dt_EXCEL == null || dt_EXCEL.Rows.Count == 0)
                {
                    ShowMessage("엑셀에 자료가 없습니다.");
                    return;
                }

                //키인이벤트가 redraw를 해도 먹기때문에 아래와같이 처리함.
                mDataArea.Enabled = false;
                _flex02.Enabled = false;
                _flex02.Redraw = false;

                DataTable dt_WCOP = null;//공정
                DataTable dt_Pitem = null;//품목
                DataTable dt_SL = null;//창고
                DataTable dt_Temp = null;
                DataRow[] dr_Select = null;

                string sPipe = string.Empty;

                DataTable dt_Group = dt_EXCEL.DefaultView.ToTable(true, "CD_MATL");

                #region 마스터 테이블 조회

                #region 품목

                foreach (DataRow drSplit in dt_Group.Rows)
                {
                    sPipe += drSplit["CD_MATL"].ToString() + "|";
                }

                //2000자리가 넘어가면 에러가 발생하므로 pipe 사용시에는 짤라서 넘기도록한다.
                string[] No_PK_Multi_array = D.StringConvert.GetPipes(sPipe, 200);

                for (int j = 0; j < No_PK_Multi_array.Length; j++)
                {
                    dt_Temp = _biz.Search_MA_Pitem(D.GetString(_cboCdPlant.SelectedValue), No_PK_Multi_array[j]);

                    if (dt_Temp != null && dt_Temp.Rows.Count > 0)
                    {
                        if (dt_Pitem == null)
                            dt_Pitem = dt_Temp.Clone();

                        dt_Pitem.Merge(dt_Temp);
                    }
                }


                if (dt_Pitem == null)
                {
                    ShowMessage("공장마스터 품목에 일치하는 품목이 없습니다.");
                    return;
                }

                #endregion

                #region 작업장

                dt_Group = dt_EXCEL.DefaultView.ToTable(true, new string[] { "CD_WC", "CD_WCOP" });

                foreach (DataRow dr in dt_Group.Rows)
                {
                    dt_Temp = _biz.Search_MA_WCOP(D.GetString(_cboCdPlant.SelectedValue), dr["CD_WC"].ToString());

                    if (dt_Temp != null && dt_Temp.Rows.Count > 0)
                    {
                        dr_Select = dt_Temp.Select(string.Format("CD_WC ='{0}' AND CD_WCOP = '{1}'", dr["CD_WC"].ToString(), dr["CD_WCOP"].ToString()));

                        if (dr_Select.Length == 0)
                        {
                            ShowMessage("작업장(@) 또는 공정(@)이 존재하지 않습니다", new object[] { D.GetString(dr["CD_WC"]), D.GetString(dr["CD_WCOP"]) });
                            return;
                        }

                        if (dt_WCOP == null)
                            dt_WCOP = dt_Temp.Clone();

                        dt_WCOP.Merge(dt_Temp);
                    }
                }




                if (dt_WCOP == null || dt_WCOP.Rows.Count == 0)
                {
                    ShowMessage("작업장또는 공정이 존재하지 않습니다.");
                    return;
                }


                #endregion

                #region 창고

                sPipe = string.Empty;

                if (dt_EXCEL.Columns.Contains("CD_SL_OT"))
                {
                    dt_Group = dt_EXCEL.DefaultView.ToTable(true, "CD_SL_OT");

                    foreach (DataRow drSplit in dt_Group.Rows)
                    {
                        sPipe += drSplit["CD_SL_OT"].ToString() + "|";
                    }

                    //2000자리가 넘어가면 에러가 발생하므로 pipe 사용시에는 짤라서 넘기도록한다.
                    No_PK_Multi_array = D.StringConvert.GetPipes(sPipe, 200);

                    for (int j = 0; j < No_PK_Multi_array.Length; j++)
                    {
                        dt_Temp = _biz.Search_SL(D.GetString(_cboCdPlant.SelectedValue), No_PK_Multi_array[j]);

                        if (dt_Temp != null && dt_Temp.Rows.Count > 0)
                        {
                            if (dt_SL == null)
                                dt_SL = dt_Temp.Clone();

                            dt_SL.Merge(dt_Temp);
                        }
                    }


                    if (dt_SL == null)
                    {
                        ShowMessage("창고마스터와 일치하는 항목이 없습니다");
                        return;
                    }

                }

                #endregion

                #endregion

                //마스터와 엑셀 비교를 위한 변수
                DataRow[] dr_CD_WCOP_Arry = null;
                DataRow[] dr_Pitem_Arry = null;
                DataRow[] dr_SL_Arry = null;

                string sMessge = "작업장\t공정코드\t품목코드\t소요일자\t출고창고\n";

                //메세지 체크를 위한 초기화 작업
                bool bMsgChk = false;
                bool bShowMsg1 = false;
                string msg = string.Empty;
                object[] obj = null;

                foreach (DataRow row in dt_EXCEL.Rows)
                {
                    #region 데이터 유효성 검사 로직

                    msg = string.Empty;

                    //마스터와 비교 및 데이터 검증 부분
                    #region 작업장 공정코드

                    dr_CD_WCOP_Arry = dt_WCOP.Select("CD_WC = '" + D.GetString(row["CD_WC"]) + "' AND CD_WCOP = '" + D.GetString(row["CD_WCOP"]) + "'");

                    msg = row["CD_WC"].ToString() + "\t" + row["CD_WCOP"].ToString() + "\t";

                    if (dr_CD_WCOP_Arry == null || dr_CD_WCOP_Arry.Length == 0)
                    {
                        bMsgChk = true;
                    }

                    #endregion

                    #region 품목
                    dr_Pitem_Arry = dt_Pitem.Select("CD_ITEM = '" + D.GetString(row["CD_MATL"]) + "'");

                    msg += D.GetString(row["CD_MATL"]) + "\t";

                    if (dr_Pitem_Arry == null || dr_Pitem_Arry.Length == 0)
                    {
                        bMsgChk = true;
                    }

                    #endregion

                    #region 소요일자

                    msg += D.GetString(row["DT_NEED"]) + "\t";

                    if (!ComFunc.DateCheck(D.GetString(row["DT_NEED"])))
                    {
                        bMsgChk = true;
                    }
                    #endregion

                    #region 창고
                    if (dt_EXCEL.Columns.Contains("CD_SL_OT"))
                    {
                        dr_SL_Arry = dt_SL.Select("CD_SL = '" + D.GetString(row["CD_SL_OT"]) + "'");

                        msg += D.GetString(row["CD_SL_OT"]) + "\n";

                        if (dr_SL_Arry == null || dr_SL_Arry.Length == 0)
                        {
                            bMsgChk = true;
                        }
                    }

                    #endregion

                    //메세지 처리가 일어난건 다음 품목으로 진행함.
                    if (bMsgChk == true)
                    {
                        //sbErrorList.AppendLine(msg);
                        sMessge += msg;
                        bShowMsg1 = true;
                        bMsgChk = false;
                        continue;
                    }

                    #endregion
                }

                #region 메세지 SHOW
                if (bShowMsg1 == true)
                {
                    ShowDetailMessage
                        (@"엑셀자료에 불일치 및 데이터 오류 항목들이 있습니다. 
                           ▼ 버튼을 눌러서 목록을 확인하세요!",
                        sMessge);
                    return;
                }

                #endregion


                foreach (DataRow row in dt_EXCEL.Rows)
                {

                    dr_CD_WCOP_Arry = dt_WCOP.Select("CD_WC = '" + D.GetString(row["CD_WC"]) + "' AND CD_WCOP = '" + D.GetString(row["CD_WCOP"]) + "'");
                    dr_Pitem_Arry = dt_Pitem.Select("CD_ITEM = '" + D.GetString(row["CD_MATL"]) + "'");
                    dr_SL_Arry = dt_SL.Select("CD_SL = '" + D.GetString(row["CD_SL_OT"]) + "'");


                    //상단행 추가 및 엑셀 데이터를 밀어 넣는 부분   
                    소요자재추가_Click(null, null);

                    if (dt_EXCEL.Columns.Contains("CD_OP"))
                        _flex02["CD_OP"] = row["CD_OP"].ToString();

                    _flex02["CD_WC"] = row["CD_WC"].ToString();
                    _flex02["NM_WC"] = dr_CD_WCOP_Arry[0]["NM_WC"].ToString();

                    _flex02["CD_WCOP"] = dr_CD_WCOP_Arry[0]["CD_WCOP"].ToString();
                    _flex02["NM_WCOP"] = dr_CD_WCOP_Arry[0]["NM_OP"].ToString();

                    _flex02["CD_MATL"] = row["CD_MATL"].ToString();
                    _flex02["NM_ITEM"] = dr_Pitem_Arry[0]["NM_ITEM"].ToString();
                    _flex02["STND_ITEM"] = dr_Pitem_Arry[0]["STND_ITEM"];
                    _flex02["STND_DETAIL_ITEM"] = dr_Pitem_Arry[0]["STND_DETAIL_ITEM"];
                    _flex02["UNIT_MO"] = dr_Pitem_Arry[0]["UNIT_MO"].ToString();
                    _flex02["NO_MODEL"] = dr_Pitem_Arry[0]["NO_MODEL"].ToString();//아카데미
                    _flex02["QT_NEED"] = D.GetDecimal(row["QT_NEED"]);
                    _flex02["DT_NEED"] = row["DT_NEED"].ToString();
                    _flex02["YN_BF"] = row["YN_BF"].ToString();
                    _flex02["FG_GIR"] = row["FG_GIR"].ToString();
                    _flex02["DC_RMK"] = row["DC_RMK"].ToString();

                    if (dt_EXCEL.Columns.Contains("CD_SL_OT"))
                    {
                        _flex02["CD_SL_OT"] = row["CD_SL_OT"].ToString();
                        _flex02["NM_SL_OT"] = dr_SL_Arry[0]["NM_SL"].ToString();

                        //현재고 구함
                        obj = new object[]{Global.MainFrame.LoginInfo.CompanyCode, D.GetString(_cboCdPlant.SelectedValue), D.GetString(row["CD_MATL"]),
                                            D.GetString(row["CD_SL_OT"])
                                           };

                        dt = _biz.get_QtInv(obj);

                        if (dt == null || dt.Rows == null || dt.Rows.Count == 0)
                        {
                            _flex02["QT_INV"] = 0;
                            continue; ;
                        }

                        _flex02["QT_INV"] = D.GetDecimal(dt.Rows[0]["QT_INV"]);

                    }


                    //단위 소요량 계산
                    if (_curQtItem.DecimalValue == 0)
                        _flex02["QT_NEED_NET"] = 0;
                    else
                        _flex02["QT_NEED_NET"] = D.GetDecimal(row["QT_NEED"]) / _curQtItem.DecimalValue;
                }

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                initState(false);   //실행 가능한 상태로 만들어준다.
                mDataArea.Enabled = true;
                _flex02.Enabled = true;
                _flex02.Redraw = true;
            }
        }
        #endregion

        #region -> 공장콤보박스 변경 이벤트
        
        private void _cboCdPlant_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                s_대체품적용여부 = _biz.isReplace(D.GetString(_cboCdPlant.SelectedValue));

                if (s_대체품적용여부 == "Y")
                    btn_MATL_REPLACE.Visible = true;
                else
                    btn_MATL_REPLACE.Visible = false;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 기준월 변경 이벤트(dt기준월_DateChanged)

        private void dt기준월_DateChanged(object sender, EventArgs e)
        {
            cur계획량.DecimalValue = _biz.Z_ANJUN_Search_계획량(D.GetString(_cboCdPlant.SelectedValue),
                                                     D.GetString(_bptxtCdItem.CodeValue),
                                                     D.GetString(dt기준월.Text));

            cur발행량.DecimalValue = _biz.Z_ANJUN_Search_발행량(D.GetString(_cboCdPlant.SelectedValue),
                                                                 D.GetString(_bptxtCdItem.CodeValue),
                                                                 D.GetString(dt기준월.Text));
        }

        #endregion

        #endregion

        #region ♣ 기타메소드       ♣

        #region -> 경로최종실적셋팅

        void 경로최종실적셋팅()
        {
            decimal MaxNoLine = _flex01.GetMaxValue("NO_LINE");

            // 최종실적 셋팅
            for (int i = _flex01.Rows.Fixed; i < _flex01.Rows.Count; i++)
            {
                if (Convert.ToDecimal(_flex01[i, "NO_LINE"]) == MaxNoLine)
                {
                    if (!_flex01[i, "YN_FINAL"].Equals("Y"))
                        _flex01[i, "YN_FINAL"] = "Y";
                }
                else
                {
                    if (!_flex01[i, "YN_FINAL"].Equals("N"))
                        _flex01[i, "YN_FINAL"] = "N";
                }
            }
        }

        #endregion

        #region -> 소요수량계산

        private void 소요수량계산()
        {
            decimal 지시수량 = _curQtItem.DecimalValue;

            for (int i = _flex02.Rows.Fixed; i < _flex02.Rows.Count; i++)
            {
                if (지시수량 == 0)
                    _flex02[i, "QT_NEED_NET"] = 0;
                else
                    _flex02[i, "QT_NEED_NET"] = Convert.ToDecimal(_flex02[i, "QT_NEED"]) / 지시수량;
            }
        }

        #endregion

        #region -> 경로유형 콤보셋팅

        private void 경로유형콤보셋팅()
        {
            DataTable dt = (DataTable)_cboPatnRout.DataSource;
            
            if (_bptxtCdItem.CodeValue == string.Empty)
            {
                dt.Rows.Clear();
                _dtpDtDue.Text = string.Empty;
            }
            else
            {
                // 경로유형 조회해오기
                object[] Return = _biz.경로유형(_cboCdPlant.SelectedValue == null ? string.Empty : _cboCdPlant.SelectedValue.ToString(), _bptxtCdItem.CodeValue, _dtpDtRel.Text);
                dt = (DataTable)Return[0];
                if (_header.JobMode == JobModeEnum.추가후수정)
                    _dtpDtDue.Text = Return[1].ToString();  // 리드타임
            }
            // 리드타임
            if (_header.JobMode == JobModeEnum.추가후수정)
                _header.CurrentRow["DT_DUE"] = _dtpDtDue.Text;

            _cboPatnRout.DataSource = dt;
            _cboPatnRout.DisplayMember = "NAME";
            _cboPatnRout.ValueMember = "CODE";

            //20110512 최인성 김성호 한성욱
            //시스템통제설정의 오더형태별 기준 공정경로에 따른 기준공정 셋팅
            //N : 미사용, Y: 사용
            if (Pr_Global.Tp_Wo_Stad_Rout_YN == "Y")
            {
                string sTp_Wo_Std_Rout = _biz.Get_Pr_TpWo_Std_Rout(new object[] { _cboCdPlant.SelectedValue.ToString(), _bptxtCdItem.CodeValue, 
                                                                                  _cboTpRout.SelectedValue == null ? string.Empty : _cboTpRout.SelectedValue.ToString() });

                if (sTp_Wo_Std_Rout != string.Empty)
                    _cboPatnRout.SelectedValue = sTp_Wo_Std_Rout;
            }
            else
            {
                //2010-10-31
                //허정민 과장 요청 공정경로 등록에 있는 오더형태를 바라보도록한다.
                //공정경로의 오더형태가 없는 경우는 기존처럼 그냥 Default값을 넣고
                //있는 경우에는 경로등록에 있는 오더형태를 가져가도록한다.
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (D.GetString(dt.Rows[0]["TP_WO"]) != string.Empty)
                        _cboTpRout.SelectedValue = dt.Rows[0]["TP_WO"].ToString();
                }
            }


            if (_cboPatnRout.Items.Count == 0)
            {
                _flex02.SetCodeHelpCol("CD_OP", "H_PR_ROUT_SUB", ShowHelpEnum.Always, new string[] { "CD_OP", "CD_WC", "NM_WC", "CD_WCOP", "NM_WCOP" }, new string[] { "CD_OP", "CD_WC", "NM_WC", "CD_WCOP", "NM_WCOP" });
            }
            else
            {
                _flex02.SetCodeHelpCol("CD_OP", HelpID.P_PR_ROUT_SUB, ShowHelpEnum.Always, new string[] { "CD_OP", "CD_WC", "NM_WC", "CD_WCOP", "NM_WCOP" }, new string[] { "CD_OP", "CD_WC", "NM_WC", "CD_WCOP", "NM_OP" }, new string[] { "CD_OP", "CD_WC", "NM_WC", "CD_WCOP", "NM_WCOP" }, ResultMode.SlowMode);
            }

            if (_header.JobMode == JobModeEnum.추가후수정)
            {
                if (dt.Rows.Count > 0)
                {
                    //_cboPatnRout.SelectedIndex = 0;
                    _header.CurrentRow["PATN_ROUT"] = _cboPatnRout.SelectedValue;
                }
            }
        }

        #endregion

        #region -> ShowMessage

        DialogResult ShowMessage(메세지 msg, params string[] param)
        {
            DialogResult result = DialogResult.OK;
            switch (msg)
            {
                case 메세지.확정된내용은수정하실수없습니다:
                    ShowMessage("PR_M200015");
                    break;
                case 메세지.RELEASE를먼저취소하세요:
                    ShowMessage("PR_M100068");
                    break;
            }
            return result;
        }

        #endregion

        #region sAutoWorkChk(일괄작업지시 버튼 체크)
        /// <summary>
        /// 오더형태에 따른 자동프로세스 확인
        /// 일괄작업지시 등록 버튼 활성화 및 비활성화
        /// </summary>
        /// <param name="obj"></param>
        private void sAutoWorkChk()
        {
            try
            {
                ////저장 전에는 안보여야 함으로 숨겼다.
                //if (_txtNoWo.Text == "")
                //{
                //    btn_PR_WorkWo.Visible = false;
                //    btn_PR_WorkWo.Enabled = false;
                //}

                #region 오더형태를 읽어와서 제어하기
                object[] obj_Process = new object[2];
                obj_Process[0] = Global.MainFrame.LoginInfo.CompanyCode;
                obj_Process[1] = D.GetString(_cboTpRout.SelectedValue);
                DataRow dr_Process = Pr_ComFunc.Get_TpWo(obj_Process);

                if (dr_Process != null)
                {
                    // 자동프로세스
                    if (dr_Process["FG_AUTO"] != System.DBNull.Value && dr_Process["FG_AUTO"].ToString().Trim() != String.Empty)
                        sTp_Wo_Auto = dr_Process["FG_AUTO"].ToString();

                    // 생산유형설정 ( 001:일반, 002:재작업, 003:검사)
                    if (dr_Process["FG_PROD"] != System.DBNull.Value && dr_Process["FG_PROD"].ToString().Trim() != String.Empty)
                        sTp_Wo_Prod = dr_Process["FG_PROD"].ToString();
                }
                #endregion

                // 자동프로세스
                //"000", "사용안함"
                //"005", "작업지시 + 작업REL");
                //"010", "작업지시 + 작업REL(확정)");
                //"015", "작업지시 + 작업REL + 작업실적");
                if (sTp_Wo_Auto == "015" && _txtNoWo.Text != "")
                {
                    btn_PR_WorkWo.Visible = true;
                    btn_PR_WorkWo.Enabled = true;
                }
                else
                {
                    btn_PR_WorkWo.Visible = false;
                    btn_PR_WorkWo.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region Batch 수량
        private void _curQtBatch_Validated(object sender, EventArgs e)
        {
            object[] obj = new object[] 
                           { LoginInfo.CompanyCode, 
                                 D.GetString(_cboCdPlant.SelectedValue),
                             D.GetString(_bptxtNoFr.CodeValue) };

            DataTable dt = _biz.Search_Formula_List(obj);

            if (dt.Rows.Count > 0)
            {
                _curQtItem.DecimalValue = D.GetDecimal(_curQtBatch.DecimalValue * D.GetDecimal(dt.Rows[0]["QT_BATCH_SIZE"]));
                _header.CurrentRow["QT_ITEM"] = _curQtBatch.DecimalValue * D.GetDecimal(dt.Rows[0]["QT_BATCH_SIZE"]);
            }
        }
        #endregion

        #region -> 사용자정의컬럼 세팅

        private void UserDefinedSetting_PR_WO_ROUT()
        {
            try
            {
                //
                // 사용자정의 콤보박스
                //
                DataTable dt = MA.GetCode("PR_0000093");

                if (dt.Rows.Count != 0)
                {
                    for(int i = 1; i <= 3; i++)
                    {
                        DataTable dt_CD_USERDEF = MA.GetCode("PR_Z00001" + D.GetString(i + 1), true);

                        if (dt_CD_USERDEF.Rows.Count > 1)
                        {
                            string colName = "CD_USERDEF" + D.GetString(i);
                            string colCaption = D.GetString(dt.Rows[i - 1]["NAME"]);

                            _flex01.SetCol(colName, colCaption, 80, true);

                            _flex01.SetDataMap(colName, dt_CD_USERDEF.Copy(), "CODE", "NAME");
                        }
                    }
                }

                //
                // 사용자정의 숫자컬럼
                // 코드관리값은 공정경로등록과 동일하게 가져간다.
                //
                DataTable dt_NUM_USERDEF = MA.GetCode("PR_0000107");

                for (int i = 1; i <= 3; i++)
                {
                    string colName = "NUM_USERDEF" + i.ToString();

                    if (dt_NUM_USERDEF.Rows.Count < i)
                    {
                        _flex01.Cols[colName].Visible = false;
                        continue;
                    }

                    _flex01.Cols[colName].Caption = D.GetString(dt_NUM_USERDEF.Rows[i - 1]["NAME"]);
                    _flex01.Cols[colName].Visible = true;
                }

                //
                // 사용자정의 텍스트컬럼
                // 코드관리값은 공정경로등록과 동일하게 가져간다.
                //
                DataTable dt_TXT_USERDEF = MA.GetCode("PR_0000108");

                for (int i = 1; i <= 3; i++)
                {
                    string colName = "TXT_USERDEF" + i.ToString();

                    if (dt_TXT_USERDEF.Rows.Count < i)
                    {
                        _flex01.Cols[colName].Visible = false;
                        continue;
                    }

                    _flex01.Cols[colName].Caption = D.GetString(dt_TXT_USERDEF.Rows[i - 1]["NAME"]);
                    _flex01.Cols[colName].Visible = true;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region ♣ 속성             ♣

        bool 공장선택여부
        {
            get
            {
                if (_cboCdPlant.SelectedValue == null || _cboCdPlant.SelectedValue.ToString() == string.Empty)
                    return false;
                return true;
            }
        }

        bool 작업품목선택여부
        {
            get
            {
                if (_bptxtCdItem.CodeValue == string.Empty)
                    return false;
                return true;
            }
        }

        bool 작업지시수량입력여부
        {
            get
            {
                if (_curQtItem.DecimalValue == 0)
                    return false;
                return true;
            }
        }

        private bool 헤더변경여부
        {
            get
            {
                bool bChange = _header.GetChanges() != null && 작업품목선택여부 ? true : false;
                return bChange;
            }

        }

        private string 경로유형MS여부
        {
            get
            {
                DataTable dt = (DataTable)_cboTpRout.DataSource;
                if (_cboTpRout.SelectedIndex >= 0)
                    return dt.Rows[_cboTpRout.SelectedIndex]["YN_WORK"].ToString();
                else
                    return "N";
            }
        }

        private bool 오더상태RELEASE여부
        {
            get
            {
                string Release = _cboStWo.SelectedValue == null ? string.Empty : _cboStWo.SelectedValue.ToString();
                if (Release == "R")
                    return true;
                return false;
            }
        }

        private bool 표준경로유형여부
        {
            get
            {
                if (_cboPatnRout.SelectedValue != null && _cboPatnRout.SelectedValue.Equals("001"))
                    return true;
                return false;
            }
        }

        private bool 추가모드여부
        {
            get
            {
                if (_header.JobMode == JobModeEnum.추가후수정 && _header.CurrentRow["NO_WO"].ToString() == "")
                    return true;
                return false;
            }
        }

        #endregion

        #region ♣ 업체전용         ♣

        #region -> 안전공업 전용 SFT코드 생성 로직

        private void MakeCode_NO_SFT(string colName, string newValue)
        {
            try
            {
                string CD_USERDEF1 = string.Empty;
                string CD_USERDEF2 = string.Empty;
                string CD_USERDEF3 = string.Empty;

                switch (colName)
                {
                    case "CD_USERDEF1":
                        CD_USERDEF1 = newValue;
                        CD_USERDEF2 = D.GetString(_flex01[_flex01.RowSel, "CD_USERDEF2"]);
                        CD_USERDEF3 = D.GetString(_flex01[_flex01.RowSel, "CD_USERDEF3"]);
                        break;

                    case "CD_USERDEF2":
                        CD_USERDEF1 = D.GetString(_flex01[_flex01.RowSel, "CD_USERDEF1"]);
                        CD_USERDEF2 = newValue;
                        CD_USERDEF3 = D.GetString(_flex01[_flex01.RowSel, "CD_USERDEF3"]);
                        break;

                    case "CD_USERDEF3":
                        CD_USERDEF1 = D.GetString(_flex01[_flex01.RowSel, "CD_USERDEF1"]);
                        CD_USERDEF2 = D.GetString(_flex01[_flex01.RowSel, "CD_USERDEF2"]);
                        CD_USERDEF3 = newValue;
                        break;
                }

                if (CD_USERDEF1 != string.Empty && CD_USERDEF2 != string.Empty && CD_USERDEF3 != string.Empty)
                {
                    DataTable dt_SFT = _biz.Get_PR_SHIFT(Global.MainFrame.LoginInfo.CompanyCode,
                                                         D.GetString(_cboCdPlant.SelectedValue),
                                                         CD_USERDEF1 + CD_USERDEF2 + CD_USERDEF3);

                    if (dt_SFT.Rows.Count != 0)
                    {
                        _flex01[_flex01.RowSel, "NO_SFT"] = D.GetString(dt_SFT.Rows[0]["NO_SFT"]);
                        _flex01[_flex01.RowSel, "NM_SFT"] = D.GetString(dt_SFT.Rows[0]["NM_SFT"]);
                    }
                    else
                    {
                        _flex01[_flex01.RowSel, "NO_SFT"] = string.Empty;
                        _flex01[_flex01.RowSel, "NM_SFT"] = string.Empty;
                    }
                }
                else
                {
                    _flex01[_flex01.RowSel, "NO_SFT"] = string.Empty;
                    _flex01[_flex01.RowSel, "NM_SFT"] = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 안전공업 전용 PR_SHIFT 생성 로직

        private bool Insert_PR_SHIFT(DataTable dt01)
        {
            if (dt01 == null)
                return true;

            foreach (DataRow dr in dt01.Rows)
            {
                if (dr.RowState == DataRowState.Deleted)
                    continue;

                //
                // CD_USERDEF1, CD_USERDEF2, CD_USERDEF3 에는 값이 있으나
                // NO_SFT에는 값이 없을 경우
                // PR_SHIFT에 데이터를 만들어주고,
                // 작업지시공정경로 테이블에 NO_SFT, NM_SFT를 넣어준다.
                //
                if (D.GetString(dr["NO_SFT"]) == string.Empty &&
                    D.GetString(dr["CD_USERDEF1"]) != string.Empty &&
                    D.GetString(dr["CD_USERDEF2"]) != string.Empty &&
                    D.GetString(dr["CD_USERDEF3"]) != string.Empty)
                {
                    string s_NO_SFT = D.GetString(dr["CD_USERDEF1"]) + D.GetString(dr["CD_USERDEF2"]) + D.GetString(dr["CD_USERDEF3"]);

                    if (s_NO_SFT.Length > 3)
                    {
                        ShowMessage("조합된 SFT코드가 3자리를 초과할 수 없습니다.", "EK1");
                        return false;
                    }

                    string s_CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
                    string s_CD_PLANT = D.GetString(_cboCdPlant.SelectedValue);

                    //
                    // PR_SHIFT에 NO_SFT가 있는지 체크한다.
                    //
                    DataTable dt_PR_SHIFT = _biz.Get_PR_SHIFT(s_CD_COMPANY, s_CD_PLANT, s_NO_SFT);

                    //
                    // NO_SFT가 없으면 PR_SHIFT에 INSERT 해주고,
                    // DataTable의 NO_SFT, NM_SFT를 바꿔준다.
                    //
                    if (dt_PR_SHIFT.Rows.Count == 0)
                    {
                        string s_NM_SFT = string.Empty;

                        for (int i = 1; i <= 3; i++)
                        {
                            DataTable dt_CD_USERDEF = MA.GetCode("PR_Z00001" + D.GetString(i + 1), true);

                            string s_Column = "CD_USERDEF" + D.GetString(i);

                            DataRow[] drs = dt_CD_USERDEF.Select("CODE = '" + D.GetString(dr[s_Column]) + "'");

                            s_NM_SFT += D.GetString(drs[0]["NAME"]);
                        }

                        try
                        {
                            _biz.Insert_PR_SHIFT(s_CD_COMPANY, s_CD_PLANT, s_NO_SFT, s_NM_SFT);
                        }
                        catch
                        {
                            ShowMessage(공통메세지.작업을정상적으로처리하지못했습니다);
                            return false;
                        }

                        dr["NO_SFT"] = s_NO_SFT;
                        dr["NM_SFT"] = s_NM_SFT;
                    }

                    //
                    // NO_SFT가 있으면
                    // DataTable의 NO_SFT, NM_SFT를 바꿔준다.
                    //
                    else
                    {
                        dr["NO_SFT"] = D.GetString(dt_PR_SHIFT.Rows[0]["NO_SFT"]);
                        dr["NM_SFT"] = D.GetString(dt_PR_SHIFT.Rows[0]["NM_SFT"]);
                    }
                }
            }

            return true;
        }
        #endregion

        #region -> 안전공업 전용 컨트롤 초기화

        private void SettingControl_ANJUN()
        {
            try
            {
                dt기준월.ToDayDate = Global.MainFrame.GetDateTimeToday();
                dt기준월.Text = Global.MainFrame.GetStringYearMonth.Substring(0, 6);

                if (Settings.Default.회사코드 != Global.MainFrame.LoginInfo.CompanyCode)
                    return;


                DataTable dt = _biz.Search_MA_Pitem(D.GetString(_cboCdPlant.SelectedValue), Settings.Default.작업품목코드 + "|");

                if (dt.Rows.Count == 0)
                    return;

                
                if (Settings.Default.작업품목코드 != string.Empty && Settings.Default.작업품목명 != string.Empty)
                {
                    _bptxtCdItem.CodeValue = Settings.Default.작업품목코드;
                    _bptxtCdItem.CodeName = Settings.Default.작업품목명;

                    _header.CurrentRow["CD_ITEM"] = Settings.Default.작업품목코드;
                    _header.CurrentRow["NM_ITEM"] = Settings.Default.작업품목명;
                }

                if (Settings.Default.작업지시수량 != decimal.Zero)
                {
                    _curQtItem.DecimalValue = Settings.Default.작업지시수량;

                    _header.CurrentRow["QT_ITEM"] = Settings.Default.작업지시수량;
                }

                if (Settings.Default.경로유형코드 != string.Empty)
                {
                    경로유형콤보셋팅();

                    DataTable dtPatnRout = (DataTable)_cboPatnRout.DataSource;

                    DataRow[] drs_Select = dtPatnRout.Select("CODE = '" + Settings.Default.경로유형코드 + "'");

                    if (drs_Select.Length == 1)
                    {
                        _cboPatnRout.SelectedValue = Settings.Default.경로유형코드;

                        _header.CurrentRow["PATN_ROUT"] = Settings.Default.경로유형코드;
                    }
                }

                if (Settings.Default.기준월 != string.Empty)
                {
                    dt기준월.Text = Settings.Default.기준월;

                    _header.CurrentRow["CD_USERDEF1"] = Settings.Default.기준월;
                }

                HelpReturn helpReturn = new HelpReturn("P_USER");

                helpReturn.Rows = dt.Select();


                HelpParam helpParam = new HelpParam(HelpID.P_USER);

                helpParam.UserHelpID = "H_MA_PITEM_SUB";


                BpQueryArgs args = new BpQueryArgs(helpReturn, helpParam, null);


                Control_QueryAfter(null, args);

                Page_DataChanged(null, null);

                txt최근발행번호.Text = _biz.Z_ANJUN_Search_WO( D.GetString(_cboCdPlant.SelectedValue),
                                                               D.GetString(_bptxtCdItem.CodeValue) );

                cur계획량.DecimalValue = _biz.Z_ANJUN_Search_계획량( D.GetString(_cboCdPlant.SelectedValue),
                                                                     D.GetString(_bptxtCdItem.CodeValue),
                                                                     D.GetString(dt기준월.Text) );

                cur발행량.DecimalValue = _biz.Z_ANJUN_Search_발행량( D.GetString(_cboCdPlant.SelectedValue),
                                                                     D.GetString(_bptxtCdItem.CodeValue),
                                                                     D.GetString(dt기준월.Text) );
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 업체에 따른 디자이너 변경

        private void Z_ChangedComponent()
        {
            try
            {
                // 테스트용 (84, 108, 40, 124)
                if (MA.ServerKey(true, new string[] {}))
                {
                    btn업체전용M1.Visible = true;
                    btn업체전용M1.Text = DD("작업조건등록");
                    btn업체전용M1.Click += new EventHandler(btn_작업조건등록_Click);

                    btn업체전용D1.Visible = true;
                    btn업체전용D1.Text = DD("장납자재전개");
                    btn업체전용D1.Click += new EventHandler(btn_장납자재전개_Click);
                }

                //
                // 전용으로 컨트롤 사용시 Tag값 설정도 해주어야함
                // (사용자정의컬럼의 경우 여러업체에서 다르게 사용할수있기때문)
                //

                //
                // 한수
                //
                if (MA.ServerKey(false, new string[] { "HANSU" }))
                {
                    //
                    // 2014.05.28 한수전용 포장단위코드, 포장단위명 컬럼추가
                    // CD_PACKUNIT 공장품목도움창으로 추가함.
                    // 한수 전용로직으로 만약 다른업체에서 비고성 품목코드추가요청이 오면
                    // 라벨만 바꿔서 사용하여도 무방함.
                    //

                    bp_PackUnit.Visible = true;
                    bp_PackUnit.Size = new System.Drawing.Size(349, 21);

                    oneGridItem16.Controls.SetChildIndex(bp_PackUnit, oneGridItem16.Controls.Count);
                }

                //
                // 안전공업
                //
                else if (MA.ServerKey(false, new string[] { "ANJUN", "ANJUN2" }))
                {
                    dt기준월.Tag = "CD_USERDEF1";

                    bpPnl_Z_ANJUN1.Visible = true;
                    bpPnl_Z_ANJUN2.Visible = true;
                    bpPnl_Z_ANJUN3.Visible = true;

                    bpPnl_Z_ANJUN1.Size = new System.Drawing.Size(267, 23);
                    bpPnl_Z_ANJUN2.Size = new System.Drawing.Size(267, 23);
                    bpPnl_Z_ANJUN3.Size = new System.Drawing.Size(267, 23);

                    // 컨트롤 순서는 역순으로 가져와야함
                    oneGridItem16.Controls.SetChildIndex(bpPnl_Z_ANJUN3, oneGridItem16.Controls.Count);
                    oneGridItem16.Controls.SetChildIndex(bpPnl_Z_ANJUN2, oneGridItem16.Controls.Count);
                    oneGridItem16.Controls.SetChildIndex(bpPnl_Z_ANJUN1, oneGridItem16.Controls.Count);
                }

                //
                // 한일도요
                //
                else if (MA.ServerKey(false, new string[] { "HANILTOYO" }))
                {
                    btn업체전용M1.Visible = true;
                    btn업체전용M1.Text = DD("작업조건등록");
                    btn업체전용M1.Click += new EventHandler(btn_작업조건등록_Click);
                }

                //
                // 쏠리드
                //
                else if (MA.ServerKey(false, new string[] { "SOLIDTECH", "SOLIDTECH1" }))
                {
                    btn업체전용D1.Visible = true;
                    btn업체전용D1.Text = DD("장납자재전개");
                    btn업체전용D1.Click += new EventHandler(btn_장납자재전개_Click);
                }
            }
            catch (System.Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 사용자정의 콤보박스 세팅
        
        private void SetUserDefine_CD_USERDEF()
        {
            try
            {
                //
                // 안전공업의 경우 사용자정의1을 날짜컨트롤로 사용하여
                // 여기를 타게되면 문제가 된다.
                //
                if (MA.ServerKey(false, new string[] { "ANJUN", "ANJUN2" }))
                    return;

                //
                // 사용자정의 컬럼 콤보박스 세팅
                //
                DataTable dt = MA.GetCode("PR_0000098");

                oneGrid1.ItemCollection.Remove(ogi_USERDEF_row1);
                oneGrid1.ItemCollection.Remove(ogi_USERDEF_row2);

                if (dt.Rows.Count != 0)
                {
                    for (int i = 1; i <= 5; i++)
                    {
                        DataTable dt_CD_USERDEF = MA.GetCode("PR_0000" + D.GetString(i + 98).PadLeft(3, '0'), true);

                        if (dt_CD_USERDEF.Rows.Count > 1)
                        {
                            string colName = "CD_USERDEF" + D.GetString(i);
                            string colCaption = D.GetString(dt.Rows[i - 1]["NAME"]);

                            switch (colName)
                            {
                                case "CD_USERDEF1":
                                    bpp_CD_USERDEF1.Visible = true;

                                    lbl_CD_USERDEF1.Text = colCaption;

                                    cbo_CD_USERDEF1.DataSource = dt_CD_USERDEF;
                                    cbo_CD_USERDEF1.DisplayMember = "NAME";
                                    cbo_CD_USERDEF1.ValueMember = "CODE";

                                    cbo_CD_USERDEF1.Tag = colName;

                                    ogi_USERDEF_row1.Visible = true;

                                    if (!oneGrid1.ItemCollection.Contains(ogi_USERDEF_row1))
                                        oneGrid1.ItemCollection.Add(ogi_USERDEF_row1);

                                    break;

                                case "CD_USERDEF2":
                                    bpp_CD_USERDEF2.Visible = true;

                                    lbl_CD_USERDEF2.Text = colCaption;

                                    cbo_CD_USERDEF2.DataSource = dt_CD_USERDEF;
                                    cbo_CD_USERDEF2.DisplayMember = "NAME";
                                    cbo_CD_USERDEF2.ValueMember = "CODE";

                                    cbo_CD_USERDEF2.Tag = colName;

                                    ogi_USERDEF_row1.Visible = true;

                                    if (!oneGrid1.ItemCollection.Contains(ogi_USERDEF_row1))
                                        oneGrid1.ItemCollection.Add(ogi_USERDEF_row1);

                                    break;

                                case "CD_USERDEF3":
                                    bpp_CD_USERDEF3.Visible = true;

                                    lbl_CD_USERDEF3.Text = colCaption;

                                    cbo_CD_USERDEF3.DataSource = dt_CD_USERDEF;
                                    cbo_CD_USERDEF3.DisplayMember = "NAME";
                                    cbo_CD_USERDEF3.ValueMember = "CODE";

                                    cbo_CD_USERDEF3.Tag = colName;

                                    ogi_USERDEF_row1.Visible = true;

                                    if (!oneGrid1.ItemCollection.Contains(ogi_USERDEF_row1))
                                        oneGrid1.ItemCollection.Add(ogi_USERDEF_row1);

                                    break;

                                case "CD_USERDEF4":
                                    bpp_CD_USERDEF4.Visible = true;

                                    lbl_CD_USERDEF4.Text = colCaption;

                                    cbo_CD_USERDEF4.DataSource = dt_CD_USERDEF;
                                    cbo_CD_USERDEF4.DisplayMember = "NAME";
                                    cbo_CD_USERDEF4.ValueMember = "CODE";

                                    cbo_CD_USERDEF4.Tag = colName;

                                    ogi_USERDEF_row2.Visible = true;

                                    if (!oneGrid1.ItemCollection.Contains(ogi_USERDEF_row2))
                                        oneGrid1.ItemCollection.Add(ogi_USERDEF_row2);

                                    break;

                                case "CD_USERDEF5":
                                    bpp_CD_USERDEF5.Visible = true;

                                    lbl_CD_USERDEF5.Text = colCaption;

                                    cbo_CD_USERDEF5.DataSource = dt_CD_USERDEF;
                                    cbo_CD_USERDEF5.DisplayMember = "NAME";
                                    cbo_CD_USERDEF5.ValueMember = "CODE";

                                    cbo_CD_USERDEF5.Tag = colName;

                                    ogi_USERDEF_row2.Visible = true;

                                    if (!oneGrid1.ItemCollection.Contains(ogi_USERDEF_row2))
                                        oneGrid1.ItemCollection.Add(ogi_USERDEF_row2);

                                    break;

                                default:
                                    break;
                            }
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

        #region -> 사용자정의 숫자컨트롤 세팅
        
        private void SetUserDefine_NUM_USERDEF()
        {
            try
            {
                //
                // 사용자정의 숫자컨트롤 세팅
                //
                DataTable dt = MA.GetCode("PR_0000104");

                oneGrid1.ItemCollection.Remove(ogi_USERDEF_row3);

                if (dt.Rows.Count != 0)
                {
                    int i_rowCount = dt.Rows.Count > 2 ? 2 : dt.Rows.Count;

                    for (int i = 1; i <= i_rowCount; i++)
                    {
                        string colName = "NUM_USERDEF" + D.GetString(i);
                        string colCaption = D.GetString(dt.Rows[i - 1]["NAME"]);

                        switch (colName)
                        {
                            case "NUM_USERDEF1":

                                bpp_NUM_USERDEF1.Visible = true;

                                lbl_NUM_USERDEF1.Text = colCaption;

                                ogi_USERDEF_row3.Visible = true;

                                cur_NUM_USERDEF1.Tag = colName;

                                if (!oneGrid1.ItemCollection.Contains(ogi_USERDEF_row3))
                                    oneGrid1.ItemCollection.Add(ogi_USERDEF_row3);

                                break;

                            case "NUM_USERDEF2":

                                bpp_NUM_USERDEF2.Visible = true;

                                lbl_NUM_USERDEF2.Text = colCaption;

                                ogi_USERDEF_row3.Visible = true;

                                cur_NUM_USERDEF2.Tag = colName;

                                if (!oneGrid1.ItemCollection.Contains(ogi_USERDEF_row3))
                                    oneGrid1.ItemCollection.Add(ogi_USERDEF_row3);

                                break;
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


        #endregion
    }
}

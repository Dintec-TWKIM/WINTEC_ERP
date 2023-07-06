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
    public partial class P_PR_WO_REG_NEW_OLD2 : Duzon.Common.Forms.PageBase
    {
        #region ♣ 생성자/초기화

        private bool b_StateMode = false;    //이벤트가 실행중인지를 확인 한다.

        P_PR_WO_REG_NEW_BIZ _biz = new P_PR_WO_REG_NEW_BIZ();
        FreeBinding _header = null;

  
      


        string PLANT = "";
        string NO_WO = "";
        decimal dNO_WO_LINE = 0m;
        string strSOURCE = string.Empty;
        string str_ServerKey = string.Empty;

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

        
        public P_PR_WO_REG_NEW_OLD2() : this("", "") { }

        public P_PR_WO_REG_NEW_OLD2(string strCD_Plant, string strNO_WO) : this(strCD_Plant, strNO_WO, 0m, "") { }

        public P_PR_WO_REG_NEW_OLD2(string strCD_Plant, string strNO_WO, decimal dNO_WO_LINE, string strSOURCE)
        {
            try
            {
                InitializeComponent();
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

        #region -> InitLoad

        protected override void InitLoad()
        {
            base.InitLoad();

            

            str_ServerKey = Global.MainFrame.ServerKeyCommon;
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
            if (str_ServerKey == "GSITM")       // 파워카본테크놀로지(주)
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

            _flex01.SetExceptEditCol("NM_WC", "NM_WCOP");
            _flex01.SetCodeHelpCol("CD_OP", HelpID.P_PR_ROUT_SUB, ShowHelpEnum.Always, new string[] { "CD_OP", "CD_WC", "NM_WC", "CD_WCOP", "NM_WCOP", "FG_WC" }, new string[] { "CD_OP", "CD_WC", "NM_WC", "CD_WCOP", "NM_OP", "TP_WC" }, new string[] { "CD_OP", "CD_WC", "NM_WC", "CD_WCOP", "NM_WCOP", "FG_WC", "CD_WC", "NM_WC", "CD_WCOP", "NM_WCOP" }, ResultMode.SlowMode);
            _flex01.SetCodeHelpCol("CD_WC", HelpID.P_MA_WC_SUB, ShowHelpEnum.Always, new string[] { "CD_WC", "NM_WC", "FG_WC" }, new string[] { "CD_WC", "NM_WC", "TP_WC" }, new string[] { "CD_WC", "NM_WC", "FG_WC", "CD_WCOP", "NM_WCOP" }, ResultMode.FastMode);
            _flex01.SetCodeHelpCol("CD_WCOP", HelpID.P_PR_WCOP_SUB, ShowHelpEnum.Always, new string[] { "CD_WCOP", "NM_WCOP" }, new string[] { "CD_WCOP", "NM_OP" }, new string[] { "CD_WCOP", "NM_WCOP" }, ResultMode.SlowMode);
            _flex01.SetCodeHelpCol("NO_SFT", "H_PR_SFT_SUB", ShowHelpEnum.Always, new string[] { "NO_SFT", "NM_SFT" }, new string[] { "NO_SFT", "NM_SFT" });
            _flex01.SetCodeHelpCol("CD_EQUIP", "H_PR_EQUIP_SUB", ShowHelpEnum.Always, new string[] { "CD_EQUIP", "NM_EQUIP" }, new string[] { "CD_EQUIP", "NM_EQUIP" });

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

                _lblNoFr.Visible = false;
                _bptxtNoFr.Visible = false;

                if (str_ServerKey == "GSITM")        // 파워카본테크놀로지(주)
                {
                    _lblBatch.Visible = true;
                    _curQtBatch.Visible = true;

                    _lblBatch.Text = "목표수율(%)";
                }
                else
                {
                    _lblBatch.Visible = false;
                    _curQtBatch.Visible = false;
                }

                #endregion
            }
            else if (Pr_Global.Pr_Wo_Batch == "100")        // 배합
            {
                #region 배합

                _lblNoFr.Visible = true;
                _bptxtNoFr.Visible = true;

                if (str_ServerKey == "GSITM")        // 파워카본테크놀로지(주)
                {
                    _lblBatch.Visible = false;
                    _curQtBatch.Visible = false;
                }
                else
                {
                    _lblBatch.Visible = true;
                    _curQtBatch.Visible = true;

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
            InitControl();

            DataSet ds = _biz.Search(_cboCdPlant.SelectedValue == null ? string.Empty : _cboCdPlant.SelectedValue.ToString(), "");
            _header.SetBinding(ds.Tables[0], _pnlMain);
            _header.ClearAndNewRow();

            _flex01.Binding = ds.Tables[1];
            _flex02.Binding = ds.Tables[2];
            _flex03.Binding = ds.Tables[3];
            _flex04.Binding = ds.Tables[4];

            //시스템환경설정의 LOT관리 여부에 따라 사용여부 변경
            if (LoginInfo.MngLot == "Y")
                _txtNoLot.ReadOnly = false;
            else
                _txtNoLot.ReadOnly = true;

            if (NO_WO != "")
            {
                DataSet dsWo = _biz.Search(PLANT, NO_WO);

                _header.SetDataTable(dsWo.Tables[0]);
                _flex01.Binding = dsWo.Tables[1];
                _flex02.Binding = dsWo.Tables[2];
                _flex03.Binding = dsWo.Tables[3];
                _flex04.Binding = dsWo.Tables[4];
                경로유형콤보셋팅();
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
        }

      

        #endregion

        #endregion

        #region ♣ 메인버튼 이벤트

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
                    DataSet ds = _biz.Search(dlg.공장코드, dlg.작업지시번호);
                    
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
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 인쇄버튼클릭

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

            
                if (!Global.MainFrame.ServerKey.Contains("GSITM") && !Global.MainFrame.ServerKey.Contains("SQL_108"))
                    rptHelper.SetDataTable(_flex02.DataTable);
                else
                {
                    //파워카본테크놀러지 전용
                    rptHelper.SetDataTable(_flex01.DataTable, 1, 0);
                    rptHelper.SetDataTable(_flex01.DataTable, 1, 17);
                    rptHelper.SetDataTable(_flex02.DataTable, 1, 20);
                }
             
                 
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

      

        #endregion

        #endregion

        #region ♣ 화면내버튼이벤트

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

                _flex01.DataTable.Rows.Add(NewRow);
            }

            _flex01.Redraw = true;
            _flex01.Row = _flex01.Rows.Count - 1;
            _flex01[_flex01.Row, "YN_FINAL"] = "Y";
            _flex01.IsDataChanged = true;

            if (str_ServerKey == "GSITM")        // 파워카본테크놀로지(주)
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
            }
            else
                dt = dt_Temp.Copy();

            _flex02.Redraw = false;
            _flex02.RemoveViewAll();
            _flex02.Redraw = true;

            decimal MaxSeq = 0;

            _flex02.Redraw = false;

            //DataRow NewRow = null;

            if (!dt.Columns.Contains("CD_TUIP_GROUP"))
                dt.Columns.Add("CD_TUIP_GROUP", typeof(string));
            if (!dt.Columns.Contains("NO_TUIP_SEQ"))
                dt.Columns.Add("NO_TUIP_SEQ", typeof(decimal));


            foreach (DataRow row in dt.Rows)
            {
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
                _flex02["QT_NEED"] = _curQtItem.DecimalValue * Convert.ToDecimal(row["QT_NEED"]);
                _flex02["QT_NEED_NET"] = Convert.ToDecimal(_flex02["QT_NEED"]) / _curQtItem.DecimalValue;
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

                    _header.CurrentRow["CD_ITEM"] = dlg.리턴품목코드;
                    _header.CurrentRow["NM_ITEM"] = dlg.리턴품목명;
                    _header.CurrentRow["STND_ITEM"] = dlg.리턴규격;
                    _header.CurrentRow["UNIT_IM"] = dlg.리턴단위;

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

                    _header.CurrentRow["CD_ITEM"] = dlg.리턴품목코드;
                    _header.CurrentRow["NM_ITEM"] = dlg.리턴품목명;
                    _header.CurrentRow["STND_ITEM"] = dlg.리턴규격;
                    _header.CurrentRow["UNIT_IM"] = dlg.리턴단위;


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

                    _header.CurrentRow["CD_ITEM"] = dlg.ReturnTable.Rows[0]["CD_ITEM"].ToString();
                    _header.CurrentRow["NM_ITEM"] = dlg.ReturnTable.Rows[0]["NM_ITEM"].ToString();
                    _header.CurrentRow["STND_ITEM"] = dlg.ReturnTable.Rows[0]["STND_ITEM"].ToString();
                    _header.CurrentRow["UNIT_IM"] = dlg.ReturnTable.Rows[0]["UNIT_IM"].ToString();

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
                    
                    경로유형콤보셋팅();

                    ChangeQtWo();

                    _tab.SelectedIndex = 3;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region Control_SelectionChangeCommitt
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

        #endregion

        #region ♣ 저장관련메소드

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
                if (_flex02[i, "CD_MATL"].Equals(_bptxtCdItem.CodeValue) && sTp_Wo_Prod == "001" && strTpRout == "000")
                {
                    ShowMessage(공통메세지._와_은는같을수없습니다, _flex02.Cols["CD_MATL"].Caption, _lblCdItem.Text);
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

                if (dr_Chk.Length > 0)
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
                ShowMessage(공통메세지._은는필수입력항목입니다, _lblCdItem.Text);
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
            if (_dtpDtRel.Value > _dtpDtDue.Value)
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
            if (Global.MainFrame.ServerKey.Contains("SOLIDTECH") || Global.MainFrame.ServerKey.Contains("SQL_108") || Global.MainFrame.ServerKey.Contains("DZSQL"))
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

            if (dtH == null && dt01 == null && dt02 == null && dt03 == null && dt04 == null)
                return true;

            bool bSucess = _biz.Save(dtH, dt01, dt02, dt03, dt04, _header.CurrentRow, dtRelease, false);
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

        #region ♣ 이벤트관련

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
                }
                else
                {
                    btn수주추적.Enabled = false;

                    //이형준대리 추가 요청 - 쏠리드(쏠리테크)
                    //계획(p), 확정(o)인 경우에는 오더형태과 경로유형 변경가능하도록 수정
                    _cboTpRout.Enabled = false;
                    _cboPatnRout.Enabled = false;
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

                            _header.CurrentRow["STND_ITEM"] = string.Empty;
                            _header.CurrentRow["UNIT_MO"] = string.Empty;
                            _header.CurrentRow["UNIT_IM"] = string.Empty;
                        }
                        else
                        {
                            DataRow row = _bptxtCdItem.HelpReturn.Rows[0];
                            _txtStndItem.Text = row["STND_ITEM"].ToString();
                            _txtUnitMo.Text = row["UNIT_MO"].ToString();

                            _header.CurrentRow["STND_ITEM"] = row["STND_ITEM"];
                            _header.CurrentRow["UNIT_IM"] = row["UNIT_IM"];
                            _header.CurrentRow["UNIT_MO"] = row["UNIT_MO"];
                        }
                        
                        경로유형콤보셋팅();

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

                        _header.CurrentRow["CD_ITEM"] = string.Empty;
                        _header.CurrentRow["NM_ITEM"] = string.Empty;
                        _header.CurrentRow["STND_ITEM"] = string.Empty;
                        _header.CurrentRow["UNIT_IM"] = string.Empty;
                        _header.CurrentRow["UNIT_MO"] = string.Empty;
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
                            ShowMessage(공통메세지._은는필수입력항목입니다, _lblCdItem.Text);
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
                                {
                                    flex["QT_INV"] = 0;
                                    return;
                                }

                                flex["QT_INV"] = D.GetDecimal(dt.Rows[0]["QT_INV"]);
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
                        if (str_ServerKey == "GSITM")        // 파워카본테크놀로지(주)
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

                   
                        if (dACT - dITEM < dEdit - dAPPLY_OLD)
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
                    if (dREMAIN < dEdit - dPreData)
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
                        _curQtItem.ReadOnly = true;
                    }
                    else
                    {
                        _bptxtCdItem.ReadOnly = ReadOnly.None;
                        _curQtItem.ReadOnly = false;
                    }
                }
                else if (strTpRout == "001")
                {
                    _bptxtCdItem.ReadOnly = ReadOnly.None;
                    _curQtItem.ReadOnly = false;
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

                    _header.CurrentRow["STND_ITEM"] = string.Empty;
                    _header.CurrentRow["UNIT_IM"] = string.Empty;
                    _header.CurrentRow["UNIT_MO"] = string.Empty;
                    //_header.CurrentRow["FG_BF"] = string.Empty;
                    //_header.CurrentRow["FG_GIR"] = string.Empty;

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
                if(strTpRout == "000") 소요량재전개();

                Page_DataChanged(null, null);
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

        #endregion

        #region ♣ 기타메소드

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

        #endregion

        #region ♣ 속성

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
        
    }
}

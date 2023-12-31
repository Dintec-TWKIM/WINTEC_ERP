using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.EMail;
using Duzon.Windows.Print;
using DzHelpFormLib;
using Duzon.ERPU.SA;
using Duzon.ERPU.SA.Common;
using Duzon.Common.Util;
using Duzon.ERPU.MF.Common;

namespace sale
{
    // **************************************
    // 작   성   자 : NJin
    // 재 작  성 일 : 2006-09-26
    // 모   듈   명 : 영업
    // 시 스  템 명 : 영업관리
    // 서브시스템명 : 출하의뢰관리
    // 페 이 지  명 : 고객납품의뢰등록
    // 프로젝트  명 : P_SA_GIR_NEW
    // 2013.04.11 : D20130411014  :납기요청일/출하예정일 /비고 추가
    // **************************************
    public partial class P_SA_GIR_NEW : Duzon.Common.Forms.PageBase
    {
        #region ♣ 생성자 & 변수 선언
        private P_SA_GIR_BIZ _biz = new P_SA_GIR_BIZ();
        private FreeBinding _header = new FreeBinding();
        string so_Price = string.Empty;         //영업그룹을 선택시 판매단가통제유무(SO_PRICE)도 가져온다.
        private bool is수주수량초과허용 = false;   //영업환경설정 : 수주수량 초과허용 추가 2009.07.17 NJin (Default Value = false 으로 셋팅)
        private string 담당자설정여부 = "";
        private 수주관리.Config 수주Config = new 수주관리.Config();
        private bool is중국고객부가세포함단가사용여부 = false;
        public P_SA_GIR_NEW()
        {
            try
            {
                InitializeComponent();
                //이렇게 해주면 위에 툴바가 자동으로 움직여브러유~~~케헤헤헤
                MainGrids = new FlexGrid[] { _flex };

                this.DataChanged += new EventHandler(Page_DataChanged);
                _header.JobModeChanged += new FreeBindingEventHandler(_header_JobModeChanged);
                _header.ControlValueChanged += new FreeBindingEventHandler(_header_ControlValueChanged);

                is수주수량초과허용 = Sa_Global.Qtso_AddAllowYN;  //영업환경설정 : 수주수량 초과허용
                is중국고객부가세포함단가사용여부 = 중국고객부가세포함단가사용여부();
                담당자설정여부 = BASIC.GetMAEXC("거래처선택-담당자적용");

                //통제값에 따라서 그리드의 특정 셋팅이 달라지므로 통제값을 구한 후에 그리드를 셋팅해줘야 한다.

            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        
        #endregion

        #region ♣ 초기화 이벤트 / 메소드

        protected override void InitLoad()
        {
            base.InitLoad();
            InitGrid();
            InitEvent();
        }

        #region ♣ InitGrid : 그리드 초기화
        private void InitGrid()
        {
            _flex.BeginSetting(1, 1, true);
            _flex.SetCol("S", "S", 30, true, CheckTypeEnum.Y_N);
            _flex.SetCol("CD_ITEM", "품목코드", 100, false);
            _flex.SetCol("NM_ITEM", "품목명", 120, false);
            _flex.SetCol("STND_ITEM", "규격", 60, false);
            _flex.SetCol("DT_DUEDATE", "납기요청일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex.SetCol("DT_REQGI", "출하예정일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex.SetCol("YN_INSPECT", "검사유무", 60, true, CheckTypeEnum.Y_N);

            if (BASIC.GetMAEXC("업체별프로세스") == "001")  //아사히카세이전용 프로세스를 사용한다.
            {
                _flex.SetCol("QT_WIDTH", "폭(mm)", 80, false, typeof(decimal), FormatTpType.QUANTITY);
                _flex.SetCol("QT_LENGTH", "길이(m)", 80, false, typeof(decimal), FormatTpType.QUANTITY);
                _flex.SetCol("QT_SQUARE", "면적(㎡)", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            }

            _flex.SetCol("CD_SL", "창고코드", 100);
            _flex.SetCol("NM_SL", "창고명", 120, false);
            _flex.SetCol("NM_GI", "출고형태", 90, false);
            _flex.SetCol("CD_EXCH", "환종", 80, false);
            _flex.SetCol("QT_GIR", "의뢰수량", 80, true, typeof(decimal), FormatTpType.QUANTITY);
            _flex.SetCol("QT_GI", "출하수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);

            if (BASIC.GetMAEXC("업체별프로세스") == "001")  //아사히카세이전용 프로세스를 사용한다.
                _flex.SetCol("SUM_SQUARE", "총면적(㎡)", 80, true, typeof(decimal), FormatTpType.QUANTITY);

            _flex.SetCol("UM", "단가", 80, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);

            if (BASIC.GetMAEXC("업체별프로세스") == "001")  //아사히카세이전용 프로세스를 사용한다.
                _flex.SetCol("UM_SQUARE", "단가(㎡)", 80, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);

            _flex.SetCol("AM_GIR", "금액", 80, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flex.SetCol("RT_EXCH", "환율", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            _flex.SetCol("AM_GIRAMT", "원화금액", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("AM_VAT", "부가세", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("AMT", "합계", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("UNIT", "재고단위", 90, false);
            _flex.SetCol("QT_GIR_IM", "재고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex.SetCol("GI_PARTNER", "납품처코드", 90);
            _flex.SetCol("LN_PARTNER", "납품처명", 120, false);
            _flex.SetCol("NO_PROJECT", "프로젝트코드", 100, false);
            _flex.SetCol("NM_PROJECT", "프로젝트", 120, false);
            _flex.SetCol("CD_ITEM_PARTNER", "거래처품번", 150, false);
            _flex.SetCol("NM_ITEM_PARTNER", "거래처품명", 150, false);
            _flex.SetCol("CD_SALEGRP", "영업그룹", 100, false);
            _flex.SetCol("NM_SALEGRP", "영업그룹명", 100, false);
            _flex.SetCol("DC_RMK", "비고", 150, true);

            if (BASIC.GetMAEXC("W/H 정보사용") == "100")
            {
                _flex.SetCol("CD_WH", "W/H코드", 80, false);
                _flex.SetCol("NM_WH", "W/H코드", 100, false);
            }

            _flex.SetCol("NO_PO_PARTNER", "거래처PO번호", 140, false);
            _flex.SetCol("NO_POLINE_PARTNER", "거래처PO항번", 100, 3, false, typeof(decimal), FormatTpType.QUANTITY);

            _flex.SetCol("YN_ATP", "ATP적용여부", false);

            if (Config.MA_ENV.YN_UNIT == "Y")
            {
                _flex.SetCol("SEQ_PROJECT", "UNIT 항번", 120, false);
                _flex.SetCol("CD_UNIT", "UNIT 코드", 120, false);
                _flex.SetCol("NM_UNIT", "UNIT 명", 120, false);
                _flex.SetCol("STND_UNIT", "UNIT 규격", 100, false);
            }

            _flex.SetCol("MAT_ITEM", "재질", false);

            if (BASIC.GetMAEXC("SET품 사용유무") == "Y")
            {
                _flex.SetCol("CD_ITEM_REF", "SET품목", 120, false);
                _flex.SetCol("NM_ITEM_REF", "SET품명", 120, false);
                _flex.SetCol("STND_ITEM_REF", "SET규격", 120, false);
            }

            if (BASIC.GetMAEXC("배차사용유무") == "Y")
                _flex.SetCol("YN_PICKING", "배송여부", 80, true, CheckTypeEnum.Y_N);
            else if (BASIC.GetMAEXC("배차사용유무") == "N" && Global.MainFrame.ServerKeyCommon.ToUpper() == "DKONT")
                _flex.SetCol("YN_PICKING", "배차여부", 80, true, CheckTypeEnum.Y_N);

            _flex.SetCol("L_CD_USERDEF1", "사용자정의CODE1", 100, true);

            _flex.SetCol("NO_SO", "수주번호", 120, false);
            _flex.SetCol("SEQ_SO", "수주항번", 80, false);
            _flex.SetCol("EN_ITEM", "품목명(영)", 120, false);

            if (Global.MainFrame.ServerKeyCommon == "ANJUN" || Global.MainFrame.ServerKeyCommon == "DZSQL")
            {
                _flex.SetCol("NO_INV", "송장번호", 100, true);
            }

            if (수주Config.부가세포함단가사용())
            {
                _flex.SetCol("TP_UM_TAX", "부가세여부", 90, false);
                _flex.SetCol("UMVAT_GIR", "부가세포함단가", 100, true, typeof(decimal), FormatTpType.UNIT_COST);
            }

            if (BASIC.GetMAEXC("업체별프로세스") == "001")  //아사히카세이전용 프로세스를 사용한다.
            {
                _flex.SetExceptEditCol("CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT", "NM_SL", "NM_GI", "CD_EXCH", "AM_GIRAMT", "AM_VAT", "AMT",
                                       "UNIT", "QT_GIR_IM", "LN_PARTNER", "NO_PROJECT", "NM_PROJECT", "CD_ITEM_PARTNER", "NM_ITEM_PARTNER", "CD_SALEGRP", "NM_SALEGRP", "QT_GI",
                                       "NO_SO", "SEQ_SO", "EN_ITEM", "TP_UM_TAX", "QT_WIDTH", "QT_LENGTH", "QT_SQUARE", "SUM_SQUARE");
            }
            else
            {
                _flex.SetExceptEditCol("CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT", "NM_SL", "NM_GI", "CD_EXCH", "AM_GIRAMT", "AM_VAT", "AMT",
                                       "UNIT", "QT_GIR_IM", "LN_PARTNER", "NO_PROJECT", "NM_PROJECT", "CD_ITEM_PARTNER", "NM_ITEM_PARTNER", "CD_SALEGRP", "NM_SALEGRP", "QT_GI",
                                       "NO_SO", "SEQ_SO", "EN_ITEM", "TP_UM_TAX");
            }

            _flex.VerifyCompare(_flex.Cols["QT_GIR"], 0, OperatorEnum.Greater);

            //의뢰S/L 필수여부에 따라 창고코드 필수사항으로 수정(2011/01/29, BY SMJUNG)
            if (BASIC.GetMAEXC("의뢰 S/L 필수여부") == "100")
                _flex.VerifyNotNull = new string[] { "CD_SL" };

            _flex.SetDummyColumn("S");
            _flex.SettingVersion = "1.0.1.5";
            _flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            //헤더에 필요없는 SUM 제거 && 반듯이 EndSetting 다음에 코딩해줘야한다.
            if (BASIC.GetMAEXC("업체별프로세스") == "001")  //아사히카세이전용 프로세스를 사용한다.
                _flex.SetExceptSumCol("QT_WIDTH", "QT_LENGTH", "QT_SQUARE", "SUM_SQUARE", "UM", "UM_SQUARE", "RT_EXCH", "SEQ_PROJECT", "UMVAT_GIR");//20100329 단가 SUM 제거
            else
                _flex.SetExceptSumCol("UM", "RT_EXCH", "SEQ_PROJECT", "UMVAT_GIR");//20100329 단가 SUM 제거

            _flex.StartEdit += new RowColEventHandler(_flex_StartEdit);
            _flex.ValidateEdit += new ValidateEditEventHandler(_flex_ValidateEdit);
            _flex.BeforeCodeHelp += new BeforeCodeHelpEventHandler(_flex_BeforeCodeHelp);

            if (BASIC.GetMAEXC("W/H 정보사용") == "100")
                _flex.SetCodeHelpCol("CD_SL", HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CD_SL", "NM_SL", "CD_WH", "NM_WH" }, new string[] { "CD_SL", "NM_SL", "CD_WH", "NM_WH" }, ResultMode.SlowMode);
            else
                _flex.SetCodeHelpCol("CD_SL", HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CD_SL", "NM_SL" }, new string[] { "CD_SL", "NM_SL" }, ResultMode.FastMode);

            //_flex.SetCodeHelpCol("GI_PARTNER", HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, "GI_PARTNER", "LN_PARTNER");
            _flex.SetCodeHelpCol("GI_PARTNER", Duzon.Common.Forms.Help.HelpID.P_SA_TPPTR_SUB, ShowHelpEnum.Always,
                                  new string[] { "GI_PARTNER", "LN_PARTNER" },
                                  new string[] { "CD_TPPTR", "NM_TPPTR" });

            _flex.AddMyMenu = true;
            _flex.AddMenuSeperator();
            ToolStripMenuItem parent = _flex.AddPopup("관련 현황");
            _flex.AddMenuItem(parent, "현재고조회", Menu_Click);
        }
        #endregion

        private void InitEvent()
        {
            btn환율변경.Click += new EventHandler(btn환율변경_Click);
            btn전자결재.Click += new EventHandler(btn전자결재_Click);
            btnPMS적용.Click += new EventHandler(btnPMS적용_Click);
            btn배송지주소.Click += new EventHandler(btn배송지주소_Click);
            btn업체전용1.Click += new EventHandler(btn업체전용1_Click);
        }

        #region ♣ InitPaint : 프리폼 초기화
        //PageBase 의 부모함수 이용 : 폼이 올라올 후 폼위에 올라온 컨트롤들을 초기화 시켜주는 역할을 한다.
        //Combo Box 셋팅시 옵션 => N:공백없음, S:공백추가, SC:공백&괄호추가, NC:괄호추가
        //뒤의 코드값들은 DB 에서 정해놓은 파라미터와 매치시켜줘야한다
        protected override void InitPaint()
        {
            base.InitPaint();

            원그리드적용하기();

            System.Diagnostics.Debug.WriteLine("창고====> " + D.GetString(ConfigSA.SA_EXC.WH정보사용));

            btn_Apply_SL.Visible = !ConfigSA.SA_EXC.WH정보사용;

            // 콤보초기화
            DataSet g_dsCombo = this.GetComboData("N;PU_C000016", "N;MA_PLANT", "S;TR_IM00006", "N;MA_B000005");

            // 거래구분
            cbo_TpBusi.DataSource = g_dsCombo.Tables[0];
            cbo_TpBusi.DisplayMember = "NAME";
            cbo_TpBusi.ValueMember = "CODE";

            // 공장 콤보
            cbo_Plant.DataSource = g_dsCombo.Tables[1];
            cbo_Plant.DisplayMember = "NAME";
            cbo_Plant.ValueMember = "CODE";

            // LC 콤보
            cbo_Lc.DataSource = g_dsCombo.Tables[2];
            cbo_Lc.DisplayMember = "NAME";
            cbo_Lc.ValueMember = "CODE";

            _flex.SetDataMap("CD_EXCH", g_dsCombo.Tables[3], "CODE", "NAME");
            _flex.SetDataMap("UNIT", MA.GetCode("MA_B000004"), "CODE", "NAME");

            SetControl str = new SetControl();
            str.SetCombobox(cbo환종, g_dsCombo.Tables[3].Copy());

            // 프리폼 초기화
            object[] obj = new object[3];

            obj[0] = string.Empty;  //회사코드
            obj[1] = string.Empty;  //의뢰번호
            obj[2] = string.Empty;  //공    장

            DataSet ds = _biz.Search(obj);

            _header.SetBinding(ds.Tables[0], tabGir);
            _header.ClearAndNewRow();        // 처음에 행을 하나 만든다. 초기에 추가모드로 하기 위해
            _flex.Binding = ds.Tables[1];

            //프리바인딩이 헤더를 모두 없애 버리니까~ 여기에서 초기화를 시켜 줘야 한다.
            cbo_TpBusi.SelectedValue = "001";   //국내
            cbo_Plant.SelectedValue = Global.MainFrame.LoginInfo.CdPlant;
            cbo_Lc.SelectedValue = string.Empty;
            txt_NoGir.Text = string.Empty;
            dt_gir.Text = Global.MainFrame.GetStringToday;
            bp_Partner.CodeValue = string.Empty;
            bp_Partner.CodeName = string.Empty;
            bp_Emp.CodeValue = Global.MainFrame.LoginInfo.EmployeeNo;
            bp_Emp.CodeName = Global.MainFrame.LoginInfo.EmployeeName;
            bp_CD_SL.CodeValue = string.Empty;
            bp_CD_SL.CodeName = string.Empty;
            txt_DcRmk.Text = string.Empty;

            _header.CurrentRow["NO_GIR"] = string.Empty;
            _header.CurrentRow["DT_GIR"] = Global.MainFrame.GetStringToday;
            _header.CurrentRow["CD_PARTNER"] = string.Empty;
            _header.CurrentRow["CD_PLANT"] = Global.MainFrame.LoginInfo.CdPlant;
            _header.CurrentRow["NO_EMP"] = Global.MainFrame.LoginInfo.EmployeeNo;
            _header.CurrentRow["TP_BUSI"] = "001";   //국내
            _header.CurrentRow["DC_RMK"] = string.Empty;

            //Authority Setting 헤더 프리폼 고정
            Authority(true);

            cbo_Lc.Enabled = false;
            btn_Apply_SO.Enabled = true;
            btn_Apply_LC.Enabled = false;

            cbo환종.SelectionChangeCommitted += new EventHandler(cbo환종_SelectionChangeCommitted);

            if (_biz.GetATP사용여부 == "001")
                bnt_ATP.Visible = true;

            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "ENTEC" || Global.MainFrame.ServerKeyCommon.ToUpper() == "YPP" || Global.MainFrame.ServerKeyCommon.ToUpper() == "DZSQL")
                btn전자결재.Visible = true;
            else if (Global.MainFrame.ServerKeyCommon.ToUpper() == "PEACE")
                btnPMS적용.Visible = true;

            MA_EXC_SET();

            if (BASIC.GetMAEXC("업체별프로세스") == "001")  //아사히카세이전용 프로세스를 사용한다.
                if (Settings.Default.거래구분 != "")
                    cbo_TpBusi.SelectedValue = Settings.Default.거래구분;

            Set환율버튼();
            사용자정의셋팅(); //헤더

            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "DKONT")
            {
                _flex.Cols["L_CD_USERDEF1"].Caption = "포장형태";
                _flex.SetDataMap("L_CD_USERDEF1", MA.GetCode("SA_Z000085", true), "CODE", "NAME");

            }
            else if (Global.MainFrame.ServerKeyCommon.ToUpper() == "ANJUN" || Global.MainFrame.ServerKeyCommon.ToUpper() == "DZSQL")
            {
                _flex.Cols["L_CD_USERDEF1"].Caption = "팔레트적용유무";
                _flex.SetDataMap("L_CD_USERDEF1", MA.GetCodeUser(new string[] { "", "Y" }, new string[] { "미적용", "적용" }), "CODE", "NAME");
                _flex.Cols["L_CD_USERDEF1"].Visible = true;
            }
            else
            {
                사용자정의라인셋팅("L_CD_USERDEF", "SA_B000122", 1, 1);
            }

            SetToolBarButtonState(true, true, false, false, false);

            if (BASIC.GetMAEXC("배차사용유무") == "Y")
                btn배송지주소.Visible = true;

            if (Global.MainFrame.ServerKeyCommon == "ANJUN" || Global.MainFrame.ServerKeyCommon == "DZSQL")
            {
                btn업체전용1.Visible = true;
                btn업체전용1.Text = "팔레트적용";
            }
        }

        void cbo환종_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (MA.기준환율.Option != MA.기준환율옵션.적용안함)
                    cur환율.DecimalValue = MA.기준환율적용(dt_gir.Text, D.GetString(cbo환종.SelectedValue.ToString()));

                if (D.GetString(cbo환종.SelectedValue.ToString()) == "000")
                    cur환율.DecimalValue = 1;

                Set환율버튼();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #endregion

        #region ♣ 현재고조회
        public void Menu_Click(object sender, EventArgs e)
        {
            switch (((ToolStripMenuItem)sender).Name)
            {
                case "현재고조회":
                    String cd_item_multi = "";

                    for (int i = _flex.Row - 2; i < _flex.Row + 40; i++)
                    {
                        if (i < _flex.Rows.Count - 2)
                            cd_item_multi += _flex.DataTable.Rows[i]["CD_ITEM"].ToString() + "|";
                        else
                            break;
                    }
                    pur.P_PU_STOCK_SUB m_dlg = new pur.P_PU_STOCK_SUB(cbo_Plant.SelectedValue.ToString(), cd_item_multi);
                    m_dlg.ShowDialog(this);
                    break;
            }
        }
        #endregion

        #region ♣ 헤더를 풀어주고 라인 삭제버튼을 막아준다.
        private void Authority(bool flag)
        {
            txt_NoGir.Enabled = flag;
            dt_gir.Enabled = flag;
            bp_Partner.Enabled = flag;
            //bp_Emp.Enabled = flag;
            cbo_Plant.Enabled = flag;
            cbo_TpBusi.Enabled = flag;
            cbo_Lc.Enabled = flag;

            if (flag == true)
                btn_DelRow.Enabled = false;
            else
                btn_DelRow.Enabled = true;
        }
        #endregion

        #region ♣ 메인버튼 클릭

        #region ♣ 필수입력 체크
        /// <summary>
        /// 필수입력 항목에 Null 체크해주는 함수
        /// 아래의 NUllCheck() 메소드가 리턴값을 Bool 형태로 반환합니다.
        /// </summary>
        /// <returns></returns>
        private bool Check(string flag)
        {
            Hashtable hList = new Hashtable();

            if (flag == "SEARCH")
            {
                //조회할때 필수 체크 항목과 저장할때 필수 체크 항목이 다르다.
                //조회하고 나면 헤더가 막히기 때문에 해당 항목에 대하여 미리 필수 체크를 한다.
            }
            else if (flag == "SAVE")
            {
                hList.Add(dt_gir, lbl_의뢰일자);
                hList.Add(bp_Emp, lbl_출하의뢰자);
            }

            if (flag != "SEARCH_SUB")
                hList.Add(bp_Partner, lbl_거래처);

            hList.Add(cbo_Plant, lbl_출하공장);
            hList.Add(cbo_TpBusi, lbl_거래구분);

            return ComFunc.NullCheck(hList);
        }
        #endregion

        #region ♣ 조회버튼 클릭
        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                string 반품여부 = "N";

                P_SA_GIR_SCH_SUB dlg = new P_SA_GIR_SCH_SUB(반품여부);

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    object[] obj = new object[3];

                    obj[0] = Global.MainFrame.LoginInfo.CompanyCode;    //회사코드
                    obj[1] = dlg.returnParams[0].ToString();            // 의뢰번호
                    obj[2] = dlg.returnParams[1].ToString();            // 공장

                    DataSet ds = _biz.Search(obj);

                    _header.SetDataTable(ds.Tables[0]);
                    _flex.Binding = ds.Tables[1];


                    Set환율();

                    if (_flex.HasNormalRow)
                        Authority(false);       //라인이 하나라도 존재할때
                    else
                        Authority(true);        //라인이 하나도 없을때
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region ♣ 추가버튼 클릭
        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                //초기화 : 조회조건, 헤더그리드, 라인그리드 모두 초기화 된다.
                InitPaint();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region ♣ 삭제버튼클릭

        #region ♣ 삭제버튼 클릭
        protected override bool BeforeDelete()
        {
            if (ShowMessage(공통메세지.자료를삭제하시겠습니까) != DialogResult.Yes)
                return false;

            return true;
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!_flex.HasNormalRow)
                    return;

                if (!BeforeDelete()) return;

                object[] obj = new object[2];

                obj[0] = Global.MainFrame.LoginInfo.CompanyCode;    //회사코드
                obj[1] = txt_NoGir.Text;                            //의뢰번호

                if (Global.MainFrame.ServerKeyCommon == "ANJUN" || Global.MainFrame.ServerKeyCommon == "DZSQL")
                {
                    if (_biz.Delete안전공업(obj))
                    {
                        ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
                        OnToolBarAddButtonClicked(sender, e);        //삭제 후 바로 초기화 시켜준다.
                    }
                }
                else
                {
                    if (_biz.Delete(obj))
                    {
                        ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
                        OnToolBarAddButtonClicked(sender, e);        //삭제 후 바로 초기화 시켜준다.
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region ♣ 라인삭제
        private void btn_DelRow_Click(object sender, EventArgs e)
        {
            try
            {
                //행이 추가되는 라인이 하나도 존재하지 않을때에는 헤더를 풀어주고 라인 삭제버튼을 막아준다.
                if (!_flex.HasNormalRow)
                    return;

                if (D.GetDecimal(_flex[_flex.Row, "QT_GI"]) > 0)
                {
                    ShowMessage("이미 출하되어 삭제가 불가능합니다.");
                    return;
                }

                _flex.RemoveItem(_flex.Row);

                if (!_flex.HasNormalRow)
                {
                    txt_NoGir.Text = string.Empty;
                    btn_DelRow.Enabled = false; //라인 삭제버튼 비활성

                    //초기화 : 조회조건, 헤더그리드, 라인그리드 모두 초기화 된다.
                    InitPaint();
                    Authority(true);            //헤더 프리폼 수정가능 
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #endregion

        #region ♣ 저장버튼 클릭
        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!_flex.HasNormalRow)
                    OnToolBarDeleteButtonClicked(null, null);   //라인 전표가 없는 헤더전표는 삭제 시켜 버린다.
                else
                {
                    if (!BeforeSave()) return;

                    //if (MsgAndSave(PageActionMode.Save))
                    //    ShowMessage(공통메세지.자료가정상적으로저장되었습니다);

                    //위에꺼 헤더 비고만 수정한 경우 업데이트 하러 SaveData() 함수를 호출 해야 하는데 호출하지 못하는 경우가 발생한다. 흠...
                    if (SaveData())
                    {
                        ShowMessage(공통메세지.자료가정상적으로저장되었습니다);

                        //적용만 받고 저장이 되지 않은 시점에는 라인이 살아 있다고 하더라도 헤더를 풀어준다. 이렇게 풀어줬으니 저장후엔 막아준다.
                        dt_gir.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            if (!Check("SAVE")) return false;

            if (!this.Verify()) return false;

            if (!ATP체크로직(true)) return false;

            DataTable dtL = new DataTable();
            string NO_GIR = string.Empty;

            if (txt_NoGir.Text == string.Empty)
            {
                txt_NoGir.Text = (string)this.GetSeq(LoginInfo.CompanyCode, "SA", "03", dt_gir.Text.Substring(0, 6));

                _header.CurrentRow["NO_GIR"] = txt_NoGir.Text;
                _header.CurrentRow["DT_GIR"] = dt_gir.Text;
                _header.CurrentRow["CD_PARTNER"] = bp_Partner.CodeValue;
                _header.CurrentRow["CD_PLANT"] = cbo_Plant.SelectedValue == null ? string.Empty : cbo_Plant.SelectedValue.ToString();
                _header.CurrentRow["NO_EMP"] = bp_Emp.CodeValue;
                _header.CurrentRow["TP_BUSI"] = cbo_TpBusi.SelectedValue == null ? string.Empty : cbo_TpBusi.SelectedValue.ToString();
                _header.CurrentRow["DC_RMK"] = txt_DcRmk.Text;
                _header.CurrentRow["YN_RETURN"] = "N";
                _header.CurrentRow.AcceptChanges();
                _header.CurrentRow.SetAdded();

                dtL = _flex.DataTable.Clone();

                decimal i = 1;

                foreach (DataRow row in _flex.DataTable.Rows)
                {
                    if (row.RowState == DataRowState.Deleted) continue;

                    row["NO_GIR"] = txt_NoGir.Text;
                    row["SEQ_GIR"] = i++;
                    row["FG_LC_OPEN"] = cbo_Lc.SelectedValue == null ? string.Empty : cbo_Lc.SelectedValue.ToString();

                    row.AcceptChanges();
                    row.SetAdded();

                    dtL.ImportRow(row);
                }
            }
            else
                dtL = _flex.GetChanges();   //한번 저장하고 나면 추가 적용은 일어나지 않어~ 그니까 라인 추가되는 부분은 항상 자동채번쪽에서 따져...

            DataTable dtH = _header.GetChanges();

            if ((dtH == null && dtL == null)) return false;

            //여신체크
            if (cbo_TpBusi.SelectedValue.ToString() == "001") //국내일때만
            {
                decimal am_sum = 0;
                foreach (DataRow row in _flex.DataTable.Rows)
                {
                    if (row.RowState == DataRowState.Deleted) continue;
                    am_sum = am_sum + D.GetDecimal(row["AM_GIRAMT"]) + D.GetDecimal(row["AM_VAT"]);
                }

                if (!_biz.CheckCredit(bp_Partner.CodeValue, am_sum)) return false;
            }

            if (MA.ServerKey(false, new string[] { "ABOV" }) && dtL != null)
            {
                foreach (DataRow row in dtL.Rows)
                {
                    if (row.RowState == DataRowState.Deleted) continue;

                    decimal 의뢰수량 = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row["QT_GIR"]));
                    decimal 단가 = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row["UM"]));
                    decimal 외화금액 = decimal.Zero;
                    외화금액 = Unit.외화금액(DataDictionaryTypes.SA, 단가 * 의뢰수량);

                    decimal 비교 = D.GetDecimal(row["AM_GIR"]) - 외화금액;

                    if (Math.Abs(비교) == 0.01m) continue;

                    if (D.GetDecimal(row["AM_GIR"]) != 외화금액)
                    {
                        ShowMessage("수량 * 단가가 외화금액이랑 맞지 않습니다.");
                        return false;
                    }
                }
            }

            bool bSuccess = _biz.Save(dtH, dtL);

            if (!bSuccess) return false;

            _header.AcceptChanges();
            _flex.AcceptChanges();
            Page_DataChanged(null, null);

            return true;
        }

        #endregion

        #region ♣ 인쇄버튼 클릭
        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            try
            {
                ReportHelper rptHelper = new ReportHelper("R_SA_GIR_PTR_0", "고객납품전표");
                rptHelper.SetData("전표번호", txt_NoGir.Text);
                rptHelper.SetData("처리일자", dt_gir.Text.Substring(0, 4) + "/" + dt_gir.Text.Substring(4, 2) + "/" + dt_gir.Text.Substring(6, 2));
                rptHelper.SetData("출하공장코드", cbo_Plant.SelectedValue == null ? string.Empty : cbo_Plant.SelectedValue.ToString());
                rptHelper.SetData("출하공장명", cbo_Plant.Text);
                rptHelper.SetData("사원코드", bp_Emp.CodeValue);
                rptHelper.SetData("사원명", bp_Emp.CodeName);
                rptHelper.SetData("수불구분", cbo_TpBusi.Text);
                rptHelper.SetData("거래처코드", bp_Partner.CodeValue);
                rptHelper.SetData("거래처명", bp_Partner.CodeName);
                rptHelper.SetData("비고", txt_DcRmk.Text);
                rptHelper.SetData("비고2", txt비고1.Text);


                object objAM_GIRAMT = _flex.DataTable.Compute("SUM(AM_GIRAMT)", "");
                object objAM_VAT = _flex.DataTable.Compute("SUM(AM_VAT)", "");
                object objAMT = _flex.DataTable.Compute("SUM(AMT)", "");

                decimal dAM_GIRAMT = D.GetDecimal(objAM_GIRAMT);
                decimal dAM_VAT = D.GetDecimal(objAM_VAT);
                decimal dAMT = D.GetDecimal(objAMT);

                rptHelper.SetData("원화금액SUM", dAM_GIRAMT.ToString(_flex.Cols["AM_GIRAMT"].Format));
                rptHelper.SetData("부가세SUM", dAM_VAT.ToString(_flex.Cols["AM_GIRAMT"].Format));
                rptHelper.SetData("합계SUM", dAMT.ToString(_flex.Cols["AM_GIRAMT"].Format));

                DataTable dtPartnerInfo = _biz.SearchPartnerInfo(bp_Partner.CodeValue);

                if (dtPartnerInfo.Rows.Count > 0)
                {
                    rptHelper.SetData("통합주소", D.GetString(dtPartnerInfo.Rows[0]["DC_ADS1_H"]) + D.GetString(dtPartnerInfo.Rows[0]["DC_ADS1_D"]));
                    rptHelper.SetData("전화번호", D.GetString(dtPartnerInfo.Rows[0]["NO_TEL1"]));
                    rptHelper.SetData("팩스번호", D.GetString(dtPartnerInfo.Rows[0]["NO_FAX1"]));
                }


                //**********************************************************
                //2011-10-18, 최승애
                //PIMS :D20111011012 추가(자사정보 가져오기)
                //**********************************************************
                DataTable dtMyInfo = _biz.SearchMyInfo();

                if (dtMyInfo.Rows.Count > 0)
                {
                    rptHelper.SetData("자사_사업장번호", D.GetString(dtMyInfo.Rows[0]["MY_NO_BIZAREA"]));
                    rptHelper.SetData("자사_사업장명", D.GetString(dtMyInfo.Rows[0]["MY_NM_COMPANY"]));
                    rptHelper.SetData("자사_대표자", D.GetString(dtMyInfo.Rows[0]["MY_NM_CEO"]));
                    rptHelper.SetData("자사_주소", D.GetString(dtMyInfo.Rows[0]["MY_ADS"]));
                    rptHelper.SetData("자사_전화번호", D.GetString(dtMyInfo.Rows[0]["MY_NO_TEL"]));
                    rptHelper.SetData("자사_팩스번호", D.GetString(dtMyInfo.Rows[0]["MY_NO_FAX"]));
                    rptHelper.SetData("자사_종목", D.GetString(dtMyInfo.Rows[0]["MY_CLS_JOB"]));
                    rptHelper.SetData("자사_업태", D.GetString(dtMyInfo.Rows[0]["MY_TP_JOB"]));
                }

                rptHelper.SetDataTable(_flex.DataTable);
                rptHelper.Print();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region -> 종료

        public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
        {
            Settings.Default.거래구분 = D.GetString(cbo_TpBusi.SelectedValue);
            Settings.Default.Save();

            return base.OnToolBarExitButtonClicked(sender, e);
        }

        #endregion

        #endregion

        #region ♣ 화면내버튼 클릭

        private DataTable getCalcurate(DataTable dt, string flag_Apply)
        {
            DataTable dt_Temp = dt.Clone();


            //DataTable에서 숫자형("System.Decimal", "System.Double", "System.Int32", "System.Int64")컬럼만 추출해주는 함수
            string valueCols = ComFunc.getNumCols(dt);
            string[] valCols = valueCols.Split('|');

            DataRow[] temp_drs;
            foreach (DataRow dr in dt.Rows)
            {
                if (flag_Apply == "SO")
                {
                    temp_drs = dt_Temp.Select("NO_SO = '" + dr["NO_SO"].ToString() + "' AND SEQ_SO = " + dr["SEQ_SO"].ToString() + "", "", DataViewRowState.CurrentRows);

                    if (temp_drs.Length == 0)
                        dt_Temp.ImportRow(dr);
                    else
                    {
                        foreach (string dc in valCols)
                        {
                            //단가는 sum 하면 안되니까 Exception 처리 한다.
                            if (dc != "UM" && dc != "SUM_SQUARE" && dc != "UM_SQUARE" && dc != "SEQ_SO")
                                temp_drs[0][dc] = _flex.CDecimal(temp_drs[0][dc]) + _flex.CDecimal(dr[dc]);
                        }
                    }
                }
                else if (flag_Apply == "LC")
                {
                    temp_drs = dt_Temp.Select("NO_SO = '" + dr["NO_SO"].ToString() + "' AND SEQ_LC = " + dr["SEQ_LC"].ToString() + "", "", DataViewRowState.CurrentRows);

                    if (temp_drs.Length == 0)
                        dt_Temp.ImportRow(dr);
                    else
                    {
                        foreach (string dc in valCols)
                        {
                            //단가는 sum 하면 안되니까 Exception 처리 한다.
                            if (dc != "UM" && dc != "SUM_SQUARE" && dc != "UM_SQUARE" && dc != "SEQ_LC")
                                temp_drs[0][dc] = _flex.CDecimal(temp_drs[0][dc]) + _flex.CDecimal(dr[dc]);
                        }
                    }
                }
            }

            return dt_Temp;
        }

        #region -> 수주적용버튼 클릭
        private void btn_Apply_SO_Click(object sender, EventArgs e)
        {
            try
            {
                //요놈이 수주적용 버튼 띄우기 전에 저장 할 사항 있는지 체크하여 저장할건지 물어봐준다.
                if (!base.BeforeSearch()) return;
                if (!Check("SEARCH_SUB")) return;

                DataTable dt = null;

                if (txt_NoGir.Text == string.Empty)
                {
                    dt = new DataTable();
                    dt = _flex.DataView.ToTable();
                }

                //수량분할이 일어날경우 sum 된 qty 와 amt 를 가져가서 단가를 구해야 한다. 
                if (dt == null) return;
                dt = getCalcurate(dt, "SO");

                P_SA_SO_SUB dlg = new P_SA_SO_SUB(bp_Partner.CodeValue, bp_Partner.CodeName,
                                                  cbo_Plant.SelectedValue.ToString(), cbo_Plant.Text,
                                                  cbo_TpBusi.SelectedValue.ToString(), cbo_TpBusi.Text,
                                                  string.Empty, string.Empty,
                                                  dt);

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    //추가후수정이지만 추가할 헤더가 이미 정해져 있으므로 컨트롤 Enabled 속성을 false로
                    bp_Partner.Enabled = false;
                    cbo_Plant.Enabled = false;
                    cbo_TpBusi.Enabled = false;
                    cbo_Lc.Enabled = false;

                    txt_NoGir.Text = string.Empty;
                    _header.CurrentRow["NO_GIR"] = txt_NoGir.Text;

                    //dlg.수주데이터.Columns.Add("DC_RMK", typeof(string));

                    if (dlg.적용구분 == "APPEND")//적용
                    {
                        _flex.Binding = dlg.수주데이터;

                        foreach (DataRow row in _flex.DataTable.Rows)
                        {
                            row.AcceptChanges();
                            row.SetAdded();
                        }

                        bp_Partner.CodeValue = _flex.DataTable.Rows[0]["CD_PARTNER"].ToString();
                        bp_Partner.CodeName = _flex.DataTable.Rows[0]["NM_PARTNER"].ToString();
                        _header.CurrentRow["CD_PARTNER"] = bp_Partner.CodeValue;
                        _header.CurrentRow["LN_PARTNER"] = bp_Partner.CodeName;

                        if (Global.MainFrame.ServerKeyCommon.ToUpper() != "KORFNT" && 담당자설정여부 == "Y")
                        {
                            DataRow row거래처 = BASIC.GetPartner(D.GetString(bp_Partner.CodeValue));
                            if (D.GetString(row거래처["CD_EMP_SALE"]) == string.Empty)
                            {
                                bp_Emp.SetCode(LoginInfo.EmployeeNo, LoginInfo.EmployeeName);
                                _header.CurrentRow["NO_EMP"] = LoginInfo.EmployeeNo;
                                _header.CurrentRow["NM_KOR"] = LoginInfo.EmployeeName;
                            }
                            else
                            {
                                bp_Emp.SetCode(D.GetString(row거래처["CD_EMP_SALE"]), D.GetString(row거래처["NM_EMP"]));
                                _header.CurrentRow["NO_EMP"] = D.GetString(row거래처["CD_EMP_SALE"]);
                                _header.CurrentRow["NM_KOR"] = D.GetString(row거래처["NM_EMP"]);
                            }
                        }
                    }
                    else if (dlg.적용구분 == "ADDAPPEND")//추가적용
                    {
                        //_flex.BindingAdd(dlg.수주데이터, "");

                        foreach (DataRow drn in dlg.수주데이터.Rows)
                        {
                            bp_Partner.CodeValue = drn["CD_PARTNER"].ToString();
                            bp_Partner.CodeName = drn["NM_PARTNER"].ToString();
                            _flex.DataTable.ImportRow(drn);
                        }

                        _flex.SumRefresh(); //추가적용을 해 왔을때 합계row에 재계산을 해준다.

                        if (Global.MainFrame.ServerKeyCommon.ToUpper() != "KORFNT" && 담당자설정여부 == "Y")
                        {
                            DataRow row거래처 = BASIC.GetPartner(D.GetString(bp_Partner.CodeValue));
                            if (D.GetString(row거래처["CD_EMP_SALE"]) == string.Empty)
                            {
                                bp_Emp.SetCode(LoginInfo.EmployeeNo, LoginInfo.EmployeeName);
                                _header.CurrentRow["NO_EMP"] = LoginInfo.EmployeeNo;
                                _header.CurrentRow["NM_KOR"] = LoginInfo.EmployeeName;
                            }
                            else
                            {
                                bp_Emp.SetCode(D.GetString(row거래처["CD_EMP_SALE"]), D.GetString(row거래처["NM_EMP"]));
                                _header.CurrentRow["NO_EMP"] = D.GetString(row거래처["CD_EMP_SALE"]);
                                _header.CurrentRow["NM_KOR"] = D.GetString(row거래처["NM_EMP"]);
                            }
                        }

                        //선택된 라인 데이터의 거래처가 섞이면 Exception 처리 하기
                        string[] str_Filter = new string[] { "CD_PARTNER" };
                        DataTable dt_Ex = ComFunc.getGridGroupBy(_flex.DataTable, str_Filter, true);
                        if (dt_Ex.Rows.Count != 1)
                        {
                            bp_Partner.CodeValue = string.Empty;
                            bp_Partner.CodeName = string.Empty;

                            if (Global.MainFrame.ServerKeyCommon.ToUpper() != "KORFNT" && 담당자설정여부 == "Y")
                            {
                                bp_Emp.SetCode(LoginInfo.EmployeeNo, LoginInfo.EmployeeName);
                                _header.CurrentRow["NO_EMP"] = LoginInfo.EmployeeNo;
                                _header.CurrentRow["NM_KOR"] = LoginInfo.EmployeeName;
                            }

                            _flex.DataTable.Clear();
                            ShowMessage(" 적용 받은 거래처가 여러개입니다.\n\n하나의 거래처만 적용 가능합니다.");
                            return;
                        }
                    }

                    _flex.IsDataChanged = true;

                    if (dlg.수주헤더셋팅구분 == "Y")
                    {
                        txt_DcRmk.Text = dlg.수주데이터.Rows[0]["SOH_DC_RMK"].ToString();
                        _header.CurrentRow["DC_RMK"] = dlg.수주데이터.Rows[0]["SOH_DC_RMK"].ToString();
                        txt비고1.Text = dlg.수주데이터.Rows[0]["SOH_DC_RMK1"].ToString();
                        _header.CurrentRow["DC_RMK1"] = dlg.수주데이터.Rows[0]["SOH_DC_RMK1"].ToString();
                    }
                    else
                    {
                        txt_DcRmk.Text = string.Empty;
                        _header.CurrentRow["DC_RMK"] = string.Empty;
                        txt비고1.Text = string.Empty;
                        _header.CurrentRow["DC_RMK1"] = string.Empty;
                    }


                    Set환율();

                    Page_DataChanged(null, null);
                    Authority(false);   //헤더 프리폼 픽스

                    //적용만 받고 저장이 되지 않은 시점에는 라인이 살아 있다고 하더라도 헤더를 풀어준다.
                    dt_gir.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region -> LC적용버튼 클릭
        private void btn_Apply_LC_Click(object sender, EventArgs e)
        {
            try
            {
                //요놈이 LC적용 버튼 띄우기 전에 저장 할 사항 있는지 체크하여 저장할건지 물어봐준다.
                if (!base.BeforeSearch()) return;
                if (!Check("SEARCH")) return;

                DataTable dt = null;

                if (txt_NoGir.Text == string.Empty)
                {
                    dt = new DataTable();
                    dt = _flex.DataView.ToTable();
                }

                //수량분할이 일어날경우 sum 된 qty 와 amt 를 가져가서 단가를 구해야 한다. 
                if (dt == null) return;
                dt = getCalcurate(dt, "LC");

                P_SA_LC_SUB dlg = new P_SA_LC_SUB(bp_Partner.CodeValue, bp_Partner.CodeName,
                                                  cbo_TpBusi.SelectedValue.ToString(), cbo_TpBusi.Text,
                                                  dt);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    //추가후수정이지만 추가할 헤더가 이미 정해져 있으므로 컨트롤 Enabled 속성을 false로
                    bp_Partner.Enabled = false;
                    cbo_Plant.Enabled = false;
                    cbo_TpBusi.Enabled = false;
                    cbo_Lc.Enabled = false;

                    txt_NoGir.Text = string.Empty;
                    _header.CurrentRow["NO_GIR"] = string.Empty;

                    dlg.LC테이블.Columns.Add("DC_RMK", typeof(string));

                    if (dlg.적용구분 == "APPEND")//적용
                        _flex.Binding = dlg.LC테이블;
                    else if (dlg.적용구분 == "ADDAPPEND")//추가적용
                    {
                        //_flex.BindingAdd(dlg.수주데이터, "");
                        foreach (DataRow drn in dlg.LC테이블.Rows)
                        {
                            _flex.DataTable.ImportRow(drn);
                        }
                    }

                    _flex.SumRefresh(); //추가적용을 해 왔을때 합계row에 재계산을 해준다.

                    _flex.IsDataChanged = true;

                    if (dlg.수주헤더셋팅구분 == "Y")
                    {
                        txt_DcRmk.Text = dlg.LC테이블.Rows[0]["SOH_DC_RMK"].ToString();
                        _header.CurrentRow["DC_RMK"] = dlg.LC테이블.Rows[0]["SOH_DC_RMK"].ToString();
                    }
                    else
                    {
                        txt_DcRmk.Text = string.Empty;
                        _header.CurrentRow["DC_RMK"] = string.Empty;
                    }

                    Set환율();

                    Page_DataChanged(null, null);
                    Authority(false);   //헤더 프리폼 픽스

                    //적용만 받고 저장이 되지 않은 시점에는 라인이 살아 있다고 하더라도 헤더를 풀어준다.
                    dt_gir.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region -> 프리폼 창고적용버튼 클릭
        private void btn_apply_Click(object sender, EventArgs e)
        {
            if (!_flex.HasNormalRow || _flex.DataTable == null)
                return;



            DataRow[] drs = _flex.DataTable.Select("", "", DataViewRowState.CurrentRows);

            _flex.Redraw = false;
            try
            {
                foreach (DataRow dr in drs)
                {
                    if (dr != null)
                    {
                        dr["CD_SL"] = bp_CD_SL.CodeValue;
                        dr["NM_SL"] = bp_CD_SL.CodeName;
                        dr["CD_WH"] = bp_WH.CodeValue;
                        dr["NM_WH"] = bp_WH.CodeName;
                    }
                }
            }
            finally
            {
                _flex.Redraw = true;
            }

            ShowMessage(공통메세지._작업을완료하였습니다, btn_Apply_SL.Text);
        }
        #endregion

        #region -> 도움창 셋팅

        #region -> Control_QueryBefore
        private void Control_QueryBefore(object sender, BpQueryArgs e)
        {
            switch (e.HelpID)
            {
                case Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB:
                    e.HelpParam.P09_CD_PLANT = cbo_Plant.SelectedValue == null ? Global.MainFrame.LoginInfo.CdPlant : cbo_Plant.SelectedValue.ToString();
                    break;
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
                    case Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB:
                        if (BASIC.GetMAEXC("W/H 정보사용") == "100")
                        {
                            bp_WH.CodeValue = D.GetString(e.HelpReturn.Rows[0]["CD_WH"]);
                            bp_WH.CodeName = D.GetString(e.HelpReturn.Rows[0]["NM_WH"]);
                        }
                        break;
                    case Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB:
                        if (담당자설정여부 == "Y")
                        {
                            DataRow row거래처 = BASIC.GetPartner(e.CodeValue);
                            bp_Emp.SetCode(D.GetString(row거래처["CD_EMP_SALE"]), D.GetString(row거래처["NM_EMP"]));
                            _header.CurrentRow["NO_EMP"] = D.GetString(row거래처["CD_EMP_SALE"]);
                            _header.CurrentRow["NM_KOR"] = D.GetString(row거래처["NM_EMP"]);
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

        private void bp_CD_SL_CodeChanged(object sender, EventArgs e)
        {
            try
            {
                if (bp_CD_SL.CodeValue == string.Empty)
                    bp_WH.SetCode(string.Empty, string.Empty);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 콘트롤 키이벤트
        private void Control_KeyEvent(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                SendKeys.SendWait("{TAB}");
        }
        #endregion

        #region -> 환율변경

        void btn환율변경_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flex.HasNormalRow || !Chk환종) return;

                string 환종 = D.GetString(cbo환종.SelectedValue);
                decimal 환율 = Unit.환율(DataDictionaryTypes.SA, cur환율.DecimalValue);

                if (환종 == "000")
                {
                    ShowMessage("KRW는 변경하실 수 없습니다.");
                    return;
                }

                if (환율 == 0)
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, DD("환율"));
                    return;
                }

                DataRow[] dr = _flex.DataTable.Select("S='Y'", "", DataViewRowState.CurrentRows);
                if (dr == null || dr.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                for (int i = _flex.Rows.Fixed; i < _flex.Rows.Count; i++)
                {
                    if (D.GetString(_flex[i, "S"]) == "N") continue;
                    string CD_EXCH = D.GetString(_flex[i, "CD_EXCH"]);

                    if (환종 != CD_EXCH) continue;

                    _flex[i, "RT_EXCH"] = 환율;
                    _flex[i, "AM_GIRAMT"] = Unit.원화금액(DataDictionaryTypes.SA, (D.GetDecimal(_flex[i, "AM_GIR"]) * D.GetDecimal(_flex[i, "RT_EXCH"])));
                    _flex[i, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, (D.GetDecimal(_flex[i, "AM_GIRAMT"]) * ((D.GetDecimal(_flex[i, "RT_VAT"]) / 100))));
                    _flex[i, "AMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex[i, "AM_GIRAMT"]) + D.GetDecimal(_flex[i, "AM_VAT"]));//합계
                }

                ShowMessage(공통메세지._작업을완료하였습니다, DD("환율변경"));
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> ATP CHECK

        private void bnt_ATP_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flex.HasNormalRow)
                    return;

                if (ATP체크로직(false))
                    ShowMessage("납기일에 이상이 없습니다.");
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        #region -> 메일전송버튼 클릭
        private void btn메일전송_Click(object sender, EventArgs e)
        {
            ReportHelper rptHelper = new ReportHelper("R_SA_GIR_PTR_0", "고객납품전표");
            rptHelper.SetDataTable(_flex.DataTable);

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["전표번호"] = txt_NoGir.Text;
            dic["처리일자"] = dt_gir.Text.Substring(0, 4) + "/" + dt_gir.Text.Substring(4, 2) + "/" + dt_gir.Text.Substring(6, 2);
            dic["출하공장코드"] = cbo_Plant.SelectedValue == null ? string.Empty : cbo_Plant.SelectedValue.ToString();
            dic["출하공장명"] = cbo_Plant.Text;
            dic["사원코드"] = bp_Emp.CodeValue;
            dic["사원명"] = bp_Emp.CodeName;
            dic["수불구분"] = cbo_TpBusi.Text;
            dic["거래처코드"] = bp_Partner.CodeValue;
            dic["거래처명"] = bp_Partner.CodeName;
            dic["비고"] = txt_DcRmk.Text;

            string[] str_histext = new string[3];
            str_histext[0] = string.Empty;              //제목
            str_histext[1] = Settings.Default.MAIL_ADD; //받는사람 이메일주소
            str_histext[2] = string.Empty;              //내용

            P_MF_EMAIL mail = new P_MF_EMAIL(new string[] { bp_Partner.CodeValue }, "R_SA_GIR_PTR_0", new ReportHelper[] { rptHelper }, dic, "고객납품전표", str_histext);
            mail.ShowDialog();
            Settings.Default.MAIL_ADD = mail._str_rt_data[0];
            Settings.Default.Save();
        }
        #endregion

        #region -> 전자결재
        void btn전자결재_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsChanged() || _header.GetChanges() != null || txt_NoGir.Text == string.Empty)
                {
                    ShowMessage("저장후 전자결재버튼을 클릭하세요.");
                    return;
                }

                GirInterWork siw = new GirInterWork();

                if (siw.전자결재(_header.CurrentRow, _flex.DataTable))
                    ShowMessage("전자결재가 완료되었습니다.");
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region -> PMS 적용

        void btnPMS적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!base.BeforeSearch()) return;
                if (!Check("SEARCH_SUB")) return;

                P_SA_Z_VPHI_PMS_SUB dlg = new P_SA_Z_VPHI_PMS_SUB(bp_Partner.CodeValue, bp_Partner.CodeName, D.GetString(cbo_Plant.SelectedValue), cbo_Plant.Text, D.GetString(cbo_TpBusi.SelectedValue), cbo_TpBusi.Text);

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    //추가후수정이지만 추가할 헤더가 이미 정해져 있으므로 컨트롤 Enabled 속성을 false로
                    bp_Partner.Enabled = false;
                    cbo_Plant.Enabled = false;
                    cbo_TpBusi.Enabled = false;
                    cbo_Lc.Enabled = false;

                    txt_NoGir.Text = string.Empty;
                    _header.CurrentRow["NO_GIR"] = txt_NoGir.Text;

                    _flex.Binding = dlg.수주데이터;

                    foreach (DataRow row in _flex.DataTable.Rows)
                    {
                        row.AcceptChanges();
                        row.SetAdded();
                    }

                    bp_Partner.CodeValue = _flex.DataTable.Rows[0]["CD_PARTNER"].ToString();
                    bp_Partner.CodeName = _flex.DataTable.Rows[0]["NM_PARTNER"].ToString();
                    _header.CurrentRow["CD_PARTNER"] = bp_Partner.CodeValue;
                    _header.CurrentRow["LN_PARTNER"] = bp_Partner.CodeName;

                    if (담당자설정여부 == "Y")
                    {
                        DataRow row거래처 = BASIC.GetPartner(D.GetString(bp_Partner.CodeValue));
                        bp_Emp.SetCode(D.GetString(row거래처["CD_EMP_SALE"]), D.GetString(row거래처["NM_EMP"]));
                        _header.CurrentRow["NO_EMP"] = D.GetString(row거래처["CD_EMP_SALE"]);
                        _header.CurrentRow["NM_KOR"] = D.GetString(row거래처["NM_EMP"]);
                    }

                    _flex.IsDataChanged = true;

                    Set환율();

                    Page_DataChanged(null, null);
                    Authority(false);   //헤더 프리폼 픽스

                    //적용만 받고 저장이 되지 않은 시점에는 라인이 살아 있다고 하더라도 헤더를 풀어준다.
                    dt_gir.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 배송지주소 클릭
        void btn배송지주소_Click(object sender, EventArgs e)
        {
            try
            {
                if (_flex.DataTable == null || _flex.DataTable.Rows.Count == 0)
                {
                    this.ShowMessage("데이터가 존재하지 않습니다.");
                    return;
                }

                P_SA_SO_DLV_SUB dlg = null; ;

                object[] obj = new object[2];
                obj[0] = MA.Login.회사코드;
                obj[1] = bp_Partner.CodeValue;

                //배송정보 도움창에 헤더의 거래처에 있는 필요정보를 넘겨준다.
                DataTable dt_partinfo = _biz.GetPartnerInfoSearch(obj);

                string[] str = new string[8];
                if (dt_partinfo.Rows.Count == 1)
                {
                    str[0] = D.GetString(dt_partinfo.Rows[0]["CD_PARTNER"]);
                    str[1] = D.GetString(dt_partinfo.Rows[0]["NO_POST2"]);
                    str[2] = D.GetString(dt_partinfo.Rows[0]["DC_ADS2_H"]);
                    str[3] = D.GetString(dt_partinfo.Rows[0]["DC_ADS2_D"]);
                    str[4] = D.GetString(dt_partinfo.Rows[0]["NO_TEL2"]);
                    str[5] = D.GetString(dt_partinfo.Rows[0]["CD_EMP_PARTNER"]);
                    str[6] = D.GetString(dt_partinfo.Rows[0]["NO_HPEMP_PARTNER"]);
                    str[7] = D.GetString(dt_partinfo.Rows[0]["CD_AREA"]);
                }

                DataTable dt = _flex.DataTable.Clone();

                foreach (DataRow dr in _flex.DataTable.Rows)
                {
                    if (dr.RowState == DataRowState.Deleted) continue;

                    dt.Rows.Add(dr.ItemArray);
                }
                dt.AcceptChanges();

                string NO_SO = "";

                if (dt != null && dt.Rows.Count > 0)
                    NO_SO = D.GetString(dt.Rows[0]["NO_SO"]);

                dlg = new P_SA_SO_DLV_SUB(dt, str, "SEQ_SO", true, NO_SO);

                if (dt != null && dt.Rows.Count > 0)
                    dlg.NO_SO = D.GetString(dt.Rows[0]["NO_SO"]);

                if (dlg.ShowDialog() != DialogResult.OK) return;
            }
            catch (Exception ex)
            {

                MsgEnd(ex);
            }
        }
        #endregion

        #region -> 업체전용1 클릭

        void btn업체전용1_Click(object sender, EventArgs e)
        {
            try
            {
                string 서버키 = Global.MainFrame.ServerKeyCommon;

                if (서버키 == "DZSQL" || 서버키 == "ANJUN")
                {

                    if (!_flex.HasNormalRow) return;

                    if (IsChanged())
                    {
                        ShowMessage("변경된 내용이 있습니다. 저장후 팔레트적용 버튼을 클릭하세요.");
                        return;
                    }


                    DataRow[] dr = _flex.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                    if (dr == null || dr.Length == 0)
                    {
                        Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                        return;
                    }

                    DataRow[] chkdr = _flex.DataTable.Select("S = 'Y' AND L_CD_USERDEF1 = 'Y' ", "", DataViewRowState.CurrentRows);

                    if (chkdr != null && chkdr.Length > 0)
                    {
                        Global.MainFrame.ShowMessage("이미 팔레트적용된 데이터가 존재합니다.");
                        return;
                    }

                    sale.관련도움창.P_SA_Z_ANJUN_PALLET_SUB dlg = new sale.관련도움창.P_SA_Z_ANJUN_PALLET_SUB(dr, D.GetString(cbo_Plant.SelectedValue));

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        object[] obj = new object[3];

                        obj[0] = Global.MainFrame.LoginInfo.CompanyCode;    //회사코드
                        obj[1] = txt_NoGir.Text;           // 의뢰번호
                        obj[2] = D.GetString(cbo_Plant.SelectedValue);            // 공장

                        DataSet ds = _biz.Search(obj);

                        _header.SetDataTable(ds.Tables[0]);
                        _flex.Binding = ds.Tables[1];


                        Set환율();

                        if (_flex.HasNormalRow)
                            Authority(false);       //라인이 하나라도 존재할때
                        else
                            Authority(true);        //라인이 하나도 없을때
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

        #region ♣ _header_ControlValueChanged
        void _header_JobModeChanged(object sender, FreeBindingArgs e)
        {
            try
            {
                if (e.JobMode == JobModeEnum.추가후수정)
                {
                    bp_Emp.Enabled = true;
                }
                else
                {
                    bp_Emp.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                bool bChange = _header.GetChanges() != null ? true : false;

                if (bChange)
                    this.ToolBarSaveButtonEnabled = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        private void _header_ControlValueChanged(object sender, FreeBindingArgs e)
        {
            try
            {
                bool bChange = _header.GetChanges() != null ? true : false;

                if (bChange)
                    this.ToolBarSaveButtonEnabled = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region ♣ 그리드 이벤트

        #region ♣ _flex_StartEdit
        private void _flex_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (Global.MainFrame.ServerKeyCommon.ToUpper() == "CSIT")
                {
                    e.Cancel = true;
                    return;
                }

                //의뢰기준 송장등록이 되어 있다면 수정을 막는다.(2012.04.18)
                if (_flex.DataTable.Columns.Contains("QT_INVL_REQ"))    //조회버튼을 눌러 기존 저장된건 조회 한 경우
                {
                    if (D.GetDecimal(_flex["QT_INVL_REQ"]) > 0m)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                else  //수주적용, L/C적용 한 경우
                {
                    string noGir = D.GetString(_flex["NO_GIR"]);

                    if (noGir != string.Empty)    //저장되지 않은건은 체크 필요 없음
                    {
                        if (_biz.IsInvReq(noGir, D.GetDecimal(_flex["SEQ_GIR"])))
                        {
                            e.Cancel = true;
                            return;
                        }
                    }
                }

                if (Global.MainFrame.ServerKeyCommon == "DZSQL" || Global.MainFrame.ServerKeyCommon == "GNSD" || is중국고객부가세포함단가사용여부)
                {
                    switch (_flex.Cols[e.Col].Name)
                    {
                        case "UM":
                        case "AM_GIR":
                            if (D.GetString(_flex["TP_UM_TAX"]) == "Y")
                            {
                                e.Cancel = true;
                                return;
                            }
                            break;
                        case "UMVAT_GIR":
                            if (D.GetString(_flex["TP_UM_TAX"]) != "Y")
                            {
                                e.Cancel = true;
                                return;
                            }
                            break;
                        default:
                            break;
                    }
                }

                switch (_flex.Cols[e.Col].Name)
                {
                    case "QT_GIR":
                        if (D.GetString(_flex["NO_PMS"]) != string.Empty)
                        {
                            e.Cancel = true;
                            return;
                        }
                        break;
                    case "RT_EXCH":
                        if (D.GetString(_flex["CD_EXCH"]) == "000")
                        {
                            e.Cancel = true;
                            return;
                        }
                        if (txt_NoGir.Text != string.Empty)
                        {
                            ShowMessage("의뢰된건은 변경하실 수 없습니다.");
                            e.Cancel = true;
                            return;
                        }
                        break;
                    case "UM":
                    case "SUM_SQUARE":
                    case "UM_SQUARE":
                    case "AM_GIR":
                        if (D.GetDecimal(_flex["QT_GI"]) > 0m)
                        {
                            e.Cancel = true;
                            return;
                        }

                        DataRow row영업그룹 = BASIC.GetSaleGrp(D.GetString(_flex["CD_SALEGRP"]));
                        if (D.GetString(row영업그룹["SO_PRICE"]) == "Y")
                        {
                            ShowMessage("영업단가통제된 영업그룹입니다.");
                            e.Cancel = true;
                            return;
                        }
                        break;
                    case "GI_PARTNER":
                        if (D.GetDecimal(_flex["QT_GI"]) > 0m)
                        {
                            e.Cancel = true;
                            return;
                        }
                        break;
                    case "YN_INSPECT":
                        if (_flex.RowState() != DataRowState.Added)
                        {
                            e.Cancel = true;
                            return;
                        }
                        break;
                    case "SEQ_PROJECT":
                    case "CD_UNIT":
                    case "NM_UNIT":
                    case "STND_UNIT":
                        e.Cancel = true;
                        break;
                    case "L_CD_USERDEF1":
                        if (Global.MainFrame.ServerKeyCommon == "ANJUN" || Global.MainFrame.ServerKeyCommon == "DZSQL")
                        {
                            e.Cancel = true;
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

        #region ♣ _flex_ValidateEdit
        private void _flex_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                string colname = _flex.Cols[e.Col].Name;
                string oldValue = D.GetString(((FlexGrid)sender).GetData(e.Row, e.Col));
                string newValue = ((FlexGrid)sender).EditData;

                if (colname == "DT_DUEDATE" || colname == "DT_REQGI")
                {
                    if (oldValue.ToUpper() == newValue.ToUpper())
                        return;
                }
                else
                {
                    //oldValue:1234.0000와 newValue:1234의 경우 string형태로 비교하면 다른값이지만 decimal형태로 비교하면 같은값이다.
                    if (D.GetDecimal(oldValue) == D.GetDecimal(newValue))
                        return;
                }

                switch (colname)
                {
                    case "QT_GIR":
                        decimal 출하수량 = D.GetDecimal(_flex["QT_GI"]);
                        if (출하수량 > 0 && D.GetDecimal(newValue) < 출하수량)
                        {
                            _flex[e.Row, "QT_GIR"] = D.GetDecimal(oldValue);
                            ShowMessage("의뢰수량이 출하수량보다 작을 수 없습니다.");
                            e.Cancel = true;
                            return;
                        }

                        //의뢰수량체크 프로스져를 호출한다.
                        object[] obj = new object[8];
                        obj[0] = Global.MainFrame.LoginInfo.CompanyCode;
                        obj[1] = txt_NoGir.Text;
                        obj[2] = D.GetDecimal(_flex[e.Row, "SEQ_GIR"]);
                        obj[3] = _flex[e.Row, "NO_SO"].ToString();
                        obj[4] = D.GetDecimal(_flex[e.Row, "SEQ_SO"]);
                        obj[5] = _flex[e.Row, "NO_LC"].ToString();
                        obj[6] = D.GetDecimal(_flex[e.Row, "SEQ_LC"]);
                        obj[7] = D.GetDecimal(newValue);

                        // is수주수량초과허용 = false 일 경우 수주(의뢰) 수량이 초과하려는 경우 수주(의뢰) 수량으로 맞춘다.
                        // is수주수량초과허용 = true  일 경우 수주(의뢰) 수량이 초과허용 가능하다.
                        //decimal qtso_AddAllow = D.GetDecimal(_flex[e.Row, "QT_MINUS"]) + (D.GetDecimal(_flex[e.Row, "QT_MINUS"]) * (D.GetDecimal(_flex[e.Row, "RT_PLUS"]) / 100));
                        decimal 의뢰한도수량 = D.GetDecimal(_flex["QT_SO"]) * D.GetDecimal(_flex["RT_PLUS"]) * 0.01M;
                        의뢰한도수량 += D.GetDecimal(_flex["QT_SO"]);
                        //D.GetDecimal(_flex[e.Row, "REQ_QT_GIR"]) + (D.GetDecimal(_flex[e.Row, "REQ_QT_GIR"]) * (D.GetDecimal(_flex[e.Row, "RT_PLUS"]) / 100));

                        decimal 분할의뢰수량;
                        bool is입력 = _biz.Check(obj, out 분할의뢰수량);

                        if (!is입력)
                        {
                            bool is허용 = false;
                            if (is수주수량초과허용)
                            {
                                // 범위 허용
                                if (의뢰한도수량 >= D.GetDecimal(newValue) + 분할의뢰수량)
                                {
                                    is허용 = true;
                                }
                            }

                            if (!is허용)
                            {
                                _flex[e.Row, "QT_GIR"] = D.GetDecimal(oldValue);
                                e.Cancel = true;
                                ShowMessage("의뢰수량이 한도수량을 초과하였습니다.");
                            }
                        }

                        금액계산();
                        break;

                    case "UM":
                        _flex[e.Row, "UM_SQUARE"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex[e.Row, "QT_SQUARE"]) == 0m ? 0m : (D.GetDecimal(newValue) / D.GetDecimal(_flex[e.Row, "QT_SQUARE"])));
                        _flex[e.Row, "AM_GIR"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex[e.Row, "QT_GIR"]) * D.GetDecimal(newValue));
                        _flex[e.Row, "AM_GIRAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex[e.Row, "AM_GIR"]) * D.GetDecimal(_flex[e.Row, "RT_EXCH"]));
                        _flex[e.Row, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex[e.Row, "AM_GIRAMT"]) * ((D.GetDecimal(_flex[e.Row, "RT_VAT"]) / 100)));
                        _flex[e.Row, "AMT"] = D.GetDecimal(_flex[e.Row, "AM_GIRAMT"]) + D.GetDecimal(_flex[e.Row, "AM_VAT"]);//합계
                        break;

                    case "UM_SQUARE":
                        _flex[e.Row, "UM"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex[e.Row, "QT_SQUARE"]) * D.GetDecimal(newValue));
                        _flex[e.Row, "AM_GIR"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex[e.Row, "QT_GIR"]) * D.GetDecimal(_flex[e.Row, "UM"]));
                        _flex[e.Row, "AM_GIRAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex[e.Row, "AM_GIR"]) * D.GetDecimal(_flex[e.Row, "RT_EXCH"]));
                        _flex[e.Row, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex[e.Row, "AM_GIRAMT"]) * ((D.GetDecimal(_flex[e.Row, "RT_VAT"]) / 100)));
                        _flex[e.Row, "AMT"] = D.GetDecimal(_flex[e.Row, "AM_GIRAMT"]) + D.GetDecimal(_flex[e.Row, "AM_VAT"]);//합계
                        break;

                    case "RT_EXCH":
                        _flex[e.Row, "AM_GIRAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex[e.Row, "AM_GIR"]) * D.GetDecimal(_flex[e.Row, "RT_EXCH"]));
                        _flex[e.Row, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex[e.Row, "AM_GIRAMT"]) * ((D.GetDecimal(_flex[e.Row, "RT_VAT"]) / 100)));
                        _flex[e.Row, "AMT"] = D.GetDecimal(_flex[e.Row, "AM_GIRAMT"]) + D.GetDecimal(_flex[e.Row, "AM_VAT"]);//합계
                        if (D.GetDecimal(_flex[e.Row, "QT_GIR"]) != 0)
                            _flex[e.Row, "UMVAT_GIR"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex[e.Row, "AMT"]) / D.GetDecimal(_flex[e.Row, "QT_GIR"]));
                        else
                            _flex[e.Row, "UMVAT_GIR"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex[e.Row, "AMT"]));
                        break;

                    case "AM_GIR":
                        if (D.GetDecimal(_flex[e.Row, "QT_GIR"]) != 0)
                        {
                            _flex[e.Row, "UM"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(newValue) / D.GetDecimal(_flex[e.Row, "QT_GIR"]));
                            _flex[e.Row, "UM_SQUARE"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex[e.Row, "QT_SQUARE"]) == 0m ? 0m : (D.GetDecimal(_flex[e.Row, "UM"]) / D.GetDecimal(_flex[e.Row, "QT_SQUARE"])));
                        }
                        else
                        {
                            _flex[e.Row, "UM"] = 0;
                            _flex[e.Row, "UM_SQUARE"] = 0;
                        }
                        _flex[e.Row, "AM_GIRAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(newValue) * D.GetDecimal(_flex[e.Row, "RT_EXCH"]));
                        _flex[e.Row, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex[e.Row, "AM_GIRAMT"]) * ((D.GetDecimal(_flex[e.Row, "RT_VAT"]) / 100)));
                        _flex[e.Row, "AMT"] = D.GetDecimal(_flex[e.Row, "AM_GIRAMT"]) + D.GetDecimal(_flex[e.Row, "AM_VAT"]);//합계
                        break;

                    case "DT_DUEDATE":
                    case "DT_REQGI":
                        if (!D.StringDate.IsValidDate(newValue, false, _flex.Cols[colname].Caption))
                        {
                            e.Cancel = true;
                            return;
                        }

                        if (colname == "DT_REQGI" && D.GetString(_flex[e.Row, "DT_REQGI"]) != string.Empty)
                            _flex[e.Row, "DT_DUEDATE"] = _flex[e.Row, "DT_REQGI"].ToString();
                        break;

                    case "UMVAT_GIR":
                        _flex["AMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["QT_GIR"]) * D.GetDecimal(newValue));
                        if (Global.MainFrame.LoginInfo.CompanyLanguage == Language.KR)
                            _flex["AM_GIRAMT"] = Decimal.Round(D.GetDecimal(_flex["AMT"]) * (100 / (100 + D.GetDecimal(_flex["RT_VAT"]))), MidpointRounding.AwayFromZero);
                        else
                            _flex["AM_GIRAMT"] = D.GetDecimal(UDecimal.GetConvertFormatData(DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.INSERT, (D.GetDecimal(_flex["AMT"]) * (100 / (100 + D.GetDecimal(_flex["RT_VAT"]))))));
                        _flex["AM_VAT"] = D.GetDecimal(_flex["AMT"]) - D.GetDecimal(_flex["AM_GIRAMT"]);
                        _flex["AM_GIR"] = D.GetDecimal(_flex["RT_EXCH"]) == 0m ? 0m : Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_GIRAMT"]) / D.GetDecimal(_flex["RT_EXCH"]));
                        _flex["UM"] = D.GetDecimal(_flex["QT_GIR"]) == 0m ? 0m : Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_GIR"]) / D.GetDecimal(_flex["QT_GIR"]));
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region ♣ _flex_BeforeCodeHelp ( 그리드 도움창 셋팅 )
        private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                switch (_flex.Cols[e.Col].Name)
                {
                    case "CD_SL":
                        if (D.GetDecimal(_flex["QT_GI"]) > 0)
                        {
                            e.Cancel = true;
                            return;
                        }
                        else
                            e.Parameter.P09_CD_PLANT = cbo_Plant.SelectedValue == null ? Global.MainFrame.LoginInfo.CdPlant : cbo_Plant.SelectedValue.ToString();
                        break;
                    case "GI_PARTNER":
                        if (D.GetDecimal(_flex["QT_GI"]) > 0)
                        {
                            e.Cancel = true;
                            return;
                        }
                        e.Parameter.P14_CD_PARTNER = bp_Partner.CodeValue;
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

        #region ♣ OnComboBoxSelectionChangeCommitted
        private void OnComboBoxSelectionChangeCommitted(object sender, System.EventArgs e)
        {
            switch (((Control)sender).Name)
            {

                case "cbo_TpBusi": //거래구분
                    if (cbo_TpBusi.SelectedValue.ToString() == "003")       // 거래구분이 "LOCAL L/C 일때..
                    {
                        cbo_Lc.Enabled = true;

                        if (cbo_Lc.SelectedValue.ToString() == "001")       //선LC이면
                        {
                            btn_Apply_LC.Enabled = true;	// L/C 적용버튼
                            btn_Apply_SO.Enabled = false;	// 수주적용버튼
                        }
                        else if (cbo_Lc.SelectedValue.ToString() == "002")  //후LC이면
                        {
                            btn_Apply_LC.Enabled = false;	// L/C 적용버튼
                            btn_Apply_SO.Enabled = true;	// 수주적용버튼
                        }
                        else                                                //널이면
                        {
                            btn_Apply_LC.Enabled = false;	// L/C 적용버튼
                            btn_Apply_SO.Enabled = false;	// 수주적용버튼
                        }
                    }
                    else if (cbo_TpBusi.SelectedValue.ToString() == "004")  //거래구분이 "MASTER L/C 일때..
                    {
                        cbo_Lc.SelectedValue = "001";  //선LC
                        cbo_Lc.Enabled = false;
                        btn_Apply_LC.Enabled = true;	// L/C 적용버튼
                        btn_Apply_SO.Enabled = false;	// 수주적용버튼
                    }
                    else if (cbo_TpBusi.SelectedValue.ToString() == "005")  //거래구분이 "MASTER 기타 일때..
                    {
                        cbo_Lc.SelectedValue = "002";  //후LC
                        cbo_Lc.Enabled = false;
                        btn_Apply_LC.Enabled = false;	// L/C 적용버튼
                        btn_Apply_SO.Enabled = true;	// 수주적용버튼
                    }
                    else
                    {
                        cbo_Lc.SelectedValue = string.Empty;
                        cbo_Lc.Enabled = false;
                        btn_Apply_LC.Enabled = false;	// L/C 적용버튼
                        btn_Apply_SO.Enabled = true;	// 수주적용버튼
                    }
                    break;

                case "cbo_Lc":
                    if (cbo_Lc.SelectedValue.ToString() == "001")       //선LC일때
                    {
                        btn_Apply_LC.Enabled = true;	                //L/C 적용버튼
                        btn_Apply_SO.Enabled = false;	                //수주적용버튼
                    }
                    else if (cbo_Lc.SelectedValue.ToString() == "002")  //후LC일때
                    {
                        btn_Apply_LC.Enabled = false;	                //L/C 적용버튼
                        btn_Apply_SO.Enabled = true;	                //수주적용버튼
                    }
                    else                                                //널일때
                    {
                        btn_Apply_LC.Enabled = false;	                //L/C 적용버튼
                        btn_Apply_SO.Enabled = false;	                //수주적용버튼
                    }
                    break;
            }
        }
        #endregion

        #region ♣ 기타이벤트

        void 금액계산()
        {
            decimal 의뢰수량 = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_flex["QT_GIR"]));
            decimal 면적2M = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_flex["QT_SQUARE"]));
            decimal 단가 = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex["UM"]));
            decimal 단가2M = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex["UM_SQUARE"]));
            decimal 환율 = Unit.환율(DataDictionaryTypes.SA, D.GetDecimal(_flex["RT_EXCH"]));

            decimal 총면적 = Unit.수량(DataDictionaryTypes.SA, 의뢰수량 * 면적2M);
            decimal 부가세율 = D.GetDecimal(_flex["RT_VAT"]) * 0.01M;
            decimal 외화금액 = decimal.Zero;
            decimal 원화금액 = decimal.Zero;
            decimal 부가세 = decimal.Zero;
            decimal 합계 = decimal.Zero;

            if (D.GetString(_flex["TP_UM_TAX"]) != "Y" || (Global.MainFrame.ServerKeyCommon != "DZSQL" && Global.MainFrame.ServerKeyCommon != "GNSD" && !is중국고객부가세포함단가사용여부))
            {
                외화금액 = Unit.외화금액(DataDictionaryTypes.SA, 단가 * 의뢰수량);
                원화금액 = Unit.원화금액(DataDictionaryTypes.SA, 외화금액 * 환율);
                부가세 = Unit.원화금액(DataDictionaryTypes.SA, 원화금액 * 부가세율);
                합계 = 원화금액 + 부가세;
                _flex["UMVAT_GIR"] = Unit.원화단가(DataDictionaryTypes.SA, 합계 / 의뢰수량);
            }
            else
            {
                합계 = Unit.원화금액(DataDictionaryTypes.SA, 의뢰수량 * D.GetDecimal(_flex["UMVAT_GIR"]));
                if (Global.MainFrame.LoginInfo.CompanyLanguage == Language.KR)
                    원화금액 = Decimal.Round(합계 * (100 / (100 + D.GetDecimal(_flex["RT_VAT"]))), MidpointRounding.AwayFromZero);
                else
                    원화금액 = D.GetDecimal(UDecimal.GetConvertFormatData(DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.INSERT, 합계 * (100 / (100 + D.GetDecimal(_flex["RT_VAT"])))));
                부가세 = 합계 - 원화금액;
                외화금액 = cur환율.DecimalValue == 0m ? 0m : Unit.외화금액(DataDictionaryTypes.SA, 원화금액 / 환율);
                _flex["UM"] = 의뢰수량 == 0m ? 0m : Unit.외화단가(DataDictionaryTypes.SA, 외화금액 / 의뢰수량);
            }

            _flex["SUM_SQUARE"] = 총면적;
            _flex["AM_GIR"] = 외화금액;
            _flex["AM_GIRAMT"] = 원화금액;
            _flex["AM_VAT"] = 부가세;
            _flex["AMT"] = 합계;

            decimal 재고단위수량 = D.GetDecimal(_flex["UNIT_SO_FACT"]) == 0 ? 1 : D.GetDecimal(_flex["UNIT_SO_FACT"]);
            _flex["QT_GIR_IM"] = Unit.수량(DataDictionaryTypes.SA, 재고단위수량 * 의뢰수량);
        }

        void Set환율()
        {
            if (_flex.HasNormalRow)
            {
                cbo환종.SelectedValue = D.GetString(_flex[_flex.Rows.Fixed, "CD_EXCH"]);
                cur환율.DecimalValue = Unit.환율(DataDictionaryTypes.SA, D.GetDecimal(_flex[_flex.Rows.Fixed, "RT_EXCH"]));
            }
            else
            {
                cbo환종.SelectedValue = "000";
                cur환율.DecimalValue = 1m;
            }
            Set환율버튼();

        }

        void Set환율버튼()
        {
            if (D.GetString(cbo환종.SelectedValue) == "000")
            {
                cur환율.DecimalValue = 1;
                cur환율.Enabled = btn환율변경.Enabled = false;
            }
            else
            {
                if (MA.기준환율.Option == MA.기준환율옵션.적용_수정불가)
                    cur환율.Enabled = false;
                else
                    cur환율.Enabled = true;

                btn환율변경.Enabled = true;
            }

            if (!_flex.HasNormalRow)
                cur환율.Enabled = btn환율변경.Enabled = false;
        }

        bool Chk환종 { get { return !Checker.IsEmpty(cbo환종, DD("환종")); } }

        bool ATP체크로직(bool 자동체크)
        {
            Duzon.ERPU.MF.Common.ATP ATP = new Duzon.ERPU.MF.Common.ATP();

            string ATP사용유무 = ATP.ATP환경설정_사용유무(LoginInfo.BizAreaCode, D.GetString(cbo_Plant.SelectedValue));
            if (ATP사용유무 == "N") return true;

            string 메뉴별ATP처리 = ATP.ATP자동체크_저장로직(D.GetString(cbo_Plant.SelectedValue), "300");
            if (메뉴별ATP처리 != "000" && 메뉴별ATP처리 != "001") return true;

            DataRow[] drs = _flex.DataTable.Select("YN_ATP = 'Y'", "", DataViewRowState.CurrentRows);

            if (drs.Length == 0) return true;

            if (drs.Length != _flex.DataTable.DefaultView.ToTable(true, new string[] { "CD_ITEM", "YN_ATP" }).Select("YN_ATP = 'Y'").Length)
            {
                if (ShowMessage("동일품목이 존재 할 경우 정확한 ATP체크를 할 수 없습니다." + Environment.NewLine + "계속 진행하시겠습니까?", "QY2") != DialogResult.Yes)
                    return false;
            }

            string s_Message = string.Empty;

            ATP.Set메뉴ID = PageID;
            ATP.Set전표번호 = txt_NoGir.Text;

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
                        if (ShowDetailMessage("납기일을 맞출 수 없습니다." + Environment.NewLine + "[︾] 버튼을 눌러 내역을 확인하세요." + Environment.NewLine + "그래도 저장하시겠습니까?", "", s_Message, "QY2") != DialogResult.Yes)
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

        void 사용자정의셋팅()
        {
            DataTable dtUserDefine = MA.GetCode("SA_B000114");

            if (dtUserDefine == null || dtUserDefine.Rows.Count == 0)
            {
                tabGir.TabPages.Remove(tabPage2);
                return;
            }

            //헤더사용자정의세팅(텍스트컨트롤)
            DataRow[] drsText = dtUserDefine.Select("CD_FLAG1 = 'TEXT'");
            for (int i = 1; i <= drsText.Length; i++)
            {
                string Name = D.GetString(drsText[i - 1]["NAME"]);

                switch (i)
                {
                    case 1:
                        lbl텍스트사용자정의1.Text = Name;
                        lbl텍스트사용자정의1.Visible = txt사용자정의1.Visible = true;
                        break;
                    case 2:
                        lbl텍스트사용자정의2.Text = Name;
                        lbl텍스트사용자정의2.Visible = txt사용자정의2.Visible = true;
                        break;
                    case 3:
                        lbl텍스트사용자정의3.Text = Name;
                        lbl텍스트사용자정의3.Visible = txt사용자정의3.Visible = true;
                        break;
                    case 4:
                        lbl텍스트사용자정의4.Text = Name;
                        lbl텍스트사용자정의4.Visible = txt사용자정의4.Visible = true;
                        break;
                }
            }
            //헤더사용자정의세팅(콤보박스컨트롤)
            SetControl str = new SetControl();
            DataRow[] drsCode = dtUserDefine.Select("CD_FLAG1 = 'CODE'");
            DataTable dtCode = null;
            DataTable dtCodeDtl = null;
            for (int i = 1; i <= drsCode.Length; i++)
            {
                string Name = D.GetString(drsCode[i - 1]["NAME"]);
                switch (i)
                {
                    case 1:
                        dtCode = MA.GetCode("SA_B000115", true);
                        dtCodeDtl = dtCode.Clone();
                        foreach (DataRow row in dtCode.Select("CODE = '' OR CD_FLAG1 = '201'"))
                            dtCodeDtl.ImportRow(row);
                        str.SetCombobox(cbo사용자정의1, dtCodeDtl);
                        lbl콤보사용자정의1.Text = Name;
                        lbl콤보사용자정의1.Visible = cbo사용자정의1.Visible = true;
                        break;
                    case 2:
                        dtCodeDtl = dtCode.Clone();
                        foreach (DataRow row in dtCode.Select("CODE = '' OR CD_FLAG1 = '202'"))
                            dtCodeDtl.ImportRow(row);
                        str.SetCombobox(cbo사용자정의2, dtCodeDtl);
                        lbl콤보사용자정의2.Text = Name;
                        lbl콤보사용자정의2.Visible = cbo사용자정의2.Visible = true;
                        break;
                    case 3:
                        dtCodeDtl = dtCode.Clone();
                        foreach (DataRow row in dtCode.Select("CODE = '' OR CD_FLAG1 = '203'"))
                            dtCodeDtl.ImportRow(row);
                        str.SetCombobox(cbo사용자정의3, dtCodeDtl);
                        lbl콤보사용자정의3.Text = Name;
                        lbl콤보사용자정의3.Visible = cbo사용자정의3.Visible = true;
                        break;
                    case 4:
                        dtCodeDtl = dtCode.Clone();
                        foreach (DataRow row in dtCode.Select("CODE = '' OR CD_FLAG1 = '204'"))
                            dtCodeDtl.ImportRow(row);
                        str.SetCombobox(cbo사용자정의4, dtCodeDtl);
                        lbl콤보사용자정의4.Text = Name;
                        lbl콤보사용자정의4.Visible = cbo사용자정의4.Visible = true;
                        break;
                }
            }
            //헤더사용자정의세팅(숫자컨트롤)
            DataRow[] drsNumber = dtUserDefine.Select("CD_FLAG1 = 'NUMBER'");
            for (int i = 1; i <= drsNumber.Length; i++)
            {
                string Name = D.GetString(drsNumber[i - 1]["NAME"]);
                switch (i)
                {
                    case 1:
                        lbl숫자사용자정의1.Text = Name;
                        lbl숫자사용자정의1.Visible = cur사용자정의1.Visible = true;
                        break;
                    case 2:
                        lbl숫자사용자정의2.Text = Name;
                        lbl숫자사용자정의2.Visible = cur사용자정의2.Visible = true;
                        break;
                }
            }
        }

        #region -> 사용자정의 라인 컬럼 캡션 셋팅

        private void 사용자정의라인셋팅(string colName, string cdField, int startIdx, int endIdx)
        {
            for (int i = startIdx; i <= endIdx; i++)
            {
                _flex.Cols[colName + D.GetString(i)].Visible = false;
            }

            DataTable dt = MA.GetCode(cdField);
            DataTable dt2 = null;
            for (int i = startIdx; (i <= dt.Rows.Count && i <= endIdx); i++)
            {
                string Name = D.GetString(dt.Rows[i - 1]["NAME"]);
                _flex.Cols[colName + D.GetString(i)].Caption = Name;

                switch (i)
                {
                    case 1:
                        dt2 = MA.GetCode("SA_B000123", true);
                        if (dt2 != null)
                        {
                            _flex.SetDataMap("L_CD_USERDEF1", dt2, "CODE", "NAME");
                            _flex.Cols[colName + D.GetString(i)].Visible = true;
                        }
                        break;
                }
            }
        }

        #endregion

        #endregion

        #region ♣ 시스템통제설정
        private void MA_EXC_SET()
        {
            if (Duzon.ERPU.MF.ComFunc.전용코드("고객납품의뢰등록-메일기능사용") == "001")
                btn메일전송.Visible = true;
        }
        #endregion

        #region ♣ 중국고객이 부가세포함단가 사용
        private bool 중국고객부가세포함단가사용여부()
        {
            DataTable dt = DBHelper.GetDataTable("SELECT FG_LANG FROM MA_COMPANY WHERE CD_COMPANY = '" + MA.Login.회사코드 + "'"); 
            bool 중국여부 = false;
            if (dt != null && dt.Rows.Count > 0) 중국여부 = D.GetString(dt.Rows[0]["FG_LANG"]) == "3";            
            return 중국여부 && Duzon.ERPU.MF.ComFunc.전용코드("수주등록-부가세 포함단가 사용여부") == "001";
        }
        #endregion

        #region ▷ 원그리드 적용하기

        void 원그리드적용하기()
        {
            System.Drawing.Size s1 = m_pnlPreForm.Size;

            m_pnlPreForm.UseCustomLayout = m_pnlPreForm_Etc.UseCustomLayout = true;
            m_pnlPreForm.IsSearchControl = false;     //ONE GRID 수정(입력 전용)
            m_pnlPreForm_Etc.IsSearchControl = false; //ONE GRID 수정(입력 전용)
            bpPanelControl1.IsNecessaryCondition = bpPanelControl2.IsNecessaryCondition = bpPanelControl3.IsNecessaryCondition = bpPanelControl5.IsNecessaryCondition = bpPanelControl7.IsNecessaryCondition = true;
            m_pnlPreForm.InitCustomLayout();
            m_pnlPreForm_Etc.InitCustomLayout();

            //tabpage resizing 하기
            System.Drawing.Size s2 = m_pnlPreForm.Size;
            System.Drawing.Size t = tabGir.Size;
            t.Height = t.Height + (s2.Height - s1.Height);
            tabGir.Size = t;

            if (m_pnlPreForm_Etc.Size.Height > t.Height)
            {
                t = m_pnlPreForm_Etc.Size;
                t.Height = m_pnlPreForm_Etc.Height + (s2.Height - s1.Height);
                m_pnlPreForm_Etc.Size = t;
            }

            return;
        }

        #endregion
    }
}
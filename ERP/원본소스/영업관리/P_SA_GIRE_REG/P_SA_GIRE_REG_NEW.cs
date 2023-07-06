using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.Common.BpControls;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using DzHelpFormLib;
using Duzon.Common.Controls;
using Duzon.ERPU.SA;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.SA.Common;

namespace sale
{
    // **************************************
    // 작   성   자 : NJin
    // 재 작  성 일 : 2009-01-23
    // 모   듈   명 : 영업
    // 시 스  템 명 : 영업관리
    // 서브시스템명 : 출하관리
    // 페 이 지  명 : 출하반품등록
    // 프로젝트  명 : P_SA_GIRE_REG_NEW => 다시 맹그럿음~캬캬
    // **************************************
    public partial class P_SA_GIRE_REG_NEW : Duzon.Common.Forms.PageBase
    {
        #region ♣ 생성자 & 변수 선언
        private P_SA_GIRE_REG_BIZ _biz = new P_SA_GIRE_REG_BIZ();

        private string MNG_LOT = Global.MainFrame.LoginInfo.MngLot; //시스템통제등록(LOT 사용여부를 가져온다.)
        private string MNG_SERIAL = string.Empty;                   //시스템통제등록 SERIAL사용여부 
        private string CD_DEPT = string.Empty;
        private string two_Unit_Mng = string.Empty;                 //영업환경설정 : 재고단위EDIT여부(2중단위관리여부) : 001
        private bool qtso_AddAllowYN = false;                       //영업환경설정 : 수주수량 초과허용 추가 2009.07.17 NJin (Default Value = false 으로 셋팅)
        private string Am_Recalc = "000";                           //영업환경설정 : 단가 및 금액 재계산 여부 Default 000 재계산, 001 재계산을 하지 않음. (분할되었을때 단가나 금액을 조정하여도 총 금액의 합이 같을 것인지 아닌지를 결정한다)

        //본죽에 전용설정위한 Flag 셋팅
        private string _전용설정_본죽 = "N";

        //FreeBinding 생성
        private FreeBinding _header = new FreeBinding();

        private 수주관리.Config 수주Config = new 수주관리.Config();

        public P_SA_GIRE_REG_NEW()
        {
            try
            {
                InitializeComponent();

                //이렇게 해주면 위에 툴바가 자동으로 움직여브러유~~~케헤헤헤
                MainGrids = new FlexGrid[] { _flexH, _flexL };

                //DetailQueryNeed 이거 사용 하려면 ~ 여기서 요거 셋팅해줘야 함~
                _flexH.DetailGrids = new FlexGrid[] { _flexL };

                //시스템통제등록 SERIAL사용여부
                object[] obj = new object[1];
                obj[0] = Global.MainFrame.LoginInfo.CompanyCode;
                MNG_SERIAL = _biz.search_SERIAL(obj);

                //영업환경설정 : 수주수량 초과허용 : 000 , 재고단위 EDIT 여부(2중단위관리 ) : 001 , 할인율 적용여부 : 002
                DataTable dt = _biz.search_EnvMng();

                if (dt.Rows.Count > 0)
                {
                    // 000:기본 100:평화 200:영우 (null이거나 ''은 000으로 치환) 
                    if (dt.Rows[0]["FG_TP"] != System.DBNull.Value && dt.Rows[0]["FG_TP"].ToString().Trim() != String.Empty)
                    {
                        two_Unit_Mng = dt.Select("FG_TP = '001'")[0]["CD_TP"].ToString(); //재고단위 EDIT 여부(2중단위관리 )
                    }
                }

                qtso_AddAllowYN = Sa_Global.Qtso_AddAllowYN;  //수주수량 초과허용 추가 2009.07.17 NJin (Default Value = false 으로 셋팅)
                Am_Recalc = Sa_Global.AM_ReCalc; //단가 및 금액 재계산 여부 Default 000 재계산, 001 재계산을 하지 않음. (분할되었을때 단가나 금액을 조정하여도 총 금액의 합이 같을 것인지 아닌지를 결정한다)

                //통제값에 따라서 그리드의 특정 셋팅이 달라지므로 통제값을 구한 후에 그리드를 셋팅해줘야 한다.
                InitGrid();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region ♣ 초기화 이벤트 / 메소드

        #region ♣ InitGrid : 그리드 초기화
        private void InitGrid()
        {
            #region ♣ _flexH
            _flexH.BeginSetting(1, 1, false);
            _flexH.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            _flexH.SetCol("NO_GIR", "의뢰번호", 100);
            _flexH.SetCol("CD_PLANT", "공장", 80);
            _flexH.SetCol("NM_PLANT", "공장명", 120);
            _flexH.SetCol("CD_PARTNER", "거래처", 80);
            _flexH.SetCol("LN_PARTNER", "거래처명", 120);
            _flexH.SetCol("DT_GIR", "의뢰일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexH.SetCol("NO_GI_EMP", "반품의뢰자", 80);
            _flexH.SetCol("NM_KOR", "반품의뢰자명", 100);
            _flexH.SetCol("DC_RMK", "비고", 200);
            _flexH.SettingVersion = "3.3.3.3";

            _flexH.ExtendLastCol = true;
            _flexH.EnabledHeaderCheck = true;
            _flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flexH.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
            _flexH.CheckHeaderClick += new EventHandler(_flex_CheckHeaderClick);
            _flexH.ValidateEdit += new ValidateEditEventHandler(_flex_ValidateEdit);


            #endregion

            #region ♣ _flexL
            _flexL.BeginSetting(1, 1, false);
            _flexL.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            _flexL.SetCol("CD_ITEM", "품목코드", 80);
            _flexL.SetCol("NM_ITEM", "품목명", 120);
            _flexL.SetCol("STND_ITEM", "규격", 80);
            _flexL.SetCol("FG_TRANS", "거래구분", 80);
            _flexL.SetCol("CD_SL", "창고코드", 80, true);
            _flexL.SetCol("NM_SL", "창고명", 120);
            _flexL.SetCol("NM_QTIOTP", "출하형태", 100);
            _flexL.SetCol("FG_TAX", "과세구분", 100);
            _flexL.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("QT_GIR_IM", "관리수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("QT_UNIT_MM", "출하수량", 90, true, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("DT_DUEDATE", "납기일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            //재고단위EDIT여부(2중단위관리여부) 에 따라서 출하관리수량을 Edit 가능 여부를 체크한다.
            if (two_Unit_Mng == "N")
                _flexL.SetCol("QT_IO", "출하관리수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            else
                _flexL.SetCol("QT_IO", "출하관리수량", 90, true, typeof(decimal), FormatTpType.QUANTITY);

            _flexL.SetCol("NO_PROJECT", "프로젝트코드", 80, false);
            _flexL.SetCol("NM_PROJECT", "프로젝트명", 80, false);
            _flexL.SetCol("UNIT", "단위", 80);
            _flexL.SetCol("CD_UNIT_MM", "관리단위", 80);
            _flexL.SetCol("NM_SALEGRP", "영업그룹", 80, false);

            if (_biz.Get출하반품_검사 == "100")
            {
                _flexL.SetCol("YN_INSPECT", "검사여부", 50, false, CheckTypeEnum.Y_N);
                _flexL.SetCol("QT_QC_PASS", "검사합격수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
                _flexL.SetCol("QT_QC_BAD", "검사불량수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
                _flexL.SetCol("QT_GR_PASS", "양품입고수량", 90, 17, true, typeof(decimal), FormatTpType.QUANTITY);
                _flexL.SetCol("QT_GR_BAD", "불량입고수량", 90, 17, true, typeof(decimal), FormatTpType.QUANTITY);
            }

            //원 출고번호 추가(2011/01/20, BY SMJUNG)
            _flexL.SetCol("NO_IO_MGMT", "원 출고번호", 100, false);
            _flexL.SetCol("DC_RMK", "비고", 150, false);

            if (Duzon.ERPU.MF.ComFunc.전용코드("출하등록-단가금액통제") == "N")
            {
                _flexL.SetCol("UM_EX_PSO", "단가", 90, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                _flexL.SetCol("AM_EX", "외화금액", 90, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                _flexL.SetCol("AM", "원화금액", 90, false, typeof(decimal), FormatTpType.MONEY);
                _flexL.SetCol("AM_VAT", "부가세", 100, false, typeof(decimal), FormatTpType.MONEY);
                _flexL.SetCol("AMT", "부가세포함금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            }

            if (수주Config.부가세포함단가사용())
            {
                _flexL.SetCol("TP_UM_TAX", "부가세여부", 90, false);
                _flexL.SetCol("UMVAT_GI", "부가세포함단가", 100, false, typeof(decimal), FormatTpType.UNIT_COST);
            }

            if (Config.MA_ENV.YN_UNIT == "Y")
            {
                _flexL.SetCol("SEQ_PROJECT", "UNIT 항번", 100, false, typeof(decimal));
                _flexL.SetCol("CD_UNIT", "UNIT 코드", 100, false);
                _flexL.SetCol("NM_UNIT", "UNIT 명", 100, false);
                _flexL.SetCol("STND_UNIT", "UNIT 규격", 100, false);
            }

            if (App.SystemEnv.PROJECT사용)
                _flexL.VerifyNotNull = new string[] { "NO_PROJECT" };

            _flexL.EnabledHeaderCheck = true;
            _flexL.SettingVersion = "3.3.3.3";
            _flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.Top);
            _flexL.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
            _flexL.BeforeCodeHelp += new BeforeCodeHelpEventHandler(_flex_BeforeCodeHelp);
            _flexL.CheckHeaderClick += new EventHandler(_flex_CheckHeaderClick);
            _flexL.ValidateEdit += new ValidateEditEventHandler(_flex_ValidateEdit);
            _flexL.SetCodeHelpCol("CD_SL", Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CD_SL", "NM_SL" }, new string[] { "CD_SL", "NM_SL" });
            _flexL.StartEdit += new RowColEventHandler(_flexL_StartEdit);
            _flexL.DoubleClick += new EventHandler(_flexL_DoubleClick);
            
            if (Config.MA_ENV.YN_UNIT == "Y")
                _flexL.SetExceptSumCol("UM_EX_PSO", "SEQ_PROJECT");
            else
                _flexL.SetExceptSumCol("UM_EX_PSO");
            #endregion
        }
        
        #endregion

        #region ♣ InitPaint : 프리폼 초기화
        //PageBase 의 부모함수 이용 : 폼이 올라올 후 폼위에 올라온 컨트롤들을 초기화 시켜주는 역할을 한다.
        //Combo Box 셋팅시 옵션 => N:공백없음, S:공백추가, SC:공백&괄호추가, NC:괄호추가
        //뒤의 코드값들은 DB 에서 정해놓은 파라미터와 매치시켜줘야한다
        protected override void InitPaint()
        {


            oneGrid1.UseCustomLayout = oneGrid2.UseCustomLayout = oneGrid3.UseCustomLayout = true;
            bpPanelControl1.IsNecessaryCondition = bpPanelControl11.IsNecessaryCondition = bpPanelControl12.IsNecessaryCondition = bpPanelControl13.IsNecessaryCondition = true;
            oneGrid1.InitCustomLayout();
            oneGrid2.InitCustomLayout();
            oneGrid3.InitCustomLayout();

            DataSet g_dsCombo = this.GetComboData("N;MA_PLANT", "S;PU_C000016", "N;MA_B000040");

            // 공장 콤보
            cbo공장.DataSource = g_dsCombo.Tables[0];
            cbo공장.DisplayMember = "NAME";
            cbo공장.ValueMember = "CODE";

            cbo공장.SelectedValue = Global.MainFrame.LoginInfo.CdPlant;

            //의뢰일자, 납기일자 콤보박스를 위한 데이터 테이블 생성
            DataTable dt = g_dsCombo.Tables[0].Clone();

            DataRow dr = dt.NewRow();
            dr["CODE"] = "GI";
            //dr["NAME"] = "의뢰일자";
            dr["NAME"] = DD("의뢰일자");
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["CODE"] = "DU";
            //dr["NAME"] = "납기일자";
            dr["NAME"] = DD("납기일자");
            dt.Rows.Add(dr);

            cbo날짜구분.DataSource = dt;
            cbo날짜구분.DisplayMember = "NAME";
            cbo날짜구분.ValueMember = "CODE";


            //거래구분
            _flexL.SetDataMap("FG_TRANS", g_dsCombo.Tables[1], "CODE", "NAME");
            //과세구분
            _flexL.SetDataMap("FG_TAX", g_dsCombo.Tables[2], "CODE", "NAME");

            DataTable dt_exc_본죽 = _biz.거래처거래처부가정보영업그룹수주형태적용();

            if (dt_exc_본죽.Rows.Count > 0)
            {
                if (dt_exc_본죽.Rows[0]["CD_EXC"] != System.DBNull.Value && dt_exc_본죽.Rows[0]["CD_EXC"].ToString().Trim() != "")
                {
                    string 전용설정_본죽 = dt_exc_본죽.Rows[0]["CD_EXC"].ToString().Trim();

                    if (전용설정_본죽 == "000")
                        _전용설정_본죽 = "N";
                    else if (전용설정_본죽 == "001")
                        _전용설정_본죽 = "Y";
                }
            }

            if (_전용설정_본죽 == "Y")  //거래처부가정보의 거래처와영업그룹매핑을 할 경우
            {
                bp영업그룹.Enabled = false;
            }

            // 조회조건 초기화
            pp일자.StartDateToString = Global.MainFrame.GetStringFirstDayInMonth;
            //dp종료일.Text = Global.MainFrame.GetStringToday;
            bp거래처.CodeValue = string.Empty;
            bp거래처.CodeName = string.Empty;
            bp납품처.CodeValue = string.Empty;
            bp납품처.CodeName = string.Empty;
            bp출하형태.CodeValue = string.Empty;
            bp출하형태.CodeName = string.Empty;
            rdo미처리.Checked = true;
            rdo처리.Checked = false;
            bp반품의뢰자.CodeValue = string.Empty;
            bp반품의뢰자.CodeName = string.Empty;
            bp영업그룹.CodeValue = string.Empty;
            bp영업그룹.CodeName = string.Empty;

            //등록조건 초기화
            dp출하일자.Text = Global.MainFrame.GetStringToday;
            //부서를 Default 로 주어야 하기 때문에 처음에 Login 한 담당자를 Default 셋팅한 다음 그에 해당하는 부서를 셋팅한당.
            CD_DEPT = Global.MainFrame.LoginInfo.DeptCode;
            bp담당자.CodeValue = Global.MainFrame.LoginInfo.EmployeeNo;
            bp담당자.CodeName = Global.MainFrame.LoginInfo.EmployeeName;
            bp창고.CodeValue = string.Empty;
            bp창고.CodeName = string.Empty;
            txt비고.Text = string.Empty;

            if (Settings.Default.auto_No == "GIR")
            {
                rdo의뢰번호별.Checked = true;
                rdo거래처.Checked = false;
            }
            else if (Settings.Default.auto_No == "PARTNER")
            {
                rdo의뢰번호별.Checked = false;
                rdo거래처.Checked = true;
            }

            // 상태값 초기화
            txt하단납품처.Text = string.Empty;
            txt하단수주번호.Text = string.Empty;
            txt하단LC번호.Text = string.Empty;

            // 버튼 활성 비활성
            //btn_apply.Enabled = false;          //창고적용
            btn양품적용.Enabled = false;     //양품적용

            if (_biz.Get출하반품_검사 == "100")
                btn불량적용.Visible = true;

            object[] obj = new object[] { "", "", "", "", "", "", "", "", "", "" };

            DataSet ds = _biz.Search(obj);

            _header.SetBinding(ds.Tables[0], oneGrid2);
            _header.ClearAndNewRow();
            _flexH.Binding = ds.Tables[1];
        }
        #endregion

        #endregion

        //#region ♣ 삭제버튼과 인쇄버튼은 이화면에서 필요 없음으로 무조건 비활성한다.
        //private void Auth()
        //{
        //    ToolBarDeleteButtonEnabled = false;
        //    ToolBarPrintButtonEnabled = false;
        //}
        //#endregion

        #region ♣ 필수입력 체크
        /// <summary>
        /// 필수입력 항목에 Null 체크해주는 함수
        /// 아래의 NUllCheck() 메소드가 리턴값을 Bool 형태로 반환합니다.
        /// </summary>
        /// <returns></returns>
        private bool FieldCheck(string Flag)
        {
            Hashtable hList = new Hashtable();

            if (Flag == "SEARCH")
            {
                //날자 구분이 레이블이 아니므로 이렇게 상속받아서 처리함
               // LabelExt lbl = new LabelExt();
                LabelExt lbl2 = new LabelExt();

                //lbl.Text = "조회일";

                //hList.Add(dp시작일, lbl);
                //hList.Add(dp종료일, lbl);
                hList.Add(cbo공장, lbl공장);
            }
            else if (Flag == "SAVE")
            {
                hList.Add(dp출하일자, lbl출하일자);
                hList.Add(bp담당자, lbl담당자);
            }

            return ComFunc.NullCheck(hList);
        }
        #endregion

        #region ♣ 조회버튼 클릭
        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!FieldCheck("SEARCH") || !Chk일자) return;

                //object[] obj = new object[13];

                //obj[0] = LoginInfo.CompanyCode;
                //obj[1] = string.Empty;
                //obj[2] = dt_Gir_From.Text;
                //obj[3] = dt_Gir_To.Text;
                //obj[4] = cbo_Plant.SelectedValue == null ? string.Empty : cbo_Plant.SelectedValue.ToString();
                //obj[5] = bp_Partner.CodeValue;
                //obj[6] = bp_GiPartner.CodeValue;
                //obj[7] = bp_TpGi.CodeValue;
                //obj[8] = "Y";           //처리상태가 미처리 상태 즉, 반품여부가 "Y" 인것
                //obj[9] = string.Empty;  //반품에서는 조회조건에 운송방법이 없음
                //obj[10] = bp_GiEmp.CodeValue;
                //obj[11] = cbo_DtGubun.SelectedValue == null ? string.Empty : cbo_DtGubun.SelectedValue.ToString();
                //          //출하등록에서 의뢰일자와 납품일자 조회 구분을 위한 헤더 조회 구분값 - 의뢰일자 : GI, 납품일자 : DU
                //obj[12] = bp_SaleGrp.CodeValue;

                object[] obj = new object[] {
                    LoginInfo.CompanyCode, 
                    pp일자.StartDateToString, 
                    pp일자.EndDateToString, 
                    D.GetString(cbo공장.SelectedValue), 
                    D.GetString(bp거래처.CodeValue), 
                    D.GetString(bp납품처.CodeValue), 
                    D.GetString(bp출하형태.CodeValue), 
                    D.GetString(bp반품의뢰자.CodeValue), 
                    D.GetString(cbo날짜구분.SelectedValue), 
                    D.GetString(bp영업그룹.CodeValue)
                };

                DataSet ds = _biz.Search(obj);

                _flexH.Binding = ds.Tables[1];

                btn양품적용.Enabled = _flexH.HasNormalRow;

                if (!_flexH.HasNormalRow)
                    ShowMessage(PageResultMode.SearchNoData);// 검색된 내용이 존재하지 않습니다..   
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region ♣ 추가버튼 클릭
        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                //초기화 : 조회조건, 등록조건, 헤더그리드, 라인그리드 모두 초기화 된다.
                InitPaint();

                //dp시작일.Focus();

                pp일자.Focus();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region ♣ 저장버튼 클릭
        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                _flexL.Focus(); // 저장 시점에 그리드의 특정셀에 포커스가 가 있는경우 인식해주기 위해~

                if (!FieldCheck("SAVE")) return;

                //Grid 무결성 및 null 체크
                if (!Verify()) return;

                //라인 입력 체크 : Verify() 이거 속도가 너무 느린 경우가 있어서 그냥 화면단에서 구현~
                DataRow[] line_check_Rows = _flexL.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);
                if (line_check_Rows == null || line_check_Rows.Length == 0) return;
                foreach (DataRow drl in line_check_Rows)
                {
                    if (drl.RowState == DataRowState.Deleted) continue;

                    if (drl["CD_SL"].ToString() == string.Empty)
                    {
                        this.ShowMessage(" 창고는 필수입력항목입니다. \n\n 창고를 확인하세요.");
                        return;
                    }

                    if (_flexL.CDecimal(drl["QT_UNIT_MM"]) <= 0)
                    {
                        ShowMessage(공통메세지._은_보다커야합니다, DD("출하반품수량"), "0");
                        _flexL.Select(_flexL.Rows.Fixed, "QT_UNIT_MM");
                        _flexL.Focus();
                        return;
                    }
                }

                if (SaveData())
                {
                    this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
                    OnToolBarSearchButtonClicked(sender, e);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            try
            {
                #region 이중단위 Exception 처리
                //출하수량(QT_UNIT_MM) * 환산수량(UNIT_SO_FACT) 이 관리수량(QT_IO) 과 불일치할 경우 체크로직을 걸어준다.
                DataTable dt_TwoEx = _flexL.DataTable.Clone();

                DataRow[] cnt_Rows = _flexL.DataTable.Select("S = 'Y' AND ISNULL(QT_IO, 0) <> ISNULL(QT_UNIT_MM, 0) * ISNULL(UNIT_SO_FACT, 1)", "", DataViewRowState.CurrentRows);
                if (cnt_Rows != null && cnt_Rows.Length != 0)
                {
                    //재고단위EDIT여부(2중단위관리여부) 에 따라서 출하관리수량을 Edit 가능 여부를 체크한다.
                    if (two_Unit_Mng == "N")
                        ShowMessage("수배수량과 관리수량이 일치하지 않아서 저장할 수 없습니다.");

                    foreach (DataRow dr_TwoUnitQty in cnt_Rows)
                    {
                        dt_TwoEx.ImportRow(dr_TwoUnitQty);
                    }

                    P_SA_TWO_UNIT_EX_SUB ddlg = new P_SA_TWO_UNIT_EX_SUB(dt_TwoEx, two_Unit_Mng);

                    if (ddlg.ShowDialog() == DialogResult.OK)
                    {
                        dt_TwoEx.Rows.Clear();

                        if (ddlg.returnDt != null && ddlg.returnDt.Rows.Count != 0)
                        {
                            foreach (DataRow dr in ddlg.returnDt.Rows)
                            {
                                dt_TwoEx.ImportRow(dr);
                            }
                        }
                    }
                    else
                        return false;
                }

                if (dt_TwoEx != null && dt_TwoEx.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt_TwoEx.Rows)
                    {
                        foreach (DataRow dr_L in _flexL.DataTable.Rows)
                        {
                            // 의뢰번호와 의뢰항번으로 
                            if(dr["NO_ISURCV"].ToString() == dr_L["NO_ISURCV"].ToString() && D.GetDecimal(dr["NO_ISURCVLINE"].ToString()) == D.GetDecimal(dr_L["NO_ISURCVLINE"].ToString()))
                                dr_L["S"] = "N";

                            // 라인의 체크가 모두 해제 되었는지 확인해서 모두 체크가 풀렸으면 헤더의 체크도 풀어준다.
                            DataRow[] drs_ex = _flexL.DataTable.Select("S = 'Y' AND NO_ISURCV = '" + dr["NO_ISURCV"].ToString() + "'");
                            if (drs_ex == null || drs_ex.Length == 0)
                            {
                                DataRow[] drs_exh = _flexH.DataTable.Select("S = 'Y' AND NO_ISURCV = '" + dr["NO_ISURCV"].ToString() + "'");
                                if (dr["NO_ISURCV"].ToString() == drs_exh[0]["NO_ISURCV"].ToString())
                                    drs_exh[0]["S"] = "N";
                            }
                        }
                    }
                }
                #endregion

                //헤더와 라인의 체크된 수량을 가져온다. 
                //헤더의 체크수량을 파악하여 1인경우에는 LOT 여부에 상관없이 저장 가능하다. 
                //라인의 체크수량과 LOT 여부를 파악한 수량을 가져와서 헤더가 2이상인 경우에는 EXCEPTION 처리를 한다. 
                DataRow[] cnt_Header = _flexH.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);
                DataRow[] cnt_Line_lot = _flexL.DataTable.Select("S = 'Y' AND NO_LOT = 'YES'", "", DataViewRowState.CurrentRows);
                DataRow[] cnt_Line_serial = _flexL.DataTable.Select("S = 'Y' AND NO_SERL = 'YES'", "", DataViewRowState.CurrentRows);

                if (cnt_Header == null || cnt_Header.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return false;
                }
                else if (MNG_LOT == "Y" && cnt_Header.Length > 1 && (cnt_Line_lot == null || cnt_Line_lot.Length > 0))
                {
                    ShowMessage(" 여러개의 의뢰번호에 해당하는 LOT품목이 여러건이 존재하여 \n\n 일괄 작업을 수행할 수 없습니다. \n\n 한건씩 처리하시기 바랍니다.");
                    return false;
                }
                else if (MNG_SERIAL == "Y" && cnt_Header.Length > 1 && (cnt_Line_serial == null || cnt_Line_serial.Length > 0))
                {
                    ShowMessage(" 여러개의 의뢰번호에 해당하는 SERIAL품목이 여러건이 존재하여 \n\n 일괄 작업을 수행할 수 없습니다. \n\n 한건씩 처리하시기 바랍니다.");
                    return false;
                }

                DataTable dt_H = null, dt_L = null, dt_LOT = null, dt_SERIAL = null, dt_TEMP = null;
                dt_H = _flexH.DataTable.Clone();
                dt_L = _flexL.DataTable.Clone();
                string NO_IO = string.Empty;

                foreach (DataRow drh_cnt in cnt_Header)
                {
                    DataRow[] header_Row = null;

                    if (rdo의뢰번호별.Checked == true)
                        header_Row = dt_H.Select("S = 'Y' AND NO_GIR = '" + drh_cnt["NO_GIR"].ToString() + "'", "", DataViewRowState.CurrentRows);
                    else
                        header_Row = dt_H.Select("S = 'Y' AND CD_PARTNER = '" + drh_cnt["CD_PARTNER"].ToString() + "'", "", DataViewRowState.CurrentRows);

                    #region 헤더에 데이터 채우기
                    if (header_Row.Length == 0)
                    {
                        //AUTO CREATE NO !! 
                        NO_IO = GetSeq(LoginInfo.CompanyCode, "SA", "09", dp출하일자.Text.Substring(0, 6)).ToString();//출하반품번호

                        #region ♣ 라인에 데이터 채우기

                        decimal line = 1;
                        DataRow[] line_Row = null;

                        if (rdo의뢰번호별.Checked == true)
                            line_Row = _flexL.DataTable.Select("S = 'Y' AND NO_GIR = '" + drh_cnt["NO_GIR"].ToString() + "'", "", DataViewRowState.CurrentRows);
                        else
                            line_Row = _flexL.DataTable.Select("S = 'Y' AND CD_PARTNER = '" + drh_cnt["CD_PARTNER"].ToString() + "'", "", DataViewRowState.CurrentRows);

                        foreach (DataRow dl in line_Row)
                        {
                            dl["NO_IO"] = NO_IO;
                            dl["NO_IOLINE"] = line++;
                            dl["DT_IO"] = dp출하일자.Text;
                            dl["FG_IO"] = "041";

                            dt_L.ImportRow(dl);
                        }

                        #endregion

                        //라인 먼저 데이터 채우고 헤더에 데이터를 채우는 이유는 라인에 있는값이 헤더에 저장되는 것들이 있기에~ 내말 뭔말인지 알쮜~ㅋㅋ
                        //원래 출하의뢰를 조회해서 가져올 사항에 데이터는 없지만 조회쿼리에 쓰레기 값을 가져오는 이유는 스키마 생성해서 멀티저장 바로 가능하게 하기위함
                        //멀티저장시에 다른 것들은 코딩상에서 스키마를 추가해서 보여줬으나 쿼리에 쓰레기값 가져와서 스키마 만들어주는 방법도 괜찮은듯...
                        drh_cnt["NO_IO"] = NO_IO;
                        drh_cnt["FG_TRANS"] = line_Row[0]["FG_TRANS"].ToString();
                        drh_cnt["NO_EMP"] = bp담당자.CodeValue;
                        drh_cnt["CD_DEPT"] = CD_DEPT;
                        drh_cnt["DT_IO"] = dp출하일자.Text;
                        drh_cnt["YN_RETURN"] = "Y";

                        if (!chk_YN.Checked)
                            drh_cnt["DC_RMK"] = txt비고.Text;

                        dt_H.ImportRow(drh_cnt);

                        if (NO_IO == string.Empty)
                        {
                            ShowMessage("출하반품번호가 존재하지 않습니다.");
                            return false;
                        }
                    }
                    #endregion
                }

                #region LOT / SERIAL 적용

                #region LOT 적용
                //MNG_LOT : 전역변수로 회사의 LOT 사용여부를 읽어옴
                if (String.Compare(MNG_LOT, "Y") == 0 && dt_L != null)
                {
                    DataRow[] DR = dt_L.Select("NO_LOT = 'YES'");

                    if (DR.Length > 0)
                    {
                        //Lot 품목인 것만 적용해주기 위해서 이런 삽질 해놨는뎅~ 풉~ 허접하기 이를데 없군요~
                        dt_TEMP = dt_L.Clone();

                        foreach (DataRow du in DR)
                        {
                            dt_TEMP.Rows.Add(du.ItemArray);
                        }
                        dt_LOT = dt_TEMP.Copy();

                        //
                        // LOT번호 자동입력- 한국코와 (2015.02.16 이명철 황승선)
                        //-수주, 의뢰, 반품이 1:1:1 이고, 
                        //-SA_SOL에 NO_LOT 컬럼이 존재(수주등록전용사용)
                        //
                        if (Global.MainFrame.ServerKeyCommon.Contains("KOWA"))
                        {
                            DataTable dtLOT_KOWA = new DataTable();
                            dtLOT_KOWA = _biz.dtLot_Schema(dtLOT_KOWA);

                            string NO_LOT_KOWA = string.Empty;

                            int i = 0;
                            foreach (DataRow dr in dt_LOT.Rows)
                            {
                                NO_LOT_KOWA = _biz.search_LOT_KOWA(D.GetString(dr["NO_PSO_MGMT"]), D.GetDecimal(dr["NO_PSOLINE_MGMT"]));

                                dtLOT_KOWA.Rows.Add();

                                dtLOT_KOWA.Rows[i]["NO_IO"] = D.GetString(dr["NO_IO"]);
                                dtLOT_KOWA.Rows[i]["NO_IOLINE"] = D.GetString(dr["NO_IOLINE"]);
                                dtLOT_KOWA.Rows[i]["NO_IOLINE2"] = 0;
                                dtLOT_KOWA.Rows[i]["NO_LOT"] = NO_LOT_KOWA;
                                dtLOT_KOWA.Rows[i]["CD_ITEM"] = D.GetString(dr["CD_ITEM"]);
                                dtLOT_KOWA.Rows[i]["DT_IO"] = dp출하일자.Text;
                                dtLOT_KOWA.Rows[i]["FG_PS"] = "1";
                                dtLOT_KOWA.Rows[i]["FG_IO"] = "041";
                                dtLOT_KOWA.Rows[i]["CD_QTIOTP"] = D.GetString(dr["CD_QTIOTP"]);
                                dtLOT_KOWA.Rows[i]["CD_SL"] = D.GetString(dr["CD_SL"]);
                                dtLOT_KOWA.Rows[i]["QT_IO"] = D.GetString(dr["QT_IO"]);

                                i++;
                            }

                            dt_LOT = dtLOT_KOWA;
                        }
                        else
                        {
                            pur.P_PU_LOT_SUB_R m_dlg = new pur.P_PU_LOT_SUB_R(dt_LOT);

                            if (m_dlg.ShowDialog(this) == DialogResult.OK)
                                dt_LOT = m_dlg.dtL;
                            else
                                return false;
                        }
                    }
                }
                #endregion

                #region SERIAL 적용
                //MNG_SERIAL : 전역변수로 회사의 SERIAL 사용여부를 읽어옴
                if (String.Compare(MNG_SERIAL, "Y") == 0 && dt_L != null)
                {
                    DataRow[] DR = dt_L.Select("NO_SERL = 'YES'");

                    if (DR.Length > 0)
                    {
                        //SERIAL 품목인 것만 적용해주기 위해서 이런 삽질 해놨는뎅~ 풉~ 허접하기 이를데 없군요~
                        dt_TEMP = dt_L.Clone();

                        foreach (DataRow du in DR)
                        {
                            dt_TEMP.Rows.Add(du.ItemArray);
                        }
                        dt_SERIAL = dt_TEMP.Copy();

                        pur.P_PU_SERL_SUB_R m_dlg = new pur.P_PU_SERL_SUB_R(dt_SERIAL);

                        if (m_dlg.ShowDialog(this) == DialogResult.OK)
                            dt_SERIAL = m_dlg.dtL;
                        else
                            return false;
                    }
                }
                #endregion

                #endregion

                #region LOT에 저장되는 정확한 수량 체크 로직 검사
                //위 검사는 LOT 품목이 수불이 일어남에도 LOT 품목 수량이 0 으로 저장되는 경우가 발생하여 여기에 체크로직을 걸어준다.
                DataRow[] dr_grid = _flexL.DataTable.Select("S = 'Y' AND NO_LOT = 'YES'", "", DataViewRowState.CurrentRows);//수불 라인에 저장되는 LOT 품목 추출

                if (MNG_LOT == "Y" && dr_grid.Length != 0 && (dt_LOT == null || dt_LOT.Rows.Count == 0))
                {
                    ShowMessage("LOT 품목 수불이 발생하였으나 해당 LOT가 생성되지 않았습니다.");
                    return false;
                }

                if (dt_LOT != null)
                {
                    #region LOT 테이블은 수불테이불과 1:n 관계이므로 LOT 테이블의 수량은 LOT테이블의 항번의 sum(수량) 으로 수불 수량과 비교해야 한다.
                    DataTable temp_Lot = dt_LOT.Clone();
                    foreach (DataRow temp_dr in dt_LOT.Rows)
                    {
                        DataRow[] temp_drs = null;

                        temp_drs = temp_Lot.Select("NO_IO = '" + temp_dr["NO_IO"].ToString() + "' AND NO_IOLINE = '" + _flexL.CDecimal(temp_dr["NO_IOLINE"]) + "'");

                        if (temp_drs.Length == 0)
                        {
                            DataRow temp_row = temp_Lot.NewRow();

                            //그리드의 채번된 첫행
                            temp_row["NO_IO"] = temp_dr["NO_IO"].ToString();
                            temp_row["NO_IOLINE"] = _flexL.CDecimal(temp_dr["NO_IOLINE"]);
                            temp_row["CD_ITEM"] = temp_dr["CD_ITEM"];
                            temp_row["QT_IO"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(temp_dr["QT_IO"]));

                            temp_Lot.Rows.Add(temp_row);
                        }
                        else
                        {
                            temp_drs[0]["QT_IO"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(temp_drs[0]["QT_IO"]) + D.GetDecimal(temp_dr["QT_IO"]));
                        }
                    }
                    #endregion

                    int cnt_item = 0; //LOT항번과 수불항번이 같은데 품목이 다를경우 EXCEPTION 처리한다.
                    int cnt_Lot = 0; //LOT수량과 수불수량이 다를경우 EXCEPTION 처리한다.
                    foreach (DataRow dr in dr_grid)
                    {
                        foreach (DataRow dr_lot in temp_Lot.Rows)
                        {
                            if (dr["NO_IO"].ToString() == dr_lot["NO_IO"].ToString() && dr["NO_IOLINE"].ToString() == dr_lot["NO_IOLINE"].ToString())
                            {
                                //LOT항번과 수불항번이 같은데 품목이 다른것이 걸릴경우 EXCEPTION 처리
                                if (dr["CD_ITEM"].ToString().Trim() != dr_lot["CD_ITEM"].ToString().Trim())
                                    cnt_item++;

                                if (dr["CD_ITEM"].ToString() == dr_lot["CD_ITEM"].ToString() && _flexL.CDecimal(dr["QT_IO"]) != _flexL.CDecimal(dr_lot["QT_IO"]))
                                    cnt_Lot++;
                            }
                        }
                    }

                    if (cnt_item > 0)
                    {
                        ShowMessage("LOT항번과 수불항번이 일치하지 않거나 품목이 일치하지 않습니다.");
                        return false;
                    }

                    if (cnt_Lot > 0)
                    {
                        ShowMessage("LOT수량과 수불수량이 일치하지 않습니다.");
                        return false;
                    }
                }
                #endregion 

                #region -> LOCATION 등록
                DataTable dt_Location = null;
                if (Config.MA_ENV.YN_LOCATION == "Y")
                {
                    bool b_lct = false;
                    DataTable dt_lc = dt_L.Clone();

                    foreach (DataRow dr in dt_L.Select())
                        dt_lc.LoadDataRow(dr.ItemArray, true);

                    if (dt_lc.Rows.Count > 0)
                    {
                        dt_Location = P_OPEN_SUBWINDOWS.P_MA_LOCATION_I_SUB(dt_lc, out b_lct);
                        if (!b_lct) return false;
                    }
                }
                #endregion 

                DataTable dt_FG_TRANS_CHK = dt_L.DefaultView.ToTable(true, new string[] { "NO_GIR", "FG_TRANS" });
                foreach (DataRow dr in dt_FG_TRANS_CHK.Rows)
                {
                    DataRow[] drs_FG_TRANS_CHK = dt_FG_TRANS_CHK.Select("NO_GIR = '" + D.GetString(dr["NO_GIR"]) + "' AND FG_TRANS <> '" + D.GetString(dr["FG_TRANS"]) + "' ");
                    if (drs_FG_TRANS_CHK.Length > 0)
                    {
                        ShowMessage("의뢰번호[" + D.GetString(dr["NO_GIR"]) + "]의 라인데이터에 거래구분(FG_TRANS)가 다른건이 존재합니다.");
                        return false;
                    }
                }

                bool bSuccess = _biz.Save(dt_H, dt_L, dt_LOT, dt_SERIAL, dt_Location);
                if (!bSuccess) return false;

                _flexH.AcceptChanges();
                _flexL.AcceptChanges();

                return true;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return false;
        }
        #endregion

        #region ♣ 양품적용
        private void btn_apply_good_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow[] ldrchk = _flexL.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                //******************************************************
                // 2011-04-22, 최승애
                // 선택된 자료가 없을때 메세지 처리함.
                //******************************************************

                if (ldrchk.Length < 1)
                {

                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                //decimal rowUnit합계 = 0;
                //decimal rowIO합계 = 0;

                //LOT 품목이 없을 수 있음으로 체크하면 안됨
                if (ldrchk != null && ldrchk.Length > 0)
                {
                    _flexL.Redraw = false;

                    foreach (DataRow row in ldrchk)
                    {
                        if (row["YN_INSPECT"].ToString() == "Y")
                        {
                            row["QT_GR_PASS"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row["QT_QC_PASS"]));
                            row["QT_UNIT_MM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row["QT_GR_PASS"]));
                            row["QT_IO"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row["QT_GR_PASS"]));
                            row["QT_GR_BAD"] = 0m;
                        }
                        else
                        {
                            row["QT_UNIT_MM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row["QT_GIR"]));
                            row["QT_IO"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row["QT_GIR_IM"]));
                        }

                        row["QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row["QT_IO"]));     //양품재고
                        //이미출하된 수량을 체크하여 수량이 0보다 클경우 부가세 재계산 하고 0일경우에는 의뢰 부가세를 그대로 적용(2010.03.04) 
                        if (D.GetDecimal(row["CHK_QT_GI"]) != 0)
                        {

                            //001재계산함.
                            if (Am_Recalc == "000")
                            {
                                //재계산
                                row["AM_EX"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(row["UM_EX_PSO"]) * D.GetDecimal(row["QT_UNIT_MM"]));
                                row["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_EX"]) * D.GetDecimal(row["RT_EXCH"]));

                                //row["VAT"] = row["AM_VAT"];   //부가세

                                if (D.GetDecimal(row["QT_IO"]) == 0m)
                                {
                                    row["UM_EX"] = 0m;
                                    row["UM"] = 0m;
                                }
                                else
                                {
                                    row["UM_EX"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row["UM_EX_PSO"]));
                                    row["UM"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(row["UM_EX_PSO"]) * D.GetDecimal(row["RT_EXCH"]));
                                }

                                //row["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM"]) * (D.GetDecimal(row["RT_VAT"]) / 100));
                            }
                            else
                            {
                                //재계산안함.
                                row["AM_EX"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(row["ORG_AM_EX"]));//A.AM_GIR - A.AM_EXGI
                                row["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["ORG_AM"]));//A.AM_GIRAMT - A.AM_GI

                                row["UM_EX"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row["UM_EX_PSO"]));
                                row["UM"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row["UM_EX_PSO"]) * D.GetDecimal(row["RT_EXCH"]));

                                //row["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_VAT"]));   //부가세
                            }

                            row["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM"]) * (D.GetDecimal(row["RT_VAT"]) / 100));   //부가세

                            if (Duzon.ERPU.MF.ComFunc.전용코드("출하등록-단가금액통제") == "N")
                            {
                                row["AM_VAT"] = row["VAT"];
                                row["AMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM"]) + D.GetDecimal(row["AM_VAT"]));
                            }
                            //if (Am_Recalc == "000")
                            //{
                            //    //재계산
                            //    //row["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM"]) * (D.GetDecimal(row["RT_VAT"]) / 100));   //부가세

                            //    if (D.GetDecimal(row["QT_IO"]) == 0)
                            //    {
                            //        row["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_EX"]) * D.GetDecimal(row["RT_EXCH"]));
                            //        row["UM_EX"] = 0m;
                            //        row["UM"] = 0m;
                            //    }
                            //    else
                            //    {
                            //        row["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_EX"]) * D.GetDecimal(row["RT_EXCH"]));
                            //        row["UM_EX"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row["AM_EX"]) / D.GetDecimal(row["QT_IO"]));
                            //        row["UM"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(row["AM"]) / D.GetDecimal(row["QT_IO"]));
                            //    }
                            //}
                            //else
                            //{
                            //    //재계산 하지 않는다.
                            //    if (D.GetDecimal(row["QT_IO"]) == 0)
                            //    {
                            //        row["UM_EX"] = 0m;
                            //        row["UM"] = 0m;
                            //    }
                            //    else
                            //    {
                            //        row["UM_EX"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row["UM_EX_PSO"]));
                            //        row["UM"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(row["UM_EX_PSO"]) * D.GetDecimal(row["RT_EXCH"]));
                            //    }

                            //    //row["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_VAT"]));   //부가세
                            //}

                            //row["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM"]) * (D.GetDecimal(row["RT_VAT"]) / 100));   //부가세
                        }
                        else
                        {
                            //001재계산함.
                            if (Am_Recalc == "000" ||
                                (Am_Recalc == "001" && Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row["QT_QC_PASS"])) != 0m && Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row["QT_QC_PASS"])) != Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row["QT_GIR"])))
                                )
                            {
                                //재계산
                                row["AM_EX"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(row["UM_EX_PSO"]) * D.GetDecimal(row["QT_UNIT_MM"]));
                                row["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_EX"]) * D.GetDecimal(row["RT_EXCH"]));

                                //row["VAT"] = row["AM_VAT"];   //부가세

                                if (D.GetDecimal(row["QT_IO"]) == 0m)
                                {
                                    row["UM_EX"] = 0m;
                                    row["UM"] = 0m;
                                }
                                else
                                {
                                    row["UM_EX"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row["UM_EX_PSO"]));
                                    row["UM"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(row["UM_EX_PSO"]) * D.GetDecimal(row["RT_EXCH"]));
                                }

                                row["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM"]) * (D.GetDecimal(row["RT_VAT"]) / 100));

                                if (Duzon.ERPU.MF.ComFunc.전용코드("출하등록-단가금액통제") == "N")
                                {
                                    row["AM_VAT"] = row["VAT"];
                                    row["AMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM"]) + D.GetDecimal(row["AM_VAT"]));
                                }
                            }
                            else
                            {
                                //재계산안함.
                                row["AM_EX"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(row["ORG_AM_EX"]));//A.AM_GIR - A.AM_EXGI
                                row["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["ORG_AM"]));//A.AM_GIRAMT - A.AM_GI

                                row["UM_EX"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row["UM_EX_PSO"]));
                                row["UM"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row["UM_EX_PSO"]) * D.GetDecimal(row["RT_EXCH"]));

                                row["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_VAT"]));   //부가세
                                
                                if (Duzon.ERPU.MF.ComFunc.전용코드("출하등록-단가금액통제") == "N")
                                {
                                    row["AM_VAT"] = row["VAT"];
                                    row["AMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM"]) + D.GetDecimal(row["AM_VAT"]));
                                }
                            }
                        }
                        //rowUnit합계 += _flexL.CDecimal(row["QT_GOOD_INV"]);
                        //rowIO합계 += _flexL.CDecimal(row["QT_IO"]);

                        //_flexL[1, "QT_UNIT_MM"] = rowUnit합계.ToString(_flexL.Cols["QT_UNIT_MM"].Format);
                        //_flexL[1, "QT_IO"] = rowIO합계.ToString(_flexL.Cols["QT_IO"].Format);
                    }

                    _flexL.Redraw = true;

                    _flexL.SumRefresh();    // 2011-05-04, 최승애
                }

                #region 이전 소스
                ////LOT 품목이 없을 수 있음으로 체크하면 안됨
                //if (ldrchk != null && ldrchk.Length > 0)
                //{
                //    foreach (DataRow row in ldrchk)
                //    {
                //        row["QT_UNIT_MM"] = row["QT_GIR"];
                //        row["QT_IO"] = row["QT_GIR_IM"];
                //        row["QT_GOOD_INV"] = row["QT_IO"];     //양품재고

                //        //이미출하된 수량을 체크하여 수량이 0보다 클경우 부가세 재계산 하고 0일경우에는 의뢰 부가세를 그대로 적용(2010.03.04) 
                //        if (D.GetDecimal(row["CHK_QT_GI"]) != 0)
                //        {
                //            //재계산 해야한다.
                //            row["VAT"] = Decimal.Truncate(D.GetDecimal(row["AM"]) * (D.GetDecimal(row["RT_VAT"]) / 100));   //부가세

                //            if (D.GetDecimal(row["QT_IO"]) == 0)
                //            {
                //                row["AM"] = Decimal.Truncate(D.GetDecimal(row["AM_EX"]) * D.GetDecimal(row["RT_EXCH"]));
                //                row["UM_EX"] = 0;
                //                row["UM"] = 0;
                //            }
                //            else
                //            {
                //                row["AM"] = Decimal.Truncate(D.GetDecimal(row["AM_EX"]) * D.GetDecimal(row["RT_EXCH"]));
                //                row["UM_EX"] = D.GetDecimal(row["AM_EX"]) / D.GetDecimal(row["QT_IO"]);
                //                row["UM"] = D.GetDecimal(row["AM"]) / D.GetDecimal(row["QT_IO"]);
                //            }
                //        }
                //        else
                //        {
                //            //재계산 하지 않는다.
                //            row["VAT"] = row["AM_VAT"];   //부가세

                //            if (D.GetDecimal(row["QT_IO"]) == 0)
                //            {
                //                row["UM_EX"] = 0;
                //                row["UM"] = 0;
                //            }
                //            else
                //            {
                //                row["UM_EX"] = row["UM_EX_PSO"];
                //                row["UM"] = D.GetDecimal(row["UM_EX_PSO"]) * D.GetDecimal(row["RT_EXCH"]);
                //            }
                //        }

                //        rowUnit합계 += _flexL.CDecimal(row["QT_GOOD_INV"]);
                //        rowIO합계 += _flexL.CDecimal(row["QT_IO"]);

                //        _flexL[1, "QT_UNIT_MM"] = rowUnit합계.ToString(_flexL.Cols["QT_UNIT_MM"].Format);
                //        _flexL[1, "QT_IO"] = rowIO합계.ToString(_flexL.Cols["QT_IO"].Format);
                //    }
                //}
                #endregion

                ShowMessage(공통메세지._작업을완료하였습니다, btn양품적용.Text);
            }
            catch (Exception ex)
            {
                _flexL.Redraw = true;
                MsgEnd(ex);
            }
            finally
            {
                _flexL.Redraw = true;
            }
        }
        #endregion

        #region ♣ 불량적용
        private void btn_apply_bad_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow[] ldrchk = _flexL.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                //******************************************************
                // 2011-04-22, 최승애
                // 선택된 자료가 없을때 메세지 처리함.
                //******************************************************

                if (ldrchk.Length < 1)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                //****************************************************** 2011-04-22, 최승애

                DataRow[] 불량체크필수 = _flexL.DataTable.Select("S = 'Y' AND YN_INSPECT = 'N'", "", DataViewRowState.CurrentRows);

                if (불량체크필수 != null && 불량체크필수.Length > 0)
                {
                    //MessageBox.Show("검사품목이 아닌 건은 불량 적용이 불가합니다.");
                    ShowMessage("검사품목이 아닌 건은 불량 적용이 불가합니다.");
                    return;
                }

                //decimal rowUnit합계 = 0;
                //decimal rowIO합계 = 0;

                //LOT 품목이 없을 수 있음으로 체크하면 안됨
                if (ldrchk != null && ldrchk.Length > 0)
                {
                    foreach (DataRow row in ldrchk)
                    {
                        if (row["YN_INSPECT"].ToString() == "Y")
                        {
                            row["QT_GR_BAD"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row["QT_QC_BAD"]));
                            row["QT_UNIT_MM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row["QT_GR_BAD"]));
                            row["QT_IO"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row["QT_GR_BAD"]));
                            row["QT_BAD_INV"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row["QT_IO"]));
                            row["QT_GR_PASS"] = 0m;
                        }
                        else
                        {
                            row["QT_UNIT_MM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row["QT_GIR"]));
                            row["QT_IO"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row["QT_GIR_IM"]));
                        }
                        
                        row["QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row["QT_IO"]));     //불량재고

                        //이미출하된 수량을 체크하여 수량이 0보다 클경우 부가세 재계산 하고 0일경우에는 의뢰 부가세를 그대로 적용(2010.03.04) 
                        if (D.GetDecimal(row["CHK_QT_GI"]) != 0)
                        {

                            //001재계산함.
                            if (Am_Recalc == "000")
                            {
                                //재계산
                                row["AM_EX"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(row["UM_EX_PSO"]) * D.GetDecimal(row["QT_IO"]));
                                row["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_EX"]) * D.GetDecimal(row["RT_EXCH"]));

                                //row["VAT"] = row["AM_VAT"];   //부가세

                                if (D.GetDecimal(row["QT_IO"]) == 0m)
                                {
                                    row["UM_EX"] = 0m;
                                    row["UM"] = 0m;
                                }
                                else
                                {
                                    row["UM_EX"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row["UM_EX_PSO"]));
                                    row["UM"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(row["UM_EX_PSO"]) * D.GetDecimal(row["RT_EXCH"]));
                                }

                                //row["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM"]) * (D.GetDecimal(row["RT_VAT"]) / 100));
                            }
                            else
                            {
                                //재계산안함.
                                row["AM_EX"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(row["ORG_AM_EX"]));//A.AM_GIR - A.AM_EXGI
                                row["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["ORG_AM"]));//A.AM_GIRAMT - A.AM_GI

                                row["UM_EX"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row["UM_EX_PSO"]));
                                row["UM"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row["UM_EX_PSO"]) * D.GetDecimal(row["RT_EXCH"]));

                                //row["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_VAT"]));   //부가세
                            }

                            row["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM"]) * (D.GetDecimal(row["RT_VAT"]) / 100));   //부가세
                           
                            if (Duzon.ERPU.MF.ComFunc.전용코드("출하등록-단가금액통제") == "N")
                            {
                                row["AM_VAT"] = row["VAT"];
                                row["AMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM"]) + D.GetDecimal(row["AM_VAT"]));
                            }
                        }
                        else
                        {
                            //001재계산함.
                            if (Am_Recalc == "000" ||
                                (Am_Recalc == "001" && Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row["QT_QC_BAD"])) != 0m && Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row["QT_QC_BAD"])) != Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row["QT_GIR"])))
                                )
                            {
                                //재계산
                                row["AM_EX"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(row["UM_EX_PSO"]) * D.GetDecimal(row["QT_IO"]));
                                row["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_EX"]) * D.GetDecimal(row["RT_EXCH"]));

                                //row["VAT"] = row["AM_VAT"];   //부가세

                                if (D.GetDecimal(row["QT_IO"]) == 0m)
                                {
                                    row["UM_EX"] = 0m;
                                    row["UM"] = 0m;
                                }
                                else
                                {
                                    row["UM_EX"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row["UM_EX_PSO"]));
                                    row["UM"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(row["UM_EX_PSO"]) * D.GetDecimal(row["RT_EXCH"]));
                                }

                                row["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM"]) * (D.GetDecimal(row["RT_VAT"]) / 100));
                                
                                if (Duzon.ERPU.MF.ComFunc.전용코드("출하등록-단가금액통제") == "N")
                                {
                                    row["AM_VAT"] = row["VAT"];
                                    row["AMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM"]) + D.GetDecimal(row["AM_VAT"]));
                                }
                            }
                            else
                            {
                                //재계산안함.
                                row["AM_EX"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(row["ORG_AM_EX"]));//A.AM_GIR - A.AM_EXGI
                                row["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["ORG_AM"]));//A.AM_GIRAMT - A.AM_GI

                                row["UM_EX"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row["UM_EX_PSO"]));
                                row["UM"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row["UM_EX_PSO"]) * D.GetDecimal(row["RT_EXCH"]));

                                row["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_VAT"]));   //부가세

                                if (Duzon.ERPU.MF.ComFunc.전용코드("출하등록-단가금액통제") == "N")
                                {
                                    row["AM_VAT"] = row["VAT"];
                                    row["AMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM"]) + D.GetDecimal(row["AM_VAT"]));
                                }
                            }
                        }

                        ////이미출하된 수량을 체크하여 수량이 0보다 클경우 부가세 재계산 하고 0일경우에는 의뢰 부가세를 그대로 적용(2010.03.04) 
                        //if (D.GetDecimal(row["CHK_QT_GI"]) != 0)
                        //{
                        //    if (Am_Recalc == "000")
                        //    {
                        //        //재계산
                        //        //row["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM"]) * (D.GetDecimal(row["RT_VAT"]) / 100));   //부가세

                        //        if (D.GetDecimal(row["QT_IO"]) == 0m)
                        //        {
                        //            row["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_EX"]) * D.GetDecimal(row["RT_EXCH"]));
                        //            row["UM_EX"] = 0m;
                        //            row["UM"] = 0m;
                        //        }
                        //        else
                        //        {
                        //            row["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_EX"]) * D.GetDecimal(row["RT_EXCH"]));
                        //            row["UM_EX"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row["AM_EX"]) / D.GetDecimal(row["QT_IO"]));
                        //            row["UM"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(row["AM"]) / D.GetDecimal(row["QT_IO"]));
                        //        }
                        //    }
                        //    else
                        //    {
                        //        //재계산 하지 않는다.
                        //        if (D.GetDecimal(row["QT_IO"]) == 0)
                        //        {
                        //            row["UM_EX"] = 0m;
                        //            row["UM"] = 0m;
                        //        }
                        //        else
                        //        {
                        //            row["UM_EX"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row["UM_EX_PSO"]));
                        //            row["UM"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(row["UM_EX_PSO"]) * D.GetDecimal(row["RT_EXCH"]));
                        //        }

                        //        //row["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_VAT"]));   //부가세
                        //    }

                        //    row["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM"]) * (D.GetDecimal(row["RT_VAT"]) / 100));   //부가세
                        //}
                        //else
                        //{
                        //    //001재계산함.
                        //    if (Am_Recalc == "000" ||
                        //        (Am_Recalc == "001" &&
                        //        Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row["QT_QC_BAD"])) != Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row["QT_GIR"])))
                        //        )
                        //    {
                        //        //재계산
                        //        row["AM_EX"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(row["UM_EX_PSO"]) * D.GetDecimal(row["QT_IO"]));
                        //        row["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_EX"]) * D.GetDecimal(row["RT_EXCH"]));

                        //        //row["VAT"] = row["AM_VAT"];   //부가세

                        //        if (D.GetDecimal(row["QT_IO"]) == 0m)
                        //        {
                        //            row["UM_EX"] = 0m;
                        //            row["UM"] = 0m;
                        //        }
                        //        else
                        //        {
                        //            row["UM_EX"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row["UM_EX_PSO"]));
                        //            row["UM"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(row["UM_EX_PSO"]) * D.GetDecimal(row["RT_EXCH"]));
                        //        }

                        //        row["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM"]) * (D.GetDecimal(row["RT_VAT"]) / 100));
                        //    }
                        //    else
                        //    {
                        //        //재계산안함.
                        //        row["AM_EX"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(row["ORG_AM_EX"]));//A.AM_GIR - A.AM_EXGI
                        //        row["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["ORG_AM"]));//A.AM_GIRAMT - A.AM_GI

                        //        row["UM_EX"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row["UM_EX_PSO"]));
                        //        row["UM"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(row["UM_EX_PSO"]) * D.GetDecimal(row["RT_EXCH"]));

                        //        row["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_VAT"]));   //부가세
                        //    }
                        //}

                        //rowUnit합계 += _flexL.CDecimal(row["QT_GOOD_INV"]);
                        //rowIO합계 += _flexL.CDecimal(row["QT_IO"]);

                        //_flexL[1, "QT_UNIT_MM"] = rowUnit합계.ToString(_flexL.Cols["QT_UNIT_MM"].Format);
                        //_flexL[1, "QT_IO"] = rowIO합계.ToString(_flexL.Cols["QT_IO"].Format);
                    }

                    _flexL.SumRefresh();            //2011-05-04, 최승애
                }

                ShowMessage(공통메세지._작업을완료하였습니다, btn불량적용.Text);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            } 
        }
        #endregion

        #region ♣ 도움창 셋팅 Event

        #region -> 도움창 Clear 이벤트(OnBpControl_CodeChanged)

        private void OnBpControl_CodeChanged(object sender, EventArgs e)
        {
            try
            {
                BpCodeTextBox bpControl = sender as BpCodeTextBox;
                if (bpControl == null) return;

                if (bpControl.CodeValue.Trim() != "") return;

                switch (bpControl.Name)
                {
                    case "bp_Partner":
                        if (_전용설정_본죽 == "Y")
                        {
                            bp영업그룹.CodeValue = "";
                            bp영업그룹.CodeName = "";
                            //_header.CurrentRow["CD_SALEGRP"] = "";
                            //_header.CurrentRow["NM_SALEGRP"] = "";
                        }
                        break;
                    case "bp_CDSL": //창고적용
                        //if (bp_CDSL.CodeValue.Trim() == "")
                        //    btn_apply.Enabled = false;
                        //else
                        //    btn_apply.Enabled = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ♣ Control_QueryBefore
        private void Control_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            switch (e.HelpID)
            {
                case Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB:
                    e.HelpParam.P09_CD_PLANT = cbo공장.SelectedValue == null ? string.Empty : cbo공장.SelectedValue.ToString();
                    break;
                case Duzon.Common.Forms.Help.HelpID.P_PU_EJTP_SUB:
                    e.HelpParam.P61_CODE1 = "010|041|042|";
                    break;
            }
        }
        #endregion

        #region ♣ Control_QueryAfter
        private void Control_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                switch (((Control)sender).Name)
                {
                    case "bp_Emp":
                        //출하 담당자가 바뀌면 해당 부서도 바뀌게~ ^^V
                        CD_DEPT = e.HelpReturn.Rows[0]["CD_DEPT"].ToString();
                        break;
                    case "bp_Partner":
                        if (_전용설정_본죽 == "Y")
                        {
                            DataRow row거래처부가 = BASIC.GetPartner(bp거래처.CodeValue);
                            bp영업그룹.CodeValue = row거래처부가["CD_SALEGRP"].ToString();
                            bp영업그룹.CodeName = row거래처부가["NM_SALEGRP"].ToString();
                            //_header.CurrentRow["CD_SALEGRP"] = row거래처부가["CD_SALEGRP"].ToString();
                            //_header.CurrentRow["NM_SALEGRP"] = row거래처부가["NM_SALEGRP"].ToString();
                        }
                        break;
                    case "bp_CDSL": //창고적용
                        //if (bp_CDSL.CodeValue.Trim() == "")
                        //    btn_apply.Enabled = false;
                        //else
                        //    btn_apply.Enabled = true;
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

        #region ♣ 창고 일괄 적용
        private void btn_apply_Click(object sender, EventArgs e)
        {
            try
            {
                if (Checker.IsEmpty(bp창고, lbl창고.Text, true)) return;

                if (_flexL.DataTable == null)   //기존에 있는 코드라 그냥 남겨 놓는다.(기존 사용자 혼란 방지)
                {
                    ShowMessage(DD("조회 후 적용 하시기 바랍니다."));
                    return;
                }

                if (!_flexL.HasNormalRow)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                //모든 선택된 의뢰번호에 대해서 창고를 일괄변경한다.
                //DataRow[] ldrchk = _flexL.DataTable.Select("S = 'Y' AND NO_GIR = '" + _flexH[_flexH.Row, "NO_GIR"].ToString() + "'", "", DataViewRowState.CurrentRows);
                DataRow[] ldrchk = _flexL.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                if (ldrchk == null || ldrchk.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                _flexL.Redraw = false;
                foreach (DataRow row in ldrchk)
                {
                    row["CD_SL"] = bp창고.CodeValue;
                    row["NM_SL"] = bp창고.CodeName;
                }
                _flexL.Redraw = true;

                ShowMessage(공통메세지._작업을완료하였습니다, btn창고적용.Text);
            }
            catch (Exception ex)
            {
                _flexL.Redraw = true;
                MsgEnd(ex);
            }
            finally
            {
                _flexL.Redraw = true;
            }
        }
        #endregion

        //#region ♣ ComboBox_KeyEvent
        //private void CommonComboBox_KeyEvent(object sender, System.Windows.Forms.KeyEventArgs e)
        //{
        //    if (e.KeyData == Keys.Enter)
        //        System.Windows.Forms.SendKeys.SendWait("{TAB}");
        //}
        //#endregion

        #region ♣ 사용자 Default Settings
        //원래는 화면이 닫힐때 사용자가 정의한 값을 읽어서 저장해야하지만 
        //화면이 닫히는 시점을 알기 어려움으로 해당 컨트롤에 변경이 일어 났을때 바로 저장 시킨다.
        public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
        {
            if (rdo의뢰번호별.Checked == true)
                Settings.Default.auto_No = "GIR";

            if (rdo거래처.Checked == true)
                Settings.Default.auto_No = "PARTNER";

            //꼭 셋팅해놓은 값을 저장하자.
            Settings.Default.Save();

            return base.OnToolBarExitButtonClicked(sender, e);
        }
        #endregion

        #region ♣ 처리상태 : 처리 일때 드릴다운 기능
        private void rdo_Yes_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdo처리.Checked == true)
                {
                    object[] args = new Object[6];

                    args[0] = dp출하일자.Text;                         //출하일자
                    args[1] = cbo공장.SelectedValue == null ? string.Empty : cbo공장.SelectedValue.ToString(); //공장
                    args[2] = bp거래처.CodeValue;               //거래처코드
                    args[3] = bp거래처.CodeName;                //거래처명
                    args[4] = bp담당자.CodeValue;                   //사번
                    args[5] = bp담당자.CodeName;                    //이름

                    //Main 이 살아 있는지 확인한후 살아 있으면 저장을 실행하고 죽어 있으면 그냥 리턴시켜버린다.
                    if (this.MainFrameInterface.IsExistPage("P_SA_GIM_REG", false))
                        this.UnLoadPage("P_SA_GIM_REG", false);   //특정 페이지 닫기

                    string ls_LinePageName = DD("출하관리");
                    bool isComplete = this.LoadPageFrom("P_SA_GIM_REG", ls_LinePageName, this.Grant, args);
                    if (!isComplete) ShowMessage(공통메세지.작업을정상적으로처리하지못했습니다);

                    rdo처리.Checked = false;
                    rdo미처리.Checked = true;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region ♣ 공장이 바뀌면 다시 조회하기
        private void cbo_Plant_SelectionChangeCommitted(object sender, EventArgs e)
        {
            OnToolBarSearchButtonClicked(sender, e);
        }
        #endregion

        #region ♣ Grid Event

        #region ♣ _flex_AfterRowChange
        private void _flex_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                FlexGrid flex = sender as FlexGrid;

                switch (flex.Name)
                {
                    case "_flexH":
                        DataTable dt = null;
                        string Key = _flexH[e.NewRange.r1, "NO_GIR"].ToString();
                        string Filter = "NO_GIR = '" + Key + "'";

                        if (_flexH.DetailQueryNeed)
                        {
                            object[] obj = new object[] {
                                LoginInfo.CompanyCode, 
                                pp일자.StartDateToString, 
                                pp일자.EndDateToString, 
                                D.GetString(cbo공장.SelectedValue), 
                                D.GetString(bp거래처.CodeValue), 
                                D.GetString(bp납품처.CodeValue), 
                                D.GetString(bp출하형태.CodeValue), 
                                D.GetString(bp반품의뢰자.CodeValue), 
                                D.GetString(cbo날짜구분.SelectedValue), 
                                D.GetString(bp영업그룹.CodeValue), 
                                Key + "|"
                            };

                            dt = _biz.SearchDetail(obj);
                        }
                        _flexL.BindingAdd(dt, Filter);
                        _flexL.SetDummyColumn("S", "CD_SL", "NM_SL", "QT_UNIT_MM", "QT_IO");
                        _flexH.DetailQueryNeed = false;
                        break;
                    case "_flexL":
                        txt하단납품처.Text = _flexL[_flexL.Row, "LN_PARTNER"].ToString();	//납품처
                        txt하단수주번호.Text = _flexL[_flexL.Row, "NO_PSO_MGMT"].ToString();		//수주번호
                        txt하단LC번호.Text = _flexL[_flexL.Row, "NO_LC"].ToString();				//LC번호
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region ♣ _flex_BeforeCodeHelp
        //Grid 도움창 셋팅
        private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                e.Parameter.P09_CD_PLANT = _flexL[_flexL.Row, "CD_PLANT"].ToString();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region ♣ _flex_CheckHeaderClick
        private void _flex_CheckHeaderClick(object sender, EventArgs e)
        {
            try
            {

                FlexGrid flex = sender as FlexGrid;

                switch (flex.Name)
                {
                    case "_flexH":  //상단 그리드 Header Click 이벤트

                        //데이타 테이블의 체크값을 변경해주어도 화면에 보이는 값을 변경시키지 못하므로 화면의 값도 같이 변경시켜줌
                        _flexH.Row = 1; // 맨처음row에 자동위치하도록 해야 틀어지지 않는다.

                        if (!_flexH.HasNormalRow) return;

                        MsgControl.ShowMsg(DD("데이타를 조회중입니다."));

                        if (D.GetString(_flexH["S"]) == "Y")
                        {
                            string strNO_GIRS = "";

                            for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
                            {
                                if (_flexH.DetailQueryNeedByRow(i))
                                {
                                    strNO_GIRS += D.GetString(_flexH.Rows[i]["NO_GIR"]) + "|";
                                }
                            }

                            DataTable dt = null;

                            if (strNO_GIRS != "")
                            {
                                object[] obj = new object[] {
                                    LoginInfo.CompanyCode, 
                                    pp일자.StartDateToString, 
                                    pp일자.EndDateToString, 
                                    D.GetString(cbo공장.SelectedValue), 
                                    D.GetString(bp거래처.CodeValue), 
                                    D.GetString(bp납품처.CodeValue), 
                                    D.GetString(bp출하형태.CodeValue), 
                                    D.GetString(bp반품의뢰자.CodeValue), 
                                    D.GetString(cbo날짜구분.SelectedValue), 
                                    D.GetString(bp영업그룹.CodeValue), 
                                    strNO_GIRS
                                };

                                dt = _biz.SearchDetail(obj);
                            }

                            string Filter = "NO_GIR = '" + D.GetString(_flexH["NO_GIR"]) + "'";
                            _flexL.BindingAdd(dt, Filter);

                            for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
                                if (_flexH.DetailQueryNeedByRow(i))
                                    _flexH.SetDetailQueryNeedByRow(i, false);

                            foreach (DataRow dr in _flexL.DataTable.Rows) dr["S"] = "Y";
                            for (int i = _flexL.Rows.Fixed; i < _flexL.Rows.Count; i++)
                                _flexL.SetCellCheck(i, _flexL.Cols["S"].Index, CheckEnum.Checked);
                        }
                        else
                        {
                            foreach (DataRow dr in _flexL.DataTable.Rows) dr["S"] = "N";
                            for (int i = _flexL.Rows.Fixed; i < _flexL.Rows.Count; i++)
                                _flexL.SetCellCheck(i, _flexL.Cols["S"].Index, CheckEnum.Unchecked);
                        }

                        //for (int h = 0; h < _flexH.Rows.Count - 1; h++)
                        //{
                        //    _flexH.Row = h + 1; //Row가 순차적으로 변경되면서 RowChange() 이벤트를 자동 실행하게 유도한다.

                        //    for (int i = _flexL.Rows.Fixed; i < _flexL.Rows.Count; i++)
                        //    {
                        //        _flexL[i, "S"] = _flexH["S"].ToString();
                        //    }

                        //    //하단 데이타데이블의 모든 체크값을 변경해줌
                        //    foreach (DataRow dr in _flexL.DataTable.Rows)
                        //    {
                        //        dr["S"] = _flexH["S"].ToString();
                        //    }
                        //}

                        break;

                    case "_flexL":  //하단 그리드 Header Click 이벤트

                        if (!_flexL.HasNormalRow) return;

                        _flexH["S"] = _flexL["S"].ToString();

                        break;
                }

                DataRow[] idx_Row = flex.DataTable.Select("S = 'Y'");

                if (idx_Row.Length > 0)
                    ToolBarSaveButtonEnabled = true;
                else
                    ToolBarSaveButtonEnabled = false;
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
        #endregion

        #region ♣ _flex_ValidateEdit
        private void _flex_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                FlexGrid flex = sender as FlexGrid;

                switch (flex.Name)
                {
                    case "_flexH":  //상단 그리드 Header Click 이벤트

                        if (!_flexH.HasNormalRow) return;

                        switch (_flexH.Cols[e.Col].Name)
                        {
                            case "S":
                                _flexH["S"] = e.Checkbox == CheckEnum.Checked ? "Y" : "N";

                                for (int i = _flexH.Rows.Fixed; i <= _flexL.DataView.Count; i++)
                                {
                                    _flexL[i + 1, "S"] = e.Checkbox == CheckEnum.Checked ? "Y" : "N";
                                }
                                break;
                        }
                        break;
                    case "_flexL":  //하단 그리드 Header Click 이벤트

                        string oldValue = ((FlexGrid)sender).GetData(e.Row, e.Col).ToString();
                        string newValue = ((FlexGrid)sender).EditData;

                        if (oldValue.ToUpper() == newValue.ToUpper()) return;

                        if (!_flexL.HasNormalRow) return;

                        switch (_flexL.Cols[e.Col].Name)
                        {
                            case "S":

                                _flexL["S"] = e.Checkbox == CheckEnum.Checked ? "Y" : "N";

                                DataRow[] drArr = _flexL.DataView.ToTable().Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                                if (drArr.Length != 0)
                                    _flexH.SetCellCheck(_flexH.Row, _flexH.Cols["S"].Index, CheckEnum.Checked);
                                else
                                    _flexH.SetCellCheck(_flexH.Row, _flexH.Cols["S"].Index, CheckEnum.Unchecked);
                                break;

                            case "QT_GR_PASS":
                            case "QT_GR_BAD":
                            case "QT_UNIT_MM":

                                #region ♣ 출하 수량 선택하면 체크박스 자동으로 선택되게 하기
                                if (_flexL.CDecimal(newValue) == 0)
                                    _flexL.SetCellCheck(_flexL.Row, _flexL.Cols["S"].Index, CheckEnum.Unchecked);
                                else
                                    _flexL.SetCellCheck(_flexL.Row, _flexL.Cols["S"].Index, CheckEnum.Checked);

                                DataRow[] drArrr = _flexL.DataView.ToTable().Select("S = 'Y'", "", DataViewRowState.CurrentRows);
                                if (drArrr.Length != 0)
                                    _flexH.SetCellCheck(_flexH.Row, _flexH.Cols["S"].Index, CheckEnum.Checked);
                                else
                                    _flexH.SetCellCheck(_flexH.Row, _flexH.Cols["S"].Index, CheckEnum.Unchecked);
                                #endregion

                                // qtso_AddAllowYN = false 일 경우 수주(의뢰) 수량이 초과하려는 경우 수주(의뢰) 수량으로 맞춘다.
                                // qtso_AddAllowYN = true  일 경우 수주(의뢰) 수량이 초과허용 가능하다.
                                decimal qtso_AddAllow = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_flexL["QT_GIR"]) + (D.GetDecimal(_flexL["QT_GIR"]) * (D.GetDecimal(_flexL["RT_PLUS"]) / 100)));
                                
                                if (_flexL["YN_INSPECT"].ToString() == "Y")
                                {
                                    if (_flexL.Cols[e.Col].Name == "QT_GR_PASS")
                                    {
                                        _flexL["QT_GR_BAD"] = 0m;
                                    }
                                    else if (_flexL.Cols[e.Col].Name == "QT_GR_BAD")
                                    {
                                        _flexL["QT_GR_PASS"] = 0m;
                                    }

                                    if (D.GetDecimal(_flexL["QT_GIR"]) >= ( D.GetDecimal(_flexL["QT_GR_PASS"]) + D.GetDecimal(_flexL["QT_GR_BAD"])))
                                    {
                                        if (_flexL.Cols[e.Col].Name == "QT_GR_PASS")
                                        {
                                            _flexL["QT_UNIT_MM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_flexL["QT_GR_PASS"]));
                                        }
                                        else if (_flexL.Cols[e.Col].Name == "QT_GR_BAD")
                                        {
                                            _flexL["QT_UNIT_MM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_flexL["QT_GR_BAD"]));
                                        }
                                    }
                                    else
                                    {
                                         _flexL["QT_UNIT_MM"] = 0m;
                                         _flexL["QT_GR_PASS"] = 0m;
                                         _flexL["QT_GR_BAD"] = 0m;
                                        //MessageBox.Show("출하수량이 의뢰수량을 초과하였습니다.");
                                        ShowMessage("출하수량이 의뢰수량을 초과하였습니다.");
                                    }
                                }
                                else
                                {
                                    if (!qtso_AddAllowYN && D.GetDecimal(_flexL["QT_GIR"]) < D.GetDecimal(newValue))
                                    {
                                        _flexL["QT_UNIT_MM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_flexL["QT_GIR"]));
                                        newValue = _flexL["QT_GIR"].ToString();     // 이부분이 안 해주면 아래 의뢰 수량과 비교할때 같지 않아서 금액 부분이 틀어진다.
                                    }
                                    else if (qtso_AddAllowYN)
                                    {
                                        if (qtso_AddAllow < D.GetDecimal(newValue))
                                        {
                                            _flexL["QT_UNIT_MM"] = 0m;
                                            newValue = "0";     // 이부분이 안 해주면 아래 의뢰 수량과 비교할때 같지 않아서 금액 부분이 틀어진다.
                                            //MessageBox.Show("출하수량이 의뢰수량을 초과하였습니다.");
                                            ShowMessage("출하수량이 의뢰수량을 초과하였습니다.");
                                        }
                                    }
                                }
                                if (D.GetDecimal(_flexL["UNIT_SO_FACT"]) == 0m)
                                    _flexL["QT_IO"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_flexL["QT_UNIT_MM"]));
                                else
                                    _flexL["QT_IO"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_flexL["QT_UNIT_MM"]) * D.GetDecimal(_flexL["UNIT_SO_FACT"]));

                                _flexL["QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_flexL["QT_IO"]));

                                if (D.GetDecimal(newValue) != D.GetDecimal(_flexL["QT_GIR"]))
                                {
                                    //수량 수정시는 무조건 재계산
                                    //재계산
                                    _flexL["AM_EX"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL["UM_EX_PSO"]) * D.GetDecimal(newValue));
                                    _flexL["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL["AM_EX"]) * D.GetDecimal(_flexL["RT_EXCH"]));
                                }
                                else
                                {
                                    if (Am_Recalc == "000")
                                    {
                                        //재계산
                                        _flexL["AM_EX"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL["UM_EX_PSO"]) * D.GetDecimal(newValue));
                                        _flexL["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL["AM_EX"]) * D.GetDecimal(_flexL["RT_EXCH"]));
                                    }
                                    else
                                    {
                                        //재계산안함.
                                        _flexL["AM_EX"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL["ORG_AM_EX"]));//A.AM_GIR - A.AM_EXGI
                                        _flexL["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL["ORG_AM"]));//A.AM_GIRAMT - A.AM_GI
                                    }
                                }

                                _flexL["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL["AM"]) * (D.GetDecimal(_flexL["RT_VAT"]) / 100));

                                if (Duzon.ERPU.MF.ComFunc.전용코드("출하등록-단가금액통제") == "N")
                                {
                                    _flexL["AM_VAT"] = _flexL["VAT"];
                                    _flexL["AMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL["AM"]) + D.GetDecimal(_flexL["AM_VAT"]));
                                }

                                if (D.GetDecimal(_flexL["QT_IO"]) == 0m)
                                {
                                    _flexL["UM_EX"] = 0m;
                                    _flexL["UM"] = 0m;
                                }
                                else
                                {
                                    if (Am_Recalc == "000")
                                    {
                                        //재계산
                                        _flexL["UM_EX"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flexL["AM_EX"]) / D.GetDecimal(_flexL["QT_IO"]));
                                        _flexL["UM"] = Unit.원화단가(DataDictionaryTypes.SA, (D.GetDecimal(_flexL["AM_EX"]) * D.GetDecimal(_flexL["RT_EXCH"])) / D.GetDecimal(_flexL["QT_IO"]));
                                    }
                                    else
                                    {
                                        //재계산안함.
                                        _flexL["UM_EX"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flexL["UM_EX_PSO"]));
                                        _flexL["UM"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(_flexL["UM_EX_PSO"]) * D.GetDecimal(_flexL["RT_EXCH"]));
                                    }
                                }

                                #region 이전소스
                            //if (D.GetDecimal(newValue) != D.GetDecimal(_flexL["QT_GIR"]))
                                //{
                                //    _flexL["AM_EX"] = D.GetDecimal(_flexL["UM_EX_PSO"]) * D.GetDecimal(newValue);
                                //    _flexL["AM"] = Decimal.Truncate(D.GetDecimal(_flexL["AM_EX"]) * D.GetDecimal(_flexL["RT_EXCH"]));
                                //}

                                //_flexL["VAT"] = Decimal.Truncate(D.GetDecimal(_flexL["AM"]) * (D.GetDecimal(_flexL["RT_VAT"]) / 100));

                                //if (D.GetDecimal(_flexL["QT_IO"]) == 0)
                                //{
                                //    _flexL["UM_EX"] = 0;
                                //    _flexL["UM"] = 0;
                                //}
                                //else
                                //{
                                //    _flexL["UM_EX"] = D.GetDecimal(_flexL["AM_EX"]) / D.GetDecimal(_flexL["QT_IO"]);
                                //    _flexL["UM"] = (D.GetDecimal(_flexL["AM_EX"]) * D.GetDecimal(_flexL["RT_EXCH"])) / D.GetDecimal(_flexL["QT_IO"]);
                                //}
                                #endregion
                                break;
                        }

                        break;
                }

                DataRow[] idx_Row = flex.DataTable.Select("S = 'Y'");

                if (idx_Row == null || idx_Row.Length == 0) return;

                if (idx_Row.Length > 0) 
                    ToolBarSaveButtonEnabled = true; 
                else 
                    ToolBarSaveButtonEnabled = false;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region ♣ _flexL_StartEdit

        void _flexL_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                switch (_flexL.Cols[e.Col].Name)
                {
                    case "QT_IO":
                    case "QT_UNIT_MM":
                        if (_flexL["YN_INSPECT"].ToString() == "Y" && _biz.Get출하반품_검사 == "100")
                        {
                            e.Cancel = true;
                            return;
                        }
                        break;

                    case "QT_GR_PASS":
                    case "QT_GR_BAD":
                        if (_flexL["YN_INSPECT"].ToString() == "N")
                        {
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

        #region ♣ _flexL_DoubleClick

        void _flexL_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                switch (_flexL.Cols[_flexL.Col].Name.ToString())
                {
                    case "NO_IO_MGMT":
                        if (_flexL.DataSource != null)
                        {
                            string sCd_Company = Global.MainFrame.LoginInfo.CompanyCode;
                            string sNO_IO_MGMT = D.GetString(_flexL[_flexL.Row, "NO_IO_MGMT"]);
                            decimal sNO_IOLINE_MGMT = D.GetDecimal(_flexL[_flexL.Row, "NO_IOLINE_MGMT"]);

                            //수불구분에 따른 팝업을 보여주기 위한 구분값
                            string sGubun = "010|";
                            //팝업 체크를 위해 현재 컬럼값(수량)을 체크 저장
                            decimal dCellValue = 0m;

                            dCellValue = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_flexL[_flexL.Row, "QT_GIR_IM"]));

                            if (dCellValue > 0m)
                            {
                                pur.P_PU_ITEMIO_DETAIL_RPT_SUB dlg = new pur.P_PU_ITEMIO_DETAIL_RPT_SUB(sCd_Company, sNO_IO_MGMT, sNO_IOLINE_MGMT);
                                dlg.ShowDialog();
                            }
                        }
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

        #endregion

        #region ♣ 속성
        #region ♣ Chk일자
        bool Chk일자 { get { return Checker.IsValid(pp일자, true, DD("조회일")); } }  
        #endregion
        #endregion
    }
}


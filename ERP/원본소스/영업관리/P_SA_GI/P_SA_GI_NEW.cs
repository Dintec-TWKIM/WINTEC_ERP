using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Controls;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.SA;
using DzHelpFormLib;
using Duzon.ERPU.SA.Common;
using Duzon.Common.Forms.Help;

namespace sale
{
    // **************************************
    // 작   성   자 : NJin
    // 재 작  성 일 : 2008-12-08
    // 모   듈   명 : 영업
    // 시 스  템 명 : 영업관리
    // 서브시스템명 : 출하관리
    // 페 이 지  명 : 출하등록
    // 프로젝트  명 : P_SA_GI_NEW
    // **************************************
    // ** Change History
    // **************************************
    // 2013.02.15 : D20130207062 : 출하등록 라인에 거래처 P/O번호 추가
    // 2013.02.15 : D20130212201 : 출하등록 헤더에 고객납품의뢰등록 헤더비고1 추가
    // 2013.03.29 : D20130313061 : 출하등록 헤더에 약식거래처 컬럼 추가
    // **************************************
    public partial class P_SA_GI_NEW : Duzon.Common.Forms.PageBase
    {
        #region ♣ 생성자 & 변수 선언
        private P_SA_GI_BIZ _biz = new P_SA_GI_BIZ();

        private string MNG_LOT = Global.MainFrame.LoginInfo.MngLot; //시스템통제등록(LOT 사용여부를 가져온다.)
        private string MNG_SERIAL = string.Empty;                   //시스템통제등록 SERIAL사용여부 
        private string CD_DEPT = string.Empty;
        private string two_Unit_Mng = string.Empty;                 //영업환경설정 : 재고단위EDIT여부(2중단위관리여부) : 001
        private bool qtso_AddAllowYN = false;                       //영업환경설정 : 수주수량 초과허용 추가 2009.07.17 NJin (Default Value = false 으로 셋팅)
        private string Am_Recalc = "000";                           //영업환경설정 : 단가 및 금액 재계산 여부 Default 000 재계산, 001 재계산을 하지 않음. (분할되었을때 단가나 금액을 조정하여도 총 금액의 합이 같을 것인지 아닌지를 결정한다)
        private bool bStorageLocation = true;       //DB에서 받아와야 함.
        private FreeBinding _header = new FreeBinding();
        private 수주관리.Config 수주Config = new 수주관리.Config();
        private bool is중국고객부가세포함단가사용여부 = false;

        public P_SA_GI_NEW()
        {
            try
            {
                InitializeComponent();

                //이렇게 해주면 위에 툴바가 자동으로 움직여브러유~~~케헤헤헤
                MainGrids = new FlexGrid[] { _flexH, _flexL };

                //DetailQueryNeed 이거 사용 하려면 ~ 여기서 요거 셋팅해줘야 함~
                _flexH.DetailGrids = new FlexGrid[] { _flexL };

                is중국고객부가세포함단가사용여부 = 중국고객부가세포함단가사용여부();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region ♣ 초기화 이벤트 / 메소드

        #region ♣ InitLoad

        protected override void InitLoad()
        {
            base.InitLoad();

            //통제값에 따라서 그리드의 특정 셋팅이 달라지므로 통제값을 구한 후에 그리드를 셋팅해줘야 한다.
            InitGrid();
        }

        #endregion

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
            _flexH.SetCol("NO_GI_EMP", "출하의뢰자", 80);
            _flexH.SetCol("NM_KOR", "출하의뢰자명", 100);
            _flexH.SetCol("DC_RMK", "비고", 200);
            _flexH.SetCol("DC_RMK1", "비고1", 200);
            _flexH.SetCol("SN_PARTNER", "거래처명(약칭)", false);
            _flexH.ExtendLastCol = true;
            _flexH.EnabledHeaderCheck = true;
            _flexL.SettingVersion = "1.0.0.1";
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
            _flexL.SetCol("NO_LOT", "LOT여부", 80);
            _flexL.SetCol("DT_DUEDATE", "납기일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexL.SetCol("FG_TRANSPORT", "운송방법", 120);
            _flexL.SetCol("FG_TRANS", "거래구분", 80);

            if (_biz.Get출하등록_검사 == "200")
            {
                _flexL.SetCol("YN_INSPECT", "검사여부", 50, true, CheckTypeEnum.Y_N);
            }

            _flexL.SetCol("CD_SL", "창고코드", 80, true);
            _flexL.SetCol("NM_SL", "창고명", 120);
            _flexL.SetCol("QT_INV", "현재고", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("NM_QTIOTP", "출하형태", 100);
            _flexL.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("QT_GIR_IM", "관리수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("QT_UNIT_MM", "출하수량", 90, true, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("QT_IO", "출하관리수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("NO_PROJECT", "프로젝트코드", 80, false);
            _flexL.SetCol("NM_PROJECT", "프로젝트명", 80, false);
            _flexL.SetCol("UNIT", "단위", 80);
            _flexL.SetCol("CD_UNIT_MM", "관리단위", 80);
            _flexL.SetCol("CD_ITEM_PARTNER", "거래처품번", 150, false);
            _flexL.SetCol("NM_ITEM_PARTNER", "거래처품명", 150, false);
            _flexL.SetCol("NO_GIR", "의뢰번호", 100);

            if (MA.ServerKey(false, new string[] { "DNCOMPANY" }))
                _flexL.SetCol("DC_RMK", "택배번호", 150, true);
            else
                _flexL.SetCol("DC_RMK", "비고", 150, true);

            _flexL.SetCol("NM_SALEGRP", "영업그룹", 80, false);
            _flexL.SetCol("GI_PARTNER", "납품처코드", 80, false);
            _flexL.SetCol("LN_PARTNER", "납품처명", 80, false);

            _flexL.SetCol("FG_SERNO", "LOT/SN", 80);
            _flexL.SetCol("NM_ITEMGRP", "품목군", 100);

            string str단가금액통제 = Duzon.ERPU.MF.ComFunc.전용코드("출하등록-단가금액통제");
            if (str단가금액통제 == "N")
            {
                _flexL.SetCol("UM_EX_PSO", "단가", 90, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                _flexL.SetCol("AM_EX", "외화금액", 90, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                _flexL.SetCol("AM", "원화금액", 90, false, typeof(decimal), FormatTpType.MONEY);
            }

            if (BASIC.GetMAEXC("W/H 정보사용") == "100")
            {
                _flexL.SetCol("CD_WH", "W/H코드", 80, false);
                _flexL.SetCol("NM_WH", "W/H코드", 100, false);
            }

            _flexL.SetCol("CD_ZONE", "LOCATION", 100, false);
            _flexL.SetCol("NM_GRP_MFG", "제품군", 100, false);

            if (Global.MainFrame.ServerKeyCommon == "CHOSUNHOTELBA")
            {
                _flexL.SetCol("CD_PARTNER_GRP", "거래처그룹", 150, false);
                _flexL.SetCol("CD_PARTNER_GRP_2", "거래처그룹2", 150, false);
                _flexL.SetCol("CD_USERDEF1", "사용자정의", 150, false);
                _flexL.SetCol("CD_USERDEF2", "사용자정의2", 150, false);
            }

            if (Config.MA_ENV.YN_UNIT == "Y")
            {
                _flexL.SetCol("SEQ_PROJECT", "UNIT 항번", 120);
                _flexL.SetCol("CD_UNIT", "UNIT 코드", 120);
                _flexL.SetCol("NM_UNIT", "UNIT 명", 120);
                _flexL.SetCol("STND_UNIT", "UNIT 규격", 100);
            }

            _flexL.SetCol("NO_PO_PARTNER", "거래처P/O번호", 120);
            _flexL.SetCol("NO_POLINE_PARTNER", "거래처P/O항번", 120);
            _flexL.SetCol("NO_ISURCV", "의뢰번호", false);
            _flexL.SetCol("NO_ISURCVLINE", "의뢰항번", false);
            _flexL.SetCol("MAT_ITEM", "재질", false);
            _flexL.SetCol("CLS_ITEM", "품목계정", false);

            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "CNP")
            {
                _flexL.SetCol("QTIO_CD_USERDEF1", "송장번호", 120, true);
                _flexL.SetCol("QTIO_CD_USERDEF2", "택배사", 120, true);
            }

            if (수주Config.부가세포함단가사용())
            {
                _flexL.SetCol("TP_UM_TAX", "부가세여부", 90, false);
                _flexL.SetCol("UMVAT_GI", "부가세포함단가", 100, false, typeof(decimal), FormatTpType.UNIT_COST);
            }

            _flexL.VerifyPrimaryKey = new string[] { "NO_ISURCV", "NO_ISURCVLINE" };

            _flexL.EnabledHeaderCheck = true;
            _flexL.SettingVersion = "1.1.4.9";
            _flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            _flexL.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
            _flexL.BeforeCodeHelp += new BeforeCodeHelpEventHandler(_flex_BeforeCodeHelp);
            _flexL.CheckHeaderClick += new EventHandler(_flex_CheckHeaderClick);
            _flexL.ValidateEdit += new ValidateEditEventHandler(_flex_ValidateEdit);
            _flexL.DoubleClick += new EventHandler(_flex_DoubleClick);
            _flexL.AfterCodeHelp += new AfterCodeHelpEventHandler(_flexL_AfterCodeHelp);

            if (BASIC.GetMAEXC("W/H 정보사용") == "100")
            {
                _flexL.SetCodeHelpCol("CD_SL", Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CD_SL", "NM_SL", "CD_WH", "NM_WH" }, new string[] { "CD_SL", "NM_SL", "CD_WH", "NM_WH" }, new string[] { "CD_SL", "NM_SL", "CD_WH", "NM_WH", "QT_INV" });
            }
            else
            {
                _flexL.SetCodeHelpCol("CD_SL", Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CD_SL", "NM_SL" }, new string[] { "CD_SL", "NM_SL" }, new string[] { "CD_SL", "NM_SL", "QT_INV" });
            }

            _flexL.AddMyMenu = true;
            _flexL.AddMenuSeperator();
            ToolStripMenuItem parent = _flexL.AddPopup(DD("관련 현황"));
            _flexL.AddMenuItem(parent, DD("현재고조회"), Menu_Click);

            if (Global.MainFrame.ServerKeyCommon == "CHOSUNHOTELBA")
            {
                //panel1.Size = new System.Drawing.Size(panel1.Size.Width, panel1.Size.Height + 26);

                oneGrid2.Visible = true;
            }

            if (Config.MA_ENV.YN_UNIT == "Y")
                _flexL.SetExceptSumCol("UM_EX_PSO", "SEQ_PROJECT", "UMVAT_GI");
            else
                _flexL.SetExceptSumCol("UM_EX_PSO", "UMVAT_GI");
            #endregion
        }

        #endregion

        #region ♣ InitPaint : 프리폼 초기화
        //PageBase 의 부모함수 이용 : 폼이 올라올 후 폼위에 올라온 컨트롤들을 초기화 시켜주는 역할을 한다.
        //Combo Box 셋팅시 옵션 => N:공백없음, S:공백추가, SC:공백&괄호추가, NC:괄호추가
        //뒤의 코드값들은 DB 에서 정해놓은 파라미터와 매치시켜줘야한다
        protected override void InitPaint()
        {
            oneGrid1.UseCustomLayout = oneGrid2.UseCustomLayout =
            oneGrid3.UseCustomLayout = oneGrid4.UseCustomLayout = true;
            bpPanelControl1.IsNecessaryCondition = bpPanelControl16.IsNecessaryCondition =
            bpPanelControl23.IsNecessaryCondition = bpPanelControl24.IsNecessaryCondition =
            bpPanelControl25.IsNecessaryCondition = true;
            oneGrid1.InitCustomLayout();
            oneGrid2.InitCustomLayout();
            oneGrid3.InitCustomLayout();
            oneGrid4.InitCustomLayout();

            if (Global.MainFrame.ServerKeyCommon == "CHOSUNHOTELBA")
            {
                //1. 거래처그룹, 2.거래처그룹2, 3. 사용자정의, 4. 사용자정의2
                DataSet dsCHOSUNHOTELBA = this.GetComboData("S;MA_B000065", "S;MA_B000067", "S;MA_B000102", "S;MA_B000103");

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

                if (_flexL.Cols.Contains("CD_PARTNER_GRP"))
                    _flexL.SetDataMap("CD_PARTNER_GRP", dsCHOSUNHOTELBA.Tables[0].Copy(), "CODE", "NAME");
                if (_flexL.Cols.Contains("CD_PARTNER_GRP_2"))
                    _flexL.SetDataMap("CD_PARTNER_GRP_2", dsCHOSUNHOTELBA.Tables[1].Copy(), "CODE", "NAME");
                if (_flexL.Cols.Contains("CD_USERDEF1"))
                    _flexL.SetDataMap("CD_USERDEF1", dsCHOSUNHOTELBA.Tables[2].Copy(), "CODE", "NAME");
                if (_flexL.Cols.Contains("CD_USERDEF2"))
                    _flexL.SetDataMap("CD_USERDEF2", dsCHOSUNHOTELBA.Tables[3].Copy(), "CODE", "NAME");
            }

            통제조회();

            bStorageLocation = BASIC.GetMAEXC("W/H 정보사용") == "100" ? true : false;

            그리드통제적용();

            DataSet g_dsCombo = this.GetComboData("N;MA_PLANT", "S;TR_IM00008", "S;PU_C000016");

            // 공장 콤보
            cbo_Plant.DataSource = g_dsCombo.Tables[0];
            cbo_Plant.DisplayMember = "NAME";
            cbo_Plant.ValueMember = "CODE";

            cbo_Plant.SelectedValue = Global.MainFrame.LoginInfo.CdPlant;

            // 운송방법
            cbo_운송방법.DataSource = g_dsCombo.Tables[1];
            cbo_운송방법.DisplayMember = "NAME";
            cbo_운송방법.ValueMember = "CODE";

            cbo_운송방법.SelectedIndex = 0;


            //의뢰일자, 납기일자 콤보박스를 위한 데이터 테이블 생성
            DataTable dtCombo = g_dsCombo.Tables[0].Clone();

            DataRow dr = dtCombo.NewRow();
            dr["CODE"] = "GI";
            //dr["NAME"] = "의뢰일자";
            dr["NAME"] = DD("의뢰일자");
            dtCombo.Rows.Add(dr);

            dr = dtCombo.NewRow();
            dr["CODE"] = "DU";
            //dr["NAME"] = "납기일자";
            dr["NAME"] = DD("납기일자");
            dtCombo.Rows.Add(dr);

            dr = dtCombo.NewRow();
            dr["CODE"] = "RQ";
            //dr["NAME"] = "출하예정일";
            dr["NAME"] = DD("출하예정일");
            dtCombo.Rows.Add(dr);

            cbo_DtGubun.DataSource = dtCombo;
            cbo_DtGubun.DisplayMember = "NAME";
            cbo_DtGubun.ValueMember = "CODE";


            //거래구분
            _flexL.SetDataMap("FG_TRANS", g_dsCombo.Tables[2], "CODE", "NAME");

            DataTable dtFG_SERNO = MA.GetCode("MA_B000015");
            DataRow[] drs = dtFG_SERNO.Select("CODE = '001'", "", DataViewRowState.CurrentRows);
            if (drs.Length == 1)
                drs[0]["NAME"] = "";
            _flexL.SetDataMap("FG_SERNO", dtFG_SERNO, "CODE", "NAME");

            _flexL.SetDataMap("CLS_ITEM", MA.GetCode("MA_B000010"), "CODE", "NAME");

            // 조회조건 초기화
            pp일자.StartDateToString = Global.MainFrame.GetStringFirstDayInMonth;
            //dt_Gir_To.Text = Global.MainFrame.GetStringToday;
            bp_Partner.CodeValue = string.Empty;
            bp_Partner.CodeName = string.Empty;
            bp_GiPartner.CodeValue = string.Empty;
            bp_GiPartner.CodeName = string.Empty;
            bp_TpGi.CodeValue = string.Empty;
            bp_TpGi.CodeName = string.Empty;
            rdo_Not.Checked = true;
            rdo_Yes.Checked = false;
            //bp_GirEmp.CodeValue = string.Empty;
            //bp_GirEmp.CodeName = string.Empty;
            bp프로젝트.CodeValue = string.Empty;
            bp프로젝트.CodeName = string.Empty;

            // 등록조건 초기화
            dt_gi.Text = Global.MainFrame.GetStringToday;
            //부서를 Default 로 주어야 하기 때문에 처음에 Login 한 담당자를 Default 셋팅한 다음 그에 해당하는 부서를 셋팅한당.
            CD_DEPT = Global.MainFrame.LoginInfo.DeptCode;
            bp_Emp.CodeValue = Global.MainFrame.LoginInfo.EmployeeNo;
            bp_Emp.CodeName = Global.MainFrame.LoginInfo.EmployeeName;
            bp_CDSL.CodeValue = string.Empty;
            bp_CDSL.CodeName = string.Empty;
            txt_DcRmk.Text = string.Empty;
            bp_SaleGrp.CodeValue = string.Empty;
            bp_SaleGrp.CodeName = string.Empty;
            bp수주형태.CodeValue = string.Empty;
            bp수주형태.CodeName = string.Empty;

            #region -> 화면 닫을당시의 세팅값 불러오기

            if (Settings.Default.auto_No == "GIR")
            {
                rdo_TaxAll.Checked = true;
                rdo_TaxEntity.Checked = false;
            }
            else if (Settings.Default.auto_No == "PARTNER")
            {
                rdo_TaxAll.Checked = false;
                rdo_TaxEntity.Checked = true;
            }

            chk_YN.Checked = Settings.Default.의뢰비고적용여부;

            #endregion

            // 상태값 초기화
            txt_GiPartner.Text = string.Empty;
            txt_NoSo.Text = string.Empty;
            txt_NoLc.Text = string.Empty;

            // 버튼 활성 비활성
            btn_apply.Enabled = false;          //창고적용
            btn_apply_good.Enabled = false;     //양품적용

            // 프리폼 초기화
            DataTable dt = new DataTable();
            dt.Columns.Add("NO_IO", typeof(string));
            dt.Columns.Add("DT_IO", typeof(string));
            dt.Columns.Add("NO_EMP", typeof(string));
            dt.Columns.Add("NM_KOR", typeof(string));
            dt.Columns.Add("CD_SL", typeof(string));
            dt.Columns.Add("NM_SL", typeof(string));
            dt.Columns.Add("DC_RMK", typeof(string));


            //헤더테이블 디퐅트값 Setting 2011-09-09, 최승애 추가함.
            dt.Columns["DT_IO"].DefaultValue = Global.MainFrame.GetStringToday;
            dt.Columns["NO_EMP"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;
            dt.Columns["NM_KOR"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeName;


            _header.SetBinding(dt, oneGrid3);
            _header.ClearAndNewRow();
            //_flexH.Binding = dt;
            //_flexL.Binding = ds.Tables[2];


            //삭제버튼과 인쇄버튼은 이화면에서 필요 없음으로 무조건 비활성한다.
            Auth();

            btn_apply.Visible = !ConfigSA.SA_EXC.WH정보사용;

            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "CNP")
                _flexL.SetDataMap("QTIO_CD_USERDEF2", MA.GetCode("CZ_CNP_007"), "CODE", "NAME");
        }

        void 통제조회()
        {
            //시스템통제등록 SERIAL사용여부
            object[] obj = new object[1];
            obj[0] = Global.MainFrame.LoginInfo.CompanyCode;
            MNG_SERIAL = _biz.search_SERIAL(obj);

            //영업환경설정 : 수주수량 초과허용 : 000 , 재고단위 EDIT 여부(2중단위관리 ) : 001 , 할인율 적용여부 : 002
            DataTable dt = _biz.search_EnvMng();

            if (dt.Rows.Count > 0)
                // 000:기본 100:평화 200:영우 (null이거나 ''은 000으로 치환) 
                if (dt.Rows[0]["FG_TP"] != System.DBNull.Value && dt.Rows[0]["FG_TP"].ToString().Trim() != String.Empty)
                    two_Unit_Mng = dt.Select("FG_TP = '001'")[0]["CD_TP"].ToString();       //재고단위 EDIT 여부(2중단위관리 )

            qtso_AddAllowYN = Sa_Global.Qtso_AddAllowYN;  //수주수량 초과허용 추가 2009.07.17 NJin (Default Value = false 으로 셋팅)
            Am_Recalc = Sa_Global.AM_ReCalc; //단가 및 금액 재계산 여부 Default 000 재계산, 001 재계산을 하지 않음. (분할되었을때 단가나 금액을 조정하여도 총 금액의 합이 같을 것인지 아닌지를 결정한다)

        }

        void 그리드통제적용()
        {
            //재고단위EDIT여부(2중단위관리여부) 에 따라서 출하관리수량을 Edit 가능 여부를 체크한다.
            if (two_Unit_Mng != "N")
                _flexL.Cols["QT_IO"].AllowEditing = true;

            if (App.SystemEnv.PROJECT사용)
                _flexL.VerifyNotNull = new string[] { "NO_PROJECT" };
        }

        #endregion

        #endregion

        #region ♣ 삭제버튼과 인쇄버튼은 이화면에서 필요 없음으로 무조건 비활성한다.
        private void Auth()
        {
            ToolBarDeleteButtonEnabled = false;
            ToolBarPrintButtonEnabled = false;
        }
        #endregion

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
                //LabelExt lbl = new LabelExt();
                LabelExt lbl2 = new LabelExt();

                //lbl.Text = "조회일";

                //hList.Add(dt_Gir_From, lbl);
                //hList.Add(dt_Gir_To, lbl);
                hList.Add(cbo_Plant, lbl_공장);
            }
            else if (Flag == "SAVE")
            {
                hList.Add(dt_gi, lbl_출하일자);
                hList.Add(bp_Emp, lbl_담당자);
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

                object[] obj = new object[] {
                    LoginInfo.CompanyCode, 
                    pp일자.StartDateToString, 
                    pp일자.EndDateToString, 
                    cbo_Plant.SelectedValue == null ? string.Empty : cbo_Plant.SelectedValue.ToString(), 
                    bp_Partner.CodeValue, 
                    bp_GiPartner.CodeValue, 
                    bp_TpGi.CodeValue, 
                    "N",    //처리상태가 미처리 상태 즉, 반품여부가 "N" 인것
                    D.GetString(cbo_운송방법.SelectedValue), 
                    D.GetString(bp_GirEmp.CodeValue), 
                    D.GetString(cbo_DtGubun.SelectedValue), 
                    D.GetString(bp_SaleGrp.CodeValue), 
                    D.GetString(bp수주형태.CodeValue), 
                    D.GetString(bp프로젝트.CodeValue), 
                    D.GetString(bp제품군.CodeValue), 
                    D.GetString(bp의뢰창고.CodeValue), 
                    D.GetString(cbo거래처그룹.SelectedValue), 
                    D.GetString(cbo거래처그룹2.SelectedValue), 
                    D.GetString(cbo사용자정의.SelectedValue), 
                    D.GetString(cbo사용자정의2.SelectedValue),
                    MA.Login.사원번호,
                    bpc생산파트.QueryWhereIn_Pipe
                };

                DataTable dt = _biz.Search(obj, D.GetString(cbo_DtGubun.SelectedValue));

                //if (dt.Rows.Count == 0)
                //{
                //    ShowMessage(PageResultMode.SearchNoData);// 검색된 내용이 존재하지 않습니다..

                //    //추가 버튼을 이용하여 Clear() 를 시키면 조회 조건으로 입력 했던것들이 바보 되니까~ 그냥 남겨 두기 위해서 그리드만 데이터 지우기~
                //    //OnToolBarAddButtonClicked(sender, e);
                //    if (_flexH != null || _flexH.DataTable != null || _flexH.DataTable.Rows.Count != 0)
                //    {
                //        _flexH.DataTable.Clear();
                //        _flexL.DataTable.Clear();
                //    }

                //    // 버튼 활성 비활성
                //    btn_apply.Enabled = false;          //창고적용 : 조회의 버튼 활성 활성에서는 안 해줘도 됨
                //    btn_apply_good.Enabled = false;     //양품적용

                //    //삭제버튼과 인쇄버튼은 이화면에서 필요 없음으로 무조건 비활성한다.
                //    Auth();

                //    return;
                //}

                dt.DefaultView.Sort = "NO_GIR ASC";  //Ordering을 프로그램에서 함.
                dt = dt.DefaultView.ToTable();
                _flexH.Binding = dt;

                if (!_flexH.HasNormalRow)
                {
                    // 버튼 활성 비활성
                    btn_apply.Enabled = false;          //창고적용 : 조회의 버튼 활성 활성에서는 안 해줘도 됨
                    btn_apply_good.Enabled = false;     //양품적용

                    //삭제버튼과 인쇄버튼은 이화면에서 필요 없음으로 무조건 비활성한다.
                    Auth();

                    ShowMessage(PageResultMode.SearchNoData);// 검색된 내용이 존재하지 않습니다..
                }

                // 버튼 활성 활성
                btn_apply_good.Enabled = true;     //양품적용

                //삭제버튼과 인쇄버튼은 이화면에서 필요 없음으로 무조건 비활성한다.
                Auth();
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

                //dt_Gir_From.Focus();
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

                    if (BASIC.GetMAEXC("W/H 정보사용") != "100" && drl["CD_SL"].ToString() == string.Empty)
                    {
                        this.ShowMessage(" 창고는 필수입력항목입니다. \n\n 창고를 확인하세요.");
                        return;
                    }

                    if (_flexL.CDecimal(drl["QT_UNIT_MM"]) <= 0)
                    {
                        ShowMessage(공통메세지._은_보다커야합니다, DD("출하수량"), "0");
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
                            if (dr["NO_ISURCV"].ToString() == dr_L["NO_ISURCV"].ToString() && D.GetDecimal(dr["NO_ISURCVLINE"].ToString()) == D.GetDecimal(dr_L["NO_ISURCVLINE"].ToString()))
                                dr_L["S"] = "N";

                            // 라인의 체크가 모두 해제 되었는지 확인해서 모두 체크가 풀렸으면 헤더의 체크도 풀어준다.
                            DataRow[] drs_ex = _flexL.DataTable.Select("S = 'Y' AND NO_ISURCV = '" + dr["NO_ISURCV"].ToString() + "'");
                            if (drs_ex == null || drs_ex.Length == 0)
                            {
                                DataRow[] drs_exh = _flexH.DataTable.Select("S = 'Y' AND NO_GIR = '" + dr["NO_ISURCV"].ToString() + "'");
                                if (dr["NO_ISURCV"].ToString() == drs_exh[0]["NO_GIR"].ToString())
                                    drs_exh[0]["S"] = "N";
                            }
                        }
                    }
                }
                #endregion

                //헤더와 라인의 체크된 수량을 가져온다. 
                //헤더의 체크수량을 파악하여 1인경우에는 LOT 여부에 상관없이 저장 가능하다. 
                //라인의 체크수량과 LOT 여부를 파악한 수량을 가져와서 헤더가 2이상인 경우에는 EXCEPTION 처리를 한다. 
                DataRow[] cnt_Header_chk = _flexH.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);
                DataRow[] cnt_Line_lot_chk = _flexL.DataTable.Select("S = 'Y' AND NO_LOT = 'YES'", "", DataViewRowState.CurrentRows);
                DataRow[] cnt_Line_serial_chk = _flexL.DataTable.Select("S = 'Y' AND NO_SERL = 'YES'", "", DataViewRowState.CurrentRows);

                if (cnt_Header_chk == null || cnt_Header_chk.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return false;
                }
                else if (MNG_LOT == "Y" && cnt_Header_chk.Length > 1 && (cnt_Line_lot_chk == null || cnt_Line_lot_chk.Length > 0))
                {
                    ShowMessage(" 여러개의 의뢰번호에 해당하는 LOT품목이 여러건이 존재하여 \n\n 일괄 작업을 수행할 수 없습니다. \n\n 한건씩 처리하시기 바랍니다.");
                    return false;
                }
                else if (MNG_SERIAL == "Y" && cnt_Header_chk.Length > 1 && (cnt_Line_serial_chk == null || cnt_Line_serial_chk.Length > 0))
                {
                    ShowMessage(" 여러개의 의뢰번호에 해당하는 SERIAL품목이 여러건이 존재하여 \n\n 일괄 작업을 수행할 수 없습니다. \n\n 한건씩 처리하시기 바랍니다.");
                    return false;
                }

                #region 여신체크
                DataTable temp_dt = new DataTable(); // 거래처로 Group 해서 총 사용할 금액을 가지고 있을 DataTable을 생성한다.
                //거래처 : 금액 컬럼 스키마 추가
                temp_dt.Columns.Add("CD_PARTNER", typeof(string));
                temp_dt.Columns.Add("CD_PARTNER_NAME", typeof(string));
                temp_dt.Columns.Add("AM_SUM", typeof(decimal));

                DataRow[] cnt_Line = _flexL.DataTable.Select("S = 'Y' AND ST_STAT <> '1'");
                //선택된 거래처로 묶어서 여신을 체크한다음 ERROR 인 경우와 경고를 무시하지 않는 경우에는 선택버튼을 풀어준다.
                foreach (DataRow temp_dr in cnt_Line)
                {
                    DataRow[] temp_drs = null;
                    temp_drs = temp_dt.Select("CD_PARTNER = '" + temp_dr["CD_PARTNER"].ToString() + "'");

                    if (temp_drs.Length == 0)
                    {
                        DataRow temp_row = temp_dt.NewRow();

                        //그리드의 첫행
                        temp_row["CD_PARTNER"] = temp_dr["CD_PARTNER"].ToString();
                        temp_row["CD_PARTNER_NAME"] = temp_dr["CD_PARTNER_NAME"].ToString();
                        temp_row["AM_SUM"] = _flexL.CDecimal(temp_dr["AM"]) + _flexL.CDecimal(temp_dr["VAT"]);

                        temp_dt.Rows.Add(temp_row);
                    }
                    else
                    {
                        temp_drs[0]["AM_SUM"] = _flexL.CDecimal(temp_drs[0]["AM_SUM"]) + _flexL.CDecimal(temp_dr["AM"]) + _flexL.CDecimal(temp_dr["VAT"]);
                    }
                }

                //여신체크
                DataTable warning_Dt = new DataTable();
                warning_Dt.Columns.Add("S", typeof(string));                    //선택
                warning_Dt.Columns.Add("CD_PARTNER", typeof(string));           //거래처
                warning_Dt.Columns.Add("CD_PARTNER_NAME", typeof(string));      //거래처명
                warning_Dt.Columns.Add("CREDIT_TOT", typeof(decimal));          //여신총액
                warning_Dt.Columns.Add("MISU_REMAIN", typeof(decimal));         //미수잔액
                warning_Dt.Columns.Add("CREDIT_RAMAIN", typeof(decimal));       //여신잔액
                warning_Dt.Columns.Add("AM_SUM", typeof(decimal));              //출하금액
                warning_Dt.Columns.Add("EX_CONTENT", typeof(string));           //WARNING or ERROR 

                DataTable error_Dt = new DataTable();
                error_Dt.Columns.Add("S", typeof(string));                      //선택
                error_Dt.Columns.Add("CD_PARTNER", typeof(string));             //거래처
                error_Dt.Columns.Add("CD_PARTNER_NAME", typeof(string));        //거래처명
                error_Dt.Columns.Add("CREDIT_TOT", typeof(decimal));            //여신총액
                error_Dt.Columns.Add("MISU_REMAIN", typeof(decimal));           //미수잔액
                error_Dt.Columns.Add("CREDIT_RAMAIN", typeof(decimal));         //여신잔액
                error_Dt.Columns.Add("AM_SUM", typeof(decimal));                //출하금액
                error_Dt.Columns.Add("EX_CONTENT", typeof(string));             //WARNING or ERROR 

                string cd_Partner = string.Empty, cd_Partner_name = string.Empty;
                foreach (DataRow dr in temp_dt.Rows)
                {
                    DataRow ex_Dr = warning_Dt.NewRow();

                    /*
                     * 여기에서 처리될 내역은 거래처로 여신을 체크해서 ROW 를 받아온다. 
                     * 받아온 ROW를 여신ERROR 도움창에 셋팅한다.
                     * 받아온 ROW에 여신상태를 읽어서 WARNING 이면 상단그리드 ERROR 이면 하단 그리드에 셋팅한다.
                     * 거래처 : 적용여신 : 여신총액 : 잔액 
                     * 
                     * 도움창에서는 WARNING 에서만 CHECK BOX 를 넣어서 체크 된 건은 놔두고 
                     * WARNING 에서 체크되지 않은 거래처와 ERROR 에 걸린 거래처만 리턴하여
                     * 본 등록의 상/하단 거래처의 처리건에 대한 체크를 해제한다. 
                     */
                    Decimal am_sum = 0;

                    cd_Partner = dr["CD_PARTNER"].ToString();
                    cd_Partner_name = dr["CD_PARTNER_NAME"].ToString();
                    am_sum = _flexL.CDecimal(dr["AM_SUM"]);

                    ex_Dr = _biz.CheckCredit(cd_Partner, cd_Partner_name, am_sum, ex_Dr);

                    if (ex_Dr == null) continue;

                    if (ex_Dr["EX_CONTENT"].ToString() == "WARNING")
                        warning_Dt.Rows.Add(ex_Dr.ItemArray);
                    else if (ex_Dr["EX_CONTENT"].ToString() == "ERROR")
                        error_Dt.Rows.Add(ex_Dr.ItemArray);
                }

                if ((warning_Dt != null || error_Dt != null) && (warning_Dt.Rows.Count != 0 || error_Dt.Rows.Count != 0))
                {
                    P_SA_CREDIT_SUB dlg = new P_SA_CREDIT_SUB(warning_Dt, error_Dt);

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        if (dlg.returnDt != null && dlg.returnDt.Rows.Count != 0)
                        {
                            foreach (DataRow dr in dlg.returnDt.Rows)
                            {
                                error_Dt.ImportRow(dr);
                            }
                        }
                    }
                    else
                        return false;
                }

                if (error_Dt != null && error_Dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in error_Dt.Rows)
                    {
                        foreach (DataRow dr_H in _flexH.DataTable.Rows)
                        {
                            if (dr["CD_PARTNER"].ToString() == dr_H["CD_PARTNER"].ToString())
                                dr_H["S"] = "N";
                        }

                        foreach (DataRow dr_L in _flexL.DataTable.Rows)
                        {
                            if (dr["CD_PARTNER"].ToString() == dr_L["CD_PARTNER"].ToString())
                                dr_L["S"] = "N";
                        }
                    }
                }

                #endregion

                #region 이 부분을 위에서 한번 사용하고 필터링 했는데 또 사용하는 이유는 여신체크를 하면서 ERROR 이나 경고로 인한 ROLLBACK 인 경우 체크박스를 해제 시켜주기 때문이다.
                //헤더와 라인의 체크된 수량을 가져온다. 
                //헤더의 체크수량을 파악하여 1인경우에는 LOT 여부에 상관없이 저장 가능하다. 
                //라인의 체크수량과 LOT 여부를 파악한 수량을 가져와서 헤더가 2이상인 경우에는 EXCEPTION 처리를 한다. 
                DataRow[] cnt_Header = _flexH.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                if (cnt_Header == null || cnt_Header.Length == 0)
                {
                    ShowMessage("여신체크로 인해 출하시킬 수 있는 출하내역이 존재하지 않습니다.");

                    return false;
                }
                #endregion

                DataTable dt_H = null, dt_L = null, dt_LOT = null, dt_SERIAL = null, dt_TEMP = null, dt_ASN = null;
                dt_H = _flexH.DataTable.Clone();
                dt_L = _flexL.DataTable.Clone();
                string NO_IO = string.Empty;

                T.SetDefaultValue(dt_H);
                T.SetDefaultValue(dt_L);

                #region -> 추가한 채번로직(한번에 가려오기 위해 추가)

                DataRow[] drsGroup = null;

                if (rdo_TaxAll.Checked)
                    drsGroup = _flexH.DataView.ToTable(true, "NO_GIR", "S").Select("S = 'Y'");
                else
                    drsGroup = _flexH.DataView.ToTable(true, "CD_PARTNER", "S").Select("S = 'Y'");

                DataTable dtGroup = drsGroup[0].Table.Clone();  //한행은 있다는 가정하에 한다.
                dtGroup.PrimaryKey = new DataColumn[] { dtGroup.Columns[0] };

                List<string> list년월 = new List<string>();

                foreach (DataRow row추가 in drsGroup)
                {
                    list년월.Add(MainFrameInterface.GetStringYearMonth);
                    dtGroup.Rows.Add(row추가.ItemArray);
                }

                string ym = string.Empty;

                foreach (string str in list년월.ToArray())
                {
                    ym += str + "|";
                }

                List<string> newYm = new List<string>();
                string[] argsYm = D.StringConvert.GetPipes(ym, 500);

                foreach (string str in argsYm)
                {
                    string[] args = str.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    DataRow[] dr채번 = (DataRow[])this.GetSeq(LoginInfo.CompanyCode, "SA", "07", args);

                    foreach (DataRow row in dr채번)
                        newYm.Add(D.GetString(row["DOCU_NO"]));
                }

                //DataRow[] dr채번 = (DataRow[])this.GetSeq(LoginInfo.CompanyCode, "SA", "07", list년월.ToArray());

                #endregion

                int i채번 = 0;

                foreach (DataRow drh_cnt in cnt_Header)
                {
                    DataRow[] header_Row = null;

                    if (rdo_TaxAll.Checked == true)
                        header_Row = dt_H.Select("S = 'Y' AND NO_GIR = '" + drh_cnt["NO_GIR"].ToString() + "'", "", DataViewRowState.CurrentRows);
                    else
                        header_Row = dt_H.Select("S = 'Y' AND CD_PARTNER = '" + drh_cnt["CD_PARTNER"].ToString() + "'", "", DataViewRowState.CurrentRows);

                    #region 헤더에 데이터 채우기
                    if (header_Row.Length == 0)
                    {
                        //AUTO CREATE NO !! 
                        //NO_IO = GetSeq(LoginInfo.CompanyCode, "SA", "07", dt_gi.Text.Substring(0, 6)).ToString();//출고번호
                        NO_IO = newYm[i채번++].ToString();

                        #region ♣ 라인에 데이터 채우기

                        decimal line = 1;
                        DataRow[] line_Row = null;

                        if (rdo_TaxAll.Checked == true)
                            line_Row = _flexL.DataTable.Select("S = 'Y' AND NO_GIR = '" + drh_cnt["NO_GIR"].ToString() + "'", "", DataViewRowState.CurrentRows);
                        else
                            line_Row = _flexL.DataTable.Select("S = 'Y' AND CD_PARTNER = '" + drh_cnt["CD_PARTNER"].ToString() + "'", "", DataViewRowState.CurrentRows);

                        foreach (DataRow dl in line_Row)
                        {
                            dl["NO_IO"] = NO_IO;
                            dl["NO_IOLINE"] = line++;
                            dl["DT_IO"] = dt_gi.Text;
                            dl["FG_IO"] = "010";

                            dt_L.ImportRow(dl);
                        }

                        #endregion

                        //라인 먼저 데이터 채우고 헤더에 데이터를 채우는 이유는 라인에 있는값이 헤더에 저장되는 것들이 있기에~ 내말 뭔말인지 알쮜~ㅋㅋ
                        //원래 출하의뢰를 조회해서 가져올 사항에 데이터는 없지만 조회쿼리에 쓰레기 값을 가져오는 이유는 스키마 생성해서 멀티저장 바로 가능하게 하기위함
                        //멀티저장시에 다른 것들은 코딩상에서 스키마를 추가해서 보여줬으나 쿼리에 쓰레기값 가져와서 스키마 만들어주는 방법도 괜찮은듯...
                        drh_cnt["NO_IO"] = NO_IO;
                        drh_cnt["FG_TRANS"] = line_Row[0]["FG_TRANS"].ToString();


                        //출하등록-출하담당자와 출하의뢰자 동기화 전용코드에 따라 
                        //출하등록담당자를 담당자/의뢰담당자로 처리하도록 수정(2011/01/20, BY SMJUNG)
                        if (rdo_TaxAll.Checked && BASIC.GetMAEXC("출하등록-출하담당자와 출하의뢰자 동기화") == "001")
                            drh_cnt["NO_EMP"] = drh_cnt["NO_GI_EMP"];
                        else
                            drh_cnt["NO_EMP"] = bp_Emp.CodeValue;


                        drh_cnt["CD_DEPT"] = CD_DEPT;
                        drh_cnt["DT_IO"] = dt_gi.Text;
                        drh_cnt["YN_RETURN"] = "N";

                        if (chk_YN.Checked != true)
                            drh_cnt["DC_RMK"] = txt_DcRmk.Text;

                        dt_H.ImportRow(drh_cnt);

                        if (NO_IO == string.Empty)
                        {
                            ShowMessage("출하번호가 존재하지 않습니다.");
                            return false;
                        }
                    }
                    #endregion
                }

                #region -> 디엔컴퍼니전용 라인비고 필수체크
                if (MA.ServerKey(false, new string[] { "DNCOMPANY" }))
                {
                    foreach (DataRow dr in dt_L.Rows)
                    {
                        if (D.GetString(dr["DC_RMK"]) == string.Empty)
                        {
                            ShowMessage("택배번호가 들어가지 않았습니다.");
                            return false;
                        }
                    }
                }
                #endregion

                #region -> StorageLocation

                if (bStorageLocation)
                {
                    DataRow[] drs = null;
                    DataTable dtSave = null;
                    if (_flexL.DataTable != null)
                    {
                        dtSave = _flexL.DataTable.Clone();
                        drs = _flexL.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);
                        foreach (DataRow dr in drs)
                        {
                            dtSave.Rows.Add(dr.ItemArray);
                        }
                    }

                    P_SA_SL_SUB_I m_dlg = new P_SA_SL_SUB_I(dtSave, two_Unit_Mng, qtso_AddAllowYN, Am_Recalc);

                    if (m_dlg.ShowDialog(this) == DialogResult.OK)
                        dt_L = m_dlg.dtL;
                    else
                        return false;
                }

                #endregion

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

                        string[] param = new string[3];

                        param[0] = "N";                 //YN_RETURN
                        param[1] = string.Empty;        //NO_PROJECT
                        param[2] = string.Empty;        //NM_PROJECT


                        pur.P_PU_LOT_SUB_I m_dlg = new pur.P_PU_LOT_SUB_I(dt_LOT, param);

                        if (m_dlg.ShowDialog(this) == DialogResult.OK)
                            dt_LOT = m_dlg.dtL;
                        else
                            return false;
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

                        pur.P_PU_SERL_SUB_I m_dlg = new pur.P_PU_SERL_SUB_I(dt_SERIAL);

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

                        temp_drs = temp_Lot.Select("출고번호 = '" + temp_dr["출고번호"].ToString() + "' AND 출고항번 = '" + _flexL.CDecimal(temp_dr["출고항번"]) + "'");

                        if (temp_drs.Length == 0)
                        {
                            DataRow temp_row = temp_Lot.NewRow();

                            //그리드의 채번된 첫행
                            temp_row["출고번호"] = temp_dr["출고번호"].ToString();
                            temp_row["출고항번"] = _flexL.CDecimal(temp_dr["출고항번"]);
                            temp_row["CD_ITEM"] = temp_dr["CD_ITEM"];
                            temp_row["QT_GOOD_MNG"] = _flexL.CDecimal(temp_dr["QT_GOOD_MNG"]);

                            temp_Lot.Rows.Add(temp_row);
                        }
                        else
                        {
                            temp_drs[0]["QT_GOOD_MNG"] = _flexL.CDecimal(temp_drs[0]["QT_GOOD_MNG"]) + _flexL.CDecimal(temp_dr["QT_GOOD_MNG"]);

                        }
                    }
                    #endregion

                    int cnt_item = 0; //LOT항번과 수불항번이 같은데 품목이 다를경우 EXCEPTION 처리한다.
                    int cnt_Lot = 0; //LOT수량과 수불수량이 다를경우 EXCEPTION 처리한다.
                    foreach (DataRow dr in dr_grid)
                    {
                        foreach (DataRow dr_lot in temp_Lot.Rows)
                        {
                            if (dr["NO_IO"].ToString() == dr_lot["출고번호"].ToString() && dr["NO_IOLINE"].ToString() == dr_lot["출고항번"].ToString())
                            {
                                //LOT항번과 수불항번이 같은데 품목이 다른것이 걸릴경우 EXCEPTION 처리
                                if (dr["CD_ITEM"].ToString().Trim() != dr_lot["CD_ITEM"].ToString().Trim())
                                    cnt_item++;

                                if (dr["CD_ITEM"].ToString() == dr_lot["CD_ITEM"].ToString() && _flexL.CDecimal(dr["QT_IO"]) != _flexL.CDecimal(dr_lot["QT_GOOD_MNG"]))
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

                DataTable dtLocation = null;
                if (Config.MA_ENV.YN_LOCATION == "Y") //시스템환경설정에서 LOCATION사용인것만 창고별로 사용인지 아닌지는 도움창 호출후 판단한다. 붙여야하는화면이 많은 관계로 여기서 통합으로 처리해주는걸로 판단함
                {                                           //넘길때 공장,창고,품목은 필수항목
                    bool bLocation = false;
                    DataTable dt_lc = dt_L.Clone().Copy();
                    foreach (DataRow dr in dt_L.Select())
                        dt_lc.LoadDataRow(dr.ItemArray, true);

                    dtLocation = P_OPEN_SUBWINDOWS.P_MA_LOCATION_I_SUB(dt_lc, out bLocation);

                    if (!bLocation) return false;
                }

                #endregion

                #region -> 안전공업전용 ASN번호등록
                if (MA.ServerKey(false, new string[] { "ANJUN" }))
                {
                    DataRow[] drarr = dt_L.Select("FG_TRANS IN ('004', '005') AND PITEM_CD_USERDEF2 = 'Y' AND CD_USERDEF1 = 'Y'", "", DataViewRowState.CurrentRows);
                    if (drarr.Length > 0)
                    {
                        DataTable dt_temp = null;
                        dt_temp = dt_L.Clone();

                        foreach (DataRow row in drarr)
                            dt_temp.Rows.Add(row.ItemArray);

                        P_SA_Z_ANJUN_GI_ASN_SUB dlg = new P_SA_Z_ANJUN_GI_ASN_SUB(dt_temp);
                        if (dlg.ShowDialog(this) == DialogResult.OK)
                            dt_ASN = dlg.dtL;
                    }
                }
                #endregion

                bool bSuccess = _biz.Save(dt_H, dt_L, dtLocation, dt_LOT, dt_SERIAL, dt_gi.Text, dt_ASN);
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
                if (!_flexH.HasNormalRow)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                if (!_flexL.HasNormalRow)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                DataRow[] ldrchk = _flexL.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                if (ldrchk == null || ldrchk.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                //decimal rowUnit합계 = 0;
                //decimal rowIO합계 = 0;

                //LOT 품목이 없을 수 있음으로 체크하면 안됨
                if (ldrchk != null && ldrchk.Length > 0)
                {
                    foreach (DataRow row in ldrchk)
                    {
                        row["QT_UNIT_MM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row["QT_GIR"]));
                        row["QT_IO"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row["QT_GIR_IM"]));
                        row["QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row["QT_IO"]));     //양품재고
                        row["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["ORG_AM"]));
                        row["AM_EX"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(row["ORG_AM_EX"]));
                        row["RT_EXCH"] = Unit.환율(DataDictionaryTypes.SA, D.GetDecimal(row["RT_EXCH"]));
                        row["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_VAT"]));

                        if (D.GetDecimal(row["CHK_QT_GI"]) != 0)
                        {
                            //부가세포함단가 미사용
                            if (D.GetString(row["TP_UM_TAX"]) != "Y" || (Global.MainFrame.ServerKeyCommon != "DZSQL" && Global.MainFrame.ServerKeyCommon != "GNSD" && !is중국고객부가세포함단가사용여부))
                            {
                                //금액잔량관리 안함(금액 = 수량 * 단가)
                                if (Am_Recalc == "000")
                                {
                                    //row["AM_EX"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(row["UM_EX_PSO"]) * D.GetDecimal(row["QT_UNIT_MM"]));
                                    //row["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_EX"]) * D.GetDecimal(row["RT_EXCH"]));
                                    //row["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM"]) * (D.GetDecimal(row["RT_VAT"]) / 100));

                                    ///옵션대로라면 위 주석 처리된 계산식처럼 (수량 * 단가)로 계산되어져야 하나 기존방식이 금액BASE 로직으로 되어 있어서
                                    ///이미 쓰고 있는 업체에 문제가 발생할 소지가 있으므로 아래의 금액BASE 로직과 똑같게 계산되도록 한다.(2014.09.24)
                                    row["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["ORG_AM"]));
                                    row["AM_EX"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(row["ORG_AM_EX"]));
                                    row["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM"]) * (D.GetDecimal(row["RT_VAT"]) / 100));
                                }
                                //금액잔량관리 함(금액 = 의뢰금액 - 기출하금액)
                                else
                                {
                                    row["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["ORG_AM"]));
                                    row["AM_EX"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(row["ORG_AM_EX"]));
                                    row["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM"]) * (D.GetDecimal(row["RT_VAT"]) / 100));
                                }

                                decimal amVat = D.GetDecimal(row["AM"]) + D.GetDecimal(row["VAT"]);
                                row["UMVAT_GI"] = Unit.원화단가(DataDictionaryTypes.SA, amVat / D.GetDecimal(row["QT_UNIT_MM"]));
                            }
                            //부가세포함단가 사용
                            else
                            {
                                decimal amVat = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["QT_UNIT_MM"]) * D.GetDecimal(row["UMVAT_GI"]));
                                if (Global.MainFrame.LoginInfo.CompanyLanguage == Language.KR)
                                    row["AM"] = Decimal.Round(amVat * (100 / (100 + D.GetDecimal(row["RT_VAT"]))), MidpointRounding.AwayFromZero);
                                else
                                    row["AM"] = D.GetDecimal(UDecimal.GetConvertFormatData(DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.INSERT, amVat * (100 / (100 + D.GetDecimal(row["RT_VAT"])))));
                                row["VAT"] = amVat - D.GetDecimal(row["AM"]);
                                row["AM_EX"] = D.GetDecimal(row["RT_EXCH"]) == 0m ? 0m : Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM"]) / D.GetDecimal(row["RT_EXCH"]));
                            }
                        }
                        else
                        {
                            row["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["ORG_AM"]));
                            row["AM_EX"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(row["ORG_AM_EX"]));
                            row["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_VAT"]));
                        }

                        row["UM_EX"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row["AM_EX"]) / D.GetDecimal(row["QT_IO"]));
                        row["UM"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(row["AM"]) / D.GetDecimal(row["QT_IO"]));

                        if (D.GetString(row["TP_UM_TAX"]) != "Y" || (Global.MainFrame.ServerKeyCommon != "DZSQL" && Global.MainFrame.ServerKeyCommon != "GNSD" && !is중국고객부가세포함단가사용여부))
                        {
                            if (Am_Recalc == "001" || D.GetDecimal(row["CHK_QT_GI"]) == 0)
                            {
                                {
                                    row["UM_EX"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row["UM_EX_PSO"]));
                                    row["UM"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(row["UM_EX_PSO"]) * D.GetDecimal(row["RT_EXCH"]));
                                }
                            }
                        }

                        #region 기존 양품적용 로직
                        /*
                        //이미출하된 수량을 체크하여 수량이 0보다 클경우 부가세 재계산 하고 0일경우에는 의뢰 부가세를 그대로 적용(2010.03.04) 
                        if (D.GetDecimal(row["CHK_QT_GI"]) != 0)
                        {
                            if (Am_Recalc == "000")
                            {
                                //재계산
                                if (D.GetDecimal(row["QT_IO"]) == 0)
                                {
                                    row["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_EX"]) * D.GetDecimal(row["RT_EXCH"]));
                                    row["UM_EX"] = 0m;
                                    row["UM"] = 0m;
                                }
                                else
                                {
                                    row["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_EX"]) * D.GetDecimal(row["RT_EXCH"]));
                                    row["UM_EX"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row["AM_EX"]) / D.GetDecimal(row["QT_IO"]));
                                    row["UM"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(row["AM"]) / D.GetDecimal(row["QT_IO"]));
                                }
                            }
                            else
                            {
                                if (D.GetDecimal(row["QT_IO"]) == 0)
                                {
                                    row["UM_EX"] = 0m;
                                    row["UM"] = 0m;
                                }
                                else
                                {
                                    row["UM_EX"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row["UM_EX_PSO"]));
                                    row["UM"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(row["UM_EX_PSO"]) * D.GetDecimal(row["RT_EXCH"]));
                                }

                                //row["VAT"] = row["AM_VAT"];   //부가세
                            }
                            row["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM"]) * (D.GetDecimal(row["RT_VAT"]) / 100m));   //부가세
                        }
                        else
                        {
                            row["UM_EX"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row["UM_EX_PSO"]));
                            row["UM"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(row["UM_EX_PSO"]) * D.GetDecimal(row["RT_EXCH"]));
                            row["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_VAT"]));   //부가세
                        }
                        */
                        #endregion

                        _flexL.SumRefresh();
                    }
                }

                ShowMessage(공통메세지._작업을완료하였습니다, btn_apply_good.Text);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region ♣ 도움창 셋팅 Event

        #region ♣ Control_QueryBefore
        private void Control_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                switch (e.HelpID)
                {
                    case Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB:
                        e.HelpParam.P09_CD_PLANT = cbo_Plant.SelectedValue == null ? string.Empty : cbo_Plant.SelectedValue.ToString();
                        break;
                    case Duzon.Common.Forms.Help.HelpID.P_PU_EJTP_SUB:
                        e.HelpParam.P61_CODE1 = "010|041|042|";
                        break;
                    case Duzon.Common.Forms.Help.HelpID.P_SA_TPSO_SUB:
                        e.HelpParam.P61_CODE1 = "N";
                        e.HelpParam.P62_CODE2 = "Y";
                        break;
                    case Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB:  //제품군
                        e.HelpParam.P41_CD_FIELD1 = "MA_B000066";
                        break;
                    case HelpID.P_MA_CODE_SUB1:     //공장품목등록사용자정의7(신세계SVN에서 생산파트)
                        e.HelpParam.P41_CD_FIELD1 = "MA_B000082";
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
                    case "bp_CDSL":
                        if (BASIC.GetMAEXC("W/H 정보사용") == "100")
                        {
                            bp_WH.CodeValue = D.GetString(e.HelpReturn.Rows[0]["CD_WH"]);
                            bp_WH.CodeName = D.GetString(e.HelpReturn.Rows[0]["NM_WH"]);
                        }
                        break;
                }


                //PIMS : D20111019025, 적용버튼이 활성화되지 않아서 추가함. 최승애
                if (bp_CDSL.CodeValue == string.Empty)
                    btn_apply.Enabled = false;
                else
                    btn_apply.Enabled = true;


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
                if (_flexL == null || _flexL.DataTable == null || _flexL.DataTable.Rows.Count == 0)
                {
                    ShowMessage("조회 후 적용 하시기 바랍니다.");
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
                    row["CD_SL"] = bp_CDSL.CodeValue;
                    row["NM_SL"] = bp_CDSL.CodeName;
                    if (BASIC.GetMAEXC("W/H 정보사용") == "100")
                    {
                        row["CD_WH"] = bp_WH.CodeValue;
                        row["NM_WH"] = bp_WH.CodeName;
                    }
                }

                #region -> 현재고 조회

                string strCD_ITEMS = "";

                DataTable dtITEM = new DataTable();
                dtITEM.Columns.Add("CD_ITEM");
                dtITEM.PrimaryKey = new DataColumn[] { dtITEM.Columns["CD_ITEM"] };

                foreach (DataRow dr in _flexL.DataTable.Rows)
                {
                    DataRow drFind = dtITEM.Rows.Find(D.GetString(dr["CD_ITEM"]));
                    if (drFind != null) continue;

                    strCD_ITEMS += D.GetString(dr["CD_ITEM"]) + "|";
                    dtITEM.Rows.Add(D.GetString(dr["CD_ITEM"]));
                }

                DataTable dtInv = SearchInv(strCD_ITEMS, D.GetString(bp_CDSL.CodeValue));

                #endregion

                #region -> 현재고 반영

                foreach (DataRow dr in _flexL.DataTable.Rows)
                {
                    DataRow drFind;
                    if (Config.MA_ENV.PJT형여부 == "N")
                        drFind = dtInv.Rows.Find(new object[] { D.GetString(dr["CD_ITEM"]), D.GetString(dr["CD_SL"]) });
                    else
                        drFind = dtInv.Rows.Find(new object[] { D.GetString(dr["CD_ITEM"]), D.GetString(dr["CD_SL"]), D.GetString(dr["NO_PROJECT"]) });

                    if (drFind == null) continue;

                    if (dr.RowState == DataRowState.Unchanged)
                    {
                        dr["QT_INV"] = D.GetDecimal(drFind["QT_INV"]);
                        dr.AcceptChanges();
                    }
                    else
                    {
                        dr["QT_INV"] = D.GetDecimal(drFind["QT_INV"]);
                    }
                }

                _flexL.SumRefresh();

                #endregion

                _flexL.Redraw = true;

                ShowMessage(공통메세지._작업을완료하였습니다, btn_apply.Text);
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

        #region ♣ 창고적용 버튼 활성 비활성
        private void bp_CDSL_Leave(object sender, EventArgs e)
        {

            if (bp_CDSL.CodeValue == string.Empty)
                btn_apply.Enabled = false;
            else
                btn_apply.Enabled = true;
        }

        private void bp_CDSL_Validated(object sender, EventArgs e)
        {
            if (bp_CDSL.CodeValue == string.Empty)
                btn_apply.Enabled = false;
            else
                btn_apply.Enabled = true;
        }
        #endregion

        #region ♣ ComboBox_KeyEvent
        private void CommonComboBox_KeyEvent(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                System.Windows.Forms.SendKeys.SendWait("{TAB}");
        }
        #endregion

        #region ♣ 사용자 Default Settings
        //원래는 화면이 닫힐때 사용자가 정의한 값을 읽어서 저장해야하지만 
        //화면이 닫히는 시점을 알기 어려움으로 해당 컨트롤에 변경이 일어 났을때 바로 저장 시킨다.
        public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
        {
            if (rdo_TaxAll.Checked == true)
                Settings.Default.auto_No = "GIR";

            if (rdo_TaxEntity.Checked == true)
                Settings.Default.auto_No = "PARTNER";

            Settings.Default.의뢰비고적용여부 = chk_YN.Checked;

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
                if (rdo_Yes.Checked == true)
                {
                    object[] args = new Object[6];

                    args[0] = dt_gi.Text;                         //출하일자
                    args[1] = cbo_Plant.SelectedValue == null ? string.Empty : cbo_Plant.SelectedValue.ToString(); //공장
                    args[2] = bp_Partner.CodeValue;               //거래처코드
                    args[3] = bp_Partner.CodeName;                //거래처명
                    args[4] = bp_Emp.CodeValue;                   //사번
                    args[5] = bp_Emp.CodeName;                    //이름

                    //Main 이 살아 있는지 확인한후 살아 있으면 저장을 실행하고 죽어 있으면 그냥 리턴시켜버린다.
                    if (this.MainFrameInterface.IsExistPage("P_SA_GIM_REG", false))
                        this.UnLoadPage("P_SA_GIM_REG", false);   //특정 페이지 닫기

                    string ls_LinePageName = DD("출하관리");
                    bool isComplete = this.LoadPageFrom("P_SA_GIM_REG", ls_LinePageName, this.Grant, args);
                    if (!isComplete) ShowMessage(공통메세지.작업을정상적으로처리하지못했습니다);

                    rdo_Yes.Checked = false;
                    rdo_Not.Checked = true;
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
                        if (!_flexH.HasNormalRow) return;

                        DataTable dt = null;
                        string Key = _flexH[e.NewRange.r1, "NO_GIR"].ToString();
                        string Filter = "NO_GIR = '" + Key + "'";

                        if (_flexH.DetailQueryNeed)
                        {
                            object[] obj = new object[] {
                                LoginInfo.CompanyCode, 
                                Key, 
                                pp일자.StartDateToString, 
                                pp일자.EndDateToString, 
                                string.Empty, 
                                string.Empty, 
                                bp_GiPartner.CodeValue, 
                                bp_TpGi.CodeValue, 
                                "N",                    //처리상태가 미처리 상태 즉, 반품여부가 "N" 인것
                                D.GetString(cbo_운송방법.SelectedValue), 
                                bp_GirEmp.CodeValue, 
                                D.GetString(cbo_DtGubun.SelectedValue), 
                                bp_SaleGrp.CodeValue, 
                                bp수주형태.CodeValue,  //(D20110317037):김영민(2011-03-18) 수주형태 추가
                                bp프로젝트.CodeValue,  //(D20110509008):??????(2011-05-11) 프로젝트 추가
                                D.GetString(bp제품군.CodeValue), 
                                D.GetString(bp의뢰창고.CodeValue), 
                                D.GetString(cbo거래처그룹.SelectedValue), 
                                D.GetString(cbo거래처그룹2.SelectedValue), 
                                D.GetString(cbo사용자정의.SelectedValue), 
                                D.GetString(cbo사용자정의2.SelectedValue),
                                MA.Login.사원번호,
                                bpc생산파트.QueryWhereIn_Pipe
                            };

                            dt = _biz.SearchDetail(obj, D.GetString(cbo_DtGubun.SelectedValue));

                            #region -> 현재고 조회

                            string strCD_ITEMS = "";

                            DataTable dtITEM = new DataTable();
                            dtITEM.Columns.Add("CD_ITEM");
                            dtITEM.PrimaryKey = new DataColumn[] { dtITEM.Columns["CD_ITEM"] };

                            foreach (DataRow dr in dt.Rows)
                            {
                                DataRow drFind = dtITEM.Rows.Find(D.GetString(dr["CD_ITEM"]));
                                if (drFind != null) continue;

                                strCD_ITEMS += D.GetString(dr["CD_ITEM"]) + "|";
                                dtITEM.Rows.Add(D.GetString(dr["CD_ITEM"]));
                            }

                            DataTable dtInv = SearchInv(strCD_ITEMS, "");

                            #endregion

                            #region -> 현재고 반영

                            foreach (DataRow dr in dt.Rows)
                            {
                                DataRow drFind;
                                if (Config.MA_ENV.PJT형여부 == "N")
                                    drFind = dtInv.Rows.Find(new object[] { D.GetString(dr["CD_ITEM"]), D.GetString(dr["CD_SL"]) });
                                else
                                    drFind = dtInv.Rows.Find(new object[] { D.GetString(dr["CD_ITEM"]), D.GetString(dr["CD_SL"]), D.GetString(dr["NO_PROJECT"]) });

                                if (drFind == null) continue;

                                if (dr.RowState == DataRowState.Unchanged)
                                {
                                    dr["QT_INV"] = D.GetDecimal(drFind["QT_INV"]);
                                    dr.AcceptChanges();
                                }
                                else
                                {
                                    dr["QT_INV"] = D.GetDecimal(drFind["QT_INV"]);
                                }
                            }

                            #endregion

                            dt.DefaultView.Sort = "NO_GIR ASC, SEQ_GIR ASC";  //Ordering을 프로그램에서 함.
                            dt = dt.DefaultView.ToTable();
                        }

                        _flexL.BindingAdd(dt, Filter);
                        _flexL.SetDummyColumn("S", "CD_SL", "NM_SL", "QT_UNIT_MM", "QT_IO");    //왜 했지?
                        _flexH.DetailQueryNeed = false;

                        Common.Setting set = new Common.Setting();
                        set.GridCheck(_flexL, "S='Y'", true);
                        break;



                    //bool isNeed = _flexH.DetailQueryNeed;

                    //if (isNeed)
                    //{
                    //    DataRow[] dr = _flexL.DataTable.Select(Filter);
                    //    if (dr != null && dr.Length > 0)
                    //        isNeed = false;
                    //}

                    //if (isNeed)
                    //{
                    //    object[] obj = new object[] {
                    //        LoginInfo.CompanyCode, 
                    //        Key, 
                    //        dt_Gir_From.Text, 
                    //        dt_Gir_To.Text, 
                    //        string.Empty, 
                    //        string.Empty, 
                    //        bp_GiPartner.CodeValue, 
                    //        bp_TpGi.CodeValue, 
                    //        "N",                    //처리상태가 미처리 상태 즉, 반품여부가 "N" 인것
                    //        D.GetString(cbo_운송방법.SelectedValue), 
                    //        bp_GirEmp.CodeValue, 
                    //        D.GetString(cbo_DtGubun.SelectedValue), 
                    //        bp_SaleGrp.CodeValue, 
                    //        bp수주형태.CodeValue,  //(D20110317037):김영민(2011-03-18) 수주형태 추가
                    //        bp프로젝트.CodeValue     //(D20110509008):??????(2011-05-11) 프로젝트 추가
                    //    };

                    //    dt = _biz.SearchDetail(obj);
                    //    dt.DefaultView.Sort = "NO_GIR ASC, SEQ_GIR ASC";  //Ordering을 프로그램에서 함.
                    //    dt = dt.DefaultView.ToTable();
                    //}
                    //_flexL.BindingAdd(dt, Filter);
                    //_flexL.SetDummyColumn("S", "CD_SL", "NM_SL", "QT_UNIT_MM", "QT_IO");
                    //_flexH.DetailQueryNeed = false;

                    //Common.Setting set = new Common.Setting();
                    //set.GridCheck(_flexL, "S='Y'", true);
                    //break;
                    case "_flexL":
                        txt_GiPartner.Text = _flexL[_flexL.Row, "LN_PARTNER"].ToString();	//납품처
                        txt_NoSo.Text = _flexL[_flexL.Row, "NO_PSO_MGMT"].ToString();		//수주번호
                        txt_NoLc.Text = _flexL[_flexL.Row, "NO_LC"].ToString();				//LC번호
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

        #region -> _flexL_AfterCodeHelp

        void _flexL_AfterCodeHelp(object sender, AfterCodeHelpEventArgs e)
        {
            try
            {
                switch (e.Result.HelpID)
                {
                    case HelpID.P_MA_SL_SUB:
                        #region -> 현재고 조회

                        string strCD_ITEMS = D.GetString(_flexL[e.Row, "CD_ITEM"]) + "|";

                        DataTable dtInv = SearchInv(strCD_ITEMS, D.GetString(e.Result.CodeValue));

                        #endregion

                        #region -> 현재고 반영

                        DataRow drFind;
                        if (Config.MA_ENV.PJT형여부 == "N")
                            drFind = dtInv.Rows.Find(new object[] { D.GetString(_flexL[e.Row, "CD_ITEM"]), D.GetString(e.Result.CodeValue) });
                        else
                            drFind = dtInv.Rows.Find(new object[] { D.GetString(_flexL[e.Row, "CD_ITEM"]), D.GetString(e.Result.CodeValue), D.GetString(_flexL[e.Row, "NO_PROJECT"]) });

                        if (drFind == null) return;

                        if (_flexL.DataView[_flexL.Rows.Count - _flexL.Rows.Fixed - 1].Row.RowState == DataRowState.Unchanged)
                        {
                            _flexL[e.Row, "QT_INV"] = D.GetDecimal(drFind["QT_INV"]);
                            _flexL.DataView[_flexL.Rows.Count - _flexL.Rows.Fixed - 1].Row.AcceptChanges();
                        }
                        else
                        {
                            _flexL[e.Row, "QT_INV"] = D.GetDecimal(drFind["QT_INV"]);
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

        #region ♣ _flex_CheckHeaderClick
        private void _flex_CheckHeaderClick(object sender, EventArgs e)
        {
            try
            {
                //2011-12-09, 최승애, 그리드에 데이터가 없을때 헤더클릭하면 오류가 발생되어서 아래 한줄 추가함.
                if (!_flexH.HasNormalRow || !_flexL.HasNormalRow) return;

                FlexGrid flex = sender as FlexGrid;

                switch (flex.Name)
                {
                    case "_flexH":  //상단 그리드 Header Click 이벤트

                        Common.Setting set = new Common.Setting();

                        #region -> 전제 체크해제하는 경우
                        if (_flexH.GetCellCheck(_flexH.Row, _flexH.Cols["S"].Index) == CheckEnum.Unchecked)
                        {
                            set.GridCheck(_flexL, "S = 'Y'", false);
                            return;
                        }
                        #endregion

                        #region -> 전제 체크하는 경우
                        string multiNoGir = string.Empty;

                        for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
                        {
                            if (!_flexH.DetailQueryNeedByRow(i)) continue;

                            multiNoGir += D.GetString(_flexH[i, "NO_GIR"]) + "|";
                            _flexH.SetDetailQueryNeedByRow(i, false);
                        }

                        if (multiNoGir == string.Empty)
                        {
                            set.GridCheck(_flexL, "S = 'N'", true);
                            return;
                        }

                        object[] obj = new object[]
                        {
                            LoginInfo.CompanyCode, 
                            "", 
                            pp일자.StartDateToString, 
                            pp일자.EndDateToString, 
                            string.Empty, 
                            string.Empty, 
                            bp_GiPartner.CodeValue, 
                            bp_TpGi.CodeValue, 
                            "N",                    //처리상태가 미처리 상태 즉, 반품여부가 "N" 인것
                            D.GetString(cbo_운송방법.SelectedValue), 
                            D.GetString(bp_GirEmp.CodeValue), 
                            D.GetString(cbo_DtGubun.SelectedValue), 
                            D.GetString(bp_SaleGrp.CodeValue), 
                            D.GetString(bp수주형태.CodeValue),  //(D20110317037):김영민(2011-03-18) 수주형태 추가
                            D.GetString(bp프로젝트.CodeValue),  //(D20110509008):??????(2011-05-11) 프로젝트 추가
                            D.GetString(bp제품군.CodeValue), 
                            D.GetString(bp의뢰창고.CodeValue), 
                            D.GetString(cbo거래처그룹.SelectedValue), 
                            D.GetString(cbo거래처그룹2.SelectedValue), 
                            D.GetString(cbo사용자정의.SelectedValue), 
                            D.GetString(cbo사용자정의2.SelectedValue),
                            MA.Login.사원번호,
                            bpc생산파트.QueryWhereIn_Pipe
                        };

                        DataTable dt = _biz.SearchCheckHeader(multiNoGir, obj);

                        #region -> 현재고 조회

                        string strCD_ITEMS = "";

                        DataTable dtITEM = new DataTable();
                        dtITEM.Columns.Add("CD_ITEM");
                        dtITEM.PrimaryKey = new DataColumn[] { dtITEM.Columns["CD_ITEM"] };

                        foreach (DataRow dr in dt.Rows)
                        {
                            DataRow drFind = dtITEM.Rows.Find(D.GetString(dr["CD_ITEM"]));
                            if (drFind != null) continue;

                            strCD_ITEMS += D.GetString(dr["CD_ITEM"]) + "|";
                            dtITEM.Rows.Add(D.GetString(dr["CD_ITEM"]));
                        }

                        DataTable dtInv = SearchInv(strCD_ITEMS, "");

                        #endregion

                        #region -> 현재고 반영

                        foreach (DataRow dr in dt.Rows)
                        {
                            DataRow drFind;
                            if (Config.MA_ENV.PJT형여부 == "N")
                                drFind = dtInv.Rows.Find(new object[] { D.GetString(dr["CD_ITEM"]), D.GetString(dr["CD_SL"]) });
                            else
                                drFind = dtInv.Rows.Find(new object[] { D.GetString(dr["CD_ITEM"]), D.GetString(dr["CD_SL"]), D.GetString(dr["NO_PROJECT"]) });

                            if (drFind == null) continue;

                            if (dr.RowState == DataRowState.Unchanged)
                            {
                                dr["QT_INV"] = D.GetDecimal(drFind["QT_INV"]);
                                dr.AcceptChanges();
                            }
                            else
                            {
                                dr["QT_INV"] = D.GetDecimal(drFind["QT_INV"]);
                            }
                        }

                        #endregion

                        string filter = "NO_GIR = '" + D.GetString(_flexH["NO_GIR"]) + "'";

                        _flexL.BindingAdd(dt, filter);
                        set.GridCheck(_flexL, "S = 'N'", true);
                        #endregion

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

                //삭제버튼과 인쇄버튼은 이화면에서 필요 없음으로 무조건 비활성한다.
                Auth();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
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

                                for (int i = _flexL.Rows.Fixed; i < _flexL.Rows.Count; i++)
                                {
                                    _flexL[i, "S"] = e.Checkbox == CheckEnum.Checked ? "Y" : "N";
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

                            case "QT_UNIT_MM":  //출하수량

                                if (D.GetDecimal(newValue) <= decimal.Zero)
                                {
                                    ShowMessage(공통메세지._은_보다커야합니다, _flexL.Cols["QT_UNIT_MM"].Caption, "0");
                                    _flexL["QT_UNIT_MM"] = oldValue;
                                    e.Cancel = true;
                                    return;
                                }

                                #region ♣ 출하 수량 선택하면 체크박스 자동으로 선택되게 하기
                                if (D.GetDecimal(newValue) == decimal.Zero)
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
                                decimal qtso_AddAllow = D.GetDecimal(_flexL["QT_GIR"]) + (D.GetDecimal(_flexL["QT_GIR"]) * (D.GetDecimal(_flexL["RT_PLUS"]) / 100));

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
                                        ShowMessage("출하수량이 의뢰수량을 초과하였습니다.");
                                    }
                                }

                                if (D.GetDecimal(_flexL["UNIT_SO_FACT"]) == 0)
                                    _flexL["QT_IO"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_flexL["QT_UNIT_MM"]));
                                else
                                    _flexL["QT_IO"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_flexL["QT_UNIT_MM"]) * D.GetDecimal(_flexL["UNIT_SO_FACT"]));

                                _flexL["QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_flexL["QT_IO"]));

                                if (D.GetDecimal(newValue) == decimal.Zero)
                                {
                                    _flexL["AM_EX"] = decimal.Zero;
                                    _flexL["AM"] = decimal.Zero;
                                    _flexL["UM_EX"] = decimal.Zero;
                                    _flexL["UM"] = decimal.Zero;
                                }
                                else if (D.GetDecimal(newValue) != D.GetDecimal(_flexL["QT_GIR"]))
                                {
                                    if (D.GetString(_flexL["TP_UM_TAX"]) != "Y" || (Global.MainFrame.ServerKeyCommon != "DZSQL" && Global.MainFrame.ServerKeyCommon != "GNSD" && !is중국고객부가세포함단가사용여부))
                                    {
                                        _flexL["AM_EX"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL["UM_EX_PSO"]) * D.GetDecimal(newValue));
                                        _flexL["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL["AM_EX"]) * D.GetDecimal(_flexL["RT_EXCH"]));
                                        _flexL["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL["AM"]) * (D.GetDecimal(_flexL["RT_VAT"]) / 100));
                                        decimal amVat = D.GetDecimal(_flexL["AM"]) + D.GetDecimal(_flexL["VAT"]);
                                        _flexL["UMVAT_GI"] = Unit.원화단가(DataDictionaryTypes.SA, amVat / D.GetDecimal(newValue));
                                    }
                                    else
                                    {
                                        decimal amVat = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(newValue) * D.GetDecimal(_flexL["UMVAT_GI"]));
                                        if (Global.MainFrame.LoginInfo.CompanyLanguage == Language.KR)
                                            _flexL["AM"] = Decimal.Round(amVat * (100 / (100 + D.GetDecimal(_flexL["RT_VAT"]))), MidpointRounding.AwayFromZero);
                                        else
                                            _flexL["AM"] = D.GetDecimal(UDecimal.GetConvertFormatData(DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.INSERT, amVat * (100 / (100 + D.GetDecimal(_flexL["RT_VAT"])))));
                                        _flexL["VAT"] = amVat - D.GetDecimal(_flexL["AM"]);
                                        _flexL["AM_EX"] = D.GetDecimal(_flexL["RT_EXCH"]) == 0m ? 0m : Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL["AM"]) / D.GetDecimal(_flexL["RT_EXCH"]));
                                    }
                                }
                                //남은 잔량과 동일하게 입력했을 시
                                else
                                {
                                    if (D.GetDecimal(_flexL["CHK_QT_GI"]) != 0)
                                    {
                                        //부가세포함단가 미사용
                                        if (D.GetString(_flexL["TP_UM_TAX"]) != "Y" || (Global.MainFrame.ServerKeyCommon != "DZSQL" && Global.MainFrame.ServerKeyCommon != "GNSD" && !is중국고객부가세포함단가사용여부))
                                        {
                                            //금액잔량관리 안함(금액 = 수량 * 단가)
                                            if (Am_Recalc == "000")
                                            {
                                                _flexL["AM_EX"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL["UM_EX_PSO"]) * D.GetDecimal(newValue));
                                                _flexL["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL["AM_EX"]) * D.GetDecimal(_flexL["RT_EXCH"]));
                                                _flexL["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL["AM"]) * (D.GetDecimal(_flexL["RT_VAT"]) / 100));
                                            }
                                            //금액잔량관리 함(금액 = 의뢰금액 - 기출하금액)
                                            else
                                            {
                                                _flexL["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL["ORG_AM"]));
                                                _flexL["AM_EX"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL["ORG_AM_EX"]));
                                                _flexL["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL["AM"]) * (D.GetDecimal(_flexL["RT_VAT"]) / 100));
                                            }

                                            decimal amVat = D.GetDecimal(_flexL["AM"]) + D.GetDecimal(_flexL["VAT"]);
                                            _flexL["UMVAT_GI"] = Unit.원화단가(DataDictionaryTypes.SA, amVat / D.GetDecimal(newValue));
                                        }
                                        //부가세포함단가 사용
                                        else
                                        {
                                            decimal amVat = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(newValue) * D.GetDecimal(_flexL["UMVAT_GI"]));
                                            if (Global.MainFrame.LoginInfo.CompanyLanguage == Language.KR)
                                                _flexL["AM"] = Decimal.Round(amVat * (100 / (100 + D.GetDecimal(_flexL["RT_VAT"]))), MidpointRounding.AwayFromZero);
                                            else
                                                _flexL["AM"] = D.GetDecimal(UDecimal.GetConvertFormatData(DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.INSERT, amVat * (100 / (100 + D.GetDecimal(_flexL["RT_VAT"])))));
                                            _flexL["VAT"] = amVat - D.GetDecimal(_flexL["AM"]);
                                            _flexL["AM_EX"] = D.GetDecimal(_flexL["RT_EXCH"]) == 0m ? 0m : Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL["AM"]) / D.GetDecimal(_flexL["RT_EXCH"]));
                                        }
                                    }
                                    else
                                    {
                                        //의뢰수량(분할 X) 만큼 출하수량 입력
                                        _flexL["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL["ORG_AM"]));
                                        _flexL["AM_EX"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL["ORG_AM_EX"]));
                                        _flexL["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL["AM_VAT"]));
                                    }
                                }

                                _flexL["UM_EX"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flexL["AM_EX"]) / D.GetDecimal(_flexL["QT_IO"]));
                                _flexL["UM"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(_flexL["AM"]) / D.GetDecimal(_flexL["QT_IO"]));

                                if (D.GetString(_flexL["TP_UM_TAX"]) != "Y" || (Global.MainFrame.ServerKeyCommon != "DZSQL" && Global.MainFrame.ServerKeyCommon != "GNSD" && !is중국고객부가세포함단가사용여부))
                                {
                                    if (Am_Recalc == "001" || D.GetDecimal(_flexL["CHK_QT_GI"]) == 0) //001 재계산 안함
                                    {
                                        {
                                            _flexL["UM_EX"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flexL["UM_EX_PSO"]));
                                            _flexL["UM"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(_flexL["UM_EX_PSO"]) * D.GetDecimal(_flexL["RT_EXCH"]));
                                        }
                                    }
                                }


                                #region 기존 수량입력 계산로직
                                /*
                                //시스템 통제: 금액
                                if (D.GetDecimal(newValue) != D.GetDecimal(_flexL["QT_GIR"]))
                                {
                                    //수량 수정시는 무조건 재계산
                                    //재계산
                                    _flexL["AM_EX"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL["UM_EX_PSO"]) * D.GetDecimal(newValue));
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
                                        _flexL["UM_EX"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flexL["AM_EX"]) / D.GetDecimal(_flexL["QT_IO"]));
                                        _flexL["UM"] = Unit.원화단가(DataDictionaryTypes.SA, (D.GetDecimal(_flexL["AM_EX"]) * D.GetDecimal(_flexL["RT_EXCH"])) / D.GetDecimal(_flexL["QT_IO"]));
                                    }
                                }

                                _flexL["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL["AM"]) * (D.GetDecimal(_flexL["RT_VAT"]) / 100));

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
                                        _flexL["UM_EX"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flexL["UM_EX_PSO"]));
                                        _flexL["UM"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(_flexL["UM_EX_PSO"]) * D.GetDecimal(_flexL["RT_EXCH"]));
                                    }
                                }*/
                                #endregion
                                break;

                            case "QT_IO":   //관리수량
                                _flexL["QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(newValue));
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

                //삭제버튼과 인쇄버튼은 이화면에서 필요 없음으로 무조건 비활성한다.
                Auth();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region ♣ _flex_DoubleClick

        void _flex_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (!_flexL.HasNormalRow) return;

                if (D.GetDecimal(_flexL["QT_UNIT_MM"]) > 0) return;

                if (D.GetString(ComFunc.전용코드("출하등록-재고출하반려도움창(제다전용)")) != "001")
                    return;

                if (_flexL.Cols[_flexL.Col].Name == "QT_GIR")
                {
                    P_SA_SO_ADJUST_BAN_SUB dlg = new P_SA_SO_ADJUST_BAN_SUB(D.GetString(_flexL["NO_PSO_MGMT"]), D.GetDecimal(_flexL["NO_PSOLINE_MGMT"]));

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
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
                    string cd_item_multi = "";
                    cd_item_multi = Common.MultiString(_flexL.DataView.ToTable(), "CD_ITEM", "|");

                    pur.P_PU_STOCK_SUB m_dlg = new pur.P_PU_STOCK_SUB(cbo_Plant.SelectedValue.ToString(), cd_item_multi);
                    m_dlg.ShowDialog(this);
                    break;
            }
        }
        #endregion

        #region -> 현재고 조회(SearchInv)

        DataTable SearchInv(string strCD_ITEMS, string strCD_SL)
        {
            object[] objInv = new object[] {
                        LoginInfo.CompanyCode, 
                        D.GetString(cbo_Plant.SelectedValue), 
                        strCD_ITEMS, 
                        strCD_SL
                    };

            DataTable dtInv = _biz.SearchInv(objInv);

            if (Config.MA_ENV.PJT형여부 == "N")
                dtInv.PrimaryKey = new DataColumn[] { dtInv.Columns["CD_ITEM"], dtInv.Columns["CD_SL"] };
            else
                dtInv.PrimaryKey = new DataColumn[] { dtInv.Columns["CD_ITEM"], dtInv.Columns["CD_SL"], dtInv.Columns["CD_PJT"] };

            return dtInv;
        }

        #endregion

        #region ♣ 속성
        #region -> 일자
        bool Chk일자 { get { return Checker.IsValid(pp일자, true, DD("조회일")); } }
        #endregion
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

    }
}
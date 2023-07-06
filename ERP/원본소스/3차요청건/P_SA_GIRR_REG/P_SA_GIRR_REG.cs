using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.SA;
using Duzon.ERPU.SA.Common;
using Duzon.ERPU.OLD;
using DzHelpFormLib; 
 
namespace sale
{
    // **************************************
    // 작   성   자 : 허성철
    // 재 작  성 일 : 2007-03-02
    // 모   듈   명 : 영업
    // 시 스  템 명 : 영업관리
    // 서브시스템명 : 반품의뢰관리
    // 페 이 지  명 : 출고반품의뢰등록
    // 프로젝트  명 : P_SA_GIRR_REG
    //======================================
    // 수 정  내 역 :  PIMS : D20110926036, 최승애, 2011-09-28, 최승애
    //2013.04.12 : D20130326009 : 삼보컴퓨터전용 출력프로시저 추가
    // **************************************
    public partial class P_SA_GIRR_REG : Duzon.Common.Forms.PageBase
    {
        #region ♣ 멤버필드

        private P_SA_GIRR_REG_BIZ _biz = new P_SA_GIRR_REG_BIZ();
        private FreeBinding _header = new FreeBinding();
        bool _헤더수정여부 = true;
        string _매출형태 = ""; string _매출유무 = "";
        string _단가적용형태 = "";              //영업그룹을 선택시 단가정보(TP_SALEPRICE)도 가져온다.
        //수주등록에서는 영업그룹을 선택시 판매단가통제유무(SO_PRICE)도 가져온다.
        //반품의뢰시에는 단가통제를 하지 않는다.
        string so_Price = "N";

        //영우에 전용설정위한 Flag 셋팅
        private string _전용설정 = "000";

        //영업환경설정  
        /*할인율 적용 여부에 대한 부연 설명*/
        /*할인율 적용은 기존 거래처별 할인이나 유형별 단가와 별개로 중복 할인도 가능하다~
         *이것은 기존 단가 통제 로직 이후에 할인율을 적용 해줘야 한다. 
         *할인율 적용여부가 N 일 경우에는 기존 단가 컬럼에 단가가 들어가서 수량 * 단가 = 공급가액 으로 계산되었으나 
         *할인율 적용여부가 Y 일 경우에는 기준단가(UM_BASE) 에 단가가 들어가서 단가 = 기준단가 - (기준단가*할인율)/100 을 한 단가를 구한뒤에
         *수량 * 계산된 단가(기존단가컬럼) = 공급가액 으로 계산되어야 한다.
         *할인율 적용 여부에 따라 기준단가와 할인율의 그리드 컬럼이 Visible true/false 되어야 하며 
         *표준으로 적용되어야 할 사항이다.
         */
        private string disCount_YN = "N";  //할인율 적용여부 추가 2009.07.16 NJin (Default Value = "N" 으로 셋팅)
        string _배송여부 = string.Empty;    //반품형태 선택시 배송여부 디폴트값을 넣어준다

        private 수주관리.Config 수주Config = new 수주관리.Config();
        private bool is중국고객부가세포함단가사용여부 = false;

        #endregion

        #region ♣ 초기화

        #region -> 생성자

        public P_SA_GIRR_REG()
        {
            try
            {
                InitializeComponent();
                MainGrids = new FlexGrid[] { _flex };
                DataChanged += new EventHandler(Page_DataChanged);

                _header.JobModeChanged += new FreeBindingEventHandler(_header_JobModeChanged);
                _header.ControlValueChanged += new FreeBindingEventHandler(_header_ControlValueChanged);

                is중국고객부가세포함단가사용여부 = 중국고객부가세포함단가사용여부();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> InitLoad

        protected override void InitLoad()
        {
            base.InitLoad();
            InitGrid();
            InitEvent();
        }

        #endregion

        #region -> InitGrid

        private void InitGrid()
        {
            _flex.BeginSetting(1, 1, true);
            _flex.SetCol("S", "선택", 50, true, CheckTypeEnum.Y_N);     //2011-09-28, 선택 컬럼 추가, 최승애
            _flex.SetCol("CD_ITEM", "품목코드", 100, 20, true);
            _flex.SetCol("NM_ITEM", "품목명", 140, 50, false);
            _flex.SetCol("STND_ITEM", "규격", 120, 50, false);
            _flex.SetCol("UNIT", "단위", 40, false);
            _flex.SetCol("DT_DUEDATE", "납기일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex.SetCol("CD_SL", "창고코드", 80, 7, true);
            _flex.SetCol("NM_SL", "창고명", 100, 50, false);
            _flex.SetCol("QT_GIR", "수량", 90, 17, true, typeof(decimal), FormatTpType.QUANTITY);

            if (Use유무환전환)
            {
                _flex.SetCol("UM", "단가", 90, 17, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                _flex.SetCol("AM_GIR", "금액", 90, 17, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                _flex.SetCol("AM_GIRAMT", "원화금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
                _flex.SetCol("AM_VAT", "부가세", 90, 17, false, typeof(decimal), FormatTpType.MONEY);

                _flex.SetCol("UM_SO", "수주단가", 90, 17, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                _flex.SetCol("AM_SO", "수주금액", 90, 17, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                _flex.SetCol("AM_SOAMT", "수주원화금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
                _flex.SetCol("AM_SOVAT", "수주부가세", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            }
            else
            {
                _flex.SetCol("UM", "단가", 90, 17, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                _flex.SetCol("AM_GIR", "금액", 90, 17, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                _flex.SetCol("AM_GIRAMT", "원화금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
                _flex.SetCol("AM_VAT", "부가세", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
                _flex.SetCol("AMVAT_GIR", "합계금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            }

            _flex.SetCol("QT_GIR_IM", "재고수량", 90, 17, true, typeof(decimal), FormatTpType.QUANTITY);
            _flex.SetCol("NO_SO", "수주반품번호", 110, 20, false);
            _flex.SetCol("NO_IO_MGMT", "수불번호", 110, 20, false);
            _flex.SetCol("NO_SO_MGMT", "수주번호", 110, 20, false);
            _flex.SetCol("GI_PARTNER", "납품처코드", 100, 20, true);
            _flex.SetCol("LN_PARTNER", "납품처명", 200, 50, false);
            _flex.SetCol("NO_PROJECT", "프로젝트코드", 100, 20, true);
            _flex.SetCol("NM_PROJECT", "프로젝트명", 120, 50, false);
            _flex.SetCol("DC_RMK", "비고", 120, 100, true);

            DataTable dt출하반품_검사 = _biz.GetMAENV("출하반품_검사");

            string str출하반품_검사 = "000";

            if (dt출하반품_검사.Rows.Count > 0)
                str출하반품_검사 = D.GetString(dt출하반품_검사.Rows[0]["CD_EXC"]);

            if (str출하반품_검사 == "100")
            {
                _flex.SetCol("YN_INSPECT", "검사여부", 50, true, CheckTypeEnum.Y_N);
                _flex.SetCol("QT_QC_PASS", "검사합격수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
                _flex.SetCol("QT_QC_BAD", "검사불량수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
                _flex.SetCol("YN_QC_FIX", "검사확정", 50, false);
            }

            if (_biz.Get과세변경유무 == "Y" && 서버키 == "KOREAF")
            {
                _flex.SetCol("TP_VAT", "VAT구분", 80, true);
                _flex.SetCol("RT_VAT", "VAT율", 70, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            }

            if (BASIC.GetMAEXC("SET품 사용유무") == "Y")
            {
                _flex.SetCol("CD_ITEM_REF", "SET품목", 120, false);
                _flex.SetCol("NM_ITEM_REF", "SET품명", 120, false);
                _flex.SetCol("STND_ITEM_REF", "SET규격", 120, false);
            }

            if (BASIC.GetMAEXC("배차사용유무") == "Y")
                _flex.SetCol("YN_PICKING", "배송여부", 80, false, CheckTypeEnum.Y_N);

            if (수주Config.부가세포함단가사용())
            {
                _flex.SetCol("TP_UM_TAX", "부가세여부", 90, false);
                _flex.SetCol("UMVAT_GIR", "부가세포함단가", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            }

            if (Config.MA_ENV.YN_UNIT == "Y")
            {
                _flex.SetCol("SEQ_PROJECT", "UNIT 항번", 100, false, typeof(decimal));
                _flex.SetCol("CD_UNIT", "UNIT 코드", 100, false);
                _flex.SetCol("NM_UNIT", "UNIT 명", 100, false);
                _flex.SetCol("STND_UNIT", "UNIT 규격", 100, false);
            }
            _flex.SetCodeHelpCol("CD_ITEM", HelpID.P_MA_PITEM_SUB1, ShowHelpEnum.Always, new string[] { "CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT", "TP_ITEM", "CD_SL", "NM_SL" }, new string[] { "CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT_SO", "TP_ITEM", "CD_GISL", "NM_GISL" }, Duzon.Common.Forms.Help.ResultMode.SlowMode);
            _flex.SetCodeHelpCol("CD_SL", HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CD_SL", "NM_SL" }, new string[] { "CD_SL", "NM_SL" }, Duzon.Common.Forms.Help.ResultMode.SlowMode);
            _flex.SetCodeHelpCol("NM_SL", HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CD_SL", "NM_SL" }, new string[] { "CD_SL", "NM_SL" }, Duzon.Common.Forms.Help.ResultMode.SlowMode);
            _flex.SetCodeHelpCol("GI_PARTNER", HelpID.P_SA_TPPTR_SUB, ShowHelpEnum.Always, new string[] { "GI_PARTNER", "LN_PARTNER" }, new string[] { "CD_TPPTR", "NM_TPPTR" });
            _flex.SetCodeHelpCol("NO_PROJECT", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new string[] { "NO_PROJECT", "NM_PROJECT" }, new string[] { "NO_PROJECT", "NM_PROJECT" });

            if (Use유무환전환)
                _flex.SetExceptEditCol("NM_ITEM", "STND_ITEM", "UNIT", "NM_SL", "AM_GIRAMT", "AM_VAT", "NO_SO", "NO_IO_MGMT", "NO_SO_MGMT", "NM_PROJECT", "AM_SOAMT", "AM_SOVAT");
            else
            {
                if (_biz.Get과세변경유무 == "Y" && 서버키 == "KOREAF")
                    _flex.SetExceptEditCol("NM_ITEM", "STND_ITEM", "UNIT", "NM_SL", "AM_GIRAMT", "AM_VAT", "NO_SO", "NO_IO_MGMT", "NO_SO_MGMT", "NM_PROJECT", "AMVAT_GIR", "RT_VAT");
                else
                    _flex.SetExceptEditCol("NM_ITEM", "STND_ITEM", "UNIT", "NM_SL", "AM_GIRAMT", "AM_VAT", "NO_SO", "NO_IO_MGMT", "NO_SO_MGMT", "NM_PROJECT", "AMVAT_GIR");
            }

            //의뢰S/L 필수여부에 따라 창고코드 필수사항으로 수정(2011/01/29, BY SMJUNG)
            if (App.SystemEnv.PROJECT사용 && BASIC.GetMAEXC("의뢰 S/L 필수여부") == "100")
                _flex.VerifyNotNull = new string[] { "CD_ITEM", "CD_SL", "NO_PROJECT" };
            else if (App.SystemEnv.PROJECT사용)
                _flex.VerifyNotNull = new string[] { "CD_ITEM", "NO_PROJECT" };
            if (BASIC.GetMAEXC("의뢰 S/L 필수여부") == "100" || Use유무환전환)
                _flex.VerifyNotNull = new string[] { "CD_ITEM", "CD_SL" };

            _flex.SetDummyColumn("S");
            _flex.SettingVersion = "1.0.0.5";
            _flex.VerifyAutoDelete = new string[] { "CD_ITEM" };
            _flex.VerifyCompare(_flex.Cols["QT_GIR"], 0, OperatorEnum.Greater);
            _flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            _flex.StartEdit += new RowColEventHandler(_flex_StartEdit);
            _flex.BeforeCodeHelp += new BeforeCodeHelpEventHandler(_flex_BeforeCodeHelp);
            _flex.AfterCodeHelp += new AfterCodeHelpEventHandler(_flex_AfterCodeHelp);
            _flex.ValidateEdit += new ValidateEditEventHandler(_flex_ValidateEdit);
            _flex.AddRow += new EventHandler(btn추가_Click);

            //헤더에 필요없는 SUM 제거 && 반듯이 EndSetting 다음에 코딩해줘야한다.

            if (Use유무환전환)
            {
                if (Config.MA_ENV.YN_UNIT == "Y")
                    _flex.SetExceptSumCol("UM", "UM_SO", "UMVAT_GIR", "SEQ_PROJECT");
                else
                    _flex.SetExceptSumCol("UM", "UM_SO", "UMVAT_GIR");
            }
            else
            {
                if (Config.MA_ENV.YN_UNIT == "Y")
                    _flex.SetExceptSumCol("UM", "UMVAT_GIR", "SEQ_PROJECT");
                else
                    _flex.SetExceptSumCol("UM", "UMVAT_GIR");
            }
        }
         
        #endregion

        #region -> InitPaint

        protected override void InitPaint()
        {
            m_pnlHead.UseCustomLayout = pnl유무환전환.UseCustomLayout = true;
            m_pnlHead.IsSearchControl = false;
            bpPanelControl1.IsNecessaryCondition = bpPanelControl2.IsNecessaryCondition = bpPanelControl3.IsNecessaryCondition =
            bpPanelControl4.IsNecessaryCondition = bpPanelControl8.IsNecessaryCondition = bpPanelControl9.IsNecessaryCondition =
            bpPanelControl16.IsNecessaryCondition = true;
            pnl유무환전환.IsSearchControl = false;
            m_pnlHead.InitCustomLayout();
            pnl유무환전환.InitCustomLayout();

            if (Use유무환전환)
                pnlButton.Visible = false;
            else
                pnl유무환전환.Visible = false;

            dp의뢰일자.Mask = GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            dp의뢰일자.ToDayDate = MainFrameInterface.GetDateTimeToday();
            dp의뢰일자.Text = MainFrameInterface.GetStringToday;

            //PageBase 의 부모함수 이용 : 폼이 올라올 후 폼위에 올라온 컨트롤들을 초기화 시켜주는 역할을 한다.
            //Combo Box 셋팅시 옵션 => N:공백없음, S:공백추가, SC:공백&괄호추가, NC:괄호추가
            //뒤의 코드값들은 DB 에서 정해놓은 파라미터와 매치시켜줘야한다
            DataSet ds = GetComboData("N;MA_PLANT", "N;PU_C000016", "N;MA_CODEDTL_005;MA_B000040", "S;MA_B000005", "S;SA_B000021");

            ds.Tables[2].PrimaryKey = new DataColumn[] { ds.Tables[2].Columns["CODE"] };
            
            cbo공장.DataSource = ds.Tables[0];
            cbo공장.DisplayMember = "NAME";
            cbo공장.ValueMember = "CODE";

            cbo거래구분.DataSource = new DataView(ds.Tables[1], "CODE IN ('001', '002', '003', '004', '005')", "CODE", DataViewRowState.CurrentRows);
            cbo거래구분.DisplayMember = "NAME";
            cbo거래구분.ValueMember = "CODE";

            cboVAT구분.DataSource = ds.Tables[2];
            cboVAT구분.DisplayMember = "NAME";
            cboVAT구분.ValueMember = "CODE";

            cbo단가유형.DataSource = ds.Tables[4];
            cbo단가유형.DisplayMember = "NAME";
            cbo단가유형.ValueMember = "CODE";
            cbo단가유형.SelectedValue = "001";

            // 프리폼 초기화
            object[] obj = new object[3];
            obj[0] = "";    //회사코드
            obj[1] = "";    // 의뢰번호
            obj[2] = "";    // 공장

            DataSet dsTemp = _biz.Search(obj);

            _header.SetBinding(dsTemp.Tables[0], m_pnlHead);
            _header.ClearAndNewRow();                       // 처음에 행을 하나 만든다. 초기에 추가모드로 하기 위해

            _flex.Binding = dsTemp.Tables[1];

            SetControl str = new SetControl();
            str.SetCombobox(cbo화폐단위, MA.GetCode("MA_B000005", true));
            str.SetCombobox(cbo수주화폐단위, MA.GetCode("MA_B000005", true));
            str.SetCombobox(cbo부가세포함, MA.GetCode("YESNO"));
            cbo화폐단위.SelectedValue = "000";
            cbo수주화폐단위.SelectedValue = "000";

            화폐단위셋팅(cbo화폐단위, cur환율);
            화폐단위셋팅(cbo수주화폐단위, cur수주환율);
            VAT구분셋팅();

            if (_biz.Get과세변경유무 == "Y" && 서버키 == "KOREAF")
                _flex.SetDataMap("TP_VAT", ds.Tables[2].Copy(), "CODE", "NAME");

            //영우만 엑셀업로드버튼과 추가버튼을 보여주지 않는다.
            //전용코드 조회 : 영우
            DataTable dt_exc = _biz.GetPartnerCodeSearch();

            if (dt_exc.Rows.Count > 0)
            {
                // 000:기본(행추가) 100:(행추가버튼사용안함)
                if (dt_exc.Rows[0]["CD_EXC"] != System.DBNull.Value && dt_exc.Rows[0]["CD_EXC"].ToString().Trim() != "")
                {
                    _전용설정 = dt_exc.Rows[0]["CD_EXC"].ToString().Trim();
                }
            }

            if (_전용설정 == "100") //행추가버튼사용안할경우
            {
                btn추가.Visible = false;
                btn엑셀업로드.Visible = false;
            }

            if (_biz.Get영업그룹적용유무 == "Y")  //거래처부가정보의 거래처와영업그룹매핑을 할 경우
            {
                bp영업그룹.Enabled = false;
            }

            //if (_biz.Get담당자적용유무 == "Y")
            //{
            //    bp담당자.Enabled = false;
            //}

            if (_biz.Get수주반품사용여부)
            {
                //btn수주반품적용.Location = btn출하적용.Location;
                btn수주반품적용.Visible = true;
            }
            else
            {
                btn출하적용.Visible = true;
            }

            //영업환경설정 : 수주수량 초과허용 : 000 , 재고단위 EDIT 여부(2중단위관리 ) : 001 , 할인율 적용여부 : 002
            DataTable dt = _biz.search_EnvMng();

            if (dt.Rows.Count > 0)
            {
                // 000:기본 100:평화 200:영우 (null이거나 ''은 000으로 치환) 
                if (dt.Rows[0]["FG_TP"] != System.DBNull.Value && dt.Rows[0]["FG_TP"].ToString().Trim() != String.Empty)
                {
                    disCount_YN = dt.Select("FG_TP = '002'")[0]["CD_TP"].ToString();    //할인율 적용여부 : 002
                }
            }

            bp영업그룹.SetCodeValue(Global.MainFrame.LoginInfo.SalesGroupCode);
            bp영업그룹.SetCodeName(Global.MainFrame.LoginInfo.SalesGroupName);

            DataRow row영업그룹 = null;
            try
            {
                row영업그룹 = BASIC.GetSaleGrp(D.GetString(bp영업그룹.CodeValue));
            }
            catch { }

            _단가적용형태 = row영업그룹 == null ? "" : D.GetString(row영업그룹["TP_SALEPRICE"]);

            bp_CD_SL.CodeValue = string.Empty;      //2011-09-28, 창고 추가
            bp_CD_SL.CodeName = string.Empty;

            if (!수주Config.부가세포함단가사용())
            {
                m_lblTpUmTax.Visible = false;
                cbo부가세포함.Visible = false;
            }

            if (Properties.Settings.Default.부가세포함단가사용 != "")
                cbo부가세포함.SelectedValue = Properties.Settings.Default.부가세포함단가사용;
            else
                cbo부가세포함.SelectedValue = "N";
        }

        #endregion

        #region -> InitEvent

        private void InitEvent()
        {
            ctx수주형태.QueryBefore += new BpQueryHandler(Control_QueryBefore);
            bp_CD_SL.QueryBefore += new BpQueryHandler(Control_QueryBefore);
        }

        #endregion

        #endregion

        #region ♣ 메인버튼이벤트

        #region -> SaveData

        protected override bool SaveData()
        {
            if (!base.SaveData()) return false;

            if (!필수값체크) return false;

            if (Use유무환전환)
            {
                if (!Chk수주형태) return false;
            }

            DataTable dtL = new DataTable();
            DataTable dtLot = new DataTable();
            DataTable dtSer = new DataTable();
            string NO_GIR = string.Empty;
            string NO_SO = string.Empty;

            if (추가모드여부)
            {
                if (txt의뢰번호.Text == "" || txt의뢰번호.Text == "")
                {
                    NO_GIR = (string)GetSeq(LoginInfo.CompanyCode, "SA", "04", dp의뢰일자.Text.Substring(0, 6));
                    txt의뢰번호.Text = NO_GIR;
                }
                else
                    NO_GIR = txt의뢰번호.Text;

                _header.CurrentRow["NO_GIR"] = NO_GIR;

                dtL = _flex.DataTable.Clone();
                decimal i = 1;

                foreach (DataRow row in _flex.DataTable.Rows)
                {
                    if (row.RowState == DataRowState.Deleted) continue;
                    row["SEQ_GIR"] = i++;

                    //배송정보 TRACK 기능 => FG_TRACK : SO(수주등록), M(창고이동, 출고요청등록), R(출하반품의뢰등록)
                    row["FG_TRACK"] = "R";
                    
                    dtL.ImportRow(row);
                }
            }
            else
            {
                NO_GIR = txt의뢰번호.Text;
                dtL = _flex.GetChanges();
            }

            if (!Verify()) return false;

            DataTable dtH = _header.GetChanges();
            if (dtH == null && dtL == null) return true;

            bool bSuccess = false;

            if (Use유무환전환)
            {
                NO_SO = (string)GetSeq(LoginInfo.CompanyCode, "SA", "02", dp의뢰일자.Text.Substring(0, 6));

                #region LOT & SERIAL 저장 ----------------------------------------------------------

                if (Config.MA_ENV.LOT관리 || Config.MA_ENV.시리얼사용)
                {
                    DataRow[] drsLot = _flex.DataTable.Select("FG_SERNO = '002'");
                    DataRow[] drsSer = _flex.DataTable.Select("FG_SERNO = '003'");

                    if (Config.MA_ENV.LOT관리 && drsLot != null && drsLot.Length != 0)
                    {
                        _biz.dtLot_Schema(dtLot);   //스키마세팅
                        dtSetting(drsLot, dtLot);   //데이타세팅

                        pur.P_PU_LOT_SUB_R m_dlg = new pur.P_PU_LOT_SUB_R(dtLot);
                        m_dlg.SetPageId = PageID;

                        if (m_dlg.ShowDialog(this) == DialogResult.OK)
                            dtLot = m_dlg.dtL;
                        else
                            return false;

                        if (drsLot.Length != 0 && (dtLot == null || dtLot.Rows.Count == 0))
                        {
                            ShowMessage("LOT품목 수불이 발생하였으나 해당 LOT가 생성되지 않았습니다.");
                            return false;
                        }

                        foreach (DataRow dr in drsLot)
                        {
                            decimal QT_GOOD_MNG_SUM = 0m;

                            DataRow[] drs2 = dtLot.Select("NO_IOLINE = " + D.GetDecimal(dr["SEQ_GIR"]) + "");

                            foreach (DataRow dr2 in drs2)
                            {
                                if (D.GetString(dr["CD_ITEM"]) != D.GetString(dr["CD_ITEM"]))
                                {
                                    ShowMessage("LOT품목과 수불품목이 일치하지 않습니다.");
                                    return false;
                                }

                                QT_GOOD_MNG_SUM += D.GetDecimal(dr2["QT_IO"]);
                            }

                            if (QT_GOOD_MNG_SUM != D.GetDecimal(dr["QT_GIR_IM"]))
                            {
                                ShowMessage("LOT수량과 수불수량이 일치하지 않습니다.");
                                return false;
                            }
                        }
                    }

                    if (Config.MA_ENV.시리얼사용 && drsSer != null && drsSer.Length != 0)
                    {
                        _biz.dtLot_Schema(dtSer);   //스키마세팅
                        dtSetting(drsSer, dtSer);   //데이타세팅

                        pur.P_PU_SERL_SUB_R m_dlg = new pur.P_PU_SERL_SUB_R(dtSer);
                        m_dlg.SetPageId = PageID;

                        if (m_dlg.ShowDialog(this) == DialogResult.OK)
                            dtSer = m_dlg.dtL;
                        else
                            return false;
                    }
                }

                #endregion-----------------------------------------------------------------------------------
            }

            bSuccess = _biz.Save(dtH, dtL, dtLot, dtSer, NO_GIR, 추가모드여부, true, this, NO_SO);

            if (!bSuccess) return false;

            _header.AcceptChanges();
            _flex.AcceptChanges();
            Page_DataChanged(null, null);

            return true;
        }

        #endregion

        #region -> 조회버튼클릭

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSearch()) return;

                string 반품여부 = "Y";

                P_SA_GIR_SCH_SUB dlg = new P_SA_GIR_SCH_SUB(반품여부);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    //_헤더수정여부 = dlg.헤더수정유무;
                    object[] obj = new object[3];
                    obj[0] = Global.MainFrame.LoginInfo.CompanyCode;    //회사코드
                    obj[1] = dlg.returnParams[0].ToString();            //의뢰번호
                    obj[2] = dlg.returnParams[1].ToString();            //공장
                    //_단가적용형태 = "";

                    DataSet ds = _biz.Search(obj);

                    _header.SetDataTable(ds.Tables[0]);
                    _flex.Binding = ds.Tables[1];
                    _매출형태 = ds.Tables [1].Rows[0]["TP_IV"].ToString();
                    _단가적용형태 = ds.Tables[1].Rows[0]["TP_SALEPRICE"].ToString();  //2011-09-05, 최승애, 단가적용형태 추가함.

                    if (수주Config.부가세포함단가사용())
                    {
                        cbo부가세포함.SelectedValue = D.GetString(ds.Tables[1].Rows[0]["TP_UM_TAX"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 추가버튼클릭

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeAdd()) return;

                _헤더수정여부 = true;

                if (_flex.DataTable != null)
                {
                    _flex.DataTable.Rows.Clear();
                    _flex.AcceptChanges();
                }

                _header.ClearAndNewRow();
                cbo화폐단위.SelectedValue = "000";
                cbo수주화폐단위.SelectedValue = "000";
                화폐단위셋팅(cbo화폐단위, cur환율);
                화폐단위셋팅(cbo수주화폐단위, cur수주환율);
                VAT구분셋팅();
                적용시컨트롤Enabled = true;

                bp영업그룹.SetCodeValue(Global.MainFrame.LoginInfo.SalesGroupCode);
                bp영업그룹.SetCodeName(Global.MainFrame.LoginInfo.SalesGroupName);
            }
            catch (System.Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> BeforeDelete

        protected override bool BeforeDelete()
        {
            if (!base.BeforeDelete()) return false;
            if (ShowMessage(공통메세지.자료를삭제하시겠습니까) != DialogResult.Yes) return false;
            return true;
        }

        #endregion

        #region -> 삭제버튼클릭

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeDelete()) return;

                if (_biz.Delete(txt의뢰번호.Text))
                {
                    ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
                    OnToolBarAddButtonClicked(sender, e);       // 삭제 후 바로 추가모드로 해준다.
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 저장버튼클릭

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!_flex.HasNormalRow)
                {
                    OnToolBarDeleteButtonClicked(null, null);
                    return;
                }

                if (!BeforeSave()) return;

                MsgControl.ShowMsg("저장 중 입니다. 잠시만 기다려 주십시요.");

                if (MsgAndSave(PageActionMode.Save))
                {
                    MsgControl.CloseMsg();
                    ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
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

        #endregion

        #region -> 인쇄버튼클릭

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforePrint() || !_flex.HasNormalRow)
                    return;

                PrintRDF rdf = new PrintRDF("R_SA_GIRR_REG_0", false);

                List<string> List = new List<string>();
                List.Add(MA.Login.회사코드);
                List.Add(txt의뢰번호.Text);
                List.Add(D.GetString(cbo공장.SelectedValue));

                DataTable dt = _biz.Print(List.ToArray());

                Hashtable Data_H = new Hashtable();
                Data_H.Add("의뢰번호", txt의뢰번호.Text);
                Data_H.Add("의뢰일자", dp의뢰일자.Text);
                Data_H.Add("거래처", bp거래처.CodeName);
                Data_H.Add("공장", D.GetString(cbo공장.Text));
                Data_H.Add("거래구분", D.GetString(cbo거래구분.Text));
                Data_H.Add("영업그룹", bp영업그룹.CodeName);
                Data_H.Add("담당장", bp담당자.CodeName);
                Data_H.Add("반품형태", bp반품형태.CodeName);
                Data_H.Add("VAT구분", D.GetString(cboVAT구분.Text));
                Data_H.Add("VAT율", D.GetString(curVATRATE.Text));
                Data_H.Add("유무환구분", txt유무환구분.Text);
                Data_H.Add("화폐단위", D.GetString(cbo화폐단위.Text));
                Data_H.Add("환율", cur환율.Text);
                Data_H.Add("계산서처리", rdo일괄.Checked == true ? "일괄" : "건별");
                Data_H.Add("비고", txt비고.Text);
                Data_H.Add("단가유형", D.GetString(cbo단가유형.Text));
                Data_H.Add("납품처", bp납품처.CodeName);

                rdf.Data = Data_H;
                rdf.SetDataTable = dt;
                rdf.ShowDialog();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 종료버튼클릭

        public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
        {
            Properties.Settings.Default.부가세포함단가사용 = D.GetString(cbo부가세포함.SelectedValue);
            Properties.Settings.Default.Save();

            return base.OnToolBarExitButtonClicked(sender, e);
        }

        #endregion

        #endregion

        #region ♣ 화면내 버튼 이벤트

        #region -> 수주반품적용 버튼 클릭 이벤트(btn수주반품적용_Click)

        void btn수주반품적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Chk의뢰일자 || !Chk공장 || !Chk거래구분 || !Chk담당자 || !Chk반품형태 || !Chk과세구분) return;

                if(예외추가여부)
                {
                    ShowMessage("예외추가건이 존재합니다.");
                    return;
                }

                if(출하적용여부)
                {
                    ShowMessage("출하적용건이 존재합니다.");
                    return;
                }

                string 거래처코드 = bp거래처.CodeValue;
                string 거래처명 = bp거래처.CodeName;
                string 공장코드 = D.GetString(cbo공장.SelectedValue);
                string 공장명 = cbo공장.Text;
                string 거래구분코드 = D.GetString(cbo거래구분.SelectedValue);
                string 거래명 = cbo거래구분.Text;

                string 영업그룹 = bp영업그룹.CodeValue;
                string 과세구분 = D.GetString(cboVAT구분.SelectedValue);
                string 환종 = D.GetString(cbo화폐단위.SelectedValue);
                string 계산서처리 = rdo일괄.Checked ? "001" : "002";
                string 단가유형 = D.GetString(cbo단가유형.SelectedValue);
                string 납품처 = bp납품처.CodeValue;

                P_SA_SO_SUB dlg = new P_SA_SO_SUB(거래처코드, 거래처명,공장코드, 공장명, 거래구분코드, 거래명,"", "", null);
                
                dlg.Set정상수주 = false;
                dlg.Set영업그룹코드 = bp영업그룹.CodeValue;
                dlg.Set영업그룹명 = bp영업그룹.CodeName;
                dlg.Set과세구분 = D.GetString(cboVAT구분.SelectedValue);
                dlg.Set환종 = D.GetString(cbo화폐단위.SelectedValue);
                dlg.Set계산서처리 = rdo일괄.Checked ? "001" : "002";
                dlg.Set단가유형 = D.GetString(cbo단가유형.SelectedValue); 
                dlg.Set납품처코드 = bp납품처.CodeValue;
                dlg.Set납품처명 = bp납품처.CodeName;
                dlg.Set수불유형 = bp반품형태.CodeValue;

                DataTable dt수주적용 = dlg.Get수주적용Schema;
                for (int i = _flex.Rows.Fixed; i < _flex.Rows.Count; i++)
                {
                    DataRow newrow = dt수주적용.NewRow();
                    newrow["NO_SO"] = _flex[i, "NO_SO"];
                    newrow["SEQ_SO"] = _flex[i, "SEQ_SO"];
                    dt수주적용.Rows.Add(newrow);
                }

                dlg.Set수주적용내용 = dt수주적용;

                if (dlg.ShowDialog() != DialogResult.OK) return;
                
                DataTable dt수주반품적용 = dlg.수주데이터;
                if (dt수주반품적용 == null || dt수주반품적용.Rows.Count == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                int SeqGir = 1;
                if (dlg.적용구분 == "APPEND")
                {
                    _헤더수정여부 = true;
                    _header.JobMode = JobModeEnum.추가후수정;
                    txt의뢰번호.Text = "";
                    _header.CurrentRow["NO_GIR"] = "";

                    bp거래처.SetCode(D.GetString(dt수주반품적용.Rows[0]["CD_PARTNER"]), D.GetString(dt수주반품적용.Rows[0]["LN_PARTNER"]));
                    _header.CurrentRow["CD_PARTNER"] = bp거래처.CodeValue;
                    _header.CurrentRow["LN_PARTNER"] = bp거래처.CodeName;

                    if (Global.MainFrame.ServerKeyCommon == "SANGBO" || Global.MainFrame.ServerKeyCommon == "DZSQL")
                    {
                        if (_biz.Get담당자적용유무 == "Y")
                        {
                            DataTable dt = _biz.Get거래처영업담당자(D.GetString(bp거래처.CodeValue));
                            if (dt.Rows.Count == 1)
                            {
                                bp담당자.CodeValue = D.GetString(dt.Rows[0]["CD_EMP_SALE"]);
                                bp담당자.CodeName = D.GetString(dt.Rows[0]["NM_EMP_SALE"]);
                                _header.CurrentRow["NO_EMP"] = D.GetString(dt.Rows[0]["CD_EMP_SALE"]);
                                _header.CurrentRow["NM_KOR"] = D.GetString(dt.Rows[0]["NM_EMP_SALE"]);
                            }
                        }
                    }

                    bp영업그룹.SetCode(D.GetString(dt수주반품적용.Rows[0]["CD_SALEGRP"]), D.GetString(dt수주반품적용.Rows[0]["NM_SALEGRP"]));
                    _header.CurrentRow["CD_SALEGRP"] = bp영업그룹.CodeValue;
                    _header.CurrentRow["NM_SALEGRP"] = bp영업그룹.CodeName;

                    cbo화폐단위.SelectedValue = (D.GetString(dt수주반품적용.Rows[0]["CD_EXCH"]));
                    _header.CurrentRow["CD_EXCH"] = cbo화폐단위.SelectedValue;

                    cbo단가유형.SelectedValue = (D.GetString(dt수주반품적용.Rows[0]["TP_PRICE_IMSI"]));
                    _header.CurrentRow["FG_UM"] = cbo단가유형.SelectedValue;

                    cur환율.Text = (D.GetString(dt수주반품적용.Rows[0]["RT_EXCH"]));
                    _header.CurrentRow["RT_EXCH"] = cur환율.Text;
                }
                else
                {
                    SeqGir = D.GetInt(_flex.GetMaxValue("SEQ_GIR"));
                    SeqGir++;
                }
                
                foreach (DataRow row수주 in dt수주반품적용.Rows)
                {
                    DataRow newrow = _flex.DataTable.NewRow();

                    foreach (DataColumn col in dt수주반품적용.Columns)
                    {
                        if (!_flex.DataTable.Columns.Contains(col.ColumnName)) continue;
                        newrow[col.ColumnName] = row수주[col.ColumnName];
                    }

                    newrow["SEQ_GIR"] = SeqGir;
                    newrow["FG_TRACK"] = "R";
                    newrow["AMVAT_GIR"] = D.GetDecimal(newrow["AM_GIRAMT"]) + D.GetDecimal(newrow["AM_VAT"]);

                    if (수주Config.부가세포함단가사용())
                        cbo부가세포함.SelectedValue = D.GetString(newrow["TP_UM_TAX"]);

                    _flex.DataTable.Rows.Add(newrow);

                    SeqGir++;
                }

                _flex.SumRefresh();
                //_flex.Row = _flex.Rows.Count - 1;
                _flex.Row = _flex.BottomRow;
                _flex.IsDataChanged = true;
                적용시컨트롤Enabled = false;
                Page_DataChanged(null, null);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 출하적용 버튼클릭 이벤트(btn출하적용_Click)

        private void btn출하적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Chk의뢰일자 || !Chk거래처 || !Chk공장 || !Chk거래구분 || !Chk영업그룹 || !Chk담당자 || !Chk과세구분 || !Chk환종 || !Chk단가유형 || !Chk납품처 || !Chk반품형태) return;

                if (Use유무환전환)
                {
                    if (!Chk수주환종) return;
                }

                if (예외추가여부)
                {
                    ShowMessage("예외추가건이 존재합니다.");
                    return;
                }

                if (수주반품적용여부)
                {
                    ShowMessage("수주반품적용건이 존재합니다.");
                    return;
                }

                P_SA_GIRR_REG_SUB dlg = new P_SA_GIRR_REG_SUB(cbo공장.SelectedValue.ToString(), cbo공장.Text, bp거래처.CodeValue, bp거래처.CodeName, cboVAT구분.SelectedValue.ToString(), cboVAT구분.Text);
                
                if (Use유무환전환) dlg.Set무환출고조회 = false; //무환출고된것만 조회하고자 할 때에는 true로 세팅해주면 됨.
                if (서버키 == "TRIGEM")
                {
                    if (txt유무환구분.Text == "N")
                        dlg.Set무환출고조회 = true;  //반품형태가 무환인 건은 무환인 출고만 조회도로록(PIMS:D20130628023)
                }

                if (dlg.ShowDialog() != DialogResult.OK)
                    return;

                _헤더수정여부 = true;
                _header.JobMode = JobModeEnum.추가후수정;
                txt의뢰번호.Text = "";
                _header.CurrentRow["NO_GIR"] = "";

                DataTable dt반품적용 = dlg.출하테이블;
                int SeqGir = 1;

                _flex.Redraw = false;

                foreach (DataRow row수주 in dt반품적용.Rows)
                {
                    DataRow newrow = _flex.DataTable.NewRow();

                    foreach (DataColumn col in dt반품적용.Columns)
                    {
                        if (!_flex.DataTable.Columns.Contains(col.ColumnName)) continue;
                        newrow[col.ColumnName] = row수주[col.ColumnName];
                    }

                    newrow["SEQ_GIR"] = SeqGir;
                    newrow["FG_TRACK"] = "R";

                    if (D.GetString(row수주["CD_EXCH"]) == "000")
                    {
                        if (D.GetDecimal(row수주["QT_IO_ORI"]) == D.GetDecimal(row수주["QT_GIR_IM_ORI"]) && D.GetDecimal(row수주["QT_RETURN"]) == decimal.Zero)
                        {
                            newrow["AM_GIR"] = row수주["AM_GIR_ORIGINAL"];
                            newrow["AM_GIRAMT"] = row수주["AM_GIRAMT_ORIGINAL"];
                            newrow["AM_VAT"] = row수주["AM_VAT_ORIGINAL"];
                        }
                        else
                        {
                            newrow["AM_GIRAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(newrow["AM_GIR"]) * cur환율.DecimalValue);            //원화금액
                            newrow["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(newrow["AM_GIRAMT"]) * (curVATRATE.DecimalValue / 100)); //부가세
                        }
                    }
                    else
                    {
                        newrow["AM_GIRAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(newrow["AM_GIR"]) * cur환율.DecimalValue);            //원화금액
                        newrow["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(newrow["AM_GIRAMT"]) * (curVATRATE.DecimalValue / 100)); //부가세
                    }

                    newrow["AMVAT_GIR"] = D.GetDecimal(newrow["AM_GIRAMT"]) + D.GetDecimal(newrow["AM_VAT"]);

                    if (Use유무환전환)
                    {
                        newrow["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(newrow["UM"]));
                        newrow["AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(newrow["AM_GIR"]));
                        newrow["AM_SOAMT"] = Unit.원화금액(DataDictionaryTypes.SA, _flex.CDecimal(newrow["AM_SO"]) * cur수주환율.DecimalValue);
                        newrow["AM_SOVAT"] = Unit.원화금액(DataDictionaryTypes.SA, _flex.CDecimal(newrow["AM_SOAMT"]) * (curVATRATE.DecimalValue / 100));
                    }

                    if (BASIC.GetMAEXC("배차사용유무") == "Y")
                        newrow["YN_PICKING"] = _배송여부;

                    if (수주Config.부가세포함단가사용())
                        cbo부가세포함.SelectedValue = D.GetString(newrow["TP_UM_TAX"]);
                    else
                        newrow["UMVAT_GIR"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(newrow["AMVAT_GIR"]) / D.GetDecimal(newrow["QT_GIR"]));

                    _flex.DataTable.Rows.Add(newrow);

                    SeqGir++;
                }

                _flex.Redraw = true;
                _flex.SumRefresh();
                _flex.Row = _flex.BottomRow;

                _flex.IsDataChanged = true;
                적용시컨트롤Enabled = false;
                Page_DataChanged(null, null);
            }
            catch (Exception ex)
            {
                _flex.Redraw = true;
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 추가 버튼클릭 이벤트(btn추가_Click)

        private void btn추가_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbo공장.SelectedValue == null || cbo공장.SelectedValue.ToString() == "")
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, lbl공장.Text);
                    cbo공장.Focus();
                    return;
                }

                // 거래처
                if (bp거래처.CodeValue == "")
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, lbl거래처.Text);
                    bp거래처.Focus();
                    return;
                }

                //과세구분
                if (cboVAT구분.SelectedValue == null || cboVAT구분.SelectedValue.ToString() == "")
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, lblVAT구분.Text);
                    cboVAT구분.Focus();
                    return;
                }

                // 환종
                if (cbo화폐단위.SelectedValue == null || cbo화폐단위.SelectedValue.ToString() == "")
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, lbl화폐단위.Text);
                    cbo화폐단위.Focus();
                    return;
                }

                // 매출형태
                if (bp반품형태.CodeValue == "")
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, lbl반품형태.Text);
                    bp반품형태.Focus();
                    return;
                }

                if (!Chk의뢰일자 || !Chk거래처 || !Chk공장 || !Chk거래구분 || !Chk영업그룹 || !Chk담당자 || !Chk과세구분 || !Chk환종 || !Chk단가유형 || !Chk납품처) return;

                if (수주반품적용여부)
                {
                    ShowMessage("수주반품적용건이 존재합니다.");
                    return;
                }

                if (출하적용여부)
                {
                    ShowMessage("출하적용건이 존재합니다.");
                    return;
                }

                _flex.Rows.Add();
                //_flex.Row = _flex.Rows.Count - 1;
                _flex.Row = _flex.BottomRow;
                if (!추가모드여부)
                {
                    _flex[_flex.Row, "SEQ_GIR"] = 그리드항번최대값 + 1;
                    btn삭제.Enabled = true;
                }

                _flex[_flex.Row, "GI_PARTNER"] = bp납품처.CodeValue;
                _flex[_flex.Row, "LN_PARTNER"] = bp납품처.CodeName;
                _flex[_flex.Row, "YN_INSPECT"] = "N";
                _flex[_flex.Row, "TP_IV"] = _매출형태;
                _flex[_flex.Row, "NO_LC"] = "";
                _flex[_flex.Row, "NO_EMP"] = bp담당자.CodeValue;
                _flex[_flex.Row, "FG_TRACK"] = "R";

                if (_flex.Row == _flex.Rows.Fixed)
                {
                    _flex["DT_DUEDATE"] = dp의뢰일자.Text;
                }
                else if (_flex.Row > _flex.Rows.Fixed)   //if (_flex.Rows.Count > 3) --> 방식이 고정되어 있어서 변경
                {
                    _flex[_flex.Row, "DT_DUEDATE"] = _flex[_flex.Row - 1, "DT_DUEDATE"];
                    _flex.Row = _flex.BottomRow;
                }

                if (BASIC.GetMAEXC("배차사용유무") == "Y")
                    _flex[_flex.Row, "YN_PICKING"] = _배송여부;

                if (수주Config.부가세포함단가사용())
                    _flex[_flex.Row, "TP_UM_TAX"] = D.GetString(cbo부가세포함.SelectedValue);

                _flex.AddFinished();
                _flex.Col = _flex.Cols.Fixed;
                _flex.Focus();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 삭제 버튼클릭 이벤트(m_btnDel_Click)

        private void m_btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                #region --> 기존소스
                /*
                if (!추가모드여부)
                {
                    if (_flex.CDecimal(_flex[_flex.Row, "QT_GI"]) > 0)
                    {
                        ShowMessage("이미 반품출하되어 삭제가 불가능합니다.");
                        return;
                    }

                    if (_flex.CDecimal(_flex[_flex.Row, "QT_GI"]) > 0)
                    {
                        ShowMessage("이미 반품출하되어 삭제가 불가능합니다.");
                        return;
                    }

                }

                _flex.RemoveItem(_flex.Row);                

                if (!_flex.HasNormalRow)
                {
                    if (추가모드여부)
                    {
                        bp거래처.Enabled = true;
                        cbo공장.Enabled = true;
                        if (_biz.Get영업그룹적용유무 != "Y")  //거래처부가정보의 거래처와영업그룹매핑을 할 경우
                            bp영업그룹.Enabled = true;
                        cboVAT구분.Enabled = true;
                        cbo화폐단위.Enabled = true;
                        if (MA.기준환율.Option == MA.기준환율옵션.적용_수정불가)
                            cur환율.Enabled = false;
                        else
                        {
                            if (D.GetString(cbo화폐단위.SelectedValue) != "000")    //KRW
                                cur환율.Enabled = true;
                            else
                                cur환율.Enabled = false;
                        }
                        bp반품형태.Enabled = true;
                        rdo일괄.Enabled = true;
                        rdo건별.Enabled = true;
                        _flex.AcceptChanges();
                    }
                }
                */
                #endregion


                _flex.Focus();
                _flex.Row = _flex.Rows.Count - 1;


                DataRow[] dr = _flex.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                if (dr == null || dr.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                foreach (DataRow drRow in dr)
                {
                    if (!추가모드여부)
                    {
                        if (D.GetDecimal(drRow["QT_GI"]) > 0)
                        {
                            ShowMessage("이미 반품출하되어 삭제가 불가능합니다.");
                            return;
                        }
                    }

                    drRow.Delete();
                   
                }


                if (!_flex.HasNormalRow)
                {
                    if (추가모드여부)
                    {
                        bp거래처.Enabled = true;
                        cbo공장.Enabled = true;
                        if (_biz.Get영업그룹적용유무 != "Y")  //거래처부가정보의 거래처와영업그룹매핑을 할 경우
                            bp영업그룹.Enabled = true;
                        //if (_biz.Get담당자적용유무 != "Y")
                            bp담당자.Enabled = true;
                        cboVAT구분.Enabled = true;
                        cbo화폐단위.Enabled = true;
                        if (MA.기준환율.Option == MA.기준환율옵션.적용_수정불가)
                            cur환율.Enabled = false;
                        else
                        {
                            if (D.GetString(cbo화폐단위.SelectedValue) != "000")    //KRW
                                cur환율.Enabled = true;
                            else
                                cur환율.Enabled = false;
                        }
                        bp반품형태.Enabled = true;
                        rdo일괄.Enabled = true;
                        rdo건별.Enabled = true;
                        cbo부가세포함.Enabled = true;
                    }
                }
                
               

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 배송지 주소 버튼 클릭 이벤트(btn배송지주소_Click)

        private void btn배송지주소_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flex.HasNormalRow)
                {
                    ShowMessage("라인을 먼저 입력하세요!");
                    return;
                }

                object[] obj = new object[] { LoginInfo.CompanyCode, bp거래처.CodeValue };

                //배송정보 도움창에 헤더의 거래처에 있는 필요정보를 넘겨준다.
                DataTable dt거래처정보 = _biz.GetPartnerInfoSearch(obj);

                string[] str = new string[7];
                if (dt거래처정보.Rows.Count == 1)
                {
                    str[0] = dt거래처정보.Rows[0]["CD_PARTNER"].ToString();
                    str[1] = dt거래처정보.Rows[0]["NO_POST2"].ToString();
                    str[2] = dt거래처정보.Rows[0]["DC_ADS2_H"].ToString();
                    str[3] = dt거래처정보.Rows[0]["DC_ADS2_D"].ToString();
                    str[4] = dt거래처정보.Rows[0]["NO_TEL2"].ToString();
                    str[5] = dt거래처정보.Rows[0]["CD_EMP_PARTNER"].ToString();
                    str[6] = dt거래처정보.Rows[0]["NO_HPEMP_PARTNER"].ToString();
                }

                DataTable dtGrid = _flex.DataTable.Copy();
                dtGrid.AcceptChanges(); //Added 상태로 해줄 필요가 있는지 체크해야함. 필요가 없으면 아래의 주석은 지우면 됨.

                //DataTable dt = _flex.DataTable.Clone();

                //foreach (DataRow dr in _flex.DataTable.Rows)
                //{
                //    if (dr.RowState == DataRowState.Deleted) continue;

                //    dt.Rows.Add(dr.ItemArray);
                //}

                P_SA_SO_DLV_SUB dlg = new sale.P_SA_SO_DLV_SUB(dtGrid, str);

                if (서버키 == "SIMMONS")
                    dlg.Str사용자정의코드1 = D.GetString(dt거래처정보.Rows[0]["CD_AREA"]);

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    foreach (DataRow dx in dlg.ReturnTable.Rows)
                    {
                        for (int i = 0; i < _flex.DataTable.Rows.Count; i++)
                        {
                            if (dx["SEQ_GIR"].ToString() == _flex[i + 2, "SEQ_GIR"].ToString())
                            {
                                _flex[i + 2, "NM_CUST_DLV"] = dx["NM_CUST_DLV"];
                                _flex[i + 2, "NO_TEL_D1"] = dx["NO_TEL_D1"];
                                _flex[i + 2, "NO_TEL_D2"] = dx["NO_TEL_D2"];
                                _flex[i + 2, "CD_ZIP"] = dx["CD_ZIP"].ToString().Replace("-", "");
                                _flex[i + 2, "ADDR1"] = dx["ADDR1"];
                                _flex[i + 2, "ADDR2"] = dx["ADDR2"];
                                _flex[i + 2, "TP_DLV"] = dx["TP_DLV"];
                                _flex[i + 2, "DC_REQ"] = dx["DC_REQ"];
                                _flex[i + 2, "DLV_TXT_USERDEF1"] = dx["DLV_TXT_USERDEF1"];
                                _flex[i + 2, "DLV_CD_USERDEF1"] = dx["DLV_CD_USERDEF1"];
                            }
                            else
                                continue;
                        }
                    }

                    _flex.IsDataChanged = true;
                    Page_DataChanged(null, null);

                    //이렇게 하지 말고 위에처럼 이중 포문 돌려서 그리드의 로우 상태값이 자동으로 제어 되도록 할것~ 아래처럼 하면 항상 _flex.Binding 되기 때문에 
                    //항상 DataRowState 값이 Added 상태가 된다.
                    //_flex.Binding = dlg.ReturnTable;
                    //_flex.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 상품적용 버튼클릭 이벤트(btn상품적용_Click)

        private void btn상품적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbo공장.SelectedValue == null || cbo공장.SelectedValue.ToString() == "")
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, lbl공장.Text);
                    cbo공장.Focus();
                    return;
                }

                if (bp거래처.CodeValue == "")
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, lbl거래처.Text);
                    bp거래처.Focus();
                    return;
                }

                if (cboVAT구분.SelectedValue == null || cboVAT구분.SelectedValue.ToString() == "")
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, lblVAT구분.Text);
                    cboVAT구분.Focus();
                    return;
                }

                if (cbo화폐단위.SelectedValue == null || cbo화폐단위.SelectedValue.ToString() == "")
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, lbl화폐단위.Text);
                    cbo화폐단위.Focus();
                    return;
                }

                if (bp반품형태.CodeValue == "")
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, lbl반품형태.Text);
                    bp반품형태.Focus();
                    return;
                }

                if (!Chk의뢰일자 || !Chk거래처 || !Chk공장 || !Chk거래구분 || !Chk영업그룹 || !Chk담당자 || !Chk과세구분 || !Chk환종 || !Chk단가유형 || !Chk납품처) return;

                if (수주반품적용여부)
                {
                    ShowMessage("수주반품적용건이 존재합니다.");
                    return;
                }

                if (출하적용여부)
                {
                    ShowMessage("출하적용건이 존재합니다.");
                    return;
                }

                object[] obj = new object[10];
                obj[0] = dp의뢰일자.Text;           //의뢰일자를 기준으로 상품의 품목들을 끌고 온다.
                obj[1] = cur환율.DecimalValue;      //환율을 가져와서 단가를 보여준다.
                obj[2] = bp거래처.CodeValue;
                obj[3] = D.GetString(cbo단가유형.SelectedValue); //단가유형
                obj[4] = D.GetString(cbo화폐단위.SelectedValue);  //환종
                obj[5] = _단가적용형태; //영업그룹을 선택시 단가적용형태를 가져오게 함
                obj[6] = so_Price;
                obj[7] = curVATRATE.DecimalValue;
                obj[8] = cbo공장.SelectedValue.ToString();
                obj[9] = "N";

                P_SA_SO_SPITEM_SUB dlg = new P_SA_SO_SPITEM_SUB(obj);

                if (dlg.ShowDialog() != DialogResult.OK) return;

                if (dlg.ReturnDataTable == null) return;
                DataTable dt_Dlg = dlg.ReturnDataTable.Copy();
                DataTable dt = _flex.DataTable.Clone();   //라인도 받고 싶은컬럼만 받게 FOREACH를 한다.

                foreach (DataRow dr in dt_Dlg.Rows)
                {
                    DataRow row = dt.NewRow();

                    //필요한 부분을 넣어야 함.
                    #region 1. 그리드의 행추가 로직

                    if (!추가모드여부)
                    {
                        row["SEQ_GIR"] = 그리드항번최대값 + 1;
                        btn삭제.Enabled = true;
                    }

                    row["GI_PARTNER"] = bp납품처.CodeValue;
                    row["LN_PARTNER"] = bp납품처.CodeName;
                    row["YN_INSPECT"] = "N";
                    row["TP_IV"] = _매출형태;
                    row["NO_LC"] = "";
                    row["NO_EMP"] = bp담당자.CodeValue;
                    row["FG_TRACK"] = "R";

                    if (_flex.Row == _flex.Rows.Fixed)
                    {
                        _flex["DT_DUEDATE"] = dp의뢰일자.Text;
                    }
                    else if (_flex.Row > _flex.Rows.Fixed)   //if (_flex.Rows.Count > 3) --> 방식이 고정되어 있어서 변경
                    {
                        row["DT_DUEDATE"] = _flex[_flex.Row - 1, "DT_DUEDATE"];
                        _flex.Row = _flex.BottomRow;
                    }

                    #endregion

                    #region 2. 품목선택시 로직

                    row["CD_ITEM"] = dr["CD_ITEM"];
                    row["NM_ITEM"] = dr["NM_ITEM"];
                    row["STND_ITEM"] = dr["STND_ITEM"];
                    row["UNIT"] = dr["UNIT_SO"];
                    row["CD_SL"] = dr["CD_SL"];
                    row["NM_SL"] = dr["NM_SL"];
                    row["TP_ITEM"] = row["TP_ITEM"];
                    row["UNIT_SO_FACT"] = _flex.CDecimal(dr["UNIT_SO_FACT"]) == 0 ? 1 : dr["UNIT_SO_FACT"];
                    row["YN_INSPECT"] = "N";
                    row["TP_IV"] = _매출형태;
                    row["NO_LC"] = "";
                    row["NO_EMP"] = bp담당자.CodeValue;
                    row["GI_PARTNER"] = bp납품처.CodeValue;
                    row["LN_PARTNER"] = bp납품처.CodeName;

                    #region 2011-10-20, 주석처리
                    /* 2011-10-20 주석처리함.
                    row["UM"] = _biz.UmSearch(
                                                        dr["CD_ITEM"].ToString(),
                                                        _header.CurrentRow["CD_PARTNER"].ToString(),
                                                        cbo단가유형.SelectedValue.ToString(),
                                                        cbo화폐단위.SelectedValue.ToString(),
                                                        "003", dp의뢰일자.Text
                                                );
                    */
                    #endregion

                    //2011-10-20, 최승애
                    row["UM"] = _biz.UmSearch(
                                                       dr["CD_ITEM"].ToString(),
                                                       _header.CurrentRow["CD_PARTNER"].ToString(),
                                                       cbo단가유형.SelectedValue.ToString(),
                                                       cbo화폐단위.SelectedValue.ToString(),
                                                       _단가적용형태, dp의뢰일자.Text
                                               );


                    row["UM"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row["UM"]));

                    if (BASIC.GetMAEXC("배차사용유무") == "Y")
                        row["YN_PICKING"] = _배송여부;

                    #endregion

                    #region -> 수주등록의 상품적용 부분(개발시 참고용)

                    //row["CD_SHOP"] = dr["CD_SHOP"].ToString();
                    //row["CD_SPITEM"] = dr["CD_SPITEM"].ToString();
                    //row["CD_OPT"] = dr["CD_OPT"].ToString();
                    //row["NO_PROJECT"] = bp_프로젝트.CodeValue;      //프로젝트번호
                    //row["SEQ_PROJECT"] = 0;                         //프로젝트라인항번 : 프로젝트를 적용받은 데이터가 아니기때문에 항번이 없다.
                    //row["CD_PLANT"] = dr["CD_PLANT"].ToString();
                    //row["CD_ITEM"] = dr["CD_ITEM"].ToString();
                    //row["NM_ITEM"] = dr["NM_ITEM"].ToString();
                    //row["STND_ITEM"] = dr["STND_ITEM"].ToString();
                    //row["UNIT_SO"] = dr["UNIT_SO"].ToString();
                    //row["TP_ITEM"] = dr["TP_ITEM"].ToString();
                    //row["GI_PARTNER"] = bp납품처.CodeValue;
                    //row["LN_PARTNER"] = bp납품처.CodeName;
                    //row["CD_SL"] = dr["CD_SL"].ToString();
                    //row["NM_SL"] = dr["NM_SL"].ToString();
                    //row["CD_CC"] = cd_CC;
                    //row["NM_CC"] = nm_CC;
                    //row["TP_VAT"] = dr["FG_VAT"].ToString();
                    //row["RT_VAT"] = D.GetDecimal(dr["RT_VAT"]);
                    //row["QT_SO"] = dr["QT_SO"].ToString();
                    //row["AM_SO"] = dr["AM_SO"].ToString();
                    //row["AM_WONAMT"] = dr["AM_WONAMT"].ToString();
                    //row["AM_VAT"] = dr["AM_VAT"].ToString();
                    //row["AMVAT_SO"] = dr["AMVAT_SO"].ToString();
                    //row["UMVAT_SO"] = dr["UMVAT_SO"].ToString();
                    //row["UNIT_SO_FACT"] = D.GetDecimal(dr["UNIT_SO_FACT"]) == 0 ? 1 : dr["UNIT_SO_FACT"];
                    //row["QT_IM"] = D.GetDecimal(dr["QT_IM"].ToString()) * D.GetDecimal(row["UNIT_SO_FACT"]);
                    //row["UNIT_IM"] = dr["UNIT_IM"].ToString();

                    #endregion

                    dt.Rows.Add(row);
                }

                _flex.BindingAdd(dt, "", false);
                _flex.IsDataChanged = true;
                적용시컨트롤Enabled = false;
                Page_DataChanged(null, null);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 단가적용 버튼클릭 이벤트(btn단가적용_Click)

        private void btn단가적용_Click(object sender, EventArgs e)
        {
            if (!_flex.HasNormalRow)
            {
                ShowMessage(공통메세지.선택된자료가없습니다);
                return;
            }

            DataSet ds반품등록 = null;
            DataTable dt반품등록된내역 = null;
            DataTable dt반품등록않된내역 = null;

            if (txt의뢰번호.Text != "" && txt의뢰번호.Enabled == false)
            {
                ds반품등록 = _biz.반품등록된건조회(txt의뢰번호.Text);
                dt반품등록된내역 = ds반품등록.Tables[0];
                dt반품등록않된내역 = ds반품등록.Tables[1];

                StringBuilder 검증리스트 = new StringBuilder();

                #region -> dt반품등록된내역 추가

                //string msg = "품목코드\t 품목명\t";
                string msg = DD("품목코드") + "\t " + DD("품목명") + "\t";
                검증리스트.AppendLine(msg);
                msg = "-".PadRight(60, '-');
                검증리스트.AppendLine(msg);

                검증리스트.AppendLine("반품등록된 내역.......................");

                foreach (DataRow dr in dt반품등록된내역.Rows)
                {
                    msg = dr["CD_ITEM"].ToString() + "\t" + dr["NM_ITEM"].ToString();
                    검증리스트.AppendLine(msg);
                }

                검증리스트.AppendLine(Environment.NewLine);

                #endregion

                #region -> 반품등록않된내역 추가

                검증리스트.AppendLine("반품등록되지 않은내역..........");

                foreach (DataRow dr in dt반품등록않된내역.Rows)
                {
                    msg = dr["CD_ITEM"].ToString() + "\t" + dr["NM_ITEM"].ToString();
                    검증리스트.AppendLine(msg);
                }

                #endregion

                if (dt반품등록않된내역.Rows.Count == 0) //의뢰 않된 내역이 없으면 작업을 진행할 수 없다.
                {
                    ShowMessage("모든 품목이 반품등록된 건입니다. 작업을 진행할 수 없습니다.");
                    return;
                }

                if (dt반품등록된내역.Rows.Count > 0)  //의뢰된 내역이 있으면
                {
                    DialogResult dlg = ShowDetailMessage("반품등록된 내역이 있습니다. 반품등록되지 않은 건에 대해서 계속 진행하시겠습니까?", "", 검증리스트.ToString(), "QY2");

                    if (dlg == DialogResult.No) return;
                }

                dt반품등록된내역.PrimaryKey = new DataColumn[] { dt반품등록된내역.Columns["SEQ_GIR"] };

                for (int i = 0; i < _flex.DataTable.Rows.Count; i++)
                {

                    DataRow dr = _flex.DataTable.Rows[i];

                    if (dr.RowState == DataRowState.Deleted) continue;

                    DataRow drFind = dt반품등록된내역.Rows.Find(dr["SEQ_GIR"]);

                    if (drFind != null) continue;

                    단가변경(ref dr);
                }
                _flex.SumRefresh();
            }
            else
            {
                단가변경();
                _flex.SumRefresh();
            }
        }

        private void 단가변경()
        {
            for (int i = 0; i < _flex.DataTable.Rows.Count; i++)
            {
                DataRow dr = _flex.DataTable.Rows[i];

                if (dr.RowState == DataRowState.Deleted) continue;

                단가변경(ref dr);
            }
        }

        private void 단가변경(ref DataRow dr)
        {
            decimal dUM_BASE = 0m;
            bool bChangeUM = false;

            #region 2011-10-20, 주석처리 단가변경
            /*
            dUM_BASE = _biz.UmSearch(dr["CD_ITEM"].ToString(),
                                    _header.CurrentRow["CD_PARTNER"].ToString(),
                                    cbo단가유형.SelectedValue.ToString(),
                                    cbo화폐단위.SelectedValue.ToString(),
                                    "003", dp의뢰일자.Text);
            */
            #endregion

            dUM_BASE = _biz.UmSearch(dr["CD_ITEM"].ToString(),
                                    _header.CurrentRow["CD_PARTNER"].ToString(),
                                    cbo단가유형.SelectedValue.ToString(),
                                    cbo화폐단위.SelectedValue.ToString(),
                                    _단가적용형태, dp의뢰일자.Text);
            
            if (dUM_BASE != 0m && dUM_BASE != D.GetDecimal(dr["UM"]))
            {
                if (!Use부가세포함 || (Global.MainFrame.ServerKeyCommon != "DZSQL" && !is중국고객부가세포함단가사용여부))
                    dr["UM"] = Unit.외화단가(DataDictionaryTypes.SA, dUM_BASE);
                else
                    dr["UMVAT_GIR"] = Unit.원화단가(DataDictionaryTypes.SA, dUM_BASE);

                bChangeUM = true;
            }

            if (bChangeUM)
            {
                //dr["AM_GIR"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["UM"]) * D.GetDecimal(dr["QT_GIR"]));
                //dr["AM_GIRAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["AM_GIR"]) * cur환율.DecimalValue);
                //dr["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["AM_GIRAMT"]) * (D.GetDecimal(_header.CurrentRow["RT_VAT"]) / 100));
                //dr["AMVAT_GIR"] = D.GetDecimal(dr["AM_GIRAMT"]) + D.GetDecimal(dr["AM_VAT"]);

                decimal 의뢰수량 = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(dr["QT_GIR"]));
                decimal 단가 = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(dr["UM"]));

                decimal 외화금액 = decimal.Zero;
                decimal 원화금액 = decimal.Zero;
                decimal 부가세 = decimal.Zero;
                decimal 합계 = decimal.Zero;

                if (!Use부가세포함 || (Global.MainFrame.ServerKeyCommon != "DZSQL" && !is중국고객부가세포함단가사용여부))
                {
                    외화금액 = Unit.외화금액(DataDictionaryTypes.SA, 단가 * 의뢰수량);
                    원화금액 = cur환율.DecimalValue == 0m ? 0m : Unit.원화금액(DataDictionaryTypes.SA, 외화금액 * cur환율.DecimalValue);
                    부가세 = Unit.원화금액(DataDictionaryTypes.SA, 원화금액 * (curVATRATE.DecimalValue / 100));
                    합계 = 원화금액 + 부가세;
                    dr["UMVAT_GIR"] = 의뢰수량 == 0m ? 0m : Unit.원화단가(DataDictionaryTypes.SA, 합계 / 의뢰수량);
                }
                else
                {
                    합계 = Unit.원화금액(DataDictionaryTypes.SA, 의뢰수량 * D.GetDecimal(dr["UMVAT_GIR"]));
                    if (Global.MainFrame.LoginInfo.CompanyLanguage == Language.KR)
                        원화금액 = Decimal.Round(합계 * (100 / (100 + curVATRATE.DecimalValue)), MidpointRounding.AwayFromZero);
                    else
                        원화금액 = D.GetDecimal(UDecimal.GetConvertFormatData(DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.INSERT, 합계 * (100 / (100 + D.GetDecimal(dr["RT_VAT"])))));
                    부가세 = 합계 - 원화금액;
                    외화금액 = cur환율.DecimalValue == 0m ? 0m : Unit.외화금액(DataDictionaryTypes.SA, 원화금액 / cur환율.DecimalValue);
                    _flex["UM"] = 의뢰수량 == 0m ? 0m : Unit.외화단가(DataDictionaryTypes.SA, 외화금액 / 의뢰수량);
                }

                dr["AM_GIR"] = 외화금액;
                dr["AM_GIRAMT"] = 원화금액;
                dr["AM_VAT"] = 부가세;
                dr["AMVAT_GIR"] = 합계;
            }
        }
        #endregion

        #region -> 선택버튼 추가 2011-09-28
        private void btn_Apply_SL_Click(object sender, EventArgs e)
        {
            try
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
                        }
                    }
                }
                finally
                {
                    _flex.Redraw = true;
                }

                ShowMessage(공통메세지._작업을완료하였습니다, btn_Apply_SL.Text);

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #endregion

        #region ♣ 엑셀업로드관련

        #region -> 엑셀업로드 버튼클릭 이벤트(btn엑셀업로드_Click)

        void btn엑셀업로드_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Chk의뢰일자 || !Chk공장 || !Chk거래처 || !Chk영업그룹 || !Chk과세구분 || !Chk환종 || !Chk매출형태) return;

                if (수주반품적용여부)
                {
                    ShowMessage("수주반품적용건이 존재합니다.");
                    return;
                }

                if (출하적용여부)
                {
                    ShowMessage("출하적용건이 존재합니다.");
                    return;
                }

                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "엑셀 파일 (*.xls)|*.xls"; 
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;

                Application.DoEvents();

                string 파일이름 = dlg.FileName;

                Excel excel = new Excel();

                DataTable dt엑셀 = excel.StartLoadExcel(파일이름);
                if (dt엑셀 == null || dt엑셀.Rows.Count == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                System.Diagnostics.Debug.WriteLine("엑셀업로드");
                if (dt엑셀.Columns.Contains("CD_ITEM"))
                    품목업로드(dt엑셀);
                else
                    상품업로드(dt엑셀);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 품목업로드

        void 품목업로드(DataTable dt엑셀)
        {
            System.Diagnostics.Debug.WriteLine("품목업로드");

            DataTable dt품목그룹 = dt엑셀.DefaultView.ToTable(true, "CD_ITEM");
            string 멀티품목 = "";
            foreach (DataRow row품목 in dt품목그룹.Rows)
            {
                멀티품목 += D.GetString(row품목["CD_ITEM"]) + "|";
            }

            bool is외화금액 = dt엑셀.Columns.Contains("AM_GIR");
            bool is단가 = dt엑셀.Columns.Contains("UM");            //2011-09-28, 최승애 추가 PIMS :D20110926036 

            string 공장 = D.GetString(cbo공장.SelectedValue);
            string 거래처 = bp거래처.CodeValue;
            string 영업그룹 = bp영업그룹.CodeValue;
            string 의뢰일자 = dp의뢰일자.Text;
            string 환종 = D.GetString(cbo화폐단위.SelectedValue);
            string 단가유형 = D.GetString(cbo단가유형.SelectedValue);

            DataTable dt품목조회 = _biz.품목관련정보(멀티품목, 공장, 거래처, 영업그룹, 의뢰일자, 환종, 단가유형);
            dt품목조회.PrimaryKey = new DataColumn[] { dt품목조회.Columns["CD_ITEM"] };

            _flex.Redraw = false;

            bool isAdd = false;
            DataTable dtNot품목 = dt품목그룹.Clone();

            foreach (DataRow row엑셀 in dt엑셀.Rows)
            {
                if (D.GetString(row엑셀["CD_ITEM"]) == "") continue;

                DataRow row품목조회 = dt품목조회.Rows.Find(row엑셀["CD_ITEM"]);
                if (row품목조회 == null)
                {
                    dtNot품목.Rows.Add(row엑셀["CD_ITEM"]);
                    continue;
                }

                DataRow newRow = _flex.DataTable.NewRow();
                if (!추가모드여부)
                {
                    newRow["SEQ_GIR"] = 그리드항번최대값 + 1;
                }

                row엑셀["QT_GIR"] = D.GetDecimal(row엑셀["QT_GIR"]) == 0 ? 1 : D.GetDecimal(row엑셀["QT_GIR"]);

                isAdd = true;
                newRow["CD_ITEM"] = row품목조회["CD_ITEM"];
                newRow["NM_ITEM"] = row품목조회["NM_ITEM"];
                newRow["STND_ITEM"] = row품목조회["STND_ITEM"];
                newRow["UNIT"] = row품목조회["UNIT_SO"];
                newRow["CD_SL"] = row품목조회["CD_GISL"];   //CD_GISL , 2011-10-10, 출고창고로 나와지도록 수정
                newRow["NM_SL"] = row품목조회["NM_SL"];
                newRow["TP_ITEM"] = row품목조회["TP_ITEM"];
                newRow["UNIT_SO_FACT"] = row품목조회["UNIT_SO_FACT"];
                newRow["YN_INSPECT"] = "N";
                newRow["TP_IV"] = _매출형태;
                newRow["NO_LC"] = "";
                newRow["NO_EMP"] = bp담당자.CodeValue;
                newRow["GI_PARTNER"] = bp납품처.CodeValue;
                newRow["LN_PARTNER"] = bp납품처.CodeName;
                newRow["DT_DUEDATE"] = newRow["DT_REQGI"] = row엑셀["DT_DUEDATE"];
                newRow["QT_GIR"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row엑셀["QT_GIR"]));
                newRow["QT_GIR_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row엑셀["QT_GIR"]) * D.GetDecimal(row품목조회["UNIT_SO_FACT"]));


                if (is단가)     //2011-09-28, 최승애 추가
                {
                    newRow["UM"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row엑셀["UM"]));
                }
                else
                {
                    newRow["UM"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row품목조회["UM"]));
                }

                newRow["FG_TRACK"] = "R";

                decimal 외화금액_엑셀 = 0m;
                decimal 수량 = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(newRow["QT_GIR"]));
                decimal 단가 = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(newRow["UM"]));
                decimal 환율 = Unit.환율(DataDictionaryTypes.SA, cur환율.DecimalValue);
                string 부가세구분 = D.GetString(cboVAT구분.SelectedValue);
                decimal 부가세율 = curVATRATE.DecimalValue;

                if (is외화금액) //업로드 하려는 엑셀파일이 금액(AM_GIR)이 포함된 양식일경우
                {
                    외화금액_엑셀 = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(row엑셀["AM_GIR"]));

                    if (!is단가)
                    {
                        단가 = 수량 > 0m ? Unit.외화단가(DataDictionaryTypes.SA, 외화금액_엑셀 / 수량) : 0m;
                        newRow["UM"] = 단가;
                    }


                }

                decimal 외화금액, 원화금액, 부가세;
                Duzon.ERPU.MF.Common.Calc.GetAmt(수량, 단가, 환율, 부가세구분, 부가세율, 모듈.SALE, false, out 외화금액, out 원화금액, out 부가세);

                if (is외화금액)
                {
                    외화금액 = 외화금액_엑셀;
                    if (환율 == 1m && 외화금액 != 원화금액)
                    {
                        원화금액 = Unit.원화금액(DataDictionaryTypes.SA, 외화금액);
                        부가세 = Unit.원화금액(DataDictionaryTypes.SA, Calc.GetVat(원화금액, 부가세구분, 부가세율, 모듈.SALE, false));
                    }
                }

                newRow["AM_GIR"] = 외화금액;
                newRow["AM_GIRAMT"] = Unit.원화금액(DataDictionaryTypes.SA, 원화금액);
                newRow["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, 부가세);
                newRow["AMVAT_GIR"] = D.GetDecimal(newRow["AM_GIRAMT"]) + D.GetDecimal(newRow["AM_VAT"]);

                if (BASIC.GetMAEXC("배차사용유무") == "Y")
                    newRow["YN_PICKING"] = _배송여부;

                if (수주Config.부가세포함단가사용())
                    newRow["TP_UM_TAX"] = D.GetString(cbo부가세포함.SelectedValue);
                else
                    newRow["UMVAT_GIR"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(newRow["AMVAT_GIR"]) / D.GetDecimal(newRow["QT_GIR"]));

                _flex.DataTable.Rows.Add(newRow);
            }
            _flex.Redraw = true;

            if (isAdd)
            {
                //_flex.Row = _flex.Rows.Count - 1;
                _flex.Row = _flex.BottomRow;
                btn삭제.Enabled = _flex.HasNormalRow;
                ToolBarDeleteButtonEnabled = ToolBarSaveButtonEnabled = _flex.IsDataChanged = true;
                _flex.SumRefresh();
            }
            
            ShowMessage(공통메세지._작업을완료하였습니다, btn엑셀업로드.Text);

            if (dtNot품목.Rows.Count == 0) return;

            StringBuilder str = new StringBuilder();

            DataTable dtNot품목그룹 = dtNot품목.DefaultView.ToTable(true, "CD_ITEM");
            foreach (DataRow row in dtNot품목그룹.Rows)
            {
                //str.AppendLine("품목코드 -> " + "\t" + D.GetString(row["CD_ITEM"]));
                str.AppendLine(DD("품목코드") + " -> " + "\t" + D.GetString(row["CD_ITEM"]));
            }

            ShowDetailMessage("업로드한 내용중 마스터품목에 등록되지 않은 품목이 존재합니다.\n▼ 버튼을 눌러서 불일치 목록을 확인하세요.", str.ToString());
        }

        #endregion

        #region -> 상품업로드

        void 상품업로드(DataTable dt엑셀)
        {
            System.Diagnostics.Debug.WriteLine("상품업로드");
            DataTable dt유형코드그룹 = dt엑셀.DefaultView.ToTable(true, "CD_SHOP");
            string 멀티유형 = "";
            foreach (DataRow row유형코드 in dt유형코드그룹.Rows)
            {
                멀티유형 += D.GetString(row유형코드["CD_SHOP"]) + "|";
            }

            string 공장 = D.GetString(cbo공장.SelectedValue);
            string 의뢰일자 = dp의뢰일자.Text;

            DataTable dt품목조회 = _biz.상품관련정보(멀티유형, 공장, 의뢰일자);
            dt품목조회.PrimaryKey = new DataColumn[] { dt품목조회.Columns["CD_SHOP"], dt품목조회.Columns["CD_SPITEM"], dt품목조회.Columns["CD_OPT"], dt품목조회.Columns["CD_ITEM"]  };

            DataTable dtNot상품 = new DataTable();
            dtNot상품.Columns.Add("CD_SHOP", typeof(string));
            dtNot상품.Columns.Add("CD_SPITEM", typeof(string));
            dtNot상품.Columns.Add("CD_OPT", typeof(string));

            dt엑셀.Columns.Add("UM_SALE", typeof(decimal));
            bool isAdd = false;
            foreach (DataRow row상품 in dt엑셀.Rows)
            {
                string 유형 = D.GetString(row상품["CD_SHOP"]);
                string 상품코드 = D.GetString(row상품["CD_SPITEM"]);
                string 옵션 = D.GetString(row상품["CD_OPT"]);

                string Filter = "CD_SHOP = '" + 유형 + "' AND CD_SPITEM = '" + 상품코드 + "' AND CD_OPT = '" + 옵션 + "'";
                DataRow[] dr품목 = dt품목조회.Select(Filter);

                if(dr품목 == null || dr품목.Length == 0)
                {
                    dtNot상품.Rows.Add(유형, 상품코드, 옵션);
                    continue;
                }

                isAdd = true;
                decimal 환율 = Unit.환율(DataDictionaryTypes.SA, cur환율.DecimalValue);
                string 부가세구분 = D.GetString(cboVAT구분.SelectedValue);
                decimal 부가세율 = curVATRATE.DecimalValue;

                상품에대한품목배부(row상품, dr품목, 부가세구분, 부가세율, 환율);
            }

            if (isAdd)
            {
                //_flex.Row = _flex.Rows.Count - 1;
                _flex.Row = _flex.BottomRow;
                btn삭제.Enabled = _flex.HasNormalRow;
                ToolBarDeleteButtonEnabled = ToolBarSaveButtonEnabled = _flex.IsDataChanged = true;
                _flex.SumRefresh();
            }

            ShowMessage(공통메세지._작업을완료하였습니다, btn엑셀업로드.Text);

            if (dtNot상품.Rows.Count == 0) return;

            StringBuilder str = new StringBuilder();

            DataTable dtNot상품그룹 = dtNot상품.DefaultView.ToTable(true, "CD_ITEM", "CD_SPITEM", "CD_OPT");
            foreach (DataRow row in dtNot상품그룹.Rows)
            {
                //str.AppendLine("쇼핑몰코드 -> " + "\t" + D.GetString(row["CD_ITEM"]) +
                //                "상품코드 -> " + "\t" + D.GetString(row["CD_SPITEM"]) +
                //                "옵션코드 -> " + "\t" + D.GetString(row["CD_OPT"]));
                str.AppendLine(DD("쇼핑몰코드") + " -> " + "\t" + D.GetString(row["CD_ITEM"]) +
                               DD("상품코드") + " -> " + "\t" + D.GetString(row["CD_SPITEM"]) +
                               DD("옵션코드") + " -> " + "\t" + D.GetString(row["CD_OPT"]));
            }

            ShowDetailMessage("업로드한 내용중 마스터품목에 등록되지 않은 품목이 존재합니다.\n▼ 버튼을 눌러서 불일치 목록을 확인하세요.", str.ToString());
        }

        #endregion

        #region -> 상품에대한품목배부

        void 상품에대한품목배부(DataRow row상품, DataRow[] dr품목, string 부가세구분, decimal 부가세율, decimal 환율)
        {
            row상품["QT"] = D.GetDecimal(row상품["QT"]) == 0 ? 1 : D.GetDecimal(row상품["QT"]); 
            Duzon.ERPU.SA.Settng.SettingECToItem EC = new Duzon.ERPU.SA.Settng.SettingECToItem();
            EC.상품판매금액 = "AM_SALE";
            EC.SetItemColumn(dr품목[0]);
            EC.Calc(row상품, dr품목, 환율, 부가세구분, 부가세율);

            DataRow newRow = null;
            foreach (DataRow row품목 in dr품목)
            {
                newRow = _flex.DataTable.NewRow();
                if (!추가모드여부)
                {
                    newRow["SEQ_GIR"] = 그리드항번최대값 + 1;
                }
                // 배송정보셋팅
                newRow["NM_CUST_DLV"] = row상품["NM_CUST_DLV"];
                newRow["NO_TEL_D1"] = row상품["NO_TEL_D1"];
                newRow["NO_TEL_D2"] = row상품["NO_TEL_D2"];
                newRow["CD_ZIP"] = row상품["CD_ZIP"];
                newRow["ADDR1"] = row상품["ADDR1"];
                newRow["ADDR2"] = row상품["ADDR2"];
                newRow["FG_TRACK"] = "R";

                newRow["CD_ITEM"] = row품목["CD_ITEM"];
                newRow["NM_ITEM"] = row품목["NM_ITEM"];
                newRow["STND_ITEM"] = row품목["STND_ITEM"];
                newRow["UNIT"] = row품목["UNIT_SO"];
                newRow["CD_SL"] = row품목["CD_GISL"];   //2011-09-28, 최승애, 입고창고(CD_SL)이 아닌 출고창고(CD_GISL)로 수정함.
                newRow["NM_SL"] = row품목["NM_SL"];     //2011-09-28, 최승애, 입고창고(NM_SL)이 아닌 출고창고(NM_GISL)로 수정함.
                newRow["TP_ITEM"] = row품목["TP_ITEM"];
                newRow["UNIT_SO_FACT"] = row품목["UNIT_SO_FACT"];
                newRow["YN_INSPECT"] = "N";
                newRow["TP_IV"] = _매출형태;
                newRow["NO_LC"] = "";
                newRow["NO_EMP"] = bp담당자.CodeValue;
                newRow["GI_PARTNER"] = bp납품처.CodeValue;
                newRow["LN_PARTNER"] = bp납품처.CodeName;
                newRow["DT_DUEDATE"] = newRow["DT_REQGI"] = row상품["DT_DUEDATE"];
                newRow["QT_GIR"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row품목[EC.품목수배수량]));
                newRow["QT_GIR_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row품목[EC.품목재고수량]));

                newRow["UM"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row품목[EC.품목단가]));
                newRow["AM_GIR"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(row품목[EC.품목외화금액]));
                newRow["AM_GIRAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row품목[EC.품목원화금액]));
                newRow["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row품목[EC.품목부가세]));
                newRow["AMVAT_GIR"] = D.GetDecimal(newRow["AM_GIRAMT"]) + D.GetDecimal(newRow["AM_VAT"]);

                if (BASIC.GetMAEXC("배차사용유무") == "Y")
                    newRow["YN_PICKING"] = _배송여부;

                if (수주Config.부가세포함단가사용())
                    newRow["TP_UM_TAX"] = D.GetString(cbo부가세포함.SelectedValue);
                else
                    newRow["UMVAT_GIR"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(newRow["AMVAT_GIR"]) / D.GetDecimal(newRow["QT_GIR"]));

                _flex.DataTable.Rows.Add(newRow);
            }
        }

        #endregion

        #endregion

        #region ♣ 그리드 이벤트

        #region -> 그리드 변경시작체 체크이벤트(_flex_StartEdit)

        void _flex_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (Use유무환전환 && !txt의뢰번호.Enabled)
                {
                    e.Cancel = true;
                    return;
                }

                if (Global.MainFrame.ServerKeyCommon == "DZSQL" || is중국고객부가세포함단가사용여부)
                {
                    switch (_flex.Cols[e.Col].Name)
                    {
                        case "UM":
                        case "AM_GIR":
                            if (Use부가세포함)
                            {
                                e.Cancel = true;
                                return;
                            }
                            break;
                        case "UMVAT_GIR":
                            if (!Use부가세포함)
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
                    case "DT_DUEDATE":
                    case "CD_SL":
                    case "QT_GIR":
                    case "UM":
                    case "AM_GIR":
                    case "QT_GIR_IM":
                    case "NO_PROJECT" :
                        if (!추가모드여부 && _flex.CDecimal(_flex[e.Row, "QT_GI"]) > 0)
                        {
                            e.Cancel = true;
                            return;
                        }
                        break;
                    case "CD_ITEM":
                        string 관련수불번호 = D.GetString(_flex[e.Row, "NO_IO_MGMT"]);
                        string 관련수주번호 = D.GetString(_flex[e.Row, "NO_SO"]);
                        if (관련수불번호 != "" || 관련수주번호 != "")
                        {
                            e.Cancel = true;
                            return;
                        }
                        break;
                    case "GI_PARTNER" :
                        if (_flex.CDecimal(_flex["QT_GI"]) > 0m || _flex.CDecimal(_flex["QT_GI_IM"]) > 0m)
                        {
                            e.Cancel = true;
                            return;
                        }
                        break;
                    case "YN_INSPECT":
                        if (!추가모드여부)
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
        
        #region -> 그리드 도움창 호출전 세팅 이벤트(_flex_BeforeCodeHelp)

        void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                switch (_flex.Cols[e.Col].Name)
                {
                    case "CD_ITEM":
                        string 관련수불번호 = _flex[e.Row, "NO_IO_MGMT"].ToString();
                        string 관련수주번호 = _flex[e.Row, "NO_SO"].ToString();
                        if (관련수불번호 != "" || 관련수주번호 != "")
                        {
                            e.Cancel = true;
                            return;
                        }
                        e.Parameter.P09_CD_PLANT = cbo공장.SelectedValue.ToString();
                        break;
                    case "CD_SL":
                    case "NM_SL":
                        if (_flex.CDecimal(_flex["QT_GI"]) > 0)
                        {
                            e.Cancel = true;
                            return;
                        }
                        e.Parameter.P09_CD_PLANT = cbo공장.SelectedValue.ToString();
                        break;
                    case "NO_PROJECT":
                        if (_flex.CDecimal(_flex["QT_GI"]) > 0)
                        {
                            e.Cancel = true;
                            return;
                        }
                        e.Parameter.P41_CD_FIELD1 = "프로젝트";             //도움창 이름 찍어줄 값
                        e.Parameter.P14_CD_PARTNER = bp거래처.CodeValue;    //거래처코드
                        e.Parameter.P63_CODE3 = bp거래처.CodeName;          //거래처명
                        e.Parameter.P17_CD_SALEGRP = bp영업그룹.CodeValue;  //영업그룹코드
                        e.Parameter.P62_CODE2 = bp영업그룹.CodeName;        //영업그룹명
                        break;
                    case "GI_PARTNER":
                        if (_flex.CDecimal(_flex["QT_GI"]) > 0 || _flex.CDecimal(_flex["QT_GI_IM"]) > 0)
                        {
                            e.Cancel = true;
                            return;
                        }
                        e.Parameter.P14_CD_PARTNER = bp납품처.CodeValue;
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 그리드 도움창 호출후 변경 이벤트(_flex_AfterCodeHelp)

        void _flex_AfterCodeHelp(object sender, AfterCodeHelpEventArgs e)
        {
            try
            {
                HelpReturn helpReturn = e.Result;

                switch (_flex.Cols[e.Col].Name)
                {
                    case "CD_ITEM":
                        if(e.Result.DialogResult == DialogResult.Cancel) return;

                        bool First = true;
                        _flex.Redraw = false;
                        _flex.SetDummyColumnAll();

                        DataTable dtUmFixed = null;
                        if (_biz.Get특수단가적용 == "003")
                        {
                            string multiItem = Common.MultiString(helpReturn.Rows, "CD_ITEM", "|");
                            dtUmFixed = _biz.SearchUmFixed(bp거래처.CodeValue, D.GetString(cbo공장.SelectedValue), multiItem);
                            dtUmFixed.PrimaryKey = new DataColumn[] { dtUmFixed.Columns["CD_ITEM"] };
                        }

                        foreach(DataRow row in helpReturn.Rows)
                        {
                            if (First)     /* 새로 품목을 입력하는 경우 */
                            {
                                _flex[e.Row, "CD_ITEM"] = row["CD_ITEM"];
                                _flex[e.Row, "NM_ITEM"] = row["NM_ITEM"];
                                _flex[e.Row, "STND_ITEM"] = row["STND_ITEM"];
                                _flex[e.Row, "UNIT"] = row["UNIT_SO"];
                                _flex[e.Row, "CD_SL"] = row["CD_GISL"];     //2011-09-28, 최승애, 입고창고(CD_SL)이 아닌 출고창고(CD_GISL)로 수정함.
                                _flex[e.Row, "NM_SL"] = row["NM_GISL"];     //2011-09-28, 최승애, 입고창고(NM_SL)이 아닌 출고창고(NM_GISL)로 수정함.
                                _flex[e.Row, "TP_ITEM"] = row["TP_ITEM"];
                                _flex[e.Row, "UNIT_SO_FACT"] = _flex.CDecimal(row["UNIT_SO_FACT"]) == 0 ? 1 : row["UNIT_SO_FACT"];
                                _flex[e.Row, "YN_INSPECT"] = "N";
                                _flex[e.Row, "TP_IV"] = _매출형태;
                                _flex[e.Row, "NO_LC"] = "";
                                _flex[e.Row, "NO_EMP"] = bp담당자.CodeValue;
                                _flex[e.Row, "GI_PARTNER"] = bp납품처.CodeValue;
                                _flex[e.Row, "LN_PARTNER"] = bp납품처.CodeName;

                                string 품목과세구분 = D.GetString(row["FG_TAX_SA"]);

                                if (_biz.Get과세변경유무 == "N" || 품목과세구분 == string.Empty || 서버키 != "KOREAF")
                                {
                                    _flex[e.Row, "TP_VAT"] = D.GetString(cboVAT구분.SelectedValue);
                                    _flex[e.Row, "RT_VAT"] = curVATRATE.DecimalValue;
                                }
                                else
                                {
                                    _flex[e.Row, "TP_VAT"] = 품목과세구분;
                                    _flex[e.Row, "RT_VAT"] = D.GetDecimal(row["RT_TAX_SA"]);
                                }

                                if (_biz.Get특수단가적용 == "003" && 서버키 == "KOREAF")  //고정단가
                                {
                                    DataRow rowUmFixed = dtUmFixed.Rows.Find(_flex[e.Row, "CD_ITEM"]);
                                    decimal dUM_BASE = decimal.Zero;

                                    if (rowUmFixed == null)
                                        dUM_BASE = _biz.UmSearch(D.GetString(row["CD_ITEM"]), bp거래처.CodeValue, D.GetString(cbo단가유형.SelectedValue), D.GetString(cbo화폐단위.SelectedValue), _단가적용형태, dp의뢰일자.Text);
                                    else
                                        dUM_BASE = D.GetDecimal(rowUmFixed["UM_FIXED"]);

                                    if (!Use부가세포함 || (Global.MainFrame.ServerKeyCommon != "DZSQL" && !is중국고객부가세포함단가사용여부))
                                        _flex[e.Row, "UM"] = Unit.외화단가(DataDictionaryTypes.SA, dUM_BASE);
                                    else
                                        _flex[e.Row, "UMVAT_GIR"] = Unit.원화단가(DataDictionaryTypes.SA, dUM_BASE);
                                }
                                else
                                {
                                    decimal dUM_BASE = _biz.UmSearch(D.GetString(row["CD_ITEM"]), bp거래처.CodeValue, D.GetString(cbo단가유형.SelectedValue), D.GetString(cbo화폐단위.SelectedValue), _단가적용형태, dp의뢰일자.Text);
                                    if (!Use부가세포함 || (Global.MainFrame.ServerKeyCommon != "DZSQL" && !is중국고객부가세포함단가사용여부))
                                        _flex[e.Row, "UM"] = Unit.외화단가(DataDictionaryTypes.SA, dUM_BASE);
                                    else
                                        _flex[e.Row, "UMVAT_GIR"] = Unit.원화단가(DataDictionaryTypes.SA, dUM_BASE);
                                }

                                //_flex[e.Row, "UM"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex[e.Row, "UM"]));
                                //_flex[e.Row, "AM_GIR"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex[e.Row, "QT_GIR"]) * D.GetDecimal(_flex[e.Row, "UM"]));
                                //_flex[e.Row, "AM_GIRAMT"] = Unit.원화금액(DataDictionaryTypes.SA, _flex.CDecimal(_flex[e.Row, "AM_GIR"]) * cur환율.DecimalValue);
                                //_flex[e.Row, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, _flex.CDecimal(_flex[e.Row, "AM_GIRAMT"]) * (D.GetDecimal(_flex[e.Row, "RT_VAT"]) / 100));
                                //_flex[e.Row, "AMVAT_GIR"] = D.GetDecimal(_flex[e.Row, "AM_GIRAMT"]) + D.GetDecimal(_flex[e.Row, "AM_VAT"]);

                                decimal 의뢰수량 = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_flex[e.Row, "QT_GIR"]));
                                decimal 단가 = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex[e.Row, "UM"]));

                                decimal 외화금액 = decimal.Zero;
                                decimal 원화금액 = decimal.Zero;
                                decimal 부가세 = decimal.Zero;
                                decimal 합계 = decimal.Zero;

                                if (!Use부가세포함 || (Global.MainFrame.ServerKeyCommon != "DZSQL" && !is중국고객부가세포함단가사용여부))
                                {
                                    외화금액 = Unit.외화금액(DataDictionaryTypes.SA, 단가 * 의뢰수량);
                                    원화금액 = cur환율.DecimalValue == 0m ? 0m : Unit.원화금액(DataDictionaryTypes.SA, 외화금액 * cur환율.DecimalValue);
                                    부가세 = Unit.원화금액(DataDictionaryTypes.SA, 원화금액 * (curVATRATE.DecimalValue / 100));
                                    합계 = 원화금액 + 부가세;
                                    _flex[e.Row, "UMVAT_GIR"] = 의뢰수량 == 0m ? 0m : Unit.원화단가(DataDictionaryTypes.SA, 합계 / 의뢰수량);
                                }
                                else
                                {
                                    합계 = Unit.원화금액(DataDictionaryTypes.SA, 의뢰수량 * D.GetDecimal(_flex[e.Row, "UMVAT_GIR"]));
                                    if (Global.MainFrame.LoginInfo.CompanyLanguage == Language.KR)
                                        원화금액 = Decimal.Round(합계 * (100 / (100 + curVATRATE.DecimalValue)), MidpointRounding.AwayFromZero);
                                    else
                                        원화금액 = D.GetDecimal(UDecimal.GetConvertFormatData(DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.INSERT, 합계 * (100 / (100 + D.GetDecimal(_flex[e.Row, "RT_VAT"])))));
                                    부가세 = 합계 - 원화금액;
                                    외화금액 = cur환율.DecimalValue == 0m ? 0m : Unit.외화금액(DataDictionaryTypes.SA, 원화금액 / cur환율.DecimalValue);
                                    _flex["UM"] = 의뢰수량 == 0m ? 0m : Unit.외화단가(DataDictionaryTypes.SA, 외화금액 / 의뢰수량);
                                }

                                _flex[e.Row, "AM_GIR"] = 외화금액;
                                _flex[e.Row, "AM_GIRAMT"] = 원화금액;
                                _flex[e.Row, "AM_VAT"] = 부가세;
                                _flex[e.Row, "AMVAT_GIR"] = 합계;

                                if (BASIC.GetMAEXC("배차사용유무") == "Y")
                                    _flex[e.Row, "YN_PICKING"] = _배송여부;

                                First = false;
                            }
                            else
                            {
                                _flex.Rows.Add();
                                _flex.Row = _flex.Rows.Count - 1;
                                _flex[_flex.Row, "CD_ITEM"] = row["CD_ITEM"];
                                _flex[_flex.Row, "NM_ITEM"] = row["NM_ITEM"];
                                _flex[_flex.Row, "STND_ITEM"] = row["STND_ITEM"];
                                _flex[_flex.Row, "UNIT"] = row["UNIT_SO"];
                                _flex[_flex.Row, "CD_SL"] = row["CD_GISL"];       //2011-09-28, 최승애, 입고창고(CD_SL)이 아닌 출고창고(CD_GISL)로 수정함.
                                _flex[_flex.Row, "NM_SL"] = row["NM_GISL"];       //2011-09-28, 최승애, 입고창고(NM_SL)이 아닌 출고창고(NM_GISL)로 수정함.
                                _flex[_flex.Row, "TP_ITEM"] = row["TP_ITEM"];
                                _flex[_flex.Row, "UNIT_SO_FACT"] = _flex.CDecimal(row["UNIT_SO_FACT"]) == 0 ? 1 : row["UNIT_SO_FACT"];
                                _flex[_flex.Row, "SEQ_GIR"] = 그리드항번최대값 + 1;
                                _flex[_flex.Row, "YN_INSPECT"] = "N";
                                _flex[_flex.Row, "TP_IV"] = _매출형태;
                                _flex[_flex.Row, "NO_LC"] = "";
                                _flex[_flex.Row, "NO_EMP"] = bp담당자.CodeValue;
                                _flex[_flex.Row, "GI_PARTNER"] = bp납품처.CodeValue;
                                _flex[_flex.Row, "LN_PARTNER"] = bp납품처.CodeName;

                                string 품목과세구분 = D.GetString(row["FG_TAX_SA"]);

                                if (_biz.Get과세변경유무 == "N" || 품목과세구분 == string.Empty || 서버키 != "KOREAF")
                                {
                                    _flex[_flex.Row, "TP_VAT"] = D.GetString(cboVAT구분.SelectedValue);
                                    _flex[_flex.Row, "RT_VAT"] = curVATRATE.DecimalValue;
                                }
                                else
                                {
                                    _flex[_flex.Row, "TP_VAT"] = 품목과세구분;
                                    _flex[_flex.Row, "RT_VAT"] = D.GetDecimal(row["RT_TAX_SA"]);
                                }

                                if (_biz.Get특수단가적용 == "003" && 서버키 == "KOREAF")  //고정단가
                                {
                                    DataRow rowUmFixed = dtUmFixed.Rows.Find(_flex[_flex.Row, "CD_ITEM"]);
                                    decimal dUM_BASE = decimal.Zero;

                                    if (rowUmFixed == null)
                                        dUM_BASE = _biz.UmSearch(D.GetString(row["CD_ITEM"]), bp거래처.CodeValue, D.GetString(cbo단가유형.SelectedValue), D.GetString(cbo화폐단위.SelectedValue), _단가적용형태, dp의뢰일자.Text);
                                    else
                                        dUM_BASE = D.GetDecimal(rowUmFixed["UM_FIXED"]);

                                    if (!Use부가세포함 || (Global.MainFrame.ServerKeyCommon != "DZSQL" && !is중국고객부가세포함단가사용여부))
                                        _flex[_flex.Row, "UM"] = Unit.외화단가(DataDictionaryTypes.SA, dUM_BASE);
                                    else
                                        _flex[_flex.Row, "UMVAT_GIR"] = Unit.원화단가(DataDictionaryTypes.SA, dUM_BASE);
                                }
                                else
                                {
                                    decimal dUM_BASE = _biz.UmSearch(D.GetString(row["CD_ITEM"]), bp거래처.CodeValue, D.GetString(cbo단가유형.SelectedValue), D.GetString(cbo화폐단위.SelectedValue), _단가적용형태, dp의뢰일자.Text);
                                    if (!Use부가세포함 || (Global.MainFrame.ServerKeyCommon != "DZSQL" && !is중국고객부가세포함단가사용여부))
                                        _flex[_flex.Row, "UM"] = Unit.외화단가(DataDictionaryTypes.SA, dUM_BASE);
                                    else
                                        _flex[_flex.Row, "UMVAT_GIR"] = Unit.원화단가(DataDictionaryTypes.SA, dUM_BASE);
                                }

                                //_flex[_flex.Row, "UM"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex[_flex.Row, "UM"]));

                                if (BASIC.GetMAEXC("배차사용유무") == "Y")
                                    _flex[_flex.Row, "YN_PICKING"] = _배송여부;
                                
                                _flex[_flex.Row, "DT_DUEDATE"] = _flex[_flex.Row - 1, "DT_DUEDATE"];

                                _flex.AddFinished();
                            }
                        }

                        _flex.RemoveDummyColumnAll();
                        
                        _flex.Col = _flex.Cols.Fixed;
                        _flex.Redraw = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 그리드 Cell 수정시 변경 이벤트(_flex_ValidateEdit)

        private void _flex_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                string OldValue = _flex.GetData(e.Row, e.Col).ToString();
                string NewValue = _flex.EditData;

                if (OldValue.ToUpper() == NewValue.ToUpper()) return;

                decimal rtVat = curVATRATE.DecimalValue;

                if (D.GetString(_flex[e.Row, "TP_VAT"]) != string.Empty)
                    rtVat = D.GetDecimal(_flex[e.Row, "RT_VAT"]);

                switch (_flex.Cols[e.Col].Name)
                {
                    case "QT_GIR":
                        //반품수량체크 프로스져를 호출한다.
                        if (_flex[e.Row, "NO_IO_MGMT"].ToString() == "" || _biz.Check(txt의뢰번호.Text, _flex.CDecimal(_flex[e.Row, "SEQ_GIR"]), _flex[e.Row, "NO_IO_MGMT"].ToString(), _flex.CDecimal(_flex[e.Row, "NO_IOLINE_MGMT"]), _flex.CDecimal(NewValue)))
                        {
                            //_flex[e.Row, "AM_GIR"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex[e.Row, "UM"]) * D.GetDecimal(NewValue));
                            //_flex[e.Row, "AM_GIRAMT"] = Unit.원화금액(DataDictionaryTypes.SA, _flex.CDecimal(_flex[e.Row, "AM_GIR"]) * cur환율.DecimalValue);
                            //_flex[e.Row, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, _flex.CDecimal(_flex[e.Row, "AM_GIRAMT"]) * (rtVat / 100));
                            //_flex[e.Row, "AMVAT_GIR"] = D.GetDecimal(_flex[e.Row, "AM_GIRAMT"]) + D.GetDecimal(_flex[e.Row, "AM_VAT"]);

                            decimal 의뢰수량 = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(NewValue));
                            decimal 단가 = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex[e.Row, "UM"]));

                            decimal 외화금액 = decimal.Zero;
                            decimal 원화금액 = decimal.Zero;
                            decimal 부가세 = decimal.Zero;
                            decimal 합계 = decimal.Zero;

                            if (!Use부가세포함 || (Global.MainFrame.ServerKeyCommon != "DZSQL" && !is중국고객부가세포함단가사용여부))
                            {
                                외화금액 = Unit.외화금액(DataDictionaryTypes.SA, 단가 * 의뢰수량);
                                원화금액 = cur환율.DecimalValue == 0m ? 0m : Unit.원화금액(DataDictionaryTypes.SA, 외화금액 * cur환율.DecimalValue);
                                부가세 = Unit.원화금액(DataDictionaryTypes.SA, 원화금액 * (rtVat / 100));
                                합계 = 원화금액 + 부가세;
                                _flex["UMVAT_GIR"] = 의뢰수량 == 0m ? 0m : Unit.원화단가(DataDictionaryTypes.SA, 합계 / 의뢰수량);
                            }
                            else
                            {
                                합계 = Unit.원화금액(DataDictionaryTypes.SA, 의뢰수량 * D.GetDecimal(_flex["UMVAT_GIR"]));
                                if (Global.MainFrame.LoginInfo.CompanyLanguage == Language.KR)
                                    원화금액 = Decimal.Round(합계 * (100 / (100 + rtVat)), MidpointRounding.AwayFromZero);
                                else
                                    원화금액 = D.GetDecimal(UDecimal.GetConvertFormatData(DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.INSERT, 합계 * (100 / (100 + rtVat))));
                                부가세 = 합계 - 원화금액;
                                외화금액 = cur환율.DecimalValue == 0m ? 0m : Unit.외화금액(DataDictionaryTypes.SA, 원화금액 / cur환율.DecimalValue);
                                _flex["UM"] = 의뢰수량 == 0m ? 0m : Unit.외화단가(DataDictionaryTypes.SA, 외화금액 / 의뢰수량);
                            }

                            _flex[e.Row, "AM_GIR"] = 외화금액;
                            _flex[e.Row, "AM_GIRAMT"] = 원화금액;
                            _flex[e.Row, "AM_VAT"] = 부가세;
                            _flex[e.Row, "AMVAT_GIR"] = 합계;

                            if (Use유무환전환)
                            {
                                _flex[e.Row, "AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex[e.Row, "UM_SO"]) * D.GetDecimal(NewValue));
                                _flex[e.Row, "AM_SOAMT"] = Unit.원화금액(DataDictionaryTypes.SA, _flex.CDecimal(_flex[e.Row, "AM_SO"]) * cur수주환율.DecimalValue);
                                _flex[e.Row, "AM_SOVAT"] = Unit.원화금액(DataDictionaryTypes.SA, _flex.CDecimal(_flex[e.Row, "AM_SOAMT"]) * (rtVat / 100));
                            }

                            if (_flex.CDecimal(_flex[e.Row, "UNIT_SO_FACT"]) == 0)
                                _flex[e.Row, "QT_GIR_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(NewValue) * 1);
                            else
                                _flex[e.Row, "QT_GIR_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(NewValue) * D.GetDecimal(_flex[e.Row, "UNIT_SO_FACT"]));
                        }
                        else
                            _flex[e.Row, "QT_GIR"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(OldValue));
                        break;

                    case "UM":
                        _flex[e.Row, "AM_GIR"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex[e.Row, "QT_GIR"]) * D.GetDecimal(NewValue));
                        _flex[e.Row, "AM_GIRAMT"] = Unit.원화금액(DataDictionaryTypes.SA, _flex.CDecimal(_flex[e.Row, "AM_GIR"]) * cur환율.DecimalValue);
                        _flex[e.Row, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, _flex.CDecimal(_flex[e.Row, "AM_GIRAMT"]) * (rtVat / 100));
                        _flex[e.Row, "AMVAT_GIR"] = D.GetDecimal(_flex[e.Row, "AM_GIRAMT"]) + D.GetDecimal(_flex[e.Row, "AM_VAT"]);
                        break;

                    case "AM_GIR":
                        if (_flex.CDecimal(_flex[e.Row, "QT_GIR"]) != 0m)
                            _flex[e.Row, "UM"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(NewValue) / D.GetDecimal(_flex[e.Row, "QT_GIR"]));
                        else
                            _flex[e.Row, "UM"] = 0m;

                        _flex[e.Row, "AM_GIRAMT"] = Unit.원화금액(DataDictionaryTypes.SA, _flex.CDecimal(NewValue) * cur환율.DecimalValue);
                        _flex[e.Row, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, _flex.CDecimal(_flex[e.Row, "AM_GIRAMT"]) * (rtVat / 100));
                        _flex[e.Row, "AMVAT_GIR"] = D.GetDecimal(_flex[e.Row, "AM_GIRAMT"]) + D.GetDecimal(_flex[e.Row, "AM_VAT"]);
                        break;

                    case "DT_DUEDATE":
                        if (!D.StringDate.IsValidDate(NewValue, false, ""))
                        {
                            e.Cancel = true;
                            return;
                        }
                        _flex["DT_REQGI"] = NewValue;
                        break;

                    case "UM_SO":
                        _flex[e.Row, "AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex[e.Row, "QT_GIR"]) * D.GetDecimal(NewValue));
                        _flex[e.Row, "AM_SOAMT"] = Unit.원화금액(DataDictionaryTypes.SA, _flex.CDecimal(_flex[e.Row, "AM_SO"]) * cur수주환율.DecimalValue);
                        _flex[e.Row, "AM_SOVAT"] = Unit.원화금액(DataDictionaryTypes.SA, _flex.CDecimal(_flex[e.Row, "AM_SOAMT"]) * (rtVat / 100));
                        break;

                    case "AM_SO":
                        if (_flex.CDecimal(_flex[e.Row, "QT_GIR"]) != 0)
                            _flex[e.Row, "UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(NewValue) / D.GetDecimal(_flex[e.Row, "QT_GIR"]));
                        else
                            _flex[e.Row, "UM_SO"] = 0m;

                        _flex[e.Row, "AM_SOAMT"] = Unit.원화금액(DataDictionaryTypes.SA, _flex.CDecimal(NewValue) * cur수주환율.DecimalValue);
                        _flex[e.Row, "AM_SOVAT"] = Unit.원화금액(DataDictionaryTypes.SA, _flex.CDecimal(_flex[e.Row, "AM_SOAMT"]) * (rtVat / 100));
                        break;

                    case "TP_VAT":
                        _flex[e.Row, "RT_VAT"] = BASIC.GetTPVAT(NewValue);
                        _flex[e.Row, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex[e.Row, "AM_GIRAMT"]) * (D.GetDecimal(_flex[e.Row, "RT_VAT"]) / 100));
                        _flex[e.Row, "AMVAT_GIR"] = D.GetDecimal(_flex[e.Row, "AM_GIRAMT"]) + D.GetDecimal(_flex[e.Row, "AM_VAT"]);

                        break;

                    case "UMVAT_GIR":
                        _flex["AMVAT_GIR"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["QT_GIR"]) * D.GetDecimal(NewValue));
                        if (Global.MainFrame.LoginInfo.CompanyLanguage == Language.KR)
                            _flex["AM_GIRAMT"] = Decimal.Round(D.GetDecimal(_flex["AMVAT_GIR"]) * (100 / (100 + rtVat)), MidpointRounding.AwayFromZero);
                        else
                            _flex["AM_GIRAMT"] = D.GetDecimal(UDecimal.GetConvertFormatData(DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.INSERT, (D.GetDecimal(_flex["AMVAT_GIR"]) * (100 / (100 + rtVat)))));
                        _flex["AM_VAT"] = D.GetDecimal(_flex["AMVAT_GIR"]) - D.GetDecimal(_flex["AM_GIRAMT"]);
                        _flex["AM_GIR"] = cur환율.DecimalValue == 0m ? 0m : Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_GIRAMT"]) / cur환율.DecimalValue);
                        _flex["UM"] = D.GetDecimal(_flex["QT_GIR"]) == 0m ? 0m : Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flex["AM_GIR"]) / D.GetDecimal(_flex["QT_GIR"]));
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

        #region ♣ 도움창 이벤트

        #region -> 도움창 Clear 이벤트(OnBpControl_CodeChanged)

        private void OnBpControl_CodeChanged(object sender, EventArgs e)
        {
            try
            {
                BpCodeTextBox bpControl = sender as BpCodeTextBox;
                if (bpControl == null) return;

                switch (bpControl.Name)
                {
                    case "bp거래처":
                        if (_biz.Get영업그룹적용유무 == "Y")
                        {
                            bp영업그룹.CodeValue = "";
                            bp영업그룹.CodeName = "";
                            _header.CurrentRow["CD_SALEGRP"] = "";
                            _header.CurrentRow["NM_SALEGRP"] = "";
                        }
                        if (_biz.Get담당자적용유무 == "Y")
                        {
                            bp담당자.CodeValue = "";
                            bp담당자.CodeName = "";
                            _header.CurrentRow["NO_EMP"] = "";
                            _header.CurrentRow["NM_KOR"] = "";
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

        #region -> 도움창 호출전 세팅 이벤트(Control_QueryBefore)

        private void Control_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                switch (e.HelpID)
                {
                    case HelpID.P_PU_EJTP_SUB:
                        e.HelpParam.P61_CODE1 = "041|042|";
                        break;
                    case HelpID.P_SA_TPPTR_SUB: // 매출거래처
                        e.HelpParam.P14_CD_PARTNER = bp거래처.CodeValue;
                        break;
                    case HelpID.P_SA_TPSO_SUB:
                        e.HelpParam.P61_CODE1 = "N";
                        e.HelpParam.P62_CODE2 = "Y";
                        break;
                    case HelpID.P_MA_SL_SUB:
                        e.HelpParam.P09_CD_PLANT = cbo공장.SelectedValue == null ? Global.MainFrame.LoginInfo.CdPlant : D.GetString(cbo공장.SelectedValue);
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 도움창 호출후 변경 이벤트(Control_QueryAfter)

        private void Control_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                string 유무환구분 = "";

                if (e.DialogResult == DialogResult.OK)
                {
                    System.Data.DataRow[] rows = e.HelpReturn.Rows;
                    switch (e.HelpID)
                    {
                        case HelpID.P_PU_EJTP_SUB:
                            _매출형태 = e.HelpReturn.Rows[0]["FG_TPSALE"].ToString();
                            유무환구분 = e.HelpReturn.Rows[0]["YN_AM"].ToString();
                            _매출유무 = e.HelpReturn.Rows[0]["YN_SALE"].ToString();
                            _배송여부 = e.HelpReturn.Rows[0]["YN_PICKING"].ToString();
                            if (유무환구분 == "Y")
                            {
                                txt유무환구분.Text = "Y";

                                if (txt유무환구분.Text == "Y" &&
                                    //Master L/C, Master 기타 추가건으로 인해서 수출만 조건에 넣음.
                                    //구매승인서 && Local L/C 는 기존에 있던대로 조건에 넣지 않음
                                    //D.GetString(cbo거래구분.SelectedValue) != "002" &&  //구매승인서
                                    //D.GetString(cbo거래구분.SelectedValue) != "003" &&  //Local L/C
                                    D.GetString(cbo거래구분.SelectedValue) != "004" &&  //Master L/C
                                    D.GetString(cbo거래구분.SelectedValue) != "005"     //Master 기타
                                    )
                                    cboVAT구분.Enabled = true;
                            }
                            else
                            {
                                txt유무환구분.Text = "N";
                            }
                            break;
                        case HelpID.P_MA_PARTNER_SUB:
                            bp납품처.CodeValue = e.HelpReturn.Rows[0]["CD_PARTNER"].ToString();
                            bp납품처.CodeName = e.HelpReturn.Rows[0]["LN_PARTNER"].ToString();

                            DataRow row거래처부가 = BASIC.GetPartner(bp거래처.CodeValue);

                            if (_biz.Get영업그룹적용유무 == "Y")
                            {
                                bp영업그룹.CodeValue = row거래처부가["CD_SALEGRP"].ToString();
                                bp영업그룹.CodeName = row거래처부가["NM_SALEGRP"].ToString();
                                _header.CurrentRow["CD_SALEGRP"] = row거래처부가["CD_SALEGRP"].ToString();
                                _header.CurrentRow["NM_SALEGRP"] = row거래처부가["NM_SALEGRP"].ToString();

                                영업그룹변경시셋팅(bp영업그룹.CodeValue);
                            }

                            if (_biz.Get담당자적용유무 == "Y")
                            {
                                DataTable dt = _biz.Get거래처영업담당자(D.GetString(bp거래처.CodeValue));
                                if (dt.Rows.Count == 1)
                                {
                                    bp담당자.CodeValue = D.GetString(dt.Rows[0]["CD_EMP_SALE"]);
                                    bp담당자.CodeName = D.GetString(dt.Rows[0]["NM_EMP_SALE"]);
                                    _header.CurrentRow["NO_EMP"] = D.GetString(dt.Rows[0]["CD_EMP_SALE"]);
                                    _header.CurrentRow["NM_KOR"] = D.GetString(dt.Rows[0]["NM_EMP_SALE"]);
                                }
                            }

                            if (_biz.Get단가유형적용유무 == "Y")
                            {
                                cbo단가유형.SelectedValue = row거래처부가["FG_UM"];
                                _header.CurrentRow["FG_UM"] = D.GetString(cbo단가유형.SelectedValue);
                            }
                            break;
                        case HelpID.P_SA_TPPTR_SUB:
                            //bp거래처.CodeValue = e.HelpReturn.Rows[0]["CD_PARTNER"].ToString();
                            //bp거래처.CodeName = e.HelpReturn.Rows[0]["LN_PARTNER"].ToString();
                            break;
                        case HelpID.P_MA_SALEGRP_SUB:
                            _단가적용형태 = e.HelpReturn.Rows[0]["TP_SALEPRICE"].ToString();
                            break;
                        default:
                            break;
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

        #region ♣ 기타 이벤트

        #region -> Page_DataChanged

        void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsChanged())
                    ToolBarSaveButtonEnabled = true;


                if (!_flex.HasNormalRow)
                {
                    if (추가모드여부)
                    {
                        dp의뢰일자.Focus();
                        cbo공장.Enabled = true;
                        cbo화폐단위.Enabled = true;
                        if (MA.기준환율.Option == MA.기준환율옵션.적용_수정불가)
                            cur환율.Enabled = false;
                        else
                        {
                            if (D.GetString(cbo화폐단위.SelectedValue) != "000")    //KRW
                                cur환율.Enabled = true;
                            else
                                cur환율.Enabled = false;
                        }
                        cboVAT구분.Enabled = true;
                        curVATRATE.Enabled = true;
                    }
                    btn삭제.Enabled = false;
                    btn추가.Enabled = true;
                    btn엑셀업로드.Enabled = true;
                    cbo부가세포함.Enabled = true;

                }
                else
                {
                    cbo공장.Enabled = false;
                    cbo화폐단위.Enabled = false;
                    cur환율.Enabled = false;
                    cboVAT구분.Enabled = false;
                    curVATRATE.Enabled = false;
                    btn삭제.Enabled = true;
                    cbo부가세포함.Enabled = false;

                    if (!_헤더수정여부)
                        btn추가.Enabled = btn엑셀업로드.Enabled  = false;
                    else
                        btn추가.Enabled = btn엑셀업로드.Enabled = true;
                }
                
                if (!추가모드여부)
                    ToolBarDeleteButtonEnabled = true;
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
                switch (((Control)sender).Name)
                {
                    case "cbo거래구분":
                        if (cbo거래구분.SelectedValue.ToString() == "" || cbo거래구분.SelectedValue.ToString() == "001") //수주유형이 국내
                        {
                            cboVAT구분.SelectedValue = "11";
                            cboVAT구분.Enabled = true;
                        }
                        else if (cbo거래구분.SelectedValue.ToString() == "002" || cbo거래구분.SelectedValue.ToString() == "003") //수주유형이 LOCAL LC, 구매승인서
                        {
                            cboVAT구분.SelectedValue = "14";
                            cboVAT구분.Enabled = false;
                        }
                        else if (cbo거래구분.SelectedValue.ToString() == "004" || cbo거래구분.SelectedValue.ToString() == "005") //수주유형이 MASTER L/C, MASTER 기타
                        {
                            cboVAT구분.SelectedValue = "15";
                            cboVAT구분.Enabled = false;
                        }

                        VAT구분셋팅();

                        break;
                }

                if (IsChanged())
                    ToolBarSaveButtonEnabled = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> _header_JobModeChanged

        void _header_JobModeChanged(object sender, FreeBindingArgs e)
        {
            try
            {
                if (e.JobMode == JobModeEnum.추가후수정)
                {
                    m_pnlHead.Enabled = true;
                    txt의뢰번호.Enabled = true;
                    cbo거래구분.Enabled = true;
                    if (_biz.Get영업그룹적용유무 != "Y")  //거래처부가정보의 거래처와영업그룹매핑을 할 경우
                        bp영업그룹.Enabled = true;
                    //if (_biz.Get담당자적용유무 != "Y")
                        bp담당자.Enabled = true;
                    bp반품형태.Enabled = true;
                    cbo화폐단위.Enabled = true;

                    if (MA.기준환율.Option == MA.기준환율옵션.적용_수정불가)
                        cur환율.Enabled = false;
                    else
                    {
                        if (D.GetString(cbo화폐단위.SelectedValue) != "000")    //KRW
                            cur환율.Enabled = true;
                        else
                            cur환율.Enabled = false;
                    }
                    rdo일괄.Enabled = true;
                    rdo건별.Enabled = true;
                    cbo공장.Enabled = true;
                    cboVAT구분.Enabled = true;

                    dp의뢰일자.Enabled = true;
                    bp거래처.Enabled = true;
                    cbo단가유형.Enabled = true;
                    cbo부가세포함.Enabled = true;
                }
                else
                {
                    if (!_헤더수정여부)
                    {
                        m_pnlHead.Enabled = false;
                        btn추가.Enabled = btn엑셀업로드.Enabled = false;
                    }
                    else
                    {
                        m_pnlHead.Enabled = true;
                        txt의뢰번호.Enabled = false;
                        cbo거래구분.Enabled = false;
                        bp영업그룹.Enabled = false;
                        bp반품형태.Enabled = false;
                        cbo화폐단위.Enabled = false;
                        cur환율.Enabled = false;
                        rdo일괄.Enabled = false;
                        rdo건별.Enabled = false;
                        cbo공장.Enabled = false;
                        cboVAT구분.Enabled = false;
                        btn추가.Enabled = btn엑셀업로드.Enabled = true;

                        dp의뢰일자.Enabled = false;
                        bp거래처.Enabled = false;
                        cbo단가유형.Enabled = false;
                        cbo부가세포함.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> Control_SelectionChangeCommitted

        private void Control_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                switch (((Control)sender).Name)
                {
                    case "cbo화폐단위": 
                        if (D.GetString(cbo부가세포함.SelectedValue) == "Y")
                        {
                            ShowMessage("부가세포함여부가 'YES' 일 때에는 원화(KRW)만 선택 할 수 있습니다.");
                            cbo화폐단위.SelectedValue = "000";
                            _header.CurrentRow["CD_EXCH"] = "000";
                            return;
                        }

                        화폐단위셋팅(cbo화폐단위, cur환율);
                        break;
                    case "cbo수주화폐단위":
                        화폐단위셋팅(cbo수주화폐단위, cur수주환율);
                        break;
                    case "cboVAT구분":
                        VAT구분셋팅();
                        break;
                    case "cbo부가세포함":
                        if (D.GetString(cbo부가세포함.SelectedValue) == "Y")
                        {
                            if (D.GetString(cbo화폐단위.SelectedValue) != "000")
                            {
                                ShowMessage("부가세포함여부는 원화(KRW)인 경우에만 'YES'로 변경 할 수 있습니다.");
                                cbo부가세포함.SelectedValue = "N";
                                return;
                            }
                        }
                        // 라인추가로 입력시 아래 기능이용하면 되지만, 금액재계산까지 해주어야 하기때문에,
                        // 라인추가시 해당 컨트롤 비활성화로 진행.. 
                        // 추후 라인추가하면서 변경하고 싶다면 아래 기능에 재계산로직만 추가해주면됨.
                        //if (_flex.HasNormalRow)
                        //{
                        //    foreach (DataRow row in _flex.DataTable.Rows)
                        //    {
                        //        if (row.RowState == DataRowState.Deleted) continue;
                        //        row["TP_UM_TAX"] = D.GetString(cbo부가세포함.SelectedValue);
                        //    }
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

        #endregion

        #region ♣ 기타메소드

        #region -> IsChanged

        protected override bool IsChanged()
        {
            if (base.IsChanged())       // 그리드가 변경되었거나
                return true;

            return 헤더변경여부;        // 헤더가 변경되었거나
        }

        #endregion

        #region -> 화폐단위셋팅

        private void 화폐단위셋팅(DropDownComboBox cboCdExch, CurrencyTextBox curRtExch)
        {
            if (cboCdExch.SelectedValue == null || D.GetString(cboCdExch.SelectedValue) != "000")
            {
                decimal _환율 = 0;
                
                if (MA.기준환율.Option != MA.기준환율옵션.적용안함)
                    _환율 = MA.기준환율적용(dp의뢰일자.Text, D.GetString(cboCdExch.SelectedValue));

                curRtExch.DecimalValue = _환율;
                _header.CurrentRow["RT_EXCH"] = _환율;

                if (MA.기준환율.Option == MA.기준환율옵션.적용_수정불가)
                    curRtExch.Enabled = false;
                else
                    curRtExch.Enabled = true;
            }
            else if (D.GetString(cboCdExch.SelectedValue) == "000") //KRW
            {
                curRtExch.DecimalValue = 1;
                _header.CurrentRow["RT_EXCH"] = 1;
                curRtExch.Enabled = false;
            }
        }

        #endregion

        #region -> VAT구분셋팅

        private void VAT구분셋팅()
        {
            if (cboVAT구분.SelectedValue == null || cboVAT구분.SelectedValue.ToString() == "" || cboVAT구분.DataSource == null)
            {
                curVATRATE.DecimalValue = 0;
                _header.CurrentRow["RT_VAT"] = 0;
                return;
            }

            DataTable dt = (DataTable)cboVAT구분.DataSource;
            DataRow row = dt.Rows.Find(cboVAT구분.SelectedValue);

            curVATRATE.DecimalValue = _flex.CDecimal(row["CD_FLAG1"]);
            _header.CurrentRow["RT_VAT"] = _flex.CDecimal(row["CD_FLAG1"]);
        }

        #endregion

        #region -> 예외추가여부

        bool 예외추가여부
        {
            get
            {
                DataRow[] dr = _flex.DataTable.Select("ISNULL(NO_SO, '') = '' AND ISNULL(NO_IO_MGMT, '') = ''", "", DataViewRowState.CurrentRows);
                if (dr != null && dr.Length > 0)
                    return true;

                return false;
            }
        }

        #endregion

        #region -> 수주반품적용여부

        bool 수주반품적용여부
        {
            get
            {
                DataRow[] dr = _flex.DataTable.Select("ISNULL(NO_SO, '') <> '' OR ISNULL(NO_IO_MGMT, '') <> ''", "", DataViewRowState.CurrentRows);
                if (dr != null && dr.Length > 0)
                    return true;
                return false;
            }
        }

        #endregion

        #region -> 출하적용여부

        bool 출하적용여부
        {
            get
            {
                DataRow[] dr = _flex.DataTable.Select("ISNULL(NO_SO_MGMT, '') <> ''", "", DataViewRowState.CurrentRows);
                if (dr != null && dr.Length > 0)
                    return true;
                return false;
            }
        }

        #endregion

        #region -> LOT/SERIAL SETTING

        private void dtSetting(DataRow[] drs, DataTable dt)
        {
            DataRow rowQtiotpInfo = Duzon.ERPU.MF.Common.CodeSearch.GetCodeInfo(MasterSearch.MM_EJTP, new string[] { MA.Login.회사코드, bp반품형태.CodeValue });

            foreach (DataRow dr in drs)
            {
                DataRow dr2 = dt.NewRow();
                dr2["NO_IO"] = string.Empty;
                dr2["NO_IOLINE"] = dr["SEQ_GIR"];
                dr2["NO_ISURCV"] = string.Empty;
                dr2["NO_GIR"] = string.Empty;
                dr2["DT_DUEDATE"] = dp의뢰일자.Text;                        //납품일자
                dr2["FG_TRANS"] = D.GetString(cbo거래구분.SelectedValue);   //거래구분
                dr2["CD_QTIOTP"] = bp반품형태.CodeValue;                    //출하형태코드(출고형태)
                dr2["NM_QTIOTP"] = rowQtiotpInfo["NM_QTIOTP"];              //출하형태명(출고형태)
                dr2["DT_IO"] = dp의뢰일자.Text;         //출하일자
                dr2["CD_SL"] = dr["CD_SL"];             //창고코드
                dr2["NM_SL"] = dr["NM_SL"];             //창고명
                dr2["CD_ITEM"] = dr["CD_ITEM"];         //품목코드
                dr2["NM_ITEM"] = dr["NM_ITEM"];         //품목명
                dr2["STND_ITEM"] = dr["STND_ITEM"];     //규격
                dr2["UNIT"] = dr["UNIT"];               //단위
                dr2["UNIT_IM"] = dr["UNIT"];            //단위
                dr2["FG_IO"] = rowQtiotpInfo["FG_IO"];  //수불구분
                dr2["QT_GIR"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(dr["QT_GIR"]));
                dr2["UNIT_SO_FACT"] = dr["UNIT_SO_FACT"];
                dr2["QT_GIR_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(dr["QT_GIR_IM"]));
                dr2["QT_IO"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(dr["QT_GIR_IM"]));
                dr2["QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(dr["QT_GIR_IM"]));
                dr2["CD_PLANT"] = D.GetString(cbo공장.SelectedValue);
                dr2["CD_PJT"] = dr["NO_PROJECT"];
                dr2["NO_PROJECT"] = dr["NO_PROJECT"];
                dr2["NM_PROJECT"] = dr["NM_PROJECT"];
                dr2["NO_EMP"] = bp담당자.CodeValue;
                dr2["NO_LOT"] = D.GetString(dr["FG_SERNO"]) == "002" ? "YES" : "NO";
                dr2["NO_SERL"] = D.GetString(dr["FG_SERNO"]) == "003" ? "YES" : "NO";
                dr2["NO_PSO_MGMT"] = dr["NO_SO_MGMT"];
                dr2["NO_PSOLINE_MGMT"] = dr["NO_SOLINE_MGMT"];
                dr2["NO_IO_MGMT"] = dr["NO_IO_MGMT"];
                dr2["NO_IOLINE_MGMT"] = dr["NO_IOLINE_MGMT"];
                dr2["UM_EX"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(dr["UM"]));
                dr2["AM_EX"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["AM_GIR"]));
                dr2["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["AM_GIRAMT"]));

                dt.Rows.Add(dr2.ItemArray);
            }
        }

        #endregion

        #endregion

        #region ♣ 속성들

        #region -> 적용시컨트롤Enabled

        bool 적용시컨트롤Enabled
        {
            set
            {
                bp거래처.Enabled = value;
                cbo공장.Enabled = value;
                cbo거래구분.Enabled = value;

                if (value == true && _biz.Get영업그룹적용유무 != "Y")  //거래처부가정보의 거래처와영업그룹매핑을 할 경우
                    bp영업그룹.Enabled = true;
                else
                    bp영업그룹.Enabled = false;

                //if (value == true && _biz.Get담당자적용유무 != "Y")
                bp담당자.Enabled = value;
                //else
                //    bp담당자.Enabled = false;

                bp반품형태.Enabled = value;
                cboVAT구분.Enabled = value;
                cbo화폐단위.Enabled = value;
                rdo일괄.Enabled = value;
                rdo건별.Enabled = value;
                cbo단가유형.Enabled = value;
                cbo부가세포함.Enabled = value;
            }
        }

        #endregion

        #region -> 영업그룹변경시셋팅
        void 영업그룹변경시셋팅(string 영업그룹)
        {
            try
            {
                DataRow row영업그룹 = BASIC.GetSaleGrp(영업그룹);
                _단가적용형태 = D.GetString(row영업그룹["TP_SALEPRICE"]);
            }
            catch(Exception ex)
            {
                _단가적용형태 = "";
                MsgEnd(ex);
            }
        }
        #endregion

        #region -> 필수값체크

        bool 필수값체크
        {
            get
            {
                if (dp의뢰일자.Text == "")
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, lbl의뢰일자.Text);
                    dp의뢰일자.Focus();
                    return false;
                }

                if (cbo거래구분.SelectedValue == null || cbo거래구분.SelectedValue.ToString() == "")
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, lbl거래구분.Text);
                    cbo거래구분.Focus();
                    return false;
                }

                if (bp영업그룹.CodeValue == "")
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, lbl영업그룹.Text);
                    bp영업그룹.Focus();
                    return false;
                }

                if (bp담당자.CodeValue == "")
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, lbl담당자.Text);
                    bp담당자.Focus();
                    return false;
                }

                if (bp반품형태.CodeValue == "")
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, lbl반품형태.Text);
                    bp반품형태.Focus();
                    return false;
                }

                if (cbo화폐단위.SelectedValue == null || cbo화폐단위.SelectedValue.ToString() == "")
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, lbl화폐단위.Text);
                    cbo화폐단위.Focus();
                    return false;
                }

                return true;
            }
        }

        #endregion

        #region -> 추가모드여부

        bool 추가모드여부
        {
            get
            {
                if (_header.JobMode == JobModeEnum.추가후수정)
                    return true;

                return false;
            }
        }

        #endregion

        #region -> 헤더변경여부

        private bool 헤더변경여부
        {
            get
            {
                bool bChange = false;

                bChange = _header.GetChanges() != null ? true : false;

                // 헤더가 변경됬지만 추가모드이고 디테일 그리드가 아무 내용이 없으면 변경안된걸로 본다.
                if (bChange && _header.JobMode == JobModeEnum.추가후수정 && !_flex.HasNormalRow)
                    bChange = false;

                return bChange;
            }

        }

        #endregion

        #region -> 그리드항번최대값

        private decimal 그리드항번최대값
        {
            get
            {
                decimal dMaxValue = _flex.GetMaxValue("SEQ_GIR");
                return dMaxValue;
            }
        }

        #endregion
          
        #region -> 품목단가 & 환산단위 계산
        private void 품목단가정보구하기(object[] m_obj)
        {
            DataSet ds = _biz.ItemInfo_Search(m_obj);

            decimal UM_EX = 0;

            if(ds != null)
            {
                if(_flex.CDecimal(ds.Tables[1].Rows[0]["RATE_EXCHG"]) == 0)
                {
                    ds.Tables[1].Rows[0]["RATE_EXCHG"] = 1m;
                }

                if(_header.CurrentRow["IV"].Equals("Y")  && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    UM_EX = _flex.CDecimal(ds.Tables[0].Rows[0]["UM_ITEM"]) / _flex.CDecimal(ds.Tables[1].Rows[0]["RATE_EXCHG"]);
                    _flex[_flex.BottomRow, "UM"] = Unit.외화단가(DataDictionaryTypes.SA, UM_EX * cur환율.DecimalValue);
                }

            }
        }
        #endregion

        #region -> 영업그룹변경시 단가적용형태 가져오기
        private void bp영업그룹_QueryAfter(object sender, BpQueryArgs e)
        {

            DataRow row영업그룹 = null;

            try
            {
                row영업그룹 = BASIC.GetSaleGrp(bp영업그룹.CodeValue);
                _단가적용형태 = D.GetString(row영업그룹["TP_SALEPRICE"]);
            }
            catch(Exception ex)
            {
                _단가적용형태 = "";
                MsgEnd(ex);
            }
        }
        #endregion

        string 서버키 { get { return Global.MainFrame.ServerKeyCommon.ToUpper(); } }
        bool Chk의뢰일자 { get { return Checker.IsValid(dp의뢰일자, true, lbl의뢰일자.Text); } }
        bool Chk공장 { get { return !Checker.IsEmpty(cbo공장, lbl공장.Text); } }
        bool Chk거래구분 { get { return !Checker.IsEmpty(cbo거래구분, DD("거래구분")); }}
        bool Chk거래처 { get { return !Checker.IsEmpty(bp거래처, lbl거래처.Text); } }
        bool Chk영업그룹 { get { return !Checker.IsEmpty(bp영업그룹, lbl영업그룹.Text); } }
        bool Chk담당자 { get { return !Checker.IsEmpty(bp담당자, DD("담당자")); }}
        bool Chk반품형태 { get { return !Checker.IsEmpty(bp반품형태, DD("반품형태")); } }
        bool Chk과세구분 { get { return !Checker.IsEmpty(cboVAT구분, lblVAT구분.Text); } }
        bool Chk환종 { get { return !Checker.IsEmpty(cbo화폐단위, lbl화폐단위.Text); } }
        bool Chk매출형태 { get { return !Checker.IsEmpty(bp반품형태, lbl반품형태.Text); } }
        bool Chk단가유형 { get { return !Checker.IsEmpty(cbo단가유형, DD("단가유형")); }}
        bool Chk납품처 { get { return !Checker.IsEmpty(bp납품처, DD("납품처")); } }
        bool Use유무환전환 { get { if (PageID == "P_SA_GI_SWITCH_YN_AM") return true; return false; } }
        bool Chk수주형태 { get { return !Checker.IsEmpty(ctx수주형태, lbl수주형태.Text); } }
        bool Chk수주환종 { get { return !Checker.IsEmpty(cbo수주화폐단위, lbl수주화폐단위.Text); } }

        internal string Get거래처 { get { return bp거래처.CodeValue; } }
        internal string Get거래구분 { get { return D.GetString(cbo거래구분.SelectedValue); } }
        internal string Get영업그룹 { get { return bp영업그룹.CodeValue; } }
        internal string Get담당자 { get { return bp담당자.CodeValue; } }
        internal string Get반품형태 { get { return D.GetString(bp반품형태.CodeValue); }}
        internal string Get과세구분 { get { return D.GetString(cboVAT구분.SelectedValue); } }
        internal decimal Get부가세율 { get { return curVATRATE.DecimalValue; }}
        internal string Get유무환구분 { get { return txt유무환구분.Text; } }
        internal string Get환종 { get { return D.GetString(cbo화폐단위.SelectedValue); } }
        internal decimal Get환율 { get { return cur환율.DecimalValue; } }
        internal string Get계산서처리 { get { return rdo일괄.Checked ? "001" : "002"; } }
        internal string Get단가유형 { get { return D.GetString(cbo단가유형.SelectedValue); } }
        internal string Get납품처 { get { return bp납품처.CodeValue; } }
        internal string Get비고 { get { return txt비고.Text; } }
        internal bool Get유무환전환 { get { return PageID == "P_SA_GI_SWITCH_YN_AM" ? true : false; } }

        internal string Get수주형태 { get { return ctx수주형태.CodeValue; } }
        internal string Get수주환종 { get { return D.GetString(cbo수주화폐단위.SelectedValue); } }
        internal decimal Get수주환율 { get { return cur수주환율.DecimalValue; } }

        bool Use부가세포함 { get { if (수주Config.부가세포함단가사용() && D.GetString(cbo부가세포함.SelectedValue) == "Y") return true; return false; } }

        bool 중국고객부가세포함단가사용여부()
        {
            DataTable dt = DBHelper.GetDataTable("SELECT FG_LANG FROM MA_COMPANY WHERE CD_COMPANY = '" + MA.Login.회사코드 + "'");
            bool 중국여부 = false;
            if (dt != null && dt.Rows.Count > 0) 중국여부 = D.GetString(dt.Rows[0]["FG_LANG"]) == "3";
            return 중국여부 && 수주Config.부가세포함단가사용();
        }

        #endregion
    }
}
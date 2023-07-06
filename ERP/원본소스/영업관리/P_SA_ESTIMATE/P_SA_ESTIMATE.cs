using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
// Framework 컴포넌트
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.SA;
using Duzon.Common.BpControls;
using Duzon.Windows.Print;


/*==========================================================
    * 작 성자		: KOPIZZANG\최인성
    * 최초작성일	: 2009-10-07 오전 8:32:34
    * 최종수정자	: 
    * 최종수정일	: 
    * 비고          :
    
============================================================*/

namespace sale
{
    public partial class P_SA_ESTIMATE : PageBase
    {

        #region 생성자 및 전역 변수 선언부

        private P_SA_ESTIMATE_BIZ _biz = new P_SA_ESTIMATE_BIZ();
        private string sNoEst = string.Empty;

        //디테일 테이블 복사를 위한 전역 테이블
        private DataTable CopyTable = new DataTable();

        private FlexGrid _flex = new FlexGrid();

        //해더 복사 여부 체크
        private bool HeaderCheck = false;

        //헤더 추가 여부 체크
        private bool HeaderAddCheck = true;

        //복사 및 신규 추가시 임시 LineSeq를 주기 위함 변수
        private decimal dTempCheckCnt = 0;

        //그리드 DropDownList를 선택하면 ActiveRow Event를 수행함으로
        //이것을 방지 하기위해 Row 변경 여부를 체크함
        private int iOldRowCnt = 0;

        string _단가적용형태 = string.Empty;    //영업그룹을 선택시 단가정보(TP_SALEPRICE)도 가져온다.
        string so_Price = string.Empty;         //영업그룹을 선택시 판매단가통제유무(SO_PRICE)도 가져온다.

        public P_SA_ESTIMATE()
        {
            try
            {
                InitializeComponent();

                //메인 툴바 저장 및 삭제 버튼 자동 활성화
                MainGrids = new FlexGrid[] { _flexH, _flexL, _flexD };

                InitGrid();

                //DetailQueryNeed 사용하기 위해 세팅해줌..
                _flexH.DetailGrids = new FlexGrid[] { _flexL };
                _flexLT.DetailGrids = new FlexGrid[] { _flexD };


            }
            catch (Exception ex)
            {

                MsgEnd(ex);
            }

        }

        #endregion 생성자 및 전역 변수 선언부

        #region InitGrid
        private void InitGrid()
        {
            #region HADER SETTING
            //그리드 컬럼 Setting
            _flexH.BeginSetting(1, 1, false);
            _flexH.SetCol("CD_COMPANY", "회사", false);
            _flexH.SetCol("CD_BIZAREA", "사업장", false);
            _flexH.SetCol("CD_SALEGRP", "영업그룹", false);
            _flexH.SetCol("CD_PARTNER", "거래처", false);
            _flexH.SetCol("NO_EMP", "사원", false);
            _flexH.SetCol("TP_VAT", "VAT구분", false);
            _flexH.SetCol("FG_VAT", "부사세여부", false);
            _flexH.SetCol("NM_SALEGRP", "영업그룹명", false);
            _flexH.SetCol("LN_PARTNER", "거래처명", false);
            _flexH.SetCol("NM_KOR", "사원명", false);
            _flexH.SetCol("NM_PTR", "거래처담당자", false);
            _flexH.SetCol("NO_TEL", "전화번호", false);
            _flexH.SetCol("NO_TEL_EMER", "비상연락", false);
            _flexH.SetCol("NO_EMAIL", "E-MAIL", false);
            _flexH.SetCol("NO_FAX", "FAX", false);
            

            _flexH.SetCol("STA_EST", "상태", false);
            _flexH.SetCol("NO_HST", "차수", 50, false);
            _flexH.SetCol("NO_EST", "견적번호", 100, false);
            _flexH.SetCol("NO_EST_NM", "견적명", 100, true, typeof(string));
            _flexH.SetCol("DT_EST", "견적일자", 100, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexH.SetCol("DT_CONT", "계약예정일", 100, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexH.SetCol("CD_EXCH", "환종", 80, true);
            _flexH.SetCol("RT_EXCH", "환율", 90, true, typeof(decimal), FormatTpType.MONEY);
            _flexH.SetCol("FG_BILL", "결제조건", 80, true);
            _flexH.SetCol("DT_VALID", "유효기간", 100, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexH.SetCol("DC_RMK", "비고", 90, true);


            //그리드 마지막 컬럼 체움
            _flexH.ExtendLastCol = true;

            //그리드 필수 항목 Setting
            _flexH.VerifyNotNull = new string[] { "CD_COMPANY", "CD_BIZAREA", "NO_EST_NM", "CD_SALEGRP", "CD_PARTNER", "NO_EMP", "TP_VAT", "FG_VAT", "STA_EST", "NO_HST", "DT_EST", "DT_CONT", "CD_EXCH", "RT_EXCH" };

            //그리드 색상, Sort, Summary 추가여부
            _flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            _flexH.AfterCodeHelp += new AfterCodeHelpEventHandler(_FlexH_AfterCodeHelp);
            _flexH.ValidateEdit += new ValidateEditEventHandler(_FlexH_ValidateEdit);
            _flexH.StartEdit += new RowColEventHandler(_flex_StartEdit);
            _flexH.AfterRowChange += new RangeEventHandler(_FlexH_AfterRowChange);

            #endregion HADER SETTING

            #region LINE SETTING
            //그리드 컬럼 Setting
            _flexL.BeginSetting(1, 1, false);
            _flexL.SetCol("CD_COMPANY", "회사", false);
            _flexL.SetCol("NO_EST", "견적번호", false);
            _flexL.SetCol("NO_HST", "차수", false);
            _flexL.SetCol("NO_LINE", "항번", false);
            _flexL.SetCol("TEMPCHECK", "임시항번", false);
            _flexL.SetCol("CD_PLANT", "공장", 100, true);
            _flexL.SetCol("CD_ITEM", "품목코드", 90, true);
            _flexL.SetCol("NM_ITEM", "품목명", 120, true);
            _flexL.SetCol("NM_ITEMGRP", "품목군명", false);
            _flexL.SetCol("GRP_ITEM", "품목군", false);
            _flexL.SetCol("STND_ITEM", "규격", 90, true);
            _flexL.SetCol("UNIT", "단위", 90, true);
            _flexL.SetCol("GRP_MFGNM", "제품군", false);
            _flexL.SetCol("AM_ESTVAT", "부가세금액", false);
            _flexL.SetCol("PU_UM", "구매단가", false);
            
            _flexL.SetCol("QT", "수량", 80, true, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("UM", "단가", 90, true, typeof(decimal), FormatTpType.MONEY);
            _flexL.SetCol("AM_STD", "견적기준가", 90, true, typeof(decimal), FormatTpType.MONEY);
            _flexL.SetCol("AM_STD_PO", "견적기준가(구매)", 100, true, typeof(decimal), FormatTpType.MONEY);
            _flexL.SetCol("RT_CUT", "할인율", 100, true, typeof(decimal));
            _flexL.SetCol("AM_EST", "견적금액", 90, true, typeof(decimal), FormatTpType.MONEY);
            _flexL.SetCol("AM_KEST", "견적원화금액", 90, true, typeof(decimal), FormatTpType.MONEY);
            _flexL.SetCol("AM_VAT", "부가세", 90, true, typeof(decimal), FormatTpType.MONEY);
            _flexL.SetCol("DC_RMK", "비고", 100, true);
            _flexL.SetCol("TP_SALEPRICE", "단가정보", false);
            _flexL.SetCol("RATE", "예상마진율", 100, false, typeof(decimal));

            _flexL.Cols["RT_CUT"].Format = "#,##0.#";
            _flexL.Cols["RATE"].Format = "#,##0.###";
            //그리드 마지막 컬럼 체움
            _flexL.ExtendLastCol = true;

            //그리드 색상, Sort, Summary 추가여부
            _flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            //헤더에 필요없는 SUM 제거 && 반듯이 EndSetting 다음에 코딩해줘야한다.
            _flexL.SetExceptSumCol("CD_PLANT", "CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT", "RT_CUT", "DC_RMK", "TP_SALEPRICE");

            //그리드 필수 항목 Setting
            _flexL.VerifyNotNull = new string[] { "NM_ITEM", "QT" };

            ////그리드 특정 컬럼 값 셋팅 후 비교
            _flexL.VerifyCompare(_flexL.Cols["QT"], 0, OperatorEnum.Greater);
            _flexL.VerifyCompare(_flexL.Cols["UM"], 0, OperatorEnum.GreaterOrEqual);
            _flexL.VerifyCompare(_flexL.Cols["AM_STD"], 0, OperatorEnum.GreaterOrEqual);
            _flexL.VerifyCompare(_flexL.Cols["RT_CUT"], 0, OperatorEnum.GreaterOrEqual);
            _flexL.VerifyCompare(_flexL.Cols["AM_EST"], 0, OperatorEnum.GreaterOrEqual);
            _flexL.VerifyCompare(_flexL.Cols["AM_KEST"], 0, OperatorEnum.GreaterOrEqual);
            _flexL.VerifyCompare(_flexL.Cols["AM_VAT"], 0, OperatorEnum.GreaterOrEqual);
            _flexL.VerifyCompare(_flexL.Cols["AM_STD_PO"], 0, OperatorEnum.GreaterOrEqual);

            //그리드에 Bp컨트롤 과 같이 도움창 작업을 위해 셋팅
            _flexL.SetCodeHelpCol("CD_ITEM", Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB1, ShowHelpEnum.Always, new string[] { "CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT" }, Duzon.Common.Forms.Help.ResultMode.SlowMode);

            _flexL.AfterCodeHelp += new AfterCodeHelpEventHandler(_FlexL_AfterCodeHelp);
            _flexL.BeforeCodeHelp += new BeforeCodeHelpEventHandler(_FlexL_BeforeCodeHelp);
            _flexL.DoubleClick += new EventHandler(_flex_DoubleClick);
            _flexL.ValidateEdit += new ValidateEditEventHandler(_FlexL_ValidateEdit);
            _flexL.StartEdit += new RowColEventHandler(_flex_StartEdit);

            #endregion LINE SETTING

            #region DETAIL SETTING
            //그리드 컬럼 Setting
            _flexD.BeginSetting(1, 1, false);
            _flexD.SetCol("CD_COMPANY", "회사", false);
            _flexD.SetCol("NO_EST", "견적번호", false);
            _flexD.SetCol("NO_HST", "차수", false);
            _flexD.SetCol("NO_LINE", "항번", false);
            _flexD.SetCol("SEQ", "항번", false);
            _flexD.SetCol("TEMPCHECK", "임시항번", false);

            _flexD.SetCol("CD_PLANT", "공장", false);
            _flexD.SetCol("CD_ITEM", "품목코드", 90, true);
            _flexD.SetCol("NM_ITEM", "품목명", 120, true);
            _flexD.SetCol("GRP_ITEM", "품목군", false);
            _flexD.SetCol("NM_ITEMGRP", "품목군명", false);
            _flexD.SetCol("STND_ITEM", "규격", 90, false);
            _flexD.SetCol("UNIT", "단위", 90, false);
            _flexD.SetCol("QT", "수량", 80, true, typeof(decimal), FormatTpType.QUANTITY);
            _flexD.SetCol("UM", "단가", 90, true, typeof(decimal), FormatTpType.MONEY);
            _flexD.SetCol("DC_RMK", "비고", 100, true);

            //그리드 마지막 컬럼 체움
            _flexD.ExtendLastCol = true;

            //그리드 색상, Sort, Summary 추가여부
            _flexD.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            //헤더에 필요없는 SUM 제거 && 반듯이 EndSetting 다음에 코딩해줘야한다.
            _flexD.SetExceptSumCol("CD_COMPANY", "NO_EST", "NO_HST", "NO_LINE", "SEQ", "CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT", "DC_RMK");

            //그리드 필수 항목 Setting
            _flexD.VerifyNotNull = new string[] { "CD_COMPANY", "NM_ITEM" };

            //그리드 특정 컬럼 값 셋팅 후 비교
            _flexD.VerifyCompare(_flexD.Cols["QT"], 0, OperatorEnum.GreaterOrEqual);
            _flexD.VerifyCompare(_flexD.Cols["UM"], 0, OperatorEnum.GreaterOrEqual);

            //그리드에 Bp컨트롤 과 같이 도움창 작업을 위해 셋팅
            _flexD.SetCodeHelpCol("CD_ITEM", Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB1, ShowHelpEnum.Always, new string[] { "CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT" }, Duzon.Common.Forms.Help.ResultMode.SlowMode);

            _flexD.AfterCodeHelp += new AfterCodeHelpEventHandler(_FlexL_AfterCodeHelp);
            _flexD.BeforeCodeHelp += new BeforeCodeHelpEventHandler(_FlexL_BeforeCodeHelp);
            _flexD.DoubleClick += new EventHandler(_flex_DoubleClick);
            _flexD.StartEdit += new RowColEventHandler(_flex_StartEdit);

            #endregion LINE SETTING

            #region LineTemp SETTING
            //그리드 컬럼 Setting
            _flexLT.BeginSetting(1, 1, false);
            _flexLT.SetCol("CD_COMPANY", "회사", false);
            _flexLT.SetCol("NO_EST", "견적번호", false);
            _flexLT.SetCol("NO_HST", "차수", false);
            _flexLT.SetCol("CD_PLANT", "공장", false);
            _flexLT.SetCol("TEMPCHECK", "임시항번", false);

            _flexLT.SetCol("NO_LINE", "항번", 50, false);
            _flexLT.SetCol("CD_ITEM", "품목코드", 90, false);
            _flexLT.SetCol("NM_ITEM", "품목명", 120, false);

            _flexLT.ExtendLastCol = true;

            //그리드 색상, Sort, Summary 추가여부
            _flexLT.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            _flexLT.AfterRowChange += new RangeEventHandler(_FlexLT_AfterRowChange);

            #endregion LINE SETTING
        }
        #endregion

        #region InitPaint
        //PageBase 의 부모함수 이용 : 폼이 올라올 후 폼위에 올라온 컨트롤들을 초기화 시켜주는 역할을 한다.
        //Combo Box 셋팅시 옵션 => N:공백없음, S:공백추가, SC:공백&괄호추가, NC:괄호추가
        //뒤의 코드값들은 DB 에서 정해놓은 파라미터와 매치시켜줘야한다
        protected override void InitPaint()
        {
            //(Vat구분), 환종, 공장, 결재조건
            DataSet ds_FreeControl = GetComboData("N;MA_CODEDTL_005;MA_B000040", "N;MA_B000005", "S;MA_PLANT", "S;SA_B000002");

            #region 프리폼 바인딩

            // VAT구분
            m_cboTpVat.DataSource = ds_FreeControl.Tables[0];
            m_cboTpVat.DisplayMember = "NAME";
            m_cboTpVat.ValueMember = "CODE";

            #endregion

            //프리폼 & 그리드 스키마 셋팅
            object[] obj = new object[7];
            obj[0] = string.Empty;    // 회사코드
            obj[1] = string.Empty;    // 견적번호
            obj[2] = null;            // 차수
            obj[3] = string.Empty;    // 영업그룹
            obj[4] = string.Empty;    // 담당자
            obj[5] = string.Empty;    // 거래처
            obj[6] = string.Empty;    // VAT 구분

            DataSet dsTemp = _biz.Search(obj);

            //항번에 따른 반인딩을 위함

            _flexH.Binding = dsTemp.Tables[0];
            _flexL.Binding = dsTemp.Tables[1];
            _flexLT.Binding = dsTemp.Tables[1].Copy();
            _flexD.Binding = dsTemp.Tables[2];

            //그리드에 콤보박스 셋팅
            _flexH.SetDataMap("CD_EXCH", ds_FreeControl.Tables[1], "CODE", "NAME");
            _flexL.SetDataMap("CD_PLANT", ds_FreeControl.Tables[2], "CODE", "NAME");
            _flexH.SetDataMap("FG_BILL", ds_FreeControl.Tables[3], "CODE", "NAME");

            bp_Emp.CodeValue = Global.MainFrame.LoginInfo.EmployeeNo;
            bp_Emp.CodeName = Global.MainFrame.LoginInfo.EmployeeName;

            //버튼 기본 셋팅
            btnH_Add.Enabled = true;
            btnH_Del.Enabled = false;

            btnL_Add.Enabled = false;
            btnL_Del.Enabled = false;

        }
        #endregion

        #region 필수입력 체크
        /// <summary>
        /// 필수입력 항목에 Null 체크해주는 함수
        /// 아래의 NUllCheck() 메소드가 리턴값을 Bool 형태로 반환합니다.
        /// </summary>
        /// <returns>bool</returns>
        private bool Check(string Flag)
        {
            Hashtable hList = new Hashtable();

            if (Flag.ToUpper() == "SEARCH")
            {
                hList.Add(txt_EstimateNo.Text, lbl_EstimateNo);
            }
            else if (Flag.ToUpper() == "ADD")
            {
                hList.Add(bp_SaleGroup, lbl_SaleGroup);
                hList.Add(bp_Emp, lbl_Emp);
                hList.Add(bp_Partner, lbl_Customer);
                hList.Add(m_cboTpVat, lbl_Vat);
            }

            return ComFunc.NullCheck(hList);
        }
        #endregion 필수입력 체크

        #region 그리드 라인 최대값 구하는 함수(MaxRowCount(string GridGubun))
        /// /// <summary>
        /// 그리드가 라인일 경우 - 최대 로우 NO_LINE 넘버 + 1
        /// 그리드가 디테일 경우 - 최대 Seq + 1 
        /// </summary>
        /// <param name="GridGubun">그리드 명</param>
        /// <returns></returns>
        private decimal MaxRowCount(string GridGubun)
        {

            decimal dMaxRow = 0;

            switch (GridGubun.ToUpper())
            {
                case "_FLEXH":
                    #region 라인 항번
                    for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
                    {
                        if (_flexH["NO_EST"].ToString() == _flexH[i, "NO_EST"].ToString())
                        {
                            if (D.GetDecimal(_flexH[i, "NO_HST"]) > dMaxRow)
                            {
                                dMaxRow = D.GetDecimal(_flexH[i, "NO_HST"]);
                            }
                        }
                    }
                    #endregion 라인 항번
                    break;
                case "_FLEXL":
                    #region 라인 항번
                    for (int i = _flexL.Rows.Fixed; i < _flexL.Rows.Count; i++)
                    {
                        if (D.GetDecimal(_flexL[i, "NO_LINE"]) > dMaxRow)
                        {
                            dMaxRow = D.GetDecimal(_flexL[i, "NO_LINE"]);
                        }
                    }
                    #endregion 라인 항번
                    break;
                case "_FLEXD":
                    #region 디테일 차수
                    for (int i = _flexD.Rows.Fixed; i < _flexD.Rows.Count; i++)
                    {
                        if (D.GetDecimal(_flexD[i, "SEQ"]) > dMaxRow)
                        {
                            dMaxRow = D.GetDecimal(_flexD[i, "SEQ"]);
                        }
                    }
                    #endregion 라인 항번
                    break;
            }

            return dMaxRow + 1;
        }
        #endregion MaxRowCount(string GridGubun) 그리드 라인 최대값 구하는 함수

        #region BtnCheck(헤더와 라인에 따라 추가 삭제 버튼 활성화 및 수정 여부 체크)
        /// <summary>
        /// 헤더 부분 ENABLE 설정 및 라인에 따른 추가 삭제 버튼 활성화 체크
        /// 할인율 적용 버튼 활성화 체크
        /// </summary>
        private void BtnCheck()
        {

            int iHCount = _flexH.Rows.Count - _flexH.Rows.Fixed;

            //헤더추가
            if (iHCount == 0)
            {
                btnH_Add.Enabled = true;
                btnH_Del.Enabled = false;

                btnL_Add.Enabled = true;
                btnL_Del.Enabled = false;

                txt_EstimateNo.Enabled = true;
            }
            else
            {
                //헤더의 차수 값을 찾아서 조회된 내용에서 맥스 차수 인지 검색
                DataTable dt = null;
                int iH_FilterCount = 0;

                dt = _flexH.DataTable;

                if (dt == null || dt.Rows.Count == 0)
                {

                    return;
                }

                DataRow[] dr = dt.Select("NO_EST = '" + txt_EstimateNo.Text + "'");

                iH_FilterCount = dr.Length;

                //해당 견적의 마지막 차수인 경우
                if (iH_FilterCount == D.GetInt(_flexH["NO_HST"]))
                {
                    btnH_Del.Enabled = true;
                    btn_Rt_Cut.Enabled = true;
                    currency_Rt_Cut.Enabled = true;

                    //헤더가 움직였는지 여부 체크
                    if (HeaderAddCheck == false)
                    {
                        btnH_Add.Enabled = false;
                    }
                    else
                    {
                        btnH_Add.Enabled = true;
                    }


                    if (tabControl.SelectedTab.Name.ToUpper() == "ITEM")
                    {
                        if ((_flexLT.Rows.Count - _flexLT.Rows.Fixed) > 0)
                        {
                            btnL_Add.Enabled = true;
                            btnL_Del.Enabled = true;
                        }
                        else
                        {
                            btnL_Add.Enabled = true;
                            btnL_Del.Enabled = false;
                        }
                    }
                    else
                    {
                        if ((_flexLT.Rows.Count - _flexLT.Rows.Fixed) > 0)
                        {
                            btnL_Add.Enabled = true;

                            if (_flexD.Rows.Count - _flexD.Rows.Fixed > 0)
                            {
                                btnL_Del.Enabled = true;
                            }
                            else
                            {
                                btnL_Del.Enabled = false;
                            }
                        }
                        else
                        {
                            btnL_Add.Enabled = false;
                            btnL_Del.Enabled = false;
                        }
                    }

                    _flexL.AllowEditing = true;
                    _flexH.AllowEditing = true;
                    _flexD.AllowEditing = true;

                    _flexH.Cols["CD_EXCH"].AllowEditing = true;
                    _flexH.Cols["FG_BILL"].AllowEditing = true;
                    _flexL.Cols["CD_PLANT"].AllowEditing = true;


                }
                else
                {
                    btnH_Add.Enabled = true;
                    btnH_Del.Enabled = false;
                    btnL_Add.Enabled = false;
                    btnL_Del.Enabled = false;

                    _flexL.AllowEditing = false;
                    _flexD.AllowEditing = false;
                    _flexH.AllowEditing = false;

                    //이렇게 안해주면 DropDown형태의 방식은 Space를 누르면 수정모드가 됨
                    //이렇게 해주면 수정모드가 안돼므로 row를 수정불가로 걸어도 dropdown은 이런형태로 처리해야함
                    _flexH.Cols["CD_EXCH"].AllowEditing = false;
                    _flexH.Cols["FG_BILL"].AllowEditing = false;
                    _flexL.Cols["CD_PLANT"].AllowEditing = false;

                    _flexH.FinishEditing(true);

                    btn_Rt_Cut.Enabled = false;
                    currency_Rt_Cut.Enabled = false;
                }
            }
        }
        #endregion BtnCheck(헤더와 라인에 따라 추가 삭제 버튼 활성화 및 수정 여부 체크)()

        #region 상단 메뉴 Event

        #region 상단메뉴 조회버튼 Event
        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {

                if (!Check("SEARCH"))
                    return;

                //조회 조건 파라메타
                object[] obj = new object[8];
                obj[0] = Global.MainFrame.LoginInfo.CompanyCode;    // 회사코드
                obj[1] = txt_EstimateNo.Text;                       // 견적번호
                obj[2] = 1;                                         // 차수
                obj[3] = bp_SaleGroup.CodeValue;                    // 영업그룹
                obj[4] = bp_Emp.CodeValue;                          // 담당자
                obj[5] = bp_Partner.CodeValue;                     // 거래처
                obj[6] = m_cboTpVat.SelectedValue == null ? string.Empty : m_cboTpVat.SelectedValue.ToString();// VAT 구분
                obj[7] = null;

                DataSet ds = _biz.Search(obj);

                //프리폼 및 그리드 바인딩
                _flexH.Binding = ds.Tables[0];
                _flexL.Binding = ds.Tables[1];
                _flexLT.Binding = ds.Tables[1].Copy();
                //_FlexD.Binding = ds.Tables[2];

                HeaderAddCheck = true;

                BtnCheck();

                CopyTable = null;

                _FlexH_AfterRowChange(null, null);

                if (!_flexH.HasNormalRow)
                {
                    ShowMessage(PageResultMode.SearchNoData);// 검색된 내용이 존재하지 않습니다..
                    return;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 상단메뉴 추가버튼 Event
        /// <summary>
        /// 신규 견적을 생성시 사용
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            txt_EstimateNo.Text = string.Empty;

            bp_SaleGroup.CodeValue = string.Empty;
            bp_SaleGroup.CodeName = string.Empty;

            bp_Emp.CodeValue = string.Empty;
            bp_Emp.CodeName = string.Empty;

            bp_Partner.CodeValue = string.Empty;
            bp_Partner.CodeName = string.Empty;

            m_cboTpVat.SelectedIndex = 0;

            InitPaint();

            tabControl.SelectedIndex = 0;

            HeaderAddCheck = true;

            BtnCheck();

            CopyTable = null;

        }
        #endregion

        #region 상단메뉴 삭제버튼 Event
        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (ShowMessageBox(1003, "해당견적번호를 모두 삭제 하시겠습니까? ") == DialogResult.Yes)
                {
                    if (_flexH.HasNormalRow)
                    {
                        //헤더에 필터를 걸어 모두 삭제 로직 추가
                        int iRowcount = _flexH.Rows.Count;
                        string sEstimateNo = _flexH["NO_EST"].ToString();

                        for (int i = 0; i < _flexH.DataTable.Rows.Count; i++)
                        {
                            if (sEstimateNo == _flexH.DataTable.Rows[i]["NO_EST"].ToString())
                            {
                                _flexH.DataTable.Rows[i].Delete();
                            }
                        }

                        if (SaveData())
                        {
                            tabControl.SelectedIndex = 0;
                            OnToolBarSearchButtonClicked(null, null);
                            ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
                        }
                        else
                        {
                            tabControl.SelectedIndex = 0;
                            OnToolBarSearchButtonClicked(null, null);
                            ShowMessage(공통메세지.자료저장중오류가발생하였습니다);
                        }
                    }
                    else
                    {
                        if (SaveData())
                        {
                            tabControl.SelectedIndex = 0;
                            OnToolBarSearchButtonClicked(null, null);
                            ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
                        }
                        else
                        {
                            tabControl.SelectedIndex = 0;
                            OnToolBarSearchButtonClicked(null, null);
                            ShowMessage(공통메세지.자료저장중오류가발생하였습니다);
                        }
                    }

                    HeaderAddCheck = true;

                    CopyTable = null;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 상단메뉴 저장버튼 Event

        #region -> SaveData

        protected override bool SaveData()
        {

            if (!base.SaveData()) return false;

            if (!this.Verify()) return false;

            DataTable dtH = new DataTable();
            DataTable dtL = new DataTable();
            DataTable dtD = new DataTable();

            #region 헤더 체번 및 항번 체번
            DataRow[] dr = _flexH.DataTable.Select("NO_EST = ''");
            decimal dSeq = 0;


            //신규 추가시 견적번호와 라인번호 차수를 재 셋팅함
            if (dr.Length > 0 && HeaderCheck == true)
            {
                string SNO_REG = (string)this.GetSeq(LoginInfo.CompanyCode, "SA", "01", Global.MainFrame.GetStringYearMonth);//채번따옴

                for (int i = 0; i < _flexH.DataTable.Rows.Count; i++)
                {
                    if (_flexH.DataTable.Rows[i]["NO_EST"] == null || _flexH.DataTable.Rows[i]["NO_EST"].ToString() == "")
                    {
                        _flexH.DataTable.Rows[i]["NO_EST"] = SNO_REG;

                    }
                }


                for (int i = 0; i < _flexL.DataTable.Rows.Count; i++)
                {
                    if (_flexL.DataTable.Rows[i]["NO_EST"] == null || _flexL.DataTable.Rows[i]["NO_EST"].ToString() == "")
                    {
                        _flexL.DataTable.Rows[i]["NO_EST"] = SNO_REG;
                        _flexL.DataTable.Rows[i]["NO_LINE"] = i + 1;
                    }

                    for (int j = 0; j < _flexD.DataTable.Rows.Count; j++)
                    {
                        if (D.GetDecimal(_flexL.DataTable.Rows[i]["TEMPCHECK"]) == D.GetDecimal(_flexD.DataTable.Rows[j]["TEMPCHECK"]))
                        {
                            dSeq = dSeq + 1;

                            _flexD.DataTable.Rows[j]["NO_EST"] = SNO_REG;
                            _flexD.DataTable.Rows[j]["NO_LINE"] = _flexL.DataTable.Rows[i]["NO_LINE"];
                            _flexD.DataTable.Rows[j]["SEQ"] = dSeq;
                        }
                    }

                    dSeq = 0;
                }
            }
            else if (HeaderCheck == true)//복사 추가 후
            {
                for (int i = 0; i < _flexL.DataTable.Rows.Count; i++)
                {
                    _flexL.DataTable.Rows[i]["NO_LINE"] = i + 1;

                    for (int j = 0; j < _flexD.DataTable.Rows.Count; j++)
                    {
                        if (D.GetDecimal(_flexL.DataTable.Rows[i]["TEMPCHECK"]) == D.GetDecimal(_flexD.DataTable.Rows[j]["TEMPCHECK"]))
                        {
                            dSeq = dSeq + 1;

                            _flexD.DataTable.Rows[j]["NO_LINE"] = _flexL.DataTable.Rows[i]["NO_LINE"];
                            _flexD.DataTable.Rows[j]["SEQ"] = dSeq;
                        }

                    }

                    dSeq = 0;
                }
            }

            #endregion 헤더 체번 및 항번 체번

            dtH = _flexH.GetChanges();
            dtL = _flexL.GetChanges();
            dtD = _flexD.GetChanges();

            bool bSuccess = _biz.Save(dtH, dtL, dtD);

            if (!bSuccess)
                return false;

            _flexH.AcceptChanges();
            _flexL.AcceptChanges();
            _flexD.AcceptChanges();

            //초기화
            HeaderCheck = false;

            return true;
        }

        #endregion

        #region OnToolBarSaveButtonClicked EVENT
        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                int iLRowCount = _flexL.Rows.Count - _flexL.Rows.Fixed;

                _flexH.Focus();

                if (iLRowCount == 0)
                {
                    ShowMessage("라인없이 저장할 수 없습니다");
                    return;
                }

                if (SaveData())
                {
                    OnToolBarSearchButtonClicked(null, null);

                    txt_EstimateNo.Enabled = true;

                    _flexL.Row = _flexL.Row;

                    HeaderAddCheck = true;

                    ShowMessage(공통메세지.자료가정상적으로저장되었습니다);

                    _flexH.Focus();

                    CopyTable = null;
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion OnToolBarSaveButtonClicked Event

        #endregion

        #region 상단 메뉴 인쇄버튼 클릭
        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!base.SaveData()) return;

                if (!this.Verify()) return;

                if (!_flexL.HasNormalRow)
                {
                    ShowMessage("조회된 내용이 없습니다");
                    return;
                }

                string sManuID = "";
                string sManuName = "";

                decimal SumEst = 0;
                DataTable dt = null;

                //TODO::수정해야할부분
                sManuID = "R_SA_ESTIMATE_0";
                sManuName = "견적서";

                ReportHelper rptHelper = new ReportHelper(sManuID, sManuName);



                rptHelper.가로출력();

                dt = _flexL.DataTable.Copy();

                rptHelper.SetData("견적번호", _flexL["NO_EST"].ToString());
                rptHelper.SetData("견적차수", _flexL["NO_HST"].ToString());
                rptHelper.SetData("견적명", _flexL["NO_EST_NM"].ToString());
                rptHelper.SetData("견적일자", _flexL["DT_EST"].ToString());
                rptHelper.SetData("계약예정일", _flexL["DT_CONT"].ToString());
                rptHelper.SetData("결제조건", _flexL["FG_BILLNAME"].ToString());
                rptHelper.SetData("영업그룹", _flexL["NM_SALEGRP"].ToString());
                rptHelper.SetData("거래처", _flexL["CD_PARTNER"].ToString());
                rptHelper.SetData("거래처명", _flexL["LN_PARTNER"].ToString());
                rptHelper.SetData("VAT구분", _flexL["TP_VAT_NAME"].ToString());
                rptHelper.SetData("사원", _flexL["NO_EMP"].ToString());
                rptHelper.SetData("사원명", _flexL["NM_KOR"].ToString());
                rptHelper.SetData("거래처담당자", _flexH["NM_PTR"].ToString());
                rptHelper.SetData("메일", _flexH["NO_EMAIL"].ToString());
                rptHelper.SetData("전화번호", _flexH["NO_TEL"].ToString());
                rptHelper.SetData("비상연락", _flexH["NO_TEL_EMER"].ToString());
                
                rptHelper.SetData("FAX", _flexH["NO_FAX"].ToString());
                rptHelper.SetData("제품군", _flexL["GRP_MFGNM"].ToString());
                rptHelper.SetData("비고", _flexL["DC_RMK"].ToString());
                rptHelper.SetData("구매단가", _flexL["PU_UM"].ToString());
                

                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    SumEst = SumEst + D.GetDecimal(dt.Rows[i]["AM_ESTVAT"]);
                }


                if (_flexH["FG_VAT"].ToString().ToUpper() == "Y")
                {
                    rptHelper.SetData("부가세포함정보", "VAT포함");
                    rptHelper.SetData("총견적금액", SumEst.ToString());
                }
                else
                {
                    rptHelper.SetData("부가세포함정보", "VAT별도");
                    rptHelper.SetData("총견적금액", SumEst.ToString());
                }


                rptHelper.SetDataTable(dt);

                rptHelper.Print();

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #endregion 상단 메뉴 Event

        #region 그리드 Event

        #region -> _FlexLT_AfterRowChange
        private void _FlexLT_AfterRowChange(object sender, RangeEventArgs e)
        {

            if (!_flexLT.HasNormalRow || !_flexH.HasNormalRow)
                return;

            DataSet ds = null;
            DataTable dt = null;

            string Filter = string.Empty;
            int iRowIndex = _flexLT.Rows.Count - _flexLT.Rows.Fixed;

            if (D.GetDecimal(_flexLT[_flexLT.Row, "TEMPCHECK"]) != 0)
            {
                Filter = "NO_EST = '" + _flexH[_flexH.Row, "NO_EST"].ToString() + "'" + " AND " + "NO_HST = " + _flexH[_flexH.Row, "NO_HST"].ToString() + " AND " + "TEMPCHECK = " + _flexLT[_flexLT.Row, "TEMPCHECK"];
            }


            if (!Check("SEARCH"))
                return;

            if (_flexLT.DetailQueryNeed)
            {
                //조회 조건 파라메타
                object[] obj = new object[8];
                obj[0] = _flexH["CD_COMPANY"];// 회사코드
                obj[1] = _flexH["NO_EST"];// 견적번호
                obj[2] = _flexH["NO_HST"];// 차수
                obj[3] = _flexH["CD_SALEGRP"];// 영업그룹
                obj[4] = _flexH["NO_EMP"];// 담당자
                obj[5] = _flexH["CD_PARTNER"];// 거래처
                obj[6] = _flexH["TP_VAT"];// VAT 구분
                obj[7] = _flexLT["NO_LINE"];

                ds = _biz.Search(obj);

                _flexD.BindingAdd(ds.Tables[2], Filter);
            }
            else
            {
                _flexD.BindingAdd(dt, Filter);
            }

            BtnCheck();
        }
        #endregion -> _FlexLT_AfterRowChange

        #region -> _FlexL_ValidateEdit
        private void _FlexL_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                switch (_flexL.Cols[e.Col].Name)
                {
                    //case "AM_STD":// 견적기준가(견적금액 = 견적 기준가 * (할인율 * 100))
                    case "RT_CUT":

                        #region 할인율

                        //견적금액
                        if (_flex.CDecimal(_flexL[e.Row, "RT_CUT"]) != 0)
                        {
                            //견적금액 = (단가 * 수량) - ((단가 * 수량)/ 할인율)
                            _flexL[e.Row, "AM_EST"] = (_flex.CDecimal(_flexL[e.Row, "UM"]) * _flex.CDecimal(_flexL[e.Row, "QT"])) - (_flex.CDecimal(_flexL[e.Row, "UM"]) * _flex.CDecimal(_flexL[e.Row, "QT"])) * (_flex.CDecimal(_flexL[e.Row, "RT_CUT"]) / 100);
                        }
                        else
                        {
                            //견적금액 = 단가 * 수량
                            _flexL[e.Row, "AM_EST"] = _flex.CDecimal(_flexL[e.Row, "UM"]) * _flex.CDecimal(_flexL[e.Row, "QT"]);
                        }

                        //견적기준가
                        if (_flex.CDecimal(_flexL[e.Row, "UM"]) != 0 && _flex.CDecimal(_flexL[e.Row, "QT"]) != 0)
                        {
                            //견적기준가 = 단가 * 수량
                            _flexL[e.Row, "AM_STD"] = _flex.CDecimal(_flexL[e.Row, "UM"]) * _flex.CDecimal(_flexL[e.Row, "QT"]);
                        }
                        else
                        {
                            _flexL[e.Row, "AM_STD"] = 0;
                        }


                        //견적원화금액 재계산
                        _flexL[e.Row, "AM_KEST"] = _flex.CDecimal(_flexL[e.Row, "AM_EST"]) * _flex.CDecimal(_flexH[_flexH.Row, "RT_EXCH"]);

                        //부가세
                        _flexL[e.Row, "AM_VAT"] = _flex.CDecimal(_flexL[e.Row, "AM_KEST"]) * (m_curTpVat.DecimalValue / 100);

                        //예상마진율
                        //예상마진율 = (( 견적원화금액 - 견적기준가(구매)) / 견적원화금액) * 100
                        if (_flex.CDecimal(_flexL[e.Row, "AM_KEST"]) - _flex.CDecimal(_flexL[e.Row, "AM_STD_PO"]) != 0 && _flex.CDecimal(_flexL[e.Row, "AM_KEST"]) != 0 && _flex.CDecimal(_flexL[e.Row, "AM_STD_PO"]) != 0)
                        {
                            _flexL[e.Row, "RATE"] = Math.Round(((_flex.CDecimal(_flexL[e.Row, "AM_KEST"]) - _flex.CDecimal(_flexL[e.Row, "AM_STD_PO"])) / _flex.CDecimal(_flexL[e.Row, "AM_KEST"])) * 100, 2, MidpointRounding.AwayFromZero);
                        }
                        else
                        {
                            _flexL[e.Row, "RATE"] = 0;
                        }

                        #endregion AM_STD, RT_CUT

                        break;

                    case "AM_KEST"://견적원화금액 (견적금액 * 헤더의 환율)

                        #region 견적원화금액

                        _flexL[e.Row, "AM_EST"] = _flex.CDecimal(_flexL[e.Row, "AM_KEST"]) / _flex.CDecimal(_flexH[_flexH.Row, "RT_EXCH"]);


                        //단가
                        if (_flex.CDecimal(_flexL[e.Row, "AM_EST"]) != 0 && _flex.CDecimal(_flexL[e.Row, "QT"]) != 0)
                        {
                            //단가 = 견적금액 / 수량
                            _flexL[e.Row, "UM"] = _flex.CDecimal(_flexL[e.Row, "AM_EST"]) / _flex.CDecimal(_flexL[e.Row, "QT"]);
                        }
                        else
                        {
                            _flexL[e.Row, "UM"] = 0;
                        }


                        //견적기준가
                        if (_flex.CDecimal(_flexL[e.Row, "UM"]) != 0 && _flex.CDecimal(_flexL[e.Row, "QT"]) != 0)
                        {
                            //견적기준가 = 단가 * 수량
                            _flexL[e.Row, "AM_STD"] = _flex.CDecimal(_flexL[e.Row, "UM"]) * _flex.CDecimal(_flexL[e.Row, "QT"]);
                        }
                        else
                        {
                            _flexL[e.Row, "AM_STD"] = 0;
                        }


                        _flexL[e.Row, "AM_VAT"] = _flex.CDecimal(_flexL[e.Row, "AM_KEST"]) * (m_curTpVat.DecimalValue / 100);

                       
                        //예상마진율
                        //예상마진율 = ( 견적원화금액 - (견적기준가(구매)) / 견적원화금액) * 100
                        if (_flex.CDecimal(_flexL[e.Row, "AM_KEST"]) - _flex.CDecimal(_flexL[e.Row, "AM_STD_PO"]) != 0 && _flex.CDecimal(_flexL[e.Row, "AM_KEST"]) != 0 && _flex.CDecimal(_flexL[e.Row, "AM_STD_PO"]) != 0)
                        {
                            _flexL[e.Row, "RATE"] = Math.Round(((_flex.CDecimal(_flexL[e.Row, "AM_KEST"]) - _flex.CDecimal(_flexL[e.Row, "AM_STD_PO"])) / _flex.CDecimal(_flexL[e.Row, "AM_KEST"])) * 100, 2, MidpointRounding.AwayFromZero);
                        }
                        else
                        {
                            _flexL[e.Row, "RATE"] = 0;
                        }


                        #endregion 견적원화금액

                        break;

                    case "QT":// 견적기준가 = 수량 * 단가
                    case "UM":

                        #region QT, UM

                        //견적금액
                        if (_flex.CDecimal(_flexL[e.Row, "RT_CUT"]) != 0)
                        {
                            //견적금액 = (단가 * 수량) - ((단가 * 수량)/ 할인율)
                            _flexL[e.Row, "AM_EST"] = (_flex.CDecimal(_flexL[e.Row, "UM"]) * _flex.CDecimal(_flexL[e.Row, "QT"])) - (_flex.CDecimal(_flexL[e.Row, "UM"]) * _flex.CDecimal(_flexL[e.Row, "QT"])) * (_flex.CDecimal(_flexL[e.Row, "RT_CUT"]) / 100);
                        }
                        else
                        {
                            //견적금액 = 단가 * 수량
                            _flexL[e.Row, "AM_EST"] = _flex.CDecimal(_flexL[e.Row, "UM"]) * _flex.CDecimal(_flexL[e.Row, "QT"]);
                        } 



                        //견적원화금액 재계산
                        _flexL[e.Row, "AM_KEST"] = _flex.CDecimal(_flexL[e.Row, "AM_EST"]) * _flex.CDecimal(_flexH[_flexH.Row, "RT_EXCH"]);

                        //부가세
                        _flexL[e.Row, "AM_VAT"] = _flex.CDecimal(_flexL[e.Row, "AM_KEST"]) * (m_curTpVat.DecimalValue / 100);

                        //예상마진율
                        //예상마진율 = ((견적원화금액 -견적기준가(구매)) / 견적원화금액) * 100
                        if (_flex.CDecimal(_flexL[e.Row, "AM_KEST"]) - _flex.CDecimal(_flexL[e.Row, "AM_STD_PO"]) != 0 && _flex.CDecimal(_flexL[e.Row, "AM_KEST"]) != 0 && _flex.CDecimal(_flexL[e.Row, "AM_STD_PO"]) != 0)
                        {
                            _flexL[e.Row, "RATE"] = Math.Round(((_flex.CDecimal(_flexL[e.Row, "AM_KEST"]) - _flex.CDecimal(_flexL[e.Row, "AM_STD_PO"])) / _flex.CDecimal(_flexL[e.Row, "AM_KEST"])) * 100, 2, MidpointRounding.AwayFromZero);
                        }
                        else
                        {
                            _flexL[e.Row, "RATE"] = 0;
                        }

                        #endregion QT, UM

                        break;


                    case "AM_EST":// 견적금액 = 단가 * 수량

                        #region AM_EST

                        if (_flex.CDecimal(_flexL[e.Row, "QT"]) != 0)
                        {
                            if (_flex.CDecimal(_flexL[e.Row, "RT_CUT"]) != 0)
                            {
                                //견적금액
                                _flexL[e.Row, "UM"] = (_flex.CDecimal(_flexL[e.Row, "AM_EST"]) + (_flex.CDecimal(_flexL[e.Row, "AM_EST"]) * (_flex.CDecimal(_flexL[e.Row, "RT_CUT"]) / 100))) / _flex.CDecimal(_flexL[e.Row, "QT"]);
                            }
                            else
                            {
                                //견적금액
                                _flexL[e.Row, "UM"] = (_flex.CDecimal(_flexL[e.Row, "AM_EST"]) / _flex.CDecimal(_flexL[e.Row, "QT"]));
                            }
                        }
                        else
                        {
                            _flexL[e.Row, "UM"] = 0;
                        }


                        //견적원화금액 = 견적금액 * 환율
                        _flexL[e.Row, "AM_KEST"] = _flex.CDecimal(_flexL[e.Row, "AM_EST"]) * _flex.CDecimal(_flexH[_flexH.Row, "RT_EXCH"]);

                        //부가세
                        _flexL[e.Row, "AM_VAT"] = _flex.CDecimal(_flexL[e.Row, "AM_KEST"]) * (m_curTpVat.DecimalValue / 100);

                        //예상마진율
                        //예상마진율 = (견적원화금액 - (견적기준가(구매)) / 견적원화금액) * 100
                        if (_flex.CDecimal(_flexL[e.Row, "AM_KEST"]) - _flex.CDecimal(_flexL[e.Row, "AM_STD_PO"]) != 0 && _flex.CDecimal(_flexL[e.Row, "AM_KEST"]) != 0 && _flex.CDecimal(_flexL[e.Row, "AM_STD_PO"]) != 0)
                        {
                            _flexL[e.Row, "RATE"] = Math.Round(((_flex.CDecimal(_flexL[e.Row, "AM_KEST"]) - _flex.CDecimal(_flexL[e.Row, "AM_STD_PO"])) / _flex.CDecimal(_flexL[e.Row, "AM_KEST"])) * 100, 2, MidpointRounding.AwayFromZero);
                        }
                        else
                        {
                            _flexL[e.Row, "RATE"] = 0;
                        }

                        #endregion AM_EST

                        break;

                    case "AM_STD_PO": //견적기준가

                        #region AM_STD_PO

                        //예상마진율 = ((견적원화금액 - 견적기준가(구매)) / 견적원화금액) * 100
                        if (_flex.CDecimal(_flexL[e.Row, "AM_KEST"]) - _flex.CDecimal(_flexL[e.Row, "AM_STD_PO"]) != 0 && _flex.CDecimal(_flexL[e.Row, "AM_KEST"]) != 0 && _flex.CDecimal(_flexL[e.Row, "AM_STD_PO"]) != 0)
                        {
                            _flexL[e.Row, "RATE"] = Math.Round(((_flex.CDecimal(_flexL[e.Row, "AM_KEST"]) - _flex.CDecimal(_flexL[e.Row, "AM_STD_PO"])) / _flex.CDecimal(_flexL[e.Row, "AM_KEST"])) * 100, 2, MidpointRounding.AwayFromZero);
                        }
                        else
                        {
                            _flexL[e.Row, "RATE"] = 0;
                        }


                        #endregion AM_STD_PO
                        break;

                    case "CD_PLANT":

                        #region CD_PLANT

                        _flexLT[e.Row - 1, "CD_PLANT"] = _flexL[e.Row, "CD_PLANT"];

                        #endregion CD_PLANT

                        break;

                    case "CD_ITEM":

                        #region CD_ITEM

                        _flexLT[e.Row - 1, "CD_ITEM"] = _flexL[e.Row, "CD_ITEM"];

                        #endregion CD_ITEM

                        break;

                    case "NM_ITEM":

                        #region NM_ITEM

                        _flexLT[e.Row - 1, "NM_ITEM"] = _flexL[e.Row, "NM_ITEM"];

                        #endregion NM_ITEM

                        break;


                }

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion -> _FlexL_ValidateEdit

        #region -> _FlexL_BeforeCodeHelp
        private void _FlexL_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                if (tabControl.SelectedTab.Name.ToUpper() == "ITEM")
                {
                    switch (_flexL.Cols[e.Col].Name)
                    {
                        #region 품목
                        case "CD_ITEM":
                            if (_flexL["CD_PLANT"].ToString() == "")
                            {
                                e.Cancel = true;

                                _flexL["CD_ITEM"] = "";

                                ShowMessage("공장을 선택하세요");

                                return;
                            }
                            else
                            {
                                e.Parameter.P09_CD_PLANT = _flexL["CD_PLANT"].ToString();
                            }

                            break;
                        #endregion 품목
                    }
                }
                else
                {
                    switch (_flexD.Cols[e.Col].Name)
                    {
                        #region 품목
                        case "CD_ITEM":
                            if (_flexLT["CD_PLANT"].ToString() == "")
                            {
                                e.Cancel = true;

                                _flexD["CD_ITEM"] = "";

                                ShowMessage("공장을 선택하세요");

                                return;
                            }
                            else
                            {
                                e.Parameter.P09_CD_PLANT = _flexLT["CD_PLANT"].ToString();
                            }

                            break;
                        #endregion 품목
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }


        }
        #endregion -> _FlexL_BeforeCodeHelp

        #region -> _FlexL_AfterCodeHelp
        private void _FlexL_AfterCodeHelp(object sender, AfterCodeHelpEventArgs e)
        {
            try
            {
                HelpReturn helpReturn = e.Result;

                if (tabControl.SelectedTab.Name.ToUpper() == "ITEM")
                {
                    switch (_flexL.Cols[e.Col].Name)
                    {

                        case "CD_ITEM":
                            #region 품목 팝업
                            if (e.Result.DialogResult == DialogResult.Cancel)
                                return;

                            bool First = true;
                            _flexL.Redraw = false;
                            _flexL.SetDummyColumnAll();

                            //_flexLT.AfterRowChange -= new RangeEventHandler(_FlexLT_AfterRowChange);

                            foreach (DataRow row in helpReturn.Rows)
                            {
                                if (First)/* 새로 품목을 입력하는 경우 */
                                {
                                    _flexL[e.Row, "CD_ITEM"] = row["CD_ITEM"];       //품목코드
                                    _flexL[e.Row, "NM_ITEM"] = row["NM_ITEM"];       //품목명
                                    _flexL[e.Row, "STND_ITEM"] = row["STND_ITEM"];   //규격
                                    _flexL[e.Row, "UNIT"] = row["UNIT_SO"];          //재고단위
                                    _flexL[e.Row, "QT"] = 1;

                                    if (_단가적용형태 == "002" || _단가적용형태 == "003")
                                    {
                                        object[] obj = new object[7];
                                        obj[0] = Global.MainFrame.LoginInfo.CompanyCode;
                                        obj[1] = _flexL[e.Row, "CD_ITEM"].ToString();
                                        obj[2] = bp_Partner.CodeValue;
                                        obj[3] = "001"; // 단가유형셋팅 (001 은 정상가 이다. 셋팅하는데가 없으니 기본 정상가로만 가져간다.)
                                        obj[4] = _flexH[_flexH.Row, "CD_EXCH"].ToString();
                                        obj[5] = _단가적용형태;
                                        obj[6] = _flexH[_flexH.Row, "DT_EST"].ToString();

                                        _flexL[e.Row, "UM"] = Sa_ComFunc.UmSearch(obj);
                                        _flexL[e.Row, "AM_STD"] = _flexL[e.Row, "UM"];
                                    }

                                    if (_flex.CDecimal(_flexL[e.Row, "RT_CUT"]) != 0)
                                    {
                                        //견적금액 = (단가 * 수량) - ((단가 * 수량)/ 할인율)
                                        _flexL[e.Row, "AM_EST"] = (_flex.CDecimal(_flexL[e.Row, "UM"]) * _flex.CDecimal(_flexL[e.Row, "QT"])) - (_flex.CDecimal(_flexL[e.Row, "UM"]) * _flex.CDecimal(_flexL[e.Row, "QT"])) * (_flex.CDecimal(_flexL[e.Row, "RT_CUT"]) / 100);
                                    }
                                    else
                                    {
                                        //견적금액 = 단가 * 수량
                                        _flexL[e.Row, "AM_EST"] = _flex.CDecimal(_flexL[e.Row, "UM"]) * _flex.CDecimal(_flexL[e.Row, "QT"]);
                                    }


                                    _flexL[e.Row, "AM_KEST"] = _flex.CDecimal(_flexL[e.Row, "AM_EST"]) * _flex.CDecimal(_flexH[_flexH.Row, "RT_EXCH"]);
                                    _flexL[e.Row, "AM_VAT"] = _flex.CDecimal(_flexL[e.Row, "AM_KEST"]) * m_curTpVat.DecimalValue;

                                    //예상마진율
                                    //예상마진율 = ( 견적원화금액 - 견적기준가(구매)) / 견적원화금액) * 100
                                    if (_flex.CDecimal(_flexL[e.Row, "AM_KEST"]) - _flex.CDecimal(_flexL[e.Row, "AM_STD_PO"]) != 0 && _flex.CDecimal(_flexL[e.Row, "AM_KEST"]) != 0 && _flex.CDecimal(_flexL[e.Row, "AM_STD_PO"]) != 0)
                                    {
                                        _flexL[e.Row, "RATE"] = Math.Round(((_flex.CDecimal(_flexL[e.Row, "AM_KEST"]) - _flex.CDecimal(_flexL[e.Row, "AM_STD_PO"])) / _flex.CDecimal(_flexL[e.Row, "AM_KEST"])) * 100, 2, MidpointRounding.AwayFromZero) ;
                                    }
                                    else
                                    {
                                        _flexL[e.Row, "RATE"] = 0;
                                    }

                                    _flexLT[e.Row - 1, "CD_PLANT"] = _flexL[e.Row, "CD_PLANT"].ToString();     //공장
                                    _flexLT[e.Row - 1, "CD_ITEM"] = row["CD_ITEM"];       //품목코드
                                    _flexLT[e.Row - 1, "NM_ITEM"] = row["NM_ITEM"];       //품목명
                                    _flexLT[e.Row - 1, "STND_ITEM"] = row["STND_ITEM"];   //규격

                                    First = false;

                                }
                                else
                                {
                                    //행추가 코드 재사용 및 필수값이 ROW에 셋팅됨
                                    btnL_Add_Click(null, null);


                                    _flexL[_flexL.Row, "CD_PLANT"] = _flexL[_flexL.Row - 1, "CD_PLANT"].ToString();     //공장
                                    _flexL[_flexL.Row, "CD_ITEM"] = row["CD_ITEM"];       //품목코드
                                    _flexL[_flexL.Row, "NM_ITEM"] = row["NM_ITEM"];       //품목명
                                    _flexL[_flexL.Row, "STND_ITEM"] = row["STND_ITEM"];   //규격
                                    _flexL[_flexL.Row, "UNIT"] = row["UNIT_SO"];          //재고단위
                                    _flexL[_flexL.Row, "QT"] = 1;

                                    if (_단가적용형태 == "002" || _단가적용형태 == "003")
                                    {
                                        object[] obj = new object[7];
                                        obj[0] = Global.MainFrame.LoginInfo.CompanyCode;
                                        obj[1] = _flexL[_flexL.Row, "CD_ITEM"].ToString();
                                        obj[2] = bp_Partner.CodeValue;
                                        obj[3] = "001"; // 단가유형셋팅 (001 은 정상가 이다. 셋팅하는데가 없으니 기본 정상가로만 가져간다.)
                                        obj[4] = _flexH[_flexH.Row, "CD_EXCH"].ToString();
                                        obj[5] = _단가적용형태;
                                        obj[6] = _flexH[_flexH.Row, "DT_EST"].ToString();

                                        _flexL[_flexL.Row, "UM"] = Sa_ComFunc.UmSearch(obj);
                                        _flexL[_flexL.Row, "AM_STD"] = _flexL[_flexL.Row, "UM"];
                                    }

                                    //견적금액
                                    if (_flex.CDecimal(_flexL[_flexL.Row, "RT_CUT"]) != 0)
                                    {
                                        //견적금액 = (단가 * 수량) - ((단가 * 수량)/ 할인율)
                                        _flexL[_flexL.Row, "AM_EST"] = (_flex.CDecimal(_flexL[_flexL.Row, "UM"]) * _flex.CDecimal(_flexL[_flexL.Row, "QT"])) - (_flex.CDecimal(_flexL[_flexL.Row, "UM"]) * _flex.CDecimal(_flexL[_flexL.Row, "QT"])) * (_flex.CDecimal(_flexL[_flexL.Row, "RT_CUT"]) / 100);
                                    }
                                    else
                                    {
                                        //견적금액 = 단가 * 수량
                                        _flexL[_flexL.Row, "AM_EST"] = _flex.CDecimal(_flexL[_flexL.Row, "UM"]) * _flex.CDecimal(_flexL[_flexL.Row, "QT"]);
                                    }


                                    _flexL[_flexL.Row, "AM_KEST"] = _flex.CDecimal(_flexL[_flexL.Row, "AM_EST"]) * _flex.CDecimal(_flexH[_flexH.Row, "RT_EXCH"]);
                                    _flexL[_flexL.Row, "AM_VAT"] = _flex.CDecimal(_flexL[_flexL.Row, "AM_KEST"]) * m_curTpVat.DecimalValue;

                                    //예상마진율
                                    //예상마진율 = (견적원화금액 - 견적기준가(구매)) / 견적원화금액 * 100
                                    if (_flex.CDecimal(_flexL[e.Row, "AM_KEST"]) - _flex.CDecimal(_flexL[e.Row, "AM_STD_PO"]) != 0 && _flex.CDecimal(_flexL[e.Row, "AM_KEST"]) != 0 && _flex.CDecimal(_flexL[e.Row, "AM_STD_PO"]) != 0)
                                    {
                                        _flexL[e.Row, "RATE"] = Math.Round(((_flex.CDecimal(_flexL[e.Row, "AM_KEST"]) - _flex.CDecimal(_flexL[e.Row, "AM_STD_PO"])) / _flex.CDecimal(_flexL[e.Row, "AM_KEST"])) * 100, 2, MidpointRounding.AwayFromZero);
                                    }
                                    else
                                    {
                                        _flexL[e.Row, "RATE"] = 0;
                                    }

                                    _flexLT[_flexL.Row - 1, "CD_ITEM"] = row["CD_ITEM"];       //품목코드
                                    _flexLT[_flexL.Row - 1, "CD_PLANT"] = _flexL[_flexL.Row - 1, "CD_PLANT"].ToString();
                                    _flexLT[_flexL.Row - 1, "NM_ITEM"] = row["NM_ITEM"];       //품목명
                                    _flexLT[_flexL.Row - 1, "STND_ITEM"] = row["STND_ITEM"];   //규격
                                }

                            }

                            _flexL.RemoveDummyColumnAll();


                            //행을 추가한 경우 마지막으로 호출해줘야 하는 메소드
                            _flexL.AddFinished();
                            _flexL.Col = _flexL.Cols.Fixed;
                            _flexL.Redraw = true;


                            _flexLT.RemoveDummyColumnAll();

                            _flexLT.AddFinished();
                            _flexLT.Col = _flexLT.Cols.Fixed;
                            _flexLT.Redraw = true;

                            //_flexLT.AfterRowChange += new RangeEventHandler(_FlexLT_AfterRowChange);

                            #endregion
                            break;
                    }
                }
                else
                {
                    switch (_flexD.Cols[e.Col].Name)
                    {

                        case "CD_ITEM":
                            #region 품목 팝업
                            if (e.Result.DialogResult == DialogResult.Cancel)
                                return;

                            bool First = true;
                            _flexD.Redraw = false;
                            _flexD.SetDummyColumnAll();


                            foreach (DataRow row in helpReturn.Rows)
                            {
                                if (First)/* 새로 품목을 입력하는 경우 */
                                {
                                    _flexD[e.Row, "CD_ITEM"] = row["CD_ITEM"];       //품목코드
                                    _flexD[e.Row, "NM_ITEM"] = row["NM_ITEM"];       //품목명
                                    _flexD[e.Row, "STND_ITEM"] = row["STND_ITEM"];   //규격
                                    _flexD[e.Row, "UNIT"] = row["UNIT_SO"];          //재고단위
                                    _flexD[e.Row, "QT"] = 1;

                                    if (_단가적용형태 == "002" || _단가적용형태 == "003")
                                    {
                                        object[] obj = new object[7];
                                        obj[0] = Global.MainFrame.LoginInfo.CompanyCode;
                                        obj[1] = _flexD[e.Row, "CD_ITEM"].ToString();
                                        obj[2] = bp_Partner.CodeValue;
                                        obj[3] = "001"; // 단가유형셋팅 (001 은 정상가 이다. 셋팅하는데가 없으니 기본 정상가로만 가져간다.)
                                        obj[4] = _flexH[_flexH.Row, "CD_EXCH"].ToString();
                                        obj[5] = _단가적용형태;
                                        obj[6] = _flexH[_flexH.Row, "DT_EST"].ToString();

                                        _flexD[e.Row, "UM"] = Sa_ComFunc.UmSearch(obj);
                                    }

                                    First = false;

                                }
                                else
                                {
                                    //행추가 코드 재사용 및 필수값이 ROW에 셋팅됨
                                    btnL_Add_Click(null, null);

                                    _flexD[_flexD.Row, "CD_ITEM"] = row["CD_ITEM"];       //품목코드
                                    _flexD[_flexD.Row, "NM_ITEM"] = row["NM_ITEM"];       //품목명
                                    _flexD[_flexD.Row, "STND_ITEM"] = row["STND_ITEM"];   //규격
                                    _flexD[_flexD.Row, "UNIT"] = row["UNIT_SO"];          //재고단위
                                    _flexD[_flexD.Row, "QT"] = 1;

                                    if (_단가적용형태 == "002" || _단가적용형태 == "003")
                                    {
                                        object[] obj = new object[7];
                                        obj[0] = Global.MainFrame.LoginInfo.CompanyCode;
                                        obj[1] = _flexD[_flexD.Row, "CD_ITEM"].ToString();
                                        obj[2] = bp_Partner.CodeValue;
                                        obj[3] = "001"; // 단가유형셋팅 (001 은 정상가 이다. 셋팅하는데가 없으니 기본 정상가로만 가져간다.)
                                        obj[4] = _flexH[_flexH.Row, "CD_EXCH"].ToString();
                                        obj[5] = _단가적용형태;
                                        obj[6] = _flexH[_flexH.Row, "DT_EST"].ToString();

                                        _flexD[_flexD.Row, "UM"] = Sa_ComFunc.UmSearch(obj);
                                    }
                                }
                            }

                            _flexD.RemoveDummyColumnAll();

                            //행을 추가한 경우 마지막으로 호출해줘야 하는 메소드
                            _flexD.AddFinished();
                            _flexD.Col = _flexD.Cols.Fixed;
                            _flexD.Redraw = true;

                            #endregion
                            break;
                    }
                }
            }
            catch (Exception ex)
            {

                MsgEnd(ex);
            }

        }
        #endregion -> _FlexL_AfterCodeHelp

        #region -> _FlexH_AfterRowChange
        private void _FlexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            int iNewRowCnt = _flexH.Row;

            if (!_flexH.HasNormalRow || _flexH["NO_EST"] == null || _flexH["NO_EST"].ToString() == "")
            {
                return;
            }
             

            //드롭다운 리스트 컬럼이 에디트를 하면 이벤트를 타기 때문에
            //이벤트를 막기 위해 컬럼을 확인하며 현재 Row와 선택 Row를 비교해야함.
            //안그러면 환율 및 DropDown 컬럼은 이벤트를 죽이기 때문.
            if ((_flexH.Cols[_flexH.ColSel].Name == "CD_EXCH" || _flexH.Cols[_flexH.ColSel].Name == "FG_BILL") && iOldRowCnt == iNewRowCnt)
                return;


            iOldRowCnt = iNewRowCnt;

            DataSet ds = null;

            string DetailFilter = string.Empty;
            int iRowIndex = _flexLT.Rows.Count - _flexLT.Rows.Fixed;

            if (!Check("SEARCH"))
                return;

            object[] obj = new object[8];
            obj[0] = _flexH["CD_COMPANY"];// 회사코드
            obj[1] = _flexH["NO_EST"];// 견적번호
            obj[2] = _flexH["NO_HST"];// 차수
            obj[3] = _flexH["CD_SALEGRP"];// 영업그룹
            obj[4] = _flexH["NO_EMP"];// 담당자
            obj[5] = _flexH["CD_PARTNER"];// 거래처
            obj[6] = _flexH["TP_VAT"];// VAT 구분
            obj[7] = _flexL["NO_LINE"];

            ds = _biz.Search(obj);

            _flexL.Binding = ds.Tables[1];
            _flexLT.Binding = ds.Tables[1].Copy();

            if (_flexH.Rows.Count - _flexH.Rows.Fixed > 0)
            {
                txt_EstimateNo.Text = _flexH[_flexH.Row, "NO_EST"].ToString();
                bp_Partner.CodeValue = _flexH[_flexH.Row, "CD_PARTNER"].ToString();
                bp_Partner.CodeName = _flexH[_flexH.Row, "LN_PARTNER"].ToString();
                bp_Emp.CodeValue = _flexH[_flexH.Row, "NO_EMP"].ToString();
                bp_Emp.CodeName = _flexH[_flexH.Row, "NM_KOR"].ToString();
                bp_SaleGroup.CodeValue = _flexH[_flexH.Row, "CD_SALEGRP"].ToString();
                bp_SaleGroup.CodeName = _flexH[_flexH.Row, "NM_SALEGRP"].ToString();
                m_cboTpVat.SelectedValue = D.GetDecimal(_flexH[_flexH.Row, "TP_VAT"].ToString());

                _단가적용형태 = _flexH[_flexH.Row, "TP_SALEPRICE"].ToString();
            }

            HeaderCheck = false;

            tabControl_SelectedIndexChanged(null, null);

            //라인의 임시 로우 값을 추가하기 위함.
            dTempCheckCnt = _flexL.DataTable.Rows.Count;

        }
        #endregion -> _FlexH_AfterRowChange

        #region -> _FlexH_ValidateEdit
        private void _FlexH_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            switch (_flexH.Cols[e.Col].Name)
            {
                case "CD_EXCH"://환종 변경시 환율적용

                    #region CD_EXCH

                    string sCD_Company = Global.MainFrame.LoginInfo.CompanyCode;
                    string sExch_Code = _flexH[_flexH.Row, "CD_EXCH"].ToString();
                    string sYYDDMM = this.MainFrameInterface.GetDateTimeToday().ToShortDateString().Replace("-", "").Substring(2, 6);//YYMMDD


                    if (MA.기준환율.Option != MA.기준환율옵션.적용안함)
                    {
                        _flexH[_flexH.Row, "RT_EXCH"] = MA.기준환율적용(D.GetString(_flexH[_flexH.Row, "DT_EST"]), sExch_Code);
                    }

                    if (sExch_Code == "000")
                        _flexH[_flexH.Row, "RT_EXCH"] = 1;
                    
                    //_flexH[_flexH.Row, "RT_EXCH"] = _biz.ExchangeSearch(sCD_Company, sExch_Code, sYYDDMM);

                    #endregion CD_EXCH
                    break;

                case "RT_EXCH"://환율변경시 환율적용

                    #region RT_EXCH

                    if (_flexL.Rows.Count > 2)
                    {
                        for (int i = 2; i < _flexL.Rows.Count; i++)
                        {
                            _flexL[i, "AM_KEST"] = _flex.CDecimal(_flexL[i, "AM_EST"]) * _flex.CDecimal(_flexH[_flexH.Row, "RT_EXCH"]);
                        }
                    }

                    #endregion RT_EXCH
                    break;

                case "DT_EST": //견적일자 입력방식 확인

                    #region DT_EST

                    if (_flexH[e.Row, "DT_EST"].ToString().Length != 8)
                    {
                        ShowMessage(공통메세지.입력형식이올바르지않습니다);

                        if (_flexH.Editor != null)
                        {
                            _flexH.Editor.Text = string.Empty;
                        }
                        e.Cancel = true;
                        return;
                    }
                    else
                    {
                        if (!_flexH.IsDate("DT_EST"))
                        {
                            ShowMessage(공통메세지.입력형식이올바르지않습니다);

                            if (_flexH.Editor != null)
                            {
                                _flexH.Editor.Text = string.Empty;
                            }
                            e.Cancel = true;
                            return;
                        }
                    }

                    #endregion DT_EST
                    break;
                case "DT_CONT": //계약예정일

                    #region DT_CONT

                    if (_flexH[e.Row, "DT_CONT"].ToString().Length != 8)
                    {
                        ShowMessage(공통메세지.입력형식이올바르지않습니다);

                        if (_flexH.Editor != null)
                        {
                            _flexH.Editor.Text = string.Empty;
                        }
                        e.Cancel = true;
                        return;
                    }
                    else
                    {
                        if (!_flexH.IsDate("DT_CONT"))
                        {
                            ShowMessage(공통메세지.입력형식이올바르지않습니다);

                            if (_flexH.Editor != null)
                            {
                                _flexH.Editor.Text = string.Empty;
                            }
                            e.Cancel = true;
                            return;
                        }
                    }

                    #endregion DT_CONT
                    break;
                case "DT_VALID": //유효기간

                    #region DT_VALID

                    if (_flexH[e.Row, "DT_VALID"].ToString().Length != 8)
                    {
                        ShowMessage(공통메세지.입력형식이올바르지않습니다);

                        if (_flexH.Editor != null)
                        {
                            _flexH.Editor.Text = string.Empty;
                        }
                        e.Cancel = true;
                        return;
                    }
                    else
                    {
                        if (!_flexH.IsDate("DT_VALID"))
                        {
                            ShowMessage(공통메세지.입력형식이올바르지않습니다);

                            if (_flexH.Editor != null)
                            {
                                _flexH.Editor.Text = string.Empty;
                            }
                            e.Cancel = true;
                            return;
                        }
                    }

                    #endregion DT_VALID
                    break;

             
            }
        }
        #endregion -> _FlexH_ValidateEdit

        #region -> _FlexH_AfterCodeHelp
        private void _FlexH_AfterCodeHelp(object sender, AfterCodeHelpEventArgs e)
        {
            switch (_flexH.Cols[e.Col].Name)
            {
                case "RT_EXCH"://환율 변경시 모든 그리드에 적용

                    if (_flexL.Rows.Count > 2)
                    {
                        for (int i = 2; i < _flexL.Rows.Count; i++)
                        {
                            _flexL[i, "AM_KEST"] = _flex.CDecimal(_flexL[i, "AM_EST"]) * _flex.CDecimal(_flexH[_flexH.Row, "RT_EXCH"]);
                        }
                    }
                    break;
            }
        }
        #endregion -> _FlexH_AfterCodeHelp

        #region -> _flex_DoubleClick
        private void _flex_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                FlexGrid flex = sender as FlexGrid;

                switch (flex.Name)
                {
                    case "_flexL":
                    case "_flexD":

                        decimal HRowCount = 0;

                        HRowCount = MaxRowCount("_FLEXH") - 1;

                        if (HRowCount != D.GetDecimal(_flexH["NO_HST"]))
                            return;

                        if (flex.Cols[flex.Col].Name == "UM")
                        {
                            //영업그룹에 영업조직을 조회하여 판매단가 통제유무가 "Y" 이면 단가와 금액 컬럼 수정을 할수 없다.
                            if (so_Price == "Y")
                            {
                                //ShowMessage("영업단가통제된 영업그룹입니다."); //여기서 메세지 처리를 해버리면~ _flex_StartEdit 에서 한것까지 2번 메세지 처리가 되삔당.~
                                return;
                            }

                            P_SA_UM_HISTORY_SUB dlg = new P_SA_UM_HISTORY_SUB(bp_Partner.CodeValue, bp_Partner.CodeName,
                            string.Empty, string.Empty, flex["CD_PLANT"].ToString(), flex["CD_ITEM"].ToString(),
                            flex["NM_ITEM"].ToString(), _flexH["CD_EXCH"].ToString());

                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                flex["UM"] = dlg.단가;
                                flex["AM_STD"] = dlg.단가;

                                if (flex.Name == "_flexL")
                                {
                                    //flex["AM_STD"] = D.GetDecimal(flex["QT"]) * D.GetDecimal(flex["UM"]);//견적기준가
                                    //flex["AM_EST"] = (D.GetDecimal(_flex["UM"]) * D.GetDecimal(_flex["QT"])) * (D.GetDecimal(_flex["RT_CUT"]) / 100);

                                    //견적금액
                                    if (_flex.CDecimal(_flex["RT_CUT"]) != 0)
                                    {
                                        //견적금액 = (단가 * 수량) - ((단가 * 수량)/ 할인율)
                                        _flex["AM_EST"] = (_flex.CDecimal(_flex["UM"]) * _flex.CDecimal(_flex["QT"])) - (_flex.CDecimal(_flex["UM"]) * _flex.CDecimal(_flex["QT"])) * (_flex.CDecimal(_flex["RT_CUT"]) / 100);
                                    }
                                    else
                                    {
                                        //견적금액 = 단가 * 수량
                                        _flex["AM_EST"] = _flex.CDecimal(_flex["UM"]) * _flex.CDecimal(_flex["QT"]);
                                    }



                                    flex["AM_KEST"] = D.GetDecimal(flex["AM_EST"]) * D.GetDecimal(_flexH["RT_EXCH"]);//견적원화금액 재계산
                                    flex["AM_VAT"] = D.GetDecimal(flex["AM_KEST"]) * (m_curTpVat.DecimalValue / 100);//부가세


                                    //예상마진율
                                    //예상마진율 = (견적원화금액 - (견적기준가(구매)) / 견적원화금액) * 100
                                    if (_flex.CDecimal(flex["AM_KEST"]) - _flex.CDecimal(flex["AM_STD_PO"]) != 0 && _flex.CDecimal(flex["AM_KEST"]) != 0 && _flex.CDecimal(flex["AM_STD_PO"]) != 0)
                                    {
                                        flex["RATE"] = Math.Round(((_flex.CDecimal(flex["AM_KEST"]) - _flex.CDecimal(flex["AM_STD_PO"])) / _flex.CDecimal(flex["AM_KEST"])) * 100, 2, MidpointRounding.AwayFromZero);
                                    }
                                    else
                                    {
                                        flex["RATE"] = 0;
                                    }

                                }
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region -> _flex_StartEdit
        private void _flex_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                FlexGrid flex = sender as FlexGrid;

                switch (flex.Name)
                {
                    case "_flexH":
                        if (flex.Cols[e.Col].Name == "RT_EXCH" && MA.기준환율.Option == MA.기준환율옵션.적용_수정불가)
                        {
                            e.Cancel = true;
                            return;
                        }

                        break;

                    case "_flexL":
                    case "_flexD":

                        if (bp_SaleGroup.CodeValue != string.Empty)
                        {
                            object[] obj = new object[2];
                            obj[0] = Global.MainFrame.LoginInfo.CompanyCode;
                            obj[1] = bp_SaleGroup.CodeValue;
                            so_Price = Sa_ComFunc.GetSaleOrgUmCheck(obj);
                        }

                        //영업그룹에 영업조직을 조회하여 판매단가 통제유무가 "Y" 이면 단가와 금액 컬럼 수정을 할수 없다.
                        if (so_Price == "Y")
                        {
                            if (flex.Name == "_flexD")
                            {
                                if (flex.Cols[e.Col].Name == "UM")
                                {
                                    ShowMessage("영업단가통제된 영업그룹입니다.");
                                    e.Cancel = true;
                                }
                            }
                            else
                            {
                                if (flex.Cols[e.Col].Name == "UM" || flex.Cols[e.Col].Name == "AM_SO")
                                {
                                    ShowMessage("영업단가통제된 영업그룹입니다.");
                                    e.Cancel = true;
                                }
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #endregion 그리드 Event

        #region 프리폼 Event

        #region m_cboTpVat_SelectionChangeCommitted
        private void m_cboTpVat_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                switch (((Control)sender).Name)
                {
                    case "m_cboTpVat":

                        if (m_cboTpVat.SelectedValue == null || m_cboTpVat.SelectedValue.ToString() == string.Empty || m_cboTpVat.DataSource == null)
                        {
                            m_curTpVat.DecimalValue = 0;
                            return;
                        }

                        DataTable dt = (DataTable)m_cboTpVat.DataSource;
                        dt.PrimaryKey = new DataColumn[] { dt.Columns[0] };
                        DataRow row = dt.Rows.Find(m_cboTpVat.SelectedValue);

                        m_curTpVat.DecimalValue = D.GetDecimal(row["CD_FLAG1"]);

                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }

        }
        #endregion m_cboTpVat_SelectionChangeCommitted

        #region tabControl_SelectedIndexChanged
        /// <summary>
        /// 디테일과 라인의 탭이 이동할때 행추가 삭제 버튼을 체크함.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab.Name.ToUpper() == "LIST")
            {
                //디테일 탭 변경과 조회 이벤트를 주기 위함
                //추가후 디테일 이벤트를 강제로 태우기 위함
                if (_flexLT.Rows.Count - _flexL.Rows.Fixed > 0)
                {
                    _flexLT.Row = 1;
                }
            }

            BtnCheck();

        }
        #endregion tabControl_SelectedIndexChanged

        #region splitter_DoubleClick
        private void splitter_DoubleClick(object sender, EventArgs e)
        {
            if (_flexLT.Visible == true)
            {
                _flexLT.Visible = false;
            }
            else
            {
                _flexLT.Visible = true;
            }
        }
        #endregion splitter_DoubleClick

        #endregion 프리폼 Event

        #region _FlexH 라인 추가 삭제
        private void btnH_Add_Click(object sender, EventArgs e)
        {
            if (!Check("ADD"))
                return;

            if (!this.Verify())
                return;


            string sCD_Company = string.Empty;
            string sExch_Code = string.Empty;
            string sYYDDMM = string.Empty;
            decimal LineSeq = 0;
            sNoEst = txt_EstimateNo.Text;


            //0이면 견적번호 신규
            if (_flexH.Rows.Count - _flexH.Rows.Fixed == 0)
            {
                #region 추가
                //헤더 추가 여부
                HeaderCheck = true;

                _flexH.Focus();

                _flexH.Rows.Add();

                _flexH.Row = _flexH.Rows.Count - 1;

                int iHeaderRow = _flexH.Rows.Count - _flexH.Rows.Fixed;

                _flexH[iHeaderRow, "CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                _flexH[iHeaderRow, "CD_BIZAREA"] = Global.MainFrame.LoginInfo.BizAreaCode;
                _flexH[iHeaderRow, "NO_EST"] = "";
                _flexH[iHeaderRow, "NO_HST"] = 1;
                _flexH[iHeaderRow, "CD_SALEGRP"] = bp_SaleGroup.CodeValue;
                _flexH[iHeaderRow, "NM_SALEGRP"] = bp_SaleGroup.CodeName;
                _flexH[iHeaderRow, "CD_EXCH"] = "000";
                _flexH[iHeaderRow, "CD_PARTNER"] = bp_Partner.CodeValue;
                _flexH[iHeaderRow, "LN_PARTNER"] = bp_Partner.CodeName;
                _flexH[iHeaderRow, "NO_EMP"] = bp_Emp.CodeValue;
                _flexH[iHeaderRow, "NM_KOR"] = bp_Emp.CodeName;
                _flexH[iHeaderRow, "TP_VAT"] = m_cboTpVat.SelectedValue == null ? string.Empty : m_cboTpVat.SelectedValue.ToString();
                _flexH[iHeaderRow, "FG_VAT"] = "N";
                _flexH[iHeaderRow, "STA_EST"] = "O";

                //환율 정보를 얻기 위함.
                sCD_Company = Global.MainFrame.LoginInfo.CompanyCode;
                sExch_Code = _flexH[iHeaderRow, "CD_EXCH"].ToString();
                sYYDDMM = this.MainFrameInterface.GetDateTimeToday().ToShortDateString().Replace("-", "").Substring(2, 6);//YYMMDD

                _flexH[iHeaderRow, "RT_EXCH"] = _biz.ExchangeSearch(sCD_Company, sExch_Code, sYYDDMM);
                _flexH[iHeaderRow, "DT_EST"] = this.MainFrameInterface.GetDateTimeToday().ToShortDateString().Replace("-", "");

                //라인 초기화를 위해 이벤트를 태움
                _flexH.AddFinished();
                _flexH.Col = _flexH.Cols.Fixed;

                //로우가 없을므로 초기화 해줌.
                dTempCheckCnt = 0;

                //헤더복사여부
                HeaderAddCheck = false;

                #endregion 추가
            }
            else
            {
                #region 복사
                _flexH.Focus();


                LineSeq = D.GetDecimal(_flexH["NO_HST"]);
                HeaderCheck = true;

                DataTable dt = null;
                dt = _flexH.DataTable;

                DataSet ds = null;

                //선택한 Row정보를 Object형으로 받아서 새로운 로우에 처리하기 위함
                object[] obj = new object[17];

                obj[0] = _flexH["CD_COMPANY"];//CD_COMPANY
                obj[1] = _flexH["CD_BIZAREA"];//CD_BIZAREA
                obj[2] = _flexH["NO_EST"];//NO_EST
                obj[4] = _flexH["CD_SALEGRP"];//CD_SALEGRP
                obj[5] = _flexH["NM_SALEGRP"];//NM_SALEGRP
                obj[6] = _flexH["CD_EXCH"];//CD_EXCH
                obj[7] = _flexH["CD_PARTNER"];//CD_PARTNER
                obj[8] = _flexH["LN_PARTNER"];//LN_PARTNER
                obj[9] = _flexH["NO_EMP"];//NO_EMP
                obj[10] = _flexH["NM_KOR"];//NM_KOR
                obj[11] = _flexH["TP_VAT"];//TP_VAT
                obj[12] = _flexH["FG_VAT"];//FG_VAT
                obj[13] = _flexH["STA_EST"];//STA_EST
                obj[14] = _flexH["NO_EST_NM"];//견적명
                obj[15] = _flexH["TP_SALEPRICE"];//견적명
                obj[16] = _flexH["FG_BILL"];//결재조건


                //해당 견적의 견적 차수를 구하기위해 필터링 함
                //TODO :: 해당 차수 복사를 위한 부분을 적용 하기 위해 해당 차수를 구하면 됨.
                //이부분만 수정하면 해결됨.
                DataRow[] dr = dt.Select("NO_EST = '" + _flexH["NO_EST"].ToString() + "'");
                obj[3] = dr.Length + 1;

                _flexH.Rows.Add();

                int iHeaderRow = _flexH.Rows.Count - _flexH.Rows.Fixed;

                _flexH[iHeaderRow, "CD_COMPANY"] = obj[0];
                _flexH[iHeaderRow, "CD_BIZAREA"] = obj[1];
                _flexH[iHeaderRow, "NO_EST"] = obj[2];
                _flexH[iHeaderRow, "NO_HST"] = obj[3];
                _flexH[iHeaderRow, "CD_SALEGRP"] = obj[4];
                _flexH[iHeaderRow, "NM_SALEGRP"] = obj[5];
                _flexH[iHeaderRow, "CD_EXCH"] = obj[6];
                _flexH[iHeaderRow, "CD_PARTNER"] = obj[7];
                _flexH[iHeaderRow, "LN_PARTNER"] = obj[8];
                _flexH[iHeaderRow, "NO_EMP"] = obj[9];
                _flexH[iHeaderRow, "NM_KOR"] = obj[10];
                _flexH[iHeaderRow, "TP_VAT"] = obj[11];
                _flexH[iHeaderRow, "FG_VAT"] = "N";
                _flexH[iHeaderRow, "STA_EST"] = "O";
                _flexH[iHeaderRow, "NO_EST_NM"] = obj[14];
                _flexH[iHeaderRow, "TP_SALEPRICE"] = obj[15];
                _flexH[iHeaderRow, "FG_BILL"] = obj[16];

                //환율 정보를 얻기 위함.
                sCD_Company = Global.MainFrame.LoginInfo.CompanyCode;
                sExch_Code = _flexH[iHeaderRow, "CD_EXCH"].ToString();
                sYYDDMM = this.MainFrameInterface.GetDateTimeToday().ToShortDateString().Replace("-", "").Substring(2, 6);//YYMMDD

                _flexH[iHeaderRow, "RT_EXCH"] = _biz.ExchangeSearch(sCD_Company, sExch_Code, sYYDDMM);
                _flexH[iHeaderRow, "DT_EST"] = this.MainFrameInterface.GetDateTimeToday().ToShortDateString().Replace("-", "");

                //단가적용형태를 받아오기 위함
                _단가적용형태 = _flexH[iHeaderRow, "TP_SALEPRICE"].ToString();

                //라인 초기화를 위해 이벤트를 태움
                _flexH.AddFinished();
                _flexH.Col = _flexH.Cols.Fixed;



                object[] objTemp = new object[8];

                objTemp[0] = _flexH["CD_COMPANY"];// 회사코드
                objTemp[1] = _flexH["NO_EST"];// 견적번호
                objTemp[2] = LineSeq;// 차수
                objTemp[3] = _flexH["CD_SALEGRP"];// 영업그룹
                objTemp[4] = _flexH["NO_EMP"];// 담당자
                objTemp[5] = _flexH["CD_PARTNER"];// 거래처
                objTemp[6] = _flexH["TP_VAT"];// VAT 구분
                objTemp[7] = null;

                ds = _biz.Search(objTemp);


                DataTable dtL = ds.Tables[1].Clone();
                CopyTable = ds.Tables[2].Clone();

                //복사된 라인의 DataRow의 상태를 INSERT로 변경함
                //저장 시점에 인식을 못해서 추가함
                if (ds.Tables[1].Rows.Count != 0)
                {
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {

                        ds.Tables[1].Rows[i]["NO_HST"] = _flexH["NO_HST"];

                        ds.Tables[1].Rows[i].AcceptChanges();
                        dtL.ImportRow(ds.Tables[1].Rows[i]);

                        dtL.Rows[i].SetAdded();
                    }


                }

                //복사된 라인디테일의 DataRow의 상태를 Insert로 변경함
                if (ds.Tables[2].Rows.Count != 0)
                {
                    for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                    {
                        ds.Tables[2].Rows[i]["NO_HST"] = _flexH["NO_HST"];

                        ds.Tables[2].Rows[i].AcceptChanges();
                        CopyTable.ImportRow(ds.Tables[2].Rows[i]);

                        CopyTable.Rows[i].SetAdded();
                    }


                }

                _flexL.Binding = dtL;
                _flexLT.Binding = _flexL.DataTable.Copy();

                decimal RowCnt = 0;


                //템프라인과 라인테이블 동기화를 위해 복사
                if (!_flexLT.HasNormalRow)
                    return;

                string Filter = "NO_EST = '" + _flexH[_flexH.Row, "NO_EST"].ToString() + "'" + " AND " + "NO_HST = " + _flexH[_flexH.Row, "NO_HST"].ToString() + " AND " + "NO_LINE = " + _flexLT[_flexLT.Row, "NO_LINE"];

                _flexD.BindingAdd(CopyTable, Filter);

                //바인딩 된 디테일 데이타 로우의 상태를 INSERT로 변경함
                for (int i = 0; i < _flexD.DataTable.Rows.Count; i++)
                {
                    _flexD.DataTable.Rows[i].SetAdded();
                }


                //라인과 디테일의 관계를 다시 잡음
                //라인의 LineNo와 Detail과의 관계를 새롭게 잡아줌
                for (int i = 0; i < _flexL.DataTable.Rows.Count; i++)
                {
                    RowCnt = D.GetDecimal(_flexL.DataTable.Rows[i]["NO_LINE"]);

                    _flexL.DataTable.Rows[i]["NO_LINE"] = 0;
                    _flexL.DataTable.Rows[i]["TEMPCHECK"] = i + 1;

                    _flexLT.DataTable.Rows[i]["NO_LINE"] = 0;
                    _flexLT.DataTable.Rows[i]["TEMPCHECK"] = i + 1;

                    for (int j = 0; j < _flexD.DataTable.Rows.Count; j++)
                    {
                        if (RowCnt == D.GetDecimal(_flexD.DataTable.Rows[j]["NO_LINE"]))
                        {
                            _flexD.DataTable.Rows[j]["SEQ"] = 0;//차수
                            _flexD.DataTable.Rows[j]["TEMPCHECK"] = _flexLT.DataTable.Rows[i]["TEMPCHECK"];//차수
                        }
                    }
                }


                //복사 후 행 추가시 임시 라인번호를 위해 초기화 시켜줌
                //이것을 안해주면 기존 디테일의 쓰레기와 연결됨.
                //신규 라인값에 추가하기 위함.
                dTempCheckCnt = _flexL.DataTable.Rows.Count;


                tabControl.SelectedIndex = 0;

                //헤더 복사 여부
                HeaderAddCheck = false;
                #endregion 복사
            }

            BtnCheck();

            HeaderCheck = true;
        }

        private void btnH_Del_Click(object sender, EventArgs e)
        {
            int iLineRowCount = _flexL.Rows.Count - _flexL.Rows.Fixed;
            int iLineTempCount = _flexLT.Rows.Count - _flexLT.Rows.Fixed;
            int iDetailRowCount = _flexD.DataTable.Rows.Count;
            string NO_EST = _flexH[_flexH.Row, "NO_EST"].ToString();
            decimal dMaxRow = 0;

            //최종 헤더 값인지를 체크 하여 삭제 후 추가 버튼 활성화 체크
            for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
            {
                if (_flexH[i, "NO_EST"].ToString() == NO_EST)
                {
                    dMaxRow = dMaxRow + 1;
                }
            }

            if (D.GetDecimal(_flexH[_flexH.Row, "NO_HST"]) == dMaxRow)
            {
                HeaderAddCheck = true;
            }


            #region 헤더 삭제 이후 라인, 템프, 디테일 삭제
            if (iLineRowCount >= 0)
            {
                for (int i = 0; i < iLineRowCount; i++)
                {
                    _flexL.RemoveItem(_flexL.Row);
                }

                for (int i = 0; i < iLineTempCount; i++)
                {
                    _flexLT.RemoveItem(_flexLT.Row);
                }

                for (int i = 0; i < iDetailRowCount; i++)
                {
                    _flexD.DataTable.Rows[0].Delete();
                }

                _flexH.RemoveItem(_flexH.Row);

            }
            #endregion 헤더 삭제 이후 라인, 템프, 디테일 삭제


            BtnCheck();
        }
        #endregion _FlexH 라인 추가 삭제

        #region _FlexL & _FlexD 라인 추가 삭제
        private void btnL_Add_Click(object sender, EventArgs e)
        {

            decimal dMaxRow = 0;
            int iLRow = 0;
            int iDRow = 0;

            //헤더가 없을 경우 추가불가
            if (_flexH.Rows.Count - _flexH.Rows.Fixed == 0)
            {
                ShowMessage("헤더없이 추가할 수 없습니다");
                return;
            }


            if (tabControl.SelectedTab.Name.ToUpper() == "ITEM")
            {
                #region 라인추가
                _flexL.Rows.Add();

                iLRow = _flexL.Rows.Count - _flexL.Rows.Fixed;

                //최대 차수에 + 1 됨
                dMaxRow = MaxRowCount("_FlexL");

                //첫 로우가 아니면 상위의 로우에서 공장과 할인율 적용
                if (iLRow != 0)
                {
                    _flexL[iLRow + 1, "CD_PLANT"] = _flexL[iLRow, "CD_PLANT"]; //공장
                    _flexL[iLRow + 1, "RT_CUT"] = _flexL[iLRow, "RT_CUT"];     //할인율
                }
                else
                {
                    _flexL[iLRow + 1, "CD_COMPANY"] = _flexH[iLRow, "CD_COMPANY"];     //회사
                    _flexL[iLRow + 1, "CD_COMPANY"] = _flexH[iLRow, "CD_COMPANY"];     //회사
                }


                _flexL[iLRow + 1, "CD_COMPANY"] = _flexH[_flexH.Row, "CD_COMPANY"];     //회사
                _flexL[iLRow + 1, "NO_EST"] = _flexH[_flexH.Row, "NO_EST"];             //견적번호
                _flexL[iLRow + 1, "NO_HST"] = _flexH[_flexH.Row, "NO_HST"];             //차수
                _flexL[iLRow + 1, "RT_CUT"] = 0.0;                                      //할인율
                _flexL[iLRow + 1, "UM"] = 0;                                            //단가
                _flexL[iLRow + 1, "AM_STD"] = 0;                                        //기준금액
                _flexL[iLRow + 1, "AM_EST"] = 0;
                _flexL[iLRow + 1, "AM_KEST"] = 0;                                       //원화견적금액
                _flexL[iLRow + 1, "AM_STD_PO"] = 0;                                     //견적기준가(구매)
                _flexL[iLRow + 1, "AM_VAT"] = _flex.CDecimal(_flexL[iLRow + 1, "AM_KEST"]) * (m_curTpVat.DecimalValue / 100);//부가세
                //예상마진율
                _flexL[iLRow + 1, "RATE"] = 0;

                //신규여부
                //또는 복사일 경우 항번을 새롭게 잡아주기 위해서 계산을 함.
                if (_flexH[_flexH.Row, "NO_EST"] == null || _flexH[_flexH.Row, "NO_EST"].ToString() == "" || HeaderCheck == true)
                {
                    _flexL[iLRow + 1, "NO_LINE"] = 0;//항번

                }
                else
                {
                    _flexL[iLRow + 1, "NO_LINE"] = dTempCheckCnt + 1; //항번
                }

                _flexL[iLRow + 1, "TEMPCHECK"] = dTempCheckCnt + 1;//항번

                //다음 행추가시 임시 항번 위해 증가시켜놓음
                dTempCheckCnt = dTempCheckCnt + 1;

                //라인 추가시 항시 해줘야함
                _flexL.Col = _flexL.Cols.Fixed;
                _flexL.AddFinished();
                _flexL.Focus();


                //라인 Temp 동기화를 위해 행추가 함.
                _flexLT.Rows.Add();

                //L라인 Temp에 임시값을 복사시킴.
                _flexLT[iLRow, "NO_LINE"] = _flexL[iLRow + 1, "NO_LINE"];
                _flexLT[iLRow, "TEMPCHECK"] = _flexL[iLRow + 1, "TEMPCHECK"];

                //그리드에 포커스를 주기 위함
                _flexL.Row = _flexL.Rows.Count - 1;
                _flexLT.Row = _flexLT.Rows.Count - 1;
                #endregion 라인추가
            }
            else
            {
                #region 디테일 추가
                _flexD.Rows.Add();

                iDRow = _flexD.Rows.Count - _flexD.Rows.Fixed;

                //최대 차수에 + 1 됨
                dMaxRow = MaxRowCount("_FlexD");

                _flexD[iDRow + 1, "CD_COMPANY"] = _flexH[_flexH.Row, "CD_COMPANY"];
                _flexD[iDRow + 1, "NO_EST"] = _flexH[_flexH.Row, "NO_EST"];         //견적번호
                _flexD[iDRow + 1, "NO_HST"] = _flexH[_flexH.Row, "NO_HST"];         //차수



                //신규여부
                //항번을 새롭게 계산하는 부분
                if (_flexH[_flexH.Row, "NO_EST"] == null || _flexH[_flexH.Row, "NO_EST"].ToString() == "" || HeaderCheck == true)
                {
                    _flexD[iDRow + 1, "NO_LINE"] = _flexLT[_flexLT.Row, "TEMPCHECK"];      //항번
                    _flexD[iDRow + 1, "SEQ"] = 0;//차수
                    _flexD[iDRow + 1, "TEMPCHECK"] = _flexLT[_flexLT.Row, "TEMPCHECK"];//차수
                }
                else
                {
                    _flexD[iDRow + 1, "NO_LINE"] = _flexLT[_flexLT.Row, "NO_LINE"];      //항번
                    _flexD[iDRow + 1, "SEQ"] = dMaxRow;//차수
                    _flexD[iDRow + 1, "TEMPCHECK"] = _flexLT[_flexLT.Row, "TEMPCHECK"]; ;//차수
                }


                _flexD[iDRow + 1, "QT"] = 0;
                _flexD[iDRow + 1, "UM"] = 0;                                        //단가

                //라인 추가시 항시 해줘야함
                _flexD.Col = _flexD.Cols.Fixed;
                _flexD.AddFinished();
                _flexD.Focus();

                //그리드에 포커스를 주기 위함
                _flexD.Row = _flexD.Rows.Count - 1;
                #endregion 디테일 추가

            }

            BtnCheck();

        }

        private void btnL_Del_Click(object sender, EventArgs e)
        {
            //견적품목 탭 라인에 추가
            //품목 삭제시 라인 템프를 동기화 시키 위함.
            if (tabControl.SelectedTab.Name.ToUpper() == "ITEM")
            {
                decimal dLineNo = D.GetDecimal(_flexL[_flexL.Row, "TEMPCHECK"]);
                int iflexDCount = _flexD.DataTable.Rows.Count;

                for (int j = 0; j < _flexD.DataTable.Rows.Count; j++)
                {
                    if (_flexD.DataTable.Rows[j].RowState != DataRowState.Deleted && D.GetDecimal(_flexD.DataTable.Rows[j]["TEMPCHECK"]) == dLineNo)
                    {
                        _flexD.DataTable.Rows[j].Delete();
                    }
                }

                _flexL.RemoveItem(_flexL.Row);

                for (int i = _flexLT.Rows.Fixed; i < _flexLT.Rows.Count; i++)
                {
                    if (D.GetDecimal(_flexLT.Rows[i]["TEMPCHECK"]) == dLineNo)
                    {
                        _flexLT.RemoveItem(i);
                    }

                }

                BtnCheck();

            }
            else
            {
                _flexD.RemoveItem(_flexD.Row);

                BtnCheck();
            }
        }
        #endregion _FlexL & _FlexD 라인 추가 삭제

        #region btn_Rt_Cut_Click(할인율 적용)
        /// /// <summary>
        /// 할인율 적용 버튼을 통한 라인 할인율 일괄 적용
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Rt_Cut_Click(object sender, EventArgs e)
        {
            //첫 라인의 할인율 수정시 일괄 수정
            if (!_flexL.HasNormalRow)
            {
                ShowMessage("적용할 라인이 없습니다.");
                return;
            }


            if (_flexL.Rows.Count - _flexL.Rows.Fixed > 0)
            {
                for (int i = _flexL.Rows.Fixed; i < _flexL.Rows.Count; i++)
                {
                    //첫 라인의 할인율을 적용
                    _flexL[i, "RT_CUT"] = currency_Rt_Cut.DecimalValue;

                    //견적금액
                    if (_flex.CDecimal(_flexL[i, "RT_CUT"]) != 0)
                    {
                        //견적금액 = (단가 * 수량) - ((단가 * 수량)/ 할인율)
                        _flexL[i, "AM_EST"] = (_flex.CDecimal(_flexL[i, "UM"]) * _flex.CDecimal(_flexL[i, "QT"])) - (_flex.CDecimal(_flexL[i, "UM"]) * _flex.CDecimal(_flexL[i, "QT"])) * (_flex.CDecimal(_flexL[i, "RT_CUT"]) / 100);
                    }
                    else
                    {
                        //견적금액 = 단가 * 수량
                        _flexL[i, "AM_EST"] = _flex.CDecimal(_flexL[i, "UM"]) * _flex.CDecimal(_flexL[i, "QT"]);
                    }


                    _flexL[i, "AM_KEST"] = D.GetDecimal(_flexL[i, "AM_EST"]) * D.GetDecimal(_flexH["RT_EXCH"]);//견적원화금액 재계산
                    _flexL[i, "AM_VAT"] = D.GetDecimal(_flexL[i, "AM_KEST"]) * (m_curTpVat.DecimalValue / 100);//부가세


                    //예상마진율
                    if (_flex.CDecimal(_flexL[i, "AM_KEST"]) - _flex.CDecimal(_flexL[i, "AM_STD_PO"]) != 0 && _flex.CDecimal(_flexL[i, "AM_KEST"]) != 0 && _flex.CDecimal(_flexL[i, "AM_STD_PO"]) != 0)
                    {
                        _flexL[i, "RATE"] = Math.Round(((_flex.CDecimal(_flexL[i, "AM_KEST"]) - _flex.CDecimal(_flexL[i, "AM_STD_PO"])) / _flex.CDecimal(_flexL[i, "AM_KEST"])) * 100, 2, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        _flexL[i, "RATE"] = 0;
                    }


                }

                ShowMessageKor("적용되었습니다.");
            }
            else
            {
                ShowMessageKor("적용할 라인이 없습니다");
            }
        }
        #endregion btn_Rt_Cut_Click(할인율 적용)

        #region 도움창셋팅

        #region Control_QueryAfter
        #region -> Control_QueryAfter

        private void Control_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                switch (e.HelpID)
                {
                    case Duzon.Common.Forms.Help.HelpID.P_MA_SALEGRP_SUB:
                        _단가적용형태 = e.HelpReturn.Rows[0]["TP_SALEPRICE"].ToString();

                        object[] obj = new object[2];
                        obj[0] = Global.MainFrame.LoginInfo.CompanyCode;
                        obj[1] = e.HelpReturn.Rows[0]["CD_SALEGRP"].ToString();
                        so_Price = Sa_ComFunc.GetSaleOrgUmCheck(obj);
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

        #endregion
    }
}

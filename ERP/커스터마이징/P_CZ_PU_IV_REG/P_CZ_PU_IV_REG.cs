using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace cz
{
	public partial class P_CZ_PU_IV_REG : PageBase
    {
        #region ♣ 멤버필드

        private P_CZ_PU_IV_REG_BIZ _biz;
        string _cddept = "";
        DataSet _dsCombo = new DataSet();
        DataTable _dt_Line = new DataTable();
        DataTable ds_Ty1 = new DataTable();

        string _지급관리통제설정 = "N";
        string _지급예정일통제설정 = "000";

        bool bStandard = false; //규격형

        #endregion        

        #region ♣ 초기화

        public P_CZ_PU_IV_REG()
        {
            StartUp.Certify(this);
            InitializeComponent();

            this.MainGrids = new FlexGrid[] { this._flexH, this._flexL };
        }

        protected override void InitLoad()
        {
            this._biz = new P_CZ_PU_IV_REG_BIZ(this.MainFrameInterface);

            //InitDD(_tlay_Main);
            this.InitMaExc();
            this.InitControl();

            this.InitGridH();
            this.InitGridL();

            this.InitEvent();

            this._flexH.DetailGrids = new FlexGrid[] { this._flexL };
        }

        private void InitMaExc()
        {
            /************************************************************************
             * _지급관리통제설정 : 회계의 지급관리에 등록된 지급관리를 사용한다
             *                     거래처등록부터 지급관리코드를 사용하기때문에 기준쪽에 통제설정있다.
             *                     외주,수입에서 함께 사용한다.
             * _지급예정일통제설정 : 지급예정일을 거래처등록의 물류지급예정이를 사용할것인지
             *                       회계 지급예정이를 쓸것인지를 결정한다.
             *                       차이는 물류 지급예정일은 거래일자 + 지급예정일
             *                       회계의 지급예정일은 차월또츤 차차월 xx일 형식으로 처리된다.
             ************************************************************************/

            this._지급관리통제설정 = BASIC.GetMAEXC("거래처정보등록-회계지급관리사용여부");
            this._지급예정일통제설정 = BASIC.GetMAEXC("매입등록-지급예정일통제설정");


            //규격형 사용 유무
            if (BASIC.GetMAEXC("공장품목등록-규격형") == "100")
            {
                this.bStandard = true;
            }
        }

        private void InitControl()
        {
            try
            {
                this.dtp처리일자.Mask = MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
                this.dtp처리일자.ToDayDate = MainFrameInterface.GetDateTimeToday();
                this.dtp처리일자.Text = MainFrameInterface.GetDateTimeToday().ToString();

                this.ctx담당자.CodeName = this.LoginInfo.EmployeeName;
                this.ctx담당자.CodeValue = this.LoginInfo.EmployeeNo;
                this._cddept = this.LoginInfo.DeptCode;

                this.rbtn계산서처리일괄.Checked = true;
                this.SetControlEnabled(true);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override void InitPaint()
        {
            #region 숨김처리
            this.bpc매입형태변경.Visible = false;
            this.bpc부가세사업장변경.Visible = false;
            #endregion

            this.oneGrid1.UseCustomLayout = true;
            this.bpPanelControl1.IsNecessaryCondition = true;
            this.bpPanelControl3.IsNecessaryCondition = true;
            this.bpPanelControl4.IsNecessaryCondition = true;
            this.bpPanelControl5.IsNecessaryCondition = true;
            this.bpc매입형태변경.IsNecessaryCondition = true;

            this.oneGrid1.InitCustomLayout();

            _dsCombo = this.GetComboData("N;PU_C000016", "S;MA_CODEDTL_003", "NC;MA_BIZAREA", "N;MA_B000005",
                                         "N;FI_J000002", "N;PU_C000044", "S;MA_AISPOSTH;200", "NC;MA_BIZAREA", "N;PU_C000005",
                                         "N;MA_B000095", "N;PU_C000014");

            // 거래구분		
            this.cbo거래구분.DataSource = _dsCombo.Tables[0];
            this.cbo거래구분.DisplayMember = "NAME";
            this.cbo거래구분.ValueMember = "CODE";

            // 매입형태변경(2011.03.16추가)
            this.cbo매입형태.DataSource = _dsCombo.Tables[6].Copy();
            this.cbo매입형태.DisplayMember = "NAME";
            this.cbo매입형태.ValueMember = "CODE";

            this.ctx사업장.CodeValue = LoginInfo.BizAreaCode;
            this.ctx사업장.CodeName = LoginInfo.BizAreaName;

            DataTable dt_biz = _biz.getCd_Pc(ctx사업장.CodeValue);

            if (dt_biz != null || dt_biz.Rows.Count > 0)
            {
                this.ctx부가세사업장.CodeValue = D.GetString(dt_biz.Rows[0]["CD_BIZAREA"]);
                this.ctx부가세사업장.CodeName = D.GetString(dt_biz.Rows[0]["NM_BIZAREA"]);
            }
            else
            {
                this.ctx부가세사업장.CodeValue = LoginInfo.BizAreaCode;
                this.ctx부가세사업장.CodeName = LoginInfo.BizAreaName;
            }

            this._flexH.SetDataMap("CD_EXCH", _dsCombo.Tables[3], "CODE", "NAME");

            this._flexH.SetDataMap("CD_DOCU", _dsCombo.Tables[4], "CODE", "NAME");
            //_flexL.SetDataMap("FG_TPPURCHASE", ldt_FG_TPPURCHAS, "CODE", "NAME");    //매입형태 코드, 네임
            this._flexL.SetDataMap("FG_TPPURCHASE", _dsCombo.Tables[6].Copy(), "CODE", "NAME");    //매입형태 코드, 네임

            if (this._지급관리통제설정 == "N")
                this._flexH.SetDataMap("FG_PAYBILL", _dsCombo.Tables[5], "CODE", "NAME");
            else
            {
                DataTable dt_pay = ComFunc.GetPayList();
                if (dt_pay != null)
                    this._flexH.SetDataMap("FG_PAYBILL", dt_pay, "CODE", "NAME");
            }

            // 부가세여부 
            this._flexL.SetDataMap("TP_UM_TAX", _dsCombo.Tables[8], "CODE", "NAME");

            this._flexH.SetDataMap("YN_JEONJA", _dsCombo.Tables[9], "CODE", "NAME"); //계산서발행형태
            this._flexH.Binding = _biz.GetLineTable();//주 그리드 초기화
            
            ///지급조건(발주) 
            this._flexH.SetDataMap("FG_PAYMENT", _dsCombo.Tables[10], "CODE", "NAME");
        }

        private void InitGridH()
        {
            this._flexH.BeginSetting(1, 1, false);

            this._flexH.SetCol("CHK", "선택", 40, true, CheckTypeEnum.Y_N);
            if (BASIC.GetMAEXC("매입등록_매입번호_KEY-IN 사용여부") == "100")
                this._flexH.SetCol("NO_IV", "매입번호", 100, true);
            else
                this._flexH.SetCol("NO_IV", "매입번호", 100, false);

            this._flexH.SetCol("CD_BIZAREA", "사업장", false);
            this._flexH.SetCol("NM_BIZAREA", "사업장명", false);
            this._flexH.SetCol("CD_BIZAREA_TAX", "부가세사업장", false);
            this._flexH.SetCol("NM_BIZAREA_TAX", "부가세사업장명", false);
            this._flexH.SetCol("CD_PARTNER", "매입처코드", false);
            this._flexH.SetCol("LN_PARTNER", "매입처명", 150);
            this._flexH.SetCol("TXT_USERDEF1", "사용자정의1", false);
            this._flexH.SetCol("NO_BIZAREA", "사업자NO", 100, false, typeof(string));
            this._flexH.SetCol("NM_TAX", "과세구분", 100);
            this._flexH.SetCol("AM_K", "공급가액", 100, 17, true, typeof(decimal), FormatTpType.MONEY);
            this._flexH.SetCol("AM_ADPAY", "선지급금액", 100, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexH.SetCol("VAT_TAX", "부가세", 100, 17, true, typeof(decimal), FormatTpType.MONEY);
            this._flexH.SetCol("AM_TOTAL", "총금액", 100, 17, false, typeof(decimal), FormatTpType.MONEY);

            this._flexH.SetCol("CD_DOCU", "전표유형", 80, true);
            this._flexH.SetCol("CD_EXCH", "통화명", 80, false);
            this._flexH.SetCol("RT_EXCH", "환율", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flexH.SetCol("AM_EX", "금액(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("DT_PAY_PREARRANGED", "지급예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("DT_DUE", "만기일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("FG_PAYBILL", "지급구분(매입처)", 80, false, typeof(string));
            this._flexH.SetCol("FG_PAYMENT", "지급조건(발주)", 80, false, typeof(string));
            this._flexH.SetCol("DC_RMK", "비고", 200, true, typeof(string));
            this._flexH.SetCol("YN_JEONJA", "계산서발행형태", 100, true);
            this._flexH.SetCol("NO_IO", "입고번호", 200);

            this._flexH.Cols["TXT_USERDEF1"].Visible = false;
            this._flexH.Cols["NO_IO"].Visible = false;
            this._flexH.Cols["NO_BIZAREA"].Format = "###-##-#####";

            this._flexH.SetDummyColumn("CHK");

            this._flexH.SetStringFormatCol("NO_BIZAREA");
            this._flexH.VerifyNotNull = new string[] { "NO_BIZAREA", "CD_BIZAREA_TAX" };

            this._flexH.SettingVersion = "2.9.8";
            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        private void InitGridL()
        {
            // 2011.03.17_김호연 추가
            this._flexL.BeginSetting(1, 1, false);

            this._flexL.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);  //선택. 사용하게될지도....

            this._flexL.SetCol("NO_IV", "매입번호", false);
            this._flexL.SetCol("NO_IOLINE", "항번", 40);
            this._flexL.SetCol("NO_IO", "입고번호", false);
            this._flexL.SetCol("DT_IO", "입고일자", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexL.SetCol("NO_DSP", "순번", 40);
            this._flexL.SetCol("CD_ITEM_PARTNER", "매출처품번", 100); // ITEM CODE 필드 추가!!
            this._flexL.SetCol("NM_ITEM_PARTNER", "매출처품명", 120); // DESCRPTION 필드 추가!!
            this._flexL.SetCol("CD_ITEM", "품목코드", 60);
            this._flexL.SetCol("NM_ITEM", "품목명", 100);
            this._flexL.SetCol("QT_IV", "입고수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("QT_CLS", "관리수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("STND_ITEM", "규격", false);
            this._flexL.SetCol("PI_PARTNER", "주거래처코드", false);
            this._flexL.SetCol("PI_LN_PARTNER", "주거래처명", false);
            this._flexL.SetCol("GI_PARTNER", "납품처코드", false);
            this._flexL.SetCol("GI_LN_PARTNER", "납품처명", 150);

            if (bStandard)
            {
                this._flexL.SetCol("UM_WEIGHT", "중량단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                this._flexL.SetCol("TOT_WEIGHT", "총중량", 100, false, typeof(decimal));

                this._flexL.Cols["TOT_WEIGHT"].Format = "###,###,###.#";
            }

            this._flexL.SetCol("UM_EX", "단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexL.SetCol("AM_EX", "금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("UM", "원화단가", 100, false, typeof(decimal), FormatTpType.UNIT_COST);
            this._flexL.SetCol("AM_IV", "원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flexL.SetCol("AM_ADPAY", "선지급금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flexL.SetCol("VAT_IV", "부가세", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flexL.SetCol("TP_UM_TAX", "부가세여부", 70, false);
            this._flexL.SetCol("AM_TOTAL", "총금액", 100, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexL.SetCol("FG_TPPURCHASE", "매입형태", 150);

            if (Config.MA_ENV.프로젝트사용)
                this._flexL.SetCol("SEQ_PROJECT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 항번" : "프로젝트항번", 120, false, typeof(decimal));

            if (Config.MA_ENV.PJT형여부 == "Y")
            {
                this._flexL.SetCol("CD_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 코드" : "프로젝트 품목코드", 140, false, typeof(string));
                this._flexL.SetCol("NM_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 명" : "프로젝트 품목명", 140, false, typeof(string));
                this._flexL.SetCol("PJT_ITEM_STND", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 규격" : "프로젝트 품목규격", 140, false, typeof(string));
                this._flexL.SetCol("NO_WBS", "WBS번호", 140, false, typeof(string));
                this._flexL.SetCol("NO_CBS", "CBS번호", 140, false, typeof(string));
                this._flexL.SetCol("CD_ACTIVITY", "ACTIVITY 코드", 140, false, typeof(string));
                this._flexL.SetCol("NM_ACTIVITY", "ACTIVITY", 140, false, typeof(string));
                this._flexL.SetCol("CD_COST", "원가코드", 140, false, typeof(string));
                this._flexL.SetCol("NM_COST", "원가명", 140, false, typeof(string));
            }
            this._flexL.SetCol("CD_PJT", "프로젝트코드", 150);
            this._flexL.SetCol("NM_PROJECT", "프로젝트명", 150);

            this._flexL.Cols["PI_PARTNER"].Visible = false;
            this._flexL.Cols["PI_LN_PARTNER"].Visible = false;
            this._flexL.Cols["GI_PARTNER"].Visible = false;
            this._flexL.Cols["GI_LN_PARTNER"].Visible = false;

            this._flexL.SetDummyColumn("S");
            this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            if (Config.MA_ENV.YN_UNIT == "Y")
                this._flexL.SetExceptSumCol("RT_EXCH", "UM_EX", "SEQ_PROJECT");
            else
                this._flexL.SetExceptSumCol("RT_EXCH", "UM_EX");

            if (Config.MA_ENV.YN_UNIT == "Y" && bStandard)
                this._flexH.SetExceptSumCol("RT_EXCH", "UM_EX", "SEQ_PROJECT", "UM_WEIGHT");
            else if (Config.MA_ENV.YN_UNIT == "Y" && !bStandard)
                this._flexH.SetExceptSumCol("RT_EXCH", "UM_EX", "SEQ_PROJECT");
            else if (Config.MA_ENV.YN_UNIT != "Y" && bStandard)
                this._flexH.SetExceptSumCol("RT_EXCH", "UM_EX", "UM_WEIGHT");
            else
                this._flexH.SetExceptSumCol("RT_EXCH", "UM_EX");


            if (Config.MA_ENV.YN_UNIT == "Y")
                this._flexL.VerifyNotNull = new string[] { "SEQ_PROJECT" };

            this._flexL.EnabledHeaderCheck = true;
            this._flexL.SettingVersion = "1.3.8";
        }

        private void InitEvent()
        {
            this.dtp처리일자.CalendarClosed += new EventHandler(this.OnControl_CalendarClosed);
            this.dtp처리일자.Validated += new EventHandler(this.OnControl_Validated);
            this.btn매입형태변경.Click += new EventHandler(this.btn매입형태변경_Click);
            this.btn매입관리.Click += new EventHandler(this.btn매입관리_Click);
            this.btn부가세사업장변경.Click += new EventHandler(this.btn부가세사업장변경_Click);
            this.btn입고적용.Click += new EventHandler(this.btn입고적용_Click);
            this.cbo거래구분.SelectionChangeCommitted += new EventHandler(this.cbo거래구분_SelectionChangeCommitted);

            this._flexH.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);
            this._flexH.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);
            this._flexH.StartEdit += new RowColEventHandler(this._flexH_StartEdit);
            this._flexH.CheckHeaderClick += new EventHandler(this._flex_CheckHeaderClick);
            this._flexH.AfterEdit += new RowColEventHandler(this._flex_AfterEdit);
            this._flexL.CheckHeaderClick += new EventHandler(this._flex_CheckHeaderClick);
            this._flexL.StartEdit += new RowColEventHandler(this._flexL_StartEdit);
            this._flexL.ValidateEdit += new ValidateEditEventHandler(this._flexL_ValidateEdit);
        }
        #endregion

        #region ♣ 메인버튼 이벤트

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarAddButtonClicked(sender, e);

                this.lbl거래구분.Focus();
                
                this.rbtn계산서처리건별.Enabled = true;
                this.rbtn계산서처리일괄.Enabled = true;
                this.btn입고적용.Enabled = true;

                this.SetControlEnabled(true);
                
                this._dt_Line.Clear();
                
                this._flexH.AllowEditing = true;
                this._flexH.DataTable.Clear();

                if (_flexL.HasNormalRow) _flexL.DataTable.Clear();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }

        }
        
        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSaveButtonClicked(sender, e);

                if (!BeforeSave()) return;

                if (MsgAndSave(PageActionMode.Save)) this.ShowMessage(PageResultMode.SaveGood);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

		protected override bool SaveData()
        {
            if (!base.SaveData() || !this.Verify()) return false;

            if (ctx담당자.IsEmpty())
            {
                this.ShowMessage("담당자는 필수입력 항목 입니다.");
                this.ctx담당자.Focus();
                return false;
            }

            InDataHeadValue();

            DataRow[] rows = (System.Data.DataRow[])this.GetSeq(this.LoginInfo.CompanyCode, "PU", "09", dtp처리일자.Text.Substring(0, 6), _flexH.DataTable.Rows.Count);

            for (int i = 0; i < _flexH.DataTable.Rows.Count; i++)
            {
                string str_no_docu = "";

                if(D.GetString(_flexH.DataTable.Rows[i]["NO_IV"]) == "")
                    str_no_docu = rows[i]["DOCU_NO"].ToString();
                else 
                   str_no_docu = D.GetString(_flexH.DataTable.Rows[i]["NO_IV"]);

                _flexH.DataTable.Rows[i]["NO_IV"] = str_no_docu;

                DataRow[] ldr_temp = _dt_Line.Select("NO_TEMP='" + _flexH.DataTable.Rows[i]["NO_TEMP"].ToString() + "'");
                DataRow[] dr_line = _flexL.DataTable.Select("NO_TEMP='" + _flexH.DataTable.Rows[i]["NO_TEMP"].ToString() + "'");
                for (int j = 0; j < ldr_temp.Length; j++)
                {
                    ldr_temp[j].BeginEdit();
                    ldr_temp[j]["NO_IV"] = str_no_docu;
                    ldr_temp[j]["NO_IVLINE"] = j + 1;
                    ldr_temp[j].EndEdit();

                    dr_line[j]["NO_IV"] = str_no_docu;
                    ldr_temp[j]["NO_IVLINE"] = j + 1;
                }
            }

            object obj = _biz.Save(_flexH.DataTable, _dt_Line, ds_Ty1, _flexH[_flexH.Row, "CD_CC"].ToString(), cbo거래구분.SelectedValue.ToString(), dtp처리일자.Text);

            for ( int i = 0 ; i < _flexH.DataTable.Rows.Count ; i++ )
            {
                if ( !_biz.부가세변경( _flexH.DataTable.Rows [i] ["NO_IV"].ToString() ) )
                    return false;
            }

            ResultData[] result = (ResultData[])obj;
                                                                 
            
           // _flexL.Binding = _dt_Line;
            _flexL.Redraw = true;                                                                                                  
            if (result[0].Result && result[1].Result)
            {
                btn입고적용.Enabled = false;
                _flexH.AllowEditing = false;
                _flexH.AcceptChanges();
                _flexL.AcceptChanges();

                return true;
            }

            SetSaveFalse();
            return false;
        }

        #endregion          

        #region ♣ 그리드 이벤트 / 메서드

        private void _flex_ValidateEdit(object sender, C1.Win.C1FlexGrid.ValidateEditEventArgs e)
        {
            try
            {

                Decimal oldValue = D.GetDecimal(((FlexGrid)sender).GetData(e.Row, e.Col).ToString());
                Decimal newValue = D.GetDecimal(((FlexGrid)sender).EditData);
                Decimal vsoldValue = (oldValue + 99);
                Decimal vmoldValue = (oldValue - 99);
                Decimal roopValue = Math.Abs(oldValue - newValue);
                CommonFunction objFunc = new CommonFunction();
                


                if (D.GetString(_flexH.Cols[e.Col].Name) == "AM_K" && (newValue > vsoldValue || newValue < vmoldValue))
                {
                    #region -> 매입등록 일정 범위에 대한 제한 김헌섭대리요청

                                //1. 외화 건에 대한 매입 시, 환율 * 외화금액을 해도 
                                //원화금액을 맞출 수 없는 경우가 있습니다. 
                                //그래서 원화금액에 대해서 수정을 할 수 있도록 함.

                                //2. 범위 제한
                                //환율 * 외화금액으로 원화금액을 못 맞추는 경우를 위해서 특정 범위를 열어줌.

                                //매입등록 상단에서 원화금액을 보정하게 되면
                                //시스템에서 임의의 품목에 해당 금액을 보정해주기 때문에...
                                //매입하는 원재료/부재료/저장품/상품에 대한 매입원가가 틀려질 수 있으며
                                //이로 인해서 제품의 원가가 달라질 수 있음.

                                //또한 사용자가 실수로 금액을 많이 잘못 넣을 수 있기 때문에 사용자에 대한 실수를
                                //보완하기 위해서 프로그램에 체크 구문을 추가한 것임.


                                //체크를 1,000원으로 범위를 늘려도 상관없지만...일정 범위안에서만 바꿀 수 있도록 제한이 필요함.
                                //이전 M3에서도 사용자가 잘못 바꾸고...시스템의 오류로 접수를 하는 경우가 빈번했음..
                                //그리고 품목별 매입원가가 달라질 수 있기 떄문에..해당 부분은 담당자가 분명히 인지해야 하는 부분임.
                    #endregion
                    ShowMessage("수정가능금액은 100원 범위 입니다.");

                    _flexH[e.Row, "AM_K"] = oldValue.ToString();

                    return;
                }

                string ColName = ((FlexGrid)sender).Cols[e.Col].Name;


                if (_flexH.AllowEditing)
                {			//	System.Diagnostics.Debugger.Break();
                    if (_flexH.GetData(e.Row, e.Col).ToString() != _flexH.EditData)
                    {
                        switch (_flexH.Cols[e.Col].Name)
                        {
                            case "AM_K":
                                //ChangeAM_K(System.Double.Parse(_flex.EditData));
                                //ChangeAM_K(D.GetDecimal(_flex.EditData)); //위치이동 부가세 계산 이후로.. 
                                
                                DataRow[] dr_flexL = _flexL.DataTable.Select(" NO_TEMP ='" + D.GetString(_flexH["NO_TEMP"] + "'"));
                                _flexH["VAT_TAX"] = Unit.원화금액(DataDictionaryTypes.PU,UDecimal.Getdivision(D.GetDecimal(_flexH.EditData), D.GetDecimal(dr_flexL[0]["TAX_RATE"])));

                                ChangeAM_K(D.GetDecimal(_flexH.EditData));

                                int i = 0;
                                while (i < roopValue)
                                {
                                    for (int j = 1; j < _flexL.Rows.Count; j++)
                                    {

                                        if (_flexL.DataTable.Select("S = 'Y'").Length == 0)
                                        {
                                            i = D.GetInt(roopValue) + 1; //roopValue보다 크게만들기위해 +1이상만 해주면됩니다.
                                        }

                                        if (D.GetString(_flexL[j, "S"]) != "Y") continue; 
                                        

                                        if (i == roopValue) break;

                                        if ((oldValue - newValue) > 0)
                                        {
                                            _flexL[j, "AM_IV"] = D.GetInt(_flexL[j, "AM_IV"]) - 1;

                                            //DataRow[] ldr_temp = _dt_Line.Select("NO_IOLINE='" + _flexL.Rows[j]["NO_IOLINE"].ToString() + "'");
                                            //ldr_temp[ldr_temp.Length - 1]["AM_EX"] = D.GetInt(_flexL[j, "AM_IV"]);
                                        }
                                        else
                                        {
                                            _flexL[j, "AM_IV"] = D.GetInt(_flexL[j, "AM_IV"]) + 1;

                                            //DataRow[] ldr_temp = _dt_Line.Select("NO_IOLINE='" + _flexL.Rows[j]["NO_IOLINE"].ToString() + "'");
                                            //ldr_temp[ldr_temp.Length - 1]["AM_EX"] = D.GetInt(_flexL[j, "AM_IV"]);
                                        }
                                        i++;
                                    }
                                }


                                break;
                            case "VAT_TAX":
                                _flexH[_flexH.Row, "AM_TOTAL"] = D.GetDecimal(_flexH.EditData) + (Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flexH[_flexH.Row, "AM_K"].ToString().Trim())) - Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flexH[_flexH.Row, "AM_ADPAY"].ToString().Trim())));
                                //ChangeVAT_K(System.Double.Parse(_flex.EditData));
                                //ChangeVAT_K(D.GetDecimal(_flex.EditData));
                                break;

                            //case "FG_PAYBILL":
                            //    string dt_due = string.Empty;
                            //    string dt_pay = string.Empty;

                            //    if (_지급관리통제설정 == "Y")
                            //    {
                            //        decimal ldb_amk = D.GetDecimal(_flexH["AM_K"]);
                            //        dt_pay = ComFunc.만기예정일자(dtp처리일자.Text, ldb_amk, D.GetString(_flexH.EditData), "1");      
                            //        // 만기예정일
                            //        if (dt_pay != string.Empty)
                            //        {
                            //            _flexH[_flexH.Row, "DT_PAY_PREARRANGED"] = dt_pay;
                            //            _flexH[_flexH.Row, "DT_DUE"] = dt_pay;
                            //        }

                            //        //어음 만기일(구분이 어음이 아닐경우 Empty가 return됨)
                            //        dt_due = ComFunc.만기예정일자(dtp처리일자.Text, ldb_amk, D.GetString(_flexH.EditData), "2");
                            //        if (dt_due != string.Empty)
                            //            _flexH[_flexH.Row, "DT_DUE"] = dt_due;

                            //    }

                            //    if (_지급관리통제설정 == "N" || dt_pay == string.Empty || dt_due == string.Empty)
                            //    {
                            //        if (_지급예정일통제설정 == "000")
                            //        {
                            //            string str_temp = objFunc.DateAdd(dtp처리일자.Text, "D", Convert.ToInt16(_flexH["DT_PAY_DAY"].ToString()));
                            //            if (dt_pay == string.Empty && Global.MainFrame.ServerKeyCommon != "TRIGEM" ) ///삼보는 이기능을 사용하고싶지않다고함
                            //                _flexH["DT_PAY_PREARRANGED"] = str_temp;

                            //            if (dt_due == string.Empty)
                            //                _flexH["DT_DUE"] = _flexH["DT_PAY_PREARRANGED"];
                            //        }
                            //        else 
                            //        {
                            //            string str_temp = objFunc.DateAdd(dtp처리일자.Text, "M", Convert.ToInt16(D.GetDecimal(_flexH["TP_PAY_DD"])));
                            //            str_temp = str_temp.Substring(0, 6) + string.Format("{0:00}", D.GetDecimal(_flexH["DT_PAY_DD"]));
                            //            if (D.GetDecimal(str_temp) <= D.GetDecimal(dtp처리일자.Text))
                            //                str_temp = dtp처리일자.Text;
                            //            else
                            //            {
                            //                string str_lastday = objFunc.GetLastDayDateText(dtp처리일자.Text);
                            //                if (D.GetDecimal(str_temp) > D.GetDecimal(str_lastday))
                            //                    str_temp = str_lastday;
                            //            }

                            //            _flexH[_flexH.Row, "DT_PAY_PREARRANGED"] = str_temp;
                            //            _flexH[_flexH.Row, "DT_DUE"] = _flexH["DT_PAY_PREARRANGED"];
                            //        }

                            //    }
                            //    break;

                            //case "FG_PAYMENT":
                            //    if (Global.MainFrame.ServerKeyCommon == "TRIGEM" || Global.MainFrame.ServerKeyCommon == "DZSQL" || Global.MainFrame.ServerKeyCommon == "SQL_")
                            //    {
                            //        DataTable dt = MF.GetCode("PU_C000014");
                            //        if (dt == null || dt.Rows.Count == 0) break;

                            //        string newval = D.GetString(((FlexGrid)sender).EditData);
                            //        DataRow[] dRow = dt.Select("CODE = '" + newval + "'");
                            //        if (dRow.Length == 0) break;

                            //        int day = D.GetInt(dRow[0]["CD_FLAG1"]);
                            //        DateTime now = DateTime.Now;
                            //        string date = dtp처리일자.Text.Substring(0, 4) + "-" + dtp처리일자.Text.Substring(4, 2) + "-" + dtp처리일자.Text.Substring(6, 2);//.ToShortDateString();
                                    
                            //        now = DateTime.Parse(date);
                                    
                            //        _flexH["DT_PAY_PREARRANGED"] = now.AddDays(day).ToString("yyyyMMdd");
                            //        // 만기예정일
                            //        _flexH["DT_DUE"] = _flexH["DT_PAY_PREARRANGED"];

                            //    }
                            //    break;
                            //case "DT_PAY_PREARRANGED":
                            //    // 만기예정일
                            //    if (D.GetString(_flexH.EditData) != string.Empty)
                            //    {
                            //        _flexH[_flexH.Row, "DT_DUE"] = D.GetString(_flexH.EditData);
                            //    }
                            //    break;
                        }
                    }
                }


            }
            catch
            {
            }

        }

        private void _flex_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (!_flexH.IsBindingEnd || !_flexH.HasNormalRow) return;
                
                string str_cd_partner = D.GetString(_flexH["CD_PARTNER"]);
                string str_no_lc = D.GetString(_flexH["NO_LC"]);
                string fg_tax = D.GetString(_flexH["FG_TAX"]);
                string Filter = string.Empty;                
                
                Filter = "NO_TEMP = '" + D.GetString(_flexH["NO_TEMP"]) + "'";

                _flexL.RowFilter = Filter;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flex_AfterEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (_flexH[_flexH.Row, "CHK"].ToString() == "Y") //클릭하는 순간은 N이므로
                {
                    for (int i = _flexL.Rows.Fixed; i < _flexL.Rows.Count; i++)
                        _flexL.SetCellCheck(i, 1, CheckEnum.Checked);
                }
                else
                {
                    for (int i = _flexL.Rows.Fixed; i < _flexL.Rows.Count; i++)
                        _flexL.SetCellCheck(i, 1, CheckEnum.Unchecked);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flexH_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {

                switch (_flexH.Cols[e.Col].Name)
                {
                    case "TXT_USERDEF1":
                        if (!_flexH["NM_TAX"].Equals("현금영수증"))
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

        private void _flexL_StartEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            try
            {
                if (_flexL.Cols[e.Col].Name == "S" && D.GetString(_flexH[_flexH.Row, "CHK"]) == "Y")
                {
                    // e.Cancel = true;
                    if (D.GetString(_flexL[_flexL.Row, "S"]) == "Y") //edit 시작점이므로 n으로 변경하려면 기존값은 y
                        _flexH[_flexH.Row, "CHK"] = "N";
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flexL_ValidateEdit(object sender, C1.Win.C1FlexGrid.ValidateEditEventArgs e)
        {
            try
            {
                if (D.GetString(_flexL.GetData(e.Row, e.Col)) != D.GetString(_flexL.EditData))
                {
                    switch (_flexL.Cols[e.Col].Name)
                    {
                        case "S":
                            Boolean bFlag = false;
                            for (int row = _flexL.Rows.Fixed; row < _flexL.DataView.Count + _flexL.Rows.Fixed; row++)
                            {
                                if (_flexL.EditData != "Y" || (e.Row != row && _flexL[row, "S"].Equals("N")))
                                {
                                    bFlag = true;
                                    break;
                                }
                            }

                            if (!bFlag)
                                _flexH["CHK"] = "Y";
                            else
                                _flexH["CHK"] = "N";

                            break;
                        

                    }
                }
            }
            catch
            {
            }
        }
        
        #endregion

        #region ♣ 버튼클릭(매입관리/입고적용)

        private void btn매입관리_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (this.IsExistPage("P_CZ_PU_IV_MNG", false))
                    this.UnLoadPage("P_CZ_PU_IV_MNG", false);

                this.LoadPageFrom("P_CZ_PU_IV_MNG", this.DD("매입관리(딘텍)"), Grant, new object[] { new object[] { this.ctx담당자.CodeValue,
                                                                                                                    this.ctx담당자.CodeName,
                                                                                                                    this.cbo거래구분.SelectedValue,
                                                                                                                    this.dtp처리일자.Text } });
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn입고적용_Click(object sender, System.EventArgs e)
        { 
            try
            {
                if (!FieldCheck()) return;
                
                P_CZ_PU_IV_REG_SUB subDlg = new P_CZ_PU_IV_REG_SUB(this.cbo거래구분.SelectedValue.ToString(), this.dtp처리일자.Text);

                if (subDlg.ShowDialog(this) == DialogResult.OK)
                {
                    SettingDiviedData(subDlg.리턴데이터, rbtn계산서처리건별.Checked);
                    PageControlEnalbed(false);
                }

                if (this.cbo거래구분.SelectedValue.ToString() == "005")
                    this.dtp처리일자.Text = this._flexL.DataTable.Compute("MAX(DT_IO)", string.Empty).ToString();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void cbo거래구분_SelectionChangeCommitted(object sender, System.EventArgs e)
        { 
            try
            {
                if(cbo거래구분.SelectedValue.ToString() == "001")
                    this._flexH.VerifyNotNull = new string[] { "NO_BIZAREA", "CD_BIZAREA_TAX" };
                else
                    this._flexH.VerifyNotNull = new string[] { "CD_BIZAREA_TAX" };
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region ♣ 컨트롤 이벤트

        private void Control_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            if (e.DialogResult == DialogResult.OK)
            {
                System.Data.DataRow[] rows = e.HelpReturn.Rows;
                switch (e.HelpID)
                {
                    case Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB:
                        _cddept = rows[0]["CD_DEPT"].ToString();
                        break;
                    default:
                        break;
                }
            }

            Control CTRL = (Control)sender;

            if (CTRL.Name == "bp_CD_BIZAREA")
            {
                DataTable dt_biz = _biz.getCd_Pc(ctx사업장.CodeValue);

                if (dt_biz != null || dt_biz.Rows.Count > 0)
                {
                    ctx부가세사업장.CodeValue = D.GetString(dt_biz.Rows[0]["CD_BIZAREA"]);
                    ctx부가세사업장.CodeName = D.GetString(dt_biz.Rows[0]["NM_BIZAREA"]);
                }
            }

        }

        private void OnControl_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!((DatePicker)sender).Modified) return;

                if (!((DatePicker)sender).IsValidated)
                {
                    this.ShowMessage("WK1_003");
                    ((DatePicker)sender).Focus();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void OnControl_CalendarClosed(object sender, EventArgs e)
        {
            try
            {
                OnControl_Validated(sender, e);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        #region ♣ 기타메소드

        private void SetControlEnabled(bool pb_enabled)
        {
            this.ctx사업장.Enabled = pb_enabled;//cbo_CD_BIZAREA.Enabled = pb_enabled;
            this.ctx담당자.Enabled = pb_enabled;
            this.cbo거래구분.Enabled = pb_enabled;
            this.cbo매입형태.Enabled = pb_enabled;
            this.dtp처리일자.Enabled = pb_enabled;
        }

        private void ChangeAM_K(decimal pdb_amk)
        {
            try
            {
                decimal ldb_kwN = 0;
                string ls_NO_TEMP = _flexH[_flexH.Row, "NO_TEMP"].ToString();
                string ls_filter = "";

                ls_filter = "NO_TEMP= '" + ls_NO_TEMP + "'";
                ldb_kwN = (System.Decimal)_dt_Line.Compute("SUM(AM_IV)", ls_filter);
                decimal gab = pdb_amk - ldb_kwN;//  ( ldb_kwN - ldb_kwY );
                DataRow[] ldr_rows = _dt_Line.Select(ls_filter);

                if (ldr_rows != null && ldr_rows.Length > 0) //화면에서는 보여지지 않습니다. dt_LINE과 그리드는 분리되어있습니다.
                {
                    ldr_rows[ldr_rows.Length - 1].BeginEdit();
                    //ldr_rows[ldr_rows.Length - 1]["AM_IV"] = System.Math.Floor(System.Double.Parse(ldr_rows[ldr_rows.Length - 1]["AM_IV"].ToString()) + gab);
                    ldr_rows[ldr_rows.Length - 1]["AM_IV"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(ldr_rows[ldr_rows.Length - 1]["AM_IV"].ToString()) + gab);
                    ldr_rows[ldr_rows.Length - 1]["UM"] = System.Double.Parse(ldr_rows[ldr_rows.Length - 1]["AM_IV"].ToString()) / System.Double.Parse(ldr_rows[ldr_rows.Length - 1]["QT_IV"].ToString());
                    //ldr_rows[ldr_rows.Length - 1]["AM_EX"] = (System.Double.Parse(ldr_rows[ldr_rows.Length - 1]["AM_IV"].ToString())) / System.Double.Parse(ldr_rows[ldr_rows.Length - 1]["RT_EXCH"].ToString());
                    //ldr_rows[ldr_rows.Length - 1]["UM_EX"] = (System.Double.Parse(ldr_rows[ldr_rows.Length - 1]["UM"].ToString().Trim())) / System.Double.Parse(ldr_rows[ldr_rows.Length - 1]["RT_EXCH"].ToString());
                    ldr_rows[ldr_rows.Length - 1].EndEdit();
                    //_flex[_flex.Row, "AM_TOTAL"] = (pdb_amk) + System.Double.Parse(_flex[_flex.Row, "VAT_TAX"].ToString().Trim());
                    _flexH[_flexH.Row, "AM_TOTAL"] = ((pdb_amk) - Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flexH[_flexH.Row, "AM_ADPAY"].ToString().Trim()))) + Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flexH[_flexH.Row, "VAT_TAX"].ToString().Trim()));
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void ChangeVAT_K(decimal pdb_vatk)
        {
            try
            {
                decimal ldb_kwN = 0;
                string ls_NO_TEMP = _flexH[_flexH.Row, "NO_TEMP"].ToString();
                string ls_filter = "";

                ls_filter = "NO_TEMP= '" + ls_NO_TEMP + "'";
                ldb_kwN = (System.Decimal)_dt_Line.Compute("SUM(VAT_IV)", ls_filter);
                decimal gab = pdb_vatk - ldb_kwN;// ( ldb_kwN - ldb_kwY );

                DataRow[] ldr_rows = _dt_Line.Select(ls_filter);
                if (ldr_rows != null && ldr_rows.Length > 0)
                {
                    ldr_rows[ldr_rows.Length - 1].BeginEdit();
                    //ldr_rows[ldr_rows.Length - 1]["VAT_IV"] = System.Math.Floor(System.Double.Parse(ldr_rows[ldr_rows.Length - 1]["VAT_IV"].ToString()) + gab);
                    ldr_rows[ldr_rows.Length - 1]["VAT_IV"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(ldr_rows[ldr_rows.Length - 1]["VAT_IV"].ToString()) + gab);
                    ldr_rows[ldr_rows.Length - 1].EndEdit();
                    //_flex[_flex.Row, "AM_TOTAL"] = (pdb_vatk) + System.Math.Floor(System.Double.Parse(_flex[_flex.Row, "AM_K"].ToString().Trim()));
                    _flexH[_flexH.Row, "AM_TOTAL"] = (pdb_vatk) + (Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flexH[_flexH.Row, "AM_K"].ToString().Trim())) - Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flexH[_flexH.Row, "AM_ADPAY"].ToString().Trim())));
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void InDataHeadValue()
        {
            try
            {

                DataRow newrow;

                ds_Ty1 = _biz.GetHeadTable();
                ds_Ty1.Clear();

                newrow = ds_Ty1.NewRow();
               // newrow["CD_BIZAREA"] = cbo_CD_BIZAREA.SelectedValue.ToString();
                newrow["CD_COMPANY"] = this.MainFrameInterface.LoginInfo.CompanyCode;
                //newrow["CD_BIZAREA_BIZ"] = cbo_CD_BIZAREA.SelectedValue.ToString();

                ds_Ty1.Rows.Add(newrow);

                ds_Ty1.BeginInit();
                DataRow ldr_row = ds_Ty1.Rows[0];

                string ls_nobizarea = "";
                DataRow[] ldrs_row = _dsCombo.Tables[2].Select("CODE = '" + ctx사업장.CodeValue + "'");
                if (ldrs_row != null && ldrs_row.Length > 0)
                {
                    ls_nobizarea = ldrs_row[0]["NO_BIZAREA"].ToString();
                }
                
                ldr_row["NO_BIZAREA"] = ls_nobizarea;
                ldr_row["DT_PROCESS"] = dtp처리일자.Text.ToString();
                ldr_row["TP_SUMTAX"] = "S";
                ldr_row["CD_DEPT"] = _cddept;
                ldr_row["NO_EMP"] = ctx담당자.CodeValue.ToString();
                ldr_row["ID_USER"] = this.LoginInfo.UserID;
                ldr_row["YN_PURSUB"] = "N";
                ldr_row["MODULE"] = "PU";

                ldr_row["FG_FGTAXP"] = "001";

                if (rbtn계산서처리건별.Checked)
                {
                    ldr_row["FG_FGTAXP"] = "002";
                }

                ds_Ty1.EndInit();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }

        }

        private void SetSaveFalse()
        {
            try
            {
                for (int i = 0; i < _flexH.DataTable.Rows.Count; i++)
                {
                    _flexH.DataTable.Rows[i].BeginEdit();
                    _flexH.DataTable.Rows[i]["NO_IV"] = "";
                    _flexH.DataTable.Rows[i].EndEdit();
                }
            }
            catch(Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private bool FieldCheck()
        {
            //if (tb_NO_EMP.CodeValue.ToString() == "")
            //{
            //    this.ShowMessage("WK1_004", lb_NO_EMP.Text);
            //    //	Duzon.Common.Controls.MessageBoxEx.Show(lb_NO_EMP.Text +  this.GetMessageDictionaryItem("CM_M000002"),this.PageName);
            //    tb_NO_EMP.Focus();
            //    return false;
            //}

            if (ctx사업장.CodeValue.ToString() == string.Empty)
            {
                this.ShowMessage("WK1_004", lbl사업장.Text);
                //	Duzon.Common.Controls.MessageBoxEx.Show(lb_NO_EMP.Text +  this.GetMessageDictionaryItem("CM_M000002"),this.PageName);
                ctx사업장.Focus();
                return false;
            }

            return true;
        }

        private void SettingDiviedData(DataTable pdt_Line, bool is_individual)
        {
            try
            {
                // 받아온것 없으면..
                if (pdt_Line == null || pdt_Line.Rows.Count <= 0)
                    return;

                // 기존에 선택된 것이 없으면 
                if (_dt_Line == null || _dt_Line.Rows.Count <= 0)
                {
                    _dt_Line = pdt_Line.Copy();
                }
                else
                {
                    for (int i = 0; i < _dt_Line.Rows.Count; i++)
                        _dt_Line.Rows[i]["NO_TEMP"] = "";
                    for (int i = 0; i < pdt_Line.Rows.Count; i++)
                        _dt_Line.ImportRow(pdt_Line.Rows[i]);
                }

                if (!_dt_Line.Columns.Contains("NUM_USERDEF1"))
                {
                    _dt_Line.Columns.Add("NUM_USERDEF1", typeof(decimal));
                }

                ///////////////////////////////////////////////////////////////
                ///	헤더 생성 
                /// 
                // 헤더 CLEAR
                _flexH.DataTable.Clear();
                _flexL.Binding = null;
                _flexH.Redraw = false;
                CommonFunction objFunc = new CommonFunction();
                String str_temp = String.Empty;

                MsgControl.ShowMsg("적용 데이터를 집계중입니다. \r\n잠시만 기다려주세요!");

                if (!_dt_Line.Columns.Contains("S"))
                {
                    _dt_Line.Columns.Add("S", typeof(string));
                    foreach (DataRow dr in _dt_Line.Rows) //각도움창의 체크버튼 기준이 YN, TF 이두개의값으로 나누어져있어서 여기서 들어올때 통일해서 들어오도록한다. 원래는 프로시져에서 부터 고치는게 맞지만...
                    {
                        dr["S"] = "Y";
                    }
                }

                //bool bContain = pdt_Line.Columns.Contains("CD_PARTNER_PJT");
                if (is_individual)
                {
                    #region 건별
                    long ll_temp = 1;
                    DataRow[] ldr_temp;
                    for (int i = 0; i < _dt_Line.Rows.Count; i++)
                    {
                        // 기존에 생성되지 않았으면
                        if (_dt_Line.Rows[i]["NO_TEMP"].ToString().Trim() == "")
                        {
                            ldr_temp = _dt_Line.Select("NO_IO='" + _dt_Line.Rows[i]["NO_IO"].ToString() + "'");

                            _flexH.Rows.Add();
                            _flexH.Row = _flexH.Rows.Count - 1;

                            _flexH[_flexH.Row, "CHK"] = "Y";
                            _flexH[_flexH.Row, "NO_IO"] = _dt_Line.Rows[i]["NO_IO"].ToString();
                            _flexH[_flexH.Row, "NO_IV"] = "";
                            _flexH[_flexH.Row, "CD_PARTNER"] = _dt_Line.Rows[i]["CD_PARTNER"].ToString();
                            _flexH[_flexH.Row, "LN_PARTNER"] = _dt_Line.Rows[i]["LN_PARTNER"].ToString();
                            _flexH[_flexH.Row, "NO_BIZAREA"] = _dt_Line.Rows[i]["NO_COMPANY"].ToString();
                            _flexH[_flexH.Row, "FG_TAX"] = _dt_Line.Rows[i]["FG_TAX"].ToString();
                            _flexH[_flexH.Row, "NM_TAX"] = _dt_Line.Rows[i]["NM_TAX"].ToString();
                            _flexH[_flexH.Row, "NO_TEMP"] = ll_temp.ToString();
                            _flexH[_flexH.Row, "FG_TRANS"] = cbo거래구분.SelectedValue.ToString();//YN_RETURN
                            _flexH[_flexH.Row, "CD_CC"] = _dt_Line.Rows[i]["CD_CC"].ToString();   //CD_CC 값 추가

                            //_flex[_flex.Row, "CD_DOCU"] = cd_docu;                                //2009.11.27 전표유형추가
                            _flexH[_flexH.Row, "FG_TPPURCHASE"] = _dt_Line.Rows[i]["FG_TPPURCHASE"].ToString();

                            _flexH[_flexH.Row, "CD_BIZAREA"] = ctx사업장.CodeValue;  // 사업장(2009.12.15 추가)
                            _flexH[_flexH.Row, "CD_BIZAREA_TAX"] = ctx부가세사업장.CodeValue;  // 부가세사업장(2009.12.15 추가)
                            _flexH[_flexH.Row, "NM_BIZAREA"] = ctx사업장.CodeName;  // 사업장(2009.12.15 추가)
                            _flexH[_flexH.Row, "NM_BIZAREA_TAX"] = ctx부가세사업장.CodeName;  // 부가세사업장(2009.12.15 추가)

                            //	_flex[_flex.Row,"YN_RETURN"] = _dt_Line.Rows[i]["YN_RETURN"].ToString();

                            decimal ldb_amk = 0;
                            decimal ldb_amkVat = 0;
                            decimal ldb_amEx = 0;
                            decimal ldb_vat = 0;
                            decimal ldb_adpay = 0;

                            for (int j = 0; j < ldr_temp.Length; j++)
                            {
                                ldr_temp[j]["NO_TEMP"] = ll_temp.ToString();
                                ldb_amk += D.GetDecimal(System.Double.Parse(ldr_temp[j]["AM_IV"].ToString()));

                                if (D.GetDecimal(ldr_temp[j]["TAX_RATE"]) > 0)
                                    ldb_amkVat += D.GetDecimal(D.GetDecimal(ldr_temp[j]["AM_IV"]));

                                ldb_amEx += D.GetDecimal(System.Double.Parse(ldr_temp[j]["AM_EX"].ToString()));
                                //ldb_vat += System.Math.Floor(System.Double.Parse(ldr_temp[j]["VAT_IV"].ToString()));
                                ldb_vat += Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(ldr_temp[j]["VAT_IV"].ToString()));
                                ldb_adpay += D.GetDecimal(System.Double.Parse(ldr_temp[j]["AM_ADPAY"].ToString()));
                            }

                            string cd_docu = Get_Type_Dcou(ldr_temp);
                            _flexH[_flexH.Row, "CD_DOCU"] = cd_docu;


                            // 과세 이고.. 거래처데이블의 TP_TAX ==001
                            if (_dt_Line.Rows[i]["TP_TAX"].ToString().Trim() == "001" &&
                                System.Double.Parse(_dt_Line.Rows[i]["TAX_RATE"].ToString().Trim()) > 0)
                            {
                                decimal ldb_VatRate = D.GetDecimal(System.Double.Parse(_dt_Line.Rows[i]["TAX_RATE"].ToString().Trim()));
                                //	double ldb_tempVat = System.Math.Floor(ldb_amk *ldb_VatRate * 0.01);

                                decimal ldb_tempVat = ldb_amkVat * ldb_VatRate * 0.01M;

                                if (ldb_tempVat < 0)
                                {
                                    //ldb_tempVat = System.Math.Ceiling(ldb_tempVat);
                                    ldb_tempVat = Unit.원화금액(DataDictionaryTypes.PU, ldb_tempVat);
                                }
                                //ldb_tempVat = System.Math.Floor(ldb_tempVat);
                                ldb_tempVat = Unit.원화금액(DataDictionaryTypes.PU, ldb_tempVat);
								//ldr_temp[ldr_temp.Length - 1]["VAT_IV"] = System.Math.Floor(System.Double.Parse(ldr_temp[ldr_temp.Length - 1]["VAT_IV"].ToString())) + (ldb_tempVat - ldb_vat);
								ldr_temp[ldr_temp.Length - 1]["VAT_IV"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(System.Double.Parse(ldr_temp[ldr_temp.Length - 1]["VAT_IV"].ToString()))) + (ldb_tempVat - ldb_vat);
								ldr_temp[ldr_temp.Length - 1]["AM_TOTAL"] = D.GetDecimal(ldr_temp[ldr_temp.Length - 1]["VAT_IV"]) + (D.GetDecimal(ldr_temp[ldr_temp.Length - 1]["AM_IV"]) - D.GetDecimal(ldr_temp[ldr_temp.Length - 1]["AM_ADPAY"]));
                                ldb_vat = ldb_tempVat;
                            }

                            // 실제 DB에 들어 가는 값
                            //_flex[_flex.Row, "AM_K"] = System.Math.Floor(ldb_amk);
                            _flexH[_flexH.Row, "AM_K"] = Unit.원화금액(DataDictionaryTypes.PU, ldb_amk);
                            _flexH[_flexH.Row, "AM_EX"] = ldb_amEx;
                            //_flex[_flex.Row, "VAT_TAX"] = System.Math.Floor(ldb_vat);
                            _flexH[_flexH.Row, "VAT_TAX"] = Unit.원화금액(DataDictionaryTypes.PU, ldb_vat);

                            //구매승인서 ,국내, LOCAL L/C 든 뭐든지 환종을 강제로 원화로 바꾸거나 하는작업을 해서는 안된다.
                            _flexH[_flexH.Row, "CD_EXCH"] = _dt_Line.Rows[i]["CD_EXCH"].ToString().Trim();
                            _flexH[_flexH.Row, "RT_EXCH"] = _flexH.CDecimal(_dt_Line.Rows[i]["RT_EXCH"]);
                            // _flex[_flex.Row, "AM_EX"] = Convert.ToDouble(_flex[_flex.Row, "AM_EX"]) + ldb_amEx;

                            if (Convert.ToDecimal(_dt_Line.Rows[i]["RT_EXCH"]) == 0 || _dt_Line.Rows[i]["RT_EXCH"] == null)
                                _dt_Line.Rows[i]["RT_EXCH"] = 1;

                            _flexH[_flexH.Row, "AM_K"] = ldb_amk; // System.Math.Floor(_flex.CDecimal(_flex[_flex.Row, "AM_EX"]) * _flex.CDecimal(_dt_Line.Rows[i]["RT_EXCH"]));
                            //_flex[_flex.Row, "AM_TOTAL"] = Convert.ToDouble(_flex[_flex.Row, "AM_K"]) + ldb_vat;
                            _flexH[_flexH.Row, "AM_ADPAY"] = ldb_adpay;
                            _flexH[_flexH.Row, "AM_TOTAL"] = (D.GetDecimal(_flexH[_flexH.Row, "AM_K"]) - ldb_adpay) + ldb_vat;

                            _flexH[_flexH.Row, "FG_PAYBILL"] = D.GetString(_dt_Line.Rows[i]["FG_PAYBILL"]);

                            #region 더존기본
                            //_flexH[_flexH.Row, "DT_PAY_DAY"] = Convert.ToInt16(_dt_Line.Rows[i]["DT_PAY_PREARRANGED"]);

                            //if (_지급관리통제설정 == "Y")
                            //{
                            //    string dt_due = ComFunc.만기예정일자(dtp처리일자.Text, ldb_amk, D.GetString(_dt_Line.Rows[i]["FG_PAYBILL"]), "1");
                            //    // 만기예정일
                            //    if (dt_due != string.Empty)
                            //        _flexH[_flexH.Row, "DT_PAY_PREARRANGED"] = dt_due;

                            //    //어음 만기일(구분이 어음이 아닐경우 Empty가 return됨)
                            //    dt_due = ComFunc.만기예정일자(dtp처리일자.Text, ldb_amk, D.GetString(_dt_Line.Rows[i]["FG_PAYBILL"]), "2");
                            //    if (dt_due != string.Empty)
                            //        _flexH[_flexH.Row, "DT_DUE"] = dt_due;

                            //}
                            //else
                            //{
                            //    if (_지급예정일통제설정 != "100")  // 물류쪽 지급예정일 사용
                            //    {
                            //        str_temp = objFunc.DateAdd(dtp처리일자.Text, "D", Convert.ToInt16(_flexH[_flexH.Row, "DT_PAY_DAY"]));
                            //        _flexH[_flexH.Row, "DT_PAY_PREARRANGED"] = str_temp;
                            //        _flexH[_flexH.Row, "DT_DUE"] = str_temp;
                            //    }
                            //    else
                            //    {
                            //        str_temp = objFunc.DateAdd(dtp처리일자.Text, "M", Convert.ToInt16(D.GetDecimal(_dt_Line.Rows[i]["TP_PAY_DD"])));
                            //        str_temp = str_temp.Substring(0, 6) + string.Format("{0:00}", D.GetDecimal(_dt_Line.Rows[i]["DT_PAY_DD"]));
                            //        if (D.GetDecimal(str_temp) <= D.GetDecimal(dtp처리일자.Text))
                            //            str_temp = dtp처리일자.Text;
                            //        else
                            //        {
                            //            string str_lastday = objFunc.GetLastDayDateText(dtp처리일자.Text);
                            //            if (D.GetDecimal(str_temp) > D.GetDecimal(str_lastday))
                            //                str_temp = str_lastday;
                            //        }

                            //        _flexH[_flexH.Row, "DT_PAY_PREARRANGED"] = str_temp;
                            //        _flexH[_flexH.Row, "DT_DUE"] = _flexH[_flexH.Row, "DT_PAY_PREARRANGED"];
                            //        _flexH[_flexH.Row, "TP_PAY_DD"] = _dt_Line.Rows[i]["TP_PAY_DD"];
                            //        _flexH[_flexH.Row, "DT_PAY_DD"] = _dt_Line.Rows[i]["DT_PAY_DD"];
                            //    }
                            //}
                            #endregion

                            #region 딘텍전용
                            string 지급예정일 = this.지급예정일계산(this.dtp처리일자.Text, D.GetDecimal(_flexH[_flexH.Row, "AM_TOTAL"]), D.GetInt(_dt_Line.Rows[i]["DT_PAY_PREARRANGED"]));
                            
                            _flexH[_flexH.Row, "DT_PAY_PREARRANGED"] = 지급예정일;
                            _flexH[_flexH.Row, "DT_DUE"] = 지급예정일;
                            #endregion

                            if (Config.MA_ENV.PJT형여부 == "Y")
                            {
                                _flexH["CD_PJT_ITEM"] = _dt_Line.Rows[i]["CD_PJT_ITEM"];
                                _flexH["NM_PJT_ITEM"] = _dt_Line.Rows[i]["NM_PJT_ITEM"];
                                _flexH["PJT_ITEM_STND"] = _dt_Line.Rows[i]["PJT_ITEM_STND"];
                                _flexH["NO_WBS"] = _dt_Line.Rows[i]["NO_WBS"];
                                _flexH["NO_CBS"] = _dt_Line.Rows[i]["NO_CBS"];
                                _flexH["CD_ACTIVITY"] = _dt_Line.Rows[i]["CD_ACTIVITY"];
                                _flexH["NM_ACTIVITY"] = _dt_Line.Rows[i]["NM_ACTIVITY"];
                                _flexH["CD_COST"] = _dt_Line.Rows[i]["CD_COST"];
                                _flexH["NM_COST"] = _dt_Line.Rows[i]["NM_COST"];
                            }
                            _flexH[_flexH.Row, "YN_JEONJA"] = _dt_Line.Rows[i]["YN_JEONJA"];
                            _flexH[_flexH.Row, "CD_PJT"] = _dt_Line.Rows[i]["CD_PJT"];
                            _flexH[_flexH.Row, "NM_PROJECT"] = _dt_Line.Rows[i]["NM_PROJECT"];

                            //(주)젬백스테크놀러지 전용 20140319 박종규
                            //지급 조건의 경우 받아온 건 중 최상단 한 건의 값이 default로 들어오도록 처리.
                            if (Global.MainFrame.ServerKeyCommon == "GEMTECH")
                            {
                                _flexH[_flexH.Row, "FG_PAYMENT"] = _dt_Line.Rows[0]["FG_PAYMENT"];
                            }

                            //if (MainFrameInterface.ServerKeyCommon == "UNIPOINT" && bContain)
                            //{
                            //    _flex["CD_PARTNER_PJT"] = _dt_Line.Rows[i]["CD_PARTNER_PJT"];
                            //    _flex["LN_PARTNER_PJT"] = _dt_Line.Rows[i]["LN_PARTNER_PJT"];
                            //    _flex["NO_EMP_PJT"] = _dt_Line.Rows[i]["NO_EMP_PJT"];
                            //    _flex["NM_KOR_PJT"] = _dt_Line.Rows[i]["NM_KOR_PJT"];
                            //    _flex["END_USER"] = _dt_Line.Rows[i]["END_USER"];
                            //} 

                            _flexH.AddFinished();
                            _flexH.Col = _flexH.Cols.Fixed;
                            ll_temp++;

                        }  // NO_TEMP가 널이면 .. 끝
                    }   // FOR문 끝
                    #endregion
                }
                else
                {
                    #region 일괄
                    DataRow[] ldr_temp;
                    long ll_temp = 1;

                    for (int i = 0; i < _dt_Line.Rows.Count; i++)
                    {
                        if (_dt_Line.Rows[i]["NO_TEMP"].ToString().Trim() == "")
                        {
                            // LOCAL LC 이면 ( 반품 구분 하지 않음 )
                            if (D.GetString(cbo거래구분.SelectedValue) == "003")
                            {
                                ldr_temp = _dt_Line.Select("CD_PARTNER='" + _dt_Line.Rows[i]["CD_PARTNER"].ToString() + "' AND NO_LC ='" + _dt_Line.Rows[i]["NO_LC"].ToString() + "' AND ISNULL(FG_TAX,'') ='" + D.GetString(_dt_Line.Rows[i]["FG_TAX"]) + "'");
                            }
                            else // 국내, 구매승인서 일경우 ( 반품 구분 하지 않음 )
                            {
                                ldr_temp = _dt_Line.Select("CD_PARTNER='" + _dt_Line.Rows[i]["CD_PARTNER"].ToString() + "' AND ISNULL(FG_TAX,'') ='" + D.GetString(_dt_Line.Rows[i]["FG_TAX"]) + "'");
                                //	ldr_temp = _dt_Line.Select("CD_PARTNER='"+_dt_Line.Rows[i]["CD_PARTNER"].ToString()+"' AND FG_TAX ='"+_dt_Line.Rows[i]["FG_TAX"].ToString()+"' AND YN_RETURN ='"+_dt_Line.Rows[i]["YN_RETURN"].ToString()+"'");							
                            }

                            _flexH.Rows.Add();

                            _flexH.Row = _flexH.Rows.Count - 1;

                            _flexH[_flexH.Row, "CHK"] = "Y";
                            //_flex[_flex.Row, "NO_IV"] = "";
                            _flexH[_flexH.Row, "NO_IO"] = _dt_Line.Rows[i]["NO_IO"].ToString();
                            _flexH[_flexH.Row, "CD_PARTNER"] = _dt_Line.Rows[i]["CD_PARTNER"].ToString();
                            _flexH[_flexH.Row, "LN_PARTNER"] = _dt_Line.Rows[i]["LN_PARTNER"].ToString();
                            _flexH[_flexH.Row, "NO_BIZAREA"] = _dt_Line.Rows[i]["NO_COMPANY"].ToString();
                            _flexH[_flexH.Row, "FG_TAX"] = _dt_Line.Rows[i]["FG_TAX"].ToString();
                            _flexH[_flexH.Row, "NM_TAX"] = _dt_Line.Rows[i]["NM_TAX"].ToString();
                            _flexH[_flexH.Row, "NO_TEMP"] = ll_temp.ToString();
                            _flexH[_flexH.Row, "FG_TRANS"] = cbo거래구분.SelectedValue.ToString();
                            _flexH[_flexH.Row, "CD_CC"] = _dt_Line.Rows[i]["CD_CC"].ToString();     //CD_CC값 추가

                            //_flex[_flex.Row,"YN_RETURN"] = _dt_Line.Rows[i]["YN_RETURN"].ToString();
                            //_flex[_flex.Row, "CD_DOCU"] = cd_docu;                                //2009.11.27 전표유형추가
                            _flexH[_flexH.Row, "FG_TPPURCHASE"] = _dt_Line.Rows[i]["FG_TPPURCHASE"].ToString();

                            _flexH[_flexH.Row, "CD_BIZAREA"] = ctx사업장.CodeValue;  // 사업장(2009.12.15 추가)
                            _flexH[_flexH.Row, "CD_BIZAREA_TAX"] = ctx부가세사업장.CodeValue;  // 부가세사업장(2009.12.15 추가)
                            _flexH[_flexH.Row, "NM_BIZAREA"] = ctx사업장.CodeName;  // 사업장(2009.12.15 추가)
                            _flexH[_flexH.Row, "NM_BIZAREA_TAX"] = ctx부가세사업장.CodeName;  // 부가세사업장(2009.12.15 추가)
                            _flexH[_flexH.Row, "NO_LC"] = D.GetString(_dt_Line.Rows[i]["NO_LC"]); //로우체인지할때사용함

                            decimal ldb_amk = 0;
                            decimal ldb_amkVat = 0;
                            decimal ldb_amEx = 0;
                            decimal ldb_vat = 0;
                            decimal ldb_adpay = 0;

                            for (int j = 0; j < ldr_temp.Length; j++)
                            {
                                ldr_temp[j]["NO_TEMP"] = D.GetString(ll_temp);
                                ldb_amk += D.GetDecimal(D.GetDecimal(ldr_temp[j]["AM_IV"]));

                                if (D.GetDecimal(ldr_temp[j]["TAX_RATE"]) > 0)
                                    ldb_amkVat += D.GetDecimal(D.GetDecimal(ldr_temp[j]["AM_IV"]));

                                ldb_amEx += D.GetDecimal(D.GetDecimal(ldr_temp[j]["AM_EX"]));
                                // ldb_vat += D.GetDecimal(System.Math.Floor(System.Double.Parse(ldr_temp[j]["VAT_IV"].ToString())));
                                ldb_vat += Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(ldr_temp[j]["VAT_IV"]));
                                ldb_adpay += D.GetDecimal(D.GetDecimal(ldr_temp[j]["AM_ADPAY"]));
                            }

                            string cd_docu = Get_Type_Dcou(ldr_temp);
                            _flexH[_flexH.Row, "CD_DOCU"] = cd_docu;


                            if (_dt_Line.Rows[i]["TP_TAX"].ToString().Trim() == "001" &&
                                System.Double.Parse(_dt_Line.Rows[i]["TAX_RATE"].ToString().Trim()) > 0)
                            {
                                decimal ldb_VatRate = D.GetDecimal(System.Double.Parse(_dt_Line.Rows[i]["TAX_RATE"].ToString().Trim()));
                                decimal ldb_tempVat = ldb_amkVat * ldb_VatRate * 0.01M;

                                if (ldb_tempVat < 0)
                                {
                                    //ldb_tempVat = System.Math.Ceiling(ldb_tempVat);
                                    ldb_tempVat = Unit.원화금액(DataDictionaryTypes.PU, ldb_tempVat);
                                }

                                //ldb_tempVat = System.Math.Floor(ldb_tempVat);
                                ldb_tempVat = Unit.원화금액(DataDictionaryTypes.PU, ldb_tempVat);
								//ldr_temp[ldr_temp.Length - 1]["VAT_IV"] = System.Math.Floor(System.Double.Parse(ldr_temp[ldr_temp.Length - 1]["VAT_IV"].ToString())) + (ldb_tempVat - ldb_vat);
								ldr_temp[ldr_temp.Length - 1]["VAT_IV"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(ldr_temp[ldr_temp.Length - 1]["VAT_IV"].ToString())) + (ldb_tempVat - ldb_vat);
								ldr_temp[ldr_temp.Length - 1]["AM_TOTAL"] = D.GetDecimal(ldr_temp[ldr_temp.Length - 1]["VAT_IV"]) + (D.GetDecimal(ldr_temp[ldr_temp.Length - 1]["AM_IV"]) - D.GetDecimal(ldr_temp[ldr_temp.Length - 1]["AM_ADPAY"]));
                                ldb_vat = ldb_tempVat;

                            }

                            // 실제 DB에 들어 가는 값
                            //_flex[_flex.Row, "AM_K"] = System.Math.Floor(ldb_amk);
                            _flexH[_flexH.Row, "AM_EX"] = ldb_amEx;
                            //_flex[_flex.Row, "VAT_TAX"] = System.Math.Floor(ldb_vat);
                            //_flex[_flex.Row, "VAT_TAX"] = Unit.원화금액(DataDictionaryTypes.PU, ldb_vat);

                            _flexH[_flexH.Row, "VAT_TAX"] = ldb_vat;
                            //구매승인서 ,국내, LOCAL L/C 든 뭐든지 환종을 강제로 원화로 바꾸거나 하는작업을 해서는 안된다.
                            _flexH[_flexH.Row, "CD_EXCH"] = _dt_Line.Rows[i]["CD_EXCH"].ToString().Trim();

                            if (Convert.ToDecimal(_dt_Line.Rows[i]["RT_EXCH"]) == 0 || _dt_Line.Rows[i]["RT_EXCH"] == null)
                                _dt_Line.Rows[i]["RT_EXCH"] = 1;

                            _flexH[_flexH.Row, "RT_EXCH"] = D.GetDecimal(_dt_Line.Rows[i]["RT_EXCH"]);

                            //_flex[_flex.Row, "AM_K"] = System.Math.Floor(_flex.CDecimal(_flex[_flex.Row, "AM_EX"]) * _flex.CDecimal(_dt_Line.Rows[i]["RT_EXCH"]));
                            _flexH[_flexH.Row, "AM_K"] = ldb_amk;
                            _flexH[_flexH.Row, "AM_ADPAY"] = ldb_adpay;
                            //_flex[_flex.Row, "AM_TOTAL"] = Convert.ToDouble(_flex[_flex.Row, "AM_K"]) + ldb_vat;
                            _flexH[_flexH.Row, "AM_TOTAL"] = (D.GetDecimal(_flexH[_flexH.Row, "AM_K"]) - ldb_adpay) + ldb_vat;

                            _flexH[_flexH.Row, "FG_PAYBILL"] = D.GetString(_dt_Line.Rows[i]["FG_PAYBILL"]);

                            #region 더존기본
                            //_flexH[_flexH.Row, "DT_PAY_DAY"] = D.GetInt(_dt_Line.Rows[i]["DT_PAY_PREARRANGED"]);
                            //if (_지급관리통제설정 == "Y")
                            //{
                            //    string dt_due = ComFunc.만기예정일자(dtp처리일자.Text, ldb_amk, D.GetString(_dt_Line.Rows[i]["FG_PAYBILL"]), "1");
                            //    // 만기예정일
                            //    if (dt_due != string.Empty)
                            //        _flexH[_flexH.Row, "DT_PAY_PREARRANGED"] = dt_due;

                            //    //어음 만기일(구분이 어음이 아닐경우 Empty가 return됨)
                            //    dt_due = ComFunc.만기예정일자(dtp처리일자.Text, ldb_amk, D.GetString(_dt_Line.Rows[i]["FG_PAYBILL"]), "2");
                            //    if (dt_due != string.Empty)
                            //        _flexH[_flexH.Row, "DT_DUE"] = dt_due;

                            //}
                            //else
                            //{
                            //    if (_지급예정일통제설정 != "100")  // 물류쪽 지급예정일 사용
                            //    {
                            //        str_temp = objFunc.DateAdd(dtp처리일자.Text, "D", D.GetInt(_flexH[_flexH.Row, "DT_PAY_DAY"]));
                            //        _flexH[_flexH.Row, "DT_PAY_PREARRANGED"] = str_temp;
                            //        _flexH[_flexH.Row, "DT_DUE"] = str_temp;
                            //    }
                            //    else
                            //    {
                            //        str_temp = objFunc.DateAdd(dtp처리일자.Text, "M", D.GetInt(D.GetDecimal(_dt_Line.Rows[i]["TP_PAY_DD"])));
                            //        str_temp = str_temp.Substring(0, 6) + string.Format("{0:00}", D.GetDecimal(_dt_Line.Rows[i]["DT_PAY_DD"]));
                            //        if (D.GetDecimal(str_temp) <= D.GetDecimal(dtp처리일자.Text))
                            //            str_temp = dtp처리일자.Text;
                            //        else
                            //        {
                            //            string str_lastday = objFunc.GetLastDayDateText(str_temp);
                            //            if (D.GetDecimal(str_temp) > D.GetDecimal(str_lastday))
                            //                str_temp = str_lastday;
                            //        }

                            //        _flexH[_flexH.Row, "DT_PAY_PREARRANGED"] = str_temp;
                            //        _flexH[_flexH.Row, "DT_DUE"] = _flexH[_flexH.Row, "DT_PAY_PREARRANGED"];
                            //        _flexH[_flexH.Row, "TP_PAY_DD"] = _dt_Line.Rows[i]["TP_PAY_DD"];
                            //        _flexH[_flexH.Row, "DT_PAY_DD"] = _dt_Line.Rows[i]["DT_PAY_DD"];
                            //    }
                            //}
                            #endregion

                            #region 딘텍전용
                            string 지급예정일 = this.지급예정일계산(this.dtp처리일자.Text, D.GetDecimal(_flexH[_flexH.Row, "AM_TOTAL"]), D.GetInt(_dt_Line.Rows[i]["DT_PAY_PREARRANGED"]));

                            _flexH[_flexH.Row, "DT_PAY_PREARRANGED"] = 지급예정일;
                            _flexH[_flexH.Row, "DT_DUE"] = 지급예정일;
                            #endregion

                            if (Config.MA_ENV.PJT형여부 == "Y")
                            {
                                _flexH["CD_PJT_ITEM"] = _dt_Line.Rows[i]["CD_PJT_ITEM"];
                                _flexH["NM_PJT_ITEM"] = _dt_Line.Rows[i]["NM_PJT_ITEM"];
                                _flexH["PJT_ITEM_STND"] = _dt_Line.Rows[i]["PJT_ITEM_STND"];
                                _flexH["NO_WBS"] = _dt_Line.Rows[i]["NO_WBS"];
                                _flexH["NO_CBS"] = _dt_Line.Rows[i]["NO_CBS"];
                                _flexH["CD_ACTIVITY"] = _dt_Line.Rows[i]["CD_ACTIVITY"];
                                _flexH["NM_ACTIVITY"] = _dt_Line.Rows[i]["NM_ACTIVITY"];
                                _flexH["CD_COST"] = _dt_Line.Rows[i]["CD_COST"];
                                _flexH["NM_COST"] = _dt_Line.Rows[i]["NM_COST"];
                            }
                            _flexH["YN_JEONJA"] = D.GetString(_dt_Line.Rows[i]["YN_JEONJA"]);
                            _flexH["CD_PJT"] = _dt_Line.Rows[i]["CD_PJT"];
                            _flexH["NM_PROJECT"] = _dt_Line.Rows[i]["NM_PROJECT"];

                            //(주)젬백스테크놀러지 전용 20140319 박종규
                            //지급 조건의 경우 받아온 건 중 최상단 한 건의 값이 default로 들어오도록 처리.
                            if (Global.MainFrame.ServerKeyCommon == "GEMTECH")
                            {
                                _flexH[_flexH.Row, "FG_PAYMENT"] = _dt_Line.Rows[0]["FG_PAYMENT"];
                            }

                            //if (MainFrameInterface.ServerKeyCommon == "UNIPOINT" && bContain)
                            //{
                            //    _flex["CD_PARTNER_PJT"] = _dt_Line.Rows[i]["CD_PARTNER_PJT"];
                            //    _flex["LN_PARTNER_PJT"] = _dt_Line.Rows[i]["LN_PARTNER_PJT"];
                            //    _flex["NO_EMP_PJT"] = _dt_Line.Rows[i]["NO_EMP_PJT"];
                            //    _flex["NM_KOR_PJT"] = _dt_Line.Rows[i]["NM_KOR_PJT"];
                            //    _flex["END_USER"] = _dt_Line.Rows[i]["END_USER"];
                            //} 

                            _flexH.AddFinished();
                            _flexH.Col = _flexH.Cols.Fixed;

                            ll_temp++;
                        }
                    }
                    #endregion
                }

                _flexL.Binding = _dt_Line.Copy();
                //Get_Type_Dcou(_dt_Line);
                _flexH.Redraw = true;
                _flexL.Redraw = true;

                _flexH.Row = _flexH.Rows.Count - 1;
                _flex_AfterRowChange(null, null);

            }
            catch(Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally 
            {
                 MsgControl.CloseMsg();
            }
        }

        public string Get_Type_Dcou(DataRow[] dtL)
        {
            string str_type_docu;
            decimal d_max_am = 0;

            if (D.GetString(this.cbo거래구분.SelectedValue) == "001")
                str_type_docu = "45";
            else
                str_type_docu = "46";

            for (int row = 0; row < dtL.Length; row++)
            {
                if (row == 0 || D.GetDecimal(dtL[row]["AM_IV"]) > d_max_am)
                {
                    d_max_am = D.GetDecimal(dtL[row]["AM_IV"]);
                    str_type_docu = dtL[row]["CD_DOCU"].ToString();
                }
            }

            return str_type_docu;
            
        }

        private void PageControlEnalbed(bool pb_enabled)
        {
            try
            {
                ctx사업장.Enabled = pb_enabled;
//                cbo_CD_BIZAREA_TAX.Enabled = pb_enabled;
                cbo거래구분.Enabled = pb_enabled;
                //cbo_FG_TPPURCHAS.Enabled = pb_enabled;
                rbtn계산서처리건별.Enabled = pb_enabled;
                rbtn계산서처리일괄.Enabled = pb_enabled;
                dtp처리일자.Enabled = pb_enabled;
                ctx담당자.Enabled = pb_enabled;
            }
            catch
            {
            }
        }

        private void btn부가세사업장변경_Click(object sender, EventArgs e)
        {
            try
            {
                if (_flexH.DataTable.Rows.Count < 1) return;

                DataRow[] rows = _flexH.DataTable.Select("CHK ='Y'");
                if (rows == null || rows.Length < 1)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                for (int i = 0; i < rows.Length; i++ )
                {
                    rows[i]["CD_BIZAREA_TAX"] = ctx부가세사업장.CodeValue;
                    rows[i]["NM_BIZAREA_TAX"] = ctx부가세사업장.CodeName;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }

        }

        private void _flex_CheckHeaderClick(object sender, EventArgs e)
        {
            try
            {
                FlexGrid flex = sender as FlexGrid;

                switch (flex.Name)
                {
                    case "_flex":  //상단 그리드 Header Click 이벤트

                        if (!_flexH.HasNormalRow) return;

                        //데이타 테이블의 체크값을 변경해주어도 화면에 보이는 값을 변경시키지 못하므로 화면의 값도 같이 변경시켜줌
                        _flexH.Row = 1; // 맨처음row에 자동위치하도록 해야 틀어지지 않는다.

                        if (!_flexH.HasNormalRow || !_flexL.HasNormalRow) return;

                        for (int h = 0; h < _flexH.Rows.Count - 1; h++)
                        {
                            _flexH.Row = h + 1; //Row가 순차적으로 변경되면서 RowChange() 이벤트를 자동 실행하게 유도한다.

                            for (int i = _flexL.Rows.Fixed; i < _flexL.Rows.Count; i++)
                            {
                                if (_flexL.RowState(i) == DataRowState.Deleted) continue;

                                _flexL[i, "S"] = _flexH["CHK"].ToString();
                            }
                        }
                        break;

                    case "_flexL":  //하단 그리드 Header Click 이벤트

                        if (!_flexL.HasNormalRow) return;

                        _flexH["CHK"] = D.GetString(_flexL["S"]);

                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn매입형태변경_Click(object sender, EventArgs e)
        {
            try
            {
                string TPPURCHASChange = D.GetString(cbo매입형태.SelectedValue); // == null ? string.Empty : cbo_FG_TPPURCHAS.SelectedValue.ToString();
                if (TPPURCHASChange == string.Empty)
                {
                    this.ShowMessage("변경할 매입형태를 입력하십시오");
                    cbo매입형태.Focus();
                    return;
                }


                DataRow[] rows = _flexL.DataTable.Select("S ='Y'");
                if (rows == null || rows.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                //string FG_TEMP = _flex[_flex.Row, "NO_IO"].ToString();
                //string FG_TEMP2 = _flex[_flex.Row, "FG_TAX"].ToString();
                //string FGfilter = "";

                //FGfilter = "NO_IO = '" + FG_TEMP + "'" + " AND  FG_TAX =  '" + FG_TEMP2 + "'" + " AND S =  'Y'";


                foreach (DataRow dr_L in _flexL.DataTable.Select())  //그리드에 보이는 화면만 변경.
                {
                    if (dr_L["S"].ToString() == "Y")
                    {
                        dr_L["FG_TPPURCHASE"] = cbo매입형태.SelectedValue;

                        string FG_TEMP = dr_L["NO_IOLINE"].ToString();
                        string FG_TEMP2 = dr_L["NO_IO"].ToString();
                        string FG_TEMP3 = D.GetString(dr_L["FG_TAX"]);
                        string FGfilter = "";

                        FGfilter = "NO_IOLINE = '" + FG_TEMP + "'" + " AND  NO_IO =  '" + FG_TEMP2 + "'" + " AND  ISNULL(FG_TAX,'') =  '" + FG_TEMP3 + "'";

                        foreach (DataRow dr in rows) //저장SP돌때 사용되는 데이터 테이블 그리드 변경과 맞쳐줌.
                        {
                            DataRow[] rowLines = _dt_Line.Select(FGfilter);

                            foreach (DataRow dlr in rowLines)
                            {
                                dlr["FG_TPPURCHASE"] = cbo매입형태.SelectedValue.ToString(); //매입형태 변경
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private string 지급예정일계산(string 처리일자, decimal 원화금액, int dtPay)
        {
            try
            {
                string 지급예정일 = string.Empty;
                int 월말일;
                DateTime 월말일자, dt처리일자, 익월;

                dt처리일자 = DateTime.ParseExact(처리일자, "yyyyMMdd", null);

                if (dtPay > 0)
                {
                    switch (dtPay)
                    {
                        case 930:
                            #region 익월 말일 결제 (30일)
                            익월 = dt처리일자.AddMonths(1);
                            지급예정일 = 익월.ToString("yyyyMM") + DateTime.DaysInMonth(익월.Year, 익월.Month).ToString();
                            #endregion
                            break;
                        case 960:
                            #region 익익월 말일 결제 (60일)
                            DateTime 익익월 = dt처리일자.AddMonths(2);
                            지급예정일 = 익익월.ToString("yyyyMM") + DateTime.DaysInMonth(익익월.Year, 익익월.Month).ToString();
                            #endregion
                            break;
                        case 907:
                            #region 세금계산서 발행일 + 7일
                            지급예정일 = dt처리일자.AddDays(7).ToString("yyyyMMdd");
                            #endregion
                            break;
                        case 914:
                            #region 세금계산서 발행일 + 14일
                            지급예정일 = dt처리일자.AddDays(14).ToString("yyyyMMdd");
                            #endregion
                            break;
                        case 931:
                            #region 세금계산서 발행일 + 30일
                            지급예정일 = dt처리일자.AddDays(30).ToString("yyyyMMdd");
                            #endregion
                            break;
                        case 996:
                            #region 세금계산서 발행일 + 59일
                            지급예정일 = dt처리일자.AddDays(59).ToString("yyyyMMdd");
                            #endregion
                            break;
                        case 961:
                            #region 세금계산서 발행일 + 60일
                            지급예정일 = dt처리일자.AddDays(60).ToString("yyyyMMdd");
                            #endregion
                            break;
                        case 997:
                            #region 익월말 하루전 결제
                            익월 = dt처리일자.AddMonths(1);

                            월말일 = DateTime.DaysInMonth(익월.Year, 익월.Month);
                            월말일자 = DateTime.Parse(익월.ToString("yyyy-MM") + "-" + 월말일.ToString());

                            지급예정일 = 월말일자.AddDays(-1).ToString("yyyyMMdd");
                            #endregion
                            break;
                        case 998:
                            #region 선지급
                            지급예정일 = 처리일자;
                            #endregion
                            break;
                        case 999:
                            #region 즉시결제
                            지급예정일 = 처리일자;
                            #endregion
                            break;
                        default:
                            #region 지급예정일이 정해져 있는 매입처 (익월 n일, 1달을 30일로 봄)
                            익월 = dt처리일자.AddMonths(dtPay / 30);

                            월말일 = DateTime.DaysInMonth(익월.Year, 익월.Month);
                            월말일자 = DateTime.Parse(익월.ToString("yyyy-MM") + "-" + 월말일.ToString());

                            지급예정일 = 월말일자.AddDays(dtPay % 30).ToString("yyyyMMdd");
                            #endregion
                            break;
                    }
                }
                //else if (원화금액 <= 1000000)
                //{
                //    #region 100만원 이하, 10일
                //    지급예정일 = dt처리일자.AddDays(10).ToString("yyyyMMdd");
                //    #endregion
                //}
                //else if (원화금액 > 1000000 && 원화금액 <= 3000000)
                //{
                //    #region 100만원 초과 300만원 이하, 익월 15일
                //    지급예정일 = dt처리일자.AddMonths(1).ToString("yyyyMM") + "15";
                //    #endregion
                //}
                //else if (원화금액 > 3000000 && 원화금액 <= 5000000)
                //{
                //    #region 300만원 초과 500만원 이하, 익월 말일
                //    DateTime 익월 = dt처리일자.AddMonths(1);
                //    지급예정일 = 익월.ToString("yyyyMM") + DateTime.DaysInMonth(익월.Year, 익월.Month).ToString();
                //    #endregion
                //}
                //else if (원화금액 > 5000000 && 원화금액 <= 10000000)
                //{
                //    #region 500만원 초과 1000만원 이하, 월 마감 후 45일
                //    월말일 = DateTime.DaysInMonth(dt처리일자.Year, dt처리일자.Month);
                //    월말일자 = DateTime.Parse(dt처리일자.ToString("yyyy-MM") + "-" + 월말일.ToString());

                //    지급예정일 = 월말일자.AddDays(45).ToString("yyyyMMdd");
                //    #endregion
                //}
                //else if (원화금액 > 10000000 && 원화금액 <= 30000000)
                //{
                //    #region 1000만원 초과 3000만원 이하, 월 마감 후 60일
                //    월말일 = DateTime.DaysInMonth(dt처리일자.Year, dt처리일자.Month);
                //    월말일자 = DateTime.Parse(dt처리일자.ToString("yyyy-MM") + "-" + 월말일.ToString());

                //    지급예정일 = 월말일자.AddDays(60).ToString("yyyyMMdd");
                //    #endregion
                //}
                //else if (원화금액 > 30000000)
                //{
                //    #region 3000만원 초과, 월 마감 후 75일
                //    월말일 = DateTime.DaysInMonth(dt처리일자.Year, dt처리일자.Month);
                //    월말일자 = DateTime.Parse(dt처리일자.ToString("yyyy-MM") + "-" + 월말일.ToString());

                //    지급예정일 = 월말일자.AddDays(75).ToString("yyyyMMdd");
                //    #endregion
                //}
                //else
                //{
                //    지급예정일 = 처리일자;
                //}

                return 지급예정일;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }

            return string.Empty;
        }
        #endregion
    }
}
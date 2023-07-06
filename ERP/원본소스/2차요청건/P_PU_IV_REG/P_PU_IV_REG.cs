using System;
using System.Data;
using System.Windows.Forms;

using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.Common.Controls;
using Duzon.ERPU;

using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.MF;
using Duzon.Common.Forms.Help;



namespace pur
{
    /// <summary>
    //********************************************************************
    // 작   성   자 : 
    // 작   성   일 : 
    // 모   듈   명 : 구매/자재
    // 시 스 템  명 : 매입관리
    // 페 이 지  명 : 매입등록
    // 프로젝트  명 : P_PU_IV_REG
    // 수   정   일 : 2004-03-05 
    // 수   정   자 : 유지영 (2006-08-30)
    // 재 작  성 자 : 허성철 (2006-08-30)
    // 변 경  내 역
    // 2009.12.15 - 부가세 사업장(CD_BIZAREA_TAX) 추가 - SMR (KHS/KTIS, 크라제)
    // 2010.02.16 - 안종호 - 로그인 사용자 소속사업장 기본셋팅(이형준대리, 백광)
    // 2010.03.08 - 신미란 - 거래구분,과세구분변경 기능추가 (이형준대리, 제다)
    // 2010.04.20 - 안종호 - 사업장,부가세사업장 콤보 컨트롤에서 헬프도움창컨트롤로 변환
    // 2013.04.16 : D20130409066 : 만기일자 DEFAULT처리: 지급예정일자로
    //********************************************************************
    /// </summary>

    public partial class P_PU_IV_REG : Duzon.Common.Forms.PageBase
    {
        #region ♣ 멤버필드

        #region -> 멤버필드(주요)

        private P_PU_IV_REG_BIZ _biz;
        string _cddept = "";
        DataSet _dsCombo = new DataSet();
        // 라인정보
        DataTable _dt_Line = new DataTable();
        // 텍스트 박스 포커스시 값 임시 저장 변수	
        //private string gstb_noemp;
        DataTable ds_Ty1 = new DataTable();

        Boolean 하단그리드필터여부 = true;

        bool IsLoadData = false;
        string _지급관리통제설정 = "N";
        string _지급예정일통제설정 = "000";

        bool bStandard = false; //규격형

        #endregion

        #endregion        

        #region ♣ 초기화

        #region -> P_PU_IV_REG

        public P_PU_IV_REG()
        {
            InitializeComponent();
            this.MainGrids = new FlexGrid[] { _flex, _flexL };
        }

        #endregion

        #region -> InitLoad

        protected override void InitLoad()
        {
            _biz = new P_PU_IV_REG_BIZ(this.MainFrameInterface);

            //InitDD(_tlay_Main);
            InitMaExc();
            InitControl();

            InitGrid();
            InitGridL();
            
            _flex.DetailGrids = new FlexGrid[] { _flexL };

        }

        #endregion

        #region -> InitMaExc
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

            _지급관리통제설정 = BASIC.GetMAEXC("거래처정보등록-회계지급관리사용여부");
            _지급예정일통제설정 = BASIC.GetMAEXC("매입등록-지급예정일통제설정");


            //규격형 사용 유무
            if (BASIC.GetMAEXC("공장품목등록-규격형") == "100")
            {
                bStandard = true;

            }
        }

        #endregion

        #region -> InitControl

        private void InitControl()
        {
            try
            {
                tb_DT_PO.Mask = MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
                tb_DT_PO.ToDayDate = MainFrameInterface.GetDateTimeToday();
                tb_DT_PO.Text = MainFrameInterface.GetDateTimeToday().ToString();

                tb_NO_EMP.CodeName = this.LoginInfo.EmployeeName;
                tb_NO_EMP.CodeValue = this.LoginInfo.EmployeeNo;
                _cddept = this.LoginInfo.DeptCode;

                btn_TAXPAPER.Enabled = false;
                rbtn_T_IV.Checked = true;
                SetControlEnabled(true);


            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }


        #endregion

        #region -> InitPaint

        protected override void InitPaint()
        {
            _dsCombo = this.GetComboData("N;PU_C000016", "S;MA_CODEDTL_003", "NC;MA_BIZAREA", "N;MA_B000005",
                                         "N;FI_J000002", "N;PU_C000044", "S;MA_AISPOSTH;200", "NC;MA_BIZAREA", "N;PU_C000005",
                                         "N;MA_B000095", "N;PU_C000014");

            DataTable ldt_FG_TRANS = _dsCombo.Tables[0].Clone();
            DataTable ldt_FG_TRANS_변경 = _dsCombo.Tables[0].Clone();
            DataTable ldt_FG_TPPURCHAS = _dsCombo.Tables[6].Clone();

            DataRow[] ldr_FgTrans = _dsCombo.Tables[0].Select("CODE IN ('001', '002', '003')");
            DataRow[] ldr_FgTrans_변경 = _dsCombo.Tables[0].Select("CODE IN ('002', '003')");
            DataRow[] ldr_FGTPPURCHAS = _dsCombo.Tables[6].Select("CODE IN ('001', '002','003','004','005')");

            if (ldr_FgTrans != null && ldr_FgTrans.Length > 0)
            {
                for (int i = 0; i < ldr_FgTrans.Length; i++)
                {
                    ldt_FG_TRANS.ImportRow(ldr_FgTrans[i]);
                    if (i > 0)
                        ldt_FG_TRANS_변경.ImportRow(ldr_FgTrans_변경[i - 1]);
                    else
                    {
                        DataRow row_tmp = null;
                        DataTable dt_tmp = _dsCombo.Tables[0].Clone();
                        row_tmp = dt_tmp.NewRow(); row_tmp["CODE"] = ""; row_tmp["NAME"] = ""; dt_tmp.Rows.Add(row_tmp);

                        ldt_FG_TRANS_변경.ImportRow(row_tmp);
                    }
                }
            }

            if (ldr_FGTPPURCHAS != null && ldr_FGTPPURCHAS.Length > 0) // 매입형태 
            {
                for (int i = 0; i < ldr_FGTPPURCHAS.Length; i++)
                {
                    DataRow row_tmp = null;
                    ldt_FG_TPPURCHAS.ImportRow(row_tmp);
                    ldt_FG_TPPURCHAS.ImportRow(ldr_FGTPPURCHAS[i]);

                }
            }

            // 거래구분		
            cbo_FG_TRANS.DataSource = ldt_FG_TRANS;
            cbo_FG_TRANS.DisplayMember = "NAME";
            cbo_FG_TRANS.ValueMember = "CODE";

            // 거래구분변경(2010.03.08추가)
            cbo_FG_TRANS_변경.DataSource = ldt_FG_TRANS_변경;
            cbo_FG_TRANS_변경.DisplayMember = "NAME";
            cbo_FG_TRANS_변경.ValueMember = "CODE";

            // 매입형태변경(2011.03.16추가)
            //cbo_FG_TPPURCHAS.DataSource = ldt_FG_TPPURCHAS;  // (D20120816062 문제원인임) 왜 001~005까지 했는지 이해안됨... 2011.03.16 외주개발자가 개발했는데 기록 없슴(표준화팀체크완료된건이였슴)
            cbo_FG_TPPURCHAS.DataSource = _dsCombo.Tables[6];  
            cbo_FG_TPPURCHAS.DisplayMember = "NAME";
            cbo_FG_TPPURCHAS.ValueMember = "CODE";


            bp_CD_BIZAREA.CodeValue = LoginInfo.BizAreaCode;
            bp_CD_BIZAREA.CodeName = LoginInfo.BizAreaName;

            DataTable dt_biz = _biz.getCd_Pc(bp_CD_BIZAREA.CodeValue);

            if (dt_biz != null || dt_biz.Rows.Count > 0)
            {
                bp_CD_BIZAREA_TAX.CodeValue = D.GetString(dt_biz.Rows[0]["CD_BIZAREA"]);
                bp_CD_BIZAREA_TAX.CodeName = D.GetString(dt_biz.Rows[0]["NM_BIZAREA"]);
            }
            else
            {
                bp_CD_BIZAREA_TAX.CodeValue = LoginInfo.BizAreaCode;
                bp_CD_BIZAREA_TAX.CodeName = LoginInfo.BizAreaName;
            }

            _flex.SetDataMap("CD_EXCH", _dsCombo.Tables[3], "CODE", "NAME");

            _flex.SetDataMap("CD_DOCU", _dsCombo.Tables[4], "CODE", "NAME");
            //_flexL.SetDataMap("FG_TPPURCHASE", ldt_FG_TPPURCHAS, "CODE", "NAME");    //매입형태 코드, 네임
            _flexL.SetDataMap("FG_TPPURCHASE", _dsCombo.Tables[6], "CODE", "NAME");    //매입형태 코드, 네임

           
            if (_지급관리통제설정 == "N")
                _flex.SetDataMap("FG_PAYBILL", _dsCombo.Tables[5], "CODE", "NAME");
            else
            {
                DataTable dt_pay = ComFunc.GetPayList();
                if (dt_pay != null)
                    _flex.SetDataMap("FG_PAYBILL", dt_pay, "CODE", "NAME");
            }

            // 부가세여부 
            _flexL.SetDataMap("TP_UM_TAX", _dsCombo.Tables[8], "CODE", "NAME");

            _flex.SetDataMap("YN_JEONJA", _dsCombo.Tables[9], "CODE", "NAME"); //계산서발행형태
            _flex.Binding = _biz.GetLineTable();//주 그리드 초기화
            
            ///지급조건(발주) 
            _flex.SetDataMap("FG_PAYMENT", _dsCombo.Tables[10], "CODE", "NAME");

            oneGrid1.UseCustomLayout = true;
            bpPanelControl1.IsNecessaryCondition = true;
            bpPanelControl3.IsNecessaryCondition = true;
            bpPanelControl4.IsNecessaryCondition = true;
            bpPanelControl5.IsNecessaryCondition = true;
            bpPanelControl8.IsNecessaryCondition = true;
            bpPanelControl9.IsNecessaryCondition = true;
            
            oneGrid1.InitCustomLayout();

            if (Global.MainFrame.ServerKeyCommon == "KIHA")
            {
                btnCdPC.Visible = true;
                ctxCdPc.Visible = true;
                lblCdPc.Visible = true;
                btnCdPC.Click += new EventHandler(btnCdPC_Click);
            }

        }

        #endregion

        #region -> InitGrid

        private void InitGrid()
        {
            _flex.BeginSetting(1, 1, false);

            //_flex.SetCol("CHK", "S", "선택", 40, true, CheckTypeEnum.Y_N);
            _flex.SetCol("CHK", "선택", 40, true, CheckTypeEnum.Y_N);
            if (BASIC.GetMAEXC("매입등록_매입번호_KEY-IN 사용여부") == "100")
                _flex.SetCol("NO_IV", "매입번호", 120, true);
            else
                _flex.SetCol("NO_IV", "매입번호", 120, false);

            _flex.SetCol("CD_BIZAREA", "사업장", 80);
            _flex.SetCol("NM_BIZAREA", "사업장명", 150);
            _flex.SetCol("CD_BIZAREA_TAX", "부가세사업장", 80);
            _flex.SetCol("NM_BIZAREA_TAX", "부가세사업장명", 150);
            _flex.SetCol("CD_PARTNER", "거래처코드", 80);
            _flex.SetCol("LN_PARTNER", "거래처명", 150);
            _flex.SetCol("TXT_USERDEF1", "사용자정의1", 150, true);
            _flex.SetCol("NO_BIZAREA", "사업자NO", 100, false, typeof(string));
            _flex.SetCol("NM_TAX", "과세구분", 100);
            _flex.SetCol("AM_K", "공급가액", 120, 17, true, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("VAT_TAX", "부가세", 120, 17, true, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("AM_TOTAL", "총금액", 120, 17, false, typeof(decimal), FormatTpType.MONEY);
          
            _flex.SetCol("CD_DOCU", "전표유형", 80, true);
            _flex.SetCol("CD_EXCH", "환종", 80, false);
            _flex.SetCol("RT_EXCH", "환율", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            _flex.SetCol("AM_EX", "금액(외화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            _flex.SetCol("FG_PAYBILL", "지급구분(거래처)", 80, true, typeof(string));
            _flex.SetCol("FG_PAYMENT", "지급조건(발주)", 80, true, typeof(string));
            _flex.SetCol("DT_PAY_PREARRANGED", "지급예정일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex.SetCol("DT_DUE", "만기일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex.SetCol("DC_RMK", "비고", 200, true, typeof(string));
            _flex.SetCol("YN_JEONJA", "계산서발행형태", 100, true);
            _flex.SetCol("NO_IO", "입고번호", 200);

            if (Global.MainFrame.ServerKeyCommon == "KIHA")
            {
                _flex.SetCol("CD_PC_USER", "회계단위코드", 80, true, typeof(string));
                _flex.SetCol("NM_PC_USER", "회계단위명", 100, false, typeof(string));
                _flex.SetCodeHelpCol("CD_PC_USER", HelpID.P_MA_PC_SUB, ShowHelpEnum.Always, new string[] { "CD_PC_USER", "NM_PC_USER" }, new string[] { "CD_PC", "NM_PC" }, ResultMode.SlowMode);
            }

            if ((Global.MainFrame.ServerKeyCommon == "119") || (Global.MainFrame.ServerKeyCommon == "DZSQL") || (Global.MainFrame.ServerKeyCommon == "SQL_108") || (Global.MainFrame.ServerKeyCommon == "SEMICS"))
            {
                _flex.Cols["TXT_USERDEF1"].Visible = true;

                _flex.Cols["TXT_USERDEF1"].Caption = "현금영수증 승인번호";
                _flex.Cols.Move(_flex.Cols["TXT_USERDEF1"].Index, 9);
            }
            else
            {
                _flex.Cols["TXT_USERDEF1"].Visible = false;
            }

            _flex.Cols["NO_IO"].Visible = false;

            _flex.Cols["NO_BIZAREA"].Format = "###-##-#####";

            _flex.SetDummyColumn("CHK");

           
            _flex.SetStringFormatCol("NO_BIZAREA");
            if (Global.MainFrame.ServerKeyCommon == "KIHA")
                _flex.VerifyNotNull = new string[] { "NO_BIZAREA", "CD_BIZAREA_TAX","CD_PC_USER" };
            else
                _flex.VerifyNotNull = new string[] { "NO_BIZAREA", "CD_BIZAREA_TAX" };

            _flex.SettingVersion = "2.9.8";
            _flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flex.ValidateEdit += new C1.Win.C1FlexGrid.ValidateEditEventHandler(_flex_ValidateEdit);
            _flex.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
            _flex.StartEdit += new RowColEventHandler(_flex_StartEdit);

            _flex.CheckHeaderClick += new EventHandler(_flex_CheckHeaderClick);
            _flex.AfterEdit += new RowColEventHandler(_flex_AfterEdit);
            
            
        }

        #endregion

        #region -> InitGridL

        private void InitGridL()
        {
            // 2011.03.17_김호연 추가
            _flexL.BeginSetting(1, 1, false);

            _flexL.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);  //선택. 사용하게될지도....

            _flexL.SetCol("NO_IV", "매입번호", 120, false);
            _flexL.SetCol("NO_IOLINE", "항번", 40);
            _flexL.SetCol("NO_IO", "입고번호", 100, false);
            _flexL.SetCol("CD_ITEM", "품목코드", 80);
            _flexL.SetCol("NM_ITEM", "품목명", 200);
            _flexL.SetCol("STND_ITEM", "규격", 60);
            _flexL.SetCol("PI_PARTNER", "주거래처코드", 80);
            _flexL.SetCol("PI_LN_PARTNER", "주거래처명", 150);
            _flexL.SetCol("GI_PARTNER", "납품처코드", 80);
            _flexL.SetCol("GI_LN_PARTNER", "납품처명", 150);
            _flexL.SetCol("QT_IV", "입고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("QT_CLS", "관리수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            
            if (bStandard)
            {
                _flexL.SetCol("UM_WEIGHT", "중량단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                _flexL.SetCol("TOT_WEIGHT", "총중량", 100, false, typeof(decimal));

                _flexL.Cols["TOT_WEIGHT"].Format = "###,###,###.#";

            }


            _flexL.SetCol("UM_EX", "단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flexL.SetCol("AM_EX", "금액", 150, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flexL.SetCol("UM", "원화단가", 150, false, typeof(decimal), FormatTpType.UNIT_COST);
            _flexL.SetCol("AM_IV", "원화금액", 150, false, typeof(decimal), FormatTpType.MONEY); 
            _flexL.SetCol("VAT_IV", "부가세", 150, false, typeof(decimal), FormatTpType.MONEY);
            _flexL.SetCol("TP_UM_TAX", "부가세여부", 70, false);
            _flexL.SetCol("AM_TOTAL", "총금액", 150, 17, false, typeof(decimal), FormatTpType.MONEY);
            _flexL.SetCol("FG_TPPURCHASE", "매입형태", 150);

            if (Config.MA_ENV.프로젝트사용)
                _flexL.SetCol("SEQ_PROJECT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 항번" : "프로젝트항번", 120, false, typeof(decimal));

            if (Config.MA_ENV.PJT형여부 == "Y")
            {
                _flexL.SetCol("CD_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 코드" : "프로젝트 품목코드", 140, false, typeof(string));
                _flexL.SetCol("NM_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 명" : "프로젝트 품목명", 140, false, typeof(string));
                _flexL.SetCol("PJT_ITEM_STND", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 규격" : "프로젝트 품목규격", 140, false, typeof(string));
                _flexL.SetCol("NO_WBS", "WBS번호", 140, false, typeof(string));
                _flexL.SetCol("NO_CBS", "CBS번호", 140, false, typeof(string));
                _flexL.SetCol("CD_ACTIVITY", "ACTIVITY 코드", 140, false, typeof(string));
                _flexL.SetCol("NM_ACTIVITY", "ACTIVITY", 140, false, typeof(string));
                _flexL.SetCol("CD_COST", "원가코드", 140, false, typeof(string));
                _flexL.SetCol("NM_COST", "원가명", 140, false, typeof(string));
            }
            _flexL.SetCol("CD_PJT", "프로젝트코드", 150);
            _flexL.SetCol("NM_PROJECT", "프로젝트명", 150);


            //if (MainFrameInterface.ServerKeyCommon == "UNIPOINT")
            //{
            //    _flexL.SetCol("CD_PARTNER_PJT", "프로젝트 거래처코드", 100, false, typeof(string));
            //    _flexL.SetCol("LN_PARTNER_PJT", "프로젝트 거래처", 100, false, typeof(string));
            //    _flexL.SetCol("NO_EMP_PJT", "프로젝트 담당자코드", 100, false, typeof(string));
            //    _flexL.SetCol("NM_KOR_PJT", "프로젝트 담당자", 100, false, typeof(string));
            //    _flexL.SetCol("END_USER", "프로젝트 END USER", 100, false, typeof(string));
            //} 

            _flexL.Cols["PI_PARTNER"].Visible = false;
            _flexL.Cols["PI_LN_PARTNER"].Visible = false;
            _flexL.Cols["GI_PARTNER"].Visible = false;
            _flexL.Cols["GI_LN_PARTNER"].Visible = false;
            
            _flexL.SetDummyColumn("S");
            _flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            if (Config.MA_ENV.YN_UNIT == "Y")
                _flexL.SetExceptSumCol("RT_EXCH", "UM_EX","SEQ_PROJECT");
            else
                _flexL.SetExceptSumCol("RT_EXCH", "UM_EX");

            if (Config.MA_ENV.YN_UNIT == "Y" && bStandard)
                _flex.SetExceptSumCol("RT_EXCH", "UM_EX", "SEQ_PROJECT", "UM_WEIGHT");
            else if (Config.MA_ENV.YN_UNIT == "Y" && !bStandard)
                _flex.SetExceptSumCol("RT_EXCH", "UM_EX", "SEQ_PROJECT");
            else if (Config.MA_ENV.YN_UNIT != "Y" && bStandard)
                _flex.SetExceptSumCol("RT_EXCH", "UM_EX", "UM_WEIGHT");
            else
                _flex.SetExceptSumCol("RT_EXCH", "UM_EX");


            if (Config.MA_ENV.YN_UNIT == "Y")
                _flexL.VerifyNotNull = new string[] {"SEQ_PROJECT" };

            _flexL.EnabledHeaderCheck = true;

            _flexL.CheckHeaderClick += new EventHandler(_flex_CheckHeaderClick);

            _flexL.SettingVersion = "1.3.8";
            _flexL.StartEdit += new RowColEventHandler(_flexL_StartEdit);
            _flexL.ValidateEdit += new C1.Win.C1FlexGrid.ValidateEditEventHandler(_flexL_ValidateEdit);
        }
        #endregion
        #endregion

        #region ♣ 메인버튼 이벤트

        #region -> SaveData

        protected override bool SaveData()
        {
            if (!this.Verify())
                return false;

            if (tb_NO_EMP.IsEmpty())
            {
                ShowMessage("담당자는 필수입력 항목 입니다.");
                tb_NO_EMP.Focus();
                return false;
            }

            InDataHeadValue();

            DataRow[] rows = (System.Data.DataRow[])this.GetSeq(this.LoginInfo.CompanyCode, "PU", "09", tb_DT_PO.Text.Substring(0, 6), _flex.DataTable.Rows.Count);

            for (int i = 0; i < _flex.DataTable.Rows.Count; i++)
            {
                string str_no_docu = "";

                if(D.GetString(_flex.DataTable.Rows[i]["NO_IV"]) == "")
                    str_no_docu = rows[i]["DOCU_NO"].ToString();
                else 
                   str_no_docu = D.GetString(_flex.DataTable.Rows[i]["NO_IV"]);

                _flex.DataTable.Rows[i]["NO_IV"] = str_no_docu;

                DataRow[] ldr_temp = _dt_Line.Select("NO_TEMP='" + _flex.DataTable.Rows[i]["NO_TEMP"].ToString() + "'");
                DataRow[] dr_line = _flexL.DataTable.Select("NO_TEMP='" + _flex.DataTable.Rows[i]["NO_TEMP"].ToString() + "'");
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

            object obj = _biz.Save(_flex.DataTable, _dt_Line, ds_Ty1, _flex[_flex.Row, "CD_CC"].ToString(), cbo_FG_TRANS.SelectedValue.ToString(), tb_DT_PO.Text);

            for ( int i = 0 ; i < _flex.DataTable.Rows.Count ; i++ )
            {
                if ( !_biz.부가세변경( _flex.DataTable.Rows [i] ["NO_IV"].ToString() ) )
                    return false;
            }

            ResultData[] result = (ResultData[])obj;
                                                                 
            
           // _flexL.Binding = _dt_Line;
            _flexL.Redraw = true;                                                                                                  
            if (result[0].Result && result[1].Result)
            {
                btn_APPLY_GI.Enabled = false;
                btn_TAXPAPER.Enabled = true;
                _flex.AllowEditing = false;
                _flex.AcceptChanges();
                _flexL.AcceptChanges();

                return true;
            }

            SetSaveFalse();
            return false;
        }

        #endregion

        #region -> 저장버튼클릭

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {

            try
            {
                if (!BeforeSave())
                    return;

                if (MsgAndSave(PageActionMode.Save))
                    this.ShowMessage(PageResultMode.SaveGood);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        #region -> 추가버튼클릭

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                lb_FG_PO_TR.Focus();
                //tb_NO_EMP.CodeName = this.LoginInfo.EmployeeName;
                //tb_NO_EMP.Tag = this.LoginInfo.EmployeeNo;
                //tb_NO_EMP.CodeValue = this.LoginInfo.EmployeeNo;
                //_cddept = this.LoginInfo.DeptCode;
                rbtn_M_IV.Enabled = true;
                rbtn_T_IV.Enabled = true;
                btn_APPLY_GI.Enabled = true;
                btn_TAXPAPER.Enabled = false;
                SetControlEnabled(true);
                tb_DT_PO.Text = this.MainFrameInterface.GetStringToday;
                _dt_Line.Clear();
                _flex.AllowEditing = true;
                _flex.DataTable.Clear();
                if(_flexL.HasNormalRow) _flexL.DataTable.Clear();
            }
            catch(Exception ex)
            {
                this.MsgEnd(ex);
            }

        }

        #endregion

        #endregion          

        #region ♣ 그리드 이벤트 / 메서드

        #region -> _flex_ValidateEdit

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
                


                if (D.GetString(_flex.Cols[e.Col].Name) == "AM_K" && (newValue > vsoldValue || newValue < vmoldValue))
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

                    _flex[e.Row, "AM_K"] = oldValue.ToString();

                    return;
                }

                string ColName = ((FlexGrid)sender).Cols[e.Col].Name;


                if (_flex.AllowEditing)
                {			//	System.Diagnostics.Debugger.Break();
                    if (_flex.GetData(e.Row, e.Col).ToString() != _flex.EditData)
                    {
                        switch (_flex.Cols[e.Col].Name)
                        {
                            case "AM_K":
                                //ChangeAM_K(System.Double.Parse(_flex.EditData));
                                //ChangeAM_K(D.GetDecimal(_flex.EditData)); //위치이동 부가세 계산 이후로.. 
                                
                                DataRow[] dr_flexL = _flexL.DataTable.Select(" NO_TEMP ='" + D.GetString(_flex["NO_TEMP"] + "'"));
                                _flex["VAT_TAX"] = Unit.원화금액(DataDictionaryTypes.PU,UDecimal.Getdivision(D.GetDecimal(_flex.EditData), D.GetDecimal(dr_flexL[0]["TAX_RATE"])));

                                ChangeAM_K(D.GetDecimal(_flex.EditData));

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
                                _flex[_flex.Row, "AM_TOTAL"] = D.GetDecimal(_flex.EditData) + Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flex[_flex.Row, "AM_K"].ToString().Trim()));
                                //ChangeVAT_K(System.Double.Parse(_flex.EditData));
                                //ChangeVAT_K(D.GetDecimal(_flex.EditData));
                                break;

                            case "FG_PAYBILL":
                                string dt_due = string.Empty;
                                string dt_pay = string.Empty;

                                if (_지급관리통제설정 == "Y")
                                {
                                    decimal ldb_amk = D.GetDecimal(_flex["AM_K"]);
                                    dt_pay = ComFunc.만기예정일자(tb_DT_PO.Text, ldb_amk, D.GetString(_flex.EditData), "1");      
                                    // 만기예정일
                                    if (dt_pay != string.Empty)
                                    {
                                        _flex[_flex.Row, "DT_PAY_PREARRANGED"] = dt_pay;
                                        _flex[_flex.Row, "DT_DUE"] = dt_pay;
                                    }

                                    //어음 만기일(구분이 어음이 아닐경우 Empty가 return됨)
                                    dt_due = ComFunc.만기예정일자(tb_DT_PO.Text, ldb_amk, D.GetString(_flex.EditData), "2");
                                    if (dt_due != string.Empty)
                                        _flex[_flex.Row, "DT_DUE"] = dt_due;

                                }

                                if (_지급관리통제설정 == "N" || dt_pay == string.Empty || dt_due == string.Empty)
                                {
                                    if (_지급예정일통제설정 == "000")
                                    {
                                        string str_temp = objFunc.DateAdd(tb_DT_PO.Text, "D", Convert.ToInt16(_flex["DT_PAY_DAY"].ToString()));
                                        if (dt_pay == string.Empty && Global.MainFrame.ServerKeyCommon != "TRIGEM" ) ///삼보는 이기능을 사용하고싶지않다고함
                                            _flex["DT_PAY_PREARRANGED"] = str_temp;

                                        if (dt_due == string.Empty)
                                            _flex["DT_DUE"] = _flex["DT_PAY_PREARRANGED"];
                                    }
                                    else 
                                    {
                                        string str_temp = objFunc.DateAdd(tb_DT_PO.Text, "M", Convert.ToInt16(D.GetDecimal(_flex["TP_PAY_DD"])));
                                        str_temp = str_temp.Substring(0, 6) + string.Format("{0:00}", D.GetDecimal(_flex["DT_PAY_DD"]));
                                        if (D.GetDecimal(str_temp) <= D.GetDecimal(tb_DT_PO.Text))
                                            str_temp = tb_DT_PO.Text;
                                        else
                                        {
                                            string str_lastday = objFunc.GetLastDayDateText(tb_DT_PO.Text);
                                            if (D.GetDecimal(str_temp) > D.GetDecimal(str_lastday))
                                                str_temp = str_lastday;
                                        }

                                        _flex[_flex.Row, "DT_PAY_PREARRANGED"] = str_temp;
                                        _flex[_flex.Row, "DT_DUE"] = _flex["DT_PAY_PREARRANGED"];
                                    }

                                }
                                break;

                            case "FG_PAYMENT":
                                if (Global.MainFrame.ServerKeyCommon == "TRIGEM" || Global.MainFrame.ServerKeyCommon == "DZSQL" || Global.MainFrame.ServerKeyCommon == "SQL_")
                                {
                                    DataTable dt = MF.GetCode("PU_C000014");
                                    if (dt == null || dt.Rows.Count == 0) break;

                                    string newval = D.GetString(((FlexGrid)sender).EditData);
                                    DataRow[] dRow = dt.Select("CODE = '" + newval + "'");
                                    if (dRow.Length == 0) break;

                                    int day = D.GetInt(dRow[0]["CD_FLAG1"]);
                                    DateTime now = DateTime.Now;
                                    string date = tb_DT_PO.Text.Substring(0, 4) + "-" + tb_DT_PO.Text.Substring(4, 2) + "-" + tb_DT_PO.Text.Substring(6, 2);//.ToShortDateString();
                                    
                                    now = DateTime.Parse(date);
                                    
                                    _flex["DT_PAY_PREARRANGED"] = now.AddDays(day).ToString("yyyyMMdd");
                                    // 만기예정일
                                    _flex["DT_DUE"] = _flex["DT_PAY_PREARRANGED"];

                                }
                                break;
                            case "DT_PAY_PREARRANGED":
                                // 만기예정일
                                if (D.GetString(_flex.EditData) != string.Empty)
                                {
                                    _flex[_flex.Row, "DT_DUE"] = D.GetString(_flex.EditData);
                                }
                                break;
                        }
                    }
                }


            }
            catch
            {
            }

        }

        #endregion

        #region -> _flex_AfterRowChange
        void _flex_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (!_flex.IsBindingEnd || !_flex.HasNormalRow) return;
                
                
                
                string str_cd_partner = D.GetString(_flex["CD_PARTNER"]);
                string str_no_lc = D.GetString(_flex["NO_LC"]);
                string fg_tax = D.GetString(_flex["FG_TAX"]);
                string Filter = string.Empty;                
                //// LOCAL LC 이면 ( 반품 구분 하지 않음 )
                //if (cbo_FG_TRANS.SelectedValue.ToString().Trim() == "003")
                //{
                //    Filter = "CD_PARTNER='" + str_cd_partner + "' AND NO_LC ='" + str_no_lc + "' AND FG_TAX ='" + fg_tax + "'";
                //}
                //else // 국내, 구매승인서 일경우 ( 반품 구분 하지 않음 )
                //{
                //    Filter = "CD_PARTNER='" + str_cd_partner + "' AND FG_TAX ='" + fg_tax + "'";
                //    //	ldr_temp = _dt_Line.Select("CD_PARTNER='"+_dt_Line.Rows[i]["CD_PARTNER"].ToString()+"' AND FG_TAX ='"+_dt_Line.Rows[i]["FG_TAX"].ToString()+"' AND YN_RETURN ='"+_dt_Line.Rows[i]["YN_RETURN"].ToString()+"'");							
                //}

                Filter = "NO_TEMP = '" + D.GetString(_flex["NO_TEMP"]) + "'";

                _flexL.RowFilter = Filter;
                //_flexL.SetDummyColumn("S");



            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                //detailBinding = false;
            }
        }
        #endregion

        #region -> _flex_AfterEdit

        void _flex_AfterEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (_flex[_flex.Row, "CHK"].ToString() == "Y") //클릭하는 순간은 N이므로
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

        #endregion

        #region -> _flex_StartEdit

        void _flex_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {

                switch (_flex.Cols[e.Col].Name)
                {
                    case "TXT_USERDEF1":
                        if (!_flex["NM_TAX"].Equals("현금영수증"))
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

        #region -> _flexL_StartEdit
        private void _flexL_StartEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            try
            {
                if (_flexL.Cols[e.Col].Name == "S" && D.GetString(_flex[_flex.Row, "CHK"]) == "Y")
                {
                    // e.Cancel = true;
                    if (D.GetString(_flexL[_flexL.Row, "S"]) == "Y") //edit 시작점이므로 n으로 변경하려면 기존값은 y
                        _flex[_flex.Row, "CHK"] = "N";
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region -> _flexL_ValidateEdit

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
                                _flex["CHK"] = "Y";
                            else
                                _flex["CHK"] = "N";

                            break;
                        

                    }
                }
            }
            catch
            {
            }
        }

        #endregion
        
        #endregion

        #region ♣ 버튼클릭(세금계산서/입고적용)

        #region -> 세금계산서 클릭

        private void btn_TAXPAPER_Click(object sender, System.EventArgs e)
        {
            try
            {
                object[] args = new object[5];
                args[0] = bp_CD_BIZAREA.CodeValue + "|" + bp_CD_BIZAREA.CodeName;//매입관리의 사업장컨트롤이 헬프도움창으로 바뀌면서 이름도 같이 넘겨줘야한다. 배열갯수를 늘리려고하다보니 행여나 모르는 부분에서 갔다쓸수도 있을것 같아서.... //cbo_CD_BIZAREA.SelectedValue.ToString();
                args[1] = tb_NO_EMP.CodeValue.ToString();
                args[2] = tb_NO_EMP.CodeName;
                args[3] = cbo_FG_TRANS.SelectedValue.ToString();
                args[4] = tb_DT_PO.MaskEditBox.ClipText;
                //string ls_pagename = MainFrameInterface.GetDataDictionaryItem(DataDictionaryTypes.PU, "P_PU_IV_MNG");

                if (MainFrameInterface.IsExistPage("P_PU_IV_MNG", false))
                {
                    //MessageBox.Show("라인 페이지 죽이기"); merge
                    //- 특정 페이지 닫기
                    MainFrameInterface.UnLoadPage("P_PU_IV_MNG", false);
                }

                MainFrameInterface.LoadPageFrom("P_PU_IV_MNG", "매입관리", Grant, args);
            }
            catch
            {
            }
        }

        #endregion 

        #region -> 입고적용 클릭
        private void btn_APPLY_GI_Click(object sender, System.EventArgs e)
        { 
            try
            {
                if (!FieldCheck())
                {
                    return;
                }

                if (cbo_FG_TRANS.SelectedValue.ToString().Trim() == "001")
                {

                    string 계산서처리구분 = string.Empty;

                    if(rbtn_M_IV.Checked)
                        계산서처리구분 = "002";
                    else
                        계산서처리구분 = "001";

                    pur.P_PU_GIIV_SUB m_dlg = new pur.P_PU_GIIV_SUB(bp_CD_BIZAREA.CodeValue, cbo_FG_TRANS.SelectedValue.ToString(), 계산서처리구분, tb_DT_PO.Text, tb_NO_EMP.CodeValue, tb_NO_EMP.CodeName);//cbo_CD_BIZAREA.SelectedValue.ToString()

                    //pur.P_PU_GIIV_SUB1 m_dlgd = new pur.P_PU_GIIV_SUB1(bp_CD_BIZAREA.CodeValue, cbo_FG_TRANS.SelectedValue.ToString(), 계산서처리구분, tb_DT_PO.Text);

                    //pur.P_PU_GIIVLC_SUB m_dlgc = new pur.P_PU_GIIVLC_SUB(bp_CD_BIZAREA.CodeValue, cbo_FG_TRANS.SelectedValue.ToString(), 계산서처리구분, tb_DT_PO.Text);
                    // 일괄이면
                    if(rbtn_T_IV.Checked)
                    {
                        m_dlg._계산서처리구분 = "001";
                    }
                    else// 건별이면
                    {
                        m_dlg._계산서처리구분 = "002";
                    }
                  
                    m_dlg._거래구분 = cbo_FG_TRANS.SelectedValue.ToString();
                    m_dlg.부가세율 = _dsCombo.Tables[1].Copy();
                    m_dlg._과세구분 = "";
                    m_dlg._사업장 = bp_CD_BIZAREA.CodeValue;//cbo_CD_BIZAREA.SelectedValue.ToString();

                     if(m_dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        SettingDiviedData(m_dlg.gdt_return, rbtn_M_IV.Checked);
                        PageControlEnalbed(false);
                    }
                }
                else if (cbo_FG_TRANS.SelectedValue.ToString().Trim() == "002")
                {
                    string 계산서처리구분 = string.Empty;

                    if(rbtn_M_IV.Checked)
                        계산서처리구분 = "002";
                    else
                        계산서처리구분 = "001";

                    pur.P_PU_GIIV_SUB1 m_dlg = new pur.P_PU_GIIV_SUB1(bp_CD_BIZAREA.CodeValue, cbo_FG_TRANS.SelectedValue.ToString(), 계산서처리구분, tb_DT_PO.Text);

                    // 일괄이면
                    if (rbtn_T_IV.Checked)
                    {
                        m_dlg._계산서처리구분 = "001";
                    }
                    else// 건별이면
                    {
                        m_dlg._계산서처리구분 = "002";
                    }

                    m_dlg._거래구분 = cbo_FG_TRANS.SelectedValue.ToString();
                    m_dlg.부가세율 = _dsCombo.Tables[1].Copy();
                    m_dlg._과세구분 = "";
                    m_dlg._사업장 = bp_CD_BIZAREA.CodeValue;

                    if (m_dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        SettingDiviedData(m_dlg.gdt_return, rbtn_M_IV.Checked);
                        PageControlEnalbed(false);
                    }
                }
                else if (cbo_FG_TRANS.SelectedValue.ToString().Trim() == "003")
                {

                    string 계산서처리구분 = string.Empty;

                    P_PU_GIIVLC_SUB m_dlg = new P_PU_GIIVLC_SUB(bp_CD_BIZAREA.CodeValue, cbo_FG_TRANS.SelectedValue.ToString(), 계산서처리구분, tb_DT_PO.Text);

                    m_dlg._계산서처리구분 = "001', '002";
                    m_dlg._거래구분 = cbo_FG_TRANS.SelectedValue.ToString();
                    m_dlg.부가세율 = _dsCombo.Tables[1].Copy();
                    m_dlg._과세구분 = "";
                    m_dlg._사업장 = bp_CD_BIZAREA.CodeValue;

                    if(m_dlg.ShowDialog(this) == DialogResult.OK)
                    {
                       

                        SettingDiviedData(m_dlg.gdt_return, rbtn_M_IV.Checked);
                       
                        PageControlEnalbed(false);
                    }

                }

                if (Global.MainFrame.ServerKeyCommon == "KIHA")
                {
                    DataTable dtCd_Pc = _biz.getCd_Pc(bp_CD_BIZAREA.CodeValue);

                    if (dtCd_Pc != null && dtCd_Pc.Rows.Count > 0)
                    {
                        foreach (DataRow row in _flex.DataTable.Rows)
                        {
                            row["CD_PC_USER"] = D.GetString(dtCd_Pc.Rows[0]["CD_PC"]);
                            row["NM_PC_USER"] = D.GetString(dtCd_Pc.Rows[0]["NM_PC"]);
                        }
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

        #region ♣ 컨트롤 이벤트

        #region -> Control_QueryAfter

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
                DataTable dt_biz = _biz.getCd_Pc(bp_CD_BIZAREA.CodeValue);

                if (dt_biz != null || dt_biz.Rows.Count > 0)
                {
                    bp_CD_BIZAREA_TAX.CodeValue = D.GetString(dt_biz.Rows[0]["CD_BIZAREA"]);
                    bp_CD_BIZAREA_TAX.CodeName = D.GetString(dt_biz.Rows[0]["NM_BIZAREA"]);
                }
            }

        }

        #endregion

        #region -> OnControl_Validated

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

        #endregion

        #region -> OnControl_CalendarClosed

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

        #endregion

        #region ♣ 기타메소드

        #region -> SetControlEnabled

        private void SetControlEnabled(bool pb_enabled)
        {
            bp_CD_BIZAREA.Enabled = pb_enabled;//cbo_CD_BIZAREA.Enabled = pb_enabled;
            tb_NO_EMP.Enabled = pb_enabled;
            cbo_FG_TRANS.Enabled = pb_enabled;
            cbo_FG_TPPURCHAS.Enabled = pb_enabled;
        }

        #endregion

        #region -> 금액변경 ( ChangeAM_K )

        private void ChangeAM_K(decimal pdb_amk)
        {
            try
            {
                decimal ldb_kwN = 0;
                string ls_NO_TEMP = _flex[_flex.Row, "NO_TEMP"].ToString();
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
                    _flex[_flex.Row, "AM_TOTAL"] = (pdb_amk) + Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flex[_flex.Row, "VAT_TAX"].ToString().Trim()));
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region -> 부가세 변경 ( ChangeVAT_K )

        private void ChangeVAT_K(decimal pdb_vatk)
        {
            try
            {
                decimal ldb_kwN = 0;
                string ls_NO_TEMP = _flex[_flex.Row, "NO_TEMP"].ToString();
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
                    _flex[_flex.Row, "AM_TOTAL"] = (pdb_vatk) + Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flex[_flex.Row, "AM_K"].ToString().Trim()));
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        #region -> InDataHeadValue

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
                DataRow[] ldrs_row = _dsCombo.Tables[2].Select("CODE = '" + bp_CD_BIZAREA.CodeValue + "'");
                if (ldrs_row != null && ldrs_row.Length > 0)
                {
                    ls_nobizarea = ldrs_row[0]["NO_BIZAREA"].ToString();
                }
                
                ldr_row["NO_BIZAREA"] = ls_nobizarea;
                ldr_row["DT_PROCESS"] = tb_DT_PO.Text.ToString();
                ldr_row["TP_SUMTAX"] = "S";
                ldr_row["CD_DEPT"] = _cddept;
                ldr_row["NO_EMP"] = tb_NO_EMP.CodeValue.ToString();
                ldr_row["ID_USER"] = this.LoginInfo.UserID;
                ldr_row["YN_PURSUB"] = "N";
                ldr_row["MODULE"] = "PU";

                ldr_row["FG_FGTAXP"] = "001";

                if (rbtn_M_IV.Checked)
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

        #endregion

        #region -> SetSaveFalse

        private void SetSaveFalse()
        {
            try
            {
                for (int i = 0; i < _flex.DataTable.Rows.Count; i++)
                {
                    _flex.DataTable.Rows[i].BeginEdit();
                    _flex.DataTable.Rows[i]["NO_IV"] = "";
                    _flex.DataTable.Rows[i].EndEdit();
                }
            }
            catch(Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        #region -> 필드 체크

        private bool FieldCheck()
        {
            //if (tb_NO_EMP.CodeValue.ToString() == "")
            //{
            //    this.ShowMessage("WK1_004", lb_NO_EMP.Text);
            //    //	Duzon.Common.Controls.MessageBoxEx.Show(lb_NO_EMP.Text +  this.GetMessageDictionaryItem("CM_M000002"),this.PageName);
            //    tb_NO_EMP.Focus();
            //    return false;
            //}

            if (bp_CD_BIZAREA.CodeValue.ToString() == string.Empty)
            {
                this.ShowMessage("WK1_004", lb_NM_BIZAREA.Text);
                //	Duzon.Common.Controls.MessageBoxEx.Show(lb_NO_EMP.Text +  this.GetMessageDictionaryItem("CM_M000002"),this.PageName);
                bp_CD_BIZAREA.Focus();
                return false;
            }

            return true;
        }

        #endregion

        #region -> SettingDiviedData

        private void SettingDiviedData(DataTable pdt_Line, bool is_individual)
        { 
            try
            {
                IsLoadData = true ;
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

                if (Global.MainFrame.ServerKeyCommon == "CGBIO")
                {
                    for (int i = 0; i < _dt_Line.Rows.Count; i++)
                        _dt_Line.Rows[i]["NUM_USERDEF1"] = _dt_Line.Rows[i]["UM_EX_PSO"];
                }

            

                if (!_dt_Line.Columns.Contains("NUM_USERDEF1"))
                {
                    _dt_Line.Columns.Add("NUM_USERDEF1", typeof(decimal));
                }

                ///////////////////////////////////////////////////////////////
                ///	헤더 생성 
                /// 
                // 헤더 CLEAR
                _flex.DataTable.Clear();
                _flexL.Binding = null;
                _flex.Redraw = false;
                CommonFunction objFunc = new CommonFunction();
                String str_temp = String.Empty;

                MsgControl.ShowMsg(" 적용 데이터를 집계중입니다. \r\n잠시만 기다려주세요!");

                if (!_dt_Line.Columns.Contains("S"))
                {
                    _dt_Line.Columns.Add("S", typeof(string));
                    foreach (DataRow dr in _dt_Line.Rows) //각도움창의 체크버튼 기준이 YN, TF 이두개의값으로 나누어져있어서 여기서 들어올때 통일해서 들어오도록한다. 원래는 프로시져에서 부터 고치는게 맞지만...
                    {
                        dr["S"] = "Y";
                    }
                }

                //bool bContain = pdt_Line.Columns.Contains("CD_PARTNER_PJT");
                //건별이면
                if (is_individual)
                {
                    long ll_temp = 1;
                    DataRow[] ldr_temp;
                    for (int i = 0; i < _dt_Line.Rows.Count; i++)
                    {
                        // 기존에 생성되지 않았으면
                        if (_dt_Line.Rows[i]["NO_TEMP"].ToString().Trim() == "")
                        {
                            ldr_temp = _dt_Line.Select("NO_IO='" + _dt_Line.Rows[i]["NO_IO"].ToString() + "'");


                            _flex.Rows.Add();
                            _flex.Row = _flex.Rows.Count - 1;

                            _flex[_flex.Row, "CHK"] = "Y";
                            _flex[_flex.Row, "NO_IO"] = _dt_Line.Rows[i]["NO_IO"].ToString();
                            _flex[_flex.Row, "NO_IV"] = "";
                            _flex[_flex.Row, "CD_PARTNER"] = _dt_Line.Rows[i]["CD_PARTNER"].ToString();
                            _flex[_flex.Row, "LN_PARTNER"] = _dt_Line.Rows[i]["LN_PARTNER"].ToString();
                            _flex[_flex.Row, "NO_BIZAREA"] = _dt_Line.Rows[i]["NO_COMPANY"].ToString();
                            _flex[_flex.Row, "FG_TAX"]     = _dt_Line.Rows[i]["FG_TAX"].ToString();
                            _flex[_flex.Row, "NM_TAX"]     = _dt_Line.Rows[i]["NM_TAX"].ToString();
                            _flex[_flex.Row, "NO_TEMP"] = ll_temp.ToString();
                            _flex[_flex.Row, "FG_TRANS"] = cbo_FG_TRANS.SelectedValue.ToString();//YN_RETURN
                            _flex[_flex.Row, "CD_CC"] = _dt_Line.Rows[i]["CD_CC"].ToString();   //CD_CC 값 추가
                            
                            //_flex[_flex.Row, "CD_DOCU"] = cd_docu;                                //2009.11.27 전표유형추가
                            _flex[_flex.Row, "FG_TPPURCHASE"] = _dt_Line.Rows[i]["FG_TPPURCHASE"].ToString();



                            _flex[_flex.Row, "CD_BIZAREA"] = bp_CD_BIZAREA.CodeValue;  // 사업장(2009.12.15 추가)
                            _flex[_flex.Row, "CD_BIZAREA_TAX"] = bp_CD_BIZAREA_TAX.CodeValue;  // 부가세사업장(2009.12.15 추가)
                            _flex[_flex.Row, "NM_BIZAREA"] = bp_CD_BIZAREA.CodeName;  // 사업장(2009.12.15 추가)
                            _flex[_flex.Row, "NM_BIZAREA_TAX"] = bp_CD_BIZAREA_TAX.CodeName;  // 부가세사업장(2009.12.15 추가)
                            
                            //	_flex[_flex.Row,"YN_RETURN"] = _dt_Line.Rows[i]["YN_RETURN"].ToString();
                            
                            decimal ldb_amk = 0;
                            decimal ldb_amEx = 0;
                            decimal ldb_vat = 0;

                            for (int j = 0; j < ldr_temp.Length; j++)
                            {
                                ldr_temp[j]["NO_TEMP"] = ll_temp.ToString();
                                ldb_amk += D.GetDecimal(System.Double.Parse(ldr_temp[j]["AM_IV"].ToString()));
                                ldb_amEx += D.GetDecimal(System.Double.Parse(ldr_temp[j]["AM_EX"].ToString()));
                                //ldb_vat += System.Math.Floor(System.Double.Parse(ldr_temp[j]["VAT_IV"].ToString()));
                                ldb_vat += Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(ldr_temp[j]["VAT_IV"].ToString()));
                            }

                            string cd_docu = Get_Type_Dcou(ldr_temp);
                            _flex[_flex.Row, "CD_DOCU"] = cd_docu;


                            // 과세 이고.. 거래처데이블의 TP_TAX ==001
                            if (_dt_Line.Rows[i]["TP_TAX"].ToString().Trim() == "001" &&
                                System.Double.Parse(_dt_Line.Rows[i]["TAX_RATE"].ToString().Trim()) > 0)
                            {
                                decimal ldb_VatRate = D.GetDecimal(System.Double.Parse(_dt_Line.Rows[i]["TAX_RATE"].ToString().Trim()));
                                //	double ldb_tempVat = System.Math.Floor(ldb_amk *ldb_VatRate * 0.01);

                                decimal ldb_tempVat = ldb_amk * ldb_VatRate * 0.01M;

                                if (ldb_tempVat < 0)
                                {
                                    //ldb_tempVat = System.Math.Ceiling(ldb_tempVat);
                                    ldb_tempVat = Unit.원화금액(DataDictionaryTypes.PU, ldb_tempVat);
                                }
                                //ldb_tempVat = System.Math.Floor(ldb_tempVat);
                                ldb_tempVat = Unit.원화금액(DataDictionaryTypes.PU, ldb_tempVat);
                                //ldr_temp[ldr_temp.Length - 1]["VAT_IV"] = System.Math.Floor(System.Double.Parse(ldr_temp[ldr_temp.Length - 1]["VAT_IV"].ToString())) + (ldb_tempVat - ldb_vat);
                                ldr_temp[ldr_temp.Length - 1]["VAT_IV"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(System.Double.Parse(ldr_temp[ldr_temp.Length - 1]["VAT_IV"].ToString()))) + (ldb_tempVat - ldb_vat);
                                ldr_temp[ldr_temp.Length - 1]["AM_TOTAL"] = D.GetDecimal(ldr_temp[ldr_temp.Length - 1]["VAT_IV"]) + D.GetDecimal(ldr_temp[ldr_temp.Length - 1]["AM_IV"]);
                                ldb_vat = ldb_tempVat;
                            } 

                            
                            // 실제 DB에 들어 가는 값
                            //_flex[_flex.Row, "AM_K"] = System.Math.Floor(ldb_amk);
                            _flex[_flex.Row, "AM_K"] = Unit.원화금액(DataDictionaryTypes.PU, ldb_amk);
                            _flex[_flex.Row, "AM_EX"] = ldb_amEx;
                            //_flex[_flex.Row, "VAT_TAX"] = System.Math.Floor(ldb_vat);
                            _flex[_flex.Row, "VAT_TAX"] = Unit.원화금액(DataDictionaryTypes.PU, ldb_vat);


                            //구매승인서 ,국내, LOCAL L/C 든 뭐든지 환종을 강제로 원화로 바꾸거나 하는작업을 해서는 안된다.
                            _flex[_flex.Row, "CD_EXCH"] = _dt_Line.Rows[i]["CD_EXCH"].ToString().Trim();
                            _flex[_flex.Row, "RT_EXCH"] = _flex.CDecimal(_dt_Line.Rows[i]["RT_EXCH"]);
                            // _flex[_flex.Row, "AM_EX"] = Convert.ToDouble(_flex[_flex.Row, "AM_EX"]) + ldb_amEx;
                            

                            if (Convert.ToDecimal(_dt_Line.Rows[i]["RT_EXCH"]) == 0 || _dt_Line.Rows[i]["RT_EXCH"] == null)
                                _dt_Line.Rows[i]["RT_EXCH"] = 1;

                            _flex[_flex.Row, "AM_K"] = ldb_amk; // System.Math.Floor(_flex.CDecimal(_flex[_flex.Row, "AM_EX"]) * _flex.CDecimal(_dt_Line.Rows[i]["RT_EXCH"]));
                            //_flex[_flex.Row, "AM_TOTAL"] = Convert.ToDouble(_flex[_flex.Row, "AM_K"]) + ldb_vat;
                            _flex[_flex.Row, "AM_TOTAL"] = D.GetDecimal(_flex[_flex.Row, "AM_K"]) + ldb_vat;


                            _flex[_flex.Row, "FG_PAYBILL"] = D.GetString(_dt_Line.Rows[i]["FG_PAYBILL"]);
                            _flex[_flex.Row, "DT_PAY_DAY"] = Convert.ToInt16(_dt_Line.Rows[i]["DT_PAY_PREARRANGED"]);

                            if (_지급관리통제설정 == "Y")
                            {
                                string dt_due = ComFunc.만기예정일자(tb_DT_PO.Text, ldb_amk, D.GetString(_dt_Line.Rows[i]["FG_PAYBILL"]), "1");
                                // 만기예정일
                                if (dt_due != string.Empty)
                                     _flex[_flex.Row, "DT_PAY_PREARRANGED"] = dt_due;

                                //어음 만기일(구분이 어음이 아닐경우 Empty가 return됨)
                                dt_due = ComFunc.만기예정일자(tb_DT_PO.Text, ldb_amk, D.GetString(_dt_Line.Rows[i]["FG_PAYBILL"]), "2");
                                if (dt_due != string.Empty)
                                    _flex[_flex.Row, "DT_DUE"] = dt_due;

                            }
                            else
                            {
                                if (_지급예정일통제설정 != "100")  // 물류쪽 지급예정일 사용
                                {
                                    str_temp = objFunc.DateAdd(tb_DT_PO.Text, "D", Convert.ToInt16(_flex[_flex.Row, "DT_PAY_DAY"]));
                                    _flex[_flex.Row, "DT_PAY_PREARRANGED"] = str_temp;
                                    _flex[_flex.Row, "DT_DUE"] = str_temp;
                                }
                                else
                                {
                                    str_temp = objFunc.DateAdd(tb_DT_PO.Text, "M", Convert.ToInt16(D.GetDecimal(_dt_Line.Rows[i]["TP_PAY_DD"])));
                                    str_temp = str_temp.Substring(0, 6) + string.Format("{0:00}", D.GetDecimal(_dt_Line.Rows[i]["DT_PAY_DD"]));
                                    if (D.GetDecimal(str_temp) <= D.GetDecimal(tb_DT_PO.Text))
                                        str_temp = tb_DT_PO.Text;
                                    else
                                    {
                                        string str_lastday = objFunc.GetLastDayDateText(tb_DT_PO.Text);
                                        if (D.GetDecimal(str_temp) > D.GetDecimal(str_lastday))
                                            str_temp = str_lastday;
                                    }

                                    _flex[_flex.Row, "DT_PAY_PREARRANGED"] = str_temp;
                                    _flex[_flex.Row, "DT_DUE"] = _flex[_flex.Row, "DT_PAY_PREARRANGED"];
                                    _flex[_flex.Row, "TP_PAY_DD"] = _dt_Line.Rows[i]["TP_PAY_DD"];
                                    _flex[_flex.Row, "DT_PAY_DD"] = _dt_Line.Rows[i]["DT_PAY_DD"];
                                }
                            }


                           

                            if (Config.MA_ENV.PJT형여부 == "Y")
                            {
                                _flex["CD_PJT_ITEM"] = _dt_Line.Rows[i]["CD_PJT_ITEM"];
                                _flex["NM_PJT_ITEM"] = _dt_Line.Rows[i]["NM_PJT_ITEM"];
                                _flex["PJT_ITEM_STND"] = _dt_Line.Rows[i]["PJT_ITEM_STND"];
                                _flex["NO_WBS"] = _dt_Line.Rows[i]["NO_WBS"];
                                _flex["NO_CBS"] = _dt_Line.Rows[i]["NO_CBS"];
                                _flex["CD_ACTIVITY"] = _dt_Line.Rows[i]["CD_ACTIVITY"];
                                _flex["NM_ACTIVITY"] = _dt_Line.Rows[i]["NM_ACTIVITY"];
                                _flex["CD_COST"] = _dt_Line.Rows[i]["CD_COST"];
                                _flex["NM_COST"] = _dt_Line.Rows[i]["NM_COST"];
                            }
                            _flex[_flex.Row, "YN_JEONJA"] = _dt_Line.Rows[i]["YN_JEONJA"];
                            _flex[_flex.Row, "CD_PJT"] = _dt_Line.Rows[i]["CD_PJT"];
                            _flex[_flex.Row, "NM_PROJECT"] = _dt_Line.Rows[i]["NM_PROJECT"];

                            //(주)젬백스테크놀러지 전용 20140319 박종규
                            //지급 조건의 경우 받아온 건 중 최상단 한 건의 값이 default로 들어오도록 처리.
                            if (Global.MainFrame.ServerKeyCommon == "GEMTECH")
                            {
                                _flex[_flex.Row, "FG_PAYMENT"] = _dt_Line.Rows[0]["FG_PAYMENT"];
                            }

                            //if (MainFrameInterface.ServerKeyCommon == "UNIPOINT" && bContain)
                            //{
                            //    _flex["CD_PARTNER_PJT"] = _dt_Line.Rows[i]["CD_PARTNER_PJT"];
                            //    _flex["LN_PARTNER_PJT"] = _dt_Line.Rows[i]["LN_PARTNER_PJT"];
                            //    _flex["NO_EMP_PJT"] = _dt_Line.Rows[i]["NO_EMP_PJT"];
                            //    _flex["NM_KOR_PJT"] = _dt_Line.Rows[i]["NM_KOR_PJT"];
                            //    _flex["END_USER"] = _dt_Line.Rows[i]["END_USER"];
                            //} 

                            _flex.AddFinished();
                            _flex.Col = _flex.Cols.Fixed;
                            ll_temp++;

                        }  // NO_TEMP가 널이면 .. 끝
                    }   // FOR문 끝
                }   // 건별이면 / 끝
                else   // 일괄이면 시작
                {
                    DataRow[] ldr_temp;
                    long ll_temp = 1;
                    
                    for (int i = 0; i < _dt_Line.Rows.Count; i++)
                    {
                        if (_dt_Line.Rows[i]["NO_TEMP"].ToString().Trim() == "")
                        {
                            // LOCAL LC 이면 ( 반품 구분 하지 않음 )
                            if (D.GetString(cbo_FG_TRANS.SelectedValue) == "003")
                            {
                                ldr_temp = _dt_Line.Select("CD_PARTNER='" + _dt_Line.Rows[i]["CD_PARTNER"].ToString() + "' AND NO_LC ='" + _dt_Line.Rows[i]["NO_LC"].ToString() + "' AND ISNULL(FG_TAX,'') ='" + D.GetString(_dt_Line.Rows[i]["FG_TAX"]) + "'");
                            }
                            else // 국내, 구매승인서 일경우 ( 반품 구분 하지 않음 )
                            {
                                ldr_temp = _dt_Line.Select("CD_PARTNER='" + _dt_Line.Rows[i]["CD_PARTNER"].ToString() + "' AND ISNULL(FG_TAX,'') ='" + D.GetString(_dt_Line.Rows[i]["FG_TAX"]) + "'");
                                //	ldr_temp = _dt_Line.Select("CD_PARTNER='"+_dt_Line.Rows[i]["CD_PARTNER"].ToString()+"' AND FG_TAX ='"+_dt_Line.Rows[i]["FG_TAX"].ToString()+"' AND YN_RETURN ='"+_dt_Line.Rows[i]["YN_RETURN"].ToString()+"'");							
                            }


                            _flex.Rows.Add();
                           
                            _flex.Row =  _flex.Rows.Count - 1;

                            _flex[_flex.Row, "CHK"] = "Y";
                            //_flex[_flex.Row, "NO_IV"] = "";
                            _flex[_flex.Row, "NO_IO"] = _dt_Line.Rows[i]["NO_IO"].ToString();
                            _flex[_flex.Row, "CD_PARTNER"] = _dt_Line.Rows[i]["CD_PARTNER"].ToString();
                            _flex[_flex.Row, "LN_PARTNER"] = _dt_Line.Rows[i]["LN_PARTNER"].ToString();
                            _flex[_flex.Row, "NO_BIZAREA"] = _dt_Line.Rows[i]["NO_COMPANY"].ToString();
                            _flex[_flex.Row, "FG_TAX"] = _dt_Line.Rows[i]["FG_TAX"].ToString();
                            _flex[_flex.Row, "NM_TAX"] = _dt_Line.Rows[i]["NM_TAX"].ToString();
                            _flex[_flex.Row, "NO_TEMP"] = ll_temp.ToString();
                            _flex[_flex.Row, "FG_TRANS"] = cbo_FG_TRANS.SelectedValue.ToString();
                            _flex[_flex.Row, "CD_CC"] = _dt_Line.Rows[i]["CD_CC"].ToString();     //CD_CC값 추가

                            //_flex[_flex.Row,"YN_RETURN"] = _dt_Line.Rows[i]["YN_RETURN"].ToString();
                            //_flex[_flex.Row, "CD_DOCU"] = cd_docu;                                //2009.11.27 전표유형추가
                            _flex[_flex.Row, "FG_TPPURCHASE"] = _dt_Line.Rows[i]["FG_TPPURCHASE"].ToString();     


                            _flex[_flex.Row, "CD_BIZAREA"] = bp_CD_BIZAREA.CodeValue;  // 사업장(2009.12.15 추가)
                            _flex[_flex.Row, "CD_BIZAREA_TAX"] = bp_CD_BIZAREA_TAX.CodeValue;  // 부가세사업장(2009.12.15 추가)
                            _flex[_flex.Row, "NM_BIZAREA"] = bp_CD_BIZAREA.CodeName;  // 사업장(2009.12.15 추가)
                            _flex[_flex.Row, "NM_BIZAREA_TAX"] = bp_CD_BIZAREA_TAX.CodeName;  // 부가세사업장(2009.12.15 추가)
                            _flex[_flex.Row, "NO_LC"] = D.GetString(_dt_Line.Rows[i]["NO_LC"]); //로우체인지할때사용함

                            decimal ldb_amk = 0;
                            decimal ldb_amEx = 0;
                            decimal ldb_vat = 0;

                            for (int j = 0; j < ldr_temp.Length; j++)
                            {
                                ldr_temp[j]["NO_TEMP"] = D.GetString(ll_temp);
                                ldb_amk += D.GetDecimal(D.GetDecimal(ldr_temp[j]["AM_IV"]));
                                ldb_amEx += D.GetDecimal(D.GetDecimal(ldr_temp[j]["AM_EX"]));
                               // ldb_vat += D.GetDecimal(System.Math.Floor(System.Double.Parse(ldr_temp[j]["VAT_IV"].ToString())));
                                ldb_vat += Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(ldr_temp[j]["VAT_IV"]));
                            }

                            string cd_docu = Get_Type_Dcou(ldr_temp);
                            _flex[_flex.Row, "CD_DOCU"] = cd_docu;


                            if (_dt_Line.Rows[i]["TP_TAX"].ToString().Trim() == "001" &&
                                System.Double.Parse(_dt_Line.Rows[i]["TAX_RATE"].ToString().Trim()) > 0)
                            {
                                decimal ldb_VatRate = D.GetDecimal(System.Double.Parse(_dt_Line.Rows[i]["TAX_RATE"].ToString().Trim()));
                                decimal ldb_tempVat = ldb_amk * ldb_VatRate * 0.01M;

                                if (ldb_tempVat < 0)
                                {
                                    //ldb_tempVat = System.Math.Ceiling(ldb_tempVat);
                                    ldb_tempVat = Unit.원화금액(DataDictionaryTypes.PU, ldb_tempVat);
                                }

                                //ldb_tempVat = System.Math.Floor(ldb_tempVat);
                                ldb_tempVat = Unit.원화금액(DataDictionaryTypes.PU, ldb_tempVat);
                                //ldr_temp[ldr_temp.Length - 1]["VAT_IV"] = System.Math.Floor(System.Double.Parse(ldr_temp[ldr_temp.Length - 1]["VAT_IV"].ToString())) + (ldb_tempVat - ldb_vat);
                                ldr_temp[ldr_temp.Length - 1]["VAT_IV"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(ldr_temp[ldr_temp.Length - 1]["VAT_IV"].ToString())) + (ldb_tempVat - ldb_vat);
                                ldr_temp[ldr_temp.Length - 1]["AM_TOTAL"] = D.GetDecimal(ldr_temp[ldr_temp.Length - 1]["VAT_IV"]) + D.GetDecimal(ldr_temp[ldr_temp.Length - 1]["AM_IV"]);
                                ldb_vat = ldb_tempVat;

                            }

                            // 실제 DB에 들어 가는 값
                            //_flex[_flex.Row, "AM_K"] = System.Math.Floor(ldb_amk);
                            _flex[_flex.Row, "AM_EX"] = ldb_amEx;
                            //_flex[_flex.Row, "VAT_TAX"] = System.Math.Floor(ldb_vat);
                            //_flex[_flex.Row, "VAT_TAX"] = Unit.원화금액(DataDictionaryTypes.PU, ldb_vat);

                            _flex[_flex.Row, "VAT_TAX"] =   ldb_vat ;
                               //구매승인서 ,국내, LOCAL L/C 든 뭐든지 환종을 강제로 원화로 바꾸거나 하는작업을 해서는 안된다.
                            _flex[_flex.Row, "CD_EXCH"] = _dt_Line.Rows[i]["CD_EXCH"].ToString().Trim();

                            if(Convert.ToDecimal(_dt_Line.Rows[i]["RT_EXCH"]) == 0 || _dt_Line.Rows[i]["RT_EXCH"] == null)
                                _dt_Line.Rows[i]["RT_EXCH"] = 1;

                            _flex[_flex.Row, "RT_EXCH"] = D.GetDecimal(_dt_Line.Rows[i]["RT_EXCH"]);

                            //_flex[_flex.Row, "AM_K"] = System.Math.Floor(_flex.CDecimal(_flex[_flex.Row, "AM_EX"]) * _flex.CDecimal(_dt_Line.Rows[i]["RT_EXCH"]));
                            _flex[_flex.Row, "AM_K"] = ldb_amk;
                            //_flex[_flex.Row, "AM_TOTAL"] = Convert.ToDouble(_flex[_flex.Row, "AM_K"]) + ldb_vat;
                            _flex[_flex.Row, "AM_TOTAL"] = D.GetDecimal(_flex[_flex.Row, "AM_K"]) + ldb_vat;
                           
                            _flex[_flex.Row, "FG_PAYBILL"] = D.GetString(_dt_Line.Rows[i]["FG_PAYBILL"]);
                            _flex[_flex.Row, "DT_PAY_DAY"] = D.GetInt(_dt_Line.Rows[i]["DT_PAY_PREARRANGED"]);
                            if (_지급관리통제설정 == "Y")
                            {
                                string dt_due = ComFunc.만기예정일자(tb_DT_PO.Text, ldb_amk, D.GetString(_dt_Line.Rows[i]["FG_PAYBILL"]), "1");
                                // 만기예정일
                                if (dt_due != string.Empty)
                                    _flex[_flex.Row, "DT_PAY_PREARRANGED"] = dt_due;

                                //어음 만기일(구분이 어음이 아닐경우 Empty가 return됨)
                                dt_due = ComFunc.만기예정일자(tb_DT_PO.Text, ldb_amk, D.GetString(_dt_Line.Rows[i]["FG_PAYBILL"]), "2");
                                if (dt_due != string.Empty)
                                    _flex[_flex.Row, "DT_DUE"] = dt_due;

                            }
                            else
                            {
                                if (_지급예정일통제설정 != "100")  // 물류쪽 지급예정일 사용
                                {
                                    str_temp = objFunc.DateAdd(tb_DT_PO.Text, "D", D.GetInt(_flex[_flex.Row, "DT_PAY_DAY"]));
                                    _flex[_flex.Row, "DT_PAY_PREARRANGED"] = str_temp;
                                    _flex[_flex.Row, "DT_DUE"] = str_temp;
                                }
                                else
                                {
                                    str_temp = objFunc.DateAdd(tb_DT_PO.Text, "M", D.GetInt(D.GetDecimal(_dt_Line.Rows[i]["TP_PAY_DD"])));
                                    str_temp = str_temp.Substring(0, 6) + string.Format("{0:00}", D.GetDecimal(_dt_Line.Rows[i]["DT_PAY_DD"]));
                                    if (D.GetDecimal(str_temp) <= D.GetDecimal(tb_DT_PO.Text))
                                        str_temp = tb_DT_PO.Text;
                                    else
                                    {
                                        string str_lastday = objFunc.GetLastDayDateText(str_temp);
                                        if (D.GetDecimal(str_temp) > D.GetDecimal(str_lastday))
                                            str_temp = str_lastday;
                                    }

                                    _flex[_flex.Row, "DT_PAY_PREARRANGED"] = str_temp;
                                    _flex[_flex.Row, "DT_DUE"] = _flex[_flex.Row, "DT_PAY_PREARRANGED"];
                                    _flex[_flex.Row, "TP_PAY_DD"] = _dt_Line.Rows[i]["TP_PAY_DD"];
                                    _flex[_flex.Row, "DT_PAY_DD"] = _dt_Line.Rows[i]["DT_PAY_DD"];
                                }
                            }

                            if (Config.MA_ENV.PJT형여부 == "Y")
                            {
                                _flex["CD_PJT_ITEM"] = _dt_Line.Rows[i]["CD_PJT_ITEM"];
                                _flex["NM_PJT_ITEM"] = _dt_Line.Rows[i]["NM_PJT_ITEM"];
                                _flex["PJT_ITEM_STND"] = _dt_Line.Rows[i]["PJT_ITEM_STND"];
                                _flex["NO_WBS"] = _dt_Line.Rows[i]["NO_WBS"];
                                _flex["NO_CBS"] = _dt_Line.Rows[i]["NO_CBS"];
                                _flex["CD_ACTIVITY"] = _dt_Line.Rows[i]["CD_ACTIVITY"];
                                _flex["NM_ACTIVITY"] = _dt_Line.Rows[i]["NM_ACTIVITY"];
                                _flex["CD_COST"] = _dt_Line.Rows[i]["CD_COST"];
                                _flex["NM_COST"] = _dt_Line.Rows[i]["NM_COST"];
                            }
                            _flex["YN_JEONJA"] = D.GetString(_dt_Line.Rows[i]["YN_JEONJA"]);
                            _flex["CD_PJT"] = _dt_Line.Rows[i]["CD_PJT"];
                            _flex["NM_PROJECT"] = _dt_Line.Rows[i]["NM_PROJECT"];
                            
                            //(주)젬백스테크놀러지 전용 20140319 박종규
                            //지급 조건의 경우 받아온 건 중 최상단 한 건의 값이 default로 들어오도록 처리.
                            if (Global.MainFrame.ServerKeyCommon == "GEMTECH")
                            {
                                _flex[_flex.Row, "FG_PAYMENT"] = _dt_Line.Rows[0]["FG_PAYMENT"];
                            }

                            //if (MainFrameInterface.ServerKeyCommon == "UNIPOINT" && bContain)
                            //{
                            //    _flex["CD_PARTNER_PJT"] = _dt_Line.Rows[i]["CD_PARTNER_PJT"];
                            //    _flex["LN_PARTNER_PJT"] = _dt_Line.Rows[i]["LN_PARTNER_PJT"];
                            //    _flex["NO_EMP_PJT"] = _dt_Line.Rows[i]["NO_EMP_PJT"];
                            //    _flex["NM_KOR_PJT"] = _dt_Line.Rows[i]["NM_KOR_PJT"];
                            //    _flex["END_USER"] = _dt_Line.Rows[i]["END_USER"];
                            //} 

                            _flex.AddFinished();
                            _flex.Col = _flex.Cols.Fixed;

                            ll_temp++;
                        }
                    }
                }

                _flexL.Binding = _dt_Line.Copy();
                //Get_Type_Dcou(_dt_Line);
                _flex.Redraw = true;
                _flexL.Redraw = true;

                _flex.Row = _flex.Rows.Count - 1;
                _flex_AfterRowChange(null, null);

            }
            catch(Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally 
            {
                 IsLoadData = false  ;
                 MsgControl.CloseMsg();
            }
        }

        #endregion

        #region -> 전표유형 구하기 (금액이 가장 큰 전표유형 구하기)
        public String Get_Type_Dcou(DataRow[] dtL)
        {
            String  str_type_docu = "45";
            Decimal d_max_am = 0;

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
        //public void Get_Type_Dcou(DataTable dtL)
        //{
        //    String str_type_docu = "45", str_tpso = string.Empty;
        //    String str = string.Empty;
        //    string Filter = string.Empty;

        //    if (_flex.DataTable.Rows.Count < 1) return;

        //    DataRow[] dr_t, dr_t0;
        //    DataTable dt = dtL.Clone();
        //    //DataTable d전표유형tH = _flex.Clone();
        //    foreach (DataRow dr in _flex.DataTable.Rows)
        //    {
        //        str_type_docu = "45";
        //        Filter = "NO_IV = '" + dr["NO_IV"].ToString() + "' ";
        //        dr_t = dtL.Select(Filter);
        //        if (dr_t.Length > 0)
        //        {
        //            for (int i = 0; i < dr_t.Length; i++)
        //            {
        //                str = "isnull(FG_TPPURCHASE,'')  = '" + dr_t[i]["FG_TPPURCHASE"].ToString() + "'";
        //                dr_t0 = dtL.Select(str);
        //                if (dr_t.Length < 1)
        //                {
        //                    dt.ImportRow(dr_t0[0]);
        //                }
        //            }

        //            if (dt.Rows.Count > 0)
        //            {
        //                foreach (DataRow dr0 in dt.Rows)
        //                {
        //                    dr0["AM_IV"] = dtL.Compute("SUM(AM_IV)", "FG_TPPURCHASE = '" + dr0["FG_TPPURCHASE"] + "'").ToString();
        //                }

        //                str = dt.Compute("MAX(AM_IV)", "").ToString();
        //                dr_t = dt.Select("AM_IV = " + Convert.ToDecimal(str).ToString());

        //                str_tpso = dr_t[0]["FG_TPPURCHASE"].ToString();
        //                str_type_docu = _biz.전표유형(str_tpso);
        //            }
        //            dr["CD_DOCU"] = str_type_docu;
        //        }

        //    }
        //    return;
        //}
        //#endregion

        //#region -> 전표유형 구하기 (금액이 가장 큰 전표유형 구하기)
        //string Get_Type_Dcou(DataTable p_dt)
        //{
        //    String str_type_docu = "45", str_tppurchase = string.Empty;
        //    DataTable dt = p_dt.Clone(), dt_t = p_dt.Clone();
        //    DataRow[] dr_t;
        //    String str = string.Empty;
            
        //    foreach (DataRow dr in p_dt.Rows)
        //    {
        //        str = "isnull(FG_TPPURCHASE,'')  = '" + dr["FG_TPPURCHASE"] + "'";
        //        dr_t = dt.Select(str);
                 
        //        if (dr_t.Length < 1)
        //        {
        //            dt.ImportRow(dr);
        //        } 
        //    }

        //    if (dt.Rows.Count > 0)
        //    {
        //        foreach(DataRow dr in dt.Rows)
        //        {
        //            dr["AM_IV"] = p_dt.Compute("SUM(AM_EX)", "FG_TPPURCHASE = '" + dr["FG_TPPURCHASE"] + "'").ToString();
        //        }

        //        str = dt.Compute("MAX(AM_IV)", "").ToString();
        //        dr_t = dt.Select("AM_IV = " + Convert.ToDecimal(str).ToString());

        //        str_tppurchase = dr_t[0]["FG_TPPURCHASE"].ToString();
        //        str_type_docu = _biz.전표유형(str_tppurchase);
        //    }


        //    return str_type_docu;
        //}
        #endregion

        #region -> PageControlEnalbed

        private void PageControlEnalbed(bool pb_enabled)
        {
            try
            {
                bp_CD_BIZAREA.Enabled = pb_enabled;
//                cbo_CD_BIZAREA_TAX.Enabled = pb_enabled;
                cbo_FG_TRANS.Enabled = pb_enabled;
                //cbo_FG_TPPURCHAS.Enabled = pb_enabled;
                rbtn_M_IV.Enabled = pb_enabled;
                rbtn_T_IV.Enabled = pb_enabled;
            }
            catch
            {
            }
        }

        #endregion

        #region -> btn_CHG_BIZAREA_Click
        private void btn_CHG_BIZAREA_Click(object sender, EventArgs e)
        {
            try
            {
                if (_flex.DataTable.Rows.Count < 1) return;

                DataRow[] rows = _flex.DataTable.Select("CHK ='Y'");
                if (rows == null || rows.Length < 1)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                for (int i = 0; i < rows.Length; i++ )
                {
                    rows[i]["CD_BIZAREA_TAX"] = bp_CD_BIZAREA_TAX.CodeValue;
                    rows[i]["NM_BIZAREA_TAX"] = bp_CD_BIZAREA_TAX.CodeName;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }

        }
        #endregion

        #region -> 하단 그리드 체크박스의 헤더를 마우스 오른쪽 클릭했을 때 발생하는 이벤트(OnCheckBoxHeaderClick)

        #region -> _flex_CheckHeaderClick
        private void _flex_CheckHeaderClick(object sender, EventArgs e)
        {
            try
            {
                FlexGrid flex = sender as FlexGrid;

                switch (flex.Name)
                {
                    case "_flex":  //상단 그리드 Header Click 이벤트

                        //데이타 테이블의 체크값을 변경해주어도 화면에 보이는 값을 변경시키지 못하므로 화면의 값도 같이 변경시켜줌
                        _flex.Row = 1; // 맨처음row에 자동위치하도록 해야 틀어지지 않는다.

                        if (!_flex.HasNormalRow || !_flexL.HasNormalRow) return;

                        for (int h = 0; h < _flex.Rows.Count - 1; h++)
                        {
                            _flex.Row = h + 1; //Row가 순차적으로 변경되면서 RowChange() 이벤트를 자동 실행하게 유도한다.

                            for (int i = _flexL.Rows.Fixed; i < _flexL.Rows.Count; i++)
                            {
                                if (_flexL.RowState(i) == DataRowState.Deleted) continue;

                                _flexL[i, "S"] = _flex["CHK"].ToString();
                            }
                        }
                        break;

                    case "_flexL":  //하단 그리드 Header Click 이벤트

                        if (!_flexL.HasNormalRow) return;

                        _flex["CHK"] = D.GetString(_flexL["S"]);

                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion


        #endregion

        #region -> btn_매입형태_변경_Click
        private void btn_매입형태_변경_Click(object sender, EventArgs e)
        {
            try
            {
                string TPPURCHASChange = D.GetString(cbo_FG_TPPURCHAS.SelectedValue); // == null ? string.Empty : cbo_FG_TPPURCHAS.SelectedValue.ToString();
                if (TPPURCHASChange == string.Empty)                
                {
                    this.ShowMessage("변경할 매입형태를 입력하십시오");
                    cbo_FG_TPPURCHAS.Focus();
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
                        dr_L["FG_TPPURCHASE"] = cbo_FG_TPPURCHAS.SelectedValue;

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
                                dlr["FG_TPPURCHASE"] = cbo_FG_TPPURCHAS.SelectedValue.ToString(); //매입형태 변경
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
        #endregion

        #region -> btn_거래구분_변경_Click
        private void btn_거래구분_변경_Click(object sender, EventArgs e)
        {
            try
            { //거래구분은 헤더에 클릭되어 있는것을 기준으로 헤더와 동일하게 라인이 모두 변하도록되어야한다.
                if (_flex.DataTable.Rows.Count < 1) return;

                DataRow[] rows = _flex.DataTable.Select("CHK ='Y'");
                if (rows == null || rows.Length < 1 || D.GetString(cbo_FG_TRANS_변경.SelectedValue) == "")
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                Object[] obj = new Object[3];
                obj[0] = LoginInfo.CompanyCode;
                obj[1] = "MA_B000046";
                obj[2] = "23";

                DataTable dt = Duzon.ERPU.MF.ComFunc.GetTableSearch("MA_CODEDTL", obj);

                for (int i = 0; i < rows.Length; i++)
                {
                    rows[i]["FG_TRANS"] = cbo_FG_TRANS_변경.SelectedValue;
                    rows[i]["FG_TAX"] = "23";


                    rows[i]["NM_TAX"] = dt.Rows[0]["NM_SYSDEF"];
                    rows[i]["VAT_TAX"] = 0;
                    rows[i]["AM_TOTAL"] = rows[i]["AM_K"];

                    foreach (DataRow dr in _flexL.DataTable.Select("NO_TEMP = '" + D.GetString(rows[i]["NO_TEMP"]) + "'"))
                    {
                        dr["FG_TRANS"] = cbo_FG_TRANS_변경.SelectedValue;
                        dr["FG_TAX"] = "23";
                        dr["NM_TAX"] = dt.Rows[0]["NM_SYSDEF"];
                        dr["VAT_IV"] = 0;
                        
                    }

                    foreach (DataRow drl in _dt_Line.Select("NO_TEMP = '" + D.GetString(rows[i]["NO_TEMP"]) + "'"))
                    {
                        drl["FG_TRANS"] = cbo_FG_TRANS_변경.SelectedValue;
                        drl["FG_TAX"] = "23";
                        drl["NM_TAX"] = dt.Rows[0]["NM_SYSDEF"];
                        drl["VAT_IV"] = 0;

                    }


                }               

                _flex_AfterRowChange(null, null);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region -> btnCdPC_Click
        void btnCdPC_Click(object sender, EventArgs e)
        {
            try
            {
                if (_flex.DataTable.Rows.Count < 1) return;

                DataRow[] rows = _flex.DataTable.Select("CHK ='Y'");
                if (rows == null || rows.Length < 1)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                for (int i = 0; i < rows.Length; i++)
                {
                    rows[i]["CD_PC_USER"] = ctxCdPc.CodeValue;
                    rows[i]["NM_PC_USER"] = ctxCdPc.CodeName;
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


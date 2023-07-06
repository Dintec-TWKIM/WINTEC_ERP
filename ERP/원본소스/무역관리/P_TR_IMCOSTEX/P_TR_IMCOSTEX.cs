using System;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;
using Duzon.Common.Forms;
using Duzon.Common.BpControls;

using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using DzHelpFormLib;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.Common.Util;

namespace trade
{

    // **************************************
    // 작   성   자 : 최승애
    // 재 작  성 일 : 2011-08-12
    // 모   듈   명 : 무역
    // 시 스  템 명 : 무역관리
    // 서브시스템명 : 수입
    // 페 이 지  명 : 기타 부대비용 등록
    //////////////////////////////////////////
    // 수 정  내 역 : 2011-08-12, 헤더에 통관번호 추가함.
    // **************************************

    public partial class P_TR_IMCOSTEX : Duzon.Common.Forms.PageBase
    {
        #region ♣변수선언

        private P_TR_IMCOSTEX_BIZ _biz = new P_TR_IMCOSTEX_BIZ();
        private string CompanyCode = Global.MainFrame.LoginInfo.CompanyCode.ToString();
        private FreeBinding _header = null;
        private DataSet g_dsCombo = null;
        DataView dv = new DataView();

        private int _정산여부_cnt;
        private int _전표처리여부;
        
        //bool 부대번호존재유무 = true;
        private DataTable dt_cost = null; //부대비용데이터테이블
        // 지급관리 통제
        private string _지급관리통제설정 = "N";
        
        // _재고평가단위 (000 : 품목별, 100 : 프로젝트별, 200 : UNIT별)
        private string _재고평가단위 = "000";

        // B/L번호의 프로젝트정보
        private DataTable _dt_BL_PJT;

        //지급예정일
        private string strDtPre = "";

        #endregion

        #region ♣생성자 / 소멸자 / 디자이너

        #region -> 생성자

        public P_TR_IMCOSTEX()
        {
            InitializeComponent();

            MainGrids = new FlexGrid[] { _flex };
            DataChanged += new EventHandler(Page_DataChanged);

            _header = new FreeBinding();
            _header.JobModeChanged += new FreeBindingEventHandler(_header_JobModeChanged);
            _header.ControlValueChanged += new FreeBindingEventHandler(_header_ControlValueChanged);
        }

        #endregion

        #region -> InitLoad

        protected override void InitLoad()
        {
            base.InitLoad();
            MA_EXC_SET();
            InitGrid();
        }

        #endregion

        #region -> 시스템통제설정
        private void MA_EXC_SET()
        {

            /*******************************************************
             * 2010.08.20 _지급관리통제설정
             * *****************************************************/
            _지급관리통제설정 = BASIC.GetMAEXC("거래처정보등록-회계지급관리사용여부");

            /*******************************************************
             * 2010.07.19 부가세처리형태 (일괄/개별)
             * *****************************************************/
            if (Settings1.Default.TY_TAX == "001" || Settings1.Default.TY_TAX == string.Empty)
                rbtn_T_IV.Checked = true;
            else
                rbtn_M_IV.Checked = true;

            /*******************************************************
             * 2010.10.29 통관대행업체
             * *****************************************************/
            chk_통관대행업체사용.Checked = Settings1.Default.chk_pop_ptn;
            chk_통관대행업체사용.Visible = true;

            /*******************************************************
             * 2014.11.14 재고평가단위 (000 : 품목별, 100 : 프로젝트별, 200 : UNIT별)
             * *****************************************************/
            _재고평가단위 = BASIC.GetMAEXC("재고평가단위");
        }
        #endregion

        #region -> InitGrid

        private void InitGrid()
        {
            _flex.BeginSetting(1, 1, false);
            _flex.SetCol("CD_COST",    "부대비용명", 80, true);


            // 2011-06-07, 최승애, QUANTITY ==> MONEY로 변경함.
            //_flex.SetCol("AM_COST",    "발생비용", 120, true, typeof(decimal), FormatTpType.QUANTITY);
            _flex.SetCol("AM_COST", "발생비용", 120, true, typeof(decimal), FormatTpType.MONEY);    // 2011-06-07, 최승애, QUANTITY ==> MONEY로 변경함.
            _flex.SetCol("AM_TAXSTD", "과세표준액", 100, true, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("FG_TAX",     "과세구분", 80, true);
            _flex.SetCol("RT_VAT",     "부가세율", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY );

            //2011-12-08, 최승애, PIMS : D20111208005, 부가세액을 수정할 수 있게 해달라고 요청이 들어와서 에디트 가능하도록 수정함. 
            _flex.SetCol("VAT",        "부가세액", 120, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flex.SetCol("CD_PARTNER", "지급거래처코드",100, true);
            _flex.SetCol("LN_PARTNER", "지급거래처명", 100,false);
            _flex.SetCol("NM_POP", "대행자", 100, true);
            _flex.SetCol("DT_PAY",     "지급예정일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex.SetCol("COND_PAY",   "지급조건", 100, true);
            _flex.SetCol("DT_DUE", "만기일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex.SetCol("CD_PJT", "프로젝트", 120, true);
            _flex.SetCol("NM_PJT", "프로젝트명", 120, false);
            
            if (Config.MA_ENV.PJT형여부 == "Y")
            {
                _flex.SetCol("SEQ_PROJECT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 항번" : "프로젝트항번", 120, false, typeof(decimal));
                _flex.SetCol("CD_UNIT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 코드" : "프로젝트 품목코드", 140, false, typeof(string));
                _flex.SetCol("NM_UNIT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 명" : "프로젝트 품목명", 140, false, typeof(string));
                _flex.SetCol("STND_UNIT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 규격" : "프로젝트 품목규격", 140, false, typeof(string));
            }
            _flex.SetCol("DC_RMK", "비고", 120, true);

            _flex.SettingVersion = "1.0.0.4";
            _flex.SetCodeHelpCol("NM_POP", Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, new string[] { "CD_POP", "NM_POP" }, new string[] { "CD_PARTNER", "LN_PARTNER" });
            _flex.SetCodeHelpCol("CD_PARTNER", Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, new string[] { "CD_PARTNER", "LN_PARTNER" }, new string[] { "CD_PARTNER", "LN_PARTNER" });
            _flex.SetCodeHelpCol("CD_PJT", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new string[] { "CD_PJT", "NM_PJT" }, new string[] { "NO_PROJECT", "NM_PROJECT" });

            if (Config.MA_ENV.YN_UNIT == "Y")
            {
                _flex.SetCodeHelpCol("CD_PJT", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new String[] { "CD_PJT", "NM_PJT", "SEQ_PROJECT", "CD_UNIT", "NM_UNIT", "STND_UNIT" }, new String[] { "NO_PROJECT", "NM_PROJECT", "SEQ_PROJECT", "CD_PJT_ITEM", "NM_PJT_ITEM", "PJT_ITEM_STND" }, new String[] { "CD_PJT", "NM_PJT", "SEQ_PROJECT", "CD_UNIT", "NM_UNIT", "STND_UNIT" }, ResultMode.FastMode);
                _flex.SetCodeHelpCol("CD_UNIT", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new String[] { "CD_PJT", "NM_PJT", "SEQ_PROJECT", "CD_UNIT", "NM_UNIT", "STND_UNIT" }, new String[] { "NO_PROJECT", "NM_PROJECT", "SEQ_PROJECT", "CD_PJT_ITEM", "NM_PJT_ITEM", "PJT_ITEM_STND" }, new String[] { "CD_PJT", "NM_PJT", "SEQ_PROJECT", "CD_UNIT", "NM_UNIT", "STND_UNIT" }, ResultMode.FastMode);
            }
            else
            {
                _flex.SetCodeHelpCol("CD_PJT", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new String[] { "CD_PJT", "NM_PJT" }, new String[] { "NO_PROJECT", "NM_PROJECT" });
            }

            _flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);


            _flex.VerifyAutoDelete = new string[] { "CD_COST" };
           

            if (Config.MA_ENV.PJT형여부 == "Y" && BASIC.GetMAEXC("수입프로젝트형사용여부") == "100")
                _flex.VerifyNotNull = new string[] { "CD_COST", "CD_PJT" };
            else
                _flex.VerifyNotNull = new string[] { "CD_COST" };


            //_flex.Cols["NM_POP"].Visible = chk_통관대행업체사용.Checked;
           
            _flex.StartEdit += new RowColEventHandler(_flex_StartEdit);
            _flex.ValidateEdit += new ValidateEditEventHandler(_flex_ValidateEdit);
            //_flex.DoubleClick += new EventHandler(_flex_DoubleClick);
            _flex.BeforeCodeHelp += new BeforeCodeHelpEventHandler(_flex_BeforeCodeHelp);
            _flex.AfterCodeHelp += new AfterCodeHelpEventHandler(_flex_AfterCodeHelp);
            _flex.EnterKeyAddRow = true;
            _flex.AddRow += new System.EventHandler(btn추가_Click);
            
        }

        void _flex_StartEdit(object sender, RowColEventArgs e)
        {
            if ( _정산여부_cnt > 0 )
            {
                ShowMessage( "이미 '미착정산처리'된 부대비용입니다" );
                e.Cancel = true;
            }

            if ( _전표처리여부 > 0  )
            {
                ShowMessage( "이미 '전표처리'된 부대비용입니다" );
                e.Cancel = true;
            }
        }

        void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            if (e.Parameter.HelpID == HelpID.P_MA_PARTNER_SUB)  //지급거래처
            {
                if (e.Parameter.HelpID == HelpID.P_MA_PARTNER_SUB)
                {
                    ;
                }
            }
        }

        void _flex_AfterCodeHelp(object sender, AfterCodeHelpEventArgs e)
        {
            string colName = _flex.Cols[e.Col].Name;

            if (e.Result.DialogResult == DialogResult.Cancel)
                return;

            switch (colName)
            {
                case "CD_PARTNER": // 수입대행사를 안쓸때
                case "NM_POP":     // 수입대행사를 쓸때


                    if (chk_통관대행업체사용.Checked && colName == "CD_PARTNER") break;
                    string l_cd_partner = D.GetString(e.Result.Rows[0]["CD_PARTNER"]);
                    DataRow dr_partner = BASIC.GetPartner(l_cd_partner);
                    _flex["DT_PAY_PREARRANGED"] = dr_partner["DT_PAY_PREARRANGED"];
                    BASIC.CacheDataClear(BASIC.CacheEnums.거래처정보);

                    break;
            }

        }


        void _flex_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                string oldValue = ((FlexGrid)sender).GetData(e.Row, e.Col).ToString();
                string newValue = ((FlexGrid)sender).EditData;

                if (oldValue.ToUpper() == newValue.ToUpper())
                    return;

                CommonFunction objFunc = new CommonFunction();
                switch (_flex.Cols[e.Col].Name)
                {

                    case "AM_COST":
                        if ( g_dsCombo.Tables [1].Rows.Count > 0 )
                        {
                            for ( int i = 0 ; i < g_dsCombo.Tables [1].Rows.Count ; i++ )
                            {
                                if ( g_dsCombo.Tables [1].Rows [i] ["CODE"].ToString().Trim() == _flex ["FG_TAX"].ToString().Trim() )
                                {
                                    _flex ["RT_VAT"] = g_dsCombo.Tables [1].Rows [i] ["CD_FLAG1"].ToString().Trim();
                                    break;
                                }
                            }
                            
                            //decimal ll_AM_COST = Decimal.Truncate( Decimal.Parse( _flex ["AM_COST"].ToString() ) * ( ( Decimal.Parse( _flex ["RT_VAT"].ToString() ) / 100 ) ) );

                            decimal ll_AM_COST = Unit.원화금액(DataDictionaryTypes.TR ,  D.GetDecimal(_flex["AM_COST"].ToString()) * ((D.GetDecimal(_flex["RT_VAT"].ToString()) / 100)));


                            _flex ["VAT"] = ll_AM_COST;

                            _flex["AM_EX"] = Unit.외화금액(DataDictionaryTypes.TR , D.GetDecimal(newValue));
                            //if (_flex.CDecimal(_flex["AM_TAXSTD"]) == 0 )   부대비용이 바뀌면 그냥 과세표준도 부대비용으로 변경
                            _flex["AM_TAXSTD"] = Unit.원화금액(DataDictionaryTypes.TR, D.GetDecimal(newValue));	
                        }
                        break;
                         
                    case "FG_TAX":
                        for (int i = 0; i < g_dsCombo.Tables[1].Rows.Count; i++)
                        {
                            if (g_dsCombo.Tables[1].Rows[i]["CODE"].ToString().Trim() == _flex["FG_TAX"].ToString().Trim())
                            {
                                _flex["RT_VAT"] = g_dsCombo.Tables[1].Rows[i]["CD_FLAG1"].ToString().Trim();
                                break;
                            }
                        }
                        //decimal ll_AM = Decimal.Truncate(Decimal.Parse(_flex["AM_COST"].ToString()) * ((Decimal.Parse(_flex["RT_VAT"].ToString()) / 100)));

                        ////////////////////////////////////////////////////////////////////////////////////////////////////
                        //2011-12-08, 최승애, PIMS : D20111208005
                        //발생비용에 * 부가세율이 아닌 과세표준액에 대해서 * 부가세율로 처리해달라는 요청에 의해서 수정함.
                        ////////////////////////////////////////////////////////////////////////////////////////////////////
                        //decimal ll_AM = Unit.원화금액(DataDictionaryTypes.TR ,   Decimal.Parse(_flex["AM_COST"].ToString()) * ((Decimal.Parse(_flex["RT_VAT"].ToString()) / 100)));

                        decimal ll_AM = Unit.원화금액(DataDictionaryTypes.TR, D.GetDecimal(_flex["AM_TAXSTD"].ToString()) * ((D.GetDecimal(_flex["RT_VAT"].ToString()) / 100)));

                        _flex["VAT"] = ll_AM;
                        break;

                    case "COND_PAY":
                        string dt_due = string.Empty;
                        string dt_pay = string.Empty;

                        if (_지급관리통제설정 == "Y")
                        {
                            decimal ldb_amk = D.GetDecimal(_flex["AM_COST"]);
                            dt_pay = ComFunc.만기예정일자(msk기표일자.Text, ldb_amk, D.GetString(_flex.EditData), "1");
                            // 만기예정일
                            if (dt_pay != string.Empty)
                            {
                                _flex[_flex.Row, "DT_PAY"] = dt_pay;
                                _flex[_flex.Row, "DT_DUE"] = dt_pay;
                            }

                            //어음 만기일(구분이 어음이 아닐경우 Empty가 return됨)
                            dt_due = ComFunc.만기예정일자(msk기표일자.Text, ldb_amk, D.GetString(_flex.EditData), "2");
                            if (dt_due != string.Empty)
                                _flex[_flex.Row, "DT_DUE"] = dt_due;

                        }

                        if (_지급관리통제설정 == "N" || dt_pay == string.Empty || dt_due == string.Empty)
                        {

                            //PIMS :  한국화스너, 지급예정일이 공백으로 넘어오는 경우 오류나서 D.GETINT 함수로 사용하였음.
                            //string str_temp = objFunc.DateAdd(msk기표일자.Text, "D", Convert.ToInt16(_flex["DT_PAY_PREARRANGED"].ToString()));                            
                            string str_temp = objFunc.DateAdd(msk기표일자.Text, "D", D.GetInt(_flex["DT_PAY_PREARRANGED"].ToString()));

                            if (dt_pay == string.Empty)
                                _flex["DT_PAY"] = str_temp;

                            if (dt_due == string.Empty)
                                _flex["DT_DUE"] = _flex["DT_PAY"];
                        }
                        break;
                    case "AM_TAXSTD":   // 과세표준
                        if (g_dsCombo.Tables[1].Rows.Count > 0)
                        {
                            for (int i = 0; i < g_dsCombo.Tables[1].Rows.Count; i++)
                            {
                                if (g_dsCombo.Tables[1].Rows[i]["CODE"].ToString().Trim() == _flex["FG_TAX"].ToString().Trim())
                                {
                                    _flex["RT_VAT"] = g_dsCombo.Tables[1].Rows[i]["CD_FLAG1"].ToString().Trim();
                                    break;
                                }
                            }

                            decimal ll_AM_COST = Unit.원화금액(DataDictionaryTypes.TR, D.GetDecimal(_flex["AM_TAXSTD"]) * ((D.GetDecimal(_flex["RT_VAT"].ToString()) / 100)));

                            _flex["VAT"] = ll_AM_COST;

                        }
                        break;

                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex_DoubleClick(object sender, EventArgs e)
        {
            P_TR_IMCOST_TAXSTD_SUB dlg = null;
            try
            {
                if (!_flex.HasNormalRow) return;

                //FlexGrid flex = (FlexGrid.Ce)sender as FlexGrid;

                Decimal ld_vat, ld_am_taxstd, ld_rt_vat;

                if (_flex.Cols[_flex.Col].Name == "VAT")
                {
                    ld_vat = _flex.CDecimal(_flex[_flex.Row, "VAT"]);
                    ld_am_taxstd = _flex.CDecimal(_flex[_flex.Row, "AM_TAXSTD"]);
                    ld_rt_vat = _flex.CDecimal(_flex[_flex.Row, "RT_VAT"]); ;
                    if (ld_am_taxstd == 0)
                    {
                        _flex[_flex.Row, "AM_TAXSTD"] = _flex.CDecimal(_flex[_flex.Row, "AM_COST"]);
                        //_flex[_flex.Row, "AM_COST"] = _flex.CDecimal(_flex[_flex.Row, "AM_TAXSTD"]);
                        ld_am_taxstd = _flex.CDecimal(_flex[_flex.Row, "AM_COST"]);
                    }

                    dlg = new P_TR_IMCOST_TAXSTD_SUB(ld_vat, ld_am_taxstd, ld_rt_vat);

                    DialogResult result = dlg.ShowDialog();
                    if (result == DialogResult.OK)
                    { 
                        _flex[_flex.Row, "VAT"] = dlg._gdt_return.Rows[0]["VAT"];
                        _flex[_flex.Row, "AM_TAXSTD"] = dlg._gdt_return.Rows[0]["AM_TAXSTD"];
                    }

                    _flex.Focus();
                }
                return;

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> InitControl()

        private void InitControl()
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            g_dsCombo = GetComboData("N;TR_IM00007", "S;MA_CODEDTL2;MA_B000046", "NC;MA_BIZAREA", "N;MA_AISPOSTH;600", "N;PU_C000044", "S;MA_B000095"); // 콤보초기화

            cbo기표사업장.DataSource = g_dsCombo.Tables[2];                         //사업장
            cbo기표사업장.DisplayMember = "NAME";
            cbo기표사업장.ValueMember = "CODE";

            dt2.Columns.Add("CODE", typeof(string));
            dt2.Columns.Add("NAME", typeof(string));

            DataRow[] drs =  g_dsCombo.Tables[0].Select("code <> '000'");
            foreach (DataRow dr in drs)
                dt2.ImportRow(dr);

            /*******************************************************
             * 2009.06.08 비용형태 추가
             * *****************************************************/

            //비용형태
            m_cbo비용형태.DataSource = g_dsCombo.Tables[3];
            m_cbo비용형태.ValueMember = "CODE";
            m_cbo비용형태.DisplayMember = "NAME";

            _flex.SetDataMap("CD_COST", dt2, "CODE", "NAME");                        //부대비용
            dt_cost = dt2.Copy();

            _flex.SetDataMap("FG_TAX", g_dsCombo.Tables[1], "CODE", "NAME");        //과세구분

            DataSet dsTemp = _biz.Search("");                                       // 프리폼 초기화
            this.btn전표발행.Enabled = false;
            this.btn전표처리취소.Enabled = false;

            _header.SetBinding(dsTemp.Tables[0], oneGrid1);
            _header.ClearAndNewRow();
            _flex.Binding = dsTemp.Tables[1];


            /*******************************************************
             * 2010.03.24 지급조건추가
             * *****************************************************/
            //_flex.SetDataMap("COND_PAY", g_dsCombo.Tables[4], "CODE", "NAME");        //지급조건추가


            if (_지급관리통제설정 == "N")                
                _flex.SetDataMap("COND_PAY", g_dsCombo.Tables[4], "CODE", "NAME");        //지급조건추가
            else
            {
                DataTable dt_pay = ComFunc.GetPayList();
                if (dt_pay != null)
                    _flex.SetDataMap("COND_PAY", dt_pay, "CODE", "NAME");
            }

            // 비용형태
            //if (m_cbo비용형태.Text != string.Empty)
            //    _header.CurrentRow["TY_COST"] = m_cbo비용형태.SelectedValue;


            /*******************************************************
             * 2010.07.19 전표처리여부
             * *****************************************************/

            dt1.Columns.Add("CODE", typeof(string));
            dt1.Columns.Add("NAME", typeof(string));

            DataRow row = null;
            row = dt1.NewRow(); row["CODE"] = "N"; row["NAME"] = DD("미처리"); dt1.Rows.Add(row);
            row = dt1.NewRow(); row["CODE"] = "Y"; row["NAME"] = DD("처리"); dt1.Rows.Add(row);

            this.cbo처리여부.DataSource = dt1;
            this.cbo처리여부.DisplayMember = "NAME";
            this.cbo처리여부.ValueMember = "CODE";

            cbo_YN_Jeonja.DataSource = g_dsCombo.Tables[5];
            cbo_YN_Jeonja.ValueMember = "CODE";
            cbo_YN_Jeonja.DisplayMember = "NAME";

            bpc담당자.QueryAfter += new BpQueryHandler(OnBpControl_QueryAfter);



        }

        #endregion

        #region -> InitPaint

        protected override void InitPaint()
        {
            base.InitPaint();

            InitControl();
            
            oneGrid1.UseCustomLayout = true;
            bpPanelControl1.IsNecessaryCondition = true;
            bpPanelControl2.IsNecessaryCondition = true;
            bpPanelControl3.IsNecessaryCondition = true;
            bpPanelControl4.IsNecessaryCondition = true;
            bpPanelControl5.IsNecessaryCondition = true;
            bpPanelControl6.IsNecessaryCondition = true;
            bpPanelControl7.IsNecessaryCondition = true;
            bpPanelControl8.IsNecessaryCondition = true;
            bpPanelControl9.IsNecessaryCondition = true;
            bpPanelControl10.IsNecessaryCondition = true;
            bpPanelControl11.IsNecessaryCondition = true;
            bpPanelControl12.IsNecessaryCondition = true;
            bpPanelControl13.IsNecessaryCondition = true;
            bpPanelControl14.IsNecessaryCondition = true;
            bpPanelControl15.IsNecessaryCondition = true;

            oneGrid1.IsSearchControl = false;

            oneGrid1.InitCustomLayout();


            OnToolBarAddButtonClicked(null, null);

           

        }

        #endregion

        //실제저장구문
        #region -> SaveData()

        protected override bool SaveData()
        {
            if (!BeforeSave()) return false;

            if (!base.SaveData())   return false;
             

            DataTable dtH = _header.GetChanges();
            String s_no_cost = txt부대비용번호.Text;
            /*백승은추가 2011.07.26 -> 김형석 과장님이 차후 다시 수정 보류*/
            //decimal ld_am_distribu  = 0;
            //ld_am_distribu = _biz.Get_정산금액(txtBL번호.Text);

            //if (ld_am_distribu != 0)
            //{
            //    DialogResult result;
            //    result = MessageBox.Show("미착정산이 모두 끝난 B/L입니다. " + Environment.NewLine +
            //        "계속 진행하시겠습니까?", "미착정산처리여부", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            //    if (result == DialogResult.Cancel)
            //        return false;
            //}


            if (dtH != null && dtH.Rows.Count > 0)
            {
                if (추가모드여부)
                {
                    s_no_cost = (string)GetSeq(LoginInfo.CompanyCode, "TR", "02", msk기표일자.Text.Substring(0, 6));
                    txt부대비용번호.Text = s_no_cost;
                    _header.CurrentRow["NO_COST"] = s_no_cost;
                    dtH.Rows[0]["NO_COST"] = s_no_cost;
                }

            }

            DataTable dtL = _flex.GetChanges();

            if (dtH == null && dtL == null) return true;

            if (dtL != null && dtL.Rows.Count > 0)
            {
                foreach (DataRow dr in dtL.Rows)
                {
                    if (dr.RowState == DataRowState.Added)
                        dr["NO_COST"] = s_no_cost;
                }
            }

            bool bSuccess = _biz.Save(dtH, dtL);
            if (!bSuccess)
            {
                return false;
            }
            //else
            //{
                //부대번호존재유무 = true;

                //string 통관번호 = _header.CurrentRow["NO_TO"].ToString();
                //_biz.Imd_Insert(통관번호);
            //}

            _header.AcceptChanges();
            _flex.AcceptChanges();
         //   this.btn삭제.Enabled = false;

            return true;
        }

        #endregion

        #region -> Page_DataChanged

        void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                ToolBarSaveButtonEnabled = IsChanged();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region ♣컨트롤이벤트

        #region -> btn전표발행_Click

        private void btn전표발행_Click(object sender, EventArgs e)
        {
            try
            {
                // 저장된것 확인하기 위한...
                if (IsChanged())
                //if (!base.BeforeAdd())
                    if (!MsgAndSave(PageActionMode.Search)) return;

                if (m_cbo비용형태.SelectedValue == DBNull.Value || m_cbo비용형태.SelectedValue == null ||  m_cbo비용형태.SelectedValue.ToString() == "")
                {
                    //this.ShowMessageKor("비용형태를 선택하시기 바랍니다.");
                    this.ShowMessage("비용형태를 선택하시기 바랍니다.");
                    return;
                }

                string 부대비용번호 = _header.CurrentRow ["NO_COST"].ToString();
                string 부가세처리형태 = (rbtn_T_IV.Checked) ? "1" : "0";
                bool 전표성공여부 = _biz.미결전표처리(부대비용번호, 부가세처리형태);

                if ( 전표성공여부 == false )
                {
                    //this.ShowMessage( "부대비용번호 = " + 부대비용번호 + " 전표처리가 정상적으로 처리되지 않았습니다.", "IK1" );		// 전표처리중 오류가 발생하였습니다.			
                    this.ShowMessage("부대비용번호 =  @  전표처리가 정상적으로 처리되지 않았습니다.", 부대비용번호, "IK1");
                    return;
                }

                Global.MainFrame.ShowMessage( "전표처리가 완료되었습니다" );
                RealSearch();
            }
            catch(Exception Ex)
            {
                this.MsgEnd(Ex.Message);
            }
        }

        #endregion

        #region -> btn전표처리취소_Click

        private void btn전표처리취소_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global.MainFrame.ServerKeyCommon == "FEELUX") //필룩스버튼권한없애달라는요청최면환팀장
                {
                    this.ShowMessage("전표처리취소버튼은사용하실수없습니다. 회계에서삭제가능합니다.");
                    return;
                }


                string 부대비용번호 = _header.CurrentRow ["NO_COST"].ToString();
                bool 전표취소성공여부 = _biz.미결전표취소( 부대비용번호 );

                if ( 전표취소성공여부 == false )
                {
                    //this.ShowMessage( "부대비용번호 = " + 부대비용번호 + " 전표처리가 정상적으로 취소되지 않았습니다.", "IK1" );		// 전표처리중 오류가 발생하였습니다.			
                    this.ShowMessage("부대비용번호 = @ 전표처리가 정상적으로 취소되지 않았습니다.", 부대비용번호, "IK1");
                    return;
                }

                Global.MainFrame.ShowMessage( "취소작업을 완료하였습니다" );
                RealSearch();
                btn삭제.Enabled = true;
            }
            catch ( Exception ex )
            {
                MsgEnd( ex );
            }
        }

        #endregion

        #region -> btn추가_Click

        private void btn추가_Click(object sender, EventArgs e)
        {
            try
            {

                if (!btn추가.Enabled)
                    return;                

                if (!HeaderCheck()) return; //필수항목 검사   

                _flex.Rows.Add();
                if (_flex.Rows.Count - 1 > _flex.Rows.Fixed)
                {
                    _flex.Row = _flex.Rows.Count - _flex.Rows.Fixed;
                    if ( _flex ["FG_TAX"].ToString().Trim() == "21" && _flex["CD_PARTNER"].ToString().Trim() == "" )
                    {
                        ShowMessage( "지급거래처를 입력하세요" );
                        _flex.Rows.Remove(_flex.Rows.Count - 1);
                        return;
                    }
                }

                _flex.Row = _flex.Rows.Count - 1;
                if (txt부대비용번호.Text != string.Empty)
                    _flex["NO_COST"] = txt부대비용번호.Text;				//부대비용번호

                _flex ["CD_COST"] = "";
                _flex["NO_LINE"] = GetMaxLine(_flex.DataTable);			//순번
                _flex["AM_COST"] = 0;						//발생비용
                _flex["RT_VAT"] = 0;						//부가세율
                _flex["CD_EXCH"] = "000";				    //환종
                _flex["AM_EX"] = 0;							//금액
                _flex["VAT"] = 0;							//부가세액
                _flex["AM_DISTRIBUT"] = 0;					//정산금액

                if (Global.MainFrame.ServerKeyCommon == "SNSTECH")
                    _flex["DT_PAY"] = msk기표일자.Text;
                else
                    _flex["DT_PAY"] = MainFrameInterface.GetStringToday;

                if(_지급관리통제설정 == "N")   _flex["COND_PAY"] = "001";

                CommonFunction objFunc = new CommonFunction();
                string str_temp = objFunc.DateAdd(D.GetString(_flex["DT_PAY"]), "D", Convert.ToInt16(strDtPre));
                _flex["DT_PAY"] = str_temp;
                
                //
                // 재고평가단위가 unit의 경우,
                // 해당 bl의 프로젝트코드와 유닛모두 top1으로 자동입력
                //
                if (Config.MA_ENV.PJT형여부 == "Y" && _재고평가단위 == "200")
                {
                    if (_dt_BL_PJT.Rows.Count > 0)
                    {
                        _flex["CD_PJT"] = D.GetString(_dt_BL_PJT.Rows[0]["CD_PJT"]);
                        _flex["NM_PJT"] = D.GetString(_dt_BL_PJT.Rows[0]["NM_PJT"]);
                        _flex["SEQ_PROJECT"] = D.GetDecimal(_dt_BL_PJT.Rows[0]["SEQ_PROJECT"]);
                        _flex["CD_UNIT"] = D.GetString(_dt_BL_PJT.Rows[0]["CD_UNIT"]);
                        _flex["NM_UNIT"] = D.GetString(_dt_BL_PJT.Rows[0]["NM_UNIT"]);
                        _flex["STND_UNIT"] = D.GetString(_dt_BL_PJT.Rows[0]["STND_UNIT"]);
                    }
                }

                _flex.AddFinished();
                _flex.Select(_flex.Rows.Count - 1, _flex.LeftCol);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> btn삭제_Click

        private void btn삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flex.HasNormalRow)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
    
                _flex.Rows.Remove(_flex.Row);
                ToolBarSaveButtonEnabled = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> btnBL번호_Click

        private void btnBL번호_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!Check()) return;

                //P_TR_BL_NO dlg = new P_TR_BL_NO("002");

                P_TR_BL_NO dlg = new P_TR_BL_NO("002","P_TR_IMCOSTEX","","");  // 2011-08-22, 최승애, B/L건이 통관완료된 것들은 뜨지 않도록 하기 위해서 수정함.

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this.txtBL번호.Text = dlg.BL번호;
                    _header.CurrentRow["NO_BL"] = dlg.BL번호;

                    this.txtLC번호.Text = dlg.LC참조번호;
                    _header.CurrentRow["NO_LC"] = dlg.LC참조번호;

                    _header.CurrentRow["CD_PARTNER"] = dlg.거래처코드;

                    this.txt거래처.Text = dlg.거래처명;
                    _header.CurrentRow["LN_PARTNER"] = dlg.거래처명;

                    cbo_YN_Jeonja.SelectedValue = dlg.YN_JEONJA;
                    _header.CurrentRow["YN_JEONJA"] = dlg.YN_JEONJA;

                    DataRow dr = _biz.Get_BL_CC(dlg.BL번호);

                    if (dr != null)
                    {
                        bpNm_CC.SetCode(D.GetString(dr["CD_CC"]), D.GetString(dr["NM_CC"]));
                        _header.CurrentRow["CD_CC"] = D.GetString(dr["CD_CC"]);
                        _header.CurrentRow["NM_CC"] = D.GetString(dr["NM_CC"]);
                    }

                    if (Config.MA_ENV.PJT형여부 == "Y" && _재고평가단위 == "200")
                        _dt_BL_PJT = _biz.Get_BL_PJT(txtBL번호.Text);

                    DataTable dt = _biz.Get_DT_PAY_PREARRANGED(dlg.거래처코드);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        strDtPre = D.GetString(dt.Rows[0]["DT_PAY_PREARRANGED"])== "" ? "0" : D.GetString(dt.Rows[0]["DT_PAY_PREARRANGED"]);
                    }
                    else
                        strDtPre = "0";
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 엑셀업로드_Check
        //CD_PLANT	CD_PARTNER	CD_TPPO	FG_UM	CD_PJT	CD_ITEM	QT_PO	CD_EXCH	DC50_PO
        private bool Chk_ExcelData(DataTable dt_Excel)
        {
            string[] str_col = new string[6] { "CD_COST", "AM_COST", "FG_TAX", "CD_PARTNER", "DT_PAY", "COND_PAY" };


            for (int i = 0; i < str_col.Length; i++)
            {
                if (!dt_Excel.Columns.Contains(str_col[i]))
                {
                    //ShowMessage("컬럼명 [" + str_col[i] + "] 이 엑셀에 존재하지 않습니다.");
                    ShowMessage("컬럼명 [@] 이 엑셀에 존재하지 않습니다.", str_col[i]);
                    return false;
                }
            }

            DataRow [] drs = dt_Excel.Select("CD_COST = '000'");
            if (drs.Length > 0)
            {
                ShowMessage("부대비용 코드 B/L결제(000)는 기타부대비용에서 등록할 수 없습니다.");
                return false;
            }

            return true;
        }

        private bool Check_dt_Excel(DataTable dt_Excel)
        {
            DataTable dt_Grid = _flex.DataTable;
            DataRow NewRow;

            DataSet ds_Master = _biz.Search_Chk_Excel();

            string s_잘못된부대비용코드 = string.Empty;
            string s_잘못된과세구분 = string.Empty;
            string s_잘못된지급거래처코드 = string.Empty;
            string s_잘못된지급조건 = string.Empty;
            string s_잘못된날짜 = string.Empty;
            bool b_엑셀자료적합여부 = true;

            DataTable dt_temp = _flex.DataTable.Clone();

            foreach (DataRow row in dt_Excel.Rows)
            {
                DataRow[] drs1 = ds_Master.Tables[0].Select("CD_SYSDEF = '" + row["CD_COST"].ToString() + "'");
                DataRow[] drs2 = ds_Master.Tables[1].Select("CD_SYSDEF = '" + row["FG_TAX"].ToString() + "'");
                DataRow[] drs3 = ds_Master.Tables[2].Select("CD_SYSDEF = '" + row["COND_PAY"].ToString() + "'");
                DataRow[] drs4 = ds_Master.Tables[3].Select("CD_PARTNER = '" + row["CD_PARTNER"].ToString() + "'");

                if (drs1.Length == 0)
                    s_잘못된부대비용코드 += row["CD_COST"].ToString() + " | ";

                if (drs2.Length == 0)
                    s_잘못된과세구분 += row["FG_TAX"].ToString() + " | ";

                if (drs3.Length == 0)
                    s_잘못된지급조건 += row["COND_PAY"].ToString() + " | ";

                if (drs4.Length == 0)
                    s_잘못된지급거래처코드 += row["CD_PARTNER"].ToString() + " | ";


                if (!Duzon.ERPU.MF.ComFunc.DateCheck(row["DT_PAY"].ToString()))
                    s_잘못된날짜 += row["DT_PAY"].ToString() + " | ";


                if (s_잘못된부대비용코드 != string.Empty || s_잘못된과세구분 != string.Empty || s_잘못된지급거래처코드 != string.Empty || s_잘못된지급조건 != string.Empty || s_잘못된날짜 != string.Empty)
                    b_엑셀자료적합여부 = false;
                else
                {
                    NewRow = dt_temp.NewRow();
                    NewRow["CD_COST"] = row["CD_COST"];
                    NewRow["AM_COST"] = _flex.CDecimal(row["AM_COST"]);
                    NewRow["FG_TAX"] = row["FG_TAX"];
                    NewRow["CD_PARTNER"] = row["CD_PARTNER"];
                    NewRow["LN_PARTNER"] = drs4[0]["LN_PARTNER"];
                    NewRow["DT_PAY"] = row["DT_PAY"];
                    NewRow["COND_PAY"] = row["COND_PAY"];

                    //NewRow["NO_COST"] = this.txt부대비용번호.Text;				//부대비용번호
                    NewRow["NO_LINE"] = GetMaxLine(dt_temp);			//순번
                    NewRow["RT_VAT"] = _flex.CDecimal(drs2[0]["CD_FLAG1"]);						//부가세율
                    NewRow["CD_EXCH"] = "000";				    //환종
                    NewRow["AM_EX"] = 0;							//금액
                    if (_flex.CDecimal(NewRow["RT_VAT"]) == 0 || _flex.CDecimal(NewRow["AM_COST"]) == 0)
                        NewRow["VAT"] = 0;							//부가세액
                    else
                        NewRow["VAT"] = Unit.원화금액(DataDictionaryTypes.TR ,  _flex.CDecimal(NewRow["AM_COST"]) * (_flex.CDecimal(NewRow["RT_VAT"]) / 100));							//부가세액
                    NewRow["AM_DISTRIBUT"] = 0;					//정산금액
                    NewRow["AM_TAXSTD"] = Unit.원화금액(DataDictionaryTypes.TR, D.GetDecimal(row["AM_COST"]));						//정산금액

                    dt_temp.Rows.Add(NewRow);

                }
            }

            if (b_엑셀자료적합여부)
            {
                foreach (DataRow dl in dt_temp.Rows)
                {
                    dl.AcceptChanges();
                    dl.SetAdded();
                    dt_Grid.ImportRow(dl);
                }
            }
            else
            {
                StringBuilder sb_부적격품목리스트 = new StringBuilder();

                sb_부적격품목리스트.AppendLine("<부대비용코드>");
                sb_부적격품목리스트.AppendLine(s_잘못된부대비용코드);
                sb_부적격품목리스트.AppendLine("<과세구분>");
                sb_부적격품목리스트.AppendLine(s_잘못된과세구분);
                sb_부적격품목리스트.AppendLine("<지급거래처>");
                sb_부적격품목리스트.AppendLine(s_잘못된지급거래처코드);
                sb_부적격품목리스트.AppendLine("<지급조건>");
                sb_부적격품목리스트.AppendLine(s_잘못된지급조건);
                sb_부적격품목리스트.AppendLine("<지급예정일>");
                sb_부적격품목리스트.AppendLine(s_잘못된날짜);

                ShowDetailMessage("엑셀자료에 적합하지 않은 값이 있습니다. \n" +
                                  "[︾] 버튼을 눌러 잘못된 내역을 확인하세요!! ", sb_부적격품목리스트.ToString());
                return false;
            }
            return true;
        }

        private void btn_ExcelUp_Click(object sender, EventArgs e)
        {
            try
            {

                if (!HeaderCheck()) return; //필수항목 검사   


                Duzon.Common.Util.Excel excel = null;
                _flex.Redraw = false;

                OpenFileDialog m_FileDlg = new OpenFileDialog();
                m_FileDlg.Filter = "엑셀 파일 (*.xls)|*.xls";

                _flex.DataTable.Clear();
                if (m_FileDlg.ShowDialog() == DialogResult.OK)
                {
                    string FileName = m_FileDlg.FileName;
                    excel = new Duzon.Common.Util.Excel();
                    DataTable dt_EXCEL = excel.StartLoadExcel(FileName);

                    if (!Chk_ExcelData(dt_EXCEL)) return;
                    if (!Check_dt_Excel(dt_EXCEL)) return;

                    if (_flex.HasNormalRow) //포커스주기
                        _flex.Select(_flex.Rows.Fixed, _flex.LeftCol);
                    _flex.IsDataChanged = true;
                    Page_DataChanged(null, null);
                  //  btn_ExcelUp.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                _flex.Redraw = true;

            }
        }

        #endregion

        #region -> 회계전표등록으로 이동
        private void txt전표번호_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string strNoDcu = txt전표번호.Text;
                if (strNoDcu != string.Empty)
                {
                    object[] args = {
                                       strNoDcu, //-- 전표번호
                                       "1", //D.GetString(_flexH["NO_ACCT"]), //-- 회계번호(모르면1)
                                       D.GetString(cbo기표사업장.SelectedValue), //-- 회계단위
                                       Global.MainFrame.LoginInfo.CompanyCode //--회사코드
                                };

                    CallOtherPageMethod("P_FI_DOCU", "전표입력(" + PageName + ")", "P_FI_DOCU", Grant, args);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion


        #endregion

        #region ♣메인버튼 클릭이벤트

        #region -> OnToolBarSearchButtonClicked(메인조회버튼 클릭)

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                //if (!Check()) return;
              
                P_TR_IMCOSTEX_SUB dlg = new P_TR_IMCOSTEX_SUB();

                if (dlg.ShowDialog() == DialogResult.Yes)
                {
                    DataSet ds2 = _biz.Search(dlg.NO_COST);

                    _header.SetDataTable(ds2.Tables[0]);
                    _flex.Binding = ds2.Tables[1];

                    DataSet ds = _biz.Search(this.txt부대비용번호.Text);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //this.bpc부서.CodeValue = ds.Tables[0].Rows[0]["CD_CC"].ToString();
                        //this.bpc부서.CodeName = ds.Tables[0].Rows[0]["NM_CC"].ToString();
                        cbo기표사업장.SelectedValue = ds.Tables[0].Rows[0]["CD_BIZAREA"].ToString();
                        this.bpc부서.CodeValue = ds.Tables[0].Rows[0]["CD_DEPT"].ToString();
                        this.bpc부서.CodeName = ds.Tables[0].Rows[0]["NM_DEPT"].ToString();
                        this.bpc담당자.CodeValue = ds.Tables[0].Rows[0]["NO_EMP"].ToString();
                        this.bpc담당자.CodeName = ds.Tables[0].Rows[0]["NM_KOR"].ToString();
                        this.m_cbo비용형태.SelectedValue = ds.Tables[0].Rows[0]["TY_COST"].ToString();
                        this.cbo처리여부.SelectedValue = ds.Tables[0].Rows[0]["YN_SLIP"].ToString();                        
                    }

                    _정산여부_cnt = System.Int32.Parse(ds.Tables[2].Rows[0]["DIS_CNT"].ToString());
                    _전표처리여부 = System.Int32.Parse(ds.Tables[3].Rows[0]["JUNPYO_CNT"].ToString());
                    SetControlEnabled(); // 조회

                    if (Config.MA_ENV.PJT형여부 == "Y" && _재고평가단위 == "200")
                        _dt_BL_PJT = _biz.Get_BL_PJT(txtBL번호.Text);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> OnToolBarAddButtonClicked(메인추가버튼 클릭)

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeAdd()) return;
                Debug.Assert(_header.CurrentRow != null);
                Debug.Assert(_flex.DataTable != null);

                _flex.DataTable.Rows.Clear();
                _flex.AcceptChanges();

                _header.ClearAndNewRow();

                //this.bpc담당자.CodeValue = Global.MainFrame.LoginInfo.EmployeeNo;
                //this.bpc담당자.CodeName = Global.MainFrame.LoginInfo.EmployeeName;
                bpc담당자.SetCode(Global.MainFrame.LoginInfo.EmployeeNo, Global.MainFrame.LoginInfo.EmployeeName);
                _header.CurrentRow["NO_EMP"] = Global.MainFrame.LoginInfo.EmployeeNo;

                //this.bpc부서.CodeValue = Global.MainFrame.LoginInfo.DeptCode;
                //this.bpc부서.CodeName = Global.MainFrame.LoginInfo.DeptName;
                bpc부서.SetCode(Global.MainFrame.LoginInfo.DeptCode, Global.MainFrame.LoginInfo.DeptName);
                _header.CurrentRow["CD_DEPT"] = Global.MainFrame.LoginInfo.DeptCode;

                // cdcc는??

                //_header.CurrentRow["CD_CC"] = _biz.GetCC(Global.MainFrame.LoginInfo.DeptCode);

                cbo기표사업장.SelectedValue = Global.MainFrame.LoginInfo.BizAreaCode;
                _header.CurrentRow["CD_BIZAREA"] = Global.MainFrame.LoginInfo.BizAreaCode;


                this.msk기표일자.Value = DateTime.Now;
                _header.CurrentRow["DT_BALLOT"] = this.msk기표일자.Text;

                //저장시 등록한다.
                //this.txt부대비용번호.Text = (string)GetSeq(LoginInfo.CompanyCode, "TR", "02", msk기표일자.Text.Substring(0, 6));
                //_header.CurrentRow["NO_COST"] = this.txt부대비용번호.Text;

                DataTable dt = (DataTable)m_cbo비용형태.DataSource;
                if (dt.Rows.Count > 0)
                {
                    m_cbo비용형태.SelectedIndex = 0;
                    _header.CurrentRow["TY_COST"] = m_cbo비용형태.SelectedValue;
                }

                dt = (DataTable)m_cbo비용형태.DataSource;
                if (dt.Rows.Count > 0)
                {
                    cbo처리여부.SelectedIndex = 0;
                    _header.CurrentRow["YN_SLIP"] = cbo처리여부.SelectedValue;
                }

                _header.CurrentRow["FG_STEP"] = "ZZ";

                if (D.GetString(Settings1.Default.TY_COST_EX) != string.Empty)
                    m_cbo비용형태.SelectedValue = Settings1.Default.TY_COST_EX;

                _정산여부_cnt = 0;
                _전표처리여부 = 0;
                SetControlEnabled();  //추가

                //btnBL번호.Enabled = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> OnToolBarDeleteButtonClicked(메인삭제버튼 클릭)

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeDelete()) return;
                if (!MsgAndSave(PageActionMode.Delete)) return;

                if (_정산여부_cnt > 0)
                {
                    ShowMessage("이미 정산처리되어 삭제할수없습니다");
                    return;
                }

                //if (전표처리여부 > 0)
                //{
                //    ShowMessage("이미 전표처리되어 삭제할수없습니다");
                //    return;
                //} 
                string NO_COST = this.txt부대비용번호.Text.ToString();
                string Yn = "Y"; 
                DataTable dt = Global.MainFrame.FillDataTable("SELECT COUNT(1) CNT FROM TR_IMCOSTH WHERE CD_COMPANY = '" + CompanyCode + "' AND NO_COST = '" + NO_COST + "' AND YN_SLIP = '" + Yn + "' ");
       
               // if (dt != null && dt.Rows.Count > 0) 
                if (D.GetDecimal(dt.Rows[0]["CNT"]) > 0)  
                {
                    ShowMessage("이미 전표처리되어 삭제할수없습니다");
                    this.btn전표처리취소.Enabled = true;
                    return;
                }

                _biz.Delete(this.txt부대비용번호.Text);

                /* 초기화 부분 */
                _header.CurrentRow["NO_COST"] = "";

                this.txt부대비용번호.Text = "";

                ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
                OnToolBarAddButtonClicked(null, null);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ♣ 종료(사용자 Default Settings)
        //원래는 화면이 닫힐때 사용자가 정의한 값을 읽어서 저장해야하지만 
        //화면이 닫히는 시점을 알기 어려움으로 해당 컨트롤에 변경이 일어 났을때 바로 저장 시킨다.
        public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
        {
            Settings1.Default.TY_TAX = (rbtn_T_IV.Checked)?"001" : "002";
            Settings1.Default.chk_pop_ptn = chk_통관대행업체사용.Checked;
            Settings1.Default.TY_COST_EX = D.GetString(m_cbo비용형태.SelectedValue);
            //꼭 저장을 해줘야 한다.
            Settings1.Default.Save();

            return base.OnToolBarExitButtonClicked(sender, e);
        }
        #endregion

        #region -> BeforeDelete Override

        protected override bool BeforeDelete()
        {
            if (!base.BeforeDelete()) return false;
            if (ShowMessage(공통메세지.자료를삭제하시겠습니까) != DialogResult.Yes)
                return false;
            return true;
        }

        #endregion

        #region -> OnToolBarSaveButtonClicked(메인저장버튼 클릭)

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSave()) return;
                 ToolBarSaveButtonEnabled = false;
                if (MsgAndSave(PageActionMode.Save))
                {
                    ShowMessage(PageResultMode.SaveGood);

                    /* 정상 저장 후 delecation 이벤트를 발생시켜 LC창의 도움창을
                     * 곧바로 띄운 효과(SP호출)를 내어 결국 no_cost가 바인딩 되게 함. 
                     * 만일 부대비용을 삭제해서 no_cost가 사라진 경우 delegation 메소드를 통해
                     * LC_IN에 no_cost를 null로 변경시키고 다시 부대비용 클릭했을때 Flex에 새로운
                     * Row를 추가시킨다.
                    */
                    if (_flex.HasNormalRow /*&& 부대번호존재유무 == true*/)  // 부대번호가 DB상에 존재한다면 해당 row를 그리드에 바인딩한다.
                    {
                        RealSearch();
                    }
                    else
                    {
                        //부대번호존재유무 = false;  // 부대번호가 DB상에 존재 X - 그리드에 Row를 하나 추가
                        전표_부대비용삭제();
                    }
                    btn삭제.Enabled = true;
                }
                else
                    ToolBarSaveButtonEnabled = true;
                //btnBL번호.Enabled = false;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
                ToolBarSaveButtonEnabled = true;
            }
        }

        #endregion

        #region -> BeforeSave Override

        protected override bool BeforeSave()
        {
            if (!Check())
                return false;

            if (!this.Verify())     // 그리드 체크
                return false;

            return true;
        }

        #endregion

        #region -> BeforeAdd

        //protected override bool BeforeAdd()
        //{
        //    if (!base.BeforeAdd())
        //        return false;

        //    if (!MsgAndSave(PageActionMode.Search))
        //        return false;

        //    return true;
        //}

        #endregion



        #endregion

        #region ♣메소드 정의

        #region -> RealSearch(실 데이터 조회)

        public void RealSearch()
        {
            try
            {
                DataSet ds = _biz.Search(this.txt부대비용번호.Text);

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    this.bpc부서.CodeValue = ds.Tables[0].Rows[0]["CD_CC"].ToString();
                //    this.bpc부서.CodeName = ds.Tables[0].Rows[0]["NM_CC"].ToString();
                //    cbo기표사업장.SelectedValue = ds.Tables[0].Rows[0]["CD_BIZAREA"].ToString();
                //    this.bpc부서.CodeValue = ds.Tables[0].Rows[0]["CD_DEPT"].ToString();
                //    this.bpc부서.CodeName = ds.Tables[0].Rows[0]["NM_DEPT"].ToString();
                //    this.bpc담당자.CodeValue = ds.Tables[0].Rows[0]["NO_EMP"].ToString();
                //    this.bpc담당자.CodeName = ds.Tables[0].Rows[0]["NM_KOR"].ToString();
                //    m_cbo비용형태.SelectedValue = ds.Tables[0].Rows[0]["TY_COST"].ToString();
                //}

                _정산여부_cnt = System.Int32.Parse(ds.Tables[2].Rows[0]["DIS_CNT"].ToString());
                _전표처리여부 = System.Int32.Parse(ds.Tables[3].Rows[0]["JUNPYO_CNT"].ToString());                

                _header.SetDataTable(ds.Tables[0]);
                _flex.Binding = ds.Tables[1];

                SetControlEnabled(); //조회

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }

        }

        #endregion

        #region -> Check(필수 입력사항 체크)

        private bool HeaderCheck()
        {
            if (this.bpc부서.CodeValue.ToString() == "" || this.bpc부서.CodeValue == string.Empty)
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, t_CostCenter.Text);
                return false;
            }

            else if (bpc담당자.CodeValue == "" || bpc담당자.CodeValue == string.Empty)
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, t_NmEmp.Text);
                return false;
            }

            else if (cbo기표사업장.SelectedValue == DBNull.Value || cbo기표사업장.SelectedValue == null || cbo기표사업장.SelectedValue.ToString() == "" || cbo기표사업장.SelectedValue.ToString() == string.Empty)
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, t_WorkBallot.Text); // 기표사업장
                return false;
            }
            else if ((txtBL번호.Text.Trim() == null || txtBL번호.Text.Trim() == "" || txtBL번호.Text.Trim() == string.Empty))
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, t_NoBL.Text); // 기표사업장
                return false;
            }
            else if (m_cbo비용형태.SelectedValue == DBNull.Value || m_cbo비용형태.SelectedValue == null || m_cbo비용형태.SelectedValue.ToString() == "" || m_cbo비용형태.SelectedValue.ToString() == string.Empty)
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, txt비용형태.Text);
                return false;
            }

            return true;
        }

        private bool Check()
        {

            if (!HeaderCheck()) return false;

            if (!_flex.HasNormalRow)
            {
                ShowMessage("부대비용 항목을 입력하십시오."); //라인데이터가 없다
                return false;

            }
            else
            {
                if (_flex.Rows.Count > 1)
                {
                    if (_flex["FG_TAX"].ToString().Trim() == "21" && _flex["CD_PARTNER"].ToString().Trim() == "")
                    {
                        ShowMessage("지급거래처를 입력하세요"); // 기표사업장
                        return false;
                    }
                }
            }
            return true;
        }

        #endregion

        #region -> 최대항번 구하는 메소드(GetMaxLine)

        private int GetMaxLine(DataTable pDt)
        {
            int li_line = 0;
            try
            {
                if (pDt.Rows.Count > 0)
                    li_line = System.Int32.Parse(pDt.Compute("MAX(NO_LINE)", "").ToString());
            }
            catch
            {
            }
            return ++li_line;
        }

        #endregion

        #region -> 추가모드여부

        private bool 추가모드여부
        {
            get
            {
                if (_header.JobMode == JobModeEnum.추가후수정)
                    return true;
                return false;
            }
        }


        private bool 처리여부
        {
            get 
            {
                if (_header.CurrentRow["YN_SLIP"].Equals("Y"))
                {
                    return true;
                }
                return false;
            }
        }

        #endregion

        #region -> IsChanged

        protected override bool IsChanged()
        {
            if (base.IsChanged())       // 그리드가 변경되었거나
                return true;

            return 헤더변경여부;        // 헤더가 변경되었거나

            //if ( base.IsChanged() || _header.GetChanges() != null )       // 그리드가 변경되었거나
            //    return true;

            //return false;
        }

        #endregion

        #region -> 전표_부대비용삭제()

        public void 전표_부대비용삭제()
        {
            try
            {
                if (_정산여부_cnt > 0)
                {
                    ShowMessage("이미 정산처리되어 삭제할수없습니다");
                    return;
                }

                _biz.Delete(this.txt부대비용번호.Text);

                /* 초기화 부분 */
                _header.CurrentRow["NO_COST"] = "";

                this.txt부대비용번호.Text = "";

                OnToolBarAddButtonClicked(null, null);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> MainForm의 조회/추가 버튼 클릭시 발생 이벤트(_header_JobModeChanged)

        void _header_JobModeChanged(object sender, FreeBindingArgs e)
        {
            try
            {
                //if (e.JobMode == JobModeEnum.추가후수정)
                //{
                //        this.txtLC번호.Enabled = true;
                //        this.txt거래처.Enabled = true;
                //        this.msk기표일자.Enabled = true;
                //        txtBL번호.Enabled = true;
                //        this.bpc부서.Enabled = true;
                //        this.cbo기표사업장.Enabled = true;
                //        this.bpc담당자.Enabled = true;
                //        this.btn삭제.Enabled = true;
                //        cbo처리여부.Enabled = true;
                //        m_cbo비용형태.Enabled = true;

                //}
                //else // 조회 후 수정
                //{
                //        this.txtLC번호.ReadOnly = true;
                //        this.txt거래처.ReadOnly = true;
                //        this.msk기표일자.Enabled = true;
                //        txtBL번호.ReadOnly = true;
                //        this.btn삭제.Enabled = false;
                //        cbo기표사업장.Enabled = false;

                //}

                
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
               
        #endregion

        #region -> 헤더값 변경시 발생 이벤트(_header_ControlValueChanged)

        void _header_ControlValueChanged(object sender, FreeBindingArgs e)
        {
            try
            {
                switch (((Control)sender).Name)
                {
                    case "m_cbo비용형태":
                        _header.CurrentRow["TY_COST"] = m_cbo비용형태.SelectedValue;
                        break;
                    case "m_cboTest":
                        break;
                }
                Page_DataChanged(null, null);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> OnBpControl_QueryAfter
        private void OnBpControl_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                if (sender is BpCodeTextBox)
                {
                    switch (((BpCodeTextBox)sender).Name)
                    {
                        case "bpc부서":
                            //_header.CurrentRow["CD_CC"] = _biz.GetCC(e.HelpReturn.Rows[0]["CD_DEPT"].ToString());
                            break;
                        case "bpc담당자":
                            _header.CurrentRow["CD_DEPT"] = D.GetString(e.HelpReturn.Rows[0]["CD_DEPT"]);
                            bpc부서.CodeValue = D.GetString(e.HelpReturn.Rows[0]["CD_DEPT"]);
                            bpc부서.CodeName = D.GetString(e.HelpReturn.Rows[0]["NM_DEPT"]);
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

        public void SetControlEnabled()
        {

            Boolean  bflag = true;

            if (_전표처리여부 > 0 || _정산여부_cnt > 0)
                 bflag = false;
                         
            // 버튼         
            btn전표발행.Enabled = bflag;
            btn전표처리취소.Enabled = !bflag;

            btn삭제.Enabled = bflag;
            btn추가.Enabled = bflag;
            btn_ExcelUp.Enabled = bflag;

            //버튼외
            txtBL번호.Enabled = bflag;
            txt거래처.Enabled = bflag;
            
            txtLC번호.Enabled = bflag;
            bpc부서.Enabled = bflag;
            cbo기표사업장.Enabled = bflag;
            bpc담당자.Enabled = bflag;
            msk기표일자.Enabled = bflag;
            cbo처리여부.Enabled = bflag;
            m_cbo비용형태.Enabled = bflag;
            btnBL번호.Enabled = bflag;

            
            if (_전표처리여부 < 1)
            {
                msk기표일자.Enabled = true;
                cbo처리여부.Enabled = true;
                m_cbo비용형태.Enabled = true;
                btn전표발행.Enabled = true;
                btn통관.Enabled = true;
                if(Global.MainFrame.ServerKey != "KPIC")
                    txt통관번호.ReadOnly = false;
                else
                    txt통관번호.ReadOnly = true;
                cbo_YN_Jeonja.Enabled = true;
            }
            else 
            {
                btn전표발행.Enabled = false;                
                btn통관.Enabled = false;
                txt통관번호.ReadOnly = true;
                cbo_YN_Jeonja.Enabled = false;
            }
            
            if (bflag)
            {
                if (_header.JobMode == JobModeEnum.조회후수정)
                {
                    txtBL번호.ReadOnly = true;
                    btnBL번호.Enabled = false;
                    cbo기표사업장.Enabled = false;

                    txt통관번호.ReadOnly = true;        // 2011-08-12, 최승애 추가
                }

            }
        }

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

        private void btn통관_Click(object sender, EventArgs e)
        {
            try
            {

                if ((txtBL번호.Text.Trim() == null || txtBL번호.Text.Trim() == "" || txtBL번호.Text.Trim() == string.Empty))
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, t_NoBL.Text); // 기표사업장
                    return;
                }



                P_TR_TO_NO2 dlg = new P_TR_TO_NO2(txtBL번호.Text);

                 if (dlg.ShowDialog() == DialogResult.OK)
                 {
                     this.txtBL번호.Text = dlg.BL번호;
                     _header.CurrentRow["NO_BL"] = dlg.BL번호;

                     this.txtLC번호.Text = dlg.LC참조번호;
                     _header.CurrentRow["NO_LC"] = dlg.LC참조번호;


                     this.txt통관번호.Text = dlg.통관번호;
                     _header.CurrentRow["NO_TO"] = dlg.통관번호;
                     

                     _header.CurrentRow["CD_PARTNER"] = dlg.거래처코드;

                     this.txt거래처.Text = dlg.거래처명;
                     _header.CurrentRow["LN_PARTNER"] = dlg.거래처명;

                     cbo_YN_Jeonja.SelectedValue = dlg.YN_JEONJA;
                     _header.CurrentRow["YN_JEONJA"] = dlg.YN_JEONJA;

                     bpNm_CC.SetCode(dlg.CC코드, dlg.CC명);
                     _header.CurrentRow["CD_CC"] = dlg.CC코드;
                     _header.CurrentRow["NM_CC"] = dlg.CC명;

                     Page_DataChanged(null, null);

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

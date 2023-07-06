using System;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.MF;

namespace pur
{
    // **************************************
    // 작   성   자 : 유지영
    // 재 작  성 일 : 2006-11-09
    // 모   듈   명 : 구매/자재
    // 시 스  템 명 : 입고관리
    // 서브시스템명 : 구매입고등록
    // 페 이 지  명 : 구매입고등록
    // 프로젝트  명 : P_PU_GR_REG
    // **************************************
    /// <summary>
    /// 2010.06.10 신미란 : 입고등록시 입고담당자가 상단에 적힌데로 들어가지 않고 LINE값과 동일하게 입력되어 있었다
    /// 2010.08.16 신미란 - WH 사용에 따른 등록 (최석제)
    /// 2010.09.10 안종호 - 라인그리드 발주헤더비고추가(김헌섭대리)
    /// 2011.03.24 송철호 - 라인그리드 품목계정 헤더추가
    /// </summary>
    public partial class P_PU_GR_REG : Duzon.Common.Forms.PageBase
    {
        #region ♣ 초기화
        private string m_sEnv = "N";
        private string m_sEnv_WH = "000";
        public string MNG_SERIAL = string.Empty; //시스템통제등록 SERIAL사용여부
        private string m_sPJT재고사용 = "000";
        private bool m_bPJT사용 = false; // 환경설정
        decimal old_am;
        decimal old_am_ex;
        bool bAUTHUM = true;
        bool bAUTHAM = true;
        bool bAUTHQT = true;

        bool bStandard = false; //규격형

        #region -> 생성자

        private P_PU_GR_REG_BIZ _biz;

        public P_PU_GR_REG()
        {
            try
            {
                InitializeComponent();

                MainGrids = new FlexGrid[] { _flexM, _flexD };
                DataChanged += new EventHandler(Page_DataChanged);

                _flexM.WhenRowChangeThenGetDetail = true;
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

            _biz = new P_PU_GR_REG_BIZ();

            m_sEnv = _biz.EnvSearch();
            m_sEnv_WH = Duzon.ERPU.MF.ComFunc.전용코드("W/H 정보사용");

            //규격형 사용 유무
            if (BASIC.GetMAEXC("공장품목등록-규격형") == "100")
            {
                bStandard = true;

            }

            DataTable dtAUTH = Duzon.ERPU.MF.Common.BASIC.MFG_AUTH("P_PU_GR_REG");
            if (dtAUTH.Rows.Count > 0)
            {
                bAUTHUM = (dtAUTH.Rows[0]["YN_UM"].ToString() == "Y") ? false : true;
                bAUTHAM = (dtAUTH.Rows[0]["YN_AM"].ToString() == "Y") ? false : true;
                bAUTHQT = (dtAUTH.Rows[0]["YN_QT"].ToString() == "Y") ? false : true;
            }

            InitGridM();
            InitGridD();

            _flexM.DetailGrids = new FlexGrid[] { _flexD };
            _flexM.WhenRowChangeThenGetDetail = true;          // true이면 Header의 Row가 바뀔때마다 Line 내용을 그때 그때 가져온다는 뜻.

            DataTable dt = _biz.Search_SERIAL();
            MNG_SERIAL = dt.Rows[0]["YN_SERIAL"].ToString();


        }

        #endregion

        #region -> InitGridM

        private void InitGridM()
        {
            _flexM.BeginSetting(1, 1, false);       //false로 세팅하면 _flexM.NewRowEditable = false와 _flexM.EnterKeyAddRow = false를 의미한다.

            _flexM.SetCol("S", "S", 50, true, CheckTypeEnum.Y_N);
            _flexM.SetCol("NO_RCV", "의뢰번호", 120);
            _flexM.SetCol("CD_PLANT", "공장코드", 80);
            _flexM.SetCol("NM_PLANT", "공장", 120);
            _flexM.SetCol("CD_PARTNER", "거래처코드", 80);
            _flexM.SetCol("LN_PARTNER", "거래처", 120);
            _flexM.SetCol("DT_REQ", "의뢰일자", 100, false, typeof(String), FormatTpType.YEAR_MONTH_DAY);
            _flexM.SetCol("NM_FG_TRANS", "거래구분", 80);
            _flexM.SetCol("YN_AM", "유무환구분", 80);
            _flexM.SetCol("DC_RMK", "의뢰비고1", 150);
            _flexM.SetCol("NO_EMP", "입고의뢰자코드", 80);
            _flexM.SetCol("NM_KOR", "입고의뢰자명", 120);

            _flexM.SetDummyColumn("S");

            //_flexM.NewRowEditable = false;
            //_flexM.EnterKeyAddRow = false;

            _flexM.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            _flexM.AfterRowChange += new RangeEventHandler(_flexM_AfterRowChange);
            _flexM.AfterEdit += new RowColEventHandler(_flexM_AfterEdit);
            _flexM.StartEdit += new RowColEventHandler(_flex_StartEdit);
            _flexM.CheckHeaderClick += new EventHandler(_flexM_CheckHeaderClick);
        }

        #endregion

        #region -> InitGridD

        private void InitGridD()
        {
            _flexD.BeginSetting(1, 1, false);

            _flexD.SetCol("S", "S", 50, true, CheckTypeEnum.Y_N);
            _flexD.SetCol("CD_ITEM", "품목코드", 120);
            _flexD.SetCol("NM_ITEM", "품목명", 80);
            _flexD.SetCol("STND_ITEM", "규격", 80);
            //품목계정 헤더 추가 2011.03.24 송철호
            _flexD.SetCol("NM_CLS_ITEM", "품목계정", 80);
            _flexD.SetCol("CD_UNIT_MM", "단위", 120);
            _flexD.SetCol("NO_LOT", "LOT여부", 80);

            if (_biz.Get구매입고등록_검사 == "200")
            {
                _flexD.SetCol("YN_INSP", "검사", 30, true, CheckTypeEnum.Y_N); 
            }
            else
            {
                _flexD.SetCol("YN_INSP", "검사", 30, false, CheckTypeEnum.Y_N);
            }
            _flexD.SetCol("TP_UM_TAX", "부가세여부", 70, false);
            if (bAUTHQT)
            {
                _flexD.SetCol("QT_MM", "의뢰량", 120, false, typeof(decimal), FormatTpType.QUANTITY);
                _flexD.SetCol("QT_REQ_RCV", "양품입고량", 120, true, typeof(decimal), FormatTpType.QUANTITY);
                _flexD.SetCol("QT_GOOD_INV", "양품수량", 120, true, typeof(decimal), FormatTpType.QUANTITY);
                _flexD.SetCol("QT_REQ_U", "관리수량", 120, (m_sEnv == "Y") ? true : false, typeof(decimal), FormatTpType.QUANTITY);

                //_flexD.SetCol("QT_REJECT_REQ_MM", "불량입고량", 120, false, typeof(decimal), FormatTpType.QUANTITY);
            }
            _flexD.SetCol("NM_SL", "창고", 120, true);
            _flexD.SetCol("UNIT_IM", "관리단위", 80);
            //_flexD.SetCol("QT_REJECT_REQ", "불량수량", 120, true, typeof(decimal), FormatTpType.QUANTITY);
            
            
            if (bStandard)
            {
                _flexD.SetCol("UM_WEIGHT", "중량단가", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                _flexD.SetCol("TOT_WEIGHT", "총중량", 100, false, typeof(decimal));

                _flexD.Cols["TOT_WEIGHT"].Format = "###,###,###.#";
            }


            _flexD.SetCol("NO_BL", "BL번호", 120 );
            _flexD.SetCol("CD_PJT", "프로젝트코드", 100);
            _flexD.SetCol("NM_PROJECT", "프로젝트", 100);
            _flexD.SetCol("DC_RMK", "의뢰라인비고1", 150, 200);
            _flexD.SetCol("DC50_PO", "발주비고", 150, 40);
            _flexD.SetCol("CD_ZONE", "LOCATION", 100, false);
            _flexD.SetCol("DT_LIMIT", "납기일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexD.SetCol("NO_PO", "발주번호", 100, false);
            

            if (m_sEnv_WH.Equals("100"))
                _flexD.SetCol("CD_WH", "W/H", 100, true);

            _flexD.SetCol("SEQ_PROJECT", "프로젝트항번", 100);
            _flexD.SetCol("CD_UNIT", "프로젝트품목코드", 100);
            _flexD.SetCol("NM_UNIT", "프로젝트품목명", 100);

            _flexD.EnterKeyAddRow = false;

            _flexD.SetDummyColumn("S","MEMO_CD","CHECK_PEN"); 
            _flexD.SetCodeHelpCol("NM_SL", Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CD_SL", "NM_SL" }, new string[] { "CD_SL", "NM_SL" });

            if (App.SystemEnv.PROJECT사용 == true)
                _flexD.VerifyNotNull = new string[] { "CD_ITEM", "NM_SL", "NM_PROJECT" };
            else
                _flexD.VerifyNotNull = new string[] { "CD_ITEM", "NM_SL" };

            _flexD.ValidateEdit += new C1.Win.C1FlexGrid.ValidateEditEventHandler(_flexD_ValidateEdit);
            _flexD.BeforeCodeHelp += new BeforeCodeHelpEventHandler(_flexD_BeforeCodeHelp);
            _flexD.StartEdit += new RowColEventHandler(_flex_StartEdit);
            _flexD.AfterCodeHelp += new AfterCodeHelpEventHandler(_flexD_AfterCodeHelp);

            //2011.03.24 버전 증가
            _flexD.SettingVersion = "1.0.0.2";

            _flexD.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            // 메모기능 & 체크
            _flexD.CellNoteInfo.EnabledCellNote = true;// 메모기능활성화 
            _flexD.CellNoteInfo.CategoryID = this.Name; // page 명입력 // 같은page명을입력했을경우여러화면에서볼수있습니다. 
            _flexD.CellNoteInfo.DisplayColumnForDefaultNote = "NM_ITEM";  // 마킹& 메모가보여질컬럼설정 

            _flexD.CheckPenInfo.EnabledCheckPen = true;// 체크펜기능활성화  
            _flexD.CellContentChanged += new CellContentEventHandler(_flexD_CellContentChanged);   // 메모& 체크추가, 삭제(수정제외) 되었을경우이벤트가발생.
            

        }

        #endregion

        #region -> InitCombo

        private void InitCombo()
        {
            try
            {
                DataSet ds = GetComboData("NC;MA_PLANT",  "N;PU_C000005");

                cb_cd_plant.DataSource = ds.Tables[0];
                cb_cd_plant.DisplayMember = "NAME";
                cb_cd_plant.ValueMember = "CODE";

                // 부가세여부
                _flexD.SetDataMap("TP_UM_TAX", ds.Tables[1], "CODE", "NAME"); 

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> InitControl

        private void InitControl()
        {
            try
            {
                periodPicker1.StartDateToString = MainFrameInterface.GetStringFirstDayInMonth;
                periodPicker1.EndDateToString = MainFrameInterface.GetStringToday;
                tb_DT_IO.Text = MainFrameInterface.GetStringToday;

                tb_no_emp2.CodeName = LoginInfo.EmployeeName;
                tb_no_emp2.CodeValue = LoginInfo.EmployeeNo;

                rb_not_pc.Checked = true;

                if (m_sEnv_WH.Equals("100"))
                {
                    _flexD.SetDataMap("CD_WH", GetComboData("S;MA_WH").Tables[0], "CODE", "NAME");// WH
                    //btn_apply.Visible = false;
                }


            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> InitPaint

        protected override void InitPaint()
        {
            base.InitPaint();

            //-->> 2011.04.29 추가(최규원)
            m_sPJT재고사용 = Duzon.ERPU.MF.ComFunc.전용코드("프로젝트재고사용");
            m_bPJT사용 = App.SystemEnv.PROJECT사용;
            //<<--

            InitCombo();
            InitControl();



            oneGrid1.UseCustomLayout = true;
            oneGrid2.UseCustomLayout = true;
            bpPanelControl1.IsNecessaryCondition = true;
            bpPanelControl2.IsNecessaryCondition = true;
            bpPanelControl7.IsNecessaryCondition = true;
            bpPanelControl10.IsNecessaryCondition = true;
            bpPanelControl11.IsNecessaryCondition = true;
            bpPanelControl12.IsNecessaryCondition = true;
            oneGrid1.InitCustomLayout();
            oneGrid2.InitCustomLayout();
        }
        #endregion

        #region -> Page_DataChanged

        void Page_DataChanged(object sender, EventArgs e)
        {
            ToolBarAddButtonEnabled = false;
            ToolBarDeleteButtonEnabled = false;
        }

        #endregion

        #endregion

        #region ♣ 메인버튼이벤트

        #region -> BeforeSearch Override

        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch()) return false;

            if (periodPicker1.StartDateToString == "")  //의뢰일자 시작일
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, lb_dt_req.Text);
                return false;
            }

            if (periodPicker1.EndDateToString == "")    //의뢰일자 종료일
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, lb_dt_req.Text);
                return false;
            }

            if (cb_cd_plant.SelectedValue == null || D.GetString(cb_cd_plant.SelectedValue) == "")    //입고공장
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, lb_gl_plant2.Text);
                return false;
            }

            return true;
        }

        #endregion

        #region -> 조회

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSearch()) return;

                string DT_FROM = periodPicker1.StartDateToString;
                string DT_TO = periodPicker1.EndDateToString;
                string CD_PLANT = D.GetString(cb_cd_plant.SelectedValue);
                string CD_SL = "";
                string CD_PARTNER = tb_nm_partner.CodeValue;
                string FG_RCV = tb_FG_TPRCV.CodeValue;
                string NO_EMP = tb_no_emp.CodeValue;
                string CD_PJT = bp_프로젝트.CodeValue;

                DataTable dt_Main = _biz.Search_Main(DT_FROM, DT_TO, CD_PLANT, CD_PARTNER, NO_EMP, CD_SL, FG_RCV, CD_PJT);
                DataTable dt_Detail = _biz.Search_Detail("","");
                
                _flexD.Binding = dt_Detail;
                _flexM.Binding = dt_Main; 	// 요기에서 곧바로 AfterRowColChange 이벤트 발생

                if (!_flexM.HasNormalRow)
                    ShowMessage(PageResultMode.SearchNoData);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> BeforeSave Override

        protected override bool BeforeSave()
        {
            if (!base.BeforeSave())
                return false;

            if (tb_DT_IO.Text == "")            //입고일자
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, lb_dt_rcv.Text);
                return false;
            }

            if (tb_no_emp2.CodeValue == "")     //담당자
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, lb_no_emp2.Text);
                return false;
            }

            if (!Verify())     // 그리드 체크
                return false;

            return true;
        }

        #endregion

        #region -> 저장

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSave()) return;

                if (MsgAndSave(PageActionMode.Save))
                {
                    ShowMessage(PageResultMode.SaveGood);
                    OnToolBarSearchButtonClicked(sender, e);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> SaveData

        protected override bool SaveData()
        {
            DataTable dtD = _flexD.GetChanges();

            DataRow[] dtDrow = dtD.Select();

            if (dtD == null) return true;

            bool chk = false;

            /*입고량이 있는 컬럼탐색중...*/
            for (int i = 0; i < dtDrow.Length; i++)
            {
                if ((_flexD.CDecimal(dtDrow[i]["QT_REQ_RCV"]) + _flexD.CDecimal(dtDrow[i]["QT_REJECT_REQ_MM"])) != 0)   //if (_flexD.CDecimal(dtD.Rows[i]["QT_REQ_RCV"].ToString()) + _flexD.CDecimal(dtD.Rows[i]["QT_REJECT_REQ_MM"].ToString()) != 0)
                    chk = true;

                if (dtDrow.Length > 1 && (dtDrow[dtDrow.Length - 2]["CD_PARTNER"].ToString()) != dtDrow[i]["CD_PARTNER"].ToString())
                {
                    ShowMessage("하나의 입고번호는 동일한 거래처이어야 합니다. 데이타를 확인하여 주십시요");
                    return false;
                }
            }

            if (!chk)
            {
                ShowMessage("PU_M000114");  //입고량이 존재하지 않으므로 자료를 저장할 수 없습니다.
                return false;
            }
              

            // 관리수량과 재고수량이 다른경우 
            DataRow[] drs = dtD.Select("(QT_REQ_RCV * RATE_EXCHG) <> QT_REQ_U", "");

            if (m_sEnv == "Y" && drs != null && drs.Length > 0)
            {
                P_PU_REQCHK_SUB m_dlg = new P_PU_REQCHK_SUB(MainFrameInterface, dtD, "P_PU_GR_REG");
                if (m_dlg.ShowDialog(this) == DialogResult.Cancel) 
                    return false;

                // 선택되지 않는것은 관리수량과 
                DataTable _rdt = m_dlg.gdt_return;

                if (_rdt != null && _rdt.Rows.Count > 0)
                {
                    //  foreach (DataRow row in _rdt)
                    for (int i = 0; i < _rdt.Rows.Count; i++)
                    {
                        //drs = dtD.Select("NO_RCV = '" + _rdt.Rows[i]["NO_RCV"].ToString() + "' AND NO_LINE = " + _rdt.Rows[i]["NO_LINE"].ToString(), "");
                        //if (_rdt != null && _rdt.Rows.Count > 0)
                        //{
                        //    drs[0]["QT_REQ_U"] = Convert.ToDecimal(drs[0]["QT_REQ_RCV"]) * Convert.ToDecimal(drs[0]["RATE_EXCHG"]);
                        //}

                        for (int row = 0; row < _flexD.DataTable.Rows.Count; row++)
                        {
                            if (_flexD.DataTable.Rows[row]["NO_RCV"].ToString() == _rdt.Rows[i]["NO_RCV"].ToString() &&
                                _flexD.DataTable.Rows[row]["NO_LINE"].ToString() == _rdt.Rows[i]["NO_LINE"].ToString())
                            {
                                _flexD.DataTable.Rows[row]["QT_REQ_U"] = Convert.ToDecimal(_flexD.DataTable.Rows[row]["QT_REQ_RCV"]) * Convert.ToDecimal(_flexD.DataTable.Rows[row]["RATE_EXCHG"]);
                                break;
                            }
                        }
                    }
                    dtD = _flexD.GetChanges();
                }
            }


            string no_seq = (string)GetSeq(LoginInfo.CompanyCode, "PU", "06", tb_DT_IO.MaskEditBox.ClipText.Substring(0, 6));
            decimal max_no_line = _flexD.GetMaxValue("NO_IOLINE");   // NO_LINE 이라는 컬럼에서 최대값 가져옴.
        
            for (int i = 0; i < dtD.Rows.Count; i++)
            {
                max_no_line++;
                dtD.Rows[i]["NO_IO"] = no_seq;
                dtD.Rows[i]["DT_IO"] = D.GetString(tb_DT_IO.Text);
                dtD.Rows[i]["NO_IOLINE"] = max_no_line; 
            }



            DataTable dt_Qtio = null; /*MM_QTIO에 들어갈 데이터는 라인수에 상관없이 1개이기 때문에 1줄만 IMPORT해준다*/
            DataTable dtLOT = null;
            DataTable dtSERL = null;


            dt_Qtio = dtD.Clone();
            dt_Qtio.ImportRow(dtD.Rows[0]); 


            #region -> StorageLocation S/L간 분할입고도움창
            bool bStorageLocation = BASIC.GetMAEXC("W/H 정보사용") == "100" ? true : false;
            if (bStorageLocation)
            {
                DataRow[] drs1 = null;
                DataTable dtSave = null;
                if (_flexD.DataTable != null)
                {
                    dtSave = _flexD.DataTable.Clone();
                    //drs1 = _flexD.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);
                    foreach (DataRow dr in dtD.Rows)
                    {
                        dtSave.Rows.Add(dr.ItemArray);
                    }
                }

                P_PU_SL_SUB_R m_dlg = new P_PU_SL_SUB_R(dtSave);//P_PU_SL_SUB_R m_dlg = new P_PU_SL_SUB_R(dtSave, two_Unit_Mng, qtso_AddAllowYN, Am_Recalc);

                if (m_dlg.ShowDialog(this) == DialogResult.OK)
                    dtD = m_dlg.dtL;
                else
                    return false;
            }
            #endregion


            //if (String.Compare(Global.MainFrame.LoginInfo.MngLot, "Y") == 0 && dtD != null)
            //{
            //    DataRow[] DR = dtD.Select("NO_LOT = 'YES'");
            //    DataTable _dtLOT = dtD.Clone();

            //    if (DR.Length > 0)
            //    {
            //        foreach (DataRow drLOT in DR)
            //        {
            //            _dtLOT.ImportRow(drLOT);
            //        }

            //        P_PU_LOT_SUB_R m_dlg = new P_PU_LOT_SUB_R(_dtLOT);  

            //        if (m_dlg.ShowDialog(this) == DialogResult.OK)
            //            dtLOT = m_dlg.dtL;
            //        else
            //            return false;
            //    }
            //}

            // m_sPJT재고사용 관련
            // PJT재고를 표현하기 어려 LOT재고로 대치 NO_LOT에 CD_PJT넣어 관리
            // 김광석, 김형석  -- 2011.04.29 추가(최규원)
            if (String.Compare(Global.MainFrame.LoginInfo.MngLot, "Y") == 0 && dtD != null)
            {
                DataTable _dtLOT = dtD.Clone();
                _dtLOT = new DataView(dtD, "NO_LOT = 'YES'", "", DataViewRowState.CurrentRows).ToTable();
                if (_dtLOT.Rows.Count > 0)
                {
                    //foreach (DataRow drLOT in DR)
                    //{
                    //    _dtLOT.ImportRow(drLOT);
                    //}

                    if (m_sPJT재고사용 == "100" && m_bPJT사용)
                    {
                        dtLOT = _dtLOT;
                        dtLOT.Columns.Add("FG_PS", typeof(string), "1");
                        dtLOT.Columns.Add("QT_IO", typeof(decimal));
                        dtLOT.Columns.Add("NO_IOLINE2", typeof(decimal), "0");
                        dtLOT.Columns.Add("DC_LOTRMK", typeof(string), "");
                        dtLOT.Columns.Remove("YN_RETURN");
                        foreach (DataRow dr_lot in dtLOT.Rows)
                        {
                            dr_lot["NO_LOT"] = dr_lot["CD_PJT"];
                            dr_lot["QT_IO"] = dr_lot["QT_GOOD_INV"];
                            //                                dr_lot["DT_LIMIT"] = null;
                        }

                    }
                    else
                    {
                        P_PU_LOT_SUB_R m_dlg = new P_PU_LOT_SUB_R(_dtLOT);

                        if (m_dlg.ShowDialog(this) == DialogResult.OK)
                            dtLOT = m_dlg.dtL;
                        else
                            return false;
                    }

                }
            }

            //시리얼추가 
            if (String.Compare(MNG_SERIAL, "Y") == 0 && dtD != null)
            {
                DataRow[] DR = dtD.Select("NO_SERL = 'YES'");
                DataTable _dtSERL = dtD.Clone();

                if (DR.Length > 0)
                {
                    foreach (DataRow drSERL in DR)
                    {
                        _dtSERL.ImportRow(drSERL);
                    }

                    P_PU_SERL_SUB_R m_dlg3;

                    m_dlg3 = new P_PU_SERL_SUB_R(_dtSERL);
                    if (m_dlg3.ShowDialog(this) == DialogResult.OK)
                        dtSERL = m_dlg3.dtL;
                    else
                        return false;
                }
            }

            dt_Qtio.Rows[0]["DC_RMK"] = (tb_dc_rmk.Text).Trim();
            dt_Qtio.Rows[0]["NO_EMP"] = tb_no_emp2.GetCodeValue();
            //dt_Qtio.Rows[0]["DT_IO"] = tb_dc_rmk.Text;

            if (dtSERL != null && dtSERL.Rows.Count != 0)
            {
                dtSERL.Columns.Add("CD_PLANT");

                foreach (DataRow dr in dtSERL.Rows)
                {
                    dr["CD_PLANT"] = D.GetString(cb_cd_plant.SelectedValue);
                }
            }

            #region -> LOCATION 등록
            DataTable dt_location = null;
            if (Config.MA_ENV.YN_LOCATION == "Y") //시스템환경설정에서 LOCATION사용인것만 창고별로 사용인지 아닌지는 도움창 호출후 판단한다. 붙여야하는화면이 많은 관계로 여기서 통합으로 처리해주는걸로 판단함
            {                                           //넘길때 공장,창고,품목은 필수항목

                bool b_lct = false;
                DataTable dt_lc = dtD.Clone().Copy();
                foreach (DataRow dr in dtD.Select())
                    dt_lc.LoadDataRow(dr.ItemArray, true);


                dt_location = P_OPEN_SUBWINDOWS.P_MA_LOCATION_R_SUB(dt_lc, out b_lct);

                if (b_lct == false)
                    return false;

            }
            #endregion

            bool bSuccess = _biz.Save(dt_Qtio, dtD, dtLOT, dtSERL, dt_location);

            if (!bSuccess)
                return false;
            else
            {
                _flexM.AcceptChanges();
                _flexD.AcceptChanges();
            }
            return true;
        }

        #endregion

        #region -> 출력

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            //try
            //{
            //    if (추가모드여부) return;

            //    ReportHelper rptHelper = new ReportHelper("R_PU_GR_REG_001", "구매입고전표");

            //    rptHelper.SetDataTable(_flexD.DataTable);
            //    rptHelper.SetData("NO_PR", tb_no_pr.Text);
            //    rptHelper.SetData("DT_PR", tb_dt_pr.Text.Substring(0, 4) + "/" + tb_dt_pr.Text.Substring(4, 2) + "/" + tb_dt_pr.Text.Substring(6, 2));
            //    rptHelper.SetData("CD_PLANT", cbo_CD_PLANT.Text);
            //    rptHelper.SetData("NM_DEPT", tb_cd_dept.Text);
            //    rptHelper.SetData("NM_KOR", tb_no_emp.CodeName);
            //    rptHelper.SetData("NM_PJT", tb_cd_pjt.CodeName);
            //    rptHelper.SetData("DC_RMK", tb_dc50_pr.Text);
            //    rptHelper.Print();

            //}
            //catch (coDbException ex)
            //{
            //    ShowErrorMessage(ex, PageName);
            //}
            //catch (Exception ex)
            //{
            //    ShowErrorMessage(ex, PageName);
            //}
            //finally
            //{
            //    Cursor.Current = Cursors.Default;
            //}
        }

        #endregion

        #endregion

        #region ♣ 도움창 이벤트

        #region -> 도움창 호출전 세팅 이벤트(OnBpCodeTextBox_QueryBefore)

        private void OnBpCodeTextBox_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                switch (e.HelpID)
                {
                    case Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB:		// 창고 도움창
                        if (_flexM.DataTable != null && _flexM.DataTable.Rows.Count > 0)
                            e.HelpParam.P09_CD_PLANT = D.GetString(_flexM.DataTable.Rows[0]["CD_PLANT"]);
                        else
                        {
                            ShowMessage("조회 후 작업해 주시기 바랍니다.");
                            e.QueryCancel= true;
                        }
                        break;
                    case Duzon.Common.Forms.Help.HelpID.P_PU_EJTP_SUB:		// 발주유형 도움창					
                        e.HelpParam.P61_CODE1 = "001|005|";
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
        
        #region ♣ 그리드 이벤트

        #region -> 그리드 행변경 이벤트(_flexM_AfterRowChange)

        private void _flexM_AfterRowChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
            try
            {
                if (!_flexM.IsBindingEnd || !_flexM.HasNormalRow) return;

                DataTable dt = null;

                string Filter = "NO_RCV= '" + _flexM["NO_RCV"].ToString() + "'";

                if (_flexM.DetailQueryNeed)
                    dt = _biz.Search_Detail(_flexM["NO_RCV"].ToString(), D.GetString(bp_프로젝트.CodeValue));

                _flexD.BindingAdd(dt, Filter);

                _flexD.SetDummyColumn("S");
                _flexD.SetDummyColumn("CD_SL");
                _flexD.SetDummyColumn("NM_SL");

                _flexM.DetailQueryNeed = false;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 그리드 도움창 호출전 세팅 이벤트(_flexD_BeforeCodeHelp)

        private void _flexD_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                switch (_flexD.Cols[e.Col].Name)
                {
                    case "NM_SL":
                        e.Parameter.P09_CD_PLANT = D.GetString(_flexD["CD_PLANT"]);
                        if (m_sEnv_WH.Equals("100"))
                            e.Parameter.P20_CD_WH = D.GetString(_flexD["CD_WH"].ToString());

                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 그리드 도움창 호출후 변경 이벤트(_flexD_AfterCodeHelp)

        private void _flexD_AfterCodeHelp(object sender, AfterCodeHelpEventArgs e)
        {
            try
            {
                FlexGrid flex = sender as FlexGrid;
                HelpReturn helpReturn = e.Result;


                switch (_flexD.Cols[e.Col].Name)
                {
                    case "NM_SL":
                        if (m_sEnv_WH.Equals("100"))
                        {
                            foreach (DataRow dr in helpReturn.Rows)
                            {
                                _flexD["CD_WH"] = dr["CD_WH"];
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

        #region -> 그리드 변경시작시 발생이벤트(_flex_StartEdit)

        private void _flex_StartEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            try
            {
                FlexGrid flex = sender as FlexGrid;
                if (flex == null) return;

                switch (flex.Name)
                {
                    case "_flexM":
                        DataRow[] dr;
                        dr = _flexD.DataTable.Select("NO_RCV = '" + _flexM["NO_RCV"].ToString() + "'"); //, "", DataViewRowState.CurrentRows);

                        if(_flexM["S"].ToString() == "N") //클릭하는 순간은 N이므로
                        {
                            int i = 0;
                            for(i = _flexD.Rows.Fixed;i <= dr.Length + 1;i++)
                                _flexD.SetCellCheck(i, 1, CheckEnum.Checked);

                            _flexD.Row = _flexD.Rows.Fixed;
                            _flexD.Col = _flexD.Cols.Fixed;
                            _flexD.Focus();
                        }
                        else
                        {
                            int i = 0;
                            for(i = _flexD.Rows.Fixed;i <= dr.Length + 1;i++)
                                _flexD.SetCellCheck(i, 1, CheckEnum.Unchecked);

                            _flexD.Row = _flexD.Rows.Fixed;
                            _flexD.Col = _flexD.Cols.Fixed;
                            _flexD.Focus();
                        }
                        break;
                    case "_flexD":
                        if(_flexD.Cols[e.Col].Name == "S")
                        {
                            if(_flexM["S"].ToString() == "N")
                            {
                                e.Cancel = true;
                                return;
                            }
                        }
                        break;
                }
                _flexD.IsDataChanged = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 그리드 ValidateEdit 이벤트(_flexD_ValidateEdit)

        private void _flexD_ValidateEdit(object sender, C1.Win.C1FlexGrid.ValidateEditEventArgs e)
        {
            try
            {
                FlexGrid _flex = sender as FlexGrid;
                if (_flex == null) return;

                decimal 입고량 = 0;             // 입고량				
                decimal 환율 = 0;               // 환율	
                decimal 단가 = 0;

                decimal 양품수량 = 0; 

                decimal P_AM_EX = 0, P_AM = 0, P_VAT = 0 ;

                bool 부가세포함 = D.GetString(_flex[e.Row, "TP_UM_TAX"]) == "001" ? true : false;
                string 과세구분 = D.GetString(_flex[e.Row, "FG_TAX"]);
                decimal 부가세율  = D.GetDecimal(_flex[e.Row, "RATE_VAT"]);

                if (sender != null) //양품적용 버튼을 클릭했을 때 sender 가 null로 넘어온다.
                {
                    string oldValue = _flex.GetData(e.Row, e.Col).ToString();
                    string newValue = _flex.EditData;

                    if (oldValue != newValue)
                    {
                        switch (_flexD.Cols[e.Col].Name)
                        {
                            case "QT_REQ_RCV": //양품입고량
                                if (BASIC.GetMAEXC("구매입고등록_입고의뢰수량-입고수량_초과허용여부") != "100")
                                {
                                    if (_flexD.CDecimal(newValue) > _flexD.CDecimal(_flexD["QT_MM"]))
                                    {
                                        ShowMessage(공통메세지._은_보다작거나같아야합니다, _flexD.Cols["QT_REQ_RCV"].Caption, _flexD.Cols["QT_MM"].Caption);
                                        _flexD["QT_REQ_RCV"] = oldValue;
                                        return;
                                    }
                                }
                                
                                if (_flexD.CDecimal(newValue) <= 0)
                                {
                                    ShowMessage(공통메세지._은_보다커야합니다, _flexD.Cols["QT_REQ_RCV"].Caption, "0");
                                    _flexD["QT_REQ_RCV"] = oldValue;
                                    return;
                                }

                                if (_flexD.CDecimal(_flexD["QT_MM"]) != _flexD.CDecimal(_flexD["QT_REQ_RCV"]))
                                {
                                    입고량 = _flexD.CDecimal(newValue);    // 입고량
                                    환율   = _flexD.CDecimal(_flexD["RT_EXCH"]);  //단가 = _flexD.CDecimal(_flexD["UM_EX_PO"]);
                                    calcAM(e.Row,D.GetDecimal(_flexD["TOT_WEIGHT"]));
                                    단가   = Unit.외화단가(DataDictionaryTypes.PU, _flexD.CDecimal(_flexD["UM_EX_PO"]));
                                    // 부가세율 = 부가세율구하기(_flexD["FG_TAX"].ToString());
                                    양품수량 = _flexD.CDecimal(newValue) * D.GetDecimal(_flexD["RATE_EXCHG"]);
                                    
                                    //P_AM_EX = 입고량 * 단가;
                                    //P_AM = P_AM_EX * 환율; 
                                    //P_AM = Unit.원화금액(DataDictionaryTypes.PU, System.Math.Round(입고량 * 단가 * 환율));
                                    Calc.GetAmt(입고량, 단가, 환율, 과세구분, 부가세율, 모듈.PUR, 부가세포함, out P_AM_EX, out P_AM, out P_VAT);
                                     
                                    _flexD["QT_REQ_RCV"]  = newValue;
                                    _flexD["QT_GOOD_INV"] = 양품수량;
                                    _flexD["QT_REQ_U"] = 양품수량;

                                    //if (!부가세포함)
                                    //{  
                                    //    _flexD["UM"]    = Unit.원화단가(DataDictionaryTypes.PU, P_AM / 양품수량);
                                    //    _flexD["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, P_AM_EX / 양품수량);
                                    //} 
                                    _flexD["AM_EX"] = P_AM_EX;
                                    _flexD["AM"]    = P_AM ;
                                    _flexD["VAT"]   = P_VAT;  //Unit.원화금액(DataDictionaryTypes.PU, P_AM * 부가세율);
                                    //_flexD["AM_TOTAL"] = Calc.합계금액(P_AM, P_VAT);    
                                }
                                else
                                {
                                    양품수량 = _flexD.CDecimal(newValue) * _flexD.CDecimal(_flexD["RATE_EXCHG"]);

                                    _flexD["AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flexD["OLD_AM"]));
                                    _flexD["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(_flexD["OLD_AM_EX"]));
                                    _flexD["VAT"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flexD["OLD_VAT"]));
                                     
                                    _flexD["QT_REQ_RCV"] = newValue;
                                    _flexD["QT_GOOD_INV"] = 양품수량;
                                    //P_AM = _flexD.CDecimal(_flexD["AM"]);
                                    //P_AM_EX = _flexD.CDecimal(_flexD["AM_EX"]);
                                    //_flexD["UM"]    = Unit.원화단가(DataDictionaryTypes.PU, P_AM / 양품수량); 
                                    //_flexD["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, P_AM_EX / 양품수량);
                                    
                                }
                                break;

                            case "CD_WH":
                                if (oldValue != newValue)
                                {
                                    _flexD[e.Row, "CD_SL"] = string.Empty;
                                    _flexD[e.Row, "NM_SL"] = string.Empty;
                                }
                                break;

                            case "UM_WEIGHT":
                                calcAM(e.Row, D.GetDecimal(_flex[e.Row, "TOT_WEIGHT"]));
                                단가 = Unit.외화단가(DataDictionaryTypes.PU, _flexD.CDecimal(_flexD["UM_EX_PO"]));
                                // 부가세율 = 부가세율구하기(_flexD["FG_TAX"].ToString());
                                양품수량 = _flexD.CDecimal(_flexD["QT_REQ_RCV"]) * D.GetDecimal(_flexD["RATE_EXCHG"]);

                                //P_AM_EX = 입고량 * 단가;
                                //P_AM = P_AM_EX * 환율; 
                                //P_AM = Unit.원화금액(DataDictionaryTypes.PU, System.Math.Round(입고량 * 단가 * 환율));
                                Calc.GetAmt(입고량, 단가, 환율, 과세구분, 부가세율, 모듈.PUR, 부가세포함, out P_AM_EX, out P_AM, out P_VAT);

                                _flexD["QT_REQ_RCV"] = newValue;
                                _flexD["QT_GOOD_INV"] = 양품수량;

                                //if (!부가세포함)
                                //{  
                                //    _flexD["UM"]    = Unit.원화단가(DataDictionaryTypes.PU, P_AM / 양품수량);
                                //    _flexD["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, P_AM_EX / 양품수량);
                                //} 
                                _flexD["AM_EX"] = P_AM_EX;
                                _flexD["AM"] = P_AM;
                                _flexD["VAT"] = P_VAT;  //Unit.원화금액(DataDictionaryTypes.PU, P_AM * 부가세율);
                                break;
                            case "TOT_WEIGHT":
                                calcAM(e.Row, D.GetDecimal(newValue));
                                단가 = Unit.외화단가(DataDictionaryTypes.PU, _flexD.CDecimal(_flexD["UM_EX_PO"]));
                                // 부가세율 = 부가세율구하기(_flexD["FG_TAX"].ToString());
                                양품수량 = _flexD.CDecimal(_flexD["QT_REQ_RCV"]) * D.GetDecimal(_flexD["RATE_EXCHG"]);

                                //P_AM_EX = 입고량 * 단가;
                                //P_AM = P_AM_EX * 환율; 
                                //P_AM = Unit.원화금액(DataDictionaryTypes.PU, System.Math.Round(입고량 * 단가 * 환율));
                                Calc.GetAmt(입고량, 단가, 환율, 과세구분, 부가세율, 모듈.PUR, 부가세포함, out P_AM_EX, out P_AM, out P_VAT);

                                _flexD["QT_REQ_RCV"] = newValue;
                                _flexD["QT_GOOD_INV"] = 양품수량;

                                //if (!부가세포함)
                                //{  
                                //    _flexD["UM"]    = Unit.원화단가(DataDictionaryTypes.PU, P_AM / 양품수량);
                                //    _flexD["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, P_AM_EX / 양품수량);
                                //} 
                                _flexD["AM_EX"] = P_AM_EX;
                                _flexD["AM"] = P_AM;
                                _flexD["VAT"] = P_VAT;  //Unit.원화금액(DataDictionaryTypes.PU, P_AM * 부가세율);
                                break;

                        }
                    }
                }
                else
                {
                    //양품적용 버튼을 클릭했을 때 이 구문을 타게 한다.

                    //양품수량 = _flexD.CDecimal(_flexD["QT_GOOD_INV"].ToString());

                    //P_AM = _flexD.CDecimal(_flexD["AM"].ToString());
                    //P_AM_EX = _flexD.CDecimal(_flexD["AM_EX"].ToString());

                    //_flexD["UM"] = P_AM / 양품수량;
                    //_flexD["UM"] = Unit.원화단가(DataDictionaryTypes.PU, P_AM / 양품수량);
                    //_flexD["UM_EX"] = P_AM_EX / 양품수량;
                    //_flexD["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, P_AM_EX / 양품수량);
                }

                Page_DataChanged(null, null);

                if (!ToolBarSaveButtonEnabled)
                    ToolBarSaveButtonEnabled = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 그리드 수정후 발생 이벤트(_flexM_AfterEdit)

        void _flexM_AfterEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (_flexM["S"].ToString() == "Y") //클릭하는 순간은 N이므로
                {
                    for (int i = _flexD.Rows.Fixed; i < _flexD.Rows.Count; i++)
                        _flexD.SetCellCheck(i, 1, CheckEnum.Checked);
                }
                else
                {
                    for (int i = _flexD.Rows.Fixed; i < _flexD.Rows.Count; i++)
                        _flexD.SetCellCheck(i, 1, CheckEnum.Unchecked);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }

        }

        #endregion

        #region -> 그리드 헤더클릭 이벤트(_flexM_CheckHeaderClick)

        void _flexM_CheckHeaderClick(object sender, EventArgs e)
        {
            /* 유의 : 클릭하자마자 헤더 그리드는 선택 상태가 변경된다(ex) Checked->UnChecked  */

            if (_flexM == null || _flexD == null) return;

            if (!_flexM.HasNormalRow || !_flexD.HasNormalRow) return;

            _flexM.Row = 1; // 맨처음row에 자동위치하도록 해야 틀어지지 않는다.
            DataRow[] dr;

            if (_flexM[_flexM.Rows.Fixed, "S"].ToString() == "N" && _flexD[_flexD.Rows.Fixed, "S"].ToString() == "Y") // 만일 체크된 row들을 해제 할 경우
            {
                for (int k = _flexM.Rows.Fixed; k <= _flexM.Rows.Count - 1; k++)
                {
                    _flexM.Row = k;  // Row가 순차적으로 변경되면서 RowChange() 이벤트를 자동 실행하게 유도한다.

                    dr = _flexD.DataTable.Select("NO_RCV = '" + _flexM[k, "NO_RCV"].ToString() + "'", "", DataViewRowState.CurrentRows);  // 라인 자동선택위함.

                    for (int j = _flexD.Rows.Fixed; j <= dr.Length + 1; j++)
                        _flexD.SetCellCheck(j, 1, CheckEnum.Unchecked);
                }
                return;
            }

            for (int i = _flexM.Rows.Fixed; i <= _flexM.Rows.Count - 1; i++)
            {
                _flexM.Row = i;

                dr = _flexD.DataTable.Select("NO_RCV = '" + _flexM[i, "NO_RCV"].ToString() + "'", "", DataViewRowState.CurrentRows);

                for (int j = _flexD.Rows.Fixed; j <= dr.Length + 1; j++)
                    _flexD.SetCellCheck(j, 1, CheckEnum.Checked);
            }
        }

        #endregion



        #region -> 그리드  메모기능 체크펜기능
        void _flexD_CellContentChanged(object sender, CellContentEventArgs e)
        {
            try
            {
                _biz.SaveContent(e.ContentType, e.CommandType, D.GetString(_flexD[e.Row, "NO_RCV"]), D.GetDecimal(_flexD[e.Row, "NO_LINE"]), e.SettingValue);

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #endregion

        #region ♣ 기타 이벤트

        #warning 부가세구하는 방법 코드관리에서 가져와야 할것 같다.(담당자는 확인후 이 메세지를 지우세요!)

        #region -> 부가세율구하기

        private decimal 부가세율구하기(string ps_taxp)
        {
            decimal TAXP = 0;

            //switch (ps_taxp.Trim())
            //{
            //    case "21":
            //        TAXP = 0.1M;
            //        break;
            //    case "22":
            //        TAXP = 0.1M;
            //        break;
            //    case "23":
            //        TAXP = 0;
            //        break;
            //    case "24":
            //        TAXP = 0.1M;
            //        break;
            //    case "25":
            //        TAXP = 0;
            //        break;
            //    case "26":
            //        TAXP = 0;
            //        break;
            //    case "27":
            //        TAXP = 2/102;
            //        break;
            //    case "28":
            //        TAXP = 2/102;
            //        break;
            //    case "29":
            //        TAXP = 3/103;
            //        break;
            //    case "30":
            //        TAXP = 3/103;
            //        break;

            //}

            TAXP = BASIC.GetTPVAT(ps_taxp)*0.01M;

            return TAXP;
        }

        #endregion

        #region -> 도움창 분기 --> 주석처리

        //private void _flex_CodeHelp(object sender, Dass.FlexGrid.CodeHelpEventArgs e)
        //{
        //    try
        //    {
        //        HelpReturn helpReturn = null;
        //        HelpParam param = null;

        //        switch (_flexD.Cols[e.Col].Name)
        //        {
        //            case "NM_SL":
        //                if (e.Source == CodeHelpEnum.CodeSearch && e.EditValue == "")
        //                {
        //                    _flexD["CD_SL"] = "";
        //                    _flexD["NM_SL"] = "";
        //                }
        //                else
        //                {
        //                    param = new Duzon.Common.Forms.Help.HelpParam(
        //                        Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB, MainFrameInterface);
        //                    param.P01_CD_COMPANY = LoginInfo.CompanyCode;
        //                    param.P09_CD_PLANT = _flexD["CD_PLANT"].ToString();
        //                    param.ResultMode = ResultMode.FastMode;
        //                    if (e.Source == CodeHelpEnum.CodeSearch)
        //                        param.P92_DETAIL_SEARCH_CODE = e.EditValue;

        //                    if (e.Source == CodeHelpEnum.CodeSearch) helpReturn = (HelpReturn)CodeSearch(param);
        //                    else helpReturn = (HelpReturn)ShowHelp(param);

        //                    if (helpReturn.DialogResult == System.Windows.Forms.DialogResult.OK)
        //                    {
        //                        _flexD["CD_SL"] = helpReturn.CodeValue;
        //                        _flexD["NM_SL"] = helpReturn.CodeName;

        //                    }
        //                }
        //                break;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MsgEnd(ex);
        //    }
        //}

        #endregion

        #region ♣ 컨트롤 이벤트

        #region -> 날짜 Control ValidateCheck 이벤트(OnControl_CalendarClosed)

        private void OnControl_CalendarClosed(object sender, EventArgs e)
        {
            try
            {
                DatePicker dp = sender as DatePicker;
                if (dp == null) return;

                if (!dp.IsValidated)
                {
                    ShowMessage(공통메세지.입력형식이올바르지않습니다);
                    dp.Focus();
                    return;
                }

                decimal dStart, dEnd;

                if (!decimal.TryParse(periodPicker1.StartDateToString, out dStart)) return;
                if (!decimal.TryParse(periodPicker1.EndDateToString, out dEnd)) return;

                if (dStart > dEnd)
                {
                    ShowMessage(공통메세지.시작일자보다종료일자가클수없습니다);
                    dp.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> (창고) 적용 버튼 클릭 이벤트(btn_apply_Click)

        private void btn_apply_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_flexM.DataTable == null || _flexM.DataTable.Rows.Count == 0) return;

                DataRow[] drH = _flexM.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                if (drH == null || drH.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                foreach (DataRow rowH in drH)
                {
                    DataRow[] ldrchk = _flexD.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                    _flexD.Redraw = false;
                    foreach (DataRow row in ldrchk)
                    {
                        row["CD_SL"] = tb_nm_sl2.CodeValue;
                        row["NM_SL"] = tb_nm_sl2.CodeName;
                    }
                }
                ShowMessage(공통메세지._작업을완료하였습니다, btn_apply.Text);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                _flexD.Redraw = true;
            }
        }

        #endregion

        #region -> 양품적용 버튼 클릭 이벤트(btn_apply_good_Click)

        private void btn_apply_good_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (!_flexM.HasNormalRow) return;
                if (!_flexD.HasNormalRow) return;

                _flexD.Redraw = false;

                DataRow[] drH = _flexM.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                if (drH == null || drH.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                foreach (DataRow rowH in drH)
                {
                    DataRow[] ldrchk = _flexD.DataTable.Select("S = 'Y' AND NO_RCV = '" + rowH["NO_RCV"].ToString() + "'", "", DataViewRowState.CurrentRows);

                    foreach (DataRow row in ldrchk)
                    {
                        row["QT_REQ_RCV"] = _flexD.CDecimal(row["QT_MM"]);      // 입고량					
                        row["QT_GOOD_INV"] = _flexD.CDecimal(row["QT_REQ_U"]);
                        row["QT_REJECT_REQ_MM"] = 0;                            // 입고량					
                        row["QT_REJECT_REQ"] = 0;
                        row["VAT"] = _flexD.CDecimal(row["OLD_VAT"]);
                        row["AM"] = _flexD.CDecimal(row["OLD_AM"]);
                        row["AM_EX"] = _flexD.CDecimal(row["OLD_AM_EX"]);
                        row["TP_UM_TAX"] =D.GetString(row["TP_UM_TAX"]);

                    }
                }

                _flexD_ValidateEdit(null, null);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                _flexD.Redraw = true;
            }
        }

        #endregion

        #region -> 처리 라디오 버튼 클릭 이벤트(rb_do_pc_CheckedChanged)

        private void rb_do_pc_CheckedChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (rb_do_pc.Checked == true)
                {
                    object[] args = new Object[10];

                    args[0] = periodPicker1.StartDateToString;
                    args[1] = periodPicker1.EndDateToString;

                    args[2] = tb_nm_partner.CodeValue;
                    args[3] = tb_nm_partner.CodeName;

                    args[4] = D.GetString(cb_cd_plant.SelectedValue);
                    args[5] = "";

                    args[6] = "";
                    args[7] = "";

                    args[8] = tb_no_emp.CodeValue;
                    args[9] = tb_no_emp.CodeName;

                    // Main 이 살아 있는지 확인한후 살아 있으면 저장을 실행하고 죽어 있으면 그냥 리턴시켜버린다.
                    if (MainFrameInterface.IsExistPage("P_PU_GRM_REG", false))
                    {
                        //- 특정 페이지 닫기
                        UnLoadPage("P_PU_GRM_REG", false);
                    }
                    bool isComplete = LoadPageFrom("P_PU_GRM_REG", "구매입고관리", Grant, args);

                    if (!isComplete)
                        ShowMessage(공통메세지.작업을정상적으로처리하지못했습니다);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        private void _flexM_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region -> 중량계산
       


        void calcAM(int row, decimal TOT_WEIGHT)
        {
            if (bStandard)
            {

                if (Global.MainFrame.ServerKey == "SINJINSM" || Global.MainFrame.ServerKey == "SQL_108" || Global.MainFrame.ServerKey == "DZSQL")
                {

                    if (D.GetDecimal(_flexD[row, "UM_WEIGHT"]) != 0)
                    {
                        // 단위 환산량
                        decimal ldb_rateExchg = _flexD.CDecimal(_flexD[row, "RT_PO"]) == 0 ? 1 : _flexD.CDecimal(_flexD[row, "RT_PO"]);

                        _flexD[row, "AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, Math.Round(TOT_WEIGHT * D.GetDecimal(_flexD[row, "UM_WEIGHT"])));
                        if (D.GetDecimal(_flexD[row, "QT_REQ_RCV"]) != 0)
                            _flexD[row, "UM_EX"] = UDecimal.Getdivision(D.GetDecimal(_flexD[row, "AM_EX"]) / D.GetDecimal(_flexD[row, "QT_REQ_RCV"]), ldb_rateExchg);
                        else
                            _flexD[row, "UM_EX"] = 0;
                        _flexD[row, "AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(_flexD[row, "AM_EX"]) * D.GetDecimal(_flexD[row,"RT_EXCH"]));


                        _flexD[row, "UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, _flexD.CDecimal(_flexD[row, "UM_EX"]) * ldb_rateExchg);
                    }
                }
            }

        }

        #endregion

        #endregion
    }
}

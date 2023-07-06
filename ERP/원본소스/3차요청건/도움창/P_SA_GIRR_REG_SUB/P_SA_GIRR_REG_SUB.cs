using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.SA.Common;

namespace sale
{
    // **************************************
    // 작   성   자 : 허성철
    // 재 작  성 일 : 2007-03-04
    // 모   듈   명 : 영업
    // 시 스  템 명 : 영업관리
    // 서브시스템명 : 출하의뢰관리
    // 페 이 지  명 : 출하반품의뢰등록 -> 반품의뢰(출하적용) 도움창
    // 프로젝트  명 : P_SA_GIRR_REG_SUB
    // **************************************
    public partial class P_SA_GIRR_REG_SUB : Duzon.Common.Forms.CommonDialog
    {
        #region ★ 멤버필드

        bool is수주등록출하적용 = false;
        bool is프로젝트사용 = false;
        bool is무환출고조회 = false;
        private 수주관리.Config 수주Config = new 수주관리.Config();
        private P_SA_GIRR_REG_SUB_BIZ _biz = new P_SA_GIRR_REG_SUB_BIZ();
        DataTable _dt = new DataTable();

        #endregion

        #region ★ 초기화

		#region -> 생성자

        public P_SA_GIRR_REG_SUB(string 공장코드, string 공장명, string 거래처코드, string 거래처명, string 과세구분코드, string 과세구분명)
		{
            try
            {
                InitializeComponent();

                m_cdePlant.CodeValue = 공장코드;
                m_cdePlant.CodeName = 공장명;
                m_cde거래처.CodeValue = 거래처코드;
                m_cde거래처.CodeName = 거래처명;
                m_cdeTpvat.CodeValue = 과세구분코드;
                m_cdeTpvat.CodeName = 과세구분명;
                bpCdsl.MainFrame = Global.MainFrame;
                bpTpGi.MainFrame = Global.MainFrame;
                bpNoEmp.MainFrame = Global.MainFrame;
                bp수주담당자.MainFrame = Global.MainFrame;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #region -> InitLoad

        protected override void InitLoad()
        {
            try
            {
                base.InitLoad();
                InitGridH();
                InitGridL();
                _flexH.DetailGrids = new FlexGrid[] { _flexL };
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #region -> InitGridH

        private void InitGridH()
        {
            _flexH.BeginSetting(1, 1, false);
            _flexH.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flexH.SetCol("NO_IO", "수불번호", 100);
            _flexH.SetCol("DT_IO", "수불일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexH.SetCol("NM_KOR", "담당자", 100);
            _flexH.SetCol("DC_RMK", "비고", 185);
            _flexH.ExtendLastCol = true;
            _flexH.EnabledHeaderCheck = false;
            _flexH.SettingVersion = "1.0.0.1";
            _flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flexH.AfterDataRefresh += new ListChangedEventHandler(_flex_AfterDataRefresh);
            _flexH.StartEdit += new RowColEventHandler(_flexH_StartEdit);
            _flexH.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
        }

        #endregion

        #region -> InitGridL

        private void InitGridL()
        {
            _flexL.BeginSetting(1, 1, false);
            _flexL.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flexL.SetCol("CD_ITEM", "품목코드", 100);
            _flexL.SetCol("NM_ITEM", "품목명", 120);
            _flexL.SetCol("STND_ITEM", "규격", 65);
            _flexL.SetCol("UNIT_SO", "단위", 65);
            _flexL.SetCol("QT_GIR", "수량", 65, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("UM_EX", "단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flexL.SetCol("AM_EX", "금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flexL.SetCol("AM", "원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flexL.SetCol("QT_IO", "재고수량", 65, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("NM_SL", "창고", 100);
            _flexL.SetCol("NM_SALEGRP", "영업그룹", 100);
            _flexL.SetCol("NM_PROJECT", "프로젝트명", 100);
            _flexL.SetCol("NO_PSO_MGMT", "수주번호", 100);
            _flexL.SetCol("GI_PARTNER", "납품처코드", 100);
            _flexL.SetCol("LN_PARTNER", "납품처명", 120);

            if (수주Config.부가세포함단가사용())
            {
                _flexL.SetCol("TP_UM_TAX", "부가세여부", 90, false);
                _flexL.SetCol("UMVAT_GIR", "부가세포함단가", 100, false, typeof(decimal), FormatTpType.UNIT_COST);
            }

            _flexL.SettingVersion = "1.0.0.1";
            _flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        #endregion

        #region -> InitPaint

        protected override void InitPaint()
        {
            try
            {
                base.InitPaint();

                DataTable dt = MA.GetCodeUser(new string[] { "Y", "N" }, new string[] { "유환", "무환" }, true);

                SetControl str = new SetControl();
                str.SetCombobox(cbo유무환구분, dt);

                m_mskStart.Mask = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT);
                m_mskStart.ToDayDate = Global.MainFrame.GetDateTimeToday();
                m_mskStart.Text = Global.MainFrame.GetStringFirstDayInMonth;

                m_mskEnd.Mask = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT);
                m_mskEnd.ToDayDate = Global.MainFrame.GetDateTimeToday();
                m_mskEnd.Text = Global.MainFrame.GetStringToday;

                bpc수주번호.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(bpc수주번호_QueryBefore);

                if (is무환출고조회 == true)
                {
                    cbo유무환구분.SelectedValue = "N";
                    cbo유무환구분.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region ★ 화면내버튼 클릭

        #region -> 조회조건체크

        private bool SearchCondition()
        {
            //출하일자FROM
            if (m_mskStart.Text == "")
            {
                //수주일자 은(는) 필수 입력입니다.
                Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, m_lblDtIo.Text);
                m_mskStart.Focus();
                return false;
            }

            //출하일자TO
            if (m_mskEnd.Text == "")
            {
                //수주일자 은(는) 필수 입력입니다.
                Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, m_lblDtIo.Text);
                m_mskEnd.Focus();
                return false;
            }

            return true;
        }

        #endregion

        #region -> 조회버튼클릭

        private void OnSearchButtonClicked(object sender, System.EventArgs e)
        {
            try
            {
                if (!SearchCondition()) return;

                _flexH.Binding = _biz.Search (  m_mskStart.Text, 
                                                m_mskEnd.Text,
                                                m_cdePlant.CodeValue,
                                                bpSalegrp.CodeValue, 
                                                m_cde거래처.CodeValue,
                                                m_cdeTpvat.CodeValue,
                                                bpCdsl.CodeValue,
                                                bpTpGi.CodeValue,
                                                bpNoEmp.CodeValue,
                                                bp_PJT.CodeValue,
                                                bpc수주번호.QueryWhereIn_Pipe,
                                                is수주등록출하적용 == true ? "Y" : "N",
                                                bp수주담당자.CodeValue,
                                                D.GetString(cbo유무환구분.SelectedValue),
                                                bp품목.CodeValue
                                                );

                if (!_flexH.HasNormalRow)
                    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #region -> 적용버튼클릭

        private void OnApply(object sender, System.EventArgs e)
        {
            try
            {
                if (!_flexL.HasNormalRow) return;

                DataRow[] drs = _flexL.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                if (drs == null || drs.Length == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }


                if (수주Config.부가세포함단가사용())
                {
                    DataRow[] drs_Y = _flexL.DataTable.Select("S = 'Y' AND TP_UM_TAX = 'Y'", "", DataViewRowState.CurrentRows);
                    DataRow[] drs_N = _flexL.DataTable.Select("S = 'Y' AND TP_UM_TAX = 'N'", "", DataViewRowState.CurrentRows);

                    if (drs_Y.Length > 0 && drs_N.Length > 0)
                    {
                        Global.MainFrame.ShowMessage("부가세단가포함을 사용하는 건과 사용하지 않는건이 같이 선택 되어있습니다.");
                        return;
                    }
                }

                if (is프로젝트사용)
                {
                    foreach (DataRow dr in drs)
                    {
                        if (D.GetString(dr["NO_PROJECT"]) == string.Empty)
                        {
                            Global.MainFrame.ShowMessage("프로젝트가 필수이나, 선택된 자료중에 프로젝트가 없는 건이 있습니다.");
                            return;
                        }
                    }
                }

                _dt = _flexL.DataTable.Clone();
                foreach (DataRow row in drs)
                    _dt.ImportRow(row);

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region ★ 그리드 이벤트

        #region -> _flex_AfterDataRefresh

        void _flex_AfterDataRefresh(object sender, ListChangedEventArgs e)
        {
            try
            {
                if (!_flexH.HasNormalRow)
                    m_btnApply.Enabled = false;
                else
                    m_btnApply.Enabled = true;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #region -> _flexH_StartEdit

        void _flexH_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                DataRow[] dr = _flexL.DataTable.Select("NO_IO = '" + _flexH[e.Row, "NO_IO"].ToString() + "'", "", DataViewRowState.CurrentRows);

                if (_flexH[e.Row, "S"].ToString() == "N") //클릭하는 순간은 N이므로
                {
                    for (int i = _flexL.Rows.Fixed; i <= dr.Length; i++)
                        _flexL.SetCellCheck(i, 1, CheckEnum.Checked);
                }
                else
                {
                    for (int i = _flexL.Rows.Fixed; i <= dr.Length; i++)
                        _flexL.SetCellCheck(i, 1, CheckEnum.Unchecked);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #region -> _flex_AfterRowChange

        private void _flex_AfterRowChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
            try
            {
                DataTable dt = null;

                string Key = _flexH[e.NewRange.r1, "NO_IO"].ToString();
                string Filter = "NO_IO = '" + Key + "'";

                if (_flexH.DetailQueryNeed)
                {
                    dt = _biz.SearchDetail (    Key,
                                                bpSalegrp.CodeValue,
                                                m_cde거래처.CodeValue,
                                                m_cdeTpvat.CodeValue,
                                                bpCdsl.CodeValue,
                                                bpTpGi.CodeValue,
                                                bpNoEmp.CodeValue,
                                                bp_PJT.CodeValue,
                                                bpc수주번호.QueryWhereIn_Pipe,
                                                is수주등록출하적용 == true ? "Y" : "N",
                                                bp수주담당자.CodeValue,
                                                D.GetString(cbo유무환구분.SelectedValue), 
                                                bp품목.CodeValue
                                                );
                }
                _flexL.BindingAdd(dt, Filter);
                _flexH.DetailQueryNeed = false;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region ★ 기타 이벤트

        #region -> Control_QueryBefore

        private void Control_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                switch (e.HelpID)
                {
                    case Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB:
                        e.HelpParam.P09_CD_PLANT = m_cdePlant.CodeValue;
                        break;
                    case Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB:
                        e.HelpParam.P09_CD_PLANT = m_cdePlant.CodeValue;
                        break;
                    case Duzon.Common.Forms.Help.HelpID.P_PU_EJTP_SUB:
                        e.HelpParam.P61_CODE1 = "010|041|042|";
                        break;
                    case Duzon.Common.Forms.Help.HelpID.P_USER:
                        if (e.ControlName == "bp_PJT")
                        {
                            e.HelpParam.P41_CD_FIELD1 = "프로젝트";  //도움창 이름 찍어줄 값
                            e.HelpParam.P14_CD_PARTNER = m_cde거래처.CodeValue;
                            e.HelpParam.P63_CODE3 = m_cde거래처.CodeName;
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

        #region -> Control_QueryAfter

        private void Control_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                switch (e.HelpID)
                {
                    case Duzon.Common.Forms.Help.HelpID.P_USER:
                        if (e.ControlName == "bp_PJT")
                        {
                            m_cde거래처.CodeValue = e.HelpReturn.Rows[0]["CD_PARTNER"].ToString();
                            m_cde거래처.CodeName = e.HelpReturn.Rows[0]["LN_PARTNER"].ToString();
                            bpSalegrp.CodeValue = e.HelpReturn.Rows[0]["CD_SALEGRP"].ToString();
                            bpSalegrp.CodeName = e.HelpReturn.Rows[0]["NM_SALEGRP"].ToString();
                            bpSalegrp.Enabled = false;
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

        #region -> Control_CodeChanged
        private void Control_CodeChanged(object sender, EventArgs e)
        {
            try
            {
                if (((Control)sender).Name == "bp_PJT")
                {
                    if (bp_PJT.CodeValue == string.Empty)
                    {
                        bpSalegrp.CodeValue = string.Empty;
                        bpSalegrp.CodeName = string.Empty;
                        bpSalegrp.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region -> bpc수주번호_QueryBefore

        void bpc수주번호_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                if(Checker.IsEmpty(m_cde거래처, Global.MainFrame.DD("거래처")))
                {
                    e.QueryCancel = true;
                    return;
                }

                bpc수주번호.UserParams = "수주번호도움창;H_SA_SO_SUB";
                e.HelpParam.P14_CD_PARTNER = m_cde거래처.CodeValue;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region ★ 리턴데이타

        public DataTable 출하테이블
        {
            get { return _dt; }
        }

        #endregion

        public bool Set수주등록출하적용 { set { is수주등록출하적용 = value; } }
        public bool Set프로젝트사용 { set { is프로젝트사용 = value; } }
        public bool Set무환출고조회 { set { is무환출고조회 = value; } }

    }
}
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Duzon.Common.Controls;
using Duzon.Common.Forms;

using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Util;
using Duzon.ERPU;

namespace sale
{
    // **************************************
    // 작   성   자 : 허성철
    // 재 작  성 일 : 2006-10-09
    // 모   듈   명 : 영업
    // 시 스  템 명 : 영업관리
    // 서브시스템명 : 출하의뢰관리
    // 페 이 지  명 : 고객납품의뢰등록 -> 납품의뢰(수주적용) 도움창
    // 프로젝트  명 : P_SA_SO_SUB
    // **************************************
    public partial class P_SA_SO_SUB : Duzon.Common.Forms.CommonDialog
    {
        #region ★ 멤버필드

        private P_SA_SO_SUB_BIZ _biz = new P_SA_SO_SUB_BIZ();
        DataTable _dt = new DataTable();

        string so_Price = string.Empty;         //영업그룹을 선택시 판매단가통제유무(SO_PRICE)도 가져온다.

        #endregion

        #region ★ 초기화

		#region -> 생성자
        //bpNm_Partner.CodeValue, bpNm_Partner.CodeName, m_cboPlantGir.SelectedValue.ToString(), 
        //m_cboPlantGir.Text, m_cboTpBusi.SelectedValue.ToString(), m_cboTpBusi.Text, txt_NoProject.Text 

		public P_SA_SO_SUB(string 거래처코드,   string 거래처명,   string 출하공장코드, string 출하공장명, 
                           string 거래구분코드, string 거래구분명, string 프로젝트코드, string 프로젝트명)
		{
            try
            {
                InitializeComponent();

                bpSalegrp.MainFrame = Global.MainFrame;
                bpNm_Sl.MainFrame = Global.MainFrame;

                m_cdePlant.CodeValue = 출하공장코드;
                m_cdePlant.CodeName = 출하공장명;
                m_cdeTpBusi.CodeValue = 거래구분코드;
                m_cdeTpBusi.CodeName = 거래구분명;

                //항상 프로젝트 명을 먼저 셋팅하고 프로젝트 코드를 셋팅하라(불문률)~ 메롱~
                //AND 그라고~
                //항상 프로젝트 코드를 먼저 셋팅하고 거래처를 셋팅하라(불문률)~ 메롱~
                bp_Project.Text = 프로젝트명;
                txt_NoProject.Text = 프로젝트코드;
                m_cdePartner.CodeValue = 거래처코드;
                m_cdePartner.CodeName = 거래처명;
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
            _flexH.SetCol("NO_SO", "수주번호", 140);
            _flexH.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexH.SetCol("NM_SO", "수주형태", 100);
            _flexH.SetCol("CD_EXCH", "환종", 80);
            _flexH.SetCol("NM_SALEGRP", "영업그룹", 80);
            _flexH.SetCol("NM_KOR", "담당자", 80);
            _flexH.SetCol("CD_PARTNER", "거래처코드", 100);
            _flexH.SetCol("LN_PARTNER", "거래처명", 100);
            _flexH.SetCol("NM_PROJECT", "프로젝트명", 100);
            _flexH.SetCol("DC_RMK", "비고", 185); 
 
            _flexH.ExtendLastCol = true;
            _flexH.EnabledHeaderCheck = true;
            _flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flexH.AfterDataRefresh += new ListChangedEventHandler(_flex_AfterDataRefresh);
            _flexH.StartEdit += new RowColEventHandler(_flexH_StartEdit);
            _flexH.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
            _flexH.CheckHeaderClick += new EventHandler(_flex_CheckHeaderClick);

            //사용자그리드셋팅 기능 : 반듯이 EndSetting 다음에 코딩해줘야한다. : 반듯이 파라미터변수는 Page명 + Grid명 으로 한다.
            _flexH.LoadUserCache("P_SA_SO_SUB_flexH");
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
            _flexL.SetCol("DT_DUEDATE", "납기요구일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexL.SetCol("DT_REQGI", "출하예정일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexL.SetCol("QT_GIR", "의뢰적용수량", 100, true, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("REQ_QT_GIR", "수주잔량", 65, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("UM", "단가", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flexL.SetCol("AM_GIR", "금액", 100, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flexL.SetCol("AM_GIRAMT", "원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flexL.SetCol("NM_SL", "창고", 120, false);
            _flexL.SetCol("UNIT", "관리단위", 65, false);
            _flexL.SetCol("QT_GIR_IM", "관리수량", 65, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("GI_PARTNER", "납품처", 120, false );
            _flexL.SetCol("LN_PARTNER", "납품처명", 120, false );
            _flexL.SetCol("CD_ITEM_PARTNER", "거래처품번", 150, false);
            _flexL.SetCol("NM_ITEM_PARTNER", "거래처품명", 150, false);

            _flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            _flexL.StartEdit += new RowColEventHandler(_flexL_StartEdit);
            _flexL.CheckHeaderClick += new EventHandler(_flex_CheckHeaderClick);
            _flexL.ValidateEdit += new ValidateEditEventHandler(_flexL_ValidateEdit);

            //헤더에 필요없는 SUM 제거 && 반듯이 EndSetting 다음에 코딩해줘야한다.
            _flexL.SetExceptSumCol("UM");

            //사용자그리드셋팅 기능 : 반듯이 EndSetting 다음에 코딩해줘야한다. : 반듯이 파라미터변수는 Page명 + Grid명 으로 한다.
            _flexL.LoadUserCache("P_SA_SO_SUB_flexL");
        } 
        #endregion

        #region -> InitPaint

        protected override void InitPaint()
        {
            try
            {
                base.InitPaint();

                m_mskStart.Mask = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT);
                m_mskStart.ToDayDate = Global.MainFrame.GetDateTimeToday();
                m_mskStart.Text = Global.MainFrame.GetStringFirstDayInMonth;

                m_mskEnd.Mask = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT);
                m_mskEnd.ToDayDate = Global.MainFrame.GetDateTimeToday();
                m_mskEnd.Text = Global.MainFrame.GetStringToday;

                DataSet ds = Global.MainFrame.GetComboData("N;MA_B000005");

                _flexH.SetDataMap("CD_EXCH", ds.Tables[0], "CODE", "NAME");

                //수주담당자는 Setting에 저장되어서 Local 별로 셋팅한 내역으로 보여준다.
                //bp_Emp.CodeValue = Global.MainFrame.LoginInfo.EmployeeNo;
                //bp_Emp.CodeName = Global.MainFrame.LoginInfo.EmployeeName;

                if (bp_Project.Text != string.Empty)
                    bp_Project.Enabled = false;
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
            //수주일자
            if (m_mskStart.Text == "" || m_mskStart.Text == string.Empty)
            {
                //수주일자 은(는) 필수 입력입니다.
                Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, m_lblDtSo.Text);
                m_mskStart.Focus();
                return false;
            }

            //수주일자
            if (m_mskEnd.Text == "" || m_mskEnd.Text == string.Empty)
            {
                //수주일자 은(는) 필수 입력입니다.
                Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, m_lblDtSo.Text);
                m_mskStart.Focus();
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

                string no_project = string.Empty;

                if (bp_Project.Text == string.Empty)
                    no_project = string.Empty;
                else
                    no_project = txt_NoProject.Text;

                _flexH.Binding = _biz.Search(m_mskStart.Text, m_mskEnd.Text, m_cdePlant.CodeValue, 
                                             m_cdeTpBusi.CodeValue, m_cdePartner.CodeValue, bpSalegrp.CodeValue,
                                             bpNm_Sl.CodeValue, no_project, bp_Emp.CodeValue);

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

        #region Settings 로 인해 사용자 값 Local 별로 셋팅 하고자 함
        private void P_SA_SO_SUB_Load(object sender, EventArgs e)
        {
            bp_Emp.CodeValue = Settings.Default.no_emp_so;
            bp_Emp.CodeName = Settings.Default.nm_emp_so;
        }
        #endregion

        private void OnApply(object sender, System.EventArgs e)
        {
            try
            {
                if (!_flexL.HasNormalRow) return;

                DataRow[] dr = _flexL.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                if (dr == null || dr.Length == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                _dt = _flexL.DataTable.Clone();

                foreach (DataRow row in dr)
                {
                    if(D.GetDecimal(row["CHK_QT_GIR"]) != 0)
                    {
                        row["AM_VAT"] = Decimal.Truncate( _flexH.CDecimal(row["AM_GIRAMT"]) * (_flexH.CDecimal(row["RT_VAT"]) / 100)); //부가세
                    }
                    _dt.ImportRow(row);
                }

                //화면이 닫히기전에 Setting 하고자 하는 내역을 반듯이 저장해주어야 한다.
                Settings.Default.no_emp_so = bp_Emp.CodeValue;
                Settings.Default.nm_emp_so = bp_Emp.CodeName;

                //꼭 저장을 해줘야 한다.
                Settings.Default.Save();

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #region -> OnClosed(화면이 닫힐때)
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
             
            //사용자그리드셋팅 기능 : 반듯이 파라미터변수는 Page명 + Grid명 으로 한다.
            _flexH.SaveUserCache("P_SA_SO_SUB_flexH");
            _flexL.SaveUserCache("P_SA_SO_SUB_flexL");
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
                DataRow[] dr = _flexL.DataTable.Select("NO_SO = '" + _flexH[e.Row, "NO_SO"].ToString() + "'", "", DataViewRowState.CurrentRows);

                if (_flexH[e.Row, "S"].ToString() == "N") //클릭하는 순간은 N이므로
                {
                    for (int i = _flexL.Rows.Fixed; i <= dr.Length + 1; i++)
                        _flexL.SetCellCheck(i, 1, CheckEnum.Checked);
                }
                else
                {
                    for (int i = _flexL.Rows.Fixed; i <= dr.Length + 1; i++)
                        _flexL.SetCellCheck(i, 1, CheckEnum.Unchecked);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        void _flexL_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                //영업그룹에 영업조직을 조회하여 판매단가 통제유무가 "Y" 이면 단가와 금액 컬럼 수정을 할수 없다.
                if (so_Price == "Y")
                {
                    if (_flexL.Cols[e.Col].Name == "UM" || _flexL.Cols[e.Col].Name == "AM_GIR")
                    {
                        Global.MainFrame.ShowMessage("영업단가통제된 영업그룹입니다.");
                        e.Cancel = true;
                    }
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
            DataTable dt = null;

            string Key = _flexH[e.NewRange.r1, "NO_SO"].ToString();
            string Filter = "NO_SO = '" + Key + "'";

            if (_flexH.DetailQueryNeed)
            {
                string no_project = string.Empty;

                if (bp_Project.Text == string.Empty)
                    no_project = string.Empty;
                else
                    no_project = txt_NoProject.Text;

                dt = _biz.SearchDetail(Key, m_cdePlant.CodeValue, m_cdeTpBusi.CodeValue, bpNm_Sl.CodeValue, no_project);
            }
            _flexL.BindingAdd(dt, Filter);
            _flexH.DetailQueryNeed = false;

            object[] obj = new object[2];
            obj[0] = Global.MainFrame.LoginInfo.CompanyCode;
            obj[1] = _flexH[e.NewRange.r1, "CD_SALEGRP"].ToString(); 
            so_Price = _biz.GetSaleOrgUmCheck(obj); 
        }

        #endregion

        #region -> _flexL_ValidateEdit
        private void _flexL_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            FlexGrid _flex = new FlexGrid();

            try
            {
                string ColName = ((FlexGrid)sender).Cols[e.Col].Name;

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

                    case "QT_GIR":
                        if (_flex.CDecimal(_flexL["QT_GIR"]) > _flex.CDecimal(_flexL["REQ_QT_GIR"]))
                        {
                            _flexL["QT_GIR"] = _flexL["REQ_QT_GIR"];
                            MessageBox.Show("의뢰적용 수량이 수주적용 수량을 초과하였습니다.");
                        }
                        else
                        {
                            _flexL["UNIT_SO_FACT"] = _flex.CDecimal(_flexL["UNIT_SO_FACT"]) == 0 ? 1 : _flexL["UNIT_SO_FACT"];

                            _flexL["AM_GIR"] = _flex.CDecimal(_flexL["QT_GIR"]) * _flex.CDecimal(_flexL["UM"]);    //수주금액(외화) = 수량 * 단가
                            _flexL["AM_GIRAMT"] = Decimal.Truncate(_flex.CDecimal(_flexL["AM_GIR"]) * _flex.CDecimal(_flexL["RT_EXCH"]));//수주금액(원화) = 수량 * 환율
                            _flexL["AM_VAT"] = Decimal.Truncate(_flex.CDecimal(_flexL["AM_GIRAMT"]) * _flex.CDecimal(_flexL["RT_VAT"])); //부가세 = 수주금액(원화) * 부가세율
                            _flexL["QT_GIR_IM"] = _flex.CDecimal(_flexL["QT_GIR"]) * _flex.CDecimal(_flexL["UNIT_SO_FACT"]);
                        }
                        break;

                    case "UM":
                        _flexL["AM_GIR"] = _flex.CDecimal(_flexL["QT_GIR"]) * _flex.CDecimal(_flexL["UM"]);     //수주금액(외화) = 수량 * 단가
                        _flexL["AM_GIRAMT"] = Decimal.Truncate(_flex.CDecimal(_flexL["AM_GIR"]) * _flex.CDecimal(_flexL["RT_EXCH"])); //수주금액(원화) = 수량 * 환율
                        _flexL["AM_VAT"] = Decimal.Truncate(_flex.CDecimal(_flexL["AM_GIRAMT"]) * _flex.CDecimal(_flexL["RT_VAT"]));  //부가세 = 수주금액(원화) * 부가세율
                        break;

                    case "AM_GIR":
                        if (_flex.CDecimal(_flexL["QT_GIR"]) != 0)
                            _flexL["UM"] = _flex.CDecimal(_flexL["AM_GIR"]) / _flex.CDecimal(_flexL["QT_GIR"]);
                        else
                            _flexL["UM"] = 0;

                        _flexL["AM_GIRAMT"] = Decimal.Truncate(_flex.CDecimal(_flexL["AM_GIR"]) * _flex.CDecimal(_flexL["RT_EXCH"])); //수주금액(원화) = 수량 * 환율
                        _flexL["AM_VAT"] = Decimal.Truncate(_flex.CDecimal(_flexL["AM_GIRAMT"]) * _flex.CDecimal(_flexL["RT_VAT"]));  //부가세 = 수주금액(원화) * 부가세율
                        break; 
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
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

                        for (int h = 0; h < _flexH.Rows.Count - 1; h++)
                        {
                            _flexH.Row = h + 1; //Row가 순차적으로 변경되면서 RowChange() 이벤트를 자동 실행하게 유도한다.

                            for (int i = _flexL.Rows.Fixed; i < _flexL.Rows.Count; i++)
                            {
                                _flexL[i, "S"] = _flexH["S"].ToString();
                            }

                            //하단 데이타데이블의 모든 체크값을 변경해줌
                            foreach (DataRow dr in _flexL.DataTable.Rows)
                            {
                                dr["S"] = _flexH["S"].ToString();
                            }
                        }

                        break;

                    case "_flexL":  //하단 그리드 Header Click 이벤트

                        if (!_flexL.HasNormalRow) return;

                        _flexH["S"] = _flexL["S"].ToString();

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

        #region ★ 기타 이벤트

        #region -> Control_QueryBefore

        private void Control_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                switch (e.HelpID)
                {
                    case Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB:
                        e.HelpParam.P09_CD_PLANT = m_cdePlant.CodeValue;
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

        public DataTable 수주데이터
        {
            get { return _dt; }
        }

        #region ♣ 프리폼 프로젝트 도움창

        #region ♣ 프로젝트 선택했을시 거래처와 영업그룹의 활성 비활성을 제어해주는 부분
        private void Auth(bool check)
        {
            m_cdePartner.Enabled = check;
            bpSalegrp.Enabled = check;
        }
        #endregion

        #region ♣ 프로젝트 도움창버튼 선택시 발생하는 이벤트
        private void bp_Project_Search(object sender, SearchEventArgs e)
        {
            P_SA_PRJ_SUB dlg = null;

            try
            {
                dlg = new P_SA_PRJ_SUB();

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    //무조건 프로젝트 명을 먼저 셋팅할것
                    //프로젝트코드를 셋팅하고 명을 셋팅하면 후에 프로젝트 명에 bp_Project_TextChanged 이벤트가 작동하며 프로젝트 코드값이 지워지는 경우의 수 있음
                    bp_Project.Text = dlg.returnParams[4];      //프로젝트명
                    txt_NoProject.Text = dlg.returnParams[0];   //프로젝트번호

                    m_cdePartner.CodeValue = dlg.returnParams[5];
                    m_cdePartner.CodeName = dlg.returnParams[6];
                    bpSalegrp.CodeValue = dlg.returnParams[7];
                    bpSalegrp.CodeName = dlg.returnParams[8];

                    if (bp_Project.Text != string.Empty)
                        Auth(false);
                    else
                        Auth(true); 
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region ♣ 프로젝트 텍스트컨트롤에 값 입력 후 포커스가 빠져나갈때 발생하는 이벤트
        private void bp_Project_Leave(object sender, EventArgs e)
        { 
            if (bp_Project.Text == string.Empty)
            {
                txt_NoProject.Text = string.Empty;

                m_cdePartner.CodeValue = string.Empty;
                m_cdePartner.CodeName = string.Empty;
                bpSalegrp.CodeValue = string.Empty;
                bpSalegrp.CodeName = string.Empty;

                Auth(true);

                return;
            }

            try
            {
                DataSet ds = null;

                object[] obj = new object[2];
                obj[0] = Global.MainFrame.LoginInfo.CompanyCode;    //회사
                obj[1] = bp_Project.Text;                           //프로젝트 번호나 명으로 조회가 가능하다.

                ds = _biz.ProjectSearch(obj);

                if (ds != null && ds.Tables[0].Rows.Count == 0)
                {
                    //무조건 프로젝트 명을 먼저 셋팅할것
                    //프로젝트코드를 셋팅하고 명을 셋팅하면 후에 프로젝트 명에 bp_Project_TextChanged 이벤트가 작동하며 프로젝트 코드값이 지워지는 경우의 수 있음
                    bp_Project.Text = string.Empty;
                    txt_NoProject.Text = string.Empty;

                    //검색된 값이 하나도 없다면 팝업창을 띄워준다.
                    bp_Project_Search(null, null);
                }
                else if (ds != null && ds.Tables[0].Rows.Count == 1)
                {
                    //무조건 프로젝트 명을 먼저 셋팅할것
                    //프로젝트코드를 셋팅하고 명을 셋팅하면 후에 프로젝트 명에 bp_Project_TextChanged 이벤트가 작동하며 프로젝트 코드값이 지워지는 경우의 수 있음
                    bp_Project.Text = ds.Tables[0].Rows[0]["NM_PROJECT"].ToString();      //프로젝트명
                    txt_NoProject.Text = ds.Tables[0].Rows[0]["NO_PROJECT"].ToString();   //프로젝트번호

                    m_cdePartner.CodeValue = ds.Tables[0].Rows[0]["CD_PARTNER"].ToString();
                    m_cdePartner.CodeName = ds.Tables[0].Rows[0]["LN_PARTNER"].ToString();
                    bpSalegrp.CodeValue = ds.Tables[0].Rows[0]["CD_SALEGRP"].ToString();
                    bpSalegrp.CodeName = ds.Tables[0].Rows[0]["NM_SALEGRP"].ToString();

                    if (bp_Project.Text != string.Empty)
                        Auth(false);
                    else
                        Auth(true);
                }
                else if (ds != null && ds.Tables[0].Rows.Count > 1)
                {
                    string[] Params = new string[6];

                    Params[0] = m_cdePartner.CodeValue;
                    Params[1] = m_cdePartner.CodeName;
                    Params[2] = bpSalegrp.CodeValue;
                    Params[3] = bpSalegrp.CodeName;

                    //종결여부는 공백이면 전체 조회가 가능하도록 한다.
                    Params[4] = "NOT_END";
                    Params[5] = bp_Project.Text;

                    P_SA_PRJ_SUB dlg = new P_SA_PRJ_SUB(Params);

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        //무조건 프로젝트 명을 먼저 셋팅할것
                        //프로젝트코드를 셋팅하고 명을 셋팅하면 후에 프로젝트 명에 bp_Project_TextChanged 이벤트가 작동하며 프로젝트 코드값이 지워지는 경우의 수 있음
                        bp_Project.Text = dlg.returnParams[4];      //프로젝트명
                        txt_NoProject.Text = dlg.returnParams[0];   //프로젝트번호

                        m_cdePartner.CodeValue = dlg.returnParams[5];
                        m_cdePartner.CodeName = dlg.returnParams[6];
                        bpSalegrp.CodeValue = dlg.returnParams[7];
                        bpSalegrp.CodeName = dlg.returnParams[8];

                        if (bp_Project.Text != string.Empty)
                            Auth(false);
                        else
                            Auth(true);
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region ♣ 프로젝트 TXT가 변경될때 발생하는 이벤트
        private void bp_Project_TextChanged(object sender, EventArgs e)
        {
            txt_NoProject.Text = string.Empty;

            m_cdePartner.CodeValue = string.Empty;
            m_cdePartner.CodeName = string.Empty;
            bpSalegrp.CodeValue = string.Empty;
            bpSalegrp.CodeName = string.Empty;
        }
        #endregion
 
        #endregion
    }
}

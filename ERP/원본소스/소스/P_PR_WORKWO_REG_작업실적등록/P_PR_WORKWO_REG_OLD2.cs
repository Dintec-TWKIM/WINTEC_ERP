using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.Common.Forms.Help;

using System.Threading;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Windows.Print;

namespace prd
{
    // **************************************
    // 작   성   자 : 김현정
    // 재 작  성 일 : 2006-09-21
    // 모   듈   명 : 생산
    // 시 스  템 명 : 생산관리
    // 서브시스템명 : 공정관리
    // 페 이 지  명 : 작업실적등록(W/O별)
    // 프로젝트  명 : P_PR_WORKWO_REG
    // **************************************

    public partial class P_PR_WORKWO_REG : Duzon.Common.Forms.PageBase
    {
        #region ♣ 생성자 & 변수 선언

        private DataTable _dtReject = new DataTable();	//불량수량 저장을 위한 테이블

        private P_PR_WORKWO_REG_BIZ _biz = null;

        public P_PR_WORKWO_REG()
        {
            try
            {
                InitializeComponent();
                MainGrids = new FlexGrid[] { _flexD };
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ♣ 초기화 이벤트 / 메소드

        #region -> InitLoad

        protected override void InitLoad()
        {
            base.InitLoad();

            _biz = new P_PR_WORKWO_REG_BIZ();
            //InitDD(_tlayMain);

            InitGridM(); //작업장그리드
            InitGridD(); //작업시간 그리드

            _flexM.DetailGrids = new FlexGrid[] { _flexD };
        }

        #endregion

        #region -> InitGrid

        private void InitGridM()
        {
            _flexM.BeginSetting(1, 1, false);
            _flexM.SetCol("NO_WO", "작업지시번호", 110);
            _flexM.SetCol("FG_WO", "작업지시구분", 90);
            _flexM.SetCol("ST_WO", "상태", 60);
            _flexM.SetCol("CD_ITEM", "품목", 120);
            _flexM.SetCol("NM_ITEM", "품목명", 200);
            _flexM.SetCol("STND_ITEM", "규격", 150);
            _flexM.SetCol("QT_ITEM", "지시수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexM.SetCol("DT_REL", "시작일", 80,false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexM.SetCol("DT_DUE", "종료일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            _flexM.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            _flexM.BeforeRowChange += new RangeEventHandler(Grid_BeforeRowChange);
            _flexM.AfterRowChange += new RangeEventHandler(Grid_AfterRowChange);
        }

        private void InitGridD()
        {
            _flexD.BeginSetting(1, 1, false);
            _flexD.SetCol("CD_OP", "OP", 30);
            _flexD.SetCol("NM_OP", "공정명", 100);
            _flexD.SetCol("CD_WC", "작업장", 60);
            _flexD.SetCol("NM_WC", "작업장명", 100);
            _flexD.SetCol("ST_OP", "상태", 40);
            _flexD.SetCol("QT_WO", "지시수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexD.SetCol("QT_START", "입고수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexD.SetCol("QT_WO_WORK", "실적수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexD.SetCol("QT_REMAIN", "작업잔량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexD.SetCol("QT_MOVE", "이동수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexD.SetCol("QT_REWORKREMAIN", "재작업잔량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexD.VerifyNotNull = new string[] { "DT_WORK" };

            _flexD.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            _flexD.BeforeRowChange += new RangeEventHandler(Grid_BeforeRowChange);

            ControlBinding binder = new ControlBinding(_flexD, new PanelExt[] { panel11, panel16 }, new object[] {});
            
            binder.SetBindningCheckBox(m_chkFgIsu, "Y", "N");
            binder.SetBindningCheckBox(m_chkFgMove, "Y", "N");
            binder.SetBindningCheckBox(m_chkFgClose, "Y", "N");
        }

        #endregion

        #region -> InitPaint

        protected override void InitPaint()
        {
            base.InitPaint();

            InitControl();
        }

        #endregion

        #region -> InitControl

        private void InitControl()
        {
            DataSet ds = GetComboData("NC;MA_PLANT", "N;PR_0000009", "N;PR_0000007", "N;PR_0000006");

            m_cboCdPlant.DataSource = ds.Tables[0];
            m_cboCdPlant.DisplayMember = "NAME";
            m_cboCdPlant.ValueMember = "CODE";

            if (ds.Tables[0].Select("CODE = '" + LoginInfo.CdPlant + "'").Length == 1)
                m_cboCdPlant.SelectedValue = LoginInfo.CdPlant;
            else if (m_cboCdPlant.Items.Count > 0)
                m_cboCdPlant.SelectedIndex = 0;

            _flexD.SetDataMap("ST_OP", ds.Tables[1], "CODE", "NAME");
            _flexM.SetDataMap("FG_WO", ds.Tables[2], "CODE", "NAME");
            _flexM.SetDataMap("ST_WO", ds.Tables[3], "CODE", "NAME");

            m_lblStWo.Text = "Status";

            m_dtpDtWork.Mask = GetFormatDescription(DataDictionaryTypes.PR, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            m_dtpDtWork.Text = MainFrameInterface.GetStringToday;

            m_dtpFrom.Mask = GetFormatDescription(DataDictionaryTypes.PR, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            m_dtpFrom.Text = MainFrameInterface.GetStringToday.Substring(0, 6) + "01";

            m_dtpTo.Mask = GetFormatDescription(DataDictionaryTypes.PR, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            m_dtpTo.Text = MainFrameInterface.GetStringToday;
        }

        #endregion

        #endregion

        #region ♣ 메인버튼 이벤트 / 메소드

        #region -> BeforeSearch Override

        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch()) return false;

            if (!공장선택여부)
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, m_lblplant.Text);
                m_cboCdPlant.Focus();
                return false;
            }
            return true;
        }

        #endregion

        #region -> 조회버튼

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSearch())    return;

                DataTable dt = _biz.Search(m_cboCdPlant.SelectedValue.ToString(), m_bptxtCdItem.CodeValue, m_txtWoFrom.CodeValue, m_txtWoTo.CodeValue, m_dtpFrom.Text, m_dtpTo.Text, 마감구분);

                _flexM.Binding = dt;

                if (!_flexM.HasNormalRow)
                    ShowMessage(PageResultMode.SearchNoData);
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
                if (!BeforeSave())  return;

                int _Row = _flexM.Row; 
                int iCount = _flexM.DataTable.Rows.Count;
                
                if (MsgAndSave(PageActionMode.Save))
                {
                    ShowMessage(PageResultMode.SaveGood);

                    OnToolBarSearchButtonClicked(null, null);

                    if (iCount == _flexM.DataTable.Rows.Count)
                        _flexM.Select(_Row, 1);	//저장 후 조회(저장전 위치로 이동)
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> Verify Override

        protected override bool Verify()
        {
            if (!base.BeforeSave()) return false;

            decimal reject = m_curQtReject.DecimalValue;

            if (reject > 0)
            {
                if (_dtReject == null || _dtReject.Rows.Count < 1)
                {
                    ShowMessage(공통메세지._이가존재하지않습니다, "불량내역");

                    ShowRejectHelp();
                    _flexD.Focus();
                    return false;
                }
            }
            return base.Verify();
        }

        #endregion

        #region -> 실제저장구문(SaveData)

        protected override bool SaveData()
        {
            if (!Verify()) return false;

            DataTable dtWork = _flexD.GetChanges();
            
            if (dtWork == null) return false;

            if (_flexD.HasNormalRow)
            {
                string day = m_dtpDtWork.Text;
                string no = (string)GetSeq(LoginInfo.CompanyCode, "PR", "05", day.Substring(0, 6));

                if (day == "")
                    day = MainFrameInterface.GetStringToday.Substring(0, 6) + "01";
                
                if (no == "")
                {
                    ShowMessage(공통메세지.자료저장중오류가발생하였습니다);
                    return false;
                }
                else
                {
                    dtWork.Rows[0]["DT_WORK"] = day;
                    dtWork.Rows[0]["NO_WORK"] = no;
                    dtWork.Rows[0]["YN_REWORK"] = "N";

                    if (_dtReject != null)
                    {
                        for (int i = 0; i < _dtReject.Rows.Count; i++)
                        {
                            _dtReject.Rows[i]["QT_WORK"] = _flexD["QT_WORK"];
                            _dtReject.Rows[i]["NO_WORK"] = no;
                        }
                    }
                }
            }

            if (!_biz.Save(dtWork, _dtReject, _flexD["NO_LINE"].ToString())) return false;

            _flexD.AcceptChanges();
            _dtReject.Rows.Clear();
            _dtReject.AcceptChanges();
            
            return true;
        }

        #endregion

        #region -> 인쇄

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforePrint()) return;
                if (!MsgAndSave(PageActionMode.Print)) return;

                ReportHelper rptHelper = new ReportHelper("R_PR_WORK_REG_0", "작업실적전표");

                DataTable dt = _biz.print(m_cboCdPlant.SelectedValue.ToString(), _flexM["NO_WO"].ToString());
                rptHelper.SetDataTable(dt);
                rptHelper.가로출력();
                rptHelper.Print();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region ♣ 그리드 이벤트 / 메소드

        #region -> 그리드 행변경 이전 이벤트(Grid_BeforeRowChange)

        private void Grid_BeforeRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (IsPageValidated)
                {
                    if (IsChanged())
                    {
                        DialogResult result = ShowMessage(공통메세지.변경된사항이있습니다저장하시겠습니까);
                        if (result == DialogResult.No)
                        {
                            _flexD.RejectChanges();     //변경된 내용을 취소함.
                        }
                        else
                        {
                            //변경된 내용을 저장함.
                            e.Cancel = true;
                            OnToolBarSaveButtonClicked(null, null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 그리드 행변경 이후 이벤트(Grid_AfterRowChange)

        private void Grid_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                DataTable dt = null;

                string Filter = "NO_WO = '" + _flexM["NO_WO"].ToString() + "'";

                if (_flexM.DetailQueryNeed)
                {
                    dt = _biz.SearchDetail(m_cboCdPlant.SelectedValue.ToString(), _flexM["NO_WO"].ToString());
                }
                _flexD.BindingAdd(dt, Filter);
                _flexM.DetailQueryNeed = false;

                _dtReject.Clear();
            }
            catch (Exception ex)
            {   
                MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region ♣ 버튼 이벤트 / 메소드

        #region -> 불량내역등록 버튼 이벤트(m_btnRejectDtl_Click)

        private void m_btnRejectDtl_Click(object sender, System.EventArgs e)
        {
            try
            {
                ShowRejectHelp();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 실제 불량내역등록 구문(ShowRejectHelp)

        private void ShowRejectHelp()
        {
            if (BasicInfo.ActiveDialog == true) return;

            if (!_flexM.HasNormalRow)
            {
                ShowMessage(공통메세지.선택된자료가없습니다);
                return;
            }

            DataRow[] cur_row = _flexD.DataTable.Select("NO_WO = '" + _flexM["NO_WO"].ToString() + "'", "", DataViewRowState.CurrentRows);
            
            int qt_reject = 0;

            qt_reject = Convert.ToInt32(m_curQtReject.DecimalValue);

            if (qt_reject == 0)
            {
                ShowMessage(공통메세지._은_보다커야합니다, "불량수량", "0");
                return;
            }

            P_PR_WORK_SUB02 dlg = new P_PR_WORK_SUB02(cur_row, MainFrameInterface, m_cboCdPlant.Text.ToString(), qt_reject,
                _flexM["NM_ITEM"].ToString(), _flexM["STND_ITEM"].ToString(), _flexM["UNIT_IM"].ToString());

            if (dlg.ShowDialog() != DialogResult.OK) return;

            if (dlg is IHelpWindow)
            {
                object[] wo_sub = ((IHelpWindow)dlg).ReturnValues;
                _dtReject = (DataTable)wo_sub[0];
                return;
            }
        }

        #endregion

        #region -> 공정재작업처리 버튼 클릭 이벤트(m_btnproc_rework_Click)

        private void m_btnproc_rework_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (BasicInfo.ActiveDialog == true) return;

                if (!_flexM.HasNormalRow || !_flexD.HasNormalRow)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                string[] loa_param = new string[15];
                int lrow_WO = _flexM.Row;
                int lcol_WO = _flexM.Col;

                int lrow = _flexD.Row;
                int lcol = _flexD.Col;

                loa_param[0] = _flexD["CD_PLANT"].ToString();	//공장
                loa_param[1] = m_cboCdPlant.Text.Substring(0, m_cboCdPlant.Text.IndexOf("(")); //공장명
                loa_param[2] = _flexD["NO_WO"].ToString();		//작지번호
                loa_param[3] = _flexD["CD_WC"].ToString();		//작업장
                loa_param[4] = _flexD["NM_WC"].ToString();		//작업장명
                loa_param[5] = _flexD["CD_OP"].ToString();		//공정
                loa_param[6] = _flexD["NM_OP"].ToString();		//공정명
                loa_param[7] = _flexD["CD_ITEM"].ToString();		//품목
                loa_param[8] = _flexM["NM_ITEM"].ToString();
                loa_param[9] = _flexM["STND_ITEM"].ToString();	//규격
                loa_param[10] = _flexM["UNIT_IM"].ToString();	//단위
                loa_param[11] = _flexD["QT_WO_REJECT"].ToString();//불량수량
                loa_param[12] = _flexD["QT_REWORK"].ToString();//재작업수량
                loa_param[13] = _flexD["QT_REWORKREMAIN"].ToString();//재작업잔량
                loa_param[14] = "WO";

                P_PR_WORK_SUB dlg = new P_PR_WORK_SUB(loa_param, _flexD.DataView, lrow);

                if (dlg.ShowDialog() != DialogResult.OK) return;

                _flexD.AcceptChanges();

                int iCount = _flexM.DataTable.Rows.Count;

                OnToolBarSearchButtonClicked(null, null);

                if (iCount == _flexM.DataTable.Rows.Count)
                    _flexM.Select(lrow_WO, lcol_WO);

                _flexM.Focus();
                _flexD.Select(lrow, lcol);
                _flexD.Focus();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 투입상세 버튼 클릭 이벤트(m_btndtl_isu_Click)

        private void m_btndtl_isu_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (Duzon.Common.Forms.BasicInfo.ActiveDialog == true)  return;

                if (!_flexM.HasNormalRow)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                
                string[] args = new string[8]{  m_cboCdPlant.SelectedValue.ToString(),
                                                _flexM["DT_REL"].ToString(),		//시작일
                                                _flexM["DT_DUE"].ToString(),		//종료일
                                                _flexD["CD_WC"].ToString(),		    //작업장코드
                                                _flexM["CD_ITEM"].ToString(),		//품목코드
                                                _flexM["NO_WO"].ToString(), 		//작업지시번호
                                                _flexD["NM_WC"].ToString(),		    //작업장명
                                                _flexM["NM_ITEM"].ToString(),		//품목명
											 };

                object[] obj_args = new object[2] { args, MainFrameInterface };

                if (IsExistPage("P_PR_II_SCH01", true) == true)
                    UnLoadPage("P_PR_II_SCH01", false);     //페이지를 먼저 닫는다.
           
                CallOtherPageMethod("P_PR_II_SCH01", "자재투입List", Grant, obj_args);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 실적이력 버튼 클릭 이벤트(m_btnhst_work_Click)

        private void m_btnhst_work_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (BasicInfo.ActiveDialog == true) return;

                if (!_flexM.HasNormalRow)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    DataRow[] cur_row = _flexD.DataTable.Select("NO_WO = '" + _flexM["NO_WO"].ToString() + "' AND CD_OP = '" + _flexD["CD_OP"].ToString() + "'", "", DataViewRowState.CurrentRows);
                    if (cur_row == null || cur_row.Length < 1)
                    {
                        ShowMessage(공통메세지._이가존재하지않습니다, "실적이력");
                        return;
                    }

                    int iQT_START = -1;
                    int iQT_WORK = -1;

                    if (_flexD.Row < _flexD.DataView.Count)//맨 마지막 행을 제외한 행일 때
                    {
                        iQT_START = Convert.ToInt32(_flexD[_flexD.Row + 1, "QT_START"]);
                        iQT_WORK = Convert.ToInt32(_flexD[_flexD.Row + 1, "QT_WO_WORK"]);
                    }

                    object dlg = LoadHelpWindow("P_PR_WORK_HST_SUB01", new object[] { cur_row, MainFrameInterface, iQT_START, iQT_WORK, 
                        m_cboCdPlant.Text.Substring(0, m_cboCdPlant.Text.IndexOf("(")), _flexM["NM_ITEM"].ToString(), _flexM["STND_ITEM"].ToString(), _flexM["UNIT_IM"].ToString()});

                    ((Duzon.Common.Forms.CommonDialog)dlg).ShowDialog();

                    OnToolBarSearchButtonClicked(null, null);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion		

        #endregion

        #region ♣ 도움창 이벤트 / 메소드

        #region -> 도움창 호출전 이벤트(OnBpControl_QueryBefore)

        private void OnBpControl_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                switch (e.HelpID)
                {
                    case HelpID.P_MA_PITEM_SUB:     //공장품목
                    case HelpID.P_PR_WO_REG_SUB:    //작업지시번호
                        e.HelpParam.P09_CD_PLANT = m_cboCdPlant.SelectedValue.ToString();
                        e.HelpParam.P65_CODE5 = m_cboCdPlant.Text.Replace(" ", "").Remove(0, m_cboCdPlant.Text.Replace(" ", "").IndexOf(")", 0) + 1);
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 도움창 호출후 이벤트(OnBpControl_QueryAfter)

        private void OnBpControl_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                DataRow[] _dr = e.HelpReturn.Rows;
                m_txtStndItem.Text = _dr[0]["STND_ITEM"].ToString();
                m_txtUnit.Text = _dr[0]["UNIT_IMNM"].ToString();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 도움창 변경 이벤트(OnBpControl_CodeChanged)

        private void OnBpControl_CodeChanged(object sender, System.EventArgs e)
        {
            try
            {
                m_txtStndItem.Text = m_txtUnit.Text = "";
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region ♣ Control 이벤트 / 메소드

        #region -> Validate Check 이벤트(OnControlValidating)

        private void OnControlValidating(object sender, CancelEventArgs e)
        {
            string control_name = ((Control)sender).Name.ToString();
            DataTable _dthelp = new DataTable();
            Decimal qtValue = 0;

            try
            {
                switch (control_name)
                {
                    case "m_dtpDtWork":
                    case "m_dtpFrom":
                    case "m_dtpTo":
                        DatePicker dt = (DatePicker)sender;
                        if (!dt.Modified) return;

                        if (dt.Text == "")
                            return;

                        if (!dt.IsValidated)
                        {
                            ShowMessage(공통메세지.입력형식이올바르지않습니다);
                            dt.Text = "";
                            dt.Focus();
                            e.Cancel = true;
                            return;
                        }

                        if (dt.Name == "m_dtpFrom" || dt.Name == "m_dtpTo")
                        {
                            if (m_dtpFrom.Value > m_dtpTo.Value)
                            {
                                ShowMessage(공통메세지.시작일자보다종료일자가클수없습니다);
                                m_dtpTo.Text = "";
                                m_dtpTo.Focus();
                                e.Cancel = true;
                                return;
                            }
                        }
                        break;
                    case "m_curQtWork":
                    case "m_curQtReject":
                        CurrencyTextBox qt = (CurrencyTextBox)sender;

                        if (!qt.Modified) return;

                        qtValue = qt.DecimalValue;

                        if (qt.Name == "m_curQtWork" && qtValue != 0)
                        {
                            if (qtValue > (Convert.ToDecimal(_flexD["QT_START"]) - Convert.ToDecimal(_flexD["QT_WO_WORK"])))
                            {
                                // 실적수량이 입고수량을 초과합니다.
                                DialogResult dialog = MessageBoxEx.Show(Global.MainFrame.GetMessageDictionaryItem("PR_M200005"), "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                                if (DialogResult.Cancel == dialog)
                                {
                                    m_curQtWork.DecimalValue = 0;
                                    e.Cancel = true;
                                    return;
                                }
                            }
                            return;
                        }
                        if (qt.Name == "m_curQtReject" && qtValue != 0)
                        {
                            if (qtValue > m_curQtWork.DecimalValue)
                            {
                                ShowMessage(메세지.불량수량이작업수량을초과합니다);
                                m_curQtReject.DecimalValue = 0;
                                e.Cancel = true;
                                return;
                            }
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

        #endregion

        #region ♣기타메서드

        #region -> 실적내용Clear 구문(ClearDataControl)

        private void ClearDataControl()
        {
            if (!_flexD.HasNormalRow)
                return;

            m_curQtReject.DecimalValue = 0;
        }

        #region -> 하단 그리드 데이터 갱신(사용하지 않음)
        
        private void SetDataValue(DataTable dt,int iCount)
        {
            if (dt == null || dt.Rows.Count < 1)
                return;

            int i = 0;
            for (i = 0; i < dt.Rows.Count  ; i++)
            {
                _flexD.Rows[i + 1]["ST_OP"] = dt.Rows[i]["ST_OP"];
                _flexD.Rows[i + 1]["QT_WO"] = dt.Rows[i]["QT_WO"];
                _flexD.Rows[i + 1]["QT_START"] = dt.Rows[i]["QT_START"];
                _flexD.Rows[i + 1]["QT_WO_WORK"] = dt.Rows[i]["QT_WO_WORK"];
                _flexD.Rows[i + 1]["QT_REMAIN"] = dt.Rows[i]["QT_REMAIN"];
                _flexD.Rows[i + 1]["QT_MOVE"] = dt.Rows[i]["QT_MOVE"];
                _flexD.DataTable.Rows[i + 1]["QT_WO_REJECT"] = dt.Rows[i]["QT_WO_REJECT"];
                _flexD.DataTable.Rows[i + 1]["QT_REWORK"] = dt.Rows[i]["QT_REWORK"];
                _flexD.DataTable.Rows[i + 1]["QT_REWORKREMAIN"] = dt.Rows[i]["QT_REWORKREMAIN"];
            }

            //작업지시상태 업데이트사항 반영
            if (iCount == _flexM.DataTable.Rows.Count)
            {
                _flexM["ST_WO"] = dt.Rows[i]["ST_WO"];
            }
            
        }

        #endregion

        #endregion

        #region -> 메세지

        private DialogResult ShowMessage(메세지 msg, params object[] paras)
        {
            switch (msg)
            {
                case 메세지.불량수량이작업수량을초과합니다:
                    return ShowMessage("PR_M200003");
            }

            return DialogResult.None;
        }

        #endregion

        #endregion

        #region ♣ 속성들

        string 진행상태
        {
            get
            {
                string StWo = "";
                if (m_chkNonClose.Checked == true && m_chkClose.Checked == true)
                    StWo = "SRC";
                else if (m_chkNonClose.Checked == true && m_chkClose.Checked == false)
                    StWo = "SR";
                else if (m_chkNonClose.Checked == false && m_chkClose.Checked == true)
                    StWo = "SC";
                else if (m_chkNonClose.Checked == false && m_chkClose.Checked == false)
                    StWo = "S";
                return StWo;
            }
        }

        string 마감구분
        {
            get
            {
                string FgClose = "";

                if (m_chkNonClose.Checked == true && m_chkClose.Checked == true)
                    FgClose = "S|R|C|";
                else if (m_chkNonClose.Checked == true && m_chkClose.Checked == false)
                    FgClose = "S|R|";
                else if (m_chkNonClose.Checked == false && m_chkClose.Checked == true)
                    FgClose = "C|";
                else if (m_chkNonClose.Checked == false && m_chkClose.Checked == false)
                    FgClose = "|";

                return FgClose;
            }
        }

        bool 공장선택여부
        {
            get
            {
                if (m_cboCdPlant.SelectedValue == null || m_cboCdPlant.SelectedValue.ToString() == "")
                    return false;
                return true;
            }
        }

        #endregion

    }
}


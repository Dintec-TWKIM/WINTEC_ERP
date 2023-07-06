using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Util;

using System.Threading;
using DzHelpFormLib;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;

namespace trade
{
    // **************************************
    // 작   성   자 : 허성철
    // 재 작  성 일 : 2006-11-17
    // 모   듈   명 : 무역
    // 시 스  템 명 : 무역관리
    // 서브시스템명 : 수출
    // 페 이 지  명 : 통관등록
    // 프로젝트  명 : P_TR_EXTO_NEW
    // 수 정 내 역  : 2013.07.25 - 윤성우 - ONE GRID 수정(입력 전용)
    // **************************************
    public partial class P_TR_EXTO_NEW : Duzon.Common.Forms.PageBase
    {
        #region ★ 멤버필드

        private P_TR_EXTO_BIZ _biz = new P_TR_EXTO_BIZ();
        private FreeBinding _header = null;

        private DataTable dtLine = null;
        private string CompanyCode = Global.MainFrame.LoginInfo.CompanyCode;

        String _송장첨부CHK설정 = "000";

        #endregion

        #region ★ 초기화

        #region -> 생성자

        public P_TR_EXTO_NEW()
        {
            try
            {
                InitializeComponent();

                _header = new FreeBinding();
                _header.JobModeChanged += new FreeBindingEventHandler(_header_JobModeChanged);
                _header.ControlValueChanged += new FreeBindingEventHandler(_header_ControlValueChanged);

            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        #region -> InitLoad

        protected override void InitLoad()
        {
            base.InitLoad();

            _송장첨부CHK설정 = Duzon.ERPU.MF.ComFunc.전용코드("송장등록-첨부파일");
        }

        #endregion

        #region -> InitPaint

        protected override void InitPaint()
        {
            base.InitPaint();

            oneGrid1.UseCustomLayout = true;
            bpPanelControl1.IsNecessaryCondition = bpPanelControl4.IsNecessaryCondition = bpPanelControl7.IsNecessaryCondition =
            bpPanelControl8.IsNecessaryCondition = bpPanelControl12.IsNecessaryCondition = bpPanelControl18.IsNecessaryCondition =
            bpPanelControl16.IsNecessaryCondition = bpPanelControl21.IsNecessaryCondition = bpPanelControl20.IsNecessaryCondition =
            bpPanelControl19.IsNecessaryCondition = bpPanelControl24.IsNecessaryCondition = bpPanelControl23.IsNecessaryCondition =
            bpPanelControl22.IsNecessaryCondition = bpPanelControl27.IsNecessaryCondition = true;

            oneGrid1.IsSearchControl = false; //2013.07.25 - 윤성우 - ONE GRID 수정(입력 전용)
            
            oneGrid1.InitCustomLayout();

            DataSet ds = GetComboData("N;TR_IM00014", "N;TR_IM00005", "S;TR_IM00015", "N;MA_B000005", "S;TR_IM00011");

            //면장구분코드 ComboBox에 Add
            cbo면장구분.DataSource = ds.Tables[0];
            cbo면장구분.ValueMember = "CODE";
            cbo면장구분.DisplayMember = "NAME";

            //L/C구분 ComboBox에 Add
            cboLC구분.DataSource = new DataView(ds.Tables[1], "CODE IN ('003', '004', '005')", "CODE", DataViewRowState.CurrentRows); 
            cboLC구분.ValueMember = "CODE";
            cboLC구분.DisplayMember = "NAME";

            //관세환급신고자 ComboBox에 Add
            cbo관세환급신고자.DataSource = ds.Tables[2];
            cbo관세환급신고자.ValueMember = "CODE";
            cbo관세환급신고자.DisplayMember = "NAME";

            //통화구분 ComboBox에 Add
            cbo통화.DataSource = ds.Tables[3];
            cbo통화.ValueMember = "CODE";
            cbo통화.DisplayMember = "NAME";

            //포장형태 ComboBox에 Add
            cbo포장형태.DataSource = ds.Tables[4];
            cbo포장형태.ValueMember = "CODE";
            cbo포장형태.DisplayMember = "NAME";

            DataTable dt1 = new DataTable();
            dt1.Columns.Add("CODE", typeof(string));
            dt1.Columns.Add("NAME", typeof(string));
            DataRow row1 = null;
            row1 = dt1.NewRow(); row1["CODE"] = "N"; row1["NAME"] = DD("출고"); dt1.Rows.Add(row1);
            row1 = dt1.NewRow(); row1["CODE"] = "Y"; row1["NAME"] = DD("반품"); dt1.Rows.Add(row1);

            // 출고구분 추가 20110825
            cbo출고구분.DataSource = dt1;
            cbo출고구분.DisplayMember = "NAME";
            cbo출고구분.ValueMember = "CODE";

            // 프리폼 초기화
            DataTable dt = _biz.Search("");
            _header.SetBinding(dt, oneGrid1);
            _header.ClearAndNewRow();

            if (MA.기준환율.Option == MA.기준환율옵션.적용_수정불가)
                cur면장환율.Enabled = false;

            cbo출고구분.SelectedIndex = 0;
            _header.CurrentRow["YN_RETURN"] = "N";

            // 2011-07-28, 첨부기능 추가함. 최승애
            if (D.GetString(_송장첨부CHK설정) == string.Empty)
                _송장첨부CHK설정 = "000";

            if (_송장첨부CHK설정 == "100")
                btn첨부파일.Visible = true;

            
            //  2012.05.10 최창종 추가 - DEFAULT 설정
            if (BASIC.GetMAEXC("통관등록-DEFAULT") == "001")
            {
                cbo통화.SelectedValue = "001";
                cboLC구분.SelectedValue = "005";
            }

            cur금액.Mask = this.GetFormatDescription(DataDictionaryTypes.TR, FormatTpType.FOREIGN_MONEY, FormatFgType.INSERT);
        }

        #endregion

        #endregion

        #region ★ 저장관련메소드

        #region -> SaveData

        protected override bool SaveData()
        {
            if (!base.SaveData())
                return false;

            DataTable dt = _header.GetChanges();

            //참조송장번호를 배열로 받아온다.
            //1.통관등록 라인테이블을 만든다.
            DataTable dtValue = Global.MainFrame.FillDataTable("SELECT * FROM TR_EXTOL WHERE CD_COMPANY = '" + CompanyCode + "' AND NO_TO = '" + this.txt신고번호.Text + "' ");

            DataTable _dtLine = new DataTable();

            _dtLine.Columns.Add("NO_TO", typeof(string));
            _dtLine.Columns.Add("NO_INV", typeof(string));
            _dtLine.Columns.Add("NO_BL", typeof(string));
            _dtLine.Columns.Add("DT_DECLARE", typeof(string));
            _dtLine.Columns.Add("DTS_INSERT", typeof(string));
            _dtLine.Columns.Add("ID_INSERT", typeof(string));
            _dtLine.Columns.Add("DTS_UPDATE", typeof(string));
            _dtLine.Columns.Add("ID_UPDATE", typeof(string));

            if (dtValue == null || dtValue.Rows.Count <= 0)
            {
                DataRow dr = null;
                //2.받아온 참조 송장번호만큼 루프를 돌린다
                for (int i = 0; i < dtLine.Rows.Count; i++)
                {
                    dr = _dtLine.NewRow();

                    dr["NO_TO"] = this.txt신고번호.Text;
                    dr["NO_INV"] = dtLine.Rows[i]["NO_INV"].ToString();
                    dr["NO_BL"] = "";
                    dr["DT_DECLARE"] = this.dtp신고일.Text;
                    dr["DTS_INSERT"] = Global.MainFrame.LoginInfo.UserID;
                    dr["ID_INSERT"] = Global.MainFrame.GetStringToday;
                    dr["DTS_UPDATE"] = Global.MainFrame.LoginInfo.UserID;
                    dr["ID_UPDATE"] = Global.MainFrame.GetStringToday;

                    _dtLine.Rows.Add(dr);
                }
            }


            if (dt == null)
                return true;

            bool bSuccess = _biz.Save(dt, _dtLine);

            if (!bSuccess)
                return false;

            _header.AcceptChanges();        // JobModeChanged 이벤트가 발생함. 모드는 "조회후수정"
            btn통관내역.Enabled = true;

            return true;
        }

        #endregion

        #region -> BeforeSave

        protected override bool BeforeSave()
        {
            if (txt신고번호.Text == "" || txt신고번호.Text == string.Empty)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, lbl신고번호.Text);
                txt신고번호.Focus();
                return false;
            }
            else if (cbo면장구분.SelectedValue == DBNull.Value || cbo면장구분.SelectedValue == null || cbo면장구분.SelectedValue.ToString() == "" || cbo면장구분.SelectedValue.ToString() == string.Empty)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, lbl면장구분.Text);
                cbo면장구분.Focus();
                return false;
            }
            else if (tbtn참조송장번호.Text == "" || tbtn참조송장번호.Text == string.Empty)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, lbl참조송장번호.Text);
                tbtn참조송장번호.Focus();
                return false;
            }
            else if (cboLC구분.SelectedValue == DBNull.Value || cboLC구분.SelectedValue == null || cboLC구분.SelectedValue.ToString() == "" || cboLC구분.SelectedValue.ToString() == string.Empty)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, lblLC구분.Text);
                cboLC구분.Focus();
                return false;
            }
            else if (bpc거래처.IsEmpty() || bpc거래처.CodeValue == "" || bpc거래처.CodeValue == string.Empty)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, lbl거래처.Text);
                bpc거래처.Focus();
                return false;
            }
            else if (bpc영업그룹.IsEmpty() || bpc영업그룹.CodeValue == "" || bpc영업그룹.CodeValue == string.Empty)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, lbl영업그룹.Text);
                bpc영업그룹.Focus();
                return false;
            }
            else if (bpc사업장.IsEmpty() || bpc사업장.CodeValue == "" || bpc사업장.CodeValue == null)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, lbl사업장.Text);
                bpc사업장.Focus();
                return false;
            }
            else if (bpc담당자.IsEmpty() || bpc담당자.CodeValue == "" || bpc담당자.CodeValue == string.Empty)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, lbl담당자.Text);
                bpc담당자.Focus();
                return false;
            }
            else if (dtp신고일.Text == "" || dtp신고일.Text == string.Empty)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, lbl신고일.Text);
                dtp신고일.Focus();
                return false;
            }
            else if (cbo통화.SelectedValue == DBNull.Value || cbo통화.SelectedValue == null || cbo통화.SelectedValue.ToString() == "" || cbo통화.SelectedValue.ToString() == string.Empty)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, lbl통화.Text);
                cbo통화.Focus();
                return false;
            }

            return true;
        }

        #endregion

        #endregion

        #region ★ 메인버튼 클릭

        #region -> 조회버튼 클릭

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                string 통관참조여부 = "001"; //통관참조아님
                if (!base.BeforeSearch()) return;   // BeforeSearch 내부에서 IsChanged 를 호출하므로 변경여부가 판단됨

                P_TR_EXTONO_SUB dlg = new P_TR_EXTONO_SUB(통관참조여부);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    DataTable dt = _biz.Search(dlg.통관번호);
                    _header.SetDataTable(dt);       // JobModeChanged 이벤트가 발생됨

                    DataTable dtLine = Global.MainFrame.FillDataTable("SELECT * FROM TR_EXTOL WHERE CD_COMPANY = '" + CompanyCode + "' AND NO_TO = '" + dlg.통관번호 + "' ");

                    if (dtLine != null && dtLine.Rows.Count > 0)
                    {
                        string strNO_INV = string.Empty;

                        for (int i = 0; i < dtLine.Rows.Count; i++)
                        {
                            strNO_INV += dtLine.Rows[i]["NO_INV"].ToString() + ",";
                        }

                        this.tbtn참조송장번호.Text = strNO_INV;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 추가버튼 클릭

        protected override bool BeforeAdd()
        {
            if (!base.BeforeAdd())
                return false;

            if (!MsgAndSave(PageActionMode.Search))
                return false;

            return true;
        }

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeAdd()) return;

                Debug.Assert(_header.CurrentRow != null);       // 혹시나 해서 한번 더 확인

                _header.ClearAndNewRow();

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 삭제버튼 클릭

        protected override bool BeforeDelete()
        {
            if (!base.BeforeDelete())
                return false;

            if (ShowMessage(공통메세지.자료를삭제하시겠습니까) != DialogResult.Yes)
                return false;
            else
            {
                if (_header.CurrentRow["YN_BL"].ToString() == "Y")
                {
                    ShowMessage("선적자료가 존재하므로 삭제할 수 없습니다.");
                    return false;
                }
            }

            return true;
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeDelete())
                    return;

                if (_biz.Delete(txt신고번호.Text))
                {
                    ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
                    _header.ClearAndNewRow();
                    OnToolBarAddButtonClicked(sender, e);       // 삭제 후 바로 추가모드로 해준다.
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 저장버튼 클릭

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSave()) return;

                if (MsgAndSave(PageActionMode.Save))
                {
                    this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region ★ 화면내버튼 클릭

        #region -> 통관내역버튼 클릭

        private void btn통관내역_Click(object sender, EventArgs e)
        {
            try
            {
                P_TR_EXTOL_SUB dlg = new P_TR_EXTOL_SUB(    
                    txt신고번호.Text, 
                    cbo면장구분.Text, 
                    dtp신고일.Text, 
                    bpc거래처.CodeName,
                    bpc대행자.CodeName, 
                    cur금액.DecimalValue, 
                    cbo통화.Text,
                    cur면장환율.DecimalValue, 
                    curFOB금액.DecimalValue, 
                    curFOB원화금액.DecimalValue,
                    tbtn참조송장번호.Text, 
                    bpc사업장.CodeValue
                                                        );
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 판매경비등록버튼 클릭

        private void btn판매경비등록_Click(object sender, EventArgs e)
        {
            try
            {
                // 경비발생구분, 관리번호, 기표일자, 기표사업장코드,기표사업장명
                // 부서코드, 부서명, 담당자코드 ,담당자명, C/C 코드, C/C명

                DataTable dt = _biz.SearchToBF(this.txt신고번호.Text.ToString());

                string[] ls_args = new string[12];
                ls_args[0] = "TO";
                ls_args[1] = txt신고번호.Text;
                ls_args[2] = dt.Rows[0]["DT_LICENSE"].ToString();
                ls_args[3] = dt.Rows[0]["CD_BIZAREA"].ToString();
                ls_args[4] = dt.Rows[0]["NM_BIZAREA"].ToString();
                ls_args[5] = dt.Rows[0]["CD_DEPT"].ToString();
                ls_args[6] = dt.Rows[0]["NM_DEPT"].ToString();
                ls_args[7] = dt.Rows[0]["NO_EMP"].ToString();
                ls_args[8] = dt.Rows[0]["NM_EMP"].ToString();
                ls_args[9] = dt.Rows[0]["CD_CC"].ToString();
                ls_args[10] = dt.Rows[0]["NM_CC"].ToString();
                ls_args[11] = "0";

                if (IsExistPage("P_TR_EXCOST", false))
                    UnLoadPage("P_TR_EXCOST", false);

                this.LoadPageFrom("P_TR_EXCOST", GetDataDictionaryItem("INPUT_COST"), Grant, ls_args);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region ★ 기타 이벤트

        #region -> _header_ControlValueChanged

        void _header_ControlValueChanged(object sender, FreeBindingArgs e)
        {
            try
            {
                switch (((Control)sender).Name)
                {
                    case "txt신고번호":
                        if (txt신고번호.Text != "" && _header.JobMode == JobModeEnum.추가후수정)
                            ToolBarSaveButtonEnabled = true;
                        break;
                    case "cur기표환율":
                        cur원화금액.DecimalValue = Unit.원화금액(DataDictionaryTypes.TR, cur기표환율.DecimalValue * cur금액.DecimalValue);
                        _header.CurrentRow["AM"] = cur원화금액.DecimalValue;
                        break;
                    case "cur금액":
                        cur원화금액.DecimalValue = Unit.원화금액(DataDictionaryTypes.TR, cur기표환율.DecimalValue * cur금액.DecimalValue);
                        _header.CurrentRow["AM"] = cur원화금액.DecimalValue;

                        //curFOB원화금액.DecimalValue = cur면장환율.DecimalValue * cur금액.DecimalValue;
                        //_header.CurrentRow["AM_FOB"] = curFOB원화금액.DecimalValue;
                        break;
                    case "cur면장환율":
                        //curFOB원화금액.DecimalValue = cur면장환율.DecimalValue * cur금액.DecimalValue;
                        //_header.CurrentRow["AM_FOB"] = curFOB원화금액.DecimalValue;
                        curFOB원화금액.DecimalValue = Unit.원화금액(DataDictionaryTypes.TR, cur면장환율.DecimalValue * curFOB금액.DecimalValue);
                        _header.CurrentRow["AM_FOB"] = curFOB원화금액.DecimalValue;
                        break;

                    case "curFOB금액":
                        //curFOB원화금액.DecimalValue = cur면장환율.DecimalValue * cur금액.DecimalValue;
                        //_header.CurrentRow["AM_FOB"] = curFOB원화금액.DecimalValue;
                        curFOB원화금액.DecimalValue = Unit.원화금액(DataDictionaryTypes.TR, cur면장환율.DecimalValue * curFOB금액.DecimalValue);
                        _header.CurrentRow["AM_FOB"] = curFOB원화금액.DecimalValue;
                        break;

                    default:
                        this.ToolBarSaveButtonEnabled = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
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
                    txt신고번호.Enabled = true;
                    txt신고번호.Focus();
                    tbtn참조송장번호.Enabled = true;

                    btn판매경비등록.Enabled = false;
                    btn통관내역.Enabled = false;

                    cbo출고구분.Enabled = true;

                    ToolBarAddButtonEnabled = true;
                    ToolBarDeleteButtonEnabled = false;
                    ToolBarSaveButtonEnabled = false;
                }
                else
                {
                    txt신고번호.Enabled = false;
                    tbtn참조송장번호.Enabled = false;
                    btn판매경비등록.Enabled = true;
                    btn통관내역.Enabled = true;

                    cbo출고구분.Enabled = false;

                    ToolBarAddButtonEnabled = true;
                    ToolBarDeleteButtonEnabled = true;
                    ToolBarSaveButtonEnabled = false;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 참조송장번호 클릭

        private void tbtn참조송장번호_Search(object sender, SearchEventArgs e)
        {
            try
            {
                P_TR_EXTO_SUB dlg = new P_TR_EXTO_SUB();
                dlg.T_거래처CODE = bpc거래처.CodeValue.ToString();
                dlg.T_거래처NAME = bpc거래처.CodeName.ToString();
                dlg.T_LC구분 = cboLC구분.SelectedValue.ToString();
                dlg.T_통화 = cbo통화.SelectedValue.ToString();
                dlg.T_출고구분 = cbo출고구분.SelectedValue.ToString();
                

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    #region -> Old 코드

                    //DataRow dr = dlg.ReturnRow;

                    //if (dr == null) return;
                    //else ToolBarSaveButtonEnabled = true;

                    //_header.CurrentRow["NO_INV"] = dr["NO_INV"].ToString();
                    //tbtn참조송장번호.Text = dr["NO_INV"].ToString();

                    //_header.CurrentRow["CD_BIZAREA"] = dr["CD_BIZAREA"].ToString();
                    //_header.CurrentRow["NM_BIZAREA"] = dr["NM_BIZAREA"].ToString();
                    //bpc사업장.CodeValue = dr["CD_BIZAREA"].ToString();
                    //bpc사업장.CodeName = dr["NM_BIZAREA"].ToString();

                    //_header.CurrentRow["CD_SALEGRP"] = dr["CD_SALEGRP"].ToString();
                    //_header.CurrentRow["NM_SALEGRP"] = dr["NM_SALEGRP"].ToString();
                    //bpc영업그룹.CodeValue = dr["CD_SALEGRP"].ToString();
                    //bpc영업그룹.CodeName = dr["NM_SALEGRP"].ToString();

                    //_header.CurrentRow["NO_EMP"] = dr["NO_EMP"].ToString();
                    //_header.CurrentRow["NM_KOR"] = dr["NM_KOR"].ToString();
                    //bpc담당자.CodeValue = dr["NO_EMP"].ToString();
                    //bpc담당자.CodeName = dr["NM_KOR"].ToString();

                    //_header.CurrentRow["CD_PARTNER"] = dr["CD_PARTNER"].ToString();
                    //_header.CurrentRow["NM_PARTNER"] = dr["NM_PARTNER"].ToString();
                    //bpc거래처.CodeValue = dr["CD_PARTNER"].ToString();
                    //bpc거래처.CodeName = dr["NM_PARTNER"].ToString();

                    //_header.CurrentRow["CD_AGENT"] = dr["CD_AGENT"].ToString();
                    //_header.CurrentRow["NM_AGENT"] = dr["NM_AGENT"].ToString();
                    //bpc대행자.CodeValue = dr["CD_AGENT"].ToString();
                    //bpc대행자.CodeName = dr["NM_AGENT"].ToString();

                    //_header.CurrentRow["CD_EXPORT"] = dr["CD_EXPORT"].ToString();
                    //_header.CurrentRow["NM_EXPORT"] = dr["NM_EXPORT"].ToString();
                    //bpc수출자.CodeValue = dr["CD_EXPORT"].ToString();
                    //bpc수출자.CodeName = dr["NM_EXPORT"].ToString();

                    //_header.CurrentRow["CD_PRODUCT"] = dr["CD_PRODUCT"].ToString();
                    //_header.CurrentRow["NM_PRODUCT"] = dr["NM_PRODUCT"].ToString();
                    //bpc제조자.CodeValue = dr["CD_PRODUCT"].ToString();
                    //bpc제조자.CodeName = dr["NM_PRODUCT"].ToString();

                    //_header.CurrentRow["FG_LC"] = dr["FG_LC"].ToString();
                    //cboLC구분.SelectedValue = dr["FG_LC"].ToString();

                    //_header.CurrentRow["CD_EXCH"] = dr["CD_EXCH"].ToString();
                    //cbo통화.SelectedValue = dr["CD_EXCH"].ToString();

                    //_header.CurrentRow["AM_EX"] = Convert.ToDecimal(dr["AM_EX"]);
                    //cur금액.DecimalValue = Convert.ToDecimal(dr["AM_EX"]);

                    //_header.CurrentRow["TP_PACKING"] = dr["TP_PACKING"].ToString();
                    //cbo포장형태.SelectedValue = dr["TP_PACKING"].ToString();

                    //_header.CurrentRow["REMARK1"] = dr["REMARK1"].ToString();
                    //txt비고1.Text = dr["REMARK1"].ToString();

                    //_header.CurrentRow["REMARK2"] = dr["REMARK2"].ToString();
                    //txt비고2.Text = dr["REMARK2"].ToString();

                    //_header.CurrentRow["REMARK3"] = dr["REMARK3"].ToString();
                    //txt비고3.Text = dr["REMARK3"].ToString();

                    #endregion

                    string 참조송장번호 = dlg.Multi참조송장번호;
                    string[] 송장번호 = 참조송장번호.Split(new char[] { '|' });

                    int 송장번호갯수 = int.Parse(송장번호.Length.ToString()) - 1;
                    _header.CurrentRow["NO_INV"] = "[" + 송장번호갯수.ToString() + "]" + "송장번호";
                    tbtn참조송장번호.Text = 참조송장번호.Replace("|", ",").ToString();

                    dtLine = _biz.송장번호Search(참조송장번호);

                    if (dtLine == null) return;
                    else ToolBarSaveButtonEnabled = true;

                    _header.CurrentRow["CD_BIZAREA"] = dtLine.Rows[0]["CD_BIZAREA"].ToString();
                    _header.CurrentRow["NM_BIZAREA"] = dtLine.Rows[0]["NM_BIZAREA"].ToString();
                    bpc사업장.CodeValue = dtLine.Rows[0]["CD_BIZAREA"].ToString();

                    _header.CurrentRow["CD_BIZAREA"] = dtLine.Rows[0]["CD_BIZAREA"].ToString();
                    _header.CurrentRow["NM_BIZAREA"] = dtLine.Rows[0]["NM_BIZAREA"].ToString();
                    bpc사업장.CodeValue = dtLine.Rows[0]["CD_BIZAREA"].ToString();
                    bpc사업장.CodeName = dtLine.Rows[0]["NM_BIZAREA"].ToString();

                    _header.CurrentRow["CD_SALEGRP"] = dtLine.Rows[0]["CD_SALEGRP"].ToString();
                    _header.CurrentRow["NM_SALEGRP"] = dtLine.Rows[0]["NM_SALEGRP"].ToString();
                    bpc영업그룹.CodeValue = dtLine.Rows[0]["CD_SALEGRP"].ToString();
                    bpc영업그룹.CodeName = dtLine.Rows[0]["NM_SALEGRP"].ToString();

                    _header.CurrentRow["NO_EMP"] = dtLine.Rows[0]["NO_EMP"].ToString();
                    _header.CurrentRow["NM_KOR"] = dtLine.Rows[0]["NM_KOR"].ToString();
                    bpc담당자.CodeValue = dtLine.Rows[0]["NO_EMP"].ToString();
                    bpc담당자.CodeName = dtLine.Rows[0]["NM_KOR"].ToString();

                    _header.CurrentRow["CD_PARTNER"] = dtLine.Rows[0]["CD_PARTNER"].ToString();
                    _header.CurrentRow["NM_PARTNER"] = dtLine.Rows[0]["NM_PARTNER"].ToString();
                    bpc거래처.CodeValue = dtLine.Rows[0]["CD_PARTNER"].ToString();
                    bpc거래처.CodeName = dtLine.Rows[0]["NM_PARTNER"].ToString();

                    _header.CurrentRow["CD_AGENT"] = dtLine.Rows[0]["CD_AGENT"].ToString();
                    _header.CurrentRow["NM_AGENT"] = dtLine.Rows[0]["NM_AGENT"].ToString();
                    bpc대행자.CodeValue = dtLine.Rows[0]["CD_AGENT"].ToString();
                    bpc대행자.CodeName = dtLine.Rows[0]["NM_AGENT"].ToString();

                    _header.CurrentRow["CD_EXPORT"] = dtLine.Rows[0]["CD_EXPORT"].ToString();
                    _header.CurrentRow["NM_EXPORT"] = dtLine.Rows[0]["NM_EXPORT"].ToString();
                    bpc수출자.CodeValue = dtLine.Rows[0]["CD_EXPORT"].ToString();
                    bpc수출자.CodeName = dtLine.Rows[0]["NM_EXPORT"].ToString();

                    _header.CurrentRow["CD_PRODUCT"] = dtLine.Rows[0]["CD_PRODUCT"].ToString();
                    _header.CurrentRow["NM_PRODUCT"] = dtLine.Rows[0]["NM_PRODUCT"].ToString();
                    bpc제조자.CodeValue = dtLine.Rows[0]["CD_PRODUCT"].ToString();
                    bpc제조자.CodeName = dtLine.Rows[0]["NM_PRODUCT"].ToString();

                    _header.CurrentRow["FG_LC"] = dtLine.Rows[0]["FG_LC"].ToString();
                    cboLC구분.SelectedValue = dtLine.Rows[0]["FG_LC"].ToString();

                    _header.CurrentRow["CD_EXCH"] = dtLine.Rows[0]["CD_EXCH"].ToString();
                    cbo통화.SelectedValue = dtLine.Rows[0]["CD_EXCH"].ToString();

                    _header.CurrentRow[D.GetString(cbo포장형태.Tag)] = cbo포장형태.SelectedValue = D.GetString(dtLine.Rows[0]["TP_PACKING"]);

                    //금액계산
                    decimal am_ex = 0;

                    for (int i = 0; i < dtLine.Rows.Count; i++)
                    {
                        am_ex += decimal.Parse(dtLine.Rows[i]["AM_EX"].ToString());
                    }

                    _header.CurrentRow["AM_EX"] = Convert.ToDecimal(am_ex);
                    cur금액.DecimalValue = Convert.ToDecimal(am_ex);

                    _header.CurrentRow["AM_FREIGHT"] = D.GetDecimal(dtLine.Rows[0]["AM_FREIGHT"]);
                    cur운임.DecimalValue = D.GetDecimal(dtLine.Rows[0]["AM_FREIGHT"]);

                    _header.CurrentRow["TP_PACKING"] = dtLine.Rows[0]["TP_PACKING"].ToString();
                    cbo포장형태.SelectedValue = dtLine.Rows[0]["TP_PACKING"].ToString();

                    _header.CurrentRow["REMARK1"] = dtLine.Rows[0]["REMARK1"].ToString();
                    txt비고1.Text = dtLine.Rows[0]["REMARK1"].ToString();

                    _header.CurrentRow["REMARK2"] = dtLine.Rows[0]["REMARK2"].ToString();
                    txt비고2.Text = dtLine.Rows[0]["REMARK2"].ToString();

                    _header.CurrentRow["REMARK3"] = dtLine.Rows[0]["REMARK3"].ToString();
                    txt비고3.Text = dtLine.Rows[0]["REMARK3"].ToString();


                    cbo출고구분.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region ★ 기타메소드

        #region -> IsChanged

        protected override bool IsChanged()
        {
            if (base.IsChanged())
                return true;

            DataTable dt = _header.GetChanges();

            if (dt != null && (dt.Rows[0]["NO_TO"].ToString() != "" || dt.Rows[0]["NO_TO"].ToString() != string.Empty))
            {
                return true;
            }

            return false;
        }

        #endregion

        #region -> cbo통화_SelectionChangeCommitted

        private void cbo통화_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                string _환종코드 = this.cbo통화.SelectedValue.ToString();

                if (MA.기준환율.Option != MA.기준환율옵션.적용안함)
                {
                    cur면장환율.DecimalValue = MA.기준환율적용(dtp신고일.Text, _환종코드);
                    _header.CurrentRow["RT_LICENSE"] = cur면장환율.DecimalValue;
                }

                if (_환종코드 == "000")
                {
                    cur면장환율.DecimalValue = 1;
                    _header.CurrentRow["RT_LICENSE"] = 1;

                }

                if (MA.기준환율.Option == MA.기준환율옵션.적용_수정불가 || _환종코드 == "000")
                    cur면장환율.Enabled = false;
                else
                    cur면장환율.Enabled = true;


                /*
                 DataTable dt = Global.MainFrame.FillDataTable("SELECT RATE_BASE FROM MA_EXCHANGE WHERE CD_COMPANY = '" + CompanyCode + "' AND CURR_DEST = '000' AND CURR_SOUR = '" + _환종코드 + "' AND YYMMDD = '" + dtp신고일.Text.ToString() + "' ");

                if (dt != null && dt.Rows.Count > 0)
                {
                    cur면장환율.DecimalValue = decimal.Parse(dt.Rows[0]["RATE_BASE"].ToString());
                }
                else
                {
                    cur면장환율.DecimalValue = 1;
                }
                */
            }
            catch (Exception Ex)
            {
                MsgEnd(Ex);
            }
        }

        #endregion

        private void btn첨부파일_Click(object sender, EventArgs e)
        {
            try
            {

                if (txt신고번호.Text == "")
                {
                    ShowMessageKor("신고번호를 먼저 등록하셔야합니다.");
                    return;
                }


                string cd_file_code = D.GetString(txt신고번호.Text); //파일 PK설정   공장코드_검사성적서번호
                master.P_MA_FILE_SUB m_dlg = new master.P_MA_FILE_SUB("PU", Global.MainFrame.CurrentPageID, cd_file_code);

                //위 내용은 변동없고 아래 내용을 추가해서 포티스에 선반영바랍니다.(서버키 포티스만 동작)
                if (Global.MainFrame.ServerKey == "FORTIS")
                {
                    m_dlg.YnUNC = true;
                    m_dlg.UNCID = "erp";
                    m_dlg.UNCPassword = "erp_disk1223";
                    m_dlg.UNCPath = @"\\192.168.4.235\homes\erp";
                }

                if (m_dlg.ShowDialog(this) == DialogResult.Cancel)
                    return;

            }
            catch (Exception Ex)
            {
                MsgEnd(Ex);
            }
        }

        #endregion
    }
}

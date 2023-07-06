using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

using Duzon.Common.Forms;
using Duzon.Common.Controls;
using Duzon.Common.Util;
using DzHelpFormLib;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;

namespace trade
{
    // **************************************
    // 작   성   자 : 허성철
    // 재 작  성 일 : 2006-11-17
    // 모   듈   명 : 무역
    // 시 스  템 명 : 무역관리
    // 서브시스템명 : 수출
    // 페 이 지  명 : 선적등록
    // 프로젝트  명 : P_TR_EXBL
    // **************************************
    public partial class P_TR_EXBL_OLD2 : PageBase
    {
        #region ★ 멤버필드

        private P_TR_EXBL_BIZ _biz = new P_TR_EXBL_BIZ();
        private FreeBinding _header = null;
        private DataTable _송장내역 = null;

        private string _str선적번호 = "";

        private string CompanyCode = Global.MainFrame.LoginInfo.CompanyCode;

        String _송장첨부CHK설정 = "000";

        #endregion

        #region ★ 초기화

        #region -> 생성자

        public P_TR_EXBL_OLD2()
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

        public P_TR_EXBL_OLD2(string str선적번호)
        {
            try
            {
                InitializeComponent();

                _str선적번호 = str선적번호;

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

            btn매출전표처리.Enabled = false;
            btn매출전표처리취소.Enabled = false;
            _송장첨부CHK설정 = Duzon.ERPU.MF.ComFunc.전용코드("송장등록-첨부파일");
        }

        #endregion

        #region -> InitPaint

        protected override void InitPaint()
        {
            base.InitPaint();

            string[] ls_args = new string[6];
            ls_args[0] = "N;TR_IM00016";// 선적구분
            ls_args[1] = "N;MA_B000005";// 통화
            ls_args[2] = "N;MA_B000020";// 도착국
            ls_args[3] = "N;TR_IM00004";// 결재형태
            ls_args[4] = "N;TR_IM00005";// L/C 구분
            ls_args[5] = "S;TR_IM00003";// 선적조건

            DataSet lds_Combo = (DataSet)GetComboData(ls_args);

            // 선적구분
            this.cbo선적구분.DataSource = lds_Combo.Tables[0];
            this.cbo선적구분.DisplayMember = "NAME";
            this.cbo선적구분.ValueMember = "CODE";

            // 통화
            this.cbo통화.DataSource = lds_Combo.Tables[1];
            this.cbo통화.DisplayMember = "NAME";
            this.cbo통화.ValueMember = "CODE";

            // 도착국
            DataTable _도착국 = Global.MainFrame.FillDataTable("SELECT CD_SYSDEF AS CODE, NM_SYSDEF AS NAME FROM MA_CODEDTL WHERE CD_COMPANY = '" + CompanyCode + "' AND CD_FIELD = 'MA_B000020' ORDER BY NAME ");

            if (_도착국 != null && _도착국.Rows.Count > 0)
            {
                DataRow dr = null;

                dr = _도착국.NewRow();
                dr["CODE"] = "";
                dr["NAME"] = "";
                _도착국.Rows.InsertAt(dr, 0);
            }

            this.cbo도착국.DataSource = _도착국;
            this.cbo도착국.DisplayMember = "NAME";
            this.cbo도착국.ValueMember = "CODE";

            // 결재형태
            this.cbo결제형태.DataSource = lds_Combo.Tables[3];
            this.cbo결제형태.DisplayMember = "NAME";
            this.cbo결제형태.ValueMember = "CODE";

            // L/C 구분
            this.cboLC구분.DataSource = new DataView(lds_Combo.Tables[4], "CODE IN ('003', '004', '005')", "CODE", DataViewRowState.CurrentRows); 
            this.cboLC구분.DisplayMember = "NAME";
            this.cboLC구분.ValueMember = "CODE";

            // 선적 조건
            this.cbo선적조건.DataSource = lds_Combo.Tables[5];
            this.cbo선적조건.DisplayMember = "NAME";
            this.cbo선적조건.ValueMember = "CODE";

            if (MA.기준환율.Option == MA.기준환율옵션.적용_수정불가)
                cur기표환율.Enabled = false;


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
            _header.SetBinding(dt, _tlayMain);
            _header.ClearAndNewRow();


            cbo출고구분.SelectedIndex = 0;
            _header.CurrentRow["YN_RETURN"] = "N";

            // 2011-07-28, 첨부기능 추가함. 최승애
            if (D.GetString(_송장첨부CHK설정) == string.Empty)
                _송장첨부CHK설정 = "000";

            if (_송장첨부CHK설정 == "100")
                btn첨부파일.Visible = true;


            cbo선적구분.SelectedIndex = 0;
            _header.CurrentRow["FG_BL"] = "001";

            // 2012.06.18 최창종 추가
            if (!string.IsNullOrEmpty(_str선적번호))
                this.ReSearch(_str선적번호);

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

            //1.통관등록 라인테이블을 만든다.
            DataTable dtValue = Global.MainFrame.FillDataTable("SELECT * FROM TR_EXBLL WHERE CD_COMPANY = '" + CompanyCode + "' AND NO_BL = '" + this.txt선적번호.Text + "' ");

            DataTable _dtLine = new DataTable();

            _dtLine.Columns.Add("NO_BL", typeof(string));
            _dtLine.Columns.Add("NO_TO", typeof(string));
            _dtLine.Columns.Add("NO_INV", typeof(string));
            _dtLine.Columns.Add("DTS_INSERT", typeof(string));
            _dtLine.Columns.Add("ID_INSERT", typeof(string));
            _dtLine.Columns.Add("DTS_UPDATE", typeof(string));
            _dtLine.Columns.Add("ID_UPDATE", typeof(string));

            if (dtValue == null || dtValue.Rows.Count <= 0)
            {
                DataRow dr = null;

                //2.받아온 참조 송장번호만큼 루프를 돌린다
                for (int i = 0; i < this._송장내역.Rows.Count; i++)
                {
                    dr = _dtLine.NewRow();

                    dr["NO_BL"] = this.txt선적번호.Text;
                    dr["NO_TO"] = _송장내역.Rows[i]["NO_TO"].ToString();
                    dr["NO_INV"] = _송장내역.Rows[i]["NO_INV"].ToString();
                    dr["DTS_INSERT"] = Global.MainFrame.LoginInfo.UserID;
                    dr["ID_INSERT"] = Global.MainFrame.GetStringToday;
                    dr["DTS_UPDATE"] = Global.MainFrame.LoginInfo.UserID;
                    dr["ID_UPDATE"] = Global.MainFrame.GetStringToday;

                    _dtLine.Rows.Add(dr);
                }
            }

            if (dt == null && _dtLine == null)
                return true;

            bool bSuccess = _biz.Save(dt, _dtLine);

            if (!bSuccess)
            {
                return false;
            }
            else
            {
                DataSet _Ds보정값 = _biz.보정값Search(this.txt선적번호.Text);

                if (_Ds보정값.Tables[0] != null && _Ds보정값.Tables[0].Rows.Count > 0)
                {
                    for (int j = 0; j < _Ds보정값.Tables[0].Rows.Count; j++)
                    {
                        string NO_QTIO = _Ds보정값.Tables[0].Rows[j]["NO_QTIO"] == null ? "" : _Ds보정값.Tables[0].Rows[j]["NO_QTIO"].ToString();
                        string NO_LINE_QTIO = _Ds보정값.Tables[0].Rows[j]["NO_LINE_QTIO"] == null ? "" : _Ds보정값.Tables[0].Rows[j]["NO_LINE_QTIO"].ToString();

                        decimal AM_EXSO = 0;
                        if (j == 0)
                        {
                            decimal Origin = _Ds보정값.Tables[0].Rows[j]["AM_EXSO"] == null ? 0 : decimal.Parse(_Ds보정값.Tables[0].Rows[j]["AM_EXSO"].ToString());
                            decimal Modify = _Ds보정값.Tables[1].Rows[0]["AM_CLS"] == null ? 0 : decimal.Parse(_Ds보정값.Tables[1].Rows[0]["AM_CLS"].ToString());
                            AM_EXSO = Origin + Modify;
                        }
                        else
                        {
                            AM_EXSO = _Ds보정값.Tables[0].Rows[j]["AM_EXSO"] == null ? 0 : decimal.Parse(_Ds보정값.Tables[0].Rows[j]["AM_EXSO"].ToString());
                        }

                        decimal QT_INVENT = _Ds보정값.Tables[0].Rows[j]["QT_INVENT"] == null ? 0 : decimal.Parse(_Ds보정값.Tables[0].Rows[j]["QT_INVENT"].ToString());
                        decimal UM_INVENT = _Ds보정값.Tables[0].Rows[j]["UM_INVENT"] == null ? 0 : decimal.Parse(_Ds보정값.Tables[0].Rows[j]["UM_INVENT"].ToString());
                        decimal QT_SO = _Ds보정값.Tables[0].Rows[j]["QT_SO"] == null ? 0 : decimal.Parse(_Ds보정값.Tables[0].Rows[j]["QT_SO"].ToString());

                        _biz.보정값Update(NO_QTIO, NO_LINE_QTIO, AM_EXSO, QT_INVENT, UM_INVENT, QT_SO );
                    }
                }
            }

            _header.AcceptChanges();        // JobModeChanged 이벤트가 발생함. 모드는 "조회후수정"

            return true;
        }

        #endregion

        #region -> BeforeSave

        protected override bool BeforeSave()
        {
            if (txt선적번호.Text == "" || txt선적번호.Text == string.Empty)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, lbl선적번호.Text);
                txt선적번호.Focus();
                return false;
            }
            else if (tbtn참조신고번호.Text == "" || tbtn참조신고번호.Text == string.Empty)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, lbl참조신고번호.Text);
                tbtn참조신고번호.Focus();
                return false;
            }
            else if (cbo선적구분.SelectedValue == DBNull.Value || cbo선적구분.SelectedValue == null || cbo선적구분.SelectedValue.ToString() == "" || cbo선적구분.SelectedValue.ToString() == string.Empty)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, lbl선적구분.Text);
                cbo선적구분.Focus();
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
            else if (dtp기표일자.Text == "" || dtp기표일자.Text == string.Empty)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, lbl기표일자.Text);
                dtp기표일자.Focus();
                return false;
            }
            else if (dtp선적일자.Text == "" || dtp선적일자.Text == string.Empty)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, lbl선적일자.Text);
                dtp선적일자.Focus();
                return false;
            }
            else if (cbo통화.SelectedValue == DBNull.Value || cbo통화.SelectedValue == null || cbo통화.SelectedValue.ToString() == "" || cbo통화.SelectedValue.ToString() == string.Empty)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, lbl통화.Text);
                cbo통화.Focus();
                return false;
            }
            else if (cbo도착국.SelectedValue == DBNull.Value || cbo도착국.SelectedValue == null || cbo도착국.SelectedValue.ToString() == "" || cbo도착국.SelectedValue.ToString() == string.Empty)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, lbl도착국.Text);
                cbo도착국.Focus();
                return false;
            }
            else if (cbo결제형태.SelectedValue == DBNull.Value || cbo결제형태.SelectedValue == null || cbo결제형태.SelectedValue.ToString() == "" || cbo결제형태.SelectedValue.ToString() == string.Empty)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, lbl결제형태.Text);
                cbo결제형태.Focus();
                return false;
            }
            else if (cboLC구분.SelectedValue == DBNull.Value || cboLC구분.SelectedValue == null || cboLC구분.SelectedValue.ToString() == "" || cboLC구분.SelectedValue.ToString() == string.Empty)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, lblLC구분.Text);
                cboLC구분.Focus();
                return false;
            }
            else if (cur기표환율.DecimalValue == 0)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, lbl기표환율.Text);
                cur기표환율.Focus();
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
                if (!base.BeforeSearch()) return;   // BeforeSearch 내부에서 IsChanged 를 호출하므로 변경여부가 판단됨

                P_TR_EXBLNO_SUB dlg = new P_TR_EXBLNO_SUB();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    string _선적번호 = dlg.선적번호;

                    this.ReSearch(_선적번호);
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

                btn매출전표처리.Enabled = false;
                btn매출전표처리취소.Enabled = false;
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

            return true;
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeDelete())
                    return;

                if (_header.CurrentRow["YN_SLIP"].ToString() == "Y")
                {
                    ShowMessage("매출전표가 처리된 상태입니다. 관련 매출전표를 취소후 삭제해 주시기 바랍니다.");
                    return;
                }

                if (_biz.Delete(txt선적번호.Text))
                {
                    ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);

                    _header.ClearAndNewRow();

                    btn매출전표처리.Enabled = false;
                    btn매출전표처리취소.Enabled = false;

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
                if (txt전표처리상태.Text == "Y")
                {
                    this.ShowMessage("이미 전표처리된 선적입니다.");
                    return;
                }

                if (!BeforeSave()) return;

                string _선적번호 = this.txt선적번호.Text;

                DataTable dt1;

                if (Global.MainFrame.DatabaseType == EnumDbType.MSSQL)
                {

                    dt1 = Global.MainFrame.FillDataTable("SELECT TOP 1 NO_DOCU FROM FI_DOCU WHERE CD_COMPANY = '" + CompanyCode + "' AND NO_MDOCU = '" + _선적번호 + "' ");
                }
                else
                {
                    dt1 = Global.MainFrame.FillDataTable("SELECT NO_DOCU FROM FI_DOCU WHERE CD_COMPANY = '" + CompanyCode + "' AND NO_MDOCU = '" + _선적번호 + "' AND ROWNUM = 1 ");
                }

                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    ShowMessage("관련 선적번호로 처리된 전표(@)가 있습니다. 해당 전표에 대하여 선적관리화면에서 확인하십시오.",dt1.Rows[0]["NO_DOCU"].ToString() );
                }

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

        #region -> 선적내역버튼 클릭

        private void btn선적내역_Click(object sender, EventArgs e)
        {
            try
            {
                P_TR_EXBL_SUB dlg = new P_TR_EXBL_SUB(_header.CurrentRow["NO_BL"].ToString(), _header.CurrentRow["NM_PARTNER"].ToString(), _header.CurrentRow["DT_LOADING"].ToString(), Convert.ToDecimal(_header.CurrentRow["RT_EXCH"]), Convert.ToDecimal(_header.CurrentRow["AM_EX"]), Convert.ToDecimal(_header.CurrentRow["AM"]), cbo선적구분.Text, cbo통화.Text);
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

                DataTable dt = _biz.SearchBlBF(this.txt선적번호.Text);

                string[] ls_args = new string[12];
                ls_args[0] = "BL";
                ls_args[1] = txt선적번호.Text;
                ls_args[2] = dt.Rows[0]["DT_BALLOT"].ToString();
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

                this.LoadPageFrom("P_TR_EXCOST", DD("INPUT_COST"), Grant, ls_args);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 매출전표처리

        private void btn매출전표처리_Click(object sender, EventArgs e)
        {
            try
            {
                string _선적번호 = this.txt선적번호.Text;
                //DataTable dt1 = Global.MainFrame.FillDataTable("SELECT TOP 1 NO_DOCU FROM FI_DOCU WHERE CD_COMPANY = '" + CompanyCode + "' AND CD_MNG = '" + _선적번호 + "' ");

                DataTable dt1;

                if (Global.MainFrame.DatabaseType == EnumDbType.MSSQL)
                {
                    dt1 = Global.MainFrame.FillDataTable("SELECT TOP 1 NO_DOCU FROM FI_DOCU WHERE CD_COMPANY = '" + CompanyCode + "' AND NO_MDOCU = '" + _선적번호 + "' ");
                }
                else
                {
                    dt1 = Global.MainFrame.FillDataTable("SELECT NO_DOCU FROM FI_DOCU WHERE CD_COMPANY = '" + CompanyCode + "' AND NO_MDOCU = '" + _선적번호 + "' AND ROWNUM = 1 ");
                }

                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    ShowMessage("관련 선적번호로 처리된 전표(@)가 있습니다. 해당 전표에 대하여 확인하십시오.", dt1.Rows[0]["NO_DOCU"].ToString() );
                }

                else
                {
                    bool iResult = _biz.미결전표처리(this.txt선적번호.Text);

                    if (iResult)
                    {
                        ShowMessage("해당 선적건이 정상적으로 전표처리 되었습니다.");
                        this.ReSearch(this.txt선적번호.Text);

                        this.btn매출전표처리취소.Enabled = true;
                        this.btn매출전표처리.Enabled = false;
                    }
                    else
                    {
                        ShowMessage("해당 선적건이 정상적으로 전표처리 되지 않았습니다. 데이터를 확인하십시오.");
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 매출전표처리취소

        private void btn매출전표처리취소_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global.MainFrame.ServerKey == "FEELUX") //필룩스버튼권한없애달라는요청최면환팀장
                {
                    this.ShowMessage("전표처리취소버튼은사용하실수없습니다. 회계에서삭제가능합니다.");
                    return;
                }

                string 선적번호 = _header.CurrentRow["NO_BL"].ToString();

                DataTable dt1;

                // 전표처리되었는지 체크하는 로직 수정. 2011.03.16 장은경               
                if (Global.MainFrame.DatabaseType == EnumDbType.MSSQL)
                {
                    dt1 = Global.MainFrame.FillDataTable("SELECT TOP 1 CD_MNG FROM FI_DOCU WHERE CD_COMPANY = '" + CompanyCode + "' AND NO_MDOCU = '" + 선적번호 + "' ");
                }
                else
                {
                    dt1 = Global.MainFrame.FillDataTable("SELECT  CD_MNG FROM FI_DOCU WHERE CD_COMPANY = '" + CompanyCode + "' AND NO_MDOCU = '" + 선적번호 + "' AND ROWNUM = 1 ");
                }

                if (dt1.Rows.Count == 0)
                {
                    this.ShowMessage("해당 선적번호로 처리된 전표가 존재하지 않습니다.");
                    return;
                }

                bool 전표취소성공여부 = _biz.미결전표취소(선적번호);

                if (전표취소성공여부 == false)
                {
                    this.ShowMessage("선적번호 = @전표처리가 정상적으로 취소되지 않았습니다.", DD(선적번호) ,"IK1");		// 전표처리중 오류가 발생하였습니다.			
                    return;
                }

                Global.MainFrame.ShowMessage("취소작업을 완료하였습니다");
                this.ReSearch(this.txt선적번호.Text);

                this.btn매출전표처리.Enabled = true;
                this.btn매출전표처리취소.Enabled = false;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> cbo통화_SelectionChangeCommitted

        private void cbo통화_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                decimal ld기표환율 = 1;

                if (MA.기준환율.Option != MA.기준환율옵션.적용안함)
                {
                    // 기준일자 : 환율을 적용하는 일자
                    ld기표환율 = MA.기준환율적용(dtp기표일자.Text, cbo통화.SelectedValue.ToString());
                }

                if (cbo통화.SelectedValue.ToString() == "000")
                {
                    ld기표환율 = 1;
                    cur기표환율.Enabled = false;
                }
                else
                {
                    if (MA.기준환율.Option == MA.기준환율옵션.적용_수정불가)
                        cur기표환율.Enabled = false;
                    else
                        cur기표환율.Enabled = true;
                }

                cur기표환율.DecimalValue = ld기표환율;
                cur기표금액.DecimalValue = Decimal.Parse(cur기표환율.DecimalValue.ToString()) * Decimal.Parse(cur외화금액.DecimalValue.ToString());

                _header.CurrentRow["RT_EXCH"] = ld기표환율;
                _header.CurrentRow["AM"] = Decimal.Parse(cur기표환율.DecimalValue.ToString()) * Decimal.Parse(cur외화금액.DecimalValue.ToString());

                
                /*
                string _환종코드 = this.cbo통화.SelectedValue.ToString();

                DataTable dt = Global.MainFrame.FillDataTable("SELECT RATE_BASE FROM MA_EXCHANGE WHERE CD_COMPANY = '" + CompanyCode + "' AND CURR_DEST = '000' AND CURR_SOUR = '" + _환종코드 + "' AND YYMMDD = '" + dtp기표일자.Text + "' ");

                if (dt != null && dt.Rows.Count > 0)
                {
                    cur기표환율.DecimalValue = decimal.Parse(dt.Rows[0]["RATE_BASE"].ToString());
                    _header.CurrentRow["RT_EXCH"] = decimal.Parse(dt.Rows[0]["RATE_BASE"].ToString());

                    _header.CurrentRow["AM"] = Decimal.Parse(cur기표환율.DecimalValue.ToString()) * Decimal.Parse(cur외화금액.DecimalValue.ToString());
                    cur기표금액.DecimalValue = Decimal.Parse(cur기표환율.DecimalValue.ToString()) * Decimal.Parse(cur외화금액.DecimalValue.ToString());
                }
                else
                {
                    cur기표환율.DecimalValue = 1;

                    _header.CurrentRow["AM"] = Decimal.Parse(cur외화금액.DecimalValue.ToString());
                    cur기표금액.DecimalValue = Decimal.Parse(cur외화금액.DecimalValue.ToString());
                }*/
            }
            catch (Exception Ex)
            {
                MsgEnd(Ex);
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
                    case "txt선적번호":
                        if (txt선적번호.Text != "" && _header.JobMode == JobModeEnum.추가후수정)
                            ToolBarSaveButtonEnabled = true;
                        break;
                    case "cur기표환율":
                        decimal _기표금액1 = cur기표환율.DecimalValue * cur외화금액.DecimalValue;
                        //cur기표금액.DecimalValue = decimal.Truncate(_기표금액1);
                        cur기표금액.DecimalValue = Unit.원화금액(DataDictionaryTypes.TR, _기표금액1);
                        _header.CurrentRow["AM"] = cur기표금액.DecimalValue;
                        _header.CurrentRow["RT_EXCH"] = cur기표환율.DecimalValue;
                        ToolBarSaveButtonEnabled = true;
                        break;
                    case "cur외화금액":
                        decimal _기표금액2 = cur외화금액.DecimalValue * cur기표환율.DecimalValue;
                        //cur기표금액.DecimalValue = decimal.Truncate(_기표금액2);
                        cur기표금액.DecimalValue = Unit.원화금액(DataDictionaryTypes.TR, _기표금액2);
                        _header.CurrentRow["AM"] = cur기표금액.DecimalValue;
                        _header.CurrentRow["AM_EX"] = cur외화금액.DecimalValue;
                        ToolBarSaveButtonEnabled = true;
                        break;
                    case "dtp기표일자":
                        if (this.bpc거래처.CodeName.ToString() != "")
                        {
                            string _거래처코드 = this.bpc거래처.CodeValue.ToString();

                            DataTable dt;

                            if (Global.MainFrame.DatabaseType == EnumDbType.MSSQL)
                            {
                                dt = Global.MainFrame.FillDataTable("SELECT TOP 1 * FROM MA_PARTNER WHERE CD_COMPANY = '" + CompanyCode + "' AND CD_PARTNER = '" + _거래처코드 + "' ");
                            }
                            else
                            {
                                dt = Global.MainFrame.FillDataTable("SELECT * FROM MA_PARTNER WHERE CD_COMPANY = '" + CompanyCode + "' AND CD_PARTNER = '" + _거래처코드 + "' AND ROWNUM = 1 ");
                            }

                            int AddDay = dt.Rows[0]["DT_RCP_PREARRANGED"] == null || dt.Rows[0]["DT_RCP_PREARRANGED"].ToString() == "" ? 0 : int.Parse(dt.Rows[0]["DT_RCP_PREARRANGED"].ToString());

                            if (AddDay > 0)
                            {
                                this.dtp결제만기일.Value = this.dtp기표일자.Value.AddDays(AddDay);
                                _header.CurrentRow["DT_PAYABLE"] = this.dtp결제만기일.Text;
                            }
                        }
                        else
                        {
                            _header.CurrentRow["DT_BALLOT"] = this.dtp기표일자.Text;
                        }
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
                    txt선적번호.Enabled = true;
                    txt선적번호.Focus();
                    tbtn참조신고번호.Enabled = true;

                    btn매출전표처리.Enabled = false;
                    btn판매경비등록.Enabled = false;
                    btn선적내역.Enabled = false;

                    ToolBarAddButtonEnabled = true;
                    ToolBarDeleteButtonEnabled = false;
                    ToolBarSaveButtonEnabled = false;

                    string _선적번호 = txt선적번호.Text;
                    if (_선적번호 != "")
                    {
                        DataTable dt = Global.MainFrame.FillDataTable("SELECT YN_SLIP FROM TR_EXBL WHERE CD_COMPANY = '" + CompanyCode + "' AND NO_BL = '" + _선적번호 + "' ");

                        if (dt.Rows[0]["YN_SLIP"].ToString() == "Y")
                        {
                            this.btn매출전표처리취소.Enabled = true;
                            this.btn매출전표처리.Enabled = false;
                        }
                        else
                        {
                            this.btn매출전표처리취소.Enabled = false;
                            this.btn매출전표처리.Enabled = true;
                        }
                    }

                    cbo출고구분.Enabled = true;
                }
                else
                {
                    txt선적번호.Enabled = false;
                    tbtn참조신고번호.Enabled = false;
                    btn판매경비등록.Enabled = true;
                    btn선적내역.Enabled = true;

                    ToolBarAddButtonEnabled = true;
                    ToolBarDeleteButtonEnabled = true;
                    ToolBarSaveButtonEnabled = false;

                    string _선적번호 = txt선적번호.Text;
                    if (_선적번호 != "")
                    {
                        DataTable dt = Global.MainFrame.FillDataTable("SELECT YN_SLIP FROM TR_EXBL WHERE CD_COMPANY = '" + CompanyCode + "' AND NO_BL = '" + _선적번호 + "' ");

                        if (dt.Rows[0]["YN_SLIP"].ToString() == "Y")
                        {
                            this.btn매출전표처리취소.Enabled = true;
                            this.btn매출전표처리.Enabled = false;
                        }
                        else
                        {
                            this.btn매출전표처리취소.Enabled = false;
                            this.btn매출전표처리.Enabled = true;
                        }
                    }

                    cbo출고구분.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        #region -> 참조신고번호 클릭

        private void tbtn참조신고번호_Search(object sender, SearchEventArgs e)
        {
            try
            {
                P_TR_EXBLREF_SUB dlg = new P_TR_EXBLREF_SUB();
                dlg.T_거래처CODE = bpc거래처.CodeValue.ToString();
                dlg.T_거래처NAME = bpc거래처.CodeName.ToString();
                dlg.T_출고구분 = cbo출고구분.SelectedValue.ToString();

                if (cboLC구분.SelectedValue != null)
                {
                    dlg.T_LC구분 = cboLC구분.SelectedValue.ToString();
                    
                }
                if (cbo통화.SelectedValue != null)
                {
                    dlg.T_통화 = cbo통화.SelectedValue.ToString();
                }

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    string 참조신고내역 = dlg.Multi참조신고내역;
                    //1. 받아온 참조신고내역을 배열로 만든다.
                    string[] 참조번호 = 참조신고내역.Split(new char[] { '|' });

                    if (참조신고내역 == null || 참조신고내역 == "") return;
                    else ToolBarSaveButtonEnabled = true;

                    //2. 배열로만들어놓은 참조신고내역의 개수를 헤더에 넣고
                    //   보여질 참조신고내역은 |를 ,로 치환시켜 텍스트박스에 뿌려준다.
                    int 참조번호갯수 = int.Parse(참조번호.Length.ToString()) - 1;
                    _header.CurrentRow["NO_TO"] = "[" + 참조번호갯수.ToString() + "]" + "참조번호";
                    tbtn참조신고번호.Text = 참조신고내역.Replace("|", ",").ToString();

                    //3. 송장내역을 조회하여 송장번호와 나머지 데이터를 가져온다.
                    _송장내역 = _biz.송장번호Search(참조신고내역);
                    string _송장번호 = string.Empty;

                    if (_송장내역 != null || _송장내역.Rows.Count > 0)
                    {
                        for (int i = 0; i < _송장내역.Rows.Count; i++)
                        {
                            _송장번호 += _송장내역.Rows[i]["NO_INV"].ToString() + ",";
                        }

                        _header.CurrentRow["NO_INV"] = "[" + _송장내역.Rows.Count.ToString() + "]" + _송장번호;
                        txt송장번호.Text = _송장번호.ToString();

                        //4. 공통적으로 뿌려질 사항들을 컨트롤에 넣어준다.
                        _header.CurrentRow["CD_BIZAREA"] = _송장내역.Rows[0]["CD_BIZAREA"].ToString();
                        _header.CurrentRow["NM_BIZAREA"] = _송장내역.Rows[0]["NM_BIZAREA"].ToString();
                        bpc사업장.CodeValue = _송장내역.Rows[0]["CD_BIZAREA"].ToString();
                        bpc사업장.CodeName = _송장내역.Rows[0]["NM_BIZAREA"].ToString();

                        _header.CurrentRow["CD_SALEGRP"] = _송장내역.Rows[0]["CD_SALEGRP"].ToString();
                        _header.CurrentRow["NM_SALEGRP"] = _송장내역.Rows[0]["NM_SALEGRP"].ToString();
                        bpc영업그룹.CodeValue = _송장내역.Rows[0]["CD_SALEGRP"].ToString();
                        bpc영업그룹.CodeName = _송장내역.Rows[0]["NM_SALEGRP"].ToString();

                        _header.CurrentRow["NO_EMP"] = _송장내역.Rows[0]["NO_EMP"].ToString();
                        _header.CurrentRow["NM_KOR"] = _송장내역.Rows[0]["NM_KOR"].ToString();
                        bpc담당자.CodeValue = _송장내역.Rows[0]["NO_EMP"].ToString();
                        bpc담당자.CodeName = _송장내역.Rows[0]["NM_KOR"].ToString();

                        _header.CurrentRow["CD_PARTNER"] = _송장내역.Rows[0]["CD_PARTNER"].ToString();
                        _header.CurrentRow["NM_PARTNER"] = _송장내역.Rows[0]["NM_PARTNER"].ToString();
                        bpc거래처.CodeValue = _송장내역.Rows[0]["CD_PARTNER"].ToString();
                        bpc거래처.CodeName = _송장내역.Rows[0]["NM_PARTNER"].ToString();

                        _header.CurrentRow["CD_EXPORT"] = _송장내역.Rows[0]["CD_EXPORT"].ToString();
                        _header.CurrentRow["NM_EXPORT"] = _송장내역.Rows[0]["NM_EXPORT"].ToString();
                        bpc수출자.CodeValue = _송장내역.Rows[0]["CD_EXPORT"].ToString();
                        bpc수출자.CodeName = _송장내역.Rows[0]["NM_EXPORT"].ToString();

                        _header.CurrentRow["FG_LC"] = _송장내역.Rows[0]["FG_LC"].ToString();
                        cboLC구분.SelectedValue = _송장내역.Rows[0]["FG_LC"].ToString();

                        _header.CurrentRow["CD_EXCH"] = _송장내역.Rows[0]["CD_EXCH"].ToString();
                        cbo통화.SelectedValue = _송장내역.Rows[0]["CD_EXCH"].ToString();

                        _header.CurrentRow["RT_EXCH"] = cur기표환율.DecimalValue = D.GetDecimal(_송장내역.Rows[0]["RT_EXCH"]);

                        _header.CurrentRow["PORT_LOADING"] = _송장내역.Rows[0]["PORT_LOADING"].ToString();
                        txt선적지.Text = _송장내역.Rows[0]["PORT_LOADING"].ToString();

                        _header.CurrentRow["PORT_ARRIVER"] = _송장내역.Rows[0]["PORT_ARRIVER"].ToString();
                        txt도착지.Text = _송장내역.Rows[0]["PORT_ARRIVER"].ToString();

                        _header.CurrentRow["REMARK1"] = _송장내역.Rows[0]["REMARK"].ToString();
                        txt비고1.Text = _송장내역.Rows[0]["REMARK"].ToString();

                        _header.CurrentRow["REMARK2"] = _송장내역.Rows[0]["TXT_REMARK2"].ToString();
                        txt비고2.Text = _송장내역.Rows[0]["TXT_REMARK2"].ToString();

                        //외화금액, 기표금액 계산
                        decimal am_ex = 0;
                        decimal am = 0;

                        for (int i = 0; i < _송장내역.Rows.Count; i++)
                        {
                            am_ex += decimal.Parse(_송장내역.Rows[i]["AM_EX"].ToString());
                            //am += decimal.Parse(_송장내역.Rows[i]["AM"].ToString());
                            am += decimal.Parse(_송장내역.Rows[i]["AM"].ToString());
                        }

                        _header.CurrentRow["AM_EX"] = Convert.ToDecimal(am_ex);
                        cur외화금액.DecimalValue = Convert.ToDecimal(am_ex);

                        //_header.CurrentRow["AM"] = decimal.Truncate(am);
                        //_header.CurrentRow["AM"] = Unit.원화금액(DataDictionaryTypes.TR, am);
                        //20120116 변경 여러 통관을 한번에 적용시 임의 첫번째통관 환율로만 원화금액계산하면 이격이 발생함.->조치
                        _header.CurrentRow["AM"] = Unit.원화금액(DataDictionaryTypes.TR, am_ex * cur기표환율.DecimalValue );
                        //cur기표금액.DecimalValue = decimal.Truncate(am);
                        //cur기표금액.DecimalValue = Unit.원화금액(DataDictionaryTypes.TR, am);
                        cur기표금액.DecimalValue = Unit.원화금액(DataDictionaryTypes.TR, am_ex * cur기표환율.DecimalValue);

                        string _거래처코드 = _송장내역.Rows[0]["CD_PARTNER"].ToString();

                        DataTable dt;

                        if (Global.MainFrame.DatabaseType == EnumDbType.MSSQL)
                        {
                            dt = Global.MainFrame.FillDataTable("SELECT TOP 1 * FROM MA_PARTNER WHERE CD_COMPANY = '" + CompanyCode + "' AND CD_PARTNER = '" + _거래처코드 + "' ");
                        }
                        else
                        {
                            dt = Global.MainFrame.FillDataTable("SELECT * FROM MA_PARTNER WHERE CD_COMPANY = '" + CompanyCode + "' AND CD_PARTNER = '" + _거래처코드 + "' AND ROWNUM = 1");
                        }

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int AddDay = dt.Rows[0]["DT_RCP_PREARRANGED"] == null || dt.Rows[0]["DT_RCP_PREARRANGED"].ToString() == "" ? 0 : int.Parse(dt.Rows[0]["DT_RCP_PREARRANGED"].ToString());

                            if (AddDay > 0)
                            {
                                this.dtp결제만기일.Value = this.dtp기표일자.Value.AddDays(AddDay);
                                _header.CurrentRow["DT_PAYABLE"] = this.dtp결제만기일.Text;
                            }
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

        #region -> bpc거래처_QueryAfter

        private void bpc거래처_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                switch (e.HelpID)
                {
                    case Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB:
                        string _거래처코드 = e.HelpReturn.Rows[0]["CD_PARTNER"].ToString();

                        DataTable dt;

                        if (Global.MainFrame.DatabaseType == EnumDbType.MSSQL)
                        {
                            dt = Global.MainFrame.FillDataTable("SELECT TOP 1 * FROM MA_PARTNER WHERE CD_COMPANY = '" + CompanyCode + "' AND CD_PARTNER = '" + _거래처코드 + "' ");
                        }
                        else
                        {
                            dt = Global.MainFrame.FillDataTable("SELECT * FROM MA_PARTNER WHERE CD_COMPANY = '" + CompanyCode + "' AND CD_PARTNER = '" + _거래처코드 + "' AND ROWNUM = 1");
                        }

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int AddDay = dt.Rows[0]["DT_RCP_PREARRANGED"] == null || dt.Rows[0]["DT_RCP_PREARRANGED"].ToString() == "" ? 0 : int.Parse(dt.Rows[0]["DT_RCP_PREARRANGED"].ToString());

                            if (AddDay > 0)
                            {
                                this.dtp결제만기일.Value = this.dtp기표일자.Value.AddDays(AddDay);
                                _header.CurrentRow["DT_PAYABLE"] = this.dtp결제만기일.Text;
                            }
                        }
                    break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion


        #region -> txt전표번호_MouseDoubleClick

        private void txt전표번호_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                string strNoDcu = txt전표번호.Text;
                if (strNoDcu != string.Empty)
                {
                    object[] args = {
                                   strNoDcu, //-- 전표번호
                                   "1", //-- 회계번호(모르면1)
                                   "", //-- 회계단위
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

        #region ★ 기타메소드

        #region -> IsChanged

        protected override bool IsChanged()
        {
            if (base.IsChanged())
                return true;

            DataTable dt = _header.GetChanges();

            if (dt != null && (dt.Rows[0]["NO_BL"].ToString() != "" || dt.Rows[0]["NO_BL"].ToString() != string.Empty))
            {
                return true;
            }

            return false;
        }

        #endregion

        #region -> ReSearch()

        private void ReSearch(string _선적번호)
        {
            try
            {
                DataTable dt = _biz.Search(_선적번호);
                _header.SetDataTable(dt);       // JobModeChanged 이벤트가 발생됨

                DataTable dtNO_TO = Global.MainFrame.FillDataTable("SELECT DISTINCT NO_TO FROM TR_EXBLL WHERE CD_COMPANY = '" + CompanyCode + "' AND NO_BL = '" + _선적번호 + "' ");

                if (dtNO_TO != null && dtNO_TO.Rows.Count > 0)
                {
                    string strNO_TO = string.Empty;

                    for (int i = 0; i < dtNO_TO.Rows.Count; i++)
                    {
                        strNO_TO += dtNO_TO.Rows[i]["NO_TO"].ToString() + ",";
                    }

                    this.tbtn참조신고번호.Text = strNO_TO;
                }

                DataTable dtNO_INV = Global.MainFrame.FillDataTable("SELECT * FROM TR_EXBLL WHERE CD_COMPANY = '" + CompanyCode + "' AND NO_BL = '" + _선적번호 + "' ");

                if (dtNO_INV != null && dtNO_INV.Rows.Count > 0)
                {
                    string strNO_INV = string.Empty;

                    for (int i = 0; i < dtNO_INV.Rows.Count; i++)
                    {
                        strNO_INV += dtNO_INV.Rows[i]["NO_INV"].ToString() + ",";
                    }

                    this.txt송장번호.Text = strNO_INV;
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["YN_SLIP"].ToString() == "Y")
                    {
                        btn매출전표처리.Enabled = false;
                        btn매출전표처리취소.Enabled = true;
                    }
                    else if (dt.Rows[0]["YN_SLIP"].ToString() == "N")
                    {
                        btn매출전표처리.Enabled = true;
                        btn매출전표처리취소.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        #region -> 기표금액 변경시 200원이상 차이가 나는것을 방지하는 이벤트(cur기표금액_Validated)

        private void cur기표금액_Validated(object sender, EventArgs e)
        {
            try
            {
                decimal d원화절대값 = Math.Abs(cur기표환율.DecimalValue * cur외화금액.DecimalValue);
                // 2012.02.29 D20120222041 최창종 수정 - 기표금액 수정 시 허용범위 변경 ( 100원 -> 200원)
                if (Math.Abs(d원화절대값 - Math.Abs(cur기표금액.DecimalValue)) > 200)
                {
                    ShowMessage("200원이상 차이가나게 수정할 수 없습니다.");
                    cur기표금액.DecimalValue = cur기표환율.DecimalValue * cur외화금액.DecimalValue;
                    return;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        private void btn첨부파일_Click(object sender, EventArgs e)
        {
            try
            {

                if (txt선적번호.Text == "")
                {
                    ShowMessageKor("선적번호를 먼저 등록하셔야합니다.");
                    return;
                }


                string cd_file_code = D.GetString(txt선적번호.Text); //파일 PK설정   공장코드_검사성적서번호
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

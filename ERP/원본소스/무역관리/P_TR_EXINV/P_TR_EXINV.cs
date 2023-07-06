using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using DzHelpFormLib;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using System.Collections;
using Duzon.ERPU;

namespace trade
{
    // **************************************
    // 작   성   자 : 허성철
    // 재 작  성 일 : 2006-11-13
    // 모   듈   명 : 무역
    // 시 스  템 명 : 무역관리
    // 서브시스템명 : 수출
    // 페 이 지  명 : 송장작성
    // 프로젝트  명 : P_TR_EXINV
    // 2013.03.14 : D20130214072 : 소문자로 key in 입력하여 거래처를 불러올 경우 거래처 정보관리에 등록된 거래처가 대문자로 등록되어있을 때거래처 코드란을 대문자로 변경하도록 함 
    // **************************************
    public partial class P_TR_EXINV : Duzon.Common.Forms.PageBase
    {
        #region ★ 멤버필드

        private P_TR_EXINV_BIZ _biz = new P_TR_EXINV_BIZ();
        private FreeBinding _header = null;

        private static string CompanyCode = Global.MainFrame.LoginInfo.CompanyCode;

        String _송장첨부CHK설정 = "000";


        #endregion

        #region ★ 초기화

        #region -> 생성자

        public P_TR_EXINV()
        {
            try
            {
                InitializeComponent();
                _header = new FreeBinding();
                _header.JobModeChanged += new FreeBindingEventHandler(_header_JobModeChanged);
                _header.ControlValueChanged += new FreeBindingEventHandler(_header_ControlValueChanged);

                DataChanged += new EventHandler(Page_DataChanged);

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
            InitEvent();
            _송장첨부CHK설정 = Duzon.ERPU.MF.ComFunc.전용코드("송장등록-첨부파일");

        }

        #endregion

        #region -> InitEvent

        protected void InitEvent()
        {
            ctx거래처.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(ctx거래처_QueryAfter);
            ctx수출자.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(ctx수출자_QueryAfter);
            ctx수하인.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(ctx수하인_QueryAfter);
            ctx착하통지처.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(ctx착하통지처_QueryAfter);
        }

        #endregion

        #region -> InitPaint

        protected override void InitPaint()
        {
            base.InitPaint();

            //	* 셋팅할 Type 지정
            //	1. N : 공백없는 내용
            //	2. S : 공백있는 내용
            //	3. U : 사용자 정의

            string[] ls_args = new string[9];
            ls_args[0] = "N;PU_C000016";// 거래구분
            ls_args[1] = "N;MA_B000005";// 통화
            ls_args[2] = "N;TR_IM00009";// 운송형태
            ls_args[3] = "S;MA_B000004";// 중량단위?????
            ls_args[4] = "N;TR_IM00008";// 운송방법
            ls_args[5] = "S;MA_B000020";// 원산지
            ls_args[6] = "S;TR_IM00011";// 포장형태
            ls_args[7] = "S;TR_IM00002";// 가격조건
            ls_args[8] = "S;TR_IM00027";// 운임구분

            DataSet lds_Combo = GetComboData(ls_args);

            // 거래구분
            cbo거래구분.DataSource = new DataView(lds_Combo.Tables[0], "CODE IN ('004', '005')", "CODE", DataViewRowState.CurrentRows); 
            cbo거래구분.DisplayMember = "NAME";
            cbo거래구분.ValueMember = "CODE";

            // 통화
            cbo통화.DataSource = lds_Combo.Tables[1];
            cbo통화.DisplayMember = "NAME";
            cbo통화.ValueMember = "CODE";

            // 운송형태
            cbo운송형태.DataSource = lds_Combo.Tables[2];
            cbo운송형태.DisplayMember = "NAME";
            cbo운송형태.ValueMember = "CODE";

            // 중량단위
            cbo중량단위.DataSource = lds_Combo.Tables[3];
            cbo중량단위.DisplayMember = "NAME";
            cbo중량단위.ValueMember = "CODE";

            // 운송방법
            cbo운송방법.DataSource = lds_Combo.Tables[4];
            cbo운송방법.DisplayMember = "NAME";
            cbo운송방법.ValueMember = "CODE";

            // 원산지
            DataTable _국가 = Global.MainFrame.FillDataTable("SELECT CD_SYSDEF AS CODE, NM_SYSDEF AS NAME FROM MA_CODEDTL WHERE CD_COMPANY = '" + CompanyCode + "' AND CD_FIELD = 'MA_B000020' ORDER BY NAME ");

            if (_국가 != null && _국가.Rows.Count > 0)
            {
                DataRow dr = null;

                dr = _국가.NewRow();
                dr["CODE"] = "";
                dr["NAME"] = "";
                _국가.Rows.InsertAt(dr, 0);
            }

            cbo원산지.DataSource = _국가;
            cbo원산지.DisplayMember = "NAME";
            cbo원산지.ValueMember = "CODE";

            // 포장형태
            cbo포장형태.DataSource = lds_Combo.Tables[6];
            cbo포장형태.DisplayMember = "NAME";
            cbo포장형태.ValueMember = "CODE";

            // 가격조건
            cbo가격조건.DataSource = lds_Combo.Tables[7];
            cbo가격조건.DisplayMember = "NAME";
            cbo가격조건.ValueMember = "CODE";

            // 운임구분
            cbo운임구분.DataSource = lds_Combo.Tables[8];
            cbo운임구분.DisplayMember = "NAME";
            cbo운임구분.ValueMember = "CODE";


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

            cbo거래구분.SelectedIndex = Settings.Default.거래구분;
            _header.CurrentRow["FG_LC"] = D.GetString(cbo거래구분.SelectedValue);

            cbo출고구분.SelectedIndex = 0;
            _header.CurrentRow["YN_RETURN"] = "N";


            // 2011-07-28, 첨부기능 추가함. 최승애
            if (D.GetString(_송장첨부CHK설정) == string.Empty)
                _송장첨부CHK설정 = "000";

            if (_송장첨부CHK설정 == "100") 
                btn첨부파일.Visible = true;





        }

        #endregion

        #region -> Page_DataChanged

        void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                switch (_header.CurrentRow.RowState)
                {
                    case DataRowState.Modified:
                        ToolBarSaveButtonEnabled = true;
                        ToolBarDeleteButtonEnabled = true;
                        break;
                    case DataRowState.Unchanged:
                        ToolBarSaveButtonEnabled = false;
                        ToolBarDeleteButtonEnabled = true;
                        break;
                    case DataRowState.Added:
                        ToolBarSaveButtonEnabled = true;
                        ToolBarDeleteButtonEnabled = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> _header_ControlValueChanged

        void _header_ControlValueChanged(object sender, FreeBindingArgs e)
        {
            try
            {
                Page_DataChanged(null, null);

                //switch (((Control)sender).Name)
                //{
                //    case "txt송장번호":
                //        if (txt송장번호.Text != "" && _header.JobMode == JobModeEnum.추가후수정)
                //            ToolBarSaveButtonEnabled = true;
                //        break;
                //    //case "cbo운송방법":
                //    //    _header.CurrentRow["TP_TRANS"] = cbo운송방법.SelectedValue.ToString();
                //    //break;

                //    //case "cbo가격조건":
                //    //    _header.CurrentRow["COND_PRICE"] = cbo가격조건.SelectedValue.ToString();
                //    //break;
                //    //default:
                //    //    ToolBarSaveButtonEnabled = true;
                //    //    break;
                //}
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> _header_JobModeChanged

        void _header_JobModeChanged(object sender, FreeBindingArgs e)
        {
            try
            {
                Page_DataChanged(null, null);

                if (e.JobMode == JobModeEnum.추가후수정)
                {
                    txt송장번호.Enabled = true;
                    txt송장번호.Focus();

                    btn판매경비등록.Enabled = false;
                    btn내역등록.Enabled = false;


                    cbo출고구분.Enabled = true;
                    ToolBarAddButtonEnabled = true;
                }
                else
                {
                    txt송장번호.Enabled = false;

                    btn판매경비등록.Enabled = true;
                    btn내역등록.Enabled = true;

                    cbo출고구분.Enabled = false;
                    ToolBarAddButtonEnabled = true;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region ★ 저장관련메소드

        #region -> SaveData

        protected override bool SaveData()
        {
            if (!base.SaveData())
                return false;

            if (_header.JobMode == JobModeEnum.추가후수정)
            {
                if (_header.CurrentRow["NO_INV"].ToString().Trim() == "")
                {
                    string 송장번호 = (string)GetSeq(Global.MainFrame.LoginInfo.CompanyCode, "TRE", "05", _header.CurrentRow["DT_BALLOT"].ToString().Substring(0, 6));

                    _header.CurrentRow["NO_INV"] = 송장번호;
                    txt송장번호.Text = 송장번호;
                }
                else
                {
                    bool bTrue = _biz.IS_NO_INV_EXIST(_header.CurrentRow["NO_INV"].ToString());
                    if (bTrue)
                    {
                        ShowMessage("송장번호 (" + _header.CurrentRow["NO_INV"].ToString() + ") 로 등록된 번호가 있습니다.");
                        return false;
                    }
                }
            }

            DataTable dt = _header.GetChanges();

            if (dt == null)
                return true;

            bool bSuccess = _biz.Save(dt);

            if (!bSuccess)
                return false;

            _header.AcceptChanges();        // JobModeChanged 이벤트가 발생함. 모드는 "조회후수정"
            btn내역등록.Enabled = true;

            return true;
        }

        #endregion

        #region -> BeforeSave

        protected override bool BeforeSave()
        {
            Hashtable hList = new Hashtable();

            //hList.Add(txt송장번호, lbl송장번호);
            hList.Add(dtp발행일자, lbl발행일자);
            hList.Add(txt거래처, lbl거래처);
            hList.Add(cbo거래구분, lbl거래구분);
            hList.Add(bbpc영업그룹, lbl영업그룹);
            hList.Add(bpc사업장, lbl사업장);
            hList.Add(bpc담당자, lbl담당자);
            hList.Add(cbo통화, lbl통화);
            hList.Add(dtp통관예정일, lbl통관예정일);
            hList.Add(cbo운송형태, lbl운송형태);
            hList.Add(cbo운송방법, lbl운송방법);

            return ComFunc.NullCheck(hList);
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

                string 송장참조여부 = "001";

                P_TR_EXINVNO_SUB dlg = new P_TR_EXINVNO_SUB(송장참조여부);
                if (dlg.ShowDialog() == DialogResult.OK)
                {

                    if (dlg.적용구분 == "복사") //추가 20111214
                    {
                        _header.ClearAndNewRow();

                        DataTable dt = _biz.Search(dlg.송장번호);
                        dt.Rows[0].SetAdded();
                        dt.Rows[0]["AM_EX"] = 0m;
                        _header.SetDataTable(dt);       // JobModeChanged 이벤트가 발생됨

                        _header.JobMode = JobModeEnum.추가후수정;
                        txt송장번호.Enabled = true;
                        txt송장번호.Focus();

                        btn판매경비등록.Enabled = false;
                        btn내역등록.Enabled = false;


                        cbo출고구분.Enabled = true;
                        ToolBarAddButtonEnabled = true;
                        ToolBarSaveButtonEnabled = true;
                        ToolBarDeleteButtonEnabled = false;

                    }
                    else
                    {
                        DataTable dt = _biz.Search(dlg.송장번호);
                        _header.SetDataTable(dt);       // JobModeChanged 이벤트가 발생됨
                    }
                    //_header.SetControlEnabled(
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

            if (ShowMessage(공통메세지.자료를삭제하시겠습니까) == DialogResult.Yes)
            {
                if (cur외화금액.DecimalValue != 0)
                {
                    if (MessageBoxEx.Show("내역이 등록되어있습니다. 모두 삭제하시겠습니까 ?", PageName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        return false;
                }
            }
            else
                return false;


            return true;
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeDelete())
                    return;

                if (_biz.Delete(txt송장번호.Text))
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
                    ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 종료버튼 클릭

        public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeExit())
                    return false;

                if (IsExistPage("P_TR_EXPACK", false))
                {
                    if (MessageBoxEx.Show("MAKE_PACKING 창을 먼저 닫아야 합니다. 창을 닫으시겠습니까 ?", PageName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        return false;
                    UnLoadPage("P_TR_EXPACK", false);
                }

                if (IsExistPage("P_TR_EXINVL", false))
                {
                    if (MessageBoxEx.Show("내역등록창을 먼저 닫아야 합니다. 창을 닫으시겠습니까 ?", PageName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        return false;
                    UnLoadPage("P_TR_EXINVL", false);
                }

                Settings.Default.거래구분 = cbo거래구분.SelectedIndex;
                Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            return true;
        }

        #endregion

        #endregion

        #region ★ 화면내버튼 클릭

        #region -> 수하인 버튼 클릭

        private void ctx수하인_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                HelpReturn helpReturn = e.HelpReturn;

                if (helpReturn != null)
                {
                    txt수하인.Text = helpReturn.CodeValue;
                    txt수하인명.Text = helpReturn.CodeName;

                    _header.CurrentRow["CD_CONSIGNEE"] = helpReturn.CodeValue;
                    _header.CurrentRow["NM_CONSIGNEE"] = helpReturn.CodeName;

                    string CD_PARTNER = helpReturn.CodeValue;

                    DataTable dt = Global.MainFrame.FillDataTable("SELECT DC_ADS1_H, DC_ADS1_D FROM MA_PARTNER WHERE  CD_COMPANY = '" + CompanyCode + "' AND CD_PARTNER = '" + CD_PARTNER + "' ");
                    txt수하인주소1.Text = D.GetString(dt.Rows[0]["DC_ADS1_H"]);
                    txt수하인주소2.Text = D.GetString(dt.Rows[0]["DC_ADS1_D"]);

                    _header.CurrentRow["ADDR1_CONSIGNEE"] = D.GetString(dt.Rows[0]["DC_ADS1_H"]);
                    _header.CurrentRow["ADDR2_CONSIGNEE"] = D.GetString(dt.Rows[0]["DC_ADS1_D"]);
                }
            }
            catch (Exception Ex)
            {
                MsgEnd(Ex);
            }
        }

        #endregion

        #region -> 착하통지처 버튼 클릭

        private void ctx착하통지처_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                HelpReturn helpReturn = e.HelpReturn;

                if (helpReturn != null)
                {
                    txt착하통지처.Text = helpReturn.CodeValue;
                    txt착하통지처명.Text = helpReturn.CodeName;

                    _header.CurrentRow["CD_NOTIFY"] = helpReturn.CodeValue;
                    _header.CurrentRow["NM_NOTIFY"] = helpReturn.CodeName;

                    string CD_PARTNER = helpReturn.CodeValue;

                    DataTable dt = Global.MainFrame.FillDataTable("SELECT DC_ADS1_H, DC_ADS1_D FROM MA_PARTNER WHERE  CD_COMPANY = '" + CompanyCode + "' AND CD_PARTNER = '" + CD_PARTNER + "' ");
                    txt착하통지처주소1.Text = D.GetString(dt.Rows[0]["DC_ADS1_H"]);
                    txt착하통지처주소2.Text = D.GetString(dt.Rows[0]["DC_ADS1_D"]);

                    _header.CurrentRow["ADDR1_NOTIFY"] = D.GetString(dt.Rows[0]["DC_ADS1_H"]);
                    _header.CurrentRow["ADDR2_NOTIFY"] = D.GetString(dt.Rows[0]["DC_ADS1_D"]);
                }
            }
            catch (Exception Ex)
            {
                MsgEnd(Ex);
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

                DataTable dt = _biz.SearchInvBF(txt송장번호.Text);

                string[] ls_args = new string[12];
                ls_args[0] = "INV";
                ls_args[1] = txt송장번호.Text;
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

                LoadPageFrom("P_TR_EXCOST", "판매경비등록", Grant, ls_args);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 내역등록버튼 클릭

        private void btn내역등록_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsExistPage("P_TR_EXINVL", false))
                    UnLoadPage("P_TR_EXINVL", false);

                object[] args = new object[22];
                args[0] = txt송장번호.Text;
                args[1] = txt거래처.Text;
                args[2] = txt거래처명.Text;
                args[3] = bpc사업장.CodeValue;
                args[4] = bpc사업장.CodeName;
                //args[5] = cbo거래구분.SelectedValue.ToString();
                args[5] = this.cbo거래구분.SelectedValue == null ? "" : this.cbo거래구분.SelectedValue.ToString();
                args[6] = cbo거래구분.Text;
                //args[7] = cbo통화.SelectedValue.ToString();
                args[7] = this.cbo통화.SelectedValue == null ? "" : this.cbo통화.SelectedValue.ToString();
                args[8] = cbo통화.Text;
                args[9] = dtp발행일자.Text;
                args[10] = txt인도조건.Text;
                args[11] = cur외화금액.DecimalValue;
                //args[12] = cbo중량단위.SelectedValue.ToString();
                args[12] = this.cbo중량단위.SelectedValue == null ? "" : this.cbo중량단위.SelectedValue.ToString();
                args[13] = cur총중량.DecimalValue;
                args[14] = cur순중량.DecimalValue;
                args[15] = txt시작CT번호.Text;
                args[16] = txt종료CT번호.Text;
                //args[17] = new VoidParamsHandler<object>(PACKING_수정내역);
                args[17] = new System.EventHandler(OnCloseLoadPageFrom_출고적용);
                args[18] = new System.EventHandler(OnCloseLoadPageFrom_Packing);

                //추가 20110825
                //args[19] = cbo출고구분.SelectedValue.ToString();
                args[19] = this.cbo출고구분.SelectedValue == null ? "" : this.cbo출고구분.SelectedValue.ToString();
                args[20] = cbo출고구분.Text;
                args[21] = new System.EventHandler(OnCloseLoadPageFrom_AddData);

                LoadPageFrom("P_TR_EXINVL", "내역등록", Grant, args);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }

        }

        #endregion

        #region -> Shipping Mark버튼 클릭

        private void btnShippingMark_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt송장번호.Text == "")
                {
                    ShowMessageKor("송장번호를 먼저 등록하셔야합니다.");
                    return;
                }

                P_TR_EXINV_SHIPPING_SUB frm = new P_TR_EXINV_SHIPPING_SUB(txt송장번호.Text);
                frm.ShowDialog();
            }
            catch (Exception Ex)
            {
                MsgEnd(Ex);
            }
        }

        #endregion

        #region -> 거래처버튼 클릭

        private void ctx거래처_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                HelpReturn helpReturn = e.HelpReturn;

                if (helpReturn != null)
                {
                    txt거래처.Text = helpReturn.CodeValue;
                    txt거래처명.Text = helpReturn.CodeName;

                    _header.CurrentRow["CD_PARTNER"] = helpReturn.CodeValue;
                    _header.CurrentRow["NM_PARTNER"] = helpReturn.CodeName;

                    string CD_PARTNER = helpReturn.CodeValue;

                    DataTable dt = Global.MainFrame.FillDataTable("SELECT DC_ADS1_H, DC_ADS1_D FROM MA_PARTNER WHERE  CD_COMPANY = '" + CompanyCode + "' AND CD_PARTNER = '" + CD_PARTNER + "' ");
                    txt거래처주소1.Text = D.GetString(dt.Rows[0]["DC_ADS1_H"]);
                    txt거래처주소2.Text = D.GetString(dt.Rows[0]["DC_ADS1_D"]);

                    _header.CurrentRow["ADDR1_PARTNER"] = D.GetString(dt.Rows[0]["DC_ADS1_H"]);
                    _header.CurrentRow["ADDR2_PARTNER"] = D.GetString(dt.Rows[0]["DC_ADS1_D"]);
                }
            }
            catch (Exception Ex)
            {
                MsgEnd(Ex);
            }
        }

        #endregion

        #region -> 수출자버튼 클릭

        private void ctx수출자_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                HelpReturn helpReturn = e.HelpReturn;

                if (helpReturn != null)
                {
                    txt수출자.Text = helpReturn.CodeValue;
                    txt수출자명.Text = helpReturn.CodeName;

                    _header.CurrentRow["CD_EXPORT"] = helpReturn.CodeValue;
                    _header.CurrentRow["NM_EXPORT"] = helpReturn.CodeName;

                    string CD_PARTNER = helpReturn.CodeValue;

                    DataTable dt = Global.MainFrame.FillDataTable("SELECT DC_ADS1_H, DC_ADS1_D FROM MA_PARTNER WHERE  CD_COMPANY = '" + CompanyCode + "' AND CD_PARTNER = '" + CD_PARTNER + "' ");
                    txt수출자주소1.Text = D.GetString(dt.Rows[0]["DC_ADS1_H"]);
                    txt수출자주소2.Text = D.GetString(dt.Rows[0]["DC_ADS1_D"]);

                    _header.CurrentRow["ADDR1_EXPORT"] = D.GetString(dt.Rows[0]["DC_ADS1_H"]);
                    _header.CurrentRow["ADDR2_EXPORT"] = D.GetString(dt.Rows[0]["DC_ADS1_D"]);
                }
            }
            catch (Exception Ex)
            {
                MsgEnd(Ex);
            }
        }

        #endregion

        #endregion

        #region ★ 기타 이벤트

        #region -> 참조 신용장번호 클릭(20071214일 윤과장님 요청에 의해 삭제)

        //private void tbtn참조신용장번호_Search(object sender, SearchEventArgs e)
        //{
        //    try
        //    {
        //        P_TR_EXLCNO_SUB dlg = new P_TR_EXLCNO_SUB("004|005|");
        //        if (dlg.ShowDialog() == DialogResult.OK)
        //        {
        //            DataRow dr = dlg.ReturnRow;

        //            if (dr == null) return;
        //            else ToolBarSaveButtonEnabled = true;

        //            _header.CurrentRow["CD_BIZAREA"] = dr["CD_BIZAREA"].ToString();
        //            _header.CurrentRow["NM_BIZAREA"] = dr["NM_BIZAREA"].ToString();
        //            bpc사업장.CodeValue = dr["CD_BIZAREA"].ToString();
        //            bpc사업장.CodeName = dr["NM_BIZAREA"].ToString();

        //            _header.CurrentRow["CD_SALEGRP"] = dr["CD_SALEGRP"].ToString();
        //            _header.CurrentRow["NM_SALEGRP"] = dr["NM_SALEGRP"].ToString();
        //            bbpc영업그룹.CodeValue = dr["CD_SALEGRP"].ToString();
        //            bbpc영업그룹.CodeName = dr["NM_SALEGRP"].ToString();

        //            _header.CurrentRow["CD_PARTNER"] = dr["CD_PARTNER"].ToString();
        //            _header.CurrentRow["NM_PARTNER"] = dr["NM_PARTNER"].ToString();
        //            bpc거래처.CodeValue = dr["CD_PARTNER"].ToString();
        //            bpc거래처.CodeName = dr["NM_PARTNER"].ToString();

        //            _header.CurrentRow["NO_EMP"] = dr["NO_EMP"].ToString();
        //            _header.CurrentRow["NM_KOR"] = dr["NM_KOR"].ToString();
        //            bpc담당자.CodeValue = dr["NO_EMP"].ToString();
        //            bpc담당자.CodeName = dr["NM_KOR"].ToString();

        //            _header.CurrentRow["FG_LC"] = dr["FG_LC"].ToString();
        //            cbo거래구분.SelectedValue = dr["FG_LC"].ToString();

        //            _header.CurrentRow["CD_EXCH"] = dr["CD_EXCH"].ToString();
        //            cbo통화.SelectedValue = dr["CD_EXCH"].ToString();

        //            _header.CurrentRow["CD_ORIGIN"] = dr["CD_ORIGIN"].ToString();
        //            cbo원산지.SelectedValue = dr["CD_ORIGIN"].ToString();

        //            if (dr["TP_TRANSPORT"] != null && dr["TP_TRANSPORT"].ToString() != string.Empty)
        //            {
        //                _header.CurrentRow["TP_TRANSPORT"] = dr["TP_TRANSPORT"].ToString();
        //                cbo운송형태.SelectedValue = dr["TP_TRANSPORT"].ToString();
        //            }

        //            if (dr["TP_TRANS"] != null && dr["TP_TRANS"].ToString() != string.Empty)
        //            {
        //                _header.CurrentRow["TP_TRANS"] = dr["TP_TRANS"].ToString();
        //                cbo운송방법.SelectedValue = dr["TP_TRANS"].ToString();
        //            }

        //            _header.CurrentRow["TP_PACKING"] = dr["TP_PACKING"].ToString();
        //            cbo포장형태.SelectedValue = dr["TP_PACKING"].ToString();

        //            _header.CurrentRow["PORT_LOADING"] = dr["PORT_LOADING"].ToString();
        //            txt선적지.Text = dr["PORT_LOADING"].ToString();

        //            _header.CurrentRow["PORT_ARRIVER"] = dr["PORT_ARRIVER"].ToString();
        //            txt도착지.Text = dr["PORT_ARRIVER"].ToString();

        //            _header.CurrentRow["DESTINATION"] = dr["DESTINATION"].ToString();
        //            txt최종목적지.Text = dr["DESTINATION"].ToString();

        //            _header.CurrentRow["NO_LC"] = dr["NO_LC"].ToString();
        //            tbtn참조신용장번호.Text = dr["NO_LC"].ToString();

        //            _header.CurrentRow["REMARK1"] = dr["REMARK1"].ToString();
        //            txt비고1.Text = dr["REMARK1"].ToString();

        //            _header.CurrentRow["REMARK2"] = dr["REMARK2"].ToString();
        //            txt비고2.Text = dr["REMARK2"].ToString();
        //            dtp발행일자.Focus();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MsgEnd(ex);
        //    }
        //}

        #endregion

        #region -> 참조 S/O번호 클릭(20071214일 윤과장님 요청에 의해 삭제)

        //private void tbtn참조SO번호_Search(object sender, SearchEventArgs e)
        //{
        //    try
        //    {
        //        P_TR_EXSONO_SUB dlg = new P_TR_EXSONO_SUB();
        //        if (dlg.ShowDialog() == DialogResult.OK)
        //        {
        //            DataRow dr = dlg.ReturnRow;

        //            if (dr == null) return;
        //            else ToolBarSaveButtonEnabled = true;

        //            _header.ClearAndNewRow();

        //            _header.CurrentRow["CD_BIZAREA"] = dr["CD_BIZAREA"].ToString();
        //            _header.CurrentRow["NM_BIZAREA"] = dr["NM_BIZAREA"].ToString();
        //            bpc사업장.CodeValue = dr["CD_BIZAREA"].ToString();
        //            bpc사업장.CodeName = dr["NM_BIZAREA"].ToString();

        //            _header.CurrentRow["CD_SALEGRP"] = dr["CD_SALEGRP"].ToString();
        //            _header.CurrentRow["NM_SALEGRP"] = dr["NM_SALEGRP"].ToString();
        //            bbpc영업그룹.CodeValue = dr["CD_SALEGRP"].ToString();
        //            bbpc영업그룹.CodeName = dr["NM_SALEGRP"].ToString();

        //            _header.CurrentRow["CD_PARTNER"] = dr["CD_PARTNER"].ToString();
        //            _header.CurrentRow["NM_PARTNER"] = dr["NM_PARTNER"].ToString();
        //            bpc거래처.CodeValue = dr["CD_PARTNER"].ToString();
        //            bpc거래처.CodeName = dr["NM_PARTNER"].ToString();

        //            _header.CurrentRow["NO_EMP"] = dr["NO_EMP"].ToString();
        //            _header.CurrentRow["NM_KOR"] = dr["NM_KOR"].ToString();
        //            bpc담당자.CodeValue = dr["NO_EMP"].ToString();
        //            bpc담당자.CodeName = dr["NM_KOR"].ToString();

        //            _header.CurrentRow["FG_LC"] = dr["FG_LC"].ToString();
        //            cbo거래구분.SelectedValue = dr["FG_LC"].ToString();

        //            _header.CurrentRow["CD_EXCH"] = dr["CD_EXCH"].ToString();
        //            cbo통화.SelectedValue = dr["CD_EXCH"].ToString();

        //            _header.CurrentRow["AM_EX"] = Convert.ToDecimal(dr["AM_EX"]);
        //            cur외화금액.DecimalValue = Convert.ToDecimal(dr["AM_EX"]);

        //            _header.CurrentRow["CD_ORIGIN"] = dr["CD_ORIGIN"].ToString();
        //            cbo원산지.SelectedValue = dr["CD_ORIGIN"].ToString();

        //            if (dr["TP_TRANSPORT"] != null && dr["TP_TRANSPORT"].ToString() == string.Empty)
        //            {
        //                _header.CurrentRow["TP_TRANSPORT"] = dr["TP_TRANSPORT"].ToString();
        //                cbo운송형태.SelectedValue = dr["TP_TRANSPORT"].ToString();
        //            }

        //            _header.CurrentRow["TP_TRANS"] = dr["TP_TRANS"].ToString();
        //            cbo운송방법.SelectedValue = dr["TP_TRANS"].ToString();

        //            _header.CurrentRow["TP_PACKING"] = dr["TP_PACKING"].ToString();
        //            cbo포장형태.SelectedValue = dr["TP_PACKING"].ToString();

        //            _header.CurrentRow["PORT_LOADING"] = dr["PORT_LOADING"].ToString();
        //            txt선적지.Text = dr["PORT_LOADING"].ToString();

        //            _header.CurrentRow["PORT_ARRIVER"] = dr["PORT_ARRIVER"].ToString();
        //            txt도착지.Text = dr["PORT_ARRIVER"].ToString();

        //            _header.CurrentRow["DESTINATION"] = dr["DESTINATION"].ToString();
        //            txt최종목적지.Text = dr["DESTINATION"].ToString();

        //            _header.CurrentRow["NO_SO"] = dr["NO_SO"].ToString();
        //            tbtn참조SO번호.Text = dr["NO_SO"].ToString();

        //            _header.CurrentRow["REMARK1"] = dr["REMARK1"].ToString();
        //            txt비고1.Text = dr["REMARK1"].ToString();

        //            _header.CurrentRow["REMARK2"] = dr["REMARK2"].ToString();
        //            txt비고2.Text = dr["REMARK2"].ToString();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MsgEnd(ex);
        //    }
        //}
        
        #endregion

        //키 다운이벤트
        #region -> txt거래처_KeyDown 이벤트

        private void txt거래처_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string _거래처코드 = txt거래처.Text;

                    DataTable dt = Global.MainFrame.FillDataTable("SELECT CD_PARTNER, LN_PARTNER AS NM_PARTNER, DC_ADS1_H, DC_ADS1_D FROM MA_PARTNER WHERE  CD_COMPANY = '" + CompanyCode + "' AND CD_PARTNER = '" + _거래처코드 + "' ");

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        txt거래처.Text = dt.Rows[0]["CD_PARTNER"].ToString();
                        txt거래처명.Text = dt.Rows[0]["NM_PARTNER"].ToString();
                        txt거래처주소1.Text = dt.Rows[0]["DC_ADS1_H"].ToString();
                        txt거래처주소2.Text = dt.Rows[0]["DC_ADS1_D"].ToString();

                        _header.CurrentRow["NM_PARTNER"] = dt.Rows[0]["NM_PARTNER"].ToString();
                        _header.CurrentRow["ADDR1_PARTNER"] = dt.Rows[0]["DC_ADS1_H"].ToString();
                        _header.CurrentRow["ADDR2_PARTNER"] = dt.Rows[0]["DC_ADS1_D"].ToString();
                    }
                    else
                    {
                        HelpReturn helpReturn = null;
                        HelpParam param = null;

                        param = new HelpParam(HelpID.P_MA_PARTNER_SUB, MainFrameInterface);
                        helpReturn = (HelpReturn)ShowHelp(param);

                        if (helpReturn.DialogResult == DialogResult.OK)
                        {
                            txt거래처.Text = helpReturn.CodeValue;
                            txt거래처명.Text = helpReturn.CodeName;

                            _header.CurrentRow["CD_PARTNER"] = helpReturn.CodeValue;
                            _header.CurrentRow["NM_PARTNER"] = helpReturn.CodeName;

                            string CD_PARTNER = helpReturn.CodeValue;

                            DataTable dt1 = Global.MainFrame.FillDataTable("SELECT DC_ADS1_H, DC_ADS1_D FROM MA_PARTNER WHERE  CD_COMPANY = '" + CompanyCode + "' AND CD_PARTNER = '" + CD_PARTNER + "' ");
                            txt거래처주소1.Text = dt1.Rows[0]["DC_ADS1_H"].ToString();
                            txt거래처주소2.Text = dt1.Rows[0]["DC_ADS1_D"].ToString();

                            _header.CurrentRow["ADDR1_PARTNER"] = dt1.Rows[0]["DC_ADS1_H"].ToString();
                            _header.CurrentRow["ADDR2_PARTNER"] = dt1.Rows[0]["DC_ADS1_D"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex.Message);
            }
        }

        #endregion

        #region -> txt수출자_KeyDown 이벤트

        private void txt수출자_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string _수출자 = txt수출자.Text;

                    DataTable dt = Global.MainFrame.FillDataTable("SELECT LN_PARTNER AS NM_PARTNER, DC_ADS1_H, DC_ADS1_D FROM MA_PARTNER WHERE  CD_COMPANY = '" + CompanyCode + "' AND CD_PARTNER = '" + _수출자 + "' ");

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        txt수출자명.Text = dt.Rows[0]["NM_PARTNER"].ToString();
                        txt수출자주소1.Text = dt.Rows[0]["DC_ADS1_H"].ToString();
                        txt수출자주소2.Text = dt.Rows[0]["DC_ADS1_D"].ToString();

                        _header.CurrentRow["NM_EXPORT"] = dt.Rows[0]["NM_PARTNER"].ToString();
                        _header.CurrentRow["ADDR1_EXPORT"] = dt.Rows[0]["DC_ADS1_H"].ToString();
                        _header.CurrentRow["ADDR2_EXPORT"] = dt.Rows[0]["DC_ADS1_D"].ToString();
                    }
                    else
                    {
                        HelpReturn helpReturn = null;
                        HelpParam param = null;

                        param = new HelpParam(HelpID.P_MA_PARTNER_SUB, MainFrameInterface);
                        helpReturn = (HelpReturn)ShowHelp(param);

                        if (helpReturn.DialogResult == DialogResult.OK)
                        {
                            txt수출자.Text = helpReturn.CodeValue;
                            txt수출자명.Text = helpReturn.CodeName;

                            _header.CurrentRow["CD_EXPORT"] = helpReturn.CodeValue;
                            _header.CurrentRow["NM_EXPORT"] = helpReturn.CodeName;

                            string CD_PARTNER = helpReturn.CodeValue;

                            DataTable dt1 = Global.MainFrame.FillDataTable("SELECT DC_ADS1_H, DC_ADS1_D FROM MA_PARTNER WHERE  CD_COMPANY = '" + CompanyCode + "' AND CD_PARTNER = '" + CD_PARTNER + "' ");
                            txt수출자주소1.Text = dt1.Rows[0]["DC_ADS1_H"].ToString();
                            txt수출자주소2.Text = dt1.Rows[0]["DC_ADS1_D"].ToString();

                            _header.CurrentRow["ADDR1_EXPORT"] = dt1.Rows[0]["DC_ADS1_H"].ToString();
                            _header.CurrentRow["ADDR2_EXPORT"] = dt1.Rows[0]["DC_ADS1_D"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex.Message);
            }
        }

        #endregion

        #region -> txt착하통지처_KeyDown 이벤트

        private void txt착하통지처_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string _착하통지처 = txt착하통지처.Text;

                    DataTable dt = Global.MainFrame.FillDataTable("SELECT LN_PARTNER AS NM_PARTNER, DC_ADS1_H, DC_ADS1_D FROM MA_PARTNER WHERE  CD_COMPANY = '" + CompanyCode + "' AND CD_PARTNER = '" + _착하통지처 + "' ");

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        txt착하통지처명.Text = dt.Rows[0]["NM_PARTNER"].ToString();
                        txt착하통지처주소1.Text = dt.Rows[0]["DC_ADS1_H"].ToString();
                        txt착하통지처주소2.Text = dt.Rows[0]["DC_ADS1_D"].ToString();

                        _header.CurrentRow["NM_NOTIFY"] = dt.Rows[0]["NM_PARTNER"].ToString();
                        _header.CurrentRow["ADDR1_NOTIFY"] = dt.Rows[0]["DC_ADS1_H"].ToString();
                        _header.CurrentRow["ADDR2_NOTIFY"] = dt.Rows[0]["DC_ADS1_D"].ToString();
                    }
                    else
                    {
                        HelpReturn helpReturn = null;
                        HelpParam param = null;

                        param = new HelpParam(HelpID.P_MA_PARTNER_SUB, MainFrameInterface);
                        helpReturn = (HelpReturn)ShowHelp(param);

                        if (helpReturn.DialogResult == DialogResult.OK)
                        {
                            txt착하통지처.Text = helpReturn.CodeValue;
                            txt착하통지처명.Text = helpReturn.CodeName;

                            _header.CurrentRow["CD_NOTIFY"] = helpReturn.CodeValue;
                            _header.CurrentRow["NM_NOTIFY"] = helpReturn.CodeName;

                            string CD_PARTNER = helpReturn.CodeValue;

                            DataTable dt1 = Global.MainFrame.FillDataTable("SELECT DC_ADS1_H, DC_ADS1_D FROM MA_PARTNER WHERE  CD_COMPANY = '" + CompanyCode + "' AND CD_PARTNER = '" + CD_PARTNER + "' ");
                            txt착하통지처주소1.Text = dt1.Rows[0]["DC_ADS1_H"].ToString();
                            txt착하통지처주소2.Text = dt1.Rows[0]["DC_ADS1_D"].ToString();

                            _header.CurrentRow["ADDR1_NOTIFY"] = dt1.Rows[0]["DC_ADS1_H"].ToString();
                            _header.CurrentRow["ADDR2_NOTIFY"] = dt1.Rows[0]["DC_ADS1_D"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex.Message);
            }
        }

        #endregion

        #region -> txt수하인_KeyDown 이벤트

        private void txt수하인_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string _수하인 = txt수하인.Text;

                    DataTable dt = Global.MainFrame.FillDataTable("SELECT LN_PARTNER AS NM_PARTNER, DC_ADS1_H, DC_ADS1_D FROM MA_PARTNER WHERE  CD_COMPANY = '" + CompanyCode + "' AND CD_PARTNER = '" + _수하인 + "' ");

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        txt수하인명.Text = dt.Rows[0]["NM_PARTNER"].ToString();
                        txt수하인주소1.Text = dt.Rows[0]["DC_ADS1_H"].ToString();
                        txt수하인주소2.Text = dt.Rows[0]["DC_ADS1_D"].ToString();

                        _header.CurrentRow["NM_CONSIGNEE"] = dt.Rows[0]["NM_PARTNER"].ToString();
                        _header.CurrentRow["ADDR1_CONSIGNEE"] = dt.Rows[0]["DC_ADS1_H"].ToString();
                        _header.CurrentRow["ADDR2_CONSIGNEE"] = dt.Rows[0]["DC_ADS1_D"].ToString();
                    }
                    else
                    {
                        HelpReturn helpReturn = null;
                        HelpParam param = null;

                        param = new HelpParam(HelpID.P_MA_PARTNER_SUB, MainFrameInterface);
                        helpReturn = (HelpReturn)ShowHelp(param);

                        if (helpReturn.DialogResult == DialogResult.OK)
                        {
                            txt수하인.Text = helpReturn.CodeValue;
                            txt수하인명.Text = helpReturn.CodeName;

                            _header.CurrentRow["CD_CONSIGNEE"] = helpReturn.CodeValue;
                            _header.CurrentRow["NM_CONSIGNEE"] = helpReturn.CodeName;

                            string CD_PARTNER = helpReturn.CodeValue;

                            DataTable dt1 = Global.MainFrame.FillDataTable("SELECT DC_ADS1_H, DC_ADS1_D FROM MA_PARTNER WHERE  CD_COMPANY = '" + CompanyCode + "' AND CD_PARTNER = '" + CD_PARTNER + "' ");
                            txt수하인주소1.Text = dt1.Rows[0]["DC_ADS1_H"].ToString();
                            txt수하인주소2.Text = dt1.Rows[0]["DC_ADS1_D"].ToString();

                            _header.CurrentRow["ADDR1_CONSIGNEE"] = dt1.Rows[0]["DC_ADS1_H"].ToString();
                            _header.CurrentRow["ADDR2_CONSIGNEE"] = dt1.Rows[0]["DC_ADS1_D"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex.Message);
            }
        }

        #endregion

        #region -> PACKING_수정내역
        ///// <summary>
        ///// 송장작성 -> 내역등록 -> PACKING 작업 page에서 저장된 내역이 반영
        ///// </summary>
        ///// <param name="param"></param>
        //public void PACKING_수정내역(params object[] param)
        //{
        //    try
        //    {
        //        _header.CurrentRow["NO_SCT"] = D.GetDecimal(param[0]);
        //        txt시작CT번호.Text = D.GetString(param[0]);
        //        _header.CurrentRow["NO_ECT"] = D.GetDecimal(param[1]);
        //        txt종료CT번호.Text = D.GetString(param[1]);
        //        _header.CurrentRow["NET_WEIGHT"] = D.GetDecimal(param[2]);
        //        cur순중량.DecimalValue = D.GetDecimal(param[2]);
        //        _header.CurrentRow["GROSS_WEIGHT"] = D.GetDecimal(param[3]);
        //        cur총중량.DecimalValue = D.GetDecimal(param[3]);
        //        _header.CurrentRow["GROSS_VOLUME"] = D.GetDecimal(param[4]);
        //        cur총부피.DecimalValue = D.GetDecimal(param[4]);
        //    }
        //    catch (Exception ex)
        //    {
        //        MsgEnd(ex);
        //    }
        //}

        #endregion

        #endregion

        #region ★ 기타 메소드

        #region -> IsChanged

        protected override bool IsChanged()
        {
            if (base.IsChanged())
                return true;

            DataTable dt = _header.GetChanges();

            switch (_header.JobMode)
            {
                case JobModeEnum.추가후수정:
                    if (D.GetString(cbo거래구분.SelectedValue) != "" &&
                        dtp발행일자.Text != "" &&
                        bpc사업장.CodeValue != "" &&
                        bbpc영업그룹.CodeValue != "" &&
                        txt거래처.Text != "" &&
                        bpc담당자.CodeValue != "" &&
                        D.GetString(cbo통화.SelectedValue) != "" &&
                        D.GetString(cbo운송형태.SelectedValue) != "" &&
                        D.GetString(cbo운송방법.SelectedValue) != "")
                        return true;
                    break;
                case JobModeEnum.조회후수정:
                    if (dt != null)
                        return true;
                    break;
            }
     
            return false;
        }

        #endregion

        #endregion

        #region -> OnCloseLoadPageFrom_출고적용

        // 내역등록 처리 후 호출되는 이벤트
        private void OnCloseLoadPageFrom_출고적용(object sender, System.EventArgs e)
        {
            DataRow row = _biz.SearchLC내도정보(txt송장번호.Text);
            if (row == null) return;


            if (row.Table.Columns.Contains("NO_LC"))
            {
                if (ShowMessage("송장화면에 부가정보를 'LC정보'로 적용하시겠습니까?", "QY2") != DialogResult.Yes) return;
            }
            else
                if (ShowMessage("송장화면에 부가정보를 '수주정보'로 적용하시겠습니까?", "QY2") != DialogResult.Yes) return;


            _header.CurrentRow[D.GetString(txt수출자.Tag)] = txt수출자.Text = D.GetString(row["CD_EXPORT"]);
            _header.CurrentRow[D.GetString(txt수출자명.Tag)] = txt수출자명.Text = D.GetString(row["NM_EXPORT"]);
            // 주소 : ADDR1_EXPORT, ADDR2_EXPORT
            _header.CurrentRow[D.GetString(txt수출자주소1.Tag)] = txt수출자주소1.Text = D.GetString(row["ADDR1_EXPORT"]);
            _header.CurrentRow[D.GetString(txt수출자주소2.Tag)] = txt수출자주소2.Text = D.GetString(row["ADDR2_EXPORT"]);

            if (row.Table.Columns.Contains("CD_PRODUCT"))
            {
                _header.CurrentRow[D.GetString("CD_PRODUCT")] = D.GetString(row["CD_PRODUCT"]);
                _header.CurrentRow[D.GetString("NM_PRODUCT")] = D.GetString(row["NM_PRODUCT"]);
                bpc제조자.SetCode(D.GetString(row["CD_PRODUCT"]), D.GetString(row["NM_PRODUCT"]));
            }

            if (row.Table.Columns.Contains("DT_LOADING"))
            {
                _header.CurrentRow[D.GetString(dtp선적에정일.Tag)] = dtp선적에정일.Text = D.GetString(row["DT_LOADING"]);
            }

            if (row.Table.Columns.Contains("CD_ORIGIN"))
            {
                _header.CurrentRow[D.GetString(cbo원산지.Tag)] = cbo원산지.SelectedValue = D.GetString(row["CD_ORIGIN"]);
            }

            _header.CurrentRow[D.GetString(cbo운송형태.Tag)] = cbo운송형태.SelectedValue = row["TP_TRANSPORT"];
            
            _header.CurrentRow[D.GetString(cbo포장형태.Tag)] = cbo포장형태.SelectedValue = row["TP_PACKING"];
            _header.CurrentRow[D.GetString(cbo운송방법.Tag)] = cbo운송방법.SelectedValue = row["TP_TRANS"];
            _header.CurrentRow[D.GetString(txt착하통지처.Tag)] = txt착하통지처.Text = D.GetString(row["CD_NOTIFY"]);
            _header.CurrentRow[D.GetString(txt착하통지처명.Tag)] = txt착하통지처명.Text = D.GetString(row["NM_NOTIFY"]);
            // 주소 : ADDR1_NOTIFY, ADDR2_NOTIFY
            _header.CurrentRow[D.GetString(txt착하통지처주소1.Tag)] = txt착하통지처주소1.Text = D.GetString(row["ADDR1_NOTIFY"]);
            _header.CurrentRow[D.GetString(txt착하통지처주소2.Tag)] = txt착하통지처주소2.Text = D.GetString(row["ADDR2_NOTIFY"]);

            _header.CurrentRow[D.GetString(txt수하인.Tag)] = txt수하인.Text = D.GetString(row["CD_CONSIGNEE"]);
            _header.CurrentRow[D.GetString(txt수하인명.Tag)] = txt수하인명.Text = D.GetString(row["NM_CONSIGNEE"]);
            // 주소 : ADDR1_CONSIGNEE, ADDR2_CONSIGNEE
            _header.CurrentRow[D.GetString(txt수하인주소1.Tag)] = txt수하인주소1.Text = D.GetString(row["ADDR1_CONSIGNEE"]);
            _header.CurrentRow[D.GetString(txt수하인주소2.Tag)] = txt수하인주소2.Text = D.GetString(row["ADDR2_CONSIGNEE"]);

            _header.CurrentRow[D.GetString(txt선적지.Tag)] = txt선적지.Text = D.GetString(row["PORT_LOADING"]);
            _header.CurrentRow[D.GetString(txt도착지.Tag)] = txt도착지.Text = D.GetString(row["PORT_ARRIVER"]);
            _header.CurrentRow[D.GetString(txt최종목적지.Tag)] = txt최종목적지.Text = D.GetString(row["DESTINATION"]);

            if (row.Table.Columns.Contains("COND_TRANS"))
            {
                _header.CurrentRow[D.GetString(txt인도조건.Tag)] = txt인도조건.Text = D.GetString(row["COND_TRANS"]);
            }
            if (row.Table.Columns.Contains("COND_PRICE"))
            {
                _header.CurrentRow[D.GetString(cbo가격조건.Tag)] = cbo가격조건.SelectedValue = row["COND_PRICE"];
            }
        }

        #endregion

        #region -> OnCloseLoadPageFrom_Packing

        private void OnCloseLoadPageFrom_Packing(object sender, System.EventArgs e)
        {
            try
            {
                DataRow row = _biz.SearchPacking(txt송장번호.Text);

                _header.CurrentRow["NO_SCT"] = txt시작CT번호.Text = row == null ? "0" : D.GetString(row["NO_SCT"]);
                _header.CurrentRow["NO_ECT"] = txt종료CT번호.Text = row == null ? "0" : D.GetString(row["NO_ECT"]);
                _header.CurrentRow["NET_WEIGHT"] = cur순중량.DecimalValue = D.GetDecimal(row["NET_WEIGHT"]);
                _header.CurrentRow["GROSS_WEIGHT"] = cur총중량.DecimalValue = D.GetDecimal(row["GROSS_WEIGHT"]);
                _header.CurrentRow["GROSS_VOLUME"] = cur총부피.DecimalValue = D.GetDecimal(row["GROSS_VOLUME"]);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> OnCloseLoadPageFrom_AddData

        private void OnCloseLoadPageFrom_AddData(object sender, System.EventArgs e)
        {
            try
            {

               // DataTable dt = _biz.Search(txt송장번호.Text);
               // _header.SetDataTable(dt);       // JobModeChanged 이벤트가 발생됨
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

                if (txt송장번호.Text == "")
                {
                    ShowMessageKor("송장번호를 먼저 등록하셔야합니다.");
                    return;
                }
                               

                string cd_file_code = D.GetString(txt송장번호.Text); //파일 PK설정   공장코드_검사성적서번호
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


    }
}

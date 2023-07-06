using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.Common.Controls;
using Duzon.Common.BpControls;
using Duzon.Common.Forms.Help;

using System.Threading;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using DzHelpFormLib;
using System.Diagnostics;
namespace trade
{
    public partial class P_TR_EXSEAL : PageBase
    {
        #region ♣변수선언

        private P_TR_EXSEAL_BIZ _biz = null;
        private FreeBinding _header = null;

        private string CompanyCode = Global.MainFrame.LoginInfo.CompanyCode;

        #endregion


        #region ♣생성자 / 소멸자 / 디자이너

        public P_TR_EXSEAL()
        {
            try
            {
                InitializeComponent();
                _header = new FreeBinding();
                _header.JobModeChanged += new FreeBindingEventHandler(_header_JobModeChanged);
                _header.ControlValueChanged += new FreeBindingEventHandler(_header_ControlValueChanged);
            }
            catch (Exception Ex)
            {
                this.MsgEnd(Ex);
            }
        }

        #endregion


        #region -> InitLoad(페이지 초기화)

        protected override void InitLoad()
        {
            base.InitLoad();
            this.MainGrids = new FlexGrid[] { _flex };

            // 프리폼 초기화
            _biz = new P_TR_EXSEAL_BIZ();

            DataSet ds = _biz.Search("");
            _header.SetBinding(ds.Tables[0], m_pnlMain);
            _header.ClearAndNewRow();

            InitGrid();
            InitControl();
        }

        #endregion

        #region -> InitGrid(그리드 초기화)

        private void InitGrid()
        {
            _flex.BeginSetting(1, 1, true);
            _flex.SetCol("NO_LINE", "항번", 45);
            _flex.SetCol("NO_IV", "계산서번호", 120);
            _flex.SetCol("NO_LC", "L/C번호", 120);
            _flex.SetCol("CD_ITEM", "품목코드", 150);
            _flex.SetCol("NM_ITEM", "품목명", 120);
            _flex.SetCol("STND_ITEM", "규격", 80);
            _flex.SetCol("UNIT_IM", "단위", 80);
            _flex.SetCol("QT_GI_CLS", "수량", 100, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex.SetCol("UM_ITEM_CLS", "단가", 100, 17, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flex.SetCol("AM_EX_CLS", "금액", 100, 17, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flex.SetCol("AM_CLS", "원화금액", 100, 17, false, typeof(decimal), FormatTpType.MONEY);

            _flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        #endregion

        #region -> InitControl(컨트롤초기화)

        private void InitControl()
        {

            DataSet ds = this.GetComboData("N;PU_C000016", "N;SA_B000002", "S;SA_B000012", "N;MA_B000043", "N;MA_AISPOSTH;300", "N;SA_B000028", "N;SA_B000010", "N;MA_B000005", "S;MA_BIZAREA");
            //환종 20080417
            m_cboCdExch.DataSource = ds.Tables[7];
            m_cboCdExch.DisplayMember = "NAME";
            m_cboCdExch.ValueMember = "CODE";

            this.dtp물품인수일자.Value = DateTime.Now;
            this.dtp발행일자.Value = DateTime.Now;
            this.dtp유효일자.Value = DateTime.Now;
            this.dtp인도일자.Value = DateTime.Now;
        }

        #endregion

        #region ♣메인버튼 클릭이벤트

        #region -> OnToolBarSearchButtonClicked(메인조회버튼 클릭)

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                P_TR_EXSEALHP_SUB frm = new P_TR_EXSEALHP_SUB();

                if (frm.ShowDialog() == DialogResult.Yes)
                {
                    string NO_SEAL = frm.NO_SEAL;

                    if (NO_SEAL != "")
                    {
                        DataSet ds = _biz.Search(NO_SEAL);
                        //_header.SetBinding(ds.Tables[0], m_pnlMain);

                        //this.txt인수증발급번호.Text = ds.Tables[0].Rows[0]["NO_SEAL"].ToString();
                        //this.bpc거래처.CodeValue = ds.Tables[0].Rows[0]["CD_PARTNER"].ToString();
                        //this.bpc거래처.CodeName = ds.Tables[0].Rows[0]["LN_PARTNER"].ToString();
                        //this.bpc사업장.CodeValue = ds.Tables[0].Rows[0]["CD_BIZAREA"].ToString();
                        //this.bpc사업장.CodeName = ds.Tables[0].Rows[0]["NM_BIZAREA"].ToString();
                        //this.cur물품인수금액.DecimalValue = decimal.Parse(ds.Tables[0].Rows[0]["AM_SEAL"].ToString());
                        //this.dtp물품인수일자.Text = ds.Tables[0].Rows[0]["DT_SEAL"].ToString();
                        //this.dtp인도일자.Text = ds.Tables[0].Rows[0]["DT_TRANS"].ToString();
                        //this.dtp유효일자.Text = ds.Tables[0].Rows[0]["DT_VALIDITY"].ToString();
                        //this.dtp발행일자.Text = ds.Tables[0].Rows[0]["DT_BALLOT"].ToString();
                        //this.bpc담당자.CodeValue = ds.Tables[0].Rows[0]["NO_EMP"].ToString();
                        //this.bpc담당자.CodeName = ds.Tables[0].Rows[0]["NM_KOR"].ToString();
                        //this.txt비고.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();


                        _header.SetDataTable(ds.Tables[0]);

                        if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                        {
                            _flex.Binding = ds.Tables[1];
                        }

                        if (_header.JobMode == JobModeEnum.조회후수정)
                        {   //조회후수정
                            txt인수증발급번호.Enabled = false;
                            bpc거래처.Enabled = false;
                        }
                        else
                        {   //추가후수정
                            txt인수증발급번호.Enabled = true;
                            bpc거래처.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                this.ShowErrorMessage(Ex, DD("인수증등록"));
            }
        }

        #endregion

        #region -> OnToolBarAddButtonClicked(메인추가버튼 클릭)

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeAdd()) return;
                Debug.Assert(_header.CurrentRow != null);       // 혹시나 해서 한번 더 확인

                this._header.ClearAndNewRow();

                DataSet ds = _biz.Search("");

                _flex.Binding = ds.Tables[1];

                txt인수증발급번호.Enabled = true;
                bpc거래처.Enabled = true;
            }
            catch (Exception Ex)
            {
                this.MsgEnd(Ex);
            }
        }

        #endregion

        #region -> OnToolBarDeleteButtonClicked(메인삭제버튼 클릭)

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                string NO_SEAL = _header.CurrentRow["NO_SEAL"].ToString();
                if (NO_SEAL != "")
                {
                    if (!BeforeDelete()) return;
                    bool bResult = _biz.Delete(NO_SEAL);

                    if (bResult == true)
                    {
                       // this.ShowMessageKor("삭제되었습니다.");
                        this.ShowMessage("삭제되었습니다.");
                        Debug.Assert(_header.CurrentRow != null);       // 혹시나 해서 한번 더 확인
                        this._header.ClearAndNewRow();
                        DataSet ds = _biz.Search("");
                        _flex.Binding = ds.Tables[1];

                        txt인수증발급번호.Enabled = true;
                        bpc거래처.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        #region -> OnToolBarSaveButtonClicked(메인저장버튼 클릭)

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSave()) return;

                if (MsgAndSave(PageActionMode.Save))
                {
                    this.ShowMessage(PageResultMode.SaveGood);
                    txt인수증발급번호.Enabled = false;
                    bpc거래처.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        #endregion


        #region ♣컨트롤이벤트

        #region -> btn계산서적용_Click

        private void btn계산서적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.bpc거래처.IsEmpty() || this.bpc거래처.CodeValue == "" || this.bpc거래처.CodeValue == string.Empty)
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl거래처.Text);
                    this.bpc거래처.Focus();
                    return;
                }

                else if (this.bpc사업장.IsEmpty() || this.bpc사업장.CodeValue == "" || this.bpc사업장.CodeValue == string.Empty)
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl사업장.Text);
                    this.bpc사업장.Focus();
                    return;
                }

                string CD_BIZAREA = this.bpc사업장.CodeValue.ToString();
                string NM_BIZAREA = this.bpc사업장.CodeName.ToString();
                string CD_PARTNER = this.bpc거래처.CodeValue.ToString();
                string LN_PARTNER = this.bpc거래처.CodeName.ToString();

                P_TR_EXSEAL_SUB frm = new P_TR_EXSEAL_SUB(CD_BIZAREA, NM_BIZAREA, CD_PARTNER, LN_PARTNER);

                if (frm.ShowDialog() == DialogResult.Yes)
                {
                    string Multi계산서번호 = frm.Multi계산서번호.ToString().Trim();

                    DataTable dt = _biz.계산서조회(Multi계산서번호);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        //물품인수금액, 발행원화금액 계산
                        decimal am_seal = 0;
                        decimal am = 0;

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dt.Rows[i]["NO_LINE"] = i + 1;
                            am_seal += decimal.Parse(dt.Rows[i]["AM_EX_CLS"].ToString());
                            am += decimal.Parse(dt.Rows[i]["AM_CLS"].ToString());
                        }

                        m_cboCdExch.SelectedValue = dt.Rows[dt.Rows.Count - 1]["CD_EXCH"].ToString();
                        _header.CurrentRow["CD_EXCH"] = dt.Rows[dt.Rows.Count - 1]["CD_EXCH"].ToString();

                        m_curCdExch.DecimalValue = decimal.Parse(dt.Rows[dt.Rows.Count - 1]["RT_EXCH"].ToString());
                        _header.CurrentRow["RT_EXCH"] = decimal.Parse(dt.Rows[dt.Rows.Count - 1]["RT_EXCH"].ToString());

                        txt마감번호.Text = dt.Rows[dt.Rows.Count - 1]["NO_IV"].ToString();
                        _header.CurrentRow["NO_IV"] = dt.Rows[dt.Rows.Count - 1]["NO_IV"].ToString();

                        this.cur물품인수금액.DecimalValue = am_seal;
                        this.cur발행원화금액.DecimalValue = am;

                        _header.CurrentRow["AM_SEAL"] = this.cur물품인수금액.DecimalValue;
                        _header.CurrentRow["AM"] = this.cur발행원화금액.DecimalValue;

                        _flex.Binding = dt;

                        bpc거래처.Enabled = false;
                    }
                }
            }
            catch (Exception Ex)
            {
             //   MessageBox.Show(Ex.Message);
                ShowMessage(Ex.Message);
            }
        }

        #endregion

        #endregion


        #region ♣메소드 정의

        //저장메소드
        #region SaveData()

        protected override bool SaveData()
        {
            DataTable header = _header.GetChanges();
            string NO_SEAL = "";

            if (header != null)
            {
                if (header.Rows[0]["NO_SEAL"].ToString().Trim() == null || header.Rows[0]["NO_SEAL"].ToString().Trim() == "")
                {
                    string yyyyMM = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString();
                    NO_SEAL = (string)GetSeq(LoginInfo.CompanyCode, "TRE", "02", yyyyMM);//인수증번호 채번
                    header.Rows[0]["NO_SEAL"] = NO_SEAL;
                    txt인수증발급번호.Text = NO_SEAL;
                }
            }

            DataTable flex = _flex.DataTable;

            if (flex != null)
            {
                for (int i = 0; i < flex.Rows.Count; i++)
                {
                    if (flex.Rows[i]["NO_SEAL"].ToString() == null || flex.Rows[i]["NO_SEAL"].ToString().Trim() == "")
                    {
                        //flex.Rows[i]["NO_SEAL"] = NO_SEAL;
                        flex.Rows[i]["NO_SEAL"] = txt인수증발급번호.Text; //key-in가능 
                    }

                    if (flex.Rows[i]["DTS_INSERT"].ToString() == "")
                        flex.Rows[i]["DTS_INSERT"] = Global.MainFrame.GetStringToday;

                    if (flex.Rows[i]["ID_INSERT"].ToString() == "")
                        flex.Rows[i]["ID_INSERT"] = Global.MainFrame.LoginInfo.UserID;

                    if (flex.Rows[i]["DTS_UPDATE"].ToString() == "")
                        flex.Rows[i]["DTS_UPDATE"] = Global.MainFrame.GetStringToday;

                    if (flex.Rows[i]["ID_UPDATE"].ToString() == "")
                        flex.Rows[i]["ID_UPDATE"] = Global.MainFrame.LoginInfo.UserID;
                }
            }

            if (header == null && flex == null)
            {
                return false;
            }

            bool iResult = _biz.Save(header, flex);
            if (!iResult)
                return false;

            _header.AcceptChanges();
            _flex.AcceptChanges();

            return true;
        }

        #endregion

        //추가관련메소드
        #region -> BeforeAdd()

        protected override bool BeforeAdd()
        {
            if (!base.BeforeAdd())
                return false;

            if (!MsgAndSave(PageActionMode.Search))
                return false;

            return true;
        }

        #endregion

        //삭제관련메소드
        #region -> BeforeDelete()

        protected override bool BeforeDelete()
        {
            if (!base.BeforeDelete())
                return false;

            if (Convert.ToDecimal(_header.CurrentRow["AM_BAN_EX"]) > 0 ) //NEGO여부 
            {
                MessageBoxEx.Show("NEGO 가 이미 진행되었으므로 삭제할 수 없습니다.", "삭제 안내", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (ShowMessage(공통메세지.자료를삭제하시겠습니까) != DialogResult.Yes)
                return false;

            return true;
        }

        #endregion

        //기타메소드
        #region -> _header_ControlValueChanged

        void _header_ControlValueChanged(object sender, FreeBindingArgs e)
        {
            try
            {
                switch (((Control)sender).Name)
                {
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
                    ToolBarAddButtonEnabled = true;
                    ToolBarDeleteButtonEnabled = false;
                    ToolBarSaveButtonEnabled = false;
                }
                else
                {
                    ToolBarAddButtonEnabled = true;
                    ToolBarDeleteButtonEnabled = true;
                    ToolBarSaveButtonEnabled = false;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        

        #endregion
    }
}

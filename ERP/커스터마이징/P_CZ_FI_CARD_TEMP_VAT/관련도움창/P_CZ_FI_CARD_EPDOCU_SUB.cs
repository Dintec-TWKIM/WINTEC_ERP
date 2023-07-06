using Duzon.BizOn.Erpu.Resource;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.FI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace cz
{
    public partial class P_CZ_FI_CARD_EPDOCU_SUB : Duzon.Common.Forms.CommonDialog
    {
        private P_CZ_FI_CARD_TEMP_VAT_BIZ _biz;
        private DataTable _dt;
        private Hashtable _ht;
        private bool _bDocu;

        public P_CZ_FI_CARD_EPDOCU_SUB()
        {
            this.InitializeComponent();
        }

        public P_CZ_FI_CARD_EPDOCU_SUB(DataTable dt, bool bDocu)
          : this()
        {
            this._dt = dt;
            this._bDocu = bDocu;
        }

        protected override void InitLoad()
        {
            base.InitLoad();
            this._biz = new P_CZ_FI_CARD_TEMP_VAT_BIZ();
            this.InitEvent();
        }

        private void InitEvent()
        {
            this.btnOK.Click += new EventHandler(this.BtnOK_Click);
            this.bpctx작성부서.QueryBefore += new BpQueryHandler(this.BpCodeNTextBox_QueryBefore);
            this.bpctx작성사원.QueryBefore += new BpQueryHandler(this.BpCodeNTextBox_QueryBefore);
            this.bpctx예산단위.QueryBefore += new BpQueryHandler(this.BpCodeNTextBox_QueryBefore);
            this.bpctx사업계획.QueryBefore += new BpQueryHandler(this.BpCodeNTextBox_QueryBefore);
            this.bpctx예산계정.QueryBefore += new BpQueryHandler(this.BpCodeNTextBox_QueryBefore);
            this.bpctx회계계정.QueryBefore += new BpQueryHandler(this.BpCodeNTextBox_QueryBefore);
            this.bpctx상대예산계정.QueryBefore += new BpQueryHandler(this.BpCodeNTextBox_QueryBefore);
            this.bpctx상대회계계정.QueryBefore += new BpQueryHandler(this.BpCodeNTextBox_QueryBefore);
            this.bpctx코스트센터.QueryBefore += new BpQueryHandler(this.BpCodeNTextBox_QueryBefore);
            this.bpctx회계단위.QueryAfter += new BpQueryHandler(this.BpCodeNTextBox_QueryAfter);
            this.bpctx예산단위.Validated += new EventHandler(this.BpCodeNTextBox_Validated);
            this.bpctx예산계정.Validated += new EventHandler(this.BpCodeNTextBox_Validated);
            this.bpctx상대예산계정.Validated += new EventHandler(this.BpCodeNTextBox_Validated);
        }

        protected override void InitPaint()
        {
            base.InitPaint();
            try
            {
                MsgControl.ShowMsg("기본 데이터 설정 작업 중입니다..");
                this.bpctx사업계획.Enabled = this.사업계획사용여부;
                this.cbo결의구분.DataSource = (object)Duzon.ERPU.FI.FI.GetCode("FI_P000013");
                this.cbo결의구분.ValueMember = "CODE";
                this.cbo결의구분.DisplayMember = "NAME";
                this.dtp회계일자.Mask = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.FI, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
                this.dtp회계일자.Text = Global.MainFrame.GetStringToday;
                this.rdo일괄.Checked = this._bDocu;
                if (MA.ServerKey(false, "KESI"))
                {
                    this.lbl예산계정.Text = "대변)예산계정";
                    this.lbl회계계정.Text = "대변)회계계정";
                    this.lbl상대예산계정.Text = "차변)예산계정";
                    this.lbl상대회계계정.Text = "차변)회계계정";
                    this.rdo건별.Checked = true;
                }
                this.DefaultDataSetting();
                MsgControl.CloseMsg();
            }
            catch (Exception ex)
            {
                MsgControl.CloseMsg();
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.EssentialCheck)
                    return;
                this.AddHashtableData((Control)this.pnlDocu);
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void BpCodeNTextBox_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                switch (((Control)sender).Name)
                {
                    case "bpctx작성부서":
                        if (this.전표회계단위체크여부)
                            break;
                        if (!this.회계단위)
                        {
                            e.QueryCancel = true;
                            break;
                        }
                        e.HelpParam.AddAuthCD_PC(this.bpctx회계단위.CodeValue);
                        break;
                    case "bpctx작성사원":
                        if (!this.작성부서)
                        {
                            e.QueryCancel = true;
                            break;
                        }
                        e.HelpParam.P05_CD_DEPT = this.bpctx작성부서.CodeValue;
                        break;
                    case "bpctx예산단위":
                        if (!this.작성부서)
                        {
                            e.QueryCancel = true;
                            break;
                        }
                        e.HelpParam.P05_CD_DEPT = this.bpctx작성부서.CodeValue;
                        break;
                    case "bpctx사업계획":
                        if (!this.예산단위)
                        {
                            this.bpctx작성부서.Focus();
                            e.QueryCancel = true;
                            break;
                        }
                        e.HelpParam.P61_CODE1 = this.bpctx예산단위.CodeValue;
                        break;
                    case "bpctx예산계정":
                    case "bpctx상대예산계정":
                        if (!this.작성부서 || !this.예산단위)
                        {
                            e.QueryCancel = true;
                            break;
                        }
                        e.HelpParam.P42_CD_FIELD2 = this.bpctx예산단위.CodeValue;
                        e.HelpParam.P43_CD_FIELD3 = this.dtp회계일자.Text.Substring(0, 6);
                        e.HelpParam.P61_CODE1 = "1";
                        e.HelpParam.P62_CODE2 = "5";
                        e.HelpParam.P64_CODE4 = this.bpctx사업계획.CodeValue;
                        break;
                    case "bpctx회계계정":
                        if (!this.예산계정)
                        {
                            e.QueryCancel = true;
                            break;
                        }
                        e.HelpParam.P31_CD_ACCT = this.bpctx예산계정.CodeValue;
                        break;
                    case "bpctx상대회계계정":
                        if (!this.상대예산계정)
                        {
                            e.QueryCancel = true;
                            break;
                        }
                        e.HelpParam.P31_CD_ACCT = this.bpctx상대예산계정.CodeValue;
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void BpCodeNTextBox_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                switch (((Control)sender).Name)
                {
                    case "bpctx회계단위":
                        this.bpctx작성부서.Clear();
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void BpCodeNTextBox_Validated(object sender, EventArgs e)
        {
            try
            {
                switch (((Control)sender).Name)
                {
                    case "bpctx예산단위":
                        string 코드1 = string.Empty;
                        string 명1 = string.Empty;
                        if (this.사업계획사용여부)
                        {
                            this._biz.GetDataSetting(D.GetString((object)this.bpctx예산단위.CodeValue), string.Empty, P_CZ_FI_CARD_EPDOCU_SUB.세팅항목.예산단위사업계획, out 코드1, out 명1);
                            this.bpctx사업계획.CodeValue = 코드1;
                            this.bpctx사업계획.CodeName = 명1;
                        }
                        else
                        {
                            this._biz.GetDataSetting(D.GetString((object)this.bpctx예산단위.CodeValue), string.Empty, P_CZ_FI_CARD_EPDOCU_SUB.세팅항목.예산단위예산계정, out 코드1, out 명1);
                            this.bpctx예산계정.CodeValue = 코드1;
                            this.bpctx예산계정.CodeName = 명1;
                        }
                        this.BpCodeNTextBox_Validated((object)this.bpctx예산계정, EventArgs.Empty);
                        break;
                    case "bpctx예산계정":
                        string 코드2 = string.Empty;
                        string 명2 = string.Empty;
                        this._biz.GetDataSetting(D.GetString((object)this.bpctx예산계정.CodeValue), string.Empty, P_CZ_FI_CARD_EPDOCU_SUB.세팅항목.회계계정, out 코드2, out 명2);
                        this.bpctx회계계정.CodeValue = 코드2;
                        this.bpctx회계계정.CodeName = 명2;
                        break;
                    case "bpctx상대예산계정":
                        string 코드3 = string.Empty;
                        string 명3 = string.Empty;
                        this._biz.GetDataSetting(D.GetString((object)this.bpctx상대예산계정.CodeValue), string.Empty, P_CZ_FI_CARD_EPDOCU_SUB.세팅항목.상대회계계정, out 코드3, out 명3);
                        this.bpctx상대회계계정.CodeValue = 코드3;
                        this.bpctx상대회계계정.CodeName = 명3;
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void DefaultDataSetting()
        {
            string 코드1 = string.Empty;
            string 명1 = string.Empty;
            string 코드2 = string.Empty;
            string 명2 = string.Empty;
            string empty1 = string.Empty;
            string empty2 = string.Empty;
            string 코드3 = string.Empty;
            string 명3 = string.Empty;
            string 코드4 = string.Empty;
            string 명4 = string.Empty;
            string 코드5 = string.Empty;
            string 명5 = string.Empty;
            string 코드6 = string.Empty;
            string 명6 = string.Empty;
            this._biz.GetDataSetting(this.bpctx작성부서.CodeValue, string.Empty, P_CZ_FI_CARD_EPDOCU_SUB.세팅항목.예산단위, out 코드1, out 명1);
            if (this.사업계획사용여부)
                this._biz.GetDataSetting(코드1, string.Empty, P_CZ_FI_CARD_EPDOCU_SUB.세팅항목.사업계획, out 코드2, out 명2);
            string val1 = D.GetString(new DataView(this._dt, "", "CD_BGACCT DESC", DataViewRowState.CurrentRows).ToTable(true, "CD_BGACCT").Rows[0]["CD_BGACCT"]);
            string str = D.GetString(new DataView(this._dt, "", "CD_BGACCT DESC", DataViewRowState.CurrentRows).ToTable(true, "CD_BGACCT", "NM_BGACCT").Rows[0]["NM_BGACCT"]);
            this._biz.GetDataSetting(val1, string.Empty, P_CZ_FI_CARD_EPDOCU_SUB.세팅항목.회계계정, out 코드3, out 명3);
            if (this.사업계획사용여부)
                this._biz.GetDataSetting(코드2, string.Empty, P_CZ_FI_CARD_EPDOCU_SUB.세팅항목.사업계획예산계정, out 코드4, out 명4);
            else
                this._biz.GetDataSetting(코드1, string.Empty, P_CZ_FI_CARD_EPDOCU_SUB.세팅항목.상대예산계정, out 코드4, out 명4);
            this._biz.GetDataSetting(코드4, string.Empty, P_CZ_FI_CARD_EPDOCU_SUB.세팅항목.상대회계계정, out 코드5, out 명5);
            this._biz.GetDataSetting(this.bpctx작성부서.CodeValue, string.Empty, P_CZ_FI_CARD_EPDOCU_SUB.세팅항목.코스트센터, out 코드6, out 명6);
            this.bpctx예산단위.CodeValue = 코드1;
            this.bpctx예산단위.CodeName = 명1;
            this.bpctx사업계획.CodeValue = 코드2;
            this.bpctx사업계획.CodeName = 명2;
            this.bpctx예산계정.CodeValue = val1;
            this.bpctx예산계정.CodeName = str;
            this.bpctx회계계정.CodeValue = D.GetString(new DataView(this._dt, "", "CD_ACCT DESC", DataViewRowState.CurrentRows).ToTable(true, "CD_ACCT").Rows[0]["CD_ACCT"]);
            this.bpctx회계계정.CodeName = D.GetString(new DataView(this._dt, "", "CD_ACCT DESC", DataViewRowState.CurrentRows).ToTable(true, "NM_ACCT").Rows[0]["NM_ACCT"]);
            this.bpctx코스트센터.CodeValue = 코드6;
            this.bpctx코스트센터.CodeName = 명6;
            this.dtp회계일자.Text = D.GetString(new DataView(this._dt, "", "TRADE_DATE DESC", DataViewRowState.CurrentRows).ToTable(true, "TRADE_DATE").Rows[0]["TRADE_DATE"]);
        }

        private bool CheckMessage(Control ctl, string msg)
        {
            int num = (int)Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, Global.MainFrame.DD(msg));
            ctl.Focus();
            return false;
        }

        private void AddHashtableData(Control ctrs)
        {
            this._ht = new Hashtable();
            foreach (Control control in (ArrangedElementCollection)ctrs.Controls)
            {
                switch (control.GetType().Name)
                {
                    case "BpCodeNTextBox":
                        this._ht.Add((object)control.Name.Replace("bpctx", ""), (object)D.GetString((object)((BpCodeNTextBox)control).CodeValue));
                        this._ht.Add((object)(control.Name.Replace("bpctx", "") + "명"), (object)D.GetString((object)((BpCodeNTextBox)control).CodeName));
                        break;
                    case "DatePicker":
                        this._ht.Add((object)control.Name.Replace("dtp", ""), (object)D.GetString((object)control.Text));
                        break;
                    case "DropDownComboBox":
                        this._ht.Add((object)control.Name.Replace("cbo", ""), (object)D.GetString(((ListControl)control).SelectedValue));
                        break;
                }
            }
            this._ht.Add((object)"문서제목", (object)this.txt문서제목.Text);
            this._ht.Add((object)"분개라인처리방식", (object)this.rdo건별.Checked);
            this._ht.Add((object)"일괄처리", (object)this.rdo일괄.Checked);
            DataRow[] dataRowArray = this._dt.Select("YN_VAT = 'Y'");
            if (dataRowArray == null || dataRowArray.Length == 0)
                this._ht.Add((object)"부가세처리", (object)false);
            else
                this._ht.Add((object)"부가세처리", (object)true);
        }

        private bool 사업계획사용여부 => !new 회계환경설정().Is환경설정(회계환경코드.사업계획사용여부, false);

        private bool 전표회계단위체크여부 => !new 회계환경설정().Is환경설정(회계환경코드.전표회계단위체크여부, false);

        private bool 회계단위 => !Checker.IsEmpty((Control)this.bpctx회계단위, Global.MainFrame.DD(nameof(회계단위)));

        private bool 작성부서 => !Checker.IsEmpty((Control)this.bpctx작성부서, Global.MainFrame.DD(nameof(작성부서)));

        private bool 예산단위 => !Checker.IsEmpty((Control)this.bpctx예산단위, Global.MainFrame.DD(nameof(예산단위)));

        private bool 예산계정 => !Checker.IsEmpty((Control)this.bpctx예산계정, Global.MainFrame.DD(nameof(예산계정)));

        private bool 상대예산계정 => !Checker.IsEmpty((Control)this.bpctx상대예산계정, Global.MainFrame.DD(nameof(상대예산계정)));

        private bool EssentialCheck
        {
            get
            {
                foreach (Control control in (ArrangedElementCollection)this.pnlDocu.Controls)
                {
                    switch (control.GetType().Name)
                    {
                        case "BpCodeNTextBox":
                            if ((!(control.Name == "bpctx사업계획") || this.사업계획사용여부) && !(control.Name == "bpctx코스트센터") && ((BpCodeNTextBox)control).CodeValue == string.Empty)
                                return this.CheckMessage(control, control.Name.Replace("bpctx", ""));
                            continue;
                        case "DatePicker":
                            if (control.Text.Length != 8)
                                return this.CheckMessage(control, control.Name.Replace("dtp", ""));
                            break;
                    }
                }
                return true;
            }
        }

        public Hashtable GetHashtable => this._ht;

        public bool GetRbo => this.rdo건별.Checked;

        internal class 세팅항목
        {
            public static string 예산단위 = "1";
            public static string 사업계획 = "2";
            public static string 예산계정 = "3";
            public static string 회계계정 = "4";
            public static string 상대예산계정 = "5";
            public static string 상대회계계정 = "6";
            public static string 코스트센터 = "7";
            public static string 사업계획예산계정 = "8";
            public static string 예산단위사업계획 = "9";
            public static string 예산단위예산계정 = "10";
        }
    }
}

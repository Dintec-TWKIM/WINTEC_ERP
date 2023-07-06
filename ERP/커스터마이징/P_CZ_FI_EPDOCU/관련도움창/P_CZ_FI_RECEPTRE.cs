using Duzon.BizOn.Erpu.Security;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.FI;
using Duzon.ERPU.FI.전표;
using Duzon.ERPU.Forms;
using DzHelpFormLib;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_FI_RECEPTRE : Duzon.Common.Forms.CommonDialog
    {
        private P_CZ_FI_RECEPTRE_BIZ _biz = new P_CZ_FI_RECEPTRE_BIZ();
        private FreeBinding _free = new FreeBinding();
        private string _접대비코드 = "###***";
        private bool _취소전표 = false;
        private DataRow _docuRow;

        private bool 계정과목체크
        {
            get
            {
                return !Checker.IsEmpty((Control)this._bpctx계정과목, "계정과목");
            }
        }

        private bool 발생일자체크
        {
            get
            {
                return !Checker.IsEmpty((Control)this._dtp발생일자, "발생일자");
            }
        }

        private bool 사용부서체크
        {
            get
            {
                return !Checker.IsEmpty((Control)this.bpt사용부서, "사용부서");
            }
        }

        private bool 사용자체크
        {
            get
            {
                return !Checker.IsEmpty((Control)this.bpt사용자, "사용자");
            }
        }

        private bool 물품대체크
        {
            get
            {
                return !Checker.IsEmpty((Control)this.cur물품대, "물품대");
            }
        }

        private bool 신용카드체크
        {
            get
            {
                if (this.cbo사용구분.SelectedValue.ToString() == "001")
                    return !Checker.IsEmpty((Control)this._bpc신용카드, "신용카드");
                return true;
            }
        }

        public P_CZ_FI_RECEPTRE()
        {
            this.InitializeComponent();
        }

        public P_CZ_FI_RECEPTRE(DataRow docuRow)
            : this()
        {
            this._docuRow = docuRow;
        }

        public P_CZ_FI_RECEPTRE(string ps_Key)
            : this()
        {
            this._pnl접대비내역.Enabled = false;
            this._취소전표 = true;
            this._접대비코드 = ps_Key;
        }

        protected override void InitLoad()
        {
            base.InitLoad();
            this.InitEvent();
        }

        private void InitEvent()
        {
            this.btn확인.Click += new EventHandler(this.btn확인_Click);
            this._free.ControlValueChanged += new FreeBindingEventHandler(this.header_ControlValueChanged);
            this.bpt사용부서.QueryBefore += new BpQueryHandler(this.BpControls_QueryBefore);
            this.bpt사용자.QueryBefore += new BpQueryHandler(this.BpControls_QueryBefore);
            this.bpt접대장소.QueryAfter += new BpQueryHandler(this.BpControl_QueryAfter);
            this.bpt접대상대.QueryAfter += new BpQueryHandler(this.BpControl_QueryAfter);
        }

        protected override void InitPaint()
        {
            DataSet ds;

            try
            {
                base.InitPaint();

                Forms.SetButtonToolTip(Forms.ButtenType.확인, this.btn확인);
                Forms.SetButtonToolTip(Forms.ButtenType.취소, this.btn_취소);

                ds = Global.MainFrame.GetComboData("N;FI_T000024",
                                                   "N;FI_T000025",
                                                   "N;FI_T000026",
                                                   "N;FI_T000048",
                                                   "N;FI_T000049");

                SetControl setControl = new SetControl();
                setControl.SetCombobox(this.cbo사용구분, ds.Tables[0]);
                setControl.SetCombobox(this.cbo증빙구분, ds.Tables[0]);
                setControl.SetCombobox(this._cbo사용지역, ds.Tables[0]);
                setControl.SetCombobox(this.cbo접대비구분, ds.Tables[0]);
                setControl.SetCombobox(this.cbo지출명의, ds.Tables[0]);

                if (this._취소전표)
                {
                    this.Search(this._접대비코드);
                }
                else
                {
                    this._접대비코드 = D.GetString(this._docuRow["CD_UMNG1"]);
                    this.Search(this._접대비코드);
                    this._dtp발생일자.Focus();
                }

                this.btn확인.Enabled = new Docu().전표연동창수정여부(D.GetString(this._docuRow["CD_PC"]), D.GetString(this._docuRow["NO_DOCU"]), D.GetInt(Global.MainFrame.ExecuteScalar(@"SELECT NO_DOLINE
                                                                                                                                                                                           FROM FI_DOCU WITH(NOLOCK)
                                                                                                                                                                                           WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "' AND NO_DOCU = '" + D.GetString(this._docuRow["NO_DOCU"]) + "' AND CD_ACCT = '" + D.GetString(this._docuRow["CD_STDACCT"]) + "'")));
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void Search(string 접대비코드)
        {
            DataTable dt = this._biz.Search(접대비코드, this._취소전표);
            this._free.SetBinding(dt, this._pnl접대비내역);

            if (dt == null || dt.Rows.Count == 0)
            {
                this._free.ClearAndNewRow();
                this._free.CurrentRow["CD_ACCT"] = (this._bpctx계정과목.CodeValue = D.GetString(this._docuRow["CD_STDACCT"]));
                this._free.CurrentRow["NM_ACCT"] = (this._bpctx계정과목.CodeName = D.GetString(Global.MainFrame.ExecuteScalar(@"SELECT NM_ACCT
                                                                                                                                FROM FI_ACCTCODE WITH(NOLOCK)
                                                                                                                                WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "' AND CD_ACCT = '" + D.GetString(this._docuRow["CD_STDACCT"]) + "'")));
                this._free.CurrentRow["DT_START"] = (this._dtp발생일자.Text = D.GetString(this._docuRow["DT_ACCT"]));
                this._free.CurrentRow["CD_CARD"] = (this._bpc신용카드.CodeValue = D.GetString(this._docuRow["CD_CARD"]));
                this._free.CurrentRow["NO_CARD"] = (this._bpc신용카드.CodeName = D.GetString(this._docuRow["NM_CARD"]));
            }
            else
                this._free.SetDataTable(dt);

            if (D.GetString(this._free.CurrentRow["RECEPT_GU10"]) == "Y")
                this._chk경조사비.Checked = true;
            else
                this._chk경조사비.Checked = false;
            
            if (this.bpt접대장소.CodeValue != string.Empty)
                this._msk사업자NO.Enabled = this._txt대표자.Enabled = this._msk주민NO.Enabled = false;
            else
                this._msk사업자NO.Enabled = this._txt대표자.Enabled = this._msk주민NO.Enabled = true;
            
            if (this.bpt접대상대.CodeValue != string.Empty)
                this._msk사업자NO1.Enabled = this._txt대표자1.Enabled = this._msk주민NO1.Enabled = false;
            else
                this._msk사업자NO1.Enabled = this._txt대표자1.Enabled = this._msk주민NO1.Enabled = false;
        }

        private void header_ControlValueChanged(object sender, FreeBindingArgs e)
        {
            try
            {
                switch ((sender as Control).Name)
                {
                    case "cur물품대":
                    case "cur봉사료":
                        this._free.CurrentRow["USE_COST"] = (this.cur사용금액.DecimalValue = this.cur물품대.DecimalValue + this.cur봉사료.DecimalValue);
                        break;
                    case "bpt접대장소":
                        if (!(this.bpt접대장소.CodeValue == string.Empty))
                            break;
                        this._msk사업자NO.Enabled = this._txt대표자.Enabled = this._msk주민NO.Enabled = true;
                        this._msk사업자NO.Text = this._txt대표자.Text = this._msk주민NO.Text = string.Empty;
                        this._free.CurrentRow["NO_COMPANY"] = this._free.CurrentRow["NM_CEO"] = this._free.CurrentRow["NO_RES"] = string.Empty;
                        break;
                    case "bpt접대상대":
                        if (!(this.bpt접대상대.CodeValue == string.Empty))
                            break;
                        this._msk사업자NO1.Enabled = this._txt대표자1.Enabled = this._msk주민NO1.Enabled = true;
                        this._msk사업자NO1.Text = this._txt대표자1.Text = this._msk주민NO1.Text = string.Empty;
                        this._free.CurrentRow["NO_COMPANY1"] = this._free.CurrentRow["NM_CEO1"] = this._free.CurrentRow["NO_RES1"] = string.Empty;
                        break;
                    case "cbo사용구분":
                        if (D.GetString(this.cbo사용구분.SelectedValue) == "001")
                        {
                            this._bpc신용카드.Enabled = true;
                            this._bpc신용카드.ItemBackColor = Color.FromArgb((int)byte.MaxValue, 237, 242);
                            break;
                        }
                        this._bpc신용카드.Clear();
                        this._bpc신용카드.Enabled = false;
                        this._bpc신용카드.ItemBackColor = Color.White;
                        break;
                    case "cbo접대비구분":
                        if (!(D.GetString(this.cbo접대비구분.SelectedValue) == "005"))
                            break;
                        this._free.CurrentRow["RECEPT_GU"] = this.cbo증빙구분.SelectedValue = "004";
                        break;
                    case "cbo증빙구분":
                        if (!(D.GetString(this.cbo접대비구분.SelectedValue) == "005") || (D.GetString(this.cbo증빙구분.SelectedValue) == "004" || D.GetString(this.cbo증빙구분.SelectedValue) == "008" || D.GetString(this.cbo증빙구분.SelectedValue) == "009"))
                            break;
                        Global.MainFrame.ShowMessage("CZ_농어민지출액은 영수증, 송금명세서,증빙없음만 선택 가능합니다.");
                        this._free.CurrentRow["RECEPT_GU"] = this.cbo증빙구분.SelectedValue = "004";
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void BpControls_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                switch ((sender as Control).Name)
                {
                    case "bpt사용부서":
                    case "bpt사용자":
                        e.HelpParam.UseAccessGrant = false;
                        e.HelpParam.UseDetailGrant = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void BpControl_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                DataEncrypter dataEncrypter = new DataEncrypter();
                switch ((sender as Control).Name)
                {
                    case "bpt접대장소":
                        string str1 = dataEncrypter.Decrypting(D.GetString(e.HelpReturn.Rows[0]["NO_RES"]), DataType.Jumin);
                        this._msk사업자NO.Enabled = this._txt대표자.Enabled = this._msk주민NO.Enabled = false;
                        this._msk사업자NO.Text = D.GetString(e.HelpReturn.Rows[0]["NO_COMPANY"]);
                        this._txt대표자.Text = D.GetString(e.HelpReturn.Rows[0]["NM_CEO"]);
                        this._msk주민NO.Text = str1;
                        this._free.CurrentRow["NO_COMPANY"] = this._msk사업자NO.Text.Replace("-", string.Empty);
                        this._free.CurrentRow["NM_CEO"] = this._txt대표자.Text;
                        this._free.CurrentRow["NO_RES"] = this._msk주민NO.Text.Replace("-", string.Empty);
                        break;
                    case "bpt접대상대":
                        string str2 = dataEncrypter.Decrypting(D.GetString(e.HelpReturn.Rows[0]["NO_RES"]), DataType.Jumin);
                        this._msk사업자NO1.Enabled = this._txt대표자1.Enabled = this._msk주민NO1.Enabled = false;
                        this._msk사업자NO1.Text = D.GetString(e.HelpReturn.Rows[0]["NO_COMPANY"]);
                        this._txt대표자1.Text = D.GetString(e.HelpReturn.Rows[0]["NM_CEO"]);
                        this._msk주민NO1.Text = str2;
                        this._free.CurrentRow["NO_COMPANY1"] = this._msk사업자NO1.Text.Replace("-", string.Empty);
                        this._free.CurrentRow["NM_CEO1"] = this._txt대표자1.Text;
                        this._free.CurrentRow["NO_RES1"] = this._msk주민NO1.Text.Replace("-", string.Empty);
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void P_FI_RECEPTRE_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Forms.OkKeyDown(e, new Forms.OKButtonDelegate(this.btn확인_Click));
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn확인_Click(object sender, EventArgs e)
        {
            if (this._취소전표)
            {
                this.DialogResult = DialogResult.Cancel;
            }
            else
            {
                DataTable changes = this._free.GetChanges();
                if (changes == null)
                {
                    Global.MainFrame.ShowMessage(공통메세지.변경된내용이없습니다);
                }
                else
                {
                    if (!this.계정과목체크 || !this.발생일자체크 || (!this.사용부서체크 || !this.사용자체크) || !this.물품대체크 || !this.신용카드체크)
                        return;
                    try
                    {
                        if (this.txt_접대비NO.Text == string.Empty)
                            changes.Rows[0]["NO_RECEPT"] = (this.txt_접대비NO.Text = (string)Global.MainFrame.GetSeq(Global.MainFrame.LoginInfo.CompanyCode, "FI", "13", this._dtp발생일자.Text.Substring(0, 6)));
                        changes.Rows[0]["NO_RES"] = D.GetString(changes.Rows[0]["NO_RES"]).Replace("-", "");
                        changes.Rows[0]["NO_RES1"] = D.GetString(changes.Rows[0]["NO_RES1"]).Replace("-", "");
                        changes.Rows[0]["NO_COMPANY"] = D.GetString(changes.Rows[0]["NO_COMPANY"]).Replace("-", "");
                        changes.Rows[0]["NO_COMPANY1"] = D.GetString(changes.Rows[0]["NO_COMPANY1"]).Replace("-", "");
                        changes.Rows[0]["CD_PC"] = D.GetString(this._docuRow["CD_PC"]);
                        if (D.GetString(changes.Rows[0]["RECEPT_GU10"]) == string.Empty)
                            changes.Rows[0]["RECEPT_GU10"] = "N";
                        this._biz.접대비저장(changes);

                        this._docuRow["DT_ACCT"] = this._dtp발생일자.Text;
                        
                        if (this._txt접대내역.Text != string.Empty)
                            this._docuRow["NM_NOTE"] = this._txt접대내역.Text;
                        
                        this._docuRow["CD_UMNG1"] = this.txt_접대비NO.Text;
                        this._docuRow["AM_TAXSTD"] = this._free.CurrentRow["USE_COST"];

                        string str = new 회계환경설정().Get환경설정("YN_CCEQDEPT", false);

                        if (str.Equals("Y"))
                        {
                            this._docuRow["CD_CC"] = this.bpt사용부서.CodeValue;
                            this._docuRow["NM_CC"] = this.bpt사용부서.CodeName;
                        }

                        this._docuRow["CD_EMPLOY"] = this.bpt사용자.CodeValue;
                        this._docuRow["NM_EMP"] = this.bpt사용자.CodeName;

                        this._docuRow["CD_CARD"] = this._bpc신용카드.CodeValue;
                        this._docuRow["NM_CARD"] = this._bpc신용카드.CodeName;

                        for (int index = 1; index <= 8; ++index)
                        {
                            switch (this._docuRow["CD_MNG" + index.ToString()].ToString())
                            {
                                case "A03":
                                    this._docuRow["CD_MNGD" + index.ToString()] = this.bpt사용부서.CodeValue;
                                    this._docuRow["NM_MNGD" + index.ToString()] = this.bpt사용부서.CodeName;
                                    break;
                                case "A06":
                                    this._docuRow["CD_MNGD" + index] = this.bpt접대장소.CodeValue;
                                    this._docuRow["NM_MNGD" + index] = this.bpt접대장소.CodeValue == "00" ? this._txt접대장소.Text : this.bpt접대장소.CodeName;
                                    break;
                                case "A04":
                                    this._docuRow["CD_MNGD" + index.ToString()] = this.bpt사용자.CodeValue;
                                    this._docuRow["NM_MNGD" + index.ToString()] = this.bpt사용자.CodeName;
                                    break;
                                case "A08":
                                    this._docuRow["CD_MNGD" + index.ToString()] = this._bpc신용카드.CodeValue;
                                    this._docuRow["NM_MNGD" + index.ToString()] = this._bpc신용카드.CodeName;
                                    break;
                                case "A02":
                                    if (str.Equals("Y"))
                                    {
                                        this._docuRow["CD_MNGD" + index.ToString()] = this.bpt사용부서.CodeValue;
                                        this._docuRow["NM_MNGD" + index.ToString()] = this.bpt사용부서.CodeName;
                                        break;
                                    }
                                    break;
                                case "C20":
                                    this._docuRow["CD_MNGD" + index.ToString()] = this.cbo증빙구분.SelectedValue;
                                    this._docuRow["NM_MNGD" + index.ToString()] = this.cbo증빙구분.Text;
                                    break;
                                case "C45":
                                    this._docuRow["CD_MNGD" + index] = this.bpt접대상대.CodeValue;
                                    this._docuRow["NM_MNGD" + index] = this.bpt접대상대.CodeValue == "00" ? this._txt접대상대.Text : this.bpt접대상대.CodeName;
                                    break;
                                case "B21":
                                    this._docuRow["NM_MNGD" + index] = this._dtp발생일자.Text;
                                    break;
                            }
                        }
                        this._free.AcceptChanges();
                        Global.MainFrame.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
                        this.DialogResult = DialogResult.OK;
                    }
                    catch (Exception ex)
                    {
                        Global.MainFrame.MsgEnd(ex);
                    }
                }
            }
        }
    }
}
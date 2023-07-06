using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.Windows.Print;
using Duzon.ERPU.MF;

namespace cz
{
    public partial class P_CZ_FI_BANK_SEND_SUB : Duzon.Common.Forms.CommonDialog
    {
        #region 초기화
        private P_CZ_FI_BANK_SEND_SUB_BIZ _biz = new P_CZ_FI_BANK_SEND_SUB_BIZ();
        private DataTable _dtH;
        private DataTable _dtD;
        private string _출금은행;
        private string _파일작성일자;
        private VoidDelegate _dele;
        private string _s호출구분;

        private string 이체파일생성방식 { get; set; }

        private string 분할이체여부 { get; set; }

        private string 연동은행코드 { get; set; }

        private string 자동채번여부 { get; set; }

        private Decimal 분할이체건별한도 { get; set; }

        public string 파일작성일자
        {
            get
            {
                return this._파일작성일자;
            }
        }

        public DataTable dt
        {
            get
            {
                DataTable dataTable = null;

                if (this.이체파일생성방식 == "1")
                    dataTable = this._dtH;
                else if (this.이체파일생성방식 == "2")
                    dataTable = this._dtD;

                return dataTable;
            }
        }

        public P_CZ_FI_BANK_SEND_SUB(string 출금계좌, string 출금은행, DataTable dt, DataTable dtGroup, VoidDelegate dele)
        {
            try
            {
                this.InitializeComponent();

                this.이체파일생성방식 = new 시스템환경설정().Get은행연동환경설정("TP_SET2");
                this.분할이체여부 = new 시스템환경설정().Get은행연동환경설정("YN_LIMITE");
                this.연동은행코드 = new 시스템환경설정().Get은행연동환경설정("BANK_CODE1");
                this.자동채번여부 = new 시스템환경설정().Get은행연동환경설정("TP_SEQ");
                this.분할이체건별한도 = D.GetDecimal(new 시스템환경설정().Get은행연동환경설정("AM_LIMITE"));

                this.txt출금계좌번호.Text = 출금계좌;
                this._출금은행 = 출금은행;

                if (this.이체파일생성방식 == "1")
                    this._dtH = dt.Copy();
                else if (this.이체파일생성방식 == "2")
                {
                    this._dtH = dtGroup.Copy();
                    this._dtD = dt.Copy();
                }

                this._dele = dele;

                if (this._dele == null)
                    this._s호출구분 = Global.MainFrame.ServerKey;

                int num;

                if (!D.Contains<string>(this.연동은행코드, new string[] { "27", "027" }))
                    num = 1;
                else
                    num = 0;

                if (num == 0)
                {
                    this.rdo건별이체.Visible = true;
                    this.rdo일괄이체.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitEvent();
            this.InitGrid();
        }

        private void InitEvent()
        {
            this._flex.AfterDataRefresh += new ListChangedEventHandler(this._flex_AfterDataRefresh);

            this.rdo건별이체.CheckedChanged += new EventHandler(this.rdo건별이체_CheckedChanged);
            this.btn확인.Click += new EventHandler(this.btn확인_Click);
            this.btn삭제.Click += new EventHandler(this.btn삭제_Click);
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);

            this._flex.SetCol("NO_DOCU_LINE_NO", "전표번호", 120);
            this._flex.SetCol("TRANS_BANK_CODE", "은행코드", 70);
            this._flex.SetCol("TRANS_BANK_NAME", "은행명", 80);
            this._flex.SetCol("TRANS_ACCT_NO", "입금계좌번호", 120);
            this._flex.SetCol("TRANS_NAME", "예금주", 85);

            this._flex.SetCol("CD_EXCH", "통화명", false);
            this._flex.SetCol("NM_EXCH", "통화명", 60);
            this._flex.SetCol("TRANS_AMT_EX", "외화이체금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex.SetCol("TRANS_AMT", "원화이체금액", 100, false, typeof(decimal), FormatTpType.MONEY);

            this._flex.SetCol("NM_BANK_EN", "은행명(해외)", 100);
            this._flex.SetCol("CD_BANK_NATION", "은행소재국", false);
            this._flex.SetCol("NM_BANK_NATION", "은행소재국", 100);
            this._flex.SetCol("NO_SORT", "은행코드", 100);
            this._flex.SetCol("CD_DEPOSIT_NATION", "예금주국적", false);
            this._flex.SetCol("NM_DEPOSIT_NATION", "예금주국적", 100);
            this._flex.SetCol("NO_SWIFT", "BIC(SWIFT)코드", 100);
            this._flex.SetCol("DC_DEPOSIT_TEL", "예금주전화번호", 100);
            this._flex.SetCol("DC_DEPOSIT_ADDRESS", "예금주주소", 100);
            this._flex.SetCol("NO_BANK_BIC", "중계은행(BIC)", 100);
            this._flex.SetCol("DC_RMK", "계좌비고", 100);

            this._flex.SetCol("CUST_CODE", "거래처/사원", 80);
            this._flex.SetCol("CUST_NAME", "거래처명/사원명", 100);
            this._flex.SetCol("CLIENT_NOTE", "보내는사람적요", 120, true);
            this._flex.SetCol("TRANS_NOTE", "받는사람적요", 120, true);
            this._flex.SetCol("NM_FUND", "자금과목", 100);
            this._flex.SetCol("TP_CHARGE", "해외은행수수료부담", 100, true);
            this._flex.SetCol("TP_SEND_BY", "송금방법", 100, true);
            this._flex.SetCol("DC_RELATION", "신청인과의관계", 100, true);

            this._flex.VerifyNotNull = new string[] { "TRANS_BANK_CODE", "TRANS_ACCT_NO" };
            this._flex.SetDataMap("TP_CHARGE", Global.MainFrame.GetComboDataCombine("S;CZ_FI00002"), "CODE", "NAME");
            this._flex.SetDataMap("TP_SEND_BY", Global.MainFrame.GetComboDataCombine("S;CZ_FI00003"), "CODE", "NAME");

            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            this._flex.ExtendLastCol = true;
            this._flex.SettingVersion = "0.0.0.1";
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.dtp파일작성일자.Mask = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.FI, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT);
            this.dtp파일작성일자.ToDayDate = Global.MainFrame.GetDateTimeToday();
            this.dtp파일작성일자.Text = Global.MainFrame.GetStringToday;

            if (this.자동채번여부 == "2")
                this.txt파일순번.Enabled = false;

            this._flex.Binding = this._dtH;
            this._flex.IsDataChanged = true;
        }
        #endregion

        #region 컨트롤 이벤트
        public bool Check()
        {
            if (this.dtp파일작성일자.Text == "" || this.dtp파일작성일자.Text == string.Empty)
            {
                Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.dtp파일작성일자.Text });
                this.dtp파일작성일자.Focus();
                return false;
            }
            else if (this.자동채번여부 == "1" && (this.txt파일순번.Text == "" || this.txt파일순번.Text == string.Empty))
            {
                Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl파일순번.Text });
                this.txt파일순번.Focus();
                return false;
            }
            else
            {
                if (!Global.MainFrame.VerifyGrid(this._flex))
                    return false;
                else
                    return true;
            }
        }

        private void btn확인_Click(object sender, EventArgs e)
        {
            decimal d = 0;

            try
            {
                if (!this.Check()) return;

                if (this.자동채번여부 == "2")
                    d = this._biz.자동순번(this.dtp파일작성일자.Text);
                string 파일순번 = (this.자동채번여부 == "2" ? D.GetString(++d) : this.txt파일순번.Text);

                bool flag1 = true;
                foreach (DataRow dataRow in this._flex.DataTable.Rows)
                {
                    if (!this._biz.Check(this.dtp파일작성일자.Text, dataRow["NODE_CODE"].ToString(), 파일순번))
                        flag1 = false;
                }

                if (!flag1)
                {
                    Global.MainFrame.ShowMessage(공통메세지.이미등록된자료가있습니다);
                }
                else
                {
                    int num2 = 1;
                    foreach (DataRow row in this._flex.DataTable.Select(string.Empty, string.Empty, DataViewRowState.CurrentRows))
                    {
                        row["SEQ"] = Convert.ToString(num2++);
                    }

                    Decimal num3 = (d == 0 ? 1 : d++);
                    if (this.이체파일생성방식 == "1")
                    {
                        foreach (DataRow dataRow in this._dtH.Rows)
                        {
                            dataRow["TRANS_DATE"] = this.dtp파일작성일자.Text;
                            dataRow["TRANS_SEQ"] = (this.자동채번여부 == "2" ? D.GetString(num3) : this.txt파일순번.Text);
                            dataRow["BANK_CODE"] = this._출금은행;
                            dataRow["ACCT_NO"] = this.txt출금계좌번호.Text;

                            if (dataRow["ACCT_DATE"].ToString() == "")
                                dataRow["ACCT_DATE"] = this.dtp파일작성일자.Text;
                        }
                    }
                    else if (this.이체파일생성방식 == "2")
                    {
                        int num4 = 0;
                        foreach (DataRow dataRow1 in this._dtH.Rows)
                        {
                            dataRow1["SEQ"] = Convert.ToString(++num4);
                            dataRow1["TRANS_DATE"] = this.dtp파일작성일자.Text;
                            dataRow1["TRANS_SEQ"] = (this.자동채번여부 == "2" ? D.GetString(num3) : this.txt파일순번.Text);
                            dataRow1["BANK_CODE"] = this._출금은행;
                            dataRow1["ACCT_NO"] = this.txt출금계좌번호.Text;

                            if (dataRow1["ACCT_DATE"].ToString() == string.Empty)
                                dataRow1["ACCT_DATE"] = this.dtp파일작성일자.Text;

                            string filterExpression = "TRANS_BANK_CODE = '" + dataRow1["TRANS_BANK_CODE"] + "' AND TRANS_ACCT_NO = '" + (string)dataRow1["TRANS_ACCT_NO"] + "' AND CUST_CODE = '" + (string)dataRow1["CUST_CODE"] + "'";

                            foreach (DataRow dataRow2 in this._dtD.Select(filterExpression))
                            {
                                dataRow2["SEQ"] = dataRow1["SEQ"];
                                dataRow2["TRANS_DATE"] = dataRow1["TRANS_DATE"];
                                dataRow2["TRANS_SEQ"] = dataRow1["TRANS_SEQ"];
                                dataRow2["BANK_CODE"] = dataRow1["BANK_CODE"];
                                dataRow2["ACCT_NO"] = dataRow1["ACCT_NO"];
                                dataRow2["ACCT_DATE"] = dataRow1["ACCT_DATE"];

                                dataRow2["CLIENT_NOTE"] = dataRow1["CLIENT_NOTE"];
                                dataRow2["TRANS_NOTE"] = dataRow1["TRANS_NOTE"];
                                dataRow2["TP_CHARGE"] = dataRow1["TP_CHARGE"];
                                dataRow2["TP_SEND_BY"] = dataRow1["TP_SEND_BY"];
                                dataRow2["DC_RELATION"] = dataRow1["DC_RELATION"];
                            }
                        }
                    }

                    string 유저ID = Global.MainFrame.LoginInfo.UserID;
                    string 이체구분 = string.Empty;

                    if (this.rdo건별이체.Visible)
                        유저ID = !this.rdo건별이체.Checked ? (이체구분 = "2") : (이체구분 = "1");

                    bool flag2 = true;

                    if (this.분할이체여부 == "N")
                    {
                        if (this.이체파일생성방식 == "1")
                            flag2 = this._biz.Save(this.txt출금계좌번호.Text, 
                                                   this.dtp파일작성일자.Text, 
                                                   D.GetString(this._dtH.Rows[0]["TRANS_SEQ"]), 
                                                   this._출금은행, 
                                                   this._dtH, 
                                                   this._s호출구분, 
                                                   유저ID, 
                                                   이체구분, 
                                                   D.GetString(this._dtH.Rows[0]["TRANS_TEXT"]));
                        else if (this.이체파일생성방식 == "2")
                            flag2 = this._biz.SaveHD(this._dtH, 
                                                     this._dtD, 
                                                     this.dtp파일작성일자.Text, 
                                                     D.GetString(this._dtH.Rows[0]["TRANS_SEQ"]), 
                                                     유저ID, 
                                                     이체구분);
                    }
                    else
                    {
                        flag2 = this._biz.SaveLimite(this._dtH, 
                                                     this.분할이체건별한도, 
                                                     this.dtp파일작성일자.Text, 
                                                     D.GetString(this._dtH.Rows[0]["TRANS_SEQ"]), 
                                                     유저ID, 
                                                     이체구분);
                    }

                    if (flag2)
                    {
                        this._파일작성일자 = this.dtp파일작성일자.Text;
                        Global.MainFrame.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
                    }
                    else
                    {
                        Global.MainFrame.ShowMessage(공통메세지.자료저장중오류가발생하였습니다);
                    }

                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn삭제_Click(object sender, EventArgs e)
        {
            try
            {
                this._dele.Invoke(new string[] { this._flex[this._flex.Row, "NO_DOCU"].ToString(),
                                                 this._flex[this._flex.Row, "LINE_NO"].ToString(),
                                                 this._flex[this._flex.Row, "NODE_CODE"].ToString() });

                this._flex.RemoveItem(this._flex.Row);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void rdo건별이체_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdo건별이체.Checked)
            {
                this._flex.Binding = this._dtD.Copy();
                this.이체파일생성방식 = "1";
            }
            else
            {
                this._flex.Binding = this._dtH.Copy();
                this.이체파일생성방식 = "2";
            }

            this._flex.IsDataChanged = true;
        }
        #endregion

        #region 그리드 이벤트
        private void _flex_AfterDataRefresh(object sender, ListChangedEventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow)
                {
                    this.btn삭제.Enabled = false;
                    this.btn확인.Enabled = false;
                }
                else
                {
                    this.btn삭제.Enabled = true;
                    this.btn확인.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion
    }
}
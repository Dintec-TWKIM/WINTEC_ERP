using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using account;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.UEncryption;

namespace cz
{
    public partial class P_CZ_FI_BANK_SEND : PageBase
    {
        #region 초기화
        private P_CZ_FI_BANK_SEND_BIZ _biz = new P_CZ_FI_BANK_SEND_BIZ();
        private string _출금은행;
        private string _sHandling;

        private bool Chk회계일자
        {
            get
            {
                return Checker.IsValid(this.dtp회계일자, true, "회계일자");
            }
        }

        private bool Chk출금계좌
        {
            get
            {
                return Checker.IsValid(this.ctx출금계좌, true, "출금계좌");
            }
        }

        public P_CZ_FI_BANK_SEND()
        {
            StartUp.Certify(this);
            this.InitializeComponent();
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flexH, this._flexL };
            this._flexH.DetailGrids = new FlexGrid[] { this._flexL };

            #region Header
            this._flexH.BeginSetting(1, 1, false);

            this._flexH.SetCol("S", "선택", 35, true, CheckTypeEnum.Y_N);
            this._flexH.SetCol("NM_PC", "회계단위", 90);
            this._flexH.SetCol("ACCT_DATE", "회계일자", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("NO_DOCU", "전표번호", 115);
            this._flexH.SetCol("ACCT_NAME", "계정과목", 120);
            this._flexH.SetCol("CUST_CODE", "거래처/사원/신용카드코드", false);
            this._flexH.SetCol("CUST_NAME", "거래처/사원/신용카드", 200);

            this._flexH.SetCol("TRANS_BANK_CODE", "지급은행코드", false);
            this._flexH.SetCol("TRANS_BANK_NAME", "지급은행", 90);
            this._flexH.SetCol("TRANS_ACCT_NO", "지급계좌번호", 110);
            this._flexH.SetCol("TRANS_NAME", "예금주", 110);
           
            this._flexH.SetCol("CD_EXCH", "통화명", false);
            this._flexH.SetCol("NM_EXCH", "통화명", 60);
            this._flexH.SetCol("AM_EX", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AMT", "원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flexH.SetCol("TRANS_AMT_EX", "이체가능외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("TRANS_AMT", "이체가능원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flexH.SetCol("AM_EXBAN", "외화반제금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AMT_BAN", "원화반제금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flexH.SetCol("TRANS_AMT_EX_A", "외화이체금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("TRANS_AMT_A", "원화이체금액", 100, false, typeof(decimal), FormatTpType.MONEY);

            this._flexH.SetCol("NM_BANK_EN", "은행명(해외)", 100);
            this._flexH.SetCol("CD_BANK_NATION", "은행소재국", false);
            this._flexH.SetCol("NM_BANK_NATION", "은행소재국", 100);
            this._flexH.SetCol("NO_SORT", "은행코드", 100);
            this._flexH.SetCol("CD_DEPOSIT_NATION", "예금주국적", false);
            this._flexH.SetCol("NM_DEPOSIT_NATION", "예금주국적", 100);
            this._flexH.SetCol("NO_SWIFT", "BIC(SWIFT)코드", 100);
            this._flexH.SetCol("DC_DEPOSIT_TEL", "예금주전화번호", 100);
            this._flexH.SetCol("DC_DEPOSIT_ADDRESS", "예금주주소", 100);
            this._flexH.SetCol("NO_BANK_BIC", "중계은행(BIC)", 100);
            this._flexH.SetCol("DC_RMK", "계좌비고", 100);

            this._flexH.SetCol("DT_END", "만기일자", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("NM_DEPT", "부서", 100, false);
            this._flexH.SetCol("NM_CC", "C/C", 100, false);
            this._flexH.SetCol("NM_PJT", "프로젝트", 100, false);
            this._flexH.SetCol("CLIENT_NOTE", "적요", 180, false);
            this._flexH.SetCol("NM_DEPOSIT", "예적금명", 150, false);
            this._flexH.SetCol("NO_COMPANY", "사업자번호", false);

            this._flexH.Cols["NO_COMPANY"].Format = "000-00-00000";
            this._flexH.Cols["NO_COMPANY"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexH.SetStringFormatCol(new string[] { "NO_COMPANY" });

            this._flexH.SettingVersion = "0.0.0.1"; 
            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flexH.Cols.Frozen = 1;
            this._flexH.Styles.Alternate.BackColor = this._flexH.Styles.Normal.BackColor;
            this._flexH.WhenRowChangeThenGetDetail = true;

            this._flexH.Styles.Add("불가").BackColor = Color.FromArgb(188, 177, 163);
            this._flexH.Styles.Add("가능").BackColor = Color.White;
            this._flexH.Styles.Add("계좌없음").BackColor = Color.FromArgb(234, 234, 234);
            #endregion

            #region Line
            this._flexL.BeginSetting(1, 1, false);

            this._flexL.SetCol("TRANS_DATE", "파일작성일", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexL.SetCol("TRANS_SEQ", "파일순번", 60);
            this._flexL.SetCol("SEQ", "순번", 60);

            this._flexL.SetCol("SETTLE_DATE", "이체일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexL.SetCol("SETTLE_YN", "이체결과", 100, false);

            this._flexL.SetCol("NM_BANK", "출금은행", 120);
            this._flexL.SetCol("ACCT_NO", "출금계좌", 100);
            this._flexL.SetCol("TRANS_BANK_NAME", "지급은행", 120);
            this._flexL.SetCol("TRANS_ACCT_NO", "지급계좌", 110);
            this._flexL.SetCol("TRANS_NAME", "예금주", 100);
            this._flexL.SetCol("NM_EXCH", "통화명", 60);
            this._flexL.SetCol("TRANS_AMT_EX", "이체외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("TRANS_AMT", "이체원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flexL.SetCol("CLIENT_NOTE", "보내는사람적요", 160);
            this._flexL.SetCol("TRANS_NOTE", "받는사람적요", 160);
            this._flexL.SetCol("NM_TP_CHARGE", "해외은행수수료부담", 100);
            this._flexL.SetCol("NM_TP_SEND_BY", "송금방법", 100);

            this._flexL.SetCol("NM_BANK_EN", "은행명(해외)", 100);
            this._flexL.SetCol("CD_BANK_NATION", "은행소재국", false);
            this._flexL.SetCol("NM_BANK_NATION", "은행소재국", 100);
            this._flexL.SetCol("NO_SORT", "은행코드", 100);
            this._flexL.SetCol("CD_DEPOSIT_NATION", "예금주국적", false);
            this._flexL.SetCol("NM_DEPOSIT_NATION", "예금주국적", 100);
            this._flexL.SetCol("NO_SWIFT", "BIC(SWIFT)코드", 100);
            this._flexL.SetCol("DC_DEPOSIT_TEL", "예금주전화번호", 100);
            this._flexL.SetCol("DC_DEPOSIT_ADDRESS", "예금주주소", 100);
            this._flexL.SetCol("NO_BANK_BIC", "중계은행(BIC)", 100);
            this._flexL.SetCol("DC_RMK", "계좌비고", 100);

            this._flexL.SettingVersion = "0.0.0.1"; 
            this._flexL.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.InitControl();
            
            this._sHandling = D.GetString(this._biz.Setting().Rows[0]["TP_TRANS"]);
        }

        private void InitEvent()
        {
            this.DataChanged += new EventHandler(this.Page_DataChanged);

            this.ctx출금계좌.QueryBefore += new BpQueryHandler(this.ctx출금계좌_QueryBefore);
            this.ctx출금계좌.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
            this.btn전표생성.Click += new EventHandler(this.btn전표생성_Click);
            this.btn전표조회.Click += new EventHandler(this.btn전표조회_Click);
            this.btn파일생성.Click += new EventHandler(this.btn파일생성_Click);
            this.btn이체계정등록.Click += new EventHandler(this.btn이체계정등록_Click);
            this.cbo거래처조회방식.SelectionChangeCommitted += new EventHandler(this.cbo거래처조회방식_SelectionChangeCommitted);
            this.cbo거래구분.SelectionChangeCommitted += new EventHandler(this.cbo거래구분_SelectionChangeCommitted);
            this.ctx거래구분FROM.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
            this.ctx거래구분TO.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
            this.ctx거래구분FROM.QueryBefore += new BpQueryHandler(this.ctx거래구분_QueryBefore);
            this.ctx거래구분TO.QueryBefore += new BpQueryHandler(this.ctx거래구분_QueryBefore);

            this._flexH.CheckHeaderClick += new EventHandler(this._flexH_CheckHeaderClick);
            this._flexH.StartEdit += new RowColEventHandler(this._flexH_StartEdit);
            this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
            this._flexH.AfterEdit += new RowColEventHandler(this._flexH_AfterEdit);
            this._flexH.OwnerDrawCell += new OwnerDrawCellEventHandler(this._flexH_OwnerDrawCell);
            this._flexH.ValidateEdit += new ValidateEditEventHandler(this._flexH_ValidateEdit);
            this._flexH.AfterMultyCheck += new MultyCheckEventHandler(this._flexH_AfterMultyCheck);
        }

        private void InitControl()
        {
            this.oneGrid.UseCustomLayout = true;

            this.oneGrid.InitCustomLayout();
            this.bpPnl_CD_PC.IsNecessaryCondition = true;
            this.bpPnl_CD_ACCT.IsNecessaryCondition = true;
            this.bpPnl_DT_ACCT.IsNecessaryCondition = true;
            this.bpPnl_ACCT_NO.IsNecessaryCondition = true;

            this.dtp회계일자.StartDateToString = Global.MainFrame.GetStringFirstDayInMonth;
            this.dtp회계일자.EndDateToString = Global.MainFrame.GetStringToday;

            PeriodPicker periodPicker = this.dtp만기일자;
            string str1;
            this.dtp만기일자.EndDateToString = (str1 = "");
            string str2 = str1;
            periodPicker.StartDateToString = str2;

            this.Page_DataChanged(null, null);
            DataSet comboData = this.GetComboData(new string[] { "N;FI_E000001",
                                                                 "S;FI_J000031",
                                                                 "S;FI_J000002",
                                                                 "S;YESNO",
                                                                 "N;FI_E000049",
                                                                 "N;FI_E000061",
                                                                 "N;FI_DEM0005",
                                                                 "N;FI_DEM0006",
                                                                 "S;FI_J000003",
                                                                 "S;FI_ZNICE82" });
            SetControl setControl = new SetControl();
            setControl.SetCombobox(this.cbo거래구분, comboData.Tables[0]);
            this.cbo거래구분_SelectionChangeCommitted(null, null);
            setControl.SetCombobox(this.cbo전표유형, comboData.Tables[2]);

            setControl.SetCombobox(this.cbo거래처계좌여부, comboData.Tables[3]);
            this.cbo거래처계좌여부.SelectedValue = "Y";
            setControl.SetCombobox(this.cbo거래처조회방식, comboData.Tables[4]);
            this.cbo거래처조회방식.SelectedValue = D.GetString(this._biz.Setting().Rows[0]["TP_SET1"]);

            DataTable dataTable1 = new DataTable();
            dataTable1.Columns.Add("CODE", typeof(string));
            dataTable1.Columns.Add("NAME", typeof(string));
            DataRow row1 = dataTable1.NewRow();
            row1["CODE"] = "";
            row1["NAME"] = "";
            dataTable1.Rows.Add(row1);
            DataRow row2 = dataTable1.NewRow();
            row2["CODE"] = "N";
            row2["NAME"] = "미완료";
            dataTable1.Rows.Add(row2);
            DataRow row3 = dataTable1.NewRow();
            row3["CODE"] = "Y";
            row3["NAME"] = "완료";
            dataTable1.Rows.Add(row3);

            DataTable dataTable2 = new DataTable();
            dataTable2.Columns.Add("CODE", typeof(string));
            dataTable2.Columns.Add("NAME", typeof(string));
            DataRow row4 = dataTable2.NewRow();
            row4["CODE"] = "";
            row4["NAME"] = "";
            dataTable2.Rows.Add(row4);
            DataRow row5 = dataTable2.NewRow();
            row5["CODE"] = "N";
            row5["NAME"] = "미완료";
            dataTable2.Rows.Add(row5);
            DataRow row6 = dataTable2.NewRow();
            row6["CODE"] = "Y";
            row6["NAME"] = "완료";
            dataTable2.Rows.Add(row6);

            setControl.SetCombobox(this.cbo반제여부, dataTable1);
            setControl.SetCombobox(this.cbo이체여부, dataTable2);
        }
        #endregion

        #region 메인버튼이벤트
        private void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow)
                {
                    this.btn전표조회.Enabled = false;
                    this.btn파일생성.Enabled = false;
                    this.btn전표생성.Enabled = false;
                }
                else
                {
                    this.btn전표조회.Enabled = true;
                    this.btn파일생성.Enabled = true;
                    this.btn전표생성.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch())
                return false;
            else if (!this.Chk회계일자)
                return false;

            if (this.bpc계정과목.CodeValues == null || this.bpc계정과목.CodeValues.ToString() == "" || this.bpc계정과목.CodeValues.ToString() == string.Empty)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl계정과목.Text });
                this.bpc계정과목.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!base.BeforeSearch())
                    return;

                string str1 = this.bpc회계단위.QueryWhereIn_Pipe;
                this.cur선택건수.DecimalValue = 0;
                this.cur선택금액.DecimalValue = 0;
                this.cur선택이체금액.DecimalValue = 0;
                string str2 = this.ctx거래구분FROM.CodeValue;
                string str3 = this.ctx거래구분TO.CodeValue;

                if (this.cbo거래구분.SelectedValue.ToString() == "4")
                {
                    str2 = UEncryption.CreditEncryption(this.ctx거래구분FROM.CodeValue);
                    str3 = UEncryption.CreditEncryption(this.ctx거래구분TO.CodeValue);
                }

                DataTable dataTable1 = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                       str1,
                                                                       this.bpc계정과목.QueryWhereIn_Pipe,
                                                                       this.dtp회계일자.StartDateToString,
                                                                       this.dtp회계일자.EndDateToString,
                                                                       this.cbo거래구분.SelectedValue.ToString(),
                                                                       str2,
                                                                       str3,
                                                                       this.dtp만기일자.StartDateToString,
                                                                       this.dtp만기일자.EndDateToString,
                                                                       this.cbo전표유형.SelectedValue,
                                                                       this.cbo거래처계좌여부.SelectedValue,
                                                                       this.cbo거래처조회방식.SelectedValue,
                                                                       this.cbo반제여부.SelectedValue,
                                                                       this.cbo이체여부.SelectedValue,
                                                                       null,
                                                                       null,
                                                                       null });

                if (this._flexH.HasNormalRow)
                {
                    DataTable dataTable2 = new DataView(this._flexH.DataTable, "S='Y'", "", DataViewRowState.CurrentRows).ToTable();

                    if (dataTable2 != null && dataTable2.Rows.Count > 0 && this.ShowMessage("체크 해제하시겠습니까?", "QY2") == DialogResult.No)
                    {
                        int num1 = 0;

                        foreach (DataRow dataRow1 in dataTable2.Rows)
                        {
                            DataTable dataTable3 = dataTable1;
                            string filterExpression = "NO_DOCU = '" + D.GetString(dataRow1["NO_DOCU"]) + "'AND LINE_NO ='" + D.GetString(dataRow1["LINE_NO"]) + "'";

                            foreach (DataRow dataRow2 in dataTable3.Select(filterExpression))
                            {
                                dataRow2["S"] = "Y";
                                this.cur선택건수.DecimalValue = ((decimal)++num1);
                                CurrencyTextBox currencyTextBox1 = this.cur선택금액;
                                decimal num2 = currencyTextBox1.DecimalValue + D.GetDecimal(dataRow2["AMT"]);
                                currencyTextBox1.DecimalValue = num2;
                                CurrencyTextBox currencyTextBox2 = this.cur선택이체금액;
                                decimal num3 = currencyTextBox2.DecimalValue + D.GetDecimal(dataRow2["TRANS_AMT_A"]);
                                currencyTextBox2.DecimalValue = num3;
                                dataRow2.AcceptChanges();
                            }
                        }
                    }
                }

                this._flexL.RemoveViewAll();
                this._flexH.Binding = dataTable1;
                this._flexH.SetDummyColumn(new string[] { "S" });

                if (!this._flexH.HasNormalRow)
                    this.ShowMessage(PageResultMode.SearchNoData);
                else
                    this.PartnerSeachSetting();

                this.GridColumnColorSetting();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 컨트롤이벤트
        private void btn파일생성_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataView(this._flexH.DataTable, "S = 'Y'", "", DataViewRowState.CurrentRows).ToTable();
                dt.Columns.Add("TRANS_TEXT", typeof(string));
                dt.Columns.Add("TP_CHARGE", typeof(string));
                dt.Columns.Add("TP_SEND_BY", typeof(string));
                dt.Columns.Add("DC_RELATION", typeof(string));

                if (dt == null || dt.Rows.Count == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    if (!this.Chk출금계좌) return;

                    if (dt.Select("ISNULL(TRANS_NAME, '') = ''").Length != 0)
                    {
                        this.ShowMessage("선택된 자료 중 예금주가 없는 데이터가 있습니다.\n확인 후 다시 파일 생성하세요.");
                        return;
                    }

                    if (dt.AsEnumerable()
                          .GroupBy(x => x["CUST_CODE"].ToString(), y => D.GetDecimal(y["TRANS_AMT_EX"]), (x, y) => new { key = x, value = y.Sum() })
                          .Where(x => x.value <= 0).Count() > 0)
                    {
                        this.ShowMessage("그룹별 이체가능외화금액 합계가 0 보다 작은 건이 선택 되었습니다.");
                        return;
                    }

                    string @string = D.GetString(this._biz.Setting().Rows[0]["TP_SET2"]);

                    DataTable dataTable1 = new DataTable();
                    int num3;

                    if (!(@string == "2"))
                        num3 = 1;
                    else
                        num3 = 0;

                    if (num3 == 0)
                    {
                        dataTable1 = dt.Clone();
                        DataTable dataTable2 = dt.DefaultView.ToTable(true, new string[] { "TRANS_BANK_CODE",
                                                                                           "TRANS_ACCT_NO",
                                                                                           "CUST_CODE",
                                                                                           "CUST_NAME" });

                        if (dataTable2 != null && dataTable2.Rows.Count > 0)
                        {
                            foreach (DataRow dataRow in dataTable2.Rows)
                            {
                                DataRow[] dataRowArray = dt.Select("TRANS_BANK_CODE = '" + dataRow["TRANS_BANK_CODE"] + "'AND TRANS_ACCT_NO = '" + dataRow["TRANS_ACCT_NO"] + "' AND CUST_CODE = '" + dataRow["CUST_CODE"] + "'");

                                if (dataRowArray != null && dataRowArray.Length > 0)
                                {
                                    dataRowArray[0]["TRANS_AMT_EX"] = dt.Compute("SUM(TRANS_AMT_EX)", "TRANS_BANK_CODE = '" + dataRow["TRANS_BANK_CODE"] + "'AND TRANS_ACCT_NO = '" + dataRow["TRANS_ACCT_NO"] + "' AND CUST_CODE = '" + dataRow["CUST_CODE"] + "'");
                                    dataRowArray[0]["TRANS_AMT"] = dt.Compute("SUM(TRANS_AMT)", "TRANS_BANK_CODE = '" + dataRow["TRANS_BANK_CODE"] + "'AND TRANS_ACCT_NO = '" + dataRow["TRANS_ACCT_NO"] + "' AND CUST_CODE = '" + dataRow["CUST_CODE"] + "'");

                                    if (dataRowArray.Length == 1)
                                    {
                                        dataRowArray[0]["TRANS_NOTE"] = dataRowArray[0]["NM_PJT"].ToString();
                                    }
                                    else
                                    {
                                        foreach (DataRow dr in dataRowArray)
                                        {
                                            dataRowArray[0]["TRANS_NOTE"] += dr["NM_PJT"].ToString() + ",";
                                        }

                                        dataRowArray[0]["TRANS_NOTE"] = dataRowArray[0]["TRANS_NOTE"].ToString().Remove(dataRowArray[0]["TRANS_NOTE"].ToString().Length - 1);
                                    }

                                    dataRowArray[0]["CLIENT_NOTE"] = "물품대";
                                    dataRowArray[0]["DC_RELATION"] = "거래처";

                                    dataTable1.ImportRow(dataRowArray[0]);
                                }
                            }
                        }
                    }

                    foreach (DataRow dataRow in dt.Rows)
                    {
                        dataRow["TRANS_NOTE"] = D.GetString(dataRow["NM_PJT"]);
                        dataRow["CLIENT_NOTE"] = "물품대";
                        dataRow["TRANS_AMT"] = D.GetDecimal(dataRow["TRANS_AMT"]);
                        dataRow["DC_RELATION"] = "거래처";
                    }

                    P_CZ_FI_BANK_SEND_SUB pFiBankSendSub = new P_CZ_FI_BANK_SEND_SUB(this.ctx출금계좌.CodeValue, this._출금은행, dt, dataTable1, new VoidDelegate(this.SetCellCheck));
                    if (pFiBankSendSub.ShowDialog() == DialogResult.OK)
                    {
                        this._flexH.Redraw = false;

                        for (int @fixed = this._flexH.Rows.Fixed; @fixed < this._flexH.Rows.Count; ++@fixed)
                        {
                            if (this._flexH[@fixed, "S"].ToString() == "Y")
                            {
                                foreach (DataRow dataRow in pFiBankSendSub.dt.Rows)
                                {
                                    if (this._flexH[@fixed, "NO_DOCU"].ToString() == dataRow["NO_DOCU"].ToString() && this._flexH[@fixed, "LINE_NO"].ToString() == dataRow["LINE_NO"].ToString() && this._flexH[@fixed, "NODE_CODE"].ToString() == dataRow["NODE_CODE"].ToString())
                                        this._flexH[@fixed, "CLIENT_NOTE"] = dataRow["CLIENT_NOTE"];
                                }
                            }
                        }

                        this._flexH.AcceptChanges();
                        this.OnToolBarSearchButtonClicked(null, null);
                        this._flexH.Redraw = true;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn이체계정등록_Click(object sender, EventArgs e)
        {
            try
            {
                new P_FI_BANK_SEND_SUB1().ShowDialog();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn전표조회_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._flexH.Rows[this._flexH.Row].IsNode)
                    return;

                this.CallOtherPageMethod("P_FI_DOCU", "전표입력(" + this.PageName + ")", this.Grant, new object[] { this._flexH["NO_DOCU"].ToString(),
                                                                                                                    this._flexH["DOCU_NO"].ToString(),
                                                                                                                    this._flexH["NODE_CODE"].ToString(),
                                                                                                                    this.LoginInfo.CompanyCode });
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn전표생성_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray, dataRowArray1;
            
            dataRowArray = this._flexH.DataTable.Select("S = 'Y'", "NO_DOCU, LINE_NO ASC");

            if (dataRowArray.Length == 0)
            {
                this.ShowMessage(공통메세지.선택된자료가없습니다);
            }
            else
            {
                dataRowArray1 = this._flexH.DataTable.Select("S = 'Y' AND AMT = AMT_BAN");

                if (dataRowArray1 != null && dataRowArray1.Length > 0)
                {
                    this.ShowMessage("전표처리 완료된 건이 포함되어 있습니다.");
                    return;
                }

                string str1 = dataRowArray[0]["NODE_CODE"].ToString();

                for (int index = 1; index < dataRowArray.Length; ++index)
                {
                    if (str1 != dataRowArray[index]["NODE_CODE"].ToString())
                    {
                        this.ShowMessage("선택한 회계단위가 다르면 전표생성을 할 수 없습니다.");
                        return;
                    }
                }

                string str2 = this.bpc회계단위.SelectedValue.ToString();
                string str3 = this.bpc회계단위.SelectedText.ToString();
                BpCodeTextBox bpCodeTextBox = new BpCodeTextBox();
                bpCodeTextBox.CodeValue = this.bpc회계단위.SelectedValue.ToString();
                bpCodeTextBox.CodeName = this.bpc회계단위.SelectedText.ToString();

                if (this._biz.IsExist신전표())
                {
                    P_CZ_FI_BANK_SEND_BAN_DOCU fiBankSendBanDocu = new P_CZ_FI_BANK_SEND_BAN_DOCU();
                    if (fiBankSendBanDocu.ShowDialog() != DialogResult.OK)
                        return;

                    string str4 = fiBankSendBanDocu._sCdTacct;
                    DataTable dataTable1 = null;
                    DataSet dataSet = this._biz.SearchCdTacct(this.ctx출금계좌.CodeValue);

                    if (str4 == string.Empty)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            str4 = D.GetString(dataSet.Tables[0].Rows[0]["CD_ACCT"]);
                            dataTable1 = dataSet.Tables[1];
                        }
                    }
                    else
                        dataTable1 = dataSet.Tables[1];

                    if (D.GetString(str4) == string.Empty)
                    {
                        this.ShowMessage("계좌번호연결에 등록된 상대계정이 없습니다.");
                    }
                    else
                    {
                        Hashtable hashtable = new Hashtable();
                        hashtable.Add("회계단위코드", str2);
                        hashtable.Add("회계단위명", str3);
                        hashtable.Add("건별일괄", (fiBankSendBanDocu._sProcess == "2" ? 1 : 0));
                        hashtable.Add("상대계정코드", str4);
                        hashtable.Add("처리방식", this._sHandling);
                        hashtable.Add("상대계정정보", dataTable1);
                        DataTable dataTable2 = new DataView(this._flexH.DataTable, "S = 'Y'", "NO_DOCU, LINE_NO ASC", DataViewRowState.CurrentRows).ToTable();
                        this.CallOtherPageMethod("P_FI_DOCU", "전표입력(" + this.PageName + ")", this.Grant, new object[] { this.PageID,
                                                                                                                            hashtable,
                                                                                                                            dataTable2,
                                                                                                                            null });
                    }
                }
                else
                {
                    P_CZ_FI_BANK_SEND_BAN_DOCU fiBankSendBanDocu = new P_CZ_FI_BANK_SEND_BAN_DOCU();
                    if (fiBankSendBanDocu.ShowDialog() == DialogResult.OK)
                    {
                        DataTable dataTable = null;
                        string str4;
                        string str5 = str4 = string.Empty;
                        string str6 = fiBankSendBanDocu._sCdTacct;
                        DataSet dataSet = this._biz.SearchCdTacct(this.ctx출금계좌.CodeValue);

                        if (str6 == "")
                        {
                            if (dataSet.Tables[0].Rows.Count > 0)
                            {
                                str6 = dataSet.Tables[0].Rows[0]["CD_ACCT"].ToString();
                                dataTable = dataSet.Tables[1];
                            }
                            if (str6 == null || str6 == "")
                            {
                                this.ShowMessage("계좌번호연결에 등록된 상대계정이 없습니다.");
                                return;
                            }
                        }
                        else
                            dataTable = dataSet.Tables[1];

                        string[] strArray1 = new string[dataRowArray.Length];
                        string[] strArray2 = new string[dataRowArray.Length];
                        decimal[] numArray = new decimal[dataRowArray.Length];

                        foreach (DataRow dataRow in this._flexH.DataView.ToTable(1 != 0, "NO_DOCU").Rows)
                            str5 = str5 + dataRow["NO_DOCU"].ToString() + "|";

                        for (int index = 0; index < dataRowArray.Length; ++index)
                        {
                            strArray1[index] = dataRowArray[index]["NO_DOCU"].ToString();
                            strArray2[index] = dataRowArray[index]["LINE_NO"].ToString();
                            if (this._sHandling == "2")
                                numArray[index] = this._flexH.CDecimal(dataRowArray[index]["TRANS_AMT_A"]) - this._flexH.CDecimal(dataRowArray[index]["AMT_BAN"]);
                            else
                                numArray[index] = this._flexH.CDecimal(dataRowArray[index]["TRANS_AMT"]);
                        }

                        this.CallOtherPageMethod("P_FI_DOCU", "전표입력(" + this.PageName + ")", this.Grant, new object[] { true,
                                                                                                                            bpCodeTextBox,
                                                                                                                            str5,
                                                                                                                            "3",
                                                                                                                            str6,
                                                                                                                            fiBankSendBanDocu._sProcess,
                                                                                                                            strArray1,
                                                                                                                            strArray2,
                                                                                                                            numArray,
                                                                                                                            dataTable });
                    }
                }
            }
        }

        private void cbo거래구분_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.ctx거래구분FROM.Clear();
            this.ctx거래구분TO.Clear();

            SetControl setControl = new SetControl();
            DataSet comboData = this.GetComboData(new string[] { "N;FI_E000049",
                                                                 "N;FI_E000061" });
            string str1 = string.Empty;
            string str2 = string.Empty;
            DataTable dataTable = this._biz.Setting();

            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                this.ShowMessage("은행연동 환경설정에 셋팅을 해주세요");
            }

            string string1 = D.GetString(dataTable.Rows[0]["TP_SET1"]);
            string string2 = D.GetString(dataTable.Rows[0]["TP_SET3"]);

            try
            {
                if (this.cbo거래구분.SelectedValue.ToString() == "1")
                {
                    this.ctx거래구분FROM.HelpID = HelpID.P_MA_PARTNER_SUB;
                    this.ctx거래구분TO.HelpID = HelpID.P_MA_PARTNER_SUB;
                    setControl.SetCombobox(this.cbo거래처조회방식, comboData.Tables[0]);
                    this.cbo거래처조회방식.SelectedValue = string1;
                    this.PartnerSeachSetting();
                }
                else if (this.cbo거래구분.SelectedValue.ToString() == "2")
                {
                    this.ctx거래구분FROM.HelpID = HelpID.P_MA_EMP_SUB;
                    this.ctx거래구분TO.HelpID = HelpID.P_MA_EMP_SUB;
                    setControl.SetCombobox(this.cbo거래처조회방식, comboData.Tables[1]);
                    this.cbo거래처조회방식.SelectedValue = string2;
                    this.PartnerSeachSetting();
                }
                else if (this.cbo거래구분.SelectedValue.ToString() == "3")
                {
                    this.ctx거래구분FROM.HelpID = HelpID.P_FI_MNGD_SUB;
                    this.ctx거래구분TO.HelpID = HelpID.P_FI_MNGD_SUB;
                    setControl.SetCombobox(this.cbo거래처조회방식, null);
                }
                else
                {
                    if (this.cbo거래구분.SelectedValue.ToString() != "4")
                        return;
                    this.ctx거래구분FROM.HelpID = HelpID.P_FI_CARD_SUB;
                    this.ctx거래구분TO.HelpID = HelpID.P_FI_CARD_SUB;
                    setControl.SetCombobox(this.cbo거래처조회방식, null);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Control_QueryAfter(object sender, BpQueryArgs e)
        {
            string name;

            try
            {
                name = ((Control)sender).Name;

                if (name == this.ctx출금계좌.Name)
                {
                    this._출금은행 = e.HelpReturn.Rows[0]["BANK_CODE"].ToString();
                }
                else
                {
                    if (name != this.ctx거래구분FROM.Name)
                        return;
                    this.ctx거래구분TO.SetCode(e.CodeValue, e.CodeName);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void ctx출금계좌_QueryBefore(object sender, BpQueryArgs e)
        {
            e.HelpParam.P61_CODE1 = "1";
        }

        private void ctx거래구분_QueryBefore(object sender, BpQueryArgs e)
        {
            if (this.cbo거래구분.SelectedValue.ToString() != "3")
                return;
            e.HelpParam.P34_CD_MNG = "A25";
        }

        private void cbo거래처조회방식_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.PartnerSeachSetting();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 그리트 이벤트
        private void _flexH_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            if (e.Row < this._flexH.Rows.Fixed || e.Col < this._flexH.Cols.Fixed)
                return;

            CellStyle style = this._flexH.Rows[e.Row].Style;

            if (style == null)
            {
                if (D.GetString(this._flexH[e.Row, "CUST_CODE"]) == "")
                    this._flexH.Rows[e.Row].Style = this._flexH.Styles["불가"];
                else
                    this._flexH.Rows[e.Row].Style = this._flexH.Styles["가능"];
            }
            else if (style.Name == "불가")
            {
                if (!(D.GetString(this._flexH[e.Row, "CUST_CODE"]) != ""))
                    return;
                this._flexH.Rows[e.Row].Style = this._flexH.Styles["가능"];
            }
            else
            {
                if (!(style.Name == "가능") || !(D.GetString(this._flexH[e.Row, "CUST_CODE"]) == ""))
                    return;
                this._flexH.Rows[e.Row].Style = this._flexH.Styles["불가"];
            }
        }

        private void _flexH_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (D.GetString(this._flexH[e.Row, "CUST_CODE"]) == "")
                    e.Cancel = true;

                if (D.GetString(this._flexH[e.Row, "TRANS_ACCT_NO"]) == "")
                {
                    this.ShowMessage("계좌정보가 없습니다. 계좌정보를 등록해주세요.");
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexH_CheckHeaderClick(object sender, EventArgs e)
        {
            try
            {
                this._flexH.Redraw = false;
                this.cur선택건수.DecimalValue = 0;
                this.cur선택금액.DecimalValue = 0;
                this.cur선택이체금액.DecimalValue = 0;

                for (int @fixed = this._flexH.Rows.Fixed; @fixed < this._flexH.Rows.Count; ++@fixed)
                {
                    int num1 = 0;
                    int num2;

                    if (D.GetString(this._flexH[@fixed, "CUST_CODE"]) == "")
                        num2 = 0;
                    else
                        num2 = 1;

                    if (num2 == 0)
                        this._flexH.SetCellCheck(@fixed, 1, CheckEnum.Unchecked);

                    int num3;

                    if (D.GetString(this._flexH[@fixed, "TRANS_ACCT_NO"]) == "")
                        num3 = 0;
                    else
                        num3 = 1;

                    if (num3 == 0)
                        this._flexH.SetCellCheck(@fixed, 1, CheckEnum.Unchecked);

                    if (this._flexH[@fixed, "S"].ToString() == "Y")
                    {
                        this.cur선택건수.DecimalValue = (this.cur선택건수.DecimalValue + (decimal)(num1 + 1));
                        this.cur선택금액.DecimalValue = (this.cur선택금액.DecimalValue + D.GetDecimal(this._flexH[@fixed, "AMT"]));
                        this.cur선택이체금액.DecimalValue = (this.cur선택이체금액.DecimalValue + D.GetDecimal(this._flexH[@fixed, "TRANS_AMT_A"]));
                    }
                }

                this._flexH.Redraw = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                DataTable dataTable = null;

                string 전표번호 = this._flexH["NO_DOCU"].ToString();
                int 라인번호 = this._flexH.CInt32(this._flexH["LINE_NO"]);
                string str = "NO_DOCU = '" + 전표번호 + "' AND LINE_NO = '" + 라인번호 + "'";

                if (this._flexH.DetailQueryNeed)
                    dataTable = this._biz.SearchDetail(전표번호, 라인번호);

                this._flexL.BindingAdd(dataTable, str);
                this._flexH.DetailQueryNeed = false;

                if (D.GetDecimal(this._flexH["AMT_BAN"]) < D.GetDecimal(this._flexH["AMT"]))
                    this.btn전표생성.Enabled = true;
                else
                    this.btn전표생성.Enabled = false;

                if (D.GetDecimal(this._flexH["TRANS_AMT_A"]) < D.GetDecimal(this._flexH["AMT"]))
                    this.btn파일생성.Enabled = true;
                else
                    this.btn파일생성.Enabled = false;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexH_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                string editData = this._flexH.EditData;
                string str = this._flexH.GetData(e.Row, e.Col).ToString();
                string name = this._flexH.Cols[e.Col].Name;
                int num = 0;

                switch (name)
                {
                    case "S":
                        if (str != editData)
                        {
                            switch (editData)
                            {
                                case "Y":
                                    this.cur선택건수.DecimalValue = (this.cur선택건수.DecimalValue + (decimal)(num + 1));
                                    this.cur선택금액.DecimalValue = (this.cur선택금액.DecimalValue + D.GetDecimal(this._flexH[e.Row, "AMT"]));
                                    this.cur선택이체금액.DecimalValue = (this.cur선택이체금액.DecimalValue + D.GetDecimal(this._flexH[e.Row, "TRANS_AMT_A"]));
                                    break;
                                case "N":
                                    this.cur선택건수.DecimalValue = (this.cur선택건수.DecimalValue + (decimal)(num - 1));
                                    this.cur선택금액.DecimalValue = (this.cur선택금액.DecimalValue - D.GetDecimal(this._flexH[e.Row, "AMT"]));
                                    this.cur선택이체금액.DecimalValue = (this.cur선택이체금액.DecimalValue - D.GetDecimal(this._flexH[e.Row, "TRANS_AMT_A"]));
                                    break;
                            }
                            break;
                        }
                        else
                            break;
                    case "TRANS_AMT":
                        if (this._sHandling != "2" || !(D.GetDecimal(this._flexL["TRANS_AMT"]) > 0) || !(D.GetDecimal(this._flexL["TRANS_AMT"]) < D.GetDecimal(this._flexH.EditData)))
                            break;
                        this._flexH["TRANS_AMT"] = D.GetString(this._flexH.GetData(e.Row, e.Col));
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexH_AfterEdit(object sender, RowColEventArgs e)
        {
            try
            {
                switch (this._flexH.Cols[e.Col].Name)
                {
                    case "TRANS_AMT":
                        if (!(this._flexH.CDecimal(this._flexH[e.Row, "AMT"]) < this._flexH.CDecimal(this._flexH[e.Row, "TRANS_AMT"])))
                            break;
                        this._flexH[e.Row, "TRANS_AMT"] = this._flexH[e.Row, "AMT"];
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexH_AfterMultyCheck(object sender, MultyCheckEventArgs e)
        {
            try
            {
                this.cur선택건수.DecimalValue = 0;
                this.cur선택금액.DecimalValue = 0;
                this.cur선택이체금액.DecimalValue = 0;

                for (int index = e.StartRow; index <= e.EndRow; ++index)
                {
                    if (this._flexH[index, "S"].ToString() == "Y")
                    {
                        this.cur선택건수.DecimalValue = (this.cur선택건수.DecimalValue + (0 + 1));
                        this.cur선택금액.DecimalValue = (this.cur선택금액.DecimalValue + D.GetDecimal(this._flexH[index, "AMT"]));
                        this.cur선택이체금액.DecimalValue = (this.cur선택이체금액.DecimalValue + D.GetDecimal(this._flexH[index, "TRANS_AMT_A"]));
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 기타 이벤트
        private void SetCellCheck(params string[] chk)
        {
            for (int @fixed = this._flexH.Rows.Fixed; @fixed < this._flexH.Rows.Count; ++@fixed)
            {
                if (this._flexH[@fixed, "S"].ToString() == "Y" && (this._flexH[@fixed, "NO_DOCU"].ToString() == chk[0] && this._flexH[@fixed, "LINE_NO"].ToString() == chk[1] && this._flexH[@fixed, "NODE_CODE"].ToString() == chk[2]))
                    (this._flexH).SetCellCheck(@fixed, 1, CheckEnum.Unchecked);
            }
        }

        private void GridColumnColorSetting()
        {
            for (int fixed1 = this._flexH.Rows.Fixed; fixed1 < this._flexH.Rows.Count; ++fixed1)
            {
                if (D.GetString(this._flexH[fixed1, "TRANS_ACCT_NO"]) == "")
                {
                    for (int fixed2 = this._flexH.Cols.Fixed; fixed2 < this._flexH.Cols.Count; ++fixed2)
                    {
                        CellRange cellRange = this._flexH.GetCellRange(fixed1, fixed2);
                        @cellRange.Style = this._flexH.Styles["계좌없음"];
                    }
                }
            }
        }

        private void PartnerSeachSetting()
        {
            string str1 = string.Empty;
            string str2 = string.Empty;
            DataTable dataTable = this._biz.Setting();

            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                this.ShowMessage("은행연동 환경설정에 셋팅을 해주세요");
            }

            string string1 = D.GetString(dataTable.Rows[0]["TP_SET1"]);
            string string2 = D.GetString(dataTable.Rows[0]["TP_SET3"]);

            if (this.cbo거래구분.SelectedIndex == 0)
            {
                if (string1 != D.GetString(this.cbo거래처조회방식.SelectedValue))
                    this.btn파일생성.Enabled = false;
                else
                    this.btn파일생성.Enabled = true;
            }
            else
            {
                if (this.cbo거래구분.SelectedIndex != 1) return;

                if (string2 != D.GetString(this.cbo거래처조회방식.SelectedValue))
                    this.btn파일생성.Enabled = false;
                else
                    this.btn파일생성.Enabled = true;
            }
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.BizOn.Erpu.BusinessConfig;
using Duzon.BizOn.Erpu.Net.Mail;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.Windows.Print;
using Duzon.ERPU.MF;
using Duzon.Common.Forms.Help;

namespace cz
{
    public partial class P_CZ_FI_BANK_SEND_RPT : PageBase
    {
        #region 초기화
        private P_CZ_FI_BANK_SEND_RPT_BIZ _biz = new P_CZ_FI_BANK_SEND_RPT_BIZ();

        public P_CZ_FI_BANK_SEND_RPT()
        {
            StartUp.Certify(this);
            InitializeComponent();
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

            this._flexH.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            this._flexH.SetCol("TRANS_DATE", "파일작성일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("TRANS_SEQ", "순번", 45);
            this._flexH.SetCol("NM_BANK", "은행", 100);
            this._flexH.SetCol("NO_ACCT", "계좌번호", 120);
            this._flexH.SetCol("TRANS_AMT", "이체원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);

            this._flexH.SettingVersion = "0.0.0.1";
            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flexH.SetDummyColumn(new string[] { "S" });
            #endregion

            #region Line
            this._flexL.BeginSetting(1, 1, false);

            this._flexL.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            this._flexL.SetCol("SEQ", "순번", 60);

            this._flexL.SetCol("SETTLE_DATE", "이체일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexL.SetCol("SETTLE_YN", "이체결과", 45, false, CheckTypeEnum.Y_N);
            
            this._flexL.SetCol("CUST_CODE", "거래처/사원", 100);
            this._flexL.SetCol("CUST_NAME", "거래처명/사원명", 120);
            this._flexL.SetCol("TRANS_BANK_NAME", "지급은행", 60);
            this._flexL.SetCol("TRANS_ACCT_NO", "입금계좌번호", 100);
            this._flexL.SetCol("TRANS_NAME", "예금주", 100);
            this._flexL.SetCol("NM_EXCH", "통화명", 60);
            this._flexL.SetCol("TRANS_AMT_EX", "이체외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("TRANS_AMT", "이체원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flexL.SetCol("CLIENT_NOTE", "보내는사람적요", 160, true);
            this._flexL.SetCol("TRANS_NOTE", "받는사람적요", 160, true);
            this._flexL.SetCol("NM_TP_CHARGE", "해외은행수수료부담", 100, true);
            this._flexL.SetCol("NM_TP_SEND_BY", "송금방법", 100, true);
            this._flexL.SetCol("DC_RELATION", "신청인과의관계", 100, true);

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

            this._flexL.SetCol("NO_HST", "출력횟수", 80);
            this._flexL.SetCol("NM_PRINT", "최종출력자", 100);
            this._flexL.SetCol("DTS_PRINT", "최종출력일자", 120, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            this._flexL.Cols["DTS_PRINT"].Format = "####/##/##/##:##:##";

            this._flexL.SetCodeHelpCol("NM_TP_CHARGE", HelpID.P_MA_CODE_SUB, ShowHelpEnum.Always, new string[] { "TP_CHARGE", "NM_TP_CHARGE" }, new string[] { "CD_SYSDEF", "NM_SYSDEF" });
            this._flexL.SetCodeHelpCol("NM_TP_SEND_BY", HelpID.P_MA_CODE_SUB, ShowHelpEnum.Always, new string[] { "TP_SEND_BY", "NM_TP_SEND_BY" }, new string[] { "CD_SYSDEF", "NM_SYSDEF" });

            this._flexL.SettingVersion = "0.0.0.2";
            this._flexL.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flexL.SetDummyColumn(new string[] { "S" });
            #endregion
        }

        private void InitEvent()
        {
            this.btn계좌번호갱신.Click += new EventHandler(this.btn계좌번호갱신_Click);
            this.btn파일생성.Click += new EventHandler(this.btn파일생성_Click);
            this.btn메일발송.Click += new EventHandler(this.btn메일발송_Click);

            this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
            this._flexH.StartEdit += new RowColEventHandler(this._flexH_StartEdit);
            this._flexH.CheckHeaderClick += new EventHandler(this._flex_CheckHeaderClick);
            this._flexL.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flexL_BeforeCodeHelp);
            this._flexL.ValidateEdit += new ValidateEditEventHandler(this._flexL_ValidateEdit);
            this._flexL.CheckHeaderClick += new EventHandler(this._flex_CheckHeaderClick);
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.oneGrid1.UseCustomLayout = true;
            this.oneGrid1.InitCustomLayout();
            this.bppnl파일작성기간.IsNecessaryCondition = true;

            if (MA.ServerKey(0 != 0, new string[] { "SKPLASMA" }))
                this.btn메일발송.Visible = true;
            if (MA.ServerKey(0 != 0, new string[] { "CHF",
                                                    "KBAM",
                                                    "ENCARSQL",
                                                    "ENCAR" }))
                this._flexL.SetDataMap("TRANS_FLAG", MA.GetCode("FI_Z_CHF08", false), "CODE", "NAME");

            this.dtp파일작성기간.StartDateToString = Global.MainFrame.GetStringFirstDayInMonth;
            this.dtp파일작성기간.EndDateToString = Global.MainFrame.GetStringToday;
        }
        #endregion

        #region 메인버튼 이벤트
        public bool Check()
        {
            if (this.dtp파일작성기간.StartDateToString == "" || this.dtp파일작성기간.StartDateToString == string.Empty)
            {
                this.dtp파일작성기간.Focus();
                return false;
            }
            else
            {
                if (!(this.dtp파일작성기간.EndDateToString == "") && !(this.dtp파일작성기간.EndDateToString == string.Empty))
                    return true;

                this.dtp파일작성기간.Focus();
                return false;
            }
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!this.Check())
                    return;

                this._flexH.Binding = this._biz.Search(new object[] { MA.Login.회사코드,
                                                                      this.dtp파일작성기간.StartDateToString,
                                                                      this.dtp파일작성기간.EndDateToString,
                                                                      this.dtp이체일자.StartDateToString,
                                                                      this.dtp이체일자.EndDateToString });
                if (!this._flexH.HasNormalRow)
                    this.ShowMessage(PageResultMode.SearchNoData);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarDeleteButtonClicked(sender, e);

                if (!this.BeforeDelete()) return;

                DataRow[] dataRowArray = this._flexH.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);
                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    if (this._flexL.DataTable.Select("S = 'Y' AND ISNULL(SETTLE_YN,'') = 'Y'").Length > 0)
                    {
                        this.ShowMessage("이체 완료된 자료는 삭제할 수 없습니다.");
                        return;
                    }

                    if (this.ShowMessage(공통메세지.자료를삭제하시겠습니까) != DialogResult.Yes)
                        return;

                    this._flexH.Redraw = false;
                    this._flexL.Redraw = false;

                    foreach (DataRow dataRow1 in dataRowArray)
                    {
                        foreach (DataRow dataRow2 in this._flexL.DataTable.Select("S = 'Y' AND TRANS_DATE = '" + dataRow1["TRANS_DATE"].ToString() + "' AND TRANS_SEQ = '" + dataRow1["TRANS_SEQ"].ToString() + "'"))
                            dataRow2.Delete();

                        if (this._flexL.DataTable.Select("TRANS_DATE = '" + dataRow1["TRANS_DATE"].ToString() + "' AND TRANS_SEQ = '" + dataRow1["TRANS_SEQ"].ToString() + "'").Length == 0)
                            dataRow1.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this._flexH.Redraw = true;
                this._flexL.Redraw = true;
            }
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSaveButtonClicked(sender, e);

                if (!base.BeforeSave() || !this.MsgAndSave(PageActionMode.Save))
                    return;

                this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            if (!base.SaveData() || !this.Verify()) return false;
            if (!this._flexH.IsDataChanged && !this._flexL.IsDataChanged) return false;

            if (!this._biz.Save(this._flexH.GetChanges(), this._flexL.GetChanges()))
                return false;

            this._flexH.AcceptChanges();
            this._flexL.AcceptChanges();

            return true;
        }

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            DataTable dataTable, dataTable1;
            DataRow[] dataRowArray, dataRowArray1;
            string contents;

            try
            {
                base.OnToolBarPrintButtonClicked(sender, e);

                if (!this._flexH.HasNormalRow) return;
                this.SaveData();

                dataRowArray = this._flexH.DataTable.Select("S = 'Y'");
                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    dataTable = this._flexL.DataTable.Clone();

                    foreach (DataRow dr in dataRowArray)
                    {
                        dataRowArray1 = this._flexL.DataTable.Select("S = 'Y' AND TRANS_DATE = '" + D.GetString(dr["TRANS_DATE"]) + "' AND TRANS_SEQ = '" + D.GetString(dr["TRANS_SEQ"]) + "'");
                        
                        foreach (DataRow dr1 in dataRowArray1)
                        {
                            if (D.GetString(dr1["SETTLE_YN"]) == "Y")
                            {
                                contents = @"** 외화송금신청서 출력 알림

- 출력횟수 : " + (D.GetDecimal(dr1["NO_HST"]) + 1).ToString("0#") + "회" + Environment.NewLine +
"- 출력자 : " + Global.MainFrame.LoginInfo.UserName + Environment.NewLine +
@"
- 출금은행 : " + D.GetString(dr["NM_BANK"]) + Environment.NewLine +
"- 출금계좌 : " + D.GetString(dr["NO_ACCT"]) + Environment.NewLine +
@"
- 입금은행 : " + D.GetString(dr1["TRANS_BANK_NAME"]) + Environment.NewLine +
"- 입금은행(영) : " + D.GetString(dr1["NM_BANK_EN"]) + Environment.NewLine +
"- 입금계좌번호 : " + D.GetString(dr1["TRANS_ACCT_NO"]) + Environment.NewLine +
"- 예금주 : " + D.GetString(dr1["TRANS_NAME"]) + Environment.NewLine +
@"
- 송금사유 : " + D.GetString(dr1["CLIENT_NOTE"]) + Environment.NewLine +
"- 통화명 : " + D.GetString(dr1["NM_EXCH"]) + Environment.NewLine +
"- 이체금액(외화) : " + D.GetString(dr1["TRANS_AMT_EX"]) + Environment.NewLine +
"- 기타 통지사항 : " + D.GetString(dr1["TRANS_NOTE"]) + Environment.NewLine +
@"
※ 본 쪽지는 발신 전용 입니다.";

                                if (Global.MainFrame.LoginInfo.CompanyCode == "K100")
                                    Messenger.SendMSG(new string[] { "S-373", "S-046", "S-306" }, contents);
                            }

                            dataTable.ImportRow(dr1);
                        }
                    }

                    dataTable1 = ComFunc.getGridGroupBy(dataTable, new string[] { "CUST_CODE",
                                                                                  "TP_CHARGE",
                                                                                  "TP_SEND_BY",
                                                                                  "TRANS_ACCT_NO",
                                                                                  "TRANS_NAME",
                                                                                  "NM_EXCH", 
                                                                                  "NM_BANK_EN",
                                                                                  "NM_BANK_NATION",
                                                                                  "NO_SORT",
                                                                                  "NM_DEPOSIT_NATION",
                                                                                  "NO_SWIFT",
                                                                                  "DC_DEPOSIT_TEL",
                                                                                  "DC_DEPOSIT_ADDRESS",
                                                                                  "DC_RELATION",
                                                                                  "NO_BANK_BIC" }, true);
                    dataTable1.Columns.Add("TRANS_AMT_EX");
                    dataTable1.Columns.Add("CLIENT_NOTE");
                    dataTable1.Columns.Add("TRANS_NOTE");

                    foreach (DataRow dr in dataTable1.Rows)
                    {
                        dataRowArray1 = dataTable.Select("CUST_CODE = '" + D.GetString(dr["CUST_CODE"]) + "' AND ISNULL(TP_CHARGE, '') = '" + D.GetString(dr["TP_CHARGE"]) + "' AND ISNULL(TP_SEND_BY, '') = '" + D.GetString(dr["TP_SEND_BY"]) + "'");

                        foreach (DataRow dr1 in dataRowArray1)
                        {
                            dr["TRANS_AMT_EX"] = D.GetDecimal(dr["TRANS_AMT_EX"]) + D.GetDecimal(dr1["TRANS_AMT_EX"]);

                            if (string.IsNullOrEmpty(D.GetString(dr["CLIENT_NOTE"])) && !string.IsNullOrEmpty(D.GetString(dr1["CLIENT_NOTE"])))
                            {
                                dr["CLIENT_NOTE"] = D.GetString(dr1["CLIENT_NOTE"]);
                            }

                            if (!string.IsNullOrEmpty(D.GetString(dr1["TRANS_NOTE"])))
                            {
                                if (string.IsNullOrEmpty(D.GetString(dr["TRANS_NOTE"])))
                                    dr["TRANS_NOTE"] = D.GetString(dr1["TRANS_NOTE"]);
                                else
                                    dr["TRANS_NOTE"] = D.GetString(dr["TRANS_NOTE"]) + "," + D.GetString(dr1["TRANS_NOTE"]);
                            }
                        }
                    }

                    string query = @"SELECT NM_COMPANY,
                                     	    EN_COMPANY,
                                     	    ADS_H,
                                     	    ADS_E,
                                            NO_TEL
                                     FROM MA_COMPANY WITH(NOLOCK) 
                                     WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'";

                    DataRow dataRow = Global.MainFrame.FillDataTable(query).Rows[0];

                    ReportHelper reportHelper = new ReportHelper("R_CZ_FI_BANK_SEND", "송금신청서", true);

                    reportHelper.SetData("회사명", D.GetString(dataRow["NM_COMPANY"]));
                    reportHelper.SetData("회사명(영)", D.GetString(dataRow["EN_COMPANY"]));
                    reportHelper.SetData("회사주소", D.GetString(dataRow["ADS_H"]));
                    reportHelper.SetData("회사주소(영)", D.GetString(dataRow["ADS_E"]));

                    switch (Global.MainFrame.LoginInfo.CompanyCode)
                    {
                        case "K100":
                            reportHelper.SetData("사업자등록번호", "604-81-07065");
                            reportHelper.SetData("회사전화", "051-664-1180");
                            reportHelper.SetData("회사국적", "KOREA");
                            break;
                        case "K200":
                            reportHelper.SetData("사업자등록번호", "605-86-24835");
                            reportHelper.SetData("회사전화", D.GetString(dataRow["NO_TEL"]));
                            reportHelper.SetData("회사국적", "KOREA");
                            break;
                        default:
                            reportHelper.SetData("사업자등록번호", "999-99-99999");
                            reportHelper.SetData("회사전화", D.GetString(dataRow["NO_TEL"]));
                            reportHelper.SetData("회사국적", "");
                            break;
                    }

                    reportHelper.SetDataTable(dataTable1, 1);
                    reportHelper.유저폰트사용();
                    reportHelper.Print();

                    this._biz.SavePrintLog(dataTable);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 그리드 이벤트
        private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                DataTable dataTable1 = null;
                string 파일작성일자 = this._flexH["TRANS_DATE"].ToString();
                string 파일작성순번 = this._flexH["TRANS_SEQ"].ToString();

                if (this._flexH.DetailQueryNeed)
                    dataTable1 = this._biz.SearchDetail(파일작성일자, 파일작성순번);

                string str1 = "TRANS_DATE = '" + 파일작성일자 + "' AND TRANS_SEQ = '" + 파일작성순번 + "'";
                this._flexL.Redraw = false;
                this._flexL.BindingAdd(dataTable1, str1);
                this._flexL.Redraw = true;
                string str2 = string.Empty;

                DataTable dataTable2 = DBHelper.GetDataTable(@"SELECT MAX(BANK_CODE1) AS BANK_CODE1
                                                               FROM BANK_SYS WITH(NOLOCK)  
                                                               WHERE C_CODE = '" + this.LoginInfo.CompanyCode + "'" +
                                                              "AND USE_YN = '1'");

                if (D.GetString(dataTable2.Rows[0][0]) == "27" || D.GetString(dataTable2.Rows[0][0]) == "027")
                    this.btn파일생성.Enabled = true;
                else
                    this.btn파일생성.Enabled = false;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexH_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                DataRow[] dr = this._flexL.DataTable.Select("TRANS_DATE = '" + D.GetString(this._flexH["TRANS_DATE"]) + "' AND TRANS_SEQ = '" + D.GetString(this._flexH["TRANS_SEQ"]) + "'");

                if (this._flexH["S"].ToString() == "N") //클릭하는 순간은 N이므로
                {
                    for (int i = this._flexL.Rows.Fixed; i <= dr.Length + 1; i++)
                        this._flexL.SetCellCheck(i, this._flexL.Cols["S"].Index, CheckEnum.Checked);
                }
                else
                {
                    for (int i = this._flexL.Rows.Fixed; i <= dr.Length + 1; i++)
                    {
                        if (this._flexL.RowState(i) == DataRowState.Deleted) continue;

                        this._flexL.SetCellCheck(i, this._flexL.Cols["S"].Index, CheckEnum.Unchecked);
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flexL_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            FlexGrid grid;

            try
            {
                grid = ((FlexGrid)sender);

                switch (grid.Cols[e.Col].Name)
                {
                    case "NM_TP_CHARGE":
                        e.Parameter.P41_CD_FIELD1 = "CZ_FI00002";
                        break;
                    case "NM_TP_SEND_BY":
                        e.Parameter.P41_CD_FIELD1 = "CZ_FI00003";
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexL_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                FlexGrid grid = sender as FlexGrid;
                if (grid == null) return;

                string ColName = ((FlexGrid)sender).Cols[e.Col].Name;

                if (grid.Cols[e.Col].Name == "S")
                {
                    grid["S"] = (e.Checkbox == CheckEnum.Checked ? "Y" : "N");

                    if (grid.Name == this._flexL.Name)
                    {
                        DataRow[] drArr = grid.DataView.ToTable().Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                        if (drArr.Length != 0)
                            this._flexH.SetCellCheck(this._flexH.Row, this._flexH.Cols["S"].Index, CheckEnum.Checked);
                        else
                            this._flexH.SetCellCheck(this._flexH.Row, this._flexH.Cols["S"].Index, CheckEnum.Unchecked);
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flex_CheckHeaderClick(object sender, EventArgs e)
        {
            try
            {
                FlexGrid flex = sender as FlexGrid;

                if (flex.Name == this._flexH.Name) //상단 그리드 Header Click 이벤트
                {
                    //데이타 테이블의 체크값을 변경해주어도 화면에 보이는 값을 변경시키지 못하므로 화면의 값도 같이 변경시켜줌
                    this._flexH.Row = 1; // 맨처음row에 자동위치하도록 해야 틀어지지 않는다.

                    if (!this._flexH.HasNormalRow || !this._flexL.HasNormalRow) return;

                    for (int h = 0; h < this._flexH.Rows.Count - 1; h++)
                    {
                        this._flexH.Row = h + 1; //Row가 순차적으로 변경되면서 RowChange() 이벤트를 자동 실행하게 유도한다.

                        for (int i = this._flexL.Rows.Fixed; i < this._flexL.Rows.Count; i++)
                        {
                            if (this._flexL.RowState(i) == DataRowState.Deleted) continue;

                            this._flexL[i, "S"] = D.GetString(this._flexH["S"]);
                        }
                    }
                }
                else //하단 그리드 Header Click 이벤트
                {
                    if (!this._flexL.HasNormalRow) return;

                    this._flexH["S"] = D.GetString(this._flexL["S"]);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region 컨드롤 이벤트
        private void btn계좌번호갱신_Click(object sender, EventArgs e)
        {
            DataTable dt;
            DataRow[] dataRowArray;
            string query;

            try
            {
                if (!this._flexH.HasNormalRow) return;
                
                dataRowArray = this._flexL.DataTable.Select("S = 'Y'");
                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    foreach(DataRow dr in dataRowArray)
                    {
                        query = @"SELECT MAX(NO_DEPOSIT) AS NO_DEPOSIT
                                  FROM MA_PARTNER_DEPOSIT WITH(NOLOCK) 
                                  WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                 "AND CD_PARTNER = '" + D.GetString(dr["CUST_CODE"]) + "'" + Environment.NewLine + 
                                 "AND USE_YN = 'Y'";

                        dt = DBHelper.GetDataTable(query);
                        if (dt != null && dt.Rows.Count > 0)
                            dr["TRANS_ACCT_NO"] = D.GetString(dt.Rows[0]["NO_DEPOSIT"]);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn메일발송_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow[] dataRowArray = this._flexL.DataTable.Select("S='Y' AND NO_EMAIL IS NOT NULL");

                if (dataRowArray == null || dataRowArray.Length == 0)
                    return;

                foreach (DataRow dr in dataRowArray)
                    this.SendMail(dr);

                this.ShowMessage("메일전송에 성공했습니다.");
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn파일생성_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray1 = this._flexH.DataTable.Select("S = 'Y'");

            if (dataRowArray1.Length != 1)
            {
                this.ShowMessage("하나의 이체 내역을 선택하여 주십시오.");
            }
            else
            {
                string str1 = "TRANS_DATE = '" + dataRowArray1[0]["TRANS_DATE"] + "' AND TRANS_SEQ = '" + (string)dataRowArray1[0]["TRANS_SEQ"] + "'";
                DataRow[] dataRowArray2 = this._flexL.DataTable.Select(str1);

                try
                {
                    string str2 = string.Empty;
                    DataTable dataTable1 = DBHelper.GetDataTable(@"SELECT USER_ID
                                                                   FROM BANK_SYS WITH(NOLOCK)  
                                                                   WHERE C_CODE = '" + this.LoginInfo.CompanyCode + "'" +
                                                                  "AND USE_YN = '1'");
                    DataTable dataTable2 = new DataTable();
                    string str3 = string.Empty;
                    List<string> list = new List<string>();
                    string path1 = "C:/erp_test/inbound";

                    if (!Directory.Exists(path1))
                        Directory.CreateDirectory(path1);

                    Decimal num2 = 0;

                    for (int index = 0; index < dataTable2.Rows.Count; ++index)
                        num2 += D.GetDecimal(dataTable2.Rows[index]["AM_PAY"]);

                    string string1 = D.GetString(this._flexH["TRANS_DATE"]);
                    string string2 = D.GetString(this._flexH["TRANS_SEQ"]);
                    string path2 = path1 + "\\이체(" + string1 + string2 + ").txt";
                    string str4 = Global.MainFrame.GetStringToday + D.GetString(Global.MainFrame.GetDateTimeToday().TimeOfDay).Replace(":", "").Substring(0, 6);
                    StreamWriter streamWriter = new StreamWriter(path2, false, Encoding.Default);
                    string string3 = D.GetString(this._flexL.DataTable.Compute("SUM(TRANS_AMT)", str1));
                    list.Add("BFTS;" + D.GetString(dataTable1.Rows[0]["USER_ID"]) + ";" + str4);
                    list.Add(Global.MainFrame.GetStringToday + ";" + D.GetString(this._flexH["TRANS_DATE"]) + ";" + D.GetString(this._flexH["TRANS_SEQ"]) + ";CMS이체파일생성;;" + D.GetString(this._flexH["BANK_CODE"]) + ";" + D.GetString(this._flexH["ACCT_NO"]) + ";KRW;;" + dataRowArray2.Length.ToString() + ";" + string3.Split('.')[0] + ";;");

                    foreach (DataRow dataRow in dataRowArray2)
                        list.Add(D.GetString(dataRow["TRANS_BANK_CODE"]) + ";" + D.GetString(dataRow["TRANS_ACCT_NO"]) + ";KRW;" + D.GetString(Math.Truncate(D.GetDecimal(dataRow["TRANS_AMT"]))) + ";;;;;;");

                    for (int index = 0; index < list.Count; ++index)
                        streamWriter.WriteLine(D.GetString(list[index]));

                    streamWriter.Close();

                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = "cmd.exe",
                        Arguments = "/c erp " + path2,
                        UseShellExecute = false,
                        RedirectStandardOutput = true
                    });

                    MsgControl.ShowMsg("파일을 작성중입니다.");
                    Thread.Sleep(5000);
                    MsgControl.CloseMsg();
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.InitialDirectory = "c:\\erp_test\\";
                    saveFileDialog.FileName = "이체(" + string1 + string2 + ").p7m";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        File.Copy("c:\\erp_test\\이체(" + string1 + string2 + ").p7m", saveFileDialog.FileName, true);
                }
                catch (Exception ex)
                {
                    this.MsgEnd(ex);
                }
            }
        }

        private void SendMail(DataRow dr)
        {
            string senderEmail = this._biz.Sender_EMail();

            Duzon.BizOn.Erpu.Net.Mail.MailMessage mailMessage = new Duzon.BizOn.Erpu.Net.Mail.MailMessage();
            mailMessage.Server = BusinessInfo.ServerInfo.SMTP;
            mailMessage.From = new MailAddress(senderEmail, MA.Login.사원명);
            mailMessage.To.Add(dr["NO_EMAIL"].ToString());
            mailMessage.Subject = "인명계좌에 송금이 되어 안내드립니다.";
            mailMessage.SubjectEncoding = Encoding.UTF8;

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("안녕하세요.");
            stringBuilder.AppendLine("인명계좌에 송금이 되어 안내드립니다.");
            stringBuilder.AppendLine("은행 : " + dr["TRANS_BANK_NAME"].ToString());
            stringBuilder.AppendLine("계좌 : " + dr["TRANS_ACCT_NO"].ToString());
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("금액 : " + D.GetDecimal(dr["TRANS_AMT"]).ToString("##,###") + " 원");
            stringBuilder.AppendLine("내역 : " + dr["CLIENT_NOTE"].ToString());
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("추가 문의사항 있으시면 연락 부탁드립니다.");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("감사합니다.");
            stringBuilder.AppendLine(MA.Login.사용자명 + " 드림");

            mailMessage.Body = stringBuilder.ToString();
            mailMessage.BodyEncoding = Encoding.UTF8;

            new MailSenderFactory().CreateMailSenderFormInstance().Send(mailMessage);
        }
        #endregion
    }
}
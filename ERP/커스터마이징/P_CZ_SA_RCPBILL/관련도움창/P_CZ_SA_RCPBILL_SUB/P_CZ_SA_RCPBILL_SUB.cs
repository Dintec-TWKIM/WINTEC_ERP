using System;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.OLD;

namespace cz
{
    public partial class P_CZ_SA_RCPBILL_SUB : Duzon.Common.Forms.CommonDialog, IHelpWindow
    {
        private object[] _Return = new object[1];
        public decimal 총수금원화 = 0;
        public decimal 반제액원화 = 0;
        public decimal 총수금 = 0;
        public decimal 반제액 = 0;
        public string 통화코드 = string.Empty;
        public string 통화명 = string.Empty;
        public decimal 환율 = 0;
        public string 거래구분코드 = string.Empty;
        public string 거래구분명 = string.Empty;
        public string 매출처코드 = string.Empty;
        public string 매출처명 = string.Empty;
        public string 수금처코드 = string.Empty;
        public string 수금처명 = string.Empty;
        public string 프로젝트코드 = string.Empty;
        public string 프로젝트명 = string.Empty;
        public string 사업장코드 = string.Empty;
        public DataTable dt매출반제 = null;
        private string 매출구분 = string.Empty;
        private string 수금일자;
        private string 수금형태;

        object[] IHelpWindow.ReturnValues
        {
            get
            {
                return this._Return;
            }
        }

        public DataTable GetDataTable
        {
            get
            {
                return (DataTable)this._Return[0];
            }
        }

        public string Set매출구분
        {
            set
            {
                this.매출구분 = value;
            }
        }

        public P_CZ_SA_RCPBILL_SUB()
        {
            this.InitializeComponent();
        }

        public P_CZ_SA_RCPBILL_SUB(string 수금일자, string 수금형태)
        {
            this.dt매출반제 = this.매출반제();
            this.수금일자 = 수금일자;
            this.수금형태 = 수금형태;
            this.InitializeComponent();
        }

        public P_CZ_SA_RCPBILL_SUB(DataTable 매출반제, string 수금일자, string 수금형태)
        {
            this.dt매출반제 = 매출반제;
            this.수금일자 = 수금일자;
            this.수금형태 = 수금형태;
            this.InitializeComponent();
        }

        protected override void InitLoad()
        {
            try
            {
                base.InitLoad();

                this.InitGrid();
                this.InitEvent();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);

            this._flex.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("NO_IV", "계산서번호", 100, 20);
            this._flex.SetCol("CD_PJT", "프로젝트", 100);
            this._flex.SetCol("NO_DOCU", "전표번호", 100);
            this._flex.SetCol("NO_DOLINE", "전표라인", 100);
            this._flex.SetCol("DT_PROCESS", "발행일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("NM_EXCH", "통화명", 80, false);
            this._flex.SetCol("RT_EXCH", "환율", 60, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flex.SetCol("AM_EX", "공급가액", 115, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex.SetCol("AM_DOCU", "공급가액(원화)", 115, false, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("AM_BAN_EX", "기반제액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex.SetCol("AM_BAN", "기반제액(원화)", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("AM_RCP_EX", "반제대상액", 100, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex.SetCol("AM_RCP", "반제대상액(원화)", 115, true, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("AM_PL_LOSS", "환차손(-원화)", 115, true, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("AM_PL_PROFIT", "환차익(+원화)", 115, true, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("AM_RCP_JAN_EX", "반제잔액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex.SetCol("AM_RCP_JAN", "반제잔액(원화)", 115, false, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("AM_PL", "환차손익(원화)", 115, false, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("NM_DEPT", "마감부서", 100, 100);
            this._flex.SetCol("NM_EMP", "마감담당자", 100, 100);

            this._flex.SetExceptEditCol(new string[] { "NM_DEPT", "NM_EMP" });
            this._flex.SetDummyColumn("S");

            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            this._flex.LoadUserCache("P_CZ_SA_RCPBILL_SUB_flex");
        }

        private void InitEvent()
        {
            this._flex.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);
            this._flex.AfterEdit += new RowColEventHandler(this._flex_AfterEdit);

            this.btn조회.Click += new EventHandler(this.btn조회_Click);
            this.btn적용.Click += new EventHandler(this.btn적용_Click);
            this.btn자동채움.Click += new EventHandler(this.btn자동채움_Click);
            this.btn자동할당.Click += new EventHandler(this.btn자동할당_Click);
        }

        private void InitControl()
        {
            this.dtp기간.StartDateToString = new CommonFunction().DateAdd(Global.MainFrame.GetStringToday, "M", -3);
            this.dtp기간.EndDateToString = Global.MainFrame.GetStringToday;

            this.cur총수금원화.DecimalValue = this.총수금원화;
            this.cur반제액원화.DecimalValue = this.반제액원화;
            this.cur잔액원화.DecimalValue = this.총수금원화 - this.반제액원화;
            this.txt통화명.Text = this.통화명;
            this.cur환율.DecimalValue = this.환율;
            this.cur총수금.DecimalValue = this.총수금;
            this.cur반제액.DecimalValue = this.반제액;
            this.cur잔액.DecimalValue = this.총수금 - this.반제액;
            this.txt거래구분.Text = this.거래구분명;
            this.txt매출처.Text = this.매출처명;
            this.txt수금처.Text = this.수금처명;
            this.txt프로젝트.Text = this.프로젝트명;
        }

        protected override void InitPaint()
        {
            base.InitPaint();
            this.OneGrid1.UseCustomLayout = true;
            this.OneGrid1.InitCustomLayout();

            this.InitControl();
            this.btn조회_Click(null, null);
        }

        private bool BeforeSearch()
        {
            if (this.dtp기간.StartDateToString == string.Empty)
            {
                Global.MainFrame.ShowMessage("WK1_004", this.lbl기간.Text);
                this.dtp기간.Focus();
                return false;
            }
            if (this.dtp기간.EndDateToString == string.Empty)
            {
                Global.MainFrame.ShowMessage("WK1_004", this.lbl기간.Text);
                this.dtp기간.Focus();
                return false;
            }

            return true;
        }

        private void btn조회_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeSearch()) return;

                this._flex.Binding = DBHelper.GetDataTable("SP_CZ_SA_RCPBILL_SUB_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                    this.dtp기간.StartDateToString,
                                                                                                    this.dtp기간.EndDateToString,
                                                                                                    this.매출처코드,
                                                                                                    this.거래구분코드,
                                                                                                    this.ctx마감부서.CodeValue,
                                                                                                    this.ctx마감담당자.CodeValue,
                                                                                                    this.수금일자,
                                                                                                    this.수금형태 });
                if (!this._flex.HasNormalRow)
                {
                    Global.MainFrame.ShowMessage("IK1_003");
                }

                int count = this.dt매출반제.Rows.Count;
                
                for (int index = 0; index < count; ++index)
                {
                    foreach (DataRow dataRow in this._flex.DataTable.Select("NO_DOCU = '" + D.GetString(this.dt매출반제.Rows[index]["NO_DOCU"]) + "' AND NO_DOLINE = '" + D.GetString(this.dt매출반제.Rows[index]["NO_DOLINE"]) + "'"))
                    {
                        dataRow["AM_BAN_EX"] = (D.GetDecimal(dataRow["AM_BAN_EX"]) + D.GetDecimal(this.dt매출반제.Rows[index]["AM_BAN_EX"]));
                        dataRow["AM_BAN"] = (D.GetDecimal(dataRow["AM_BAN"]) + D.GetDecimal(this.dt매출반제.Rows[index]["AM_BAN"]));
                        dataRow["AM_RCP_JAN_EX"] = (D.GetDecimal(dataRow["AM_RCP_JAN_EX"]) - D.GetDecimal(this.dt매출반제.Rows[index]["AM_BAN_EX"]));
                        dataRow["AM_RCP_JAN"] = (D.GetDecimal(dataRow["AM_RCP_JAN"]) - D.GetDecimal(this.dt매출반제.Rows[index]["AM_BAN"]));
                    }
                }

                this.잔액계산();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        public void btn적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow) return;

                if (this.반제액체크() && Global.MainFrame.ShowMessage("반제원화 금액이 총수금액 보다 많습니다. 그래도 적용하시겠습니까?", "QY2") == DialogResult.No)
                    return;

                if (this.반제액체크2() && Global.MainFrame.ShowMessage("반제외화 금액이 총수금액 보다 많습니다. 그래도 적용하시겠습니까?", "QY2") == DialogResult.No)
                    return;

                 for (int @fixed = this._flex.Rows.Fixed; @fixed < this._flex.Rows.Count; ++@fixed)
                 {
                     if (Math.Abs(Convert.ToDecimal(this._flex[@fixed, "AM_RCP_EX"])) > 0)
                     {
                         if (this._flex.GetCellCheck(@fixed, this._flex.Cols["S"].Index) == CheckEnum.Unchecked)
                         {
                             this._flex[@fixed, "S"] = "Y";
                             this._flex.SetCellCheck(@fixed, this._flex.Cols["S"].Index, CheckEnum.Checked);
                         }
                     }
                     else if (this._flex.GetCellCheck(@fixed, this._flex.Cols["S"].Index) == CheckEnum.Checked)
                     {
                         this._flex[@fixed, "S"] = "N";
                         this._flex.SetCellCheck(@fixed, this._flex.Cols["S"].Index, CheckEnum.Unchecked);
                     }
                 }

                 DataTable dataTable = this._flex.DataTable.Clone();
                 DataRow[] dataRowArray = this._flex.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                 if (dataRowArray == null || dataRowArray.Length == 0)
                 {
                     Global.MainFrame.ShowMessage("반제대상액 또는 선택된 자료가 없습니다.");
                 }
                 else
                 {
                     this.잔액계산();

                     foreach (DataRow row in dataRowArray)
                     {
                         dataTable.ImportRow(row);
                     }

                     this._Return[0] = dataTable;
                     this.DialogResult = DialogResult.OK;
                 }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private bool IsDocuApply(string noIv)
        {
            DataTable dataTable = DBHelper.GetDataTable(@"SELECT TOP 1 ST_DOCU 
                                                          FROM FI_DOCU WITH(NOLOCK) 
                                                          WHERE CD_COMPANY = '" + MA.Login.회사코드 + "'" + Environment.NewLine + 
                                                         "AND NO_MDOCU = '" + noIv + "'");
            return dataTable.Rows.Count != 0 && !(D.GetString(dataTable.Rows[0]["ST_DOCU"]) == "1");
        }

        private void _flex_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                string str = this._flex.GetData(e.Row, e.Col).ToString();
                string editData = this._flex.EditData;
                if (str.ToUpper() == editData.ToUpper()) return;

                string name = ((C1FlexGridBase)sender).Cols[e.Col].Name;
                
                if (name == "AM_PL_LOSS")
                {
                    if (Convert.ToDecimal(this._flex["AM_PL_LOSS"]) > 0)
                    {
                        Global.MainFrame.ShowMessage("입력형식은 음수(-)입니다.");
                        if (this._flex.Editor != null)
                            this._flex.Editor.Text = str;
                        this._flex["AM_PL_LOSS"] = str;
                        return;
                    }

                    this._flex["AM_PL"] = (0 - Convert.ToDecimal(this._flex["AM_PL_LOSS"]));
                    this._flex["AM_PL_PROFIT"] = 0;
                }
                else if (name == "AM_PL_PROFIT")
                {
                    if (Convert.ToDecimal(this._flex["AM_PL_PROFIT"]) < 0)
                    {
                        Global.MainFrame.ShowMessage("입력형식은 양수(+)입니다.");
                        if (this._flex.Editor != null)
                            this._flex.Editor.Text = str;
                        this._flex["AM_PL_PROFIT"] = str;
                        return;
                    }

                    this._flex["AM_PL"] = (0 - Convert.ToDecimal(this._flex[ "AM_PL_PROFIT"]));
                    this._flex["AM_PL_LOSS"] = 0;
                }

                if (name != "AM_RCP_EX" && name != "AM_RCP")
                    return;

                if (Convert.ToDecimal(this._flex["AM_RCP_EX"]) * Convert.ToDecimal(this._flex["AM_RCP_JAN_EX"]) < 0)
                {
                    Global.MainFrame.ShowMessage("반제금액은 반제잔액과 부호가 일치해야합니다.");
                    if (this._flex.Editor != null)
                        this._flex.Editor.Text = str;
                    this._flex["AM_RCP_EX"] = str;
                }
                else if (Math.Abs(Convert.ToDecimal(this._flex["AM_RCP_EX"])) > Math.Abs(Convert.ToDecimal(this._flex["AM_RCP_JAN_EX"])))
                {
                    Global.MainFrame.ShowMessage("SA_M000127");
                    if (this._flex.Editor != null)
                        this._flex.Editor.Text = str;
                    this._flex["AM_RCP_EX"] = str;
                }
                else
                {
                    if (name == "AM_RCP_EX")
                    {
                        this._flex["AM_RCP_EX"] = Convert.ToDecimal(editData);

                        this._flex["AM_RCP"] = this.반제원화(this._flex.CDecimal(editData), this._flex.CDecimal(this._flex["RT_EXCH_CUR"]));
                        this._flex["AM_PL"] = this.환차손익(this._flex.CDecimal(editData), this._flex.CDecimal(this._flex["RT_EXCH"]), this._flex.CDecimal(this._flex["RT_EXCH_CUR"]));
                        
                        if (Convert.ToDecimal(this._flex["AM_PL"]) >= 0)
                        {
                            this._flex["AM_PL_LOSS"] = (0 - Convert.ToDecimal(this._flex["AM_PL"]));
                            this._flex["AM_PL_PROFIT"] = 0;
                        }
                        else
                        {
                            this._flex["AM_PL_LOSS"] = 0;
                            this._flex["AM_PL_PROFIT"] = (0 - Convert.ToDecimal(this._flex["AM_PL"]));
                        }
                    }

                    if (name == "AM_RCP")
                    {
                        this._flex["AM_RCP"] = Convert.ToDecimal(editData);

                        if (this._flex.CDecimal(this._flex["AM_RCP_EX"]) == this._flex.CDecimal(this._flex["AM_RCP_JAN_EX"]))
                            this._flex["AM_PL"] = (this._flex.CDecimal(this._flex["AM_RCP_JAN"]) - this._flex.CDecimal(this._flex["AM_RCP"]));
                        else
                            this._flex["AM_PL"] = this.환차손익2(this._flex.CDecimal(this._flex["AM_RCP_EX"]), this._flex.CDecimal(this._flex["RT_EXCH"]), this._flex.CDecimal(this._flex["AM_RCP"]));
                        
                        if (Convert.ToDecimal(this._flex["AM_PL"]) >= 0)
                        {
                            this._flex["AM_PL_LOSS"] = (0 - Convert.ToDecimal(this._flex["AM_PL"]));
                            this._flex["AM_PL_PROFIT"] = 0;
                        }
                        else
                        {
                            this._flex["AM_PL_LOSS"] = 0;
                            this._flex["AM_PL_PROFIT"] = (0 - Convert.ToDecimal(this._flex["AM_PL"]));
                        }
                    }

                    if (Convert.ToDecimal(this._flex["AM_RCP_EX"]) == 0)
                    {
                        if (this._flex.GetCellCheck(e.Row, this._flex.Cols["S"].Index) == CheckEnum.Checked)
                            this._flex.SetCellCheck(e.Row, this._flex.Cols["S"].Index, CheckEnum.Unchecked);
                    }
                    else if (this._flex.GetCellCheck(e.Row, this._flex.Cols["S"].Index) == CheckEnum.Unchecked)
                        this._flex.SetCellCheck(e.Row, this._flex.Cols["S"].Index, CheckEnum.Checked);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flex_AfterEdit(object sender, RowColEventArgs e)
        {
            try
            {
                this.잔액계산();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private bool 반제액체크()
        {
            return (Math.Abs(this.반제액원화구하기()) > Math.Abs(this.cur총수금원화.DecimalValue));
        }

        private bool 반제액체크2()
        {
            return (Math.Abs(this.반제액구하기()) != Math.Abs(this.cur총수금.DecimalValue));
        }

        private decimal 반제액구하기()
        {
            return D.GetDecimal(this._flex.DataTable.Compute("SUM(AM_RCP_EX)", string.Empty));
        }

        private decimal 반제액원화구하기()
        {
            return D.GetDecimal(this._flex.DataTable.Compute("SUM(AM_RCP)", string.Empty));
        }

        private void btn자동채움_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;
            decimal 수금외화금액, 수금원화금액, 반제외화금액, 반제원화금액;

            try
            {
                if (this._flex.HasNormalRow == false) return;

                dataRowArray = this._flex.DataTable.Select("S = 'Y'");

                if (dataRowArray != null && dataRowArray.Length > 0)
                {
                    수금외화금액 = this.cur총수금.DecimalValue;
                    수금원화금액 = this.cur총수금원화.DecimalValue;

                    반제외화금액 = D.GetDecimal(this._flex.DataTable.Compute("SUM(AM_RCP_EX)", "S = 'Y'"));
                    반제원화금액 = D.GetDecimal(this._flex.DataTable.Compute("SUM(AM_RCP)", "S = 'Y'"));

                    foreach (DataRow row in dataRowArray)
                    {
                        row["AM_RCP_EX"] = row["AM_RCP_JAN_EX"];
                        반제외화금액 += D.GetDecimal(row["AM_RCP_EX"]);

                        if (수금외화금액 == 반제외화금액)
                            row["AM_RCP"] = (수금원화금액 - 반제원화금액);
                        else
                        {
                            row["AM_RCP"] = this.반제원화(this._flex.CDecimal(row["AM_RCP_EX"]), this._flex.CDecimal(row["RT_EXCH_CUR"]));
                            반제원화금액 += D.GetDecimal(row["AM_RCP"]);
                        }

                        row["AM_PL"] = (D.GetDecimal(row["AM_RCP_JAN"]) - D.GetDecimal(row["AM_RCP"]));

                        if (Convert.ToDecimal(row["AM_PL"]) >= 0)
                        {
                            row["AM_PL_LOSS"] = (0 - Convert.ToDecimal(row["AM_PL"]));
                            row["AM_PL_PROFIT"] = 0;
                        }
                        else
                        {
                            row["AM_PL_LOSS"] = 0;
                            row["AM_PL_PROFIT"] = (0 - Convert.ToDecimal(row["AM_PL"]));
                        }
                    }

                    this.잔액계산();

                    this._flex.SumRefresh();
                }
                else
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn자동할당_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray1 = this._flex.DataTable.Select("", "DT_PROCESS ASC, NO_IV ASC");

            foreach (DataRow dataRow in dataRowArray1)
            {
                dataRow["AM_RCP_EX"] = 0;
                dataRow["AM_RCP"] = 0;
                dataRow["AM_PL"] = 0;
                dataRow["AM_PL_LOSS"] = 0;
                dataRow["AM_PL_PROFIT"] = 0;
                dataRow["S"] = "N";
            }

            DataRow[] dataRowArray2 = this._flex.DataTable.Select("IV_GUBUN = '0'", " DT_PROCESS ASC, NO_IV ASC");
            this._flex.DataTable.Select("IV_GUBUN = '1'", " DT_PROCESS ASC, NO_IV ASC ");

            this.잔액계산();

            if (this.cur총수금.DecimalValue < 0)
            {
                foreach (DataRow dataRow in dataRowArray2)
                {
                    if (this.cur잔액.DecimalValue > 0 && this.cur잔액원화.DecimalValue > 0)
                    {
                        if (Math.Abs(D.GetDecimal(dataRow["AM_RCP_JAN"]) - D.GetDecimal(dataRow["AM_RCP"])) > 0)
                        {
                            if (Math.Abs(D.GetDecimal(dataRow["AM_RCP_JAN"]) - D.GetDecimal(dataRow["AM_RCP"])) < this.cur잔액원화.DecimalValue)
                            {
                                dataRow["AM_RCP_EX"] = (D.GetDecimal(dataRow["AM_RCP_EX"]) + Math.Abs(D.GetDecimal(dataRow["AM_RCP_JAN_EX"]) - D.GetDecimal(dataRow["AM_RCP_EX"])) * -1);
                                dataRow["S"] = "Y";
                            }
                            else if (Math.Abs(D.GetDecimal(dataRow["AM_RCP_JAN"]) - D.GetDecimal(dataRow["AM_RCP"])) >= this.cur잔액원화.DecimalValue)
                            {
                                dataRow["AM_RCP_EX"] = (D.GetDecimal(dataRow["AM_RCP_EX"]) + this.cur잔액.DecimalValue);
                                dataRow["S"] = "Y";
                            }

                            dataRow["AM_RCP"] = this.반제원화(this._flex.CDecimal(dataRow["AM_RCP_EX"]), this._flex.CDecimal(dataRow["RT_EXCH_CUR"]));
                            dataRow["AM_PL"] = this.환차손익(this._flex.CDecimal(dataRow["AM_RCP_EX"]), this._flex.CDecimal(dataRow["RT_EXCH"]), this._flex.CDecimal(dataRow["RT_EXCH_CUR"]));

                            if (Convert.ToDecimal(dataRow["AM_PL"]) >= 0)
                            {
                                dataRow["AM_PL_LOSS"] = (0 - Convert.ToDecimal(dataRow["AM_PL"]));
                                dataRow["AM_PL_PROFIT"] = 0;
                            }
                            else
                            {
                                dataRow["AM_PL_LOSS"] = 0;
                                dataRow["AM_PL_PROFIT"] = (0 - Convert.ToDecimal(dataRow["AM_PL"]));
                            }

                            this.잔액계산();
                        }
                    }
                    else
                        break;
                }
            }
            else
            {
                foreach (DataRow dataRow in dataRowArray1)
                {
                    if (this.cur잔액.DecimalValue > 0 && this.cur잔액원화.DecimalValue > 0)
                    {
                        if (Math.Abs(D.GetDecimal(dataRow["AM_RCP_JAN"]) - D.GetDecimal(dataRow["AM_RCP"])) < 0)
                        {
                            dataRow["AM_RCP_EX"] = D.GetDecimal(dataRow["AM_RCP_JAN_EX"]);
                            dataRow["S"] = "Y";
                        }
                        else if (D.GetDecimal(dataRow["AM_RCP_JAN"]) - D.GetDecimal(dataRow["AM_RCP"]) < this.cur잔액원화.DecimalValue)
                        {
                            dataRow["AM_RCP_EX"] = D.GetDecimal(dataRow["AM_RCP_JAN_EX"]);
                            dataRow["S"] = "Y";
                        }
                        else if (D.GetDecimal(dataRow["AM_RCP_JAN"]) - D.GetDecimal(dataRow["AM_RCP"]) >= this.cur잔액원화.DecimalValue)
                        {
                            dataRow["AM_RCP_EX"] = (D.GetDecimal(dataRow["AM_RCP_EX"]) + this.cur잔액.DecimalValue);
                            dataRow["S"] = "Y";
                        }

                        dataRow["AM_RCP"] = this.반제원화(this._flex.CDecimal(dataRow["AM_RCP_EX"]), this._flex.CDecimal(dataRow["RT_EXCH_CUR"]));
                        dataRow["AM_PL"] = this.환차손익(this._flex.CDecimal(dataRow["AM_RCP_EX"]), this._flex.CDecimal(dataRow["RT_EXCH"]), this._flex.CDecimal(dataRow["RT_EXCH_CUR"]));

                        if (Convert.ToDecimal(dataRow["AM_PL"]) >= 0)
                        {
                            dataRow["AM_PL_LOSS"] = (0 - Convert.ToDecimal(dataRow["AM_PL"]));
                            dataRow["AM_PL_PROFIT"] = 0;
                        }
                        else
                        {
                            dataRow["AM_PL_LOSS"] = 0;
                            dataRow["AM_PL_PROFIT"] = (0 - Convert.ToDecimal(dataRow["AM_PL"]));
                        }

                        this.잔액계산();
                    }
                    else
                        break;
                }

                if (반제액 < 0)
                {
                    Global.MainFrame.ShowMessage("반제할 대상액이 부족합니다.");

                    foreach (DataRow dataRow in dataRowArray1)
                    {
                        dataRow["AM_RCP_EX"] = 0;
                        dataRow["AM_RCP"] = 0;
                        dataRow["AM_PL"] = 0;
                        dataRow["AM_PL_LOSS"] = 0;
                        dataRow["AM_PL_PROFIT"] = 0;
                        dataRow["S"] = "N";
                    }

                    this.잔액계산();
                }
            }

            this._flex.Sort(SortFlags.Ascending, 2);
        }

        protected override void OnClosed(EventArgs e)
        {
            this._flex.SaveUserCache("P_CZ_SA_RCPBILL_SUB_flex");
        }

        internal DataTable 매출반제()
        {
            return new DataTable()
            {
                Columns = { { "NO_DOCU", typeof (string) },
                            { "NO_DOLINE", typeof (string) },
                            { "AM_BAN_EX", typeof (decimal) },
                            { "AM_BAN", typeof (decimal) } }
            };
        }

        private void 잔액계산()
        {
            this.cur반제액.DecimalValue = this.반제액구하기();
            this.cur잔액.DecimalValue = this.cur총수금.DecimalValue - this.cur반제액.DecimalValue;
            this.cur반제액원화.DecimalValue = this.반제액원화구하기();
            this.cur잔액원화.DecimalValue = this.cur총수금원화.DecimalValue - this.cur반제액원화.DecimalValue;
        }

        private decimal 반제원화(decimal 외화금액, decimal 환율)
        {
            try
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    return Decimal.Round(외화금액 * 환율, 2, MidpointRounding.AwayFromZero);
                else
                    return Decimal.Round(외화금액 * 환율, MidpointRounding.AwayFromZero);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return 0;
        }

        private decimal 환차손익(decimal 외화금액, decimal 대상환율, decimal 반제환율)
        {
            decimal 대상금액, 반제금액;

            try
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                {
                    대상금액 = Decimal.Round(외화금액 * 대상환율, 2, MidpointRounding.AwayFromZero);
                    반제금액 = Decimal.Round(외화금액 * 반제환율, 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    대상금액 = Decimal.Round(외화금액 * 대상환율, MidpointRounding.AwayFromZero);
                    반제금액 = Decimal.Round(외화금액 * 반제환율, MidpointRounding.AwayFromZero);
                }

                return 대상금액 - 반제금액;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return 0;
        }

        private decimal 환차손익2(decimal 외화금액, decimal 대상환율, decimal 원화금액)
        {
            decimal 대상금액, 반제금액;

            try
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                {
                    대상금액 = Decimal.Round(외화금액 * 대상환율, 2, MidpointRounding.AwayFromZero);
                    반제금액 = Decimal.Round(원화금액, 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    대상금액 = Decimal.Round(외화금액 * 대상환율, MidpointRounding.AwayFromZero);
                    반제금액 = Decimal.Round(원화금액, MidpointRounding.AwayFromZero);
                }

                return 대상금액 - 반제금액;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return 0;
        }
    }
}

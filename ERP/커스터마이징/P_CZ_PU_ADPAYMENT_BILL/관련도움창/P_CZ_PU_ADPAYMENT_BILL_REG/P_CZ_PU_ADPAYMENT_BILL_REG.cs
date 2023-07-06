using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;

namespace cz
{
    public partial class P_CZ_PU_ADPAYMENT_BILL_REG : Duzon.Common.Forms.CommonDialog
    {
        public DataTable resultDt;
        private DataTable 선지급반제;
        private string 거래구분;
        private string 통화명;

        public P_CZ_PU_ADPAYMENT_BILL_REG()
        {
            InitializeComponent();
        }

        public P_CZ_PU_ADPAYMENT_BILL_REG(DataTable 선지급반제, string 거래구분, string 매입처코드, string 매입처, string 통화명, decimal 환율, decimal 총지급, decimal 총지급원화)
        {
            InitializeComponent();

            this.선지급반제 = 선지급반제;
            this.거래구분 = 거래구분;
            this.통화명 = 통화명;

            this.ctx매입처.CodeValue = 매입처코드;
            this.ctx매입처.CodeName = 매입처;

            this.cur환율.DecimalValue = 환율;

            this.cur총지급.DecimalValue = 총지급;
            this.cur반제액.DecimalValue = 0;
            this.cur잔액.DecimalValue = 총지급;

            this.cur총지급원화.DecimalValue = 총지급원화;
            this.cur반제액원화.DecimalValue = 0;
            this.cur잔액원화.DecimalValue = 총지급원화;
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, true);

            this._flex.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("NO_IV", "매입번호", 100, 20);
            this._flex.SetCol("CD_PJT", "프로젝트", 100);
            this._flex.SetCol("NO_DOCU", "전표번호", 100);
            this._flex.SetCol("NO_DOLINE", "전표번호", 100);
            this._flex.SetCol("DT_PROCESS", "매입일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("NM_EXCH", "통화명", 60);
            this._flex.SetCol("RT_EXCH", "환율", 45, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flex.SetCol("AM_EX", "외화금액", 115, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex.SetCol("AM_DOCU", "원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
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

            this._flex.SetDummyColumn(new string[] { "S" });
            this._flex.SetExceptEditCol(new string[] { "NM_DEPT", "NM_EMP" });

            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex.SetExceptSumCol(new string[] { "RT_EXCH" });
        }

        private void InitEvent()
        {
            this.btn자동채움.Click += new EventHandler(this.btn자동채움_Click);
            this.btn자동할당.Click += new EventHandler(this.btn자동할당_Click);
            this.btn조회.Click += new EventHandler(this.btn조회_Click);
            this.btn적용.Click += new EventHandler(this.btn적용_Click);

            this._flex.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);
            this._flex.AfterEdit += new RowColEventHandler(this._flex_AfterEdit);
        }

        protected override void InitPaint()
        {
            DataSet ds;

            try
            {
                base.InitPaint();

                this.dtp매입일자.StartDate = Global.MainFrame.GetDateTimeToday().AddMonths(-3);
                this.dtp매입일자.EndDate = Global.MainFrame.GetDateTimeToday();

                ds = Global.MainFrame.GetComboData(new string[] { "N;PU_C000016", "N;MA_B000005" });

                this.cbo거래구분.DataSource = ds.Tables[0];
                this.cbo거래구분.ValueMember = "CODE";
                this.cbo거래구분.DisplayMember = "NAME";
                this.cbo거래구분.SelectedValue = this.거래구분;

                this.cbo통화명.DataSource = ds.Tables[1];
                this.cbo통화명.ValueMember = "CODE";
                this.cbo통화명.DisplayMember = "NAME";
                this.cbo통화명.SelectedValue = this.통화명;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
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
                    수금외화금액 = this.cur총지급.DecimalValue;
                    수금원화금액 = this.cur총지급원화.DecimalValue;

                    반제외화금액 = D.GetDecimal(this._flex.DataTable.Compute("SUM(AM_RCP_EX)", "S = 'Y'"));
                    반제원화금액 = D.GetDecimal(this._flex.DataTable.Compute("SUM(AM_RCP)", "S = 'Y'"));

                    foreach (DataRow row in dataRowArray)
                    {
                        row["AM_RCP_EX"] = row["AM_RCP_JAN_EX"];

                        반제외화금액 += D.GetDecimal(row["AM_RCP_EX"]);

                        if ((this.cur환율.DecimalValue == D.GetDecimal(row["RT_EXCH"])) && (수금외화금액 == 반제외화금액))
                            row["AM_RCP"] = (수금원화금액 - 반제원화금액);
                        else
                        {
                            row["AM_RCP"] = this.반제원화(this._flex.CDecimal(row["AM_RCP_EX"]), this._flex.CDecimal(this._flex["RT_EXCH"]));
                            반제원화금액 += D.GetDecimal(row["AM_RCP"]);
                        }

                        row["AM_PL"] = this.환차손익(this._flex.CDecimal(row["AM_RCP_EX"]), this.cur환율.DecimalValue, this._flex.CDecimal(this._flex["RT_EXCH"]));

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
            try
            {
                if (this._flex.HasNormalRow == false) return;
                
                DataRow[] dataRowArray1 = this._flex.DataTable.Select("", " DT_PROCESS ASC, NO_IV ASC ");

                foreach (DataRow dataRow in dataRowArray1)
                {
                    dataRow["AM_RCP_EX"] = 0;
                    dataRow["AM_RCP"] = 0;
                    dataRow["AM_PL"] = 0;
                    dataRow["AM_PL_LOSS"] = 0;
                    dataRow["AM_PL_PROFIT"] = 0;
                    dataRow["S"] = "N";
                }

                DataRow[] dataRowArray2 = this._flex.DataTable.Select("IV_GUBUN = '0'", " DT_PROCESS ASC, NO_IV ASC ");
                this._flex.DataTable.Select("IV_GUBUN = '1'", " DT_PROCESS ASC, NO_IV ASC ");

                this.cur반제액.DecimalValue = this.반제액구하기();
                this.cur잔액.DecimalValue = (this.cur잔액.DecimalValue - this.cur반제액.DecimalValue);

                Decimal decimalValue1 = this.cur잔액.DecimalValue;
                Decimal decimalValue2 = this.cur반제액.DecimalValue;
                Decimal num4 = decimalValue1 - decimalValue2;

                if (decimalValue1 < 0)
                {
                    foreach (DataRow dataRow in dataRowArray2)
                    {
                        if (!(Math.Abs(num4) <= 0))
                        {
                            if (!(Math.Abs(this._flex.CDecimal(dataRow["AM_RCP_JAN_EX"]) - this._flex.CDecimal(dataRow["AM_RCP_EX"])) <= 0))
                            {
                                if (Math.Abs(this._flex.CDecimal(dataRow["AM_RCP_JAN_EX"]) - this._flex.CDecimal(dataRow["AM_RCP_EX"])) < Math.Abs(num4))
                                {
                                    decimalValue2 += Math.Abs(this._flex.CDecimal(dataRow["AM_RCP_JAN_EX"]) - this._flex.CDecimal(dataRow["AM_RCP_EX"])) * new Decimal(-1);
                                    num4 = decimalValue1 - decimalValue2;
                                    dataRow["AM_RCP_EX"] = (this._flex.CDecimal(dataRow["AM_RCP_EX"]) + Math.Abs(this._flex.CDecimal(dataRow["AM_RCP_JAN_EX"]) - this._flex.CDecimal(dataRow["AM_RCP_EX"])) * new Decimal(-1));
                                    dataRow["S"] = "Y";
                                }
                                else if (Math.Abs(this._flex.CDecimal(dataRow["AM_RCP_JAN_EX"]) - this._flex.CDecimal(dataRow["AM_RCP_EX"])) >= Math.Abs(num4))
                                {
                                    dataRow["AM_RCP_EX"] = (this._flex.CDecimal(dataRow["AM_RCP_EX"]) + num4);
                                    dataRow["S"] = "Y";
                                    decimalValue2 += num4;
                                    num4 = decimalValue1 - decimalValue2;
                                }

                                dataRow["AM_RCP"] = this.반제원화(this._flex.CDecimal(dataRow["AM_RCP_EX"]), this._flex.CDecimal(dataRow["RT_EXCH"]));
                                dataRow["AM_PL"] = this.환차손익(this._flex.CDecimal(dataRow["AM_RCP_EX"]), this.cur환율.DecimalValue, this._flex.CDecimal(dataRow["RT_EXCH"]));

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
                        if (!(num4 <= 0))
                        {
                            if (Math.Abs(D.GetDecimal(dataRow["AM_RCP_JAN_EX"]) - D.GetDecimal(dataRow["AM_RCP_EX"])) < 0)
                            {
                                decimalValue2 += D.GetDecimal(dataRow["AM_RCP_JAN_EX"]);
                                num4 = decimalValue1 - decimalValue2;
                                dataRow["AM_RCP_EX"] = D.GetDecimal(dataRow["AM_RCP_JAN_EX"]);
                                dataRow["S"] = "Y";
                            }
                            else if (D.GetDecimal(dataRow["AM_RCP_JAN_EX"]) - D.GetDecimal(dataRow["AM_RCP_EX"]) < num4)
                            {
                                decimalValue2 += D.GetDecimal(dataRow["AM_RCP_JAN_EX"]);
                                num4 = decimalValue1 - decimalValue2;
                                dataRow["AM_RCP_EX"] = D.GetDecimal(dataRow["AM_RCP_JAN_EX"]);
                                dataRow["S"] = "Y";
                            }
                            else if (D.GetDecimal(dataRow["AM_RCP_JAN_EX"]) - D.GetDecimal(dataRow["AM_RCP_EX"]) >= num4)
                            {
                                dataRow["AM_RCP_EX"] = (D.GetDecimal(dataRow["AM_RCP_EX"]) + num4);
                                dataRow["S"] = "Y";
                                decimalValue2 += num4;
                                num4 = decimalValue1 - decimalValue2;
                            }

                            dataRow["AM_RCP"] = this.반제원화(this._flex.CDecimal(dataRow["AM_RCP_EX"]), this._flex.CDecimal(dataRow["RT_EXCH"]));
                            dataRow["AM_PL"] = this.환차손익(this._flex.CDecimal(dataRow["AM_RCP_EX"]), this.cur환율.DecimalValue, this._flex.CDecimal(dataRow["RT_EXCH"]));

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
                    if (decimalValue2 < 0)
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
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn조회_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!this.chk_기간.Checked && (Global.MainFrame.ShowMessage("기간조회조건을 제외할경우 시간이 오래걸릴수있습니다. 계속하시겠습니까?", "IK2") == DialogResult.Cancel))
                //    return;

                this._flex.Binding = DBHelper.GetDataTable("SP_CZ_PU_ADPAYMENT_BILL_REG_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                           this.dtp매입일자.StartDateToString,
                                                                                                           this.dtp매입일자.EndDateToString,
                                                                                                           this.ctx매입처.CodeValue,
                                                                                                           this.cbo거래구분.SelectedValue,
                                                                                                           this.cbo통화명.SelectedValue,
                                                                                                           this.ctx매입부서.CodeValue,
                                                                                                           this.ctx매입담당자.CodeValue });
                
                if (!this._flex.HasNormalRow)
                {
                    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }

                int count = this.선지급반제.Rows.Count;

                for (int index = 0; index < count; ++index)
                {
                    foreach (DataRow dataRow in this._flex.DataTable.Select("NO_DOCU = '" + D.GetString(this.선지급반제.Rows[index]["NO_DOCU"]) + "' AND NO_DOLINE = '" + D.GetString(this.선지급반제.Rows[index]["NO_DOLINE"]) + "'"))
                    {
                        dataRow["AM_BAN_EX"] = (D.GetDecimal(dataRow["AM_BAN_EX"]) + D.GetDecimal(this.선지급반제.Rows[index]["AM_BAN_EX"]));
                        dataRow["AM_BAN"] = (D.GetDecimal(dataRow["AM_BAN"]) + D.GetDecimal(this.선지급반제.Rows[index]["AM_BAN"]));
                        dataRow["AM_RCP_JAN_EX"] = (D.GetDecimal(dataRow["AM_RCP_JAN_EX"]) - D.GetDecimal(this.선지급반제.Rows[index]["AM_BAN_EX"]));
                        dataRow["AM_RCP_JAN"] = (D.GetDecimal(dataRow["AM_RCP_JAN"]) - D.GetDecimal(this.선지급반제.Rows[index]["AM_BAN"]));
                    }
                }

                this.잔액계산();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow) return;

                if (this.반제액체크())
                {
                    Global.MainFrame.ShowMessage("반제액이 총지급액을 초과할 수 없습니다.");
                }
                else
                {
                    if (this.반제액체크2() && Global.MainFrame.ShowMessage("외화반제 적용할 반제액이 총지급액과 일치하지않습니다. 그래도 적용하시겠습니까?", "QY2") == DialogResult.No)
                        return;

                    for (int @fixed = this._flex.Rows.Fixed; @fixed < this._flex.Rows.Count; ++@fixed)
                    {
                        if (Math.Abs(Convert.ToDecimal(this._flex[@fixed, "AM_RCP_EX"])) > 0)
                        {
                            if (this._flex.GetCellCheck(@fixed, this._flex.Cols["S"].Index) == CheckEnum.Unchecked)
                            {
                                this._flex[@fixed, "S"] ="Y";
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

                        this.resultDt = dataTable;
                        this.DialogResult = DialogResult.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flex_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                string str = this._flex.GetData(e.Row, e.Col).ToString();
                string editData = this._flex.EditData;

                if (str.ToUpper() == editData.ToUpper())
                    return;
                
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

                    this._flex["AM_PL"] =  (0 - Convert.ToDecimal(this._flex["AM_PL_LOSS"]));
                    this._flex["AM_PL_PROFIT"] = 0;
                }

                if (name == "AM_PL_PROFIT")
                {
                    if (Convert.ToDecimal(this._flex["AM_PL_PROFIT"]) < 0)
                    {
                        Global.MainFrame.ShowMessage("입력형식은 양수(+)입니다.");
                        if (this._flex.Editor != null)
                            this._flex.Editor.Text = str;
                        this._flex["AM_PL_PROFIT"] = str;
                        return;
                    }

                    this._flex["AM_PL"] = (0 - Convert.ToDecimal(this._flex["AM_PL_PROFIT"]));
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
                    Global.MainFrame.ShowMessage("CZ_반제대상액이 잔액을 초과할 수 없습니다.");
                    if (this._flex.Editor != null)
                        this._flex.Editor.Text = str;
                    this._flex["AM_RCP_EX"] = str;
                }
                else
                {
                    if (name == "AM_RCP_EX")
                    {
                        this._flex["AM_RCP_EX"] = Convert.ToDecimal(editData);

                        this._flex["AM_RCP"] = this.반제원화(this._flex.CDecimal(editData), this._flex.CDecimal(this._flex["RT_EXCH"]));
                        this._flex["AM_PL"] = this.환차손익(this._flex.CDecimal(editData), this.cur환율.DecimalValue, this._flex.CDecimal(this._flex["RT_EXCH"]));

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
                            this._flex["AM_PL"] = this.환차손익2(this._flex.CDecimal(this._flex["AM_RCP_EX"]), this.cur환율.DecimalValue, this._flex.CDecimal(this._flex["AM_RCP"]));

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

                    if (D.GetDecimal(this._flex["AM_RCP_EX"]) == 0)
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

        private bool IsDocuApply(string noIv)
        {
            DataTable dataTable = DBHelper.GetDataTable(@"SELECT TOP 1 ST_DOCU 
                                                          FROM FI_DOCU WITH(NOLOCK)
                                                          WHERE CD_COMPANY = '" + MA.Login.회사코드 + "'" + Environment.NewLine +
                                                         "AND NO_MDOCU = '" + noIv + "'");
            return dataTable.Rows.Count != 0 && !(D.GetString(dataTable.Rows[0]["ST_DOCU"]) == "1");
        }

        private bool 반제액체크()
        {
            return Math.Abs(this.cur반제액.DecimalValue) > Math.Abs(this.cur총지급.DecimalValue);
        }

        private bool 반제액체크2()
        {
            return Math.Abs(this.cur반제액.DecimalValue) != Math.Abs(this.cur총지급.DecimalValue);
        }

        private decimal 반제액구하기()
        {
            return D.GetDecimal(this._flex.DataTable.Compute("SUM(AM_RCP_EX)", string.Empty));
        }

        private decimal 반제액원화구하기()
        {
            return D.GetDecimal(this._flex.DataTable.Compute("SUM(AM_RCP)", string.Empty));
        }

        private void 잔액계산()
        {
            this.cur반제액.DecimalValue = this.반제액구하기();
            this.cur잔액.DecimalValue = this.cur총지급.DecimalValue - this.cur반제액.DecimalValue;
            this.cur반제액원화.DecimalValue = this.반제액원화구하기();
            this.cur잔액원화.DecimalValue = this.cur총지급원화.DecimalValue - this.cur반제액원화.DecimalValue;
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

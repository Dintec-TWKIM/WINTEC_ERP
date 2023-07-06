using System;
using System.Data;
using System.Text;
using DevExpress.Utils;
using DevExpress.XtraPivotGrid;
using Dintec;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.Pivot;
using DevExpress.Data.PivotGrid;
using System.Collections.Generic;
using System.Drawing;

namespace cz
{
    public partial class P_CZ_SA_MONTHLYRPT_PIVOT : PageBase
    {
        private P_CZ_SA_MONTHLYRPT_PIVOT_BIZ _biz = new P_CZ_SA_MONTHLYRPT_PIVOT_BIZ();
        private DataTable _목표데이터;
        
        public P_CZ_SA_MONTHLYRPT_PIVOT()
        {
            StartUp.Certify(this);
            InitializeComponent();
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitPivot();
            this.InitEvent();
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.dtp귀속년월.Mask = this.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH, FormatFgType.INSERT);
            this.dtp귀속년월.Text = this.MainFrameInterface.GetStringFirstDayInMonth;
            this.dtp귀속년월.ToDayDate = this.MainFrameInterface.GetDateTimeToday();

            this.cbo목표기준.DataSource = MA.GetCodeUser(new string[] { "0", "1", "2", "3" },
                                                         new string[] { Global.MainFrame.DD("C/C"),
                                                                        Global.MainFrame.DD("매출처그룹"),
                                                                        Global.MainFrame.DD("영업그룹"),
                                                                        Global.MainFrame.DD("매출처") });
            this.cbo목표기준.ValueMember = "CODE";
            this.cbo목표기준.DisplayMember = "NAME";

            this.cbo견적유형.DataSource = MA.GetCodeUser(new string[] { "000", "001", "002" },
                                                         new string[] { "전체",
                                                                        "재고매칭",
                                                                        "견적제출" });
            this.cbo견적유형.ValueMember = "CODE";
            this.cbo견적유형.DisplayMember = "NAME";

            this.chk무상공급금액제외.Checked = true;
            this.chk비용포함.Checked = true;
        }

        private void InitEvent()
        {
            this._pivot.PivotGridControl.CustomSummary += new PivotGridCustomSummaryEventHandler(this.PivotGridControl_CustomSummary);
            this._pivot.PivotGridControl.CustomDrawCell += new PivotCustomDrawCellEventHandler(this.PivotGridControl_CustomDrawCell);
        }

        private void InitPivot()
        {
            this._pivot.SetStart();

            this._pivot.AddPivotField("NO_SO", "수주번호", 80, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("TP_SO", "수주유형", 120, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("NM_EMP", "수주담당자", 120, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("NM_EMP_QTN", "견적담당자", 120, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("NM_EMP_STK", "재고담당자", 120, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("NM_SALEGRP", "영업그룹", 100, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("NM_PARTNER", "매출처", 120, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("NM_PARTNER_GRP", "매출처그룹", 100, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("NM_SUPPLIER", "매입처", 100, true, PivotArea.FilterArea);

            this._pivot.AddPivotField("NM_SALEORG", "영업조직", 120, true, PivotArea.RowArea);
            this._pivot.AddPivotField("NM_CC", "C/C", 120, true, PivotArea.RowArea);

            this._pivot.AddPivotField("AM_SO", "수주금액", 110, true, PivotArea.DataArea);
            this._pivot.AddPivotField("AM_PO", "발주금액", 110, true, PivotArea.DataArea);
            this._pivot.AddPivotField("PROFIT", "이윤", 110, true, PivotSummaryType.Custom, PivotArea.DataArea);
            this._pivot.AddPivotField("RT_PROFIT", "이윤율", 60, true, PivotSummaryType.Custom, PivotArea.DataArea);
            this._pivot.AddPivotField("AM_MONTHWON", "월목표", 110, true, PivotSummaryType.Custom, PivotArea.DataArea);
            this._pivot.AddPivotField("RT_MONTH", "월달성율", 60, true, PivotSummaryType.Custom, PivotArea.DataArea);

            this._pivot.AddPivotField("AM_HALF_YEAR1", "상반기목표", 110, true, PivotSummaryType.Custom, PivotArea.DataArea);
            this._pivot.AddPivotField("AM_SO_HALF_YAER1", "상반기누계(수주)", 110, true, PivotArea.DataArea);
            this._pivot.AddPivotField("AM_PO_HALF_YEAR1", "상반기누계(발주)", 110, true, PivotArea.DataArea);
            this._pivot.AddPivotField("RT_HALF_YEAR1", "상반기달성율", 100, true, PivotSummaryType.Custom, PivotArea.DataArea);

            this._pivot.AddPivotField("AM_HALF_YEAR2", "하반기목표", 110, true, PivotSummaryType.Custom, PivotArea.DataArea);
            this._pivot.AddPivotField("AM_SO_HALF_YAER2", "하반기누계(수주)", 110, true, PivotArea.DataArea);
            this._pivot.AddPivotField("AM_PO_HALF_YEAR2", "하반기누계(발주)", 110, true, PivotArea.DataArea);
            this._pivot.AddPivotField("RT_HALF_YEAR2", "하반기달성율", 100, true, PivotSummaryType.Custom, PivotArea.DataArea);

            this._pivot.AddPivotField("AM_YEARWON", "년목표", 110, true, PivotSummaryType.Custom, PivotArea.DataArea);
            this._pivot.AddPivotField("AM_SO_YEAR", "년누계(수주)", 110, true, PivotArea.DataArea);
            this._pivot.AddPivotField("AM_PO_YEAR", "년누계(발주)", 110, true, PivotArea.DataArea);
            this._pivot.AddPivotField("RT_YEAR", "년달성율", 60, true, PivotSummaryType.Custom, PivotArea.DataArea);

            this._pivot.PivotGridControl.Fields["AM_SO"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_SO"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["AM_PO"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_PO"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["PROFIT"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["PROFIT"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["RT_PROFIT"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["RT_PROFIT"].CellFormat.FormatString = "#,###,###,###,##0.00%";
            this._pivot.PivotGridControl.Fields["AM_MONTHWON"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_MONTHWON"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["RT_MONTH"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["RT_MONTH"].CellFormat.FormatString = "#,###,###,###,##0.00%";

            this._pivot.PivotGridControl.Fields["AM_HALF_YEAR1"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_HALF_YEAR1"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["AM_SO_HALF_YAER1"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_SO_HALF_YAER1"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["AM_PO_HALF_YEAR1"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_PO_HALF_YEAR1"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["RT_HALF_YEAR1"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["RT_HALF_YEAR1"].CellFormat.FormatString = "#,###,###,###,##0.00%";

            this._pivot.PivotGridControl.Fields["AM_HALF_YEAR2"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_HALF_YEAR2"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["AM_SO_HALF_YAER2"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_SO_HALF_YAER2"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["AM_PO_HALF_YEAR2"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_PO_HALF_YEAR2"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["RT_HALF_YEAR2"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["RT_HALF_YEAR2"].CellFormat.FormatString = "#,###,###,###,##0.00%";

            this._pivot.PivotGridControl.Fields["AM_YEARWON"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_YEARWON"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["AM_SO_YEAR"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_SO_YEAR"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["AM_PO_YEAR"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_PO_YEAR"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["RT_YEAR"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["RT_YEAR"].CellFormat.FormatString = "#,###,###,###,##0.00%";

            this._pivot.SetEnd();
        }

        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch()) return false;

            if (string.IsNullOrEmpty(this.dtp귀속년월.Text))
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl귀속년월.Text);
                return false;
            }

            return true;
        }


        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!BeforeSearch()) return;

                MsgControl.ShowMsg("조회중입니다. \r\n잠시만 기다려주세요!");

                this._목표데이터 = this._biz.Get목표데이터(this.dtp귀속년월.Text,
                                                           D.GetString(this.cbo목표기준.SelectedValue));

                dt = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                     this.dtp귀속년월.Text,
                                                     D.GetString(this.cbo목표기준.SelectedValue),
                                                     (this.chk무상공급금액제외.Checked == true ? "Y" : "N"),
                                                     (this.chk비용포함.Checked == true ? "Y" : "N"),
                                                     this.cbo견적유형.SelectedValue.ToString() });

                dt.TableName = this.PageID;
                this._pivot.DataSource = dt;

                if (dt == null || dt.Rows.Count == 0)
                {
                    ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                    return;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
            }
        }

        private void PivotGridControl_CustomSummary(object sender, PivotGridCustomSummaryEventArgs e)
        {
            PivotDrillDownDataSource drillDownDataSource;
            Dictionary<string, decimal> dic;
            string fieldName;
            decimal num1, num2, num3;

            try
            {
                drillDownDataSource = e.CreateDrillDownDataSource();
                num1 = 0;
                num2 = 0;
                num3 = 0;

                switch (this.cbo목표기준.SelectedValue.ToString())
                {
                    case "0":
                        fieldName = "NM_CC";
                        break;
                    case "1":
                        fieldName = "NM_PARTNER_GRP";
                        break;
                    case "2":
                        fieldName = "NM_SALEGRP";
                        break;
                    case "3":
                        fieldName = "NM_PARTNER";
                        break;
                    default:
                        return;
                }

                switch (e.DataField.FieldName)
                {
                    case "PROFIT":
                        for (int index = 0; index < drillDownDataSource.RowCount; index++)
                        {
                            num1 += D.GetDecimal(drillDownDataSource[index]["AM_SO"]);
                            num2 += D.GetDecimal(drillDownDataSource[index]["AM_PO"]);
                        }
                        e.CustomValue = (num1 - num2);
                        break;
                    case "RT_PROFIT":
                        for (int index = 0; index < drillDownDataSource.RowCount; index++)
                        {
                            num1 += D.GetDecimal(drillDownDataSource[index]["AM_SO"]);
                            num2 += D.GetDecimal(drillDownDataSource[index]["AM_PO"]);
                        }
                        e.CustomValue = (num1 == 0 ? 0 : ((num1 - num2) / num1));
                        break;
                    case "AM_MONTHWON":
                        dic = new Dictionary<string, decimal>();

                        for (int index = 0; index < drillDownDataSource.RowCount; index++)
                        {
                            if (drillDownDataSource[index][fieldName] != null && !dic.ContainsKey(drillDownDataSource[index][fieldName].ToString()))
                                dic.Add(drillDownDataSource[index][fieldName].ToString(), D.GetDecimal(drillDownDataSource[index]["AM_MONTHWON"]));
                        }

                        foreach (decimal value in dic.Values)
                        {
                            e.CustomValue = D.GetDecimal(e.CustomValue) + value;
                        }
                        break;
                    case "RT_MONTH":
                        dic = new Dictionary<string, decimal>();

                        for (int index = 0; index < drillDownDataSource.RowCount; index++)
                        {
                            if (drillDownDataSource[index][fieldName] != null && !dic.ContainsKey(drillDownDataSource[index][fieldName].ToString()))
                                dic.Add(drillDownDataSource[index][fieldName].ToString(), D.GetDecimal(drillDownDataSource[index]["AM_MONTHWON"]));
                            
                            num3 += D.GetDecimal(drillDownDataSource[index]["AM_SO"]);
                        }

                        foreach (decimal value in dic.Values)
                        {
                            num1 += value;
                        }
                        
                        e.CustomValue = (num1 == 0 ? 0 : (num3 / num1));
                        break;
                    case "AM_HALF_YEAR1":
                        dic = new Dictionary<string, decimal>();

                        for (int index = 0; index < drillDownDataSource.RowCount; index++)
                        {
                            if (drillDownDataSource[index][fieldName] != null && !dic.ContainsKey(drillDownDataSource[index][fieldName].ToString()))
                                dic.Add(drillDownDataSource[index][fieldName].ToString(), D.GetDecimal(drillDownDataSource[index]["AM_HALF_YEAR1"]));
                        }

                        foreach (decimal value in dic.Values)
                        {
                            e.CustomValue = D.GetDecimal(e.CustomValue) + value;
                        }
                        break;
                    case "RT_HALF_YEAR1":
                        dic = new Dictionary<string, decimal>();

                        for (int index = 0; index < drillDownDataSource.RowCount; index++)
                        {
                            if (drillDownDataSource[index][fieldName] != null && !dic.ContainsKey(drillDownDataSource[index][fieldName].ToString()))
                                dic.Add(drillDownDataSource[index][fieldName].ToString(), D.GetDecimal(drillDownDataSource[index]["AM_HALF_YEAR1"]));

                            num3 += D.GetDecimal(drillDownDataSource[index]["AM_SO_HALF_YAER1"]);
                        }

                        foreach (decimal value in dic.Values)
                        {
                            num1 += value;
                        }

                        e.CustomValue = (num1 == 0 ? 0 : (num3 / num1));
                        break;
                    case "AM_HALF_YEAR2":
                        dic = new Dictionary<string, decimal>();

                        for (int index = 0; index < drillDownDataSource.RowCount; index++)
                        {
                            if (drillDownDataSource[index][fieldName] != null && !dic.ContainsKey(drillDownDataSource[index][fieldName].ToString()))
                                dic.Add(drillDownDataSource[index][fieldName].ToString(), D.GetDecimal(drillDownDataSource[index]["AM_HALF_YEAR2"]));
                        }

                        foreach (decimal value in dic.Values)
                        {
                            e.CustomValue = D.GetDecimal(e.CustomValue) + value;
                        }
                        break;
                    case "RT_HALF_YEAR2":
                        dic = new Dictionary<string, decimal>();

                        for (int index = 0; index < drillDownDataSource.RowCount; index++)
                        {
                            if (drillDownDataSource[index][fieldName] != null && !dic.ContainsKey(drillDownDataSource[index][fieldName].ToString()))
                                dic.Add(drillDownDataSource[index][fieldName].ToString(), D.GetDecimal(drillDownDataSource[index]["AM_HALF_YEAR2"]));

                            num3 += D.GetDecimal(drillDownDataSource[index]["AM_SO_HALF_YAER2"]);
                        }

                        foreach (decimal value in dic.Values)
                        {
                            num1 += value;
                        }

                        e.CustomValue = (num1 == 0 ? 0 : (num3 / num1));
                        break;
                    case "AM_YEARWON":
                        dic = new Dictionary<string, decimal>();

                        for (int index = 0; index < drillDownDataSource.RowCount; index++)
                        {
                            if (drillDownDataSource[index][fieldName] != null && !dic.ContainsKey(drillDownDataSource[index][fieldName].ToString()))
                                dic.Add(drillDownDataSource[index][fieldName].ToString(), D.GetDecimal(drillDownDataSource[index]["AM_YEARWON"]));
                        }

                        foreach (decimal value in dic.Values)
                        {
                            e.CustomValue = D.GetDecimal(e.CustomValue) + value;
                        }
                        break;
                    case "RT_YEAR":
                        dic = new Dictionary<string, decimal>();

                        for (int index = 0; index < drillDownDataSource.RowCount; index++)
                        {
                            if (drillDownDataSource[index][fieldName] != null && !dic.ContainsKey(drillDownDataSource[index][fieldName].ToString()))
                                dic.Add(drillDownDataSource[index][fieldName].ToString(), D.GetDecimal(drillDownDataSource[index]["AM_YEARWON"]));
                            
                            num3 += D.GetDecimal(drillDownDataSource[index]["AM_SO_YEAR"]);
                        }

                        foreach (decimal value in dic.Values)
                        {
                            num1 += value;
                        }
                        
                        e.CustomValue = (num1 == 0 ? 0 : (num3 / num1));
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void PivotGridControl_CustomDrawCell(object sender, PivotCustomDrawCellEventArgs e)
        {
            try
            {
                if (e.RowValueType == PivotGridValueType.Total)
                {
                    e.Appearance.BackColor2 = Color.Yellow;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private decimal 목표금액(bool is월별)
        {
            string 목표컬럼;

            try
            {
                if (is월별)
                    목표컬럼 = "AM_MONTHWON";
                else
                    목표컬럼 = "AM_TOTWON";

                return D.GetDecimal(this._목표데이터.Compute("SUM(" + 목표컬럼 + ")", string.Empty));
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }

            return 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Dintec;
using DevExpress.XtraPivotGrid;
using DevExpress.Utils;
using DevExpress.Data.PivotGrid;
using Duzon.ERPU;
using Duzon.Common.BpControls;
using Duzon.BizOn.Windows.PivotGrid;

namespace cz
{
    public partial class P_CZ_SA_TR_PERFORM_PIVOT : PageBase
    {
        P_CZ_SA_TR_PERFORM_PIVOT_BIZ _biz = new P_CZ_SA_TR_PERFORM_PIVOT_BIZ();

        private string 마감여부
        {
            get
            {
                if (this.rdo마감전체.Checked == true)
                    return "A";
                else if (this.rdo마감.Checked == true)
                    return "Y";
                else
                    return "N";
            }
        }

        public P_CZ_SA_TR_PERFORM_PIVOT()
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

        private void InitPivot()
        {
            this.Pivot초기화(this._pivot수주율건수);
            this.Pivot초기화(this._pivot수주율종수);
            this.Pivot초기화(this._pivot수주율수량);
            this.Pivot초기화(this._pivot수주율금액);
            this.Pivot초기화(this._pivot이윤율);
        }

        private void InitEvent()
        {
            this._pivot수주율건수.PivotGridControl.CustomSummary += new PivotGridCustomSummaryEventHandler(this.PivotGridControl_CustomSummary);
            this._pivot수주율종수.PivotGridControl.CustomSummary += new PivotGridCustomSummaryEventHandler(this.PivotGridControl_CustomSummary);
            this._pivot수주율수량.PivotGridControl.CustomSummary += new PivotGridCustomSummaryEventHandler(this.PivotGridControl_CustomSummary);
            this._pivot수주율금액.PivotGridControl.CustomSummary += new PivotGridCustomSummaryEventHandler(this.PivotGridControl_CustomSummary);
            this._pivot이윤율.PivotGridControl.CustomSummary += new PivotGridCustomSummaryEventHandler(this.PivotGridControl_CustomSummary);

            this.bpc거래처그룹.QueryBefore += new BpQueryHandler(Control_QueryBefore);
            this.bpc거래처그룹2.QueryBefore += new BpQueryHandler(Control_QueryBefore);
        }

        protected override void InitPaint()
        {
            try
            {
                base.InitPaint();

                this.dtp기준일자.StartDateToString = Global.MainFrame.GetDateTimeToday().Year + "0101";
                this.dtp기준일자.EndDateToString = Global.MainFrame.GetStringToday;

                this.cbo기준일자.DataSource = MA.GetCodeUser(new string[] { "001", "002", "003", "004" }, new string[] { "기간별", "견적일자", "수주일자", "수통일자" });
                this.cbo기준일자.DisplayMember = "NAME";
                this.cbo기준일자.ValueMember = "CODE";

                this.cbo데이터기준.DataSource = MA.GetCodeUser(new string[] { "001", "002" }, new string[] { "견적기준", "수주기준" });
                this.cbo데이터기준.DisplayMember = "NAME";
                this.cbo데이터기준.ValueMember = "CODE";

                this.cbo제품군.DataSource = Global.MainFrame.GetComboDataCombine("S;MA_B000066");
                this.cbo제품군.ValueMember = "CODE";
                this.cbo제품군.DisplayMember = "NAME";

                this.SetToolBarButtonState(true, false, false, false, true);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void Control_QueryBefore(object sender, BpQueryArgs e)
        {
            if (e.ControlName == this.bpc거래처그룹.Name)
                e.HelpParam.P41_CD_FIELD1 = "MA_B000065";
            else if (e.ControlName == this.bpc거래처그룹2.Name)
                e.HelpParam.P41_CD_FIELD1 = "MA_B000067";
        }

        private void PivotGridControl_CustomSummary(object sender, PivotGridCustomSummaryEventArgs e)
        {
            PivotDrillDownDataSource drillDownDataSource;
            decimal num1, num2;

            try
            {
                drillDownDataSource = e.CreateDrillDownDataSource();
                num1 = 0;
                num2 = 0;

                for (int index = 0; index < drillDownDataSource.RowCount; ++index)
                {
                    switch(e.DataField.FieldName)
                    {
                        case "RT_SO_FILE":
                            num1 += D.GetDecimal(drillDownDataSource[index]["QT_FILE_SO"]);
                            num2 += D.GetDecimal(drillDownDataSource[index]["QT_FILE_QTN"]);
                            break;
                        case "RT_SO_ITEM":
                            num1 += D.GetDecimal(drillDownDataSource[index]["QT_ITEM_SO"]);
                            num2 += D.GetDecimal(drillDownDataSource[index]["QT_ITEM_QTN"]);
                            break;
                        case "RT_SO":
                            num1 += D.GetDecimal(drillDownDataSource[index]["QT_SO"]);
                            num2 += D.GetDecimal(drillDownDataSource[index]["QT_QTN"]);
                            break;
                        case "RT_SO_AM":
                            num1 += D.GetDecimal(drillDownDataSource[index]["AM_KR_SO"]);
                            num2 += D.GetDecimal(drillDownDataSource[index]["AM_KR_QTN"]);
                            break;
                        case "RT_PROFIT":
                            num1 += D.GetDecimal(drillDownDataSource[index]["AM_SO"]);
                            num2 += (D.GetDecimal(drillDownDataSource[index]["AM_PO"]) + D.GetDecimal(drillDownDataSource[index]["AM_STOCK"]));
                            break;
                    }
                }

                if (e.DataField.FieldName == "RT_PROFIT")
                    e.CustomValue = (num1 == 0 ? 0 : (num1 - num2) / num1);
                else
                    e.CustomValue = (num2 == 0 ? 0 : (num1 / num2));
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch()) return false;

            if (string.IsNullOrEmpty(this.bpc매출처.QueryWhereIn_Pipe))
            {
                if (this.ShowMessage("매출처를 지정하지 않고 조회하면 시간이 매우 오래걸릴 수 있습니다.\n그래도 조회하시겠습니까?", "QY2") != DialogResult.Yes)
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

                object[] parameters = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                     this.tcl메인.SelectedIndex.ToString(),
                                                     this.cbo기준일자.SelectedValue,
                                                     this.dtp기준일자.StartDateToString,
                                                     this.dtp기준일자.EndDateToString,
                                                     this.bpc거래처그룹.QueryWhereIn_Pipe,
                                                     this.bpc거래처그룹2.QueryWhereIn_Pipe,
                                                     this.bpc매출처.QueryWhereIn_Pipe,
                                                     this.ctx품목군.CodeValue,
                                                     this.cbo제품군.SelectedValue,
                                                     this.bpc영업그룹.QueryWhereIn_Pipe,
                                                     this.ctx담당자.CodeValue,
                                                     (this.chk비용여부.Checked == true ? "Y" : "N"),
                                                     (this.chk클레임여부.Checked == true ? "Y" : "N"),
                                                     this.마감여부 };

                if (this.cbo데이터기준.SelectedValue.ToString() == "001")
                    dt = this._biz.Search(parameters);
                else if (this.cbo데이터기준.SelectedValue.ToString() == "002")
                    dt = this._biz.Search1(parameters);
                else
                    return;

                dt.TableName = this.PageID;

                switch (this.tcl메인.SelectedIndex)
                {
                    case 0:
                        this._pivot수주율건수.DataSource = dt;
                        break;
                    case 1:
                        this._pivot수주율종수.DataSource = dt;
                        break;
                    case 2:
                        this._pivot수주율수량.DataSource = dt;
                        break;
                    case 3:
                        this._pivot수주율금액.DataSource = dt;
                        break;
                    case 4:
                        this._pivot이윤율.DataSource = dt;
                        break;
                }

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

        private void Pivot초기화(PivotGrid pivot)
        {
            try
            {
                pivot.SetStart();

                pivot.AddPivotField("NM_KOR", "담당자", 100, true, PivotArea.FilterArea);
                pivot.AddPivotField("LN_PARTNER", "매출처", 100, true, PivotArea.FilterArea);
                pivot.AddPivotField("LN_PARTNER_SP", "싱가폴매출처", 100, true, PivotArea.FilterArea);
                pivot.AddPivotField("NM_VESSEL", "호선", 100, true, PivotArea.FilterArea);
                pivot.AddPivotField("NM_PARTNER_GRP", "매출처그룹명", 100, true, PivotArea.FilterArea);
                pivot.AddPivotField("NM_SALEGRP", "영업그룹명", 100, true, PivotArea.FilterArea);
                pivot.AddPivotField("NM_SALEORG", "영업조직명", 100, true, PivotArea.FilterArea);
                pivot.AddPivotField("NM_GRP_MFG", "제품군", 100, true, PivotArea.FilterArea);
                pivot.AddPivotField("NM_CLS_L", "대분류", 100, true, PivotArea.FilterArea);
                pivot.AddPivotField("NM_CLS_M", "중분류", 100, true, PivotArea.FilterArea);
                pivot.AddPivotField("NM_CLS_S", "소분류", 100, true, PivotArea.FilterArea);
                pivot.AddPivotField("NM_EXCH_Q", "통화명(견적)", 100, true, PivotArea.FilterArea);
                pivot.AddPivotField("NM_EXCH_S", "통화명(수주)", 100, true, PivotArea.FilterArea);
                pivot.AddPivotField("NM_FG_PARTNER", "거래처구분", 100, true, PivotArea.FilterArea);

                pivot.AddPivotField("NM_ITEMGRP", "품목군", 100, true, PivotArea.RowArea);

                pivot.AddPivotField("YYYY", "년도", 100, true, PivotArea.ColumnArea);

                if (pivot.Name == this._pivot수주율건수.Name)
                {
                    pivot.AddPivotField("QT_FILE_QTN", "견적건수", 120, true, PivotArea.DataArea);
                    pivot.AddPivotField("QT_FILE_SO", "수주건수", 120, true, PivotArea.DataArea);
                    pivot.AddPivotField("RT_SO_FILE", "수주율(건수)", 120, true, PivotSummaryType.Custom, PivotArea.DataArea);

                    pivot.PivotGridControl.Fields["QT_FILE_QTN"].CellFormat.FormatType = FormatType.Numeric;
                    pivot.PivotGridControl.Fields["QT_FILE_QTN"].CellFormat.FormatString = "0";
                    pivot.PivotGridControl.Fields["QT_FILE_SO"].CellFormat.FormatType = FormatType.Numeric;
                    pivot.PivotGridControl.Fields["QT_FILE_SO"].CellFormat.FormatString = "0";
                    pivot.PivotGridControl.Fields["RT_SO_FILE"].CellFormat.FormatType = FormatType.Numeric;
                    pivot.PivotGridControl.Fields["RT_SO_FILE"].CellFormat.FormatString = "#,###,###,###,##0.00%";
                }
                else if (pivot.Name == this._pivot수주율종수.Name)
                {
                    pivot.AddPivotField("QT_ITEM_QTN", "견적종수", 120, true, PivotArea.DataArea);
                    pivot.AddPivotField("QT_ITEM_SO", "수주종수", 120, true, PivotArea.DataArea);
                    pivot.AddPivotField("RT_SO_ITEM", "수주율(종수)", 120, true, PivotSummaryType.Custom, PivotArea.DataArea);

                    pivot.PivotGridControl.Fields["QT_ITEM_QTN"].CellFormat.FormatType = FormatType.Numeric;
                    pivot.PivotGridControl.Fields["QT_ITEM_QTN"].CellFormat.FormatString = "0";
                    pivot.PivotGridControl.Fields["QT_ITEM_SO"].CellFormat.FormatType = FormatType.Numeric;
                    pivot.PivotGridControl.Fields["QT_ITEM_SO"].CellFormat.FormatString = "0";
                    pivot.PivotGridControl.Fields["RT_SO_ITEM"].CellFormat.FormatType = FormatType.Numeric;
                    pivot.PivotGridControl.Fields["RT_SO_ITEM"].CellFormat.FormatString = "#,###,###,###,##0.00%";
                }
                else if (pivot.Name == this._pivot수주율수량.Name)
                {
                    pivot.AddPivotField("QT_QTN", "견적수량", 120, true, PivotArea.DataArea);
                    pivot.AddPivotField("QT_SO", "수주수량", 120, true, PivotArea.DataArea);
                    pivot.AddPivotField("RT_SO", "수주율(수량)", 120, true, PivotSummaryType.Custom, PivotArea.DataArea);

                    pivot.PivotGridControl.Fields["QT_QTN"].CellFormat.FormatType = FormatType.Numeric;
                    pivot.PivotGridControl.Fields["QT_QTN"].CellFormat.FormatString = "0";
                    pivot.PivotGridControl.Fields["QT_SO"].CellFormat.FormatType = FormatType.Numeric;
                    pivot.PivotGridControl.Fields["QT_SO"].CellFormat.FormatString = "0";
                    pivot.PivotGridControl.Fields["RT_SO"].CellFormat.FormatType = FormatType.Numeric;
                    pivot.PivotGridControl.Fields["RT_SO"].CellFormat.FormatString = "#,###,###,###,##0.00%";
                }
                else if (pivot.Name == this._pivot수주율금액.Name)
                {
                    pivot.AddPivotField("AM_EX_QTN", "외화금액(견적)", 120, true, PivotArea.DataArea);
                    pivot.AddPivotField("AM_EX_SO", "외화금액(수주)", 120, true, PivotArea.DataArea);
                    pivot.AddPivotField("AM_KR_QTN", "원화금액(견적)", 120, true, PivotArea.DataArea);
                    pivot.AddPivotField("AM_KR_SO", "원화금액(수주)", 120, true, PivotArea.DataArea);
                    pivot.AddPivotField("RT_SO_AM", "수주율(원화)", 120, true, PivotSummaryType.Custom, PivotArea.DataArea);

                    pivot.PivotGridControl.Fields["AM_EX_QTN"].CellFormat.FormatType = FormatType.Numeric;
                    pivot.PivotGridControl.Fields["AM_EX_QTN"].CellFormat.FormatString = "#,###,###,###,##0.##";
                    pivot.PivotGridControl.Fields["AM_EX_SO"].CellFormat.FormatType = FormatType.Numeric;
                    pivot.PivotGridControl.Fields["AM_EX_SO"].CellFormat.FormatString = "#,###,###,###,##0.##";
                    pivot.PivotGridControl.Fields["AM_KR_QTN"].CellFormat.FormatType = FormatType.Numeric;
                    pivot.PivotGridControl.Fields["AM_KR_QTN"].CellFormat.FormatString = "#,###,###,###,##0.##";
                    pivot.PivotGridControl.Fields["AM_KR_SO"].CellFormat.FormatType = FormatType.Numeric;
                    pivot.PivotGridControl.Fields["AM_KR_SO"].CellFormat.FormatString = "#,###,###,###,##0.##";
                    pivot.PivotGridControl.Fields["RT_SO_AM"].CellFormat.FormatType = FormatType.Numeric;
                    pivot.PivotGridControl.Fields["RT_SO_AM"].CellFormat.FormatString = "#,###,###,###,##0.00%";
                }
                else if (pivot.Name == this._pivot이윤율.Name)
                {
                    pivot.AddPivotField("AM_SO", "수주금액", 120, true, PivotArea.DataArea);
                    pivot.AddPivotField("AM_STOCK", "재고금액", 120, true, PivotArea.DataArea);
                    pivot.AddPivotField("AM_PO", "발주금액", 120, true, PivotArea.DataArea);
                    pivot.AddPivotField("RT_PROFIT", "이윤율", 120, true, PivotSummaryType.Custom, PivotArea.DataArea);

                    pivot.PivotGridControl.Fields["AM_SO"].CellFormat.FormatType = FormatType.Numeric;
                    pivot.PivotGridControl.Fields["AM_SO"].CellFormat.FormatString = "#,###,###,###,##0.##";
                    pivot.PivotGridControl.Fields["AM_STOCK"].CellFormat.FormatType = FormatType.Numeric;
                    pivot.PivotGridControl.Fields["AM_STOCK"].CellFormat.FormatString = "#,###,###,###,##0.##";
                    pivot.PivotGridControl.Fields["AM_PO"].CellFormat.FormatType = FormatType.Numeric;
                    pivot.PivotGridControl.Fields["AM_PO"].CellFormat.FormatString = "#,###,###,###,##0.##";
                    pivot.PivotGridControl.Fields["RT_PROFIT"].CellFormat.FormatType = FormatType.Numeric;
                    pivot.PivotGridControl.Fields["RT_PROFIT"].CellFormat.FormatString = "#,###,###,###,##0.00%";
                }
                
                pivot.SetEnd();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
    }
}

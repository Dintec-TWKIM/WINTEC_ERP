using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using DevExpress.XtraPivotGrid;
using DevExpress.Utils;
using Duzon.ERPU;
using Dintec;

namespace cz
{
    public partial class P_CZ_MM_NORMAL_ITEM_RPT : PageBase
    {
        P_CZ_MM_NORMAL_ITEM_RPT_BIZ _biz = new P_CZ_MM_NORMAL_ITEM_RPT_BIZ();

        public P_CZ_MM_NORMAL_ITEM_RPT()
        {
			StartUp.Certify(this);
			InitializeComponent();
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitPivot();
            this.InitGrid();
            this.InitEvent();
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.dtp기준일자.Text = Global.MainFrame.GetStringToday;

            if (Global.MainFrame.LoginInfo.CompanyCode == "K100")
            {
                this.bpc회사.AddItem("K100", "(주)딘텍");
                this.bpc회사.AddItem("K200", "(주)두베코");
            }
            else
                this.bpc회사.AddItem(Global.MainFrame.LoginInfo.CompanyCode, Global.MainFrame.LoginInfo.CompanyName);
        }

        private void InitPivot()
        {
            this._pivot.SetStart();

            this._pivot.AddPivotField("NM_COMPANY", "회사명", 80, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("NO_SO", "수주번호", 80, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("NO_LINE", "항번", 80, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("NO_DSP", "순번", 80, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("LN_PARTNER", "매출처", 80, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("NM_VESSEL", "호선명", 80, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("NM_SUPPLIER", "매입처", 80, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("NM_EMP", "담당자", 80, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("CD_ITEM", "재고코드", 80, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("CD_ITEM_PARTNER", "품목코드", 80, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("NM_ITEM_PARTNER", "품목명", 80, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("NM_EXCH", "통화명", 80, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("RT_EXCH", "환율", 80, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("UM_EX_PO", "발주단가(외화)", 80, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("UM_PO", "발주단가(원화)", 80, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("UM_STOCK", "재고단가", 80, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("DT_IN", "입고일자", 80, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("NO_GIR", "협조전번호", 80, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("NO_PACK", "포장번호", 80, true, PivotArea.FilterArea);
            this._pivot.AddPivotField("NM_PACK_LOCATION", "포장로케이션", 80, true, PivotArea.FilterArea);

            this._pivot.AddPivotField("NM_LOCATION", "로케이션", 80, true, PivotArea.RowArea);

            this._pivot.AddPivotField("QT_SO", "수주수량", 80, true, PivotArea.DataArea);
            this._pivot.AddPivotField("QT_IN", "입고수량", 80, true, PivotArea.DataArea);
            this._pivot.AddPivotField("QT_STOCK_PACK", "재고포장수량", 80, true, PivotArea.DataArea);
            this._pivot.AddPivotField("QT_IN_RETURN", "입고반품수량", 80, true, PivotArea.DataArea);
            this._pivot.AddPivotField("QT_OUT", "일반출고수량", 80, true, PivotArea.DataArea);
            this._pivot.AddPivotField("QT_STOCK_OUT", "재고출고수량", 80, true, PivotArea.DataArea);
            this._pivot.AddPivotField("QT_OUT_RETURN", "일반반품수량", 80, true, PivotArea.DataArea);
            this._pivot.AddPivotField("QT_STOCK_RETURN", "재고반품수량", 80, true, PivotArea.DataArea);
            this._pivot.AddPivotField("QT_IO_OUT", "대체출고수량", 80, true, PivotArea.DataArea);
            this._pivot.AddPivotField("QT_IO_IN", "대체입고수량", 80, true, PivotArea.DataArea);
            this._pivot.AddPivotField("QT_INV", "총수량", 80, true, PivotArea.DataArea);
            this._pivot.AddPivotField("AM_INV", "총금액", 80, true, PivotArea.DataArea);

            this._pivot.PivotGridControl.Fields["QT_SO"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["QT_SO"].CellFormat.FormatString = "#0.##";
            this._pivot.PivotGridControl.Fields["QT_IN"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["QT_IN"].CellFormat.FormatString = "#0.##";
            this._pivot.PivotGridControl.Fields["QT_STOCK_PACK"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["QT_STOCK_PACK"].CellFormat.FormatString = "#0.##";
            this._pivot.PivotGridControl.Fields["QT_IN_RETURN"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["QT_IN_RETURN"].CellFormat.FormatString = "#0.##";
            this._pivot.PivotGridControl.Fields["QT_OUT"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["QT_OUT"].CellFormat.FormatString = "#0.##";
            this._pivot.PivotGridControl.Fields["QT_STOCK_OUT"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["QT_STOCK_OUT"].CellFormat.FormatString = "#0.##";
            this._pivot.PivotGridControl.Fields["QT_OUT_RETURN"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["QT_OUT_RETURN"].CellFormat.FormatString = "#0.##";
            this._pivot.PivotGridControl.Fields["QT_STOCK_RETURN"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["QT_STOCK_RETURN"].CellFormat.FormatString = "#0.##";
            this._pivot.PivotGridControl.Fields["QT_IO_OUT"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["QT_IO_OUT"].CellFormat.FormatString = "#0.##";
            this._pivot.PivotGridControl.Fields["QT_IO_IN"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["QT_IO_IN"].CellFormat.FormatString = "#0.##";
            this._pivot.PivotGridControl.Fields["QT_INV"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["QT_INV"].CellFormat.FormatString = "#0.##";

            this._pivot.PivotGridControl.Fields["RT_EXCH"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["RT_EXCH"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["UM_EX_PO"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["UM_EX_PO"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["UM_PO"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["UM_PO"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["UM_STOCK"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["UM_STOCK"].CellFormat.FormatString = "#,###,###,###,##0.##";
            this._pivot.PivotGridControl.Fields["AM_INV"].CellFormat.FormatType = FormatType.Numeric;
            this._pivot.PivotGridControl.Fields["AM_INV"].CellFormat.FormatString = "#,###,###,###,##0.##";

            this._pivot.SetEnd();
        }

        private void InitGrid()
        {
            #region Header
            this._flexH.DetailGrids = new FlexGrid[] { this._flexL, this._flexR };

            this._flexH.BeginSetting(2, 1, false);

            this._flexH.SetCol("NM_COMPANY", "회사명", 80);
            this._flexH.SetCol("NO_SO", "수주번호", 80);
            this._flexH.SetCol("NO_LINE", "항번", 80);
            this._flexH.SetCol("NO_DSP", "순번", 80);
            this._flexH.SetCol("LN_PARTNER", "매출처", 80);
            this._flexH.SetCol("NM_VESSEL", "호선명", 80);
            this._flexH.SetCol("NM_SUPPLIER", "매입처", 80);
            this._flexH.SetCol("NM_EMP", "담당자", 80);
            this._flexH.SetCol("CD_ITEM", "재고코드", 80);
            this._flexH.SetCol("CD_ITEM_PARTNER", "품목코드", 80);
            this._flexH.SetCol("NM_ITEM_PARTNER", "품목명", 80);
            this._flexH.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);

            this._flexH.SetCol("QT_IN", "입고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexH.SetCol("QT_IN_RETURN", "반품수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);

            this._flexH.SetCol("QT_OUT", "출고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexH.SetCol("QT_OUT_RETURN", "반품수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);

            this._flexH.SetCol("QT_STOCK_PACK", "포장수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexH.SetCol("QT_STOCK_OUT", "출고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexH.SetCol("QT_STOCK_RETURN", "반품수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);

            this._flexH.SetCol("QT_IO_OUT", "출고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexH.SetCol("QT_IO_IN", "입고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            
            this._flexH.SetCol("QT_INV", "총수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexH.SetCol("DT_IN", "입고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("NO_GIR", "협조전번호", 80);
            this._flexH.SetCol("NO_PACK", "포장번호", 80);
            this._flexH.SetCol("NM_LOCATION", "로케이션", 80);
            this._flexH.SetCol("NM_PACK_LOCATION", "포장로케이션", 80);
            this._flexH.SetCol("NM_EXCH", "통화명", 60);
            this._flexH.SetCol("RT_EXCH", "환율", 60, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flexH.SetCol("UM_EX_PO", "발주단가(외화)", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexH.SetCol("UM_PO", "발주단가(원화)", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexH.SetCol("UM_STOCK", "재고단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_INV", "총금액", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flexH.SetCol("DC_REASON", "원인", 80);

            this._flexH[0, this._flexH.Cols["QT_IN"].Index] = this.DD("입고");
            this._flexH[0, this._flexH.Cols["QT_IN_RETURN"].Index] = this.DD("입고");

            this._flexH[0, this._flexH.Cols["QT_OUT"].Index] = this.DD("출고");
            this._flexH[0, this._flexH.Cols["QT_OUT_RETURN"].Index] = this.DD("출고");

            this._flexH[0, this._flexH.Cols["QT_STOCK_PACK"].Index] = this.DD("재고");
            this._flexH[0, this._flexH.Cols["QT_STOCK_OUT"].Index] = this.DD("재고");
            this._flexH[0, this._flexH.Cols["QT_STOCK_RETURN"].Index] = this.DD("재고");

            this._flexH[0, this._flexH.Cols["QT_IO_OUT"].Index] = this.DD("계정대체");
            this._flexH[0, this._flexH.Cols["QT_IO_IN"].Index] = this.DD("계정대체");

            this._flexH.SettingVersion = "0.0.0.2";
            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flexH.SetExceptSumCol("RT_EXCH", "UM_EX_PO", "UM_PO", "UM_STOCK");
            #endregion

            #region Left
            this._flexL.BeginSetting(2, 1, false);

            this._flexL.SetCol("NO_PO", "발주번호", 80);
            this._flexL.SetCol("NO_IO", "입고번호", 80);
            this._flexL.SetCol("NO_LINE", "발주항번", 80);
            this._flexL.SetCol("NO_IOLINE", "입고항번", 80);
            this._flexL.SetCol("DT_PO", "발주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexL.SetCol("DT_IO", "입고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexL.SetCol("NM_SUPPLIER", "매입처", 100);
            this._flexL.SetCol("YN_RETURN", "반품유무", 80, false, CheckTypeEnum.Y_N);
            this._flexL.SetCol("NM_QTIOTP", "입고유형", 80);
            this._flexL.SetCol("NM_SL", "입고창고", 80);
            this._flexL.SetCol("CD_LOCATION", "입고로케이션", 80);
            this._flexL.SetCol("QT_PO", "발주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("QT_IO", "입고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("NM_EXCH", "통화명", 60);
            this._flexL.SetCol("RT_EXCH", "환율", 60, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flexL.SetCol("UM_EX_PO", "발주단가(외화)", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexL.SetCol("UM_PO", "발주단가(원화)", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexL.SetCol("UM_STOCK", "재고단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_IO", "금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flexL.SetCol("NO_IO_OUT", "번호", 80);
            this._flexL.SetCol("NO_IOLINE_OUT", "항번", 80);
            this._flexL.SetCol("DT_IO_OUT", "일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexL.SetCol("CD_ITEM_OUT", "품목", 80);
            this._flexL.SetCol("QT_IO_OUT", "수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);

            this._flexL.SetCol("NO_IO_IN", "번호", 80);            
            this._flexL.SetCol("NO_IOLINE_IN", "항번", 80);
            this._flexL.SetCol("DT_IO_IN", "일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexL.SetCol("CD_ITEM_IN", "품목", 80);
            this._flexL.SetCol("QT_IO_IN", "수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);

            this._flexL[0, this._flexL.Cols["NO_IO_OUT"].Index] = this.DD("계정대체출고");
            this._flexL[0, this._flexL.Cols["NO_IOLINE_OUT"].Index] = this.DD("계정대체출고");
            this._flexL[0, this._flexL.Cols["DT_IO_OUT"].Index] = this.DD("계정대체출고");
            this._flexL[0, this._flexL.Cols["CD_ITEM_OUT"].Index] = this.DD("계정대체출고");
            this._flexL[0, this._flexL.Cols["QT_IO_OUT"].Index] = this.DD("계정대체출고");

            this._flexL[0, this._flexL.Cols["NO_IO_IN"].Index] = this.DD("계정대체입고");
            this._flexL[0, this._flexL.Cols["NO_IOLINE_IN"].Index] = this.DD("계정대체입고");
            this._flexL[0, this._flexL.Cols["DT_IO_IN"].Index] = this.DD("계정대체입고");
            this._flexL[0, this._flexL.Cols["CD_ITEM_IN"].Index] = this.DD("계정대체입고");
            this._flexL[0, this._flexL.Cols["QT_IO_IN"].Index] = this.DD("계정대체입고");

            this._flexL.SettingVersion = "0.0.0.2";
            this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #region Right
            this._flexR.BeginSetting(2, 1, false);

            this._flexR.SetCol("NO_SO", "수주번호", 80);
            this._flexR.SetCol("NO_GIR", "협조전번호", 80);
            this._flexR.SetCol("NO_IO", "출고번호", 80);
            this._flexR.SetCol("NO_SOLINE", "수주항번", 80);
            this._flexR.SetCol("SEQ_GIR", "협조전항번", 80);
            this._flexR.SetCol("NO_IOLINE", "출고항번", 80);
            this._flexR.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexR.SetCol("DT_GIR", "협조전일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexR.SetCol("DT_IO", "출고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexR.SetCol("YN_RETURN", "반품유무", 80, false, CheckTypeEnum.Y_N);
            this._flexR.SetCol("NM_QTIOTP", "출고유형", 80);
            this._flexR.SetCol("NM_SL", "출고창고", 80);
            this._flexR.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexR.SetCol("QT_IO", "출고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexR.SetCol("NM_EXCH", "통화명", 60);
            this._flexR.SetCol("RT_EXCH", "환율", 60, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flexR.SetCol("UM_EX_SO", "수주단가(외화)", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexR.SetCol("UM_SO", "수주단가(원화)", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexR.SetCol("AM_IO", "금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flexR.SetCol("NO_IO_OUT", "번호", 80);
            this._flexR.SetCol("NO_IOLINE_OUT", "항번", 80);
            this._flexR.SetCol("DT_IO_OUT", "일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexR.SetCol("CD_ITEM_OUT", "품목", 80);
            this._flexR.SetCol("QT_IO_OUT", "수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);

            this._flexR.SetCol("NO_IO_IN", "번호", 80);
            this._flexR.SetCol("NO_IOLINE_IN", "항번", 80);
            this._flexR.SetCol("DT_IO_IN", "일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexR.SetCol("CD_ITEM_IN", "품목", 80);
            this._flexR.SetCol("QT_IO_IN", "수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);

            this._flexR[0, this._flexR.Cols["NO_IO_OUT"].Index] = this.DD("계정대체출고");
            this._flexR[0, this._flexR.Cols["NO_IOLINE_OUT"].Index] = this.DD("계정대체출고");
            this._flexR[0, this._flexR.Cols["DT_IO_OUT"].Index] = this.DD("계정대체출고");
            this._flexR[0, this._flexR.Cols["CD_ITEM_OUT"].Index] = this.DD("계정대체출고");
            this._flexR[0, this._flexR.Cols["QT_IO_OUT"].Index] = this.DD("계정대체출고");

            this._flexR[0, this._flexR.Cols["NO_IO_IN"].Index] = this.DD("계정대체입고");
            this._flexR[0, this._flexR.Cols["NO_IOLINE_IN"].Index] = this.DD("계정대체입고");
            this._flexR[0, this._flexR.Cols["DT_IO_IN"].Index] = this.DD("계정대체입고");
            this._flexR[0, this._flexR.Cols["CD_ITEM_IN"].Index] = this.DD("계정대체입고");
            this._flexR[0, this._flexR.Cols["QT_IO_IN"].Index] = this.DD("계정대체입고");

            this._flexR.SettingVersion = "0.0.0.2";
            this._flexR.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion
        }

        private void InitEvent()
        {
            this.btn도움말.Click += new EventHandler(this.btn도움말_Click);

            this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!BeforeSearch()) return;
                if (string.IsNullOrEmpty(this.dtp기준일자.Text))
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl기준일자.Text);
                    return;
                }

                if (this.chk재고품제외.Checked)
                {
                    dt = this._biz.Search(new object[] { this.bpc회사.QueryWhereIn_Pipe,
                                                         this.dtp기준일자.Text,
                                                         this.txt수주번호.Text });
                }
                else
                {
                    dt = this._biz.Search1(new object[] { this.bpc회사.QueryWhereIn_Pipe,
                                                          this.dtp기준일자.Text,
                                                          this.txt수주번호.Text });
                }

                dt.TableName = this.PageID;
                this._pivot.DataSource = dt;
                this._flexH.Binding = dt;

                if (!this._flexH.HasNormalRow)
                    ShowMessage(공통메세지.조건에해당하는내용이없습니다);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataSet ds;

            try
            {
                ds = this._biz.SearchDetail(new object[] { D.GetString(this._flexH["CD_COMPANY"]),
                                                           this.dtp기준일자.Text,
                                                           D.GetString(this._flexH["NO_SO"]),
                                                           D.GetString(this._flexH["NO_LINE"]),
                                                           D.GetString(this._flexH["NO_SOLINE"]),
                                                           (this.chk재고품제외.Checked == true ? "Y" : "N") });

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                    this._flexL.Binding = ds.Tables[0];

                if (ds != null && ds.Tables.Count > 1 && ds.Tables[1] != null)
                    this._flexR.Binding = ds.Tables[1];
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn도움말_Click(object sender, EventArgs e)
        {
            string message;

            try
            {
                message = @"※ 수량 설명
수주수량 : 수주 받은 수량
입고수량 : 일반품 입고된 수량
입고반품수량 : 일반품 입고 되었다 매입처로 반품된 수량
출고수량 : 일반품 출고된 수량
출고반품수량 : 일반품 출고 되었다 반품된 수량
재고포장수량 : 재고품 창고에서 일반품 창고로 포장되어서 내려온 수량
재고출고수량 : 재고품 출고된 수량
재고반품수량 : 재고품 출고 되었다 반품된 수량
계정대체출고수량 : 일반품 -> 재고품으로 전환하기 위해 출고 시킨 수량 (출고되었다 반품되면 재고품이라도 일반품으로 재입고 됨)
계정대체입고수량 : 일반품 -> 재고품으로 전환하기 위해 입고 시킨 수량 (출고되었다 반품되면 재고품이라도 일반품으로 재입고 됨)
총수량 : 일반품 창고에 있어야 하는 수량";

                this.ShowMessage(message);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
    }
}

using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.Windows.Print;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace cz
{
    public partial class P_CZ_PR_OPOUT_PO_CNT_RPT : PageBase
    {
        private P_CZ_PR_OPOUT_PO_CNT_RPT_BIZ _biz = new P_CZ_PR_OPOUT_PO_CNT_RPT_BIZ();
        private bool bGridrowChanging = false;
        private FlexGrid _flex;
        private DataTable dt_Plant;
        public P_CZ_PR_OPOUT_PO_CNT_RPT()
        {
            try
            {
                this.InitializeComponent();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this._flexM1.DetailGrids = new FlexGrid[] { this._flexD1 };
            this._flexM2.DetailGrids = new FlexGrid[] { this._flexD2 };
            this._flexM3.DetailGrids = new FlexGrid[] { this._flexD3 };
            this._flexM4.DetailGrids = new FlexGrid[] { this._flexD4 };
            this._flexM1.WhenRowChangeThenGetDetail = true;
            this._flexM2.WhenRowChangeThenGetDetail = true;
            this._flexM3.WhenRowChangeThenGetDetail = true;
            this._flexM4.WhenRowChangeThenGetDetail = true;

            #region GridM1
            this._flexM1.BeginSetting(1, 1, false);
            this._flexM1.SetCol("CHK", "S", 30, true, CheckTypeEnum.Y_N);
            this._flexM1.SetCol("NO_PO", "발주번호", 100, false);
            this._flexM1.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            this._flexM1.SetDummyColumn(new string[] { "CHK" });
            #endregion

            #region GridD1
            this._flexD1.BeginSetting(1, 1, false);
            this._flexD1.SetCol("CD_PLANT", "공장코드", 100, false);
            this._flexD1.SetCol("NO_PO", "발주번호", 100, false);
            this._flexD1.SetCol("NO_LINE", "순번", 50, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD1.SetCol("CD_PARTNER", "외주처코드", 100, false);
            this._flexD1.SetCol("LN_PARTNER", "외주처명", 100, false);
            this._flexD1.SetCol("CD_ITEM", "품목코드", 100, 20, false);
            this._flexD1.SetCol("NM_ITEM", "품목명", 140, false);
            this._flexD1.SetCol("NO_DESIGN", "도면번호", 100, false);
            this._flexD1.SetCol("STND_ITEM", "규격", 120, false);
            this._flexD1.SetCol("UNIT_IM", "단위", 40, false);
            this._flexD1.SetCol("QT_ITEM", "지시수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD1.SetCol("QT_PO", "발주수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD1.SetCol("QT_RCV", "실적수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD1.SetCol("QT_BAD", "불량수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD1.SetCol("QT_CLS", "마감수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD1.SetCol("CD_EXCH", "환종코드", 70, false);
            this._flexD1.SetCol("NM_EXCH", "환종명", 60, false);
            this._flexD1.SetCol("RT_EXCH", "환율", 90, 17, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flexD1.SetCol("UM_EX", "외화단가", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD1.SetCol("UM", "단가", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD1.SetCol("AM_EX", "외화금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD1.SetCol("AM", "금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD1.SetCol("AM_VAT", "부가세", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD1.SetCol("AM_TOTAL", "총금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD1.SetCol("NO_EMP", "담당자코드", 100, false);
            this._flexD1.SetCol("NM_KOR", "담당자", 100, false);
            this._flexD1.SetCol("FG_TAX", "과세구분코드", 100, false);
            this._flexD1.SetCol("NM_FG_TAX", "과세구분", 100, false);
            this._flexD1.SetCol("VAT_RATE", "부가세율", 90, 17, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flexD1.SetCol("NO_WO", "작업지시번호", 100, false);
            this._flexD1.SetCol("DT_PO", "발주일", 80, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexD1.SetCol("DT_DUE", "납기일", 80, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexD1.SetCol("CD_OP", "OP", 25, false);
            this._flexD1.SetCol("CD_WC", "작업장", 50, false);
            this._flexD1.SetCol("NM_WC", "작업장명", 100, false);
            this._flexD1.SetCol("CD_WCOP", "공정", 50, false);
            this._flexD1.SetCol("NM_OP", "공정명", 100, false);
            this._flexD1.SetCol("DC_RMK", "적요", 120, 100, false);
            this._flexD1.SetCol("UNIT_CH", "변환단위", 100, false);
            this._flexD1.SetCol("QT_CHCOEF", "변환계수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD1.SetCol("OLD_QT_PO", "변환전수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD1.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            this._flexD1.SetExceptSumCol(new string[] { "NO_LINE",
                                                        "QT_CLS",
                                                        "RT_EXCH",
                                                        "UM_EX",
                                                        "UM" });
            this._flexD1.Cols["NO_LINE"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD1.AddMyMenu = true;
            this._flexD1.AddMenuSeperator();
            ToolStripMenuItem toolStripMenuItem1 = this._flexD1.AddPopup(this.DD("메뉴이동"));
            this._flexD1.AddMenuItem(toolStripMenuItem1, this.DD("공정외주발주등록"), new EventHandler(this.Menu_Click));
            this._flexD1.AddMenuItem(toolStripMenuItem1, this.DD("작업지시등록"), new EventHandler(this.Menu_Click));
            #endregion

            #region GridM2
            this._flexM2.BeginSetting(1, 1, false);
            this._flexM2.SetCol("CHK", "S", 30, true, CheckTypeEnum.Y_N);
            this._flexM2.SetCol("CD_PARTNER", "외주처코드", 100, false);
            this._flexM2.SetCol("LN_PARTNER", "외주처명", 100, false);
            this._flexM2.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            this._flexM2.SetDummyColumn(new string[] { "CHK" });
            #endregion

            #region GridD2
            this._flexD2.BeginSetting(1, 1, false);
            this._flexD2.SetCol("CD_PLANT", "공장코드", 100, false);
            this._flexD2.SetCol("NO_PO", "발주번호", 100, false);
            this._flexD2.SetCol("NO_LINE", "순번", 50, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD2.SetCol("CD_PARTNER", "외주처코드", 100, false);
            this._flexD2.SetCol("LN_PARTNER", "외주처명", 100, false);
            this._flexD2.SetCol("CD_ITEM", "품목코드", 100, 20, false);
            this._flexD2.SetCol("NM_ITEM", "품목명", 140, false);
            this._flexD2.SetCol("NO_DESIGN", "도면번호", 100, false);
            this._flexD2.SetCol("STND_ITEM", "규격", 120, false);
            this._flexD2.SetCol("UNIT_IM", "단위", 40, false);
            this._flexD2.SetCol("QT_ITEM", "지시수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD2.SetCol("QT_PO", "발주수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD2.SetCol("QT_RCV", "실적수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD2.SetCol("QT_BAD", "불량수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD2.SetCol("QT_CLS", "마감수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD2.SetCol("CD_EXCH", "환종코드", 70, false);
            this._flexD2.SetCol("NM_EXCH", "환종명", 60, false);
            this._flexD2.SetCol("RT_EXCH", "환율", 90, 17, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flexD2.SetCol("UM_EX", "외화단가", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD2.SetCol("UM", "단가", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD2.SetCol("AM_EX", "외화금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD2.SetCol("AM", "금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD2.SetCol("AM_VAT", "부가세", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD2.SetCol("AM_TOTAL", "총금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD2.SetCol("NO_EMP", "담당자코드", 100, false);
            this._flexD2.SetCol("NM_KOR", "담당자", 100, false);
            this._flexD2.SetCol("FG_TAX", "과세구분코드", 100, false);
            this._flexD2.SetCol("NM_FG_TAX", "과세구분", 100, false);
            this._flexD2.SetCol("VAT_RATE", "부가세율", 90, 17, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flexD2.SetCol("NO_WO", "작업지시번호", 100, false);
            this._flexD2.SetCol("DT_PO", "발주일", 80, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexD2.SetCol("DT_DUE", "납기일", 80, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexD2.SetCol("CD_OP", "OP", 25, false);
            this._flexD2.SetCol("CD_WC", "작업장", 50, false);
            this._flexD2.SetCol("NM_WC", "작업장명", 100, false);
            this._flexD2.SetCol("CD_WCOP", "공정", 50, false);
            this._flexD2.SetCol("NM_OP", "공정명", 100, false);
            this._flexD2.SetCol("DC_RMK", "적요", 120, 100, false);
            this._flexD2.SetCol("UNIT_CH", "변환단위", 100, false);
            this._flexD2.SetCol("QT_CHCOEF", "변환계수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD2.SetCol("OLD_QT_PO", "변환전수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD2.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            this._flexD2.SetExceptSumCol(new string[] { "NO_LINE",
                                                        "QT_CLS",
                                                        "RT_EXCH",
                                                        "UM_EX",
                                                        "UM" });
            this._flexD2.Cols["NO_LINE"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD2.AddMyMenu = true;
            this._flexD2.AddMenuSeperator();
            ToolStripMenuItem toolStripMenuItem2 = this._flexD2.AddPopup(this.DD("메뉴이동"));
            this._flexD2.AddMenuItem(toolStripMenuItem2, this.DD("공정외주발주등록"), new EventHandler(this.Menu_Click));
            this._flexD2.AddMenuItem(toolStripMenuItem2, this.DD("작업지시등록"), new EventHandler(this.Menu_Click));
            #endregion

            #region GridM3
            this._flexM3.BeginSetting(1, 1, false);
            this._flexM3.SetCol("CHK", "S", 30, true, CheckTypeEnum.Y_N);
            this._flexM3.SetCol("CD_ITEM", "품목코드", 100, 20, false);
            this._flexM3.SetCol("NM_ITEM", "품목명", 140, false);
            this._flexM3.SetCol("NO_DESIGN", "도면번호", 100, false);
            this._flexM3.SetCol("STND_ITEM", "규격", 120, false);
            this._flexM3.SetCol("UNIT_IM", "단위", 40, false);
            this._flexM3.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            this._flexM3.SetDummyColumn(new string[] { "CHK" });
            #endregion

            #region GridD3
            this._flexD3.BeginSetting(1, 1, false);
            this._flexD3.SetCol("CD_PLANT", "공장코드", 100, false);
            this._flexD3.SetCol("NO_PO", "발주번호", 100, false);
            this._flexD3.SetCol("NO_LINE", "순번", 50, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD3.SetCol("CD_PARTNER", "외주처코드", 100, false);
            this._flexD3.SetCol("LN_PARTNER", "외주처명", 100, false);
            this._flexD3.SetCol("CD_ITEM", "품목코드", 100, 20, false);
            this._flexD3.SetCol("NM_ITEM", "품목명", 140, false);
            this._flexD3.SetCol("NO_DESIGN", "도면번호", 100, false);
            this._flexD3.SetCol("STND_ITEM", "규격", 120, false);
            this._flexD3.SetCol("UNIT_IM", "단위", 40, false);
            this._flexD3.SetCol("QT_ITEM", "지시수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD3.SetCol("QT_PO", "발주수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD3.SetCol("QT_RCV", "실적수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD3.SetCol("QT_BAD", "불량수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD3.SetCol("QT_CLS", "마감수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD3.SetCol("CD_EXCH", "환종코드", 70, false);
            this._flexD3.SetCol("NM_EXCH", "환종명", 60, false);
            this._flexD3.SetCol("RT_EXCH", "환율", 90, 17, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flexD3.SetCol("UM_EX", "외화단가", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD3.SetCol("UM", "단가", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD3.SetCol("AM_EX", "외화금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD3.SetCol("AM", "금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD3.SetCol("AM_VAT", "부가세", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD3.SetCol("AM_TOTAL", "총금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD3.SetCol("NO_EMP", "담당자코드", 100, false);
            this._flexD3.SetCol("NM_KOR", "담당자", 100, false);
            this._flexD3.SetCol("FG_TAX", "과세구분코드", 100, false);
            this._flexD3.SetCol("NM_FG_TAX", "과세구분", 100, false);
            this._flexD3.SetCol("VAT_RATE", "부가세율", 90, 17, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flexD3.SetCol("NO_WO", "작업지시번호", 100, false);
            this._flexD3.SetCol("DT_PO", "발주일", 80, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexD3.SetCol("DT_DUE", "납기일", 80, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexD3.SetCol("CD_OP", "OP", 25, false);
            this._flexD3.SetCol("CD_WC", "작업장", 50, false);
            this._flexD3.SetCol("NM_WC", "작업장명", 100, false);
            this._flexD3.SetCol("CD_WCOP", "공정", 50, false);
            this._flexD3.SetCol("NM_OP", "공정명", 100, false);
            this._flexD3.SetCol("DC_RMK", "적요", 120, 100, false);
            this._flexD3.SetCol("UNIT_CH", "변환단위", 100, false);
            this._flexD3.SetCol("QT_CHCOEF", "변환계수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD3.SetCol("OLD_QT_PO", "변환전수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD3.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            this._flexD3.SetExceptSumCol(new string[] { "NO_LINE",
                                                        "QT_CLS",
                                                        "RT_EXCH",
                                                        "UM_EX",
                                                        "UM" });
            this._flexD3.Cols["NO_LINE"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD3.AddMyMenu = true;
            this._flexD3.AddMenuSeperator();
            ToolStripMenuItem toolStripMenuItem3 = this._flexD3.AddPopup(this.DD("메뉴이동"));
            this._flexD3.AddMenuItem(toolStripMenuItem3, this.DD("공정외주발주등록"), new EventHandler(this.Menu_Click));
            this._flexD3.AddMenuItem(toolStripMenuItem3, this.DD("작업지시등록"), new EventHandler(this.Menu_Click));
            #endregion

            #region GridM4
            this._flexM4.BeginSetting(1, 1, false);
            this._flexM4.SetCol("CHK", "S", 30, true, CheckTypeEnum.Y_N);
            this._flexM4.SetCol("NO_WO", "작업지시번호", 100, false);
            this._flexM4.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            this._flexM4.SetDummyColumn(new string[] { "CHK" });
            #endregion

            #region GridD4
            this._flexD4.BeginSetting(1, 1, false);
            this._flexD4.SetCol("CD_PLANT", "공장코드", 100, false);
            this._flexD4.SetCol("NO_PO", "발주번호", 100, false);
            this._flexD4.SetCol("NO_LINE", "순번", 50, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD4.SetCol("CD_PARTNER", "외주처코드", 100, false);
            this._flexD4.SetCol("LN_PARTNER", "외주처명", 100, false);
            this._flexD4.SetCol("CD_ITEM", "품목코드", 100, 20, false);
            this._flexD4.SetCol("NM_ITEM", "품목명", 140, false);
            this._flexD4.SetCol("NO_DESIGN", "도면번호", 100, false);
            this._flexD4.SetCol("STND_ITEM", "규격", 120, false);
            this._flexD4.SetCol("UNIT_IM", "단위", 40, false);
            this._flexD4.SetCol("QT_ITEM", "지시수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD4.SetCol("QT_PO", "발주수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD4.SetCol("QT_RCV", "실적수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD4.SetCol("QT_BAD", "불량수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD4.SetCol("QT_CLS", "마감수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD4.SetCol("CD_EXCH", "환종코드", 70, false);
            this._flexD4.SetCol("NM_EXCH", "환종명", 60, false);
            this._flexD4.SetCol("RT_EXCH", "환율", 90, 17, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flexD4.SetCol("UM_EX", "외화단가", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD4.SetCol("UM", "단가", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD4.SetCol("AM_EX", "외화금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD4.SetCol("AM", "금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD4.SetCol("AM_VAT", "부가세", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD4.SetCol("AM_TOTAL", "총금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD4.SetCol("NO_EMP", "담당자코드", 100, false);
            this._flexD4.SetCol("NM_KOR", "담당자", 100, false);
            this._flexD4.SetCol("FG_TAX", "과세구분코드", 100, false);
            this._flexD4.SetCol("NM_FG_TAX", "과세구분", 100, false);
            this._flexD4.SetCol("VAT_RATE", "부가세율", 90, 17, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flexD4.SetCol("NO_WO", "작업지시번호", 100, false);
            this._flexD4.SetCol("DT_PO", "발주일", 80, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexD4.SetCol("DT_DUE", "납기일", 80, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexD4.SetCol("CD_OP", "OP", 25, false);
            this._flexD4.SetCol("CD_WC", "작업장", 50, false);
            this._flexD4.SetCol("NM_WC", "작업장명", 100, false);
            this._flexD4.SetCol("CD_WCOP", "공정", 50, false);
            this._flexD4.SetCol("NM_OP", "공정명", 100, false);
            this._flexD4.SetCol("DC_RMK", "적요", 120, 100, false);
            this._flexD4.SetCol("UNIT_CH", "변환단위", 100, false);
            this._flexD4.SetCol("QT_CHCOEF", "변환계수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD4.SetCol("OLD_QT_PO", "변환전수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD4.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            this._flexD4.SetExceptSumCol(new string[] { "NO_LINE",
                                                        "QT_CLS",
                                                        "RT_EXCH",
                                                        "UM_EX",
                                                        "UM" });
            this._flexD4.Cols["NO_LINE"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD4.AddMyMenu = true;
            this._flexD4.AddMenuSeperator();
            ToolStripMenuItem toolStripMenuItem4 = this._flexD4.AddPopup(this.DD("메뉴이동"));
            this._flexD4.AddMenuItem(toolStripMenuItem4, this.DD("공정외주발주등록"), new EventHandler(this.Menu_Click));
            this._flexD4.AddMenuItem(toolStripMenuItem4, this.DD("작업지시등록"), new EventHandler(this.Menu_Click));
            #endregion
        }

        private void InitEvent()
        {
            this.tabControl1.SelectedIndexChanged += new EventHandler(this.tabControl1_SelectedIndexChanged);
            this.cbo사업장.SelectedValueChanged += new EventHandler(this.cbo사업장_SelectedValueChanged);
            this.ctx공정.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
            this.ctx작업장.CodeChanged += new EventHandler(this.OnBpControl_CodeChanged);
            this.ctx작업장.QueryAfter += new BpQueryHandler(this.OnBpControl_QueryAfter);
            this.ctx작업장.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
            this.ctx외주처.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
            this.bp품목시작.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
            this.bp품목끝.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);

            this._flexM1.BeforeRowChange += new RangeEventHandler(this._flex_BeforeRowChange);
            this._flexM1.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);
            this._flexM1.CheckHeaderClick += new EventHandler(this._flex_CheckHeaderClick);
            this._flexD1.HelpClick += new EventHandler(this._flex_HelpClick);
            this._flexM2.BeforeRowChange += new RangeEventHandler(this._flex_BeforeRowChange);
            this._flexM2.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);
            this._flexM2.CheckHeaderClick += new EventHandler(this._flex_CheckHeaderClick);
            this._flexD2.HelpClick += new EventHandler(this._flex_HelpClick);
            this._flexM3.BeforeRowChange += new RangeEventHandler(this._flex_BeforeRowChange);
            this._flexM3.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);
            this._flexM3.CheckHeaderClick += new EventHandler(this._flex_CheckHeaderClick);
            this._flexD3.HelpClick += new EventHandler(this._flex_HelpClick);
            this._flexM4.BeforeRowChange += new RangeEventHandler(this._flex_BeforeRowChange);
            this._flexM4.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);
            this._flexM4.CheckHeaderClick += new EventHandler(this._flex_CheckHeaderClick);
            this._flexD4.HelpClick += new EventHandler(this._flex_HelpClick);
        }

        protected override void InitPaint()
        {
            base.InitPaint();
            this.oneGrid1.UseCustomLayout = true;
            this.bpP_Date.IsNecessaryCondition = true;
            this.bpP_Bizarea.IsNecessaryCondition = true;
            this.bpP_Plant.IsNecessaryCondition = true;
            this.oneGrid1.InitCustomLayout();
            DataSet comboData = this.GetComboData(new string[] { "NC;MA_BIZAREA", "NC;MA_PLANT", "N;PU_C000013", "N;PU_C000026" });
            this.cbo사업장.DataSource = comboData.Tables[0];
            this.cbo사업장.DisplayMember = "NAME";
            this.cbo사업장.ValueMember = "CODE";
            if (this.LoginInfo.BizAreaCode != "")
                this.cbo사업장.SelectedValue = this.LoginInfo.BizAreaCode;
            else
                this.cbo사업장.SelectedIndex = 0;
            this.dt_Plant = comboData.Tables[1];
            this.사업장에따른공장설정();
            this.cbo공장.DataSource = comboData.Tables[1];
            this.cbo공장.DisplayMember = "NAME";
            this.cbo공장.ValueMember = "CODE";
            if (comboData.Tables[0].Select("CODE = '" + this.LoginInfo.CdPlant + "'").Length == 1)
                this.cbo공장.SelectedValue = this.LoginInfo.CdPlant;
            else if (this.cbo공장.Items.Count > 0)
                this.cbo공장.SelectedIndex = 0;
            DataTable dataTable = comboData.Tables[2].Clone();
            foreach (DataRow row in comboData.Tables[2].Select("CODE IN ('001', '002')"))
                dataTable.ImportRow(row);
            this.cbo일자구분.DataSource = dataTable;
            this.cbo일자구분.DisplayMember = "NAME";
            this.cbo일자구분.ValueMember = "CODE";
            this.cbo일자구분.SelectedIndex = 0;
            this.cbo처리구분.DataSource = comboData.Tables[3];
            this.cbo처리구분.DisplayMember = "NAME";
            this.cbo처리구분.ValueMember = "CODE";
            this.dtp발주일자.Mask = this.GetFormatDescription((DataDictionaryTypes)5, (FormatTpType)7, (FormatFgType)0);
            this.dtp발주일자.StartDate = this.MainFrameInterface.GetDateTimeToday();
            this.dtp발주일자.StartDateToString = this.MainFrameInterface.GetStringFirstDayInMonth;
            this.dtp발주일자.EndDate = this.MainFrameInterface.GetDateTimeToday();
            this.dtp발주일자.EndDateToString = this.MainFrameInterface.GetStringToday;
        }

        private void 사업장에따른공장설정()
        {
            if (this.cbo사업장.Items.Count == 0 || this.cbo사업장.SelectedValue == null || this.dt_Plant == null)
                return;
            DataTable dataTable = this.dt_Plant.Clone();
            DataRow[] dataRowArray = this.dt_Plant.Select("CD_BIZAREA IN ('" + this.cbo사업장.SelectedValue.ToString() + "') OR CODE = '' ", "");
            if (dataRowArray.Length > 0)
            {
                foreach (DataRow row in dataRowArray)
                    dataTable.ImportRow(row);
            }
      this.cbo공장.DataSource = dataTable;
            this.cbo공장.DisplayMember = "NAME";
            this.cbo공장.ValueMember = "CODE";
        }

        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch())
                return false;
            if (!this.일자구분선택여부)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD("날짜타입") });
                this.cbo일자구분.Focus();
                return false;
            }
            if (!this.발주일자시작등록여부)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD("날짜타입의 시작일") });
                this.dtp발주일자.Focus();
                return false;
            }
            if (!this.발주일자끝등록여부)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD("날짜타입의 종료일") });
                this.dtp발주일자.Focus();
                return false;
            }
            if (!Checker.IsValid(this.dtp발주일자, true, this.cbo일자구분.Text))
                return false;
            if (!this.사업장선택여부)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl사업장.Text });
                this.cbo사업장.Focus();
                return false;
            }
            if (this.공장선택여부)
                return true;
            this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl공장.Text });
            this.cbo공장.Focus();
            return false;
        }
        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!base.BeforeSearch())
                    return;
                string str = this.tabControl1.SelectedTab.Tag.ToString();
                DataTable dataTable = this._biz.Search(new object[] { this.LoginInfo.CompanyCode,
                                                                      this.cbo일자구분.SelectedValue.ToString(),   
                                                                      this.dtp발주일자.StartDateToString,
                                                                      this.dtp발주일자.EndDateToString,
                                                                      this.cbo사업장.SelectedValue.ToString(),
                                                                      this.cbo공장.SelectedValue.ToString(),
                                                                      this.ctx외주처.CodeValue,
                                                                      this.ctx작업장.CodeValue,
                                                                      this.ctx공정.CodeValue,
                                                                      this.bp품목시작.CodeValue,
                                                                      this.bp품목끝.CodeValue,
                                                                      this.cbo처리구분.SelectedValue.ToString(),
                                                                      str,
                                                                      Global.SystemLanguage.MultiLanguageLpoint });
                switch (str)
                {
                    case "NO_PO":
                        this._flexM1.Binding = dataTable;
                        this.ToolBarPrintButtonEnabled = this._flexM1.HasNormalRow;
                        if (this._flexM1.HasNormalRow)
                            return;
                        break;
                    case "CD_PARTNER":
                        this._flexM2.Binding = dataTable;
                        this.ToolBarPrintButtonEnabled = this._flexM2.HasNormalRow;
                        if (this._flexM2.HasNormalRow)
                            return;
                        break;
                    case "CD_ITEM":
                        this._flexM3.Binding = dataTable;
                        this.ToolBarPrintButtonEnabled = this._flexM3.HasNormalRow;
                        if (this._flexM3.HasNormalRow)
                            return;
                        break;
                    case "NO_WO":
                        this._flexM4.Binding = dataTable;
                        this.ToolBarPrintButtonEnabled = this._flexM4.HasNormalRow;
                        if (this._flexM4.HasNormalRow)
                            return;
                        break;
                }
                this.ShowMessage(공통메세지.조건에해당하는내용이없습니다, new string[0]);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforePrint() || !this.MsgAndSave(PageActionMode.Print))
                    return;
                ReportHelper rptHelper = null;
                string str = this.tabControl1.SelectedTab.Tag.ToString();
                switch (str)
                {
                    case "NO_PO":
                        rptHelper = new ReportHelper("R_PR_OPOUT_PO_CNT_RPT_0", "공정외주발주진행현황(발주번호별)");
                        this._flex = this._flexM1;
                        break;
                    case "CD_PARTNER":
                        rptHelper = new ReportHelper("R_PR_OPOUT_PO_CNT_RPT_1", "공정외주발주진행현황(외주처별)");
                        this._flex = this._flexM2;
                        break;
                    case "CD_ITEM":
                        rptHelper = new ReportHelper("R_PR_OPOUT_PO_CNT_RPT_2", "공정외주발주진행현황(품목별)");
                        this._flex = this._flexM3;
                        break;
                    case "NO_WO":
                        rptHelper = new ReportHelper("R_PR_OPOUT_PO_CNT_RPT_3", "공정외주발주진행현황(작업지시별)");
                        this._flex = this._flexM4;
                        break;
                }
                DataRow[] dataRowArray = this._flex.DataTable.Select("CHK = 'Y'");
                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다, new string[0]);
                }
                else
                {
                    rptHelper.가로출력();
                    rptHelper.SetData("발주일자시작", this.dtp발주일자.StartDateToString);
                    rptHelper.SetData("발주일자끝", this.dtp발주일자.EndDateToString);
                    rptHelper.SetData("사업장코드", this.cbo사업장.SelectedValue.ToString());
                    rptHelper.SetData("사업장명", this.cbo사업장.Text.Substring(0, this.cbo사업장.Text.IndexOf("(")));
                    rptHelper.SetData("공장코드", this.cbo공장.SelectedValue.ToString());
                    rptHelper.SetData("공장명", this.cbo공장.Text.Substring(0, this.cbo공장.Text.IndexOf("(")));
                    rptHelper.SetData("외주처코드", this.ctx외주처.CodeValue);
                    rptHelper.SetData("외주처명", this.ctx외주처.CodeName);
                    rptHelper.SetData("작업장코드", this.ctx작업장.CodeValue);
                    rptHelper.SetData("작업장명", this.ctx작업장.CodeName);
                    rptHelper.SetData("공정코드", this.ctx공정.CodeValue);
                    rptHelper.SetData("공정명", this.ctx공정.CodeName);
                    rptHelper.SetData("품목시작", this.bp품목시작.CodeValue);
                    rptHelper.SetData("품목시작", this.bp품목시작.CodeName);
                    rptHelper.SetData("품목끝코드", this.bp품목끝.CodeValue);
                    rptHelper.SetData("품목끝명", this.bp품목끝.CodeName);
                    switch (str)
                    {
                        case "NO_PO":
                            if (!this.PrintGridSetting(ref this._flexM1, ref this._flexD1, "NO_PO", ref rptHelper))
                                return;
                            break;
                        case "CD_PARTNER":
                            if (!this.PrintGridSetting(ref this._flexM2, ref this._flexD2, "CD_PARTNER", ref rptHelper))
                                return;
                            break;
                        case "CD_ITEM":
                            if (!this.PrintGridSetting(ref this._flexM3, ref this._flexD3, "CD_ITEM", ref rptHelper))
                                return;
                            break;
                        case "NO_WO":
                            if (!this.PrintGridSetting(ref this._flexM4, ref this._flexD4, "NO_WO", ref rptHelper))
                                return;
                            break;
                    }
                    rptHelper.Print();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private bool PrintGridSetting(
          ref FlexGrid _flexM,
          ref FlexGrid _flexD,
          string strFilterCondition,
          ref ReportHelper rptHelper)
        {
            if (!_flexM.HasNormalRow)
                return false;
            DataRow[] dataRowArray1 = _flexM.DataTable.Select("CHK = 'Y'", "", DataViewRowState.CurrentRows);
            if (dataRowArray1.Length == 0)
                return false;
            string filterExpression = strFilterCondition + " IN (";
            for (int index = 0; index < dataRowArray1.Length; ++index)
            {
                string str = filterExpression + " '" + dataRowArray1[index][strFilterCondition].ToString() + "'";
                filterExpression = dataRowArray1.Length - 1 == index ? str + ") " : str + ", ";
            }
            DataRow[] dataRowArray2 = _flexD.DataTable.Select(filterExpression, strFilterCondition);
            if (dataRowArray2.Length == 0)
                return false;
            DataTable dt = _flexD.DataTable.Clone();
            foreach (DataRow row in dataRowArray2)
                dt.ImportRow(row);
            this.CaptionMapping(new FlexGrid[] { _flexM, _flexD }, ref dt);
            rptHelper.SetDataTable(dt);
            return true;
        }

        private void CaptionMapping(FlexGrid[] _flexArr, ref DataTable dt)
        {
            foreach (DataColumn column in dt.Columns)
            {
                foreach (FlexGrid flexGrid in _flexArr)
                {
                    if (flexGrid.Cols.Contains(column.ColumnName))
                        column.Caption = flexGrid.Cols[column.ColumnName].Caption;
                }
            }
        }

        private void OnBpControl_CodeChanged(object sender, EventArgs e)
        {
            try
            {
                if (!(sender is BpCodeTextBox bpCodeTextBox))
                    return;
                switch (bpCodeTextBox.Name)
                {
                    case "bp작업장":
                        this.ctx공정.CodeValue = "";
                        this.ctx공정.CodeName = "";
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void OnBpControl_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                HelpID helpId = e.HelpID;
                if (helpId != HelpID.P_MA_PITEM_SUB && helpId != HelpID.P_MA_WC_SUB)
                {
                    if (helpId == HelpID.P_PR_WCOP_SUB)
                    {
                        if (!this.공장선택여부)
                        {
                            this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl공장.Text });
                            e.QueryCancel = true;
                            this.cbo공장.Focus();
                        }
                        else if (this.ctx작업장.CodeValue == "")
                        {
                            this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.ctx작업장.Text });
                            e.QueryCancel = true;
                            this.ctx작업장.Focus();
                        }
                        else
                        {
                            e.HelpParam.P09_CD_PLANT = this.cbo공장.SelectedValue.ToString();
                            e.HelpParam.P65_CODE5 = this.cbo공장.Text.Replace(" ", "").Remove(0, this.cbo공장.Text.Replace(" ", "").IndexOf(")", 0) + 1);
                            e.HelpParam.P20_CD_WC = this.ctx작업장.CodeValue;
                        }
                    }
                }
                else if (!this.공장선택여부)
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl공장.Text });
                    e.QueryCancel = true;
                    this.cbo공장.Focus();
                }
                else
                    e.HelpParam.P09_CD_PLANT = this.cbo공장.SelectedValue.ToString();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void OnBpControl_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                if (e.HelpID != HelpID.P_MA_WC_SUB)
                    return;
                this.ctx공정.CodeValue = "";
                this.ctx공정.CodeName = "";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_BeforeRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (this.bGridrowChanging)
                    return;
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                this.ToolBarSearchButtonEnabled = false;
                this.bGridrowChanging = false;
                FlexGrid _flexM = sender as FlexGrid;
                if (sender == null)
                    return;
                this.ChangeFilter(ref _flexM, ref _flexM.DetailGrids[0], this.tabControl1.SelectedTab.Tag.ToString());
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this.ToolBarSearchButtonEnabled = true;
                this.bGridrowChanging = true;
            }
        }

        private void ChangeFilter(ref FlexGrid _flexM, ref FlexGrid _flexD, string strSelectedTabName)
        {
            DataTable dataTable = new DataTable();
            string str1 = "";
            string str2 = "";
            switch (strSelectedTabName)
            {
                case "NO_PO":
                    str2 = _flexM["NO_PO"].ToString();
                    break;
                case "CD_PARTNER":
                    str2 = _flexM["CD_PARTNER"].ToString();
                    break;
                case "CD_ITEM":
                    str2 = _flexM["CD_ITEM"].ToString();
                    break;
                case "NO_WO":
                    str2 = _flexM["NO_WO"].ToString();
                    break;
            }
            object[] objArray = new object[] { this.LoginInfo.CompanyCode,
                                               this.cbo일자구분.SelectedValue.ToString(),
                                               this.dtp발주일자.StartDateToString,
                                               this.dtp발주일자.EndDateToString,
                                               this.cbo사업장.SelectedValue.ToString(),
                                               this.cbo공장.SelectedValue.ToString(),
                                               this.ctx외주처.CodeValue,
                                               this.ctx작업장.CodeValue,
                                               this.ctx공정.CodeValue,
                                               this.bp품목시작.CodeValue,
                                               this.bp품목끝.CodeValue,
                                               this.cbo처리구분.SelectedValue.ToString(),
                                               strSelectedTabName,
                                               str2,
                                               Global.SystemLanguage.MultiLanguageLpoint };
            switch (strSelectedTabName)
            {
                case "NO_PO":
                    str1 = "NO_PO = '" + _flexM["NO_PO"].ToString() + "' ";
                    if (_flexM.DetailQueryNeed)
                    {
                        dataTable = this._biz.SearchDetail(objArray);
                        break;
                    }
                    break;
                case "CD_PARTNER":
                    str1 = "CD_PARTNER = '" + _flexM["CD_PARTNER"].ToString() + "'";
                    if (_flexM.DetailQueryNeed)
                    {
                        dataTable = this._biz.SearchDetail(objArray);
                        break;
                    }
                    break;
                case "CD_ITEM":
                    str1 = "CD_ITEM = '" + _flexM["CD_ITEM"].ToString() + "'";
                    if (_flexM.DetailQueryNeed)
                    {
                        dataTable = this._biz.SearchDetail(objArray);
                        break;
                    }
                    break;
                case "NO_WO":
                    str1 = "NO_WO = '" + _flexM["NO_WO"].ToString() + "'";
                    if (_flexM.DetailQueryNeed)
                    {
                        dataTable = this._biz.SearchDetail(objArray);
                        break;
                    }
                    break;
            }
            _flexD.BindingAdd(dataTable, str1);
            _flexM.DetailQueryNeed = false;
        }

        private void _flex_CheckHeaderClick(object sender, EventArgs e)
        {
            try
            {
                if (!(sender is FlexGrid flexGrid) || !flexGrid.HasNormalRow || (flexGrid[flexGrid.Rows.Fixed, "CHK"].ToString() == "Y" ? CheckEnum.Checked : CheckEnum.Unchecked) == CheckEnum.Unchecked)
                    return;
                int row = flexGrid.Row;
                for (int index = flexGrid.Rows.Fixed; index <= flexGrid.Rows.Count - 1; ++index)
                {
                    if (flexGrid.DetailQueryNeedByRow(index))
                        flexGrid.Row = index;
                }
            flexGrid.Row = row;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_HelpClick(object sender, EventArgs e)
        {
            try
            {
                if (!(sender is FlexGrid flex) || !flex.HasNormalRow)
                    return;
                switch (flex.Cols[flex.Col].Name)
                {
                    case "NO_PO":
                        this.Menu_Jump(flex, "PO");
                        break;
                    case "NO_WO":
                        this.Menu_Jump(flex, "WO");
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (this.tabControl1.SelectedTab.Tag.ToString())
                {
                    case "NO_PO":
                        this.ToolBarPrintButtonEnabled = this._flexM1.HasNormalRow;
                        break;
                    case "CD_PARTNER":
                        this.ToolBarPrintButtonEnabled = this._flexM2.HasNormalRow;
                        break;
                    case "CD_ITEM":
                        this.ToolBarPrintButtonEnabled = this._flexM3.HasNormalRow;
                        break;
                    case "NO_WO":
                        this.ToolBarPrintButtonEnabled = this._flexM4.HasNormalRow;
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void cbo사업장_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                this.사업장에따른공장설정();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public bool 일자구분선택여부 => this.cbo일자구분.SelectedValue != null && !(this.cbo일자구분.SelectedValue.ToString() == "");

        public bool 발주일자시작등록여부 => !(this.dtp발주일자.StartDateToString == "");

        public bool 발주일자끝등록여부 => !(this.dtp발주일자.EndDateToString == "");

        public bool 사업장선택여부 => this.cbo사업장.SelectedValue != null && !(this.cbo사업장.SelectedValue.ToString() == "");

        public bool 공장선택여부 => this.cbo공장.SelectedValue != null && !(this.cbo공장.SelectedValue.ToString() == "");

        public bool 외주처등록여부 => !(this.ctx외주처.CodeValue == "");

        public bool 작업장등록여부 => !(this.ctx작업장.CodeValue == "");

        public bool 공정등록여부 => !(this.ctx공정.CodeValue == "");

        public bool 품목시작등록여부 => !(this.bp품목시작.CodeValue == "");

        public bool 품목끝등록여부 => !(this.bp품목끝.CodeValue == "");

        public void Menu_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(sender is ToolStripMenuItem toolStripMenuItem))
                    return;
                FlexGrid flex = new FlexGrid();
                switch (this.tabControl1.SelectedIndex)
                {
                    case 0:
                        flex = this._flexD1;
                        break;
                    case 1:
                        flex = this._flexD2;
                        break;
                    case 2:
                        flex = this._flexD3;
                        break;
                    case 3:
                        flex = this._flexD4;
                        break;
                }
                if (flex == null || !flex.HasNormalRow)
                    return;
                if (toolStripMenuItem.Name == this.DD("공정외주발주등록"))
                    this.Menu_Jump(flex, "PO");
                else if (toolStripMenuItem.Name == this.DD("작업지시등록"))
                    this.Menu_Jump(flex, "WO");
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Menu_Jump(FlexGrid flex, string MenuGubun)
        {
            try
            {
                if (!flex.HasNormalRow)
                    return;
                string str1 = string.Empty;
                string str2 = string.Empty;
                string empty = string.Empty;
                object[] objArray1 = new object[0];
                if (MenuGubun == "PO")
                {
                    str1 = "NO_PO";
                    if (D.GetString(flex[str1]) == string.Empty)
                        return;
                    str2 = "P_CZ_PR_OPOUT_PO_REG";
                }
                else if (MenuGubun == "WO")
                {
                    str1 = "NO_WO";
                    if (D.GetString(flex[str1]) == string.Empty)
                        return;
                    str2 = "P_CZ_PR_WO_REG_NEW";
                }
                object[] objArray2 = new object[] { D.GetString(flex["CD_PLANT"]), D.GetString(flex[str1]) };
                if (str2 == string.Empty)
                    return;
                if (this.MainFrameInterface.IsExistPage(str2, false))
                    this.MainFrameInterface.UnLoadPage(str2, false);
                string str3 = MA.PageName(str2);
                this.CallOtherPageMethod(str2, str3, this.Grant, objArray2);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
    }
}

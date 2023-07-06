using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.Erpiu.Windows.OneControls;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;
using Duzon.Windows.Print;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_PU_POST_RPT_WINTEC : PageBase
    {
        private P_CZ_PU_POST_RPT_WINTEC_BIZ _biz = new P_CZ_PU_POST_RPT_WINTEC_BIZ();
        private DataTable gdt_plant = null;
        private FlexGrid flex_Toolmenu = null;
        private string str사업장 = "";
        private string str공장명 = "";
        private string str발주일_납기일 = "";
        private string str발주일_납기일시작 = "";
        private string str발주일_납기일끝 = "";
        private string str발주상태 = "";
        private string str거래구분 = "";
        private string str구매그룹 = "";
        private string str담당자 = "";
        private string str단위선택 = "";
        private string str처리구분 = "";
        private string strCD_CC = "";
        private string REQ_PURGRP = "";
        private string REQ_DEPT = "";
        private string REQ_EMP = "";
        private ToolStripMenuItem parent = null;
        public P_CZ_PU_POST_RPT_WINTEC()
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

            this.ctx요청자.CodeValue = Settings1.Default.bp_REEMP_SAVE;
            this.ctx요청자.CodeName = Settings1.Default.bp_REEMP_SAVE1;
            this.ctx요청부서.CodeValue = Settings1.Default.bp_REDEPT_SAVE;
            this.ctx요청부서.CodeName = Settings1.Default.bp_REDEPT_SAVE1;

            this.gdt_plant = new DataTable();


            this.InitGrid();
            this.InitEvent();
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            DataSet comboData = this.GetComboData(new string[]
            {
        "NC;MA_BIZAREA",
        "SC;MA_PLANT",
        "N;PU_C000013",
        "S;PU_C000009",
        "S;PU_C000016",
        "N;PU_C000024",
        "N;PU_C000026",
        "S;PU_C000014",
        "S;MA_B000046",
        "S;PU_C000005",
        "S;MA_B000005",
        "S;PU_C000012"
            });
            this.cbo사업장.DataSource = comboData.Tables[0];
            this.cbo사업장.DisplayMember = "NAME";
            this.cbo사업장.ValueMember = "CODE";
            this.cbo사업장.SelectedIndex = 0;
            if (D.GetString(Global.MainFrame.LoginInfo.BizAreaCode) == string.Empty)
                this.cbo사업장.SelectedIndex = 0;
            else
                this.cbo사업장.SelectedValue = Global.MainFrame.LoginInfo.BizAreaCode;
            this.gdt_plant = comboData.Tables[1];
            this.SetPlantCombo();
            DataTable dataTable = comboData.Tables[2].Clone();
            foreach (DataRow row in comboData.Tables[2].Select("CODE IN ('001', '002')"))
                dataTable.ImportRow(row);
            this.cbo일자구분.DataSource = dataTable;
            this.cbo일자구분.DisplayMember = "NAME";
            this.cbo일자구분.ValueMember = "CODE";
            this.cbo일자구분.SelectedIndex = 0;
            this.cbo거래구분.DataSource = comboData.Tables[4];
            this.cbo거래구분.DisplayMember = "NAME";
            this.cbo거래구분.ValueMember = "CODE";
            this.cbo단위선택.DataSource = comboData.Tables[5];
            this.cbo단위선택.DisplayMember = "NAME";
            this.cbo단위선택.ValueMember = "CODE";
            this.cbo처리구분.DataSource = comboData.Tables[6];
            this.cbo처리구분.DisplayMember = "NAME";
            this.cbo처리구분.ValueMember = "CODE";
            this._flexM1.SetDataMap("FG_PAYMENT", comboData.Tables[7].Copy(), "CODE", "NAME");
            this._flexM1.SetDataMap("FG_TAX", comboData.Tables[8].Copy(), "CODE", "NAME");
            this._flexM1.SetDataMap("TP_UM_TAX", comboData.Tables[9].Copy(), "CODE", "NAME");
            this._flexM1.SetDataMap("CD_EXCH", comboData.Tables[10].Copy(), "CODE", "NAME");
            this._flexM1.SetDataMap("FG_TAXP", comboData.Tables[11].Copy(), "CODE", "NAME");
            this._flexD2.SetDataMap("FG_PAYMENT", comboData.Tables[7].Copy(), "CODE", "NAME");
            this._flexD2.SetDataMap("FG_TAX", comboData.Tables[8].Copy(), "CODE", "NAME");
            this._flexD2.SetDataMap("TP_UM_TAX", comboData.Tables[9].Copy(), "CODE", "NAME");
            this._flexD2.SetDataMap("CD_EXCH", comboData.Tables[10].Copy(), "CODE", "NAME");
            this._flexD2.SetDataMap("FG_TAXP", comboData.Tables[11].Copy(), "CODE", "NAME");
            this._flexD3.SetDataMap("FG_PAYMENT", comboData.Tables[7].Copy(), "CODE", "NAME");
            this._flexD3.SetDataMap("FG_TAX", comboData.Tables[8].Copy(), "CODE", "NAME");
            this._flexD3.SetDataMap("TP_UM_TAX", comboData.Tables[9].Copy(), "CODE", "NAME");
            this._flexD3.SetDataMap("CD_EXCH", comboData.Tables[10].Copy(), "CODE", "NAME");
            this._flexD3.SetDataMap("FG_TAXP", comboData.Tables[11].Copy(), "CODE", "NAME");
            this._flexD4.SetDataMap("FG_PAYMENT", comboData.Tables[7].Copy(), "CODE", "NAME");
            this._flexD4.SetDataMap("FG_TAX", comboData.Tables[8].Copy(), "CODE", "NAME");
            this._flexD4.SetDataMap("TP_UM_TAX", comboData.Tables[9].Copy(), "CODE", "NAME");
            this._flexD4.SetDataMap("CD_EXCH", comboData.Tables[10].Copy(), "CODE", "NAME");
            this._flexD4.SetDataMap("FG_TAXP", comboData.Tables[11].Copy(), "CODE", "NAME");
            this._flexD5.SetDataMap("FG_PAYMENT", comboData.Tables[7].Copy(), "CODE", "NAME");
            this._flexD5.SetDataMap("FG_TAX", comboData.Tables[8].Copy(), "CODE", "NAME");
            this._flexD5.SetDataMap("TP_UM_TAX", comboData.Tables[9].Copy(), "CODE", "NAME");
            this._flexD5.SetDataMap("CD_EXCH", comboData.Tables[10].Copy(), "CODE", "NAME");
            this._flexD5.SetDataMap("FG_TAXP", comboData.Tables[11].Copy(), "CODE", "NAME");
            this._flexD6.SetDataMap("FG_PAYMENT", comboData.Tables[7].Copy(), "CODE", "NAME");
            this._flexD6.SetDataMap("FG_TAX", comboData.Tables[8].Copy(), "CODE", "NAME");
            this._flexD6.SetDataMap("TP_UM_TAX", comboData.Tables[9].Copy(), "CODE", "NAME");
            this._flexD6.SetDataMap("CD_EXCH", comboData.Tables[10].Copy(), "CODE", "NAME");
            this._flexD6.SetDataMap("FG_TAXP", comboData.Tables[11].Copy(), "CODE", "NAME");
            this._flexD7.SetDataMap("FG_PAYMENT", comboData.Tables[7].Copy(), "CODE", "NAME");
            this._flexD7.SetDataMap("FG_TAX", comboData.Tables[8].Copy(), "CODE", "NAME");
            this._flexD7.SetDataMap("TP_UM_TAX", comboData.Tables[9].Copy(), "CODE", "NAME");
            this._flexD7.SetDataMap("CD_EXCH", comboData.Tables[10].Copy(), "CODE", "NAME");
            this._flexD7.SetDataMap("FG_TAXP", comboData.Tables[11].Copy(), "CODE", "NAME");
            this.dtp일자구분.StartDateToString = this.MainFrameInterface.GetStringFirstDayInMonth;
            this.dtp일자구분.EndDateToString = this.MainFrameInterface.GetStringToday;
            this.cbo창고.UseGrant = false;
            this.oneGrid1.UseCustomLayout = true;
            this.bpPanelControl1.IsNecessaryCondition = true;
            this.oneGrid1.InitCustomLayout();
        }

        private void SetPlantCombo()
        {
            try
            {
                DataTable dataTable = this.gdt_plant.Clone();
                DataRow[] dataRowArray = this.gdt_plant.Select("CD_BIZAREA IN('','" + this.cbo사업장.SelectedValue.ToString().Trim() + "')");
                if (dataRowArray != null && dataRowArray.Length > 0)
                {
                    for (int index = 0; index < dataRowArray.Length; ++index)
                        dataTable.ImportRow(dataRowArray[index]);
                }
              this.cbo공장.DataSource = dataTable;
                this.cbo공장.DisplayMember = "NAME";
                this.cbo공장.ValueMember = "CODE";
                if (dataTable.Rows.Count >= 2)
                    this.cbo공장.SelectedIndex = 1;
                else
                    this.cbo공장.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void InitGrid()
        {
            this._flexM1.DetailGrids = new FlexGrid[] { this._flexD1 };
            this._flexM2.DetailGrids = new FlexGrid[] { this._flexD2 };
            this._flexM3.DetailGrids = new FlexGrid[] { this._flexD3 };
            this._flexM4.DetailGrids = new FlexGrid[] { this._flexD4 };
            this._flexM5.DetailGrids = new FlexGrid[] { this._flexD5 };
            this._flexM6.DetailGrids = new FlexGrid[] { this._flexD6 };
            this._flexM7.DetailGrids = new FlexGrid[] { this._flexD7 };
            this._flexM8.DetailGrids = new FlexGrid[] { this._flexD8 };
            this._flexM1.WhenRowChangeThenGetDetail = true;
            this._flexM2.WhenRowChangeThenGetDetail = true;
            this._flexM3.WhenRowChangeThenGetDetail = true;
            this._flexM4.WhenRowChangeThenGetDetail = true;
            this._flexM5.WhenRowChangeThenGetDetail = true;
            this._flexM6.WhenRowChangeThenGetDetail = true;
            this._flexM7.WhenRowChangeThenGetDetail = true;
            this._flexM8.WhenRowChangeThenGetDetail = true;

            #region M1
            this._flexM1.BeginSetting(1, 1, false);
            this._flexM1.SetCol("CHK", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flexM1.SetCol("NO_PO", "발주번호", 100, false);
            this._flexM1.SetCol("NM_PLANT", "공장", 150, false);
            this._flexM1.SetCol("CD_PARTNER", "거래처코드", 100, false);
            this._flexM1.SetCol("LN_PARTNER", "거래처", 120, false);
            this._flexM1.SetCol("DT_PO", "발주일자", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexM1.SetCol("NM_PURGRP", "구매그룹", 120, false);
            this._flexM1.SetCol("NM_KOR", "담당자", 120, false);
            this._flexM1.SetCol("NM_TPPO", "발주형태명", 120, false);
            this._flexM1.SetCol("NM_TRANS", "거래구분", 120, false);
            this._flexM1.SetCol("NM_PURCHASE", "매입형태", 120, false);
            this._flexM1.SetCol("FG_PAYMENT", "지급조건", 120, false);
            this._flexM1.SetCol("FG_TAX", "과세구분", 120, false);
            this._flexM1.SetCol("TP_UM_TAX", "부가세포함", 80, false);
            this._flexM1.SetCol("CD_EXCH", "환종", 40, false);
            this._flexM1.SetCol("FG_TAXP", "계산서처리방법", 120, false);
            this._flexM1.SetCol("YN_AM", "유무환", 50, false);
            this._flexM1.SetCol("DC_RMK", "발주비고1", 100);
            this._flexM1.SetCol("DC_RMK2", "발주비고2", 100);
            this._flexM1.SetCol("FG_WEB", "확정여부(WEB)", false);
            this._flexM1.SetCol("DT_WEB", "확인일자(WEB)", false);
            this._flexM1.Cols["DT_WEB"].Format = "####/##/##";
            this._flexM1.SetStringFormatCol("DT_WEB");
            this._flexM1.SetCol("NO_ORDER", "ORDER번호", 100);
            this._flexM1.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            this._flexM1.SetDummyColumn(new string[] { "CHK" });
            #endregion

            #region D1
            this._flexD1.BeginSetting(1, 1, false);
            this._flexD1.SetCol("NO_LINE", "발주항번", 60, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD1.SetCol("CD_ITEM", "품목코드", 100, 20, false);
            this._flexD1.SetCol("NM_ITEM", "품목명", 140, false);
            this._flexD1.SetCol("STND_ITEM", "규격", 120, false);
            this._flexD1.SetCol("GRP_ITEM", "품목군코드", 80);
            this._flexD1.SetCol("NM_ITEMGRP", "품목군명", 120);
            this._flexD1.SetCol("GRP_MFG", "제품군코드", 80);
            this._flexD1.SetCol("NM_GRP_MFG", "제품군명", 120);
            this._flexD1.SetCol("STND_DETAIL_ITEM", "세부규격", 80);
            this._flexD1.SetCol("MAT_ITEM", "재질", 80);
            this._flexD1.SetCol("NM_PARTNER", "주거래처", 100);
            this._flexD1.SetCol("UNIT_IM", "단위", 40, false);
            this._flexD1.SetCol("DT_LIMIT", "납기일", 70, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexD1.SetCol("QT_PO", "발주량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD1.SetCol("QT_REV_MM", "가입고수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD1.SetCol("QT_REQ", "의뢰량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD1.SetCol("QT_GR", "입고량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD1.SetCol("QT_CLS", "마감수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD1.SetCol("QT_RETURN", "반품수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD1.SetCol("QT_TR", "수입수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD1.SetCol("QT_REMAIN", "미입고량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD1.SetCol("AM_EX", "외화금액", 90, 17, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexD1.SetCol("AM", "원화금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD1.SetCol("VAT", "부가세", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD1.SetCol("NM_POST", "발주상태", 60, false);
            this._flexD1.SetCol("NM_CC", "C/C명", 100, false);
            this._flexD1.SetCol("NO_PR", "요청번호", 100, false);
            this._flexD1.SetCol("NO_PRLINE", "요청항번", 60, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD1.SetCol("NO_APP", "품의번호", 100, false);
            this._flexD1.SetCol("NO_APPLINE", "품의항번", 60, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD1.SetCol("NM_FG_RCV", "수불형태", 140, false);
            this._flexD1.SetCol("YN_RETURN", "반품여부", 60, false);
            this._flexD1.SetCol("NO_CONTRACT", "계약번호", false);
            this._flexD1.SetCol("NO_CTLINE", "계약항번", false);
            this._flexD1.SetCol("NM_PROJECT", "프로젝트", 120);
            this._flexD1.SetCol("CD_PJT", "프로젝트코드", 120);
            if (Config.MA_ENV.PJT형여부 == "Y")
            {
                this._flexD1.SetCol("CD_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 코드" : "프로젝트 품목코드", 140, false, typeof(string));
                this._flexD1.SetCol("NM_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 명" : "프로젝트 품목명", 140, false, typeof(string));
                this._flexD1.SetCol("SEQ_PROJECT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 항번" : "프로젝트항번", 120, false, typeof(decimal));
                this._flexD1.SetCol("PJT_ITEM_STND", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 규격" : "프로젝트 품목규격", 140, false, typeof(string));
                this._flexD1.SetCol("NO_PJT_DESIGN", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 도면번호" : "프로젝트 도면번호", 140, false, typeof(string));
            }
            this._flexD1.SetCol("DT_PLAN", "납품예정일", 70, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexD1.SetCol("YN_IMPORT", "수입여부", 90, false);
            this._flexD1.SetCol("YN_ORDER", "자동승인여부", 90, false);
            this._flexD1.SetCol("DC1", "발주라인비고1", 200, false);
            this._flexD1.SetCol("DC2", "발주라인비고2", 200, false);
            this._flexD1.SetCol("DC3", "발주라인비고3", 200, false);
            this._flexD1.SetCol("NM_REQ_PURGRP", "요청구매그룹", 100);
            this._flexD1.SetCol("REQ_NM_KOR", "요청자", 100);
            this._flexD1.SetCol("REQ_NM_DEPT", "요청부서", 100);
            this._flexD1.SetCol("NO_MODEL", "모델코드", 100, false);
            this._flexD1.SetCol("UNIT_PO", "발주단위", 60);
            this._flexD1.SetCol("NM_SL", "입고창고명", 100);
            this._flexD1.SetCol("NO_DESIGN", "품목도면번호", 100);
            this._flexD1.SetCol("QT_PASS_MM", "합격수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD1.SetCol("QT_BAD_MM", "불량수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD1.SettingVersion = "1.1.3";
            this._flexD1.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            this._flexD1.Cols["NO_LINE"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD1.Cols["NO_PRLINE"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD1.Cols["NO_APPLINE"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD1.Cols["YN_RETURN"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD1.Cols["YN_IMPORT"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD1.Cols["YN_ORDER"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD1.SetExceptSumCol(new string[] { "NO_LINE",
                                                        "NO_PRLINE",
                                                        "NO_APPLINE",
                                                        "CD_SL",
                                                        "NM_SL",
                                                        "QT_INV" });
            this._flexD1.AddMyMenu = true;
            this._flexD1.AddMenuSeperator();
            this._flexD1.AddMenuItem("key", "", new EventHandler(this.PopupItemSelectdEventHandler));
            this._flexD1.AddMenuItem("key1", "", new EventHandler(this.PopupItemSelectdEventHandler));
            this._flexD1.AddMenuItem("key2", "", new EventHandler(this.PopupItemSelectdEventHandler));
            this._flexD1.AddMenuItem("key3", "", new EventHandler(this.PopupItemSelectdEventHandler));
            #endregion

            #region M2
            this._flexM2.BeginSetting(1, 1, false);
            this._flexM2.SetCol("CHK", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flexM2.SetCol("DT_PO", "발주일자", 70, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexM2.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            this._flexM2.SetDummyColumn(new string[] { "CHK" });
            #endregion

            #region D2
            this._flexD2.BeginSetting(1, 1, false);
            this._flexD2.SetCol("NO_PO", "발주번호", 100, false);
            this._flexD2.SetCol("NM_PLANT", "공장", 150, false);
            this._flexD2.SetCol("CD_PARTNER", "거래처코드", 100, false);
            this._flexD2.SetCol("LN_PARTNER", "거래처", 120, false);
            this._flexD2.SetCol("DT_PO", "발주일자", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexD2.SetCol("NM_PURGRP", "구매그룹", 120, false);
            this._flexD2.SetCol("NM_KOR", "담당자", 120, false);
            this._flexD2.SetCol("NM_TPPO", "발주형태명", 120, false);
            this._flexD2.SetCol("NM_TRANS", "거래구분", 120, false);
            this._flexD2.SetCol("NM_PURCHASE", "매입형태", 120, false);
            this._flexD2.SetCol("FG_PAYMENT", "지급조건", 120, false);
            this._flexD2.SetCol("FG_TAX", "과세구분", 120, false);
            this._flexD2.SetCol("TP_UM_TAX", "부가세포함", 80, false);
            this._flexD2.SetCol("CD_EXCH", "환종", 40, false);
            this._flexD2.SetCol("FG_TAXP", "계산서처리방법", 120, false);
            this._flexD2.SetCol("YN_AM", "유무환", 50, false);
            this._flexD2.SetCol("DC_RMK", "발주비고1", 100);
            this._flexD2.SetCol("NM_CC", "C/C명", 100, false);
            this._flexD2.SetCol("NO_LINE", "발주항번", 60, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD2.SetCol("CD_ITEM", "품목코드", 100, 20, false);
            this._flexD2.SetCol("NM_ITEM", "품목명", 140, false);
            this._flexD2.SetCol("STND_ITEM", "규격", 120, false);
            this._flexD2.SetCol("GRP_ITEM", "품목군코드", 80);
            this._flexD2.SetCol("NM_ITEMGRP", "품목군명", 120);
            this._flexD2.SetCol("GRP_MFG", "제품군코드", 80);
            this._flexD2.SetCol("NM_GRP_MFG", "제품군명", 120);
            this._flexD2.SetCol("STND_DETAIL_ITEM", "세부규격", 80);
            this._flexD2.SetCol("MAT_ITEM", "재질", 80);
            this._flexD2.SetCol("NM_PARTNER", "주거래처", 100);
            this._flexD2.SetCol("UNIT_IM", "단위", 40, false);
            this._flexD2.SetCol("DT_LIMIT", "납기일", 70, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexD2.SetCol("QT_PO", "발주량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD2.SetCol("QT_REV_MM", "가입고수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD2.SetCol("QT_REQ", "의뢰량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD2.SetCol("QT_GR", "입고량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD2.SetCol("QT_CLS", "마감수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD2.SetCol("QT_RETURN", "반품수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD2.SetCol("QT_TR", "수입수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD2.SetCol("QT_REMAIN", "미입고량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD2.SetCol("AM_EX", "외화금액", 90, 17, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexD2.SetCol("AM", "원화금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD2.SetCol("VAT", "부가세", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD2.SetCol("NM_POST", "발주상태", 60, false);
            this._flexD2.SetCol("NO_PR", "요청번호", 100, false);
            this._flexD2.SetCol("NO_PRLINE", "요청항번", 60, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD2.SetCol("NO_APP", "품의번호", 100, false);
            this._flexD2.SetCol("NO_APPLINE", "품의항번", 60, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD2.SetCol("NM_PROJECT", "프로젝트", 120);
            this._flexD2.SetCol("CD_PJT", "프로젝트코드", 120);
            if (Config.MA_ENV.PJT형여부 == "Y")
            {
                this._flexD2.SetCol("CD_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 코드" : "프로젝트 품목코드", 140, false, typeof(string));
                this._flexD2.SetCol("NM_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 명" : "프로젝트 품목명", 140, false, typeof(string));
                this._flexD2.SetCol("SEQ_PROJECT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 항번" : "프로젝트항번", 120, false, typeof(decimal));
                this._flexD2.SetCol("PJT_ITEM_STND", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 규격" : "프로젝트 품목규격", 140, false, typeof(string));
                this._flexD2.SetCol("NO_PJT_DESIGN", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 도면번호" : "프로젝트 도면번호", 140, false, typeof(string));
            }
            this._flexD2.SetCol("DT_PLAN", "납품예정일", 70, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexD2.SetCol("NM_FG_RCV", "수불형태", 140, false);
            this._flexD2.SetCol("YN_RETURN", "반품여부", 60, false);
            this._flexD2.SetCol("YN_IMPORT", "수입여부", 90, false);
            this._flexD2.SetCol("YN_ORDER", "자동승인여부", 90, false);
            this._flexD2.SetCol("NM_REQ_PURGRP", "요청구매그룹", 100);
            this._flexD2.SetCol("REQ_NM_KOR", "요청자", 100);
            this._flexD2.SetCol("REQ_NM_DEPT", "요청부서", 100);
            this._flexD2.SetCol("DC1", "발주라인비고1", 200, false);
            this._flexD2.SetCol("DC2", "발주라인비고2", 200, false);
            this._flexD2.SetCol("DC3", "발주라인비고3", 200, false);
            this._flexD2.SetCol("NO_CONTRACT", "계약번호", false);
            this._flexD2.SetCol("NO_CTLINE", "계약항번", false);
            this._flexD2.SetCol("NO_MODEL", "모델코드", 100, false);
            this._flexD2.SetCol("FG_WEB", "확정여부(WEB)", false);
            this._flexD2.SetCol("DT_WEB", "확인일자(WEB)", false);
            this._flexD2.Cols["DT_WEB"].Format = "####/##/##";
            this._flexD2.SetStringFormatCol("DT_WEB");
            this._flexD2.SetCol("UNIT_PO", "발주단위", 60);
            this._flexD2.SetCol("NO_DESIGN", "품목도면번호", 100);
            this._flexD2.SetCol("DC_RMK2", "발주비고2", 100);
            this._flexD2.SetCol("NO_ORDER", "ORDER번호", 100);
            this._flexD2.SetCol("CD_SL", "창고", 100);
            this._flexD2.SetCol("NM_SL", "창고명", 100);
            this._flexD2.SetCol("QT_PASS_MM", "합격수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD2.SetCol("QT_BAD_MM", "불량수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD2.SettingVersion = "1.1.4";
            this._flexD2.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            this._flexD2.Cols["NO_LINE"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD2.Cols["NO_PRLINE"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD2.Cols["NO_APPLINE"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD2.Cols["YN_RETURN"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD2.Cols["YN_IMPORT"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD2.Cols["YN_ORDER"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD2.SetExceptSumCol(new string[] { "NO_LINE", "NO_PRLINE", "NO_APPLINE" });
            this._flexD2.AddMyMenu = true;
            this._flexD2.AddMenuSeperator();
            this._flexD2.AddMenuItem("key", "", new EventHandler(this.PopupItemSelectdEventHandler));
            this._flexD2.AddMenuItem("key1", "", new EventHandler(this.PopupItemSelectdEventHandler));
            this._flexD2.AddMenuItem("key2", "", new EventHandler(this.PopupItemSelectdEventHandler));
            this._flexD2.AddMenuItem("key3", "", new EventHandler(this.PopupItemSelectdEventHandler));
            #endregion

            #region M3
            this._flexM3.BeginSetting(1, 1, false);
            this._flexM3.SetCol("CHK", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flexM3.SetCol("CD_PARTNER", "거래처코드", 100);
            this._flexM3.SetCol("LN_PARTNER", "거래처명", 140);
            this._flexM3.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            this._flexM3.SetDummyColumn(new string[] { "CHK" });
            #endregion

            #region D3
            this._flexD3.BeginSetting(1, 1, false);
            this._flexD3.SetCol("NO_PO", "발주번호", 100, false);
            this._flexD3.SetCol("NM_PLANT", "공장", 150, false);
            this._flexD3.SetCol("CD_PARTNER", "거래처코드", 100, false);
            this._flexD3.SetCol("LN_PARTNER", "거래처", 120, false);
            this._flexD3.SetCol("DT_PO", "발주일자", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexD3.SetCol("NM_PURGRP", "구매그룹", 120, false);
            this._flexD3.SetCol("NM_KOR", "담당자", 120, false);
            this._flexD3.SetCol("NM_TPPO", "발주형태명", 120, false);
            this._flexD3.SetCol("NM_TRANS", "거래구분", 120, false);
            this._flexD3.SetCol("NM_PURCHASE", "매입형태", 120, false);
            this._flexD3.SetCol("FG_PAYMENT", "지급조건", 120, false);
            this._flexD3.SetCol("FG_TAX", "과세구분", 120, false);
            this._flexD3.SetCol("TP_UM_TAX", "부가세포함", 80, false);
            this._flexD3.SetCol("CD_EXCH", "환종", 40, false);
            this._flexD3.SetCol("FG_TAXP", "계산서처리방법", 120, false);
            this._flexD3.SetCol("YN_AM", "유무환", 50, false);
            this._flexD3.SetCol("DC_RMK", "발주비고1", 100);
            this._flexD3.SetCol("NM_CC", "C/C명", 100, false);
            this._flexD3.SetCol("NO_LINE", "발주항번", 60, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD3.SetCol("CD_ITEM", "품목코드", 100, 20, false);
            this._flexD3.SetCol("NM_ITEM", "품목명", 140, false);
            this._flexD3.SetCol("STND_ITEM", "규격", 120, false);
            this._flexD3.SetCol("GRP_ITEM", "품목군코드", 80);
            this._flexD3.SetCol("NM_ITEMGRP", "품목군명", 120);
            this._flexD3.SetCol("GRP_MFG", "제품군코드", 80);
            this._flexD3.SetCol("NM_GRP_MFG", "제품군명", 120);
            this._flexD3.SetCol("STND_DETAIL_ITEM", "세부규격", 80);
            this._flexD3.SetCol("MAT_ITEM", "재질", 80);
            this._flexD3.SetCol("NM_PARTNER", "주거래처", 100);
            this._flexD3.SetCol("UNIT_IM", "단위", 40, false);
            this._flexD3.SetCol("DT_LIMIT", "납기일", 70, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexD3.SetCol("QT_PO", "발주량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD3.SetCol("QT_REV_MM", "가입고수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD3.SetCol("QT_REQ", "의뢰량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD3.SetCol("QT_GR", "입고량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD3.SetCol("QT_CLS", "마감수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD3.SetCol("QT_RETURN", "반품수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD3.SetCol("QT_TR", "수입수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD3.SetCol("QT_REMAIN", "미입고량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD3.SetCol("AM_EX", "외화금액", 90, 17, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexD3.SetCol("AM", "원화금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD3.SetCol("VAT", "부가세", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD3.SetCol("NM_POST", "발주상태", 60, false);
            this._flexD3.SetCol("NO_PR", "요청번호", 100, false);
            this._flexD3.SetCol("NO_PRLINE", "요청항번", 60, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD3.SetCol("NO_APP", "품의번호", 100, false);
            this._flexD3.SetCol("NO_APPLINE", "품의항번", 60, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD3.SetCol("NM_PROJECT", "프로젝트", 120);
            this._flexD3.SetCol("CD_PJT", "프로젝트코드", 120);
            if (Config.MA_ENV.PJT형여부 == "Y")
            {
                this._flexD3.SetCol("CD_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 코드" : "프로젝트 품목코드", 140, false, typeof(string));
                this._flexD3.SetCol("NM_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 명" : "프로젝트 품목명", 140, false, typeof(string));
                this._flexD3.SetCol("SEQ_PROJECT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 항번" : "프로젝트항번", 120, false, typeof(decimal));
                this._flexD3.SetCol("PJT_ITEM_STND", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 규격" : "프로젝트 품목규격", 140, false, typeof(string));
                this._flexD3.SetCol("NO_PJT_DESIGN", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 도면번호" : "프로젝트 도면번호", 140, false, typeof(string));
            }
            this._flexD3.SetCol("DT_PLAN", "납품예정일", 70, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexD3.SetCol("NM_FG_RCV", "수불형태", 140, false);
            this._flexD3.SetCol("YN_RETURN", "반품여부", 60, false);
            this._flexD3.SetCol("YN_IMPORT", "수입여부", 90, false);
            this._flexD3.SetCol("YN_ORDER", "자동승인여부", 90, false);
            this._flexD3.SetCol("NM_REQ_PURGRP", "요청구매그룹", 100);
            this._flexD3.SetCol("REQ_NM_KOR", "요청자", 100);
            this._flexD3.SetCol("REQ_NM_DEPT", "요청부서", 100);
            this._flexD3.SetCol("DC1", "발주라인비고1", 200, false);
            this._flexD3.SetCol("DC2", "발주라인비고2", 200, false);
            this._flexD3.SetCol("DC3", "발주라인비고3", 200, false);
            this._flexD3.SetCol("NO_CONTRACT", "계약번호", false);
            this._flexD3.SetCol("NO_CTLINE", "계약항번", false);
            this._flexD3.SetCol("NO_MODEL", "모델코드", 100, false);
            this._flexD3.SetCol("FG_WEB", "확정여부(WEB)", false);
            this._flexD3.SetCol("DT_WEB", "확인일자(WEB)", false);
            this._flexD3.Cols["DT_WEB"].Format = "####/##/##";
            this._flexD3.SetStringFormatCol("DT_WEB");
            this._flexD3.SetCol("UNIT_PO", "발주단위", 60);
            this._flexD3.SetCol("NO_DESIGN", "품목도면번호", 100);
            this._flexD3.SetCol("DC_RMK2", "발주비고2", 100);
            this._flexD3.SetCol("NO_ORDER", "ORDER번호", 100);
            this._flexD3.SetCol("CD_SL", "창고", 100);
            this._flexD3.SetCol("NM_SL", "창고명", 100);
            this._flexD3.SetCol("QT_PASS_MM", "합격수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD3.SetCol("QT_BAD_MM", "불량수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD3.SettingVersion = "1.1.5";
            this._flexD3.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            this._flexD3.Cols["NO_LINE"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD3.Cols["NO_PRLINE"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD3.Cols["NO_APPLINE"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD3.Cols["YN_RETURN"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD3.Cols["YN_IMPORT"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD3.Cols["YN_ORDER"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD3.SetExceptSumCol(new string[] { "NO_LINE", "NO_PRLINE", "NO_APPLINE" });
            this._flexD3.AddMyMenu = true;
            this._flexD3.AddMenuSeperator();
            this._flexD3.AddMenuItem("key", "", new EventHandler(this.PopupItemSelectdEventHandler));
            this._flexD3.AddMenuItem("key1", "", new EventHandler(this.PopupItemSelectdEventHandler));
            this._flexD3.AddMenuItem("key2", "", new EventHandler(this.PopupItemSelectdEventHandler));
            this._flexD3.AddMenuItem("key3", "", new EventHandler(this.PopupItemSelectdEventHandler));
            #endregion

            #region M4
            this._flexM4.BeginSetting(1, 1, false);
            this._flexM4.SetCol("CHK", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flexM4.SetCol("CD_ITEM", "품목코드", 100, 20, false);
            this._flexM4.SetCol("NM_ITEM", "품목명", 140, false);
            this._flexM4.SetCol("STND_ITEM", "규격", 120, false);
            this._flexM4.SetCol("GRP_ITEM", "품목군코드", 80);
            this._flexM4.SetCol("NM_ITEMGRP", "품목군명", 120);
            this._flexM4.SetCol("GRP_MFG", "제품군코드", 80);
            this._flexM4.SetCol("NM_GRP_MFG", "제품군명", 120);
            this._flexM4.SetCol("STND_DETAIL_ITEM", "세부규격", 80);
            this._flexM4.SetCol("MAT_ITEM", "재질", 80);
            this._flexM4.SetCol("LN_PARTNER", "주거래처", 100);
            this._flexM4.SetCol("UNIT_IM", "단위", 40, false);
            this._flexM4.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            this._flexM4.SetDummyColumn(new string[] { "CHK" });
            #endregion

            #region  D4
            this._flexD4.BeginSetting(1, 1, false);
            this._flexD4.SetCol("NO_PO", "발주번호", 100, false);
            this._flexD4.SetCol("NM_PLANT", "공장", 150, false);
            this._flexD4.SetCol("CD_PARTNER", "거래처코드", 100, false);
            this._flexD4.SetCol("LN_PARTNER", "거래처", 120, false);
            this._flexD4.SetCol("DT_PO", "발주일자", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexD4.SetCol("NM_PURGRP", "구매그룹", 120, false);
            this._flexD4.SetCol("NM_KOR", "담당자", 120, false);
            this._flexD4.SetCol("NM_TPPO", "발주형태명", 120, false);
            this._flexD4.SetCol("NM_TRANS", "거래구분", 120, false);
            this._flexD4.SetCol("NM_PURCHASE", "매입형태", 120, false);
            this._flexD4.SetCol("FG_PAYMENT", "지급조건", 120, false);
            this._flexD4.SetCol("FG_TAX", "과세구분", 120, false);
            this._flexD4.SetCol("TP_UM_TAX", "부가세포함", 80, false);
            this._flexD4.SetCol("CD_EXCH", "환종", 40, false);
            this._flexD4.SetCol("FG_TAXP", "계산서처리방법", 120, false);
            this._flexD4.SetCol("YN_AM", "유무환", 50, false);
            this._flexD4.SetCol("DC_RMK", "발주비고1", 100);
            this._flexD4.SetCol("NM_CC", "C/C명", 100, false);
            this._flexD4.SetCol("NO_LINE", "발주항번", 60, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD4.SetCol("CD_ITEM", "품목코드", 100, 20, false);
            this._flexD4.SetCol("NM_ITEM", "품목명", 140, false);
            this._flexD4.SetCol("STND_ITEM", "규격", 120, false);
            this._flexD4.SetCol("UNIT_IM", "단위", 40, false);
            this._flexD4.SetCol("DT_LIMIT", "납기일", 70, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexD4.SetCol("QT_PO", "발주량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD4.SetCol("QT_REV_MM", "가입고수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD4.SetCol("QT_REQ", "의뢰량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD4.SetCol("QT_GR", "입고량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD4.SetCol("QT_CLS", "마감수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD4.SetCol("QT_RETURN", "반품수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD4.SetCol("QT_TR", "수입수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD4.SetCol("QT_REMAIN", "미입고량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD4.SetCol("AM_EX", "외화금액", 90, 17, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexD4.SetCol("AM", "원화금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD4.SetCol("VAT", "부가세", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD4.SetCol("NM_POST", "발주상태", 60, false);
            this._flexD4.SetCol("NO_PR", "요청번호", 100, false);
            this._flexD4.SetCol("NO_PRLINE", "요청항번", 60, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD4.SetCol("NO_APP", "품의번호", 100, false);
            this._flexD4.SetCol("NO_APPLINE", "품의항번", 60, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD4.SetCol("NM_PROJECT", "프로젝트", 120);
            this._flexD4.SetCol("CD_PJT", "프로젝트코드", 120);
            if (Config.MA_ENV.PJT형여부 == "Y")
            {
                this._flexD4.SetCol("CD_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 코드" : "프로젝트 품목코드", 140, false, typeof(string));
                this._flexD4.SetCol("NM_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 명" : "프로젝트 품목명", 140, false, typeof(string));
                this._flexD4.SetCol("SEQ_PROJECT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 항번" : "프로젝트항번", 120, false, typeof(decimal));
                this._flexD4.SetCol("PJT_ITEM_STND", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 규격" : "프로젝트 품목규격", 140, false, typeof(string));
                this._flexD4.SetCol("NO_PJT_DESIGN", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 도면번호" : "프로젝트 도면번호", 140, false, typeof(string));
            }
            this._flexD4.SetCol("DT_PLAN", "납품예정일", 70, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexD4.SetCol("NM_FG_RCV", "수불형태", 140, false);
            this._flexD4.SetCol("YN_RETURN", "반품여부", 60, false);
            this._flexD4.SetCol("YN_IMPORT", "수입여부", 90, false);
            this._flexD4.SetCol("YN_ORDER", "자동승인여부", 90, false);
            this._flexD4.SetCol("NM_REQ_PURGRP", "요청구매그룹", 100);
            this._flexD4.SetCol("REQ_NM_KOR", "요청자", 100);
            this._flexD4.SetCol("REQ_NM_DEPT", "요청부서", 100);
            this._flexD4.SetCol("DC1", "발주라인비고1", 200, false);
            this._flexD4.SetCol("DC2", "발주라인비고2", 200, false);
            this._flexD4.SetCol("DC3", "발주라인비고3", 200, false);
            this._flexD4.SetCol("NO_CONTRACT", "계약번호", false);
            this._flexD4.SetCol("NO_CTLINE", "계약항번", false);
            this._flexD4.SetCol("NO_MODEL", "모델코드", 100, false);
            this._flexD4.SetCol("FG_WEB", "확정여부(WEB)", false);
            this._flexD4.SetCol("DT_WEB", "확인일자(WEB)", false);
            this._flexD4.Cols["DT_WEB"].Format = "####/##/##";
            this._flexD4.SetStringFormatCol("DT_WEB");
            this._flexD4.SetCol("UNIT_PO", "발주단위", 60);
            this._flexD4.SetCol("NO_DESIGN", "품목도면번호", 100);
            this._flexD4.SetCol("DC_RMK2", "발주비고2", 100);
            this._flexD4.SetCol("NO_ORDER", "ORDER번호", 100);
            this._flexD4.SetCol("CD_SL", "창고", 100);
            this._flexD4.SetCol("NM_SL", "창고명", 100);
            this._flexD4.SetCol("QT_PASS_MM", "합격수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD4.SetCol("QT_BAD_MM", "불량수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD4.SettingVersion = "1.1.4";
            this._flexD4.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            this._flexD4.Cols["NO_LINE"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD4.Cols["NO_PRLINE"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD4.Cols["NO_APPLINE"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD4.Cols["YN_RETURN"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD4.Cols["YN_IMPORT"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD4.Cols["YN_ORDER"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD4.SetExceptSumCol(new string[] { "NO_LINE", "NO_PRLINE", "NO_APPLINE" });
            this._flexD4.AddMyMenu = true;
            this._flexD4.AddMenuSeperator();
            this._flexD4.AddMenuItem("key", "", new EventHandler(this.PopupItemSelectdEventHandler));
            this._flexD4.AddMenuItem("key1", "", new EventHandler(this.PopupItemSelectdEventHandler));
            this._flexD4.AddMenuItem("key2", "", new EventHandler(this.PopupItemSelectdEventHandler));
            this._flexD4.AddMenuItem("key3", "", new EventHandler(this.PopupItemSelectdEventHandler));
            #endregion

            #region M5
            this._flexM5.BeginSetting(1, 1, false);
            this._flexM5.SetCol("CHK", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flexM5.SetCol("CD_PJT", "프로젝트", 100, false);
            this._flexM5.SetCol("NM_PROJECT", "프로젝트명", 120, false);
            this._flexM5.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            this._flexM5.SetDummyColumn(new string[] { "CHK" });
            #endregion

            #region D5
            this._flexD5.BeginSetting(1, 1, false);
            this._flexD5.SetCol("NO_PO", "발주번호", 100, false);
            this._flexD5.SetCol("NM_PLANT", "공장", 150, false);
            this._flexD5.SetCol("CD_PARTNER", "거래처코드", 100, false);
            this._flexD5.SetCol("LN_PARTNER", "거래처", 120, false);
            this._flexD5.SetCol("DT_PO", "발주일자", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexD5.SetCol("NM_PURGRP", "구매그룹", 120, false);
            this._flexD5.SetCol("NM_KOR", "담당자", 120, false);
            this._flexD5.SetCol("NM_TPPO", "발주형태명", 120, false);
            this._flexD5.SetCol("NM_TRANS", "거래구분", 120, false);
            this._flexD5.SetCol("NM_PURCHASE", "매입형태", 120, false);
            this._flexD5.SetCol("FG_PAYMENT", "지급조건", 120, false);
            this._flexD5.SetCol("FG_TAX", "과세구분", 120, false);
            this._flexD5.SetCol("TP_UM_TAX", "부가세포함", 80, false);
            this._flexD5.SetCol("CD_EXCH", "환종", 40, false);
            this._flexD5.SetCol("FG_TAXP", "계산서처리방법", 120, false);
            this._flexD5.SetCol("YN_AM", "유무환", 50, false);
            this._flexD5.SetCol("DC_RMK", "발주비고1", 100);
            this._flexD5.SetCol("NM_CC", "C/C명", 100, false);
            this._flexD5.SetCol("NO_LINE", "발주항번", 60, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD5.SetCol("CD_ITEM", "품목코드", 100, 20, false);
            this._flexD5.SetCol("NM_ITEM", "품목명", 140, false);
            this._flexD5.SetCol("STND_ITEM", "규격", 120, false);
            this._flexD5.SetCol("UNIT_IM", "단위", 40, false);
            this._flexD5.SetCol("GRP_ITEM", "품목군코드", 80);
            this._flexD5.SetCol("NM_ITEMGRP", "품목군명", 120);
            this._flexD5.SetCol("GRP_MFG", "제품군코드", 80);
            this._flexD5.SetCol("NM_GRP_MFG", "제품군명", 120);
            this._flexD5.SetCol("STND_DETAIL_ITEM", "세부규격", 80);
            this._flexD5.SetCol("MAT_ITEM", "재질", 80);
            this._flexD5.SetCol("NM_PARTNER", "주거래처", 100);
            this._flexD5.SetCol("DT_LIMIT", "납기일", 70, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexD5.SetCol("QT_PO", "발주량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD5.SetCol("QT_REV_MM", "가입고수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD5.SetCol("QT_REQ", "의뢰량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD5.SetCol("QT_GR", "입고량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD5.SetCol("QT_CLS", "마감수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD5.SetCol("QT_RETURN", "반품수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD5.SetCol("QT_TR", "수입수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD5.SetCol("QT_REMAIN", "미입고량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD5.SetCol("AM_EX", "외화금액", 90, 17, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexD5.SetCol("AM", "원화금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD5.SetCol("VAT", "부가세", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD5.SetCol("NM_POST", "발주상태", 60, false);
            this._flexD5.SetCol("NO_PR", "요청번호", 100, false);
            this._flexD5.SetCol("NO_PRLINE", "요청항번", 60, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD5.SetCol("NO_APP", "품의번호", 100, false);
            this._flexD5.SetCol("NO_APPLINE", "품의항번", 60, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD5.SetCol("NM_PROJECT", "프로젝트", 120);
            this._flexD5.SetCol("CD_PJT", "프로젝트코드", 120);
            if (Config.MA_ENV.PJT형여부 == "Y")
            {
                this._flexD5.SetCol("CD_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 코드" : "프로젝트 품목코드", 140, false, typeof(string));
                this._flexD5.SetCol("NM_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 명" : "프로젝트 품목명", 140, false, typeof(string));
                this._flexD5.SetCol("SEQ_PROJECT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 항번" : "프로젝트항번", 120, false, typeof(decimal));
                this._flexD5.SetCol("PJT_ITEM_STND", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 규격" : "프로젝트 품목규격", 140, false, typeof(string));
                this._flexD5.SetCol("NO_PJT_DESIGN", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 도면번호" : "프로젝트 도면번호", 140, false, typeof(string));
            }
            this._flexD5.SetCol("DT_PLAN", "납품예정일", 70, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexD5.SetCol("NM_REQ_PURGRP", "요청구매그룹", 100);
            this._flexD5.SetCol("REQ_NM_KOR", "요청자", 100);
            this._flexD5.SetCol("REQ_NM_DEPT", "요청부서", 100);
            this._flexD5.SetCol("NM_FG_RCV", "수불형태", 140, false);
            this._flexD5.SetCol("YN_RETURN", "반품여부", 60, false);
            this._flexD5.SetCol("YN_IMPORT", "수입여부", 90, false);
            this._flexD5.SetCol("YN_ORDER", "자동승인여부", 90, false);
            this._flexD5.SetCol("DC1", "발주라인비고1", 200, false);
            this._flexD5.SetCol("DC2", "발주라인비고2", 200, false);
            this._flexD5.SetCol("DC3", "발주라인비고3", 200, false);
            this._flexD5.SetCol("NO_CONTRACT", "계약번호", false);
            this._flexD5.SetCol("NO_CTLINE", "계약항번", false);
            this._flexD5.SetCol("NO_MODEL", "모델코드", 100, false);
            this._flexD5.SetCol("FG_WEB", "확정여부(WEB)", false);
            this._flexD5.SetCol("DT_WEB", "확인일자(WEB)", false);
            this._flexD5.Cols["DT_WEB"].Format = "####/##/##";
            this._flexD5.SetStringFormatCol("DT_WEB");
            this._flexD5.SetCol("UNIT_PO", "발주단위", 60);
            this._flexD5.SetCol("NO_DESIGN", "품목도면번호", 100);
            this._flexD5.SetCol("DC_RMK2", "발주비고2", 100);
            this._flexD5.SetCol("NO_ORDER", "ORDER번호", 100);
            this._flexD5.SetCol("QT_PASS_MM", "합격수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD5.SetCol("QT_BAD_MM", "불량수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD5.SetCol("UM", "원화단가", 90, 17, false, typeof(decimal), (FormatTpType)2);
            this._flexD5.SetCol("AM_GR_REMAIN", "미입고금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD5.SettingVersion = "1.1.4";
            this._flexD5.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            this._flexD5.Cols["NO_LINE"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD5.Cols["NO_PRLINE"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD5.Cols["NO_APPLINE"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD5.Cols["YN_RETURN"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD5.Cols["YN_IMPORT"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD5.Cols["YN_ORDER"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD5.SetExceptSumCol(new string[] { "NO_LINE", "NO_PRLINE", "NO_APPLINE" });
            this._flexD5.AddMyMenu = true;
            this._flexD5.AddMenuSeperator();
            this._flexD5.AddMenuItem("key", "", new EventHandler(this.PopupItemSelectdEventHandler));
            this._flexD5.AddMenuItem("key1", "", new EventHandler(this.PopupItemSelectdEventHandler));
            this._flexD5.AddMenuItem("key2", "", new EventHandler(this.PopupItemSelectdEventHandler));
            this._flexD5.AddMenuItem("key3", "", new EventHandler(this.PopupItemSelectdEventHandler));
            #endregion

            #region M6
            this._flexM6.BeginSetting(1, 1, false);
            this._flexM6.SetCol("CHK", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flexM6.SetCol("CD_PURGRP", "프로젝트", 100, false);
            this._flexM6.SetCol("NM_PURGRP", "구매그룹", 120, false);
            this._flexM6.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            this._flexM6.SetDummyColumn(new string[] { "CHK" });
            #endregion

            #region D6
            this._flexD6.BeginSetting(1, 1, false);
            this._flexD6.SetCol("NO_PO", "발주번호", 100, false);
            this._flexD6.SetCol("NM_PLANT", "공장", 150, false);
            this._flexD6.SetCol("CD_PARTNER", "거래처코드", 100, false);
            this._flexD6.SetCol("LN_PARTNER", "거래처", 120, false);
            this._flexD6.SetCol("DT_PO", "발주일자", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexD6.SetCol("NM_PURGRP", "구매그룹", 120, false);
            this._flexD6.SetCol("NM_KOR", "담당자", 120, false);
            this._flexD6.SetCol("NM_TPPO", "발주형태명", 120, false);
            this._flexD6.SetCol("NM_TRANS", "거래구분", 120, false);
            this._flexD6.SetCol("NM_PURCHASE", "매입형태", 120, false);
            this._flexD6.SetCol("FG_PAYMENT", "지급조건", 120, false);
            this._flexD6.SetCol("FG_TAX", "과세구분", 120, false);
            this._flexD6.SetCol("TP_UM_TAX", "부가세포함", 80, false);
            this._flexD6.SetCol("CD_EXCH", "환종", 40, false);
            this._flexD6.SetCol("FG_TAXP", "계산서처리방법", 120, false);
            this._flexD6.SetCol("YN_AM", "유무환", 50, false);
            this._flexD6.SetCol("DC_RMK", "발주비고1", 100);
            this._flexD6.SetCol("NM_CC", "C/C명", 100, false);
            this._flexD6.SetCol("NO_LINE", "발주항번", 60, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD6.SetCol("CD_ITEM", "품목코드", 100, 20, false);
            this._flexD6.SetCol("NM_ITEM", "품목명", 140, false);
            this._flexD6.SetCol("STND_ITEM", "규격", 120, false);
            this._flexD6.SetCol("UNIT_IM", "단위", 40, false);
            this._flexD6.SetCol("GRP_ITEM", "품목군코드", 80);
            this._flexD6.SetCol("NM_ITEMGRP", "품목군명", 120);
            this._flexD6.SetCol("GRP_MFG", "제품군코드", 80);
            this._flexD6.SetCol("NM_GRP_MFG", "제품군명", 120);
            this._flexD6.SetCol("STND_DETAIL_ITEM", "세부규격", 80);
            this._flexD6.SetCol("MAT_ITEM", "재질", 80);
            this._flexD6.SetCol("NM_PARTNER", "주거래처", 100);
            this._flexD6.SetCol("DT_LIMIT", "납기일", 70, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexD6.SetCol("QT_PO", "발주량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD6.SetCol("QT_REV_MM", "가입고수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD6.SetCol("QT_REQ", "의뢰량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD6.SetCol("QT_GR", "입고량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD6.SetCol("QT_CLS", "마감수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD6.SetCol("QT_RETURN", "반품수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD6.SetCol("QT_TR", "수입수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD6.SetCol("QT_REMAIN", "미입고량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD6.SetCol("AM_EX", "외화금액", 90, 17, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexD6.SetCol("AM", "원화금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD6.SetCol("VAT", "부가세", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD6.SetCol("NM_POST", "발주상태", 60, false);
            this._flexD6.SetCol("NO_PR", "요청번호", 100, false);
            this._flexD6.SetCol("NO_PRLINE", "요청항번", 60, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD6.SetCol("NO_APP", "품의번호", 100, false);
            this._flexD6.SetCol("NO_APPLINE", "품의항번", 60, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD6.SetCol("NM_PROJECT", "프로젝트", 120);
            this._flexD6.SetCol("CD_PJT", "프로젝트코드", 120);
            if (Config.MA_ENV.PJT형여부 == "Y")
            {
                this._flexD6.SetCol("CD_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 코드" : "프로젝트 품목코드", 140, false, typeof(string));
                this._flexD6.SetCol("NM_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 명" : "프로젝트 품목명", 140, false, typeof(string));
                this._flexD6.SetCol("SEQ_PROJECT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 항번" : "프로젝트항번", 120, false, typeof(decimal));
                this._flexD6.SetCol("PJT_ITEM_STND", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 규격" : "프로젝트 품목규격", 140, false, typeof(string));
                this._flexD6.SetCol("NO_PJT_DESIGN", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 도면번호" : "프로젝트 도면번호", 140, false, typeof(string));
            }
            this._flexD6.SetCol("DT_PLAN", "납품예정일", 70, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexD6.SetCol("NM_REQ_PURGRP", "요청구매그룹", 100);
            this._flexD6.SetCol("REQ_NM_KOR", "요청자", 100);
            this._flexD6.SetCol("REQ_NM_DEPT", "요청부서", 100);
            this._flexD6.SetCol("NM_FG_RCV", "수불형태", 140, false);
            this._flexD6.SetCol("YN_RETURN", "반품여부", 60, false);
            this._flexD6.SetCol("YN_IMPORT", "수입여부", 90, false);
            this._flexD6.SetCol("YN_ORDER", "자동승인여부", 90, false);
            this._flexD6.SetCol("DC1", "발주라인비고1", 200, false);
            this._flexD6.SetCol("DC2", "발주라인비고2", 200, false);
            this._flexD6.SetCol("DC3", "발주라인비고3", 200, false);
            this._flexD6.SetCol("NO_CONTRACT", "계약번호", false);
            this._flexD6.SetCol("NO_CTLINE", "계약항번", false);
            this._flexD6.SetCol("NO_MODEL", "모델코드", 100, false);
            this._flexD6.SetCol("FG_WEB", "확정여부(WEB)", false);
            this._flexD6.SetCol("DT_WEB", "확인일자(WEB)", false);
            this._flexD6.Cols["DT_WEB"].Format = "####/##/##";
            this._flexD6.SetStringFormatCol("DT_WEB");
            this._flexD6.SetCol("UNIT_PO", "발주단위", 60);
            this._flexD6.SetCol("NO_DESIGN", "품목도면번호", 100);
            this._flexD6.SetCol("DC_RMK2", "발주비고2", 100);
            this._flexD6.SetCol("NO_ORDER", "ORDER번호", 100);
            this._flexD6.SetCol("QT_PASS_MM", "합격수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD6.SetCol("QT_BAD_MM", "불량수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD6.SettingVersion = "1.1.4";
            this._flexD6.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            this._flexD6.Cols["NO_LINE"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD6.Cols["NO_PRLINE"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD6.Cols["NO_APPLINE"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD6.Cols["YN_RETURN"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD6.Cols["YN_IMPORT"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD6.Cols["YN_ORDER"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD6.SetExceptSumCol(new string[] { "NO_LINE", "NO_PRLINE", "NO_APPLINE" });
            this._flexD6.AddMyMenu = true;
            this._flexD6.AddMenuSeperator();
            this._flexD6.AddMenuItem("key", "", new EventHandler(this.PopupItemSelectdEventHandler));
            this._flexD6.AddMenuItem("key1", "", new EventHandler(this.PopupItemSelectdEventHandler));
            this._flexD6.AddMenuItem("key2", "", new EventHandler(this.PopupItemSelectdEventHandler));
            this._flexD6.AddMenuItem("key3", "", new EventHandler(this.PopupItemSelectdEventHandler));
            #endregion

            #region M7
            this._flexM7.BeginSetting(1, 1, false);
            this._flexM7.SetCol("CHK", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flexM7.SetCol("CD_TPPO", "발주형태", 100, false);
            this._flexM7.SetCol("NM_TPPO", "발주형태명", 120, false);
            this._flexM7.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            this._flexM7.SetDummyColumn(new string[] { "CHK" });
            #endregion

            #region D7
            this._flexD7.BeginSetting(1, 1, false);
            this._flexD7.SetCol("NO_PO", "발주번호", 100, false);
            this._flexD7.SetCol("NM_PLANT", "공장", 150, false);
            this._flexD7.SetCol("CD_PARTNER", "거래처코드", 100, false);
            this._flexD7.SetCol("LN_PARTNER", "거래처", 120, false);
            this._flexD7.SetCol("DT_PO", "발주일자", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexD7.SetCol("NM_PURGRP", "구매그룹", 120, false);
            this._flexD7.SetCol("NM_KOR", "담당자", 120, false);
            this._flexD7.SetCol("NM_TPPO", "발주형태명", 120, false);
            this._flexD7.SetCol("NM_TRANS", "거래구분", 120, false);
            this._flexD7.SetCol("NM_PURCHASE", "매입형태", 120, false);
            this._flexD7.SetCol("FG_PAYMENT", "지급조건", 120, false);
            this._flexD7.SetCol("FG_TAX", "과세구분", 120, false);
            this._flexD7.SetCol("TP_UM_TAX", "부가세포함", 80, false);
            this._flexD7.SetCol("CD_EXCH", "환종", 40, false);
            this._flexD7.SetCol("FG_TAXP", "계산서처리방법", 120, false);
            this._flexD7.SetCol("YN_AM", "유무환", 50, false);
            this._flexD7.SetCol("DC_RMK", "발주비고1", 100);
            this._flexD7.SetCol("NM_CC", "C/C명", 100, false);
            this._flexD7.SetCol("NO_LINE", "발주항번", 60, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD7.SetCol("CD_ITEM", "품목코드", 100, 20, false);
            this._flexD7.SetCol("NM_ITEM", "품목명", 140, false);
            this._flexD7.SetCol("STND_ITEM", "규격", 120, false);
            this._flexD7.SetCol("UNIT_IM", "단위", 40, false);
            this._flexD7.SetCol("GRP_ITEM", "품목군코드", 80);
            this._flexD7.SetCol("NM_ITEMGRP", "품목군명", 120);
            this._flexD7.SetCol("GRP_MFG", "제품군코드", 80);
            this._flexD7.SetCol("NM_GRP_MFG", "제품군명", 120);
            this._flexD7.SetCol("STND_DETAIL_ITEM", "세부규격", 80);
            this._flexD7.SetCol("MAT_ITEM", "재질", 80);
            this._flexD7.SetCol("NM_PARTNER", "주거래처", 100);
            this._flexD7.SetCol("DT_LIMIT", "납기일", 70, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexD7.SetCol("QT_PO", "발주량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD7.SetCol("QT_REV_MM", "가입고수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD7.SetCol("QT_REQ", "의뢰량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD7.SetCol("QT_GR", "입고량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD7.SetCol("QT_CLS", "마감수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD7.SetCol("QT_RETURN", "반품수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD7.SetCol("QT_TR", "수입수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD7.SetCol("QT_REMAIN", "미입고량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD7.SetCol("AM_EX", "외화금액", 90, 17, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexD7.SetCol("AM", "원화금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD7.SetCol("VAT", "부가세", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD7.SetCol("NM_POST", "발주상태", 60, false);
            this._flexD7.SetCol("NO_PR", "요청번호", 100, false);
            this._flexD7.SetCol("NO_PRLINE", "요청항번", 60, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD7.SetCol("NO_APP", "품의번호", 100, false);
            this._flexD7.SetCol("NO_APPLINE", "품의항번", 60, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD7.SetCol("NM_PROJECT", "프로젝트", 120);
            this._flexD7.SetCol("CD_PJT", "프로젝트코드", 120);
            if (Config.MA_ENV.PJT형여부 == "Y")
            {
                this._flexD7.SetCol("CD_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 코드" : "프로젝트 품목코드", 140, false, typeof(string));
                this._flexD7.SetCol("NM_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 명" : "프로젝트 품목명", 140, false, typeof(string));
                this._flexD7.SetCol("SEQ_PROJECT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 항번" : "프로젝트항번", 120, false, typeof(decimal));
                this._flexD7.SetCol("PJT_ITEM_STND", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 규격" : "프로젝트 품목규격", 140, false, typeof(string));
                this._flexD7.SetCol("NO_PJT_DESIGN", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 도면번호" : "프로젝트 도면번호", 140, false, typeof(string));
            }
            this._flexD7.SetCol("DT_PLAN", "납품예정일", 70, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexD7.SetCol("NM_FG_RCV", "수불형태", 140, false);
            this._flexD7.SetCol("YN_RETURN", "반품여부", 60, false);
            this._flexD7.SetCol("YN_IMPORT", "수입여부", 90, false);
            this._flexD7.SetCol("YN_ORDER", "자동승인여부", 90, false);
            this._flexD7.SetCol("DC1", "발주라인비고1", 200, false);
            this._flexD7.SetCol("DC2", "발주라인비고2", 200, false);
            this._flexD7.SetCol("DC3", "발주라인비고3", 200, false);
            this._flexD7.SetCol("NO_CONTRACT", "계약번호", false);
            this._flexD7.SetCol("NO_CTLINE", "계약항번", false);
            this._flexD7.SetCol("NM_REQ_PURGRP", "요청구매그룹", 100);
            this._flexD7.SetCol("REQ_NM_KOR", "요청자", 100);
            this._flexD7.SetCol("REQ_NM_DEPT", "요청부서", 100);
            this._flexD7.SetCol("NO_MODEL", "모델코드", 100, false);
            this._flexD7.SetCol("FG_WEB", "확정여부(WEB)", false);
            this._flexD7.SetCol("DT_WEB", "확인일자(WEB)", false);
            this._flexD7.Cols["DT_WEB"].Format = "####/##/##";
            this._flexD7.SetStringFormatCol("DT_WEB");
            this._flexD7.SetCol("UNIT_PO", "발주단위", 60);
            this._flexD7.SetCol("NO_DESIGN", "품목도면번호", 100);
            this._flexD7.SetCol("DC_RMK2", "발주비고2", 100);
            this._flexD7.SetCol("NO_ORDER", "ORDER번호", 100);
            this._flexD7.SetCol("QT_PASS_MM", "합격수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD7.SetCol("QT_BAD_MM", "불량수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD7.SettingVersion = "1.1.4";
            this._flexD7.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            this._flexD7.Cols["NO_LINE"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD7.Cols["NO_PRLINE"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD7.Cols["NO_APPLINE"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD7.Cols["YN_RETURN"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD7.Cols["YN_IMPORT"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD7.Cols["YN_ORDER"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD7.SetExceptSumCol(new string[] { "NO_LINE", "NO_PRLINE", "NO_APPLINE" });
            this._flexD7.AddMyMenu = true;
            this._flexD7.AddMenuSeperator();
            this._flexD7.AddMenuItem("key", "", new EventHandler(this.PopupItemSelectdEventHandler));
            this._flexD7.AddMenuItem("key1", "", new EventHandler(this.PopupItemSelectdEventHandler));
            this._flexD7.AddMenuItem("key2", "", new EventHandler(this.PopupItemSelectdEventHandler));
            this._flexD7.AddMenuItem("key3", "", new EventHandler(this.PopupItemSelectdEventHandler));
            #endregion

            #region Sum
            this._flexSumItem.BeginSetting(1, 1, false);
            this._flexSumItem.SetCol("GRP_ITEM", "품목군", 80, false);
            this._flexSumItem.SetCol("NM_ITEMGRP", "품목군명", 100, false);
            this._flexSumItem.SetCol("GRP_MFG", "제품군", 80, false);
            this._flexSumItem.SetCol("NM_GRP_MFG", "제품군명", 100, false);
            this._flexSumItem.SetCol("CLS_S", "소분류", 80, false);
            this._flexSumItem.SetCol("NM_CLS_S", "소분류명", 100, false);
            this._flexSumItem.SetCol("CLS_M", "중분류", 80, false);
            this._flexSumItem.SetCol("NM_CLS_M", "중분류명", 100, false);
            this._flexSumItem.SetCol("CLS_L", "대분류", 80, false);
            this._flexSumItem.SetCol("NM_CLS_L", "대분류명", 100, false);
            this._flexSumItem.SetCol("CD_ITEM", "품목코드", 80, false);
            this._flexSumItem.SetCol("NM_ITEM", "품목명", 100, false);
            this._flexSumItem.SetCol("STND_ITEM", "규격", 100, false);
            this._flexSumItem.SetCol("UNIT_IM", "단위", 80, false);
            this._flexSumItem.SetCol("STND_DETAIL_ITEM", "세부규격", 80);
            this._flexSumItem.SetCol("MAT_ITEM", "재질", 80);
            this._flexSumItem.SetCol("LN_PARTNER", "주거래처", 100);
            this._flexSumItem.SetCol("NM_REQ_PURGRP", "요청구매그룹", 100);
            this._flexSumItem.SetCol("REQ_NM_KOR", "요청자", 100);
            this._flexSumItem.SetCol("REQ_NM_DEPT", "요청부서", 100);
            this._flexSumItem.SetCol("QT_PO", "발주수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexSumItem.SetCol("QT_REV_MM", "가입고수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexSumItem.SetCol("QT_REQ", "의뢰수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexSumItem.SetCol("QT_GR", "입고수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexSumItem.SetCol("QT_REMAN", "미납수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexSumItem.SetCol("QT_INV", "현재고수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexSumItem.SetCol("NM_CC", "C/C명", 100, false);
            this._flexSumItem.SetCol("NO_MODEL", "모델코드", 100, false);
            this._flexSumItem.SetCol("UNIT_PO", "발주단위", 60);
            this._flexSumItem.SetCol("NO_DESIGN", "품목도면번호", 100);
            this._flexSumItem.SetCol("QT_PASS_MM", "합격수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexSumItem.SetCol("QT_BAD_MM", "불량수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexSumItem.SettingVersion = "1.1.4";
            this._flexSumItem.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #region M8
            this._flexM8.BeginSetting(1, 1, false);
            this._flexM8.SetCol("CHK", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flexM8.SetCol("CD_CC", "C/C코드", 100, false);
            this._flexM8.SetCol("NM_CC", "C/C코드명", 120, false);
            this._flexM8.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            this._flexM8.SetDummyColumn(new string[] { "CHK" });
            #endregion

            #region D8
            this._flexD8.BeginSetting(1, 1, false);
            this._flexD8.SetCol("NO_PO", "발주번호", 100, false);
            this._flexD8.SetCol("NM_PLANT", "공장", 150, false);
            this._flexD8.SetCol("CD_PARTNER", "거래처코드", 100, false);
            this._flexD8.SetCol("LN_PARTNER", "거래처", 120, false);
            this._flexD8.SetCol("DT_PO", "발주일자", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexD8.SetCol("NM_PURGRP", "구매그룹", 120, false);
            this._flexD8.SetCol("NM_KOR", "담당자", 120, false);
            this._flexD8.SetCol("NM_TPPO", "발주형태명", 120, false);
            this._flexD8.SetCol("NM_TRANS", "거래구분", 120, false);
            this._flexD8.SetCol("NM_PURCHASE", "매입형태", 120, false);
            this._flexD8.SetCol("NM_PROJECT", "PROJECT", 120);
            this._flexD8.SetCol("FG_PAYMENT", "지급조건", 120, false);
            this._flexD8.SetCol("FG_TAX", "과세구분", 120, false);
            this._flexD8.SetCol("TP_UM_TAX", "부가세포함", 80, false);
            this._flexD8.SetCol("CD_EXCH", "환종", 40, false);
            this._flexD8.SetCol("FG_TAXP", "계산서처리방법", 120, false);
            this._flexD8.SetCol("YN_AM", "유무환", 50, false);
            this._flexD8.SetCol("DC_RMK", "발주비고1", 100);
            this._flexD8.SetCol("NO_LINE", "발주항번", 60, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD8.SetCol("CD_ITEM", "품목코드", 100, 20, false);
            this._flexD8.SetCol("NM_ITEM", "품목명", 140, false);
            this._flexD8.SetCol("STND_ITEM", "규격", 120, false);
            this._flexD8.SetCol("UNIT_IM", "단위", 40, false);
            this._flexD8.SetCol("GRP_ITEM", "품목군코드", 80);
            this._flexD8.SetCol("NM_ITEMGRP", "품목군명", 120);
            this._flexD8.SetCol("GRP_MFG", "제품군코드", 80);
            this._flexD8.SetCol("NM_GRP_MFG", "제품군명", 120);
            this._flexD8.SetCol("STND_DETAIL_ITEM", "세부규격", 80);
            this._flexD8.SetCol("MAT_ITEM", "재질", 80);
            this._flexD8.SetCol("NM_PARTNER", "주거래처", 100);
            this._flexD8.SetCol("DT_LIMIT", "납기일", 70, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexD8.SetCol("QT_PO", "발주량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD8.SetCol("QT_REV_MM", "가입고수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD8.SetCol("QT_REQ", "의뢰량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD8.SetCol("QT_GR", "입고량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD8.SetCol("QT_CLS", "마감수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD8.SetCol("QT_RETURN", "반품수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD8.SetCol("QT_TR", "수입수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD8.SetCol("QT_REMAIN", "미입고량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD8.SetCol("AM_EX", "외화금액", 90, 17, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexD8.SetCol("AM", "원화금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD8.SetCol("VAT", "부가세", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD8.SetCol("NM_POST", "발주상태", 60, false);
            this._flexD8.SetCol("NO_PR", "요청번호", 100, false);
            this._flexD8.SetCol("NO_PRLINE", "요청항번", 60, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD8.SetCol("NO_APP", "품의번호", 100, false);
            this._flexD8.SetCol("NO_APPLINE", "품의항번", 60, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD8.SetCol("NM_PROJECT", "프로젝트", 120);
            this._flexD8.SetCol("CD_PJT", "프로젝트코드", 120);
            if (Config.MA_ENV.PJT형여부 == "Y")
            {
                this._flexD8.SetCol("CD_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 코드" : "프로젝트 품목코드", 140, false, typeof(string));
                this._flexD8.SetCol("NM_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 명" : "프로젝트 품목명", 140, false, typeof(string));
                this._flexD8.SetCol("SEQ_PROJECT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 항번" : "프로젝트항번", 120, false, typeof(decimal));
                this._flexD8.SetCol("PJT_ITEM_STND", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 규격" : "프로젝트 품목규격", 140, false, typeof(string));
                this._flexD8.SetCol("NO_PJT_DESIGN", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 도면번호" : "프로젝트 도면번호", 140, false, typeof(string));
            }
            this._flexD8.SetCol("DT_PLAN", "납품예정일", 70, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexD8.SetCol("NM_REQ_PURGRP", "요청구매그룹", 100);
            this._flexD8.SetCol("REQ_NM_KOR", "요청자", 100);
            this._flexD8.SetCol("REQ_NM_DEPT", "요청부서", 100);
            this._flexD8.SetCol("NM_FG_RCV", "수불형태", 140, false);
            this._flexD8.SetCol("YN_RETURN", "반품여부", 60, false);
            this._flexD8.SetCol("YN_IMPORT", "수입여부", 90, false);
            this._flexD8.SetCol("YN_ORDER", "자동승인여부", 90, false);
            this._flexD8.SetCol("DC1", "발주라인비고1", 200, false);
            this._flexD8.SetCol("DC2", "발주라인비고2", 200, false);
            this._flexD8.SetCol("DC3", "발주라인비고3", 200, false);
            this._flexD8.SetCol("NO_CONTRACT", "계약번호", false);
            this._flexD8.SetCol("NO_CTLINE", "계약항번", false);
            this._flexD8.SetCol("NO_MODEL", "모델코드", 100, false);
            this._flexD8.SetCol("FG_WEB", "확정여부(WEB)", false);
            this._flexD8.SetCol("DT_WEB", "확인일자(WEB)", false);
            this._flexD8.SetCol("UNIT_PO", "발주단위", 60);
            this._flexD8.Cols["DT_WEB"].Format = "####/##/##";
            this._flexD8.SetStringFormatCol("DT_WEB");
            this._flexD8.SetCol("NO_DESIGN", "품목도면번호", 100);
            this._flexD8.SetCol("DC_RMK2", "발주비고2", 100);
            this._flexD8.SetCol("NO_ORDER", "ORDER번호", 100);
            this._flexD8.SetCol("QT_PASS_MM", "합격수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD8.SetCol("QT_BAD_MM", "불량수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD8.SettingVersion = "1.1.5";
            this._flexD8.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            this._flexD8.Cols["NO_LINE"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD8.Cols["NO_PRLINE"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD8.Cols["NO_APPLINE"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD8.Cols["YN_RETURN"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD8.Cols["YN_IMPORT"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD8.Cols["YN_ORDER"].TextAlign = TextAlignEnum.CenterCenter;
            this._flexD8.SetExceptSumCol(new string[] { "NO_LINE", "NO_PRLINE", "NO_APPLINE" });
            this._flexD8.AddMyMenu = true;
            this._flexD8.AddMenuSeperator();
            this._flexD8.AddMenuItem("key", "", new EventHandler(this.PopupItemSelectdEventHandler));
            this._flexD8.AddMenuItem("key1", "", new EventHandler(this.PopupItemSelectdEventHandler));
            this._flexD8.AddMenuItem("key2", "", new EventHandler(this.PopupItemSelectdEventHandler));
            this._flexD8.AddMenuItem("key3", "", new EventHandler(this.PopupItemSelectdEventHandler));
            #endregion
        }


        private void InitEvent() 
        {
            this._flexM1.AfterRowChange += _flexM_AfterRowChange;
            this._flexM1.HelpClick += new EventHandler(this._flex_HelpClick);
            this._flexD1.BeforeShowContextMenu += new EventHandler(this.PopupEventHandler);
            this._flexD1.HelpClick += new EventHandler(this._flex_HelpClick);
            this._flexM2.AfterRowChange += _flexM_AfterRowChange;
            this._flexD2.BeforeShowContextMenu += new EventHandler(this.PopupEventHandler);
            this._flexD2.HelpClick += new EventHandler(this._flex_HelpClick);
            this._flexM3.AfterRowChange += _flexM_AfterRowChange;
            this._flexD3.BeforeShowContextMenu += new EventHandler(this.PopupEventHandler);
            this._flexD3.HelpClick += new EventHandler(this._flex_HelpClick);
            this._flexM4.AfterRowChange += _flexM_AfterRowChange;
            this._flexD4.BeforeShowContextMenu += new EventHandler(this.PopupEventHandler);
            this._flexD4.HelpClick += new EventHandler(this._flex_HelpClick);
            this._flexM5.AfterRowChange += _flexM_AfterRowChange;
            this._flexD5.BeforeShowContextMenu += new EventHandler(this.PopupEventHandler);
            this._flexD5.HelpClick += new EventHandler(this._flex_HelpClick);
            this._flexM6.AfterRowChange += _flexM_AfterRowChange;
            this._flexD6.BeforeShowContextMenu += new EventHandler(this.PopupEventHandler);
            this._flexD6.HelpClick += new EventHandler(this._flex_HelpClick);
            this._flexM7.AfterRowChange += _flexM_AfterRowChange;
            this._flexD7.BeforeShowContextMenu += new EventHandler(this.PopupEventHandler);
            this._flexD7.HelpClick += new EventHandler(this._flex_HelpClick);
            this._flexM8.AfterRowChange += _flexM_AfterRowChange;
            this._flexD8.BeforeShowContextMenu += new EventHandler(this.PopupEventHandler);
            this._flexD8.HelpClick += new EventHandler(this._flex_HelpClick);

            this.cbo발주상태.QueryBefore += cbo발주상태_QueryBefore;

            this.cbo사업장.SelectionChangeCommitted += this.cbo사업장_SelectionChangeCommitted;
            this.cbo창고.QueryBefore += OnBpControl_QueryBefore;
            this.ctx품목.QueryBefore += OnBpControl_QueryBefore;
        }
        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch())
                return false;
            if (!this.사업장선택여부)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl사업장.Text });
                this.cbo사업장.Focus();
                return false;
            }
            if (!this.일자구분선택여부)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD("날짜타입") });
                this.cbo일자구분.Focus();
                return false;
            }
            if (!this.일자시작일등록여부)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD("날짜타입의 시작일") });
                this.dtp일자구분.Focus();
                return false;
            }
            if (!this.일자종료일등록여부)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD("날짜타입의 종료일") });
                this.dtp일자구분.Focus();
                return false;
            }
            if (this.단위구분선택여부)
                return true;
            this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl단위선택.Text });
            this.cbo단위선택.Focus();
            return false;
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!base.BeforeSearch())
                    return;
                string name = this.tablControl.SelectedTab.Name;
                MA.Auth.SetControlAuth(this.ctx요청구매그룹, "P_PU_PR_REG", AuthGroup.YN_PURGRP);
                DataTable dataTable = this._biz.search(new object[] { this.LoginInfo.CompanyCode,
                                                                      this.cbo사업장.SelectedValue.ToString(),
                                                                      this.cbo공장.SelectedValue.ToString(),
                                                                      this.cbo일자구분.SelectedValue.ToString(),
                                                                      this.dtp일자구분.StartDateToString,
                                                                      this.dtp일자구분.EndDateToString,
                                                                      this.cbo발주상태.SelectedValue == null ?  string.Empty :  this.cbo발주상태.QueryWhereIn_Pipe.ToString(),
                                                                      this.cbo거래구분.SelectedValue.ToString(),
                                                                      this.ctx구매그룹.QueryWhereIn_Pipe,
                                                                      this.ctx담당자.CodeValue,
                                                                      this.cbo단위선택.SelectedValue.ToString(),
                                                                      this.cbo처리구분.SelectedValue.ToString(),
                                                                      name,
                                                                      this.cbo창고.SelectedValue == null ?  string.Empty :  this.cbo창고.QueryWhereIn_Pipe.ToString(),
                                                                      this.ctxCC.CodeValue,
                                                                      this.ctx요청구매그룹.QueryWhereIn_Pipe,
                                                                      this.ctx요청부서.CodeValue.ToString(),
                                                                      this.ctx요청자.CodeValue.ToString(),
                                                                      this.txt발주번호.Text.ToUpper(),
                                                                      this.ctx거래처.CodeValue,
                                                                      this.ctx프로젝트.CodeValue,
                                                                      this.ctx품목.CodeValue,
                                                                      Global.SystemLanguage.MultiLanguageLpoint.ToString() });
                this.str사업장 = this.cbo사업장.SelectedValue.ToString();
                this.str공장명 = this.cbo공장.SelectedValue.ToString();
                this.str발주일_납기일 = this.cbo일자구분.SelectedValue.ToString();
                this.str발주일_납기일시작 = this.dtp일자구분.StartDateToString;
                this.str발주일_납기일끝 = this.dtp일자구분.EndDateToString;
                this.str발주상태 = this.cbo발주상태.QueryWhereIn_Pipe.ToString();
                this.str거래구분 = this.cbo거래구분.SelectedValue.ToString();
                this.str구매그룹 = this.ctx구매그룹.QueryWhereIn_Pipe;
                this.str담당자 = this.ctx담당자.CodeValue;
                this.str단위선택 = this.cbo단위선택.SelectedValue.ToString();
                this.str처리구분 = this.cbo처리구분.SelectedValue.ToString();
                this.cbo창고.QueryWhereIn_Pipe.ToString();
                this.strCD_CC = this.ctxCC.CodeValue;
                this.REQ_PURGRP = this.ctx요청구매그룹.QueryWhereIn_Pipe;
                this.REQ_DEPT = this.ctx요청부서.CodeValue.ToString();
                this.REQ_EMP = this.ctx요청자.CodeValue.ToString();
                switch (name)
                {
                    case "NO_PO":
                        this._flexM1.Binding = dataTable;
                        this.ToolBarPrintButtonEnabled = this._flexM1.HasNormalRow;
                        if (!this._flexM1.HasNormalRow)
                        {
                            this.ShowMessage(공통메세지.조건에해당하는내용이없습니다, new string[0]);
                            break;
                        }
                        break;
                    case "DT":
                        this._flexM2.Binding = dataTable;
                        this.ToolBarPrintButtonEnabled = this._flexM2.HasNormalRow;
                        if (!this._flexM2.HasNormalRow)
                        {
                            this.ShowMessage(공통메세지.조건에해당하는내용이없습니다, new string[0]);
                            break;
                        }
                        break;
                    case "CD_PARTNER":
                        this._flexM3.Binding = dataTable;
                        this.ToolBarPrintButtonEnabled = this._flexM3.HasNormalRow;
                        if (!this._flexM3.HasNormalRow)
                        {
                            this.ShowMessage(공통메세지.조건에해당하는내용이없습니다, new string[0]);
                            break;
                        }
                        break;
                    case "CD_ITEM":
                        this._flexM4.Binding = dataTable;
                        this.ToolBarPrintButtonEnabled = this._flexM4.HasNormalRow;
                        if (!this._flexM4.HasNormalRow)
                        {
                            this.ShowMessage(공통메세지.조건에해당하는내용이없습니다, new string[0]);
                            break;
                        }
                        break;
                    case "PROJECT":
                        this._flexM5.Binding = dataTable;
                        this.ToolBarPrintButtonEnabled = this._flexM5.HasNormalRow;
                        if (!this._flexM5.HasNormalRow)
                        {
                            this.ShowMessage(공통메세지.조건에해당하는내용이없습니다, new string[0]);
                            break;
                        }
                        break;
                    case "CD_PURGRP":
                        this._flexM6.Binding = dataTable;
                        this.ToolBarPrintButtonEnabled = this._flexM6.HasNormalRow;
                        if (!this._flexM6.HasNormalRow)
                        {
                            this.ShowMessage(공통메세지.조건에해당하는내용이없습니다, new string[0]);
                            break;
                        }
                        break;
                    case "TP_PU":
                        this._flexM7.Binding = dataTable;
                        this.ToolBarPrintButtonEnabled = this._flexM7.HasNormalRow;
                        if (!this._flexM7.HasNormalRow)
                        {
                            this.ShowMessage(공통메세지.조건에해당하는내용이없습니다, new string[0]);
                            break;
                        }
                        break;
                    case "SumItem":
                        this._flexSumItem.Binding = dataTable;
                        this.ToolBarPrintButtonEnabled = this._flexSumItem.HasNormalRow;
                        if (!this._flexSumItem.HasNormalRow)
                        {
                            this.ShowMessage(공통메세지.조건에해당하는내용이없습니다, new string[0]);
                            break;
                        }
                        break;
                    case "TP_CC":
                        this._flexM8.Binding = dataTable;
                        this.ToolBarPrintButtonEnabled = this._flexM8.HasNormalRow;
                        if (!this._flexM8.HasNormalRow)
                        {
                            this.ShowMessage(공통메세지.조건에해당하는내용이없습니다, new string[0]);
                            break;
                        }
                        break;
                }
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
                if (!this.BeforePrint())
                    return;
                FlexGrid flexGrid1 = null;
                FlexGrid flexGrid2 = null;
                string str1 = string.Empty;
                string str2 = string.Empty;
                string STATE = this.tablControl.SelectedIndex.ToString();
                if (STATE == "0")
                {
                    str1 = "R_PU_POST_RPT_0";
                    str2 = "발주진행현황-발주번호별";
                    flexGrid1 = this._flexM1;
                    flexGrid2 = this._flexD1;
                }
                else if (STATE == "1")
                {
                    str1 = "R_PU_POST_RPT_1";
                    str2 = "발주진행현황-일자별";
                    flexGrid1 = this._flexM2;
                    flexGrid2 = this._flexD2;
                }
                else if (STATE == "2")
                {
                    str1 = "R_PU_POST_RPT_2";
                    str2 = "발주진행현황-거래처별";
                    flexGrid1 = this._flexM3;
                    flexGrid2 = this._flexD3;
                }
                else if (STATE == "3")
                {
                    str1 = "R_PU_POST_RPT_3";
                    str2 = "발주진행현황-품목코드별";
                    flexGrid1 = this._flexM4;
                    flexGrid2 = this._flexD4;
                }
                else if (STATE == "4")
                {
                    str1 = "R_PU_POST_RPT_4";
                    str2 = "발주진행현황-프로젝트별";
                    flexGrid1 = this._flexM5;
                    flexGrid2 = this._flexD5;
                }
                else if (STATE == "5")
                {
                    str1 = "R_PU_POST_RPT_5";
                    str2 = "발주진행현황-구매그룹별";
                    flexGrid1 = this._flexM6;
                    flexGrid2 = this._flexD6;
                }
                else if (STATE == "6")
                {
                    str1 = "R_PU_POST_RPT_6";
                    str2 = "발주진행현황-발주형태별";
                    flexGrid1 = this._flexM7;
                    flexGrid2 = this._flexD7;
                }
                else
                {
                    if (STATE == "7")
                        return;
                    if (STATE == "8")
                    {
                        str1 = "R_PU_POST_RPT_8";
                        str2 = "발주진행현황-CC코드별";
                        flexGrid1 = this._flexM8;
                        flexGrid2 = this._flexD8;
                    }
                }
                string NO_KEY = "";
                if (!flexGrid1.HasNormalRow)
                    return;
                DataRow[] dataRowArray = flexGrid1.DataTable.Select("CHK = 'Y'");
                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다, new string[0]);
                }
                else
                {
                    switch (str2)
                    {
                        case "발주진행현황-발주번호별":
                            for (int index = this._flexM1.Rows.Fixed; index < this._flexM1.Rows.Count; ++index)
                            {
                                if (this._flexM1[index, "CHK"].ToString() == "Y")
                                    NO_KEY = NO_KEY + this._flexM1[index, "NO_PO"].ToString() + "|";
                            }
                            break;
                        case "발주진행현황-일자별":
                            for (int index = this._flexM2.Rows.Fixed; index < this._flexM2.Rows.Count; ++index)
                            {
                                if (this._flexM2[index, "CHK"].ToString() == "Y")
                                    NO_KEY = NO_KEY + this._flexM2[index, "DT_PO"].ToString() + "|";
                            }
                            break;
                        case "발주진행현황-거래처별":
                            for (int index = this._flexM3.Rows.Fixed; index < this._flexM3.Rows.Count; ++index)
                            {
                                if (this._flexM3[index, "CHK"].ToString() == "Y")
                                    NO_KEY = NO_KEY + this._flexM3[index, "CD_PARTNER"].ToString() + "|";
                            }
                            break;
                        case "발주진행현황-품목코드별":
                            for (int index = this._flexM4.Rows.Fixed; index < this._flexM4.Rows.Count; ++index)
                            {
                                if (this._flexM4[index, "CHK"].ToString() == "Y")
                                    NO_KEY = NO_KEY + this._flexM4[index, "CD_ITEM"].ToString() + "|";
                            }
                            break;
                        case "발주진행현황-프로젝트별":
                            for (int index = this._flexM5.Rows.Fixed; index < this._flexM5.Rows.Count; ++index)
                            {
                                if (this._flexM5[index, "CHK"].ToString() == "Y")
                                    NO_KEY = NO_KEY + this._flexM5[index, "CD_PJT"].ToString() + "|";
                            }
                            break;
                        case "발주진행현황-구매그룹별":
                            for (int index = this._flexM6.Rows.Fixed; index < this._flexM6.Rows.Count; ++index)
                            {
                                if (this._flexM6[index, "CHK"].ToString() == "Y")
                                    NO_KEY = NO_KEY + this._flexM6[index, "CD_PURGRP"].ToString() + "|";
                            }
                            break;
                        case "발주진행현황-발주형태별":
                            for (int index = this._flexM7.Rows.Fixed; index < this._flexM7.Rows.Count; ++index)
                            {
                                if (this._flexM7[index, "CHK"].ToString() == "Y")
                                    NO_KEY = NO_KEY + this._flexM7[index, "CD_TPPO"].ToString() + "|";
                            }
                            break;
                        case "발주진행현황-CC코드별":
                            for (int index = this._flexM8.Rows.Fixed; index < this._flexM8.Rows.Count; ++index)
                            {
                                if (this._flexM8[index, "CHK"].ToString() == "Y")
                                    NO_KEY = NO_KEY + this._flexM8[index, "CD_CC"].ToString() + "|";
                            }
                            break;
                    }
                    if (!flexGrid1.HasNormalRow && flexGrid2.HasNormalRow)
                        return;
                    ReportHelper reportHelper = new ReportHelper(str1, str2);
                    reportHelper.가로출력();
                    DataTable dataTable = this._biz.SearchPrint(this.dtp일자구분.StartDateToString, this.dtp일자구분.EndDateToString, this.cbo사업장.SelectedValue == null ? "" : this.cbo사업장.SelectedValue.ToString(), this.cbo공장.SelectedValue == null ? "" : this.cbo공장.SelectedValue.ToString(), this.cbo일자구분.SelectedValue == null ? "" : this.cbo일자구분.SelectedValue.ToString(), this.cbo발주상태.SelectedValue == null ? "" : this.cbo발주상태.QueryWhereIn_Pipe.ToString(), this.cbo거래구분.SelectedValue == null ? "" : this.cbo거래구분.SelectedValue.ToString(), this.ctx구매그룹.CodeValue, this.ctx담당자.CodeValue, this.cbo단위선택.SelectedValue == null ? "" : this.cbo단위선택.SelectedValue.ToString(), this.cbo처리구분.SelectedValue == null ? "" : this.cbo처리구분.SelectedValue.ToString(), STATE, NO_KEY, this.ctxCC.CodeValue, this.ctx요청구매그룹.CodeValue.ToString(), this.ctx요청부서.CodeValue.ToString(), this.ctx요청자.CodeValue.ToString());
                    reportHelper.SetData("사업장코드", this.cbo사업장.SelectedValue == null ? "" : this.cbo사업장.SelectedValue.ToString());
                    reportHelper.SetData("사업장명", this.cbo사업장.SelectedValue == null ? "" : this.cbo사업장.Text);
                    reportHelper.SetData("공장코드", this.cbo공장.SelectedValue == null ? "" : this.cbo공장.SelectedValue.ToString());
                    reportHelper.SetData("공장명", this.cbo공장.SelectedValue == null ? "" : this.cbo공장.Text);
                    reportHelper.SetData("기간구분", this.cbo일자구분.SelectedValue == null ? "" : this.cbo일자구분.Text);
                    reportHelper.SetData("수불기간FROM", this.dtp일자구분.StartDateToString.Substring(0, 4) + this.DD("년") + this.dtp일자구분.StartDateToString.Substring(4, 2) + this.DD("월") + this.dtp일자구분.StartDateToString.Substring(6, 2) + this.DD("일"));
                    reportHelper.SetData("수불기간TO", this.dtp일자구분.EndDateToString.Substring(0, 4) + this.DD("년") + this.dtp일자구분.EndDateToString.Substring(4, 2) + this.DD("월") + this.dtp일자구분.EndDateToString.Substring(6, 2) + this.DD("일"));
                    reportHelper.SetData("발주상태", this.cbo발주상태.SelectedValue == null ? "" : this.cbo발주상태.Text);
                    reportHelper.SetData("거래구분", this.cbo거래구분.SelectedValue == null ? "" : this.cbo거래구분.Text);
                    reportHelper.SetData("구매그룹", this.ctx구매그룹.CodeName);
                    reportHelper.SetData("담당자", this.ctx담당자.CodeName);
                    reportHelper.SetData("단위", this.cbo단위선택.SelectedValue == null ? "" : this.cbo단위선택.Text);
                    reportHelper.SetData("처리구분", this.cbo처리구분.SelectedValue == null ? "" : this.cbo처리구분.Text);
                    reportHelper.SetData("요청구매그룹", this.ctx요청구매그룹.CodeValue == null ? "" : this.ctx요청구매그룹.CodeValue);
                    reportHelper.SetData("요청부서", this.ctx요청부서.CodeValue == null ? "" : this.ctx요청부서.CodeValue);
                    reportHelper.SetData("요청자", this.ctx요청자.CodeValue == null ? "" : this.ctx요청자.CodeValue);
                    reportHelper.SetDataTable(dataTable);
                    reportHelper.Print();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeExit())
                    return false;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            Settings1.Default.bp_REEMP_SAVE = this.ctx요청자.CodeValue;
            Settings1.Default.bp_REEMP_SAVE1 = this.ctx요청자.CodeName;
            Settings1.Default.bp_REDEPT_SAVE = this.ctx요청부서.CodeValue;
            Settings1.Default.bp_REDEPT_SAVE1 = this.ctx요청부서.CodeName;
            Settings1.Default.Save();
            return true;
        }

        private void _flexM_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                FlexGrid _flex = sender as FlexGrid;
                if (sender == null)
                    return;
                this.ChangeFilter(ref _flex);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void ChangeFilter(ref FlexGrid _flex)
        {
            DataTable dataTable = new DataTable();
            string name = this.tablControl.SelectedTab.Name;
            string str1 = "";
            string str2 = "";
            object[] args = new object[] { this.LoginInfo.CompanyCode,
                                           this.str사업장,
                                           _flex.DataTable.Columns.Contains("CD_PLANT") ?  _flex["CD_PLANT"].ToString() :  this.str공장명,
                                           this.str발주일_납기일,
                                           this.str발주일_납기일시작,
                                           this.str발주일_납기일끝,
                                           this.cbo발주상태.SelectedValue == null ?  string.Empty :  this.cbo발주상태.QueryWhereIn_Pipe.ToString(),
                                           this.str거래구분,
                                           this.str구매그룹,
                                           this.str담당자,
                                           this.str단위선택,
                                           this.str처리구분,
                                           name,
                                           str2,
                                           this.cbo창고.SelectedValue == null ?  string.Empty :  this.cbo창고.QueryWhereIn_Pipe.ToString(),
                                           this.ctxCC.CodeValue,
                                           this.ctx요청구매그룹.CodeValue.ToString(),
                                           this.ctx요청부서.CodeValue.ToString(),
                                           this.ctx요청자.CodeValue.ToString(),
                                           this.txt발주번호.Text.ToUpper(),
                                           this.ctx거래처.CodeValue.ToString(),
                                           this.ctx프로젝트.CodeValue,
                                           this.ctx품목.CodeValue,
                                           Global.SystemLanguage.MultiLanguageLpoint.ToString() };
            switch (name)
            {
                case "NO_PO":
                    args[13] = _flex["NO_PO"].ToString().Trim();
                    str1 = "NO_PO = '" + _flex["NO_PO"].ToString().Trim() + "' ";
                    break;
                case "DT":
                    args[13] = _flex["DT_PO"].ToString().Trim();
                    str1 = "DT_PO = '" + _flex["DT_PO"].ToString().Trim() + "' ";
                    break;
                case "CD_PARTNER":
                    args[13] = _flex["CD_PARTNER"].ToString().Trim();
                    str1 = "CD_PARTNER = '" + _flex["CD_PARTNER"].ToString().Trim() + "' ";
                    break;
                case "CD_ITEM":
                    args[13] = _flex["CD_ITEM"].ToString().Trim();
                    str1 = "CD_ITEM = '" + _flex["CD_ITEM"].ToString() + "' ";
                    break;
                case "PROJECT":
                    args[13] = _flex["CD_PJT"].ToString().Trim();
                    str1 = !(args[13].ToString().Trim() == "") ? "CD_PJT = '" + _flex["CD_PJT"].ToString().Trim() + "' " : "CD_PJT ='' OR CD_PJT IS NULL";
                    break;
                case "CD_PURGRP":
                    args[13] = _flex["CD_PURGRP"].ToString().Trim();
                    str1 = "CD_PURGRP = '" + _flex["CD_PURGRP"].ToString().Trim() + "' ";
                    break;
                case "TP_PU":
                    args[13] = _flex["CD_TPPO"].ToString().Trim();
                    str1 = "CD_TPPO = '" + _flex["CD_TPPO"].ToString().Trim() + "' ";
                    break;
                case "TP_CC":
                    args[13] = _flex["CD_CC"].ToString().Trim();
                    str1 = !(args[13].ToString().Trim() == "") ? "CD_CC = '" + _flex["CD_CC"].ToString().Trim() + "' " : "CD_CC ='' OR CD_CC IS NULL";
                    break;
            }
            if (_flex.DetailQueryNeed)
            {
                DataTable table1 = this._biz.SearchDetail(args);
                DataTable table2 = new DataView(table1, "", "NO_LINE ASC", DataViewRowState.CurrentRows).ToTable();
                if (Global.MainFrame.ServerKeyCommon.Contains("SLFIRE"))
                {
                    foreach (DataRow row in table2.Rows)
                    {
                        if (row["FG_POST"].ToString().Trim().Equals("C"))
                            row["QT_REMAIN"] = 0;
                    }
                }
                table1.AcceptChanges();
                _flex.DetailGrids[0].BindingAdd(table2, str1);
            }
            else
                _flex.DetailGrids[0].BindingAdd(dataTable, str1);
            _flex.DetailQueryNeed = false;
        }

        private void _flex_HelpClick(object sender, EventArgs e)
        {
            try
            {
                FlexGrid flexGrid = (FlexGrid)sender;
                if (!flexGrid.HasNormalRow)
                    return;
                if (flexGrid.Cols[flexGrid.Col].Name == "NO_PR" && D.GetString(flexGrid["NO_PR"]) != "")
                {
                    object[] objArray = new object[] { D.GetString(flexGrid["NO_PR"]),
                                                       D.GetString(flexGrid["CD_PLANT"]),
                                                       0M,
                                                       "PMS" };
                    this.CallOtherPageMethod("P_PU_PR_REG", MA.PageName("P_PU_PR_REG"), this.Grant, objArray);
                }
                else if (flexGrid.Cols[flexGrid.Col].Name == "NO_APP" && D.GetString(flexGrid["NO_APP"]) != "")
                {
                    object[] objArray = new object[] { D.GetString(flexGrid["NO_APP"]),
                                                       0M,
                                                       "PU_APPL" };
                    this.CallOtherPageMethod("P_PU_APP_REG", MA.PageName("P_PU_APP_REG"), this.Grant, objArray);
                }
                else if (flexGrid.Cols[flexGrid.Col].Name == "NO_PO")
                {
                    object[] objArray = new object[] { D.GetString(flexGrid["NO_PO"]),
                                                       0M,
                                                       "발주번호" };
                    this.CallOtherPageMethod("P_CZ_PU_PO_WINTEC", MA.PageName("P_CZ_PU_PO_WINTEC"), this.Grant, objArray);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void cbo발주상태_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                e.HelpParam.P41_CD_FIELD1 = "PU_C000009";
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
                if (helpId != HelpID.P_MA_PITEM_SUB)
                {
                    if (helpId == HelpID.P_MA_SL_SUB1)
                    {
                        if (this.cbo공장.SelectedValue == null || this.cbo공장.SelectedValue.ToString() == string.Empty)
                        {
                            this.ShowMessage(공통메세지._자료가선택되어있지않습니다, new string[] { this.lbl공장명.Text });
                            this.cbo공장.Focus();
                            e.QueryCancel = true;
                        }
                        else
                            e.HelpParam.P09_CD_PLANT = this.cbo공장.SelectedValue.ToString();
                    }
                }
                else
                {
                    e.HelpParam.P09_CD_PLANT = this.cbo공장.SelectedValue.ToString();
                    e.HelpParam.P65_CODE5 = this.cbo공장.Text.Replace(" ", "").Remove(0, this.cbo공장.Text.Replace(" ", "").IndexOf(")", 0) + 1);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void cbo사업장_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.SetPlantCombo();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void PopupEventHandler(object sender, EventArgs e)
        {
            try
            {
                FlexGrid flexGrid = sender as FlexGrid;
                this.flex_Toolmenu = flexGrid;
                if (!flexGrid.HasNormalRow || flexGrid.Rows[flexGrid.Row].IsNode || flexGrid.Row < flexGrid.Rows.Fixed)
                    return;
                string str1 = this.DD("계약번호") + "(&C) : " + flexGrid["NO_CONTRACT"].ToString();
                string str2 = this.DD("요청번호") + "(&R) : " + flexGrid["NO_PR"].ToString();
                string str3 = this.DD("발주번호") + "(&P) : " + flexGrid["NO_PO"].ToString();
                string str4 = this.DD("품의번호") + "(&A) : " + flexGrid["NO_APP"].ToString();
                ToolStripItem toolStripItem1 = flexGrid.GridContextMenuStrip.Items["key"];
                ToolStripItem toolStripItem2 = flexGrid.GridContextMenuStrip.Items["key1"];
                ToolStripItem toolStripItem3 = flexGrid.GridContextMenuStrip.Items["key2"];
                ToolStripItem toolStripItem4 = flexGrid.GridContextMenuStrip.Items["key3"];
                toolStripItem1.Text = str1;
                toolStripItem1.Tag = 0;
                toolStripItem2.Text = str2;
                toolStripItem2.Tag = 1;
                toolStripItem3.Text = str3;
                toolStripItem3.Tag = 2;
                toolStripItem4.Text = str4;
                toolStripItem4.Tag = 3;
                toolStripItem1.Enabled = !(flexGrid["NO_CONTRACT"].ToString() == "");
                toolStripItem2.Enabled = !(flexGrid["NO_PR"].ToString() == "");
                toolStripItem3.Enabled = !(flexGrid["NO_PO"].ToString() == "");
                toolStripItem4.Enabled = !(flexGrid["NO_APP"].ToString() == "");
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void PopupItemSelectdEventHandler(object sender, EventArgs e)
        {
            try
            {
                FlexGrid flexToolmenu = this.flex_Toolmenu;
                object tag = ((ToolStripItem)sender).Tag;
                if (tag.ToString() == "0")
                {
                    object[] objArray = new object[1]
                    {
             flexToolmenu["NO_CONTRACT"].ToString()
                    };
                    if (this.MainFrameInterface.IsExistPage("P_PU_CT_REG", false))
                        this.MainFrameInterface.UnLoadPage("P_PU_CT_REG", false);
                    this.MainFrameInterface.LoadPageFrom("P_PU_CT_REG", MA.PageName("P_PU_CT_REG"), this.Grant, objArray);
                }
                else if (tag.ToString() == "1")
                {
                    object[] objArray = new object[] { flexToolmenu["NO_PR"].ToString(), flexToolmenu["CD_PLANT"].ToString(), nameof(P_CZ_PU_POST_RPT_WINTEC) };
                    if (this.MainFrameInterface.IsExistPage("P_PU_PR_REG", false))
                        this.MainFrameInterface.UnLoadPage("P_PU_PR_REG", false);
                    this.MainFrameInterface.LoadPageFrom("P_PU_PR_REG", MA.PageName("P_PU_PR_REG"), this.Grant, objArray);
                }
                else if (tag.ToString() == "2")
                {
                    object[] objArray = new object[] { flexToolmenu["NO_PO"].ToString() };
                    if (flexToolmenu["CD_PLANT"].ToString() != "")
                    {
                        if (this.MainFrameInterface.IsExistPage("P_PU_PO_REG2", false))
                            this.MainFrameInterface.UnLoadPage("P_PU_PO_REG2", false);
                        this.MainFrameInterface.LoadPageFrom("P_PU_PO_REG2", MA.PageName("P_PU_PO_REG2"), this.Grant, objArray);
                    }
                    else
                    {
                        if (this.MainFrameInterface.IsExistPage("P_PU_PO_REG", false))
                            this.MainFrameInterface.UnLoadPage("P_PU_PO_REG", false);
                        this.MainFrameInterface.LoadPageFrom("P_PU_PO_REG", MA.PageName("P_PU_PO_REG"), this.Grant, objArray);
                    }
                }
                else
                {
                    if (!(tag.ToString() == "3"))
                        return;
                    object[] objArray = new object[] { D.GetString(flexToolmenu["NO_APP"]), 0M, "PU_APPL" };
                    this.CallOtherPageMethod("P_PU_APP_REG", MA.PageName("P_PU_APP_REG"), this.Grant, objArray);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public bool 사업장선택여부 => this.cbo사업장.SelectedValue != null && !(this.cbo사업장.SelectedValue.ToString() == "");

        public bool 일자구분선택여부 => this.cbo일자구분.SelectedValue != null && !(this.cbo일자구분.SelectedValue.ToString() == "");

        public bool 일자시작일등록여부 => !(this.dtp일자구분.StartDateToString == "");

        public bool 일자종료일등록여부 => !(this.dtp일자구분.EndDateToString == "");

        public bool 단위구분선택여부 => this.cbo단위선택.SelectedValue != null && !(this.cbo단위선택.SelectedValue.ToString() == "");
    }
}

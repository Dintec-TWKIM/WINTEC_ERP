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
using Dintec;
using Duzon.ERPU;
using Duzon.Windows.Print;
using Duzon.ERPU.Grant;
using Duzon.ERPU.MF;

namespace cz
{
    public partial class P_CZ_PU_STS_BATCH_REG : PageBase
    {
        P_CZ_PU_STS_BATCH_REG_BIZ _biz = new P_CZ_PU_STS_BATCH_REG_BIZ();

        public P_CZ_PU_STS_BATCH_REG()
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
            this.MainGrids = new FlexGrid[] { this._flex출고의뢰H, this._flex출고의뢰L };
            this._flex출고의뢰H.DetailGrids = new FlexGrid[] { this._flex출고의뢰L };

            #region 출고의뢰

            #region Header
            this._flex출고의뢰H.BeginSetting(1, 1, false);

            this._flex출고의뢰H.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flex출고의뢰H.SetCol("TP_STATE", "출고상태", 100);
            this._flex출고의뢰H.SetCol("YN_DELETE", "삭제된협조전", 100, false, CheckTypeEnum.Y_N);
            this._flex출고의뢰H.SetCol("CNT_PRINT", "출력횟수", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex출고의뢰H.SetCol("NM_COMPANY", "회사", 100);
            this._flex출고의뢰H.SetCol("NO_GIREQ", "요청번호", 100);
            this._flex출고의뢰H.SetCol("NO_GIR", "협조전번호", 100);
            this._flex출고의뢰H.SetCol("NM_SL", "출고창고", 100);
            this._flex출고의뢰H.SetCol("NM_GRSL", "입고창고", 100);
            this._flex출고의뢰H.SetCol("NM_STA_GIR", "진행상태", 80);
            this._flex출고의뢰H.SetCol("NM_PARTNER", "매출처", 100);
            this._flex출고의뢰H.SetCol("NM_DELIVERY_TO", "납품처", 100);
            this._flex출고의뢰H.SetCol("NM_VESSEL", "호선", 100);
            this._flex출고의뢰H.SetCol("NM_ITEMGRP", "품목군", 100);
            this._flex출고의뢰H.SetCol("NM_TP_GIR", "대분류", 100);
            this._flex출고의뢰H.SetCol("NM_MAIN_CATEGORY", "중분류", 100);
            this._flex출고의뢰H.SetCol("NM_SUB_CATEGORY", "소분류", 100);
            this._flex출고의뢰H.SetCol("DT_GIR", "의뢰일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex출고의뢰H.SetCol("DT_GIREQ", "요청일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex출고의뢰H.SetCol("DT_START", "포장예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex출고의뢰H.SetCol("DT_COMPLETE", "출고예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex출고의뢰H.SetCol("NM_EMP_GIREQ", "요청자", 60);
            this._flex출고의뢰H.SetCol("NM_PIRNT", "최종출력자", 60);
            this._flex출고의뢰H.SetCol("CNT_GIR", "품목종수", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex출고의뢰H.SetCol("DC50_PO", "비고", 100, true);

            this._flex출고의뢰H.SetDummyColumn("S");

            this._flex출고의뢰H.SettingVersion = "0.0.0.2";
            this._flex출고의뢰H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #region Line
            this._flex출고의뢰L.BeginSetting(1, 1, false);

            this._flex출고의뢰L.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flex출고의뢰L.SetCol("NO_DSP", "순번", 40);
            this._flex출고의뢰L.SetCol("NO_LINE", "항번", 40);
            this._flex출고의뢰L.SetCol("NO_SO", "수주번호", 80);
            this._flex출고의뢰L.SetCol("CD_ITEM", "재고코드", 80);
            this._flex출고의뢰L.SetCol("NM_ITEM", "재고명", 100);
            this._flex출고의뢰L.SetCol("NM_CLS_ITEM", "계정구분", 100);
            this._flex출고의뢰L.SetCol("NM_ITEMGRP", "품목군", 100);
            this._flex출고의뢰L.SetCol("STND_DETAIL_ITEM", "U코드", 100);
            this._flex출고의뢰L.SetCol("STND_ITEM", "파트번호", 100);
            this._flex출고의뢰L.SetCol("NM_SUBJECT", "주제", 100);
            this._flex출고의뢰L.SetCol("CD_ITEM_PARTNER", "매출처품번", 100);
            this._flex출고의뢰L.SetCol("NM_ITEM_PARTNER", "매출처품명", 100);
            this._flex출고의뢰L.SetCol("NM_SL", "출고창고", 80);
            this._flex출고의뢰L.SetCol("CD_ZONE", "저장위치", 80);
            this._flex출고의뢰L.SetCol("NM_GRSL", "입고창고", 80);
            this._flex출고의뢰L.SetCol("STAND_PRC", "재고평균단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex출고의뢰L.SetCol("QT_GIREQ", "요청수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex출고의뢰L.SetCol("QT_GI", "출고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex출고의뢰L.SetCol("QT_INV", "현재고", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex출고의뢰L.SetCol("QT_REMAIN", "이동후량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex출고의뢰L.SetCol("NO_IO", "출고번호", 100);
            this._flex출고의뢰L.SetCol("DT_IO", "출고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex출고의뢰L.SetCol("NM_EMP_IO", "출고자", 60);
            this._flex출고의뢰L.SetCol("DC_RMK", "출고비고", 500);

            this._flex출고의뢰L.SetDummyColumn("S");
            this._flex출고의뢰L.ExtendLastCol = true;

            this._flex출고의뢰L.SettingVersion = "0.0.0.2";
            this._flex출고의뢰L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #endregion

            #region 품목
            this._flex의뢰품목.BeginSetting(1, 1, false);

            this._flex의뢰품목.SetCol("TP_STATE", "출고상태", 100);
            this._flex의뢰품목.SetCol("YN_DELETE", "삭제된협조전", 100, false, CheckTypeEnum.Y_N);
            this._flex의뢰품목.SetCol("CNT_PRINT", "출력횟수", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex의뢰품목.SetCol("NM_COMPANY", "회사", 100);
            this._flex의뢰품목.SetCol("NO_GIREQ", "요청번호", 100);
            this._flex의뢰품목.SetCol("NO_GIR", "협조전번호", 100);
            this._flex의뢰품목.SetCol("NM_SL", "출고창고", 100);
            this._flex의뢰품목.SetCol("NM_GRSL", "입고창고", 100);
            this._flex의뢰품목.SetCol("NM_STA_GIR", "진행상태", 80);
            this._flex의뢰품목.SetCol("NM_PARTNER", "매출처", 100);
            this._flex의뢰품목.SetCol("NM_DELIVERY_TO", "납품처", 100);
            this._flex의뢰품목.SetCol("NM_VESSEL", "호선", 100);
            this._flex의뢰품목.SetCol("NM_TP_GIR", "대분류", 100);
            this._flex의뢰품목.SetCol("NM_MAIN_CATEGORY", "중분류", 100);
            this._flex의뢰품목.SetCol("NM_SUB_CATEGORY", "소분류", 100);
            this._flex의뢰품목.SetCol("DT_GIR", "의뢰일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex의뢰품목.SetCol("DT_START", "작업시작일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex의뢰품목.SetCol("DT_GIREQ", "요청일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex의뢰품목.SetCol("NM_EMP_GIREQ", "요청자", 60);
            this._flex의뢰품목.SetCol("NM_PIRNT", "최종출력자", 60);
            this._flex의뢰품목.SetCol("DC50_PO", "비고", 100, true);

            this._flex의뢰품목.SetCol("NO_DSP", "순번", 40);
            this._flex의뢰품목.SetCol("NO_LINE", "항번", 40);
            this._flex의뢰품목.SetCol("NO_SO", "수주번호", 80);
            this._flex의뢰품목.SetCol("CD_ITEM", "재고코드", 80);
            this._flex의뢰품목.SetCol("NM_ITEM", "재고명", 100);
            this._flex의뢰품목.SetCol("NM_CLS_ITEM", "계정구분", 100);
            this._flex의뢰품목.SetCol("NM_ITEMGRP", "품목군", 100);
            this._flex의뢰품목.SetCol("STND_DETAIL_ITEM", "U코드", 100);
            this._flex의뢰품목.SetCol("STND_ITEM", "파트번호", 100);
            this._flex의뢰품목.SetCol("NM_SUBJECT", "주제", 100);
            this._flex의뢰품목.SetCol("CD_ITEM_PARTNER", "매출처품번", 100);
            this._flex의뢰품목.SetCol("NM_ITEM_PARTNER", "매출처품명", 100);
            this._flex의뢰품목.SetCol("CD_ZONE", "저장위치", 80);
            this._flex의뢰품목.SetCol("STAND_PRC", "재고평균단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex의뢰품목.SetCol("QT_GIREQ", "요청수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex의뢰품목.SetCol("QT_GI", "출고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex의뢰품목.SetCol("QT_INV", "현재고", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex의뢰품목.SetCol("QT_REMAIN", "이동후량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex의뢰품목.SetCol("NO_IO", "출고번호", 100);
            this._flex의뢰품목.SetCol("DT_IO", "출고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex의뢰품목.SetCol("NM_EMP_IO", "출고자", 60);
            this._flex의뢰품목.SetCol("DC_RMK", "출고비고", 500);

            this._flex의뢰품목.SettingVersion = "0.0.0.1";
            this._flex의뢰품목.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion
        }

        private void InitEvent()
        {
            this.btn재고출고.Click += new EventHandler(this.btn재고출고_Click);
            this.btn출고취소.Click += new EventHandler(this.btn출고취소_Click);
			this.btn출고취소품목.Click += Btn출고취소품목_Click;
			this.btn요청삭제품목.Click += Btn요청삭제품목_Click;
			
            this._flex출고의뢰H.AfterEdit += new RowColEventHandler(this._flexH_AfterEdit);
            this._flex출고의뢰H.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
            this._flex출고의뢰H.CheckHeaderClick += new EventHandler(this._flexH_CheckHeaderClick);
        }

		protected override void InitPaint()
        {
            try
            {
                base.InitPaint();

                this.cbo일자구분.DataSource = MA.GetCodeUser(new string[] { "001", "002", "003" }, new string[] { "요청일자", "포장예정일", "출고일자" }, false);
                this.cbo일자구분.ValueMember = "CODE";
                this.cbo일자구분.DisplayMember = "NAME";

                this.dtp일자.StartDate = this.MainFrameInterface.GetDateTimeToday().AddMonths(-1);
                this.dtp일자.EndDate = this.MainFrameInterface.GetDateTimeToday();

                this.cbo처리상태.DataSource = Global.MainFrame.GetComboDataCombine("S;FI_F000011");
                this.cbo처리상태.ValueMember = "CODE";
                this.cbo처리상태.DisplayMember = "NAME";

                this.cbo계정구분.DataSource = Global.MainFrame.GetComboDataCombine("S;MA_B000010");
                this.cbo계정구분.ValueMember = "CODE";
                this.cbo계정구분.DisplayMember = "NAME";

                if (Global.MainFrame.LoginInfo.CompanyCode == "K100")
                {
                    this.bpc회사.AddItem("K100", "(주)딘텍");
                    this.bpc회사.AddItem("K200", "(주)두베코");
                }
                else
                {
                    this.bpc회사.AddItem(Global.MainFrame.LoginInfo.CompanyCode, Global.MainFrame.LoginInfo.CompanyName);
                }

                this.bpc회사.SelectedValue = Global.MainFrame.LoginInfo.CompanyCode;

                UGrant ugrant = new UGrant();
                ugrant.GrantButtonEnble(Global.MainFrame.CurrentPageID, "CONFIRM", this.btn재고출고, true);
                ugrant.GrantButtonEnble(Global.MainFrame.CurrentPageID, "CANCEL", this.btn출고취소, true);
                ugrant.GrantButtonEnble(Global.MainFrame.CurrentPageID, "CANCEL", this.btn출고취소품목, true);

                if (this.Grant.CanDelete)
                    this.btn요청삭제품목.Enabled = true;
                else
                    this.btn요청삭제품목.Enabled = false;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!this.BeforeSearch()) return;

                if (this.tabControlExt1.SelectedIndex == 0)
                {
                    this._flex출고의뢰H.Binding = this._biz.Search(new object[] { this.bpc회사.QueryWhereIn_Pipe,
                                                                                  this.txt요청번호.Text,
                                                                                  this.cbo일자구분.SelectedValue,
                                                                                  this.dtp일자.StartDateToString,
                                                                                  this.dtp일자.EndDateToString,
                                                                                  this.txt협조전번호.Text,
                                                                                  this.txt수주번호.Text,
                                                                                  this.txt재고코드.Text,
                                                                                  this.cbo처리상태.SelectedValue,
                                                                                  this.cbo계정구분.SelectedValue,
                                                                                  this.bpc품목군.QueryWhereIn_Pipe,
                                                                                  (this.chk재고출고의뢰건.Checked == true ? "Y" : "N"),
                                                                                  this.cur재고단가.DecimalValue });

                    if (!this._flex출고의뢰H.HasNormalRow)
                        this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
                else
                {
                    this._flex의뢰품목.Binding = this._biz.SearchDetail1(new object[] { this.bpc회사.QueryWhereIn_Pipe,
                                                                                        this.txt요청번호.Text,
                                                                                        this.cbo일자구분.SelectedValue,
                                                                                        this.dtp일자.StartDateToString,
                                                                                        this.dtp일자.EndDateToString,
                                                                                        this.txt협조전번호.Text,
                                                                                        this.txt수주번호.Text,
                                                                                        this.txt재고코드.Text,
                                                                                        this.cbo처리상태.SelectedValue,
                                                                                        this.cbo계정구분.SelectedValue,
                                                                                        this.bpc품목군.QueryWhereIn_Pipe,
                                                                                        (this.chk재고출고의뢰건.Checked == true ? "Y" : "N"),
                                                                                        this.cur재고단가.DecimalValue });

                    if (!this._flex의뢰품목.HasNormalRow)
                        this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            ReportHelper reportHelper;
            DataTable dtH, dtL, dtTemp;
            DataRow[] dataRowArray, dataRowArray1;
			string nmSubject;

            try
            {
                base.OnToolBarPrintButtonClicked(sender, e);

                if (this._flex출고의뢰H.HasNormalRow == false) return;

                dataRowArray = this._flex출고의뢰H.DataTable.Select("S = 'Y'", "NO_SORT ASC");
                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    if (this._flex출고의뢰H.DataTable.Select("S = 'Y' AND ISNULL(NO_GIR, '') = ''").Length > 0)
                    {
                        this.ShowMessage("재고출고의뢰가 아닌 건이 선택 되어 있습니다.");
                        return;
                    }

                    if (this._flex출고의뢰H.DataTable.Select("S = 'Y' AND QT_GI > 0").Length > 0)
                    {
                        this.ShowMessage("이미 재고 출고 된 건이 선택 되어 있습니다.");
                        return;
                    }

                    dtH = this._flex출고의뢰H.DataTable.Clone();
                    dtL = this._flex출고의뢰L.DataTable.Clone();

                    dtL.Columns.Add("NM_VESSEL");
                    dtL.Columns.Add("NM_PARTNER");
                    dtL.Columns.Add("NM_MAIN_CATEGORY");
                    dtL.Columns.Add("NM_SUB_CATEGORY");
                    dtL.Columns.Add("NO_GIR");
                    dtL.Columns.Add("DT_GIR");
                    dtL.Columns.Add("NM_DELIVERY_TO");
                    dtL.Columns.Add("DT_START");
                    dtL.Columns.Add("DT_COMPLETE");
                    dtL.Columns.Add("NM_EMP_GIREQ");

                    foreach (DataRow dr in dataRowArray)
                    {
                        dtH.ImportRow(dr);

						dataRowArray1 = this._flex출고의뢰L.DataTable.Select("CD_COMPANY = '" + D.GetString(dr["CD_COMPANY"]) + "' AND NO_GIREQ = '" + D.GetString(dr["NO_GIREQ"]) + "'");

						dtTemp = ComFunc.getGridGroupBy(dataRowArray1, new string[] { "LN_PARTNER" }, true);
						nmSubject = string.Empty;

						foreach (DataRow dr1 in dtTemp.Rows)
						{
							nmSubject += dr1["LN_PARTNER"].ToString() + ", ";
						}

						foreach (DataRow dr1 in dataRowArray1)
                        {
							dtL.ImportRow(dr1);

							dtL.Rows[dtL.Rows.Count - 1]["NM_SUBJECT"] = nmSubject;
							dtL.Rows[dtL.Rows.Count - 1]["NM_VESSEL"] = dr["NM_VESSEL"];
                            dtL.Rows[dtL.Rows.Count - 1]["NM_PARTNER"] = dr["NM_PARTNER"];
                            dtL.Rows[dtL.Rows.Count - 1]["NM_MAIN_CATEGORY"] = dr["NM_MAIN_CATEGORY"];
                            dtL.Rows[dtL.Rows.Count - 1]["NM_SUB_CATEGORY"] = dr["NM_SUB_CATEGORY"];
                            dtL.Rows[dtL.Rows.Count - 1]["NO_GIR"] = dr["NO_GIR"];
                            dtL.Rows[dtL.Rows.Count - 1]["DT_GIR"] = dr["DT_GIR"];
                            dtL.Rows[dtL.Rows.Count - 1]["NM_DELIVERY_TO"] = dr["NM_DELIVERY_TO"];
                            dtL.Rows[dtL.Rows.Count - 1]["DT_START"] = dr["DT_START"];
                            dtL.Rows[dtL.Rows.Count - 1]["DT_COMPLETE"] = dr["DT_COMPLETE"];
                            dtL.Rows[dtL.Rows.Count - 1]["NM_EMP_GIREQ"] = dr["NM_EMP_GIREQ"];
                        }
                    }

                    dtL.DefaultView.Sort = "CD_COMPANY ASC, NO_GIREQ ASC, NO_SO ASC, CD_ITEM ASC";
                    dtL = dtL.DefaultView.ToTable();

                    reportHelper = Util.SetReportHelper(Util.GetReportFileName("R_CZ_PU_STS_BATCH_REG", this.LoginInfo.CompanyCode), "재고출고등록", this.LoginInfo.CompanyCode);
                    reportHelper.SetDataTable(dtH, 1);
                    reportHelper.SetDataTable(dtL, 2);

                    Util.RPT_Print(reportHelper);

                    this._biz.SavePrintLog(dtH.DefaultView.ToTable(true, "CD_COMPANY", "NO_GIREQ"));
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarDeleteButtonClicked(sender, e);

                if (!this.BeforeDelete() || !this._flex출고의뢰H.HasNormalRow) return;

                if (this._flex출고의뢰H.DataTable.Select("S = 'Y' AND ISNULL(NO_GIR, '') = ''").Length > 0)
                {
                    this.ShowMessage("재고출고의뢰가 아닌 건이 선택 되어 있습니다.");
                    return;
                }

                if (this._flex출고의뢰H.DataTable.Select("S = 'Y' AND QT_GI > 0").Length > 0)
                {
                    this.ShowMessage("재고 출고된 건이 선택되어 있습니다.");
                    return;
                }

                if (!Util.CheckPW()) return;

                foreach (DataRow dr in this._flex출고의뢰H.DataTable.Select("S = 'Y'"))
                    dr.Delete();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSaveButtonClicked(sender, e);

                if (!this.BeforeSave()) return;

                if (MsgAndSave(PageActionMode.Save))
                {
                    this.ShowMessage(PageResultMode.SaveGood);
                    this.OnToolBarSearchButtonClicked(null, null);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            if (!base.SaveData() || !base.Verify()) return false;
            if (this._flex출고의뢰H.IsDataChanged == false) return false;

            if (!this._biz.Save(this._flex출고의뢰H.GetChanges())) return false;
                
            this._flex출고의뢰H.AcceptChanges();

            return true;
        }

        private void _flexH_AfterEdit(object sender, RowColEventArgs e)
        {
            FlexGrid flexGrid;

            try
            {
                flexGrid = ((FlexGrid)sender);

                if (flexGrid.HasNormalRow == false) return;
                if (flexGrid.Cols[e.Col].Name != "DC50_PO") return;

                DBHelper.ExecuteScalar(@"UPDATE MM_GIREQH
                                         SET DC50_PO = '" + flexGrid["DC50_PO"].ToString() + "'" + Environment.NewLine +
                                        "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +  Environment.NewLine +
                                        "AND NO_GIREQ = '" + flexGrid["NO_GIREQ"].ToString() + "'");

                this._flex출고의뢰H.AcceptChanges();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt = null;
            string key, key1, filter;

            try
            {
                key = D.GetString(this._flex출고의뢰H["CD_COMPANY"]);
                key1 = D.GetString(this._flex출고의뢰H["NO_GIREQ"]);

                filter = "CD_COMPANY ='" + key + "' AND NO_GIREQ = '" + key1 + "'";

                if (this._flex출고의뢰H.DetailQueryNeed)
                {
                    dt = this._biz.SearchDetail(new object[] { key,
                                                               key1,
                                                               this.txt수주번호.Text,
                                                               this.txt재고코드.Text,
                                                               this.cbo계정구분.SelectedValue,
                                                               this.bpc품목군.QueryWhereIn_Pipe,
                                                               this.cur재고단가.DecimalValue,
                                                               this.cbo일자구분.SelectedValue,
                                                               this.dtp일자.StartDateToString,
                                                               this.dtp일자.EndDateToString });
                }

                this._flex출고의뢰L.BindingAdd(dt, filter);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flexH_CheckHeaderClick(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex출고의뢰H.HasNormalRow) return;

                this._flex출고의뢰H.Redraw = false;

                //데이타 테이블의 체크값을 변경해주어도 화면에 보이는 값을 변경시키지 못하므로 화면의 값도 같이 변경시켜줌
                this._flex출고의뢰H.Row = 1; // 맨처음row에 자동위치하도록 해야 틀어지지 않는다.

                for (int h = 0; h < this._flex출고의뢰H.Rows.Count - 1; h++)
                {
                    MsgControl.ShowMsg("자료를 조회 중 입니다.잠시만 기다려 주세요.(@/@)", new string[] { D.GetString(h + 1), D.GetString(this._flex출고의뢰H.Rows.Count - 1) });

                    this._flex출고의뢰H.Row = h + 1; //Row가 순차적으로 변경되면서 RowChange() 이벤트를 자동 실행하게 유도한다.
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
                this._flex출고의뢰H.Redraw = true;
            }
        }

        private void btn재고출고_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;
            DataTable dt;

            try
            {
                if (!this._flex출고의뢰H.HasNormalRow) return;

                dataRowArray = this._flex출고의뢰H.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    if (this._flex출고의뢰H.DataTable.Select("S = 'Y' AND ISNULL(NO_GIR, '') = ''").Length > 0)
                    {
                        this.ShowMessage("재고출고의뢰가 아닌 건이 선택 되어 있습니다.");
                        return;
                    }

                    if (this._flex출고의뢰H.DataTable.Select("S = 'Y' AND QT_GI > 0").Length > 0)
                    {
                        this.ShowMessage("이미 출고된 건이 선택 되어 있습니다.");
                        return;
                    }

                    this.btn재고출고.Enabled = false;

                    dt = this._flex출고의뢰H.DataTable.Clone();

                    foreach (DataRow dr in dataRowArray)
                    {
                        dt.ImportRow(dr);
                    }

                    this._biz.재고출고(dt);

                    this.ShowMessage(공통메세지._작업을완료하였습니다, "재고출고");

                    this.OnToolBarSearchButtonClicked(null, null);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                this.btn재고출고.Enabled = true;
            }
        }

        private void btn출고취소_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;
            DataTable dt;

            try
            {
                if (!this._flex출고의뢰H.HasNormalRow) return;

                dataRowArray = this._flex출고의뢰H.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    if (this._flex출고의뢰H.DataTable.Select("S = 'Y' AND ISNULL(NO_GIR, '') = ''").Length > 0)
                    {
                        this.ShowMessage("재고출고의뢰가 아닌 건이 선택 되어 있습니다.");
                        return;
                    }

                    if (this._flex출고의뢰H.DataTable.Select("S = 'Y' AND QT_GI = 0").Length > 0)
                    {
                        this.ShowMessage("출고되지 않은 건이 선택 되어 있습니다.");
                        return;
                    }

                    this.btn출고취소.Enabled = false;

                    dt = new DataTable();

                    dt.Columns.Add("CD_COMPANY");
                    dt.Columns.Add("NO_IO");
                    dt.Columns.Add("NO_ISURCVLINE");

                    DataRow newRow;

                    foreach (DataRow dr in dataRowArray)
                    {
                        foreach (DataRow dr1 in ComFunc.getGridGroupBy(this._flex출고의뢰L.DataTable.Select("NO_GIREQ = '" + dr["NO_GIREQ"].ToString() + "'"), new string[] { "CD_COMPANY", "NO_IO" }, true).Rows)
                        {
                            newRow = dt.NewRow();
                            newRow["CD_COMPANY"] = dr1["CD_COMPANY"].ToString();
                            newRow["NO_IO"] = dr1["NO_IO"].ToString();
                            newRow["NO_ISURCVLINE"] = 0;

                            dt.Rows.Add(newRow);
                        }
                    }

                    this._biz.출고취소(dt);

                    this.ShowMessage(공통메세지._작업을완료하였습니다, "출고취소");

                    this.OnToolBarSearchButtonClicked(null, null);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                this.btn출고취소.Enabled = true;
            }
        }

        private void Btn요청삭제품목_Click(object sender, EventArgs e)
        {
            string query;
            DataTable dt, dt1;
            DataRow[] dataRowArray;

            try
            {
                if (Global.MainFrame.ShowMessage("선택된 항목을 삭제 하시겠습니까?", "QY2") != DialogResult.Yes)
                    return;

                dataRowArray = this._flex출고의뢰L.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    dt = new DataTable();

                    dt.Columns.Add("CD_COMPANY");
                    dt.Columns.Add("NO_GIREQ");
                    dt.Columns.Add("NO_LINE");

                    DataRow newRow;

                    query = @"SELECT * 
FROM MM_QTIO OL WITH(NOLOCK)
WHERE OL.CD_COMPANY = '{0}'
AND OL.NO_ISURCV = '{1}'
AND OL.NO_ISURCVLINE = '{2}'";

                    foreach (DataRow dr in dataRowArray)
                    {
                        dt1 = DBHelper.GetDataTable(string.Format(query, dr["CD_COMPANY"].ToString(),
                                                                         dr["NO_GIREQ"].ToString(),
                                                                         dr["NO_LINE"].ToString()));

                        if (dt1 != null && dt1.Rows.Count > 0)
                        {
                            this.ShowMessage("출고처리된 품목이 선택되어 있습니다.");
                            return;
                        }

                        newRow = dt.NewRow();
                        newRow["CD_COMPANY"] = dr["CD_COMPANY"].ToString();
                        newRow["NO_GIREQ"] = dr["NO_GIREQ"].ToString();
                        newRow["NO_LINE"] = dr["NO_LINE"].ToString();

                        dt.Rows.Add(newRow);
                    }

                    this._biz.요청삭제(dt);

                    this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn요청삭제품목.Text);

                    this.OnToolBarSearchButtonClicked(null, null);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Btn출고취소품목_Click(object sender, EventArgs e)
        {
            DataTable dt;
            DataRow[] dataRowArray;

            try
            {
                if (Global.MainFrame.ShowMessage("선택된 항목의 출고를 취소 하시겠습니까?", "QY2") != DialogResult.Yes)
                    return;

                dataRowArray = this._flex출고의뢰L.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    dt = new DataTable();

                    dt.Columns.Add("CD_COMPANY");
                    dt.Columns.Add("NO_IO");
                    dt.Columns.Add("NO_ISURCVLINE");

                    DataRow newRow;

                    foreach (DataRow dr in dataRowArray)
                    {
                        newRow = dt.NewRow();
                        newRow["CD_COMPANY"] = dr["CD_COMPANY"].ToString();
                        newRow["NO_IO"] = dr["NO_IO"].ToString();
                        newRow["NO_ISURCVLINE"] = dr["NO_LINE"].ToString();

                        dt.Rows.Add(newRow);
                    }

                    this._biz.출고취소(dt);

                    this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn출고취소품목.Text);

                    this.OnToolBarSearchButtonClicked(null, null);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
    }
}

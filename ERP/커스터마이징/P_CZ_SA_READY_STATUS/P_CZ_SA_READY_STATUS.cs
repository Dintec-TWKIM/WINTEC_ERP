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
using Duzon.ERPU;
using Dintec;

namespace cz
{
    public partial class P_CZ_SA_READY_STATUS : PageBase
    {
        P_CZ_SA_READY_STATUS_BIZ _biz = new P_CZ_SA_READY_STATUS_BIZ();

        public P_CZ_SA_READY_STATUS()
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
            this.MainGrids = new FlexGrid[] { this._flexH };
            this._flexH.DetailGrids = new FlexGrid[] { this._flexL };

            #region Header
            this._flexH.BeginSetting(2, 1, false);

            this._flexH.SetCol("LN_PARTNER", "Buyer Name", 120);
            this._flexH.SetCol("NM_VESSEL", "Vessel Name", 120);
            this._flexH.SetCol("NO_PO_PARTNER", "Order No.", 80);
            this._flexH.SetCol("NO_SO", "Dintec Ref No", 100);
            this._flexH.SetCol("DT_SO", "Order Date", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("DT_DUEDATE", "Due Date", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("TP_STATUS", "Status Code", false);
            this._flexH.SetCol("NM_TP_STATUS", "Status", 80);
            this._flexH.SetCol("YN_ONTIME", "On Time\n(Expected)", 80, true, CheckTypeEnum.Y_N);
            this._flexH.SetCol("YN_PARTIAL", "Partial", 80, false, CheckTypeEnum.Y_N);
            this._flexH.SetCol("DT_EXPECT", "Ready Date\n(Expected)", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("DC_ITEMS", "Ready Items\n(Item No.)", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("QT_PACK", "Package Qty.", 60);
            this._flexH.SetCol("DC_PACK", "Package Info.", 400);
            this._flexH.SetCol("DC_RMK", "Remark", 150, true);

            this._flexH.ExtendLastCol = true;
            this._flexH.EnabledHeaderCheck = false;

            this._flexH.Cols["NM_TP_STATUS"].TextAlign = TextAlignEnum.CenterCenter;

            this._flexH.SettingVersion = "0.0.0.1";
            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flexH.AllowMerging = AllowMergingEnum.RestrictCols;
            this._flexH.Cols["LN_PARTNER"].AllowMerging = true;
            this._flexH.Cols["NM_VESSEL"].AllowMerging = true;

            this._flexH.Styles.Add("005").BackColor = Color.Green;
            this._flexH.Styles.Add("004").BackColor = Color.GreenYellow;
            this._flexH.Styles.Add("003").BackColor = Color.Yellow;
            this._flexH.Styles.Add("002").BackColor = Color.Orange;
            this._flexH.Styles.Add("001").BackColor = Color.Red;

            this._flexH.Styles.Add("ON_TIME").BackColor = Color.White;
            this._flexH.Styles.Add("DELAYED").BackColor = Color.Red;
            #endregion

            #region Line
            this._flexL.BeginSetting(1, 1, false);

            this._flexL.SetCol("NO_DSP", "No", 60);
            this._flexL.SetCol("CD_ITEM_PARTNER", "Item Code", 100);
            this._flexL.SetCol("NM_ITEM_PARTNER", "Description", 150);
            this._flexL.SetCol("UNIT", "Unit", 60);
            this._flexL.SetCol("QT_INQ", "INQ Qty", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("QT_SO", "SO Qty", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("QT_PO", "PO Qty", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("QT_GR", "GR Qty", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("QT_GIR", "GIR Qty", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("QT_GI", "GI Qty", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("DT_DUEDATE", "Due Date", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexL.SetCol("TP_STATUS", "Status Code", false);
            this._flexL.SetCol("NM_TP_STATUS", "Status", 80);

            this._flexL.Cols["NM_TP_STATUS"].TextAlign = TextAlignEnum.CenterCenter;

            this._flexL.SettingVersion = "0.0.0.1";
            this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flexL.Styles.Add("005").BackColor = Color.Green;
            this._flexL.Styles.Add("004").BackColor = Color.GreenYellow;
            this._flexL.Styles.Add("003").BackColor = Color.Yellow;
            this._flexL.Styles.Add("002").BackColor = Color.Orange;
            this._flexL.Styles.Add("001").BackColor = Color.Red;
            #endregion
        }

        private void InitEvent()
        {
            this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
            this._flexH.OwnerDrawCell += new OwnerDrawCellEventHandler(this._flex_OwnerDrawCell);

            this._flexL.OwnerDrawCell += new OwnerDrawCellEventHandler(this._flex_OwnerDrawCell);
        }

        protected override void InitPaint()
        {
            base.InitPaint();
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!BeforeSearch()) return;

                this._flexH.Redraw = false;

                this._flexH.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                      this.dtp수주일자.StartDateToString,
                                                                      this.dtp수주일자.EndDateToString,
                                                                      this.ctx매출처.CodeValue,
                                                                      this.ctx호선.CodeValue,
                                                                      this.txt수주번호.Text,
                                                                      this.txt매출처발주번호.Text });

                if (!this._flexH.HasNormalRow)
                    ShowMessage(공통메세지.조건에해당하는내용이없습니다);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                this._flexH.Redraw = true;
            }
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSaveButtonClicked(sender, e);

                if (!this.BeforeSave()) return;

                if (MsgAndSave(PageActionMode.Save))
                    ShowMessage(PageResultMode.SaveGood);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            try
            {
                if (!base.SaveData() || !base.Verify()) return false;

                if (this._flexH == null || this._flexH.GetChanges() == null) return false;

                if (this._biz.SaveData(this._flexH.GetChanges()))
                {
                    this._flexH.AcceptChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }

            return false;
        }

        private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt = null;
            string key, filter;
            FlexGrid flex;

            try
            {
                flex = ((FlexGrid)sender);

                key = flex["NO_SO"].ToString();
                filter = "NO_SO = '" + key + "'";

                if (flex.DetailQueryNeed == true)
                {
                    dt = this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                               key });
                }

                flex.DetailGrids[0].BindingAdd(dt, filter);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            CellStyle style;
            FlexGrid grid;


            try
            {
                grid = ((FlexGrid)sender);

                if (e.Row < grid.Rows.Fixed || e.Col < grid.Cols.Fixed) return;

                if (grid.Name == this._flexH.Name)
                {
                    if (grid.Cols[e.Col].Name != "NM_TP_STATUS" &&
                        grid.Cols[e.Col].Name != "YN_ONTIME") return;

                    style = grid.GetCellStyle(e.Row, e.Col);

                    switch (grid.Cols[e.Col].Name)
                    {
                        case "YN_ONTIME":
                            if (style == null)
                            {
                                if (D.GetString(grid.Rows[e.Row]["YN_ONTIME"]) == "Y" || D.GetString(grid.Rows[e.Row]["TP_STATUS"]) == "005")
                                    grid.SetCellStyle(e.Row, e.Col, grid.Styles["ON_TIME"]);
                                else
                                    grid.SetCellStyle(e.Row, e.Col, grid.Styles["DELAYED"]);
                            }
                            else if (style.Name == "ON_TIME")
                            {
                                if (D.GetString(grid.Rows[e.Row]["YN_ONTIME"]) == "N" && D.GetString(grid.Rows[e.Row]["TP_STATUS"]) != "005")
                                    grid.SetCellStyle(e.Row, e.Col, grid.Styles["DELAYED"]);
                            }
                            else if (style.Name == "DELAYED")
                            {
                                if (D.GetString(grid.Rows[e.Row]["YN_ONTIME"]) == "Y" || D.GetString(grid.Rows[e.Row]["TP_STATUS"]) == "005")
                                    grid.SetCellStyle(e.Row, e.Col, grid.Styles["ON_TIME"]);
                            }
                            return;
                        case "NM_TP_STATUS":
                            if (style == null)
                            {
                                grid.SetCellStyle(e.Row, e.Col, grid.Styles[D.GetString(grid.Rows[e.Row]["TP_STATUS"])]);
                                return;
                            }
                            return;
                    }
                }
                else if (grid.Name == this._flexL.Name)
                {
                    if (grid.Cols[e.Col].Name != "NM_TP_STATUS") return;

                    style = grid.GetCellStyle(e.Row, e.Col);

                    switch (grid.Cols[e.Col].Name)
                    {
                        case "NM_TP_STATUS":
                            if (style == null)
                            {
                                grid.SetCellStyle(e.Row, e.Col, grid.Styles[D.GetString(grid.Rows[e.Row]["TP_STATUS"])]);
                                return;
                            }
                            return;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
    }
}

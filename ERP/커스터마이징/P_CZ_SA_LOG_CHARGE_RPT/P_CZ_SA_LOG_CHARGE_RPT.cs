using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.Forms;
using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_SA_LOG_CHARGE_RPT : PageBase
    {
        P_CZ_SA_LOG_CHARGE_RPT_BIZ _biz = new P_CZ_SA_LOG_CHARGE_RPT_BIZ();

        public P_CZ_SA_LOG_CHARGE_RPT()
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

        protected override void InitPaint()
        {
            base.InitPaint();

            this.dtp일자.StartDateToString = MainFrameInterface.GetDateTimeToday().AddMonths(-1).ToString("yyyyMMdd");
            this.dtp일자.EndDateToString = MainFrameInterface.GetStringToday;

            this.cbo기간유형.DataSource = MA.GetCodeUser(new string[] { "001", "002", "003", "004" }, new string[] { "포장일자", "수주일자", "선적일자", "출고일자" }, false);
            this.cbo기간유형.DisplayMember = "NAME";
            this.cbo기간유형.ValueMember = "CODE";
            this.cbo기간유형.SelectedValue = "004";

            this.cbo청구여부.DataSource = MA.GetCodeUser(new string[] { "001", "002" }, new string[] { "가능", "불가" }, true);
            this.cbo청구여부.DisplayMember = "NAME";
            this.cbo청구여부.ValueMember = "CODE";
            this.cbo청구여부.SelectedValue = "001";

            this.cbo포장형태.DataSource = MA.GetCode("TR_IM00011", true);
            this.cbo포장형태.DisplayMember = "NAME";
            this.cbo포장형태.ValueMember = "CODE";
        }

        private void InitGrid()
        {
            try
            {
                this._flexH.DetailGrids = new FlexGrid[] { this._flex수주번호, this._flex목공포장 };

                #region 수주번호별
                this._flex.BeginSetting(1, 1, false);

                this._flex.SetCol("YN_BILL", "청구가능", 60, false, CheckTypeEnum.Y_N);
                this._flex.SetCol("NO_SO", "수주번호", 100);
                this._flex.SetCol("DT_SO", "수주일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                this._flex.SetCol("NM_PACKING", "포장형태", 100);
                this._flex.SetCol("NM_PACK", "실제포장", 100);
                this._flex.SetCol("AM_SO", "수주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flex.SetCol("AM_PO", "발주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flex.SetCol("AM_STOCK", "재고금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flex.SetCol("AM_PROFIT", "이윤", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flex.SetCol("AM_CHARGE", "부대비용", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flex.SetCol("AM_PACK", "포장금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flex.SetCol("AM_IV", "청구금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flex.SetCol("DT_WARNING", "알림발송일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

                this._flex.SettingVersion = "0.0.0.1";
                this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

                this._flex.Styles.Add("YELLOW").BackColor = Color.Yellow;
                this._flex.Styles.Add("WHITE").BackColor = Color.White;
                #endregion

                #region 협조전별

                #region Header
                this._flexH.BeginSetting(2, 1, false);

                this._flexH.SetCol("YN_BILL", "청구가능", 60, false, CheckTypeEnum.Y_N);
                this._flexH.SetCol("NO_GIR", "협조전번호", 100);
                this._flexH.SetCol("NO_IO", "출고번호", 100);
                this._flexH.SetCol("NO_SO", "수주번호", 100);
                this._flexH.SetCol("DT_PACK", "포장일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                this._flexH.SetCol("DT_IO", "출고일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                this._flexH.SetCol("DT_LOADING", "선적일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                this._flexH.SetCol("DT_WARNING", "알림발송일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                this._flexH.SetCol("NM_PARTNER", "매출처", 100);
                this._flexH.SetCol("NM_PARTNER_GRP", "매출처그룹", 100);
                this._flexH.SetCol("NM_DELIVERY_TO", "납품처", 100);
                this._flexH.SetCol("NM_PACKING", "포장형태", 100);
                this._flexH.SetCol("NM_TYPE", "실제포장", 100);
                this._flexH.SetCol("NM_EMP", "영업담당자", 100);
                this._flexH.SetCol("NM_LOG_EMP", "영업물류담당자", 100);
                this._flexH.SetCol("NM_EXCH", "통화명", 100);
                this._flexH.SetCol("RT_EXCH", "환율", 100);

                this._flexH.SetCol("AM_PROFIT", "이윤", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

                this._flexH.SetCol("AM_CBM", "원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flexH.SetCol("AM_EX_CBM", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flexH.SetCol("AM_CHARGE1", "청구금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flexH.SetCol("AM_AUTO_CHARGE1", "청구금액(자동)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

                this._flexH.SetCol("AM_FORWARD", "원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flexH.SetCol("AM_EX_FORWARD", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flexH.SetCol("AM_CHARGE2", "청구금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flexH.SetCol("AM_AUTO_CHARGE2", "청구금액(자동)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

                this._flexH[0, this._flexH.Cols["AM_CBM"].Index] = "포장";
                this._flexH[0, this._flexH.Cols["AM_EX_CBM"].Index] = "포장";
                this._flexH[0, this._flexH.Cols["AM_CHARGE1"].Index] = "포장";
                this._flexH[0, this._flexH.Cols["AM_AUTO_CHARGE1"].Index] = "포장";

                this._flexH[0, this._flexH.Cols["AM_FORWARD"].Index] = "운임";
                this._flexH[0, this._flexH.Cols["AM_EX_FORWARD"].Index] = "운임";
                this._flexH[0, this._flexH.Cols["AM_CHARGE2"].Index] = "운임";
                this._flexH[0, this._flexH.Cols["AM_AUTO_CHARGE2"].Index] = "운임";

                this._flexH.SetCol("DC_RESULT", "포장비고", 100);
                this._flexH.SetCol("DC_RMK1", "비고", 100, true);

                this._flexH.SettingVersion = "0.0.0.2";
                this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

                this._flexH.Styles.Add("YELLOW").BackColor = Color.Yellow;
                this._flexH.Styles.Add("WHITE").BackColor = Color.White;
                #endregion

                #region 수주번호
                this._flex수주번호.BeginSetting(1, 1, false);

                this._flex수주번호.SetCol("YN_BILL", "청구가능", 60, false, CheckTypeEnum.Y_N);
                this._flex수주번호.SetCol("NO_SO", "수주번호", 100);
                this._flex수주번호.SetCol("DT_SO", "수주일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                this._flex수주번호.SetCol("NM_PACKING", "포장형태", 100);
                this._flex수주번호.SetCol("NM_EMP", "영업담당자", 100);
                this._flex수주번호.SetCol("AM_SO", "수주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flex수주번호.SetCol("AM_PO", "발주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flex수주번호.SetCol("AM_STOCK", "재고금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flex수주번호.SetCol("AM_PROFIT", "이윤", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flex수주번호.SetCol("DT_WARNING", "알림발송일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

                this._flex수주번호.SettingVersion = "0.0.0.1";
                this._flex수주번호.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
                #endregion

                #region 목공포장
                this._flex목공포장.BeginSetting(1, 1, false);

                this._flex목공포장.SetCol("NO_PACK", "포장번호", 100);
                this._flex목공포장.SetCol("DT_PACK", "포장일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                this._flex목공포장.SetCol("NO_FILE", "수주번호", 100);
                this._flex목공포장.SetCol("QT_WIDTH", "가로", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                this._flex목공포장.SetCol("QT_LENGTH", "세로", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                this._flex목공포장.SetCol("QT_HEIGHT", "높이", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                this._flex목공포장.SetCol("QT_CBM", "CBM", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                this._flex목공포장.SetCol("AM", "포장금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                this._flex목공포장.SetCol("QT_NET_WEIGHT", "무게", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                this._flex목공포장.SetCol("QT_GROSS_WEIGHT", "총무게", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                this._flex목공포장.SetCol("DC_FILE", "포장사진", 100);

                this._flex목공포장.SettingVersion = "0.0.0.1";
                this._flex목공포장.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
                #endregion

                #endregion
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void InitEvent()
        {
            this._flex.OwnerDrawCell += this._flex_OwnerDrawCell;
            this._flexH.OwnerDrawCell += this._flex_OwnerDrawCell;

            this._flexH.AfterRowChange += _flexH_AfterRowChange;
            this._flexH.AfterEdit += new RowColEventHandler(this._flex_AfterEdit);

            this._flex목공포장.DoubleClick += _flex목공포장_DoubleClick;

            this.btn포장비계산.Click += Btn포장비계산_Click;
        }

        private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt, dt1;
            string key, filter;
            
            try
            {
                key = D.GetString(this._flexH["NO_GIR"]);
                filter = "NO_GIR ='" + key + "'";

                dt = null;
                dt1 = null;

                if (this._flexH.DetailQueryNeed)
                {
                    dt = this._biz.SearchLine(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                             key });

                    dt1 = this._biz.SearchLine1(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                               key });
                }

                this._flex수주번호.BindingAdd(dt, filter);
                this._flex목공포장.BindingAdd(dt1, filter);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            FlexGrid grid = (FlexGrid)sender;

            if (e.Row < grid.Rows.Fixed || e.Col < grid.Cols.Fixed)
                return;

            CellStyle style = grid.Rows[e.Row].Style;

            if (style == null)
            {
                if (!string.IsNullOrEmpty(grid[e.Row, "DT_WARNING"].ToString()))
                    grid.Rows[e.Row].Style = grid.Styles["WHITE"];
                else
                    grid.Rows[e.Row].Style = grid.Styles["YELLOW"];
            }
            else if (style.Name == "YELLOW")
            {
                if (!string.IsNullOrEmpty(grid[e.Row, "DT_WARNING"].ToString()))
                    return;

                grid.Rows[e.Row].Style = grid.Styles["WHITE"];
            }
            else
            {
                if (style.Name != "WHITE" || string.IsNullOrEmpty(grid[e.Row, "DT_WARNING"].ToString()))
                    return;

                grid.Rows[e.Row].Style = grid.Styles["YELLOW"];
            }
        }

        private void _flex_AfterEdit(object sender, RowColEventArgs e)
        {
            FlexGrid flexGrid;
            string query;

            try
            {
                flexGrid = ((FlexGrid)sender);

                if (flexGrid.HasNormalRow == false) return;
                if (flexGrid.Cols[e.Col].Name != "DC_RMK1") return;

                query = @"UPDATE GH
                          SET GH.DC_RMK1 = '" + flexGrid["DC_RMK1"].ToString() + "'" + Environment.NewLine +
                         "FROM SA_GIRH GH" + Environment.NewLine +
                         "WHERE GH.CD_COMPANY = '" + flexGrid["CD_COMPANY"].ToString() + "'" + Environment.NewLine +
                         "AND GH.NO_GIR = '" + flexGrid["NO_GIR"].ToString() + "'";

                DBHelper.ExecuteScalar(query);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex목공포장_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (this._flex목공포장.Cols[this._flex목공포장.Col].Name == "DC_FILE" && !string.IsNullOrEmpty(this._flex목공포장["DC_FILE"].ToString()))
                {
                    WebClient wc = new WebClient();

                    string 로컬경로 = Application.StartupPath + "/temp/";
                    string 서버경로 = Global.MainFrame.HostURL + "/Upload/CZ_SA_PACKH_FILE/" + this._flex목공포장["CD_COMPANY"].ToString() + "/" + this._flex목공포장["NO_GIR"].ToString() + "/";

                    Directory.CreateDirectory(로컬경로);
                    wc.DownloadFile(서버경로 + this._flex목공포장["NM_FILE"].ToString(), 로컬경로 + this._flex목공포장["NM_FILE"].ToString());
                    Process.Start(로컬경로 + this._flex목공포장["NM_FILE"].ToString());
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void Btn포장비계산_Click(object sender, EventArgs e)
        {
            P_CZ_SA_LOG_CHARGE_SUB dialog = new P_CZ_SA_LOG_CHARGE_SUB();
            dialog.ShowDialog();
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!this.BeforeSearch()) return;

                if (this.tabControl1.SelectedIndex == 0)
                {
                    dt = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                         this.dtp일자.StartDateToString,
                                                         this.dtp일자.EndDateToString,
                                                         this.txt수주번호.Text,
                                                         this.cbo청구여부.SelectedValue.ToString(),
                                                         this.cbo포장형태.SelectedValue.ToString(),
                                                         (this.chk포장금액존재.Checked == true ? "Y" : "N"),
                                                         (this.chk이윤보다큰건.Checked == true ? "Y" : "N") });

                    this._flex.Binding = dt;

                    if (!this._flex.HasNormalRow)
                        this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
                else
                {
                    dt = this._biz.SearchHeader(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                               this.cbo기간유형.SelectedValue.ToString(),
                                                               this.dtp일자.StartDateToString,
                                                               this.dtp일자.EndDateToString,
                                                               this.txt수주번호.Text,
                                                               this.txt협조전번호.Text,
                                                               this.cbo청구여부.SelectedValue.ToString(),
                                                               this.cbo포장형태.SelectedValue.ToString(),
                                                               (this.chk포장금액존재.Checked == true ? "Y" : "N"),
                                                               (this.chk이윤보다큰건.Checked == true ? "Y" : "N") });

                    this._flexH.Binding = dt;

                    if (!this._flexH.HasNormalRow)
                        this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
    }
}

using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.Forms;
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
    public partial class P_CZ_MM_NOT_GI_ITEM_LIST : PageBase
    {
        P_CZ_MM_NOT_GI_ITEM_LIST_BIZ _biz = new P_CZ_MM_NOT_GI_ITEM_LIST_BIZ();

        public P_CZ_MM_NOT_GI_ITEM_LIST()
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

            this.dtp입고일자.StartDateToString = MainFrameInterface.GetDateTimeToday().AddDays(-1).ToString("yyyyMMdd");
            this.dtp입고일자.EndDateToString = MainFrameInterface.GetDateTimeToday().AddDays(-1).ToString("yyyyMMdd");
        }

        private void InitGrid()
        {
            #region 미출고리스트
            this._flex미출고리스트.BeginSetting(1, 1, false);

            this._flex미출고리스트.SetCol("NO_DSP", "순번", 100);
            this._flex미출고리스트.SetCol("NM_COMPANY", "회사명", 100);
            this._flex미출고리스트.SetCol("NO_IO", "입고번호", 100);
            this._flex미출고리스트.SetCol("DT_IO", "입고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex미출고리스트.SetCol("LN_PARTNER", "매입처", 150);
            this._flex미출고리스트.SetCol("NO_PO", "발주번호", 100);
            this._flex미출고리스트.SetCol("CD_ITEM", "재고코드", 100);
            this._flex미출고리스트.SetCol("CD_ITEM_PARTNER", "품목코드", 100);
            this._flex미출고리스트.SetCol("NM_ITEM_PARTNER", "품목명", 150);
            this._flex미출고리스트.SetCol("QT_IO", "입고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex미출고리스트.SetCol("CD_LOCATION", "로케이션", 100);
            this._flex미출고리스트.SetCol("YN_WARNING", "주의", 100);
            this._flex미출고리스트.SetCol("DC_RMK_WORK", "작업비고", 100);
            this._flex미출고리스트.SetCol("DC_RMK_SPO", "기획실비고", 100, true);
            this._flex미출고리스트.SetCol("DC_RMK_ALBA", "아르바이트비고", 100, true);

            this._flex미출고리스트.SettingVersion = "0.0.0.1";
            this._flex미출고리스트.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #region 재고코드
            this._flex재고코드.BeginSetting(1, 1, false);

            this._flex재고코드.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flex재고코드.SetCol("CD_ITEM", "재고코드", 100);
            this._flex재고코드.SetCol("NM_ITEM", "품목명", 400);
            this._flex재고코드.SetCol("QT_AVST", "가용재고", 100);




            this._flex재고코드.SettingVersion = "0.0.0.1";
            this._flex재고코드.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

        }

        private void InitEvent()
		{
			this._flex미출고리스트.AfterEdit += _flex_AfterEdit;
		}

        private void _flex_AfterEdit(object sender, RowColEventArgs e)
        {
            FlexGrid flexGrid;
            string query;

            try
            {
                flexGrid = ((FlexGrid)sender);

                if (flexGrid.HasNormalRow == false) return;

                if (flexGrid.Cols[e.Col].Name != "DC_RMK_SPO" &&
                    flexGrid.Cols[e.Col].Name != "DC_RMK_ALBA") return;

                query = @"UPDATE MM_QTIO
                          SET CD_USERDEF1 = '{0}',
                              CD_USERDEF2 = '{1}'
                          WHERE CD_COMPANY = '{2}'
                          AND NO_IO = '{3}'
                          AND NO_IOLINE = '{4}'";

                DBHelper.ExecuteScalar(string.Format(query, flexGrid["DC_RMK_SPO"].ToString(),
                                                            flexGrid["DC_RMK_ALBA"].ToString(),
                                                            flexGrid["CD_COMPANY"].ToString(),
                                                            flexGrid["NO_IO"].ToString(),
                                                            flexGrid["NO_IOLINE"].ToString()));
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

                switch (this.tabControl1.SelectedIndex)
				{
                    case 0:
                        this._flex미출고리스트.Binding = this._biz.Search(new object[] { this.dtp입고일자.StartDateToString,
                                                                             this.dtp입고일자.EndDateToString,
                                                                             (this.chk이미지미등록.Checked == true ? "Y" : "N") });
                        if (!this._flex미출고리스트.HasNormalRow)
                            this.ShowMessage(PageResultMode.SearchNoData);
                        break;

                    case 1:
                        this._flex재고코드.Binding = this._biz.Search1();
                        if (!this._flex재고코드.HasNormalRow)
                            this.ShowMessage(PageResultMode.SearchNoData);
                        break;
                }
                //인쇄버튼 활성화
                this.ToolBarPrintButtonEnabled = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            ReportHelper reportHelper;
            DataTable dt;
            DataRow[] dataRowArray;
            DataRow newRow;
            FlexGrid grid;
            string columnName;
            int index;

            try
            {
                base.OnToolBarPrintButtonClicked(sender, e);


                if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.IndexOf(this.tpg재고코드))
                {
                    grid = this._flex재고코드;
                    columnName = "CD_ITEM";
                }
                else
                    return;

                if (!grid.HasNormalRow) return;


				//체크리스트 선택시
				dataRowArray = grid.DataTable.Select("S = 'Y'");
                if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
				}
				else
				{
					dt = new DataTable();
					dt.Columns.Add("NO_KEY");
					dt.Columns.Add("CD_QR");
					dt.Columns.Add("NO_KEY1");
					dt.Columns.Add("CD_QR1");

					newRow = dt.NewRow();
					index = 0;
					foreach (DataRow dr in dataRowArray)
					{
						if (index != 0 && index % 2 == 0)
							newRow = dt.NewRow();

						if (index % 2 == 0)
						{
							newRow["NO_KEY"] = dr[columnName].ToString() + Environment.NewLine + dr["CD_ITEM"].ToString();
							newRow["CD_QR"] = dr["CD_QR"].ToString();
						}
						else
						{
							newRow["NO_KEY1"] = dr[columnName].ToString() + Environment.NewLine + dr["CD_ITEM"].ToString();
							newRow["CD_QR1"] = dr["CD_QR"].ToString();
						}

						if (index % 2 != 0 || index == dataRowArray.Length - 1)
							dt.Rows.Add(newRow);

						index++;
					}

					reportHelper = new ReportHelper("R_CZ_MM_NOT_GI_ITEM_LIST", "미출고리스트");
					reportHelper.SetDataTable(dt, 1);
					reportHelper.Print();
				}
			}
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
    }
}

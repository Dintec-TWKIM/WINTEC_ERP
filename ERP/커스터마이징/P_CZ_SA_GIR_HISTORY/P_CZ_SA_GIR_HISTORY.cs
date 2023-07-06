using System;
using System.Data;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.Forms;
using Duzon.ERPU;
using System.Windows.Forms;
using System.Drawing;

namespace cz
{
	public partial class P_CZ_SA_GIR_HISTORY : PageBase
	{
		P_CZ_SA_GIR_HISTORY_BIZ _biz = new P_CZ_SA_GIR_HISTORY_BIZ();

		public P_CZ_SA_GIR_HISTORY()
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
			this._flexH.DetailGrids = new FlexGrid[] { this._flexL };

			#region Header
			this._flexH.BeginSetting(1, 1, false);

			this._flexH.SetCol("NM_COMPANY", "회사", 100);
			this._flexH.SetCol("NO_GIR", "의뢰번호", 100);
			this._flexH.SetCol("NO_HST", "차수", 100);
            this._flexH.SetCol("NO_GIREQ", "재고이동번호", 100);
			this._flexH.SetCol("DT_GIR", "의뢰일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexH.SetCol("DT_START", "작업시작일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexH.SetCol("DT_COMPLETE", "완료예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexH.SetCol("DT_BILL", "청구예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexH.SetCol("NM_TP_GIR", "의뢰구분(대)", 90);
			this._flexH.SetCol("NM_MAIN_CATEGORY", "의뢰구분(중)", 90);
			this._flexH.SetCol("NM_SUB_CATEGORY", "의뢰구분(소)", 90);
			this._flexH.SetCol("YN_PACKING", "포장여부", 60, false, CheckTypeEnum.Y_N);
            this._flexH.SetCol("YN_TAX_RETURN", "관세환급여부", 60, false, CheckTypeEnum.Y_N);
			this._flexH.SetCol("YN_URGENT", "긴급여부", 60, false, CheckTypeEnum.Y_N);
			this._flexH.SetCol("YN_AUTO_SUBMIT", "자동제출", 60, false, CheckTypeEnum.Y_N);
			this._flexH.SetCol("TP_CHARGE", "비용청구유형", 100);
			this._flexH.SetCol("NM_STA_GIR", "진행상태", 100);
			this._flexH.SetCol("DT_DONE", "작업완료일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexH.SetCol("NM_EMP", "의뢰자", 60);
			this._flexH.SetCol("NM_VESSEL", "호선명", 140);
			this._flexH.SetCol("NM_VESSEL_BILL", "청구호선명", 140);
			this._flexH.SetCol("NM_PARTNER", "매출처명", 140);
			this._flexH.SetCol("NM_DELIVERY_TO", "납품처명", 140);
			this._flexH.SetCol("ARRIVER_COUNTRY", "도착국가", 80);
			this._flexH.SetCol("NM_RETURN", "출고구분", 80);
			this._flexH.SetCol("DC_RMK1", "취소사유", 100);
			this._flexH.SetCol("DC_RMK2", "매출비고", 100);
            this._flexH.SetCol("DC_RMK3", "기포장정보", 100);
			this._flexH.SetCol("DC_RMK4", "포장비고", 100);
			this._flexH.SetCol("DC_RMK5", "PICKING비고", 100);
			this._flexH.SetCol("NM_RMK", "협조내용", 180);
			this._flexH.SetCol("DC_RMK", "서류처리", 180);
			this._flexH.SetCol("DC_RESULT", "결과비고", 180);
			this._flexH.SetCol("DTS_INSERT", "등록일자", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexH.SetCol("DTS_UPDATE", "수정일자", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexH.SetCol("DTS_SUBMIT", "제출일자", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexH.SetCol("DTS_CONFIRM", "확정일자", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexH.SetCol("DTS_DELETE", "삭제일자", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexH.SetCol("NM_DELETE", "삭제자", 100);

			this._flexH.Cols["DT_DONE"].Format = "####/##/##/##:##:##";
			this._flexH.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";
			this._flexH.Cols["DTS_UPDATE"].Format = "####/##/##/##:##:##";
			this._flexH.Cols["DTS_SUBMIT"].Format = "####/##/##/##:##:##";
			this._flexH.Cols["DTS_CONFIRM"].Format = "####/##/##/##:##:##";
			this._flexH.Cols["DTS_DELETE"].Format = "####/##/##/##:##:##";

			this._flexH.UseMultySorting = true;

			this._flexH.SettingVersion = "0.0.0.1";
			this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

			this._flexH.SetDataMap("TP_CHARGE", MA.GetCodeUser(new string[] { "001", "002" }, new string[] { "자동청구", "수동청구" }), "CODE", "NAME");
			#endregion

			#region Line
			this._flexL.BeginSetting(1, 1, false);

			this._flexL.SetCol("NO_SO", "수주번호", 80);
			this._flexL.SetCol("SEQ_SO", "수주항번", 40);
			this._flexL.SetCol("NO_DSP", "순번", 40);
			this._flexL.SetCol("CD_ITEM_PARTNER", "매출처품번", 100);
			this._flexL.SetCol("NM_ITEM_PARTNER", "매출처품명", 120);
			this._flexL.SetCol("CD_ITEM", "품목코드", 100);
			this._flexL.SetCol("NM_ITEM", "품목명", 120);
			this._flexL.SetCol("UNIT", "단위", 40);
			this._flexL.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexL.SetCol("QT_GIR_STOCK", "재고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexL.SetCol("QT_GI", "출고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("YN_ADD_STOCK", "추가반출", 60, false, CheckTypeEnum.Y_N);

			this._flexL.SettingVersion = "0.0.0.1";
			this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion
		}

		private void InitEvent()
		{
			this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
            this._flexH.MouseDoubleClick += new MouseEventHandler(this._flexH_MouseDoubleClick);
		}

        protected override void InitPaint()
        {
            base.InitPaint();

            this.dtp삭제일자.StartDateToString = Global.MainFrame.GetDateTimeToday().AddMonths(-3).ToString("yyyyMMdd");
            this.dtp삭제일자.EndDateToString = Global.MainFrame.GetStringToday;
        }

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarSearchButtonClicked(sender, e);

				if (!BeforeSearch()) return;

				this._flexH.Binding = this._biz.Search(new object[] { this.ctx회사.CodeValue,
                                                                      this.dtp삭제일자.StartDateToString,
                                                                      this.dtp삭제일자.EndDateToString,
                                                                      this.txt협조전번호.Text,
                                                                      this.txt수주번호.Text,
                                                                      this.txt품목코드.Text });

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
			string filter, key, key1, key2;
			decimal key3;
			DataTable dt;

			try
			{
				if (this._flexH.HasNormalRow == false) return;

				key = D.GetString(this._flexH["CD_COMPANY"]);
				key1 = D.GetString(this._flexH["TP_GIR"]);
				key2 = D.GetString(this._flexH["NO_GIR"]);
				key3 = D.GetDecimal(this._flexH["NO_HST"]);

				filter = "CD_COMPANY = '" + key + "'" + Environment.NewLine +
						 "AND TP_GIR = '" + key1 + "'" + Environment.NewLine +
						 "AND NO_GIR = '" + key2 + "'" + Environment.NewLine +
						 "AND NO_HST = '" + key3 + "'";

				dt = null;
				if (this._flexH.DetailQueryNeed == true)
				{
					dt = this._biz.SearchDetail(new object[] { key,
															   key1,
															   key2,
															   key3,
                                                               this.txt수주번호.Text,
                                                               this.txt품목코드.Text });
				}
				
				this._flexL.BindingAdd(dt, filter);
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

        private void _flexH_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FlexGrid grid;
            string pageId, pageName;
            object[] obj;

            try
            {
                grid = (sender as FlexGrid);
                if (grid.HasNormalRow == false) return;
                if (grid.MouseRow < grid.Rows.Fixed) return;

                if (grid.Cols["NO_GIR"] != null && grid.ColSel == grid.Cols["NO_GIR"].Index)
                {
                    if (D.GetString(grid["YN_RETURN"]) == "Y")
                    {
                        pageId = "P_CZ_SA_GIRR_REG";
                        pageName = this.DD("출고반품의뢰등록");
                        obj = new object[] { pageName, D.GetString(grid["NO_GIR"]), D.GetDecimal(grid["NO_HST"]) };
                    }
                    else
                    {
                        if (D.GetString(grid["TP_GIR"]) == "001")
                        {
                            pageId = "P_CZ_SA_GIR";
                            pageName = this.DD("물류업무협조전");
                            obj = new object[] { pageName, D.GetString(grid["CD_COMPANY"]), D.GetString(grid["NO_GIR"]), D.GetDecimal(grid["NO_HST"]) };
                        }
                        else
                        {
                            pageId = "P_CZ_SA_GIR_PACK";
                            pageName = this.DD("포장업무협조전");
                            obj = new object[] { pageName, D.GetString(grid["CD_COMPANY"]), D.GetString(grid["NO_GIR"]), D.GetDecimal(grid["NO_HST"]) };
                        }
                    }

                    if (Global.MainFrame.IsExistPage(pageId, false))
                        Global.MainFrame.UnLoadPage(pageId, false);

                    Global.MainFrame.LoadPageFrom(pageId, pageName, this.Grant, obj);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
	}
}

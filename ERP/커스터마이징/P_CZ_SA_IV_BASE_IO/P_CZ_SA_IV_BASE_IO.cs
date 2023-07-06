using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
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
	public partial class P_CZ_SA_IV_BASE_IO : PageBase
	{
        P_CZ_SA_IV_BASE_IO_BIZ _biz = new P_CZ_SA_IV_BASE_IO_BIZ();

        public P_CZ_SA_IV_BASE_IO()
		{
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
            #region Header
            this._flexH.BeginSetting(1, 1, false);

            this._flexH.SetCol("NO_PROJECT", "프로젝트", 100);
            this._flexH.SetCol("NO_IO", "출고번호", 100);
            this._flexH.SetCol("DT_IO", "출고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("LN_PARTNER", "매출처", 100);
            this._flexH.SetCol("NM_CC", "C/C명", 100);
            this._flexH.SetCol("AM_SO", "수주금액", 150, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_SA_IV", "출고금액", 150, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_SA_CHARGE", "출고비용", 150, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_PO", "발주금액", 150, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_PU_IV", "매입금액", 150, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_PU_CHARGE", "매입비용", 150, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_STOCK", "재고금액", 150, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_SA_TOTAL", "매출합계", 150, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_PU_TOTAL", "매입합계", 150, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_PROFIT", "이윤", 150, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("RT_PROFIT", "이윤율", 60, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flexH.SettingVersion = "0.0.0.1";
            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flexH.SetExceptSumCol("RT_PROFIT");
            #endregion

            #region Line
            this._flexL.BeginSetting(1, 1, false);

            this._flexL.SetCol("NO_SO", "수주번호", 100);
            this._flexL.SetCol("NO_IO", "출고번호", 100);
            this._flexL.SetCol("DT_IO", "출고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexL.SetCol("AM_SO", "수주금액", 150, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_SA_IV", "출고금액", 150, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_SA_CHARGE", "출고비용", 150, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_PO", "발주금액", 150, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_PU_IV", "매입금액", 150, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_PU_CHARGE", "매입비용", 150, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_STOCK", "재고금액", 150, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("TP_TYPE", "유형", 100);

            this._flexL.SettingVersion = "0.0.0.1";
            this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion
        }

        private void InitEvent()
		{
			this.btn상세보기.Click += Btn상세보기_Click;
		}

        private void Btn상세보기_Click(object sender, EventArgs e)
        {
            try
            {
                this._flexL.Binding = this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                            this._flexH["NO_PROJECT"].ToString() });
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override void InitPaint()
		{
			base.InitPaint();

            this.dtp조회기간.StartDate = Global.MainFrame.GetDateTimeToday().AddMonths(-1);
            this.dtp조회기간.EndDate = Global.MainFrame.GetDateTimeToday();
        }

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!BeforeSearch()) return;

                this._flexH.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                     this.dtp조회기간.StartDateToString,
                                                                     this.dtp조회기간.EndDateToString });

                if (!this._flexH.HasNormalRow)
                    ShowMessage(공통메세지.조건에해당하는내용이없습니다);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
	}
}

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
	public partial class P_CZ_PR_ID_NO_HISTORY : PageBase
	{
		P_CZ_PR_ID_NO_HISTORY_BIZ _biz = new P_CZ_PR_ID_NO_HISTORY_BIZ();

		public P_CZ_PR_ID_NO_HISTORY()
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
			this._flexH.DetailGrids = new FlexGrid[] { this._flex공정, this._flex실적, this._flex측정치 };
			this._flex공정.DetailGrids = new FlexGrid[] { this._flex실적, this._flex측정치 };

			#region Header
			this._flexH.BeginSetting(1, 1, false);

			this._flexH.SetCol("NO_WO", "작업지시번호", 100);
			this._flexH.SetCol("NO_ID", "ID번호", 100);
			this._flexH.SetCol("NO_ID_OLD", "ID번호(이전)", 100);
			this._flexH.SetCol("CD_ITEM", "품목코드", 100);
			this._flexH.SetCol("NM_ITEM", "품목명", 100);
			this._flexH.SetCol("STND_ITEM", "규격", 100);
			this._flexH.SetCol("NO_DESIGN", "도면번호", 100);
			this._flexH.SetCol("NO_HEAT", "소재HEAT번호", 100);
			this._flexH.SetCol("YN_BAD", "불량여부", 60, false, CheckTypeEnum.Y_N);

			this._flexH.SettingVersion = "0.0.0.1";
			this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion

			#region 공정
			this._flex공정.BeginSetting(1, 1, false);

			this._flex공정.SetCol("CD_OP", "Op.", 100);
			this._flex공정.SetCol("NM_ST_OP", "상태", 100);
			this._flex공정.SetCol("NM_WC", "작업장명", 100);
			this._flex공정.SetCol("NM_OP", "공정명", 100);
			this._flex공정.SetCol("CD_EQUIP", "설비코드", 100);
			this._flex공정.SetCol("NM_EQUIP", "설비명", 100);
			this._flex공정.SetCol("QT_WO", "지시수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex공정.SetCol("QT_START", "입고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex공정.SetCol("QT_WORK", "작업수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex공정.SetCol("QT_MOVE", "이동수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex공정.SetCol("QT_WIP", "대기수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex공정.SetCol("YN_BAD", "불량여부", 60, false, CheckTypeEnum.Y_N);

			this._flex공정.SettingVersion = "0.0.0.1";
			this._flex공정.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion

			#region 측정치
			this._flex측정치.BeginSetting(1, 1, false);

			this._flex측정치.SetCol("NO_INSP", "측정순번", 100);
			this._flex측정치.SetCol("DT_INSP", "측정일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex측정치.SetCol("NM_KOR", "측정자", 100);
			this._flex측정치.SetCol("NO_DATA1", "측정치1", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex측정치.SetCol("NO_DATA2", "측정치2", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex측정치.SetCol("NO_DATA3", "측정치3", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex측정치.SetCol("NO_DATA4", "측정치4", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex측정치.SetCol("NO_DATA5", "측정치5", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex측정치.SetCol("NO_HEAT", "열처리번호", 100);
			this._flex측정치.SetCol("NM_REJECT", "불량종류", 100);
			this._flex측정치.SetCol("NM_RESOURCE", "불량원인", 100);
			this._flex측정치.SetCol("DTS_INSERT", "등록일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			
			this._flex측정치.Cols["DTS_INSERT"].Format = "####/##/## ##:##:##";

			this._flex측정치.SettingVersion = "0.0.0.1";
			this._flex측정치.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion

			#region 실적
			this._flex실적.BeginSetting(1, 1, false);

			this._flex실적.SetCol("NO_WORK", "실적번호", 100);
			this._flex실적.SetCol("NM_KOR", "담당자", 100);
			this._flex실적.SetCol("DT_WORK", "실적일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex실적.SetCol("QT_WORK", "실적수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex실적.SetCol("NM_EQUIP", "설비명", 100);
			this._flex실적.SetCol("DTS_INSERT", "등록일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

			this._flex실적.Cols["DTS_INSERT"].Format = "####/##/## ##:##:##";

			this._flex실적.SettingVersion = "0.0.0.1";
			this._flex실적.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion
		}

		private void InitEvent()
		{
			this._flexH.AfterRowChange += _flexH_AfterRowChange;
			this._flex공정.AfterRowChange += _flex공정_AfterRowChange;
		}

		protected override void InitPaint()
		{
			base.InitPaint();

			this.splitContainer1.SplitterDistance = 679;
			this.splitContainer2.SplitterDistance = 343;
		}

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                this._flexH.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																      this.txt작업지시번호.Text,
																      this.txtID번호.Text,
																      this.txtID번호이전.Text });

                if (!this._flexH.HasNormalRow)
                {
                    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

		private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
		{
			string filter;
			DataTable dt;

			try
			{
				dt = null;
				filter = string.Format("NO_WO = '{0}' AND SEQ_WO = '{1}'", this._flexH["NO_WO"].ToString(),
																		   this._flexH["SEQ_WO"].ToString());

				if (this._flexH.DetailQueryNeed == true)
				{
					dt = this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
														       this._flexH["NO_WO"].ToString(),
														       this._flexH["SEQ_WO"].ToString() });
				}

				this._flex공정.BindingAdd(dt, filter);
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void _flex공정_AfterRowChange(object sender, RangeEventArgs e)
		{
			string filter;
			DataTable dt, dt1;

			try
			{
				dt = null;
				dt1 = null;
				filter = string.Format("NO_WO = '{0}' AND SEQ_WO = '{1}' AND NO_LINE = '{2}'", this._flex공정["NO_WO"].ToString(),
																							   this._flex공정["SEQ_WO"].ToString(),
																							   this._flex공정["NO_LINE"].ToString());

				if (this._flex공정.DetailQueryNeed == true)
				{
					dt = this._biz.SearchDetail1(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																this._flex공정["NO_WO"].ToString(),
																this._flex공정["SEQ_WO"].ToString(),
																this._flex공정["NO_LINE"].ToString() });

					dt1 = this._biz.SearchDetail2(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																 this._flex공정["NO_WO"].ToString(),
																 this._flex공정["SEQ_WO"].ToString(),
																 this._flex공정["NO_LINE"].ToString(),
																 this._flex공정["CD_OP"].ToString() });
				}

				this._flex측정치.BindingAdd(dt, filter);
				this._flex실적.BindingAdd(dt1, filter);
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}
	}
}

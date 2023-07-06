using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.Forms;
using Duzon.ERPU;
using DX;
using System;
using System.Data;
using System.Drawing;
using System.IO;

namespace cz
{
	public partial class P_CZ_SA_SO_MONTHLY_REPORT : PageBase
	{
		P_CZ_SA_SO_MONTHLY_REPORT_BIZ _biz = new P_CZ_SA_SO_MONTHLY_REPORT_BIZ();

		public P_CZ_SA_SO_MONTHLY_REPORT()
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
			this._flex.BeginSetting(1, 1, false);

			this._flex.SetCol("NM_CC", "C/C", 100);
			this._flex.SetCol("AM_SO", "수주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex.SetCol("AM_MONTHWON", "월목표", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex.SetCol("RT_MONTH", "월달성율", 60, false, typeof(decimal), FormatTpType.RATE);
			this._flex.SetCol("AM_SO_OVER", "초과실적", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

			this._flex.SettingVersion = "0.0.0.2";
			this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.None);

			this._flex.Styles.Add("일반").ForeColor = Color.Black;
			this._flex.Styles.Add("일반").BackColor = Color.White;
			this._flex.Styles.Add("총계").ForeColor = Color.Blue;
			this._flex.Styles.Add("총계").BackColor = Color.White;

			this._flex.SetSumColumnStyle("NM_CC");

			this._flex.OwnerDrawCell += _flex_OwnerDrawCell;
		}

		private void InitEvent()
		{
			this.btn다운로드.Click += Btn다운로드_Click;
		}

		private void Btn다운로드_Click(object sender, EventArgs e)
		{
			try
			{
				string head = @"<style type='text/css'>
									table { table-layout:fixed; }
									th, td	{ padding-top:5px;  padding-bottom:5px; border:1px solid #000; }
									th		{ padding-left:5px; padding-right:5px;  background:#e6f4ff; }
									td		{ padding-left:9px; padding-right:3px; }
									th		{ white-space:nowrap; }
									td		{ white-space:nowrap; }
								</style>";

				string body = @"<div>
									<table>
										<tr>
											<th>C/C</th>
											<th>수주금액</th>
											<th>월목표</th>
											<th>월달성율</th>
											<th>초과수주</th>
										</tr>";

				DataTable dt = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode });

				foreach(DataRow dr in dt.Rows)
				{
					body += string.Format(@"<tr>
												<th> {0} </th>
												<td style = 'text-align:right;'> {1} </td>
												<td style = 'text-align:right;'> {2} </td>
												<td style = 'text-align:right;'> {3} </td>
												<td style = 'text-align:right;'> {4} </td>
											</tr>", dr["NM_CC"].ToString(),
													Util.GetTO_Money(dr["AM_SO"]),
													Util.GetTO_Money(dr["AM_MONTHWON"]),
													dr["RT_MONTH"].ToString() + '%',
													Util.GetTO_Money(dr["AM_SO_OVER"]));
				}

				body += @"		</table>
						  </div>";

				string html = HTML.GetHtmlDocument(head, body);
				HTML.ConvertToImage(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "//img.jpg", html);
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void _flex_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
		{
			try
			{
				if (!this._flex.HasNormalRow) return;

				CellStyle cellStyle = this._flex.Rows[e.Row].Style;

				switch (D.GetString(this._flex[e.Row, "TP_COLOR"]))
				{
					case "000":
						if (cellStyle == null || cellStyle.Name != "일반")
							this._flex.Rows[e.Row].Style = this._flex.Styles["일반"];
						break;
					case "001":
						if (cellStyle == null || cellStyle.Name != "총계")
							this._flex.Rows[e.Row].Style = this._flex.Styles["총계"];
						break;
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarSearchButtonClicked(sender, e);

				this._flex.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode });

				if (!this._flex.HasNormalRow)
				{
					Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}
	}
}

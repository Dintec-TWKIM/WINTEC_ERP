using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using System;

namespace cz
{
	public partial class P_CZ_PR_POP_REG : PageBase
	{
		P_CZ_PR_POP_REG_BIZ _biz = new P_CZ_PR_POP_REG_BIZ();

		public P_CZ_PR_POP_REG()
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

			this._flex.SetCol("NM_ST_WO", "진행현황", 100);
			this._flex.SetCol("NO_WO", "작업지시번호", 100);
			this._flex.SetCol("NO_SO", "수주번호", false);
			this._flex.SetCol("DT_DUE", "납기일", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex.SetCol("CD_PITEM", "품목코드", 100);
			this._flex.SetCol("NM_PITEM", "품목명", 100);
			this._flex.SetCol("NO_DESIGN", "도면번호", 100);

			this._flex.SetCol("QT_WO", "지시수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex.SetCol("QT_START", "입고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex.SetCol("QT_WORK", "작업수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex.SetCol("QT_REJECT", "불량수량", false);
			this._flex.SetCol("QT_BAD", "불량처리수량", false);
			this._flex.SetCol("QT_WIP", "대기수량", false);
			this._flex.SetCol("QT_MOVE", "이동수량", false);
			this._flex.SetCol("QT_REWORK", "재작업수량", false);
			this._flex.SetCol("QT_REMAIN", "작업잔량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex.SetCol("QT_REWORK_REMAIN", "재작업잔량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex.SetCol("QT_OUTPO", "외주발주수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			
			this._flex.SetCol("NM_OP", "현재공정", 100);
			this._flex.SetCol("NM_OP_NEXT", "다음공정", 100);
			this._flex.SetCol("CD_EQUIP", "설비코드", 100);
			this._flex.SetCol("NM_EQUIP", "설비명", 100);
			this._flex.SetCol("YN_INSP", "측정여부", false);
			this._flex.SetCol("DC_OP", "작업내용", 100);

			this._flex.ExtendLastCol = true;

			this._flex.SettingVersion = "0.0.0.3";
			this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
		}

		private void InitEvent()
		{
			this.ctx설비.QueryBefore += Ctx설비_QueryBefore;

			this.btn작업.Click += Btn작업_Click;
			this.btn재작업.Click += Btn재작업_Click;
			this.btn지침서보기.Click += Btn지침서보기_Click;

			this._flex.AfterRowChange += _flex_AfterRowChange;
		}

		private void _flex_AfterRowChange(object sender, RangeEventArgs e)
		{
			try
			{
				if (this._flex["YN_IMAGE"].ToString() == "Y")
					this.btn지침서보기.Enabled = true;
				else
					this.btn지침서보기.Enabled = false;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		protected override void InitPaint()
		{
			base.InitPaint();

			this.cbo공장.DataSource = Global.MainFrame.GetComboDataCombine("NC;MA_PLANT");
			this.cbo공장.ValueMember = "CODE";
			this.cbo공장.DisplayMember = "NAME";
		}

		private void Ctx설비_QueryBefore(object sender, BpQueryArgs e)
		{
			try
			{
				e.HelpParam.P00_CHILD_MODE = "설비 도움창";
				e.HelpParam.P61_CODE1 = "EQ.CD_EQUIP AS CODE, EQ.NM_EQUIP AS NAME";
				e.HelpParam.P62_CODE2 = @"PR_EQUIP EQ";
				e.HelpParam.P63_CODE3 = string.Format(@"WHERE EQ.CD_COMPANY = '{0}'
														AND EQ.CD_PLANT = '{1}'", Global.MainFrame.LoginInfo.CompanyCode, this.cbo공장.SelectedValue.ToString());
				e.HelpParam.P64_CODE4 = "GROUP BY EQ.CD_EQUIP, EQ.NM_EQUIP";
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

				this._flex.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																     this.ctx설비.CodeValue,
																	 this.txt작업지시번호.Text });

				if (!this._flex.HasNormalRow)
					this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn작업_Click(object sender, EventArgs e)
		{
			try
			{
				if (this._flex["YN_SUBCON"].ToString() == "Y" && 
					Convert.ToDecimal(this._flex["QT_OUTPO"]) != Convert.ToDecimal(this._flex["QT_START"]))
				{
					this.ShowMessage(string.Format("외주발주수량과 입고수량이 일치하지 않습니다. (외주발주수량 : {0}, 입고수량 : {1})", Convert.ToDecimal(this._flex["QT_OUTPO"]), Convert.ToDecimal(this._flex["QT_START"])));
					return;
				}

				if (Convert.ToDecimal(this._flex["QT_REMAIN"]) <= 0)
				{
					this.ShowMessage(공통메세지._은_보다커야합니다, new string[] { "작업잔량", "0" });
					return;
				}

				if (this._flex["YN_INSP"].ToString() == "Y")
				{
					P_CZ_PR_POP_REG_SUB1 dialog = new P_CZ_PR_POP_REG_SUB1(this.cbo공장.SelectedValue.ToString(),
																		   this._flex["NO_WO"].ToString(),
																		   Convert.ToInt32(this._flex["NO_LINE"].ToString()),
																		   false,
																		   this._flex["CD_EQUIP"].ToString(),
																		   this._flex["NM_EQUIP"].ToString());
					dialog.ShowDialog();
				}
				else
				{
					P_CZ_PR_POP_REG_SUB dialog = new P_CZ_PR_POP_REG_SUB(this.cbo공장.SelectedValue.ToString(),
																		 this._flex["NO_WO"].ToString(),
																		 Convert.ToInt32(this._flex["NO_LINE"].ToString()),
																		 false,
																		 this._flex["CD_EQUIP"].ToString(),
																		 this._flex["NM_EQUIP"].ToString());
					dialog.ShowDialog();
				}

				this.OnToolBarSearchButtonClicked(null, null);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn재작업_Click(object sender, EventArgs e)
		{
			try
			{
				if (Convert.ToDecimal(this._flex["QT_REWORK_REMAIN"]) <= 0)
				{
					this.ShowMessage(공통메세지._은_보다커야합니다, new string[] { "재작업잔량", "0" });
					return;
				}

				if (this._flex["YN_INSP"].ToString() == "Y")
				{
					P_CZ_PR_POP_REG_SUB1 dialog = new P_CZ_PR_POP_REG_SUB1(this.cbo공장.SelectedValue.ToString(),
																		   this._flex["NO_WO"].ToString(),
																		   Convert.ToInt32(this._flex["NO_LINE"].ToString()),
																		   true,
																		   this._flex["CD_EQUIP"].ToString(),
																		   this._flex["NM_EQUIP"].ToString());
					dialog.ShowDialog();
				}
				else
				{
					P_CZ_PR_POP_REG_SUB dialog = new P_CZ_PR_POP_REG_SUB(this.cbo공장.SelectedValue.ToString(),
																		 this._flex["NO_WO"].ToString(),
																		 Convert.ToInt32(this._flex["NO_LINE"].ToString()),
																		 true,
																		 this._flex["CD_EQUIP"].ToString(),
																		 this._flex["NM_EQUIP"].ToString());
					dialog.ShowDialog();
				}

				this.OnToolBarSearchButtonClicked(null, null);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn지침서보기_Click(object sender, EventArgs e)
		{
			try
			{
				if (this._flex["YN_IMAGE"].ToString() != "Y") return;

				P_CZ_PR_POP_REG_IMAGE_SUB dialog = new P_CZ_PR_POP_REG_IMAGE_SUB(this.cbo공장.SelectedValue.ToString(),
																				 this._flex["CD_PITEM"].ToString(),
																				 this._flex["PATN_ROUT"].ToString(),
																				 this._flex["CD_OP"].ToString(),
																				 this._flex["CD_WCOP"].ToString());
				dialog.Show();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}
	}
}

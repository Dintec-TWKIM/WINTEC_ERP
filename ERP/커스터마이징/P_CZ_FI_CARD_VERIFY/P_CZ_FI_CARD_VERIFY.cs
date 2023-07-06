using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.ERPU.FI;
using Duzon.ERPU.Grant;
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
	public partial class P_CZ_FI_CARD_VERIFY : PageBase
	{
		P_CZ_FI_CARD_VERIFY_BIZ _biz = new P_CZ_FI_CARD_VERIFY_BIZ();

		public P_CZ_FI_CARD_VERIFY()
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
			this.MainGrids = new FlexGrid[] { this._flexH };
			this._flexH.DetailGrids = new FlexGrid[] { this._flex승인내역, this._flex승인내역IBK, this._flex청구내역IBK, this._flex승인내역BC, this._flex매입내역BC, this._flex청구내역BC, this._flex전표정보 };

			#region Header
			this._flexH.BeginSetting(1, 1, false);

			this._flexH.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
			this._flexH.SetCol("NO_CARD", "카드번호", 100);
			this._flexH.SetCol("NM_CARD", "카드명", 100);
			this._flexH.SetCol("NM_OWNER", "소유자", 100);
			this._flexH.SetCol("ADMIN_NO", "승인번호", 100);
			this._flexH.SetCol("TRADE_DATE", "승인일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexH.SetCol("ADMIN_AMT", "승인금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexH.SetCol("AM_DOCU", "전표금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexH.SetCol("AM_APPR_IBK", "승인금액(IBK)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexH.SetCol("AM_BILL_IBK", "청구금액(IBK)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexH.SetCol("AM_APPR_BC", "승인금액(BC)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexH.SetCol("AM_ACQ_BC", "매입금액(BC)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexH.SetCol("AM_BILL_BC", "청구금액(BC)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexH.SetCol("YN_VERIFY", "확인여부", 100);
			this._flexH.SetCol("DC_RMK", "비고", 100, true);

			this._flexH.SettingVersion = "0.0.0.1";
            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion

			#region 승인내역
			this._flex승인내역.BeginSetting(1, 1, false);

			this._flex승인내역.SetCol("TRADE_DATE", "승인일자", 100, false , typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex승인내역.SetCol("TRADE_TIME", "승인시간", 100);
			this._flex승인내역.SetCol("CURRENCY", "통화명", 100);
			this._flex승인내역.SetCol("AQUI_RATE", "환율", 100);
			this._flex승인내역.SetCol("AQUI_DOLL", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex승인내역.SetCol("SUPPLY_AMT", "공급가액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex승인내역.SetCol("VAT_AMT", "부가세", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex승인내역.SetCol("ADMIN_AMT", "승인금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex승인내역.SetCol("NO_DOCU", "전표번호", 100);
			this._flex승인내역.SetCol("LINE_NO", "전표라인", 100);
			this._flex승인내역.SetCol("ST_DOCU", "전표상태", 100);
			this._flex승인내역.SetCol("TRADE_PLACE", "가맹점", 100);
			this._flex승인내역.SetCol("S_IDNO", "사업자등록번호", 100);
			this._flex승인내역.SetCol("MCC_CODE_NAME", "업종", 100);

			this._flex승인내역.SetDataMap("ST_DOCU", FI.GetCode("FI_J000003"), "CODE", "NAME");

			this._flex승인내역.Cols["TRADE_TIME"].Format = "00:00:00";
			this._flex승인내역.Cols["TRADE_TIME"].TextAlign = TextAlignEnum.CenterCenter;
			this._flex승인내역.Cols["S_IDNO"].Format = "000-00-00000";
			this._flex승인내역.Cols["S_IDNO"].TextAlign = TextAlignEnum.CenterCenter;
			this._flex승인내역.SetStringFormatCol(new string[] { "TRADE_TIME", "S_IDNO" });

			this._flex승인내역.SettingVersion = "0.0.0.1";
			this._flex승인내역.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion

			#region 승인내역(IBK)
			this._flex승인내역IBK.BeginSetting(1, 1, false);

			this._flex승인내역IBK.SetCol("APPR_DATE", "승인일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex승인내역IBK.SetCol("APPR_TIME", "승인시간", 100);
			this._flex승인내역IBK.SetCol("CURRENCY", "통화명", 100);
			this._flex승인내역IBK.SetCol("USE_AMT", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex승인내역IBK.SetCol("WON_AMT", "원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex승인내역IBK.SetCol("APPR_AMT", "승인금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex승인내역IBK.SetCol("CHAIN_NM", "가맹점", 100);
			this._flex승인내역IBK.SetCol("BRANCHTYPE", "업종", 100);

			this._flex승인내역IBK.SettingVersion = "0.0.0.1";
			this._flex승인내역IBK.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion

			#region 청구내역(IBK)
			this._flex청구내역IBK.BeginSetting(1, 1, false);

			this._flex청구내역IBK.SetCol("BILL_YYYYMM", "청구년월", 100, false, typeof(string), FormatTpType.YEAR_MONTH);
			this._flex청구내역IBK.SetCol("USE_DATE", "사용일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex청구내역IBK.SetCol("SETT_DATE", "청구일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex청구내역IBK.SetCol("USE_AMT", "사용금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex청구내역IBK.SetCol("DEMAND_AMT", "청구원금", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex청구내역IBK.SetCol("DC_AMT", "할인금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex청구내역IBK.SetCol("BILL_AMT", "청구금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex청구내역IBK.SetCol("CHAIN_NM", "가맹점", 100);
			this._flex청구내역IBK.SetCol("BRANCHTYPE", "업종", 100);

			this._flex청구내역IBK.SettingVersion = "0.0.0.1";
			this._flex청구내역IBK.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion

			#region 승인내역(BC)
			this._flex승인내역BC.BeginSetting(1, 1, false);

			this._flex승인내역BC.SetCol("SENDDATE", "수신일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex승인내역BC.SetCol("TRANSDATE", "승인일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex승인내역BC.SetCol("TRANSTIME", "승인시간", 100);
			this._flex승인내역BC.SetCol("CURRCODE", "통화명", 100);
			this._flex승인내역BC.SetCol("APPREXCH", "환율", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex승인내역BC.SetCol("USDAPPRTOT", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex승인내역BC.SetCol("APPRAMT", "원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex승인내역BC.SetCol("VAT", "부가세", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex승인내역BC.SetCol("APPRTOT", "합계", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex승인내역BC.SetCol("MERCHNAME", "가맹점", 100);
			this._flex승인내역BC.SetCol("MCCNAME", "업종", 100);

			this._flex승인내역BC.SettingVersion = "0.0.0.1";
			this._flex승인내역BC.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion

			#region 매입내역(BC)
			this._flex매입내역BC.BeginSetting(1, 1, false);

			this._flex매입내역BC.SetCol("SENDDATE", "수신일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex매입내역BC.SetCol("APPRDATE", "승인일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex매입내역BC.SetCol("PURCHDATE", "매입일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex매입내역BC.SetCol("PURCHTIME", "매입시간", 100);
			this._flex매입내역BC.SetCol("SETTDATE", "결제예정일", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex매입내역BC.SetCol("CURRCODE", "통화명", 100);
			this._flex매입내역BC.SetCol("ACQUEXCH", "환율", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex매입내역BC.SetCol("USDACQUTOT", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex매입내역BC.SetCol("APPRAMT", "공급가액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex매입내역BC.SetCol("VAT", "부가세", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex매입내역BC.SetCol("ACQUTOT", "매입합계", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex매입내역BC.SetCol("PURCHTOT", "매입금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex매입내역BC.SetCol("MERCHNAME", "가맹점", 100);
			this._flex매입내역BC.SetCol("MCCNAME", "업종", 100);

			this._flex매입내역BC.SettingVersion = "0.0.0.1";
			this._flex매입내역BC.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion

			#region 청구내역(BC)
			this._flex청구내역BC.BeginSetting(1, 1, false);

			this._flex청구내역BC.SetCol("SENDDATE", "수신일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex청구내역BC.SetCol("WORKDATE", "청구년월", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex청구내역BC.SetCol("SETTDATE", "예상청구일", 100);
			this._flex청구내역BC.SetCol("ORGNAPPRDATE", "승인일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex청구내역BC.SetCol("ACQUEXCH", "환율", 100);
			this._flex청구내역BC.SetCol("USDACQUTOT", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex청구내역BC.SetCol("BILAMT", "청구원금", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex청구내역BC.SetCol("BILFEE", "부가세", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex청구내역BC.SetCol("BILTOT", "청구금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex청구내역BC.SetCol("MERCHNAME", "가맹점", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			
			this._flex청구내역BC.SettingVersion = "0.0.0.1";
			this._flex청구내역BC.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion

			#region 전표정보
			this._flex전표정보.BeginSetting(1, 1, false);

			this._flex전표정보.SetCol("NO_DOCU", "전표번호", 100);
			this._flex전표정보.SetCol("NO_DOLINE", "전표라인", 100);
			this._flex전표정보.SetCol("CD_ACCT", "계정코드", 100);
			this._flex전표정보.SetCol("NM_ACCT", "계정명", 100);
			this._flex전표정보.SetCol("NM_NOTE", "적요", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex전표정보.SetCol("AM_DR", "대변", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex전표정보.SetCol("AM_CR", "차변", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

			this._flex전표정보.SettingVersion = "0.0.0.1";
			this._flex전표정보.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion
		}

		private void InitEvent()
		{
			this.btn확인처리.Click += Btn확인처리_Click;
			this.btn확인해제.Click += Btn확인해제_Click;

			this._flexH.AfterRowChange += _flexH_AfterRowChange;
			this._flex승인내역.AfterRowChange += _flex승인내역_AfterRowChange;
		}

		protected override void InitPaint()
		{
			base.InitPaint();

			this.dtp승인일자.StartDateToString = Global.MainFrame.GetDateTimeToday().AddYears(-1).ToString("yyyyMMdd");
			this.dtp승인일자.EndDateToString = Global.MainFrame.GetStringToday;

			UGrant ugrant = new UGrant();
			ugrant.GrantButtonVisible(this.PageID, "VERIFY", this.btn확인처리);
			ugrant.GrantButtonVisible(this.PageID, "VERIFY", this.btn확인해제);
		}

		private void _flex승인내역_AfterRowChange(object sender, RangeEventArgs e)
		{
			try
			{
				if (this._flex승인내역.HasNormalRow == false) return;

				this._flex전표정보.Binding = this._biz.SearchDetail2(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																				    this._flex승인내역["ACCT_NO"].ToString(),
																				    this._flex승인내역["ADMIN_NO"].ToString(),
																				    this._flex승인내역["NO_DOCU"].ToString() });
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
		{
			DataTable dt, dt1, dt2, dt3, dt4, dt5;
			string key, key1, filter;

			try
			{
				if (this._flexH.HasNormalRow == false) return;

				key = this._flexH["ACCT_NO"].ToString();
				key1 = this._flexH["ADMIN_NO"].ToString();

				filter = "ACCT_NO = '" + key + "' AND ADMIN_NO = '" + key1 + "'";
				dt = null;
				dt1 = null;
				dt2 = null;
				dt3 = null;
				dt4 = null;
				dt5 = null;

				if (this._flexH.DetailQueryNeed == true)
				{
					dt = this._biz.SearchDetail1(new object[] { Global.MainFrame.LoginInfo.CompanyCode, key, key1 });
					dt1 = this._biz.SearchDetail3(new object[] { key, key1 });
					dt2 = this._biz.SearchDetail4(new object[] { key, key1 });
					dt3 = this._biz.SearchDetail5(new object[] { key, key1 });
					dt4 = this._biz.SearchDetail6(new object[] { key, key1 });
					dt5 = this._biz.SearchDetail7(new object[] { key, key1 });
				}

				this._flex승인내역.BindingAdd(dt, filter);
				this._flex승인내역IBK.BindingAdd(dt1, filter);
				this._flex청구내역IBK.BindingAdd(dt2, filter);
				this._flex승인내역BC.BindingAdd(dt3, filter);
				this._flex매입내역BC.BindingAdd(dt4, filter);
				this._flex청구내역BC.BindingAdd(dt5, filter);
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

				this._flexH.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																	  this.dtp승인일자.StartDateToString,
																	  this.dtp승인일자.EndDateToString,
																	  this.txt승인번호.Text,
																	  this.ctx카드번호.CodeValue,
																	  (this.chk전표처리.Checked == true ? "Y" : "N"),
																	  (this.chk전표승인.Checked == true ? "Y" : "N"),
																	  (this.chk전표vs승인.Checked == true ? "Y" : "N"),
																	  (this.chk전표vs승인IBK.Checked == true ? "Y" : "N"),
																	  (this.chk전표vs청구IBK.Checked == true ? "Y" : "N"),
																	  (this.chk전표vs승인BC.Checked == true ? "Y" : "N"),
																	  (this.chk전표vs매입BC.Checked == true ? "Y" : "N"),
																	  (this.chk전표vs청구BC.Checked == true ? "Y" : "N"),
																	  (this.chk데이터없음.Checked == true ? "Y" : "N"),
																	  (this.chk확인제외.Checked == true ? "Y" : "N") });

				if (!this._flexH.HasNormalRow)
					this.ShowMessage(PageResultMode.SearchNoData);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
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
			if (!base.SaveData() || !base.Verify()) return false;
			if (this._flexH.IsDataChanged == false) return false;

			if (!_biz.Save(this._flexH.GetChanges())) return false;

			this._flexH.AcceptChanges();

			return true;
		}

		private void Btn확인해제_Click(object sender, EventArgs e)
		{
			DataRow[] dataRowArray;

			try
			{
				if (!this._flexH.HasNormalRow) return;

				dataRowArray = this._flexH.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
				}
				else
				{
					foreach (DataRow dr in dataRowArray)
					{
						dr["YN_VERIFY"] = "N";
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn확인처리_Click(object sender, EventArgs e)
		{
			DataRow[] dataRowArray;

			try
			{
				if (!this._flexH.HasNormalRow) return;

				dataRowArray = this._flexH.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
				}
				else
				{
					foreach (DataRow dr in dataRowArray)
					{
						dr["YN_VERIFY"] = "Y";
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}
	}
}

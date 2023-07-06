using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.ERPU;
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
	public partial class P_CZ_FI_ETAX_DIFF_RPT : PageBase
	{
		P_CZ_FI_ETAX_DIFF_RPT_BIZ _biz = new P_CZ_FI_ETAX_DIFF_RPT_BIZ();

		public P_CZ_FI_ETAX_DIFF_RPT()
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
			#region 차이
            this._flex차이.BeginSetting(1, 1, false);

			this._flex차이.SetCol("S", "선택", 100, true, CheckTypeEnum.Y_N);
			this._flex차이.SetCol("YN_AUTO", "자동대상", 100, false, CheckTypeEnum.Y_N);
			this._flex차이.SetCol("NO_ETAX", "승인번호", 100);
			this._flex차이.SetCol("DT_START", "작성일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex차이.SetCol("NO_COMPANY1", "사업자등록번호", 100);
			this._flex차이.SetCol("NM_COMPANY1", "상호", 100);
			this._flex차이.SetCol("AM_TAXSTD", "공급가액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex차이.SetCol("AM_ADDTAX", "세액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex차이.SetCol("AM_SUM", "합계", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex차이.SetCol("TP_TAX", "유형", 100);
			this._flex차이.SetCol("NM_BIGO", "비고", 100);
			this._flex차이.SetCol("BUY_DAM_EMAIL", "메일주소1", 100);
			this._flex차이.SetCol("BUY_DAM_EMAIL2", "메일주소2", 100);
			this._flex차이.SetCol("NM_ITEM", "품목비고", 100);
			this._flex차이.SetCol("NM_KOR", "담당자", 100);
			this._flex차이.SetCol("DC_RMK", "비고", 100, true);

			this._flex차이.SettingVersion = "0.0.0.1";
            this._flex차이.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion

			#region 국세청
			this._flex국세청.BeginSetting(1, 1, false);

			this._flex국세청.SetCol("YN_AUTO", "자동대상", 100, false, CheckTypeEnum.Y_N);
			this._flex국세청.SetCol("NO_ETAX", "승인번호", 100);
			this._flex국세청.SetCol("DT_START", "작성일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex국세청.SetCol("DT_SEND", "전송일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex국세청.SetCol("NO_COMPANY1", "사업자등록번호", 100);
			this._flex국세청.SetCol("NM_CEO1", "대표자", 100);
			this._flex국세청.SetCol("NM_COMPANY1", "상호", 100);
			this._flex국세청.SetCol("AM_TAXSTD", "공급가액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex국세청.SetCol("AM_ADDTAX", "세액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex국세청.SetCol("AM_SUM", "합계", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex국세청.SetCol("TP_TAX", "유형", 100);
			this._flex국세청.SetCol("NM_TAX", "구분", 100);
			this._flex국세청.SetCol("TP_BAL", "발급구분", 100);
			this._flex국세청.SetCol("NM_BIGO", "비고", 100);
			this._flex국세청.SetCol("SELL_DAM_EMAIL", "공급자메일", 100);
			this._flex국세청.SetCol("BUY_DAM_EMAIL", "메일주소1", 100);
			this._flex국세청.SetCol("BUY_DAM_EMAIL2", "메일주소2", 100);
			this._flex국세청.SetCol("NM_ITEM", "품목비고", 100);

			this._flex국세청.SettingVersion = "0.0.0.1";
			this._flex국세청.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion

			#region ERP
			this._flexERP.BeginSetting(1, 1, false);

			this._flexERP.SetCol("NO_TAX", "매입번호", 100);
			this._flexERP.SetCol("NO_DOCU", "전표번호", 100);
			this._flexERP.SetCol("NO_DOLINE", "전표라인", 100);
			this._flexERP.SetCol("DT_START", "계산서일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexERP.SetCol("LN_PARTNER", "거래처명", 100);
			this._flexERP.SetCol("AM_TAXSTD", "공급가액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexERP.SetCol("AM_ADDTAX", "부가세", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexERP.SetCol("NM_TAX", "과세구분", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

			this._flexERP.SettingVersion = "0.0.0.1";
			this._flexERP.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion
		}

		private void InitEvent()
		{
			this._flex차이.AfterRowChange += _flex차이_AfterRowChange;
			this._flex차이.AfterEdit += _flex차이_AfterEdit;
			this._flexERP.MouseDoubleClick += _flexERP_MouseDoubleClick;

			this.btn첨부파일.Click += Btn첨부파일_Click;
			this.btn세금계산서가져오기.Click += Btn세금계산서가져오기_Click;
		}

		private void _flex차이_AfterEdit(object sender, RowColEventArgs e)
		{
			FlexGrid flexGrid;
			string columnName, query;

			try
			{
				flexGrid = ((FlexGrid)sender);

				if (flexGrid.HasNormalRow == false) return;

				columnName = flexGrid.Cols[e.Col].Name;

				switch (columnName)
				{
					case "DC_RMK":
						query = @"IF EXISTS (SELECT 1 
	       FROM CZ_FI_ETAX_DIFF_RPT ED
	       WHERE ED.CD_COMPANY = '{0}'
           AND ED.NO_ETAX = '{1}')
BEGIN
    UPDATE ED
    SET ED.DC_RMK = '{2}',
        ED.ID_UPDATE = '{3}',
        ED.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
    FROM CZ_FI_ETAX_DIFF_RPT ED
    WHERE ED.CD_COMPANY = '{0}'
    AND ED.NO_ETAX = '{1}'
END
ELSE
BEGIN
    INSERT INTO CZ_FI_ETAX_DIFF_RPT
    (
    	CD_COMPANY,
        NO_ETAX,
        DC_RMK,
        ID_INSERT,
        DTS_INSERT
    )
    VALUES
    (
        '{0}',
        '{1}',
        '{2}',
        '{3}',
        NEOE.SF_SYSDATE(GETDATE())
    )
END";

						DBHelper.ExecuteScalar(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																				   flexGrid["NO_ETAX"].ToString(),
																				   flexGrid["DC_RMK"].ToString(),
																				   Global.MainFrame.LoginInfo.UserID }));
						break;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flexERP_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			try
			{
				if (this._flexERP.HasNormalRow == false) return;
				if (this._flexERP.MouseRow < this._flexERP.Rows.Fixed) return;

				if (this._flexERP.ColSel == this._flexERP.Cols["NO_DOCU"].Index)
				{
					if (string.IsNullOrEmpty(this._flexERP["NO_DOCU"].ToString()))
						return;

					this.CallOtherPageMethod("P_FI_DOCU", "전표입력(" + this.PageName + ")", "P_FI_DOCU", this.Grant, new object[] { D.GetString(this._flexERP["NO_DOCU"]),
																																	 "1",
																																	 D.GetString(this._flexERP["CD_PC"]),
																																	 Global.MainFrame.LoginInfo.CompanyCode });
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void Btn세금계산서가져오기_Click(object sender, EventArgs e)
		{
			DataRow[] dataRowArray;

			try
			{
				if (!this._flex차이.HasNormalRow) return;

				dataRowArray = this._flex차이.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else if (this._flex차이.DataTable.Select("S = 'Y' AND YN_AUTO = 'Y'").Length > 0)
				{
					this.ShowMessage("자동대상이 선택되어 있습니다.");
					return;
				}
				else
				{
					string query = @"INSERT INTO CZ_RPA_WORK_QUEUE 
(
	CD_COMPANY,
	CD_RPA,
	NO_FILE,
	CD_PARTNER,
	YN_READ,
	YN_DONE,
	NO_BOTS,
	URGENT,
	ID_INSERT,
	DTS_INSERT	
)
VALUES
(
	'{0}',
	'HOMETAX_SINGLE',
	'{1}',
	NULL,
	'N',
	'N',
	'8',
	'3',
	'SYSTEM',
	NEOE.SF_SYSDATE(GETDATE())
)";

					foreach (DataRow dr in dataRowArray)
					{
						DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
																	dr["NO_ETAX"].ToString()));
					}

					this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn세금계산서가져오기.Text);
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn첨부파일_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flex국세청.HasNormalRow) return;
				if (this._flex국세청["YN_AUTO"].ToString() != "Y") return;

				P_CZ_MA_FILE_SUB dialog = new P_CZ_MA_FILE_SUB(Global.MainFrame.LoginInfo.CompanyCode, "CZ", "P_CZ_PU_IV_AUTO_MNG", this._flex국세청["NO_ETAX"].ToString(), "P_CZ_PU_IV_AUTO_MNG" + "/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + this._flex국세청["NO_ETAX"].ToString().Substring(0, 4));
				dialog.ShowDialog();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flex차이_AfterRowChange(object sender, RangeEventArgs e)
		{
			DataTable dtL, dtR;
			string 사업자등록번호, 작성일자, 계산서유형;

			try
			{
				사업자등록번호 = this._flex차이["NO_COMPANY1"].ToString();
				작성일자 = this._flex차이["DT_START"].ToString();
				계산서유형 = this._flex차이["TP_ETAX"].ToString();

				dtL = this._biz.SearchDetailL(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
															 작성일자,
															 사업자등록번호,
															 계산서유형 });

				dtR = this._biz.SearchDetailR(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
															 작성일자,
															 사업자등록번호,
															 계산서유형 });

				this._flex국세청.Binding = dtL;
				this._flexERP.Binding = dtR;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		protected override void InitPaint()
		{
			base.InitPaint();

			DateTime 전월 = Global.MainFrame.GetDateTimeToday().AddMonths(-1);

			this.dtp작성일자.StartDateToString = 전월.ToString("yyyyMM") + "01";
			this.dtp작성일자.EndDateToString = 전월.ToString("yyyyMM") + DateTime.DaysInMonth(전월.Year, 전월.Month).ToString();
		}

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarSearchButtonClicked(sender, e);

				this._flex차이.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																		 this.dtp작성일자.StartDateToString,
																		 this.dtp작성일자.EndDateToString,
																		 (this.chk차이건만.Checked == true ? "Y" : "N"),
																	     this.txt승인번호.Text });

				if (!this._flex차이.HasNormalRow)
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

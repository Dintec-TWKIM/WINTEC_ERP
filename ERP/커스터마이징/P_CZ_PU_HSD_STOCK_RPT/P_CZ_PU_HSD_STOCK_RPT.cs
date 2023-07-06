using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.OLD;
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
	public partial class P_CZ_PU_HSD_STOCK_RPT : PageBase
	{
		P_CZ_PU_HSD_STOCK_RPT_BIZ _biz = new P_CZ_PU_HSD_STOCK_RPT_BIZ();

		public P_CZ_PU_HSD_STOCK_RPT()
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
			#region ERP 기준
			this._flexERP기준.BeginSetting(1, 1, false);

			this._flexERP기준.SetCol("DC1", "Order No.", 100);
			this._flexERP기준.SetCol("NO_ORDER", "PJT No.", 100);
			this._flexERP기준.SetCol("NO_FILE", "수주번호", 100);
			this._flexERP기준.SetCol("CD_ITEM", "품목코드", 100);
			this._flexERP기준.SetCol("DT_SO", "수주일자", 100, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexERP기준.SetCol("QT_SO", "판매수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexERP기준.SetCol("NM_PARTNER", "판매처", 100, true);
			this._flexERP기준.SetCol("NM_VESSEL", "선박정보", 100, true);
			this._flexERP기준.SetCol("NM_MODEL", "Engine Type", 100, true);
			this._flexERP기준.SetCol("NO_PO_PARTNER", "PO REF.No", 100, true);
			this._flexERP기준.SetCol("YN_LOSS", "분실여부", 100, false, CheckTypeEnum.Y_N);
			this._flexERP기준.SetCol("YN_SUPPLIER", "판매처확인대상", 100);
			this._flexERP기준.SetCol("DC_RMK", "Remark", 100);

			this._flexERP기준.SettingVersion = "0.0.0.1";
			this._flexERP기준.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion

			#region HSD 등록
			this._flexHSD등록.BeginSetting(1, 1, false);

			this._flexHSD등록.SetCol("DC1", "Order No.", 100);
			this._flexHSD등록.SetCol("NO_ORDER", "PJT No.", 100);
			this._flexHSD등록.SetCol("NO_FILE", "수주번호", 100);
			this._flexHSD등록.SetCol("CD_ITEM", "품목코드", 100);
			this._flexHSD등록.SetCol("DT_SO", "수주일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexHSD등록.SetCol("QT_SO", "판매수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexHSD등록.SetCol("QT_MINUS", "취소수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexHSD등록.SetCol("NM_PARTNER", "판매처", 100, true);
			this._flexHSD등록.SetCol("NM_VESSEL", "선박정보", 100, true);
			this._flexHSD등록.SetCol("NM_MODEL", "Engine Type", 100, true);
			this._flexHSD등록.SetCol("NO_PO_PARTNER", "PO REF.No", 100, true);
			this._flexHSD등록.SetCol("YN_LOSS", "분실여부", 100, false, CheckTypeEnum.Y_N);
			this._flexHSD등록.SetCol("DC_RMK", "Remark", 100);

			this._flexHSD등록.SettingVersion = "0.0.0.1";
			this._flexHSD등록.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion

		}

		private void InitEvent()
		{
			this.btn판매데이터업로드.Click += Btn판매데이터업로드_Click;
			this.btn자동등록.Click += Btn자동등록_Click;

			this._flexERP기준.AfterEdit += _flex_AfterEdit;
		}

		private void _flex_AfterEdit(object sender, RowColEventArgs e)
		{
			FlexGrid flexGrid;
			string columnName;

			try
			{
				flexGrid = ((FlexGrid)sender);

				if (flexGrid.HasNormalRow == false) return;

				columnName = flexGrid.Cols[e.Col].Name;

				switch (columnName)
				{
					case "DT_SO":
					case "NM_PARTNER":
					case "NM_VESSEL":
					case "NM_MODEL":
					case "NO_PO_PARTNER":
						DBHelper.ExecuteNonQuery("SP_CZ_PU_HSD_STOCK_RPT_U", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																						    flexGrid["NO_FILE"].ToString(),
																						    flexGrid["NO_LINE"].ToString(),
																							flexGrid["DT_SO"].ToString(),
																							flexGrid["NO_PO_PARTNER"].ToString(),
																							flexGrid["NM_PARTNER"].ToString(),
																							flexGrid["NM_VESSEL"].ToString(),
																							flexGrid["NM_MODEL"].ToString(),
																							Global.MainFrame.LoginInfo.UserID });
						break;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn자동등록_Click(object sender, EventArgs e)
		{
			try
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
	'HSD_SALES',
	'HSD_SALES',
	NULL,
	'N',
	'N',
	'7',
	'3',
	'SYSTEM',
	NEOE.SF_SYSDATE(GETDATE())
)";

				DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode));

				this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn자동등록.Text);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		protected override void InitPaint()
		{
			base.InitPaint();

			this.dtp수주일자.StartDateToString = Global.MainFrame.GetDateTimeToday().AddMonths(-1).ToString("yyyyMM") + "16";
			this.dtp수주일자.EndDateToString = Global.MainFrame.GetDateTimeToday().ToString("yyyyMM") + "15";

			this.cbo취소여부.DataSource = MA.GetCodeUser(new string[] { "", "Y", "N" }, new string[] { "", "Y", "N" });
			this.cbo취소여부.DisplayMember = "NAME";
			this.cbo취소여부.ValueMember = "CODE";
		}

		private void Btn판매데이터업로드_Click(object sender, EventArgs e)
		{
			try
			{
				OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "엑셀 파일 (*.xls)|*.xls";
                 
				if (openFileDialog.ShowDialog() != DialogResult.OK) return;
                 
				DataTable dt = new Excel().StartLoadExcel(openFileDialog.FileName, 0, 1);
                if (dt == null || dt.Rows.Count == 0) return;

				if (this._biz.SaveExcel(dt))
					this.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { this.btn판매데이터업로드.Text });
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

				if (this.tabControlExt1.SelectedTab == this.tpgERP기준)
				{
					this._flexERP기준.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																			    this.dtp수주일자.StartDateToString,
																			    this.dtp수주일자.EndDateToString,
																			    this.cbo취소여부.SelectedValue,
																			    (this.chk판매처확인대상.Checked == true ? "Y" : "N"),
																			    (this.chk입력된건제외.Checked == true ? "Y" : "N") });

					if (!this._flexERP기준.HasNormalRow)
					{
						this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
					}
				}
				else
				{
					this._flexHSD등록.Binding = this._biz.Search1(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																				this.dtp수주일자.StartDateToString,
																				this.dtp수주일자.EndDateToString,
																				this.cbo취소여부.SelectedValue });

					if (!this._flexHSD등록.HasNormalRow)
					{
						this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
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

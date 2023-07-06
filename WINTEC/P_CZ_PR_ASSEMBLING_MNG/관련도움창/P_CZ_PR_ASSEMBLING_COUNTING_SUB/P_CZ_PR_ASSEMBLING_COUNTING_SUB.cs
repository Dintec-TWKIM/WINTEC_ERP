using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.Grant;
using DX;
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
	public partial class P_CZ_PR_ASSEMBLING_COUNTING_SUB : Duzon.Common.Forms.CommonDialog
	{
		P_CZ_PR_ASSEMBLING_COUNTING_SUB_BIZ _biz = new P_CZ_PR_ASSEMBLING_COUNTING_SUB_BIZ();
		public P_CZ_PR_ASSEMBLING_COUNTING_SUB()
		{
			InitializeComponent();
		}

		protected override void InitLoad()
		{
			base.InitLoad();

			this.InitEvent();
			this.InitGrid();
		}

		protected override void InitPaint()
		{
			base.InitPaint();

			UGrant ugrant = new UGrant();
			ugrant.GrantButtonEnble("P_CZ_PR_ASSEMBLING_MNG", "ADMIN", this.btn실사취소, true);
		}

		private void InitGrid()
		{
			#region 재고실사
			this._flex재고실사.BeginSetting(1, 1, false);

			this._flex재고실사.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
			this._flex재고실사.SetCol("NO_WO", "작업지시번호", 100);
			this._flex재고실사.SetCol("NO_ID", "ID번호", 100);
			this._flex재고실사.SetCol("DTS_COUNTING", "실사일자", 150, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex재고실사.Cols["DTS_COUNTING"].Format = "####/##/##/##:##:##";
			this._flex재고실사.SetCol("SEQ_WO", "순번", 100);
			this._flex재고실사.SetCol("NO_HEAT", "소재HEAT번호", 100);
			this._flex재고실사.SetCol("NO_LOT", "열처리LOT번호", 100);
			this._flex재고실사.SetCol("NO_SO", "수주번호", 100);

			this._flex재고실사.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.None);
			#endregion
		}

		private void InitEvent()
		{
			this.btn조회.Click += Btn조회_Click;
			this.btn대기.Click += Btn대기_Clcik;
			this.btn재고실사.Click += Btn재고실사_Click;
			this.btn실사취소.Click += Btn실사취소_Click;

			this.txtQR코드스캔.KeyDown += TxtQR코드스캔_KeyDown;

			this.ctx품목.QueryBefore += Ctx품목_QueryBefore;
		}

		private void Ctx품목_QueryBefore(object sender, BpQueryArgs e)
		{
			try
			{
				e.HelpParam.P09_CD_PLANT = Global.MainFrame.LoginInfo.CdPlant;
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void TxtQR코드스캔_KeyDown(object sender, KeyEventArgs e)
		{
			DataRow[] dataRowArray;
			try
			{
				if (e.KeyData == Keys.Enter)
				{
					if (string.IsNullOrEmpty(this.txtQR코드스캔.Text))
					{
						Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, "ID번호");
						return;
					}

					dataRowArray = this._flex재고실사.DataTable.Select(string.Format("NO_ID = '{0}'", this.txtQR코드스캔.Text));

					if (dataRowArray == null || dataRowArray.Length == 0)
					{
						Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
						return;
					}
					else
					{
						foreach (DataRow dr in dataRowArray)
						{
							dr["S"] = "Y";
						}

						this._flex재고실사.AcceptChanges();
					}

					this.txtQR코드스캔.Text = string.Empty;
					this.txtQR코드스캔.Focus();
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void Btn조회_Click(object sender, EventArgs e)
		{
			string query;
			DataTable dt;
			try
			{
				if (string.IsNullOrEmpty(this.ctx품목.CodeValue) &&
						string.IsNullOrEmpty(this.txtID번호.Text))
				{
					Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl품목.Text);
					return;
				}
				else if (!string.IsNullOrEmpty(this.txtID번호.Text))
				{
					query = @"SELECT WR.CD_ITEM, MP.NM_ITEM
FROM CZ_PR_WO_REQ_D WR
LEFT JOIN MA_PITEM MP ON MP.CD_COMPANY = WR.CD_COMPANY AND MP.CD_ITEM = WR.CD_ITEM
WHERE WR.CD_COMPANY = '{0}'
AND WR.NO_ID = '{1}'";
					dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, this.txtID번호.Text));

					if (dt != null && dt.Rows.Count > 0)
					{
						this.ctx품목.CodeValue = dt.Rows[0]["CD_ITEM"].ToString();
						this.ctx품목.CodeName = dt.Rows[0]["NM_ITEM"].ToString();
					}
					else
					{
						query = @"SELECT IO.CD_ITEM, MP.NM_ITEM
FROM CZ_PR_ASSEMBLING_ID_OLD IO
LEFT JOIN MA_PITEM MP ON MP.CD_COMPANY = IO.CD_COMPANY AND MP.CD_ITEM = IO.CD_ITEM
WHERE IO.CD_COMPANY = '{0}'
AND IO.NO_ID = '{1}'";
						dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, this.txtID번호.Text));

						if (dt != null && dt.Rows.Count > 0)
						{
							this.ctx품목.CodeValue = dt.Rows[0]["CD_ITEM"].ToString();
							this.ctx품목.CodeName = dt.Rows[0]["NM_ITEM"].ToString();
						}
						else
						{
							query = @"SELECT IO.CD_ITEM, MP.NM_ITEM
FROM CZ_PR_MATCHING_ID_OLD IO
LEFT JOIN MA_PITEM MP ON MP.CD_COMPANY = IO.CD_COMPANY AND MP.CD_ITEM = IO.CD_ITEM
WHERE IO.CD_COMPANY = '{0}'
AND IO.NO_ID = '{1}'";
							dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, this.txtID번호.Text));

							if (dt != null && dt.Rows.Count > 0)
							{
								this.ctx품목.CodeValue = dt.Rows[0]["CD_ITEM"].ToString();
								this.ctx품목.CodeName = dt.Rows[0]["NM_ITEM"].ToString();
							}
						}
					}
				}
				this._flex재고실사.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																			this.ctx품목.CodeValue,
																			this.chk사용유무.Checked ? "Y" : "N"});

				if (!this._flex재고실사.HasNormalRow)
				{
					Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void Btn대기_Clcik(object sender, EventArgs e)
		{
			DataTable dt, tmpDt;
			DataRow drTemp;
			DataRow[] dataRowArray;
			string query;
			try
			{
				if (!this._flex재고실사.HasNormalRow) return;

				dataRowArray = this._flex재고실사.DataTable.Select("S = 'Y'");

				dt = new DataTable();

				dt.Columns.Add("CD_COMPANY");
				dt.Columns.Add("CD_PLANT");
				dt.Columns.Add("NO_ID");
				dt.Columns.Add("STA_DEACTIVATE");
				dt.Columns.Add("DC_RMK");

				if (dataRowArray == null && dataRowArray.Length == 0)
				{
					Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					foreach (DataRow dr in dataRowArray)
					{
						drTemp = dt.NewRow();

						drTemp["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
						drTemp["CD_PLANT"] = Global.MainFrame.LoginInfo.CdPlant;
						drTemp["NO_ID"] = dr["NO_ID"].ToString();
						drTemp["STA_DEACTIVATE"] = "001"; // 정상
						drTemp["DC_RMK"] = string.Empty;

						dt.Rows.Add(drTemp);

						dr.Delete();
					}
				}

				if (dt.Rows.Count > 0)
				{
					query = @"SELECT 1
FROM CZ_PR_MATCHING_ITEM
WHERE CD_COMPANY = '{0}'
AND CD_PLANT = '{1}'
AND CD_PITEM = '{2}'";
					tmpDt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, Global.MainFrame.LoginInfo.CdPlant, this.ctx품목.CodeValue));
					if (tmpDt.Rows.Count > 0)
					{
						//현합
						DBHelper.ExecuteNonQuery("SP_CZ_PR_MATCHING_DEACTIVATE_SUB_JSON", new object[] { dt.Json(), Global.MainFrame.LoginInfo.UserID });
					}
					else
					{
						//조립
						DBHelper.ExecuteNonQuery("SP_CZ_PR_ASSEMBLING_DEACTIVATE_SUB_JSON", new object[] { dt.Json(), Global.MainFrame.LoginInfo.UserID });
					}

					Global.MainFrame.ShowMessage(공통메세지._작업을완료하였습니다, this.btn대기.Text);
					this.Btn조회_Click(null, null);
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void Btn재고실사_Click(object sender, EventArgs e)
		{
			DataTable dt;
			DataRow[] dataRowArray;
			DataRow drTemp;
			try
			{
				if (!this._flex재고실사.HasNormalRow) return;

				if (this._flex재고실사.DataTable.Select("S = 'Y'").Length == 0)
				{
					Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else if (this._flex재고실사.DataTable.Select("S = 'Y' AND ISNULL(DTS_COUNTING, '') <> ''").Length > 0)
				{
					Global.MainFrame.ShowMessage("이미 재고실사 된 건이 선택되어 있습니다.");
					return;
				}
				else
				{
					dataRowArray = this._flex재고실사.DataTable.Select("S = 'Y'");

					dt = new DataTable();


					dt.Columns.Add("CD_COMPANY");
					dt.Columns.Add("NO_WO");
					dt.Columns.Add("SEQ_WO");
					dt.Columns.Add("NO_ID");
					dt.Columns.Add("DTS_COUNTING");

					foreach (DataRow dr in dataRowArray)
					{
						drTemp = dt.NewRow();

						drTemp["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
						drTemp["NO_WO"] = dr["NO_WO"].ToString();
						drTemp["SEQ_WO"] = dr["SEQ_WO"].ToString();
						drTemp["NO_ID"] = dr["NO_ID"].ToString();
						drTemp["DTS_COUNTING"] = dr["DTS_COUNTING"].ToString();

						dt.Rows.Add(drTemp);
					}

					if (dt.Rows.Count > 0)
					{
						DBHelper.ExecuteNonQuery("SP_CZ_PR_ASSEMBLING_COUNTING_SUB_JSON", new object[] { dt.Json(), Global.MainFrame.LoginInfo.UserID });
					}

					Global.MainFrame.ShowMessage(공통메세지._작업을완료하였습니다, this.btn재고실사.Text);
					this.Btn조회_Click(null, null);
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void Btn실사취소_Click(object sender, EventArgs e)
		{
			try
			{
				DataTable dt;
				DataRow[] dataRowArray;
				DataRow drTemp;
				try
				{
					if (!this._flex재고실사.HasNormalRow) return;

					if (this._flex재고실사.DataTable.Select("S = 'Y'").Length == 0)
					{
						Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
						return;
					}
					else if (this._flex재고실사.DataTable.Select("S = 'Y' AND ISNULL(DTS_COUNTING, '') = ''").Length > 0)
					{
						Global.MainFrame.ShowMessage("재고실사 되지 않은 건이 선택되어 있습니다.");
						return;
					}
					else
					{
						dataRowArray = this._flex재고실사.DataTable.Select("S = 'Y'");

						dt = new DataTable();

						dt.Columns.Add("CD_COMPANY");
						dt.Columns.Add("NO_WO");
						dt.Columns.Add("SEQ_WO");
						dt.Columns.Add("NO_ID");
						dt.Columns.Add("DTS_COUNTING");

						foreach (DataRow dr in dataRowArray)
						{
							drTemp = dt.NewRow();

							drTemp["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
							drTemp["NO_WO"] = dr["NO_WO"].ToString();
							drTemp["SEQ_WO"] = dr["SEQ_WO"].ToString();
							drTemp["NO_ID"] = dr["NO_ID"].ToString();
							drTemp["DTS_COUNTING"] = dr["DTS_COUNTING"].ToString();

							dt.Rows.Add(drTemp);
						}

						dt.AcceptChanges();

						foreach (DataRow dr in dt.Rows) dr.Delete();

						if (dt.Rows.Count > 0)
						{
							DBHelper.ExecuteNonQuery("SP_CZ_PR_ASSEMBLING_COUNTING_SUB_JSON", new object[] { dt.Json(), Global.MainFrame.LoginInfo.UserID });
						}

						Global.MainFrame.ShowMessage(공통메세지._작업을완료하였습니다, this.btn실사취소.Text);
						this.Btn조회_Click(null, null);
					}
				}
				catch (Exception ex)
				{
					Global.MainFrame.MsgEnd(ex);
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}
	}
}

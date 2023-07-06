using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.Grant;
using DX;
using System;
using System.Data;
using System.Windows.Forms;

namespace cz
{
	public partial class P_CZ_PR_ASSEMBLING_SA_SOL_SUB : Duzon.Common.Forms.CommonDialog
	{
		P_CZ_PR_ASSEMBLING_SA_SOL_SUB_BIZ _biz = new P_CZ_PR_ASSEMBLING_SA_SOL_SUB_BIZ();

		public P_CZ_PR_ASSEMBLING_SA_SOL_SUB()
		{
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

			this.dtp납기일자.StartDateToString = Global.MainFrame.GetDateTimeToday().AddMonths(-6).ToString("yyyyMMdd");
			this.dtp납기일자.EndDateToString = Global.MainFrame.GetDateTimeToday().AddMonths(1).ToString("yyyyMMdd");

			UGrant ugrant = new UGrant();
			ugrant.GrantButtonEnble("P_CZ_PR_ASSEMBLING_MNG", "ADMIN", this.btn판매해제, true);
		}

		private void InitGrid()
		{
			#region 수주품목
			this._flex수주품목.BeginSetting(1, 1, false);

			this._flex수주품목.SetCol("NO_SO", "수주번호", 100);
			this._flex수주품목.SetCol("DT_DUEDATE", "납기일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex수주품목.SetCol("SEQ_SO", "수주항번", 100);
			this._flex수주품목.SetCol("CD_ITEM", "품목코드", 100);
			this._flex수주품목.SetCol("NM_ITEM", "품목명", 100);
			this._flex수주품목.SetCol("QT_SO", "수주수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex수주품목.SetCol("QT_SALE", "판매수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);

			this._flex수주품목.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.None);
			#endregion

			#region 생산품목
			this._flex생산품목.BeginSetting(1, 1, false);

			this._flex생산품목.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
			this._flex생산품목.SetCol("NO_WO", "작업지시번호", 100);
			this._flex생산품목.SetCol("SEQ_WO", "순번", 100);
			this._flex생산품목.SetCol("NO_HEAT", "소재HEAT번호", 100);
			this._flex생산품목.SetCol("NO_ID", "ID번호", 100);
			this._flex생산품목.SetCol("NO_LOT", "열처리LOT번호", 100);
			this._flex생산품목.SetCol("NO_SO", "수주번호", 100);

			this._flex생산품목.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.None);
			#endregion
		}

		private void InitEvent()
		{
			this.btn조회.Click += Btn조회_Click;
			this.btn판매처리.Click += Btn판매처리_Click;
			this.btn판매해제.Click += Btn판매해제_Click;
			this.txtQR코드스캔.KeyDown += TxtQR코드스캔_KeyDown;

			this._flex수주품목.AfterRowChange += _flex수주품목_AfterRowChange;
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

					dataRowArray = this._flex생산품목.DataTable.Select(string.Format("NO_ID = '{0}'", this.txtQR코드스캔.Text));

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

						this._flex생산품목.AcceptChanges();
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

		private void Btn판매해제_Click(object sender, EventArgs e)
		{
			DataTable dt;
			DataRow[] dataRowArray;
			DataRow drTemp;

			try
			{
				if (!this._flex생산품목.HasNormalRow) return;

				dataRowArray = this._flex생산품목.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else if (this._flex생산품목.DataTable.Select("S = 'Y' AND ISNULL(NO_SO, '') = ''").Length > 0)
				{
					Global.MainFrame.ShowMessage("판매처리 되지 않은 건이 선택되어 있습니다.");
					return;
				}
				else
				{
					dt = new DataTable();

					dt.Columns.Add("CD_COMPANY");
					dt.Columns.Add("CD_PLANT");
					dt.Columns.Add("NO_SO");
					dt.Columns.Add("SEQ_SO");
					dt.Columns.Add("NO_WO");
					dt.Columns.Add("SEQ_WO");
					dt.Columns.Add("NO_ID");

					foreach (DataRow dr in dataRowArray)
					{
						drTemp = dt.NewRow();

						drTemp["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
						drTemp["CD_PLANT"] = Global.MainFrame.LoginInfo.CdPlant;
						drTemp["NO_SO"] = this._flex수주품목["NO_SO"].ToString();
						drTemp["SEQ_SO"] = this._flex수주품목["SEQ_SO"].ToString();
						drTemp["NO_WO"] = dr["NO_WO"].ToString();
						drTemp["SEQ_WO"] = dr["SEQ_WO"].ToString();
						drTemp["NO_ID"] = dr["NO_ID"].ToString();

						dt.Rows.Add(drTemp);	
					}

					dt.AcceptChanges();

					foreach (DataRow dr in dt.Rows)
					{
						dr.Delete();
					}

					if (dt.Rows.Count > 0)
					{
						DBHelper.ExecuteNonQuery("SP_CZ_PR_ASSEMBLING_SA_SOL_SUB_JSON", new object[] { dt.GetChanges().Json(), Global.MainFrame.LoginInfo.UserID });
					}

					Global.MainFrame.ShowMessage(공통메세지._작업을완료하였습니다, this.btn판매해제.Text);
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void Btn판매처리_Click(object sender, EventArgs e)
		{
			DataTable dt, dt1;
			DataRow[] dataRowArray;
			DataRow drTemp;
			decimal 수주수량, 기판매수량;
			string query;

			try
			{
				if (!this._flex생산품목.HasNormalRow) return;

				dataRowArray = this._flex생산품목.DataTable.Select("S = 'Y'", "NO_WO, SEQ_WO");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else if (this._flex생산품목.DataTable.Select("S = 'Y' AND ISNULL(NO_SO, '') <> ''").Length > 0)
				{
					Global.MainFrame.ShowMessage("판매처리 된 건이 선택되어 있습니다.");
					return;
				}
				else
				{
					dt = new DataTable();

					dt.Columns.Add("CD_COMPANY");
					dt.Columns.Add("CD_PLANT");
					dt.Columns.Add("NO_SO");
					dt.Columns.Add("SEQ_SO");
					dt.Columns.Add("NO_WO");
					dt.Columns.Add("SEQ_WO");
					dt.Columns.Add("CD_ITEM");
					dt.Columns.Add("NO_ID");

					query = @"SELECT SL.QT_SO,
       SL1.QT_SALE
FROM SA_SOL SL WITH(NOLOCK)
LEFT JOIN (SELECT SL.CD_COMPANY, SL.CD_PLANT, SL.NO_SO, SL.SEQ_SO,
                  COUNT(1) AS QT_SALE
           FROM CZ_PR_ASSEMBLING_SA_SOL SL WITH(NOLOCK)
           GROUP BY SL.CD_COMPANY, SL.CD_PLANT, SL.NO_SO, SL.SEQ_SO) SL1
ON SL1.CD_COMPANY = SL.CD_COMPANY AND SL1.CD_PLANT = SL.CD_PLANT AND SL1.NO_SO = SL.NO_SO AND SL1.SEQ_SO = SL.SEQ_SO
WHERE SL.CD_COMPANY = '{0}'
AND SL.NO_SO = '{1}'
AND SL.SEQ_SO = '{2}'";

					dt1 = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 
																	 this._flex수주품목["NO_SO"].ToString(), 
																	 this._flex수주품목["SEQ_SO"].ToString()));

					수주수량 = D.GetDecimal(dt1.Rows[0]["QT_SO"]);
					기판매수량 = D.GetDecimal(dt1.Rows[0]["QT_SALE"]);

					if (수주수량 < (기판매수량 + dataRowArray.Length))
					{
						Global.MainFrame.ShowMessage(공통메세지._은_보다작거나같아야합니다, "판매수량", "수주수량");
						return;
					}

					foreach (DataRow dr in dataRowArray)
					{
						drTemp = dt.NewRow();

						drTemp["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
						drTemp["CD_PLANT"] = Global.MainFrame.LoginInfo.CdPlant;
						drTemp["NO_SO"] = this._flex수주품목["NO_SO"].ToString();
						drTemp["SEQ_SO"] = this._flex수주품목["SEQ_SO"].ToString();
						drTemp["NO_WO"] = dr["NO_WO"].ToString();
						drTemp["SEQ_WO"] = dr["SEQ_WO"].ToString();
						drTemp["CD_ITEM"] = this._flex수주품목["CD_ITEM"].ToString();
						drTemp["NO_ID"] = dr["NO_ID"].ToString();

						dt.Rows.Add(drTemp);
					}

					if (dt.Rows.Count > 0)
					{
						DBHelper.ExecuteNonQuery("SP_CZ_PR_ASSEMBLING_SA_SOL_SUB_JSON", new object[] { dt.GetChanges().Json(), Global.MainFrame.LoginInfo.UserID });
					}

					Global.MainFrame.ShowMessage(공통메세지._작업을완료하였습니다, this.btn판매처리.Text);
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void Btn조회_Click(object sender, EventArgs e)
		{
			try
			{
				this._flex수주품목.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																			 this.txt수주번호.Text,
																			 this.txtID번호.Text,
																			 this.dtp납기일자.StartDateToString,
																			 this.dtp납기일자.EndDateToString,
																		     (this.chk의뢰제외.Checked ? "Y" : "N"),
																			 (this.chk출고제외.Checked ? "Y" : "N"),
																			 (this.chk종결제외.Checked ? "Y" : "N") });
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void _flex수주품목_AfterRowChange(object sender, RangeEventArgs e)
		{
			try
			{
				if (!this._flex수주품목.HasNormalRow) return;

				this._flex생산품목.Binding = this._biz.Search1(new object[] { Global.MainFrame.LoginInfo.CompanyCode, 
																			  this._flex수주품목["CD_ITEM"].ToString(),
																			  this.txtID번호.Text,
																			  (this.chk판매품목제외.Checked == true ? "Y" : "N") });
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}
	}
}

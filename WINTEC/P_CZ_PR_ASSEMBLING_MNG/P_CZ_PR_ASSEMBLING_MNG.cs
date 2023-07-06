using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.MF;
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
	public partial class P_CZ_PR_ASSEMBLING_MNG : PageBase
	{
		P_CZ_PR_ASSEMBLING_MNG_BIZ _biz = new P_CZ_PR_ASSEMBLING_MNG_BIZ();
		private string 지시품목;

		public P_CZ_PR_ASSEMBLING_MNG()
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
			this._flex작업지시.DetailGrids = new FlexGrid[] { this._flex수주번호 };

			#region 조립

			#region 작업지시
			this._flex작업지시.BeginSetting(2, 1, false);

			this._flex작업지시.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
			this._flex작업지시.SetCol("NO_WO", "작업지시번호", 100);
			this._flex작업지시.SetCol("NM_OP", "공정명", 100);
			this._flex작업지시.SetCol("CD_ITEM", "품목코드", 100);
			this._flex작업지시.SetCol("NM_ITEM", "품목명", 100);
			this._flex작업지시.SetCol("QT_ITEM", "지시수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex작업지시.SetCol("QT_WIP", "대기수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex작업지시.SetCol("QT_ASSEMBLING", "조립수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex작업지시.SetCol("QT_WORK", "작업수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);

			this._flex작업지시.AddDummyColumn("S");

			this._flex작업지시.SettingVersion = "0.0.0.1";
			this._flex작업지시.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion

			#region 수주번호
			this._flex수주번호.BeginSetting(1, 1, false);

			this._flex수주번호.SetCol("NO_SO", "수주번호", 100);
			this._flex수주번호.SetCol("DT_DUEDATE", "납기일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex수주번호.SetCol("QT_APPLY", "지시수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex수주번호.SetCol("QT_INSP", "조립수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex수주번호.SetCol("NM_VESSEL", "호선번호", 100);

			this._flex수주번호.SettingVersion = "0.0.0.1";
			this._flex수주번호.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion

			#region 조립
			this._flex조립.BeginSetting(2, 1, false);

			this._flex조립.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
			this._flex조립.SetCol("NO_WO", "작업지시번호", 100);
			this._flex조립.SetCol("SEQ_WO", "순번", 100);
			this._flex조립.SetCol("NO_ID", "가공ID", 100);
			
			this._flex조립.AddDummyColumn("S");
			this._flex조립.KeyActionEnter = KeyActionEnum.MoveDown;

			this._flex조립.SettingVersion = "0.0.0.1";
			this._flex조립.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion

			#region 대상품목
			this._flex대상품목.BeginSetting(1, 1, false);

			this._flex대상품목.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
			this._flex대상품목.SetCol("NO_ID", "가공ID", 100);
			this._flex대상품목.SetCol("NO_HEAT", "소재HEAT번호", 100);
			this._flex대상품목.SetCol("NO_LOT", "열처리LOT번호", 100);

			this._flex대상품목.AddDummyColumn("S");

			this._flex대상품목.SettingVersion = "0.0.0.1";
			this._flex대상품목.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion

			#region 현합품목
			this._flex현합품목.BeginSetting(1, 1, false);

			this._flex현합품목.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
			this._flex현합품목.SetCol("NO_ID", "가공ID", 100);
			this._flex현합품목.SetCol("001", "1번품목", 100);
			this._flex현합품목.SetCol("002", "2번품목", 100);
			this._flex현합품목.SetCol("003", "3번품목", 100);
			this._flex현합품목.SetCol("004", "4번품목", 100);
			this._flex현합품목.SetCol("005", "5번품목", 100);

			this._flex현합품목.AddDummyColumn("S");

			this._flex현합품목.SettingVersion = "0.0.0.1";
			this._flex현합품목.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion

			#endregion

			#region 현황
			this._flex현황.BeginSetting(3, 1, false);

			this._flex현황.SetCol("NO_ID", "가공ID", 100);
			
			this._flex현황.SettingVersion = "0.0.0.1";
			this._flex현황.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion
		}

		private void InitEvent()
		{
			this.btn조립품목등록.Click += Btn조립품목등록_Click;
			this.btn현합적용해제.Click += Btn조립해제_Click;
			this.btn대상품목추가.Click += Btn대상품목추가_Click;
			this.btn대기품목관리.Click += Btn대기품목관리_Click;
			this.btn단품판매등록.Click += Btn단품판매등록_Click;
			this.btn재고실사.Click += Btn재고실사_Click;

			this.btn지시적용.Click += Btn지시적용_Click;
			this.btn실적등록.Click += Btn실적등록_Click;
			this.btn수주할당.Click += Btn수주할당_Click;

			this.btn조회.Click += Btn조회_Click;
			this.btn교체.Click += Btn교체_Click;
			this.btn대기.Click += Btn대기_Click;
			this.btn적용.Click += Btn적용_Click;
			this.btn조회M.Click += Btn조회M_Click;
			this.btn대기M.Click += Btn대기M_Click;
			this.btn적용M.Click += Btn적용M_Click;

			this.btn조립완료.Click += Btn조립완료_Click;
			this.btn완료해제.Click += Btn완료해제_Click;

			this.btn번호갱신.Click += Btn번호갱신_Click;

			this.ctx모품목.QueryBefore += Ctx모품목_QueryBefore;
			this.ctx조립품목.QueryBefore += Ctx조립품목_QueryBefore;

			this._flex작업지시.AfterRowChange += _flex작업지시_AfterRowChange;
			this._flex조립.ValidateEdit += _flex조립_ValidateEdit;
		}

		private void Ctx조립품목_QueryBefore(object sender, BpQueryArgs e)
		{
			try
			{
				e.HelpParam.P09_CD_PLANT = Global.MainFrame.LoginInfo.CdPlant;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn번호갱신_Click(object sender, EventArgs e)
		{
			string query;

			try
			{
				query = @"UPDATE AD
SET AD.NO_HEAT = ID.NO_HEAT 
FROM CZ_PR_ASSEMBLING_DATA AD WITH(NOLOCK)
LEFT JOIN (SELECT WD.CD_COMPANY, WD.NO_ID,
                  ISNULL(NULLIF(WD.NO_HEAT, ''), WO.TXT_USERDEF1) AS NO_HEAT
           FROM PR_WO WO WITH(NOLOCK)
           JOIN CZ_PR_WO_REQ_D WD WITH(NOLOCK) ON WD.CD_COMPANY = WO.CD_COMPANY AND WD.NO_WO = WO.NO_WO 
           UNION ALL
           SELECT CD_COMPANY, NO_ID, NO_HEAT 
           FROM CZ_PR_ASSEMBLING_ID_OLD WITH(NOLOCK)
		   UNION ALL
           SELECT CD_COMPANY, NO_ID, NO_HEAT 
           FROM CZ_PR_MATCHING_ID_OLD WITH(NOLOCK)) ID
ON ID.CD_COMPANY = AD.CD_COMPANY AND ID.NO_ID = AD.NO_ID_C
WHERE AD.CD_COMPANY = '{0}' 
AND ISNULL(AD.NO_HEAT, '') <> ISNULL(ID.NO_HEAT, '')";

				DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode));

				query = @"UPDATE AD
SET AD.NO_LOT = ID.NO_LOT
FROM CZ_PR_ASSEMBLING_DATA AD WITH(NOLOCK)
LEFT JOIN (SELECT WD.CD_COMPANY, WD.NO_ID,
                  (SELECT MAX(WI2.NO_HEAT) AS NO_HEAT 
                   FROM CZ_PR_WO_INSP WI2 
                   WHERE WI2.CD_COMPANY = WO.CD_COMPANY 
                   AND WI2.NO_WO = WO.NO_WO  
                   AND WI2.NO_INSP IN (0, 999, 995)
                   AND WI2.SEQ_WO = WD.SEQ_WO) AS NO_LOT
           FROM PR_WO WO WITH(NOLOCK)
           JOIN CZ_PR_WO_REQ_D WD WITH(NOLOCK) ON WD.CD_COMPANY = WO.CD_COMPANY AND WD.NO_WO = WO.NO_WO 
           UNION ALL
           SELECT CD_COMPANY, NO_ID, NO_LOT 
           FROM CZ_PR_ASSEMBLING_ID_OLD WITH(NOLOCK)
		   UNION ALL
           SELECT CD_COMPANY, NO_ID, NO_LOT 
           FROM CZ_PR_MATCHING_ID_OLD WITH(NOLOCK)) ID
ON ID.CD_COMPANY = AD.CD_COMPANY AND ID.NO_ID = AD.NO_ID_C
WHERE AD.CD_COMPANY = '{0}' 
AND ISNULL(AD.NO_LOT, '') <> ISNULL(ID.NO_LOT, '')";

				DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode));

				this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn번호갱신.Text);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn완료해제_Click(object sender, EventArgs e)
		{
			DataRow[] dataRowArray;
			string query;

			try
			{
				dataRowArray = this._flex조립.DataTable.Select("S = 'Y'");

				if (dataRowArray != null && dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					query = @"UPDATE AD
SET AD.DT_ASSEMBLE = NULL
FROM CZ_PR_ASSEMBLING_DATA AD WITH(NOLOCK)
WHERE AD.CD_COMPANY = '{0}' 
AND AD.CD_PLANT = '{1}'
AND AD.NO_WO = '{2}'
AND AD.NO_LINE = '{3}'
AND AD.SEQ_WO = '{4}'";

					foreach (DataRow dr in dataRowArray)
					{
						DBHelper.ExecuteScalar(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																				   Global.MainFrame.LoginInfo.CdPlant,
																				   dr["NO_WO"].ToString(),
																				   dr["NO_LINE"].ToString(),
																				   dr["SEQ_WO"].ToString() }));

						dr["DT_ASSEMBLE"] = string.Empty;
					}

					this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn완료해제.Text);
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn조립완료_Click(object sender, EventArgs e)
		{
			DataRow[] dataRowArray;
			string query;

			try
			{
				dataRowArray = this._flex조립.DataTable.Select("S = 'Y'");

				if (dataRowArray != null && dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					query = @"UPDATE AD
SET AD.DT_ASSEMBLE = CONVERT(CHAR(8), GETDATE(), 112)
FROM CZ_PR_ASSEMBLING_DATA AD WITH(NOLOCK)
WHERE AD.CD_COMPANY = '{0}'
AND	AD.CD_PLANT = '{1}' 
AND AD.NO_WO = '{2}'
AND AD.NO_LINE = '{3}'
AND AD.SEQ_WO = '{4}'";

					foreach (DataRow dr in dataRowArray)
					{
						DBHelper.ExecuteScalar(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																				   Global.MainFrame.LoginInfo.CdPlant,
																				   dr["NO_WO"].ToString(),
																				   dr["NO_LINE"].ToString(),
																				   dr["SEQ_WO"].ToString() }));

						dr["DT_ASSEMBLE"] = Global.MainFrame.GetStringToday;
					}

					this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn조립완료.Text);
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn수주할당_Click(object sender, EventArgs e)
		{
			DataTable dt, tmpDt;
			DataRow[] dataRowArray;
			DataRow drTemp;
			string 수주번호, query;

			try
			{
				if (!this._flex조립.HasNormalRow) return;
				if (!this._flex수주번호.HasNormalRow) return;

				수주번호 = this._flex수주번호["NO_SO"].ToString();

				if (string.IsNullOrEmpty(수주번호))
				{
					this.ShowMessage("수주번호가 선택되어 있지 않습니다.");
					return;
				}

				dataRowArray = this._flex조립.DataTable.Select("S = 'Y'");

				if (dataRowArray == null && dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					#region 수주번호
					dt = new DataTable();

					dt.Columns.Add("CD_COMPANY");
					dt.Columns.Add("NO_WO");
					dt.Columns.Add("NO_LINE");
					dt.Columns.Add("SEQ_WO");
					dt.Columns.Add("NO_SO");
					dt.Columns.Add("DC_RMK");
					#endregion

					query = @"SELECT ISNULL(SUM(QT_APPLY), 0) AS QT_APPLY,
       ISNULL(SUM(WI.QT_INSP), 0) QT_INSP
FROM CZ_PR_SA_SOL_PR_WO_MAPPING SW WITH(NOLOCK)
LEFT JOIN (SELECT WI.CD_COMPANY, WI.NO_WO, WI.NO_LINE, WI.NO_SO,
                  COUNT(1) AS QT_INSP
           FROM CZ_PR_WO_INSP WI WITH(NOLOCK)
           WHERE WI.NO_INSP = 996
           GROUP BY WI.CD_COMPANY, WI.NO_WO, WI.NO_LINE, WI.NO_SO) WI
ON WI.CD_COMPANY = SW.CD_COMPANY AND WI.NO_WO = SW.NO_WO AND WI.NO_SO = SW.NO_SO
WHERE SW.CD_COMPANY = '{0}'
AND SW.NO_WO = '{1}'
AND SW.NO_SO = '{2}'";

					foreach (DataRow dr in dataRowArray)
					{
						tmpDt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, dr["NO_WO"].ToString(), 수주번호));

						if (tmpDt == null || tmpDt.Rows.Count == 0)
						{
							this.ShowMessage("잘못된 수주번호가 선택되었습니다.");
							return;
						}

						if (D.GetDecimal(tmpDt.Rows[0]["QT_APPLY"]) < D.GetDecimal(tmpDt.Rows[0]["QT_INSP"]) + dt.Select(string.Format("NO_WO = '{0}' AND NO_SO = '{1}'", dr["NO_WO"].ToString(), 수주번호)).Length + 1)
						{
							this.ShowMessage(공통메세지._은_보다작거나같아야합니다, "수주할당 수량", "작업지시 수량");
							return;
						}

						if (string.IsNullOrEmpty(dr["NO_SO"].ToString()))
						{
							dr["NO_SO"] = 수주번호;

							drTemp = dt.NewRow();

							drTemp["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
							drTemp["NO_WO"] = dr["NO_WO"].ToString();
							drTemp["NO_LINE"] = dr["NO_LINE"].ToString();
							drTemp["SEQ_WO"] = dr["SEQ_WO"].ToString();
							drTemp["NO_SO"] = 수주번호;
							drTemp["DC_RMK"] = string.Empty;

							dt.Rows.Add(drTemp);
						}
					}

					if (dt.Rows.Count > 0)
					{
						DBHelper.ExecuteNonQuery("SP_CZ_PR_ASSEMBLING_MNG_JSON1", new object[] { dt.GetChanges().Json(), Global.MainFrame.LoginInfo.UserID });
					}
				}
		    }
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn대기M_Click(object sender, EventArgs e)
		{
			DataTable dt;
			DataRow drTemp;
			DataRow[] dataRowArray;

			try
			{
				if (!this._flex현합품목.HasNormalRow) return;

				dataRowArray = this._flex현합품목.DataTable.Select("S = 'Y'");

				dt = new DataTable();

				dt.Columns.Add("CD_COMPANY");
				dt.Columns.Add("CD_PLANT");
				dt.Columns.Add("NO_ID");
				dt.Columns.Add("STA_DEACTIVATE");
				dt.Columns.Add("DC_RMK");

				if (dataRowArray == null && dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
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
					DBHelper.ExecuteNonQuery("SP_CZ_PR_ASSEMBLING_DEACTIVATE_SUB_JSON", new object[] { dt.Json(), Global.MainFrame.LoginInfo.UserID });
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn대기_Click(object sender, EventArgs e)
		{
			DataTable dt;
			DataRow drTemp;
			DataRow[] dataRowArray;

			try
			{
				if (!this._flex대상품목.HasNormalRow) return;

				dataRowArray = this._flex대상품목.DataTable.Select("S = 'Y'");

				dt = new DataTable();

				dt.Columns.Add("CD_COMPANY");
				dt.Columns.Add("CD_PLANT");
				dt.Columns.Add("NO_ID");
				dt.Columns.Add("STA_DEACTIVATE");
				dt.Columns.Add("DC_RMK");

				if (dataRowArray == null && dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
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
					DBHelper.ExecuteNonQuery("SP_CZ_PR_ASSEMBLING_DEACTIVATE_SUB_JSON", new object[] { dt.Json(), Global.MainFrame.LoginInfo.UserID });
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn대기품목관리_Click(object sender, EventArgs e)
		{
			try
			{
				P_CZ_PR_ASSEMBLING_DEACTIVATE_SUB dialog = new P_CZ_PR_ASSEMBLING_DEACTIVATE_SUB();
				dialog.ShowDialog();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn재고실사_Click(object sender, EventArgs e)
		{
			try
			{
				P_CZ_PR_ASSEMBLING_COUNTING_SUB dialog = new P_CZ_PR_ASSEMBLING_COUNTING_SUB();
				dialog.ShowDialog();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn단품판매등록_Click(object sender, EventArgs e)
		{
			try
			{
				P_CZ_PR_ASSEMBLING_SA_SOL_SUB dialog = new P_CZ_PR_ASSEMBLING_SA_SOL_SUB();
				dialog.ShowDialog();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn대상품목추가_Click(object sender, EventArgs e)
		{
			try
			{
				P_CZ_PR_ASSEMBLING_ID_OLD_SUB dialog = new P_CZ_PR_ASSEMBLING_ID_OLD_SUB();
				dialog.ShowDialog();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Ctx모품목_QueryBefore(object sender, BpQueryArgs e)
		{
			try
			{
				e.HelpParam.P09_CD_PLANT = Global.MainFrame.LoginInfo.CdPlant;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn실적등록_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.cur실적수량.DecimalValue > (D.GetDecimal(this._flex작업지시["QT_ASSEMBLING"]) - D.GetDecimal(this._flex작업지시["QT_WORK"])))
				{
					this.ShowMessage(공통메세지._은_보다작거나같아야합니다, "실적수량", "조립수량-작업수량");
					return;
				}

				bool isSuccess = DBHelper.ExecuteNonQuery("SP_CZ_PR_ASSEMBLING_MNG_WO_I", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																									     this._flex작업지시["NO_WO"].ToString(),
																									     this._flex작업지시["NO_LINE"].ToString(),
																									     Global.MainFrame.LoginInfo.UserID,
																									     this.cur실적수량.DecimalValue });

				if (isSuccess == true)
				{
					this.cur실적수량.DecimalValue = 0;
					this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn조립해제_Click(object sender, EventArgs e)
		{
			DataTable dt;
			DataRow[] dataRowArray;
			string query, query1, query2, query3;

			try
			{
				dataRowArray = this._flex조립.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else if (this._flex조립.DataTable.Select("S = 'Y' AND ISNULL(DT_ASSEMBLE, '') <> ''").Length > 0)
				{
					this.ShowMessage("조립일자가 입력되어 있는 건이 선택되어 있습니다.");
					return;
				}
				else
				{
					query = @"DELETE AD 
							  FROM CZ_PR_ASSEMBLING_DATA AD WITH(NOLOCK)
							  WHERE AD.CD_COMPANY = '{0}'
							  AND AD.CD_PLANT = '{1}'
							  AND AD.NO_ID = '{2}'
							  AND EXISTS (SELECT 1 
										  FROM CZ_PR_MATCHING_DATA MD
										  WHERE MD.CD_COMPANY = AD.CD_COMPANY
										  AND MD.NO_ID_C = AD.NO_ID_C)";

					query1 = @"DELETE WI
							   FROM CZ_PR_WO_INSP WI
							   LEFT JOIN CZ_PR_WO_REQ_D WD ON WD.CD_COMPANY = WI.CD_COMPANY AND WD.NO_WO = WI.NO_WO AND WD.SEQ_WO = WI.SEQ_WO
							   WHERE WI.CD_COMPANY = '{0}'
							   AND WD.NO_ID = '{1}'
							   AND WI.NO_INSP = 996";

					query2 = @"SELECT ISNULL(WR.QT_WORK, 0) AS QT_WORK,
							   	      ISNULL(WI.QT_ASSEMBLING, 0) AS QT_ASSEMBLING
							   FROM PR_WO WO WITH(NOLOCK)
							   JOIN PR_WO_ROUT WR WITH(NOLOCK) ON WR.CD_COMPANY = WO.CD_COMPANY AND WR.NO_WO = WO.NO_WO
							   LEFT JOIN (SELECT WI.CD_COMPANY, WI.NO_WO, WI.NO_LINE,
							   		             COUNT(1) AS QT_ASSEMBLING 
							   		      FROM CZ_PR_WO_INSP WI WITH(NOLOCK)
							   		      WHERE WI.NO_INSP = 996
							   		      GROUP BY WI.CD_COMPANY, WI.NO_WO, WI.NO_LINE) WI
							   ON WI.CD_COMPANY = WO.CD_COMPANY AND WI.NO_WO = WO.NO_WO AND WI.NO_LINE = WR.NO_LINE
							   WHERE WO.CD_COMPANY = '{0}'
							   AND WO.NO_WO = '{1}'
							   AND WR.NO_LINE = '{2}'";

					query3 = @"SELECT AD.CD_PITEM 
							   FROM CZ_PR_ASSEMBLING_DATA AD WITH(NOLOCK)
							   WHERE AD.CD_COMPANY = '{0}'
							   AND AD.CD_PLANT = '{1}'
							   AND AD.NO_ID = '{2}'
							   AND EXISTS (SELECT 1 
							   		       FROM CZ_PR_MATCHING_DATA MD
							   		       WHERE MD.CD_COMPANY = AD.CD_COMPANY
							   		       AND MD.NO_ID_C = AD.NO_ID_C)";
							   
					foreach (DataRow dr in dataRowArray)
					{
						dt = DBHelper.GetDataTable(string.Format(query2, Global.MainFrame.LoginInfo.CompanyCode, 
																	     dr["NO_WO"].ToString(), 
																	     dr["NO_LINE"].ToString()));

						if (D.GetInt(dt.Rows[0]["QT_WORK"]) > D.GetInt(dt.Rows[0]["QT_ASSEMBLING"]) - 1)
						{
							this.ShowMessage(공통메세지._은_보다작거나같아야합니다, "작업수량", "조립수량");
							return;
						}

						dt = DBHelper.GetDataTable(string.Format(query3, Global.MainFrame.LoginInfo.CompanyCode,
																		 Global.MainFrame.LoginInfo.CdPlant,
																		 dr["NO_ID"].ToString()));

						DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 
																	Global.MainFrame.LoginInfo.CdPlant, 
																	dr["NO_ID"].ToString()));

						DBHelper.ExecuteScalar(string.Format(query1, Global.MainFrame.LoginInfo.CompanyCode,
																	 dr["NO_ID"].ToString()));

						foreach (DataRow dr1 in dt.Rows)
						{
							dr[dr1["CD_PITEM"].ToString() + "_1"] = string.Empty;
						}

						dr["NO_SO"] = string.Empty;
					}

					this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn현합적용해제.Text);
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn적용M_Click(object sender, EventArgs e)
		{
			DataTable dt, dt1;
			DataRow[] dataRowArray, dataRowArray1;
			DataRow drTo, drTemp;
			string 위치코드, 품목코드, 수주번호, query, query1, cdPitem, seqItem;

			try
			{
				if (!this._flex조립.HasNormalRow) return;

				if (string.IsNullOrEmpty(this.cbo수주번호.SelectedValue.ToString()))
				{
					this.ShowMessage("수주번호를 지정해야 합니다.");
					return;
				}

				if (string.IsNullOrEmpty(this.cbo현합품목M.SelectedValue.ToString()))
				{
					this.ShowMessage("현합품목을 지정해야 합니다.");
					return;
				}

				dataRowArray = this._flex현합품목.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					품목코드 = this.cbo현합품목M.SelectedValue.ToString();
					수주번호 = this.cbo수주번호.SelectedValue.ToString();

					#region 품목
					dt = this._flex현합품목.DataTable.Clone();

					dt.Columns.Add("CD_COMPANY");
					dt.Columns.Add("CD_PLANT");
					dt.Columns.Add("NO_ID_C");
					dt.Columns.Add("CD_PITEM");
					dt.Columns.Add("SEQ_ITEM");
					dt.Columns.Add("NO_LOT");
					dt.Columns.Add("NO_HEAT");
					#endregion

					#region 수주번호
					dt1 = new DataTable();

					dt1.Columns.Add("CD_COMPANY");
					dt1.Columns.Add("NO_WO");
					dt1.Columns.Add("NO_LINE");
					dt1.Columns.Add("SEQ_WO");
					dt1.Columns.Add("NO_SO");
					dt1.Columns.Add("DC_RMK");
					#endregion

					query = @"SELECT AI.CD_PITEM
FROM CZ_PR_ASSEMBLING_ITEM AI WITH(NOLOCK)
WHERE AI.CD_COMPANY = '{0}'
AND AI.CD_ITEM = '{1}'
AND AI.CD_MITEM = '{2}'
AND AI.YN_MATCHING = 'Y'
AND (EXISTS (SELECT 1 
			 FROM CZ_PR_WO_REQ_D WD WITH(NOLOCK)
			 WHERE WD.CD_COMPANY = AI.CD_COMPANY
			 AND WD.CD_ITEM = AI.CD_PITEM
			 AND WD.NO_ID = '{3}') OR EXISTS (SELECT 1 
                                                 FROM CZ_PR_MATCHING_ID_OLD ID WITH(NOLOCK)
                                                 WHERE ID.CD_COMPANY = AI.CD_COMPANY
                                                 AND ID.CD_ITEM = AI.CD_PITEM
                                                 AND ID.NO_ID = '{3}'))";

					query1 = @"SELECT 1 
FROM CZ_PR_SA_SOL_PR_WO_MAPPING SP WITH(NOLOCK)
WHERE SP.CD_COMPANY = '{0}'
AND SP.NO_SO = '{1}'
AND SP.NO_WO = '{2}'";

					foreach (DataRow dr in dataRowArray)
					{
						if (string.IsNullOrEmpty(dr["001"].ToString())) 
							continue;

						#region 품목
						위치코드 = DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 지시품목, 품목코드, dr["001"].ToString())).ToString() + "_1";
						cdPitem = DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 지시품목, 품목코드, dr["001"].ToString())).ToString();
						seqItem = "1";
						dataRowArray1 = this._flex조립.DataTable.Select(string.Format("ISNULL([{0}], '') = '' AND (ISNULL(NO_SO, '') = '' OR NO_SO = '{1}')", 위치코드, 수주번호));

						if (dataRowArray1.Length == 0)
						{
							this.ShowMessage("조립대상 행이 없습니다.");
							break;
						}

						drTo = null;

						foreach (DataRow dr1 in dataRowArray1)
						{
							DataTable dt2 = DBHelper.GetDataTable(string.Format(query1, Global.MainFrame.LoginInfo.CompanyCode, 수주번호, dr1["NO_WO"].ToString()));

							if (dt2.Rows.Count > 0)
							{
								drTo = dr1;
								break;
							}
						}

						if (drTo == null)
						{
							this.ShowMessage("수주번호에 해당하는 건이 없습니다.");
							break;
						}

						drTo[위치코드] = dr["001"].ToString();

						drTemp = dt.NewRow();

						drTemp["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
						drTemp["CD_PLANT"] = Global.MainFrame.LoginInfo.CdPlant;
						drTemp["NO_WO"] = drTo["NO_WO"];
						drTemp["NO_LINE"] = drTo["NO_LINE"];
						drTemp["SEQ_WO"] = drTo["SEQ_WO"];
						drTemp["CD_PITEM"] = cdPitem;
						drTemp["SEQ_ITEM"] = seqItem;
						drTemp["NO_ID_C"] = dr["001"].ToString();
						drTemp["NO_ID"] = drTo["NO_ID"];
						drTemp["NO_LOT"] = dr["NO_LOT1"];
						drTemp["NO_HEAT"] = dr["NO_HEAT1"];

						dt.Rows.Add(drTemp);

						if (!string.IsNullOrEmpty(dr["002"].ToString()))
						{
							위치코드 = DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 지시품목, 품목코드, dr["002"].ToString())).ToString() + "_1";
							cdPitem = DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 지시품목, 품목코드, dr["002"].ToString())).ToString();
							seqItem = "1";

							drTo[위치코드] = dr["002"].ToString();

							drTemp = dt.NewRow();

							drTemp["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
							drTemp["CD_PLANT"] = Global.MainFrame.LoginInfo.CdPlant;
							drTemp["NO_WO"] = drTo["NO_WO"];
							drTemp["NO_LINE"] = drTo["NO_LINE"];
							drTemp["SEQ_WO"] = drTo["SEQ_WO"];
							drTemp["CD_PITEM"] = cdPitem;
							drTemp["SEQ_ITEM"] = seqItem;
							drTemp["NO_ID_C"] = dr["002"].ToString();
							drTemp["NO_ID"] = drTo["NO_ID"];
							drTemp["NO_LOT"] = dr["NO_LOT2"];
							drTemp["NO_HEAT"] = dr["NO_HEAT2"];

							dt.Rows.Add(drTemp);
						}

						if (!string.IsNullOrEmpty(dr["003"].ToString()))
						{
							위치코드 = DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 지시품목, 품목코드, dr["003"].ToString())).ToString() + "_1";
							cdPitem = DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 지시품목, 품목코드, dr["003"].ToString())).ToString();
							seqItem = "1";

							drTo[위치코드] = dr["003"].ToString();

							drTemp = dt.NewRow();

							drTemp["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
							drTemp["CD_PLANT"] = Global.MainFrame.LoginInfo.CdPlant;
							drTemp["NO_WO"] = drTo["NO_WO"];
							drTemp["NO_LINE"] = drTo["NO_LINE"];
							drTemp["SEQ_WO"] = drTo["SEQ_WO"];
							drTemp["CD_PITEM"] = cdPitem;
							drTemp["SEQ_ITEM"] = seqItem;
							drTemp["NO_ID_C"] = dr["003"].ToString();
							drTemp["NO_ID"] = drTo["NO_ID"];
							drTemp["NO_LOT"] = dr["NO_LOT3"];
							drTemp["NO_HEAT"] = dr["NO_HEAT3"];

							dt.Rows.Add(drTemp);
						}

						if (!string.IsNullOrEmpty(dr["004"].ToString()))
						{
							위치코드 = DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 지시품목, 품목코드, dr["004"].ToString())).ToString() + "_1";
							cdPitem = DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 지시품목, 품목코드, dr["004"].ToString())).ToString();
							seqItem = "1";

							drTo[위치코드] = dr["004"].ToString();

							drTemp = dt.NewRow();

							drTemp["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
							drTemp["CD_PLANT"] = Global.MainFrame.LoginInfo.CdPlant;
							drTemp["NO_WO"] = drTo["NO_WO"];
							drTemp["NO_LINE"] = drTo["NO_LINE"];
							drTemp["SEQ_WO"] = drTo["SEQ_WO"];
							drTemp["CD_PITEM"] = cdPitem;
							drTemp["SEQ_ITEM"] = seqItem;
							drTemp["NO_ID_C"] = dr["004"].ToString();
							drTemp["NO_ID"] = drTo["NO_ID"];
							drTemp["NO_LOT"] = dr["NO_LOT4"];
							drTemp["NO_HEAT"] = dr["NO_HEAT4"];

							dt.Rows.Add(drTemp);
						}

						if (!string.IsNullOrEmpty(dr["005"].ToString()))
						{
							위치코드 = DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 지시품목, 품목코드, dr["005"].ToString())).ToString() + "_1";
							cdPitem = DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 지시품목, 품목코드, dr["005"].ToString())).ToString();
							seqItem = "1";

							drTo[위치코드] = dr["005"].ToString();

							drTemp = dt.NewRow();

							drTemp["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
							drTemp["CD_PLANT"] = Global.MainFrame.LoginInfo.CdPlant;
							drTemp["NO_WO"] = drTo["NO_WO"];
							drTemp["NO_LINE"] = drTo["NO_LINE"];
							drTemp["SEQ_WO"] = drTo["SEQ_WO"];
							drTemp["CD_PITEM"] = cdPitem;
							drTemp["SEQ_ITEM"] = seqItem;
							drTemp["NO_ID_C"] = dr["005"].ToString();
							drTemp["NO_ID"] = drTo["NO_ID"];
							drTemp["NO_LOT"] = dr["NO_LOT5"];
							drTemp["NO_HEAT"] = dr["NO_HEAT5"];

							dt.Rows.Add(drTemp);
						}
						#endregion

						if (string.IsNullOrEmpty(drTo["NO_SO"].ToString()))
						{
							drTo["NO_SO"] = 수주번호;

							drTemp = dt1.NewRow();

							drTemp["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
							drTemp["NO_WO"] = drTo["NO_WO"].ToString();
							drTemp["NO_LINE"] = drTo["NO_LINE"].ToString();
							drTemp["SEQ_WO"] = drTo["SEQ_WO"].ToString();
							drTemp["NO_SO"] = 수주번호;
							drTemp["DC_RMK"] = string.Empty;

							dt1.Rows.Add(drTemp);
						}

						dr.Delete();
					}

					if (dt.Rows.Count > 0)
					{
						DBHelper.ExecuteNonQuery("SP_CZ_PR_ASSEMBLING_MNG_JSON", new object[] { dt.Json(), Global.MainFrame.LoginInfo.UserID });
					}

					if (dt1.Rows.Count > 0)
					{
						DBHelper.ExecuteNonQuery("SP_CZ_PR_ASSEMBLING_MNG_JSON1", new object[] { dt1.GetChanges().Json(), Global.MainFrame.LoginInfo.UserID });
					}
				}

				this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn적용.Text);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn조회M_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flex조립.HasNormalRow) return;
				if (string.IsNullOrEmpty(this.cbo수주번호.SelectedValue.ToString()))
				{
					this.ShowMessage("수주번호가 설정되어 있지 않습니다.");
					return;
				}

				DataTable dt = this._biz.SearchIDMatch(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																	  this.cbo수주번호.SelectedValue.ToString(),
																	  this.cbo현합품목M.SelectedValue.ToString(),
																	  this.txt범위조회MFrom.Text,
																	  this.txt범위조회MTo.Text });

				this._flex현합품목.Binding = dt;

				this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn조회M.Text);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flex조립_ValidateEdit(object sender, ValidateEditEventArgs e)
		{
			DataTable dt, dt1;
			DataRow drTemp;
			string[] strArray;
			string oldValue, newValue, columnName, 품목코드, seqItem;

			try
			{
				columnName = this._flex조립.Cols[e.Col].Name;

				oldValue = this._flex조립[e.Row, e.Col].ToString();
				newValue = this._flex조립.EditData;

				if (columnName == "S") return;
				if (oldValue == newValue) return;
				if (!string.IsNullOrEmpty(this._flex조립["DT_ASSEMBLE"].ToString()))
				{
					this.ShowMessage("조립일자가 있는 건은 수정 할 수 없습니다.");
					e.Cancel = true;
					return;
				}

				if (columnName == "DC_RMK")
				{
					#region 비고
					if (string.IsNullOrEmpty(this._flex조립["NO_SO"].ToString()))
					{
						this.ShowMessage("수주번호가 할당되어 있지 않은 건 입니다.");
						e.Cancel = true;
						return;
					}

					dt = new DataTable();

					dt.Columns.Add("CD_COMPANY");
					dt.Columns.Add("NO_WO");
					dt.Columns.Add("NO_LINE");
					dt.Columns.Add("SEQ_WO");
					dt.Columns.Add("NO_SO");
					dt.Columns.Add("DC_RMK");

					drTemp = dt.NewRow();

					drTemp["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
					drTemp["NO_WO"] = this._flex조립["NO_WO"].ToString();
					drTemp["NO_LINE"] = this._flex조립["NO_LINE"].ToString();
					drTemp["SEQ_WO"] = this._flex조립["SEQ_WO"].ToString();
					drTemp["NO_SO"] = this._flex조립["NO_SO"].ToString();
					drTemp["DC_RMK"] = newValue;

					dt.Rows.Add(drTemp);
					drTemp.AcceptChanges();
					drTemp.SetModified();

					if (dt.Rows.Count > 0)
					{
						DBHelper.ExecuteNonQuery("SP_CZ_PR_ASSEMBLING_MNG_JSON1", new object[] { dt.GetChanges().Json(), Global.MainFrame.LoginInfo.UserID });
					}
					#endregion
				}
				else if (columnName == "TXT_OPENING")
                {
					#region OPENING
					if (string.IsNullOrEmpty(this._flex조립["NO_SO"].ToString()))
					{
						this.ShowMessage("수주번호가 할당되어 있지 않은 건 입니다.");
						e.Cancel = true;
						return;
					}

					dt = new DataTable();

					dt.Columns.Add("CD_COMPANY");
					dt.Columns.Add("NO_WO");
					dt.Columns.Add("NO_LINE");
					dt.Columns.Add("SEQ_WO");
					dt.Columns.Add("NO_SO");
					dt.Columns.Add("TXT_OPENING");

					drTemp = dt.NewRow();

					drTemp["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
					drTemp["NO_WO"] = this._flex조립["NO_WO"].ToString();
					drTemp["NO_LINE"] = this._flex조립["NO_LINE"].ToString();
					drTemp["SEQ_WO"] = this._flex조립["SEQ_WO"].ToString();
					drTemp["NO_SO"] = this._flex조립["NO_SO"].ToString();
					drTemp["TXT_OPENING"] = newValue;

					dt.Rows.Add(drTemp);
					drTemp.AcceptChanges();
					drTemp.SetModified();

					if (dt.Rows.Count > 0)
					{
						DBHelper.ExecuteNonQuery("SP_CZ_PR_ASSEMBLING_MNG_JSON2", new object[] { dt.GetChanges().Json(), Global.MainFrame.LoginInfo.UserID });
					}
					#endregion
				}
				else if (columnName == "TXT_VENTING")
				{
					#region OPENING
					if (string.IsNullOrEmpty(this._flex조립["NO_SO"].ToString()))
					{
						this.ShowMessage("수주번호가 할당되어 있지 않은 건 입니다.");
						e.Cancel = true;
						return;
					}

					dt = new DataTable();

					dt.Columns.Add("CD_COMPANY");
					dt.Columns.Add("NO_WO");
					dt.Columns.Add("NO_LINE");
					dt.Columns.Add("SEQ_WO");
					dt.Columns.Add("NO_SO");
					dt.Columns.Add("TXT_VENTING");

					drTemp = dt.NewRow();

					drTemp["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
					drTemp["NO_WO"] = this._flex조립["NO_WO"].ToString();
					drTemp["NO_LINE"] = this._flex조립["NO_LINE"].ToString();
					drTemp["SEQ_WO"] = this._flex조립["SEQ_WO"].ToString();
					drTemp["NO_SO"] = this._flex조립["NO_SO"].ToString();
					drTemp["TXT_VENTING"] = newValue;

					dt.Rows.Add(drTemp);
					drTemp.AcceptChanges();
					drTemp.SetModified();

					if (dt.Rows.Count > 0)
					{
						DBHelper.ExecuteNonQuery("SP_CZ_PR_ASSEMBLING_MNG_JSON3", new object[] { dt.GetChanges().Json(), Global.MainFrame.LoginInfo.UserID });
					}
					#endregion
				}
				else if (columnName == "TXT_SHIM")
				{
					#region OPENING
					if (string.IsNullOrEmpty(this._flex조립["NO_SO"].ToString()))
					{
						this.ShowMessage("수주번호가 할당되어 있지 않은 건 입니다.");
						e.Cancel = true;
						return;
					}

					dt = new DataTable();

					dt.Columns.Add("CD_COMPANY");
					dt.Columns.Add("NO_WO");
					dt.Columns.Add("NO_LINE");
					dt.Columns.Add("SEQ_WO");
					dt.Columns.Add("NO_SO");
					dt.Columns.Add("TXT_SHIM");

					drTemp = dt.NewRow();

					drTemp["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
					drTemp["NO_WO"] = this._flex조립["NO_WO"].ToString();
					drTemp["NO_LINE"] = this._flex조립["NO_LINE"].ToString();
					drTemp["SEQ_WO"] = this._flex조립["SEQ_WO"].ToString();
					drTemp["NO_SO"] = this._flex조립["NO_SO"].ToString();
					drTemp["TXT_SHIM"] = newValue;

					dt.Rows.Add(drTemp);
					drTemp.AcceptChanges();
					drTemp.SetModified();

					if (dt.Rows.Count > 0)
					{
						DBHelper.ExecuteNonQuery("SP_CZ_PR_ASSEMBLING_MNG_JSON4", new object[] { dt.GetChanges().Json(), Global.MainFrame.LoginInfo.UserID });
					}
					#endregion
				}
				else
				{
					#region ID
					strArray = columnName.Split('_');
					품목코드 = strArray[0];
					seqItem = strArray[1];

					dt = null;

					if (!string.IsNullOrEmpty(newValue))
					{
						dt = this._biz.SearchID(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
															   Global.MainFrame.LoginInfo.CdPlant,
															   품목코드,
															   newValue,
															   newValue });

						if (dt == null || dt.Rows.Count == 0)
						{
							this.ShowMessage("ID에 해당하는 항목이 없습니다.");
							e.Cancel = true;
							return;
						}
						else
						{
							dt1 = dt.Clone();
						}
					}
					else
					{
						dt1 = this._biz.SearchID(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																Global.MainFrame.LoginInfo.CdPlant,
																품목코드,
																"99999",
																"99999" });
					}

					dt1.Columns.Add("CD_COMPANY");
					dt1.Columns.Add("CD_PLANT");
					dt1.Columns.Add("NO_WO");
					dt1.Columns.Add("NO_LINE");
					dt1.Columns.Add("SEQ_WO");
					dt1.Columns.Add("NO_ID_C");
					dt1.Columns.Add("SEQ_ITEM");

					if (string.IsNullOrEmpty(oldValue) && !string.IsNullOrEmpty(newValue))
					{
						drTemp = dt1.NewRow();

						drTemp["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
						drTemp["CD_PLANT"] = Global.MainFrame.LoginInfo.CdPlant;
						drTemp["NO_WO"] = this._flex조립["NO_WO"].ToString();
						drTemp["NO_LINE"] = this._flex조립["NO_LINE"].ToString();
						drTemp["SEQ_WO"] = this._flex조립["SEQ_WO"].ToString();
						drTemp["CD_PITEM"] = 품목코드;
						drTemp["SEQ_ITEM"] = seqItem;
						drTemp.ItemArray = dt.Rows[0].ItemArray;
						drTemp["NO_ID_C"] = drTemp["NO_ID"];
						drTemp["NO_ID"] = this._flex조립["NO_ID"].ToString();

						dt1.Rows.Add(drTemp);
					}
					else if (!string.IsNullOrEmpty(oldValue) && string.IsNullOrEmpty(newValue))
					{
						drTemp = dt1.NewRow();

						drTemp["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
						drTemp["CD_PLANT"] = Global.MainFrame.LoginInfo.CdPlant;
						drTemp["NO_WO"] = this._flex조립["NO_WO"].ToString();
						drTemp["NO_LINE"] = this._flex조립["NO_LINE"].ToString();
						drTemp["SEQ_WO"] = this._flex조립["SEQ_WO"].ToString();
						drTemp["NO_ID"] = this._flex조립["NO_ID"].ToString();
						drTemp["CD_PITEM"] = 품목코드;
						drTemp["SEQ_ITEM"] = seqItem;
						drTemp["NO_ID_C"] = oldValue;
						drTemp["CD_PITEM"] = 품목코드;

						dt1.Rows.Add(drTemp);
						drTemp.AcceptChanges();
						drTemp.Delete();
					}
					else if (!string.IsNullOrEmpty(oldValue) && !string.IsNullOrEmpty(newValue))
					{
						drTemp = dt1.NewRow();

						drTemp["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
						drTemp["CD_PLANT"] = Global.MainFrame.LoginInfo.CdPlant;
						drTemp["NO_WO"] = this._flex조립["NO_WO"].ToString();
						drTemp["NO_LINE"] = this._flex조립["NO_LINE"].ToString();
						drTemp["SEQ_WO"] = this._flex조립["SEQ_WO"].ToString();
						drTemp["CD_PITEM"] = 품목코드;
						drTemp["SEQ_ITEM"] = seqItem;
						drTemp.ItemArray = dt.Rows[0].ItemArray;
						drTemp["NO_ID_C"] = drTemp["NO_ID"];
						drTemp["NO_ID"] = this._flex조립["NO_ID"].ToString();

						dt1.Rows.Add(drTemp);
						drTemp.AcceptChanges();
						drTemp.SetModified();
					}

					if (dt1.Rows.Count > 0)
					{
						DBHelper.ExecuteNonQuery("SP_CZ_PR_ASSEMBLING_MNG_JSON", new object[] { dt1.GetChanges().Json(), Global.MainFrame.LoginInfo.UserID });
					}
					#endregion
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn교체_Click(object sender, EventArgs e)
		{
			DataTable dt;
			DataRow[] dataRowArray;
			DataRow drFrom, drTemp;
			string 위치코드, ID번호, 품목코드;
			try
			{
				if (!this._flex조립.HasNormalRow) return;

				if (string.IsNullOrEmpty(this.cbo대상품목.SelectedValue.ToString()))
				{
					this.ShowMessage("대상품목을 지정해야 합니다.");
					return;
				}

				dataRowArray = this._flex대상품목.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else if (dataRowArray.Length != 1)
				{
					this.ShowMessage("대상품목 중 1개의 ID번호만 선택 가능합니다.");
				}
				else
				{
					품목코드 = this.cbo대상품목.SelectedValue.ToString();
					위치코드 = ((DataRowView)cbo대상품목.SelectedItem).Row.ItemArray[0].ToString();

					dt = this._flex대상품목.DataTable.Clone();

					dt.Columns.Add("CD_COMPANY");
					dt.Columns.Add("CD_PLANT");
					dt.Columns.Add("NO_WO");
					dt.Columns.Add("NO_LINE");
					dt.Columns.Add("SEQ_WO");
					dt.Columns.Add("NO_ID_C");

					drFrom = dataRowArray[0];

					ID번호 = drFrom["NO_ID"].ToString();

					if (drFrom["CD_PITEM"].ToString() != 품목코드)
					{
						this.ShowMessage("선택한 대상품목과 현합품목이 다릅니다.");
						return;
					}
					else if (this._flex조립.DataTable.Select("ISNULL([" + 위치코드 + "], '') = '" + ID번호 + "'").Length > 0)
					{
						this.ShowMessage("동일한 가공ID가 등록되어 있습니다. (" + ID번호 + ")");
						return;
					}
					else
					{
						this._flex조립[위치코드] = ID번호;

						drTemp = dt.NewRow();

						drTemp["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
						drTemp["CD_PLANT"] = Global.MainFrame.LoginInfo.CdPlant;
						drTemp["NO_WO"] = this._flex조립["NO_WO"];
						drTemp["NO_LINE"] = this._flex조립["NO_LINE"];
						drTemp["SEQ_WO"] = this._flex조립["SEQ_WO"];
						drTemp["CD_PITEM"] = 위치코드;
						drTemp.ItemArray = drFrom.ItemArray;
						drTemp["NO_ID_C"] = drTemp["NO_ID"];
						drTemp["NO_ID"] = this._flex조립["NO_ID"];

						dt.Rows.Add(drTemp);

						drFrom.Delete();
					}

					if (dt.Rows.Count > 0)
					{
						DBHelper.ExecuteNonQuery("SP_CZ_PR_ASSEMBLING_MNG_JSON5", new object[] { dt.Json(), Global.MainFrame.LoginInfo.UserID });
					}

					DataTable dt2 = this._biz.SearchID(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																 Global.MainFrame.LoginInfo.CdPlant,
																 this.cbo대상품목.SelectedValue.ToString(),
																 this.txt범위조회From.Text,
																 this.txt범위조회To.Text });

					this._flex대상품목.Binding = dt2;

					this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn교체.Text);
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn적용_Click(object sender, EventArgs e)
		{
			DataTable dt;
			DataRow[] dataRowArray;
			DataRow drFrom, drTemp;
			string[] strArray;
			string 위치코드, ID번호, 품목코드, seqItem;
			int index;

			try
			{
				if (!this._flex조립.HasNormalRow) return;

				if (string.IsNullOrEmpty(this.cbo대상품목.SelectedValue.ToString()))
				{
					this.ShowMessage("대상품목을 지정해야 합니다.");
					return;
				}

				dataRowArray = this._flex대상품목.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					strArray = this.cbo대상품목.SelectedValue.ToString().Split('_');
					품목코드 = strArray[0];
					seqItem = strArray[1];
					위치코드 = ((DataRowView)cbo대상품목.SelectedItem).Row.ItemArray[0].ToString();

					index = 0;

					dt = this._flex대상품목.DataTable.Clone();

					dt.Columns.Add("CD_COMPANY");
					dt.Columns.Add("CD_PLANT");
					dt.Columns.Add("NO_WO");
					dt.Columns.Add("NO_LINE");
					dt.Columns.Add("SEQ_WO");
					dt.Columns.Add("NO_ID_C");
					dt.Columns.Add("SEQ_ITEM");

					foreach (DataRow dr in this._flex조립.DataTable.Select("ISNULL([" + 위치코드 + "], '') = '' AND ISNULL(DT_ASSEMBLE, '') = ''"))
					{
						if (dataRowArray.Length - 1 < index)
							break;

						drFrom = dataRowArray[index];

						ID번호 = drFrom["NO_ID"].ToString();

						if (drFrom["CD_PITEM"].ToString() != 품목코드)
						{
							this.ShowMessage("선택한 대상품목과 현합품목이 다릅니다.");
							break;
						}
						else if (this._flex조립.DataTable.Select("ISNULL([" + 위치코드 + "], '') = '" + ID번호 + "'").Length > 0)
						{
							this.ShowMessage("동일한 가공ID가 등록되어 있습니다. (" + ID번호 + ")");
							break;
						}
						else
						{
							dr[위치코드] = ID번호;

							drTemp = dt.NewRow();

							drTemp["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
							drTemp["CD_PLANT"] = Global.MainFrame.LoginInfo.CdPlant;
							drTemp["NO_WO"] = dr["NO_WO"];
							drTemp["NO_LINE"] = dr["NO_LINE"];
							drTemp["SEQ_WO"] = dr["SEQ_WO"];
							drTemp["CD_PITEM"] = 품목코드;
							drTemp["SEQ_ITEM"] = seqItem;
							drTemp.ItemArray = drFrom.ItemArray;
							drTemp["NO_ID_C"] = drTemp["NO_ID"];
							drTemp["NO_ID"] = dr["NO_ID"];

							dt.Rows.Add(drTemp);

							drFrom.Delete();
						}

						index++;
					}

					if (dt.Rows.Count > 0)
					{
						DBHelper.ExecuteNonQuery("SP_CZ_PR_ASSEMBLING_MNG_JSON", new object[] { dt.Json(), Global.MainFrame.LoginInfo.UserID });
					}
				}

				this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn적용.Text);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn조회_Click(object sender, EventArgs e)
		{
			string 품목코드;
			string[] strArray;
			try
			{
				if (!this._flex조립.HasNormalRow) return;
				if (string.IsNullOrEmpty(this.cbo대상품목.SelectedValue.ToString()))
				{
					this.ShowMessage("대상품목이 설정되어 있지 않습니다.");
					return;
				}
				strArray = this.cbo대상품목.SelectedValue.ToString().Split('_');
				품목코드 = strArray[0];

				DataTable dt = this._biz.SearchID(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																 Global.MainFrame.LoginInfo.CdPlant,
																 품목코드,
																 this.txt범위조회From.Text,
																 this.txt범위조회To.Text });

				this._flex대상품목.Binding = dt;

				this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn조회.Text);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flex작업지시_AfterRowChange(object sender, RangeEventArgs e)
		{
			DataTable dt = null;
			string key, filter;

			try
			{
				if (this._flex작업지시.HasNormalRow == false) return;

				key = this._flex작업지시["NO_WO"].ToString();
				filter = "NO_WO = '" + key + "'";

				if (this._flex작업지시.DetailQueryNeed == true)
					dt = this._biz.Search2(new object[] { Global.MainFrame.LoginInfo.CompanyCode, key, (this.chk조립제외.Checked == true ? "Y" : "N") });

				this._flex수주번호.BindingAdd(dt, filter);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn지시적용_Click(object sender, EventArgs e)
		{
			string query;
			DataTable dt, dt1;
			DataRow[] dataRowArray;

			try
			{
				if (!this._flex작업지시.HasNormalRow) return;

				dataRowArray = this._flex작업지시.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else if (this._flex작업지시.DataTable.Select("S = 'Y'").Select(x => x["CD_ITEM"]).Distinct().Count() > 1)
				{
					this.ShowMessage("동일하지 않은 품목이 선택되어 있습니다.");
					return;
				}
				else
				{
					지시품목 = this._flex작업지시.DataTable.Select("S = 'Y'").Select(x => x["CD_ITEM"]).FirstOrDefault().ToString();

					#region 리스트 갱신
					query = @";WITH tblA (CD_ITEM, NM_ITEM, CD_MITEM, NM_MITEM, QT_ITEM, YN_MATCHING, DC_RMK) AS
(
	SELECT MI.CD_ITEM,
		   MI.NM_ITEM,
		   ISNULL(MI1.CD_ITEM, MI.CD_ITEM) AS CD_MITEM,
		   ISNULL(MI1.NM_ITEM, MI.NM_ITEM) AS NM_MITEM,
		   AI.QT_ITEM,
		   AI.YN_MATCHING,
		   IG.DC_RMK
	FROM CZ_PR_ASSEMBLING_ITEM AI WITH(NOLOCK)
	LEFT JOIN MA_PITEM MI WITH(NOLOCK) ON MI.CD_COMPANY = AI.CD_COMPANY AND MI.CD_PLANT = AI.CD_PLANT AND MI.CD_ITEM = AI.CD_PITEM
	LEFT JOIN MA_PITEM MI1 WITH(NOLOCK) ON MI1.CD_COMPANY = AI.CD_COMPANY AND MI1.CD_PLANT = AI.CD_PLANT AND MI1.CD_ITEM = AI.CD_MITEM
	LEFT JOIN MA_ITEMGRP IG WITH(NOLOCK) ON IG.CD_COMPANY = MI.CD_COMPANY AND IG.CD_ITEMGRP = MI.GRP_ITEM
	WHERE AI.CD_COMPANY = '{0}'
	AND AI.CD_ITEM = '{1}'
)
,tblB (CD_ITEM, NM_ITEM, CD_MITEM, NM_MITEM, QT_ITEM, SEQ, YN_MATCHING, DC_RMK) AS
(
	SELECT
		a.CD_ITEM
	,	a.NM_ITEM
	,	a.CD_MITEM
	,	a.NM_MITEM
	,	a.QT_ITEM
	,	SEQ = 1
	,	a.YN_MATCHING
	,	a.DC_RMK
	FROM tblA a
	UNION ALL
	SELECT
		a.CD_ITEM
	,	a.NM_ITEM
	,	a.CD_MITEM
	,	a.NM_MITEM
	,	a.QT_ITEM
	,	a.SEQ + 1
	,	a.YN_MATCHING
	,	a.DC_RMK
	FROM tblB a
	WHERE a.SEQ + 1 <= a.QT_ITEM
)
SELECT
	a.CD_ITEM + '_' + CONVERT(NVARCHAR, a.SEQ) AS CD_ITEM
,	CASE WHEN a.SEQ = 1 THEN a.NM_ITEM ELSE a.NM_ITEM + '_' + CONVERT(NVARCHAR, a.SEQ) END AS NM_ITEM
,	a.CD_MITEM
,	CASE WHEN a.SEQ = 1 THEN a.NM_MITEM ELSE a.NM_MITEM + '_' + CONVERT(NVARCHAR, a.SEQ) END AS NM_MITEM
,	a.SEQ
,	a.YN_MATCHING
FROM tblB a
ORDER BY a.DC_RMK, a.CD_ITEM, a.SEQ ASC";

					dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, this._flex작업지시.DataTable.Select("S = 'Y'").Select(x => x["CD_ITEM"]).FirstOrDefault()));

					this._flex조립.BeginSetting(2, 1, false);
					this._flex조립.Cols.Count = 5;

					foreach (DataRow dr in dt.Rows)
					{
						this._flex조립.SetCol(dr["CD_ITEM"].ToString(), dr["NM_ITEM"].ToString(), 100, (dr["YN_MATCHING"].ToString() == "Y" ? false : true));
						this._flex조립[0, this._flex조립.Cols[dr["CD_ITEM"].ToString()].Index] = dr["NM_MITEM"].ToString();
					}

					this._flex조립.SetCol("NO_SO", "수주번호", 100, false);
					this._flex조립.SetCol("DT_ASSEMBLE", "조립일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
					this._flex조립.SetCol("TXT_OPENING", "OPENING", 100, true);
					this._flex조립.SetCol("TXT_VENTING", "VENTING", 100, true);
					this._flex조립.SetCol("TXT_SHIM", "SHIM", 100, true);
					this._flex조립.SetCol("DC_RMK", "비고", 100, true);

					this._flex조립.AllowCache = false;
					this._flex조립.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
					#endregion

					#region 대상품목
					this._flex대상품목.ClearData();

					this.cbo대상품목.DataSource = dt.Select("ISNULL(YN_MATCHING, 'N') = 'N'").ToDataTable();
					this.cbo대상품목.ValueMember = "CD_ITEM";
					this.cbo대상품목.DisplayMember = "NM_ITEM";
					#endregion

					#region 현합품목
					this.cbo현합품목M.DataSource = ComFunc.getGridGroupBy(dt.Select("ISNULL(YN_MATCHING, 'N') = 'Y'"), new string[] { "CD_MITEM", "NM_MITEM" }, true);
					this.cbo현합품목M.ValueMember = "CD_MITEM";
					this.cbo현합품목M.DisplayMember = "NM_MITEM";
					#endregion

					#region 데이터추가
					dt = this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode, string.Empty, 0, (this.chk조립제외.Checked == true ? "Y" : "N") });
					dt1 = this._biz.Search2(new object[] { Global.MainFrame.LoginInfo.CompanyCode, string.Empty, (this.chk조립제외.Checked == true ? "Y" : "N") });

					foreach (DataRow dr in dataRowArray)
					{
						dt.Merge(this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode, dr["NO_WO"].ToString(), dr["NO_LINE"].ToString(), (this.chk조립제외.Checked == true ? "Y" : "N") }));
						dt1.Merge(this._biz.Search2(new object[] { Global.MainFrame.LoginInfo.CompanyCode, dr["NO_WO"].ToString(), (this.chk조립제외.Checked == true ? "Y" : "N") }));
					}

					this._flex조립.Binding = dt;
					#endregion

					#region 수주번호
					this._flex현합품목.ClearData();

					this.cbo수주번호.DataSource = dt1.Select().ToDataTable();
					this.cbo수주번호.ValueMember = "NO_SO";
					this.cbo수주번호.DisplayMember = "NO_SO";
					#endregion
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn조립품목등록_Click(object sender, EventArgs e)
		{
			try
			{
				P_CZ_PR_ASSEMBLING_ITEM_SUB dialog = new P_CZ_PR_ASSEMBLING_ITEM_SUB();
				dialog.ShowDialog();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			DataTable dt;
			string query;

			try
			{
				base.OnToolBarSearchButtonClicked(sender, e);

				if (this.tabControl1.SelectedTab == this.tpg조립)
				{
					this._flex작업지시.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																			     Global.MainFrame.LoginInfo.CdPlant,
																			     this.ctx조립품목.CodeValue,
																			     this.txt작업지시번호.Text,
																			     this.txt수주번호.Text,
																				 this.chk완료제외.Checked == true ? "Y" : "N" });

					if (!this._flex작업지시.HasNormalRow)
					{
						this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
					}
				}
				else
				{
					if (string.IsNullOrEmpty(this.ctx모품목.CodeValue) && 
					    string.IsNullOrEmpty(this.txtID번호.Text))
					{
						this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl모품목.Text);
						return;
					}
					else if (!string.IsNullOrEmpty(this.txtID번호.Text))
					{
						query = @"SELECT WO.CD_ITEM,
										 MI.NM_ITEM
FROM CZ_PR_ASSEMBLING_DATA AD WITH(NOLOCK)
LEFT JOIN PR_WO WO ON WO.CD_COMPANY = AD.CD_COMPANY AND WO.NO_WO = AD.NO_WO
LEFT JOIN MA_PITEM MI ON MI.CD_COMPANY = WO.CD_COMPANY AND MI.CD_ITEM = WO.CD_ITEM
WHERE AD.CD_COMPANY = '{0}'
AND AD.CD_PLANT = '{1}'
AND AD.NO_ID_C = '{2}'";

						dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
																		Global.MainFrame.LoginInfo.CdPlant,
																		this.txtID번호.Text));

						if (dt != null && dt.Rows.Count > 0)
						{
							this.ctx모품목.CodeValue = dt.Rows[0]["CD_ITEM"].ToString();
							this.ctx모품목.CodeName = dt.Rows[0]["NM_ITEM"].ToString();
						}
					}

					#region 리스트 갱신
					query = @";WITH tblA (CD_ITEM, NM_ITEM, CD_MITEM, NM_MITEM, QT_ITEM, YN_MATCHING, DC_RMK) AS
(
	SELECT MI.CD_ITEM,
		   MI.NM_ITEM,
		   ISNULL(MI1.CD_ITEM, MI.CD_ITEM) AS CD_MITEM,
		   ISNULL(MI1.NM_ITEM, MI.NM_ITEM) AS NM_MITEM,
		   AI.QT_ITEM,
		   AI.YN_MATCHING,
		   IG.DC_RMK
	FROM CZ_PR_ASSEMBLING_ITEM AI WITH(NOLOCK)
	LEFT JOIN MA_PITEM MI WITH(NOLOCK) ON MI.CD_COMPANY = AI.CD_COMPANY AND MI.CD_PLANT = AI.CD_PLANT AND MI.CD_ITEM = AI.CD_PITEM
	LEFT JOIN MA_PITEM MI1 WITH(NOLOCK) ON MI1.CD_COMPANY = AI.CD_COMPANY AND MI1.CD_PLANT = AI.CD_PLANT AND MI1.CD_ITEM = AI.CD_MITEM
	LEFT JOIN MA_ITEMGRP IG WITH(NOLOCK) ON IG.CD_COMPANY = MI.CD_COMPANY AND IG.CD_ITEMGRP = MI.GRP_ITEM
	WHERE AI.CD_COMPANY = '{0}'
	AND AI.CD_ITEM = '{1}'
)
,tblB (CD_ITEM, NM_ITEM, CD_MITEM, NM_MITEM, QT_ITEM, SEQ, YN_MATCHING, DC_RMK) AS
(
	SELECT
		a.CD_ITEM
	,	a.NM_ITEM
	,	a.CD_MITEM
	,	a.NM_MITEM
	,	a.QT_ITEM
	,	SEQ = 1
	,	a.YN_MATCHING
	,	a.DC_RMK
	FROM tblA a
	UNION ALL
	SELECT
		a.CD_ITEM
	,	a.NM_ITEM
	,	a.CD_MITEM
	,	a.NM_MITEM
	,	a.QT_ITEM
	,	a.SEQ + 1
	,	a.YN_MATCHING
	,	a.DC_RMK
	FROM tblB a
	WHERE a.SEQ + 1 <= a.QT_ITEM
)
SELECT
	a.CD_ITEM + '_' + CONVERT(NVARCHAR, a.SEQ) AS CD_ITEM
,	CASE WHEN a.SEQ = 1 THEN a.NM_ITEM ELSE a.NM_ITEM + '_' + CONVERT(NVARCHAR, a.SEQ) END AS NM_ITEM
,	a.CD_MITEM
,	CASE WHEN a.SEQ = 1 THEN a.NM_MITEM ELSE a.NM_MITEM + '_' + CONVERT(NVARCHAR, a.SEQ) END AS NM_MITEM
,	a.SEQ
,	a.YN_MATCHING
FROM tblB a
ORDER BY a.DC_RMK, a.CD_ITEM, a.SEQ ASC";

					dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, this.ctx모품목.CodeValue));

					this._flex현황.BeginSetting(3, 1, false);
					this._flex현황.Cols.Count = 1;

					foreach (DataRow dr in dt.Rows)
					{
						this._flex현황.SetCol(dr["CD_ITEM"].ToString() + "-2", "HEAT번호", 100, false);
						this._flex현황.SetCol(dr["CD_ITEM"].ToString(), "ID번호", 100, false);
						this._flex현황.SetCol(dr["CD_ITEM"].ToString() + "-1", "LOT번호", 100, false);
						this._flex현황[0, this._flex현황.Cols[dr["CD_ITEM"].ToString()].Index] = dr["NM_MITEM"].ToString();
						this._flex현황[0, this._flex현황.Cols[dr["CD_ITEM"].ToString() + "-1"].Index] = dr["NM_MITEM"].ToString();
						this._flex현황[0, this._flex현황.Cols[dr["CD_ITEM"].ToString() + "-2"].Index] = dr["NM_MITEM"].ToString();
						this._flex현황[1, this._flex현황.Cols[dr["CD_ITEM"].ToString()].Index] = dr["NM_ITEM"].ToString();
						this._flex현황[1, this._flex현황.Cols[dr["CD_ITEM"].ToString() + "-1"].Index] = dr["NM_ITEM"].ToString();
						this._flex현황[1, this._flex현황.Cols[dr["CD_ITEM"].ToString() + "-2"].Index] = dr["NM_ITEM"].ToString();
					}

					this._flex현황.SetCol("NO_SO", "수주번호", 100, false);
					this._flex현황.SetCol("TXT_OPENING", "OPENING", 100, false);
					this._flex현황.SetCol("TXT_VENTING", "VENTING", 100, false);
					this._flex현황.SetCol("TXT_SHIM", "SHIM", 100, false);
					this._flex현황.SetCol("DC_RMK", "비고", 100, false);

					this._flex현황.AllowCache = false;
					this._flex현황.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
					#endregion

					this._flex현황.Binding = this._biz.Search1(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																			  Global.MainFrame.LoginInfo.CdPlant,
																			  this.ctx모품목.CodeValue,
																			  this.txt수주번호현황.Text });

					if (!this._flex현황.HasNormalRow)
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

using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.Forms;
using Duzon.Erpiu.ComponentModel;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using System;
using System.Data;
using System.Windows.Forms;

namespace cz
{
	public partial class P_CZ_SA_LOG_PLAN_MNG : PageBase
	{
		P_CZ_SA_LOG_PLAN_MNG_BIZ _biz = new P_CZ_SA_LOG_PLAN_MNG_BIZ();

		public P_CZ_SA_LOG_PLAN_MNG()
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
			this.MainGrids = new FlexGrid[] { this._flex차수, this._flex상용구, this._flex선적H, this._flex선적L, this._flex전달, this._flex대행실적, this._flex대행실적L };
			this._flex차수.DetailGrids = new FlexGrid[] { this._flex선적H, this._flex선적L, this._flex전달 };
			this._flex선적H.DetailGrids = new FlexGrid[] { this._flex선적L };
			this._flex대행실적.DetailGrids = new FlexGrid[] { this._flex대행실적L };

			#region 차수
			this._flex차수.BeginSetting(1, 1, false);

			this._flex차수.SetCol("NO_REV", "차수", 100);
			this._flex차수.SetCol("DT_WORK", "작업일자", 100, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex차수.SetCol("DC_RMK", "비고", 100, true);

			this._flex차수.SettingVersion = "1.0.0.1";
			this._flex차수.EndSetting(GridStyleEnum.Green, AllowSortingEnum.SingleColumn, SumPositionEnum.None);
			#endregion

			#region 상용구
			this._flex상용구.BeginSetting(1, 1, false);

			this._flex상용구.SetCol("DC_WORD", "상용구", 100);

			this._flex상용구.SetOneGridBinding(null, new IUParentControl[] { this.oneGrid3 });

			this._flex상용구.SettingVersion = "1.0.0.1";
			this._flex상용구.EndSetting(GridStyleEnum.Green, AllowSortingEnum.SingleColumn, SumPositionEnum.None);
			#endregion

			#region 협조전
			this._flex대상조회.BeginSetting(1, 1, false);

			this._flex대상조회.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
			this._flex대상조회.SetCol("NM_SUB_CATEGORY", "의뢰구분(소)", 100);
			this._flex대상조회.SetCol("NM_RMK", "협조내용", 100);
			this._flex대상조회.SetCol("NM_GI_PARTNER", "납품처명", 100);
			this._flex대상조회.SetCol("NM_EMP", "의뢰자", 100);
			this._flex대상조회.SetCol("QT_ITEM", "품목수", 100);
			this._flex대상조회.SetCol("NM_VESSEL", "호선명", 100);
			this._flex대상조회.SetCol("DT_COMPLETE", "완료예정일", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex대상조회.SetCol("NO_SO_PRE", "파일구분", 100);
			this._flex대상조회.SetCol("DC_RMK1", "취소사유", 100);
			this._flex대상조회.SetCol("NM_ITEMGRP", "품목유형", 100);
			this._flex대상조회.SetCol("NO_GIR", "의뢰번호", 100);
			this._flex대상조회.SetCol("DC_RESULT_PACK", "결과비고(요약)", 100);
			this._flex대상조회.SetCol("DC_RMK_CI", "상세요청", 100);
			this._flex대상조회.SetCol("DC_RMK3", "기포장정보", 100);
			this._flex대상조회.SetCol("DC_RESULT", "결과비고", 100);
			this._flex대상조회.SetCol("AM_GIR_10000", "원화금액(만단위)", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex대상조회.SetCol("DC_RMK_PL", "포장비고", 100);
			this._flex대상조회.SetCol("DTS_CONFIRM", "확정일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex대상조회.SetCol("DTS_UPDATE", "수정일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

			this._flex대상조회.Cols["DTS_CONFIRM"].Format = "####/##/##/##:##:##";
			this._flex대상조회.Cols["DTS_UPDATE"].Format = "####/##/##/##:##:##";

			this._flex대상조회.SetDummyColumn(new string[] { "S" });

			this._flex대상조회.SettingVersion = "1.0.0.1";
			this._flex대상조회.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			#endregion

			#region 선적

			#region Header
			this._flex선적H.BeginSetting(1, 1, false);

			this._flex선적H.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
			this._flex선적H.SetCol("DC_SORT", "정렬", 100, true);
			this._flex선적H.SetCol("DC_LOCATION", "장소", 100, true);
			this._flex선적H.SetCol("DC_PORT", "부두", 100, true);
			this._flex선적H.SetCol("NM_VESSEL", "호선명", 100, true);
			this._flex선적H.SetCol("DC_EMP", "작업자", 100, true);
			this._flex선적H.SetCol("DC_ETB", "ETB", 100, true);
			this._flex선적H.SetCol("DC_ETD", "ETD", 100, true);
			this._flex선적H.SetCol("DT_COMPLETE", "실행계획일", 100, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex선적H.SetCol("DC_CAR", "차량", 100, true);
			this._flex선적H.SetCol("DC_ITEM", "물량확인", 100);
			this._flex선적H.SetCol("DC_TEL", "연락처", 100, true);
			this._flex선적H.SetCol("NO_GIR", "협조전번호", 100);
			this._flex선적H.SetCol("DC_PACK", "포장정보", 100);
			this._flex선적H.SetCol("QT_PACK", "포장넓이", 100, false, typeof(decimal), FormatTpType.RATE);
			this._flex선적H.SetCol("DC_ETC", "기타정보", 100, true);

			this._flex선적H.SetOneGridBinding(null, new IUParentControl[] { this.pnl선적H });

			this._flex선적H.SetDummyColumn(new string[] { "S" });

			this._flex선적H.UseMultySorting = true;

			this._flex선적H.SettingVersion = "1.0.0.1";
			this._flex선적H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion

			#region Line
			this._flex선적L.BeginSetting(1, 1, false);

			this._flex선적L.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
			this._flex선적L.SetCol("NO_GIR", "협조전번호", 100);
			this._flex선적L.SetCol("DT_COMPLETE", "실행계획일", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex선적L.SetCol("DC_ITEM", "물량확인", 100);
			this._flex선적L.SetCol("DC_PACK", "포장정보", 100);
			this._flex선적L.SetCol("QT_PACK", "포장넓이", 100, false, typeof(decimal), FormatTpType.RATE);
			this._flex선적L.SetCol("DC_ETC", "기타정보", 100);

			this._flex선적L.SetOneGridBinding(null, new IUParentControl[] { this.pnl선적L });

			this._flex선적L.SetDummyColumn(new string[] { "S" });

			this._flex선적L.SettingVersion = "1.0.0.1";
			this._flex선적L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion

			#region 선박일정
			this._flex선박일정.BeginSetting(1, 1, false);

			this._flex선박일정.SetCol("NM_PRTAG", "항만청", 100);
			this._flex선박일정.SetCol("CLSGN", "CALL SIGN", 100);
			this._flex선박일정.SetCol("NM_VSSL", "선명", 100);
			this._flex선박일정.SetCol("DTS_ETRYPT", "입항", 100);
			this._flex선박일정.SetCol("DTS_TKOFF", "출항", 100);

			this._flex선박일정.SettingVersion = "1.0.0.1";
			this._flex선박일정.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion

			#endregion

			#region 전달
			this._flex전달.BeginSetting(1, 1, false);

			this._flex전달.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
			this._flex전달.SetCol("DC_LOCATION", "장소", 100, true);
			this._flex전달.SetCol("DC_TASK", "업무", 100, true);
			this._flex전달.SetCol("LN_PARTNER", "업체", 100, true);
			this._flex전달.SetCol("NM_EMP", "의뢰자", 100);
			this._flex전달.SetCol("QT_ITEM", "품목수", 100);
			this._flex전달.SetCol("NM_VESSEL", "호선명", 100);
			this._flex전달.SetCol("DT_COMPLETE", "기한", 100, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex전달.SetCol("NO_SO_PRE", "수주번호", 100);
			this._flex전달.SetCol("DC_UPDATE", "수정사항", 100, true);
			this._flex전달.SetCol("DC_LIMIT", "기한", 100, true);
			this._flex전달.SetCol("NO_GIR", "협조전번호", 100);
			this._flex전달.SetCol("DC_PACK", "포장정보", 100);
			this._flex전달.SetCol("QT_PACK", "포장넓이", 100, false, typeof(decimal), FormatTpType.RATE);
			this._flex전달.SetCol("DC_ETC", "기타정보", 100, true);

			this._flex전달.SetOneGridBinding(null, new IUParentControl[] { this.pnl전달 });

			this._flex전달.SetDummyColumn(new string[] { "S" });

			this._flex전달.UseMultySorting = true;

			this._flex전달.SettingVersion = "1.0.0.1";
			this._flex전달.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion

			#region 대행실적
			this._flex대행실적.BeginSetting(1, 1, false);

			this._flex대행실적.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
			this._flex대행실적.SetCol("DC_LOCATION", "장소", 100);
			this._flex대행실적.SetCol("DC_PORT", "부두", 100);
			this._flex대행실적.SetCol("NM_VESSEL", "호선명", 100);
			this._flex대행실적.SetCol("DC_EMP", "작업자", 100);
			this._flex대행실적.SetCol("DC_ETB", "묘박지", 100, true);
			this._flex대행실적.SetCol("DC_ETD", "대행비", 100, true);
			this._flex대행실적.SetCol("DT_COMPLETE", "실행계획일", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex대행실적.SetCol("DC_CAR", "용차비용", 100, true);
			this._flex대행실적.SetCol("DC_ITEM", "물량확인", 100);
			this._flex대행실적.SetCol("DC_TEL", "선적비고", 100, true);
			this._flex대행실적.SetCol("NO_GIR", "협조전번호", 100);
			this._flex대행실적.SetCol("DC_PACK", "포장정보", 100);
			this._flex대행실적.SetCol("QT_PACK", "포장넓이", 100, false, typeof(decimal), FormatTpType.RATE);
			this._flex대행실적.SetCol("DC_ETC", "기타정보", 100);

			this._flex대행실적.SetDummyColumn(new string[] { "S" });

			this._flex대행실적.UseMultySorting = true;

			this._flex대행실적.SettingVersion = "1.0.0.1";
			this._flex대행실적.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion

			#region 대행실적 Line
			this._flex대행실적L.BeginSetting(1, 1, false);

			this._flex대행실적L.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
			this._flex대행실적L.SetCol("NO_GIR", "협조전번호", 100);
			this._flex대행실적L.SetCol("DT_COMPLETE", "실행계획일", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex대행실적L.SetCol("DC_ITEM", "물량확인", 100);
			this._flex대행실적L.SetCol("DC_PACK", "포장정보", 100);
			this._flex대행실적L.SetCol("QT_PACK", "포장넓이", 100, false, typeof(decimal), FormatTpType.RATE);
			this._flex대행실적L.SetCol("DC_ETC", "기타정보", 100);

			this._flex대행실적L.SetDummyColumn(new string[] { "S" });

			this._flex대행실적L.SettingVersion = "1.0.0.1";
			this._flex대행실적L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion
		}

		private void InitEvent()
		{
			this._flex차수.AfterRowChange += _flex차수_AfterRowChange;
			this._flex선적H.AfterRowChange += _flex선적H_AfterRowChange;
			this._flex선적H.AfterEdit += _flex선적H_AfterEdit;
			this._flex대행실적.AfterRowChange += _flex대행실적_AfterRowChange;

			this.btn선적상세.Click += btn상세보기_Click;
			this.btn선적상세L.Click += btn상세보기_Click;
			this.btn전달상세.Click += btn상세보기_Click;

			this.btn선적추가.Click += Btn선적추가_Click;
			this.btn임의추가_선적.Click += Btn임의추가_선적_Click;
			this.btn선적삭제.Click += Btn선적삭제_Click;
			this.btn선적삭제L.Click += Btn선적삭제L_Click;
			this.btn전달추가.Click += Btn전달추가_Click;
			this.btn임의추가_전달.Click += Btn임의추가_전달_Click;
			this.btn전달삭제.Click += Btn전달삭제_Click;
			this.btn단독추가.Click += Btn단독추가_Click;
			this.btn타배선적.Click += Btn타배선적_Click;
			this.btn타배선적1.Click += Btn타배선적1_Click;
			this.btn대행실적.Click += Btn대행실적_Click;

			this.btn선적조회.Click += Btn협조전조회_Click;
			this.btn전달조회.Click += Btn협조전조회_Click;
			this.btn차수추가.Click += Btn차수추가_Click;
			this.btn차수삭제.Click += Btn차수삭제_Click;
			this.btn차수복사.Click += Btn차수복사_Click;
			this.btn상용구추가.Click += Btn상용구추가_Click;
			this.btn상용구삭제.Click += Btn상용구삭제_Click;
			this.btn일괄적용.Click += Btn일괄적용_Click;
			this.btn자동조정_선적.Click += Btn자동조정_Click;
			this.btn자동조정_전달.Click += Btn자동조정_Click;
			this.btn자동조정_대행실적.Click += Btn자동조정_Click;
			this.btn대행실적_조회.Click += Btn대행실적조회_Click;
			this.btn대행실적_삭제.Click += Btn대행실적_삭제_Click;

			this.btn설정저장.Click += Btn설정저장_Click;
			this.btn설정불러오기.Click += Btn설정불러오기_Click;
		}

		private void Btn설정불러오기_Click(object sender, EventArgs e)
		{
			try
			{
				if (Global.MainFrame.ShowMessage("해당 그리드의 설정을 불러오시겠습니까?", "QY2") != DialogResult.Yes)
					return;

				this.설정불러오기("P_CZ_SA_LOG_PLAN_MNG_VESSEL", this._flex선적H);
				this.설정불러오기("P_CZ_SA_LOG_PLAN_MNG_DELIVERY", this._flex전달);

				this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn설정불러오기.Text);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void 설정불러오기(string id, FlexGrid grid)
		{
			try
			{
				string query = "SELECT * FROM CZ_MA_USER_CONFIG_GRID WHERE CD_COMPANY = @CD_COMPANY AND NO_EMP = @NO_EMP AND GRID_UID = @GRID_UID ORDER BY SEQ";

				SQL sql = new SQL(query, SQLType.Text);
				sql.Parameter.Add2("@CD_COMPANY", Global.MainFrame.LoginInfo.CompanyCode);
				sql.Parameter.Add2("@NO_EMP", Global.MainFrame.LoginInfo.UserID);
				sql.Parameter.Add2("@GRID_UID", id);
				DataTable dt = sql.GetDataTable();

				foreach (DataRow row in dt.Rows)
				{
					if (!grid.Cols.Contains(row["COL_NAME"].ToString()))
						continue;

					int indexOld = grid.Cols[row["COL_NAME"].ToString()].Index;
					int indexNew = row["SEQ"].ToInt();

					// 순서 설정
					if (indexOld != indexNew)
						grid.Cols.Move(indexOld, indexNew);

					// 넓이 설정
					if (row["COL_WIDTH"].ToInt() > -1)
						grid.Cols[indexNew].Width = row["COL_WIDTH"].ToInt();

					// Visible 설정
					grid.Cols[indexNew].Visible = row["VISIBLE"].ToBoolean();
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn설정저장_Click(object sender, EventArgs e)
		{
			try
			{
				if (Global.MainFrame.ShowMessage("해당 그리드의 설정을 저장 하시겠습니까?", "QY2") != DialogResult.Yes)
					return;

				this.설정저장("P_CZ_SA_LOG_PLAN_MNG_VESSEL", this._flex선적H);
				this.설정저장("P_CZ_SA_LOG_PLAN_MNG_DELIVERY", this._flex전달);

				this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn설정저장.Text);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void 설정저장(string id, FlexGrid grid)
		{
			try
			{
				DataTable dt = new DataTable();
				dt.Columns.Add("SEQ", typeof(int));
				dt.Columns.Add("COL_NAME", typeof(string));
				dt.Columns.Add("COL_WIDTH", typeof(int));
				dt.Columns.Add("VISIBLE", typeof(int));

				for (int i = 1; i < grid.Cols.Count; i++)
					dt.Rows.Add(i, grid.Cols[i].Name, grid.Cols[i].Width, grid.Cols[i].Visible ? 1 : 0);

				SQL sql = new SQL("PX_CZ_MA_USER_CONFIG_GRID", SQLType.Procedure);
				sql.Parameter.Add2("@CD_COMPANY", Global.MainFrame.LoginInfo.CompanyCode);
				sql.Parameter.Add2("@NO_EMP", Global.MainFrame.LoginInfo.UserID);
				sql.Parameter.Add2("@GRID_UID", id);
				sql.Parameter.Add2("@XML", dt.ToXml());
				sql.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn대행실적_삭제_Click(object sender, EventArgs e)
		{
			DataRow[] dataRowArray;

			try
			{
				if (!this._flex대행실적.HasNormalRow) return;

				dataRowArray = this._flex대행실적.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					this._flex대행실적.Redraw = false;

					foreach (DataRow dr in this._flex대행실적.DataTable.Select("S = 'Y'"))
					{
						dr.Delete();
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
			finally
			{
				this._flex대행실적.Redraw = true;
			}
		}

		private void _flex대행실적_AfterRowChange(object sender, RangeEventArgs e)
		{
			DataTable dt;
			string filter;

			try
			{
				dt = null;
				filter = string.Format("NO_IDX = '{0}'", this._flex대행실적["NO_IDX"].ToString());

				if (this._flex대행실적.DetailQueryNeed == true)
				{
					dt = this._biz.Search대행실적Line(new object[] { this._flex대행실적["NO_IDX"].ToString() });
				}

				this._flex대행실적L.BindingAdd(dt, filter);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn대행실적_Click(object sender, EventArgs e)
		{
			DataRow[] dataRowArray;
			int 순번;

			try
			{
				if (!this._flex선적H.HasNormalRow) return;
				if (this._flex대행실적.DataTable == null) return;

				dataRowArray = this._flex선적H.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					this._flex대행실적.Redraw = false;
					this._flex대행실적L.Redraw = false;
					this._flex선적H.Redraw = false;

					foreach (DataRow dr in this._flex선적H.DataTable.Select("S = 'Y'"))
					{
						순번 = this.Get대행실적순번();

						this._flex대행실적.DataTable.ImportRow(dr);
						this._flex대행실적.DataTable.Rows[this._flex대행실적.DataTable.Rows.Count - 1]["NO_IDX"] = 순번;
						this._flex대행실적.DataTable.Rows[this._flex대행실적.DataTable.Rows.Count - 1].AcceptChanges();
						this._flex대행실적.DataTable.Rows[this._flex대행실적.DataTable.Rows.Count - 1].SetAdded();

						foreach (DataRow dr1 in this._flex선적L.DataTable.Select(string.Format("NO_REV = '{0}' AND NO_IDX = '{1}'", dr["NO_REV"].ToString(),
																																	dr["NO_IDX"].ToString())))
						{
							this._flex대행실적L.DataTable.ImportRow(dr1);
							this._flex대행실적L.DataTable.Rows[this._flex대행실적L.DataTable.Rows.Count - 1]["NO_IDX"] = 순번;
							this._flex대행실적L.DataTable.Rows[this._flex대행실적L.DataTable.Rows.Count - 1].AcceptChanges();
							this._flex대행실적L.DataTable.Rows[this._flex대행실적L.DataTable.Rows.Count - 1].SetAdded();
						}

						dr.Delete();
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
			finally
			{
				this._flex대행실적.Redraw = true;
				this._flex대행실적L.Redraw = true;
				this._flex선적H.Redraw = true;
			}
		}

		private void Btn대행실적조회_Click(object sender, EventArgs e)
		{
			DataTable dt;

			try
			{
				dt = this._biz.Search대행실적(new object[] { this.dtp실행계획일S.StartDateToString,
															 this.dtp실행계획일S.EndDateToString });

				this._flex대행실적.Binding = dt;

				if (!this._flex대행실적.HasNormalRow)
				{
					Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
				}

				this._flex대행실적L.Binding = this._biz.Search대행실적Line(new object[] { "999" });
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn타배선적_Click(object sender, EventArgs e)
		{
			DataRow[] dataRowArray;

			try
			{
				if (this._flex차수["NO_REV"] == null) return;

				dataRowArray = this._flex대상조회.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					this._flex대상조회.Redraw = false;
					this._flex선적L.Redraw = false;

					foreach (DataRow dr in dataRowArray)
					{
						this._flex선적L.Rows.Add();
						this._flex선적L.Row = this._flex선적L.Rows.Count - 1;

						this._flex선적L["CD_COMPANY"] = dr["CD_COMPANY"].ToString();
						this._flex선적L["NO_REV"] = this._flex차수["NO_REV"].ToString();

						this._flex선적L["NO_IDX"] = this._flex선적H["NO_IDX"];
						this._flex선적L["NO_GIR"] = dr["NO_GIR"].ToString();
						this._flex선적L["DT_COMPLETE"] = dr["DT_COMPLETE"].ToString();
						this._flex선적L["DC_ITEM"] = dr["DC_RMK_QTY"].ToString();
						this._flex선적L["DC_ETC"] = dr["DC_RMK_CI"].ToString();

						this._flex선적L.AddFinished();
						this._flex선적L.Col = this._flex선적L.Cols.Fixed;
						this._flex선적L.Focus();

						dr.Delete();
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
			finally
			{
				this._flex대상조회.Redraw = true;
				this._flex선적L.Redraw = true;
			}
		}

		private void Btn타배선적1_Click(object sender, EventArgs e)
		{
			DataRow[] dataRowArray;

			try
			{
				if (this._flex차수["NO_REV"] == null) return;

				dataRowArray = this._flex선적L.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					this._flex선적H.Redraw = false;
					this._flex선적L.Redraw = false;

					DataTable dt = ComFunc.getGridGroupBy(dataRowArray.ToDataTable(), new string[] { "NO_REV", "NO_IDX" }, true);

					foreach (DataRow dr in dataRowArray)
					{
						this._flex선적L.Rows.Add();
						this._flex선적L.Row = this._flex선적L.Rows.Count - 1;

						this._flex선적L["CD_COMPANY"] = dr["CD_COMPANY"].ToString();
						this._flex선적L["NO_REV"] = this._flex차수["NO_REV"].ToString();

						this._flex선적L["NO_IDX"] = this._flex선적H["NO_IDX"];
						this._flex선적L["NO_GIR"] = dr["NO_GIR"].ToString();
						this._flex선적L["DT_COMPLETE"] = dr["DT_COMPLETE"].ToString();
						this._flex선적L["DC_ITEM"] = dr["DC_ITEM"].ToString();
						this._flex선적L["DC_ETC"] = dr["DC_ETC"].ToString();

						this._flex선적L.AddFinished();
						this._flex선적L.Col = this._flex선적L.Cols.Fixed;
						this._flex선적L.Focus();

						dr.Delete();
					}

					foreach (DataRow dr in dt.Rows)
					{
						DataRow drH = this._flex선적H.DataTable.Select(string.Format("NO_REV = '{0}' AND NO_IDX = '{1}'", dr["NO_REV"].ToString(), dr["NO_IDX"].ToString()))[0];

						if (this._flex선적L.DataTable.Select(string.Format("NO_REV = '{0}' AND NO_IDX = '{1}'", dr["NO_REV"].ToString(), dr["NO_IDX"].ToString())).Length == 0)
						{
							drH.Delete();
							continue;
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
			finally
			{
				this._flex선적H.Redraw = true;
				this._flex선적L.Redraw = true;
			}
		}

		private void Btn단독추가_Click(object sender, EventArgs e)
		{
			DataRow[] dataRowArray;

			try
			{
				if (this._flex차수["NO_REV"] == null) return;

				dataRowArray = this._flex대상조회.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					this._flex대상조회.Redraw = false;
					this._flex선적H.Redraw = false;
					this._flex선적L.Redraw = false;

					foreach (DataRow dr in this._flex대상조회.DataTable.Select("S = 'Y'"))
					{
						if (this._flex선적H.DataTable.Select(string.Format("NO_REV = '{0}' AND NO_GIR LIKE '%{1}%'", this._flex차수["NO_REV"].ToString(), dr["NO_GIR"].ToString())).Length == 0)
						{
							this._flex선적H.Rows.Add();
							this._flex선적H.Row = this._flex선적H.Rows.Count - 1;

							this._flex선적H["CD_COMPANY"] = dr["CD_COMPANY"].ToString();
							this._flex선적H["NO_REV"] = this._flex차수["NO_REV"].ToString();
							this._flex선적H["NO_IDX"] = this.Get선적순번(this._flex차수["NO_REV"].ToString());
							this._flex선적H["NO_IMO"] = dr["NO_IMO"].ToString();
							this._flex선적H["NM_VESSEL"] = dr["NM_VESSEL"].ToString();
							this._flex선적H["DT_COMPLETE"] = dr["DT_COMPLETE"].ToString();
							this._flex선적H["DC_ITEM"] = dr["DC_RMK_QTY"].ToString();
							this._flex선적H["DC_ETC"] = dr["DC_RMK_CI"].ToString();
							this._flex선적H["NO_GIR"] = dr["NO_GIR"].ToString();

							this._flex선적H.AddFinished();
							this._flex선적H.Col = this._flex선적H.Cols.Fixed;
							this._flex선적H.Focus();
						}
					}

					foreach (DataRow dr in this._flex대상조회.DataTable.Select("S = 'Y'"))
					{
						if (this._flex선적H.DataTable.Select(string.Format("NO_REV = '{0}' AND NO_GIR LIKE '%{1}%'", this._flex차수["NO_REV"].ToString(), dr["NO_GIR"].ToString())).Length == 0)
						{
							this._flex선적L.Rows.Add();
							this._flex선적L.Row = this._flex선적L.Rows.Count - 1;

							this._flex선적L["CD_COMPANY"] = dr["CD_COMPANY"].ToString();
							this._flex선적L["NO_REV"] = this._flex차수["NO_REV"].ToString();
							this._flex선적L["NO_IDX"] = this._flex선적H.DataTable.Select(string.Format("NO_REV = '{0}' AND NO_IMO = '{1}'", this._flex차수["NO_REV"].ToString(),
																																			dr["NO_IMO"].ToString()))[0]["NO_IDX"];
							this._flex선적L["NO_GIR"] = dr["NO_GIR"].ToString();
							this._flex선적L["DT_COMPLETE"] = dr["DT_COMPLETE"].ToString();
							this._flex선적L["DC_ITEM"] = dr["DC_RMK_QTY"].ToString();
							this._flex선적L["DC_ETC"] = dr["DC_RMK_CI"].ToString();

							this._flex선적L.AddFinished();
							this._flex선적L.Col = this._flex선적L.Cols.Fixed;
							this._flex선적L.Focus();

							dr.Delete();
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
			finally
			{
				this._flex대상조회.Redraw = true;
				this._flex선적H.Redraw = true;
				this._flex선적L.Redraw = true;
			}
		}

		private void Btn자동조정_Click(object sender, EventArgs e)
		{
			string name;

			try
			{
				name = ((Control)sender).Name;

				if (name == this.btn자동조정_선적.Name)
					this._flex선적H.AutoSizeRows();
				else if (name == this.btn자동조정_대행실적.Name)
					this._flex대행실적.AutoSizeRows();
				else
					this._flex전달.AutoSizeRows();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		protected override void InitPaint()
		{
			base.InitPaint();

			this.dtp확정일시.CustomFormat = "yyyy/MM/dd tt hh:mm ~";

			this.dtp실행계획일S.StartDateToString = Global.MainFrame.GetDateTimeToday().AddMonths(-3).ToString("yyyyMMdd");
			this.dtp실행계획일S.EndDateToString = Global.MainFrame.GetStringToday;

			this.dtp작업일자.StartDateToString = Global.MainFrame.GetDateTimeToday().AddMonths(-1).ToString("yyyyMMdd");
			this.dtp작업일자.EndDateToString = Global.MainFrame.GetStringToday;

			this.split선적.SplitterDistance = Settings1.Default.선적;
			this.split선적상세.SplitterDistance = Settings1.Default.선적상세;
			this.split전달.SplitterDistance = Settings1.Default.전달;

			this.split선적.Panel2Collapsed = true;
			this.split선적상세.Panel2Collapsed = true;
			this.split전달.Panel2Collapsed = true;
		}

		protected override bool BeforeSearch()
		{
			if (!base.BeforeSearch()) return false;

			return true;
		}

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarSearchButtonClicked(sender, e);

				if (!this.BeforeSearch()) return;

				this._flex차수.Binding = this._biz.Search차수(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																			 this.dtp작업일자.StartDateToString,
																			 this.dtp작업일자.EndDateToString });
				this._flex상용구.Binding = this._biz.Search상용구(new object[] { Global.MainFrame.LoginInfo.CompanyCode });

				if (!this._flex차수.HasNormalRow)
				{
					Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
				}
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

				if (MsgAndSave(PageActionMode.Save))
					this.ShowMessage(PageResultMode.SaveGood);
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
		{
			try
			{
				Settings1.Default.선적 = this.split선적.SplitterDistance;
				Settings1.Default.선적상세 = this.split선적상세.SplitterDistance;
				Settings1.Default.전달 = this.split전달.SplitterDistance;

				Settings1.Default.Save();

				return base.OnToolBarExitButtonClicked(sender, e);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}

			return false;
		}

		protected override bool SaveData()
		{
			if (!base.SaveData() || !base.Verify() || !this.BeforeSave()) return false;

			if (this._flex차수.IsDataChanged == false &&
				this._flex상용구.IsDataChanged == false &&
				this._flex선적H.IsDataChanged == false &&
				this._flex선적L.IsDataChanged == false &&
				this._flex전달.IsDataChanged == false &&
				this._flex대행실적.IsDataChanged == false &&
				this._flex대행실적L.IsDataChanged == false) return false;

			if (!this._biz.Save(this._flex차수.GetChanges(),
								this._flex상용구.GetChanges(),
								this._flex선적H.GetChanges(),
								this._flex선적L.GetChanges(),
								this._flex전달.GetChanges(),
								this._flex대행실적.GetChanges(),
								this._flex대행실적L.GetChanges())) return false;

			this._flex차수.AcceptChanges();
			this._flex상용구.AcceptChanges();
			this._flex선적H.AcceptChanges();
			this._flex선적L.AcceptChanges();
			this._flex전달.AcceptChanges();
			this._flex대행실적.AcceptChanges();
			this._flex대행실적L.AcceptChanges();

			return true;
		}

		private void _flex선적H_AfterRowChange(object sender, RangeEventArgs e)
		{
			DataTable dt;
			string filter, query;

			try
			{
				dt = null;
				filter = string.Format("NO_REV = '{0}' AND NO_IDX = '{1}'", this._flex선적H["NO_REV"].ToString(),
																			this._flex선적H["NO_IDX"].ToString());

				if (this._flex선적H.DetailQueryNeed == true)
				{
					dt = this._biz.Search선적Detail(new object[] { this._flex선적H["NO_REV"].ToString(),
																   this._flex선적H["NO_IDX"].ToString() });
				}

				this._flex선적L.BindingAdd(dt, filter);

				if (this.chk자동조회.Checked == true)
				{
					query = @"SELECT NM_PRTAG,
       CLSGN,
       NM_VSSL,
       (SUBSTRING(DTS_ETRYPT, 5, 2) + '.' + SUBSTRING(DTS_ETRYPT, 7, 2) + '.' + SUBSTRING(DTS_ETRYPT, 9, 4)) AS DTS_ETRYPT,
       (SUBSTRING(DTS_TKOFF, 5, 2) + '.' + SUBSTRING(DTS_TKOFF, 7, 2) + '.' + SUBSTRING(DTS_TKOFF, 9, 4)) AS DTS_TKOFF
FROM CZ_SA_VSSL_ETRYNDH WITH(NOLOCK)
WHERE DTS_ETRYPT >= CONVERT(CHAR(8), GETDATE(), 112)
AND EXISTS (SELECT 1 
            FROM CZ_MA_HULL MH WITH(NOLOCK)
            WHERE MH.NO_IMO = '{0}'
            AND (CLSGN = MH.CALL_SIGN OR NM_VSSL = MH.NM_VESSEL))
ORDER BY CLSGN, DTS_ETRYPT";

					dt = DBHelper.GetDataTable(string.Format(query, this._flex선적H["NO_IMO"].ToString()));

					this._flex선박일정.Binding = dt;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flex차수_AfterRowChange(object sender, RangeEventArgs e)
		{
			DataTable dt1, dt2;
			string filter;

			try
			{
				if (!this._flex차수.HasNormalRow) return;

				if (this._flex차수["IDX"].ToString() == "1")
					this.ControlEnable(true);
				else
					this.ControlEnable(false);

				dt1 = null;
				dt2 = null;
				filter = string.Format("NO_REV = '{0}'", this._flex차수["NO_REV"].ToString());

				if (this._flex차수.DetailQueryNeed == true)
				{
					dt1 = this._biz.Search선적(new object[] { this._flex차수["NO_REV"].ToString() });

					dt2 = this._biz.Search전달(new object[] { this._flex차수["NO_REV"].ToString() });
				}

				this._flex선적H.BindingAdd(dt1, filter);
				this._flex전달.BindingAdd(dt2, filter);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

	    private void ControlEnable(bool isEnable)
		{
			this.btn임의추가_선적.Enabled = isEnable;
			this.btn단독추가.Enabled = isEnable;
			this.btn대행실적.Enabled = isEnable;
			this.btn선적추가.Enabled = isEnable;
			this.btn선적삭제.Enabled = isEnable;
			this.btn차수복사.Enabled = isEnable;
			this.btn타배선적.Enabled = isEnable;
			this.btn타배선적1.Enabled = isEnable;
			this.btn선적삭제L.Enabled = isEnable;

			this.btn일괄적용.Enabled = isEnable;
			this.btn임의추가_전달.Enabled = isEnable;
			this.btn전달추가.Enabled = isEnable;
			this.btn전달삭제.Enabled = isEnable;

			this._flex선적H.Cols["DC_SORT"].AllowEditing = isEnable;
			this._flex선적H.Cols["DC_LOCATION"].AllowEditing = isEnable;
			this._flex선적H.Cols["DC_PORT"].AllowEditing = isEnable;
			this._flex선적H.Cols["NM_VESSEL"].AllowEditing = isEnable;
			this._flex선적H.Cols["DC_EMP"].AllowEditing = isEnable;
			this._flex선적H.Cols["DC_ETB"].AllowEditing = isEnable;
			this._flex선적H.Cols["DC_ETD"].AllowEditing = isEnable;
			this._flex선적H.Cols["DT_COMPLETE"].AllowEditing = isEnable;
			this._flex선적H.Cols["DC_CAR"].AllowEditing = isEnable;
			this._flex선적H.Cols["DC_ITEM"].AllowEditing = isEnable;
			this._flex선적H.Cols["DC_TEL"].AllowEditing = isEnable;
			this._flex선적H.Cols["NO_GIR"].AllowEditing = isEnable;
			this._flex선적H.Cols["DC_ETC"].AllowEditing = isEnable;

			this._flex전달.Cols["DC_LOCATION"].AllowEditing = isEnable;
			this._flex전달.Cols["DC_TASK"].AllowEditing = isEnable;
			this._flex전달.Cols["LN_PARTNER"].AllowEditing = isEnable;
			this._flex전달.Cols["DT_COMPLETE"].AllowEditing = isEnable;
			this._flex전달.Cols["DC_UPDATE"].AllowEditing = isEnable;
			this._flex전달.Cols["DC_LIMIT"].AllowEditing = isEnable;
			this._flex전달.Cols["DC_ETC"].AllowEditing = isEnable;
		}

		private void _flex선적H_AfterEdit(object sender, RowColEventArgs e)
		{
			FlexGrid flexGrid;
			string columnName;

			try
			{
				flexGrid = ((FlexGrid)sender);

				if (flexGrid.HasNormalRow == false) return;

				columnName = flexGrid.Cols[e.Col].Name;

				if (columnName == "DT_COMPLETE")
				{
					foreach (DataRow dr in this._flex선적L.DataTable.Select(string.Format("NO_REV = '{0}' AND NO_IDX = '{1}'", this._flex선적H["NO_REV"].ToString(), this._flex선적H["NO_IDX"].ToString())))
					{
						dr["DT_COMPLETE"] = this._flex선적H["DT_COMPLETE"].ToString();
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn선적삭제L_Click(object sender, EventArgs e)
		{
			DataTable dt;
			DataRow[] dataRowArray;

			try
			{
				if (!this._flex선적L.HasNormalRow) return;
				if (this._flex대상조회.DataTable == null) return;

				dataRowArray = this._flex선적L.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					this._flex대상조회.Redraw = false;
					this._flex선적L.Redraw = false;

					foreach (DataRow dr in this._flex선적L.DataTable.Select("S = 'Y'"))
					{
						dt = this._biz.Search협조전1(new object[] { dr["CD_COMPANY"].ToString(), dr["NO_GIR"].ToString() });

						this._flex대상조회.DataTable.ImportRow(dt.Rows[0]);

						dr.Delete();
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
			finally
			{
				this._flex대상조회.Redraw = true;
				this._flex선적L.Redraw = true;
			}
		}

		private void btn상세보기_Click(object sender, EventArgs e)
		{
			string name;

			try
			{
				name = ((Control)sender).Name;

				if (name == this.btn선적상세.Name)
				{
					if (this.split선적.Panel2Collapsed)
						this.split선적.Panel2Collapsed = false;
					else
						this.split선적.Panel2Collapsed = true;
				}
				else if (name == this.btn선적상세L.Name)
				{
					if (this.split선적상세.Panel2Collapsed)
						this.split선적상세.Panel2Collapsed = false;
					else
						this.split선적상세.Panel2Collapsed = true;
				}
				else if (name == this.btn전달상세.Name)
				{
					if (this.split전달.Panel2Collapsed)
						this.split전달.Panel2Collapsed = false;
					else
						this.split전달.Panel2Collapsed = true;
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void Btn선적추가_Click(object sender, EventArgs e)
		{
			DataRow[] dataRowArray;
			string query;

			try
			{
				if (this._flex차수["NO_REV"] == null) return;

				dataRowArray = this._flex대상조회.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					this._flex대상조회.Redraw = false;
					this._flex선적H.Redraw = false;
					this._flex선적L.Redraw = false;

					query = @"SELECT PS.NO_GIR 
FROM CZ_SA_LOG_PLAN_SHIP PS WITH(NOLOCK)
WHERE PS.NO_REV = '{0}'
AND PS.NO_GIR = '{1}'";

					foreach (DataRow dr in this._flex대상조회.DataTable.Select("S = 'Y'"))
					{
						if (this._flex선적H.DataTable.Select(string.Format("NO_REV = '{0}' AND NO_IMO = '{1}'", this._flex차수["NO_REV"].ToString(),
																												dr["NO_IMO"].ToString())).Length == 0)
						{
							this._flex선적H.Rows.Add();
							this._flex선적H.Row = this._flex선적H.Rows.Count - 1;

							this._flex선적H["CD_COMPANY"] = dr["CD_COMPANY"].ToString();
							this._flex선적H["NO_REV"] = this._flex차수["NO_REV"].ToString();
							this._flex선적H["NO_IDX"] = this.Get선적순번(this._flex차수["NO_REV"].ToString());
							this._flex선적H["NO_IMO"] = dr["NO_IMO"].ToString();
							this._flex선적H["NM_VESSEL"] = dr["NM_VESSEL"].ToString();
							this._flex선적H["DT_COMPLETE"] = dr["DT_COMPLETE"].ToString();
							this._flex선적H["DC_ITEM"] = dr["DC_RMK_QTY"].ToString();
							this._flex선적H["DC_ETC"] = dr["DC_RMK_CI"].ToString();
							this._flex선적H["NO_GIR"] = dr["NO_GIR"].ToString();

							this._flex선적H.AddFinished();
							this._flex선적H.Col = this._flex선적H.Cols.Fixed;
							this._flex선적H.Focus();
						}
					}

					foreach (DataRow dr in this._flex대상조회.DataTable.Select("S = 'Y'"))
					{
						if (this._flex선적L.DataTable.Select(string.Format("NO_REV = '{0}' AND NO_GIR = '{1}'", this._flex차수["NO_REV"].ToString(), dr["NO_GIR"].ToString())).Length == 0)
						{
							DataTable dt = DBHelper.GetDataTable(string.Format(query, new string[] { this._flex차수["NO_REV"].ToString(), dr["NO_GIR"].ToString() }));

							if (dt != null && dt.Rows.Count > 0) continue;

							this._flex선적L.Rows.Add();
							this._flex선적L.Row = this._flex선적L.Rows.Count - 1;

							this._flex선적L["CD_COMPANY"] = dr["CD_COMPANY"].ToString();
							this._flex선적L["NO_REV"] = this._flex차수["NO_REV"].ToString();
							this._flex선적L["NO_IDX"] = this._flex선적H.DataTable.Select(string.Format("NO_REV = '{0}' AND NO_IMO = '{1}'", this._flex차수["NO_REV"].ToString(),
																																			dr["NO_IMO"].ToString()))[0]["NO_IDX"];
							this._flex선적L["NO_GIR"] = dr["NO_GIR"].ToString();
							this._flex선적L["DT_COMPLETE"] = dr["DT_COMPLETE"].ToString();
							this._flex선적L["DC_ITEM"] = dr["DC_RMK_QTY"].ToString();
							this._flex선적L["DC_ETC"] = dr["DC_RMK_CI"].ToString();

							this._flex선적L.AddFinished();
							this._flex선적L.Col = this._flex선적L.Cols.Fixed;
							this._flex선적L.Focus();

							dr.Delete();
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
			finally
			{
				this._flex대상조회.Redraw = true;
				this._flex선적H.Redraw = true;
				this._flex선적L.Redraw = true;
			}
		}

		private void Btn선적삭제_Click(object sender, EventArgs e)
		{
			DataRow[] dataRowArray;

			try
			{
				if (!this._flex선적H.HasNormalRow) return;

				dataRowArray = this._flex선적H.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					this._flex선적H.Redraw = false;

					foreach (DataRow dr in this._flex선적H.DataTable.Select("S = 'Y'"))
					{
						dr.Delete();
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
			finally
			{
				this._flex선적H.Redraw = true;
			}
		}

		private void Btn전달추가_Click(object sender, EventArgs e)
		{
			DataRow[] dataRowArray;

			try
			{
				if (this._flex차수["NO_REV"] == null) return;

				dataRowArray = this._flex대상조회.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					this._flex전달.Redraw = false;

					foreach (DataRow dr in this._flex대상조회.DataTable.Select("S = 'Y'"))
					{
						DataRow[] dataRowArray1 = this._flex전달.DataTable.Select(string.Format("NO_REV = '{0}' AND NO_GIR = '{1}'", this._flex차수["NO_REV"].ToString(), dr["NO_GIR"].ToString()));

						if (dataRowArray1.Length > 0)
						{
							dataRowArray1[0]["YN_UPDATE"] = "Y";
						}
						else
						{
							this._flex전달.Rows.Add();
							this._flex전달.Row = this._flex전달.Rows.Count - 1;

							this._flex전달["NO_REV"] = this._flex차수["NO_REV"].ToString();
							this._flex전달["NO_IDX"] = this.Get전달순번(this._flex차수["NO_REV"].ToString());
							this._flex전달["CD_COMPANY"] = dr["CD_COMPANY"].ToString();
							this._flex전달["DC_LOCATION"] = dr["NM_SUB_CATEGORY"].ToString();
							this._flex전달["CD_PARTNER"] = dr["GI_PARTNER"].ToString();
							this._flex전달["LN_PARTNER"] = dr["NM_GI_PARTNER"].ToString();
							this._flex전달["NO_EMP"] = dr["NO_EMP"].ToString();
							this._flex전달["NM_EMP"] = dr["NM_EMP"].ToString();
							this._flex전달["QT_ITEM"] = dr["QT_ITEM"].ToString();
							this._flex전달["NO_IMO"] = dr["NO_IMO"].ToString();
							this._flex전달["NM_VESSEL"] = dr["NM_VESSEL"].ToString();
							this._flex전달["DT_COMPLETE"] = dr["DT_COMPLETE"].ToString();
							this._flex전달["NO_SO_PRE"] = dr["NO_SO_PRE"].ToString();
							this._flex전달["DC_UPDATE"] = dr["DC_RMK1"].ToString();
							this._flex전달["NO_GIR"] = dr["NO_GIR"].ToString();
							this._flex전달["DC_ETC"] = dr["DC_RMK_CI"].ToString();
							this._flex전달["YN_UPDATE"] = "N";

							this._flex전달.AddFinished();
							this._flex전달.Col = this._flex전달.Cols.Fixed;
							this._flex전달.Focus();

							dr.Delete();
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
			finally
			{
				this._flex전달.Redraw = true;
			}
		}

		private void Btn전달삭제_Click(object sender, EventArgs e)
		{
			DataTable dt;
			DataRow[] dataRowArray;

			try
			{
				if (!this._flex전달.HasNormalRow) return;
				if (this._flex대상조회.DataTable == null) return;

				dataRowArray = this._flex전달.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					this._flex전달.Redraw = false;
					this._flex대상조회.Redraw = false;

					foreach (DataRow dr in this._flex전달.DataTable.Select("S = 'Y'"))
					{
						dt = this._biz.Search협조전1(new object[] { dr["CD_COMPANY"].ToString(), dr["NO_GIR"].ToString() });

						this._flex대상조회.DataTable.ImportRow(dt.Rows[0]);

						dr.Delete();
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
			finally
			{
				this._flex전달.Redraw = true;
				this._flex대상조회.Redraw = true;
			}
		}

		private void Btn상용구삭제_Click(object sender, EventArgs e)
		{
			try
			{
				if (Global.MainFrame.ShowMessage("선택된 상용구를 삭제 하시겠습니까?", "QY2") != DialogResult.Yes)
					return;

				this._flex상용구.RemoveItem(this._flex상용구.Row);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn상용구추가_Click(object sender, EventArgs e)
		{
			try
			{
				this._flex상용구.Rows.Add();
				this._flex상용구.Row = this._flex상용구.Rows.Count - 1;

				this._flex상용구["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;

				this._flex상용구.AddFinished();
				this._flex상용구.Col = this._flex상용구.Cols.Fixed;
				this._flex상용구.Focus();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn차수복사_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flex차수.HasNormalRow) return;

				DBHelper.ExecuteNonQuery("SP_CZ_SA_LOG_PLAN_MNG_REV_COPY", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																						  this._flex차수["NO_REV"].ToString(),
																						  Global.MainFrame.LoginInfo.UserID });

				this.OnToolBarSearchButtonClicked(null, null);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn차수추가_Click(object sender, EventArgs e)
		{
			try
			{
				this._flex차수.Rows.Add();
				this._flex차수.Row = this._flex차수.Rows.Count - 1;

				this._flex차수["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
				this._flex차수["NO_REV"] = this.Get차수();

				this._flex차수.AddFinished();
				this._flex차수.Col = this._flex차수.Cols.Fixed;
				this._flex차수.Focus();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn차수삭제_Click(object sender, EventArgs e)
		{
			try
			{
				if (Global.MainFrame.ShowMessage("선택된 차수를 삭제 하시겠습니까?", "QY2") != DialogResult.Yes)
					return;

				this._flex차수.RemoveItem(this._flex차수.Row);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn협조전조회_Click(object sender, EventArgs e)
		{
			DataTable dt = null;
			string name;

			try
			{
				name = ((Control)sender).Name;
				string 확정일자 = string.Format("{0:0000}", this.dtp확정일시.Value.Year) +
								  string.Format("{0:00}", this.dtp확정일시.Value.Month) +
								  string.Format("{0:00}", this.dtp확정일시.Value.Day) +
								  string.Format("{0:00}", this.dtp확정일시.Value.Hour) +
								  string.Format("{0:00}", this.dtp확정일시.Value.Minute) +
								  "00";

				if (name == this.btn선적조회.Name)
				{
					dt = this._biz.Search협조전(true, new object[] { 확정일자,
																	 this.ctx호선.CodeValue,
																	 this.txt협조전번호.Text });
				}
				else if (name == this.btn전달조회.Name)
				{
					dt = this._biz.Search협조전(false, new object[] { 확정일자,
																	  this.ctx호선.CodeValue,
																	  this.txt협조전번호.Text });
				}

				this._flex대상조회.Binding = dt;

				if (!this._flex대상조회.HasNormalRow)
				{
					Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn임의추가_전달_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flex차수.HasNormalRow) return;
				if (this._flex차수["NO_REV"] == null) return;

				this._flex전달.Rows.Add();
				this._flex전달.Row = this._flex전달.Rows.Count - 1;

				this._flex전달["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
				this._flex전달["NO_REV"] = this._flex차수["NO_REV"].ToString();
				this._flex전달["NO_IDX"] = this.Get전달순번(this._flex차수["NO_REV"].ToString());

				this._flex전달.AddFinished();
				this._flex전달.Col = this._flex전달.Cols.Fixed;
				this._flex전달.Focus();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn임의추가_선적_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flex차수.HasNormalRow) return;
				if (this._flex차수["NO_REV"] == null) return;

				this._flex선적H.Rows.Add();
				this._flex선적H.Row = this._flex선적H.Rows.Count - 1;

				this._flex선적H["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
				this._flex선적H["NO_REV"] = this._flex차수["NO_REV"].ToString();
				this._flex선적H["NO_IDX"] = this.Get선적순번(this._flex차수["NO_REV"].ToString());

				this._flex선적H.AddFinished();
				this._flex선적H.Col = this._flex선적H.Cols.Fixed;
				this._flex선적H.Focus();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn일괄적용_Click(object sender, EventArgs e)
		{
			DataRow[] dataRowArray;
			string 컬럼, 값;

			try
			{
				dataRowArray = this._flex전달.DataTable.Select("S = 'Y'");
				컬럼 = this._flex전달.Cols[this._flex전달.Col].Name;
				값 = this._flex전달[컬럼].ToString();

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					foreach (DataRow dr in dataRowArray)
					{
						dr[컬럼] = 값;
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private Decimal Get차수()
		{
			Decimal num = 0, num1 = 0;
			DataTable dataTable = DBHelper.GetDataTable(@"SELECT MAX(NO_REV) AS NO_REV 
                                                          FROM CZ_SA_LOG_PLAN_REV WITH(NOLOCK)  
                                                          WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'");

			if (dataTable != null && dataTable.Rows.Count != 0)
				num = D.GetDecimal(dataTable.Rows[0]["NO_REV"]);

			num1 = D.GetDecimal(this._flex차수.DataTable.Compute("MAX(NO_REV)", string.Empty));

			if (num >= num1)
				return (num + 1);
			else
				return (num1 + 1);
		}

		private Decimal Get선적순번(string 차수)
		{
			Decimal num = 0, num1 = 0;
			DataTable dataTable = DBHelper.GetDataTable(string.Format(@"SELECT MAX(NO_IDX) AS NO_IDX 
																		FROM CZ_SA_LOG_PLAN_VESSEL WITH(NOLOCK)  
																		WHERE CD_COMPANY IN ('K100', 'K200')
																		AND NO_REV = '{0}'", 차수));

			if (dataTable != null && dataTable.Rows.Count != 0)
				num = D.GetDecimal(dataTable.Rows[0]["NO_IDX"]);

			num1 = D.GetDecimal(this._flex선적H.DataTable.Compute("MAX(NO_IDX)", string.Empty));

			if (num >= num1)
				return (num + 1);
			else
				return (num1 + 1);
		}

		private Decimal Get전달순번(string 차수)
		{
			Decimal num = 0, num1 = 0;
			DataTable dataTable = DBHelper.GetDataTable(string.Format(@"SELECT MAX(NO_IDX) AS NO_IDX 
																		FROM CZ_SA_LOG_PLAN_DELIVERY WITH(NOLOCK)  
																		WHERE CD_COMPANY IN ('K100', 'K200')
																		AND NO_REV = '{0}'", 차수));

			if (dataTable != null && dataTable.Rows.Count != 0)
				num = D.GetDecimal(dataTable.Rows[0]["NO_IDX"]);

			num1 = D.GetDecimal(this._flex전달.DataTable.Compute("MAX(NO_IDX)", string.Empty));

			if (num >= num1)
				return (num + 1);
			else
				return (num1 + 1);
		}

		private int Get대행실적순번()
		{
			int num = 0, num1 = 0;
			DataTable dataTable = DBHelper.GetDataTable(@"SELECT MAX(NO_IDX) AS NO_IDX 
														  FROM CZ_SA_LOG_PLAN_OLD WITH(NOLOCK)");

			if (dataTable != null && dataTable.Rows.Count != 0)
				num = D.GetInt(dataTable.Rows[0]["NO_IDX"]);

			num1 = D.GetInt(this._flex대행실적.DataTable.Compute("MAX(NO_IDX)", string.Empty));

			if (num >= num1)
				return (num + 1);
			else
				return (num1 + 1);
		}
	}
}

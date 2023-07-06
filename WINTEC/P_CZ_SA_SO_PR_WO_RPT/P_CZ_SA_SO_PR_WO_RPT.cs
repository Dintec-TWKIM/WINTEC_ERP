using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using System;
using System.Drawing;

namespace cz
{
	public partial class P_CZ_SA_SO_PR_WO_RPT : PageBase
	{
		P_CZ_SA_SO_PR_WO_RPT_BIZ _biz = new P_CZ_SA_SO_PR_WO_RPT_BIZ();

		public P_CZ_SA_SO_PR_WO_RPT()
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
			#region Header
			this._flexH.BeginSetting(2, 1, false);

			this._flexH.SetCol("NO_SO", "수주번호", 100);
			this._flexH.SetCol("NO_PO_PARTNER", "거래처번호", 100);
			this._flexH.SetCol("DT_SO", "수주일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexH.SetCol("LN_PARTNER", "매출처명", 100);
			this._flexH.SetCol("NO_HULL", "호선번호", 100);
			this._flexH.SetCol("NM_TP_ENGINE", "엔진타입", 100);
			this._flexH.SetCol("DT_DUEDATE", "최종납기일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexH.SetCol("DT_IO", "납품일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

			this._flexH.SetCol("CD_ITEM", "품목코드", 100);
			this._flexH.SetCol("NM_ITEM", "품목명", 100);
			this._flexH.SetCol("NO_DESIGN", "도면번호", 100);
			this._flexH.SetCol("DT_EXPECT", "작업완료예정일", 100, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexH.SetCol("QT_SO", "수주수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexH.SetCol("QT_GIR", "의뢰수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexH.SetCol("QT_GI", "출고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			
			this._flexH.SetCol("LEVEL", "단계", 40);
			this._flexH.SetCol("CD_MATL", "품목코드", 100);
			this._flexH.SetCol("NM_MATL", "품목명", 100);
			this._flexH.SetCol("NO_DESIGN1", "도면번호", 100);
			this._flexH.SetCol("QT_BOM", "필요수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexH.SetCol("NO_WO", "작업지시번호", 100);
			this._flexH.SetCol("QT_APPLY", "지시수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			
			this._flexH.SetCol("YN_FINAL", "최종공정여부", 45, false, CheckTypeEnum.Y_N);
			this._flexH.SetCol("NM_WC", "작업장명", 100);
			this._flexH.SetCol("NM_OP", "공정명", 100);
			this._flexH.SetCol("NM_EQUIP", "설비", 100);
			this._flexH.SetCol("QT_WO", "지시수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexH.SetCol("QT_START", "입고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexH.SetCol("QT_WORK", "작업수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexH.SetCol("QT_REMAIN", "작업잔량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexH.SetCol("RT_WORK", "진행율(%)", 100, false, typeof(decimal), FormatTpType.RATE);

			this._flexH[0, this._flexH.Cols["NO_SO"].Index] = "수주";
			this._flexH[0, this._flexH.Cols["NO_PO_PARTNER"].Index] = "수주";
			this._flexH[0, this._flexH.Cols["DT_SO"].Index] = "수주";
			this._flexH[0, this._flexH.Cols["LN_PARTNER"].Index] = "수주";
			this._flexH[0, this._flexH.Cols["NO_HULL"].Index] = "수주";
			this._flexH[0, this._flexH.Cols["NM_TP_ENGINE"].Index] = "수주";
			this._flexH[0, this._flexH.Cols["DT_DUEDATE"].Index] = "수주";
			this._flexH[0, this._flexH.Cols["DT_IO"].Index] = "수주";

			this._flexH[0, this._flexH.Cols["CD_ITEM"].Index] = "수주";
			this._flexH[0, this._flexH.Cols["NM_ITEM"].Index] = "수주";
			this._flexH[0, this._flexH.Cols["NO_DESIGN"].Index] = "수주";
			this._flexH[0, this._flexH.Cols["DT_EXPECT"].Index] = "수주";
			this._flexH[0, this._flexH.Cols["QT_SO"].Index] = "수주";
			this._flexH[0, this._flexH.Cols["QT_GIR"].Index] = "수주";
			this._flexH[0, this._flexH.Cols["QT_GI"].Index] = "수주";
			
			this._flexH[0, this._flexH.Cols["LEVEL"].Index] = "작업지시";
			this._flexH[0, this._flexH.Cols["CD_MATL"].Index] = "작업지시";
			this._flexH[0, this._flexH.Cols["NM_MATL"].Index] = "작업지시";
			this._flexH[0, this._flexH.Cols["NO_DESIGN1"].Index] = "작업지시";
			this._flexH[0, this._flexH.Cols["QT_BOM"].Index] = "작업지시";
			this._flexH[0, this._flexH.Cols["NO_WO"].Index] = "작업지시";
			this._flexH[0, this._flexH.Cols["QT_APPLY"].Index] = "작업지시";

			this._flexH[0, this._flexH.Cols["YN_FINAL"].Index] = "공정";
			this._flexH[0, this._flexH.Cols["NM_WC"].Index] = "공정";
			this._flexH[0, this._flexH.Cols["NM_OP"].Index] = "공정";
			this._flexH[0, this._flexH.Cols["NM_EQUIP"].Index] = "공정";
			this._flexH[0, this._flexH.Cols["QT_WO"].Index] = "공정";
			this._flexH[0, this._flexH.Cols["QT_START"].Index] = "공정";
			this._flexH[0, this._flexH.Cols["QT_WORK"].Index] = "공정";
			this._flexH[0, this._flexH.Cols["QT_REMAIN"].Index] = "공정";
			this._flexH[0, this._flexH.Cols["RT_WORK"].Index] = "공정";

			this._flexH.SettingVersion = "0.0.0.2";
			this._flexH.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

			this._flexH.SetExceptSumCol(new string[] { "RT_WORK" });
			this._flexH.Cols.Frozen = 8;

			this._flexH.Styles.Add("완료").BackColor = Color.Gray;
			this._flexH.Styles.Add("완료").ForeColor = Color.Black;
			this._flexH.Styles.Add("미완료").BackColor = Color.White;
			this._flexH.Styles.Add("미완료").ForeColor = Color.Black;
			#endregion

			#region Detail
			this._flexD.BeginSetting(1, 1, false);

			this._flexD.SetCol("CD_OP", "순번", 100);
			this._flexD.SetCol("NM_WC", "작업장명", 100); 
			this._flexD.SetCol("NM_OP", "공정명", 100);
			this._flexD.SetCol("NM_ST_OP", "상태", 100);
			this._flexD.SetCol("NM_EQUIP", "설비", 100);
			this._flexD.SetCol("YN_FINAL", "최종공정여부", 45, false, CheckTypeEnum.Y_N);
			this._flexD.SetCol("QT_WO", "지시수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexD.SetCol("QT_START", "입고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexD.SetCol("QT_WORK", "작업수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexD.SetCol("QT_REJECT", "불량수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexD.SetCol("QT_REWORK", "재작업수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexD.SetCol("QT_MOVE", "이동수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexD.SetCol("QT_WIP", "대기수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexD.SetCol("RT_WORK", "진행율(%)", 100, false, typeof(decimal), FormatTpType.RATE);

			this._flexD.SettingVersion = "0.0.0.1";
			this._flexD.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

			this._flexD.SetExceptSumCol(new string[] { "RT_WORK" });

			this._flexD.Styles.Add("진행중").BackColor = Color.White;
			this._flexD.Styles.Add("진행중").ForeColor = Color.Red;
			this._flexD.Styles.Add("일반").BackColor = Color.White;
			this._flexD.Styles.Add("일반").ForeColor = Color.Black;
			#endregion
		}

		private void InitEvent()
		{
			this._flexH.OwnerDrawCell += _flexH_OwnerDrawCell;
			this._flexD.OwnerDrawCell += _flexD_OwnerDrawCell;

			this._flexH.AfterRowChange += _flexH_AfterRowChange;
			this._flexH.AfterEdit += _flexH_AfterEdit;

			this.bpc작업장.QueryBefore += OnBpControl_QueryBefore;
			this.bpc공정.QueryBefore += OnBpControl_QueryBefore;
			this.bpc설비.QueryBefore += OnBpControl_QueryBefore;
			this.ctx품목코드.QueryBefore += Ctx품목코드_QueryBefore;
		}

		private void Ctx품목코드_QueryBefore(object sender, BpQueryArgs e)
		{
			try
			{
				e.HelpParam.P09_CD_PLANT = this.cbo공장.SelectedValue.ToString();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flexH_AfterEdit(object sender, RowColEventArgs e)
		{
			FlexGrid flexGrid;

			try
			{
				flexGrid = ((FlexGrid)sender);

				if (flexGrid.HasNormalRow == false) return;
				if (flexGrid.Cols[e.Col].Name != "DT_EXPECT") return;

				DBHelper.ExecuteNonQuery("SP_CZ_SA_SO_PR_WO_RPTH_U", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																					this._flexH["NO_SO"].ToString(),
																					this._flexH["SEQ_SO"].ToString(),
																					this._flexH["DT_EXPECT"].ToString(),
																					Global.MainFrame.LoginInfo.UserID });
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void OnBpControl_QueryBefore(object sender, BpQueryArgs e)
		{
			try
			{
				switch (e.HelpID)
				{
					case HelpID.P_MA_WC_SUB1:
						e.HelpParam.P09_CD_PLANT = this.cbo공장.SelectedValue.ToString();
						e.HelpParam.P07_NO_EMP = Global.MainFrame.LoginInfo.EmployeeNo;
						break;
					case HelpID.P_PR_WCOP_SUB1:
						if (D.GetString(this.bpc작업장.SelectedValue) == string.Empty)
						{
							Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl작업장.Text);
							e.QueryCancel = true;
							break;
						}
						e.HelpParam.P09_CD_PLANT = this.cbo공장.SelectedValue.ToString();
						e.HelpParam.P20_CD_WC = this.bpc작업장.QueryWhereIn_Pipe;
						break;
					case HelpID.P_MA_TABLE_SUB1:
						e.HelpParam.P00_CHILD_MODE = "설비 도움창";
						e.HelpParam.P61_CODE1 = "WE.CD_EQUIP AS CODE, EQ.NM_EQUIP AS NAME ";
						e.HelpParam.P62_CODE2 = @"PR_WCOP_EQUIP WE
												  LEFT JOIN PR_EQUIP EQ ON WE.CD_COMPANY = EQ.CD_COMPANY AND WE.CD_PLANT = EQ.CD_PLANT AND WE.CD_EQUIP = EQ.CD_EQUIP";
						e.HelpParam.P63_CODE3 = string.Format(@"WHERE WE.CD_COMPANY = '{0}'
															    AND WE.CD_PLANT = '{1}'
															    AND (ISNULL('{2}', '') = '' OR WE.CD_WC IN (SELECT CD_STR FROM GETTABLEFROMSPLIT('{2}')))
																AND (ISNULL('{3}', '') = '' OR WE.CD_WCOP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT('{3}')))", Global.MainFrame.LoginInfo.CompanyCode,
																																						      this.cbo공장.SelectedValue.ToString(),
																																						      this.bpc작업장.QueryWhereIn_Pipe,
																																						      this.bpc공정.QueryWhereIn_Pipe);
						e.HelpParam.P64_CODE4 = "GROUP BY WE.CD_EQUIP, EQ.NM_EQUIP";
						break;
				}
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
			this.cbo공장.DisplayMember = "NAME";
			this.cbo공장.ValueMember = "CODE";
			this.cbo공장.SelectedValue = this.LoginInfo.CdPlant;

			this.cbo공정경로.DataSource = DBHelper.GetDataTable("UP_PR_PATN_ROUT_ALL_S2", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																										 Global.SystemLanguage.MultiLanguageLpoint });
			this.cbo공정경로.DisplayMember = "NAME";
			this.cbo공정경로.ValueMember = "CODE";

			this.dtp납기일자.StartDateToString = this.MainFrameInterface.GetStringToday.ToString().Substring(0, 6) + "01";
			this.dtp납기일자.EndDateToString = this.MainFrameInterface.GetStringToday;
		}

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarSearchButtonClicked(sender, e);

				if (!this.BeforeSearch()) return;

				this._flexH.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																      this.cbo공장.SelectedValue.ToString(),
																	  this.cbo공정경로.SelectedValue.ToString(),
															          this.dtp납기일자.StartDateToString,
																	  this.dtp납기일자.EndDateToString,
																      this.txt수주번호.Text,
																	  this.txt작업지시번호.Text,
																	  this.bpc작업장.QueryWhereIn_Pipe,
																	  this.bpc공정.QueryWhereIn_Pipe,
																      this.bpc설비.QueryWhereIn_Pipe,
																      (this.chk납품완료제외.Checked == true ? "Y" : "N"),
																	  this.ctx매출처.CodeValue,
																	  this.ctx품목코드.CodeValue,
																	  this.txt도면번호.Text,
																      this.txt품목명.Text,
																	  (this.chk모품목포함.Checked == true ? "Y" : "N") });

				if (!this._flexH.HasNormalRow)
				{
					this.ShowMessage(PageResultMode.SearchNoData);
				}
				else
				{
					if (this.chk셀병합.Checked == true)
						this.셀병합(this._flexH);
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
		{
			try
			{
				this._flexD.Binding = this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																			this._flexH["NO_WO"].ToString() });
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void _flexD_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
		{
			try
			{
				if (!this._flexD.HasNormalRow) return;
				if (e.Row < this._flexD.Rows.Fixed || e.Col < this._flexD.Cols.Fixed)
					return;

				CellStyle cellStyle = this._flexD.Rows[e.Row].Style;

				if (D.GetDecimal(this._flexD[e.Row, "QT_WIP"]) > 0)
				{
					if (cellStyle == null || cellStyle.Name != "진행중")
						this._flexD.Rows[e.Row].Style = this._flexD.Styles["진행중"];
				}
				else
				{
					if (cellStyle == null || cellStyle.Name != "일반")
						this._flexD.Rows[e.Row].Style = this._flexD.Styles["일반"];
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flexH_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
		{
			try
			{
				if (!this._flexH.HasNormalRow) return;
				if (e.Row < this._flexH.Rows.Fixed || e.Col < this._flexH.Cols.Fixed)
					return;

				CellStyle cellStyle = this._flexH.Rows[e.Row].Style;

				if (this._flexH[e.Row, "YN_FINAL"].ToString() == "Y" &&
					this._flexH[e.Row, "QT_START"].ToString() == this._flexH[e.Row, "QT_WORK"].ToString())
				{
					if (cellStyle == null || cellStyle.Name != "완료")
						this._flexH.Rows[e.Row].Style = this._flexH.Styles["완료"];
				}
				else
				{
					if (cellStyle == null || cellStyle.Name != "미완료")
						this._flexH.Rows[e.Row].Style = this._flexH.Styles["미완료"];
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void 셀병합(FlexGrid grid)
		{
			CellRange cellRange;
			try
			{
				if (grid.HasNormalRow == false) return;

				grid.Redraw = false;

				for (int row = grid.Rows.Fixed; row < grid.Rows.Count; row++)
				{
					foreach (Column column in grid.Cols)
					{
						switch (column.Name)
						{
							case "NO_SO":
							case "NO_PO_PARTNER":
							case "DT_SO":
							case "LN_PARTNER":
							case "NO_HULL":
							case "NM_TP_ENGINE":
							case "DT_DUEDATE":
							case "DT_IO":
							case "CD_ITEM":
							case "NM_ITEM":
							case "NO_DESIGN":
							case "DT_EXPECT":
								cellRange = grid.GetCellRange(row, column.Name, row, column.Name);
								cellRange.UserData = D.GetString(grid.GetData(row, "NO_SO")) + "_" + column.Name;
								break;
							case "LEVEL":
							case "CD_MATL":
							case "NM_MATL":
							case "NO_DESIGN1":
							case "NO_WO":
								cellRange = grid.GetCellRange(row, column.Name, row, column.Name);
								cellRange.UserData = D.GetString(grid.GetData(row, "NO_SO")) + "_" + D.GetString(grid.GetData(row, "NO_WO")) + "_" + column.Name;
								break;
							default:
								continue;
						}
					}
				}

				grid.DoMerge();
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
			finally
			{
				grid.Redraw = true;
			}
		}
	}
}

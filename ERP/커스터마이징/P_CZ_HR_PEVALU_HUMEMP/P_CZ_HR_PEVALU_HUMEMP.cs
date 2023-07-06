using System;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.OLD;
using System.Data.OleDb;
using System.Text;
using System.Net;

namespace cz
{
	public partial class P_CZ_HR_PEVALU_HUMEMP : PageBase
	{
		#region 생성자 & 초기화
		private P_CZ_HR_PEVALU_HUMEMP_BIZ _biz = new P_CZ_HR_PEVALU_HUMEMP_BIZ();
		private string 마감여부 = null;

		public P_CZ_HR_PEVALU_HUMEMP()
		{
			StartUp.Certify(this);
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
			this.MainGrids = new FlexGrid[] { this._flex평가자, this._flex피평가자 };
			this._flex평가자.DetailGrids = new FlexGrid[] { this._flex피평가자 };

			#region 평가자
			this._flex평가자.BeginSetting(1, 1, false);

			this._flex평가자.SetCol("S", "S", 30, true, CheckTypeEnum.Y_N);
			this._flex평가자.SetCol("NM_DEPT", "부서명", 110, false);
			this._flex평가자.SetCol("NO_EMPM", "사번", 110, false);
			this._flex평가자.SetCol("NM_KOR", "성명", 110, false);
			this._flex평가자.SetCol("NM_DUTY_RANK", "직위", 110, false);

			this._flex평가자.SetCodeHelpCol("NO_EMPM", HelpID.P_MA_TABLE_SUB, ShowHelpEnum.Always, new string[] { "NO_EMPM", "NM_KOR", "CD_COMPANY", "CD_DUTY_RANK", "NM_DUTY_RANK", "CD_BIZAREA", "CD_DEPT", "NM_DEPT", "CD_EMP", "TP_EMP", "CD_DUTY_STEP", "CD_DUTY_RESP", "CD_DUTY_TYPE", "CD_DUTY_WORK", "CD_JOB_SERIES", "CD_CC", "CD_PJT" },
																								   new string[] { "CODE", "NAME", "CD_COMPANY", "CD_DUTY_RANK", "NM_DUTY_RANK", "CD_BIZAREA", "CD_DEPT", "NM_DEPT", "CD_EMP", "TP_EMP", "CD_DUTY_STEP", "CD_DUTY_RESP", "CD_DUTY_TYPE", "CD_DUTY_WORK", "CD_JOB_SERIES", "CD_CC", "CD_PJT" });

			this._flex평가자.SettingVersion = "0.0.0.1";
			this._flex평가자.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

			this._flex평가자.SetDummyColumn("S");
			#endregion

			#region 피평가자
			this._flex피평가자.BeginSetting(1, 1, false);

			this._flex피평가자.SetCol("S", "S", 30, true, CheckTypeEnum.Y_N);
			this._flex피평가자.SetCol("NM_DEPT", "부서명", 110, false);
			this._flex피평가자.SetCol("NO_EMPAN", "사번", 120, false);
			this._flex피평가자.SetCol("NM_KOR", "성명", 120, false);
			this._flex피평가자.SetCol("NM_DUTY_RANK", "직위", 90, false);

			this._flex피평가자.SetCodeHelpCol("NO_EMPAN", HelpID.P_MA_TABLE_SUB, ShowHelpEnum.Always, new string[] { "NO_EMPAN", "NM_KOR", "CD_COMPANY", "CD_DUTY_RANK", "NM_DUTY_RANK", "CD_BIZAREA", "CD_DEPT", "NM_DEPT", "CD_EMP", "TP_EMP", "CD_DUTY_STEP", "CD_DUTY_RESP", "CD_DUTY_TYPE", "CD_DUTY_WORK", "CD_JOB_SERIES", "CD_CC", "CD_PJT" },
																									  new string[] { "CODE", "NAME", "CD_COMPANY", "CD_DUTY_RANK", "NM_DUTY_RANK", "CD_BIZAREA", "CD_DEPT", "NM_DEPT", "CD_EMP", "TP_EMP", "CD_DUTY_STEP", "CD_DUTY_RESP", "CD_DUTY_TYPE", "CD_DUTY_WORK", "CD_JOB_SERIES", "CD_CC", "CD_PJT" });

			this._flex피평가자.SettingVersion = "0.0.0.1";
			this._flex피평가자.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

			this._flex피평가자.SetDummyColumn("S");
			#endregion
		}

		private void InitEvent()
		{
			this.btn대상자선정평가자.Click += new EventHandler(this.btn대상자선정평가자_Click);
			this.btn대상자선정피평가자.Click += new EventHandler(this.btn대상자선정피평가자_Click);
			this.btn추가평가자.Click += new EventHandler(this.btn추가평가자_Click);
			this.btn추가피평가자.Click += new EventHandler(this.btn추가피평가자_Click);
			this.btn삭제평가자.Click += new EventHandler(this.btn삭제평가자_Click);
			this.btn삭제피평가자.Click += new EventHandler(this.btn삭제피평가자_Click);
			this.ctx평가코드.QueryBefore += new BpQueryHandler(this.bpc평가코드_QueryBefore);
			this.ctx평가코드.QueryAfter += new BpQueryHandler(this.bpc평가코드_QueryAfter);
			this.btn엑셀양식다운로드.Click += new EventHandler(this.btn엑셀양식다운로드_Click);
			this.btn엑셀업로드.Click += new EventHandler(this.btn엑셀업로드_Click);
			this.btn복사.Click += new EventHandler(this.btn복사_Click);

			this._flex평가자.AfterRowChange += new RangeEventHandler(this._flex평가자_AfterRowChange);
			this._flex평가자.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex_BeforeCodeHelp);
			this._flex피평가자.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex_BeforeCodeHelp);
			this._flex평가자.AfterCodeHelp += new AfterCodeHelpEventHandler(this._flex_AfterCodeHelp);
		}

		protected override void InitPaint()
		{
			base.InitPaint();

			this.oneGrid1.UseCustomLayout = false;
			this.oneGrid1.IsSearchControl = false;
			this.oneGrid1.InitCustomLayout();
			this.bppnl평가코드.IsNecessaryCondition = true;
		}
		#endregion

		#region 메인버튼 이벤트
		protected override bool BeforeSearch()
		{
			if (!this.ctx평가코드.IsEmpty())
				return base.BeforeSearch();

			this.ShowMessage(공통메세지._은는필수입력항목입니다, this.DD("평가코드"));
			this.ctx평가코드.Focus();
			
			return false;
		}

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarSearchButtonClicked(sender, e);

				if (!this.BeforeSearch()) return;

				this._flex평가자.Binding = this._biz.Search평가자(new object[] { D.GetString(this.ctx평가코드.CodeValue),
																				 D.GetString(this.cbo평가유형.SelectedValue),
																				 D.GetString(this.cbo평가차수.SelectedValue),
																				 D.GetString(this.cbo평가그룹.SelectedValue) });
				this.btn대상자선정평가자.Enabled = true;
				this.btn추가평가자.Enabled = true;
				this.btn삭제평가자.Enabled = true;
				this.btn엑셀업로드.Enabled = true;
				this.btn복사.Enabled = true;

				if (!this._flex평가자.HasNormalRow)
				{
					if (this._flex피평가자.DataTable != null)
						this._flex피평가자.DataTable.Clear();

					this.btn대상자선정피평가자.Enabled = false;
					this.btn추가피평가자.Enabled = false;
					this.btn삭제피평가자.Enabled = false;

					this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
				}
				else
				{
					this.btn대상자선정피평가자.Enabled = true;
					this.btn추가피평가자.Enabled = true;
					this.btn삭제피평가자.Enabled = true;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		protected override bool BeforeSave()
		{
			if (this._flex평가자.DataTable.Select("ISNULL(NO_EMPM, '') = ''").Length > 0)
			{
				this.ShowMessage(공통메세지._은는필수입력항목입니다, this.DD("사원번호"));
				return false;
			}

			if (this._flex피평가자.HasNormalRow == true && this._flex피평가자.DataTable.Select("ISNULL(NO_EMPAN, '') = ''").Length > 0)
			{
				this.ShowMessage(공통메세지._은는필수입력항목입니다, this.DD("사원번호"));
				return false;
			}

			return base.BeforeSave();
		}

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarSaveButtonClicked(sender, e);

				if (MsgAndSave(PageActionMode.Save))
					ShowMessage(PageResultMode.SaveGood);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		protected override bool SaveData()
		{
			if (!base.SaveData() || !this.Verify() || !this.BeforeSave()) return false;
			if (!this._flex평가자.IsDataChanged && !this._flex피평가자.IsDataChanged) return false;

			if (!this._biz.Save(this._flex평가자.GetChanges(), this._flex피평가자.GetChanges()))
				return false;

			this._flex평가자.AcceptChanges();
			this._flex피평가자.AcceptChanges();

			return true;
		}
		#endregion

		#region 그리드 이벤트
		private void _flex평가자_AfterRowChange(object sender, RangeEventArgs e)
		{
			DataTable dt;
			string filter;

			try
			{
				if (string.IsNullOrEmpty(D.GetString(this._flex평가자["NO_EMPM"])))
					return;

				dt = null;
				filter = "NO_EMPM = '" + D.GetString(this._flex평가자["NO_EMPM"]) + "'";

				if (this._flex평가자.DetailQueryNeed == true)
				{
					dt = this._biz.Search피평가자(new object[] { D.GetString(this.ctx평가코드.CodeValue),
																 D.GetString(this.cbo평가유형.SelectedValue),
																 D.GetString(this.cbo평가차수.SelectedValue),
																 D.GetString(this.cbo평가그룹.SelectedValue),
																 D.GetString(this._flex평가자["NO_EMPM"]) });
				}

				this._flex피평가자.BindingAdd(dt, filter);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
		{
			try
			{
				e.Parameter.P00_CHILD_MODE = "대상자선정 도움창";
				e.Parameter.P61_CODE1 = @"ME.CD_COMPANY,
										  ME.NO_EMP AS CODE,
										  ME.NM_KOR AS NAME,
										  ME.CD_DUTY_RANK,
									      (SELECT (CASE '" + Global.MainFrame.LoginInfo.Language + @"' WHEN 'KR' THEN NM_SYSDEF
		                      																		   WHEN 'US' THEN NM_SYSDEF_E
		                      																		   WHEN 'JP' THEN NM_SYSDEF_JP
		                      																		   WHEN 'CH' THEN NM_SYSDEF_CH END)
										   FROM MA_CODEDTL
										   WHERE CD_COMPANY = ME.CD_COMPANY
										   AND CD_FIELD = 'HR_H000002'
										   AND CD_SYSDEF = ME.CD_DUTY_RANK) AS NM_DUTY_RANK,
										  ME.CD_BIZAREA,
										  ME.CD_DEPT,
										  MD.NM_DEPT,
										  ME.CD_EMP,
										  ME.TP_EMP,
										  ME.CD_DUTY_STEP,
										  ME.CD_DUTY_RESP,
										  ME.CD_DUTY_TYPE,
										  ME.CD_DUTY_WORK,
										  ME.CD_JOB_SERIES,
										  ME.CD_CC,
										  ME.CD_PJT";
				e.Parameter.P62_CODE2 = "MA_EMP ME WITH(NOLOCK)" + Environment.NewLine +
										"JOIN MA_DEPT MD WITH(NOLOCK) ON ME.CD_COMPANY = MD.CD_COMPANY AND ME.CD_DEPT = MD.CD_DEPT";
				e.Parameter.P63_CODE3 = "WHERE ISNULL(ME.NM_KOR, '') <> ''" + Environment.NewLine +
										"AND ISNULL(ME.CD_INCOM, '001') <> '099'" + Environment.NewLine +
										"AND ME.CD_COMPANY <> 'TEST'";
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flex_AfterCodeHelp(object sender, AfterCodeHelpEventArgs e)
		{
			DataTable dt;
			string filter;

			try
			{
				dt = null;
				filter = "NO_EMPM = '" + e.Result.CodeValue + "'";

				dt = this._biz.Search피평가자(new object[] { D.GetString(this.ctx평가코드.CodeValue),
															 D.GetString(this.cbo평가유형.SelectedValue),
															 D.GetString(this.cbo평가차수.SelectedValue),
															 D.GetString(this.cbo평가그룹.SelectedValue),
															 e.Result.CodeValue });

				this._flex피평가자.BindingAdd(dt, filter);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}
		#endregion

		#region 컨트롤 이벤트
		private void bpc평가코드_QueryBefore(object sender, BpQueryArgs e)
		{
			try
			{
				e.HelpParam.P00_CHILD_MODE = "인사평가 코드도움창";
				e.HelpParam.P61_CODE1 = "CD_EVALU AS CODE, NM_EVALU AS NAME, YM_EVALU AS YM, YN_CLOSE AS YNCLOSE ";
				e.HelpParam.P62_CODE2 = "HR_PEVALU_SCHE WITH(NOLOCK) WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'";
				e.HelpParam.P63_CODE3 = "";
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void bpc평가코드_QueryAfter(object sender, BpQueryArgs e)
		{
			try
			{
				this.ctx평가코드.CodeValue = e.HelpReturn.Rows[0]["CODE"].ToString();
				this.ctx평가코드.CodeName = e.HelpReturn.Rows[0]["NAME"].ToString();

				this.마감여부 = e.HelpReturn.Rows[0]["YNCLOSE"].ToString();
				
				this.cbo평가유형.DataSource = DBHelper.GetDataTable(@"SELECT CD_HCODE AS CODE, 
																			 NM_HCODE AS NAME 
																	  FROM HR_PEVALU_CODE WITH(NOLOCK) 
																	  WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
																	@"AND CD_FIELD = '100' 
																	  AND CD_EVALU = '" + e.HelpReturn.Rows[0]["CODE"].ToString() + "'" + Environment.NewLine +
																	 "AND YN_USE = 'Y'");
				this.cbo평가유형.DisplayMember = "NAME";
				this.cbo평가유형.ValueMember = "CODE";
				
				this.cbo평가차수.DataSource = DBHelper.GetDataTable(@"SELECT CD_HCODE AS CODE, 
																			 NM_HCODE AS NAME 
																	  FROM HR_PEVALU_CODE WITH(NOLOCK)  
																	  WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine + 
																	@"AND CD_FIELD = '300' 
																	  AND CD_EVALU = '" + e.HelpReturn.Rows[0]["CODE"].ToString() + "'" + Environment.NewLine +
																	 "AND YN_USE = 'Y'");
				this.cbo평가차수.DisplayMember = "NAME";
				this.cbo평가차수.ValueMember = "CODE";
				
				this.cbo평가그룹.DataSource = DBHelper.GetDataTable(@"SELECT CD_HCODE AS CODE,
																			 NM_HCODE AS NAME 
																	  FROM HR_PEVALU_CODE WITH(NOLOCK)  
																	  WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
																	@"AND CD_FIELD = '200' 
																	  AND CD_EVALU = '" + e.HelpReturn.Rows[0]["CODE"].ToString() + "'" + Environment.NewLine +
																	 "AND YN_USE = 'Y'");
				this.cbo평가그룹.DisplayMember = "NAME";
				this.cbo평가그룹.ValueMember = "CODE";
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn추가평가자_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.마감여부 == "Y")
				{
					this.ShowMessage("CZ_마감된 @ 입니다.", this.DD("평가코드"));
				}
				else
				{
					DataRow row = this._flex평가자.DataTable.NewRow();

					row["S"] = "N";
					row["CD_EVALU"] = this.ctx평가코드.CodeValue;
					row["CD_EVTYPE"] = this.cbo평가유형.SelectedValue.ToString();
					row["CD_EVNUMBER"] = this.cbo평가차수.SelectedValue.ToString();
					row["CD_GROUP"] = this.cbo평가그룹.SelectedValue.ToString();
					row["NUM_EWEIGHT"] = 0;

					this._flex평가자.DataTable.Rows.Add(row);
					this.btn대상자선정피평가자.Enabled = true;
					this.btn추가피평가자.Enabled = true;
					this.ToolBarSaveButtonEnabled = true;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn추가피평가자_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.마감여부 == "Y")
				{
					this.ShowMessage("CZ_마감된 @ 입니다.", this.DD("평가코드"));
				}
				else
				{
					if (string.IsNullOrEmpty(D.GetString(this._flex평가자["NO_EMPM"])))
						return;

					DataRow row = this._flex피평가자.DataTable.NewRow();

					row["S"] = "N";
					row["CD_EVALU"] = this.ctx평가코드.CodeValue;
					row["CD_EVTYPE"] = this.cbo평가유형.SelectedValue.ToString();
					row["CD_EVNUMBER"] = this.cbo평가차수.SelectedValue.ToString();
					row["CD_GROUP"] = this.cbo평가그룹.SelectedValue.ToString();
					row["NO_EMPM"] = D.GetString(this._flex평가자["NO_EMPM"]);

					this._flex피평가자.DataTable.Rows.Add(row);
					this.ToolBarSaveButtonEnabled = true;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn삭제평가자_Click(object sender, EventArgs e)
		{
			int index;

			try
			{
				if (!this._flex평가자.HasNormalRow)
					return;
				if (this.마감여부 == "Y")
				{
					this.ShowMessage("CZ_마감된 @ 입니다.", this.DD("평가코드"));
				}
				else
				{
					DataRow[] dataRowArray = this._flex평가자.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);
					if (dataRowArray == null || dataRowArray.Length == 0)
					{
						this.ShowMessage("IK1_007");
					}
					else
					{
						this._flex평가자.Redraw = false;
						index = 0;

						foreach (DataRow dataRow in dataRowArray)
						{
							MsgControl.ShowMsg("CZ_처리중입니다. 잠시만 기다려주세요. (@/@)", new string[] { D.GetString(++index), D.GetString(dataRowArray.Length) });
							dataRow.Delete();
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
				MsgControl.CloseMsg();
				this._flex평가자.Redraw = true;
			}
		}

		private void btn삭제피평가자_Click(object sender, EventArgs e)
		{
			int index;

			try
			{
				if (!this._flex피평가자.HasNormalRow)
					return;
				if (this.마감여부 == "Y")
				{
					this.ShowMessage("CZ_마감된 @ 입니다.", this.DD("평가코드"));
				}
				else
				{
					DataRow[] dataRowArray = this._flex피평가자.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);
					if (dataRowArray == null || dataRowArray.Length == 0)
					{
						this.ShowMessage("IK1_007");
					}
					else
					{
						this._flex피평가자.Redraw = false;
						index = 0;

						foreach (DataRow dataRow in dataRowArray)
						{
							MsgControl.ShowMsg("CZ_처리중입니다. 잠시만 기다려주세요. (@/@)", new string[] { D.GetString(++index), D.GetString(dataRowArray.Length) });
							dataRow.Delete();
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
				MsgControl.CloseMsg();
				this._flex피평가자.Redraw = true;
			}
		}

		private void btn대상자선정평가자_Click(object sender, EventArgs e)
		{
			int index;

			try
			{
				if (this.마감여부 == "Y")
				{
					this.ShowMessage("CZ_마감된 @ 입니다.", this.DD("평가코드"));
				}
				else
				{
					string codeValue = this.ctx평가코드.CodeValue;
					string str1 = this.cbo평가유형.SelectedValue.ToString();
					string str2 = this.cbo평가차수.SelectedValue.ToString();
					string str3 = this.cbo평가그룹.SelectedValue.ToString();
					P_CZ_HR_PEVALU_HUMEMP_TAB1 pevaluHumempTaB1 = new P_CZ_HR_PEVALU_HUMEMP_TAB1();
					switch (pevaluHumempTaB1.ShowDialog())
					{
						case DialogResult.OK:
							DataRow[] dataRowArray = pevaluHumempTaB1.ReturnValues;
							if (dataRowArray == null || dataRowArray.Length == 0)
							{
								this.ShowMessage("추가할 사원이 존재하지 않습니다");
								return;
							}

							this._flex평가자.Redraw = false;
							index = 0;

							foreach (DataRow dataRow in dataRowArray)
							{
								MsgControl.ShowMsg("CZ_처리중입니다. 잠시만 기다려주세요. (@/@)", new string[] { D.GetString(++index), D.GetString(dataRowArray.Length) });

								if (this._flex평가자.DataTable.Select("NO_EMPM = '" + D.GetString(dataRow["NO_EMP"]) + "'").Length > 0)
									continue;

								DataRow row = this._flex평가자.DataTable.NewRow();
								row["S"] = "N";
								row["CD_COMPANY"] = dataRow["CD_COMPANY"];
								row["CD_EVALU"] = codeValue;
								row["CD_EVTYPE"] = str1;
								row["CD_EVNUMBER"] = str2;
								row["CD_GROUP"] = str3;
								row["NO_EMPM"] = dataRow["NO_EMP"];
								row["NM_KOR"] = dataRow["NM_KOR"];
								row["CD_DUTY_RANK"] = dataRow["CD_DUTY_RANK"];
								row["NM_DUTY_RANK"] = dataRow["NM_DUTY_RANK"];
								row["CD_DEPT"] = dataRow["CD_DEPT"];
								row["NM_DEPT"] = dataRow["NM_DEPT"];
								row["CD_BIZAREA"] = dataRow["CD_BIZAREA"];
								row["CD_EMP"] = dataRow["CD_EMP"];
								row["TP_EMP"] = dataRow["TP_EMP"];
								row["CD_DUTY_STEP"] = dataRow["CD_DUTY_STEP"];
								row["CD_DUTY_RESP"] = dataRow["CD_DUTY_RESP"];
								row["CD_DUTY_TYPE"] = dataRow["CD_DUTY_TYPE"];
								row["CD_PAY_STEP"] = dataRow["CD_PAY_STEP"];
								row["CD_DUTY_WORK"] = dataRow["CD_DUTY_WORK"];
								row["CD_JOB_SERIES"] = dataRow["CD_JOB_SERIES"];
								row["CD_CC"] = dataRow["CD_CC"];
								row["CD_PJT"] = dataRow["CD_PJT"];
								row["NUM_EWEIGHT"] = 0;
								this._flex평가자.DataTable.Rows.Add(row);
							}
							this.btn대상자선정피평가자.Enabled = true;
							this.btn추가피평가자.Enabled = true;
							this.OnToolBarSaveButtonClicked(null, null);
							break;
						case DialogResult.Cancel:
							this.ShowMessage("CZ_선정된 사원이 없습니다.");
							break;
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
			finally
			{
				MsgControl.CloseMsg();
				this._flex평가자.Redraw = true;
			}
		}

		private void btn대상자선정피평가자_Click(object sender, EventArgs e)
		{
			int index;

			try
			{
				if (this.마감여부 == "Y")
				{
					this.ShowMessage("CZ_마감된 @ 입니다.", this.DD("평가코드"));
				}
				else
				{
					string codeValue = this.ctx평가코드.CodeValue;
					string str1 = this.cbo평가유형.SelectedValue.ToString();
					string str2 = this.cbo평가차수.SelectedValue.ToString();
					string str3 = this.cbo평가그룹.SelectedValue.ToString();
					string @string = D.GetString(this._flex평가자[this._flex평가자.Row, "NO_EMPM"]);
					P_CZ_HR_PEVALU_HUMEMP_TAB1 pevaluHumempTaB1 = new P_CZ_HR_PEVALU_HUMEMP_TAB1();
					switch (pevaluHumempTaB1.ShowDialog())
					{
						case DialogResult.OK:
							DataRow[] dataRowArray = pevaluHumempTaB1.ReturnValues;
							if (dataRowArray == null || dataRowArray.Length == 0)
							{
								this.ShowMessage("추가할 사원이 존재하지 않습니다");
								break;
							}

							this._flex피평가자.Redraw = false;
							index = 0;

							foreach (DataRow dataRow in dataRowArray)
							{
								MsgControl.ShowMsg("CZ_처리중입니다. 잠시만 기다려주세요. (@/@)", new string[] { D.GetString(++index), D.GetString(dataRowArray.Length) });

								if (this._flex피평가자.DataTable.Select("NO_EMPAN = '" + D.GetString(dataRow["NO_EMP"]) + "'").Length > 0)
									continue;

								DataRow row = this._flex피평가자.DataTable.NewRow();
								row["S"] = "N";
								row["CD_COMPANY"] = dataRow["CD_COMPANY"];
								row["CD_EVALU"] = codeValue;
								row["CD_EVTYPE"] = str1;
								row["CD_EVNUMBER"] = str2;
								row["CD_GROUP"] = str3;
								row["NO_EMPM"] = @string;
								row["NO_EMPAN"] = dataRow["NO_EMP"];
								row["NM_KOR"] = dataRow["NM_KOR"];
								row["CD_DUTY_RANK"] = dataRow["CD_DUTY_RANK"];
								row["NM_DUTY_RANK"] = dataRow["NM_DUTY_RANK"];
								row["CD_DEPT"] = dataRow["CD_DEPT"];
								row["NM_DEPT"] = dataRow["NM_DEPT"];
								row["CD_BIZAREA"] = dataRow["CD_BIZAREA"];
								row["CD_EMP"] = dataRow["CD_EMP"];
								row["TP_EMP"] = dataRow["TP_EMP"];
								row["CD_DUTY_STEP"] = dataRow["CD_DUTY_STEP"];
								row["CD_DUTY_RESP"] = dataRow["CD_DUTY_RESP"];
								row["CD_DUTY_TYPE"] = dataRow["CD_DUTY_TYPE"];
								row["CD_PAY_STEP"] = dataRow["CD_PAY_STEP"];
								row["CD_DUTY_WORK"] = dataRow["CD_DUTY_WORK"];
								row["CD_JOB_SERIES"] = dataRow["CD_JOB_SERIES"];
								row["CD_CC"] = dataRow["CD_CC"];
								row["CD_PJT"] = dataRow["CD_PJT"];
								this._flex피평가자.DataTable.Rows.Add(row);
								this.ToolBarSaveButtonEnabled = true;
							}
							break;
						case DialogResult.Cancel:
							this.ShowMessage("CZ_선택된 @ 이(가) 없습니다.", this.DD("사원"));
							break;
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
			finally
			{
				MsgControl.CloseMsg();
				this._flex피평가자.Redraw = true;
			}
		}

		private void btn엑셀양식다운로드_Click(object sender, EventArgs e)
		{
			try
			{
				FolderBrowserDialog dlg = new FolderBrowserDialog();
				if (dlg.ShowDialog() != DialogResult.OK) return;

				string localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_평가자피평가자등록_" + Global.MainFrame.GetStringToday + ".xls";
				string serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_HR_PEVALU_HUMEMP.xls";

				WebClient client = new WebClient();
				client.DownloadFile(serverPath, localPath);
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void btn엑셀업로드_Click(object sender, EventArgs e)
		{
			OpenFileDialog fileDlg;
			DataTable dt;
			DataRow dr평가자, dr피평가자, drTemp;
			string query, query1;
			int index;

			try
			{
				#region btn엑셀업로드
				fileDlg = new OpenFileDialog();
				fileDlg.Filter = "엑셀 파일 (*.xls)|*.xls";

				if (fileDlg.ShowDialog() != DialogResult.OK) return;

				this._flex평가자.Redraw = false;
				this._flex피평가자.Redraw = false;

				string FileName = fileDlg.FileName;

				Excel excel = new Excel();
				DataTable dtExcel = null;
				dtExcel = excel.StartLoadExcel(FileName, 0, 3); // 3번째 라인부터 저장

				// 필요한 컬럼 존재 유무 파악
				string[] argsPk = new string[] { "CD_COMPANY", "NO_EMPM", "CD_BIZAREA", "NO_EMPAN" };
				string[] argsPkNm = new string[] { "평가자회사코드", "평가자사번", "피평가자회사코드", "피평가자사번" };

				for (int i = 0; i < argsPk.Length; i++)
				{
					if (!dtExcel.Columns.Contains(argsPk[i]))
					{
						this.ShowMessage("필수항목이 존재하지 않습니다. -> @ : @", new object[] { argsPk[i], argsPkNm[i] });
						return;
					}
				}

				query = @"SELECT 'N' AS S,
								 ME.CD_COMPANY,
								 MC.NM_COMPANY,
								 ME.CD_BIZAREA,
								 MB.NM_BIZAREA,
								 ME.CD_DEPT,
								 MD.NM_DEPT,
								 ME.NO_EMP,
								 ME.NM_KOR,
								 ME.CD_DUTY_RANK,
								 CD.NM_SYSDEF AS NM_DUTY_RANK,
								 ME.DT_ENTER,
								 ME.DT_RETIRE,
								 ME.DT_BAN,
								 ME.DT_LASTBAN,
								 ME.CD_PAY_STEP,
								 ME.DT_GENTER,
								 ME.CD_EMP,
								 ME.TP_EMP,
								 ME.CD_DUTY_STEP,
								 ME.CD_DUTY_RESP,
								 ME.CD_DUTY_TYPE,
								 ME.CD_DUTY_WORK,
								 ME.CD_JOB_SERIES,
								 ME.CD_CC,
								 ME.CD_PJT
						  FROM MA_EMP ME WITH(NOLOCK) 	
						  JOIN MA_COMPANY MC WITH(NOLOCK) ON MC.CD_COMPANY = ME.CD_COMPANY
						  JOIN MA_BIZAREA MB WITH(NOLOCK) ON ME.CD_COMPANY = MB.CD_COMPANY AND ME.CD_BIZAREA = MB.CD_BIZAREA
						  JOIN MA_DEPT MD WITH(NOLOCK) ON ME.CD_COMPANY = MD.CD_COMPANY AND ME.CD_DEPT = MD.CD_DEPT
						  LEFT JOIN MA_CODEDTL CD WITH(NOLOCK) ON CD.CD_COMPANY = ME.CD_COMPANY AND CD.CD_FIELD = 'HR_H000002' AND CD.CD_SYSDEF = ME.CD_DUTY_RANK";

				// 데이터 읽으면서 해당하는 값 셋팅
				index = 0;
				foreach (DataRow dr in dtExcel.Rows)
				{
					MsgControl.ShowMsg("[자료저장중] 잠시만 기다려 주세요 (@/@)", new string[] { D.GetString(++index), D.GetString(dtExcel.Rows.Count) });

					if (!string.IsNullOrEmpty(D.GetString(dr["NO_EMPM"])) && 
						this._flex평가자.DataTable.Select("NO_EMPM = '" + D.GetString(dr["NO_EMPM"]) + "'").Length == 0)
					{
						#region 평가자
						query1 = query + Environment.NewLine +
								"WHERE ME.CD_COMPANY = '" + D.GetString(dr["CD_COMPANY"]) + "'" + Environment.NewLine +
								"AND ME.NO_EMP = '" + D.GetString(dr["NO_EMPM"]) + "'";

						dt = DBHelper.GetDataTable(query1);
						if (dt == null || dt.Rows.Count == 0)
						{
							this.ShowMessage("사원이 존재하지 않습니다. (회사코드 : " + D.GetString(dr["CD_COMPANY"]) + ", 사원번호 : " + D.GetString(dr["NO_EMPM"]) + ")");
							continue;
						}
						else
							drTemp = dt.Rows[0];

						dr평가자 = this._flex평가자.DataTable.NewRow();

						dr평가자["S"] = "N";
						dr평가자["CD_COMPANY"] = D.GetString(drTemp["CD_COMPANY"]);
						dr평가자["CD_EVALU"] = this.ctx평가코드.CodeValue;
						dr평가자["CD_EVTYPE"] = D.GetString(this.cbo평가유형.SelectedValue);
						dr평가자["CD_EVNUMBER"] = D.GetString(this.cbo평가차수.SelectedValue);
						dr평가자["CD_GROUP"] = D.GetString(this.cbo평가그룹.SelectedValue);
						dr평가자["NO_EMPM"] = D.GetString(dr["NO_EMPM"]);
						dr평가자["NM_KOR"] = D.GetString(drTemp["NM_KOR"]);
						dr평가자["CD_DUTY_RANK"] = D.GetString(drTemp["CD_DUTY_RANK"]);
						dr평가자["NM_DUTY_RANK"] = D.GetString(drTemp["NM_DUTY_RANK"]);
						dr평가자["CD_DEPT"] = D.GetString(drTemp["CD_DEPT"]);
						dr평가자["NM_DEPT"] = D.GetString(drTemp["NM_DEPT"]);
						dr평가자["CD_BIZAREA"] = D.GetString(drTemp["CD_BIZAREA"]);
						dr평가자["CD_EMP"] = D.GetString(drTemp["CD_EMP"]);
						dr평가자["TP_EMP"] = D.GetString(drTemp["TP_EMP"]);
						dr평가자["CD_DUTY_STEP"] = D.GetString(drTemp["CD_DUTY_STEP"]);
						dr평가자["CD_DUTY_RESP"] = D.GetString(drTemp["CD_DUTY_RESP"]);
						dr평가자["CD_DUTY_TYPE"] = D.GetString(drTemp["CD_DUTY_TYPE"]);
						dr평가자["CD_PAY_STEP"] = D.GetString(drTemp["CD_PAY_STEP"]);
						dr평가자["CD_DUTY_WORK"] = D.GetString(drTemp["CD_DUTY_WORK"]);
						dr평가자["CD_JOB_SERIES"] = D.GetString(drTemp["CD_JOB_SERIES"]);
						dr평가자["CD_CC"] = D.GetString(drTemp["CD_CC"]);
						dr평가자["CD_PJT"] = D.GetString(drTemp["CD_PJT"]);
						dr평가자["NUM_EWEIGHT"] = 0;

						this._flex평가자.DataTable.Rows.Add(dr평가자);
						this._flex평가자.Row = this._flex평가자.Rows.Count - 1;
						this.ToolBarSaveButtonEnabled = true;
						#endregion
					}

					if (!string.IsNullOrEmpty(D.GetString(dr["NO_EMPM"])) &&
						!string.IsNullOrEmpty(D.GetString(dr["NO_EMPAN"])) && 
						this._flex피평가자.DataTable.Select("NO_EMPM = '" + D.GetString(dr["NO_EMPM"]) + "' AND NO_EMPAN = '" + D.GetString(dr["NO_EMPAN"]) + "'").Length == 0)
					{
						query1 = query + Environment.NewLine +
								"WHERE ME.CD_COMPANY = '" + D.GetString(dr["CD_BIZAREA"]) + "'" + Environment.NewLine +
								"AND ME.NO_EMP = '" + D.GetString(dr["NO_EMPAN"]) + "'";

						dt = DBHelper.GetDataTable(query1);
						if (dt == null || dt.Rows.Count == 0)
						{
							this.ShowMessage("사원이 존재하지 않습니다. (회사코드 : " + D.GetString(dr["CD_BIZAREA"]) + ", 사원번호 : " + D.GetString(dr["NO_EMPAN"]) + ")");
							continue;
						}
						else
							drTemp = dt.Rows[0];

						dr피평가자 = this._flex피평가자.DataTable.NewRow();

						dr피평가자["S"] = "N";
						dr피평가자["CD_COMPANY"] = D.GetString(drTemp["CD_COMPANY"]);
						dr피평가자["CD_EVALU"] = this.ctx평가코드.CodeValue;
						dr피평가자["CD_EVTYPE"] = D.GetString(this.cbo평가유형.SelectedValue);
						dr피평가자["CD_EVNUMBER"] = D.GetString(this.cbo평가차수.SelectedValue);
						dr피평가자["CD_GROUP"] = D.GetString(this.cbo평가그룹.SelectedValue);
						dr피평가자["NO_EMPM"] = D.GetString(dr["NO_EMPM"]);
						dr피평가자["NO_EMPAN"] = D.GetString(dr["NO_EMPAN"]);
						dr피평가자["NM_KOR"] = D.GetString(drTemp["NM_KOR"]);
						dr피평가자["CD_DUTY_RANK"] = D.GetString(drTemp["CD_DUTY_RANK"]);
						dr피평가자["NM_DUTY_RANK"] = D.GetString(drTemp["NM_DUTY_RANK"]);
						dr피평가자["CD_DEPT"] = D.GetString(drTemp["CD_DEPT"]);
						dr피평가자["NM_DEPT"] = D.GetString(drTemp["NM_DEPT"]);
						dr피평가자["CD_BIZAREA"] = D.GetString(drTemp["CD_BIZAREA"]);
						dr피평가자["CD_EMP"] = D.GetString(drTemp["CD_EMP"]);
						dr피평가자["TP_EMP"] = D.GetString(drTemp["TP_EMP"]);
						dr피평가자["CD_DUTY_STEP"] = D.GetString(drTemp["CD_DUTY_STEP"]);
						dr피평가자["CD_DUTY_RESP"] = D.GetString(drTemp["CD_DUTY_RESP"]);
						dr피평가자["CD_DUTY_TYPE"] = D.GetString(drTemp["CD_DUTY_TYPE"]);
						dr피평가자["CD_PAY_STEP"] = D.GetString(drTemp["CD_PAY_STEP"]);
						dr피평가자["CD_DUTY_WORK"] = D.GetString(drTemp["CD_DUTY_WORK"]);
						dr피평가자["CD_JOB_SERIES"] = D.GetString(drTemp["CD_JOB_SERIES"]);
						dr피평가자["CD_CC"] = D.GetString(drTemp["CD_CC"]);
						dr피평가자["CD_PJT"] = D.GetString(drTemp["CD_PJT"]);

						this._flex피평가자.DataTable.Rows.Add(dr피평가자);
						this.ToolBarSaveButtonEnabled = true;
					}
				}

				MsgControl.CloseMsg();

				this.ShowMessage("엑셀업로드 작업을 완료하였습니다.");

				this._flex평가자.Redraw = true;
				this._flex피평가자.Redraw = true;
				#endregion
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
			finally
			{
				this._flex평가자.Redraw = true;
				this._flex피평가자.Redraw = true;
				MsgControl.CloseMsg();
			}
		}

		private void btn복사_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this.BeforeSearch())
					return;
				if (this._flex평가자.DataTable.Rows.Count > 0)
				{
					this.ShowMessage("CZ_등록된 평가자가 존재합니다. 평가자 삭제 후 다시 시도해주세요.");
				}
				else if (new P_CZ_HR_PEVALU_HUMEMP_SUB(this.ctx평가코드.CodeValue, D.GetString(this.cbo평가유형.SelectedValue), D.GetString(this.cbo평가그룹.SelectedValue), D.GetString(this.cbo평가차수.SelectedValue)).ShowDialog() == DialogResult.OK)
					this.OnToolBarSearchButtonClicked(null, null);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}
		#endregion
	}
}

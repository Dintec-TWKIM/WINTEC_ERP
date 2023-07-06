using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.ERPU;
using System;
using System.Data;
using System.Windows.Forms;

namespace cz
{
	public partial class P_CZ_PR_POP_REG_SUB1 : Duzon.Common.Forms.CommonDialog
	{
		private string _공장;
		private string _작업지시번호;
		private int _공정번호;
		private bool _재작업여부;
		private DataRow _dr;
		private string _focus;

		public P_CZ_PR_POP_REG_SUB1()
		{
			InitializeComponent();
		}

		public P_CZ_PR_POP_REG_SUB1(string 공장, string 작업지시번호, int 공정번호, bool 재작업여부, string 설비코드, string 설비명)
		{
			InitializeComponent();

			this._공장 = 공장;
			this._작업지시번호 = 작업지시번호;
			this._공정번호 = 공정번호;
			this._재작업여부 = 재작업여부;

			if (this._재작업여부 == true)
			{
				this.TitleText = "실적등록(재작업)";
				this.lbl작업잔량.Text = "재작업잔량";
			}
			else
			{
				this.TitleText = "실적등록";
				this.lbl작업잔량.Text = "작업잔량";
			}

			this.ctx설비.CodeValue = 설비코드;
			this.ctx설비.CodeName = 설비명;
		}

		protected override void InitLoad()
		{
			base.InitLoad();

			this.InitGrid();
			this.InitEvent();
		}

		private void InitGrid()
		{
			#region 측정치참고
			this._flex측정치참고.BeginSetting(1, 1, false);

			this._flex측정치참고.SetCol("NO_INSP", "측정순번", 100);
			this._flex측정치참고.SetCol("DC_ITEM", "구분", 100);
			this._flex측정치참고.SetCol("CD_MEASURE", "측정장비", 100);
			this._flex측정치참고.SetCol("DC_LOCATION", "위치", 100);
			this._flex측정치참고.SetCol("DC_SPEC", "SPEC", 100);
			this._flex측정치참고.SetCol("MIN_VALUE", "최소값", 100);
			this._flex측정치참고.SetCol("MAX_VALUE", "최대값", 100);
			this._flex측정치참고.SetCol("TP_DATA", "대표값유형", 100);
			this._flex측정치참고.SetCol("CNT_INSP", "측정포인트", 100);
			this._flex측정치참고.SetCol("YN_CERT", "성적서여부", 60, false, CheckTypeEnum.Y_N);

			this._flex측정치참고.SetDataMap("CD_MEASURE", Global.MainFrame.GetComboDataCombine("S;CZ_WIN0012"), "CODE", "NAME");
			this._flex측정치참고.SetDataMap("TP_DATA", MA.GetCodeUser(new string[] { "MIN", "MAX" }, new string[] { "최소값", "최대값" }), "CODE", "NAME");

			this._flex측정치참고.SettingVersion = "0.0.0.1";
			this._flex측정치참고.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			#endregion

			#region 측정치
			this._flex측정치.BeginSetting(1, 1, false);

			this._flex측정치.SetCol("SEQ_WO", "순번", 100);
			this._flex측정치.SetCol("NO_ID", "ID NO", 100);
			this._flex측정치.SetCol("DC_ITEM", "구분", 100);
			this._flex측정치.SetCol("TP_DATA", "대표값유형", 100);
			this._flex측정치.SetCol("NO_INSP", "측정순번", 100);
			this._flex측정치.SetCol("NO_DATA1", "측정치1", 100, true);
			this._flex측정치.SetCol("NO_DATA2", "측정치2", 100, true);
			this._flex측정치.SetCol("NO_DATA3", "측정치3", 100, true);
			this._flex측정치.SetCol("NO_DATA4", "측정치4", 100, true);
			this._flex측정치.SetCol("NO_DATA5", "측정치5", 100, true);
			this._flex측정치.SetCol("NO_DATA", "대표측정치", 100);

			this._flex측정치.SetDataMap("TP_DATA", MA.GetCodeUser(new string[] { "MIN", "MAX" }, new string[] { "최소값", "최대값" }), "CODE", "NAME");

			this._flex측정치.SettingVersion = "0.0.0.1";
			this._flex측정치.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			#endregion
		}

		private void InitEvent()
		{
			this.btnNum1.Click += BtnNum_Click;
			this.btnNum2.Click += BtnNum_Click;
			this.btnNum3.Click += BtnNum_Click;
			this.btnNum4.Click += BtnNum_Click;
			this.btnNum5.Click += BtnNum_Click;
			this.btnNum6.Click += BtnNum_Click;
			this.btnNum7.Click += BtnNum_Click;
			this.btnNum8.Click += BtnNum_Click;
			this.btnNum9.Click += BtnNum_Click;
			this.btnNum0.Click += BtnNum_Click;
			this.btnNum00.Click += BtnNum_Click;
			this.btnNumMinus.Click += BtnNum_Click;

			this.btnClear.Click += BtnClear_Click;
			this.btnBackspace.Click += BtnBackspace_Click;

			this.btn진행.Click += Btn진행_Click;
			this.btn대기.Click += Btn대기_Click;
			this.btn실적등록.Click += Btn실적등록_Click;

			this.ctx설비.QueryBefore += Ctx설비_QueryBefore;

			this.cur실적수량.GotFocus += Cur양품수량_GotFocus;
			this.cur불량수량.GotFocus += Cur불량수량_GotFocus;
			this.cur불량처리수량.GotFocus += Cur불량처리수량_GotFocus;

			this._flex측정치.Click += _flex측정치_Click;
		}

		private void _flex측정치_Click(object sender, EventArgs e)
		{
			if (this._flex측정치.Cols[this._flex측정치.Col].Name == "NO_DATA1")
				this._focus = "4";
			else if (this._flex측정치.Cols[this._flex측정치.Col].Name == "NO_DATA2")
				this._focus = "5";
			else if (this._flex측정치.Cols[this._flex측정치.Col].Name == "NO_DATA3")
				this._focus = "6";
			else if (this._flex측정치.Cols[this._flex측정치.Col].Name == "NO_DATA4")
				this._focus = "7";
			else if (this._flex측정치.Cols[this._flex측정치.Col].Name == "NO_DATA5")
				this._focus = "8";
		}

		private void Cur양품수량_GotFocus(object sender, EventArgs e)
		{
			this._focus = "1";
		}

		private void Cur불량수량_GotFocus(object sender, EventArgs e)
		{
			this._focus = "2";
		}

		private void Cur불량처리수량_GotFocus(object sender, EventArgs e)
		{
			this._focus = "3";
		}

		protected override void InitPaint()
		{
			base.InitPaint();

			DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_POP_REG_SUB_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																						  this._공장,
																						  this._작업지시번호,
																						  this._공정번호});

			if (dt == null || dt.Rows.Count == 0)
			{
				Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
				this.DialogResult = DialogResult.Cancel;
				return;
			}

			this._dr = dt.Rows[0];

			this.txt지시번호.Text = this._작업지시번호;
			this.txt수주번호.Text = this._dr["NO_SO"].ToString();
			this.ctx생산품.CodeValue = this._dr["CD_ITEM"].ToString();
			this.ctx생산품.CodeName = this._dr["NM_ITEM"].ToString();
			this.txt도면번호.Text = this._dr["NO_DESIGN"].ToString();
			this.txt재질.Text = this._dr["MAT_ITEM"].ToString();
			this.txt현재공정.Text = this._dr["NM_OP"].ToString();
			this.txt다음공정.Text = this._dr["NM_OP_NEXT"].ToString();
			this.cur입고수량.Text = this._dr["QT_START"].ToString();

			if (this._재작업여부 == true)
				this.cur작업잔량.Text = this._dr["QT_REWORK_REMAIN"].ToString();
			else
				this.cur작업잔량.Text = this._dr["QT_REMAIN"].ToString();

			this.cbo불량종류.DataSource = Global.MainFrame.GetComboDataCombine("S;QU_2000007");
			this.cbo불량종류.ValueMember = "CODE";
			this.cbo불량종류.DisplayMember = "NAME";

			this.cbo불량원인.DataSource = Global.MainFrame.GetComboDataCombine("S;QU_2000009");
			this.cbo불량원인.ValueMember = "CODE";
			this.cbo불량원인.DisplayMember = "NAME";

			this._flex측정치참고.Binding = DBHelper.GetDataTable("SP_CZ_PR_POP_REG_SUB1_INSP", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																									          this._공장,
																									          this._작업지시번호,
																									          this._공정번호 });

			this._flex측정치.Binding = DBHelper.GetDataTable("SP_CZ_PR_POP_REG_SUB1_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																									   this._공장,
																									   this._작업지시번호,
																									   this._공정번호 });

			this._focus = "1";

			this.splitContainer1.SplitterDistance = 99;
		}

		private void Btn대기_Click(object sender, EventArgs e)
		{
			string query;

			try
			{
				query = @"UPDATE PR_WO_ROUT
						  SET CD_USERDEF1 = 'D'
						  WHERE CD_COMPANY = '{0}'
						  AND NO_WO = '{1}'
						  AND NO_LINE = {2}";

				DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 
															this.txt지시번호.Text,
														    this._공정번호));
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void Btn진행_Click(object sender, EventArgs e)
		{
			string query;

			try
			{
				query = @"UPDATE PR_WO_ROUT
						  SET CD_USERDEF1 = 'I'
						  WHERE CD_COMPANY = '{0}'
						  AND NO_WO = '{1}'
						  AND NO_LINE = {2}";

				DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
															this.txt지시번호.Text,
															this._공정번호));
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void Btn실적등록_Click(object sender, EventArgs e)
		{
			object[] outParameters = new object[1];

			try
			{
				if (Global.MainFrame.ShowMessage("선택된 대상분에 대해서 저장(실적처리) 하시겠습니까?", "QY2") != DialogResult.Yes) return;

				if (string.IsNullOrEmpty(this.ctx설비.CodeValue))
				{
					Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl설비.Text);
					return;
				}

				if (this.cur실적수량.DecimalValue <= 0)
				{
					Global.MainFrame.ShowMessage(공통메세지._은_보다커야합니다, new string[] { this.lbl실적수량.Text, "0" });
					return;
				}

				if (this.cur실적수량.DecimalValue > this.cur작업잔량.DecimalValue)
				{
					Global.MainFrame.ShowMessage(공통메세지._은_보다작거나같아야합니다, new string[] { this.lbl실적수량.Text, this.lbl작업잔량.Text });
					return;
				}

				if (this.cur불량수량.DecimalValue > 0)
				{
					if (this.cur불량수량.DecimalValue > this.cur실적수량.DecimalValue)
					{
						Global.MainFrame.ShowMessage(공통메세지._은_보다작거나같아야합니다, new string[] { this.lbl불량수량.Text, this.lbl실적수량.Text });
						return;
					}

					if (string.IsNullOrEmpty(this.cbo불량종류.SelectedValue.ToString()))
					{
						Global.MainFrame.ShowMessage("불량종류가 선택되어 있지 않습니다.");
						return;
					}

					if (string.IsNullOrEmpty(this.cbo불량원인.SelectedValue.ToString()))
					{
						Global.MainFrame.ShowMessage("불량원인이 선택되어 있지 않습니다.");
						return;
					}
				}

				if (this.cur불량처리수량.DecimalValue > this.cur불량수량.DecimalValue)
				{
					Global.MainFrame.ShowMessage(공통메세지._은_보다작거나같아야합니다, new string[] { this.lbl불량처리수량.Text, this.lbl불량수량.Text });
					return;
				}

				if (string.IsNullOrEmpty(this._dr["CD_SL_IN"].ToString()))
				{
					Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, "공장품목등록 -> 입고S/L");
					return;
				}

				if (string.IsNullOrEmpty(this._dr["CD_SL_BAD"].ToString()))
				{
					Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, "작업장별공정등록 -> 공정불량창고");
					return;
				}

				foreach (DataRow dr in this._flex측정치.DataTable.Select(string.Empty, string.Empty, DataViewRowState.ModifiedCurrent))
				{
					DBHelper.ExecuteNonQuery("SP_CZ_PR_POP_REG_SUB1_I", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																					   this._작업지시번호,
																					   dr["SEQ_WO"].ToString(),
																					   dr["NO_LINE"].ToString(),
																					   dr["NO_INSP"].ToString(),
																					   Global.MainFrame.LoginInfo.EmployeeNo,
																					   dr["SPEC_VALUE"].ToString(),
																					   D.GetDecimal(dr["NO_DATA1"]),
																					   D.GetDecimal(dr["NO_DATA2"]),
																					   D.GetDecimal(dr["NO_DATA3"]),
																					   D.GetDecimal(dr["NO_DATA4"]),
																					   D.GetDecimal(dr["NO_DATA5"]),
																					   Global.MainFrame.LoginInfo.UserID });
				}

				this._flex측정치.AcceptChanges();

				DBHelper.ExecuteNonQuery("SP_CZ_PR_LINK_MES_I", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																			   this._공장,
																			   this.ctx생산품.CodeValue,
																			   Global.MainFrame.LoginInfo.EmployeeNo,
																			   this._dr["CD_WC"].ToString(),
																			   this._dr["CD_OP"].ToString(),
																			   this._dr["CD_WCOP"].ToString(),
																			   "N",
																			   this._작업지시번호,
																			   "Y",
																			   this.cur실적수량.DecimalValue,
																			   this.cur불량수량.DecimalValue,
																			   this.cur불량처리수량.DecimalValue,
																			   this._dr["CD_SL_IN"].ToString(),
																			   this._dr["CD_SL_BAD"].ToString(),
																			   (this._재작업여부 == true ? "Y" : "N"),
																			   "N",
																			   0,
																			   0,
																			   0,
																			   string.Empty,
																			   string.Empty,
																			   this.ctx설비.CodeValue,
																			   (this.cur불량수량.DecimalValue > 0 ? this.cbo불량종류.SelectedValue.ToString() : string.Empty),
																			   (this.cur불량수량.DecimalValue > 0 ? this.cbo불량원인.SelectedValue.ToString() : string.Empty),
																			   string.Empty,
																			   Global.MainFrame.LoginInfo.UserID }, out outParameters);

				DBHelper.ExecuteNonQuery("SP_CZ_PR_LINK_MES_BATCH", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																				   this._공장,
																				   outParameters[0].ToString() });

				this.DialogResult = DialogResult.OK;
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void Ctx설비_QueryBefore(object sender, BpQueryArgs e)
		{
			try
			{
				e.HelpParam.P00_CHILD_MODE = "설비 도움창";
				e.HelpParam.P61_CODE1 = "WE.CD_EQUIP AS CODE, EQ.NM_EQUIP AS NAME ";
				e.HelpParam.P62_CODE2 = @"PR_WCOP_EQUIP WE
										  LEFT JOIN PR_EQUIP EQ ON WE.CD_COMPANY = EQ.CD_COMPANY AND WE.CD_PLANT = EQ.CD_PLANT AND WE.CD_EQUIP = EQ.CD_EQUIP";
				e.HelpParam.P63_CODE3 = string.Format(@"WHERE WE.CD_COMPANY = '{0}'
														AND WE.CD_PLANT = '{1}'", Global.MainFrame.LoginInfo.CompanyCode, this._공장);
				e.HelpParam.P64_CODE4 = "GROUP BY WE.CD_EQUIP, EQ.NM_EQUIP";
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void BtnBackspace_Click(object sender, EventArgs e)
		{
			string value;

			try
			{
				if (this._focus == "1" && this.cur실적수량.Text.Trim() != string.Empty)
				{
					value = this.cur실적수량.Text.Substring(0, this.cur실적수량.Text.Length - 1);
					if (value == "-") value = string.Empty;

					this.cur실적수량.Text = value;
				}
				else if (this._focus == "2" && this.cur불량수량.Text.Trim() != string.Empty)
				{
					value = this.cur불량수량.Text.Substring(0, this.cur불량수량.Text.Length - 1);
					if (value == "-") value = string.Empty;

					this.cur불량수량.Text = value;
				}
				else if (this._focus == "3" && this.cur불량처리수량.Text.Trim() != string.Empty)
				{
					value = this.cur불량처리수량.Text.Substring(0, this.cur불량처리수량.Text.Length - 1);
					if (value == "-") value = string.Empty;

					this.cur불량처리수량.Text = value;
				}
				else if (this._focus == "4" && this._flex측정치["NO_DATA1"].ToString() != string.Empty)
				{
					value = this._flex측정치["NO_DATA1"].ToString().Substring(0, this._flex측정치["NO_DATA1"].ToString().Length - 1);
					if (value == "-") value = string.Empty;

					this._flex측정치["NO_DATA1"] = value;
				}
				else if (this._focus == "5" && this._flex측정치["NO_DATA2"].ToString() != string.Empty)
				{
					value = this._flex측정치["NO_DATA2"].ToString().Substring(0, this._flex측정치["NO_DATA2"].ToString().Length - 1);
					if (value == "-") value = string.Empty;

					this._flex측정치["NO_DATA2"] = value;
				}
				else if (this._focus == "6" && this._flex측정치["NO_DATA3"].ToString() != string.Empty)
				{
					value = this._flex측정치["NO_DATA3"].ToString().Substring(0, this._flex측정치["NO_DATA3"].ToString().Length - 1);
					if (value == "-") value = string.Empty;

					this._flex측정치["NO_DATA3"] = value;
				}
				else if (this._focus == "7" && this._flex측정치["NO_DATA4"].ToString() != string.Empty)
				{
					value = this._flex측정치["NO_DATA4"].ToString().Substring(0, this._flex측정치["NO_DATA4"].ToString().Length - 1);
					if (value == "-") value = string.Empty;

					this._flex측정치["NO_DATA4"] = value;
				}
				else if (this._focus == "8" && this._flex측정치["NO_DATA5"].ToString() != string.Empty)
				{
					value = this._flex측정치["NO_DATA5"].ToString().Substring(0, this._flex측정치["NO_DATA5"].ToString().Length - 1);
					if (value == "-") value = string.Empty;

					this._flex측정치["NO_DATA5"] = value;
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void BtnClear_Click(object sender, EventArgs e)
		{
			try
			{
				if (this._focus == "1")
					this.cur실적수량.Text = string.Empty;
				else if (this._focus == "2")
					this.cur불량수량.Text = string.Empty;
				else if (this._focus == "3")
					this.cur불량처리수량.Text = string.Empty;
				else if (this._focus == "4")
					this._flex측정치["NO_DATA1"] = string.Empty;
				else if (this._focus == "5")
					this._flex측정치["NO_DATA2"] = string.Empty;
				else if (this._focus == "6")
					this._flex측정치["NO_DATA3"] = string.Empty;
				else if (this._focus == "7")
					this._flex측정치["NO_DATA4"] = string.Empty;
				else if (this._focus == "8")
					this._flex측정치["NO_DATA5"] = string.Empty;
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void BtnNum_Click(object sender, EventArgs e)
		{
			string name;

			try
			{
				name = ((Control)sender).Name;

				if (name == this.btnNum1.Name)
				{
					if (this._focus == "1")
						this.cur실적수량.Text += "1";
					else if (this._focus == "2")
						this.cur불량수량.Text += "1";
					else if (this._focus == "3")
						this.cur불량처리수량.Text += "1";
					else if (this._focus == "4")
						this._flex측정치["NO_DATA1"] += "1";
					else if (this._focus == "5")
						this._flex측정치["NO_DATA2"] += "1";
					else if (this._focus == "6")
						this._flex측정치["NO_DATA3"] += "1";
					else if (this._focus == "7")
						this._flex측정치["NO_DATA4"] += "1";
					else if (this._focus == "8")
						this._flex측정치["NO_DATA5"] += "1";
				}
				else if (name == this.btnNum2.Name)
				{
					if (this._focus == "1")
						this.cur실적수량.Text += "2";
					else if (this._focus == "2")
						this.cur불량수량.Text += "2";
					else if (this._focus == "3")
						this.cur불량처리수량.Text += "2";
					else if (this._focus == "4")
						this._flex측정치["NO_DATA1"] += "2";
					else if (this._focus == "5")
						this._flex측정치["NO_DATA2"] += "2";
					else if (this._focus == "6")
						this._flex측정치["NO_DATA3"] += "2";
					else if (this._focus == "7")
						this._flex측정치["NO_DATA4"] += "2";
					else if (this._focus == "8")
						this._flex측정치["NO_DATA5"] += "2";
				}
				else if (name == this.btnNum3.Name)
				{
					if (this._focus == "1")
						this.cur실적수량.Text += "3";
					else if (this._focus == "2")
						this.cur불량수량.Text += "3";
					else if (this._focus == "3")
						this.cur불량처리수량.Text += "3";
					else if (this._focus == "4")
						this._flex측정치["NO_DATA1"] += "3";
					else if (this._focus == "5")
						this._flex측정치["NO_DATA2"] += "3";
					else if (this._focus == "6")
						this._flex측정치["NO_DATA3"] += "3";
					else if (this._focus == "7")
						this._flex측정치["NO_DATA4"] += "3";
					else if (this._focus == "8")
						this._flex측정치["NO_DATA5"] += "3";
				}
				else if (name == this.btnNum4.Name)
				{
					if (this._focus == "1")
						this.cur실적수량.Text += "4";
					else if (this._focus == "2")
						this.cur불량수량.Text += "4";
					else if (this._focus == "3")
						this.cur불량처리수량.Text += "4";
					else if (this._focus == "4")
						this._flex측정치["NO_DATA1"] += "4";
					else if (this._focus == "5")
						this._flex측정치["NO_DATA2"] += "4";
					else if (this._focus == "6")
						this._flex측정치["NO_DATA3"] += "4";
					else if (this._focus == "7")
						this._flex측정치["NO_DATA4"] += "4";
					else if (this._focus == "8")
						this._flex측정치["NO_DATA5"] += "4";
				}
				else if (name == this.btnNum5.Name)
				{
					if (this._focus == "1")
						this.cur실적수량.Text += "5";
					else if (this._focus == "2")
						this.cur불량수량.Text += "5";
					else if (this._focus == "3")
						this.cur불량처리수량.Text += "5";
					else if (this._focus == "4")
						this._flex측정치["NO_DATA1"] += "5";
					else if (this._focus == "5")
						this._flex측정치["NO_DATA2"] += "5";
					else if (this._focus == "6")
						this._flex측정치["NO_DATA3"] += "5";
					else if (this._focus == "7")
						this._flex측정치["NO_DATA4"] += "5";
					else if (this._focus == "8")
						this._flex측정치["NO_DATA5"] += "5";
				}
				else if (name == this.btnNum6.Name)
				{
					if (this._focus == "1")
						this.cur실적수량.Text += "6";
					else if (this._focus == "2")
						this.cur불량수량.Text += "6";
					else if (this._focus == "3")
						this.cur불량처리수량.Text += "6";
					else if (this._focus == "4")
						this._flex측정치["NO_DATA1"] += "6";
					else if (this._focus == "5")
						this._flex측정치["NO_DATA2"] += "6";
					else if (this._focus == "6")
						this._flex측정치["NO_DATA3"] += "6";
					else if (this._focus == "7")
						this._flex측정치["NO_DATA4"] += "6";
					else if (this._focus == "8")
						this._flex측정치["NO_DATA5"] += "6";
				}
				else if (name == this.btnNum7.Name)
				{
					if (this._focus == "1")
						this.cur실적수량.Text += "7";
					else if (this._focus == "2")
						this.cur불량수량.Text += "7";
					else if (this._focus == "3")
						this.cur불량처리수량.Text += "7";
					else if (this._focus == "4")
						this._flex측정치["NO_DATA1"] += "7";
					else if (this._focus == "5")
						this._flex측정치["NO_DATA2"] += "7";
					else if (this._focus == "6")
						this._flex측정치["NO_DATA3"] += "7";
					else if (this._focus == "7")
						this._flex측정치["NO_DATA4"] += "7";
					else if (this._focus == "8")
						this._flex측정치["NO_DATA5"] += "7";
				}
				else if (name == this.btnNum8.Name)
				{
					if (this._focus == "1")
						this.cur실적수량.Text += "8";
					else if (this._focus == "2")
						this.cur불량수량.Text += "8";
					else if (this._focus == "3")
						this.cur불량처리수량.Text += "8";
					else if (this._focus == "4")
						this._flex측정치["NO_DATA1"] += "8";
					else if (this._focus == "5")
						this._flex측정치["NO_DATA2"] += "8";
					else if (this._focus == "6")
						this._flex측정치["NO_DATA3"] += "8";
					else if (this._focus == "7")
						this._flex측정치["NO_DATA4"] += "8";
					else if (this._focus == "8")
						this._flex측정치["NO_DATA5"] += "8";
				}
				else if (name == this.btnNum9.Name)
				{
					if (this._focus == "1")
						this.cur실적수량.Text += "9";
					else if (this._focus == "2")
						this.cur불량수량.Text += "9";
					else if (this._focus == "3")
						this.cur불량처리수량.Text += "9";
					else if (this._focus == "4")
						this._flex측정치["NO_DATA1"] += "9";
					else if (this._focus == "5")
						this._flex측정치["NO_DATA2"] += "9";
					else if (this._focus == "6")
						this._flex측정치["NO_DATA3"] += "9";
					else if (this._focus == "7")
						this._flex측정치["NO_DATA4"] += "9";
					else if (this._focus == "8")
						this._flex측정치["NO_DATA5"] += "9";
				}
				else if (name == this.btnNum0.Name)
				{
					if (this._focus == "1")
						this.cur실적수량.Text += "0";
					else if (this._focus == "2")
						this.cur불량수량.Text += "0";
					else if (this._focus == "3")
						this.cur불량처리수량.Text += "0";
					else if (this._focus == "4")
						this._flex측정치["NO_DATA1"] += "0";
					else if (this._focus == "5")
						this._flex측정치["NO_DATA2"] += "0";
					else if (this._focus == "6")
						this._flex측정치["NO_DATA3"] += "0";
					else if (this._focus == "7")
						this._flex측정치["NO_DATA4"] += "0";
					else if (this._focus == "8")
						this._flex측정치["NO_DATA5"] += "0";
				}
				else if (name == this.btnNum00.Name)
				{
					if (this._focus == "1")
						this.cur실적수량.Text += "00";
					else if (this._focus == "2")
						this.cur불량수량.Text += "00";
					else if (this._focus == "3")
						this.cur불량처리수량.Text += "00";
					else if (this._focus == "4")
						this._flex측정치["NO_DATA1"] += "00";
					else if (this._focus == "5")
						this._flex측정치["NO_DATA2"] += "00";
					else if (this._focus == "6")
						this._flex측정치["NO_DATA3"] += "00";
					else if (this._focus == "7")
						this._flex측정치["NO_DATA4"] += "00";
					else if (this._focus == "8")
						this._flex측정치["NO_DATA5"] += "00";
				}
				else if (name == this.btnNumMinus.Name)
				{
					if (this._focus == "1")
						this.cur실적수량.Text += "-";
					else if (this._focus == "2")
						this.cur불량수량.Text += "-";
					else if (this._focus == "3")
						this.cur불량처리수량.Text += "-";
					else if (this._focus == "4" && !string.IsNullOrEmpty(this._flex측정치["NO_DATA1"].ToString()))
						this._flex측정치["NO_DATA1"] += "-";
					else if (this._focus == "5" && !string.IsNullOrEmpty(this._flex측정치["NO_DATA2"].ToString()))
						this._flex측정치["NO_DATA2"] += "-";
					else if (this._focus == "6" && !string.IsNullOrEmpty(this._flex측정치["NO_DATA3"].ToString()))
						this._flex측정치["NO_DATA3"] += "-";
					else if (this._focus == "7" && !string.IsNullOrEmpty(this._flex측정치["NO_DATA4"].ToString()))
						this._flex측정치["NO_DATA4"] += "-";
					else if (this._focus == "8" && !string.IsNullOrEmpty(this._flex측정치["NO_DATA5"].ToString()))
						this._flex측정치["NO_DATA5"] += "-";
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}
	}
}

using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.ERPU;
using System;
using System.Data;
using System.Windows.Forms;

namespace cz
{
	public partial class P_CZ_PR_POP_REG_SUB : Duzon.Common.Forms.CommonDialog
	{
		private string _공장;
		private string _작업지시번호;
		private int _공정번호;
		private bool _재작업여부;
		private DataRow _dr;
		private string _focus;

		public P_CZ_PR_POP_REG_SUB()
		{
			InitializeComponent();
		}

		public P_CZ_PR_POP_REG_SUB(string 공장, string 작업지시번호, int 공정번호, bool 재작업여부, string 설비코드, string 설비명)
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

			this.InitEvent();
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

			this.btnClear.Click += BtnClear_Click;
			this.btnBackspace.Click += BtnBackspace_Click;

			this.btn진행.Click += Btn진행_Click;
			this.btn대기.Click += Btn대기_Click;
			this.btn실적등록.Click += Btn실적등록_Click;

			this.ctx설비.QueryBefore += Ctx설비_QueryBefore;

			this.cur실적수량.GotFocus += Cur실적수량_GotFocus;
			this.cur불량수량.GotFocus += Cur불량수량_GotFocus;
			this.cur불량처리수량.GotFocus += Cur불량처리수량_GotFocus;
		}

		private void Cur실적수량_GotFocus(object sender, EventArgs e)
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
																						  this._공정번호 });

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

			this._focus = "1";
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
			try
			{
				if (this._focus == "1" && this.cur실적수량.Text.Trim() != string.Empty)
					this.cur실적수량.Text = this.cur실적수량.Text.Substring(0, this.cur실적수량.Text.Length - 1);
				else if (this._focus == "2" && this.cur불량수량.Text.Trim() != string.Empty)
					this.cur불량수량.Text = this.cur불량수량.Text.Substring(0, this.cur불량수량.Text.Length - 1);
				else if (this._focus == "3" && this.cur불량처리수량.Text.Trim() != string.Empty)
					this.cur불량처리수량.Text = this.cur불량처리수량.Text.Substring(0, this.cur불량처리수량.Text.Length - 1);
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
				}
				else if (name == this.btnNum2.Name)
				{
					if (this._focus == "1")
						this.cur실적수량.Text += "2";
					else if (this._focus == "2")
						this.cur불량수량.Text += "2";
					else if (this._focus == "3")
						this.cur불량처리수량.Text += "2";
				}
				else if (name == this.btnNum3.Name)
				{
					if (this._focus == "1")
						this.cur실적수량.Text += "3";
					else if (this._focus == "2")
						this.cur불량수량.Text += "3";
					else if (this._focus == "3")
						this.cur불량처리수량.Text += "3";
				}
				else if (name == this.btnNum4.Name)
				{
					if (this._focus == "1")
						this.cur실적수량.Text += "4";
					else if (this._focus == "2")
						this.cur불량수량.Text += "4";
					else if (this._focus == "3")
						this.cur불량처리수량.Text += "4";
				}
				else if (name == this.btnNum5.Name)
				{
					if (this._focus == "1")
						this.cur실적수량.Text += "5";
					else if (this._focus == "2")
						this.cur불량수량.Text += "5";
					else if (this._focus == "3")
						this.cur불량처리수량.Text += "5";
				}
				else if (name == this.btnNum6.Name)
				{
					if (this._focus == "1")
						this.cur실적수량.Text += "6";
					else if (this._focus == "2")
						this.cur불량수량.Text += "6";
					else if (this._focus == "3")
						this.cur불량처리수량.Text += "6";
				}
				else if (name == this.btnNum7.Name)
				{
					if (this._focus == "1")
						this.cur실적수량.Text += "7";
					else if (this._focus == "2")
						this.cur불량수량.Text += "7";
					else if (this._focus == "3")
						this.cur불량처리수량.Text += "7";
				}
				else if (name == this.btnNum8.Name)
				{
					if (this._focus == "1")
						this.cur실적수량.Text += "8";
					else if (this._focus == "2")
						this.cur불량수량.Text += "8";
					else if (this._focus == "3")
						this.cur불량처리수량.Text += "8";
				}
				else if (name == this.btnNum9.Name)
				{
					if (this._focus == "1")
						this.cur실적수량.Text += "9";
					else if (this._focus == "2")
						this.cur불량수량.Text += "9";
					else if (this._focus == "3")
						this.cur불량처리수량.Text += "9";
				}
				else if (name == this.btnNum0.Name)
				{
					if (this._focus == "1")
						this.cur실적수량.Text += "0";
					else if (this._focus == "2")
						this.cur불량수량.Text += "0";
					else if (this._focus == "3")
						this.cur불량처리수량.Text += "0";
				}
				else if (name == this.btnNum00.Name)
				{
					if (this._focus == "1")
						this.cur실적수량.Text += "00";
					else if (this._focus == "2")
						this.cur불량수량.Text += "00";
					else if (this._focus == "3")
						this.cur불량처리수량.Text += "00";
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}
	}
}

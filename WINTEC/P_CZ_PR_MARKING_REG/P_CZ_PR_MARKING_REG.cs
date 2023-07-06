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
	public partial class P_CZ_PR_MARKING_REG : PageBase
	{
		P_CZ_PR_MARKING_REG_BIZ _biz = new P_CZ_PR_MARKING_REG_BIZ();
		DataRow dr작업지시, drTRUST마킹, dr납품의뢰;

		public P_CZ_PR_MARKING_REG()
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
			#region 작업지시
			this._flex작업지시.BeginSetting(1, 1, false);

			this._flex작업지시.SetCol("NO_WO", "작업지시번호", 100);
			this._flex작업지시.SetCol("NM_OP", "공정명", 100);
			this._flex작업지시.SetCol("CD_ITEM", "품목코드", 100);
			this._flex작업지시.SetCol("NM_ITEM", "품목명", 100);
			this._flex작업지시.SetCol("NO_DESIGN", "도면번호", 100);
			this._flex작업지시.SetCol("STND_ITEM", "규격", 100);
			this._flex작업지시.SetCol("QT_START", "입고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex작업지시.SetCol("QT_MARKING", "마킹수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex작업지시.SetCol("QT_WORK", "실적수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);

			this._flex작업지시.SettingVersion = "0.0.0.1";
			this._flex작업지시.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			#endregion

			#region TRUST마킹
			this._flexTRUST마킹.BeginSetting(1, 1, false);

			this._flexTRUST마킹.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
			this._flexTRUST마킹.SetCol("NO_SO", "수주번호", 100);
			this._flexTRUST마킹.SetCol("SEQ_SO", "순번", 100);
			this._flexTRUST마킹.SetCol("CD_MATL", "품목코드", 100);
			this._flexTRUST마킹.SetCol("NM_ITEM", "품목명", 100);
			this._flexTRUST마킹.SetCol("NO_DESIGN", "도면번호", 100);
			this._flexTRUST마킹.SetCol("STND_ITEM", "규격", 100);
			this._flexTRUST마킹.SetCol("QT_SO", "수주수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexTRUST마킹.SetCol("QT_MARKING", "마킹수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);

			this._flexTRUST마킹.AddDummyColumn("S");

			this._flexTRUST마킹.SettingVersion = "0.0.0.1";
			this._flexTRUST마킹.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			#endregion

			#region 납품의뢰
			this._flex납품의뢰.BeginSetting(1, 1, false);

			this._flex납품의뢰.SetCol("NO_GIR", "납품의뢰번호", 100);
			this._flex납품의뢰.SetCol("SEQ_GIR", "순번", 100);
			this._flex납품의뢰.SetCol("CD_MATL", "품목코드", 100);
			this._flex납품의뢰.SetCol("NM_ITEM", "품목명", 100);
			this._flex납품의뢰.SetCol("NO_DESIGN", "도면번호", 100);
			this._flex납품의뢰.SetCol("STND_ITEM", "규격", 100);
			this._flex납품의뢰.SetCol("QT_GIR", "납품의뢰수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex납품의뢰.SetCol("QT_MARKING", "마킹수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);

			this._flex납품의뢰.SettingVersion = "0.0.0.1";
			this._flex납품의뢰.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			#endregion
		}

		private void InitEvent()
		{
			this.btn마킹.Click += Btn마킹_Click;
			this.btn마킹취소.Click += Btn마킹취소_Click;
			this.btn실적등록.Click += Btn실적등록_Click;
			this.btn마킹품목설정.Click += Btn마킹품목설정_Click;
			this.btnTRUST마킹실적.Click += BtnTRUST마킹실적_Click;
			this.btn삭제.Click += Btn삭제_Click;

			this.cbo작업단계.SelectedValueChanged += Cbo작업단계_SelectedValueChanged;
			this._flex작업지시.AfterRowChange += _flex작업지시_AfterRowChange;
			this._flex납품의뢰.AfterRowChange += _flex납품의뢰_AfterRowChange;
			this._flexTRUST마킹.AfterRowChange += _flexTRUST마킹_AfterRowChange;
		}

		protected override void InitPaint()
		{
			base.InitPaint();

			this.cbo작업단계.DataSource = MA.GetCodeUser(new string[] { "001", "002", "003" }, new string[] { "작업지시", "TRUST마킹" , "납품의뢰" });
			this.cbo작업단계.ValueMember = "CODE";
			this.cbo작업단계.DisplayMember = "NAME";

			this.cbo설비.DataSource = DBHelper.GetDataTable(string.Format(@"SELECT EQ.CD_EQUIP AS CODE,
																				   EQ.NM_EQUIP AS NAME
																			FROM PR_EQUIP EQ WITH(NOLOCK)
																			WHERE EQ.CD_COMPANY = '{0}'
																			AND EXISTS (SELECT 1 
																					    FROM PR_WCOP_EQUIP WE WITH(NOLOCK)
																						WHERE WE.CD_COMPANY = EQ.CD_COMPANY
																						AND WE.CD_EQUIP = EQ.CD_EQUIP
																						AND WE.CD_WC = 'W531')", Global.MainFrame.LoginInfo.CompanyCode));
			this.cbo설비.ValueMember = "CODE";
			this.cbo설비.DisplayMember = "NAME";

			this.splitContainer1.SplitterDistance = 934;
		}

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarSearchButtonClicked(sender, e);

				if (!BeforeSearch()) return;

				if (this.cbo작업단계.SelectedValue.ToString() == "001")
				{
					this._flex작업지시.Binding = this._biz.SearchWO(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																				   this.cbo설비.SelectedValue.ToString(),
																				   this.txt작업지시번호S.Text,
																				   this.txt품목코드S.Text });

					if (!this._flex작업지시.HasNormalRow)
						this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
				}
				else if (this.cbo작업단계.SelectedValue.ToString() == "002")
                {
					this._flexTRUST마킹.Binding = this._biz.SearchTM(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																				    this.txt수주번호S.Text,
																				    this.txt품목코드S.Text });

					if (!this._flexTRUST마킹.HasNormalRow)
						this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
				}
				else
				{
					this._flex납품의뢰.Binding = this._biz.SearchGIR(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																				   this.txt납품의뢰번호S.Text,
																				   this.txt품목코드S.Text });

					if (!this._flex납품의뢰.HasNormalRow)
						this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void Cbo작업단계_SelectedValueChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.cbo작업단계.SelectedValue.ToString() == "001")
				{
					this.tabControl1.SelectedTab = this.tpg작업지시;
					this.tabControl2.SelectedTab = this.tpg작업지시1;
					this.btn실적등록.Enabled = true;
				}
				else if (this.cbo작업단계.SelectedValue.ToString() == "002")
                {
					this.tabControl1.SelectedTab = this.tpgTRUST마킹;
					this.tabControl2.SelectedTab = this.tpgTRUST마킹1;
					this.btn실적등록.Enabled = false;
				}
				else
				{
					this.tabControl1.SelectedTab = this.tpg납품의뢰;
					this.tabControl2.SelectedTab = this.tpg납품의뢰1;
					this.btn실적등록.Enabled = false;
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void Btn마킹_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.cbo작업단계.SelectedValue.ToString() == "001")
				{
					if (this._flex작업지시.HasNormalRow == false) return;
					if (string.IsNullOrEmpty(this.txtQR코드.Text))
					{
						this.ShowMessage("마킹 가능한 항목이 없습니다.");
						return;
					}

					//마킹작업

					DBHelper.ExecuteNonQuery("SP_CZ_PR_MARKING_REG_WO_U", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																						 dr작업지시["NO_WO"].ToString(),
																						 dr작업지시["SEQ_WO"].ToString(),
																						 dr작업지시["NO_LINE"].ToString(),
																						 "Y",
																						 Global.MainFrame.LoginInfo.UserID });

					this.작업지시갱신();
				}
				else if (this.cbo작업단계.SelectedValue.ToString() == "002")
                {
					if (this._flexTRUST마킹.HasNormalRow == false) return;
					if (string.IsNullOrEmpty(this.txtQR코드2.Text))
                    {
						this.ShowMessage("마킹 가능한 항목이 없습니다.");
						return;
                    }
					if (string.IsNullOrEmpty(this.txt가공ID번호2.Text))
                    {
						this.ShowMessage(공통메세지._은는필수입력항목입니다, "가공ID번호");
						this.txt가공ID번호2.Focus();
						return;
					}

					//마킹작업

					DBHelper.ExecuteNonQuery("SP_CZ_PR_MARKING_REG_TM_I", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																						 this._flexTRUST마킹["NO_SO"].ToString(),
																						 D.GetDecimal(this._flexTRUST마킹["SEQ_SO"]),
																						 this._flexTRUST마킹["CD_MATL"].ToString(),
																						 this.txtQR코드2.Text,
																						 this.txt가공ID번호2.Text,
																						 Global.MainFrame.LoginInfo.UserID });

					this.TRUST마킹갱신();
				}
				else
				{
					if (this._flex납품의뢰.HasNormalRow == false) return;
					if (string.IsNullOrEmpty(this.txt텍스트.Text))
					{
						this.ShowMessage("마킹 가능한 항목이 없습니다.");
						return;
					}

					//마킹작업

					DBHelper.ExecuteNonQuery("SP_CZ_PR_MARKING_REG_GIR_I", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																						  this._flex납품의뢰["NO_GIR"].ToString(),
																						  D.GetDecimal(this._flex납품의뢰["SEQ_GIR"]),
																						  D.GetDecimal(this._flex납품의뢰["NO_SEQ"]),
																						  this._flex납품의뢰["CD_MATL"].ToString(),
																						  this.txt텍스트.Text,
																						  Global.MainFrame.LoginInfo.UserID });

					this.납품의뢰갱신();
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void Btn마킹취소_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.cbo작업단계.SelectedValue.ToString() == "001")
				{
					if (this._flex작업지시.HasNormalRow == false) return;
					if (string.IsNullOrEmpty(this.txtQR코드.Text))
					{
						this.ShowMessage("마킹 취소 가능한 항목이 없습니다.");
						return;
					}

					DBHelper.ExecuteNonQuery("SP_CZ_PR_MARKING_REG_WO_U", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																						 dr작업지시["NO_WO"].ToString(),
																						 dr작업지시["SEQ_WO"].ToString(),
																						 dr작업지시["NO_LINE"].ToString(),
																						 "N",
																						 Global.MainFrame.LoginInfo.UserID });

					this.작업지시갱신();
				}
				else if (this.cbo작업단계.SelectedValue.ToString() == "002")
				{
					if (this._flexTRUST마킹.HasNormalRow == false) return;
					if (string.IsNullOrEmpty(this.txtQR코드2.Text))
					{
						this.ShowMessage("마킹 취소 가능한 항목이 없습니다.");
						return;
					}

					DBHelper.ExecuteNonQuery("SP_CZ_PR_MARKING_REG_TM_D", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																						 this._flexTRUST마킹["NO_SO"].ToString(),
																						 D.GetDecimal(this._flexTRUST마킹["SEQ_SO"]),
																						 this._flexTRUST마킹["CD_MATL"].ToString()});

					this.TRUST마킹갱신();
				}
				else
				{
					if (this._flex납품의뢰.HasNormalRow == false) return;
					if (string.IsNullOrEmpty(this.txt텍스트.Text))
					{
						this.ShowMessage("마킹 취소 가능한 항목이 없습니다.");
						return;
					}

					DBHelper.ExecuteNonQuery("SP_CZ_PR_MARKING_REG_GIR_D", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																						  this._flex납품의뢰["NO_GIR"].ToString(),
																						  D.GetDecimal(this._flex납품의뢰["SEQ_GIR"]),
																						  D.GetDecimal(this._flex납품의뢰["NO_SEQ"]),
																						  this._flex납품의뢰["CD_MATL"].ToString() });

					this.납품의뢰갱신();
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void Btn실적등록_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flex작업지시.HasNormalRow) return;

				if (this.cbo작업단계.SelectedValue.ToString() != "001")
				{
					this.ShowMessage("작업지시 단계만 실적등록 가능 합니다.");
					return;
				}

				bool isSuccess = DBHelper.ExecuteNonQuery("SP_CZ_PR_MARKING_REG_WO_I", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																									  this._flex작업지시["NO_WO"].ToString(),
																									  this._flex작업지시["NO_LINE"].ToString(),
																									  Global.MainFrame.LoginInfo.UserID });

				if (isSuccess == true)
					this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void Btn삭제_Click(object sender, EventArgs e)
		{
			DataTable dt;
			DataRow[] dataRowArray;
			string query;
			try
			{
				if (!this._flexTRUST마킹.HasNormalRow) return;

				dataRowArray = this._flexTRUST마킹.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					query = @"SELECT *
FROM CZ_SA_SOL_TRUST_WINTEC
WHERE CD_COMPANY = '{0}'
AND NO_SO = '{1}'
AND SEQ_SO = '{2}'
AND YN_TRUST = 'Y'";
					foreach (DataRow dr in dataRowArray)
					{
						dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, dr["NO_SO"].ToString(), dr["SEQ_SO"].ToString()));
						if (dt.Rows.Count > 0)
						{
							this.ShowMessage("TRUST 마킹 실적이 있는 건이 선택되어 삭제할 수 없습니다.");
							return;
						}
					}
						
					query = @"UPDATE SA_SOL
SET TXT_USERDEF9 = 'N'
WHERE CD_COMPANY = '{0}'
AND NO_SO = '{1}'
AND SEQ_SO = '{2}'";
					foreach (DataRow dr in dataRowArray)
					{
						DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, dr["NO_SO"].ToString(), dr["SEQ_SO"].ToString()));
					}
					this.OnToolBarSearchButtonClicked(null, null);
					this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn삭제.Text);
				}
				
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void BtnTRUST마킹실적_Click(object sender, EventArgs e)
        {
            try
            {
				P_CZ_TRUST_MARKING_REG_SUB dialog = new P_CZ_TRUST_MARKING_REG_SUB();
				dialog.ShowDialog();

				if (this._flexTRUST마킹.HasNormalRow == false) return;
				TRUST마킹갱신();
			}
			catch (Exception ex)
            {
				this.MsgEnd(ex);
            }
        }

		private void Btn마킹품목설정_Click(object sender, EventArgs e)
		{
			try
			{
				P_CZ_PR_MARKING_REG_SUB dialog = new P_CZ_PR_MARKING_REG_SUB();
				dialog.ShowDialog();
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void _flex작업지시_AfterRowChange(object sender, RangeEventArgs e)
		{
			try
			{
				if (this._flex작업지시.HasNormalRow == false) return;

				this.작업지시갱신();
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void _flexTRUST마킹_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
				if (this._flexTRUST마킹.HasNormalRow == false) return;

				this.TRUST마킹갱신();
            }
			catch (Exception ex)
            {
				this.MsgEnd(ex);
            }
        }

		private void _flex납품의뢰_AfterRowChange(object sender, RangeEventArgs e)
		{
			try
			{
				if (this._flex납품의뢰.HasNormalRow == false) return;

				this.납품의뢰갱신();
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void 작업지시갱신()
		{
			DataTable dt;

			try
			{
				dt = this._biz.SearchWODetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
															 this._flex작업지시["NO_WO"].ToString(),
															 D.GetDecimal(this._flex작업지시["NO_LINE"]) });

				if (dt != null && dt.Rows.Count > 0)
				{
					dr작업지시 = dt.Rows[0];

					this.txt작업지시번호.Text = dr작업지시["NO_WO"].ToString() + " / " + dr작업지시["SEQ_WO"].ToString();
					this.txt공정명.Text = dr작업지시["NM_OP"].ToString();
					this.txt품목코드.Text = dr작업지시["CD_ITEM"].ToString();
					this.txt품목명.Text = dr작업지시["NM_ITEM"].ToString();
					this.txt도면번호.Text = dr작업지시["NO_DESIGN"].ToString();
					this.txtQR코드.Text = dr작업지시["CD_QR"].ToString();
					this.txtID번호.Text = dr작업지시["NO_ID"].ToString();
				}
				else
				{
					dr작업지시 = null;

					this.txt작업지시번호.Text = string.Empty;
					this.txt공정명.Text = string.Empty;
					this.txt품목코드.Text = string.Empty;
					this.txt품목명.Text = string.Empty;
					this.txt도면번호.Text = string.Empty;
					this.txtQR코드.Text = string.Empty;
					this.txtID번호.Text = string.Empty;
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

        private void TRUST마킹갱신()
        {
			DataTable dt;

            try
            {
				dt = this._biz.SearchTMDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
															 this._flexTRUST마킹["NO_SO"].ToString(),
															 D.GetDecimal(this._flexTRUST마킹["SEQ_SO"]),
															 this._flexTRUST마킹["CD_MATL"].ToString() });

				if (dt != null && dt.Rows.Count > 0)
                {
					drTRUST마킹 = dt.Rows[0];

					this.txt수주번호2.Text = drTRUST마킹["NO_SO"].ToString() + " / " + drTRUST마킹["SEQ_SO"].ToString();
					this.txt품목코드2.Text = drTRUST마킹["CD_MATL"].ToString();
					this.txt품목명2.Text = drTRUST마킹["NM_ITEM"].ToString();
					this.txt도면번호2.Text = drTRUST마킹["NO_DESIGN"].ToString();
					this.txt시리얼.Text = drTRUST마킹["NO_TRUST"].ToString();
					this.txt가공ID번호2.Text = string.Empty;
					this.txtQR코드2.Text = drTRUST마킹["CD_QR"].ToString();

					this.txt가공ID번호2.Focus();
				}
                else
                {
					drTRUST마킹 = null;

					this.txt수주번호2.Text = string.Empty;
					this.txt품목코드2.Text = string.Empty;
					this.txt품목명2.Text = string.Empty;
					this.txt도면번호2.Text = string.Empty;
					this.txt가공ID번호2.Text = string.Empty;
					this.txtQR코드2.Text = string.Empty;
				}
            }
			catch (Exception ex)
            {
				this.MsgEnd(ex);
            }
        }

		private void 납품의뢰갱신()
		{
			DataTable dt;

			try
			{
				dt = this._biz.SearchGIRDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																  this._flex납품의뢰["NO_GIR"].ToString(),
																  D.GetDecimal(this._flex납품의뢰["SEQ_GIR"]),
																  D.GetDecimal(this._flex납품의뢰["NO_SEQ"]),
																  this._flex납품의뢰["CD_MATL"].ToString() });

				if (dt != null && dt.Rows.Count > 0)
				{
					dr납품의뢰 = dt.Rows[0];

					this.txt납품의뢰번호.Text = dr납품의뢰["NO_GIR"].ToString() + " / " + dr납품의뢰["NO_LINE"].ToString();
					this.txt품목코드1.Text = dr납품의뢰["CD_MATL"].ToString();
					this.txt품목명1.Text = dr납품의뢰["NM_ITEM"].ToString();
					this.txt도면번호1.Text = dr납품의뢰["NO_DESIGN"].ToString();
					this.txt텍스트.Text = dr납품의뢰["DC_TEXT"].ToString();
				}
				else
				{
					dr납품의뢰 = null;

					this.txt납품의뢰번호.Text = string.Empty;
					this.txt품목코드1.Text = string.Empty;
					this.txt품목명1.Text = string.Empty;
					this.txt도면번호1.Text = string.Empty;
					this.txt텍스트.Text = string.Empty;
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}
	}
}

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
	public partial class P_CZ_PR_WO_SA_SOL_CHANGE : PageBase
	{
		P_CZ_PR_WO_SA_SOL_CHANGE_BIZ _biz = new P_CZ_PR_WO_SA_SOL_CHANGE_BIZ();

		public P_CZ_PR_WO_SA_SOL_CHANGE()
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

			this.dtp납기일자.StartDateToString = Global.MainFrame.GetDateTimeToday().AddYears(-1).ToString("yyyyMMdd");
			this.dtp납기일자.EndDateToString = Global.MainFrame.GetStringToday;

			this.cbo경로유형.DataSource = DBHelper.GetDataTable("UP_PR_PATN_ROUT_ALL_S2", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																										 Global.SystemLanguage.MultiLanguageLpoint });
			this.cbo경로유형.DisplayMember = "NAME";
			this.cbo경로유형.ValueMember = "CODE";
		}

		private void InitGrid()
		{
			#region 수주정보 Header
			this._flex수주정보H.BeginSetting(1, 1, false);

			this._flex수주정보H.SetCol("NO_SO", "수주번호", 100);
			this._flex수주정보H.SetCol("SEQ_SO", "수주항번", 100);
			this._flex수주정보H.SetCol("DT_DUEDATE", "납기일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex수주정보H.SetCol("CD_ITEM", "품목코드", 100);
			this._flex수주정보H.SetCol("NM_ITEM", "품목명", 100);
			this._flex수주정보H.SetCol("QT_SO", "수주수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex수주정보H.SetCol("QT_GIR", "의뢰수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex수주정보H.SetCol("QT_GI", "출고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex수주정보H.SetCol("QT_APPLY", "지시수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex수주정보H.SetCol("QT_STOCK", "재고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);

			this._flex수주정보H.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.SingleColumn, SumPositionEnum.None);
			#endregion

			#region 수주정보 Line
			this._flex수주정보L.BeginSetting(1, 1, false);

			this._flex수주정보L.SetCol("LEVEL", "레벨", 60);
			this._flex수주정보L.SetCol("CD_MATL", "품목코드", 100);
			this._flex수주정보L.SetCol("NM_MATL", "품목명", 100);
			this._flex수주정보L.SetCol("QT_SO", "수주수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex수주정보L.SetCol("QT_BOM", "BOM수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex수주정보L.SetCol("QT_NEED", "필요수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex수주정보L.SetCol("QT_APPLY", "지시수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex수주정보L.SetCol("QT_STOCK", "재고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex수주정보L.SetCol("QT_REMAIN", "나머지수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex수주정보L.SetCol("QT_ADD", "추가수량", 100, true, typeof(decimal), FormatTpType.QUANTITY);

			this._flex수주정보L.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.SingleColumn, SumPositionEnum.None);
			#endregion

			#region 작업지시정보 Header
			this._flex작업지시H.BeginSetting(1, 1, false);

			this._flex작업지시H.SetCol("NUM_ORDER", "우선순위", 100, true, typeof(decimal), FormatTpType.QUANTITY);
			this._flex작업지시H.SetCol("NO_WO", "지시번호", 100);
			this._flex작업지시H.SetCol("DT_DUEDATE", "납기일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex작업지시H.SetCol("CD_ITEM", "품목코드", 100);
			this._flex작업지시H.SetCol("NM_ITEM", "품목명", 100);
			this._flex작업지시H.SetCol("QT_ITEM", "지시수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex작업지시H.SetCol("QT_REJECT", "불량수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex작업지시H.SetCol("NM_WC", "작업장", 100);
			this._flex작업지시H.SetCol("NM_OP", "공정", 100);
			this._flex작업지시H.SetCol("QT_START", "입고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex작업지시H.SetCol("QT_WORK", "작업수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex작업지시H.SetCol("QT_APPLY", "수주수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			
			this._flex작업지시H.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.SingleColumn, SumPositionEnum.None);
			#endregion

			#region 작업지시정보 Line
			this._flex작업지시L.BeginSetting(1, 1, false);

			this._flex작업지시L.SetCol("NO_SO", "수주번호", 100);
			this._flex작업지시L.SetCol("DT_DUEDATE", "납기일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex작업지시L.SetCol("NO_LINE", "수주항번", 100);
			this._flex작업지시L.SetCol("QT_APPLY", "지시수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex작업지시L.SetCol("QT_GIR", "의뢰수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex작업지시L.SetCol("QT_GI", "출고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);

			this._flex작업지시L.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.SingleColumn, SumPositionEnum.Top);
			#endregion

			#region 수주변경
			this._flex작업지시S.BeginSetting(1, 1, false);

			this._flex작업지시S.SetCol("NO_SO", "수주번호", 100);
			this._flex작업지시S.SetCol("DT_DUEDATE", "납기일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex작업지시S.SetCol("NO_LINE", "수주항번", 100);
			this._flex작업지시S.SetCol("NO_WO", "작업지시번호", 100);
			this._flex작업지시S.SetCol("QT_APPLY", "지시수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex작업지시S.SetCol("QT_GIR", "의뢰수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex작업지시S.SetCol("QT_GI", "출고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);

			this._flex작업지시S.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.SingleColumn, SumPositionEnum.Top);
			#endregion
		}

		private void InitEvent()
		{
			this._flex수주정보H.AfterRowChange += _flex수주정보H_AfterRowChange;
			this._flex수주정보L.AfterRowChange += _flex수주정보L_AfterRowChange;
			this._flex작업지시H.AfterRowChange += _flex작업지시H_AfterRowChange;
			this._flex작업지시L.AfterRowChange += _flex작업지시L_AfterRowChange;

			this._flex작업지시H.AfterEdit += _flex작업지시H_AfterEdit;

			this.btn수주추가.Click += Btn수주추가_Click;
			this.btn수주제거.Click += Btn수주제거_Click;
			this.btn수주변경.Click += Btn수주변경_Click;
			this.btn작업지시갱신.Click += Btn작업지시갱신_Click;
			this.btn수주갱신.Click += Btn수주갱신_Click;
			this.btn자동변경.Click += Btn자동변경_Click;

			this.ctx품목코드.QueryBefore += Ctx품목코드_QueryBefore;
		}

		private void _flex작업지시H_AfterEdit(object sender, RowColEventArgs e)
		{
			FlexGrid flexGrid;
			string query, name;

			try
			{
				flexGrid = ((FlexGrid)sender);

				if (flexGrid.HasNormalRow == false) return;

				name = flexGrid.Cols[e.Col].Name;

				switch (name)
				{
					case "NUM_ORDER":
						query = @"UPDATE WO
SET NUM_USERDEF1 = '{2}',
    ID_UPDATE = '{3}',
    DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
FROM PR_WO WO
WHERE WO.CD_COMPANY = '{0}'
AND WO.NO_WO = '{1}'";

						DBHelper.ExecuteScalar(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																				   this._flex작업지시H["NO_WO"].ToString(),
																				   this._flex작업지시H["NUM_ORDER"].ToString(),
																				   Global.MainFrame.LoginInfo.UserID }));
						break;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flex작업지시L_AfterRowChange(object sender, RangeEventArgs e)
		{
			if (this._flex작업지시L["YN_USE"].ToString() == "Y")
			{
				this.btn수주변경.Enabled = false;
				this.btn수주제거.Enabled = false;
			}
			else
			{
				this.btn수주변경.Enabled = true;
				this.btn수주제거.Enabled = true;
			}
		}

		private void Btn자동변경_Click(object sender, EventArgs e)
		{
			try
			{
				if (string.IsNullOrEmpty(this.cbo경로유형.SelectedValue.ToString()))
				{
					this.ShowMessage("경로유형이 선택되어 있지 않습니다.");
					return;
				}

				if (this.ShowMessage("자동변경 작업을 진행 하시겠습니까?\n작업 진행 실행시, 이전 데이터는 복구 할 수 없습니다.", "QY2") != DialogResult.Yes)
					return;

				DBHelper.ExecuteNonQuery("SP_CZ_PR_WO_SA_SOL_CHANGE_AUTO", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																						  this.cbo경로유형.SelectedValue.ToString() });

				this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn자동변경.Text);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn수주갱신_Click(object sender, EventArgs e)
		{
			try
			{
				this._flex수주정보H_AfterRowChange(null, null);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn작업지시갱신_Click(object sender, EventArgs e)
		{
			try
			{
				this._flex작업지시H_AfterRowChange(null, null);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Ctx품목코드_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
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

		private void Btn수주변경_Click(object sender, EventArgs e)
		{
			string query;

			try
			{
				if (!this._flex작업지시L.HasNormalRow ||
					!this._flex작업지시S.HasNormalRow ) return;

				if (D.GetDecimal(this._flex작업지시L["QT_GIR"]) > 0 ||
					D.GetDecimal(this._flex작업지시L["QT_GI"]) > 0 ||
					D.GetDecimal(this._flex작업지시S["QT_GIR"]) > 0 ||
					D.GetDecimal(this._flex작업지시S["QT_GI"]) > 0)
				{
					this.ShowMessage("출고처리 된 수주건은 변경 할 수 없습니다.");
					return;
				}

				query = @"SELECT MP.NO_WO 
FROM CZ_PR_SA_SOL_PR_WO_MAPPING MP WITH(NOLOCK)
WHERE MP.CD_COMPANY = '{0}'
AND MP.NO_WO = '{1}'
AND MP.NO_SO = '{2}'
AND MP.NO_LINE = {3}
AND MP.QT_APPLY = '{4}'";

				DataTable dt = DBHelper.GetDataTable(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																						 this._flex작업지시L["NO_WO"].ToString(),
																					     this._flex작업지시L["NO_SO"].ToString(),
																						 D.GetDecimal(this._flex작업지시L["NO_LINE"]),
																						 D.GetDecimal(this._flex작업지시L["QT_APPLY"]) }));

				if (dt == null || dt.Rows.Count == 0)
				{
					this.ShowMessage("유효하지 않은 수주가 선택되어 있습니다.\n데이터 갱신 후 다시 시도하시기 바랍니다.");
					return;
				}

				dt = DBHelper.GetDataTable(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																			   this._flex작업지시S["NO_WO"].ToString(),
																			   this._flex작업지시S["NO_SO"].ToString(),
																			   D.GetDecimal(this._flex작업지시S["NO_LINE"]),
																			   D.GetDecimal(this._flex작업지시S["QT_APPLY"]) }));

				if (dt == null || dt.Rows.Count == 0)
				{
					this.ShowMessage("유효하지 않은 수주가 선택되어 있습니다.\n데이터 갱신 후 다시 시도하시기 바랍니다.");
					return;
				}

				query = @"SELECT WI.NO_SO
FROM CZ_PR_WO_INSP WI WITH(NOLOCK)
WHERE WI.CD_COMPANY = '{0}'
AND WI.NO_WO = '{1}'
AND WI.NO_SO = '{2}'
AND WI.NO_INSP IN (996, 997)";

				dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
																this._flex작업지시L["NO_WO"].ToString(),
															    this._flex작업지시L["NO_SO"].ToString()));

				if (dt != null && dt.Rows.Count > 0)
				{
					this.ShowMessage("현합, 조립공정에 사용된 수주번호 입니다.");
					return;
				}

				DBHelper.ExecuteNonQuery("SP_CZ_PR_WO_SA_SOL_CHANGE_SWITCH", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																						    this._flex작업지시L["NO_WO"].ToString(),
																							this._flex작업지시L["NO_SO"].ToString(),
																							D.GetDecimal(this._flex작업지시L["NO_LINE"]),
																							D.GetDecimal(this._flex작업지시L["QT_APPLY"]),
																							this._flex작업지시S["NO_WO"].ToString(),
																							this._flex작업지시S["NO_SO"].ToString(),
																							D.GetDecimal(this._flex작업지시S["NO_LINE"]),
																							D.GetDecimal(this._flex작업지시S["QT_APPLY"]),
																							Global.MainFrame.LoginInfo.UserID });

				this._flex작업지시H_AfterRowChange(null, null);

				this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn수주변경.Text);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn수주제거_Click(object sender, EventArgs e)
		{
			string query;

			try
			{
				if (!this._flex작업지시L.HasNormalRow) return;

				query = @"SELECT WI.NO_SO
FROM CZ_PR_WO_INSP WI WITH(NOLOCK)
WHERE WI.CD_COMPANY = '{0}'
AND WI.NO_WO = '{1}'
AND WI.NO_SO = '{2}'
AND WI.NO_INSP IN (996, 997)";

				DataTable dt = DBHelper.GetDataTable(string.Format(query, this._flex작업지시L["CD_COMPANY"].ToString(),
																		  this._flex작업지시L["NO_WO"].ToString(),
																		  this._flex작업지시L["NO_SO"].ToString()));

				if (dt != null && dt.Rows.Count > 0)
				{
					this.ShowMessage("현합, 조립공정에 사용된 수주번호 입니다.");
					return;
				}

				query = @"DELETE A 
FROM CZ_PR_SA_SOL_PR_WO_MAPPING A 
WHERE A.CD_COMPANY = '{0}'
AND A.CD_PLANT = '{1}'
AND A.NO_SO = '{2}'
AND A.NO_LINE = '{3}'
AND A.NO_WO = '{4}'";

				DBHelper.ExecuteScalar(string.Format(query, this._flex작업지시L["CD_COMPANY"].ToString(),
															this._flex작업지시L["CD_PLANT"].ToString(),
															this._flex작업지시L["NO_SO"].ToString(),
															this._flex작업지시L["NO_LINE"].ToString(),
															this._flex작업지시L["NO_WO"].ToString()));

				this._flex작업지시H_AfterRowChange(null, null);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn수주추가_Click(object sender, EventArgs e)
		{
			DataTable dt;
			decimal 추가수량, 나머지수량, 수주수량;
			string query;

			try
			{
				추가수량 = D.GetDecimal(this._flex수주정보L["QT_ADD"]);
				나머지수량 = D.GetDecimal(this._flex수주정보L["QT_REMAIN"]);

				if (추가수량 <= 0)
				{
					this.ShowMessage(공통메세지._은_보다커야합니다, "추가수량", "0");
					return;
				}

				if (추가수량 > 나머지수량)
				{
					this.ShowMessage(공통메세지._은_보다작거나같아야합니다, "추가수량", "나머지수량");
					return;
				}

				query = @"SELECT ISNULL(SUM(QT_APPLY), 0) AS QT_APPLY 
FROM CZ_PR_SA_SOL_PR_WO_MAPPING WM WITH(NOLOCK)
WHERE WM.CD_COMPANY = '{0}'
AND WM.NO_WO = '{1}'";


				수주수량 = D.GetDecimal(DBHelper.ExecuteScalar(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																					               this._flex작업지시H["NO_WO"].ToString() })));

				if ((D.GetDecimal(this._flex작업지시H["QT_ITEM"]) - D.GetDecimal(this._flex작업지시H["QT_REJECT"])) < 수주수량 + 추가수량)
				{
					this.ShowMessage("수주수량은 (작업지시수량 - 불량수량) 보다 작거나 같아야 합니다.");
					return;
				}

				dt = this._flex작업지시L.DataTable.Copy();

				DataRow dr = dt.NewRow();

				dr["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
				dr["CD_PLANT"] = Global.MainFrame.LoginInfo.CdPlant;
				dr["NO_SO"] = this._flex수주정보L["NO_SO"].ToString();
				dr["NO_LINE"] = this._flex수주정보L["SEQ_SO"].ToString();
				dr["NO_WO"] = this._flex작업지시H["NO_WO"].ToString();
				dr["CD_ITEM"] = this._flex작업지시H["CD_ITEM"].ToString();
				dr["QT_APPLY"] = 추가수량;
				dr["NO_SO_S"] = this._flex작업지시H["NO_SO"].ToString();
				dr["SEQ_SO"] = this._flex작업지시H["SEQ_SO"].ToString();

				dt.Rows.Add(dr);

				this._biz.Save(dt);

				this._flex작업지시H_AfterRowChange(null, null);

				this._flex수주정보L["QT_ADD"] = 0;
				this._flex수주정보L["QT_REMAIN"] = D.GetDecimal(this._flex수주정보L["QT_REMAIN"]) - 추가수량;
				this._flex수주정보L["QT_APPLY"] = D.GetDecimal(this._flex수주정보L["QT_APPLY"]) + 추가수량;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flex수주정보H_AfterRowChange(object sender, RangeEventArgs e)
		{
			try
			{
				this._flex수주정보L.Binding = this._biz.Search1(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																			   this.cbo경로유형.SelectedValue.ToString(),
																			   this._flex수주정보H["NO_SO"].ToString(),
																			   this._flex수주정보H["SEQ_SO"].ToString(),
																			   this._flex수주정보H["CD_ITEM"].ToString(),
																			   this.txt작업지시번호.Text,
																			   this.ctx품목코드.CodeValue });
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flex수주정보L_AfterRowChange(object sender, RangeEventArgs e)
		{
			try
			{
				this._flex작업지시H.Binding = this._biz.Search2(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																			   this._flex수주정보L["NO_SO"].ToString(),
																			   this._flex수주정보L["SEQ_SO"].ToString(),
																			   this._flex수주정보L["CD_MATL"].ToString(),
																			   (this.chk의뢰제외.Checked == true ? "Y" : "N"),
																			   (this.chk출고제외.Checked == true ? "Y" : "N"),
																			   this.txt작업지시번호.Text });
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flex작업지시H_AfterRowChange(object sender, RangeEventArgs e)
		{
			try
			{
				this._flex작업지시L.Binding = this._biz.Search3(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
														                       this._flex작업지시H["NO_SO"].ToString(),
														                       this._flex작업지시H["SEQ_SO"].ToString(),
														                       this._flex작업지시H["NO_WO"].ToString(),
														                       (this.chk의뢰제외.Checked == true ? "Y" : "N"),
														                       (this.chk출고제외.Checked == true ? "Y" : "N"),
														                       (this.chk종결제외.Checked == true ? "Y" : "N") });

				this._flex작업지시S.Binding = this._biz.Search4(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																			   this._flex작업지시H["NO_WO"].ToString(),
																			   this._flex작업지시H["CD_ITEM"].ToString(),
																			   (this.chk의뢰제외.Checked == true ? "Y" : "N"),
																			   (this.chk출고제외.Checked == true ? "Y" : "N"),
																			   (this.chk종결제외.Checked == true ? "Y" : "N") });
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

				if (!BeforeSearch()) return;

				this._flex수주정보H.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																			  this.cbo경로유형.SelectedValue,
																		      this.txt수주번호.Text,
																			  this.ctx품목코드.CodeValue,
																			  this.txt작업지시번호.Text,
																		      this.dtp납기일자.StartDateToString,
																			  this.dtp납기일자.EndDateToString,
																			  (this.chk의뢰제외.Checked == true ? "Y" : "N"),
																			  (this.chk출고제외.Checked == true ? "Y" : "N"),
																			  (this.chk종결제외.Checked == true ? "Y" : "N") });

				if (!this._flex수주정보H.HasNormalRow)
					this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}
	}
}

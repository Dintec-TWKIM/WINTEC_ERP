using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
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
	public partial class P_CZ_PR_DEPT_EQUIP_MNG : PageBase
	{
		P_CZ_PR_DEPT_EQUIP_MNG_BIZ _biz = new P_CZ_PR_DEPT_EQUIP_MNG_BIZ();

		public P_CZ_PR_DEPT_EQUIP_MNG()
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
			this.MainGrids = new FlexGrid[] { this._flex작업대기 };

			#region 작업대기
			this._flex작업대기.BeginSetting(1, 1, false);

			this._flex작업대기.SetCol("IDX_WORK", "작업순서", 100);
			this._flex작업대기.SetCol("CD_USERDEF1", "우선순위", 100, true);
			this._flex작업대기.SetCol("STND_ITEM", "엔진타입", 100);
			this._flex작업대기.SetCol("CD_ITEM", "품목코드", false);
			this._flex작업대기.SetCol("NM_ITEM", "품목명", 100);
			this._flex작업대기.SetCol("NO_DESIGN", "도면번호", 100);
			this._flex작업대기.SetCol("QT_ITEM", "수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex작업대기.SetCol("NO_WO", "작업지시번호", 100);
			this._flex작업대기.SetCol("DT_REL", "지시일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex작업대기.SetCol("NM_OP_CURR", "현재공정", 100);
			this._flex작업대기.SetCol("NM_OP", "공정명", 100);
			this._flex작업대기.SetCol("CD_EQUIP", "설비코드", 100);
			this._flex작업대기.SetCol("NM_EQUIP", "설비명", 100);
			
			this._flex작업대기.SettingVersion = "0.0.0.1";
			this._flex작업대기.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion

			#region 수주정보
			this._flex수주정보.BeginSetting(1, 1, false);

			this._flex수주정보.SetCol("LN_PARTNER", "매출처", 100);
			this._flex수주정보.SetCol("NO_PO_PARTNER", "거래처PO번호", 100);
			this._flex수주정보.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex수주정보.SetCol("NM_ENGINE", "엔진유형", 100);
			this._flex수주정보.SetCol("NM_ITEM", "품목명", 100);
			this._flex수주정보.SetCol("NO_HULL", "호선번호", 100);
			this._flex수주정보.SetCol("NO_SO", "수주번호", 100);
			this._flex수주정보.SetCol("NO_DESIGN", "도면번호", 100);
			this._flex수주정보.SetCol("QT_SO", "수주수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex수주정보.SetCol("UNIT_SO", "수주단위", 100);
			this._flex수주정보.SetCol("NM_CERT", "선급검사기관1", 100);
			this._flex수주정보.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex수주정보.SetCol("DT_REQGI", "출하예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

			this._flex수주정보.SettingVersion = "0.0.0.1";
			this._flex수주정보.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion

			#region 작업진행
			this._flex작업진행.BeginSetting(2, 1, false);

			this._flex작업진행.SetCol("CD_EQUIP", "설비코드", 100);
			this._flex작업진행.SetCol("NM_EQUIP", "설비명", 100);

			this._flex작업진행.SetCol("NO_WO", "작업지시번호", 100);
			this._flex작업진행.SetCol("STND_ITEM", "엔진타입", 100);
			this._flex작업진행.SetCol("CD_ITEM", "품목코드", false);
			this._flex작업진행.SetCol("NM_ITEM", "품목명", 100);
			this._flex작업진행.SetCol("NO_DESIGN", "도면번호", 100);
			this._flex작업진행.SetCol("NM_OP", "공정명", 100);
			this._flex작업진행.SetCol("QT_ITEM", "지시수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex작업진행.SetCol("QT_WIP", "입고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex작업진행.SetCol("QT_WORK", "작업수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex작업진행.SetCol("NM_STATE", "상태", 100);
			this._flex작업진행.SetCol("NM_EMP", "담당자", 100);

			this._flex작업진행.SetCol("STND_ITEM_1", "엔진타입", 100);
			this._flex작업진행.SetCol("CD_ITEM_1", "품목코드", false);
			this._flex작업진행.SetCol("NM_ITEM_1", "품목명", 100);
			this._flex작업진행.SetCol("NO_DESIGN_1", "도면번호", 100);
			this._flex작업진행.SetCol("QT_ITEM_1", "지시수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex작업진행.SetCol("QT_WIP_1", "입고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex작업진행.SetCol("NO_WO_1", "작업지시번호", 100);
			this._flex작업진행.SetCol("DT_REL_1", "지시일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex작업진행.SetCol("NM_OP_1", "공정명", 100);

			this._flex작업진행.SetCol("STND_ITEM_2", "엔진타입", 100);
			this._flex작업진행.SetCol("CD_ITEM_2", "품목코드", false);
			this._flex작업진행.SetCol("NM_ITEM_2", "품목명", 100);
			this._flex작업진행.SetCol("NO_DESIGN_2", "도면번호", 100);
			this._flex작업진행.SetCol("QT_ITEM_2", "지시수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex작업진행.SetCol("QT_WIP_2", "입고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex작업진행.SetCol("NO_WO_2", "작업지시번호", 100);
			this._flex작업진행.SetCol("DT_REL_2", "지시일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex작업진행.SetCol("NM_OP_2", "공정명", 100);

			this._flex작업진행.SetCol("STND_ITEM_3", "엔진타입", 100);
			this._flex작업진행.SetCol("CD_ITEM_3", "품목코드", false);
			this._flex작업진행.SetCol("NM_ITEM_3", "품목명", 100);
			this._flex작업진행.SetCol("NO_DESIGN_3", "도면번호", 100);
			this._flex작업진행.SetCol("QT_ITEM_3", "지시수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex작업진행.SetCol("QT_WIP_3", "입고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex작업진행.SetCol("NO_WO_3", "작업지시번호", 100);
			this._flex작업진행.SetCol("DT_REL_3", "지시일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex작업진행.SetCol("NM_OP_3", "공정명", 100);

			this._flex작업진행[0, this._flex작업진행.Cols["NO_WO"].Index] = "작업진행";
			this._flex작업진행[0, this._flex작업진행.Cols["CD_ITEM"].Index] = "작업진행";
			this._flex작업진행[0, this._flex작업진행.Cols["NM_ITEM"].Index] = "작업진행";
			this._flex작업진행[0, this._flex작업진행.Cols["NO_DESIGN"].Index] = "작업진행";
			this._flex작업진행[0, this._flex작업진행.Cols["STND_ITEM"].Index] = "작업진행";
			this._flex작업진행[0, this._flex작업진행.Cols["NM_OP"].Index] = "작업진행";
			this._flex작업진행[0, this._flex작업진행.Cols["QT_ITEM"].Index] = "작업진행";
			this._flex작업진행[0, this._flex작업진행.Cols["QT_WIP"].Index] = "작업진행";
			this._flex작업진행[0, this._flex작업진행.Cols["QT_WORK"].Index] = "작업진행";
			this._flex작업진행[0, this._flex작업진행.Cols["NM_STATE"].Index] = "작업진행";
			this._flex작업진행[0, this._flex작업진행.Cols["NM_EMP"].Index] = "작업진행";

			this._flex작업진행[0, this._flex작업진행.Cols["NO_WO_1"].Index] = "작업대기1";
			this._flex작업진행[0, this._flex작업진행.Cols["DT_REL_1"].Index] = "작업대기1";
			this._flex작업진행[0, this._flex작업진행.Cols["CD_ITEM_1"].Index] = "작업대기1";
			this._flex작업진행[0, this._flex작업진행.Cols["NM_ITEM_1"].Index] = "작업대기1";
			this._flex작업진행[0, this._flex작업진행.Cols["NO_DESIGN_1"].Index] = "작업대기1";
			this._flex작업진행[0, this._flex작업진행.Cols["STND_ITEM_1"].Index] = "작업대기1";
			this._flex작업진행[0, this._flex작업진행.Cols["NM_OP_1"].Index] = "작업대기1";
			this._flex작업진행[0, this._flex작업진행.Cols["QT_ITEM_1"].Index] = "작업대기1";
			this._flex작업진행[0, this._flex작업진행.Cols["QT_WIP_1"].Index] = "작업대기1";

			this._flex작업진행[0, this._flex작업진행.Cols["NO_WO_2"].Index] = "작업대기2";
			this._flex작업진행[0, this._flex작업진행.Cols["DT_REL_2"].Index] = "작업대기2";
			this._flex작업진행[0, this._flex작업진행.Cols["CD_ITEM_2"].Index] = "작업대기2";
			this._flex작업진행[0, this._flex작업진행.Cols["NM_ITEM_2"].Index] = "작업대기2";
			this._flex작업진행[0, this._flex작업진행.Cols["NO_DESIGN_2"].Index] = "작업대기2";
			this._flex작업진행[0, this._flex작업진행.Cols["STND_ITEM_2"].Index] = "작업대기2";
			this._flex작업진행[0, this._flex작업진행.Cols["NM_OP_2"].Index] = "작업대기2";
			this._flex작업진행[0, this._flex작업진행.Cols["QT_ITEM_2"].Index] = "작업대기2";
			this._flex작업진행[0, this._flex작업진행.Cols["QT_WIP_2"].Index] = "작업대기2";

			this._flex작업진행[0, this._flex작업진행.Cols["NO_WO_3"].Index] = "작업대기3";
			this._flex작업진행[0, this._flex작업진행.Cols["DT_REL_3"].Index] = "작업대기3";
			this._flex작업진행[0, this._flex작업진행.Cols["CD_ITEM_3"].Index] = "작업대기3";
			this._flex작업진행[0, this._flex작업진행.Cols["NM_ITEM_3"].Index] = "작업대기3";
			this._flex작업진행[0, this._flex작업진행.Cols["NO_DESIGN_3"].Index] = "작업대기3";
			this._flex작업진행[0, this._flex작업진행.Cols["STND_ITEM_3"].Index] = "작업대기3";
			this._flex작업진행[0, this._flex작업진행.Cols["NM_OP_3"].Index] = "작업대기3";
			this._flex작업진행[0, this._flex작업진행.Cols["QT_ITEM_3"].Index] = "작업대기3";
			this._flex작업진행[0, this._flex작업진행.Cols["QT_WIP_3"].Index] = "작업대기3";

			Color 작업진행 = Color.FromArgb(99, 190, 123);
			Color 작업대기1 = Color.FromArgb(151, 206, 134);
			Color 작업대기2 = Color.FromArgb(203, 222, 145);
			Color 작업대기3 = Color.FromArgb(255, 239, 156);

			this._flex작업진행.Cols["NO_WO"].Style.BackColor = 작업진행;
			this._flex작업진행.Cols["CD_ITEM"].Style.BackColor = 작업진행;
			this._flex작업진행.Cols["NM_ITEM"].Style.BackColor = 작업진행;
			this._flex작업진행.Cols["NO_DESIGN"].Style.BackColor = 작업진행;
			this._flex작업진행.Cols["STND_ITEM"].Style.BackColor = 작업진행;
			this._flex작업진행.Cols["NM_OP"].Style.BackColor = 작업진행;
			this._flex작업진행.Cols["QT_ITEM"].Style.BackColor = 작업진행;
			this._flex작업진행.Cols["QT_WIP"].Style.BackColor = 작업진행;
			this._flex작업진행.Cols["QT_WORK"].Style.BackColor = 작업진행;
			this._flex작업진행.Cols["NM_STATE"].Style.BackColor = 작업진행;
			this._flex작업진행.Cols["NM_EMP"].Style.BackColor = 작업진행;

			this._flex작업진행.Cols["NO_WO_1"].Style.BackColor = 작업대기1;
			this._flex작업진행.Cols["DT_REL_1"].Style.BackColor = 작업대기1;
			this._flex작업진행.Cols["CD_ITEM_1"].Style.BackColor = 작업대기1;
			this._flex작업진행.Cols["NM_ITEM_1"].Style.BackColor = 작업대기1;
			this._flex작업진행.Cols["NO_DESIGN_1"].Style.BackColor = 작업대기1;
			this._flex작업진행.Cols["STND_ITEM_1"].Style.BackColor = 작업대기1;
			this._flex작업진행.Cols["NM_OP_1"].Style.BackColor = 작업대기1;
			this._flex작업진행.Cols["QT_ITEM_1"].Style.BackColor = 작업대기1;
			this._flex작업진행.Cols["QT_WIP_1"].Style.BackColor = 작업대기1;

			this._flex작업진행.Cols["NO_WO_2"].Style.BackColor = 작업대기2;
			this._flex작업진행.Cols["DT_REL_2"].Style.BackColor = 작업대기2;
			this._flex작업진행.Cols["CD_ITEM_2"].Style.BackColor = 작업대기2;
			this._flex작업진행.Cols["NM_ITEM_2"].Style.BackColor = 작업대기2;
			this._flex작업진행.Cols["NO_DESIGN_2"].Style.BackColor = 작업대기2;
			this._flex작업진행.Cols["STND_ITEM_2"].Style.BackColor = 작업대기2;
			this._flex작업진행.Cols["NM_OP_2"].Style.BackColor = 작업대기2;
			this._flex작업진행.Cols["QT_ITEM_2"].Style.BackColor = 작업대기2;
			this._flex작업진행.Cols["QT_WIP_2"].Style.BackColor = 작업대기2;

			this._flex작업진행.Cols["NO_WO_3"].Style.BackColor = 작업대기3;
			this._flex작업진행.Cols["DT_REL_3"].Style.BackColor = 작업대기3;
			this._flex작업진행.Cols["CD_ITEM_3"].Style.BackColor = 작업대기3;
			this._flex작업진행.Cols["NM_ITEM_3"].Style.BackColor = 작업대기3;
			this._flex작업진행.Cols["NO_DESIGN_3"].Style.BackColor = 작업대기3;
			this._flex작업진행.Cols["STND_ITEM_3"].Style.BackColor = 작업대기3;
			this._flex작업진행.Cols["NM_OP_3"].Style.BackColor = 작업대기3;
			this._flex작업진행.Cols["QT_ITEM_3"].Style.BackColor = 작업대기3;
			this._flex작업진행.Cols["QT_WIP_3"].Style.BackColor = 작업대기3;
			#endregion

			this._flex작업진행.SettingVersion = "0.0.0.1";
			this._flex작업진행.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
		}

		private void InitEvent()
		{
			this._flex작업대기.AfterRowChange += _flex작업대기_AfterRowChange;

			this.btn설비할당.Click += Btn설비할당_Click;
			this.btn할당취소.Click += Btn할당취소_Click;

			this.ctx설비.QueryBefore += Ctx설비_QueryBefore;
		}

		private void Ctx설비_QueryBefore(object sender, BpQueryArgs e)
		{
			try
			{
				e.HelpParam.P00_CHILD_MODE = "설비 도움창";
				e.HelpParam.P61_CODE1 = "EQ.CD_EQUIP AS CODE, EQ.NM_EQUIP AS NAME";
				e.HelpParam.P62_CODE2 = @"PR_EQUIP EQ";
				e.HelpParam.P63_CODE3 = string.Format(@"WHERE EQ.CD_COMPANY = '{0}'
														AND EQ.CD_PLANT = '{1}'", Global.MainFrame.LoginInfo.CompanyCode, Global.MainFrame.LoginInfo.CdPlant);
				e.HelpParam.P64_CODE4 = "GROUP BY EQ.CD_EQUIP, EQ.NM_EQUIP";
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn설비할당_Click(object sender, EventArgs e)
		{
			string query;

			try
			{
				query = @"UPDATE WR
SET WR.CD_EQUIP = '{3}',
	WR.ID_UPDATE = '{4}',
    WR.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
FROM PR_WO_ROUT WR
WHERE WR.CD_COMPANY = '{0}'
AND NO_WO = '{1}'
AND NO_LINE = {2}";

				DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 
															this._flex작업대기["NO_WO"].ToString(), 
															this._flex작업대기["NO_LINE"].ToString(),
															this._flex작업진행["CD_EQUIP"].ToString(),
															Global.MainFrame.LoginInfo.UserID ));

				this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn설비할당.Text);
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void Btn할당취소_Click(object sender, EventArgs e)
		{
			string query;

			try
			{
				query = @"UPDATE WR
SET WR.CD_EQUIP = NULL,
	WR.ID_UPDATE = '{3}',
    WR.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
FROM PR_WO_ROUT WR
WHERE WR.CD_COMPANY = '{0}'
AND NO_WO = '{1}'
AND NO_LINE = {2}";

				DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
															this._flex작업대기["NO_WO"].ToString(),
															this._flex작업대기["NO_LINE"].ToString(),
															Global.MainFrame.LoginInfo.UserID));

				this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn할당취소.Text);
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void _flex작업대기_AfterRowChange(object sender, RangeEventArgs e)
		{
			DataTable dt = null;
			
			try
			{
				dt = this._biz.Search2(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
												      D.GetString(this._flex작업대기["NO_WO"]) });

				this._flex수주정보.Binding = dt;
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		protected override void InitPaint()
		{
			base.InitPaint();

			this.cbo부서.DataSource = DBHelper.GetDataTable(string.Format(@"SELECT MD.CD_DEPT AS CODE,
	   MD.NM_DEPT AS NAME,
	   MD.CD_CC
FROM MA_DEPT MD
WHERE MD.CD_COMPANY = '{0}'
AND MD.H_DEPT = '060500'
AND MD.CD_DEPT <> '060600'", Global.MainFrame.LoginInfo.CompanyCode));
			this.cbo부서.ValueMember = "CODE";
			this.cbo부서.DisplayMember = "NAME";

			this.splitContainer1.SplitterDistance = 504;
			this.splitContainer2.SplitterDistance = 1092;
		}

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarSearchButtonClicked(sender, e);

				this._flex작업대기.Binding = this._biz.Search1(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																			 ((DataRowView)this.cbo부서.SelectedItem).Row["CD_CC"].ToString(),
																			 this.txt작업지시번호.Text,
																		     (this.chk설비미할당.Checked == true ? "Y" : "N"),
																			 this.ctx설비.CodeValue });

				this._flex작업진행.Binding = this._biz.Search3(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																			  this.cbo부서.SelectedValue.ToString() });

				if (!this._flex작업대기.HasNormalRow && !this._flex작업진행.HasNormalRow)
				{
					Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarSaveButtonClicked(sender, e);

				if (!this.BeforeSave()) return;

				if (MsgAndSave(PageActionMode.Save))
					ShowMessage(PageResultMode.SaveGood);
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		protected override bool SaveData()
		{
			if (!base.SaveData() || !base.Verify()) return false;
			if (this._flex작업대기.IsDataChanged == false) return false;

			if (!_biz.Save(this._flex작업대기.GetChanges())) return false;

			this._flex작업대기.AcceptChanges();

			return true;
		}
	}
}

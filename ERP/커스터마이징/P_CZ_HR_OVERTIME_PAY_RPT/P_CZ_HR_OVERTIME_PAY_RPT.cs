using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Duzon.Common.Forms;
using Duzon.Common.BpControls;
using DzHelpFormLib;
using Duzon.Windows.Print;

using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.ERPU;
using Dintec;
using Duzon.Common.Util;

namespace cz
{
	public partial class P_CZ_HR_OVERTIME_PAY_RPT : PageBase
	{
		#region ===================================================================================================== Property

		public string CD_COMPANY { get; set; }

		#endregion

		#region ==================================================================================================== Constructor

		public P_CZ_HR_OVERTIME_PAY_RPT()
		{
			StartUp.Certify(this);
			InitializeComponent();
			CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
		}

		#endregion

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{
			InitControl();
			InitGrid();
			InitEvent();
		}

		private void InitControl()
		{
			txt포커스.Left = -500;

			flexH.DetailGrids = new FlexGrid[] { flexLO, flexLS };
		}

		private void InitGrid()
		{
			// ========== H
			flexH.BeginSetting(1, 1, false);

			flexH.SetCol("CHK"		, "S"				, 30	, true	, CheckTypeEnum.Y_N);
			flexH.SetCol("YM"		, "급여반영월"		, 80	, false	, typeof(string), FormatTpType.YEAR_MONTH);
			flexH.SetCol("NO_EMP"	, "사번"				, 60);
			flexH.SetCol("NM_EMP"	, "성명"				, 80);
			flexH.SetCol("YN_PAY"	, "상태"				, 70);

			flexH.Cols["NO_EMP"].TextAlign = TextAlignEnum.CenterCenter;
			flexH.Cols["NM_EMP"].TextAlign = TextAlignEnum.CenterCenter;
			flexH.Cols["YN_PAY"].TextAlign = TextAlignEnum.CenterCenter;
			flexH.SetDataMap("YN_PAY", MA.GetCode("CZ_MA00007"), "CODE", "NAME");

			flexH.SettingVersion = "16.02.18.01";
			flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

			// ========== L
			flexLO.BeginSetting(2, 1, false);

			flexLO.SetCol("YN_PAY"	, "S"				, 30	, true	, CheckTypeEnum.Y_N);
			flexLO.SetCol("YM"		, "급여반영월"		, false);
			flexLO.SetCol("NO_EMP"	, "사번"				, false);						
			flexLO.SetCol("DT_WORK"	, "일자"				, 80	, false	, typeof(string), FormatTpType.YEAR_MONTH_DAY);	
			flexLO.SetCol("CD_WEEK"	, "요일코드"			, false);
			flexLO.SetCol("NM_WEEK"	, "요일"				, 55);
			flexLO.SetCol("CD_WORK"	, "근무코드"			, false);
			flexLO.SetCol("NM_WORK"	, "근무"				, 45);
			flexLO.SetCol("CD_WCODE", "구분코드"			, false);
			flexLO.SetCol("NM_WCODE", "구분"				, 45);
			flexLO.SetCol("TM_START", "출근(숫자값)"		, false);
			flexLO.SetCol("TM_CLOSE", "퇴근(숫자값)"		, false);
			flexLO.SetCol("NM_START", "출근"				, 45);
			flexLO.SetCol("NM_CLOSE", "퇴근"				, 45);
			flexLO.SetCol("DC_RMK"	, "업무내용"			, 150);
			flexLO.SetCol("OT_W1"	, "07:00\n이전"		, 45);
			flexLO.SetCol("OT_W2"	, "20:00\n~21:00"	, 45);
			flexLO.SetCol("OT_W3"	, "21:00\n~21:30"	, 45);
			flexLO.SetCol("OT_W4"	, "21:30\n~22:00"	, 45);
			flexLO.SetCol("OT_W5"	, "22:00\n~22:30"	, 45);
			flexLO.SetCol("OT_W6"	, "22:30\n~23:00"	, 45);
			flexLO.SetCol("OT_W7"	, "23:00\n~23:30"	, 45);
			flexLO.SetCol("OT_W8"	, "23:30\n~24:00"	, 45);
			flexLO.SetCol("OT_W9"	, "24:00\n초과"		, 45);
			flexLO.SetCol("OT_H1"	, "1H\n이하"			, 45);
			flexLO.SetCol("OT_H2"	, "1H\n~1.5H"		, 45);
			flexLO.SetCol("OT_H3"	, "1.5H\n~2H"		, 45);
			flexLO.SetCol("OT_H4"	, "2H\n~2.5H"		, 45);
			flexLO.SetCol("OT_H5"	, "2.5H\n~3H"		, 45);
			flexLO.SetCol("OT_H6"	, "3H\n~3.5H"		, 45);
			flexLO.SetCol("OT_H7"	, "3.5H\n~4H"		, 45);
			flexLO.SetCol("OT_H8"	, "4H\n~4.5H"		, 45);
			flexLO.SetCol("OT_H9"	, "4.5H\n~5H"		, 45);	// ↓↓↓↓↓ 물류부 영역
			flexLO.SetCol("OT_H10"	, "5H\n~5.5H"		, 45);
			flexLO.SetCol("OT_H11"	, "5.5H\n~6H"		, 45);
			flexLO.SetCol("OT_H12"	, "6H\n~6.5H"		, 45);
			flexLO.SetCol("OT_H13"	, "6.5H\n~7H"		, 45);
			flexLO.SetCol("OT_H14"	, "7H\n~7.5H"		, 45);
			flexLO.SetCol("OT_H15"	, "7.5H\n초과"		, 45);

			int s, e;

			// 평일근무 헤더
			s = flexLO.Cols["OT_W1"].Index;
			e = flexLO.Cols["OT_W9"].Index;

			for (int i = s; i <= e; i++)
				flexLO[0, i] = "평일근무";

			// 휴일근무 헤더
			s = flexLO.Cols["OT_H1"].Index;
			e = flexLO.Cols["OT_H8"].Index;

			for (int i = s; i <= e; i++)
				flexLO[0, i] = "휴일근무";

			// 휴일근무 헤더(물류부 전용)
			s = flexLO.Cols["OT_H9"].Index;
			e = flexLO.Cols["OT_H15"].Index;

			for (int i = s; i <= e; i++)
				flexLO[0, i] = "휴일근무(물류부)";

			// 중앙정렬
			s = flexLO.Cols["DT_WORK"].Index;
			e = flexLO.Cols["OT_H15"].Index;

			for (int i = s; i <= e; i++) flexLO.Cols[i].TextAlign = TextAlignEnum.CenterCenter;
			flexLO.Cols["DC_RMK"].TextAlign = TextAlignEnum.LeftCenter;

			flexLO.KeyActionEnter = KeyActionEnum.MoveDown;
			flexLO.SettingVersion = "19.02.07.01";
			flexLO.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

			flexLO.Rows[0].Height = 26;
			flexLO.Rows[1].Height = 40;

			FlexUtil.AddEditStyle(flexLO, "DC_RMK");

			CellStyle style = flexLO.Styles.Add("HOLIDAY");
			style.ForeColor = Color.Red;

			// ================================================== L - Shipment
			flexLS.BeginSetting(1, 1, false);
			
			flexLS.SetCol("YN_PAY"		, "S"			, 30	, true, CheckTypeEnum.Y_N);
			flexLS.SetCol("YM"			, "급여반영월"	, false);
			flexLS.SetCol("NO_EMP"		, "사번"			, false);			
			flexLS.SetCol("DT_WORK"		, "일자"			, 80	, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			//flexLS.SetCol("CD_WEEK"		, "요일코드"		, false);
			//flexLS.SetCol("NM_WEEK"		, "요일"			, 55);
			//flexLS.SetCol("CD_WORK"		, "근무코드"		, false);
			//flexLS.SetCol("NM_WORK"		, "근무"			, 45);
			//flexLS.SetCol("CD_WCODE"	, "구분코드"		, false);
			//flexLS.SetCol("NM_WCODE"	, "구분"			, 45);
			flexLS.SetCol("CD_SHIPMENT"	, "선적구분"		, 250);
			flexLS.SetCol("DC_RMK"		, "선적내용"		, 250	, true);

			flexLS.SetDataMap("CD_SHIPMENT", Util.GetDB_CODE("CZ_HR00004"), "CODE", "NAME");		
				 
			flexLS.SettingVersion = "16.02.18.02";
			flexLS.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
		}

		private void InitEvent()
		{
			btn급여반영.Click += new EventHandler(btn급여반영_Click);

			flexH.AfterRowChange += new RangeEventHandler(flexH_AfterRowChange);
		}

		protected override void InitPaint()
		{
			txt포커스.Focus();
		}

		#endregion

		#region ==================================================================================================== 버튼 이벤트

		private void btn급여반영_Click(object sender, EventArgs e)
		{
			if (flexH.DataTable == null || flexLO.DataTable == null) return;

			DataTable dtH = flexH.GetCheckedRows("CHK");

			DataRow[] rowLO = flexLO.DataTable.Select("YN_PAY = 'Y'");
			DataRow[] rowLS = flexLS.DataTable.Select("YN_PAY = 'Y'");

			DataTable dtLO = (rowLO.Length > 0) ? rowLO.CopyToDataTable() : null;
			DataTable dtLS = (rowLS.Length > 0) ? rowLS.CopyToDataTable() : null;

			string xmlH = Util.GetTO_Xml(dtH);
			string xmlLO = Util.GetTO_Xml(dtLO);
			string xmlLS = Util.GetTO_Xml(dtLS);
			DBMgr.ExecuteNonQuery("SP_CZ_HR_OVERTIME_PAY_RPT_XML", new object[] { xmlH, xmlLO, xmlLS });

			flexH.AcceptChanges();
			flexLO.AcceptChanges();
			flexLS.AcceptChanges();

			Global.MainFrame.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
		}

		#endregion

		#region ==================================================================================================== 그리드 이벤트

		private void flexH_AfterRowChange(object sender, RangeEventArgs e)
		{
			flexLO.Redraw = false;
			flexLS.Redraw = false;
			string filter = "YM = '" + flexH["YM"] + "' AND NO_EMP = '" + flexH["NO_EMP"] + "'";

			if (flexH.DetailQueryNeed)
			{
				// 잔업수당
				DataTable dtLO = DBMgr.GetDataTable("SP_CZ_HR_OVERTIME_PAYL_RPT_SELECT", new object[] { CD_COMPANY, flexH["YM"], flexH["NO_EMP"] });
				flexLO.BindingAdd(dtLO, filter);

				// 선적수당
				DataTable dtLS = DBMgr.GetDataTable("SP_CZ_HR_OVERTIME_PAY_RPT_L_SELECT_SHIPMENT", new object[] { CD_COMPANY, flexH["YM"], flexH["NO_EMP"] });
				flexLS.BindingAdd(dtLS, filter);
			}
			else
			{
				flexLO.BindingAdd(null, filter);
				flexLS.BindingAdd(null, filter);
			}

			AddGridEffect();
			flexLO.Redraw = true;
			flexLS.Redraw = true;

			// 상태 결정
			if (flexH["YN_PAY"].ToString() == "Y")
			{
				flexLO.AllowEditing = false;
				flexLS.AllowEditing = false;
			}
			else
			{
				flexLO.AllowEditing = true;
				flexLS.AllowEditing = true;
			}
		}

		#endregion

		#region ==================================================================================================== Search

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_HR_OVERTIME_PAYH_RPT_SELECT", new object[] { CD_COMPANY });
			flexH.Binding = dt;
			if (!flexH.HasNormalRow) ShowMessage(공통메세지.조건에해당하는내용이없습니다);
		}

		#endregion

		#region ==================================================================================================== Add

		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
			if (flexH[flexH.Rows.Count - 1, "YM"].ToString() == Util.GetToday().Substring(0, 6))
			{
				ShowMessage("이번달은 이미 작성하였습니다!");
				return;
			}

			flexH.Rows.Add();
			flexH.Row = flexH.Rows.Count - 1;
			flexH.AddFinished();
		}

		#endregion

		#region ==================================================================================================== Save

		protected override bool BeforeSave()
		{
			if (!base.BeforeSave()) return false;

			return true;
		}

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			if (!BeforeSave()) return;
			if (!MsgAndSave(PageActionMode.Save)) return;

			ShowMessage(PageResultMode.SaveGood);
		}

		protected override bool SaveData()
		{
			if (!base.SaveData() || !base.Verify()) return false;

			return true;
		}

		#endregion

		#region ==================================================================================================== Delete

		public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{
			if (!base.BeforeDelete() || !flexH.HasNormalRow) return;
			flexH.Rows.Remove(flexH.Row);
		}

		#endregion

		#region ==================================================================================================== 기타

		private void AddGridEffect()
		{
			for (int i = 2; i < flexLO.Rows.Count; i++)
			{
				if (flexLO[i, "CD_WORK"].ToString() == "H")
				{
					flexLO.SetCellStyle(i, flexLO.Cols["DT_WORK"].Index, "HOLIDAY");
					flexLO.SetCellStyle(i, flexLO.Cols["NM_WEEK"].Index, "HOLIDAY");
					flexLO.SetCellStyle(i, flexLO.Cols["NM_WORK"].Index, "HOLIDAY");
				}
			}
		}

		#endregion
	}
}

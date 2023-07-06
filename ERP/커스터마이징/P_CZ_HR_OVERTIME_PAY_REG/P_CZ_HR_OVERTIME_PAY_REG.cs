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
	public partial class P_CZ_HR_OVERTIME_PAY_REG : PageBase
	{
		#region ===================================================================================================== Property

		public string CD_COMPANY { get; set; }

		public string NO_EMP { get; set; }

		public string NM_EMP { get; set; }

		public string YM
		{
			get
			{
				return flexH.HasNormalRow ? flexH["YM"].ToString() : "";
			}
		}

		#endregion

		#region ==================================================================================================== Constructor

		public P_CZ_HR_OVERTIME_PAY_REG()
		{
			StartUp.Certify(this);
			CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
			NO_EMP = Global.MainFrame.LoginInfo.UserID;
			NM_EMP = Global.MainFrame.LoginInfo.UserName;
			InitializeComponent();
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
			//txt포커스.Left = -500;

			dtp작성From.Text = Util.GetToday(-30);
			dtp작성To.Text = Util.GetToday();

			txt사번.Text = NO_EMP;
			txt성명.Text = NM_EMP;
			txt부서.Text = Global.MainFrame.LoginInfo.DeptName;

			Util.SetCON_ReadOnly(pnl사번, true);
			Util.SetCON_ReadOnly(pnl성명, true);
			Util.SetCON_ReadOnly(pnl부서, true);
			Util.SetDB_CODE(cbo선적지역, "CZ_HR00004", true);

			MainGrids = new FlexGrid[] { flexH, flexLO, flexLS };
			flexH.DetailGrids = new FlexGrid[] { flexLO, flexLS };
		}

		private void InitGrid()
		{
			// ================================================== H
			flexH.BeginSetting(1, 1, false);
							
			flexH.SetCol("YM"		, "급여반영월"	, 80	, false, typeof(string), FormatTpType.YEAR_MONTH);
			flexH.SetCol("NO_EMP"	, "사번"			, false);
			flexH.SetCol("NM_TITLE"	, "제목"			, 250);
			flexH.SetCol("DT_REG"	, "작성일"		, 90	, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			flexH.SetCol("DT_START"	, "잔업시작일"	, 90	, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			flexH.SetCol("DT_CLOSE"	, "잔업종료일"	, 90	, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			flexH.SetCol("ST_STAT"	, "결재"			, 70);
			flexH.SetCol("NO_DOCU"	, "문서번호"			, false);

			flexH.Cols["ST_STAT"].TextAlign = TextAlignEnum.CenterCenter;
			flexH.SetDataMap("ST_STAT", Util.GetDB_CODE("FI_J000031"), "CODE", "NAME");
			flexH.SetOneGridBinding(new object[] { }, oneH, oneH2);
			flexH.VerifyNotNull = new string[] { "NM_TITLE" };

			flexH.SettingVersion = "16.01.14.01";
			flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

			// ================================================== L - Overtime
			flexLO.BeginSetting(2, 1, false);
			
			flexLO.SetCol("YN_SEL"	, "S"				, 30	, true, CheckTypeEnum.Y_N);
			flexLO.SetCol("YM"		, "급여반영월"		, false);
			flexLO.SetCol("NO_EMP"	, "사번"				, false);			
			flexLO.SetCol("DT_WORK"	, "일자"				, 80	, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);	
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
			flexLO.SetCol("DC_RMK"	, "업무내용"			, 150	, true);
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
			flexLO.SetCol("OT_H8"	, "4H\n~4.5H"		, 45);// ↓↓↓↓↓ 물류부 영역
			flexLO.SetCol("OT_H9"	, "4.5H\n~5H"		, 45);	
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
			s = flexLO.Cols["OT_H8"].Index;
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
			
			flexLS.SetCol("YN_SEL"		, "S"			, 30	, true, CheckTypeEnum.Y_N);
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
			flexLS.SetOneGridBinding(new object[] { }, oneL);
			flexLS.VerifyNotNull = new string[] { "DT_WORK", "CD_SHIPMENT" };
				 
			flexLS.SettingVersion = "16.02.16.05";
			flexLS.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

			FlexUtil.AddEditStyle(flexLS, "DC_RMK");
		}

		private void InitEvent()
		{
			btn가져오기.Click += new EventHandler(btn가져오기_Click);
			btn전자결재.Click += new EventHandler(btn전자결재_Click);
			btn추가.Click += new EventHandler(btn추가_Click);
			btn삭제.Click += new EventHandler(btn삭제_Click);

			flexH.AfterRowChange += new RangeEventHandler(flexH_AfterRowChange);
		}

		protected override void InitPaint()
		{
			//txt포커스.Focus();
		}
		
		#endregion

		#region ==================================================================================================== 버튼 이벤트

		private void btn가져오기_Click(object sender, EventArgs e)
		{
			if (Util.GetTO_Date(dtp잔업시작일.Text) < Util.GetTO_Date("20160101"))
			{
				ShowMessage("2015년도 잔업은 작성할 수 없습니다.");
				return;
			}

			flexLO.Redraw = false;

			// 기존 DATA 삭제
			for (int i = flexLO.Rows.Count - 1; i >= flexLO.Rows.Fixed; i--) flexLO.Rows.Remove(i);

			// 신규 DATA 바인딩
			DataTable dt = DBMgr.GetDataTable("SP_CZ_HR_OVERTIME_PAYL_REG_SELECT_NEW", new object[] { CD_COMPANY, NO_EMP, YM, flexH["DT_START"], flexH["DT_CLOSE"] });
			flexLO.BindingAdd(dt, "YM = '" + YM + "'", false);
			AddGridEffect();

			flexLO.Redraw = true;
		}

		private void btn전자결재_Click(object sender, EventArgs e)
		{
			// 라인 테이블 체크
			DataTable dtL = DBMgr.GetDataTable("PS_CZ_HR_OVERTIME_PAY_REG_L", CD_COMPANY, NO_EMP, YM);
			if (dtL.Rows.Count == 0) { ShowMessage("조회 후 재시도 바랍니다."); return; }

			// 결재 상태 체크
			string query = @"
SELECT
	A.NO_DOCU, B.ST_STAT
FROM	  CZ_HR_OVERTIME_PAYH	AS A
LEFT JOIN FI_GWDOCU				AS B ON A.NO_DOCU = B.NO_DOCU
WHERE 1 = 1
	AND A.CD_COMPANY = '" + CD_COMPANY + @"'
	AND A.YM = '" + YM + @"'
	AND A.NO_EMP = '" + NO_EMP + "'";

			DataTable dt = DBMgr.GetDataTable(query);
			string NO_DOCU = dt.Rows[0]["NO_DOCU"].ToString();
			string ST_STAT = dt.Rows[0]["ST_STAT"].ToString();

			if (ST_STAT == "0") { ShowMessage("결재 진행중인 문서입니다!"); return; }
			if (ST_STAT == "1") { ShowMessage("결재 완료된 문서입니다!"); return; }

			// html 만들기 - 잔업수당
			string html = "";

			if (flexLO.HasNormalRow)
			{
				html = @"
<div class='overtime'>
<div class='subtitle'>
  ※ 추가연장 내역
</div>
<table>
  <tr>
    <th>일 자</th>
    <th>시 간</th>
    <th>업 무 내 용</th>
	<th>추가근무신청</th>
  </tr>";

				for (int i = flexLO.Rows.Fixed; i < flexLO.Rows.Count; i++)
				{
					if (flexLO[i, "YN_SEL"].ToString() == "Y")
					{
						string dayString = Util.GetTo_DateStringS(flexLO[i, "DT_WORK"]) + "(" + flexLO[i, "NM_WEEK"].Left(1) + ")";
						string holiday = (flexLO[i, "CD_WORK"].ToString() == "H") ? " holiday" : "";

						// 추가연장신청서에 있는 정보를 DataTable에서 가져옴 (임시조치)
						DataRow[] row = dtL.Select("DT_WORK = '" + flexLO[i, "DT_WORK"] + "'");
						string gwAppTime = "";
						if (row[0]["DT_GW_START"].ToString() != "")
							gwAppTime = row[0]["DT_GW_START"] + " ~ " + row[0]["DT_GW_END"];

						html += @"
  <tr>
    <td class='col1" + holiday + "'>" + dayString + @"</td>
    <td class='col2'>" + flexLO[i, "NM_START"] + " ~ " + flexLO[i, "NM_CLOSE"] + @"</td>
    <td class='col3'>" + flexLO[i, "DC_RMK"] + @"</td>
	<td class='col4'>" + gwAppTime + @"</td>
  </tr>";
					}
				}


				html += @"
</table>
</div>";
			}

			// html 만들기 - 선적수당
			if (flexLS.HasNormalRow)
			{
				DataTable dtCD_SHIPMENT = Util.GetDB_CODE("CZ_HR00004");

				html += @"
<br />
<div class='shipment'>
<div class='subtitle'>
  ※ 선적 내역
</div>
<table>
  <tr>
    <th>일 자</th>
    <th>선 적 구 분</th>
    <th>선 적 내 용</th>
  </tr>";

				for (int i = flexLS.Rows.Fixed; i < flexLS.Rows.Count; i++)
				{
					string dayString = Util.GetTo_DateStringS(flexLS[i, "DT_WORK"]);
					//string holiday = (flexLS[i, "CD_WORK"].ToString() == "H") ? " holiday" : "";
					string NM_SHIPMENT = dtCD_SHIPMENT.Select("CODE = '" + flexLS[i, "CD_SHIPMENT"] + "'")[0]["NAME"].ToString();

					html += @"
  <tr>
    <td class='col1'>" + dayString + @"</td>
    <td class='col2'>" + NM_SHIPMENT + @"</td>
    <td class='col3'>" + flexLS[i, "DC_RMK"] + @"</td>
  </tr>";
				}

				html += @"
</table>
</div>";
			}

			// 전자결재 팝업	
			// GW_KEY UPDATE 쿼리
			query = @"
UPDATE CZ_HR_OVERTIME_PAYH SET
	NO_DOCU = '@NO_DOCU'
WHERE 1 = 1
	AND CD_COMPANY = '" + CD_COMPANY + @"'
	AND YM = '" + YM + @"'
	AND NO_EMP = '" + NO_EMP + "'";

			GroupWare.Save(txt제목.Text, html, NO_DOCU, 2002, query, true);
		}

		private void btn추가_Click(object sender, EventArgs e)
		{
			flexLS.Rows.Add();
			flexLS.Row = flexLS.Rows.Count - 1;
			flexLS["YM"] = YM;
			flexLS["NO_EMP"] = NO_EMP;
			flexLS.AddFinished();
		}

		private void btn삭제_Click(object sender, EventArgs e)
		{
			flexLS.Rows.Remove(flexLS.Row);
		}

		#endregion

		#region ==================================================================================================== 그리드 이벤트

		private void flexH_AfterRowChange(object sender, RangeEventArgs e)
		{
			flexLO.Redraw = false;
			flexLS.Redraw = false;

			if (flexH.DetailQueryNeed)
			{
				// 잔업수당
				if (flexH["NO_DOCU"].ToString() == "NEW")	// 추가모드
				{
					DataTable dt = DBMgr.GetDataTable("SP_CZ_HR_OVERTIME_PAYL_REG_SELECT_NEW", new object[] { CD_COMPANY, NO_EMP, YM, flexH["DT_START"], flexH["DT_CLOSE"] });
					flexLO.BindingAdd(dt, "YM = '" + YM + "'", false);
				}
				else
				{
					DataTable dt = DBMgr.GetDataTable("SP_CZ_HR_OVERTIME_PAYL_REG_SELECT", new object[] { CD_COMPANY, NO_EMP, YM });
					flexLO.BindingAdd(dt, "YM = '" + YM + "'");
				}

				// 선적수당
				DataTable dtLS = DBMgr.GetDataTable("SP_CZ_HR_OVERTIME_PAY_REG_L_SELECT_SHIPMENT", new object[] { CD_COMPANY, NO_EMP, YM });
				flexLS.BindingAdd(dtLS, "YM = '" + YM + "'");
			}
			else
			{
				flexLO.BindingAdd(null, "YM = '" + YM + "'");
				flexLS.BindingAdd(null, "YM = '" + YM + "'");
			}

			AddGridEffect();
			flexLO.Redraw = true;
			flexLS.Redraw = true;

			// 적용년월 display
			lbl적용년월.Text = YM.Substring(0, 4) + "년 " + YM.Substring(4, 2) + "월";

			// 상태 결정
			if (flexH["ST_STAT"].ToString() == "0" || flexH["ST_STAT"].ToString() == "1" || YM != Util.GetToday().Substring(0, 6))
			{
				btn가져오기.Enabled = false;
				btn전자결재.Enabled = false;
				btn추가.Enabled = false;
				btn삭제.Enabled = false;
				oneH2.Enabled = false;
				flexLO.AllowEditing = false;
			}
			else
			{
				btn가져오기.Enabled = true;
				btn전자결재.Enabled = true;
				btn추가.Enabled = true;
				btn삭제.Enabled = true;
				oneH2.Enabled = true;
				flexLO.AllowEditing = true;
			}
		}

		#endregion

		#region ==================================================================================================== Search

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			Util.Clear(flexH);
			Util.Clear(flexLO);

			DBMgr dbm = new DBMgr(DBConn.iU);
			dbm.Procedure = "SP_CZ_HR_OVERTIME_PAYH_REG_SELECT";
			dbm.AddParameter("CD_COMPANY"	, CD_COMPANY);
			dbm.AddParameter("NO_EMP"		, NO_EMP);
			dbm.AddParameter("DT_FROM"		, dtp작성From.Text);
			dbm.AddParameter("DT_TO"		, dtp작성To.Text);
			dbm.AddParameter("NM_TITLE"		, txt제목S.Text);
			DataTable dt = dbm.GetDataTable();

			flexH.Binding = dt;
			flexH.Row = flexH.Rows.Count - 1;
			if (!flexH.HasNormalRow) ShowMessage(공통메세지.조건에해당하는내용이없습니다);
		}

		#endregion

		#region ==================================================================================================== Add

		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
			string YM = Util.GetToday().Substring(0, 6);
			string query = "SELECT * FROM CZ_HR_OVERTIME_PAYH WHERE CD_COMPANY = '" + CD_COMPANY + "' AND YM = '" + YM + "' AND NO_EMP = '" + NO_EMP + "'";
			DataTable dtDB = DBMgr.GetDataTable(query);
			DataTable dtTB = flexH.DataTable;
			if (dtDB.Rows.Count > 0 || dtTB.Select("YM = '" + YM + "'").Length > 0) { ShowMessage("해당월은 이미 작성하였습니다."); return; }

			flexH.Rows.Add();
			flexH.Row = flexH.Rows.Count - 1;
			flexH["YM"] = YM;
			flexH["NO_EMP"] = NO_EMP;
			flexH["NM_TITLE"] = NM_EMP + "_" + YM.Substring(0, 4) + "년" + YM.Substring(4, 2) + "월 추가연장수당 신청서";
			//flexH["DT_START"] = Util.GetToday(-31);
			flexH["DT_START"] = "20160101";
			flexH["DT_CLOSE"] = Util.GetToday(-1);
			flexH["NO_DOCU"] = "NEW";
			flexH.AddFinished();
		}

		#endregion

		#region ==================================================================================================== Save

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			if (!BeforeSave()) return;
			if (!MsgAndSave(PageActionMode.Save)) return;

			ShowMessage(PageResultMode.SaveGood);
		}

		protected override bool BeforeSave()
		{
			if (!base.BeforeSave()) return false;

			return true;
		}

		protected override bool SaveData()
		{
			if (!base.SaveData() || !base.Verify()) return false;

			DataTable dtH = flexH.GetChanges();
			DataTable dtLO = flexLO.GetChanges();
			DataTable dtLS = flexLS.GetChanges();

			string xmlH = Util.GetTO_Xml(dtH);
			string xmlLO = Util.GetTO_Xml(dtLO);
			string xmlLS = Util.GetTO_Xml(dtLS);
			DBMgr.ExecuteNonQuery("SP_CZ_HR_OVERTIME_PAY_REG_XML", new object[] { xmlH, xmlLO, xmlLS });

			flexH.AcceptChanges();
			flexLO.AcceptChanges();

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

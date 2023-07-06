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
using System.Data.SqlClient;

namespace cz
{
	public partial class P_CZ_HR_CAR_PAY_REG : PageBase
	{
		bool useEvent = true;

		#region ===================================================================================================== Property

		public string CD_COMPANY { get; set; }

		public string NO_EMP { get; set; }

		public string NM_EMP { get; set; }

		public string NO_DOCU
		{
			get
			{
				return flexH.HasNormalRow ? flexH["NO_DOCU"].ToString() : "";
			}
		}

		public string YM
		{
			get
			{
				return flexH.HasNormalRow ? flexH["YM"].ToString() : "";
			}
		}

		#endregion

		#region ==================================================================================================== Constructor

		public P_CZ_HR_CAR_PAY_REG()
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
			txt포커스.Left = -500;

			dtp작성From.Text = Util.GetToday(-30);
			dtp작성To.Text = Util.GetToday();

			txt사번.Text = NO_EMP;
			txt성명.Text = NM_EMP;
			txt부서.Text = Global.MainFrame.LoginInfo.DeptName;

			Util.SetCON_ReadOnly(pnl사번, true);
			Util.SetCON_ReadOnly(pnl성명, true);
			Util.SetCON_ReadOnly(pnl부서, true);

			MainGrids = new FlexGrid[] { flexH, flexL };
			flexH.DetailGrids = new FlexGrid[] { flexL };
		}

		private void InitGrid()
		{
			// ================================================== H
			flexH.BeginSetting(1, 1, false);

			flexH.SetCol("YM"		, "급여반영월"	, 80	, false, typeof(string), FormatTpType.YEAR_MONTH);
			flexH.SetCol("NO_EMP"	, "사번"			, false);
			flexH.SetCol("NM_TITLE"	, "제목"			, 250);
			flexH.SetCol("DT_REG"	, "작성일"		, 90	, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			flexH.SetCol("ST_STAT"	, "결재"			, 70);
			flexH.SetCol("NO_DOCU"	, "문서번호"		, false);

			flexH.Cols["ST_STAT"].TextAlign = TextAlignEnum.CenterCenter;
			flexH.SetDataMap("ST_STAT", Util.GetDB_CODE("FI_J000031"), "CODE", "NAME");
			flexH.SetOneGridBinding(new object[] { }, oneH);
			flexH.VerifyNotNull = new string[] { "NM_TITLE" };

			flexH.SettingVersion = "15.11.19.03";
			flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

			// ================================================== L
			flexL.BeginSetting(1, 1, false);
	
			flexL.SetCol("YM"		, "급여반영월"	, false);
			flexL.SetCol("NO_EMP"	, "사번"			, false);
			flexL.SetCol("DT_WORK"	, "운행일자"		, 80	, false, typeof(string)	, FormatTpType.YEAR_MONTH_DAY);
			flexL.SetCol("NM_TRIP"	, "출장지"		, 120);
			flexL.SetCol("DC_TRIP"	, "출장목적"		, 180);			
			flexL.SetCol("DISTANCE"	, "거리"			, 55	, false, typeof(decimal), FormatTpType.MONEY);
			flexL.SetCol("UM_KM"	, "단가"			, 55	, false, typeof(decimal), FormatTpType.MONEY);
			flexL.SetCol("AM_OIL"	, "유류비"		, 70	, false, typeof(decimal), FormatTpType.MONEY);
			flexL.SetCol("AM_PARK"	, "주차비"		, 70	, false, typeof(decimal), FormatTpType.MONEY);
			flexL.SetCol("AM_TOLL"	, "통행료"		, 70	, false, typeof(decimal), FormatTpType.MONEY);
			flexL.SetCol("AM"		, "합계"			, false);

			flexL.SetOneGridBinding(new object[] { }, oneL);
			flexL.VerifyNotNull = new string[] { "DT_WORK", "NM_TRIP" };
			
			flexL.SettingVersion = "15.11.19.11";
			flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			flexL.SetExceptSumCol("UM_KM");
		}

		private void InitEvent()
		{
			btn단가등록.Click += new EventHandler(btn단가등록_Click);
			btn전자결재.Click += new EventHandler(btn전자결재_Click);
			btn추가.Click += new EventHandler(btn추가_Click);
			btn삭제.Click += new EventHandler(btn삭제_Click);

			spcM.SplitterMoved += new SplitterEventHandler(spcM_SplitterMoved);
			dtp운행일자.DateChanged += new EventHandler(dtp운행일자_DateChanged);
			cur거리.Validated += new EventHandler(cur거리_Validated);
			cur주차비.Validated += new EventHandler(cur주차비_Validated);
			cur통행료.Validated += new EventHandler(cur통행료_Validated);

			flexH.AfterRowChange += new RangeEventHandler(flexH_AfterRowChange);
			flexL.AfterRowChange += new RangeEventHandler(flexL_AfterRowChange);
		}
	
		protected override void InitPaint()
		{
			txt포커스.Focus();
			spcM.SplitterDistance = 550;
		}

		#endregion

		#region ==================================================================================================== Event

		private void spcM_SplitterMoved(object sender, SplitterEventArgs e)
		{
			btn단가등록.Left = spcM.SplitterDistance + 614;
			btn전자결재.Left = spcM.SplitterDistance + 690;
		}

		private void dtp운행일자_DateChanged(object sender, EventArgs e)
		{
			if (!useEvent) return;
			flexL["DT_WORK"] = dtp운행일자.Text;

			string YM = dtp운행일자.Text.Substring(0, 6);
			string query = "SELECT * FROM CZ_HR_CAR_OIL_CODE WHERE CD_COMPANY = '" + CD_COMPANY + "' AND YM = '" + YM + "'";
			DataTable dt = DBMgr.GetDataTable(query);

			if (dt.Rows.Count == 1) flexL["UM_KM"] = dt.Rows[0]["UM_KM"];
			else flexL["UM_KM"] = 0;

			CalcAM_OIL(flexL.Row);
		}
		private void cur거리_Validated(object sender, EventArgs e)
		{
			CalcAM_OIL(flexL.Row);
		}
		private void cur주차비_Validated(object sender, EventArgs e)
		{
			CalcAM(flexL.Row);
		}
		private void cur통행료_Validated(object sender, EventArgs e)
		{
			CalcAM(flexL.Row);
		}

		#endregion

		#region ==================================================================================================== 버튼 이벤트

		private void btn단가등록_Click(object sender, EventArgs e)
		{
			H_CZ_OIL_PRICE help = new H_CZ_OIL_PRICE();
			help.ShowDialog();
		}

		private void btn전자결재_Click(object sender, EventArgs e)
		{
			// 결재 상태 체크
			string query = @"
SELECT
	A.NO_DOCU, B.ST_STAT
FROM	  CZ_HR_CAR_PAYH	AS A
LEFT JOIN FI_GWDOCU			AS B ON A.NO_DOCU = B.NO_DOCU
WHERE 1 = 1
	AND A.CD_COMPANY = '" + CD_COMPANY + @"'
	AND A.YM = '" + YM + @"'
	AND A.NO_EMP = '" + NO_EMP + "'";

			DataTable dt = DBMgr.GetDataTable(query);
			string NO_DOCU = dt.Rows[0]["NO_DOCU"].ToString();
			string ST_STAT = dt.Rows[0]["ST_STAT"].ToString();

			if (ST_STAT == "0") { ShowMessage("결재 진행중인 문서입니다!"); return; }
			if (ST_STAT == "1") { ShowMessage("결재 완료된 문서입니다!"); return; }

			// html 만들기
			string html = @"
<div class='header'>
  ※ 차량 운행 내역
</div>
<table>
  <tr>
    <th>순번</th>
    <th>운행일자</th>
    <th>출장지</th>
    <th>출장목적</th>
    <th>거리(KM)</th>
    <th>주차비</th>
    <th>통행료</th>
  </tr>";

			int idx = 1;
			for (int i = flexL.Rows.Fixed; i < flexL.Rows.Count; i++)
			{
				html += @"
  <tr>
    <td class='col1'>" + idx++ + @"</td>
    <td class='col2'>" + Util.GetTo_DateStringS(flexL[i, "DT_WORK"]) + @"</td>
    <td class='col3'>" + flexL[i, "NM_TRIP"] + @"</td>
    <td class='col4'>" + flexL[i, "DC_TRIP"] + @"</td>
    <td class='col5'>" + string.Format("{0:#,###}", flexL[i, "DISTANCE"]) + @"</td>
	<td class='col6'>" + string.Format("{0:#,###}", flexL[i, "AM_PARK"]) + @"</td>
    <td class='col7'>" + string.Format("{0:#,###}", flexL[i, "AM_TOLL"]) + @"</td>
  </tr>";
			}

			DataTable dtL = flexL.DataTable;
			html += @"
   <tr>
    <th colspan='4'>운행거리 합계</th>
	<td colspan='3' class='sum'>" + string.Format("{0:#,###}", dtL.Compute("SUM(DISTANCE)", "YM = '" + YM + "'")) +@" KM</td>
  </tr>
  <tr>
    <th colspan='4'>유류비 합계</th>
	<td colspan='3' class='sum'>" + string.Format("{0:#,###}", dtL.Compute("SUM(AM_OIL)", "YM = '" + YM + "'")) + @" 원</td>
  </tr>
  <tr>
    <th colspan='4'>주차비 합계</th>
	<td colspan='3' class='sum'>" + string.Format("{0:#,###}", dtL.Compute("SUM(AM_PARK)", "YM = '" + YM + "'")) + @" 원</td>
  </tr>
  <tr>
    <th colspan='4'>통행료 합계</th>
	<td colspan='3' class='sum'>" + string.Format("{0:#,###}", dtL.Compute("SUM(AM_TOLL)", "YM = '" + YM + "'")) + @" 원</td>
  </tr>
  <tr>
    <th colspan='4'>당월 지급액</th>
	<td colspan='3' class='sum fin'>" + string.Format("{0:#,###}", dtL.Compute("SUM(AM)", "YM = '" + YM + "'")) + @" 원</td>
  </tr>
  <tr>
    <td colspan='7' class='rmk'>
      1) 회사업무와 관련된 공식, 비공식 출장운행<br />
      2) 공휴일 출퇴근 운행<br />
      3) 차량구간거리계에 의한 정확한 거리측정 기록
    </td>
  </tr>
</table>";

			// html 업데이트 및 전자결재 팝업			
			query = @"
UPDATE CZ_HR_CAR_PAYH SET
	NO_DOCU = '@NO_DOCU'
WHERE 1 = 1
	AND CD_COMPANY = '" + CD_COMPANY + @"'
	AND YM = '" + YM + @"'
	AND NO_EMP = '" + NO_EMP + "'";

			GroupWare.Save(txt제목.Text, html, NO_DOCU, 2003, query, true);
		}

		private void btn추가_Click(object sender, EventArgs e)
		{			
			flexL.Rows.Add();
			flexL.Row = flexL.Rows.Count - 1;
			flexL["YM"] = YM;
			flexL["NO_EMP"] = NO_EMP;
			flexL.AddFinished();
		}

		private void btn삭제_Click(object sender, EventArgs e)
		{
			flexL.Rows.Remove(flexL.Row);
		}

		#endregion

		#region ==================================================================================================== 그리드 이벤트

		private void flexH_AfterRowChange(object sender, RangeEventArgs e)
		{
			DataTable dtL = null;
			if (flexH.DetailQueryNeed) dtL = DBMgr.GetDataTable("SP_CZ_HR_CAR_PAYL_REG_SELECT", new object[] { CD_COMPANY, YM, NO_EMP });
			flexL.BindingAdd(dtL, "YM = '" + YM + "'");

			// 상태 결정
			if (Util.GetTO_String(flexH["ST_STAT"]) == "0" || Util.GetTO_String(flexH["ST_STAT"]) == "1")
			{
				btn전자결재.Enabled = false;
				btn추가.Enabled = false;
				btn삭제.Enabled = false;
				Util.SetCON_ReadOnly(pnl제목, true);
				Util.SetCON_ReadOnly(pnl운행일자, true);
				Util.SetCON_ReadOnly(pnl출장지, true);
				Util.SetCON_ReadOnly(pnl운행거리, true);
				Util.SetCON_ReadOnly(pnl출장목적, true);
				Util.SetCON_ReadOnly(pnl주차비, true);
				Util.SetCON_ReadOnly(pnl통행료, true);
				flexL.AllowEditing = false;
			}
			else
			{
				btn전자결재.Enabled = true;
				btn추가.Enabled = true;
				btn삭제.Enabled = true;
				Util.SetCON_ReadOnly(pnl제목, false);
				Util.SetCON_ReadOnly(pnl운행일자, false);
				Util.SetCON_ReadOnly(pnl출장지, false);
				Util.SetCON_ReadOnly(pnl운행거리, false);
				Util.SetCON_ReadOnly(pnl출장목적, false);
				Util.SetCON_ReadOnly(pnl주차비, false);
				Util.SetCON_ReadOnly(pnl통행료, false);
				flexL.AllowEditing = true;
			}

			// 합계금액 표시
			CalcTotal();
		}

		protected void flexL_AfterRowChange(object sender, RangeEventArgs e)
		{
			useEvent = false;
			dtp운행일자.Text = flexL["DT_WORK"].ToString();
			useEvent = true;
		}

		#endregion

		#region ==================================================================================================== Search

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			DBMgr dbm = new DBMgr(DBConn.iU);
			dbm.Procedure = "SP_CZ_HR_CAR_PAYH_REG_SELECT";
			dbm.AddParameter("CD_COMPANY"	, CD_COMPANY);
			dbm.AddParameter("NO_EMP"		, NO_EMP);
			dbm.AddParameter("DT_FROM"		, dtp작성From.Text);
			dbm.AddParameter("DT_TO"		, dtp작성To.Text);
			dbm.AddParameter("NM_TITLE"		, txt제목S.Text);
			DataTable dt = dbm.GetDataTable();

			flexH.Binding = dt;
			if (!flexH.HasNormalRow) ShowMessage(공통메세지.조건에해당하는내용이없습니다);
		}

		#endregion

		#region ==================================================================================================== Add

		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
			string YM = Util.GetToday().Substring(0, 6);
			string query = "SELECT * FROM CZ_HR_CAR_PAYH WHERE CD_COMPANY = '" + CD_COMPANY + "' AND YM = '" + YM + "' AND NO_EMP = '" + NO_EMP + "'";
			DataTable dtDB = DBMgr.GetDataTable(query);
			DataTable dtTB = flexH.DataTable;
			if (dtDB.Rows.Count > 0 || dtTB.Select("YM = '" + YM + "'").Length > 0) { ShowMessage("해당월은 이미 작성하였습니다."); return; }

			flexH.Rows.Add();
			flexH.Row = flexH.Rows.Count - 1;
			flexH["YM"] = YM;
			flexH["NO_EMP"] = NO_EMP;
			flexH["NM_TITLE"] = NM_EMP + "_" + YM.Substring(0, 4) + "년" + YM.Substring(4, 2) + "월 차량운행기록부";
			flexH.AddFinished();

			btn추가_Click(null, null);
		}

		#endregion

		#region ==================================================================================================== Save

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			if (!BeforeSave()) return;
			if (!MsgAndSave(PageActionMode.Save)) return;

			ShowMessage(PageResultMode.SaveGood);
		}

		protected override bool SaveData()
		{
			if (!base.SaveData() || !base.Verify()) return false;

			DataTable dtH = flexH.GetChanges();
			DataTable dtL = flexL.GetChanges();

			string xmlH = Util.GetTO_Xml(dtH);
			string xmlL = Util.GetTO_Xml(dtL);
			DBMgr.ExecuteNonQuery("SP_CZ_HR_CAR_PAY_REG_XML", new object[] { xmlH, xmlL });

			flexH.AcceptChanges();
			flexL.AcceptChanges();
			
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

		#region ==================================================================================================== 계산식

		private void CalcAM_OIL(int row)
		{
			decimal DISTANCE = Util.GetTO_Decimal(flexL[row, "DISTANCE"]);
			decimal UM_KM = Util.GetTO_Decimal(flexL[row, "UM_KM"]);

			flexL[row, "AM_OIL"] = DISTANCE * UM_KM;
			CalcAM(flexL.Row);
		}

		private void CalcAM(int row)
		{
			decimal AM_OIL = Util.GetTO_Decimal(flexL[row, "AM_OIL"]);
			decimal AM_PARK = Util.GetTO_Decimal(flexL[row, "AM_PARK"]);
			decimal AM_TOLL = Util.GetTO_Decimal(flexL[row, "AM_TOLL"]);

			flexL[row, "AM"] = AM_PARK + AM_TOLL + AM_OIL;
			CalcTotal();
		}

		protected void CalcTotal()
		{
			DataTable dt = flexL.DataTable;

			decimal AM_OIL = Util.GetTO_Decimal(dt.Compute("SUM(AM_OIL)", "YM = '" + YM + "'"));
			decimal AM_PARK = Util.GetTO_Decimal(dt.Compute("SUM(AM_PARK)", "YM = '" + YM + "'"));
			decimal AM_TOLL = Util.GetTO_Decimal(dt.Compute("SUM(AM_TOLL)", "YM = '" + YM + "'"));
			decimal AM = Util.GetTO_Decimal(dt.Compute("SUM(AM)", "YM = '" + YM + "'"));

			lbl유류비.Text = string.Format("{0:#,##0}", AM_OIL);
			lbl주차비.Text = string.Format("{0:#,##0}", AM_PARK);
			lbl통행료.Text = string.Format("{0:#,##0}", AM_TOLL);
			lbl합계.Text = string.Format("{0:#,##0}", AM);
		}

		#endregion
	}
}

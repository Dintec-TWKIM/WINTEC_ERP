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
	public partial class P_CZ_HR_WAPPLY_REG : PageBase
	{
		bool useEvent = true;

		#region ===================================================================================================== Property

		public string CD_COMPANY { get; set; }

		public string CD_BIZAREA { get; set; }

		public string NO_EMP { get; set; }

		public string NM_EMP { get; set; }

		public string NO_DOCU
		{
			get
			{
				return flexH.HasNormalRow ? flexH["NO_DOCU"].ToString() : "";
			}
		}

		#endregion

		#region ==================================================================================================== Constructor

		public P_CZ_HR_WAPPLY_REG()
		{
			StartUp.Certify(this);
			CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
			CD_BIZAREA = Global.MainFrame.LoginInfo.BizAreaCode;
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

			Util.SetDB_CODE(cbo구분, "CD_WCODE", true);

			MainGrids = new FlexGrid[] { flexH, flexL };
			flexH.DetailGrids = new FlexGrid[] { flexL };
		}

		private void InitGrid()
		{
			// ================================================== H
			flexH.BeginSetting(1, 1, false);
			
			flexH.SetCol("NO_DOCU"		, "문서번호"		, false);
			flexH.SetCol("NO_EMP"		, "사번"			, false);
			flexH.SetCol("NM_PUMM"		, "제목"			, 250);
			flexH.SetCol("DT_ACCT"		, "작성일"		, 90	, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			flexH.SetCol("ST_STAT"		, "결재"			, 70);

			flexH.Cols["ST_STAT"].TextAlign = TextAlignEnum.CenterCenter;
			flexH.SetDataMap("ST_STAT", Util.GetDB_CODE("FI_J000031"), "CODE", "NAME");
			flexH.SetOneGridBinding(new object[] { }, oneH);
			flexH.VerifyNotNull = new string[] { "NM_PUMM" };

			flexH.SettingVersion = "16.01.14.01";
			flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

			// ================================================== L
			flexL.BeginSetting(1, 1, false);

			flexL.SetCol("NO_PROPOSAL"	, "신청번호"		, false);
			flexL.SetCol("NO_EMP"		, "사번"			, false);
			flexL.SetCol("CD_WCODE"		, "구분"			, 110);
			flexL.SetCol("DT_START"		, "시작일자"		, 110	, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			flexL.SetCol("DT_CLOSE"		, "종료일자"		, 110	, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			flexL.SetCol("DY_PROPOSAL"	, "신청일수"		, 70);
			flexL.SetCol("DC_RMK"		, "비고"			, 300);
			flexL.SetCol("NO_DOCU"		, "문서번호"		, false);

			flexL.Cols["DY_PROPOSAL"].Format = "0.#";
			flexL.Cols["CD_WCODE"].TextAlign = TextAlignEnum.CenterCenter;
			flexL.Cols["DY_PROPOSAL"].TextAlign = TextAlignEnum.CenterCenter;
			flexL.SetDataMap("CD_WCODE", Util.GetDB_CODE("CD_WCODE"), "CODE", "NAME");
			flexL.SetOneGridBinding(new object[] { }, oneL);

			flexL.SettingVersion = "15.11.17.04";
			flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
		}

		private void InitEvent()
		{
			btn전자결재.Click += new EventHandler(btn전자결재_Click);
			btn추가.Click += new EventHandler(btn추가_Click);
			btn삭제.Click += new EventHandler(btn삭제_Click);

			spcM.SplitterMoved += new SplitterEventHandler(spcM_SplitterMoved);
			cbo구분.SelectionChangeCommitted += new EventHandler(cbo구분_SelectionChangeCommitted);
			dtp시작일자.DateChanged += new EventHandler(dtp시작일자_DateChanged);
			dtp종료일자.DateChanged += new EventHandler(dtp종료일자_DateChanged);

			flexH.AfterRowChange += new RangeEventHandler(flexH_AfterRowChange);
			flexL.AfterRowChange += new RangeEventHandler(flexL_AfterRowChange);
		}

		protected override void InitPaint()
		{
			txt포커스.Focus();

			// 연차개수 및 잔여일수
			DataTable dt = DBMgr.GetDataTable("UP_HR_WAPPRO_SELECT", new object[] { CD_COMPANY, NO_EMP });
			if (dt.Rows.Count > 0)
			{
				lbl발생휴가.Text = string.Format("{0:0.#}", dt.Rows[0]["OCCURYEAR"]);
				lbl사용휴가.Text = string.Format("{0:0.#}", dt.Rows[0]["USEYEAR"]);
				lbl잔여휴가.Text = string.Format("{0:0.#}", dt.Rows[0]["EYEAR"]);
			}
		}

		#endregion

		#region ==================================================================================================== Event

		private void spcM_SplitterMoved(object sender, SplitterEventArgs e)
		{
			btn전자결재.Left = spcM.SplitterDistance + 690;
		}

		private void cbo구분_SelectionChangeCommitted(object sender, EventArgs e)
		{
			decimal factor = Util.GetTO_Decimal(((DataTable)cbo구분.DataSource).Select("CODE = '" + cbo구분.SelectedValue + "'")[0]["DY_WOCCUR"]);			

			if (factor > 0 && factor < 1)
			{
				dtp종료일자.Text = dtp시작일자.Text;
				dtp종료일자.Enabled = false;
			}
			else
			{
				dtp종료일자.Enabled = true;
			}

			CalcAtt();
		}

		private void dtp시작일자_DateChanged(object sender, EventArgs e)
		{
			if (!useEvent) return;
			flexL["DT_START"] = dtp시작일자.Text;
			decimal factor = Util.GetTO_Decimal(((DataTable)cbo구분.DataSource).Select("CODE = '" + cbo구분.SelectedValue + "'")[0]["DY_WOCCUR"]);

			if (factor > 0 && factor < 1)
			{
				dtp종료일자.Text = dtp시작일자.Text;
			}
			
			CalcAtt();
		}

		private void dtp종료일자_DateChanged(object sender, EventArgs e)
		{
			if (!useEvent) return;
			flexL["DT_CLOSE"] = dtp종료일자.Text;

			CalcAtt();
		}

		private void CalcAtt()
		{
			if (dtp시작일자.Text == "") return;
			if (dtp종료일자.Text == "") return;
			if (cbo구분.SelectedIndex <= 0) { flexL["DY_PROPOSAL"] = 0; return; }

			decimal factor = Util.GetTO_Decimal(((DataTable)cbo구분.DataSource).Select("CODE = '" + cbo구분.SelectedValue + "'")[0]["DY_WOCCUR"]);

			if (factor > 0 && factor < 1)
			{
				flexL["DY_PROPOSAL"] = factor;
			}
			else
			{
				// 총일수
				int days = (Util.GetTO_Date(dtp종료일자.Text) - Util.GetTO_Date(dtp시작일자.Text)).Days + 1;

				// 휴일일수
				SqlParameter outPar = new SqlParameter("@P_DAY", DbType.Int32);
				outPar.Direction = ParameterDirection.Output;

				DBMgr db = new DBMgr(DBConn.iU);
				db.Procedure = "UP_HR_WAPPRO_SELECT2";
				db.AddParameter("@P_CD_COMPANY", CD_COMPANY);
				db.AddParameter("@P_CD_BIZAREA", CD_BIZAREA);
				db.AddParameter("@P_NO_EMP", NO_EMP);
				db.AddParameter("@P_DT_FROM", dtp시작일자.Text);
				db.AddParameter("@P_DT_TO", dtp종료일자.Text);
				db.AddParameter("@P_CD_WCODE", cbo구분.SelectedValue);
				db.AddParameter(outPar);
				db.ExecuteNonQuery();

				// 신청일수
				if (Util.GetTO_Decimal(flexL["DY_PROPOSAL"]) != days - Util.GetTO_Decimal(outPar.Value))
				{
					flexL["DY_PROPOSAL"] = days - Util.GetTO_Decimal(outPar.Value);
				}
			}
		}

		#endregion 

		#region ==================================================================================================== 버튼 이벤트

		private void btn전자결재_Click(object sender, EventArgs e)
		{
			// 결재 상태 체크
			string query = "SELECT ST_STAT FROM FI_GWDOCU WHERE NO_DOCU = '" + NO_DOCU + "'";
			DataTable dt = DBMgr.GetDataTable(query);
			string ST_STAT = dt.Rows[0]["ST_STAT"].ToString();

			if (ST_STAT == "0") { ShowMessage("결재 진행중인 문서입니다!"); return; }
			if (ST_STAT == "1") { ShowMessage("결재 완료된 문서입니다!"); return; }

			// html 만들기
			string html = @"
<div class='header'>
  ※ 휴가 신청 내역
</div>
<table>
  <tr>
    <th>순번</th>
    <th>구분</th>
    <th>시작일자</th>
    <th>종료일자</th>
    <th>신청일수</th>
    <th>사 유</th>
  </tr>";

			int idx = 1;
			for (int i = flexL.Rows.Fixed; i < flexL.Rows.Count; i++)
			{
				html += @"
  <tr>
    <td class='col1'>" + idx++ + @"</td>
    <td class='col2'>" + ((DataTable)cbo구분.DataSource).Select("CODE = '" + flexL[i, "CD_WCODE"] + "'")[0]["NAME"] + @"</td>
    <td class='col3'>" + Util.GetTo_DateStringS(flexL[i, "DT_START"]) + @"</td>
    <td class='col4'>" + Util.GetTo_DateStringS(flexL[i, "DT_CLOSE"]) + @"</td>
    <td class='col5'>" + string.Format("{0:0.#}", flexL[i, "DY_PROPOSAL"]) + @"</td>
    <td class='col6'>" + flexL[i, "DC_RMK"] + @"</td>
  </tr>";
			}

			decimal sum = GetTo.Decimal(flexL.DataTable.Compute("SUM(DY_PROPOSAL)", "NO_DOCU = '" + NO_DOCU + "' AND (CD_WCODE = 'G05' OR CD_WCODE = 'G18' OR CD_WCODE = 'G19')"));
			html += @"
   <tr>
    <th colspan='4'>총 사용일수</th>
    <th>" + string.Format("{0:0.#}", sum) + @"</th>
    <th>잔여일수 : " + string.Format("{0:0.#}", GetTo.Decimal(lbl잔여휴가.Text) - sum) + @"</th>
  </tr>
</table>
<div class='footer'>
  위의 사유로 휴가원을 제출합니다.
</div>";

			// html 업데이트 및 전자결재 팝업
			query = "UPDATE FI_GWDOCU SET NM_NOTE = '" + html.Replace("'", "''") + "' WHERE NO_DOCU = '" + NO_DOCU + "'";
			DBMgr.ExecuteNonQuery(query);
			GroupWare.Popup(NO_DOCU);
		}

		private void btn추가_Click(object sender, EventArgs e)
		{
			flexL.Rows.Add();
			flexL.Row = flexL.Rows.Count - 1;
			flexL["NO_EMP"] = NO_EMP;
			flexL["NO_DOCU"] = NO_DOCU;
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
			if (flexH.DetailQueryNeed) dtL = DBMgr.GetDataTable("SP_CZ_HR_WAPPLYL_REG_SELECT", new object[] { CD_COMPANY, NO_EMP, NO_DOCU });
			flexL.BindingAdd(dtL, "NO_DOCU = '" + NO_DOCU + "'");			

			// 상태 결정
			if (Util.GetTO_String(flexH["ST_STAT"]) == "0" || Util.GetTO_String(flexH["ST_STAT"]) == "1")
			{
				btn전자결재.Enabled = false;
				btn추가.Enabled = false;
				btn삭제.Enabled = false;
				Util.SetCON_ReadOnly(pnl제목, true);
				Util.SetCON_ReadOnly(pnl구분, true);
				Util.SetCON_ReadOnly(pnl기간, true);
				Util.SetCON_ReadOnly(pnl사유, true);
				flexL.AllowEditing = false;				
			}
			else
			{
				btn전자결재.Enabled = true;
				btn추가.Enabled = true;
				btn삭제.Enabled = true;
				Util.SetCON_ReadOnly(pnl제목, false);
				Util.SetCON_ReadOnly(pnl구분, false);
				Util.SetCON_ReadOnly(pnl기간, false);
				Util.SetCON_ReadOnly(pnl사유, false);
				flexL.AllowEditing = true;
			}
		}

		private void flexL_AfterRowChange(object sender, RangeEventArgs e)
		{
			useEvent = false;

			dtp시작일자.Text = flexL["DT_START"].ToString();
			dtp종료일자.Text = flexL["DT_CLOSE"].ToString();

			useEvent = true;
		}

		#endregion

		#region ==================================================================================================== Search

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_HR_WAPPLYH_REG_SELECT", new object[] { CD_COMPANY, NO_EMP });

			flexH.Redraw = false;
			flexL.Redraw = false;

			flexH.Binding = dt;
			flexH.Row = flexH.Rows.Count - 1;

			flexH.Redraw = true;
			flexL.Redraw = true;

			if (!flexH.HasNormalRow) ShowMessage(공통메세지.조건에해당하는내용이없습니다);
		}

		#endregion

		#region ==================================================================================================== Add

		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
			DataTable dtH = flexH.DataTable;
			if (dtH.Select("NO_DOCU = 'NEW'").Length > 0) { ShowMessage("작성중인 문서가 있습니다."); return; }

			flexH.Rows.Add();
			flexH.Row = flexH.Rows.Count - 1;
			flexH["NO_DOCU"] = "NEW";
			flexH["NO_EMP"] = NO_EMP;
			flexH["NM_PUMM"] = NM_EMP + "_" + "휴가신청";
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
			OnToolBarSearchButtonClicked(null, null);
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
			DataTable dtL = flexL.GetChanges();

			if (dtH != null)
			{
				dtH.Columns.Add("CD_COMPANY_REAL", typeof(string), "'" + CD_COMPANY + "'");
				dtH.Columns.Add("CD_COMPANY"	 , typeof(string), "'" + GroupWare.GetERP_CD_COMPANY() + "'");
				dtH.Columns.Add("CD_PC"			 , typeof(string), "'" + GroupWare.GetERP_CD_PC() + "'");
			}

			string xmlH = Util.GetTO_Xml(dtH);
			string xmlL = Util.GetTO_Xml(dtL);
			DataTable dtR = DBMgr.GetDataTable("SP_CZ_HR_WAPPLY_REG_XML", new object[] { xmlH, xmlL });

			// L 그리드 NO_DOCU 변경
			if (dtR.Rows.Count > 0)
			{
				string NO_DOCU = dtR.Rows[0]["NO_DOCU"].ToString();
				foreach (DataRow row in flexL.DataTable.Select("NO_DOCU = 'NEW'")) row["NO_DOCU"] = NO_DOCU;
			}

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
	}
}
